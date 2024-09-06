using System;
using System.Collections.Generic;
using System.Text;
using Ilitera.Data;

namespace Ilitera.Opsa.Data
{
    [Database("opsa", "ContratoServicoExameDicionarioReajuste", "IdContratoServicoExameDicionarioReajuste")]
    public class ContratoServicoExameDicionarioReajuste : Ilitera.Data.Table
    {
        private int _IdContratoServicoExameDicionarioReajuste;
        private ContratoServicoExameDicionario _IdContratoServicoExameDicionario;
        private ContratoReajuste _IdContratoReajuste;
        private float _ValorAnterior = 0F;
        private float _ValorComReajuste = 0F;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public ContratoServicoExameDicionarioReajuste()
        {

        }
        public override int Id
        {
            get { return _IdContratoServicoExameDicionarioReajuste; }
            set { _IdContratoServicoExameDicionarioReajuste = value; }
        }
        public ContratoServicoExameDicionario IdContratoServicoExameDicionario
        {
            get { return _IdContratoServicoExameDicionario; }
            set { _IdContratoServicoExameDicionario = value; }
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
