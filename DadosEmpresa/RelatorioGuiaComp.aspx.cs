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
	public partial class RelatorioGuiaComp : System.Web.UI.Page
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
            string xIdEmprFunc;

            //Int32 xTipoExame = 0;


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
                xImpDt = Request["ImpDt"];
                xObs = Session["Obs_Guia"].ToString().Trim();
                xEnvio_Email = Request["Mail"].ToString().Trim();
                xIdEmprFunc = Request["IdEmprFunc"];


                string xUsuario = Session["usuarioLogado"].ToString();

                //StringBuilder st = new StringBuilder();

                //st.Append("nID_EMPR=" + Session["Empresa"]);

                //st.Append(" AND gTERCEIRO=0");

                //st.Append(" ORDER BY tNO_EMPG");

                //ArrayList listEmpregado = new Empregado().Find(st.ToString());

                //if(listEmpregado.Count > 0)
                //{


                CrystalDecisions.CrystalReports.Engine.ReportClass[] reports = new ReportClass[1];

                Cliente zCliente = new Cliente();
                zCliente.Find(System.Convert.ToInt32(xId_Empresa));


                if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().IndexOf("Prajna") > 0)
                {
                    //RptGuia_Prajna report0 = new DataSourceGuia_Prajna(cliente, System.Convert.ToInt32(xId_Empregado), System.Convert.ToInt32(xId_Empresa), System.Convert.ToInt32(xId_Clinica), xExames, xExames2, xExames3, xExames4, xData_Exame, xHora_Exame, xTipo, xBasico, xObs, xImpDt ).GetReport();
                    RptGuia_Nova_Prajna report0 = new DataSourceGuia_Prajna(cliente, System.Convert.ToInt32(xId_Empregado), System.Convert.ToInt32(xId_Empresa), System.Convert.ToInt32(xId_Clinica), xExames, xExames2, xExames3, xExames4, xData_Exame, xHora_Exame, xTipo, xBasico, xObs, xImpDt, System.Convert.ToInt32(xIdEmprFunc)).GetReport_Nova();
                    //CreatePDFDocument(report, this.Response);
                    reports[0] = report0;
                }              
                else
                {
                    //RptGuia report0 = new DataSourceGuia(cliente, System.Convert.ToInt32(xId_Empregado), System.Convert.ToInt32(xId_Empresa), System.Convert.ToInt32(xId_Clinica), xExames, xExames2, xExames3, xExames4, xData_Exame, xHora_Exame, xTipo, xBasico, xObs, xImpDt).GetReport();
                    RptGuia_Nova report0 = new DataSourceGuia(cliente, System.Convert.ToInt32(xId_Empregado), System.Convert.ToInt32(xId_Empresa), System.Convert.ToInt32(xId_Clinica), xExames, xExames2, xExames3, xExames4, xData_Exame, xHora_Exame, xTipo, xBasico, xObs, xImpDt, System.Convert.ToInt32(xIdEmprFunc)).GetReport_Nova();
                    //CreatePDFDocument(report, this.Response);
                    reports[0] = report0;
                }
                                

                CreatePDFMerged(reports, this.Response, "", false, xId_Clinica, xId_Empregado, xId_Empresa, xData_Exame);

                      
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


        protected void Envio_Email_Prajna( string xPara, string xCopia, string xSubject, string xBody, string xAttach, string xModulo, Int32 xIdEmpregado, Int32 xIdEmpresa)
        {

            string xDestinatario = "";

            //enviar e-mail para clinica e prajna

            MailMessage objEmail = new MailMessage();

            //rementente do email           
            objEmail.From = new MailAddress("agendamento@essencenet.com.br");

            
            //para
            string xEmail = "";

            xEmail = xPara;

            if (xEmail == "")
            {
                throw new Exception("Clínica não possui e-mail cadastrado.");                
            }

            if (xEmail.IndexOf(";") > 0) xEmail = xEmail.Substring(0, xEmail.IndexOf(";"));

            objEmail.To.Add(xEmail);
            xDestinatario = xEmail + "; agendamento@5aessence.com.br ;";
                        
            objEmail.CC.Add("agendamento@5aessence.com.br");


            //cópia para usuário logado
            Entities.Usuario usuario = (Entities.Usuario)Session["usuarioLogado"];

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
            objSmtp.Credentials = new NetworkCredential("agendamento@essencenet.com.br", "kr.prj1705");

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
            //xEnvio.Envio_eMail(xIdEmpregado, xIdEmpresa, xDestinatario, "Guia / ASO / PCI");

            return;

        }




        protected void Envio_Email_Ilitera(string xPara, string xCopia, string xSubject, string xBody, string xAttach, string xModulo, Int32 xIdEmpregado, Int32 xIdEmpresa)
        {
            string xDestinatario = "";
            //enviar e-mail para clinica e prajna

            MailMessage objEmail = new MailMessage();

            //rementente do email
           // objEmail.From = new MailAddress("agendamento.sp.sto@ilitera.com.br");
            

            //caixa-postal de onde será enviado o e-mail
            //objEmail.ReplyTo = new MailAddress("email@seusite.com.br");

            //para
            //objEmail.To.Add("lasdowsky@gmail.com");
            string xEmail = "";

            xEmail = xPara;

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

            objSmtp.Send(objEmail);
            Ilitera.Data.Clientes_Clinicas xEnvio = new Ilitera.Data.Clientes_Clinicas();
            xEnvio.Envio_eMail(xIdEmpregado, xIdEmpresa, xDestinatario, "Guia / ASO / PCI");

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

            xEmail = xPara;

            if (xEmail == "")
            {
                throw new Exception("Clínica não possui e-mail cadastrado.");
            }

            if (xEmail.IndexOf(";") > 0) xEmail = xEmail.Substring(0, xEmail.IndexOf(";"));

            objEmail.To.Add(xEmail);
            xDestinatario = xEmail + "; atendimento@ilitera.com.br ;";

            //objEmail.Bcc.Add("oculto@provedor.com.br");

            
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


            objEmail.From = new MailAddress("guias2@globalsegmed.com.br");            
            objSmtp.Credentials = new NetworkCredential("guias2@globalsegmed.com.br", "Guiasglobal2#");


            objEmail.ReplyTo = new MailAddress("guias2@globalsegmed.com.br");
            ;

            objSmtp.Send(objEmail);


            Ilitera.Data.Clientes_Clinicas xEnvio = new Ilitera.Data.Clientes_Clinicas();
            xEnvio.Envio_eMail(xIdEmpregado, xIdEmpresa, xDestinatario, "Guia / ASO / PCI");

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

            xEmail = xPara;

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

            if (System.DateTime.Now.Second % 3 == 0)
            {
                objSmtp.Credentials = new NetworkCredential("agendamento1@ilitera.com.br", "Ilitera_3624");
            }
            else
            {
                objSmtp.Credentials = new NetworkCredential("agendamento3@ilitera.com.br", "Ilitera_3625");
            }

            objEmail.ReplyTo = new MailAddress("agendamento@ilitera.com.br");

            objSmtp.Send(objEmail);


            Ilitera.Data.Clientes_Clinicas xEnvio = new Ilitera.Data.Clientes_Clinicas();
            xEnvio.Envio_eMail(xIdEmpregado, xIdEmpresa, xDestinatario, "Guia / ASO / PCI");

            return;

        }



        protected void CreatePDFMerged(CrystalDecisions.CrystalReports.Engine.ReportClass[] reports, HttpResponse response, string watermark, bool RenumerarPaginas, string xIdClinica, string xIdEmpregado, string xIdEmpresa, string xData)
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

            CreatePDFMerged(streams, response, watermark, RenumerarPaginas, xIdClinica, xIdEmpregado, xIdEmpresa, xData);
        }



        protected  void CreatePDFMerged(Stream[] streams, HttpResponse response, string watermark, bool RenumerarPaginas, string xIdClinica, string xIdEmpregado, string xIdEmpresa, string xData)
        {
            MemoryStream reportStream = PdfSharpUtility.MergeReports(streams, string.Empty, watermark, RenumerarPaginas);


            if (xEnvio_Email == "S" || xEnvio_Email == "T")
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

                    Clinica xClinica = new Clinica(System.Convert.ToInt32( xIdClinica));//exame.IdJuridica.Id);
                    Empregado xEmpr = new Empregado(System.Convert.ToInt32(xIdEmpregado));
                    Cliente xCli = new Cliente(System.Convert.ToInt32(xIdEmpresa));


                    string xCorpo = "<body><p><font size='2' face='Tahoma'><H1 align='center'>Guia de Encaminhamento</H1></font></p> <br></br>" +
                                    "<p><font size='3' face='Tahoma'>Nome: " + xEmpr.tNO_EMPG + "<br>" +
                                    "RG: " + xEmpr.tNO_IDENTIDADE.ToString() + "<br>" +
                                    "Data de Nascimento: " + xEmpr.hDT_NASC.ToString("dd/MM/yyyy") + "<br><br>" +
                                    "Empresa:  " + "<br>" +
                                    "CNPJ: " + xCli.GetCnpj() + "<br>" +
                                     "Clinica: " + xClinica.NomeAbreviado + "<br>" +
                                     "Endereço da clinica: " + xClinica.GetEndereco().IdTipoLogradouro.NomeCompleto + " " + xClinica.GetEndereco().Logradouro + " " + xClinica.GetEndereco().Numero + " " + xClinica.GetEndereco().Bairro + " " + xClinica.GetEndereco().Municipio + "/" + xClinica.GetEndereco().Uf + "   Fone: " + xClinica.GetContatoTelefonico() + "<br>" +
                                     "Tipo de Exame: Exames Complementares <br>" +
                                     "Data do Exame: " + xData + " </font></p></body>";

                    //"Segue em anexo Guia de Encaminhamento / ASO / PCI do Colaborador " + exame.IdEmpregado.tNO_EMPG + " da empresa " + exame.IdEmpregado.nID_EMPR.NomeAbreviado, xPath, "Guia / ASO / PCI"

                    if (xEnvio_Email == "T")
                    {
                        Envio_Email_Prajna( xEmpr.teMail.ToString(), "agendamento@5aessence.com.br", "Guia de Encaminhamento", xCorpo, xPath, "Guia / Complementar", xEmpr.Id, xEmpr.nID_EMPR.Id);
                    }
                    else
                    {
                        Envio_Email_Prajna(xClinica.Email.ToString(), "agendamento@5aessence.com.br", "Guia de Encaminhamento", xCorpo, xPath, "Guia / Complementar", xEmpr.Id, xEmpr.nID_EMPR.Id);
                    }
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

                    Clinica xClinica = new Clinica(System.Convert.ToInt32(xIdClinica));//exame.IdJuridica.Id);
                    Empregado xEmpr = new Empregado(System.Convert.ToInt32(xIdEmpregado));
                    Cliente xCli = new Cliente(System.Convert.ToInt32(xIdEmpresa));


                    string xCorpo = "<body><p><font size='2' face='Tahoma'><H1 align='center'>Guia de Encaminhamento</H1></font></p> <br></br>" +
                                    "<p><font size='3' face='Tahoma'>Nome: " + xEmpr.tNO_EMPG + "<br>" +
                                    "RG: " + xEmpr.tNO_IDENTIDADE.ToString() + "<br>" +
                                    "Data de Nascimento: " + xEmpr.hDT_NASC.ToString("dd/MM/yyyy") + "<br><br>" +
                                    "Empresa:  " + "<br>" +
                                    "CNPJ: " + xCli.GetCnpj() + "<br>" +
                                     "Clinica: " + xClinica.NomeAbreviado + "<br>" +
                                     "Endereço da clinica: " + xClinica.GetEndereco().IdTipoLogradouro.NomeCompleto + " " + xClinica.GetEndereco().Logradouro + " " + xClinica.GetEndereco().Numero + " " + xClinica.GetEndereco().Bairro + " " + xClinica.GetEndereco().Municipio + "/" + xClinica.GetEndereco().Uf + "   Fone: " + xClinica.GetContatoTelefonico() + "<br>" +
                                     "Tipo de Exame: Exames Complementares <br>" +
                                     "Data do Exame: " + xData + " </font></p></body>";
                    //"Segue em anexo Guia de Encaminhamento / ASO / PCI do Colaborador " + exame.IdEmpregado.tNO_EMPG + " da empresa " + exame.IdEmpregado.nID_EMPR.NomeAbreviado, xPath, "Guia / ASO / PCI"

                    if (xEnvio_Email == "T")
                    {
                        Envio_Email_Global(xEmpr.teMail.ToString(), "agendamento@5aessence.com.br", "Guia de Encaminhamento", xCorpo, xPath, "Guia / Complementar", xEmpr.Id, xEmpr.nID_EMPR.Id);
                    }
                    else
                    {
                        Envio_Email_Global(xClinica.Email.ToString(), "agendamento@5aessence.com.br", "Guia de Encaminhamento", xCorpo, xPath, "Guia / Complementar", xEmpr.Id, xEmpr.nID_EMPR.Id);
                    }
                }
                else if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().Trim().ToUpper() == "OPSA")
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

                    Clinica xClinica = new Clinica(System.Convert.ToInt32(xIdClinica));//exame.IdJuridica.Id);
                    Empregado xEmpr = new Empregado(System.Convert.ToInt32(xIdEmpregado));
                    Cliente xCli = new Cliente(System.Convert.ToInt32(xIdEmpresa));

                    xEmpr.nID_EMPR.Find();

                    string xCorpo = "<body><p><font size='2' face='Tahoma'><H1 align='center'>Guia de Encaminhamento</H1></font></p> <br></br>" +
                                    "<p><font size='3' face='Tahoma'>Nome: " + xEmpr.tNO_EMPG + "<br>" +
                                    "RG: " + xEmpr.tNO_IDENTIDADE.ToString() + "<br>" +
                                    "Data de Nascimento: " + xEmpr.hDT_NASC.ToString("dd/MM/yyyy") + "<br><br>" +
                                    "Empresa:  " + xEmpr.nID_EMPR.NomeAbreviado.Trim() +  "<br>" +
                                    "CNPJ: " + xCli.GetCnpj() + "<br>" +
                                     "Clinica: " + xClinica.NomeAbreviado + "<br>" +
                                     "Endereço da clinica: " + xClinica.GetEndereco().IdTipoLogradouro.NomeCompleto + " " + xClinica.GetEndereco().Logradouro + " " + xClinica.GetEndereco().Numero + " " + xClinica.GetEndereco().Bairro + " " + xClinica.GetEndereco().Municipio + "/" + xClinica.GetEndereco().Uf + "   Fone: " + xClinica.GetContatoTelefonico() + "<br>" +
                                     "Tipo de Exame: Exames Complementares <br>" +
                                     "Data do Exame: " + xData + " </font></p></body>";
                    //"Segue em anexo Guia de Encaminhamento / ASO / PCI do Colaborador " + exame.IdEmpregado.tNO_EMPG + " da empresa " + exame.IdEmpregado.nID_EMPR.NomeAbreviado, xPath, "Guia / ASO / PCI"

                    if (xEnvio_Email == "T")
                    {
                        Envio_Email_Ilitera(xEmpr.teMail.ToString(), "agendamento@5aessence.com.br", "Guia de Encaminhamento", xCorpo, xPath, "Guia / Complementar", xEmpr.Id, xEmpr.nID_EMPR.Id);
                    }
                    else
                    {
                        Envio_Email_Ilitera(xClinica.Email.ToString(), "agendamento@5aessence.com.br", "Guia de Encaminhamento", xCorpo, xPath, "Guia / Complementar", xEmpr.Id, xEmpr.nID_EMPR.Id);
                    }

                }

            }

            ShowPdfDocument(response, reportStream);

            if (xEnvio_Email == "T")  //apenas para envio de email o "T"
            {
                MsgBox1.Show("Ilitera.Net", "Guia enviada para o e-mail cadastrado.", null,
                new EO.Web.MsgBoxButton("OK"));
            }

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
