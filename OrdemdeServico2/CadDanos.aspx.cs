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
using System.Text;
using Ilitera.Opsa.Data;


namespace Ilitera.Net.OrdemDeServico
{
	/// <summary>
    /// Summary description for CadDanos.
	/// </summary>
    public partial class CadDanos : System.Web.UI.Page
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
		

            Entities.Usuario user = (Entities.Usuario)Session["usuarioLogado"];

            int xIdEmpresa = System.Convert.ToInt32(Session["Empresa"]);
            int xIdUsuario = user.IdUsuario;

            lbl_Id_Empresa.Text = xIdEmpresa.ToString().Trim();
            lbl_Id_Usuario.Text = xIdUsuario.ToString().Trim();


			if(!IsPostBack)
			{
				PopulaLsbDanos();
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
            lsbDanos.Attributes.Add("onClick", "javascript:setNome(this.options[this.selectedIndex].text);");
			btnExcluir.Attributes.Add("onClick" ,"javascript:return confirm('Você deseja realmente excluir este Dano?');");
		}

        private void PopulaLsbDanos()
		{			
			lsbDanos.DataSource = new Dano().GetIdNome("Nome", "IdCliente=" + lbl_Id_Empresa.Text);
            lsbDanos.DataValueField = "Id";
            lsbDanos.DataTextField = "Nome";
            lsbDanos.DataBind();

            lsbDanos.Items.Insert(0, new ListItem("Cadastro de um Novo Dano...", "0"));
            lsbDanos.Items[0].Selected = true;
		}

		protected void btnGravar_Click(object sender, System.EventArgs e)
		{
            try
            {
                Dano dano = new Dano();
                string tipoProcess = string.Empty;

                Cliente xCliente;

                xCliente = new Cliente(System.Convert.ToInt32(lbl_Id_Empresa.Text));

                if (lsbDanos.SelectedValue.Equals("0"))
                {
                    tipoProcess = "cadastrado";
                    dano.Inicialize();
                    dano.IdCliente = xCliente;
                }
                else
                {
                    tipoProcess = "editado";
                    dano.Find(Convert.ToInt32(lsbDanos.SelectedValue));
                }

                dano.Nome = txtNome.Text.Trim();

                dano.UsuarioId = System.Convert.ToInt32( lbl_Id_Usuario.Text);
                dano.Save();

                PopulaLsbDanos();
                lsbDanos.ClearSelection();
                lsbDanos.Items.FindByValue(dano.Id.ToString()).Selected = true;
                btnExcluir.Enabled = true;

                //StringBuilder st = new StringBuilder();

                ////st.Append("window.opener.document.getElementById('txtAuxiliar').value='atualizaDanos';");
                ////st.Append("window.opener.document.forms[0].submit();");
                //st.Append("window.alert('O Dano foi " + tipoProcess + " com sucesso!');");

                //this.ClientScript.RegisterStartupScript(this.GetType(), "SaveDano", st.ToString(), true);

                StringBuilder st = new StringBuilder();

                //st.Append("window.opener.document.getElementById('txtAuxiliar').value='atualizaCelula';");
                //st.Append("window.opener.document[0]['txtAuxiliar']='atualizaCelula';"); ;   // por que não funciona ??? - Wagner
                //st.Append("window.opener.document.forms[0].submit();");

                Session["txtAuxiliar"] = "atualizaDanos";

                //st.Append("<script language='javascript'>");
                st.Append("window.opener.location.reload();");// refreshes main window (normally postback)
                st.Append("self.close();");//closes the pop up
                st.Append("</script>");
                //Page.RegisterStartUpScript("PopUpClose",st.ToString());


                this.ClientScript.RegisterStartupScript(this.GetType(), "SaveDano", st.ToString(),true);


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
                DataSet dsDanoRisco = new OperacaoPerigoRiscoDano().Get("IdDano=" + lsbDanos.SelectedValue);
                if (dsDanoRisco.Tables[0].Rows.Count > 0)
                    throw new Exception("Não é possível excluir este Dano! O Dano está associado a um ou mais Riscos!");

                Dano dano = new Dano(Convert.ToInt32(lsbDanos.SelectedValue));

                dano.UsuarioId = System.Convert.ToInt32( lbl_Id_Usuario.Text);
                dano.Delete();

                txtNome.Text = string.Empty;
                btnExcluir.Enabled = false;
                PopulaLsbDanos();

                //StringBuilder st = new StringBuilder();

                ////st.Append("window.opener.document.getElementById('txtAuxiliar').value='atualizaDanos';");
                ////st.Append("window.opener.document.forms[0].submit();");
                //st.Append("window.alert('O Dano foi excluído com sucesso!');");

                //this.ClientScript.RegisterStartupScript(this.GetType(), "ExcluiDano", st.ToString(), true);

                StringBuilder st = new StringBuilder();

                //st.Append("window.opener.document.getElementById('txtAuxiliar').value='atualizaCelula';");
                //st.Append("window.opener.document[0]['txtAuxiliar']='atualizaCelula';"); ;   // por que não funciona ??? - Wagner
                //st.Append("window.opener.document.forms[0].submit();");

                Session["txtAuxiliar"] = "atualizaDanos";

                //st.Append("<script language='javascript'>");
                st.Append("window.opener.location.reload();");// refreshes main window (normally postback)
                st.Append("self.close();");//closes the pop up
                st.Append("</script>");
                //Page.RegisterStartUpScript("PopUpClose",st.ToString());


                this.ClientScript.RegisterStartupScript(this.GetType(), "ExcluiDano", st.ToString());


            }
            catch (Exception ex)
            {
                MsgBox1.Show("Ilitera.Net", ex.Message, null,
                       new EO.Web.MsgBoxButton("OK"));

                btnExcluir.Enabled = true;
            }
		}
	}
}
