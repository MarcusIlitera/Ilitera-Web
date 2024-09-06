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
    public partial class CadTipoExtintor : System.Web.UI.Page
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
				PopulalsbTipoExtintores();
				btnFechar.Attributes.Add("onClick", "javascript:self.close();");
				btnExcluir.Attributes.Add("onClick", "javascript:return confirm('Você deseja realmente excluir este Tipo de Extintor de Incêndio?');");
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
			this.ID = "CadTipoExtintor";

		}
		#endregion

		private void PopulalsbTipoExtintores()
		{
			lsbTipoExtintor.DataSource = new TipoExtintor().Get("IdCliente=" + Request["IdEmpresa"] + " ORDER BY ModeloExtintor");
			lsbTipoExtintor.DataValueField = "IdTipoExtintor";
			lsbTipoExtintor.DataTextField = "ModeloExtintor";
			lsbTipoExtintor.DataBind();

			lsbTipoExtintor.Items.Insert(0, new ListItem("Novo Tipo de Extintor...", "Novo"));
			lsbTipoExtintor.Items.FindByValue("Novo").Selected = true;

			txtModeloExtintor.Text = string.Empty;
			txtAgenteExtintor.Text = string.Empty;
			btnExcluir.Visible = false;

			SetFocus();
		}

		private void SetFocus()
		{
			StringBuilder st = new StringBuilder();

			st.Append("document.forms[0].txtModeloExtintor.focus();");

            this.ClientScript.RegisterStartupScript(this.GetType(), "Focus", st.ToString(), true);
		}

		protected void btnGravar_Click(object sender, System.EventArgs e)
		{
			try
			{
				TipoExtintor tipoExtintor = new TipoExtintor();
				string tipoCadastro = string.Empty;

				if (lsbTipoExtintor.SelectedValue == "Novo")
				{
					tipoExtintor.Inicialize();
					tipoExtintor.IdCliente.Id = Convert.ToInt32(Request["IdEmpresa"]);
					tipoCadastro = "cadastrado";
				}
				else
				{
					tipoExtintor = new TipoExtintor(Convert.ToInt32(lsbTipoExtintor.SelectedValue));
					tipoCadastro = "editado";
				}

				tipoExtintor.ModeloExtintor = txtModeloExtintor.Text.Trim();
				tipoExtintor.AgenteExtintor = txtAgenteExtintor.Text.Trim();

				tipoExtintor.UsuarioId = usuario.Id;
				tipoExtintor.Save();

				StringBuilder st = new StringBuilder();

				st.Append("window.opener.document.getElementById('txtAuxiliar').value='atualizaTipo';");
				st.Append("window.opener.document.forms[0].submit();");
				st.Append("window.alert('O Tipo de Extintor de Incêndio foi " + tipoCadastro + " com sucesso!');");

                this.ClientScript.RegisterStartupScript(this.GetType(), "EdicaoCadastroTipoExtintor", st.ToString(), true);

				PopulalsbTipoExtintores();
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
				DataSet dsExtintores = new Extintores().Get("IdTipoExtintor=" + lsbTipoExtintor.SelectedValue);
				if (dsExtintores.Tables[0].Rows.Count > 0)
					throw new Exception("Não é possível Excluir este Tipo de Extintor de Incêndio! Ele já está sendo utilizado no cadastro de um Extintor de Incêndio!");
				
				TipoExtintor tipoExtintor = new TipoExtintor(Convert.ToInt32(lsbTipoExtintor.SelectedValue));
				tipoExtintor.UsuarioId = usuario.Id;
				tipoExtintor.Delete();

				StringBuilder st = new StringBuilder();

				st.Append("window.opener.document.getElementById('txtAuxiliar').value='atualizaTipo';");
				st.Append("window.opener.document.forms[0].submit();");
				st.Append("window.alert('O Tipo de Extintor de Incêndio foi Excluído com sucesso!');");

                this.ClientScript.RegisterStartupScript(this.GetType(), "ExclusaoExtintor", st.ToString(), true);

				PopulalsbTipoExtintores();
			}
			catch (Exception ex)
			{
                MsgBox1.Show("Ilitera.Net", ex.Message, null,
               new EO.Web.MsgBoxButton("OK")); 

			}
		}

		protected void lsbTipoExtintor_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (lsbTipoExtintor.SelectedValue == "Novo")
			{
				txtModeloExtintor.Text = string.Empty;
				txtAgenteExtintor.Text = string.Empty;
				btnExcluir.Visible = false;
			}
			else
			{
				TipoExtintor tipoExtintor = new TipoExtintor(Convert.ToInt32(lsbTipoExtintor.SelectedValue));

				txtModeloExtintor.Text = tipoExtintor.ModeloExtintor;
				txtAgenteExtintor.Text = tipoExtintor.AgenteExtintor;
				btnExcluir.Visible = true;
			}

			SetFocus();
		}
	}
}
