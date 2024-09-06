using System;
using Ilitera.Data;
using System.Data;
using System.Collections;
using Ilitera.Common;

namespace Ilitera.Opsa.Data
{
	[Database("opsa","PagamentoClinica","IdPagamentoClinica")]
	public class PagamentoClinica : Ilitera.Data.Table , IAgendar
	{
		private int _IdPagamentoClinica;
		private Clinica _IdClinica;
		private DateTime _DataVencimento;
		private DateTime _DataPagamento;
		private double _ValorDocumento;
		private double _ValorOutros;
		private double _ValorImposto;
		private double _ValorPago;
		private string _DescricaoOutros = string.Empty;
		private Tarefa _IdTarefa;
		private Compromisso _IdCompromisso;
		private Prestador _IdPrestador;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public PagamentoClinica()
		{

		}
		public override int Id
		{
			get{return _IdPagamentoClinica;}
			set{_IdPagamentoClinica = value;}
		}
		[Obrigatorio(true, "Clinica é campo obrigatório!")]
		public Clinica IdClinica
		{
			get{return _IdClinica;}
			set{_IdClinica = value;}
		}
		public DateTime DataVencimento
		{
			get{return _DataVencimento;}
			set{_DataVencimento = value;}
		}
		public DateTime DataPagamento
		{
			get{return _DataPagamento;}
			set{_DataPagamento = value;}
		}
		public double ValorDocumento
		{
			get{return _ValorDocumento;}
			set{_ValorDocumento = value;}
		}
		public double ValorImposto
		{
			get{return _ValorImposto;}
			set{_ValorImposto = value;}
		}
		public double ValorOutros
		{
			get{return _ValorOutros;}
			set{_ValorOutros = value;}
		}
		public double ValorPago
		{
			get{return _ValorPago;}
			set{_ValorPago = value;}
		}
		public string DescricaoOutros
		{
			get{return _DescricaoOutros;}
			set{_DescricaoOutros = value;}
		}
		public Tarefa IdTarefa
		{	
			get{return _IdTarefa;}
			set{_IdTarefa = value;}
		}
		public Compromisso IdCompromisso
		{	
			get{return _IdCompromisso;}
			set{_IdCompromisso = value;}
		}
		public Prestador IdPrestador
		{	
			get{return _IdPrestador;}
			set{_IdPrestador = value;}
		}
		public override string ToString()
		{
			return this.DataPagamento.ToString("dd-MM-yyyy")+" "+ this.ValorPago.ToString("n");
		}
	}
}
