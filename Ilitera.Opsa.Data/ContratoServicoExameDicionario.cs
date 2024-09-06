using System;
using System.Collections.Generic;
using System.Text;

using Ilitera.Data;
using Ilitera.Common;

namespace Ilitera.Opsa.Data
{
    [Database("opsa", "ContratoServicoExameDicionario", "IdContratoServicoExameDicionario")]
    public class ContratoServicoExameDicionario : Ilitera.Data.Table
    {
        private int _IdContratoServicoExameDicionario;
        private ContratoServico _IdContratoServico;
        private ExameDicionario _IdExameDicionario;
        private float _ValorUnitario = 0F;


        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public ContratoServicoExameDicionario()
        {

        }
        public override int Id
        {
            get { return _IdContratoServicoExameDicionario; }
            set { _IdContratoServicoExameDicionario = value; }
        }
        public ContratoServico IdContratoServico
        {
            get { return _IdContratoServico; }
            set { _IdContratoServico = value; }
        }
        public ExameDicionario IdExameDicionario
        {
            get { return _IdExameDicionario; }
            set { _IdExameDicionario = value; }
        }
        public float ValorUnitario
        {
            get { return _ValorUnitario; }
            set { _ValorUnitario = value; }
        }

        public void Reajustar(ContratoReajuste contratoReajuste)
        {
            ContratoServicoExameDicionarioReajuste reajuste = new ContratoServicoExameDicionarioReajuste();
            reajuste.Inicialize();
            reajuste.IdContratoServicoExameDicionario = this;
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
