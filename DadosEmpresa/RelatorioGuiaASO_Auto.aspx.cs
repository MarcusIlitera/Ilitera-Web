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

//using MestraNET;

namespace Ilitera.Net
{
	/// <summary>
	/// 
	/// </summary>
	public partial class RelatorioGuiaASO_Auto : System.Web.UI.Page
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
            string xID;
            string xId_Exame;

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
                xObs = ""; //Session["Obs_Guia"].ToString().Trim();
                xEnvio_Email = Request["Mail"].ToString().Trim();
                xApt = Request["Apt"].ToString().Trim();
                xID = Request["xId"].ToString().Trim();
                xId_Exame = Request["xIdExame"].ToString().Trim();


                xDtDemissao = "";

                string xUsuario = Session["usuarioLogado"].ToString();

                lbl_Arq.Text = "I:\\temp\\ws_" + Request["xArq"].ToString().Trim() + ".txt";



                Ilitera.Opsa.Data.Empregado empregado = new Ilitera.Opsa.Data.Empregado();


                //criando ASO
                xTipoExame = System.Convert.ToInt16(xTipo);



                Cliente zCliente = new Cliente();
                zCliente.Find(System.Convert.ToInt32(xId_Empresa));


                if (xEnvio_Email == "2")  // 2.via de ASO
                {

                    ExameBase rexame = new ExameBase();
                    rexame.Find(" IDEMPREGADO = " + xId_Empregado + " and IdExameDicionario = " + xTipoExame.ToString() + " and convert( char(10),DataExame,103 ) = '" + xData_Exame + "' ");

                    if (rexame.Id == 0)
                    {
                        //MsgBox1.Show("Ilitera.Net", "Problema na obtenção de dados do ASO", null, new EO.Web.MsgBoxButton("OK"));

                        TextWriter tw = new StreamWriter(lbl_Arq.Text);
                        tw.WriteLine("Problema na obtenção de dados do ASO");
                        tw.Close();

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

                    exame.IdEmpregadoFuncao = EmpregadoFuncao.GetEmpregadoFuncao(empregado);
                    exame.IdPcmso = exame.IdEmpregadoFuncao.nID_EMPR.GetUltimoPcmso();

                    exame.apt_Espaco_Confinado = "";
                    exame.apt_Trabalho_Altura = "";
                    exame.apt_Transporte = "";
                    exame.apt_Submerso = "";
                    exame.apt_Eletricidade = "";
                    exame.apt_Alimento = "";
                    exame.apt_Brigadista = "";
                    exame.apt_Socorrista = "";

                    if (xApt.IndexOf("A") >= 0) exame.apt_Trabalho_Altura = "1";
                    if (xApt.IndexOf("C") >= 0) exame.apt_Espaco_Confinado = "1";
                    if (xApt.IndexOf("T") >= 0) exame.apt_Transporte = "1";
                    if (xApt.IndexOf("S") >= 0) exame.apt_Submerso = "1";
                    if (xApt.IndexOf("E") >= 0) exame.apt_Eletricidade = "1";
                    if (xApt.IndexOf("Q") >= 0) exame.apt_Aquaviario = "1";
                    if (xApt.IndexOf("M") >= 0) exame.apt_Alimento = "1";
                    if (xApt.IndexOf("B") >= 0) exame.apt_Brigadista = "1";
                    if (xApt.IndexOf("R") >= 0) exame.apt_Socorrista = "1";




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
                    xMedico.Find(1111);  // -2133369037);

                    exame.IdMedico = xMedico;

                    //Usuario xusuario = new Usuario();
                    //xusuario.Find(System.Convert.ToInt32(Request["IdUsuario"].ToString()));

                    //exame.UsuarioId = 


                    //Entities.Usuario usuario = (Entities.Usuario)Session["usuarioLogado"];

                    Entities.Usuario usuario = new Entities.Usuario();
                    usuario.IdUsuario = System.Convert.ToInt32(Request["IdUsuario"].ToString());


                    int zStatus = 0;
                    try
                    {
                        zStatus = exame.Save(System.Convert.ToInt32(Request["IdUsuario"].ToString().Trim()));
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


                exame2.apt_Trabalho_Altura2 = "0";
                exame2.apt_Espaco_Confinado2 = "0";
                exame2.apt_Transporte2 = "0";
                exame2.apt_Submerso2 = "0";
                exame2.apt_Eletricidade2 = "0";
                exame2.apt_Aquaviario2 = "0";
                exame2.apt_Alimento2 = "0";
                exame2.apt_Brigadista2 = "0";
                exame2.apt_Socorrista2 = "0";


                if (xApt.IndexOf("A") >= 0) exame2.apt_Trabalho_Altura2 = "1";
                if (xApt.IndexOf("C") >= 0) exame2.apt_Espaco_Confinado2 = "1";
                if (xApt.IndexOf("T") >= 0) exame2.apt_Transporte2 = "1";
                if (xApt.IndexOf("S") >= 0) exame2.apt_Submerso2 = "1";
                if (xApt.IndexOf("E") >= 0) exame2.apt_Eletricidade2 = "1";
                if (xApt.IndexOf("Q") >= 0) exame2.apt_Aquaviario2 = "1";
                if (xApt.IndexOf("M") >= 0) exame2.apt_Alimento2 = "1";
                if (xApt.IndexOf("B") >= 0) exame2.apt_Brigadista2 = "1";
                if (xApt.IndexOf("R") >= 0) exame2.apt_Socorrista2 = "1";






                Int16 zTamanho = 3;

                exame2.IdEmpregado.Find();
                exame2.IdEmpregado.nID_EMPR.Find();
                exame2.IdEmpregado.nID_EMPR.IdGrupoEmpresa.Find();

                if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("PRAJNA") > 0)//exame2.IdEmpregado.nID_EMPR.IdGrupoEmpresa.Descricao.ToUpper().Trim() == "CAPGEMINI"  )  // && Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("_HOM") > 0)  //.Id == -905238295)  // Capgemini
                {
                    zTamanho = 4;
                }


                string xTitulo = "Kit Guia/ASO/PCI - Convocação"; ;

                if ( xId_Exame != "4")  //apenas guia de complementar
                {
                    zTamanho = 1;
                    xTitulo = "Kit Guia Complementar - Convocação";
                }


                CrystalDecisions.CrystalReports.Engine.ReportClass[] reports = new ReportClass[zTamanho];

                //Cliente zCliente = new Cliente();
                //zCliente.Find(System.Convert.ToInt32(xId_Empresa));





                //se for apenas guia de encaminhamento - convocação exames complementares


                if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().IndexOf("Prajna") > 0)
                {
                    //RptGuia_Prajna report0 = new DataSourceGuia_Prajna(cliente, System.Convert.ToInt32(xId_Empregado), System.Convert.ToInt32(xId_Empresa), System.Convert.ToInt32(xId_Clinica), xExames, xExames2, xExames3, xExames4, xData_Exame, xHora_Exame, xTipo, xBasico, xObs, xImpDt).GetReport();
                    RptGuia_Nova_Prajna report0 = new DataSourceGuia_Prajna(cliente, System.Convert.ToInt32(xId_Empregado), System.Convert.ToInt32(xId_Empresa), System.Convert.ToInt32(xId_Clinica), xExames, xExames2, xExames3, xExames4, xData_Exame, xHora_Exame, xTipo, xBasico, xObs, xImpDt, exame2.IdEmpregadoFuncao.Id).GetReport_Nova();
                    //CreatePDFDocument(report, this.Response);
                    reports[0] = report0;
                }
                else
                {
                    //RptGuia report0 = new DataSourceGuia(cliente, System.Convert.ToInt32(xId_Empregado), System.Convert.ToInt32(xId_Empresa), System.Convert.ToInt32(xId_Clinica), xExames, xExames2, xExames3, xExames4, xData_Exame, xHora_Exame, xTipo, xBasico, xObs, xImpDt).GetReport();
                    RptGuia_Nova report0 = new DataSourceGuia(cliente, System.Convert.ToInt32(xId_Empregado), System.Convert.ToInt32(xId_Empresa), System.Convert.ToInt32(xId_Clinica), xExames, xExames2, xExames3, xExames4, xData_Exame, xHora_Exame, xTipo, xBasico, xObs, xImpDt, exame2.IdEmpregadoFuncao.Id).GetReport_Nova();
                    //CreatePDFDocument(report, this.Response);
                    reports[0] = report0;
                }







                if (xId_Exame == "4")
                {



                    RptAso report2 = new DataSourceExameAsoPci(exame2).GetReport();

                    //tenho o ID do ASO para colocar no registro da guia gerada ?
                    //dar um update ?




                    //Funcionalidade funcionalidade = Funcionalidade.GetFuncionalidade(FuncionalidadeTipo.Relatorio, report.GetType());
                    //ImpressaoDocUsuario.InsereRegistro(usuario, cliente, Convert.ToInt64(report.SummaryInfo.KeywordsInReport), true, funcionalidade);

                    //CreatePDFDocument(report, this.Response);


                    //Ilitera.Opsa.Data.Empregado empregado = new Ilitera.Opsa.Data.Empregado();

                    //Clinico exame2 = new Clinico(Convert.ToInt32(Request["IdExame"]));

                    reports[1] = report2;

                    Juridica xClin = new Juridica();
                    xClin.Find(Convert.ToInt32(Request["IdClinica"].ToString()));

                    string xClinNome = "";

                    if (xClin != null) xClinNome = xClin.NomeAbreviado.ToUpper().Trim();


                    if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().IndexOf("Daiti") > 0 || Ilitera.Data.SQLServer.EntitySQLServer.xDB2.Trim() == "Sied_Novo" || xClinNome.IndexOf("DAITI") >= 0 || xClinNome.IndexOf("IPATINGA") >= 0)
                    {
                        RptPci report3 = new DataSourceExameAsoPci(exame2).GetReportPci_Antigo();
                        reports[2] = report3;
                    }
                    else
                    {

                        exame2.IdEmpregado.Find();
                        exame2.IdEmpregado.nID_EMPR.Find();
                        exame2.IdEmpregado.nID_EMPR.IdGrupoEmpresa.Find();

                        //if (exame.IdEmpregado.nID_EMPR.IdGrupoEmpresa.Descricao.ToUpper().Trim() == "CAPGEMINI" && Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("_HOM") > 0)  //.Id == -905238295)  // Capgemini

                        if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("PRAJNA") > 0) //  && exame2.IdEmpregado.nID_EMPR.IdGrupoEmpresa.Descricao.ToUpper().Trim() == "CAPGEMINI")  // && Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("_HOM") > 0)  //.Id == -905238295)  // Capgemini
                        {
                            RptPci_Novo_Capgemini report3 = new DataSourceExameAsoPci(exame2).GetReportPciCapgemini();
                            reports[2] = report3;
                        }
                        else
                        {
                            RptPci_Novo report3 = new DataSourceExameAsoPci(exame2).GetReportPci();
                            reports[2] = report3;
                        }
                    }
                    //RptPci report3 = new DataSourceExameAsoPci(exame2).GetReportPci();


