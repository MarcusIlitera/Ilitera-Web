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
using Ilitera.PCMSO.Report;
using System.Text;
//using Ilitera.Net.Documentos;

using System.IO;
using System.Net;
using System.Xml;
using System.Xml.Linq;

using System.Configuration;


namespace Ilitera.Net
{
    /// <summary>
    /// 
    /// </summary>
    public partial class Agendamento_Auto : System.Web.UI.Page
    {



        protected void Page_Load(object sender, System.EventArgs e)
        {
            // InicializaWebPageObjects();

          

            Ilitera.Data.SQLServer.EntitySQLServer.xDB1 = ConfigurationManager.AppSettings["DB1"].ToString();
            Ilitera.Data.SQLServer.EntitySQLServer.xDB2 = ConfigurationManager.AppSettings["DB2"].ToString();
            Ilitera.Data.SQLServer.EntitySQLServer._LocalServer = ConfigurationManager.AppSettings["LocalServer"].ToString();

            //controlar controles visiveis neste primeiro momento
            cmdConfirmar.Enabled = false;
            txtSenha.Enabled = false;


            string xToken = "";

            xToken = Request["xToken"].Trim();


            

            //procurar Token no tblConvocacao.  Se já estiver com hDT_Convocacao preenchido, avisar que já está agendado

            Convocacao rConvocacao = new Convocacao();
            rConvocacao.Find(System.Convert.ToInt32(xToken));


            if ( rConvocacao.Id == 0 )
            {
                //avisar que token é inválido 
                txt_Status.Text = "Link para agendamento inválido.";
            }
            else
            {
                if ( rConvocacao.hDt_Convocacao != null && rConvocacao.hDt_Convocacao.Year > 2018 )
                {
                    //avisar que já foi agendado e sair
                    txt_Status.Text = "Agendamento já foi realizado.";
                }
                else
                {
                    cmdConfirmar.Enabled = true;
                    txtSenha.Enabled = true;

                    lblIdExameDicionario.Text = rConvocacao.nID_Exame_Tipo.ToString().Trim();

                    lbl_Token.Text = xToken.Trim();
                }
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

        protected void cmdAgendamento_Click(object sender, EventArgs e)
        {

            //criar Guia e ASO
            //enviar e-mail para colaborador, clínica e responsável
            //abrir tela com guia e ASO para colaborador salvar se quiser


            //atualizar data de agendamento em tblConvocacao
            string zData = "";


            if (rd_D1.Checked == true)
                zData = rd_D1.Text;
            else if ( rd_D2.Checked == true )
                zData = rd_D2.Text;
            else if (rd_D3.Checked == true)
                zData = rd_D3.Text;
            else if (rd_D4.Checked == true)
                zData = rd_D4.Text;
                        
            rd_D1.Enabled = false;
            rd_D2.Enabled = false;
            rd_D3.Enabled = false;
            rd_D4.Enabled = false;

            cmdAgendamento.Enabled = false;


            if (zData == "")
                txt_Status.Text = "Erro na carga de data de agendamento.";
            else
            {
                //if (lblIdExameDicionario.Text.Trim() == "4")
                //{
                    Entities.Usuario user = (Entities.Usuario)Session["usuarioLogado"];
                    Response.Redirect("~\\PCMSO\\DadosEmpregado_Guia_Auto.aspx?IdEmpregado=" + lbl_IdEmpregado.Text + "&IdEmpresa=" + lbl_IdEmpresa.Text + "&IdUsuario=245253956&TipoExame=4&Data=" + zData + "&IdClinica=" + lbl_IdClinica.Text + "&CodUsuario=245253956&xId=KIT_" + lbl_Token.Text + "&xIdExame=" + lblIdExameDicionario.Text.Trim());
                //}
                //else
                //{

                //}

                txt_Status.Text = "Processamento Finalizado";
            }

        }



        protected void cmdConfirmar_Click(object sender, EventArgs e)
        {

            Convocacao rConvocacao = new Convocacao();
            rConvocacao.Find(System.Convert.ToInt32(lbl_Token.Text));

            if ( rConvocacao.Id!= 0 )
            {

                if ( rConvocacao.nSenha_Temp.ToUpper().Trim() != txtSenha.Text.ToUpper().Trim() )
                {
                    txt_Status.Text = "Senha inválida.";
                    txtSenha.Enabled = false;
                    cmdConfirmar.Enabled = false;
                }
                else
                {
                    cmdConfirmar.Visible = false;
                    txtSenha.Visible = false;
                    lblSenhaValidada.Visible = true;

                    Carregar_Agendamento( rConvocacao.nID_Empregado, rConvocacao.nID_Empr, rConvocacao.nID_Clinica, rConvocacao.hDt_Planejada, rConvocacao.Data_Envio );
                }
            }
            
        }



        private void Carregar_Agendamento( Int32 nIdEmpregado, Int32 nIdEmpr, Int32 nIdClinica, DateTime rDataPlanejada, DateTime rDataEnvio )
        {
            
            Clinica rClinica = new Clinica();
            Empregado rEmpregado = new Empregado();
            Ilitera.Common.Pessoa rEmpresa = new Ilitera.Common.Pessoa();

            System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");

            txtSenha.Enabled = false;
            cmdConfirmar.Enabled = false;



            //pegar Empresa                    
            rEmpresa.Find(nIdEmpr);

            //se não achar empresa,  emitir retorno avisando
            if (rEmpresa.Id == 0)
            {
                txt_Status.Text = "Erro: Empresa não localizada";
            }


            lblEmpresa.Text = rEmpresa.NomeAbreviado;
            lbl_IdEmpresa.Text = rEmpresa.Id.ToString().Trim();


            if (txt_Status.Text == "")
            {
                //pegar Colaborador                       
                rEmpregado.Find(nIdEmpregado);

                //se não achar empregado,  emitir retorno avisando
                if (rEmpregado.Id == 0)
                {
                    txt_Status.Text = "Erro: Empregado não localizado";
                }
            }

            lblColaborador.Text = rEmpregado.tNO_EMPG;
            lbl_IdEmpregado.Text = rEmpregado.Id.ToString().Trim();


            if (txt_Status.Text == "")
            {

                rClinica.Find(nIdClinica);

                //se não achar clinica,  emitir retorno avisando
                if (rClinica.Id == 0)
                {
                    txt_Status.Text = "Erro: Clínica não localizada";
                }

            }

            lblClinica.Text = rClinica.NomeCompleto;
            lbl_IdClinica.Text = rClinica.Id.ToString().Trim();

            lblDataPlanejada.Text = rDataPlanejada.ToString("dd/MM/yyyy", ptBr);

            lblDataEnvio.Text = rDataEnvio.ToString("dd/MM/yyyy", ptBr);



            //rClinica.GetEndereco();

            if (txt_Status.Text == "")
            {

                string xD1 = "";
                string xD2 = "";
                string xD3 = "";
                string xD4 = "";


                if (rDataPlanejada >= rDataEnvio)
                {
                    if (rDataPlanejada.AddDays(-1).DayOfWeek == DayOfWeek.Saturday)
                    {
                        xD1 = rDataPlanejada.AddDays(-2).ToString("dd/MM/yyyy", ptBr);
                        xD2 = rDataPlanejada.AddDays(1).ToString("dd/MM/yyyy", ptBr);
                        xD3 = rDataPlanejada.AddDays(2).ToString("dd/MM/yyyy", ptBr);
                        xD4 = rDataPlanejada.AddDays(3).ToString("dd/MM/yyyy", ptBr);
                    }
                    else if (rDataPlanejada.AddDays(-1).DayOfWeek == DayOfWeek.Sunday)
                    {
                        xD1 = rDataPlanejada.AddDays(-3).ToString("dd/MM/yyyy", ptBr);
                        xD2 = rDataPlanejada.ToString("dd/MM/yyyy", ptBr);
                        xD3 = rDataPlanejada.AddDays(1).ToString("dd/MM/yyyy", ptBr);
                        xD4 = rDataPlanejada.AddDays(2).ToString("dd/MM/yyyy", ptBr);
                    }
                    else
                    {
                        xD1 = rDataPlanejada.AddDays(-1).ToString("dd/MM/yyyy", ptBr);

                        if (rDataPlanejada.DayOfWeek == DayOfWeek.Saturday)
                            xD2 = rDataPlanejada.AddDays(+2).ToString("dd/MM/yyyy", ptBr);
                        else if (rDataPlanejada.DayOfWeek == DayOfWeek.Saturday)
                            xD2 = rDataPlanejada.AddDays(+1).ToString("dd/MM/yyyy", ptBr);
                        else
                            xD2 = rDataPlanejada.ToString("dd/MM/yyyy", ptBr);


                        if (rDataPlanejada.AddDays(1).DayOfWeek == DayOfWeek.Saturday)
                            xD3 = rDataPlanejada.AddDays(+3).ToString("dd/MM/yyyy", ptBr);
                        else if (rDataPlanejada.AddDays(1).DayOfWeek == DayOfWeek.Sunday)
                            xD3 = rDataPlanejada.AddDays(2).ToString("dd/MM/yyyy", ptBr);
                        else
                            xD3 = rDataPlanejada.AddDays(1).ToString("dd/MM/yyyy", ptBr);

                        if (rDataPlanejada.AddDays(2).DayOfWeek == DayOfWeek.Saturday)
                            xD4 = rDataPlanejada.AddDays(4).ToString("dd/MM/yyyy", ptBr);
                        else if (rDataPlanejada.AddDays(2).DayOfWeek == DayOfWeek.Sunday)
                            xD4 = rDataPlanejada.AddDays(3).ToString("dd/MM/yyyy", ptBr);
                        else
                            xD4 = rDataPlanejada.AddDays(2).ToString("dd/MM/yyyy", ptBr);
                    }

                }
                else
                {
                    if (rDataEnvio.AddDays(+1).DayOfWeek == DayOfWeek.Saturday)
                    {
                        xD1 = rDataEnvio.AddDays(3).ToString("dd/MM/yyyy", ptBr);
                        xD2 = rDataEnvio.AddDays(4).ToString("dd/MM/yyyy", ptBr);
                        xD3 = rDataEnvio.AddDays(5).ToString("dd/MM/yyyy", ptBr);
                        xD4 = rDataEnvio.AddDays(6).ToString("dd/MM/yyyy", ptBr);
                    }
                    else if (rDataEnvio.AddDays(+1).DayOfWeek == DayOfWeek.Sunday)
                    {
                        xD1 = rDataEnvio.AddDays(2).ToString("dd/MM/yyyy", ptBr);
                        xD2 = rDataEnvio.AddDays(3).ToString("dd/MM/yyyy", ptBr);
                        xD3 = rDataEnvio.AddDays(4).ToString("dd/MM/yyyy", ptBr);
                        xD4 = rDataEnvio.AddDays(5).ToString("dd/MM/yyyy", ptBr);
                    }
                    else
                    {
                        xD1 = rDataEnvio.AddDays(1).ToString("dd/MM/yyyy", ptBr);


                        if (rDataEnvio.AddDays(+2).DayOfWeek == DayOfWeek.Saturday)
                            xD2 = rDataEnvio.AddDays(4).ToString("dd/MM/yyyy", ptBr);
                        else if (rDataEnvio.AddDays(+2).DayOfWeek == DayOfWeek.Sunday)
                            xD2 = rDataEnvio.AddDays(3).ToString("dd/MM/yyyy", ptBr);
                        else
                            xD2 = rDataEnvio.AddDays(2).ToString("dd/MM/yyyy", ptBr);

                        if (rDataEnvio.AddDays(3).DayOfWeek == DayOfWeek.Saturday)
                            xD3 = rDataEnvio.AddDays(5).ToString("dd/MM/yyyy", ptBr);
                        else if (rDataEnvio.AddDays(3).DayOfWeek == DayOfWeek.Sunday)
                            xD3 = rDataEnvio.AddDays(4).ToString("dd/MM/yyyy", ptBr);
                        else
                            xD3 = rDataEnvio.AddDays(3).ToString("dd/MM/yyyy", ptBr);

                        if (rDataEnvio.AddDays(4).DayOfWeek == DayOfWeek.Saturday)
                            xD4 = rDataEnvio.AddDays(6).ToString("dd/MM/yyyy", ptBr);
                        else if (rDataEnvio.AddDays(4).DayOfWeek == DayOfWeek.Sunday)
                            xD4 = rDataEnvio.AddDays(5).ToString("dd/MM/yyyy", ptBr);
                        else
                            xD4 = rDataEnvio.AddDays(4).ToString("dd/MM/yyyy", ptBr);
                    }

                }


                ExameDicionario rExame = new ExameDicionario();
                rExame.Find("IdExameDicionario = " + lblIdExameDicionario.Text);
                lblExame.Text = rExame.Nome;

                rd_D1.Text = xD1;
                rd_D2.Text = xD2;
                rd_D3.Text = xD3;
                rd_D4.Text = xD4;
                

                lblTit1.Visible = true;
                lblTit2.Visible = true;
                lblTit3.Visible = true;
                lblTit4.Visible = true;
                lblTit5.Visible = true;
                lblTit6.Visible = true;

                lblClinica.Visible = true;
                lblColaborador.Visible = true;
                lblEmpresa.Visible = true;
                lblExame.Visible = true;

                lbl_Data.Visible = true;
                lblDataPlanejada.Visible = true;
                lblDataEnvio.Visible = true;

                rd_D1.Visible = true;
                rd_D2.Visible = true;
                rd_D3.Visible = true;
                rd_D4.Visible = true;


                cmdAgendamento.Visible = true;

            }
            

        }




    }
}

