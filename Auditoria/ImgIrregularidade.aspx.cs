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
	public partial class ImgIrregularidade : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label lblEmpresa;
		
		protected void Page_Load(object sender, System.EventArgs e)
		{
            string xUsuario = Session["usuarioLogado"].ToString();
            imgIrregula.ImageUrl = Request["PathImg"];
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
			this.ID = "ImgIrregularidade";

		}
		#endregion

	}
}
