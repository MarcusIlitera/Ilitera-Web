using System;
using System.Collections.Generic;
using System.Text;
using Ilitera.Data;

namespace Ilitera.Opsa.Data
{
    [Database("opsa", "ContratoServicoObrigacaoReajuste", "IdContratoServicoObrigacaoReajuste")]
    public class ContratoServicoObrigacaoReajuste : Ilitera.Data.Table
    {
        private int _IdContratoServicoObrigacaoReajuste;
        private ContratoServicoObrigacao _IdContratoServicoObrigacao;
        private ContratoReajuste _IdContratoReajuste;
        private float _ValorAnterior = 0F;
        private float _ValorComReajuste = 0F;


        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public ContratoServicoObrigacaoReajuste()
        {

        }
        public override int Id
        {
            get { return _IdContratoServicoObrigacaoReajuste; }
            set { _IdContratoServicoObrigacaoReajuste = value; }
        }
        public ContratoServicoObrigacao IdContratoServicoObrigacao
        {
            get { return _IdContratoServicoObrigacao; }
            set { _IdContratoServicoObrigacao = value; }
        }
        public ContratoReajuste IdContratoReajuste
        {
            get { return _IdContratoReajuste; }
            set { _IdContratoReajuste = value; }
        }
        public float ValorAnterior
        {
            get { return _ValorAnterior; }
            set { _ValorAnterior = value; }
        }
        public float ValorComReajuste
        {
            get { return _ValorComReajuste; }
            set { _ValorComReajuste = value; }
        }
    }
}
