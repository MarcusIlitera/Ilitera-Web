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
using Ilitera.Opsa.Report;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
//using MestraNET;

namespace Ilitera.Net.ControleEPI
{
    /// <summary>
	/// Summary description.
	/// </summary>
	
	public partial class RelatorioComplTodosEntEPI : WebPageController
	{		
		protected void Page_Load(object sender, System.EventArgs e)
		{
            InicializaWebPageObjects();
            string xUsuario = Session["usuarioLogado"].ToString();

            try
			{
				RptEntregaEPIEmpr report = new DataSourceEPI().GetReportEntregaEPIEmprTodos(cliente);

                //Funcionalidade funcionalidade = Funcionalidade.GetFuncionalidade(FuncionalidadeTipo.Relatorio, report.GetType(), string.Empty, "Todos");
                //ImpressaoDocUsuario.InsereRegistro(usuario, cliente, Convert.ToInt64(report.SummaryInfo.KeywordsInReport), true, funcionalidade);
                
                CreatePDFDocument(report, this.Response);
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
			this.ID = "RelatorioComplTodosEntEPI";

		}
		#endregion
	}
}
