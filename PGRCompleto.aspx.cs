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


namespace Ilitera.Net
{
	/// <summary>
	/// 
	/// </summary>
    public partial class PGRCompleto : System.Web.UI.Page
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
                RptIntroducaoPGR reportIntroducaoPPRA = new DataSourceIntroducaoPGR(Convert.ToInt32(Request["IdLaudoTecnico"])).GetReport();
                RptIntroducaoPGR_2 reportIntroducaoPPRA_2 = new DataSourceIntroducaoPGR(Convert.ToInt32(Request["IdLaudoTecnico"])).GetReport_2();
                RptIntroducaoPGR_3 reportIntroducaoPPRA_3 = new DataSourceIntroducaoPGR(Convert.ToInt32(Request["IdLaudoTecnico"])).GetReport_3();
                Funcionalidade funcionalidadeIntroducaoPPRA = Funcionalidade.GetFuncionalidade(FuncionalidadeTipo.Relatorio, reportIntroducaoPPRA.GetType());

                RptPGR_A4 reportPlanilhaPPRA = new RptPGR_A4();

                //RptPPRA_A4_Novo reportPlanilhaPPRA_Novo = new RptPPRA_A4_Novo();
                //RptPPRA_A4 reportPlanilhaPPRA = new RptPPRA_A4();
                //RptPPRA_A4_Prajna reportPlanilhaPPRAPrajna = new RptPPRA_A4_Prajna();

                ////Inicio - Gerando informações para montagem do relatório Planilha PPRA

                //if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("PRAJNA") > 0)
                //{
                //    reportPlanilhaPPRAPrajna = new DataSourcePlanilhaA4PPRA(Convert.ToInt32(Request["IdLaudoTecnico"])).GetReport_Prajna();
                //    Funcionalidade funcionalidadePlanilhaPPRA = Funcionalidade.GetFuncionalidade(FuncionalidadeTipo.Relatorio, reportPlanilhaPPRAPrajna.GetType());
                //}
                ////if (cliente.NomeAbreviado.ToUpper().IndexOf("GROB") >= 0 || cliente.NomeAbreviado.ToUpper().IndexOf("COFERLY") >= 0 || Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("ROPHE") > 0)
                //else if (cliente.Microbiologia == true)
                //{
                //    reportPlanilhaPPRA_Novo = new DataSourcePlanilhaA4PPRA_Novo(Convert.ToInt32(Request["IdLaudoTecnico"])).GetReport();
                //    Funcionalidade funcionalidadePlanilhaPPRA = Funcionalidade.GetFuncionalidade(FuncionalidadeTipo.Relatorio, reportPlanilhaPPRA_Novo.GetType());
                //}
                //else
                //{
                    reportPlanilhaPPRA = new DataSourcePlanilhaA4PGR(Convert.ToInt32(Request["IdLaudoTecnico"])).GetReport();
                    Funcionalidade funcionalidadePlanilhaPPRA = Funcionalidade.GetFuncionalidade(FuncionalidadeTipo.Relatorio, reportPlanilhaPPRA.GetType());
                //}


                //Inicio - Gerando informações para montagem do relatório Documento Base
                //RptDocumentoBasePGR reportDocumentoBase = new DataSourceDocumentoBasePPRA(Convert.ToInt32(Request["IdLaudoTecnico"])).GetReportPGR();
                RptDocumentoBasePGR reportDocumentoBase = new Ilitera.Relatorios.Report.DataSourceDocumentoBasePGR(laudo).GetReportPGR();
                Funcionalidade funcionalidadeDocumentoBase = Funcionalidade.GetFuncionalidade(FuncionalidadeTipo.Relatorio, reportDocumentoBase.GetType());
                

                //Inicio - Gerando informações para montagem do relatório Quadro EPI                
                RptQuadroEPI_PGR reportQuadroEPI = new DataSourceQuadroEPI(laudo).GetReportPGR();
                Funcionalidade funcionalidadeQuadroEPI = Funcionalidade.GetFuncionalidade(FuncionalidadeTipo.Relatorio, reportQuadroEPI.GetType());
                


                RptListaFuncoes reportFuncoes = new DataSourceClassificacaoFuncional(cliente, laudo).GetReportListaFuncoes(2);

                RptPPRA_NR20_2 reportNR20 = new RptPPRA_NR20_2() ;

                if (laudo.NR20 == true)
                {
                    reportNR20 = new DataSourcePPRA_NR20(laudo).GetReport_New();
                    Funcionalidade funcionalidadeNR20 = Funcionalidade.GetFuncionalidade(FuncionalidadeTipo.Relatorio, reportNR20.GetType());
                }


                //RptListaSetor reportSetores = new DataSourceClassificacaoFuncional(cliente, laudo).GetReportListaSetor();
                

                RptPPRAAnexo2 reportAnexo = new DataSourcePPRAAnexo(Convert.ToInt32(Request["IdLaudoTecnico"])).GetReport();
                

                

           
                reports[0] = reportIntroducaoPPRA;
                reports[1] = reportIntroducaoPPRA_2;
                reports[2] = reportIntroducaoPPRA_3;

                reports[3] = reportDocumentoBase;

                if (zEPI == true)
                {               

                    reports[4] = reportQuadroEPI;

                    reports[5] = reportPlanilhaPPRA;
                

                    if (laudo.NR20 == true)
                    {

                        reports[6] = reportNR20;

                        reports[7] = reportFuncoes;
                        //reports[5] = reportSetores;

                        xTamanho = 8;

                        if (zAnexos == true) //(xTamanho == 8)
                        {
                            reports[8] = reportAnexo;
                            xTamanho++;
                        }

                    }
                    else
                    {
                       reports[6] = reportFuncoes;
                       //reports[5] = reportSetores;

                       xTamanho = 7;

                       if ( zAnexos == true ) //(xTamanho == 8)
                       {
                           reports[7] = reportAnexo;
                           xTamanho++;
                       }

                    }
                }
                else
                {
           
                   reports[4] = reportPlanilhaPPRA;
                

                    if (laudo.NR20 == true)
                    {

                        reports[5] = reportNR20;
                        reports[6] = reportFuncoes;
                        //reports[4] = reportSetores;

                        xTamanho = 7;

                        if (zAnexos == true)  //(xTamanho == 6)
                        {
                            reports[7] = reportAnexo;
                            xTamanho++;
                        }

                    }
                    else
                    {
                        reports[5] = reportFuncoes;
                        //reports[4] = reportSetores;

                        xTamanho = 6;

                        if (zAnexos == true)  //(xTamanho == 6)
                        {
                            reports[6] = reportAnexo;
                            xTamanho++;
                        }
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
