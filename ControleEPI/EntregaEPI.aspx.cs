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
using Entities;

namespace Ilitera.Net
{
    /// <summary>
	/// Summary description for ListagemEPI.
	/// </summary>
	public partial class EntregaEPI : System.Web.UI.Page
	{
		private string QtdEntregue, NumeroCA, IdCA = string.Empty;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
            InicializaWebPageObjects();
            string xUsuario = Session["usuarioLogado"].ToString();

            Entities.Usuario user = (Entities.Usuario)Session["usuarioLogado"];

            int xIdEmpresa = System.Convert.ToInt32(Session["Empresa"]);
            int xIdUsuario = user.IdUsuario;

            lbl_Id_Empresa.Text = xIdEmpresa.ToString().Trim();
            lbl_Id_Usuario.Text = xIdUsuario.ToString().Trim();

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
				PopulaDropDownEPI();
				PopulaDropDownEmpregado();
				PopulaGridDefault();
			}
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
			this.DGridItemsEPI.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGridItemsEPI_ItemCommand);
			this.DGridItemsEPI.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DGridItemsEPI_PageIndexChanged);
			this.DGridItemsEPI.CancelCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGridItemsEPI_CancelCommand);
			this.DGridItemsEPI.EditCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGridItemsEPI_EditCommand);
			this.DGridItemsEPI.UpdateCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGridItemsEPI_UpdateCommand);

		}
		#endregion

		private void PopulaGridDefault()
		{
			DataSet ds = new DataSet();
			
			DataTable table = new DataTable("Default");
			table.Columns.Add("IdEPIClienteCA", Type.GetType("System.String"));
			table.Columns.Add("IdEPI", Type.GetType("System.String"));
			table.Columns.Add("IdCA", Type.GetType("System.String"));
			table.Columns.Add("Remover", Type.GetType("System.String"));
			table.Columns.Add("NumeroCA", Type.GetType("System.String"));
			table.Columns.Add("DataRecebimento", Type.GetType("System.String"));
			table.Columns.Add("QtdEntregue", Type.GetType("System.String"));
			
			ds.Tables.Add(table);

			ViewState["ItemsEPI"] = ds;

			DGridItemsEPI.DataSource = ds;
			DGridItemsEPI.Visible = true;
			DGridItemsEPI.AllowPaging = false;
			DGridItemsEPI.DataBind();
		}

		private void PopulaDropDownEmpregado()
		{
            //ddlEmpregado.DataSource = new Ilitera.Opsa.Data.Empregado().Get("nID_EMPR=" + lbl_Id_Empresa.Text + " ORDER BY tNO_EMPG");
            ddlEmpregado.DataSource = new Ilitera.Opsa.Data.Empregado().Get(" nId_Empregado in ( select nId_Empregado from tblEmpregado_Funcao where hdt_Termino is null and nID_EMPR=" + Convert.ToInt32(lbl_Id_Empresa.Text) + " ) ORDER BY tNO_EMPG");
            ddlEmpregado.DataValueField= "nID_EMPREGADO";
			ddlEmpregado.DataTextField = "tNO_EMPG";
			ddlEmpregado.DataBind();
			ddlEmpregado.Items.Insert(0, new ListItem("Selecione o Empregado...","0"));
		}

        protected void InicializaWebPageObjects()
        {
            //base.InicializaWebPageObjects();

			btnGravar.Attributes.Add("onClick", "javascript:return VerificaData();");
		}

		private void PopulaDropDownEPI()
		{
            //LaudoTecnico laudo = LaudoTecnico.GetUltimoLaudo( System.Convert.ToInt32( lbl_Id_Empresa.Text ));
			
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

            DataSet epitotal = new DataSet();
            Ilitera.Data.PPRA_EPI xEpis = new Ilitera.Data.PPRA_EPI();

            epitotal = xEpis.Retornar_EPIs_Utilizados_Laudos(System.Convert.ToInt32(lbl_Id_Empresa.Text));

			
			ddlEPI.DataSource = epitotal;
			ddlEPI.DataValueField= "Id";
			ddlEPI.DataTextField = "Descricao";
			ddlEPI.DataBind();
			ddlEPI.Items.Insert(0, new ListItem("Selecione o EPI...","0"));
		}

		private void PopulaDropDownCA(string IdEPI)
		{
            //se estoque habilitado
			//ddlCA.DataSource = new CA().GetCACadDispoEstoque(lbl_Id_Empresa.Text, IdEPI);

            ddlCA.DataSource = new CA().GetCAAssociadoEPI( System.Convert.ToInt32( lbl_Id_Empresa.Text), System.Convert.ToInt32( IdEPI));

			ddlCA.DataValueField= "IdCA";
			ddlCA.DataTextField = "NumeroCA";
			ddlCA.DataBind();
			ddlCA.Items.Insert(0, new ListItem("Selecione...","0"));
		}

		protected void btnGravar_Click(object sender, System.EventArgs e)
		{
			try
			{
				EPICAEntrega epiEntrega = new EPICAEntrega();

				PopulaEPICAEntrega(epiEntrega);

				StringBuilder st = new StringBuilder();
				Guid strAux = Guid.NewGuid();
						
				st.Append("window.alert(\"A entrega de EPI's ao empregado foi cadastrada com sucesso!\");");

                System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");

                if (ckbRelatorioEntrega.Checked)
                    st.Append(strOpenReport("ControleEPI", "RelatorioEntregaEPI.aspx?IliteraSystem=" + strAux.ToString() + strAux.ToString()
                    	+ "&IdUsuario=" + lbl_Id_Usuario.Text + "&IdEmpresa=" + lbl_Id_Empresa.Text +  "&D1=" + epiEntrega.DataRecebimento.ToString("dd/MM/yyyy", ptBr ) + "&D2=" + epiEntrega.DataRecebimento.ToString("dd/MM/yyyy", ptBr) + "&IdEmpregado=" + ddlEmpregado.SelectedValue + "&IdEPICAEntrega=" + epiEntrega.Id, "RelatorioEntregaEPI"));
                    

                this.ClientScript.RegisterStartupScript(this.GetType(), "FecharJanela", st.ToString(), true);
				
				ResetHTMLControls();
			}
			catch(Exception ex)
			{
				lblError.Text = ex.Message;
			} 
		}

		private void PopulaEPICAEntrega(EPICAEntrega epiEntrega)
		{
			if (DGridItemsEPI.Items.Count != 0)
			{
				if (ddlEmpregado.SelectedItem.Value != "0")
				{
					epiEntrega.Inicialize();

					epiEntrega.IdEmpregado.Id = Convert.ToInt32(ddlEmpregado.SelectedItem.Value);
					epiEntrega.DataRecebimento = new DateTime(Convert.ToInt32(txtaar.Text), Convert.ToInt32(txtmmr.Text), Convert.ToInt32(txtddr.Text));

					epiEntrega.UsuarioId = System.Convert.ToInt32( lbl_Id_Usuario.Text);
					epiEntrega.Save();

                    foreach (DataRow EPIItem in ((DataSet)ViewState["ItemsEPI"]).Tables[0].Select())
					{
						int qtdestoque = 0;

						EPICAEntregaDetalhe epiEntregaDetalhe = new EPICAEntregaDetalhe();
						epiEntregaDetalhe.Inicialize();

						epiEntregaDetalhe.IdEPICAEntrega = epiEntrega;
						epiEntregaDetalhe.IdEPIClienteCA.Id = Convert.ToInt32(EPIItem["IdEPIClienteCA"]);
						epiEntregaDetalhe.QtdEntregue = Convert.ToInt32(EPIItem["QtdEntregue"]);
                        epiEntregaDetalhe.Origem = "M";

						DataSet ds = new EPIClienteCA().GetEPIClienteCAExistente(lbl_Id_Empresa.Text , EPIItem["IdCA"].ToString());

						foreach (DataRow rowepiclienteca in ds.Tables[0].Select())
						{
							EPIClienteCA epica = new EPIClienteCA(Convert.ToInt32(rowepiclienteca["IdEPIClienteCA"]));
							qtdestoque = epica.QtdEstoque - Convert.ToInt32(EPIItem["QtdEntregue"]);
						
							if (qtdestoque < 0)
								epica.QtdEstoque = 0;
							else
								epica.QtdEstoque -= Convert.ToInt32(EPIItem["QtdEntregue"]);

							epica.UsuarioId = System.Convert.ToInt32( lbl_Id_Usuario.Text);
							epica.Save();
						}
                        epiEntregaDetalhe.UsuarioId = System.Convert.ToInt32(lbl_Id_Usuario.Text);
						epiEntregaDetalhe.Save();
					}
				}
				else
					throw new Exception("O empregado precisa ser selecionado!");
			}
			else
				throw new Exception("Deve ser adicionado pelo menos um EPI para sua entrega ao empregado!");
		}
		
		private void ResetHTMLControls()
		{
			ddlEPI.SelectedItem.Selected = false;
			int itemsca = ddlCA.Items.Count;
			if (itemsca>1)
				for (int x=0; x<itemsca-1; x++)
					ddlCA.Items.RemoveAt(1);
			
			txtQtdEntregue.Text = "";
			PopulaGridDefault();
			lblTotRegistros.Text = "";
			ddlEmpregado.SelectedItem.Selected = false;
			txtddr.Text = "";
			txtmmr.Text = "";
			txtaar.Text = "";
			ckbRelatorioEntrega.Checked = true;
			lblError.Text = "";
		}

		protected void lblCancel_Click(object sender, System.EventArgs e)
		{
			ResetHTMLControls();
		}

		protected void ddlEPI_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			PopulaDropDownCA(ddlEPI.SelectedItem.Value);
		}

		protected DataTable GetDropDownCAEditar(string idca, string IdEPI)
		{
			DataSet ds = new CA().Get("IdCA IN (SELECT IdCA FROM EPIClienteCA WHERE IdCliente=" + lbl_Id_Empresa.Text 
				+" AND IdEPI=" + IdEPI
				+" AND QtdEstoque>0)"
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

		protected void btnAdicionarEPI_Click(object sender, System.EventArgs e)
		{
			if (ddlCA.SelectedItem.Value != "0")
			{
				if (txtQtdEntregue.Text != "" || !txtQtdEntregue.Text.Equals(""))
				{
					//PopulaGrid(DGridItemsEPI, GeraDataSet("Adicionar"), lblTotRegistros);
                    DGridItemsEPI.DataSource = GeraDataSet("Adicionar");
                    DGridItemsEPI.DataBind();
                        
					lblError.Text = "";
				}
				else
					lblError.Text = "A quantidade de EPI entregue deve ser informada!";
			}
			else
				lblError.Text = "O CA deve ser selecionado!";
		}

		private DataSet GeraDataSet(string tipo)
		{
			DataSet dscache = new DataSet();
			DataSet ds = new DataSet();
			DataRow rowexiste;
			//bool checkEPI = false;

			DataTable table = new DataTable("Default");
			table.Columns.Add("IdEPIClienteCA", Type.GetType("System.String"));
			table.Columns.Add("IdEPI", Type.GetType("System.String"));
			table.Columns.Add("IdCA", Type.GetType("System.String"));
			table.Columns.Add("Remover", Type.GetType("System.String"));
			table.Columns.Add("NomeEPI", Type.GetType("System.String"));
			table.Columns.Add("NumeroCA", Type.GetType("System.String"));
			table.Columns.Add("QtdEntregue", Type.GetType("System.String"));
			ds.Tables.Add(table);

            dscache = (DataSet)ViewState["ItemsEPI"];
			
			foreach (DataRow rowdscachecheck in dscache.Tables[0].Select())
			{
				if (rowdscachecheck["IdEPI"].ToString() == ddlEPI.SelectedItem.Value && rowdscachecheck["IdCA"].ToString() == ddlCA.SelectedItem.Value)
				{
					//checkEPI = true;
					break;
				}
			}

			foreach (DataRow rowdscache in dscache.Tables[0].Select())
			{
				if (ViewState["IdCA"].ToString() != rowdscache["IdCA"].ToString() || ViewState["IdEPI"].ToString() != rowdscache["IdEPI"].ToString())
				{
					rowexiste = ds.Tables[0].NewRow();

					rowexiste["IdEPIClienteCA"] = rowdscache["IdEPIClienteCA"];
					rowexiste["IdEPI"] = rowdscache["IdEPI"];
					rowexiste["IdCA"] = rowdscache["IdCA"];
					rowexiste["Remover"] = "Remover";
					rowexiste["NomeEPI"] = rowdscache["NomeEPI"];
					rowexiste["NumeroCA"] = rowdscache["NumeroCA"];
					rowexiste["QtdEntregue"] = rowdscache["QtdEntregue"];

					ds.Tables[0].Rows.Add(rowexiste);
				}
				else
				{
					if (tipo == "AtualizarEPI")
					{
						bool checkEPIEditar = false;
						ArrayList listCAExistente = new ArrayList();

						foreach (DataRow rowdscachecheckeditar in dscache.Tables[0].Select())
						{
							if (rowdscachecheckeditar["IdEPI"].ToString() == ViewState["IdEPI"].ToString())
								listCAExistente.Add(rowdscachecheckeditar["IdCA"].ToString());
						}

						foreach (string idca in listCAExistente)
						{
							if (ViewState["IdCA"].ToString() != IdCA && IdCA == idca)
							{
								checkEPIEditar = true;
								break;
							}
						}

						if (checkEPIEditar == false)
						{
							rowexiste = ds.Tables[0].NewRow();

							rowexiste["IdEPIClienteCA"] = rowdscache["IdEPIClienteCA"];
							rowexiste["IdEPI"] = rowdscache["IdEPI"];
							rowexiste["IdCA"] = IdCA;
							rowexiste["Remover"] = "Remover";
							rowexiste["NomeEPI"] = rowdscache["NomeEPI"];
							rowexiste["NumeroCA"] = NumeroCA;
							rowexiste["QtdEntregue"] = QtdEntregue;

							ds.Tables[0].Rows.Add(rowexiste);
						}
						else if (checkEPIEditar == true)
						{
							StringBuilder st = new StringBuilder();

							st.Append("window.alert(\"Não é possível alterar este item para este CA! Ele já está adicionado!\");");

                            this.ClientScript.RegisterStartupScript(this.GetType(), "VerificaEPIExistente", st.ToString(), true);

							rowexiste = ds.Tables[0].NewRow();

							rowexiste["IdEPIClienteCA"] = rowdscache["IdEPIClienteCA"];
							rowexiste["IdEPI"] = rowdscache["IdEPI"];
							rowexiste["IdCA"] = rowdscache["IdCA"];
							rowexiste["Remover"] = "Remover";
							rowexiste["NomeEPI"] = rowdscache["NomeEPI"];
							rowexiste["NumeroCA"] = rowdscache["NumeroCA"];
							rowexiste["QtdEntregue"] = rowdscache["QtdEntregue"];

							ds.Tables[0].Rows.Add(rowexiste);
						}
					}
				}
			}

			if (tipo == "Adicionar" ) //&& checkEPI == false)
			{
				DataRow rowatual;
				rowatual = ds.Tables[0].NewRow();
			
				EPIClienteCA epica = new EPIClienteCA();
				DataSet findepica = new EPIClienteCA().GetEPIClienteCAExistente(ddlEPI.SelectedItem.Value, lbl_Id_Empresa.Text, ddlCA.SelectedItem.Value);

				rowatual["IdEPIClienteCA"] = findepica.Tables[0].Rows[0]["IdEPIClienteCA"];
				rowatual["IdEPI"] = ddlEPI.SelectedItem.Value;
				rowatual["IdCA"] = ddlCA.SelectedItem.Value;
				rowatual["Remover"] = "Remover";
				if (ddlEPI.SelectedItem.Text.Length > 32)
                    rowatual["NomeEPI"] = ddlEPI.SelectedItem.Text.Substring(0, 32) + "...";
				else
					rowatual["NomeEPI"] = ddlEPI.SelectedItem.Text;
				rowatual["NumeroCA"] = ddlCA.SelectedItem.Text;
				rowatual["QtdEntregue"] = txtQtdEntregue.Text;
				
				ds.Tables[0].Rows.Add(rowatual);
			}
            //else if (tipo == "Adicionar" ) // && checkEPI == true)
            //{
            //    StringBuilder st = new StringBuilder();

            //    st.Append("window.alert(\"Não é possível adicionar este EPI! Ele já está adicionado!\");");

            //    this.ClientScript.RegisterStartupScript(this.GetType(), "VerificaEPIExistente", st.ToString(), true);
            //}

            ViewState["ItemsEPI"] = ds;
			ViewState["IdCA"] = "";
			ViewState["IdEPI"] = "";

			return ds;
		}

		private void DGridItemsEPI_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DGridItemsEPI.CurrentPageIndex = e.NewPageIndex;
			//PopulaGrid(DGridItemsEPI, GeraDataSet("ChangePage"), lblTotRegistros);
		}

		private void DGridItemsEPI_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			if (e.CommandName == "RemoverEPI")
			{
				ViewState["IdCA"] = e.Item.Cells[2].Text;
				ViewState["IdEPI"] = e.Item.Cells[1].Text;
				//PopulaGrid(DGridItemsEPI, GeraDataSet("Remove"), lblTotRegistros);

				if (lblTotRegistros.Text == "Nenhum Registro Encontrado")
				{
					lblTotRegistros.Text = "";
					DGridItemsEPI.Visible = true;
				}
			}
		}

		private void DGridItemsEPI_EditCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			DGridItemsEPI.EditItemIndex = e.Item.ItemIndex;
			//PopulaGrid(DGridItemsEPI, GeraDataSet("EditItem"), lblTotRegistros);
		}

		private void DGridItemsEPI_CancelCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			DGridItemsEPI.EditItemIndex = -1;
			//PopulaGrid(DGridItemsEPI, GeraDataSet("CancelEditItem"), lblTotRegistros);
		}

		private void DGridItemsEPI_UpdateCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			ViewState["IdCA"] = ((TextBox)e.Item.FindControl("txtIdCA")).Text;
			ViewState["IdEPI"] = ((TextBox)e.Item.FindControl("txtIdEPI")).Text;
			IdCA = ((DropDownList)e.Item.FindControl("ddlCAEditar")).SelectedItem.Value;
			NumeroCA = ((DropDownList)e.Item.FindControl("ddlCAEditar")).SelectedItem.Text;
			QtdEntregue = ((TextBox)e.Item.FindControl("txtQtdEntregueEditar")).Text;
			DGridItemsEPI.EditItemIndex = -1;
			//PopulaGrid(DGridItemsEPI, GeraDataSet("AtualizarEPI"), lblTotRegistros);
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
