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
using CrystalDecisions.Web;
using CrystalDecisions.Windows.Forms;

using Ilitera.Data;
using Ilitera.Opsa.Data;
using Ilitera.Common;
using Ilitera.Sied.Report;


namespace Ilitera.Net
{
	/// <summary>
	/// 
	/// </summary>
    public partial class PPRACompletoIndice : System.Web.UI.Page
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
                /*Ilitera 11/10/2010
                    Pegando as informações necessárias para a geração do PPRA Completo, contendo todos os outros relatórios
                 */


                string zIndice = "";                
                string zIndice2 = "";
                int zTotPage = 1;



                LaudoTecnico laudo = new LaudoTecnico(Convert.ToInt32(Request["IdLaudoTecnico"]));

                laudo.nID_EMPR.Find();

                if (laudo.nID_EMPR.IdJuridicaPai.Id != 0)
                {
                    zTotPage = 2;
                    //zIntr = 1;   //ajustar paginas da introd.no indice
                }


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



                int zLinha = 9;

                ArrayList listGhe = new Ghe().Find("nID_LAUD_TEC=" + laudo.Id + " ORDER BY tNO_FUNC");



                CrystalDecisions.CrystalReports.Engine.ReportClass[] reports = new ReportClass[10];

                //Inicio - Gerando informações para montagem do relatório Introdução PPRA
                RptIntroducaoPPRA reportIntroducaoPPRA = new DataSourceIntroducaoPPRA(Convert.ToInt32(Request["IdLaudoTecnico"])).GetReport();
                Funcionalidade funcionalidadeIntroducaoPPRA = Funcionalidade.GetFuncionalidade(FuncionalidadeTipo.Relatorio, reportIntroducaoPPRA.GetType());

                if (listGhe.Count < 63)  //se indice tiver duas páginas, deve-se somar mais 1
                    zTotPage = reportIntroducaoPPRA.FormatEngine.GetLastPageNumber(new CrystalDecisions.Shared.ReportPageRequestContext()) + 1;
                else
                    zTotPage = reportIntroducaoPPRA.FormatEngine.GetLastPageNumber(new CrystalDecisions.Shared.ReportPageRequestContext()) + 2;



                zIndice = zIndice + "Introdução PPRA...................................................................................." + (zTotPage - 7).ToString() + System.Environment.NewLine;
                zIndice = zIndice + "   Resolução......................................................................................." + (zTotPage - 7).ToString() + System.Environment.NewLine;
                zIndice = zIndice + "   Introdução......................................................................................" + (zTotPage - 6).ToString() + System.Environment.NewLine;
                zIndice = zIndice + "   Metodologia....................................................................................." + (zTotPage - 5).ToString() + System.Environment.NewLine;
                zIndice = zIndice + "   Antecipação....................................................................................." + (zTotPage - 4).ToString() + System.Environment.NewLine;
                zIndice = zIndice + "   Fluxograma - Estudo das Etapas do PPRA.........................................................." + (zTotPage - 3).ToString() + System.Environment.NewLine;
                zIndice = zIndice + "   Fluxograma - Estudo da Exposição ao Ruído Ocupacional..........................................." + (zTotPage - 2).ToString() + System.Environment.NewLine;
                zIndice = zIndice + "   Fluxograma - Estudo da Exposição ao Calor......................................................." + (zTotPage - 1).ToString() + System.Environment.NewLine;
                zIndice = zIndice + "   Fluxograma - Estudo da Exposição a Produtos Químicos............................................" + (zTotPage).ToString() + System.Environment.NewLine;

                

                //Inicio - Gerando informações para montagem do relatório Documento Base
                RptDocumentoBasePPRA reportDocumentoBase = new DataSourceDocumentoBasePPRA(Convert.ToInt32(Request["IdLaudoTecnico"])).GetReport();
                Funcionalidade funcionalidadeDocumentoBase = Funcionalidade.GetFuncionalidade(FuncionalidadeTipo.Relatorio, reportDocumentoBase.GetType());
                zIndice = zIndice + "Documento Base....................................................................................." + (zTotPage+1).ToString() + System.Environment.NewLine;
                zTotPage = zTotPage + reportDocumentoBase.FormatEngine.GetLastPageNumber(new CrystalDecisions.Shared.ReportPageRequestContext());

