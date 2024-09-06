using System;
using Ilitera.Common;
using System.Collections;
using Ilitera.Data;
using System.Data;


namespace Ilitera.Opsa.Data
{

//	CREATE TABLE [dbo].[_SP_RAMO] (
//	[ID] [int] IDENTITY (1, 1) NOT NULL ,
//	[RAZAO_SOCIAL] [nvarchar] (255) COLLATE Latin1_General_CI_AS NULL ,
//	[ENDERECO] [nvarchar] (255) COLLATE Latin1_General_CI_AS NULL ,
//	[BAIRRO] [nvarchar] (255) COLLATE Latin1_General_CI_AS NULL ,
//	[CEP] [nvarchar] (255) COLLATE Latin1_General_CI_AS NULL ,
//	[MUNICIPIO] [nvarchar] (255) COLLATE Latin1_General_CI_AS NULL ,
//	[EST] [nvarchar] (255) COLLATE Latin1_General_CI_AS NULL ,
//	[RAMO_ATIV] [nvarchar] (255) COLLATE Latin1_General_CI_AS NULL ,
//	[CONTATO] [nvarchar] (255) COLLATE Latin1_General_CI_AS NULL ,
//	[EMPREGADOS] [float] NULL ,
//	[FONE] [nvarchar] (255) COLLATE Latin1_General_CI_AS NULL ,
//	[FAX] [nvarchar] (255) COLLATE Latin1_General_CI_AS NULL ,
//	[HOMEPAGE] [nvarchar] (255) COLLATE Latin1_General_CI_AS NULL ,
//	[EMAIL] [nvarchar] (255) COLLATE Latin1_General_CI_AS NULL 
//	) ON [PRIMARY]

	[Database("opsa", "_SP_RAMO", "ID")]
	public class Prospeccao_SP_RAMO : Ilitera.Data.Table
	{
		private int _ID;
		private string _RAZAO_SOCIAL=string.Empty;
		private string _ENDERECO=string.Empty;
		private string _BAIRRO=string.Empty;
		private string _CEP=string.Empty;
		private string _MUNICIPIO=string.Empty;
		private string _EST=string.Empty;
		private string _RAMO_ATIV=string.Empty;
		private string _FONE=string.Empty;
		private string _FAX=string.Empty;
		private string _CONTATO1=string.Empty;
		private string _CONTATO=string.Empty;
		private string _CARGO=string.Empty;
		private int _EMPREGADOS;
		private string _EMAIL=string.Empty;
		private string _HOMEPAGE=string.Empty;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public Prospeccao_SP_RAMO()
		{

		}
		public override int Id
		{														  
			get{return _ID;}
			set	{_ID = value;}
		}	
		public string RAZAO_SOCIAL
		{														  
			get	{return _RAZAO_SOCIAL;}
			set {_RAZAO_SOCIAL = value;}
		}
		public string ENDERECO
		{														  
			get	{return _ENDERECO;}
			set {_ENDERECO = value;}
		}
		public string BAIRRO
		{														  
			get	{return _BAIRRO;}
			set {_BAIRRO = value;}
		}
		public string CEP
		{														  
			get	{return _CEP;}
			set {_CEP = value;}
		}
		public string MUNICIPIO
		{														  
			get	{return _MUNICIPIO;}
			set {_MUNICIPIO = value;}
		}
		public string EST
		{														  
			get	{return _EST;}
			set {_EST = value;}
		}
		public string RAMO_ATIV
		{														  
			get	{return _RAMO_ATIV;}
			set {_RAMO_ATIV = value;}
		}
		public string FONE
		{														  
			get	{return _FONE;}
			set {_FONE = value;}
		}
		public string FAX
		{														  
			get	{return _FAX;}
			set {_FAX = value;}
		}
		public string CONTATO1
		{														  
			get	{return _CONTATO1;}
			set {_CONTATO1 = value;}
		}
		public string CONTATO
		{														  
			get	{return _CONTATO;}
			set {_CONTATO = value;}
		}
		public string CARGO
		{														  
			get	{return _CARGO;}
			set {_CARGO = value;}
		}
		public int EMPREGADOS
		{														  
			get	{return _EMPREGADOS;}
			set {_EMPREGADOS = value;}
		}
		public string EMAIL
		{														  
			get	{return _EMAIL;}
			set {_EMAIL = value;}
		}
		public string HOMEPAGE
		{														  
			get	{return _HOMEPAGE;}
			set {_HOMEPAGE = value;}
		}

