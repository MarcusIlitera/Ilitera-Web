using System;
using Ilitera.Data;
using System.Collections;

namespace Ilitera.Common
{
    [Database("opsa", "Transporte", "IdTransporte", "", "Transporte")]
    public class Transporte : Ilitera.Data.Table
    {

        private int _IdTransporte;
        private string _Descricao = string.Empty;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]
        public Transporte()
        {

        }

        public override int Id
        {
            get { return _IdTransporte; }
            set { _IdTransporte = value; }
        }
        [Obrigatorio(true, "A Descrição é campo obrigatório!")]
        public string Descricao
        {
            get { return _Descricao; }
            set { _Descricao = value; }
        }
        public override int Save()
        {
            return base.Save();
        }
        public override string ToString()
        {
            if (this.mirrorOld == null)
                this.Find();

            return _Descricao;
        }
    }
}