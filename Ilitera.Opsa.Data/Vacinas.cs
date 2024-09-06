using System;
using Ilitera.Data;

namespace Ilitera.Opsa.Data
{
    [Database("opsa", "Vacina", "IdVacina")]
    public class Vacina : Ilitera.Data.Table
    {
        private int _IdVacina;
        private int _IdVacinaDose;
        private int _IdEmpregado;
        private string _Observacao = string.Empty;
        private DateTime _DataVacina;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public Vacina()
        {

        }
        public override int Id
        {
            get { return _IdVacina; }
            set { _IdVacina = value; }
        }

        public int IdVacinaDose
        {
            get { return _IdVacinaDose; }
            set { _IdVacinaDose = value; }
        }
        public int IdEmpregado
        {
            get { return _IdEmpregado; }
            set { _IdEmpregado = value; }
        }
        //[Obrigatorio(true, "Descrição é campo obrigatório!")]
        public string Observacao
        {
            get { return _Observacao; }
            set { _Observacao = value; }
        }
        public DateTime DataVacina
        {
            get { return _DataVacina; }
            set { _DataVacina = value; }
        }

    }


    [Database("opsa", "Vacina_Dose", "IdVacinaDose")]
    public class Vacina_Dose : Ilitera.Data.Table
    {        
        private int _IdVacinaDose;
        private int _IdVacinaTipo;
        private string _Dose = string.Empty;
        private int _IndPeriodicidade;
        private Int16 _Periodicidade;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public Vacina_Dose()
        {

        }
        public override int Id
        {
            get { return _IdVacinaDose; }
            set { _IdVacinaDose = value; }
        }

        public int IdVacinaTipo
        {
            get { return _IdVacinaTipo; }
            set { _IdVacinaTipo = value; }
        }
        [Obrigatorio(true, "Descrição da Dose é campo obrigatório!")]
        public string Dose
        {
            get { return _Dose; }
            set { _Dose = value; }
        }
        public int IndPeriodicidade
        {
            get { return _IndPeriodicidade; }
            set { _IndPeriodicidade = value; }
        }
        public Int16 Periodicidade
        {
            get { return _Periodicidade; }
            set { _Periodicidade = value; }
        }

    }


    [Database("opsa", "Vacina_Tipo", "IdVacinaTipo")]
    public class Vacina_Tipo : Ilitera.Data.Table
    {
        
        private int _IdVacinaTipo;
        private string _VacinaTipo;
        private string _Observacao = string.Empty;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public Vacina_Tipo()
        {

        }
        public override int Id
        {
            get { return _IdVacinaTipo; }
            set { _IdVacinaTipo = value; }
        }

        [Obrigatorio(true, "Descrição da Vacina é campo obrigatório!")]
        public string VacinaTipo
        {
            get { return _VacinaTipo; }
            set { _VacinaTipo = value; }
        }

        public string Observacao
        {
            get { return _Observacao; }
            set { _Observacao = value; }
        }
    }


    [Database("opsa", "Vacina_Setor", "IdVacinaSetor")]
    public class Vacina_Setor : Ilitera.Data.Table
    {
        private int _IdVacinaSetor;
        private int _IdVacinaTipo;
        private int _IdSetor;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public Vacina_Setor()
        {

        }
        public override int Id
        {
            get { return _IdVacinaSetor; }
            set { _IdVacinaSetor = value; }
        }

        public int IdVacinaTipo
        {
            get { return _IdVacinaTipo; }
            set { _IdVacinaTipo = value; }
        }
        public int IdSetor
        {
            get { return _IdSetor; }
            set { _IdSetor = value; }
        }

    }


}
