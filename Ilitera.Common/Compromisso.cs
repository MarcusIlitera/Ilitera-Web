using System;
using System.Data;
using System.Text;
using System.Collections;
using System.Collections.Generic;

using Ilitera.Data;

namespace Ilitera.Common
{
    #region  IAgendar

    public interface IAgendar
	{
		Compromisso IdCompromisso
		{
			get;
			set;
		}
		Tarefa IdTarefa
		{
			get;
			set;
		}
    }
    #endregion

    [Database("opsa","Compromisso","IdCompromisso")]
    public class Compromisso : Ilitera.Data.Table, Ilitera.Common.IDataInicioTermino
    {
        #region Properties
        private int _IdCompromisso;
		private Pessoa _IdPessoa;
		private Pessoa _IdPessoaContato;
		private Pessoa _IdPessoaCriador;
		private DateTime _DataInicio;
		private DateTime _DataTermino;
		private bool _ODiaTodo;
		private string _Assunto=string.Empty;
		private string _Descricao=string.Empty;
		private string _ColorName=string.Empty;
		private Categoria _IdCategoria;
		private Repetir _IdRepetir;
		private bool _AvisoConflito=true;
		private bool _AConfirmar;
		private bool _HorarioDisponivel;
		private bool _Particular;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public Compromisso()
		{

		}
		public override int Id
		{
			get{return _IdCompromisso;}
			set{_IdCompromisso = value;}
		}
		[Obrigatorio(true, "A pessoa é obrigatório!")]
		public Pessoa IdPessoa
		{
			get{return _IdPessoa;}
			set{_IdPessoa = value;}
		}
		public Pessoa IdPessoaContato
		{
			get{return _IdPessoaContato;}
			set{_IdPessoaContato = value;}
		}
		[Obrigatorio(true, "A pessoa que criou o compromisso é obrigatório!")]
		public Pessoa IdPessoaCriador
		{
			get{return _IdPessoaCriador;}
			set{_IdPessoaCriador = value;}
		}
		[Obrigatorio(true, "A Data de Inicio é obrigatório!")]
		public DateTime DataInicio
		{
			get{return _DataInicio;}
			set{_DataInicio = value;}
		}
		[Obrigatorio(true, "A Data de Término é obrigatório")]
		public DateTime DataTermino
		{
			get{return _DataTermino;}
			set{_DataTermino = value;}
		}
		public bool ODiaTodo
		{
			get{return _ODiaTodo;}
			set{_ODiaTodo = value;}
		}
		[Obrigatorio(true, "O Assunto é obrigatório")]
		public string Assunto
		{
			get{return _Assunto;}
			set{_Assunto = value;}
		}
		public string Descricao
		{
			get{return _Descricao;}
			set{_Descricao = value;}
		}
		public string ColorName
		{
			get{return _ColorName;}
			set{_ColorName = value;}
		}
		[Obrigatorio(true, "A Categoria é obrigatório")]
		public Categoria IdCategoria
		{
			get{return _IdCategoria;}
			set{_IdCategoria = value;}
		}
		public Repetir IdRepetir
		{
			get{return _IdRepetir;}
			set{_IdRepetir = value;}
		}
		public bool AvisoConflito
		{
			get{return _AvisoConflito;}
			set{_AvisoConflito = value;}
		}
		public bool AConfirmar
		{
			get{return _AConfirmar;}
			set{_AConfirmar = value;}
		}
		public bool HorarioDisponivel
		{
			get{return _HorarioDisponivel;}
			set{_HorarioDisponivel = value;}
		}
		public bool Particular
		{
			get{return _Particular;}
			set{_Particular = value;}
		}
        #endregion

        #region Metodos

        #region override ToString

        public override string ToString()
        {
            return this.GetDataComprimisso() + " - " + this.Assunto;
        }
        #endregion

        #region SetHorarioDisponivel

        public void SetHorarioDisponivel(DateTime data, string horaAgendamento, int duracao)
		{
			int hora	= Convert.ToInt32(horaAgendamento.Substring(0,2));
			int minuto	= Convert.ToInt32(horaAgendamento.Substring(3,2));
			this.SetHorarioDisponivel(data, horaAgendamento, duracao, hora, minuto);
        }
       

