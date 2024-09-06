using System;
using Ilitera.Data;

namespace Ilitera.Opsa.Data
{
	[Database("sied_novo", "tblMAT_PRM", "nID_MAT_PRM", true)]
	public class MateriaPrima : Ilitera.Data.Table
	{
		private int _nID_MAT_PRM;
		private string _tNO_MAT_PRM = string.Empty;
		private string _tDS_MAT_PRM = string.Empty;
        private string _Codigo_eSocial = string.Empty;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public MateriaPrima()
		{

		}		
		public override int Id
		{														  
			get	{return _nID_MAT_PRM;}
			set {_nID_MAT_PRM = value;}
		}	
		public string tNO_MAT_PRM
		{														  
			get	{return _tNO_MAT_PRM;}
			set {_tNO_MAT_PRM = value;}
		}
		public string tDS_MAT_PRM
		{														  
			get	{return _tDS_MAT_PRM;}
			set {_tDS_MAT_PRM = value;}
		}
        public string Codigo_eSocial
        {
            get { return _Codigo_eSocial; }
            set { _Codigo_eSocial = value; }
        }
        public override string ToString()
		{
			return "Agente Específico - "+this._tNO_MAT_PRM;
		}
	}
}
