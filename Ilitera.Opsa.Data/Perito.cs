using System;
using Ilitera.Data; 
using Ilitera.Common; 

namespace Ilitera.Opsa.Data
{	
	[Database("opsa","Perito","IdPerito")]
	public class Perito : Ilitera.Common.Fisica
	{
		public Perito()
		{			
			this.IndFisicaPapel = FisicaPapel.Perito;
		}
		private int _IdPerito;
        private string _Profissao = string.Empty;
		private	string _Observacao = string.Empty;
		
		public override int Id
		{
			get{return _IdPerito;}
			set{_IdPerito = value;}
		}
        public string Profissao
        {
            get { return _Profissao; }
            set { _Profissao = value; }
        }	
		public string Observacao
		{
			get{return _Observacao;}
			set{_Observacao = value;}
		}		
	}

    [Database("opsa", "Advogado", "IdAdvogado")]
    public class Advogado : Ilitera.Common.Fisica
    {
        public Advogado()
        {
            this.IndFisicaPapel = FisicaPapel.Advogado;
        }
        private int _IdAdvogado;
        private string _Numero = string.Empty;

        public override int Id
        {
            get { return _IdAdvogado; }
            set { _IdAdvogado = value; }
        }
        public string Numero
        {
            get { return _Numero; }
            set { _Numero = value; }
        }
    }
}
