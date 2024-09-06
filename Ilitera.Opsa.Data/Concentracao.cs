using System;
using Ilitera.Data;

namespace Ilitera.Opsa.Data
{	
	[Database("opsa","Concentracao","IdConcentracao")]
	public class Concentracao : Ilitera.Data.Table 
	{
		private int _IdConcentracao;
		private AgenteQuimico _IdAgenteQuimico;
		private float _ConcentracaoDe;
		private float _ConcentracaoAte;


        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public Concentracao()
		{			
		}

		public override int Id
		{
			get{return _IdConcentracao;}
			set{_IdConcentracao = value;}
		}
		public AgenteQuimico IdAgenteQuimico
		{
			get{return _IdAgenteQuimico;}
			set{_IdAgenteQuimico = value;}
		}
		public float ConcentracaoDe
		{
			get{return _ConcentracaoDe;}
			set{_ConcentracaoDe = value;}
		}
		public float ConcentracaoAte
		{
			get{return _ConcentracaoAte;}
			set{_ConcentracaoAte = value;}
		}
		public override string ToString()
		{
			return "Concentração De " + this.ConcentracaoDe + " Até " + this.ConcentracaoAte;
		}
	}
}
