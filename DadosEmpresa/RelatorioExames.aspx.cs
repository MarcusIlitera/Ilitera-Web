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
	public partial class RelatorioExames : System.Web.UI.Page
	{
		protected void Page_Load(object sender, System.EventArgs e)
		{
            
            string xId_Empresa;
            string xData1;
            string xData2;
            string xNomeCliente;

            string xTipo;
            string xEmp;
            string xExames;
            string xResultado;


			//InicializaWebPageObjects();
            
            try
			{

                xId_Empresa = Request["IdEmpresa"];
                xData1 = Request["Data1"];
                xData2 = Request["Data2"];
                xNomeCliente = Request["NomeCliente"];
                xTipo = Request["Tipo"].ToString().Trim();
                xEmp = Request["Emp"].ToString().Trim();
                xExames = Request["Exames"].ToString().Trim();
                xResultado = Request["Result"].ToString().Trim();

                string xUsuario = Session["usuarioLogado"].ToString();


                if (xTipo == "A")
                {
                    rptClientesExames report = new DataSourceClientesExames().GetReport(System.Convert.ToInt32(xId_Empresa), xData1, xData2, xNomeCliente, xExames, xResultado, System.Convert.ToInt32(xEmp));
                    CreatePDFDocument(report, this.Response);
                }
                else
                {
                    if (Ilitera.Data.SQLServer.EntitySQLServer._LocalServer.ToString().ToUpper().IndexOf("EY") > 0)
                    {
                        rptClientesExames_Sumarizado_2 report = new DataSourceClientesExames().GetReport_Sumarizado2(System.Convert.ToInt32(xId_Empresa), xData1, xData2, System.Convert.ToInt16(xEmp), xExames, xResultado);
                        CreatePDFDocument(report, this.Response);
                    }
                    else
                    {
                        rptClientesExames_Sumarizado report = new DataSourceClientesExames().GetReport_Sumarizado(System.Convert.ToInt32(xId_Empresa), xData1, xData2, System.Convert.ToInt16(xEmp), xExames, xResultado);
                        CreatePDFDocument(report, this.Response);
                    }
                    
                }
                

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
