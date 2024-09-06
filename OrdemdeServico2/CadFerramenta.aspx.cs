using System;
using System.Data;
using System.Web.SessionState;
using System.Web.UI.WebControls;
using System.Text;
using Ilitera.Opsa.Data;


namespace Ilitera.Net.OrdemDeServico
{
	/// <summary>
	/// Summary description for ListaEquipamento.
	/// </summary>
	public partial class CadFerramenta : System.Web.UI.Page
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
				PopulaLsbFerramenta();
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
			btnExcluir.Attributes.Add("onClick" ,"javascript:return confirm('Você deseja realmente excluir esta Ferramenta?');");
		}

		private void PopulaLsbFerramenta()
		{
			lsbFerramentas.DataSource = new Ferramenta().Get("IdCliente=" + lbl_Id_Empresa.Text + " ORDER BY Nome");
			lsbFerramentas.DataValueField = "IdFerramenta";
			lsbFerramentas.DataTextField = "Nome";
			lsbFerramentas.DataBind();

			lsbFerramentas.Items.Insert(0, new ListItem("Cadastrar Nova Ferramenta...", "0"));
			lsbFerramentas.Items[0].Selected = true;
		}

		protected void lsbFerramentas_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (lsbFerramentas.SelectedValue.Equals("0"))
			{
				txtNome.Text = string.Empty;
				txtDescricao.Text = string.Empty;
				btnExcluir.Enabled = false;
			}
			else
			{
				Ferramenta ferramenta = new Ferramenta(Convert.ToInt32(lsbFerramentas.SelectedValue));

				txtNome.Text = ferramenta.Nome;
				txtDescricao.Text = ferramenta.Descricao;

				btnExcluir.Enabled = true;
			}
		}

		protected void btnGravar_Click(object sender, System.EventArgs e)
		{
			try
			{
				Ferramenta ferramenta = new Ferramenta();
				//string tipoProcesso;

                Cliente xCliente;

                xCliente = new Cliente(System.Convert.ToInt32(lbl_Id_Empresa.Text));


				if (lsbFerramentas.SelectedValue.Equals("0"))
				{
					//tipoProcesso = "cadastrada";
					ferramenta.Inicialize();
					ferramenta.IdCliente = xCliente;
				}
				else
				{
					//tipoProcesso = "editada";
					ferramenta.Find(Convert.ToInt32(lsbFerramentas.SelectedValue));
				}

				ferramenta.Nome = txtNome.Text.Trim();
				ferramenta.Descricao = txtDescricao.Text.Trim();

				ferramenta.UsuarioId = System.Convert.ToInt32( lbl_Id_Usuario.Text );
				ferramenta.Save();
				
				PopulaLsbFerramenta();
				lsbFerramentas.ClearSelection();
				lsbFerramentas.Items.FindByValue(ferramenta.Id.ToString()).Selected = true;

				btnExcluir.Enabled = true;

                //StringBuilder st = new StringBuilder();

                ////st.Append("window.opener.document.getElementById('txtAuxiliar').value='atualizaFerramenta';");
                //st.Append("window.opener.document.forms[0].submit();");
                //st.Append("window.alert('A Ferramenta foi " + tipoProcesso + " com sucesso!');");
				
                //this.ClientScript.RegisterStartupScript(this.GetType(), "AtualizaFerramenta", st.ToString(), true);

                StringBuilder st = new StringBuilder();

                //st.Append("window.opener.document.getElementById('txtAuxiliar').value='atualizaCelula';");
                //st.Append("window.opener.document[0]['txtAuxiliar']='atualizaCelula';"); ;   // por que não funciona ??? - Wagner
                //st.Append("window.opener.document.forms[0].submit();");

                Session["txtAuxiliar"] = "atualizaEquipamento";

                //st.Append("<script language='javascript'>");
                st.Append("window.opener.location.reload();");// refreshes main window (normally postback)
                st.Append("self.close();");//closes the pop up
                st.Append("</script>");
                //Page.RegisterStartUpScript("PopUpClose",st.ToString());


                this.ClientScript.RegisterStartupScript(this.GetType(), "AtualizaFerramenta", st.ToString(),true);


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
				DataSet dsProcedimentoFerramenta = new ProcedimentoFerramenta().Get("IdFerramenta=" + lsbFerramentas.SelectedValue);
				if (dsProcedimentoFerramenta.Tables[0].Rows.Count > 0)
					throw new Exception("Não é possível excluir esta Ferramenta! Ela está sendo utilizada em um ou mais procedimentos!");

				Ferramenta ferramenta = new Ferramenta(Convert.ToInt32(lsbFerramentas.SelectedValue));

				ferramenta.UsuarioId = System.Convert.ToInt32( lbl_Id_Usuario.Text);
				ferramenta.Delete();

				PopulaLsbFerramenta();
				txtNome.Text = string.Empty;
				txtDescricao.Text = string.Empty;

				btnExcluir.Enabled = false;

                //StringBuilder st = new StringBuilder();

                ////st.Append("window.opener.document.getElementById('txtAuxiliar').value='atualizaFerramenta';");
                //st.Append("window.opener.document.forms[0].submit();");
                //st.Append("window.alert('A Ferramenta " + ferramenta.Nome + " foi excluída com sucesso!');");

                //this.ClientScript.RegisterStartupScript(this.GetType(), "ExcluiFerramenta", st.ToString(), true);

                StringBuilder st = new StringBuilder();

                //st.Append("window.opener.document.getElementById('txtAuxiliar').value='atualizaCelula';");
                //st.Append("window.opener.document[0]['txtAuxiliar']='atualizaCelula';"); ;   // por que não funciona ??? - Wagner
                //st.Append("window.opener.document.forms[0].submit();");

                Session["txtAuxiliar"] = "atualizaFerramenta";

                //st.Append("<script language='javascript'>");
                st.Append("window.opener.location.reload();");// refreshes main window (normally postback)
                st.Append("self.close();");//closes the pop up
                st.Append("</script>");
                //Page.RegisterStartUpScript("PopUpClose",st.ToString());


                this.ClientScript.RegisterStartupScript(this.GetType(), "ExcluiFerramenta", st.ToString());


			}
			catch(Exception ex)
			{
                MsgBox1.Show("Ilitera.Net", ex.Message, null,
                       new EO.Web.MsgBoxButton("OK"));

			}
		}
	}
}
