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
using System.IO;
using Ilitera.VasoCaldeira.Report;



namespace Ilitera.Net
{
	/// <summary>
	/// 
	/// </summary>
	public partial class Vasos : System.Web.UI.Page
	{
		protected void Page_Load(object sender, System.EventArgs e)
		{
            //InicializaWebPageObjects();
            string xUsuario = Session["usuarioLogado"].ToString();

            if (Request["Tipo"].ToString().Trim() == "1")
            {

                try
                {
                    //LaudoTecnico laudo = new LaudoTecnico(Convert.ToInt32(Request["IdLaudoTecnico"]));
                    //Cliente xCliente = new Cliente(  System.Convert.ToInt32( Session["Empresa"].ToString()));
                    ProjetoVasoCaldeira xProj = new ProjetoVasoCaldeira();
                    xProj.Find(" IdVasoCaldeiraBase = " + Request["IdVaso"].ToString());

                    if (xProj.Id == 0)
                    {
                        MsgBox1.Show("Ilitera.Net", "Projeto de Instalação não localizado.", null,
                               new EO.Web.MsgBoxButton("OK"));
                        return;
                    }


                    CrystalDecisions.CrystalReports.Engine.ReportClass[]
                                       reports = new CrystalDecisions.CrystalReports.Engine.ReportClass[1];


                    int i = 0;


                    //using (RptVasoProjeto report = new DataSourceVasoProjeto(this.usrCntrlProjeto1.projetoVasoCaldeira).GetReport())

                    reports[i++] = new DataSourceVasoProjeto(xProj).GetReport();



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
            else if (Request["Tipo"].ToString().Trim() == "2")
            {


                try
                {
                    //LaudoTecnico laudo = new LaudoTecnico(Convert.ToInt32(Request["IdLaudoTecnico"]));
                    //Cliente xCliente = new Cliente(  System.Convert.ToInt32( Session["Empresa"].ToString()));
                    InspecaoVasoCaldeira xProj = new InspecaoVasoCaldeira();

                    
                    //ajustar daqui para baixo

                    xProj.Find( System.Convert.ToInt32( Request["IdVaso"].ToString()) );

                    if (xProj.Id == 0)
                    {
                        MsgBox1.Show("Ilitera.Net", "Inspeção não localizada.", null,
                               new EO.Web.MsgBoxButton("OK"));
                        return;
                    }


                    CrystalDecisions.CrystalReports.Engine.ReportClass[]
                                       reports = new CrystalDecisions.CrystalReports.Engine.ReportClass[1];


                    int i = 0;


                    //using (RptVasoProjeto report = new DataSourceVasoProjeto(this.usrCntrlProjeto1.projetoVasoCaldeira).GetReport())                                       
                    reports[i++] = new DataSourceVasoInspecao(xProj).GetReport();



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
            else if (Request["Tipo"].ToString().Trim() == "12")
            {


                try
                {
                    //LaudoTecnico laudo = new LaudoTecnico(Convert.ToInt32(Request["IdLaudoTecnico"]));
                    //Cliente xCliente = new Cliente(  System.Convert.ToInt32( Session["Empresa"].ToString()));
                    InspecaoVasoCaldeira xProj = new InspecaoVasoCaldeira();


                    //ajustar daqui para baixo

                    xProj.Find(System.Convert.ToInt32(Request["IdVaso"].ToString()));

                    if (xProj.Id == 0)
                    {
                        MsgBox1.Show("Ilitera.Net", "Inspeção não localizada.", null,
                               new EO.Web.MsgBoxButton("OK"));
                        return;
                    }


                    CrystalDecisions.CrystalReports.Engine.ReportClass[]
                                       reports = new CrystalDecisions.CrystalReports.Engine.ReportClass[1];


                    int i = 0;


                    //using (RptVasoProjeto report = new DataSourceVasoProjeto(this.usrCntrlProjeto1.projetoVasoCaldeira).GetReport())                                       
                    reports[i++] = new DataSourceVasoInspecao(xProj).GetReport_Novo();



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
