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
using Ilitera.Opsa.Data;
using System.Text;


namespace Ilitera.Net.OrdemDeServico
{
	/// <summary>
	/// Summary description for CadRiscoAcidente.
	/// </summary>
	public partial class CadRiscoAcidente : System.Web.UI.Page
	{

        protected Ilitera.Common.Usuario usuario = new Ilitera.Common.Usuario();
        protected Cliente cliente = new Cliente();
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
            //InicializaWebPageObjects();
            string xUsuario = Session["usuarioLogado"].ToString();

            if (!IsPostBack)
			{
				PopulaLsbRiscoAcidente();
				RegisterClienteCode();
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

		}
		#endregion
		
		private void RegisterClienteCode()
		{
			lsbRiscoAcidente.Attributes.Add("onClick", "javascript:setNome(this.options[this.selectedIndex].text);");
			btnExcluir.Attributes.Add("onClick", "javascript:return confirm('Você deseja realmente excluir este Risco de Acidente?');");
		}

		private void PopulaLsbRiscoAcidente()
		{
			lsbRiscoAcidente.DataSource = new RiscoAcidente().Get("IdCliente=" + cliente.Id + " ORDER BY Nome");
			lsbRiscoAcidente.DataValueField = "IdRiscoAcidente";
			lsbRiscoAcidente.DataTextField = "Nome";
			lsbRiscoAcidente.DataBind();

			lsbRiscoAcidente.Items.Insert(0, new ListItem("Cadastro de um Novo Risco de Acidente...", "0"));
			lsbRiscoAcidente.Items[0].Selected = true;
		}

		protected void btnGravar_Click(object sender, System.EventArgs e)
		{
			try
			{
				RiscoAcidente riscoAcidente = new RiscoAcidente();
				string tipoProcess = string.Empty;

				if (lsbRiscoAcidente.SelectedValue.Equals("0"))
				{
					tipoProcess = "cadastrado";
					riscoAcidente.Inicialize();
					riscoAcidente.IdCliente = cliente;
				}
				else
				{
					tipoProcess = "editado";
					riscoAcidente.Find(Convert.ToInt32(lsbRiscoAcidente.SelectedValue));
				}

				riscoAcidente.Nome = txtNome.Text.Trim();
				
				riscoAcidente.UsuarioId = usuario.Id;
				riscoAcidente.Save();

				PopulaLsbRiscoAcidente();
				lsbRiscoAcidente.ClearSelection();
				lsbRiscoAcidente.Items.FindByValue(riscoAcidente.Id.ToString()).Selected = true;

				btnExcluir.Enabled = true;
				
                //StringBuilder st = new StringBuilder();

                //st.Append("window.opener.document.getElementById('txtAuxiliar').value='atualizaRiscoAcidente';");
                //st.Append("window.opener.document.forms[0].submit();");
                //st.Append("window.alert('O Risco de Acidente foi " + tipoProcess + " com sucesso!');");

                //this.ClientScript.RegisterStartupScript(this.GetType(), "SalvarRiscoAcidente", st.ToString(), true);

                StringBuilder st = new StringBuilder();

 

                Session["txtAuxiliar"] = "atualizaRiscoAcidente";

                //st.Append("<script language='javascript'>");
                st.Append("window.opener.location.reload();");// refreshes main window (normally postback)
                st.Append("self.close();");//closes the pop up
                st.Append("</script>");
                //Page.RegisterStartUpScript("PopUpClose",st.ToString());


                this.ClientScript.RegisterStartupScript(this.GetType(), "SalvarRiscoAcidente", st.ToString());
			}
			catch(Exception ex)
			{
                MsgBox1.Show("Ilitera.Net", ex.Message, null,
                       new EO.Web.MsgBoxButton("OK"));

			}
		}

		protected void btnExcluir_Click(object sender, System.EventArgs e)
		{
			try
			{
				DataSet dsOperacaoRiscoAcidente = new OperacaoRiscoAcidente().Get("IdRiscoAcidente=" + lsbRiscoAcidente.SelectedValue);
				if (dsOperacaoRiscoAcidente.Tables[0].Rows.Count > 0)
					throw new Exception("Não é possível excluir este Risco de Acidente! Este Risco já está sendo utilizado em uma ou mais Operações!");

				RiscoAcidente riscoAcidente = new RiscoAcidente(Convert.ToInt32(lsbRiscoAcidente.SelectedValue));
				
				riscoAcidente.UsuarioId = usuario.Id;
				riscoAcidente.Delete();

				PopulaLsbRiscoAcidente();

				txtNome.Text = string.Empty;
				btnExcluir.Enabled = false;

                //StringBuilder st = new StringBuilder();

                //st.Append("window.opener.document.getElementById('txtAuxiliar').value='atualizaRiscoAcidente';");
                //st.Append("window.opener.document.forms[0].submit();");
                //st.Append("window.alert('O Risco de Acidente selecionado foi excluído com sucesso!');");

                //this.ClientScript.RegisterStartupScript(this.GetType(), "ExcluirRiscoAcidente", st.ToString(), true);

                StringBuilder st = new StringBuilder();

                //st.Append("window.opener.document.getElementById('txtAuxiliar').value='atualizaCelula';");
                //st.Append("window.opener.document[0]['txtAuxiliar']='atualizaCelula';"); ;   // por que não funciona ??? - Wagner
                //st.Append("window.opener.document.forms[0].submit();");

                Session["txtAuxiliar"] = "atualizaRiscoAcidente";

                //st.Append("<script language='javascript'>");
                st.Append("window.opener.location.reload();");// refreshes main window (normally postback)
                st.Append("self.close();");//closes the pop up
                st.Append("</script>");
                //Page.RegisterStartUpScript("PopUpClose",st.ToString());


                this.ClientScript.RegisterStartupScript(this.GetType(), "ExcluirRiscoAcidente", st.ToString());


			}
			catch(Exception ex)
			{
                MsgBox1.Show("Ilitera.Net", ex.Message, null,
                       new EO.Web.MsgBoxButton("OK"));

				btnExcluir.Enabled = true;
			}
		}
	}
}
