using System;
using System.Text;
using System.Data;
using System.Collections;
using Ilitera.Data;
namespace Ilitera.Opsa.Data
{
	[Database("sied_novo", "tblEPC", "nID_EPC")]
	public class EPC: Ilitera.Data.Table 
	{
		private int _nID_EPC;
		private Risco _nID_RSC;
		private AgenteQuimico _nID_AG_NCV;
		private string _tNO_EPC=string.Empty;
		private string _tDS_EPC=string.Empty;
		private bool _gIN_SUG;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public EPC()
		{

		}
		public override int Id
		{														  
			get	{return _nID_EPC;}
			set {_nID_EPC = value;}
		}
		public Risco nID_RSC
		{														  
			get	{return _nID_RSC;}
			set {_nID_RSC = value;}
		}	
		public AgenteQuimico nID_AG_NCV
		{														  
			get	{return _nID_AG_NCV;}
			set {_nID_AG_NCV = value;}
		}
		public string tNO_EPC
		{														  
			get	{return _tNO_EPC;}
			set {_tNO_EPC = value;}
		}
		public string tDS_EPC
		{														  
			get	{return _tDS_EPC;}
			set {_tDS_EPC = value;}
		}
		public bool gIN_SUG
		{														  
			get	{return _gIN_SUG;}
			set {_gIN_SUG = value;}
		}
		public override string ToString()
		{
			return this._tNO_EPC;
		}
	}
}