                int xPaginaPlanilha = zTotPage;

                RptPPRA_A4_Novo reportPlanilhaPPRA_Novo = new RptPPRA_A4_Novo();
                RptPPRA_A4 reportPlanilhaPPRA = new RptPPRA_A4();
                RptPPRA_A4_Prajna reportPlanilhaPPRAPrajna = new RptPPRA_A4_Prajna();


                //Inicio - Gerando informações para montagem do relatório Planilha PPRA

                if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("PRAJNA") > 0)
                {
                    reportPlanilhaPPRAPrajna = new DataSourcePlanilhaA4PPRA(Convert.ToInt32(Request["IdLaudoTecnico"])).GetReport_Prajna();
                    zIndice = zIndice + "Planilha PPRA......................................................................................" + (zTotPage + 1).ToString() + System.Environment.NewLine;
                    zTotPage = zTotPage + reportPlanilhaPPRAPrajna.FormatEngine.GetLastPageNumber(new CrystalDecisions.Shared.ReportPageRequestContext());

                    Funcionalidade funcionalidadePlanilhaPPRA = Funcionalidade.GetFuncionalidade(FuncionalidadeTipo.Relatorio, reportPlanilhaPPRAPrajna.GetType());
                }
                //if (cliente.NomeAbreviado.ToUpper().IndexOf("GROB") >= 0 || cliente.NomeAbreviado.ToUpper().IndexOf("COFERLY") >= 0 || Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("ROPHE") > 0)
                else if (cliente.Microbiologia == true)
                {
                    reportPlanilhaPPRA_Novo = new DataSourcePlanilhaA4PPRA_Novo(Convert.ToInt32(Request["IdLaudoTecnico"])).GetReport();
                    zIndice = zIndice + "Planilha PPRA......................................................................................" + (zTotPage + 1).ToString() + System.Environment.NewLine;
                    zTotPage = zTotPage + reportPlanilhaPPRA_Novo.FormatEngine.GetLastPageNumber(new CrystalDecisions.Shared.ReportPageRequestContext());

                    Funcionalidade funcionalidadePlanilhaPPRA = Funcionalidade.GetFuncionalidade(FuncionalidadeTipo.Relatorio, reportPlanilhaPPRA_Novo.GetType());
                }
                else
                {
                    reportPlanilhaPPRA = new DataSourcePlanilhaA4PPRA(Convert.ToInt32(Request["IdLaudoTecnico"])).GetReport();
                    zIndice = zIndice + "Planilha PPRA......................................................................................" + (zTotPage + 1).ToString() + System.Environment.NewLine;
                    zTotPage = zTotPage + reportPlanilhaPPRA.FormatEngine.GetLastPageNumber(new CrystalDecisions.Shared.ReportPageRequestContext());

                    Funcionalidade funcionalidadePlanilhaPPRA = Funcionalidade.GetFuncionalidade(FuncionalidadeTipo.Relatorio, reportPlanilhaPPRA.GetType());
                }




                //Pegar paginas dos GHEs da planilha
                Ilitera.Common.Pessoa xPessoa = new Ilitera.Common.Pessoa();

                //na web, não terei esse viewer, como fazer ??
                //Ilitera.Viewer.FormReportViewer xViewer = new Ilitera.Viewer.FormReportViewer(xPessoa, reports[i - 1], "teste");

                int xLoc = 0;
                string zGhe = "";

                CrystalDecisions.Windows.Forms.CrystalReportViewer zViewer = new CrystalDecisions.Windows.Forms.CrystalReportViewer();


                
                if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("PRAJNA") > 0)
                {
                    zViewer.ReportSource = reportPlanilhaPPRAPrajna;
                }
                //if (cliente.NomeAbreviado.ToUpper().IndexOf("GROB") >= 0 || cliente.NomeAbreviado.ToUpper().IndexOf("COFERLY") >= 0 || Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("ROPHE") > 0)
                else if (cliente.Microbiologia == true)
                {
                    zViewer.ReportSource = reportPlanilhaPPRA_Novo;
                }
                else
                {
                    zViewer.ReportSource = reportPlanilhaPPRA;
                }

                zViewer.RefreshReport();
                


                //this.crystalReportViewer1.SearchForText(xTexto);
                //int xLoc = crystalReportViewer1.GetCurrentPageNumber();
                                

                foreach (Ghe ghe in listGhe)
                {
                    if (zViewer.SearchForText(ghe.tNO_FUNC.Trim()) == true)
                    {
                        xLoc = zViewer.GetCurrentPageNumber();
                    }

                    if (ghe.tNO_FUNC.Trim().Length > 80)
                        zGhe = "      " + ghe.tNO_FUNC.Trim().Substring(0, 80);
                    else
                        zGhe = "      " + ghe.tNO_FUNC.Trim();

                    for (int zT = zGhe.Length; zT <= 98; zT++)
                    {
                        zGhe = zGhe + ".";
                    }

                    zGhe = zGhe + (xPaginaPlanilha + xLoc ).ToString();
                    zLinha++;

                    if (zLinha >= 65)
                        zIndice2 = zIndice2 + zGhe + System.Environment.NewLine;
                    else
                        zIndice = zIndice + zGhe + System.Environment.NewLine;
                }


                zTotPage++;


                

                RptPPRA_NR20_2 reportNR20 = new RptPPRA_NR20_2() ;

                if (laudo.NR20 == true)
                {
                    reportNR20 = new DataSourcePPRA_NR20(laudo).GetReport_New();
                    Funcionalidade funcionalidadeNR20 = Funcionalidade.GetFuncionalidade(FuncionalidadeTipo.Relatorio, reportNR20.GetType());

                    if (zLinha >= 65)
                        zIndice2 = zIndice2 + "NR 20.............................................................................................." + zTotPage.ToString() + System.Environment.NewLine;
                    else
                        zIndice = zIndice + "NR 20.............................................................................................." + zTotPage.ToString() + System.Environment.NewLine;

                    zTotPage = zTotPage + reportNR20.FormatEngine.GetLastPageNumber(new CrystalDecisions.Shared.ReportPageRequestContext());

                }


                RptListaFuncoes reportFuncoes = new DataSourceClassificacaoFuncional(cliente, laudo).GetReportListaFuncoes(1);
                if (zLinha >= 65)
                    zIndice2 = zIndice2 + "Funções............................................................................................" + zTotPage.ToString() + System.Environment.NewLine;
                else
                    zIndice = zIndice + "Funções............................................................................................" + zTotPage.ToString() + System.Environment.NewLine;

                zTotPage = zTotPage + reportFuncoes.FormatEngine.GetLastPageNumber(new CrystalDecisions.Shared.ReportPageRequestContext());



                //RptListaSetor reportSetores = new DataSourceClassificacaoFuncional(cliente, laudo).GetReportListaSetor();
                RptPPRAAnexo2 reportAnexo = new DataSourcePPRAAnexo(Convert.ToInt32(Request["IdLaudoTecnico"])).GetReport();

                if (zAnexos == true)
                {               

                    if (zLinha >= 65)
                        zIndice2 = zIndice2 + "Anexos............................................................................................." + zTotPage.ToString() + System.Environment.NewLine;
                    else
                        zIndice = zIndice + "Anexos............................................................................................." + zTotPage.ToString() + System.Environment.NewLine;
                    zTotPage = zTotPage + reportAnexo.FormatEngine.GetLastPageNumber(new CrystalDecisions.Shared.ReportPageRequestContext());
                    
                }



                //Inicio - Gerando informações para montagem do relatório Quadro EPI                
                RptQuadroEPI reportQuadroEPI = new DataSourceQuadroEPI(laudo).GetReport();
                Funcionalidade funcionalidadeQuadroEPI = Funcionalidade.GetFuncionalidade(FuncionalidadeTipo.Relatorio, reportQuadroEPI.GetType());

                if (zLinha >= 65)
                    zIndice2 = zIndice2 + "Quadro EPI........................................................................................." + zTotPage.ToString() + System.Environment.NewLine;
                else
                    zIndice = zIndice + "Quadro EPI........................................................................................." + zTotPage.ToString() + System.Environment.NewLine;
                



                reportIntroducaoPPRA = new DataSourceIntroducaoPPRA(Convert.ToInt32(Request["IdLaudoTecnico"])).GetReportIndice(zIndice, zIndice2);

                reports[0] = reportIntroducaoPPRA;
                reports[1] = reportDocumentoBase;

                if (zEPI == true)
                {


                    if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("PRAJNA") > 0)
                    {
                        reports[2] = reportPlanilhaPPRAPrajna;
                    }
                    //if (cliente.NomeAbreviado.ToUpper().IndexOf("GROB") >= 0 || cliente.NomeAbreviado.ToUpper().IndexOf("COFERLY") >= 0 || Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("ROPHE") > 0)
                    else if (cliente.Microbiologia == true)
                    {
                        reports[2] = reportPlanilhaPPRA_Novo;
                    }
                    else
                    {
                        reports[2] = reportPlanilhaPPRA;
                    }

                    if (laudo.NR20 == true)
                    {

                        reports[3] = reportNR20;

                        reports[4] = reportFuncoes;
                        //reports[5] = reportSetores;

                        xTamanho = 6;

                        if (zAnexos == true) //(xTamanho == 8)
                        {
                            reports[5] = reportAnexo;
                            xTamanho++;
                        }

                        reports[6] = reportQuadroEPI;

                    }
                    else
                    {
                       reports[3] = reportFuncoes;
                       //reports[5] = reportSetores;

                       xTamanho = 5;

                        if (zAnexos == true) //(xTamanho == 8)
                        {
                            reports[4] = reportAnexo;
                            reports[5] = reportQuadroEPI;
                            xTamanho++;
                        }
                        else
                        {
                            reports[4] = reportQuadroEPI;                            
                        }

                    }
                }
                else
                {

                    if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("PRAJNA") > 0)
                    {
                        reports[2] = reportPlanilhaPPRAPrajna;
                    }
                    //if (cliente.NomeAbreviado.ToUpper().IndexOf("GROB") >= 0 || cliente.NomeAbreviado.ToUpper().IndexOf("COFERLY") >= 0 || Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("ROPHE") > 0)
                    else if (cliente.Microbiologia == true)
                    {
                        reports[2] = reportPlanilhaPPRA_Novo;
                    }
                    else
                    {
                        reports[2] = reportPlanilhaPPRA;
                    }


                    if (laudo.NR20 == true)
                    {

                        reports[3] = reportNR20;
                        reports[4] = reportFuncoes;
                        //reports[4] = reportSetores;

                        xTamanho = 5;

                        if (zAnexos == true)  //(xTamanho == 6)
                        {
                            reports[5] = reportAnexo;
                            xTamanho++;
                        }

                    }
                    else
                    {
                        reports[3] = reportFuncoes;
                        //reports[4] = reportSetores;

                        xTamanho = 4;

                        if (zAnexos == true)  //(xTamanho == 6)
                        {
                            reports[4] = reportAnexo;
                            xTamanho++;
                        }
                    }

                }
                
                //reports[4] = reportEmpregado;

                Array.Resize(ref reports, xTamanho);

                //Passando o parâmetro de renumeração de páginas para false
                CreatePDFMerged(reports, this.Response, "", true);

                reports = null;
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();


                //LaudoTecnico laudo = new LaudoTecnico(Convert.ToInt32(Request["IdLaudoTecnico"]));

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
