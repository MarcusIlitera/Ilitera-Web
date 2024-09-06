using System;
using Ilitera.Data;
using Ilitera.Common;
using Ilitera.Opsa.Data;

namespace Ilitera.Marshall.Data
{
	[Database("marshall", "ReceberPagar", "ID", true)]
	public class ReceberPagar: Ilitera.Data.Table
	{
		private int _ID;
		private bool _DC;
		private Cliente _IdCliente;
		private DateTime _DataEmissão;
		private string _Documento=string.Empty;
		private string _Contato=string.Empty;
		private string _Telefone=string.Empty;
		private Contas _Conta;
		private float _ValorDocumento;
		private float _ValorImposto;
		private float _ValorImpostoPisConfisCsll;
		private DateTime _DataVencimento;
		private float _ValorJuros;
		private float _ValorPago;
		private DateTime _DataPagamento;
		private string _Observação=string.Empty;
		private bool _FlagBaixa;
		private string _TipoNota;
		private DateTime _Estorno;
		private int _MicroEmpresa;
		private int _CodigoLancamento;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public ReceberPagar()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		public override int Id
		{
			get{return _ID;}
			set{_ID = value;}
		}
		public bool DC
		{
			get{return _DC;}
			set{_DC = value;}
		}
		public Cliente IdCliente
		{
			get{return _IdCliente;}
			set{_IdCliente = value;}
		}
		public DateTime DataEmissão
		{
			get{return _DataEmissão;}
			set{_DataEmissão = value;}
		}
		public Contas Conta
		{
			get{return _Conta;}
			set{_Conta = value;}
		}
		public string Documento
		{
			get{return _Documento;}
			set{_Documento = value;}
		}
		public float ValorDocumento
		{
			get{return _ValorDocumento;}
			set{_ValorDocumento = value;}
		}
		public float ValorImposto
		{
			get{return _ValorImposto;}
			set{_ValorImposto = value;}
		}
		public float ValorImpostoPisConfisCsll
		{
			get{return _ValorImpostoPisConfisCsll;}
			set{_ValorImpostoPisConfisCsll = value;}
		}
		public DateTime DataVencimento
		{
			get{return _DataVencimento;}
			set{_DataVencimento = value;}
		}
		public float ValorJuros
		{
			get{return _ValorJuros;}
			set{_ValorJuros = value;}
		}
		public float ValorPago
		{
			get{return _ValorPago;}
			set{_ValorPago = value;}
		}
		public DateTime DataPagamento
		{
			get{return _DataPagamento;}
			set{_DataPagamento = value;}
		}
		public string Observação
		{
			get{return _Observação;}
			set{_Observação = value;}
		}
		public bool FlagBaixa
		{
			get{return _FlagBaixa;}
			set{_FlagBaixa = value;}
		}
		public string TipoNota
		{
			get{return _TipoNota;}
			set{_TipoNota = value;}
		}
		public DateTime Estorno
		{
			get{return _Estorno;}
			set{_Estorno = value;}
		}
		public int MicroEmpresa
		{
			get{return _MicroEmpresa;}
			set{_MicroEmpresa = value;}
		}
		public int CodigoLancamento
		{
			get{return _CodigoLancamento;}
			set{_CodigoLancamento = value;}
		}
	}
}
