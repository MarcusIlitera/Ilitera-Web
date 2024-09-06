using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

using Ilitera.PCMSO.Report;
using Ilitera.Opsa.Data;
using Ilitera.Common;
using Ilitera.Opsa.Report;

//using MestraNET;

namespace Ilitera.Net.DadosEmpresa
{
	/// <summary>
	/// 
	/// </summary>
	public partial class RelatorioMailing : System.Web.UI.Page
	{
		protected void Page_Load(object sender, System.EventArgs e)
		{
            
            string xId_Empresa;
            string xData1;
            string xData2;
            string xNomeCliente;

            //string xTipo;
            Int16 xEmp;
            //string xExames;

            Int16 zAttach = 0;
            Int16 zSemAgendamento = 0;
            Int16 zSemResultado = 0;
            Int16 zSemEnvio = 0;



            //InicializaWebPageObjects();

            try
            {

                xId_Empresa = Request["IdEmpresa"];
                xData1 = Request["Data1"];
                xData2 = Request["Data2"];
                xNomeCliente = Request["NomeCliente"];                
                xEmp = System.Convert.ToInt16(Request["Emp"].ToString().Trim());

                zAttach = System.Convert.ToInt16(Request["zAttach"].ToString().Trim());
                zSemEnvio = System.Convert.ToInt16(Request["zSemEnvio"].ToString().Trim());
                zSemAgendamento= System.Convert.ToInt16(Request["zSemAgendamento"].ToString().Trim());
                zSemResultado = System.Convert.ToInt16(Request["zSemResultado"].ToString().Trim());

                string xUsuario = Session["usuarioLogado"].ToString();

                rptMailing report = new DataSourceClientesExames().GetReport_Mailing(System.Convert.ToInt32(xId_Empresa), xData1, xData2, xEmp, zAttach, zSemAgendamento, zSemEnvio, zSemResultado);
                CreatePDFDocument(report, this.Response);
               
            
                //Funcionalidade funcionalidade = Funcionalidade.GetFuncionalidade(FuncionalidadeTipo.Relatorio, report.GetType());

				             
			}
			catch(Exception ex)
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
