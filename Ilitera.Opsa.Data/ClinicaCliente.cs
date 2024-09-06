using System;
using System.Data;
using System.Text;
using System.Collections;
using System.Collections.Generic;

using Ilitera.Common;
using Ilitera.Data;

namespace Ilitera.Opsa.Data
{
	[Database("opsa", "ClinicaCliente", "IdClinicaCliente")]
	public class ClinicaCliente : Ilitera.Data.Table 
	{
		private int	_IdClinicaCliente;
		private Clinica	_IdClinica;
		private Cliente	_IdCliente;
		private bool _ClinicaPadrao;
		private DateTime _DataEnvioAviso;


        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public ClinicaCliente()
		{			

		}

		public override int Id
		{														  
			get{return _IdClinicaCliente;}
			set	{_IdClinicaCliente = value;}
		}
		public Clinica IdClinica 
		{														  
			get{return _IdClinica;}
			set	{_IdClinica = value;}
		}
		public Cliente IdCliente 
		{														  
			get{return _IdCliente;}
			set	{_IdCliente = value;}
		}		
		public bool ClinicaPadrao 
		{														  
			get{return _ClinicaPadrao;}
			set	{_ClinicaPadrao = value;}
		}
		public DateTime DataEnvioAviso 
		{														  
			get{return _DataEnvioAviso;}
			set	{_DataEnvioAviso = value;}
		}
		public override string ToString()
		{
			if(this.IdClinica.mirrorOld==null)
				this.IdClinica.Find();

			return this.IdClinica.NomeAbreviado;
		}

		public void SetClinicaPadrao(bool bPadrao)
		{
			if(bPadrao)
			{
				ArrayList listClinicaCliente = new ClinicaCliente().Find("IdCliente="+this.IdCliente.Id);
				foreach(ClinicaCliente clinicaCliente in listClinicaCliente)
				{
					clinicaCliente.ClinicaPadrao = false;
					clinicaCliente.Save();
				}
			}
			this.ClinicaPadrao = bPadrao;
			this.Save();
		}

        public override int Save()
        {
            if (this.IdCliente.mirrorOld == null)
                this.IdCliente.Find();

            if (this.IdCliente.IdJuridicaPai.Id != 0)
            {
                ClinicaCliente clinicaCliente = new ClinicaCliente();
                clinicaCliente.Find("IdClinica=" + this.IdClinica.Id + " AND IdCliente=" + this.IdCliente.IdJuridicaPai.Id);

                if (clinicaCliente.Id == 0)
                    throw new Exception("Essa clínica só pode ser credenciada para esse local de trabalho se a empresa "
                            + this.IdCliente.IdJuridicaPai.ToString() 
                            + " também estiver credenciada para essa clínica!");
            }

            try
            {
                return base.Save();
            }
            catch (Exception ex)
            {
                if (ex.Message.IndexOf("IX_ClinicaCliente") != -1)
                    throw new Exception("A clínica '" + this.IdClinica.ToString()
                                        + "' já está credenciada para o cliente '" + this.IdCliente.ToString() + "'!");
                else
                    throw ex;
            }
        }

        public static void CredenciarClinica(Cliente cliente, Clinica clinica, bool IsPadrao)
        {
            ClinicaCliente clinicaCliente = new ClinicaCliente();
            clinicaCliente.Find("IdCliente=" + cliente.Id + " AND IdClinica=" + clinica.Id);

            if (clinicaCliente.Id != 0)
                throw new Exception("A Clinica '" + clinica.ToString() + "' já credenciada para o cliente '" + cliente.ToString() + "'!");

            clinicaCliente.Inicialize();
            clinicaCliente.IdCliente.Id = cliente.Id;
            clinicaCliente.IdClinica.Id = clinica.Id;
            clinicaCliente.ClinicaPadrao = IsPadrao;
            clinicaCliente.Save();
            clinicaCliente.GerarClinicaClienteExameDicionario();
        }

