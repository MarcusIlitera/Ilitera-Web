using System;
using System.Data;
using System.Web.SessionState;
using System.Web.UI.WebControls;
using Ilitera.Common;
using Ilitera.Opsa.Data;
using System.Text;
using System.Collections;
using System.Web;
using System.IO;


namespace Ilitera.Net
{
	/// <summary>
	/// Summary description for ListaEmpresa.
	/// </summary>
	public partial class CadastroCurso : System.Web.UI.Page
	{

        private Treinamento treinamento;
        //private Infragistics.WebUI.WebDataInput.WebDateTimeEdit wdeDataInicio;

        protected Prestador prestador = new Prestador();
        protected Usuario usuario = new Usuario();
        protected Cliente cliente = new Cliente();
        protected Empregado empregado = new Empregado();
	

        protected void Page_Load(object sender, System.EventArgs e)
        {
            InicializaWebPageObjects();
            if (!IsPostBack)
            {
                lsbParticipantes.Attributes.Add("ondblclick", "javascript:addItemPop(centerWin('CadNovoParticipante.aspx?IdEmpresa=" + Request["IdEmpresa"] + "&IdUsuario=" + Request["IdUsuario"] + "&IdParticipante=' + this.value + '',300,165,\'CadastroNovoTreinador\'));");
                btnExcluir.Attributes.Add("onClick", "javascript:return confirm('Deseja realmente Excluir este Curso?');");

                cliente.Find(System.Convert.ToInt32(Request["IdEmpresa"].ToString()));

                PopulaDDLCursos();
                PopulaDDLTreinador();

                PopulaLsbEmpregado();
                if (treinamento.Id.Equals(0))
                    PopulaTextCertificado(Convert.ToInt32(ddlCursos.SelectedValue));
                else
                {
                    PopulaLsbParticipante();
                    PopulaTela();
                }

                //UltraWebTabCertificado.SelectedTabIndex = 2;

            }
            else if (txtAuxiliar.Value.Equals("atualizaColaborador"))
            {
                txtAuxiliar.Value = string.Empty;
                PopulaMantemDDLTreinador();
            }
            else if (txtAuxiliar.Value.Equals("atualizaParticipante"))
            {
                txtAuxiliar.Value = string.Empty;
                PopulaLsbParticipante();
            }
        }

        #region Inicialização de componentes do WebTab

        private void InicializaComponentes()
        {
            //txtPeriodo = (TextBox)UltraWebTabCertificado.FindControl("tabtxtPeriodo");
            //ddlTreinador = (DropDownList)UltraWebTabCertificado.FindControl("tabddlTreinador");
            //lsbEmpregados = (ListBox)UltraWebTabCertificado.FindControl("tablsbEmpregados");
            //imbAdiciona = (ImageButton)UltraWebTabCertificado.FindControl("tabImbAdiciona");
            //imbAdicionaTodos = (ImageButton)UltraWebTabCertificado.FindControl("tabImbAdicionaTodos");
            //imbRemove = (ImageButton)UltraWebTabCertificado.FindControl("tabImgRemove");
            //imbRemoveTodos = (ImageButton)UltraWebTabCertificado.FindControl("tabImgRemoveTodos");
            //lsbParticipantes = (ListBox)UltraWebTabCertificado.FindControl("tablsbParticipantes");
            //btnNovoParticipante = (Button)UltraWebTabCertificado.FindControl("tabbtnNovoParticipante");
            //wdeDataInicio = (Infragistics.WebUI.WebDataInput.WebDateTimeEdit)UltraWebTabCertificado.FindControl("tabwdeDataInicio");
        }

        protected void InicializaEventosDosComponentes()
        {
            ImbAdiciona.Click += new System.Web.UI.ImageClickEventHandler(imbAdiciona_Click);
            ImbAdicionaTodos.Click += new System.Web.UI.ImageClickEventHandler(imbAdicionaTodos_Click);
            ImbRemove.Click += new System.Web.UI.ImageClickEventHandler(imbRemove_Click);
            ImbRemoveTodos.Click += new System.Web.UI.ImageClickEventHandler(imbRemoveTodos_Click);
            ddlTreinador.SelectedIndexChanged += new System.EventHandler(ddlTreinador_SelectedIndexChanged);
            btnNovoParticipante.Click += new System.EventHandler(btnNovoParticipante_Click);
        }
        #endregion

        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InicializaComponentes();
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

