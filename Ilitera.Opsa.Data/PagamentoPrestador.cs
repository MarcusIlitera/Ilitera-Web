using System;
using System.Collections;
using System.Data;
using Ilitera.Data;
using Ilitera.Common;


namespace Ilitera.Opsa.Data
{
	[Database("opsa","PagamentoPrestador","IdPagamentoPrestador")]
	public class PagamentoPrestador : Ilitera.Data.Table 
	{
		private int _IdPagamentoPrestador;
		private Prestador _IdPrestador;
		private Pedido _IdPedido;
		private Obrigacao _IdObrigacao;
		private Cliente _IdCliente;
		private DateTime _Data;
		private float _Quantidade;
		private double _Valor;
		private string _Observacao = string.Empty;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public PagamentoPrestador()
		{

		}

		public override int Id
		{
			get{return _IdPagamentoPrestador;}
			set{_IdPagamentoPrestador = value;}
		}
		public Prestador IdPrestador
		{
			get{return _IdPrestador;}
			set{_IdPrestador = value;}
		}
		public Pedido IdPedido
		{
			get{return _IdPedido;}
			set{_IdPedido = value;}
		}
		public Obrigacao IdObrigacao
		{
			get{return _IdObrigacao;}
			set{_IdObrigacao = value;}
		}
		public Cliente IdCliente
		{
			get{return _IdCliente;}
			set{_IdCliente = value;}
		}
		public DateTime Data
		{
			get{return _Data;}
			set{_Data = value;}
		}
		public float Quantidade
		{
			get{return _Quantidade;}
			set{_Quantidade = value;}
		}
		public double Valor
		{
			get{return _Valor;}
			set{_Valor = value;}
		}
		public string Observacao
		{
			get{return _Observacao;}
			set{_Observacao = value;}
		}
	}
}
