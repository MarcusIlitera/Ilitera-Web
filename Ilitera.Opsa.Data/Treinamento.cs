using System;
using System.Collections;
using Ilitera.Data;
using Ilitera.Common;
using System.Data;
using System.Text;

namespace Ilitera.Opsa.Data
{
	/// <summary>
	/// Summary description
	/// </summary>
	/// 
	[Database("opsa","Treinamento","IdTreinamento","","Treinamento")]
	public class Treinamento : Documento
	{
		private int _IdTreinamento;
		private TreinamentoDicionario _IdTreinamentoDicionario;
		private bool _IsFromCliente;
		private Prestador _IdResponsavel;
		private string _Periodo=string.Empty;
		private string _TextoColetivo=string.Empty;
		private string _TextoIndividual=string.Empty;
        private int _Carga_Horaria;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public Treinamento()
		{
		}

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public Treinamento(int Id)
		{
			this.Find(Id);
		}
        public override int Id
		{	
			get{return _IdTreinamento;}
			set{_IdTreinamento = value;}
		}
		public TreinamentoDicionario IdTreinamentoDicionario
		{	
			get{return _IdTreinamentoDicionario;}
			set{_IdTreinamentoDicionario = value;}
		}
		public bool IsFromCliente
		{	
			get{return _IsFromCliente;}
			set{_IsFromCliente = value;}
		}
		public Prestador IdResponsavel
		{	
			get{return _IdResponsavel;}
			set{_IdResponsavel = value;}
		}
		public string Periodo
		{	
			get{return _Periodo;}
			set{_Periodo = value;}
		}
        public string TextoColetivo
		{
			get{return _TextoColetivo;}
			set{_TextoColetivo = value;}
		}
		public string TextoIndividual
		{
			get{return _TextoIndividual;}
			set{_TextoIndividual = value;}
		}
        public int Carga_Horaria
        {
            get { return _Carga_Horaria; }
            set { _Carga_Horaria = value; }
        }


        public static Treinamento GetTreinamento(Pedido pedido)
        {
            Treinamento treinamento = new Treinamento();
            treinamento.Find("IdPedido=" + pedido.Id);

            if (treinamento.Id == 0)
            {
                if (pedido.DataConclusao != new DateTime())
                    throw new Exception("Treinamento não localizado!");

                pedido.IdPedidoGrupo.Find();
                pedido.IdPedidoGrupo.IdCompromisso.Find();

                treinamento.Inicialize();
                treinamento.IdDocumentoBase.Id = (int)Documentos.Treinamentos;
                treinamento.IdPedido = pedido;
                treinamento.IdCliente = pedido.IdCliente;
                treinamento.IdPrestador = pedido.IdPrestador;
                
                ArrayList listTreinamentoDic = new TreinamentoDicionario().Find("IdObrigacao=" + pedido.IdObrigacao.Id);
                
                if (listTreinamentoDic.Count > 0)
                {
                    treinamento.IdTreinamentoDicionario = (TreinamentoDicionario)listTreinamentoDic[0];
                    treinamento.TextoIndividual = ((TreinamentoDicionario)listTreinamentoDic[0]).TextoIndividual;
                    treinamento.TextoColetivo = ((TreinamentoDicionario)listTreinamentoDic[0]).TextoColetivo;
                }
                if (pedido.IdPedidoGrupo.IdCompromisso.Id == 0)
                {
                    treinamento.DataLevantamento = new DateTime(pedido.DataSolicitacao.Year,
                                                                pedido.DataSolicitacao.Month,
                                                                pedido.DataSolicitacao.Day);

                    treinamento.Periodo = pedido.DataSolicitacao.ToString("dd-MM-yyyy");
                }
                else
                {
                    DateTime dataTreinamento = pedido.IdPedidoGrupo.IdCompromisso.DataInicio;

                    treinamento.DataLevantamento = new DateTime(dataTreinamento.Year,
                                                                dataTreinamento.Month,
                                                                dataTreinamento.Day);

                    treinamento.Periodo = pedido.IdPedidoGrupo.IdCompromisso.GetPeriodo();
                }
            }
            return treinamento;
        }
        		
		public static Treinamento GetTreinamentoSemPedido(Cliente cliente, TreinamentoDicionario treinamentoDicionario)
		{
			Treinamento treinamento = new Treinamento();
			treinamento.Inicialize();

			treinamento.Inicialize();
			treinamento.IdDocumentoBase.Id	= (int)Documentos.Treinamentos;
			treinamento.IdCliente			= cliente;
			treinamento.IdPrestador.Id		= 20; //Tenente
			treinamento.IdResponsavel.Id	= 20; //Tenente
			
			if(treinamentoDicionario.Id!=0)
			{
				treinamento.IdTreinamentoDicionario.Id = treinamentoDicionario.Id;
				treinamento.TextoIndividual	= treinamentoDicionario.TextoIndividual;
				treinamento.TextoColetivo	= treinamentoDicionario.TextoColetivo;
			}
			treinamento.DataLevantamento = DateTime.Today;
            treinamento.Periodo = treinamento.DataLevantamento.ToString("dd-MM-yyyy");

			return treinamento;
		}
	}
	
