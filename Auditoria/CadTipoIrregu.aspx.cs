using System;
using System.Data;
using System.Web.SessionState;
using System.Web.UI.WebControls;
using Ilitera.Common;
using Ilitera.Opsa.Data;
using System.Text;


namespace Ilitera.Net
{
	/// <summary>
	/// Summary description for ListaEmpresa.
	/// </summary>
    public partial class CadTipoIrregu : System.Web.UI.Page
	{
		protected void Page_Load(object sender, System.EventArgs e)
		{
            //InicializaWebPageObjects();
            string xUsuario = Session["usuarioLogado"].ToString();

            if (!IsPostBack)
            {
                RegisterClientCode();
                PopulaLBXTipo();
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
			this.ID = "CadTipoIrregu";

		}
		#endregion

        private void RegisterClientCode()
        {
            lbxTipoIrre.Attributes.Add("onClick", "javascript:setNome(this.options[this.selectedIndex].text);");
            btnExcluir.Attributes.Add("onClick", "javascript: return confirm('Você deseja mesmo excluir este Tipo da Irregularidade?');");
        }
        
        private void PopulaLBXTipo()
		{
            Cliente cliente;
            cliente = new Cliente(System.Convert.ToInt32(Session["Empresa"].ToString()));

			lbxTipoIrre.DataSource = new TipoIrregularidade().Get("IdCliente=" + cliente.Id + " ORDER BY Nome");
			lbxTipoIrre.DataValueField = "IdTipoIrregularidade";
			lbxTipoIrre.DataTextField = "Nome";
			lbxTipoIrre.DataBind();

            lbxTipoIrre.Items.Insert(0, new ListItem("Cadastro de um Novo Tipo de Irregularidade...", "0"));
            lbxTipoIrre.Items[0].Selected = true;
		}

		protected void btnSalvar_Click(object sender, System.EventArgs e)
		{
			try
			{
                TipoIrregularidade tipoIrre = new TipoIrregularidade();
                string tipo;
				
				if (!lbxTipoIrre.SelectedValue.Equals("0"))
				{
					tipo = "atualizado";
					tipoIrre.Find(Convert.ToInt32(lbxTipoIrre.SelectedValue));
				}
				else
				{
					tipo = "cadastrado";
					tipoIrre.Inicialize();
				}

                Cliente cliente;
                cliente = new Cliente(System.Convert.ToInt32(Session["Empresa"].ToString()));

				tipoIrre.Nome = txtTipoIrregu.Text.Trim();
				tipoIrre.IdCliente = cliente;

                Usuario usuario = new Usuario();
                usuario.Find(System.Convert.ToInt32(Request["IdUsuario"].ToString()));

                tipoIrre.UsuarioId = usuario.Id;
                tipoIrre.Save();

                PopulaLBXTipo();
                lbxTipoIrre.ClearSelection();
                lbxTipoIrre.Items.FindByValue(tipoIrre.Id.ToString()).Selected = true;
                btnExcluir.Enabled = true;

				StringBuilder st = new StringBuilder();

                st.Append("window.opener.document.getElementById('txtAuxiliar').value='atualizaTipoIrregularidade';");
                st.Append("window.opener.document.forms[0].submit();");
				st.Append("window.alert('O Tipo da Irregularidade foi " + tipo + " com sucesso!');");

				this.ClientScript.RegisterStartupScript(this.GetType(), "SavarIrreg", st.ToString(), true);
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
				TipoIrregularidade tipoIrre = new TipoIrregularidade(Convert.ToInt32(lbxTipoIrre.SelectedValue));

				DataSet dsVerificaUsoTipoIrre = new Irregularidade().Get("IdTipoIrregularidade=" + tipoIrre.Id);
                if (dsVerificaUsoTipoIrre.Tables[0].Rows.Count > 0)
                    throw new Exception("Este Tipo de Irregularidade já está sendo utilizado! Não é possível excluí-lo!");

                Usuario usuario = new Usuario();
                usuario.Find(System.Convert.ToInt32(Request["IdUsuario"].ToString()));

                tipoIrre.UsuarioId = usuario.Id;
                tipoIrre.Delete();

                PopulaLBXTipo();
                txtTipoIrregu.Text = string.Empty;
                btnExcluir.Enabled = false;

                StringBuilder st = new StringBuilder();

                st.Append("window.opener.document.getElementById('txtAuxiliar').value='atualizaTipoIrregularidade';");
                st.Append("window.opener.document.forms[0].submit();");
				st.Append("window.alert('O Tipo da Irregularidade foi deletado com sucesso!');");

				this.ClientScript.RegisterStartupScript(this.GetType(), "DeletarIrreg", st.ToString(), true);
			}
			catch (Exception ex)
			{
                MsgBox1.Show("Ilitera.Net", ex.Message, null,
                new EO.Web.MsgBoxButton("OK"));

			}
		}
	}
}
