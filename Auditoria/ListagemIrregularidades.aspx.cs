using System;
using System.Data;
using System.Web.SessionState;
using System.Web.UI.WebControls;
using Ilitera.Common;
using Ilitera.Opsa.Data;
using System.Text;

using System.Web;
using System.Web.UI;

using System.Web.UI.HtmlControls;

using System.Collections;
using Ilitera.Auditoria.Report;

using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;


namespace Ilitera.Net
{
	/// <summary>
	/// Summary description for ListaEmpresa.
	/// </summary>
    public partial class ListagemIrregularidades : System.Web.UI.Page
    {
        protected System.Web.UI.WebControls.Label lblEmpresa;

        protected void Page_Load(object sender, System.EventArgs e)
        {
            //InicializaWebPageObjects();
            

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

                //GetMenu(((int)IndMenuType.Empresa).ToString(), Request["IdUsuario"].ToString(), Request["IdEmpresa"].ToString());
                //PreencheLabels("lblEmp", cliente.NomeAbreviado.ToString());
                PopulaDDLAuditoria();

                if (ddlAuditoria.Items.Count.Equals(0))
                {
                    Cliente cliente;
                    cliente = new Cliente(System.Convert.ToInt32(Session["Empresa"].ToString()));

                    lblError.Text = "A empresa " + cliente.NomeCompleto + " não possui nenhuma Auditoria!";
                    lblAuditoria.Visible = false;
                    ddlAuditoria.Visible = false;
                    lblBuscaIrregularidade.Visible = false;
                    txtIrregula.Visible = false;
                    btnLocalizar.Visible = false;
                    UltraWebGridIrregularidades.Visible = false;
                    lbtListaIrre.Visible = false;
                    rblFiltro.Visible = false;
                    lkbIntroducaoAuditoria.Visible = false;
                    lkbListagemIrregCompleta.Visible = false;
                    lkbListagemIrregSimples.Visible = false;
                    imbAuditoria.Visible = false;
                }
                else
                {
                    UltraWebGridIrregularidades.DataSource = GeraDataSet();
                    UltraWebGridIrregularidades.DataBind();
                }
            }
        }

        private void PopulaDDLAuditoria()
        {
            Cliente cliente;
            cliente = new Cliente(System.Convert.ToInt32(Session["Empresa"].ToString()));

            DataSet dsAuditoria = new Ilitera.Opsa.Data.Auditoria().Get("IdCliente=" + cliente.Id
                + " AND IdPedido IN (SELECT IdPedido FROM Pedido WHERE IdPedidoGrupo IN (SELECT IdPedidoGrupo FROM PedidoGrupo WHERE DataConclusao IS NOT NULL))"
                + " ORDER BY DataLevantamento DESC");

            DataRow row;
            DataTable tableAuditoria = new DataTable();
            tableAuditoria.Columns.Add("IdAuditoria", Type.GetType("System.String"));
            tableAuditoria.Columns.Add("DataLevantamento", Type.GetType("System.String"));

            foreach (DataRow rowAuditoria in dsAuditoria.Tables[0].Select())
            {
                row = tableAuditoria.NewRow();
                row["IdAuditoria"] = rowAuditoria["IdAuditoria"];
                row["DataLevantamento"] = Convert.ToDateTime(rowAuditoria["DataLevantamento"]).ToString("dd-MM-yyyy");
                tableAuditoria.Rows.Add(row);
            }

            ddlAuditoria.DataSource = tableAuditoria;
            ddlAuditoria.DataValueField = "IdAuditoria";
            ddlAuditoria.DataTextField = "DataLevantamento";
            ddlAuditoria.DataBind();
        }

        protected void btnLocalizar_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            lblError.Text = string.Empty;

