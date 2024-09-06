using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using Ilitera.Data;
using Ilitera.Common;

namespace Ilitera.Opsa.Data
{
    [Database("opsa", "ContratoServicoObrigacao", "IdContratoServicoObrigacao")]
    public class ContratoServicoObrigacao : Ilitera.Data.Table
    {
        private int _IdContratoServicoObrigacao;
        private ContratoServico _IdContratoServico;
        private Obrigacao _IdObrigacao;
        private float _ValorUnitario = 0F;
        private int _MesFaturamento;


        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public ContratoServicoObrigacao()
        {

        }
        public override int Id
        {
            get { return _IdContratoServicoObrigacao; }
            set { _IdContratoServicoObrigacao = value; }
        }
        public ContratoServico IdContratoServico
        {
            get { return _IdContratoServico; }
            set { _IdContratoServico = value; }
        }
        public Obrigacao IdObrigacao
        {
            get { return _IdObrigacao; }
            set { _IdObrigacao = value; }
        }
        public int MesFaturamento
        {
            get { return _MesFaturamento; }
            set { _MesFaturamento = value; }
        }
        public float ValorUnitario
        {
            get { return _ValorUnitario; }
            set { _ValorUnitario = value; }
        }

        public void Reajustar(ContratoReajuste contratoReajuste)
        {
            ContratoServicoObrigacaoReajuste reajuste = new ContratoServicoObrigacaoReajuste();
            reajuste.Inicialize();
            reajuste.IdContratoServicoObrigacao = this;
            reajuste.IdContratoReajuste = contratoReajuste;
            reajuste.ValorAnterior = this.ValorUnitario;
            reajuste.ValorComReajuste = Convert.ToSingle(System.Math.Round(Convert.ToDecimal((this.ValorUnitario * contratoReajuste.IdCotacao.ValorCotacao) + this.ValorUnitario), 2));
            
            this.ValorUnitario = reajuste.ValorComReajuste;

            reajuste.Transaction = contratoReajuste.Transaction;
            reajuste.Save();

            this.Transaction = contratoReajuste.Transaction;
            this.Save();
        }
    }
}
