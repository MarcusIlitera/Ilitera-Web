using System;
using System.Collections;
using System.Data;
using Ilitera.Data;
using Ilitera.Common;

namespace Ilitera.Opsa.Data
{	
	public enum CronogramaTipo: int
	{
		PPRA,
		Ergonomia
	}

	[Database("opsa", "Cronograma", "IdCronograma")]
	public class Cronograma : Ilitera.Data.Table
	{
		private int _IdCronograma;
		private int _IndCronograma;
		private DateTime _Prazo;
		private bool _Mes01;
		private bool _Mes02;
		private bool _Mes03;
		private bool _Mes04;
		private bool _Mes05;
		private bool _Mes06;
		private bool _Mes07;
		private bool _Mes08;
		private bool _Mes09;
		private bool _Mes10;
		private bool _Mes11;
		private bool _Mes12;
		private string _Ano = string.Empty;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public Cronograma()
		{

		}	
		public override int Id
		{														  
			get	{return _IdCronograma;}
			set {_IdCronograma = value;}
		}	
		public int IndCronograma
		{														  
			get	{return _IndCronograma;}
			set {_IndCronograma = value;}
		}
		public DateTime Prazo
		{														  
			get	{return _Prazo;}
			set {_Prazo = value;}
		}
		public bool Mes01
		{														  
			get	{return _Mes01;}
			set {_Mes01 = value;}
		}
		public bool Mes02
		{														  
			get	{return _Mes02;}
			set {_Mes02 = value;}
		}
		public bool Mes03
		{														  
			get	{return _Mes03;}
			set {_Mes03 = value;}
		}
		public bool Mes04
		{														  
			get	{return _Mes04;}
			set {_Mes04 = value;}
		}
		public bool Mes05
		{														  
			get	{return _Mes05;}
			set {_Mes05 = value;}
		}
		public bool Mes06
		{														  
			get	{return _Mes06;}
			set {_Mes06 = value;}
		}
		public bool Mes07
		{														  
			get	{return _Mes07;}
			set {_Mes07 = value;}
		}
		public bool Mes08
		{														  
			get	{return _Mes08;}
			set {_Mes08 = value;}
		}
		public bool Mes09
		{														  
			get	{return _Mes09;}
			set {_Mes09 = value;}
		}
		public bool Mes10
		{														  
			get	{return _Mes10;}
			set {_Mes10 = value;}
		}
		public bool Mes11
		{														  
			get	{return _Mes11;}
			set {_Mes11 = value;}
		}
		public bool Mes12
		{														  
			get	{return _Mes12;}
			set {_Mes12 = value;}
		}
		public string Ano
		{														  
			get	{return _Ano;}
			set {_Ano = value;}
		}
	}


	[Database("opsa", "CronogramaPPRA", "IdCronogramaPPRA")]
	public class CronogramaPPRA : Ilitera.Opsa.Data.Cronograma
	{
		private int _IdCronogramaPPRA;
		private LaudoTecnico _IdLaudoTecnico;
		private string _PlanejamentoAnual = string.Empty;
		private string _MetodologiaAcao = string.Empty;
		private string _FormaRegistro = string.Empty;
        private Int16 _Prioridade = 0;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public CronogramaPPRA()
		{
			this.IndCronograma = (int)CronogramaTipo.PPRA;
		}	
		public override int Id
		{														  
			get	{return _IdCronogramaPPRA;}
			set {_IdCronogramaPPRA = value;}
		}	
		public LaudoTecnico IdLaudoTecnico
		{														  
			get	{return _IdLaudoTecnico;}
			set {_IdLaudoTecnico = value;}
		}
		public string PlanejamentoAnual
		{														  
			get	{return _PlanejamentoAnual;}
			set {_PlanejamentoAnual = value;}
		}
		public string MetodologiaAcao
		{														  
			get	{return _MetodologiaAcao;}
			set {_MetodologiaAcao = value;}
		}
		public string FormaRegistro
		{														  
			get	{return _FormaRegistro;}
			set {_FormaRegistro = value;}
		}
        public Int16 Prioridade
        {
            get { return _Prioridade; }
            set { _Prioridade = value; }
        }


        public void PopularPrazo()
		{
			if(this.Prazo.Month==1)
				this.Mes01 = true;
			else if(this.Prazo.Month==2)
				this.Mes02 = true;
			else if(this.Prazo.Month==3)
				this.Mes03 = true;
			else if(this.Prazo.Month==4)
				this.Mes04 = true;
			else if(this.Prazo.Month==5)
				this.Mes05 = true;
			else if(this.Prazo.Month==6)
				this.Mes06 = true;
			else if(this.Prazo.Month==7)
				this.Mes07 = true;
			else if(this.Prazo.Month==8)
				this.Mes08 = true;
			else if(this.Prazo.Month==9)
				this.Mes09 = true;
			else if(this.Prazo.Month==10)
				this.Mes10 = true;
			else if(this.Prazo.Month==11)
				this.Mes11 = true;
			else if(this.Prazo.Month==12)
				this.Mes12 = true;

			if(this.Ano == string.Empty)
				this.Ano = this.Prazo.Year.ToString();
			else
			{
				if(this.Ano.IndexOf(this.Prazo.Year.ToString())== -1)
					this.Ano = this.Ano + " e "+this.Prazo.Year.ToString();
			}
		}

		public static ArrayList GetCronograma(LaudoTecnico laudoTecnico)
		{
			ArrayList list = new CronogramaPPRA().Find("IdLaudoTecnico="+laudoTecnico.Id
				+" ORDER BY PlanejamentoAnual");
			
			if(list.Count==0)
				laudoTecnico.GerarCronogramaPadrao();

			return list;
		}
	}
}
