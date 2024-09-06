using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Ilitera.Opsa.Data;
using System.Data.SqlClient;
using System.Text;
using Ilitera.Common;

namespace Ilitera.Net.OrdemDeServico
{
    public partial class CadAPR : System.Web.UI.Page
    {
        private Procedimento procedimento;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            InicializaWebPageObjects();

            Entities.Usuario user = (Entities.Usuario)Session["usuarioLogado"];

            int xIdEmpresa = System.Convert.ToInt32(Session["Empresa"]);
            int xIdUsuario = user.IdUsuario;

            lbl_Id_Empresa.Text = xIdEmpresa.ToString().Trim();
            lbl_Id_Usuario.Text = xIdUsuario.ToString().Trim();

            if (!IsPostBack)
            {
                lblTitle.Text = "Configuração dos dados da APP no Procedimento nPOPs " + procedimento.Numero.ToString("0000");
                PopulaLsbOperacao();
                PopulaDDLResponsaveis();
                wdtDataEleboracao.Text = procedimento.DataElaboracaoAPP.ToString("dd/MM/yyyy");
                wdtDataRevisao.Text = procedimento.DataRevisaoAPP.ToString("dd/MM/yyyy");

                if (ddlRespElaboracao.Items.FindByValue(procedimento.IdElaboradorAPP.Id.ToString()) != null)
                    ddlRespElaboracao.Items.FindByValue(procedimento.IdElaboradorAPP.Id.ToString()).Selected = true;

                if (ddlRespRevisao.Items.FindByValue(procedimento.IdRevisorAPP.Id.ToString()) != null)
                    ddlRespRevisao.Items.FindByValue(procedimento.IdRevisorAPP.Id.ToString()).Selected = true;              
            }
            else
            {
                if (txtAuxiliar.Value.Equals("atualizaDanos"))
                {                    
                        PopulaLsbDano();
                }
                else if (txtAuxiliar.Value.Equals("atualizaColaborador"))
                {
                    PopulaDDLResponsaveis();

                    if (ddlRespElaboracao.Items.FindByValue(procedimento.IdElaboradorAPP.Id.ToString()) != null)
                        ddlRespElaboracao.Items.FindByValue(procedimento.IdElaboradorAPP.Id.ToString()).Selected = true;

                    if (ddlRespRevisao.Items.FindByValue(procedimento.IdRevisorAPP.Id.ToString()) != null)
                        ddlRespRevisao.Items.FindByValue(procedimento.IdRevisorAPP.Id.ToString()).Selected = true;        
                }

                txtAuxiliar.Value = string.Empty;
            }
        }




        protected  void InicializaWebPageObjects()
        {
            //base.InicializaWebPageObjects();

            procedimento = new Procedimento(Convert.ToInt32(Request.QueryString["IdProcedimento"]));
        }

        private void PopulaDDLResponsaveis()
        {
            Cliente xCliente;
            xCliente = new Cliente();
            xCliente.Inicialize();
            xCliente.Find(System.Convert.ToInt32(lbl_Id_Empresa.Text));

            ArrayList alPrestadores = new Prestador().GetListaPrestador(xCliente, false, (int)TipoPrestador.ContatoEmpresa);
            alPrestadores.Sort();

            ddlRespElaboracao.DataSource = alPrestadores;
            ddlRespElaboracao.DataValueField = "Id";
            ddlRespElaboracao.DataTextField = "NomeCompleto";
            ddlRespElaboracao.DataBind();

            ddlRespRevisao.DataSource = alPrestadores;
            ddlRespRevisao.DataValueField = "Id";
            ddlRespRevisao.DataTextField = "NomeCompleto";
            ddlRespRevisao.DataBind();

            ddlRespElaboracao.Items.Insert(0, new ListItem("Selecione o Elaborador...", "0"));
            ddlRespRevisao.Items.Insert(0, new ListItem("Selecione o Revisor...", "0"));
        }

