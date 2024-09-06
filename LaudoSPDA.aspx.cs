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
using Ilitera.SPDA.Report;



namespace Ilitera.Net
{
	/// <summary>
	/// 
	/// </summary>
    public partial class LaudoSPDA : System.Web.UI.Page
	{


        protected Prestador prestador = new Prestador();
        protected Usuario usuario = new Usuario();
        protected Cliente cliente = new Cliente();
        protected Empregado empregado = new Empregado();

		protected void Page_Load(object sender, System.EventArgs e)
		{

            string xUsuario = Session["usuarioLogado"].ToString();

            try
            {
                
                Int32 zTamanho = 0;
                string zTipo = "O";
                
                Ilitera.Opsa.Data.LaudoSPDA zLaudo = new Ilitera.Opsa.Data.LaudoSPDA();  
                zLaudo.Find(System.Convert.ToInt32(Request["IdLaudoSPDA"].ToString()));


                if (zLaudo.Anexo1 != null)
                {
                    if (zLaudo.Anexo1.Trim() != "" || zLaudo.Anexo2.Trim() != "" || zLaudo.Anexo3.Trim() != "" || zLaudo.Anexo4.Trim() != "") zTamanho++;
                }





                int i = 0;

                Ilitera.Opsa.Data.Cliente zCliente = new Ilitera.Opsa.Data.Cliente();

                zCliente.Find(System.Convert.ToInt32(Session["Empresa"].ToString()));


                //DataSet rDs = new LaudoEletrico_Adequacao().Get(" IdLaudoEletrico = " + lbl_ID.Text);

                //if ( rDs.Tables[0].Rows.Count > 0 )
                //{
                // DataSet zDs = new LaudoEletrico_Adequacao_Item().Get(" IdLaudoEletrico_Adequacao = " + rDs.Tables[0].Rows[0]["IdLaudoEletrico_Adequacao"].ToString() + " order by Ordem ");

                DataSet zDs = new SPDA_Adequacao_Item2().Get(" IdSPDA_Adequacao in ( Select IdSPDA_Adequacao from SPDA_Adequacao2 where IdSPDA = " + Request["IdLaudoSPDA"].ToString() + " ) ");

                if (zDs.Tables[0].Rows.Count > 0)
                {
                    zTipo = "N";
                    zTamanho = zTamanho + 4;
                }
                else
                {
                    zTamanho = 2;
                }
                //}

                



                CrystalDecisions.CrystalReports.Engine.ReportClass[]
                    reports = new CrystalDecisions.CrystalReports.Engine.ReportClass[zTamanho];

                //definir se vai ser o relatório antigo ou o novo, se usou adequacoes novas
                if (zTipo == "N")
                {
                    reports[i++] = new DataSourceLaudoSPDA2(zCliente, zLaudo.Id).GetReport_Intro();
                    reports[i++] = new DataSourceLaudoSPDA2(zCliente, zLaudo.Id).GetReport();
                    reports[i++] = new DataSourceLaudoSPDA2(zCliente, zLaudo.Id).GetReport_Final();
                    reports[i++] = new DataSourceLaudoSPDA2(zCliente, zLaudo.Id).GetReport_Conclusao();

                    if (zLaudo.Anexo1 != null)
                    {


                        if (zLaudo.Anexo1.Trim() != "" || zLaudo.Anexo2.Trim() != "" || zLaudo.Anexo3.Trim() != "" || zLaudo.Anexo4.Trim() != "")
                        {
                            zLaudo.IdCliente.Find();

                            reports[i++] = new Ilitera.Sied.Report.DataSourcePPRAAnexo(zLaudo.IdCliente).GetReport_LaudoSPDA(zLaudo.Id);
                        }
                    }


                }
                else
                {
                    reports[i++] = new DataSourceLaudoSPDA(zCliente, zLaudo.Id).GetReport();
                    reports[i++] = new DataSourceLaudoSPDA(zCliente, zLaudo.Id).GetReport_Conclusao();
                }


                




                CreatePDFMerged(reports, this.Response, "", true);

                reports = null;
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();


                //FormShowReport form = new FormShowReport(reports, "Laudo Elétrico", string.Empty, true, zCliente);
                //form.Show(this);




            }
            catch (Exception ex)
            {
                MsgBox1.Show("Ilitera.Net", ex.Message, null,
                       new EO.Web.MsgBoxButton("OK"));

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