        protected void InicializaWebPageObjects()
        {
            //base.InicializaWebPageObjects();

            if (Request["IdTreinamento"] != string.Empty && Request["IdTreinamento"] != null)
            {
                treinamento = new Treinamento(Convert.ToInt32(Request["IdTreinamento"]));
                btnExcluir.Enabled = true;
            }
            else if (ViewState["IdTreinamento"] != null && ViewState["IdTreinamento"].ToString() != string.Empty)
            {
                treinamento = new Treinamento(Convert.ToInt32(ViewState["IdTreinamento"]));
                btnExcluir.Enabled = true;
            }
            else
            {
                treinamento = new Treinamento();
                treinamento.Inicialize();
                btnExcluir.Enabled = false;
            }
        }

        private void PopulaMantemDDLTreinador()
        {
            string ddlValue = ddlTreinador.SelectedValue;

            PopulaDDLTreinador();

            ddlTreinador.Items.FindByValue(ddlValue).Selected = true;
        }

       
        private void PopulaLsbEmpregado()
        {
            DataSet ds = new Empregado().Get("nID_EMPR=" + cliente.Id + " AND hDT_DEM IS NULL"
                + " AND nID_EMPREGADO NOT IN (SELECT opsa.dbo.ParticipanteTreinamento.IdEmpregado FROM opsa.dbo.ParticipanteTreinamento WHERE opsa.dbo.ParticipanteTreinamento.IdTreinamento=" + treinamento.Id
                + " AND opsa.dbo.ParticipanteTreinamento.IdEmpregado IS NOT NULL)"
                + " ORDER BY tNO_EMPG");

            lsbEmpregados.DataSource = ds;
            lsbEmpregados.DataValueField = "nID_EMPREGADO";
            lsbEmpregados.DataTextField = "tNO_EMPG";
            lsbEmpregados.DataBind();
        }

        private void PopulaDDLTreinador()
        {
            ArrayList prestadores = new Prestador().GetListaPrestador(cliente, false, (int)TipoPrestador.ContatoEmpresa);
            prestadores.Sort();
            ddlTreinador.DataSource = prestadores;
            ddlTreinador.DataValueField = "Id";
            ddlTreinador.DataTextField = "NomeCompleto";
            ddlTreinador.DataBind();

            ddlTreinador.Items.Insert(0, new ListItem("Selecione o Instrutor do curso...", "0"));
            ddlTreinador.Items.Insert(ddlTreinador.Items.Count, new ListItem("Cadastre um Novo Instrutor...", "Novo"));
        }

        private void PopulaTextCertificado(int IdTreinamento)
        {
            //TreinamentoDicionario treinamento = new TreinamentoDicionario(IdTreinamento);

            //WHETextoColetivo.Text = treinamento.TextoColetivo;
            //WHETextoIndividual.Text = treinamento.TextoIndividual;
        }

        private void PopulaDDLCursos()
        {
            DataSet ds = new DataSet();
            DataRow newRow;
            DataTable table = new DataTable();
            table.Columns.Add("IdTreinamentoDicionario", typeof(string));
            table.Columns.Add("NomeTreinamento", typeof(string));
            ds.Tables.Add(table);

            ArrayList alTreinamentoDic = new TreinamentoDicionario().Find("IdCliente=" + Request["IdEmpresa"] + " ORDER BY Nome");

            foreach (TreinamentoDicionario treinamento in alTreinamentoDic)
            {
                newRow = ds.Tables[0].NewRow();
                newRow["IdTreinamentoDicionario"] = treinamento.Id.ToString();
                newRow["NomeTreinamento"] = treinamento.Nome;
                ds.Tables[0].Rows.Add(newRow);
            }

            ddlCursos.DataSource = ds;
            ddlCursos.DataTextField = "NomeTreinamento";
            ddlCursos.DataValueField = "IdTreinamentoDicionario";
            ddlCursos.DataBind();
        }

        protected void ddlCursos_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            PopulaTextCertificado(Convert.ToInt32(ddlCursos.SelectedValue));
        }