        private void PopulaLsbOperacao()
        {
            DataSet dsOperacoes = new Operacao().Get("IdProcedimento=" + procedimento.Id + " ORDER BY Sequencia");

            foreach (DataRow rowOperacao in dsOperacoes.Tables[0].Select())
                lsbOperacao.Items.Add(new ListItem(((short)rowOperacao["Sequencia"]).ToString("00") + " - " + rowOperacao["Descricao"],
                    rowOperacao["IdOperacao"].ToString()));
        }

        protected void lsbOperacao_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (isEnabledImpactoTab() || isEnabledRiscoTab())
              //  DesabilitaAllTabs();
            
            lsbRisco.Items.Clear();
            lsbImpacto.Items.Clear();

            PopulaLsbPerigo();
            PopulaLsbAspecto();
        }

        private void PopulaLsbPerigo()
        {
            lsbPerigo.DataSource = new Perigo().GetIdNome("Nome", "IdPerigo IN (SELECT IdPerigo FROM OperacaoPerigo WHERE IdOperacao="
                + lsbOperacao.SelectedValue + ")");
            lsbPerigo.DataValueField = "Id";
            lsbPerigo.DataTextField = "Nome";
            lsbPerigo.DataBind();
        }

        protected void lsbPerigo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isEnabledRiscoTab())
                DesabilitaRiscoTab();

            PopulaLsbRisco();
        }

        private void PopulaLsbRisco()
        {
            lsbRisco.DataSource = new RiscoAcidente().GetIdNome("Nome", "IdRiscoAcidente IN (SELECT IdRiscoAcidente FROM OperacaoPerigoRiscoAcidente WHERE"
                + " IdOperacaoPerigo IN (SELECT IdOperacaoPerigo FROM OperacaoPerigo WHERE IdOperacao=" + lsbOperacao.SelectedValue
                + " AND IdPerigo=" + lsbPerigo.SelectedValue + "))");
            lsbRisco.DataValueField = "Id";
            lsbRisco.DataTextField = "Nome";
            lsbRisco.DataBind();
        }

        private void DesabilitaAllTabs()
        {
            //UltraWebTabAPR.Tabs.GetTab(0).Enabled = false;
            //UltraWebTabAPR.Tabs.GetTab(1).Enabled = false;
            //UltraWebTabAPR.Tabs.GetTab(2).Enabled = false;
        }

        private void HabilitaImpactoTab()
        {
            //UltraWebTabAPR.Tabs.GetTab(2).Enabled = true;
            //UltraWebTabAPR.SelectedTab = 2;
        }

        private void HabilitaRiscoTab()
        {
            //UltraWebTabAPR.Tabs.GetTab(0).Enabled = true;
            //UltraWebTabAPR.Tabs.GetTab(1).Enabled = true;
            //UltraWebTabAPR.SelectedTab = 0;
        }

        private void DesabilitaImpactoTab()
        {
            //UltraWebTabAPR.Tabs.GetTab(2).Enabled = false;

            //if (isEnabledRiscoTab())
            //    UltraWebTabAPR.SelectedTab = 0;
        }

        private void DesabilitaRiscoTab()
        {
            //UltraWebTabAPR.Tabs.GetTab(0).Enabled = false;
            //UltraWebTabAPR.Tabs.GetTab(1).Enabled = false;
        }

        private bool isEnabledImpactoTab()
        {
            //return UltraWebTabAPR.Tabs.GetTab(2).Enabled;
            return true;
        }

        private bool isEnabledRiscoTab()
        {
            //return (UltraWebTabAPR.Tabs.GetTab(0).Enabled && UltraWebTabAPR.Tabs.GetTab(1).Enabled);
            return true;
        }

        protected void lsbRisco_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isEnabledRiscoTab())
                HabilitaRiscoTab();

            PopulaLsbDano();
            PopulaLsbEpi();
            PopulaTelaControleRisco();
        }

        private void PopulaTelaControleRisco()
        {
            OperacaoPerigoRiscoAcidente operacaoPerigoRiscoAcidente = new OperacaoPerigoRiscoAcidente();
            operacaoPerigoRiscoAcidente.Find("IdRiscoAcidente=" + lsbRisco.SelectedValue
                + " AND IdOperacaoPerigo IN (SELECT IdOperacaoPerigo FROM OperacaoPerigo WHERE IdOperacao=" + lsbOperacao.SelectedValue
                + " AND IdPerigo=" + lsbPerigo.SelectedValue + ")");

            txtEPC.Text = operacaoPerigoRiscoAcidente.Epc;
            txtMedAdm.Text = operacaoPerigoRiscoAcidente.MedidasAdm;
            txtMedEdu.Text = operacaoPerigoRiscoAcidente.MedidasEdu;
        }

        private void PopulaLsbDano()
        {
            lsbDanos.DataSource = new Dano().GetIdNome("Nome", "IdCliente=" + lbl_Id_Empresa.Text
                + " AND IdDano NOT IN (SELECT IdDano FROM OperacaoPerigoRiscoDano WHERE"
                + " IdOperacaoPerigoRiscoAcidente IN (SELECT IdOperacaoPerigoRiscoAcidente FROM OperacaoPerigoRiscoAcidente WHERE"
                + " IdRiscoAcidente=" + lsbRisco.SelectedValue
                + " AND IdOperacaoPerigo IN (SELECT IdOperacaoPerigo FROM OperacaoPerigo WHERE IdOperacao=" + lsbOperacao.SelectedValue
                + " AND IdPerigo=" + lsbPerigo.SelectedValue + ")))");
            lsbDanos.DataValueField = "Id";
            lsbDanos.DataTextField = "Nome";
            lsbDanos.DataBind();

            PopulaLsbDanoSelected();
        }

        private void PopulaLsbDanoSelected()
        {
            lsbDanosSelected.Items.Clear();

            ArrayList alDanosSelected = new OperacaoPerigoRiscoDano().Find("IdOperacaoPerigoRiscoAcidente IN (SELECT IdOperacaoPerigoRiscoAcidente FROM OperacaoPerigoRiscoAcidente WHERE"
                + " IdRiscoAcidente=" + lsbRisco.SelectedValue
                + " AND IdOperacaoPerigo IN (SELECT IdOperacaoPerigo FROM OperacaoPerigo WHERE IdOperacao=" + lsbOperacao.SelectedValue
                + " AND IdPerigo=" + lsbPerigo.SelectedValue + "))"
                + " ORDER BY (SELECT Nome FROM Dano WHERE Dano.IdDano=OperacaoPerigoRiscoDano.IdDano)");

            foreach (OperacaoPerigoRiscoDano operacaoPerigoRiscoDano in alDanosSelected)
            {
                operacaoPerigoRiscoDano.IdDano.Find();
                lsbDanosSelected.Items.Add(new ListItem(operacaoPerigoRiscoDano.IdDano.Nome, operacaoPerigoRiscoDano.Id.ToString()));
            }
        }

        protected void imbAddDano_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (lsbDanos.SelectedItem == null)
                    throw new Exception("É necessário selecionar os Danos que deseja associar!");

                OperacaoPerigoRiscoAcidente operacaoPerigoRisco = new OperacaoPerigoRiscoAcidente();
                operacaoPerigoRisco.Find("IdRiscoAcidente=" + lsbRisco.SelectedValue
                    + " AND IdOperacaoPerigo IN (SELECT IdOperacaoPerigo FROM OperacaoPerigo WHERE IdOperacao=" + lsbOperacao.SelectedValue
                    + " AND IdPerigo=" + lsbPerigo.SelectedValue + ")");

                bool checkProcess = false;

                foreach (ListItem itemDano in lsbDanos.Items)
                    if (itemDano.Selected)
                    {
                        OperacaoPerigoRiscoDano operacaoPerigoRiscoDano = new OperacaoPerigoRiscoDano();
                        operacaoPerigoRiscoDano.Inicialize();

                        operacaoPerigoRiscoDano.IdOperacaoPerigoRiscoAcidente = operacaoPerigoRisco;
                        operacaoPerigoRiscoDano.IdDano.Id = Convert.ToInt32(itemDano.Value);

                        if (!checkProcess)
                        {
                            checkProcess = true;
                            operacaoPerigoRiscoDano.UsuarioProcessoRealizado = "Associação de Danos ao Risco do Procedimento";
                            operacaoPerigoRiscoDano.UsuarioId = System.Convert.ToInt32( lbl_Id_Usuario.Text);
                        }

                        operacaoPerigoRiscoDano.Save();
                    }

                PopulaLsbDano();

                
                MsgBox1.Show("Ordem de Serviço", "Os Danos selecionados foram associados com sucesso ao Risco do Procedimento!", null,
                                            new EO.Web.MsgBoxButton("OK"));                
            }
            catch (Exception ex)
            {

                MsgBox1.Show("Ordem de Serviço", ex.Message, null,
                                            new EO.Web.MsgBoxButton("OK"));                
                
            }
        }

        protected void imbRemoveDano_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (lsbDanosSelected.SelectedItem == null)
                    throw new Exception("É necessário selecionar os Danos que deseja desassociar!");
                
                bool checkProcess = false;

                foreach (ListItem itemDanoSelected in lsbDanosSelected.Items)
                    if (itemDanoSelected.Selected)
                    {
                        OperacaoPerigoRiscoDano operacaoPerigoRiscoDano = new OperacaoPerigoRiscoDano(Convert.ToInt32(itemDanoSelected.Value));

                        if (!checkProcess)
                        {
                            checkProcess = true;
                            operacaoPerigoRiscoDano.UsuarioProcessoRealizado = "Desassociação de Danos do Risco do Procedimento";
                            operacaoPerigoRiscoDano.UsuarioId = System.Convert.ToInt32( lbl_Id_Usuario.Text);
                        }

                        operacaoPerigoRiscoDano.Delete();
                    }

                PopulaLsbDano();

                MsgBox1.Show("Ordem de Serviço", "Os Danos selecionados foram desassociados com sucesso do Risco do Procedimento!", null,
                                            new EO.Web.MsgBoxButton("OK"));                

            }
            catch (Exception ex)
            {                
                MsgBox1.Show("Ordem de Serviço", ex.Message, null,
                                            new EO.Web.MsgBoxButton("OK"));                
            }
        }

        private void PopulaLsbEpi()
        {
            lsbEPI.DataSource = new Epi().GetIdNome("Descricao", "IdEPI NOT IN (SELECT IdEPI FROM OperacaoPerigoRiscoEPI WHERE"
                + " IdOperacaoPerigoRiscoAcidente IN (SELECT IdOperacaoPerigoRiscoAcidente FROM OperacaoPerigoRiscoAcidente WHERE"
                + " IdRiscoAcidente=" + lsbRisco.SelectedValue + " AND IdOperacaoPerigo IN (SELECT IdOperacaoPerigo FROM OperacaoPerigo WHERE"
                + " IdOperacao=" + lsbOperacao.SelectedValue + " AND IdPerigo=" + lsbPerigo.SelectedValue + ")))");
            lsbEPI.DataValueField = "Id";
            lsbEPI.DataTextField = "Nome";
            lsbEPI.DataBind();

            PopulaLsbEpiSelected();
        }

        private void PopulaLsbEpiSelected()
        {
            lsbEPISelected.Items.Clear();

            ArrayList alOperacaoPerigoRiscoEPI = new OperacaoPerigoRiscoEPI().Find("IdOperacaoPerigoRiscoAcidente"
                + " IN (SELECT IdOperacaoPerigoRiscoAcidente FROM OperacaoPerigoRiscoAcidente WHERE IdRiscoAcidente=" + lsbRisco.SelectedValue
                + " AND IdOperacaoPerigo IN (SELECT IdOperacaoPerigo FROM OperacaoPerigo WHERE IdOperacao=" + lsbOperacao.SelectedValue
                + " AND IdPerigo=" + lsbPerigo.SelectedValue + "))"
                + " ORDER BY (SELECT Descricao FROM EPI WHERE EPI.IdEPI=OperacaoPerigoRiscoEPI.IdEPI)");

            foreach (OperacaoPerigoRiscoEPI operacaoPerigoRiscoEPI in alOperacaoPerigoRiscoEPI)
            {
                operacaoPerigoRiscoEPI.IdEpi.Find();
                lsbEPISelected.Items.Add(new ListItem(operacaoPerigoRiscoEPI.IdEpi.ToString(), operacaoPerigoRiscoEPI.Id.ToString()));
            }
        }	

        protected void imbAddEPI_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (lsbEPI.SelectedItem == null)
                    throw new Exception("É necessário selecionar os EPIs que deseja associar para o controle do Risco!");

                OperacaoPerigoRiscoAcidente operacaoPerigoRisco = new OperacaoPerigoRiscoAcidente();
                operacaoPerigoRisco.Find("IdRiscoAcidente=" + lsbRisco.SelectedValue
                    + " AND IdOperacaoPerigo IN (SELECT IdOperacaoPerigo FROM OperacaoPerigo WHERE IdOperacao=" + lsbOperacao.SelectedValue
                    + " AND IdPerigo=" + lsbPerigo.SelectedValue + ")");

                bool checkProcess = false;

                foreach (ListItem itemEPI in lsbEPI.Items)
                    if (itemEPI.Selected)
                    {
                        OperacaoPerigoRiscoEPI operacaoPerigoRiscoEPI = new OperacaoPerigoRiscoEPI();
                        operacaoPerigoRiscoEPI.Inicialize();

                        operacaoPerigoRiscoEPI.IdOperacaoPerigoRiscoAcidente = operacaoPerigoRisco;
                        operacaoPerigoRiscoEPI.IdEpi.Id = Convert.ToInt32(itemEPI.Value);

                        if (!checkProcess)
                        {
                            checkProcess = true;
                            operacaoPerigoRiscoEPI.UsuarioProcessoRealizado = "Associação de EPIs para o controle do Risco do Procedimento";
                            operacaoPerigoRiscoEPI.UsuarioId = System.Convert.ToInt32( lbl_Id_Usuario.Text);
                        }

                        operacaoPerigoRiscoEPI.Save();
                    }

                PopulaLsbEpi();

                MsgBox1.Show("Ordem de Serviço", "Os EPIs selecionados foram associados com sucesso para o controle do Risco do Procedimento!", null,
                            new EO.Web.MsgBoxButton("OK"));                
            }
            catch (Exception ex)
            {
                
                MsgBox1.Show("Ordem de Serviço", ex.Message, null,
                            new EO.Web.MsgBoxButton("OK"));                
            }
        }

        protected void imbRemoveEPI_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (lsbEPISelected.SelectedItem == null)
                    throw new Exception("É necessário selecionar os EPIs que deseja desassociar do controle do Risco!");

                bool checkProcess = false;

                foreach (ListItem itemEPISelected in lsbEPISelected.Items)
                    if (itemEPISelected.Selected)
                    {
                        OperacaoPerigoRiscoEPI operacaoPerigoRiscoEPI = new OperacaoPerigoRiscoEPI(Convert.ToInt32(itemEPISelected.Value));

                        if (!checkProcess)
                        {
                            checkProcess = true;
                            operacaoPerigoRiscoEPI.UsuarioProcessoRealizado = "Desassociação de EPIs do controle do Risco do Procedimento";
                            operacaoPerigoRiscoEPI.UsuarioId = System.Convert.ToInt32( lbl_Id_Usuario.Text );
                        }

                        operacaoPerigoRiscoEPI.Delete();
                    }

                PopulaLsbEpi();

                
                MsgBox1.Show("Ordem de Serviço", "Os EPIs selecionados foram desassociados com sucesso do controle do Risco do Procedimento!", null,
                              new EO.Web.MsgBoxButton("OK"));                

            }
            catch (Exception ex)
            {                
                MsgBox1.Show("Ordem de Serviço", ex.Message, null,
                           new EO.Web.MsgBoxButton("OK"));                
            }
        }

        protected void btnGravar_Click(object sender, EventArgs e)
        {
            IDbTransaction transaction = procedimento.GetTransaction();
            
            try
            {
                procedimento.IdElaboradorAPP.Id = Convert.ToInt32(ddlRespElaboracao.SelectedValue);
                procedimento.DataElaboracaoAPP = Convert.ToDateTime( wdtDataEleboracao.Text);
                procedimento.IdRevisorAPP.Id = Convert.ToInt32(ddlRespRevisao.SelectedValue);
                procedimento.DataRevisaoAPP = Convert.ToDateTime(wdtDataRevisao.Text);

                procedimento.UsuarioProcessoRealizado = "Edição dos Responsáveis da APP";
                procedimento.UsuarioId = System.Convert.ToInt32( lbl_Id_Usuario.Text );
                procedimento.Save();
                
                if (isEnabledRiscoTab())
                {
                    OperacaoPerigoRiscoAcidente operacaoPerigoRiscoAcidente = new OperacaoPerigoRiscoAcidente();
                    operacaoPerigoRiscoAcidente.Find("IdRiscoAcidente=" + lsbRisco.SelectedValue
                        + " AND IdOperacaoPerigo IN (SELECT IdOperacaoPerigo FROM OperacaoPerigo WHERE IdOperacao=" + lsbOperacao.SelectedValue
                        + " AND IdPerigo=" + lsbPerigo.SelectedValue + ")");

                    operacaoPerigoRiscoAcidente.Epc = txtEPC.Text.Trim();
                    operacaoPerigoRiscoAcidente.MedidasAdm = txtMedAdm.Text.Trim();
                    operacaoPerigoRiscoAcidente.MedidasEdu = txtMedEdu.Text.Trim();

                    operacaoPerigoRiscoAcidente.UsuarioProcessoRealizado = "Edição dos dados para o controle do Risco no Procedimento";
                    operacaoPerigoRiscoAcidente.UsuarioId = System.Convert.ToInt32( lbl_Id_Usuario.Text );
                    operacaoPerigoRiscoAcidente.Transaction = transaction;
                    operacaoPerigoRiscoAcidente.Save();
                }

                if (isEnabledImpactoTab())
                {
                    OperacaoAspectoAmbientalImpacto operacaoAspectoAmbientalImpacto = new OperacaoAspectoAmbientalImpacto();
                    operacaoAspectoAmbientalImpacto.Find("IdImpactoAmbiental=" + lsbImpacto.SelectedValue
                        + " AND IdOperacaoAspectoAmbiental IN (SELECT IdOperacaoAspectoAmbiental FROM OperacaoAspectoAmbiental WHERE IdOperacao=" + lsbOperacao.SelectedValue
                        + " AND IdAspectoAmbiental=" + lsbAspecto.SelectedValue + ")");

                    operacaoAspectoAmbientalImpacto.MedidasOpe = txtMedOpeAmb.Text.Trim();
                    operacaoAspectoAmbientalImpacto.MedidasEdu = txtMedEduAmb.Text.Trim();

                    operacaoAspectoAmbientalImpacto.UsuarioProcessoRealizado = "Edição dos dados para o controle do Impacto no Procedimento";
                    operacaoAspectoAmbientalImpacto.UsuarioId = System.Convert.ToInt32( lbl_Id_Usuario.Text );
                    operacaoAspectoAmbientalImpacto.Transaction = transaction;
                    operacaoAspectoAmbientalImpacto.Save();
                }

                transaction.Commit();
                                
                if (isEnabledRiscoTab() && isEnabledImpactoTab())                    
                    MsgBox1.Show("Ordem de Serviço", "Os dados para o controle do Risco e do Impacto no Procedimento, bem como os Responsáveis pela APP, foram atualizados com sucesso!", null,
                                            new EO.Web.MsgBoxButton("OK"));                
                else if (isEnabledRiscoTab())
                    MsgBox1.Show("Ordem de Serviço", "Os dados para o controle do Risco no Procedimento, bem como os Responsáveis pela APP, foram atualizados com sucesso!", null,
                                            new EO.Web.MsgBoxButton("OK"));                
                else if (isEnabledImpactoTab())                    
                    MsgBox1.Show("Ordem de Serviço", "Os dados para o controle do Impacto no Procedimento, bem como os Responsáveis pela APP, foram atualizados com sucesso!", null,
                                            new EO.Web.MsgBoxButton("OK"));                
                else if (!isEnabledRiscoTab() && !isEnabledImpactoTab())
                    MsgBox1.Show("Ordem de Serviço", "Os Responsáveis pela APP foram atualizados com sucesso!", null,
                                            new EO.Web.MsgBoxButton("OK"));                

            }
            catch (Exception ex)
            {
                transaction.Rollback();
                MsgBox1.Show("Ordem de Serviço", ex.Message, null,
                                        new EO.Web.MsgBoxButton("OK"));                
            }
        }

        private void PopulaLsbAspecto()
        {
            lsbAspecto.DataSource = new AspectoAmbiental().GetIdNome("Nome", "IdAspectoAmbiental IN (SELECT IdAspectoAmbiental FROM OperacaoAspectoAmbiental WHERE IdOperacao="
                + lsbOperacao.SelectedValue + ")");
            lsbAspecto.DataValueField = "Id";
            lsbAspecto.DataTextField = "Nome";
            lsbAspecto.DataBind();
        }

        protected void lsbAspecto_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isEnabledImpactoTab())
                DesabilitaImpactoTab();

            PopulaLsbImpacto();
        }

        private void PopulaLsbImpacto()
        {
            lsbImpacto.DataSource = new ImpactoAmbiental().GetIdNome("Nome", "IdImpactoAmbiental IN (SELECT IdImpactoAmbiental FROM OperacaoAspectoAmbientalImpacto WHERE"
                + " IdOperacaoAspectoAmbiental IN (SELECT IdOperacaoAspectoAmbiental FROM OperacaoAspectoAmbiental WHERE IdOperacao=" + lsbOperacao.SelectedValue
                + " AND IdAspectoAmbiental=" + lsbAspecto.SelectedValue + "))");
            lsbImpacto.DataValueField = "Id";
            lsbImpacto.DataTextField = "Nome";
            lsbImpacto.DataBind();
        }

        protected void lsbImpacto_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isEnabledImpactoTab())
                HabilitaImpactoTab();

            PopulaTelaControleImpacto();
        }

        private void PopulaTelaControleImpacto()
        {
            OperacaoAspectoAmbientalImpacto operacaoAspectoAmbientalImpacto = new OperacaoAspectoAmbientalImpacto();
            operacaoAspectoAmbientalImpacto.Find("IdImpactoAmbiental=" + lsbImpacto.SelectedValue
                + " AND IdOperacaoAspectoAmbiental IN (SELECT IdOperacaoAspectoAmbiental FROM OperacaoAspectoAmbiental WHERE IdOperacao=" + lsbOperacao.SelectedValue
                + " AND IdAspectoAmbiental=" + lsbAspecto.SelectedValue + ")");

            txtMedOpeAmb.Text = operacaoAspectoAmbientalImpacto.MedidasOpe;
            txtMedEduAmb.Text = operacaoAspectoAmbientalImpacto.MedidasEdu;
        }

        protected void btnProcedimento_Click(object sender, EventArgs e)
        {
            //Server.Transfer("CadProcedimento.aspx", true);
            Response.Redirect("CadProcedimento.aspx?IdProcedimento=" + procedimento.Numero.ToString("0000") + "&IdEmpresa=" + lbl_Id_Empresa.Text + "&IdUsuario=" + lbl_Id_Usuario.Text);
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            //Server.Transfer("ListaProcedimento.aspx", true);
            Response.Redirect("ListaProcedimento.aspx?IdEmpresa=" + lbl_Id_Empresa.Text + "&IdUsuario=" + lbl_Id_Usuario.Text);			
        }
        protected void lkbAPR_Click(object sender, EventArgs e)
        {            
			Guid strAux = Guid.NewGuid();

			OpenReport("OrdemDeServico", "RptAPR.aspx?IliteraSystem=" + strAux.ToString() + strAux.ToString()
				+ "&IdUsuario=" + lbl_Id_Usuario.Text + "&IdEmpresa=" + lbl_Id_Empresa.Text + "&IdProcedimento=" + procedimento.Id, "RptAPR");
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




    }
}