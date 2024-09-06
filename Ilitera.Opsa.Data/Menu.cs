using System;
using Ilitera.Data;
using System.Data; 
using System.Collections;
using Ilitera.Common;
using System.Text;


namespace Ilitera.Opsa.Data
{
	[Database("opsa", "AplicacaoGrupo", "IdAplicacaoGrupo")]
	public class AplicacaoGrupo : Ilitera.Data.Table
	{
		private int	_IdAplicacaoGrupo;
		private TipoAplicacao _IdTipoAplicacao;
		private Grupo _IdGrupo;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public AplicacaoGrupo()
		{
		}

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public AplicacaoGrupo(int Id)
		{
			this.Find(Id);
		}

		public override int Id
		{														  
			get{return _IdAplicacaoGrupo;}
			set	{_IdAplicacaoGrupo = value;}
		}

		public TipoAplicacao IdTipoAplicacao
		{
			get{return _IdTipoAplicacao;}
			set	{_IdTipoAplicacao = value;}
		}
		
		public Grupo IdGrupo
		{														  
			get{return _IdGrupo;}
			set	{_IdGrupo = value;}
		}

		public override string ToString()
		{
			this.IdTipoAplicacao.Find();
			return this.IdTipoAplicacao.Nome;
		}
	}	
	
	[Database("opsa", "TargetLink", "IdTargetLink")]
	public class TargetLink : Ilitera.Data.Table
	{
		private int	_IdTargertLink;
		private string _TargetLinkNome = string.Empty;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public TargetLink()
		{
		}

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public TargetLink(int Id)
		{
			this.Find(Id);
		}

		public override int Id
		{														  
			get{return _IdTargertLink;}
			set	{_IdTargertLink = value;}
		}

		public string TargetLinkNome
		{														  
			get{return _TargetLinkNome;}
			set	{_TargetLinkNome = value;}
		}
	}
	
	[Database("opsa", "Link", "IdLink")]
	public class Link : Ilitera.Opsa.Data.TargetLink
	{
		private int	_IdLink;
		private string _LinkNome = string.Empty;
		private int _Width;
		private int _Heigth;
		private string _NomeJanela = string.Empty;
		private string _TipoCloseWin = string.Empty;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public Link()
		{
		}

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public Link(int Id)
		{
			this.Find(Id);
		}

		public override int Id
		{														  
			get{return _IdLink;}
			set	{_IdLink = value;}
		}

		public string LinkNome
		{														  
			get{return _LinkNome;}
			set	{_LinkNome = value;}
		}

		public int Width
		{														  
			get{return _Width;}
			set	{_Width = value;}
		}

		public int Heigth
		{														  
			get{return _Heigth;}
			set	{_Heigth = value;}
		}

		public string NomeJanela
		{														  
			get{return _NomeJanela;}
			set	{_NomeJanela = value;}
		}

		public string TipoCloseWin
		{														  
			get{return _TipoCloseWin;}
			set	{_TipoCloseWin = value;}
		}
	}

	[Database("opsa", "MenuList", "IdMenuList", "", "Menu")]
	public class MenuList : Ilitera.Opsa.Data.Link
	{
		private int	_IdMenuList;
		private TipoAplicacao _IdTipoAplicacao;
		private string _TextoItem = string.Empty;
		private int _Sequencia;
		private int _hasIdPai;
		private bool _IsAtivoToEmpregado;
		private bool _IsAtivoToEmpresa;
		private MenuLanguage _IdMenuLanguage;
		private string _MenuImage = string.Empty;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public MenuList()
		{
		}

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public MenuList(int Id)
		{
			this.Find(Id);
		}
		public override int Id
		{														  
			get{return _IdMenuList;}
			set	{_IdMenuList = value;}
		}
		public TipoAplicacao IdTipoAplicacao
		{														  
			get{return _IdTipoAplicacao;}
			set	{_IdTipoAplicacao = value;}
		}
		public string TextoItem
		{														  
			get{return _TextoItem;}
			set	{_TextoItem = value;}
		}
		public int Sequencia
		{														  
			get{return _Sequencia;}
			set	{_Sequencia = value;}
		}
		public int hasIdPai
		{
			get{return _hasIdPai;}
			set	{_hasIdPai = value;}
		}
		public bool IsAtivoToEmpregado
		{														  
			get{return _IsAtivoToEmpregado;}
			set	{_IsAtivoToEmpregado = value;}
		}
		public bool IsAtivoToEmpresa
		{														  
			get{return _IsAtivoToEmpresa;}
			set	{_IsAtivoToEmpresa = value;}
		}
		public MenuLanguage IdMenuLanguage
		{														  
			get{return _IdMenuLanguage;}
			set	{_IdMenuLanguage = value;}
		}
		public string MenuImage
		{														  
			get{return _MenuImage;}
			set	{_MenuImage = value;}
		}
	}

