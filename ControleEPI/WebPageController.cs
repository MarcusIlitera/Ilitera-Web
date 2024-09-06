using System;
using System.IO;
using System.Data;
using System.Drawing;
using System.Configuration;
using System.Collections;
using System.Collections.Specialized;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;

//using Infragistics.WebUI.UltraWebGrid;

using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

using Ilitera.Opsa.Data;
using Ilitera.Common;

namespace Ilitera.Net.ControleEPI
{
    /// <summary>
    /// Classe que contém métodos comuns à todos os arquivos no projeto Ilitera.NET
    /// </summary>
    public class WebPageController : System.Web.UI.Page
    {
        protected Prestador prestador = new Prestador();
        protected Usuario usuario = new Usuario();
        protected Cliente cliente = new Cliente();
        protected Empregado empregado = new Empregado();

        protected Color backColorDisabledBox = Color.FromName("#EBEBEB");
        protected Color backColorEnabledBox = Color.FromName("#FCFEFD");
        protected Color foreColorDisabledLabel = Color.Gray;
        protected Color foreColorEnabledLabel = Color.FromName("#44926D");
        protected Color foreColorEnabledTextBox = Color.FromName("#004000");
        protected Color foreColorDisabledTextBox = Color.Gray;
        protected Color borderColorDisabledBox = Color.LightGray;
        protected Color borderColorEnabledBox = Color.FromName("#7CC5A1");
        protected Color backColorEnabledBoxYellow = Color.LightYellow;

        public WebPageController()
        {

        }


        public enum IndTipoPagina : int
        {
            Nenhuma,
            Popup,
            BigContents,
            SmallContents
        }

        #region General Methods

        protected virtual void InicializaWebPageObjects()
        {
            InicializaWebPageObjects((int)IndTipoPagina.Nenhuma, string.Empty);
        }

        /// <summary>
        /// Ilitera - Identificador para separar tratamentos realizados pela Ilitera
        /// </summary>
        /// <param name="isIlitera"></param>
        protected virtual void InicializaWebPageObjects(bool isIlitera)
        {            
            InicializaWebPageObjects((int)IndTipoPagina.Nenhuma, string.Empty, isIlitera);
        }

        protected virtual void InicializaWebPageObjects(int TipoPagina)
        {
            InicializaWebPageObjects(TipoPagina, string.Empty);
        }

