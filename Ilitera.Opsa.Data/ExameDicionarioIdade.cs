using System;
using Ilitera.Data;

namespace Ilitera.Opsa.Data
{
	[Database("opsa","ExameDicionarioIdade","IdExameDicionarioIdade")]
    public class ExameDicionarioIdade : Ilitera.Data.Table, Ilitera.Opsa.Data.IPeriodicidadeIdade
	{
		private int _IdExameDicionarioIdade;
		private ExameDicionario _IdExameDicionario;
		private int _AnoInicio;
		private int _AnoTermino;
		private int _IndPeriodicidade;
		private int _Intervalo;
		private int _IndSexoIdade=1;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public ExameDicionarioIdade()
		{

		}
		public override int Id
		{
			get{return _IdExameDicionarioIdade;}
			set{_IdExameDicionarioIdade = value;}
		}
		public ExameDicionario IdExameDicionario
		{
			get{return _IdExameDicionario;}
			set{_IdExameDicionario = value;}
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
	}
}
