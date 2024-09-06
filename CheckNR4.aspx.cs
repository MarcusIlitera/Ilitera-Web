using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Ilitera.Opsa.Data;
using CrystalDecisions.CrystalReports.Engine;
using System.Text;

namespace Ilitera.Net
{
    public partial class CheckNR4 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    string xUsuario = Session["usuarioLogado"].ToString();

                    //try
                    //{
                    //    string FormKey = this.Page.ToString().Substring(4);

                    //    Mestra.Common.Funcionalidade funcionalidade = new Mestra.Common.Funcionalidade();
                    //    funcionalidade.Find("ClassName='" + FormKey + "'");

                    //    if (funcionalidade.Id == 0)
                    //        throw new Exception("Formulário não cadastrado - " + FormKey);

                    //    Entities.Usuario xUser = (Entities.Usuario)Session["usuarioLogado"];
                    //    Mestra.Common.Usuario.Permissao_Web(xUser.IdUsuario, funcionalidade.Id);
                    //}

                    //catch (Exception ex)
                    //{
                    //    Session["Message"] = ex.Message;
                    //    Server.Transfer("~/Tratar_Excecao.aspx");
                    //    return;
                    //}
                    if (Session["Empresa"] != null && Session["Empresa"].ToString() != String.Empty)
                    {
                        txt_Ano.Text = DateTime.Now.Year.ToString().Trim();
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alerta", String.Format("alert('{0}');", ex.Message), true);
            }
        }

      

        protected void lkbCronograma_Click(object sender, EventArgs e)
        {
            try
            {
                AbreLaudoerg("QuadroNR4");
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alerta", String.Format("alert('{0}');", ex.Message), true);
            }
        }

       

        private void AbreLaudoerg(string page)
        {
            if (txt_Ano.Text.Trim() == String.Empty)
            {
                //Aviso("É necessário indicar o ano do levantamento!");
            }
            else
            {
                Guid strAux = Guid.NewGuid();

                OpenReport("", page + ".aspx?IliteraSystem=" + strAux.ToString() + strAux.ToString()
                    + "&IdEmpresa=" + Session["Empresa"] + "&Ano=" + txt_Ano.Text.Trim() + "&IdUsuario=" + Request["IdUsuario"], page);
            }
        }


        protected static void CreatePDFDocument(ReportClass report, HttpResponse response)
        {
            CreatePDFDocument(report, response, false, string.Empty);
        }

        protected static void CreatePDFDocument(ReportClass report, HttpResponse response, bool forceDownload, string DownloadfileName)
        {
            report.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, response, forceDownload, DownloadfileName);
        }


        protected void OpenReport(string directory, string fileAndQuery, string ReportName)
        {
            this.OpenReport(directory, fileAndQuery, ReportName, true);
        }

        protected void OpenReport(string directory, string fileAndQuery, string ReportName, bool useDirectoryForLocalProcess)
        {
            StringBuilder st = new StringBuilder();

            Guid strAux = Guid.NewGuid();

            string valueProcess = "Local";

            if (valueProcess.Equals("Remote"))
                //st.Append("void(window.open('http://report.ilitera.net/index.aspx?Identity=" + strAux.ToString() + "opsa" + strAux.ToString()
                //    + "&Key=" + strAux.ToString() + "MestraNet" + strAux.ToString()
                //    + "&PathAndQuery=" + HttpUtility.UrlEncode(directory + "/" + fileAndQuery) + "', '" + ReportName + "', 'scrollbars=yes, resizable=yes, toolbar=no, addressbar=no, menubar=no, width=800px, height=600px'));");
                System.Diagnostics.Debug.WriteLine("");
            else if (valueProcess.Equals("Local"))
            {
                if (useDirectoryForLocalProcess)
                {
                    //st.Append("void(window.open('../" + directory + "/" + fileAndQuery + "', '" + ReportName + "', 'scrollbars=yes, resizable=yes, toolbar=no, addressbar=no, menubar=no, width=800px, height=600px'));");
                    //st.AppendFormat("void(window.open('../{0}/{1}', '{2}','scrollbars=yes, resizable=yes, toolbar=no, addressbar=no, menubar=no, width=800px, height=600px'));", directory, fileAndQuery, ReportName);
                    st.AppendFormat("void(window.open('{1}', '{2}','scrollbars=yes, resizable=yes, toolbar=no, addressbar=no, menubar=no, width=800px, height=600px'));", directory, fileAndQuery, ReportName);
                }
                else
                {
                    //st.Append("void(window.open('" + fileAndQuery + "', '" + ReportName + "', 'scrollbars=yes, resizable=yes, toolbar=no, addressbar=no, menubar=no, width=800px, height=600px'));");
                    st.AppendFormat("void(window.open('{0}','{1}', 'scrollbars=yes, resizable=yes, toolbar=no, addressbar=no, menubar=no, width=800px, height=600px'));", fileAndQuery, ReportName);
                }
            }

            ScriptManager.RegisterStartupScript(this, this.GetType(), String.Format("OpenReport{0}", ReportName), st.ToString(), true);
        }

    }
}
