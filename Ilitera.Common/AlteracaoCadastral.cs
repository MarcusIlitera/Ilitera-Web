using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Ilitera.Data;

namespace Ilitera.Common
{    
    [Database("opsa", "AlteracaoCadastral", "IdAlteracaoCadastral")]

    //[System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.InheritanceDemand, Flags = System.Security.Permissions.SecurityPermissionFlag.UnmanagedCode)]
    //[System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.LinkDemand, Flags = System.Security.Permissions.SecurityPermissionFlag.UnmanagedCode)]


    //[System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.LinkDemand, Name = "FullTrust", Unrestricted = false)]

    
    public class AlteracaoCadastral : Ilitera.Data.Table, IPessoa, IJuridica, IEndereco
    {
        
        private int _IdAlteracaoCadastral;
        private Juridica _IdJuridica;
        private DateTime _DataAlteracao;
        private string _Descricao = string.Empty;
        private string _NomeAbreviado_New = string.Empty;
        private string _NomeAbreviado = string.Empty;
        private string _NomeCompleto_New = string.Empty;
        private string _NomeCompleto = string.Empty;
        private string _NomeCodigo_New = string.Empty;
        private string _NomeCodigo = string.Empty;
        private string _IE_New = string.Empty;
        private string _IE = string.Empty;
        private string _CCM_New = string.Empty;
        private string _CCM = string.Empty;
        private Municipio _IdMunicipio_New;
        private Municipio _IdMunicipio;
        private string _Cep_New;
        private string _Cep;
        private TipoLogradouro _IdTipoLogradouro_New;
        private TipoLogradouro _IdTipoLogradouro;
        private string _Logradouro_New = string.Empty;
        private string _Logradouro = string.Empty;
        private string _Numero_New = string.Empty;
        private string _Numero = string.Empty;
        private string _Complemento_New = string.Empty;
        private string _Complemento = string.Empty;
        private string _Bairro_New = string.Empty;
        private string _Bairro = string.Empty;
        private CNAE _IdCNAE_New;
        private CNAE _IdCNAE;

        //[System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.InheritanceDemand, Flags = System.Security.Permissions.SecurityPermissionFlag.UnmanagedCode)]
        //[System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.LinkDemand, Flags = System.Security.Permissions.SecurityPermissionFlag.UnmanagedCode)]


        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]
        public AlteracaoCadastral()
        {

        }

        
        public override int Id
        {            
            get { return _IdAlteracaoCadastral; }
            set { _IdAlteracaoCadastral = value; }
        }
        public Juridica IdJuridica
        {
            get { return _IdJuridica; }
            set { _IdJuridica = value; }
        }
        public DateTime DataAlteracao
        {
            get { return _DataAlteracao; }
            set { _DataAlteracao = value; }
        }
        public string Descricao
        {
            get { return _Descricao; }
            set { _Descricao = value; }
        }
        public string NomeCodigo_New
        {
            get { return _NomeCodigo_New; }
            set { _NomeCodigo_New = value; }
        }
        public string NomeCodigo
        {
            get { return _NomeCodigo; }
            set { _NomeCodigo = value; }
        }
        public string NomeAbreviado_New
        {
            get { return _NomeAbreviado_New; }
            set { _NomeAbreviado_New = value; }
        }
        public string NomeAbreviado
        {
            get { return _NomeAbreviado; }
            set { _NomeAbreviado = value; }
        }
        public string NomeCompleto_New
        {
            get { return _NomeCompleto_New; }
            set { _NomeCompleto_New = value; }
        }
        public string NomeCompleto
        {
            get { return _NomeCompleto; }
            set { _NomeCompleto = value; }
        }
        public string IE_New
        {
            get { return _IE_New; }
            set { _IE_New = value; }
        }
        public string IE
        {
            get { return _IE; }
            set { _IE = value; }
        }
        public string CCM_New
        {
            get { return _CCM_New; }
            set { _CCM_New = value; }
        }
        public string CCM
        {
            get { return _CCM; }
            set { _CCM = value; }
        }
        public Municipio IdMunicipio_New
        {
            get { return _IdMunicipio_New; }
            set { _IdMunicipio_New = value; }
        }
        public Municipio IdMunicipio
        {
            get { return _IdMunicipio; }
            set { _IdMunicipio = value; }
        }
        public string Cep_New
        {
            get { return _Cep_New; }
            set { _Cep_New = value; }
        }
        public string Cep
        {
            get { return _Cep; }
            set { _Cep = value; }
        }
        public TipoLogradouro IdTipoLogradouro_New
        {
            get { return _IdTipoLogradouro_New; }
            set { _IdTipoLogradouro_New = value; }
        }
        public TipoLogradouro IdTipoLogradouro
        {
            get { return _IdTipoLogradouro; }
            set { _IdTipoLogradouro = value; }
        }
        public string Logradouro_New
        {
            get { return _Logradouro_New; }
            set { _Logradouro_New = value; }
        }
        public string Logradouro
        {
            get { return _Logradouro; }
            set { _Logradouro = value; }
        }
        public string Numero_New
        {
            get { return _Numero_New; }
            set { _Numero_New = value; }
        }
        public string Numero
        {
            get { return _Numero; }
            set { _Numero = value; }
        }
        public string Complemento_New
        {
            get { return _Complemento_New; }
            set { _Complemento_New = value; }
        }
        public string Complemento
        {
            get { return _Complemento; }
            set { _Complemento = value; }
        }
        public string Bairro_New
        {
            get { return _Bairro_New; }
            set { _Bairro_New = value; }
        }
        public string Bairro
        {
            get { return _Bairro; }
            set { _Bairro = value; }
        }
        public CNAE IdCNAE_New
        {
            get { return _IdCNAE_New; }
            set { _IdCNAE_New = value; }
        }
        public CNAE IdCNAE
        {
            get { return _IdCNAE; }
            set { _IdCNAE = value; }
        }

        public override int Save()
        {
            if (this.Id != 0)
                throw new Exception("Alteração Cadastral não pode ser editada!");

            IDbTransaction trans = this.GetTransaction();

            int ret = this.Id;

            try
            {
                this.IdJuridica.Find();

                IPessoa iPessoa = this.IdJuridica;
                iPessoa.NomeAbreviado = this.NomeAbreviado_New;
                iPessoa.NomeCompleto= this.NomeCompleto_New;
                iPessoa.NomeCodigo = this.NomeCodigo_New;

                IJuridica iJuridica = this.IdJuridica;
                iJuridica.CCM = this.CCM_New;
                iJuridica.IdCNAE = this.IdCNAE_New;
                iJuridica.IE= this.IE_New;

                this.IdJuridica.Transaction = trans;
                this.IdJuridica.Save();

                Endereco endereco = this.IdJuridica.GetEndereco();

                IEndereco iEndereco = endereco;
                iEndereco.Bairro = this.Bairro_New;
                iEndereco.Cep = this.Cep_New;
                iEndereco.Complemento = this.Complemento_New;
                iEndereco.IdMunicipio = this.IdMunicipio_New;
                iEndereco.IdTipoLogradouro = this.IdTipoLogradouro_New;
                iEndereco.Logradouro = this.Logradouro_New;
                iEndereco.Numero = this.Numero_New;

                endereco.Transaction = trans;
                endereco.Save();

                ret =  base.Save();

                trans.Commit();

                return ret;
            }
            catch (Exception ex)
            {
                trans.Rollback();

                throw ex;
            }
        }
    }
}
