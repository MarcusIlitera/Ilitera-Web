using System;
using System.Collections;
using Ilitera.Data;

namespace Ilitera.Opsa.Data
{
	/// <summary>
	/// Summary description for RateiroDespesaCliente.
	/// </summary>
	[Database("opsa", "RateioDespesaCliente", "IdRateioDespesaCliente")]
	public class RateioDespesaCliente :Ilitera.Data.Table
	{
		private int	_IdRateioDespesaCliente;
//		private Lancamento _IdLancamento;
		private Cliente _IdCliente;
		private string _Descricao = string.Empty;
		private DateTime _DataPagamento;
		private float _ValorPagamento;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public RateioDespesaCliente()
		{

		}

		public override int Id
		{														  
			get{return _IdRateioDespesaCliente;}
			set	{_IdRateioDespesaCliente = value;}
		}	
//		public Lancamento IdLancamento
//		{														  
//			get{return _IdLancamento;}
//			set	{_IdLancamento = value;}
//		}
		public Cliente IdCliente
		{														  
			get{return _IdCliente;}
			set	{_IdCliente = value;}
		}
		public string Descricao
		{														  
			get{return _Descricao;}
			set	{_Descricao = value;}
		}
		public DateTime DataPagamento
		{														  
			get{return _DataPagamento;}
			set	{_DataPagamento = value;}
		}
		public float ValorPagamento
		{														  
			get{return _ValorPagamento;}
			set	{_ValorPagamento = value;}
		}
	}
}
