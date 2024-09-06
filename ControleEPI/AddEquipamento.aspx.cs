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
	public partial class AddEquipamento : WebPageController
	{
		private TipoEPI cadEquipamento;
		private string execucao;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
            InicializaWebPageObjects();
            string xUsuario = Session["usuarioLogado"].ToString();

            if (!IsPostBack)
			{
				btnCancela.Attributes.Add("onClick", "javascript:self.close()");
				PopulaDDLEquipamento();
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

		private void PopulaDDLEquipamento()
		{
			ddlEquipamento.DataSource = new TipoEPI().GetAll("Nome");
			ddlEquipamento.DataValueField = "IdTipoEPI";
			ddlEquipamento.DataTextField = "Nome";
			ddlEquipamento.DataBind();
			ddlEquipamento.Items.Insert(0, new ListItem("Novo Equipamento", "0"));
			
			ddlEquipamento.Items.FindByValue(Request["IdEquipamento"]).Selected = true;

			if (ddlEquipamento.SelectedItem.Value != "0")
				txtEquipamento.Text = ddlEquipamento.SelectedItem.Text;
		}
		
		protected void btnGravar_Click(object sender, System.EventArgs e)
		{
			try
			{
				PopulaEquipamento();	
				cadEquipamento.Save();
				
				StringBuilder st = new StringBuilder("");

	            st.Append("window.opener." + Request["TipoJanela"] + ".submit(); window.alert(\"O Equipamento \'" + txtEquipamento.Text + "\' foi " + execucao + " com sucesso!\"); self.close();");

				ClientScript.RegisterStartupScript(this.GetType(), "FecharJanela", st.ToString(), true);
			}
			catch(Exception ex)
			{
				lblError.Text = ex.Message;
			} 
		}

		private void PopulaEquipamento()
		{
			if (ddlEquipamento.SelectedItem.Value == "0")
			{
				cadEquipamento = new TipoEPI();
				execucao = "adicionado";
			}
			else
			{
				cadEquipamento = new TipoEPI(Convert.ToInt32(ddlEquipamento.SelectedItem.Value));
				execucao = "editado";
			}

			cadEquipamento.Nome = txtEquipamento.Text;
		}

		protected void ddlEquipamento_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (ddlEquipamento.SelectedItem.Value != "0")
				txtEquipamento.Text = ddlEquipamento.SelectedItem.Text;
			else
				txtEquipamento.Text = "";
		}
	}
}
