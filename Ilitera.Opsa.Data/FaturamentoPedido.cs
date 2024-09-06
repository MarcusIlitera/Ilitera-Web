using System;
using System.Data;
using System.Text;
using System.Collections;
using Ilitera.Data;
using Ilitera.Common;
using Ilitera.Opsa.Data;


namespace Ilitera.Opsa.Data
{
	[Database("opsa", "FaturamentoPedido", "IdFaturamentoPedido")]
	public class FaturamentoPedido: Ilitera.Data.Table
	{
		private int _IdFaturamentoPedido;
		private Faturamento _IdFaturamento;
		private Servico _IdServico;
		private Cliente _IdCliente;
		private Pedido _IdPedido;
		private float _ValorUnitario;


        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public FaturamentoPedido()
		{

		}
		public override int Id
		{														  
			get{return _IdFaturamentoPedido;}
			set	{_IdFaturamentoPedido = value;}
		}
		public Faturamento IdFaturamento
		{														  
			get{return _IdFaturamento;}
			set	{_IdFaturamento = value;}
		}
		public Servico IdServico
		{														  
			get{return _IdServico;}
			set	{_IdServico = value;}
		}
		public Cliente IdCliente
		{														  
			get{return _IdCliente;}
			set	{_IdCliente = value;}
		}
		public Pedido IdPedido
		{														  
			get{return _IdPedido;}
			set	{_IdPedido = value;}
		}
		public float ValorUnitario
		{														  
			get{return _ValorUnitario;}
			set	{_ValorUnitario = value;}
		}

        public string GetDescricaoFatura()
        {
            if (this.IdPedido.mirrorOld == null)
                this.IdPedido.Find();

            StringBuilder ret = new StringBuilder();

            ret.Append("Pedido Nº " + this.IdPedido.GetNumeroPedido() + " - ");

            if (this.IdPedido.DataConclusao != new DateTime())
                ret.Append(this.IdPedido.DataConclusao.ToString("dd-MM-yyyy"));
            else
                ret.Append(this.IdPedido.GetSituacaoPedido());

            ret.Append(" - ");

            ret.Append(this.IdPedido.IdObrigacao.NomeReduzido);

            return ret.ToString();
        }
	}

	[Database("opsa", "FaturamentoExameBase", "IdFaturamentoExameBase")]
	public class FaturamentoExameBase: Ilitera.Data.Table
	{
		private int _IdFaturamentoExameBase;
		private Faturamento _IdFaturamento;
		private Servico _IdServico;
		private Cliente _IdCliente;
		private ExameBase _IdExameBase;
		private float _ValorUnitario;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public FaturamentoExameBase()
		{

		}
		public override int Id
		{														  
			get{return _IdFaturamentoExameBase;}
			set	{_IdFaturamentoExameBase = value;}
		}
		public Faturamento IdFaturamento
		{														  
			get{return _IdFaturamento;}
			set	{_IdFaturamento = value;}
		}
		public Servico IdServico
		{														  
			get{return _IdServico;}
			set	{_IdServico = value;}
		}
		public Cliente IdCliente
		{														  
			get{return _IdCliente;}
			set	{_IdCliente = value;}
		}
		public ExameBase IdExameBase
		{														  
			get{return _IdExameBase;}
			set	{_IdExameBase = value;}
		}
		public float ValorUnitario
		{														  
			get{return _ValorUnitario;}
			set	{_ValorUnitario = value;}
		}
		public override void Delete()
		{
			base.Delete ();
		}
	}
}
