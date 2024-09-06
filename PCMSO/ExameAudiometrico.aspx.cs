using System;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Data;
using System.Drawing;
using System.Web;

using Ilitera.Opsa.Data;
using Ilitera.Common;
//using MestraNET;

namespace Ilitera.Net
{
    public partial class ExameAudiometrico : System.Web.UI.Page
    {
        
        protected System.Web.UI.WebControls.Label Label39;
		protected System.Web.UI.WebControls.Label Label1;
		private Audiometria exameAudiometrico;
		private AnamneseAudiologica anamneseAudiologica;
		private string tipo;
        
        protected Prestador prestador = new Prestador();
        protected Ilitera.Opsa.Data.Empregado empregado = new Ilitera.Opsa.Data.Empregado();
        protected Ilitera.Common.Usuario usuario = new Ilitera.Common.Usuario();


		protected void Page_Load(object sender, System.EventArgs e)
		{
            string xUsuario = Session["usuarioLogado"].ToString();
            InicializaWebPageObjects();
			
			if (!IsPostBack)
			{ 				
				//InicializeChart(ChtDiagramaOD, Orelha.Direita);
				//InicializeChart(ChtDiagramaOE, Orelha.Esquerda);

				RegisterClientCode();

                Guid strAux = Guid.NewGuid();

                lkbAudiograma.Attributes.Add("onClick", "javascript:" + strOpenReport("PCMSO", "Audiograma.aspx?IliteraSystem=" + strAux.ToString() + strAux.ToString()
                 + "&IdEmpresa=" + Request["IdEmpresa"] + "&IdEmpregado=" + Request["IdEmpregado"] + "&IdUsuario=" + Request["IdUsuario"] + "&IdExame=" + exameAudiometrico.Id , "Audiograma"));

                
                lkbAnamnese.Attributes.Add("onClick", "javascript:" + strOpenReport("PCMSO", "AnamneseAud.aspx?IliteraSystem=" + strAux.ToString() + strAux.ToString()
                 + "&IdEmpresa=" + Request["IdEmpresa"] + "&IdEmpregado=" + Request["IdEmpregado"] + "&IdUsuario=" + Request["IdUsuario"] + "&IdExame=" + exameAudiometrico.Id , "Anamnese"));



                string zAcao = Request["Acao"].ToString().Trim();

                if (zAcao == "E")
                {
                    btbOk.Visible = true;
                    btnExcluir.Visible = true;
                }
                else
                {
                    btbOk.Visible = false;
                    btnExcluir.Visible = false;
                }


				if (!usuario.NomeUsuario.Equals("Admin"))
				{
                    Entities.Usuario zusuario = (Entities.Usuario)Session["usuarioLogado"];
                    prestador.Find(" IdPessoa = " + zusuario.IdPessoa.ToString());

                    prestador.IdJuridica.Find();


					if (prestador.IdJuridica.IdJuridicaPapel.Id == (int)IndJuridicaPapel.Clinica)
					{
						Clinica clinica = new Clinica(prestador.IdJuridica.Id);

                        //if (!clinica.IsClinicaInterna)
                        //{
                        //    lblClinica.ForeColor = foreColorDisabledLabel;
                        //    ddlClinica.Items.Add(new ListItem(clinica.NomeAbreviado, clinica.Id.ToString()));
                        //    ddlClinica.BackColor = backColorDisabledBox;
                        //    ddlClinica.Enabled = false;
                        //    btnAddClinica.Disabled = true;
                        //    lblMedico.ForeColor = foreColorDisabledLabel;
                        //    ddlMedico.Items.Add(new ListItem(prestador.NomeCompleto, prestador.Id.ToString()));
                        //    ddlMedico.BackColor = backColorDisabledBox;
                        //    ddlMedico.Enabled = false;
                        //    btnAddMedico.Disabled = true;
                        //}
					}
				}

				if (ddlClinica.Enabled)
				{
					PopulaDDLClinica();
					ddlAudiometro.Items.Insert(0, new ListItem("Selecione...", "0"));
					ddlMedico.Items.Insert(0, new ListItem("Selecione...", "0"));
				}
				else
					PopulaDDLAudiometro();

				WbUsrCntrlAudiogramaDireito.PopulaDDLDiagnostico();
				WbUsrCntrlAudiogramaEsquerda.PopulaDDLDiagnostico();

                if (exameAudiometrico.Id != 0)
                {
                    PopulaTela();
                    lkbAnamnese.Visible = true;
                    lkbAudiograma.Visible = true;
                }
                else
                {
                    lkbAnamnese.Visible = false;
                    lkbAudiograma.Visible = false;
                }


                if (exameAudiometrico.Id == 0)
                {
                    TabStrip1.Items[5].Visible = false;
                }
                else
                {
                    //Guid strAux = Guid.NewGuid();

                    lkbAnamnese2.Attributes.Add("onClick", "javascript:" + strOpenReport("PCMSO", "AnamneseDinamica.aspx?IliteraSystem=" + strAux.ToString() + strAux.ToString()
                        + "&IdExame=" + exameAudiometrico.Id, "Anamnese"));

                    exameAudiometrico.IdEmpregado.Find();
                    exameAudiometrico.IdEmpregado.nID_EMPR.Find();
                    exameAudiometrico.IdEmpregado.nID_EMPR.IdGrupoEmpresa.Find();

                    if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("PRAJNA") > 0 ) //exameAudiometrico.IdEmpregado.nID_EMPR.IdGrupoEmpresa.Descricao.ToUpper().Trim() == "CAPGEMINI" )  //&& Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("_HOM") > 0)  //.Id == -905238295)  // Capgemini
                    {
                        TabStrip1.Items[5].Visible = true;
                        Carregar_Dados_Anamnese_Exame();
                        Carregar_Grid_Anamnese_Capgemini();
                    }
                    else
                    {
                        TabStrip1.Items[5].Visible = false;
                    }
                }





            }
            else if (txtAuxiliar.Value.Equals("atualizaClinica"))
			{
				txtAuxiliar.Value = string.Empty;
				PopulaDDLClinica();
			}
			else if (txtAuxiliar.Value.Equals("atualizaMedico"))
			{
				txtAuxiliar.Value = string.Empty;
				PopulaDDLMedico();
			}
			else if (txtAuxiliar.Value.Equals("atualizaAudiometro"))
			{
				txtAuxiliar.Value = string.Empty;
				PopulaDDLAudiometro();
			}
		}

