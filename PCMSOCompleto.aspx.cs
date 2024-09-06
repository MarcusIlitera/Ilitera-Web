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
using Ilitera.PCMSO.Report;
using Ilitera.Sied.Report;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.IO;
using System.Collections.Generic;

namespace Ilitera.Net
{
	/// <summary>
	/// 
	/// </summary>
    public partial class PCMSOCompleto : System.Web.UI.Page
	{

        protected Usuario usuario = new Usuario();
        protected Cliente cliente = new Cliente();


		protected void Page_Load(object sender, System.EventArgs e)
		{
			//InicializaWebPageObjects();

            string zMedicos = Request["Medicos"];
            string zCronograma = Request["Crono"];
            string zPreposto = Request["Preposto"];

            string xUsuario = Session["usuarioLogado"].ToString();

            try
			{
                Pcmso pcmso = new Pcmso();
                pcmso.Find(Convert.ToInt32(Request["IdPcmso"]));

                List<PCMSO_Anexo> rAnexos = new PCMSO_Anexo().Find<PCMSO_Anexo>(" IdPcmso = " + pcmso.Id.ToString() + " order by Arquivo ");
                int zAnexo = 0;
                int zCrono = 0;

                if (rAnexos.Count > 0) zAnexo = 1;

                if (zCronograma == "S") zCrono = 1;



                PcmsoDocumento pcmsodocumento = new PcmsoDocumento();

                if (!pcmsodocumento.VerificaExisteDocumento(pcmso.Id))
                    throw new Exception("As Normas Gerais de Ação ainda não foram configuradas no PCMSO!");
                
                ReportClass[] reports = new ReportClass[2 + zAnexo + zCrono];

                if (zPreposto == "0")
                {
                    reports[0] = new DataSourcePCMSO(pcmso).GetReport(zMedicos);
                }
                else
                {
                    reports[0] = new DataSourcePCMSO(pcmso).GetReport_Preposto(zMedicos, System.Convert.ToInt32( zPreposto) );
                }

                if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().IndexOf("Prajna") > 0)
                {
                    reports[1] = new DataSourcePlanilhaA4PCMSO(pcmso).GetReport_Prajna();
                }
                else
                {

                    Ilitera.Data.Clientes_Clinicas xExames = new Ilitera.Data.Clientes_Clinicas();
                    Int32 xTotExames = xExames.Retorna_Exames_PCMSO_GHE(pcmso.Id);

                    if (xTotExames < 20)
                    {
                        reports[1] = new DataSourcePlanilhaA4PCMSO(pcmso).GetReport();
                    }
                    else
                    {
                        reports[1] = new DataSourcePlanilhaA4PCMSO(pcmso).GetReport_Prajna();
                    }                    
                }


                if (zCronograma == "S")
                {
                    reports[2] = new DataSourceQuadroPCMSO(pcmso).GetReportCronogramaPGR();

                    if (zAnexo == 1)
                    {
                        reports[3] = new DataSourcePPRAAnexo().GetReportPCMSO(pcmso.Id);
                    }
                }
                else
                {
                    if (zAnexo == 1)
                    {
                        reports[2] = new DataSourcePPRAAnexo().GetReportPCMSO(pcmso.Id);
                    }
                }


                /*Código comentado para que o relatório não imprima as páginas que não foram tratadas até o momento - Início*/
                //reports[2] = new DataSourceRelatorioAnual(pcmso).GetReport();
                //reports[3] = new DataSourceExamePlanejamento(pcmso).GetReportExamePorEmpregado();
                //reports[4] = new DataSourceExamePlanejamento(pcmso).GetReportExamePorData();
                /*Código comentado para que o relatório não imprima as páginas que não foram tratadas até o momento - Fim*/

                Funcionalidade funcionalidade = Funcionalidade.GetFuncionalidade(FuncionalidadeTipo.Relatorio, reports[0].GetType(), string.Empty, "PCMSO Completo");
                
                /*Código comentado para que o relatório não imprima as páginas que não foram tratadas até o momento - Início*/
                //ImpressaoDocUsuario.InsereRegistro(usuario, cliente, Convert.ToInt64(reports[0].SummaryInfo.KeywordsInReport) + Convert.ToInt64(reports[1].SummaryInfo.KeywordsInReport) +
                    //Convert.ToInt64(reports[2].SummaryInfo.KeywordsInReport) + Convert.ToInt64(reports[3].SummaryInfo.KeywordsInReport) + Convert.ToInt64(reports[4].SummaryInfo.KeywordsInReport), 
                    //true, funcionalidade);
                /*Código comentado para que o relatório não imprima as páginas que não foram tratadas até o momento - Fim*/

                //ImpressaoDocUsuario.InsereRegistro(usuario, cliente, Convert.ToInt64(reports[0].SummaryInfo.KeywordsInReport) + Convert.ToInt64(reports[1].SummaryInfo.KeywordsInReport)
                //    , true, funcionalidade);

				CreatePDFMerged(reports, this.Response, string.Empty, true);

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
			this.ID = "PPRAIntroducao";

		}
		#endregion
	}
}
