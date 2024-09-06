using System;
using System.Data;
using System.Web.SessionState;
using System.Web.UI.WebControls;

using Ilitera.Opsa.Data;
using System.Text;

using System.Collections;
using System.Web;
using System.Web.UI;

using Entities;

namespace Ilitera.Net.PCMSO
{
    public partial class ListaUniformes : System.Web.UI.Page
    {

        protected System.Web.UI.WebControls.Label lblEmpresa;

        private Int32 xIdEmpresa;
        private Int32 xIdUsuario;
		
		protected void Page_Load(object sender, System.EventArgs e)
		{
           // InicializaWebPageObjects();

            Usuario user = (Usuario)Session["usuarioLogado"];

            xIdEmpresa = System.Convert.ToInt32(Session["Empresa"]);
            xIdUsuario = user.IdUsuario;
					
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


                //hlkNovo.NavigateUrl = "javascript:void(addItem(centerWin('CadastroUniformes.aspx?IdEmpresa=" + xIdEmpresa.ToString() + "&IdUsuario=" + xIdUsuario.ToString() + "',420,510,\'CadastroUniformes\'),\'Todos\'))";
                hlkNovo.NavigateUrl = "javascript:void(window.open('CadastroUniformes.aspx?IdEmpresa=" + Session["Empresa"] + @"&IdUsuario=" + Session["Usuario"] + "@', 'Uniformes','scrollbars=yes, resizable=yes, toolbar=no, addressbar=no, menubar=no, width=600px, height=500px'));";

                                
				
				PopulaGrid();
                Carregar_Emails();

            }
			else
			{

                PopulaGrid();

				if (txtAuxiliar.Value == "atualiza")
				{
					//PopulaGrid();
					txtAuxiliar.Value = string.Empty;
				}
				else if (txtAuxiliar.Value == "atualizaTipo")
				{
					
					txtAuxiliar.Value = string.Empty;
				}
				else if (txtAuxiliar.Value == "atualizaFabricante")
				{
					
					txtAuxiliar.Value = string.Empty;
				}
			}
		}



        //protected void UltraWebGridListaExtintores_ClickCellButton(object sender, Infragistics.WebUI.UltraWebGrid.CellEventArgs e)
        //{
        //    string xCod = "";

        //    xCod = e.Cell.Row.Cells[0].Text;

        //    //lbl_ID.Text = e.Cell.Row.Cells[0].Text;
        //    //ViewState["IdEPICAEntrega"] = e.Cell.Row.Cells[0].Text;
        //    //DataSet ds = dsItems(Convert.ToInt32(ViewState["IdEPICAEntrega"]));

        //    //UltraWebGridItemsEntrega.DisplayLayout.Pager.CurrentPageIndex = 1;
        //    //UltraWebGridItemsEntrega.DataSource = ds;
        //    //UltraWebGridItemsEntrega.DataBind();

        //    //EPICAEntrega epiEntrega = new EPICAEntrega(Convert.ToInt32(ViewState["IdEPICAEntrega"]));
        //    //lblValorData.Text = epiEntrega.DataRecebimento.ToString("dd-MM-yyyy");

        //    //if (UltraWebGridItemsEntrega.Rows.Count > 0)
        //    //    lblTotRegistrosItems.Text = "Total de Registros: " + ds.Tables[0].Rows.Count.ToString();
        //    //else
        //    //    lblTotRegistrosItems.Text = "Nenhum Registro Encontrado";
        //}


		

		private void PopulaGrid()
		{
            UltraWebGridListaUniformes.DataSource = GeraDataSet();
            UltraWebGridListaUniformes.DataBind();
		}

