using System;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;
using Ilitera.Data;
using System.IO;

namespace Ilitera.Common
{
    #region enum Periodicidade

    public enum Periodicidade : int
    {
        Nenhuma,
        Dia,
        Semana,
        Mes,
        Ano,
        Semestral,
        Minuto,
        Hora
    }
    #endregion

    #region IDescricao

    public interface IDescricao
    {
        int Id
        {
            get;
            set;
        }
        string NomeCodigo
        {
            get;
            set;
        }
        string NomeAbreviado
        {
            get;
            set;
        }
        string NomeCompleto
        {
            get;
            set;
        }
    }
    #endregion

    #region Pessoa

    public enum PessoaPapeis : int
    {
        Prestador,
        Empregado,
        IntegranteSESMT,
        Contato
    }

    [Database("opsa", "Pessoa", "IdPessoa")]
    public class Pessoa : Ilitera.Data.Table, Ilitera.Common.IPessoa
    {
        private int _IdPessoa;
        private string _NomeCodigo = string.Empty;
        private string _NomeCompleto = string.Empty;
        private string _NomeAbreviado = string.Empty;
        private short _IndTipoPessoa;
        private string _Email = string.Empty;
        private string _Site = string.Empty;
        private DateTime _DataCadastro = DateTime.Today;
        private string _Foto = string.Empty;
        private bool _IsInativo;
        private string _Origem = string.Empty;

        private Endereco endereco;
        private Municipio municipio;
        private ContatoTelefonico contatoTelefonico1;
        private ContatoTelefonico contatoTelefonico2;
        private ContatoTelefonico contatoTelefonico3;
        private ContatoTelefonico contatoTelefonico4;
        private ContatoTelefonico contatoTelefonico5;
        private ContatoTelefonico fax;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public Pessoa()
        {

        }

        public override int Id
        {
            get { return _IdPessoa; }
            set { _IdPessoa = value; }
        }
        public string NomeCodigo
        {
            get { return _NomeCodigo; }
            set { _NomeCodigo = value; }
        }
        [Obrigatorio(true, "O nome abreviado é obrigatório!")]
        public string NomeAbreviado
        {
            get { return _NomeAbreviado; }
            set     { _NomeAbreviado = value; }
        }

        [Obrigatorio(true, "O nome completo é obrigatório!")]
        public string NomeCompleto
        {
            get { return _NomeCompleto; }
            set { _NomeCompleto = value; }
        }
        public enum TipoPessoa : short
        {
            JuridicaPessoa,
            Juridica,
            Fisica,
        }
        public short IndTipoPessoa
        {
            get { return _IndTipoPessoa; }
            set { _IndTipoPessoa = value; }
        }
        public string Email
        {
            get { return _Email; }
            set { _Email = value; }
        }
        public string Site
        {
            get { return _Site; }
            set { _Site = value; }
        }
        public DateTime DataCadastro
        {
            get { return _DataCadastro; }
            set { _DataCadastro = value; }
        }
        public string Foto
        {
            get { return _Foto; }
            set { _Foto = value; }
        }
        public bool IsInativo
        {
            get { return _IsInativo; }
            set { _IsInativo = value; }
        }
        public string Origem
        {
            get { return _Origem; }
            set { _Origem = value; }
        }


        public Endereco GetEndereco()
        {
            if (endereco != null)
                return endereco;

            return this.GetEndereco(TipoEndereco.Default);
        }

        public Endereco GetEndereco(TipoEndereco tipoEndereco)
        {
            endereco = new Endereco();
            endereco.Find("IdPessoa=" + this.Id
                + " AND IndTipoEndereco=" + (int)tipoEndereco);

            if (endereco.Id == 0)
            {
                endereco.Inicialize();
                endereco.IdPessoa.Id = this.Id;
                endereco.IndTipoEndereco = (int)tipoEndereco;
                endereco.IdMunicipio.Id = 98;
            }
            return endereco;
        }

        public Municipio GetMunicipio()
        {
            if (municipio == null)
            {
                endereco = GetEndereco();
                endereco.IdMunicipio.Find();
                municipio = endereco.IdMunicipio;
            }
            return municipio;
        }

        public ContatoTelefonico GetContatoTelefonico()
        {
            if (this.mirrorOld == null)
                this.Find();

            if (contatoTelefonico1 == null)
            {
                contatoTelefonico1 = new ContatoTelefonico();
                contatoTelefonico1.Find("IdPessoa=" + this.Id
                                    + " AND IndTipoTelefone=" + (short)Ilitera.Common.ContatoTelefonico.TipoTelefone.Comercial);

                if (contatoTelefonico1.Id == 0)
                {
                    contatoTelefonico1.Inicialize();
                    contatoTelefonico1.IndTipoTelefone = (short)Ilitera.Common.ContatoTelefonico.TipoTelefone.Comercial;
                }
            }
            return contatoTelefonico1;
        }

        public ContatoTelefonico GetContatoTelefonico2()
        {
            if (contatoTelefonico2 == null)
            {
                contatoTelefonico2 = new ContatoTelefonico();
                contatoTelefonico2.Find("IdPessoa=" + this.Id + " AND IndTipoTelefone=" + (short)Ilitera.Common.ContatoTelefonico.TipoTelefone.Comercial2);
                if (contatoTelefonico2.Id == 0)
                {
                    contatoTelefonico2.Inicialize();
                    contatoTelefonico2.IndTipoTelefone = (short)Ilitera.Common.ContatoTelefonico.TipoTelefone.Comercial2;
                }
            }
            return contatoTelefonico2;
        }

        public ContatoTelefonico GetContatoTelefonico3()
        {
            if (contatoTelefonico3 == null)
            {
                contatoTelefonico3 = new ContatoTelefonico();
                contatoTelefonico3.Find("IdPessoa=" + this.Id + " AND IndTipoTelefone=" + (short)Ilitera.Common.ContatoTelefonico.TipoTelefone.Comercial3);
                if (contatoTelefonico3.Id == 0)
                {
                    contatoTelefonico3.Inicialize();
                    contatoTelefonico3.IndTipoTelefone = (short)Ilitera.Common.ContatoTelefonico.TipoTelefone.Comercial2;
                }
            }
            return contatoTelefonico3;
        }

