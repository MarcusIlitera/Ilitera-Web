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
using System.IO;

using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

using Ilitera.Data;
using Ilitera.Opsa.Data;
using Ilitera.Common;
using Ilitera.Sied.Report;
using Ilitera.Relatorios.Report;

using Ilitera.PPR.Report;


namespace Ilitera.Net
{
	/// <summary>
	/// 
	/// </summary>
    public partial class PGRTR : System.Web.UI.Page
	{


        protected Prestador prestador = new Prestador();
        protected Usuario usuario = new Usuario();
        protected Cliente cliente = new Cliente();
        protected Empregado empregado = new Empregado();

		protected void Page_Load(object sender, System.EventArgs e)
		{
            //InicializaWebPageObjects();
            string xUsuario = Session["usuarioLogado"].ToString();

            Boolean zAnexos = false;

			try
			{

                
                Ilitera.Data.PPRA_EPI xAnexos = new Ilitera.Data.PPRA_EPI();
                DataSet xdS = xAnexos.Retornar_Anexos_PPRA(Convert.ToInt32(Request["IdLaudoTecnico"]), "-PDF");

                int xTamanho = 0;

                if (xdS.Tables[0].Rows.Count > 0)
                {
                    //xTamanho = 18;
                    zAnexos = true;
                }
                else
                {
                    //xTamanho = 16;
                    zAnexos = false;
                }


                LaudoTecnico laudo = new LaudoTecnico(Convert.ToInt32(Request["IdLaudoTecnico"]));

                Cliente cliente = new Cliente(Convert.ToInt32(Session["Empresa"]));

                PPRA_EPI xEPI = new PPRA_EPI();
                DataSet ydS = xEPI.Gerar_Relatorio(Convert.ToInt32(Request["IdLaudoTecnico"]));
                Boolean zEPI;
                //int xRetirar = 0;


                //if (laudo.NR20 == false)
                //{
                //    xRetirar = 1;
                //}



                if (ydS.Tables[0].Rows.Count == 0)
                {
                    //xTamanho--;
                    zEPI = false;
                }
                else
                {
                    //xTamanho--;
                    zEPI = true;
                }



                CrystalDecisions.CrystalReports.Engine.ReportClass[] reports = new ReportClass[12];

                //Inicio - Gerando informações para montagem do relatório Introdução PPRA
                RptIntroducaoPGRTR reportIntroducaoPPRA = new DataSourceIntroducaoPGRTR(Convert.ToInt32(Request["IdLaudoTecnico"])).GetReportIndicePGRTR("", "");
                RptIntroducaoPGRTR_2 reportIntroducaoPPRA_2 = new DataSourceIntroducaoPGRTR(Convert.ToInt32(Request["IdLaudoTecnico"])).GetReportIndicePGRTR_2("", "");
                RptIntroducaoPGRTR_3 reportIntroducaoPPRA_3 = new DataSourceIntroducaoPGRTR(Convert.ToInt32(Request["IdLaudoTecnico"])).GetReportIndicePGRTR_3("", "");
                //Funcionalidade funcionalidadeIntroducaoPPRA = Funcionalidade.GetFuncionalidade(FuncionalidadeTipo.Relatorio, reportIntroducaoPPRA.GetType());

                RptPGRTR_A4 reportPlanilhaPPRA = new RptPGRTR_A4();

                reportPlanilhaPPRA = new DataSourcePlanilhaA4PGRTR(Convert.ToInt32(Request["IdLaudoTecnico"])).GetReport();
                //Funcionalidade funcionalidadePlanilhaPPRA = Funcionalidade.GetFuncionalidade(FuncionalidadeTipo.Relatorio, reportPlanilhaPPRA.GetType());

                RptListaFuncoes reportFuncoes = new DataSourceClassificacaoFuncional(cliente, laudo).GetReportListaFuncoes(2);


                //Inicio - Gerando informações para montagem do relatório Documento Base
                //RptDocumentoBasePGR reportDocumentoBase = new DataSourceDocumentoBasePPRA(Convert.ToInt32(Request["IdLaudoTecnico"])).GetReportPGR();

                //RptDocumentoBasePGR reportDocumentoBase = new Ilitera.Relatorios.Report.DataSourceDocumentoBasePGR(laudo).GetReportPGR();
                //Funcionalidade funcionalidadeDocumentoBase = Funcionalidade.GetFuncionalidade(FuncionalidadeTipo.Relatorio, reportDocumentoBase.GetType());


                //Inicio - Gerando informações para montagem do relatório Quadro EPI                
                RptQuadroEPI_PGRTR reportQuadroEPI = new DataSourceQuadroEPI(laudo).GetReportPGRTR();
                Funcionalidade funcionalidadeQuadroEPI = Funcionalidade.GetFuncionalidade(FuncionalidadeTipo.Relatorio, reportQuadroEPI.GetType());
                
                RptDocumentoBasePGRTR reportDocumentoBase = new DataSourceDocumentoBasePGRTR(laudo).GetReport();
                
           
                RptPPRAAnexo2 reportAnexo = new DataSourcePPRAAnexo(Convert.ToInt32(Request["IdLaudoTecnico"])).GetReport();
                
                               

           
                reports[0] = reportIntroducaoPPRA;
                reports[1] = reportIntroducaoPPRA_2;
                reports[2] = reportIntroducaoPPRA_3;
                reports[3] = reportDocumentoBase;

                if (zEPI == true)
                {

                    reports[4] = reportQuadroEPI;

                    reports[5] = reportPlanilhaPPRA;



                    reports[6] = reportFuncoes;
                    //reports[5] = reportSetores;

                    xTamanho = 7;

                    if (zAnexos == true) //(xTamanho == 8)
                    {
                        reports[7] = reportAnexo;
                        xTamanho++;
                    }


                }
                else
                {

                    reports[4] = reportPlanilhaPPRA;


                    reports[5] = reportFuncoes;
                    //reports[4] = reportSetores;

                    xTamanho = 6;

                    if (zAnexos == true)  //(xTamanho == 6)
                    {
                        reports[6] = reportAnexo;
                        xTamanho++;
                    }


                }
                
                //reports[4] = reportEmpregado;

                Array.Resize(ref reports, xTamanho);

                //Passando o parâmetro de renumeração de páginas para false
                CreatePDFMerged(reports, this.Response, "", true);

                //LaudoTecnico laudo = new LaudoTecnico(Convert.ToInt32(Request["IdLaudoTecnico"]));

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
