using System;
using System.Data;
using System.Text;
using System.Web.SessionState;
using System.Web.UI.WebControls;
using System.Web;
using System.Web.UI;
using Ilitera.Opsa.Data;
using Entities;


namespace Ilitera.Net
{
	/// <summary>
	/// 
	/// </summary>
	public partial class ListaProcedimento : System.Web.UI.Page
	{
		protected Procedimento procedimento;

        private Int32 xIdEmpresa;
        private Int32 xIdUsuario;

	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			//InicializaWebPageObjects();
                        
			if(!IsPostBack)
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

                Usuario user = (Usuario)Session["usuarioLogado"];

                xIdEmpresa = System.Convert.ToInt32(Session["Empresa"]);
                xIdUsuario = user.IdUsuario;

                lbl_Id_Empresa.Text = xIdEmpresa.ToString().Trim();
                lbl_Id_Usuario.Text = xIdUsuario.ToString().Trim();


                hlk_Imp.NavigateUrl = "javascript:void(centerWin('ImportProcGenerico.aspx?IdUsuario=" + lbl_Id_Usuario.Text + "&IdEmpresa=" + lbl_Id_Empresa.Text + "',460,280).focus());";

                //hlkNovo.NavigateUrl = "javascript:void(addItem(centerWin('CadProcedimento.aspx?IdEmpresa=" + xIdEmpresa.ToString() + "&IdUsuario=" + xIdUsuario.ToString() + "',400,310,\'OrdemDeServico2\'),\'Todos\');)";
                

				//GetMenu(((int)IndMenuType.Empresa).ToString(), xIdUsuario.ToString(), xIdEmpresa.ToString());
				//PreencheLabels("lblEmp", cliente.NomeAbreviado.ToString());
				lisTodas.NavigateUrl = "ListaProcedimento.aspx?IdUsuario=" + xIdUsuario.ToString() + "&IdEmpresa=" + xIdEmpresa.ToString();
				PopulaDdls();
				
                //DataSet dsProcedimentos = GetDataSourceProcedimentos();

                //lkbListagemPOPs.Enabled = !(dsProcedimentos.Tables[0].Rows.Count.Equals(0));
                //lkbListagemNome.Enabled = !(dsProcedimentos.Tables[0].Rows.Count.Equals(0));

                //UltraWebGridProcedimentos.DataSource = dsProcedimentos;
                //UltraWebGridProcedimentos.DataBind();
			}

            DataSet dsProcedimentos = GetDataSourceProcedimentos();

            lkbListagemPOPs.Enabled = !(dsProcedimentos.Tables[0].Rows.Count.Equals(0));
            lkbListagemNome.Enabled = !(dsProcedimentos.Tables[0].Rows.Count.Equals(0));

            //UltraWebGridProcedimentos.DataSource = dsProcedimentos;
            //UltraWebGridProcedimentos.DataBind();

