using System;
using System.Collections;
using System.Data;
using System.Web.SessionState;
using System.Web.UI.WebControls;
using Ilitera.Opsa.Data;
using System.Text;
using System.Web;
using System.Web.UI;

namespace Ilitera.Net
{
	/// <summary>
	/// Summary description for CadConjuntoProcedimento.
	/// </summary>
	public partial class CadConjuntoProcedimento : System.Web.UI.Page
	{

        protected Ilitera.Common.Usuario usuario = new Ilitera.Common.Usuario();
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

                //hlkNovo.NavigateUrl = "javascript:void(addItem(centerWin('CadConjunto.aspx?IdEmpresa=" + lbl_Id_Empresa.Text + "&amp;IdUsuario=" + lbl_Id_Usuario.Text + "',350,225,'ConjuntoProcedimento'),\'Todos\'))";
                //hlkNovo.NavigateUrl = "javascript:void(centerWin('CadConjunto.aspx?IdEmpresa=" + lbl_Id_Empresa.Text + "&amp;IdUsuario=" + lbl_Id_Usuario.Text + "',350,225,'ConjuntoProcedimento'))";								

                PopulaDdls();

            }
            else if (txtAuxiliar.Value.Equals("atualizaConjuntos"))
            {
                string valueDDLConjunto = ddlConjunto.SelectedValue;

                txtAuxiliar.Value = string.Empty;
                PopulaDdlConjuntos();

                if (ddlConjunto.Items.FindByValue(valueDDLConjunto) != null)
                    ddlConjunto.Items.FindByValue(valueDDLConjunto).Selected = true;
                else
                {
                    PopulaListConjuntoProcedimento();
                    PopulaListProcedimentos();
                }
            }
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//

