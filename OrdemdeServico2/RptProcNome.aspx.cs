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
using Ilitera.OrdemServico.Report;
using Ilitera.Common;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;


namespace Ilitera.Net.OrdemDeServico
{
	/// <summary>
	///
	/// </summary>
	public partial class RptProcNome : System.Web.UI.Page
	{

        protected Cliente cliente = new Cliente();

		protected void Page_Load(object sender, System.EventArgs e)
		{
			//InicializaWebPageObjects();

            Cliente xCliente = new Cliente();
            xCliente.Find(System.Convert.ToInt32(Request["IdEmpresa"].ToString()));

            string xUsuario = Session["usuarioLogado"].ToString();


            try
			{
				RptListaProcedimentos report = new DataSourceListaProcedimento(xCliente, false).GetReportListaProcedimento();

                Funcionalidade funcionalidade = Funcionalidade.GetFuncionalidade(FuncionalidadeTipo.Relatorio, report.GetType(), string.Empty, "PorNome");
                //ImpressaoDocUsuario.InsereRegistro(usuario, cliente, Convert.ToInt64(report.SummaryInfo.KeywordsInReport), true, funcionalidade);
				
				CreatePDFDocument(report, this.Response);
			}
			catch(Exception ex)
			{
                MsgBox1.Show("Ilitera.Net", ex.Message, null,
                       new EO.Web.MsgBoxButton("OK"));

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
			this.ID = "RptProcNome";

		}
		#endregion
	}
}
