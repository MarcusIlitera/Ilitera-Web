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
using Ilitera.PCMSO.Report;
using System.Text;
using System.Net;
//using Ilitera.Net.Documentos;

namespace Ilitera.Net
{
	/// <summary>
	/// 
	/// </summary>
	public partial class help : System.Web.UI.Page
	{
		protected void Page_Load(object sender, System.EventArgs e)
		{
			//InicializaWebPageObjects();

			try
			{
                //string path = Server.MapPath(Session["Ajuda"].ToString());
                string path = Session["Ajuda"].ToString();

                String strPathAndQuery = HttpContext.Current.Request.Url.PathAndQuery;
                //String strUrl = HttpContext.Current.Request.Url.AbsoluteUri.Replace(strPathAndQuery, "/");

                string xPath = "";
                xPath = HttpContext.Current.Request.Url.AbsoluteUri;

                try
                {

                    xPath = xPath.Replace(HttpContext.Current.Request.Url.Segments[2].ToString(), "");
                    xPath = xPath.Replace(HttpContext.Current.Request.Url.Segments[3].ToString(), "");
                    xPath = xPath.Replace(HttpContext.Current.Request.Url.Segments[4].ToString(), "");
                    xPath = xPath.Replace(HttpContext.Current.Request.Url.Segments[5].ToString(), "");

                }
                catch
                {

                }

                xPath = xPath + path.Substring(2);



                WebClient client = new WebClient();
                //Byte[] buffer = client.DownloadData(HttpContext.Current.Request.Url.AbsoluteUri.Replace(strPathAndQuery, "/") + path.Substring(2));
                Byte[] buffer = client.DownloadData(xPath);
                if (buffer != null)
                {
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-length", buffer.Length.ToString());
                    Response.BinaryWrite(buffer);
                }

			}
			catch(Exception ex)
			{
                MsgBox1.Show("Ilitera.Net", ex.Message, null,
                       new EO.Web.MsgBoxButton("OK"));

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
	}
}
