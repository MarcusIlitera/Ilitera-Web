
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
using System.Text;

using System.Data.SqlClient;
using Entities;

namespace Ilitera.Net.OrdemDeServico
{
	/// <summary>
	/// Summary description for CadProcedimento.
	/// </summary>
	public partial class CadProcedimento : System.Web.UI.Page
	{

        protected Color backColorDisabledBox = Color.FromName("#EBEBEB");
        protected Color backColorEnabledBox = Color.FromName("#FCFEFD");
        protected Color foreColorDisabledLabel = Color.Gray;
        protected Color foreColorEnabledLabel = Color.FromName("#44926D");
        protected Color foreColorEnabledTextBox = Color.FromName("#004000");
        protected Color foreColorDisabledTextBox = Color.Gray;
        protected Color borderColorDisabledBox = Color.LightGray;
        protected Color borderColorEnabledBox = Color.FromName("#7CC5A1");
        protected Color backColorEnabledBoxYellow = Color.LightYellow;

        protected Ilitera.Common.Usuario usuario = new Ilitera.Common.Usuario();
        protected Cliente cliente = new Cliente();

		#region Declaração dos Web Components da página

		//Dados Basicos	
        //private TextBox txtnPOPs;
        //private RadioButtonList rblTipoProcedimento;
        //private System.Web.UI.WebControls.Image imgFoto;
        //private LinkButton lkbRemoverImagem;	
        //private TextBox txtNome;
        //private TextBox txtDescricao;
        //private DropDownList ddlProcedimentoResumo;
        //private Label lblProcedimentoResumo;
        //private Label lblTipoProcedimento;

        ////Dados Complementares
        //private ListBox lstSetor;
        //private ListBox lstEquipamento;
        //private ListBox lstCelula;
        //private ListBox lstFerramenta;
        //private ListBox lstProduto;
        //private ListBox lstSetorProc;
        //private ListBox lstEquipamentoProc;
        //private ListBox lstCelulaProc;
        //private ListBox lstFerramentaProc;
        //private ListBox lstProdutoProc;
        //private ImageButton imbAddSetor;
        //private ImageButton imbRemoveSetor;
        //private ImageButton imbAddEquipamento;
        //private ImageButton imbRemoveEquipamento;
        //private ImageButton imbAddCelula;
        //private ImageButton imbRemoveCelula;
        //private ImageButton imbAddFerramenta;
        //private ImageButton imbRemoveFerramenta;
        //private ImageButton imbAddProduto;
        //private ImageButton imbRemoveProduto;
        //private Label lblEquipamento;
        //private Label lblCelula;
        //private Label lblFerramenta;
        //private Label lblProduto;

        ////Medidas de Controle
        ////private Infragistics.WebUI.UltraWebTab.UltraWebTab UltraWebTabMedControle;
        //private ListBox lsbEPI;
        //private ImageButton imbDown;
        //private ImageButton imbUp;
        //private ListBox lstBoxEPIs;
        //private TextBox txtMedidasAdm;
        //private TextBox txtMedidasEdu;
        //private TextBox txtEPC;
        //private DropDownList ddlLaudos;
        //private DropDownList ddlGHE;
        //private ListBox lstBoxEPIGHE;
        //private TextBox txtMedidasAdmGHE;
        //private TextBox txtEPCGHE;
        //private TextBox txtMedidasEduGHE;
        //private Button btnTransfMedidasControle;
        //private Label lblEPI;
        //private Label lblEPC;
        //private Label lblMedidasAdm;
        //private Label lblMedidasEdu;

        ////Operacoes
        ////private Infragistics.WebUI.UltraWebTab.UltraWebTab UltraWebTabOperacoes;
        //private ListBox lstBoxOperacoes;
        //private ImageButton imbUpOperacao;
        //private ImageButton imbDownOperacao;
        //private Button btnExcluirOpe;
        //private TextBox txtOperacao;
        //private TextBox txtObsQualidade;
        //private Button btnGravarOperacao;
        //private ListBox lsbRiscos;
        //private ListBox listBxRiscosOperacao;
        //private Button btnGravarRisco;
        //private Label lblOperacao;

        ////Atualizacoes
        //private ListBox lstBoxAtualizacoes;
        //private TextBox wdtDataAtualizacao;
        //private TextBox txtDescricaoAtualizacao;
        //private DropDownList ddlResponsavelAtualizacao;
        //private DropDownList ddlOperadorAtualizacao;
        //private Button btnGravarAtualizacao;
        //private Button btnRemoverAtualizacao;
        //private Label lblAtualizacoes;
		
		#endregion

		private Procedimento procedimento;
		private bool saveProcedimentoProcess;
		
		protected void Page_Load(object sender, System.EventArgs e)
		{



            InicializaWebPageObjects();

            Entities.Usuario user = (Entities.Usuario)Session["usuarioLogado"];

            int xIdEmpresa = System.Convert.ToInt32(Session["Empresa"]);
            int xIdUsuario = user.IdUsuario;

            lbl_Id_Empresa.Text = xIdEmpresa.ToString().Trim();
            lbl_Id_Usuario.Text = xIdUsuario.ToString().Trim();


            //PopulaDdls();

            if (!IsPostBack)
			{

              

                RegisterClientCode(procedimento);
                PopulaDdls();

				if (procedimento.Id != 0)
					PopulaTela();
				else
				{
					lblProcedimentoResumo.ForeColor = foreColorDisabledLabel;
					ddlProcedimentoResumo.ClearSelection();
					ddlProcedimentoResumo.Enabled = false;
					lstBoxOperacoes.Items.Insert(0, new ListItem("Cadastrar Nova Operação...", "0"));
					lstBoxOperacoes.Items[0].Selected = true;
					lstBoxAtualizacoes.Items.Insert(0, new ListItem("Cadastrar Nova Atualização...", "0"));
					lstBoxAtualizacoes.Items[0].Selected = true;

					btnDuplicarProc.Enabled = false;
					btnExcluir.Enabled = false;
                    lkbFichaPOPs.Enabled = false;
                    btnAPP.Enabled = false;
				}
			}
			else
			{
				if (txtAuxiliar.Value.Equals("atualizaEquipamento"))
				{
					PopulaLsbEquipamento();
                    PopulaLsbEquipamentoProc();
				}
				else if (txtAuxiliar.Value.Equals("atualizaCelula"))
				{
					PopulaLsbCelula();
					PopulaLsbCelulaProc();
				}
				else if (txtAuxiliar.Value.Equals("atualizaFerramenta"))
				{
					PopulaLsbFerramenta();
					PopulaLsbFerramentaProc();
				}
				else if (txtAuxiliar.Value.Equals("atualizaProduto"))
				{
					PopulaLsbProduto();
					PopulaLsbProdutoProc();
				}
				else if (txtAuxiliar.Value.Equals("atualizaPerigo"))
				{
                    if (!lstBoxOperacoes.SelectedValue.Equals("0"))
                        PopulaLsbPerigos(Convert.ToInt32(lstBoxOperacoes.SelectedValue));
				}
				else if (txtAuxiliar.Value.Equals("atualizaColaborador"))
				{
					PopulaDDLResponsaveisAtualizacao();
				}
                else if (txtAuxiliar.Value.Equals("atualizaAspecto"))
                {
                   if (!lstBoxOperacoes.SelectedValue.Equals("0"))
                        PopulaLsbAspectos(Convert.ToInt32(lstBoxOperacoes.SelectedValue));
                }

                txtAuxiliar.Value = string.Empty;
            }
		}

		#region Inicialização de componentes do WebTab
		
        //protected void InicializaComponentes()
        //{
        //    //Dados Basicos	
        //    txtnPOPs = (TextBox)FindControl("tabtxtnPOPs");
        //    rblTipoProcedimento = (RadioButtonList)FindControl("tabrblTipoProcedimento");
        //    imgFoto = (System.Web.UI.WebControls.Image)FindControl("tabimgFoto");
        //    lkbRemoverImagem = (LinkButton)FindControl("tablkbRemoverImagem");
        //    txtNome = (TextBox)FindControl("tabtxtNome");
        //    txtDescricao = (TextBox)FindControl("tabtxtDescricao");
        //    ddlProcedimentoResumo = (DropDownList)FindControl("tabddlProcedimentoResumo");
        //    lblProcedimentoResumo = (Label)FindControl("tablblProcedimentoResumo");
        //    lblTipoProcedimento = (Label)FindControl("tablblTipoProcedimento");

        //    //Dados Complementares
        //    lstSetor = (ListBox)FindControl("tablstSetor");
        //    lstEquipamento = (ListBox)FindControl("tablstEquipamento");
        //    lstCelula = (ListBox)FindControl("tablstCelula");
        //    lstFerramenta = (ListBox)FindControl("tablstFerramenta");
        //    lstProduto = (ListBox)FindControl("tablstProduto");
        //    lstSetorProc = (ListBox)FindControl("tablstSetorProc");
        //    lstEquipamentoProc = (ListBox)FindControl("tablstEquipamentoProc");
        //    lstCelulaProc = (ListBox)FindControl("tablstCelulaProc");
        //    lstFerramentaProc = (ListBox)FindControl("tablstFerramentaProc");
        //    lstProdutoProc = (ListBox)FindControl("tablstProdutoProc");
        //    imbAddSetor = (ImageButton)FindControl("tabimbAddSetor");
        //    imbRemoveSetor = (ImageButton)FindControl("tabimbRemoveSetor");
        //    imbAddEquipamento = (ImageButton)FindControl("tabimbAddEquipamento");
        //    imbRemoveEquipamento = (ImageButton)FindControl("tabimbRemoveEquipamento");
        //    imbAddCelula = (ImageButton)FindControl("tabimbAddCelula");
        //    imbRemoveCelula = (ImageButton)FindControl("tabimbRemoveCelula");
        //    imbAddFerramenta = (ImageButton)FindControl("tabimbAddFerramenta");
        //    imbRemoveFerramenta = (ImageButton)FindControl("tabimbRemoveFerramenta");
        //    imbAddProduto = (ImageButton)FindControl("tabimbAddProduto");
        //    imbRemoveProduto = (ImageButton)FindControl("tabimbRemoveProduto");
        //    lblEquipamento = (Label)FindControl("tablblEquipamento");
        //    lblCelula = (Label)FindControl("tablblCelula");
        //    lblFerramenta = (Label)FindControl("tablblFerramenta");
        //    lblProduto = (Label)FindControl("tablblProduto");
	
        //    //Medidas de Controle            						
        //    lsbEPI = (ListBox)FindControl("tablsbEPI");
        //    imbDown = (ImageButton)FindControl("tabimbDown");
        //    imbUp = (ImageButton)FindControl("tabimbUp");
        //    lstBoxEPIs = (ListBox)FindControl("tablstBoxEPIs");
        //    txtMedidasAdm = (TextBox)FindControl("tabtxtMedidasAdm"); 
        //    txtMedidasEdu = (TextBox)FindControl("tabtxtMedidasEdu"); 
        //    txtEPC = (TextBox)FindControl("tabtxtEPC");
        //    ddlLaudos = (DropDownList)FindControl("tabddlLaudos");
        //    ddlGHE = (DropDownList)FindControl("tabddlGHE");
        //    lstBoxEPIGHE = (ListBox)FindControl("tablstBoxEPIGHE");
        //    txtMedidasAdmGHE = (TextBox)FindControl("tabtxtMedidasAdmGHE");
        //    txtEPCGHE = (TextBox)FindControl("tabtxtEPCGHE");
        //    txtMedidasEduGHE = (TextBox)FindControl("tabtxtMedidasEduGHE");
        //    btnTransfMedidasControle = (Button)FindControl("tabbtnTransfMedidasControle");
        //    lblEPI = (Label)FindControl("tablblEPI");
        //    lblEPC = (Label)FindControl("tablblEPC");
        //    lblMedidasAdm = (Label)FindControl("tablblMedidasAdm");
        //    lblMedidasEdu = (Label)FindControl("tablblMedidasEdu");

        //    //Operacoes			
        //    lstBoxOperacoes = (ListBox)FindControl("tablstBoxOperacoes");
        //    imbUpOperacao = (ImageButton)FindControl("tabimbUpOperacao");
        //    imbDownOperacao = (ImageButton)FindControl("tabimbDownOperacao");
        //    btnExcluirOpe = (Button)FindControl("tabbtnExcluirOpe");
        //    txtOperacao = (TextBox)FindControl("tabtxtOperacao");
        //    txtObsQualidade = (TextBox)FindControl("tabtxtObsQualidade");
        //    btnGravarOperacao = (Button)FindControl("tabbtnGravarOperacao");
        //    lsbRiscos = (ListBox)FindControl("tablsbRiscos");
        //    listBxRiscosOperacao = (ListBox)FindControl("tablistBxRiscosOperacao");
        //    btnGravarRisco = (Button)FindControl("tabbtnGravarRisco");
        //    lblOperacao = (Label)FindControl("tablblOperacao");

        //    //Atualizacoes
        //    lstBoxAtualizacoes = (ListBox)FindControl("tablstBoxAtualizacoes");
        //    wdtDataAtualizacao = (TextBox)FindControl("tabwdtDataAtualizacao");
        //    txtDescricaoAtualizacao = (TextBox)FindControl("tabtxtDescricaoAtualizacao");
        //    ddlResponsavelAtualizacao = (DropDownList)FindControl("tabddlResponsavelAtualizacao");
        //    ddlOperadorAtualizacao = (DropDownList)FindControl("tabddlOperadorAtualizacao");
        //    btnGravarAtualizacao = (Button)FindControl("tabbtnGravarAtualizacao");
        //    btnRemoverAtualizacao = (Button)FindControl("tabbtnRemoverAtualizacao");
        //    lblAtualizacoes = (Label)FindControl("tablblAtualizacoes");
        //}

		protected void InicializaEventosDosComponentes()
		{
			rblTipoProcedimento.SelectedIndexChanged += new EventHandler(rblTipoProcedimento_SelectedIndexChanged);
			ddlProcedimentoResumo.SelectedIndexChanged += new EventHandler(ddlProcedimentoResumo_SelectedIndexChanged);
			lkbRemoverImagem.Click += new EventHandler(lkbRemoverImagem_Click);
			imbAddSetor.Click += new ImageClickEventHandler(imbAddSetor_Click);
			imbRemoveSetor.Click += new ImageClickEventHandler(imbRemoveSetor_Click);
			imbAddEquipamento.Click += new ImageClickEventHandler(imbAddEquipamento_Click);
			imbRemoveEquipamento.Click += new ImageClickEventHandler(imbRemoveEquipamento_Click);
			imbAddCelula.Click += new ImageClickEventHandler(imbAddCelula_Click);
			imbRemoveCelula.Click += new ImageClickEventHandler(imbRemoveCelula_Click);
			imbAddFerramenta.Click += new ImageClickEventHandler(imbAddFerramenta_Click);
			imbRemoveFerramenta.Click += new ImageClickEventHandler(imbRemoveFerramenta_Click);
			imbAddProduto.Click += new ImageClickEventHandler(imbAddProduto_Click);
			imbRemoveProduto.Click += new ImageClickEventHandler(imbRemoveProduto_Click);
			imbDown.Click += new ImageClickEventHandler(imbDown_Click);
			imbUp.Click += new ImageClickEventHandler(imbUp_Click);
			ddlGHE.SelectedIndexChanged += new EventHandler(ddlGHE_SelectedIndexChanged);
			ddlLaudos.SelectedIndexChanged += new EventHandler(ddlLaudos_SelectedIndexChanged);
			//btnTransfMedidasControle.Click += new EventHandler(btnTransfMedidasControle_Click1);
			btnGravarOperacao.Click += new EventHandler(btnGravarOperacao_Click);
			lstBoxOperacoes.SelectedIndexChanged += new EventHandler(lstBoxOperacoes_SelectedIndexChanged);
			listBxRiscosOperacao.SelectedIndexChanged += new EventHandler(listBxRiscosOperacao_SelectedIndexChanged);
			//btnGravarRisco.Click += new EventHandler(btnGravarRisco_Click1);
			imbUpOperacao.Click += new ImageClickEventHandler(imbUpOperacao_Click);
			imbDownOperacao.Click += new ImageClickEventHandler(imbDownOperacao_Click);
			//btnExcluirOpe.Click += new EventHandler(btnExcluirOpe_Click);
			lstBoxAtualizacoes.SelectedIndexChanged += new EventHandler(lstBoxAtualizacoes_SelectedIndexChanged);
			//btnGravarAtualizacao.Click += new EventHandler(btnGravarAtualizacao_Click1);
			//btnRemoverAtualizacao.Click += new EventHandler(btnRemoverAtualizacao_Click1);
		}
		#endregion

		#region Web Form Designer generated code
        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            //InicializaComponentes();
            InicializaEventosDosComponentes();
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

		protected  void InicializaWebPageObjects()
		{
            //base.InicializaWebPageObjects();

            //if (procedimento == null || procedimento.Id==0)
            //{
            if ( lbl_Id_Procedimento.Text!="0" )
            {
                procedimento = new Procedimento(Convert.ToInt32(lbl_Id_Procedimento.Text));
            }
            else
            { 
                if (Request.QueryString["IdProcedimento"] == null || Request.QueryString["IdProcedimento"] == "")
                {
                    if (ViewState["IdProcedimento"] == null || ViewState["IdProcedimento"].ToString() == "")
                    {
                        procedimento = new Procedimento();
                        procedimento.Inicialize();
                        procedimento.IdCliente = cliente;
                        lbl_Id_Procedimento.Text = "0";
                    }
                    else
                    {
                        procedimento = new Procedimento(Convert.ToInt32(ViewState["IdProcedimento"]));
                        lbl_Id_Procedimento.Text = ViewState["IdProcedimento"].ToString();
                    }


                }
                else
                {
                    procedimento = new Procedimento(Convert.ToInt32(Request.QueryString["IdProcedimento"]));
                    lbl_Id_Procedimento.Text = Request.QueryString["IdProcedimento"].ToString();
                }
            }

            if (searchFoto.HasFile && searchFoto.PostedFile.ContentLength > 0)
            {

                string xExtension = searchFoto.FileName.Substring(searchFoto.FileName.Length - 3, 3).ToUpper().Trim();

                if (xExtension == "PDF" || xExtension == "JPG")
                {
                    Session["zFoto"] = searchFoto.FileBytes;
                    txt_Arq.Text = searchFoto.FileName.ToString();
                }

                //ViewState["myData"] = searchFoto.FileBytes;
                //ViewState["Filename"] = searchFoto.PostedFile.FileName;

                //txtSelectedFile.Text = ViewState["Filename"].ToString();
            }
		}
		
		private void RegisterClientCode(Procedimento procedimento)
		{
			if (procedimento.Id != 0)
			{
                lkbRemoverImagem.Enabled = true;
                lkbRemoverImagem.Attributes.Add("onClick", "javascript:return confirm('Você deseja realmente remover a foto do procedimento nPOPS " + procedimento.Numero.ToString("0000") + "?');");
				btnTransfMedidasControle.Attributes.Add("onClick", "javascript:return confirm('Você deseja realmente transferir os dados do GHE selecionado para o Procedimento nPOPS " + procedimento.Numero.ToString("0000") + "? Lembre-se que os dados atuais serão substituídos!');");
			}
			//else
				btnTransfMedidasControle.Attributes.Add("onClick", "javascript:return confirm('Você deseja realmente transferir os dados do GHE selecionado para este novo Procedimento?');");

			btnExcluirOpe.Attributes.Add("onClick","javascript:return confirm('Você deseja realmente excluir a operação selecionada?');");
            btnExcluirRisco.Attributes.Add("onClick", "javascript:return confirm('Você deseja realmente excluir o Risco selecionado?');");
            btnExcluirImpacto.Attributes.Add("onClick", "javascript:return confirm('Você deseja realmente excluir o Impacto Ambiental selecionado?');");
			btnRemoverAtualizacao.Attributes.Add("onClick", "javascript:return confirm('Você deseja realmente excluir a Atualização selecionada?');");
			btnExcluir.Attributes.Add("onClick" ,"javascript:return confirm('Você deseja realmente excluir este Procedimento?');");
            btnDuplicarProc.Attributes.Add("onClick", "javascript:return confirm('Você deseja realmente duplicar este Procedimento nPOPs " + procedimento.Numero.ToString("0000") + "?');");
		}