	[Database("opsa", "MenuLanguage", "IdMenuLanguage", "", "Idioma para Menu")]
	public class MenuLanguage : Ilitera.Data.Table
	{
		private int	_IdMenuLanguage;
		private string _NameLanguage = string.Empty;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public MenuLanguage()
		{
		}

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public MenuLanguage(int Id)
		{
			this.Find(Id);
		}
		public override int Id
		{
			get{return _IdMenuLanguage;}
			set	{_IdMenuLanguage = value;}
		}
		public string NameLanguage
		{														  
			get{return _NameLanguage;}
			set	{_NameLanguage = value;}
		}
	}

	public enum IdAplicacao: int
	{
		DadosEmpresa=1,
		Documentos,
		Treinamentos,
		Ferramentas,
		Ajuda,
		PPRAG,
		PPRPS,
		PCMAT,
		ControleExtintores,
		MapadeRiscos,
		LaudoEletrico,
		ParaRaios,
		VasosdePressao,
		Caldeiras,
		LTCAT,
		PPP,
		MedicinaOcupacional,
		OrdemdeServico,
		FornecimentodeEPI,
		ManualBioSeguranca,
		AuditoriadeSeguranca,
		PCMSO,
		PPRA,
		ClinicasExames,
		LaudoErgonomico,
		ElaboracaoCIPA,
		QuadrosNR4,
		ExtranetEmpresa,
		ExtranetAgendaPrestadores,
		ExtranetRelatorios,
		ExtranetUsersLogins,
		ExtranetMenuWeb,
        OrdemServicoNR17,
        ControleServico
	}

	public enum IndMenuType: int
	{
		Disabled,
		Empresa,
		Empregado,
		AllEnabled
	}

	public enum IndTipoMenuAplic: int
	{
		MestraNet = 1,
		Extranet
	}

	[Database("opsa", "AplicacaoUsuario", "IdAplicacaoUsuario")]
	public class AplicacaoUsuario : Ilitera.Data.Table
	{
		private int	_IdAplicacaoUsuario;
		private TipoAplicacao _IdTipoAplicacao;
		private Usuario _IdUsuario;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public AplicacaoUsuario()
		{
		}

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public AplicacaoUsuario(int Id)
		{
			this.Find(Id);
		}

		public override int Id
		{														  
			get{return _IdAplicacaoUsuario;}
			set	{_IdAplicacaoUsuario = value;}
		}

		public TipoAplicacao IdTipoAplicacao
		{
			get{return _IdTipoAplicacao;}
			set	{_IdTipoAplicacao = value;}
		}
		
		public Usuario IdUsuario
		{														  
			get{return _IdUsuario;}
			set	{_IdUsuario = value;}
		}

		public override string ToString()
		{
			this.IdTipoAplicacao.Find();
			return this.IdTipoAplicacao.Nome;
		}
	}

	[Database("opsa", "TipoAplicacao", "IdTipoAplicacao")]
	public class TipoAplicacao : Ilitera.Data.Table
	{
		private int	_IdTipoAplicacao;
		private string _Nome = string.Empty;
		private string _Descricao = string.Empty;
		private string _Link = string.Empty;
		private int _hasIdPai;
		private int _Sequencia;
		private int _IndTipoMenuAplicacao;
		private bool VerificaExisteGrupo, VerificaExiste, VerificaExisteItem, VerificaExisteGrupoItem = false;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public TipoAplicacao()
		{
		}

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public TipoAplicacao(int Id)
		{
			this.Find(Id);
		}
		public override int Id
		{														  
			get{return _IdTipoAplicacao;}
			set	{_IdTipoAplicacao = value;}
		}
		public string Nome
		{
			get{return _Nome;}
			set	{_Nome = value;}
		}
		public string Descricao
		{														  
			get{return _Descricao;}
			set	{_Descricao = value;}
		}
		public string Link
		{														  
			get{return _Link;}
			set	{_Link = value;}
		}
		public int hasIdPai
		{														  
			get{return _hasIdPai;}
			set	{_hasIdPai = value;}
		}
		public int Sequencia
		{														  
			get{return _Sequencia;}
			set	{_Sequencia = value;}
		}
		public int IndTipoMenuAplicacao
		{
			get{return _IndTipoMenuAplicacao;}
			set{_IndTipoMenuAplicacao = value;}
		}

