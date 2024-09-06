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
//using MestraNET;

namespace Ilitera.Net.ControleEPI
{
    /// <summary>
	/// Summary description.
	/// </summary>
	
	public partial class RelatorioEntregaEPI : WebPageController
	{		
		protected void Page_Load(object sender, System.EventArgs e)
		{
            InicializaWebPageObjects();
            string xUsuario = Session["usuarioLogado"].ToString();

            try
			{
                string zD1;
                int zEmpr;



                zD1 = Request["D1"].ToString().Trim();//.Substring(0,10);
                zEmpr = System.Convert.ToInt32( Request["IdEmpregado"].ToString().Trim());

                int zIdEPICAEntrega;
                zIdEPICAEntrega = System.Convert.ToInt32(Request["IdEPICAEntrega"].ToString().Trim());

                

                //if (zD1 == "00/00/0000")
                if ( zIdEPICAEntrega != 0 )                
                {
                    EPICAEntregaDetalhe xEntrega = new EPICAEntregaDetalhe();
                    xEntrega.Find(" IdEPICAEntrega = " + zIdEPICAEntrega.ToString());

                    if (xEntrega.Origem == "B")
                    {

                        RptReciboEPI2_Biometria report;
                        report = new ListaEntregaEPI(System.Convert.ToInt32(Request["IdEmpresa"].ToString()), zD1, zD1).GetReportEntregaEPI_Biometria(System.Convert.ToInt32(Request["IdEmpresa"].ToString()), zD1, zD1, zIdEPICAEntrega, zEmpr);

                        CreatePDFDocument(report, this.Response);
                    }
                    else
                    {

                        RptReciboEPI2 report;
                        report = new ListaEntregaEPI(System.Convert.ToInt32(Request["IdEmpresa"].ToString()), zD1, zD1).GetReportEntregaEPI(System.Convert.ToInt32(Request["IdEmpresa"].ToString()), zD1, zD1, zIdEPICAEntrega, zEmpr);

                        CreatePDFDocument(report, this.Response);
                    }

                }
                else
                {
                    RptReciboEPI2 report;
                    report = new ListaEntregaEPI(System.Convert.ToInt32(Request["IdEmpresa"].ToString()), Request["D1"].ToString(), Request["D2"].ToString()).GetReportEntregaEPI(System.Convert.ToInt32(Request["IdEmpresa"].ToString()), Request["D1"].ToString(), Request["D2"].ToString(),0, zEmpr);
                    
                    Funcionalidade funcionalidade = Funcionalidade.GetFuncionalidade(FuncionalidadeTipo.Relatorio, report.GetType());
                    ImpressaoDocUsuario.InsereRegistro(usuario, cliente, Convert.ToInt64(report.SummaryInfo.KeywordsInReport), true, funcionalidade);

                    CreatePDFDocument(report, this.Response);

                }

			}
			catch(Exception ex)
			{                
                MsgBox1.Show("Ilitera.Net", ex.Message, null,
                        new EO.Web.MsgBoxButton("OK"));                

			}
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
