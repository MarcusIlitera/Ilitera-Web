using System;
using Ilitera.Data;

namespace Ilitera.Opsa.Data
{
	/// <summary>
	/// Summary description for ObrigacaoAtividade.
	/// </summary>
		
	[Database("opsa","Atividade","IdAtividade")]
	public class Atividade: Ilitera.Data.Table 
	{
		private int _IdAtividade;
		private string _Descricao=string.Empty;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public Atividade()
		{

		}
		public override int Id
		{
			get{return _IdAtividade;}
			set{_IdAtividade = value;}
		}
		public string Descricao
		{
			get{return _Descricao;}
			set{_Descricao = value;}
		}
		public override string ToString()
		{
			return this._IdAtividade.ToString("000") + " - " + this._Descricao.ToString();
		}
	}
	
	[Database("opsa","ObrigacaoAtividade","IdObrigacaoAtividade")]
	public class ObrigacaoAtividade: Ilitera.Data.Table 
	{
		private int _IdObrigacaoAtividade;
		private Obrigacao _IdObrigacao;
		private Atividade _IdAtividade;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public ObrigacaoAtividade()
		{

		}
		public override int Id
		{
			get{return _IdObrigacaoAtividade;}
			set{_IdObrigacaoAtividade = value;}
		}
		public Obrigacao IdObrigacao
		{
			get{return _IdObrigacao;}
			set{_IdObrigacao = value;}
		}
		public Atividade IdAtividade
		{
			get{return _IdAtividade;}
			set{_IdAtividade = value;}
		}
		public override string ToString()
		{
			this._IdObrigacao.Find();
			return  this._IdObrigacao.ToString();
		}
	}

	[Database("opsa","ObrigacaoAtividade","IdObrigacaoAtividade")]
	public class AtividadeObrigacao: Ilitera.Data.Table 
	{
		private int _IdObrigacaoAtividade;
		private Obrigacao _IdObrigacao;
		private Atividade _IdAtividade;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public AtividadeObrigacao()
		{

		}
		public override int Id
		{
			get{return _IdObrigacaoAtividade;}
			set{_IdObrigacaoAtividade = value;}
		}
		public Obrigacao IdObrigacao
		{
			get{return _IdObrigacao;}
			set{_IdObrigacao = value;}
		}
		public Atividade IdAtividade
		{
			get{return _IdAtividade;}
			set{_IdAtividade = value;}
		}
		public override string ToString()
		{
			this._IdAtividade.Find();
			return  this._IdAtividade.ToString();
		}
	}
}
