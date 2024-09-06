using System;
using System.Data;
using System.Web.SessionState;
using System.Web.UI.WebControls;
using Ilitera.Common;
using Ilitera.Opsa.Data;
using System.Text;
using System.Web;
using System.Collections;
using System.Web.UI;


namespace Ilitera.Net.Treinamentos
{
	/// <summary>
	/// 
	/// </summary>
	public partial class EmpregadoTreinamento : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label lblEmpresa;

        protected Prestador prestador = new Prestador();
        protected Usuario usuario = new Usuario();
        protected Cliente cliente = new Cliente();
        protected Empregado empregado = new Empregado();
		
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

				PopulaLsbEmpregado();
				PopulaLsbTreinamentos();
			}
		}

		private void PopulaLsbTreinamentos()
		{
			this.PopulaLsbTreinamentos(0, true);
		}
		
		private void PopulaLsbTreinamentos(int IdEmpregado, bool isDisponivel)
		{
			StringBuilder sqlstm = new StringBuilder();
			
			sqlstm.Append("IdTreinamentoDicionario");
			if (isDisponivel)
				sqlstm.Append(" NOT");
			sqlstm.Append(" IN (SELECT IdTreinamentoDicionario FROM TreinamentoDicionarioEmpregado WHERE IdEmpregado=" + IdEmpregado + ")");
			sqlstm.Append(" AND (IdCliente=310 OR IdCliente=" + Session["Empresa"] + ")");
			
			ArrayList alTreinamentos = new TreinamentoDicionario().Find(sqlstm.ToString());

			DataSet dsTreinamentos = new DataSet(), dsTreinamentosSorted = new DataSet();
			DataTable table = new DataTable(), tableSorted = new DataTable();
			
			table.Columns.Add("IdTreinamentoDicionario", typeof(string));
			table.Columns.Add("NomeTreinamento", typeof(string));
			dsTreinamentos.Tables.Add(table);

			tableSorted.Columns.Add("IdTreinamentoDicionario", typeof(string));
			tableSorted.Columns.Add("NomeTreinamento", typeof(string));
			dsTreinamentosSorted.Tables.Add(tableSorted);
			
			DataRow newRow, newRowSorted;

			foreach (TreinamentoDicionario treinamento in alTreinamentos)
			{
				newRow = dsTreinamentos.Tables[0].NewRow();
				newRow["IdTreinamentoDicionario"] = treinamento.Id.ToString();

				if (treinamento.IdCliente.Id.Equals(310))
				{
					treinamento.IdObrigacao.Find();

					newRow["NomeTreinamento"] = "*" + treinamento.IdObrigacao.NomeReduzido;
				}
				else
					newRow["NomeTreinamento"] = treinamento.Nome;
				dsTreinamentos.Tables[0].Rows.Add(newRow);
			}

			DataRow[] rows = dsTreinamentos.Tables[0].Select("", "NomeTreinamento");

			foreach (DataRow row in rows)
			{
				newRowSorted = dsTreinamentosSorted.Tables[0].NewRow();
				newRowSorted["IdTreinamentoDicionario"] = row["IdTreinamentoDicionario"];
				newRowSorted["NomeTreinamento"] = row["NomeTreinamento"];
				dsTreinamentosSorted.Tables[0].Rows.Add(newRowSorted);
			}

			if (isDisponivel)
			{
				lsbTreinamentos.DataSource = dsTreinamentosSorted;
				lsbTreinamentos.DataValueField = "IdTreinamentoDicionario";
				lsbTreinamentos.DataTextField = "NomeTreinamento";
				lsbTreinamentos.DataBind();
			}
			else
			{
				lsbSelecionados.DataSource = dsTreinamentosSorted;
				lsbSelecionados.DataValueField = "IdTreinamentoDicionario";
				lsbSelecionados.DataTextField = "NomeTreinamento";
				lsbSelecionados.DataBind();
			}
		}
		
		private void PopulaLsbEmpregado()
		{
			DataSet dsEmpregado = new Empregado().Get("nID_EMPR=" + Session["Empresa"]
				+" AND gTERCEIRO=0 AND hDT_DEM IS NULL ORDER BY tNO_EMPG");

			lsbEmpregados.DataSource = dsEmpregado;
			lsbEmpregados.DataTextField = "tNO_EMPG";
			lsbEmpregados.DataValueField = "nID_EMPREGADO";
			lsbEmpregados.DataBind();
		}
		
		private DataSet GetDSTreinamentos()
		{
			ArrayList alTreinamentos = new Treinamento().Find("IdTreinamento IN (SELECT IdTreinamento FROM ParticipanteTreinamento WHERE IdEmpregado=" + lsbEmpregados.SelectedValue + ")"
				+" ORDER BY DataLevantamento DESC");

			DataSet ds = new DataSet();
			DataTable table = new DataTable();
			DataRow row;

			table.Columns.Add("Id", Type.GetType("System.String"));
			table.Columns.Add("Data", Type.GetType("System.String"));
			table.Columns.Add("NomeTreinamento", Type.GetType("System.String"));
			ds.Tables.Add(table);
			
			foreach (Treinamento treinamento in alTreinamentos)
			{
				treinamento.IdTreinamentoDicionario.Find();
				row = ds.Tables[0].NewRow();

				row["Id"] = treinamento.Id.ToString();
				row["Data"] = treinamento.DataLevantamento.ToString("dd-MM-yyyy");

				if (treinamento.IsFromCliente)
					row["NomeTreinamento"] = treinamento.IdTreinamentoDicionario.Nome;
				else
				{
					treinamento.IdTreinamentoDicionario.IdObrigacao.Find();
					
					row["NomeTreinamento"] = treinamento.IdTreinamentoDicionario.IdObrigacao.NomeReduzido;
				}

				if (row["NomeTreinamento"].ToString().Length > 39)
					row["NomeTreinamento"] = row["NomeTreinamento"].ToString().Substring(0, 36) + "...";

				ds.Tables[0].Rows.Add(row);
			}

			if (ds.Tables[0].Rows.Count.Equals(0))
				lblTotRegistros.Text = "Nenhum registro encontrado";
			else
				lblTotRegistros.Text = "Total de Registros: <b>" + ds.Tables[0].Rows.Count.ToString() + "</b>";

			return ds;
		}

        protected void lsbEmpregados_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            PopulaLsbTreinamentos(Convert.ToInt32(lsbEmpregados.SelectedValue), true);
            PopulaLsbTreinamentos(Convert.ToInt32(lsbEmpregados.SelectedValue), false);

            UltraWebGridTreinamentos.DataSource = GetDSTreinamentos();
            UltraWebGridTreinamentos.DataBind();
        }

		protected void ImbAdiciona_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			bool ProcessEntity = true;

			if (lsbEmpregados.SelectedItem != null)
			{
				foreach (ListItem treinamento in lsbTreinamentos.Items)
					if (treinamento.Selected)
					{
						TreinamentoDicionarioEmpregado treinamentoEmpregado = new TreinamentoDicionarioEmpregado();
						treinamentoEmpregado.Inicialize();

						treinamentoEmpregado.IdEmpregado.Id = Convert.ToInt32(lsbEmpregados.SelectedValue);
						treinamentoEmpregado.IdTreinamentoDicionario.Id = Convert.ToInt32(treinamento.Value);
						treinamentoEmpregado.UsuarioId = usuario.Id;
						
						if (ProcessEntity)
						{
							ProcessEntity = false;
							treinamentoEmpregado.UsuarioProcessoRealizado = "Configuração de Treinamentos ao Empregado " + lsbEmpregados.SelectedItem.Text;
							treinamentoEmpregado.Save();
						}
						else
							treinamentoEmpregado.Save();
					}

				if (!ProcessEntity)
				{
					PopulaLsbTreinamentos(Convert.ToInt32(lsbEmpregados.SelectedValue), true);
					PopulaLsbTreinamentos(Convert.ToInt32(lsbEmpregados.SelectedValue), false);
				}
			}
			else                
                MsgBox1.Show("Ilitera.Net", "É necessário selecionar o empregado antes de adicionar os Treinamentos!", null,
                              new EO.Web.MsgBoxButton("OK"));

		}

		protected void ImbAdicionaTodos_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			bool ProcessEntity = true;

			if (lsbEmpregados.SelectedItem != null)
			{
				foreach (ListItem treinamento in lsbTreinamentos.Items)
				{
					TreinamentoDicionarioEmpregado treinamentoEmpregado = new TreinamentoDicionarioEmpregado();
					treinamentoEmpregado.Inicialize();

					treinamentoEmpregado.IdEmpregado.Id = Convert.ToInt32(lsbEmpregados.SelectedValue);
					treinamentoEmpregado.IdTreinamentoDicionario.Id = Convert.ToInt32(treinamento.Value);
					treinamentoEmpregado.UsuarioId = usuario.Id;
					
					if (ProcessEntity)
					{
						ProcessEntity = false;
						treinamentoEmpregado.UsuarioProcessoRealizado = "Configuração de Treinamentos ao Empregado " + lsbEmpregados.SelectedItem.Text;
						treinamentoEmpregado.Save();
					}
					else
						treinamentoEmpregado.Save();
				}

				if (!ProcessEntity)
				{
					PopulaLsbTreinamentos(Convert.ToInt32(lsbEmpregados.SelectedValue), true);
					PopulaLsbTreinamentos(Convert.ToInt32(lsbEmpregados.SelectedValue), false);
				}
			}
			else
                MsgBox1.Show("Ilitera.Net", "É necessário selecionar o empregado antes de adicionar os Treinamentos!", null,
                             new EO.Web.MsgBoxButton("OK"));

		}

		protected void ImgRemove_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			bool ProcessEntity = true;

			if (lsbEmpregados.SelectedItem != null)
			{
				foreach (ListItem treinamento in lsbSelecionados.Items)
					if (treinamento.Selected)
					{
						TreinamentoDicionarioEmpregado treinamentoEmpregado = new TreinamentoDicionarioEmpregado();
						treinamentoEmpregado.Find("IdTreinamentoDicionario=" + treinamento.Value
							+" AND IdEmpregado=" + lsbEmpregados.SelectedValue);
						treinamentoEmpregado.UsuarioId = usuario.Id;
						
						if (ProcessEntity)
						{
							ProcessEntity = false;
							treinamentoEmpregado.UsuarioProcessoRealizado = "Configuração de Treinamentos ao Empregado " + lsbEmpregados.SelectedItem.Text;
							treinamentoEmpregado.Delete();
						}
						else
							treinamentoEmpregado.Delete();
					}

				if (!ProcessEntity)
				{
					PopulaLsbTreinamentos(Convert.ToInt32(lsbEmpregados.SelectedValue), true);
					PopulaLsbTreinamentos(Convert.ToInt32(lsbEmpregados.SelectedValue), false);
				}
			}
			else
                MsgBox1.Show("Ilitera.Net", "É necessário selecionar o empregado antes de remover os Treinamentos!", null,
                           new EO.Web.MsgBoxButton("OK"));

		}

		protected void ImgRemoveTodos_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			bool ProcessEntity = true;

			if (lsbEmpregados.SelectedItem != null)
			{
				foreach (ListItem treinamento in lsbSelecionados.Items)
				{
					TreinamentoDicionarioEmpregado treinamentoEmpregado = new TreinamentoDicionarioEmpregado();
					treinamentoEmpregado.Find("IdTreinamentoDicionario=" + treinamento.Value
						+" AND IdEmpregado=" + lsbEmpregados.SelectedValue);
					treinamentoEmpregado.UsuarioId = usuario.Id;
					
					if (ProcessEntity)
					{
						ProcessEntity = false;
						treinamentoEmpregado.UsuarioProcessoRealizado = "Configuração de Treinamentos ao Empregado " + lsbEmpregados.SelectedItem.Text;
						treinamentoEmpregado.Delete();
					}
					else
						treinamentoEmpregado.Delete();
				}

				if (!ProcessEntity)
				{
					PopulaLsbTreinamentos(Convert.ToInt32(lsbEmpregados.SelectedValue), true);
					PopulaLsbTreinamentos(Convert.ToInt32(lsbEmpregados.SelectedValue), false);
				}
			}
			else
                MsgBox1.Show("Ilitera.Net", "É necessário selecionar o empregado antes de remover os Treinamentos!", null,
                         new EO.Web.MsgBoxButton("OK"));

		}

        //protected void UltraWebGridTreinamentos_PageIndexChanged(object sender, Infragistics.WebUI.UltraWebGrid.PageEventArgs e)
        //{
        //    UltraWebGridTreinamentos.DataSource = GetDSTreinamentos();
        //    UltraWebGridTreinamentos.DisplayLayout.Pager.CurrentPageIndex = e.NewPageIndex;
        //    UltraWebGridTreinamentos.DataBind();
        //}

        //private void UltraWebGridTreinamentos_ClickCellButton(object sender, Infragistics.WebUI.UltraWebGrid.CellEventArgs e)
        //{
        //    if (e.Cell.Key.Equals("NomeTreinamento"))
        //    {
        //        StringBuilder st = new StringBuilder();

        //        st.Append("void(addItem(centerWin('ListaEmpregadoCurso.aspx?IdTreinamento=" + e.Cell.Row.Cells[0].Text + "&IdUsuario=" + Session["Usuario"] + "&IdEmpresa=" + Session["Empresa"] + "', 400, 315,\'ListaTreinamentoParticipantes\'),\'Todos\'))");

        //        this.ClientScript.RegisterStartupScript(this.GetType(), "ListaTreinamentoParticipantes", st.ToString(), true);
        //    }
        //}

		protected void lkbListagemTodos_Click(object sender, System.EventArgs e)
		{
            DataSet dsTreinamentoEmpregados = new TreinamentoDicionarioEmpregado().Get("IdEmpregado IN (SELECT nID_EMPREGADO FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEMPREGADO WHERE nID_EMPR=" + Session["Empresa"]
				+" AND gTERCEIRO=0 AND hDT_DEM IS NULL)");
			
			if (dsTreinamentoEmpregados.Tables[0].Rows.Count > 0)
			{
				Guid strAux = Guid.NewGuid();

				OpenReport("Treinamentos", "ConfiguracaoEmpregado.aspx?IliteraSystem=" + strAux.ToString() + strAux.ToString() + "&IdUsuario=" + usuario.Id
					+ "&IdEmpresa=" + cliente.Id, "ConfiguracaoEmpregado");
			}
			else
                MsgBox1.Show("Ilitera.Net", "Não é possível visualizar a configuração de treinamentos para os empregados! Não há nenhum treinamento configurado para os empregados da empresa " + cliente.NomeCompleto + "!", null,
                         new EO.Web.MsgBoxButton("OK"));

		}

		protected void lkbFichaTreinamento_Click(object sender, System.EventArgs e)
		{
			if (lsbEmpregados.SelectedItem != null)
			{
				Guid strAux = Guid.NewGuid();

				OpenReport("Treinamentos", "FichaEmpregado.aspx?IliteraSystem=" + strAux.ToString() + strAux.ToString() + "&IdUsuario=" + usuario.Id + "&IdEmpresa=" + cliente.Id
					+ "&IdEmpregado=" + lsbEmpregados.SelectedValue, "FichaEmpregado");
			}
			else
                MsgBox1.Show("Ilitera.Net", "É necessário selecionar o empregado antes de visualizar a sua Ficha de Treinamentos!", null,
                         new EO.Web.MsgBoxButton("OK"));

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



        protected void UltraWebGridTreinamentos_ItemCommand(object sender, EO.Web.GridCommandEventArgs e)
        {

            if (e.CommandName == "2")   //add
            {

            }
            else if (e.CommandName == "3")   //print
            {

            }

            //Session["Empregado"] = e.Item.Key.ToString();
            //Session["NomeEmpregado"] = e.Item.Cells[0].Value.ToString();


        }

	}
}
