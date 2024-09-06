using System;
using System.Collections.Generic;
using System.Text;
using Ilitera.Data;
using Ilitera.Common;

namespace Ilitera.Common
{
    [Database("opsa", "UsuarioGrupo", "IdUsuarioGrupo")]
    public class UsuarioGrupo : Ilitera.Data.Table
    {
        private int _IdUsuarioGrupo;
        private Usuario _IdUsuario;
        private Grupo _IdGrupo;


        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public UsuarioGrupo()
        {

        }
        public override int Id
        {
            get { return _IdUsuarioGrupo; }
            set { _IdUsuarioGrupo = value; }
        }
        public Usuario IdUsuario
        {
            get { return _IdUsuario; }
            set { _IdUsuario = value; }
        }
        public Grupo IdGrupo
        {
            get { return _IdGrupo; }
            set { _IdGrupo = value; }
        }
        public override string ToString()
        {
            if (this.mirrorOld == null)
                this.Find();

            return this.IdGrupo.ToString() + " - " + this.IdUsuario.ToString();
        }
    }
}
