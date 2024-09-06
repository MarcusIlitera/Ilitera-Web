using System;
using Ilitera.Data;
using System.Reflection;
using System.Collections;
using System.Data;
using System.Text;

namespace Ilitera.Common
{
	public enum IndStatusExecucao: int
	{
		Executando,
		ConcluidoExito,
		ErroExecucao
	}

	[Database("opsa", "ProcessoAgendamento", "IdProcessoAgendamento")]
	public class ProcessoAgendamento :  Ilitera.Data.Table
	{
		private int _IdProcessoAgendamento;
		private string _NomeProcesso=string.Empty;
        private string _Descricao = string.Empty;
		private string _ProjectName=string.Empty;
		private string _ClassName=string.Empty;
		private string _InvokeName=string.Empty;
		private int _IndPeriodicidade;
		private int _Hora;
		private Pessoa _IdOperador;
		private AvisoOperador _IndAvisoOperador;
        private TipoProcesso _IndTipoProcesso;
        private bool _NecessitaPCMSO;


        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public ProcessoAgendamento()
		{
		}

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public ProcessoAgendamento(int Id)
        {
            this.Find(Id);
        }
		public override int Id
		{														  
			get{return _IdProcessoAgendamento;}
			set	{_IdProcessoAgendamento = value;}
		}
		public string NomeProcesso
		{														  
			get{return _NomeProcesso;}
			set	{_NomeProcesso = value;}
		}
        public string Descricao
        {
            get { return _Descricao; }
            set { _Descricao = value; }
        }
		public string InvokeName
		{														  
			get{return _InvokeName;}
			set	{_InvokeName = value;}
		}
		public string ProjectName
		{														  
			get{return _ProjectName;}
			set	{_ProjectName = value;}
		}
		public string ClassName
		{														  
			get{return _ClassName;}
			set	{_ClassName = value;}
		}
		public int IndPeriodicidade
		{														  
			get{return _IndPeriodicidade;}
			set	{_IndPeriodicidade = value;}
		}
		public int Hora
		{														  
			get{return _Hora;}
			set	{_Hora = value;}
		}
		public Pessoa IdOperador
		{														  
			get{return _IdOperador;}
			set	{_IdOperador = value;}
		}
		public AvisoOperador IndAvisoOperador
		{
            get { return _IndAvisoOperador; }
            set { _IndAvisoOperador = value; }
		}
        public TipoProcesso IndTipoProcesso
        {
            get { return _IndTipoProcesso; }
            set { _IndTipoProcesso = value; }
        }
        public bool NecessitaPCMSO
        {
            get { return _NecessitaPCMSO; }
            set { _NecessitaPCMSO = value; }
        }

        public enum TipoProcesso : int
        {
            Sistema = 1,
            FerramentaGestao,
            AlertaGeralEmpresa,
            AlertaGeralUsuario
        }

        public enum AvisoOperador : int
        {
            NaoAvisar,
            AvisarSomenteQuandoErro,
            Avisar
        }

        public enum ProcessosAgendamento : int
        {
            VerificaDataDemissao = 2,
            VerificaUsuarioSemAcesso = 3
        }

        public string strPeriodicidade()
        {
            string strPeriodicidade = string.Empty;
            
            switch (this.IndPeriodicidade)
            {
                case (int)Periodicidade.Ano:
                    strPeriodicidade = "Anual";
                    break;
                case (int)Periodicidade.Dia:
                    strPeriodicidade = "Diária";
                    break;
                case (int)Periodicidade.Hora:
                    strPeriodicidade = "1 hora";
                    break;
                case (int)Periodicidade.Mes:
                    strPeriodicidade = "Mensal";
                    break;
                case (int)Periodicidade.Minuto:
                    strPeriodicidade = "15 minutos";
                    break;
                case (int)Periodicidade.Nenhuma:
                    strPeriodicidade = "Nenhuma";
                    break;
                case (int)Periodicidade.Semana:
                    strPeriodicidade = "Semanal";
                    break;
                case (int)Periodicidade.Semestral:
                    strPeriodicidade = "Semestral";
                    break;
            }

            return strPeriodicidade;
        }
	}
	
