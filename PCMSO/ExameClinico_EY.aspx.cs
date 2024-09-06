using System;
using System.Web.SessionState;
using System.Data;
using System.Web.UI.WebControls;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using Ilitera.Common;
using Ilitera.Opsa.Data;
using System.Globalization;
using System.Configuration;
using System.Drawing;
using System.Web;
using System.Data.SqlClient;

namespace Ilitera.Net
{
    public partial class ExameClinico_EY : System.Web.UI.Page
    {
        protected Prestador prestador = new Prestador();
        protected Ilitera.Opsa.Data.Empregado empregado = new Ilitera.Opsa.Data.Empregado();
        protected Ilitera.Common.Usuario usuario = new Ilitera.Common.Usuario();

        private string tipo;
        private ExameClinicoFacade exame;

        #region Override

        //protected override void OnPreInit(EventArgs e)
        //{
        //    //InicializaWebPageObjects();
        //    base.OnPreInit(e);
        //}

        #endregion

        protected void Page_Load(object sender, System.EventArgs e)
        {


            string xUsuario = Session["usuarioLogado"].ToString();

            if (Session["NomeEmpregado"] == null)
            {

            }
            else
            {

                if (Session["NomeEmpregado"].ToString().Trim() == "")
                {
                    lblNome.Text = "Exame Clínico ";
                }
                else
                {
                    lblNome.Text = "Exame Clínico : " + Session["NomeEmpregado"].ToString().Trim();
                }

            }

            string zAcao = Request["Acao"].ToString().Trim();

            if (zAcao == "E")
            {
                btnOK.Visible = true;
                btnExcluir.Visible = true;
                imbAdd.Enabled = true;
                imbAddAll.Enabled = true;
                imbRemove.Enabled = true;
                imbRemoveAll.Enabled = true;
                chk_TodosMedicos.Enabled = true;
            }
            else
            {
                btnOK.Visible = false;
                btnExcluir.Visible = false;
                imbAdd.Enabled = false;
                imbAddAll.Enabled = false;
                imbRemove.Enabled = false;
                imbRemoveAll.Enabled = false;
                chk_TodosMedicos.Enabled = false;
            }

            ////InicializaExameClinico();
            InicializaWebPageObjects();

            if (wnePeso.Text.Trim() != "" && wneAltura.Text.Trim() != "")
            {
                Calcular_IMC();
            }
            else
            {
                lblIMC.Text = "";
            }


            if (!IsPostBack)
            {
                RegisterClientCode();
                PopulaDropDownExame((int)IndTipoExame.Clinico);
                PopulaUltraWebGrid(grd_Complementares, GetExamesComplementares(), lblTotRegistros);
                PopulaLsbFatorRisco();


                Cliente cliente;
                cliente = new Cliente(System.Convert.ToInt32(Session["Empresa"].ToString()));


                cliente.IdGrupoEmpresa.Find();

                TabStrip1.Items[0].Visible = true;
                //   TabStrip1.Items[7].Visible = false;



                if (Ilitera.Data.SQLServer.EntitySQLServer._LocalServer.ToUpper().IndexOf("EY") > 0 && exame.Id == 0)
                {
                    //EY - salvar exame ao abrir, e já disponibolizar anamnese


                    ddlTipoExame.Items.FindByValue("4").Selected = true;
                    exame.IdExameDicionario.Id = Convert.ToInt32(ddlTipoExame.SelectedItem.Value);


                    //if (wdtDataExame.Date.Hour.Equals(0) && wdtDataExame.Date.Minute.Equals(0) && wdtDataExame.Date.Second.Equals(0))
                    //exame.DataExame = wdtDataExame.Date.AddHours(DateTime.Now.Hour).AddMinutes(DateTime.Now.Minute).AddSeconds(DateTime.Now.Second);
                    //else

                    System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");

                    wdtDataExame.Text = System.DateTime.Now.ToString("dd/MM/yyyy", ptBr);
                    exame.DataExame = System.Convert.ToDateTime(wdtDataExame.Text, ptBr);

                    PopulaDDLClinicas(ddlTipoExame.SelectedValue.ToString());

                    ddlClinica.SelectedIndex = 1;
                    exame.IdJuridica.Find(Convert.ToInt32(ddlClinica.SelectedValue));

                    PopulaDDLMedico();

                    rblResultado.SelectedIndex = 2;
                    exame.IndResultado = Convert.ToInt16(rblResultado.SelectedItem.Value);

                    Entities.Usuario usuario = (Entities.Usuario)Session["usuarioLogado"];
                    ViewState["IdExame"] = exame.Save(usuario.IdUsuario);


                    //gera erro ao entrar aqui
                    //PopulaTelaExame();

                    tipo = "atualizado";

                }
                else
                {




                    if (!exame.Id.Equals(0))   //quando abre exame já salvo, checar porque não seleciona checkboxs selecionados. - Wagner.
                    {
                        PopulaTelaExame();

                        //exame.Fisico.hasPeleNormal = (int)StatusAnamnese.Sim;
                        //ckbPeleNormal.Checked = true;
                        //exame.Fisico.hasOlhosNormal = (int)StatusAnamnese.Sim;
                        //ckbOlhosNormal.Checked = true;
                        //exame.Fisico.hasCabecaNormal = (int)StatusAnamnese.Sim;
                        //ckbCabecaNormal.Checked = true;
                        //exame.Fisico.hasMSNormal = (int)StatusAnamnese.Sim;
                        //ckbOmbrosNormal.Checked = true;
                        //exame.Fisico.hasMINormal = (int)StatusAnamnese.Sim;
                        //ckbMINormal.Checked = true;
                        //exame.Fisico.hasMaosNormal = (int)StatusAnamnese.Sim;
                        //ckbMaosNormal.Checked = true;


                        Calcular_IMC();
                    }
                    else
                    {

                        if (Request["IdEmpresa"].ToString().Trim() == "1260401972" || Request["IdEmpresa"].ToString().Trim() == "1801248344")
                        {
                            if (txtObs.Text.ToString().IndexOf("Apto para manipulação de alimentos.") < 0)
                            {
                                txtObs.Text = txtObs.Text.Trim() + " Apto para manipulação de alimentos.";
                            }
                        }

                    }
                }




                if (exame.Id == 0)
                {

                    chk_apt_Altura.Checked = false;
                    chk_inapt_Altura.Checked = false;
                    chk_apt_Confinado.Checked = false;
                    chk_inapt_Confinado.Checked = false;
                    chk_apt_Transportes.Checked = false;
                    chk_inapt_Transportes.Checked = false;
                    chk_apt_Submerso.Checked = false;
                    chk_inapt_Submerso.Checked = false;
                    chk_apt_Eletricidade.Checked = false;
                    chk_inapt_Eletricidade.Checked = false;
                    chk_apt_Aquaviarios.Checked = false;
                    chk_inapt_Aquaviarios.Checked = false;
                    chk_apt_Alimento.Checked = false;
                    chk_inapt_Alimento.Checked = false;
                    chk_Apt_Brigadista.Checked = false;
                    chk_Inapt_Brigadista.Checked = false;
                    chk_Apt_Socorrista.Checked = false;
                    chk_Inapt_Socorrista.Checked = false;
                    chk_Apt_Respirador.Checked = false;
                    chk_Inapt_Respirador.Checked = false;
                    chk_Apt_Radiacao.Checked = false;
                    chk_Inapt_Radiacao.Checked = false;

                    chk_TodosMedicos.Checked = false;

                    TabStrip1.Items[6].Visible = false;

                    if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("PRAJNA") > 0)
                    {
                        Carregar_Exames_Especiais();
                    }
                    else
                    {
                        Ativar_Exames_Especiais();
                    }




                }
                else
                {
                    chk_TodosMedicos.Checked = true;

                    Carregar_Imagem();

                    exame.IdEmpregado.Find();
                    exame.IdEmpregado.nID_EMPR.Find();
                    exame.IdEmpregado.nID_EMPR.IdGrupoEmpresa.Find();

                    if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("PRAJNA") > 0 || Ilitera.Data.SQLServer.EntitySQLServer._LocalServer.ToUpper().IndexOf("EY") > 0) //if (exame.IdEmpregado.nID_EMPR.IdGrupoEmpresa.Descricao.ToUpper().Trim() == "CAPGEMINI" ) // && Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("_HOM") > 0)  //.Id == -905238295)  // Capgemini
                    {
                        TabStrip1.Items[6].Visible = true;
                        TabStrip1.Items[0].Visible = false;
                        Carregar_Dados_Anamnese_Exame2();
                        Carregar_Grid_Anamnese_Capgemini();

                        Carregar_Exames_Especiais();

                    }
                    else
                    {
                        TabStrip1.Items[6].Visible = false;
                        TabStrip1.Items[0].Visible = true;

                        Ativar_Exames_Especiais();
                    }
                }






            }

