using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;

using Ilitera.Data;


namespace Ilitera.Common
{
    public enum IndLocaisTrabalho : short
    {
        LocaisAtivos,
        LocaisInativos,
        Todos
    }

    [Database("opsa", "Juridica", "IdJuridica")]
    public class Juridica : Ilitera.Common.Pessoa, Ilitera.Common.IJuridica
    {
        #region Properties

        private int _IdJuridica;
        private JuridicaPapel _IdJuridicaPapel;
        private string _Atividade = string.Empty;
        private string _IE = string.Empty;
        private string _CCM = string.Empty;
        private DateTime _DataFundacao;
        private int _QtdEmpregados;
        private GrupoEmpresa _IdGrupoEmpresa;
        private Juridica _IdJuridicaPai;
        private string _Observacao = string.Empty;
        private string _Diretor = string.Empty;
        private Transporte _IdTransporte;
        private CNAE _IdCNAE;
        //private CNAE _IdCNAE_1;
        private bool _IsOptanteSimples;
        private bool _AtivarLocalDeTrabalho;
        private string _Logotipo = string.Empty;
        private string _CodigoLojaObra = string.Empty;
        private string _DiretorioPadrao = string.Empty;
        private bool _IsPesonalizarCarimboCnpj;
        private string _CarimboCnpjHtml = string.Empty;
        private bool _IsPersonalizarDadosEmpresa;
        private string _DadosEmpresaHtml = string.Empty;
        private string _CEI = string.Empty;
        private string _CNO = string.Empty;
        private string _Auxiliar;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public Juridica()
        {
            IndTipoPessoa = (short)Pessoa.TipoPessoa.Juridica;
        }

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public Juridica(string NomePessoa)
        {
            this.NomeAbreviado = NomePessoa;
        }
        public override int Id
        {
            get { return _IdJuridica; }
            set { _IdJuridica = value; }
        }
        public JuridicaPapel IdJuridicaPapel
        {
            get { return _IdJuridicaPapel; }
            set { _IdJuridicaPapel = value; }
        }
        public string Atividade
        {
            get { return _Atividade; }
            set { _Atividade = value; }
        }
        public string IE
        {
            get { return _IE; }
            set { _IE = value; }
        }
        public string CEI
        {
            get { return _CEI; }
            set { _CEI = value; }
        }
        public string CNO
        {
            get { return _CNO; }
            set { _CNO = value; }
        }
        public string CCM
        {
            get { return _CCM; }
            set { _CCM = value; }
        }
        public DateTime DataFundacao
        {
            get { return _DataFundacao; }
            set { _DataFundacao = value; }
        }
        public int QtdEmpregados
        {
            get { return _QtdEmpregados; }
            set { _QtdEmpregados = value; }
        }
        public GrupoEmpresa IdGrupoEmpresa
        {
            get { return _IdGrupoEmpresa; }
            set { _IdGrupoEmpresa = value; }
        }
        public Juridica IdJuridicaPai
        {
            get { return _IdJuridicaPai; }
            set { _IdJuridicaPai = value; }
        }
        public string Observacao
        {
            get { return _Observacao; }
            set { _Observacao = value; }
        }
        public string Diretor
        {
            get { return _Diretor; }
            set { _Diretor = value; }
        }
        public Transporte IdTransporte
        {
            get { return _IdTransporte; }
            set { _IdTransporte = value; }
        }
        public CNAE IdCNAE
        {
            get { return _IdCNAE; }
            set { _IdCNAE = value; }
        }
        //public CNAE IdCNAE_1
        //{
        //    get { return _IdCNAE_1; }
        //    set { _IdCNAE_1 = value; }
        //}
        public bool IsOptanteSimples
        {
            get { return _IsOptanteSimples; }
            set { _IsOptanteSimples = value; }
        }
        public bool AtivarLocalDeTrabalho
        {
            get { return _AtivarLocalDeTrabalho; }
            set { _AtivarLocalDeTrabalho = value; }
        }
        public string Logotipo
        {
            get { return _Logotipo; }
            set { _Logotipo = value; }
        }
        public string CodigoLojaObra
        {
            get { return _CodigoLojaObra; }
            set { _CodigoLojaObra = value; }
        }
        public string DiretorioPadrao
        {
            get { return _DiretorioPadrao; }
            set { _DiretorioPadrao = value; }
        }
        public bool IsPesonalizarCarimboCnpj
        {
            get { return _IsPesonalizarCarimboCnpj; }
            set { _IsPesonalizarCarimboCnpj = value; }
        }
        public string CarimboCnpjHtml
        {
            get { return _CarimboCnpjHtml; }
            set { _CarimboCnpjHtml = value; }
        }
        public bool IsPersonalizarDadosEmpresa
        {
            get { return _IsPersonalizarDadosEmpresa; }
            set { _IsPersonalizarDadosEmpresa = value; }
        }
        public string DadosEmpresaHtml
        {
            get { return _DadosEmpresaHtml; }
            set { _DadosEmpresaHtml = value; }
        }
        public string Auxiliar
        {
            get { return _Auxiliar; }
            set { _Auxiliar = value; }
        }

        public override string ToString()
        {
            if (this.mirrorOld == null)
                this.Find();

            return this.NomeAbreviado;
        }

        #endregion

        #region Metodos

        #region CNPJ

        public string GetCnpj()
        {
            if (this.mirrorOld == null)
                this.Find();

            return FormatarCnpj(this.NomeCodigo);
        }

        public static string FormatarCnpj(string cnpj)
        {
            string ret;

            if (!IsCNPJ(cnpj) && cnpj.Length == 14)
            {
                string padrao = @"^(\d{2})(\d{3})(\d{3})(\d{4})(\d{2})";

                Regex re = new Regex(padrao);

                Match m = re.Match(cnpj);

                if (m.Success)
                    ret = m.Groups[1].Value + "." + m.Groups[2].Value + "." + m.Groups[3].Value + "/" + m.Groups[4].Value + "-" + m.Groups[5].Value;
                else
                    ret = cnpj;
            }
            else
                ret = cnpj;

            return ret;
        }
        #endregion

        #region InscricaoEstadual

