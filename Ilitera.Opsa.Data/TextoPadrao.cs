using System;
using System.Collections.Generic;
using System.Text;

using Ilitera.Data;

namespace Ilitera.Opsa.Data
{
    [Database("opsa", "TextoPadraoDicionario", "IdTextoPadraoDicionario")]
    public class TextoPadraoDicionario : Ilitera.Data.Table
    {
        private int _IdTextoPadraoDicionario;
        private string _Descricao = string.Empty;
        private TipoAplicacao _IndTipoAplicacao;
        private FormatoTexto _IndFormatoTexto;

        public enum FormatoTexto : int
        {
            Text,
            Rtf,
            Html
        }
        public enum TipoAplicacao : int
        {
            Fax = 0,
            OrdemServicoARO,
            Email,
            Pcmso,
            PPRA,
            Quesitos
        }

        public enum TextoPadraoDicionarios : int
        {
            Fax = 1,
            MedicaControleCaraterAdministrativo = 2,
            MedicaControleCaraterEcucacional = 3
        }

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public TextoPadraoDicionario()
        {

        }
        public override int Id
        {
            get { return _IdTextoPadraoDicionario; }
            set { _IdTextoPadraoDicionario = value; }
        }
        public string Descricao
        {
            get { return _Descricao; }
            set { _Descricao = value; }
        }

        public TipoAplicacao IndTipoAplicacao
        {
            get { return _IndTipoAplicacao; }
            set { _IndTipoAplicacao = value; }
        }
        public FormatoTexto IndFormatoTexto
        {
            get { return _IndFormatoTexto; }
            set { _IndFormatoTexto = value; }
        }
    }

    [Database("opsa", "TextoPadrao", "IdTextoPadrao")]
    public class TextoPadrao : Ilitera.Data.Table
    {
        private int _IdTextoPadrao;
        private TextoPadraoDicionario _IdTextoPadraoDicionario;
        private string _Descricao = string.Empty;
        private string _Titulo = string.Empty;
        private string _Texto = string.Empty;
        private int _Ordem;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public TextoPadrao()
        {

        }
        public override int Id
        {
            get { return _IdTextoPadrao; }
            set { _IdTextoPadrao = value; }
        }
        public TextoPadraoDicionario IdTextoPadraoDicionario
        {
            get { return _IdTextoPadraoDicionario; }
            set { _IdTextoPadraoDicionario = value; }
        }

        public string Descricao
        {
            get { return _Descricao; }
            set { _Descricao = value; }
        }
        public string Titulo
        {
            get { return _Titulo; }
            set { _Titulo = value; }
        }
        public string Texto
        {
            get { return _Texto; }
            set { _Texto = value; }
        }
        public int Ordem
        {
            get { return _Ordem; }
            set { _Ordem = value; }
        }
        public override string ToString()
        {
            return this.Descricao;
        }
        public enum TextoPadroes : int
        {
            Fax = 1,
        }
    }
}
