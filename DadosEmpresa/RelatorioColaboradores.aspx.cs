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
	public partial class RelatorioColaboradores : System.Web.UI.Page
	{
		protected void Page_Load(object sender, System.EventArgs e)
		{
            
            string xId_Empresa;
            string xData1;
            string xData2;
            string xNomeCliente;
            string xDefic;
            string xAtivosEm;
            string xSemEmail;
            string xInconsistencia;
            string xClassif;

            string xTipo;
            string xEmp;
            string xSit;

			//InicializaWebPageObjects();

            try
            {

                xId_Empresa = Request["IdEmpresa"].ToString().Trim();
                xData1 = Request["Data1"].ToString().Trim();
                xData2 = Request["Data2"].ToString().Trim();
                xNomeCliente = Request["NomeCliente"].ToString().Trim();
                xTipo = Request["Tipo"].ToString().Trim();
                xEmp = Request["Emp"].ToString().Trim();
                xSit = Request["Sit"].ToString().Trim();
                xDefic = Request["Defic"].ToString().Trim();
                xAtivosEm = Request["AtivosEm"].ToString().Trim();
                xSemEmail = Request["SemEmail"].ToString().Trim();
                xInconsistencia = Request["xInconsistencia"].ToString().Trim();
                xClassif = Request["Classif"].ToString().Trim();

                string xUsuario = Session["usuarioLogado"].ToString();
                

                string zFiltro = "";

                if (xEmp == "1")   //empresa
                {
                    zFiltro = "  " + xId_Empresa + "  ";
                }
                else if (xEmp == "2")   //empresa            
                {
                    zFiltro = "   select idpessoa from " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.juridica where idjuridica = " + xId_Empresa + " or idjuridicapai = " + xId_Empresa + "  ";
                }
                else if (xEmp == "3")   //empresa            
                {
                    zFiltro = "  select idpessoa from " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.juridica where idgrupoempresa in ( select idgrupoempresa from " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.juridica where idpessoa = " + xId_Empresa + " )  ";
                }
                else
                {
                    zFiltro = "  " + xId_Empresa + "  ";
                }




                //if (xTipo == "A")
                //{

                if ( xTipo == "APT")
                {
                    rptColaboradoresAptidoes report = new DataSourceColaboradores().GetReport_Aptidao(System.Convert.ToInt32(xId_Empresa));
                    CreatePDFDocument(report, this.Response);
                }
                else if (xTipo == "PCD")
                {
                    rptPCD report = new DataSourcePCD().GetReport(System.Convert.ToInt32(xId_Empresa), xData1, xNomeCliente, xEmp, zFiltro, xData2, xSit);
                    CreatePDFDocument(report, this.Response);
                }
                else if (xTipo == "PCDA")
                {
                    rptPCD_Sumarizado report = new DataSourcePCD().GetReport_Sum(System.Convert.ToInt32(xId_Empresa), xData1, xNomeCliente, xEmp, zFiltro, xData2, xSit);
                    CreatePDFDocument(report, this.Response);
                }
                else
                {
                    rptColaboradores report = new DataSourceColaboradores().GetReport(System.Convert.ToInt32(xId_Empresa), xData1, xNomeCliente, xEmp, zFiltro, xSit, xTipo, xDefic, xAtivosEm, xSemEmail, xInconsistencia, xClassif);
                    CreatePDFDocument(report, this.Response);
                }




                //Funcionalidade funcionalidade = Funcionalidade.GetFuncionalidade(FuncionalidadeTipo.Relatorio, report.GetType());



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
