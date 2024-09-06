using System;
using System.Data;
using System.Web.SessionState;
using System.Web.UI.WebControls;
using Ilitera.Common;
using Ilitera.Opsa.Data;
using System.Text;
using System.Web.UI;

using System.Web;
using System.Collections;

namespace Ilitera.Net.ControleCIPA
{
    public partial class ListaReunioesCIPA : System.Web.UI.Page
    {

        protected Prestador prestador = new Prestador();
        protected Usuario usuario = new Usuario();
        protected Cliente cliente = new Cliente();
        protected Empregado empregado = new Empregado();

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

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


                //está dando erro - requests retornam NULL

                Entities.Usuario user = (Entities.Usuario)Session["usuarioLogado"];

                //GetMenu(((int)IndMenuType.Empresa).ToString(), user.IdUsuario.ToString(), Session["Empresa"].ToString());
				//PreencheLabels("lblEmp", cliente.NomeAbreviado.ToString());
				PopulaDDLCIPA();
			}
            else if (txtAuxiliar.Value.Equals("atualizaGridCIPA"))
            {
                txtAuxiliar.Value = string.Empty;
               //PopulaUltraWebGrid(UltraWebGridReuniaoCipa, GeraDataSet(), lblTotRegistros);
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
			this.ID = "ListaReunioesCIPA";

		}
        #endregion

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        private void PopulaDDLCIPA()
		{
            //DataSet dsReuniao = new Cipa().GetIdNome("Posse", "IdCliente=" + Session["Empresa"].ToString()
            //    +" AND IdCIPA IN (SELECT IdCIPA FROM EventoCipa WHERE IdEventoCipa IN (SELECT IdEventoCipa FROM ReuniaoCipa)"
            //    +" AND IdPedido IN (SELECT IdPedido FROM Pedido WHERE (IdPedido IN (SELECT IdPedido FROM ControlePedido WHERE (IdControle=" + (int)Controles.CipaEnvEmailAta
            //    +" AND Termino IS NOT NULL) OR (IdControle=" + (int)Controles.CipaAguarDadosAta + " AND Termino IS NOT NULL)))"
            //    +" OR (DataConclusao IS NOT NULL AND DataCancelamento IS NULL)))", "Posse DESC");

            
            //DataSet dsReuniao = new Cipa().GetIdNome("Posse", "IdCliente=" + Session["Empresa"].ToString() + " and Posse is not Null ", "Posse DESC");
            DataSet dsReuniao = new Cipa().GetIdNome("Edital", "IdCliente=" + Session["Empresa"].ToString() + " and Edital is not Null ", "Posse DESC");

            DataRow row;			
            DataTable table = new DataTable();
			table.Columns.Add("IdCipa", typeof(string));
			table.Columns.Add("DataPosse", typeof(string));

			if (dsReuniao.Tables[0].Rows.Count > 0)
			{
                foreach (DataRow rowReuniao in dsReuniao.Tables[0].Select())
                {
                    row = table.NewRow();
                    row["IdCipa"] = rowReuniao["Id"];
                    row["DataPosse"] = Convert.ToDateTime(rowReuniao["Nome"]).ToString("dd/MM/yyyy");
                    table.Rows.Add(row);
                }
                
                ddlCIPA.DataSource = table;
				ddlCIPA.DataValueField = "IdCipa";
				ddlCIPA.DataTextField = "DataPosse";
				ddlCIPA.DataBind();

                ddlCIPA.SelectedIndex = 0;

                UltraWebGridReuniaoCipa.DataSource = GeraDataSet();
                UltraWebGridReuniaoCipa.DataBind();


                //PopulaUltraWebGrid(UltraWebGridReuniaoCipa, GeraDataSet(), lblTotRegistros);
			}
			else
			{
				ddlCIPA.Visible = false;
				lkbPrintCalendario.Visible = false;
				lblCipa.Visible = false;
               // UltraWebGridReuniaoCipa.Visible = false;
				lblError.Text = "A empresa " + cliente.NomeCompleto + " não possui CIPA!";
			}
		}

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        private DataSet GeraDataSet()
		{
            Guid strAux = Guid.NewGuid();

            //ArrayList alReunioesCIPA = new ReuniaoCipa().Find("IdCipa=" + ddlCIPA.SelectedItem.Value
            //    +" AND (Pedido.IdPedido IN (SELECT IdPedido FROM ControlePedido WHERE IdControle=" + (int)Controles.CipaEnvEmailAta
            //    +" AND Termino IS NOT NULL)"
            //    +" OR (DataConclusao IS NOT NULL AND DataCancelamento IS NULL))"
            //    + " ORDER BY DataSolicitacao");

            ArrayList alReunioesCIPA = new ReuniaoCipa().Find("IdCipa=" + ddlCIPA.SelectedItem.Value + " ORDER BY DataSolicitacao");


			DataSet dsReunioes = new DataSet();
			DataRow rowds;
			DataTable table = new DataTable();
			table.Columns.Add("IdReuniaoCipa", typeof(string));
			table.Columns.Add("TipoEvento", typeof(string));
			table.Columns.Add("Data", typeof(string));
			table.Columns.Add("Executa", typeof(string));
			dsReunioes.Tables.Add(table);

			foreach (ReuniaoCipa reuniao in alReunioesCIPA)
			{
				ControlePedido controlePedido = new ControlePedido();
				controlePedido.Find("IdPedido=" + reuniao.Id
					+" AND IdControle=" + (int)Controles.CipaAguarDadosAta);
				reuniao.IdEventoBaseCipa.Find();

				rowds = dsReunioes.Tables[0].NewRow();
				rowds["IdReuniaoCipa"] = reuniao.Id.ToString();
				rowds["TipoEvento"] = reuniao.IdEventoBaseCipa.Descricao;
				rowds["Data"] = reuniao.DataSolicitacao.ToString("dd-MM-yyyy");
				if (reuniao.DataConclusao == new DateTime())
					if (controlePedido.Termino == new DateTime())
                        rowds["Executa"] = @"<a href=""#"" onClick=""javascript:void(addItem(centerWin('ConfirmaDadosCipa.aspx?IdReuniaoCipa=" + reuniao.Id
                                            + "&IdUsuario=" + usuario.Id + "&IdEmpresa=" + cliente.Id + @"', 580, 640,'ConfirmaDadosCipa'),'Todos'));""><Span class=""defaultDarkFont"">Confirmação de dados para Ata </Span><img src=""img/editaAta.gif"" border=""0"" alt=""Confirmação de dados para Ata""></a>";
					else
						rowds["Executa"] = "Aguardando finalização da Ata <img src='img/clock.gif' border=0 alt='Aguardando finalização da Ata'>";
				else
					rowds["Executa"] = @"<a href=""#"" onClick=""javascript:" + strOpenReport("ControleCIPA", "AtaReuniao.aspx?IliteraSystem=" + strAux.ToString() + strAux.ToString()
                        + "&IdUsuario=" + usuario.Id + "&IdEmpresa=" + cliente.Id + "&IdReuniaoCipa=" + reuniao.Id, "AtaReuniao") + @"""><Span class=""defaultDarkFont"">Imprimir Ata </Span><img src=""img/print.gif"" border=""0"" alt=""Imprimir Ata""></a>";
				dsReunioes.Tables[0].Rows.Add(rowds);
			}

			return dsReunioes;
		}

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        protected void ddlCIPA_SelectedIndexChanged(object sender, System.EventArgs e)
		{
            //PopulaUltraWebGrid(UltraWebGridReuniaoCipa, GeraDataSet(), lblTotRegistros);

            if (ddlCIPA.SelectedIndex < 0)
            {
                UltraWebGridReuniaoCipa.DataSource = null;
                UltraWebGridReuniaoCipa.DataBind();
            }
            else
            {
                UltraWebGridReuniaoCipa.DataSource = GeraDataSet();
                UltraWebGridReuniaoCipa.DataBind();
            }

		}

		private void DGridReuniaoCIPA_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			if (e.CommandName == "Ata")
			{
                if (((LinkButton)e.Item.Cells[3].Controls[0]).Text.IndexOf("Imprimir") != -1)
                {
                    Guid strAux = Guid.NewGuid();

                    OpenReport("ControleCIPA", "AtaReuniao.aspx?IliteraSystem=" + strAux.ToString() + strAux.ToString()
                        + "&IdUsuario=" + Request["IdUsuario"] + "&IdEmpresa=" + Request["IdEmpresa"] + "&IdReuniaoCipa=" + e.Item.Cells[0].Text, "AtaReuniao");
                }
                else if (((LinkButton)e.Item.Cells[3].Controls[0]).Text.IndexOf("Aguardando") == -1)
                {
                    StringBuilder st = new StringBuilder();

                    st.Append("void(addItem(centerWin('ConfirmaDadosCipa.aspx?IdReuniaoCipa=" + e.Item.Cells[0].Text + "&IdUsuario=" + Request["IdUsuario"] + "&IdEmpresa=" + Request["IdEmpresa"] + "', 580, 640,\'ConfirmaDadosCipa\'),\'Todos\'))");

                    this.ClientScript.RegisterStartupScript(this.GetType(), "AbreIrregularidade", st.ToString(), true);
                }
                else
                    System.Diagnostics.Debug.WriteLine(""); 
					//Aviso("A Ata está sendo concluída. Por favor, tente acessá-la mais tarde!");
			}
		}
        protected void lkbPrintCalendario_Click(object sender, EventArgs e)
        {
            Guid strAux = Guid.NewGuid();

            OpenReport("ControleCIPA", "RelatorioAnual.aspx?IliteraSystem=" + strAux.ToString() + strAux.ToString()
                + "&IdUsuario=" + usuario.Id + "&IdEmpresa=" + cliente.Id + "&IdCipa=" + ddlCIPA.SelectedValue, "CipaRelatorioAnual");
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






    }
}
