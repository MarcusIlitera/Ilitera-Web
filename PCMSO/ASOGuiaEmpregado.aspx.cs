
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Ilitera.Opsa.Data;
using Ilitera.Common;
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


using System.Collections;
using System.ComponentModel;
using System.Drawing;

using System.IO;

using Entities;
using BLL;
using Ilitera.PCMSO.Report;
using Ilitera.Sied.Report;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;


//using MestraNET;


namespace Ilitera.Net.PCMSO
{
    public partial class ASOGuiaEmpregado : System.Web.UI.Page
    {



        protected Prestador prestador = new Prestador();
        protected Ilitera.Common.Usuario usuario = new Ilitera.Common.Usuario();
        protected Cliente cliente = new Cliente();
        //protected Ilitera.Opsa.Data.Empregado empregado = new Ilitera.Opsa.Data.Empregado();        

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

			//InicializaWebPageObjects();
            
          
            try
            {

                xId_Empregado = Request["IdEmpregado"];
                xId_Empresa = Request["IdEmpresa"];
                xId_Clinica = Request["IdClinica"];
                xExames = Request["Exames"];
                xExames2 = Request["Exames2"];
                xExames3 = Request["Exames3"];
                xExames4 = Request["Exames4"];
                xData_Exame = Request["Data_Exame"];
                xHora_Exame = Request["Hora_Exame"];
                xTipo = Request["Tipo"];
                xBasico = Request["Basico"];
                xImpDt = Request["ImpDt"];
                xObs = ""; //Session["Obs_Guia"].ToString().Trim();

                //StringBuilder st = new StringBuilder();

                //st.Append("nID_EMPR=" + Session["Empresa"]);

                //st.Append(" AND gTERCEIRO=0");

                //st.Append(" ORDER BY tNO_EMPG");

                //ArrayList listEmpregado = new Empregado().Find(st.ToString());

                //if(listEmpregado.Count > 0)
                //{



                string xUsuario = Session["usuarioLogado"].ToString();

                //InicializaWebPageObjects();			
                Ilitera.Opsa.Data.Empregado empregado = new Ilitera.Opsa.Data.Empregado();

			Clinico exame = new Clinico(Convert.ToInt32(Request["IdExame"]));

                if (exame.IdPcmso.Id.Equals(0) || exame.IdEmpregadoFuncao.Id.Equals(0) || exame.IdEmpregadoFuncao.nID_FUNCAO == null )
				{

                    empregado.Find(" tblEMPREGADO.nID_EMPREGADO = " + Request["IdEmpregado"].ToString());

					exame.IdEmpregadoFuncao = EmpregadoFuncao.GetEmpregadoFuncao(empregado);
                    exame.IdPcmso = exame.IdEmpregadoFuncao.nID_EMPR.GetUltimoPcmso();
					
					exame.UsuarioId = System.Convert.ToInt32( Request["IdUsuario"].ToString() );
					exame.Save();
				}

                //if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("GLOBAL") > 0)
                //{
                //    RptAso_Global report2 = new DataSourceExameAsoPci(exame).GetReport_Global();                    
                //}
                //else
                //{
                //    RptAso report2 = new DataSourceExameAsoPci(exame).GetReport();                    
                //}

                //Funcionalidade funcionalidade = Funcionalidade.GetFuncionalidade(FuncionalidadeTipo.Relatorio, report.GetType());
                //ImpressaoDocUsuario.InsereRegistro(usuario, cliente, Convert.ToInt64(report.SummaryInfo.KeywordsInReport), true, funcionalidade);

				//CreatePDFDocument(report, this.Response);


            //Ilitera.Opsa.Data.Empregado empregado = new Ilitera.Opsa.Data.Empregado();
		
                Clinico exame2 = new Clinico(Convert.ToInt32(Request["IdExame"]));

				if (exame2.IdPcmso.Id.Equals(0) || exame2.IdEmpregadoFuncao.Id.Equals(0))
				{
                    empregado.Find(" tblEMPREGADO.nID_EMPREGADO = " + Request["IdEmpregado"].ToString());

					exame2.IdEmpregadoFuncao = EmpregadoFuncao.GetEmpregadoFuncao(empregado);
                    exame2.IdPcmso = exame.IdEmpregadoFuncao.nID_EMPR.GetUltimoPcmso();

                    exame2.UsuarioId = System.Convert.ToInt32(  Request["IdUsuario"].ToString()); //usuario.Id;

                    exame2.Save();
				}
				
				RptPci_Novo report3 = new DataSourceExameAsoPci(exame2).GetReportPci();
                //RptPci report3 = new DataSourceExameAsoPci(exame2).GetReportPci();


                CrystalDecisions.CrystalReports.Engine.ReportClass[] reports = new ReportClass[3];




                DataSet dsExames = new ExameDicionario().GetIdNome("Nome", " IdExameDicionario IN (SELECT IdExameDicionario FROM ClinicaExameDicionario WHERE IdClinica IN (SELECT IdClinica FROM ClinicaCliente WHERE IdCliente=" + Request["IdEmpresa"] + " and IdClinica = " + exame.IdJuridica.Id + " ))");

                DataSet ds = new ClinicaExameDicionario().Get("IdClinica IN (SELECT IdClinica FROM ClinicaCliente WHERE IdCliente=" + Request["IdEmpresa"] + " " + " and IdClinica = " + exame.IdJuridica.Id + " ) " + " AND IDCLINICAEXAMEDICIONARIO IN " +
                 "( " +
                 "   SELECT IdClinicaExameDicionario " +
                 "   FROM ClinicaClienteExameDicionario  " +
                 "    WHERE IdClinicaCliente IN ( " +
                 "      SELECT IdClinicaCliente FROM ClinicaCliente " +
                 "      WHERE IdCliente=" + Request["IdEmpresa"] + " " + " and IdClinica = " + exame.IdJuridica.Id + " and IsAutorizado = 1 ) ) ");



            Pcmso pcmso = new Pcmso();
            pcmso = exame.IdPcmso;

            Ghe ghe = new Ghe();

            if (pcmso.IdLaudoTecnico != null)
            {
                List<Ghe> ghes = new Ghe().Find<Ghe>("nID_LAUD_TEC=" + pcmso.IdLaudoTecnico.Id);
                                

                if (ghes == null || ghes.Count == 0)
                    ghe = exame.IdEmpregadoFuncao.GetGheEmpregado(pcmso.IdLaudoTecnico);
                else
                {
                    int IdGhe = exame.IdEmpregadoFuncao.GetIdGheEmpregado(pcmso.IdLaudoTecnico);

                    ghe = ghes.Find(delegate(Ghe g) { return g.Id == IdGhe; });
                }
            }

                string sExamesOcupacionais = exame.GetPlanejamentoExamesAso_Guia(ghe, true);



                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    DataRow[] rows = dsExames.Tables[0].Select("Id=" + Convert.ToInt32(row["IdExameDicionario"]));

                    //valueListClinicaClienteExameDicionario.ValueListItems.Add(Convert.ToInt32(row[0]), Convert.ToString(rows[0]["Nome"]));
                    if (rows.Count() > 0)
                    {

                        // retirar admissional, periodico, retorno ao trab., complement. desta lista
                        if (rows[0]["Nome"].ToString().Trim().ToUpper() != "RETORNO AO TRABALHO" &&
                            rows[0]["Nome"].ToString().Trim().ToUpper() != "DEMISSIONAL" &&
                            rows[0]["Nome"].ToString().Trim().ToUpper() != "ADMISSIONAL" &&
                            rows[0]["Nome"].ToString().Trim().ToUpper() != "MUDANÇA DE FUNÇÃO" &&
                            rows[0]["Nome"].ToString().Trim().ToUpper() != "PERIÓDICO")
                        {

                            //  lst_Exames.Items.Add(Convert.ToString(rows[0]["Nome"]));

                            if (sExamesOcupacionais.ToUpper().IndexOf(Convert.ToString(rows[0]["Nome"]).ToUpper()) > 0)
                            {

                                //lst_Exames.Items.Add(Convert.ToString(rows[0]["Nome"]));
                                //lst_Exames.Items[lst_Exames.Items.Count - 1].Selected = true;
                            }

                        }
                    }
                }
             
