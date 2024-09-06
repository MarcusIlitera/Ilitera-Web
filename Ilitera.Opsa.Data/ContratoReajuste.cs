using System;
using System.Collections.Generic;
using System.Text;
using Ilitera.Data;

namespace Ilitera.Opsa.Data
{
    [Database("opsa", "ContratoReajuste", "IdContratoReajuste")]
    public class ContratoReajuste : Ilitera.Data.Table 
    {
        private int _IdContratoReajuste;
        private Contrato _IdContrato;
        private Cotacao _IdCotacao;
        private DateTime _DataReajuste;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public ContratoReajuste()
		{

		}

        public override int Id
        {
            get { return _IdContratoReajuste; }
            set { _IdContratoReajuste = value; }
        }
        public Contrato IdContrato
        {
            get { return _IdContrato; }
            set { _IdContrato = value; }
        }
        public Cotacao IdCotacao
        {
            get { return _IdCotacao; }
            set { _IdCotacao = value; }
        }
        public DateTime DataReajuste
        {
            get { return _DataReajuste; }
            set { _DataReajuste = value; }
        }
        public override string ToString()
        {
            return this.DataReajuste.ToString("dd-MM-yyyy") + " (" + this.IdCotacao.ToString()+")";
        }
    }
}
