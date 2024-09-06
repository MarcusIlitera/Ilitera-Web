using System;
using System.Collections.Generic;
using System.Text;

using Ilitera.Data;

namespace Ilitera.Common
{
    [Database("opsa", "Endereco_Historico", "IdEnderecoHistorico")]
    public class Endereco_Historico : Ilitera.Data.Table//, Ilitera.Common.IEndereco
    {
        private int _IdEnderecoHistorico;
        private Int32 _IdPessoa;
        private int _IndTipoEndereco;
        private Int32 _IdMunicipio;
        private string _Municipio = string.Empty;
        private string _Uf = string.Empty;
        private string _Cep = string.Empty;
        private Int32 _IdTipoLogradouro;
        private string _Logradouro = string.Empty;
        private string _Numero = string.Empty;
        private string _Complemento = string.Empty;
        private string _Bairro = string.Empty;
        private string _Observacao = string.Empty;
        private DateTime _DataCadastro = DateTime.Now;
        private string _Mapa = string.Empty;
        private DateTime _Inicio_Vigencia;
        private DateTime _Termino_Vigencia;

        public Endereco_Historico()
        {

        }
        public override int Id
        {
            get { return _IdEnderecoHistorico; }
            set { _IdEnderecoHistorico = value; }
        }
        public Int32 IdPessoa
        {
            get { return _IdPessoa; }
            set { _IdPessoa = value; }
        }
        public int IndTipoEndereco
        {
            get { return _IndTipoEndereco; }
            set { _IndTipoEndereco = value; }
        }
        public Int32 IdMunicipio
        {
            get { return _IdMunicipio; }
            set { _IdMunicipio = value; }
        }
        public string Municipio
        {
            get { return _Municipio; }
            set { _Municipio = value; }
        }
        public string Uf
        {
            get { return _Uf; }
            set { _Uf = value; }
        }
        public string Cep
        {
            get { return _Cep; }
            set { _Cep = value; }
        }
        public Int32 IdTipoLogradouro
        {
            get { return _IdTipoLogradouro; }
            set { _IdTipoLogradouro = value; }
        }
        public string Logradouro
        {
            get { return _Logradouro; }
            set { _Logradouro = value; }
        }
        public string Numero
        {
            get { return _Numero; }
            set { _Numero = value; }
        }
        public string Complemento
        {
            get { return _Complemento; }
            set { _Complemento = value; }
        }
        public string Bairro
        {
            get { return _Bairro; }
            set { _Bairro = value; }
        }
        public string Observacao
        {
            get { return _Observacao; }
            set { _Observacao = value; }
        }
        public DateTime DataCadastro
        {
            get { return _DataCadastro; }
            set { _DataCadastro = value; }
        }
        public string Mapa
        {
            get { return _Mapa; }
            set { _Mapa = value; }
        }
        public DateTime Inicio_Vigencia
        {
            get { return _Inicio_Vigencia; }
            set { _Inicio_Vigencia = value; }
        }
        public DateTime Termino_Vigencia
        {
            get { return _Termino_Vigencia; }
            set { _Termino_Vigencia = value; }
        }
    }




    [Database("opsa", "Identificacao_Historico", "IdIdentificacaoHistorico")]
    public class Identificacao_Historico : Ilitera.Data.Table//, Ilitera.Common.IEndereco
    {
        private int _IdIdentificacaoHistorico;
        private Int32 _IdPessoa;
        private string _NomeCompleto = string.Empty;
        private string _NomeAbreviado = string.Empty;
        private string _CNPJ = string.Empty;
        private DateTime _Inicio_Vigencia;
        private DateTime _Termino_Vigencia;

        public Identificacao_Historico()
        {

        }
        public override int Id
        {
            get { return _IdIdentificacaoHistorico; }
            set { _IdIdentificacaoHistorico = value; }
        }
        public Int32 IdPessoa
        {
            get { return _IdPessoa; }
            set { _IdPessoa = value; }
        }
        public string NomeAbreviado
        {
            get { return _NomeAbreviado; }
            set { _NomeAbreviado = value; }
        }
        public string NomeCompleto
        {
            get { return _NomeCompleto; }
            set { _NomeCompleto = value; }
        }
        public string CNPJ
        {
            get { return _CNPJ; }
            set { _CNPJ = value; }
        }
        public DateTime Inicio_Vigencia
        {
            get { return _Inicio_Vigencia; }
            set { _Inicio_Vigencia = value; }
        }
        public DateTime Termino_Vigencia
        {
            get { return _Termino_Vigencia; }
            set { _Termino_Vigencia = value; }
        }
    }


}