		private void RegisterClientCode()
		{
            btnAddAudiometro.Attributes.Add("onClick", "javascript:AbreAudiometro(" + Request["IdEmpresa"].ToString() + ", " + Request["IdUsuario"].ToString() + ");");
            btnAddClinica.Attributes.Add("onClick", "javascript:AbreClinicas(" + Request["IdEmpresa"].ToString() + ", " + Request["IdUsuario"].ToString() + ");");
            btnAddMedico.Attributes.Add("onClick", "javascript:AbreMedicos(" +  Request["IdUsuario"].ToString() + ");");
			btnExcluir.Attributes.Add("onClick" ,"javascript:return confirm('Você deseja realmente excluir este Exame Audiométrico?');");

			ckbDificuldadeS.Attributes.Add("onClick", "ConsisteCkeckBoxDeAlteracao(this)");
			ckbDificuldadeN.Attributes.Add("onClick", "ConsisteCkeckBoxDeAlteracao(this)");
			ckbVertigensN.Attributes.Add("onClick", "ConsisteCkeckBoxDeAlteracao(this)");
			ckbVertigensS.Attributes.Add("onClick", "ConsisteCkeckBoxDeAlteracao(this)");
			ckbAcufenosN.Attributes.Add("onClick", "ConsisteCkeckBoxDeAlteracao(this)");
			ckbAcufenosS.Attributes.Add("onClick", "ConsisteCkeckBoxDeAlteracao(this)");
			ckbFamiliaresN.Attributes.Add("onClick", "ConsisteCkeckBoxDeAlteracao(this)");
			ckbFamiliaresS.Attributes.Add("onClick", "ConsisteCkeckBoxDeAlteracao(this)");
			ckbMedicacaoN.Attributes.Add("onClick", "ConsisteCkeckBoxDeAlteracao(this)");
			ckbMedicacaoS.Attributes.Add("onClick", "ConsisteCkeckBoxDeAlteracao(this)");
			ckbRuidoExtraN.Attributes.Add("onClick", "ConsisteCkeckBoxDeAlteracao(this)");
			ckbRuidoExtraS.Attributes.Add("onClick", "ConsisteCkeckBoxDeAlteracao(this)");
			ckbototoxicosN.Attributes.Add("onClick", "ConsisteCkeckBoxDeAlteracao(this)");
			ckbototoxicosS.Attributes.Add("onClick", "ConsisteCkeckBoxDeAlteracao(this)");
			ckbMeatosN.Attributes.Add("onClick", "ConsisteCkeckBoxDeAlteracao(this)");
			ckbMeatosS.Attributes.Add("onClick", "ConsisteCkeckBoxDeAlteracao(this)");
		}

		protected void InicializaWebPageObjects()
		{
			//base.InicializaWebPageObjects();
			
			//WbUsrCntrlAudiogramaDireito.IdEmpregado = empregado.Id;
			//WbUsrCntrlAudiogramaDireito.IndOrelha = (int)Orelha.Direita;

			//WbUsrCntrlAudiogramaEsquerda.IdEmpregado = empregado.Id;
			//WbUsrCntrlAudiogramaEsquerda.IndOrelha = (int)Orelha.Esquerda;

			anamneseAudiologica = new AnamneseAudiologica();

			if (Request["IdExame"] != null && Request["IdExame"] != "")
			{
				exameAudiometrico = new Audiometria(Convert.ToInt32(Request["IdExame"]));								
				anamneseAudiologica.Find("IdAudiometria=" + exameAudiometrico.Id);
				tipo = "atualizado";
			}
			else if (ViewState["IdExameAudiometrico"] != null)
			{
				exameAudiometrico = new Audiometria(Convert.ToInt32(ViewState["IdExameAudiometrico"]));
				anamneseAudiologica.Find("IdAudiometria=" + exameAudiometrico.Id);
				tipo = "atualizado";
			}
			else
			{
				exameAudiometrico = new Audiometria();
				exameAudiometrico.Inicialize();
                exameAudiometrico.IdEmpregado.Find( System.Convert.ToInt32(Request["IdEmpregado"].ToString())); 

				anamneseAudiologica.Inicialize();
			
				btnExcluir.Enabled = false;
				tipo = "cadastrado";
			}
		}
        
		private void PopulaDDLClinica()
		{
            ddlClinica.DataSource = new Clinica().Get("((IdClinica IN (SELECT IdClinica FROM ClinicaCliente WHERE IdCliente=" + Request["IdEmpresa"].ToString() + ")"
				+" AND IdClinica IN (SELECT IdClinica FROM ClinicaExameDicionario WHERE IdExameDicionario=6)" //Exame Audiométrico
				+" AND IdJuridicaPapel=" + (int)IndJuridicaPapel.Clinica + ") )"
                +"  ORDER BY NomeAbreviado");
                //+ " OR (IdClinica IN (SELECT IdClinica FROM ClinicaCliente WHERE IdCliente=" + Request["IdEmpresa"].ToString() + ")"
                //+" AND IdJuridicaPapel=" + (int)IndJuridicaPapel.ClinicaOutras + "))"
                //+" AND FazAudiometrico=1 ORDER BY NomeAbreviado");
			ddlClinica.DataValueField = "IdClinica";
			ddlClinica.DataTextField = "NomeAbreviado";
			ddlClinica.DataBind();

			ddlClinica.Items.Insert(0, new ListItem("Selecione...", "0"));
            ddlClinica.Items.Insert(1, new ListItem("Ilitera", "310"));
		}

		private void PopulaDDLAudiometro()
		{
            int IdClinica = 0;

            Int32.TryParse(Convert.ToString(ddlClinica.SelectedValue), out IdClinica);

            if (IdClinica == 0)
                return;

            ddlAudiometro.DataSource = new Audiometro().Find("IdClinica=" + IdClinica + " ORDER BY Nome");
			ddlAudiometro.DataValueField = "Id";
			ddlAudiometro.DataTextField = "Nome";
			ddlAudiometro.DataBind();

			ddlAudiometro.Items.Insert(0, new ListItem("-","0"));
		}

		private void PopulaDDLMedico()
		{
            int IdClinica = 0;

            Int32.TryParse(Convert.ToString(ddlClinica.SelectedValue), out IdClinica);

            if (IdClinica == 0)
                return;

            Clinica clinica = new Clinica(IdClinica);
            
            ArrayList medicos = new Medico().GetListaPrestador(clinica, false, (int)TipoPrestador.Medico, false);

            ddlMedico.DataSource = medicos;
            ddlMedico.DataValueField = "Id";
            ddlMedico.DataTextField = "NomeCompleto";
            ddlMedico.DataBind();

			ddlMedico.Items.Insert(0, new ListItem("Selecione...", "0"));
		}

