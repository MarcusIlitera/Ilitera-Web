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
using Ilitera.Opsa.Report;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
//using MestraNET;

namespace Ilitera.Net.ControleEPI
{
    /// <summary>
	/// Summary description.
	/// </summary>
	
	public partial class RelatorioComplEntEPI : System.Web.UI.Page
	{

        protected Cliente cliente = new Cliente();
        //protected Ilitera.Opsa.Data.Empregado empregado = new Ilitera.Opsa.Data.Empregado();
        protected Ilitera.Common.Usuario usuario = new Ilitera.Common.Usuario();

		protected void Page_Load(object sender, System.EventArgs e)
		{
            //InicializaWebPageObjects();
            string xUsuario = Session["usuarioLogado"].ToString();

            try
			{
                System.TimeSpan xTime = new System.TimeSpan(1,50,50 );
                Page.AsyncTimeout = xTime;

                //empregado.Find(System.Convert.ToInt32(Request["IdEmpregado"].ToString()));

                RptEntregaEPIEmpr report = new DataSourceEPI().GetReportEntregaEPIEmpr(System.Convert.ToInt32(Request["IdEmpregado"].ToString()), Request["D1"].ToString(), Request["D2"].ToString());

                Funcionalidade funcionalidade = Funcionalidade.GetFuncionalidade(FuncionalidadeTipo.Relatorio, report.GetType());
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
		}
		#endregion
	}
}
