using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using Ilitera.Common;
using Ilitera.Data;
using Ilitera.Data.SQLServer;
using Ilitera.Opsa.Data;
using System.Data.SqlClient;
using System.Text;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

using System.IO;




namespace Ilitera.Net
{
    /// <summary>
	/// 
	/// </summary>
	public partial class HistoricoEntrega : System.Web.UI.Page
	{

        protected Usuario usuario = new Usuario();
        protected Cliente cliente = new Cliente();
	
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

                Entities.Usuario user = (Entities.Usuario)Session["usuarioLogado"];

                int xIdEmpresa = System.Convert.ToInt32(Session["Empresa"]);
                int xIdUsuario = user.IdUsuario;

                lbl_Id_Empresa.Text = xIdEmpresa.ToString().Trim();
                lbl_Id_Usuario.Text = xIdUsuario.ToString().Trim();

                dt_1.VisibleDate = System.DateTime.Now.AddMonths(-2);
                dt_1.SelectedDate = System.DateTime.Now.AddMonths(-2);
                dt_1.TodaysDate = System.DateTime.Now.AddMonths(-2);
                 

                dt_2.VisibleDate = System.DateTime.Now.AddDays(1);
                dt_2.SelectedDate = System.DateTime.Now.AddDays(1);
                dt_2.TodaysDate = System.DateTime.Now.AddDays(1);
                

                PopulaDropDownEmpregado();
            }
            //if (ddlEmpregado.SelectedItem.Value != "0")
            //    btnRelaEmp.Visible = true;
            //else
            //    btnRelaEmp.Visible = false;
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            Guid strAux = Guid.NewGuid();


            Entities.Usuario user = (Entities.Usuario)Session["usuarioLogado"];

            int xIdEmpresa = System.Convert.ToInt32(Session["Empresa"]);
            int xIdUsuario = user.IdUsuario;

            lbl_Id_Empresa.Text = xIdEmpresa.ToString().Trim();
            lbl_Id_Usuario.Text = xIdUsuario.ToString().Trim();


