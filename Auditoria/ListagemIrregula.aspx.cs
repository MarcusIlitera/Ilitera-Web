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
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Text;

using Ilitera.Auditoria.Report;

namespace Ilitera.Net
{
	/// <summary>
	
	/// </summary>
    public partial class ListagemIrregula : System.Web.UI.Page
	{
		protected void Page_Load(object sender, System.EventArgs e)
		{
            //InicializaWebPageObjects();
            string xUsuario = Session["usuarioLogado"].ToString();

            try
			{
                Ilitera.Opsa.Data.Auditoria auditoria = new Ilitera.Opsa.Data.Auditoria(Convert.ToInt32(Request.QueryString["IdAuditoria"]));
				
				string tipoListagem = string.Empty;

				switch (Request.QueryString["TipoListagem"])
				{
					case "T":
						tipoListagem = "Todas";
						break;
					case "R":
						tipoListagem = "Regularizadas";
						break;
					case "P":
						tipoListagem = "Pendentes";
						break;
				}


                Cliente cliente;
                cliente = new Cliente(System.Convert.ToInt32(Session["Empresa"].ToString()));

                Usuario usuario = new Usuario();
                usuario.Find(System.Convert.ToInt32(Request["IdUsuario"].ToString()));

				RptListaIrregularidades report = new DataSourceIrregularidades(auditoria, cliente, tipoListagem).GetReportListaIrregularidades();

                Funcionalidade funcionalidade = Funcionalidade.GetFuncionalidade(FuncionalidadeTipo.Relatorio, report.GetType());

                ImpressaoDocUsuario.InsereRegistro(usuario, cliente, Convert.ToInt64(report.SummaryInfo.KeywordsInReport), true, funcionalidade);

                CreatePDFDocument(report, this.Response);
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
			this.ID = "ListagemIrregula";

		}
		#endregion
	}
}
