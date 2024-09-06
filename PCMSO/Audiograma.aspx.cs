
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using Entities;
using BLL;
using Ilitera.Opsa.Data;
using Ilitera.Common;
using Ilitera.PCMSO.Report;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;



namespace Ilitera.Net.PCMSO
{
    public partial class Audiograma : System.Web.UI.Page
    {

		protected void Page_Load(object sender, System.EventArgs e)
		{
            //InicializaWebPageObjects();
            string xUsuario = Session["usuarioLogado"].ToString();

            try
			{
                Audiometria audiometria = new Audiometria(Convert.ToInt32(Request["IdExame"]));

				RptAudiometria report = new DataSourceAudiometria(audiometria).GetReport(true);

                Funcionalidade funcionalidade = Funcionalidade.GetFuncionalidade(FuncionalidadeTipo.Relatorio, report.GetType());
                //ImpressaoDocUsuario.InsereRegistro(usuario, cliente, Convert.ToInt64(report.SummaryInfo.KeywordsInReport), true, funcionalidade);

                CreatePDFDocument(report, this.Response);
			}
			catch (Exception ex)
			{
                System.Diagnostics.Debug.WriteLine(ex.Message);
                //Aviso(ex.Message, true);
            }
		}

        protected static void CreatePDFDocument(ReportClass report, HttpResponse response)
        {
            CreatePDFDocument(report, response, false, string.Empty);
        }

        protected static void CreatePDFDocument(ReportClass report, HttpResponse response, bool forceDownload, string DownloadfileName)
        {
            report.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, response, forceDownload, DownloadfileName);
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
			this.ID = "Audiograma";

		}
		#endregion
	}
}
