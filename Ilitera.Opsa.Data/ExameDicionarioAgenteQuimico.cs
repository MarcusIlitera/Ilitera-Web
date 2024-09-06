using System;
using System.Collections;
using System.Text;
using System.Data;
using Ilitera.Data;
using Ilitera.Common;

namespace Ilitera.Opsa.Data
{
	[Database("opsa","ExameDicionarioAgenteQuimico","IdExameDicionarioAgenteQuimico")]
	public class ExameDicionarioAgenteQuimico: Ilitera.Data.Table
	{
		private int _IdExameDicionarioAgenteQuimico;
		private ExameDicionario _IdExameDicionario;
		private AgenteQuimico _IdAgenteQuimico;
		private MateriaPrima _IdMateriaPrima;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public ExameDicionarioAgenteQuimico()
		{

		}
		public override int Id
		{
			get{return _IdExameDicionarioAgenteQuimico;}
			set{_IdExameDicionarioAgenteQuimico = value;}
		}
		public ExameDicionario IdExameDicionario
		{
			get{return _IdExameDicionario;}
			set{_IdExameDicionario = value;}
		}
		public AgenteQuimico IdAgenteQuimico
		{
			get{return _IdAgenteQuimico;}
			set{_IdAgenteQuimico = value;}
		}
		public MateriaPrima IdMateriaPrima
		{
			get{return _IdMateriaPrima;}
			set{_IdMateriaPrima = value;}
		}

		public override string ToString()
		{
			string ret = string.Empty;
			if(this.IdAgenteQuimico.Id!=0)
			{
				this.IdAgenteQuimico.Find();
				ret = this.IdAgenteQuimico.ToString();
			}
			else if(this.IdMateriaPrima.Id!=0)
			{
				this.IdMateriaPrima.Find();
				ret = this.IdMateriaPrima.ToString();
			}
			return ret;
		}		
	}
}
