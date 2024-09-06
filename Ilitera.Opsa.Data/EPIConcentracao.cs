using System;
using Ilitera.Data;

namespace Ilitera.Opsa.Data
{	
	[Database("opsa", "EPIConcentracao", "IdEPIConcentracao")]
	public class EPIConcentracao : Ilitera.Data.Table
	{
		private int _IdEPIConcentracao;
		private Epi _IdEpi;
		private Concentracao _IdConcentracao;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public EPIConcentracao()
		{		
		}
		
		public override int Id
		{														  
			get	{return _IdEPIConcentracao;}
			set {_IdEPIConcentracao = value;}
		}
		public Epi IdEpi
		{														  
			get	{return _IdEpi;}
			set {_IdEpi = value;}
		}
		public Concentracao IdConcentracao
		{														  
			get	{return _IdConcentracao;}
			set {_IdConcentracao = value;}
		}
		public override string ToString()
		{
			this.IdEpi.Find();
			return this.IdEpi.Descricao;
		}
	}
}
