using System;
using Ilitera.Data;

namespace Ilitera.Opsa.Data
{
    [Database("opsa", "Unidade", "IdUnidade")]
    public class Unidade : Ilitera.Data.Table
    {
        public enum Unidades : int
        {
            ppm = 1,
            mg_m3 = 2,
            AsfixianteSimples = 7,
            mR_h = 8
        }

        private int _IdUnidade;
        private string _Descricao = string.Empty;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public Unidade()
        {

        }
        public override int Id
        {
            get { return _IdUnidade; }
            set { _IdUnidade = value; }
        }
        [Obrigatorio(true, "Descrição é campo obrigatório!")]
        public string Descricao
        {
            get { return _Descricao; }
            set { _Descricao = value; }
        }
        public override string ToString()
        {
            if (this.mirrorOld == null)
                this.Find();

            return this.Descricao;
        }
    }
}
