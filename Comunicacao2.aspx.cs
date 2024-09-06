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
using Ilitera.Data;
using Ilitera.Common;
using Ilitera.Sied.Report;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Ilitera.PCMSO.Report;
using System.Text;
//using Ilitera.Net.Documentos;
using System.Configuration;

using System.IO;
using System.Net;
using System.Xml;
using System.Xml.Linq;


namespace Ilitera.Net
{
    /// <summary>
    /// 
    /// </summary>
    public partial class Comunicacao2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, System.EventArgs e)
        {
            //InicializaWebPageObjects();

            Ilitera.Data.SQLServer.EntitySQLServer.xDB1 = ConfigurationManager.AppSettings["DB1"].ToString();
            Ilitera.Data.SQLServer.EntitySQLServer.xDB2 = ConfigurationManager.AppSettings["DB2"].ToString();
            Ilitera.Data.SQLServer.EntitySQLServer._LocalServer = ConfigurationManager.AppSettings["LocalServer"].ToString();


            //string rSelect = "";

            try
            {
                txt_Status.Text = Session["zErro"].ToString();
            }
            catch
            {
                txt_Status.Text = "";
            }

            if (txt_Status.Text.Trim() != "") return;

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

