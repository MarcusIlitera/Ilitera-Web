
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Ilitera.Opsa.Data;
using Ilitera.Common;
using System.Configuration;
using System.Linq;
using System.Web;
using System.IO;

using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using Entities;
using BLL;
using Ilitera.PCMSO.Report;
using Ilitera.Sied.Report;
using Ilitera.ASO.Report;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
//using MestraNET;


namespace Ilitera.Net.PCMSO
{
    public partial class ASOEmpregado : System.Web.UI.Page
    {


		protected void Page_Load(object sender, System.EventArgs e)
		{
            string xUsuario = Session["usuarioLogado"].ToString();
            //InicializaWebPageObjects();			
            Ilitera.Opsa.Data.Empregado empregado = new Ilitera.Opsa.Data.Empregado();

			try
			{
                Clinico exame = new Clinico(Convert.ToInt32(Request["IdExame"]));

                exame.IdPcmso.Find();
                exame.IdEmpregadoFuncao.Find();

                if (exame.IdPcmso.Id.Equals(0) || exame.IdEmpregadoFuncao.Id.Equals(0) || exame.IdEmpregadoFuncao.nID_FUNCAO == null )
				{

                    empregado.Find(" tblEMPREGADO.nID_EMPREGADO = " + Request["IdEmpregado"].ToString());

                    exame.IdEmpregadoFuncao.nID_EMPR.Find();
                    Int32 xId_Empresa = exame.IdEmpregadoFuncao.nID_EMPR.Id;

                    //exame.IdEmpregadoFuncao = EmpregadoFuncao.GetEmpregadoFuncao(empregado);
                    exame.IdEmpregadoFuncao = EmpregadoFuncao.GetEmpregadoFuncao(empregado, xId_Empresa);
                    exame.IdPcmso = exame.IdEmpregadoFuncao.nID_EMPR.GetUltimoPcmso();
					
					exame.UsuarioId = System.Convert.ToInt32( Request["IdUsuario"].ToString() );
					exame.Save();
				}


                exame.IdEmpregadoFuncao.nID_EMPR.Find();

                if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("PRAJNA") > 0)
                {
                    CrystalDecisions.CrystalReports.Engine.ReportClass[] reports = new ReportClass[4];

                    reports[0] = new DataSourceExameAsoPci_Novo(exame).GetReport_Unico("Via Empresa");
                    reports[1] = new DataSourceExameAsoPci_Novo(exame).GetReport_Unico("Via Empregado");
                    reports[2] = new DataSourceExameAsoPci_Novo(exame).GetReport_Unico("Via Clínica");
                    reports[3] = new DataSourceExameAsoPci_Novo(exame).GetReport_Unico("Via Ilitera");

                    ////PCI
                    //reports[4] = new DataSourceExameAsoPci_Novo(exame).GetReportPciUnico(false);

                    // reports[5] = new DataSourceGuia_Prajna(cliente, System.Convert.ToInt32(xId_Empregado), System.Convert.ToInt32(xId_Empresa), System.Convert.ToInt32(xId_Clinica), xExames, xExames2, xExames3, xExames4, xData_Exame, xHora_Exame, xTipo, xBasico, xObs, xImpDt, exame2.IdEmpregadoFuncao.Id).GetReport_Nova();

                    CreatePDFMerged(reports, this.Response, "", false);

                    reports = null;
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    GC.Collect();
                }
                //else if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("VIA") > 0 || Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper().IndexOf("_SHO") > 0 || (Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper().Trim() == "SIED_NOVO" && Ilitera.Data.SQLServer.EntitySQLServer._LocalServer.Trim() == "54.94.157.244") ||
                //    Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper().IndexOf("_UNO") > 0 || Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper().IndexOf("_DAITI") > 0 || Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper().IndexOf("_GLOBAL") > 0 || Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper().IndexOf("_SDTOURIN") > 0 || Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper().IndexOf("_FOX") > 0
                //    ||  (Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper().IndexOf("_SAFETY") > 0 && Ilitera.Data.SQLServer.EntitySQLServer._LocalServer.Trim() == "54.94.157.244"))
                //{
                else
                { 
                    
                    CrystalDecisions.CrystalReports.Engine.ReportClass[] reports = new ReportClass[4];

                    reports[0] = new DataSourceExameAsoPci_Novo(exame).GetReport_Unico("Via Empresa");
                    reports[1] = new DataSourceExameAsoPci_Novo(exame).GetReport_Unico("Via Empregado");
                    reports[2] = new DataSourceExameAsoPci_Novo(exame).GetReport_Unico("Via Clínica");
                    reports[3] = new DataSourceExameAsoPci_Novo(exame).GetReport_Unico("Via Ilitera");

                    ////PCI
                    //reports[4] = new DataSourceExameAsoPci_Novo(exame).GetReportPciUnico(false);

                   // reports[5] = new DataSourceGuia_Prajna(cliente, System.Convert.ToInt32(xId_Empregado), System.Convert.ToInt32(xId_Empresa), System.Convert.ToInt32(xId_Clinica), xExames, xExames2, xExames3, xExames4, xData_Exame, xHora_Exame, xTipo, xBasico, xObs, xImpDt, exame2.IdEmpregadoFuncao.Id).GetReport_Nova();
                    
                    CreatePDFMerged(reports, this.Response, "", false);

                    reports = null;
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    GC.Collect();

                }
                //else
                //{
                //    RptAso report = new DataSourceExameAsoPci(exame).GetReport();
                //    Funcionalidade funcionalidade = Funcionalidade.GetFuncionalidade(FuncionalidadeTipo.Relatorio, report.GetType());
                //    //ImpressaoDocUsuario.InsereRegistro(usuario, cliente, Convert.ToInt64(report.SummaryInfo.KeywordsInReport), true, funcionalidade);
                //    CreatePDFDocument(report, this.Response);

                //    report.Dispose();
                //    GC.Collect();
                //    GC.WaitForPendingFinalizers();
                //    GC.Collect();

                //}
            }
			catch (Exception ex)
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
		}
		#endregion
	}
}