		public override string ToString()
		{
			return this.Nome;
		}

		public DataSet GetAplicacaoesTitu(string idusuario, int IndTipoMenuAplic)
		{			
			DataSet dsgrupo = this.Get("IdTipoAplicacao IN (SELECT IdTipoAplicacao FROM AplicacaoGrupo WHERE IdGrupo IN"
				+" (SELECT IdGrupo FROM UsuarioGrupo WHERE IdUsuario=" + idusuario + "))"
				+" AND (hasIdPai IS NULL OR hasIdPai='')"
				+" AND IndTipoMenuAplicacao=" + IndTipoMenuAplic
				+" ORDER BY Sequencia");

			DataSet ds = this.Get("IdTipoAplicacao IN (SELECT IdTipoAplicacao FROM AplicacaoUsuario WHERE IdUsuario=" + idusuario
				+") AND (hasIdPai IS NULL OR hasIdPai='')"
				+" AND IndTipoMenuAplicacao=" + IndTipoMenuAplic
				+" ORDER BY Sequencia");

			DataSet dsAplicacoesTitu = new DataSet();

			DataTable table = new DataTable("AplicacaoTitulo");
			table.Columns.Add("IdTipoAplicacao", Type.GetType("System.Int32"));
			table.Columns.Add("Nome", Type.GetType("System.String"));
			table.Columns.Add("hasIdPai", Type.GetType("System.Int32"));
			table.Columns.Add("Descricao", Type.GetType("System.String"));
			table.Columns.Add("Link", Type.GetType("System.String"));
			table.Columns.Add("Sequencia", Type.GetType("System.Int32"));
			
			dsAplicacoesTitu.Tables.Add(table);

			foreach (DataRow dsgruporow in dsgrupo.Tables[0].Select())
			{
				foreach(DataRow dsAplicacoesTiturow in dsAplicacoesTitu.Tables[0].Select())
				{
					if (dsAplicacoesTiturow["IdTipoAplicacao"] == dsgruporow["IdTipoAplicacao"])
					{
						VerificaExisteGrupo = true;
						break;
					}
					else
						VerificaExisteGrupo = false;
				}

				if (VerificaExisteGrupo == false)
				{
					DataRow newrow = dsAplicacoesTitu.Tables[0].NewRow();

					newrow["IdTipoAplicacao"] = dsgruporow["IdTipoAplicacao"];
					newrow["Nome"] = dsgruporow["Nome"];
					newrow["hasIdPai"] = dsgruporow["hasIdPai"];
					newrow["Descricao"] = dsgruporow["Descricao"];
					newrow["Link"] = dsgruporow["Link"];
					newrow["Sequencia"] = dsgruporow["Sequencia"];

					dsAplicacoesTitu.Tables[0].Rows.Add(newrow);
				}
			}

			foreach (DataRow dsrow in ds.Tables[0].Select())
			{
				foreach(DataRow dsAplicacoesTiturow in dsAplicacoesTitu.Tables[0].Select())
				{
					if (dsAplicacoesTiturow["IdTipoAplicacao"] == dsrow["IdTipoAplicacao"])
					{
						VerificaExiste = true;
						break;
					}
					else
						VerificaExiste = false;
				}

				if (VerificaExiste == false)
				{
					DataRow newrow = dsAplicacoesTitu.Tables[0].NewRow();

					newrow["IdTipoAplicacao"] = dsrow["IdTipoAplicacao"];
					newrow["Nome"] = dsrow["Nome"];
					newrow["hasIdPai"] = dsrow["hasIdPai"];
					newrow["Descricao"] = dsrow["Descricao"];
					newrow["Link"] = dsrow["Link"];
					newrow["Sequencia"] = dsrow["Sequencia"];

					dsAplicacoesTitu.Tables[0].Rows.Add(newrow);
				}
			}
            
			return dsAplicacoesTitu;
		}

