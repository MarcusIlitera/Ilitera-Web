using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Ilitera.Opsa.Data;
using Ilitera.PCMSO.Report;
using System.IO;
using Ilitera.Common;
using Ilitera.ASO.Report;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using Entities;
using BLL;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;


namespace Ilitera.Net.PCMSO
{
    public partial class AnotacoesPCI : System.Web.UI.Page
    {
        

        protected void Page_Load(object sender, System.EventArgs e)
		{
            //InicializaWebPageObjects();
            string xUsuario = Session["usuarioLogado"].ToString();

            Ilitera.Opsa.Data.Empregado empregado = new Ilitera.Opsa.Data.Empregado();
		
			try
			{
                Clinico exame = new Clinico(Convert.ToInt32(Request["IdExame"]));

				if (exame.IdPcmso.Id.Equals(0) || exame.IdEmpregadoFuncao.Id.Equals(0))
				{
                    empregado.Find(" tblEMPREGADO.nID_EMPREGADO = " + Request["IdEmpregado"].ToString());

					exame.IdEmpregadoFuncao = EmpregadoFuncao.GetEmpregadoFuncao(empregado);
                    exame.IdPcmso = exame.IdEmpregadoFuncao.nID_EMPR.GetUltimoPcmso();
                    
                    exame.UsuarioId = System.Convert.ToInt32(  Request["IdUsuario"].ToString()); //usuario.Id;

                    exame.Save();
				}

                Juridica xClin = new Juridica();
                xClin.Find(exame.IdJuridica.Id);

                string xClinNome = "";

                if (xClin != null) xClinNome = xClin.NomeAbreviado.ToUpper().Trim();



                //if (Ilitera.Data.SQLServer.EntitySQLServer._LocalServer.ToString().IndexOf("BENE") >= 0)   //pci com testes especiais
                //{

                //    CrystalDecisions.CrystalReports.Engine.ReportClass[] reports = new ReportClass[2];

                //    RptPci report3 = new DataSourceExameAsoPci(exame).GetReportPci_Antigo();
                //    reports[0] = report3;

                //    Int32 xIdExame = exame.Id;


                //    RptTestesEspeciais report4 = new DataSourceTestesEspeciais(xIdExame, false).GetReport();

                //    reports[1] = report4;

                //    CreatePDFMerged(reports, this.Response, "", false);

                //    reports = null;
                //    GC.Collect();
                //    GC.WaitForPendingFinalizers();
                //    GC.Collect();

                //}
                //else 
                if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("PRAJNA") > 0)
                {

                    CrystalDecisions.CrystalReports.Engine.ReportClass[] reports = new ReportClass[2];

                    reports[0] = new DataSourceExameAsoPci_Novo(exame).GetReportPciUnico_Essence(false);


                    Int32 xIdExame = exame.Id;

                    Carregar_Dados_Anamnese_Exame(exame.Id);

                    reports[1] = new DataSourceExameAsoPci_Novo(exame).GetReport_Anamnese(true);

                    CreatePDFMerged(reports, this.Response, "", false);

                    reports = null;
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    GC.Collect();
                }
                else if (Ilitera.Data.SQLServer.EntitySQLServer._LocalServer.ToString().ToUpper().IndexOf("EY") > 0)
                {


                    exame.IdEmpregado.Find();
                    exame.IdEmpregado.nID_EMPR.Find();
                    exame.IdEmpregado.nID_EMPR.IdGrupoEmpresa.Find();



                    CrystalDecisions.CrystalReports.Engine.ReportClass[] reports = new ReportClass[2];

                    RptPci_Novo_EY2 report3 = new DataSourceExameAsoPci(exame).GetReportPciEY2();
                    reports[0] = report3;

                    Int32 xIdExame = exame.Id;

                    Carregar_Dados_Anamnese_Exame(exame.Id);

                    RptAnamnese report4 = new DataSourceExameAnamnese(xIdExame, false).GetReport();
                    reports[1] = report4;

                    CreatePDFMerged(reports, this.Response, "", false);

                    reports = null;
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    GC.Collect();

                }
                //else if ( Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("VIA") > 0 || Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper().IndexOf("_SHO") > 0 || (Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper().Trim() == "SIED_NOVO" && Ilitera.Data.SQLServer.EntitySQLServer._LocalServer.Trim() == "54.94.157.244") ||
                //         Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper().IndexOf("_UNO") > 0 || Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper().IndexOf("_DAITI") > 0 || Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper().IndexOf("_GLOBAL") > 0 || Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper().IndexOf("_SDTOURIN") > 0 || Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper().IndexOf("_FOX") > 0 ||
                //         (Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper().IndexOf("_SAFETY") > 0 && Ilitera.Data.SQLServer.EntitySQLServer._LocalServer.Trim() == "54.94.157.244")     )
                else
                {

                    CrystalDecisions.CrystalReports.Engine.ReportClass[] reports = new ReportClass[1];

                    //PCI
                    reports[0] = new DataSourceExameAsoPci_Novo(exame).GetReportPciUnico(false);

                    // reports[5] = new DataSourceGuia_Prajna(cliente, System.Convert.ToInt32(xId_Empregado), System.Convert.ToInt32(xId_Empresa), System.Convert.ToInt32(xId_Clinica), xExames, xExames2, xExames3, xExames4, xData_Exame, xHora_Exame, xTipo, xBasico, xObs, xImpDt, exame2.IdEmpregadoFuncao.Id).GetReport_Nova();

                    CreatePDFMerged(reports, this.Response, "", false);

                    reports = null;
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    GC.Collect();


                }
                //else if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().IndexOf("Daiti") > 0 || Ilitera.Data.SQLServer.EntitySQLServer.xDB2.Trim() == "Sied_Novo" || xClinNome.IndexOf("DAITI") >= 0 || xClinNome.IndexOf("IPATINGA") >= 0)
                //{
                //    RptPci report = new DataSourceExameAsoPci(exame).GetReportPci_Antigo();
                //    Funcionalidade funcionalidade = Funcionalidade.GetFuncionalidade(FuncionalidadeTipo.Relatorio, report.GetType());
                //    CreatePDFDocument(report, this.Response);

                //    report.Dispose();
                //    GC.Collect();
                //    GC.WaitForPendingFinalizers();
                //    GC.Collect();

                //}
                //else
                //{

                //    if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("PRAJNA") > 0)
                //    {

                //        CrystalDecisions.CrystalReports.Engine.ReportClass[] reports = new ReportClass[2];

                //        reports[0] = new DataSourceExameAsoPci_Novo(exame).GetReportPciUnico_Essence(false);


                //        Int32 xIdExame = exame.Id;

                //        Carregar_Dados_Anamnese_Exame(exame.Id);

                //        reports[1] = new DataSourceExameAsoPci_Novo(exame).GetReport_Anamnese(true);

                //        CreatePDFMerged(reports, this.Response, "", false);

                //        reports = null;
                //        GC.Collect();
                //        GC.WaitForPendingFinalizers();
                //        GC.Collect();
                //    }
                    //else if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("PRAJNA") > 0) // || exame.IdEmpregado.nID_EMPR.IdGrupoEmpresa.Descricao.ToUpper().Trim() == "CAPGEMINI"  ) // && Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("_HOM") > 0)  //.Id == -905238295)  // Capgemini
                    //{

                    //    CrystalDecisions.CrystalReports.Engine.ReportClass[] reports = new ReportClass[2];

        
                    //    RptPci_Novo_Capgemini report3 = new DataSourceExameAsoPci(exame).GetReportPciCapgemini();
                    //    reports[0] = report3;
                        

                    //    Int32 xIdExame = exame.Id;

                    //    Carregar_Dados_Anamnese_Exame(exame.Id);

                    //    RptAnamnese report4 = new DataSourceExameAnamnese(xIdExame, false).GetReport();
                    //    reports[1] = report4;

                    //    CreatePDFMerged(reports, this.Response, "", false);

                    //    reports = null;
                    //    GC.Collect();
                    //    GC.WaitForPendingFinalizers();
                    //    GC.Collect();


                    //}
                    //else if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("GLOBAL") > 0)
                    //{
                    //    RptPci_Novo_Global report = new DataSourceExameAsoPci(exame).GetReportPci_Global();
                    //    Funcionalidade funcionalidade = Funcionalidade.GetFuncionalidade(FuncionalidadeTipo.Relatorio, report.GetType());
                    //    CreatePDFDocument(report, this.Response);

                    //    report.Dispose();
                    //    GC.Collect();
                    //    GC.WaitForPendingFinalizers();
                    //    GC.Collect();

                    //}
                    //else
                    //{
                    //    RptPci_Novo report = new DataSourceExameAsoPci(exame).GetReportPci();
                    //    Funcionalidade funcionalidade = Funcionalidade.GetFuncionalidade(FuncionalidadeTipo.Relatorio, report.GetType());
                    //    CreatePDFDocument(report, this.Response);

                    //    report.Dispose();
                    //    GC.Collect();
                    //    GC.WaitForPendingFinalizers();
                    //    GC.Collect();

                    //}
                //}
                

			}
			catch (Exception ex)
			{
                MsgBox1.Show("Ilitera.Net", ex.Message, null,
                       new EO.Web.MsgBoxButton("OK"));

			}
		}