        public ContatoTelefonico GetContatoTelefonico4()
        {
            if (contatoTelefonico4 == null)
            {
                contatoTelefonico4 = new ContatoTelefonico();
                contatoTelefonico4.Find("IdPessoa=" + this.Id + " AND IndTipoTelefone=" + (short)Ilitera.Common.ContatoTelefonico.TipoTelefone.Comercial4);
                if (contatoTelefonico4.Id == 0)
                {
                    contatoTelefonico4.Inicialize();
                    contatoTelefonico4.IndTipoTelefone = (short)Ilitera.Common.ContatoTelefonico.TipoTelefone.Comercial4;
                }
            }
            return contatoTelefonico4;
        }

        public ContatoTelefonico GetContatoTelefonico5()
        {
            if (contatoTelefonico5 == null)
            {
                contatoTelefonico5 = new ContatoTelefonico();
                contatoTelefonico5.Find("IdPessoa=" + this.Id + " AND IndTipoTelefone=" + (short)Ilitera.Common.ContatoTelefonico.TipoTelefone.Comercial5);
                if (contatoTelefonico5.Id == 0)
                {
                    contatoTelefonico5.Inicialize();
                    contatoTelefonico5.IndTipoTelefone = (short)Ilitera.Common.ContatoTelefonico.TipoTelefone.Comercial5;
                }
            }
            return contatoTelefonico5;
        }

        public ContatoTelefonico GetFax()
        {
            fax = new ContatoTelefonico();
            fax.Find("IdPessoa=" + this.Id + " AND IndTipoTelefone=" + (short)Ilitera.Common.ContatoTelefonico.TipoTelefone.Fax);
            if (fax.Id == 0)
            {
                fax.Inicialize();
                fax.IndTipoTelefone = (short)Ilitera.Common.ContatoTelefonico.TipoTelefone.Fax;
            }
            return fax;
        }

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public System.Drawing.Bitmap GetFoto()
        {
            System.Drawing.Bitmap ret = null;
            if (this.Foto != string.Empty)
            {
                ret = Fotos.GetImageFoto(this.Foto);
            }
            return ret;
        }

        public override string ToString()
        {
            if (this.mirrorOld == null)
                this.Find();

            return _NomeAbreviado;
        }

        public Juridica GetJuridica()
        {
            Juridica juridica = new Juridica();

            if (this.mirrorOld == null)
                this.Find();

            if (this.Id != 0 && this.IndTipoPessoa != (int)Pessoa.TipoPessoa.Juridica)
            {
                JuridicaPessoa juridicaPessoa = new JuridicaPessoa();
                juridicaPessoa.Find("IdPessoa=" + this.Id);

                if (juridicaPessoa.Id != 0)
                    juridica.Find(juridicaPessoa.IdJuridica.Id);
            }
            else
                juridica.Find(this.Id);

            return juridica;
        }

        /// <summary>
        /// Veririca se um C.P.F. é válido.
        /// </summary>
        /// <param name="cpf">Número do C.P.F. que será validado.</param>
        /// <returns>string</returns>
        public static bool VeriricaCPF(string cpf)
        {
            int d1, d2;
            int soma = 0;
            string digitado = "";
            string calculado = "";

            // Pesos para calcular o primeiro digito
            int[] peso1 = new int[] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            // Pesos para calcular o segundo digito
            int[] peso2 = new int[] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] n = new int[11];

            // Se o tamanho for < 11 entao retorna como inválido
            if (cpf.Length != 11)
                return false;

            // Caso coloque todos os numeros iguais
            switch (cpf)
            {
                case "11111111111":
                    return false;
                case "00000000000":
                    return false;
                case "2222222222":
                    return false;
                case "33333333333":
                    return false;
                case "44444444444":
                    return false;
                case "55555555555":
                    return false;
                case "66666666666":
                    return false;
                case "77777777777":
                    return false;
                case "88888888888":
                    return false;
                case "99999999999":
                    return false;
            }

            try
            {
                // Quebra cada digito do CPF
                n[0] = Convert.ToInt32(cpf.Substring(0, 1));
                n[1] = Convert.ToInt32(cpf.Substring(1, 1));
                n[2] = Convert.ToInt32(cpf.Substring(2, 1));
                n[3] = Convert.ToInt32(cpf.Substring(3, 1));
                n[4] = Convert.ToInt32(cpf.Substring(4, 1));
                n[5] = Convert.ToInt32(cpf.Substring(5, 1));
                n[6] = Convert.ToInt32(cpf.Substring(6, 1));
                n[7] = Convert.ToInt32(cpf.Substring(7, 1));
                n[8] = Convert.ToInt32(cpf.Substring(8, 1));
                n[9] = Convert.ToInt32(cpf.Substring(9, 1));
                n[10] = Convert.ToInt32(cpf.Substring(10, 1));
            }
            catch
            {
                return false;
            }

            // Calcula cada digito com seu respectivo peso
            for (int i = 0; i <= peso1.GetUpperBound(0); i++)
                soma += (peso1[i] * Convert.ToInt32(n[i]));

            // Pega o resto da divisao
            int resto = soma % 11;

            if (resto == 1 || resto == 0)
                d1 = 0;
            else
                d1 = 11 - resto;

            soma = 0;

            // Calcula cada digito com seu respectivo peso
            for (int i = 0; i <= peso2.GetUpperBound(0); i++)
                soma += (peso2[i] * Convert.ToInt32(n[i]));

            // Pega o resto da divisao
            resto = soma % 11;
            if (resto == 1 || resto == 0)
                d2 = 0;
            else
                d2 = 11 - resto;

            calculado = d1.ToString() + d2.ToString();
            digitado = n[9].ToString() + n[10].ToString();

            // Se os ultimos dois digitos calculados bater com
            // os dois ultimos digitos do cpf entao é válido
            if (calculado == digitado)
                return (true);
            else
                return (false);
        }


        public string GetNomeContato()
        {
            string ret;

            if (this.Id != 0 && this.NomeAbreviado == string.Empty)
                this.Find();

            if (this.Id == 0)
                ret = string.Empty;
            else if (this.IndTipoPessoa == (int)Pessoa.TipoPessoa.Juridica)
                ret = this.NomeAbreviado;
            else
            {
                JuridicaPessoa juridicaPessoa = new JuridicaPessoa();
                juridicaPessoa.Find("IdPessoa=" + this.Id);
                juridicaPessoa.IdJuridica.Find();
                ret = juridicaPessoa.IdJuridica.NomeAbreviado + " / " + this.NomeAbreviado;
            }
            return ret;
        }

        public Prestador GetPrestador()
        {
            Prestador prestador;
            if (this.Id == 0)
                prestador = new Prestador();
            else if (this.IndTipoPessoa == (int)Pessoa.TipoPessoa.Juridica)
                prestador = null;
            else
            {
                prestador = new Prestador();
                prestador.Find("IdPessoa=" + this.Id);
            }
            return prestador;
        }