		private void PopulaDdls()
		{
			PopulaDdlProcedimentoResumo();
			PopulaLsbSetor();
			PopulaLsbFerramenta();
			PopulaLsbEquipamento();
			PopulaLsbProduto();
			PopulaLsbCelula();
			PopulaDDLLevantamentos();
			PopulaDDLGhe();
			PopulaLsbEpi();
			PopulaDDLResponsaveisAtualizacao();
		}

        private void PopulaLsbPerigos(int IdOperacao)
        {
            lsbPerigos.DataSource = new Perigo().GetIdNome("Nome", "IdCliente=" + lbl_Id_Empresa.Text
                + " AND IdPerigo NOT IN (SELECT IdPerigo FROM OperacaoPerigo WHERE IdOperacao=" + IdOperacao + ")");
            lsbPerigos.DataValueField = "Id";
            lsbPerigos.DataTextField = "Nome";
            lsbPerigos.DataBind();
        }

        private void PopulaLsbAspectos(int IdOperacao)
        {
            lsbAspectos.DataSource = new AspectoAmbiental().GetIdNome("Nome", "IdCliente=" + lbl_Id_Empresa.Text
                + " AND IdAspectoAmbiental NOT IN (SELECT IdAspectoAmbiental FROM OperacaoAspectoAmbiental WHERE IdOperacao=" + IdOperacao + ")");
            lsbAspectos.DataValueField = "Id";
            lsbAspectos.DataTextField = "Nome";
            lsbAspectos.DataBind();
        }

        private void PopulaLsbDDLPerigosProc(int IdOperacao)
        {
            DataSet dsPerigo = new Perigo().GetIdNome("Nome", "IdPerigo IN (SELECT IdPerigo FROM OperacaoPerigo WHERE IdOperacao=" + IdOperacao + ")");

            lsbPerigoSelected.DataSource = dsPerigo;
            lsbPerigoSelected.DataValueField = "Id";
            lsbPerigoSelected.DataTextField = "Nome";
            lsbPerigoSelected.DataBind();

            ddlPerigo.DataSource = dsPerigo;
            ddlPerigo.DataValueField = "Id";
            ddlPerigo.DataTextField = "Nome";
            ddlPerigo.DataBind();

            PopulaLsbRiscosPerigo(dsPerigo.Tables[0].Rows.Count);
        }

        private void PopulaLsbDDLAspectosProc(int IdOperacao)
        {
            DataSet dsAspecto = new AspectoAmbiental().GetIdNome("Nome", "IdAspectoAmbiental IN (SELECT IdAspectoAmbiental FROM OperacaoAspectoAmbiental WHERE IdOperacao=" + IdOperacao + ")");

            lsbAspectoSelected.DataSource = dsAspecto;
            lsbAspectoSelected.DataValueField = "Id";
            lsbAspectoSelected.DataTextField = "Nome";
            lsbAspectoSelected.DataBind();

            ddlAspecto.DataSource = dsAspecto;
            ddlAspecto.DataValueField = "Id";
            ddlAspecto.DataTextField = "Nome";
            ddlAspecto.DataBind();

            PopulaLsbImpactoAspecto();
        }

        private void PopulaLsbRiscosPerigo(int numPerigo)
        {
            listBxRiscosOperacao.Items.Clear();
            rblSeveridade.ClearSelection();
            rblProbabilidade.ClearSelection();

            if (numPerigo.Equals(0))
            {
                ArrayList alOperacaoRisco = new OperacaoRiscoAcidente().Find("IdOperacao=" + lstBoxOperacoes.SelectedValue
                    + " ORDER BY (SELECT Nome FROM RiscoAcidente WHERE RiscoAcidente.IdRiscoAcidente=OperacaoRiscoAcidente.IdRiscoAcidente)");

                foreach (OperacaoRiscoAcidente risco in alOperacaoRisco)
                {
                    risco.IdRiscoAcidente.Find();
                    listBxRiscosOperacao.Items.Add(new ListItem(risco.IdRiscoAcidente.Nome, risco.Id.ToString()));
                }
            }
            else
            {
                ArrayList alOperacaoPerigoRisco = new OperacaoPerigoRiscoAcidente().Find("IdOperacaoPerigo IN (SELECT IdOperacaoPerigo FROM OperacaoPerigo"
                    + " WHERE IdOperacao=" + lstBoxOperacoes.SelectedValue + " AND IdPerigo=" + ddlPerigo.SelectedValue + ")"
                    + " ORDER BY (SELECT Nome FROM RiscoAcidente WHERE RiscoAcidente.IdRiscoAcidente=OperacaoPerigoRiscoAcidente.IdRiscoAcidente)");

                foreach (OperacaoPerigoRiscoAcidente risco in alOperacaoPerigoRisco)
                {
                    risco.IdRiscoAcidente.Find();
                    listBxRiscosOperacao.Items.Add(new ListItem(risco.IdRiscoAcidente.Nome, risco.Id.ToString()));
                }
            }

            btnGravarRisco.Enabled = false;
            btnExcluirRisco.Enabled = false;
        }

        private void PopulaLsbImpactoAspecto()
        {
            lsbImpactosSelected.Items.Clear();
            rblSeveridadeImpacto.ClearSelection();
            rblProbabilidadeImpacto.ClearSelection();

            if (ddlAspecto.Items.Count > 0)
            {
                ArrayList alOperacaoAspectoImpacto = new OperacaoAspectoAmbientalImpacto().Find("IdOperacaoAspectoAmbiental IN (SELECT IdOperacaoAspectoAmbiental FROM OperacaoAspectoAmbiental"
                    + " WHERE IdOperacao=" + lstBoxOperacoes.SelectedValue + " AND IdAspectoAmbiental=" + ddlAspecto.SelectedValue + ")"
                    + " ORDER BY (SELECT Nome FROM ImpactoAmbiental WHERE ImpactoAmbiental.IdImpactoAmbiental=OperacaoAspectoAmbientalImpacto.IdImpactoAmbiental)");

                foreach (OperacaoAspectoAmbientalImpacto impacto in alOperacaoAspectoImpacto)
                {
                    impacto.IdImpactoAmbiental.Find();
                    lsbImpactosSelected.Items.Add(new ListItem(impacto.IdImpactoAmbiental.Nome, impacto.Id.ToString()));
                }
            }

            btnGravarDetalhesImpacto.Enabled = false;
            btnExcluirImpacto.Enabled = false;
        }

		private void PopulaDDLResponsaveisAtualizacao()
		{
            Cliente xCliente;
            xCliente = new Cliente( System.Convert.ToInt32( lbl_Id_Empresa.Text));

			ArrayList alPrestadores = new Prestador().GetListaPrestador(xCliente, false, (int)TipoPrestador.ContatoEmpresa);
			alPrestadores.Sort();

			ddlResponsavelAtualizacao.DataSource = alPrestadores;
			ddlResponsavelAtualizacao.DataValueField = "Id";
			ddlResponsavelAtualizacao.DataTextField = "NomeCompleto";
			ddlResponsavelAtualizacao.DataBind();

			ddlOperadorAtualizacao.DataSource = alPrestadores;
			ddlOperadorAtualizacao.DataValueField = "Id";
			ddlOperadorAtualizacao.DataTextField = "NomeCompleto";
			ddlOperadorAtualizacao.DataBind();

			ddlResponsavelAtualizacao.Items.Insert(0, new ListItem("Selecione o Responsável...", "0"));
			ddlOperadorAtualizacao.Items.Insert(0, new ListItem("Selecione o Operador...", "0"));
		}

		private void PopulaLsbRiscoOperacao(string IdOperacao)
		{
			listBxRiscosOperacao.Items.Clear();
			
			ArrayList alOperacaoRiscoAcidente = new OperacaoRiscoAcidente().Find("IdOperacao=" + IdOperacao
				+" ORDER BY (SELECT Nome FROM RiscoAcidente WHERE OperacaoRiscoAcidente.IdRiscoAcidente = RiscoAcidente.IdRiscoAcidente)");

			foreach (OperacaoRiscoAcidente operacaoRiscoAcidente in alOperacaoRiscoAcidente)
			{
				operacaoRiscoAcidente.IdRiscoAcidente.Find();
				listBxRiscosOperacao.Items.Add(new ListItem(operacaoRiscoAcidente.IdRiscoAcidente.Nome, operacaoRiscoAcidente.Id.ToString()));
			}
		}

		private void PopulaDDLLevantamentos()
		{
			DataSet ds = new LaudoTecnico().GetLaudosTecnicos(lbl_Id_Empresa.Text);

			ddlLaudos.DataSource = ds;
			ddlLaudos.DataValueField = "IdLaudoTecnico";
			ddlLaudos.DataTextField = "DataLaudo";
			ddlLaudos.DataBind();
			
			ddlLaudos.Items.Insert(0, new ListItem("Selecione...", "0"));
			
			if (ds.Tables[0].Rows.Count > 0)
				ddlLaudos.Items[1].Selected = true;
		}

		private void PopulaDDLGhe()
		{
			ddlGHE.DataSource = new Ghe().Get("nID_LAUD_TEC=" + ddlLaudos.SelectedValue + " ORDER BY tNO_FUNC");
			ddlGHE.DataValueField = "nID_FUNC";
			ddlGHE.DataTextField = "tNO_FUNC";
			ddlGHE.DataBind();

			ddlGHE.Items.Insert(0, new ListItem("Selecione um GHE...", "0"));
		}

		public void PopulaLsbSetor()
		{
            lstSetor.Items.Clear();
			lstSetor.DataSource = new Setor().Get("nID_EMPR=" + lbl_Id_Empresa.Text
                + " AND nID_SETOR NOT IN (SELECT IdSetor FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.ProcedimentoSetor WHERE IdProcedimento=" + procedimento.Id + ")"
				+" ORDER BY tNO_STR_EMPR");
			lstSetor.DataValueField = "nID_SETOR";
			lstSetor.DataTextField = "tNO_STR_EMPR";
			lstSetor.DataBind();
		}

		public void PopulaLsbFerramenta()
		{
            lstFerramenta.Items.Clear();
			lstFerramenta.DataSource = new Ferramenta().Get("IdCliente=" + lbl_Id_Empresa.Text
				+" AND IdFerramenta NOT IN (SELECT IdFerramenta FROM ProcedimentoFerramenta WHERE IdProcedimento=" + procedimento.Id + ")"
				+" ORDER BY Nome");
			lstFerramenta.DataValueField = "IdFerramenta";
			lstFerramenta.DataTextField = "Nome";
			lstFerramenta.DataBind();
		}

		public void PopulaLsbEquipamento()
		{
            lstEquipamento.Items.Clear();
			lstEquipamento.DataSource = new Equipamento().Get("IdCliente=" + lbl_Id_Empresa.Text
				+" AND IdEquipamento NOT IN (SELECT IdEquipamento FROM ProcedimentoEquipamento WHERE IdProcedimento=" + procedimento.Id + ")"
				+" ORDER BY Nome");
			lstEquipamento.DataValueField = "IdEquipamento";
			lstEquipamento.DataTextField = "Nome";
			lstEquipamento.DataBind();
		}

		public void PopulaLsbProduto()
		{
            lstProduto.Items.Clear();
			lstProduto.DataSource = new Produto().Get("IdCliente=" + lbl_Id_Empresa.Text
				+" AND IdProduto NOT IN (SELECT IdProduto FROM ProcedimentoProduto WHERE IdProcedimento=" + procedimento.Id + ")"
				+" ORDER BY Nome");
			lstProduto.DataValueField = "IdProduto";
			lstProduto.DataTextField = "Nome";
			lstProduto.DataBind();
		}

		public void PopulaLsbCelula()
		{
            lstCelula.Items.Clear();
			lstCelula.DataSource = new Celula().Get("IdCliente=" + lbl_Id_Empresa.Text
				+" AND IdCelula NOT IN (SELECT IdCelula FROM ProcedimentoCelula WHERE IdProcedimento=" + procedimento.Id + ")"
				+" ORDER BY Nome");
			lstCelula.DataValueField = "IdCelula";
			lstCelula.DataTextField = "Nome";
			lstCelula.DataBind();
		}

		public void PopulaLsbSetorProc()
		{
			lstSetorProc.Items.Clear();
			
			ArrayList alProcedimentoSetor = new ProcedimentoSetor().Find("IdProcedimento=" + procedimento.Id
                + " ORDER BY (SELECT tNO_STR_EMPR FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblSETOR WHERE " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblSETOR.nID_SETOR =  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.ProcedimentoSetor.IdSetor)");
			
			foreach (ProcedimentoSetor procedimentoSetor in alProcedimentoSetor)
			{
				procedimentoSetor.IdSetor.Find();
				lstSetorProc.Items.Add(new ListItem(procedimentoSetor.IdSetor.tNO_STR_EMPR, procedimentoSetor.Id.ToString()));
			}
		}

		public void PopulaLsbFerramentaProc()
		{
			lstFerramentaProc.Items.Clear();

			ArrayList alProcedimentoFerramenta = new ProcedimentoFerramenta().Find("IdProcedimento=" + procedimento.Id
				+" ORDER BY (SELECT Nome FROM Ferramenta WHERE Ferramenta.IdFerramenta = ProcedimentoFerramenta.IdFerramenta)");
			
			foreach (ProcedimentoFerramenta procedimentoFerramenta in alProcedimentoFerramenta)
			{
				procedimentoFerramenta.IdFerramenta.Find();
				lstFerramentaProc.Items.Add(new ListItem(procedimentoFerramenta.IdFerramenta.Nome, procedimentoFerramenta.Id.ToString()));
			}
		}

		public void PopulaLsbEquipamentoProc()
		{
			lstEquipamentoProc.Items.Clear();

			ArrayList alProcedimentoEquipamento = new ProcedimentoEquipamento().Find("IdProcedimento=" + procedimento.Id
				+" ORDER BY (SELECT Nome FROM Equipamento WHERE Equipamento.IdEquipamento = ProcedimentoEquipamento.IdEquipamento)");

			foreach (ProcedimentoEquipamento procedimentoEquipamento in  alProcedimentoEquipamento)
			{
				procedimentoEquipamento.IdEquipamento.Find();
				lstEquipamentoProc.Items.Add(new ListItem(procedimentoEquipamento.IdEquipamento.Nome, procedimentoEquipamento.Id.ToString()));
			}
		}

		public void PopulaLsbProdutoProc()
		{
			lstProdutoProc.Items.Clear();

			ArrayList alProcedimentoProduto = new ProcedimentoProduto().Find("IdProcedimento=" + procedimento.Id
				+" ORDER BY (SELECT Nome FROM Produto WHERE Produto.IdProduto = ProcedimentoProduto.IdProduto)");

			foreach (ProcedimentoProduto procedimentoProduto in  alProcedimentoProduto)
			{
				procedimentoProduto.IdProduto.Find();
				lstProdutoProc.Items.Add(new ListItem(procedimentoProduto.IdProduto.Nome, procedimentoProduto.Id.ToString()));
			}
		}

		public void PopulaLsbCelulaProc()
		{
			lstCelulaProc.Items.Clear();

			ArrayList alProcedimentoCelula = new ProcedimentoCelula().Find("IdProcedimento=" + procedimento.Id
				+" ORDER BY (SELECT Nome FROM Celula WHERE Celula.IdCelula = ProcedimentoCelula.IdCelula)");

			foreach (ProcedimentoCelula procedimentoCelula in  alProcedimentoCelula)
			{
				procedimentoCelula.IdCelula.Find();
				lstCelulaProc.Items.Add(new ListItem(procedimentoCelula.IdCelula.Nome, procedimentoCelula.Id.ToString()));
			}
		}

		private void PopulaDdlProcedimentoResumo()
		{
			DataSet ds = new Procedimento().Get("IdCliente=" + lbl_Id_Empresa.Text
				+" AND IndTipoProcedimento=" + (int)TipoProcedimento.Resumo + " ORDER BY Numero");
			
			DataSet dsProc = new DataSet();
			DataRow rowProc;
			DataTable table = new DataTable();
			table.Columns.Add("IdProcedimento", typeof(string));
			table.Columns.Add("Nome", typeof(string));
			dsProc.Tables.Add(table);

			foreach (DataRow row in ds.Tables[0].Select())
			{
				rowProc = dsProc.Tables[0].NewRow();
				rowProc["IdProcedimento"] = row["IdProcedimento"];
				rowProc["Nome"] = row["Numero"] + "-" + row["Nome"];
				dsProc.Tables[0].Rows.Add(rowProc);
			}

            ddlProcedimentoResumo.Items.Clear();
			ddlProcedimentoResumo.DataSource = dsProc;
			ddlProcedimentoResumo.DataTextField = "Nome";
			ddlProcedimentoResumo.DataValueField = "IdProcedimento";
			ddlProcedimentoResumo.DataBind();
			ddlProcedimentoResumo.Items.Insert(0, new ListItem("Selecione um Procedimento Resumo...","0"));
		}

		private void rblTipoProcedimento_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			HabilitaAcaoProcedimento();
		}

		private void HabilitaAcaoProcedimento()
		{
            switch (Convert.ToInt32(rblTipoProcedimento.SelectedValue))
            {
                case (int)TipoProcedimento.Especifico:
                    SetWebFormsToEspecifico();
                    break;
                case (int)TipoProcedimento.Resumo:
                    SetWebFormsToResumo();
                    break;
                case (int)TipoProcedimento.Instrutivo:
                    SetWebFormsToInstrutivo();
                    break;
            }
		}

		private void PopulaControlesProcedimento(Procedimento procedimento)
		{
			if (procedimento.mirrorOld == null)
                procedimento.Find();

            txtEPC.Text = procedimento.Epc;
            txtMedidasAdm.Text = procedimento.MedidasAdm;
            txtMedidasEdu.Text = procedimento.MedidasEdu;

            rblIncidencia.ClearSelection();
            if (!procedimento.IndTipoIncidencia.Equals(0))
                rblIncidencia.Items.FindByValue(procedimento.IndTipoIncidencia.ToString()).Selected = true;
            rblSituacao.ClearSelection();
            if (!procedimento.IndTipoSituacao.Equals(0))
                rblSituacao.Items.FindByValue(procedimento.IndTipoSituacao.ToString()).Selected = true;

            PopulaListBoxEpi(procedimento);
            PopulaListBoxOperacoes(procedimento);
            PopulaListBoxAtualizacoes(procedimento);
		}


        private string FotoProcedimento()
        {
            if ( procedimento.Foto_Procedimento.ToString().Trim()=="")
                return "img/default_img.gif";

            Ilitera.Opsa.Data.Cliente xCliente = new Ilitera.Opsa.Data.Cliente();
            xCliente.Find(System.Convert.ToInt32(Session["Empresa"].ToString()));

            string xArq = "";
                        
           // if ( Session["Servidor_Web"].ToString().Trim().ToUpper()=="ILITERA")
               xArq = "I:\\fotosdocsdigitais\\" + xCliente.DiretorioPadrao.ToString().ToUpper().Trim() + "\\OrdemServico\\" + procedimento.Foto_Procedimento;
           // else
           //    xArq = "C:\\DRIVE_I\\fotosdocsdigitais\\" + xCliente.DiretorioPadrao.ToString().ToUpper().Trim() + "\\OrdemServico\\" + procedimento.Foto_Procedimento;

            return Ilitera.Common.Fotos.PathFoto_Uri(xArq);

        }



