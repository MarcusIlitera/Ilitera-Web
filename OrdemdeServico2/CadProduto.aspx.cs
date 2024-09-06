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
	public partial class CadProduto : System.Web.UI.Page
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
				PopulaLsbProduto();
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
			btnExcluir.Attributes.Add("onClick" ,"javascript:return confirm('Você deseja realmente excluir este Produto?');");
		}

		private void PopulaLsbProduto()
		{
			lsbProdutos.DataSource = new Produto().Get("IdCliente=" + lbl_Id_Empresa.Text + " ORDER BY Nome");
			lsbProdutos.DataValueField = "IdProduto";
			lsbProdutos.DataTextField = "Nome";
			lsbProdutos.DataBind();

			lsbProdutos.Items.Insert(0, new ListItem("Cadastrar Novo Produto...", "0"));
			lsbProdutos.Items[0].Selected = true;
		}

		protected void lsbProdutos_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (lsbProdutos.SelectedValue.Equals("0"))
			{
				txtNome.Text = string.Empty;
				txtDescricao.Text = string.Empty;
				btnExcluir.Enabled = false;
			}
			else
			{
				Produto produto = new Produto(Convert.ToInt32(lsbProdutos.SelectedValue));

				txtNome.Text = produto.Nome;
				txtDescricao.Text = produto.Descricao;

				btnExcluir.Enabled = true;
			}
		}

		protected void btnGravar_Click(object sender, System.EventArgs e)
		{
			try
			{
				Produto produto = new Produto();
				//string tipoProcesso;

                Cliente xCliente;

                xCliente = new Cliente(System.Convert.ToInt32(lbl_Id_Empresa.Text));

				if (lsbProdutos.SelectedValue.Equals("0"))
				{
					//tipoProcesso = "cadastrado";
					produto.Inicialize();
					produto.IdCliente = xCliente;
				}
				else
				{
					//tipoProcesso = "editado";
					produto.Find(Convert.ToInt32(lsbProdutos.SelectedValue));
				}

				produto.Nome = txtNome.Text.Trim();
				produto.Descricao = txtDescricao.Text.Trim();

				produto.UsuarioId = System.Convert.ToInt32( lbl_Id_Usuario.Text);
				produto.Save();
				
				PopulaLsbProduto();
				lsbProdutos.ClearSelection();
				lsbProdutos.Items.FindByValue(produto.Id.ToString()).Selected = true;

				btnExcluir.Enabled = true;

                //StringBuilder st = new StringBuilder();

                ////está dando erro, mas está atualizando listbox.  Ver se pode tirar duas linhas abaixo. - Wagner
                ////st.Append("window.opener.document.getElementById('txtAuxiliar').value='atualizaProduto';");
                //st.Append("window.opener.document.forms[0].submit();");
                //st.Append("window.alert('O Produto foi " + tipoProcesso + " com sucesso!');");

                //this.ClientScript.RegisterStartupScript(this.GetType(), "AtualizaProduto", st.ToString(), true);

                StringBuilder st = new StringBuilder();

                //st.Append("window.opener.document.getElementById('txtAuxiliar').value='atualizaCelula';");
                //st.Append("window.opener.document[0]['txtAuxiliar']='atualizaCelula';"); ;   // por que não funciona ??? - Wagner
                //st.Append("window.opener.document.forms[0].submit();");

                Session["txtAuxiliar"] = "atualizaProduto";

                //st.Append("<script language='javascript'>");
                st.Append("window.opener.location.reload();");// refreshes main window (normally postback)
                st.Append("self.close();");//closes the pop up
                st.Append("</script>");
                //Page.RegisterStartUpScript("PopUpClose",st.ToString());


                this.ClientScript.RegisterStartupScript(this.GetType(), "AtualizaProduto", st.ToString(),true);


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
				DataSet dsProcedimentoProduto = new ProcedimentoProduto().Get("IdProduto=" + lsbProdutos.SelectedValue);
				if (dsProcedimentoProduto.Tables[0].Rows.Count > 0)
					throw new Exception("Não é possível excluir este Produto! Ele está sendo utilizado em um ou mais procedimentos!");

				Produto produto = new Produto(Convert.ToInt32(lsbProdutos.SelectedValue));

				produto.UsuarioId = System.Convert.ToInt32( lbl_Id_Usuario.Text);
				produto.Delete();

				PopulaLsbProduto();
				txtNome.Text = string.Empty;
				txtDescricao.Text = string.Empty;

				btnExcluir.Enabled = false;

                //StringBuilder st = new StringBuilder();

                ////st.Append("window.opener.document.getElementById('txtAuxiliar').value='atualizaProduto';");
                ////st.Append("window.opener.document.forms[0].submit();");
                //st.Append("window.alert('O Produto " + produto.Nome + " foi excluído com sucesso!');");

                //this.ClientScript.RegisterStartupScript(this.GetType(), "ExcluiProduto", st.ToString(), true);

                StringBuilder st = new StringBuilder();

                //st.Append("window.opener.document.getElementById('txtAuxiliar').value='atualizaCelula';");
                //st.Append("window.opener.document[0]['txtAuxiliar']='atualizaCelula';"); ;   // por que não funciona ??? - Wagner
                //st.Append("window.opener.document.forms[0].submit();");

                Session["txtAuxiliar"] = "atualizaProduto";

                //st.Append("<script language='javascript'>");
                st.Append("window.opener.location.reload();");// refreshes main window (normally postback)
                st.Append("self.close();");//closes the pop up
                st.Append("</script>");
                //Page.RegisterStartUpScript("PopUpClose",st.ToString());


                this.ClientScript.RegisterStartupScript(this.GetType(), "ExcluiProduto", st.ToString());


			}
			catch(Exception ex)
			{
                MsgBox1.Show("Ilitera.Net", ex.Message, null,
                       new EO.Web.MsgBoxButton("OK"));

			}
		}
	}
}
