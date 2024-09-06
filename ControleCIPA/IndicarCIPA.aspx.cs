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
using Ilitera.Opsa.Data;
using Ilitera.Opsa.Report;
using System.Text;
using Entities;

namespace Ilitera.Net.ControleCIPA
{
    /// <summary>
	/// Summary description for ListagemEPI.
	/// </summary>
	public partial class IndicarCIPA : System.Web.UI.Page
	{
		private string QtdEntregue, NumeroCA, IdCA = string.Empty;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
            InicializaWebPageObjects();
            string xUsuario = Session["usuarioLogado"].ToString();


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

			if (!IsPostBack)
			{
				PopulaDropDownCIPA();
				PopulaDropDownEmpregado();
				PopulaGridDefault();
			}

            //desabilitar botão de adicionar candidato, se já possuir votação ??



            
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
			this.DGridCandidatos.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGridCandidatos_ItemCommand);
			this.DGridCandidatos.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DGridCandidatos_PageIndexChanged);
			this.DGridCandidatos.CancelCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGridCandidatos_CancelCommand);
			this.DGridCandidatos.EditCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGridCandidatos_EditCommand);
			this.DGridCandidatos.UpdateCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGridCandidatos_UpdateCommand);

		}
		#endregion


        private void PopulaGridCandidatos()
        {
            DataSet ds = new DataSet();

            DataTable table = new DataTable("Default");
            table.Columns.Add("IdParticipantesEleicaoCipa", Type.GetType("System.Int32"));
            table.Columns.Add("tNo_Empg", Type.GetType("System.Int32"));
            
            ds.Tables.Add(table);

            Ilitera.Data.PPRA_EPI xCIPA = new Ilitera.Data.PPRA_EPI();

            ds = xCIPA.Retornar_Candidatos_CIPA(System.Convert.ToInt32(ddlCIPA.SelectedValue.ToString()));



            DGridCandidatos.DataSource = ds;            
            DGridCandidatos.DataBind();
        }



        private void PopulaGridComissao()
        {
            DataSet ds = new DataSet();

            DataTable table = new DataTable("Default");
            table.Columns.Add("IdMembroComissaoEleitoral", Type.GetType("System.Int32"));
            table.Columns.Add("IdEmpregado", Type.GetType("System.Int32"));
            table.Columns.Add("IdCipa", Type.GetType("System.Int32"));
            table.Columns.Add("NomeCargo", Type.GetType("System.String"));
            table.Columns.Add("NomeMembro", Type.GetType("System.String"));
            

            ds.Tables.Add(table);


            DGridComissao.DataSource = new MembroComissaoEleitoral().Get("IdCipa=" + ddlCIPA.SelectedValue.ToString());
            DGridComissao.DataBind();
            
          
        }

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

			DGridCandidatos.DataSource = ds;
			DGridCandidatos.Visible = true;
			DGridCandidatos.AllowPaging = false;
			DGridCandidatos.DataBind();

            DGridComissao.DataSource = ds;
            DGridComissao.Visible = true;
            DGridComissao.AllowPaging = false;
            DGridComissao.DataBind();

            string strWhere = "IdCipa =" + ddlCIPA.SelectedValue.ToString()
                    + " AND Numero = 0 "
                    + " AND IndGrupoMembro IN (0, 1) "
                    + " AND IndStatus=" + ((int)MembroCipa.Status.Ativo).ToString()
                    + " AND IndTitularSuplente=" + (int)TitularSuplente.Titular
                    + " AND NomeMembro NOT IN(SELECT NomeMembro FROM MembroComissaoEleitoral WHERE IdCipa=" + ddlCIPA.SelectedValue.ToString() + " AND NomeMembro IS NOT NULL)"
                    + " ORDER BY Numero";

            DGridComissao.DataSource = new MembroCipa().Get(strWhere);


		}

		private void PopulaDropDownEmpregado()
		{
            ddlEmpregado2.DataSource = new Ilitera.Opsa.Data.Empregado().Get("nID_EMPR=" + lbl_Id_Empresa.Text + " ORDER BY tNO_EMPG");
            ddlEmpregado2.DataValueField = "nID_EMPREGADO";
            ddlEmpregado2.DataTextField = "tNO_EMPG";
            ddlEmpregado2.DataBind();
            ddlEmpregado2.Items.Insert(0, new ListItem("Selecione o Empregado...", "0"));

		}

        protected  void InicializaWebPageObjects()
        {

            //base.InicializaWebPageObjects();
            	
		}

		private void PopulaDropDownCIPA()
		{

            Ilitera.Data.PPRA_EPI xCIPA = new Ilitera.Data.PPRA_EPI();

            DataSet ds = xCIPA.Retornar_CIPAS(System.Convert.ToInt32(Session["Empresa"].ToString()));

			
			ddlCIPA.DataSource = ds;
			ddlCIPA.DataValueField= "IdCipa";
			ddlCIPA.DataTextField = "xComissaoEleitoral";
			ddlCIPA.DataBind();
			ddlCIPA.Items.Insert(0, new ListItem("Selecione a CIPA...","0"));
		}

		private void PopulaDropDownCA(string IdEPI)
		{
            //se estoque habilitado
			//ddlCA.DataSource = new CA().GetCACadDispoEstoque(lbl_Id_Empresa.Text, IdEPI);

            //ddlCA.DataSource = new CA().GetCAAssociadoEPI( System.Convert.ToInt32( lbl_Id_Empresa.Text), System.Convert.ToInt32( IdEPI));

			//ddlCA.DataValueField= "IdCA";
			//ddlCA.DataTextField = "NumeroCA";
			//ddlCA.DataBind();
			//ddlCA.Items.Insert(0, new ListItem("Selecione...","0"));
		}



	
		
		private void ResetHTMLControls()
		{
			ddlCIPA.SelectedItem.Selected = false;
			//int itemsca = ddlCA.Items.Count;
			//if (itemsca>1)
			//	for (int x=0; x<itemsca-1; x++)
			//		ddlCA.Items.RemoveAt(1);
			
			
			PopulaGridDefault();
			lblTotRegistros.Text = "";
			ddlEmpregado2.SelectedItem.Selected = false;
			
			lblError.Text = "";
		}

	
        protected void ddlCIPA_SelectedIndexChanged(object sender, System.EventArgs e)
        {

            txt_Data.ReadOnly = false;
            txt_Data.Text = "";
            txt_Data.ReadOnly = true;
            
            PopulaGridComissao();

            PopulaGridCandidatos();

            
            if (ddlCIPA.SelectedItem.ToString().IndexOf("*") >= 0  || ddlCIPA.SelectedValue.ToString().Trim() == "0" )
            {
                btnAdicionarMembroCandidato.Visible = false;
            }
            else
            {
                btnAdicionarMembroCandidato.Visible = true;
            }


            if ( ddlCIPA.SelectedValue.ToString().Trim() != "0" )
            {
                Cipa zCipa = new Cipa();
                zCipa.Find(System.Convert.ToInt32(ddlCIPA.SelectedValue.ToString()));

                txt_Data.ReadOnly = false;
                txt_Data.Text = zCipa.InicioInscricao.ToString("dd/MM/yyyy");
                if (txt_Data.Text == "01/01/0001")
                {
                    txt_Data.Text = "--/--/----";
                    btnImprimirFicha.Visible = false;
                    btnAlterarData.Visible = true;
                }
                else
                {
                    btnImprimirFicha.Visible = true;
                    btnAlterarData.Visible = false;
                }
                txt_Data.ReadOnly = true;

                txt_Eleicao.ReadOnly = false;
                txt_Eleicao.Text = zCipa.Eleicao.ToString("dd/MM/yyyy");
                if (txt_Eleicao.Text == "01/01/0001")
                {
                    txt_Eleicao.Text = "--/--/----";
                    btnImprimirFicha.Visible = false;
                    btnAlterarEleicao.Visible = true;
                }
                else
                {
                    if ( btnImprimirFicha.Visible == true )
                    {
                        btnImprimirFicha.Visible = true;
                    }
                    btnAlterarEleicao.Visible = false;
                }
                txt_Eleicao.ReadOnly = true;

            }


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

		protected void btnAdicionarMembro_Click(object sender, System.EventArgs e)
		{
			if (ddlEmpregado2.SelectedItem.Value != "0")
			{
				//PopulaGrid(DGridCandidatos, GeraDataSet("Adicionar"), lblTotRegistros);
        		lblError.Text = "";
			}
			else
				lblError.Text = "O membro deve ser selecionado!";
		}

		private DataSet GeraDataSet(string tipo)
		{
			DataSet dscache = new DataSet();
			DataSet ds = new DataSet();
			DataRow rowexiste;
			bool checkEPI = false;

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
				//if (rowdscachecheck["IdEPI"].ToString() == ddlCIPA.SelectedItem.Value && rowdscachecheck["IdCA"].ToString() == ddlCA.SelectedItem.Value)
				//{
					//checkEPI = true;
					//break;
				//}
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

			if (tipo == "Adicionar" && checkEPI == false)
			{
				DataRow rowatual;
				rowatual = ds.Tables[0].NewRow();
			
				EPIClienteCA epica = new EPIClienteCA();
				DataSet findepica = new EPIClienteCA().GetEPIClienteCAExistente(ddlCIPA.SelectedItem.Value, lbl_Id_Empresa.Text, ddlEmpregado2.SelectedItem.Value);

				rowatual["IdEPIClienteCA"] = findepica.Tables[0].Rows[0]["IdEPIClienteCA"];
				rowatual["IdEPI"] = ddlCIPA.SelectedItem.Value;
				//rowatual["IdCA"] = ddlCA.SelectedItem.Value;
				rowatual["Remover"] = "Remover";
				if (ddlCIPA.SelectedItem.Text.Length > 32)
                    rowatual["NomeEPI"] = ddlCIPA.SelectedItem.Text.Substring(0, 32) + "...";
				else
					rowatual["NomeEPI"] = ddlCIPA.SelectedItem.Text;
				//rowatual["NumeroCA"] = ddlCA.SelectedItem.Text;
                rowatual["QtdEntregue"] = 1;
				
				ds.Tables[0].Rows.Add(rowatual);
			}
			else if (tipo == "Adicionar" && checkEPI == true)
			{
				StringBuilder st = new StringBuilder();

				st.Append("window.alert(\"Não é possível adicionar este EPI! Ele já está adicionado!\");");

                this.ClientScript.RegisterStartupScript(this.GetType(), "VerificaEPIExistente", st.ToString(), true);
			}

            ViewState["ItemsEPI"] = ds;
			ViewState["IdCA"] = "";
			ViewState["IdEPI"] = "";

			return ds;
		}

		private void DGridCandidatos_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DGridCandidatos.CurrentPageIndex = e.NewPageIndex;
			//PopulaGrid(DGridCandidatos, GeraDataSet("ChangePage"), lblTotRegistros);
		}

		private void DGridCandidatos_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			if (e.CommandName == "RemoverEPI")
			{
				ViewState["IdCA"] = e.Item.Cells[2].Text;
				ViewState["IdEPI"] = e.Item.Cells[1].Text;
				//PopulaGrid(DGridCandidatos, GeraDataSet("Remove"), lblTotRegistros);

				if (lblTotRegistros.Text == "Nenhum Registro Encontrado")
				{
					lblTotRegistros.Text = "";
					DGridCandidatos.Visible = true;
				}
			}
		}

		private void DGridCandidatos_EditCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			DGridCandidatos.EditItemIndex = e.Item.ItemIndex;
			//PopulaGrid(DGridCandidatos, GeraDataSet("EditItem"), lblTotRegistros);
		}

		private void DGridCandidatos_CancelCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			DGridCandidatos.EditItemIndex = -1;
			//PopulaGrid(DGridCandidatos, GeraDataSet("CancelEditItem"), lblTotRegistros);
		}

		private void DGridCandidatos_UpdateCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			ViewState["IdCA"] = ((TextBox)e.Item.FindControl("txtIdCA")).Text;
			ViewState["IdEPI"] = ((TextBox)e.Item.FindControl("txtIdEPI")).Text;
			//IdCA = ((DropDownList)e.Item.FindControl("ddlCAEditar")).SelectedItem.Value;
			NumeroCA = ((DropDownList)e.Item.FindControl("ddlCAEditar")).SelectedItem.Text;
			QtdEntregue = ((TextBox)e.Item.FindControl("txtQtdEntregueEditar")).Text;
			DGridCandidatos.EditItemIndex = -1;
			//PopulaGrid(DGridCandidatos, GeraDataSet("AtualizarEPI"), lblTotRegistros);
		}


        protected void btnAdicionarMembroCandidato_Click(object sender, EventArgs e)
        {
            Cipa zCipa = new Cipa();
            zCipa.Find( System.Convert.ToInt32( ddlCIPA.SelectedValue.ToString() ) );


            ParticipantesEleicaoCipa candidato = new ParticipantesEleicaoCipa();
            candidato.Inicialize();
            candidato.IdCipa = zCipa;
            candidato.IdEmpregado.Id = Convert.ToInt32(ddlEmpregado2.SelectedValue.ToString());
            candidato.Save();

            PopulaGridCandidatos();
        }



        protected void btnImprimirFicha_Click(object sender, EventArgs e)
        {
            Guid strAux = Guid.NewGuid();

            OpenReport("DadosEmpresa", "RelatorioInscricaoCIPA.aspx?IliteraSystem=" + strAux.ToString() + strAux.ToString()
             + "&IdCipa=" + ddlCIPA.SelectedValue.ToString(), "RelatorioIncricaoCIPA", true);


            //Cipa zCipa = new Cipa();
            //zCipa.Find(System.Convert.ToInt32(ddlCIPA.SelectedValue.ToString()));
            
            //EventoCipa zEventoCipa = EventoCipa.GetEventoCipa(zCipa, EventoBase.Edital);
            
            //RptComprovanteDeInscricao report = new DataSourceCipa(zEventoCipa).GetReportComprovanteInscricao();

            //Funcionalidade funcionalidade = Funcionalidade.GetFuncionalidade(FuncionalidadeTipo.Relatorio, report.GetType());           

            //CreatePDFDocument(report, this.Response);

        }

        protected DataTable GetDropDownCandidatoExcluir(string idmembro)
        {

            DataTable dT = new DataTable();

            return dT;
        }

        protected void btnAlterarData_Click(object sender, EventArgs e)
        {

            if (btnAlterarData.Text == "Alterar Data")
            {

                btnAlterarData.Text = "Salvar";
                txt_Data.ReadOnly = false;
                txt_Data.Text = "";
                txt_Data.Focus();                   

            }
            else
            {
                btnAlterarData.Text = "Alterar Data";
                btnAlterarData.Visible = false;

                Cipa zCipa = new Cipa();
                zCipa.Find(System.Convert.ToInt32(ddlCIPA.SelectedValue.ToString()));

                zCipa.InicioInscricao = System.Convert.ToDateTime(txt_Data.Text);
                zCipa.Save();

                object xsender = new object(); 
                System.EventArgs xe = new System.EventArgs();

                ddlCIPA_SelectedIndexChanged(xsender, xe);
            }
        }

        protected void btnAlterarEleicao_Click(object sender, EventArgs e)
        {
            if (btnAlterarEleicao.Text == "Alterar Data")
            {

                btnAlterarEleicao.Text = "Salvar";
                txt_Eleicao.ReadOnly = false;
                txt_Eleicao.Text = "";
                txt_Eleicao.Focus();                                   
            }
            else
            {
                btnAlterarEleicao.Text = "Alterar Data";
                btnAlterarEleicao.Visible = false;

                Cipa zCipa = new Cipa();
                zCipa.Find(System.Convert.ToInt32(ddlCIPA.SelectedValue.ToString()));

                zCipa.Eleicao = System.Convert.ToDateTime(txt_Eleicao.Text);
                zCipa.Save();

                object xsender = new object(); 
                System.EventArgs xe = new System.EventArgs();

                ddlCIPA_SelectedIndexChanged(xsender, xe);
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





	}
}
