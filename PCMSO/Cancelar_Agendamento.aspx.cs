using System;
using System.Data;
using System.Web;
using System.Web.SessionState;
using System.Web.UI.WebControls;
using Ilitera.Common;
using System.Text;
using Ilitera.Opsa.Data;
using Ilitera.Data;
using System.Web.UI;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;


using System.Net.Mail;
using System.Net;

using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using Facade;
using System.Configuration;
using System.Linq;
using System.Web.Security;

using System.Xml.Linq;

using Entities;
using BLL;

//  ter um grid com os CAs já indicados para este funcionário

//  idéia será exibir lista de PCMSOs
//  selecionou PCMSO,  exibir EPIs em combo
//  selecionou EPI,  colocar dados do CA, data entrega
//  salvar em nova tabela, com IdPCMSO, IdEPI, nIdEmpregado, dDtEntrega, CA

namespace Ilitera.Net
{
    public partial class Cancelar_Agendamento : System.Web.UI.Page
    {

        protected System.Web.UI.WebControls.TextBox txtPagina;
        protected System.Web.UI.WebControls.TextBox txtBuscaEmpresa;
        protected System.Web.UI.HtmlControls.HtmlTableCell TDGridListaEmpregado;
        protected System.Web.UI.HtmlControls.HtmlAnchor Principal;
        protected System.Web.UI.HtmlControls.HtmlTableCell NavBar;


        protected Prestador prestador = new Prestador();
        protected Ilitera.Common.Usuario usuario = new Ilitera.Common.Usuario();
        protected Cliente cliente = new Cliente();
        protected Ilitera.Opsa.Data.Empregado empregado = new Ilitera.Opsa.Data.Empregado();

        protected ExameClinicoFacade exame = new ExameClinicoFacade();


