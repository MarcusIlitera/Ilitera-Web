using System;
using System.Data;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Globalization;
using System.Text;
using System.Collections;

using Ilitera.Data;
using Ilitera.Common;
using Ilitera.Opsa.Data;



namespace Ilitera.Net
{
	/// <summary>
	/// Summary description for WebForm1.
	/// </summary>
    public partial class LoginRestrito : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.LinkButton lkbChangePass;

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

		protected void Page_Load(object sender, System.EventArgs e)
		{
            //InicializaWebPageObjects();
            string xUsuario = Session["usuarioLogado"].ToString();
        }

		protected void btnLogin_Click(object sender, System.EventArgs e)
		{
			try
			{
				Login();

                Usuario usuario = new Usuario();
                usuario.Find(System.Convert.ToInt32(Request["IdUsuario"].ToString()));

				Response.Redirect("PageSet.aspx?IdUsuario=" + usuario.Id, true);
			}
			catch(Exception ex)
			{
                System.Diagnostics.Debug.WriteLine(ex.Message);
                //Aviso(ex.Message);
            }
		}

		private void Login()
		{
            Usuario usuario = new Usuario();
            usuario.Find(System.Convert.ToInt32(Request["IdUsuario"].ToString()));

            usuario.LoginWeb(usuario.NomeUsuario, txtSenha.Text, 0, 0);

			if (usuario.Id != 1)
			{
                string criteria = String.Format("IndResponsavelPapel={0}"
                            + " AND IdPrestador IN (SELECT IdJuridicaPessoa FROM JuridicaPessoa WHERE IdPessoa={1})",
                            (int)ResponsavelPapel.AcessoAuditoria,
                            usuario.IdPessoa.Id);

                int count = new Responsavel().ExecuteCount(criteria);

                if (count == 0)
					throw new Exception("O Usuário atual não possui autorização para acessar a Auditoria de Segurança!");
			}
		}
	}
}
