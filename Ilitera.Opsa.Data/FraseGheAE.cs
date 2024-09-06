using System;
using System.Text;
using System.Collections;
using Ilitera.Data;

namespace Ilitera.Opsa.Data
{
	[Database("sied_novo", "tblFRS_FUNC_AE", "nID_FRS_FUNC_AE")]
	public class FraseGheAE : Ilitera.Data.Table 
	{
		private int _nID_FRS_FUNC_AE;
		private string _tNO_FRS_FUNC_AE=string.Empty;
		private string _tDS_FRS_FUNC_AE=string.Empty;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public FraseGheAE()
		{

		}
		public override int Id
		{														  
			get	{return _nID_FRS_FUNC_AE;}
			set {_nID_FRS_FUNC_AE = value;}
		}
		public string tNO_FRS_FUNC_AE
		{														  
			get	{return _tNO_FRS_FUNC_AE;}
			set {_tNO_FRS_FUNC_AE = value;}
		}	
		public string tDS_FRS_FUNC_AE
		{														  
			get	{return _tDS_FRS_FUNC_AE;}
			set {_tDS_FRS_FUNC_AE = value;}
		}	
		public override string ToString()
		{
			return tNO_FRS_FUNC_AE;
		}
	}
}
