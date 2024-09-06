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

using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

using Ilitera.Data;
using Ilitera.Opsa.Data;
using Ilitera.Common;
using Ilitera.Sied.Report;
using Ilitera.Net.Documentos;
using System.IO;

namespace Ilitera.Net.OrdemDeServico
{
	/// <summary>
	/// 
	/// </summary>
    public partial class OSCompleto : System.Web.UI.Page
	{
		protected void Page_Load(object sender, System.EventArgs e)
		{
            //InicializaWebPageObjects();

            string xUsuario = Session["usuarioLogado"].ToString();

            string xCabecalho = "ORDEM DE SERVIÇO";

            try
            {
                xCabecalho = Request["Cabec"].ToString();
            }
            catch
            {
                xCabecalho = "ORDEM DE SERVIÇO";
            }
            

            try
			{
                int xItems = 0;
                string xEmpregados = "";
                

                int zItem = 0;
                string xId = "";

                string zImpressao = Request["Impressao"];

                xItems = System.Convert.ToInt32(Request["Items"].ToString());
                xEmpregados = Session["OSEmpregados"].ToString();

                



                Ilitera.Opsa.Data.Empregado xEmpregado = new Ilitera.Opsa.Data.Empregado();
                


                //ReportOrdemDeServico report = new Ilitera.OrdemServico.Report.DataSourceOrdemDeServico(empregado, Convert.ToDateTime(Request["DataOS"])).GetReportOrdemDeServico();
                ReportClass[] reports = new ReportClass[xItems];


                if (zImpressao == "V")
                {

                    for (int zCont = 0; zCont < xEmpregados.Length; zCont++)
                    {
                        if (xEmpregados.Substring(zCont, 1) != "|" && zCont != xEmpregados.Length - 1)
                        {
                            xId = xId + xEmpregados.Substring(zCont, 1);
                        }
                        else
                        {
                            if (xId.Trim() != "")
                            {
                                xEmpregado.Find(System.Convert.ToInt32(xId));

                                System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");

                                reports[zItem] = new Ilitera.OrdemServico.Report.DataSourceOrdemDeServico(xEmpregado, Convert.ToDateTime(Request["OSData"].ToString(), ptBr)).GetReportOrdemDeServico_Vertical(xCabecalho);
                                zItem = zItem + 1;
                                xId = "";
                            }
                        }
                    }
                }
                else
                {
                    for (int zCont = 0; zCont < xEmpregados.Length; zCont++)
                    {
                        if (xEmpregados.Substring(zCont, 1) != "|" && zCont != xEmpregados.Length - 1)
                        {
                            xId = xId + xEmpregados.Substring(zCont, 1);
                        }
                        else
                        {
                            if (xId.Trim() != "")
                            {
                                xEmpregado.Find(System.Convert.ToInt32(xId));

                                System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");

                                reports[zItem] = new Ilitera.OrdemServico.Report.DataSourceOrdemDeServico(xEmpregado, Convert.ToDateTime(Request["OSData"].ToString(), ptBr)).GetReportOrdemDeServico(xCabecalho);
                                zItem = zItem + 1;
                                xId = "";
                            }
                        }
                    }

                }

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
