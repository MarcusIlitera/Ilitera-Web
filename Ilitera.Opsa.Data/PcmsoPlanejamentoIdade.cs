using System;
using System.Data;
using System.Collections;
using Ilitera.Data;

namespace Ilitera.Opsa.Data
{
    interface IPeriodicidadeIdade
    {
        int AnoInicio { get; set; }
        int AnoTermino { get; set; }
        int IndPeriodicidade { get; set; }
        int IndSexoIdade { get; set; }
        int Intervalo { get; set; }
    }

	[Database("opsa","PcmsoPlanejamentoIdade","IdPcmsoPlanejamentoIdade")]
	public class PcmsoPlanejamentoIdade: Ilitera.Data.Table, Ilitera.Opsa.Data.IPeriodicidadeIdade
	{
		private int _IdPcmsoPlanejamentoIdade;
		private PcmsoPlanejamento _IdPcmsoPlanejamento;
		private int _AnoInicio;
		private int _AnoTermino;
		private int _IndPeriodicidade;
		private int _Intervalo;
		private int _IndSexoIdade=1;
        private bool _Temporario;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public PcmsoPlanejamentoIdade()
		{

		}

		public override int Id
		{
			get{return _IdPcmsoPlanejamentoIdade;}
			set{_IdPcmsoPlanejamentoIdade = value;}
		}
		public PcmsoPlanejamento IdPcmsoPlanejamento
		{
			get{return _IdPcmsoPlanejamento;}
			set{_IdPcmsoPlanejamento = value;}
		}
		public int AnoInicio
		{
			get{return _AnoInicio;}
			set{_AnoInicio = value;}
		}
		public int IndPeriodicidade
		{
			get{return _IndPeriodicidade;}
			set{_IndPeriodicidade = value;}
		}
		public int Intervalo
		{
			get{return _Intervalo;}
			set{_Intervalo = value;}
		}
		public int AnoTermino
		{
			get{return _AnoTermino;}
			set{_AnoTermino = value;}
		}
		public int IndSexoIdade
		{
			get{return _IndSexoIdade;}
			set{_IndSexoIdade = value;}
		}
        public bool Temporario
        {
            get { return _Temporario; }
            set { _Temporario = value; }
        }

    }
}
