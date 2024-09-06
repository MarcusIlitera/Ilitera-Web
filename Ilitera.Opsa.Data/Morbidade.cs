using System;
using System.Collections.Generic;
using System.Text;
using Ilitera.Data;

namespace Ilitera.Opsa.Data
{
    [Database("opsa", "FatorRisco", "IdFatorRisco", "", "Fator de Risco para morbidade")]
    public class FatorRisco : Table
    {
        private int _IdFatorRisco;
        private Cliente _IdCliente;
        private string _Nome;
        private string _Descricao;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public FatorRisco()
        {
        }

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public FatorRisco(int Id)
        {
            this.Find(Id);
        }
        public override int Id
        {
            get { return _IdFatorRisco; }
            set { _IdFatorRisco = value; }
        }
        public Cliente IdCliente
        {
            get { return _IdCliente; }
            set { _IdCliente = value; }
        }
        [Obrigatorio(true, "O Nome do Fator de Risco é de preenchimento obrigatório!")]
        public string Nome
        {
            get { return _Nome; }
            set { _Nome = value; }
        }
        public string Descricao
        {
            get { return _Descricao; }
            set { _Descricao = value; }
        }
    }

    [Database("opsa", "FatorRiscoClinico", "IdFatorRiscoClinico")]
    public class FatorRiscoClinico : Table
    {
        private int _IdFatorRiscoClinico;
        private FatorRisco _IdFatorRisco;
        private Clinico _IdClinico;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public FatorRiscoClinico()
        {
        }

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public FatorRiscoClinico(int Id)
        {
            this.Find(Id);
        }
        public override int Id
        {
            get { return _IdFatorRiscoClinico; }
            set { _IdFatorRiscoClinico = value; }
        }
        public FatorRisco IdFatorRisco
        {
            get { return _IdFatorRisco; }
            set { _IdFatorRisco = value; }
        }
        public Clinico IdClinico
        {
            get { return _IdClinico; }
            set { _IdClinico = value; }
        }
    }
}