		private void PopulaTela()
		{
			//if (!procedimento.FotoProcedimentoUrl().Equals(string.Empty))
            if (procedimento.Foto_Procedimento != null)
				imgFoto.ImageUrl = FotoProcedimento(); 
			else
				imgFoto.ImageUrl = "img/default_img.gif";
			
			txtnPOPs.Text = procedimento.Numero.ToString("0000");
			rblTipoProcedimento.ClearSelection();
			rblTipoProcedimento.Items.FindByValue(((int)procedimento.IndTipoProcedimento).ToString()).Selected = true;
			txtNome.Text = procedimento.Nome;
			txtDescricao.Text = procedimento.Descricao;
			

			ddlProcedimentoResumo.ClearSelection();

            if (procedimento.IndTipoProcedimento != Ilitera.Opsa.Data.TipoProcedimento.Especifico)
            {
                ddlProcedimentoResumo.Items.FindByValue(procedimento.IdProcedimentoResumo.Id.ToString()).Selected = true;
            }
			PopulaLsbSetorProc();
			PopulaLsbEquipamentoProc();
			PopulaLsbCelulaProc();
			PopulaLsbFerramentaProc();
			PopulaLsbProdutoProc();
			
			lblTipoProcedimento.ForeColor = foreColorDisabledLabel;
			rblTipoProcedimento.Enabled = false;

            switch (procedimento.IndTipoProcedimento)
            {
                case TipoProcedimento.Especifico:
                    SetWebFormsToEspecifico();
                    PopulaControlesProcedimento(procedimento.IdProcedimentoResumo);

                    lstBoxOperacoes.ClearSelection();
                    lstBoxAtualizacoes.ClearSelection();
                    break;
                case TipoProcedimento.Resumo:
                    PopulaControlesProcedimento(procedimento);

                    //Dados Básicos
                    lblProcedimentoResumo.ForeColor = foreColorDisabledLabel;
                    ddlProcedimentoResumo.Enabled = false;
                    break;
                case TipoProcedimento.Instrutivo:
                    SetWebFormsToInstrutivo();
                    PopulaControlesProcedimento(procedimento);
                    break;
            }
		}

        private void SetWebFormsToResumo()
        {
            //Dados Básicos
            lblProcedimentoResumo.ForeColor = foreColorDisabledLabel;
            ddlProcedimentoResumo.ClearSelection();
            ddlProcedimentoResumo.Enabled = false;
            lblIncidencia.ForeColor = foreColorEnabledLabel;
            rblIncidencia.Enabled = true;
            rblIncidencia.ClearSelection();
            lblSituacao.ForeColor = foreColorEnabledLabel;
            rblSituacao.Enabled = true;
            rblSituacao.ClearSelection();

            //Dados Complementares
            lblEquipamento.ForeColor = foreColorEnabledLabel;
            lstEquipamento.Enabled = true;
            lstEquipamento.BackColor = backColorEnabledBox;
            lstEquipamentoProc.Enabled = true;
            lstEquipamentoProc.BackColor = backColorEnabledBoxYellow;
            imbAddEquipamento.Enabled = true;
            imbAddEquipamento.ImageUrl = "img/right.jpg";
            imbRemoveEquipamento.Enabled = true;
            imbRemoveEquipamento.ImageUrl = "img/left.jpg";

            lblFerramenta.ForeColor = foreColorEnabledLabel;
            lstFerramenta.Enabled = true;
            lstFerramenta.BackColor = backColorEnabledBox;
            lstFerramentaProc.Enabled = true;
            lstFerramentaProc.BackColor = backColorEnabledBoxYellow;
            imbAddFerramenta.Enabled = true;
            imbAddFerramenta.ImageUrl = "img/right.jpg";
            imbRemoveFerramenta.Enabled = true;
            imbRemoveFerramenta.ImageUrl = "img/left.jpg";

            lblProduto.ForeColor = foreColorEnabledLabel;
            lstProduto.Enabled = true;
            lstProduto.BackColor = backColorEnabledBox;
            lstProdutoProc.Enabled = true;
            lstProdutoProc.BackColor = backColorEnabledBoxYellow;
            imbAddProduto.Enabled = true;
            imbAddProduto.ImageUrl = "img/right.jpg";
            imbRemoveProduto.Enabled = true;
            imbRemoveProduto.ImageUrl = "img/left.jpg";

            //Operações
            lblOperacao.ForeColor = foreColorEnabledLabel;
            lstBoxOperacoes.ClearSelection();
            lstBoxOperacoes.BackColor = backColorEnabledBox;
            while (lstBoxOperacoes.Items.Count > 1)
                lstBoxOperacoes.Items.RemoveAt(1);
            lstBoxOperacoes.Items[0].Selected = true;
            lstBoxOperacoes.AutoPostBack = true;
            imbUpOperacao.Enabled = true;
            imbUpOperacao.ImageUrl = "../InfragisticsImg/XpOlive/Up_down.gif";
            imbDownOperacao.Enabled = true;
            imbDownOperacao.ImageUrl = "../InfragisticsImg/XpOlive/Down_down.gif";
            lblOperacaoDescricao.ForeColor = foreColorEnabledLabel;
            txtOperacao.BorderColor = borderColorEnabledBox;
            txtOperacao.BackColor = backColorEnabledBox;
            txtOperacao.Enabled = true;
            lblObsQualidade.ForeColor = foreColorEnabledLabel;
            txtObsQualidade.BorderColor = borderColorEnabledBox;
            txtObsQualidade.BackColor = backColorEnabledBox;            
            txtObsQualidade.Enabled = true;

            txtPrecaucoes.BorderColor = borderColorEnabledBox;
            txtPrecaucoes.BackColor = backColorEnabledBox;
            txtPrecaucoes.Enabled = true;

            btnGravarOperacao.Enabled = true;
            
            listBxRiscosOperacao.BackColor = backColorEnabledBoxYellow;
            lsbImpactosSelected.BackColor = backColorEnabledBoxYellow;

            //Medidas de Controle            
            lblEPI.ForeColor = foreColorEnabledLabel;
            lsbEPI.Enabled = true;
            lsbEPI.BackColor = backColorEnabledBox;
            PopulaLsbEpi();
            imbDown.Enabled = true;
            imbDown.ImageUrl = "../InfragisticsImg/XpOlive/Down_down.gif";
            imbUp.Enabled = true;
            imbUp.ImageUrl = "../InfragisticsImg/XpOlive/Up_down.gif";
            lstBoxEPIs.BackColor = backColorEnabledBoxYellow;
            lstBoxEPIs.Items.Clear();
            lblMedidasAdm.ForeColor = foreColorEnabledLabel;
            txtMedidasAdm.ReadOnly = false;
            txtMedidasAdm.BackColor = backColorEnabledBox;
            txtMedidasAdm.BorderColor = borderColorEnabledBox;
            txtMedidasAdm.Text = string.Empty;
            lblMedidasEdu.ForeColor = foreColorEnabledLabel;
            txtMedidasEdu.ReadOnly = false;
            txtMedidasEdu.BackColor = backColorEnabledBox;
            txtMedidasEdu.BorderColor = borderColorEnabledBox;
            txtMedidasEdu.Text = string.Empty;
            lblEPC.ForeColor = foreColorEnabledLabel;
            txtEPC.ReadOnly = false;
            txtEPC.BackColor = backColorEnabledBox;
            txtEPC.BorderColor = borderColorEnabledBox;
            txtEPC.Text = string.Empty;
            
            
            //Atualizações
            lblAtualizacoes.ForeColor = foreColorEnabledLabel;
            lstBoxAtualizacoes.AutoPostBack = true;
            lstBoxAtualizacoes.BackColor = backColorEnabledBox;
            while (lstBoxAtualizacoes.Items.Count > 1)
                lstBoxAtualizacoes.Items.RemoveAt(1);
            lstBoxAtualizacoes.Items[0].Selected = true;
            lblData.ForeColor = foreColorEnabledLabel;
            wdtDataAtualizacao.BorderColor = borderColorEnabledBox;
            wdtDataAtualizacao.BackColor = backColorEnabledBox;
            wdtDataAtualizacao.Enabled = true;
            lblDescricao.ForeColor = foreColorEnabledLabel;
            txtDescricaoAtualizacao.BorderColor = borderColorEnabledBox;
            txtDescricaoAtualizacao.BackColor = backColorEnabledBox;
            txtDescricaoAtualizacao.Enabled = true;
            btnGravarAtualizacao.Enabled = true;

            btnDuplicarProc.Enabled = true;
        }

        private void SetWebFormsToEspecifico()
        {
            //Dados Básicos
            lblProcedimentoResumo.ForeColor = foreColorEnabledLabel;
            ddlProcedimentoResumo.Enabled = true;
            lblIncidencia.ForeColor = foreColorDisabledLabel;
            rblIncidencia.ClearSelection();
            rblIncidencia.Enabled = false;
            lblSituacao.ForeColor = foreColorDisabledLabel;
            rblSituacao.ClearSelection();
            rblSituacao.Enabled = false;

            //Dados Complementares
            lblEquipamento.ForeColor = foreColorEnabledLabel;
            lstEquipamento.Enabled = true;
            lstEquipamento.BackColor = backColorEnabledBox;
            lstEquipamentoProc.Enabled = true;
            lstEquipamentoProc.BackColor = backColorEnabledBoxYellow;
            imbAddEquipamento.Enabled = true;
            imbAddEquipamento.ImageUrl = "img/right.jpg";
            imbRemoveEquipamento.Enabled = true;
            imbRemoveEquipamento.ImageUrl = "img/left.jpg";

            lblFerramenta.ForeColor = foreColorEnabledLabel;
            lstFerramenta.Enabled = true;
            lstFerramenta.BackColor = backColorEnabledBox;
            lstFerramentaProc.Enabled = true;
            lstFerramentaProc.BackColor = backColorEnabledBoxYellow;
            imbAddFerramenta.Enabled = true;
            imbAddFerramenta.ImageUrl = "img/right.jpg";
            imbRemoveFerramenta.Enabled = true;
            imbRemoveFerramenta.ImageUrl = "img/left.jpg";

            lblProduto.ForeColor = foreColorEnabledLabel;
            lstProduto.Enabled = true;
            lstProduto.BackColor = backColorEnabledBox;
            lstProdutoProc.Enabled = true;
            lstProdutoProc.BackColor = backColorEnabledBoxYellow;
            imbAddProduto.Enabled = true;
            imbAddProduto.ImageUrl = "img/right.jpg";
            imbRemoveProduto.Enabled = true;
            imbRemoveProduto.ImageUrl = "img/left.jpg";
            
            //Operações
            lblOperacao.ForeColor = foreColorDisabledLabel;
            lstBoxOperacoes.ClearSelection();
            lstBoxOperacoes.BackColor = backColorDisabledBox;
            lstBoxOperacoes.AutoPostBack = false;
            imbUpOperacao.ImageUrl = "InfragisticsImg/XpOlive/Up_downDisabled.gif";
            imbUpOperacao.Enabled = false;
            imbDownOperacao.ImageUrl = "InfragisticsImg/XpOlive/Down_downDisabled.gif";
            imbDownOperacao.Enabled = false;
            lblOperacaoDescricao.ForeColor = foreColorDisabledLabel;
            txtOperacao.BorderColor = borderColorDisabledBox;
            txtOperacao.BackColor = backColorDisabledBox;
            txtOperacao.Enabled = false;
            lblObsQualidade.ForeColor = foreColorDisabledLabel;

            txtObsQualidade.BorderColor = borderColorDisabledBox;
            txtObsQualidade.BackColor = backColorDisabledBox;
            txtObsQualidade.Enabled = false;

            txtPrecaucoes.BorderColor = borderColorDisabledBox;
            txtPrecaucoes.BackColor = backColorDisabledBox;
            txtPrecaucoes.Enabled = false;

            btnGravarOperacao.Enabled = false;
            listBxRiscosOperacao.BackColor = backColorDisabledBox;
            lsbImpactosSelected.BackColor = backColorDisabledBox;
            

            //Medidas de Controle
            
            lblEPI.ForeColor = foreColorDisabledLabel;
            lsbEPI.Items.Clear();
            lsbEPI.BackColor = backColorDisabledBox;
            lsbEPI.Enabled = false;
            imbDown.ImageUrl = "InfragisticsImg/XpOlive/Down_downDisabled.gif";
            imbDown.Enabled = false;
            imbUp.ImageUrl = "InfragisticsImg/XpOlive/Up_downDisabled.gif";
            imbUp.Enabled = false;
            lstBoxEPIs.BackColor = backColorDisabledBox;
            lblMedidasAdm.ForeColor = foreColorDisabledLabel;
            txtMedidasAdm.ReadOnly = true;
            txtMedidasAdm.BackColor = backColorDisabledBox;
            txtMedidasAdm.BorderColor = borderColorDisabledBox;
            lblMedidasEdu.ForeColor = foreColorDisabledLabel;
            txtMedidasEdu.ReadOnly = true;
            txtMedidasEdu.BackColor = backColorDisabledBox;
            txtMedidasEdu.BorderColor = borderColorDisabledBox;
            lblEPC.ForeColor = foreColorDisabledLabel;
            txtEPC.ReadOnly = true;
            txtEPC.BackColor = backColorDisabledBox;
            txtEPC.BorderColor = borderColorDisabledBox;
            

            //Atualizações
            lblAtualizacoes.ForeColor = foreColorDisabledLabel;
            lstBoxAtualizacoes.ClearSelection();
            lstBoxAtualizacoes.BackColor = backColorDisabledBox;
            lstBoxAtualizacoes.AutoPostBack = false;
            lblData.ForeColor = foreColorDisabledLabel;
            wdtDataAtualizacao.BorderColor = borderColorDisabledBox;
            wdtDataAtualizacao.BackColor = backColorDisabledBox;
            wdtDataAtualizacao.Enabled = false;
            lblDescricao.ForeColor = foreColorDisabledLabel;
            txtDescricaoAtualizacao.BorderColor = borderColorDisabledBox;
            txtDescricaoAtualizacao.BackColor = backColorDisabledBox;
            txtDescricaoAtualizacao.Enabled = false;
            btnGravarAtualizacao.Enabled = false;

            btnDuplicarProc.Enabled = false;
        }
        
        private void SetWebFormsToInstrutivo()
        {
            //Dados Básicos
            lblProcedimentoResumo.ForeColor = foreColorDisabledLabel;
            ddlProcedimentoResumo.ClearSelection();
            ddlProcedimentoResumo.Enabled = false;
            lblIncidencia.ForeColor = foreColorEnabledLabel;
            rblIncidencia.Enabled = true;
            rblIncidencia.ClearSelection();
            lblSituacao.ForeColor = foreColorEnabledLabel;
            rblSituacao.Enabled = true;
            rblSituacao.ClearSelection();

            //Dados Complementares
            lblEquipamento.ForeColor = foreColorDisabledLabel;
            lstEquipamento.BackColor = backColorDisabledBox;
            lstEquipamento.Enabled = false;
            lstEquipamentoProc.BackColor = backColorDisabledBox;
            lstEquipamentoProc.Enabled = false;
            imbAddEquipamento.ImageUrl = "img/rightDisabled.jpg";
            imbAddEquipamento.Enabled = false;
            imbRemoveEquipamento.ImageUrl = "img/leftDisabled.jpg";
            imbRemoveEquipamento.Enabled = false;

            lblFerramenta.ForeColor = foreColorDisabledLabel;
            lstFerramenta.BackColor = backColorDisabledBox;
            lstFerramenta.Enabled = false;
            lstFerramentaProc.BackColor = backColorDisabledBox;
            lstFerramentaProc.Enabled = false;
            imbAddFerramenta.ImageUrl = "img/rightDisabled.jpg";
            imbAddFerramenta.Enabled = false;
            imbRemoveFerramenta.ImageUrl = "img/leftDisabled.jpg";
            imbRemoveFerramenta.Enabled = false;

            lblProduto.ForeColor = foreColorDisabledLabel;
            lstProduto.BackColor = backColorDisabledBox;
            lstProduto.Enabled = false;
            lstProdutoProc.BackColor = backColorDisabledBox;
            lstProdutoProc.Enabled = false;
            imbAddProduto.ImageUrl = "img/rightDisabled.jpg";
            imbAddProduto.Enabled = false;
            imbRemoveProduto.ImageUrl = "img/leftDisabled.jpg";
            imbRemoveProduto.Enabled = false;

            //Operações
            lblOperacao.ForeColor = foreColorEnabledLabel;
            lstBoxOperacoes.ClearSelection();
            lstBoxOperacoes.BackColor = backColorEnabledBox;
            while (lstBoxOperacoes.Items.Count > 1)
                lstBoxOperacoes.Items.RemoveAt(1);
            if (lstBoxOperacoes.Items.Count.Equals(1))
                lstBoxOperacoes.Items[0].Selected = true;
            lstBoxOperacoes.AutoPostBack = true;
            imbUpOperacao.Enabled = true;
            imbUpOperacao.ImageUrl = "InfragisticsImg/XpOlive/Up_down.gif";
            imbDownOperacao.Enabled = true;
            imbDownOperacao.ImageUrl = "InfragisticsImg/XpOlive/Down_down.gif";
            lblOperacaoDescricao.ForeColor = foreColorEnabledLabel;
            txtOperacao.BorderColor = borderColorEnabledBox;
            txtOperacao.BackColor = backColorEnabledBox;
            txtOperacao.Enabled = true;
            lblObsQualidade.ForeColor = foreColorEnabledLabel;
            
            txtObsQualidade.BorderColor = borderColorEnabledBox;
            txtObsQualidade.BackColor = backColorEnabledBox;
            txtObsQualidade.Enabled = true;

            txtPrecaucoes.BorderColor = borderColorEnabledBox;
            txtPrecaucoes.BackColor = backColorEnabledBox;
            txtPrecaucoes.Enabled = true;

            btnGravarOperacao.Enabled = true;
            listBxRiscosOperacao.BackColor = backColorDisabledBox;
            lsbImpactosSelected.BackColor = backColorDisabledBox;
            


            //Atualizações
            lblAtualizacoes.ForeColor = foreColorEnabledLabel;
            lstBoxAtualizacoes.AutoPostBack = true;
            lstBoxAtualizacoes.BackColor = backColorEnabledBox;
            while (lstBoxAtualizacoes.Items.Count > 1)
                lstBoxAtualizacoes.Items.RemoveAt(1);
            if (lstBoxAtualizacoes.Items.Count.Equals(1))
                lstBoxAtualizacoes.Items[0].Selected = true;
            lblData.ForeColor = foreColorEnabledLabel;
            wdtDataAtualizacao.BorderColor = borderColorEnabledBox;
            wdtDataAtualizacao.BackColor = backColorEnabledBox;
            wdtDataAtualizacao.Enabled = true;
            lblDescricao.ForeColor = foreColorEnabledLabel;
            txtDescricaoAtualizacao.BorderColor = borderColorEnabledBox;
            txtDescricaoAtualizacao.BackColor = backColorEnabledBox;
            txtDescricaoAtualizacao.Enabled = true;
            btnGravarAtualizacao.Enabled = true;

            btnDuplicarProc.Enabled = true;
        }

		private void PopulaLsbEpi()
		{
			lsbEPI.DataSource = new Epi().Get("IdEpi NOT IN (SELECT IdEPI FROM ProcedimentoEPI WHERE IdProcedimento=" + procedimento.Id.ToString() + ") ORDER BY Descricao");
			lsbEPI.DataValueField = "IdEpi";
			lsbEPI.DataTextField  = "Descricao";
			lsbEPI.DataBind();
		}
		
