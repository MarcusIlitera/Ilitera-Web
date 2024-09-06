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
using System.Text;
using Entities;
using System.Collections.Generic;
using Ilitera.VasoCaldeira.Report;


namespace Ilitera.Net
{

    public partial class CheckLaudoEletricoPeriodos : System.Web.UI.Page
    {

       // private List<EmpregadoFuncao> listEmpregadoFuncao2;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {

                    try
                    {
                        string FormKey = this.Page.ToString().Substring(4);

                        Ilitera.Common.Funcionalidade funcionalidade = new Ilitera.Common.Funcionalidade();
                        funcionalidade.Find("ClassName='" + FormKey + "'");

                        if (funcionalidade.Id == 0)
                            throw new Exception("Formulário não cadastrado - " + FormKey);

                        Entities.Usuario xUser = (Entities.Usuario)Session["usuarioLogado"];
                        Ilitera.Common.Usuario.Permissao_Web(xUser.IdUsuario, funcionalidade.Id);
                    }

                    catch (Exception ex)
                    {
                        Session["Message"] = ex.Message;
                        Server.Transfer("~/Tratar_Excecao.aspx");
                        return;
                    }


                    if (Session["Empresa"] != null && Session["Empresa"].ToString() != String.Empty)
                    {
                        PopulaDDLLaudos();
                    }

                  

                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alerta", String.Format("alert('{0}');", ex.Message), true);
            }
        }

        private void PopulaDDLLaudos()
        {
             //DataSet ds = new LaudoTecnico().GetLaudosTecnicos_Data(Session["Empresa"].ToString());
            DataSet ds = new Ilitera.Opsa.Data.LaudoEletrico().Get(" IdCliente = " + Session["Empresa"].ToString() + " order by Data_Laudo desc ");

            ddlLaudos2.DataSource = ds;
            
            ddlLaudos2.DataValueField= "IdLaudoEletrico";
            ddlLaudos2.DataTextField = "Descricao";
            ddlLaudos2.DataBind();

            //ddlLaudos2.Items.Insert(0, new Infragistics.Web.UI.ListControls.DropDownItem("Selecione um Levantamento...", "0"));

            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlLaudos2.SelectedIndex = 0;               
            }
        }



        protected void lkbIntroducao_Click(object sender, EventArgs e)
        {
            //string zTipo = "O";

            if (ddlLaudos2.SelectedIndex < 0) return;

            Guid strAux = Guid.NewGuid();

            OpenReport("", "LaudoEletrico.aspx?IliteraSystem=" + strAux.ToString() + strAux.ToString()
                + "&IdEmpresa=" + Request["IdEmpresa"] + "&IdLaudoEletrico=" + ddlLaudos2.SelectedValue + "&IdUsuario=" + Request["IdUsuario"], "LaudoEletrico");

            return;

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
                    + "&Key=" + strAux.ToString() + "IliteraNet" + strAux.ToString()
                    + "&PathAndQuery=" + HttpUtility.UrlEncode(directory + "/" + fileAndQuery) + "', '" + ReportName + "', 'scrollbars=yes, resizable=yes, toolbar=no, addressbar=no, menubar=no, width=800px, height=600px').focus());");
            else if (valueProcess.Equals("Local"))
            {
                if (useDirectoryForLocalProcess)
                {
                    //st.AppendFormat("void(window.open('../{0}/{1}', '{2}', 'scrollbars=yes, resizable=yes, toolbar=no, addressbar=no, menubar=no, width=800px, height=600px').focus());", directory, fileAndQuery, ReportName);
                    st.AppendFormat("void(window.open('{1}', '{2}', 'scrollbars=yes, resizable=yes, toolbar=no, addressbar=no, menubar=no, width=800px, height=600px').focus());", directory, fileAndQuery, ReportName);
                }
                else
                {
                    st.AppendFormat("void(window.open('{0}','{1}', 'scrollbars=yes, resizable=yes, toolbar=no, addressbar=no, menubar=no, width=800px, height=600px').focus());", fileAndQuery, ReportName);
                }
            }

            return st.ToString();
        }




        
    }
}
