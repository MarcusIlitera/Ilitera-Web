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
using Ilitera.Data;
using Ilitera.Opsa.Data;
using Ilitera.Common;


namespace Ilitera.Net
{
	/// <summary>
	/// Summary description for menu.
	/// </summary>
    public partial class ListaMenu : System.Web.UI.Page
	{

		protected void Page_Load(object sender, System.EventArgs e)
		{
			//PopulaMenuList(UltraWebListbarMenu, (int)IdAplicacao.AuditoriadeSeguranca, Request["menutype"].ToString(), Convert.ToInt32(Request["IdUsuario"]));
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
			this.ID = "SetMenu";

		}
		#endregion
	}
}