			//this.imbRelaTodos.Click += new System.Web.UI.ImageClickEventHandler(this.imbRelaTodos_Click);
            imbRelaTodos.Attributes.Add("onclick", "void(addItem(centerWin('RelatorioComplTodosEntEPI.aspx?IliteraSystem=" + strAux.ToString() + strAux.ToString() + "&IdUsuario=" + lbl_Id_Usuario.Text + "&IdEmpresa=" + lbl_Id_Empresa.Text + "',1000,700,\'DetalheCA\'),\'Todos\'))");
                        
            
            //this.UltraWebGridDataEntrega.ClickCellButton += new Infragistics.WebUI.UltraWebGrid.ClickCellButtonEventHandler(this.UltraWebGridDataEntrega_ClickCellButton);
            //this.UltraWebGridDataEntrega.PageIndexChanged += new Infragistics.WebUI.UltraWebGrid.PageIndexChangedEventHandler(this.UltraWebGridDataEntrega_PageIndexChanged);
            //this.UltraWebGridItemsEntrega.PageIndexChanged += new Infragistics.WebUI.UltraWebGrid.PageIndexChangedEventHandler(this.UltraWebGridItemsEntrega_PageIndexChanged);

		}
		#endregion

		private void PopulaDropDownEmpregado()
		{
            //ddlEmpregado.DataSource = new Empregado().Get("nID_EMPR=" + Convert.ToInt32(lbl_Id_Empresa.Text) +" ORDER BY tNO_EMPG");
            //ddlEmpregado.DataSource = new Empregado().Get(" nId_Empregado in ( select nId_Empregado from tblEmpregado_Funcao where hdt_Termino is null and nID_EMPR=" + Convert.ToInt32(lbl_Id_Empresa.Text) + " ) ORDER BY tNO_EMPG");
            ddlEmpregado.DataSource = new Empregado().Get(" nId_Empregado in ( select nId_Empregado from tblEmpregado_Funcao where nID_EMPR=" + Convert.ToInt32(lbl_Id_Empresa.Text) + " ) ORDER BY tNO_EMPG");
            ddlEmpregado.DataValueField= "nID_EMPREGADO";
			ddlEmpregado.DataTextField = "tNO_EMPG";
			ddlEmpregado.DataBind();
			ddlEmpregado.Items.Insert(0, new ListItem("Selecione o Empregado...","0"));
		}

		private DataSet dsData()
		{
			DataSet ds = new EPICAEntrega().Get("IdEmpregado=" + ddlEmpregado.SelectedItem.Value
				+" ORDER BY DataRecebimento");

			DataSet dsData = new DataSet();
			DataRow row;
			Guid strAux = Guid.NewGuid();


            imbRelaComp.Attributes.Add("onclick", "void(addItem(centerWin('RelatorioComplEntEPI.aspx?IliteraSystem=" + strAux.ToString() + strAux.ToString() + "&IdUsuario=" + lbl_Id_Usuario.Text + "&IdEmpresa=" + lbl_Id_Empresa.Text + "&IdEmpregado=" + ddlEmpregado.SelectedValue + "&D1=" + String.Format("{0:dd/MM/yyyy}",dt_1.SelectedDate) + "&D2=" + String.Format("{0:dd/MM/yyyy}",dt_2.SelectedDate) + "',1000,700,\'DetalheCA\'),\'Todos\'))");

            imbRelaRec.Attributes.Add("onclick", "void(addItem(centerWin('RelatorioEntregaEPI.aspx?IliteraSystem=" + strAux.ToString() + strAux.ToString() + "&IdUsuario=" + lbl_Id_Usuario.Text + "&IdEPICAEntrega=0&IdEmpresa=" + lbl_Id_Empresa.Text + "&IdEmpregado=" + ddlEmpregado.SelectedValue + "&D1=" + String.Format("{0:dd/MM/yyyy}", dt_1.SelectedDate) + "&D2=" + String.Format("{0:dd/MM/yyyy}", dt_2.SelectedDate) + "',1000,700,\'DetalheCA\'),\'Todos\'))");


			DataTable table = new DataTable("Default");
			table.Columns.Add("IdEPICAEntrega", Type.GetType("System.String"));
			//table.Columns.Add("DataRecebimento", Type.GetType("System.DateTime"));
            table.Columns.Add("DataRecebimento", Type.GetType("System.String"));
			table.Columns.Add("Acrobat", Type.GetType("System.String"));
			table.Columns.Add("Deletar", Type.GetType("System.String"));
			dsData.Tables.Add(table);


            System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");



			foreach (DataRow rowds in ds.Tables[0].Select())
			{
				row = dsData.Tables[0].NewRow();
                string zData = "";

				row["IdEPICAEntrega"] = rowds["IdEPICAEntrega"];

                DateTime dt1 = System.Convert.ToDateTime( rowds["DataRecebimento"] );
               
                row["DataRecebimento"] = dt1.ToString("dd/MM/yyyy", ptBr);

                zData = rowds["DataRecebimento"].ToString(); 


                //row["Acrobat"] = @"<a href=""#"" onClick=""" + strOpenReport("ControleEPI", "RelatorioEntregaEPI.aspx?IliteraSystem=" + strAux.ToString() + strAux.ToString()
                //    + "&IdUsuario=" + lbl_Id_Usuario.Text + "&IdEmpresa=" + lbl_Id_Empresa.Text + "&IdEmpregado=" + ddlEmpregado.SelectedValue + "&IdEPICAEntrega=" + rowds["IdEPICAEntrega"].ToString() + "&D1=" + String.Format("{0:dd/MM/yyyy}", zData),
                //    "RelatorioEntregaEPI") + @"""><img src='img/print.gif' border=0 alt='Imprimir Relatorio de Entrega dos EPIs'></a>";


                //imbRelaRec.Attributes.Add("onclick", "void(addItem(centerWin('RelatorioEntregaEPI.aspx?IliteraSystem=" + strAux.ToString() + strAux.ToString() + "&IdUsuario=" + lbl_Id_Usuario.Text + "&IdEmpresa=" + lbl_Id_Empresa.Text + "&IdEmpregado=" + ddlEmpregado.SelectedValue + "&D1=" + String.Format("{0:dd/MM/yyyy}", dt_1.SelectedDate) + "&D2=" + String.Format("{0:dd/MM/yyyy}", dt_2.SelectedDate) + "',1000,700,\'DetalheCA\'),\'Todos\'))");

				//row["Deletar"] = @"<a href=""#"" onClick=""javascript:DeletaReuniao('" + row["IdEPICAEntrega"].ToString() + @"');"" title=""Excluir Entrega"">X</a>";
                
				dsData.Tables[0].Rows.Add(row);
			}
			
			return dsData;
		}

		protected void ddlEPI_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			DataSet ds = dsData();

            
            UltraWebGridDataEntrega.DataSource = ds;
            UltraWebGridDataEntrega.DataBind();

            if (ddlEmpregado.SelectedItem.Value != "0")
                if (UltraWebGridDataEntrega.RecordCount > 0)
                    lblTotRegistrosDatas.Text = "Total de Registros: " + UltraWebGridDataEntrega.RecordCount.ToString();
                else
                    lblTotRegistrosDatas.Text = "Nenhum Registro Encontrado";
            else
                lblTotRegistrosDatas.Text = string.Empty;

            ViewState["IdEPICAEntrega"] = null;
            lblValorData.Text = string.Empty;
            lblTotRegistrosItems.Text = string.Empty;

            
            UltraWebGridItemsEntrega.DataSource = dsItems(0);
            UltraWebGridItemsEntrega.DataBind();
		}

		private DataSet dsItems(int IdEPICAEntrega)
		{
			ArrayList alEPIEntregue = new EPICAEntregaDetalhe().Find("IdEPICAEntrega=" + IdEPICAEntrega
				+" ORDER BY (SELECT Nome FROM Epi WHERE IdEPI IN (SELECT IdEPI FROM EPIClienteCA WHERE EPIClienteCA.IdEPIClienteCA = EPICAEntregaDetalhe.IdEPIClienteCA))");

			DataSet ds = new DataSet();
			DataTable table = new DataTable("Result");
			table.Columns.Add("NOME_EPI", Type.GetType("System.String"));
			table.Columns.Add("CA", Type.GetType("System.String"));
			table.Columns.Add("QTD_ENTREGUE", Type.GetType("System.String"));
			table.Columns.Add("PERIODICIDADE", Type.GetType("System.String"));
			ds.Tables.Add(table);
			DataRow newRow;
			foreach (EPICAEntregaDetalhe epientregue in alEPIEntregue)
			{
				epientregue.IdEPIClienteCA.Find();
				epientregue.IdEPIClienteCA.IdEPI.Find();
				epientregue.IdEPIClienteCA.IdCA.Find();
				newRow = ds.Tables[0].NewRow();
				newRow["NOME_EPI"] = epientregue.IdEPIClienteCA.IdEPI.ToString();
				newRow["CA"] = epientregue.IdEPIClienteCA.IdCA.NumeroCA.ToString("00000");
				newRow["QTD_ENTREGUE"] = epientregue.QtdEntregue.ToString();
				newRow["PERIODICIDADE"] = epientregue.IdEPIClienteCA.GetPeriodicidade();
				ds.Tables[0].Rows.Add(newRow);
			}

			return ds;
		}

        


        protected void cmd_Excluir_Click(object sender, EventArgs e)
        {
           

            if (lbl_ID.Text.Trim() != "0")
            {


                try
                {
                    EPICAEntrega epiEntrega = new EPICAEntrega(Convert.ToInt32(lbl_ID.Text));
                    epiEntrega.UsuarioId = usuario.Id;
                    epiEntrega.Delete();

                    ddlEPI_SelectedIndexChanged(new object(), new System.EventArgs());

                    lbl_ID.Text = "0";

                    MsgBox1.Show("Ilitera.Net", "A Entrega de EPI's foi deletada com sucesso!", null,
                            new EO.Web.MsgBoxButton("OK"));                


                }
                catch (Exception ex)
                {                
                    MsgBox1.Show("Histórico de Entrega de EPI", ex.Message, null,
                                    new EO.Web.MsgBoxButton("OK"));                

                }


            }
            else
            {
                MsgBox1.Show("Histórico de Entrega de EPI", "Selecione o registro de fornecimento a ser excluído!", null,
                                new EO.Web.MsgBoxButton("OK"));                
            }


        }        


        protected void btnRelaEmp_Click(object sender, System.EventArgs e)
		{
			Guid strAux = Guid.NewGuid();

			//OpenReport("ControleEPI", "RelatorioComplEntEPI.aspx?IliteraSystem=" + strAux.ToString() + strAux.ToString()
			//	+ "&IdUsuario=" + lbl_Id_Usuario.Text + "&IdEmpresa=" + lbl_Id_Empresa.Text + "&IdEmpregado=" + ddlEmpregado.SelectedValue, "RelatorioCompletoEntregaEPI");
		}

		private void imbRelaTodos_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Guid strAux = Guid.NewGuid();

			//OpenReport("ControleEPI", "RelatorioComplTodosEntEPI.aspx?IliteraSystem=" + strAux.ToString() + strAux.ToString()
			//	+ "&IdUsuario=" + lbl_Id_Usuario.Text + "&IdEmpresa=" + lbl_Id_Empresa.Text, "RelatorioCompletoTodosEntregaEPI");
		}

        protected void dt_1_SelectionChanged(object sender, EventArgs e)
        {
            Guid strAux = Guid.NewGuid();
            imbRelaComp.Attributes.Add("onclick", "void(addItem(centerWin('RelatorioComplEntEPI.aspx?IliteraSystem=" + strAux.ToString() + strAux.ToString() + "&IdUsuario=" + lbl_Id_Usuario.Text + "&IdEmpresa=" + lbl_Id_Empresa.Text + "&IdEmpregado=" + ddlEmpregado.SelectedValue + "&D1=" + String.Format("{0:dd/MM/yyyy}", dt_1.SelectedDate) + "&D2=" + String.Format("{0:dd/MM/yyyy}", dt_2.SelectedDate) + "',1000,700,\'DetalheCA\'),\'Todos\'))");

            imbRelaRec.Attributes.Add("onclick", "void(addItem(centerWin('RelatorioEntregaEPI.aspx?IliteraSystem=" + strAux.ToString() + strAux.ToString() + "&IdUsuario=" + lbl_Id_Usuario.Text + "&IdEPICAEntrega=0&IdEmpresa=" + lbl_Id_Empresa.Text + "&IdEmpregado=" + ddlEmpregado.SelectedValue + "&D1=" + String.Format("{0:dd/MM/yyyy}", dt_1.SelectedDate) + "&D2=" + String.Format("{0:dd/MM/yyyy}", dt_2.SelectedDate) + "',1000,700,\'DetalheCA\'),\'Todos\'))");
        }

        protected void dt_2_SelectionChanged(object sender, EventArgs e)
        {
            Guid strAux = Guid.NewGuid();
            imbRelaComp.Attributes.Add("onclick", "void(addItem(centerWin('RelatorioComplEntEPI.aspx?IliteraSystem=" + strAux.ToString() + strAux.ToString() + "&IdUsuario=" + lbl_Id_Usuario.Text + "&IdEmpresa=" + lbl_Id_Empresa.Text + "&IdEmpregado=" + ddlEmpregado.SelectedValue + "&D1=" + String.Format("{0:dd/MM/yyyy}", dt_1.SelectedDate) + "&D2=" + String.Format("{0:dd/MM/yyyy}", dt_2.SelectedDate) + "',1000,700,\'DetalheCA\'),\'Todos\'))");

            imbRelaRec.Attributes.Add("onclick", "void(addItem(centerWin('RelatorioEntregaEPI.aspx?IliteraSystem=" + strAux.ToString() + strAux.ToString() + "&IdUsuario=" + lbl_Id_Usuario.Text + "&IdEPICAEntrega=0&IdEmpresa=" + lbl_Id_Empresa.Text + "&IdEmpregado=" + ddlEmpregado.SelectedValue + "&D1=" + String.Format("{0:dd/MM/yyyy}", dt_1.SelectedDate) + "&D2=" + String.Format("{0:dd/MM/yyyy}", dt_2.SelectedDate) + "',1000,700,\'DetalheCA\'),\'Todos\'))");
        }

        protected void chk_Todos_CheckedChanged(object sender, EventArgs e)
        {
            Guid strAux = Guid.NewGuid();

            if (chk_Todos.Checked == true)
            {
                dt_1.Enabled = false;
                dt_2.Enabled = false;
                imbRelaComp.Attributes.Add("onclick", "void(addItem(centerWin('RelatorioComplEntEPI.aspx?IliteraSystem=" + strAux.ToString() + strAux.ToString() + "&IdUsuario=" + lbl_Id_Usuario.Text + "&IdEmpresa=" + lbl_Id_Empresa.Text + "&IdEmpregado=" + ddlEmpregado.SelectedValue + "&D1=" + String.Format("{0:dd/MM/yyyy}", DateTime.Now.AddYears(-20)) + "&D2=" + String.Format("{0:dd/MM/yyyy}", DateTime.Now.AddYears(+20)) + "',1000,700,\'DetalheCA\'),\'Todos\'))");

                imbRelaRec.Attributes.Add("onclick", "void(addItem(centerWin('RelatorioEntregaEPI.aspx?IliteraSystem=" + strAux.ToString() + strAux.ToString() + "&IdUsuario=" + lbl_Id_Usuario.Text + "&IdEPICAEntrega=0&IdEmpresa=" + lbl_Id_Empresa.Text + "&IdEmpregado=" + ddlEmpregado.SelectedValue + "&D1=" + String.Format("{0:dd/MM/yyyy}", DateTime.Now.AddYears(-20)) + "&D2=" + String.Format("{0:dd/MM/yyyy}", DateTime.Now.AddYears(+20)) + "',1000,700,\'DetalheCA\'),\'Todos\'))");                               					
            }
            else
            {
                dt_1.Enabled = true;
                dt_2.Enabled = true;
                imbRelaComp.Attributes.Add("onclick", "void(addItem(centerWin('RelatorioComplEntEPI.aspx?IliteraSystem=" + strAux.ToString() + strAux.ToString() + "&IdUsuario=" + lbl_Id_Usuario.Text + "&IdEmpresa=" + lbl_Id_Empresa.Text + "&IdEmpregado=" + ddlEmpregado.SelectedValue + "&D1=" + String.Format("{0:dd/MM/yyyy}", dt_1.SelectedDate) + "&D2=" + String.Format("{0:dd/MM/yyyy}", dt_2.SelectedDate) + "',1000,700,\'DetalheCA\'),\'Todos\'))");

                imbRelaRec.Attributes.Add("onclick", "void(addItem(centerWin('RelatorioEntregaEPI.aspx?IliteraSystem=" + strAux.ToString() + strAux.ToString() + "&IdUsuario=" + lbl_Id_Usuario.Text + "&IdEPICAEntrega=0&IdEmpresa=" + lbl_Id_Empresa.Text + "&IdEmpregado=" + ddlEmpregado.SelectedValue + "&D1=" + String.Format("{0:dd/MM/yyyy}", dt_1.SelectedDate) + "&D2=" + String.Format("{0:dd/MM/yyyy}", dt_2.SelectedDate) + "',1000,700,\'DetalheCA\'),\'Todos\'))");
            }
        }



        protected void UltraWebGridDataEntrega_ItemCommand(object sender, EO.Web.GridCommandEventArgs e)
        {
            

            if (e.CommandName.ToString().Trim() == "3")
            {
                string zData = "";
                Guid strAux = Guid.NewGuid();


                zData = e.Item.Cells[1].Value.ToString();

                OpenReport("", "RelatorioEntregaEPI.aspx?IliteraSystem=" + strAux.ToString() + strAux.ToString()
                        + "&IdUsuario=" + lbl_Id_Usuario.Text + "&IdEmpresa=" + lbl_Id_Empresa.Text + "&IdEmpregado=" + ddlEmpregado.SelectedValue + "&IdEPICAEntrega=" + e.Item.Key.ToString() + "&D1=" + String.Format("{0:dd/MM/yyyy}", zData),
                        "RelatorioEntregaEPI");

            }
            else if (e.CommandName.ToString().Trim() == "2")
            {

                ViewState["IdEPICAEntrega"] = e.Item.Key.ToString();
                DataSet ds = dsItems(Convert.ToInt32(ViewState["IdEPICAEntrega"]));

                UltraWebGridItemsEntrega.DataSource = ds;
                UltraWebGridItemsEntrega.DataBind();

                EPICAEntrega epiEntrega = new EPICAEntrega(Convert.ToInt32(ViewState["IdEPICAEntrega"]));
                lblValorData.Text = epiEntrega.DataRecebimento.ToString("dd-MM-yyyy");

            }
            else if (e.CommandName.ToString().Trim() == "4")
            {

                ViewState["IdEPICAEntrega"] = e.Item.Key.ToString();

                    MsgBox1.Show("Histórico de Entrega de EPI",
                                  "Confirma deleção deste histórico de Entrega de EPI ?", null,
                                  new EO.Web.MsgBoxButton("Yes", null, "Delete"),
                                  new EO.Web.MsgBoxButton("Cancel"));


            }

       
            
      
    }

    
    protected void MsgBox1_ButtonClick( object sender, System.Web.UI.WebControls.CommandEventArgs e)
    {
        //Use the command name to determine which
        //button was clicked
        if (e.CommandName == "Delete")
        {
                try
                {


                    EPICAEntrega epiEntrega = new EPICAEntrega(Convert.ToInt32(ViewState["IdEPICAEntrega"]));
                    epiEntrega.UsuarioId = usuario.Id;
                    epiEntrega.Delete();

                    ddlEPI_SelectedIndexChanged(new object(), new System.EventArgs());

                    lbl_ID.Text = "0";

                    MsgBox1.Show("Ilitera.Net", "A Entrega de EPI's foi deletada com sucesso!", null,
                            new EO.Web.MsgBoxButton("OK"));                


                }
                catch (Exception ex)
                {
                    MsgBox1.Show("Histórico de Entrega de EPI", ex.Message, null,
                                    new EO.Web.MsgBoxButton("OK"));

                }
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



        protected void cmd_Relat_Todos_CSV_Click(object sender, EventArgs e)
        {

                       

            string xFile = "Relat_EPI_" + System.DateTime.Now.Second.ToString().Trim() + System.DateTime.Now.Millisecond.ToString().Trim() + System.DateTime.Now.Minute.ToString().Trim() + System.DateTime.Now.Day.ToString().Trim() + System.DateTime.Now.Month.ToString().Trim() + ".csv";
            string myStringWebResource = "I:\\temp\\" + xFile;
            string zLinha = "";

            Ilitera.Opsa.Data.Cliente xCliente = new Ilitera.Opsa.Data.Cliente();
            xCliente.Find(System.Convert.ToInt32(Session["Empresa"]));


            Ilitera.Opsa.Report.DataSourceEPI xDados = new Ilitera.Opsa.Report.DataSourceEPI();
            DataSet zDs = xDados.GetEntregaEPIEmprTodos(xCliente);


            TextWriter tw = new StreamWriter(myStringWebResource, false, Encoding.GetEncoding(1252));

            zLinha = "Empresa;Colaborador;Data Fornecimento;EPI;CA;Qtde;Periodicidade";
            tw.WriteLine(zLinha);

            for (int zCont = 0; zCont < zDs.Tables[0].Rows.Count; zCont++)
            {
                zLinha = "";

                for (int zAux = 0; zAux < zDs.Tables[0].Columns.Count; zAux++)
                {
                    zLinha = zLinha + zDs.Tables[0].Rows[zCont][zAux].ToString().Trim().Replace(";", " / ") + ";";
                }

                tw.WriteLine(zLinha);
            }

            tw.Close();



            // myWebClient.DownloadFile(myStringWebResource, "I:\\temp\\teste.csv");	
            

            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            String Header = "Attachment; Filename=" + xFile;
            HttpContext.Current.Response.AppendHeader("Content-Disposition", Header);
            System.IO.FileInfo Dfile = new System.IO.FileInfo(myStringWebResource); //HttpContext.Current.Server.MapPath(myStringWebResource));
            HttpContext.Current.Response.WriteFile(Dfile.FullName);
            HttpContext.Current.Response.End();

            

            MsgBox1.Show("Ilitera.Net", "Arquivo gerado.", null,
            new EO.Web.MsgBoxButton("OK"));
            
        }
    }
}