        protected void btnGravar_Click(object sender, System.EventArgs e)
        {
            try
            {
                string aux = string.Empty;

                if (wdeDataInicio.Text == "")
                    throw new Exception("A Data de Início do Curso deve ser preenchida!");
                if (txtPeriodo.Text.Trim().Equals(string.Empty))
                    throw new Exception("O Período do Curso deve ser preenchido!");
                if (ddlTreinador.SelectedValue.Equals("0"))
                    throw new Exception("O Instrutor do Curso deve ser selecionado!");

                if (treinamento.Id.Equals(0))
                    aux = "cadastrado";
                else
                    aux = "editado";

                PopulaTreinamento();

                ViewState["IdTreinamento"] = treinamento.Id;
                ViewState["ddlTreinadorValorBanco"] = treinamento.IdResponsavel.Id;

                if (aux.Equals("cadastrado"))
                    btnExcluir.Enabled = true;

                StringBuilder st = new StringBuilder();

                st.Append("window.opener.document.getElementById('txtAuxiliar').value='atualiza';");
                st.Append("window.opener.document.forms[0].submit();");
                st.Append("window.alert('O Curso foi " + aux + " com sucesso!');");

                this.ClientScript.RegisterStartupScript(this.GetType(), "GravaTreinamento", st.ToString(), true);
            }
            catch (Exception ex)
            {
                MsgBox1.Show("Ilitera.Net", ex.Message, null,
                                new EO.Web.MsgBoxButton("OK"));                

            }
        }

        private void PopulaTreinamento()
        {
            treinamento.IdDocumentoBase.Id = (int)Ilitera.Opsa.Data.Documentos.Treinamentos;
            treinamento.IdCliente = cliente;
            treinamento.IdPrestador = prestador;
            treinamento.DataLevantamento = System.Convert.ToDateTime( wdeDataInicio.Text );

            treinamento.IdTreinamentoDicionario.Id = Convert.ToInt32(ddlCursos.SelectedValue);
            treinamento.IsFromCliente = true;
            treinamento.IdResponsavel.Id = Convert.ToInt32(ddlTreinador.SelectedValue);
            treinamento.Periodo = txtPeriodo.Text.Trim();
            //treinamento.TextoColetivo = WHETextoColetivo.Text.Trim();
            //treinamento.TextoIndividual = WHETextoIndividual.Text.Trim();

            treinamento.UsuarioId = usuario.Id;
            treinamento.Save();
        }