        private void Carregar_Dados_Anamnese_Exame(Int32 zExame)
        {


            if (zExame == 0)
            {
                return;
            }



            List<Anamnese_Exame> AnExame = new Anamnese_Exame().Find<Anamnese_Exame>(" IdExameBase = " + zExame);


            Clinico vExame = new Clinico();
            vExame.Find(zExame);


            if (AnExame.Count == 0)
            {
                //trazer padrão para cliente
                vExame.IdEmpregado.Find();
                vExame.IdEmpregado.nID_EMPR.Find();
                List<Anamnese_Dinamica> anExamePadrao = new Anamnese_Dinamica().Find<Anamnese_Dinamica>(" IdPessoa = " + vExame.IdEmpregado.nID_EMPR.Id);


                if (anExamePadrao.Count == 0)
                {
                    return;
                }
                else
                {

                    Ilitera.Data.Clientes_Funcionarios xAnam = new Ilitera.Data.Clientes_Funcionarios();
                    xAnam.Carregar_Anamnese_Dinamica(vExame.IdEmpregado.nID_EMPR.Id, vExame.Id);

                    //foreach (Anamnese_Dinamica zPadrao in anExamePadrao)
                    //{
                    //    Anamnese_Exame rTestes = new Anamnese_Exame();

                    //    rTestes.IdAnamneseDinamica = zPadrao.Id;
                    //    rTestes.IdExameBase = vExame.Id;
                    //    rTestes.Resultado = "N";
                    //    rTestes.Peso = zPadrao.Peso;
                    //    rTestes.Save();
                    //}

                }

            }


            return;


        }



