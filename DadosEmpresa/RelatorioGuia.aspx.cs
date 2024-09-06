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
using Ilitera.Sied.Report;

//using MestraNET;

namespace Ilitera.Net
{
	/// <summary>
	/// 
	/// </summary>
	public partial class RelatorioGuia : System.Web.UI.Page
	{

        protected Prestador prestador = new Prestador();
        protected Usuario usuario = new Usuario();
        protected Cliente cliente = new Cliente();
        protected Empregado empregado = new Empregado();

		protected void Page_Load(object sender, System.EventArgs e)
		{
            string xId_Empregado;
            string xId_Empresa;
            string xId_Clinica;
            string xExames;
            string xExames2;
            string xExames3;
            string xExames4;
            string xData_Exame;
            string xHora_Exame;
            string xTipo;
            string xBasico;
            string xObs;
            string xImpDt;
            string xIdEmprFunc;

            //InicializaWebPageObjects();

            try
			{

                xId_Empregado = Request["IdEmpregado"];
                xId_Empresa = Request["IdEmpresa"];
                xId_Clinica = Request["IdClinica"];
                xExames = Request["E1"];
                xExames2 = Request["E2"];
                xExames3 = Request["E3"];
                xExames4 = Request["E4"];
                xData_Exame = Request["D_E"];
                xHora_Exame = Request["H_E"];
                xTipo = Request["Tipo"];
                xBasico = Request["Basico"];
                xImpDt = xBasico = Request["ImpDt"];
                xObs = Session["Obs_Guia"].ToString().Trim();
                xIdEmprFunc = Request["IdEmprFunc"];


                string xUsuario = Session["usuarioLogado"].ToString();

                //StringBuilder st = new StringBuilder();

                //st.Append("nID_EMPR=" + Session["Empresa"]);

                //st.Append(" AND gTERCEIRO=0");

                //st.Append(" ORDER BY tNO_EMPG");

                //ArrayList listEmpregado = new Empregado().Find(st.ToString());

                //if(listEmpregado.Count > 0)
                //{

                Cliente zCliente = new Cliente();
                zCliente.Find(System.Convert.ToInt32(xId_Empresa));


                if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().IndexOf("Prajna") > 0)
                {
                    //RptGuia_Prajna report = new DataSourceGuia_Prajna(cliente, System.Convert.ToInt32(xId_Empregado), System.Convert.ToInt32(xId_Empresa), System.Convert.ToInt32(xId_Clinica), xExames, xExames2, xExames3, xExames4, xData_Exame, xHora_Exame, xTipo, xBasico, xObs, xImpDt).GetReport();
                    RptGuia_Nova_Prajna report = new DataSourceGuia_Prajna(cliente, System.Convert.ToInt32(xId_Empregado), System.Convert.ToInt32(xId_Empresa), System.Convert.ToInt32(xId_Clinica), xExames, xExames2, xExames3, xExames4, xData_Exame, xHora_Exame, xTipo, xBasico, xObs, xImpDt, System.Convert.ToInt32(xIdEmprFunc)).GetReport_Nova();
                    CreatePDFDocument(report, this.Response);
                }              
                else
                {
                    //RptGuia report = new DataSourceGuia(cliente, System.Convert.ToInt32(xId_Empregado), System.Convert.ToInt32(xId_Empresa), System.Convert.ToInt32(xId_Clinica), xExames, xExames2, xExames3, xExames4, xData_Exame, xHora_Exame, xTipo, xBasico, xObs, xImpDt).GetReport();
                    RptGuia_Nova report = new DataSourceGuia(cliente, System.Convert.ToInt32(xId_Empregado), System.Convert.ToInt32(xId_Empresa), System.Convert.ToInt32(xId_Clinica), xExames, xExames2, xExames3, xExames4, xData_Exame, xHora_Exame, xTipo, xBasico, xObs, xImpDt, System.Convert.ToInt32(xIdEmprFunc)).GetReport_Nova();
                    CreatePDFDocument(report, this.Response);
                }


                //Funcionalidade funcionalidade = Funcionalidade.GetFuncionalidade(FuncionalidadeTipo.Relatorio, report.GetType());
                    //ImpressaoDocUsuario.InsereRegistro(usuario, cliente, Convert.ToInt64(report.SummaryInfo.KeywordsInReport), true, funcionalidade);

				
                //}		
                //else
                //    throw new Exception("Problema na geração da guia !");
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