            if (!this.DesignMode)
            {
                InitializeComponent();
            }
			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.imgBusca.Click += new System.Web.UI.ImageClickEventHandler(this.imgBusca_Click);
			this.imbAddProcedimento.Click += new System.Web.UI.ImageClickEventHandler(this.imbAddProcedimento_Click);
			this.imbAddAllProcedimento.Click += new System.Web.UI.ImageClickEventHandler(this.imbAddAllProcedimento_Click);
			this.imbRemoveProcedimento.Click += new System.Web.UI.ImageClickEventHandler(this.imbRemoveProcedimento_Click);
			this.imbRemoveAllProcedimento.Click += new System.Web.UI.ImageClickEventHandler(this.imbRemoveAllProcedimento_Click);
			this.imbConjuntoProcedimentos.Click += new System.Web.UI.ImageClickEventHandler(this.imbConjuntoProcedimentos_Click);

		}
		#endregion

		private void PopulaDdls()
		{
			PopulaDdlSetor();
			PopulaDdlFerramenta();
			PopulaDdlEquipamento();
			PopulaDdlProduto();
			PopulaDdlCelula();
			PopulaDdlTipoProcedimento();
			PopulaDdlConjuntos();
			PopulaListProcedimentos();
			PopulaListConjuntoProcedimento();
		}

		public void PopulaDdlTipoProcedimento()
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

		private void PopulaDdlConjuntos()
		{
			ddlConjunto.DataSource = new Conjunto().Get("IdCliente=" + lbl_Id_Empresa.Text + " ORDER BY Nome");
			ddlConjunto.DataTextField = "Nome";
			ddlConjunto.DataValueField = "IdConjunto";
			ddlConjunto.DataBind();
		}

		private DataSet GetDataSourceProcedimentos()
		{
			StringBuilder st = new StringBuilder();

			st.Append("IdCliente=" + lbl_Id_Empresa.Text);
			
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

			if (ddlConjunto.SelectedItem != null)
                st.Append(" AND IdProcedimento NOT IN (SELECT IdProcedimento FROM ConjuntoProcedimento WHERE IdConjunto=" + ddlConjunto.SelectedValue + ")");
			
			st.Append(" ORDER BY Numero");
			
			DataSet dsProcedimento = new Procedimento().Get(st.ToString());

			lblTotProcedimentos.Text = dsProcedimento.Tables[0].Rows.Count.ToString();

			if (dsProcedimento.Tables[0].Rows.Count > 0 && ddlConjunto.SelectedItem != null)
			{
				imbAddProcedimento.Enabled = true;
				imbAddProcedimento.ImageUrl = "InfragisticsImg/XpOlive/next_down.gif";
				imbAddAllProcedimento.Enabled = true;
				imbAddAllProcedimento.ImageUrl = "InfragisticsImg/XpOlive/ff_down.gif";
			}
			else
			{
				imbAddProcedimento.ImageUrl = "InfragisticsImg/XpOlive/next_disabled.gif";
				imbAddProcedimento.Enabled = false;
				imbAddAllProcedimento.ImageUrl = "InfragisticsImg/XpOlive/ff_disabled.gif";
				imbAddAllProcedimento.Enabled = false;
			}

			return dsProcedimento;
		}

		private void PopulaListProcedimentos()
		{	
			listBxProcedimento.Items.Clear();

			foreach (DataRow rowProcedimento in GetDataSourceProcedimentos().Tables[0].Select())
				listBxProcedimento.Items.Add(new ListItem(((short)rowProcedimento["Numero"]).ToString("0000") + " - " + rowProcedimento["Nome"], rowProcedimento["IdProcedimento"].ToString()));
		}

		private void PopulaListConjuntoProcedimento()
		{	
			if(ddlConjunto.SelectedItem != null)
			{		
				listBxConjuntoProcedimento.Items.Clear();

				DataSet dsProcedimento = new Procedimento().Get("IdProcedimento IN (SELECT IdProcedimento FROM ConjuntoProcedimento WHERE IdConjunto=" + ddlConjunto.SelectedValue + ")"
					+" ORDER BY Numero");
				
				foreach (DataRow rowProcedimento in dsProcedimento.Tables[0].Select())
					listBxConjuntoProcedimento.Items.Add(new ListItem(((short)rowProcedimento["Numero"]).ToString("0000") + " - " + rowProcedimento["Nome"], rowProcedimento["IdProcedimento"].ToString()));
				
				lblTotConjProcedimento.Text = dsProcedimento.Tables[0].Rows.Count.ToString();

				if (dsProcedimento.Tables[0].Rows.Count > 0)
				{
					imbRemoveProcedimento.Enabled = true;
					imbRemoveProcedimento.ImageUrl = "InfragisticsImg/XpOlive/prev_down.gif";
					imbRemoveAllProcedimento.Enabled = true;
					imbRemoveAllProcedimento.ImageUrl = "InfragisticsImg/XpOlive/rew_down.gif";
				}
				else
				{
					imbRemoveProcedimento.ImageUrl = "InfragisticsImg/XpOlive/prev_disabled.gif";
					imbRemoveProcedimento.Enabled = false;
					imbRemoveAllProcedimento.ImageUrl = "InfragisticsImg/XpOlive/rew_disabled.gif";
					imbRemoveAllProcedimento.Enabled = false;
				}
			}
			else
			{
				imbRemoveProcedimento.ImageUrl = "InfragisticsImg/XpOlive/prev_disabled.gif";
				imbRemoveProcedimento.Enabled = false;
				imbRemoveAllProcedimento.ImageUrl = "InfragisticsImg/XpOlive/rew_disabled.gif";
				imbRemoveAllProcedimento.Enabled = false;
			}
		}

		protected void ddlConjunto_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			PopulaListConjuntoProcedimento();
			PopulaListProcedimentos();
		}

		private void imgBusca_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			PopulaListProcedimentos();
		}

		protected void lkbListaTodos_Click(object sender, System.EventArgs e)
		{
			txtProcedimento.Text = string.Empty;
			ddlTipoProcedimento.ClearSelection();
			ddlSetor.ClearSelection();
			ddlCelula.ClearSelection();
			ddlEquipamento.ClearSelection();
			ddlProduto.ClearSelection();
			ddlFerramenta.ClearSelection();

			PopulaListProcedimentos();
		}

		private void imbAddProcedimento_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			try
			{
				if(listBxProcedimento.SelectedItem == null)
					throw new Exception("É necessário selecionar pelo menos um Procedimento para adicionar ao Conjunto!");

				bool checkProcess = false;
				
				foreach (ListItem itemProcedimento in listBxProcedimento.Items)
					if (itemProcedimento.Selected)
					{
						ConjuntoProcedimento conjuntoProcedimento = new ConjuntoProcedimento();
						conjuntoProcedimento.Inicialize();

						conjuntoProcedimento.IdConjunto.Id = Convert.ToInt32(ddlConjunto.SelectedValue);
						conjuntoProcedimento.IdProcedimento.Id = Convert.ToInt32(itemProcedimento.Value);

						if (!checkProcess)
						{
							checkProcess = true;
							conjuntoProcedimento.UsuarioProcessoRealizado = "Cadastro de Procedimentos à Conjuntos de Procedimentos";
						}

						conjuntoProcedimento.UsuarioId = usuario.Id;
						conjuntoProcedimento.Save();
					}
				
				PopulaListConjuntoProcedimento();
				PopulaListProcedimentos();


                MsgBox1.Show("Ilitera.Net", "Os Procedimentos selecionados foram adicionados com sucesso ao Conjunto " + ddlConjunto.SelectedItem.Text + "!", null,
                       new EO.Web.MsgBoxButton("OK"));

			}
			catch(Exception ex)
			{
                MsgBox1.Show("Ilitera.Net", ex.Message, null,
                       new EO.Web.MsgBoxButton("OK"));

			}
		}

		private void imbAddAllProcedimento_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			try
			{
				bool checkProcess = false;
				
				foreach (ListItem itemProcedimento in listBxProcedimento.Items)
				{
					ConjuntoProcedimento conjuntoProcedimento = new ConjuntoProcedimento();
					conjuntoProcedimento.Inicialize();

					conjuntoProcedimento.IdConjunto.Id = Convert.ToInt32(ddlConjunto.SelectedValue);
					conjuntoProcedimento.IdProcedimento.Id = Convert.ToInt32(itemProcedimento.Value);

					if (!checkProcess)
					{
						checkProcess = true;
						conjuntoProcedimento.UsuarioProcessoRealizado = "Cadastro de todos os Procedimentos ao Conjunto " + ddlConjunto.SelectedItem.Text;
					}

                    conjuntoProcedimento.UsuarioId = System.Convert.ToInt32( lbl_Id_Usuario.Text);                     //usuario.Id;
					conjuntoProcedimento.Save();
				}
				
				PopulaListConjuntoProcedimento();
				PopulaListProcedimentos();


                MsgBox1.Show("Ilitera.Net", "Todos os Procedimentos foram adicionados com sucesso ao Conjunto " + ddlConjunto.SelectedItem.Text + "!", null,
                       new EO.Web.MsgBoxButton("OK"));

			}
			catch(Exception ex)
			{
                MsgBox1.Show("Ilitera.Net", ex.Message, null,
                        new EO.Web.MsgBoxButton("OK"));

			}
		}

		private void imbRemoveProcedimento_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			try
			{
				if(listBxConjuntoProcedimento.SelectedItem == null)
					throw new Exception("É necessário selecionar pelo menos um Procedimento para ser removido do Conjunto!");

				bool checkProcess = false;
				
				foreach (ListItem itemProcedimento in listBxConjuntoProcedimento.Items)
					if (itemProcedimento.Selected)
					{
						ConjuntoProcedimento conjuntoProcedimento = new ConjuntoProcedimento();
						conjuntoProcedimento.Find("IdConjunto=" + ddlConjunto.SelectedValue + " AND IdProcedimento=" + itemProcedimento.Value);

						if (!checkProcess)
						{
							checkProcess = true;
							conjuntoProcedimento.UsuarioProcessoRealizado = "Exclusão de Procedimentos do Conjunto " + ddlConjunto.SelectedItem.Text;
						}

                        conjuntoProcedimento.UsuarioId = System.Convert.ToInt32(lbl_Id_Usuario.Text); 
						conjuntoProcedimento.Delete();
					}
				
				PopulaListConjuntoProcedimento();
				PopulaListProcedimentos();


                MsgBox1.Show("Ilitera.Net", "Os Procedimentos selecionados foram removidos com sucesso do Conjunto " + ddlConjunto.SelectedItem.Text + "!", null,
                       new EO.Web.MsgBoxButton("OK"));

			}
			catch(Exception ex)
			{
                MsgBox1.Show("Ilitera.Net", ex.Message, null,
                       new EO.Web.MsgBoxButton("OK"));

			}
		}

		private void imbRemoveAllProcedimento_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			try
			{
				bool checkProcess = false;
				
				foreach (ListItem itemProcedimento in listBxConjuntoProcedimento.Items)
				{
					ConjuntoProcedimento conjuntoProcedimento = new ConjuntoProcedimento();
					conjuntoProcedimento.Find("IdConjunto=" + ddlConjunto.SelectedValue + " AND IdProcedimento=" + itemProcedimento.Value);

					if (!checkProcess)
					{
						checkProcess = true;
						conjuntoProcedimento.UsuarioProcessoRealizado = "Exclusão de todos os Procedimentos do Conjunto " + ddlConjunto.SelectedItem.Text;
					}

                    conjuntoProcedimento.UsuarioId = System.Convert.ToInt32(lbl_Id_Usuario.Text); //usuario.Id;
					conjuntoProcedimento.Delete();
				}
				
				PopulaListConjuntoProcedimento();
				PopulaListProcedimentos();


                MsgBox1.Show("Ilitera.Net", "Todos os Procedimentos foram removidos com sucesso do Conjunto " + ddlConjunto.SelectedItem.Text + "!", null,
                       new EO.Web.MsgBoxButton("OK"));

			}
			catch(Exception ex)
			{
                MsgBox1.Show("Ilitera.Net", ex.Message, null,
                       new EO.Web.MsgBoxButton("OK"));

			}
		}

		private void imbConjuntoProcedimentos_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Guid strAux = Guid.NewGuid();

			OpenReport("OrdemDeServico", "RptConjuntoProcedimento.aspx?IliteraSystem=" + strAux.ToString() + strAux.ToString()
				+ "&IdEmpresa=" + lbl_Id_Empresa.Text + "&IdUsuario=" + lbl_Id_Usuario.Text, "RptConjuntoProcedimento");
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

        protected void imbAddProcedimento_Click1(object sender, ImageClickEventArgs e)
        {

        }

        protected void imbAddAllProcedimento_Click1(object sender, ImageClickEventArgs e)
        {

        }

        protected void imbRemoveProcedimento_Click1(object sender, ImageClickEventArgs e)
        {

        }

        protected void imbRemoveAllProcedimento_Click1(object sender, ImageClickEventArgs e)
        {

        }

        protected void imgBusca_Click1(object sender, ImageClickEventArgs e)
        {

        }



     
	}
}