                    exame2.IdEmpregado.Find();
                    exame2.IdEmpregado.nID_EMPR.Find();
                    exame2.IdEmpregado.nID_EMPR.IdGrupoEmpresa.Find();


                    if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("PRAJNA") > 0) //exame2.IdEmpregado.nID_EMPR.IdGrupoEmpresa.Descricao.ToUpper().Trim() == "CAPGEMINI"  ) // && Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("_HOM") > 0)  //.Id == -905238295)  // Capgemini
                    {
                        Int32 xIdExame = exame2.Id;

                        Carregar_Dados_Anamnese_Exame(exame2.Id);

                        RptAnamnese report4 = new DataSourceExameAnamnese(xIdExame, true).GetReport();
                        reports[3] = report4;

                    }



                }


                //CreatePDFMerged(reports, this.Response, "", false, xID);

                HttpResponse response = this.Response;
                string watermark = "";
                bool RenumerarPaginas = false;
                string xId = xID;


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


                    //CreatePDFMerged(streams, response, watermark, RenumerarPaginas, xId);

         

           

                MemoryStream reportStream = PdfSharpUtility.MergeReports(streams, string.Empty, watermark, RenumerarPaginas);



                string xPath = "I:\\temp\\guia_" + xId.Trim() + ".pdf";

                //if (xEnvio_Email == "S")
                //{

                //if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().IndexOf("Prajna") > 0)
                //{




                FileStream fileStream = File.Create(xPath, (int)reportStream.Length);
                // Initialize the bytes array with the stream length and then fill it with data
                byte[] bytesInStream = new byte[reportStream.Length];
                reportStream.Read(bytesInStream, 0, bytesInStream.Length);
                // Use write method to write to the file specified above
                fileStream.Write(bytesInStream, 0, bytesInStream.Length);

                fileStream.Flush();

                fileStream.Dispose();
                fileStream = null;

                Clinica xClinica = new Clinica(exame.IdJuridica.Id);

                Cliente xCliente = new Cliente(exame.IdEmpregado.nID_EMPR.Id);
                string xEmpresa = "";

                if (xCliente.IdJuridicaPai.Id != 0 && xCliente.IdJuridicaPai.Id != xCliente.Id)
                {
                    if (xCliente.IdJuridicaPai.ToString().ToUpper().IndexOf("KNOX") > 0)
                    {
                        xEmpresa = xCliente.IdJuridicaPai.ToString();
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



                string xCorpo = "<p style='font-family:Tahoma;font-size:18px;text-align:center;text-decoration:underline;color:black> </p>" +
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
                                  "Data do Exame: " + exame.DataExame.ToString("dd/MM/yyyy") + " </font></p></body>";


                //string xCorpo = "<p style='font-family:Tahoma;font-size:18px;text-align:center;text-decoration:underline;color:black> </p>" +
                //                 "<p style='font-family:Tahoma;font-size:18px;text-align:center;text-decoration:underline;color:black> DADOS DO AGENDAMENTO: </p><br>" +
                //                 "<p><font size='3' face='Tahoma'>Nome: " + exame.IdEmpregado.tNO_EMPG + "<br>" +                             
                //                  "Data do Exame: " + exame.DataExame.ToString("dd/MM/yyyy") + " </font></p></body>";



                if (xId.IndexOf("KIT") >= 0)
                {
                    //procurar tblConvocacao e enviar e-mail com kit para colaborador

                    //attach arquivo - por exemplo i:\\temp\\guia_KIT_962020797.pdf   ( guia_ + ID + .pdf )



                    Convocacao rConvocacao = new Convocacao();
                    rConvocacao.Find(System.Convert.ToInt32(xId.Substring(4)));




                    string zEmail = "";

                    if (rConvocacao.eMail_Envio.IndexOf("|") > 0)
                        zEmail = rConvocacao.eMail_Envio.Substring(0, rConvocacao.eMail_Envio.IndexOf("|") - 1).Trim();
                    else
                        zEmail = rConvocacao.eMail_Envio;



                    rConvocacao.hDt_Convocacao = exame.DataExame;
                    rConvocacao.Save();



                    //testar com envio ilitera
                    //Envio_Email_Prajna(  zEmail  , "agendamento@5aessence.com.br", "Kit Guia/ASO/PCI", xCorpo, xPath, "Guia / ASO / PCI", exame.IdEmpregado.Id, exame.IdEmpregado.nID_EMPR.Id);



                    //if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().IndexOf("Prajna") > 0)
                    //{


                    Envio_Email_Ilitera(zEmail, "agendamento@5aessence.com.br", xTitulo, xCorpo, xPath, "Kit Guia / ASO / PCI", exame.IdEmpregado.Id, exame.IdEmpregado.nID_EMPR.Id);
                    //}
                    //else if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().Trim().ToUpper().IndexOf("SAFETY") > 0)
                    //{
                    //    Envio_Email_Ilitera(zEmail, "agendamento@5aessence.com.br", "Kit Guia/ASO/PCI", xCorpo, xPath, "Kit Guia / ASO / PCI", exame.IdEmpregado.Id, exame.IdEmpregado.nID_EMPR.Id);
                    //}
                    //else if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().Trim().ToUpper() == "OPSA")
                    //{
                    //    Envio_Email_Ilitera(zEmail, "agendamento@5aessence.com.br", "Kit Guia/ASO/PCI", xCorpo, xPath, "Kit Guia / ASO / PCI", exame.IdEmpregado.Id, exame.IdEmpregado.nID_EMPR.Id);
                    //}
                    //else if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().IndexOf("Daiti") > 0)
                    //{
                    //    Envio_Email_Ilitera(zEmail, "agendamento@5aessence.com.br", "Kit Guia/ASO/PCI", xCorpo, xPath, "Kit Guia / ASO / PCI", exame.IdEmpregado.Id, exame.IdEmpregado.nID_EMPR.Id);
                    //}
                    //else if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("GLOBAL") > 0)
                    //{
                    //    Envio_Email_Ilitera(zEmail, "agendamento@5aessence.com.br", "Kit Guia/ASO/PCI", xCorpo, xPath, "Kit Guia / ASO / PCI", exame.IdEmpregado.Id, exame.IdEmpregado.nID_EMPR.Id);
                    //}
                    //else if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("COTIA") > 0)
                    //{
                    //    Envio_Email_Ilitera(zEmail, "agendamento@5aessence.com.br", "Kit Guia/ASO/PCI", xCorpo, xPath, "Kit Guia / ASO / PCI", exame.IdEmpregado.Id, exame.IdEmpregado.nID_EMPR.Id);
                    //}
                    //else if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("COTIA") > 0)
                    //{
                    //    Envio_Email_Ilitera(zEmail, "agendamento@5aessence.com.br", "Kit Guia/ASO/PCI", xCorpo, xPath, "Kit Guia / ASO / PCI", exame.IdEmpregado.Id, exame.IdEmpregado.nID_EMPR.Id);
                    //}
                    //else
                    //{
                    //    Envio_Email_Ilitera(zEmail, "agendamento@5aessence.com.br", "Kit Guia/ASO/PCI", xCorpo, xPath, "Kit Guia / ASO / PCI", exame.IdEmpregado.Id, exame.IdEmpregado.nID_EMPR.Id);
                    //}

                }













            //Funcionalidade funcionalidade = Funcionalidade.GetFuncionalidade(FuncionalidadeTipo.Relatorio, report.GetType());
            //ImpressaoDocUsuario.InsereRegistro(usuario, cliente, Convert.ToInt64(report.SummaryInfo.KeywordsInReport), true, funcionalidade);


            //}		
            //else
            //    throw new Exception("Problema na geração da guia !");



        }
            catch (Exception ex)
            {
                //MsgBox1.Show("Ilitera.Net", ex.Message, null,
                //new EO.Web.MsgBoxButton("OK"));      
                //Session["zErro"] = "Erro ao gerar arquivos da Guia e ASO: " + ex.Message;


                TextWriter tw = new StreamWriter(lbl_Arq.Text);
                tw.WriteLine("Erro ao gerar arquivos da Guia e ASO: " + ex.Message);
                tw.Close();

                //Response.Redirect("~/Comunicacao2.aspx");
                //Response.Redirect("~/Comunicacao.aspx?Status='Erro ao gerar arquivos da Guia e ASO: " + ex.Message + "' ");
                return;

            }
            finally
            {

                
               // Session["zErro"] = "Fim Processamento";

                //Response.Redirect("~/Comunicacao2.aspx");
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

            //enviar e-mail para clinica e prajna

            MailMessage objEmail = new MailMessage();

            //rementente do email           
            objEmail.From = new MailAddress("agendamento@5aessencenet.com.br");

            
            //para
            string xEmail = "";

            xEmail = exame.IdJuridica.Email.ToString().Trim();

            if (xEmail == "")
            {
                throw new Exception("Clínica não possui e-mail cadastrado.");                
            }

            if (xEmail.IndexOf(";") > 0) xEmail = xEmail.Substring(0, xEmail.IndexOf(";"));

            objEmail.To.Add(xEmail);
            xDestinatario = xEmail + "; agendamento@5aessence.com.br ;";
                        
            objEmail.CC.Add("agendamento@5aessence.com.br");


            //cópia para usuário logado
            //Entities.Usuario usuario = (Entities.Usuario)Session["usuarioLogado"];
            Entities.Usuario usuario = new Entities.Usuario();
            usuario.IdUsuario = System.Convert.ToInt32(Request["IdUsuario"].ToString());


            Prestador xPrestador = new Prestador(usuario.IdPrestador);

            
            if (xPrestador.Email != null)
            {
                xEmail = xPrestador.Email.ToString().Trim();
                if (xEmail.IndexOf(";") > 0) xEmail = xEmail.Substring(0, xEmail.IndexOf(";"));

                if (xEmail.Trim() != "")
                {
                    objEmail.Bcc.Add(xEmail);
                    xDestinatario = xDestinatario + xEmail + ";";
                }
            }

            objEmail.Priority = MailPriority.Normal;
            objEmail.IsBodyHtml = true;

            objEmail.Subject = xSubject;
            objEmail.Body = xBody;
            objEmail.SubjectEncoding = Encoding.GetEncoding("ISO-8859-1");
            objEmail.BodyEncoding = Encoding.GetEncoding("ISO-8859-1");

            Attachment xItem = new Attachment(xAttach);
            objEmail.Attachments.Add(xItem);

            SmtpClient objSmtp = new SmtpClient();
            objSmtp.Host = "mail.exchange.locaweb.com.br";
            objSmtp.Port = 587;
            objSmtp.Credentials = new NetworkCredential("agendamento@essencenet.com.br", "Vug31145");
            //objSmtp.EnableSsl = true;

            ////objSmtp.Host = "outlook.office.com";
            //objSmtp.Host = "smtp.office365.com";
            //objSmtp.Port = 587;
            //objSmtp.Credentials = new NetworkCredential("agendamento@5aessence.com.br", "Agend_5060");

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
            //xEnvio.Envio_eMail(xIdEmpregado, xIdEmpresa, xDestinatario, "Kit Guia / ASO / PCI");

            return;

        }




        protected void Envio_Email_Ilitera(string xPara, string xCopia, string xSubject, string xBody, string xAttach, string xModulo, Int32 xIdEmpregado, Int32 xIdEmpresa)
        {
            string xDestinatario = "";
            //enviar e-mail para clinica e prajna

            MailMessage objEmail = new MailMessage();

            //rementente do email
            objEmail.From = new MailAddress("agendamento1@ilitera.com.br");
            

            //caixa-postal de onde será enviado o e-mail
            //objEmail.ReplyTo = new MailAddress("email@seusite.com.br");

            //para
            //objEmail.To.Add("lasdowsky@gmail.com");
            //string xEmail = "";

            //xEmail = exame.IdJuridica.Email.ToString().Trim();

            //if (xEmail == "")
            //{
            //    throw new Exception("Clínica não possui e-mail cadastrado.");
            //}

            //if (xEmail.IndexOf(";") > 0) xEmail = xEmail.Substring(0, xEmail.IndexOf(";"));

            //objEmail.To.Add(xEmail);

            objEmail.To.Add(xPara);
            //xDestinatario = xEmail + "; atendimento@ilitera.com.br ;";

            //objEmail.Bcc.Add("oculto@provedor.com.br");

            //objEmail.CC.Add("atendimento@ilitera.com.br");
            //objEmail.CC.Add("atendimento2@ilitera.com.br");

            objEmail.Priority = MailPriority.Normal;
            objEmail.IsBodyHtml = true;

            objEmail.Subject = xSubject;
            objEmail.Body = xBody;
            objEmail.SubjectEncoding = Encoding.GetEncoding("ISO-8859-1");
            objEmail.BodyEncoding = Encoding.GetEncoding("ISO-8859-1");

            Attachment xItem = new Attachment(xAttach);
            objEmail.Attachments.Add(xItem);

            SmtpClient objSmtp = new SmtpClient();
            objSmtp.Host = "smtp.ilitera.com.br";            
            objSmtp.Port = 587;


            objSmtp.Credentials = new NetworkCredential("agendamento1@ilitera.com.br", "Ilitera_3624");


            Ilitera.Data.Clientes_Clinicas xEnvio = new Ilitera.Data.Clientes_Clinicas();
            //checar se não é e-mail duplicado

            DataSet rDs = xEnvio.Checar_Envio_Email(xIdEmpregado, xIdEmpresa, xDestinatario, "Kit Guia / ASO / PCI");

            if (rDs.Tables[0].Rows.Count == 0)
            {
                objSmtp.Send(objEmail);
                xEnvio.Envio_eMail(xIdEmpregado, xIdEmpresa, xDestinatario, "Kit Guia / ASO / PCI");
            }
            
                                 
            return;

        }



        protected void Envio_Email_Global(string xPara, string xCopia, string xSubject, string xBody, string xAttach, string xModulo, Int32 xIdEmpregado, Int32 xIdEmpresa)
        {
            string xDestinatario = "";
            //enviar e-mail para clinica e prajna

            MailMessage objEmail = new MailMessage();

            //rementente do email
            objEmail.From = new MailAddress("agendamento.sp.sto@ilitera.com.br");


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
            xDestinatario = xEmail + "; atendimento@ilitera.com.br ; ";

            //objEmail.Bcc.Add("oculto@provedor.com.br");

            objEmail.CC.Add("atendimento@ilitera.com.br");
            objEmail.CC.Add("guias@globalsegmed.com.br");
            objEmail.CC.Add("asos@globalsegmed.com.br");

            objEmail.Priority = MailPriority.Normal;
            objEmail.IsBodyHtml = true;

            objEmail.Subject = xSubject;
            objEmail.Body = xBody;
            objEmail.SubjectEncoding = Encoding.GetEncoding("ISO-8859-1");
            objEmail.BodyEncoding = Encoding.GetEncoding("ISO-8859-1");

            Attachment xItem = new Attachment(xAttach);
            objEmail.Attachments.Add(xItem);

            SmtpClient objSmtp = new SmtpClient();
            objSmtp.Host = "smtp.globalsegmed.com.br";
            objSmtp.Port = 587;

            objEmail.From = new MailAddress("envio@globalsegmed.com.br");
            objSmtp.Credentials = new NetworkCredential("envio@globalsegmed.com.br", "Globalsegmed2024#");


            objEmail.ReplyTo = new MailAddress("envio@globalsegmed.com.br");


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



        protected void Envio_Email_Qteck(string xPara, string xCopia, string xSubject, string xBody, string xAttach, string xModulo, Int32 xIdEmpregado, Int32 xIdEmpresa)
        {
            string xDestinatario = "";
            //enviar e-mail para clinica e prajna

            MailMessage objEmail = new MailMessage();

            //rementente do email
            objEmail.From = new MailAddress("agendamento.sp.sto@ilitera.com.br");


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
            xDestinatario = xEmail + "; atendimento@ilitera.com.br ; ";

            //objEmail.Bcc.Add("oculto@provedor.com.br");

            //objEmail.CC.Add("atendimento@ilitera.com.br");
            objEmail.CC.Add("seguranca3@qteck.com.br");

            objEmail.Priority = MailPriority.Normal;
            objEmail.IsBodyHtml = true;

            objEmail.Subject = xSubject;
            objEmail.Body = xBody;
            objEmail.SubjectEncoding = Encoding.GetEncoding("ISO-8859-1");
            objEmail.BodyEncoding = Encoding.GetEncoding("ISO-8859-1");

            Attachment xItem = new Attachment(xAttach);
            objEmail.Attachments.Add(xItem);

            SmtpClient objSmtp = new SmtpClient();
            objSmtp.Host = "smtp.ilitera.com.br";
            objSmtp.Port = 587;
            objSmtp.Credentials = new NetworkCredential("agendamento.sp.sto@ilitera.com.br", "Ilitera_3624");


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




        protected void Envio_Email_Cotia(string xPara, string xCopia, string xSubject, string xBody, string xAttach, string xModulo, Int32 xIdEmpregado, Int32 xIdEmpresa)
        {
            string xDestinatario = "";
            //enviar e-mail para clinica e prajna

            MailMessage objEmail = new MailMessage();

            //rementente do email
            objEmail.From = new MailAddress("agendamento.sp.sto@ilitera.com.br");


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
            objEmail.CC.Add("keile.sp.cot@ilitera.com.br");
            objEmail.CC.Add("medicina.sp.cot@ilitera.com.br");

            objEmail.Priority = MailPriority.Normal;
            objEmail.IsBodyHtml = true;

            objEmail.Subject = xSubject;
            objEmail.Body = xBody;
            objEmail.SubjectEncoding = Encoding.GetEncoding("ISO-8859-1");
            objEmail.BodyEncoding = Encoding.GetEncoding("ISO-8859-1");

            Attachment xItem = new Attachment(xAttach);
            objEmail.Attachments.Add(xItem);

            SmtpClient objSmtp = new SmtpClient();
            objSmtp.Host = "smtp.ilitera.com.br";
            objSmtp.Port = 587;
            objSmtp.Credentials = new NetworkCredential("agendamento.sp.sto@ilitera.com.br", "Ilitera_3624");


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
            objEmail.From = new MailAddress("agendamento1@ilitera.com.br");


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
            objSmtp.Host = "smtp.ilitera.com.br";
            objSmtp.Port = 587;
            objSmtp.Credentials = new NetworkCredential("agendamento1@ilitera.com.br", "Ilitera_3624");


            Ilitera.Data.Clientes_Clinicas xEnvio = new Ilitera.Data.Clientes_Clinicas();
            //checar se não é e-mail duplicado

            DataSet rDs = xEnvio.Checar_Envio_Email(xIdEmpregado, xIdEmpresa, xDestinatario, "Guia / ASO / PCI");

            if (rDs.Tables[0].Rows.Count == 0)
            {
              //  objSmtp.Send(objEmail);
              //  xEnvio.Envio_eMail(xIdEmpregado, xIdEmpresa, xDestinatario, "Guia / ASO / PCI");
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
            //    objEmail.CC.Add("agendamento.sp.dai@ilitera.com.br");
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
            objSmtp.Host = "smtp.ilitera.com.br";
            objSmtp.Port = 587;
            //objSmtp.Credentials = new NetworkCredential("agendamento.sp.sto@ilitera.com.br", "bibi6096");
            objSmtp.Credentials = new NetworkCredential("agendamento.sp.dai@ilitera.com.br", "Daiiti_7654");

            objSmtp.Send(objEmail);


            Ilitera.Data.Clientes_Clinicas xEnvio = new Ilitera.Data.Clientes_Clinicas();
            xEnvio.Envio_eMail(xIdEmpregado, xIdEmpresa, xDestinatario, "Guia / ASO / PCI");

            return;

        }


        protected void CreatePDFMerged(CrystalDecisions.CrystalReports.Engine.ReportClass[] reports, HttpResponse response, string watermark, bool RenumerarPaginas, string xId)
        {


            try
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


            CreatePDFMerged(streams, response, watermark, RenumerarPaginas, xId);

            }
            catch (Exception ex)
            {
                //MsgBox1.Show("Ilitera.Net", ex.Message, null,
                //new EO.Web.MsgBoxButton("OK"));      
                //Session["zErro"] = "Erro ao gerar agendamento(1): " + ex.Message;

                //Response.Redirect("~/Comunicacao2.aspx");
                //Response.Redirect("~/Comunicacao.aspx?Status='Erro ao gerar arquivos da Guia e ASO: " + ex.Message + "' ");

                TextWriter tw = new StreamWriter(lbl_Arq.Text);
                tw.WriteLine("Erro ao gerar agendamento(1): " + ex.Message);
                tw.Close();

                return;

            }

        }




        protected void CreatePDFMerged(Stream[] streams, HttpResponse response, string watermark, bool RenumerarPaginas, string xId)
        {


            try
            { 

            MemoryStream reportStream = PdfSharpUtility.MergeReports(streams, string.Empty, watermark, RenumerarPaginas);



            string xPath = "I:\\temp\\guia_" + xId.Trim() + ".pdf";

            //if (xEnvio_Email == "S")
            //{

            //if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().IndexOf("Prajna") > 0)
            //{




            FileStream fileStream = File.Create(xPath, (int)reportStream.Length);
            // Initialize the bytes array with the stream length and then fill it with data
            byte[] bytesInStream = new byte[reportStream.Length];
            reportStream.Read(bytesInStream, 0, bytesInStream.Length);
            // Use write method to write to the file specified above
            fileStream.Write(bytesInStream, 0, bytesInStream.Length);

            fileStream.Flush();

            fileStream.Dispose();
            fileStream = null;

            Clinica xClinica = new Clinica(exame.IdJuridica.Id);

            Cliente xCliente = new Cliente(exame.IdEmpregado.nID_EMPR.Id);
            string xEmpresa = "";

            if (xCliente.IdJuridicaPai.Id != 0 && xCliente.IdJuridicaPai.Id != xCliente.Id)
            {
                if (xCliente.IdJuridicaPai.ToString().ToUpper().IndexOf("KNOX") > 0)
                {
                    xEmpresa = xCliente.IdJuridicaPai.ToString();
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



            string xCorpo = "<p style='font-family:Tahoma;font-size:18px;text-align:center;text-decoration:underline;color:black> </p>" +
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
                              "Data do Exame: " + exame.DataExame.ToString("dd/MM/yyyy") + " </font></p></body>";


            //string xCorpo = "<p style='font-family:Tahoma;font-size:18px;text-align:center;text-decoration:underline;color:black> </p>" +
            //                 "<p style='font-family:Tahoma;font-size:18px;text-align:center;text-decoration:underline;color:black> DADOS DO AGENDAMENTO: </p><br>" +
            //                 "<p><font size='3' face='Tahoma'>Nome: " + exame.IdEmpregado.tNO_EMPG + "<br>" +                             
            //                  "Data do Exame: " + exame.DataExame.ToString("dd/MM/yyyy") + " </font></p></body>";



            if (xId.IndexOf("KIT") >= 0)
            {
                //procurar tblConvocacao e enviar e-mail com kit para colaborador

                //attach arquivo - por exemplo i:\\temp\\guia_KIT_962020797.pdf   ( guia_ + ID + .pdf )



                Convocacao rConvocacao = new Convocacao();
                rConvocacao.Find(System.Convert.ToInt32(xId.Substring(4)));




                string zEmail = "";

                if (rConvocacao.eMail_Envio.IndexOf("|") > 0)
                    zEmail = rConvocacao.eMail_Envio.Substring(0, rConvocacao.eMail_Envio.IndexOf("|") - 1).Trim();
                else
                    zEmail = rConvocacao.eMail_Envio;



                rConvocacao.hDt_Convocacao = exame.DataExame;
                rConvocacao.Save();



                //testar com envio ilitera
                //Envio_Email_Prajna(  zEmail  , "agendamento@5aessence.com.br", "Kit Guia/ASO/PCI", xCorpo, xPath, "Guia / ASO / PCI", exame.IdEmpregado.Id, exame.IdEmpregado.nID_EMPR.Id);



                //if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().IndexOf("Prajna") > 0)
                //{


                    Envio_Email_Ilitera(zEmail, "agendamento@5aessence.com.br", "Kit Guia/ASO/PCI", xCorpo, xPath, "Kit Guia / ASO / PCI", exame.IdEmpregado.Id, exame.IdEmpregado.nID_EMPR.Id);
                //}
                //else if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().Trim().ToUpper().IndexOf("SAFETY") > 0)
                //{
                //    Envio_Email_Ilitera(zEmail, "agendamento@5aessence.com.br", "Kit Guia/ASO/PCI", xCorpo, xPath, "Kit Guia / ASO / PCI", exame.IdEmpregado.Id, exame.IdEmpregado.nID_EMPR.Id);
                //}
                //else if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().Trim().ToUpper() == "OPSA")
                //{
                //    Envio_Email_Ilitera(zEmail, "agendamento@5aessence.com.br", "Kit Guia/ASO/PCI", xCorpo, xPath, "Kit Guia / ASO / PCI", exame.IdEmpregado.Id, exame.IdEmpregado.nID_EMPR.Id);
                //}
                //else if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().IndexOf("Daiti") > 0)
                //{
                //    Envio_Email_Ilitera(zEmail, "agendamento@5aessence.com.br", "Kit Guia/ASO/PCI", xCorpo, xPath, "Kit Guia / ASO / PCI", exame.IdEmpregado.Id, exame.IdEmpregado.nID_EMPR.Id);
                //}
                //else if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("GLOBAL") > 0)
                //{
                //    Envio_Email_Ilitera(zEmail, "agendamento@5aessence.com.br", "Kit Guia/ASO/PCI", xCorpo, xPath, "Kit Guia / ASO / PCI", exame.IdEmpregado.Id, exame.IdEmpregado.nID_EMPR.Id);
                //}
                //else if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("COTIA") > 0)
                //{
                //    Envio_Email_Ilitera(zEmail, "agendamento@5aessence.com.br", "Kit Guia/ASO/PCI", xCorpo, xPath, "Kit Guia / ASO / PCI", exame.IdEmpregado.Id, exame.IdEmpregado.nID_EMPR.Id);
                //}
                //else if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("COTIA") > 0)
                //{
                //    Envio_Email_Ilitera(zEmail, "agendamento@5aessence.com.br", "Kit Guia/ASO/PCI", xCorpo, xPath, "Kit Guia / ASO / PCI", exame.IdEmpregado.Id, exame.IdEmpregado.nID_EMPR.Id);
                //}
                //else
                //{
                //    Envio_Email_Ilitera(zEmail, "agendamento@5aessence.com.br", "Kit Guia/ASO/PCI", xCorpo, xPath, "Kit Guia / ASO / PCI", exame.IdEmpregado.Id, exame.IdEmpregado.nID_EMPR.Id);
                //}

            }


            }
            catch (Exception ex)
            {
                //MsgBox1.Show("Ilitera.Net", ex.Message, null,
                //new EO.Web.MsgBoxButton("OK"));      
                //Session["zErro"] = "Erro ao gerar agendamento(2): " + ex.Message;

                //Response.Redirect("~/Comunicacao2.aspx");
                //Response.Redirect("~/Comunicacao.aspx?Status='Erro ao gerar arquivos da Guia e ASO: " + ex.Message + "' ");

                TextWriter tw = new StreamWriter(lbl_Arq.Text);
                tw.WriteLine("Erro ao gerar agendamento(2): " + ex.Message);
                tw.Close();

                return;

            }


            //}
            //    else if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().Trim().ToUpper().IndexOf("SAFETY") > 0 )
            //    {


            //        FileStream fileStream = File.Create(xPath, (int)reportStream.Length);
            //        // Initialize the bytes array with the stream length and then fill it with data
            //        byte[] bytesInStream = new byte[reportStream.Length];
            //        reportStream.Read(bytesInStream, 0, bytesInStream.Length);
            //        // Use write method to write to the file specified above
            //        fileStream.Write(bytesInStream, 0, bytesInStream.Length);

            //        fileStream.Flush();

            //        fileStream.Dispose();
            //        fileStream = null;

            //        //Clinica xClinica = new Clinica(exame.IdJuridica.Id);

            //        //Cliente xCliente = new Cliente(exame.IdEmpregado.nID_EMPR.Id);
            //        //string xEmpresa = "";

            //        //if (xCliente.IdJuridicaPai.Id != 0 && xCliente.IdJuridicaPai.Id != xCliente.Id)
            //        //{
            //        //    xEmpresa = xCliente.IdJuridicaPai.ToString() + "   Unidade: " + xCliente.NomeAbreviado.ToString();
            //        //}
            //        //else
            //        //{
            //        //    xEmpresa = xCliente.GetNomeEmpresa();
            //        //}



            //        //string xCorpo = "<body><p><font size='2' face='Tahoma'><H1 align='center'>Guia de Encaminhamento</H1></font></p> <br></br>" +
            //        //                "<p><font size='3' face='Tahoma'>Nome: " + exame.IdEmpregado.tNO_EMPG + "<br>" +
            //        //                "RG: " + exame.IdEmpregado.tNO_IDENTIDADE.ToString() + "<br>" +
            //        //                "Data de Nascimento: " + exame.IdEmpregado.hDT_NASC.ToString("dd/MM/yyyy") + "<br><br>" +
            //        //                "Empresa:  " + xEmpresa + "<br>" +
            //        //                "CNPJ: " + exame.IdEmpregado.nID_EMPR.GetCnpj() + "<br>" +
            //        //                "Cargo: " + exame.IdEmpregadoFuncao.nID_FUNCAO.ToString() + "<br>" +
            //        //                 "Setor: " + exame.IdEmpregadoFuncao.nID_SETOR.ToString() + "<br>" +
            //        //                 "Unidade de Trabalho: " + exame.IdEmpregadoFuncao.GetLocalDeTrabalho(exame.IdEmpregado.nID_EMPR) + "<br>" +
            //        //                 "Clinica: " + exame.IdJuridica.NomeAbreviado + "<br>" +
            //        //                 "Endereço da clinica: " + xClinica.GetEndereco().IdTipoLogradouro.NomeCompleto + " " + xClinica.GetEndereco().Logradouro + " " + xClinica.GetEndereco().Numero + " " + xClinica.GetEndereco().Bairro + " " + xClinica.GetEndereco().Municipio + "/" + xClinica.GetEndereco().Uf + "   Fone: " + exame.IdJuridica.GetContatoTelefonico() + "<br>" +
            //        //                 "Tipo de Exame: " + exame.IdExameDicionario.Nome + " <br>" +
            //        //                 "Data do Exame: " + exame.DataExame.ToString("dd/MM/yyyy") + " </font></p></body>";



            //        //Envio_Email_Grafeno(exame.IdJuridica.Email.ToString(), "agendamento@5aessence.com.br", "Guia de Encaminhamento", xCorpo, xPath, "Guia / ASO / PCI", exame.IdEmpregado.Id, exame.IdEmpregado.nID_EMPR.Id);

            //    }
            //    else if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().Trim().ToUpper() == "OPSA")
            //    {


            //        FileStream fileStream = File.Create(xPath, (int)reportStream.Length);
            //        // Initialize the bytes array with the stream length and then fill it with data
            //        byte[] bytesInStream = new byte[reportStream.Length];
            //        reportStream.Read(bytesInStream, 0, bytesInStream.Length);
            //        // Use write method to write to the file specified above
            //        fileStream.Write(bytesInStream, 0, bytesInStream.Length);

            //        fileStream.Flush();

            //        fileStream.Dispose();
            //        fileStream = null;

            //        //Clinica xClinica = new Clinica(exame.IdJuridica.Id);

            //        //Cliente xCliente = new Cliente(exame.IdEmpregado.nID_EMPR.Id);
            //        //string xEmpresa = "";

            //        //if (xCliente.IdJuridicaPai.Id != 0 && xCliente.IdJuridicaPai.Id != xCliente.Id)
            //        //{
            //        //    xEmpresa = xCliente.IdJuridicaPai.ToString() + "   Unidade: " + xCliente.NomeAbreviado.ToString();
            //        //}
            //        //else
            //        //{
            //        //    xEmpresa = xCliente.GetNomeEmpresa();
            //        //}



            //        //string xCorpo = "<body><p><font size='2' face='Tahoma'><H1 align='center'>Guia de Encaminhamento</H1></font></p> <br></br>" +
            //        //                "<p><font size='3' face='Tahoma'>Nome: " + exame.IdEmpregado.tNO_EMPG + "<br>" +
            //        //                "RG: " + exame.IdEmpregado.tNO_IDENTIDADE.ToString() + "<br>" +
            //        //                "Data de Nascimento: " + exame.IdEmpregado.hDT_NASC.ToString("dd/MM/yyyy") + "<br><br>" +
            //        //                "Empresa:  " + xEmpresa + "<br>" +
            //        //                "CNPJ: " + exame.IdEmpregado.nID_EMPR.GetCnpj() + "<br>" +
            //        //                "Cargo: " + exame.IdEmpregadoFuncao.nID_FUNCAO.ToString() + "<br>" +
            //        //                 "Setor: " + exame.IdEmpregadoFuncao.nID_SETOR.ToString() + "<br>" +
            //        //                 "Unidade de Trabalho: " + exame.IdEmpregadoFuncao.GetLocalDeTrabalho(exame.IdEmpregado.nID_EMPR) + "<br>" +
            //        //                 "Clinica: " + exame.IdJuridica.NomeAbreviado + "<br>" +
            //        //                 "Endereço da clinica: " + xClinica.GetEndereco().IdTipoLogradouro.NomeCompleto + " " + xClinica.GetEndereco().Logradouro + " " + xClinica.GetEndereco().Numero + " " + xClinica.GetEndereco().Bairro + " " + xClinica.GetEndereco().Municipio + "/" + xClinica.GetEndereco().Uf + "   Fone: " + exame.IdJuridica.GetContatoTelefonico() + "<br>" +
            //        //                 "Tipo de Exame: " + exame.IdExameDicionario.Nome + " <br>" +
            //        //                 "Data do Exame: " + exame.DataExame.ToString("dd/MM/yyyy") + " </font></p></body>";


            //       // Envio_Email_Ilitera(exame.IdJuridica.Email.ToString(), "agendamento@5aessence.com.br", "Guia de Encaminhamento", xCorpo, xPath, "Guia / ASO / PCI", exame.IdEmpregado.Id, exame.IdEmpregado.nID_EMPR.Id);

            //    }
            //    else if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().IndexOf("Daiti") > 0)
            //    {


            //        FileStream fileStream = File.Create(xPath, (int)reportStream.Length);
            //        // Initialize the bytes array with the stream length and then fill it with data
            //        byte[] bytesInStream = new byte[reportStream.Length];
            //        reportStream.Read(bytesInStream, 0, bytesInStream.Length);
            //        // Use write method to write to the file specified above
            //        fileStream.Write(bytesInStream, 0, bytesInStream.Length);

            //        fileStream.Flush();

            //        fileStream.Dispose();
            //        fileStream = null;

            //        //Clinica xClinica = new Clinica(exame.IdJuridica.Id);

            //        //Cliente xCliente = new Cliente(exame.IdEmpregado.nID_EMPR.Id);
            //        //string xEmpresa = "";

            //        //if (xCliente.IdJuridicaPai.Id != 0 && xCliente.IdJuridicaPai.Id != xCliente.Id)
            //        //{
            //        //    xEmpresa = xCliente.IdJuridicaPai.ToString() + "   Unidade: " + xCliente.NomeAbreviado.ToString();
            //        //}
            //        //else
            //        //{
            //        //    xEmpresa = xCliente.GetNomeEmpresa();
            //        //}



            //        //string xCorpo = "<body><p><font size='2' face='Tahoma'><H1 align='center'>Guia de Encaminhamento</H1></font></p> <br></br>" +
            //        //                "<p><font size='3' face='Tahoma'>Nome: " + exame.IdEmpregado.tNO_EMPG + "<br>" +
            //        //                "RG: " + exame.IdEmpregado.tNO_IDENTIDADE.ToString() + "      CPF: " +  exame.IdEmpregado.tNO_CPF + " <br>" +
            //        //                "Data de Nascimento: " + exame.IdEmpregado.hDT_NASC.ToString("dd/MM/yyyy") + "<br><br>" +
            //        //                "Empresa:  " + xEmpresa + "<br>" +
            //        //                "CNPJ: " + exame.IdEmpregado.nID_EMPR.GetCnpj() + "<br>" +
            //        //                "Cargo: " + exame.IdEmpregadoFuncao.nID_FUNCAO.ToString() + "<br>" +
            //        //                 "Setor: " + exame.IdEmpregadoFuncao.nID_SETOR.ToString() + "<br>" +
            //        //                 "Unidade de Trabalho: " + exame.IdEmpregadoFuncao.GetLocalDeTrabalho(exame.IdEmpregado.nID_EMPR) + "<br>" +
            //        //                 "Clinica: " + exame.IdJuridica.NomeAbreviado + "<br>" +
            //        //                 "Endereço da clinica: " + xClinica.GetEndereco().IdTipoLogradouro.NomeCompleto + " " + xClinica.GetEndereco().Logradouro + " " + xClinica.GetEndereco().Numero + " " + xClinica.GetEndereco().Bairro + " " + xClinica.GetEndereco().Municipio + "/" + xClinica.GetEndereco().Uf + "   Fone: " + exame.IdJuridica.GetContatoTelefonico() + "<br>" +
            //        //                 "Tipo de Exame: " + exame.IdExameDicionario.Nome + " <br>" +
            //        //                 "Data do Exame: " + exame.DataExame.ToString("dd/MM/yyyy") + " </font></p></body>";


            //       // Envio_Email_Daiti(exame.IdJuridica.Email.ToString(), "agendamento@5aessence.com.br", "Guia de Encaminhamento", xCorpo, xPath, "Guia / ASO / PCI", exame.IdEmpregado.Id, exame.IdEmpregado.nID_EMPR.Id);

            //    }
            //    else if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("GLOBAL") > 0)
            //    {


            //        FileStream fileStream = File.Create(xPath, (int)reportStream.Length);
            //        // Initialize the bytes array with the stream length and then fill it with data
            //        byte[] bytesInStream = new byte[reportStream.Length];
            //        reportStream.Read(bytesInStream, 0, bytesInStream.Length);
            //        // Use write method to write to the file specified above
            //        fileStream.Write(bytesInStream, 0, bytesInStream.Length);

            //        fileStream.Flush();

            //        fileStream.Dispose();
            //        fileStream = null;

            //        //Clinica xClinica = new Clinica(exame.IdJuridica.Id);

            //        //Cliente xCliente = new Cliente(exame.IdEmpregado.nID_EMPR.Id);
            //        //string xEmpresa = "";

            //        //if (xCliente.IdJuridicaPai.Id != 0 && xCliente.IdJuridicaPai.Id != xCliente.Id)
            //        //{
            //        //    xEmpresa = xCliente.IdJuridicaPai.ToString() + "   Unidade: " + xCliente.NomeAbreviado.ToString();
            //        //}
            //        //else
            //        //{
            //        //    xEmpresa = xCliente.GetNomeEmpresa();
            //        //}



            //        //string xCorpo = "<body><p><font size='2' face='Tahoma'><H1 align='center'>Guia de Encaminhamento</H1></font></p> <br></br>" +
            //        //                "<p><font size='3' face='Tahoma'>Nome: " + exame.IdEmpregado.tNO_EMPG + "<br>" +
            //        //                "RG: " + exame.IdEmpregado.tNO_IDENTIDADE.ToString() + "<br>" +
            //        //                "Data de Nascimento: " + exame.IdEmpregado.hDT_NASC.ToString("dd/MM/yyyy") + "<br><br>" +
            //        //                "Empresa:  " + xEmpresa + "<br>" +
            //        //                "CNPJ: " + exame.IdEmpregado.nID_EMPR.GetCnpj() + "<br>" +
            //        //                "Cargo: " + exame.IdEmpregadoFuncao.nID_FUNCAO.ToString() + "<br>" +
            //        //                 "Setor: " + exame.IdEmpregadoFuncao.nID_SETOR.ToString() + "<br>" +
            //        //                 "Unidade de Trabalho: " + exame.IdEmpregadoFuncao.GetLocalDeTrabalho(exame.IdEmpregado.nID_EMPR) + "<br>" +
            //        //                 "Clinica: " + exame.IdJuridica.NomeAbreviado + "<br>" +
            //        //                 "Endereço da clinica: " + xClinica.GetEndereco().IdTipoLogradouro.NomeCompleto + " " + xClinica.GetEndereco().Logradouro + " " + xClinica.GetEndereco().Numero + " " + xClinica.GetEndereco().Bairro + " " + xClinica.GetEndereco().Municipio + "/" + xClinica.GetEndereco().Uf + "   Fone: " + exame.IdJuridica.GetContatoTelefonico() + "<br>" +
            //        //                 "Tipo de Exame: " + exame.IdExameDicionario.Nome + " <br>" +
            //        //                 "Data do Exame: " + exame.DataExame.ToString("dd/MM/yyyy") + " </font></p></body>";



            //      //  Envio_Email_Global(exame.IdJuridica.Email.ToString(), "agendamento@5aessence.com.br", "Guia de Encaminhamento", xCorpo, xPath, "Guia / ASO / PCI", exame.IdEmpregado.Id, exame.IdEmpregado.nID_EMPR.Id);

            //    }
            //    else if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("QTECK") > 0)
            //    {


            //        FileStream fileStream = File.Create(xPath, (int)reportStream.Length);
            //        // Initialize the bytes array with the stream length and then fill it with data
            //        byte[] bytesInStream = new byte[reportStream.Length];
            //        reportStream.Read(bytesInStream, 0, bytesInStream.Length);
            //        // Use write method to write to the file specified above
            //        fileStream.Write(bytesInStream, 0, bytesInStream.Length);

            //        fileStream.Flush();

            //        fileStream.Dispose();
            //        fileStream = null;

            //        //Clinica xClinica = new Clinica(exame.IdJuridica.Id);

            //        //Cliente xCliente = new Cliente(exame.IdEmpregado.nID_EMPR.Id);
            //        //string xEmpresa = "";

            //        //if (xCliente.IdJuridicaPai.Id != 0 && xCliente.IdJuridicaPai.Id != xCliente.Id)
            //        //{
            //        //    xEmpresa = xCliente.IdJuridicaPai.ToString() + "   Unidade: " + xCliente.NomeAbreviado.ToString();
            //        //}
            //        //else
            //        //{
            //        //    xEmpresa = xCliente.GetNomeEmpresa();
            //        //}



            //        //string xCorpo = "<body><p><font size='2' face='Tahoma'><H1 align='center'>Guia de Encaminhamento</H1></font></p> <br></br>" +
            //        //                "<p><font size='3' face='Tahoma'>Nome: " + exame.IdEmpregado.tNO_EMPG + "<br>" +
            //        //                "RG: " + exame.IdEmpregado.tNO_IDENTIDADE.ToString() + "<br>" +
            //        //                "Data de Nascimento: " + exame.IdEmpregado.hDT_NASC.ToString("dd/MM/yyyy") + "<br><br>" +
            //        //                "Empresa:  " + xEmpresa + "<br>" +
            //        //                "CNPJ: " + exame.IdEmpregado.nID_EMPR.GetCnpj() + "<br>" +
            //        //                "Cargo: " + exame.IdEmpregadoFuncao.nID_FUNCAO.ToString() + "<br>" +
            //        //                 "Setor: " + exame.IdEmpregadoFuncao.nID_SETOR.ToString() + "<br>" +
            //        //                 "Unidade de Trabalho: " + exame.IdEmpregadoFuncao.GetLocalDeTrabalho(exame.IdEmpregado.nID_EMPR) + "<br>" +
            //        //                 "Clinica: " + exame.IdJuridica.NomeAbreviado + "<br>" +
            //        //                 "Endereço da clinica: " + xClinica.GetEndereco().IdTipoLogradouro.NomeCompleto + " " + xClinica.GetEndereco().Logradouro + " " + xClinica.GetEndereco().Numero + " " + xClinica.GetEndereco().Bairro + " " + xClinica.GetEndereco().Municipio + "/" + xClinica.GetEndereco().Uf + "   Fone: " + exame.IdJuridica.GetContatoTelefonico() + "<br>" +
            //        //                 "Tipo de Exame: " + exame.IdExameDicionario.Nome + " <br>" +
            //        //                 "Data do Exame: " + exame.DataExame.ToString("dd/MM/yyyy") + " </font></p></body>";


            //     //   Envio_Email_Qteck(exame.IdJuridica.Email.ToString(), "agendamento@5aessence.com.br", "Guia de Encaminhamento", xCorpo, xPath, "Guia / ASO / PCI", exame.IdEmpregado.Id, exame.IdEmpregado.nID_EMPR.Id);

            //    }
            //    else if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("COTIA") > 0)
            //    {


            //        FileStream fileStream = File.Create(xPath, (int)reportStream.Length);
            //        // Initialize the bytes array with the stream length and then fill it with data
            //        byte[] bytesInStream = new byte[reportStream.Length];
            //        reportStream.Read(bytesInStream, 0, bytesInStream.Length);
            //        // Use write method to write to the file specified above
            //        fileStream.Write(bytesInStream, 0, bytesInStream.Length);

            //        fileStream.Flush();

            //        fileStream.Dispose();
            //        fileStream = null;

            //        //Clinica xClinica = new Clinica(exame.IdJuridica.Id);

            //        //Cliente xCliente = new Cliente(exame.IdEmpregado.nID_EMPR.Id);
            //        //string xEmpresa = "";

            //        //if (xCliente.IdJuridicaPai.Id != 0 && xCliente.IdJuridicaPai.Id != xCliente.Id)
            //        //{
            //        //    xEmpresa = xCliente.IdJuridicaPai.ToString() + "   Unidade: " + xCliente.NomeAbreviado.ToString();
            //        //}
            //        //else
            //        //{
            //        //    xEmpresa = xCliente.GetNomeEmpresa();
            //        //}



            //        //string xCorpo = "<body><p><font size='2' face='Tahoma'><H1 align='center'>Guia de Encaminhamento</H1></font></p> <br></br>" +
            //        //                "<p><font size='3' face='Tahoma'>Nome: " + exame.IdEmpregado.tNO_EMPG + "<br>" +
            //        //                "RG: " + exame.IdEmpregado.tNO_IDENTIDADE.ToString() + "<br>" +
            //        //                "Data de Nascimento: " + exame.IdEmpregado.hDT_NASC.ToString("dd/MM/yyyy") + "<br><br>" +
            //        //                "Empresa:  " + xEmpresa + "<br>" +
            //        //                "CNPJ: " + exame.IdEmpregado.nID_EMPR.GetCnpj() + "<br>" +
            //        //                "Cargo: " + exame.IdEmpregadoFuncao.nID_FUNCAO.ToString() + "<br>" +
            //        //                 "Setor: " + exame.IdEmpregadoFuncao.nID_SETOR.ToString() + "<br>" +
            //        //                 "Unidade de Trabalho: " + exame.IdEmpregadoFuncao.GetLocalDeTrabalho(exame.IdEmpregado.nID_EMPR) + "<br>" +
            //        //                 "Clinica: " + exame.IdJuridica.NomeAbreviado + "<br>" +
            //        //                 "Endereço da clinica: " + xClinica.GetEndereco().IdTipoLogradouro.NomeCompleto + " " + xClinica.GetEndereco().Logradouro + " " + xClinica.GetEndereco().Numero + " " + xClinica.GetEndereco().Bairro + " " + xClinica.GetEndereco().Municipio + "/" + xClinica.GetEndereco().Uf + "   Fone: " + exame.IdJuridica.GetContatoTelefonico() + "<br>" +
            //        //                 "Tipo de Exame: " + exame.IdExameDicionario.Nome + " <br>" +
            //        //                 "Data do Exame: " + exame.DataExame.ToString("dd/MM/yyyy") + " </font></p></body>";

            //      //  Envio_Email_Cotia(exame.IdJuridica.Email.ToString(), "agendamento@5aessence.com.br", "Guia de Encaminhamento", xCorpo, xPath, "Guia / ASO / PCI", exame.IdEmpregado.Id, exame.IdEmpregado.nID_EMPR.Id);

            //    }


            // }



            //System.IO.File.Delete(xPath);
            //esse i:\\temp\\ ele considera do webserver ou da máquina local ??

            // ShowPdfDocument(response, reportStream);


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