		private void PopulaListBoxEpi(Procedimento procedimento)
		{
			lstBoxEPIs.Items.Clear();

			ArrayList alEPIProcedimento = new ProcedimentoEPI().Find("IdProcedimento=" + procedimento.Id.ToString()
				+" ORDER BY (SELECT Nome FROM Epi WHERE Epi.IdEpi=ProcedimentoEPI.IdEpi)");
			
			foreach (ProcedimentoEPI procedimentoEPI in alEPIProcedimento)
			{
				procedimentoEPI.IdEpi.Find();
				lstBoxEPIs.Items.Add(new ListItem(procedimentoEPI.IdEpi.ToString(), procedimentoEPI.Id.ToString()));
			}
		}	

		private void PopulaListBoxAtualizacoes(Procedimento procedimento)
		{
			lstBoxAtualizacoes.Items.Clear();
			
			DataSet dsAtualizacao = new AtualizacaoProcedimento().Get("IdProcedimento=" + procedimento.Id + " ORDER BY Data DESC");

			foreach (DataRow rowAtualizacao in dsAtualizacao.Tables[0].Select())
				lstBoxAtualizacoes.Items.Add(new ListItem(Convert.ToDateTime(rowAtualizacao["Data"]).ToString("dd-MM-yyyy") + " - " + rowAtualizacao["Descricao"], 
					rowAtualizacao["IdAtualizacaoProcedimento"].ToString()));

			lstBoxAtualizacoes.Items.Insert(0, new ListItem("Cadastrar Nova Atualização...", "0"));
			lstBoxAtualizacoes.Items[0].Selected = true;
		}
		
		private void PopulaListBoxOperacoes(Procedimento procedimento)
		{
			lstBoxOperacoes.Items.Clear();
			
			DataSet dsOperacoes = new Operacao().Get("IdProcedimento=" + procedimento.Id + " ORDER BY Sequencia");
			
			foreach(DataRow rowOperacao in dsOperacoes.Tables[0].Select())
				lstBoxOperacoes.Items.Add(new ListItem(((short)rowOperacao["Sequencia"]).ToString("00") + " - " + rowOperacao["Descricao"], 
					rowOperacao["IdOperacao"].ToString()));

			lstBoxOperacoes.Items.Insert(0, new ListItem("Cadastrar Nova Operação...", "0"));
			lstBoxOperacoes.Items[0].Selected = true;
		}

		private void PopulaProcedimento()
		{
            switch (Convert.ToInt32(rblTipoProcedimento.SelectedValue))
            {
                case (int)TipoProcedimento.Resumo:
                    procedimento.IndTipoProcedimento = TipoProcedimento.Resumo;
                    break;
                case (int)TipoProcedimento.Especifico:
                    procedimento.IndTipoProcedimento = TipoProcedimento.Especifico;
                    break;
                case (int)TipoProcedimento.Instrutivo:
                    procedimento.IndTipoProcedimento = TipoProcedimento.Instrutivo;
                    break;
            }

			procedimento.Nome = txtNome.Text.Trim();
			procedimento.Descricao = txtDescricao.Text.Trim();
			
			if ((Convert.ToInt32(rblTipoProcedimento.SelectedValue) == (int)TipoProcedimento.Especifico))
			{
				if (ddlProcedimentoResumo.SelectedValue.Equals("0"))
					throw new Exception("O Procedimento Resumo deve ser selecionado!");

				procedimento.IdProcedimentoResumo.Id = Convert.ToInt32(ddlProcedimentoResumo.SelectedValue);
			}
			else 
			{
				procedimento.Epc = txtEPC.Text.Trim();  
				procedimento.MedidasAdm = txtMedidasAdm.Text.Trim();  
				procedimento.MedidasEdu = txtMedidasEdu.Text.Trim();

                if (rblIncidencia.SelectedItem != null)
                    procedimento.IndTipoIncidencia = Convert.ToInt16(rblIncidencia.SelectedValue);
                if (rblSituacao.SelectedItem != null)
                    procedimento.IndTipoSituacao = Convert.ToInt16(rblSituacao.SelectedValue);
			}


            Cliente cliente = new Cliente();
            cliente.Find(System.Convert.ToInt32(Session["Empresa"].ToString()));
            procedimento.IdCliente = cliente;


            procedimento.UsuarioId = usuario.Id;
			procedimento.Save();

			saveProcedimentoProcess = true;
		}

		private void UploadFoto()
		{
            if (Session["zFoto"] != null && txt_Arq.Text != "")

            //if (ViewState["myData"] != null)
                //if (ViewState["Filename"].ToString().ToLower().IndexOf(".jpg") != -1 ||
                //    ViewState["Filename"].ToString().ToLower().IndexOf(".gif") != -1)
                {

                    ///////////////////
                    string xNomeArq = txt_Arq.Text;  // ViewState["Filename"].ToString();

                    Ilitera.Opsa.Data.Cliente xCliente = new Ilitera.Opsa.Data.Cliente();
                    xCliente.Find(System.Convert.ToInt32(Session["Empresa"].ToString()));

                    string xArq = "";
                    
                  //  if (Session["Servidor_Web"].ToString().Trim().ToUpper() == "ILITERA")
                       xArq = "I:\\fotosdocsdigitais\\" + xCliente.DiretorioPadrao.ToString().ToUpper().Trim() + "\\OrdemServico\\" + xNomeArq;
                   // else
                  //     xArq = "C:\\DRIVE_I\\fotosdocsdigitais\\" + xCliente.DiretorioPadrao.ToString().ToUpper().Trim() + "\\OrdemServico\\" + xNomeArq;


                    byte[] zFoto;


                    zFoto = (byte[])Session["zFoto"];

                    //se for o caso, diminuir tamanho da foto
                    System.IO.MemoryStream xStream = new System.IO.MemoryStream(zFoto);


                    System.Drawing.Bitmap workingBitmap;
                    workingBitmap = new System.Drawing.Bitmap(xStream);

                    if (workingBitmap.Size.Width > 500)
                    {
                        float zIndice = 2;
                        zIndice = workingBitmap.Size.Width / 500;

                        workingBitmap = BitmapManipulator.ResizeBitmap(workingBitmap,
                                                                (int)(workingBitmap.Size.Width / zIndice),
                                                                (int)(workingBitmap.Size.Height / zIndice));

                    }


                    workingBitmap.Save(xArq);



                    //using (System.Drawing.Image image = System.Drawing.Image.FromStream(new System.IO.MemoryStream(zFoto)))
                    //{
                    //    image.Save(xArq);  // Or Png
                    //}

                    xArq = "I:\\fotosdocsdigitais\\" + xCliente.DiretorioPadrao.ToString().ToUpper().Trim() + "\\Organogramas\\" + xNomeArq;
                    //txt_Arq.Text = xArq;


                    procedimento.Foto_Procedimento = xNomeArq;
                    procedimento.Save();
                    


                    ////////////////////

                    //byte[] fileFoto = (byte[])ViewState["myData"];


                    ////ordemservico

                    //StringBuilder checkDirectory = new StringBuilder();
                    //checkDirectory.Append(@"\\ClusterMestra\DocsDigitais$");
                    //checkDirectory.Append(procedimento.FotoDiretorio);

                    //System.IO.DirectoryInfo directoryFotoInfo = new System.IO.DirectoryInfo(checkDirectory.ToString());

                    //if (!directoryFotoInfo.Exists)
                    //    directoryFotoInfo.Create();

                    //Procedimento procedimentoFoto = new Procedimento();
                    //procedimentoFoto.FindMax("FotoNumero", "IdCliente=" + lbl_Id_Empresa.Text);

                    //procedimento.FotoNumero = Convert.ToInt16(procedimentoFoto.FotoNumero + 1);

                    //System.IO.FileInfo fileFotoInfo = new System.IO.FileInfo(CreateFilePath(procedimento.FotoNumero));

                    //while (fileFotoInfo.Exists)
                    //{
                    //    procedimento.FotoNumero += 1;
                    //    fileFotoInfo = new System.IO.FileInfo(CreateFilePath(procedimento.FotoNumero));
                    //}

                    //Mestra.Common.Fotos.UploadWriteToFile(fileFotoInfo.FullName, ref fileFoto);
                    //procedimento.Save();
                }
                //else
                //    throw new Exception("O arquivo selecionado para a foto do procedimento não é de formato válido!");
		}

		private string CreateFilePath(int numFoto)
		{			
			StringBuilder path = new StringBuilder();

            path.Append(@"\\ClusterMestra\DocsDigitais$");
			path.Append(procedimento.FotoDiretorio);
			path.Append(procedimento.FotoInicio);
			path.Append(numFoto.ToString(Fotos.FormatarTamanho(Convert.ToByte(procedimento.FotoTamanho))));
			path.Append(procedimento.FotoTermino);
			path.Append(procedimento.FotoExtensao);

			return path.ToString();
		}

		protected void btnExcluir_Click(object sender, System.EventArgs e)
		{
			try
			{				
				DataSet dsProcedimentoEmpregado = new ProcedimentoEmpregado().Get("IdProcedimento=" + procedimento.Id);
				if (dsProcedimentoEmpregado.Tables[0].Rows.Count > 0)
					throw new Exception("Este Procedimento não pode ser excluído! O Procedimento está associado a um ou mais empregados!");
				
				DataSet dsConjuntoProcedimento = new ConjuntoProcedimento().Get("IdProcedimento=" + procedimento.Id);
				if (dsConjuntoProcedimento.Tables[0].Rows.Count > 0)
					throw new Exception("Este Procedimento não pode ser excluído! O Procedimento pertence a um ou mais conjuntos de procedimentos!");
				
				if (procedimento.IndTipoProcedimento == TipoProcedimento.Resumo)
				{
					DataSet dsCheckProcPai = new Procedimento().Get("IndTipoProcedimento=" + (int)TipoProcedimento.Especifico
						+" AND IdProcedimentoResumo=" + procedimento.Id);

					if (dsCheckProcPai.Tables[0].Rows.Count > 0)
						throw new Exception("Este Procedimento Resumo não pode ser apagado! Ele está sendo utilizado por Procedimento(s) Específico(s)!");
				}

				procedimento.UsuarioId = usuario.Id;
				procedimento.Delete();

				ViewState["IdProcedimento"] = null;
				
				StringBuilder st = new StringBuilder();

				st.Append("window.alert('O Procedimento foi excluído com sucesso!');");
				st.Append("window.location.href='ListaProcedimento.aspx?IdEmpresa=" + lbl_Id_Empresa.Text + "&IdUsuario=" + lbl_Id_Usuario.Text + "';");

                this.ClientScript.RegisterStartupScript(this.GetType(), "ExcluiProcedimento", st.ToString(), true);

                Response.Redirect("ListaProcedimento.aspx?IdEmpresa=" + lbl_Id_Empresa.Text + "&IdUsuario=" + lbl_Id_Usuario.Text);			
			}
			catch(Exception ex)
			{
                MsgBox1.Show("Ordem de Serviço", ex.Message, null,
                        new EO.Web.MsgBoxButton("OK"));                

			}			
		}

		protected void btnCancelar_Click(object sender, System.EventArgs e)
		{
			Response.Redirect("ListaProcedimento.aspx?IdEmpresa=" + lbl_Id_Empresa.Text + "&IdUsuario=" + lbl_Id_Usuario.Text);			
		}

