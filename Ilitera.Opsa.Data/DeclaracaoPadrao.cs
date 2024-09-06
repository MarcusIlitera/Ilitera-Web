using System;
using Ilitera.Data;

namespace Ilitera.Opsa.Data
{
	[Database("sied_novo", "tblDECL_PDR", "nID_DECL_PDR", true)]
	public class DeclaracaoPadrao: Ilitera.Data.Table
	{
		private int _nID_DECL_PDR;
		private string _tDS_DECL_PDR = string.Empty;
		private string _mDECL_EMPS = string.Empty;
		private string _mDECL_EMPG = string.Empty;
		private string _mDECL_EMPG_S_EPI = string.Empty;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public DeclaracaoPadrao()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public override int Id
		{
			get{return _nID_DECL_PDR;}
			set{_nID_DECL_PDR = value;}
		}
		public string tDS_DECL_PDR
		{
			get{return _tDS_DECL_PDR;}
			set{_tDS_DECL_PDR = value;}
		}
		public string mDECL_EMPS
		{
			get{return _mDECL_EMPS;}
			set{_mDECL_EMPS = value;}
		}
		public string mDECL_EMPG
		{
			get{return _mDECL_EMPG;}
			set{_mDECL_EMPG = value;}
		}
		public string mDECL_EMPG_S_EPI
		{
			get{return _mDECL_EMPG_S_EPI;}
			set{_mDECL_EMPG_S_EPI = value;}
		}
	}
}
