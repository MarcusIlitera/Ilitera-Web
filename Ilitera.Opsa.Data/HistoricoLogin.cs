using System;
using System.Data;
using System.Text;
using System.Collections;
using Ilitera.Data;
using Ilitera.Common;

namespace Ilitera.Opsa.Data
{
	[Database("opsa", "HistoricoLogin", "IdHistoricoLogin")]
	public class HistoricoLogin: Ilitera.Data.Table
	{
		private int	_IdHistoricoLogin;
		private Usuario _IdUsuario;
		private DateTime _DataLogin;
		private string _IPRemoteComputer = string.Empty;
		private string _RemoteComputerName = string.Empty;
		private string _Browser = string.Empty;
		private string _NomeBrowser = string.Empty;
		private string _VersaoBrowser = string.Empty;
		private bool _IsBetaBrowser;
		private bool _hasSuportCookies;
		private bool _hasSuportFrames;
		private bool _hasSuportJavaApplets;
		private bool _hasSuportJavaScript;
		private bool _hasSuportVBScript;
		private bool _IsWin16BasedComputer;
		private bool _IsWin32BasedComputer;
		private string _CLRVersion = string.Empty;
		private string _Plataforma = string.Empty;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public HistoricoLogin()
		{
		}

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public HistoricoLogin(int Id)
		{
			this.Find(Id);
		}
		public override int Id
		{														  
			get{return _IdHistoricoLogin;}
			set	{_IdHistoricoLogin = value;}
		}
		public Usuario IdUsuario
		{														  
			get{return _IdUsuario;}
			set	{_IdUsuario = value;}
		}
		public DateTime DataLogin
		{														  
			get{return _DataLogin;}
			set	{_DataLogin = value;}
		}
		public string IPRemoteComputer
		{														  
			get{return _IPRemoteComputer;}
			set	{_IPRemoteComputer = value;}
		}
		public string RemoteComputerName
		{														  
			get{return _RemoteComputerName;}
			set	{_RemoteComputerName = value;}
		}
		public string Browser
		{														  
			get{return _Browser;}
			set	{_Browser = value;}
		}
		public string NomeBrowser
		{														  
			get{return _NomeBrowser;}
			set	{_NomeBrowser = value;}
		}
		public string VersaoBrowser
		{
			get{return _VersaoBrowser;}
			set	{_VersaoBrowser = value;}
		}
		public bool IsBetaBrowser
		{														  
			get{return _IsBetaBrowser;}
			set	{_IsBetaBrowser = value;}
		}
		public string CLRVersion
		{														  
			get{return _CLRVersion;}
			set	{_CLRVersion = value;}
		}
		public string Plataforma
		{														  
			get{return _Plataforma;}
			set	{_Plataforma = value;}
		}
		public bool hasSuportCookies
		{														  
			get{return _hasSuportCookies;}
			set	{_hasSuportCookies = value;}
		}
		public bool hasSuportFrames
		{														  
			get{return _hasSuportFrames;}
			set	{_hasSuportFrames = value;}
		}
		public bool hasSuportJavaApplets
		{														  
			get{return _hasSuportJavaApplets;}
			set	{_hasSuportJavaApplets = value;}
		}
		public bool hasSuportJavaScript
		{														  
			get{return _hasSuportJavaScript;}
			set	{_hasSuportJavaScript = value;}
		}
		public bool hasSuportVBScript
		{														  
			get{return _hasSuportVBScript;}
			set	{_hasSuportVBScript = value;}
		}
		public bool IsWin16BasedComputer
		{														  
			get{return _IsWin16BasedComputer;}
			set	{_IsWin16BasedComputer = value;}
		}
		public bool IsWin32BasedComputer
		{														  
			get{return _IsWin32BasedComputer;}
			set	{_IsWin32BasedComputer = value;}
		}


		public override void Delete()
		{
			base.Delete();
			ArrayList list = new HistoricoLogin().Find("IdUsuario="+this.IdUsuario.Id+" ORDER BY DataLogin DESC");
			this.IdUsuario.Find();
			this.IdUsuario.NumLogin--;
			if(list.Count>0)
				this.IdUsuario.DatUltLogin	= ((HistoricoLogin)list[0]).DataLogin;
			this.IdUsuario.Save();
		}

		public DataSet GetAcessos(Usuario usuario)
		{
			DataSet ds = new DataSet();

			DataTable table = new DataTable("Result");
			table.Columns.Add("IdHistoricoLogin", Type.GetType("System.String"));			
			table.Columns.Add("Data/Hora", Type.GetType("System.String"));
			table.Columns.Add("IP", Type.GetType("System.String"));
			table.Columns.Add("Browser", Type.GetType("System.String"));
			table.Columns.Add(".NET", Type.GetType("System.String"));
			ds.Tables.Add(table);
			DataRow newRow;

			if(usuario == null)
				usuario = new Usuario();

			ArrayList list = this.Find("IdUsuario=" + usuario.Id 
									+ " ORDER BY DataLogin DESC");

			if(list.Count==0)
			{
				newRow = ds.Tables[0].NewRow();
				ds.Tables[0].Rows.Add(newRow);
			}
			
			foreach(HistoricoLogin historicoLogin in list)
			{
				newRow = ds.Tables[0].NewRow();
				
				newRow["IdHistoricoLogin"]	= historicoLogin.Id;
				newRow["Data/Hora"]			= historicoLogin.DataLogin.ToString();
				newRow["IP"]				= historicoLogin.IPRemoteComputer;				
				newRow["Browser"]			= historicoLogin.NomeBrowser 
											+ " - " + historicoLogin.VersaoBrowser;
				
				if(historicoLogin.CLRVersion.Equals("0.0"))
					newRow[".NET"]	= "Não - " + historicoLogin.Plataforma;	
				else
					newRow[".NET"]	= historicoLogin.CLRVersion +" - " + historicoLogin.Plataforma;	
				
				ds.Tables[0].Rows.Add(newRow);
			}

			return ds;
		}
	}
}