        public string GetTituloPrestador()
        {
            string ret;
            if (this.Id != 0 && this.NomeAbreviado == string.Empty)
                this.Find();
            if (this.Id == 0)
                ret = string.Empty;
            else if (this.IndTipoPessoa == (int)Pessoa.TipoPessoa.Juridica)
                ret = string.Empty;
            else
            {
                Prestador prestador = new Prestador();
                prestador.Find("IdPessoa=" + this.Id);
                ret = prestador.Titulo;
            }
            return ret;
        }

        public string GetNomePrestador()
        {
            string ret;
            if (this.Id != 0 && this.NomeAbreviado == string.Empty)
                this.Find();
            if (this.Id == 0)
                ret = string.Empty;
            else if (this.IndTipoPessoa == (int)Pessoa.TipoPessoa.Juridica)
                ret = string.Empty;
            else
                ret = this.NomeAbreviado;
            return ret;
        }

        public string GetNomeEmpresa()
        {
            string ret;
            if (this.Id != 0 && this.NomeAbreviado == string.Empty)
                this.Find();

            if (this.Id == 0)
                ret = string.Empty;
            else if (this.IndTipoPessoa == (int)Pessoa.TipoPessoa.Juridica)
                ret = this.NomeAbreviado;
            else
            {
                JuridicaPessoa juridicaPessoa = new JuridicaPessoa();
                juridicaPessoa.Find("IdPessoa=" + this.Id);
                juridicaPessoa.IdJuridica.Find();
                ret = juridicaPessoa.IdJuridica.NomeAbreviado;
            }
            return ret;
        }

        private static bool ValidarCNPJ(string cnpj)
        {
            cnpj = cnpj.Replace(".", string.Empty);
            cnpj = cnpj.Replace("-", string.Empty);
            cnpj = cnpj.Replace("/", string.Empty);

            if (cnpj.Length != 14)
                return false;

            // Caso coloque todos os numeros iguais
            switch (cnpj)
            {
                case "11111111111111":
                    return false;
                case "00000000000000":
                    return false;
                case "22222222222222":
                    return false;
                case "33333333333333":
                    return false;
                case "44444444444444":
                    return false;
                case "55555555555555":
                    return false;
                case "66666666666666":
                    return false;
                case "77777777777777":
                    return false;
                case "88888888888888":
                    return false;
                case "99999999999999":
                    return false;
            }

            string l, inx, dig;
            int s1, s2, i, d1, d2, v, m1, m2;
            inx = cnpj.Substring(12, 2);
            cnpj = cnpj.Substring(0, 12);
            s1 = 0;
            s2 = 0;
            m2 = 2;
            for (i = 11; i >= 0; i--)
            {
                l = cnpj.Substring(i, 1);
                v = Convert.ToInt16(l);
                m1 = m2;
                m2 = m2 < 9 ? m2 + 1 : 2;
                s1 += v * m1;
                s2 += v * m2;
            }
            s1 %= 11;
            d1 = s1 < 2 ? 0 : 11 - s1;
            s2 = (s2 + 2 * d1) % 11;
            d2 = s2 < 2 ? 0 : 11 - s2;
            dig = d1.ToString() + d2.ToString();

            return (inx == dig);
        }

        protected static bool IsCNPJ(string nomeCodigo)
        {
            string padrao = @"\d{2}.\d{3}.\d{3}/\d{4}-\d{2}";

            Regex re = new Regex(padrao);

            return re.IsMatch(nomeCodigo);
        }

        public override void Validate()
        {
            //if (this.Email != string.Empty && !Ilitera.Common.EmailBase.IsEmail(this.Email))
            //    throw new Exception("Email Inválido!");

            if (this.IndTipoPessoa == (int)TipoPessoa.Juridica
                && this.NomeCodigo != string.Empty
                && IsCNPJ(this.NomeCodigo)
                && !ValidarCNPJ(this.NomeCodigo))
                throw new Exception("CNPJ Inválido!");

            base.Validate();
        }

