using System;
using System.Data;
using System.Text;
using System.Collections;
using Ilitera.Data;
using Ilitera.Common;

namespace Ilitera.Opsa.Data
{
	[Database("opsa", "HistoricoPageErrors", "IdHistoricoPageErrors")]
	public class HistoricoPageErrors: Ilitera.Data.Table
	{
		private int	_IdHistoricoPageErrors;
		private Usuario _IdUsuario;
		private DateTime _DataErro;
		private string _DescricaoErro = string.Empty;
		private string _PathPage = string.Empty;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public HistoricoPageErrors()
		{
		}

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public HistoricoPageErrors(int Id)
		{
			this.Find(Id);
		}
		public override int Id
		{														  
			get{return _IdHistoricoPageErrors;}
			set	{_IdHistoricoPageErrors = value;}
		}
		public Usuario IdUsuario
		{														  
			get{return _IdUsuario;}
			set	{_IdUsuario = value;}
		}
		public DateTime DataErro
		{														  
			get{return _DataErro;}
			set	{_DataErro = value;}
		}
		public string DescricaoErro
		{														  
			get{return _DescricaoErro;}
			set	{_DescricaoErro = value;}
		}
		public string PathPage
		{														  
			get{return _PathPage;}
			set	{_PathPage = value;}
		}
	}
}