	[Database("opsa","TreinamentoDicionario","IdTreinamentoDicionario","","Treinamento Dicionário")]
	public class TreinamentoDicionario : Table
	{
		private int _IdTreinamentoDicionario;
		private Obrigacao _IdObrigacao;
		private string _Nome = string.Empty;
		private string _TextoColetivo = string.Empty;
		private string _TextoIndividual = string.Empty;
		private Cliente _IdCliente;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public TreinamentoDicionario()
		{
		}

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public TreinamentoDicionario(int Id)
		{
			this.Find(Id);
		}
		public override int Id
		{
			get{return _IdTreinamentoDicionario;}
			set{_IdTreinamentoDicionario = value;}
		}
		public Obrigacao IdObrigacao
		{
			get{return _IdObrigacao;}
			set{_IdObrigacao = value;}
		}
		public string Nome
		{
			get{return _Nome;}
			set{_Nome = value;}
		}
		public string TextoColetivo
		{
			get{return _TextoColetivo;}
			set{_TextoColetivo = value;}
		}
		public string TextoIndividual
		{
			get{return _TextoIndividual;}
			set{_TextoIndividual = value;}
		}

		public override string ToString()
		{
			string ret;

			if(this.IdObrigacao==null)
				this.Find();
			
			if(this.IdObrigacao.Id!=0)
			{
				if(this.IdObrigacao.IdDocumentoBase==null)
					this.IdObrigacao.Find();

				ret = this.IdObrigacao.NomeReduzido;
			}
			else
			{
				ret = this.Nome;
			}

			return ret;
		}

		public Cliente IdCliente
		{
			get{return _IdCliente;}
			set{_IdCliente = value;}
		}
	}

    [Database("opsa", "ParticipanteTreinamento", "IdParticipanteTreinamento", "", "Participante de Treinamento")]
    public class ParticipanteTreinamento : Table
    {
        private int _IdParticipanteTreinamento;
        private Treinamento _IdTreinamento;
        private Empregado _IdEmpregado;
        private string _NomeParticipante = string.Empty;
        private string _IdentidadeParticipante = string.Empty;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public ParticipanteTreinamento()
        {
        }

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public ParticipanteTreinamento(int Id)
        {
            this.Find(Id);
        }
        public override int Id
        {
            get { return _IdParticipanteTreinamento; }
            set { _IdParticipanteTreinamento = value; }
        }
        public Treinamento IdTreinamento
        {
            get { return _IdTreinamento; }
            set { _IdTreinamento = value; }
        }
        public Empregado IdEmpregado
        {
            get { return _IdEmpregado; }
            set { _IdEmpregado = value; }
        }
        public string NomeParticipante
        {
            get { return _NomeParticipante; }
            set { _NomeParticipante = value; }
        }
        public string IdentidadeParticipante
        {
            get { return _IdentidadeParticipante; }
            set { _IdentidadeParticipante = value; }
        }
        public string GetIdentidadeParticipante()
        {
            string ret;

            if (this.IdEmpregado.Id != 0 && this.IdentidadeParticipante == string.Empty)
            {
                if (this.IdEmpregado.mirrorOld == null)
                    this.IdEmpregado.Find();

                ret = this.IdEmpregado.tNO_IDENTIDADE;
            }
            else
                ret = this.IdentidadeParticipante;

            return ret;
        }

        public string GetNomeParticipante()
        {
            string ret;

            if (this.IdEmpregado.Id == 0)
                ret = this.NomeParticipante;
            else
                ret = this.IdEmpregado.ToString();

            return ret;
        }

        public string GetCPFParticipante()
        {
            string ret;

            if (this.IdEmpregado.Id != 0)
            {
                if (this.IdEmpregado.mirrorOld == null)
                    this.IdEmpregado.Find();

                ret = this.IdEmpregado.tNO_CPF;
            }
            else
                ret = "";

            return ret;
        }



        public string GetNomeCPFParticipante()
        {
            string ret;

            //if (this.GetIdentidadeParticipante() == string.Empty)
            //    ret = this.ToString();
            //else
            //{
            //    if (this.GetIdentidadeParticipante().ToUpper().IndexOf("RG") == -1)
            //        ret = this.GetNomeParticipante() + " - RG " + this.GetIdentidadeParticipante();
            //    else
            //        ret = this.GetNomeParticipante() + " - " + this.GetIdentidadeParticipante();
            //}

            if (this.GetCPFParticipante() == string.Empty)
                ret = this.ToString();
            else
            {
                if (this.GetCPFParticipante().ToUpper().IndexOf("CPF") == -1)
                    ret = this.GetNomeParticipante() + " - CPF " + this.GetCPFParticipante();
                else
                    ret = this.GetNomeParticipante() + " - " + this.GetCPFParticipante();
            }
            return ret;

        }

        public override string ToString()
        {
            return GetNomeParticipante();
        }
    }

	[Database("opsa","TreinamentoDicionarioEmpregado","IdTreinamentoDicionarioEmpregado")]
	public class TreinamentoDicionarioEmpregado : Table
	{
		private int _IdTreinamentoDicionarioEmpregado;
		private TreinamentoDicionario _IdTreinamentoDicionario;
		private Empregado _IdEmpregado;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public TreinamentoDicionarioEmpregado()
		{
		}

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public TreinamentoDicionarioEmpregado(int Id)
		{
			this.Find(Id);
		}
		public override int Id
		{
			get{return _IdTreinamentoDicionarioEmpregado;}
			set{_IdTreinamentoDicionarioEmpregado = value;}
		}
		public TreinamentoDicionario IdTreinamentoDicionario
		{
			get{return _IdTreinamentoDicionario;}
			set{_IdTreinamentoDicionario = value;}
		}
		public Empregado IdEmpregado
		{
			get{return _IdEmpregado;}
			set{_IdEmpregado = value;}
		}
	}
}
