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
	public partial class ListaTreinamentos : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label lblEmpresa;

        protected Prestador prestador = new Prestador();
        protected Usuario usuario = new Usuario();
        protected Cliente cliente = new Cliente();
        protected Empregado empregado = new Empregado();
	
		
		protected void Page_Load(object sender, System.EventArgs e)
		{
			//InicializaWebPageObjects();
			if (!Page.IsPostBack)
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
				UltraWebGridListaTreinamentos.DataSource = GetTreinamentos();
				UltraWebGridListaTreinamentos.DataBind();
			}
			else if (txtAuxiliar.Value.Equals("atualiza"))
			{
				txtAuxiliar.Value = string.Empty;
				UltraWebGridListaTreinamentos.DataSource = GetTreinamentos();
				UltraWebGridListaTreinamentos.DataBind();
			}
		}


        private DataSet GetTreinamentos()
		{
            DataSet ds = new DataSet();
            DataTable table = new DataTable("Default");
            DataRow row;
            ArrayList alTreinamentos;

            if (Session["Empresa"] != null && Session["Empresa"].ToString() != String.Empty)
            {
                alTreinamentos = new Treinamento().Find("IdCliente=" + Session["Empresa"] + " ORDER BY DataLevantamento DESC");
            }
            else
            {
                return ds; 
            }

			table.Columns.Add("Id", Type.GetType("System.String"));
			table.Columns.Add("Periodo", Type.GetType("System.String"));
			table.Columns.Add("NomeTreinamento", Type.GetType("System.String"));
			table.Columns.Add("EditTreinamento", Type.GetType("System.String"));
            table.Columns.Add("Certificado", Type.GetType("System.String"));
			ds.Tables.Add(table);
			
			foreach (Treinamento treinamento in alTreinamentos)
			{
				treinamento.IdTreinamentoDicionario.Find();
				row = ds.Tables[0].NewRow();

				row["Id"] = treinamento.Id.ToString();
				row["Periodo"] = treinamento.DataLevantamento.ToString("dd-MM-yyyy");

				//if (treinamento.IsFromCliente)
				//{
					row["NomeTreinamento"] = treinamento.IdTreinamentoDicionario.Nome;
					//row["EditTreinamento"] = @"<a href=""#"" onClick=""addItem(centerWin('CadastroCurso.aspx?IdEmpresa=" + Session["Empresa"] + @"&IdUsuario=" + Session["Usuario"] + @"&IdTreinamento=" + treinamento.Id + @"', 590, 450, 'CadastroCurso'), 'Todos');""><img src='img/edita.gif' border=0 alt='Editar o cadastro do Treinamento'></a>";

                    //row["Certificado"] = @"<a href=""#"" onClick=""addItem(centerWin('ListaEmpregadoCurso.aspx?IdEmpresa=" + Session["Empresa"] + @"&IdUsuario=" + Session["Usuario"] + @"&IdTreinamento=" + treinamento.Id + @"', 590, 450, 'CadastroCurso'), 'Todos');""><img src='img/print.gif' border=0 alt='Certificado'></a>";
				//}
				//else
				//{
					treinamento.IdTreinamentoDicionario.IdObrigacao.Find();
					
					row["NomeTreinamento"] = treinamento.IdTreinamentoDicionario.IdObrigacao.NomeReduzido;
				//}

				ds.Tables[0].Rows.Add(row);
			}

			if (ds.Tables[0].Rows.Count.Equals(0))
			{
				btnNovoTreinamento.Visible = false;
				lkbListagemCurso.Visible = false;
				lblTotRegistros.Text = string.Empty;
				lblNomeEmpresa.Text = "Não há nenhum Treinamento realizado para a " + cliente.NomeCompleto;
			}
			else
			{
                btnNovoTreinamento.Visible = false;
				//btnNovoTreinamento.Visible = true;
				lkbListagemCurso.Visible = true;
				lblTotRegistros.Text = "Total de Registros: <b>" + ds.Tables[0].Rows.Count.ToString() + "</b>";
				lblNomeEmpresa.Text = string.Empty;
			}

			return ds;
		}




        //protected void UltraWebGridListaTreinamentos_PageIndexChanged(object sender, Infragistics.WebUI.UltraWebGrid.PageEventArgs e)
        //{
        //    UltraWebGridListaTreinamentos.DataSource = GetTreinamentos();
        //    UltraWebGridListaTreinamentos.DataBind();

        //    //if (UltraWebGridListaTreinamentos.DisplayLayout.Pager.PageCount < e.NewPageIndex)
        //    //    UltraWebGridListaTreinamentos.DisplayLayout.Pager.CurrentPageIndex = UltraWebGridListaTreinamentos.DisplayLayout.Pager.PageCount;
        //    //else
        //    //    UltraWebGridListaTreinamentos.DisplayLayout.Pager.CurrentPageIndex = e.NewPageIndex;
				
        //    UltraWebGridListaTreinamentos.DataBind();
        //}

		protected void lkbListagemCurso_Click(object sender, System.EventArgs e)
		{
			Guid strAux = Guid.NewGuid();

			OpenReport("Treinamentos", "CursoEmpresa.aspx?IliteraSystem=" + strAux.ToString() + strAux.ToString()
				+ "&IdEmpresa=" + cliente.Id + "&IdUsuario=" + usuario.Id, "CursoEmpresa");
		}

		protected void btnNovoTreinamento_Click(object sender, System.EventArgs e)
		{
			DataSet dsTreinamentoDicionario = new TreinamentoDicionario().Get("IdCliente=" + Session["Empresa"]);
			StringBuilder st = new StringBuilder();

            Entities.Usuario xUser = (Entities.Usuario)Session["usuarioLogado"];

			if (dsTreinamentoDicionario.Tables[0].Rows.Count > 0)
                st.Append("void(addItem(centerWin('CadastroCurso.aspx?IdUsuario=" + xUser.IdUsuario.ToString() + "&IdEmpresa=" + Session["Empresa"] + "', 570, 420,\'CadastroCurso\'),\'Todos\'))");
			else
				st.Append("window.alert('Não é possível cadastrar um novo curso! É necessário Cadastrar e Configurar o Layout de pelo menos 1 dos treinamentos que deseja cadastrar!');");

            this.ClientScript.RegisterStartupScript(this.GetType(), "CadastroCurso", st.ToString(), true);
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



        protected void UltraWebGridListaTreinamentos_ItemCommand(object sender, EO.Web.GridCommandEventArgs e)
        {
            StringBuilder st = new StringBuilder();

            Treinamento xTreinamento = new Treinamento();

            xTreinamento.Find(System.Convert.ToInt32(e.Item.Key.ToString()));

            if ( e.CommandName == "2" )   //add
            {                
                st.AppendFormat("void(window.open('CadastroCurso.aspx?IdEmpresa=" + Session["Empresa"] + @"&IdUsuario=" + Session["Usuario"] + @"&IdTreinamento=" + xTreinamento.Id + @"', 'CursoEmpresa','scrollbars=yes, resizable=yes, toolbar=no, addressbar=no, menubar=no, width=600px, height=500px'));");
                    
                ScriptManager.RegisterStartupScript(this, this.GetType(), " ", st.ToString(), true);

            }
            else if ( e.CommandName == "3" )   //print
            {
                                
                st.AppendFormat("void(window.open('ListaEmpregadoCurso.aspx?IdEmpresa=" + Session["Empresa"] + @"&IdUsuario=" + Session["Usuario"] + @"&IdTreinamento=" + xTreinamento.Id + @"', 'CursoEmpresa','scrollbars=yes, resizable=yes, toolbar=no, addressbar=no, menubar=no, width=600px, height=500px'));");

                ScriptManager.RegisterStartupScript(this, this.GetType(), " ", st.ToString(), true);
            }

            //Session["Empregado"] = e.Item.Key.ToString();
            //Session["NomeEmpregado"] = e.Item.Cells[0].Value.ToString();
            
        }
      

	}

}
