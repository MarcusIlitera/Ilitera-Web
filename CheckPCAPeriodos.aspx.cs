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



namespace Ilitera.Net
{

    public partial class CheckPCAPeriodos : System.Web.UI.Page
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
                        PopulaDDLLaudos();
                        PopulaAssinados();
                    }

                    //if (Session["Empregado"].ToString().Trim() != "")
                    //{
                    //    Ilitera.Opsa.Data.Empregado empregado = new Ilitera.Opsa.Data.Empregado(Convert.ToInt32(Session["Empregado"]));

                    //    if (Checar_GFIP04(empregado) == true)
                    //    {
                    //        btnEmpregado.Enabled = false;
                    //        return;
                    //    }
                    //}

                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alerta", String.Format("alert('{0}');", ex.Message), true);
            }
        }

        private void PopulaDDLLaudos()
        {
            DataSet ds = new LaudoTecnico().GetLaudosTecnicos_Data(Session["Empresa"].ToString());

            ddlLaudos2.DataSource = ds;
            ddlLaudos2.DataValueField= "IdLaudoTecnico";
            ddlLaudos2.DataTextField = "DataLaudo";
            ddlLaudos2.DataBind();

            //ddlLaudos2.Items.Insert(0, new Infragistics.Web.UI.ListControls.DropDownItem("Selecione um Levantamento...", "0"));

            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlLaudos2.SelectedIndex = 0;
                Ajustar_Botao_PCA();
            }
        }



        private void AbrePPRA(string page)
        {
            Guid strAux = Guid.NewGuid();

            if (ddlLaudos2.SelectedValue == "0" || ddlLaudos2.SelectedValue.Trim() == String.Empty)
            {
                MsgBox1.Show("PPRA","É necessário selecionar o Levantamento antes de emitir o documento!", null,
                                new EO.Web.MsgBoxButton("OK"));                
            }
            else
                OpenReport("", page + ".aspx?IliteraSystem=" + strAux.ToString() + strAux.ToString()
                    + "&IdEmpresa=" + Request["IdEmpresa"] + "&IdLaudoTecnico=" + ddlLaudos2.SelectedValue + "&IdUsuario=" + Request["IdUsuario"], page);
        }

    




        protected void lkbPCA_Click(object sender, EventArgs e)
        {

            try
            {
                AbrePPRA("PCA");
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alerta", String.Format("alert('{0}');", ex.Message), true);
            }            


        }

        protected void lkbPPR_Click(object sender, EventArgs e)
        {
            
            try
            {
                AbrePPRA("PPR");
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alerta", String.Format("alert('{0}');", ex.Message), true);
            }            

        }

        protected void ddlLaudos2_SelectedIndexChanged(object sender, EventArgs e)
        {
            Ajustar_Botao_PCA();
        }


        private void Ajustar_Botao_PCA()
        {
            if (ddlLaudos2.SelectedValue.ToString().Trim() == "0")
            {
                btnPCA.Enabled = false;
            }
            else
            {
                btnPCA.Enabled = true;
            }


            LaudoTecnico laudo = new LaudoTecnico(Convert.ToInt32(ddlLaudos2.SelectedValue.ToString()));
            //Cliente xCliente = new Cliente(  System.Convert.ToInt32( Session["Empresa"].ToString()));

            //checar se há algum GHE acima dos 80dB
            DataSet ds = new Ghe().Get("nID_LAUD_TEC=" + laudo.Id + " ORDER BY tNO_FUNC");

            Boolean zLoc = false;

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                //row["tNO_FUNC"].ToString();
                DataSet ds2 = new PPRA().Get("nID_FUNC=" + row["nId_FUNC"].ToString() + " and nId_Rsc = 0 and tVL_Med >=80 ");

                foreach (DataRow row2 in ds2.Tables[0].Rows)
                {
                    zLoc = true;
                    break;
                }

                if (zLoc == true) break;

            }

            if (zLoc == false)
            {
                btnPCA.Attributes.Add("OnClick", "return confirm('Como não existem colaboradores expostos à um ambiente que prejudique a saúde auditiva, ou seja, acima dos níveis de ação de 80dB(A), não há necessidade de elaboração do Programa de Conservação Auditiva.  Deseja imprimir assim mesmo ?');");
            }
            else
            {
                btnPCA.Attributes.Add("OnClick", "return true;");
            }

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

        //protected void lkbTeste_Click(object sender, EventArgs e)
        //{

        //    WebRequest req = null;
        //    WebResponse rsp = null;
        //    try
        //    {
        //        string fileName = "I:\\temp\\Exame.xml";
        //        //string uri = "http://localhost:46870/Comunicacao.aspx";

        //        string uri = "https://www.ilitera.net.br/essence_hom/Comunicacao.aspx";
        //        req = WebRequest.Create(uri);
        //        //req.Proxy = WebProxy.GetDefaultProxy(); // Enable if using proxy
        //        req.Method = "POST";        // Post method
        //        req.ContentType = "text/xml";     // content type
        //                                          // Wrap the request stream with a text-based writer
        //        StreamWriter writer = new StreamWriter(req.GetRequestStream());
        //        // Write the XML text into the stream
        //        writer.WriteLine(this.GetTextFromXMLFile(fileName));
        //        writer.Close();
        //        // Send the data to the webserver
        //        rsp = req.GetResponse();




        //    }
        //    catch (WebException webEx)
        //    {

        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //    finally
        //    {
        //       // if (req != null) req.GetRequestStream().Close();
        //       // if (rsp != null) rsp.GetResponseStream().Close();
        //    }
        //}

        //private string GetTextFromXMLFile(string file)
        //{
        //    StreamReader reader = new StreamReader(file);
        //    string ret = reader.ReadToEnd();
        //    reader.Close();
        //    return ret;
        //}



        private void PopulaAssinados()
        {

            Ilitera.Data.PPRA_EPI xRep = new Ilitera.Data.PPRA_EPI();
            DataSet zDs = xRep.Retornar_Repositorio_Tipo(System.Convert.ToInt32(Session["Empresa"].ToString()), "'X', 'Z'");

            grd_Clinicos.DataSource = zDs;
            grd_Clinicos.DataBind();

        }



        protected void grd_Clinicos_ItemCommand(object sender, EO.Web.GridCommandEventArgs e)
        {


            if (e.CommandName.ToString().Trim() == "3")
            {
                string xArquivo = e.Item.Cells[2].Value.ToString();

                if (xArquivo.ToUpper().IndexOf(".PDF") > 0)
                {
                    System.Net.WebClient client = new System.Net.WebClient();
                    Byte[] buffer = client.DownloadData(xArquivo);
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("Content-Disposition", "attachment; filename=" + xArquivo);
                    Response.AddHeader("content-length", buffer.Length.ToString());
                    Response.BinaryWrite(buffer);
                    Response.End();
                }

            }



        }


    }
}
