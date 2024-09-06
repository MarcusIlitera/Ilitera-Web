using System;
using System.Data;
using System.Web.SessionState;
using System.Web.UI.WebControls;
using Ilitera.Common;
using Ilitera.Opsa.Data;
using System.Text;
//using MestraNET;

namespace Ilitera.Net.ControleExtintores
{
    public partial class CadFabricanteExtintor : System.Web.UI.Page
    {

        protected Prestador prestador = new Prestador();
        protected Usuario usuario = new Usuario();
        protected Cliente cliente = new Cliente();
        protected Empregado empregado = new Empregado();

		
		protected void Page_Load(object sender, System.EventArgs e)
		{
            //InicializaWebPageObjects();
            string xUsuario = Session["usuarioLogado"].ToString();

            if (!IsPostBack)
			{
				PopulalsbFabricanteExtintores();
				btnFechar.Attributes.Add("onClick", "javascript:self.close();");
				btnExcluir.Attributes.Add("onClick", "javascript:return confirm('Você deseja realmente excluir este Fabricante de Extintor de Incêndio?');");
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
			this.ID = "CadFabricanteExtintor";

		}
		#endregion

		private void PopulalsbFabricanteExtintores()
		{
			lsbFabricanteExtintor.DataSource = new FabricanteExtintor().Get("IdCliente=" + Request["IdEmpresa"] + " ORDER BY NomeCompleto");
			lsbFabricanteExtintor.DataValueField = "IdFabricanteExtintor";
			lsbFabricanteExtintor.DataTextField = "NomeCompleto";
			lsbFabricanteExtintor.DataBind();

			lsbFabricanteExtintor.Items.Insert(0, new ListItem("Novo Fabricante de Extintor...", "Novo"));
			lsbFabricanteExtintor.Items.FindByValue("Novo").Selected = true;

			txtFabricante.Text = string.Empty;
			btnExcluir.Visible = false;

			SetFocus();
		}

		private void SetFocus()
		{
			StringBuilder st = new StringBuilder();

			st.Append("document.forms[0].txtFabricante.focus();");

            this.ClientScript.RegisterStartupScript(this.GetType(), "Focus", st.ToString(), true);
		}

		protected void btnGravar_Click(object sender, System.EventArgs e)
		{
			try
			{
				FabricanteExtintor fabricanteExtintor = new FabricanteExtintor();
				string tipoCadastro = string.Empty;

				if (lsbFabricanteExtintor.SelectedValue == "Novo")
				{
					fabricanteExtintor.Inicialize();
					fabricanteExtintor.IdCliente.Id = Convert.ToInt32(Request["IdEmpresa"]);
					tipoCadastro = "cadastrado";
				}
				else
				{
					fabricanteExtintor = new FabricanteExtintor(Convert.ToInt32(lsbFabricanteExtintor.SelectedValue));
					tipoCadastro = "editado";
				}

				fabricanteExtintor.NomeCompleto = txtFabricante.Text.Trim();
				
				if (txtFabricante.Text.Trim().IndexOf(" ") == -1)
					fabricanteExtintor.NomeAbreviado = txtFabricante.Text.Trim();
				else
					fabricanteExtintor.NomeAbreviado = txtFabricante.Text.Trim().Substring(0, txtFabricante.Text.Trim().IndexOf(" "));

				fabricanteExtintor.IdJuridicaPapel.Id = (int)IndJuridicaPapel.FabricanteExtintor;

				fabricanteExtintor.UsuarioId = usuario.Id;
				fabricanteExtintor.Save();

				StringBuilder st = new StringBuilder();

				st.Append("window.opener.document.getElementById('txtAuxiliar').value='atualizaFabricante';");
				st.Append("window.opener.document.forms[0].submit();");
				st.Append("window.alert('O Fabricante de Extintor de Incêndio foi " + tipoCadastro + " com sucesso!');");

                this.ClientScript.RegisterStartupScript(this.GetType(), "EdicaoCadastroFabricanteExtintor", st.ToString(), true);

				PopulalsbFabricanteExtintores();
			}
			catch (Exception ex)
			{
                MsgBox1.Show("Ilitera.Net", ex.Message, null,
                new EO.Web.MsgBoxButton("OK")); 
			}
		}

		protected void btnExcluir_Click(object sender, System.EventArgs e)
		{
			try
			{
				DataSet dsExtintores = new Extintores().Get("IdFabricanteExtintor=" + lsbFabricanteExtintor.SelectedValue);

				if (dsExtintores.Tables[0].Rows.Count > 0)
					throw new Exception("Não é possível Excluir este Fabricante de Extintor! Ele já está sendo utilizado no cadastro de um Extintor de Incêndio!");

				FabricanteExtintor fabricanteExtintor = new FabricanteExtintor(Convert.ToInt32(lsbFabricanteExtintor.SelectedValue));
				fabricanteExtintor.UsuarioId = usuario.Id;
				fabricanteExtintor.Delete();

				StringBuilder st = new StringBuilder();

				st.Append("window.opener.document.getElementById('txtAuxiliar').value='atualizaFabricante';");
				st.Append("window.opener.document.forms[0].submit();");
				st.Append("window.alert('O Fabricante de Extintor de Incêndio foi Excluído com sucesso!');");

                this.ClientScript.RegisterStartupScript(this.GetType(), "ExclusaoFabricante", st.ToString(), true);

				PopulalsbFabricanteExtintores();
			}
			catch (Exception ex)
			{
                MsgBox1.Show("Ilitera.Net", ex.Message, null,
                new EO.Web.MsgBoxButton("OK")); 
			}
		}

		protected void lsbFabricanteExtintor_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (lsbFabricanteExtintor.SelectedValue == "Novo")
			{
				txtFabricante.Text = string.Empty;
				btnExcluir.Visible = false;
			}
			else
			{
				FabricanteExtintor fabricanteExtintor = new FabricanteExtintor(Convert.ToInt32(lsbFabricanteExtintor.SelectedValue));

				txtFabricante.Text = fabricanteExtintor.NomeCompleto;
				btnExcluir.Visible = true;
			}

			SetFocus();
		}
	}
}