		public static void ImportarCadastro()
		{
			ArrayList list = new Prospeccao_SP_RAMO().Find("MUNICIPIO='SAO PAULO' ORDER BY RAZAO_SOCIAL");
			
			foreach(Prospeccao_SP_RAMO prospecaoExcel in list)
			{
				try
				{
					Prospeccao prosp = new Prospeccao();
					prosp.Inicialize();
					prosp.IdJuridicaPapel.Id		=(int)IndJuridicaPapel.Prospeccao3;
					if(prospecaoExcel.RAZAO_SOCIAL.Length<=50)
						prosp.NomeAbreviado	= prospecaoExcel.RAZAO_SOCIAL;
					else
						prosp.NomeAbreviado	= prospecaoExcel.RAZAO_SOCIAL.Substring(0, 50);
					prosp.NomeCompleto				= prospecaoExcel.RAZAO_SOCIAL;
					prosp.Atividade					= prospecaoExcel.RAMO_ATIV;
					prosp.Diretor					= prospecaoExcel.CONTATO1;
					prosp.QtdEmpregados				= prospecaoExcel.EMPREGADOS;
					//prosp.Email						= Ilitera.Common.EmailBase.IsEmail(prospecaoExcel.EMAIL)?prospecaoExcel.EMAIL:string.Empty;
					prosp.Site						= prospecaoExcel.HOMEPAGE;
					prosp.IdCadastroImportacao.Id	= (int)CadastrosImportacao.RamoAtividade;
					prosp.Save();

					ContatoTelefonico contatoTelefonico = new ContatoTelefonico();
					if(prospecaoExcel.CONTATO!=string.Empty)
					{
						contatoTelefonico.Inicialize();
						contatoTelefonico.IndTipoTelefone = (int)ContatoTelefonico.TipoTelefone.Comercial;
						contatoTelefonico.IdPessoa.Id	= prosp.Id;
						contatoTelefonico.Nome			= prospecaoExcel.CONTATO;
						contatoTelefonico.Departamento	= prospecaoExcel.CARGO;
						contatoTelefonico.Numero		= prospecaoExcel.FONE;
						contatoTelefonico.Save();
					}

					ContatoTelefonico fax = new ContatoTelefonico();
					if(prospecaoExcel.FAX!=string.Empty)
					{
						fax.Inicialize();
						fax.IndTipoTelefone = (int)ContatoTelefonico.TipoTelefone.Fax;
						fax.IdPessoa.Id	= prosp.Id;
						fax.Numero = prospecaoExcel.FAX;
						fax.Save();
					}

					Endereco endereco = new Endereco();
					if(prospecaoExcel.ENDERECO!=string.Empty)
					{
						endereco.Inicialize();
						endereco.IdPessoa.Id	= prosp.Id;
						endereco.Logradouro		= prospecaoExcel.ENDERECO;
						endereco.Bairro			= prospecaoExcel.BAIRRO;
						endereco.Cep			= prospecaoExcel.CEP;
						endereco.IdMunicipio.Id = (int)Municipios.SaoPaulo;
						endereco.Save();
					}
				}
				catch(Exception ex)
				{	
                    System.Diagnostics.Debug.WriteLine(ex.Message);
				}	
			}
		}
	}

