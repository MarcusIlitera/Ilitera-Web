using Ilitera.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ilitera.Opsa.Data
{
    #region class ListaEmailsEnviaAta

    [Database("opsa", "ListaEmailsEnviarAta", "IdListaEmailsEnviarAta")]
    public class ListaEmailsEnviarAta : Ilitera.Data.Table
    {
        #region Properties

        private int _idListaEmailsEnivarAta;
        private int _idReuniaoCipa;
        private int _idMembroCipa;
        private int _idEmpregado;
        private string _nome;
        private string _email;

        public override int Id
        {
            get { return _idListaEmailsEnivarAta; }
            set { _idListaEmailsEnivarAta = value; }
        }

        public int IdReuniaoCipa
        {
            get { return _idReuniaoCipa; }
            set { _idReuniaoCipa = value; }
        }

        public int IdMembroCipa
        {
            get { return _idMembroCipa; }
            set { _idMembroCipa = value; }
        }

        public int IdEmpregado
        {
            get { return _idEmpregado; }
            set { _idEmpregado = value;}
        }

        public string Nome
        {
            get { return _nome; }
            set { _nome = value; }
        }

        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }
        #endregion
        #region Construtor

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]
        public ListaEmailsEnviarAta()
        {

        }
        #endregion
    }
    #endregion
}
