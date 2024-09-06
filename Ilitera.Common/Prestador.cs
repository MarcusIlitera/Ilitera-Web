using System;
using System.Data;
using System.Drawing;
using System.Text;
using System.Collections;

using Ilitera.Data;

namespace Ilitera.Common
{
     public enum TipoPrestador : int
    {
        ContatoEmpresa,
        Engenheiro,
        Medico,
        TodosPrestadores
    }

    public enum ResponsavelPapel : int
    {
        ContatoPrincipal = 1,
        Boleto = 4,
        AcessoAuditoria = 6,
        ASOPCIBranco = 0,
        CIPA = 2,
        Contrato = 5,
        Diretoria = 3,
        NaoRecebeAvisos = 7,
    }

    [Database("opsa", "Prestador", "IdPrestador")]
    public class Prestador : Ilitera.Common.JuridicaPessoa
    {
        private int _IdPrestador;
        private int _CodigoAntigo;
        private int _IndTipoPrestador;
        private TipoVinculo _IndVinculo;
        private string _Titulo = string.Empty;
        private string _Numero = string.Empty;
        private string _Contato = string.Empty;
        private string _FotoRG = string.Empty;
        private string _FotoAss = string.Empty;
        private bool _IsInativo;
        private bool _hasVisibilidadeGrupo;
        private ContatoTelefonico contatoTelefonico;
        private int _IdDocumentoBase;
        private string _Departamento = string.Empty;
        private float _ValorHora;
        private string _UF;
        private string _CPF;

        public enum TipoVinculo : int
        {
            Empregado = 0,
            Estagiario = 1,
            Autonomo = 2,
        }

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public Prestador()
        {
            this.IndPessoaPapel = (int)PessoaPapeis.Prestador;
        }

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public Prestador(int Id)
        {
            this.Find(Id);
        }

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public Prestador(string NomePessoa)
        {
            this.IdPessoa = new Pessoa();
            this.IdPessoa.NomeAbreviado = NomePessoa;
            this.IdPessoa.IndTipoPessoa = (int)Pessoa.TipoPessoa.JuridicaPessoa;
            this.IndPessoaPapel = (int)PessoaPapeis.Prestador;
        }

        public override int Id
        {
            get { return _IdPrestador; }
            set { _IdPrestador = value; }
        }
        public int CodigoAntigo
        {
            get { return _CodigoAntigo; }
            set { _CodigoAntigo = value; }
        }
        public int IndTipoPrestador
        {
            get { return _IndTipoPrestador; }
            set { _IndTipoPrestador = value; }
        }
        public TipoVinculo IndVinculo
        {
            get { return _IndVinculo; }
            set { _IndVinculo = value; }
        }
        public string Titulo
        {
            get { return _Titulo; }
            set { _Titulo = value; }
        }
        public string Numero
        {
            get { return _Numero; }
            set { _Numero = value; }
        }
        [Persist(false)]
        public string Contato
        {
            get
            {
                if (contatoTelefonico == null)
                    contatoTelefonico = this.IdJuridica.GetContatoTelefonico();

                return contatoTelefonico.GetDDDTelefone();
            }
        }
        public string FotoRG
        {
            get { return _FotoRG; }
            set { _FotoRG = value; }
        }
        public string FotoAss
        {
            get { return _FotoAss; }
            set { _FotoAss = value; }
        }
        public bool IsInativo
        {
            get { return _IsInativo; }
            set { _IsInativo = value; }
        }
        public bool hasVisibilidadeGrupo
        {
            get { return _hasVisibilidadeGrupo; }
            set { _hasVisibilidadeGrupo = value; }
        }
        public int IdDocumentoBase
        {
            get { return _IdDocumentoBase; }
            set { _IdDocumentoBase = value; }
        }

        public string Departamento
        {
            get { return _Departamento; }
            set { _Departamento = value; }
        }

        public float ValorHora
        {
            get { return _ValorHora; }
            set { _ValorHora = value; }
        }

        public string UF
        {
            get { return _UF; }
            set { _UF = value; }
        }

        public string CPF
        {
            get { return _CPF; }
            set { _CPF = value; }
        }