		private void ddlGHE_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (ddlGHE.SelectedValue != "0")
			{
				Ghe ghe = new Ghe();
				ghe.Find(Convert.ToInt32(ddlGHE.SelectedValue));

				txtEPCGHE.Text = ghe.Epc();
				txtMedidasAdmGHE.Text = ghe.GetMedidasControleAdministrativa();
				txtMedidasEduGHE.Text = ghe.GetMedidasControleEducacional();

				DataSet dsEpi = ghe.GetEPI_com_Acidentes();
			
				lstBoxEPIGHE.DataSource = dsEpi;
				lstBoxEPIGHE.DataValueField = "Id";
				lstBoxEPIGHE.DataTextField  = "Descricao";
				lstBoxEPIGHE.DataBind();
			}
			else
			{
				lstBoxEPIGHE.Items.Clear();
				txtEPCGHE.Text = string.Empty;
				txtMedidasAdmGHE.Text = string.Empty;
				txtMedidasEduGHE.Text = string.Empty;
			}
		}

		private void ddlProcedimentoResumo_SelectedIndexChanged(object sender, EventArgs e)
		{
            Procedimento procedimentoResumo = new Procedimento(Convert.ToInt32(ddlProcedimentoResumo.SelectedValue));
            PopulaControlesProcedimento(procedimentoResumo);

            lstBoxOperacoes.ClearSelection();
            lstBoxAtualizacoes.ClearSelection();
            KeepBackColorDisabled();
		}

        private void KeepBackColorDisabled()
        {
            lstBoxOperacoes.BackColor = backColorDisabledBox;
            txtOperacao.BackColor = backColorDisabledBox;

            txtObsQualidade.BackColor = backColorDisabledBox;
            txtPrecaucoes.BackColor = backColorDisabledBox;

            lsbEPI.BackColor = backColorDisabledBox;
            lstBoxEPIs.BackColor = backColorDisabledBox;
            txtMedidasAdm.BackColor = backColorDisabledBox;
            txtMedidasEdu.BackColor = backColorDisabledBox;
            txtEPC.BackColor = backColorDisabledBox;
            lstBoxAtualizacoes.BackColor = backColorDisabledBox;
            wdtDataAtualizacao.BackColor = backColorDisabledBox;
            txtDescricaoAtualizacao.BackColor = backColorDisabledBox;
        }

        private void KeepBackColorDisabledInstrutivo()
        {
            lstEquipamento.BackColor = backColorDisabledBox;
            lstEquipamentoProc.BackColor = backColorDisabledBox;
            lstFerramenta.BackColor = backColorDisabledBox;
            lstFerramentaProc.BackColor = backColorDisabledBox;
            lstProduto.BackColor = backColorDisabledBox;
            lstProdutoProc.BackColor = backColorDisabledBox;
        }

		protected void btnGravar_Click(object sender, System.EventArgs e)
		{
			try
			{
				saveProcedimentoProcess = false;

				PopulaProcedimento();
				UploadFoto();
				
				ViewState["IdProcedimento"] = procedimento.Id;
				RegisterClientCode(procedimento);

				txtnPOPs.Text = procedimento.Numero.ToString("0000");
                //if (!procedimento.FotoProcedimentoUrl().Equals(string.Empty))
                if (procedimento.Foto_Procedimento != null)
                    imgFoto.ImageUrl = FotoProcedimento();
                else
                    imgFoto.ImageUrl = "img/default_img.gif";


                if (!Convert.ToInt32(rblTipoProcedimento.SelectedValue).Equals((int)TipoProcedimento.Especifico))
                    btnDuplicarProc.Enabled = true;                    

				btnExcluir.Enabled = true;
                lkbFichaPOPs.Enabled = true;
                btnAPP.Enabled = true;
				
				lblTipoProcedimento.ForeColor = foreColorDisabledLabel;
				rblTipoProcedimento.Enabled = false;


                MsgBox1.Show("Ordem de Serviço", "O Procedimento foi salvo com sucesso!", null,
                        new EO.Web.MsgBoxButton("OK"));                

			}
			catch(Exception ex)
			{
				if (saveProcedimentoProcess)
				{
                    ViewState["IdProcedimento"] = procedimento.Id;

                    txtnPOPs.Text = procedimento.Numero.ToString("0000");
                    //if (!procedimento.FotoProcedimentoUrl().Equals(string.Empty))
                    if (procedimento.Foto_Procedimento != null)
                        imgFoto.ImageUrl = FotoProcedimento();
                    else
                        imgFoto.ImageUrl = "img/default_img.gif";

                    if (!Convert.ToInt32(rblTipoProcedimento.SelectedValue).Equals((int)TipoProcedimento.Especifico))
                        btnDuplicarProc.Enabled = true;

					btnExcluir.Enabled = true;
                    lkbFichaPOPs.Enabled = true;
                    btnAPP.Enabled = true;
				
					lblTipoProcedimento.ForeColor = foreColorDisabledLabel;
					rblTipoProcedimento.Enabled = false;


                    MsgBox1.Show("Ordem de Serviço", "O procedimento foi salvo com sucesso, porém não foi possível gravar a foto do procedimento! " + ex.Message, null,
                            new EO.Web.MsgBoxButton("OK"));                

				}
				else
                    MsgBox1.Show("Ordem de Serviço", ex.Message, null,
                            new EO.Web.MsgBoxButton("OK"));                

			}

            if (Convert.ToInt32(rblTipoProcedimento.SelectedValue).Equals((int)TipoProcedimento.Especifico))
                KeepBackColorDisabled();
            else if (Convert.ToInt32(rblTipoProcedimento.SelectedValue).Equals((int)TipoProcedimento.Instrutivo))
                KeepBackColorDisabledInstrutivo();
		}

		private void lkbRemoverImagem_Click(object sender, EventArgs e)
		{
			try
			{
				if (procedimento.Foto_Procedimento == "")
					throw new Exception("Este Procedimento não possui Foto!");

                //System.IO.FileInfo fileFotoInfo = new System.IO.FileInfo(CreateFilePath(procedimento.FotoNumero));
                //fileFotoInfo.Delete();     
                
                procedimento.FotoNumero = 0;
                procedimento.Foto_Procedimento = "";

				procedimento.UsuarioId = usuario.Id;
				procedimento.UsuarioProcessoRealizado = "Exclusão da Foto do Procedimento nPOPS " + procedimento.Numero.ToString("0000");
				procedimento.Save();

				imgFoto.ImageUrl = "img/default_img.gif";


                MsgBox1.Show("Ordem de Serviço", "A Foto do Procedimento nPOPS '" + procedimento.Numero.ToString("0000") + "' foi removida com sucesso!", null,
                        new EO.Web.MsgBoxButton("OK"));                

			}
			catch (Exception ex)
			{
                MsgBox1.Show("Ordem de Serviço", ex.Message, null,
                        new EO.Web.MsgBoxButton("OK"));                

			}
		}

		private void imbAddSetor_Click(object sender, ImageClickEventArgs e)
		{            
            if (lstSetor.SelectedItem != null)
				try
				{
					if (procedimento.Id == 0)
					{
						PopulaProcedimento();
						txtnPOPs.Text = procedimento.Numero.ToString("0000");
		
						if (Convert.ToInt32(rblTipoProcedimento.SelectedValue) != (int)TipoProcedimento.Especifico)
							btnDuplicarProc.Enabled = true;
						
                        btnExcluir.Enabled = true;
                        lkbFichaPOPs.Enabled = true;
                        btnAPP.Enabled = true;
						
						lblTipoProcedimento.ForeColor = foreColorDisabledLabel;
						rblTipoProcedimento.Enabled = false;
					}

					bool checkProcess = false;

					foreach (ListItem itemSetor in lstSetor.Items)
						if (itemSetor.Selected)
						{
							ProcedimentoSetor procedimentoSetor = new ProcedimentoSetor();
							procedimentoSetor.Inicialize();

							procedimentoSetor.IdProcedimento = procedimento;
							procedimentoSetor.IdSetor.Id = Convert.ToInt32(itemSetor.Value);
							
							if (!checkProcess)
							{
								checkProcess = true;
								procedimentoSetor.UsuarioProcessoRealizado = "Cadastro de Setores ao procedimento nPOPS " + procedimento.Numero.ToString();
							}

							procedimentoSetor.UsuarioId = usuario.Id;
							procedimentoSetor.Save();
						}

					PopulaLsbSetor();
					PopulaLsbSetorProc();

					ViewState["IdProcedimento"] = procedimento.Id;


                    MsgBox1.Show("Ordem de Serviço", "Os Setores selecionados foram adicionados com sucesso ao procedimento!", null,
                            new EO.Web.MsgBoxButton("OK"));                

				}
				catch (Exception ex)
				{

                    MsgBox1.Show("Ordem de Serviço", "O Procedimento pode não ter sido salvo e os Setores selecionados não puderam ser adicionados! " + ex.Message, null,
                            new EO.Web.MsgBoxButton("OK"));                

				}
			else
                MsgBox1.Show("Ordem de Serviço", "É necessário selecionar pelo menos 1 Setor para ser adicionado ao procedimento!", null,
                    new EO.Web.MsgBoxButton("OK"));                


            if (Convert.ToInt32(rblTipoProcedimento.SelectedValue).Equals((int)TipoProcedimento.Especifico))
                KeepBackColorDisabled();
            else if (Convert.ToInt32(rblTipoProcedimento.SelectedValue).Equals((int)TipoProcedimento.Instrutivo))
                KeepBackColorDisabledInstrutivo();
		}

		private void imbRemoveSetor_Click(object sender, ImageClickEventArgs e)
		{
			if (lstSetorProc.SelectedItem != null)
				try
				{
					bool checkProcess = false;
					
					foreach (ListItem itemSetor in lstSetorProc.Items)
						if (itemSetor.Selected)
						{
							ProcedimentoSetor procedimentoSetor = new ProcedimentoSetor(Convert.ToInt32(itemSetor.Value));

							if (!checkProcess)
							{
								checkProcess = true;
								procedimentoSetor.UsuarioProcessoRealizado = "Exclusão de Setores do procedimento nPOPS " + procedimento.Numero.ToString();
							}

							procedimentoSetor.UsuarioId = usuario.Id;
							procedimentoSetor.Delete();
						}

					PopulaLsbSetor();
					PopulaLsbSetorProc();


                    MsgBox1.Show("Ordem de Serviço", "Os Setores selecionados foram removidos com sucesso do procedimento!", null,
                            new EO.Web.MsgBoxButton("OK"));                

				}
				catch (Exception ex)
				{
                    MsgBox1.Show("Ordem de Serviço", ex.Message, null,
                            new EO.Web.MsgBoxButton("OK"));                

				}
			else
                MsgBox1.Show("Ordem de Serviço", "É necessário selecionar pelo menos 1 Setor para ser removido do procedimento!", null,
                    new EO.Web.MsgBoxButton("OK"));                


            if (Convert.ToInt32(rblTipoProcedimento.SelectedValue).Equals((int)TipoProcedimento.Especifico))
                KeepBackColorDisabled();
            else if (Convert.ToInt32(rblTipoProcedimento.SelectedValue).Equals((int)TipoProcedimento.Instrutivo))
                KeepBackColorDisabledInstrutivo();
		}

		private void imbAddEquipamento_Click(object sender, ImageClickEventArgs e)
		{
			if (lstEquipamento.SelectedItem != null)
				try
				{
					if (procedimento.Id == 0)
					{
						PopulaProcedimento();
						txtnPOPs.Text = procedimento.Numero.ToString("0000");

						if (Convert.ToInt32(rblTipoProcedimento.SelectedValue) != (int)TipoProcedimento.Especifico)
							btnDuplicarProc.Enabled = true;
						btnExcluir.Enabled = true;
                        lkbFichaPOPs.Enabled = true;
                        btnAPP.Enabled = true;
						
						lblTipoProcedimento.ForeColor = foreColorDisabledLabel;
						rblTipoProcedimento.Enabled = false;
					}

					bool checkProcess = false;

					foreach (ListItem itemEquipamento in lstEquipamento.Items)
						if (itemEquipamento.Selected)
						{
							ProcedimentoEquipamento procedimentoEquipamento = new ProcedimentoEquipamento();
							procedimentoEquipamento.Inicialize();

							procedimentoEquipamento.IdProcedimento = procedimento;
							procedimentoEquipamento.IdEquipamento.Id = Convert.ToInt32(itemEquipamento.Value);
							
							if (!checkProcess)
							{
								checkProcess = true;
								procedimentoEquipamento.UsuarioProcessoRealizado = "Cadastro de Equipamentos ao procedimento nPOPS " + procedimento.Numero.ToString();
							}

							procedimentoEquipamento.UsuarioId = usuario.Id;
							procedimentoEquipamento.Save();
						}

					PopulaLsbEquipamento();
					PopulaLsbEquipamentoProc();

					ViewState["IdProcedimento"] = procedimento.Id;


                    MsgBox1.Show("Ordem de Serviço", "Os Equipamentos selecionados foram adicionados com sucesso ao procedimento!", null,
                            new EO.Web.MsgBoxButton("OK"));                

				}
				catch (Exception ex)
				{
                    MsgBox1.Show("Ordem de Serviço", "O Procedimento pode não ter sido salvo e os Equipamentos selecionados não puderam ser adicionados! " + ex.Message, null,
                            new EO.Web.MsgBoxButton("OK"));                

				}
			else
                MsgBox1.Show("Ordem de Serviço", "É necessário selecionar pelo menos 1 Equipamento para ser adicionado ao procedimento!", null,
                    new EO.Web.MsgBoxButton("OK"));                


            if (Convert.ToInt32(rblTipoProcedimento.SelectedValue).Equals((int)TipoProcedimento.Especifico))
                KeepBackColorDisabled();
		}

		private void imbRemoveEquipamento_Click(object sender, ImageClickEventArgs e)
		{
			if (lstEquipamentoProc.SelectedItem != null)
				try
				{
					bool checkProcess = false;
					
					foreach (ListItem itemEquipamento in lstEquipamentoProc.Items)
						if (itemEquipamento.Selected)
						{
							ProcedimentoEquipamento procedimentoEquipamento = new ProcedimentoEquipamento(Convert.ToInt32(itemEquipamento.Value));

							if (!checkProcess)
							{
								checkProcess = true;
								procedimentoEquipamento.UsuarioProcessoRealizado = "Exclusão de Equipamentos do procedimento nPOPS " + procedimento.Numero.ToString();
							}

							procedimentoEquipamento.UsuarioId = usuario.Id;
							procedimentoEquipamento.Delete();
						}

					PopulaLsbEquipamento();
					PopulaLsbEquipamentoProc();

                    MsgBox1.Show("Ordem de Serviço", "Os Equipamentos selecionados foram removidos com sucesso do procedimento!", null,
                            new EO.Web.MsgBoxButton("OK"));                

				}
				catch (Exception ex)
				{
                    MsgBox1.Show("Ordem de Serviço", ex.Message, null,
                            new EO.Web.MsgBoxButton("OK"));                
				}
			else
                MsgBox1.Show("Ordem de Serviço", "É necessário selecionar pelo menos 1 Equipamento para ser removido do procedimento!", null,
                    new EO.Web.MsgBoxButton("OK"));                


            if (Convert.ToInt32(rblTipoProcedimento.SelectedValue).Equals((int)TipoProcedimento.Especifico))
                KeepBackColorDisabled();
		}

		private void imbAddCelula_Click(object sender, ImageClickEventArgs e)
		{
			if (lstCelula.SelectedItem != null)
				try
				{
					if (procedimento.Id == 0)
					{
						PopulaProcedimento();
						txtnPOPs.Text = procedimento.Numero.ToString("0000");
						
						if (Convert.ToInt32(rblTipoProcedimento.SelectedValue) != (int)TipoProcedimento.Especifico)
							btnDuplicarProc.Enabled = true;
						btnExcluir.Enabled = true;
                        lkbFichaPOPs.Enabled = true;
                        btnAPP.Enabled = true;

						lblTipoProcedimento.ForeColor = foreColorDisabledLabel;
						rblTipoProcedimento.Enabled = false;
					}

					bool checkProcess = false;

					foreach (ListItem itemCelula in lstCelula.Items)
						if (itemCelula.Selected)
						{
							ProcedimentoCelula procedimentoCelula = new ProcedimentoCelula();
							procedimentoCelula.Inicialize();

							procedimentoCelula.IdProcedimento = procedimento;
							procedimentoCelula.IdCelula.Id = Convert.ToInt32(itemCelula.Value);
							
							if (!checkProcess)
							{
								checkProcess = true;
								procedimentoCelula.UsuarioProcessoRealizado = "Cadastro de Celulas ao procedimento nPOPS " + procedimento.Numero.ToString();
							}

							procedimentoCelula.UsuarioId = usuario.Id;
							procedimentoCelula.Save();
						}

					PopulaLsbCelula();
					PopulaLsbCelulaProc();

					ViewState["IdProcedimento"] = procedimento.Id;


                    MsgBox1.Show("Ordem de Serviço", "As Celulas selecionadas foram adicionadas com sucesso ao procedimento!", null,
                            new EO.Web.MsgBoxButton("OK"));                

				}
				catch (Exception ex)
				{
                    MsgBox1.Show("Ordem de Serviço", "O Procedimento pode não ter sido salvo e as Celulas selecionadas não puderam ser adicionadas! " + ex.Message, null,
                            new EO.Web.MsgBoxButton("OK"));                
				}
			else
                MsgBox1.Show("Ordem de Serviço", "É necessário selecionar pelo menos 1 Celula para ser adicionada ao procedimento!", null,
                    new EO.Web.MsgBoxButton("OK"));                


            if (Convert.ToInt32(rblTipoProcedimento.SelectedValue).Equals((int)TipoProcedimento.Especifico))
                KeepBackColorDisabled();
            else if (Convert.ToInt32(rblTipoProcedimento.SelectedValue).Equals((int)TipoProcedimento.Instrutivo))
                KeepBackColorDisabledInstrutivo();
		}

		private void imbRemoveCelula_Click(object sender, ImageClickEventArgs e)
		{
			if (lstCelulaProc.SelectedItem != null)
				try
				{
					bool checkProcess = false;
					
					foreach (ListItem itemCelula in lstCelulaProc.Items)
						if (itemCelula.Selected)
						{
							ProcedimentoCelula procedimentoCelula = new ProcedimentoCelula(Convert.ToInt32(itemCelula.Value));

							if (!checkProcess)
							{
								checkProcess = true;
								procedimentoCelula.UsuarioProcessoRealizado = "Exclusão de Celulas do procedimento nPOPS " + procedimento.Numero.ToString();
							}

							procedimentoCelula.UsuarioId = usuario.Id;
							procedimentoCelula.Delete();
						}

					PopulaLsbCelula();
					PopulaLsbCelulaProc();


                    MsgBox1.Show("Ordem de Serviço", "As Celulas selecionadas foram removidas com sucesso do procedimento!", null,
                            new EO.Web.MsgBoxButton("OK"));                

				}
				catch (Exception ex)
				{
                    MsgBox1.Show("Ordem de Serviço", ex.Message, null,
                            new EO.Web.MsgBoxButton("OK"));                

				}
			else
                MsgBox1.Show("Ordem de Serviço", "É necessário selecionar pelo menos 1 Celula para ser removida do procedimento!", null,
                    new EO.Web.MsgBoxButton("OK"));                


            if (Convert.ToInt32(rblTipoProcedimento.SelectedValue).Equals((int)TipoProcedimento.Especifico))
                KeepBackColorDisabled();
            else if (Convert.ToInt32(rblTipoProcedimento.SelectedValue).Equals((int)TipoProcedimento.Instrutivo))
                KeepBackColorDisabledInstrutivo();
		}

		private void imbAddFerramenta_Click(object sender, ImageClickEventArgs e)
		{
			if (lstFerramenta.SelectedItem != null)
				try
				{
					if (procedimento.Id == 0)
					{
						PopulaProcedimento();
						txtnPOPs.Text = procedimento.Numero.ToString("0000");

						if (Convert.ToInt32(rblTipoProcedimento.SelectedValue) != (int)TipoProcedimento.Especifico)
							btnDuplicarProc.Enabled = true;
						btnExcluir.Enabled = true;
                        lkbFichaPOPs.Enabled = true;
                        btnAPP.Enabled = true;

						lblTipoProcedimento.ForeColor = foreColorDisabledLabel;
						rblTipoProcedimento.Enabled = false;
					}

					bool checkProcess = false;

					foreach (ListItem itemFerramenta in lstFerramenta.Items)
						if (itemFerramenta.Selected)
						{
							ProcedimentoFerramenta procedimentoFerramenta = new ProcedimentoFerramenta();
							procedimentoFerramenta.Inicialize();

							procedimentoFerramenta.IdProcedimento = procedimento;
							procedimentoFerramenta.IdFerramenta.Id = Convert.ToInt32(itemFerramenta.Value);
							
							if (!checkProcess)
							{
								checkProcess = true;
								procedimentoFerramenta.UsuarioProcessoRealizado = "Cadastro de Ferramentas ao procedimento nPOPS " + procedimento.Numero.ToString();
							}

							procedimentoFerramenta.UsuarioId = usuario.Id;
							procedimentoFerramenta.Save();
						}

					PopulaLsbFerramenta();
					PopulaLsbFerramentaProc();

					ViewState["IdProcedimento"] = procedimento.Id;

                    MsgBox1.Show("Ordem de Serviço", "As Ferramentas selecionadas foram adicionadas com sucesso ao procedimento!", null,
                            new EO.Web.MsgBoxButton("OK"));                

				}
				catch (Exception ex)
				{
                    MsgBox1.Show("Ordem de Serviço", "O Procedimento pode não ter sido salvo e as Ferramentas selecionadas não puderam ser adicionadas! " + ex.Message, null,
                            new EO.Web.MsgBoxButton("OK"));                
				}
			else
                MsgBox1.Show("Ordem de Serviço", "É necessário selecionar pelo menos 1 Ferramenta para ser adicionada ao procedimento!", null,
                    new EO.Web.MsgBoxButton("OK"));                


            if (Convert.ToInt32(rblTipoProcedimento.SelectedValue).Equals((int)TipoProcedimento.Especifico))
                KeepBackColorDisabled();
		}

		private void imbRemoveFerramenta_Click(object sender, ImageClickEventArgs e)
		{
			if (lstFerramentaProc.SelectedItem != null)
				try
				{
					bool checkProcess = false;
					
					foreach (ListItem itemFerramenta in lstFerramentaProc.Items)
						if (itemFerramenta.Selected)
						{
							ProcedimentoFerramenta procedimentoFerramenta = new ProcedimentoFerramenta(Convert.ToInt32(itemFerramenta.Value));

							if (!checkProcess)
							{
								checkProcess = true;
								procedimentoFerramenta.UsuarioProcessoRealizado = "Exclusão de Ferramentas do procedimento nPOPS " + procedimento.Numero.ToString();
							}

							procedimentoFerramenta.UsuarioId = usuario.Id;
							procedimentoFerramenta.Delete();
						}

					PopulaLsbFerramenta();
					PopulaLsbFerramentaProc();


                    MsgBox1.Show("Ordem de Serviço", "As Ferramentas selecionadas foram removidas com sucesso do procedimento!", null,
                            new EO.Web.MsgBoxButton("OK"));                

				}
				catch (Exception ex)
				{
                    MsgBox1.Show("Ordem de Serviço", ex.Message, null,
                            new EO.Web.MsgBoxButton("OK"));                

				}
			else
                MsgBox1.Show("Ordem de Serviço", "É necessário selecionar pelo menos 1 Ferramenta para ser removida do procedimento!", null,
                    new EO.Web.MsgBoxButton("OK"));                


            if (Convert.ToInt32(rblTipoProcedimento.SelectedValue).Equals((int)TipoProcedimento.Especifico))
                KeepBackColorDisabled();
		}

		private void imbAddProduto_Click(object sender, ImageClickEventArgs e)
		{
			if (lstProduto.SelectedItem != null)
				try
				{
					if (procedimento.Id == 0)
					{
						PopulaProcedimento();
						txtnPOPs.Text = procedimento.Numero.ToString("0000");

						if (Convert.ToInt32(rblTipoProcedimento.SelectedValue) != (int)TipoProcedimento.Especifico)
							btnDuplicarProc.Enabled = true;
						btnExcluir.Enabled = true;
                        lkbFichaPOPs.Enabled = true;
                        btnAPP.Enabled = true;

						lblTipoProcedimento.ForeColor = foreColorDisabledLabel;
						rblTipoProcedimento.Enabled = false;
					}

					bool checkProcess = false;

					foreach (ListItem itemProduto in lstProduto.Items)
						if (itemProduto.Selected)
						{
							ProcedimentoProduto procedimentoProduto = new ProcedimentoProduto();
							procedimentoProduto.Inicialize();

							procedimentoProduto.IdProcedimento = procedimento;
							procedimentoProduto.IdProduto.Id = Convert.ToInt32(itemProduto.Value);
							
							if (!checkProcess)
							{
								checkProcess = true;
								procedimentoProduto.UsuarioProcessoRealizado = "Cadastro de Produtos ao procedimento nPOPS " + procedimento.Numero.ToString();
							}

							procedimentoProduto.UsuarioId = usuario.Id;
							procedimentoProduto.Save();
						}

					PopulaLsbProduto();
					PopulaLsbProdutoProc();

					ViewState["IdProcedimento"] = procedimento.Id;


                    MsgBox1.Show("Ordem de Serviço", "Os Produtos selecionados foram adicionados com sucesso ao procedimento!", null,
                            new EO.Web.MsgBoxButton("OK"));                

				}
				catch (Exception ex)
				{
                    MsgBox1.Show("Ordem de Serviço", "O Procedimento pode não ter sido salvo e os Produtos selecionados não puderam ser adicionados! " + ex.Message, null,
                            new EO.Web.MsgBoxButton("OK"));                
				}
			else
                MsgBox1.Show("Ordem de Serviço", "É necessário selecionar pelo menos 1 Produto para ser adicionado ao procedimento!", null,
                    new EO.Web.MsgBoxButton("OK"));                


            if (Convert.ToInt32(rblTipoProcedimento.SelectedValue).Equals((int)TipoProcedimento.Especifico))
                KeepBackColorDisabled();
		}

		private void imbRemoveProduto_Click(object sender, ImageClickEventArgs e)
		{
			if (lstProdutoProc.SelectedItem != null)
				try
				{
					bool checkProcess = false;
					
					foreach (ListItem itemProduto in lstProdutoProc.Items)
						if (itemProduto.Selected)
						{
							ProcedimentoProduto procedimentoProduto = new ProcedimentoProduto(Convert.ToInt32(itemProduto.Value));

							if (!checkProcess)
							{
								checkProcess = true;
								procedimentoProduto.UsuarioProcessoRealizado = "Exclusão de Produtos do procedimento nPOPS " + procedimento.Numero.ToString();
							}

							procedimentoProduto.UsuarioId = usuario.Id;
							procedimentoProduto.Delete();
						}

					PopulaLsbProduto();
					PopulaLsbProdutoProc();

                    MsgBox1.Show("Ordem de Serviço", "Os Produtos selecionados foram removidos com sucesso do procedimento!", null,
                            new EO.Web.MsgBoxButton("OK"));                

				}
				catch (Exception ex)
				{
                    MsgBox1.Show("Ordem de Serviço", ex.Message, null,
                            new EO.Web.MsgBoxButton("OK"));                

				}
			else
                MsgBox1.Show("Ordem de Serviço", "É necessário selecionar pelo menos 1 Produto para ser removido do procedimento!", null,
                    new EO.Web.MsgBoxButton("OK"));                


            if (Convert.ToInt32(rblTipoProcedimento.SelectedValue).Equals((int)TipoProcedimento.Especifico))
                KeepBackColorDisabled();
		}

		private void imbDown_Click(object sender, ImageClickEventArgs e)
		{
			if (lsbEPI.SelectedItem != null)
				try
				{
					if (procedimento.Id == 0)
					{
						PopulaProcedimento();
						txtnPOPs.Text = procedimento.Numero.ToString("0000");

						if (Convert.ToInt32(rblTipoProcedimento.SelectedValue) != (int)TipoProcedimento.Especifico)
							btnDuplicarProc.Enabled = true;
						btnExcluir.Enabled = true;
                        lkbFichaPOPs.Enabled = true;
                        btnAPP.Enabled = true;

						lblTipoProcedimento.ForeColor = foreColorDisabledLabel;
						rblTipoProcedimento.Enabled = false;
					}
					
					bool checkProcess = false;

					foreach (ListItem itemEPI in lsbEPI.Items)
						if (itemEPI.Selected)
						{
							ProcedimentoEPI procedimentoEPI = new ProcedimentoEPI();
							procedimentoEPI.Inicialize();

							procedimentoEPI.IdProcedimento = procedimento;
							procedimentoEPI.IdEpi.Id = Convert.ToInt32(itemEPI.Value);

							if (!checkProcess)
							{
								checkProcess = true;
								procedimentoEPI.UsuarioProcessoRealizado = "Cadastro de EPIs ao procedimento nPOPS " + procedimento.Numero.ToString();
							}

							procedimentoEPI.UsuarioId = usuario.Id;
							procedimentoEPI.Save();
						}

					PopulaLsbEpi();
					PopulaListBoxEpi(procedimento);

					ViewState["IdProcedimento"] = procedimento.Id;


                    MsgBox1.Show("Ordem de Serviço", "Os EPIs selecionados foram adicionados com sucesso ao procedimento!", null,
                            new EO.Web.MsgBoxButton("OK"));                

				}
				catch (Exception ex)
				{
                    MsgBox1.Show("Ordem de Serviço", "O Procedimento pode não ter sido salvo e os EPIs selecionados não puderam ser adicionados! " + ex.Message, null,
                            new EO.Web.MsgBoxButton("OK"));                

				}
			else
                MsgBox1.Show("Ordem de Serviço", "É necessário selecionar pelo menos 1 EPI para ser adicionado ao procedimento!", null,
                    new EO.Web.MsgBoxButton("OK"));                

		}

		private void imbUp_Click(object sender, ImageClickEventArgs e)
		{
			if (lstBoxEPIs.SelectedItem != null)
				try
				{
					bool checkProcess = false;

					foreach (ListItem itemEPI in lstBoxEPIs.Items)
						if (itemEPI.Selected)
						{
							ProcedimentoEPI procedimentoEPI = new ProcedimentoEPI(Convert.ToInt32(itemEPI.Value));

							if (!checkProcess)
							{
								checkProcess = true;
								procedimentoEPI.UsuarioProcessoRealizado = "Exclusão de EPIs do procedimento nPOPS " + procedimento.Numero.ToString();
							}

							procedimentoEPI.UsuarioId = usuario.Id;
							procedimentoEPI.Delete();
						}

					PopulaLsbEpi();
					PopulaListBoxEpi(procedimento);

                    MsgBox1.Show("Ordem de Serviço", "Os EPIs selecionados foram removidos com sucesso do procedimento!", null,
                            new EO.Web.MsgBoxButton("OK"));                

				}
				catch (Exception ex)
				{
                    MsgBox1.Show("Ordem de Serviço", ex.Message, null,
                            new EO.Web.MsgBoxButton("OK"));                

				}
			else
                MsgBox1.Show("Ordem de Serviço", "É necessário selecionar pelo menos 1 EPI para ser removido do procedimento!", null,
                    new EO.Web.MsgBoxButton("OK"));                

		}

		private void ddlLaudos_SelectedIndexChanged(object sender, EventArgs e)
		{
			PopulaDDLGhe();

			lstBoxEPIGHE.Items.Clear();
			txtEPCGHE.Text = string.Empty;
			txtMedidasAdmGHE.Text = string.Empty;
			txtMedidasEduGHE.Text = string.Empty;
		}

		protected void btnTransfMedidasControle_Click1(object sender, EventArgs e)
		{
			if (!ddlGHE.SelectedValue.Equals("0"))
			{
                //IDbTransaction transaction = procedimento.GetTransaction();
              

				try
				{
                    if (lbl_Id_Procedimento.Text == "0")   //  procedimento.Id == 0)
					{
						txtMedidasAdm.Text = txtMedidasAdmGHE.Text.Trim();
						txtEPC.Text = txtEPCGHE.Text.Trim();
						txtMedidasEdu.Text = txtMedidasEduGHE.Text.Trim();
						PopulaProcedimento();
						txtnPOPs.Text = procedimento.Numero.ToString("0000");

						if (Convert.ToInt32(rblTipoProcedimento.SelectedValue) != (int)TipoProcedimento.Especifico)
							btnDuplicarProc.Enabled = true;
						btnExcluir.Enabled = true;
                        lkbFichaPOPs.Enabled = true;
                        btnAPP.Enabled = true;

						lblTipoProcedimento.ForeColor = foreColorDisabledLabel;
						rblTipoProcedimento.Enabled = false;
					}
					else
					{
                        procedimento = new Procedimento(System.Convert.ToInt32(lbl_Id_Procedimento.Text));
                        
                        ArrayList alProcedimentoEPI = new ProcedimentoEPI().Find("IdProcedimento=" + lbl_Id_Procedimento.Text); //procedimento.Id.ToString());

						foreach (ProcedimentoEPI procedimentoEPI in alProcedimentoEPI)
						{
							//procedimentoEPI.Transaction = transaction;
							procedimentoEPI.UsuarioId = usuario.Id;
							procedimentoEPI.Delete();
                            System.Threading.Thread.Sleep(200);
                        }
					}
				
					foreach (ListItem itemEPI in lstBoxEPIGHE.Items)
					{
                        string xCondicao = "";

						ProcedimentoEPI procedimentoEPI = new ProcedimentoEPI();
						procedimentoEPI.Inicialize();

						procedimentoEPI.IdProcedimento = procedimento;
						procedimentoEPI.IdEpi.Id = Convert.ToInt32(itemEPI.Value);

                        if (itemEPI.Text.IndexOf("(") > 0)
                        {
                            xCondicao = itemEPI.Text.Substring(itemEPI.Text.IndexOf("(") - 1);
                            procedimentoEPI.Condicao = xCondicao;
                        }
						
						//procedimentoEPI.Transaction = transaction;
						procedimentoEPI.UsuarioId = usuario.Id;
						procedimentoEPI.Save();

                       System.Threading.Thread.Sleep(400);
                    }

					procedimento.Epc = txtEPCGHE.Text.Trim();
					procedimento.MedidasAdm = txtMedidasAdmGHE.Text.Trim();
					procedimento.MedidasEdu = txtMedidasEduGHE.Text.Trim();

					procedimento.UsuarioProcessoRealizado = "Transferência de dados de Medidas de Controle do GHE para o Procedimento";
					//procedimento.Transaction = transaction;
					procedimento.UsuarioId = usuario.Id;
					procedimento.Save();

                    //transaction.Commit

                    ViewState["IdProcedimento"] = lbl_Id_Procedimento.Text; //procedimento.Id;

					PopulaLsbEpi();
				    PopulaListBoxEpi(procedimento);
					txtEPC.Text = procedimento.Epc;
					txtMedidasAdm.Text = procedimento.MedidasAdm;
					txtMedidasEdu.Text = procedimento.MedidasEdu;

                 

                    MsgBox1.Show("Ordem de Serviço", "Os dados do GHE selecionado foram transferidos com sucesso para o Procedimento nPOPS " + procedimento.Numero.ToString("0000"), null,
                            new EO.Web.MsgBoxButton("OK"));
                    return;          

				}
				catch (Exception ex)
				{
					//transaction.Rollback();
                    MsgBox1.Show("Ordem de Serviço", "O Procedimento pode não ter sido salvo e os dados do GHE selecionado não puderam ser transferidos! " + ex.Message, null,
                            new EO.Web.MsgBoxButton("OK"));                

				}
			}
			else
                MsgBox1.Show("Ordem de Serviço", "É necessário selecionar o Levantamento e o GHE para executar a transferência dos dados de Medidas de Controle!", null,
                    new EO.Web.MsgBoxButton("OK"));                

		}

		protected void btnGravarOperacao_Click(object sender, EventArgs e)
		{
			bool saveProcedimentoProcess = false;

			try
			{
				if (procedimento.Id == 0)
				{
					PopulaProcedimento();
					txtnPOPs.Text = procedimento.Numero.ToString("0000");

					if (Convert.ToInt32(rblTipoProcedimento.SelectedValue) != (int)TipoProcedimento.Especifico)
						btnDuplicarProc.Enabled = true;
					btnExcluir.Enabled = true;
                    lkbFichaPOPs.Enabled = true;
                    btnAPP.Enabled = true;

					lblTipoProcedimento.ForeColor = foreColorDisabledLabel;
					rblTipoProcedimento.Enabled = false;
					saveProcedimentoProcess = true;
				}
				else
					saveProcedimentoProcess = true;

				string tipoProcess = string.Empty;
				Operacao operacao = new Operacao();

				if (lstBoxOperacoes.SelectedValue.Equals("0"))
				{
					tipoProcess = "cadastrada";
					operacao.Inicialize();
					operacao.IdProcedimento = procedimento;
				}
				else
				{
					tipoProcess = "editada";
					operacao.Find(Convert.ToInt32(lstBoxOperacoes.SelectedValue));
				}

				operacao.Descricao = txtOperacao.Text.Trim();
				
                operacao.Precaucoes = txtObsQualidade.Text.Trim();

				operacao.UsuarioId = usuario.Id;
				operacao.Save();

				PopulaListBoxOperacoes(procedimento);
				lstBoxOperacoes.ClearSelection();
				lstBoxOperacoes.Items.FindByValue(operacao.Id.ToString()).Selected = true;

				btnExcluirOpe.Enabled = true;

                ViewState["IdProcedimento"] = procedimento.Id;
								
                MsgBox1.Show("Ordem de Serviço", "A Operação foi " + tipoProcess + " com sucesso!", null,
                        new EO.Web.MsgBoxButton("OK"));                


                if (tipoProcess.Equals("cadastrada") && procedimento.IndTipoProcedimento.Equals(TipoProcedimento.Resumo))
                {
                    HabilitaWebFormsOperacao();

                    PopulaLsbPerigos(operacao.Id);
                    PopulaLsbAspectos(operacao.Id);
                }
			}
			catch (Exception ex)
			{
				if (!saveProcedimentoProcess)
                    MsgBox1.Show("Ordem de Serviço", "O Procedimento não foi salvo e a Operação não foi gravada com sucesso! " + ex.Message, null,
                        new EO.Web.MsgBoxButton("OK"));                

				else
                    MsgBox1.Show("Ordem de Serviço", ex.Message, null,
                            new EO.Web.MsgBoxButton("OK"));                

			}

            if (Convert.ToInt32(rblTipoProcedimento.SelectedValue).Equals((int)TipoProcedimento.Instrutivo))
                KeepBackColorDisabledInstrutivo();
		}

        private void HabilitaWebFormsOperacao()
        {
            lblPerigos.ForeColor = foreColorEnabledLabel;
            lsbPerigos.Enabled = true;
            lsbPerigos.BackColor = backColorEnabledBox;
            lblPerigoSelected.ForeColor = foreColorEnabledLabel;
            lsbPerigoSelected.Enabled = true;
            lsbPerigoSelected.BackColor = backColorEnabledBoxYellow;
            imbAddPerigo.Enabled = true;
            imbAddPerigo.ImageUrl = "InfragisticsImg/XpOlive/next_down.gif";
            imbRemovePerigo.Enabled = true;
            imbRemovePerigo.ImageUrl = "InfragisticsImg/XpOlive/prev_down.gif";
            lblddlPerigo.ForeColor = foreColorEnabledLabel;
            ddlPerigo.Enabled = true;
            ddlPerigo.BackColor = backColorEnabledBox;
            listBxRiscosOperacao.Items.Clear();
            btnExcluirRisco.Enabled = false;
            rblSeveridade.ClearSelection();
            rblProbabilidade.ClearSelection();
            btnGravarRisco.Enabled = false;
            lblAspectos.ForeColor = foreColorEnabledLabel;
            lsbAspectos.Enabled = true;
            lsbAspectos.BackColor = backColorEnabledBox;
            lblAspectoSelected.ForeColor = foreColorEnabledLabel;
            lsbAspectoSelected.Enabled = true;
            lsbAspectoSelected.BackColor = backColorEnabledBoxYellow;
            imbAddAspecto.Enabled = true;
            imbAddAspecto.ImageUrl = "InfragisticsImg/XpOlive/next_down.gif";
            imbRemoveAspecto.Enabled = true;
            imbRemoveAspecto.ImageUrl = "InfragisticsImg/XpOlive/prev_down.gif";
            lblddlAspecto.ForeColor = foreColorEnabledLabel;
            ddlAspecto.Enabled = true;
            ddlAspecto.BackColor = backColorEnabledBox;
            lsbImpactosSelected.Items.Clear();
            btnExcluirImpacto.Enabled = false;
            rblSeveridadeImpacto.ClearSelection();
            rblProbabilidadeImpacto.ClearSelection();
            btnGravarDetalhesImpacto.Enabled = false;
        }

        private void DesabilitaWebFormsOperacao()
        {
            lblPerigos.ForeColor = foreColorDisabledLabel;
            lsbPerigos.Items.Clear();
            lsbPerigos.BackColor = backColorDisabledBox;
            lsbPerigos.Enabled = false;
            lblPerigoSelected.ForeColor = foreColorDisabledLabel;
            lsbPerigoSelected.Items.Clear();
            lsbPerigoSelected.BackColor = backColorDisabledBox;
            lsbPerigoSelected.Enabled = false;
            imbAddPerigo.ImageUrl = "InfragisticsImg/XpOlive/next_downDisabled.gif";
            imbAddPerigo.Enabled = false;
            imbRemovePerigo.ImageUrl = "InfragisticsImg/XpOlive/prev_downDisabled.gif";
            imbRemovePerigo.Enabled = false;
            lblddlPerigo.ForeColor = foreColorDisabledLabel;
            ddlPerigo.Items.Clear();
            ddlPerigo.BackColor = backColorDisabledBox;
            ddlPerigo.Enabled = false;
            listBxRiscosOperacao.Items.Clear();
            btnExcluirRisco.Enabled = false;
            rblSeveridade.ClearSelection();
            rblProbabilidade.ClearSelection();
            btnGravarRisco.Enabled = false;
            lblAspectos.ForeColor = foreColorDisabledLabel;
            lsbAspectos.Items.Clear();
            lsbAspectos.BackColor = backColorDisabledBox;
            lsbAspectos.Enabled = false;
            lblAspectoSelected.ForeColor = foreColorDisabledLabel;
            lsbAspectoSelected.Items.Clear();
            lsbAspectoSelected.BackColor = backColorDisabledBox;
            lsbAspectoSelected.Enabled = false;
            imbAddAspecto.ImageUrl = "InfragisticsImg/XpOlive/next_downDisabled.gif";
            imbAddAspecto.Enabled = false;
            imbRemoveAspecto.ImageUrl = "InfragisticsImg/XpOlive/prev_downDisabled.gif";
            imbRemoveAspecto.Enabled = false;
            lblddlAspecto.ForeColor = foreColorDisabledLabel;
            ddlAspecto.Items.Clear();
            ddlAspecto.BackColor = backColorDisabledBox;
            ddlAspecto.Enabled = false;
            lsbImpactosSelected.Items.Clear();
            btnExcluirImpacto.Enabled = false;
            rblSeveridadeImpacto.ClearSelection();
            rblProbabilidadeImpacto.ClearSelection();
            btnGravarDetalhesImpacto.Enabled = false;
        }

		private void lstBoxOperacoes_SelectedIndexChanged(object sender, EventArgs e)
		{
            txtPrecaucoes.Text = "";

			if (lstBoxOperacoes.SelectedValue.Equals("0"))
			{
				txtOperacao.Text = string.Empty;
				txtObsQualidade.Text = string.Empty;
				btnExcluirOpe.Enabled = false;

                DesabilitaWebFormsOperacao();
			}
			else if (!lstBoxOperacoes.SelectedValue.Equals(string.Empty))
			{
				btnExcluirOpe.Enabled = true;

				Operacao operacao = new Operacao(Convert.ToInt32(lstBoxOperacoes.SelectedValue));
                txtOperacao.Text = operacao.Descricao;
				txtObsQualidade.Text = operacao.Precaucoes;

                if (procedimento.IndTipoProcedimento.Equals(TipoProcedimento.Resumo))
                {
                    HabilitaWebFormsOperacao();

                    PopulaLsbPerigos(Convert.ToInt32(lstBoxOperacoes.SelectedValue));
                    PopulaLsbDDLPerigosProc(Convert.ToInt32(lstBoxOperacoes.SelectedValue));
                    PopulaLsbAspectos(Convert.ToInt32(lstBoxOperacoes.SelectedValue));
                    PopulaLsbDDLAspectosProc(Convert.ToInt32(lstBoxOperacoes.SelectedValue));
                }
			}

            if (procedimento.IndTipoProcedimento.Equals(TipoProcedimento.Instrutivo))
                KeepBackColorDisabledInstrutivo();
		}

		private void listBxRiscosOperacao_SelectedIndexChanged(object sender, EventArgs e)
		{
			rblSeveridade.ClearSelection();
            rblProbabilidade.ClearSelection();

            if (ddlPerigo.Items.Count.Equals(0))
            {
                OperacaoRiscoAcidente operacaoRisco = new OperacaoRiscoAcidente(Convert.ToInt32(listBxRiscosOperacao.SelectedValue));

                if (!operacaoRisco.GrauSeveridade.Equals(0))
                    rblSeveridade.Items.FindByValue(operacaoRisco.GrauSeveridade.ToString()).Selected = true;
                if (!operacaoRisco.GrauFrequencia.Equals(0))
                    rblProbabilidade.Items.FindByValue(operacaoRisco.GrauFrequencia.ToString()).Selected = true;
            }
            else
            {
                OperacaoPerigoRiscoAcidente operacaoPerigoRisco = new OperacaoPerigoRiscoAcidente(Convert.ToInt32(listBxRiscosOperacao.SelectedValue));

                if (!Convert.ToInt32(operacaoPerigoRisco.IndGrauSeveridadeRisco).ToString().Equals("0"))
                    rblSeveridade.Items.FindByValue(Convert.ToInt32(operacaoPerigoRisco.IndGrauSeveridadeRisco).ToString()).Selected = true;
                if (!Convert.ToInt32(operacaoPerigoRisco.IndProbabilidadeRisco).ToString().Equals("0"))
                    rblProbabilidade.Items.FindByValue(Convert.ToInt32(operacaoPerigoRisco.IndProbabilidadeRisco).ToString()).Selected = true;
            }

            btnExcluirRisco.Enabled = true;
            btnGravarRisco.Enabled = true;
		}

        protected void btnGravarRisco_Click(object sender, EventArgs e)
		{
		    try
		    {
                if (ddlPerigo.Items.Count.Equals(0))
                {
                    OperacaoRiscoAcidente operacaoRisco = new OperacaoRiscoAcidente(Convert.ToInt32(listBxRiscosOperacao.SelectedValue));

                    if (rblSeveridade.SelectedItem != null)
                        switch (Convert.ToInt32(rblSeveridade.SelectedValue))
                        {
                            case (int)GrauSeveridadeRisco.ExtremamentePrejudicial:
                                operacaoRisco.GrauSeveridade = (int)GrauSeveridadeRisco.ExtremamentePrejudicial;
                                break;
                            case (int)GrauSeveridadeRisco.Marginal:
                                operacaoRisco.GrauSeveridade = (int)GrauSeveridadeRisco.Marginal;
                                break;
                            case (int)GrauSeveridadeRisco.Critica:
                                operacaoRisco.GrauSeveridade = (int)GrauSeveridadeRisco.Critica;
                                break;
                            case (int)GrauSeveridadeRisco.Desprezivel:
                                operacaoRisco.GrauSeveridade = (int)GrauSeveridadeRisco.Desprezivel;
                                break;
                        }

                    if (rblProbabilidade.SelectedItem != null)
                        switch (Convert.ToInt32(rblProbabilidade.SelectedValue))
                        {
                            case (int)ProbabilidadeRisco.Critico:
                                operacaoRisco.GrauFrequencia = (int)ProbabilidadeRisco.Critico;
                                break;
                            case (int)ProbabilidadeRisco.Moderado:
                                operacaoRisco.GrauFrequencia = (int)ProbabilidadeRisco.Moderado;
                                break;
                            case (int)ProbabilidadeRisco.Desprezivel:
                                operacaoRisco.GrauFrequencia = (int)ProbabilidadeRisco.Desprezivel;
                                break;
                        }

                    operacaoRisco.UsuarioProcessoRealizado = "Edição dos detalhes do Risco no Procedimento";
                    operacaoRisco.UsuarioId = usuario.Id;
                    operacaoRisco.Save();
                }
                else
                {
                    OperacaoPerigoRiscoAcidente operacaoPerigoRisco = new OperacaoPerigoRiscoAcidente(Convert.ToInt32(listBxRiscosOperacao.SelectedValue));

                    if (rblSeveridade.SelectedItem != null)
                        switch (Convert.ToInt32(rblSeveridade.SelectedValue))
                        {
                            case (int)GrauSeveridadeRisco.ExtremamentePrejudicial:
                                operacaoPerigoRisco.IndGrauSeveridadeRisco = GrauSeveridadeRisco.ExtremamentePrejudicial;
                                break;
                            case (int)GrauSeveridadeRisco.Marginal:
                                operacaoPerigoRisco.IndGrauSeveridadeRisco = GrauSeveridadeRisco.Marginal;
                                break;
                            case (int)GrauSeveridadeRisco.Critica:
                                operacaoPerigoRisco.IndGrauSeveridadeRisco = GrauSeveridadeRisco.Critica;
                                break;
                            case (int)GrauSeveridadeRisco.Desprezivel:
                                operacaoPerigoRisco.IndGrauSeveridadeRisco = GrauSeveridadeRisco.Desprezivel;
                                break;
                        }

                    if (rblProbabilidade.SelectedItem != null)
                        switch (Convert.ToInt32(rblProbabilidade.SelectedValue))
                        {
                            case (int)ProbabilidadeRisco.Critico:
                                operacaoPerigoRisco.IndProbabilidadeRisco = ProbabilidadeRisco.Critico;
                                break;
                            case (int)ProbabilidadeRisco.Moderado:
                                operacaoPerigoRisco.IndProbabilidadeRisco = ProbabilidadeRisco.Moderado;
                                break;
                            case (int)ProbabilidadeRisco.Desprezivel:
                                operacaoPerigoRisco.IndProbabilidadeRisco = ProbabilidadeRisco.Desprezivel;
                                break;
                        }

                    operacaoPerigoRisco.UsuarioProcessoRealizado = "Edição dos detalhes do Risco no Procedimento";
                    operacaoPerigoRisco.UsuarioId = usuario.Id;
                    operacaoPerigoRisco.Save();
                }

                MsgBox1.Show("Ordem de Serviço", "Os detalhes do Risco selecionado foram atualizados com sucesso!", null,
                        new EO.Web.MsgBoxButton("OK"));                

		    }
		    catch (Exception ex)
		    {
                MsgBox1.Show("Ordem de Serviço", ex.Message, null,
                        new EO.Web.MsgBoxButton("OK"));                

		    }
		}

		private void imbUpOperacao_Click(object sender, ImageClickEventArgs e)
		{				
			try
			{
				if (lstBoxOperacoes.SelectedIndex.Equals(0))
					throw new Exception("É necessário selecionar uma Operação para alterar sua posição!");

				if (lstBoxOperacoes.SelectedIndex.Equals(1))
					throw new Exception("É necessário selecionar uma Operação em uma posição posterior para movê-la para a posição anterior!");
				
				Operacao operacaoSelected = new Operacao(Convert.ToInt32(lstBoxOperacoes.SelectedValue));
				Operacao operacaoPrevious = new Operacao(Convert.ToInt32(lstBoxOperacoes.Items[lstBoxOperacoes.SelectedIndex - 1].Value));

				operacaoSelected.Sequencia -= 1;
				operacaoPrevious.Sequencia += 1;
			
				operacaoSelected.UsuarioProcessoRealizado = "Edição da sequência das Operações no Procedimento nPOPS " + procedimento.Numero.ToString("0000");
				operacaoSelected.UsuarioId = usuario.Id;
				operacaoSelected.Save();

				operacaoPrevious.Save();

				PopulaListBoxOperacoes(procedimento);
				lstBoxOperacoes.ClearSelection();
				lstBoxOperacoes.Items.FindByValue(operacaoSelected.Id.ToString()).Selected = true;


                MsgBox1.Show("Ordem de Serviço", "A sequência da Operação selecionada foi alterada com sucesso!", null,
                        new EO.Web.MsgBoxButton("OK"));                

			}
			catch (Exception ex)
			{
                MsgBox1.Show("Ordem de Serviço", ex.Message, null,
                        new EO.Web.MsgBoxButton("OK"));                

			}

           if (Convert.ToInt32(rblTipoProcedimento.SelectedValue).Equals((int)TipoProcedimento.Instrutivo))
                KeepBackColorDisabledInstrutivo();
		}

		private void imbDownOperacao_Click(object sender, ImageClickEventArgs e)
		{
			try
			{
				if (lstBoxOperacoes.SelectedIndex.Equals(0))
					throw new Exception("É necessário selecionar uma Operação para alterar sua posição!");

				if (lstBoxOperacoes.SelectedIndex.Equals(lstBoxOperacoes.Items.Count - 1))
					throw new Exception("É necessário selecionar uma Operação em uma posição anterior para movê-la para a posição posterior!");
					
				Operacao operacaoSelected = new Operacao(Convert.ToInt32(lstBoxOperacoes.SelectedValue));
				Operacao operacaoNext = new Operacao(Convert.ToInt32(lstBoxOperacoes.Items[lstBoxOperacoes.SelectedIndex + 1].Value));

				operacaoSelected.Sequencia += 1;
				operacaoNext.Sequencia -= 1;
				
				operacaoSelected.UsuarioProcessoRealizado = "Edição da sequência das Operações no Procedimento nPOPS " + procedimento.Numero.ToString("0000");
				operacaoSelected.UsuarioId = usuario.Id;
				operacaoSelected.Save();

				operacaoNext.Save();

				PopulaListBoxOperacoes(procedimento);
				lstBoxOperacoes.ClearSelection();
				lstBoxOperacoes.Items.FindByValue(operacaoSelected.Id.ToString()).Selected = true;

                MsgBox1.Show("Ordem de Serviço", "A sequência da Operação selecionada foi alterada com sucesso!", null,
                        new EO.Web.MsgBoxButton("OK"));                

			}
			catch (Exception ex)
			{
                MsgBox1.Show("Ordem de Serviço", ex.Message, null,
                        new EO.Web.MsgBoxButton("OK"));                

			}

            if (Convert.ToInt32(rblTipoProcedimento.SelectedValue).Equals((int)TipoProcedimento.Instrutivo))
                KeepBackColorDisabledInstrutivo();
		}

        protected void btnExcluirOpe_Click(object sender, EventArgs e)
		{			
			try
			{
				Operacao operacao = new Operacao(Convert.ToInt32(lstBoxOperacoes.SelectedValue));

				operacao.UsuarioId = usuario.Id;
				operacao.Delete();

				PopulaListBoxOperacoes(procedimento);
				
                btnExcluirOpe.Enabled = false;
                txtOperacao.Text = string.Empty;
                txtObsQualidade.Text = string.Empty;

                DesabilitaWebFormsOperacao();


                MsgBox1.Show("Ordem de Serviço", "A Operação selecionada foi excluída com sucesso!", null,
                        new EO.Web.MsgBoxButton("OK"));                

			}
			catch (Exception ex)
			{
                MsgBox1.Show("Ordem de Serviço", ex.Message, null,
                        new EO.Web.MsgBoxButton("OK"));                

			}

            if (Convert.ToInt32(rblTipoProcedimento.SelectedValue).Equals((int)TipoProcedimento.Instrutivo))
                KeepBackColorDisabledInstrutivo();
		}

		private void lstBoxAtualizacoes_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (lstBoxAtualizacoes.SelectedValue.Equals("0"))
			{
				wdtDataAtualizacao.Text = "";
				txtDescricaoAtualizacao.Text = string.Empty;
				ddlResponsavelAtualizacao.ClearSelection();
				ddlOperadorAtualizacao.ClearSelection();
				btnRemoverAtualizacao.Enabled = false;
			}
			else if (!lstBoxAtualizacoes.SelectedValue.Equals(string.Empty))
			{
				AtualizacaoProcedimento atualizacaoProcedimento = new AtualizacaoProcedimento(Convert.ToInt32(lstBoxAtualizacoes.SelectedValue));

                wdtDataAtualizacao.Text = atualizacaoProcedimento.Data.ToString("dd/MM/yyyy");
				txtDescricaoAtualizacao.Text = atualizacaoProcedimento.Descricao;
				ddlResponsavelAtualizacao.ClearSelection();
				ddlResponsavelAtualizacao.Items.FindByValue(atualizacaoProcedimento.IdResponsavel.Id.ToString()).Selected = true;
				ddlOperadorAtualizacao.ClearSelection();
				ddlOperadorAtualizacao.Items.FindByValue(atualizacaoProcedimento.IdOperador.Id.ToString()).Selected = true;

				btnRemoverAtualizacao.Enabled = true;
			}

            if (Convert.ToInt32(rblTipoProcedimento.SelectedValue).Equals((int)TipoProcedimento.Instrutivo))
                KeepBackColorDisabledInstrutivo();
		}

        protected void btnGravarAtualizacao_Click1(object sender, EventArgs e)
		{
			bool saveProcedimentoProcess = false;
			
			try
			{
				if (procedimento.Id == 0)
				{
					PopulaProcedimento();
					txtnPOPs.Text = procedimento.Numero.ToString("0000");

					btnDuplicarProc.Enabled = true;
					btnExcluir.Enabled = true;
                    lkbFichaPOPs.Enabled = true;
                    btnAPP.Enabled = true;

					lblTipoProcedimento.ForeColor = foreColorDisabledLabel;
					rblTipoProcedimento.Enabled = false;
				}

                saveProcedimentoProcess = true;
				
				AtualizacaoProcedimento atualizacaoProcedimento = new AtualizacaoProcedimento();
				string tipoCadastro = string.Empty;
				
				if (lstBoxAtualizacoes.SelectedValue.Equals("0"))
				{
					atualizacaoProcedimento.Inicialize();
					atualizacaoProcedimento.IdProcedimento = procedimento;

					tipoCadastro = "cadastrada";
				}
				else
				{
					atualizacaoProcedimento = new AtualizacaoProcedimento(Convert.ToInt32(lstBoxAtualizacoes.SelectedValue));

					tipoCadastro = "editada";
				}

				atualizacaoProcedimento.Data =  Convert.ToDateTime( wdtDataAtualizacao.Text );
				atualizacaoProcedimento.Descricao = txtDescricaoAtualizacao.Text.Trim();
				atualizacaoProcedimento.IdResponsavel.Id = Convert.ToInt32(ddlResponsavelAtualizacao.SelectedValue);
				atualizacaoProcedimento.IdOperador.Id = Convert.ToInt32(ddlOperadorAtualizacao.SelectedValue);

				atualizacaoProcedimento.UsuarioId = usuario.Id;
				atualizacaoProcedimento.Save();

				PopulaListBoxAtualizacoes(procedimento);
				lstBoxAtualizacoes.ClearSelection();
				lstBoxAtualizacoes.Items.FindByValue(atualizacaoProcedimento.Id.ToString()).Selected = true;
				btnRemoverAtualizacao.Enabled = true;

				ViewState["IdProcedimento"] = procedimento.Id;
				
				
                MsgBox1.Show("Ordem de Serviço", "A Atualização do Procedimento foi " + tipoCadastro + " com sucesso!", null,
                        new EO.Web.MsgBoxButton("OK"));                

			}
			catch (Exception ex)
			{
				if (!saveProcedimentoProcess)
                    MsgBox1.Show("Ordem de Serviço", "O Procedimento não foi salvo e a Atualização não foi gravada com sucesso! " + ex.Message, null,
                        new EO.Web.MsgBoxButton("OK"));                

				else
                    MsgBox1.Show("Ordem de Serviço", ex.Message, null,
                            new EO.Web.MsgBoxButton("OK"));                

			}

            if (Convert.ToInt32(rblTipoProcedimento.SelectedValue).Equals((int)TipoProcedimento.Instrutivo))
                KeepBackColorDisabledInstrutivo();
		}

        protected void btnRemoverAtualizacao_Click1(object sender, EventArgs e)
		{
			try
			{
				AtualizacaoProcedimento atualizacaoProcedimento = new AtualizacaoProcedimento(Convert.ToInt32(lstBoxAtualizacoes.SelectedValue));

				atualizacaoProcedimento.UsuarioId = usuario.Id;
				atualizacaoProcedimento.Delete();

				PopulaListBoxAtualizacoes(procedimento);
				btnRemoverAtualizacao.Enabled = false;


                MsgBox1.Show("Ordem de Serviço", "A Atualização selecionada foi excluída com sucesso!", null,
                        new EO.Web.MsgBoxButton("OK"));                

			}
			catch (Exception ex)
			{
                MsgBox1.Show("Ordem de Serviço", ex.Message, null,
                        new EO.Web.MsgBoxButton("OK"));                

			}

            if (Convert.ToInt32(rblTipoProcedimento.SelectedValue).Equals((int)TipoProcedimento.Instrutivo))
                KeepBackColorDisabledInstrutivo();
		}

		protected void btnDuplicarProc_Click(object sender, System.EventArgs e)
		{
			try
			{
				Procedimento newProcedimento = procedimento.GetNewProcedimento(usuario.Id);
				ViewState["IdProcedimento"] = null;
			
				StringBuilder st = new StringBuilder();

				st.Append("window.alert(\"O Procedimento foi duplicado com succeso! Novo nPOPs do Procedimento: " + newProcedimento.Numero.ToString("0000") + "\");");
				st.Append("window.location.href='CadProcedimento.aspx?IdProcedimento=" + newProcedimento.Id + "&IdEmpresa=" + lbl_Id_Empresa.Text + "&IdUsuario=" + lbl_Id_Usuario.Text + "';");

                this.ClientScript.RegisterStartupScript(this.GetType(), "DuplicaProcedimento", st.ToString(), true);
			}
			catch(Exception ex)
			{
                MsgBox1.Show("Ordem de Serviço", ex.Message, null,
                        new EO.Web.MsgBoxButton("OK"));                

			}
		}

        protected void imbAddPerigo_Click(object sender, ImageClickEventArgs e)
        {
            IDbTransaction transaction = new OperacaoPerigo().GetTransaction();

            using (transaction)
            {
                try
                {
                    if (lsbPerigos.SelectedItem == null)
                        throw new Exception("É necessário selecionar pelo menos 1 Perigo para ser adicionado à Operação selecionada!");

                    bool checkProcess = false;

                    foreach (ListItem itemPerigo in lsbPerigos.Items)
                        if (itemPerigo.Selected)
                        {
                            OperacaoPerigo operacaoPerigo = new OperacaoPerigo();
                            operacaoPerigo.Inicialize();

                            operacaoPerigo.IdOperacao.Id = Convert.ToInt32(lstBoxOperacoes.SelectedValue);
                            operacaoPerigo.IdPerigo.Id = Convert.ToInt32(itemPerigo.Value);

                            if (!checkProcess)
                            {
                                checkProcess = true;
                                operacaoPerigo.UsuarioProcessoRealizado = "Cadastro de Perigo(s) à uma Operação";
                            }

                            //operacaoPerigo.Precaucoes = txtPrecaucoes.Text.Trim();

                            operacaoPerigo.UsuarioId = usuario.Id;
                            operacaoPerigo.Transaction = transaction;
                            operacaoPerigo.Save();

                            DataSet dsRiscos = new PerigoRiscoAcidente().Get("IdPerigo=" + itemPerigo.Value);
                            
                            foreach (DataRow rowRisco in dsRiscos.Tables[0].Select())
                            {
                                OperacaoPerigoRiscoAcidente operacaoPerigoRisco = new OperacaoPerigoRiscoAcidente();
                                operacaoPerigoRisco.Inicialize();

                                operacaoPerigoRisco.IdOperacaoPerigo = operacaoPerigo;
                                operacaoPerigoRisco.IdRiscoAcidente.Id = Convert.ToInt32(rowRisco["IdRiscoAcidente"]);

                                operacaoPerigoRisco.UsuarioId = usuario.Id;
                                operacaoPerigoRisco.Transaction = transaction;
                                operacaoPerigoRisco.Save();
                            }
                        }

                    transaction.Commit();

                    PopulaLsbPerigos(Convert.ToInt32(lstBoxOperacoes.SelectedValue));
                    PopulaLsbDDLPerigosProc(Convert.ToInt32(lstBoxOperacoes.SelectedValue));


                    MsgBox1.Show("Ordem de Serviço", "Os Perigos foram adicionados com sucesso à Operação selecionada!", null,
                            new EO.Web.MsgBoxButton("OK"));                

                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MsgBox1.Show("Ordem de Serviço", ex.Message, null,
                            new EO.Web.MsgBoxButton("OK"));                

                }
            }
        }

        protected void imbRemovePerigo_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (lsbPerigoSelected.SelectedItem == null)
                    throw new Exception("É necessário selecionar pelo menos 1 Perigo para ser removido da Operação selecionada!");

                bool checkProcess = false;
                
                foreach (ListItem itemPerigoSel in lsbPerigoSelected.Items)
                    if (itemPerigoSel.Selected)
                    {
                        OperacaoPerigo operacaoPerigo = new OperacaoPerigo();
                        operacaoPerigo.Find("IdOperacao=" + lstBoxOperacoes.SelectedValue
                            + " AND IdPerigo=" + itemPerigoSel.Value);

                        if (!checkProcess)
                        {
                            checkProcess = true;
                            operacaoPerigo.UsuarioProcessoRealizado = "Exclusão de Perigo(s) de uma Operação";
                        }

                        operacaoPerigo.UsuarioId = usuario.Id;
                        operacaoPerigo.Delete();
                    }

                PopulaLsbPerigos(Convert.ToInt32(lstBoxOperacoes.SelectedValue));
                PopulaLsbDDLPerigosProc(Convert.ToInt32(lstBoxOperacoes.SelectedValue));

                txtPrecaucoes.Text = "";


                MsgBox1.Show("Ordem de Serviço", "Os Perigos foram removidos com sucesso da Operação selecionada!", null,
                        new EO.Web.MsgBoxButton("OK"));                

            }
            catch (Exception ex)
            {
                MsgBox1.Show("Ordem de Serviço", ex.Message, null,
                        new EO.Web.MsgBoxButton("OK"));                

            }
        }

        protected void ddlPerigo_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulaLsbRiscosPerigo(ddlPerigo.Items.Count);
        }

        protected void btnExcluirRisco_Click1(object sender, EventArgs e)
        {
            try
            {
                if (ddlPerigo.Items.Count.Equals(0))
                {
                    OperacaoRiscoAcidente operacaoRisco = new OperacaoRiscoAcidente(Convert.ToInt32(listBxRiscosOperacao.SelectedValue));

                    operacaoRisco.UsuarioId = usuario.Id;
                    operacaoRisco.UsuarioProcessoRealizado = "Exclusão do Risco no Procedimento";
                    operacaoRisco.Delete();
                }
                else
                {
                    OperacaoPerigoRiscoAcidente operacaoPerigoRisco = new OperacaoPerigoRiscoAcidente(Convert.ToInt32(listBxRiscosOperacao.SelectedValue));

                    operacaoPerigoRisco.UsuarioId = usuario.Id;
                    operacaoPerigoRisco.UsuarioProcessoRealizado = "Exclusão do Risco associado ao Perigo no Procedimento";
                    operacaoPerigoRisco.Delete();
                }

                PopulaLsbRiscosPerigo(ddlPerigo.Items.Count);


                MsgBox1.Show("Ordem de Serviço", "O Risco selecionado foi excluído com sucesso!", null,
                        new EO.Web.MsgBoxButton("OK"));                

            }
            catch (Exception ex)
            {
                MsgBox1.Show("Ordem de Serviço", ex.Message, null,
                        new EO.Web.MsgBoxButton("OK"));                

            }
        }

        protected void imbAddAspecto_Click(object sender, ImageClickEventArgs e)
        {
            IDbTransaction transaction = new OperacaoAspectoAmbiental().GetTransaction();

            using (transaction)
            {
                try
                {
                    if (lsbAspectos.SelectedItem == null)
                        throw new Exception("É necessário selecionar pelo menos 1 Aspecto Ambiental para ser adicionado à Operação selecionada!");

                    bool checkProcess = false;

                    foreach (ListItem itemAspecto in lsbAspectos.Items)
                        if (itemAspecto.Selected)
                        {
                            OperacaoAspectoAmbiental operacaoAspecto = new OperacaoAspectoAmbiental();
                            operacaoAspecto.Inicialize();

                            operacaoAspecto.IdOperacao.Id = Convert.ToInt32(lstBoxOperacoes.SelectedValue);
                            operacaoAspecto.IdAspectoAmbiental.Id = Convert.ToInt32(itemAspecto.Value);

                            if (!checkProcess)
                            {
                                checkProcess = true;
                                operacaoAspecto.UsuarioProcessoRealizado = "Cadastro de Aspectos Ambientais à uma Operação";
                            }

                            operacaoAspecto.UsuarioId = usuario.Id;
                            operacaoAspecto.Transaction = transaction;
                            operacaoAspecto.Save();

                            DataSet dsImpactos = new AspectoImpacto().Get("IdAspectoAmbiental=" + itemAspecto.Value);

                            foreach (DataRow rowAspecto in dsImpactos.Tables[0].Select())
                            {
                                OperacaoAspectoAmbientalImpacto operacaoAspectoImpacto = new OperacaoAspectoAmbientalImpacto();
                                operacaoAspectoImpacto.Inicialize();

                                operacaoAspectoImpacto.IdOperacaoAspectoAmbiental = operacaoAspecto;
                                operacaoAspectoImpacto.IdImpactoAmbiental.Id = Convert.ToInt32(rowAspecto["IdImpactoAmbiental"]);

                                operacaoAspectoImpacto.UsuarioId = usuario.Id;
                                operacaoAspectoImpacto.Transaction = transaction;
                                operacaoAspectoImpacto.Save();
                            }
                        }

                    transaction.Commit();

                    PopulaLsbAspectos(Convert.ToInt32(lstBoxOperacoes.SelectedValue));
                    PopulaLsbDDLAspectosProc(Convert.ToInt32(lstBoxOperacoes.SelectedValue));


                    MsgBox1.Show("Ordem de Serviço", "Os Aspectos Ambientais foram adicionados com sucesso à Operação selecionada!", null,
                            new EO.Web.MsgBoxButton("OK"));                

                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MsgBox1.Show("Ordem de Serviço", ex.Message, null,
                            new EO.Web.MsgBoxButton("OK"));                

                }
            }
        }

        protected void imbRemoveAspecto_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (lsbAspectoSelected.SelectedItem == null)
                    throw new Exception("É necessário selecionar pelo menos 1 Aspecto Ambiental para ser removido da Operação selecionada!");

                bool checkProcess = false;

                foreach (ListItem itemAspectoSel in lsbAspectoSelected.Items)
                    if (itemAspectoSel.Selected)
                    {
                        OperacaoAspectoAmbiental operacaoAspecto = new OperacaoAspectoAmbiental();
                        operacaoAspecto.Find("IdOperacao=" + lstBoxOperacoes.SelectedValue
                            + " AND IdAspectoAmbiental=" + itemAspectoSel.Value);

                        if (!checkProcess)
                        {
                            checkProcess = true;
                            operacaoAspecto.UsuarioProcessoRealizado = "Exclusão de Aspectos Ambientais de uma Operação";
                        }

                        operacaoAspecto.UsuarioId = usuario.Id;
                        operacaoAspecto.Delete();
                    }

                PopulaLsbAspectos(Convert.ToInt32(lstBoxOperacoes.SelectedValue));
                PopulaLsbDDLAspectosProc(Convert.ToInt32(lstBoxOperacoes.SelectedValue));


                MsgBox1.Show("Ordem de Serviço", "Os Aspectos Ambientais foram removidos com sucesso da Operação selecionada!", null,
                        new EO.Web.MsgBoxButton("OK"));                

            }
            catch (Exception ex)
            {
                MsgBox1.Show("Ordem de Serviço", ex.Message, null,
                        new EO.Web.MsgBoxButton("OK"));                

            }
        }

        protected void ddlAspecto_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulaLsbImpactoAspecto();
        }

        protected void lsbImpactosSelected_SelectedIndexChanged(object sender, EventArgs e)
        {
            rblSeveridadeImpacto.ClearSelection();
            rblProbabilidadeImpacto.ClearSelection();

            OperacaoAspectoAmbientalImpacto operacaoAspectoImpacto = new OperacaoAspectoAmbientalImpacto(Convert.ToInt32(lsbImpactosSelected.SelectedValue));

            if (!Convert.ToInt32(operacaoAspectoImpacto.IndGrauSeveridadeImpacto).ToString().Equals("0"))
                rblSeveridadeImpacto.Items.FindByValue(Convert.ToInt32(operacaoAspectoImpacto.IndGrauSeveridadeImpacto).ToString()).Selected = true;
            if (!Convert.ToInt32(operacaoAspectoImpacto.IndProbabilidadeImpacto).ToString().Equals("0"))
                rblProbabilidadeImpacto.Items.FindByValue(Convert.ToInt32(operacaoAspectoImpacto.IndProbabilidadeImpacto).ToString()).Selected = true;

            btnExcluirImpacto.Enabled = true;
            btnGravarDetalhesImpacto.Enabled = true;
        }

        protected void btnExcluirImpacto_Click(object sender, EventArgs e)
        {
            try
            {
                OperacaoAspectoAmbientalImpacto operacaoAspectoImpacto = new OperacaoAspectoAmbientalImpacto(Convert.ToInt32(lsbImpactosSelected.SelectedValue));

                operacaoAspectoImpacto.UsuarioId = usuario.Id;
                operacaoAspectoImpacto.UsuarioProcessoRealizado = "Exclusão do Impacto Ambiental associado ao Aspecto Ambiental no Procedimento";
                operacaoAspectoImpacto.Delete();

                PopulaLsbImpactoAspecto();


                MsgBox1.Show("Ordem de Serviço", "O Impacto Ambiental selecionado foi excluído com sucesso!", null,
                        new EO.Web.MsgBoxButton("OK"));                

            }
            catch (Exception ex)
            {
                MsgBox1.Show("Ordem de Serviço", ex.Message, null,
                        new EO.Web.MsgBoxButton("OK"));                

            }
        }

        protected void btnGravarDetalhesImpacto_Click(object sender, EventArgs e)
        {
            try
            {
                OperacaoAspectoAmbientalImpacto operacaoAspectoImpacto = new OperacaoAspectoAmbientalImpacto(Convert.ToInt32(lsbImpactosSelected.SelectedValue));

                if (rblSeveridadeImpacto.SelectedItem != null)
                    switch (Convert.ToInt32(rblSeveridadeImpacto.SelectedValue))
                    {
                        case (int)GrauSeveridadeImpacto.Benefico:
                            operacaoAspectoImpacto.IndGrauSeveridadeImpacto = GrauSeveridadeImpacto.Benefico;
                            break;
                        case (int)GrauSeveridadeImpacto.AdversoMedio:
                            operacaoAspectoImpacto.IndGrauSeveridadeImpacto = GrauSeveridadeImpacto.AdversoMedio;
                            break;
                        case (int)GrauSeveridadeImpacto.AdversoAlto:
                            operacaoAspectoImpacto.IndGrauSeveridadeImpacto = GrauSeveridadeImpacto.AdversoAlto;
                            break;
                    }

                if (rblProbabilidadeImpacto.SelectedItem != null)
                    switch (Convert.ToInt32(rblProbabilidadeImpacto.SelectedValue))
                    {
                        case (int)ProbabilidadeImpacto.Superior1Ano:
                            operacaoAspectoImpacto.IndProbabilidadeImpacto = ProbabilidadeImpacto.Superior1Ano;
                            break;
                        case (int)ProbabilidadeImpacto.Superior1Mes:
                            operacaoAspectoImpacto.IndProbabilidadeImpacto = ProbabilidadeImpacto.Superior1Mes;
                            break;
                        case (int)ProbabilidadeImpacto.Inferior1Mes:
                            operacaoAspectoImpacto.IndProbabilidadeImpacto = ProbabilidadeImpacto.Inferior1Mes;
                            break;
                    }

                operacaoAspectoImpacto.UsuarioProcessoRealizado = "Edição dos detalhes do Impacto Ambiental no Procedimento";
                operacaoAspectoImpacto.UsuarioId = usuario.Id;
                operacaoAspectoImpacto.Save();


                MsgBox1.Show("Ordem de Serviço", "Os detalhes do Impacto Ambiental selecionado foram atualizados com sucesso!", null,
                        new EO.Web.MsgBoxButton("OK"));                

            }
            catch (Exception ex)
            {
                MsgBox1.Show("Ordem de Serviço", ex.Message, null,
                        new EO.Web.MsgBoxButton("OK"));                

            }
        }

        protected void btnAPP_Click(object sender, EventArgs e)
        {
            //StringBuilder sb = new StringBuilder();

            //sb.Append("window.location.href='CadAPR.aspx?IdUsuario=" + lbl_Id_Usuario.Text + "&IdEmpresa=" + lbl_Id_Empresa.Text + "&IdProcedimento=" + procedimento.Id + "';");

            //this.ClientScript.RegisterStartupScript(this.GetType(), "CadAPR", sb.ToString(), true);
            Response.Redirect("CadAPR.aspx?IdUsuario=" + lbl_Id_Usuario.Text + "&IdEmpresa=" + lbl_Id_Empresa.Text + "&IdProcedimento=" + procedimento.Id);
        }

        protected void lkbClear_Click(object sender, EventArgs e)
        {
            ViewState["myData"] = null;
            ViewState["Filename"] = null;

            txtSelectedFile.Text = string.Empty;
            txt_Arq.Text = "";
            Session["zFoto"] = null;
            
        }

        protected void lkbFichaPOPs_Click(object sender, EventArgs e)
        {
            Guid strAux = Guid.NewGuid();

            OpenReport("OrdemDeServico", "RptProcedimento.aspx?IliteraSystem=" + strAux.ToString() + strAux.ToString()
                + "&IdProcedimento=" + procedimento.Id + "&Impressao=H"  + "&IdUsuario=" + lbl_Id_Usuario.Text + "&IdEmpresa=" + lbl_Id_Empresa.Text, "RptProcedimento");
        }

        protected void lkbFichaPOPsV_Click(object sender, EventArgs e)
        {
            Guid strAux = Guid.NewGuid();

            OpenReport("OrdemDeServico", "RptProcedimento.aspx?IliteraSystem=" + strAux.ToString() + strAux.ToString()
                + "&IdProcedimento=" + procedimento.Id + "&Impressao=V" + "&IdUsuario=" + lbl_Id_Usuario.Text + "&IdEmpresa=" + lbl_Id_Empresa.Text, "RptProcedimento");
        }

        protected void Button1_Click(object sender, EventArgs e)
        {

            if (searchFoto.FileName.ToString().Trim() != "")
            {

                string xExtension = searchFoto.FileName.Substring(searchFoto.FileName.Length - 3, 3).ToUpper().Trim();

                if (xExtension == "PDF" || xExtension == "JPG")
                {

                    Session["zFoto"] = searchFoto.FileBytes;
                    txt_Arq.Text = searchFoto.FileName.ToString();
                }
            }
            

        }



        protected void OpenReport(string directory, string fileAndQuery, string ReportName)
        {
            this.OpenReport(directory, fileAndQuery, ReportName, true);
        }

        protected void OpenReport(string directory, string fileAndQuery, string ReportName, bool useDirectoryForLocalProcess)
        {
            StringBuilder st = new StringBuilder();

            Guid strAux = Guid.NewGuid();

            string valueProcess = "Local";

            if (valueProcess.Equals("Remote"))
                //st.Append("void(window.open('http://report.ilitera.net/index.aspx?Identity=" + strAux.ToString() + "opsa" + strAux.ToString()
                //    + "&Key=" + strAux.ToString() + "MestraNet" + strAux.ToString()
                //    + "&PathAndQuery=" + HttpUtility.UrlEncode(directory + "/" + fileAndQuery) + "', '" + ReportName + "', 'scrollbars=yes, resizable=yes, toolbar=no, addressbar=no, menubar=no, width=800px, height=600px'));");
                //;
                System.Diagnostics.Debug.WriteLine("");
            else if (valueProcess.Equals("Local"))
            {
                if (useDirectoryForLocalProcess)
                {
                    //st.Append("void(window.open('../" + directory + "/" + fileAndQuery + "', '" + ReportName + "', 'scrollbars=yes, resizable=yes, toolbar=no, addressbar=no, menubar=no, width=800px, height=600px'));");
                    //st.AppendFormat("void(window.open('../{0}/{1}', '{2}','scrollbars=yes, resizable=yes, toolbar=no, addressbar=no, menubar=no, width=800px, height=600px'));", directory, fileAndQuery, ReportName);
                    st.AppendFormat("void(window.open('{1}', '{2}','scrollbars=yes, resizable=yes, toolbar=no, addressbar=no, menubar=no, width=800px, height=600px'));", directory, fileAndQuery, ReportName);
                }
                else
                {
                    //st.Append("void(window.open('" + fileAndQuery + "', '" + ReportName + "', 'scrollbars=yes, resizable=yes, toolbar=no, addressbar=no, menubar=no, width=800px, height=600px'));");
                    st.AppendFormat("void(window.open('{0}','{1}', 'scrollbars=yes, resizable=yes, toolbar=no, addressbar=no, menubar=no, width=800px, height=600px'));", fileAndQuery, ReportName);
                }
            }

            ScriptManager.RegisterStartupScript(this, this.GetType(), String.Format("OpenReport{0}", ReportName), st.ToString(), true);
        }

        protected void tabbtnGravarOperacao_Click(object sender, EventArgs e)
        {

        }



        protected void tabbtnGravarAtualizacao_Click(object sender, EventArgs e)
        {

        }

        protected void tabbtnRemoverAtualizacao_Click(object sender, EventArgs e)
        {

        }

        protected void btnGravarOperacao_Click1(object sender, EventArgs e)
        {
            bool saveProcedimentoProcess = false;

            try
            {
                if (procedimento.Id == 0)
                {
                    PopulaProcedimento();
                    txtnPOPs.Text = procedimento.Numero.ToString("0000");

                    if (Convert.ToInt32(rblTipoProcedimento.SelectedValue) != (int)TipoProcedimento.Especifico)
                        btnDuplicarProc.Enabled = true;
                    btnExcluir.Enabled = true;
                    lkbFichaPOPs.Enabled = true;
                    btnAPP.Enabled = true;

                    lblTipoProcedimento.ForeColor = foreColorDisabledLabel;
                    rblTipoProcedimento.Enabled = false;
                    saveProcedimentoProcess = true;
                }
                else
                    saveProcedimentoProcess = true;

                string tipoProcess = string.Empty;
                Operacao operacao = new Operacao();

                if (lstBoxOperacoes.SelectedValue.Equals("0"))
                {
                    tipoProcess = "cadastrada";
                    operacao.Inicialize();
                    operacao.IdProcedimento = procedimento;
                }
                else
                {
                    tipoProcess = "editada";
                    operacao.Find(Convert.ToInt32(lstBoxOperacoes.SelectedValue));
                }

                operacao.Descricao = txtOperacao.Text.Trim();
                operacao.Precaucoes = txtObsQualidade.Text.Trim();

                operacao.UsuarioId = usuario.Id;
                operacao.Save();

                PopulaListBoxOperacoes(procedimento);
                lstBoxOperacoes.ClearSelection();
                lstBoxOperacoes.Items.FindByValue(operacao.Id.ToString()).Selected = true;

                btnExcluirOpe.Enabled = true;

                ViewState["IdProcedimento"] = procedimento.Id;

                MsgBox1.Show("Ordem de Serviço", "A Operação foi " + tipoProcess + " com sucesso!", null,
                        new EO.Web.MsgBoxButton("OK"));


                if (tipoProcess.Equals("cadastrada") && procedimento.IndTipoProcedimento.Equals(TipoProcedimento.Resumo))
                {
                    HabilitaWebFormsOperacao();

                    PopulaLsbPerigos(operacao.Id);
                    PopulaLsbAspectos(operacao.Id);
                }
            }
            catch (Exception ex)
            {
                if (!saveProcedimentoProcess)
                    MsgBox1.Show("Ordem de Serviço", "O Procedimento não foi salvo e a Operação não foi gravada com sucesso! " + ex.Message, null,
                        new EO.Web.MsgBoxButton("OK"));

                else
                    MsgBox1.Show("Ordem de Serviço", ex.Message, null,
                            new EO.Web.MsgBoxButton("OK"));

            }

            if (Convert.ToInt32(rblTipoProcedimento.SelectedValue).Equals((int)TipoProcedimento.Instrutivo))
                KeepBackColorDisabledInstrutivo();

        }

        protected void lsbPerigoSelected_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (lsbPerigoSelected.SelectedIndex >= 0)
            {
                if (System.Convert.ToInt32(lsbPerigoSelected.SelectedValue.ToString()) == 0 || System.Convert.ToInt32(lstBoxOperacoes.SelectedValue.ToString()) == 0)
                {
                    txtPrecaucoes.Text = "";
                    return;
                }

                OperacaoPerigo zOP = new OperacaoPerigo();

                zOP.Find( "IdPerigo = " + lsbPerigoSelected.SelectedValue.ToString() + " and IdOperacao = "  + lstBoxOperacoes.SelectedValue.ToString());

                txtPrecaucoes.Text = zOP.Precaucoes;
            }
            else
            {
                txtPrecaucoes.Text = "";
            }

        }

        protected void cmd_Salvar_Precaucoes_Click(object sender, EventArgs e)
        {

            if (lsbPerigoSelected.SelectedIndex >= 0)
            {
                if (System.Convert.ToInt32(lsbPerigoSelected.SelectedValue.ToString()) == 0 || System.Convert.ToInt32(lstBoxOperacoes.SelectedValue.ToString()) == 0)
                {
                    txtPrecaucoes.Text = "";
                    return;
                }

                OperacaoPerigo zOP = new OperacaoPerigo();

                zOP.Find("IdPerigo = " + lsbPerigoSelected.SelectedValue.ToString() + " and IdOperacao = " + lstBoxOperacoes.SelectedValue.ToString());


                zOP.Precaucoes = txtPrecaucoes.Text;
                zOP.Save();
            }
            else
            {
                txtPrecaucoes.Text = "";
            }
        }








        
    }
}