            //else
            //{
            if (lbl_Atualiza.Text == "1")
            {
                lbl_Atualiza.Text = "0";
                txtAuxiliar.Value = string.Empty;
                PopulaLsbFatorRisco();
                PopulaLsbFatorRiscoSel();
            }
            //}
            //}
        }




        protected static void PopulaUltraWebGrid(EO.Web.Grid zGrid, DataSet ds, Label lblTotRegistros)
        {
            zGrid.DataSource = ds;
            zGrid.DataBind();

            if (ds.Tables[0].Rows.Count > 0)
                lblTotRegistros.Text = "Total de registros: <b>" + ds.Tables[0].Rows.Count.ToString() + "</b>";
            else
                lblTotRegistros.Text = "Nenhum registro encontrado";
        }


        protected void cmd_Voltar_Click(object sender, EventArgs e)
        {
            Entities.Usuario user = (Entities.Usuario)Session["usuarioLogado"];

            if (Session["Retorno"].ToString().Trim() == "1")
                Response.Redirect("DadosEmpregado.aspx?IdEmpregado=" + Session["Empregado"].ToString() + "&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString() + "&Tipo=1");
            else if (Session["Retorno"].ToString().Trim() == "9")
                Response.Redirect("DadosEmpregado_Dashboard.aspx?IdEmpregado=" + Session["Empregado"].ToString() + "&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString() + "&Tipo=6");
            else if (Session["Retorno"].ToString().Trim() == "91")
                Response.Redirect("DadosEmpregado_Questionario.aspx?IdEmpregado=" + Session["Empregado"].ToString() + "&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString() + "&Tipo=6");
            else if (Session["Retorno"].ToString().Trim() == "19")
                Response.Redirect("ExameQuestionario.aspx?IdEmpregado=" + Session["Empregado"].ToString() + "&IdExame=" + Session["Questionario"].ToString().Trim() + "&IdUsuario=" + Request["IdUsuario"] + "&IdEmpresa=" + Request["IdEmpresa"] + "&Acao=E" );
            else
                Response.Redirect("DadosEmpregado_Lista.aspx?IdEmpregado=" + Session["Empregado"].ToString() + "&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString() + "&Tipo=6");
        }




        //#region Popula ListBox&DropDown

        private void PopulaLsbFatorRisco()
        {
            lsbFatorRisco.DataSource = new FatorRisco().GetIdNome("Nome", "IdCliente=" + Request["IdEmpresa"]
                + " AND IdFatorRisco NOT IN (SELECT IdFatorRisco FROM FatorRiscoClinico WHERE IdClinico=" + exame.Id + ")");

            lsbFatorRisco.DataValueField = "Id";
            lsbFatorRisco.DataTextField = "Nome";
            lsbFatorRisco.DataBind();

            //if (lsbFatorRisco.Items.Count.Equals(0) || exame.Id.Equals(0))
            //{
            //    imbAdd.ImageUrl = "img/rightDisabled.jpg";
            //    imbAddAll.ImageUrl = "img/rightAllDisabled.jpg";
            //}
            //else
            //{
            imbAdd.ImageUrl = "img/right.jpg";
            imbAddAll.ImageUrl = "img/rightAllEnabled.jpg";
            //}

            //imbAdd.Enabled = true;//(!lsbFatorRisco.Items.Count.Equals(0) && !exame.Id.Equals(0));
            //imbAddAll.Enabled = true; //(!lsbFatorRisco.Items.Count.Equals(0) && !exame.Id.Equals(0));
        }


        private void PopulaLsbFatorRiscoSel()
        {
            lsbFatorRiscoSel.DataSource = new FatorRisco().GetIdNome("Nome", "IdFatorRisco IN (SELECT IdFatorRisco FROM FatorRiscoClinico WHERE IdClinico=" + exame.Id + ")");

            lsbFatorRiscoSel.DataValueField = "Id";
            lsbFatorRiscoSel.DataTextField = "Nome";
            lsbFatorRiscoSel.DataBind();

            //if (lsbFatorRiscoSel.Items.Count.Equals(0))
            //{
            //    imbRemove.ImageUrl = "img/leftDisabled.jpg";
            //    imbRemoveAll.ImageUrl = "img/leftAllDisabled.jpg";
            //}
            //else
            //{
            imbRemove.ImageUrl = "img/left.jpg";
            imbRemoveAll.ImageUrl = "img/leftAllEnabled.jpg";
            //}

            //imbRemove.Enabled = true; // !lsbFatorRiscoSel.Items.Count.Equals(0);
            //imbRemoveAll.Enabled = true; // !lsbFatorRiscoSel.Items.Count.Equals(0);
        }

        private void PopulaDropDownExame(int IndSaude)
        {
            ddlTipoExame.DataSource = new ExameDicionario().Find("IndExame=" + IndSaude + " AND IndSaude=1 ORDER BY Descricao");
            ddlTipoExame.DataTextField = "Descricao";
            ddlTipoExame.DataValueField = "Id";
            ddlTipoExame.DataBind();
            ddlTipoExame.Items.Insert(0, new ListItem("Selecione o tipo", "0"));
        }

        //#endregion

        //#region ClientCode

        private void RegisterClientCode()
        {
            Guid strAux = Guid.NewGuid();

            if (exame.Id != 0)
                lkbPCI.Attributes.Add("onClick", "javascript:" + strOpenReport("PCMSO", "AnotacoesPCI.aspx?IliteraSystem=" + strAux.ToString() + strAux.ToString()
                    + "&IdExame=" + exame.Id + "&IdEmpresa=" + Request["IdEmpresa"] + "&IdEmpregado=" + Request["IdEmpregado"] + "&IdUsuario=" + Request["IdUsuario"], "AnotacoesPCI"));
            else
                lkbPCI.Attributes.Add("onClick", "javascript:window.alert('É necessário salvar o exame antes de emitir o Anotações para PCI!');");

            //if (exame.IndResultado != (int)ResultadoExame.NaoRealizado && exame.IndResultado != (int)ResultadoExame.EmEspera)
            lkbASO.Attributes.Add("onClick", "javascript:" + strOpenReport("PCMSO", "ASOEmpregado.aspx?IliteraSystem=" + strAux.ToString() + strAux.ToString()
                + "&IdExame=" + exame.Id + "&IdEmpresa=" + Request["IdEmpresa"] + "&IdEmpregado=" + Request["IdEmpregado"] + "&IdUsuario=" + Request["IdUsuario"], "ASO"));
            //else
            //    lkbASO.Attributes.Add("onClick", "javascript:window.alert('É necessário colocar o resultado do exame e salvá-lo (Apto ou Inapto) antes de emitir o ASO!');");

            lkbAnamnese.Attributes.Add("onClick", "javascript:" + strOpenReport("PCMSO", "AnamneseDinamica.aspx?IliteraSystem=" + strAux.ToString() + strAux.ToString()
                + "&IdExame=" + exame.Id, "Anamnese"));

            lkbAnamneseBranco.Attributes.Add("onClick", "javascript:" + strOpenReport("PCMSO", "AnamneseDinamicaBranco.aspx?IliteraSystem=" + strAux.ToString() + strAux.ToString()
                + "&IdExame=" + exame.Id, "Anamnese"));


            btnNovoFatorRisco.Attributes.Add("onClick", "javascript:addItemPop(centerWin('CadFatorRisco.aspx?IdEmpresa=" + Request["IdEmpresa"] + "&IdUsuario=" + Request["IdUsuario"] + "', 480, 320, 'CadQueixaClinica'))");
            lbtAusente.Attributes.Add("onClick", "javascript:return(confirm('Atenção! O Exame Clínico será excluído e cadastrado como exame não realizado por motivo de falta! Deseja realmente continuar?'))");
            btnExcluir.Attributes.Add("onClick", "javascript:return(confirm('Deseja mesmo excluir este exame?'))");
            btnResetText.Attributes.Add("onClick", "javascript:return(confirm('Atenção! Ao atualizar as anotações, todas as informações adicionais digitadas serão perdidas! O texto padrão referente aos itens selecionados na Anamnese e no Exame Físico será gerado e substituirá o atual! Deseja continuar?'))");

            btnOK.Attributes.Add("onClick", "javascript:return VerificaProcesso()");

            ckbQueixaS.Attributes.Add("onClick", "ConsisteCkeckBoxDeAlteracao(this)");
            ckbQueixaN.Attributes.Add("onClick", "ConsisteCkeckBoxDeAlteracao(this)");
            //ckbDeficienciaN.Attributes.Add("onClick", "ConsisteCkeckBoxDeAlteracao(this)");
            //ckbDeficienciaS.Attributes.Add("onClick", "ConsisteCkeckBoxDeAlteracao(this)");
            //ckbEtilismoN.Attributes.Add("onClick", "ConsisteCkeckBoxDeAlteracao(this)");
            //ckbEtilismoS.Attributes.Add("onClick", "ConsisteCkeckBoxDeAlteracao(this)");
            //ckbTraumaN.Attributes.Add("onClick", "ConsisteCkeckBoxDeAlteracao(this)");
            //ckbTraumaS.Attributes.Add("onClick", "ConsisteCkeckBoxDeAlteracao(this)");
            //ckbTabagismoN.Attributes.Add("onClick", "ConsisteCkeckBoxDeAlteracao(this)");
            //ckbTabagismoS.Attributes.Add("onClick", "ConsisteCkeckBoxDeAlteracao(this)");
            //ckbCirurgiaN.Attributes.Add("onClick", "ConsisteCkeckBoxDeAlteracao(this)");
            //ckbCirurgiaS.Attributes.Add("onClick", "ConsisteCkeckBoxDeAlteracao(this)");
            //ckbMedicamentosN.Attributes.Add("onClick", "ConsisteCkeckBoxDeAlteracao(this)");
            //ckbMedicamentosS.Attributes.Add("onClick", "ConsisteCkeckBoxDeAlteracao(this)");
            //ckbDoencaN.Attributes.Add("onClick", "ConsisteCkeckBoxDeAlteracao(this)");
            //ckbDoencaS.Attributes.Add("onClick", "ConsisteCkeckBoxDeAlteracao(this)");
            //ckbAnteFamiN.Attributes.Add("onClick", "ConsisteCkeckBoxDeAlteracao(this)");
            //ckbAnteFamiS.Attributes.Add("onClick", "ConsisteCkeckBoxDeAlteracao(this)");
            //ckbAfastadoN.Attributes.Add("onClick", "ConsisteCkeckBoxDeAlteracao(this)");
            //ckbAfastadoS.Attributes.Add("onClick", "ConsisteCkeckBoxDeAlteracao(this)");
            //ckbMIN.Attributes.Add("onClick", "ConsisteCkeckBoxDeAlteracao(this)");
            //ckbMIS.Attributes.Add("onClick", "ConsisteCkeckBoxDeAlteracao(this)");
            //ckbCoracaoN.Attributes.Add("onClick", "ConsisteCkeckBoxDeAlteracao(this)");
            //ckbCoracaoS.Attributes.Add("onClick", "ConsisteCkeckBoxDeAlteracao(this)");
            //ckbMSN.Attributes.Add("onClick", "ConsisteCkeckBoxDeAlteracao(this)");
            //ckbMSS.Attributes.Add("onClick", "ConsisteCkeckBoxDeAlteracao(this)");
            //ckbTireoide.Attributes.Add("onClick", "ConsisteCkeckBoxDeAlteracao(this)");
            //chkCabecaNormal.Attributes.Add("onClick", "ConsisteCkeckBoxDeAlteracao(this)");
            //ckbAbdomemN.Attributes.Add("onClick", "ConsisteCkeckBoxDeAlteracao(this)");
            //ckbAbdomemS.Attributes.Add("onClick", "ConsisteCkeckBoxDeAlteracao(this)");
            //ckbOsteoN.Attributes.Add("onClick", "ConsisteCkeckBoxDeAlteracao(this)");
            //ckbOsteoS.Attributes.Add("onClick", "ConsisteCkeckBoxDeAlteracao(this)");
            //ckbPulmoesN.Attributes.Add("onClick", "ConsisteCkeckBoxDeAlteracao(this)");
            //ckbPulmoesS.Attributes.Add("onClick", "ConsisteCkeckBoxDeAlteracao(this)");
            //ckbDescoradas.Attributes.Add("onClick", "ConsisteCkeckBoxDeAlteracao(this)");
            //ckbPeleNormal.Attributes.Add("onClick", "ConsisteCkeckBoxDeAlteracao(this)");
        }

        //#endregion

        //#region PopulaExame

        private void PopulaAnamnese()
        {

            if (chk_apt_Altura.Checked)
                exame.apt_Trabalho_Altura = "1";
            else if (chk_inapt_Altura.Checked)
                exame.apt_Trabalho_Altura = "0";
            else
                exame.apt_Trabalho_Altura = "";

            if (chk_apt_Confinado.Checked)
                exame.apt_Espaco_Confinado = "1";
            else if (chk_inapt_Confinado.Checked)
                exame.apt_Espaco_Confinado = "0";
            else
                exame.apt_Espaco_Confinado = "";

            if (chk_apt_Transportes.Checked)
                exame.apt_Transporte = "1";
            else if (chk_inapt_Transportes.Checked)
                exame.apt_Transporte = "0";
            else
                exame.apt_Transporte = "";

            if (chk_apt_Submerso.Checked)
                exame.apt_Submerso = "1";
            else if (chk_inapt_Submerso.Checked)
                exame.apt_Submerso = "0";
            else
                exame.apt_Submerso = "";

            if (chk_apt_Eletricidade.Checked)
                exame.apt_Eletricidade = "1";
            else if (chk_inapt_Eletricidade.Checked)
                exame.apt_Eletricidade = "0";
            else
                exame.apt_Eletricidade = "";

            if (chk_apt_Aquaviarios.Checked)
                exame.apt_Aquaviario = "1";
            else if (chk_inapt_Aquaviarios.Checked)
                exame.apt_Aquaviario = "0";
            else
                exame.apt_Aquaviario = "";

            if (chk_apt_Alimento.Checked)
                exame.apt_Alimento = "1";
            else if (chk_inapt_Alimento.Checked)
                exame.apt_Alimento = "0";
            else
                exame.apt_Alimento = "";

            if (chk_Apt_Brigadista.Checked)
                exame.apt_Brigadista = "1";
            else if (chk_Inapt_Brigadista.Checked)
                exame.apt_Brigadista = "0";
            else
                exame.apt_Brigadista = "";

            if (chk_Apt_Radiacao.Checked)
                exame.apt_Radiacao = "1";
            else if (chk_Inapt_Radiacao.Checked)
                exame.apt_Radiacao = "0";
            else
                exame.apt_Radiacao = "";


            if (chk_Apt_Socorrista.Checked)
                exame.apt_Socorrista = "1";
            else if (chk_Inapt_Socorrista.Checked)
                exame.apt_Socorrista = "0";
            else
                exame.apt_Socorrista = "";

            if (chk_Apt_Respirador.Checked)
                exame.apt_Respirador = "1";
            else if (chk_Inapt_Respirador.Checked)
                exame.apt_Respirador = "0";
            else
                exame.apt_Respirador = "";


            if (ckbQueixaN.Checked)
                exame.Anamnese.HasQueixasAtuais = (int)StatusAnamnese.Nao;
            else if (ckbQueixaS.Checked)
                exame.Anamnese.HasQueixasAtuais = (int)StatusAnamnese.Sim;
            else
                exame.Anamnese.HasQueixasAtuais = (int)StatusAnamnese.NaoPreenchido;

            if (ckbTraumatismoN.Checked)
                exame.Anamnese.HasTraumatismos = (int)StatusAnamnese.Nao;
            else if (ckbTraumatismoS.Checked)
                exame.Anamnese.HasTraumatismos = (int)StatusAnamnese.Sim;
            else
                exame.Anamnese.HasTraumatismos = (int)StatusAnamnese.NaoPreenchido;

            if (ckbAfastamentoN.Checked)
                exame.Anamnese.HasAfastamento = (int)StatusAnamnese.Nao;
            else if (ckbAfastamentoS.Checked)
                exame.Anamnese.HasAfastamento = (int)StatusAnamnese.Sim;
            else
                exame.Anamnese.HasAfastamento = (int)StatusAnamnese.NaoPreenchido;

            if (ckbCirurgiaN.Checked)
                exame.Anamnese.HasCirurgia = (int)StatusAnamnese.Nao;
            else if (ckbCirurgiaS.Checked)
                exame.Anamnese.HasCirurgia = (int)StatusAnamnese.Sim;
            else
                exame.Anamnese.HasCirurgia = (int)StatusAnamnese.NaoPreenchido;

            if (ckbTabagismoN.Checked)
                exame.Anamnese.HasTabagismo = (int)StatusAnamnese.Nao;
            else if (ckbTabagismoS.Checked)
                exame.Anamnese.HasTabagismo = (int)StatusAnamnese.Sim;
            else
                exame.Anamnese.HasTabagismo = (int)StatusAnamnese.NaoPreenchido;

            if (ckbAlcoolismoN.Checked)
                exame.Anamnese.HasAlcoolismo = (int)StatusAnamnese.Nao;
            else if (ckbAlcoolismoS.Checked)
                exame.Anamnese.HasAlcoolismo = (int)StatusAnamnese.Sim;
            else
                exame.Anamnese.HasAlcoolismo = (int)StatusAnamnese.NaoPreenchido;

            if (ckbMedicacoesN.Checked)
                exame.Anamnese.HasMedicacoes = (int)StatusAnamnese.Nao;
            else if (ckbMedicacoesS.Checked)
                exame.Anamnese.HasMedicacoes = (int)StatusAnamnese.Sim;
            else
                exame.Anamnese.HasMedicacoes = (int)StatusAnamnese.NaoPreenchido;

            if (ckbDoencaCronicaN.Checked)
                exame.Anamnese.HasDoencaCronica = (int)StatusAnamnese.Nao;
            else if (ckbDoencaCronicaS.Checked)
                exame.Anamnese.HasDoencaCronica = (int)StatusAnamnese.Sim;
            else
                exame.Anamnese.HasDoencaCronica = (int)StatusAnamnese.NaoPreenchido;


            if (ckbBronquiteN.Checked)
                exame.Anamnese.HasBronquite = (int)StatusAnamnese.Nao;
            else if (ckbBronquiteS.Checked)
                exame.Anamnese.HasBronquite = (int)StatusAnamnese.Sim;
            else
                exame.Anamnese.HasBronquite = (int)StatusAnamnese.NaoPreenchido;

            if (ckbDigestivaN.Checked)
                exame.Anamnese.HasDigestiva = (int)StatusAnamnese.Nao;
            else if (ckbDigestivaS.Checked)
                exame.Anamnese.HasDigestiva = (int)StatusAnamnese.Sim;
            else
                exame.Anamnese.HasDigestiva = (int)StatusAnamnese.NaoPreenchido;

            if (ckbEstomagoN.Checked)
                exame.Anamnese.HasEstomago = (int)StatusAnamnese.Nao;
            else if (ckbEstomagoS.Checked)
                exame.Anamnese.HasEstomago = (int)StatusAnamnese.Sim;
            else
                exame.Anamnese.HasEstomago = (int)StatusAnamnese.NaoPreenchido;

            if (ckbEnxergaN.Checked)
                exame.Anamnese.HasEnxerga = (int)StatusAnamnese.Nao;
            else if (ckbEnxergaS.Checked)
                exame.Anamnese.HasEnxerga = (int)StatusAnamnese.Sim;
            else
                exame.Anamnese.HasEnxerga = (int)StatusAnamnese.NaoPreenchido;

            if (ckbDorCabecaN.Checked)
                exame.Anamnese.HasDorCabeca = (int)StatusAnamnese.Nao;
            else if (ckbDorCabecaS.Checked)
                exame.Anamnese.HasDorCabeca = (int)StatusAnamnese.Sim;
            else
                exame.Anamnese.HasDorCabeca = (int)StatusAnamnese.NaoPreenchido;

            if (ckbDesmaioN.Checked)
                exame.Anamnese.HasDesmaio = (int)StatusAnamnese.Nao;
            else if (ckbDesmaioS.Checked)
                exame.Anamnese.HasDesmaio = (int)StatusAnamnese.Sim;
            else
                exame.Anamnese.HasDesmaio = (int)StatusAnamnese.NaoPreenchido;

            if (ckbDoencaCoracaoN.Checked)
                exame.Anamnese.HasCoracao = (int)StatusAnamnese.Nao;
            else if (ckbDoencaCoracaoS.Checked)
                exame.Anamnese.HasCoracao = (int)StatusAnamnese.Sim;
            else
                exame.Anamnese.HasCoracao = (int)StatusAnamnese.NaoPreenchido;

            if (ckbUrinariaN.Checked)
                exame.Anamnese.HasUrinaria = (int)StatusAnamnese.Nao;
            else if (ckbUrinariaS.Checked)
                exame.Anamnese.HasUrinaria = (int)StatusAnamnese.Sim;
            else
                exame.Anamnese.HasUrinaria = (int)StatusAnamnese.NaoPreenchido;

            if (ckbDiabetesN.Checked)
                exame.Anamnese.HasDiabetes = (int)StatusAnamnese.Nao;
            else if (ckbDiabetesS.Checked)
                exame.Anamnese.HasDiabetes = (int)StatusAnamnese.Sim;
            else
                exame.Anamnese.HasDiabetes = (int)StatusAnamnese.NaoPreenchido;

            if (ckbGripadoN.Checked)
                exame.Anamnese.HasGripado = (int)StatusAnamnese.Nao;
            else if (ckbGripadoS.Checked)
                exame.Anamnese.HasGripado = (int)StatusAnamnese.Sim;
            else
                exame.Anamnese.HasGripado = (int)StatusAnamnese.NaoPreenchido;

            if (ckbEscutaN.Checked)
                exame.Anamnese.HasEscuta = (int)StatusAnamnese.Nao;
            else if (ckbEscutaS.Checked)
                exame.Anamnese.HasEscuta = (int)StatusAnamnese.Sim;
            else
                exame.Anamnese.HasEscuta = (int)StatusAnamnese.NaoPreenchido;

            if (ckbDoresCostaN.Checked)
                exame.Anamnese.HasDoresCosta = (int)StatusAnamnese.Nao;
            else if (ckbDoresCostaS.Checked)
                exame.Anamnese.HasDoresCosta = (int)StatusAnamnese.Sim;
            else
                exame.Anamnese.HasDoresCosta = (int)StatusAnamnese.NaoPreenchido;

            if (ckbReumatismoN.Checked)
                exame.Anamnese.HasReumatismo = (int)StatusAnamnese.Nao;
            else if (ckbReumatismoS.Checked)
                exame.Anamnese.HasReumatismo = (int)StatusAnamnese.Sim;
            else
                exame.Anamnese.HasReumatismo = (int)StatusAnamnese.NaoPreenchido;

            if (ckbAlergiaN.Checked)
                exame.Anamnese.HasAlergia = (int)StatusAnamnese.Nao;
            else if (ckbAlergiaS.Checked)
                exame.Anamnese.HasAlergia = (int)StatusAnamnese.Sim;
            else
                exame.Anamnese.HasAlergia = (int)StatusAnamnese.NaoPreenchido;

            if (ckbEsporteN.Checked)
                exame.Anamnese.HasEsporte = (int)StatusAnamnese.Nao;
            else if (ckbEsporteS.Checked)
                exame.Anamnese.HasEsporte = (int)StatusAnamnese.Sim;
            else
                exame.Anamnese.HasEsporte = (int)StatusAnamnese.NaoPreenchido;

            if (ckbAcidentouN.Checked)
                exame.Anamnese.HasAcidentou = (int)StatusAnamnese.Nao;
            else if (ckbAcidentouS.Checked)
                exame.Anamnese.HasAcidentou = (int)StatusAnamnese.Sim;
            else
                exame.Anamnese.HasAcidentou = (int)StatusAnamnese.NaoPreenchido;

            if (ckbAFHipertensaoN.Checked)
                exame.Anamnese.Has_AF_Hipertensao = (int)StatusAnamnese.Nao;
            else if (ckbAFHipertensaoS.Checked)
                exame.Anamnese.Has_AF_Hipertensao = (int)StatusAnamnese.Sim;
            else
                exame.Anamnese.Has_AF_Hipertensao = (int)StatusAnamnese.NaoPreenchido;

            if (ckbAFDiabetesN.Checked)
                exame.Anamnese.Has_AF_Diabetes = (int)StatusAnamnese.Nao;
            else if (ckbAFDiabetesS.Checked)
                exame.Anamnese.Has_AF_Diabetes = (int)StatusAnamnese.Sim;
            else
                exame.Anamnese.Has_AF_Diabetes = (int)StatusAnamnese.NaoPreenchido;

            if (ckbAFCoracaoN.Checked)
                exame.Anamnese.Has_AF_Coracao = (int)StatusAnamnese.Nao;
            else if (ckbAFCoracaoS.Checked)
                exame.Anamnese.Has_AF_Coracao = (int)StatusAnamnese.Sim;
            else
                exame.Anamnese.Has_AF_Coracao = (int)StatusAnamnese.NaoPreenchido;

            if (ckbAFDerramesN.Checked)
                exame.Anamnese.Has_AF_Derrames = (int)StatusAnamnese.Nao;
            else if (ckbAFDerramesS.Checked)
                exame.Anamnese.Has_AF_Derrames = (int)StatusAnamnese.Sim;
            else
                exame.Anamnese.Has_AF_Derrames = (int)StatusAnamnese.NaoPreenchido;

            if (ckbAFObesidadeN.Checked)
                exame.Anamnese.Has_AF_Obesidade = (int)StatusAnamnese.Nao;
            else if (ckbAFObesidadeS.Checked)
                exame.Anamnese.Has_AF_Obesidade = (int)StatusAnamnese.Sim;
            else
                exame.Anamnese.Has_AF_Obesidade = (int)StatusAnamnese.NaoPreenchido;

            if (ckbAFCancerN.Checked)
                exame.Anamnese.Has_AF_Cancer = (int)StatusAnamnese.Nao;
            else if (ckbAFCancerS.Checked)
                exame.Anamnese.Has_AF_Cancer = (int)StatusAnamnese.Sim;
            else
                exame.Anamnese.Has_AF_Cancer = (int)StatusAnamnese.NaoPreenchido;

            if (ckbAFColesterolN.Checked)
                exame.Anamnese.Has_AF_Colesterol = (int)StatusAnamnese.Nao;
            else if (ckbAFColesterolS.Checked)
                exame.Anamnese.Has_AF_Colesterol = (int)StatusAnamnese.Sim;
            else
                exame.Anamnese.Has_AF_Colesterol = (int)StatusAnamnese.NaoPreenchido;

            if (ckbAFPsiquiatricosN.Checked)
                exame.Anamnese.Has_AF_Psiquiatricos = (int)StatusAnamnese.Nao;
            else if (ckbAFPsiquiatricosS.Checked)
                exame.Anamnese.Has_AF_Psiquiatricos = (int)StatusAnamnese.Sim;
            else
                exame.Anamnese.Has_AF_Psiquiatricos = (int)StatusAnamnese.NaoPreenchido;



            //if (ckbAfastadoN.Checked)
            //    exame.Anamnese.HasAfastamento = (int)StatusAnamnese.Nao;
            //else if (ckbAfastadoS.Checked)
            //    exame.Anamnese.HasAfastamento = (int)StatusAnamnese.Sim;
            //if (ckbAnteFamiN.Checked)
            //    exame.Anamnese.HasAntecedentes = (int)StatusAnamnese.Nao;
            //else if (ckbAnteFamiS.Checked)
            //    exame.Anamnese.HasAntecedentes = (int)StatusAnamnese.Sim;
            //if (ckbDoencaN.Checked)
            //    exame.Anamnese.HasDoencaCronica = (int)StatusAnamnese.Nao;
            //else if (ckbDoencaS.Checked)
            //    exame.Anamnese.HasDoencaCronica = (int)StatusAnamnese.Sim;
            //if (ckbCirurgiaN.Checked)
            //    exame.Anamnese.HasCirurgia = (int)StatusAnamnese.Nao;
            //else if (ckbCirurgiaS.Checked)
            //    exame.Anamnese.HasCirurgia = (int)StatusAnamnese.Sim;
            //if (ckbTraumaN.Checked)
            //    exame.Anamnese.HasTraumatismos = (int)StatusAnamnese.Nao;
            //else if (ckbTraumaS.Checked)
            //    exame.Anamnese.HasTraumatismos = (int)StatusAnamnese.Sim;
            //if (ckbDeficienciaN.Checked)
            //    exame.Anamnese.HasDeficienciaFisica = (int)StatusAnamnese.Nao;
            //else if (ckbDeficienciaS.Checked)
            //    exame.Anamnese.HasDeficienciaFisica = (int)StatusAnamnese.Sim;
            //if (ckbMedicamentosN.Checked)
            //    exame.Anamnese.HasMedicacoes = (int)StatusAnamnese.Nao;
            //else if (ckbMedicamentosS.Checked)
            //    exame.Anamnese.HasMedicacoes = (int)StatusAnamnese.Sim;
            //if (ckbTabagismoN.Checked)
            //    exame.Anamnese.HasTabagismo = (int)StatusAnamnese.Nao;
            //else if (ckbTabagismoS.Checked)
            //    exame.Anamnese.HasTabagismo = (int)StatusAnamnese.Sim;
            //if (ckbEtilismoN.Checked)
            //    exame.Anamnese.HasAlcoolismo = (int)StatusAnamnese.Nao;
            //else if (ckbEtilismoS.Checked)
            //    exame.Anamnese.HasAlcoolismo = (int)StatusAnamnese.Sim;
        }

        private void PopulaExameFisico()
        {
            System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");

            if (wdtDUM.Text.Trim() != "") exame.Fisico.DataUltimaMenstruacao = System.Convert.ToDateTime(wdtDUM.Text, ptBr);



            string userInput = wneAltura.Text.Trim();
            int result;
            bool isValidNumber = Int32.TryParse(userInput, out result);
            if (isValidNumber == false) wneAltura.Text = "0";

            userInput = wnePeso.Text.Trim();
            isValidNumber = Int32.TryParse(userInput, out result);
            if (isValidNumber == false) wnePeso.Text = "0";

            userInput = txtCircunferencia.Text.Trim();
            isValidNumber = Int32.TryParse(userInput, out result);
            if (isValidNumber == false)  txtCircunferencia.Text = "0";

            userInput = wnePulso.Text.Trim();
            isValidNumber = Int32.TryParse(userInput, out result);
            if (isValidNumber == false) wnePulso.Text = "0";

            userInput = txtSistolica.Text.Trim();
            isValidNumber = Int32.TryParse(userInput, out result);
            if (isValidNumber == false) txtSistolica.Text = "0";

            userInput = txtDiastolica.Text.Trim();
            isValidNumber = Int32.TryParse(userInput, out result);
            if (isValidNumber == false) txtDiastolica.Text = "0";


            if (wneAltura.Text.Trim() != "") exame.Fisico.Altura = System.Convert.ToInt32(wneAltura.Text);

            exame.Fisico.PressaoArterial = "";

            if (txtDiastolica.Text.Trim() == "") txtDiastolica.Text = "0";
            if (txtSistolica.Text.Trim() == "") txtSistolica.Text = "0";

            exame.Fisico.Diastolica= System.Convert.ToInt16( txtDiastolica.Text.Trim() );
            exame.Fisico.Sistolica= System.Convert.ToInt16(txtSistolica.Text.Trim());
            

            if (wnePulso.Text.Trim() != "") exame.Fisico.Pulso = System.Convert.ToInt32(wnePulso.Text);

            if (wnePeso.Text.Trim() != "") exame.Fisico.Peso = System.Convert.ToInt32(wnePeso.Text);

            exame.Fisico.CircunferenciaAbdominal = txtCircunferencia.Text.Trim();


            exame.Fisico.AP_Penultima_Empresa = txt_Penultima_Empresa.Text;
            exame.Fisico.AP_Penultima_Funcao = txt_Penultima_Funcao.Text;
            exame.Fisico.AP_Penultima_Tempo = txt_Penultima_Tempo.Text;

            exame.Fisico.AP_Ultima_Empresa = txt_Ultima_Empresa.Text;
            exame.Fisico.AP_Ultima_Funcao = txt_Ultima_Funcao.Text;
            exame.Fisico.AP_Ultima_Tempo = txt_Ultima_Tempo.Text;


            exame.Fisico.PeleAnexosAlterado = txtPele.Text;            
            exame.Fisico.CabecaAlterado = txtCabeca.Text;
            exame.Fisico.CoracaoAlterado = txtCoracao.Text;
            exame.Fisico.PulmaoAlterado = txtPulmoes.Text;
            exame.Fisico.AbdomemAlterado = txtAbdomem.Text;
            exame.Fisico.MSAlterado = txtOmbros.Text;
            exame.Fisico.MIAlterado = txtMI.Text;
            exame.Fisico.ColunaVertebralAlterado = txtColuna.Text;
            exame.Fisico.MaosPunhosAlterado = txtMaos.Text;
            exame.Fisico.OlhosAlterado = txtOlhos.Text;

            //exame.Fisico.MVsemRAAlterado= txtMVsemRA.Text;
            //exame.Fisico.BRNFAlterado= txtBRNF.Text;
            //exame.Fisico.PlanoAlterado = txtPlano.Text;
            //exame.Fisico.DBAlterado = txtDBA.Text;
            //exame.Fisico.RHAAlterado = txtRHA.Text;
            //exame.Fisico.IndolorAlterado = txtIndolor.Text;
            //exame.Fisico.AbaulamentosAlterado = txtAbaulamento.Text;
            //exame.Fisico.ColunaVertebralAlterado = txtColunaVertebral.Text;

            if ( ckbCabecaNormal.Checked)
                exame.Fisico.hasCabecaNormal=(int)StatusAnamnese.Sim;
            else
                exame.Fisico.hasCabecaNormal = (int)StatusAnamnese.Nao;

            if (ckbTireoide.Checked)
                exame.Fisico.hasTireoide= (int)StatusAnamnese.Sim;
            else
                exame.Fisico.hasTireoide = (int)StatusAnamnese.Nao;

            if (ckbDente.Checked)
                exame.Fisico.hasDente= (int)StatusAnamnese.Sim;
            else
                exame.Fisico.hasDente = (int)StatusAnamnese.Nao;

            if (ckbGarganta.Checked)
                exame.Fisico.hasGarganta= (int)StatusAnamnese.Sim;
            else
                exame.Fisico.hasGarganta= (int)StatusAnamnese.Nao;

            if (ckbAdenomegalia.Checked)
                exame.Fisico.hasAdenomegalia= (int)StatusAnamnese.Sim;
            else
                exame.Fisico.hasAdenomegalia = (int)StatusAnamnese.Nao;

            if (ckbOuvido.Checked)
                exame.Fisico.hasOuvido= (int)StatusAnamnese.Sim;
            else
                exame.Fisico.hasOuvido= (int)StatusAnamnese.Nao;

            if (ckbOlhosNormal.Checked)
                exame.Fisico.hasOlhosNormal= (int)StatusAnamnese.Sim;
            else
                exame.Fisico.hasOlhosNormal= (int)StatusAnamnese.Nao;

            if ( ckbOculos.Checked)
                exame.Fisico.hasOculosLentes= (int)StatusAnamnese.Sim;
            else
                exame.Fisico.hasOculosLentes= (int)StatusAnamnese.Nao;

            if (ckbPeleNormal.Checked)
                exame.Fisico.hasPeleNormal= (int)StatusAnamnese.Sim;
            else
                exame.Fisico.hasPeleNormal= (int)StatusAnamnese.Nao;

            if (ckbDescoradas.Checked)
                exame.Fisico.hasDescoradas= (int)StatusAnamnese.Sim;
            else
                exame.Fisico.hasDescoradas= (int)StatusAnamnese.Nao;

            if (ckbIctericas.Checked)
                exame.Fisico.hasIctericas= (int)StatusAnamnese.Sim;
            else
                exame.Fisico.hasIctericas= (int)StatusAnamnese.Nao;

            if (ckbCianoticas.Checked)
                exame.Fisico.hasCianoticas= (int)StatusAnamnese.Sim;
            else
                exame.Fisico.hasCianoticas= (int)StatusAnamnese.Nao;

            if (ckbDermatoses.Checked)
                exame.Fisico.hasDermatoses= (int)StatusAnamnese.Sim;
            else
                exame.Fisico.hasDermatoses = (int)StatusAnamnese.Nao;

            if (ckbMVsemRA.Checked)
                exame.Fisico.hasMVsemRA= (int)StatusAnamnese.Sim;
            else
                exame.Fisico.hasMVsemRA= (int)StatusAnamnese.Nao;

            if (ckbMVAlterado.Checked)
                exame.Fisico.hasMVAlterado= (int)StatusAnamnese.Sim;
            else
                exame.Fisico.hasMVAlterado = (int)StatusAnamnese.Nao;

            if (ckbRuidos.Checked)
                exame.Fisico.hasAdventicios= (int)StatusAnamnese.Sim;
            else
                exame.Fisico.hasAdventicios= (int)StatusAnamnese.Nao;

            if (ckbBRNF.Checked)
                exame.Fisico.hasBRNF= (int)StatusAnamnese.Sim;
            else
                exame.Fisico.hasBRNF= (int)StatusAnamnese.Nao;

            if (ckbArritmias.Checked)
                exame.Fisico.hasArritmias = (int)StatusAnamnese.Sim;
            else
                exame.Fisico.hasArritmias = (int)StatusAnamnese.Nao;

            if (ckbSopros.Checked)
                exame.Fisico.hasSopros= (int)StatusAnamnese.Sim;
            else
                exame.Fisico.hasSopros = (int)StatusAnamnese.Nao;

            if (ckbCardioVascularOutros.Checked)
                exame.Fisico.hasCardioOutros = (int)StatusAnamnese.Sim;
            else
                exame.Fisico.hasCardioOutros = (int)StatusAnamnese.Nao;

            if (ckbPlano.Checked)
                exame.Fisico.hasPlano = (int)StatusAnamnese.Sim;
            else
                exame.Fisico.hasPlano = (int)StatusAnamnese.Nao;

            if (ckbPalpacao.Checked)
                exame.Fisico.hasPalpacao= (int)StatusAnamnese.Sim;
            else
                exame.Fisico.hasPalpacao = (int)StatusAnamnese.Nao;

            if (ckbHepatoesplenomegalia.Checked)
                exame.Fisico.hasHepatoesplenomegalia= (int)StatusAnamnese.Sim;
            else
                exame.Fisico.hasHepatoesplenomegalia= (int)StatusAnamnese.Nao;

            if (ckbMassas.Checked)
                exame.Fisico.hasMassas = (int)StatusAnamnese.Sim;
            else
                exame.Fisico.hasMassas = (int)StatusAnamnese.Nao;

            if (ckbMINormal.Checked)
                exame.Fisico.hasMINormal = (int)StatusAnamnese.Sim;
            else
                exame.Fisico.hasMINormal = (int)StatusAnamnese.Nao;

            if (ckbEdemas.Checked)
                exame.Fisico.hasEdemas= (int)StatusAnamnese.Sim;
            else
                exame.Fisico.hasEdemas = (int)StatusAnamnese.Nao;

            if (ckbPulsos.Checked)
                exame.Fisico.hasPulsos= (int)StatusAnamnese.Sim;
            else
                exame.Fisico.hasPulsos = (int)StatusAnamnese.Nao;

            if (ckbArticular.Checked)
                exame.Fisico.hasArticular = (int)StatusAnamnese.Sim;
            else
                exame.Fisico.hasArticular = (int)StatusAnamnese.Nao;

            if (ckbVarizes.Checked)
                exame.Fisico.hasVarizes= (int)StatusAnamnese.Sim;
            else
                exame.Fisico.hasVarizes = (int)StatusAnamnese.Nao;

            if (ckbOmbrosNormal.Checked)
                exame.Fisico.hasMSNormal = (int)StatusAnamnese.Sim;
            else
                exame.Fisico.hasMSNormal = (int)StatusAnamnese.Nao;

            if (ckbRotacao.Checked)
                exame.Fisico.hasDorRotacao= (int)StatusAnamnese.Sim;
            else
                exame.Fisico.hasDorRotacao = (int)StatusAnamnese.Nao;

            if (ckbPalpacaoBiceps.Checked)
                exame.Fisico.hasPalpacaoBiceps= (int)StatusAnamnese.Sim;
            else
                exame.Fisico.hasPalpacaoBiceps= (int)StatusAnamnese.Nao;

            if (ckbPalpacaoSubacromial.Checked)
                exame.Fisico.hasPalpacaoSubacromial = (int)StatusAnamnese.Sim;
            else
                exame.Fisico.hasPalpacaoSubacromial = (int)StatusAnamnese.Nao;

            if (ckbMaosNormal.Checked)
                exame.Fisico.hasMaosNormal= (int)StatusAnamnese.Sim;
            else
                exame.Fisico.hasMaosNormal = (int)StatusAnamnese.Nao;

            if (ckbTinelD.Checked)
                exame.Fisico.hasTinelD= (int)StatusAnamnese.Sim;
            else
                exame.Fisico.hasTinelD = (int)StatusAnamnese.Nao;
            
            if (ckbTinelE.Checked)
                exame.Fisico.hasTinelE = (int)StatusAnamnese.Sim;
            else
                exame.Fisico.hasTinelE = (int)StatusAnamnese.Nao;

            if (ckbPrensaoD.Checked)
                exame.Fisico.hasPreensaoD= (int)StatusAnamnese.Sim;
            else
                exame.Fisico.hasPreensaoD = (int)StatusAnamnese.Nao;

            if (ckbPrensaoE.Checked)
                exame.Fisico.hasPreensaoE = (int)StatusAnamnese.Sim;
            else
                exame.Fisico.hasPreensaoE = (int)StatusAnamnese.Nao;

            if (ckbFinkelsteinD.Checked)
                exame.Fisico.hasFinkelsteinD= (int)StatusAnamnese.Sim;
            else
                exame.Fisico.hasFinkelsteinD = (int)StatusAnamnese.Nao;

            if (ckbFinkelsteinE.Checked)
                exame.Fisico.hasFinkelsteinE = (int)StatusAnamnese.Sim;
            else
                exame.Fisico.hasFinkelsteinE = (int)StatusAnamnese.Nao;

            if ( ckbFalenD.Checked)
                exame.Fisico.hasPhalenD= (int)StatusAnamnese.Sim;
            else
                exame.Fisico.hasPhalenD = (int)StatusAnamnese.Nao;

            if (ckbFalenE.Checked)
                exame.Fisico.hasPhalenE = (int)StatusAnamnese.Sim;
            else
                exame.Fisico.hasPhalenE = (int)StatusAnamnese.Nao;
            
            if (ckbNodulosD.Checked)
                exame.Fisico.hasNodulosD= (int)StatusAnamnese.Sim;
            else
                exame.Fisico.hasNodulosD = (int)StatusAnamnese.Nao;

            if (ckbNodulosE.Checked)
                exame.Fisico.hasNodulosE = (int)StatusAnamnese.Sim;
            else
                exame.Fisico.hasNodulosE = (int)StatusAnamnese.Nao;

            if (ckbCrepitacaoD.Checked)
                exame.Fisico.hasCrepitacaoD= (int)StatusAnamnese.Sim;
            else
                exame.Fisico.hasCrepitacaoD = (int)StatusAnamnese.Nao;

            if (ckbCrepitacaoE.Checked)
                exame.Fisico.hasCrepitacaoE = (int)StatusAnamnese.Sim;
            else
                exame.Fisico.hasCrepitacaoE = (int)StatusAnamnese.Nao;

            if (ckbEdemaD.Checked)
                exame.Fisico.hasEdemaD= (int)StatusAnamnese.Sim;
            else
                exame.Fisico.hasEdemaD = (int)StatusAnamnese.Nao;

            if (ckbEdemaE.Checked)
                exame.Fisico.hasEdemaE = (int)StatusAnamnese.Sim;
            else
                exame.Fisico.hasEdemaE = (int)StatusAnamnese.Nao;

            if (ckbColunaNormal.Checked)
                exame.Fisico.hasColunaNormal= (int)StatusAnamnese.Sim;
            else
                exame.Fisico.hasColunaNormal= (int)StatusAnamnese.Nao;

            if (ckbCifose.Checked)
                exame.Fisico.hasCifose= (int)StatusAnamnese.Sim;
            else
                exame.Fisico.hasCifose = (int)StatusAnamnese.Nao;
            
            if (ckbLordose.Checked)
                exame.Fisico.hasLordose= (int)StatusAnamnese.Sim;
            else
                exame.Fisico.hasLordose = (int)StatusAnamnese.Nao;

            if (ckbEscoliose.Checked)
                exame.Fisico.hasEscoliose= (int)StatusAnamnese.Sim;
            else
                exame.Fisico.hasEscoliose = (int)StatusAnamnese.Nao;

            if (ckbLasegue.Checked)
                exame.Fisico.hasLasegue= (int)StatusAnamnese.Sim;
            else
                exame.Fisico.hasLasegue = (int)StatusAnamnese.Nao;

            if (ckbFlexao.Checked)
                exame.Fisico.hasLimitacaoFlexao= (int)StatusAnamnese.Sim;
            else
                exame.Fisico.hasLimitacaoFlexao = (int)StatusAnamnese.Nao;



            ////deixar "normal" selecionado caso nenhum esteja selecionado
            //if (ckbDescoradas.Checked == false && ckbIctericas.Checked == false && ckbCianoticas.Checked == false && ckbDermatoses.Checked == false)
            //{
            //    exame.Fisico.hasPeleNormal = (int)StatusAnamnese.Sim;
            //    ckbPeleNormal.Checked = true;
            //}

            //if (ckbOculos.Checked == false)
            //{
            //    exame.Fisico.hasOlhosNormal = (int)StatusAnamnese.Sim;
            //    ckbOlhosNormal.Checked = true;
            //}

            //if (ckbTireoide.Checked == false && ckbDente.Checked == false && ckbGarganta.Checked == false && ckbAdenomegalia.Checked == false && ckbOuvido.Checked == false)
            //{
            //    exame.Fisico.hasCabecaNormal = (int)StatusAnamnese.Sim;
            //    ckbCabecaNormal.Checked = true;
            //}

            //if (ckbRotacao.Checked == false && ckbPalpacaoBiceps.Checked == false && ckbPalpacaoSubacromial.Checked == false)
            //{
            //    exame.Fisico.hasMSNormal = (int)StatusAnamnese.Sim;
            //    ckbOmbrosNormal.Checked = true;
            //}

            //if (ckbEdemas.Checked == false && ckbPulsos.Checked == false && ckbArticular.Checked == false && ckbVarizes.Checked == false)
            //{
            //    exame.Fisico.hasMINormal = (int)StatusAnamnese.Sim;
            //    ckbMINormal.Checked = true;
            //}

            //if (ckbCifose.Checked == false && ckbLordose.Checked == false && ckbEscoliose.Checked == false && ckbLasegue.Checked == false && ckbFlexao.Checked == false)
            //{
            //    exame.Fisico.hasColunaNormal = (int)StatusAnamnese.Sim;
            //    ckbColunaNormal.Checked = true;
            //}

            //if (ckbTinelD.Checked == false && ckbTinelE.Checked == false && ckbFinkelsteinD.Checked == false && ckbFinkelsteinE.Checked == false && ckbNodulosD.Checked == false && ckbNodulosE.Checked == false && ckbEdemaD.Checked == false && ckbEdemaE.Checked == false && ckbPrensaoD.Checked == false && ckbPrensaoE.Checked == false && ckbFalenD.Checked == false && ckbFalenE.Checked == false && ckbCrepitacaoD.Checked == false && ckbCrepitacaoE.Checked == false)
            //{
            //    exame.Fisico.hasMaosNormal = (int)StatusAnamnese.Sim;
            //    ckbMaosNormal.Checked = true;
            //}

            //if (ckbSopros.Checked == false && ckbArritmias.Checked == false && ckbCardioVascularOutros.Checked == false)
            //{
            //    exame.Fisico.hasBRNF = (int)StatusAnamnese.Sim;
            //    ckbBRNF.Checked = true;
            //}

            //if (ckbMVAlterado.Checked == false && ckbRuidos.Checked == false)
            //{
            //    exame.Fisico.hasMVsemRA = (int)StatusAnamnese.Sim;
            //    ckbMVsemRA.Checked = true;
            //}

            //if (ckbPalpacao.Checked == false && ckbMassas.Checked == false && ckbHepatoesplenomegalia.Checked == false)
            //{
            //    exame.Fisico.hasPlano = (int)StatusAnamnese.Sim;
            //    ckbPlano.Checked = true;
            //}



        }

        private void PopulaExame()
        {
            //if (!cliente.ContrataPCMSO.Equals((int)TipoPcmsoContratada.NaoContratada))
            //{
            //if (!usuario.NomeUsuario.Equals("Admin"))
            //{

            //if (exame.IdJuridica.Id == 0)
            //{
            //    Entities.Usuario usuario = (Entities.Usuario)Session["usuarioLogado"];
            //    prestador.Find(" IdPessoa = " + usuario.IdPessoa.ToString());
            //    //exame.IdMedico.Id = prestador.Id;
            //    exame.IdJuridica.Id = prestador.IdJuridica.Id;
            //}
            //}
            //else
            //    exame.IdJuridica = cliente;
            PopulaAnamnese();
            PopulaExameFisico();

            if (txtAnotacoes.Text.ToString().Length > 1000)
                exame.Prontuario = txtAnotacoes.Text.ToString().Substring(0, 1000);
            else
                exame.Prontuario = txtAnotacoes.Text;

            if (txtObs.Text.ToString().Length > 255)
                exame.Observacao = txtObs.Text.ToString().Substring(0, 255);
            else
                exame.Observacao = txtObs.Text;




            if (exame.apt_Espaco_Confinado == null || exame.apt_Espaco_Confinado.ToString().Trim() == "")
            {
                chk_apt_Confinado.Checked = false;
                chk_inapt_Confinado.Checked = false;
            }
            else if (exame.apt_Espaco_Confinado.ToString() == "1" || exame.apt_Espaco_Confinado.ToString().Trim().ToUpper() == "TRUE")
                chk_apt_Confinado.Checked = true;
            else if (exame.apt_Espaco_Confinado.ToString() == "0" || exame.apt_Espaco_Confinado.ToString().Trim().ToUpper() == "FALSE")
                chk_inapt_Confinado.Checked = true;
            else
            {
                chk_apt_Confinado.Checked = false;
                chk_inapt_Confinado.Checked = false;
            }


            if (exame.apt_Trabalho_Altura == null || exame.apt_Trabalho_Altura.ToString().Trim() == "")
            {
                chk_apt_Altura.Checked = false;
                chk_inapt_Altura.Checked = false;
            }
            else if (exame.apt_Trabalho_Altura.ToString() == "1" || exame.apt_Trabalho_Altura.ToString().Trim().ToUpper() == "TRUE")
                chk_apt_Altura.Checked = true;
            else if (exame.apt_Trabalho_Altura.ToString() == "0" || exame.apt_Trabalho_Altura.ToString().Trim().ToUpper() == "FALSE")
                chk_inapt_Altura.Checked = true;
            else
            {
                chk_apt_Altura.Checked = false;
                chk_inapt_Altura.Checked = false;
            }

            if (exame.apt_Transporte == null || exame.apt_Transporte.ToString().Trim() == "")
            {
                chk_apt_Transportes.Checked = false;
                chk_inapt_Transportes.Checked = false;
            }
            else if (exame.apt_Transporte.ToString() == "1" || exame.apt_Transporte.ToString().Trim().ToUpper() == "TRUE")
                chk_apt_Transportes.Checked = true;
            else if (exame.apt_Transporte.ToString() == "0" || exame.apt_Transporte.ToString().Trim().ToUpper() == "FALSE")
                chk_inapt_Transportes.Checked = true;
            else
            {
                chk_apt_Transportes.Checked = false;
                chk_inapt_Transportes.Checked = false;
            }

            if (exame.apt_Eletricidade == null || exame.apt_Eletricidade.ToString().Trim() == "")
            {
                chk_apt_Eletricidade.Checked = false;
                chk_inapt_Eletricidade.Checked = false;
            }
            else if (exame.apt_Eletricidade.ToString() == "1" || exame.apt_Eletricidade.ToString().Trim().ToUpper() == "TRUE")
                chk_apt_Eletricidade.Checked = true;
            else if (exame.apt_Eletricidade.ToString() == "0" || exame.apt_Eletricidade.ToString().Trim().ToUpper() == "FALSE")
                chk_inapt_Eletricidade.Checked = true;
            else
            {
                chk_apt_Eletricidade.Checked = false;
                chk_inapt_Eletricidade.Checked = false;
            }

            if (exame.apt_Submerso == null || exame.apt_Submerso.ToString().Trim() == "")
            {
                chk_apt_Submerso.Checked = false;
                chk_inapt_Submerso.Checked = false;
            }
            else if (exame.apt_Submerso.ToString() == "1" || exame.apt_Submerso.ToString().Trim().ToUpper() == "TRUE")
                chk_apt_Submerso.Checked = true;
            else if (exame.apt_Submerso.ToString() == "0" || exame.apt_Submerso.ToString().Trim().ToUpper() == "FALSE")
                chk_inapt_Submerso.Checked = true;
            else
            {
                chk_apt_Submerso.Checked = false;
                chk_inapt_Submerso.Checked = false;
            }

            if (exame.apt_Aquaviario == null || exame.apt_Aquaviario.ToString().Trim() == "")
            {
                chk_apt_Aquaviarios.Checked = false;
                chk_inapt_Aquaviarios.Checked = false;
            }
            else if (exame.apt_Aquaviario.ToString() == "1" || exame.apt_Aquaviario.ToString().Trim().ToUpper() == "TRUE")
                chk_apt_Aquaviarios.Checked = true;
            else if (exame.apt_Aquaviario.ToString() == "0" || exame.apt_Aquaviario.ToString().Trim().ToUpper() == "FALSE")
                chk_inapt_Aquaviarios.Checked = true;
            else
            {
                chk_apt_Aquaviarios.Checked = false;
                chk_inapt_Aquaviarios.Checked = false;
            }

            if (exame.apt_Alimento == null || exame.apt_Alimento.ToString().Trim() == "")
            {
                chk_apt_Alimento.Checked = false;
                chk_inapt_Alimento.Checked = false;
            }
            else if (exame.apt_Alimento.ToString() == "1" || exame.apt_Alimento.ToString().Trim().ToUpper() == "TRUE")
                chk_apt_Alimento.Checked = true;
            else if (exame.apt_Alimento.ToString() == "0" || exame.apt_Alimento.ToString().Trim().ToUpper() == "FALSE")
                chk_inapt_Alimento.Checked = true;
            else
            {
                chk_apt_Alimento.Checked = false;
                chk_inapt_Alimento.Checked = false;
            }

            if (exame.apt_Brigadista == null || exame.apt_Brigadista.ToString().Trim() == "")
            {
                chk_Apt_Brigadista.Checked = false;
                chk_Inapt_Brigadista.Checked = false;
            }
            else if (exame.apt_Brigadista.ToString() == "1" || exame.apt_Brigadista.ToString().Trim().ToUpper() == "TRUE")
                chk_Apt_Brigadista.Checked = true;
            else if (exame.apt_Brigadista.ToString() == "0" || exame.apt_Brigadista.ToString().Trim().ToUpper() == "FALSE")
                chk_Inapt_Brigadista.Checked = true;
            else
            {
                chk_Apt_Brigadista.Checked = false;
                chk_Inapt_Brigadista.Checked = false;
            }

            if (exame.apt_Radiacao == null || exame.apt_Radiacao.ToString().Trim() == "")
            {
                chk_Apt_Radiacao.Checked = false;
                chk_Inapt_Radiacao.Checked = false;
            }
            else if (exame.apt_Radiacao.ToString() == "1" || exame.apt_Radiacao.ToString().Trim().ToUpper() == "TRUE")
                chk_Apt_Radiacao.Checked = true;
            else if (exame.apt_Radiacao.ToString() == "0" || exame.apt_Radiacao.ToString().Trim().ToUpper() == "FALSE")
                chk_Inapt_Radiacao.Checked = true;
            else
            {
                chk_Apt_Radiacao.Checked = false;
                chk_Inapt_Radiacao.Checked = false;
            }


            if (exame.apt_Socorrista == null || exame.apt_Socorrista.ToString().Trim() == "")
            {
                chk_Apt_Socorrista.Checked = false;
                chk_Inapt_Socorrista.Checked = false;
            }
            else if (exame.apt_Socorrista.ToString() == "1" || exame.apt_Socorrista.ToString().Trim().ToUpper() == "TRUE")
                chk_Apt_Socorrista.Checked = true;
            else if (exame.apt_Socorrista.ToString() == "0" || exame.apt_Socorrista.ToString().Trim().ToUpper() == "FALSE")
                chk_Inapt_Socorrista.Checked = true;
            else
            {
                chk_Apt_Socorrista.Checked = false;
                chk_Inapt_Socorrista.Checked = false;
            }


            if (exame.apt_Respirador == null || exame.apt_Respirador.ToString().Trim() == "")
            {
                chk_Apt_Respirador.Checked = false;
                chk_Inapt_Respirador.Checked = false;
            }
            else if (exame.apt_Respirador.ToString() == "1" || exame.apt_Respirador.ToString().Trim().ToUpper() == "TRUE")
                chk_Apt_Respirador.Checked = true;
            else if (exame.apt_Respirador.ToString() == "0" || exame.apt_Respirador.ToString().Trim().ToUpper() == "FALSE")
                chk_Inapt_Respirador.Checked = true;
            else
            {
                chk_Apt_Respirador.Checked = false;
                chk_Inapt_Respirador.Checked = false;
            }


            if (exame.IdEmpregadoFuncao.Id.Equals(0) || exame.IdPcmso.Id.Equals(0))
            {

                Cliente cliente;
                cliente = new Cliente(System.Convert.ToInt32(Session["Empresa"].ToString()));

                exame.IdEmpregadoFuncao = EmpregadoFuncao.GetEmpregadoFuncao( cliente, empregado);
                exame.IdPcmso = exame.IdEmpregadoFuncao.nID_EMPR.GetUltimoPcmso();
                exame.IdMedico.Id = 1111;  //Coordenador PCMSO não contratada
            }
            //}
            //else
            //{
            //exame.IdMedico.Id = 1111;//Coordenador PCMSO não contratada
            //exame.IdJuridica.Id = 310;//Mestra Paulista
            //}

            exame.IdExameDicionario.Id = Convert.ToInt32(ddlTipoExame.SelectedItem.Value);

            //if (wdtDataExame.Date.Hour.Equals(0) && wdtDataExame.Date.Minute.Equals(0) && wdtDataExame.Date.Second.Equals(0))
            //    exame.DataExame = wdtDataExame.Date.AddHours(DateTime.Now.Hour).AddMinutes(DateTime.Now.Minute).AddSeconds(DateTime.Now.Second);
            //else

            System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");
            exame.DataExame = System.Convert.ToDateTime(wdtDataExame.Text, ptBr);


            
            if (ddlClinica.SelectedIndex > 0)
            {
                exame.IdJuridica.Find(Convert.ToInt32(ddlClinica.SelectedValue));
            }
            else
            {
                Entities.Usuario usuario = (Entities.Usuario)Session["usuarioLogado"];
                prestador.Find(" IdPessoa = " + usuario.IdPessoa.ToString());
                //exame.IdMedico.Id = prestador.Id;
                exame.IdJuridica.Id = prestador.IdJuridica.Id;
            }

            if (ddlMedico.SelectedIndex > 0)
            {
                exame.IdMedico.Find(Convert.ToInt32(ddlMedico.SelectedValue));
            }
            else
            {
                exame.IdMedico.Id = 1111;
                exame.IdMedico.Find();
            }


            try
            {
                exame.IndResultado = Convert.ToInt16(rblResultado.SelectedItem.Value);
            }
            catch
            {
                // new Exception("O resultado do exame deve ser colocado!");
                MsgBox1.Show("Ilitera.Net", "O resultado do exame deve ser colocado", null,
                new EO.Web.MsgBoxButton("OK"));

            }
        }

        //#endregion

        //#region PopulaTelaExame

        private void PopulaTelaExame()
        {
            wdtDataExame.Text = exame.DataExame.ToString("dd/MM/yyyy");

            ddlTipoExame.ClearSelection();
            ddlTipoExame.Items.FindByValue(exame.IdExameDicionario.Id.ToString().Trim()).Selected = true;

            rblResultado.ClearSelection();
            if (exame.IndResultado != 0)
                rblResultado.Items.FindByValue(exame.IndResultado.ToString()).Selected = true;

            lblDemissao.Text = "";

            if ( exame.IdExameDicionario.Id==2)
            {

                if (exame.DataDemissao != null && exame.DataDemissao.ToString().Trim() != "" && exame.DataDemissao != new DateTime())
                {
                    lbl_Demissao2.Visible = true;
                    lblDemissao.Visible = true;

                    System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");
                    lblDemissao.Text = exame.DataDemissao.ToString("dd/MM/yyyy", ptBr);
                }
            }

            if (!exame.Id.Equals(0))
            {
                PopulaDDLClinicas(exame.IdExameDicionario.Id.ToString());

                try
                {
                    ddlClinica.ClearSelection();
                    ddlClinica.Items.FindByValue(exame.IdJuridica.Id.ToString()).Selected = true;
                }
                catch
                {
                    exame.IdJuridica.Find();
                    ddlClinica.Items.Insert(1, new ListItem(exame.IdJuridica.NomeAbreviado, exame.IdJuridica.Id.ToString()));
                    ddlClinica.Items[1].Selected = true;
                }
            }

            PopulaDDLMedico();

            try
            {
                ddlMedico.ClearSelection();
                ddlMedico.Items.FindByValue(exame.IdMedico.Id.ToString()).Selected = true;
            }
            catch
            {
                exame.IdMedico.Find();
                ddlMedico.Items.Insert(1, new ListItem(exame.IdMedico.NomeCompleto, exame.IdMedico.Id.ToString()));
                ddlMedico.Items[1].Selected = true;
            }

            if (chk_PacienteCritico.Visible == true)
            {
                if (exame.Paciente_Critico == true) chk_PacienteCritico.Checked = true;
                else chk_PacienteCritico.Checked = false;
            }
            else
                chk_PacienteCritico.Checked = false;



            ProntuarioDigital xProntuario = new ProntuarioDigital();
            xProntuario.Find("IdExameBase = " + exame.Id.ToString() + " ");

            if (xProntuario.Arquivo.Trim() != "")
            {
                txt_Arq.Text = xProntuario.Arquivo.Trim();
                txt_Arq.ReadOnly = true;
            }
            else
            {
                txt_Arq.Text = "";
                txt_Arq.ReadOnly = true;
            }


            //chk_apt_Confinado.Checked = exame.apt_Espaco_Confinado;
            //chk_apt_Altura.Checked = exame.apt_Trabalho_Altura;
            //chk_apt_Transportes.Checked = exame.apt_Transporte;
            //chk_apt_Submerso.Checked = exame.apt_Submerso;
            //chk_apt_Eletricidade.Checked = exame.apt_Eletricidade;
            //chk_apt_Aquaviarios.Checked = exame.apt_Aquaviario;
            //chk_Apt_Alimento.Checked = exame.apt_Alimento;
            //chk_Apt_Brigadista.Checked = exame.apt_Brigadista;
            //chk_Apt_Socorrista.Checked = exame.apt_Socorrista;

            if (exame.apt_Espaco_Confinado == null || exame.apt_Espaco_Confinado.ToString().Trim() == "")
            {
                chk_apt_Confinado.Checked = false;
                chk_inapt_Confinado.Checked = false;
            }
            else if (exame.apt_Espaco_Confinado.ToString() == "1" || exame.apt_Espaco_Confinado.ToString().Trim().ToUpper() == "TRUE")
                chk_apt_Confinado.Checked = true;
            else if (exame.apt_Espaco_Confinado.ToString() == "0" || exame.apt_Espaco_Confinado.ToString().Trim().ToUpper() == "FALSE")
                chk_inapt_Confinado.Checked = true;
            else
            {
                chk_apt_Confinado.Checked = false;
                chk_inapt_Confinado.Checked = false;
            }


            if (exame.apt_Trabalho_Altura == null || exame.apt_Trabalho_Altura.ToString().Trim() == "")
            {
                chk_apt_Altura.Checked = false;
                chk_inapt_Altura.Checked = false;
            }
            else if (exame.apt_Trabalho_Altura.ToString() == "1" || exame.apt_Trabalho_Altura.ToString().Trim().ToUpper() == "TRUE")
                chk_apt_Altura.Checked = true;
            else if (exame.apt_Trabalho_Altura.ToString() == "0" || exame.apt_Trabalho_Altura.ToString().Trim().ToUpper() == "FALSE")
                chk_inapt_Altura.Checked = true;
            else
            {
                chk_apt_Altura.Checked = false;
                chk_inapt_Altura.Checked = false;
            }

            if (exame.apt_Transporte == null || exame.apt_Transporte.ToString().Trim() == "")
            {
                chk_apt_Transportes.Checked = false;
                chk_inapt_Transportes.Checked = false;
            }
            else if (exame.apt_Transporte.ToString() == "1" || exame.apt_Transporte.ToString().Trim().ToUpper() == "TRUE")
                chk_apt_Transportes.Checked = true;
            else if (exame.apt_Transporte.ToString() == "0" || exame.apt_Transporte.ToString().Trim().ToUpper() == "FALSE")
                chk_inapt_Transportes.Checked = true;
            else
            {
                chk_apt_Transportes.Checked = false;
                chk_inapt_Transportes.Checked = false;
            }

            if (exame.apt_Eletricidade == null || exame.apt_Eletricidade.ToString().Trim() == "")
            {
                chk_apt_Eletricidade.Checked = false;
                chk_inapt_Eletricidade.Checked = false;
            }
            else if (exame.apt_Eletricidade.ToString() == "1" || exame.apt_Eletricidade.ToString().Trim().ToUpper() == "TRUE")
                chk_apt_Eletricidade.Checked = true;
            else if (exame.apt_Eletricidade.ToString() == "0" || exame.apt_Eletricidade.ToString().Trim().ToUpper() == "FALSE")
                chk_inapt_Eletricidade.Checked = true;
            else
            {
                chk_apt_Eletricidade.Checked = false;
                chk_inapt_Eletricidade.Checked = false;
            }

            if (exame.apt_Submerso == null || exame.apt_Submerso.ToString().Trim() == "")
            {
                chk_apt_Submerso.Checked = false;
                chk_inapt_Submerso.Checked = false;
            }
            else if (exame.apt_Submerso.ToString() == "1" || exame.apt_Submerso.ToString().Trim().ToUpper() == "TRUE")
                chk_apt_Submerso.Checked = true;
            else if (exame.apt_Submerso.ToString() == "0" || exame.apt_Submerso.ToString().Trim().ToUpper() == "FALSE")
                chk_inapt_Submerso.Checked = true;
            else
            {
                chk_apt_Submerso.Checked = false;
                chk_inapt_Submerso.Checked = false;
            }

            if (exame.apt_Aquaviario == null || exame.apt_Aquaviario.ToString().Trim() == "")
            {
                chk_apt_Aquaviarios.Checked = false;
                chk_inapt_Aquaviarios.Checked = false;
            }
            else if (exame.apt_Aquaviario.ToString() == "1" || exame.apt_Aquaviario.ToString().Trim().ToUpper() == "TRUE")
                chk_apt_Aquaviarios.Checked = true;
            else if (exame.apt_Aquaviario.ToString() == "0" || exame.apt_Aquaviario.ToString().Trim().ToUpper() == "FALSE")
                chk_inapt_Aquaviarios.Checked = true;
            else
            {
                chk_apt_Aquaviarios.Checked = false;
                chk_inapt_Aquaviarios.Checked = false;
            }

            if (exame.apt_Alimento == null || exame.apt_Alimento.ToString().Trim() == "")
            {
                chk_apt_Alimento.Checked = false;
                chk_inapt_Alimento.Checked = false;
            }
            else if (exame.apt_Alimento.ToString() == "1" || exame.apt_Alimento.ToString().Trim().ToUpper() == "TRUE")
                chk_apt_Alimento.Checked = true;
            else if (exame.apt_Alimento.ToString() == "0" || exame.apt_Alimento.ToString().Trim().ToUpper() == "FALSE")
                chk_inapt_Alimento.Checked = true;
            else
            {
                chk_apt_Alimento.Checked = false;
                chk_inapt_Alimento.Checked = false;
            }

            if (exame.apt_Brigadista == null || exame.apt_Brigadista.ToString().Trim() == "")
            {
                chk_Apt_Brigadista.Checked = false;
                chk_Inapt_Brigadista.Checked = false;
            }
            else if (exame.apt_Brigadista.ToString() == "1" || exame.apt_Brigadista.ToString().Trim().ToUpper() == "TRUE")
                chk_Apt_Brigadista.Checked = true;
            else if (exame.apt_Brigadista.ToString() == "0" || exame.apt_Brigadista.ToString().Trim().ToUpper() == "FALSE")
                chk_Inapt_Brigadista.Checked = true;
            else
            {
                chk_Apt_Brigadista.Checked = false;
                chk_Inapt_Brigadista.Checked = false;
            }


            if (exame.apt_Radiacao == null || exame.apt_Radiacao.ToString().Trim() == "")
            {
                chk_Apt_Radiacao.Checked = false;
                chk_Inapt_Radiacao.Checked = false;
            }
            else if (exame.apt_Radiacao.ToString() == "1" || exame.apt_Radiacao.ToString().Trim().ToUpper() == "TRUE")
                chk_Apt_Radiacao.Checked = true;
            else if (exame.apt_Radiacao.ToString() == "0" || exame.apt_Radiacao.ToString().Trim().ToUpper() == "FALSE")
                chk_Inapt_Radiacao.Checked = true;
            else
            {
                chk_Apt_Radiacao.Checked = false;
                chk_Inapt_Radiacao.Checked = false;
            }


            if (exame.apt_Socorrista == null || exame.apt_Socorrista.ToString().Trim() == "")
            {
                chk_Apt_Socorrista.Checked = false;
                chk_Inapt_Socorrista.Checked = false;
            }
            else if (exame.apt_Socorrista.ToString() == "1" || exame.apt_Socorrista.ToString().Trim().ToUpper() == "TRUE")
                chk_Apt_Socorrista.Checked = true;
            else if (exame.apt_Socorrista.ToString() == "0" || exame.apt_Socorrista.ToString().Trim().ToUpper() == "FALSE")
                chk_Inapt_Socorrista.Checked = true;
            else
            {
                chk_Apt_Socorrista.Checked = false;
                chk_Inapt_Socorrista.Checked = false;
            }


            if (exame.apt_Respirador == null || exame.apt_Respirador.ToString().Trim() == "")
            {
                chk_Apt_Respirador.Checked = false;
                chk_Inapt_Respirador.Checked = false;
            }
            else if (exame.apt_Respirador.ToString() == "1" || exame.apt_Respirador.ToString().Trim().ToUpper() == "TRUE")
                chk_Apt_Respirador.Checked = true;
            else if (exame.apt_Respirador.ToString() == "0" || exame.apt_Respirador.ToString().Trim().ToUpper() == "FALSE")
                chk_Inapt_Respirador.Checked = true;
            else
            {
                chk_Apt_Respirador.Checked = false;
                chk_Inapt_Respirador.Checked = false;
            }




            //if (!cliente.ContrataPCMSO.Equals((int)TipoPcmsoContratada.NaoContratada))
            //{
            if (exame.Anamnese.HasQueixasAtuais == (int)StatusAnamnese.Nao)
                ckbQueixaN.Checked = true;
            else if (exame.Anamnese.HasQueixasAtuais == (int)StatusAnamnese.Sim)
                ckbQueixaS.Checked = true;


            if (exame.Anamnese.HasTraumatismos == (int)StatusAnamnese.Nao)
                ckbTraumatismoN.Checked = true;
            else if (exame.Anamnese.HasTraumatismos == (int)StatusAnamnese.Sim)
                ckbTraumatismoS.Checked = true;

            if (exame.Anamnese.HasAfastamento == (int)StatusAnamnese.Nao)
                ckbAfastamentoN.Checked = true;
            else if (exame.Anamnese.HasAfastamento == (int)StatusAnamnese.Sim)
                ckbAfastamentoS.Checked = true;

            if (exame.Anamnese.HasCirurgia == (int)StatusAnamnese.Nao)
                ckbCirurgiaN.Checked = true;
            else if (exame.Anamnese.HasCirurgia == (int)StatusAnamnese.Sim)
                ckbCirurgiaS.Checked = true;

            if (exame.Anamnese.HasTabagismo == (int)StatusAnamnese.Nao)
                ckbTabagismoN.Checked = true;
            else if (exame.Anamnese.HasTabagismo == (int)StatusAnamnese.Sim)
                ckbTabagismoS.Checked = true;

            if (exame.Anamnese.HasAlcoolismo == (int)StatusAnamnese.Nao)
                ckbAlcoolismoN.Checked = true;
            else if (exame.Anamnese.HasAlcoolismo == (int)StatusAnamnese.Sim)
                ckbAlcoolismoS.Checked = true;

            if (exame.Anamnese.HasMedicacoes == (int)StatusAnamnese.Nao)
                ckbMedicacoesN.Checked = true;
            else if (exame.Anamnese.HasMedicacoes == (int)StatusAnamnese.Sim)
                ckbMedicacoesS.Checked = true;

            if (exame.Anamnese.HasDoencaCronica == (int)StatusAnamnese.Nao)
                ckbDoencaCronicaN.Checked = true;
            else if (exame.Anamnese.HasDoencaCronica == (int)StatusAnamnese.Sim)
                ckbTabagismoS.Checked = true;

            if (exame.Anamnese.HasBronquite == (int)StatusAnamnese.Nao)
                ckbBronquiteN.Checked = true;
            else if (exame.Anamnese.HasBronquite == (int)StatusAnamnese.Sim)
                ckbBronquiteS.Checked = true;

            if (exame.Anamnese.HasDigestiva == (int)StatusAnamnese.Nao)
                ckbDigestivaN.Checked = true;
            else if (exame.Anamnese.HasDigestiva == (int)StatusAnamnese.Sim)
                ckbDigestivaS.Checked = true;

            if (exame.Anamnese.HasEstomago == (int)StatusAnamnese.Nao)
                ckbEstomagoN.Checked = true;
            else if (exame.Anamnese.HasEstomago == (int)StatusAnamnese.Sim)
                ckbEstomagoS.Checked = true;

            if (exame.Anamnese.HasEnxerga == (int)StatusAnamnese.Nao)
                ckbEnxergaN.Checked = true;
            else if (exame.Anamnese.HasEnxerga == (int)StatusAnamnese.Sim)
                ckbEnxergaS.Checked = true;

            if (exame.Anamnese.HasDorCabeca == (int)StatusAnamnese.Nao)
                ckbDorCabecaN.Checked = true;
            else if (exame.Anamnese.HasDorCabeca == (int)StatusAnamnese.Sim)
                ckbDorCabecaS.Checked = true;

            if (exame.Anamnese.HasDesmaio == (int)StatusAnamnese.Nao)
                ckbDesmaioN.Checked = true;
            else if (exame.Anamnese.HasDesmaio == (int)StatusAnamnese.Sim)
                ckbDesmaioS.Checked = true;

            if (exame.Anamnese.HasCoracao == (int)StatusAnamnese.Nao)
                ckbDoencaCoracaoN.Checked = true;
            else if (exame.Anamnese.HasCoracao == (int)StatusAnamnese.Sim)
                ckbDoencaCoracaoS.Checked = true;

            if (exame.Anamnese.HasUrinaria == (int)StatusAnamnese.Nao)
                ckbUrinariaN.Checked = true;
            else if (exame.Anamnese.HasUrinaria == (int)StatusAnamnese.Sim)
                ckbUrinariaS.Checked = true;

            if (exame.Anamnese.HasDiabetes == (int)StatusAnamnese.Nao)
                ckbDiabetesN.Checked = true;
            else if (exame.Anamnese.HasDiabetes == (int)StatusAnamnese.Sim)
                ckbDiabetesS.Checked = true;

            if (exame.Anamnese.HasGripado == (int)StatusAnamnese.Nao)
                ckbGripadoN.Checked = true;
            else if (exame.Anamnese.HasGripado == (int)StatusAnamnese.Sim)
                ckbGripadoS.Checked = true;

            if (exame.Anamnese.HasEscuta == (int)StatusAnamnese.Nao)
                ckbEscutaN.Checked = true;
            else if (exame.Anamnese.HasEscuta == (int)StatusAnamnese.Sim)
                ckbEscutaS.Checked = true;

            if (exame.Anamnese.HasDoresCosta == (int)StatusAnamnese.Nao)
                ckbDoresCostaN.Checked = true;
            else if (exame.Anamnese.HasDoresCosta == (int)StatusAnamnese.Sim)
                ckbDoresCostaS.Checked = true;

            if (exame.Anamnese.HasReumatismo == (int)StatusAnamnese.Nao)
                ckbReumatismoN.Checked = true;
            else if (exame.Anamnese.HasReumatismo == (int)StatusAnamnese.Sim)
                ckbReumatismoS.Checked = true;

            if (exame.Anamnese.HasAlergia == (int)StatusAnamnese.Nao)
                ckbAlergiaN.Checked = true;
            else if (exame.Anamnese.HasAlergia == (int)StatusAnamnese.Sim)
                ckbAlergiaS.Checked = true;

            if (exame.Anamnese.HasEsporte == (int)StatusAnamnese.Nao)
                ckbEsporteN.Checked = true;
            else if (exame.Anamnese.HasEsporte == (int)StatusAnamnese.Sim)
                ckbEsporteS.Checked = true;

            if (exame.Anamnese.HasAcidentou == (int)StatusAnamnese.Nao)
                ckbAcidentouN.Checked = true;
            else if (exame.Anamnese.HasAcidentou == (int)StatusAnamnese.Sim)
                ckbAcidentouS.Checked = true;

            if (exame.Anamnese.Has_AF_Hipertensao == (int)StatusAnamnese.Nao)
                ckbAFHipertensaoN.Checked = true;
            else if (exame.Anamnese.Has_AF_Hipertensao == (int)StatusAnamnese.Sim)
                ckbAFHipertensaoS.Checked = true;

            if (exame.Anamnese.Has_AF_Diabetes == (int)StatusAnamnese.Nao)
                ckbAFDiabetesN.Checked = true;
            else if (exame.Anamnese.Has_AF_Diabetes == (int)StatusAnamnese.Sim)
                ckbAFDiabetesS.Checked = true;

            if (exame.Anamnese.Has_AF_Coracao == (int)StatusAnamnese.Nao)
                ckbAFCoracaoN.Checked = true;
            else if (exame.Anamnese.Has_AF_Coracao == (int)StatusAnamnese.Sim)
                ckbAFCoracaoS.Checked = true;

            if (exame.Anamnese.Has_AF_Derrames == (int)StatusAnamnese.Nao)
                ckbAFDerramesN.Checked = true;
            else if (exame.Anamnese.Has_AF_Derrames == (int)StatusAnamnese.Sim)
                ckbAFDerramesS.Checked = true;

            if (exame.Anamnese.Has_AF_Obesidade == (int)StatusAnamnese.Nao)
                ckbAFObesidadeN.Checked = true;
            else if (exame.Anamnese.Has_AF_Obesidade == (int)StatusAnamnese.Sim)
                ckbAFObesidadeS.Checked = true;

            if (exame.Anamnese.Has_AF_Cancer == (int)StatusAnamnese.Nao)
                ckbAFCancerN.Checked = true;
            else if (exame.Anamnese.Has_AF_Cancer == (int)StatusAnamnese.Sim)
                ckbAFCancerS.Checked = true;

            if (exame.Anamnese.Has_AF_Colesterol == (int)StatusAnamnese.Nao)
                ckbAFColesterolN.Checked = true;
            else if (exame.Anamnese.Has_AF_Colesterol == (int)StatusAnamnese.Sim)
                ckbAFColesterolS.Checked = true;

            if (exame.Anamnese.Has_AF_Psiquiatricos == (int)StatusAnamnese.Nao)
                ckbAFPsiquiatricosN.Checked = true;
            else if (exame.Anamnese.Has_AF_Psiquiatricos == (int)StatusAnamnese.Sim)
                ckbAFPsiquiatricosS.Checked = true;

            

            //if (ckbAFPsiquiatricosN.Checked)
            //    exame.Anamnese.Has_AF_Psiquiatricos = (int)StatusAnamnese.Nao;
            //else if (ckbAFPsiquiatricosS.Checked)
            //    exame.Anamnese.Has_AF_Psiquiatricos = (int)StatusAnamnese.Sim;






            //if (exame.Anamnese.HasAfastamento == (int)StatusAnamnese.Nao)
            //    ckbAfastadoN.Checked = true;
            //else if (exame.Anamnese.HasAfastamento == (int)StatusAnamnese.Sim)
            //    ckbAfastadoS.Checked = true;
            //if (exame.Anamnese.HasAntecedentes == (int)StatusAnamnese.Nao)
            //    ckbAnteFamiN.Checked = true;
            //else if (exame.Anamnese.HasAntecedentes == (int)StatusAnamnese.Sim)
            //    ckbAnteFamiS.Checked = true;
            //if (exame.Anamnese.HasDoencaCronica == (int)StatusAnamnese.Nao)
            //    ckbDoencaN.Checked = true;
            //else if (exame.Anamnese.HasDoencaCronica == (int)StatusAnamnese.Sim)
            //    ckbDoencaS.Checked = true;
            //if (exame.Anamnese.HasCirurgia == (int)StatusAnamnese.Nao)
            //    ckbCirurgiaN.Checked = true;
            //else if (exame.Anamnese.HasCirurgia == (int)StatusAnamnese.Sim)
            //    ckbCirurgiaS.Checked = true;
            //if (exame.Anamnese.HasTraumatismos == (int)StatusAnamnese.Nao)
            //    ckbTraumaN.Checked = true;
            //else if (exame.Anamnese.HasTraumatismos == (int)StatusAnamnese.Sim)
            //    ckbTraumaS.Checked = true;
            //if (exame.Anamnese.HasDeficienciaFisica == (int)StatusAnamnese.Nao)
            //    ckbDeficienciaN.Checked = true;
            //else if (exame.Anamnese.HasDeficienciaFisica == (int)StatusAnamnese.Sim)
            //    ckbDeficienciaS.Checked = true;
            //if (exame.Anamnese.HasMedicacoes == (int)StatusAnamnese.Nao)
            //    ckbMedicamentosN.Checked = true;
            //else if (exame.Anamnese.HasMedicacoes == (int)StatusAnamnese.Sim)
            //    ckbMedicamentosS.Checked = true;
            //if (exame.Anamnese.HasTabagismo == (int)StatusAnamnese.Nao)
            //    ckbTabagismoN.Checked = true;
            //else if (exame.Anamnese.HasTabagismo == (int)StatusAnamnese.Sim)
            //    ckbTabagismoS.Checked = true;
            //if (exame.Anamnese.HasAlcoolismo == (int)StatusAnamnese.Nao)
            //    ckbEtilismoN.Checked = true;
            //else if (exame.Anamnese.HasAlcoolismo == (int)StatusAnamnese.Sim)
            //    ckbEtilismoS.Checked = true;

            wdtDUM.Text = exame.Fisico.DataUltimaMenstruacao.ToString("dd/MM/yyyy");
            wneAltura.Text = exame.Fisico.Altura.ToString();

            txtDiastolica.Text = exame.Fisico.Diastolica.ToString();
            txtSistolica.Text = exame.Fisico.Sistolica.ToString();

            wnePulso.Text = exame.Fisico.Pulso.ToString();
            wnePeso.Text = exame.Fisico.Peso.ToString();

            txtCircunferencia.Text = exame.Fisico.CircunferenciaAbdominal;

            if (exame.Fisico.AP_Penultima_Empresa == null) txt_Penultima_Empresa.Text = "";
            else  txt_Penultima_Empresa.Text = exame.Fisico.AP_Penultima_Empresa.Trim();

            if (exame.Fisico.AP_Penultima_Funcao == null) txt_Penultima_Funcao.Text = "";
            else  txt_Penultima_Funcao.Text = exame.Fisico.AP_Penultima_Funcao.Trim();

            if (exame.Fisico.AP_Penultima_Tempo == null) txt_Penultima_Tempo.Text = "";
            else  txt_Penultima_Tempo.Text = exame.Fisico.AP_Penultima_Tempo.Trim();

            if (exame.Fisico.AP_Ultima_Empresa == null) txt_Ultima_Empresa.Text = "";
            else txt_Ultima_Empresa.Text = exame.Fisico.AP_Ultima_Empresa.Trim();

            if (exame.Fisico.AP_Ultima_Funcao == null) txt_Ultima_Funcao.Text = "";
            else txt_Ultima_Funcao.Text = exame.Fisico.AP_Ultima_Funcao.Trim();

            if (exame.Fisico.AP_Ultima_Tempo == null) txt_Ultima_Tempo.Text = "";
            else txt_Ultima_Tempo.Text = exame.Fisico.AP_Ultima_Tempo.Trim();



            if (exame.Fisico.PeleAnexosAlterado == null) txtPele.Text = "";
            else txtPele.Text = exame.Fisico.PeleAnexosAlterado;
            if (exame.Fisico.CabecaAlterado == null) txtCabeca.Text = "";
            else txtCabeca.Text = exame.Fisico.CabecaAlterado;
            if (exame.Fisico.CoracaoAlterado == null) txtCoracao.Text = "";
            else txtCoracao.Text = exame.Fisico.CoracaoAlterado;
            if (exame.Fisico.PulmaoAlterado== null)  txtPulmoes.Text = "";
            else txtPulmoes.Text = exame.Fisico.PulmaoAlterado;
            if (exame.Fisico.AbdomemAlterado == null) txtAbdomem.Text = "";
            else txtAbdomem.Text = exame.Fisico.AbdomemAlterado;
            if (exame.Fisico.MIAlterado == null) txtMI.Text = "";
            else txtMI.Text = exame.Fisico.MIAlterado;


            if (exame.Fisico.ColunaVertebralAlterado == null) txtColuna.Text = "";
            else txtColuna.Text = exame.Fisico.ColunaVertebralAlterado;
            if (exame.Fisico.OlhosAlterado == null) txtOlhos.Text = "";
            else txtOlhos.Text = exame.Fisico.OlhosAlterado;
            if (exame.Fisico.MIAlterado == null) txtMI.Text = "";
            else txtMI.Text = exame.Fisico.MIAlterado;
            if (exame.Fisico.MSAlterado == null) txtOmbros.Text = "";
            else txtOmbros.Text = exame.Fisico.MSAlterado;
            if (exame.Fisico.MaosPunhosAlterado == null) txtMaos.Text = "";
            else txtMaos.Text = exame.Fisico.MaosPunhosAlterado;




            //if (exame.Fisico.MVsemRAAlterado == null) txtMVsemRA.Text = "";
            //else txtMVsemRA.Text = exame.Fisico.MVsemRAAlterado;
            //if (exame.Fisico.BRNFAlterado == null) txtBRNF.Text = "";
            //else txtBRNF.Text = exame.Fisico.BRNFAlterado;
            //if (exame.Fisico.PlanoAlterado == null) txtPlano.Text = "";
            //else txtPlano.Text = exame.Fisico.PlanoAlterado;
            //if (exame.Fisico.DBAlterado == null) txtDBA.Text = "";
            //else txtDBA.Text = exame.Fisico.DBAlterado;
            //if (exame.Fisico.RHAAlterado == null) txtRHA.Text = "";
            //else txtRHA.Text = exame.Fisico.RHAAlterado;
            //if (exame.Fisico.IndolorAlterado == null) txtIndolor.Text = ""; 
            //else txtIndolor.Text = exame.Fisico.IndolorAlterado;
            //if (exame.Fisico.AbaulamentosAlterado == null) txtAbaulamento.Text = "";
            //else txtAbaulamento.Text = exame.Fisico.AbaulamentosAlterado;



            Calcular_IMC();
            //if (wneAltura.Text.Trim() != "0" && wnePeso.Text.Trim() != "0")
            //    lblIMC.Text = ((double)(System.Convert.ToDouble(wnePeso.Text) / Math.Pow(System.Convert.ToDouble(wneAltura.Text), 2.0))).ToString("0.00");

            if (exame.Fisico.hasCabecaNormal == (int)StatusAnamnese.Sim)
                ckbCabecaNormal.Checked = true;
            else
                ckbCabecaNormal.Checked = false;

            if (exame.Fisico.hasTireoide== (int)StatusAnamnese.Sim)
                ckbTireoide.Checked = true;
            else
                ckbTireoide.Checked = false;

            if (exame.Fisico.hasDente== (int)StatusAnamnese.Sim)
                ckbDente.Checked = true;
            else
                ckbDente.Checked = false;

            if (exame.Fisico.hasGarganta== (int)StatusAnamnese.Sim)
                ckbGarganta.Checked = true;
            else
                ckbGarganta.Checked = false;

            if (exame.Fisico.hasAdenomegalia == (int)StatusAnamnese.Sim)
                ckbAdenomegalia.Checked = true;
            else
                ckbAdenomegalia.Checked = false;

            if (exame.Fisico.hasOuvido == (int)StatusAnamnese.Sim)
                ckbOuvido.Checked = true;
            else
                ckbOuvido.Checked = false;

            if (exame.Fisico.hasOlhosNormal == (int)StatusAnamnese.Sim)
                ckbOlhosNormal.Checked = true;
            else
                ckbOlhosNormal.Checked = false;

            if (exame.Fisico.hasOculosLentes == (int)StatusAnamnese.Sim)
                ckbOculos.Checked = true;
            else
                ckbOculos.Checked = false;

            if (exame.Fisico.hasPeleNormal == (int)StatusAnamnese.Sim)
                ckbPeleNormal.Checked = true;
            else
                ckbPeleNormal.Checked = false;

            if (exame.Fisico.hasDescoradas== (int)StatusAnamnese.Sim)
                ckbDescoradas.Checked = true;
            else
                ckbDescoradas.Checked = false;

            if (exame.Fisico.hasIctericas== (int)StatusAnamnese.Sim)
                ckbIctericas.Checked = true;
            else
                ckbIctericas.Checked = false;

            if (exame.Fisico.hasCianoticas == (int)StatusAnamnese.Sim)
                ckbCianoticas.Checked = true;
            else
                ckbCianoticas.Checked = false;

            if (exame.Fisico.hasDermatoses== (int)StatusAnamnese.Sim)
                ckbDermatoses.Checked = true;
            else
                ckbDermatoses.Checked = false;

            if (exame.Fisico.hasMVsemRA== (int)StatusAnamnese.Sim)
                ckbMVsemRA.Checked = true;
            else
                ckbMVsemRA.Checked = false;

            if (exame.Fisico.hasMVAlterado== (int)StatusAnamnese.Sim)
                ckbMVAlterado.Checked = true;
            else
                ckbMVAlterado.Checked = false;

            if (exame.Fisico.hasAdventicios == (int)StatusAnamnese.Sim)
                ckbRuidos.Checked = true;
            else
                ckbRuidos.Checked = false;

            if (exame.Fisico.hasBRNF == (int)StatusAnamnese.Sim)
                ckbBRNF.Checked = true;
            else
                ckbBRNF.Checked = false;

            if (exame.Fisico.hasArritmias == (int)StatusAnamnese.Sim)
                ckbArritmias.Checked = true;
            else
                ckbArritmias.Checked = false;

            if (exame.Fisico.hasSopros == (int)StatusAnamnese.Sim)
                ckbSopros.Checked = true;
            else
                ckbSopros.Checked = false;

            if (exame.Fisico.hasCardioOutros== (int)StatusAnamnese.Sim)
                ckbCardioVascularOutros.Checked = true;
            else
                ckbCardioVascularOutros.Checked = false;

            if (exame.Fisico.hasPlano == (int)StatusAnamnese.Sim)
                ckbPlano.Checked = true;
            else
                ckbPlano.Checked = false;

            if (exame.Fisico.hasPalpacao == (int)StatusAnamnese.Sim)
                ckbPalpacao.Checked = true;
            else
                ckbPalpacao.Checked = false;
            
            if (exame.Fisico.hasHepatoesplenomegalia == (int)StatusAnamnese.Sim)
                ckbHepatoesplenomegalia.Checked = true;
            else
                ckbHepatoesplenomegalia.Checked = false;

            if (exame.Fisico.hasMassas == (int)StatusAnamnese.Sim)
                ckbMassas.Checked = true;
            else
                ckbMassas.Checked = false;

            if (exame.Fisico.hasMINormal == (int)StatusAnamnese.Sim)
                ckbMINormal.Checked = true;
            else
                ckbMINormal.Checked = false;

            if (exame.Fisico.hasEdemas== (int)StatusAnamnese.Sim)
                ckbEdemas.Checked = true;
            else
                ckbEdemas.Checked = false;

            if (exame.Fisico.hasPulsos == (int)StatusAnamnese.Sim)
                ckbPulsos.Checked = true;
            else
                ckbPulsos.Checked = false;

            if (exame.Fisico.hasArticular == (int)StatusAnamnese.Sim)
                ckbArticular.Checked = true;
            else
                ckbArticular.Checked = false;

            if (exame.Fisico.hasVarizes== (int)StatusAnamnese.Sim)
                ckbVarizes.Checked = true;
            else
                ckbVarizes.Checked = false;

            if (exame.Fisico.hasMSNormal == (int)StatusAnamnese.Sim)
                ckbOmbrosNormal.Checked = true;
            else
                ckbOmbrosNormal.Checked = false;

            if (exame.Fisico.hasDorRotacao == (int)StatusAnamnese.Sim)
                ckbRotacao.Checked = true;
            else
                ckbRotacao.Checked = false;

            if (exame.Fisico.hasPalpacaoBiceps == (int)StatusAnamnese.Sim)
                ckbPalpacaoBiceps.Checked = true;
            else
                ckbPalpacaoBiceps.Checked = false;

            if (exame.Fisico.hasPalpacaoSubacromial== (int)StatusAnamnese.Sim)
                ckbPalpacaoSubacromial.Checked = true;
            else
                ckbPalpacaoSubacromial.Checked = false;

            if (exame.Fisico.hasMaosNormal == (int)StatusAnamnese.Sim)
                ckbMaosNormal.Checked = true;
            else
                ckbMaosNormal.Checked = false;

            if (exame.Fisico.hasTinelD== (int)StatusAnamnese.Sim)
                ckbTinelD.Checked = true;
            else
                ckbTinelD.Checked = false;

            if (exame.Fisico.hasTinelE == (int)StatusAnamnese.Sim)
                ckbTinelE.Checked = true;
            else
                ckbTinelE.Checked = false;

            if (exame.Fisico.hasPreensaoD == (int)StatusAnamnese.Sim)
                ckbPrensaoD.Checked = true;
            else
                ckbPrensaoD.Checked = false;

            if (exame.Fisico.hasPreensaoE == (int)StatusAnamnese.Sim)
                ckbPrensaoE.Checked = true;
            else
                ckbPrensaoE.Checked = false;

            if (exame.Fisico.hasFinkelsteinD == (int)StatusAnamnese.Sim)
                ckbFinkelsteinD.Checked = true;
            else
                ckbFinkelsteinD.Checked = false;

            if (exame.Fisico.hasFinkelsteinE == (int)StatusAnamnese.Sim)
                ckbFinkelsteinE.Checked = true;
            else
                ckbFinkelsteinE.Checked = false;

            if (exame.Fisico.hasPhalenD == (int)StatusAnamnese.Sim)
                ckbFalenD.Checked = true;
            else
                ckbFalenD.Checked = false;

            if (exame.Fisico.hasPhalenE == (int)StatusAnamnese.Sim)
                ckbFalenE.Checked = true;
            else
                ckbFalenE.Checked = false;

            if (exame.Fisico.hasNodulosD == (int)StatusAnamnese.Sim)
                ckbNodulosD.Checked = true;
            else
                ckbNodulosD.Checked = false;

            if (exame.Fisico.hasNodulosE == (int)StatusAnamnese.Sim)
                ckbNodulosE.Checked = true;
            else
                ckbNodulosE.Checked = false;

            if (exame.Fisico.hasCrepitacaoD == (int)StatusAnamnese.Sim)
                ckbCrepitacaoD.Checked = true;
            else
                ckbCrepitacaoD.Checked = false;

            if (exame.Fisico.hasCrepitacaoE == (int)StatusAnamnese.Sim)
                ckbCrepitacaoE.Checked = true;
            else
                ckbCrepitacaoE.Checked = false;

            if (exame.Fisico.hasEdemaD== (int)StatusAnamnese.Sim)
                ckbEdemaD.Checked = true;
            else
                ckbEdemaD.Checked = false;
            
            if (exame.Fisico.hasEdemaE == (int)StatusAnamnese.Sim)
                ckbEdemaE.Checked = true;
            else
                ckbEdemaE.Checked = false;


            if (exame.Fisico.hasColunaNormal == (int)StatusAnamnese.Sim)
                ckbColunaNormal.Checked = true;
            else
                ckbColunaNormal.Checked = false;
            
            if (exame.Fisico.hasCifose== (int)StatusAnamnese.Sim)
                ckbCifose.Checked = true;
            else
                ckbCifose.Checked = false;

            if (exame.Fisico.hasLordose == (int)StatusAnamnese.Sim)
                ckbLordose.Checked = true;
            else
                ckbLordose.Checked = false;

            if (exame.Fisico.hasEscoliose == (int)StatusAnamnese.Sim)
                ckbEscoliose.Checked = true;
            else
                ckbEscoliose.Checked = false;

            if (exame.Fisico.hasLasegue == (int)StatusAnamnese.Sim)
                ckbLasegue.Checked = true;
            else
                ckbLasegue.Checked = false;

            if (exame.Fisico.hasLimitacaoFlexao== (int)StatusAnamnese.Sim)
                ckbFlexao.Checked = true;
            else
                ckbFlexao.Checked = false;


            //if (exame.Fisico.hasPeleAnexosAlterado == (int)StatusAnamnese.Nao)
            //    ckbDescoradas.Checked = true;
            //else if (exame.Fisico.hasPeleAnexosAlterado == (int)StatusAnamnese.Sim)
            //    ckbPeleNormal.Checked = true;
            //if (exame.Fisico.hasOsteoAlterado == (int)StatusAnamnese.Nao)
            //    ckbOsteoN.Checked = true;
            //else if (exame.Fisico.hasOsteoAlterado == (int)StatusAnamnese.Sim)
            //    ckbOsteoS.Checked = true;
            //if (exame.Fisico.hasCabecaAlterado == (int)StatusAnamnese.Nao)
            //    ckbTireoide.Checked = true;
            //else if (exame.Fisico.hasCabecaAlterado == (int)StatusAnamnese.Sim)
            //    chkCabecaNormal.Checked = true;
            //if (exame.Fisico.hasCoracaoAlterado == (int)StatusAnamnese.Nao)
            //    ckbCoracaoN.Checked = true;
            //else if (exame.Fisico.hasCoracaoAlterado == (int)StatusAnamnese.Sim)
            //    ckbCoracaoS.Checked = true;
            //if (exame.Fisico.hasPulmaoAlterado == (int)StatusAnamnese.Nao)
            //    ckbPulmoesN.Checked = true;
            //else if (exame.Fisico.hasPulmaoAlterado == (int)StatusAnamnese.Sim)
            //    ckbPulmoesS.Checked = true;
            //if (exame.Fisico.hasAbdomemAlterado == (int)StatusAnamnese.Nao)
            //    ckbAbdomemN.Checked = true;
            //else if (exame.Fisico.hasAbdomemAlterado == (int)StatusAnamnese.Sim)
            //    ckbAbdomemS.Checked = true;
            //if (exame.Fisico.hasMSAlterado == (int)StatusAnamnese.Nao)
            //    ckbMSN.Checked = true;
            //else if (exame.Fisico.hasMSAlterado == (int)StatusAnamnese.Sim)
            //    ckbMSS.Checked = true;
            //if (exame.Fisico.hasMIAlterado == (int)StatusAnamnese.Nao)
            //    ckbMIN.Checked = true;
            //else if (exame.Fisico.hasMIAlterado == (int)StatusAnamnese.Sim)
            //    ckbMIS.Checked = true;

            //if (exame.Fisico.hasColunaVertebral == (int)StatusAnamnese.Nao)
            //    ckbColunaVertebralN.Checked = true;
            //else if (exame.Fisico.hasColunaVertebral== (int)StatusAnamnese.Sim)
            //    ckbColunaVertebralS.Checked = true;
            //if (exame.Fisico.hasMVsemRA== (int)StatusAnamnese.Nao)
            //    ckbMVsemRAN.Checked = true;
            //else if (exame.Fisico.hasMVsemRA == (int)StatusAnamnese.Sim)
            //    ckbMVsemRAS.Checked = true;
            //if (exame.Fisico.hasBRNF == (int)StatusAnamnese.Nao)
            //    chkBRNFN.Checked = true;
            //else if (exame.Fisico.hasBRNF== (int)StatusAnamnese.Sim)
            //    ckbTinelD.Checked = true;
            //if (exame.Fisico.hasPlano== (int)StatusAnamnese.Nao)
            //    ckbPlanoN.Checked = true;
            //else if (exame.Fisico.hasPlano== (int)StatusAnamnese.Sim)
            //    ckbPlanoS.Checked = true;
            //if (exame.Fisico.hasDB== (int)StatusAnamnese.Nao)
            //    ckbDBAN.Checked = true;
            //else if (exame.Fisico.hasDB== (int)StatusAnamnese.Sim)
            //    ckbDBAS.Checked = true;
            //if (exame.Fisico.hasRHA== (int)StatusAnamnese.Nao)
            //    ckbRHAN.Checked = true;
            //else if (exame.Fisico.hasRHA== (int)StatusAnamnese.Sim)
            //    ckbMaosNormal.Checked = true;
            //if (exame.Fisico.hasIndolor == (int)StatusAnamnese.Nao)
            //    ckbIndolorN.Checked = true;
            //else if (exame.Fisico.hasIndolor== (int)StatusAnamnese.Sim)
            //    ckbIndolorS.Checked = true;
            //if (exame.Fisico.hasAbaulamentos== (int)StatusAnamnese.Nao)
            //    ckbAbaulamentoN.Checked = true;
            //else if (exame.Fisico.hasAbaulamentos== (int)StatusAnamnese.Sim)
            //    ckbAbaulamentoS.Checked = true;



            PopulaLsbFatorRiscoSel();
            txtAnotacoes.Text = exame.Prontuario;
            txtObs.Text = exame.Observacao;


            //liotecnica
            if (Request["IdEmpresa"].ToString().Trim() == "1260401972" || Request["IdEmpresa"].ToString().Trim() == "1801248344")
            {
                if (txtObs.Text.ToString().IndexOf("Apto para manipulação de alimentos.") < 0)
                {
                    txtObs.Text = txtObs.Text.Trim() + " Apto para manipulação de alimentos.";
                    exame.Observacao = txtObs.Text;
                    Entities.Usuario usuario = (Entities.Usuario)Session["usuarioLogado"];
                    exame.Save(usuario.IdUsuario);
                }
            }


            //}
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
                lblIMC.Text = "";
                zReturn = true;
            }


            if (float.TryParse(wneAltura.Text, out zAltura))
            {
                // success! Use f here
            }
            else
            {
                wneAltura.Text = "0";
                lblIMC.Text = "";
                zReturn = true;
            }


            if (zAltura <= 0)
            {
                wneAltura.Text = "0";
                lblIMC.Text = "";
                zReturn = true;
            }

            if (zPeso <= 0)
            {
                wnePeso.Text = "0";
                lblIMC.Text = "";
                zReturn = true;
            }


            if (zReturn == true) return;

            if (zAltura > 100) zAltura = zAltura / 100;

            if (zAltura == 0)
            {
                lblIMC.Text = "";
                return;
            }

            zIMC = (zPeso / (zAltura * zAltura));

            lblIMC.Text = zIMC.ToString("#.##");
            return;

        }

        //#endregion

        //#region PrivateAuxMethods

        ////private void InicializaExameClinico()

        private void InicializaWebPageObjects()
        {
            exame = new ExameClinicoFacade();
            if (ViewState["IdExame"] != null && ViewState["IdExame"].ToString() != "")
            {
                exame.Find(Convert.ToInt32(ViewState["IdExame"]));
                tipo = "atualizado";
            }
            else if (Request["IdExame"] != null && Request["IdExame"] != "")
            {
                exame.Find(Convert.ToInt32(Request["IdExame"]));
                tipo = "atualizado";
            }
            else
            {
                exame.Inicialize();
                exame.IdEmpregado.Id = Convert.ToInt32(Request["IdEmpregado"]);
                tipo = "cadastrado";
            }

            if (exame.Id != 0)
            {
                if (exame.IndResultado == (int)ResultadoExame.NaoRealizado)
                {
                    lblAusente.Visible = true;
                    lbtAusente.Visible = true;
                }
                else
                {
                    lblAusente.Visible = false;
                    lbtAusente.Visible = false;
                }
                btnExcluir.Enabled = true;
                //if (!cliente.ContrataPCMSO.Equals((int)TipoPcmsoContratada.NaoContratada))
                //{
                ddlTipoExame.Enabled = false;
                //uwtExameClinico.Enabled = true;
                lkbASO.Enabled = true;
                lkbPCI.Enabled = true;
                //}
                //else
                //{
                //    ddlTipoExame.Enabled = true;
                //    uwtExameClinico.Enabled = false;
                //    lkbASO.Enabled = false;
                //    lkbPCI.Enabled = false;
                //}
            }
            else
            {
                lblAusente.Visible = false;
                lbtAusente.Visible = false;
                btnExcluir.Enabled = false;
                ddlTipoExame.Enabled = true;
                //if (!cliente.ContrataPCMSO.Equals((int)TipoPcmsoContratada.NaoContratada))
                //{
                //uwtExameClinico.Enabled = true;
                lkbASO.Enabled = true;
                lkbPCI.Enabled = true;
                //}
                //else
                //{
                //   uwtExameClinico.Enabled = false;
                //   lkbASO.Enabled = false;
                //   lkbPCI.Enabled = false;
                //}
            }

            empregado.Find(System.Convert.ToInt32(Request["IdEmpregado"].ToString()));


        }

        private DataSet GetExamesComplementares()
        {
            return new Complementar().ListaExamesComplementar("IdEmpregado=" + Request["IdEmpregado"].ToString() + " ORDER BY DataExame DESC");
        }

        //#endregion

        //#region SaveAndDelete
        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]


        protected void btnOK_Click(object sender, System.EventArgs e)
        {


            if (txtSistolica.Text.Trim() != "" && txtSistolica.Text.Trim() != "0")
            {
                if (System.Convert.ToInt16(txtSistolica.Text) <= 40)
                {
                    MsgBox1.Show("Ilitera.Net", "Valor Sistólica Inválido", null,
                    new EO.Web.MsgBoxButton("OK"));
                    return;
                }
                else
                {
                    if (txtDiastolica.Text.Trim() == "" || txtDiastolica.Text.Trim() == "0")
                    {
                        MsgBox1.Show("Ilitera.Net", "Valor Diastólica Inválido", null,
                        new EO.Web.MsgBoxButton("OK"));
                        return;
                    }
                    else
                    {
                        if (System.Convert.ToInt16(txtDiastolica.Text) <= 40)
                        {
                            MsgBox1.Show("Ilitera.Net", "Valor Diastólica Inválido", null,
                            new EO.Web.MsgBoxButton("OK"));
                            return;
                        }
                        else
                        {
                            if (System.Convert.ToInt16(txtDiastolica.Text) >= System.Convert.ToInt16(txtSistolica.Text))
                            {
                                MsgBox1.Show("Ilitera.Net", "Valor Diastólica Inválido", null,
                                new EO.Web.MsgBoxButton("OK"));
                                return;
                            }
                        }
                    }

                }

            }





            try
            {

                string xNomeArq = File1.FileName.Trim();
               
                Entities.Usuario usuario = (Entities.Usuario)Session["usuarioLogado"];

                PopulaExame();



                exame.IdMedico.Find();

                if (exame.IdMedico.Id == 0 && (exame.IndResultado == 1 || exame.IndResultado == 2))
                {
                    MsgBox1.Show("Ilitera.Net", "Necessário selecionar médico responsável pelo atendimento.", null,
                        new EO.Web.MsgBoxButton("OK"));
                    return;
                }


                if (exame.Id == 0)
                {

                    if (ddlTipoExame.SelectedItem.Value.ToString().Trim() == "2" || ddlTipoExame.SelectedItem.Value.ToString().Trim() == "4")
                    {
                        //ExameBase rexame2 = new ExameBase();

                        ArrayList nExame = new ExameBase().Find(" IDEMPREGADO = " + Request["IdEmpregado"].ToString() + " and IdExameDicionario in ( 1,2,3,4,5 )  and  datediff(day, DataExame, convert( smalldatetime,'" + wdtDataExame.Text + "', 103 )) <= 90 and IndResultado not in ( 0, 3 ) ");
                        //rexame2.Find(" IDEMPREGADO = " + Request["IdEmpregado"].ToString() + " and IdExameDicionario in ( 1,2,3,4,5 )  and  datediff(day, DataExame, convert( smalldatetime,'" + wdtDataExame.Text + "', 103 )) <= 90 and IndResultado <> 0 ");


                        foreach (ExameBase rexame2 in nExame)
                        {

                            if (rexame2.Id != 0)
                            {

                                if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().IndexOf("Prajna") > 0)
                                {
                                    //if (ddlTipoExame.SelectedItem.Value.ToString().Trim() == "2")
                                    //{
                                    //    MsgBox1.Show("Ilitera.Net", "O último exame clínico realizado possibilita a demissão deste colaborador sem a necessidade de exame demissional. " +
                                    //                 "Caso deseje realizá-lo mesmo assim, favor manter contato com a central de atendimento no e-mail centraldeatendimento@5aessence.com.br", null,
                                    //    new EO.Web.MsgBoxButton("Ok", null, "Delete"),
                                    //    new EO.Web.MsgBoxButton("Saiba mais", null, "Saiba mais"));
                                    //    return;
                                    //}
                                    //else
                                    //{

                                    //    MsgBox1.Show("Ilitera.Net", "Último exame dentro da validade, caso deseje realizá-lo mesmo assim, favor manter contato com a central de atendimento no e-mail centraldeatendimento@5aessence.com.br", null,
                                    //    new EO.Web.MsgBoxButton("OK"));
                                    //    return;
                                    //}
                                }
                                else
                                {
                                    if (ddlTipoExame.SelectedItem.Value.ToString().Trim() == "2")
                                    {
                                        if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().IndexOf("Daiti") > 0)
                                        {
                                            MsgBox1.Show("Ilitera.Net", "O último exame clínico realizado possibilita a demissão deste colaborador sem a necessidade de exame demissional. " +
                                                         "Caso deseje realizá-lo mesmo assim, favor manter contato com a central de atendimento no e-mail agendamento.sp.dai@ilitera.com.br", null,
                                                            new EO.Web.MsgBoxButton("OK"));
                                            return;
                                            //new EO.Web.MsgBoxButton("Ok", null, "Delete"),
                                            //new EO.Web.MsgBoxButton("Saiba mais", null, "Saiba mais"));
                                        }
                                        else
                                        {
                                            Cliente xCliente = new Cliente();
                                            xCliente.Find(System.Convert.ToInt32(Request["IdEmpresa"].ToString()));

                                            if (xCliente.Bloquear_ASO_Planejamento == true)
                                            {
                                                MsgBox1.Show("Ilitera.Net", "O último exame clínico realizado possibilita a demissão deste colaborador sem a necessidade de exame demissional. " +
                                                             "Caso deseje realizá-lo mesmo assim, favor manter contato com a central de atendimento no e-mail atendimento@ilitera.com.br", null,
                                                new EO.Web.MsgBoxButton("Ok", null, "Delete"),
                                                new EO.Web.MsgBoxButton("Saiba mais", null, "Saiba mais"));
                                                return;
                                            }
                                        }
                                        
                                    }
                                    else
                                    {
                                        if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().IndexOf("Daiti") > 0)
                                        {
                                            MsgBox1.Show("Ilitera.Net", "Último exame dentro da validade, caso deseje realizá-lo mesmo assim, favor manter contato com a central de atendimento no e-mail agendamento.sp.dai@ilitera.com.br", null,
                                            new EO.Web.MsgBoxButton("OK"));
                                            return;
                                        }
                                        else
                                        {
                                            Cliente xCliente = new Cliente();
                                            xCliente.Find(System.Convert.ToInt32(Request["IdEmpresa"].ToString()));

                                            if (xCliente.Bloquear_ASO_Planejamento == true)
                                            {
                                                MsgBox1.Show("Ilitera.Net", "Último exame dentro da validade, caso deseje realizá-lo mesmo assim, favor manter contato com a central de atendimento no e-mail atendimento@ilitera.com.br", null,
                                                new EO.Web.MsgBoxButton("OK"));
                                                return;
                                            }
                                        }
                                        
                                    }
                                }
                            }


                        }


                        int zDias_Desconsiderar = 0;

                        Cliente rCliente = new Cliente();
                        rCliente.Find(System.Convert.ToInt32(Request["IdEmpresa"].ToString()));

                        //dias desconsiderar dos exames complementares vai valer para validar exame fora do planejamento
                        if (rCliente.Ativar_DesconsiderarCompl == true)
                        {
                            if (rCliente.Dias_Desconsiderar > 0)
                            {
                                zDias_Desconsiderar = rCliente.Dias_Desconsiderar;
                            }
                        }



                        //checar planejamento de exames - apenas para Prajna ??
                        if (ddlTipoExame.SelectedItem.Value.ToString().Trim() == "4")
                        {

                            Ilitera.Data.Clientes_Funcionarios xPlan = new Ilitera.Data.Clientes_Funcionarios();

                            if (xPlan.Buscar_Planejamento_Exame_Colaborador(System.Convert.ToInt32(Request["IdEmpregado"].ToString()), 4, wdtDataExame.Text.Trim(), zDias_Desconsiderar) == false)
                            {
                                if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().IndexOf("Prajna") > 0)
                                {
                                    //MsgBox1.Show("Ilitera.Net", "Exame fora do planejamento, caso deseje realizá-lo mesmo assim, favor manter contato com a central de atendimento no e-mail centraldeatendimento@5aessence.com.br", null,
                                    //new EO.Web.MsgBoxButton("OK"));
                                    //return;
                                }
                                else
                                {
                                    if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().IndexOf("Daiti") > 0)
                                    {
                                        MsgBox1.Show("Ilitera.Net", "Exame fora do planejamento, caso deseje realizá-lo mesmo assim, favor manter contato com a central de atendimento no e-mail agendamento.sp.dai@ilitera.com.br", null,
                                        new EO.Web.MsgBoxButton("OK"));
                                    }
                                    else if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().IndexOf("Cotia") > 0)
                                    {
                                        MsgBox1.Show("Ilitera.Net", "Exame fora do planejamento, caso deseje realizá-lo mesmo assim, favor manter contato com a central de atendimento no e-mail medicina.sp.cot@ilitera.com.br", null,
                                        new EO.Web.MsgBoxButton("OK"));
                                    }
                                    else
                                    {
                                        MsgBox1.Show("Ilitera.Net", "Exame fora do planejamento, caso deseje realizá-lo mesmo assim, favor manter contato com a central de atendimento no e-mail atendimento@ilitera.com.br", null,
                                        new EO.Web.MsgBoxButton("OK"));
                                    }
                                    return;
                                }
                            }
                        }

                    }
                }



                if (chk_PacienteCritico.Checked == true)
                {
                    //checar se campo obs. foi preenchido
                    if (txtObs.Text.Trim() == "" || txtObs.Text.Trim().Length < 5)
                    {
                        MsgBox1.Show("Ilitera.Net", "Utilize o campo Observação para colocar um parecer do paciente crítico.", null,
                              new EO.Web.MsgBoxButton("OK"));                        
                        TabStrip1.Items[3].Selected = true;
                        return;
                    }

                    if ( ddlMedico.SelectedIndex < 1)
                    {
                        MsgBox1.Show("Ilitera.Net", "Selecione Médico examinador que indicou paciente crítico.", null,
                              new EO.Web.MsgBoxButton("OK"));
                        TabStrip1.Items[0].Selected = true;
                        return;
                    }

                    exame.Paciente_Critico = true;
                }
                else
                {
                    exame.Paciente_Critico = false;
                }


                ////deixar "normal" selecionado caso nenhum esteja selecionado
                //if (ckbDescoradas.Checked == false && ckbIctericas.Checked == false && ckbCianoticas.Checked == false && ckbDermatoses.Checked == false)
                //{
                //    exame.Fisico.hasPeleNormal = (int)StatusAnamnese.Sim;
                //    ckbPeleNormal.Checked = true;
                //}

                //if (ckbOculos.Checked == false)
                //{
                //    exame.Fisico.hasOlhosNormal = (int)StatusAnamnese.Sim;
                //    ckbOlhosNormal.Checked = true;
                //}

                //if (ckbTireoide.Checked == false && ckbDente.Checked == false && ckbGarganta.Checked == false && ckbAdenomegalia.Checked == false && ckbOuvido.Checked == false)
                //{
                //    exame.Fisico.hasCabecaNormal = (int)StatusAnamnese.Sim;
                //    ckbCabecaNormal.Checked = true;
                //}

                //if (ckbRotacao.Checked == false && ckbPalpacaoBiceps.Checked == false && ckbPalpacaoSubacromial.Checked == false)
                //{
                //    exame.Fisico.hasMSNormal = (int)StatusAnamnese.Sim;
                //    ckbOmbrosNormal.Checked = true;
                //}

                //if (ckbEdemas.Checked == false && ckbPulsos.Checked == false && ckbArticular.Checked == false && ckbVarizes.Checked == false)
                //{
                //    exame.Fisico.hasMINormal = (int)StatusAnamnese.Sim;
                //    ckbMINormal.Checked = true;
                //}

                //if (ckbCifose.Checked == false && ckbLordose.Checked == false && ckbEscoliose.Checked == false && ckbLasegue.Checked == false && ckbFlexao.Checked == false)
                //{
                //    exame.Fisico.hasColunaNormal = (int)StatusAnamnese.Sim;
                //    ckbColunaNormal.Checked = true;
                //}

                //if (ckbTinelD.Checked == false && ckbTinelE.Checked == false && ckbFinkelsteinD.Checked == false && ckbFinkelsteinE.Checked == false && ckbNodulosD.Checked == false && ckbNodulosE.Checked == false && ckbEdemaD.Checked == false && ckbEdemaE.Checked == false && ckbPrensaoD.Checked == false && ckbPrensaoE.Checked == false && ckbFalenD.Checked == false && ckbFalenE.Checked == false && ckbCrepitacaoD.Checked == false && ckbCrepitacaoE.Checked == false)
                //{
                //    exame.Fisico.hasMaosNormal = (int)StatusAnamnese.Sim;
                //    ckbMaosNormal.Checked = true;
                //}

                //if (ckbSopros.Checked == false && ckbArritmias.Checked == false && ckbCardioVascularOutros.Checked == false)
                //{
                //    exame.Fisico.hasBRNF = (int)StatusAnamnese.Sim;
                //    ckbBRNF.Checked = true;
                //}

                //if (ckbMVAlterado.Checked == false && ckbRuidos.Checked == false)
                //{
                //    exame.Fisico.hasMVsemRA = (int)StatusAnamnese.Sim;
                //    ckbMVsemRA.Checked = true;
                //}

                //if (ckbPalpacao.Checked == false && ckbMassas.Checked == false && ckbHepatoesplenomegalia.Checked == false)
                //{
                //    exame.Fisico.hasPlano = (int)StatusAnamnese.Sim;
                //    ckbPlano.Checked = true;
                //}



                ViewState["IdExame"] = exame.Save(usuario.IdUsuario);



                //salvar data de exame em demissional
                if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().Trim().ToUpper() == "OPSA" || Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("QTECK") > 0 || Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("VIA") > 0 || Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("DAITI") > 0 || Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("COTIA") > 0)
                {
                    if (ddlTipoExame.SelectedItem.Value.ToString().Trim() == "2")
                        {
                        if (exame.IndResultado == 1)
                        {
                            //System.Convert.ToInt32(Request["IdEmpregado"].ToString())
                            //txt_Data.Text                        
                            Ilitera.Data.PPRA_EPI xGuia = new Ilitera.Data.PPRA_EPI();

                            //xGuia.Guia_Demissao_Colaborador(System.Convert.ToInt32(Request["IdEmpregado"].ToString()), wdtDataExame.Text);
                            if ( lblDemissao.Text.Trim()!="")
                                xGuia.Guia_Demissao_Colaborador_Obras(System.Convert.ToInt32(Request["IdEmpregado"].ToString()), lblDemissao.Text);
                            else
                                xGuia.Guia_Demissao_Colaborador_Obras(System.Convert.ToInt32(Request["IdEmpregado"].ToString()), wdtDataExame.Text);
                        }

                    }
                }
                else if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().IndexOf("Prajna") > 0)
                {
                    if (ddlTipoExame.SelectedItem.Value.ToString().Trim() == "2")
                    {
                        if (exame.IndResultado == 1)
                        {
                            Ilitera.Data.PPRA_EPI xDem = new Ilitera.Data.PPRA_EPI();

                            if (lblDemissao.Text.Trim() != "")
                                xDem.Guia_Demissao_Colaborador_Obras(System.Convert.ToInt32(Request["IdEmpregado"].ToString()), lblDemissao.Text);
                            else
                                xDem.Guia_Demissao_Colaborador_Obras(System.Convert.ToInt32(Request["IdEmpregado"].ToString()), wdtDataExame.Text);
                        }
                    }

                }




                //salvar prontuario
                if (xNomeArq != string.Empty)
                {


                    string xExtension = xNomeArq.Substring(xNomeArq.Length - 3, 3).ToUpper().Trim();

                    if (xExtension == "PDF" || xExtension == "JPG")
                    {

                        Cliente xCliente = new Cliente();
                        xCliente.Find(System.Convert.ToInt32(Request["IdEmpresa"].ToString()));

                        string xArq = "";

                        //if (Session["Servidor_Web"].ToString().Trim().ToUpper() == "ILITERA")
                        xArq = "I:\\fotosdocsdigitais\\" + xCliente.DiretorioPadrao.ToString().ToUpper().Trim() + "\\Prontuario\\" + xNomeArq;
                        // else
                        //    xArq = "C:\\DRIVE_I\\fotosdocsdigitais\\" + xCliente.DiretorioPadrao.ToString().ToUpper().Trim() + "\\Prontuario\\" + xNomeArq;

                        File1.SaveAs(xArq);

                        xArq = "I:\\fotosdocsdigitais\\" + xCliente.DiretorioPadrao.ToString().ToUpper().Trim() + "\\Prontuario\\" + xNomeArq;


                        ExameBase xExameBase = new ExameBase();
                        xExameBase.Find(exame.Id);

                        Empregado xEmpregado = new Empregado();
                        xEmpregado.Find(System.Convert.ToInt32(Request["IdEmpregado"].ToString()));

                        Entities.Usuario zusuario = (Entities.Usuario)Session["usuarioLogado"];
                        prestador.Find(" IdPessoa = " + zusuario.IdPessoa.ToString());

                        ProntuarioDigital xProntuario = new ProntuarioDigital();
                        xProntuario.IdExameBase = xExameBase;
                        xProntuario.IdEmpregado = xEmpregado;

                        System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");
                        xProntuario.DataProntuario = System.Convert.ToDateTime(wdtDataExame.Text, ptBr);

                        xProntuario.Arquivo = xArq; //searchFile.Value.ToString();
                        xProntuario.DataDigitalizacao = System.DateTime.Now;
                        xProntuario.IdDigitalizador = prestador;
                        xProntuario.Save();

                    }
                }






                btnExcluir.Enabled = true;
                RegisterClientCode();

                if (!lsbFatorRisco.Items.Count.Equals(0))
                {
                    imbAdd.ImageUrl = "img/right.jpg";
                    imbAddAll.ImageUrl = "img/rightAllEnabled.jpg";
                    imbAdd.Enabled = true;
                    imbAddAll.Enabled = true;
                }

                txtAuxAviso.Value = "O Exame Clínico foi " + tipo + " com sucesso!";
                txtExecutePost.Value = "True";

                //if (cliente.ContrataPCMSO.Equals((int)TipoPcmsoContratada.NaoContratada))
                txtCloseWindow.Value = "True";

                StringBuilder strBuilder = new StringBuilder();

                strBuilder.Append("window.opener.document.getElementById('txtAuxiliar').value='atualiza';");
                strBuilder.Append("window.opener.document.forms[0].submit();");
                strBuilder.Append("window.alert('O Exame Clínico foi " + tipo + " com sucesso!');");

                this.ClientScript.RegisterStartupScript(this.GetType(), "CadastroClinico", strBuilder.ToString(), true);

                //Response.Write("<script>window.opener.document.forms[0].submit(); window.close();</script>");
                Entities.Usuario user = (Entities.Usuario)Session["usuarioLogado"];


                if (ddlTipoExame.SelectedItem.Value.ToString().Trim() == "5")
                {
                    Cliente cliente;
                    cliente = new Cliente(System.Convert.ToInt32(Session["Empresa"].ToString()));

                    cliente.IdGrupoEmpresa.Find();

                    if (cliente.IdGrupoEmpresa.Descricao.ToUpper().Trim() == "CAPGEMINI" && Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("PRAJNA") > 0)  //.Id == -905238295)  // Capgemini
                    {

                        if (exame.IndResultado == 1)
                        {

                            //checar se há afastamento INSS aberto
                            //  em caso positivo,  perguntar se quer colocar data final igual a data do exame 
                            System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");

                            ArrayList nAfast = new Afastamento().Find(" IDEMPREGADO = " + Request["IdEmpregado"].ToString() + " and DataVolta is null and DataInicial <= convert( smalldatetime, '" + exame.DataExame.ToString("dd/MM/yyyy", ptBr ) +  "', 103 )  and ( INSS is not null and INSS = 1 ) ");

                            //foreach (Afastamento zAfast in nAfast)
                            if (nAfast.Count > 0)
                            {
                                MsgBox1.Show("Ilitera.Net", "Existem afastamentos pelo INSS aberto para esse colaborador. " +
                                             "Colocar a data deste exame como data de retorno do afastamento ?", null,
                                new EO.Web.MsgBoxButton("Sim", null, "Afastamento_Fechar"),
                                new EO.Web.MsgBoxButton("Não", null, "Afastamento_Nao_Fechar"));


                            }
                            else
                            {
                                MsgBox1.Show("Ilitera.Net", "Dados salvos.", null,
                                new EO.Web.MsgBoxButton("OK"));

                                return;   //capgemini quer permanecer na tela
                                //if (Session["Retorno"].ToString().Trim() == "1")
                                //    Response.Redirect("DadosEmpregado.aspx?IdEmpregado=" + Session["Empregado"].ToString() + "&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString() + "&Tipo=1");
                                //else if (Session["Retorno"].ToString().Trim() == "9")
                                //    Response.Redirect("DadosEmpregado_Dashboard.aspx?IdEmpregado=" + Session["Empregado"].ToString() + "&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString() + "&Tipo=6");
                                //else
                                //    Response.Redirect("DadosEmpregado_Lista.aspx?IdEmpregado=" + Session["Empregado"].ToString() + "&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString() + "&Tipo=6");
                            }

                        }
                        else
                        {
                            MsgBox1.Show("Ilitera.Net", "Dados salvos.", null,
                            new EO.Web.MsgBoxButton("OK"));

                            return;  //capgemini quer permanecer na tela
                            //if (Session["Retorno"].ToString().Trim() == "1")
                            //    Response.Redirect("DadosEmpregado.aspx?IdEmpregado=" + Session["Empregado"].ToString() + "&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString() + "&Tipo=1");
                            //else if (Session["Retorno"].ToString().Trim() == "9")
                            //    Response.Redirect("DadosEmpregado_Dashboard.aspx?IdEmpregado=" + Session["Empregado"].ToString() + "&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString() + "&Tipo=6");
                            //else
                            //    Response.Redirect("DadosEmpregado_Lista.aspx?IdEmpregado=" + Session["Empregado"].ToString() + "&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString() + "&Tipo=6");
                        }

                    }
                    else
                    {
                        if (Session["Retorno"].ToString().Trim() == "1")
                            Response.Redirect("DadosEmpregado.aspx?IdEmpregado=" + Session["Empregado"].ToString() + "&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString() + "&Tipo=1");
                        else if (Session["Retorno"].ToString().Trim() == "9")
                            Response.Redirect("DadosEmpregado_Dashboard.aspx?IdEmpregado=" + Session["Empregado"].ToString() + "&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString() + "&Tipo=6");
                        else if (Session["Retorno"].ToString().Trim() == "91")
                            Response.Redirect("DadosEmpregado_Questionario.aspx?IdEmpregado=" + Session["Empregado"].ToString() + "&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString() + "&Tipo=6");
                        else if (Session["Retorno"].ToString().Trim() == "19")
                            Response.Redirect("ExameQuestionario.aspx?IdEmpregado=" + Session["Empregado"].ToString() + "&IdExame=" + Session["Questionario"].ToString().Trim() + "&IdUsuario=" + Request["IdUsuario"] + "&IdEmpresa=" + Request["IdEmpresa"] + "&Acao=E");
                        else
                            Response.Redirect("DadosEmpregado_Lista.aspx?IdEmpregado=" + Session["Empregado"].ToString() + "&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString() + "&Tipo=6");
                    }
                }
                else
                {


                    Cliente cliente;
                    cliente = new Cliente(System.Convert.ToInt32(Session["Empresa"].ToString()));

                    cliente.IdGrupoEmpresa.Find();

                    if (cliente.IdGrupoEmpresa.Descricao.ToUpper().Trim() == "CAPGEMINI" && Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("PRAJNA") > 0)  //.Id == -905238295)  // Capgemini
                    {
                        MsgBox1.Show("Ilitera.Net", "Dados salvos.", null,
                        new EO.Web.MsgBoxButton("OK"));

                        return;  //capgemini quer permanecer na tela
                    }
                    else
                    {

                        if (Session["Retorno"].ToString().Trim() == "1")
                            Response.Redirect("DadosEmpregado.aspx?IdEmpregado=" + Session["Empregado"].ToString() + "&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString() + "&Tipo=1");
                        else if (Session["Retorno"].ToString().Trim() == "9")
                            Response.Redirect("DadosEmpregado_Dashboard.aspx?IdEmpregado=" + Session["Empregado"].ToString() + "&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString() + "&Tipo=6");
                        else if (Session["Retorno"].ToString().Trim() == "91")
                            Response.Redirect("DadosEmpregado_Questionario.aspx?IdEmpregado=" + Session["Empregado"].ToString() + "&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString() + "&Tipo=6");
                        else if (Session["Retorno"].ToString().Trim() == "19")
                            Response.Redirect("ExameQuestionario.aspx?IdEmpregado=" + Session["Empregado"].ToString() + "&IdExame=" + Session["Questionario"].ToString().Trim() + "&IdUsuario=" + Request["IdUsuario"] + "&IdEmpresa=" + Request["IdEmpresa"] + "&Acao=E");
                        else
                            Response.Redirect("DadosEmpregado_Lista.aspx?IdEmpregado=" + Session["Empregado"].ToString() + "&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString() + "&Tipo=6");
                    }

                }



            }
            catch (Exception ex)
            {
                txtAuxAviso.Value = ex.Message;
            }
        }

        protected void btnExcluir_Click(object sender, System.EventArgs e)
        {

            if (exame.Id != 0)
            {
                Cliente cliente;
                cliente = new Cliente(System.Convert.ToInt32(Session["Empresa"].ToString()));

                Ilitera.Data.eSocial xChecagem = new Ilitera.Data.eSocial();
                if (xChecagem.Checar_Chave_eSocial(exame.Id, "2220", cliente.ESocial_Ambiente) > 0)
                {
                    MsgBox1.Show("Ilitera.Net", "Exame com evento criado no eSocial não pode ser excluído.", null,
                    new EO.Web.MsgBoxButton("OK"));
                    return;
                }
            }


            try
            {

                Entities.Usuario usuario = (Entities.Usuario)Session["usuarioLogado"];

                //if (!cliente.ContrataPCMSO.Equals((int)TipoPcmsoContratada.NaoContratada))
                ProntuarioDigital xProntuario = new ProntuarioDigital();
                xProntuario.Find("IdExameBase = " + exame.Id.ToString() + " ");
                if (xProntuario.Id != 0)
                {
                    xProntuario.Delete();
                }

                exame.Delete(usuario.IdUsuario);

                //else
                //exame.Delete(true, usuario.IdUsuario);

                txtAuxAviso.Value = "O Exame Clínico foi deletado com sucesso";
                txtExecutePost.Value = "True";
                txtCloseWindow.Value = "True";

                //Response.Write("<script>window.opener.document.forms[0].submit(); window.close();</script>");
                Entities.Usuario user = (Entities.Usuario)Session["usuarioLogado"];

                if (Session["Retorno"].ToString() == "1")
                    Response.Redirect("DadosEmpregado.aspx?IdEmpregado=" + Session["Empregado"].ToString() + "&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString() + "&Tipo=1");
                else if (Session["Retorno"].ToString().Trim() == "9")
                    Response.Redirect("DadosEmpregado_Dashboard.aspx?IdEmpregado=" + Session["Empregado"].ToString() + "&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString() + "&Tipo=6");
                else if (Session["Retorno"].ToString().Trim() == "91")
                    Response.Redirect("DadosEmpregado_Questionario.aspx?IdEmpregado=" + Session["Empregado"].ToString() + "&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString() + "&Tipo=6");
                else if (Session["Retorno"].ToString().Trim() == "19")
                    Response.Redirect("ExameQuestionario.aspx?IdEmpregado=" + Session["Empregado"].ToString() + "&IdExame=" + Session["Questionario"].ToString().Trim() + "&IdUsuario=" + Request["IdUsuario"] + "&IdEmpresa=" + Request["IdEmpresa"] + "&Acao=E");
                else
                    Response.Redirect("DadosEmpregado_Lista.aspx?IdEmpregado=" + Session["Empregado"].ToString() + "&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString() + "&Tipo=6");


            }
            catch (Exception ex)
            {
                txtAuxAviso.Value = ex.Message;
            }
        }

        //#endregion

        //#region PageEvents

        protected void btnResetText_Click(object sender, System.EventArgs e)
        {
            StringBuilder st = new StringBuilder();

            //if (ckbQueixaS.Checked)
            //    st.Append("Queixas atuais: \n");
            //if (ckbAfastadoS.Checked)
            //    st.Append("Afastamento do trabalho: \n");
            //if (ckbAnteFamiS.Checked)
            //    st.Append("Antecedentes familiares: \n");
            //if (ckbDoencaS.Checked)
            //    st.Append("Doença crônica / Atopia: \n");
            //if (ckbCirurgiaS.Checked)
            //    st.Append("Cirurgia: \n");
            //if (ckbTraumaS.Checked)
            //    st.Append("Trauma: \n");
            //if (ckbDeficienciaS.Checked)
            //    st.Append("Deficiência fisica: \n");
            //if (ckbMedicamentosS.Checked)
            //    st.Append("Uso de medicamentos: \n");
            //if (ckbTabagismoS.Checked)
            //    st.Append("Tabagista: \n");
            //if (ckbEtilismoS.Checked)
            //    st.Append("Etilista: \n");
            //if (ckbOsteoS.Checked)
            //    st.Append("Osteo muscular: \n");
            //if (ckbPeleS.Checked)
            //    st.Append("Pele e anexos: \n");
            //if (ckbCabecaS.Checked)
            //    st.Append("Cabeça e pescoço: \n");
            //if (ckbCoracaoS.Checked)
            //    st.Append("Coração: \n");
            //if (ckbPulmoesS.Checked)
            //    st.Append("Pulmões: \n");
            //if (ckbAbdomemS.Checked)
            //    st.Append("Abdomem: \n");
            //if (ckbMSS.Checked)
            //    st.Append("Membros superiores: \n");
            //if (ckbMIS.Checked)
            //    st.Append("Membros inferiores: \n");

            txtAnotacoes.Text = st.ToString();
        }

        protected void lbtAusente_Click(object sender, System.EventArgs e)
        {
            try
            {
                exame.Faltou(usuario);

                txtAuxAviso.Value = "O Exame Clínico foi excluído com sucesso e cadastrado como não realizado por motivo de falta!";
                txtExecutePost.Value = "True";
                txtCloseWindow.Value = "No";
            }
            catch (Exception ex)
            {
                MsgBox1.Show("Ilitera.Net", ex.Message, null,
                       new EO.Web.MsgBoxButton("OK"));

            }
        }

        protected void imbAddAll_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            try
            {
                if (exame.IndResultado.Equals((int)ResultadoExame.NaoRealizado) || exame.Id == 0)
                {
                    //throw new Exception("É necessário colocar o resultado do exame e salvá-lo antes de adicionar todos os Fatores de Risco!");
                    MsgBox1.Show("Ilitera.Net", "É necessário colocar o resultado do exame e salvá-lo antes de adicionar todos os Fatores de Risco!", null,
                    new EO.Web.MsgBoxButton("OK"));
                    return;
                }


                FatorRiscoClinico fatorRiscoClinico;
                int numItem = lsbFatorRisco.Items.Count;

                foreach (ListItem fatorRisco in lsbFatorRisco.Items)
                {
                    numItem--;

                    fatorRiscoClinico = new FatorRiscoClinico();
                    fatorRiscoClinico.Inicialize();
                    fatorRiscoClinico.IdFatorRisco.Id = Convert.ToInt32(fatorRisco.Value);
                    fatorRiscoClinico.IdClinico.Id = exame.Id;
                    fatorRiscoClinico.UsuarioId = usuario.Id;

                    if (numItem.Equals(0))
                        fatorRiscoClinico.UsuarioProcessoRealizado = "Cadastro de Indicadores de Morbidade para o exame clínico.";

                    fatorRiscoClinico.Save();
                }

                PopulaLsbFatorRisco();
                PopulaLsbFatorRiscoSel();

                txtAuxAviso.Value = "Todos os Fatores de Risco foram adicionados com sucesso!";
            }
            catch (Exception ex)
            {
                //txtAuxAviso.Value = ex.Message;
                MsgBox1.Show("Ilitera.Net", ex.Message, null,
                new EO.Web.MsgBoxButton("OK"));
            }
        }

        protected void imbAdd_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            try
            {
                if (exame.IndResultado.Equals((int)ResultadoExame.NaoRealizado) || exame.Id == 0)
                //throw new Exception("É necessário colocar o resultado do exame e salvá-lo antes de adicionar um ou mais Fatores de Risco!");
                {
                    MsgBox1.Show("Ilitera.Net", "É necessário colocar o resultado do exame e salvá-lo antes de adicionar um ou mais Fatores de Risco!", null,
                    new EO.Web.MsgBoxButton("OK"));
                    return;
                }



                if (lsbFatorRisco.SelectedIndex < 0)  // .SelectedItem == null)
                //throw new Exception("É necessário selecionar pelo menos 1 Fator de Risco para ser adicionado!");
                {
                    MsgBox1.Show("Ilitera.Net", "É necessário selecionar pelo menos 1 Fator de Risco para ser adicionado!", null,
                    new EO.Web.MsgBoxButton("OK"));
                    return;
                }


                FatorRiscoClinico fatorRiscoClinico;
                int numAux = 0;

                foreach (ListItem fatorRisco in lsbFatorRisco.Items)
                    if (fatorRisco.Selected)
                    {
                        fatorRiscoClinico = new FatorRiscoClinico();
                        fatorRiscoClinico.Inicialize();
                        fatorRiscoClinico.IdFatorRisco.Id = Convert.ToInt32(fatorRisco.Value);
                        fatorRiscoClinico.IdClinico.Id = exame.Id;
                        fatorRiscoClinico.UsuarioId = usuario.Id;

                        if (numAux.Equals(0))
                        {
                            fatorRiscoClinico.UsuarioProcessoRealizado = "Cadastro de Indicadores de Morbidade para o exame clínico.";
                            numAux++;
                        }

                        fatorRiscoClinico.Save();
                    }

                PopulaLsbFatorRisco();
                PopulaLsbFatorRiscoSel();

                txtAuxAviso.Value = "Os Fatores de Risco selecionados foram adicionados com sucesso!";
            }
            catch (Exception ex)
            {
                //txtAuxAviso.Value = ex.Message;
                MsgBox1.Show("Ilitera.Net", ex.Message, null,
                new EO.Web.MsgBoxButton("OK"));
            }
        }

        protected void imbRemove_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            try
            {
                if (lsbFatorRiscoSel.SelectedItem == null)
                //throw new Exception("É necessário selecionar pelo menos 1 Fator de Risco para ser removido!");
                {
                    MsgBox1.Show("Ilitera.Net", "É necessário selecionar pelo menos 1 Fator de Risco para ser removido!", null,
                    new EO.Web.MsgBoxButton("OK"));
                    return;
                }




                FatorRiscoClinico fatorRiscoClinico;
                int numAux = 0;

                foreach (ListItem fatorRiscoSel in lsbFatorRiscoSel.Items)
                    if (fatorRiscoSel.Selected)
                    {
                        fatorRiscoClinico = new FatorRiscoClinico();
                        fatorRiscoClinico.Find("IdFatorRisco=" + fatorRiscoSel.Value + " AND IdClinico=" + exame.Id);
                        fatorRiscoClinico.UsuarioId = usuario.Id;

                        if (numAux.Equals(0))
                        {
                            fatorRiscoClinico.UsuarioProcessoRealizado = "Exclusão de Indicadores de Morbidade para o exame clínico.";
                            numAux++;
                        }

                        fatorRiscoClinico.Delete();
                    }

                PopulaLsbFatorRisco();
                PopulaLsbFatorRiscoSel();

                txtAuxAviso.Value = "Os Fatores de Risco selecionados foram excluídos com sucesso!";
            }
            catch (Exception ex)
            {
                //txtAuxAviso.Value = ex.Message;
                MsgBox1.Show("Ilitera.Net", ex.Message, null,
                new EO.Web.MsgBoxButton("OK"));

            }
        }

        protected void imbRemoveAll_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            try
            {
                FatorRiscoClinico fatorRiscoClinico;
                int numItem = lsbFatorRiscoSel.Items.Count;

                foreach (ListItem fatorRiscoSel in lsbFatorRiscoSel.Items)
                {
                    numItem--;

                    fatorRiscoClinico = new FatorRiscoClinico();
                    fatorRiscoClinico.Find("IdFatorRisco=" + fatorRiscoSel.Value + " AND IdClinico=" + exame.Id);
                    fatorRiscoClinico.UsuarioId = usuario.Id;

                    if (numItem.Equals(0))
                        fatorRiscoClinico.UsuarioProcessoRealizado = "Exclusão de Indicadores de Morbidade para o exame clínico.";

                    fatorRiscoClinico.Delete();
                }

                PopulaLsbFatorRisco();
                PopulaLsbFatorRiscoSel();

                //txtAuxAviso.Value = "Todos os Fatores de Risco foram excluídos com sucesso!";
            }
            catch (Exception ex)
            {
                //txtAuxAviso.Value = ex.Message;
                MsgBox1.Show("Ilitera.Net", ex.Message, null,
                new EO.Web.MsgBoxButton("OK"));
            }
        }

        //protected void wneAltura_ValueChange(object sender, Infragistics.WebUI.WebDataInput.ValueChangeEventArgs e)
        //{
        //    if (!wneAltura.ValueInt.Equals(0) && !wnePeso.ValueInt.Equals(0))
        //        lblIMC.Text = ((double)(wnePeso.ValueDouble / Math.Pow(wneAltura.ValueDouble, 2.0))).ToString("0.00");
        //    else
        //        lblIMC.Text = string.Empty;
        //}

        //protected void wnePeso_ValueChange(object sender, Infragistics.WebUI.WebDataInput.ValueChangeEventArgs e)
        //{
        //    if (!wneAltura.ValueInt.Equals(0) && !wnePeso.ValueInt.Equals(0))
        //        lblIMC.Text = ((double)(wnePeso.ValueDouble / Math.Pow(wneAltura.ValueDouble, 2.0))).ToString("0.00");
        //    else
        //        lblIMC.Text = string.Empty;
        //}

        //protected void UltraWebGridExComplementares_PageIndexChanged(object sender, Infragistics.WebUI.UltraWebGrid.PageEventArgs e)
        //{
        //    UltraWebGridExComplementares.DataSource = GetExamesComplementares();
        //    UltraWebGridExComplementares.DataBind();
        //}

        //#endregion

        //protected void uwtExameClinico_TabClick(object sender, Infragistics.WebUI.UltraWebTab.WebTabEvent e)
        //{

        //}


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




        protected void MsgBox1_ButtonClick(object sender, System.Web.UI.WebControls.CommandEventArgs e)
        {
            //Use the command name to determine which
            //button was clicked
            if (e.CommandName == "Saiba mais")
            {
                //Proceed to delete....
                MsgBox2.Show("Saiba mais", "Observemos que o exame demissional é obrigatório desde que o último exame médico " +
                                            "ocupacional, seja de que natureza for – admissional, retorno ao trabalho, mudança de função ou mesmo o periódico - tenha sido realizado há mais de 135 ou 90 dias, sendo a primeira opção de 135 dias para as empresas com grau de risco 1 ou 2 e 90 dias para as empresas com grau de risco equivalente a 3 ou 4. " + System.Environment.NewLine +
                                            " O texto legal é claro: os exames demissionais somente serão obrigatórios se as condições especificadas no parágrafo anterior foram preenchidas. O exame clínico demissional não é obrigatório em quaisquer circunstâncias, bastando a demissão do colaborador para que ele se torne necessário. Para a sua realização deverá haver duas condições obrigatórias, ou sejam a demissão do colaborador e a não realização de exame clínico anterior nos prazos estabelecidos nos dois item da norma legal, 135 ou 90 dias conforme o caso. " + System.Environment.NewLine +
                                            "Pensar em exame demissional com realização automática a partir da demissão é impactar o custo operacional dos programas médicos com exames desnecessários e procedimentos complementares, a cargo das empresas, com valores maiores ainda. O legislador foi sábio ao determinar o procedimento em questão, uma vez que, na grande maioria dos casos, a avaliação clínica dentro dos períodos especificados, 135 e 90 dias, são extremamente satisfatórias do ponto de vista da boa conduta médica e das diretrizes estabelecidas e aceitas pelo Conselho Federal de Medicina, órgão regulamentador máximo da profissão médica. " + System.Environment.NewLine +
                                            "Ressalte-se que a não exigência dos exames demissionais, nos casos específicos  " +
                                            "apresentados, não se trata de “brecha” na legislação vigente, mas, isto sim, foi alternativa pensada, discutida, debatida pelo grupo legislador e criador da Norma Regulamentadora nº 7, no ano de 1994, quando da emissão da Portaria nº 24 pelo Ministério do Trabalho e Emprego.  " + System.Environment.NewLine +
                                            "Vinte anos serão completados da sua publicação em dezembro próximo e jamais houve qualquer alteração neste item legal. ", null,
                new EO.Web.MsgBoxButton("OK"));
                return;


            }
            else if (e.CommandName == "Afastamento_Fechar")
            {
                ArrayList nAfast = new Afastamento().Find(" IDEMPREGADO = " + Request["IdEmpregado"].ToString() + " and DataVolta is null and ( INSS is not null and INSS = 1 ) ");

                foreach (Afastamento zAfast in nAfast)
                {
                    zAfast.DataVolta = exame.DataExame;
                    zAfast.Save();
                }

                Entities.Usuario user = (Entities.Usuario)Session["usuarioLogado"];

                if (Session["Retorno"].ToString().Trim() == "1")
                    Response.Redirect("DadosEmpregado.aspx?IdEmpregado=" + Session["Empregado"].ToString() + "&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString() + "&Tipo=1");
                else if (Session["Retorno"].ToString().Trim() == "9")
                    Response.Redirect("DadosEmpregado_Dashboard.aspx?IdEmpregado=" + Session["Empregado"].ToString() + "&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString() + "&Tipo=6");
                else if (Session["Retorno"].ToString().Trim() == "91")
                    Response.Redirect("DadosEmpregado_Questionario.aspx?IdEmpregado=" + Session["Empregado"].ToString() + "&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString() + "&Tipo=6");
                else if (Session["Retorno"].ToString().Trim() == "19")
                    Response.Redirect("ExameQuestionario.aspx?IdEmpregado=" + Session["Empregado"].ToString() + "&IdExame=" + Session["Questionario"].ToString().Trim() + "&IdUsuario=" + Request["IdUsuario"] + "&IdEmpresa=" + Request["IdEmpresa"] + "&Acao=E");
                else
                    Response.Redirect("DadosEmpregado_Lista.aspx?IdEmpregado=" + Session["Empregado"].ToString() + "&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString() + "&Tipo=6");

            }
            else if (e.CommandName == "Afastamento_Nao_Fechar")
            {

                Entities.Usuario user = (Entities.Usuario)Session["usuarioLogado"];

                if (Session["Retorno"].ToString().Trim() == "1")
                    Response.Redirect("DadosEmpregado.aspx?IdEmpregado=" + Session["Empregado"].ToString() + "&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString() + "&Tipo=1");
                else if (Session["Retorno"].ToString().Trim() == "9")
                    Response.Redirect("DadosEmpregado_Dashboard.aspx?IdEmpregado=" + Session["Empregado"].ToString() + "&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString() + "&Tipo=6");
                else if (Session["Retorno"].ToString().Trim() == "91")
                    Response.Redirect("DadosEmpregado_Questionario.aspx?IdEmpregado=" + Session["Empregado"].ToString() + "&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString() + "&Tipo=6");
                else
                    Response.Redirect("DadosEmpregado_Lista.aspx?IdEmpregado=" + Session["Empregado"].ToString() + "&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString() + "&Tipo=6");

            }


        }


        protected void btnNovoFatorRisco_Click(object sender, EventArgs e)
        {
            lbl_Atualiza.Text = "1";
        }


        private void PopulaDDLClinicas(string IdExameDicionario)
        {
            //ddlClinica.DataSource = new Clinica().Get("((IdClinica IN (SELECT IdClinica FROM ClinicaCliente WHERE IdCliente=" + Request["IdEmpresa"].ToString() + " )"
            //    +" AND IdClinica IN (SELECT IdClinica FROM ClinicaExameDicionario WHERE IdExameDicionario=" + IdExameDicionario + ")"
            //    +" AND IdJuridicaPapel=" + (int)IndJuridicaPapel.Clinica + ")"
            //    + " OR (IdClinica IN (SELECT IdClinica FROM ClinicaCliente WHERE IdCliente=" + Request["IdEmpresa"].ToString() + ")"
            //    +" AND IdJuridicaPapel=" + (int)IndJuridicaPapel.ClinicaOutras + "))"
            //    +" AND FazComplementar=1"
            //    +" AND IsInativo=0 ORDER BY NomeAbreviado");

            ddlClinica.DataSource = new Clinica().Get("((IdClinica IN (SELECT IdClinica FROM ClinicaCliente WHERE IdCliente=" + Request["IdEmpresa"].ToString() + " )"
                 + " AND IdClinica IN (SELECT IdClinica FROM ClinicaExameDicionario WHERE IdExameDicionario=" + IdExameDicionario + ")"
                 + " AND IdJuridicaPapel=" + (int)IndJuridicaPapel.Clinica + ")"
                 + " OR (IdClinica IN (SELECT IdClinica FROM ClinicaCliente WHERE IdCliente=" + Request["IdEmpresa"].ToString() + ")"
                 + " AND IdJuridicaPapel=" + (int)IndJuridicaPapel.ClinicaOutras + "))"
                 + " AND IsInativo=0 ORDER BY NomeAbreviado");
            ddlClinica.DataValueField = "IdClinica";
            ddlClinica.DataTextField = "NomeAbreviado";
            ddlClinica.DataBind();

            ddlClinica.Items.Insert(0, new ListItem("Selecione...", "0"));
            ddlClinica.Items.Insert(1, new ListItem("Ilitera", "310"));
        }

        private void PopulaDDLMedico()
        {
            if (chk_TodosMedicos.Checked == true)
            {
                ddlMedico.DataSource = new qryMedico().Get(" ( IdJuridica=" + ddlClinica.SelectedValue + " or IdJuridica = 310 ) "
                    + " AND IsInativo=0"
                    + " AND IdMedico IN (SELECT IdJuridicaPessoa FROM JuridicaPessoa WHERE"
                    + " IdPessoa IN (SELECT IdPessoa FROM Pessoa WHERE IsInativo=0)) ORDER BY NomeCompleto");
            }
            else           
            {
                ddlMedico.DataSource = new qryMedico().Get(" ( IdJuridica=" + ddlClinica.SelectedValue + " ) "
                    + " AND IsInativo=0"
                    + " AND IdMedico IN (SELECT IdJuridicaPessoa FROM JuridicaPessoa WHERE"
                    + " IdPessoa IN (SELECT IdPessoa FROM Pessoa WHERE IsInativo=0)) ORDER BY NomeCompleto");
            }
            ddlMedico.DataTextField = "NomeCompleto";
            ddlMedico.DataValueField = "IdMedico";
            ddlMedico.DataBind();

            ddlMedico.Items.Insert(0, new ListItem("Selecione...", "0"));

            if (ddlMedico.Items.Count == 2) ddlMedico.SelectedIndex = 1;

        }

        protected void ddlClinica_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            PopulaDDLMedico();
        }

        protected void ddlTipoExame_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulaDDLClinicas(ddlTipoExame.SelectedValue.ToString());
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


            //se estiver incompleta a Anamnese, apagar e recriar

            List<Anamnese_Exame> AnExame = new Anamnese_Exame().Find<Anamnese_Exame>(" IdExameBase = " + exame.Id);


            if (AnExame.Count <= 30)
            {
                //apagar e recriar

                //foreach (Anamnese_Exame zPadrao in AnExame)
                for (int zCont = 0; zCont < AnExame.Count; zCont++)
                {
                    Anamnese_Exame rTestes = new Anamnese_Exame();
                    rTestes.Find(AnExame[zCont].Id);
                    rTestes.Delete();
                }

                Carregar_Dados_Anamnese_Exame2();

                if (AnExame.Count <= 35)
                {
                    //apagar e recriar

                    //foreach (Anamnese_Exame zPadrao in AnExame)
                    for (int zCont = 0; zCont < AnExame.Count; zCont++)
                    {
                        Anamnese_Exame rTestes = new Anamnese_Exame();
                        rTestes.Find(AnExame[zCont].Id);
                        rTestes.Delete();
                    }

                    Carregar_Dados_Anamnese_Exame2();

                }
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
                    

                    foreach (Anamnese_Dinamica zPadrao in anExamePadrao)
                    {

                        try
                        {
                            Anamnese_Exame rTestes = new Anamnese_Exame();

                            rTestes.IdAnamneseDinamica = zPadrao.Id;
                            rTestes.IdExameBase = exame.Id;
                            rTestes.Resultado = "N";
                            rTestes.Peso = zPadrao.Peso;
                            rTestes.Obs = "";                            
                            rTestes.Save();
                        }
                        catch ( Exception ex)
                        {
                            System.Diagnostics.Debug.WriteLine(ex.Message);
                        }
                        finally
                        {
          
                        }


                    }

                }

            }


            return;


        }




        private void Carregar_Dados_Anamnese_Exame2()
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

                    //    try
                    //    {
                    //        Anamnese_Exame rTestes = new Anamnese_Exame();

                    //        rTestes.IdAnamneseDinamica = zPadrao.Id;
                    //        rTestes.IdExameBase = exame.Id;
                    //        rTestes.Resultado = "N";
                    //        rTestes.Peso = zPadrao.Peso;
                    //        rTestes.Save();
                    //    }
                    //    catch (Exception ex)
                    //    {

                    //    }
                    //    finally
                    //    {

                    //    }


                    //}

                }

            }


            return;


        }



        protected void cmd_Anamnese_Click(object sender, EventArgs e)
        {

            Int32 zKey = 0;
            string zResultado = "N";
            string zObs = "";


            if (gridAnamnese.ChangedItems.Length == 0)
            {
                MsgBox1.Show("Ilitera.Net", "Não houve alterações na Anamnese", null,
                new EO.Web.MsgBoxButton("OK"));
                return;
            }

            else
            {

                // Ilitera.Data.Clientes_Funcionarios zAtualiza = new Ilitera.Data.Clientes_Funcionarios(); 

                Entities.Usuario user = (Entities.Usuario)Session["usuarioLogado"];


                Ilitera.Data.Clientes_Clinicas xEnvio = new Ilitera.Data.Clientes_Clinicas();
                xEnvio.Log_Anamnese(exame.IdEmpregado.Id, exame.Id, user.IdUsuario, "Início da Salva de Anamnese Dinâmica");


                foreach (EO.Web.GridItem item in gridAnamnese.ChangedItems)
                {
                    foreach (EO.Web.GridCell cell in item.Cells)
                    {
                        if (cell.Modified)
                        {

                            zKey = System.Convert.ToInt32(item.Key);

                            if (cell.Column.Name.ToUpper() == "RESULTADO")
                            {
                                zResultado = cell.Value.ToString().Trim();
                                try
                                {
                                    Ilitera.Data.Clientes_Funcionarios zAtualiza = new Ilitera.Data.Clientes_Funcionarios();
                                    zAtualiza.Atualizar_Anamnese_Dinamica(zKey, zResultado.Substring(0, 1));
                                }
                                catch (Exception ex)
                                {
                                    System.Diagnostics.Debug.WriteLine(ex.Message);
                                }
                                finally
                                {

                                }

                            }
                            else if (cell.Column.Name.ToUpper() == "OBS")
                            {
                                zObs = cell.Value.ToString().Trim();

                                try
                                {
                                    Ilitera.Data.Clientes_Funcionarios zAtualiza = new Ilitera.Data.Clientes_Funcionarios();
                                    zAtualiza.Atualizar_Anamnese_Dinamica_Obs(zKey, zObs);
                                }
                                catch (Exception ex)
                                {
                                    System.Diagnostics.Debug.WriteLine(ex.Message);
                                }
                                finally
                                {

                                }

                            }
                        


                            

                        }
                    }
                }


                
                Ilitera.Data.Clientes_Clinicas xEnvio2 = new Ilitera.Data.Clientes_Clinicas();
                xEnvio2.Log_Anamnese(exame.IdEmpregado.Id, exame.Id, user.IdUsuario, "Conclusão da Salva de Anamnese Dinâmica");


                MsgBox1.Show("Ilitera.Net", "Alterações na Anamnese salva.", null,
                new EO.Web.MsgBoxButton("OK"));
                return;

            }

        }


        protected void Chk_PacienteCritico_CheckedChanged(object sender, EventArgs e)
        {

        }

        //public void TabStrip_ItemClick(object sender, EO.Web.NavigationItemEventArgs e)
        //{

        //    if ( e.TabItem.Text.ToString().Trim() == "Anamnese Dinâmica")
        //    {

        //    }

        //}


        //public void TabItem_ItemClick(object sender, EO.Web.NavigationItemEventArgs e)
        //{

        //    if (e.TabItem.Text.ToString().Trim() == "Anamnese Dinâmica")
        //    {

        //    }

        //}


        private void Carregar_Imagem()
        {
            string xPath = txt_Arq.Text.Trim();

            if (xPath.Trim() == "")
            {
                cmd_PDF.Visible = false;
                ImgFunc.Visible = false;
            }
            else
            {

                if (xPath.ToUpper().IndexOf(".PDF") > 0)
                {
                    //Response.ContentType = "application/pdf";
                    //Response.AddHeader("content-disposition", "attachment; filename='" + Mestra.Common.Fotos.PathFoto_Uri(xPath)+"'");
                    //Response.WriteFile(Mestra.Common.Fotos.PathFoto_Uri(xPath));
                    //HttpContext.Current.ApplicationInstance.CompleteRequest();

                    //System.Diagnostics.Process.Start(Mestra.Common.Fotos.PathFoto_Uri(xPath));
                    
                    //Response.Redirect( "ViewPDF.aspx?Arquivo=" + xPath);
                    //Response.Write("<script>window.open('ViewPDF.aspx?" + xPath + "', '_newtab');</script>");
                    //Server.Transfer("ViewPDF.aspx?Arquivo=" + xPath);

                    cmd_Imagem.Visible = false;
                    ImgFunc.Visible = false;
                    cmd_PDF.Visible = true;


                    //Cliente cliente;
                    //cliente = new Cliente(System.Convert.ToInt32(Session["Empresa"].ToString()));

                    //cliente.IdGrupoEmpresa.Find();



                    //cmd_PDF.Attributes.Add("onclick", "window.open('" + Ilitera.Common.Fotos.PathFoto_Uri(xPath) + "', '_newtab')");
                    lbl_Path.Text =  Ilitera.Common.Fotos.PathFoto_Uri(xPath);



                    //Response.Redirect(Mestra.Common.Fotos.PathFoto_Uri(xPath) );

                    //Response.Write("<script>");
                    //Response.Write("<script>window.open('" + Mestra.Common.Fotos.PathFoto_Uri(xPath) + "', '_newtab');</script>");
                    //Response.Write("</script>");

                    //Page.ClientScript.RegisterStartupScript(this.GetType(), "Open PDF", "<script type='text/javascript'>window.open('" + Mestra.Common.Fotos.PathFoto_Uri(xPath) + "', '_newtab');</script>", true);

                }
                else
                {
                    cmd_PDF.Visible = false;
                    cmd_Imagem.Visible = true;
                }
            }

        }

        protected void cmd_Imagem_Click(object sender, EventArgs e)
        {

            Cliente cliente;
            cliente = new Cliente(System.Convert.ToInt32(Session["Empresa"].ToString()));

            cliente.IdGrupoEmpresa.Find();

            if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().IndexOf("Prajna") >= 0 && cliente.IdGrupoEmpresa.Id != -905238295 && cliente.IdGrupoEmpresa.Id != -682029520 && cliente.IdGrupoEmpresa.Id != -768119567 && cliente.IdGrupoEmpresa.Id != 1577634488)
                return;

            string xPath = txt_Arq.Text.Trim();

            ImgFunc.Visible = false;
            //ImgFunc.ImageUrl = Ilitera.Common.Fotos.PathFoto_Uri(xPath, ConfigurationManager.AppSettings["Servidor_Web"].ToString());

            lbl_Path.Text = Ilitera.Common.Fotos.PathFoto_Uri(xPath);

            System.Net.WebClient client = new System.Net.WebClient();
            Byte[] buffer = client.DownloadData(lbl_Path.Text);
            Response.ContentType = "image/jpeg";
            Response.AddHeader("Content-Disposition", "attachment; filename=" + lbl_Path.Text);
            Response.AddHeader("content-length", buffer.Length.ToString());
            Response.BinaryWrite(buffer);
            Response.End();
        }



        private void Carregar_Exames_Especiais()
        {

            Empregado_Aptidao rAptidao = new Empregado_Aptidao();
            rAptidao.Find(" nId_Empregado = " + exame.IdEmpregado.Id.ToString());
            
            if (rAptidao.Id != 0)
            {

                if ( rAptidao.apt_Alimento == true )
                {
                    chk_apt_Alimento.Enabled = true;
                    chk_inapt_Alimento.Enabled = true;
                    lbl_Apt_Alimento.Enabled = true;
                    chk_apt_Alimento.ForeColor = System.Drawing.Color.Black;
                    chk_inapt_Alimento.ForeColor = System.Drawing.Color.Black;
                    lbl_Apt_Alimento.ForeColor = System.Drawing.Color.Black;
                }

                if ( rAptidao.apt_Aquaviario == true )
                {
                    chk_apt_Aquaviarios.Enabled = true;
                    chk_inapt_Aquaviarios.Enabled = true;
                    lbl_apt_Aquaviarios.Enabled = true;
                    chk_apt_Aquaviarios.ForeColor = System.Drawing.Color.Black;
                    chk_inapt_Aquaviarios.ForeColor = System.Drawing.Color.Black;
                    lbl_apt_Aquaviarios.ForeColor = System.Drawing.Color.Black;
                }

                if ( rAptidao.apt_Brigadista == true )
                {
                    chk_Apt_Brigadista.Enabled = true;
                    chk_Inapt_Brigadista.Enabled = true;
                    lbl_Apt_Brigadista.Enabled = true;
                    chk_Apt_Brigadista.ForeColor = System.Drawing.Color.Black;
                    chk_Inapt_Brigadista.ForeColor = System.Drawing.Color.Black;
                    lbl_Apt_Brigadista.ForeColor = System.Drawing.Color.Black;
                }

                if (rAptidao.apt_Radiacao == true)
                {
                    chk_Apt_Radiacao.Enabled = true;
                    chk_Inapt_Radiacao.Enabled = true;
                    lbl_Apt_Radiacao.Enabled = true;
                    chk_Apt_Radiacao.ForeColor = System.Drawing.Color.Black;
                    chk_Inapt_Radiacao.ForeColor = System.Drawing.Color.Black;
                    lbl_Apt_Radiacao.ForeColor = System.Drawing.Color.Black;
                }


                if ( rAptidao.apt_Eletricidade == true )
                {
                    chk_apt_Eletricidade.Enabled = true;
                    chk_inapt_Eletricidade.Enabled = true;
                    lbl_apt_Eletricidade.Enabled = true;
                    chk_apt_Eletricidade.ForeColor = System.Drawing.Color.Black;
                    chk_inapt_Eletricidade.ForeColor = System.Drawing.Color.Black;
                    lbl_apt_Eletricidade.ForeColor = System.Drawing.Color.Black;
                }

                if ( rAptidao.apt_Espaco_Confinado == true )
                {                    
                    chk_apt_Confinado.Enabled = true;
                    chk_inapt_Confinado.Enabled = true;
                    lbl_apt_Confinado.Enabled = true;
                    chk_apt_Confinado.ForeColor = System.Drawing.Color.Black;
                    chk_inapt_Confinado.ForeColor = System.Drawing.Color.Black;
                    lbl_apt_Confinado.ForeColor = System.Drawing.Color.Black;
                }

                if ( rAptidao.apt_Socorrista == true )
                {
                    chk_Apt_Socorrista.Enabled = true;
                    chk_Inapt_Socorrista.Enabled = true;
                    lbl_Apt_Socorrista.Enabled = true;
                    chk_Apt_Socorrista.ForeColor = System.Drawing.Color.Black;
                    chk_Inapt_Socorrista.ForeColor = System.Drawing.Color.Black;
                    lbl_Apt_Socorrista.ForeColor = System.Drawing.Color.Black;
                }

                if (rAptidao.apt_Respirador == true)
                {
                    chk_Apt_Respirador.Enabled = true;
                    chk_Inapt_Respirador.Enabled = true;
                    lbl_Apt_Respirador.Enabled = true;
                    chk_Apt_Respirador.ForeColor = System.Drawing.Color.Black;
                    chk_Inapt_Respirador.ForeColor = System.Drawing.Color.Black;
                    lbl_Apt_Respirador.ForeColor = System.Drawing.Color.Black;
                }

                if ( rAptidao.apt_Submerso == true )
                {
                    chk_apt_Submerso.Enabled = true;
                    chk_inapt_Submerso.Enabled = true;
                    lbl_apt_Submerso.Enabled = true;
                    chk_apt_Submerso.ForeColor = System.Drawing.Color.Black;
                    chk_inapt_Submerso.ForeColor = System.Drawing.Color.Black;
                    lbl_apt_Submerso.ForeColor = System.Drawing.Color.Black;
                }

                if ( rAptidao.apt_Trabalho_Altura == true )
                {
                    chk_apt_Altura.Enabled = true;
                    chk_inapt_Altura.Enabled = true;
                    lbl_apt_Altura.Enabled = true;
                    chk_apt_Altura.ForeColor = System.Drawing.Color.Black;
                    chk_inapt_Altura.ForeColor = System.Drawing.Color.Black;
                    lbl_apt_Altura.ForeColor = System.Drawing.Color.Black;
                }

                if ( rAptidao.apt_Transporte == true )
                {
                    chk_apt_Transportes.Enabled = true;
                    chk_inapt_Transportes.Enabled = true;
                    lbl_apt_Transportes.Enabled = true;
                    chk_apt_Transportes.ForeColor = System.Drawing.Color.Black;
                    chk_inapt_Transportes.ForeColor = System.Drawing.Color.Black;
                    lbl_apt_Transportes.ForeColor = System.Drawing.Color.Black;
                }
                
            
            }

            exame.IdEmpregadoFuncao.Find();
            exame.IdPcmso.Find();
            exame.IdPcmso.IdLaudoTecnico.Find();

            GHE_Aptidao zAptidao = new GHE_Aptidao();
            zAptidao.Find("nId_Func = " +  exame.IdEmpregadoFuncao.GetIdGheEmpregado(exame.IdPcmso.IdLaudoTecnico));


            if (zAptidao.Id != 0)
            {

                if (zAptidao.apt_Alimento == true)
                {
                    chk_apt_Alimento.Enabled = true;
                    chk_inapt_Alimento.Enabled = true;
                    lbl_Apt_Alimento.Enabled = true;
                    chk_apt_Alimento.ForeColor = System.Drawing.Color.Black;
                    chk_inapt_Alimento.ForeColor = System.Drawing.Color.Black;
                    lbl_Apt_Alimento.ForeColor = System.Drawing.Color.Black;
                }

                if (zAptidao.apt_Aquaviario == true)
                {
                    chk_apt_Aquaviarios.Enabled = true;
                    chk_inapt_Aquaviarios.Enabled = true;
                    lbl_apt_Aquaviarios.Enabled = true;
                    chk_apt_Aquaviarios.ForeColor = System.Drawing.Color.Black;
                    chk_inapt_Aquaviarios.ForeColor = System.Drawing.Color.Black;
                    lbl_apt_Aquaviarios.ForeColor = System.Drawing.Color.Black;
                }

                if (zAptidao.apt_Brigadista == true)
                {
                    chk_Apt_Brigadista.Enabled = true;
                    chk_Inapt_Brigadista.Enabled = true;
                    lbl_Apt_Brigadista.Enabled = true;
                    chk_Apt_Brigadista.ForeColor = System.Drawing.Color.Black;
                    chk_Inapt_Brigadista.ForeColor = System.Drawing.Color.Black;
                    lbl_Apt_Brigadista.ForeColor = System.Drawing.Color.Black;
                }

                if (zAptidao.apt_Radiacao == true)
                {
                    chk_Apt_Radiacao.Enabled = true;
                    chk_Inapt_Radiacao.Enabled = true;
                    lbl_Apt_Radiacao.Enabled = true;
                    chk_Apt_Radiacao.ForeColor = System.Drawing.Color.Black;
                    chk_Inapt_Radiacao.ForeColor = System.Drawing.Color.Black;
                    lbl_Apt_Radiacao.ForeColor = System.Drawing.Color.Black;
                }


                if (zAptidao.apt_Eletricidade == true)
                {
                    chk_apt_Eletricidade.Enabled = true;
                    chk_inapt_Eletricidade.Enabled = true;
                    lbl_apt_Eletricidade.Enabled = true;
                    chk_apt_Eletricidade.ForeColor = System.Drawing.Color.Black;
                    chk_inapt_Eletricidade.ForeColor = System.Drawing.Color.Black;
                    lbl_apt_Eletricidade.ForeColor = System.Drawing.Color.Black;
                }

                if (zAptidao.apt_Espaco_Confinado == true)
                {
                    chk_apt_Confinado.Enabled = true;
                    chk_inapt_Confinado.Enabled = true;
                    lbl_apt_Confinado.Enabled = true;
                    chk_apt_Confinado.ForeColor = System.Drawing.Color.Black;
                    chk_inapt_Confinado.ForeColor = System.Drawing.Color.Black;
                    lbl_apt_Confinado.ForeColor = System.Drawing.Color.Black;
                }

                if (zAptidao.apt_Socorrista == true)
                {
                    chk_Apt_Socorrista.Enabled = true;
                    chk_Inapt_Socorrista.Enabled = true;
                    lbl_Apt_Socorrista.Enabled = true;
                    chk_Apt_Socorrista.ForeColor = System.Drawing.Color.Black;
                    chk_Inapt_Socorrista.ForeColor = System.Drawing.Color.Black;
                    lbl_Apt_Socorrista.ForeColor = System.Drawing.Color.Black;
                }

                //if (zAptidao.apt_Respirador == true)
                //{
                //    chk_Apt_Respirador.Enabled = true;
                //    chk_Inapt_Respirador.Enabled = true;
                //    lbl_Apt_Respirador.Enabled = true;
                //    chk_Apt_Respirador.ForeColor = System.Drawing.Color.Black;
                //    chk_Inapt_Respirador.ForeColor = System.Drawing.Color.Black;
                //    lbl_Apt_Respirador.ForeColor = System.Drawing.Color.Black;
                //}


                if (zAptidao.apt_Submerso == true)
                {
                    chk_apt_Submerso.Enabled = true;
                    chk_inapt_Submerso.Enabled = true;
                    lbl_apt_Submerso.Enabled = true;
                    chk_apt_Submerso.ForeColor = System.Drawing.Color.Black;
                    chk_inapt_Submerso.ForeColor = System.Drawing.Color.Black;
                    lbl_apt_Submerso.ForeColor = System.Drawing.Color.Black;
                }

                if (zAptidao.apt_Trabalho_Altura == true)
                {
                    chk_apt_Altura.Enabled = true;
                    chk_inapt_Altura.Enabled = true;
                    lbl_apt_Altura.Enabled = true;
                    chk_apt_Altura.ForeColor = System.Drawing.Color.Black;
                    chk_inapt_Altura.ForeColor = System.Drawing.Color.Black;
                    lbl_apt_Altura.ForeColor = System.Drawing.Color.Black;
                }

                if (zAptidao.apt_Transporte == true)
                {
                    chk_apt_Transportes.Enabled = true;
                    chk_inapt_Transportes.Enabled = true;
                    lbl_apt_Transportes.Enabled = true;
                    chk_apt_Transportes.ForeColor = System.Drawing.Color.Black;
                    chk_inapt_Transportes.ForeColor = System.Drawing.Color.Black;
                    lbl_apt_Transportes.ForeColor = System.Drawing.Color.Black;
                }


            }




            return;

        }


        private void Ativar_Exames_Especiais()
        {

            chk_apt_Alimento.Enabled = true;
            chk_inapt_Alimento.Enabled = true;
            lbl_Apt_Alimento.Enabled = true;
            chk_apt_Alimento.ForeColor = System.Drawing.Color.Black;
            chk_inapt_Alimento.ForeColor = System.Drawing.Color.Black;
            lbl_Apt_Alimento.ForeColor = System.Drawing.Color.Black;

            chk_apt_Aquaviarios.Enabled = true;
            chk_inapt_Aquaviarios.Enabled = true;
            lbl_apt_Aquaviarios.Enabled = true;
            chk_apt_Aquaviarios.ForeColor = System.Drawing.Color.Black;
            chk_inapt_Aquaviarios.ForeColor = System.Drawing.Color.Black;
            lbl_apt_Aquaviarios.ForeColor = System.Drawing.Color.Black;

            chk_Apt_Brigadista.Enabled = true;
            chk_Inapt_Brigadista.Enabled = true;
            lbl_Apt_Brigadista.Enabled = true;
            chk_Apt_Brigadista.ForeColor = System.Drawing.Color.Black;
            chk_Inapt_Brigadista.ForeColor = System.Drawing.Color.Black;
            lbl_Apt_Brigadista.ForeColor = System.Drawing.Color.Black;

            chk_Apt_Radiacao.Enabled = true;
            chk_Inapt_Radiacao.Enabled = true;
            lbl_Apt_Radiacao.Enabled = true;
            chk_Apt_Radiacao.ForeColor = System.Drawing.Color.Black;
            chk_Inapt_Radiacao.ForeColor = System.Drawing.Color.Black;
            lbl_Apt_Radiacao.ForeColor = System.Drawing.Color.Black;

            chk_apt_Eletricidade.Enabled = true;
            chk_inapt_Eletricidade.Enabled = true;
            lbl_apt_Eletricidade.Enabled = true;
            chk_apt_Eletricidade.ForeColor = System.Drawing.Color.Black;
            chk_inapt_Eletricidade.ForeColor = System.Drawing.Color.Black;
            lbl_apt_Eletricidade.ForeColor = System.Drawing.Color.Black;

            chk_apt_Confinado.Enabled = true;
            chk_inapt_Confinado.Enabled = true;
            lbl_apt_Confinado.Enabled = true;
            chk_apt_Confinado.ForeColor = System.Drawing.Color.Black;
            chk_inapt_Confinado.ForeColor = System.Drawing.Color.Black;
            lbl_apt_Confinado.ForeColor = System.Drawing.Color.Black;

            chk_Apt_Socorrista.Enabled = true;
            chk_Inapt_Socorrista.Enabled = true;
            lbl_Apt_Socorrista.Enabled = true;
            chk_Apt_Socorrista.ForeColor = System.Drawing.Color.Black;
            chk_Inapt_Socorrista.ForeColor = System.Drawing.Color.Black;
            lbl_Apt_Socorrista.ForeColor = System.Drawing.Color.Black;

            chk_Apt_Respirador.Enabled = true;
            chk_Inapt_Respirador.Enabled = true;
            lbl_Apt_Respirador.Enabled = true;
            chk_Apt_Respirador.ForeColor = System.Drawing.Color.Black;
            chk_Inapt_Respirador.ForeColor = System.Drawing.Color.Black;
            lbl_Apt_Respirador.ForeColor = System.Drawing.Color.Black;

            chk_apt_Submerso.Enabled = true;
            chk_inapt_Submerso.Enabled = true;
            lbl_apt_Submerso.Enabled = true;
            chk_apt_Submerso.ForeColor = System.Drawing.Color.Black;
            chk_inapt_Submerso.ForeColor = System.Drawing.Color.Black;
            lbl_apt_Submerso.ForeColor = System.Drawing.Color.Black;

            chk_apt_Altura.Enabled = true;
            chk_inapt_Altura.Enabled = true;
            lbl_apt_Altura.Enabled = true;
            chk_apt_Altura.ForeColor = System.Drawing.Color.Black;
            chk_inapt_Altura.ForeColor = System.Drawing.Color.Black;
            lbl_apt_Altura.ForeColor = System.Drawing.Color.Black;

            chk_apt_Transportes.Enabled = true;
            chk_inapt_Transportes.Enabled = true;
            lbl_apt_Transportes.Enabled = true;
            chk_apt_Transportes.ForeColor = System.Drawing.Color.Black;
            chk_inapt_Transportes.ForeColor = System.Drawing.Color.Black;
            lbl_apt_Transportes.ForeColor = System.Drawing.Color.Black;

            return;

        }

        protected void chk_TodosMedicos_CheckedChanged(object sender, EventArgs e)
        {
            PopulaDDLMedico();
        }

        protected void cmd_PDF_Click(object sender, EventArgs e)
        {
            if (lbl_Path.Text != "")
            {
                System.Net.WebClient client = new System.Net.WebClient();
                Byte[] buffer = client.DownloadData(lbl_Path.Text);
                Response.ContentType = "application/pdf";
                Response.AddHeader("Content-Disposition", "attachment; filename=" + lbl_Path.Text);
                Response.AddHeader("content-length", buffer.Length.ToString());
                Response.BinaryWrite(buffer);
                Response.End();
            }
        }


        //private void ckbOlhosNormal_CheckedChanged(object sender, EventArgs e)
        //{

        //    if (ckbOlhosNormal.Checked == true)
        //    {
        //        ckbOculos.Checked = false;
        //    }

        //}


        protected void ckbOlhosNormal_CheckedChanged1(object sender, EventArgs e)
        {
                if (ckbOlhosNormal.Checked == true)
                {
                    ckbOculos.Checked = false;
                }
        }

        protected void ckbCabecaNormal_CheckedChanged(object sender, EventArgs e)
        {

            if (ckbCabecaNormal.Checked == true)
            {
                ckbTireoide.Checked = false;
                ckbDente.Checked = false;
                ckbOuvido.Checked = false;
                ckbAdenomegalia.Checked = false;
                ckbGarganta.Checked = false;
            }

        }

        protected void ckbOmbrosNormal_CheckedChanged(object sender, EventArgs e)
        {

            if (ckbOmbrosNormal.Checked == true)
            {
                ckbRotacao.Checked = false;
                ckbPalpacaoBiceps.Checked = false;
                ckbPalpacaoSubacromial.Checked = false;
            }


        }

        protected void ckbMINormal_CheckedChanged(object sender, EventArgs e)
        {

            if (ckbMINormal.Checked == true)
            {
                ckbPulsos.Checked = false;
                ckbVarizes.Checked = false;
                ckbEdemas.Checked = false;
                ckbArticular.Checked = false;
            }

        }


        protected void ckbRotacao_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbRotacao.Checked == true)
            {
                ckbOmbrosNormal.Checked = false;
            }

        }

        protected void ckbPalpacaoBiceps_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbPalpacaoBiceps.Checked == true)
            {
                ckbOmbrosNormal.Checked = false;
            }
        }

        protected void ckbPalpacaoSubacromial_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbPalpacaoSubacromial.Checked == true)
            {
                ckbOmbrosNormal.Checked = false;
            }
        }

        protected void ckbTireoide_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbTireoide.Checked == true)
            {
                ckbCabecaNormal.Checked = false;
            }
        }

        protected void ckbDente_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbDente.Checked == true)
            {
                ckbCabecaNormal.Checked = false;
            }
        }

        protected void ckbGarganta_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbGarganta.Checked == true)
            {
                ckbCabecaNormal.Checked = false;
            }
        }

        protected void ckbAdenomegalia_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbAdenomegalia.Checked == true)
            {
                ckbCabecaNormal.Checked = false;
            }
        }

        protected void ckbOuvido_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbOuvido.Checked == true)
            {
                ckbCabecaNormal.Checked = false;
            }
        }

        //protected void ckbOculos_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (ckbOculos.Checked == true)
        //    {
        //        ckbOlhosNormal.Checked = false;
        //    }
        //}


        protected void ckbOculos_CheckedChanged1(object sender, EventArgs e)
        {
                if (ckbOculos.Checked == true)
                {
                    ckbOlhosNormal.Checked = false;
                }
        }



        protected void ckbEdemas_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbEdemas.Checked == true)
            {
                ckbMINormal.Checked = false;
            }
        }

        protected void ckbEdemas_CheckedChanged1(object sender, EventArgs e)
        {
            if (ckbEdemas.Checked == true)
            {
                ckbMINormal.Checked = false;
            }

        }


        protected void ckbPulsos_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbPulsos.Checked == true)
            {
                ckbMINormal.Checked = false;
            }
        }

        protected void ckbPulsos_CheckedChanged1(object sender, EventArgs e)
        {
            if (ckbPulsos.Checked == true)
            {
                ckbMINormal.Checked = false;
            }

        }


        protected void ckbArticular_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbArticular.Checked == true)
            {
                ckbMINormal.Checked = false;
            }
        }

        protected void ckbArticular_CheckedChanged1(object sender, EventArgs e)
        {
            if (ckbArticular.Checked == true)
            {
                ckbMINormal.Checked = false;
            }

        }



        protected void ckbVarizes_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbVarizes.Checked == true)
            {
                ckbMINormal.Checked = false;
            }
        }


        protected void ckbVarizes_CheckedChanged1(object sender, EventArgs e)
        {
            if (ckbVarizes.Checked == true)
            {
                ckbMINormal.Checked = false;
            }

        }

        protected void ckbLasegue_CheckedChanged1(object sender, EventArgs e)
        {
            if (ckbLasegue.Checked == true)
            {
                ckbColunaNormal.Checked = false;
            }

        }

        protected void ckbFlexao_CheckedChanged1(object sender, EventArgs e)
        {
            if (ckbFlexao.Checked == true)
            {
                ckbColunaNormal.Checked = false;
            }
        }

        protected void ckbEscoliose_CheckedChanged1(object sender, EventArgs e)
        {
            if (ckbEscoliose.Checked == true)
            {
                ckbColunaNormal.Checked = false;
            }
        }

        protected void ckbLordose_CheckedChanged1(object sender, EventArgs e)
        {
            if (ckbLordose.Checked == true)
            {
                ckbColunaNormal.Checked = false;
            }
        }

        protected void ckbCifose_CheckedChanged1(object sender, EventArgs e)
        {
            if (ckbCifose.Checked == true)
            {
                ckbColunaNormal.Checked = false;
            }

        }

        protected void ckbColunaNormal_CheckedChanged1(object sender, EventArgs e)
        {

            if (ckbColunaNormal.Checked == true)
            {
                ckbLordose.Checked = false;
                ckbCifose.Checked = false;
                ckbFlexao.Checked = false;
                ckbLasegue.Checked = false;
                ckbEscoliose.Checked = false;
            }
        }


        protected void ckbColunaNormal_CheckedChanged(object sender, EventArgs e)
        {

            if (ckbColunaNormal.Checked == true)
            {
                ckbLordose.Checked = false;
                ckbCifose.Checked = false;
                ckbFlexao.Checked = false;
                ckbLasegue.Checked = false;
                ckbEscoliose.Checked = false;
            }

        }

        protected void ckbEscoliose_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbEscoliose.Checked == true)
            {
                ckbColunaNormal.Checked = false;
            }
        }

        protected void ckbLasegue_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbLasegue.Checked == true)
            {
                ckbColunaNormal.Checked = false;
            }

        }

        protected void ckbFlexao_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbFlexao.Checked == true)
            {
                ckbColunaNormal.Checked = false;
            }

        }

        protected void ckbLordose_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbLordose.Checked == true)
            {
                ckbColunaNormal.Checked = false;
            }

        }

        protected void ckbCifose_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbCifose.Checked == true)
            {
                ckbColunaNormal.Checked = false;
            }

        }

        protected void ckbPeleNormal_CheckedChanged(object sender, EventArgs e)
        {

            if (ckbPeleNormal.Checked == true)
            {
                ckbDescoradas.Checked = false;
                ckbIctericas.Checked = false;
                ckbCianoticas.Checked = false;
                ckbDermatoses.Checked = false;
            }
        }

        protected void ckbDescoradas_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbDescoradas.Checked == true)
            {
                ckbPeleNormal.Checked = false;
            }
        }

        protected void ckbIctericas_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbIctericas.Checked == true)
            {
                ckbPeleNormal.Checked = false;
            }
        }

        protected void ckbCianoticas_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbCianoticas.Checked == true)
            {
                ckbPeleNormal.Checked = false;
            }
        }

        protected void ckbDermatoses_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbDermatoses.Checked == true)
            {
                ckbPeleNormal.Checked = false;
            }
        }
        




        protected void ckbMaosNormal_CheckedChanged(object sender, EventArgs e)
        {


            if (ckbMaosNormal.Checked == true)
            {
                ckbFinkelsteinD.Checked = false;
                ckbFinkelsteinE.Checked = false;
                ckbTinelD.Checked = false;
                ckbTinelE.Checked = false;
                ckbNodulosD.Checked = false;
                ckbNodulosE.Checked = false;
                ckbCrepitacaoD.Checked = false;
                ckbCrepitacaoE.Checked = false;
                ckbFalenD.Checked = false;
                ckbFalenE.Checked = false;
                ckbEdemaD.Checked = false;
                ckbEdemaE.Checked = false;
                ckbPrensaoD.Checked = false;
                ckbPrensaoE.Checked = false;
            }

        }

        protected void ckbTinelD_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbTinelD.Checked == true)
            {
                ckbMaosNormal.Checked = false;
            }
        }

        protected void ckbTinelE_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbTinelE.Checked == true)
            {
                ckbMaosNormal.Checked = false;
            }

        }

        protected void ckbFinkelsteinD_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbFinkelsteinD.Checked == true)
            {
                ckbMaosNormal.Checked = false;
            }

        }

        protected void ckbFinkelsteinE_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbFinkelsteinE.Checked == true)
            {
                ckbMaosNormal.Checked = false;
            }

        }

        protected void ckbNodulosD_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbNodulosD.Checked == true)
            {
                ckbMaosNormal.Checked = false;
            }

        }

        protected void ckbNodulosE_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbNodulosE.Checked == true)
            {
                ckbMaosNormal.Checked = false;
            }

        }

        protected void ckbCrepitacaoD_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbCrepitacaoD.Checked == true)
            {
                ckbMaosNormal.Checked = false;
            }

        }

        protected void ckbCrepitacaoE_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbCrepitacaoE.Checked == true)
            {
                ckbMaosNormal.Checked = false;
            }

        }

        protected void ckbFalenD_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbFalenD.Checked == true)
            {
                ckbMaosNormal.Checked = false;
            }

        }

        protected void ckbFalenE_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbFalenE.Checked == true)
            {
                ckbMaosNormal.Checked = false;
            }

        }

        protected void ckbEdemaD_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbEdemaD.Checked == true)
            {
                ckbMaosNormal.Checked = false;
            }

        }

        protected void ckbEdemaE_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbEdemaE.Checked == true)
            {
                ckbMaosNormal.Checked = false;
            }

        }

        protected void ckbPrensaoD_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbPrensaoD.Checked == true)
            {
                ckbMaosNormal.Checked = false;
            }

        }

        protected void ckbPrensaoE_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbPrensaoE.Checked == true)
            {
                ckbMaosNormal.Checked = false;
            }

        }



        protected void ckbBRNF_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbBRNF.Checked == true)
            {
                ckbSopros.Checked = false;
                ckbArritmias.Checked = false;
                ckbCardioVascularOutros.Checked = false;
            }
        }

        protected void ckbSopros_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbSopros.Checked == true)
            {
                ckbBRNF.Checked = false;
            }
        }

        protected void ckbArritmias_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbArritmias.Checked == true)
            {
                ckbBRNF.Checked = false;
            }
        }

        protected void ckbCardioVascularOutros_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbCardioVascularOutros.Checked == true)
            {
                ckbBRNF.Checked = false;
            }
        }

        protected void ckbMVsemRA_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbMVsemRA.Checked == true)
            {
                ckbMVAlterado.Checked = false;
                ckbRuidos.Checked = false;
            }

        }

        protected void ckbMVAlterado_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbMVAlterado.Checked == true)
            {
                ckbMVsemRA.Checked = false;
            }

        }

        protected void ckbRuidos_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbRuidos.Checked == true)
            {
                ckbMVsemRA.Checked = false;
            }
        }

        protected void ckbPlano_CheckedChanged(object sender, EventArgs e)
        {

            if (ckbPlano.Checked == true)
            {
                ckbPalpacao.Checked = false;
                ckbMassas.Checked = false;
                ckbHepatoesplenomegalia.Checked = false;
            }
        }

        protected void ckbPalpacao_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbPalpacao.Checked == true)
            {
                ckbPlano.Checked = false;
            }
        }

        protected void ckbMassas_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbMassas.Checked == true)
            {
                ckbPlano.Checked = false;
            }
        }

        protected void ckbHepatoesplenomegalia_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbHepatoesplenomegalia.Checked == true)
            {
                ckbPlano.Checked = false;
            }
        }

    }
}