        protected virtual void InicializaWebPageObjects(int TipoPagina, string pageToRedirect)
        {
            if (Request.QueryString["IdUsuario"] != null
                && Request.QueryString["IdUsuario"] != "")
            {
                this.usuario = new Usuario(Convert.ToInt32(Request.QueryString["IdUsuario"]));
                this.usuario.IdPessoa.Find();

                if (!this.usuario.NomeUsuario.Equals("Admin"))
                {
                    this.prestador = new Prestador();
                    this.prestador.FindByPessoa(this.usuario.IdPessoa);
                    this.prestador.IdPessoa.Find();
                }
            }

            if (Request.QueryString["IdEmpresa"] != null
                && Request.QueryString["IdEmpresa"] != "")
                this.cliente = new Cliente(Convert.ToInt32(Request.QueryString["IdEmpresa"]));

            if (Request.QueryString["IdEmpregado"] != null
                && Request.QueryString["IdEmpregado"] != "")
                this.empregado = new Empregado(Convert.ToInt32(Request.QueryString["IdEmpregado"]));

            if (TipoPagina != (int)IndTipoPagina.Nenhuma)
            {
                try
                {
                    Usuario.Permissao(this.GetType().BaseType, this.usuario, AcaoPermissao.Executar);
                }
                catch (Exception ex)
                {
                    StringBuilder st = new StringBuilder();

                    switch (TipoPagina)
                    {
                        case (int)IndTipoPagina.Popup:
                            Aviso(ex.Message, true);
                            break;
                        case (int)IndTipoPagina.BigContents:
                            st.AppendFormat("window.alert(\"{0}\");", ex.Message);
                            st.AppendFormat("window.location.href=\"{0}?IdUsuario={1}&IdEmpresa={2}&IdEmpregado={3}\";", pageToRedirect, this.usuario.Id, this.cliente.Id, this.empregado.Id);
                            
                            //st.Append("window.alert(\"" + ex.Message + "\");");
                            //st.Append("window.location.href=\"" + pageToRedirect + "?IdUsuario=" + this.usuario.Id + "&IdEmpresa=" + this.cliente.Id + "&IdEmpregado=" + this.empregado.Id + "\";");

                            this.ClientScript.RegisterStartupScript(this.GetType(), "BigContentsRedirect", st.ToString(), true);
                            break;
                        case (int)IndTipoPagina.SmallContents:
                            //st.Append("window.alert(\"" + ex.Message + "\");");
                            //st.Append("window.location.href=\"" + pageToRedirect + "?IdUsuario=" + this.usuario.Id + "&IdEmpresa=" + this.cliente.Id + "&IdEmpregado=" + this.empregado.Id + "\";");

                            st.AppendFormat("window.alert(\"{0}\");", ex.Message);
                            st.AppendFormat("window.location.href=\"{0}?IdUsuario={1}&IdEmpresa={2}&IdEmpregado={3}\";", pageToRedirect, this.usuario.Id, this.cliente.Id, this.empregado.Id);

                            this.ClientScript.RegisterStartupScript(this.GetType(), "SmallContentsRedirect", st.ToString(), true);
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// Ilitera - Sobrecarga do método para personalizações referentes a Ilitera
        /// </summary>
        /// <param name="TipoPagina"></param>
        /// <param name="pageToRedirect"></param>
        /// <param name="isIlitera"></param>
        protected virtual void InicializaWebPageObjects(int TipoPagina, string pageToRedirect, bool isIlitera)
        {

            if (Session["usuarioLogado"] != null && Session["usuarioLogado"].ToString() != String.Empty)
            {
                Entities.Usuario user = (Entities.Usuario)Session["usuarioLogado"];

                this.usuario = new Usuario(user.IdUsuario);
                this.usuario.IdPessoa.Find();

                if (!this.usuario.NomeUsuario.Equals("Admin"))
                {
                    this.prestador = new Prestador();
                    this.prestador.FindByPessoa(this.usuario.IdPessoa);
                    this.prestador.IdPessoa.Find();
                }
            }

            if (Session["Empresa"] != null && Session["Empresa"].ToString() != String.Empty)
                this.cliente = new Cliente(Convert.ToInt32(Session["Empresa"]));

            if (Session["Empregado"] != null && Session["Empregado"].ToString() != String.Empty)
                this.empregado = new Empregado(Convert.ToInt32(Session["Empregado"]));

            if (TipoPagina != (int)IndTipoPagina.Nenhuma)
            {
                try
                {
                    Usuario.Permissao(this.GetType().BaseType, this.usuario, AcaoPermissao.Executar);
                }
                catch (Exception ex)
                {
                    StringBuilder st = new StringBuilder();

                    switch (TipoPagina)
                    {
                        case (int)IndTipoPagina.Popup:
                            Aviso(ex.Message, true);
                            break;
                        case (int)IndTipoPagina.BigContents:
                            //st.Append("window.alert(\"" + ex.Message + "\");");
                            //st.Append("window.location.href=\"" + pageToRedirect + "?IdUsuario=" + this.usuario.Id + "&IdEmpresa=" + this.cliente.Id + "&IdEmpregado=" + this.empregado.Id + "\";");

                            st.AppendFormat("window.alert(\"{0}\");", ex.Message);
                            st.AppendFormat("window.location.href=\"{0}?IdUsuario={1}&IdEmpresa&IdEmpregado={2}\";", pageToRedirect, this.usuario.Id, this.cliente.Id, this.empregado.Id);

                            this.ClientScript.RegisterStartupScript(this.GetType(), "BigContentsRedirect", st.ToString(), true);
                            break;

                        case (int)IndTipoPagina.SmallContents:
                            //st.Append("window.alert(\"" + ex.Message + "\");");
                            //st.Append("window.location.href=\"" + pageToRedirect + "?IdUsuario=" + this.usuario.Id + "&IdEmpresa=" + this.cliente.Id + "&IdEmpregado=" + this.empregado.Id + "\";");

                            st.AppendFormat("window.alert(\"{0}\");", ex.Message);
                            st.AppendFormat("window.location.href=\"{0}?IdUsuario={1}&IdEmpresa={2}&IdEmpregado={3}\";", pageToRedirect, this.usuario.Id, this.cliente.Id, this.empregado.Id);

                            this.ClientScript.RegisterStartupScript(this.GetType(), "SmallContentsRedirect", st.ToString(), true);
                            break;
                    }
                }
            }

            /*---------------------------------------------------------------------------------------------------------*/
            /*---------------------------------------------------------------------------------------------------------*/
            /*---------------------------------------------------------------------------------------------------------*/

            //if (Request.QueryString["IdUsuario"] != null
            //    && Request.QueryString["IdUsuario"] != "")
            //{
            //    this.usuario = new Usuario(Convert.ToInt32(Request.QueryString["IdUsuario"]));
            //    this.usuario.IdPessoa.Find();

            //    if (!this.usuario.NomeUsuario.Equals("Admin"))
            //    {
            //        this.prestador = new Prestador();
            //        this.prestador.FindByPessoa(this.usuario.IdPessoa);
            //        this.prestador.IdPessoa.Find();
            //    }
            //}

            //if (Request.QueryString["IdEmpresa"] != null
            //    && Request.QueryString["IdEmpresa"] != "")
            //    this.cliente = new Cliente(Convert.ToInt32(Request.QueryString["IdEmpresa"]));

            //if (Request.QueryString["IdEmpregado"] != null
            //    && Request.QueryString["IdEmpregado"] != "")
            //    this.empregado = new Empregado(Convert.ToInt32(Request.QueryString["IdEmpregado"]));

            //if (TipoPagina != (int)IndTipoPagina.Nenhuma)
            //{
            //    try
            //    {
            //        Usuario.Permissao(this.GetType().BaseType, this.usuario, AcaoPermissao.Executar);
            //    }
            //    catch (Exception ex)
            //    {
            //        StringBuilder st = new StringBuilder();

            //        switch (TipoPagina)
            //        {
            //            case (int)IndTipoPagina.Popup:
            //                Aviso(ex.Message, true);
            //                break;
            //            case (int)IndTipoPagina.BigContents:
            //                st.Append("window.alert(\"" + ex.Message + "\");");
            //                st.Append("window.location.href=\"" + pageToRedirect + "?IdUsuario=" + this.usuario.Id + "&IdEmpresa=" + this.cliente.Id + "&IdEmpregado=" + this.empregado.Id + "\";");

            //                this.ClientScript.RegisterStartupScript(this.GetType(), "BigContentsRedirect", st.ToString(), true);
            //                break;
            //            case (int)IndTipoPagina.SmallContents:
            //                st.Append("window.alert(\"" + ex.Message + "\");");
            //                st.Append("window.location.href=\"" + pageToRedirect + "?IdUsuario=" + this.usuario.Id + "&IdEmpresa=" + this.cliente.Id + "&IdEmpregado=" + this.empregado.Id + "\";");

            //                this.ClientScript.RegisterStartupScript(this.GetType(), "SmallContentsRedirect", st.ToString(), true);
            //                break;
            //        }
            //    }
        }


        protected void HistoricoLogin()
        {
            if (Request.IsLocal)
                return;

            HistoricoLogin histlogin = new HistoricoLogin();
            histlogin.Inicialize();

            histlogin.IdUsuario = usuario;
            histlogin.DataLogin = DateTime.Now;
            histlogin.IPRemoteComputer = Request.UserHostAddress.ToString();
            histlogin.RemoteComputerName = Request.UserHostName.ToString();
            histlogin.Browser = Request.UserAgent.ToString();
            histlogin.NomeBrowser = Request.Browser.Browser.ToString();
            histlogin.VersaoBrowser = Request.Browser.Version.ToString();
            histlogin.IsBetaBrowser = Request.Browser.Beta;
            histlogin.hasSuportCookies = Request.Browser.Cookies;
            histlogin.hasSuportFrames = Request.Browser.Frames;
            histlogin.hasSuportJavaApplets = Request.Browser.JavaApplets;
            histlogin.hasSuportJavaScript = (Request.Browser.EcmaScriptVersion.Major >= 1);
            histlogin.hasSuportVBScript = Request.Browser.VBScript;
            histlogin.IsWin16BasedComputer = Request.Browser.Win16;
            histlogin.IsWin32BasedComputer = Request.Browser.Win32;
            histlogin.CLRVersion = Request.Browser.ClrVersion.ToString();
            histlogin.Plataforma = Request.Browser.Platform.ToString();

            histlogin.Save();
        }

        protected void AppendCookie()
        {
            HttpCookie coockie = FormsAuthentication.GetAuthCookie(usuario.NomeUsuario.ToString(), false);

            if (!Request.IsLocal)
                coockie.Domain = "ilitera.net";

            Response.AppendCookie(coockie);
        }

        protected string GetGuid()
        {
            return Guid.NewGuid().ToString()
                    + Guid.NewGuid().ToString()
                    + Guid.NewGuid().ToString();
        }

        protected void PermissaoEdicaoExame(ExameBase exame)
        { 
            string AvisoPrazo = string.Empty, AvisoPermissao = string.Empty, AvisoAutorizacao = string.Empty;
            bool VerificaMedico = false;

            if (exame.IdExameDicionario.mirrorOld == null)
                exame.IdExameDicionario.Find();

            switch (exame.IdExameDicionario.IndExame)
            {
                case (int)IndTipoExame.Clinico:
                    AvisoPrazo = "O prazo de 2 horas para a edição do exame clínico se encerrou! Não é mais possível editar o exame!";
                    AvisoAutorizacao = String.Format("O usuário {0} não possui autorização para visualizar detalhes deste exame clínico!", usuario.NomeUsuario);
                    AvisoPermissao = String.Format("O usuário {0} não é do tipo médico e não possui permissão para visualizar detalhes deste exame clínico!" ,usuario.NomeUsuario);
                    VerificaMedico = true;
                    break;

                case (int)IndTipoExame.Audiometrico:
                    AvisoPrazo = "O prazo de 2 horas para a edição do exame audiométrico se encerrou! Não é mais possível editar o exame!";
                    AvisoPermissao = String.Format("O usuário {0} não é do tipo médico e não possui permissão para visualizar detalhes deste exame audiométrico!", usuario.NomeUsuario);
                    VerificaMedico = false;
                    break;

                case (int)IndTipoExame.Complementar:
                    AvisoPrazo = "O prazo de 2 horas para a edição do exame complementar se encerrou! Não é mais possível editar o exame!";
                    AvisoPermissao = String.Format("O usuário {0} não é do tipo médico e não possui permissão para visualizar detalhes deste exame complementar!", usuario.NomeUsuario);
                    VerificaMedico = false;
                    break;

                case (int)IndTipoExame.NaoOcupacional:
                    AvisoPrazo = "O prazo de 2 horas para a edição do exame ambulatorial se encerrou! Não é mais possível editar o exame!";
                    AvisoAutorizacao = String.Format("O usuário {0} não possui autorização para visualizar detalhes deste exame ambulatorial!", usuario.NomeUsuario);
                    AvisoPermissao = String.Format("O usuário {0} não é do tipo médico e não possui permissão para visualizar detalhes deste exame ambulatorial!", usuario.NomeUsuario);
                    VerificaMedico = true;
                    break;
            }

            if (!usuario.NomeUsuario.Equals("Admin"))
            {
                switch (prestador.IndTipoPrestador)
                {
                    case (int)TipoPrestador.Medico:
                        if (!cliente.ContrataPCMSO.Equals((int)TipoPcmsoContratada.NaoContratada))
                        {
                            if (!exame.IdMedico.Id.Equals(0) &&
                                (exame.IdExameDicionario.IndExame.Equals((int)IndTipoExame.NaoOcupacional) ||
                                (!exame.IdExameDicionario.IndExame.Equals((int)IndTipoExame.NaoOcupacional) && !exame.IndResultado.Equals((int)ResultadoExame.NaoRealizado) &&
                                !exame.IndResultado.Equals((int)ResultadoExame.EmEspera))))
                                if (VerificaMedico)
                                {
                                    if (exame.IdMedico.Id.Equals(prestador.Id))
                                    {
                                        if (DateTime.Now > exame.DataCriacao.AddHours(2))
                                            throw new Exception(AvisoPrazo);
                                    }
                                    else
                                        throw new Exception(AvisoAutorizacao);
                                }
                                else
                                    if (DateTime.Now > exame.DataCriacao.AddHours(2))
                                        throw new Exception(AvisoPrazo);
                        }
                        break;
                    case (int)TipoPrestador.ContatoEmpresa:
                        if (!cliente.ContrataPCMSO.Equals((int)TipoPcmsoContratada.NaoContratada))
                            throw new Exception(AvisoPermissao);
                        break;
                    default:
                        throw new Exception(AvisoPermissao);
                }
            }
        }

        #endregion

        #region Report Methods

        protected static void CreatePDFDocument(ReportClass report, HttpResponse response)
        {
            CreatePDFDocument(report, response, false, string.Empty);
        }

        protected static void CreatePDFDocument(ReportClass report, HttpResponse response, bool forceDownload, string DownloadfileName)
        {
            report.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, response, forceDownload, DownloadfileName);
        }

        protected static void CreatePDFMerged(CrystalDecisions.CrystalReports.Engine.ReportClass[] reports, HttpResponse response, string watermark, bool RenumerarPaginas)
        {
            Stream[] streams = new Stream[reports.Length];

            int i = 0;

            foreach (CrystalDecisions.CrystalReports.Engine.ReportClass report in reports)
            {
                if (RenumerarPaginas)
                    report.ReportDefinition.ReportObjects["PaginaNdeN1"].ObjectFormat.EnableSuppress = true;

                streams[i] = report.ExportToStream(ExportFormatType.PortableDocFormat);

                report.Close();

                i++;
            }

            CreatePDFMerged(streams, response, watermark, RenumerarPaginas);
        }

        protected static void CreatePDFMerged(Stream[] streams, HttpResponse response, string watermark, bool RenumerarPaginas)
        {
            MemoryStream reportStream = PdfSharpUtility.MergeReports(streams, string.Empty, watermark, RenumerarPaginas);

            ShowPdfDocument(response, reportStream);
        }

        protected static void ShowPdfDocument(HttpResponse response, MemoryStream reportStream)
        {
            response.Clear();
            response.ContentType = "application/pdf";
            response.AddHeader("content-length", reportStream.Length.ToString());
            response.BinaryWrite(reportStream.ToArray()); ;
            response.Flush();
            reportStream.Close();
            response.End();
        }

        protected void OpenReport(string directory, string fileAndQuery, string ReportName)
        {
            this.OpenReport(directory, fileAndQuery, ReportName, false);
        }

        protected void OpenReport(string directory, string fileAndQuery, string ReportName, bool useDirectoryForLocalProcess)
        {
            StringBuilder st = new StringBuilder();

            Guid strAux = Guid.NewGuid();

            string valueProcess = "Local";

            if (valueProcess.Equals("Remote"))
                st.Append("void(window.open('http://report.ilitera.net/index.aspx?Identity=" + strAux.ToString() + "opsa" + strAux.ToString()
                    + "&Key=" + strAux.ToString() + "MestraNet" + strAux.ToString()
                    + "&PathAndQuery=" + HttpUtility.UrlEncode(directory + "/" + fileAndQuery) + "', '" + ReportName + "', 'scrollbars=yes, resizable=yes, toolbar=no, addressbar=no, menubar=no, width=800px, height=600px'));");
            else if (valueProcess.Equals("Local"))
            {
                if (useDirectoryForLocalProcess)
                {
                    //st.Append("void(window.open('../" + directory + "/" + fileAndQuery + "', '" + ReportName + "', 'scrollbars=yes, resizable=yes, toolbar=no, addressbar=no, menubar=no, width=800px, height=600px'));");
                    st.AppendFormat("void(window.open('../{0}/{1}', '{2}','scrollbars=yes, resizable=yes, toolbar=no, addressbar=no, menubar=no, width=800px, height=600px'));", directory, fileAndQuery, ReportName);
                }
                else
                {
                    //st.Append("void(window.open('" + fileAndQuery + "', '" + ReportName + "', 'scrollbars=yes, resizable=yes, toolbar=no, addressbar=no, menubar=no, width=800px, height=600px'));");
                    st.AppendFormat("void(window.open('{0}','{1}', 'scrollbars=yes, resizable=yes, toolbar=no, addressbar=no, menubar=no, width=800px, height=600px'));", fileAndQuery, ReportName);
                }
            }

            ScriptManager.RegisterStartupScript(this, this.GetType(), String.Format("OpenReport{0}", ReportName), st.ToString(), true);
        }

        protected static string strOpenReport(string directory, string fileAndQuery, string ReportName)
        {
            return strOpenReport(directory, fileAndQuery, ReportName, false);
        }

        protected static string strOpenReport(string directory, string fileAndQuery, string ReportName, bool useDirectoryForLocalProcess)
        {
            StringBuilder st = new StringBuilder();

            Guid strAux = Guid.NewGuid();

            string valueProcess = "Local";

            if (valueProcess.Equals("Remote"))
                st.Append("void(window.open('http://report.ilitera.net/index.aspx?Identity=" + strAux.ToString() + "opsa" + strAux.ToString()
                    + "&Key=" + strAux.ToString() + "MestraNet" + strAux.ToString()
                    + "&PathAndQuery=" + HttpUtility.UrlEncode(directory + "/" + fileAndQuery) + "', '" + ReportName + "', 'scrollbars=yes, resizable=yes, toolbar=no, addressbar=no, menubar=no, width=800px, height=600px').focus());");
            else if (valueProcess.Equals("Local"))
            {
                if (useDirectoryForLocalProcess)
                {
                    st.AppendFormat("void(window.open('../{0}/{1}', '{2}', 'scrollbars=yes, resizable=yes, toolbar=no, addressbar=no, menubar=no, width=800px, height=600px').focus());", directory, fileAndQuery, ReportName);
                }
                else
                {
                    st.AppendFormat("void(window.open('{0}','{1}', 'scrollbars=yes, resizable=yes, toolbar=no, addressbar=no, menubar=no, width=800px, height=600px').focus());", fileAndQuery, ReportName);
                }
            }

            return st.ToString();
        }

        #endregion

        #region ClientScripts Methods

        protected void PreencheLabels(string lb, string texto)
        {
            StringBuilder st = new StringBuilder();

            st.AppendFormat("top.window.document.frames.conteudo.window.document.getElementById('{0}').innerText = \"{1}\";", lb, texto);

            this.ClientScript.RegisterStartupScript(this.GetType(), "Label", st.ToString(), true);
        }

        protected void ResetIdValues()
        {
            StringBuilder st = new StringBuilder();

            st.Append("top.window.document.frames.conteudo.window.document.getElementById('txtIdEmpregado').value = \"\";");
            st.Append("top.window.document.frames.conteudo.window.document.getElementById('txtIdEmpresa').value = \"\";");

            this.ClientScript.RegisterStartupScript(this.GetType(), "ResetValues", st.ToString(), true);
        }

        protected void Aviso(string message)
        {
            this.Aviso(message, false);
        }

        protected void Aviso(string message, bool closeWin)
        {
            StringBuilder stError = new StringBuilder();
            stError.Append(message);
            stError.Replace("\'", string.Empty);
            stError.Replace("\r", string.Empty);
            stError.Replace("\n", string.Empty);

            StringBuilder st = new StringBuilder();

            //st.Append("window.alert(\"" + stError.ToString() + "\");");
            st.AppendFormat("window.alert(\"{0}\");", stError.ToString());

            if (closeWin)
                st.Append("self.close();");

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Aviso", st.ToString(), true);
        }

        protected void GetMenu(string menuType, string IdUsuario, string IdEmpresa, string IdEmpregado)
        {
            StringBuilder mntype = new StringBuilder();

            mntype.AppendFormat("top.window.document.frames.conteudo.window.document.frames.principal.window.document.frames.menu.window.location.href = 'MenuList.aspx?menutype={0}&IdUsuario={1}&IdEmpresa{2}&IdEmpregado{3}';", menuType, IdUsuario, IdEmpresa, IdEmpregado);

            this.ClientScript.RegisterStartupScript(this.GetType(), "MenuList", mntype.ToString(), true);
        }

        protected void GetMenu(string menuType, string IdUsuario, string IdEmpresa)
        {
            this.GetMenu(menuType, IdUsuario, IdEmpresa, string.Empty);
        }

        protected void GetMenu(string menuType, string IdUsuario)
        {
            this.GetMenu(menuType, IdUsuario, string.Empty, string.Empty);
        }

        #endregion

        #region Infragistics Methods and Class

        public class CustomDay
        {
            public int year = 0;
            public int month = 0;
            public int day = 0;
            public string url = "";
            public string back = "";

            public CustomDay(int year, int month, int day, string back, string url)
            {
                this.year = year;
                this.month = month;
                this.day = day;
                this.url = (url == null) ? string.Empty : url;
                this.back = (back == null) ? string.Empty : back;
            }
        }

        public class CustomDays : ArrayList
        {
            public void AddDay(DateTime date, string back, string url)
            {
                this.AddDay(date.Year, date.Month, date.Day, back, url);
            }
            public void AddDay(int year, int month, int day, string back, string url)
            {
                int days = this.Count;

                CustomDay d = null;

                while (days-- > 0)
                {
                    d = this[days] as CustomDay;
                    if (year == d.year && month == d.month && day == d.day)
                        break;
                }
                if (days >= 0)
                {
                    if ((back == null || back.Length == 0) && (url == null || url.Length == 0))
                        this.Remove(d);
                    else
                    {
                        d.back = (back == null) ? string.Empty : back;
                        d.url = (url == null) ? string.Empty : url;
                    }
                }
                else if ((back != null && back.Length > 0) || (url != null && url.Length > 0))
                    this.Add(new CustomDay(year, month, day, back, url));
            }

            public override string ToString()
            {
                StringBuilder str = new StringBuilder();
                str.Append("var customDays = [");
                bool first = true;
                foreach (CustomDay day in this)
                {
                    if (first) first = false;
                    else str.Append(",\n");
                    str.Append("[").Append(day.year).Append(",").Append(day.month).Append(",").Append(day.day).Append(",\"");
                    str.Append(day.back).Append("\",\"").Append(day.url).Append("\"]");
                }
                str.Append("];\n");
                return str.ToString();
            }
        }

        //protected static void SetUltraWebGridType(UltraWebGrid UltraWebGridListaEmpresa, bool isLocalTrabalho)
        //{
        //    if (isLocalTrabalho)
        //    {
        //        Infragistics.WebUI.UltraWebGrid.UltraGridBand band = new Infragistics.WebUI.UltraWebGrid.UltraGridBand(true);
        //        Infragistics.WebUI.UltraWebGrid.UltraGridColumn columnId = new Infragistics.WebUI.UltraWebGrid.UltraGridColumn(true);
        //        Infragistics.WebUI.UltraWebGrid.UltraGridColumn columnNome = new Infragistics.WebUI.UltraWebGrid.UltraGridColumn(true);

        //        columnId.Width = 0;
        //        columnId.Hidden = true;
        //        columnId.BaseColumnName = "IdLocalTrabalho";
        //        columnId.Key = "IdLocalTrabalho";

        //        columnNome.BaseColumnName = "NomeAbreviado";
        //        columnNome.Key = "NomeAbreviado";
        //        columnNome.Header.Caption = "Locais de Trabalho";
        //        columnNome.Width = 540;
        //        columnNome.CellStyle.HorizontalAlign = HorizontalAlign.Left;
        //        columnNome.CellStyle.Padding.Left = 3;
        //        columnNome.CellStyle.BorderDetails.StyleBottom = BorderStyle.None;
        //        columnNome.CellStyle.BorderDetails.StyleRight = BorderStyle.Solid;
        //        columnNome.CellStyle.BorderDetails.WidthRight = 2;
        //        columnNome.Header.Style.BorderDetails.StyleRight = BorderStyle.Solid;
        //        columnNome.Header.Style.BorderDetails.WidthRight = 2;

        //        band.Columns.Add(columnId);
        //        band.Columns.Add(columnNome);

        //        UltraWebGridListaEmpresa.Bands.Add(band);
        //        UltraWebGridListaEmpresa.Bands[0].Columns[1].CellStyle.BorderDetails.StyleLeft = BorderStyle.Solid;
        //        UltraWebGridListaEmpresa.Bands[0].Columns[1].Width = 187;
        //        UltraWebGridListaEmpresa.Bands[0].Columns[2].CellStyle.BorderDetails.StyleRight = BorderStyle.Solid;
        //        UltraWebGridListaEmpresa.Bands[0].Columns[2].CellStyle.BorderDetails.WidthRight = 2;
        //        UltraWebGridListaEmpresa.Bands[0].Columns[2].Header.Style.BorderDetails.StyleRight = BorderStyle.Solid;
        //        UltraWebGridListaEmpresa.Bands[0].Columns[2].Header.Style.BorderDetails.WidthRight = 2;
        //        UltraWebGridListaEmpresa.Bands[0].Columns[2].Width = 375;

        //        UltraWebGridListaEmpresa.Bands[1].RowExpAreaStyle.BackColor = Color.FromName("#F5FAF7");
        //        UltraWebGridListaEmpresa.Bands[1].HeaderStyle.BorderDetails.StyleLeft = BorderStyle.Solid;
        //        UltraWebGridListaEmpresa.Bands[1].HeaderStyle.BorderDetails.WidthLeft = 1;
        //        UltraWebGridListaEmpresa.Bands[1].HeaderStyle.BorderDetails.StyleTop = BorderStyle.Solid;
        //        UltraWebGridListaEmpresa.Bands[1].HeaderStyle.BorderDetails.WidthTop = 1;
        //    }
        //    else
        //    {
        //        if (UltraWebGridListaEmpresa.Bands.Count.Equals(2))
        //            UltraWebGridListaEmpresa.Bands.RemoveAt(1);
        //        UltraWebGridListaEmpresa.Bands[0].Columns[1].CellStyle.BorderDetails.StyleLeft = BorderStyle.None;
        //        UltraWebGridListaEmpresa.Bands[0].Columns[1].Width = 200;
        //        UltraWebGridListaEmpresa.Bands[0].Columns[2].CellStyle.BorderDetails.StyleRight = BorderStyle.None;
        //        UltraWebGridListaEmpresa.Bands[0].Columns[2].Header.Style.BorderDetails.StyleRight = BorderStyle.None;
        //        UltraWebGridListaEmpresa.Bands[0].Columns[2].Width = 400;
        //    }
        //}

        //protected static void PopulaUltraWebGrid(Infragistics.WebUI.UltraWebGrid.UltraWebGrid ultraWebGrid, DataSet ds, Label lblTotRegistros)
        //{
        //    ultraWebGrid.DataSource = ds;
        //    ultraWebGrid.DataBind();

        //    if (ds.Tables[0].Rows.Count > 0)
        //        lblTotRegistros.Text = "Total de registros: <b>" + ds.Tables[0].Rows.Count.ToString() + "</b>";
        //    else
        //        lblTotRegistros.Text = "Nenhum registro encontrado";
        //}

        //protected static void PopulaMenuList(Infragistics.WebUI.UltraWebListbar.UltraWebListbar UltraWebListbarMenu,
        //                                      int IndTipoAplicacao,
        //                                      string sMenuType,
        //                                      int IdUsuario)
        //{
        //    DataSet dsMenuTitulo = new MenuList().Get("IdTipoAplicacao=" + IndTipoAplicacao
        //                                            + " AND (hasIdPai IS NULL OR hasIdPai='')"
        //                                            + " ORDER BY Sequencia");

        //    foreach (DataRow rowTitulo in dsMenuTitulo.Tables[0].Select())
        //    {
        //        UltraWebListbarMenu.Groups.Add(rowTitulo["TextoItem"].ToString(), rowTitulo["TextoItem"].ToString());
        //        UltraWebListbarMenu.Groups.FromKey(rowTitulo["TextoItem"].ToString()).TextAlign = "center";
        //        UltraWebListbarMenu.Groups.FromKey(rowTitulo["TextoItem"].ToString()).ToolTip = rowTitulo["TextoItem"].ToString();

        //        if (rowTitulo["MenuImage"].ToString() != string.Empty)
        //        {
        //            UltraWebListbarMenu.Groups.FromKey(rowTitulo["TextoItem"].ToString()).HeaderAppearance.ExpandedAppearance.Image = rowTitulo["MenuImage"].ToString();
        //            if (rowTitulo["Sequencia"].ToString().Equals("0"))
        //            {
        //                UltraWebListbarMenu.Groups.FromKey(rowTitulo["TextoItem"].ToString()).HeaderAppearance.ExpandedAppearance.Style.Height = Unit.Pixel(22);
        //                UltraWebListbarMenu.Groups.FromKey(rowTitulo["TextoItem"].ToString()).HeaderAppearance.ExpandedAppearance.ExpansionIndicatorImage = "Biguparrows_white.gif";
        //                UltraWebListbarMenu.Groups.FromKey(rowTitulo["TextoItem"].ToString()).HeaderAppearance.CollapsedAppearance.ExpansionIndicatorImage = "Bigdownarrows_white.gif";
        //                UltraWebListbarMenu.Groups.FromKey(rowTitulo["TextoItem"].ToString()).HeaderAppearance.ExpandedAppearance.Style.BackgroundImage = "BigHeaderBackGreen.gif";
        //            }
        //        }

        //        if (rowTitulo["LinkNome"].ToString() != string.Empty)
        //        {
        //            UltraWebListbarMenu.Groups.FromKey(rowTitulo["TextoItem"].ToString()).TargetUrl = "javascript:SetLink('" + rowTitulo["LinkNome"].ToString() + "', '" + rowTitulo["TargetLinkNome"].ToString() + "', '" + rowTitulo["Width"].ToString() + "', '" + rowTitulo["Heigth"].ToString() + "', '" + rowTitulo["NomeJanela"].ToString() + "', '" + rowTitulo["TipoCloseWin"].ToString() + "');";
        //            UltraWebListbarMenu.Groups.FromKey(rowTitulo["TextoItem"].ToString()).HeaderAppearance.ExpandedAppearance.Style.Cursor = Infragistics.WebUI.Shared.Cursors.Hand;
        //            UltraWebListbarMenu.Groups.FromKey(rowTitulo["TextoItem"].ToString()).HeaderAppearance.ExpandedAppearance.ExpansionIndicatorImage = "TranspBack.gif";
        //            UltraWebListbarMenu.Groups.FromKey(rowTitulo["TextoItem"].ToString()).HeaderAppearance.CollapsedAppearance.ExpansionIndicatorImage = "TranspBack.gif";
        //            UltraWebListbarMenu.Groups.FromKey(rowTitulo["TextoItem"].ToString()).Expanded = false;
        //        }

        //        DataSet dsMenuItem = new MenuList().Get("IdTipoAplicacao=" + IndTipoAplicacao
        //            + " AND hasIdPai=" + rowTitulo["IdMenuList"]
        //            + " ORDER BY Sequencia");

        //        foreach (DataRow rowItem in dsMenuItem.Tables[0].Select())
        //        {
        //            UltraWebListbarMenu.Groups.FromKey(rowTitulo["TextoItem"].ToString()).Items.Add(rowItem["TextoItem"].ToString(), rowItem["TextoItem"].ToString());
        //            UltraWebListbarMenu.Groups.FromKey(rowTitulo["TextoItem"].ToString()).Items.FromKey(rowItem["TextoItem"].ToString()).ToolTip = rowItem["TextoItem"].ToString();
        //            UltraWebListbarMenu.Groups.FromKey(rowTitulo["TextoItem"].ToString()).Items.FromKey(rowItem["TextoItem"].ToString()).Align = "Left";
        //            if (sMenuType == ((int)IndMenuType.Disabled).ToString())
        //                UltraWebListbarMenu.Groups.FromKey(rowTitulo["TextoItem"].ToString()).Items.FromKey(rowItem["TextoItem"].ToString()).Enabled = false;
        //            else if (sMenuType == ((int)IndMenuType.Empresa).ToString())
        //            {
        //                if (Convert.ToBoolean(rowItem["IsAtivoToEmpresa"]))
        //                    UltraWebListbarMenu.Groups.FromKey(rowTitulo["TextoItem"].ToString()).Items.FromKey(rowItem["TextoItem"].ToString()).TargetUrl = "javascript:SetLink('" + rowItem["LinkNome"].ToString() + "', '" + rowItem["TargetLinkNome"].ToString() + "', '" + rowItem["Width"].ToString() + "', '" + rowItem["Heigth"].ToString() + "', '" + rowItem["NomeJanela"].ToString() + "', '" + rowItem["TipoCloseWin"].ToString() + "');";
        //                else
        //                    UltraWebListbarMenu.Groups.FromKey(rowTitulo["TextoItem"].ToString()).Items.FromKey(rowItem["TextoItem"].ToString()).Enabled = false;
        //            }
        //            else if (sMenuType == ((int)IndMenuType.Empregado).ToString())
        //            {
        //                if (Convert.ToBoolean(rowItem["IsAtivoToEmpregado"]))
        //                    UltraWebListbarMenu.Groups.FromKey(rowTitulo["TextoItem"].ToString()).Items.FromKey(rowItem["TextoItem"].ToString()).TargetUrl = "javascript:SetLink('" + rowItem["LinkNome"].ToString() + "', '" + rowItem["TargetLinkNome"].ToString() + "', '" + rowItem["Width"].ToString() + "', '" + rowItem["Heigth"].ToString() + "', '" + rowItem["NomeJanela"].ToString() + "', '" + rowItem["TipoCloseWin"].ToString() + "');";
        //                else
        //                    UltraWebListbarMenu.Groups.FromKey(rowTitulo["TextoItem"].ToString()).Items.FromKey(rowItem["TextoItem"].ToString()).Enabled = false;
        //            }
        //            else if (sMenuType == ((int)IndMenuType.AllEnabled).ToString())
        //                UltraWebListbarMenu.Groups.FromKey(rowTitulo["TextoItem"].ToString()).Items.FromKey(rowItem["TextoItem"].ToString()).TargetUrl = "javascript:SetLink('" + rowItem["LinkNome"].ToString() + "', '" + rowItem["TargetLinkNome"].ToString() + "', '" + rowItem["Width"].ToString() + "', '" + rowItem["Heigth"].ToString() + "', '" + rowItem["NomeJanela"].ToString() + "', '" + rowItem["TipoCloseWin"].ToString() + "');";
        //        }

        //        if (sMenuType == ((int)IndMenuType.Disabled).ToString())
        //        {
        //            UltraWebListbarMenu.Groups.FromKey(rowTitulo["TextoItem"].ToString()).Enabled = false;
        //            UltraWebListbarMenu.Groups.FromKey(rowTitulo["TextoItem"].ToString()).HeaderAppearance.ExpandedAppearance.Style.Cursor = Infragistics.WebUI.Shared.Cursors.Default;
        //        }
        //        else if (sMenuType == ((int)IndMenuType.Empresa).ToString())
        //        {
        //            if (Convert.ToBoolean(rowTitulo["IsAtivoToEmpresa"]))
        //                UltraWebListbarMenu.Groups.FromKey(rowTitulo["TextoItem"].ToString()).Enabled = true;
        //            else
        //            {
        //                UltraWebListbarMenu.Groups.FromKey(rowTitulo["TextoItem"].ToString()).Enabled = false;
        //                UltraWebListbarMenu.Groups.FromKey(rowTitulo["TextoItem"].ToString()).HeaderAppearance.ExpandedAppearance.Style.Cursor = Infragistics.WebUI.Shared.Cursors.Default;
        //            }
        //        }
        //        else if (sMenuType == ((int)IndMenuType.Empregado).ToString())
        //        {
        //            if (Convert.ToBoolean(rowTitulo["IsAtivoToEmpregado"]))
        //                UltraWebListbarMenu.Groups.FromKey(rowTitulo["TextoItem"].ToString()).Enabled = true;
        //            else
        //            {
        //                UltraWebListbarMenu.Groups.FromKey(rowTitulo["TextoItem"].ToString()).Enabled = false;
        //                UltraWebListbarMenu.Groups.FromKey(rowTitulo["TextoItem"].ToString()).HeaderAppearance.ExpandedAppearance.Style.Cursor = Infragistics.WebUI.Shared.Cursors.Default;
        //            }
        //        }
        //        else if (sMenuType == ((int)IndMenuType.AllEnabled).ToString())
        //            UltraWebListbarMenu.Groups.FromKey(rowTitulo["TextoItem"].ToString()).Enabled = true;
        //    }

        //    if (sMenuType == ((int)IndMenuType.Empresa).ToString() || sMenuType == ((int)IndMenuType.Empregado).ToString())
        //    {
        //        DataSet listaempresa = new Cliente().Lista(new Usuario(IdUsuario), string.Empty);

        //        if (listaempresa.Tables[0].Rows.Count != 1)
        //        {
        //            UltraWebListbarMenu.Groups.Add("Outra Empresa", "OutraEmpresa");
        //            UltraWebListbarMenu.Groups.FromKey("OutraEmpresa").TextAlign = "center";
        //            UltraWebListbarMenu.Groups.FromKey("OutraEmpresa").ToolTip = "Outra Empresa";
        //            UltraWebListbarMenu.Groups.FromKey("OutraEmpresa").TargetUrl = "javascript:TrocaEmp();";
        //            UltraWebListbarMenu.Groups.FromKey("OutraEmpresa").HeaderAppearance.ExpandedAppearance.Style.Cursor = Infragistics.WebUI.Shared.Cursors.Hand;
        //            UltraWebListbarMenu.Groups.FromKey("OutraEmpresa").HeaderAppearance.ExpandedAppearance.Images.ExpansionIndicatorImage.Url = "TranspBack.gif";
        //            UltraWebListbarMenu.Groups.FromKey("OutraEmpresa").HeaderAppearance.CollapsedAppearance.Images.ExpansionIndicatorImage.Url = "TranspBack.gif";
        //            UltraWebListbarMenu.Groups.FromKey("OutraEmpresa").Expanded = false;
        //            UltraWebListbarMenu.Groups.FromKey("OutraEmpresa").HeaderAppearance.ExpandedAppearance.Image = "change.gif";

        //            if (dsMenuTitulo.Tables[0].Rows.Count.Equals(0))
        //            {
        //                UltraWebListbarMenu.Groups.FromKey("OutraEmpresa").HeaderAppearance.ExpandedAppearance.Style.Height = Unit.Pixel(22);
        //                UltraWebListbarMenu.Groups.FromKey("OutraEmpresa").HeaderAppearance.ExpandedAppearance.Style.BackgroundImage = "BigBackChangeClient.gif";
        //            }
        //            else
        //                UltraWebListbarMenu.Groups.FromKey("OutraEmpresa").HeaderAppearance.ExpandedAppearance.Style.BackgroundImage = "BackChangeClient.gif";
        //        }
        //        else
        //        {

        //            Cliente clienteAcesso = new Cliente(Convert.ToInt32(listaempresa.Tables[0].Rows[0]["IdCliente"]));

        //            if (clienteAcesso.AtivarLocalDeTrabalho)
        //            {
        //                UltraWebListbarMenu.Groups.Add("Outra Empresa", "OutraEmpresa");
        //                UltraWebListbarMenu.Groups.FromKey("OutraEmpresa").TextAlign = "center";
        //                UltraWebListbarMenu.Groups.FromKey("OutraEmpresa").ToolTip = "Outra Empresa";
        //                UltraWebListbarMenu.Groups.FromKey("OutraEmpresa").TargetUrl = "javascript:TrocaEmp();";
        //                UltraWebListbarMenu.Groups.FromKey("OutraEmpresa").HeaderAppearance.ExpandedAppearance.Style.Cursor = Infragistics.WebUI.Shared.Cursors.Hand;
        //                UltraWebListbarMenu.Groups.FromKey("OutraEmpresa").HeaderAppearance.ExpandedAppearance.ExpansionIndicatorImage = "TranspBack.gif";
        //                UltraWebListbarMenu.Groups.FromKey("OutraEmpresa").HeaderAppearance.CollapsedAppearance.ExpansionIndicatorImage = "TranspBack.gif";
        //                UltraWebListbarMenu.Groups.FromKey("OutraEmpresa").Expanded = false;
        //                UltraWebListbarMenu.Groups.FromKey("OutraEmpresa").HeaderAppearance.ExpandedAppearance.Image = "change.gif";

        //                if (dsMenuTitulo.Tables[0].Rows.Count.Equals(0))
        //                {
        //                    UltraWebListbarMenu.Groups.FromKey("OutraEmpresa").HeaderAppearance.ExpandedAppearance.Style.Height = Unit.Pixel(22);
        //                    UltraWebListbarMenu.Groups.FromKey("OutraEmpresa").HeaderAppearance.ExpandedAppearance.Style.BackgroundImage = "BigBackChangeClient.gif";
        //                }
        //                else
        //                    UltraWebListbarMenu.Groups.FromKey("OutraEmpresa").HeaderAppearance.ExpandedAppearance.Style.BackgroundImage = "BackChangeClient.gif";
        //            }
        //        }
        //    }

        //    if (sMenuType == ((int)IndMenuType.Empregado).ToString())
        //    {
        //        UltraWebListbarMenu.Groups.Add("Outro Empregado", "OutroEmpregado");
        //        UltraWebListbarMenu.Groups.FromKey("OutroEmpregado").TextAlign = "center";
        //        UltraWebListbarMenu.Groups.FromKey("OutroEmpregado").ToolTip = "Outro Empregado";
        //        UltraWebListbarMenu.Groups.FromKey("OutroEmpregado").TargetUrl = "javascript:TrocaFunc();";
        //        UltraWebListbarMenu.Groups.FromKey("OutroEmpregado").HeaderAppearance.ExpandedAppearance.Style.Cursor = Infragistics.WebUI.Shared.Cursors.Hand;
        //        UltraWebListbarMenu.Groups.FromKey("OutroEmpregado").HeaderAppearance.ExpandedAppearance.ExpansionIndicatorImage = "TranspBack.gif";
        //        UltraWebListbarMenu.Groups.FromKey("OutroEmpregado").HeaderAppearance.CollapsedAppearance.ExpansionIndicatorImage = "TranspBack.gif";
        //        UltraWebListbarMenu.Groups.FromKey("OutroEmpregado").Expanded = false;
        //        UltraWebListbarMenu.Groups.FromKey("OutroEmpregado").HeaderAppearance.ExpandedAppearance.Style.BackgroundImage = "BackChangeClient.gif";
        //        UltraWebListbarMenu.Groups.FromKey("OutroEmpregado").HeaderAppearance.ExpandedAppearance.Image = "change.gif";
        //    }

        //    if (sMenuType.Equals("0") && dsMenuTitulo.Tables[0].Rows.Count.Equals(0))
        //        UltraWebListbarMenu.Visible = false;
        //}

        #endregion

        #region Methods to be Updated

        protected static bool VerificaCaracter(TextBox txtToValidade, Label errormessage)
        {
            ArrayList caracteres = new ArrayList();

            caracteres.Add("<");
            caracteres.Add(">");
            caracteres.Add("'");
            caracteres.Add("\"");
            caracteres.Add("+");
            caracteres.Add("%");
            caracteres.Add("&");
            caracteres.Add("*");

            foreach (string carac in caracteres)
            {
                if (txtToValidade.Text.IndexOf(carac) != -1)
                {
                    errormessage.Text = "O caracter " + carac + " não é válido!";
                    return false;
                }
            }

            errormessage.Text = string.Empty;
            return true;
        }

        protected static void VerificaCaracter(ServerValidateEventArgs args, TextBox txtToValidade, CustomValidator validamessage)
        {
            ArrayList caracteres = new ArrayList();

            caracteres.Add("<");
            caracteres.Add(">");
            caracteres.Add("'");
            caracteres.Add("\"");
            caracteres.Add("+");
            caracteres.Add("%");
            caracteres.Add("&");
            caracteres.Add("*");

            foreach (string carac in caracteres)
            {
                if (txtToValidade.Text.IndexOf(carac) != -1)
                {
                    validamessage.ErrorMessage = "O caracter " + carac + " não é válido!";
                    args.IsValid = false;
                    break;
                }
                else
                    args.IsValid = true;
            }
        }

        protected static void PopulaGrid(DataGrid dg, DataSet ds, Label lbl)
        {
            dg.DataSource = ds;
            dg.AllowPaging = true;

            try
            {
                dg.DataBind();
            }
            catch (Exception ex)// Se for problema com Current
            {
                lbl.Text = ex.Message;
            }

            if (dg.PageCount == 1)
            {
                dg.AllowPaging = false;
                dg.DataBind();
                lbl.Text = "Total de Registros: " + ds.Tables[0].Rows.Count.ToString();
            }
            if (dg.Items.Count == 0)
            {
                dg.Visible = false;
                lbl.Text = "Nenhum Registro Encontrado";
            }
            else
            {
                dg.Visible = true;
                lbl.Text = "Total de Registros: " + ds.Tables[0].Rows.Count.ToString();
            }
        }

        protected static void PopulaGrid(DataGrid dg, ArrayList al, Label lbl)
        {
            dg.DataSource = al;
            dg.AllowPaging = true;

            try
            {
                dg.DataBind();
            }
            catch (Exception ex)// Se for problema com Current
            {
                lbl.Text = ex.Message;
            }

            if (dg.PageCount == 1)
            {
                dg.AllowPaging = false;
                dg.DataBind();
                lbl.Text = "Total de Registros: " + al.Count.ToString();
            }
            if (dg.Items.Count == 0)
            {
                dg.Visible = false;
                lbl.Text = "Nennhum Registro Encontrado";
            }
            else
            {
                dg.Visible = true;
                lbl.Text = "Total de Registros: " + al.Count.ToString();
            }
        }

        protected DataSet GeraDataSetEPI(string tipolistagem, string IdEmpregado)
        {
            DataSet ds = new DataSet();
            DataSet dsReturn = new DataSet();
            DataRow newrow;
            int dias = 0;

            DataTable table = new DataTable("Default");
            table.Columns.Add("IdEPI", Type.GetType("System.String"));
            table.Columns.Add("Nome", Type.GetType("System.String"));
            table.Columns.Add("QtdEntregue", Type.GetType("System.String"));
            dsReturn.Tables.Add(table);

            if (tipolistagem == "EPIEmpregado")
                ds = new EPIClienteCA().GetEPIemUtilizacaoEmpregado(Convert.ToInt32(IdEmpregado));
            else if (tipolistagem == "EPITodos")
                ds = new EPIClienteCA().GetEPIemUtilizacaoTodos(this.cliente.Id);

            foreach (DataRow row in ds.Tables[0].Select())
            {
                bool hasIdEPI = false;

                switch (Convert.ToInt32(row["Periodicidade"]))
                {
                    case 0:
                        dias = Convert.ToInt32(row["NumPeriodicidade"]);
                        break;
                    case 1:
                        dias = Convert.ToInt32(row["NumPeriodicidade"]) * 31;
                        break;
                    case 2:
                        dias = Convert.ToInt32(row["NumPeriodicidade"]) * 365;
                        break;
                }

                foreach (DataRow rowaux in dsReturn.Tables[0].Select())
                {
                    if (rowaux["IdEPI"].ToString() == row["IdEPI"].ToString())
                    {
                        hasIdEPI = true;
                        if (DateTime.Now < Convert.ToDateTime(row["DataRecebimento"]).AddDays(dias * Convert.ToInt32(row["QtdEntregue"])))
                            rowaux["QtdEntregue"] = Convert.ToInt32(rowaux["QtdEntregue"]) + Convert.ToInt32(row["QtdEntregue"]);
                        break;
                    }
                }

                if ((!hasIdEPI) && (DateTime.Now < Convert.ToDateTime(row["DataRecebimento"]).AddDays(dias * Convert.ToInt32(row["QtdEntregue"]))))
                {
                    newrow = dsReturn.Tables[0].NewRow();

                    newrow["IdEPI"] = row["IdEPI"];
                    newrow["Nome"] = row["Nome"];
                    newrow["QtdEntregue"] = row["QtdEntregue"];

                    dsReturn.Tables[0].Rows.Add(newrow);
                }
            }

            return dsReturn;
        }

        public string GetLimitarString(int Tamanho, string str)
        {
            if (str.Length > Tamanho)
                return str.Substring(0, (Tamanho - 2)) + "...";
            else
                return str.ToString();
        }

        public string GetFormatarData(Object data)
        {
            if (Utility.IsNull(data))
                return "__-__-____";
            else
                return Convert.ToDateTime(data).ToString("dd-MM-yyyy");
        }

        #endregion
    }
}