            if (Page.IsValid)
            {
                //UltraWebGridIrregularidades.DisplayLayout.Pager.CurrentPageIndex = 1;
                UltraWebGridIrregularidades.DataSource = GeraDataSet();
                UltraWebGridIrregularidades.DataBind();
            }
        }

        private DataSet GeraDataSet()
        {
            DataSet ds = new DataSet();
            DataRow row;
            DataTable table = new DataTable();
            table.Columns.Add("IdIrregularidade", Type.GetType("System.String"));
            table.Columns.Add("Norma", Type.GetType("System.String"));
            table.Columns.Add("Local", Type.GetType("System.String"));
            table.Columns.Add("DescricaoIrregularidade", Type.GetType("System.String"));
            //table.Columns.Add("Pdf", Type.GetType("System.String"));
            ds.Tables.Add(table);

            ArrayList alIrregularidades = new ArrayList();

            switch (rblFiltro.SelectedItem.Value)
            {
                case "T":
                    alIrregularidades = new Irregularidade().FindIrregularidades(ddlAuditoria.SelectedValue, txtIrregula.Text.Trim());
                    break;
                case "R":
                    alIrregularidades = new Irregularidade().FindIrregularidades(ddlAuditoria.SelectedValue, txtIrregula.Text.Trim(), (int)IndIrregularidade.Regularizadas);
                    break;
                case "P":
                    alIrregularidades = new Irregularidade().FindIrregularidades(ddlAuditoria.SelectedValue, txtIrregula.Text.Trim(), (int)IndIrregularidade.NaoRegularizadas);
                    break;
            }

            Guid strAux = Guid.NewGuid();
                       

            foreach (Irregularidade irregularidade in alIrregularidades)
            {
                irregularidade.IdNorma.Find();

                row = ds.Tables[0].NewRow();
                row["IdIrregularidade"] = irregularidade.Id.ToString();
                row["Norma"] = irregularidade.IdNorma.CodigoItem;
                if (irregularidade.strLocalIrregularidade().Length > 30)
                    row["Local"] = irregularidade.strLocalIrregularidade().Substring(0, 29) + "...";
                else
                    row["Local"] = irregularidade.strLocalIrregularidade();
                if (irregularidade.strAcoesExecutar().Length > 111)
                    row["DescricaoIrregularidade"] = irregularidade.strAcoesExecutar().Substring(0, 109) + "...";
                else
                    row["DescricaoIrregularidade"] = irregularidade.strAcoesExecutar();
                //row["Pdf"] = @"<a href=""#""><img src=""img/print.gif"" border=0 alt=""Imprimir Irregularidade"" onClick=""javascript:" + strOpenReport("Auditoria", "Auditoria.aspx?MestraSystem=" + strAux.ToString() + strAux.ToString()
                //    + "&IdUsuario=" + Request["IdUsuario"] + "&IdEmpresa=" + Request["IdEmpresa"] + "&IdIrregularidade=" + irregularidade.Id + "&IdAuditoria=" + irregularidade.IdAuditoria.Id, "AuditoriaReport") + @"""></a>";
                ds.Tables[0].Rows.Add(row);
            }

            if (ds.Tables[0].Rows.Count > 0)
                lblTotRegistros.Text = "Total de Registros: <b>" + ds.Tables[0].Rows.Count.ToString() + "</b>";
            else
                lblTotRegistros.Text = "Nenhum Registro Encontrado";


            DataSet dsTransf = new DataSet(); //Declare a dataSet to be filled.

            ds.Tables[0].DefaultView.Sort = "Norma";

            dsTransf.Tables.Add(ds.Tables[0].DefaultView.ToTable());


           return dsTransf;
        }

        protected void cvdCaracteres_ServerValidate(object source, System.Web.UI.WebControls.ServerValidateEventArgs args)
        {
            //VerificaCaracter(args, txtIrregula, cvdCaracteres);
        }

        protected void ddlAuditoria_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            txtIrregula.Text = string.Empty;

            //UltraWebGridIrregularidades.DisplayLayout.Pager.CurrentPageIndex = 1;
            UltraWebGridIrregularidades.DataSource = GeraDataSet();
            UltraWebGridIrregularidades.DataBind();
        }

        protected void lbtListaIrre_Click(object sender, System.EventArgs e)
        {
            txtIrregula.Text = string.Empty;

            //UltraWebGridIrregularidades.DisplayLayout.Pager.CurrentPageIndex = 1;
            UltraWebGridIrregularidades.DataSource = GeraDataSet();
            UltraWebGridIrregularidades.DataBind();
        }

        protected void rblFiltro_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            //UltraWebGridIrregularidades.DisplayLayout.Pager.CurrentPageIndex = 1;

            UltraWebGridIrregularidades.DataSource = GeraDataSet();
            UltraWebGridIrregularidades.DataBind();
        }

        //protected void UltraWebGridIrregularidades_PageIndexChanged(object sender, Infragistics.WebUI.UltraWebGrid.PageEventArgs e)
        //{
        //    lblError.Text = string.Empty;

        //    if (VerificaCaracter(txtIrregula, lblError))
        //    {
        //        UltraWebGridIrregularidades.DataSource = GeraDataSet();
        //        UltraWebGridIrregularidades.DataBind();

        //        if (UltraWebGridIrregularidades.DisplayLayout.Pager.PageCount < e.NewPageIndex)
        //            UltraWebGridIrregularidades.DisplayLayout.Pager.CurrentPageIndex = UltraWebGridIrregularidades.DisplayLayout.Pager.PageCount;
        //        else
        //            UltraWebGridIrregularidades.DisplayLayout.Pager.CurrentPageIndex = e.NewPageIndex;

        //        UltraWebGridIrregularidades.DataBind();
        //    }
        //}

        protected void lkbListagemIrregSimples_Click(object sender, System.EventArgs e)
        {
            Guid strAux = Guid.NewGuid();

          
            if (lblTotRegistros.Text != "Nenhum Registro Encontrado")
            {

                Entities.Usuario xUsuario = (Entities.Usuario)Session["usuarioLogado"];

                OpenReport("Auditoria", "ListagemIrregula.aspx?IliteraSystem=" + strAux.ToString() + strAux.ToString()
                    + "&IdUsuario=" + xUsuario.IdUsuario + "&IdEmpresa=" + Session["Empresa"].ToString() + "&IdAuditoria=" + ddlAuditoria.SelectedValue + "&TipoListagem=" + rblFiltro.SelectedValue, "ListagemIrregula");

            }
            else
                MsgBox1.Show("Ilitera.Net", "Não há nenhuma Irregularidade para ser Listada!", null,
                new EO.Web.MsgBoxButton("OK"));


        }

        protected void lkbListagemIrregCompleta_Click(object sender, System.EventArgs e)
        {
            Guid strAux = Guid.NewGuid();

            //if (GeraDataSet().Tables[0].Rows.Count > 0)
            if ( lblTotRegistros.Text != "Nenhum Registro Encontrado" )
            {

                Entities.Usuario xUsuario = (Entities.Usuario)Session["usuarioLogado"];

                OpenReport("Auditoria", "ListagemIrregulaCompl.aspx?IliteraSystem=" + strAux.ToString() + strAux.ToString()
                    + "&IdUsuario=" + xUsuario.IdUsuario + "&IdEmpresa=" + Session["Empresa"].ToString() + "&IdAuditoria=" + ddlAuditoria.SelectedValue + "&TipoListagem=" + rblFiltro.SelectedValue, "ListagemIrregulaCompl");

            }
            else
                MsgBox1.Show("Ilitera.Net", "Não há nenhuma Irregularidade para ser Listada!", null,
                new EO.Web.MsgBoxButton("OK"));

        }

        protected void lkbIntroducaoAuditoria_Click(object sender, System.EventArgs e)
        {
            Guid strAux = Guid.NewGuid();

            //OpenReport("Auditoria", "AudIntroducao.aspx?MestraSystem=" + strAux.ToString() + strAux.ToString()
            //    + "&IdUsuario=" + usuario.Id + "&IdEmpresa=" + cliente.Id + "&IdAuditoria=" + ddlAuditoria.SelectedValue, "AudIntroducao");

            if (lblTotRegistros.Text != "Nenhum Registro Encontrado")
            {

                Entities.Usuario xUsuario = (Entities.Usuario)Session["usuarioLogado"];

                OpenReport("Auditoria", "AudIntroducao.aspx?IliteraSystem=" + strAux.ToString() + strAux.ToString()
                    + "&IdUsuario=" + xUsuario.IdUsuario + "&IdEmpresa=" + Session["Empresa"].ToString() + "&IdAuditoria=" + ddlAuditoria.SelectedValue + "&TipoListagem=" + rblFiltro.SelectedValue, "AudIntroducao");

            }
            else
                MsgBox1.Show("Ilitera.Net", "Não há nenhuma Irregularidade para ser Listada!", null,
                new EO.Web.MsgBoxButton("OK"));
        }

        protected void imbAuditoria_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {

            Guid strAux = Guid.NewGuid();

            //OpenReport("Auditoria", "AudIntroducao.aspx?MestraSystem=" + strAux.ToString() + strAux.ToString()
            //    + "&IdUsuario=" + usuario.Id + "&IdEmpresa=" + cliente.Id + "&IdAuditoria=" + ddlAuditoria.SelectedValue, "AudIntroducao");

            if (ddlAuditoria.SelectedIndex >= 0)
            {

                Entities.Usuario xUsuario = (Entities.Usuario)Session["usuarioLogado"];

                OpenReport("Auditoria", "ListagemAuditorias.aspx?IliteraSystem=" + strAux.ToString() + strAux.ToString()
                    + "&IdUsuario=" + xUsuario.IdUsuario + "&IdEmpresa=" + Session["Empresa"].ToString() + "&IdAuditoria=" + ddlAuditoria.SelectedValue , "AudIntroducao");

            }
            else
                MsgBox1.Show("Ilitera.Net", "Não há nenhuma Auditoria selecionada!", null,
                new EO.Web.MsgBoxButton("OK"));

            //try
            //{
            //    Ilitera.Opsa.Data.Auditoria auditoria = new Ilitera.Opsa.Data.Auditoria(Convert.ToInt32(ddlAuditoria.SelectedValue));

            //    Cliente cliente;
            //    cliente = new Cliente(System.Convert.ToInt32(Session["Empresa"].ToString()));

            //    Entities.Usuario xUsuario = (Entities.Usuario)Session["usuarioLogado"];
            //    Usuario usuario = new Usuario(xUsuario.IdUsuario);


            //    RptAuditoria2 report = new DataSourceAuditoria(cliente, Convert.ToInt32(ddlAuditoria.SelectedValue)).GetReport();

            //    Funcionalidade funcionalidade = Funcionalidade.GetFuncionalidade(FuncionalidadeTipo.Metodo, this.GetType(), "imbAuditoria_Click");
            //    ImpressaoDocUsuario.InsereRegistro(usuario, cliente, Convert.ToInt64(report.SummaryInfo.KeywordsInReport), true, funcionalidade);

            //    CreatePDFDocument(report, this.Response, true, "Auditoria" + cliente.NomeAbreviado + auditoria.DataLevantamento.Year);
            //}
            //catch (Exception ex)
            //{
            //    MsgBox1.Show("Ilitera.Net", ex.Message, null,
            //    new EO.Web.MsgBoxButton("OK"));
            //}
        }



        protected void UltraWebGridIrregularidades_ItemCommand(object sender, EO.Web.GridCommandEventArgs e)
        {


            if (e.CommandName.ToString().Trim() == "4")
            {
                //string zData = "";
                Guid strAux = Guid.NewGuid();


                //zData = e.Item.Cells[1].Value.ToString();

                Entities.Usuario xUsuario = (Entities.Usuario)Session["usuarioLogado"];                

                OpenReport("Auditoria", "Auditoria.aspx?IliteraSystem=" + strAux.ToString() + strAux.ToString()
                        + "&IdUsuario=" + xUsuario.IdUsuario + "&IdEmpresa=" + Session["Empresa"].ToString() + "&IdIrregularidade=" + e.Item.Key.ToString() + "&IdAuditoria=" + ddlAuditoria.SelectedValue.ToString(), "AuditoriaReport");

            }

            else if (e.CommandName.ToString().Trim() == "5")
            {

   			   StringBuilder st = new StringBuilder();

               Entities.Usuario xUsuario = (Entities.Usuario)Session["usuarioLogado"];

                st.Append("javascript:void(centerWin('DetalheIrregularidade.aspx?IdIrregularidade=" + e.Item.Key.ToString() + "&IdUsuario=" + xUsuario.IdUsuario + "&IdEmpresa=" + Session["Empresa"].ToString() + "',750,600,\'DetalheIrregularidade\'))");

               //this.ClientScript.RegisterStartupScript(this.GetType(), "Detalhes", st.ToString(), true);

               ScriptManager scriptManager = ScriptManager.GetCurrent(Page);
               ScriptManager.RegisterClientScriptBlock(
              this,
              this.GetType(),
              "DertalheIrregularidade",
              st.ToString(),
              true);

             this.ClientScript.RegisterStartupScript(this.GetType(), "DetalheIrregularidade", st.ToString(), true);

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
                //if (useDirectoryForLocalProcess)
                //{
                //    st.AppendFormat("void(window.open('../{0}/{1}', '{2}', 'scrollbars=yes, resizable=yes, toolbar=no, addressbar=no, menubar=no, width=800px, height=600px').focus());", directory, fileAndQuery, ReportName);
                //}
                //else
                //{
                    st.AppendFormat("void(window.open('{0}','{1}', 'scrollbars=yes, resizable=yes, toolbar=no, addressbar=no, menubar=no, width=800px, height=600px').focus());", fileAndQuery, ReportName);
               // }
            }


            return st.ToString();
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
