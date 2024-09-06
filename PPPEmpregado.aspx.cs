using System;
using System.Text;
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

using Ilitera.Opsa.Data;
using Ilitera.Common;
using Ilitera.Sied.Report;


//using Ilitera.Net.Documentos;

namespace Ilitera.Net
{
	/// <summary>
	///
	/// </summary>
	public partial class PPPEmpregado : System.Web.UI.Page
	{


        protected Prestador prestador = new Prestador();
        protected Usuario usuario = new Usuario();
        protected Cliente cliente = new Cliente();
        protected Empregado empregado = new Empregado();

        protected void Page_Load(object sender, System.EventArgs e)
        {
            //InicializaWebPageObjects();
            string xUsuario = Session["usuarioLogado"].ToString();

            try
            {
                string zAssinatura = Request["Assinatura"];
                string zRiscos = Request["Riscos"];

                string xPreposto = Session["Preposto"].ToString() + "|";
                if (xPreposto == null) xPreposto = "" + "|";

                string xPreposto2 = Session["Preposto2"].ToString();
                if (xPreposto2 != null) xPreposto = xPreposto + xPreposto2;
                else xPreposto = xPreposto + " ";

                empregado.Find(System.Convert.ToInt32(Request["IdEmpregado"].ToString()));

                empregado.nID_EMPR.Find();
                empregado.nID_EMPR.IdGrupoEmpresa.Find();

                Int32 zIdGrupo = empregado.nID_EMPR.IdGrupoEmpresa.Id;


                if (Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper().IndexOf("GLOBAL") > 0 &&
                    (zIdGrupo == -359564237 || zIdGrupo == 518994214 || zIdGrupo == 1635302153 || zIdGrupo == -987166118 || zIdGrupo == -1551657879))
                {
                    ReportPPP99_Global report = new PPP99(empregado, xPreposto, zAssinatura, zRiscos).GetReportPPP_Global();
                    Funcionalidade funcionalidade = Funcionalidade.GetFuncionalidade(FuncionalidadeTipo.Relatorio, report.GetType());
                    //ImpressaoDocUsuario.InsereRegistro(usuario, cliente, Convert.ToInt64(report.SummaryInfo.KeywordsInReport), true, funcionalidade);

                    CreatePDFDocument(report, this.Response);
                }
                else
                {
                    ReportPPP99 report = new PPP99(empregado, xPreposto, zAssinatura, zRiscos).GetReportPPP();
                    Funcionalidade funcionalidade = Funcionalidade.GetFuncionalidade(FuncionalidadeTipo.Relatorio, report.GetType());
                    //ImpressaoDocUsuario.InsereRegistro(usuario, cliente, Convert.ToInt64(report.SummaryInfo.KeywordsInReport), true, funcionalidade);

                    CreatePDFDocument(report, this.Response);
                }


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