        public override int Save()
        {
            IDbTransaction trans = this.GetTransaction();

            try
            {
                this.Transaction = trans;

                this.Email = this.Email.ToLower();

                base.Save();

                if (this.endereco != null)
                {
                    this.endereco.IdPessoa.Id = this.Id;
                    this.endereco.Transaction = trans;
                    this.endereco.Save();
                }

                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                throw ex;
            }

            return this.Id;
        }
    }

    #endregion

    #region  GrupoEmpresa

    [Database("opsa", "GrupoEmpresa", "IdGrupoEmpresa", "", "Grupo Empresa")]

    public class GrupoEmpresa : Ilitera.Data.Table
    {
        private int _IdGrupoEmpresa;
        private string _Descricao = string.Empty;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]
        public GrupoEmpresa()
        {

        }

        public override int Id
        {
            get { return _IdGrupoEmpresa; }
            set { _IdGrupoEmpresa = value; }
        }
        [Obrigatorio(true, "A descrição é campo obrigatório!")]
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
    #endregion

    #region JuridicaPapel

    public enum IndJuridicaPapel : short
    {
        Cliente = 1,
        ClienteInativo_bak,
        AposentEspecial,
        Prospeccao,
        Sindicato,
        Empresa,
        DRT,
        Clinica,
        Agencia,
        Prospects2,
        Diversos,
        Estagiario,
        Particular,
        Prospeccao3,
        FornecedorEPI,
        FabricanteExtintor,
        Tomadora,
        Obras,
        TerceiroAutonomo,
        Filial_bak,
        ClinicaOutras
    }


    [Database("opsa", "JuridicaPapel", "IdJuridicaPapel")]
    public class JuridicaPapel : Ilitera.Data.Table
    {
        private int _IdJuridicaPapel;
        private string _Descricao = string.Empty;
        private bool _IsInativo;
        private bool _IsLocalTrabalho;
        private bool _IsVisualizar = true;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public JuridicaPapel()
        {

        }
        public override int Id
        {
            get { return _IdJuridicaPapel; }
            set { _IdJuridicaPapel = value; }
        }
        [Obrigatorio(true, "A descrição é campo obrigatório!")]
        public string Descricao
        {
            get { return _Descricao; }
            set { _Descricao = value; }
        }
        public bool IsInativo
        {
            get { return _IsInativo; }
            set { _IsInativo = value; }
        }
        public bool IsLocalTrabalho
        {
            get { return _IsLocalTrabalho; }
            set { _IsLocalTrabalho = value; }
        }
        public bool IsVisualizar
        {
            get { return _IsVisualizar; }
            set { _IsVisualizar = value; }
        }
        public override string ToString()
        {
            if (this.mirrorOld == null)
                this.Find();

            return _Descricao;
        }

        public bool IsCliente()
        {
            return IsCliente(this.Id);
        }

        public static bool IsCliente(int IdJuridicaPapel)
        {
            return (IdJuridicaPapel == (int)IndJuridicaPapel.Cliente
                    || IdJuridicaPapel == (int)IndJuridicaPapel.AposentEspecial
                    || IdJuridicaPapel == (int)IndJuridicaPapel.Tomadora
                    || IdJuridicaPapel == (int)IndJuridicaPapel.Obras
                    || IdJuridicaPapel == (int)IndJuridicaPapel.TerceiroAutonomo);
        }

        public bool IsClinica()
        {
            return IsClinica(this.Id);
        }

        public static bool IsClinica(int IdJuridicaPapel)
        {
            return (IdJuridicaPapel == (int)IndJuridicaPapel.Clinica
                    || IdJuridicaPapel == (int)IndJuridicaPapel.ClinicaOutras);
        }

        public bool IsLocalDeTrabalho()
        {
            return IsLocalDeTrabalho(this.Id);
        }

        public static bool IsLocalDeTrabalho(int IdJuridicaPapel)
        {
            return (IdJuridicaPapel == (int)IndJuridicaPapel.Tomadora
                || IdJuridicaPapel == (int)IndJuridicaPapel.Obras);
        }

        public bool IsProspeccao()
        {
            return IsProspeccao(this.Id);
        }

        public static bool IsProspeccao(int IdJuridicaPapel)
        {
            return (IdJuridicaPapel == (int)IndJuridicaPapel.Prospeccao
                || IdJuridicaPapel == (int)IndJuridicaPapel.Prospeccao3
                || IdJuridicaPapel == (int)IndJuridicaPapel.Prospects2);
        }

        public bool IsMesmoTipo(int IdJuridicaPapel)
        {
            bool bVal;

            if (this.IsCliente() && IsCliente(IdJuridicaPapel))
                bVal = true;
            else if (this.IsClinica() && IsClinica(IdJuridicaPapel))
                bVal = true;
            else if (this.IsProspeccao() && IsProspeccao(IdJuridicaPapel))
                bVal = true;
            else
                bVal = this.Id == IdJuridicaPapel;

            return bVal;
        }

        public string GetDescricaoLocaldeTrabalho()
        {
            return GetDescricaoLocaldeTrabalho(this.Id);

        }

        public static string GetDescricaoLocaldeTrabalho(int IdJuridicaPapel)
        {
            string ret = string.Empty;

            if (IdJuridicaPapel == (int)IndJuridicaPapel.Tomadora)
                ret = "Contratante - Cessão de Mão de Obra";
            else if (IdJuridicaPapel == (int)IndJuridicaPapel.Obras)
                ret = "Unidade de Serviço";
            //else if (IdJuridicaPapel == (int)IndJuridicaPapel.Filial)
            //    ret = "Local do Levantamento";
            else if (IdJuridicaPapel == (int)IndJuridicaPapel.TerceiroAutonomo)
                ret = "Empresa";

            return ret;
        }

        public static string GetDescricaoLocaldeTrabalho(int IdJuridicaPapel, string xUnidade)
        {
            string ret = string.Empty;

            string xUS = string.Empty;

            if (IdJuridicaPapel == (int)IndJuridicaPapel.Tomadora)
                ret = "Contratante - Cessão de Mão de Obra";
            else if (IdJuridicaPapel == (int)IndJuridicaPapel.Obras)
            {
                xUS = xUnidade.Substring(xUnidade.LastIndexOf("-") + 1);

                ret = "Unidade de Serviço : " + xUS;
            }
            //else if (IdJuridicaPapel == (int)IndJuridicaPapel.Filial)
            //    ret = "Local do Levantamento";
            else if (IdJuridicaPapel == (int)IndJuridicaPapel.TerceiroAutonomo)
                ret = "Empresa";

            return ret;
        }

    }
    #endregion

    #region Fisica

    public enum FisicaPapel : int
    {
        Empregado,
        TestemunhaCAT,        
        MembroCipa,
        PrestadorMestra,
        Perito,
        PrestadorCliente,
        Advogado
    }

    [Database("opsa", "Fisica", "IdFisica")]
    public class Fisica : Ilitera.Common.Pessoa
    {
        private int _IdFisica;
        private FisicaPapel _IndFisicaPapel;
        private string _Identidade = string.Empty;
        private string _CPF = string.Empty;
        private DateTime _DataNascimento;
        private string _Sexo = string.Empty;
        private float _Peso;
        private float _Altura;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public Fisica()
        {

        }
        public override void Inicialize()
        {
            base.Inicialize();

            base.IndTipoPessoa = (short)Pessoa.TipoPessoa.Fisica;
        }
        public override int Id
        {
            get { return _IdFisica; }
            set { _IdFisica = value; }
        }

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public Fisica(int Id)
        {
            this.Find(Id);
        }
        public FisicaPapel IndFisicaPapel
        {
            get { return _IndFisicaPapel; }
            set { _IndFisicaPapel = value; }
        }
        public string Identidade
        {
            get { return _Identidade; }
            set { _Identidade = value; }
        }
        public string CPF
        {
            get { return _CPF; }
            set { _CPF = value; }
        }
        public DateTime DataNascimento
        {
            get { return _DataNascimento; }
            set { _DataNascimento = value; }
        }
        public string Sexo
        {
            get { return _Sexo; }
            set
            {
                if (value.Length > 1)
                    value = value.Substring(0, 1);

                if (value == "M" || value == "F" || value == "S" || value.Trim() == string.Empty)
                    _Sexo = value;
                else
                    throw new Exception("O sexo só pode ser Masculino ou Feminino");
            }
        }
        public float Peso
        {
            get { return _Peso; }
            set { _Peso = value; }
        }
        public float Altura
        {
            get { return _Altura; }
            set { _Altura = value; }
        }
    }
    #endregion

    #region Endereco

    public enum TipoEndereco : short
    {
        Default,
        Cobranca,
        Comercial,
        Residencial,
        Entrega
    }

    [Database("opsa", "Endereco", "IdEndereco")]
    public class Endereco : Ilitera.Data.Table, Ilitera.Common.IEndereco
    {
        private int _IdEndereco;
        private Pessoa _IdPessoa;
        private int _IndTipoEndereco;
        private Municipio _IdMunicipio;
        private string _Municipio = string.Empty;
        private string _Uf = string.Empty;
        private string _Cep = string.Empty;
        private TipoLogradouro _IdTipoLogradouro;
        private string _Logradouro = string.Empty;
        private string _Numero = string.Empty;
        private string _Complemento = string.Empty;
        private string _Bairro = string.Empty;
        private string _Observacao = string.Empty;
        private DateTime _DataCadastro = DateTime.Now;
        private string _Mapa = string.Empty;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public Endereco()
        {

        }
        public override int Id
        {
            get { return _IdEndereco; }
            set { _IdEndereco = value; }
        }
        public Pessoa IdPessoa
        {
            get { return _IdPessoa; }
            set { _IdPessoa = value; }
        }
        public int IndTipoEndereco
        {
            get { return _IndTipoEndereco; }
            set { _IndTipoEndereco = value; }
        }
        public Municipio IdMunicipio
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
        public TipoLogradouro IdTipoLogradouro
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

        public string GetPathMapa()
        {
            if (!this.Mapa.Equals(string.Empty))
                return Path.Combine(Fotos.GetRaizPath(), @"+MapaLocalizacao\" + this.Mapa);
            else
                return string.Empty;
        }

        public bool IsCEP()
        {
            string padrao = @"\d{5}-\d{3}";

            Regex re = new Regex(padrao);

            return re.IsMatch(this.Cep);
        }

        private string FormatarCEP()
        {
            string ret;

            if (this.Cep != string.Empty
                && !this.IsCEP())
            {
                string padrao = @"^(\d{5})(\d{3})";

                string cep = Utility.RemoveCaracteresEspeciais(this.Cep);

                if (cep.Length == 5)
                    cep = cep + "000";
                else if (cep.Length == 7)
                    cep = "0" + cep;
                else if (cep.Length != 8)
                    throw new Exception("CEP Inválido!");

                Regex re = new Regex(padrao);

                Match m = re.Match(cep);

                if (m.Success)
                    ret = m.Groups[1].Value + "-" + m.Groups[2].Value;
                else
                    throw new Exception("CEP Inválido!");
            }
            else
                ret = this.Cep;

            return ret;
        }

        public string GetEnderecoCompleto()
        {
            if (this.Id == 0)
                return string.Empty;

            return GetEnderecoCompleto(this);
        }

        public static string GetEnderecoCompleto(IEndereco iEndereco)
        {
            string str = Endereco.GetEndereco(iEndereco);

            if (str == string.Empty)
                return str;
            else
                return str + " " + Endereco.GetCidade(iEndereco) + " - " + Endereco.GetEstado(iEndereco) + " " + iEndereco.Cep;
        }

        public string GetEnderecoCompletoPorLinha()
        {
            if (this.Id == 0)
                return string.Empty;

            string str = this.GetEndereco();

            if (str == string.Empty)
                return str;
            else
                return str + " \n" + this.GetCidade() + " - " + this.GetEstado() + "\nCEP " + this.Cep + "       " + this.Observacao;
        }

        public string GetEnderecoCompletoPorLinhaSemCep()
        {
            if (this.Id == 0)
                return string.Empty;

            string str = this.GetEndereco();

            if (str == string.Empty)
                return str;
            else
                return str + " \n" + this.GetCidade() + " - " + this.GetEstado();
        }

        public string GetEndereco()
        {
            if (this.Id == 0)
                return string.Empty;

            return GetEndereco(this);
        }

        public static string GetEndereco(IEndereco iEndereco)
        {
            StringBuilder str = new StringBuilder();

            if (iEndereco.IdTipoLogradouro.Id != 0)
            {
                if (iEndereco.IdTipoLogradouro.mirrorOld == null)
                    iEndereco.IdTipoLogradouro.Find();

                str.Append(iEndereco.IdTipoLogradouro.NomeAbreviado);
                str.Append(" " + iEndereco.Logradouro);
            }
            else
                str.Append(iEndereco.Logradouro);

            if (iEndereco.Numero != string.Empty)
                str.Append(" " + iEndereco.Numero);

            if (iEndereco.Complemento != string.Empty && iEndereco.Complemento.ToUpper() != "NULL")
                str.Append(" " + iEndereco.Complemento);

            return str.ToString();
        }

        public static string GetEndereco(string tipoLogradouro,
                                        string logradouro,
                                        string numero,
                                        string complemento)
        {
            StringBuilder str = new StringBuilder();

            if (tipoLogradouro != string.Empty)
            {
                str.Append(tipoLogradouro);
                str.Append(" " + logradouro);
            }
            else
                str.Append(logradouro);

            if (numero != string.Empty)
                str.Append(" " + numero);

            if (complemento != string.Empty && complemento.ToUpper() != "NULL")
                str.Append(" " + complemento);

            return str.ToString();
        }

        public string GetCidadeEstado()
        {
            return this.GetCidade() + " - " + this.GetEstado();
        }

        public string GetCidade()
        {
            return GetCidade(this);
        }

        public static string GetCidade(IEndereco iEndereco)
        {
            string ret = string.Empty;

            if (iEndereco.IdMunicipio != null)
            {
                if (iEndereco.IdMunicipio.mirrorOld == null)
                    iEndereco.IdMunicipio.Find();

                ret = iEndereco.IdMunicipio.NomeCompleto;
            }
            return ret;
        }

        public string GetEstado()
        {
            return GetEstado(this);
        }

        public static string GetEstado(IEndereco iEndereco)
        {
            string ret = string.Empty;

            if (iEndereco.IdMunicipio != null)
                if (iEndereco.IdMunicipio.mirrorOld == null)
                    iEndereco.IdMunicipio.Find();

            if (iEndereco.IdMunicipio.IdUnidadeFederativa != null)
            {
                if (iEndereco.IdMunicipio.IdUnidadeFederativa.mirrorOld == null)
                    iEndereco.IdMunicipio.IdUnidadeFederativa.Find();

                ret = iEndereco.IdMunicipio.IdUnidadeFederativa.NomeAbreviado;
            }

            return ret;
        }

        public string GetCepBairro()
        {
            if (this.Bairro == string.Empty)
                return "CEP " + this.Cep;
            else
                return "CEP " + this.Cep + " " + this.Bairro;
        }

        public string GetTipoEndereco()
        {
            string ret;
            if (this.IndTipoEndereco == (int)TipoEndereco.Default)
                ret = "Padrão";
            else if (this.IndTipoEndereco == (int)TipoEndereco.Cobranca)
                ret = "de Cobrança";
            else if (this.IndTipoEndereco == (int)TipoEndereco.Entrega)
                ret = "de Entrega";
            else if (this.IndTipoEndereco == (int)TipoEndereco.Residencial)
                ret = "Residencial";
            else if (this.IndTipoEndereco == (int)TipoEndereco.Comercial)
                ret = "Comercial";
            else
                ret = string.Empty;
            return ret;
        }

        public override int Save()
        {
            if (this.IdMunicipio.Id != 0)
            {
                this.Municipio = GetCidade();
                this.Uf = GetEstado();
            }

            this.Cep = this.FormatarCEP();

            return base.Save();
        }
    }
    #endregion

    #region Regiao

    [Database("opsa", "FaixaCEP", "IdFaixaCEP")]
    public class FaixaCEP : Ilitera.Data.Table
    {
        private int _IdFaixaCEP;
        private string _NomeAbreviado = string.Empty;
        private string _InicioCEP = string.Empty;
        private string _TerminoCEP = string.Empty;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public FaixaCEP()
        {

        }
        public override int Id
        {
            get { return _IdFaixaCEP; }
            set { _IdFaixaCEP = value; }
        }
        [Obrigatorio(true, "O Nome Abreviado é campo obrigatório!")]
        public string NomeAbreviado
        {
            get { return _NomeAbreviado; }
            set { _NomeAbreviado = value; }
        }
        [Obrigatorio(true, "O CEP inicio é campo obrigatório!")]
        public string InicioCEP
        {
            get { return _InicioCEP; }
            set { _InicioCEP = value; }
        }
        [Obrigatorio(true, "O CEP término é campo obrigatório!")]
        public string TerminoCEP
        {
            get { return _TerminoCEP; }
            set { _TerminoCEP = value; }
        }

        public override string ToString()
        {
            return this.NomeAbreviado;
        }
    }
    #endregion

    #region LocalizacaoGeografica

    public enum TipoLocalizacaoGeografica : short
    {
        GrupoEconomico,
        Pais,
        UnidadeFederativa,
        Municipio
    }

    [Database("opsa", "LocalizacaoGeografica", "IdLocalizacaoGeografica")]
    public class LocalizacaoGeografica : Ilitera.Data.Table
    {
        private int _IdLocalizacaoGeografica;
        private string _NomeAbreviado = string.Empty;
        private string _NomeCompleto = string.Empty;
        private short _IndTipoLocalizacaoGeografica;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public LocalizacaoGeografica()
        {

        }
        public override int Id
        {
            get { return _IdLocalizacaoGeografica; }
            set { _IdLocalizacaoGeografica = value; }
        }
        [Obrigatorio(true, "O Nome Abreviado é campo obrigatório!")]
        public string NomeAbreviado
        {
            get { return _NomeAbreviado; }
            set { _NomeAbreviado = value; }
        }
        [Obrigatorio(true, "O Nome Completo é campo obrigatório!")]
        public string NomeCompleto
        {
            get { return _NomeCompleto; }
            set { _NomeCompleto = value; }
        }
        public short IndTipoLocalizacaoGeografica
        {
            get { return _IndTipoLocalizacaoGeografica; }
            set { _IndTipoLocalizacaoGeografica = value; }
        }
        public override string ToString()
        {
            return this.NomeCompleto;
        }
    }
    
    public enum Municipios : int
    {
        SaoPaulo = 98
    }

    [Database("opsa", "Municipio", "IdMunicipio")]
    public class Municipio : Ilitera.Common.LocalizacaoGeografica
    {
        private int _IdMunicipio;
        private UnidadeFederativa _IdUnidadeFederativa;
        private int _Cod_Municipio_Completo;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public Municipio()
        {
            this.IndTipoLocalizacaoGeografica = (short)TipoLocalizacaoGeografica.Municipio;
        }
        public override int Id
        {
            get { return _IdMunicipio; }
            set { _IdMunicipio = value; }
        }
        public int Cod_Municipio_Completo
        {
            get { return _Cod_Municipio_Completo; }
            set { _Cod_Municipio_Completo = value; }
        }
        [Obrigatorio(true, "Unidade Federativa é campo obrigatório!")]
        public UnidadeFederativa IdUnidadeFederativa
        {
            get { return _IdUnidadeFederativa; }
            set { _IdUnidadeFederativa = value; }
        }

        public string GetUnidadeFederetiva()
        {
            if (this.IdUnidadeFederativa == null)
                this.IdUnidadeFederativa = new UnidadeFederativa();
            else
                this.Find();

            if (this.IdUnidadeFederativa.mirrorOld == null)
                this.IdUnidadeFederativa.Find();

            return this.IdUnidadeFederativa.NomeAbreviado;
        }

        public override string ToString()
        {
            if (this.mirrorOld == null)
                this.Find();

            return NomeCompleto;
        }

        public static Municipio GetMunicipio(string strUF, string strMunicipio)
        {
            if (strMunicipio == string.Empty || strUF == string.Empty || strUF.Length != 2)
                return new Municipio();

            UnidadeFederativa uf = new UnidadeFederativa();
            uf.Find("UPPER(NomeAbreviado)='" + strUF.ToUpper() + "'");

            ArrayList listMunicipios = new Municipio().Find("IdUnidadeFederativa=" + uf.Id
                + " AND (UPPER(NomeCompleto COLLATE SQL_Latin1_General_Cp1251_CS_AS) ='" + strMunicipio.ToUpper() + "'"
                + " OR UPPER(NomeAbreviado COLLATE SQL_Latin1_General_Cp1251_CS_AS) ='" + strMunicipio.ToUpper() + "')");

            Municipio municipio = new Municipio();

            if (listMunicipios.Count > 0)
            {
                municipio = (Municipio)listMunicipios[0];
            }
            else if (listMunicipios.Count == 0 && uf.Id != 0)
            {
                municipio.Inicialize();
                municipio.IdUnidadeFederativa.Id = uf.Id;
                municipio.NomeAbreviado = strMunicipio;
                municipio.NomeCompleto = strMunicipio;
                municipio.Save();
            }

            return municipio;
        }
    }

    [Database("opsa", "Pais", "IdPais")]
    public class Pais : Ilitera.Common.LocalizacaoGeografica
    {
        private int _IdPais;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public Pais()
        {
            this.IndTipoLocalizacaoGeografica = (short)TipoLocalizacaoGeografica.Pais;
        }
        public override int Id
        {
            get { return _IdPais; }
            set { _IdPais = value; }
        }
    }

    [Database("opsa", "UnidadeFederativa", "IdUnidadeFederativa")]
    public class UnidadeFederativa : Ilitera.Common.LocalizacaoGeografica
    {
        private int _IdUnidadeFederativa;
        private Pais _IdPais;
        private string _NomeAbreviado;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public UnidadeFederativa()
        {
            this.IndTipoLocalizacaoGeografica = (short)TipoLocalizacaoGeografica.UnidadeFederativa;
        }
        public override int Id
        {
            get { return _IdUnidadeFederativa; }
            set { _IdUnidadeFederativa = value; }
        }
        public string NomeAbreviado
        {
            get { return _NomeAbreviado; }
            set { _NomeAbreviado = value; }
        }
        public Pais IdPais
        {
            get { return _IdPais; }
            set { _IdPais = value; }
        }
    }
    #endregion

    #region ContatoTelefonico

    [Database("opsa", "ContatoTelefonico", "IdContatoTelefonico")]
    public class ContatoTelefonico : Ilitera.Data.Table
    {
        private int _IdContatoTelefonico;
        private Pessoa _IdPessoa;
        private short _IndTipoTelefone;
        private string _Numero = string.Empty;
        private string _DDD = string.Empty;
        private string _Nome = string.Empty;
        private string _Departamento = string.Empty;
        private string _eMail = string.Empty;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public ContatoTelefonico()
        {

        }
        public enum TipoTelefone : int
        {
            Comercial,
            Residencial,
            Fax,
            Celular,
            Residencial2,
            Residencial3,
            Residencial4,
            Comercial2,
            Comercial3,
            Comercial4,
            Comercial5,
            Celular2,
            Celular3,
            Celular4,
        }

        public override int Id
        {
            get { return _IdContatoTelefonico; }
            set { _IdContatoTelefonico = value; }
        }
        public Pessoa IdPessoa
        {
            get { return _IdPessoa; }
            set { _IdPessoa = value; }
        }
        public short IndTipoTelefone
        {
            get { return _IndTipoTelefone; }
            set { _IndTipoTelefone = value; }
        }
        public string DDD
        {
            get { return _DDD; }
            set { _DDD = value; }
        }
        public string Numero
        {
            get { return _Numero; }
            set { _Numero = value; }
        }
        public string Nome
        {
            get { return _Nome; }
            set { _Nome = value; }
        }
        public string Departamento
        {
            get { return _Departamento; }
            set { _Departamento = value; }
        }
        public string eMail
        {
            get { return _eMail; }
            set { _eMail = value; }
        }

        public string GetDDDTelefone()
        {
            if (this.mirrorOld == null)
                this.Find();

            string ret;

            if (this._DDD != string.Empty)
                ret = this._DDD + " " + this._Numero;
            else
                ret = this._Numero;

            return ret;
        }

        public override string ToString()
        {
            string ret = string.Empty;

            if (this._DDD != string.Empty)
                ret = this._DDD + " " + this._Numero;
            else
                ret = this._Numero;

            if (this.Nome != string.Empty && this.Departamento == string.Empty)
                ret = ret + " (" + this.Nome + ")";
            else if (this.Nome != string.Empty && this.Departamento != string.Empty)
                ret = ret + " (" + this.Nome + " - " + this.Departamento + ")";

            if (ret == string.Empty)
                ret = "-";

            return ret;
        }

        public string GetTipo()
        {
            string ret = string.Empty;

            if (this.IndTipoTelefone == (int)ContatoTelefonico.TipoTelefone.Comercial)
                ret = "Comercial";
            else if (this.IndTipoTelefone == (int)ContatoTelefonico.TipoTelefone.Residencial)
                ret = "Residencial";
            else if (this.IndTipoTelefone == (int)ContatoTelefonico.TipoTelefone.Celular)
                ret = "Celular";
            else if (this.IndTipoTelefone == (int)ContatoTelefonico.TipoTelefone.Fax)
                ret = "Fax";
            else if (this.IndTipoTelefone == (int)ContatoTelefonico.TipoTelefone.Comercial2)
                ret = "Comercial_2";
            else if (this.IndTipoTelefone == (int)ContatoTelefonico.TipoTelefone.Comercial3)
                ret = "Comercial_3";
            else if (this.IndTipoTelefone == (int)ContatoTelefonico.TipoTelefone.Comercial4)
                ret = "Comercial_4";
            else if (this.IndTipoTelefone == (int)ContatoTelefonico.TipoTelefone.Comercial5)
                ret = "Comercial_5";
            else if (this.IndTipoTelefone == (int)ContatoTelefonico.TipoTelefone.Residencial2)
                ret = "Residencial_2";
            else if (this.IndTipoTelefone == (int)ContatoTelefonico.TipoTelefone.Residencial3)
                ret = "Residencial_3";
            else if (this.IndTipoTelefone == (int)ContatoTelefonico.TipoTelefone.Residencial4)
                ret = "Residencial_4";
            else if (this.IndTipoTelefone == (int)ContatoTelefonico.TipoTelefone.Celular2)
                ret = "Celular_2";
            else if (this.IndTipoTelefone == (int)ContatoTelefonico.TipoTelefone.Celular3)
                ret = "Celular_3";
            else if (this.IndTipoTelefone == (int)ContatoTelefonico.TipoTelefone.Celular4)
                ret = "Celular_4";

            return ret;
        }

        public DataSet ListaContatoTelefonico(Pessoa pessoa)
        {
            DataSet ds = new DataSet();
            DataTable table = new DataTable("Result");
            table.Columns.Add("Id", Type.GetType("System.String"));
            table.Columns.Add("Nome", Type.GetType("System.String"));
            table.Columns.Add("Departamento", Type.GetType("System.String"));
            table.Columns.Add("Tipo", Type.GetType("System.String"));
            table.Columns.Add("DDD", Type.GetType("System.String"));
            table.Columns.Add("Numero", Type.GetType("System.String"));
            ds.Tables.Add(table);
            DataRow newRow;
            
            ArrayList list = this.Find("IdPessoa = " + pessoa.Id + " ORDER BY IndTipoTelefone");
            
            for (int i = 0; i < list.Count; i++)
            {
                newRow = ds.Tables[0].NewRow();
                newRow["Id"] = ((ContatoTelefonico)list[i]).Id;
                newRow["Nome"] = ((ContatoTelefonico)list[i]).Nome;
                newRow["Departamento"] = ((ContatoTelefonico)list[i]).Departamento;
                newRow["Tipo"] = ((ContatoTelefonico)list[i]).GetTipo();
                newRow["DDD"] = ((ContatoTelefonico)list[i]).DDD;
                newRow["Numero"] = ((ContatoTelefonico)list[i]).Numero;
                ds.Tables[0].Rows.Add(newRow);
            }

            return ds;
        }
    }
    #endregion

    #region Cep

    [Database("opsa", "Cep", "IdCep")]
    public class Cep : Ilitera.Data.Table
    {
        private int _IdCep;
        private string _numeroCep;
        private string _Logradouro;
        private Municipio _IdMunicipio;
        private string _Bairro;
        private string _Bairro2;
        private TipoLogradouro _IdTipoLogradouro;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public Cep()
        {
        }
        public override int Id
        {
            get { return _IdCep; }
            set { _IdCep = value; }
        }
        public string numeroCep
        {
            get { return _numeroCep; }
            set { _numeroCep = value; }
        }
        public string Logradouro
        {
            get { return _Logradouro; }
            set { _Logradouro = value; }
        }
        public Municipio IdMunicipio
        {
            get { return _IdMunicipio; }
            set { _IdMunicipio = value; }
        }
        public string Bairro
        {
            get { return _Bairro; }
            set { _Bairro = value; }
        }
        public string Bairro2
        {
            get { return _Bairro2; }
            set { _Bairro2 = value; }
        }
        public TipoLogradouro IdTipoLogradouro
        {
            get { return _IdTipoLogradouro; }
            set { _IdTipoLogradouro = value; }
        }
    }
    #endregion

    #region TipoLogradouro

    [Database("opsa", "TipoLogradouro", "IdTipoLogradouro", "", "Tipo Logradouro")]
    public class TipoLogradouro : Ilitera.Data.Table
    {
        private int _IdTipoLogradouro;
        private string _NomeAbreviado = string.Empty;
        private string _NomeCompleto = string.Empty;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public TipoLogradouro()
        {

        }
        public override int Id
        {
            get { return _IdTipoLogradouro; }
            set { _IdTipoLogradouro = value; }
        }
        [Obrigatorio(true, "Nome Abreviado é campo obrigatório!")]
        public string NomeAbreviado
        {
            get { return _NomeAbreviado; }
            set { _NomeAbreviado = value; }
        }
        [Obrigatorio(true, "Nome Completo é campo obrigatório!")]
        public string NomeCompleto
        {
            get { return _NomeCompleto; }
            set { _NomeCompleto = value; }
        }
        public override string ToString()
        {
            if (this.mirrorOld == null)
                this.Find();

            return this.NomeAbreviado;
        }
    }
    #endregion

    #region Feriado

    public enum FeriadoTipo : short
    {
        Default,
        Postecipa,
        Antecipa
    }

    [Database("opsa", "Feriado", "IdFeriado")]
    public class Feriado : Ilitera.Data.Table
    {

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public Feriado()
        {

        }

        private int _IdFeriado;
        private DateTime _DataFeriado = DateTime.Today;
        private LocalizacaoGeografica _IdLocalizacaoGeografica;
        private string _NomeFeriado = string.Empty;
        private bool _IndPermanente;

        public override int Id
        {
            get { return _IdFeriado; }
            set { _IdFeriado = value; }
        }
        [Obrigatorio(true, "A data é obrigatória!")]
        public DateTime DataFeriado
        {
            get { return _DataFeriado; }
            set { _DataFeriado = value; }
        }
        [Obrigatorio(true, "A Localização Geografica é obrigatória!")]
        public LocalizacaoGeografica IdLocalizacaoGeografica
        {
            get { return _IdLocalizacaoGeografica; }
            set { _IdLocalizacaoGeografica = value; }
        }
        [Obrigatorio(true, "Nome do feriado é obrigatório!")]
        public string NomeFeriado
        {
            get { return _NomeFeriado; }
            set { _NomeFeriado = value; }
        }
        public bool IndPermanente
        {
            get { return _IndPermanente; }
            set { _IndPermanente = value; }
        }
        public override string ToString()
        {
            string ret = string.Empty;

            if (_IndPermanente)
                ret = _DataFeriado.ToString("dd-MM") + " - " + this._NomeFeriado;
            else
                ret = _DataFeriado.ToString("dd-MM-yyyy") + " - " + this._NomeFeriado;

            return ret;
        }

        public static bool IsFeriado(DateTime data, Municipio municipio)
        {
            bool ret = false;
            Feriado feriado = new Feriado();
            if (municipio.IdUnidadeFederativa == null)
                municipio.IdUnidadeFederativa = new UnidadeFederativa();
            municipio.IdUnidadeFederativa.Find();
            string strWhere = ") AND ((IndPermanente = 1 AND MONTH(DataFeriado) = "
                            + data.Month.ToString() + " AND DAY(DataFeriado) = "
                            + data.Day.ToString() + ") OR "
                            + "(IndPermanente = 0 AND DataFeriado = '"
                            + data.ToString("yyyy-MM-dd") + "'))";
            if (feriado.Find("(IdLocalizacaoGeografica = " + municipio.Id.ToString() + strWhere).Count > 0)
                ret = true;
            else if (feriado.Find("(IdLocalizacaoGeografica=" + municipio.IdUnidadeFederativa.Id.ToString() + strWhere).Count > 0)
                ret = true;
            else if (feriado.Find("(IdLocalizacaoGeografica=" + municipio.IdUnidadeFederativa.IdPais.Id.ToString() + strWhere).Count > 0)
                ret = true;
            return ret;
        }

        public static bool IsFinalSemana(DateTime data)
        {
            return (data.DayOfWeek == DayOfWeek.Sunday || data.DayOfWeek == DayOfWeek.Saturday);
        }

        public static bool IsDomingo(DateTime data)
        {
            return (data.DayOfWeek == DayOfWeek.Sunday);
        }

        public static DateTime AjustaData(DateTime data, short IndFeriado)
        {
            if (IndFeriado != (short)FeriadoTipo.Default)
            {
                while (IsFinalSemana(data))
                {
                    if (IndFeriado == (short)FeriadoTipo.Postecipa)
                        data = data.AddDays(1);
                    else if (IndFeriado == (short)FeriadoTipo.Antecipa)
                        data = data.AddDays(-1);
                }
            }
            return data;
        }

        public static DateTime AjustaData(DateTime data, Municipio municipio, short IndFeriado)
        {
            if (IndFeriado != (short)FeriadoTipo.Default)
            {
                while (IsFinalSemana(data) || IsFeriado(data, municipio))
                {
                    if (IndFeriado == (short)FeriadoTipo.Postecipa)
                        data = data.AddDays(1);
                    else if (IndFeriado == (short)FeriadoTipo.Antecipa)
                        data = data.AddDays(-1);
                }
            }
            return data;
        }
    }
    #endregion
}
