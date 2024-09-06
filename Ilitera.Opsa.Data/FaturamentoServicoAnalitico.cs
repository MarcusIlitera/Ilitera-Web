using System;
using System.Collections.Generic;
using System.Text;
using Ilitera.Data;
using Ilitera.Common;
using Ilitera.Opsa.Data;

namespace Ilitera.Opsa.Data
{
    [Database("opsa", "FaturamentoServicoAnalitico", "IdFaturamentoServicoAnalitico")]
    public class FaturamentoServicoAnalitico: Ilitera.Data.Table
    {
		private int _IdFaturamentoServicoAnalitico;
        private Faturamento _IdFaturamento;
		private Servico _IdServico;
        private Cliente _IdCliente;
        private float _Quantidade;
        private float _ValorUnitario;
        private float _ValorTotal;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public FaturamentoServicoAnalitico()
        {

        }
        public override int Id
        {
            get { return _IdFaturamentoServicoAnalitico; }
            set { _IdFaturamentoServicoAnalitico = value; }
        }
        public Faturamento IdFaturamento
        {
            get { return _IdFaturamento; }
            set { _IdFaturamento = value; }
        }
        public Servico IdServico
        {
            get { return _IdServico; }
            set { _IdServico = value; }
        }
        public Cliente IdCliente
        {
            get { return _IdCliente; }
            set { _IdCliente = value; }
        }
        public float Quantidade
        {
            get { return _Quantidade; }
            set { _Quantidade = value; }
        }
        public float ValorUnitario
        {
            get { return _ValorUnitario; }
            set { _ValorUnitario = value; }
        }
        public float ValorTotal
        {
            get { return _ValorTotal; }
            set { _ValorTotal = value; }
        }
    }

    [Database("opsa", "FaturamentoServicoAnaliticoEmpregado", "IdFaturamentoServicoAnaliticoEmpregado")]
    public class FaturamentoServicoAnaliticoEmpregado : Ilitera.Data.Table
    {
        private int _IdFaturamentoServicoAnaliticoEmpregado;
        private FaturamentoServicoAnalitico _IdFaturamentoServicoAnalitico;
        private Empregado _IdEmpregado;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public FaturamentoServicoAnaliticoEmpregado()
        {

        }
        public override int Id
        {
            get { return _IdFaturamentoServicoAnaliticoEmpregado; }
            set { _IdFaturamentoServicoAnaliticoEmpregado = value; }
        }

        public FaturamentoServicoAnalitico IdFaturamentoServicoAnalitico
        {
            get { return _IdFaturamentoServicoAnalitico; }
            set { _IdFaturamentoServicoAnalitico = value; }
        }

        public Empregado IdEmpregado
        {
            get { return _IdEmpregado; }
            set { _IdEmpregado = value; }
        }
    }
}
