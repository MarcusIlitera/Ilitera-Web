using System;
using System.Data;
using System.Web.SessionState;
using System.Web.UI.WebControls;

using Ilitera.Opsa.Data;
using System.Text;
using System.Web.UI;
using System.Collections;
using System.Web;
using Entities;

namespace Ilitera.Net.ControleExtintores
{
    public partial class ListaExtintores : System.Web.UI.Page
    {

        protected System.Web.UI.WebControls.Label lblEmpresa;

        private Int32 xIdEmpresa;
        private Int32 xIdUsuario;
		
		protected void Page_Load(object sender, System.EventArgs e)
		{
            //InicializaWebPageObjects();

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

                //this.UltraWebGridListaExtintores.ClickCellButton += new Infragistics.WebUI.UltraWebGrid.ClickCellButtonEventHandler(this.UltraWebGridListaExtintores_ClickCellButton);

                //GetMenu(((int)IndMenuType.Empresa).ToString(), xIdUsuario.ToString().ToString(), xIdEmpresa.ToString().ToString());
				//PreencheLabels("lblEmp", cliente.NomeAbreviado);

                
                //hlkNovo.NavigateUrl = "javascript:void(addItem(centerWin('CadastroExtintores.aspx?IdEmpresa=" + xIdEmpresa.ToString() + "&IdUsuario=" + xIdUsuario.ToString() + "',400,310,\'CadastroExtintores\'),\'Todos\'))";

                
                
                //lisTodas.NavigateUrl = "ListaExtintores.aspx?IdUsuario=" + user.IdUsuario.ToString() + "&IdEmpresa=" + Session["Empresa"];
				PopulaDDLTipoExtintor();
				PopulaDDLFabricante();
				PopulaDDLSetor();
				PopulaGrid();

                AvisoExtintor zAviso = new AvisoExtintor();
                zAviso.Find("IdPessoa = " + Session["Empresa"].ToString());

                if (zAviso.Id != 0)
                {
                    txtEmail.Text = zAviso.eMail.Trim();
                    txt_Dias.Text =  zAviso.Dias.ToString().Trim() ;
                    chk_Alerta.Checked = true;                 
                }
                else
                {
                    txtEmail.Text = "";
                    txt_Dias.Text = "";
                    chk_Alerta.Checked = false;
                }

                if (txtEmail.Text.Trim() != "")
                {
                    chk_Alerta.Checked = true;
                    txtEmail.Enabled = true;
                    txt_Dias.Enabled = true;
                }
                else
                {
                    chk_Alerta.Checked = false;
                    txtEmail.Enabled = false;
                    txt_Dias.Enabled = false;
                }

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
					PopulaDDLTipoExtintor();
					txtAuxiliar.Value = string.Empty;
				}
				else if (txtAuxiliar.Value == "atualizaFabricante")
				{
					PopulaDDLFabricante();
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


		private void PopulaDDLSetor()
		{
            ddlSetor.DataSource = new Setor().Get("nID_EMPR=" + xIdEmpresa.ToString() + " ORDER BY tNO_STR_EMPR");
			ddlSetor.DataValueField = "nID_SETOR";
			ddlSetor.DataTextField = "tNO_STR_EMPR";
			ddlSetor.DataBind();

			ddlSetor.Items.Insert(0, new ListItem("Todos os Setores...", "0"));
		}

		private void PopulaDDLFabricante()
		{
			ddlFabricante.DataSource = new FabricanteExtintor().Get("IdCliente=" + xIdEmpresa.ToString()	+ " ORDER BY NomeCompleto");
			ddlFabricante.DataValueField = "IdFabricanteExtintor";
			ddlFabricante.DataTextField = "NomeAbreviado";
			ddlFabricante.DataBind();

			ddlFabricante.Items.Insert(0, new ListItem("Todos os Fabricantes...", "0"));
			ddlFabricante.Items.Add(new ListItem("Cadastro Novo Fabricante...", "Novo"));
		}
		
		private void PopulaDDLTipoExtintor()
		{
			ddlTipoExtintor.DataSource = new TipoExtintor().Get("IdCliente=" + xIdEmpresa.ToString() + " ORDER BY ModeloExtintor");
			ddlTipoExtintor.DataValueField = "IdTipoExtintor";
			ddlTipoExtintor.DataTextField = "ModeloExtintor";
			ddlTipoExtintor.DataBind();

			ddlTipoExtintor.Items.Insert(0, new ListItem("Todos os Tipos...", "0"));
			ddlTipoExtintor.Items.Add(new ListItem("Cadastro de um Novo Tipo...", "Novo"));
		}

		private void PopulaGrid()
		{
			UltraWebGridListaExtintores.DataSource = GeraDataSet();
			UltraWebGridListaExtintores.DataBind();
		}

		protected void btnLocalizar_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			lblError.Text = string.Empty;
			if (Page.IsValid)
			{
				//UltraWebGridListaExtintores.DisplayLayout.Pager.CurrentPageIndex = 1;
				PopulaGrid();
			}
		}