        public void SetHorarioDisponivel(DateTime data, string horaAgendamento, int duracao, int hora, int minuto)
		{
			this.DataInicio	= new DateTime(data.Year, data.Month, data.Day, hora, minuto, 0);
			this.DataTermino= this.DataInicio.AddMinutes(duracao);
		}
        #endregion

        #region GetDataComprimisso

        public string GetDataComprimisso()
        {
            if (this.mirrorOld == null)
                this.Find();

            string strData = string.Empty;

            if (this.Id != 0)
            {
                if (this.ODiaTodo)
                    strData = this.DataInicio.ToString("dd-MM-yyyy");
                else
                    strData = this.DataInicio.ToString("dd-MM-yyyy")
                        + " das " + this.DataInicio.ToString("t")
                        + " às " + this.DataTermino.ToString("t");
            }

            return strData;
        }
        #endregion

        #region GetShortDataComprimisso

        public string GetShortDataComprimisso()
        {
            if (this.mirrorOld == null)
                this.Find();

            StringBuilder ret = new StringBuilder();

            if (this.Id == 0)
                return string.Empty;

            if (this.IdRepetir.Id == 0)
            {
                ret.Append(this.DataInicio.ToString("dd-MM-yy"));
            }
            else
            {
                List<Compromisso>
                    compromissos = new Compromisso().Find<Compromisso>("IdRepetir=" + this.IdRepetir.Id
                    + " ORDER BY DataInicio");

                foreach (Compromisso compromisso in compromissos)
                {
                    if (compromisso.Id != compromissos[compromissos.Count - 1].Id)
                        ret.Append(compromisso.DataInicio.Day.ToString() + ", ");
                    else
                        ret.Append(compromisso.DataInicio.ToString("dd-MM-yy"));
                }
            }

            return ret.ToString();
        }
        #endregion

        #region GetHoraComprimisso

        public string GetHoraComprimisso()
        {
            string strData = string.Empty;

            if (this.Id != 0)
            {
                if (this.ODiaTodo)
                    strData = "O dia todo.";
                else
                    strData = this.DataInicio.ToString("t")
                        + " às " + this.DataTermino.ToString("t");
            }

            return strData;
        }
        #endregion

        #region GetPeriodo

        public string GetPeriodo()
        {
            StringBuilder str = new StringBuilder();

            if (this.IdRepetir.Id != 0)
            {
                string where = "IdRepetir=" + this.IdRepetir.Id + " ORDER BY DataInicio";

                ArrayList listCompromissos = new Compromisso().Find(where);

                foreach (Compromisso compromisso in listCompromissos)
                    str.Append(compromisso.DataInicio.Day + ",");

                str.Remove(str.ToString().Length - 1, 1);
                str.Append(" de " + this.DataInicio.ToString("MMMM"));
                str.Append(" de " + this.DataInicio.ToString("yyyy"));
            }
            else
                str.Append(this.DataInicio.ToString("d \"de\" MMMM \"de\" yyyy"));

            return str.ToString();
        }
        #endregion

        #region GetAviso

        public Aviso GetAviso()
        {
            Aviso aviso = new Aviso();
            aviso.Find("IdCompromisso=" + this.Id);

            if (aviso.Id == 0)
            {
                aviso.Inicialize();
                aviso.IdCompromisso = this;
            }

            return aviso;
        }
        #endregion

        #region VerificarPermissaoHorarioDisponivel

        public void VerificarPermissaoHorarioDisponivel()
		{
			if (this.UsuarioId != 0)
			{
				Usuario usuario = new Usuario(UsuarioId);

				if( this.HorarioDisponivel 
					&& this.IdPessoaCriador.Id!= usuario.IdPessoa.Id
					&& ! usuario.IsGrupo(Grupos.Administracao))
				{
					this.IdPessoaCriador.Find();
					throw new Exception("Este compromisso só pode ser alterado por "+this.IdPessoaCriador.NomeAbreviado+"!");
				}
			}
        }
        #endregion

        #region VerificarPermissaoParticular

