using System;
using Ilitera.Data;


namespace Ilitera.Opsa.Data
{
	[Database("opsa","ControleObrigacao","IdControleObrigacao")]
	public class ControleObrigacao : Ilitera.Data.Table  
	{
		private int _IdControleObrigacao;
		private Obrigacao _IdObrigacao;
		private Controle _IdControle;
		private int _Ordem;
		private bool _IsProducao;
		private bool _IsInativo;


        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public ControleObrigacao()
		{
		}
		public override int Id
		{
			get{return _IdControleObrigacao;}
			set{_IdControleObrigacao = value;}
		}
		public Obrigacao IdObrigacao
		{
			get{return _IdObrigacao;}
			set{_IdObrigacao = value;}
		}
		[Obrigatorio(true, "Controle é campo obrigatório!")]
		public Controle IdControle
		{
			get{return _IdControle;}
			set{_IdControle = value;}
		}
		[Obrigatorio(true, "Ordem é campo obrigatório!")]
		public int Ordem
		{
			get{return _Ordem;}
			set{_Ordem = value;}
		}
		public bool IsProducao
		{
			get{return _IsProducao;}
			set{_IsProducao = value;}
		}
		public bool IsInativo
		{
			get{return _IsInativo;}
			set{_IsInativo = value;}
		}		
		public override string ToString()
		{
			_IdControle.Find();
			if(_IdControle.Nome==string.Empty)
				return string.Empty;
			else
				return _Ordem.ToString("000") + " - " +  _IdControle.Nome;
		}
	}
}