	[Database("opsa", "ProcessoExecutado", "IdProcessoExecutado")]
	public class ProcessoExecutado :  Ilitera.Data.Table
	{
		private int _IdProcessoExecutado;
		private ProcessoAgendamento _IdProcessoAgendamento;
		private DateTime _ExecucaoInicio;
		private DateTime _ExecucaoTermino;
		private int _IndStatusExecucao;
		private string _MensagemErro=string.Empty;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public ProcessoExecutado()
		{

		}
		public override int Id
		{														  
			get{return _IdProcessoExecutado;}
			set	{_IdProcessoExecutado = value;}
		}
		public  ProcessoAgendamento IdProcessoAgendamento
		{														  
			get{return _IdProcessoAgendamento;}
			set	{_IdProcessoAgendamento = value;}
		}
		public  DateTime ExecucaoInicio
		{														  
			get{return _ExecucaoInicio;}
			set	{_ExecucaoInicio = value;}
		}
		public  DateTime ExecucaoTermino
		{														  
			get{return _ExecucaoTermino;}
			set	{_ExecucaoTermino = value;}
		}
		public int IndStatusExecucao
		{														  
			get{return _IndStatusExecucao;}
			set	{_IndStatusExecucao = value;}
		}
		public string MensagemErro
		{														  
			get{return _MensagemErro;}
			set	{_MensagemErro = value;}
		}
	}

    [Database("opsa", "PrestadorProcessoAgendamento", "IdPrestadorProcessoAgendamento", "", "ativação do Serviço de Avisos")]
    public class PrestadorProcessoAgendamento : Ilitera.Data.Table
    {
        private int _IdPrestadorProcessoAgendamento;
        private ProcessoAgendamento _IdProcessoAgendamento;
        private Prestador _IdPrestador;
        private string _EmailsAdicionais = string.Empty;


        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public PrestadorProcessoAgendamento()
        {
        }
        public override int Id
        {
            get { return _IdPrestadorProcessoAgendamento; }
            set { _IdPrestadorProcessoAgendamento = value; }
        }
        public ProcessoAgendamento IdProcessoAgendamento
        {
            get { return _IdProcessoAgendamento; }
            set { _IdProcessoAgendamento = value; }
        }
        public Prestador IdPrestador
        {
            get { return _IdPrestador; }
            set { _IdPrestador = value; }
        }
        public string EmailsAdicionais
        {
            get { return _EmailsAdicionais; }
            set { _EmailsAdicionais = value; }
        }

        public string GetEmailsPrestador()
        {
            StringBuilder strEmails = new StringBuilder();         
            
            strEmails.Append(this.IdPrestador.Email);

            if (!this.EmailsAdicionais.Equals(string.Empty))
            {
                if (!strEmails.ToString().Equals(string.Empty))
                    if (!strEmails.ToString().Substring(strEmails.ToString().Length - 1, strEmails.ToString().Length).Equals(";"))
                        strEmails.Append(";");

                strEmails.Append(this.EmailsAdicionais);
            }

            //if (!EmailBase.IsEmail(strEmails.ToString()))
            //{
            //    if (this.IdPrestador.IdJuridica.mirrorOld == null)
            //        this.IdPrestador.IdJuridica.Find();

            //    throw new Exception("Os e-mails cadastrados para o Prestador " + this.IdPrestador.NomeCompleto + "(" + this.IdPrestador.IdJuridica.NomeAbreviado + ") não são válidos!");
            //}

            //return strEmails.ToString();

            return "rdgaoki@terra.com.br";
        }
    }

    [Database("opsa", "PrestadorProcessoAgendamentoJuridica", "IdPrestadorProcessoAgendamentoJuridica")]
    public class PrestadorProcessoAgendamentoJuridica : Ilitera.Data.Table
    {
        private int _IdPrestadorProcessoAgendamentoJuridica;
        private PrestadorProcessoAgendamento _IdPrestadorProcessoAgendamento;
        private Juridica _IdJuridica;


        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public PrestadorProcessoAgendamentoJuridica()
        {
        }
        public override int Id
        {
            get { return _IdPrestadorProcessoAgendamentoJuridica; }
            set { _IdPrestadorProcessoAgendamentoJuridica = value; }
        }
        public PrestadorProcessoAgendamento IdPrestadorProcessoAgendamento
        {
            get { return _IdPrestadorProcessoAgendamento; }
            set { _IdPrestadorProcessoAgendamento = value; }
        }
        public Juridica IdJuridica
        {
            get { return _IdJuridica; }
            set { _IdJuridica = value; }
        }
    }
}
