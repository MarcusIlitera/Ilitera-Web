using System;
using System.Collections.Generic;
using System.Text;

using Ilitera.Data;

namespace Ilitera.Common
{
    public enum Categorias : int
    {
        Medico = 1,
        Reunião = 2,
        Pessoal = 3,
        Viagens = 5,
        AvaliacaoAmbiental = 6,
        Auditoria = 7,
        Aniversario = 8,
        Pedido = 10,
        Telemarketing = 11,
        Previsao = 12,
        Suporte = 577027646
    }

    [Database("opsa", "Categoria", "IdCategoria")]
    public class Categoria : Ilitera.Data.Table
    {
        private int _IdCategoria;
        private string _Descricao = string.Empty;
        private string _TextoTarefa = string.Empty;
        private int _Image;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public Categoria()
        {

        }
        public override int Id
        {
            get { return _IdCategoria; }
            set { _IdCategoria = value; }
        }
        [Obrigatorio(true, "Descrição é campo obrigatório!")]
        public string Descricao
        {
            get { return _Descricao; }
            set { _Descricao = value; }
        }
        public string TextoTarefa
        {
            get { return _TextoTarefa; }
            set { _TextoTarefa = value; }
        }
        public int Image
        {
            get { return _Image; }
            set { _Image = value; }
        }
        public override string ToString()
        {
            return this._Descricao;
        }
    }
}
