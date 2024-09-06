using System;
using Ilitera.Data;
using Ilitera.Common;

namespace Ilitera.Opsa.Data
{
	[Database("opsa", "Movimentacao", "IdMovimentacao")]
	public class Movimentacao : Ilitera.Data.Table
	{
		private int _IdMovimentacao;
		private int _IndDebitoCredito;
		private string _Documento = string.Empty;
		private float _ValorDocumento;
		private DateTime _DataDocumento;
		private string _Descricao =string.Empty;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public Movimentacao()
		{

		}
		public override int Id
		{														  
			get{return _IdMovimentacao;}
			set	{_IdMovimentacao = value;}
		}
		public int IndDebitoCredito
		{														  
			get{return _IndDebitoCredito;}
			set	{_IndDebitoCredito = value;}
		}
		public string Documento
		{														  
			get{return _Documento;}
			set	{_Documento = value;}
		}
		public float ValorDocumento
		{														  
			get{return _ValorDocumento;}
			set	{_ValorDocumento = value;}
		}
		public DateTime DataDocumento
		{														  
			get{return _DataDocumento;}
			set	{_DataDocumento = value;}
		}
		public string Descricao
		{														  
			get{return _Descricao;}
			set	{_Descricao = value;}
		}
	}

	[Database("opsa", "Bancaria", "IdBancaria")]
	public class Bancaria: Ilitera.Opsa.Data.Movimentacao
	{
		private int _IdBancaria;
		private ContaCorrente _IdContaCorrente;
		private int _Ordem;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public Bancaria()
		{

		}
		public override int Id
		{														  
			get{return _IdBancaria;}
			set	{_IdBancaria = value;}
		}
		public ContaCorrente IdContaCorrente
		{														  
			get{return _IdContaCorrente;}
			set	{_IdContaCorrente = value;}
		}
		public int Ordem
		{														  
			get{return _Ordem;}
			set	{_Ordem = value;}
		}
	}
}
