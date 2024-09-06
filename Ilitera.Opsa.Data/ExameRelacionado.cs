using System;
using Ilitera.Common;
using Ilitera.Data;

namespace Ilitera.Opsa.Data
{
	[Database("opsa", "ExameRelacionado", "IdExameRelacionado")]
	public class ExameRelacionado : Ilitera.Data.Table
	{
		private int _IdExameRelacionado;
		private ExameDicionario _IdExameDicionario;
		private ExameDicionario _IdExameDicionarioRelacionado;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public ExameRelacionado()
		{
		}

		public override int Id
		{														  
			get	{return _IdExameRelacionado;}
			set {_IdExameRelacionado = value;}
		}
		public ExameDicionario IdExameDicionario
		{														  
			get	{return _IdExameDicionario;}
			set {_IdExameDicionario = value;}
		}
		public ExameDicionario IdExameDicionarioRelacionado
		{														  
			get	{return _IdExameDicionarioRelacionado;}
			set {_IdExameDicionarioRelacionado = value;}
		}

		public override string ToString()
		{
			this.IdExameDicionarioRelacionado.Find();
			return this.IdExameDicionarioRelacionado.Nome;
		}
	}
}