        protected void CreatePDFMerged(CrystalDecisions.CrystalReports.Engine.ReportClass[] reports, HttpResponse response, string watermark, bool RenumerarPaginas)
        {
            Stream[] streams = new Stream[reports.Length];

            int i = 0;

            foreach (CrystalDecisions.CrystalReports.Engine.ReportClass report in reports)
            {
                if (RenumerarPaginas)
                    report.ReportDefinition.ReportObjects["PaginaNdeN1"].ObjectFormat.EnableSuppress = true;

                streams[i] = report.ExportToStream(ExportFormatType.PortableDocFormat);

                report.Close();

                i++;
            }

            CreatePDFMerged(streams, response, watermark, RenumerarPaginas);
        }


        protected static void CreatePDFMerged(Stream[] streams, HttpResponse response, string watermark, bool RenumerarPaginas)
        {
            MemoryStream reportStream = PdfSharpUtility.MergeReports(streams, string.Empty, watermark, RenumerarPaginas);

            ShowPdfDocument(response, reportStream);
        }

        protected static void ShowPdfDocument(HttpResponse response, MemoryStream reportStream)
        {
            response.Clear();
            response.ContentType = "application/pdf";
            response.AddHeader("content-length", reportStream.Length.ToString());
            response.BinaryWrite(reportStream.ToArray()); ;
            response.Flush();
            reportStream.Close();
            response.End();
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