		private void PopulaTela()
		{
            System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");          
			wdeDataExame.Text = exameAudiometrico.DataExame.ToString("dd/MM/yyyy",ptBr);

			if (ddlClinica.Enabled)
			{
				try
				{
					ddlClinica.Items.FindByValue(exameAudiometrico.IdJuridica.Id.ToString()).Selected = true;
				}
				catch
				{
					exameAudiometrico.IdJuridica.Find();
					ddlClinica.Items.Insert(1, new ListItem(exameAudiometrico.IdJuridica.NomeAbreviado, exameAudiometrico.IdJuridica.Id.ToString()));
					ddlClinica.Items[1].Selected = true;
				}
                   
				PopulaDDLMedico();
				PopulaDDLAudiometro();

				try
				{
					ddlMedico.Items.FindByValue(exameAudiometrico.IdMedico.Id.ToString()).Selected = true;
				}
				catch
				{
					exameAudiometrico.IdMedico.Find();
					ddlMedico.Items.Insert(1, new ListItem(exameAudiometrico.IdMedico.NomeCompleto, exameAudiometrico.IdMedico.Id.ToString()));
					ddlMedico.Items[1].Selected = true;
				}
			}

			ddlTipoExame.Items.FindByValue(exameAudiometrico.IndAudiometriaTipo.ToString()).Selected = true;
			
			try
			{
				ddlAudiometro.Items.FindByValue(exameAudiometrico.IdAudiometro.Id.ToString()).Selected = true;
			}
			catch
			{
				exameAudiometrico.IdAudiometro.Find();
				ddlAudiometro.Items.Insert(1, new ListItem(exameAudiometrico.IdAudiometro.Nome, exameAudiometrico.IdAudiometro.Id.ToString()));
				ddlAudiometro.Items[1].Selected = true;
			}


            ProntuarioDigital xProntuario = new ProntuarioDigital();
            xProntuario.Find("IdExameBase = " +  exameAudiometrico.Id.ToString() + " ");

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




			wneRepouso.Text =  exameAudiometrico.TempoRepouso.ToString();

			if (anamneseAudiologica.HasSenteDificuldadeAuditiva == (int)StatusAnamnese.Sim)
				ckbDificuldadeS.Checked = true;
			else if (anamneseAudiologica.HasSenteDificuldadeAuditiva == (int)StatusAnamnese.Nao)
				ckbDificuldadeN.Checked = true;
			if (anamneseAudiologica.HasTemPresencaVertigens == (int)StatusAnamnese.Sim)
				ckbVertigensS.Checked = true;
			else if (anamneseAudiologica.HasTemPresencaVertigens == (int)StatusAnamnese.Nao)
				ckbVertigensN.Checked = true;
			if (anamneseAudiologica.HasTemPresencaAcufenos == (int)StatusAnamnese.Sim)
				ckbAcufenosS.Checked = true;
			else if (anamneseAudiologica.HasTemPresencaAcufenos == (int)StatusAnamnese.Nao)
				ckbAcufenosN.Checked = true;
			if (anamneseAudiologica.HasFamiliaresComProblemaAuditivo == (int)StatusAnamnese.Sim)
				ckbFamiliaresS.Checked = true;
			else if (anamneseAudiologica.HasFamiliaresComProblemaAuditivo == (int)StatusAnamnese.Nao)
				ckbFamiliaresN.Checked = true;

			ckbIsCaxumba.Checked = anamneseAudiologica.IsAntecedenteCaxumba;
			ckbIsSarampo.Checked = anamneseAudiologica.IsAntecedenteSarampo;
			ckbIsMeningite.Checked = anamneseAudiologica.IsAntecedenteMenigite;

			if (anamneseAudiologica.HasFazUsoMedicacao == (int)StatusAnamnese.Sim)
				ckbMedicacaoS.Checked = true;
			else if (anamneseAudiologica.HasFazUsoMedicacao == (int)StatusAnamnese.Nao)
				ckbMedicacaoN.Checked = true;

			txtMedicacao.Text = anamneseAudiologica.FazUsoMedicacaoQual;
			txtTempoExposicao.Text = anamneseAudiologica.TempoExposicaoRuidoOcupacional;
			txtTempoUso.Text = anamneseAudiologica.TempoUsoProtetorAuricular;

			if (anamneseAudiologica.HasExposicaoRuidoExtraLaboral == (int)StatusAnamnese.Sim)
				ckbRuidoExtraS.Checked = true;
			else if (anamneseAudiologica.HasExposicaoRuidoExtraLaboral == (int)StatusAnamnese.Nao)
				ckbRuidoExtraN.Checked = true;
			if (anamneseAudiologica.HasExposicaoProdutosOtotoxicos == (int)StatusAnamnese.Sim)
				ckbototoxicosS.Checked = true;
			else if (anamneseAudiologica.HasExposicaoProdutosOtotoxicos == (int)StatusAnamnese.Nao)
				ckbototoxicosN.Checked = true;

			txtototoxicos.Text = anamneseAudiologica.ExposicaoProdutosOtotoxicosQual;
            
			if (anamneseAudiologica.HasAlteracaoMeatosAcusticos == (int)StatusAnamnese.Sim)
				ckbMeatosS.Checked = true;
			else if (anamneseAudiologica.HasAlteracaoMeatosAcusticos == (int)StatusAnamnese.Nao)
				ckbMeatosN.Checked = true;

			txtMeatos.Text = anamneseAudiologica.AlteracaoMeatosAcusticosQual;
			txtObsAnamnese.Text = anamneseAudiologica.Observacao;

			WbUsrCntrlAudiogramaDireito.PopulaAudiograma(exameAudiometrico,0);
			WbUsrCntrlAudiogramaEsquerda.PopulaAudiograma(exameAudiometrico,1);

			txtObsExame.Text = exameAudiometrico.ObservacaoResultado;

            //PopulaChartAudiograma(ChtDiagramaOD, Orelha.Direita);
            //PopulaChartAudiograma(ChtDiagramaOE, Orelha.Esquerda);

            rblResultado.ClearSelection();
            if (exameAudiometrico.IndResultado != 0)
                rblResultado.Items.FindByValue(exameAudiometrico.IndResultado.ToString()).Selected = true;
            else
                rblResultado.Items[2].Selected = true;


        }

        private void PopulaAudiometria()
		{
            if (ddlTipoExame.SelectedValue.Equals("-"))
                throw new Exception("É necessário selecionar o Tipo do Exame!");
            if (ddlClinica.SelectedValue.Equals("0"))
                throw new Exception("É necessário selecionar uma Clínica!");
            //if (ddlAudiometro.SelectedValue.Equals("0"))
            //    throw new Exception("É necessário selecionar o Audiômetro!");
            
            //if (wdeDataExame.Date.Hour.Equals(0) && wdeDataExame.Date.Minute.Equals(0) && wdeDataExame.Date.Second.Equals(0))
			//	exameAudiometrico.DataExame = wdeDataExame.Date.AddHours(DateTime.Now.Hour).AddMinutes(DateTime.Now.Minute).AddSeconds(DateTime.Now.Second);
			//else

            System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");           
			exameAudiometrico.DataExame = System.Convert.ToDateTime( wdeDataExame.Text, ptBr );
			
			exameAudiometrico.IndAudiometriaTipo = Convert.ToInt32(ddlTipoExame.SelectedValue);
			exameAudiometrico.IdAudiometro.Id = Convert.ToInt32(ddlAudiometro.SelectedValue);
			exameAudiometrico.IdAudiometro.Find();
			exameAudiometrico.DataUltimaAfericao = exameAudiometrico.IdAudiometro.DataUltimaAfericao;
			exameAudiometrico.TempoRepouso = System.Convert.ToSingle( wneRepouso.Text );

			exameAudiometrico.IdJuridica.Id = Convert.ToInt32(ddlClinica.SelectedValue);
			exameAudiometrico.IdMedico.Id = Convert.ToInt32(ddlMedico.SelectedValue);
			exameAudiometrico.ObservacaoResultado = txtObsExame.Text.Trim();
		}

