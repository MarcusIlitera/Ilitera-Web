using System;
using System.Collections.Generic;
using System.Text;
using Ilitera.Data;
using Ilitera.Common;

namespace Ilitera.Common
{
    public enum Grupos : int
    {
        Administracao = -1851832308,
        Diretoria = 62163380,
        Jurídico = -1413798138,
        Medicina = -2107146276,
        Pcmso = 1541206482,
        Producao = 178170190,
        Vendas = -2108334570,
        Telemarketing = 20,
        Telemarketing2 = 21,
        Pericia = 22,
        TI = 26,
        Digitacao = 1262659269
    }

    [Database("opsa", "Grupo", "IdGrupo")]
    public class Grupo : Ilitera.Data.Table
    {
        private int _IdGrupo;
        private string _NomeGrupo = string.Empty;
        private bool _IsMenuWeb;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public Grupo()
        {

        }
        public override int Id
        {
            get { return _IdGrupo; }
            set { _IdGrupo = value; }
        }
        public string NomeGrupo
        {
            get { return _NomeGrupo; }
            set { _NomeGrupo = value; }
        }
        public bool IsMenuWeb
        {
            get { return _IsMenuWeb; }
            set { _IsMenuWeb = value; }
        }
        public override string ToString()
        {
            if (this.mirrorOld == null)
                this.Find();

            return _NomeGrupo;
        }
    }
}
