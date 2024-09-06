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

namespace Ilitera.Net.ControleEPI
{
    /// <summary>
	/// Summary description for ListagemEPI.
	/// </summary>
	public partial class AddFabricante : WebPageController
	{
		private Fabricante cadFabricante;
		private string execucao;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
            InicializaWebPageObjects();
            string xUsuario = Session["usuarioLogado"].ToString();

            if (!IsPostBack)
			{
				btnCancela.Attributes.Add("onClick", "javascript:self.close()");
				PopulaDDLFabricante();
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

		private void PopulaDDLFabricante()
		{
			ddlFabricante.DataSource = new Fabricante().GetAll("Nome");
			ddlFabricante.DataValueField = "IdFabricante";
			ddlFabricante.DataTextField = "Nome";
			ddlFabricante.DataBind();
			ddlFabricante.Items.Insert(0, new ListItem("Novo Fabricante", "0"));
			
			ddlFabricante.Items.FindByValue(Request["IdFabricante"]).Selected = true;

			if (ddlFabricante.SelectedItem.Value != "0")
				txtFabricante.Text = ddlFabricante.SelectedItem.Text;
		}
		
		protected void btnGravar_Click(object sender, System.EventArgs e)
		{
			try
			{
                PopulaFabricante();	
				cadFabricante.Save();
                PopulaFabricante();	
				
				StringBuilder st = new StringBuilder("");

                st.Append("window.opener." + Request["TipoJanela"] + ".submit(); window.alert(\"O Fabricante \'" + txtFabricante.Text + "\' foi " + execucao + " com sucesso!\"); self.close();");

				ClientScript.RegisterStartupScript(this.GetType(), "FecharJanela", st.ToString(), true);

                Response.Write("<script>window.opener.document.forms[0].submit(); window.close();</script>"); 
			}
			catch(Exception ex)
			{
				lblError.Text = ex.Message;
			} 
		}

		private void PopulaFabricante()
		{
			if (ddlFabricante.SelectedItem.Value == "0")
			{
				cadFabricante = new Fabricante();
				execucao = "adicionado";
			}
			else
			{
				cadFabricante = new Fabricante(Convert.ToInt32(ddlFabricante.SelectedItem.Value));
				execucao = "editado";
			}

			cadFabricante.Nome = txtFabricante.Text;
		}

		protected void ddlFabricante_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (ddlFabricante.SelectedItem.Value != "0")
				txtFabricante.Text = ddlFabricante.SelectedItem.Text;
			else
				txtFabricante.Text = "";
		}
	}
}
