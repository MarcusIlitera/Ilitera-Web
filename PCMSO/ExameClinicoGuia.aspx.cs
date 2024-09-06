using System;
using System.Web.SessionState;
using System.Data;
using System.Web.UI.WebControls;
using System.Text;
using System.Collections;
using Ilitera.Common;
using Ilitera.Opsa.Data;
using System.Globalization;
//using MestraNET;
using System.Drawing;
using System.Web;
using System.Data.SqlClient;

namespace Ilitera.Net
{
    public partial class ExameClinicoGuia : System.Web.UI.Page
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

            ////InicializaExameClinico();
            InicializaWebPageObjects();

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

                ddlClinica.Items.Insert(0, new ListItem("Selecione...", "0"));

                RegisterClientCode();
                PopulaDropDownExame((int)IndTipoExame.Clinico);
                PopulaUltraWebGrid(grd_Complementares, GetExamesComplementares(), lblTotRegistros);
                PopulaLsbFatorRisco();
                if (!exame.Id.Equals(0))   //quando abre exame já salvo, checar porque não seleciona checkboxs selecionados. - Wagner.
                {
                    PopulaTelaExame();
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
            else
            {
                if (txtAuxiliar.Value.Equals("atualizaFatorRisco"))
                {
                    txtAuxiliar.Value = string.Empty;
                    PopulaLsbFatorRisco();
                    PopulaLsbFatorRiscoSel();
                }
            }
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
            Response.Redirect("DadosEmpregado.aspx?IdEmpregado=" + Session["Empregado"].ToString() + "&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString() + "&Tipo=7");
        }




        private void PopulaDDLClinicas(string IdExameDicionario)
        {
           

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



        //#region Popula ListBox&DropDown

        private void PopulaLsbFatorRisco()
        {
            lsbFatorRisco.DataSource = new FatorRisco().GetIdNome("Nome", "IdCliente=" + Request["IdEmpresa"]
                + " AND IdFatorRisco NOT IN (SELECT IdFatorRisco FROM FatorRiscoClinico WHERE IdClinico=" + exame.Id + ")");

            lsbFatorRisco.DataValueField = "Id";
            lsbFatorRisco.DataTextField = "Nome";
            lsbFatorRisco.DataBind();

            if (lsbFatorRisco.Items.Count.Equals(0) || exame.Id.Equals(0))
            {
                imbAdd.ImageUrl = "img/rightDisabled.jpg";
                imbAddAll.ImageUrl = "img/rightAllDisabled.jpg";
            }
            else
            {
                imbAdd.ImageUrl = "img/right.jpg";
                imbAddAll.ImageUrl = "img/rightAllEnabled.jpg";
            }

            imbAdd.Enabled = (!lsbFatorRisco.Items.Count.Equals(0) && !exame.Id.Equals(0));
            imbAddAll.Enabled = (!lsbFatorRisco.Items.Count.Equals(0) && !exame.Id.Equals(0));
        }


        private void PopulaLsbFatorRiscoSel()
        {
            lsbFatorRiscoSel.DataSource = new FatorRisco().GetIdNome("Nome", "IdFatorRisco IN (SELECT IdFatorRisco FROM FatorRiscoClinico WHERE IdClinico=" + exame.Id + ")");

            lsbFatorRiscoSel.DataValueField = "Id";
            lsbFatorRiscoSel.DataTextField = "Nome";
            lsbFatorRiscoSel.DataBind();

            if (lsbFatorRiscoSel.Items.Count.Equals(0))
            {
                imbRemove.ImageUrl = "img/leftDisabled.jpg";
                imbRemoveAll.ImageUrl = "img/leftAllDisabled.jpg";
            }
            else
            {
                imbRemove.ImageUrl = "img/left.jpg";
                imbRemoveAll.ImageUrl = "img/leftAllEnabled.jpg";
            }

            imbRemove.Enabled = !lsbFatorRiscoSel.Items.Count.Equals(0);
            imbRemoveAll.Enabled = !lsbFatorRiscoSel.Items.Count.Equals(0);
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

            lkb_ASOGuia.Attributes.Add("onClick", "javascript:" + strOpenReport("PCMSO", "ASOGuiaEmpregado.aspx?IliteraSystem=" + strAux.ToString() + strAux.ToString()
                + "&IdExame=" + exame.Id + "&IdEmpresa=" + Request["IdEmpresa"] + "&IdEmpregado=" + Request["IdEmpregado"] + "&IdUsuario=" + Request["IdUsuario"], "ASO"));

            //else
            //    lkbASO.Attributes.Add("onClick", "javascript:window.alert('É necessário colocar o resultado do exame e salvá-lo (Apto ou Inapto) antes de emitir o ASO!');");

            btnNovoFatorRisco.Attributes.Add("onClick", "javascript:addItemPop(centerWin('CadFatorRisco.aspx?IdEmpresa=" + Request["IdEmpresa"] + "&IdUsuario=" + Request["IdUsuario"] + "', 580, 390, 'CadQueixaClinica'))");
            lbtAusente.Attributes.Add("onClick", "javascript:return(confirm('Atenção! O Exame Clínico será excluído e cadastrado como exame não realizado por motivo de falta! Deseja realmente continuar?'))");
            btnExcluir.Attributes.Add("onClick", "javascript:return(confirm('Deseja mesmo excluir este exame?'))");
            btnResetText.Attributes.Add("onClick", "javascript:return(confirm('Atenção! Ao atualizar as anotações, todas as informações adicionais digitadas serão perdidas! O texto padrão referente aos itens selecionados na Anamnese e no Exame Físico será gerado e substituirá o atual! Deseja continuar?'))");

            btnOK.Attributes.Add("onClick", "javascript:return VerificaProcesso()");

            ckbQueixaS.Attributes.Add("onClick", "ConsisteCkeckBoxDeAlteracao(this)");
            ckbQueixaN.Attributes.Add("onClick", "ConsisteCkeckBoxDeAlteracao(this)");
            ckbDeficienciaN.Attributes.Add("onClick", "ConsisteCkeckBoxDeAlteracao(this)");
            ckbDeficienciaS.Attributes.Add("onClick", "ConsisteCkeckBoxDeAlteracao(this)");
            ckbEtilismoN.Attributes.Add("onClick", "ConsisteCkeckBoxDeAlteracao(this)");
            ckbEtilismoS.Attributes.Add("onClick", "ConsisteCkeckBoxDeAlteracao(this)");
            ckbTraumaN.Attributes.Add("onClick", "ConsisteCkeckBoxDeAlteracao(this)");
            ckbTraumaS.Attributes.Add("onClick", "ConsisteCkeckBoxDeAlteracao(this)");
            ckbTabagismoN.Attributes.Add("onClick", "ConsisteCkeckBoxDeAlteracao(this)");
            ckbTabagismoS.Attributes.Add("onClick", "ConsisteCkeckBoxDeAlteracao(this)");
            ckbCirurgiaN.Attributes.Add("onClick", "ConsisteCkeckBoxDeAlteracao(this)");
            ckbCirurgiaS.Attributes.Add("onClick", "ConsisteCkeckBoxDeAlteracao(this)");
            ckbMedicamentosN.Attributes.Add("onClick", "ConsisteCkeckBoxDeAlteracao(this)");
            ckbMedicamentosS.Attributes.Add("onClick", "ConsisteCkeckBoxDeAlteracao(this)");
            ckbDoencaN.Attributes.Add("onClick", "ConsisteCkeckBoxDeAlteracao(this)");
            ckbDoencaS.Attributes.Add("onClick", "ConsisteCkeckBoxDeAlteracao(this)");
            ckbAnteFamiN.Attributes.Add("onClick", "ConsisteCkeckBoxDeAlteracao(this)");
            ckbAnteFamiS.Attributes.Add("onClick", "ConsisteCkeckBoxDeAlteracao(this)");
            ckbAfastadoN.Attributes.Add("onClick", "ConsisteCkeckBoxDeAlteracao(this)");
            ckbAfastadoS.Attributes.Add("onClick", "ConsisteCkeckBoxDeAlteracao(this)");
            ckbMIN.Attributes.Add("onClick", "ConsisteCkeckBoxDeAlteracao(this)");
            ckbMIS.Attributes.Add("onClick", "ConsisteCkeckBoxDeAlteracao(this)");
            ckbCoracaoN.Attributes.Add("onClick", "ConsisteCkeckBoxDeAlteracao(this)");
            ckbCoracaoS.Attributes.Add("onClick", "ConsisteCkeckBoxDeAlteracao(this)");
            ckbMSN.Attributes.Add("onClick", "ConsisteCkeckBoxDeAlteracao(this)");
            ckbMSS.Attributes.Add("onClick", "ConsisteCkeckBoxDeAlteracao(this)");
            ckbCabecaN.Attributes.Add("onClick", "ConsisteCkeckBoxDeAlteracao(this)");
            ckbCabecaS.Attributes.Add("onClick", "ConsisteCkeckBoxDeAlteracao(this)");
            ckbAbdomemN.Attributes.Add("onClick", "ConsisteCkeckBoxDeAlteracao(this)");
            ckbAbdomemS.Attributes.Add("onClick", "ConsisteCkeckBoxDeAlteracao(this)");
            ckbOsteoN.Attributes.Add("onClick", "ConsisteCkeckBoxDeAlteracao(this)");
            ckbOsteoS.Attributes.Add("onClick", "ConsisteCkeckBoxDeAlteracao(this)");
            ckbPulmoesN.Attributes.Add("onClick", "ConsisteCkeckBoxDeAlteracao(this)");
            ckbPulmoesS.Attributes.Add("onClick", "ConsisteCkeckBoxDeAlteracao(this)");
            ckbPeleN.Attributes.Add("onClick", "ConsisteCkeckBoxDeAlteracao(this)");
            ckbPeleS.Attributes.Add("onClick", "ConsisteCkeckBoxDeAlteracao(this)");
        }

        //#endregion

        //#region PopulaExame

        private void PopulaAnamnese()
        {
            if (ckbQueixaN.Checked)
                exame.Anamnese.HasQueixasAtuais = (int)StatusAnamnese.Nao;
            else if (ckbQueixaS.Checked)
                exame.Anamnese.HasQueixasAtuais = (int)StatusAnamnese.Sim;
            if (ckbAfastadoN.Checked)
                exame.Anamnese.HasAfastamento = (int)StatusAnamnese.Nao;
            else if (ckbAfastadoS.Checked)
                exame.Anamnese.HasAfastamento = (int)StatusAnamnese.Sim;
            if (ckbAnteFamiN.Checked)
                exame.Anamnese.HasAntecedentes = (int)StatusAnamnese.Nao;
            else if (ckbAnteFamiS.Checked)
                exame.Anamnese.HasAntecedentes = (int)StatusAnamnese.Sim;
            if (ckbDoencaN.Checked)
                exame.Anamnese.HasDoencaCronica = (int)StatusAnamnese.Nao;
            else if (ckbDoencaS.Checked)
                exame.Anamnese.HasDoencaCronica = (int)StatusAnamnese.Sim;
            if (ckbCirurgiaN.Checked)
                exame.Anamnese.HasCirurgia = (int)StatusAnamnese.Nao;
            else if (ckbCirurgiaS.Checked)
                exame.Anamnese.HasCirurgia = (int)StatusAnamnese.Sim;
            if (ckbTraumaN.Checked)
                exame.Anamnese.HasTraumatismos = (int)StatusAnamnese.Nao;
            else if (ckbTraumaS.Checked)
                exame.Anamnese.HasTraumatismos = (int)StatusAnamnese.Sim;
            if (ckbDeficienciaN.Checked)
                exame.Anamnese.HasDeficienciaFisica = (int)StatusAnamnese.Nao;
            else if (ckbDeficienciaS.Checked)
                exame.Anamnese.HasDeficienciaFisica = (int)StatusAnamnese.Sim;
            if (ckbMedicamentosN.Checked)
                exame.Anamnese.HasMedicacoes = (int)StatusAnamnese.Nao;
            else if (ckbMedicamentosS.Checked)
                exame.Anamnese.HasMedicacoes = (int)StatusAnamnese.Sim;
            if (ckbTabagismoN.Checked)
                exame.Anamnese.HasTabagismo = (int)StatusAnamnese.Nao;
            else if (ckbTabagismoS.Checked)
                exame.Anamnese.HasTabagismo = (int)StatusAnamnese.Sim;
            if (ckbEtilismoN.Checked)
                exame.Anamnese.HasAlcoolismo = (int)StatusAnamnese.Nao;
            else if (ckbEtilismoS.Checked)
                exame.Anamnese.HasAlcoolismo = (int)StatusAnamnese.Sim;
        }

        private void PopulaExameFisico()
        {
            if ( wdtDUM.Text.Trim() != "" )      exame.Fisico.DataUltimaMenstruacao = System.Convert.ToDateTime( wdtDUM.Text);

            if ( wneAltura.Text.Trim() != "" )   exame.Fisico.Altura =  System.Convert.ToSingle( wneAltura.Text );

            exame.Fisico.PressaoArterial = txtPA.Text.Trim();

            if ( wnePulso.Text.Trim() != "" )     exame.Fisico.Pulso = System.Convert.ToInt32( wnePulso.Text );

            if ( wnePeso.Text.Trim() != "" )      exame.Fisico.Peso = System.Convert.ToSingle( wnePeso.Text );

            if (ckbPeleN.Checked)
                exame.Fisico.hasPeleAnexosAlterado = (int)StatusAnamnese.Nao;
            else if (ckbPeleS.Checked)
                exame.Fisico.hasPeleAnexosAlterado = (int)StatusAnamnese.Sim;
            if (ckbOsteoN.Checked)
                exame.Fisico.hasOsteoAlterado = (int)StatusAnamnese.Nao;
            else if (ckbOsteoS.Checked)
                exame.Fisico.hasOsteoAlterado = (int)StatusAnamnese.Sim;
            if (ckbCabecaN.Checked)
                exame.Fisico.hasCabecaAlterado = (int)StatusAnamnese.Nao;
            else if (ckbCabecaS.Checked)
                exame.Fisico.hasCabecaAlterado = (int)StatusAnamnese.Sim;
            if (ckbCoracaoN.Checked)
                exame.Fisico.hasCoracaoAlterado = (int)StatusAnamnese.Nao;
            else if (ckbCoracaoS.Checked)
                exame.Fisico.hasCoracaoAlterado = (int)StatusAnamnese.Sim;
            if (ckbPulmoesN.Checked)
                exame.Fisico.hasPulmaoAlterado = (int)StatusAnamnese.Nao;
            else if (ckbPulmoesS.Checked)
                exame.Fisico.hasPulmaoAlterado = (int)StatusAnamnese.Sim;
            if (ckbAbdomemN.Checked)
                exame.Fisico.hasAbdomemAlterado = (int)StatusAnamnese.Nao;
            else if (ckbAbdomemS.Checked)
                exame.Fisico.hasAbdomemAlterado = (int)StatusAnamnese.Sim;
            if (ckbMSN.Checked)
                exame.Fisico.hasMSAlterado = (int)StatusAnamnese.Nao;
            else if (ckbMSS.Checked)
                exame.Fisico.hasMSAlterado = (int)StatusAnamnese.Sim;
            if (ckbMIN.Checked)
                exame.Fisico.hasMIAlterado = (int)StatusAnamnese.Nao;
            else if (ckbMIS.Checked)
                exame.Fisico.hasMIAlterado = (int)StatusAnamnese.Sim;
        }

        private void PopulaExame()
        {
            //if (!cliente.ContrataPCMSO.Equals((int)TipoPcmsoContratada.NaoContratada))
            //{
            //if (!usuario.NomeUsuario.Equals("Admin"))
            //{

            Entities.Usuario usuario = (Entities.Usuario)Session["usuarioLogado"];
            prestador.Find(" IdPessoa = " + usuario.IdPessoa.ToString());
            //exame.IdMedico.Id = prestador.Id;
            exame.IdJuridica.Id = prestador.IdJuridica.Id;
            //}
            //else
            //    exame.IdJuridica = cliente;
            PopulaAnamnese();
            PopulaExameFisico();
            exame.Prontuario = txtAnotacoes.Text;
            exame.Observacao = txtObs.Text;


            if (exame.IdEmpregadoFuncao.Id.Equals(0) || exame.IdPcmso.Id.Equals(0))
            {
                exame.IdEmpregadoFuncao = EmpregadoFuncao.GetEmpregadoFuncao(empregado);
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

            exame.IdJuridica.Id = Convert.ToInt32(ddlClinica.SelectedItem.Value);


            if (ddlTipoExame.SelectedIndex >= 0) PopulaDDLClinicas(ddlTipoExame.SelectedValue.ToString());

            if (exame.IdJuridica != null) ddlClinica.Items.FindByValue(exame.IdJuridica.Id.ToString().Trim()).Selected = true;

            //if (wdtDataExame.Date.Hour.Equals(0) && wdtDataExame.Date.Minute.Equals(0) && wdtDataExame.Date.Second.Equals(0))
            //    exame.DataExame = wdtDataExame.Date.AddHours(DateTime.Now.Hour).AddMinutes(DateTime.Now.Minute).AddSeconds(DateTime.Now.Second);
            //else
                exame.DataExame = System.Convert.ToDateTime( wdtDataExame.Text );

            try
            {
                exame.IndResultado = Convert.ToInt16(rblResultado.SelectedItem.Value);
            }
            catch
            {
                throw new Exception("O resultado do exame deve ser colocado!");
            }
        }

        //#endregion

        //#region PopulaTelaExame

        private void PopulaTelaExame()
        {
            wdtDataExame.Text = exame.DataExame.ToString("dd/MM/yyyy");

            ddlTipoExame.Items.FindByValue(exame.IdExameDicionario.Id.ToString().Trim()).Selected = true;

            if (ddlTipoExame.SelectedIndex >= 0)
                PopulaDDLClinicas(ddlTipoExame.SelectedValue.ToString());

            if (exame.IdJuridica != null)
            {
                try
                {
                    ddlClinica.Items.FindByValue(exame.IdJuridica.Id.ToString().Trim()).Selected = true;
                }
                catch { }                

            }


            rblResultado.ClearSelection();
            if (exame.IndResultado != 0)
                rblResultado.Items.FindByValue(exame.IndResultado.ToString()).Selected = true;


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



            //if (!cliente.ContrataPCMSO.Equals((int)TipoPcmsoContratada.NaoContratada))
            //{
            if (exame.Anamnese.HasQueixasAtuais == (int)StatusAnamnese.Nao)
                ckbQueixaN.Checked = true;
            else if (exame.Anamnese.HasQueixasAtuais == (int)StatusAnamnese.Sim)
                ckbQueixaS.Checked = true;
            if (exame.Anamnese.HasAfastamento == (int)StatusAnamnese.Nao)
                ckbAfastadoN.Checked = true;
            else if (exame.Anamnese.HasAfastamento == (int)StatusAnamnese.Sim)
                ckbAfastadoS.Checked = true;
            if (exame.Anamnese.HasAntecedentes == (int)StatusAnamnese.Nao)
                ckbAnteFamiN.Checked = true;
            else if (exame.Anamnese.HasAntecedentes == (int)StatusAnamnese.Sim)
                ckbAnteFamiS.Checked = true;
            if (exame.Anamnese.HasDoencaCronica == (int)StatusAnamnese.Nao)
                ckbDoencaN.Checked = true;
            else if (exame.Anamnese.HasDoencaCronica == (int)StatusAnamnese.Sim)
                ckbDoencaS.Checked = true;
            if (exame.Anamnese.HasCirurgia == (int)StatusAnamnese.Nao)
                ckbCirurgiaN.Checked = true;
            else if (exame.Anamnese.HasCirurgia == (int)StatusAnamnese.Sim)
                ckbCirurgiaS.Checked = true;
            if (exame.Anamnese.HasTraumatismos == (int)StatusAnamnese.Nao)
                ckbTraumaN.Checked = true;
            else if (exame.Anamnese.HasTraumatismos == (int)StatusAnamnese.Sim)
                ckbTraumaS.Checked = true;
            if (exame.Anamnese.HasDeficienciaFisica == (int)StatusAnamnese.Nao)
                ckbDeficienciaN.Checked = true;
            else if (exame.Anamnese.HasDeficienciaFisica == (int)StatusAnamnese.Sim)
                ckbDeficienciaS.Checked = true;
            if (exame.Anamnese.HasMedicacoes == (int)StatusAnamnese.Nao)
                ckbMedicamentosN.Checked = true;
            else if (exame.Anamnese.HasMedicacoes == (int)StatusAnamnese.Sim)
                ckbMedicamentosS.Checked = true;
            if (exame.Anamnese.HasTabagismo == (int)StatusAnamnese.Nao)
                ckbTabagismoN.Checked = true;
            else if (exame.Anamnese.HasTabagismo == (int)StatusAnamnese.Sim)
                ckbTabagismoS.Checked = true;
            if (exame.Anamnese.HasAlcoolismo == (int)StatusAnamnese.Nao)
                ckbEtilismoN.Checked = true;
            else if (exame.Anamnese.HasAlcoolismo == (int)StatusAnamnese.Sim)
                ckbEtilismoS.Checked = true;

            wdtDUM.Text = exame.Fisico.DataUltimaMenstruacao.ToString("dd/MM/yyyy");
            wneAltura.Text = exame.Fisico.Altura.ToString();
            txtPA.Text = exame.Fisico.PressaoArterial;
            wnePulso.Text  = exame.Fisico.Pulso.ToString();
            wnePeso.Text = exame.Fisico.Peso.ToString();

            if (wneAltura.Text.Trim() != "0" && wnePeso.Text.Trim() != "0")
                lblIMC.Text = ((double)( System.Convert.ToDouble( wnePeso.Text ) / Math.Pow( System.Convert.ToDouble( wneAltura.Text), 2.0))).ToString("0.00");

            if (exame.Fisico.hasPeleAnexosAlterado == (int)StatusAnamnese.Nao)
                ckbPeleN.Checked = true;
            else if (exame.Fisico.hasPeleAnexosAlterado == (int)StatusAnamnese.Sim)
                ckbPeleS.Checked = true;
            if (exame.Fisico.hasOsteoAlterado == (int)StatusAnamnese.Nao)
                ckbOsteoN.Checked = true;
            else if (exame.Fisico.hasOsteoAlterado == (int)StatusAnamnese.Sim)
                ckbOsteoS.Checked = true;
            if (exame.Fisico.hasCabecaAlterado == (int)StatusAnamnese.Nao)
                ckbCabecaN.Checked = true;
            else if (exame.Fisico.hasCabecaAlterado == (int)StatusAnamnese.Sim)
                ckbCabecaS.Checked = true;
            if (exame.Fisico.hasCoracaoAlterado == (int)StatusAnamnese.Nao)
                ckbCoracaoN.Checked = true;
            else if (exame.Fisico.hasCoracaoAlterado == (int)StatusAnamnese.Sim)
                ckbCoracaoS.Checked = true;
            if (exame.Fisico.hasPulmaoAlterado == (int)StatusAnamnese.Nao)
                ckbPulmoesN.Checked = true;
            else if (exame.Fisico.hasPulmaoAlterado == (int)StatusAnamnese.Sim)
                ckbPulmoesS.Checked = true;
            if (exame.Fisico.hasAbdomemAlterado == (int)StatusAnamnese.Nao)
                ckbAbdomemN.Checked = true;
            else if (exame.Fisico.hasAbdomemAlterado == (int)StatusAnamnese.Sim)
                ckbAbdomemS.Checked = true;
            if (exame.Fisico.hasMSAlterado == (int)StatusAnamnese.Nao)
                ckbMSN.Checked = true;
            else if (exame.Fisico.hasMSAlterado == (int)StatusAnamnese.Sim)
                ckbMSS.Checked = true;
            if (exame.Fisico.hasMIAlterado == (int)StatusAnamnese.Nao)
                ckbMIN.Checked = true;
            else if (exame.Fisico.hasMIAlterado == (int)StatusAnamnese.Sim)
                ckbMIS.Checked = true;

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
                ddlClinica.Enabled = false;
                //uwtExameClinico.Enabled = true;
                lkbASO.Enabled = true;
                lkbPCI.Enabled = true;
                lkb_ASOGuia.Enabled = true;
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
                ddlClinica.Enabled = true;
                //if (!cliente.ContrataPCMSO.Equals((int)TipoPcmsoContratada.NaoContratada))
                //{
                //uwtExameClinico.Enabled = true;
                lkbASO.Enabled = true;
                lkbPCI.Enabled = true;
                lkb_ASOGuia.Enabled = true;

                //}
                //else
                //{
                //   uwtExameClinico.Enabled = false;
                //   lkbASO.Enabled = false;
                //   lkbPCI.Enabled = false;
                //}
            }

            empregado.Find( System.Convert.ToInt32( Request["IdEmpregado"].ToString() ));


        }

        private DataSet GetExamesComplementares()
        {
            return new Complementar().ListaExamesComplementar("IdEmpregado=" + Request["IdEmpregado"].ToString() + " ORDER BY DataExame DESC");
        }

        //#endregion

        //#region SaveAndDelete

        protected void btnOK_Click(object sender, System.EventArgs e)
        {
            try
            {

                string xNomeArq = File1.FileName.Trim();

                Entities.Usuario usuario = (Entities.Usuario)Session["usuarioLogado"];

                PopulaExame();
                ViewState["IdExame"] = exame.Save(usuario.IdUsuario);
                

                //salvar prontuario
                if (xNomeArq != string.Empty)
                {

                    string xExtension = xNomeArq.Substring(xNomeArq.Length - 3, 3).ToUpper().Trim();

                    if (xExtension == "PDF" || xExtension == "JPG")
                    {

                        Cliente xCliente = new Cliente();
                        xCliente.Find(System.Convert.ToInt32(Request["IdEmpresa"].ToString()));

                        string xArq = "";

                        // if (Session["Servidor_Web"].ToString().Trim().ToUpper() == "ILITERA")
                        xArq = "I:\\fotosdocsdigitais\\" + xCliente.DiretorioPadrao.ToString().ToUpper().Trim() + "\\Prontuario\\" + xNomeArq;
                        // else
                        //     xArq = "C:\\DRIVE_I\\fotosdocsdigitais\\" + xCliente.DiretorioPadrao.ToString().ToUpper().Trim() + "\\Prontuario\\" + xNomeArq;

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
                        xProntuario.DataProntuario = System.Convert.ToDateTime(wdtDataExame.Text);
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
                Response.Redirect("DadosEmpregado.aspx?IdEmpregado=" + Session["Empregado"].ToString() + "&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString() + "&Tipo=1");


            }
            catch (Exception ex)
            {
                txtAuxAviso.Value = ex.Message;
            }
        }

        protected void btnExcluir_Click(object sender, System.EventArgs e)
        {

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

                Response.Write("<script>window.opener.document.forms[0].submit(); window.close();</script>");
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

            if (ckbQueixaS.Checked)
                st.Append("Queixas atuais: \n");
            if (ckbAfastadoS.Checked)
                st.Append("Afastamento do trabalho: \n");
            if (ckbAnteFamiS.Checked)
                st.Append("Antecedentes familiares: \n");
            if (ckbDoencaS.Checked)
                st.Append("Doença crônica / Atopia: \n");
            if (ckbCirurgiaS.Checked)
                st.Append("Cirurgia: \n");
            if (ckbTraumaS.Checked)
                st.Append("Trauma: \n");
            if (ckbDeficienciaS.Checked)
                st.Append("Deficiência fisica: \n");
            if (ckbMedicamentosS.Checked)
                st.Append("Uso de medicamentos: \n");
            if (ckbTabagismoS.Checked)
                st.Append("Tabagista: \n");
            if (ckbEtilismoS.Checked)
                st.Append("Etilista: \n");
            if (ckbOsteoS.Checked)
                st.Append("Osteo muscular: \n");
            if (ckbPeleS.Checked)
                st.Append("Pele e anexos: \n");
            if (ckbCabecaS.Checked)
                st.Append("Cabeça e pescoço: \n");
            if (ckbCoracaoS.Checked)
                st.Append("Coração: \n");
            if (ckbPulmoesS.Checked)
                st.Append("Pulmões: \n");
            if (ckbAbdomemS.Checked)
                st.Append("Abdomem: \n");
            if (ckbMSS.Checked)
                st.Append("Membros superiores: \n");
            if (ckbMIS.Checked)
                st.Append("Membros inferiores: \n");

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
                if (exame.IndResultado.Equals((int)ResultadoExame.NaoRealizado))
                    throw new Exception("É necessário colocar o resultado do exame e salvá-lo antes de adicionar todos os Fatores de Risco!");

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
                txtAuxAviso.Value = ex.Message;
            }
        }

        protected void imbAdd_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            try
            {
                if (exame.IndResultado.Equals((int)ResultadoExame.NaoRealizado))
                    throw new Exception("É necessário colocar o resultado do exame e salvá-lo antes de adicionar um ou mais Fatores de Risco!");

                if (lsbFatorRisco.SelectedItem == null)
                    throw new Exception("É necessário selecionar pelo menos 1 Fator de Risco para ser adicionado!");
                
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
                txtAuxAviso.Value = ex.Message;
            }
        }

        protected void imbRemove_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            try
            {
                if (lsbFatorRiscoSel.SelectedItem == null)
                    throw new Exception("É necessário selecionar pelo menos 1 Fator de Risco para ser removido!");
                
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
                txtAuxAviso.Value = ex.Message;
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
                System.Diagnostics.Debug.WriteLine(ex.Message);
                //txtAuxAviso.Value = ex.Message;
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

        protected void ddlClinica_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlTipoExame_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulaDDLClinicas(ddlTipoExame.SelectedValue);
        }


}
}