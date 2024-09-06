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
	/// Summary description for CadRiscoAcidente.
	/// </summary>
	public partial class CadConjunto : System.Web.UI.Page
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			//InicializaWebPageObjects();

			if(!IsPostBack)
			{

                Entities.Usuario user = (Entities.Usuario)Session["usuarioLogado"];

                int xIdEmpresa = System.Convert.ToInt32(Session["Empresa"]);
                int xIdUsuario = user.IdUsuario;

                lbl_Id_Empresa.Text = xIdEmpresa.ToString().Trim();
                lbl_Id_Usuario.Text = xIdUsuario.ToString().Trim();


				PopulaLsbConjuntos();
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
			listBxConjuntos.Attributes.Add("onClick", "javascript:setNome(this.options[this.selectedIndex].text);");
			btnExcluir.Attributes.Add("onClick" ,"javascript:return confirm('Você deseja realmente excluir este Conjunto de Procedimentos?');");
		}

		private void PopulaLsbConjuntos()
		{			
			listBxConjuntos.DataSource = new Conjunto().Get("IdCliente=" + lbl_Id_Empresa.Text + " ORDER BY Nome");
			listBxConjuntos.DataValueField = "IdConjunto";
			listBxConjuntos.DataTextField = "Nome";
			listBxConjuntos.DataBind();

			listBxConjuntos.Items.Insert(0, new ListItem("Cadastro de um Novo Conjunto...", "0"));
			listBxConjuntos.Items[0].Selected = true;
		}

		protected void btnGravar_Click(object sender, System.EventArgs e)
		{
			try
			{
				Conjunto conjunto = new Conjunto();
				string tipoProcess = string.Empty;

				if (listBxConjuntos.SelectedValue.Equals("0"))
				{
					tipoProcess = "cadastrado";
					conjunto.Inicialize();

                    Ilitera.Opsa.Data.Cliente xCli;

                    xCli = new Ilitera.Opsa.Data.Cliente(System.Convert.ToInt32(lbl_Id_Empresa.Text));

                    conjunto.IdCliente = xCli;                     //cliente;
				}
				else
				{
					tipoProcess = "editado";
					conjunto.Find(Convert.ToInt32(listBxConjuntos.SelectedValue));
				}

				conjunto.Nome = txtNome.Text.Trim();
				
				conjunto.UsuarioId = System.Convert.ToInt32( lbl_Id_Usuario.Text);
				conjunto.Save();

				PopulaLsbConjuntos();
				listBxConjuntos.ClearSelection();
				listBxConjuntos.Items.FindByValue(conjunto.Id.ToString()).Selected = true;

                //StringBuilder st = new StringBuilder();

                ////st.Append("window.opener.document.getElementById('txtAuxiliar').value='atualizaConjuntos';");
                ////st.Append("window.opener.document.forms[0].submit();");
                //st.Append("window.alert('O Conjunto de Procedimentos foi " + tipoProcess + " com sucesso!');");

                //this.ClientScript.RegisterStartupScript(this.GetType(), "SaveConjunto", st.ToString(), true);

                StringBuilder st = new StringBuilder();

                //st.Append("window.opener.document.getElementById('txtAuxiliar').value='atualizaCelula';");
                //st.Append("window.opener.document[0]['txtAuxiliar']='atualizaCelula';"); ;   // por que não funciona ??? - Wagner
                //st.Append("window.opener.document.forms[0].submit();");

                Session["txtAuxiliar"] = "atualizaConjunto";

                //st.Append("<script language='javascript'>");
                st.Append("window.opener.location.reload();");// refreshes main window (normally postback)
                st.Append("self.close();");//closes the pop up
                st.Append("</script>");
                //Page.RegisterStartUpScript("PopUpClose",st.ToString());


                this.ClientScript.RegisterStartupScript(this.GetType(), "SaveConjunto", st.ToString(),true);


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
				DataSet dsConjuntoEmpregado = new ConjuntoEmpregado().Get("IdConjunto=" + listBxConjuntos.SelectedValue);
				if (dsConjuntoEmpregado.Tables[0].Rows.Count > 0)
					throw new Exception("Não é possível excluir este Conjunto de Procedimentos! O Conjunto está associado a um ou mais Empregados!");

				DataSet dsConjuntoProcedimento = new ConjuntoProcedimento().Get("IdConjunto=" + listBxConjuntos.SelectedValue);
				if (dsConjuntoProcedimento.Tables[0].Rows.Count > 0)
					throw new Exception("Não é possível excluir este Conjunto de Procedimentos! Um ou mais Procedimentos pertencem a esse Conjunto!");

				Conjunto conjunto = new Conjunto(Convert.ToInt32(listBxConjuntos.SelectedValue));

				conjunto.UsuarioId = System.Convert.ToInt32( lbl_Id_Usuario.Text);
				conjunto.Delete();

				txtNome.Text = string.Empty;
				btnExcluir.Enabled = false;
				PopulaLsbConjuntos();

				//StringBuilder st = new StringBuilder();

				//st.Append("window.opener.document.getElementById('txtAuxiliar').value='atualizaConjuntos';");
				//st.Append("window.opener.document.forms[0].submit();");
				//st.Append("window.alert('O Conjunto de Procedimentos foi excluído com sucesso!');");

                //this.ClientScript.RegisterStartupScript(this.GetType(), "ExcluiConjunto", st.ToString(), true);

                StringBuilder st = new StringBuilder();

                //st.Append("window.opener.document.getElementById('txtAuxiliar').value='atualizaCelula';");
                //st.Append("window.opener.document[0]['txtAuxiliar']='atualizaCelula';"); ;   // por que não funciona ??? - Wagner
                //st.Append("window.opener.document.forms[0].submit();");

                Session["txtAuxiliar"] = "atualizaConjunto";

                //st.Append("<script language='javascript'>");
                st.Append("window.opener.location.reload();");// refreshes main window (normally postback)
                st.Append("self.close();");//closes the pop up
                st.Append("</script>");
                //Page.RegisterStartUpScript("PopUpClose",st.ToString());


                this.ClientScript.RegisterStartupScript(this.GetType(), "ExcluiConjunto", st.ToString(),true);



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
