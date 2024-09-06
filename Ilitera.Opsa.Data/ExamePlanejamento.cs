using System;
using System.Collections;

using Ilitera.Common;
using Ilitera.Data;

namespace Ilitera.Opsa.Data
{	
	[Database("opsa", "ExamePlanejamento", "IdExamePlanejamento")]
	public class ExamePlanejamento: Ilitera.Data.Table
	{
		private int _IdExamePlanejamento;
		private PcmsoPlanejamento _IdPcmsoPlanejamento;
        private Pcmso _IdPcmso;
		private Empregado _IdEmpregado;
		private ExameDicionario _IdExameDicionario;
		private DateTime _DataUltima;
		private DateTime _DataVencimento;
		private DateTime _DataProxima;
		private bool _PossuiRisco;
		private int _Intervalo;
		private int _IndPeriodicidade;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public ExamePlanejamento()
		{

		}
		public override int Id
		{														  
			get	{return _IdExamePlanejamento;}
			set {_IdExamePlanejamento = value;}
		}
		public PcmsoPlanejamento IdPcmsoPlanejamento
		{														  
			get	{return _IdPcmsoPlanejamento;}
			set {_IdPcmsoPlanejamento = value;}
		}
        public Pcmso IdPcmso
        {
            get { return _IdPcmso; }
            set { _IdPcmso = value; }
        }
		public Empregado IdEmpregado
		{														  
			get	{return _IdEmpregado;}
			set {_IdEmpregado = value;}
		}
		public ExameDicionario IdExameDicionario
		{														  
			get	{return _IdExameDicionario;}
			set {_IdExameDicionario = value;}
		}
		public DateTime DataUltima
		{														  
			get	{return _DataUltima;}
			set {_DataUltima = value;}
		}
		public DateTime DataVencimento
		{														  
			get	{return _DataVencimento;}
			set {_DataVencimento = value;}
		}
		public DateTime DataProxima
		{														  
			get	{return _DataProxima;}
			set {_DataProxima = value;}
		}
		public bool PossuiRisco
		{														  
			get	{return _PossuiRisco;}
			set {_PossuiRisco = value;}
		}
		public int Intervalo
		{														  
			get	{return _Intervalo;}
			set {_Intervalo = value;}
		}
		public int IndPeriodicidade
		{														  
			get	{return _IndPeriodicidade;}
			set {_IndPeriodicidade = value;}
		}
	}
}