        public override int Save()
        {
            if (this.IdPessoa.mirrorOld == null)
                this.IdPessoa.Find();

            this.IdPessoa.IsInativo = this.IsInativo;

            bool AtualizaTimeSheet = this.IdJuridica.Id == 310
                                    && this.ValorHora != 0
                                    && this.mirrorOld != null
                                    && this.ValorHora != ((Prestador)this.mirrorOld).ValorHora;

            int Id = base.Save();

            if (AtualizaTimeSheet)
                AtualizarValorHoraTimeSheet();

            return Id;
        }

        public void AtualizarValorHoraTimeSheet()
        {
            string cmd = "USE " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + " UPDATE TimeSheet"
                        + " SET ValorHora=" + this.ValorHora.ToString("n").Replace(",", ".")
                        + " WHERE IdPessoa=" + this.IdPessoa.Id;

            new TimeSheet().ExecuteScalar(cmd);
        }

        public string GetEnderecoTelefoneEmail()
        {
            if (this.IdJuridica.mirrorOld == null)
                this.IdJuridica.Find();

            Endereco endereco = this.IdJuridica.GetEndereco();

            ContatoTelefonico telefone = this.IdJuridica.GetContatoTelefonico();


            return endereco.GetEnderecoCompletoPorLinha()
                        + "\n" + telefone.GetDDDTelefone()
                        + "\n" + this.IdJuridica.Email;
        }

        public static string GetVinculo(TipoVinculo tipoVinculo)
        {
            string ret;

            if (tipoVinculo == TipoVinculo.Empregado)
                ret = "Empregado";
            else if (tipoVinculo == TipoVinculo.Autonomo)
                ret = "Autônomo";
            else
                ret = "Estagiário";

            return ret;
        }

        public Prestador GetPrestadorByIdPessoa(int IdPessoa)
        {
            Prestador prestador = new Prestador();
            DataSet ds = new JuridicaPessoa().Get("IdPessoa=" + IdPessoa);
            prestador.Find(Convert.ToInt32(ds.Tables[0].Rows[0]["IdJuridicaPessoa"]));
            return prestador;
        }

        public DataSet DataSourceAssinatura()
        {
            return DataSourceAssinatura(false);
        }

        public DataSet DataSourceAssinatura(bool isFromWeb)
        {
            DataSet ds = new DataSet();
            DataTable table = new DataTable("Result");
            table.Columns.Add("iAssinatura", Type.GetType("System.Byte[]"));
            ds.Tables.Add(table);

            DataRow newRow;

            newRow = ds.Tables[0].NewRow();

            if (this.FotoAss != string.Empty)
            {
                try
                {
                    newRow["iAssinatura"] = Ilitera.Common.Fotos.GetByteFoto_Uri(this._FotoAss);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                    //System.Diagnostics.Trace.WriteLine(ex.Message);
                }
            }
            ds.Tables[0].Rows.Add(newRow);

            return ds;
        }

        public string FotoPrestadorPathRelativo()
        {
            this.IdPessoa.Find();

            if (!this.IdPessoa.Foto.Equals(string.Empty))
                return Fotos.UrlFoto(this.IdPessoa.Foto);
            else
                return "../img/foto_null.gif";
        }

        public string GetAssinaturaComLogoEmail()
        {
            string ret;

            //EmailAssinatura emailAss = new EmailAssinatura();
            //emailAss.Find("IdPessoa=" + this.IdPessoa.Id);

            ret = "";
            //ret = emailAss.HtmlAss1;

            return ret;
        }