        public void VerificarPermissaoParticular()
        {
            if (this.UsuarioId != 0)
            {
                Usuario usuario = new Usuario(UsuarioId);

                if (this.Particular
                    && this.IdPessoaCriador.Id != usuario.IdPessoa.Id)
                {
                    this.IdPessoaCriador.Find();
                    throw new Exception("Este compromisso só pode ser alterado por " + this.IdPessoaCriador.NomeAbreviado + "!");
                }
            }
        }
        #endregion

        #region VerificarPermissaoCadastro

        public void VerificarPermissaoCadastro()
        {
            if (this.UsuarioId != 0)
            {
                Usuario usuarioCriador = new Usuario();
                usuarioCriador.Find("IdPessoa=" + this.IdPessoaCriador.Id);

                if (usuarioCriador.IsGrupo(Grupos.Administracao))
                {
                    Usuario usuario = new Usuario(UsuarioId);

                    JuridicaPessoa juridicaPessoa = new JuridicaPessoa();
                    juridicaPessoa.Find("IdPessoa=" + usuario.IdPessoa.Id
                        + " AND JuridicaPessoa.IdJuridicaPessoa IN (SELECT IdPrestador FROM Prestador)");

                    if (juridicaPessoa.IdJuridica.Id == 310 //Mestra
                        && this.IdPessoaCriador.Id != usuario.IdPessoa.Id
                        && this.IdPessoa.Id != usuario.IdPessoa.Id
                        && !usuario.IsGrupo(Grupos.Administracao))
                            throw new Exception("Este compromisso só pode ser alterado ou excluído pelo usuário "
                                    + this.IdPessoaCriador.ToString() + "!");
                }
            }
        }
        #endregion

        #region VerificaConflitoHorarioDisponivel

        private void VerificaConflitoHorarioDisponivel()
        {
            if (this.HorarioDisponivel)
            {
                DataSet dsHorarioDisponivel = new Compromisso().Get("DataInicio<'" + this.DataTermino.ToString("yyyy-MM-dd") + " " + this.DataTermino.ToString("T")
                    + "' AND DataTermino>'" + this.DataInicio.ToString("yyyy-MM-dd") + " " + this.DataInicio.ToString("T")
                    + "' AND IdCategoria=" + (int)Categorias.Medico
                    + " AND HorarioDisponivel=1"
                    + " AND IdPessoa=" + this.IdPessoa.Id
                    + " AND IdCompromisso<>" + this.Id);

                if (dsHorarioDisponivel.Tables[0].Rows.Count > 0)
                    throw new Exception("Não é possível cadastrar ou alterar o compromisso de disponibilidade! Há compromissos de disponibilidade já cadastrados dentro do período informado!");
            }
        }
        #endregion

        #region override Validate

        public override void Validate()
		{
            if (this.DataInicio!= new DateTime() && this.DataInicio > this.DataTermino)
				throw new Exception("A Data de Inicio deve ser menor que a Data de Término!");

			VerificarPermissaoHorarioDisponivel();
			VerificarPermissaoParticular();
			VerificarPermissaoCadastro();
            VerificaConflitoHorarioDisponivel();

			base.Validate();
        }
        #endregion

        #region override Delete

        public override void Delete()
		{
			try
			{
				Validate();

				base.Delete();
			}
			catch(Exception ex)
			{
				if(ex.Message.IndexOf("FK_ObrigacaoCliente_Compromisso")!=-1)
					throw new Exception("Compromisso relacionado com a previsão de realização, só pode ser cancelado através do cadastro de previsão");
				else if(ex.Message.IndexOf("FK_PedidoGrupo_Compromisso")!=-1)
					throw new Exception("Compromisso relacionado com pedido, só pode ser cancelado através do cadastro de pedidos");
				else if(ex.Message.IndexOf("FK_ExameBase_Compromisso")!=-1)
					throw new Exception("Compromisso relacionado com exame, só pode ser excluído através do cadastro de exames.");
				else if(ex.Message.IndexOf("FK_Telemarketing_Compromisso")!=-1)
					throw new Exception("Compromisso relacionado com Telemarketing, só pode ser excluído através do cadastro de Telemarketing.");
				else if(ex.Message.IndexOf("FK_Compromisso_Repetir")!=-1)
				{
					new Repetir().Delete("IdRepetir="+this.IdRepetir.Id);
					base.Delete();
				}
				else
					throw ex;
			}
        }
        #endregion

        #endregion
    }       
}
