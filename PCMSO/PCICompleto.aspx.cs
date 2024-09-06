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
using Ilitera.Opsa.Report;
using Ilitera.PCMSO.Report;


namespace Ilitera.Net
{
	/// <summary>
	/// 
	/// </summary>
    public partial class PCICompleto : System.Web.UI.Page
	{


     
		protected void Page_Load(object sender, System.EventArgs e)
		{
			//InicializaWebPageObjects();

            string xIds = "";

            string xUsuario = Session["usuarioLogado"].ToString();

            try
			{

                xIds = Request["xId"].ToString().Trim();

                Ilitera.Data.Clientes_Funcionarios xLista = new Ilitera.Data.Clientes_Funcionarios();

                DataSet zDs = new DataSet();
                zDs = xLista.Gerar_Lista_Exames(Convert.ToInt32(Request["IdEmpregado"].ToString()));

                Int32 zId = 0;
                string zTipo = "";

                int zRelatorios = 0;
                int zPDF = 0;


                for (int zCont = 0; zCont < zDs.Tables[0].Rows.Count; zCont++)
                {

                    zId = System.Convert.ToInt32(zDs.Tables[0].Rows[zCont][0]);
                    zTipo = zDs.Tables[0].Rows[zCont][2].ToString().ToUpper().Trim();

                    if (xIds.IndexOf(zId.ToString().Trim()) >= 0)
                    {

                        if (zTipo == "PERIÓDICO" || zTipo == "MUDANÇA DE FUNÇÃO" || zTipo == "ADMISSIONAL" || zTipo == "DEMISSIONAL" || zTipo == "RETORNO AO TRABALHO")
                        {

                            zRelatorios = zRelatorios + 2;

                        }
                        else if (zTipo == "AUDIOMETRIA")
                        {

                            zRelatorios = zRelatorios + 2;

                        }
                        else
                        {
                            ProntuarioDigital zComp = new ProntuarioDigital();

                            zComp.Find("IdExameBase = " + zId.ToString());

                            if (zComp != null)
                            {
                                //VER SE TEM ANEXO
                                if (zComp.Arquivo.Trim() != "")
                                {
                                    if (zComp.Arquivo.ToUpper().IndexOf("PDF") < 0)
                                    {
                                        zRelatorios = zRelatorios + 1;
                                    }
                                    else
                                    {
                                        zPDF = zPDF + 1;
                                    }
                                }
                            }
                        }

                    }
                }



                CrystalDecisions.CrystalReports.Engine.ReportClass[]
                                   reports = new CrystalDecisions.CrystalReports.Engine.ReportClass[zRelatorios];


                int zAux = 0;
                int zAuxPDF = 0;

                
                Stream[] zPDFstreams = new Stream[zPDF];



                for (int zCont = 0; zCont < zDs.Tables[0].Rows.Count; zCont++)
                {

                    zId = System.Convert.ToInt32(zDs.Tables[0].Rows[zCont][0]);
                    zTipo = zDs.Tables[0].Rows[zCont][2].ToString().ToUpper().Trim();

                    if (xIds.IndexOf(zId.ToString().Trim()) >= 0)
                    {


                        if (zTipo == "PERIÓDICO" || zTipo == "MUDANÇA DE FUNÇÃO" || zTipo == "ADMISSIONAL" || zTipo == "DEMISSIONAL" || zTipo == "RETORNO AO TRABALHO")
                        {
                            //reports[zCont+1] = new DataSourcePCA(laudo).GetReport();
                            Clinico exame = new Clinico(zId);

                            //reports[zCont+1] = new DataSourceExameAsoPci(exame).GetReport();
                            reports[zAux] = new DataSourceExameAsoPci(exame).GetReport();
                            zAux++;

                            //pegar PCI
                            if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().IndexOf("Daiti") > 0 || Ilitera.Data.SQLServer.EntitySQLServer.xDB2.Trim() == "Sied_Novo" )
                            {
                                reports[zAux] = new DataSourceExameAsoPci(exame).GetReportPci_Antigo();
                            }
                            else
                            {
                                reports[zAux] = new DataSourceExameAsoPci(exame).GetReportPci();
                            }
                            zAux++;


                        }
                        else if (zTipo == "AUDIOMETRIA")
                        {
                            Audiometria audiometria = new Audiometria(zId);

                            //pegar anamenese

                            //pegar grafico
                            reports[zAux] = new DataSourceAudiometria(audiometria).GetReport(true);
                            zAux++;
                            reports[zAux] = new DataSourceAudiometria(audiometria).GetReport(false);
                            zAux++;


                        }
                        else
                        {
                            ProntuarioDigital zComp = new ProntuarioDigital();

                            zComp.Find("IdExameBase = " + zId.ToString());

                            if (zComp != null)
                            {
                                //VER SE TEM ANEXO
                                if (zComp.Arquivo.Trim() != "")
                                {
                                    if (zComp.Arquivo.ToUpper().IndexOf("PDF") < 0)
                                    {
                                        reports[zAux] = new DataSourceAnexo(zComp.Arquivo.Trim()).GetReport();
                                        zAux++;
                                    }
                                    else
                                    {
                                        // carregar stream e tentar adicionar ao final do PDFMerge
                                        zPDFstreams[zAuxPDF] = new MemoryStream(Ilitera.Common.Fotos.GetByteFoto_Uri(zComp.Arquivo.Trim()));
                                        zAuxPDF++;
                                    }
                                }
                            }



                        }

                    }
                }

                if (zRelatorios > 0)
                {
                    Funcionalidade funcionalidade = Funcionalidade.GetFuncionalidade(FuncionalidadeTipo.Relatorio, reports.GetType());
                    //ImpressaoDocUsuario.InsereRegistro(usuario, cliente, Convert.ToInt64(report.SummaryInfo.KeywordsInReport), true, funcionalidade);

                    CreatePDFMerged(reports, this.Response, "", false, zPDFstreams);

                    reports = null;
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    GC.Collect();

                }
                else
                {

                    MsgBox1.Show("Ilitera.Net", "Sem dados para gerar relatório.", null,
                       new EO.Web.MsgBoxButton("OK"));
                }


			}
			catch(Exception ex)
			{
                MsgBox1.Show("Ilitera.Net", ex.Message, null,
                       new EO.Web.MsgBoxButton("OK"));

			}
		}