        private string GetAssinaturaEmail()
        {
            string ret;

            ret = "Atenciosamente,"
                + "<br>" + this.NomeCompleto
                + "<br>" + this.Titulo
                + "<br>" + this.IdJuridica.NomeCompleto
                + "<br>" + this.IdJuridica.GetEndereco().GetEndereco()
                + "<br>" + "CEP: " + this.IdJuridica.GetEndereco().Cep + " - " + this.IdJuridica.GetEndereco().GetCidadeEstado()
                + "<br>" + "Telefone: " + this.IdJuridica.GetContatoTelefonico().GetDDDTelefone()
                + "<br>" + "Fax: " + this.IdJuridica.GetFax().GetDDDTelefone()
                + @"<br> <A href=""mailto:" + this.Email + @""">" + this.Email + "</A>";

            return ret.Replace("<br><br>", "<br>");
        }

        private static string GeraLoginPara(string nomeAbreviado)
        {
            string ret = nomeAbreviado.ToUpper();

            ret = ret.Replace("PCMSO", string.Empty);
            ret = ret.Replace("N/CIPA", string.Empty);

            if (ret.IndexOf("/") != -1)
                ret = ret.Substring(0, ret.IndexOf("/"));

            ret = ret.Replace("N-CIPA", string.Empty);
            ret = ret.Replace("-N.F.-CIPA-", string.Empty);
            ret = ret.Replace("CIPA", string.Empty);
            ret = ret.Replace("PCMSO", string.Empty);
            ret = ret.Replace("  ", string.Empty);
            ret = ret.Replace(" - ", string.Empty);
            ret = ret.Replace("-", string.Empty);
            ret = ret.Replace("(", "_");
            ret = ret.Replace(")", string.Empty);
            ret = ret.Replace("'", string.Empty);
            ret = ret.Replace(".", string.Empty);
            ret = ret.Trim();
            ret = ret.Replace(" ", "_");

            return ret;
        }

        public static void GerarSenhaTodosClientes()
        {
            ArrayList list = new Juridica().Find(@"IdJuridicaPapel = 1 AND dbo.Pessoa.Email IS NOT NULL AND dbo.Pessoa.Email <>'' AND dbo.Pessoa.Email LIKE '%@%'");

            foreach (Juridica juridica in list)
            {
                try
                {
                    GerarSenhaPPP(juridica);
                }
                catch { }
            }
        }

        //public static void EnviarAvisoTodosClientes()
        //{
        //    ArrayList listCliente = new Juridica().Find(@"IdJuridicaPapel =" + (int)IndJuridicaPapel.Cliente
        //                                                + " AND IsInativo=0"
        //                                                + " AND dbo.Pessoa.Email IS NOT NULL"
        //                                                + " AND dbo.Pessoa.Email <>''"
        //                                                + " AND dbo.Pessoa.Email LIKE '%@%'");

        //    foreach(Juridica juridica in listCliente)
        //    {
        //        try
        //        {
        //            AvisoPrestadores(juridica);
        //        }
        //        catch{}
        //    }

        //    throw new Exception("Os avisos foram enviados com sucesso!");
        //}

        //public static void AvisoPrestadores(Juridica juridica)
        //{
        //    ArrayList listPrestador = new Prestador().Find("IdJuridica=" + juridica.Id + " AND IndTipoPrestador IN(0,2)");

        //    foreach (Prestador prestador in listPrestador)
        //    {
        //        prestador.EnviarEmailArquivoHtml("2004-01-19/index.html",
        //                                        "Proibição de disposição de dados médicos no PPP - Parecer jurídico",
        //                                        LogEmailSend.TipoEmail.AvisoManual,
        //                                        juridica,
        //                                        prestador);
        //    }
        //}

        public static void GerarSenhaPPP(Juridica juridica)
        {

        }

        public static void ReenviarEmailSenhaPPP(Juridica juridica)
        {
            ArrayList listPrestador = new Prestador().Find("IdJuridica=" + juridica.Id
                + " AND IndTipoPrestador IN (0,2)");

            foreach (Prestador prestador in listPrestador)
                ReenviarEmailSenhaPPP(prestador, string.Empty);
        }

        public static void ReenviarEmailSenhaPPP(Prestador prestador, string patchEmail)
        {
            Usuario usuario = new Usuario();
            usuario.Find("IdPessoa=" + prestador.IdPessoa.Id);

            if (usuario.Id != 0)
                usuario.EnviarEmailPrimeiraVez("Envio de Senha para o Ilitera.NET",
                                                patchEmail,
                                                prestador.IdPessoa);
        }

        public static void ReenviarEmailSenhaPPP(Prestador prestador)
        {
            Usuario usuario = new Usuario();
            usuario.Find("IdPessoa=" + prestador.IdPessoa.Id);

            if (usuario.Id != 0)
                usuario.EnviarEmailPrimeiraVez("Envio de Senha para o Ilitera.NET",
                                                prestador.IdPessoa);
        }

        public override string ToString()
        {
            if (this.mirrorOld == null)
                this.Find();

            return this.NomeAbreviado;
        }

        public void FindByPessoa(Pessoa pessoa)
        {
            JuridicaPessoa juridicaPessoa = new JuridicaPessoa();
            juridicaPessoa.Find("IdPessoa =" + pessoa.Id);
            this.Find("JuridicaPessoa.IdJuridicaPessoa=" + juridicaPessoa.Id);
        }

        public ArrayList GetListaPrestador()
        {
            return GetListaPrestador(false);
        }

        public ArrayList GetListaPrestador(bool IsInativo)
        {
            Juridica juridica = new Juridica();
            juridica.Find(310);
            return GetListaPrestador(juridica, IsInativo);
        }

        public ArrayList GetListaPrestador(Juridica juridica, bool IsInativo)
        {
            StringBuilder str = new StringBuilder();

            if (juridica.IdGrupoEmpresa.Id == 0)
                str.Append("IdJuridica=" + juridica.Id);
            else
                str.Append("IdJuridica IN (SELECT IdJuridica FROM Juridica WHERE IdGrupoEmpresa=" + juridica.IdGrupoEmpresa.Id + ")");

            str.Append(" AND IsInativo=" + Convert.ToInt16(IsInativo));

            str.Append(" ORDER BY (SELECT NomeAbreviado FROM Pessoa WHERE JuridicaPessoa.IdPessoa=Pessoa.IdPessoa)");

            return new Prestador().Find(str.ToString());
        }

        public ArrayList GetListaPrestador(Juridica juridica, bool IsInativo, bool useGrupoEmpresa)
        {
            return GetListaPrestador(juridica, IsInativo, (int)TipoPrestador.TodosPrestadores, useGrupoEmpresa);
        }

        public ArrayList GetListaPrestador(Juridica juridica, bool IsInativo, int IndTipoPrestador)
        {
            return GetListaPrestador(juridica, IsInativo, IndTipoPrestador, true);
        }

        public ArrayList GetListaPrestador(Juridica juridica, bool IsInativo, int IndTipoPrestador, bool useGrupoEmpresa)
        {
            StringBuilder str = new StringBuilder();

            if (useGrupoEmpresa)
            {
                if (juridica.IdGrupoEmpresa.Id == 0)
                    str.Append("IdJuridica=" + juridica.Id);
                else
                    str.Append("IdJuridica IN (SELECT IdJuridica FROM Juridica WHERE IdGrupoEmpresa=" + juridica.IdGrupoEmpresa.Id + ")");
            }
            else
                str.Append("IdJuridica=" + juridica.Id);
            
            str.Append(" AND IsInativo=" + Convert.ToInt16(IsInativo));

            if (!IndTipoPrestador.Equals((int)TipoPrestador.TodosPrestadores))
                str.Append(" AND IndTipoPrestador=" + IndTipoPrestador);

            str.Append(" AND IdPessoa IN (SELECT IdPessoa FROM Pessoa WHERE IsInativo=" + Convert.ToInt16(IsInativo) + ")");
            str.Append(" ORDER BY (SELECT NomeCompleto FROM Pessoa WHERE JuridicaPessoa.IdPessoa=Pessoa.IdPessoa)");

            return new Prestador().Find(str.ToString());
        }

        public ArrayList GetListaOperadorTelemarketing()
        {
            StringBuilder str = new StringBuilder();
            str.Append(" IdPrestador<>0");
            str.Append(" AND JuridicaPessoa.IdJuridicaPessoa IN (SELECT IdJuridicaPessoa FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.JuridicaPessoa WHERE IdJuridica=310)");
            str.Append(" AND IsInativo=0");
            str.Append(" AND IdPessoa IN (SELECT IdPessoa FROM Pessoa WHERE IsInativo=0)");

            return new Prestador().Find(str.ToString());
        }
    }
}
