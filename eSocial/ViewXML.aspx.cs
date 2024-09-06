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
using Ilitera.Opsa.Data;
using Ilitera.Common;
using Ilitera.Sied.Report;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Text;


namespace Ilitera.Net
{
	/// <summary>
	/// 
	/// </summary>
	public partial class ViewXML : System.Web.UI.Page
	{
        

		protected void Page_Load(object sender, System.EventArgs e)
		{

            string xmlContent = string.Empty;
            if (Session["XML"] != null)
                xmlContent = Session["XML"].ToString();

            Response.Clear();
            Response.ContentType = "application/xml";
            Response.Write(xmlContent);
            Response.Flush();
            Response.End();
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
	}
}