        protected static void CreatePDFMerged(CrystalDecisions.CrystalReports.Engine.ReportClass[] reports, HttpResponse response, string watermark, bool RenumerarPaginas, Stream[] PDFstreams)
        {
            Stream[] streams = new Stream[reports.Length + PDFstreams.Length];

            int i = 0;

            foreach (CrystalDecisions.CrystalReports.Engine.ReportClass report in reports)
            {
                if (RenumerarPaginas)
                    report.ReportDefinition.ReportObjects["PaginaNdeN1"].ObjectFormat.EnableSuppress = true;

                streams[i] = report.ExportToStream(ExportFormatType.PortableDocFormat);

                report.Close();

                i++;
            }

            foreach (Stream zStream in PDFstreams)
            {
                streams[i] = zStream;
                i++;
            }

            CreatePDFMerged(streams, response, watermark, RenumerarPaginas);
        }

        //protected static void CreatePDFMerged(CrystalDecisions.CrystalReports.Engine.ReportClass[] reports, HttpResponse response, string watermark, bool RenumerarPaginas)
        //{
        //    Stream[] streams = new Stream[reports.Length];

        //    int i = 0;

        //    foreach (CrystalDecisions.CrystalReports.Engine.ReportClass report in reports)
        //    {
        //        if (RenumerarPaginas)
        //            report.ReportDefinition.ReportObjects["PaginaNdeN1"].ObjectFormat.EnableSuppress = true;

        //        streams[i] = report.ExportToStream(ExportFormatType.PortableDocFormat);

        //        report.Close();

        //        i++;
        //    }

        //    CreatePDFMerged(streams, response, watermark, RenumerarPaginas);
        //}

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