        public static void GerarClinicaClienteExameDicionarioParaTodosClientes()
        {
            List<Cliente> clientes = new Cliente().Find<Cliente>("IdJuridicaPapel=" + (int)IndJuridicaPapel.Cliente
                            + " AND ContrataPCMSO<>" + (int)TipoPcmsoContratada.NaoContratada
                            + " AND IdCliente IN (SELECT IdCliente FROM qryClienteAtivos)");

            foreach (Cliente cliente in clientes)
                GerarClinicaClienteExameDicionario(cliente);
        }

        public static void GerarClinicaClienteExameDicionario(Cliente cliente)
        {
            Pcmso pcmso = cliente.GetUltimoPcmso();

            List<ClinicaCliente> credendiados = new ClinicaCliente().Find<ClinicaCliente>("IdCliente=" + cliente.Id);

            foreach (ClinicaCliente clinicaCliente in credendiados)
                clinicaCliente.GerarExamesClinicaClienteExameDicionario(pcmso);
        }

        public void GerarClinicaClienteExameDicionario()
        {
            Pcmso pcmso = this.IdCliente.GetUltimoPcmso();

            GerarExamesClinicaClienteExameDicionario(pcmso);
        }

        public void GerarExamesClinicaClienteExameDicionario(Pcmso pcmso)
        {
            StringBuilder strFind = new StringBuilder();

            if (pcmso.Id != 0)
                strFind.Append("(IdExameDicionario IN (SELECT IdExameDicionario "
                                + " FROM PcmsoPlanejamento WHERE IdPcmso=" + pcmso.Id + ")"
                                + " OR IdExameDicionario IN (1,2,3,4,5))"
                                + " AND IdClinica =" + this.IdClinica.Id);
            else
                strFind.Append("IdClinica=" + this.IdClinica.Id);

            List<ClinicaExameDicionario> 
                list = new ClinicaExameDicionario().Find<ClinicaExameDicionario>(strFind.ToString());

            foreach (ClinicaExameDicionario clinicaExameDicionario in list)
            {
                ClinicaClienteExameDicionario
                clinicaClienteExameDicionario = new ClinicaClienteExameDicionario();
                clinicaClienteExameDicionario.Find("IdClinicaCliente=" + this.Id
                                                + " AND IdClinicaExameDicionario=" + clinicaExameDicionario.Id);

                if (clinicaClienteExameDicionario.Id != 0)
                    continue;

                clinicaClienteExameDicionario.Inicialize();
                clinicaClienteExameDicionario.IdClinicaCliente = this;
                clinicaClienteExameDicionario.IdClinicaExameDicionario = clinicaExameDicionario;
                clinicaClienteExameDicionario.IsAutorizado = true;
                clinicaClienteExameDicionario.Save();
            }
        }

        public void EnviarEmail(Usuario usuario)
        {

        }

        private void ValidadeAvisoCredenciamentoClinica()
        {
            if (this.IdClinica.mirrorOld == null)
                this.IdClinica.Find();

            if (this.IdCliente.mirrorOld == null)
                this.IdCliente.Find();

            if (this.IdClinica.Id == (int)Empresas.MestraPaulista)
                throw new Exception("Não é possível enviar esse email para a Clínica Mestra!");

            if (this.IdClinica.IsClinicaInterna)
                throw new Exception("Não é possível enviar esse email para a Clínica do tipo interna!");

            if (this.IdClinica.IdJuridicaPapel.Id == (int)IndJuridicaPapel.ClinicaOutras)
                throw new Exception("Não é possível enviar esse email para a Clínica Outras!");

            if (this.IdClinica.Email == string.Empty)
                throw new Exception("Email não cadastrado para a clínica!");
        }

        public string GetBodyAvisoCredenciamentoClinica()
        {
            ValidadeAvisoCredenciamentoClinica();

            StringBuilder emailBody = new StringBuilder();

            return emailBody.ToString();
        }
	}
}
