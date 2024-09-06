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


namespace Ilitera.Net
{

    public partial class CheckPGRPeriodos : System.Web.UI.Page
    {

        private List<EmpregadoFuncao> listEmpregadoFuncao2;


        protected void InicializaWebPageObjects()
        {
            ////base.InicializaWebPageObjects();
            //StringBuilder st = new StringBuilder();

            ////st.Append("top.window.document.frames.conteudo.window.document.getElementById('txtIdEmpregado').value = '" + empregado.Id + "';");
            //st.Append("top.window.document.getElementById('txtIdEmpregado').value = '" + Request["IdEmpregado"] + "';");
            //st.Append("top.window.document.getElementById('txtIdUsuario').value = '" + Request["IdUsuario"] + "';");
            //st.Append("top.window.document.getElementById('txtIdEmpresa').value = '" + Request["IdEmpresa"] + "';");


            //this.ClientScript.RegisterStartupScript(this.GetType(), "IdEmpregado", st.ToString(), true);

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {

                    try
                    {
                        //string FormKey = this.Page.ToString().Substring(4);
                        string FormKey = "checkppraperiodos_aspx";  //usar mesmo acesso do PPRA

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
            //DataSet ds = new LaudoTecnico().GetLaudosTecnicos_Data_PGR(Session["Empresa"].ToString());

            ddlLaudos2.DataSource = ds;
            ddlLaudos2.DataValueField= "IdLaudoTecnico";
            ddlLaudos2.DataTextField = "DataLaudo";
            ddlLaudos2.DataBind();

            //ddlLaudos2.Items.Insert(0, new Infragistics.Web.UI.ListControls.DropDownItem("Selecione um Levantamento...", "0"));

            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlLaudos2.SelectedIndex = 0;
                
            }
        }

        protected void lkbIntroducao_Click(object sender, EventArgs e)
        {
            try
            {
                AbrePPRA("PGRIntroducao");
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alerta", String.Format("alert('{0}');", ex.Message), true);
            }            
        }

        private void AbrePPRA(string page)
        {
            Guid strAux = Guid.NewGuid();

            if (ddlLaudos2.SelectedValue == "0" || ddlLaudos2.SelectedValue.Trim() == String.Empty)
            {
                MsgBox1.Show("PGR","É necessário selecionar o Levantamento antes de emitir o documento!", null,
                                new EO.Web.MsgBoxButton("OK"));                
            }
            else
                OpenReport("", page + ".aspx?IliteraSystem=" + strAux.ToString() + strAux.ToString()
                    + "&IdEmpresa=" + Request["IdEmpresa"] + "&IdLaudoTecnico=" + ddlLaudos2.SelectedValue + "&IdUsuario=" + Request["IdUsuario"], page);
        }

        protected void lkbDocumentoBase_Click(object sender, EventArgs e)
        {
            try
            {
                AbrePPRA("PPRADocBase");
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alerta", String.Format("alert('{0}');", ex.Message), true);
            }            
        }

        protected void lkbPlanilha_Click(object sender, EventArgs e)
        {
            try
            {
                AbrePPRA("PlanilhaPGR");
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alerta", String.Format("alert('{0}');", ex.Message), true);
            }            
        }

        protected void lkbQuadroEPI_Click(object sender, EventArgs e)
        {
            try
            {
                AbrePPRA("QuadroEPI");
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alerta", String.Format("alert('{0}');", ex.Message), true);
            }            
        }

        protected void lkbEmpregado_Click(object sender, EventArgs e)
        {
            try
            {
                StringBuilder st = new StringBuilder();
                Guid strAux = Guid.NewGuid();

                Usuario user = (Usuario)Session["usuarioLogado"];

                if (Session["Empregado"] != null && Session["Empregado"].ToString() != String.Empty)
                {
                    Ilitera.Opsa.Data.Empregado empregado = new Ilitera.Opsa.Data.Empregado(Convert.ToInt32(Session["Empregado"]));

                    if (!empregado.gTERCEIRO)
                    {
                        st.Append(strOpenReport("Documentos", "LTCAT.aspx?IliteraSystem=" + strAux.ToString() + strAux.ToString()
                            + "&IdEmpresa=" + Session["Empresa"].ToString() + "&IdEmpregado=" + Session["Empregado"] + "&TipoLTCAT=PPRA&IdUsuario=" + user.IdUsuario, "PPRAEmpregado"));
                    }
                    else
                    {                        
                        MsgBox1.Show("PPRA", "O documento PPRA - Empregado não pode ser elaborado para este empregado! O empregado é Terceiro, Estagiário ou ainda Não Empregado!", null,
                                        new EO.Web.MsgBoxButton("OK"));                

                    }
                }
                else
                {
                    st.Append("if(confirm(\"O documento será impresso para todos os empregados da empresa! Para empresas com um grande número de funcionários este procedimento não é aconselhado! Neste caso, prefira imprimir o documento de cada empregado individualmente! Lembre-se que o documento de alguns funcionários poderão não ser visualizados devido à falta de avaliação quantitativa nos Laudos Técnicos ou à não atribuição do empregado ao respectivo GHE em cada um dos Laudos Técnicos!\"))");
                    st.Append(strOpenReport("Documentos", "LTCAT.aspx?IliteraSystem=" + strAux.ToString() + strAux.ToString()
                        + "&IdEmpresa=" + Session["Empresa"].ToString() + "&TipoLTCAT=PPRA&IdUsuario=" + user.IdUsuario.ToString(), "PPRAEmpregado"));
                }

                ScriptManager.RegisterStartupScript(this, this.GetType(), "PPRAEmpregado", st.ToString(), true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alerta", String.Format("alert('{0}');", ex.Message), true);
            }
        }

        protected void lkbPPRA_Click(object sender, EventArgs e)
        {
            try
            {
                AbrePPRA("PGRCompleto");
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alerta", String.Format("alert('{0}');", ex.Message), true);
            }            
        }


        protected void lkbPGRTR_Click(object sender, EventArgs e)
        {
            try
            {
                AbrePPRA("PGRTR");
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alerta", String.Format("alert('{0}');", ex.Message), true);
            }
        }




        protected void lkbEPI_Click(object sender, EventArgs e)
        {

            try
            {
                AbrePPRA("QuadroEPI2");
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alerta", String.Format("alert('{0}');", ex.Message), true);
            }            

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
            
        }


   

        private bool Checar_GFIP04(Ilitera.Opsa.Data.Empregado xEmpr)
        {

            GetEmpregadoFuncao(xEmpr);

            foreach (EmpregadoFuncao emprFunc in listEmpregadoFuncao2)
            {



                LaudoTecnico laudoTecnico = new LaudoTecnico();
                //laudoTecnico.FindMax("hDT_LAUDO", "nID_EMPR=" + cliente.Id + " AND nID_LAUD_TEC IN (SELECT nID_LAUD_TEC FROM " + Mestra.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblFUNC_EMPREGADO)");
                laudoTecnico = emprFunc.GetLaudoTecnico();

                Ghe ghe = new Ghe();

                GheEmpregado gheEmpregado = new GheEmpregado();
                gheEmpregado.Find("nID_LAUD_TEC=" + laudoTecnico.Id + " AND nID_EMPREGADO_FUNCAO=" + emprFunc.Id);

                ghe.Find(gheEmpregado.nID_FUNC.Id);


                if (emprFunc.nIND_GFIP == (int)CodigoGFIP.PPRA)
                {

                    if (ghe.GetGFIP() == "04")
                    {
                        return true;
                    }
                }
                else
                {

                    if (Ghe.GetGFIP(Convert.ToInt32((emprFunc.nIND_GFIP))) == "04")
                        return true;
                }

            }

            return false;

        }


        private List<EmpregadoFuncao> GetEmpregadoFuncao(Ilitera.Opsa.Data.Empregado zEmpr)
        {

            StringBuilder str = new StringBuilder();

            str.Append("nID_EMPREGADO=" + zEmpr.Id);

            str.Append(" AND nID_EMPREGADO_FUNCAO IN (SELECT nID_EMPREGADO_FUNCAO FROM tblFUNC_EMPREGADO WHERE nID_LAUD_TEC");
            str.Append(" IN (SELECT nID_LAUD_TEC FROM tblLAUDO_TEC WHERE nID_PEDIDO");
            str.Append(" IN (SELECT IdPedido FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.Pedido WHERE DataConclusao IS NOT NULL) OR bAE=1))");


            //    str.Append(" AND (hDT_TERMINO>='" + dataInicioPPP.ToString("yyyy-MM-dd") + "' OR hDT_TERMINO IS NULL)");

            str.Append(" ORDER BY hDT_INICIO");

            listEmpregadoFuncao2 = new EmpregadoFuncao().Find<EmpregadoFuncao>(str.ToString());




            return listEmpregadoFuncao2;
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

        protected void lkbPPRAcomIndice_Click(object sender, EventArgs e)
        {
            try
            {
                AbrePPRA("PPRACompletoIndice");
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alerta", String.Format("alert('{0}');", ex.Message), true);
            }

        }


        private void PopulaAssinados()
        {

            Ilitera.Data.PPRA_EPI xRep = new Ilitera.Data.PPRA_EPI();
            DataSet zDs = xRep.Retornar_Repositorio_Tipo(System.Convert.ToInt32(Session["Empresa"].ToString()), "'W','5'"); //, 'X', 'Z'");

            grd_Clinicos.DataSource = zDs;
            grd_Clinicos.DataBind();

        }



        protected void grd_Clinicos_ItemCommand(object sender, EO.Web.GridCommandEventArgs e)
        {
           

            if (e.CommandName.ToString().Trim() == "3")
            {
                string xArquivo = e.Item.Cells[2].Value.ToString();

                if (xArquivo.ToUpper().IndexOf(".PDF") > 0 )
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
