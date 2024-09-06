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



namespace Ilitera.Net
{
    public partial class CheckVasos : System.Web.UI.Page
    {
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
                        PopularGridVasosPressao();
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alerta", String.Format("alert('{0}');", ex.Message), true);
            }
        }



        private void PopularGridVasosPressao()
        {
            StringBuilder where = new StringBuilder();

            where.Append("IdCliente=" + Session["Empresa"].ToString());

            where.Append(" ORDER BY NumeroIdentificacao");



            DataSet ds = new VasoPressao().Get(where.ToString());


            //ddlLaudos2.Items.Add("Selecione um Mapa de Risco...");

            ddlLaudos2.DataSource = ds;
            ddlLaudos2.DataValueField = "IdVasoPressao";
            ddlLaudos2.DataTextField = "Descricao";
            ddlLaudos2.DataBind();
            

            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlLaudos2.SelectedIndex = 0;

                PopularInspecoesRealizadas();

                if (ddlInspecao.Items.Count > 0)
                {
                    ddlInspecao.Visible = true;
                    lblInspecao.Visible = true;
                    btnInspecao.Visible = true;
                    btnInspecaoNovo.Visible = true;
                }
            }

        }


     
                  



        protected void btnProjeto_Click(object sender, EventArgs e)
        {

            Guid strAux = Guid.NewGuid();


            if (ddlLaudos2.SelectedValue.ToString().Trim() == "0")
            {
                return;
            }


            OpenReport("", "Vasos.aspx?IliteraSystem=" + strAux.ToString() + strAux.ToString()
            + "&IdEmpresa=" + Request["IdEmpresa"] + "&IdVaso=" + ddlLaudos2.SelectedValue + "&IdUsuario=" + Request["IdUsuario"] + "&Tipo=1", "Vasos");

            


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
                    + "&Key=" + strAux.ToString() + "IliteraNet" + strAux.ToString()
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

        protected void ddlLaudos2_SelectedIndexChanged(object sender, EventArgs e)
        {

            if ( ddlLaudos2.SelectedIndex < 0 )
            {
                ddlInspecao.Visible = false;
                lblInspecao.Visible = false;
                btnInspecao.Visible = false;
            }
            else
            {

                PopularInspecoesRealizadas();

                if (ddlInspecao.Items.Count > 0)
                {
                    ddlInspecao.Visible = true;
                    lblInspecao.Visible = true;
                    btnInspecao.Visible = true;
                }

            }


        }



        private void PopularInspecoesRealizadas()
        {

            VasoPressao rVaso = new VasoPressao();
            rVaso.Find(" IdVasoPressao = " + ddlLaudos2.SelectedValue + " ");


            ddlInspecao.Items.Clear();

            string where = " IdVasoCaldeiraBase=" + rVaso.Id + " ORDER BY DataLevantamento DESC";

            DataSet vDs = new InspecaoVasoCaldeira().Get(where);

            ddlInspecao.DataSource = vDs;
            ddlInspecao.DataValueField = "IdInspecaoVasoCaldeira";
            ddlInspecao.DataTextField = "DataLevantamento";
            ddlInspecao.DataBind();


            if (vDs.Tables[0].Rows.Count > 0)
            {
                ddlInspecao.SelectedIndex = 0;
            }

        }

        protected void btnInspecao_Click(object sender, EventArgs e)
        {

            Guid strAux = Guid.NewGuid();


            if (ddlInspecao.SelectedIndex < 0)
            {
                return;
            }


            OpenReport("", "Vasos.aspx?IliteraSystem=" + strAux.ToString() + strAux.ToString()
            + "&IdEmpresa=" + Request["IdEmpresa"] + "&IdVaso=" + ddlInspecao.SelectedValue + "&IdUsuario=" + Request["IdUsuario"] + "&Tipo=2", "Vasos");

            return;
                       

        }

        protected void btnInspecaoNovo_Click(object sender, EventArgs e)
        {
            Guid strAux = Guid.NewGuid();


            if (ddlInspecao.SelectedIndex < 0)
            {
                return;
            }


            OpenReport("", "Vasos.aspx?IliteraSystem=" + strAux.ToString() + strAux.ToString()
            + "&IdEmpresa=" + Request["IdEmpresa"] + "&IdVaso=" + ddlInspecao.SelectedValue + "&IdUsuario=" + Request["IdUsuario"] + "&Tipo=12", "Vasos");

            return;
        }
    }
}
