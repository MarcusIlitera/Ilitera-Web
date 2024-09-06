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
using Ilitera.PPR.Report;
using Ilitera.Sied.Report;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.IO;



namespace Ilitera.Net
{
	/// <summary>
	/// 
	/// </summary>
	public partial class PPR : System.Web.UI.Page
	{
		protected void Page_Load(object sender, System.EventArgs e)
		{
            //InicializaWebPageObjects();
            string xUsuario = Session["usuarioLogado"].ToString();

            try
			{				
				LaudoTecnico laudo = new LaudoTecnico(Convert.ToInt32(Request["IdLaudoTecnico"]));
                //Cliente xCliente = new Cliente(  System.Convert.ToInt32( Session["Empresa"].ToString()));


                int xCronog = 0;

                ArrayList listCronograma = new CronogramaPPR().Find("nID_LAUD_TEC=" + laudo.Id);

                if (listCronograma.Count > 0)
                {
                    xCronog = 1;

                }
                else
                {
                    xCronog = 0;
                }


                CrystalDecisions.CrystalReports.Engine.ReportClass[]
                                   reports = new CrystalDecisions.CrystalReports.Engine.ReportClass[2+xCronog];

                
                int i = 0;
         
                reports[i++] = new DataSourcePPR(laudo).GetReport();

                reports[i++] = new DataSourcePlanilhaA4PPRA_PPR(laudo).GetReport();

                if (xCronog == 1)
                {
                    reports[i++] = new Ilitera.PPR.Report.DataSourcePPR(laudo).GetReportCronogramaPPR();
                }



                //RptPCA report = new DataSourcePCA(xCliente).GetReport();
                //RptQuadroEPI_2 report = new DataSourceQuadroEPI_2(laudo).GetReport();

                Funcionalidade funcionalidade = Funcionalidade.GetFuncionalidade(FuncionalidadeTipo.Relatorio, reports.GetType());
                //ImpressaoDocUsuario.InsereRegistro(usuario, cliente, Convert.ToInt64(report.SummaryInfo.KeywordsInReport), true, funcionalidade);

				CreatePDFMerged(reports, this.Response,"",false);

                reports = null;
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();



            }
            catch (Exception ex)
			{
                MsgBox1.Show("Ilitera.Net", ex.Message, null,
                       new EO.Web.MsgBoxButton("OK"));

			}
		}

        protected static void CreatePDFMerged(CrystalDecisions.CrystalReports.Engine.ReportClass[] reports, HttpResponse response, string watermark, bool RenumerarPaginas)
        {
            Stream[] streams = new Stream[reports.Length];

            int i = 0;

            foreach (CrystalDecisions.CrystalReports.Engine.ReportClass report in reports)
            {
                if (RenumerarPaginas)
                    report.ReportDefinition.ReportObjects["PaginaNdeN1"].ObjectFormat.EnableSuppress = true;

                streams[i] = report.ExportToStream(ExportFormatType.PortableDocFormat);

                report.Close();

                i++;
            }

            CreatePDFMerged(streams, response, watermark, RenumerarPaginas);
        }

        protected static void CreatePDFMerged(Stream[] streams, HttpResponse response, string watermark, bool RenumerarPaginas)
        {
            MemoryStream reportStream = PdfSharpUtility.MergeReports(streams, string.Empty, watermark, RenumerarPaginas);

            ShowPdfDocument(response, reportStream);
        }

        protected static void ShowPdfDocument(HttpResponse response, MemoryStream reportStream)
        {
            response.Clear();
            response.ContentType = "application/pdf";
            response.AddHeader("content-length", reportStream.Length.ToString());
            response.BinaryWrite(reportStream.ToArray()); ;
            response.Flush();
            reportStream.Close();
            response.End();
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
			this.ID = "QuadroEPI";

		}
		#endregion
	}
}
