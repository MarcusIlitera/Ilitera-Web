using System;
using Ilitera.Data; 

namespace Ilitera.Opsa.Data
{
	[Database("opsa","TipoCliente","IdTipoCliente")]
    
    public class TipoCliente : Ilitera.Data.Table
	{
		private int _IdTipoCliente;
		private string _Descricao = string.Empty;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]
        public TipoCliente()
        {

        }

		public override int Id
		{
			get{return _IdTipoCliente;}
			set{_IdTipoCliente = value;}
		}

		public string Descricao
		{
			get{return _Descricao;}
			set{_Descricao = value;}
		}
		public override string ToString()
		{
			return this.Descricao;
		}
	}
}
