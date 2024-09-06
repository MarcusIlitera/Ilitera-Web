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
using System.Text;


namespace Ilitera.Net
{
	/// <summary>
	/// 
	/// </summary>
	public partial class LTCAT : System.Web.UI.Page
	{
        protected Prestador prestador = new Prestador();
        protected Usuario usuario = new Usuario();
        protected Cliente cliente = new Cliente();
        protected Empregado empregado = new Empregado();

		protected void Page_Load(object sender, System.EventArgs e)
		{
            //InicializaWebPageObjects(true);

            string xUsuario = Session["usuarioLogado"].ToString();

            ArrayList alEmpregados = new ArrayList();

            if (Request["IdEmpregado"] != null) empregado.Find(System.Convert.ToInt32(Request["IdEmpregado"].ToString()));

            //if (!empregado.Id.Equals(0))
                alEmpregados = new Empregado().Find("nID_EMPREGADO=" + empregado.Id);
            //else
            //    alEmpregados = new Empregado().Find("nID_EMPR=" + cliente.Id + " AND nID_EMPREGADO IN"
            //        + " (SELECT nID_EMPREGADO FROM tblEMPREGADO_FUNCAO WHERE nID_EMPREGADO_FUNCAO IN"
            //        + " (SELECT nID_EMPREGADO_FUNCAO FROM tblFUNC_EMPREGADO) AND nID_FUNCAO IS NOT NULL AND nID_SETOR IS NOT NULL)"
            //        + " AND gTERCEIRO=0"
            //        + " ORDER BY tNO_EMPG");

            EmpregadoFuncao empregadoFuncao = new EmpregadoFuncao();
            LaudoTecnico laudoTecnico = new LaudoTecnico();

            if (Request["IdClassificacaoFuncional"] != null)
            {
                empregadoFuncao.Find(Convert.ToInt32(Request["IdClassificacaoFuncional"]));
                laudoTecnico.Find(Convert.ToInt32(Request["IdLaudoTecnico"]));
            }

            try
            {
                ReportClass report = new ReportClass();
                Funcionalidade funcionalidade = new Funcionalidade();

                if (Request["TipoLTCAT"] == "PPRA")
                {
                    if (Request["IdClassificacaoFuncional"] != null)
                        report = new DataSourcePPRAEmpregado(empregadoFuncao, laudoTecnico, (int)IndTipoPPRA.PPRA).GetReport();
                    else
                        report = new DataSourcePPRAEmpregado(alEmpregados, (int)IndTipoPPRA.PPRA).GetReport();

                    funcionalidade = Funcionalidade.GetFuncionalidade(FuncionalidadeTipo.Relatorio, report.GetType());
                }
                else if (Request["TipoLTCAT"] == "LTCAT")
                {
                    if (Request["IdClassificacaoFuncional"] != null)
                        report = new DataSourcePPRAEmpregado(empregadoFuncao, laudoTecnico, (int)IndTipoPPRA.LTCAT).GetReport();
                    else
                        report = new DataSourcePPRAEmpregado(alEmpregados, (int)IndTipoPPRA.LTCAT).GetReport();

                    if (report.GetType().Equals(typeof(RptPPRAEmpregadoPrevidenciario)))
                        funcionalidade = Funcionalidade.GetFuncionalidade(FuncionalidadeTipo.Relatorio, report.GetType());
                    else
                        funcionalidade = Funcionalidade.GetFuncionalidade(FuncionalidadeTipo.Relatorio, report.GetType(), string.Empty, "LTCAT");
                }
                else
                {
                    if (Request["IdClassificacaoFuncional"] != null)
                        report = new DataSourcePPRAEmpregado(empregadoFuncao, laudoTecnico, (int)IndTipoPPRA.LTCAT).GetReport();
                    else
                        report = new DataSourcePPRAEmpregado(alEmpregados, (int)IndTipoPPRA.LTCAT).GetReport();

                    if (report.GetType().Equals(typeof(RptPPRAEmpregadoPrevidenciario)))
                        funcionalidade = Funcionalidade.GetFuncionalidade(FuncionalidadeTipo.Relatorio, report.GetType());
                    else
                        funcionalidade = Funcionalidade.GetFuncionalidade(FuncionalidadeTipo.Relatorio, report.GetType(), string.Empty, "LTCAT");
                }

                //ImpressaoDocUsuario.InsereRegistro(usuario, cliente, Convert.ToInt64(report.SummaryInfo.KeywordsInReport), true, funcionalidade);

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


        protected static string strOpenReport(string directory, string fileAndQuery, string ReportName)
        {
            return strOpenReport(directory, fileAndQuery, ReportName, false);
        }

        protected static string strOpenReport(string directory, string fileAndQuery, string ReportName, bool useDirectoryForLocalProcess)
        {
            StringBuilder st = new StringBuilder();

            Guid strAux = Guid.NewGuid();

            string valueProcess = "Local";

            if (valueProcess.Equals("Remote"))
                st.Append("void(window.open('http://report.ilitera.net/index.aspx?Identity=" + strAux.ToString() + "opsa" + strAux.ToString()
                    + "&Key=" + strAux.ToString() + "IliteraNet" + strAux.ToString()
                    + "&PathAndQuery=" + HttpUtility.UrlEncode(directory + "/" + fileAndQuery) + "', '" + ReportName + "', 'scrollbars=yes, resizable=yes, toolbar=no, addressbar=no, menubar=no, width=800px, height=600px').focus());");
            else if (valueProcess.Equals("Local"))
            {
                if (useDirectoryForLocalProcess)
                {
                    st.AppendFormat("void(window.open('../{0}/{1}', '{2}', 'scrollbars=yes, resizable=yes, toolbar=no, addressbar=no, menubar=no, width=800px, height=600px').focus());", directory, fileAndQuery, ReportName);
                }
                else
                {
                    st.AppendFormat("void(window.open('{0}','{1}', 'scrollbars=yes, resizable=yes, toolbar=no, addressbar=no, menubar=no, width=800px, height=600px').focus());", fileAndQuery, ReportName);
                }
            }

            return st.ToString();
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