                //Entities.Usuario user = (Entities.Usuario)Session["usuarioLogado"];


                //Ilitera.Data.PPRA_EPI xGuia = new Ilitera.Data.PPRA_EPI();

                //xGuia.Salvar_Dados_Guia_Encaminhamento(System.Convert.ToInt32(Request["IdEmpresa"].ToString()), System.Convert.ToInt32(Request["IdEmpregado"].ToString()), xTipo, xExames + "|" + xExames2 + "|" + xExames3 + "|" + xExames4, txt_Data.Text, txt_Hora.Text, cmb_Clinicas.Items[cmb_Clinicas.SelectedIndex].ToString(), user.IdUsuario); 

                Cliente zCliente = new Cliente();
                zCliente.Find(System.Convert.ToInt32( xId_Empresa) );

                exame.IdEmpregadoFuncao.Find();

                if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().IndexOf("Prajna") > 0)
                {
                    //RptGuia_Prajna report_Prajna = new DataSourceGuia_Prajna(cliente, System.Convert.ToInt32(xId_Empregado), System.Convert.ToInt32(xId_Empresa), System.Convert.ToInt32(xId_Clinica), xExames, xExames2, xExames3, xExames4, xData_Exame, xHora_Exame, xTipo, xBasico, xObs, xImpDt).GetReport();
                    RptGuia_Nova_Prajna report_Prajna = new DataSourceGuia_Prajna(cliente, System.Convert.ToInt32(xId_Empregado), System.Convert.ToInt32(xId_Empresa), System.Convert.ToInt32(xId_Clinica), xExames, xExames2, xExames3, xExames4, xData_Exame, xHora_Exame, xTipo, xBasico, xObs, xImpDt, exame.IdEmpregadoFuncao.Id).GetReport_Nova();
                    reports[0] = report_Prajna;
                }              
                else
                {
                    //RptGuia report_Guia = new DataSourceGuia(cliente, System.Convert.ToInt32(xId_Empregado), System.Convert.ToInt32(xId_Empresa), System.Convert.ToInt32(xId_Clinica), xExames, xExames2, xExames3, xExames4, xData_Exame, xHora_Exame, xTipo, xBasico, xObs, xImpDt).GetReport();
                    RptGuia_Nova report_Guia = new DataSourceGuia(cliente, System.Convert.ToInt32(xId_Empregado), System.Convert.ToInt32(xId_Empresa), System.Convert.ToInt32(xId_Clinica), xExames, xExames2, xExames3, xExames4, xData_Exame, xHora_Exame, xTipo, xBasico, xObs, xImpDt, exame.IdEmpregadoFuncao.Id).GetReport_Nova();
                    reports[0] = report_Guia;
                }

                //                reports[1] = report2;
                if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("GLOBAL") > 0)
                {
                    RptAso_Global report2 = new DataSourceExameAsoPci(exame).GetReport_Global();
                    reports[1] = report2;
                }
                else
                {
                    RptAso report2 = new DataSourceExameAsoPci(exame).GetReport();
                    reports[1] = report2;
                }



                reports[2] = report3;



                CreatePDFMerged(reports, this.Response, "", true);

                reports = null;
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();


            }





            catch (Exception ex)
			{
                MsgBox1.Show("Ilitera.Net", ex.Message, null,
                       new EO.Web.MsgBoxButton("OK"));

			}


		}



        protected static void CreatePDFMerged(CrystalDecisions.CrystalReports.Engine.ReportClass[] reports, HttpResponse response, string watermark, bool RenumerarPaginas)
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
