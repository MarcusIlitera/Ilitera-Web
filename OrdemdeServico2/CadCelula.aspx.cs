using System;
using System.Data;
using System.Web.SessionState;
using System.Web.UI.WebControls;
using System.Text;
using Ilitera.Opsa.Data;


namespace Ilitera.Net.OrdemDeServico
{
	/// <summary>
	/// Summary description for CadCelula
	/// </summary>
	public partial class CadCelula : System.Web.UI.Page
	{

		public void PageLoadEvent(object sender, System.EventArgs e)
		{
			//InicializaWebPageObjects();

            Entities.Usuario user = (Entities.Usuario)Session["usuarioLogado"];

            int xIdEmpresa = System.Convert.ToInt32(Session["Empresa"]);
            int xIdUsuario = user.IdUsuario;

            lbl_Id_Empresa.Text = xIdEmpresa.ToString().Trim();
            lbl_Id_Usuario.Text = xIdUsuario.ToString().Trim();


			if (!IsPostBack)
			{
				PopulaLsbCelula();
				RegisterClientCode();
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
			this.Load += new System.EventHandler(this.PageLoadEvent);

		}
		#endregion

		private void RegisterClientCode()
		{
			btnExcluir.Attributes.Add("onClick" ,"javascript:return confirm('Você deseja realmente excluir esta Celula?');");
		}

		private void PopulaLsbCelula()
		{
			lsbCelulas.DataSource = new Celula().Get("IdCliente=" + lbl_Id_Empresa.Text + " ORDER BY Nome");
			lsbCelulas.DataValueField = "IdCelula";
			lsbCelulas.DataTextField = "Nome";
			lsbCelulas.DataBind();

			lsbCelulas.Items.Insert(0, new ListItem("Cadastrar Nova Celula...", "0"));
			lsbCelulas.Items[0].Selected = true;
		}

		protected void lsbCelulas_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (lsbCelulas.SelectedValue.Equals("0"))
			{
				txtNome.Text = string.Empty;
				txtDescricao.Text = string.Empty;
				btnExcluir.Enabled = false;
			}
			else
			{
				Celula celula = new Celula(Convert.ToInt32(lsbCelulas.SelectedValue));

				txtNome.Text = celula.Nome;
				txtDescricao.Text = celula.Descricao;

				btnExcluir.Enabled = true;
			}
		}

		protected void btnGravar_Click(object sender, System.EventArgs e)
		{
			try
			{
				Celula celula = new Celula();
				string tipoProcesso;

                Cliente xCliente;

                xCliente = new Cliente(System.Convert.ToInt32(lbl_Id_Empresa.Text));


				if (lsbCelulas.SelectedValue.Equals("0"))
				{
					tipoProcesso = "cadastrada";
					celula.Inicialize();
					celula.IdCliente = xCliente;
				}
				else
				{
					tipoProcesso = "editada";
					celula.Find(Convert.ToInt32(lsbCelulas.SelectedValue));
				}

				celula.Nome = txtNome.Text.Trim();
				celula.Descricao = txtDescricao.Text.Trim();

                celula.UsuarioId = System.Convert.ToInt32( lbl_Id_Usuario.Text);
				celula.Save();
				
				PopulaLsbCelula();
				lsbCelulas.ClearSelection();
				lsbCelulas.Items.FindByValue(celula.Id.ToString()).Selected = true;

				btnExcluir.Enabled = true;

				StringBuilder st = new StringBuilder();

                
             

                Session["txtAuxiliar"] = "atualizaCelula";



                //Funcionou - Wagner

                //st.Append("<script language='javascript'>");
                st.Append("window.opener.location.reload();");// refreshes main window (normally postback)
                st.Append("self.close();");//closes the pop up
                st.Append("</script>");
                //Page.RegisterStartUpScript("PopUpClose",st.ToString());
                this.ClientScript.RegisterStartupScript(this.GetType(), "AtualizaCelula", st.ToString(), true);

                
				st.Append("window.alert('A Celula foi " + tipoProcesso + " com sucesso!');");

                
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
				DataSet dsProcedimentoCelula = new ProcedimentoCelula().Get("IdCelula=" + lsbCelulas.SelectedValue);
				if (dsProcedimentoCelula.Tables[0].Rows.Count > 0)
					throw new Exception("Não é possível excluir esta Celula! Ela está sendo utilizada em um ou mais procedimentos!");

				Celula celula = new Celula(Convert.ToInt32(lsbCelulas.SelectedValue));

				celula.UsuarioId = System.Convert.ToInt32( lbl_Id_Usuario.Text);
				celula.Delete();

				PopulaLsbCelula();
				txtNome.Text = string.Empty;
				txtDescricao.Text = string.Empty;

				btnExcluir.Enabled = false;

				StringBuilder st = new StringBuilder();

				//st.Append("window.opener.document.getElementById('txtAuxiliar').value='atualizaCelula';");
                //st.Append("window.opener.document[0]['txtAuxiliar']='atualizaCelula';"); ;   // por que não funciona ??? - Wagner
                //st.Append("window.opener.document.forms[0].submit();");

                Session["txtAuxiliar"] = "atualizaCelula";

               //st.Append("<script language='javascript'>");
               st.Append("window.opener.location.reload();");// refreshes main window (normally postback)
               st.Append("self.close();");//closes the pop up
               st.Append("</script>");
               //Page.RegisterStartUpScript("PopUpClose",st.ToString());


               this.ClientScript.RegisterStartupScript(this.GetType(), "ExcluiCelula", st.ToString());
                
				//st.Append("window.alert('A Celula " + celula.Nome + " foi excluída com sucesso!');");

                
			}
			catch(Exception ex)
			{
                MsgBox1.Show("Ilitera.Net", ex.Message, null,
                       new EO.Web.MsgBoxButton("OK"));

			}
		}
	}
}
