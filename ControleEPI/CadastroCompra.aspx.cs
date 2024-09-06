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
//using MestraNET;
using Ilitera.Common;
using Ilitera.Data;
using Ilitera.Opsa.Data;
using System.Text;
using System.Data.SqlClient;

namespace Ilitera.Net
{
    /// <summary>
	/// Summary description for ListagemEPI.
	/// </summary>
	public partial class CadastroCompra : System.Web.UI.Page
	{
		private string Quantidade, ValorUnitario, NumeroCA, IdCA = string.Empty;
		private CompraEPI populaCompraEPI;
		private CAFornecedorEPI fornecedorCAEPI = new CAFornecedorEPI();
		private bool verifica;

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


				PopulaDropDownFornecedor();
				PopulaGridDefault();
				PopulaDropDownCA();
				btnSalvar.Attributes.Add("onClick", "javascript: return VerificaData();");
			}
			else
				RetornaValorDDLFornecedor(ddlFornecedor);
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
			this.DGridItemsCompra.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGridItemsCompra_ItemCommand);
			this.DGridItemsCompra.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DGridItemsCompra_PageIndexChanged);
			this.DGridItemsCompra.CancelCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGridItemsCompra_CancelCommand);
			this.DGridItemsCompra.EditCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGridItemsCompra_EditCommand);
			this.DGridItemsCompra.UpdateCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGridItemsCompra_UpdateCommand);

		}
		#endregion
		
		private void RetornaValorDDLFornecedor(DropDownList ddl)
		{
			string ddlvalue = ddl.SelectedItem.Value;

			ddl.DataSource = new FornecedorEPI().GetFornecedor(lbl_Id_Empresa.Text);
			ddl.DataValueField = "IdFornecedorEPI";
			ddl.DataTextField = "NomeCompleto";
			ddl.DataBind();

			ddl.Items.Insert(0, new ListItem("Selecione o Fornecedor...","0"));
			
			for (int i=0; i<ddl.Items.Count; i++)
			{
				if (ddl.Items[i].Value == ddlvalue)
					ddl.Items.FindByValue(ddlvalue).Selected = true;
			}
		}

		protected DataTable GetDropDownCAEditar(string idca)
		{
			DataSet ds = new CA().Get("IdCA IN (SELECT IdCA FROM EPIClienteCA WHERE IdCliente=" + lbl_Id_Empresa.Text
				+" AND IdEPI IN (" + GetStrIdsEPIAtual() + "))"
				+" AND IdCA<>" + idca
				+" ORDER BY NumeroCA");

			DataSet dsSelect = new CA().Get("IdCA=" + idca);
			DataSet dsReturn = new DataSet();
			DataRow rowSelect, row;

			DataTable tableCAEdicao = new DataTable();

			tableCAEdicao.Columns.Add("IdCA", Type.GetType("System.String"));
			tableCAEdicao.Columns.Add("NumeroCA", Type.GetType("System.String"));

			dsReturn.Tables.Add(tableCAEdicao);

			rowSelect = dsReturn.Tables[0].NewRow();

			rowSelect["IdCA"] = dsSelect.Tables[0].Rows[0]["IdCA"];
			rowSelect["NumeroCA"] = dsSelect.Tables[0].Rows[0]["NumeroCA"];

			dsReturn.Tables[0].Rows.Add(rowSelect);

			foreach (DataRow dsrow in ds.Tables[0].Select())
			{
				row = dsReturn.Tables[0].NewRow();
	
				row["IdCA"] = dsrow["IdCA"];
				row["NumeroCA"] = dsrow["NumeroCA"];

				dsReturn.Tables[0].Rows.Add(row);
			}

			return dsReturn.Tables[0];
		}

		private string GetStrIdsEPIAtual()
		{
            //LaudoTecnico laudo = LaudoTecnico.GetUltimoLaudo( System.Convert.ToInt32( lbl_Id_Empresa.Text) );
			
            //DataSet ds = new Ghe().Get("nID_LAUD_TEC =" + laudo.Id + " ORDER BY tNO_FUNC");
            //ArrayList epitotal = new ArrayList();
            //bool Verifica;
            //bool Verifica2;

            //foreach (DataRow ghe in ds.Tables[0].Select())
            //{
            //    ArrayList listGheEpiExistente = new GheEpiExistente().Find("nID_FUNC=" + ghe["nID_FUNC"]);
            //    ArrayList listRiscoEpiExistente = new EpiExistente().FindIn(new PPRA().GetType(), "", true, "nID_FUNC=" + ghe["nID_FUNC"]);
			
            //    foreach (GheEpiExistente gheepi in listGheEpiExistente)
            //    {
            //        gheepi.nID_EPI.Find();
            //        Verifica = true;
				
            //        foreach (Epi epicompare in epitotal)
            //        {
            //            if (Convert.ToInt32(gheepi.nID_EPI.Id) == Convert.ToInt32(epicompare.Id))
            //            {
            //                Verifica = false;
            //                break;
            //            }
            //        }
				
            //        if (Verifica == true)
            //            epitotal.Add(gheepi.nID_EPI);
            //    }

            //    foreach (EpiExistente riscoepi in listRiscoEpiExistente)
            //    {
            //        riscoepi.nID_EPI.Find();
            //        Verifica2 = true;
				
            //        foreach (Epi epicompare in epitotal)
            //        {
            //            if (Convert.ToInt32(riscoepi.nID_EPI.Id) == Convert.ToInt32(epicompare.Id))
            //            {
            //                Verifica2 = false;
            //                break;
            //            }
            //        }
				
            //        if (Verifica2 == true)
            //            epitotal.Add(riscoepi.nID_EPI);
            //    }
            //}

            //epitotal.Sort();

            //ArrayList alEPIAtual = epitotal;
            //string IdsEPI = string.Empty;

            //foreach (Epi epiAtual in alEPIAtual)
            //    if (IdsEPI == string.Empty)
            //        IdsEPI = epiAtual.Id.ToString();
            //    else
            //        IdsEPI += ", " + epiAtual.Id.ToString();

            DataSet dS = new DataSet();
            Ilitera.Data.PPRA_EPI xEpis = new Ilitera.Data.PPRA_EPI();

            dS = xEpis.Retornar_EPIs_Utilizados_Laudos(System.Convert.ToInt32(lbl_Id_Empresa.Text));

            //ArrayList epitotal = new ArrayList();
            string IdsEPI = string.Empty;

            foreach (DataRow dtRow in dS.Tables[0].Rows)
            {
                // epitotal.Add(dtRow);
                if (IdsEPI == string.Empty)
                    IdsEPI = dtRow["Id"].ToString();
                else
                    IdsEPI += ", " + dtRow["Id"].ToString();

            }

            if (IdsEPI.Trim() == "")
            { IdsEPI = " 0 "; }

			return IdsEPI;
		}
		
		private void PopulaDropDownCA()
		{			
			ddlCA.DataSource = new CA().Get("IdCA IN (SELECT IdCA FROM EPIClienteCA WHERE IdCliente=" + lbl_Id_Empresa.Text
				+" AND IdEPI IN (" + GetStrIdsEPIAtual() + "))"
				+" ORDER BY NumeroCA");
			ddlCA.DataValueField= "IdCA";
			ddlCA.DataTextField = "NumeroCA";
			ddlCA.DataBind();

			ddlCA.Items.Insert(0, new ListItem("Selecione o CA...", "0"));
		}

		private void PopulaGridDefault()
		{
			DataSet ds = new DataSet();
			
			DataTable table = new DataTable("Default");
			table.Columns.Add("IdCA", Type.GetType("System.String"));
			table.Columns.Add("LoteFabricacao", Type.GetType("System.String"));
			table.Columns.Add("Remover", Type.GetType("System.String"));
			table.Columns.Add("NumeroCA", Type.GetType("System.String"));
			table.Columns.Add("Quantidade", Type.GetType("System.String"));
			table.Columns.Add("ValorItem", Type.GetType("System.String"));
			table.Columns.Add("ValorTotal", Type.GetType("System.String"));
			
			ds.Tables.Add(table);

			Cache["ItemsCompra"] = ds;

			DGridItemsCompra.DataSource = ds;
			DGridItemsCompra.Visible = true;
			DGridItemsCompra.AllowPaging = false;
			DGridItemsCompra.DataBind();
		}
		
		private void PopulaDropDownFornecedor()
		{
			ddlFornecedor.DataSource = new FornecedorEPI().GetFornecedor(lbl_Id_Empresa.Text);
			ddlFornecedor.DataValueField= "IdFornecedorEPI";
			ddlFornecedor.DataTextField = "NomeCompleto";
			ddlFornecedor.DataBind();
			ddlFornecedor.Items.Insert(0, new ListItem("Selecione o Fornecedor...","0"));
		}

		protected void btnNovoFornecedor_Click(object sender, System.EventArgs e)
		{
			StringBuilder st = new StringBuilder();

			st.Append("void(addItem(centerWin('AddFornecedor.aspx?TipoJanela=CadastroCompra&IdFornecedorEPI=" + ddlFornecedor.SelectedItem.Value + "&IdUsuario=" + lbl_Id_Usuario.Text + "&IdEmpresa=" + lbl_Id_Empresa.Text + "',310,175,\'CadFornecedor\'),\'Todos\'))");

            this.ClientScript.RegisterStartupScript(this.GetType(), "CadastroCompra", st.ToString(), true);
		}

		private void PopulaCompraEPI()
		{

            Cliente xCliente = new Cliente(System.Convert.ToInt32(lbl_Id_Empresa.Text));

			populaCompraEPI.IdCliente = xCliente;
			populaCompraEPI.IdFornecedorEPI.Id = Convert.ToInt32(ddlFornecedor.SelectedItem.Value);
			populaCompraEPI.Documento = txtDocumento.Text;
			populaCompraEPI.DataCompra = new DateTime(Convert.ToInt32(txtaac.Text), Convert.ToInt32(txtmmc.Text), Convert.ToInt32(txtddc.Text));
			
			if (txtValorTotal.Text == "" || txtValorTotal.Text.Equals(""))
				throw new ArgumentException("Você deve preencher o Valor Total da compra!");
			else
				populaCompraEPI.ValorTotal = Convert.ToDouble(txtValorTotal.Text);
		}

		private void ResetHTMLControls()
		{
			ddlCA.ClearSelection();
			ddlFornecedor.ClearSelection();
			txtDocumento.Text = "";
			lblError.Text = "";
			txtddc.Text = "";
			txtmmc.Text = "";
			txtaac.Text = "";
			txtValorTotal.Text = "";
			txtQuantidade.Text = "";
			txtLoteFabricacao.Text = "";
			txtValorUnitario.Text = "";
			PopulaGridDefault();
			lblTotRegistros.Text = "";
		}

		private DataSet GeraDataSet(string tipo)
		{
			DataSet dscache = new DataSet();
			DataSet ds = new DataSet();
			DataRow rowexiste;
			bool checkCA = false;

			DataTable table = new DataTable("Default");
			table.Columns.Add("IdCA", Type.GetType("System.String"));
			table.Columns.Add("LoteFabricacao", Type.GetType("System.String"));
			table.Columns.Add("Remover", Type.GetType("System.String"));
			table.Columns.Add("NumeroCA", Type.GetType("System.String"));
			table.Columns.Add("Quantidade", Type.GetType("System.String"));
			table.Columns.Add("ValorItem", Type.GetType("System.String"));
			table.Columns.Add("ValorTotal", Type.GetType("System.String"));
			ds.Tables.Add(table);
			
			dscache = (DataSet)Cache["ItemsCompra"];
			
			foreach (DataRow rowdscachecheck in dscache.Tables[0].Select())
			{
				if (rowdscachecheck["IdCA"].ToString() == ddlCA.SelectedItem.Value)
				{
					checkCA = true;
					break;
				}
			}

			foreach (DataRow rowdscache in dscache.Tables[0].Select())
			{
				if (ViewState["IdCA"].ToString() != rowdscache["IdCA"].ToString())
				{
					rowexiste = ds.Tables[0].NewRow();

					rowexiste["IdCA"] = rowdscache["IdCA"];
					rowexiste["LoteFabricacao"] = rowdscache["LoteFabricacao"];
					rowexiste["Remover"] = "Remover";
					rowexiste["NumeroCA"] = rowdscache["NumeroCA"];
					rowexiste["Quantidade"] = rowdscache["Quantidade"];
					rowexiste["ValorItem"] = rowdscache["ValorItem"];
					rowexiste["ValorTotal"] = rowdscache["ValorTotal"];

					ds.Tables[0].Rows.Add(rowexiste);
				}
				else
				{
					if (tipo == "AtualizarCA")
					{
						bool checkCAEditar = false;
						ArrayList listCAExistente = new ArrayList();

						foreach (DataRow rowdscachecheckeditar in dscache.Tables[0].Select())
						{
							listCAExistente.Add(rowdscachecheckeditar["IdCA"].ToString());
						}

						foreach (string idca in listCAExistente)
						{
							if (ViewState["IdCA"].ToString() != IdCA && IdCA == idca)
							{
								checkCAEditar = true;
								break;
							}
						}
						
						if (checkCAEditar == false)
						{
							rowexiste = ds.Tables[0].NewRow();

							rowexiste["IdCA"] = IdCA;
							rowexiste["LoteFabricacao"] = rowdscache["LoteFabricacao"];
							rowexiste["Remover"] = "Remover";
							rowexiste["NumeroCA"] = NumeroCA;
							rowexiste["Quantidade"] = Quantidade;
							rowexiste["ValorItem"] = ValorUnitario;
							rowexiste["ValorTotal"] = Convert.ToInt32(Quantidade) * Convert.ToDouble(ValorUnitario);

							ds.Tables[0].Rows.Add(rowexiste);
						}
						else if (checkCAEditar == true)
						{
							StringBuilder st = new StringBuilder();

							st.Append("window.alert(\"Não é possível alterar este item para este CA! Ele já está adicionado!\");");

                            this.ClientScript.RegisterStartupScript(this.GetType(), "VerificaCAExistente", st.ToString(), true);

							rowexiste = ds.Tables[0].NewRow();

							rowexiste["IdCA"] = rowdscache["IdCA"];
							rowexiste["LoteFabricacao"] = rowdscache["LoteFabricacao"];
							rowexiste["Remover"] = "Remover";
							rowexiste["NumeroCA"] = rowdscache["NumeroCA"];
							rowexiste["Quantidade"] = rowdscache["Quantidade"];
							rowexiste["ValorItem"] = rowdscache["ValorItem"];
							rowexiste["ValorTotal"] = rowdscache["ValorTotal"];

							ds.Tables[0].Rows.Add(rowexiste);
						}
					}
				}
			}

			if (tipo == "Adicionar" && checkCA == false)
			{
				DataRow rowatual;
				rowatual = ds.Tables[0].NewRow();
			
				rowatual["IdCA"] = ddlCA.SelectedItem.Value;
				rowatual["LoteFabricacao"] = txtLoteFabricacao.Text;
				rowatual["Remover"] = "Remover";
				rowatual["NumeroCA"] = ddlCA.SelectedItem.Text;
				rowatual["Quantidade"] = txtQuantidade.Text;
				rowatual["ValorItem"] = txtValorUnitario.Text;
				rowatual["ValorTotal"] = Convert.ToInt32(txtQuantidade.Text)*Convert.ToDouble(txtValorUnitario.Text);
				
				ds.Tables[0].Rows.Add(rowatual);
			}
			else if (tipo == "Adicionar" && checkCA == true)
			{
				StringBuilder st = new StringBuilder();

				st.Append("window.alert(\"Não é possível adicionar este CA! Ele já está adicionado!\");");

                this.ClientScript.RegisterStartupScript(this.GetType(), "VerificaCAExistente", st.ToString(), true);
			}

			Cache["ItemsCompra"] = ds;
			ViewState["IdCA"] = "";

			return ds;
		}

		private void DGridItemsCompra_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DGridItemsCompra.CurrentPageIndex = e.NewPageIndex;
            DGridItemsCompra.DataSource = GeraDataSet("ChangePage");
            DGridItemsCompra.DataBind();

            lblTotRegistros.Visible = false;
		}

		private void DGridItemsCompra_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			if (e.CommandName == "RemoverCA")
			{
				ViewState["IdCA"] = e.Item.Cells[0].Text;
				
                DGridItemsCompra.DataSource = GeraDataSet("Remove");
                DGridItemsCompra.DataBind();

                lblTotRegistros.Visible = false;

				//if (lblTotRegistros.Text == "Nenhum Registro Encontrado")
				//{
				//	lblTotRegistros.Text = "";
				//	DGridItemsCompra.Visible = true;
				//}
			}
		}

		protected void btnCadastro_Click(object sender, System.EventArgs e)
		{
			//StringBuilder st = new StringBuilder();

			//st.Append("window.location.href='CadastroEPI.aspx?IdUsuario=" + lbl_Id_Usuario.Text + "&IdEmpresa=" + lbl_Id_Empresa.Text + "';");

            //this.ClientScript.RegisterStartupScript(this.GetType(), "CadastroCompra", st.ToString(), true);

            Session["Pagina_Anterior"] = "CadastroCompra.aspx";
            Response.Redirect("NovoCA.aspx?IdUsuario=" + lbl_Id_Usuario.Text + "&IdEmpresa=" + lbl_Id_Empresa.Text);

            PopulaDropDownCA();

		}

		protected void btnAdicionar_Click(object sender, System.EventArgs e)
		{
			if (ddlCA.SelectedItem.Value != "0")
				if (txtQuantidade.Text != "" || !txtQuantidade.Text.Equals(""))
					if (txtLoteFabricacao.Text != "" || !txtLoteFabricacao.Text.Equals(""))
						if (txtValorUnitario.Text != "" || !txtValorUnitario.Text.Equals(""))
						{							
                            DGridItemsCompra.DataSource = GeraDataSet("Adicionar");
                            DGridItemsCompra.DataBind();

                            lblTotRegistros.Visible = false;
							lblError.Text = "";
						}
						else
							lblError.Text = "O Valor Unitário deve ser preenchido!";
					else
						lblError.Text = "O Lote de Fabricação deve ser preenchido!";
				else
					lblError.Text = "A Quantidade deve ser preenchida!";
			else
				lblError.Text = "O CA deve ser selecionado!";
		}

		private void DGridItemsCompra_EditCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			DGridItemsCompra.EditItemIndex = e.Item.ItemIndex;
			DGridItemsCompra.Columns[0].HeaderStyle.Width = new Unit("0px");
			DGridItemsCompra.Columns[1].HeaderStyle.Width = new Unit("0px");
			DGridItemsCompra.Columns[2].HeaderStyle.Width = new Unit("80px");
			DGridItemsCompra.Columns[3].HeaderStyle.Width = new Unit("114px");
			DGridItemsCompra.Columns[4].HeaderStyle.Width = new Unit("113px");
			DGridItemsCompra.Columns[5].HeaderStyle.Width = new Unit("114px");
			DGridItemsCompra.Columns[6].HeaderStyle.Width = new Unit("114px");
			DGridItemsCompra.Columns[7].HeaderStyle.Width = new Unit("60px");

            DGridItemsCompra.DataSource = GeraDataSet("EditItem");
            DGridItemsCompra.DataBind();

            lblTotRegistros.Visible = false;
		}

		private void DGridItemsCompra_CancelCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			DGridItemsCompra.EditItemIndex = -1;

            DGridItemsCompra.DataSource = GeraDataSet("CancelEditItem");
            DGridItemsCompra.DataBind();

            lblTotRegistros.Visible = false;

		}

		private void DGridItemsCompra_UpdateCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			ViewState["IdCA"] = ((TextBox)e.Item.FindControl("txtIdCA")).Text;
			IdCA = ((DropDownList)e.Item.FindControl("ddlCAEditar")).SelectedItem.Value;
			NumeroCA = ((DropDownList)e.Item.FindControl("ddlCAEditar")).SelectedItem.Text;
			Quantidade = ((TextBox)e.Item.FindControl("txtQuantidadeGrid")).Text;
			ValorUnitario = ((TextBox)e.Item.FindControl("txtValorItem")).Text;
			DGridItemsCompra.EditItemIndex = -1;

            DGridItemsCompra.DataSource = GeraDataSet("AtualizarCA");
            DGridItemsCompra.DataBind();

            lblTotRegistros.Visible = false;
		}

		protected void btnSalvar_Click(object sender, System.EventArgs e)
		{
			try
			{
				populaCompraEPI = new CompraEPI();
				populaCompraEPI.Inicialize();

				PopulaCompraEPI();

				if (DGridItemsCompra.Items.Count == 0)
					throw new ArgumentException("Você deve adicionar o(s) Item(s) da Compra!");
				else
				{
					populaCompraEPI.UsuarioId = usuario.Id;
					populaCompraEPI.Save();
					foreach (DataRow itemrow in ((DataSet)Cache["ItemsCompra"]).Tables[0].Select())
					{
						CompraEPIDetalhe populaEPIDetalhe = new CompraEPIDetalhe();
						populaEPIDetalhe.Inicialize();

						populaEPIDetalhe.IdCompraEPI.Id = populaCompraEPI.Id;
						populaEPIDetalhe.IdCA.Id = Convert.ToInt32(itemrow["IdCA"]);
						populaEPIDetalhe.Quantidade = Convert.ToInt32(itemrow["Quantidade"]);
						populaEPIDetalhe.ValorItem = Convert.ToDouble(itemrow["ValorItem"]);
						populaEPIDetalhe.LoteFabricacao = itemrow["LoteFabricacao"].ToString();

						verifica = false;
						DataSet verificaFornecedorCAEPI = fornecedorCAEPI.Get("IdFornecedorEPI=" + ddlFornecedor.SelectedItem.Value);
						
						if (verificaFornecedorCAEPI.Tables[0].Rows.Count == 0)
						{
							fornecedorCAEPI = new CAFornecedorEPI();
							fornecedorCAEPI.Inicialize();

							fornecedorCAEPI.IdCA.Id = Convert.ToInt32(itemrow["IdCA"]);
							fornecedorCAEPI.IdFornecedorEPI.Id = Convert.ToInt32(ddlFornecedor.SelectedItem.Value);

							fornecedorCAEPI.UsuarioId = usuario.Id;
							fornecedorCAEPI.Save();
						}
						else
						{
							foreach (DataRow rowFornecedorCAEPI in verificaFornecedorCAEPI.Tables[0].Select())
								if (rowFornecedorCAEPI["IdCA"].ToString() == itemrow["IdCA"].ToString())
								{
									verifica = true;
									break;
								}

							if (verifica == false)
							{
								fornecedorCAEPI = new CAFornecedorEPI();
								fornecedorCAEPI.Inicialize();

								fornecedorCAEPI.IdCA.Id = Convert.ToInt32(itemrow["IdCA"]);
								fornecedorCAEPI.IdFornecedorEPI.Id = Convert.ToInt32(ddlFornecedor.SelectedItem.Value);

								fornecedorCAEPI.UsuarioId = usuario.Id;
								fornecedorCAEPI.Save();
							}
						}

						EPIClienteCA quantidadeCA = new EPIClienteCA();
						DataSet quantCAds = quantidadeCA.GetEPIClienteCAExistente(lbl_Id_Empresa.Text, itemrow["IdCA"].ToString());

						foreach (DataRow CArow in quantCAds.Tables[0].Select())
						{
							EPIClienteCA updateQuantCA = new EPIClienteCA(Convert.ToInt32(CArow["IdEPIClienteCA"]));

							updateQuantCA.QtdEstoque += Convert.ToInt32(itemrow["Quantidade"]);

							updateQuantCA.UsuarioId = usuario.Id;
							updateQuantCA.Save();
						}
						
						populaEPIDetalhe.UsuarioId = usuario.Id;
						populaEPIDetalhe.Save();
					}
				}
				
				StringBuilder st = new StringBuilder("");

				st.Append("window.alert(\"A compra foi adicionada com sucesso!\");");

                this.ClientScript.RegisterStartupScript(this.GetType(), "FecharJanela", st.ToString(), true);

				ResetHTMLControls();
			}
			catch(Exception ex)
			{
				lblError.Text = ex.Message;
			}
		}

		protected void lblCancelar_Click(object sender, System.EventArgs e)
		{
			ResetHTMLControls();
		}

        protected void btnNovaCelula_Click(object sender, EventArgs e)
        {
            Session["Pagina_Anterior"] = "CadastroCompra.aspx";
            Response.Redirect("AddFornecedor.aspx?IdUsuario=" + lbl_Id_Usuario.Text + "&IdEmpresa=" + lbl_Id_Empresa.Text);

            PopulaDropDownFornecedor();

        }
	}
}