            gridEmpregados.DataSource = dsProcedimentos;
            gridEmpregados.DataBind();
		}

		private void PopulaDdls()
		{
			PopulaDdlSetor();
			PopulaDdlFerramenta();
			PopulaDdlEquipamento();
			PopulaDdlProduto();
			PopulaDdlCelula();
			PopulaDdlTipoProcediemnto();
		}

		public void PopulaDdlTipoProcediemnto()
		{
			ddlTipoProcedimento.Items.Add(new ListItem("Todos os Tipos",""));
			ddlTipoProcedimento.Items.Add(new ListItem("Resumo - Pai",((int)TipoProcedimento.Resumo).ToString()));
			ddlTipoProcedimento.Items.Add(new ListItem("Específico - Filho",((int)TipoProcedimento.Especifico).ToString()));
			ddlTipoProcedimento.Items.Add(new ListItem("Instrutivo",((int)TipoProcedimento.Instrutivo).ToString()));
		}

		public void PopulaDdlSetor()
		{
            ddlSetor.DataSource = new Setor().Get("nID_EMPR=" + lbl_Id_Empresa.Text + "ORDER BY tNO_STR_EMPR");
			ddlSetor.DataValueField = "nID_SETOR";
			ddlSetor.DataTextField = "tNO_STR_EMPR";
			ddlSetor.DataBind();
			ddlSetor.Items.Insert(0,new ListItem("Todos os Setores","0"));
		}

		public void PopulaDdlFerramenta()
		{
            ddlFerramenta.DataSource = new Ferramenta().Get("IdCliente=" + lbl_Id_Empresa.Text + " ORDER BY Nome");
			ddlFerramenta.DataValueField = "IdFerramenta";
			ddlFerramenta.DataTextField = "Nome";
			ddlFerramenta.DataBind();
			ddlFerramenta.Items.Insert(0,new ListItem("Todas as Ferramentas","0"));
		}

		public void PopulaDdlEquipamento()
		{
            ddlEquipamento.DataSource = new Equipamento().Get("IdCliente=" + lbl_Id_Empresa.Text + " ORDER BY Nome");
			ddlEquipamento.DataValueField = "IdEquipamento";
			ddlEquipamento.DataTextField = "Nome";
			ddlEquipamento.DataBind();
			ddlEquipamento.Items.Insert(0,new ListItem("Todos os Equipamentos","0"));
		}

		public void PopulaDdlProduto()
		{
            ddlProduto.DataSource = new Produto().Get("IdCliente=" + lbl_Id_Empresa.Text + " ORDER BY Nome");
			ddlProduto.DataValueField = "IdProduto";
			ddlProduto.DataTextField = "Nome";
			ddlProduto.DataBind();
			ddlProduto.Items.Insert(0,new ListItem("Todos os Produtos","0"));
		}

		public void PopulaDdlCelula()
		{  
            ddlCelula.DataSource = new Celula().Get("IdCliente=" + lbl_Id_Empresa.Text + " ORDER BY Nome");
			ddlCelula.DataValueField = "IdCelula";
			ddlCelula.DataTextField = "Nome";
			ddlCelula.DataBind();
			ddlCelula.Items.Insert(0,new ListItem("Todas as Celulas","0"));
		}

		private DataSet GetDataSourceProcedimentos()
		{
			StringBuilder st = new StringBuilder();

            st.Append("IdCliente=" +  lbl_Id_Empresa.Text);
			
			if (ddlEquipamento.SelectedIndex != 0)
				st.Append(" AND IdProcedimento IN (SELECT IdProcedimento FROM ProcedimentoEquipamento WHERE IdEquipamento=" + ddlEquipamento.SelectedValue + ")");
			if (ddlProduto.SelectedIndex != 0)
				st.Append(" AND IdProcedimento IN (SELECT IdProcedimento FROM ProcedimentoProduto WHERE IdProduto=" + ddlProduto.SelectedValue + ")");
			if (ddlSetor.SelectedIndex != 0)
				st.Append(" AND IdProcedimento IN (SELECT IdProcedimento FROM ProcedimentoSetor WHERE IdSetor=" + ddlSetor.SelectedValue + ")");
			if (ddlFerramenta.SelectedIndex != 0)
				st.Append(" AND IdProcedimento IN (SELECT IdProcedimento FROM ProcedimentoFerramenta WHERE IdFerramenta=" + ddlFerramenta.SelectedValue + ")");
			if (ddlCelula.SelectedIndex != 0)
				st.Append(" AND IdProcedimento IN (SELECT IdProcedimento FROM ProcedimentoCelula WHERE IdCelula=" + ddlCelula.SelectedValue + ")");
			if (ddlTipoProcedimento.SelectedIndex != 0)
                st.Append(" AND IndTipoProcedimento=" + ddlTipoProcedimento.SelectedValue);

			if (!txtProcedimento.Text.Trim().Equals(string.Empty))
			{
				try
				{
					Convert.ToInt32(txtProcedimento.Text.Trim());

					st.Append(" AND (Nome LIKE '%" + txtProcedimento.Text.Trim() + "%' OR Numero=" + txtProcedimento.Text.Trim() + ")");
				}
				catch (Exception)
				{
					st.Append(" AND Nome LIKE '%" + txtProcedimento.Text.Trim() + "%'");
				}
			}

			st.Append(" ORDER BY Numero");
			
			DataSet ds = new Procedimento().Get(st.ToString());
			DataSet dsProcedimento = new DataSet();
			DataRow newRow;
			DataTable table = new DataTable();
			table.Columns.Add("IdProcedimento", typeof(string));
			table.Columns.Add("Numero", typeof(string));
			table.Columns.Add("Nome", typeof(string));
			dsProcedimento.Tables.Add(table);

			foreach (DataRow row in ds.Tables[0].Select())
			{
				newRow = dsProcedimento.Tables[0].NewRow();
				newRow["IdProcedimento"] = row["IdProcedimento"].ToString();
				
				if (row["Nome"].ToString().Length > 77)
					newRow["Nome"] = row["Nome"].ToString().Substring(0, 74) + "...";
				else
					newRow["Nome"] = row["Nome"].ToString();
				
				newRow["Numero"] = Convert.ToInt16(row["Numero"]).ToString("0000");

				dsProcedimento.Tables[0].Rows.Add(newRow);
			}
			
			if (dsProcedimento.Tables[0].Rows.Count > 0)
				lblTotalRegistros.Text = "Total de registros: <b>" + dsProcedimento.Tables[0].Rows.Count.ToString() + "</b>";
			else
				lblTotalRegistros.Text = "Nenhum registro encontrado";
			
			return dsProcedimento;
		}

		protected void imgBusca_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
            //UltraWebGridProcedimentos.DisplayLayout.Pager.CurrentPageIndex = 1;
            //UltraWebGridProcedimentos.DataSource = GetDataSourceProcedimentos();
            //UltraWebGridProcedimentos.DataBind();

            
            gridEmpregados.DataSource = GetDataSourceProcedimentos(); 
            gridEmpregados.DataBind();
		}

		private void AbreRelatorioProcedimento(string page)
		{
			Guid strAux = Guid.NewGuid();

			OpenReport("OrdemDeServico", page + ".aspx?IliteraSystem=" + strAux.ToString() + strAux.ToString()
                + "&IdEmpresa=" + lbl_Id_Empresa.Text + "&IdUsuario=" + lbl_Id_Usuario.Text, page);
		}

		protected void lkbListagemPOPs_Click(object sender, System.EventArgs e)
		{
			AbreRelatorioProcedimento("RptProcNumero");
		}

		protected void lkbListagemNome_Click(object sender, System.EventArgs e)
		{
			AbreRelatorioProcedimento("RptProcNome");
		}

		protected void btnIncluirNovo_Click(object sender, System.EventArgs e)
		{
			//Server.Transfer("CadProcedimento.aspx?IdEmpresa=" + xIdEmpresa.ToString() + "&IdUsuario=" + xIdUsuario.ToString());
            Response.Redirect( "CadProcedimento.aspx?IdEmpresa=" + lbl_Id_Empresa.Text + "&IdUsuario=" + lbl_Id_Usuario.Text);
		}

		protected void btnImportar_Click(object sender, System.EventArgs e)
		{
			StringBuilder st = new StringBuilder();

            st.Append("javascript:void(centerWin('ImportProcGenerico.aspx?IdUsuario=" + lbl_Id_Usuario.Text + "&IdEmpresa=" + lbl_Id_Empresa.Text + "',400,240).focus());");

            this.ClientScript.RegisterStartupScript(this.GetType(), "ImportProcGenerico", st.ToString(), true);

            //Response.Redirect("ImportProcGenerico.aspx?IdEmpresa=" + lbl_Id_Empresa.Text + "&IdUsuario=" + lbl_Id_Usuario.Text);
		}

        //protected void UltraWebGridProcedimentos_PageIndexChanged(object sender, Infragistics.WebUI.UltraWebGrid.PageEventArgs e)
        //{
        //    UltraWebGridProcedimentos.DataSource = GetDataSourceProcedimentos();
        //    UltraWebGridProcedimentos.DataBind();

        //    if (UltraWebGridProcedimentos.DisplayLayout.Pager.PageCount < e.NewPageIndex)
        //        UltraWebGridProcedimentos.DisplayLayout.Pager.CurrentPageIndex = UltraWebGridProcedimentos.DisplayLayout.Pager.PageCount;
        //    else
        //        UltraWebGridProcedimentos.DisplayLayout.Pager.CurrentPageIndex = e.NewPageIndex;
				
        //    UltraWebGridProcedimentos.DataBind();
        //}


        protected void btn_Ajustar_Numeracao_Click(object sender, System.EventArgs e)
        {


            Ilitera.Data.PPRA_EPI xNum = new Ilitera.Data.PPRA_EPI();

            xNum.Ajustar_Numeracao_Ordem_Servico(System.Convert.ToInt32(Session["Empresa"].ToString()));

            DataSet dsProcedimentos = GetDataSourceProcedimentos();

            lkbListagemPOPs.Enabled = !(dsProcedimentos.Tables[0].Rows.Count.Equals(0));
            lkbListagemNome.Enabled = !(dsProcedimentos.Tables[0].Rows.Count.Equals(0));

            //UltraWebGridProcedimentos.DataSource = dsProcedimentos;
            //UltraWebGridProcedimentos.DataBind();


            gridEmpregados.DataSource = dsProcedimentos;
            gridEmpregados.DataBind();
            

            ScriptManager.RegisterStartupScript(this, this.GetType(), "alerta", String.Format("alert('{0}')", "Ordem dos procedimentos atualizada."), true);
        
        }


        //protected void gridEmpregados_CellSelectionChanged(object sender, SelectedCellEventArgs e)
        //{
        //    try
        //    {
        //        if (e.CurrentSelectedCells.Count > 0)
        //        {
        //            //Session["Empregado"] = e.CurrentSelectedCells[0].Row.DataKey.GetValue(0).ToString();
        //            //Session["NomeEmpregado"] = e.CurrentSelectedCells[0].Row.DataKey.GetValue(1).ToString();//e.CurrentSelectedCells[0].Text;

        //            Response.Redirect("CadProcedimento.aspx?IdProcedimento=" + e.CurrentSelectedCells[0].Row.DataKey.GetValue(0).ToString() + "&IdEmpresa=" + lbl_Id_Empresa.Text + "&IdUsuario=" + lbl_Id_Usuario.Text);
        //            //window.location.href = "CadProcedimento.aspx?IdProcedimento=" + IdProcedimento + "&IdEmpresa=" + IdEmpresa + "&IdUsuario=" + IdUsuario;

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "alerta", String.Format("alert('{0}');", ex.Message), true);
        //    }
        //}


    
        //protected void gridEmpregados_RowSelectionChanged(object sender, SelectedRowEventArgs e)
        //{
        //    try
        //    {
        //        if (e.CurrentSelectedRows.Count > 0)
        //        {
        //            //Session["Empregado"] = e.CurrentSelectedRows[0].DataKey.GetValue(0).ToString();
        //            //Session["NomeEmpregado"] = e.CurrentSelectedRows[0].DataKey.GetValue(1).ToString();//e.CurrentSelectedCells[0].Text;

        //            Response.Redirect("CadProcedimento.aspx?IdProcedimento=" + e.CurrentSelectedRows[0].DataKey.GetValue(0).ToString() + "&IdEmpresa=" + lbl_Id_Empresa.Text + "&IdUsuario=" + lbl_Id_Usuario.Text);
        //            //window.location.href = "CadProcedimento.aspx?IdProcedimento=" + IdProcedimento + "&IdEmpresa=" + IdEmpresa + "&IdUsuario=" + IdUsuario;

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "alerta", String.Format("alert('{0}');", ex.Message), true);
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


        protected void gridEmpregados_ItemCommand(object sender, EO.Web.GridCommandEventArgs e)
        {


            //Session["Empregado"] = e.Item.Key.ToString();
            //Session["NomeEmpregado"] = e.Item.Cells[0].Value.ToString();

            Response.Redirect("CadProcedimento.aspx?IdProcedimento=" + e.Item.Key.ToString() + "&IdEmpresa=" + lbl_Id_Empresa.Text + "&IdUsuario=" + lbl_Id_Usuario.Text);

        }


	}
}