		private void PopulaAnamnese()
		{
			if (ckbDificuldadeS.Checked)
				anamneseAudiologica.HasSenteDificuldadeAuditiva = (int)StatusAnamnese.Sim;
			else if (ckbDificuldadeN.Checked)
				anamneseAudiologica.HasSenteDificuldadeAuditiva = (int)StatusAnamnese.Nao;
			if (ckbVertigensS.Checked)
				anamneseAudiologica.HasTemPresencaVertigens = (int)StatusAnamnese.Sim;
			else if (ckbVertigensN.Checked)
				anamneseAudiologica.HasTemPresencaVertigens = (int)StatusAnamnese.Nao;
			if (ckbAcufenosS.Checked)
				anamneseAudiologica.HasTemPresencaAcufenos = (int)StatusAnamnese.Sim;
			else if (ckbAcufenosN.Checked)
				anamneseAudiologica.HasTemPresencaAcufenos = (int)StatusAnamnese.Nao;
			if (ckbFamiliaresS.Checked)
				anamneseAudiologica.HasFamiliaresComProblemaAuditivo = (int)StatusAnamnese.Sim;
			else if (ckbFamiliaresN.Checked)
				anamneseAudiologica.HasFamiliaresComProblemaAuditivo = (int)StatusAnamnese.Nao;

			anamneseAudiologica.IsAntecedenteCaxumba = ckbIsCaxumba.Checked;
			anamneseAudiologica.IsAntecedenteSarampo = ckbIsSarampo.Checked;
			anamneseAudiologica.IsAntecedenteMenigite = ckbIsMeningite.Checked;

			if (ckbMedicacaoS.Checked)
				anamneseAudiologica.HasFazUsoMedicacao = (int)StatusAnamnese.Sim;
			else if (ckbMedicacaoN.Checked)
				anamneseAudiologica.HasFazUsoMedicacao = (int)StatusAnamnese.Nao;

			anamneseAudiologica.FazUsoMedicacaoQual = txtMedicacao.Text.Trim();
			anamneseAudiologica.TempoExposicaoRuidoOcupacional = txtTempoExposicao.Text.Trim();
			anamneseAudiologica.TempoUsoProtetorAuricular = txtTempoUso.Text.Trim();

			if (ckbRuidoExtraS.Checked)
				anamneseAudiologica.HasExposicaoRuidoExtraLaboral = (int)StatusAnamnese.Sim;
			else if (ckbRuidoExtraN.Checked)
				anamneseAudiologica.HasExposicaoRuidoExtraLaboral = (int)StatusAnamnese.Nao;
			if (ckbototoxicosS.Checked)
				anamneseAudiologica.HasExposicaoProdutosOtotoxicos = (int)StatusAnamnese.Sim;
			else if (ckbototoxicosN.Checked)
				anamneseAudiologica.HasExposicaoProdutosOtotoxicos = (int)StatusAnamnese.Nao;

			anamneseAudiologica.ExposicaoProdutosOtotoxicosQual = txtototoxicos.Text.Trim();

			if (ckbMeatosS.Checked)
				anamneseAudiologica.HasAlteracaoMeatosAcusticos = (int)StatusAnamnese.Sim;
			else if (ckbMeatosN.Checked)
				anamneseAudiologica.HasAlteracaoMeatosAcusticos = (int)StatusAnamnese.Nao;

			anamneseAudiologica.AlteracaoMeatosAcusticosQual = txtMeatos.Text.Trim();
			anamneseAudiologica.Observacao = txtObsAnamnese.Text.Trim();
		}

		protected void btbOk_Click(object sender, System.EventArgs e)
		{
			bool isSaved = false;


            try
            {
                exameAudiometrico.IndResultado = Convert.ToInt16(rblResultado.SelectedItem.Value);
            }
            catch
            {
                // new Exception("O resultado do exame deve ser colocado!");
                MsgBox1.Show("Ilitera.Net", "O resultado do exame deve ser colocado", null,
                new EO.Web.MsgBoxButton("OK"));

            }

            try
			{
				SaveExameAudiometrico();


                string xNomeArq = File1.FileName.Trim();


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
                        //else
                        //    xArq = "C:\\DRIVE_I\\fotosdocsdigitais\\" + xCliente.DiretorioPadrao.ToString().ToUpper().Trim() + "\\Prontuario\\" + xNomeArq;


                        File1.SaveAs(xArq);

                        xArq = "I:\\fotosdocsdigitais\\" + xCliente.DiretorioPadrao.ToString().ToUpper().Trim() + "\\Prontuario\\" + xNomeArq;

                        ExameBase xExameBase = new ExameBase();
                        xExameBase.Find(exameAudiometrico.Id);

                        Empregado xEmpregado = new Empregado();
                        xEmpregado.Find(System.Convert.ToInt32(Request["IdEmpregado"].ToString()));

                        Entities.Usuario zusuario = (Entities.Usuario)Session["usuarioLogado"];
                        prestador.Find(" IdPessoa = " + zusuario.IdPessoa.ToString());

                        ProntuarioDigital xProntuario = new ProntuarioDigital();
                        xProntuario.IdExameBase = xExameBase;
                        xProntuario.IdEmpregado = xEmpregado;

                        System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");
                        xProntuario.DataProntuario = System.Convert.ToDateTime(wdeDataExame.Text, ptBr);

                        xProntuario.Arquivo = xArq; //searchFile.Value.ToString();

                        xProntuario.DataDigitalizacao = System.DateTime.Now;
                        xProntuario.IdDigitalizador = prestador;
                        xProntuario.Save();

                    }
                }




                if (exameAudiometrico.IndResultado == 1 || exameAudiometrico.IndResultado == 2)
                {
                    Cliente rCliente = new Cliente();
                    rCliente.Find(System.Convert.ToInt32(Request["IdEmpresa"].ToString()));

                    if (rCliente.Ativar_DesconsiderarCompl != null && rCliente.Dias_Desconsiderar != null)
                    {
                        if (rCliente.Ativar_DesconsiderarCompl == true && rCliente.Dias_Desconsiderar > 0)
                        {
                            new Pcmso().GerarExamePlanejamento(exameAudiometrico.IdEmpregado.Id);
                        }
                    }

                }



                isSaved = true;

				//PopulaChartAudiograma(ChtDiagramaOD, Orelha.Direita);
				//PopulaChartAudiograma(ChtDiagramaOE, Orelha.Esquerda);

				//SalvarChartAudiograma(ChtDiagramaOD, ChtDiagramaOE);

				StringBuilder strBuilder = new StringBuilder();

				strBuilder.Append("window.opener.document.getElementById('txtAuxiliar').value='atualiza';");
				strBuilder.Append("window.opener.document.forms[0].submit();");
				strBuilder.Append("window.alert('O Exame Audiométrico foi " + tipo + " com sucesso!');");

                this.ClientScript.RegisterStartupScript(this.GetType(), "CadastroEdicaoAudiometria", strBuilder.ToString(), true);

                Entities.Usuario user = (Entities.Usuario)Session["usuarioLogado"];


                if (Session["Retorno"].ToString() == "1")
                    Response.Redirect("DadosEmpregado.aspx?IdEmpregado=" + Session["Empregado"].ToString() + "&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString() + "&Tipo=4");
                else
                    Response.Redirect("DadosEmpregado_Lista.aspx?IdEmpregado=" + Session["Empregado"].ToString() + "&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString() + "&Tipo=6");


			}
			catch(Exception ex)
			{			
				if (isSaved)
				{
					StringBuilder stError = new StringBuilder();
					stError.Append(ex.Message);

					stError.Replace("\'", string.Empty);
					stError.Replace("\r", string.Empty);
					stError.Replace("\n", string.Empty);
					
					StringBuilder strBuilder = new StringBuilder();

					strBuilder.Append("window.opener.document.getElementById('txtAuxiliar').value='atualiza';");
					strBuilder.Append("window.opener.document.forms[0].submit();");
					strBuilder.Append("window.alert(\"O Exame Audiométrico foi " 
                        + tipo + " com sucesso! Porém, não foi possível gravar as imagens dos Audiogramas no servidor! " 
                        + stError.ToString() + "\");");

                    this.ClientScript.RegisterStartupScript(this.GetType(), "CadastroEdicaoAudiometria", strBuilder.ToString(), true);

                    //ChtDiagramaOD.Width = Unit.Pixel(380);
                    //ChtDiagramaOD.Height = Unit.Pixel(270);

                    //ChtDiagramaOE.Width = Unit.Pixel(380);
                    //ChtDiagramaOE.Height = Unit.Pixel(270);
				}
				else
                    MsgBox1.Show("Ilitera.Net", ex.Message, null,
                           new EO.Web.MsgBoxButton("OK"));

			}
		}

