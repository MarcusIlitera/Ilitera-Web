using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.IO;

using System.Net.Mail;
using System.Net;





using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

using Ilitera.PCMSO.Report;
using Ilitera.Opsa.Data;
using Ilitera.Common;
using Ilitera.Sied.Report;
using Ilitera.ASO.Report;

//using MestraNET;

namespace Ilitera.Net
{
	/// <summary>
	/// 
	/// </summary>
	public partial class RelatorioGuiaASO : System.Web.UI.Page
	{

        protected Prestador prestador = new Prestador();
        protected Usuario usuario = new Usuario();
        protected Cliente cliente = new Cliente();
        protected Empregado empregado = new Empregado();
        protected ExameClinicoFacade exame = new ExameClinicoFacade();
        private string xEnvio_Email = "";
        

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
            string xApt;
            string xDtDemissao;
            string xTipoGuia;
            
            Int32 xTipoExame = 0;


            //InicializaWebPageObjects();
            //Page.Response.Cache.SetCacheability(HttpCacheability.NoCache);

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
                xImpDt = Request["ImpDt"];
                xObs = Session["Obs_Guia"].ToString().Trim();
                xEnvio_Email = Request["Mail"].ToString().Trim();
                xApt = Request["Apt"].ToString().Trim();
                xTipoGuia = Request["TipoGuia"].ToString().Trim();

                try
                {
                    xDtDemissao = Request["Dem"].ToString().Trim();
                }
                catch (Exception ex)
                {
                    xDtDemissao = "";
                    string zErro = ex.Message;
                }


                string xUsuario = Session["usuarioLogado"].ToString();


                //testar, para evitar criar novo exame se clicar back após ter criado aso/guia
                if (xEnvio_Email != "2")
                {
                    if (Session["Ultima_Guia"].ToString().Trim() == xId_Empregado + "-" + xId_Clinica + "-" + xData_Exame + "-" + xTipo)
                    {
                        ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                        return;
                    }
                    else
                        Session["Ultima_Guia"] = xId_Empregado + "-" + xId_Clinica + "-" + xData_Exame + "-" + xTipo;
                }




                Ilitera.Opsa.Data.Empregado empregado = new Ilitera.Opsa.Data.Empregado();


                //criando ASO


                if (xTipo.ToUpper().Trim() == "A")
                {
                    xTipoExame = 1;
                }
                else if (xTipo.ToUpper().Trim() == "D")
                {
                    xTipoExame = 2;
                }
                else if (xTipo.ToUpper().Trim() == "M")
                {
                    xTipoExame = 3;
                }
                else if (xTipo.ToUpper().Trim() == "P")
                {
                    xTipoExame = 4;
                }
                else if (xTipo.ToUpper().Trim() == "R")
                {
                    xTipoExame = 5;
                }
                else
                {
                    xTipoExame = 4;
                }


                Cliente zCliente = new Cliente();
                zCliente.Find(System.Convert.ToInt32(xId_Empresa));


                if (xEnvio_Email == "2")  // 2.via de ASO
                {

                    ExameBase rexame = new ExameBase();
                    rexame.Find(" IDEMPREGADO = " + xId_Empregado + " and IdExameDicionario = " + xTipoExame.ToString() + " and convert( char(10),DataExame,103 ) = '" + xData_Exame + "' ");

                    if (rexame.Id == 0)
                    {
                        MsgBox1.Show("Ilitera.Net", "Problema na obtenção de dados do ASO", null, new EO.Web.MsgBoxButton("OK"));
                        return;
                    }
                    else
                    {

                        exame.Find(rexame.Id);
                    }




                }
                else
                {

                    exame.Prontuario = "";
                    //exame.Observacao = "";

                    empregado.Find(" tblEMPREGADO.nID_EMPREGADO = " + Request["IdEmpregado"].ToString());

                    exame.IdEmpregado = empregado;

                    //exame.IdEmpregadoFuncao = EmpregadoFuncao.GetEmpregadoFuncao(empregado);
                    exame.IdEmpregadoFuncao = EmpregadoFuncao.GetEmpregadoFuncao(empregado, System.Convert.ToInt32(xId_Empresa));
                    exame.IdPcmso = exame.IdEmpregadoFuncao.nID_EMPR.GetUltimoPcmso();

                    exame.apt_Espaco_Confinado = "";
                    exame.apt_Trabalho_Altura = "";
                    exame.apt_Transporte = "";
                    exame.apt_Submerso = "";
                    exame.apt_Eletricidade = "";
                    exame.apt_Alimento = "";
                    exame.apt_Brigadista = "";
                    exame.apt_Socorrista = "";
                    exame.apt_Respirador = "";

                    if (xApt.IndexOf("A") >= 0) exame.apt_Trabalho_Altura = "1";
                    if (xApt.IndexOf("C") >= 0) exame.apt_Espaco_Confinado = "1";
                    if (xApt.IndexOf("T") >= 0) exame.apt_Transporte = "1";
                    if (xApt.IndexOf("S") >= 0) exame.apt_Submerso = "1";
                    if (xApt.IndexOf("E") >= 0) exame.apt_Eletricidade = "1";
                    if (xApt.IndexOf("Q") >= 0) exame.apt_Aquaviario = "1";
                    if (xApt.IndexOf("M") >= 0) exame.apt_Alimento = "1";
                    if (xApt.IndexOf("B") >= 0) exame.apt_Brigadista = "1";
                    if (xApt.IndexOf("R") >= 0) exame.apt_Socorrista = "1";
                    if (xApt.IndexOf("P") >= 0) exame.apt_Respirador = "1";




                    ExameDicionario xExameDicionario = new ExameDicionario();

                    xExameDicionario.Find(xTipoExame);

                    Juridica xJuridica = new Juridica();
                    xJuridica.Find(Convert.ToInt32(Request["IdClinica"].ToString()));

                    exame.IdExameDicionario = xExameDicionario;
                    exame.IdJuridica = xJuridica;

                    //exame.IdExameDicionario.Find( xTipoExame );

                    //exame.IdJuridica.Find(Convert.ToInt32(Request["IdClinica"].ToString()));

                    System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");
                    exame.DataExame = System.Convert.ToDateTime(xData_Exame, ptBr);

                    if (xTipoExame == 2 && xDtDemissao.Trim() != "") exame.DataDemissao = System.Convert.ToDateTime(xDtDemissao, ptBr);

                    exame.IndResultado = 3;


                    Medico xMedico = new Medico();
                    xMedico.Find(1111);

                    exame.IdMedico = xMedico;

                    //Usuario xusuario = new Usuario();
                    //xusuario.Find(System.Convert.ToInt32(Request["IdUsuario"].ToString()));

                    //exame.UsuarioId = 


                    Entities.Usuario usuario = (Entities.Usuario)Session["usuarioLogado"];


                    //if (Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper().IndexOf("VIA") > 0 || Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper().IndexOf("_SHO") > 0 || (Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper().Trim() == "SIED_NOVO" && Ilitera.Data.SQLServer.EntitySQLServer._LocalServer.Trim() == "54.94.157.244") ||
                    //    Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper().IndexOf("_UNO") > 0 || Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper().IndexOf("_DAITI") > 0 || Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper().IndexOf("_GLOBAL") > 0 || Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper().IndexOf("_SDTOURIN") > 0 || Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper().IndexOf("_FOX") > 0 ||
                    //    (Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper().IndexOf("_SAFETY") > 0 && Ilitera.Data.SQLServer.EntitySQLServer._LocalServer.Trim() == "54.94.157.244"))
                    //{
                    //    if (exame.Id == 0)
                    //    {
                    //        exame.CodBusca = Gerar_CodBusca();
                    //    }
                    //}
                    //else
                    //{
                    //    if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("PRAJNA") > 0)
                    //    {
                            if (exame.Id == 0)
                            {
                                exame.CodBusca = Gerar_CodBusca();
                            }
                    //    }
                    //}

                    exame.Tirar_eSocial = false;

                    //colocar em espera para todas áreas, exceto Global, essence, Daiti e Prime
                    if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper().IndexOf("_PRIME") < 0 && Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper().IndexOf("_DAITI") < 0 && Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper().IndexOf("_GLOBAL") < 0 || Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper().IndexOf("_PRAJNA") < 0)
                    {
                        if (exame.Id == 0)
                        {
                            exame.IndResultado = (int)ResultadoExame.EmEspera;
                        }
                    }


                    int zStatus = 0;
                    try
                    {
                        zStatus = exame.Save(usuario.IdUsuario);
                    }
                    catch (Exception ex)
                    {
                        if (ex.Message.ToUpper() != "MÉTODO NÃO-ESTÁTICO REQUER UM DESTINO." && ex.Message.ToUpper() != "NON-STATIC METHOD REQUIRES A TARGET.")
                            throw new Exception(ex.Message.ToString());


                    }

                    //exame.Save();

                    //if ( cliente.Gerar_Complementares_Guia==true)  // criar exames complementares do ASO, se estes não existirem ainda.  Colocar como em espera
                    //{






                    //}
                }



                Clinico exame2 = new Clinico();
                exame2.Find(exame.Id);


                exame2.apt_Trabalho_Altura2 = "";
                exame2.apt_Espaco_Confinado2 = "";
                exame2.apt_Transporte2 = "";
                exame2.apt_Submerso2 = "";
                exame2.apt_Eletricidade2 = "";
                exame2.apt_Aquaviario2 = "";
                exame2.apt_Alimento2 = "";
                exame2.apt_Brigadista2 = "";
                exame2.apt_Socorrista2 = "";
                exame2.apt_Respirador2 = "";


                if (xApt.IndexOf("A") >= 0) exame2.apt_Trabalho_Altura2 = "1";
                if (xApt.IndexOf("C") >= 0) exame2.apt_Espaco_Confinado2 = "1";
                if (xApt.IndexOf("T") >= 0) exame2.apt_Transporte2 = "1";
                if (xApt.IndexOf("S") >= 0) exame2.apt_Submerso2 = "1";
                if (xApt.IndexOf("E") >= 0) exame2.apt_Eletricidade2 = "1";
                if (xApt.IndexOf("Q") >= 0) exame2.apt_Aquaviario2 = "1";
                if (xApt.IndexOf("M") >= 0) exame2.apt_Alimento2 = "1";
                if (xApt.IndexOf("B") >= 0) exame2.apt_Brigadista2 = "1";
                if (xApt.IndexOf("R") >= 0) exame2.apt_Socorrista2 = "1";
                if (xApt.IndexOf("P") >= 0) exame2.apt_Respirador2 = "1";


                exame2.IdEmpregado.Find();
                exame2.IdEmpregado.nID_EMPR.Find();
                exame2.IdEmpregado.nID_EMPR.IdGrupoEmpresa.Find();


