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
	public partial class CadEquipamento : System.Web.UI.Page
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
				PopulaLsbEquipamento();
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
			btnExcluir.Attributes.Add("onClick" ,"javascript:return confirm('Você deseja realmente excluir este Equipamento?');");
		}

		private void PopulaLsbEquipamento()
		{
			lsbEquipamentos.DataSource = new Equipamento().Get("IdCliente=" + lbl_Id_Empresa.Text + " ORDER BY Nome");
			lsbEquipamentos.DataValueField = "IdEquipamento";
			lsbEquipamentos.DataTextField = "Nome";
			lsbEquipamentos.DataBind();

			lsbEquipamentos.Items.Insert(0, new ListItem("Cadastrar Novo Equipamento...", "0"));
			lsbEquipamentos.Items[0].Selected = true;
		}

		protected void lsbEquipamentos_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (lsbEquipamentos.SelectedValue.Equals("0"))
			{
				txtNome.Text = string.Empty;
				txtDescricao.Text = string.Empty;
				btnExcluir.Enabled = false;
			}
			else
			{
				Equipamento equipamento = new Equipamento(Convert.ToInt32(lsbEquipamentos.SelectedValue));

				txtNome.Text = equipamento.Nome;
				txtDescricao.Text = equipamento.Descricao;

				btnExcluir.Enabled = true;
			}
		}

		protected void btnGravar_Click(object sender, System.EventArgs e)
		{
			try
			{
				Equipamento equipamento = new Equipamento();
				//string tipoProcesso;

                Cliente xCliente;

                xCliente = new Cliente(System.Convert.ToInt32(lbl_Id_Empresa.Text));

				if (lsbEquipamentos.SelectedValue.Equals("0"))
				{
					//tipoProcesso = "cadastrado";
					equipamento.Inicialize();
					equipamento.IdCliente = xCliente;
				}
				else
				{
					//tipoProcesso = "editado";
					equipamento.Find(Convert.ToInt32(lsbEquipamentos.SelectedValue));
				}

				equipamento.Nome = txtNome.Text.Trim();
				equipamento.Descricao = txtDescricao.Text.Trim();

				equipamento.UsuarioId = System.Convert.ToInt32( lbl_Id_Usuario.Text);
				equipamento.Save();
				
				PopulaLsbEquipamento();
				lsbEquipamentos.ClearSelection();
				lsbEquipamentos.Items.FindByValue(equipamento.Id.ToString()).Selected = true;

				btnExcluir.Enabled = true;

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


                this.ClientScript.RegisterStartupScript(this.GetType(), "AtualizaEquipamento", st.ToString(),true);



                //StringBuilder st = new StringBuilder();

                ////st.Append("window.opener.document.getElementById('txtAuxiliar').value='atualizaEquipamento';");
                //st.Append("window.opener.document.forms[0].submit();");
                //st.Append("window.alert('O Equipamento foi " + tipoProcesso + " com sucesso!');");

                //this.ClientScript.RegisterStartupScript(this.GetType(), "AtualizaEquipamento", st.ToString(), true);
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
				DataSet dsProcedimentoEquipamento = new ProcedimentoEquipamento().Get("IdEquipamento=" + lsbEquipamentos.SelectedValue);
				if (dsProcedimentoEquipamento.Tables[0].Rows.Count > 0)
					throw new Exception("Não é possível excluir este Equipamento! Ele está sendo utilizado em um ou mais procedimentos!");

				Equipamento equipamento = new Equipamento(Convert.ToInt32(lsbEquipamentos.SelectedValue));

				equipamento.UsuarioId = System.Convert.ToInt32( lbl_Id_Usuario.Text);
				equipamento.Delete();

				PopulaLsbEquipamento();
				txtNome.Text = string.Empty;
				txtDescricao.Text = string.Empty;

				btnExcluir.Enabled = false;

                //StringBuilder st = new StringBuilder();

                ////st.Append("window.opener.document.getElementById('txtAuxiliar').value='atualizaEquipamento';");
                ////st.Append("window.opener.document.forms[0].submit();");
                //st.Append("window.alert('O Equipamento " + equipamento.Nome + " foi excluído com sucesso!');");

                //this.ClientScript.RegisterStartupScript(this.GetType(), "ExcluiEquipamento", st.ToString(), true);


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


                this.ClientScript.RegisterStartupScript(this.GetType(), "ExcluiEquipamento", st.ToString());


			}
			catch(Exception ex)
			{
                MsgBox1.Show("Ilitera.Net", ex.Message, null,
                       new EO.Web.MsgBoxButton("OK"));

			}
		}
	}
}