		protected void Page_Load(object sender, System.EventArgs e)
		{
			InicializaWebPageObjects();
            //PreencheLabels("lblEmp", cliente.NomeAbreviado.ToString());

            string xUsuario = Session["usuarioLogado"].ToString();
            if (!IsPostBack)
			{

                cmd_Excluir.Attributes.Add("onClick", "javascript:return confirm('Você deseja realmente excluir este ASO/Guia ?');");
                PopulaGrid();


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

		protected void InicializaWebPageObjects()
		{
			//base.InicializaWebPageObjects();
			StringBuilder st = new StringBuilder();

			//st.Append("top.window.document.frames.conteudo.window.document.getElementById('txtIdEmpregado').value = '" + empregado.Id + "';");
            st.Append("top.window.document.getElementById('txtIdEmpregado').value = '" + Request["IdEmpregado"] + "';");
            st.Append("top.window.document.getElementById('txtIdUsuario').value = '" + Request["IdUsuario"] + "';");
            st.Append("top.window.document.getElementById('txtIdEmpresa').value = '" + Request["IdEmpresa"] + "';");
                            
            this.ClientScript.RegisterStartupScript(this.GetType(), "IdEmpregado", st.ToString(), true);
            //btnFichaCompleta.Attributes.Add("onClick", "addItem(centerWin('../DadosEmpresa/FichaCompleta.aspx?IdEmpregado=" + Request["IdEmpregado"] + "&IdEmpresa=" + Request["IdEmpresa"] + "&IdUsuario=" + Request["IdUsuario"] + "',560,320,\'FichaCompleta\'),\'Todos\'); Reload();");
                        
		}




        
        

     


        


	

        protected void cmd_Voltar_Click(object sender, EventArgs e)
        {
            Entities.Usuario user = (Entities.Usuario)Session["usuarioLogado"];
            Response.Redirect("~\\ListaEmpregados.aspx?&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString());
        }
        

        private void PopulaGrid()
        {
            gridEmpregados.DataSource = GeraDataSet();
            gridEmpregados.DataBind();
        }


        private DataSet GeraDataSet()
        {

            DataSet ds;

            Ilitera.Data.PPRA_EPI xEPI = new Ilitera.Data.PPRA_EPI();

            ds = xEPI.Trazer_Exames_Clinicos_Sem_Resultado_Em_Espera(System.Convert.ToInt32(Request["IdEmpregado"].ToString()));

            return ds;
        }





        //protected void gridEmpregados_CellSelectionChanged(object sender, SelectedCellEventArgs e)
        //{

        //    string xId = "0";

        //    //try
        //    //{
        //        if (e.CurrentSelectedCells.Count >= 0)
        //        {
        //            xId = e.CurrentSelectedCells[0].Row.DataKey.GetValue(0).ToString();
        //            lbl_ID.Text = xId;
        //        }
        //    //}
        //    //catch (Exception ex)
        //    //{
        //    //    this.ClientScript.RegisterStartupScript(this.GetType(), "alerta", String.Format("alert('{0}');", ex.Message), true);
        //    //}
        //}

        protected void cmd_Excluir_Click(object sender, EventArgs e)
        {


            if (  lbl_ID.Text.Trim() != "0" )
            {


                exame.Find(System.Convert.ToInt32(lbl_ID.Text));

                string xPath = "";



                Clinica xClinica = new Clinica(exame.IdJuridica.Id);

                Ilitera.Opsa.Data.Cliente xCliente = new Ilitera.Opsa.Data.Cliente(System.Convert.ToInt32(Session["Empresa"].ToString()));
                Ilitera.Opsa.Data.Empregado xEmpregado = new Ilitera.Opsa.Data.Empregado(System.Convert.ToInt32(Session["Empregado"].ToString()));



                if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().IndexOf("Prajna") > 0)
                {
                    
                    
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



                    string xCorpo = "<body><p><font size='2' face='Tahoma'><H1 align='center'>Cancelamento de Guia de Encaminhamento</H1></font></p> <br></br>" +
                                    "<p><font size='3' face='Tahoma'>Nome: " + xEmpregado.tNO_EMPG + "<br>" +
                                    "RG: " + xEmpregado.tNO_IDENTIDADE.ToString() + "<br>" +
                                    "Data de Nascimento: " + xEmpregado.hDT_NASC.ToString("dd/MM/yyyy") + "<br><br>" +
                                    "Empresa:  " + xEmpresa + "<br>" +
                                    "CNPJ: " + xCliente.GetCnpj() + "<br>" +
                                     "Tipo de Exame: " + exame.IdExameDicionario.ToString() + " <br>" +
                                     "Data do Exame: " + exame.DataExame.ToString("dd/MM/yyyy") + " </font></p></body>";

                    //"Segue em anexo Guia de Encaminhamento / ASO / PCI do Colaborador " + exame.IdEmpregado.tNO_EMPG + " da empresa " + exame.IdEmpregado.nID_EMPR.NomeAbreviado, xPath, "Guia / ASO / PCI"

                    Envio_Email_Prajna(exame.IdJuridica.Email.ToString(), "agendamento@5aessence.com.br", "Cancelamento de Guia de Encaminhamento", xCorpo, xPath, "Cancelamento Guia / ASO / PCI", xEmpregado.Id, xCliente.Id);
                }
                else if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().Trim().ToUpper() == "OPSA")
                {
                                  

                    string xEmpresa = "";

                    if (xCliente.IdJuridicaPai.Id != 0 && xCliente.IdJuridicaPai.Id != xCliente.Id)
                    {
                        xEmpresa = xCliente.IdJuridicaPai.ToString() + "   Unidade: " + xCliente.NomeAbreviado.ToString();
                    }
                    else
                    {
                        xEmpresa = xCliente.GetNomeEmpresa();
                    }
                    


                    string xCorpo = "<body><p><font size='2' face='Tahoma'><H1 align='center'>Cancelamento de Guia de Encaminhamento</H1></font></p> <br></br>" +
                                    "<p><font size='3' face='Tahoma'>Nome: " + xEmpregado.tNO_EMPG + "<br>" +
                                    "RG: " + xEmpregado.tNO_IDENTIDADE.ToString() + "<br>" +
                                    "Data de Nascimento: " + xEmpregado.hDT_NASC.ToString("dd/MM/yyyy") + "<br><br>" +
                                    "Empresa:  " + xEmpresa + "<br>" +
                                    "CNPJ: " + xCliente.GetCnpj() + "<br>" +                                    
                                     "Tipo de Exame: " + exame.IdExameDicionario.ToString() + " <br>" +
                                     "Data do Exame: " + exame.DataExame.ToString("dd/MM/yyyy") + " </font></p></body>";

                    //"Segue em anexo Guia de Encaminhamento / ASO / PCI do Colaborador " + exame.IdEmpregado.tNO_EMPG + " da empresa " + exame.IdEmpregado.nID_EMPR.NomeAbreviado, xPath, "Guia / ASO / PCI"

                    Envio_Email_Ilitera(exame.IdJuridica.Email.ToString(), "agendamento@5aessence.com.br", "Cancelamento de Guia de Encaminhamento", xCorpo, xPath, "Cancelamento Guia / ASO / PCI", xEmpregado.Id, xCliente.Id);

                }



                Ilitera.Data.PPRA_EPI xEPI = new Ilitera.Data.PPRA_EPI();

                xEPI.Excluir_Exame_Clinico(System.Convert.ToInt32(lbl_ID.Text)); 


                lbl_ID.Text = "0";
               
                PopulaGrid();

            }
                

        }

     


        protected void gridEmpregados_ItemCommand(object sender, EO.Web.GridCommandEventArgs e)
        {

            //Session["Empregado"] = e.Item.Key.ToString();
            //Session["NomeEmpregado"] = e.Item.Cells[0].Value.ToString();

            string xId = "";

            xId = e.Item.Key.ToString();
            lbl_ID.Text = xId;

        }




        protected void  Envio_Email_Prajna(string xPara, string xCopia, string xSubject, string xBody, string xAttach, string xModulo, Int32 xIdEmpregado, Int32 xIdEmpresa)
        {

            string xDestinatario = "";

            //enviar e-mail para clinica e prajna

            MailMessage objEmail = new MailMessage();

            //rementente do email           
            objEmail.From = new MailAddress("agendamento@essencenet.com.br");


            //para
            string xEmail = "";

            xEmail = exame.IdJuridica.Email.ToString().Trim();

            if (xEmail == "")
            {
                MsgBox1.Show("e-mail", "Clínica não possui e-mail cadastrado.  E-mail então será enviado ao prestador.", null,
                new EO.Web.MsgBoxButton("OK"));
                objEmail.To.Add("agendamento@5aessence.com.br");
            }
            else
            {
                if (xEmail.IndexOf(";") > 0) xEmail = xEmail.Substring(0, xEmail.IndexOf(";"));

                objEmail.To.Add(xEmail);
            }

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

            if (xAttach != "")
            {
                Attachment xItem = new Attachment(xAttach);
                objEmail.Attachments.Add(xItem);
            }

            SmtpClient objSmtp = new SmtpClient();
            //objSmtp.Host = "mail.exchange.locaweb.com.br";
            //objSmtp.Port = 587;
            //objSmtp.Credentials = new NetworkCredential("agendamento@essencenet.com.br", "kr.prj1705");

            //objSmtp.EnableSsl = true;

            //objSmtp.Host = "outlook.office.com";
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
            //xEnvio.Envio_eMail(xIdEmpregado, xIdEmpresa, xDestinatario, "Guia / ASO / PCI");

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
           // xEmail = "wagner.sp.sto@ilitera.com.br";

            if (xEmail == "")
            {
                MsgBox1.Show("e-mail", "Clínica não possui e-mail cadastrado.  E-mail então será enviado ao prestador.", null,
                new EO.Web.MsgBoxButton("OK"));
                objEmail.To.Add("atendimento@ilitera.com.br");
            }
            else
            {
                if (xEmail.IndexOf(";") > 0) xEmail = xEmail.Substring(0, xEmail.IndexOf(";"));

                objEmail.To.Add(xEmail);
            }

            xDestinatario = xEmail + "; atendimento@ilitera.com.br ;";

            //objEmail.Bcc.Add("oculto@provedor.com.br");

            objEmail.CC.Add("atendimento@ilitera.com.br");

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

            objSmtp.Send(objEmail);


            Ilitera.Data.Clientes_Clinicas xEnvio = new Ilitera.Data.Clientes_Clinicas();
            xEnvio.Envio_eMail(xIdEmpregado, xIdEmpresa, xDestinatario, "Guia / ASO / PCI");

            return;

        }




	}


}