        private DataSet GeraDataSet()
        {
            ArrayList alExtintores;

            DataSet ds = new DataSet();
            DataRow row;
            DataTable table = new DataTable("Default");
            table.Columns.Add("Id", Type.GetType("System.String"));
            table.Columns.Add("AtivoFixo", Type.GetType("System.String"));
            table.Columns.Add("DataFabricacao", Type.GetType("System.String"));
            table.Columns.Add("TipoExtintor", Type.GetType("System.String"));
            table.Columns.Add("Setor", Type.GetType("System.String"));
            table.Columns.Add("Historico", Type.GetType("System.String"));
            table.Columns.Add("VencimentoRecarga", Type.GetType("System.String"));
            table.Columns.Add("PdfExtintor", Type.GetType("System.String"));
            ds.Tables.Add(table);

            StringBuilder st = new StringBuilder();

            if (txtChave.Text.Trim() != string.Empty)
            {
                st.Append(" AND (AtivoFixo LIKE '%" + txtChave.Text + "%'");
                st.Append(" OR Localizacao LIKE '%" + txtChave.Text + "%'");
                st.Append(" OR Observacao LIKE '%" + txtChave.Text + "%')");
            }
            if (rblCondicao.SelectedValue != "0")
                st.Append(" AND IndCondicao=" + rblCondicao.SelectedValue);
            if (ddlTipoExtintor.SelectedValue != "0" && ddlTipoExtintor.SelectedValue != "Novo")
                st.Append(" AND IdTipoExtintor=" + ddlTipoExtintor.SelectedValue);
            if (wneGarantia.Text != "")
                st.Append(" AND Garantia=" + wneGarantia.Text);
            if (ddlFabricante.SelectedValue != "0" && ddlFabricante.SelectedValue != "Novo")
                st.Append(" AND IdFabricanteExtintor=" + ddlFabricante.SelectedValue);
            if (ddlSetor.SelectedValue != "0")
                st.Append(" AND IdSetor=" + ddlSetor.SelectedValue);

            alExtintores = new Extintores().Find("IdCliente=" + xIdEmpresa.ToString()
                + st.ToString()
                + " ORDER BY VencimentoRecarga, AtivoFixo");

            foreach (Extintores extintor in alExtintores)
            {
                extintor.IdTipoExtintor.Find();
                extintor.IdSetor.Find();
                Guid strAux = Guid.NewGuid();

                row = ds.Tables[0].NewRow();
                row["Id"] = extintor.Id.ToString();
                row["AtivoFixo"] = extintor.AtivoFixo;
                row["DataFabricacao"] = extintor.DataFabricacao.ToString("dd-MM-yyyy");
                row["VencimentoRecarga"] = extintor.VencimentoRecarga.ToString("dd-MM-yyyy");
                row["TipoExtintor"] = extintor.IdTipoExtintor.ModeloExtintor;
                row["Setor"] = extintor.IdSetor.tNO_STR_EMPR;
                row["Historico"] = @"<a href=""#"" onClick=""addItem(centerWin('HistoricoExtintores.aspx?IdEmpresa=" + xIdEmpresa.ToString() + @"&IdUsuario=" + xIdUsuario.ToString() + @"&IdExtintores=" + extintor.Id + @"', 450, 355, 'CadastroHistoricoExtintor'), 'Todos');""><img src='img/history.gif' border=0 alt='Histórico do Extintor'></a>";
                row["PdfExtintor"] = @"<a href=""#"" onClick=""" + strOpenReport("ControleExtintores", "ExtintorDetalhe.aspx?IliteraSystem=" + strAux.ToString() + strAux.ToString()
                    + "&IdEmpresa=" + xIdEmpresa.ToString() + "&IdUsuario=" + xIdUsuario.ToString() + "&IdExtintores=" + extintor.Id, "ExtintorDetalhe") + @"""><img src='img/print.gif' border=0 alt='Imprimir Ficha Cadastral do Extintor'></a>";

                ds.Tables[0].Rows.Add(row);
            }

            //if (ds.Tables[0].Rows.Count > 0)
            //    lblTotRegistros.Text = "Total de Registros: <b>" + ds.Tables[0].Rows.Count.ToString() + "</b>";
            //else
            //	lblTotRegistros.Text = "Nenhum Registro Encontrado";

            return ds;
        }

        protected void btnNovoExtintor_Click(object sender, System.EventArgs e)
        {
            StringBuilder st = new StringBuilder();

            st.Append("addItem(centerWin('CadastroExtintores.aspx?IdEmpresa=" + xIdEmpresa.ToString() + "&IdUsuario=" + xIdUsuario.ToString() + "',400,310,\'CadastroExtintores\'),\'Todos\');");

            this.ClientScript.RegisterStartupScript(this.GetType(), "CadastroExtintores", st.ToString(), true);
        }

		protected void cvdCaracteres_ServerValidate(object source, System.Web.UI.WebControls.ServerValidateEventArgs args)
		{
			//VerificaCaracter(args, txtChave, cvdCaracteres);
		}

        //protected void UltraWebGridListaEmpresa_PageIndexChanged(object sender, Infragistics.WebUI.UltraWebGrid.PageEventArgs e)
        //{
        //    lblError.Text = string.Empty;

        //    if (VerificaCaracter(txtChave, lblError))
        //    {
        //        PopulaGrid();
			
        //        if (e.NewPageIndex > UltraWebGridListaExtintores.DisplayLayout.Pager.PageCount)
        //            UltraWebGridListaExtintores.DisplayLayout.Pager.CurrentPageIndex = UltraWebGridListaExtintores.DisplayLayout.Pager.PageCount;
        //        else
        //            UltraWebGridListaExtintores.DisplayLayout.Pager.CurrentPageIndex = e.NewPageIndex;
			
        //        UltraWebGridListaExtintores.DataBind();
        //    }
        //}

		protected void ddlTipoExtintor_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (ddlTipoExtintor.SelectedValue == "Novo")
			{
				ddlTipoExtintor.ClearSelection();
				StringBuilder st = new StringBuilder();

				st.Append("addItem(centerWin('CadTipoExtintor.aspx?IdEmpresa=" + xIdEmpresa.ToString() + "&IdUsuario=" + xIdUsuario.ToString() + "',300,300,\'CadastroTipoExtintores\'),\'Todos\');");

                this.ClientScript.RegisterStartupScript(this.GetType(), "CadastroTipoExtintores", st.ToString(), true);
			}
		}

		protected void ddlFabricante_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (ddlFabricante.SelectedValue == "Novo")
			{
				ddlFabricante.ClearSelection();
				StringBuilder st = new StringBuilder();

				st.Append("addItem(centerWin('CadFabricanteExtintor.aspx?IdEmpresa=" + xIdEmpresa.ToString() + "&IdUsuario=" + xIdUsuario.ToString() + "',300,280,\'CadastroFabricanteExtintores\'),\'Todos\');");

                this.ClientScript.RegisterStartupScript(this.GetType(), "CadastroFabricanteExtintores", st.ToString(), true);
			}
		}

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

        //    int width = 750, height = 510;

        //    try
        //    {
        //        PermissaoEdicaoExame(new Clinico(Convert.ToInt32(e.Row.Cells[0].Text)));

        //        //e.Row.Cells.FromKey("AtivoFixo").TargetURL = "javascript:void(addItem(centerWin('ExameNaoOcupacional.aspx?IdEmpregado=" + Request["IdEmpregado"] + "&IdExame=" + e.Row.Cells[0].Text
        //        //    + "&IdUsuario=" + Request["IdUsuario"] + "&IdEmpresa=" + Request["IdEmpresa"] + "'," + width + "," + height + ",\'EC\'),\'Todos\'));";

        //        e.Row.Cells.FromKey("AtivoFixo").TargetURL = "javascript:void(addItem(centerWin('CadastroExtintores.aspx?IdEmpresa=" + xIdEmpresa.ToString() + "&IdUsuario=" + xIdUsuario.ToString() + "&IdExtintores=" + e.Row.Cells[0].Text + "',400,310,\'CadastroExtintores\'),\'Todos\'))";
        //    }
        //    catch (Exception ex)
        //    {
        //        e.Row.Cells.FromKey("Tipo").TargetURL = "javascript:window.alert(\"" + ex.Message + "\");";
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



        protected void cmd_Novo_Equipamento_Click(object sender, EventArgs e)
        {
                        
            StringBuilder st = new StringBuilder();

            st.AppendFormat("void(window.open('CadastroExtintores.aspx?IdEmpresa=" + xIdEmpresa.ToString() + "&IdUsuario=" + xIdUsuario.ToString() + @"', 'CadastroExtintores','scrollbars=yes, resizable=yes, toolbar=no, addressbar=no, menubar=no, width=450px, height=350px'));");

            ScriptManager.RegisterStartupScript(this, this.GetType(), " ", st.ToString(), true);
            
        }



        protected void cmd_Listar_Todas_Click(object sender, EventArgs e)
        {

            StringBuilder st = new StringBuilder();

            st.AppendFormat("void(window.open('ListagemExtintores.aspx?IdUsuario=" + xIdUsuario.ToString() + "&IdEmpresa=" + xIdEmpresa.ToString() + @"', 'CadastroExtintores','scrollbars=yes, resizable=yes, toolbar=no, addressbar=no, menubar=no, width=600px, height=500px'));");
            

            ScriptManager.RegisterStartupScript(this, this.GetType(), " ", st.ToString(), true);
            
        }




        protected void UltraWebGridListaExtintores_ItemCommand(object sender, EO.Web.GridCommandEventArgs e)
        {
            StringBuilder st = new StringBuilder();


            st.AppendFormat("void(window.open('CadastroExtintores.aspx?IdEmpresa=" + Session["Empresa"] + @"&IdUsuario=" + Session["Usuario"] + @"&IdExtintores=" + e.Item.Key.ToString() + @"', 'CursoEmpresa','scrollbars=yes, resizable=yes, toolbar=no, addressbar=no, menubar=no, width=600px, height=500px'));");

            ScriptManager.RegisterStartupScript(this, this.GetType(), " ", st.ToString(), true);

            //Session["Empregado"] = e.Item.Key.ToString();
            //Session["NomeEmpregado"] = e.Item.Cells[0].Value.ToString();

        }

        protected void chk_Alerta_CheckedChanged(object sender, EventArgs e)
        {

            if ( chk_Alerta.Checked == true )
            {
                txtEmail.Enabled = true;
                txt_Dias.Enabled = true;
            }
            else
            {
                txtEmail.Enabled = false;
                txt_Dias.Enabled = false;
            }

        }

        protected void btnGravarServico_Click(object sender, EventArgs e)
        {

            AvisoExtintor zAviso = new AvisoExtintor();
            zAviso.Find("IdPessoa = " + Session["Empresa"].ToString());

            bool isNumerical;
            int myInt;

            if ( chk_Alerta.Checked == false )
            {
                txtEmail.Text = "";
                txt_Dias.Text = "";
            }


            if (txt_Dias.Text.Trim() == "") txt_Dias.Text = "0";

            isNumerical = int.TryParse(txt_Dias.Text.Trim(), out myInt);
            if (isNumerical == false)
            {
                MsgBox1.Show("Ilitera.Net", "Número de dias inválido", null,
                new EO.Web.MsgBoxButton("OK"));
                return;
            }


            


            if (zAviso.Id != 0)
            {
                zAviso.eMail = txtEmail.Text;
                zAviso.Dias = System.Convert.ToInt16( txt_Dias.Text );
                zAviso.Save();
            }
            else
            {
                AvisoExtintor zAviso2 = new AvisoExtintor();
                zAviso.IdPessoa = System.Convert.ToInt32(Session["Empresa"].ToString());
                zAviso.eMail = txtEmail.Text;
                zAviso.Dias = System.Convert.ToInt16(txt_Dias.Text);
                zAviso.Save();
            }

            MsgBox1.Show("Ilitera.Net", "Dados de alerta de Extintores salvo", null,
                new EO.Web.MsgBoxButton("OK"));

            return;

        }
    }
}
