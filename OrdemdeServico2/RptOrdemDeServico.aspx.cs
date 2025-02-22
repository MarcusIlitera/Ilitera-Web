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
using Ilitera.OrdemServico.Report;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;


namespace Ilitera.Net.OrdemDeServico
{
	/// <summary>
	///
	/// </summary>
	public partial class RptOrdemDeServico : System.Web.UI.Page
	{

        protected Ilitera.Opsa.Data.Empregado empregado = new Ilitera.Opsa.Data.Empregado();

		protected void Page_Load(object sender, System.EventArgs e)
		{

            string zImpressao = Request["Impressao"];

			//InicializaWebPageObjects();
            empregado.Find(System.Convert.ToInt32(Request["IdEmpregado"].ToString()));

            string xUsuario = Session["usuarioLogado"].ToString();

            string xCabecalho = "ORDEM DE SERVI�O";

            try
            {
                xCabecalho = Request["Cabec"].ToString();
            }
            catch
            {
                xCabecalho = "ORDEM DE SERVI�O";
            }


            if (zImpressao == "V")
            {
                try
                {
                    System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");
                    ReportOrdemDeServico2_Vertical report = new Ilitera.OrdemServico.Report.DataSourceOrdemDeServico(empregado, Convert.ToDateTime(Request["DataOS"], ptBr)).GetReportOrdemDeServico_Vertical(xCabecalho);

                    Funcionalidade funcionalidade = Funcionalidade.GetFuncionalidade(FuncionalidadeTipo.Relatorio, report.GetType());
                    //ImpressaoDocUsuario.InsereRegistro(usuario, cliente, Convert.ToInt64(report.SummaryInfo.KeywordsInReport), true, funcionalidade);

                    CreatePDFDocument(report, this.Response);
                }
                catch (Exception ex)
                {
                    MsgBox1.Show("Ilitera.Net", ex.Message, null,
                           new EO.Web.MsgBoxButton("OK"));

                }
            }
            else
            {

                try
                {
                    System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");
                    ReportOrdemDeServico2 report = new Ilitera.OrdemServico.Report.DataSourceOrdemDeServico(empregado, Convert.ToDateTime(Request["DataOS"], ptBr)).GetReportOrdemDeServico(xCabecalho);

                    Funcionalidade funcionalidade = Funcionalidade.GetFuncionalidade(FuncionalidadeTipo.Relatorio, report.GetType());
                    //ImpressaoDocUsuario.InsereRegistro(usuario, cliente, Convert.ToInt64(report.SummaryInfo.KeywordsInReport), true, funcionalidade);

                    CreatePDFDocument(report, this.Response);
                }
                catch (Exception ex)
                {
                    MsgBox1.Show("Ilitera.Net", ex.Message, null,
                           new EO.Web.MsgBoxButton("OK"));

                }

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