	[Database("opsa", "_SP_PORTE", "ID")]
	public class Prospeccao_SP_PORTE : Ilitera.Data.Table
	{
		private int _ID;
		private string _RAZAO_SOCIAL=string.Empty;
		private string _ENDERECO=string.Empty;
		private string _CEP=string.Empty;
		private string _MUNICIPIO=string.Empty;
		private string _EST=string.Empty;
		private string _RAMO_ATIV=string.Empty;
		private string _FONE=string.Empty;
		private string _FAX=string.Empty;
		private string _TAM=string.Empty;
		private string _CONTATO=string.Empty;
		private string _CARGO=string.Empty;
		private string _EMAIL=string.Empty;
		private string _HOMEPAGE=string.Empty;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public Prospeccao_SP_PORTE()
		{

		}
		public override int Id
		{														  
			get{return _ID;}
			set	{_ID = value;}
		}	
		public string RAZAO_SOCIAL
		{														  
			get	{return _RAZAO_SOCIAL;}
			set {_RAZAO_SOCIAL = value;}
		}
		public string ENDERECO
		{														  
			get	{return _ENDERECO;}
			set {_ENDERECO = value;}
		}
		public string CEP
		{														  
			get	{return _CEP;}
			set {_CEP = value;}
		}
		public string MUNICIPIO
		{														  
			get	{return _MUNICIPIO;}
			set {_MUNICIPIO = value;}
		}
		public string EST
		{														  
			get	{return _EST;}
			set {_EST = value;}
		}
		public string RAMO_ATIV
		{														  
			get	{return _RAMO_ATIV;}
			set {_RAMO_ATIV = value;}
		}
		public string FONE
		{														  
			get	{return _FONE;}
			set {_FONE = value;}
		}
		public string FAX
		{														  
			get	{return _FAX;}
			set {_FAX = value;}
		}
		public string TAM
		{														  
			get	{return _TAM;}
			set {_TAM = value;}
		}
		public string CONTATO
		{														  
			get	{return _CONTATO;}
			set {_CONTATO = value;}
		}
		public string CARGO
		{														  
			get	{return _CARGO;}
			set {_CARGO = value;}
		}
		public string EMAIL
		{														  
			get	{return _EMAIL;}
			set {_EMAIL = value;}
		}
		public string HOMEPAGE
		{														  
			get	{return _HOMEPAGE;}
			set {_HOMEPAGE = value;}
		}

		public static void ImportarCadastro()
		{
			ArrayList list = new Prospeccao_SP_PORTE().Find("MUNICIPIO='SAO PAULO' ORDER BY RAZAO_SOCIAL");
			
			foreach(Prospeccao_SP_PORTE prospecaoExcel in list)
			{
				try
				{
					Prospeccao prosp = new Prospeccao();
					prosp.Inicialize();
					prosp.IdJuridicaPapel.Id		=(int)IndJuridicaPapel.Prospeccao3;
					if(prospecaoExcel.RAZAO_SOCIAL.Length<=50)
						prosp.NomeAbreviado	= prospecaoExcel.RAZAO_SOCIAL;
					else
						prosp.NomeAbreviado	= prospecaoExcel.RAZAO_SOCIAL.Substring(0, 50);
					prosp.NomeCompleto				= prospecaoExcel.RAZAO_SOCIAL;
					prosp.Atividade					= prospecaoExcel.RAMO_ATIV;
					prosp.Diretor					= prospecaoExcel.CONTATO;
					//prosp.Email						= Ilitera.Common.EmailBase.IsEmail(prospecaoExcel.EMAIL)?prospecaoExcel.EMAIL:string.Empty;
					prosp.Site						= prospecaoExcel.HOMEPAGE;
					prosp.IndPorteEmpresa			= (int)Prospeccao.GetPorteEmpresa(prospecaoExcel.TAM);
					prosp.IdCadastroImportacao.Id	= (int)CadastrosImportacao.Porte;
					prosp.Save();

					ContatoTelefonico contatoTelefonico = new ContatoTelefonico();
					if(prospecaoExcel.CONTATO!=string.Empty)
					{
						contatoTelefonico.Inicialize();
						contatoTelefonico.IndTipoTelefone = (int)ContatoTelefonico.TipoTelefone.Comercial;
						contatoTelefonico.IdPessoa.Id	= prosp.Id;
						contatoTelefonico.Nome			= prospecaoExcel.CONTATO;
						contatoTelefonico.Departamento	= prospecaoExcel.CARGO;
						contatoTelefonico.Numero		= prospecaoExcel.FONE;
						contatoTelefonico.Save();
					}

					ContatoTelefonico fax = new ContatoTelefonico();
					if(prospecaoExcel.FAX!=string.Empty)
					{
						fax.Inicialize();
						fax.IndTipoTelefone = (int)ContatoTelefonico.TipoTelefone.Fax;
						fax.IdPessoa.Id	= prosp.Id;
						fax.Numero = prospecaoExcel.FAX;
						fax.Save();
					}

					Endereco endereco = new Endereco();
					if(prospecaoExcel.ENDERECO!=string.Empty)
					{
						endereco.Inicialize();
						endereco.IdPessoa.Id	= prosp.Id;
						endereco.Logradouro		= prospecaoExcel.ENDERECO;
						endereco.Cep			= prospecaoExcel.CEP;
						endereco.IdMunicipio.Id = (int)Municipios.SaoPaulo;
						endereco.Save();
					}
				}
				catch(Exception ex)
				{	
                    System.Diagnostics.Debug.WriteLine(ex.Message);
				}	
			}
		}
	}
}