                if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("PRAJNA") > 0)
                {

                    CrystalDecisions.CrystalReports.Engine.ReportClass[] reports = new ReportClass[7];
                    Carregar_Dados_Anamnese_Exame(exame2.Id);

                    exame2.IdEmpregado.Find();
                    exame2.IdEmpregado.nID_EMPR.Find();



                    RptGuia_Nova_Prajna report0 = new DataSourceGuia_Prajna(cliente, System.Convert.ToInt32(xId_Empregado), System.Convert.ToInt32(xId_Empresa), System.Convert.ToInt32(xId_Clinica), xExames, xExames2, xExames3, xExames4, xData_Exame, xHora_Exame, xTipo, xBasico, xObs, xImpDt, exame2.IdEmpregadoFuncao.Id).GetReport_Nova();

                    reports[0] = report0;

                    reports[1] = new DataSourceExameAsoPci_Novo(exame2).GetReport_Unico("Via Empresa");
                    reports[2] = new DataSourceExameAsoPci_Novo(exame2).GetReport_Unico("Via Empregado");
                    reports[3] = new DataSourceExameAsoPci_Novo(exame2).GetReport_Unico("Via Clínica");
                    reports[4] = new DataSourceExameAsoPci_Novo(exame2).GetReport_Unico("Via Ilitera");

                    //PCI
                    reports[5] = new DataSourceExameAsoPci_Novo(exame2).GetReportPciUnico_Essence(false);

                    //anamnese
                    reports[6] = new DataSourceExameAsoPci_Novo(exame2).GetReport_Anamnese(true);


                    CreatePDFMerged(reports, this.Response, "", false);

                    reports = null;
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    GC.Collect();

                }
                //if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("VIA") > 0  || Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper().IndexOf("_SHO") > 0 || (Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper().Trim() == "SIED_NOVO" && Ilitera.Data.SQLServer.EntitySQLServer._LocalServer.Trim() == "54.94.157.244") ||
                //    Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper().IndexOf("_UNO") > 0 || Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper().IndexOf("_DAITI") > 0 || Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper().IndexOf("_GLOBAL") > 0 || Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper().IndexOf("_SDTOURIN") > 0 || Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper().IndexOf("_FOX") > 0
                //    || (Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper().IndexOf("_SAFETY") > 0 && Ilitera.Data.SQLServer.EntitySQLServer._LocalServer.Trim() == "54.94.157.244"))
                else   
                {
                    Int16 zQuestionario = 0;

                    exame2.IdEmpregado.Find();
                    exame2.IdEmpregado.nID_EMPR.Find();

                    if (exame2.IdEmpregado.nID_EMPR.NomeAbreviado.ToUpper().IndexOf("DEERE") >= 0)
                    {

                        exame2.IdEmpregado.Find();

                        Empregado_Aptidao rAptidao = new Empregado_Aptidao();
                        rAptidao.Find(" nId_Empregado = " + exame2.IdEmpregado.Id.ToString());


                        exame2.IdPcmso.Find();
                        exame2.IdPcmso.IdLaudoTecnico.Find();
                        Int32 zIdGHE = exame2.IdEmpregadoFuncao.GetIdGheEmpregado(exame2.IdPcmso.IdLaudoTecnico);
                        GHE_Aptidao zAptidao = new GHE_Aptidao();
                        zAptidao.Find("nId_Func = " + zIdGHE.ToString());


                        if (rAptidao.Id != 0)
                        {
                            if (rAptidao.apt_Respirador == true)
                            {
                                zQuestionario = 1;
                            }
                        }

                        if (zAptidao.Id != 0)
                        {
                            if (zAptidao.apt_PPR == true)
                            {
                                zQuestionario = 1;
                            }
                        }



                    }


                    CrystalDecisions.CrystalReports.Engine.ReportClass[] reports = new ReportClass[6 + zQuestionario];

                    exame2.IdEmpregado.Find();
                    exame2.IdEmpregado.nID_EMPR.Find();

                    if (Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper().IndexOf("_GLOBAL") > 0)
                       reports[0] = new DataSourceGuia2(exame2.IdEmpregado.nID_EMPR, System.Convert.ToInt32(xId_Empregado), System.Convert.ToInt32(xId_Empresa), System.Convert.ToInt32(xId_Clinica), xExames, xExames2, xExames3, xExames4, xData_Exame, xHora_Exame, xTipo, xBasico, xObs, xImpDt, exame2.IdEmpregadoFuncao.Id).GetReport_Nova_Global();
                    else
                        reports[0] = new DataSourceGuia2(exame2.IdEmpregado.nID_EMPR, System.Convert.ToInt32(xId_Empregado), System.Convert.ToInt32(xId_Empresa), System.Convert.ToInt32(xId_Clinica), xExames, xExames2, xExames3, xExames4, xData_Exame, xHora_Exame, xTipo, xBasico, xObs, xImpDt, exame2.IdEmpregadoFuncao.Id).GetReport_Nova();

                    reports[1] = new DataSourceExameAsoPci_Novo(exame2).GetReport_Unico("Via Empresa");
                    reports[2] = new DataSourceExameAsoPci_Novo(exame2).GetReport_Unico("Via Empregado");
                    reports[3] = new DataSourceExameAsoPci_Novo(exame2).GetReport_Unico("Via Clínica");
                    reports[4] = new DataSourceExameAsoPci_Novo(exame2).GetReport_Unico("Via Ilitera");

                    //PCI
                    reports[5] = new DataSourceExameAsoPci_Novo(exame2).GetReportPciUnico(false);


                    if (zQuestionario == 1)
                    {
                        reports[6] = new DataSourceExameAsoPci_Novo(exame2).GetReport_Questionario();
                    }


                    CreatePDFMerged(reports, this.Response, "", false);

                    reports = null;
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    GC.Collect();



                }

                //else
                //{



                //    Int16 zTamanho = 3;

                //    exame2.IdEmpregado.Find();
                //    exame2.IdEmpregado.nID_EMPR.Find();
                //    exame2.IdEmpregado.nID_EMPR.IdGrupoEmpresa.Find();

                //    if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("PRAJNA") > 0 || Ilitera.Data.SQLServer.EntitySQLServer._LocalServer.ToString().ToUpper().IndexOf("EY") > 0)
                //    {
                //        zTamanho = 4;
                //    }

                //    if (xTipoGuia == "2")  // Guia + Ficha Clinica 
                //    {
                //        zTamanho = (Int16)(zTamanho - 1);
                //    }


                //    CrystalDecisions.CrystalReports.Engine.ReportClass[] reports = new ReportClass[zTamanho];

                //    //Cliente zCliente = new Cliente();
                //    //zCliente.Find(System.Convert.ToInt32(xId_Empresa));


                //    Int32 zRelat = 0;

                //    exame2.IdEmpregadoFuncao.Find();

                //    if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().IndexOf("Prajna") > 0)
                //    {
                //        //RptGuia_Prajna report0 = new DataSourceGuia_Prajna(cliente, System.Convert.ToInt32(xId_Empregado), System.Convert.ToInt32(xId_Empresa), System.Convert.ToInt32(xId_Clinica), xExames, xExames2, xExames3, xExames4, xData_Exame, xHora_Exame, xTipo, xBasico, xObs, xImpDt).GetReport();
                //        RptGuia_Nova_Prajna report0 = new DataSourceGuia_Prajna(cliente, System.Convert.ToInt32(xId_Empregado), System.Convert.ToInt32(xId_Empresa), System.Convert.ToInt32(xId_Clinica), xExames, xExames2, xExames3, xExames4, xData_Exame, xHora_Exame, xTipo, xBasico, xObs, xImpDt, exame2.IdEmpregadoFuncao.Id).GetReport_Nova();
                //        //CreatePDFDocument(report, this.Response);
                //        reports[zRelat] = report0;
                //        zRelat = zRelat + 1;
                //    }
                //    else
                //    {
                //        RptGuia_Nova report0 = new DataSourceGuia(cliente, System.Convert.ToInt32(xId_Empregado), System.Convert.ToInt32(xId_Empresa), System.Convert.ToInt32(xId_Clinica), xExames, xExames2, xExames3, xExames4, xData_Exame, xHora_Exame, xTipo, xBasico, xObs, xImpDt, exame2.IdEmpregadoFuncao.Id).GetReport_Nova();
                //        //RptGuia report0 = new DataSourceGuia(cliente, System.Convert.ToInt32(xId_Empregado), System.Convert.ToInt32(xId_Empresa), System.Convert.ToInt32(xId_Clinica), xExames, xExames2, xExames3, xExames4, xData_Exame, xHora_Exame, xTipo, xBasico, xObs, xImpDt).GetReport();

                //        //CreatePDFDocument(report, this.Response);
                //        reports[zRelat] = report0;
                //        zRelat = zRelat + 1;
                //    }








                //    if (xTipoGuia != "2")
                //    {

                //        if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("GLOBAL") > 0)
                //        {
                //            RptAso_Global report2 = new DataSourceExameAsoPci(exame2).GetReport_Global();
                //            reports[zRelat] = report2;
                //            zRelat = zRelat + 1;
                //        }
                //        else
                //        {
                //            RptAso report2 = new DataSourceExameAsoPci(exame2).GetReport();
                //            reports[zRelat] = report2;
                //            zRelat = zRelat + 1;
                //        }
                //    }


                //    //tenho o ID do ASO para colocar no registro da guia gerada ?
                //    //dar um update ?




                //    //Funcionalidade funcionalidade = Funcionalidade.GetFuncionalidade(FuncionalidadeTipo.Relatorio, report.GetType());
                //    //ImpressaoDocUsuario.InsereRegistro(usuario, cliente, Convert.ToInt64(report.SummaryInfo.KeywordsInReport), true, funcionalidade);

                //    //CreatePDFDocument(report, this.Response);


                //    //Ilitera.Opsa.Data.Empregado empregado = new Ilitera.Opsa.Data.Empregado();

                //    //Clinico exame2 = new Clinico(Convert.ToInt32(Request["IdExame"]));



                //    Juridica xClin = new Juridica();
                //    xClin.Find(Convert.ToInt32(Request["IdClinica"].ToString()));

                //    string xClinNome = "";

                //    if (xClin != null) xClinNome = xClin.NomeAbreviado.ToUpper().Trim();


                //    if (Ilitera.Data.SQLServer.EntitySQLServer._LocalServer.ToString().ToUpper().IndexOf("EY") > 0)
                //    {
                //        //RptPci_NovoEY report3 = new DataSourceExameAsoPci(exame2).GetReportPci_EY();
                //        //reports[2] = report3;
                //        RptPci_Novo_EY2 report3 = new DataSourceExameAsoPci(exame2).GetReportPciEY2();
                //        reports[zRelat] = report3;
                //        zRelat = zRelat + 1;
                //    }
                //    //else if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().IndexOf("Daiti") > 0 || Ilitera.Data.SQLServer.EntitySQLServer.xDB2.Trim() == "Sied_Novo" || xClinNome.IndexOf("DAITI") >= 0 || xClinNome.IndexOf("IPATINGA") >= 0)
                //    //{
                //    //    RptPci report3 = new DataSourceExameAsoPci(exame2).GetReportPci_Antigo();
                //    //    reports[zRelat] = report3;
                //    //    zRelat = zRelat + 1;
                //    //}
                //    else
                //    {

                //        exame2.IdEmpregado.Find();
                //        exame2.IdEmpregado.nID_EMPR.Find();
                //        exame2.IdEmpregado.nID_EMPR.IdGrupoEmpresa.Find();

                //        //if (exame.IdEmpregado.nID_EMPR.IdGrupoEmpresa.Descricao.ToUpper().Trim() == "CAPGEMINI" && Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("_HOM") > 0)  //.Id == -905238295)  // Capgemini

                //        if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("PRAJNA") > 0) //  && exame2.IdEmpregado.nID_EMPR.IdGrupoEmpresa.Descricao.ToUpper().Trim() == "CAPGEMINI")  // && Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("_HOM") > 0)  //.Id == -905238295)  // Capgemini
                //        {
                //            RptPci_Novo_Capgemini report3 = new DataSourceExameAsoPci(exame2).GetReportPciCapgemini();
                //            reports[zRelat] = report3;
                //            zRelat = zRelat + 1;
                //        }
                //        else if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("GLOBAL") > 0)
                //        {
                //            RptPci_Novo_Global report3 = new DataSourceExameAsoPci(exame2).GetReportPci_Global();
                //            reports[zRelat] = report3;
                //            zRelat = zRelat + 1;
                //        }
                //        else
                //        {
                //            RptPci_Novo report3 = new DataSourceExameAsoPci(exame2).GetReportPci();
                //            reports[zRelat] = report3;
                //            zRelat = zRelat + 1;
                //        }
                //    }
                //    //RptPci report3 = new DataSourceExameAsoPci(exame2).GetReportPci();


                //    exame2.IdEmpregado.Find();
                //    exame2.IdEmpregado.nID_EMPR.Find();
                //    exame2.IdEmpregado.nID_EMPR.IdGrupoEmpresa.Find();


                //    if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("PRAJNA") > 0 || Ilitera.Data.SQLServer.EntitySQLServer._LocalServer.ToString().ToUpper().IndexOf("EY") > 0) //exame2.IdEmpregado.nID_EMPR.IdGrupoEmpresa.Descricao.ToUpper().Trim() == "CAPGEMINI"  ) // && Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("_HOM") > 0)  //.Id == -905238295)  // Capgemini
                //    {
                //        Int32 xIdExame = exame2.Id;

                //        Carregar_Dados_Anamnese_Exame(exame2.Id);

                //        RptAnamnese report4 = new DataSourceExameAnamnese(xIdExame, true).GetReport();
                //        reports[zRelat] = report4;

                //    }




                //    CreatePDFMerged(reports, this.Response, "", false);



                //    reports = null;
                //    GC.Collect();
                //    GC.WaitForPendingFinalizers();
                //    GC.Collect();

                //}





                //Funcionalidade funcionalidade = Funcionalidade.GetFuncionalidade(FuncionalidadeTipo.Relatorio, report.GetType());
                //ImpressaoDocUsuario.InsereRegistro(usuario, cliente, Convert.ToInt64(report.SummaryInfo.KeywordsInReport), true, funcionalidade);


                //}		
                //else
                //    throw new Exception("Problema na geração da guia !");



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
                    xAnam.Carregar_Anamnese_Dinamica(vExame.IdEmpregado.nID_EMPR.Id, zExame);


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



        protected void Envio_Email_Prajna( string xPara, string xCopia, string xSubject, string xBody, string xAttach, string xModulo, Int32 xIdEmpregado, Int32 xIdEmpresa)
        {

            string xDestinatario = "";

         
            MailMessage objEmail = new MailMessage();

            //rementente do email           
            //objEmail.From = new MailAddress("agendamento@5aessence.com.br");

            //objEmail.From = new MailAddress("agendamento@essencenet.com.br");

            //teste
            //objEmail.From = new MailAddress("agendamento.sp.sto@ilitera.com.br");
            objEmail.From = new MailAddress("admin@sharepoint-imexis.com.br");




            //para
            string xEmail = "";

            xEmail = exame.IdJuridica.Email.ToString().Trim();

            if (xEmail == "")
            {
                throw new Exception("Clínica não possui e-mail cadastrado.");                
            }

            if (xEmail.IndexOf(";") > 0) xEmail = xEmail.Substring(0, xEmail.IndexOf(";"));

            objEmail.To.Add("wagner@ilitera.com.br");
            //objEmail.To.Add(xEmail);
            //xDestinatario = xEmail + "; agendamento@5aessence.com.br ;";

            //objEmail.CC.Add("agendamento@5aessence.com.br");


            //cópia para usuário logado
            //Entities.Usuario usuario = (Entities.Usuario)Session["usuarioLogado"];

            //Prestador xPrestador = new Prestador(usuario.IdPrestador);

            
            //if (xPrestador.Email != null)
            //{
            //    xEmail = xPrestador.Email.ToString().Trim();
            //    if (xEmail.IndexOf(";") > 0) xEmail = xEmail.Substring(0, xEmail.IndexOf(";"));

            //    if (xEmail.Trim() != "")
            //    {
            //        objEmail.Bcc.Add(xEmail);
            //        xDestinatario = xDestinatario + xEmail + ";";
            //    }
            //}

            objEmail.Priority = MailPriority.Normal;
            objEmail.IsBodyHtml = true;

            objEmail.Subject = xSubject;
            objEmail.Body = xBody;
            objEmail.SubjectEncoding = Encoding.GetEncoding("ISO-8859-1");
            objEmail.BodyEncoding = Encoding.GetEncoding("ISO-8859-1");

            Attachment xItem = new Attachment(xAttach);
            objEmail.Attachments.Add(xItem);

            SmtpClient objSmtp = new SmtpClient();

            //objSmtp.Host = "mail.exchange.locaweb.com.br";
            //objSmtp.Port = 587;

            objSmtp.Host = "smtp.office365.com";
            objSmtp.Port = 587;
            //objSmtp.Credentials = new NetworkCredential("agendamento@essencenet.com.br", "Vug31145");

            //Teste Office 365
            //objSmtp.Credentials = new System.Net.NetworkCredential("agendamento.sp.sto@ilitera.com.br", "Ilitera_1010");
         objSmtp.Credentials = new System.Net.NetworkCredential("admin@sharepoint-imexis.com.br", "Imex1s@ti24");



            //objSmtp.EnableSsl = false;

            //teste
            objSmtp.EnableSsl = true;

            //objSmtp.Host = "outlook.office.com";


            //objSmtp.Host = "smtp.office365.com";
            //objSmtp.Port = 587;                       
            //objSmtp.Credentials = new NetworkCredential("agendamento@5aessence.com.br", "Agend_5060");

            //objSmtp.Host = "smtp.5aessencenet.com.br";
            //objSmtp.Port = 587;
            //objSmtp.Credentials = new NetworkCredential("agendamento@5aessencenet.com.br", "Prana@2022!@");

            //System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;

            //objSmtp.Send(objEmail);
            
            //Ilitera.Data.Clientes_Clinicas xEnvio = new Ilitera.Data.Clientes_Clinicas();
            //xEnvio.Envio_eMail(xIdEmpregado, xIdEmpresa, xDestinatario, "Guia / ASO / PCI");

            return;

        }



        protected void Envio_Email_Ilitera_Alerta(string xPara, string xCopia, string xSubject, string xBody, string xAttach, string xModulo, Int32 xIdEmpregado, Int32 xIdEmpresa)
        {
            string xDestinatario = "";
            //enviar e-mail para clinica e prajna

            MailMessage objEmail = new MailMessage();

            //rementente do email
            //objEmail.From = new MailAddress("agendamento.sp.sto@ilitera.com.br");


            string[] stringSeparators4 = new string[] { ";" };
            string[] result4;
            result4 = xPara.Trim().Split(stringSeparators4, StringSplitOptions.RemoveEmptyEntries);

            foreach (string s in result4)
            {
                if (s.Trim() != "")
                {
                    objEmail.To.Add(s);
                }
            }



            string[] stringSeparators5 = new string[] { ";" };
            string[] result5;
            result5 = xCopia.Trim().Split(stringSeparators5, StringSplitOptions.RemoveEmptyEntries);

            foreach (string s in result5)
            {
                if (s.Trim() != "")
                {
                    objEmail.CC.Add(s);
                }
            }





            objEmail.Priority = MailPriority.Normal;
            objEmail.IsBodyHtml = true;

            objEmail.Subject = xSubject;
            objEmail.Body = xBody;
            objEmail.SubjectEncoding = Encoding.GetEncoding("ISO-8859-1");
            objEmail.BodyEncoding = Encoding.GetEncoding("ISO-8859-1");

            if (xAttach != "")
            {
                Attachment xItem = new Attachment(xAttach);
                objEmail.Attachments.Add(xItem);
            }

            SmtpClient objSmtp = new SmtpClient();
            //objSmtp.Host = "smtp.ilitera.com.br";
            objSmtp.Host = "smtp.office365.com";
            objSmtp.EnableSsl = true;
            objSmtp.Port = 587;

            if (System.DateTime.Now.Second % 3 == 0)
            {
                objEmail.From = new MailAddress("agendamento1@ilitera.com.br");
                objSmtp.Credentials = new NetworkCredential("agendamento1@ilitera.com.br", "Ilitera_3624");                
            }
            else
            {
                objEmail.From = new MailAddress("agendamento3@ilitera.com.br");
                objSmtp.Credentials = new NetworkCredential("agendamento3@ilitera.com.br", "Ilitera_3625");
            }

            objEmail.ReplyTo = new MailAddress("agendamento@ilitera.com.br");


            Ilitera.Data.Clientes_Clinicas xEnvio = new Ilitera.Data.Clientes_Clinicas();
            //checar se não é e-mail duplicado

            objSmtp.Send(objEmail);
            xEnvio.Envio_eMail(xIdEmpregado, xIdEmpresa, xDestinatario, "Alerta Agendamento exame  web");



            return;

        }


        protected void Envio_Email_Global_Alerta(string xPara, string xCopia, string xSubject, string xBody, string xAttach, string xModulo, Int32 xIdEmpregado, Int32 xIdEmpresa)
        {
            string xDestinatario = "";
            //enviar e-mail para clinica e prajna

            MailMessage objEmail = new MailMessage();

            //rementente do email
            //objEmail.From = new MailAddress("agendamento.sp.sto@ilitera.com.br");


            string[] stringSeparators4 = new string[] { ";" };
            string[] result4;
            result4 = xPara.Trim().Split(stringSeparators4, StringSplitOptions.RemoveEmptyEntries);

            foreach (string s in result4)
            {
                if (s.Trim() != "")
                {
                    objEmail.To.Add(s);
                }
            }



            string[] stringSeparators5 = new string[] { ";" };
            string[] result5;
            result5 = xCopia.Trim().Split(stringSeparators5, StringSplitOptions.RemoveEmptyEntries);

            foreach (string s in result5)
            {
                if (s.Trim() != "")
                {
                    objEmail.CC.Add(s);
                }
            }





            objEmail.Priority = MailPriority.Normal;
            objEmail.IsBodyHtml = true;

            objEmail.Subject = xSubject;
            objEmail.Body = xBody;
            objEmail.SubjectEncoding = Encoding.GetEncoding("ISO-8859-1");
            objEmail.BodyEncoding = Encoding.GetEncoding("ISO-8859-1");

            if (xAttach != "")
            {
                Attachment xItem = new Attachment(xAttach);
                objEmail.Attachments.Add(xItem);
            }

            SmtpClient objSmtp = new SmtpClient();
            objSmtp.Host = "smtp.globalsegmed.com.br";
            objSmtp.Port = 587;



            Juridica xJur = new Juridica();
            xJur.Find(xIdEmpresa);

            if (xJur.Id != 0)
            {
                if (xJur.Auxiliar == "RAMO" || xJur.Auxiliar == "GLOBAL2")
                {

                    objSmtp.Host = "smtp.ramoassessoria.com.br";
                    objEmail.From = new MailAddress("guias@ramoassessoria.com.br");
                    objSmtp.Credentials = new NetworkCredential("guias@ramoassessoria.com.br", "Ramo@2024");

                    objEmail.ReplyTo = new MailAddress("guias@ramoassessoria.com.br");

                    if (xPara.IndexOf("asos2@globalsegmed.com.br") < 0)
                    {
                        objEmail.CC.Add("asos2@globalsegmed.com.br");
                    }
                }
                //else if (xJur.Auxiliar == "GLOBAL2")
                //{
                //    objEmail.From = new MailAddress("guias2@globalsegmed.com.br");
                //    objSmtp.Credentials = new NetworkCredential("guias2@globalsegmed.com.br", "Guiasglobal2#");

                //    objEmail.ReplyTo = new MailAddress("guias2@globalsegmed.com.br");

                //    if (xPara.IndexOf("asos2@globalsegmed.com.br") < 0)
                //    {
                //        objEmail.CC.Add("asos2@globalsegmed.com.br");
                //    }
                //}
                else
                {
                    objEmail.From = new MailAddress("guias@globalsegmed.com.br");
                    objSmtp.Credentials = new NetworkCredential("guias@globalsegmed.com.br", "Sergio2024@");

                    objEmail.ReplyTo = new MailAddress("guias@globalsegmed.com.br");

                    if (xPara.IndexOf("asos@globalsegmed.com.br") < 0)
                    {
                        objEmail.CC.Add("asos@globalsegmed.com.br");
                    }
                    

                }
            }
            else
            {
                objEmail.From = new MailAddress("guias@globalsegmed.com.br");
                objSmtp.Credentials = new NetworkCredential("guias@globalsegmed.com.br", "Sergio2024@");

                objEmail.ReplyTo = new MailAddress("guias@globalsegmed.com.br");
                objEmail.CC.Add("asos@globalsegmed.com.br");
                
            }


           // objEmail.From = new MailAddress("guias2@globalsegmed.com.br");
            //objSmtp.Credentials = new NetworkCredential("guias2@globalsegmed.com.br", "Guiasglobal2#");
            
           // objEmail.ReplyTo = new MailAddress("guias2@globalsegmed.com.br");


            Ilitera.Data.Clientes_Clinicas xEnvio = new Ilitera.Data.Clientes_Clinicas();
            //checar se não é e-mail duplicado

            objSmtp.Send(objEmail);
            xEnvio.Envio_eMail(xIdEmpregado, xIdEmpresa, xDestinatario, "Alerta Agendamento exame  web");



            return;

        }



        protected void Envio_Email_Prajna_Alerta(string xPara, string xCopia, string xSubject, string xBody, string xAttach, string xModulo, Int32 xIdEmpregado, Int32 xIdEmpresa)
        {

            string xDestinatario = "";


            MailMessage objEmail = new MailMessage();

            //rementente do email           
            //objEmail.From = new MailAddress("agendamento@5aessence.com.br");
            objEmail.From = new MailAddress("agendamento@essencenet.com.br");



            string[] stringSeparators4 = new string[] { ";" };
            string[] result4;
            result4 = xPara.Trim().Split(stringSeparators4, StringSplitOptions.RemoveEmptyEntries);

            foreach (string s in result4)
            {
                if (s.Trim() != "")
                {
                    objEmail.To.Add(s);
                }
            }



            string[] stringSeparators5 = new string[] { ";" };
            string[] result5;
            result5 = xCopia.Trim().Split(stringSeparators5, StringSplitOptions.RemoveEmptyEntries);

            foreach (string s in result5)
            {
                if (s.Trim() != "")
                {
                    objEmail.CC.Add(s);
                }
            }



            objEmail.Priority = MailPriority.Normal;
            objEmail.IsBodyHtml = true;

            objEmail.Subject = xSubject;
            objEmail.Body = xBody;
            objEmail.SubjectEncoding = Encoding.GetEncoding("ISO-8859-1");
            objEmail.BodyEncoding = Encoding.GetEncoding("ISO-8859-1");

            if (xAttach != "")
            {
                Attachment xItem = new Attachment(xAttach);
                objEmail.Attachments.Add(xItem);
            }

            SmtpClient objSmtp = new SmtpClient();
            objSmtp.Host = "mail.exchange.locaweb.com.br";
            objSmtp.Port = 587;
            objSmtp.Credentials = new NetworkCredential("agendamento@essencenet.com.br", "Vug31145");

            objSmtp.EnableSsl = false;

            //objSmtp.Host = "outlook.office.com";


            //objSmtp.Host = "smtp.office365.com";
            //objSmtp.Port = 587;                       
            //objSmtp.Credentials = new NetworkCredential("agendamento@5aessence.com.br", "Agend_5060");

            //objSmtp.Host = "smtp.5aessencenet.com.br";
            //objSmtp.Port = 587;
            //objSmtp.Credentials = new NetworkCredential("agendamento@5aessencenet.com.br", "Prana@2022!@");


            //objSmtp.Send(objEmail);
            //Ilitera.Data.Clientes_Clinicas xEnvio = new Ilitera.Data.Clientes_Clinicas();
            //xEnvio.Envio_eMail(xIdEmpregado, xIdEmpresa, xDestinatario, "Alerta Agendamento exame  web");

            return;

        }




        protected void Envio_Email_Ilitera(string xPara, string xCopia, string xSubject, string xBody, string xAttach, string xModulo, Int32 xIdEmpregado, Int32 xIdEmpresa)
        {
            string xDestinatario = "";
            //enviar e-mail para clinica e prajna

            MailMessage objEmail = new MailMessage();

            //rementente do email
            //objEmail.From = new MailAddress("agendamento.sp.sto@ilitera.com.br");
            

            //caixa-postal de onde será enviado o e-mail
            //objEmail.ReplyTo = new MailAddress("email@seusite.com.br");

            //para
            //objEmail.To.Add("lasdowsky@gmail.com");
            string xEmail = "";

            xEmail = exame.IdJuridica.Email.ToString().Trim();

            if (xEmail == "")
            {
                throw new Exception("Clínica não possui e-mail cadastrado.");
            }

            if (xEmail.IndexOf(";") > 0) xEmail = xEmail.Substring(0, xEmail.IndexOf(";"));

            objEmail.To.Add(xEmail);
            xDestinatario = xEmail + "; atendimento@ilitera.com.br ;";

            //objEmail.Bcc.Add("oculto@provedor.com.br");

            objEmail.CC.Add("atendimento@ilitera.com.br");

            //if (Ilitera.Data.SQLServer.EntitySQLServer._LocalServer.ToUpper().IndexOf("EY") > 0)
            //    objEmail.CC.Add("alberto.pereira@br.ey.com");
            //else
            //    objEmail.CC.Add("lucas.sp.sto@ilitera.com.br");

            objEmail.Priority = MailPriority.Normal;
            objEmail.IsBodyHtml = true;

            objEmail.Subject = xSubject;
            objEmail.Body = xBody;
            objEmail.SubjectEncoding = Encoding.GetEncoding("ISO-8859-1");
            objEmail.BodyEncoding = Encoding.GetEncoding("ISO-8859-1");

            Attachment xItem = new Attachment(xAttach);
            objEmail.Attachments.Add(xItem);

            SmtpClient objSmtp = new SmtpClient();
            //objSmtp.Host = "smtp.ilitera.com.br";            
            objSmtp.Host = "smtp.office365.com";
            objSmtp.EnableSsl = true;
            objSmtp.Port = 587;

            if (System.DateTime.Now.Second % 3 == 0)
            {
                objEmail.From = new MailAddress("agendamento1@ilitera.com.br");
                objSmtp.Credentials = new NetworkCredential("agendamento1@ilitera.com.br", "Ilitera_3624");
            }
            else
            {
                objEmail.From = new MailAddress("agendamento3@ilitera.com.br");
                objSmtp.Credentials = new NetworkCredential("agendamento3@ilitera.com.br", "Ilitera_3625");
            }

            objEmail.ReplyTo = new MailAddress("agendamento@ilitera.com.br");

            Ilitera.Data.Clientes_Clinicas xEnvio = new Ilitera.Data.Clientes_Clinicas();
            //checar se não é e-mail duplicado

            DataSet rDs = xEnvio.Checar_Envio_Email(xIdEmpregado, xIdEmpresa, xDestinatario, "Guia / ASO / PCI");

            if (rDs.Tables[0].Rows.Count == 0)
            {
                objSmtp.Send(objEmail);
                xEnvio.Envio_eMail(xIdEmpregado, xIdEmpresa, xDestinatario, "Guia / ASO / PCI");
            }
            
                                 
            return;

        }



        protected void Envio_Email_Global(string xPara, string xCopia, string xSubject, string xBody, string xAttach, string xModulo, Int32 xIdEmpregado, Int32 xIdEmpresa)
        {
            string xDestinatario = "";
            //enviar e-mail para clinica e prajna

            MailMessage objEmail = new MailMessage();

            //rementente do email
            //objEmail.From = new MailAddress("agendamento.sp.sto@ilitera.com.br");


            //caixa-postal de onde será enviado o e-mail
            //objEmail.ReplyTo = new MailAddress("email@seusite.com.br");

            //para
            //objEmail.To.Add("lasdowsky@gmail.com");
            string xEmail = "";

            xEmail = exame.IdJuridica.Email.ToString().Trim();

            if (xEmail == "")
            {
                throw new Exception("Clínica não possui e-mail cadastrado.");
            }

            if (xEmail.IndexOf(";") > 0) xEmail = xEmail.Substring(0, xEmail.IndexOf(";"));

            objEmail.To.Add(xEmail);
            xDestinatario = xEmail;// + "; atendimento@ilitera.com.br ; ";

            //objEmail.Bcc.Add("oculto@provedor.com.br");

            //objEmail.CC.Add("atendimento@ilitera.com.br");
            //objEmail.CC.Add("guias@globalsegmed.com.br");
            //objEmail.CC.Add("asos@globalsegmed.com.br");

            objEmail.Priority = MailPriority.Normal;
            objEmail.IsBodyHtml = true;

            objEmail.Subject = xSubject;
            objEmail.Body = xBody;
            objEmail.SubjectEncoding = Encoding.GetEncoding("ISO-8859-1");
            objEmail.BodyEncoding = Encoding.GetEncoding("ISO-8859-1");

            Attachment xItem = new Attachment(xAttach);
            objEmail.Attachments.Add(xItem);

            //if ( xIdEmpresa == 910917500)  //obra 547
            if ( xBody.ToUpper().IndexOf("OBRA 547") > 0)
            {
                Attachment xItem2 = new Attachment(@"I:\Prestadores\ROMBERG.pdf");
                objEmail.Attachments.Add(xItem2);
            }

            SmtpClient objSmtp = new SmtpClient();
            objSmtp.Host = "smtp.globalsegmed.com.br";
            objSmtp.Port = 587;


            Juridica xJur = new Juridica();
            xJur.Find(xIdEmpresa);

            if ( xJur.Id!=0)
            {
                if ( xJur.Auxiliar=="RAMO" || xJur.Auxiliar == "GLOBAL2")
                {
                    objSmtp.Host = "smtp.ramoassessoria.com.br";
                    objEmail.From = new MailAddress("guias@ramoassessoria.com.br");
                    objSmtp.Credentials = new NetworkCredential("guias@ramoassessoria.com.br", "Ramo@2024");

                    objEmail.ReplyTo = new MailAddress("guias@ramoassessoria.com.br");
                    //objEmail.CC.Add("asos2@globalsegmed.com.br");
                    objEmail.CC.Add("guias@ramoassessoria.com.br");


                }
                //else if (xJur.Auxiliar == "GLOBAL2")
                //{
                //    objEmail.From = new MailAddress("guias2@globalsegmed.com.br");
                //    objSmtp.Credentials = new NetworkCredential("guias2@globalsegmed.com.br", "Guiasglobal2#");

                //    objEmail.ReplyTo = new MailAddress("guias2@globalsegmed.com.br");
                //}
                else
                {
                    objEmail.From = new MailAddress("guias@globalsegmed.com.br");
                    objSmtp.Credentials = new NetworkCredential("guias@globalsegmed.com.br", "Sergio2024@");

                    objEmail.ReplyTo = new MailAddress("guias@globalsegmed.com.br");
                    objEmail.CC.Add("guias@globalsegmed.com.br");

                    if (xPara.IndexOf("asos@globalsegmed.com.br") < 0)
                    {
                        objEmail.CC.Add("asos@globalsegmed.com.br");
                    }
                }
            }
            else
            {
                objEmail.From = new MailAddress("guias@globalsegmed.com.br");
                objSmtp.Credentials = new NetworkCredential("guias@globalsegmed.com.br", "Sergio2024@");

                objEmail.ReplyTo = new MailAddress("guias@globalsegmed.com.br");
                objEmail.CC.Add("guias@globalsegmed.com.br");

                if (xPara.IndexOf("asos@globalsegmed.com.br") < 0)
                {
                    objEmail.CC.Add("asos@globalsegmed.com.br");
                }
            }

            //objEmail.From = new MailAddress("guias2@globalsegmed.com.br");            
            //objSmtp.Credentials = new NetworkCredential("guias2@globalsegmed.com.br", "Guiasglobal2#");

            //objEmail.ReplyTo = new MailAddress("guias2@globalsegmed.com.br");


            Ilitera.Data.Clientes_Clinicas xEnvio = new Ilitera.Data.Clientes_Clinicas();
            //checar se não é e-mail duplicado

            DataSet rDs = xEnvio.Checar_Envio_Email(xIdEmpregado, xIdEmpresa, xDestinatario, "Guia / ASO / PCI");

            if (rDs.Tables[0].Rows.Count == 0)
            {
                objSmtp.Send(objEmail);
                xEnvio.Envio_eMail(xIdEmpregado, xIdEmpresa, xDestinatario, "Guia / ASO / PCI");
            }


            return;

        }






        protected void Envio_Email_Fox(string xPara, string xCopia, string xSubject, string xBody, string xAttach, string xModulo, Int32 xIdEmpregado, Int32 xIdEmpresa)
        {
            string xDestinatario = "";
            //enviar e-mail para clinica e prajna

            MailMessage objEmail = new MailMessage();

            //rementente do email
            //objEmail.From = new MailAddress("agendamento.sp.sto@ilitera.com.br");


            //caixa-postal de onde será enviado o e-mail
            //objEmail.ReplyTo = new MailAddress("email@seusite.com.br");

            //para
            //objEmail.To.Add("lasdowsky@gmail.com");
            string xEmail = "";

            xEmail = exame.IdJuridica.Email.ToString().Trim();

            if (xEmail == "")
            {
                throw new Exception("Clínica não possui e-mail cadastrado.");
            }

            if (xEmail.IndexOf(";") > 0) xEmail = xEmail.Substring(0, xEmail.IndexOf(";"));

            objEmail.To.Add(xEmail);
            xDestinatario = xEmail + "; atendimento@ilitera.com.br ;";

            //objEmail.Bcc.Add("oculto@provedor.com.br");

            //objEmail.CC.Add("atendimento@ilitera.com.br");
            objEmail.CC.Add("engtanisia.rs.fox@ilitera.com.br");
            

            objEmail.Priority = MailPriority.Normal;
            objEmail.IsBodyHtml = true;

            objEmail.Subject = xSubject;
            objEmail.Body = xBody;
            objEmail.SubjectEncoding = Encoding.GetEncoding("ISO-8859-1");
            objEmail.BodyEncoding = Encoding.GetEncoding("ISO-8859-1");

            Attachment xItem = new Attachment(xAttach);
            objEmail.Attachments.Add(xItem);

            SmtpClient objSmtp = new SmtpClient();
            //objSmtp.Host = "smtp.ilitera.com.br";
            objSmtp.Host = "smtp.office365.com";
            objSmtp.EnableSsl = true;
            objSmtp.Port = 587;

            if (System.DateTime.Now.Second % 3 == 0)
            {
                objEmail.From = new MailAddress("agendamento1@ilitera.com.br");
                objSmtp.Credentials = new NetworkCredential("agendamento1@ilitera.com.br", "Ilitera_3624");
            }
            else
            {
                objEmail.From = new MailAddress("agendamento3@ilitera.com.br");
                objSmtp.Credentials = new NetworkCredential("agendamento3@ilitera.com.br", "Ilitera_3625");
            }

            objEmail.ReplyTo = new MailAddress("agendamento@ilitera.com.br");


            Ilitera.Data.Clientes_Clinicas xEnvio = new Ilitera.Data.Clientes_Clinicas();
            //checar se não é e-mail duplicado

            DataSet rDs = xEnvio.Checar_Envio_Email(xIdEmpregado, xIdEmpresa, xDestinatario, "Guia / ASO / PCI");

            if (rDs.Tables[0].Rows.Count == 0)
            {
                objSmtp.Send(objEmail);
                xEnvio.Envio_eMail(xIdEmpregado, xIdEmpresa, xDestinatario, "Guia / ASO / PCI");
            }


            return;

        }



        protected void Envio_Email_Grafeno(string xPara, string xCopia, string xSubject, string xBody, string xAttach, string xModulo, Int32 xIdEmpregado, Int32 xIdEmpresa)
        {
            string xDestinatario = "";
            //enviar e-mail para clinica e prajna

            MailMessage objEmail = new MailMessage();

            //rementente do email
            objEmail.From = new MailAddress("agendamento.sp.gfn@ilitera.com.br");


            //caixa-postal de onde será enviado o e-mail
            //objEmail.ReplyTo = new MailAddress("email@seusite.com.br");

            //para
            //objEmail.To.Add("lasdowsky@gmail.com");
            string xEmail = "";

            xEmail = exame.IdJuridica.Email.ToString().Trim();

            if (xEmail == "")
            {
                throw new Exception("Clínica não possui e-mail cadastrado.");
            }

            if (xEmail.IndexOf(";") > 0) xEmail = xEmail.Substring(0, xEmail.IndexOf(";"));

            objEmail.To.Add(xEmail);
            xDestinatario = xEmail + "; artur.sp.gfn@ilitera.com.br ;";

            //objEmail.Bcc.Add("oculto@provedor.com.br");

            objEmail.CC.Add("artur.sp.gfn@ilitera.com.br");

            objEmail.Priority = MailPriority.Normal;
            objEmail.IsBodyHtml = true;

            objEmail.Subject = xSubject;
            objEmail.Body = xBody;
            objEmail.SubjectEncoding = Encoding.GetEncoding("ISO-8859-1");
            objEmail.BodyEncoding = Encoding.GetEncoding("ISO-8859-1");

            Attachment xItem = new Attachment(xAttach);
            objEmail.Attachments.Add(xItem);

            SmtpClient objSmtp = new SmtpClient();
            //objSmtp.Host = "smtp.ilitera.com.br";
            objSmtp.Host = "smtp.office365.com";
            objSmtp.EnableSsl = true;
            objSmtp.Port = 587;
            
            objSmtp.Credentials = new NetworkCredential("agendamento.sp.gfn@ilitera.com.br", "Ilitera_5044");


            Ilitera.Data.Clientes_Clinicas xEnvio = new Ilitera.Data.Clientes_Clinicas();
            //checar se não é e-mail duplicado

            DataSet rDs = xEnvio.Checar_Envio_Email(xIdEmpregado, xIdEmpresa, xDestinatario, "Guia / ASO / PCI");

            if (rDs.Tables[0].Rows.Count == 0)
            {
                objSmtp.Send(objEmail);
                xEnvio.Envio_eMail(xIdEmpregado, xIdEmpresa, xDestinatario, "Guia / ASO / PCI");
            }


            return;

        }



        protected void Envio_Email_Daiti(string xPara, string xCopia, string xSubject, string xBody, string xAttach, string xModulo, Int32 xIdEmpregado, Int32 xIdEmpresa)
        {
            string xDestinatario = "";
            //enviar e-mail para clinica e prajna

            MailMessage objEmail = new MailMessage();

            //rementente do email
            objEmail.From = new MailAddress("agendamento.sp.dai@ilitera.com.br");



            string xEmail = "";

            xEmail = exame.IdJuridica.Email.ToString().Trim();

            if (xEmail == "")
            {
                throw new Exception("Clínica não possui e-mail cadastrado.");
            }

            if (xEmail.IndexOf(";") > 0) xEmail = xEmail.Substring(0, xEmail.IndexOf(";"));

            //xEmail = "credenciamento.sp.dai@ilitera.com.br";
            objEmail.To.Add(xEmail);
            //xDestinatario = xEmail + "; atendimento@ilitera.com.br ;";

            //objEmail.Bcc.Add("oculto@provedor.com.br");

            //objEmail.CC.Add("credenciamento.sp.dai@ilitera.com.br");

            //if (exame.IdJuridica.NomeAbreviado.ToUpper().IndexOf("ARATEL") > 0)
            //{
            objEmail.CC.Add("agendamento.sp.dai@ilitera.com.br");
            //}

            objEmail.Priority = MailPriority.Normal;
            objEmail.IsBodyHtml = true;

            objEmail.Subject = xSubject;
            objEmail.Body = xBody;
            objEmail.SubjectEncoding = Encoding.GetEncoding("ISO-8859-1");
            objEmail.BodyEncoding = Encoding.GetEncoding("ISO-8859-1");

            Attachment xItem = new Attachment(xAttach);
            objEmail.Attachments.Add(xItem);

            SmtpClient objSmtp = new SmtpClient();
            //objSmtp.Host = "smtp.ilitera.com.br";
            objSmtp.Host = "smtp.office365.com";
            objSmtp.EnableSsl = true;
            objSmtp.Port = 587;
            //objSmtp.Credentials = new NetworkCredential("agendamento.sp.sto@ilitera.com.br", "bibi6096");
            objSmtp.Credentials = new NetworkCredential("agendamento.sp.dai@ilitera.com.br", "Daiiti_7654");

            objSmtp.Send(objEmail);


            Ilitera.Data.Clientes_Clinicas xEnvio = new Ilitera.Data.Clientes_Clinicas();
            xEnvio.Envio_eMail(xIdEmpregado, xIdEmpresa, xDestinatario, "Guia / ASO / PCI");

            return;

        }

        
        protected  void CreatePDFMerged(CrystalDecisions.CrystalReports.Engine.ReportClass[] reports, HttpResponse response, string watermark, bool RenumerarPaginas)
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

        protected  void CreatePDFMerged(Stream[] streams, HttpResponse response, string watermark, bool RenumerarPaginas)
        {
            MemoryStream reportStream = PdfSharpUtility.MergeReports(streams, string.Empty, watermark, RenumerarPaginas);

            exame.IdEmpregado.Find();
            exame.IdEmpregado.nID_EMPR.Find();
            Cliente xCliente = new Cliente(exame.IdEmpregado.nID_EMPR.Id);

            exame.IdJuridica.Find();
            Clinica xClinica = new Clinica(exame.IdJuridica.Id);



            if (xEnvio_Email == "S")
            {

                if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().IndexOf("Prajna") > 0)
                {


                    string xPath = "I:\\temp\\guia_t" + System.DateTime.Now.Second.ToString().Trim() + System.DateTime.Now.Minute.ToString().Trim() + System.DateTime.Now.Millisecond.ToString().Trim() + System.DateTime.Now.Day.ToString().Trim() + System.DateTime.Now.Month.ToString().Trim() + ".pdf";

                    FileStream fileStream = File.Create(xPath, (int)reportStream.Length);
                    // Initialize the bytes array with the stream length and then fill it with data
                    byte[] bytesInStream = new byte[reportStream.Length];
                    reportStream.Read(bytesInStream, 0, bytesInStream.Length);
                    // Use write method to write to the file specified above
                    fileStream.Write(bytesInStream, 0, bytesInStream.Length);

                    fileStream.Flush();

                    fileStream.Dispose();
                    fileStream = null;


                    string xEmpresa = "";

                    if (xCliente.IdJuridicaPai.Id != 0 && xCliente.IdJuridicaPai.Id != xCliente.Id)
                    {
                        if (xCliente.IdJuridicaPai.ToString().ToUpper().IndexOf("KNOX") > 0)
                        {
                            xEmpresa  = xCliente.IdJuridicaPai.ToString(); 
                        }
                        else
                        {
                            xEmpresa = xCliente.IdJuridicaPai.ToString() + "   Unidade: " + xCliente.NomeAbreviado.ToString();
                        }
                    }
                    else
                    {
                        xEmpresa = xCliente.GetNomeEmpresa();
                    }



                    string xCorpo = "<body><p style='font-family:Tahoma;font-size:11px;text-align:center;color:black > </p>" +
                                    "<p style='font-family:Tahoma;font-size:11px;text-align:center;color:black>***Este é um e - mail automático enviado através de sistema informatizado, favor não responder. *** </p><br>" +
                                    "<p style='font-family:Tahoma;font-size:22px;text-align:center;color:black> </p>" +
                                    "<p style='font-family:Tahoma;font-size:24px;text-align:center;color:black>Guia de Encaminhamento</p><br>" +
                                    "<p style='font-family:Tahoma;font-size:18px;text-align:center;text-decoration:underline;color:black> </p>" +
                                    "<p style='font-family:Tahoma;font-size:18px;text-align:center;text-decoration:underline;color:black> ORIENTAÇÕES GERAIS: </p><br>" +
                                    "<u><font color='red'>ASO INAPTO</u></font> – não entregar para os colaboradores, enviar por e - mail para: centraldeatendimento@essencenet.com.br informando o motivo da INAPTIDÃO. O Médico Coordenador da ESSENCE deverá se envolver na decisão e comunicação à empresa, em seguida entraremos em contato para a liberação do ASO.<br><br>" +
                                    "<u><font color='red'>ASO em CASOS ESPECIAIS</u></font> – não entregar para os colaboradores, enviar por e - mail para: centraldeatendimento@essencenet.com.br <br> " +
                                    "Consideramos casos especiais as situações em que o colaborador apresenta muitos atestados médicos, às vezes sem NEXO com a função, ou quando o colaborador refere dores, incapacidade para o trabalho, situações de difícil decisão.<br>" +
                                    "O Médico Coordenador da ESSENCE deverá se envolver na decisão e comunicação à empresa, em seguida entraremos em contato para a liberação do ASO.<br><br>";


                    xCorpo = xCorpo + "<u><font color='red'>INFORMAÇÕES DO ASO PARA eSOCIAL</u></font> – Em atendimento ao eSOCIAL, solicitamos que ao final de cada dia sejam enviados todos os ASO´s dos atendimentos do dia digitalizados por e-mail para: centraldeatendimento@essencenet.com.br </font><br><br><br>";


                    //xCorpo = xCorpo + "<table style='width:100%' border='1'> <tr><th>Data Exame</th><th>Tipo Exame</th><th>Nome do Funcionário</th><th>Empresa</th><th>Resultado do Exame</th><th>Nome do Médico</th><th>CRM</th><th>UF CRM</th></tr> " +
                    //                  "<tr><th>XX/XX/XX</th><th>Admissional</th><th>Fulano da Silva</th><th>Fort Knox</th><th>Apto</th><th>João da Silva</th><th>01010102</th><th>SP</th></tr> " +
                    //                  "<tr><th>XX/XX/XX</th><th>Hemograma</th><th>Fulano da Silva</th><th>Fort Knox</th><th>Normal</th><th>João da Silva</th><th>01010102</th><th>SP</th></tr> " +
                    //                  "</table><br>";

                     xCorpo = xCorpo + "<p style='font-family:Tahoma;font-size:18px;text-align:center;text-decoration:underline;color:black> </p>" +
                                      "<p style='font-family:Tahoma;font-size:18px;text-align:center;text-decoration:underline;color:black> DADOS DO AGENDAMENTO: </p><br>" +
                                      "<p><font size='3' face='Tahoma'>Nome: " + exame.IdEmpregado.tNO_EMPG + "<br>" +
                                      "RG: " + exame.IdEmpregado.tNO_IDENTIDADE.ToString() + "<br>" +
                                      "Data de Nascimento: " + exame.IdEmpregado.hDT_NASC.ToString("dd/MM/yyyy") + "<br><br>" +
                                      "Empresa:  " + xEmpresa + "<br>" +
                                      "CNPJ: " + exame.IdEmpregado.nID_EMPR.GetCnpj() + "<br>" +
                                      "Cargo: " + exame.IdEmpregadoFuncao.nID_FUNCAO.ToString() + "<br>" +
                                       "Setor: " + exame.IdEmpregadoFuncao.nID_SETOR.ToString() + "<br>" +
                                       "Unidade de Trabalho: " + exame.IdEmpregadoFuncao.GetLocalDeTrabalho(exame.IdEmpregado.nID_EMPR) + "<br>" +
                                       "Clinica: " + exame.IdJuridica.NomeAbreviado + "<br>" +
                                       "Endereço da clinica: " + xClinica.GetEndereco().IdTipoLogradouro.NomeCompleto + " " + xClinica.GetEndereco().Logradouro + " " + xClinica.GetEndereco().Numero + " " + xClinica.GetEndereco().Bairro + " " + xClinica.GetEndereco().Municipio + "/" + xClinica.GetEndereco().Uf + "   Fone: " + exame.IdJuridica.GetContatoTelefonico() + "<br>" +
                                       "Tipo de Exame: " + exame.IdExameDicionario.Nome + " <br>" +
                                       "Data do Exame: " + exame.DataExame.ToString("dd/MM/yyyy") + "<br><br><br><H5>Atenção: Esta é uma mensagem automática e não deve ser respondida. Respostas a este e-mail não serão lidas e não serão encaminhadas para os destinatários apropriados. Por favor, entre em contato com a Essence para mais informações.</H5></font></p></body>";




                    //string xCorpo = "<body><p><font size='2' face='Tahoma'><H1 align='center'>Guia de Encaminhamento</H1></font></p> <br></br>" +
                    //               "<p><font size='3' face='Tahoma'>Nome: " + exame.IdEmpregado.tNO_EMPG + "<br>" +
                    //               "RG: " + exame.IdEmpregado.tNO_IDENTIDADE.ToString() + "<br>" +
                    //               "Data de Nascimento: " + exame.IdEmpregado.hDT_NASC.ToString("dd/MM/yyyy") + "<br><br>" +
                    //               "Empresa:  " + xEmpresa + "<br>" +
                    //               "CNPJ: " + exame.IdEmpregado.nID_EMPR.GetCnpj() + "<br>" +
                    //               "Cargo: " + exame.IdEmpregadoFuncao.nID_FUNCAO.ToString() + "<br>" +
                    //                "Setor: " + exame.IdEmpregadoFuncao.nID_SETOR.ToString() + "<br>" +
                    //                "Unidade de Trabalho: " + exame.IdEmpregadoFuncao.GetLocalDeTrabalho(exame.IdEmpregado.nID_EMPR) + "<br>" +
                    //                "Clinica: " + exame.IdJuridica.NomeAbreviado + "<br>" +
                    //                "Endereço da clinica: " + xClinica.GetEndereco().IdTipoLogradouro.NomeCompleto + " " + xClinica.GetEndereco().Logradouro + " " + xClinica.GetEndereco().Numero + " " + xClinica.GetEndereco().Bairro + " " + xClinica.GetEndereco().Municipio + "/" + xClinica.GetEndereco().Uf + "   Fone: " + exame.IdJuridica.GetContatoTelefonico() + "<br>" +
                    //                "Tipo de Exame: " + exame.IdExameDicionario.Nome + " <br>" +
                    //                "Data do Exame: " + exame.DataExame.ToString("dd/MM/yyyy") + " </font></p></body>";




                    //"Segue em anexo Guia de Encaminhamento / ASO / PCI do Colaborador " + exame.IdEmpregado.tNO_EMPG + " da empresa " + exame.IdEmpregado.nID_EMPR.NomeAbreviado, xPath, "Guia / ASO / PCI"

                    Envio_Email_Prajna(exame.IdJuridica.Email.ToString(), "agendamento@5aessence.com.br", "Guia de Encaminhamento (não responder)", xCorpo, xPath, "Guia / ASO / PCI", exame.IdEmpregado.Id, exame.IdEmpregado.nID_EMPR.Id);
                }
                else if (Ilitera.Data.SQLServer.EntitySQLServer._LocalServer.ToUpper().IndexOf("FOCUS") < 0 && Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("SAFETY") > 0)  // if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().Trim().ToUpper().IndexOf("SAFETY") > 0 )
                {

                    string xPath = "I:\\temp\\guia_grafeno_" + System.DateTime.Now.Second.ToString().Trim() + System.DateTime.Now.Minute.ToString().Trim() + System.DateTime.Now.Millisecond.ToString().Trim() + System.DateTime.Now.Day.ToString().Trim() + System.DateTime.Now.Month.ToString().Trim() + ".pdf";

                    FileStream fileStream = File.Create(xPath, (int)reportStream.Length);
                    // Initialize the bytes array with the stream length and then fill it with data
                    byte[] bytesInStream = new byte[reportStream.Length];
                    reportStream.Read(bytesInStream, 0, bytesInStream.Length);
                    // Use write method to write to the file specified above
                    fileStream.Write(bytesInStream, 0, bytesInStream.Length);

                    fileStream.Flush();

                    fileStream.Dispose();
                    fileStream = null;

                    string xEmpresa = "";

                    if (xCliente.IdJuridicaPai.Id != 0 && xCliente.IdJuridicaPai.Id != xCliente.Id)
                    {
                        xEmpresa = xCliente.IdJuridicaPai.ToString() + "   Unidade: " + xCliente.NomeAbreviado.ToString();
                    }
                    else
                    {
                        xEmpresa = xCliente.GetNomeEmpresa();
                    }



                    string xCorpo = "<body><p><font size='2' face='Tahoma'><H1 align='center'>Guia de Encaminhamento</H1></font></p> <br></br>" +
                                    "<p><font size='3' face='Tahoma'>Nome: " + exame.IdEmpregado.tNO_EMPG + "<br>" +
                                    "RG: " + exame.IdEmpregado.tNO_IDENTIDADE.ToString() + "<br>" +
                                    "Data de Nascimento: " + exame.IdEmpregado.hDT_NASC.ToString("dd/MM/yyyy") + "<br><br>" +
                                    "Empresa:  " + xEmpresa + "<br>" +
                                    "CNPJ: " + exame.IdEmpregado.nID_EMPR.GetCnpj() + "<br>" +
                                    "Cargo: " + exame.IdEmpregadoFuncao.nID_FUNCAO.ToString() + "<br>" +
                                     "Setor: " + exame.IdEmpregadoFuncao.nID_SETOR.ToString() + "<br>" +
                                     "Unidade de Trabalho: " + exame.IdEmpregadoFuncao.GetLocalDeTrabalho(exame.IdEmpregado.nID_EMPR) + "<br>" +
                                     "Clinica: " + exame.IdJuridica.NomeAbreviado + "<br>" +
                                     "Endereço da clinica: " + xClinica.GetEndereco().IdTipoLogradouro.NomeCompleto + " " + xClinica.GetEndereco().Logradouro + " " + xClinica.GetEndereco().Numero + " " + xClinica.GetEndereco().Bairro + " " + xClinica.GetEndereco().Municipio + "/" + xClinica.GetEndereco().Uf + "   Fone: " + exame.IdJuridica.GetContatoTelefonico() + "<br>" +
                                     "Tipo de Exame: " + exame.IdExameDicionario.Nome + " <br>" +
                                     "Data do Exame: " + exame.DataExame.ToString("dd/MM/yyyy") + "<br><br><br><H5>Atenção: Esta é uma mensagem automática e não deve ser respondida. Respostas a este e-mail não serão lidas e não serão encaminhadas para os destinatários apropriados. Por favor, entre em contato com a Ilitera para mais informações.</H5></font></p></body>";

                    //"Segue em anexo Guia de Encaminhamento / ASO / PCI do Colaborador " + exame.IdEmpregado.tNO_EMPG + " da empresa " + exame.IdEmpregado.nID_EMPR.NomeAbreviado, xPath, "Guia / ASO / PCI"

                    Envio_Email_Grafeno(exame.IdJuridica.Email.ToString(), "agendamento.sp.sto@ilitera.com.br", "Guia de Encaminhamento (não responder)", xCorpo, xPath, "Guia / ASO / PCI", exame.IdEmpregado.Id, exame.IdEmpregado.nID_EMPR.Id);

                }
                else if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().IndexOf("Daiti") > 0)
                {

                    string xPath = "I:\\temp\\guia_Daiti_" + System.DateTime.Now.Second.ToString().Trim() + System.DateTime.Now.Minute.ToString().Trim() + System.DateTime.Now.Millisecond.ToString().Trim() + System.DateTime.Now.Day.ToString().Trim() + System.DateTime.Now.Month.ToString().Trim() + ".pdf";

                    FileStream fileStream = File.Create(xPath, (int)reportStream.Length);
                    // Initialize the bytes array with the stream length and then fill it with data
                    byte[] bytesInStream = new byte[reportStream.Length];
                    reportStream.Read(bytesInStream, 0, bytesInStream.Length);
                    // Use write method to write to the file specified above
                    fileStream.Write(bytesInStream, 0, bytesInStream.Length);

                    fileStream.Flush();

                    fileStream.Dispose();
                    fileStream = null;


                    string xEmpresa = "";

                    if (xCliente.IdJuridicaPai.Id != 0 && xCliente.IdJuridicaPai.Id != xCliente.Id)
                    {
                        xEmpresa = xCliente.IdJuridicaPai.ToString() + "   Unidade: " + xCliente.NomeAbreviado.ToString();
                    }
                    else
                    {
                        xEmpresa = xCliente.GetNomeEmpresa();
                    }



                    string xCorpo = "<body><p><font size='2' face='Tahoma'><H1 align='center'>Guia de Encaminhamento</H1></font></p> <br></br>" +
                                    "<p><font size='3' face='Tahoma'>Nome: " + exame.IdEmpregado.tNO_EMPG + "<br>" +
                                    "RG: " + exame.IdEmpregado.tNO_IDENTIDADE.ToString() + "      CPF: " +  exame.IdEmpregado.tNO_CPF + " <br>" +
                                    "Data de Nascimento: " + exame.IdEmpregado.hDT_NASC.ToString("dd/MM/yyyy") + "<br><br>" +
                                    "Empresa:  " + xEmpresa + "<br>" +
                                    "CNPJ: " + exame.IdEmpregado.nID_EMPR.GetCnpj() + "<br>" +
                                    "Cargo: " + exame.IdEmpregadoFuncao.nID_FUNCAO.ToString() + "<br>" +
                                     "Setor: " + exame.IdEmpregadoFuncao.nID_SETOR.ToString() + "<br>" +
                                     "Unidade de Trabalho: " + exame.IdEmpregadoFuncao.GetLocalDeTrabalho(exame.IdEmpregado.nID_EMPR) + "<br>" +
                                     "Clinica: " + exame.IdJuridica.NomeAbreviado + "<br>" +
                                     "Endereço da clinica: " + xClinica.GetEndereco().IdTipoLogradouro.NomeCompleto + " " + xClinica.GetEndereco().Logradouro + " " + xClinica.GetEndereco().Numero + " " + xClinica.GetEndereco().Bairro + " " + xClinica.GetEndereco().Municipio + "/" + xClinica.GetEndereco().Uf + "   Fone: " + exame.IdJuridica.GetContatoTelefonico() + "<br>" +
                                     "Tipo de Exame: " + exame.IdExameDicionario.Nome + " <br>" +
                                     "Data do Exame: " + exame.DataExame.ToString("dd/MM/yyyy") + "<br><br><br><H5>Atenção: Esta é uma mensagem automática e não deve ser respondida. Respostas a este e-mail não serão lidas e não serão encaminhadas para os destinatários apropriados. Por favor, entre em contato com a Ilitera para mais informações.</H5></font></p></body>";

                    //"Segue em anexo Guia de Encaminhamento / ASO / PCI do Colaborador " + exame.IdEmpregado.tNO_EMPG + " da empresa " + exame.IdEmpregado.nID_EMPR.NomeAbreviado, xPath, "Guia / ASO / PCI"

                    Envio_Email_Daiti(exame.IdJuridica.Email.ToString(), "agendamento.sp.sto@ilitera.com.br", "Guia de Encaminhamento (não responder)", xCorpo, xPath, "Guia / ASO / PCI", exame.IdEmpregado.Id, exame.IdEmpregado.nID_EMPR.Id);

                }
                else if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("GLOBAL") > 0)
                {

                    string xPath = "I:\\temp\\guia_global_" + System.DateTime.Now.Second.ToString().Trim() + System.DateTime.Now.Minute.ToString().Trim() + System.DateTime.Now.Millisecond.ToString().Trim() + System.DateTime.Now.Day.ToString().Trim() + System.DateTime.Now.Month.ToString().Trim() + ".pdf";

                    FileStream fileStream = File.Create(xPath, (int)reportStream.Length);
                    // Initialize the bytes array with the stream length and then fill it with data
                    byte[] bytesInStream = new byte[reportStream.Length];
                    reportStream.Read(bytesInStream, 0, bytesInStream.Length);
                    // Use write method to write to the file specified above
                    fileStream.Write(bytesInStream, 0, bytesInStream.Length);

                    fileStream.Flush();

                    fileStream.Dispose();
                    fileStream = null;

                    string xEmpresa = "";

                    if (xCliente.IdJuridicaPai.Id != 0 && xCliente.IdJuridicaPai.Id != xCliente.Id)
                    {
                        xEmpresa = xCliente.IdJuridicaPai.ToString() + "   Unidade: " + xCliente.NomeAbreviado.ToString();
                    }
                    else
                    {
                        xEmpresa = xCliente.GetNomeEmpresa();
                    }



                    string xCorpo = "<body><p><font size='2' face='Tahoma'><H1 align='center'>Guia de Encaminhamento</H1></font></p> <br></br>" +
                                    "<p><font size='3' face='Tahoma'>Nome: " + exame.IdEmpregado.tNO_EMPG + "<br>" +
                                    "RG: " + exame.IdEmpregado.tNO_IDENTIDADE.ToString() + "<br>" +
                                    "Data de Nascimento: " + exame.IdEmpregado.hDT_NASC.ToString("dd/MM/yyyy") + "<br><br>" +
                                    "Empresa:  " + xEmpresa + "<br>" +
                                    "CNPJ: " + exame.IdEmpregado.nID_EMPR.GetCnpj() + "<br>" +
                                    "Cargo: " + exame.IdEmpregadoFuncao.nID_FUNCAO.ToString() + "<br>" +
                                     "Setor: " + exame.IdEmpregadoFuncao.nID_SETOR.ToString() + "<br>" +
                                     "Unidade de Trabalho: " + exame.IdEmpregadoFuncao.GetLocalDeTrabalho(exame.IdEmpregado.nID_EMPR) + "<br>" +
                                     "Clinica: " + exame.IdJuridica.NomeAbreviado + "<br>" +
                                     "Endereço da clinica: " + xClinica.GetEndereco().IdTipoLogradouro.NomeCompleto + " " + xClinica.GetEndereco().Logradouro + " " + xClinica.GetEndereco().Numero + " " + xClinica.GetEndereco().Bairro + " " + xClinica.GetEndereco().Municipio + "/" + xClinica.GetEndereco().Uf + "   Fone: " + exame.IdJuridica.GetContatoTelefonico() + "<br>" +
                                     "Tipo de Exame: " + exame.IdExameDicionario.Nome + " <br>" +
                                     "Data do Exame: " + exame.DataExame.ToString("dd/MM/yyyy") + "<br><br><br><H5>Atenção: Esta é uma mensagem automática e não deve ser respondida. Respostas a este e-mail não serão lidas e não serão encaminhadas para os destinatários apropriados. Por favor, entre em contato com a Ilitera para mais informações.</H5></font></p></body>";

                    //"Segue em anexo Guia de Encaminhamento / ASO / PCI do Colaborador " + exame.IdEmpregado.tNO_EMPG + " da empresa " + exame.IdEmpregado.nID_EMPR.NomeAbreviado, xPath, "Guia / ASO / PCI"

                    Envio_Email_Global(exame.IdJuridica.Email.ToString(), "agendamento.sp.sto@ilitera.com.br", "Guia de Encaminhamento (não responder)", xCorpo, xPath, "Guia / ASO / PCI", exame.IdEmpregado.Id, exame.IdEmpregado.nID_EMPR.Id);

                }
                else if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("FOX") > 0)
                {

                    string xPath = "I:\\temp\\guia_fox_" + System.DateTime.Now.Second.ToString().Trim() + System.DateTime.Now.Minute.ToString().Trim() + System.DateTime.Now.Millisecond.ToString().Trim() + System.DateTime.Now.Day.ToString().Trim() + System.DateTime.Now.Month.ToString().Trim() + ".pdf";

                    FileStream fileStream = File.Create(xPath, (int)reportStream.Length);
                    // Initialize the bytes array with the stream length and then fill it with data
                    byte[] bytesInStream = new byte[reportStream.Length];
                    reportStream.Read(bytesInStream, 0, bytesInStream.Length);
                    // Use write method to write to the file specified above
                    fileStream.Write(bytesInStream, 0, bytesInStream.Length);

                    fileStream.Flush();

                    fileStream.Dispose();
                    fileStream = null;


                    string xEmpresa = "";

                    if (xCliente.IdJuridicaPai.Id != 0 && xCliente.IdJuridicaPai.Id != xCliente.Id)
                    {
                        xEmpresa = xCliente.IdJuridicaPai.ToString() + "   Unidade: " + xCliente.NomeAbreviado.ToString();
                    }
                    else
                    {
                        xEmpresa = xCliente.GetNomeEmpresa();
                    }



                    string xCorpo = "<body><p><font size='2' face='Tahoma'><H1 align='center'>Guia de Encaminhamento</H1></font></p> <br></br>" +
                                    "<p><font size='3' face='Tahoma'>Nome: " + exame.IdEmpregado.tNO_EMPG + "<br>" +
                                    "RG: " + exame.IdEmpregado.tNO_IDENTIDADE.ToString() + "<br>" +
                                    "Data de Nascimento: " + exame.IdEmpregado.hDT_NASC.ToString("dd/MM/yyyy") + "<br><br>" +
                                    "Empresa:  " + xEmpresa + "<br>" +
                                    "CNPJ: " + exame.IdEmpregado.nID_EMPR.GetCnpj() + "<br>" +
                                    "Cargo: " + exame.IdEmpregadoFuncao.nID_FUNCAO.ToString() + "<br>" +
                                     "Setor: " + exame.IdEmpregadoFuncao.nID_SETOR.ToString() + "<br>" +
                                     "Unidade de Trabalho: " + exame.IdEmpregadoFuncao.GetLocalDeTrabalho(exame.IdEmpregado.nID_EMPR) + "<br>" +
                                     "Clinica: " + exame.IdJuridica.NomeAbreviado + "<br>" +
                                     "Endereço da clinica: " + xClinica.GetEndereco().IdTipoLogradouro.NomeCompleto + " " + xClinica.GetEndereco().Logradouro + " " + xClinica.GetEndereco().Numero + " " + xClinica.GetEndereco().Bairro + " " + xClinica.GetEndereco().Municipio + "/" + xClinica.GetEndereco().Uf + "   Fone: " + exame.IdJuridica.GetContatoTelefonico() + "<br>" +
                                     "Tipo de Exame: " + exame.IdExameDicionario.Nome + " <br>" +
                                     "Data do Exame: " + exame.DataExame.ToString("dd/MM/yyyy") + "<br><br><br><H5>Atenção: Esta é uma mensagem automática e não deve ser respondida. Respostas a este e-mail não serão lidas e não serão encaminhadas para os destinatários apropriados. Por favor, entre em contato com a Ilitera para mais informações.</H5></font></p></body>";

                    //"Segue em anexo Guia de Encaminhamento / ASO / PCI do Colaborador " + exame.IdEmpregado.tNO_EMPG + " da empresa " + exame.IdEmpregado.nID_EMPR.NomeAbreviado, xPath, "Guia / ASO / PCI"

                    Envio_Email_Fox(exame.IdJuridica.Email.ToString(), "agendamento.sp.sto@ilitera.com.br", "Guia de Encaminhamento (não responder)", xCorpo, xPath, "Guia / ASO / PCI", exame.IdEmpregado.Id, exame.IdEmpregado.nID_EMPR.Id);

                }
                else //if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().Trim().ToUpper() == "OPSA")
                {

                    string xPath = "I:\\temp\\guia_ilitera_" + System.DateTime.Now.Second.ToString().Trim() + System.DateTime.Now.Minute.ToString().Trim() + System.DateTime.Now.Millisecond.ToString().Trim() + System.DateTime.Now.Day.ToString().Trim() + System.DateTime.Now.Month.ToString().Trim() + ".pdf";

                    FileStream fileStream = File.Create(xPath, (int)reportStream.Length);
                    // Initialize the bytes array with the stream length and then fill it with data
                    byte[] bytesInStream = new byte[reportStream.Length];
                    reportStream.Read(bytesInStream, 0, bytesInStream.Length);
                    // Use write method to write to the file specified above
                    fileStream.Write(bytesInStream, 0, bytesInStream.Length);

                    fileStream.Flush();

                    fileStream.Dispose();
                    fileStream = null;

                    string xEmpresa = "";

                    if (xCliente.IdJuridicaPai.Id != 0 && xCliente.IdJuridicaPai.Id != xCliente.Id)
                    {
                        xEmpresa = xCliente.IdJuridicaPai.ToString() + "   Unidade: " + xCliente.NomeAbreviado.ToString();
                    }
                    else
                    {
                        xEmpresa = xCliente.GetNomeEmpresa();
                    }



                    string xCorpo = "<body><p><font size='2' face='Tahoma'><H1 align='center'>Guia de Encaminhamento</H1></font></p> <br></br>" +
                                    "<p><font size='3' face='Tahoma'>Nome: " + exame.IdEmpregado.tNO_EMPG + "<br>" +
                                    "RG: " + exame.IdEmpregado.tNO_IDENTIDADE.ToString() + "<br>" +
                                    "Data de Nascimento: " + exame.IdEmpregado.hDT_NASC.ToString("dd/MM/yyyy") + "<br><br>" +
                                    "Empresa:  " + xEmpresa + "<br>" +
                                    "CNPJ: " + exame.IdEmpregado.nID_EMPR.GetCnpj() + "<br>" +
                                    "Cargo: " + exame.IdEmpregadoFuncao.nID_FUNCAO.ToString() + "<br>" +
                                     "Setor: " + exame.IdEmpregadoFuncao.nID_SETOR.ToString() + "<br>" +
                                     "Unidade de Trabalho: " + exame.IdEmpregadoFuncao.GetLocalDeTrabalho(exame.IdEmpregado.nID_EMPR) + "<br>" +
                                     "Clinica: " + exame.IdJuridica.NomeAbreviado + "<br>" +
                                     "Endereço da clinica: " + xClinica.GetEndereco().IdTipoLogradouro.NomeCompleto + " " + xClinica.GetEndereco().Logradouro + " " + xClinica.GetEndereco().Numero + " " + xClinica.GetEndereco().Bairro + " " + xClinica.GetEndereco().Municipio + "/" + xClinica.GetEndereco().Uf + "   Fone: " + exame.IdJuridica.GetContatoTelefonico() + "<br>" +
                                     "Tipo de Exame: " + exame.IdExameDicionario.Nome + " <br>" +
                                     "Data do Exame: " + exame.DataExame.ToString("dd/MM/yyyy") + "<br><br><br><H5>Atenção: Esta é uma mensagem automática e não deve ser respondida. Respostas a este e-mail não serão lidas e não serão encaminhadas para os destinatários apropriados. Por favor, entre em contato com a Ilitera para mais informações.</H5></font></p></body>";

                    //"Segue em anexo Guia de Encaminhamento / ASO / PCI do Colaborador " + exame.IdEmpregado.tNO_EMPG + " da empresa " + exame.IdEmpregado.nID_EMPR.NomeAbreviado, xPath, "Guia / ASO / PCI"

                    Envio_Email_Ilitera(exame.IdJuridica.Email.ToString(), "agendamento.sp.sto@ilitera.com.br", "Guia de Encaminhamento (não responder)", xCorpo, xPath, "Guia / ASO / PCI", exame.IdEmpregado.Id, exame.IdEmpregado.nID_EMPR.Id);

                }



            }


            //enviar email alerta - inserção colaborador
            if (xCliente.Alerta_web_Agendamento!=null && xCliente.Alerta_web_Agendamento.Trim() != "")
            {
                System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");
                Entities.Usuario usuario = (Entities.Usuario)Session["usuarioLogado"];

                exame.IdEmpregadoFuncao.Find();

                string xCorpo = "<body><p><font size='2' face='Tahoma'><H1 align='center'>Alerta de Envio de Convocação de Exame - Ili.Net</H1></font></p> <br></br>" +
                                    "<p><font size='3' face='Tahoma'>Nome: " + exame.IdEmpregado.tNO_EMPG + "<br>" +                                    
                                    "Data de Nascimento: " + exame.IdEmpregado.hDT_NASC.ToString("dd/MM/yyyy") + "<br><br>" +
                                    "Empresa:  " + xCliente.NomeAbreviado + "<br>" +
                                    "CNPJ: " + exame.IdEmpregado.nID_EMPR.GetCnpj() + "<br>" +
                                    "Cargo: " + exame.IdEmpregadoFuncao.nID_FUNCAO.ToString() + "<br>" +
                                     "Setor: " + exame.IdEmpregadoFuncao.nID_SETOR.ToString() + "<br>" +
                                     "Unidade de Trabalho: " + exame.IdEmpregadoFuncao.GetLocalDeTrabalho(exame.IdEmpregado.nID_EMPR) + "<br>" +
                                     "Clinica: " + exame.IdJuridica.NomeAbreviado + "<br>" +
                                     "Endereço da clinica: " + xClinica.GetEndereco().IdTipoLogradouro.NomeCompleto + " " + xClinica.GetEndereco().Logradouro + " " + xClinica.GetEndereco().Numero + " " + xClinica.GetEndereco().Bairro + " " + xClinica.GetEndereco().Municipio + "/" + xClinica.GetEndereco().Uf + "   Fone: " + exame.IdJuridica.GetContatoTelefonico() + "<br>" +
                                     "Tipo de Exame: " + exame.IdExameDicionario.Nome + " <br>" +
                                     "Data do Exame: " + exame.DataExame.ToString("dd/MM/yyyy") + "<br><br>" +
                                     "Usuário responsável: " + usuario.NomeUsuario.ToUpper() + " em " + System.DateTime.Now.ToString("dd/MM/yyyy hh:mm", ptBr) + "<br></font></p></body>";



                if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().IndexOf("Prajna") > 0)
                {
                    Envio_Email_Prajna_Alerta(xCliente.Alerta_web_Agendamento.Trim(), "", "Alerta de Envio de Convocação de Exame", xCorpo, "", "Convocação de Exame", exame.IdEmpregado.Id, exame.IdEmpregado.nID_EMPR.Id);
                }
                else
                {
                    exame.IdEmpregado.Find();
                    exame.IdEmpregado.nID_EMPR.Find();

                    if (exame.IdEmpregado.nID_EMPR.NomeAbreviado.ToUpper().IndexOf("JOHN DEERE") >= 0)
                    {
                        if ( exame.IdExameDicionario.Id==5)
                        {
                            Envio_Email_Ilitera_Alerta(xCliente.Alerta_web_Agendamento.Trim(), "", "Alerta de Envio de Convocação de Exame", xCorpo, "", "Convocação de Exame", exame.IdEmpregado.Id, exame.IdEmpregado.nID_EMPR.Id);
                        }
                    }
                    else
                    {

                        if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("GLOBAL") > 0)
                            Envio_Email_Global_Alerta(xCliente.Alerta_web_Agendamento.Trim(), "", "Alerta de Envio de Convocação de Exame", xCorpo, "", "Convocação de Exame", exame.IdEmpregado.Id, exame.IdEmpregado.nID_EMPR.Id);
                        else
                            Envio_Email_Ilitera_Alerta(xCliente.Alerta_web_Agendamento.Trim(), "", "Alerta de Envio de Convocação de Exame", xCorpo, "", "Convocação de Exame", exame.IdEmpregado.Id, exame.IdEmpregado.nID_EMPR.Id);
                    }


                }
            }



            //System.IO.File.Delete(xPath);
            //esse i:\\temp\\ ele considera do webserver ou da máquina local ??

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


        private int Gerar_CodBusca()
        {
            string zRetorno = "0";

            string zFranqueado = "";
            Int32 zSequencia = 0;

            Int32 zCodMax = 0;
            Int32 zCodMinimo = 0;


            // 9 números
            // dois primeiros caracteres -  10 a 99
            // terceiro a oitavo caracteres - sequência do ASO
            // nono caracter - dígito verificador


            //Servidor Essence
            // 10 a 19 - Essence

            //Servidor Santo André
            // 20 a 25 - Via
            // 26,27,28 - Santo André
            // 29,30,31 - Daiti
            // 32,33 - Mappas
            // 34,35 - PRO
            // 36,37 - Qteck
            // 38,39 - Grafeno
            // 40,41 - UNO
            // 42,43 - SOMA

            //Servidor SCJ
            // 47 a 49 - SCJ

            //Servidor EY
            // 50 a 55 - EY


            //Servidor Focus
            // 60,61,62,63 - Global
            // 643 - GOI (Likkas)
            // 65 - LIM ( Suavelis )
            // 66,67- Fox
            // 68,69 - Metodo
            // 70,71 - Prime
            // 72,73 - Safety
            // 74,75 - Ergon
            // 76,77 - SDTourin            
            // 78,79 - Seg


            if (Ilitera.Data.SQLServer.EntitySQLServer._LocalServer == "54.94.157.244")
            {
                if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() == "OPSA")
                {
                    zCodMinimo = 260000010;
                    zCodMax = 289999999;
                }
                else if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper().IndexOf("DAITI") > 0)
                {
                    zCodMinimo = 290000010;
                    zCodMax = 319999999;
                }
                else if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper().IndexOf("VIA") > 0)
                {
                    zCodMinimo = 200000010;
                    zCodMax = 259999999;
                }
                else if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper().IndexOf("MAPPAS") > 0)
                {
                    zCodMinimo = 320000010;
                    zCodMax = 339999999;
                }
                else if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper().IndexOf("PRO") > 0)
                {
                    zCodMinimo = 340000010;
                    zCodMax = 359999999;
                }
                else if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper().IndexOf("QTECK") > 0)
                {
                    zCodMinimo = 360000010;
                    zCodMax = 379999999;
                }
                else if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper().IndexOf("SAFETY") > 0)
                {
                    zCodMinimo = 380000010;
                    zCodMax = 399999999;
                }
                else if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper().IndexOf("UNO") > 0)
                {
                    zCodMinimo = 400000010;
                    zCodMax = 419999999;
                }
                else if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper().IndexOf("SOMA") > 0)
                {
                    zCodMinimo = 420000010;
                    zCodMax = 439999999;
                }
            }
            else if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper().IndexOf("PRAJNA_HOM") > 0 || (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper().Trim() == "OPSA" && Ilitera.Data.SQLServer.EntitySQLServer._LocalServer.ToUpper().IndexOf("ESSENCE") > 0))
            {
                zCodMinimo = 190000010;
                zCodMax = 199999999;
            }
            else if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper().IndexOf("PRAJNA") > 0)
            {
                zCodMinimo = 100000010;
                zCodMax = 189999999;
            }
            else if (Ilitera.Data.SQLServer.EntitySQLServer._LocalServer.ToUpper().IndexOf("161.140") > 0)  //JOHNSON
            {
                zCodMinimo = 470000010;
                zCodMax = 499999999;
            }
            else if (Ilitera.Data.SQLServer.EntitySQLServer._LocalServer.ToUpper().IndexOf("ILITERAEY") > 0)
            {
                zCodMinimo = 500000010;
                zCodMax = 559999999;
            }
            else if (Ilitera.Data.SQLServer.EntitySQLServer._LocalServer.ToUpper().IndexOf("FOCUS") > 0)
            {
                if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper().IndexOf("GLOBAL") > 0)
                {
                    zCodMinimo = 600000010;
                    zCodMax = 639999999;
                }
                else if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper().IndexOf("GOI") > 0)
                {
                    zCodMinimo = 640000010;
                    zCodMax = 649999999;
                }
                else if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper().IndexOf("LIM") > 0)
                {
                    zCodMinimo = 650000010;
                    zCodMax = 659999999;
                }
                else if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper().IndexOf("FOX") > 0)
                {
                    zCodMinimo = 660000010;
                    zCodMax = 679999999;
                }
                else if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper().IndexOf("METODO") > 0)
                {
                    zCodMinimo = 680000010;
                    zCodMax = 699999999;
                }
                else if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper().IndexOf("PRIME") > 0)
                {
                    zCodMinimo = 700000010;
                    zCodMax = 719999999;
                }
                else if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper().IndexOf("SAFETY") > 0)
                {
                    zCodMinimo = 720000010;
                    zCodMax = 739999999;
                }
                else if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper().IndexOf("SHO") > 0)
                {
                    zCodMinimo = 740000010;
                    zCodMax = 759999999;
                }
                else if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper().IndexOf("SDTOURIN") > 0)
                {
                    zCodMinimo = 760000010;
                    zCodMax = 779999999;
                }
                else if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() == "OPSA")
                {
                    zCodMinimo = 780000010;
                    zCodMax = 799999999;
                }

            }






            Ilitera.Data.Clientes_Funcionarios xBusca = new Data.Clientes_Funcionarios();
            zCodMax = xBusca.Retornar_CodBusca(zCodMinimo, zCodMax);



            //tamanho inválido
            if (zCodMax.ToString().Trim().Length != 9)
                zCodMax = zCodMinimo;

            if (zCodMax != 0)
            {
                //se não for inteiro válido
                Int32 result;

                bool isValidNumber = Int32.TryParse(zCodMax.ToString(), out result);

                if (isValidNumber == false)
                {
                    zCodMax = 0;
                }
            }


            if (zCodMax != 0)
            {
                //pegar franqueado
                zFranqueado = zCodMax.ToString().Trim().Substring(0, 2);
                zSequencia = System.Convert.ToInt32(zCodMax.ToString().Trim().Substring(2, 6));
            }


            zSequencia = zSequencia + 1;

            zCodMinimo = System.Convert.ToInt32(zFranqueado.ToString() + new string('0', 6 - zSequencia.ToString().Trim().Length) + zSequencia.ToString().Trim());




            //colocar dígito verificador
            int xDigito = 0;
            string xDigito_Verificacao = "0";

            for (int zPos = 0; zPos < 8; zPos++)
            {
                xDigito = xDigito + System.Convert.ToInt16(zCodMinimo.ToString().Trim().Substring(zPos, 1));
            }

            while (xDigito > 9)
            {
                xDigito = System.Convert.ToInt16(xDigito.ToString().Substring(0, 1)) + System.Convert.ToInt16(xDigito.ToString().Substring(1, 1));
            }

            xDigito_Verificacao = xDigito.ToString().Trim();



            //montar código final
            zRetorno = zCodMinimo.ToString().Trim().Substring(0, 8) + xDigito_Verificacao;


            return System.Convert.ToInt32(zRetorno);

        }

    }
}
