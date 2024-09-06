using System;
using System.Data;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using Ilitera.Common;
using Ilitera.Opsa.Data;
using System.Globalization;
using System.Collections;
//using MestraNET;
using System.Web;
using System.IO;

namespace Ilitera.Net
{
    public partial class ExameDigitalizado : System.Web.UI.Page
    {
        
        protected System.Web.UI.HtmlControls.HtmlTableCell TDGridListaEmpresa;
		private ProntuarioDigital prontuarioDigital;
		private string tipo;

        protected Prestador prestador = new Prestador();
        protected Usuario usuario = new Usuario();
        protected Cliente cliente = new Cliente();
        protected Empregado empregado = new Empregado();
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			InicializaWebPageObjects();

            string xUsuario = Session["usuarioLogado"].ToString();

            if (!IsPostBack)
			{

                string zAcao = Request["Acao"].ToString().Trim();

                if (zAcao == "E")
                {
                    btnGravar.Visible = true;
                    btnExcluir.Visible = true;
                }
                else
                {
                    btnGravar.Visible = false;
                    btnExcluir.Visible = false;
                }

				RegisterClientCode();
				if (prontuarioDigital.Id != 0)
                    PopulaTelaExameDigitalizado();
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
        
        protected void InicializaWebPageObjects()
		{
			//base.InicializaWebPageObjects();
			
			prontuarioDigital = new ProntuarioDigital();
			
			if (Request["IdExame"] != null && Request["IdExame"] != "")
            {
				prontuarioDigital.Find(Convert.ToInt32(Request["IdExame"]));
				tipo = "atualizado";
			}
			else if (ViewState["IdExame"] != null)
			{
				prontuarioDigital.Find(Convert.ToInt32(ViewState["IdExame"]));
				tipo = "atualizado";
			}
			else
			{
				prontuarioDigital.Inicialize();
				prontuarioDigital.IdEmpregado = empregado;
				prontuarioDigital.IdDigitalizador = prestador;
				tipo = "cadastrado";
				btnExcluir.Enabled = false;
				lkbVisualizar.Enabled = false;
			}

            txt_Arq.Text = prontuarioDigital.Arquivo;
		}

		private void RegisterClientCode()
		{
			btnExcluir.Attributes.Add("onClick" ,"javascript:return(confirm('Deseja realmente excluir este Exame Digitalizado?'))");
		}

		

		private void PopulaExameDigitalizado()
		{
            System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");

			prontuarioDigital.DataProntuario = System.Convert.ToDateTime( wdtDataDocumento.Text, ptBr );
			prontuarioDigital.DataDigitalizacao = System.Convert.ToDateTime( wdtDataDigitalizacao.Text, ptBr );
			prontuarioDigital.IndTipoDocumento = Convert.ToInt32(rblTipoExame.SelectedValue);
			prontuarioDigital.Descricao = txtDescricao.Text.Trim();
		}

		public void PopulaTelaExameDigitalizado()
		{
			wdtDataDigitalizacao.Text = prontuarioDigital.DataDigitalizacao.ToString("dd/MM/yyyy" );
            wdtDataDocumento.Text = prontuarioDigital.DataProntuario.ToString("dd/MM/yyyy");
            txtDescricao.Text = prontuarioDigital.Descricao;

			rblTipoExame.ClearSelection();
			rblTipoExame.Items.FindByValue(prontuarioDigital.IndTipoDocumento.ToString()).Selected = true;

			txtDescricao.Text = prontuarioDigital.Descricao;
		}

		protected void btnGravar_Click(object sender, System.EventArgs e)
		{
            try
            {
                //PopulaExameDigitalizado();
                //UploadFoto();

                //prontuarioDigital.UsuarioId = usuario.Id;
                //prontuarioDigital.Save();

                string xNomeArq = File1.FileName.Trim();

                Entities.Usuario usuario = (Entities.Usuario)Session["usuarioLogado"];

                string xArq = "";


                ProntuarioDigital xProntuario = new ProntuarioDigital();
                if (Request["IdExame"] != null && Request["IdExame"] != "")
                {
                    xProntuario.Find(Convert.ToInt32(Request["IdExame"]));
                    tipo = "atualizado";
                }

                xProntuario.Descricao = txtDescricao.Text;


                //salvar prontuario
                if (xNomeArq != string.Empty)
                {


                    string xExtension = xNomeArq.Substring(xNomeArq.Length - 3, 3).ToUpper().Trim();

                    if (xExtension == "PDF" || xExtension == "JPG")
                    {

                        Cliente xCliente = new Cliente();
                        xCliente.Find(System.Convert.ToInt32(Request["IdEmpresa"].ToString()));



                        //string xArq = "C:\\DRIVE_I\\fotosdocsdigitais\\" + xCliente.DiretorioPadrao.ToString().ToUpper().Trim() + "\\Prontuario\\" + File1.FileName;

                        // if (Session["Servidor_Web"].ToString().Trim().ToUpper() == "ILITERA")
                        xArq = "I:\\fotosdocsdigitais\\" + xCliente.DiretorioPadrao.ToString().ToUpper().Trim() + "\\Prontuario\\" + xNomeArq;
                        // else
                        //     xArq = "C:\\DRIVE_I\\fotosdocsdigitais\\" + xCliente.DiretorioPadrao.ToString().ToUpper().Trim() + "\\Prontuario\\" + xNomeArq;

                        File1.SaveAs(xArq);

                        xArq = "I:\\fotosdocsdigitais\\" + xCliente.DiretorioPadrao.ToString().ToUpper().Trim() + "\\Prontuario\\" + xNomeArq;


                        ExameBase xExameBase = new ExameBase();

                        Empregado xEmpregado = new Empregado();
                        xEmpregado.Find(System.Convert.ToInt32(Request["IdEmpregado"].ToString()));

                        Entities.Usuario zusuario = (Entities.Usuario)Session["usuarioLogado"];
                        prestador.Find(" IdPessoa = " + zusuario.IdPessoa.ToString());


                        System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");

                        xProntuario.IdExameBase = xExameBase;
                        xProntuario.IdEmpregado = xEmpregado;
                        xProntuario.DataProntuario = System.Convert.ToDateTime(wdtDataDocumento.Text, ptBr);
                        xProntuario.Arquivo = xArq; //searchFile.Value.ToString();
                        xProntuario.DataDigitalizacao = System.DateTime.Now;
                        xProntuario.IdDigitalizador = prestador;

                    }
                    
                }

                string xExtension2 = xNomeArq.Substring(xNomeArq.Length - 3, 3).ToUpper().Trim();

                if (xExtension2 == "PDF" || xExtension2 == "JPG")
                {
                    xProntuario.Save();
                }


                btnExcluir.Enabled = true;
                lkbVisualizar.Enabled = true;

                ViewState["IdExameDigitalizado"] = prontuarioDigital.Id;

                StringBuilder st = new StringBuilder();

                st.Append("window.opener.document.getElementById('txtAuxiliar').value='atualiza';");
                st.Append("window.opener.document.forms[0].submit();");
                st.Append("window.alert('O Exame Digitalizado foi " + tipo + " com sucesso!');");

                this.ClientScript.RegisterStartupScript(this.GetType(), "GravarExameDigitalizado", st.ToString(), true);

                Response.Write("<script>window.opener.document.forms[0].submit(); window.close();</script>");

                Entities.Usuario user = (Entities.Usuario)Session["usuarioLogado"];
                Response.Redirect("DadosEmpregado.aspx?IdEmpregado=" + Session["Empregado"].ToString() + "&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString() + "&Tipo=6");

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
				
				prontuarioDigital.UsuarioId = usuario.Id;
				prontuarioDigital.Delete();

				ViewState["IdExameDigitalizado"] = null;

				StringBuilder st = new StringBuilder();

				st.Append("window.opener.document.getElementById('txtAuxiliar').value='atualiza';");
				st.Append("window.opener.document.forms[0].submit();");
				st.Append("window.alert('O Exame Digitalizado foi excluído com sucesso!');");
				st.Append("self.close();");

                this.ClientScript.RegisterStartupScript(this.GetType(), "ExcluirExameDigitalizado", st.ToString(), true);

                Response.Write("<script>window.opener.document.forms[0].submit(); window.close();</script>");

                Entities.Usuario user = (Entities.Usuario)Session["usuarioLogado"];
                Response.Redirect("DadosEmpregado.aspx?IdEmpregado=" + Session["Empregado"].ToString() + "&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString() + "&Tipo=6");

			}
			catch (Exception ex)
			{
                MsgBox1.Show("Ilitera.Net", ex.Message, null,
                       new EO.Web.MsgBoxButton("OK"));

			}
		}

		protected void lkbVisualizar_Click(object sender, System.EventArgs e)
		{
			Guid strAux = Guid.NewGuid();

            Funcionalidade funcionalidade = Funcionalidade.GetFuncionalidade(FuncionalidadeTipo.Metodo, this.GetType(), "lkbVisualizar");
            ImpressaoDocUsuario.InsereRegistro(usuario, cliente, 0L, true, funcionalidade);
			
			StringBuilder st = new StringBuilder();

			st.Append("window.open('" + Fotos.UrlFoto(prontuarioDigital.GetArquivo(cliente)) + "?IliteraSystem=" + strAux.ToString() + strAux.ToString()
				+"', 'ExameDigitalizado', 'scrollbars=yes, resizable=yes, toolbar=no, addressbar=no, menubar=no, width=800px, height=600px');");

            this.ClientScript.RegisterStartupScript(this.GetType(), "AbreExameDigitalizado", st.ToString(), true);
		}

        protected void cmd_Voltar_Click(object sender, EventArgs e)
        {
            Entities.Usuario user = (Entities.Usuario)Session["usuarioLogado"];
            Response.Redirect("DadosEmpregado.aspx?IdEmpregado=" + Session["Empregado"].ToString() + "&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString() + "&Tipo=6");
        }
	}
}