        private static bool IsIncricaoEstadualSP(string ie)
        {
            Int64 val;

            if (!System.Int64.TryParse(ie, out val))//Não é número
                return true;

            int dig1, dig2, soma = 0; //

            int[] Peso1 = { 1, 3, 4, 5, 6, 7, 8, 10 };  //Calcular o 1o. dígito
            int[] Peso2 = { 3, 2, 10, 9, 8, 7, 6, 5, 4, 3, 2 }; //Calcular o 2o. dígito

            string FimIE; //Guarda os valores encontrados por nossa função

            try
            {
                //Vamos achar o valor do 1o. digito       
                for (int tmp = 0; tmp <= 7; tmp++)
                    soma = soma + (Convert.ToInt32(ie.Substring(tmp, 1)) * Peso1[tmp]);

                dig1 = soma % 11;  //Resto da divisão da soma do 1o. Dígito

                if (dig1 >= 10)
                    dig1 = 0;

                //faz a junção dos 8 primeiros numeros com o digito encontrado,
                //apartir desse ponto acharemos o segundo digito.

                FimIE = ie.Substring(0, 8) + dig1.ToString() + ie.Substring(9, 2);
                soma = 0;

                for (int tmp = 0; tmp <= 10; tmp++)
                    soma = soma + (Convert.ToInt32(ie.Substring(tmp, 1)) * Peso2[tmp]);

                dig2 = soma % 11; //Resto da divisão da soma do 2o. Dígito

                if (dig2 >= 10)
                    dig2 = 0;

                //Faz a junção do 2o. digito

                FimIE = FimIE + dig2.ToString();

                if (FimIE == ie)
                    return true;
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }

        private void ValidarInscricaoEstadual()
        {
            Endereco endereco = this.GetEndereco();

            if (endereco.IdMunicipio.mirrorOld == null)
                endereco.IdMunicipio.Find();

            if (endereco.IdMunicipio.Id != 0 && endereco.IdMunicipio.IdUnidadeFederativa.Id == 1)//Estado de São Paulo
            {
                if (this.IE != string.Empty && this.IE.ToUpper() != "ISENTO" && this.IE.ToUpper() != "ISENTA")
                {
                    if (!IsIncricaoEstadualSP(this.IE.Replace(".", string.Empty)))
                        throw new Exception("Número de Inscrição Estadual Inválido!");
                }
            }
        }

        #endregion

        #region FotoFachada

        public string GetFotoFachada()
        {
            string ret;

            if (this.Foto == string.Empty
                && this.IdJuridicaPai.Id != 0)
            {
                if (this.IdJuridicaPai.IdCNAE == null)
                    this.IdJuridicaPai.Find();
                ret = this.IdJuridicaPai.GetFotoFachada();
            }
            else
                ret = this.Foto;

            return ret;
        }
        #endregion

        #region GetCnaeCipa

        public CNAE GetCnaeCipa()
        {
            if (this.IdCNAE == null)
                this.Find();

            return this.IdCNAE;
        }

        #endregion 

        #region GetCnaeSesmt

        public CNAE GetCnaeSesmt()
        {
            if (this.IdCNAE.mirrorOld == null)
                this.IdCNAE.Find();

            return this.IdCNAE;
        }

        #endregion 

        #region DimensionamentoCipa

        public DimensionamentoCipa GetDimensionamentoCipa()
        {
            CNAE cnae = this.GetCnaeCipa();

            if (cnae.IdGrupoCipa == null)
                cnae.Find();

            return GetDimensionamentoCipa(cnae, this.QtdEmpregados);
        }

        public static DimensionamentoCipa GetDimensionamentoCipa(CNAE cnae, int QuantidadeEmpregados)
        {
            DimensionamentoCipa dimensCipa = new DimensionamentoCipa();

            int IdGrupoCipa = cnae.IdGrupoCipa.Id;

            ArrayList list = dimensCipa.Find("IdGrupoCipa=" + IdGrupoCipa);

            for (int i = 0; i < list.Count; i++)
            {
                if (QuantidadeEmpregados >= ((DimensionamentoCipa)list[i]).MinEmpregado &&
                    QuantidadeEmpregados <= ((DimensionamentoCipa)list[i]).MaxEmpregado)
                {
                    dimensCipa = (DimensionamentoCipa)list[i];
                    break;
                }
            }

            return dimensCipa;
        }
        #endregion

        #region HasCipa

        public bool HasCipa()
        {
            bool ret;
            DimensionamentoCipa dimensCipa = new DimensionamentoCipa();
            try
            {
                dimensCipa = GetDimensionamentoCipa();
            }
            catch { }
            ret = (dimensCipa.Efetivo > 0 || dimensCipa.Suplente > 0);
            return ret;
        }
        #endregion

        #region HasSesmt

        public bool HasSesmt()
        {
            Sesmt sesmt = new Sesmt();
            try
            {
                sesmt = GetSesmt();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return (sesmt.Tecnico > 0);
        }
        #endregion

        #region SESMT

        public int GetQtdEmpregadosSESMT()
        {
            int ret = 0;
            //try
            //{
            //    string strCmd = "SELECT SUM(QtdEmpregados)"
            //            + " FROM Juridica WHERE "
            //            + " IdJuridicaPapel=" + (int)this.IdJuridicaPapel.Id
            //            + " AND IdJuridica IN (SELECT IdPessoa FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.Pessoa WHERE IsInativo=0"
            //            + " AND NomeCodigo Like '" + this.NomeCodigo.Substring(0, this.NomeCodigo.IndexOf("/")) + "%'"
            //            + " OR NomeCompleto ='" + this.NomeCompleto + "')";

            //    ret = Convert.ToInt32(new Juridica().ExecuteScalar(strCmd));
            //}
            //catch { }

            ret = this.QtdEmpregados;

            return ret;
        }

        public Sesmt GetSesmt()
        {
            return GetSesmt(this.GetCnaeSesmt());
        }

        public Sesmt GetSesmt(CNAE cnae)
        {
            int numEmpregados = this.GetQtdEmpregadosSESMT();

            if (cnae.IdGrupoCipa == null)
                cnae.Find();

            Sesmt sesmt = new Sesmt();

            ArrayList list = sesmt.Find("GrauRisco=" + cnae.GrauRisco);

            for (int i = 0; i < list.Count; i++)
            {
                if (numEmpregados >= ((Sesmt)list[i]).MinEmpreg &&
                    numEmpregados <= ((Sesmt)list[i]).MaxEmpreg)
                {
                    sesmt = (Sesmt)list[i];
                    break;
                }
            }

            return sesmt;
        }
        #endregion

        #region LogoEmpresa

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public System.Drawing.Bitmap LogoEmpresa()
        {
            System.Drawing.Bitmap ret = null;

            string path = string.Empty;

            if (!this.Logotipo.Equals(string.Empty))
            {

                path = this.Logotipo;
                

                ret = Ilitera.Common.Fotos.GetImageFoto(path);
            }

            return ret;
        }
        #endregion

        #region IsContratadaEContratante

        private bool IsContratadaEContratante()
        {
            return this.IdJuridicaPapel.Id == (int)IndJuridicaPapel.Tomadora;
        }
        #endregion

        #region CarimboCNPJ

        #region GetCarimboCnpjHtml(DateTime dataDocumento)

        public string GetCarimboCnpjHtml(DateTime dataDocumento)
        {
            if (this.mirrorOld == null)
                this.Find();

            if (this.IsPesonalizarCarimboCnpj && CarimboCnpjHtml != string.Empty)
                return CarimboCnpjHtml;
            else
                return GetCarimboCnpjHtml(dataDocumento, false);
        }
        #endregion

        #region GetCarimboCnpjHtml(DateTime dataDocumento, bool IsCipa)

        public string GetCarimboCnpjHtml(DateTime dataDocumento, bool IsCipa)
        {
            if (this.mirrorOld == null)
                this.Find();

            if (this.IsPesonalizarCarimboCnpj && CarimboCnpjHtml != string.Empty)
                return CarimboCnpjHtml;
            else
            {
                if (IsContratadaEContratante())
                    return GetCarimboCnpjContratadoEContratante(dataDocumento);
                else
                    return GetCarimboCjpj(dataDocumento, IsCipa);
            }
        }
        #endregion

        #region GetCarimboCjpj(DateTime dataDocumento, bool IsCipa)

        //private string GetCarimboCjpj(DateTime dataDocumento, bool IsCipa)
        //{
        //    //////////////////////////////////////////////////////////////////
        //    ////////                    Juridica 
        //    //////////////////////////////////////////////////////////////////
        //    if (this.mirrorOld == null)
        //        this.Find();

        //    IPessoa iPessoa;
        //    IJuridica iJuridica;
        //    IEndereco iEndereco;

        //    GetDadosEmpresa(dataDocumento, out iPessoa, out iJuridica, out iEndereco);

        //    //////////////////////////////////////////////////////////////////
        //    ////////                     HTML
        //    //////////////////////////////////////////////////////////////////

        //    StringBuilder str = new StringBuilder();
        //    str.Append("<P align='center'>");

        //    if (this.IdJuridicaPapel.IsLocalDeTrabalho())
        //    {
        //        //////////////////////////////////////////////////////////////////
        //        ////////                 Juridica Pai
        //        //////////////////////////////////////////////////////////////////

        //        if (this.IdJuridicaPai.mirrorOld == null)
        //            this.IdJuridicaPai.Find();

        //        IPessoa iPessoaPai;


        //        AlteracaoCadastral alteracaoCadastralPai = this.IdJuridicaPai.GetAlteracaoCadastral(ref dataDocumento);

        //        if (alteracaoCadastralPai.Id == 0)
        //            iPessoaPai = this.IdJuridicaPai;
        //        else
        //            iPessoaPai = alteracaoCadastralPai;

        //        str.Append("<B>" + iPessoaPai.NomeCompleto + "</B>");
        //        str.Append("<br>");
        //        str.Append("<FONT size=2>" + this.IdJuridicaPapel.GetDescricaoLocaldeTrabalho());

        //        str.Append("<br>");

        //        str.Append(iPessoa.NomeCompleto);

        //        str.Append("<br>");

        //        str.Append("</FONT>");

        //        str.Append("<br>&nbsp;<br>");
        //    }
        //    else
        //    {
        //        str.Append("<B>" + iPessoa.NomeCompleto + "</B>");
        //        str.Append("<br>&nbsp;<br>");
        //    }

        //    str.Append("<FONT size=2>" + Endereco.GetEndereco(iEndereco));
        //    str.Append("<br>");
        //    str.Append("CEP " + iEndereco.Cep + " " + iEndereco.Bairro);
        //    str.Append("<br>");
        //    str.Append(Endereco.GetCidade(iEndereco) + "  " + Endereco.GetEstado(iEndereco) + "</FONT>");
        //    str.Append("<br>&nbsp;<br>");
        //    str.Append("<B>CNPJ " + Juridica.FormatarCnpj(iPessoa.NomeCodigo) + "</B>");
        //    str.Append("<br>");

        //    if (iJuridica.IdCNAE.Id != 0)
        //    {
        //        iJuridica.IdCNAE.Find();

        //        if(IsCipa)
        //            str.Append("<FONT size=2>CNAE " + iJuridica.IdCNAE.Codigo + "&nbsp;&nbsp;  Grupo CIPA: " + iJuridica.IdCNAE.IdGrupoCipa.ToString() + "</FONT>");
        //        else
        //            str.Append("<FONT size=2>CNAE " + iJuridica.IdCNAE.Codigo  + "</FONT>");
        //    }

        //    return str.ToString();
        //}

        private string GetCarimboCjpj(DateTime dataDocumento, bool IsCipa)
        {
            //////////////////////////////////////////////////////////////////
            ////////                    Juridica 
            //////////////////////////////////////////////////////////////////
            if (this.mirrorOld == null)
                this.Find();

            IPessoa iPessoa;
            IJuridica iJuridica;
            IEndereco iEndereco;

            GetDadosEmpresa(dataDocumento, out iPessoa, out iJuridica, out iEndereco);

            //////////////////////////////////////////////////////////////////
            ////////                     HTML
            //////////////////////////////////////////////////////////////////

            StringBuilder str = new StringBuilder();
            str.Append("<P align='center'>");



            if (this.IdJuridicaPapel.IsLocalDeTrabalho())
            {
                //////////////////////////////////////////////////////////////////
                ////////                 Juridica Pai
                //////////////////////////////////////////////////////////////////

                if (this.IdJuridicaPai.mirrorOld == null)
                    this.IdJuridicaPai.Find();

                IPessoa iPessoaPai;


                AlteracaoCadastral alteracaoCadastralPai = this.IdJuridicaPai.GetAlteracaoCadastral(ref dataDocumento);

                if (alteracaoCadastralPai.Id == 0)
                    iPessoaPai = this.IdJuridicaPai;
                else
                    iPessoaPai = alteracaoCadastralPai;

                str.Append("<FONT size=2><B>" + iPessoaPai.NomeCompleto + "</B><br>");
                str.Append("<B>CNPJ " + Juridica.FormatarCnpj(iPessoaPai.NomeCodigo) + "</B>");
                str.Append("<br>");


                if (iJuridica.IdCNAE.Id != 0)
                {
                    iJuridica.IdCNAE.Find();

                    if (IsCipa)
                        str.Append("<FONT size=2>CNAE " + iJuridica.IdCNAE.Codigo + "&nbsp;&nbsp;  Grupo CIPA: " + iJuridica.IdCNAE.IdGrupoCipa.ToString() + "</FONT>");
                    else
                        str.Append("<FONT size=2>CNAE " + iJuridica.IdCNAE.Codigo + "</FONT>");
                }


                //str.Append("<br>&nbsp;<br>");
                str.Append("<br>");
                str.Append("<br>");

                str.Append("<FONT size=2>" + this.IdJuridicaPapel.GetDescricaoLocaldeTrabalho());
                str.Append("<br>");

                


                System.Globalization.CultureInfo ptBr2 = new System.Globalization.CultureInfo("pt-Br");

                Identificacao_Historico rInd = new Identificacao_Historico();
                rInd.Find(" IdPessoa = " + this.Id + " and  convert( smalldatetime, '" + dataDocumento.ToString("dd/MM/yyyy", ptBr2) + "',103 ) between Inicio_Vigencia and Termino_Vigencia ");


                if (rInd.Id != 0)
                {
                    if (this.CEI != null && this.CEI != "")
                    {
                        str.Append("<B>CNPJ/CEI " + Juridica.FormatarCnpj(rInd.CNPJ) + " / " + this.CEI + "</B>");
                    }
                    else
                    {
                        str.Append("<B>CNPJ " + Juridica.FormatarCnpj(rInd.CNPJ) + "</B>");
                    }

                    str.Append("<br>" + rInd.NomeCompleto);
                }
                else
                {
                    if (this.CEI != null && this.CEI != "")
                    {
                        str.Append("<B>CNPJ/CEI " + Juridica.FormatarCnpj(this.NomeCodigo) + " / " + this.CEI + "</B>");
                    }
                    else
                    {
                        str.Append("<B>CNPJ " + Juridica.FormatarCnpj(this.NomeCodigo) + "</B>");
                    }

                    
                    str.Append("<br>" + iPessoa.NomeCompleto);
                }

                str.Append("</FONT>");

                
       
                str.Append("<br>");
            }
            else
            {


                System.Globalization.CultureInfo ptBr2 = new System.Globalization.CultureInfo("pt-Br");

                Identificacao_Historico rInd = new Identificacao_Historico();
                rInd.Find(" IdPessoa = " + this.Id + " and  convert( smalldatetime, '" + dataDocumento.ToString("dd/MM/yyyy", ptBr2) + "',103 ) between Inicio_Vigencia and Termino_Vigencia ");

                if (rInd.Id != 0)
                {
                    str.Append("<B>" + rInd.NomeCompleto + "</B>");
                }
                else
                {
                    str.Append("<B>" + iPessoa.NomeCompleto + "</B>");
                }

                
                
                str.Append("<br>&nbsp;<br>");

                if (iJuridica.IdCNAE.Id != 0)
                {
                    iJuridica.IdCNAE.Find();

                    if (IsCipa)
                        str.Append("<FONT size=2>CNAE " + iJuridica.IdCNAE.Codigo + "&nbsp;&nbsp;  Grupo CIPA: " + iJuridica.IdCNAE.IdGrupoCipa.ToString() + "</FONT>");
                    else
                        str.Append("<FONT size=2>CNAE " + iJuridica.IdCNAE.Codigo + "</FONT>");
                }


                str.Append("<br>");
                //str.Append("<br>&nbsp;<br>");


                if (rInd.Id != 0)
                {
                    str.Append("<B>CNPJ " + Juridica.FormatarCnpj(rInd.CNPJ) + "</B>");
                }
                else
                {
                    str.Append("<B>CNPJ " + Juridica.FormatarCnpj(iPessoa.NomeCodigo) + "</B>");
                }


                str.Append("<br>");


            }


            System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");

            Endereco_Historico rEnd = new Endereco_Historico();
            rEnd.Find(" IdPessoa = " + this.Id + " and convert( smalldatetime, '" + dataDocumento.ToString("dd/MM/yyyy", ptBr) + "',103 ) between Inicio_Vigencia and Termino_Vigencia ");

            if (rEnd.Id != 0)
            {
                StringBuilder str2 = new StringBuilder();

                TipoLogradouro rTp = new TipoLogradouro();
                rTp.Find(rEnd.IdTipoLogradouro);

                if (rTp.Id != 0)
                {
                    str2.Append(rTp.NomeAbreviado);
                }

                str2.Append(" " + rEnd.Logradouro);

                if (rEnd.Numero != string.Empty)
                    str2.Append(" " + rEnd.Numero);

                if (rEnd.Complemento != string.Empty && rEnd.Complemento.ToUpper() != "NULL")
                    str2.Append(" " + rEnd.Complemento);

                str.Append("<FONT size=2>" + str2);
                str.Append("<br>");
                str.Append("CEP" + rEnd.Cep + "  " + rEnd.Bairro + "<br>" + rEnd.Municipio + "  " + rEnd.Uf + "</FONT>");

            }
            else
            {
                str.Append("<FONT size=2>" + Endereco.GetEndereco(iEndereco));
                str.Append("<br>");
                str.Append("CEP " + iEndereco.Cep + " " + iEndereco.Bairro);
                str.Append("<br>");
                str.Append(Endereco.GetCidade(iEndereco) + "  " + Endereco.GetEstado(iEndereco) + "</FONT>");
            }

            if (str.Length > 290)
            {
                str.Replace("<FONT size=2>", "<FONT size=1>");
            }


            return str.ToString();
        }


        #endregion

        #region GetDadosEmpresa

        public void GetDadosEmpresa(DateTime dataDocumento, 
                                    out IPessoa iPessoa, 
                                    out IJuridica iJuridica, 
                                    out IEndereco iEndereco)
        {
            AlteracaoCadastral alteracaoCadastral = this.GetAlteracaoCadastral(ref dataDocumento);

            if (alteracaoCadastral.Id == 0)
            {
                iPessoa = this;
                iJuridica = this;
                iEndereco = this.GetEndereco(TipoEndereco.Default);
            }
            else
            {
                iPessoa = alteracaoCadastral;
                iJuridica = alteracaoCadastral;
                iEndereco = alteracaoCadastral;
            }
        }


        #endregion

        #region GetCarimboCnpjContratadoEContratante

        private string GetCarimboCnpjContratadoEContratante(DateTime dataDocumento)
        {
            //////////////////////////////////////////////////////////////////
            ////////                    Juridica 
            //////////////////////////////////////////////////////////////////
            if (this.mirrorOld == null)
                this.Find();

            IPessoa iPessoa;
            IJuridica iJuridica;
            IEndereco iEndereco;

            AlteracaoCadastral alteracaoCadastral = this.GetAlteracaoCadastral(ref dataDocumento);

            if (alteracaoCadastral.Id == 0)
            {
                iPessoa = this;
                iJuridica = this;
                iEndereco = this.GetEndereco(TipoEndereco.Default);
            }
            else
            {
                iPessoa = alteracaoCadastral;
                iJuridica = alteracaoCadastral;
                iEndereco = alteracaoCadastral;
            }

            //////////////////////////////////////////////////////////////////
            ////////                 Juridica Pai
            //////////////////////////////////////////////////////////////////
            if (this.IdJuridicaPai.mirrorOld == null)
                this.IdJuridicaPai.Find();

            IPessoa iPessoaPai;
            IJuridica iJuridicaPai;
            IEndereco iEnderecoPai;

            AlteracaoCadastral alteracaoCadastralPai = this.IdJuridicaPai.GetAlteracaoCadastral(ref dataDocumento);

            if (alteracaoCadastralPai.Id == 0)
            {
                iPessoaPai = this.IdJuridicaPai;
                iJuridicaPai = this.IdJuridicaPai;
                iEnderecoPai = this.IdJuridicaPai.GetEndereco(TipoEndereco.Default);
            }
            else
            {
                iPessoaPai = alteracaoCadastralPai;
                iJuridicaPai = alteracaoCadastralPai;
                iEnderecoPai = alteracaoCadastralPai;
            }

            //////////////////////////////////////////////////////////////////
            ////////                     HTML
            //////////////////////////////////////////////////////////////////

            StringBuilder str = new StringBuilder();

            str.Append("<P align='center'>");
            str.Append("<B>" + iPessoaPai.NomeCompleto + "</B>");
            str.Append("<br>");
            str.Append("<FONT size=2>" + Endereco.GetEnderecoCompleto(iEnderecoPai) + "</FONT>");
            str.Append("<br>");
            str.Append("<FONT size=2><B>CNPJ " + Juridica.FormatarCnpj(iPessoaPai.NomeCodigo) + "</B></FONT>");
            str.Append("<br>");

            if (iJuridicaPai.IdCNAE.Id != 0)
            {
                iJuridicaPai.IdCNAE.Find();
                str.Append("<FONT size=2>CNAE " + iJuridicaPai.IdCNAE.Codigo 
                        //+ "   Grau de Risco: " + iJuridicaPai.IdCNAE.GrauRisco.ToString() 
                        + "</FONT>");
            }

            str.Append("<br>");
            str.Append("<FONT size=2>" + this.IdJuridicaPapel.GetDescricaoLocaldeTrabalho() + "</FONT>");
            str.Append("<br>");
            str.Append("<B>" + iPessoa.NomeCompleto + "</B>");
            str.Append("<br>");
            str.Append("<FONT size=2>" + Endereco.GetEnderecoCompleto(iEndereco));
            str.Append("<br>");
            str.Append("<B>CNPJ " + Juridica.FormatarCnpj(iPessoa.NomeCodigo) + "</B>");
            str.Append("<br>");

            if (iJuridica.IdCNAE.Id != 0)
                str.Append("CNAE " + iJuridica.IdCNAE.GetCodigo()); 

            str.Append("</FONT>");
            str.Append("</P>");

            return str.ToString();
        }
        #endregion

        #region GetCnaeGrupo

        public string GetCnaeGrupo()
        {
            if (this.IdCNAE.Id == 0)
                return string.Empty;

            if (this.IdCNAE.mirrorOld == null)
                this.IdCNAE.Find();

            return "CNAE: " + this.IdCNAE.Codigo + "   Grupo CIPA: " + this.IdCNAE.IdGrupoCipa.ToString() + "   Nº Empregados: " + this.QtdEmpregados.ToString() ;

        }
        #endregion

        #region GetAlteracaoCadastral

        private AlteracaoCadastral GetAlteracaoCadastral(ref DateTime dataDocumento)
        {
            string where = "IdJuridica=" + this.Id
                            + " AND DataAlteracao >'"
                            + dataDocumento.ToString("yyyy-MM-dd") + "'";

            AlteracaoCadastral alteracaoCadastral = new AlteracaoCadastral();
            alteracaoCadastral.FindMin("DataAlteracao", where);

            return alteracaoCadastral;
        }
#endregion

        #endregion

        #region DadosEmpresa HTML

        #region GetDadosEmpresaHtml

        public string GetDadosEmpresaHtml_Obra_Prajna(DateTime dataDocumento, Int32 xNumColaboradores)
        {
            return GetDadosEmpresa_Obra_Prajna(dataDocumento, this, xNumColaboradores);
        }


        public string GetDadosEmpresaHtml_Obra_Prajna(DateTime dataDocumento, Int32 xNumColaboradores, bool xInibirDadosMatriz_Laudos)
        {
            return GetDadosEmpresa_Obra_Prajna(dataDocumento, this, xNumColaboradores, xInibirDadosMatriz_Laudos);
        }



        public string GetDadosEmpresaHtml(DateTime dataDocumento)
        {
            if (this.IsPersonalizarDadosEmpresa && this.DadosEmpresaHtml != string.Empty)
            {
                return this.DadosEmpresaHtml;
            }
            else
            {
                if (IsContratadaEContratante())
                    return GetDadosEmpresaContratadoEContratante(dataDocumento);
                else
                    return GetDadosEmpresa(dataDocumento, this);
            }
        }

        public string GetDadosEmpresaHtml(DateTime dataDocumento, int xColaboradoresExpostos)
        {
            if (this.IsPersonalizarDadosEmpresa && this.DadosEmpresaHtml != string.Empty)
            {
                return this.DadosEmpresaHtml;
            }
            else
            {
                if (IsContratadaEContratante())
                    return GetDadosEmpresaContratadoEContratante(dataDocumento);
                else
                    return GetDadosEmpresa(dataDocumento, this, xColaboradoresExpostos);
            }
        }



        public string GetDadosEmpresaHtml(DateTime dataDocumento, int xColaboradoresExpostos, bool xInibir_US)
        {
            if (this.IsPersonalizarDadosEmpresa && this.DadosEmpresaHtml != string.Empty)
            {
                return this.DadosEmpresaHtml;
            }
            else
            {
                if (IsContratadaEContratante())
                    return GetDadosEmpresaContratadoEContratante(dataDocumento);
                else
                    return GetDadosEmpresa3(dataDocumento, this, xColaboradoresExpostos);
            }
        }

        public string GetDadosEmpresaHtml(DateTime dataDocumento, int xColaboradoresExpostos, Ilitera.Common.Juridica zJuridica)
        {
            if (this.IsPersonalizarDadosEmpresa && this.DadosEmpresaHtml != string.Empty)
            {
                return this.DadosEmpresaHtml;
            }
            else
            {
                if (IsContratadaEContratante())
                    return GetDadosEmpresaContratadoEContratante(dataDocumento);
                else
                    return GetDadosEmpresa2(dataDocumento, zJuridica, xColaboradoresExpostos);
            }
        }

        #endregion

        #region GetDadosEmpresa(DateTime dataDocumento, Juridica juridica)


        private static string GetDadosEmpresa3(DateTime dataDocumento, Juridica juridica, int ColaboradoresExpostos)
        {
            //////////////////////////////////////////////////////////////////
            ////////                    Juridica 
            //////////////////////////////////////////////////////////////////
            if (juridica.mirrorOld == null)
                juridica.Find();

            IPessoa iPessoa;
            IJuridica iJuridica;
            IEndereco iEndereco;

            GetAlteracaoCadastral(dataDocumento, juridica, out iPessoa, out iJuridica, out iEndereco);

            //////////////////////////////////////////////////////////////////
            ////////                    HTML 
            //////////////////////////////////////////////////////////////////

            StringBuilder str = new StringBuilder();

            str.Append(@"<P align=center>");




            if (JuridicaPapel.IsLocalDeTrabalho(juridica.IdJuridicaPapel.Id))
            {
                //////////////////////////////////////////////////////////////////
                ////////                 Juridica Pai
                //////////////////////////////////////////////////////////////////

                if (juridica.IdJuridicaPai.mirrorOld == null)
                    juridica.IdJuridicaPai.Find();

                IPessoa iPessoaPai;

                AlteracaoCadastral alteracaoCadastralPai = juridica.IdJuridicaPai.GetAlteracaoCadastral(ref dataDocumento);


                iPessoaPai = juridica;


                Juridica iJuridica2;
                IJuridica iJuridica3;
                IEndereco iEndereco2;

                iJuridica2 = new Juridica(juridica.IdJuridicaPai.ToString());

                //GetAlteracaoCadastral(dataDocumento, iJuridica2, out iPessoaPai, out iJuridica3, out iEndereco2);

                Pessoa pessoa2;
                pessoa2 = new Pessoa();
                pessoa2.Find(juridica.Id);



                str.Append(@"<FONT color=""#000080"" size=5><B>" + iPessoaPai.NomeCompleto + "</B></FONT>");
                str.Append("<BR>");
                str.Append(@"<FONT color=""#000080"" size=3><B>" + pessoa2.GetEndereco().IdTipoLogradouro.ToString() + " " + pessoa2.GetEndereco().Logradouro.ToString() + "  " + pessoa2.GetEndereco().Numero.ToString() + "   " + pessoa2.GetEndereco().Complemento.ToString() + "</B></FONT>");
                str.Append("<BR>");
                str.Append(@"<FONT color=""#000080"" size=3><B>" + pessoa2.GetEndereco().Bairro.ToString() + "  -  " + pessoa2.GetEndereco().Municipio.ToString() + "  -  " + pessoa2.GetEndereco().Uf.ToString() + "</B></FONT>");
                str.Append("<BR>");
                //str.Append("<BR>&nbsp");
            }
            else
            {
                str.Append(@"<FONT color=""#000080"" size=3><B>" + iPessoa.NomeCompleto + "</B></FONT>");
                //str.Append("<BR>&nbsp<BR>");
                str.Append("<BR>&nbsp");
            }


            if (iJuridica.IdCNAE.Id != 0)
            {
                str.Append(@"<FONT color=""#000080"" size=3>" + "ATIVIDADES - " + iJuridica.IdCNAE.ToString() + "</FONT>");
                //str.Append("<BR>&nbsp<BR>");
                str.Append("<BR>&nbsp");
                str.Append(@"<FONT color=""#000080"" size=3>" + "CNAE" + "&nbsp;" + iJuridica.IdCNAE.Codigo + "</FONT>");

                CNAE cnae = juridica.GetCnaeSesmt();

                if (cnae.Id != 0 && cnae.GrauRisco != 0)
                {
                    str.Append("&nbsp&nbsp;&nbsp;");
                    //str.Append("<BR>&nbsp<BR>");
                    str.Append(@"<FONT color=""#000080"" size=3>Grau de Risco" + "&nbsp;&nbsp;&nbsp;" + cnae.GrauRisco.ToString() + "</FONT>");
                }
            }

            str.Append("<BR>&nbsp");

            if (juridica.CEI != null && juridica.CEI != "")
            {
                str.Append(@"<FONT color=""#000080"" size=3>" + "CNPJ / CEI " + "&nbsp;&nbsp;&nbsp;" + Juridica.FormatarCnpj(iPessoa.NomeCodigo) + " / " + juridica.CEI + "&nbsp;&nbsp;&nbsp;" + "Empregados " + ColaboradoresExpostos.ToString() + "</B></FONT>");
            }
            else
            {
                str.Append(@"<FONT color=""#000080"" size=3>" + "CNPJ" + "&nbsp;&nbsp;&nbsp;" + Juridica.FormatarCnpj(iPessoa.NomeCodigo) + "&nbsp;&nbsp;&nbsp;" + "Empregados " + ColaboradoresExpostos.ToString() + "</B></FONT>");
            }
            str.Append("<BR>&nbsp");
            str.Append("<BR>&nbsp");



            str.Append("</P>");

            return str.ToString();
        }




        private static string GetDadosEmpresa(DateTime dataDocumento, Juridica juridica, int ColaboresExpostos)
        {
            //////////////////////////////////////////////////////////////////
            ////////                    Juridica 
            //////////////////////////////////////////////////////////////////
            if (juridica.mirrorOld == null)
                juridica.Find();

            IPessoa iPessoa;
            IJuridica iJuridica;
            IEndereco iEndereco;

            GetAlteracaoCadastral(dataDocumento, juridica, out iPessoa, out iJuridica, out iEndereco);

            //////////////////////////////////////////////////////////////////
            ////////                    HTML 
            //////////////////////////////////////////////////////////////////

            StringBuilder str = new StringBuilder();

            str.Append(@"<P align=center>");

            if (JuridicaPapel.IsLocalDeTrabalho(juridica.IdJuridicaPapel.Id))
            {
                //////////////////////////////////////////////////////////////////
                ////////                 Juridica Pai
                //////////////////////////////////////////////////////////////////

                if (juridica.IdJuridicaPai.mirrorOld == null)
                    juridica.IdJuridicaPai.Find();

                IPessoa iPessoaPai;

                AlteracaoCadastral alteracaoCadastralPai = juridica.IdJuridicaPai.GetAlteracaoCadastral(ref dataDocumento);

                if (alteracaoCadastralPai.Id == 0)
                    iPessoaPai = juridica.IdJuridicaPai;
                else
                    iPessoaPai = alteracaoCadastralPai;

                Juridica iJuridica2;
                //IJuridica iJuridica3;
                //IEndereco iEndereco2;

                iJuridica2 = new Juridica(juridica.IdJuridicaPai.ToString());

                //GetAlteracaoCadastral(dataDocumento, iJuridica2, out iPessoaPai, out iJuridica3, out iEndereco2);

                Pessoa pessoa2;
                pessoa2 = new Pessoa();
                pessoa2.Find(juridica.IdJuridicaPai._IdJuridica);



                str.Append(@"<FONT color=""#000080"" size=5><B>" + iPessoaPai.NomeCompleto + "</B></FONT>");
                str.Append("<BR>");
                str.Append(@"<FONT color=""#000080"" size=3><B>" + pessoa2.GetEndereco().IdTipoLogradouro.ToString() + " " + pessoa2.GetEndereco().Logradouro.ToString() + "  " + pessoa2.GetEndereco().Numero.ToString() + "   " + pessoa2.GetEndereco().Complemento.ToString() + "</B></FONT>");
                str.Append("<BR>");
                str.Append(@"<FONT color=""#000080"" size=3><B>" + pessoa2.GetEndereco().Bairro.ToString() + "  -  " + pessoa2.GetEndereco().Municipio.ToString() + "  -  " + pessoa2.GetEndereco().Uf.ToString() + "</B></FONT>");
                str.Append("<BR>");

                str.Append(@"<FONT color=""#000080"" size=4>" + JuridicaPapel.GetDescricaoLocaldeTrabalho(juridica.IdJuridicaPapel.Id) + "</FONT>");
                str.Append("<BR>&nbsp<BR>");
                //str.Append("<BR>&nbsp");
            }
            else
            {
                System.Globalization.CultureInfo ptBr3 = new System.Globalization.CultureInfo("pt-Br");

                Identificacao_Historico rInd2 = new Identificacao_Historico();
                rInd2.Find(" IdPessoa = " + juridica.Id + " and  convert( smalldatetime, '" + dataDocumento.ToString("dd/MM/yyyy", ptBr3) + "',103 ) between Inicio_Vigencia and Termino_Vigencia ");

                if (rInd2.Id != 0)
                {
                    str.Append(@"<FONT color=""#000080"" size=3><B>" + rInd2.NomeCompleto + "</B></FONT>");
                }
                else
                {
                    str.Append(@"<FONT color=""#000080"" size=3><B>" + iPessoa.NomeCompleto + "</B></FONT>");
                }


                //str.Append("<BR>&nbsp<BR>");
                str.Append("<BR>&nbsp");
            }

            //carimbo ilitera

            System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");

            Endereco_Historico rEnd = new Endereco_Historico();
            rEnd.Find(" IdPessoa = " + juridica.Id + " and  convert( smalldatetime, '" + dataDocumento.ToString("dd/MM/yyyy", ptBr) + "',103 ) between Inicio_Vigencia and Termino_Vigencia ");

            if (rEnd.Id != 0)
            {
                StringBuilder str2 = new StringBuilder();

                TipoLogradouro rTp = new TipoLogradouro();
                rTp.Find(rEnd.IdTipoLogradouro);

                if (rTp.Id != 0)
                {
                    str2.Append(rTp.NomeAbreviado);
                }

                str2.Append(" " + rEnd.Logradouro);

                if (rEnd.Numero != string.Empty)
                    str2.Append(" " + rEnd.Numero);

                if (rEnd.Complemento != string.Empty && rEnd.Complemento.ToUpper() != "NULL")
                    str2.Append(" " + rEnd.Complemento);

                str.Append(@"<FONT color=""#000080"" size=3>" + str2 + "</FONT>");
                //str.Append("<BR>&nbsp<BR>");
                str.Append("&nbsp&nbsp&nbsp");
                str.Append(@"<FONT color=""#000080"" size=3>" + "CEP" + "&nbsp;&nbsp;&nbsp;" + rEnd.Cep + "&nbsp;&nbsp;&nbsp;" + rEnd.Bairro + "&nbsp;&nbsp;&nbsp;" + rEnd.Municipio + "&nbsp;&nbsp;&nbsp;" + rEnd.Uf + "</FONT>");

            }
            else
            {
                str.Append(@"<FONT color=""#000080"" size=3>" + Endereco.GetEndereco(iEndereco) + "</FONT>");
                //str.Append("<BR>&nbsp<BR>");
                str.Append("&nbsp&nbsp&nbsp");
                str.Append(@"<FONT color=""#000080"" size=3>" + "CEP" + "&nbsp;&nbsp;&nbsp;" + iEndereco.Cep + "&nbsp;&nbsp;&nbsp;" + iEndereco.Bairro + "&nbsp;&nbsp;&nbsp;" + Endereco.GetCidade(iEndereco) + "&nbsp;&nbsp;&nbsp;" + Endereco.GetEstado(iEndereco) + "</FONT>");
                //str.Append("<BR>&nbsp<BR>");                
            }

            str.Append("<BR>&nbsp");




            System.Globalization.CultureInfo ptBr2 = new System.Globalization.CultureInfo("pt-Br");

            Identificacao_Historico rInd = new Identificacao_Historico();
            rInd.Find(" IdPessoa = " + juridica.Id + " and  convert( smalldatetime, '" + dataDocumento.ToString("dd/MM/yyyy", ptBr2) + "',103 ) between Inicio_Vigencia and Termino_Vigencia ");

            if (rInd.Id != 0)
            {

                if (juridica.CEI != null && juridica.CEI != "")
                {
                    str.Append(@"<FONT color=""#000080"" size=3>" + "CNPJ/CEI" + "&nbsp;&nbsp;&nbsp;" + Juridica.FormatarCnpj(rInd.CNPJ) + " / " + juridica.CEI + "&nbsp;&nbsp;&nbsp;" + "Empregados " + ColaboresExpostos.ToString() + "</B></FONT>");
                }
                else
                {
                    str.Append(@"<FONT color=""#000080"" size=3>" + "CNPJ" + "&nbsp;&nbsp;&nbsp;" + Juridica.FormatarCnpj(rInd.CNPJ) + "&nbsp;&nbsp;&nbsp;" + "Empregados " + ColaboresExpostos.ToString() + "</B></FONT>");
                }
                str.Append("<BR>&nbsp");
                str.Append("<BR>&nbsp");


                //if (JuridicaPapel.IsLocalDeTrabalho(juridica.IdJuridicaPapel.Id))
                //{
                //    str.Append(@"<FONT color=""#000080"" size=4>" + JuridicaPapel.GetDescricaoLocaldeTrabalho(juridica.IdJuridicaPapel.Id, rInd.NomeCompleto.Trim()) + "</FONT>");
                //    str.Append("<BR>&nbsp");

                //}
            }
            else
            {

                if (juridica.CEI != null && juridica.CEI != "")
                {
                    str.Append(@"<FONT color=""#000080"" size=3>" + "CNPJ/CEI" + "&nbsp;&nbsp;&nbsp;" + Juridica.FormatarCnpj(iPessoa.NomeCodigo) + " / " + juridica.CEI + "&nbsp;&nbsp;&nbsp;" + "Empregados " + ColaboresExpostos.ToString() + "</B></FONT>");
                }
                else
                {
                    str.Append(@"<FONT color=""#000080"" size=3>" + "CNPJ" + "&nbsp;&nbsp;&nbsp;" + Juridica.FormatarCnpj(iPessoa.NomeCodigo) + "&nbsp;&nbsp;&nbsp;" + "Empregados " + ColaboresExpostos.ToString() + "</B></FONT>");
                }
                str.Append("<BR>&nbsp");
                str.Append("<BR>&nbsp");


                //if (JuridicaPapel.IsLocalDeTrabalho(juridica.IdJuridicaPapel.Id))
                //{
                //    str.Append(@"<FONT color=""#000080"" size=4>" + JuridicaPapel.GetDescricaoLocaldeTrabalho(juridica.IdJuridicaPapel.Id, juridica.NomeCompleto.Trim()) + "</FONT>");
                //    str.Append("<BR>&nbsp");

                //}

            }



        


            //str.Append("<BR>&nbsp<BR>");
            str.Append("<BR>&nbsp");

            




            if (iJuridica.IdCNAE.Id != 0)
            {
                str.Append(@"<FONT color=""#000080"" size=3>" + "ATIVIDADES - " + iJuridica.IdCNAE.ToString() + "</FONT>");
                //str.Append("<BR>&nbsp<BR>");
                str.Append("<BR>&nbsp");
                str.Append(@"<FONT color=""#000080"" size=3>" + "CNAE" + "&nbsp;" + iJuridica.IdCNAE.Codigo + "</FONT>");
                
                CNAE cnae = juridica.GetCnaeSesmt();

                if (cnae.Id != 0 && cnae.GrauRisco != 0)
                {
                    str.Append("&nbsp;&nbsp;&nbsp;");
                    //str.Append("<BR>&nbsp<BR>");
                    str.Append(@"<FONT color=""#000080"" size=3>Grau de Risco" + "&nbsp;" + cnae.GrauRisco.ToString() + "</FONT>");
                }
            }

            str.Append("</P>");

            return str.ToString();
        }



        private static string GetDadosEmpresa2(DateTime dataDocumento, Juridica juridica, int ColaboradoresExpostos)
        {
            //////////////////////////////////////////////////////////////////
            ////////                    Juridica 
            //////////////////////////////////////////////////////////////////
            if (juridica.mirrorOld == null)
                juridica.Find();

            IPessoa iPessoa;
            IJuridica iJuridica;
            IEndereco iEndereco;

            GetAlteracaoCadastral(dataDocumento, juridica, out iPessoa, out iJuridica, out iEndereco);

            //////////////////////////////////////////////////////////////////
            ////////                    HTML 
            //////////////////////////////////////////////////////////////////

            StringBuilder str = new StringBuilder();

            str.Append(@"<P align=center>");




            if (JuridicaPapel.IsLocalDeTrabalho(juridica.IdJuridicaPapel.Id))
            {
                //////////////////////////////////////////////////////////////////
                ////////                 Juridica Pai
                //////////////////////////////////////////////////////////////////

                if (juridica.IdJuridicaPai.mirrorOld == null)
                    juridica.IdJuridicaPai.Find();

                IPessoa iPessoaPai;

                AlteracaoCadastral alteracaoCadastralPai = juridica.IdJuridicaPai.GetAlteracaoCadastral(ref dataDocumento);

                if (alteracaoCadastralPai.Id == 0)
                    iPessoaPai = juridica.IdJuridicaPai;
                else
                    iPessoaPai = alteracaoCadastralPai;


                Juridica iJuridica2;
                //IJuridica iJuridica3;
                //IEndereco iEndereco2;

                iJuridica2 = new Juridica(juridica.IdJuridicaPai.ToString());

                //GetAlteracaoCadastral(dataDocumento, iJuridica2, out iPessoaPai, out iJuridica3, out iEndereco2);

                Pessoa pessoa2;
                pessoa2 = new Pessoa();
                pessoa2.Find(juridica.IdJuridicaPai._IdJuridica);



                str.Append(@"<FONT color=""#000080"" size=5><B>" + iPessoaPai.NomeCompleto + "</B></FONT>");
                str.Append("<BR>");
                str.Append(@"<FONT color=""#000080"" size=3><B>" + pessoa2.GetEndereco().IdTipoLogradouro.ToString() + " " + pessoa2.GetEndereco().Logradouro.ToString() + "  " + pessoa2.GetEndereco().Numero.ToString() + "   " + pessoa2.GetEndereco().Complemento.ToString() + "</B></FONT>");
                str.Append("<BR>");
                str.Append(@"<FONT color=""#000080"" size=3><B>" + pessoa2.GetEndereco().Bairro.ToString() + "  -  " + pessoa2.GetEndereco().Municipio.ToString() + "  -  " + pessoa2.GetEndereco().Uf.ToString() + "</B></FONT>");
                str.Append("<BR>");
                if (iJuridica2.CEI != null && iJuridica2.CEI != "")
                {
                    str.Append(@"<FONT color=""#000080"" size=3><B>CNPJ/CEI: " + iPessoaPai.NomeCodigo.ToString() + " / " + iJuridica2.CEI + "</B></FONT>");
                }
                else
                {
                    str.Append(@"<FONT color=""#000080"" size=3><B>CNPJ: " + iPessoaPai.NomeCodigo.ToString() + "</B></FONT>");
                }

                str.Append("<BR>");

                //str.Append("<BR>&nbsp");
            }
            else
            {
                System.Globalization.CultureInfo ptBr3 = new System.Globalization.CultureInfo("pt-Br");

                Identificacao_Historico rInd2 = new Identificacao_Historico();
                rInd2.Find(" IdPessoa = " + juridica.Id + " and  convert( smalldatetime, '" + dataDocumento.ToString("dd/MM/yyyy", ptBr3) + "',103 ) between Inicio_Vigencia and Termino_Vigencia ");

                if (rInd2.Id != 0)
                {
                    str.Append(@"<FONT color=""#000080"" size=3><B>" + rInd2.NomeCompleto + "</B></FONT>");
                }
                else
                {
                    str.Append(@"<FONT color=""#000080"" size=3><B>" + iPessoa.NomeCompleto + "</B></FONT>");
                }

                //str.Append("<BR>&nbsp<BR>");
                str.Append("<BR>&nbsp");
            }


            if (JuridicaPapel.IsLocalDeTrabalho(juridica.IdJuridicaPapel.Id))
            {
                System.Globalization.CultureInfo ptBr2 = new System.Globalization.CultureInfo("pt-Br");

                Identificacao_Historico rInd = new Identificacao_Historico();
                rInd.Find(" IdPessoa = " + juridica.Id + " and  convert( smalldatetime, '" + dataDocumento.ToString("dd/MM/yyyy", ptBr2) + "',103 ) between Inicio_Vigencia and Termino_Vigencia ");

                if (rInd.Id != 0)
                {
                    if (JuridicaPapel.IsLocalDeTrabalho(juridica.IdJuridicaPapel.Id))
                    {
                        str.Append("<BR>");
                        str.Append(@"<FONT color=""#000080"" size=4>" + JuridicaPapel.GetDescricaoLocaldeTrabalho(juridica.IdJuridicaPapel.Id, rInd.NomeCompleto.Trim()) + "</FONT>");
                        str.Append("<BR>&nbsp");
                    }
                }
                else
                {
                    if (JuridicaPapel.IsLocalDeTrabalho(juridica.IdJuridicaPapel.Id))
                    {
                        str.Append("<BR>");
                        str.Append(@"<FONT color=""#000080"" size=4>" + JuridicaPapel.GetDescricaoLocaldeTrabalho(juridica.IdJuridicaPapel.Id, juridica.NomeCompleto.Trim()) + "</FONT>");
                        str.Append("<BR>&nbsp");

                    }
                }

            }

            //carimbo ilitera
            System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");

            Endereco_Historico rEnd = new Endereco_Historico();
            rEnd.Find(" IdPessoa = " + juridica.Id + " and  convert( smalldatetime, '" + dataDocumento.ToString("dd/MM/yyyy", ptBr) + "',103 ) between Inicio_Vigencia and Termino_Vigencia ");

            if (rEnd.Id != 0)
            {
                StringBuilder str2 = new StringBuilder();

                TipoLogradouro rTp = new TipoLogradouro();
                rTp.Find(rEnd.IdTipoLogradouro);

                if (rTp.Id != 0)
                {
                    str2.Append(rTp.NomeAbreviado);
                }

                str2.Append(" " + rEnd.Logradouro);

                if (rEnd.Numero != string.Empty)
                    str2.Append(" " + rEnd.Numero);

                if (rEnd.Complemento != string.Empty && rEnd.Complemento.ToUpper() != "NULL")
                    str2.Append(" " + rEnd.Complemento);

                str.Append(@"<FONT color=""#000080"" size=3>" + str2 + "</FONT>");
                //str.Append("<BR>&nbsp<BR>");
                str.Append("&nbsp&nbsp&nbsp");
                str.Append(@"<FONT color=""#000080"" size=3>" + "CEP" + "&nbsp;&nbsp;&nbsp;" + rEnd.Cep + "&nbsp;&nbsp;&nbsp;" + rEnd.Bairro + "&nbsp;&nbsp;&nbsp;" + rEnd.Municipio + "&nbsp;&nbsp;&nbsp;" + rEnd.Uf + "</FONT>");

            }
            else
            {

                str.Append(@"<FONT color=""#000080"" size=3>" + Endereco.GetEndereco(iEndereco) + "</FONT>");
                //str.Append("<BR>&nbsp<BR>");
                str.Append("&nbsp&nbsp&nbsp");
                str.Append(@"<FONT color=""#000080"" size=3>" + "CEP" + "&nbsp;&nbsp;&nbsp;" + iEndereco.Cep + "&nbsp;&nbsp;&nbsp;" + iEndereco.Bairro + "&nbsp;&nbsp;&nbsp;" + Endereco.GetCidade(iEndereco) + "&nbsp;&nbsp;&nbsp;" + Endereco.GetEstado(iEndereco) + "</FONT>");
                //str.Append("<BR>&nbsp<BR>");
            }


            //str.Append(@"<FONT color=""#000080"" size=3><B>" + "CNPJ: " + Juridica.FormatarCnpj(iPessoa.NomeCodigo) + "</B></FONT>");

            //str.Append("<BR>&nbsp<BR>");


            if (iJuridica.IdCNAE.Id != 0)
            {
                str.Append("<BR>");
                str.Append(@"<FONT color=""#000080"" size=3>" + "ATIVIDADES - " + iJuridica.IdCNAE.ToString() + "</FONT>");
                //str.Append("<BR>&nbsp<BR>");
                str.Append("<BR>&nbsp");
                str.Append(@"<FONT color=""#000080"" size=3>" + "CNAE" + "&nbsp;" + iJuridica.IdCNAE.Codigo + "</FONT>");

                CNAE cnae = juridica.GetCnaeSesmt();

                if (cnae.Id != 0 && cnae.GrauRisco != 0)
                {
                    str.Append("&nbsp;&nbsp;&nbsp;");
                    //str.Append("<BR>&nbsp<BR>");
                    str.Append(@"<FONT color=""#000080"" size=3>Grau de Risco" + "&nbsp;" + cnae.GrauRisco.ToString() + "</FONT>");
                }
            }

            str.Append("<BR>&nbsp");
            //str.Append(@"<FONT color=""#000080"" size=3>" + "CNPJ" + "&nbsp;&nbsp;&nbsp;" + Juridica.FormatarCnpj(iPessoa.NomeCodigo) + "&nbsp;&nbsp;&nbsp;" + "Empregados " + ColaboradoresExpostos.ToString() + "</B></FONT>");
            if (juridica.CEI != null && juridica.CEI != "")
            {
                str.Append(@"<FONT color=""#000080"" size=3>" + "CNPJ/CEI" + "&nbsp;&nbsp;&nbsp;" + Juridica.FormatarCnpj(iPessoa.NomeCodigo) + " / " + juridica.CEI + "&nbsp;&nbsp;&nbsp;" + "Empregados " + ColaboradoresExpostos.ToString() + "</B></FONT>");
            }
            else
            {
                str.Append(@"<FONT color=""#000080"" size=3>" + "CNPJ" + "&nbsp;&nbsp;&nbsp;" + Juridica.FormatarCnpj(iPessoa.NomeCodigo) + "&nbsp;&nbsp;&nbsp;" + "Empregados " + ColaboradoresExpostos.ToString() + "</B></FONT>");
            }

            str.Append("<BR>&nbsp");
            str.Append("<BR>&nbsp");






            str.Append("</P>");

            return str.ToString();
        }


        private static string GetDadosEmpresa(DateTime dataDocumento, Juridica juridica)
        {
            //////////////////////////////////////////////////////////////////
            ////////                    Juridica 
            //////////////////////////////////////////////////////////////////
            if (juridica.mirrorOld == null)
                juridica.Find();

            IPessoa iPessoa;
            IJuridica iJuridica;
            IEndereco iEndereco;

            GetAlteracaoCadastral(dataDocumento, juridica, out iPessoa, out iJuridica, out iEndereco);

            //////////////////////////////////////////////////////////////////
            ////////                    HTML 
            //////////////////////////////////////////////////////////////////

            StringBuilder str = new StringBuilder();

            str.Append(@"<P align=center>");

            if (JuridicaPapel.IsLocalDeTrabalho(juridica.IdJuridicaPapel.Id))
            {
                //////////////////////////////////////////////////////////////////
                ////////                 Juridica Pai
                //////////////////////////////////////////////////////////////////

                if (juridica.IdJuridicaPai.mirrorOld == null)
                    juridica.IdJuridicaPai.Find();

                IPessoa iPessoaPai;

                AlteracaoCadastral alteracaoCadastralPai = juridica.IdJuridicaPai.GetAlteracaoCadastral(ref dataDocumento);

                if (alteracaoCadastralPai.Id == 0)
                    iPessoaPai = juridica.IdJuridicaPai;
                else
                    iPessoaPai = alteracaoCadastralPai;


                Juridica iJuridica2;
                //IJuridica iJuridica3;
                //IEndereco iEndereco2;

                iJuridica2 = new Juridica(juridica.IdJuridicaPai.ToString());

                //GetAlteracaoCadastral(dataDocumento, iJuridica2, out iPessoaPai, out iJuridica3, out iEndereco2);

                Pessoa pessoa2;
                pessoa2 = new Pessoa();
                pessoa2.Find(juridica.IdJuridicaPai._IdJuridica);

                

                str.Append(@"<FONT color=""#000080"" size=5><B>" + iPessoaPai.NomeCompleto + "</B></FONT>");
                str.Append("<BR>");
                str.Append(@"<FONT color=""#000080"" size=3><B>" + pessoa2.GetEndereco().IdTipoLogradouro.ToString() + " " + pessoa2.GetEndereco().Logradouro.ToString() + "  " + pessoa2.GetEndereco().Numero.ToString() + "   " + pessoa2.GetEndereco().Complemento.ToString() + "</B></FONT>");
                str.Append("<BR>");
                str.Append(@"<FONT color=""#000080"" size=3><B>" + pessoa2.GetEndereco().Bairro.ToString() + "  -  " + pessoa2.GetEndereco().Municipio.ToString() + "  -  " + pessoa2.GetEndereco().Uf.ToString() + "</B></FONT>");
                str.Append("<BR>");
                //str.Append("<BR>&nbsp");
                //str.Append(@"<FONT color=""#000080"" size=5><B>" + iPessoaPai.NomeCompleto + "</B></FONT>");
                //str.Append("<BR>&nbsp<BR>");
                //str.Append(@"<FONT color=""#000080"" size=4>" + JuridicaPapel.GetDescricaoLocaldeTrabalho(juridica.IdJuridicaPapel.Id) + "</FONT>");
                //str.Append("<BR>&nbsp<BR>");
                //str.Append("<BR>&nbsp");
            }
            else
            {
                System.Globalization.CultureInfo ptBr3 = new System.Globalization.CultureInfo("pt-Br");

                Identificacao_Historico rInd2 = new Identificacao_Historico();
                rInd2.Find(" IdPessoa = " + juridica.Id + " and  convert( smalldatetime, '" + dataDocumento.ToString("dd/MM/yyyy", ptBr3) + "',103 ) between Inicio_Vigencia and Termino_Vigencia ");

                if (rInd2.Id != 0)
                {
                    str.Append(@"<FONT color=""#000080"" size=3><B>" + rInd2.NomeCompleto + "</B></FONT>");
                }
                else
                {
                    str.Append(@"<FONT color=""#000080"" size=3><B>" + iPessoa.NomeCompleto + "</B></FONT>");
                }
                //str.Append("<BR>&nbsp<BR>");
                str.Append("<BR>&nbsp");
            }

            //carimbo ilitera
            System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");

            Endereco_Historico rEnd = new Endereco_Historico();
            rEnd.Find(" IdPessoa = " + juridica.Id + " and  convert( smalldatetime, '" + dataDocumento.ToString("dd/MM/yyyy", ptBr) + "',103 ) between Inicio_Vigencia and Termino_Vigencia ");

            if (rEnd.Id != 0)
            {
                StringBuilder str2 = new StringBuilder();

                TipoLogradouro rTp = new TipoLogradouro();
                rTp.Find(rEnd.IdTipoLogradouro);

                if (rTp.Id != 0)
                {
                    str2.Append(rTp.NomeAbreviado);
                }

                str2.Append(" " + rEnd.Logradouro);

                if (rEnd.Numero != string.Empty)
                    str2.Append(" " + rEnd.Numero);

                if (rEnd.Complemento != string.Empty && rEnd.Complemento.ToUpper() != "NULL")
                    str2.Append(" " + rEnd.Complemento);

                str.Append(@"<FONT color=""#000080"" size=3>" + str2 + "</FONT>");
                //str.Append("<BR>&nbsp<BR>");
                str.Append("&nbsp&nbsp&nbsp");
                str.Append(@"<FONT color=""#000080"" size=3>" + "CEP" + "&nbsp;&nbsp;&nbsp;" + rEnd.Cep + "&nbsp;&nbsp;&nbsp;" + rEnd.Bairro + "&nbsp;&nbsp;&nbsp;" + rEnd.Municipio + "&nbsp;&nbsp;&nbsp;" + rEnd.Uf + "</FONT>");

            }
            else
            {                
                str.Append(@"<FONT color=""#000080"" size=3>" + Endereco.GetEndereco(iEndereco) + "</FONT>");
                //str.Append("<BR>&nbsp<BR>");
                str.Append("&nbsp&nbsp&nbsp");
                str.Append(@"<FONT color=""#000080"" size=3>" + "CEP" + "&nbsp;&nbsp;&nbsp;" + iEndereco.Cep + "&nbsp;&nbsp;&nbsp;" + iEndereco.Bairro + "&nbsp;&nbsp;&nbsp;" + Endereco.GetCidade(iEndereco) + "&nbsp;&nbsp;&nbsp;" + Endereco.GetEstado(iEndereco) + "</FONT>");
                //str.Append("<BR>&nbsp<BR>");                
            }

            str.Append("<BR>&nbsp");

            //str.Append(@"<FONT color=""#000080"" size=3><B>" + "CNPJ: " + Juridica.FormatarCnpj(iPessoa.NomeCodigo) + "</B></FONT>");
            //str.Append(@"<FONT color=""#000080"" size=3>" + "CNPJ" + "&nbsp;&nbsp;&nbsp;" + Juridica.FormatarCnpj(iPessoa.NomeCodigo) + "</B></FONT>");


            System.Globalization.CultureInfo ptBr2 = new System.Globalization.CultureInfo("pt-Br");

            Identificacao_Historico rInd3 = new Identificacao_Historico();
            rInd3.Find(" IdPessoa = " + juridica.Id + " and  convert( smalldatetime, '" + dataDocumento.ToString("dd/MM/yyyy", ptBr2) + "',103 ) between Inicio_Vigencia and Termino_Vigencia ");

            if (rInd3.Id != 0)
            {
                str.Append(@"<FONT color=""#000080"" size=3>" + "CNPJ" + "&nbsp;&nbsp;&nbsp;" + Juridica.FormatarCnpj(rInd3.CNPJ) + "&nbsp;&nbsp;&nbsp;" + "&nbsp;&nbsp;&nbsp;" + "</B></FONT>");  //+ "Empregados " + juridica.QtdEmpregados.ToString() + "</B></FONT>");
            }
            else
            {
                str.Append(@"<FONT color=""#000080"" size=3>" + "CNPJ" + "&nbsp;&nbsp;&nbsp;" + Juridica.FormatarCnpj(iPessoa.NomeCodigo) + "&nbsp;&nbsp;&nbsp;" + "&nbsp;&nbsp;&nbsp;" + "</B></FONT>");  //+ "Empregados " + juridica.QtdEmpregados.ToString() + "</B></FONT>");
            }



            //str.Append("<BR>&nbsp<BR>");
            str.Append("<BR>&nbsp");

            if (iJuridica.IdCNAE.Id != 0)
            {
                str.Append(@"<FONT color=""#000080"" size=3>" + "ATIVIDADES - " + iJuridica.IdCNAE.ToString() + "</FONT>");
                //str.Append("<BR>&nbsp<BR>");
                str.Append("<BR>&nbsp");
                str.Append(@"<FONT color=""#000080"" size=3>" + "CNAE" + "&nbsp;" + iJuridica.IdCNAE.Codigo + "</FONT>");

                CNAE cnae = juridica.GetCnaeSesmt();

                if (cnae.Id != 0 && cnae.GrauRisco != 0)
                {
                    str.Append("&nbsp&nbsp;&nbsp;");
                    //str.Append("<BR>&nbsp<BR>");
                    str.Append(@"<FONT color=""#000080"" size=3>Grau de Risco" + "&nbsp;" + cnae.GrauRisco.ToString() + "</FONT>");
                }
            }

            str.Append("</P>");

            return str.ToString();
        }

        private static string GetDadosEmpresa_Obra_Prajna(DateTime dataDocumento, Juridica juridica, Int32 xNumColaboradores, bool xInibirDadosMatriz_Laudos)
        {
            //////////////////////////////////////////////////////////////////
            ////////                    Juridica 
            //////////////////////////////////////////////////////////////////
            if (juridica.mirrorOld == null)
                juridica.Find();

            IPessoa iPessoa;
            IJuridica iJuridica;
            IEndereco iEndereco;

            GetAlteracaoCadastral(dataDocumento, juridica, out iPessoa, out iJuridica, out iEndereco);

            //////////////////////////////////////////////////////////////////
            ////////                    HTML 
            //////////////////////////////////////////////////////////////////

            StringBuilder str = new StringBuilder();

            str.Append(@"<P align=center>");



            if (JuridicaPapel.IsLocalDeTrabalho(juridica.IdJuridicaPapel.Id))
            {
                //////////////////////////////////////////////////////////////////
                ////////                 Juridica Pai
                //////////////////////////////////////////////////////////////////

                if (juridica.IdJuridicaPai.mirrorOld == null)
                    juridica.IdJuridicaPai.Find();

                IPessoa iPessoaPai;

                AlteracaoCadastral alteracaoCadastralPai = juridica.IdJuridicaPai.GetAlteracaoCadastral(ref dataDocumento);

                if (alteracaoCadastralPai.Id == 0)
                    iPessoaPai = juridica.IdJuridicaPai;
                else
                    iPessoaPai = alteracaoCadastralPai;

                if (xInibirDadosMatriz_Laudos == false)
                {
                    str.Append(@"<FONT color=""#000080"" size=5><B>" + iPessoaPai.NomeCompleto + "</B></FONT>");
                }

                str.Append("<BR>&nbsp<BR>");

                //str.Append("<BR>&nbsp");
            }
            else
            {
                System.Globalization.CultureInfo ptBr3 = new System.Globalization.CultureInfo("pt-Br");

                Identificacao_Historico rInd2 = new Identificacao_Historico();
                rInd2.Find(" IdPessoa = " + juridica.Id + " and  convert( smalldatetime, '" + dataDocumento.ToString("dd/MM/yyyy", ptBr3) + "',103 ) between Inicio_Vigencia and Termino_Vigencia ");

                if (rInd2.Id != 0)
                {
                    str.Append(@"<FONT color=""#000080"" size=3><B>" + rInd2.NomeCompleto + "</B></FONT>");
                }
                else
                {
                    str.Append(@"<FONT color=""#000080"" size=3><B>" + iPessoa.NomeCompleto + "</B></FONT>");
                }

                //str.Append("<BR>&nbsp<BR>");
                str.Append("<BR>&nbsp");
            }


            str.Append("<BR>&nbsp<BR>");


            System.Globalization.CultureInfo ptBr2 = new System.Globalization.CultureInfo("pt-Br");

            Identificacao_Historico rInd = new Identificacao_Historico();
            rInd.Find(" IdPessoa = " + juridica.Id + " and  convert( smalldatetime, '" + dataDocumento.ToString("dd/MM/yyyy", ptBr2) + "',103 ) between Inicio_Vigencia and Termino_Vigencia ");

            if (rInd.Id != 0)
            {
                if (JuridicaPapel.IsLocalDeTrabalho(juridica.IdJuridicaPapel.Id))
                {
                    str.Append(@"<FONT color=""#000080"" size=4>" + JuridicaPapel.GetDescricaoLocaldeTrabalho(juridica.IdJuridicaPapel.Id, rInd.NomeCompleto.Trim()) + "</FONT>");
                    str.Append("<BR>&nbsp");
                }
            }
            else
            {
                if (JuridicaPapel.IsLocalDeTrabalho(juridica.IdJuridicaPapel.Id))
                {
                    str.Append(@"<FONT color=""#000080"" size=4>" + JuridicaPapel.GetDescricaoLocaldeTrabalho(juridica.IdJuridicaPapel.Id, juridica.NomeCompleto.Trim()) + "</FONT>");
                    str.Append("<BR>&nbsp");
                }
            }


            str.Append("<BR>&nbsp");

            System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");
            //carimbo ilitera
            Endereco_Historico rEnd = new Endereco_Historico();
            rEnd.Find(" IdPessoa = " + juridica.Id + " and  convert( smalldatetime, '" + dataDocumento.ToString("dd/MM/yyyy", ptBr) + "',103 ) between Inicio_Vigencia and Termino_Vigencia ");

            if (rEnd.Id != 0)
            {
                StringBuilder str2 = new StringBuilder();

                TipoLogradouro rTp = new TipoLogradouro();
                rTp.Find(rEnd.IdTipoLogradouro);

                if (rTp.Id != 0)
                {
                    str2.Append(rTp.NomeAbreviado);
                }

                str2.Append(" " + rEnd.Logradouro);

                if (rEnd.Numero != string.Empty)
                    str2.Append(" " + rEnd.Numero);

                if (rEnd.Complemento != string.Empty && rEnd.Complemento.ToUpper() != "NULL")
                    str2.Append(" " + rEnd.Complemento);

                str.Append(@"<FONT color=""#000080"" size=3>" + str2 + "</FONT>");
                //str.Append("<BR>&nbsp<BR>");
                str.Append("&nbsp&nbsp&nbsp");
                str.Append(@"<FONT color=""#000080"" size=3>" + "CEP" + "&nbsp;&nbsp;&nbsp;" + rEnd.Cep + "&nbsp;&nbsp;&nbsp;" + rEnd.Bairro + "&nbsp;&nbsp;&nbsp;" + rEnd.Municipio + "&nbsp;&nbsp;&nbsp;" + rEnd.Uf + "</FONT>");

            }
            else
            {
                str.Append(@"<FONT color=""#000080"" size=3>" + Endereco.GetEndereco(iEndereco) + "</FONT>");
                //str.Append("<BR>&nbsp<BR>");            
                str.Append("<BR>&nbsp");
                str.Append(@"<FONT color=""#000080"" size=3>" + "CEP" + "&nbsp;&nbsp;&nbsp;" + iEndereco.Cep + "&nbsp;&nbsp;&nbsp;" + iEndereco.Bairro + "&nbsp;&nbsp;&nbsp;" + Endereco.GetCidade(iEndereco) + "&nbsp;&nbsp;&nbsp;" + Endereco.GetEstado(iEndereco) + "</FONT>");
            }
            //str.Append("<BR>&nbsp<BR>");

            //str.Append(@"<FONT color=""#000080"" size=3><B>" + "CNPJ: " + Juridica.FormatarCnpj(iPessoa.NomeCodigo) + "</B></FONT>");

            //str.Append("<BR>&nbsp<BR>");

            if (xNumColaboradores > 0)
            {
                str.Append("<BR>&nbsp");
                str.Append(@"<FONT color=""#000080"" size=3>" + "Empregados " + xNumColaboradores.ToString() + "</B></FONT>");
            }


            str.Append("</P>");

            return str.ToString();
        }


        private static string GetDadosEmpresa_Obra_Prajna(DateTime dataDocumento, Juridica juridica, Int32 xNumColaboradores)
        {
            //////////////////////////////////////////////////////////////////
            ////////                    Juridica 
            //////////////////////////////////////////////////////////////////
            if (juridica.mirrorOld == null)
                juridica.Find();

            IPessoa iPessoa;
            IJuridica iJuridica;
            IEndereco iEndereco;

            GetAlteracaoCadastral(dataDocumento, juridica, out iPessoa, out iJuridica, out iEndereco);

            //////////////////////////////////////////////////////////////////
            ////////                    HTML 
            //////////////////////////////////////////////////////////////////

            StringBuilder str = new StringBuilder();

            str.Append(@"<P align=center>");



            if (JuridicaPapel.IsLocalDeTrabalho(juridica.IdJuridicaPapel.Id))
            {
                //////////////////////////////////////////////////////////////////
                ////////                 Juridica Pai
                //////////////////////////////////////////////////////////////////

                if (juridica.IdJuridicaPai.mirrorOld == null)
                    juridica.IdJuridicaPai.Find();

                IPessoa iPessoaPai;

                AlteracaoCadastral alteracaoCadastralPai = juridica.IdJuridicaPai.GetAlteracaoCadastral(ref dataDocumento);

                if (alteracaoCadastralPai.Id == 0)
                    iPessoaPai = juridica.IdJuridicaPai;
                else
                    iPessoaPai = alteracaoCadastralPai;

                str.Append(@"<FONT color=""#000080"" size=5><B>" + iPessoaPai.NomeCompleto + "</B></FONT>");
                str.Append("<BR>&nbsp<BR>");
                //str.Append("<BR>&nbsp");
            }
            else
            {
                System.Globalization.CultureInfo ptBr3 = new System.Globalization.CultureInfo("pt-Br");

                Identificacao_Historico rInd2 = new Identificacao_Historico();
                rInd2.Find(" IdPessoa = " + juridica.Id + " and  convert( smalldatetime, '" + dataDocumento.ToString("dd/MM/yyyy", ptBr3) + "',103 ) between Inicio_Vigencia and Termino_Vigencia ");

                if (rInd2.Id != 0)
                {
                    str.Append(@"<FONT color=""#000080"" size=3><B>" + rInd2.NomeCompleto + "</B></FONT>");
                }
                else
                {
                    str.Append(@"<FONT color=""#000080"" size=3><B>" + iPessoa.NomeCompleto + "</B></FONT>");
                }

                //str.Append("<BR>&nbsp<BR>");
                str.Append("<BR>&nbsp");
            }


            str.Append("<BR>&nbsp<BR>");

            System.Globalization.CultureInfo ptBr2 = new System.Globalization.CultureInfo("pt-Br");

            Identificacao_Historico rInd = new Identificacao_Historico();
            rInd.Find(" IdPessoa = " + juridica.Id + " and  convert( smalldatetime, '" + dataDocumento.ToString("dd/MM/yyyy", ptBr2) + "',103 ) between Inicio_Vigencia and Termino_Vigencia ");

            if (rInd.Id != 0)
            {
                if (JuridicaPapel.IsLocalDeTrabalho(juridica.IdJuridicaPapel.Id))
                {
                    str.Append(@"<FONT color=""#000080"" size=4>" + JuridicaPapel.GetDescricaoLocaldeTrabalho(juridica.IdJuridicaPapel.Id, rInd.NomeCompleto.Trim()) + "</FONT>");
                    str.Append("<BR>&nbsp");
                }
            }
            else
            {
                if (JuridicaPapel.IsLocalDeTrabalho(juridica.IdJuridicaPapel.Id))
                {
                    str.Append(@"<FONT color=""#000080"" size=4>" + JuridicaPapel.GetDescricaoLocaldeTrabalho(juridica.IdJuridicaPapel.Id, juridica.NomeCompleto.Trim()) + "</FONT>");
                    str.Append("<BR>&nbsp");
                }
            }



            str.Append("<BR>&nbsp");

            //carimbo ilitera
            System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");

            Endereco_Historico rEnd = new Endereco_Historico();
            rEnd.Find(" IdPessoa = " + juridica.Id + " and  convert( smalldatetime, '" + dataDocumento.ToString("dd/MM/yyyy", ptBr) + "',103 ) between Inicio_Vigencia and Termino_Vigencia ");

            if (rEnd.Id != 0)
            {
                StringBuilder str2 = new StringBuilder();

                TipoLogradouro rTp = new TipoLogradouro();
                rTp.Find(rEnd.IdTipoLogradouro);

                if (rTp.Id != 0)
                {
                    str2.Append(rTp.NomeAbreviado);
                }

                str2.Append(" " + rEnd.Logradouro);

                if (rEnd.Numero != string.Empty)
                    str2.Append(" " + rEnd.Numero);

                if (rEnd.Complemento != string.Empty && rEnd.Complemento.ToUpper() != "NULL")
                    str2.Append(" " + rEnd.Complemento);

                str.Append(@"<FONT color=""#000080"" size=3>" + str2 + "</FONT>");
                //str.Append("<BR>&nbsp<BR>");
                str.Append("&nbsp&nbsp&nbsp");
                str.Append(@"<FONT color=""#000080"" size=3>" + "CEP" + "&nbsp;&nbsp;&nbsp;" + rEnd.Cep + "&nbsp;&nbsp;&nbsp;" + rEnd.Bairro + "&nbsp;&nbsp;&nbsp;" + rEnd.Municipio + "&nbsp;&nbsp;&nbsp;" + rEnd.Uf + "</FONT>");

            }
            else
            {

                str.Append(@"<FONT color=""#000080"" size=3>" + Endereco.GetEndereco(iEndereco) + "</FONT>");
                //str.Append("<BR>&nbsp<BR>");            
                str.Append("<BR>&nbsp");
                str.Append(@"<FONT color=""#000080"" size=3>" + "CEP" + "&nbsp;&nbsp;&nbsp;" + iEndereco.Cep + "&nbsp;&nbsp;&nbsp;" + iEndereco.Bairro + "&nbsp;&nbsp;&nbsp;" + Endereco.GetCidade(iEndereco) + "&nbsp;&nbsp;&nbsp;" + Endereco.GetEstado(iEndereco) + "</FONT>");
            }

            //str.Append("<BR>&nbsp<BR>");

            //str.Append(@"<FONT color=""#000080"" size=3><B>" + "CNPJ: " + Juridica.FormatarCnpj(iPessoa.NomeCodigo) + "</B></FONT>");

            //str.Append("<BR>&nbsp<BR>");

            if (xNumColaboradores > 0)
            {
                str.Append("<BR>&nbsp");
                str.Append(@"<FONT color=""#000080"" size=3>" + "Empregados " + xNumColaboradores.ToString() + "</B></FONT>");
            }


            str.Append("</P>");

            return str.ToString();
        }


        #endregion

        #region GetDadosEmpresaContratadoEContratante

        private string GetDadosEmpresaContratadoEContratante(DateTime dataDocumento)
        {
            StringBuilder str = new StringBuilder();

            Endereco endereco = this.GetEndereco();
            Endereco enderecoPai = this.IdJuridicaPai.GetEndereco(TipoEndereco.Default);

            this.IdJuridicaPai.Find();

            str.Append(@"<P align=center>");
            str.Append(@"<FONT color=""#000080"" size=3><B>" + this.IdJuridicaPai.NomeCompleto + "</B></FONT><BR>");
            str.Append(@"<FONT color=""#000080"" size=2>" + enderecoPai.GetEndereco() + "</FONT><BR>");
            str.Append(@"<FONT color=""#000080"" size=2>" + "CEP " + enderecoPai.Cep + " " + enderecoPai.Bairro + "  " + enderecoPai.GetCidade() + "  " + enderecoPai.GetEstado() + "</FONT><BR>");
            str.Append(@"<FONT color=""#000080"" size=3><B>" + "CNPJ: " + this.IdJuridicaPai.GetCnpj() + "</B></FONT><BR>");

            if (this.IdJuridicaPai.IdCNAE.Id != 0)
            {
                str.Append(@"<FONT color=""#000080"" size=2>" + "ATIVIDADES - " + this.IdJuridicaPai.IdCNAE.ToString() + "</FONT><BR>");
                str.Append(@"<FONT color=""#000080"" size=2>" + "CNAE - " + this.IdJuridicaPai.IdCNAE.Codigo + "</FONT><BR>");
            }

            str.Append("&nbsp<BR>");
            str.Append(@"<FONT color=""#000080"" size=1>" + this.IdJuridicaPapel.GetDescricaoLocaldeTrabalho() + "</FONT><BR>");
            str.Append(@"<FONT color=""#000080"" size=4>" + this.NomeCompleto + "</FONT><BR>");
            str.Append(@"<FONT color=""#000080"" size=2>" + endereco.GetEndereco() + "</FONT><BR>");
            str.Append(@"<FONT color=""#000080"" size=2>" + "CEP " + endereco.Cep + " " + endereco.Bairro + "  " + endereco.GetCidade() + "  " + endereco.GetEstado() + "</FONT><BR>");
            str.Append(@"<FONT color=""#000080"" size=3><B>" + "CNPJ: " + this.GetCnpj() + "</B></FONT><BR>");

            if (this.IdCNAE.Id != 0)
            {
                str.Append(@"<FONT color=""#000080"" size=2>" + "ATIVIDADES - " + this.IdCNAE.ToString() + "</FONT><BR>");
                str.Append(@"<FONT color=""#000080"" size=2>" + "CNAE - " + this.IdCNAE.Codigo + "</FONT><BR>");
            }

            str.Append(@"</P>");

            return str.ToString();
        }
        #endregion

        #region GetAlteracaoCadastral

        private static void GetAlteracaoCadastral(DateTime dataDocumento,
                                            Juridica juridica,
                                            out IPessoa iPessoa,
                                            out IJuridica iJuridica,
                                            out IEndereco iEndereco)
        {
            AlteracaoCadastral alteracaoCadastral = juridica.GetAlteracaoCadastral(ref dataDocumento);

            if (alteracaoCadastral.Id == 0)
            {
                iPessoa = juridica;
                iJuridica = juridica;
                iEndereco = juridica.GetEndereco(TipoEndereco.Default);
            }
            else
            {
                iPessoa = alteracaoCadastral;
                iJuridica = alteracaoCadastral;
                iEndereco = alteracaoCadastral;
            }
        }
        #endregion

        #region GetCidade

        public string GetCidade(DateTime dataDocumento)
        {
            IPessoa iPessoa;
            IJuridica iJuridica;
            IEndereco iEndereco;

            GetAlteracaoCadastral(dataDocumento, this, out iPessoa, out iJuridica, out iEndereco);

            return Endereco.GetCidade(iEndereco);
        }
        #endregion

        #endregion

        #region DadosEmpresa HTML CIPA

        public string GetDadosEmpresaCipa(DateTime dataDocumento)
        {
            if (this.mirrorOld == null)
                this.Find();

            IPessoa iPessoa;
            IJuridica iJuridica;
            IEndereco iEndereco;

            GetDadosEmpresa(dataDocumento, out iPessoa, out iJuridica, out iEndereco);

            StringBuilder str = new StringBuilder();

            str.Append(@"<P align='center'>");
            str.Append(@"<FONT color=""#003300"" size=4>");

            if (this.IdJuridicaPapel.IsLocalDeTrabalho())
            {
                if (this.IdJuridicaPai.mirrorOld == null)
                    this.IdJuridicaPai.Find();

                IPessoa iPessoaPai;

                AlteracaoCadastral alteracaoCadastralPai = this.IdJuridicaPai.GetAlteracaoCadastral(ref dataDocumento);

                if (alteracaoCadastralPai.Id == 0)
                    iPessoaPai = this.IdJuridicaPai;
                else
                    iPessoaPai = alteracaoCadastralPai;

                str.Append(@"<B>" + iPessoaPai.NomeCompleto + "</B>");
                str.Append("<br>");
                str.Append(@"<FONT color=""#003300"" size=2>" + this.IdJuridicaPapel.GetDescricaoLocaldeTrabalho());
                str.Append("<br>");
                str.Append(iPessoa.NomeCompleto);
                str.Append("<br>");
                str.Append("</FONT>");
            }
            else
                str.Append("<B>" + iPessoa.NomeCompleto + "</B>");

            str.Append("<br>");
            str.Append(@"<FONT color=""#003300"" size=2>" + Endereco.GetEndereco(iEndereco));
            str.Append("<br>");
            str.Append("CEP " + iEndereco.Cep + " " + iEndereco.Bairro);
            str.Append("<br>");
            str.Append(Endereco.GetCidade(iEndereco) + "  " + Endereco.GetEstado(iEndereco) + "</FONT>");
            str.Append("<br>");
            str.Append("<B>CNPJ: " + Juridica.FormatarCnpj(iPessoa.NomeCodigo) + "</B>");
            str.Append("<br>");

            if (iJuridica.IdCNAE.Id != 0)
            {
                iJuridica.IdCNAE.Find();
                str.Append(@"<FONT color=""#003300"" size=2>"
                    +"CNAE: " + iJuridica.IdCNAE.Codigo 
                    + "&nbsp;&nbsp;  Grupo CIPA: " + iJuridica.IdCNAE.IdGrupoCipa.ToString()
                    + "&nbsp;&nbsp;  NºEmpregados: " + this.QtdEmpregados.ToString() 
                    + "</FONT></P>");
            }

            return str.ToString();
        }


        public string GetDadosEmpresaCipa_Sem_GrupoCipa(DateTime dataDocumento)
        {
            if (this.mirrorOld == null)
                this.Find();

            IPessoa iPessoa;
            IJuridica iJuridica;
            IEndereco iEndereco;

            GetDadosEmpresa(dataDocumento, out iPessoa, out iJuridica, out iEndereco);

            StringBuilder str = new StringBuilder();

            str.Append(@"<P align='center'>");
            str.Append(@"<FONT color=""#003300"" size=4>");

            if (this.IdJuridicaPapel.IsLocalDeTrabalho())
            {
                if (this.IdJuridicaPai.mirrorOld == null)
                    this.IdJuridicaPai.Find();

                IPessoa iPessoaPai;

                AlteracaoCadastral alteracaoCadastralPai = this.IdJuridicaPai.GetAlteracaoCadastral(ref dataDocumento);

                if (alteracaoCadastralPai.Id == 0)
                    iPessoaPai = this.IdJuridicaPai;
                else
                    iPessoaPai = alteracaoCadastralPai;

                str.Append(@"<B>" + iPessoaPai.NomeCompleto + "</B>");
                str.Append("<br>");
                str.Append(@"<FONT color=""#003300"" size=2>" + this.IdJuridicaPapel.GetDescricaoLocaldeTrabalho());
                str.Append("<br>");
                str.Append(iPessoa.NomeCompleto);
                str.Append("<br>");
                str.Append("</FONT>");
            }
            else
                str.Append("<B>" + iPessoa.NomeCompleto + "</B>");

            str.Append("<br>");
            str.Append(@"<FONT color=""#003300"" size=2>" + Endereco.GetEndereco(iEndereco));
            str.Append("<br>");
            str.Append("CEP " + iEndereco.Cep + "   " + iEndereco.Bairro);
            str.Append("<br>");
            str.Append(Endereco.GetCidade(iEndereco) + "   " + Endereco.GetEstado(iEndereco) + "</FONT>");
            str.Append("<br>");
            str.Append("<B>CNPJ " + Juridica.FormatarCnpj(iPessoa.NomeCodigo) + "</B>");
            str.Append("<br>");

            if (iJuridica.IdCNAE.Id != 0)
            {
                iJuridica.IdCNAE.Find();
                str.Append(@"<FONT color=""#003300"" size=2>"
                    + "CNAE: " + iJuridica.IdCNAE.Codigo
                    + "&nbsp;&nbsp;  NºEmpregados: " + this.QtdEmpregados.ToString()
                    + "</FONT></P>");
            }

            return str.ToString();
        }
        #endregion

        #region GetPrestadoresComEmail

        public List<Prestador> GetPrestadoresComEmail()
        {
            return GetPrestadoresComEmail(ResponsavelPapel.NaoRecebeAvisos);
        }

        public List<Prestador> GetPrestadoresComEmail(ResponsavelPapel responsavelPapel)
        {
            if (this.mirrorOld == null)
                this.Find();

            StringBuilder str = new StringBuilder();
            str.Append("(IdPrestador IN (SELECT IdJuridicaPessoa FROM JuridicaPessoa WHERE");

            if (this.IdJuridicaPai.Id != 0)
                str.Append(" IdJuridica=" + this.IdJuridicaPai.Id + ")");
            else
                str.Append(" IdJuridica=" + this.Id + ")");

            str.Append(" OR IdPrestador IN (SELECT IdPrestador FROM PrestadorCliente WHERE IdCliente=" + this.Id + "))");

            str.Append(" AND IsInativo=0");
            //str.Append(" AND IdPessoa IN (SELECT IdPessoa FROM Usuario)");
            str.Append(" AND IdPessoa IN (SELECT IdPessoa FROM Pessoa WHERE Email IS NOT NULL AND Email<>'' AND IsInativo=0)");
            str.Append(" AND IndTipoPrestador=" + (int)TipoPrestador.ContatoEmpresa);

            if (responsavelPapel == ResponsavelPapel.NaoRecebeAvisos)
                str.Append(" AND IdPrestador NOT IN (SELECT IdPrestador FROM Responsavel WHERE IndResponsavelPapel=" + (int)ResponsavelPapel.NaoRecebeAvisos + ")");
            else
                str.Append(" AND IdPrestador IN (SELECT IdPrestador FROM Responsavel WHERE IndResponsavelPapel=" + (int)responsavelPapel + ")");

            List<Prestador> prestadores = new Prestador().Find<Prestador>(str.ToString());

            return prestadores;
        }
        #endregion

        #region Save

        public override int Save()
        {
            this.NomeCodigo = FormatarCnpj(this.NomeCodigo);

            AtualizarCnaeCipa();

            //ValidarInscricaoEstadual();

            //ValidarFilial();

            return base.Save();
        }

        protected int Save(bool bVal)
        {
            return base.Save();
        }
        #endregion

        #region AtualizarCnaeCipa

        private void AtualizarCnaeCipa()
        {
            if (this.mirrorOld == null)
                return;

            CNAE cnaeOld = ((Juridica)mirrorOld).IdCNAE;

            if (cnaeOld.Id == this.IdCNAE.Id)
                return;

            if (cnaeOld.Id == 0 || this.IdCNAE.Id == 0)
                return;

            if (cnaeOld.mirrorOld == null)
                cnaeOld.Find();

            if (this.IdCNAE.mirrorOld == null)
                this.IdCNAE.Find();

        }
        #endregion

        #region JuridicaPapel

        public bool IsCliente()
        {
            return (this.IdJuridicaPapel.Id == (int)IndJuridicaPapel.Cliente
                || this.IdJuridicaPapel.Id == (int)IndJuridicaPapel.AposentEspecial
                || this.IdJuridicaPapel.Id == (int)IndJuridicaPapel.Tomadora
                || this.IdJuridicaPapel.Id == (int)IndJuridicaPapel.Obras
                || this.IdJuridicaPapel.Id == (int)IndJuridicaPapel.TerceiroAutonomo);
        }

        public bool IsProspeccao()
        {
            return this.IdJuridicaPapel.Id == (int)IndJuridicaPapel.Prospeccao
                        || this.IdJuridicaPapel.Id == (int)IndJuridicaPapel.Prospeccao3
                        || this.IdJuridicaPapel.Id ==  (int)IndJuridicaPapel.Prospects2;
        }

        public bool IsLocalDeTrabalho()
        {
            return this.IdJuridicaPapel.Id == (int)IndJuridicaPapel.Tomadora
                || this.IdJuridicaPapel.Id == (int)IndJuridicaPapel.Obras
                || this.IdJuridicaPapel.Id == (int)IndJuridicaPapel.TerceiroAutonomo;
        }

        public bool IsTomadora()
        {
            return this.IdJuridicaPapel.Id == (int)IndJuridicaPapel.Tomadora
                || this.IdJuridicaPapel.Id == (int)IndJuridicaPapel.TerceiroAutonomo;
        }
        #endregion

        #region Diretorio

        #region GetFotoDiretorioPadrao

        public string GetFotoDiretorioPadrao(EnvironmentUtitity.SubFoldersFotos folder)
        {
            return System.IO.Path.Combine(EnvironmentUtitity.GetRaizPathFoto(),
                   System.IO.Path.Combine(this.GetFotoDiretorioPadrao(), folder.ToString()));
        }

        #endregion

        #region GetFotoDiretorioPadrao

        public string GetFotoDiretorioPadrao()
        {
         
            if (this.mirrorOld == null)
                this.Find();

            string sFotoDiretorioPadrao;

            //if (this.IdJuridicaPai.Id != 0 && this.IsLocalDeTrabalho())
            //{
            //    if (this.IdJuridicaPai.mirrorOld == null)
            //        this.IdJuridicaPai.Find();

            //    sFotoDiretorioPadrao = Path.Combine(this.IdJuridicaPai.DiretorioPadrao,
            //                           Path.Combine(EnvironmentUtitity.SubFolderTomadoras, this.DiretorioPadrao));
            //}
            //else
                sFotoDiretorioPadrao = this.DiretorioPadrao;

            return sFotoDiretorioPadrao;
        }

        #endregion

        #region VerificarDiretorioPadrao

        public bool IsDiretorioPadrao(string path)
        {
            string diretorioPadrao = this.GetFotoDiretorioPadrao();

            DirectoryInfo dir1 = new DirectoryInfo(path);

            string diretorio = dir1.FullName.Substring(dir1.Root.Name.Length);

            bool ret = (diretorio.IndexOf(diretorioPadrao) == 0);

            return ret;
        }

        private DirectoryInfo GetRootDirectory(DirectoryInfo dir)
        {
            if (IsRootDirectory(dir))
                return dir;
            else
                return GetRootDirectory(dir.Parent);
        }

        public static bool IsRootDirectory(DirectoryInfo dir)
        {
            return dir.Root.Name == dir.Parent.Name;
        }

        #endregion

        #region CriarDiretorioTodasEmpresas

        public static void CriarDiretorioTodasEmpresas()
        {
            List<Juridica> clientes = new Juridica().Find<Juridica>("IdJuridica IN (SELECT IdCliente FROM qryClienteAtivos)"
                                                + " AND IdJuridicaPapel=" + (int)IndJuridicaPapel.Cliente
                                                + " ORDER BY NomeAbreviado");

            foreach (Juridica juridica in clientes)
            {
                //System.Diagnostics.Debug.WriteLine(juridica.NomeAbreviado);

                juridica.CriarDiretorio();
            }
        }
        #endregion

        #region CriarDiretorio

        public void CriarDiretorio()
        {
            bool bSalvar = false;

            if (this.DiretorioPadrao == string.Empty)
            {
                this.DiretorioPadrao = Utility.RemoveAcentosECaracteresEspeciais(this.NomeAbreviado);

                bSalvar = true;
            }

            CreateFolders();

            if (bSalvar)
                this.Save();
        }

        private void CreateFolders()
        {
            string[] pastas = EnvironmentUtitity.GetShareFolders();

            foreach (string pasta in pastas)
            {
                string dirEmpresaPath;

                if (this.IsLocalDeTrabalho())
                {
                    if (this.IdJuridicaPai.mirrorOld == null)
                        this.IdJuridicaPai.Find();

                    dirEmpresaPath = Path.Combine(pasta,
                                     Path.Combine(this.IdJuridicaPai.DiretorioPadrao, 
                                     Path.Combine(EnvironmentUtitity.SubFolderTomadoras, this.DiretorioPadrao)));
                }
                else
                    dirEmpresaPath = Path.Combine(pasta, this.DiretorioPadrao);

                if (!Directory.Exists(dirEmpresaPath))
                    Directory.CreateDirectory(dirEmpresaPath);

                string[] subPastas;

                if (pasta == EnvironmentUtitity.GetShareFolder(EnvironmentUtitity.ShareFolders.DocsDigitais))
                    subPastas = EnvironmentUtitity.GetSubFoldersFotos();
                else
                    subPastas = EnvironmentUtitity.GetSubFolders();

                foreach (string subPasta in subPastas)
                {
                    string subPastaPath = Path.Combine(dirEmpresaPath, subPasta);

                    if (!Directory.Exists(subPastaPath))
                        Directory.CreateDirectory(subPastaPath);
                }
            }
        }


        #endregion

        #endregion

        #endregion
    }
}