        private void imbAdiciona_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            try
            {
                bool checkSelecionado = false;

                if (treinamento.Id.Equals(0))
                    throw new Exception("É necessário gravar o curso antes de adicionar seus participantes!");

                foreach (ListItem empregado in lsbEmpregados.Items)
                    if (empregado.Selected)
                    {
                        checkSelecionado = true;
                        ParticipanteTreinamento participante = new ParticipanteTreinamento();
                        participante.Inicialize();

                        participante.IdTreinamento = treinamento;
                        participante.IdEmpregado.Id = Convert.ToInt32(empregado.Value);

                        participante.UsuarioId = usuario.Id;
                        participante.Save();
                    }

                if (!checkSelecionado)
                    throw new Exception("Não há nenhum empregado selecionado para ser adicionado como participante!");

                PopulaLsbEmpregado();
                PopulaLsbParticipante();


                MsgBox1.Show("Ilitera.Net", "Os empregados selecionados foram adicionados com sucesso!", null,
                                new EO.Web.MsgBoxButton("OK"));                

            }
            catch (Exception ex)
            {
                MsgBox1.Show("Ilitera.Net", ex.Message, null,
                                new EO.Web.MsgBoxButton("OK"));                

            }
        }

        private void PopulaLsbParticipante()
        {
            DataSet ds = new DataSet(), sortedDS = new DataSet();
            DataTable table = new DataTable(), sortedTable = new DataTable();
            DataRow newRow, newSortedRow;

            table.Columns.Add("IdParticipante", typeof(string));
            table.Columns.Add("NomeParticipante", typeof(string));
            ds.Tables.Add(table);

            sortedTable.Columns.Add("IdParticipante", typeof(string));
            sortedTable.Columns.Add("NomeParticipante", typeof(string));
            sortedDS.Tables.Add(sortedTable);

            ArrayList alParticipantes = new ParticipanteTreinamento().Find("IdTreinamento=" + treinamento.Id);

            foreach (ParticipanteTreinamento participante in alParticipantes)
            {
                newRow = ds.Tables[0].NewRow();
                newRow["IdParticipante"] = participante.Id.ToString();

                if (participante.IdEmpregado.Id.Equals(0))
                    newRow["NomeParticipante"] = participante.NomeParticipante;
                else
                {
                    participante.IdEmpregado.Find();
                    newRow["NomeParticipante"] = participante.IdEmpregado.tNO_EMPG;
                }

                ds.Tables[0].Rows.Add(newRow);
            }

            DataRow[] rows = ds.Tables[0].Select("", "NomeParticipante");

            foreach (DataRow row in rows)
            {
                newSortedRow = sortedDS.Tables[0].NewRow();
                newSortedRow["IdParticipante"] = row["IdParticipante"];
                newSortedRow["NomeParticipante"] = row["NomeParticipante"];
                sortedDS.Tables[0].Rows.Add(newSortedRow);
            }

            lsbParticipantes.DataSource = sortedDS;
            lsbParticipantes.DataTextField = "NomeParticipante";
            lsbParticipantes.DataValueField = "IdParticipante";
            lsbParticipantes.DataBind();
        }

        private void imbAdicionaTodos_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            try
            {
                bool checkSelecionado = false;

                if (treinamento.Id.Equals(0))
                    throw new Exception("É necessário gravar o curso antes de adicionar seus participantes!");

                foreach (ListItem empregado in lsbEmpregados.Items)
                {
                    checkSelecionado = true;
                    ParticipanteTreinamento participante = new ParticipanteTreinamento();
                    participante.Inicialize();

                    participante.IdTreinamento = treinamento;
                    participante.IdEmpregado.Id = Convert.ToInt32(empregado.Value);

                    participante.UsuarioId = usuario.Id;
                    participante.Save();
                }

                if (!checkSelecionado)
                    throw new Exception("Não há nenhum empregado para ser adicionado como participante!");

                PopulaLsbEmpregado();
                PopulaLsbParticipante();


                MsgBox1.Show("Ilitera.Net", "Todos os empregados foram adicionados com sucesso!", null,
                                new EO.Web.MsgBoxButton("OK"));                

            }
            catch (Exception ex)
            {
                MsgBox1.Show("Ilitera.Net", ex.Message, null,
                                new EO.Web.MsgBoxButton("OK"));                

            }
        }

        private void imbRemove_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            try
            {
                bool checkSelecionado = false;

                foreach (ListItem participante in lsbParticipantes.Items)
                    if (participante.Selected)
                    {
                        checkSelecionado = true;
                        ParticipanteTreinamento partAtual = new ParticipanteTreinamento(Convert.ToInt32(participante.Value));
                        partAtual.UsuarioId = usuario.Id;
                        partAtual.Delete();
                    }

                if (!checkSelecionado)
                    throw new Exception("Não há nenhum participante selecionado para ser removido!");

                PopulaLsbEmpregado();
                PopulaLsbParticipante();


                MsgBox1.Show("Ilitera.Net", "Os participantes selecionados foram removidos com sucesso!", null,
                                new EO.Web.MsgBoxButton("OK"));                

            }
            catch (Exception ex)
            {
                MsgBox1.Show("Ilitera.Net", ex.Message, null,
                                new EO.Web.MsgBoxButton("OK"));                

            }
        }

        private void imbRemoveTodos_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            try
            {
                bool checkSelecionado = false;

                foreach (ListItem participante in lsbParticipantes.Items)
                {
                    checkSelecionado = true;
                    ParticipanteTreinamento partAtual = new ParticipanteTreinamento(Convert.ToInt32(participante.Value));
                    partAtual.UsuarioId = usuario.Id;
                    partAtual.Delete();
                }

                if (!checkSelecionado)
                    throw new Exception("Não há nenhum participante para ser removido!");

                PopulaLsbEmpregado();
                PopulaLsbParticipante();


                MsgBox1.Show("Ilitera.Net", "Todos os participantes foram removidos com sucesso!", null,
                                new EO.Web.MsgBoxButton("OK"));                

            }
            catch (Exception ex)
            {
                MsgBox1.Show("Ilitera.Net", ex.Message, null,
                                new EO.Web.MsgBoxButton("OK"));                

            }
        }

        private void ddlTreinador_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTreinador.SelectedValue == "Novo")
            {
                ddlTreinador.ClearSelection();

                if (ViewState["ddlTreinadorValorBanco"] != null && ViewState["ddlTreinadorValorBanco"].ToString() != "0")
                    ddlTreinador.Items.FindByValue(ViewState["ddlTreinadorValorBanco"].ToString()).Selected = true;

                StringBuilder st = new StringBuilder();

                st.Append("addItemPop(centerWin('../CommonPages/CadResponsavel.aspx?IdEmpresa=" + Request["IdEmpresa"] + "&IdUsuario=" + Request["IdUsuario"] + "',350,300,\'CadastroNovoTreinador\'));");

                this.ClientScript.RegisterStartupScript(this.GetType(), "CadastroNovoTreinador", st.ToString(), true);
            }
        }

        private void btnNovoParticipante_Click(object sender, EventArgs e)
        {
            if (!treinamento.Id.Equals(0))
            {
                StringBuilder st = new StringBuilder();

                st.Append("addItemPop(centerWin('CadNovoParticipante.aspx?IdEmpresa=" + Request["IdEmpresa"] + "&IdUsuario=" + Request["IdUsuario"] + "&IdTreinamento=" + treinamento.Id + "',300,165,\'CadastroNovoTreinador\'));");

                this.ClientScript.RegisterStartupScript(this.GetType(), "CadastroNovoTreinador", st.ToString(), true);
            }
            else                
               MsgBox1.Show("Ilitera.Net", "É necessário gravar o curso antes de adicionar seus participantes!", null,
                            new EO.Web.MsgBoxButton("OK"));                

        }


        private void PopulaTela()
        {
            //ddlCursos.Items.FindByValue(treinamento.IdTreinamentoDicionario.Id.ToString()).Selected = true;
            ddlCursos.Items.Clear();
            //treinamento.IdTreinamentoDicionario.IdObrigacao.Find(" IdObrigacao in ( Select IdObrigacao from TreinamentoDicionario where IdTreinamentoDicionario = " + treinamento.IdTreinamentoDicionario.Id.ToString() + " ) ");
            ddlCursos.Items.Add(treinamento.IdTreinamentoDicionario.ToString());  //.IdObrigacao.Nome.ToString());
            ddlCursos.SelectedIndex = 0;
            ddlCursos.Enabled = false;


            //WHETextoColetivo.Text = treinamento.TextoColetivo;
            //WHETextoIndividual.Text = treinamento.TextoIndividual;
            wdeDataInicio.Text = treinamento.DataLevantamento.ToString("dd/MM/yyyy");
            txtPeriodo.Text = treinamento.Periodo;


            //ddlTreinador.Items.FindByValue(treinamento.IdResponsavel.Id.ToString()).Selected = true;
            ddlTreinador.Items.Clear();
            ddlTreinador.Items.Add(treinamento.IdResponsavel.NomeCompleto);
            ddlTreinador.SelectedIndex = 0;
            ddlTreinador.Enabled = false;


            ViewState["ddlTreinadorValorBanco"] = treinamento.IdResponsavel.Id;
        }

        protected void btnExcluir_Click(object sender, EventArgs e)
        {
            try
            {
                treinamento.UsuarioId = usuario.Id;
                treinamento.Delete();

                StringBuilder st = new StringBuilder();

                st.Append("window.opener.document.getElementById('txtAuxiliar').value='atualiza';");
                st.Append("window.opener.document.forms[0].submit();");
                st.Append("window.alert('O Curso foi Excluído com sucesso!');");
                st.Append("self.close();");

                this.ClientScript.RegisterStartupScript(this.GetType(), "ExcluiTreinamento", st.ToString(), true);
            }
            catch (Exception ex)
            {                
                MsgBox1.Show("Ilitera.Net", ex.Message, null,
                                new EO.Web.MsgBoxButton("OK"));                

            }
        }
	}
}
