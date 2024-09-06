using System;
using Ilitera.Data;
using Ilitera.Common;

namespace Ilitera.Opsa.Data
{
	[Database("opsa", "Lancamento", "IdLancamento")]
	public class Lancamento : Ilitera.Data.Table
	{
		private int _IdLancamento;
		private DateTime _DataLancamento;
		private Conta _IdContaSaida;
		private Conta _IdContaEntrada;
		private Empresa _IdEmpresa;
		private float _ValorLancamento;
		private int _IdHistoricoPadrao;
		private string _Complemento = string.Empty;
		private string _Observacao = string.Empty;
		private DateTime _DataBalanco;
		private bool _IsBalanco;
		private bool _IsSaldoInicial;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public Lancamento()
		{

		}
		public override int Id
		{														  
			get{return _IdLancamento;}
			set	{_IdLancamento = value;}
		}
		public DateTime DataLancamento
		{														  
			get{return _DataLancamento;}
			set	{_DataLancamento = value;}
		}
		public Conta IdContaSaida
		{														  
			get{return _IdContaSaida;}
			set	{_IdContaSaida = value;}
		}
		public Conta IdContaEntrada
		{														  
			get{return _IdContaEntrada;}
			set	{_IdContaEntrada = value;}
		}
		public Empresa IdEmpresa
		{														  
			get{return _IdEmpresa;}
			set	{_IdEmpresa = value;}
		}
		public float ValorLancamento
		{														  
			get{return _ValorLancamento;}
			set	{_ValorLancamento = value;}
		}
		public int IdHistoricoPadrao
		{														  
			get{return _IdHistoricoPadrao;}
			set	{_IdHistoricoPadrao = value;}
		}
		public string Complemento 
		{														  
			get{return _Complemento;}
			set	{_Complemento = value;}
		}
		public string Observacao 
		{														  
			get{return _Observacao;}
			set	{_Observacao = value;}
		}
		public DateTime DataBalanco
		{														  
			get{return _DataBalanco;}
			set	{_DataBalanco = value;}
		}
		public bool IsBalanco
		{														  
			get{return _IsBalanco;}
			set	{_IsBalanco = value;}
		}
		public bool IsSaldoInicial
		{														  
			get{return _IsSaldoInicial;}
			set	{_IsSaldoInicial = value;}
		}
	}
}