		protected void btnLocalizar_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			lblError.Text = string.Empty;
			if (Page.IsValid)
			{
				
				PopulaGrid();
			}
		}



        private DataSet GeraDataSet()
        {

            DataSet ds;

            Ilitera.Data.PPRA_EPI xEPI = new Ilitera.Data.PPRA_EPI();

            ds = xEPI.Trazer_Uniformes(System.Convert.ToInt32(Session["Empresa"].ToString()));

            return ds;
        }


   

        //protected void UltraWebGridListaEmpresa_PageIndexChanged(object sender, Infragistics.WebUI.UltraWebGrid.PageEventArgs e)
        //{
        //    lblError.Text = string.Empty;

        //    UltraWebGridListaExtintores.DisplayLayout.Pager.CurrentPageIndex = e.NewPageIndex;
			
        //    //UltraWebGridListaExtintores.DataBind();
		
        //}


		protected void lkbListagemExtintores_Click(object sender, System.EventArgs e)
		{
			//if (lblTotRegistros.Text != "Nenhum Registro Encontrado")
			//{
				Guid strAux = Guid.NewGuid();

                OpenReport("ControleExtintores", "ListagemExtintores.aspx?IliteraSystem=" + strAux.ToString() + strAux.ToString()
					+ "&IdEmpresa=" + xIdEmpresa.ToString() + "&IdUsuario=" + xIdUsuario.ToString(), "ListagemExtintores");
			//}
			//else
			//	Aviso("Não há nenhum Extintor de Incêndio cadastrado para imprimir a Listagem dos Extintores!");
		}



        //protected void gridEmpregados_CellSelectionChanged(object sender, SelectedCellEventArgs e)
        //{
        //}

        //protected void UltraWebGridListaEmpresa_InitializeRow(object sender, Infragistics.WebUI.UltraWebGrid.RowEventArgs e)
        //{

        //    int width = 780, height = 820;

        //    try
        //    {
        //        PermissaoEdicaoExame(new Clinico(Convert.ToInt32(e.Row.Cells[0].Text)));


        //        e.Row.Cells.FromKey("Uniforme").TargetURL = "javascript:void(addItem(centerWin('CadastroUniformes.aspx?IdEmpresa=" + xIdEmpresa.ToString() + "&IdUsuario=" + xIdUsuario.ToString() + "&IdUniforme=" + e.Row.Cells[0].Text + "',420,510,\'CadastroUniformes\'),\'Todos\'))";
        //    }
        //    catch (Exception ex)
        //    {
        //        e.Row.Cells.FromKey("Uniforme").TargetURL = "javascript:window.alert(\"" + ex.Message + "\");";
        //    }
        //}


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




        protected void UltraWebGridListaUniformes_ItemCommand(object sender, EO.Web.GridCommandEventArgs e)
        {
            StringBuilder st = new StringBuilder();

           
            if (e.CommandName == "3")   //add
            {
                st.AppendFormat("void(window.open('CadastroUniformes.aspx?IdEmpresa=" + Session["Empresa"] + @"&IdUsuario=" + Session["Usuario"] + @"&IdUniforme=" + e.Item.Key.ToString() + @"', 'CursoEmpresa','scrollbars=yes, resizable=yes, toolbar=no, addressbar=no, menubar=no, width=600px, height=500px'));");

                ScriptManager.RegisterStartupScript(this, this.GetType(), " ", st.ToString(), true);

            }
            
        }

        protected void cmd_Salvar_Dias_Click(object sender, EventArgs e)
        {
            if (txt_Dias.Text.Trim() == "") txt_Dias.Text = "0";


            if (txt_Email1.Text.Trim() != "")
            {
                if (txt_Email1.Text.IndexOf("@") < 2)
                {
                    MsgBox2.Show("Ilitera.Net", "Formato inválido do e-mail (1) ", null,
                                  new EO.Web.MsgBoxButton("OK"));
                    return;

                }

                if (txt_Email1.Text.IndexOf(".com") < 2)
                {
                    MsgBox2.Show("Ilitera.Net", "Formato inválido do e-mail (1) ", null,
                                  new EO.Web.MsgBoxButton("OK"));
                    return;
                }

            }

            if (txt_Email2.Text.Trim() != "")
            {
                if (txt_Email2.Text.IndexOf("@") < 2)
                {
                    MsgBox2.Show("Ilitera.Net", "Formato inválido do e-mail (1) ", null,
                                  new EO.Web.MsgBoxButton("OK"));
                    return;

                }

                if (txt_Email2.Text.IndexOf(".com") < 2)
                {
                    MsgBox2.Show("Ilitera.Net", "Formato inválido do e-mail (1) ", null,
                                  new EO.Web.MsgBoxButton("OK"));
                    return;
                }

            }


            int n;
            bool isNumeric = int.TryParse(txt_Dias.Text.Trim(), out n);

            if (!isNumeric)
            {
                MsgBox2.Show("Ilitera.Net", "Número de dias em formato inválido.", null,
                              new EO.Web.MsgBoxButton("OK"));
                return;
            }

            

            Ilitera.Data.PPRA_EPI rLista = new Ilitera.Data.PPRA_EPI();
            rLista.Inserir_Uniforme_Alerta(System.Convert.ToInt32(Session["Empresa"].ToString()), txt_Email1.Text.Trim(), txt_Email2.Text.Trim(), System.Convert.ToInt32(txt_Dias.Text.Trim()));

            MsgBox2.Show("Ilitera.Net", "Dados de e-mail de uniforme salvos", null,
                          new EO.Web.MsgBoxButton("OK"));
            return;



        }

        protected void Carregar_Emails()
        {
            DataSet rDs = new DataSet();

            Ilitera.Data.PPRA_EPI rLista = new Ilitera.Data.PPRA_EPI();
            rDs = rLista.Carregar_Uniforme_Alerta(System.Convert.ToInt32(Session["Empresa"].ToString()));


            if (rDs.Tables[0].Rows.Count > 0)
            {
                txt_Dias.Text = rDs.Tables[0].Rows[0]["Dias"].ToString().Trim();
                txt_Email1.Text = rDs.Tables[0].Rows[0]["email1"].ToString().Trim();
                txt_Email2.Text = rDs.Tables[0].Rows[0]["email2"].ToString().Trim();

            }

            return;

        }


        protected void MsgBox2_ButtonClick(object sender, CommandEventArgs e)
        {

        }
    }
}
