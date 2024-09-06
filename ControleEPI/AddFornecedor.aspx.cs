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

namespace Ilitera.Net
{

	/// <summary>
	/// Summary description for ListagemEPI.
	/// </summary>
	public partial class AddFornecedor : System.Web.UI.Page 
	{
		private FornecedorEPI cadFornecedor;
		private string execucao;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
            //InicializaWebPageObjects();
            string xUsuario = Session["usuarioLogado"].ToString();

            if (!IsPostBack)
			{

                Entities.Usuario user = (Entities.Usuario)Session["usuarioLogado"];

                int xIdEmpresa = System.Convert.ToInt32(Session["Empresa"]);
                int xIdUsuario = user.IdUsuario;

                lbl_Id_Empresa.Text = xIdEmpresa.ToString().Trim();
                lbl_Id_Usuario.Text = xIdUsuario.ToString().Trim();



				btnCancela.Attributes.Add("onClick", "javascript:self.close()");
				btnExcluir.Attributes.Add("onClick", "javascript: if(confirm('Você deseja realmente Excluir este Fornecedor?')) return true; else return false;");
				PopulaDDLFornecedor();
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

		private void PopulaDDLFornecedor()
		{
			ddlFornecedor.DataSource = new FornecedorEPI().GetFornecedor(lbl_Id_Empresa.Text);
			ddlFornecedor.DataValueField = "IdFornecedorEPI";
			ddlFornecedor.DataTextField = "NomeCompleto";
			ddlFornecedor.DataBind();
			ddlFornecedor.Items.Insert(0, new ListItem("Novo Fornecedor", "0"));
			
			//ddlFornecedor.Items.FindByValue(Request["IdFornecedorEPI"]).Selected = true;

			if (ddlFornecedor.SelectedItem.Value != "0")
				txtFornecedor.Text = ddlFornecedor.SelectedItem.Text;
			else
				btnExcluir.Visible = false;
		}
		
		protected void btnGravar_Click(object sender, System.EventArgs e)
		{
			try
			{
				PopulaFornecedor();	
				cadFornecedor.Save();
				
				StringBuilder st = new StringBuilder("");
                
                st.Append("window.opener.document.forms[0].submit(); window.alert(\"O Fornecedor \'" + txtFornecedor.Text + "\' foi " + execucao + " com sucesso!\"); self.close();");

				ClientScript.RegisterStartupScript(this.GetType(), "FecharJanela", st.ToString(), true);
                Response.Redirect("~/ControleEPI/" + Session["Pagina_Anterior"].ToString());
			}
			catch(Exception ex)
			{
				lblError.Text = ex.Message;
			} 
		}

		private void PopulaFornecedor()
		{
			if (ddlFornecedor.SelectedItem.Value == "0")
			{
				cadFornecedor = new FornecedorEPI();
				cadFornecedor.Inicialize();
				execucao = "adicionado";
			}
			else
			{
				cadFornecedor = new FornecedorEPI(Convert.ToInt32(ddlFornecedor.SelectedItem.Value));
				execucao = "editado";
			}

			cadFornecedor.Find();
			cadFornecedor.NomeCompleto = txtFornecedor.Text;
			
			if (txtFornecedor.Text.IndexOf(" ") == -1)
				cadFornecedor.NomeAbreviado = txtFornecedor.Text;
			else
				cadFornecedor.NomeAbreviado = txtFornecedor.Text.Substring(0, txtFornecedor.Text.IndexOf(" "));

			cadFornecedor.IdJuridicaPapel.Id = (int)IndJuridicaPapel.FornecedorEPI;
			cadFornecedor.IdCliente.Id = System.Convert.ToInt32( lbl_Id_Empresa.Text);
		}

		protected void ddlFornecedor_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			lblError.Text = string.Empty;
			if (ddlFornecedor.SelectedItem.Value != "0")
			{
				txtFornecedor.Text = ddlFornecedor.SelectedItem.Text;
				btnExcluir.Visible = true;
			}
			else
			{
				txtFornecedor.Text = "";
				btnExcluir.Visible = false;
			}
		}

		protected void btnExcluir_Click(object sender, System.EventArgs e)
		{
			try
			{
				cadFornecedor = new FornecedorEPI(Convert.ToInt32(ddlFornecedor.SelectedValue));
				cadFornecedor.UsuarioId = System.Convert.ToInt32( lbl_Id_Usuario.Text );
				cadFornecedor.Delete();

				StringBuilder st = new StringBuilder("");

				st.Append("window.opener.document.forms[0].submit(); window.alert(\"O Fornecedor \'" + txtFornecedor.Text + "\' foi excluído com sucesso!\"); self.close();");

				ClientScript.RegisterStartupScript(this.GetType(), "ExcluirFornecedor", st.ToString(), true);
                Response.Redirect("~/ControleEPI/" + Session["Pagina_Anterior"].ToString());
			}
			catch (Exception ex)
			{				
                MsgBox1.Show("Ilitera.Net", ex.Message, null,
                        new EO.Web.MsgBoxButton("OK"));                

			}
		}

        protected void btnCancela_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/ControleEPI/" + Session["Pagina_Anterior"].ToString());
        }
	}
}