		public DataSet GetAplicacaoesItem(string idusuario, int IdPai)
		{
			DataSet dsgrupo = this.Get("IdTipoAplicacao IN (SELECT IdTipoAplicacao FROM AplicacaoGrupo WHERE IdGrupo IN"
				+" (SELECT IdGrupo FROM UsuarioGrupo WHERE IdUsuario=" + idusuario + "))"
				+" AND (hasIdPai=" + IdPai + ")"
				+" ORDER BY Sequencia");

			DataSet ds = this.Get("IdTipoAplicacao IN (SELECT IdTipoAplicacao FROM AplicacaoUsuario WHERE IdUsuario=" + idusuario
				+") AND hasIdPai=" + IdPai
				+" ORDER BY Sequencia");

			DataSet AplicacoesItem = new DataSet();

			DataTable table = new DataTable("AplicacoesItem");
			table.Columns.Add("IdTipoAplicacao", Type.GetType("System.Int32"));
			table.Columns.Add("Nome", Type.GetType("System.String"));
			table.Columns.Add("hasIdPai", Type.GetType("System.Int32"));
			table.Columns.Add("Descricao", Type.GetType("System.String"));
			table.Columns.Add("Link", Type.GetType("System.String"));
			table.Columns.Add("Sequencia", Type.GetType("System.Int32"));

			AplicacoesItem.Tables.Add(table);
			
			foreach (DataRow dsgruporow in dsgrupo.Tables[0].Select())
			{
				foreach(DataRow AplicacoesItemrow in AplicacoesItem.Tables[0].Select())
				{
					if (AplicacoesItemrow["IdTipoAplicacao"] == dsgruporow["IdTipoAplicacao"])
					{
						VerificaExisteGrupoItem = true;
						break;
					}
					else
						VerificaExisteGrupoItem = false;
				}

				if (VerificaExisteGrupoItem == false)
				{
					DataRow newrow = AplicacoesItem.Tables[0].NewRow();

					newrow["IdTipoAplicacao"] = dsgruporow["IdTipoAplicacao"];
					newrow["Nome"] = dsgruporow["Nome"];
					newrow["hasIdPai"] = dsgruporow["hasIdPai"];
					newrow["Descricao"] = dsgruporow["Descricao"];
					newrow["Link"] = dsgruporow["Link"];
					newrow["Sequencia"] = dsgruporow["Sequencia"];

					AplicacoesItem.Tables[0].Rows.Add(newrow);
				}
			}

			foreach (DataRow dsrow in ds.Tables[0].Select())
			{
				foreach(DataRow AplicacoesItemrow in AplicacoesItem.Tables[0].Select())
				{
					if (AplicacoesItemrow["IdTipoAplicacao"] == dsrow["IdTipoAplicacao"])
					{
						VerificaExisteItem = true;
						break;
					}
					else
						VerificaExisteItem = false;
				}

				if (VerificaExisteItem == false)
				{
					DataRow newrow = AplicacoesItem.Tables[0].NewRow();

					newrow["IdTipoAplicacao"] = dsrow["IdTipoAplicacao"];
					newrow["Nome"] = dsrow["Nome"];
					newrow["hasIdPai"] = dsrow["hasIdPai"];
					newrow["Descricao"] = dsrow["Descricao"];
					newrow["Link"] = dsrow["Link"];
					newrow["Sequencia"] = dsrow["Sequencia"];

					AplicacoesItem.Tables[0].Rows.Add(newrow);
				}
			}
            
			return AplicacoesItem;
		}

		public DataSet GetAplicacaoesTituAll(int IndTipoMenuAplic)
		{
			DataSet ds = this.Get("(hasIdPai IS NULL OR hasIdPai='')"
				+" AND IndTipoMenuAplicacao=" + IndTipoMenuAplic
				+" ORDER BY Sequencia");

			return ds;
		}

		public DataSet GetAplicacaoesItemAll(int IdPai)
		{
			DataSet ds = this.Get("hasIdPai=" + IdPai + " ORDER BY Sequencia");

			return ds;
		}
	}
}