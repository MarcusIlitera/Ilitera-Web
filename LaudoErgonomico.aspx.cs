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
using Ilitera.Sied.Report;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Text;
using System.Collections.Generic;
using System.IO;


namespace Ilitera.Net
{
	/// <summary>
	/// 
	/// </summary>
	public partial class LaudoErgonomico : System.Web.UI.Page
	{
		protected void Page_Load(object sender, System.EventArgs e)
		{
            //InicializaWebPageObjects();

            string xUsuario = Session["usuarioLogado"].ToString();

            //         try
            //{
            //	RptErgonomia report = new DataSourceLaudoErgonomico(Convert.ToInt32(Request.QueryString["IdLaudoTecnico"])).GetReport();

            //            //Funcionalidade funcionalidade = Funcionalidade.GetFuncionalidade(FuncionalidadeTipo.Relatorio, report.GetType());
            //             //ImpressaoDocUsuario.InsereRegistro(usuario, cliente, Convert.ToInt64(report.SummaryInfo.KeywordsInReport), true, funcionalidade);

            //	CreatePDFDocument(report, this.Response);
            //}
            //catch(Exception ex)
            //{
            //             MsgBox1.Show("Ilitera.Net", ex.Message, null,
            //                     new EO.Web.MsgBoxButton("OK"));                

            //}


            try
            {

                Int32 zLaudo = Convert.ToInt32(Request.QueryString["IdLaudoTecnico"]);

                Ilitera.Data.PPRA_EPI xAnexos = new Ilitera.Data.PPRA_EPI();
                DataSet xdS = xAnexos.Retornar_Anexos_Erg(zLaudo, "-PDF");

                int xTamanho = 0;
                int xTextoAnexo = 0;

                if (xdS.Tables[0].Rows.Count > 0)
                {
                    xTamanho = 1;
                }
                else
                {
                    xTamanho = 0;
                }


                List<LaudoErgonomicoDocumento> rAnexos = new LaudoErgonomicoDocumento().Find<LaudoErgonomicoDocumento>(" nId_Laud_Tec = " + zLaudo.ToString() + " and ( Titulo <> '' ) ");

                if (rAnexos.Count > 0)
                {
                    xTextoAnexo++;
                }



                ArrayList listCronograma = new CronogramaErgonomia().Find("nID_LAUD_TEC=" + zLaudo);

                if (listCronograma.Count == 0 && xTamanho == 1)
                {

                    CrystalDecisions.CrystalReports.Engine.ReportClass[]
                        reports = new CrystalDecisions.CrystalReports.Engine.ReportClass[2 + xTextoAnexo];



                    int i = 0;

                    LaudoTecnico rLaudo = new LaudoTecnico(zLaudo);

                    if (rLaudo.Ergonomia_PGR == false)
                    {
                        reports[i++] = new DataSourceLaudoErgonomico(zLaudo).GetReport();
                    }
                    else
                    {
                        reports[i++] = new DataSourceLaudoErgonomico(zLaudo).GetReport_New();
                    }


                    reports[i++] = new DataSourcePPRAAnexo(zLaudo, "Erg").GetReport();


                    if (rAnexos.Count > 0)
                    {
                        reports[i++] = new DataSourcePPRAAnexo(zLaudo).GetReport_Texto_Erg();    
                    }


                    CreatePDFMerged(reports, this.Response, "", true);

                    reports = null;
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    GC.Collect();

                }
                else if (listCronograma.Count == 0 && xTamanho == 0)
                {
                    CrystalDecisions.CrystalReports.Engine.ReportClass[]
                        reports = new CrystalDecisions.CrystalReports.Engine.ReportClass[1 + xTextoAnexo];

  

                    int i = 0;

                    LaudoTecnico rLaudo = new LaudoTecnico(zLaudo);

                    if (rLaudo.Ergonomia_PGR == false)
                    {
                        reports[i++] = new DataSourceLaudoErgonomico(zLaudo).GetReport();
                    }
                    else
                    {
                        reports[i++] = new DataSourceLaudoErgonomico(zLaudo).GetReport_New();
                    }



                    if (rAnexos.Count > 0)
                    {
                        reports[i++] = new DataSourcePPRAAnexo(zLaudo).GetReport_Texto_Erg();
                    }



                    CreatePDFMerged(reports, this.Response, "", true);

                    reports = null;
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    GC.Collect();

                }
                else if (listCronograma.Count > 0 && xTamanho == 0)
                {
                    CrystalDecisions.CrystalReports.Engine.ReportClass[]
                        reports = new CrystalDecisions.CrystalReports.Engine.ReportClass[2 + xTextoAnexo];


                    int i = 0;

                    LaudoTecnico rLaudo = new LaudoTecnico(zLaudo);

                    if (rLaudo.Ergonomia_PGR == false)
                    {
                        reports[i++] = new DataSourceLaudoErgonomico(zLaudo).GetReport();
                    }
                    else
                    {
                        reports[i++] = new DataSourceLaudoErgonomico(zLaudo).GetReport_New();
                    }



                    reports[i++] = new DataSourceCronogramaErgonomia(zLaudo).GetReport();
         

                    if (rAnexos.Count > 0)
                    {
                        reports[i++] = new DataSourcePPRAAnexo(zLaudo).GetReport_Texto_Erg();
                    }

                    CreatePDFMerged(reports, this.Response, "", true);

                    reports = null;
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    GC.Collect();



                }
                else
                {
                    CrystalDecisions.CrystalReports.Engine.ReportClass[]
                        reports = new CrystalDecisions.CrystalReports.Engine.ReportClass[3 + xTextoAnexo];
                                        

                    int i = 0;

                    LaudoTecnico rLaudo = new LaudoTecnico(zLaudo);

                    if (rLaudo.Ergonomia_PGR == false)
                    {
                        reports[i++] = new DataSourceLaudoErgonomico(zLaudo).GetReport();
                    }
                    else
                    {
                        reports[i++] = new DataSourceLaudoErgonomico(zLaudo).GetReport_New();
                    }


                    reports[i++] = new DataSourceCronogramaErgonomia(zLaudo).GetReport();
                   

                    reports[i++] = new DataSourcePPRAAnexo(zLaudo, "Erg").GetReport();
                  

                    if (rAnexos.Count > 0)
                    {
                        reports[i++] = new DataSourcePPRAAnexo(zLaudo).GetReport_Texto_Erg();
                       

                    }


                    CreatePDFMerged(reports, this.Response, "", true);

                    reports = null;
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    GC.Collect();

                }



            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
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



        protected static void CreatePDFDocument(ReportClass report, HttpResponse response)
        {
            CreatePDFDocument(report, response, false, string.Empty);
        }

        protected static void CreatePDFDocument(ReportClass report, HttpResponse response, bool forceDownload, string DownloadfileName)
        {
            report.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, response, forceDownload, DownloadfileName);
        }


        protected static string strOpenReport(string directory, string fileAndQuery, string ReportName)
        {
            return strOpenReport(directory, fileAndQuery, ReportName, false);
        }

        protected static string strOpenReport(string directory, string fileAndQuery, string ReportName, bool useDirectoryForLocalProcess)
        {
            StringBuilder st = new StringBuilder();

            Guid strAux = Guid.NewGuid();

            string valueProcess = "Local";

            if (valueProcess.Equals("Remote"))
                st.Append("void(window.open('http://report.ilitera.net/index.aspx?Identity=" + strAux.ToString() + "opsa" + strAux.ToString()
                    + "&Key=" + strAux.ToString() + "IliteraNet" + strAux.ToString()
                    + "&PathAndQuery=" + HttpUtility.UrlEncode(directory + "/" + fileAndQuery) + "', '" + ReportName + "', 'scrollbars=yes, resizable=yes, toolbar=no, addressbar=no, menubar=no, width=800px, height=600px').focus());");
            else if (valueProcess.Equals("Local"))
            {
                if (useDirectoryForLocalProcess)
                {
                    st.AppendFormat("void(window.open('../{0}/{1}', '{2}', 'scrollbars=yes, resizable=yes, toolbar=no, addressbar=no, menubar=no, width=800px, height=600px').focus());", directory, fileAndQuery, ReportName);
                }
                else
                {
                    st.AppendFormat("void(window.open('{0}','{1}', 'scrollbars=yes, resizable=yes, toolbar=no, addressbar=no, menubar=no, width=800px, height=600px').focus());", fileAndQuery, ReportName);
                }
            }

            return st.ToString();
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
