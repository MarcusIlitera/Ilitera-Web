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
using System.IO;


namespace Ilitera.Net
{
	/// <summary>
	/// 
	/// </summary>
    public partial class PCA : System.Web.UI.Page
    {
        protected void Page_Load(object sender, System.EventArgs e)
        {
            //InicializaWebPageObjects();

            string xUsuario = Session["usuarioLogado"].ToString();

            try
            {
                LaudoTecnico laudo = new LaudoTecnico(Convert.ToInt32(Request["IdLaudoTecnico"]));
                //Cliente xCliente = new Cliente(  System.Convert.ToInt32( Session["Empresa"].ToString()));


                ////checar se há algum GHE acima dos 80dB
                //DataSet ds = new Ghe().Get("nID_LAUD_TEC=" + laudo.Id + " ORDER BY tNO_FUNC");

                //Boolean zLoc = false;

                //foreach (DataRow row in ds.Tables[0].Rows)
                //{
                //    //row["tNO_FUNC"].ToString();
                //    DataSet ds2 = new PPRA().Get("nID_FUNC=" + row["nId_FUNC"].ToString() + " and nId_Rsc = 0 and tVL_Med >=80 ");

                //    foreach (DataRow row2 in ds2.Tables[0].Rows)
                //    {
                //        zLoc = true;
                //        break;
                //    }

                //    if (zLoc == true) break;

                //}



                //if (zLoc == true)
                //{


                //    StringBuilder st = new StringBuilder();
                //    st.Append("if(confirm(\"Como não existem colaboradores expostos à um ambiente que prejudique a saúde auditiva, ou seja, acima dos níveis de ação de 80dB(A), não há necessidade de elaboração do Programa de Conservação Auditiva.  Deseja imprimir assim mesmo ?\"))");

                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "PCA", st.ToString(), true);


                //}

                int xCronog = 0;

                ArrayList listCronograma = new CronogramaPCA().Find("nID_LAUD_TEC=" + laudo.Id);

                if (listCronograma.Count > 0)
                {
                    xCronog = 1;
                }
                else
                {
                    xCronog = 0;
                }



                CrystalDecisions.CrystalReports.Engine.ReportClass[]
                                   reports = new CrystalDecisions.CrystalReports.Engine.ReportClass[2 + xCronog];


                int i = 0;

                if (laudo.hDT_LAUDO < new DateTime(2022, 1, 3))
                {
                    reports[i++] = new DataSourcePCA(laudo).GetReport();
                    reports[i++] = new DataSourcePlanilhaA4PPRA_PCA(laudo).GetReport();
                }
                else
                {
                    reports[i++] = new DataSourcePCA(laudo).GetReport_PCAPGR();
                    reports[i++] = new DataSourcePlanilhaA4PPRA_PCA(laudo).GetReport_PCAPGR();
                }


                //reports[i++] = new DataSourcePCA(laudo).GetReport();

                //reports[i++] = new DataSourcePlanilhaA4PPRA_PCA(laudo).GetReport();


                if (xCronog == 1)
                {
                    reports[i++] = new Ilitera.PPR.Report.DataSourcePPR(laudo).GetReportCronogramaPCA();
                }



                //RptPCA report = new DataSourcePCA(xCliente).GetReport();
                //RptQuadroEPI_2 report = new DataSourceQuadroEPI_2(laudo).GetReport();

                Funcionalidade funcionalidade = Funcionalidade.GetFuncionalidade(FuncionalidadeTipo.Relatorio, reports.GetType());
                //ImpressaoDocUsuario.InsereRegistro(usuario, cliente, Convert.ToInt64(report.SummaryInfo.KeywordsInReport), true, funcionalidade);

                CreatePDFMerged(reports, this.Response, "", false);

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



    }
}
