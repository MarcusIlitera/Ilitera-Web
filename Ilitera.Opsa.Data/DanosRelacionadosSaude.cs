using System;
using System.Text;
using System.Data;
using System.Collections;
using Ilitera.Data;

namespace Ilitera.Opsa.Data
{
	[Database("sied_novo", "tblDANO_REL_SAU", "nID_DANO_REL_SAU")]
	public class DanosRelacionadosSaude: Ilitera.Data.Table
	{
		private int _nID_DANO_REL_SAU;
		private Risco _nID_RSC;
		private AgenteQuimico _nID_AG_NCV;
		private bool _gIN_SUG;
		private string _tNO_DANO_REL_SAU = string.Empty;
		private string _mDS_DANO_REL_SAU = string.Empty;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public DanosRelacionadosSaude()
		{

		}
		public override int Id
		{														  
			get	{return _nID_DANO_REL_SAU;}
			set {_nID_DANO_REL_SAU = value;}
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
		public bool gIN_SUG
		{														  
			get	{return _gIN_SUG;}
			set {_gIN_SUG = value;}
		}
		public string tNO_DANO_REL_SAU
		{														  
			get	{return _tNO_DANO_REL_SAU;}
			set {_tNO_DANO_REL_SAU = value;}
		}
		public string mDS_DANO_REL_SAU
		{														  
			get	{return _mDS_DANO_REL_SAU;}
			set {_mDS_DANO_REL_SAU = value;}
		}
		public override string ToString()
		{
			return this.tNO_DANO_REL_SAU;
		}
	}
}