		protected void btnExcluir_Click(object sender, System.EventArgs e)
		{
			try
			{
				DataSet dsCheckExameReferencial = new AudiometriaAudiograma().Get("IdAudiogramaReferencial IN (SELECT IdAudiometriaAudiograma FROM AudiometriaAudiograma WHERE IdAudiometria=" + exameAudiometrico.Id + ")");
				if (dsCheckExameReferencial.Tables[0].Rows.Count > 0)
					throw new Exception("Não é possível excluir este Exame Audiométrico! Ele está cadastrado como um Exame Referencial de um ou mais Exames Sequenciais!");

                ProntuarioDigital xProntuario = new ProntuarioDigital();
                xProntuario.Find("IdExameBase = " + exameAudiometrico.Id.ToString() + " ");
                if (xProntuario.Id != 0)
                {
                    xProntuario.Delete();
                }


                exameAudiometrico.UsuarioId = System.Convert.ToInt32( Request["IdUsuario"].ToString());
				exameAudiometrico.Delete();

				ViewState["IdExameAudiometrico"] = null;

				StringBuilder strBuilder = new StringBuilder();

				strBuilder.Append("window.opener.document.getElementById('txtAuxiliar').value='atualiza';");
				strBuilder.Append("window.opener.document.forms[0].submit();");
				strBuilder.Append("window.alert('O Exame Audiométrico foi excluído com sucesso!');");
				strBuilder.Append("self.close();");

                this.ClientScript.RegisterStartupScript(this.GetType(), "ExcluirAudiometria", strBuilder.ToString(), true);
                Response.Write("<script>window.opener.document.forms[0].submit(); window.close();</script>");

                Entities.Usuario user = (Entities.Usuario)Session["usuarioLogado"];


                if (Session["Retorno"].ToString() == "1")
                    Response.Redirect("DadosEmpregado.aspx?IdEmpregado=" + Session["Empregado"].ToString() + "&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString() + "&Tipo=4");
                else
                    Response.Redirect("DadosEmpregado_Lista.aspx?IdEmpregado=" + Session["Empregado"].ToString() + "&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString() + "&Tipo=6");



			}
			catch(Exception ex)
			{
                MsgBox1.Show("Ilitera.Net", ex.Message, null,
                       new EO.Web.MsgBoxButton("OK"));

			}
		}

		protected void lkbAudiograma_Click(object sender, System.EventArgs e)
		{
            //bool isSaved = false;
			
            //try
            //{
            //    SaveExameAudiometrico();

            //    //isSaved = true;

            //    ////PopulaChartAudiograma(ChtDiagramaOD, Orelha.Direita);
            //    ////PopulaChartAudiograma(ChtDiagramaOE, Orelha.Esquerda);

            //    ////SalvarChartAudiograma(ChtDiagramaOD, ChtDiagramaOE);

            //    //StringBuilder strBuilder = new StringBuilder();

            //    //strBuilder.Append("window.opener.document.getElementById('txtAuxiliar').value='atualiza';");
            //    //strBuilder.Append("window.opener.document.forms[0].submit();");

            //    //this.ClientScript.RegisterStartupScript(this.GetType(), "ViewAudiograma", strBuilder.ToString(), true);

            //    Guid strAux = Guid.NewGuid();

            //    strOpenReport("PCMSO", "Audiograma.aspx?IliteraSystem=" + "99"
            //        + "&IdEmpresa=" + Request["IdEmpresa"].ToString() + "&IdExame=" + exameAudiometrico.Id + "&IdEmpregado=" + Request["IdEmpregado"].ToString() + "&IdUsuario=" + Request["IdUsuario"].ToString(), "Audiograma");
            //}
            //catch(Exception ex)
            //{
            //    if (isSaved)
            //    {
            //        StringBuilder stError = new StringBuilder();
            //        stError.Append(ex.Message);

            //        stError.Replace("\'", string.Empty);
            //        stError.Replace("\r", string.Empty);
            //        stError.Replace("\n", string.Empty);

            //        StringBuilder strBuilder = new StringBuilder();

            //        strBuilder.Append("window.alert(\"Não foi possível gravar as imagens dos Audiogramas no servidor! " + stError.ToString() + "\");");
            //        strBuilder.Append("window.opener.document.getElementById('txtAuxiliar').value='atualiza';");
            //        strBuilder.Append("window.opener.document.forms[0].submit();");

            //        this.ClientScript.RegisterStartupScript(this.GetType(), "ViewAudiograma", strBuilder.ToString(), true);

            //        //ChtDiagramaOD.Width = Unit.Pixel(380);
            //        //ChtDiagramaOD.Height = Unit.Pixel(270);

            //        //ChtDiagramaOE.Width = Unit.Pixel(380);
            //        //ChtDiagramaOE.Height = Unit.Pixel(270);

            //        Guid strAux = Guid.NewGuid();

            //        //OpenReport("PCMSO", "Audiograma.aspx?MestraSystem=" + strAux.ToString() + strAux.ToString()
            //        //+ "&IdEmpresa=" + Request["IdEmpresa"].ToString() + "&IdExame=" + exameAudiometrico.Id + "&IdEmpregado=" + Request["Empregado"].ToString() + "&IdUsuario=" + Request["IdUsuario"].ToString(), "Audiograma");
            //    }
            //    else                    
            //    MsgBox1.Show("Ilitera.Net", ex.Message, null,
            //           new EO.Web.MsgBoxButton("OK"));

            //}
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
                    + "&Key=" + strAux.ToString() + "IliteraNet" + strAux.ToString()
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



		private void SaveExameAudiometrico()
		{
			PopulaAudiometria();
			PopulaAnamnese();

			AudiometriaAudiograma audiogramaOD = WbUsrCntrlAudiogramaDireito.GetAudiograma(exameAudiometrico,0);
			AudiometriaAudiograma audiogramaOE = WbUsrCntrlAudiogramaEsquerda.GetAudiograma(exameAudiometrico,1);

            //if (audiogramaOD.Id == 0)
            //{
            //    audiogramaOD.IndOrelha = 0;
            //    audiogramaOE.IndOrelha = 1;
            //}

			Audiometria.SalvarAudiometria(exameAudiometrico, audiogramaOD, audiogramaOE, anamneseAudiologica);

			WbUsrCntrlAudiogramaDireito.InterpretacaoAtual = audiogramaOD.InterpretacaoPortaria19();
			WbUsrCntrlAudiogramaEsquerda.InterpretacaoAtual = audiogramaOE.InterpretacaoPortaria19();

			ViewState["IdExameAudiometrico"] = exameAudiometrico.Id;
			btnExcluir.Enabled = true;

            Response.Write("<script>window.opener.document.forms[0].submit(); window.close();</script>"); 

		}

		protected void lkbAnamnese_Click(object sender, System.EventArgs e)
		{
        //    bool isSaved = false;
			
        //    try
        //    {
        //        SaveExameAudiometrico();

        //        isSaved = true;

        //        //PopulaChartAudiograma(ChtDiagramaOD, Orelha.Direita);
        //        //PopulaChartAudiograma(ChtDiagramaOE, Orelha.Esquerda);

        //        //SalvarChartAudiograma(ChtDiagramaOD, ChtDiagramaOE);

        //        StringBuilder strBuilder = new StringBuilder();

        //        strBuilder.Append("window.opener.document.getElementById('txtAuxiliar').value='atualiza';");
        //        strBuilder.Append("window.opener.document.forms[0].submit();");

        //        this.ClientScript.RegisterStartupScript(this.GetType(), "ViewAnamnese", strBuilder.ToString(), true);

        //        Guid strAux = Guid.NewGuid();

        //        //OpenReport("PCMSO", "AnamneseAud.aspx?MestraSystem=" + strAux.ToString() + strAux.ToString()
        //        //    + "&IdEmpresa=" + Request["IdEmpresa"].ToString() + "&IdExame=" + exameAudiometrico.Id + "&IdEmpregado=" + Request["Empregado"].ToString() + "&IdUsuario=" + Request["IdUsuario"].ToString(), "AnamneseAudiologica");
					
        //    }
        //    catch(Exception ex)
        //    {
        //        if (isSaved)
        //        {
        //            StringBuilder stError = new StringBuilder();
        //            stError.Append(ex.Message);

        //            stError.Replace("\'", string.Empty);
        //            stError.Replace("\r", string.Empty);
        //            stError.Replace("\n", string.Empty);
					
        //            StringBuilder strBuilder = new StringBuilder();

        //            strBuilder.Append("window.alert(\"Não foi possível gravar as imagens dos Audiogramas no servidor! " + stError.ToString() + "\");");
        //            strBuilder.Append("window.opener.document.getElementById('txtAuxiliar').value='atualiza';");
        //            strBuilder.Append("window.opener.document.forms[0].submit();");

        //            this.ClientScript.RegisterStartupScript(this.GetType(), "ViewAnamnese", strBuilder.ToString(), true);

        //            //ChtDiagramaOD.Width = Unit.Pixel(380);
        //            //ChtDiagramaOD.Height = Unit.Pixel(270);

        //            //ChtDiagramaOE.Width = Unit.Pixel(380);
        //            //ChtDiagramaOE.Height = Unit.Pixel(270);

        //            Guid strAux = Guid.NewGuid();

        //            //OpenReport("PCMSO", "AnamneseAud.aspx?MestraSystem=" + strAux.ToString() + strAux.ToString()
        //            //    + "&IdEmpresa=" + Request["IdEmpresa"].ToString() + "&IdExame=" + exameAudiometrico.Id + "&IdEmpregado=" + Request["Empregado"].ToString() + "&IdUsuario=" + Request["IdUsuario"].ToString(), "AnamneseAudiologica");
        //        }
        //        else
        //            MsgBox1.Show("Ilitera.Net", ex.Message + " Para visualizar a Anamnese Audiológica deve-se antes salvar o Exame Audiométrico!", null,
        //                   new EO.Web.MsgBoxButton("OK"));

        //    }
        }

		protected void ddlClinica_SelectedIndexChanged(object sender, EventArgs e)
		{
            if (ddlClinica.SelectedValue.Equals("0"))
            {
                while (ddlAudiometro.Items.Count > 1)
                    ddlAudiometro.Items.RemoveAt(1);

                while (ddlMedico.Items.Count > 1)
                    ddlMedico.Items.RemoveAt(1);
            }
            else
            {
                PopulaDDLAudiometro();
                PopulaDDLMedico();
            }
		}

        protected void cmd_Voltar_Click(object sender, EventArgs e)
        {
            Entities.Usuario user = (Entities.Usuario)Session["usuarioLogado"];

            if (Session["Retorno"].ToString() == "1")
                Response.Redirect("DadosEmpregado.aspx?IdEmpregado=" + Session["Empregado"].ToString() + "&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString() + "&Tipo=4");
            else
                Response.Redirect("DadosEmpregado_Lista.aspx?IdEmpregado=" + Session["Empregado"].ToString() + "&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString() + "&Tipo=6");

        }

        #region Metodos Chart

        //private void InicializeChart(Dundas.Charting.WebControl.Chart chart, Orelha orelha)
        //{
        ////Clear Chart
        //chart.Series.Clear();
        //chart.ChartAreas.Clear();
        //chart.Legends.Clear();

        //Dundas.Charting.WebControl.ChartArea chartArea = new Dundas.Charting.WebControl.ChartArea();

        //chartArea.Name = "Default";

        //// Set Eixo X
        //chartArea.AxisX.LabelsAutoFit = false;
        //chartArea.AxisX.Maximum = Double.NaN;
        //chartArea.AxisX.Minimum = Double.NaN;
        ////chartArea1.AxisX.Title = "Hz";
        //chartArea.AxisX.Logarithmic = true;
        //chartArea.AxisX.LogarithmBase = 10D;
        //chartArea.AxisX.TitleFont = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
        //chartArea.AxisX.LineStyle = Dundas.Charting.WebControl.ChartDashStyle.Dash;
        //chartArea.AxisX.MajorGrid.LineStyle = Dundas.Charting.WebControl.ChartDashStyle.Dash;
        //chartArea.AxisX.MajorTickMark.Enabled = false;
        //chartArea.AxisX.MinorTickMark.Enabled = false;

        //// Set Eixo Y
        //chartArea.AxisY.Interval = 10;
        //chartArea.AxisY.IntervalType = Dundas.Charting.WebControl.DateTimeIntervalType.Number;
        //chartArea.AxisY.StartFromZero = true;
        //chartArea.AxisY.LabelsAutoFit = false;
        ////chartArea1.AxisY.Title = "Db";
        //chartArea.AxisY.Maximum = 130;
        //chartArea.AxisY.Minimum = -10;
        //chartArea.AxisY.Reverse = true;
        //chartArea.AxisY.TitleFont = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
        //chartArea.AxisY.LineStyle = Dundas.Charting.WebControl.ChartDashStyle.Dash;
        //chartArea.AxisY.MajorGrid.LineStyle = Dundas.Charting.WebControl.ChartDashStyle.Dash;
        //chartArea.AxisY.MajorTickMark.Enabled = false;
        //chartArea.AxisY.MinorTickMark.Enabled = false;

        //chart.ChartAreas.Add(chartArea);

        //AddSeries(chart, orelha);

        //AddCustomLabel(chart);
        //}

        //private void AddSeries(Dundas.Charting.WebControl.Chart chart, Orelha orelha)
        //{
        //    if (orelha == Orelha.Direita)
        //    {
        //        //Aéreo Orelha Direita
        //        Dundas.Charting.WebControl.Series seriesOD = new Dundas.Charting.WebControl.Series("AereoOD");
        //        seriesOD.ChartType = "Line";
        //        seriesOD.Color = System.Drawing.Color.Red;
        //        seriesOD.BorderStyle = Dundas.Charting.WebControl.ChartDashStyle.Solid;
        //        seriesOD.BorderWidth = 2;
        //        chart.Series.Add(seriesOD);

        //        //Osseo Orelha Direita
        //        Dundas.Charting.WebControl.Series seriesOsseoOD = new Dundas.Charting.WebControl.Series("OsseoOD");
        //        seriesOsseoOD.ChartType = "Point";
        //        seriesOsseoOD.Color = System.Drawing.Color.Red;
        //        chart.Series.Add(seriesOsseoOD);
        //    }
        //    else if (orelha == Orelha.Esquerda)
        //    {
        //        //Aéreo Orelha Esquerda
        //        Dundas.Charting.WebControl.Series seriesOE = new Dundas.Charting.WebControl.Series("AereoOE");
        //        seriesOE.ChartType = "Line";
        //        seriesOE.Color = System.Drawing.Color.Blue;
        //        seriesOE.BorderStyle = Dundas.Charting.WebControl.ChartDashStyle.Dash;
        //        seriesOE.BorderWidth = 2;
        //        chart.Series.Add(seriesOE);			

        //        //Osseo Orelha Esquerda
        //        Dundas.Charting.WebControl.Series seriesOsseoOE = new Dundas.Charting.WebControl.Series("OsseoOE");
        //        seriesOsseoOE.ChartType = "Point";
        //        seriesOsseoOE.Color = System.Drawing.Color.Blue;
        //        chart.Series.Add(seriesOsseoOE);
        //    }

        //    //Empty
        //    Dundas.Charting.WebControl.Series seriesEmpty = new Dundas.Charting.WebControl.Series("Empty");
        //    seriesEmpty.ChartType = "Point";
        //    chart.Series.Add(seriesEmpty);

        //    Dundas.Charting.WebControl.DataPoint dataPoint = new Dundas.Charting.WebControl.DataPoint(0.15D, 15.0D);
        //    dataPoint.Color = Color.White;
        //    chart.Series["Empty"].Points.Add(dataPoint);

        //    dataPoint = new Dundas.Charting.WebControl.DataPoint(7.5D, 15.0D);
        //    dataPoint.Color = Color.White;
        //    chart.Series["Empty"].Points.Add(dataPoint);
        //}

        //private void AddCustomLabel(Dundas.Charting.WebControl.Chart chart)
        //{
        //    AddCustomLabel(System.Math.Log10(0.125D), System.Math.Log10(0.5D),"250", chart);
        //    AddCustomLabel(System.Math.Log10(0.25D), System.Math.Log10(1D), "500", chart);
        //    AddCustomLabel(System.Math.Log10(0.5D), System.Math.Log10(2D), "1K", chart);
        //    AddCustomLabel(System.Math.Log10(1.75D), System.Math.Log10(2.25D), "2K", chart);
        //    AddCustomLabel(System.Math.Log10(2.5D), System.Math.Log10(3.5D), "3K", chart);
        //    AddCustomLabel(System.Math.Log10(3.5D), System.Math.Log10(4.5D), "4K", chart);
        //    AddCustomLabel(System.Math.Log10(5.0D), System.Math.Log10(7.0D), "6K", chart);
        //    AddCustomLabel(System.Math.Log10(7.0D), System.Math.Log10(9.0D), "8K", chart);
        //    AddCustomLabel(System.Math.Log10(9.0D), System.Math.Log10(11.0D), string.Empty, chart);
        //}

        //private void AddCustomLabel(double from, double to, string text, Dundas.Charting.WebControl.Chart chart)
        //{
        //    Dundas.Charting.WebControl.CustomLabel customLabel = new Dundas.Charting.WebControl.CustomLabel();

        //    customLabel.From  = from;
        //    customLabel.To  = to;
        //    customLabel.Text = text;

        //    int element = chart.ChartAreas["Default"].AxisX.CustomLabels.Add(customLabel);

        //    chart.ChartAreas["Default"].AxisX.CustomLabels[element].GridTick = Dundas.Charting.WebControl.GridTick.GridLine;
        //}

        //private void PopulaChartAudiograma(Dundas.Charting.WebControl.Chart chart, Orelha orelha)
        //{
        //    ClearPoints(chart);

        //    AudiometriaAudiograma audiograma = exameAudiometrico.GetAudiograma(orelha);

        //    if (orelha == Orelha.Direita)
        //    {
        //        if(!audiograma.IsAudiogramaAereoEmBranco())
        //        {
        //            AddDataPoits(0.25D,audiograma.Aereo250, true, false, audiograma.IsAereoMascarado250, chart);
        //            AddDataPoits(0.5D, audiograma.Aereo500, true, false, audiograma.IsAereoMascarado500, chart);
        //            AddDataPoits(1.0D, audiograma.Aereo1000, true, false, audiograma.IsAereoMascarado1000, chart);
        //            AddDataPoits(2.0D, audiograma.Aereo2000, true, false, audiograma.IsAereoMascarado2000, chart);
        //            AddDataPoits(3.0D, audiograma.Aereo3000, true, false, audiograma.IsAereoMascarado3000, chart);
        //            AddDataPoits(4.0D, audiograma.Aereo4000, true, false, audiograma.IsAereoMascarado4000, chart);
        //            AddDataPoits(6.0D, audiograma.Aereo6000, true, false, audiograma.IsAereoMascarado6000, chart);
        //            AddDataPoits(8.0D, audiograma.Aereo8000, true, false, audiograma.IsAereoMascarado8000, chart);
        //        }

        //        if(!audiograma.IsAudiogramaOsseoEmBranco())
        //        {
        //            AddDataPoits(0.5D, audiograma.Osseo500, true, true, audiograma.IsOsseoMascarado500, chart);
        //            AddDataPoits(1.0D, audiograma.Osseo1000, true, true, audiograma.IsOsseoMascarado1000, chart);
        //            AddDataPoits(2.0D, audiograma.Osseo2000, true, true, audiograma.IsOsseoMascarado2000, chart);
        //            AddDataPoits(3.0D, audiograma.Osseo3000, true, true, audiograma.IsOsseoMascarado3000, chart);
        //            AddDataPoits(4.0D, audiograma.Osseo4000, true, true, audiograma.IsOsseoMascarado4000, chart);
        //            AddDataPoits(6.0D, audiograma.Osseo6000, true, true, audiograma.IsOsseoMascarado6000, chart);
        //        }
        //    }
        //    else if (orelha == Orelha.Esquerda)
        //    {
        //        if(!audiograma.IsAudiogramaAereoEmBranco())
        //        {
        //            AddDataPoits(0.25D,audiograma.Aereo250, false, false, audiograma.IsAereoMascarado250, chart);
        //            AddDataPoits(0.5D, audiograma.Aereo500, false, false, audiograma.IsAereoMascarado500, chart);
        //            AddDataPoits(1.0D, audiograma.Aereo1000, false, false, audiograma.IsAereoMascarado1000, chart);
        //            AddDataPoits(2.0D, audiograma.Aereo2000, false, false, audiograma.IsAereoMascarado2000, chart);
        //            AddDataPoits(3.0D, audiograma.Aereo3000, false, false, audiograma.IsAereoMascarado3000, chart);
        //            AddDataPoits(4.0D, audiograma.Aereo4000, false, false, audiograma.IsAereoMascarado4000, chart);
        //            AddDataPoits(6.0D, audiograma.Aereo6000, false, false, audiograma.IsAereoMascarado6000, chart);
        //            AddDataPoits(8.0D, audiograma.Aereo8000, false, false, audiograma.IsAereoMascarado8000, chart);
        //        }
        //        if(!audiograma.IsAudiogramaOsseoEmBranco())
        //        {
        //            AddDataPoits(0.5D, audiograma.Osseo500, false, true, audiograma.IsOsseoMascarado500, chart);
        //            AddDataPoits(1.0D, audiograma.Osseo1000, false, true, audiograma.IsOsseoMascarado1000, chart);
        //            AddDataPoits(2.0D, audiograma.Osseo2000, false, true, audiograma.IsOsseoMascarado2000, chart);
        //            AddDataPoits(3.0D, audiograma.Osseo3000, false, true, audiograma.IsOsseoMascarado3000, chart);
        //            AddDataPoits(4.0D, audiograma.Osseo4000, false, true, audiograma.IsOsseoMascarado4000, chart);
        //            AddDataPoits(6.0D, audiograma.Osseo6000, false, true, audiograma.IsOsseoMascarado6000, chart);
        //        }
        //    }
        //}

        //private void ClearPoints(Dundas.Charting.WebControl.Chart chart)
        //{
        //    foreach(Dundas.Charting.WebControl.Series series in chart.Series)
        //        if(series.Name!="Empty")
        //            series.Points.Clear();
        //}

        //private void AddDataPoits(double Hz, string strdB, bool IsDireita,	bool IsOsseo, bool IsMascarada, 
        //                            Dundas.Charting.WebControl.Chart chart)
        //{
        //    Dundas.Charting.WebControl.DataPoint dataPoint;

        //    if(strdB=="NE" || strdB==string.Empty)
        //        return;

        //    double dB = AudiometriaAudiograma.GetVal(strdB, IsOsseo);

        //    dataPoint = new Dundas.Charting.WebControl.DataPoint(Hz, dB);

        //    bool IsAusencia = AudiometriaAudiograma.IsAusenciaResposta(strdB);

        //    dataPoint.MarkerBorderColor = Color.FromArgb(((System.Byte)(64)), ((System.Byte)(64)), ((System.Byte)(64)));
        //    dataPoint.MarkerImageTranspColor = Color.Magenta;
        //    dataPoint.MarkerStyle = Dundas.Charting.WebControl.MarkerStyle.None;
        //    dataPoint.ToolTip = "#VALY db em #VALX KHz"; 

        //    Dundas.Charting.WebControl.Series series;

        //    if(IsOsseo || IsAusencia)
        //    {
        //        if(IsDireita)
        //            series = chart.Series["OsseoOD"];
        //        else
        //            series = chart.Series["OsseoOE"];
        //    }
        //    else
        //    {
        //        if(IsDireita)
        //            series = chart.Series["AereoOD"];
        //        else
        //            series = chart.Series["AereoOE"];
        //    }

        //    if(!IsOsseo)
        //    {
        //        if(!IsAusencia)
        //        {
        //            if(!IsMascarada)
        //            {
        //                if(IsDireita)
        //                    dataPoint.MarkerImage = "http://www.mestra.net/DocsDigitais/+Audiograma/ODAerea.bmp";
        //                else
        //                    dataPoint.MarkerImage = "http://www.mestra.net/DocsDigitais/+Audiograma/OEAerea.bmp";
        //            }
        //            else
        //            {
        //                if(IsDireita)
        //                    dataPoint.MarkerImage = "http://www.mestra.net/DocsDigitais/+Audiograma/ODAereaMasc.bmp";
        //                else
        //                    dataPoint.MarkerImage = "http://www.mestra.net/DocsDigitais/+Audiograma/OEAereaMasc.bmp";
        //            }
        //        }
        //        else
        //        {
        //            dataPoint.YValues[0] = chart.ChartAreas["Default"].AxisY.Maximum;

        //            if(!IsMascarada)
        //            {
        //                if(IsDireita)
        //                    dataPoint.MarkerImage = "http://www.mestra.net/DocsDigitais/+Audiograma/ODAereaAusencia.bmp";
        //                else
        //                    dataPoint.MarkerImage = "http://www.mestra.net/DocsDigitais/+Audiograma/OEAereaAusencia.bmp";
        //            }
        //            else
        //            {
        //                if(IsDireita)
        //                    dataPoint.MarkerImage = "http://www.mestra.net/DocsDigitais/+Audiograma/ODAereaMascAusencia.bmp";
        //                else
        //                    dataPoint.MarkerImage = "http://www.mestra.net/DocsDigitais/+Audiograma/OEAereaMascAusencia.bmp";
        //            }
        //        }
        //    }
        //    else
        //    {
        //        if(!IsAusencia)
        //        {
        //            if(!IsMascarada)
        //            {
        //                if(IsDireita)
        //                    dataPoint.MarkerImage = "http://www.mestra.net/DocsDigitais/+Audiograma/ODOssea.bmp";
        //                else
        //                    dataPoint.MarkerImage = "http://www.mestra.net/DocsDigitais/+Audiograma/OEOssea.bmp";
        //            }
        //            else
        //            {
        //                if(IsDireita)
        //                    dataPoint.MarkerImage = "http://www.mestra.net/DocsDigitais/+Audiograma/ODOsseaMasc.bmp";
        //                else
        //                    dataPoint.MarkerImage = "http://www.mestra.net/DocsDigitais/+Audiograma/OEOsseaMasc.bmp";
        //            }
        //        }
        //        else
        //        {
        //            if(!IsMascarada)
        //            {
        //                if(IsDireita)
        //                    dataPoint.MarkerImage = "http://www.mestra.net/DocsDigitais/+Audiograma/ODOsseaAusencia.bmp";
        //                else
        //                    dataPoint.MarkerImage = "http://www.mestra.net/DocsDigitais/+Audiograma/OEOsseaAusencia.bmp";
        //            }
        //            else
        //            {
        //                if(IsDireita)
        //                    dataPoint.MarkerImage = "http://www.mestra.net/DocsDigitais/+Audiograma/ODOsseaMascAusencia.bmp";
        //                else
        //                    dataPoint.MarkerImage = "http://www.mestra.net/DocsDigitais/+Audiograma/OEOsseaMascAusencia.bmp";
        //            }
        //        }
        //    }

        //    series.Points.Add(dataPoint);
        //}

        //private void SalvarChartAudiograma(Dundas.Charting.WebControl.Chart chartOD, Dundas.Charting.WebControl.Chart chartOE)
        //{			
        //    string fileGraficoOD = (WbUsrCntrlAudiogramaDireito.GetAudiograma(exameAudiometrico)).GetArquivo(cliente);
        //    string fileGraficoOE = (WbUsrCntrlAudiogramaEsquerda.GetAudiograma(exameAudiometrico)).GetArquivo(cliente);

        //    chartOD.Width = Unit.Pixel(503);
        //    chartOD.Height = Unit.Pixel(460);

        //    chartOE.Width = Unit.Pixel(503);
        //    chartOE.Height = Unit.Pixel(460);

        //    chartOD.Save(fileGraficoOD);
        //    chartOE.Save(fileGraficoOE);

        //    chartOD.Width = Unit.Pixel(380);
        //    chartOD.Height = Unit.Pixel(270);

        //    chartOE.Width = Unit.Pixel(380);
        //    chartOE.Height = Unit.Pixel(270);
        //}

        #endregion






        protected void gridAnamnese_ItemCommand(object sender, EO.Web.GridCommandEventArgs e)
        {
            //Check whether it is from our client side
            //JavaScript call


            //Session["Id2"] = e.Item.Key.ToString();
            //Session["NomeEmpregado"] = e.Item.Cells[0].Value.ToString();



        }



        private void Carregar_Grid_Anamnese_Capgemini()
        {

            if (exameAudiometrico.Id == 0)
            {
                // criar registros padrão,  mas como criar se idclinico = 0
                return;
            }

            //DataSet ds = new Ilitera.Opsa.Data.Anamnese_Questao().Get(" Questao is not null order by Sistema, Questao ");

            Ilitera.Data.Clientes_Funcionarios zQuest = new Ilitera.Data.Clientes_Funcionarios();

            DataSet ds = zQuest.Trazer_Anamnese_Exame(exameAudiometrico.Id);


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


            if (exameAudiometrico.Id == 0)
            {
                return;
            }



            List<Anamnese_Exame> AnExame = new Anamnese_Exame().Find<Anamnese_Exame>(" IdExameBase = " + exameAudiometrico.Id);



            if (AnExame.Count == 0)
            {
                //trazer padrão para cliente
                exameAudiometrico.IdEmpregado.nID_EMPR.Find();
                List<Anamnese_Dinamica> anExamePadrao = new Anamnese_Dinamica().Find<Anamnese_Dinamica>(" IdPessoa = " + exameAudiometrico.IdEmpregado.nID_EMPR.Id);


                if (anExamePadrao.Count == 0)
                {
                    return;
                }
                else
                {
                    Ilitera.Data.Clientes_Funcionarios xAnam = new Ilitera.Data.Clientes_Funcionarios();
                    xAnam.Carregar_Anamnese_Dinamica(exameAudiometrico.IdEmpregado.nID_EMPR.Id, exameAudiometrico.Id);

                    //foreach (Anamnese_Dinamica zPadrao in anExamePadrao)
                    //{
                    //    Anamnese_Exame rTestes = new Anamnese_Exame();

                    //    rTestes.IdAnamneseDinamica = zPadrao.Id;
                    //    rTestes.IdExameBase = exameAudiometrico.Id;
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



    }
}
