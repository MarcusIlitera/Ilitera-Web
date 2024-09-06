using System;
using System.Collections.Generic;
using System.Text;
using Ilitera.Data;

namespace Ilitera.Opsa.Data
{
    [Database("opsa", "RegimeRevezamento", "IdRegimeRevezamento", "", "Regime de Revezamento")]
    public class RegimeRevezamento : Ilitera.Data.Table
    {
        private int _IdRegimeRevezamento;
        private string _Descricao = string.Empty;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public RegimeRevezamento()
        {

        }

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public RegimeRevezamento(int Id)
        {
            this.Id = Id;
        }
        public override int Id
        {
            get { return _IdRegimeRevezamento; }
            set { _IdRegimeRevezamento = value; }
        }
        public string Descricao
        {
            get { return _Descricao; }
            set { _Descricao = value; }
        }

        public override string ToString()
        {
            if (this.Id == 0)
                return string.Empty;

            if (this.mirrorOld == null)
                this.Find();

            return _Descricao;
        }
    }
}
