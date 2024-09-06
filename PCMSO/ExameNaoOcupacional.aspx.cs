using System;
using System.Data;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using Ilitera.Common;
using Ilitera.Opsa.Data;
using System.Globalization;
using System.Collections;
using System.Collections.Generic;
using System.Web;


using System.Net.Mail;
using System.Net;

//using MestraNET;

namespace Ilitera.Net
{
    public partial class ExameNaoOcupacional : System.Web.UI.Page
    {
        
        private ExameFisicoAmbulatorial exame;
        
		//private string tipo;

        protected Prestador prestador = new Prestador();
        protected Usuario usuario = new Usuario();
        protected Cliente cliente = new Cliente();
        protected Empregado empregado = new Empregado();

        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);

           
        }

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        protected void Page_Load(object sender, System.EventArgs e)
		{

            string xUsuario = Session["usuarioLogado"].ToString();
            InicializaWebPageObjects();

            if ( wnePeso.Text.Trim() != "" && wneAltura.Text.Trim() != ""  )
            {
                Calcular_IMC();                 
            }
            else
            {
                txtIMC.Text = "";
            }

            if (!IsPostBack)
            {

                string zAcao = Request["Acao"].ToString().Trim();

                if (zAcao == "E")
                {
                    btnOK.Visible = true;
                    btnExcluir.Visible = true;
                }
                else
                {
                    btnOK.Visible = false;
                    btnExcluir.Visible = false;
                }


                rd_Enfermagem.Checked = true;
                rd_Medico.Checked = false;
                rd_Outros.Checked = false;
            

                Session["txtAuxiliar"] = string.Empty; 
                RegisterClientCode();
                PopulaDDLQueixas();
                PopulaDDLProcedimentos();
                if (!exame.Id.Equals(0))
                {
                    PopulaTelaExame();
                    Calcular_IMC();
                }



                if (exame.Id == 0)
                {
                    TabStrip1.Items[1].Visible = false;


                    System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");
                    wdtDataExame.Text = System.DateTime.Now.ToString("dd/MM/yyyy", ptBr);
                  

                    //se Capgemini, checar se existem 5 exames ambulatoriais já criados entre hoje e 60 dias anteriores
                    Cliente cliente;
                    cliente = new Cliente(System.Convert.ToInt32(Session["Empresa"].ToString()));

                    
                    cliente.IdGrupoEmpresa.Find();

                    if (cliente.IdGrupoEmpresa.Descricao.ToUpper().Trim() == "CAPGEMINI" && Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("PRAJNA") > 0)  //.Id == -905238295)  // Capgemini
                    {

                        btnemp.Visible = false;


                        ArrayList rExames = new ExameBase().Find("IdEmpregado=" + Request["IdEmpregado"].ToString() + " and IdExameDicionario = 7 and DataExame between dateadd( dd, -60, getdate() ) and Getdate()  order by DataExame desc");
                        


                        int rContador = 0;

                        if (rExames.Count > 4)
                        {

                            //checar se nos ultimos 5, algum está com alerta médico
                            foreach (ExameBase ambExame in rExames)
                            {

                                ExameFisicoAmbulatorial gExame = new ExameFisicoAmbulatorial(ambExame.Id);

                                //if (ambExame.IdMedico.Id == 1111)
                                if (gExame.Alerta_Medico != true)
                                    rContador++;
                                else
                                {
                                    rContador = 0;
                                    break;
                                }

                                if (rContador > 4)
                                    break;
                            }


                            if (rContador > 4)
                            {
                                //MsgBox1.Show("Ilitera.Net", "Encaminhar ao Atendimento com Médico", null,
                                //       new EO.Web.MsgBoxButton("OK"));

                                lblEncaminhar.Visible = true;

                                rd_Enfermagem.Checked = false;
                                rd_Outros.Checked = false;
                                rd_Medico.Checked = true;
                                rd_Medico.Enabled = false;
                                exame.Tipo = "M";
                                exame.Alerta_Medico = true;

                                chk_PacienteCritico.Visible = true;
                                
                                
                                rd_Enfermagem.Enabled = false;
                                rd_Outros.Enabled = false;

                                //exibir combo médicos e obrigar preenchimento
                                lblMedico.Visible = true;
                                ddlMedico.Visible = true;

                                PopulaDDLMedico();                      
                            }

                        }

                    }

                }
                else
                {
                    Guid strAux = Guid.NewGuid();

                    lkbAnamnese.Attributes.Add("onClick", "javascript:" + strOpenReport("PCMSO", "AnamneseDinamica.aspx?IliteraSystem=" + strAux.ToString() + strAux.ToString()
                        + "&IdExame=" + exame.Id, "Anamnese"));

                    exame.IdEmpregado.Find();
                    exame.IdEmpregado.nID_EMPR.Find();
                    exame.IdEmpregado.nID_EMPR.IdGrupoEmpresa.Find();

                    if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("PRAJNA") > 0 ) //exame.IdEmpregado.nID_EMPR.IdGrupoEmpresa.Descricao.ToUpper().Trim() == "CAPGEMINI" ) //&& Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("_HOM") > 0)  //.Id == -905238295)  // Capgemini
                    {
                        btnemp.Visible = true;

                        TabStrip1.Items[1].Visible = true;
                        Carregar_Dados_Anamnese_Exame();
                        Carregar_Grid_Anamnese_Capgemini();
                    }
                    else
                    {
                        TabStrip1.Items[1].Visible = false;
                        btnemp.Visible = false;
                    }


                    //if ( exame.IdMedico.Id != 1111 && exame.IdMedico.Id != 0 )
                    if (exame.Alerta_Medico == true)
                    {
                        rd_Enfermagem.Checked = false;
                        rd_Outros.Checked = false;
                        rd_Medico.Checked = true;
                        rd_Medico.Enabled = false;
                        exame.Tipo = "M";
                        rd_Enfermagem.Enabled = false;
                        rd_Outros.Enabled = false;

                        lblEncaminhar.Visible = true;

                        //exibir combo médicos e obrigar preenchimento
                        lblMedico.Visible = true;
                        ddlMedico.Visible = true;

                        chk_PacienteCritico.Visible = true;

                        PopulaDDLMedico();
                        ddlMedico.Items.FindByValue(exame.IdMedico.Id.ToString()).Selected = true;                        

                    }
                }



            }
            else
            {
                string IdDDL = string.Empty;

                //if (txtAuxiliar.Value.Equals("atualizaQueixaClinica"))
                if (Session["txtAuxiliar"].ToString().Trim()=="atualizaQueixaClinica")
                {
                    IdDDL = ddlQueixas.SelectedValue;
                    PopulaDDLQueixas();

                    if (ddlQueixas.Items.FindByValue(IdDDL) != null)
                        ddlQueixas.Items.FindByValue(IdDDL).Selected = true;
                }
                else if (Session["txtAuxiliar"].ToString().Trim()=="atualizaProcedimentoClinico")
                {
                    IdDDL = ddlProcedimento.SelectedValue;
                    PopulaDDLProcedimentos();

                    if (ddlProcedimento.Items.FindByValue(IdDDL) != null)
                        ddlProcedimento.Items.FindByValue(IdDDL).Selected = true;
                }

                txtAuxiliar.Value = string.Empty;
                Session["txtAuxiliar"] = string.Empty; 
            }


            if (exame.Id != 0)
            {
                if (exame.Confidencial == true)
                {
                    if (exame.IdUsuario_Confidencial != null)
                    {
                        Entities.Usuario user2 = (Entities.Usuario)Session["usuarioLogado"];

                        if (exame.IdUsuario_Confidencial != user2.IdUsuario)
                        {

                            ViewState["IdExameNaoOcupacional"] = 0;  // null;

                            btnExcluir.Enabled = true;
                            StringBuilder st = new StringBuilder();

                            txtAuxAviso.Value = "O Exame Ambulatorial é confidencial, não pode ser aberto. ";
                            txtExecutePost.Value = "True";
                            txtCloseWindow.Value = "True";

                            Response.Write("<script>window.opener.document.forms[0].submit(); window.close();</script>");

                            Entities.Usuario user = (Entities.Usuario)Session["usuarioLogado"];


                            if (Session["Retorno"].ToString().Trim() == "1")
                                Response.Redirect("DadosEmpregado.aspx?IdEmpregado=" + Session["Empregado"].ToString() + "&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString() + "&Tipo=2");
                            else if (Session["Retorno"].ToString().Trim() == "9")
                                Response.Redirect("DadosEmpregado_Dashboard.aspx?IdEmpregado=" + Session["Empregado"].ToString() + "&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString() + "&Tipo=6");
                            else if (Session["Retorno"].ToString().Trim() == "91")
                                Response.Redirect("DadosEmpregado_Questionario.aspx?IdEmpregado=" + Session["Empregado"].ToString() + "&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString() + "&Tipo=6");
                            else if (Session["Retorno"].ToString().Trim() == "19")
                                Response.Redirect("ExameQuestionario.aspx?IdEmpregado=" + Session["Empregado"].ToString() + "&IdExame=" + Session["Questionario"].ToString().Trim() + "&IdUsuario=" + Request["IdUsuario"] + "&IdEmpresa=" + Request["IdEmpresa"] + "&Acao=E");
                            else
                                Response.Redirect("DadosEmpregado_Lista.aspx?IdEmpregado=" + Session["Empregado"].ToString() + "&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString() + "&Tipo=6");

                            return;

                        }
                    }
                }

            }


        }

		protected void InicializaWebPageObjects()
		{
			//base.InicializaWebPageObjects();

			if (Request["IdExame"] != null && Request["IdExame"] != "")
			{
				exame = new ExameFisicoAmbulatorial(Convert.ToInt32(Request["IdExame"]));
				//tipo = "atualizado";
			}
			else if (ViewState["IdExameNaoOcupacional"] != null)
			{
				exame = new ExameFisicoAmbulatorial(Convert.ToInt32(ViewState["IdExameNaoOcupacional"]));
				//tipo = "atualizado";
			}
			else
			{
                exame = new ExameFisicoAmbulatorial();

                Empregado xEmpregado = new Empregado();
                xEmpregado.Find(System.Convert.ToInt32(Request["IdEmpregado"].ToString()));


				exame.IdEmpregado = xEmpregado;


                Entities.Usuario xusuario = (Entities.Usuario)Session["usuarioLogado"];
                prestador.Find(" IdPessoa = " + xusuario.IdPessoa.ToString());

                //if (usuario.NomeUsuario.Equals("Admin"))
                //    exame.IdJuridica = cliente;
                //else
                //{
                //    //exame.IdMedico.Id = prestador.Id;
                //    exame.IdJuridica.Id = prestador.IdJuridica.Id;
                //}

                //if (!cliente.ContrataPCMSO.Equals((int)TipoPcmsoContratada.NaoContratada))
                //{
                //    if (!usuario.NomeUsuario.Equals("Admin"))

                if (lblEncaminhar.Visible == true)
                {
                    exame.Alerta_Medico = true;
                    exame.Tipo = "M";
                    if (ddlMedico.SelectedIndex < 0 || ddlMedico.SelectedValue.ToString().Trim() == "0")
                        exame.IdMedico.Id = 1111;
                    else
                        exame.IdMedico.Id = System.Convert.ToInt32(ddlMedico.SelectedValue);
                }
                else
                {
                    if (ddlMedico.Visible == true)
                    {
                        if (ddlMedico.SelectedIndex < 0 || ddlMedico.SelectedValue.ToString().Trim() == "0")
                            exame.IdMedico.Id = 1111;
                        else
                            exame.IdMedico.Id = System.Convert.ToInt32(ddlMedico.SelectedValue);
                    }
                    else
                    {
                        exame.IdMedico.Id = 1111;//prestador.Id;                     
                    }

                    exame.Alerta_Medico = false;
                }


                exame.IdJuridica.Id = 310;

                //exame.IdMedico.Find();
                //exame.IdJuridica.Find();

                
                
                //}
                //else
                //    exame.IdMedico.Id = 1111; //PCMSO não contratada;

				//tipo = "cadastrado";
				btnExcluir.Enabled = false;
			}
		}


        private void PopulaDDLMedico()
        {
            //ajustar para pegar clinicas do cliente selecionado

            ddlMedico.DataSource = new qryMedico().Get(" ( IdJuridica in ( Select IdClinica from Clinica where IdClinica IN (SELECT IdClinica FROM ClinicaCliente WHERE IdCliente=" + Request["IdEmpresa"].ToString() + " ) or IdJuridica = 310 ) ) "
                + " AND IsInativo=0"
                + " AND IdMedico IN (SELECT IdJuridicaPessoa FROM JuridicaPessoa WHERE"
                + " IdPessoa IN (SELECT IdPessoa FROM Pessoa WHERE IsInativo=0)) ORDER BY NomeCompleto");
            ddlMedico.DataTextField = "NomeCompleto";
            ddlMedico.DataValueField = "IdMedico";
            ddlMedico.DataBind();

            ddlMedico.Items.Insert(0, new ListItem("Selecione...", "0"));
        }


        private void RegisterClientCode()
		{
            btnAddQueixas.Disabled = false;
            btnAddProcedimento.Disabled = false;
            btnAddQueixas.Attributes.Add("onClick", "javascript:AbreCadastro('CadQueixaClinica', " + Request["IdEmpresa"].ToString() + ", " + Request["IdUsuario"].ToString() + ");");
            btnAddProcedimento.Attributes.Add("onClick", "javascript:AbreCadastro('CadProcedimentoClinico', " + Request["IdEmpresa"].ToString() + ", " + Request["IdUsuario"].ToString()  + ");");
            btnExcluir.Attributes.Add("onClick", "javascript:return(confirm('Deseja realmente excluir este Exame Ambulatorial?'))");
		}

        private void PopulaDDLQueixas()
        {
            ddlQueixas.DataSource = new QueixaClinica().GetIdNome("NomeQueixa", "IdCliente=" + System.Convert.ToInt32( Request["IdEmpresa"].ToString()));
            ddlQueixas.DataValueField = "Id";
            ddlQueixas.DataTextField = "Nome";
            ddlQueixas.DataBind();

            ddlQueixas.Items.Insert(0, new ListItem("Selecione uma Queixa Clínica...", "0"));
        }

        private void PopulaDDLProcedimentos()
        {
            ddlProcedimento.DataSource = new ProcedimentoClinico().GetIdNome("NomeProcedimento", "IdCliente=" + System.Convert.ToInt32(Request["IdEmpresa"].ToString()));
            ddlProcedimento.DataValueField = "Id";
            ddlProcedimento.DataTextField = "Nome";
            ddlProcedimento.DataBind();

            ddlProcedimento.Items.Insert(0, new ListItem("Selecione um Procedimento Clínico...", "0"));
        }

        private void PopulaExame()
        {
            System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");
            exame.DataExame = System.Convert.ToDateTime(wdtDataExame.Text.Trim(), ptBr);
            exame.Prontuario = txtDescricao.Text.Trim();


            //if (!cliente.ContrataPCMSO.Equals((int)TipoPcmsoContratada.NaoContratada))
            //{
            exame.Altura = System.Convert.ToSingle(wneAltura.Text.Trim());
            exame.Peso = System.Convert.ToSingle(wnePeso.Text.Trim());
            exame.PressaoArterial = wtePA.Text.Trim();
            exame.Pulso = System.Convert.ToInt16(wnePulso.Text.Trim());
            exame.Temperatura = System.Convert.ToSingle(wneTemperatura.Text.Trim());

            try
            {
                exame.IdQueixaClinica.Id = Convert.ToInt32(ddlQueixas.SelectedValue);
                exame.IdProcedimentoClinico.Id = Convert.ToInt32(ddlProcedimento.SelectedValue);
            }
            catch ( Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }

            exame.Glicose = txtGlicose.Text.Trim();
            
            

            if (rd_Enfermagem.Checked == true)
            {
                exame.Tipo = "E";
            }
            else if (rd_Medico.Checked == true)
            {
                exame.Tipo = "M";
            }
            else
            {
                exame.Tipo = "O";
            }
            //}            
        }

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public void PopulaTelaExame()
        {
            wdtDataExame.Text = exame.DataExame.ToString("dd/MM/yyyy");
            txtDescricao.Text = exame.Prontuario;

            //if (!cliente.ContrataPCMSO.Equals((int)TipoPcmsoContratada.NaoContratada))
            //{
            wneAltura.Text = exame.Altura.ToString();
            wnePeso.Text = exame.Peso.ToString();
            wtePA.Text = exame.PressaoArterial;
            wnePulso.Text = exame.Pulso.ToString();
            wneTemperatura.Text = exame.Temperatura.ToString();

            try
            {
                ddlQueixas.Items.FindByValue(exame.IdQueixaClinica.Id.ToString()).Selected = true;
                ddlProcedimento.Items.FindByValue(exame.IdProcedimentoClinico.Id.ToString()).Selected = true;
            }
            catch ( Exception Ex)
            {
                System.Diagnostics.Debug.WriteLine(Ex.Message);
            }
            finally
            {

            }

            txtGlicose.Text = exame.Glicose.Trim();


            if ( exame.Paciente_Critico == true )
            {
                chk_PacienteCritico.Visible = true;
                chk_PacienteCritico.Checked = true;
                ddlMedico.Visible = true;
                lblMedico.Visible = true;
                PopulaDDLMedico();

                ddlMedico.Items.FindByValue(exame.IdMedico.Id.ToString()).Selected = true;

            }
            else
            {
                chk_PacienteCritico.Checked = false;                
            }


            if (exame.Tipo == "E")
            {
                rd_Medico.Checked = false;
                rd_Outros.Checked = false;
                rd_Enfermagem.Checked = true;
            }
            else if (exame.Tipo == "M")
            {
                rd_Enfermagem.Checked = false;
                rd_Outros.Checked = false;
                rd_Medico.Checked = true;
                Cliente cliente;
                cliente = new Cliente(System.Convert.ToInt32(Session["Empresa"].ToString()));

                cliente.IdGrupoEmpresa.Find();

                if (cliente.IdGrupoEmpresa.Descricao.ToUpper().Trim() == "CAPGEMINI" && Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("PRAJNA") > 0)  //.Id == -905238295)  // Capgemini
                {
                    chk_PacienteCritico.Visible = true;
                }
            }
            else
            {
                rd_Enfermagem.Checked = false;
                rd_Medico.Checked = false;
                rd_Outros.Checked = true;
            }


            //}			
        }
		
		protected void btnOK_Click(object sender, System.EventArgs e)
		{
			try
			{

                System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");

                if (wdtDataExame.Text.Trim() == "")
                {
                    wdtDataExame.Text = DateTime.Now.ToString("dd/MM/yyyy", ptBr);
                }


                if ( lblEncaminhar.Visible == true || chk_PacienteCritico.Checked == true)
                {
                    //se nessa condição,  obrigar preenchimento do ddlMedico
                    if ( ddlMedico.SelectedIndex < 0 || ddlMedico.SelectedValue.ToString().Trim() == "0")
                    {
                        MsgBox1.Show("Ilitera.Net", "Selecione Médico responsável pelo atendimento.", null,
                               new EO.Web.MsgBoxButton("OK"));
                        return;
                    }

                }


                if (chk_PacienteCritico.Checked == true)
                {
                    if ( txtDescricao.Text.Trim() == "" || txtDescricao.Text.Trim().Length < 5 )
                    {
                        MsgBox1.Show("Ilitera.Net", "Utilize o campo Observação para colocar um parecer do paciente crítico.", null,
                              new EO.Web.MsgBoxButton("OK"));
                        return;
                    }
                }


                PopulaExame();

                //exame.IdEmpregado = System.Convert.ToInt32(Request["IdEmpregado"].ToString());
                
                if ( lblEncaminhar.Visible == true)
                {
                    exame.Alerta_Medico = true;
                    exame.Tipo = "M";
                }

                if ( chk_PacienteCritico.Checked == true )
                {
                    exame.Paciente_Critico = true;
                }
                else
                {
                    exame.Paciente_Critico = false;
                }



                if (lblEncaminhar.Visible == true)
                {
                    exame.Alerta_Medico = true;
                    exame.Tipo = "M";
                    if (ddlMedico.SelectedIndex < 0 || ddlMedico.SelectedValue.ToString().Trim() == "0")
                        exame.IdMedico.Id = 1111;
                    else
                        exame.IdMedico.Id = System.Convert.ToInt32(ddlMedico.SelectedValue);
                }
                else
                {
                    if (ddlMedico.Visible == true)
                    {
                        if (ddlMedico.SelectedIndex < 0 || ddlMedico.SelectedValue.ToString().Trim() == "0")
                            exame.IdMedico.Id = 1111;
                        else
                            exame.IdMedico.Id = System.Convert.ToInt32(ddlMedico.SelectedValue);
                    }
                    else
                    {
                        exame.IdMedico.Id = 1111;//prestador.Id;                     
                    }

                    exame.Alerta_Medico = false;
                }


                exame.IdJuridica.Id = 310;


                if ( exame.Id==0)
                {
                    cliente = new Cliente(System.Convert.ToInt32(Session["Empresa"].ToString()));

                    if ( cliente.NomeAbreviado.ToUpper().IndexOf("JOHN DEERE") >=0 && cliente.Alerta_web_Agendamento.Trim()!="" )
                    {
                        exame.IdEmpregado.Find();                        

                        string xCorpo = "<body><p><font size='2' face='Tahoma'><H1 align='center'>Alerta de Envio de Convocação de Exame - Ili.Net</H1></font></p> <br></br>" +
                                   "<p><font size='3' face='Tahoma'>Nome: " + exame.IdEmpregado.tNO_EMPG + "<br>" +
                                   "Data de Nascimento: " + exame.IdEmpregado.hDT_NASC.ToString("dd/MM/yyyy") + "<br><br>" +
                                   "Empresa:  " + cliente.NomeAbreviado + "<br>" +
                                   "CNPJ: " + exame.IdEmpregado.nID_EMPR.GetCnpj() + "<br>" +                                  
                                    "Tipo de Exame: Exame Não-Ocupacional <br>" +
                                    "Data do Exame: " + exame.DataExame.ToString("dd/MM/yyyy") + "<br><br>" +
                                    "Usuário responsável: " + usuario.NomeUsuario.ToUpper() + " em " + System.DateTime.Now.ToString("dd/MM/yyyy hh:mm", ptBr) + "<br></font></p></body>";

                        Envio_Email_Ilitera_Alerta(cliente.Alerta_web_Agendamento.Trim(), "", "Alerta de Envio de Convocação de Exame", xCorpo, "", "Convocação de Exame", exame.IdEmpregado.Id, exame.IdEmpregado.nID_EMPR.Id);

                    }
                }

                exame.Save( System.Convert.ToInt32( Request["IdUsuario"].ToString()));
				
				ViewState["IdExameNaoOcupacional"] = exame.Id;
				btnExcluir.Enabled = true;
                				
				StringBuilder st = new StringBuilder();

				//st.Append("window.opener.document.getElementById('txtAuxiliar').value='atualiza';");
				//st.Append("window.opener.document.forms[0].submit();");
				//st.Append("window.alert('O Exame Ambulatorial foi " + tipo + " com sucesso!');");

                txtAuxAviso.Value = "O Exame Ambulatorial foi salvo com sucesso!";
                txtExecutePost.Value = "True";

                //if (cliente.ContrataPCMSO.Equals((int)TipoPcmsoContratada.NaoContratada))
                txtCloseWindow.Value = "True";

                Entities.Usuario user = (Entities.Usuario)Session["usuarioLogado"];

                if (Session["Retorno"].ToString().Trim() == "1")
                    Response.Redirect("DadosEmpregado.aspx?IdEmpregado=" + Session["Empregado"].ToString() + "&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString() + "&Tipo=2");
                else if (Session["Retorno"].ToString().Trim() == "9")
                    Response.Redirect("DadosEmpregado_Dashboard.aspx?IdEmpregado=" + Session["Empregado"].ToString() + "&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString() + "&Tipo=6");
                else if (Session["Retorno"].ToString().Trim() == "91")
                    Response.Redirect("DadosEmpregado_Questionario.aspx?IdEmpregado=" + Session["Empregado"].ToString() + "&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString() + "&Tipo=6");
                else if (Session["Retorno"].ToString().Trim() == "19")
                    Response.Redirect("ExameQuestionario.aspx?IdEmpregado=" + Session["Empregado"].ToString() + "&IdExame=" + Session["Questionario"].ToString().Trim() + "&IdUsuario=" + Request["IdUsuario"] + "&IdEmpresa=" + Request["IdEmpresa"] + "&Acao=E");
                else
                    Response.Redirect("DadosEmpregado_Lista.aspx?IdEmpregado=" + Session["Empregado"].ToString() + "&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString() + "&Tipo=6");



                //this.ClientScript.RegisterStartupScript(this.GetType(), "AtualizaNaoOcupacional", st.ToString(), true);
			}
			catch(Exception ex)
			{
                txtAuxAviso.Value = ex.Message;
                MsgBox1.Show("Ilitera.Net", ex.Message, null,
                       new EO.Web.MsgBoxButton("OK"));

			}
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




        protected void btnExcluir_Click(object sender, System.EventArgs e)
		{
			try
			{
                exame.Delete(System.Convert.ToInt32( Request["IdUsuario"].ToString() ));

                ViewState["IdExameNaoOcupacional"] = 0;  // null;

                btnExcluir.Enabled = true;
                StringBuilder st = new StringBuilder();

                txtAuxAviso.Value = "O Exame Ambulatorial foi deletado com sucesso";
                txtExecutePost.Value = "True";
                txtCloseWindow.Value = "True";

                Response.Write("<script>window.opener.document.forms[0].submit(); window.close();</script>");

                Entities.Usuario user = (Entities.Usuario)Session["usuarioLogado"];


                if (Session["Retorno"].ToString().Trim() == "1")
                    Response.Redirect("DadosEmpregado.aspx?IdEmpregado=" + Session["Empregado"].ToString() + "&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString() + "&Tipo=2");
                else if (Session["Retorno"].ToString().Trim() == "9")
                    Response.Redirect("DadosEmpregado_Dashboard.aspx?IdEmpregado=" + Session["Empregado"].ToString() + "&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString() + "&Tipo=6");
                else if (Session["Retorno"].ToString().Trim() == "91")
                    Response.Redirect("DadosEmpregado_Questionario.aspx?IdEmpregado=" + Session["Empregado"].ToString() + "&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString() + "&Tipo=6");
                else if (Session["Retorno"].ToString().Trim() == "19")
                    Response.Redirect("ExameQuestionario.aspx?IdEmpregado=" + Session["Empregado"].ToString() + "&IdExame=" + Session["Questionario"].ToString().Trim() + "&IdUsuario=" + Request["IdUsuario"] + "&IdEmpresa=" + Request["IdEmpresa"] + "&Acao=E");
                else
                    Response.Redirect("DadosEmpregado_Lista.aspx?IdEmpregado=" + Session["Empregado"].ToString() + "&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString() + "&Tipo=6");


            }
			catch(Exception ex)
			{
                txtAuxAviso.Value = ex.Message;
                MsgBox1.Show("Ilitera.Net", ex.Message, null,
                       new EO.Web.MsgBoxButton("OK"));

			}
		}

        protected void cmd_Voltar_Click(object sender, EventArgs e)
        {

            Entities.Usuario user = (Entities.Usuario)Session["usuarioLogado"];
            if (Session["Retorno"].ToString().Trim() == "1")
                Response.Redirect("DadosEmpregado.aspx?IdEmpregado=" + Session["Empregado"].ToString() + "&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString() + "&Tipo=2");
            else if (Session["Retorno"].ToString().Trim() == "9")
                Response.Redirect("DadosEmpregado_Dashboard.aspx?IdEmpregado=" + Session["Empregado"].ToString() + "&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString() + "&Tipo=6");
            else if (Session["Retorno"].ToString().Trim() == "91")
                Response.Redirect("DadosEmpregado_Questionario.aspx?IdEmpregado=" + Session["Empregado"].ToString() + "&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString() + "&Tipo=6");
            else if (Session["Retorno"].ToString().Trim() == "19")
                Response.Redirect("ExameQuestionario.aspx?IdEmpregado=" + Session["Empregado"].ToString() + "&IdExame=" + Session["Questionario"].ToString().Trim() + "&IdUsuario=" + Request["IdUsuario"] + "&IdEmpresa=" + Request["IdEmpresa"] + "&Acao=E");
            else
                Response.Redirect("DadosEmpregado_Lista.aspx?IdEmpregado=" + Session["Empregado"].ToString() + "&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString() + "&Tipo=6");


        }


        private void Calcular_IMC()
        {
            float zIMC = 0;

            float zPeso = 0;
            float zAltura = 0;

            bool zReturn = false;

            string decimalSeparator = CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator;
            string xTroca = " ";

            if (decimalSeparator == ".") xTroca = ",";
            else xTroca = ".";


            wnePeso.Text = wnePeso.Text.Replace(xTroca, decimalSeparator);
            wneAltura.Text = wneAltura.Text.Replace(xTroca, decimalSeparator);


            if (float.TryParse(wnePeso.Text, out zPeso))
            {
                // success! Use f here
            }
            else
            {
                wnePeso.Text = "0";
                txtIMC.Text = "";
                zReturn = true;
            }


            if (float.TryParse(wneAltura.Text, out zAltura))
            {
                // success! Use f here
            }
            else
            {
                wneAltura.Text = "0";
                txtIMC.Text = "";
                zReturn = true;
            }
            

            if (zAltura <= 0)
            {
                wneAltura.Text = "0";
                txtIMC.Text = "";
                zReturn = true;
            }

            if ( zPeso <= 0 )
            {
                wnePeso.Text = "0";
                txtIMC.Text = "";
                zReturn = true;
            }


            if (zReturn == true) return;
            
            if (zAltura > 100) zAltura = zAltura / 100;

            if ( zAltura == 0 )
            {
                txtIMC.Text = "";
                return;
            }

            zIMC = (zPeso / (zAltura * zAltura));

            txtIMC.Text = zIMC.ToString("#.##");
            return;

        }








        protected void gridAnamnese_ItemCommand(object sender, EO.Web.GridCommandEventArgs e)
        {
            //Check whether it is from our client side
            //JavaScript call


            //Session["Id2"] = e.Item.Key.ToString();
            //Session["NomeEmpregado"] = e.Item.Cells[0].Value.ToString();



        }



        private void Carregar_Grid_Anamnese_Capgemini()
        {
            
            if (exame.Id == 0)
            {
                // criar registros padrão,  mas como criar se idclinico = 0
                return;
            }

            //DataSet ds = new Ilitera.Opsa.Data.Anamnese_Questao().Get(" Questao is not null order by Sistema, Questao ");

            Ilitera.Data.Clientes_Funcionarios zQuest = new Ilitera.Data.Clientes_Funcionarios();

            DataSet ds = zQuest.Trazer_Anamnese_Exame(exame.Id);


            gridAnamnese.DataSource = ds;
            gridAnamnese.DataBind();


            //string xSistema = "";
            //System.Drawing.Color zColor = new System.Drawing.Color();

            //int zMud = 0;

            //for (int xCont = 0; xCont < gridAnamnese.Items.Count; xCont++)
            //{
            //    if (xSistema != gridAnamnese.Items[xCont].Cells[4].Value.ToString())
            //    {
            //        zMud = zMud + 1;

            //        if (zMud % 2 == 1)
            //            zColor = Color.LightYellow;
            //        else
            //            zColor = Color.White;

            //        xSistema = gridAnamnese.Items[xCont].Cells[4].Value.ToString();

            //    }

            //    //gridAnamnese.Items[xCont].Grid.BackColor = zColor;

            //    //gridAnamnese.Items[xCont].Cells[6].Column.CellStyle.BackColor = zColor;
            //    gridAnamnese.Items[xCont].Grid.BackColor = zColor;
            //}

        }


        private void Carregar_Dados_Anamnese_Exame()
        {


            if (exame.Id == 0)
            {
                return;
            }



            List<Anamnese_Exame> AnExame = new Anamnese_Exame().Find<Anamnese_Exame>(" IdExameBase = " + exame.Id);



            if (AnExame.Count == 0)
            {
                //trazer padrão para cliente
                exame.IdEmpregado.nID_EMPR.Find();
                List<Anamnese_Dinamica> anExamePadrao = new Anamnese_Dinamica().Find<Anamnese_Dinamica>(" IdPessoa = " + exame.IdEmpregado.nID_EMPR.Id);


                if (anExamePadrao.Count == 0)
                {
                    return;
                }
                else
                {

                    Ilitera.Data.Clientes_Funcionarios xAnam = new Ilitera.Data.Clientes_Funcionarios();
                    xAnam.Carregar_Anamnese_Dinamica(exame.IdEmpregado.nID_EMPR.Id, exame.Id);

                    //foreach (Anamnese_Dinamica zPadrao in anExamePadrao)
                    //{
                    //    Anamnese_Exame rTestes = new Anamnese_Exame();

                    //    rTestes.IdAnamneseDinamica = zPadrao.Id;
                    //    rTestes.IdExameBase = exame.Id;
                    //    rTestes.Resultado = "N";
                    //    rTestes.Peso = zPadrao.Peso;
                    //    rTestes.Save();
                    //}

                }

            }


            return;


        }

        protected void cmd_Anamnese_Click(object sender, EventArgs e)
        {

            Int32 zKey = 0;
            string zResultado = "N";


            if (gridAnamnese.ChangedItems.Length == 0)
            {
                MsgBox1.Show("Ilitera.Net", "Não houve alterações na Anamnese", null,
                new EO.Web.MsgBoxButton("OK"));
                return;
            }

            else
            {
                foreach (EO.Web.GridItem item in gridAnamnese.ChangedItems)
                {
                    foreach (EO.Web.GridCell cell in item.Cells)
                    {
                        if (cell.Modified)
                        {
                            //string text = string.Format(
                            //    "Cell Changed: Key = {0}, Field = {1}, New Value = {2}",
                            //    item.Key,
                            //    cell.Column.DataField,
                            //    cell.Value);
                            zKey = System.Convert.ToInt32(item.Key);
                            zResultado = cell.Value.ToString().Trim();


                            Ilitera.Data.Clientes_Funcionarios zAtualiza = new Ilitera.Data.Clientes_Funcionarios();

                            zAtualiza.Atualizar_Anamnese_Dinamica(zKey, zResultado.Substring(0, 1));

                        }
                    }
                }

                MsgBox1.Show("Ilitera.Net", "Alterações na Anamnese salva.", null,
                new EO.Web.MsgBoxButton("OK"));
                return;

            }

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
                    + "&Key=" + strAux.ToString() + "MestraNet" + strAux.ToString()
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

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        protected void rd_Medico_CheckedChanged(object sender, EventArgs e)
        {

            Cliente cliente;
            cliente = new Cliente(System.Convert.ToInt32(Session["Empresa"].ToString()));

            cliente.IdGrupoEmpresa.Find();

            if (cliente.IdGrupoEmpresa.Descricao.ToUpper().Trim() == "CAPGEMINI" && Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("PRAJNA") > 0)  //.Id == -905238295)  // Capgemini
            {

                if (rd_Medico.Checked == true)
                {
                    chk_PacienteCritico.Visible = true;
                }
                else
                {
                    chk_PacienteCritico.Checked = false;
                    chk_PacienteCritico.Visible = false;

                    if ( lblEncaminhar.Visible == false)
                    {
                        ddlMedico.Visible = false;
                        lblMedico.Visible = false;
                    }

                }
            }

        }

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        protected void rd_Enfermagem_CheckedChanged(object sender, EventArgs e)
        {
            Cliente cliente;
            cliente = new Cliente(System.Convert.ToInt32(Session["Empresa"].ToString()));

            cliente.IdGrupoEmpresa.Find();

            if (cliente.IdGrupoEmpresa.Descricao.ToUpper().Trim() == "CAPGEMINI" && Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("PRAJNA") > 0)  //.Id == -905238295)  // Capgemini
            {

                if (rd_Medico.Checked == true)
                {
                    chk_PacienteCritico.Visible = true;
                }
                else
                {
                    chk_PacienteCritico.Checked = false;
                    chk_PacienteCritico.Visible = false;

                    if (lblEncaminhar.Visible == false)
                    {
                        ddlMedico.Visible = false;
                        lblMedico.Visible = false;
                    }
                }
            }
        }

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        protected void rd_Outros_CheckedChanged(object sender, EventArgs e)
        {
            Cliente cliente;
            cliente = new Cliente(System.Convert.ToInt32(Session["Empresa"].ToString()));

            cliente.IdGrupoEmpresa.Find();

            if (cliente.IdGrupoEmpresa.Descricao.ToUpper().Trim() == "CAPGEMINI" && Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("PRAJNA") > 0)  //.Id == -905238295)  // Capgemini
            {

                if (rd_Medico.Checked == true)
                {
                    chk_PacienteCritico.Visible = true;
                }
                else
                {
                    chk_PacienteCritico.Checked = false;
                    chk_PacienteCritico.Visible = false;

                    if (lblEncaminhar.Visible == false)
                    {
                        ddlMedico.Visible = false;
                        lblMedico.Visible = false;
                    }
                }
            }
        }

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        protected void Chk_PacienteCritico_CheckedChanged(object sender, EventArgs e)
        {
            Cliente cliente;
            cliente = new Cliente(System.Convert.ToInt32(Session["Empresa"].ToString()));

            cliente.IdGrupoEmpresa.Find();

            if (cliente.IdGrupoEmpresa.Descricao.ToUpper().Trim() == "CAPGEMINI" && Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("PRAJNA") > 0)  //.Id == -905238295)  // Capgemini
            {

                if (chk_PacienteCritico.Checked == true)
                {
                    ddlMedico.Visible = true;
                    lblMedico.Visible = true;
                    PopulaDDLMedico();
                }
                else
                {
                    if (lblEncaminhar.Visible == false)
                    {
                        ddlMedico.Visible = false;
                        lblMedico.Visible = false;
                    }
                }

            }

        }

        protected void btnemp_Click(object sender, EventArgs e)
        {

            Guid strAux = Guid.NewGuid();
            System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");

            if ( exame.Id == 0 )
            {
                if ( wdtDataExame.Text.Trim()== "")
                {
                    wdtDataExame.Text = DateTime.Now.ToString("dd/MM/yyyy", ptBr);
                }
                

                exame.Save(System.Convert.ToInt32(Request["IdUsuario"].ToString()));
                ViewState["IdExameNaoOcupacional"] = exame.Id;

                Carregar_Dados_Anamnese_Exame();
            }

            OpenReport("PCMSO", "AnamneseDinamicaBranco.aspx?IliteraSystem=" + strAux.ToString() + "&IdExame=" + exame.Id , "Anamnese", true);

        }



        protected void OpenReport(string directory, string fileAndQuery, string ReportName)
        {
            this.OpenReport(directory, fileAndQuery, ReportName, false);
        }

        protected void OpenReport(string directory, string fileAndQuery, string ReportName, bool useDirectoryForLocalProcess)
        {
            StringBuilder st = new StringBuilder();

            Guid strAux = Guid.NewGuid();

            string valueProcess = "Local";

            if (valueProcess.Equals("Remote"))
                st.Append("void(window.open('http://report.ilitera.net/index.aspx?Identity=" + strAux.ToString() + "opsa" + strAux.ToString()
                    + "&Key=" + strAux.ToString() + "MestraNet" + strAux.ToString()
                    + "&PathAndQuery=" + HttpUtility.UrlEncode(directory + "/" + fileAndQuery) + "', '" + ReportName + "', 'scrollbars=yes, resizable=yes, toolbar=no, addressbar=no, menubar=no, width=800px, height=600px'));");
            else if (valueProcess.Equals("Local"))
            {
                if (useDirectoryForLocalProcess)
                {
                    //st.Append("void(window.open('../" + directory + "/" + fileAndQuery + "', '" + ReportName + "', 'scrollbars=yes, resizable=yes, toolbar=no, addressbar=no, menubar=no, width=800px, height=600px'));");
                    st.AppendFormat("void(window.open('../{0}/{1}', '{2}','scrollbars=yes, resizable=yes, toolbar=no, addressbar=no, menubar=no, width=800px, height=600px'));", directory, fileAndQuery, ReportName);
                }
                else
                {
                    //st.Append("void(window.open('" + fileAndQuery + "', '" + ReportName + "', 'scrollbars=yes, resizable=yes, toolbar=no, addressbar=no, menubar=no, width=800px, height=600px'));");
                    st.AppendFormat("void(window.open('{0}','{1}', 'scrollbars=yes, resizable=yes, toolbar=no, addressbar=no, menubar=no, width=800px, height=600px'));", fileAndQuery, ReportName);
                }
            }

            ScriptManager.RegisterStartupScript(this, this.GetType(), String.Format("OpenReport{0}", ReportName), st.ToString(), true);
        }





    }
}
