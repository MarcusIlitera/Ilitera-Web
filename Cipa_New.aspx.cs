using System;
using Ilitera.Opsa.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ilitera.Common;
using EO.Web;
using System.Data;
using System.Text;
using Ilitera.Opsa.Report;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.IO;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Collections;
using System.Reflection;

namespace Ilitera.Net
{
    public partial class Cipa_New : System.Web.UI.Page
    {
        private Cliente cliente = new Cliente();
        private Cipa cipa = new Cipa();
        private Juridica juridica = new Juridica();
        protected void Page_Load(object sender, EventArgs e)
        {

            cliente.Find(Convert.ToInt32(Session["Empresa"]));
            juridica = cliente;
            cipa = cliente.GetGestaoAtual();

            if (IsPostBack)
            {
                EleicaoCipa eleicaoCipa = new EleicaoCipa();
                EventoCipa eventoCipa = EventoCipa.GetEventoCipa(cipa, EventoBase.Eleicao);
                eventoCipa.Find(eventoCipa.Id);
                if (eventoCipa.GetType() == typeof(EleicaoCipa))
                {
                    eleicaoCipa = (EleicaoCipa)eventoCipa;
                }
                else
                {
                    eleicaoCipa = new EleicaoCipa();
                    eleicaoCipa.Find(eventoCipa.Id);
                }
                eleicaoCipa.Find(eleicaoCipa.Id);

                string eventTarget = Request["__EVENTTARGET"];
                if (eventTarget == btnAddIndicados.ClientID)
                {
                    Grid2.DataSource = null;
                    Grid2.DataBind();
                    StringBuilder str = new StringBuilder();
                    str.Append("IdCipa=" + eleicaoCipa.IdCipa.Id);
                    str.Append(" AND IndGrupoMembro=" + (int)GrupoMembro.Empregador);
                    if (chkInativos2.Checked)
                        str.Append(" AND (IndStatus=" + (int)MembroCipa.Status.Afastado + " OR IndStatus=" + (int)MembroCipa.Status.Renunciou + ")");
                    else
                        str.Append(" AND IndStatus=" + (int)MembroCipa.Status.Ativo);
                    str.Append(" ORDER BY IndTitularSuplente, Numero");

                    DataSet empregados = new MembroCipa().Get(str.ToString());
                    DataTable zDt = new DataTable();
                    zDt.Columns.Add("NomeEmpregado");
                    zDt.Columns.Add("CargoCipa");


                    foreach (DataRow row in empregados.Tables[0].Rows)
                    {
                        if (row["IdEmpregado"] != DBNull.Value)
                        {
                            Empregado empregado = new Empregado();
                            empregado.Find(Convert.ToInt32(row["IdEmpregado"]));
                            MembroCipa membroCipa = new MembroCipa();
                            membroCipa.Find(Convert.ToInt32(row["IdMembroCipa"]));
                            string cargoCipa = membroCipa.GetNomeCargo();
                            zDt.Rows.Add(empregado.tNO_EMPG, cargoCipa);
                        }
                        else
                        {
                            MembroCipa membroCipa = new MembroCipa();
                            membroCipa.Find(Convert.ToInt32(row["IdMembroCipa"]));
                            string cargoCipa = membroCipa.GetNomeCargo();
                            zDt.Rows.Add(membroCipa.NomeMembro, cargoCipa);
                        }
                    }


                    DataSet zDs = new DataSet();
                    zDs.Tables.Add(zDt);
                    Grid2.DataSource = zDs;
                    Grid2.DataBind();
                }
            }

            if (!IsPostBack)
            {
                PopulaInicio(cipa, juridica);
                PopulaCriacaoComissaoEleitoral();
                PopulaEditalConvocacaoEleitoral();
                PopulaComunicacaoSindicato();
                PopulaInicioIncricoes();
                PopulaTerminoIncricoes();
                PopulaCandidatos();
                PopulaCedulaVotacao();
                PopulaAtaEleicaoPosse();
                PopulaAtaPosseNovaGestao();
                PopularCalendarioReunioesOrdinarias();
                PopulaCandidatos();
            }
        }


        private void PopulaInicio(Cipa cipa, Juridica juridica)
        {
            try
            {
                cliente.IdSindicato.Find();

            }
            catch (Exception ex)
            {

            }
            txtSindicato.Text = cliente.IdSindicato.NomeAbreviado.Trim() == "" ? "" : cliente.IdSindicato.NomeAbreviado;

            txtEmailsAlerta.Text = cipa.ListaEmailsAviso == null ? "" : cipa.ListaEmailsAviso.ToString();

            DimensionamentoCipa dim = new DimensionamentoCipa();
            try
            {
                dim = juridica.GetDimensionamentoCipa();
            }
            catch (Exception e)
            {

            }
            txtSuplentes.Text = dim.Suplente.ToString();
            txtTitulares.Text = dim.Efetivo.ToString();
            txtTerminoMandato.Text = cipa.GetTerminoMandato().ToString("dd/MM/yyyy");


        }

        private void PopulaCriacaoComissaoEleitoral()
        {
            grd_Membros.DataSource = null;
            grd_Membros.DataBind();
            dataCalendario_Comissao.Text = cipa.ComissaoEleitoral.ToString("dd/MM/yyyy");
            grd_Presidente_Vice.Items.Clear();
            grd_Presidente_Vice.DataSource = null;
            grd_Presidente_Vice.DataBind();
            grd_Presidente_Vice.Columns[1].Visible = false;
            grd_Presidente_Vice.Columns[1].Width = 0;
            grd_Presidente_Vice.Columns[0].Width = 420;
            grd_Presidente_Vice.Width = 440;
            EventoCipa eventoCipa = EventoCipa.GetEventoCipa(cipa, EventoBase.ComissaoEleitoral);
            DataSet membros = new MembroComissaoEleitoral().Get("IdCipa=" + eventoCipa.IdCipa.Id);
            DataTable zDt = new DataTable();
            zDt.Columns.Add("IdEmprcImaegado");
            zDt.Columns.Add("NomeMembro");
            zDt.Columns.Add("NomeCargo");

            foreach (DataRow row in membros.Tables[0].Rows)
            {
                zDt.Rows.Add(row["IdMembroComissaoEleitoral"], row["NomeMembro"], row["NomeCargo"]);

            }

            DataSet zDs = new DataSet();
            zDs.Tables.Add(zDt);
            grd_Membros.DataSource = zDs;
            grd_Membros.DataBind();

            PopulaGridPresidenteVice();
        }

        private void PopulaEditalConvocacaoEleitoral()
        {
            dataCalendario_Edital.Text = cipa.Edital.ToString("dd/MM/yyyy");
            EventoCipa eventoCipa = EventoCipa.GetEventoCipa(cipa, EventoBase.Edital);
            eventoCipa.Find(eventoCipa.Id);
            eventoCipa.IdCipa.Find();
            eventoCipa.IdCipa.IdCliente.Find();
            txt_horario_local.Text = eventoCipa.HoraInicio;
            txt_local.Text = eventoCipa.Local;
        }

        private void PopulaComunicacaoSindicato()
        {
            data_comunicacao.Text = cipa.ComunicacaoSindicato.ToString("dd/MM/yyyy");
        }

        private void PopulaInicioIncricoes()
        {
            data_inscricoes.Text = cipa.InicioInscricao.ToString("dd/MM/yyyy");
        }

        private void PopulaTerminoIncricoes()
        {
            txtDataTermInscricoes.Text = cipa.TerminoInscricao.ToString("dd/MM/yyyy");
        }

        private void PopulaCandidatos()
        {
            Grid4.DataSource = null;
            Grid4.DataBind();
            lstEmpregados.Items.Clear();
            txtDataCandidatos.Text = cipa.ListaDosCandidatos.ToString("dd/MM/yyyy");
            System.Text.StringBuilder strEmpregados = new System.Text.StringBuilder();

            strEmpregados.Append("nID_EMPR =" + cipa.IdCliente.Id);
            strEmpregados.Append(" AND hDT_DEM IS NULL");
            strEmpregados.Append(" ORDER BY tNO_EMPG ");

            DataSet zDs = new Empregado().Get(strEmpregados.ToString());

            for (int zCont = 0; zCont < zDs.Tables[0].Rows.Count; zCont++)
            {
                string texto = zDs.Tables[0].Rows[zCont][1].ToString();
                string valor = zDs.Tables[0].Rows[zCont][0].ToString();

                ListItem item = new ListItem(texto, valor);
                lstEmpregados.Items.Add(item);
            }



            System.Text.StringBuilder strMembros = new System.Text.StringBuilder();
            strMembros.Append("IdCipa=" + cipa.Id);
            strMembros.Append(" ORDER BY Votos DESC,");
            strMembros.Append(" (SELECT hDT_ADM FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEMPREGADO WHERE ");
            strMembros.Append(" nID_EMPREGADO = ParticipantesEleicaoCipa.IdEmpregado)");

            DataSet membros = new ParticipantesEleicaoCipa().Get(strMembros.ToString());
            DataTable zDt = new DataTable();
            zDt.Columns.Add("IdEmprcImaegado");
            zDt.Columns.Add("Editar");
            zDt.Columns.Add("Nome");
            zDt.Columns.Add("Setor");
            zDt.Columns.Add("Apelido");

            foreach (DataRow row in membros.Tables[0].Rows)
            {
                Empregado empregado = new Empregado();
                empregado.Find(Convert.ToInt32(row["IdEmpregado"]));
                EmpregadoFuncao empregadoFuncao = EmpregadoFuncao.GetEmpregadoFuncao(empregado);
                zDt.Rows.Add(row["IdParticipantesEleicaoCipa"], "Editar", empregado.tNO_EMPG, empregadoFuncao.GetNomeSetor(), empregado.tNO_APELIDO);
            }


            bool linhaVazia = false;
            if(zDt.Rows.Count < 1)
            {
                zDt.Rows.Add("", "", "", "", "");
                linhaVazia = true;
            }

            zDs = new DataSet();
            zDs.Tables.Add(zDt);
            Grid4.DataSource = zDs;
            Grid4.DataBind();

            if (linhaVazia)
            {
                EO.Web.GridItem lastRow = Grid4.Items[Grid4.Items.Count - 1];
                foreach (EO.Web.GridCell cell in lastRow.Cells)
                {
                    cell.Style = "linhaVazia";
                }
            }
        }

        private void PopulaCedulaVotacao()
        {
            txtDataCedula.Text = cipa.CedulaVotacao.ToString("dd/MM/yyyy");
            EventoCipa eventoCipa = EventoCipa.GetEventoCipa(cipa, EventoBase.CedulaVotacao);
            cipa.IdCliente.Find();

            int qtdempregados = cipa.IdCliente.QtdEmpregados;
            txtQtdEmpregado.Text = qtdempregados.ToString();
            txtQtdCedulas.Text = qtdempregados.ToString();
        }

        private void PopulaAtaEleicaoPosse()
        {
            GridCandidatos.DataSource = null;
            GridCandidatos.DataBind();
            txtDataPosse.Text = cipa.Posse.ToString("dd/MM/yyyy");
            EleicaoCipa eleicaoCipa = new EleicaoCipa();
            EventoCipa eventoCipa = EventoCipa.GetEventoCipa(cipa, EventoBase.Eleicao);
            eventoCipa.Find(eventoCipa.Id);
            if (eventoCipa.GetType() == typeof(EleicaoCipa))
            {
                eleicaoCipa = (EleicaoCipa)eventoCipa;
            }
            else
            {
                eleicaoCipa = new EleicaoCipa();
                eleicaoCipa.Find(eventoCipa.Id);
            }
            eleicaoCipa.Find(eleicaoCipa.Id);
            eleicaoCipa.IdCipa.Find();
            eleicaoCipa.IdCipa.IdCliente.Find();
            txtDataCalendario.Text = eleicaoCipa.DataSolicitacao.ToString("dd/MM/yyyy");
            txtHoraReuniao.Text = eleicaoCipa.HoraInicio.Trim() == "" ? "--:--" : eleicaoCipa.HoraInicio;
            txtHoraReuniao2.Text = eleicaoCipa.HoraFim.Trim() == "" ? "--:--" : eleicaoCipa.HoraFim;
            txtLocal.Text = eleicaoCipa.Local;
            txtBranco.Text = eleicaoCipa.VotosBrancos.ToString();
            txtNulo.Text = eleicaoCipa.VotosNulos.ToString();
            int totalVotos = eleicaoCipa.VotosBrancos + eleicaoCipa.VotosNulos;
            txtQtdEmpregados.Text = eleicaoCipa.IdCipa.IdCliente.QtdEmpregados.ToString();
            lstTexto.Text = eleicaoCipa.FraseAdicional;

            //Preenchendo GridCandidatos
            //List<Opsa.Report.CandidatosApurados> lista = new List<Opsa.Report.CandidatosApurados>();
            //if (lista.Count > 0) lista.Clear();

            string where = "IdCipa=" + eleicaoCipa.IdCipa.Id
                       + " ORDER BY (SELECT tNO_EMPG FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEMPREGADO WHERE"
                       + " nID_EMPREGADO = ParticipantesEleicaoCipa.IdEmpregado)";

            DataSet candidatos = new ParticipantesEleicaoCipa().Get(where);
            DataTable zDt = new DataTable();
            zDt.Columns.Add("IdEmpregado");
            zDt.Columns.Add("Editar");
            zDt.Columns.Add("NomeEmpregado");
            zDt.Columns.Add("Votos");
            zDt.Columns.Add("VicePresidente");

            foreach (DataRow row in candidatos.Tables[0].Rows)
            {
                Empregado empregado = new Empregado();
                empregado.Find(Convert.ToInt32(row["IdEmpregado"]));
                string isVicePresidente = Convert.ToBoolean(row["IsVicePresidente"]) ? "Sim" : "";
                zDt.Rows.Add(empregado.Id, "Editar", empregado.tNO_EMPG, row["Votos"], isVicePresidente);
                totalVotos += Convert.ToInt32(row["Votos"]);
            }

            DataSet zDs = new DataSet();
            zDs.Tables.Add(zDt);
            GridCandidatos.DataSource = zDs;
            GridCandidatos.DataBind();
            txtTotal.Text = totalVotos.ToString();
            //Preenchendo Empregados Eleitos
            StringBuilder str = new StringBuilder();
            str.Append("IdCipa=" + eleicaoCipa.IdCipa.Id);
            str.Append(" AND IndGrupoMembro=" + (int)Ilitera.Opsa.Data.GrupoMembro.Empregado);
            if (chkInativos1.Checked)
                str.Append(" AND (IndStatus=" + (int)MembroCipa.Status.Afastado + " OR IndStatus=" + (int)MembroCipa.Status.Renunciou + ")");
            else
                str.Append(" AND IndStatus=" + (int)MembroCipa.Status.Ativo);
            str.Append(" ORDER BY IndTitularSuplente, Numero");

            DataSet empregadosEleitos = new MembroCipa().Get(str.ToString());
            zDt = new DataTable();
            zDt.Columns.Add("NomeEmpregado");
            zDt.Columns.Add("CargoCipa");
            zDt.Columns.Add("Estabilidade");

            foreach (DataRow row in empregadosEleitos.Tables[0].Rows)
            {
                Empregado empregado = new Empregado();
                empregado.Find(Convert.ToInt32(row["IdEmpregado"]));
                MembroCipa membroCipa = new MembroCipa();
                membroCipa.Find(Convert.ToInt32(row["IdMembroCipa"]));
                string cargoCipa = membroCipa.GetNomeCargo();
                string dataEstabilidade = row["Estabilidade"].ToString();
                if (dataEstabilidade.Length < 10)
                {
                    zDt.Rows.Add(empregado.tNO_EMPG, cargoCipa, dataEstabilidade);
                }
                else
                {
                    zDt.Rows.Add(empregado.tNO_EMPG, cargoCipa, dataEstabilidade.Remove(10));
                }
            }


            zDs = new DataSet();
            zDs.Tables.Add(zDt);
            Grid1.DataSource = zDs;
            Grid1.DataBind();

            //Preenchendo Empregados

            str = new StringBuilder();
            str.Append("IdCipa=" + eleicaoCipa.IdCipa.Id);
            str.Append(" AND IndGrupoMembro=" + (int)GrupoMembro.Empregador);
            if (chkInativos2.Checked)
                str.Append(" AND (IndStatus=" + (int)MembroCipa.Status.Afastado + " OR IndStatus=" + (int)MembroCipa.Status.Renunciou + ")");
            else
                str.Append(" AND IndStatus=" + (int)MembroCipa.Status.Ativo);
            str.Append(" ORDER BY IndTitularSuplente, Numero");

            DataSet empregados = new MembroCipa().Get(str.ToString());
            zDt = new DataTable();
            zDt.Columns.Add("NomeEmpregado");
            zDt.Columns.Add("CargoCipa");


            foreach (DataRow row in empregados.Tables[0].Rows)
            {
                if (row["IdEmpregado"] != DBNull.Value)
                {
                    Empregado empregado = new Empregado();
                    empregado.Find(Convert.ToInt32(row["IdEmpregado"]));
                    MembroCipa membroCipa = new MembroCipa();
                    membroCipa.Find(Convert.ToInt32(row["IdMembroCipa"]));
                    string cargoCipa = membroCipa.GetNomeCargo();
                    zDt.Rows.Add(empregado.tNO_EMPG, cargoCipa);
                }
                else
                {
                    MembroCipa membroCipa = new MembroCipa();
                    membroCipa.Find(Convert.ToInt32(row["IdMembroCipa"]));
                    string cargoCipa = membroCipa.GetNomeCargo();
                    zDt.Rows.Add(membroCipa.NomeMembro, cargoCipa);
                }
            }


            zDs = new DataSet();
            zDs.Tables.Add(zDt);
            Grid2.DataSource = zDs;
            Grid2.DataBind();
        }

        private void PopulaAtaPosseNovaGestao()
        {
            txtDataPosse.Text = cipa.Posse.ToString("dd/MM/yyyy");
            EventoCipa eventoCipa = EventoCipa.GetEventoCipa(cipa, EventoBase.Posse);
            eventoCipa.Find(eventoCipa.Id);
            eventoCipa.IdCipa.Find();
            eventoCipa.IdCipa.IdCliente.Find();
            txtHoraPosse.Text = eventoCipa.HoraInicio.Trim() == "" ? "--:--" : eventoCipa.HoraInicio;
            txtHoraPosse2.Text = eventoCipa.HoraFim.Trim() == "" ? "--:--" : eventoCipa.HoraFim;
            txtLocalPosse.Text = eventoCipa.Local;
        }

        private void PopularCalendarioReunioesOrdinarias()
        {
            EventoCipa eventoCipa = EventoCipa.GetEventoCipa(cipa, EventoBase.Calendario);
            ReuniaoCipa reuniaoCipa = new ReuniaoCipa();
            if (eventoCipa.GetType() == typeof(ReuniaoCipa))
            {
                reuniaoCipa = (ReuniaoCipa)eventoCipa;
            }
            else
            {
                reuniaoCipa.Find(eventoCipa.Id);
            }
            eventoCipa.Find(eventoCipa.Id);
            eventoCipa.IdCipa.Find();
            eventoCipa.IdCipa.IdCliente.Find();
            txtDataCal.Text = eventoCipa.DataSolicitacao.ToString("dd/MM/yyyy");
            txtHoraCal.Text = eventoCipa.HoraInicio.Trim() == "" ? "--:--" : eventoCipa.HoraInicio;
            txtHoraCal2.Text = eventoCipa.HoraFim.Trim() == "" ? "--:--" : eventoCipa.HoraFim;
            txtLocalCal.Text = eventoCipa.Local;

            PopulaGrdReunioes();
        }

        private void PopulaEventosInicio()
        {
            txtSindicato.TextChanged += txtSindicato_TextChanged;
            txtTerminoMandato.TextChanged += txtTerminoMandato_TextChanged;
            txtEmailsAlerta.TextChanged += txtEmailsAlerta_TextChanged;
        }

        private void PopulaEventosComissaoEleitoral()
        {
            txtLocalizaCandidatos.TextChanged += txtLocalizarCandidatos_TextChanged;
            dataCalendario_Comissao.TextChanged += dataCalendario_Comissao_TextChanged;
            ImageButton1.Click += subir_Click;
            ImageButton2.Click += direita_Click;
            ImageButton3.Click += esquerda_Click;
            ImageButton4.Click += descer_Click;
        }

        private void PopulaEventosEdital()
        {
            dataCalendario_Edital.TextChanged += dataCalendario_Edital_TextChanged;
            txt_horario_local.TextChanged += txt_horario_local_TextChanged;
            txt_local.TextChanged += txt_local_TextChanged;
            btn_folha_inscricao.Click += btn_folha_inscricao_Click;
            btn_folha_comprovante.Click += btn_folha_comprovante_Click;
            btn_edital.Click += btn_edital_Click;
        }

        private void PopulaEventosComunicacao()
        {
            data_comunicacao.TextChanged += data_comunicacao_TextChanged;
            btn_imprimir_comunicacao.Click += btn_imprimir_comunicacao_Click;
        }


        private void PopulaEventosInicioInscricoes()
        {
            data_inscricoes.TextChanged += data_inscricoes_TextChanged;
        }

        private void PopulaEventosTerminoInscricoes()
        {
            txtDataTermInscricoes.TextChanged += txtDataTerminoInscricoes_TextChanged;
        }

        private void PopulaEventosCandidatos()
        {
            txtLocalizar.TextChanged += txtLocalizar_TextChanged;
            btnSubir.Click += btnSubir_Click;
            btnAdicionar.Click += btnDireita_Click;
            btnRemover.Click += btnEsquerda_Click;
            btnDescer.Click += btnDescer_Click;
        }

        private void PopulaEventosCedulaVotacao()
        {
            txtDataCedula.TextChanged += txtDataCedula_TextChanged;
            btnImprimirCedulas.Click += btnImprimirCedulas_Click;
        }

        private void PopulaEventosAtaEleicaoPosse()
        {
            txtDataCalendario.TextChanged += txtDataCalendario_TextChanged;
            txtHoraReuniao.TextChanged += txtHoraReuniao_TextChanged;
            txtHoraReuniao2.TextChanged += txtHoraReuniao2_TextChanged;
            txtLocal.TextChanged += txtLocal_TextChanged;
            txtBranco.TextChanged += txtBranco_TextChanged;
            txtNulo.TextChanged += txtNulo_TextChanged;
            btnAtaEleicao1.Click += btnAtaEleicao1_Click;
            btnApurarVotos.Click += btnApurarVotos_Click;
            btnFolhaApuracao.Click += btnFolhaApuracao_Click;
        }
        protected void rbMembros_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            txtLocalizaCandidatos.Text = "";
            grd_Presidente_Vice.Columns[1].Visible = true;
            grd_Presidente_Vice.Columns[1].Width = 220;
            grd_Presidente_Vice.Columns[0].Width = 220;
            grd_Presidente_Vice.Width = 440;
            PopulaGridPresidenteVice();
        }

        private void PopulaGridPresidenteVice()
        {
            grd_Presidente_Vice.DataSource = null;
            grd_Presidente_Vice.DataBind();
            EventoCipa eventoCipa = EventoCipa.GetEventoCipa(cipa, EventoBase.ComissaoEleitoral);
            Cipa cipaGestaoAtual = cliente.GetGestaoAtual();
            string strWhere;
            int selectedValue = Convert.ToInt32(rbMembros.SelectedValue);
            switch (selectedValue)
            {
                case 1:
                    grd_Presidente_Vice.Items.Clear();
                    grd_Presidente_Vice.DataSource = null;
                    grd_Presidente_Vice.DataBind();
                    strWhere = "IdCipa =" + cipaGestaoAtual.Id
                    + " AND Numero = 0 "
                    + " AND IndGrupoMembro IN (0, 1) "
                    + " AND IndStatus=" + ((int)MembroCipa.Status.Ativo).ToString()
                    + " AND IndTitularSuplente=" + (int)TitularSuplente.Titular
                    + " AND NomeMembro NOT IN(SELECT NomeMembro FROM MembroComissaoEleitoral WHERE IdCipa=" + eventoCipa.IdCipa.Id + " AND NomeMembro IS NOT NULL)"
                    + " AND IdEmpregado NOT IN(SELECT nID_EMPREGADO FROM Sied_Novo.dbo.tblEMPREGADO Where tNO_EMPG IN(SELECT NomeMembro FROM MembroComissaoEleitoral"
                    + " WHERE IdCipa = " + cipa.Id + " AND NomeMembro IS NOT NULL))"
                    + " ORDER BY Numero";

                    DataSet presidentes = new MembroCipa().Get(strWhere);
                    DataTable zDt = new DataTable();
                    zDt.Columns.Add("IdEmprcImaegado");
                    zDt.Columns.Add("Empregado");
                    zDt.Columns.Add("Cargo");

                    foreach (DataRow row in presidentes.Tables[0].Rows)
                    {
                        Empregado empregado = new Empregado();
                        empregado.Find(Convert.ToInt32(row["IdEmpregado"]));

                        MembroCipa membroCipa = new MembroCipa();
                        membroCipa.Find(Convert.ToInt32(row["IdMembroCipa"]));
                        string cargoCipa = membroCipa.GetNomeCargo();

                        zDt.Rows.Add(row["IdEmpregado"], empregado.tNO_EMPG, cargoCipa);
                    }

                    DataSet zDs = new DataSet();
                    zDs.Tables.Add(zDt);
                    grd_Presidente_Vice.DataSource = zDs;
                    grd_Presidente_Vice.DataBind();
                    break;
                case 2:
                    grd_Presidente_Vice.Items.Clear();
                    grd_Presidente_Vice.DataSource = null;
                    grd_Presidente_Vice.DataBind();
                    strWhere = "IdCipa =" + cipaGestaoAtual.Id
                        + " AND IndStatus=" + ((int)MembroCipa.Status.Ativo).ToString()
                        + " AND IdEmpregado Not IN (SELECT nID_EMPREGADO FROM Sied_Novo.dbo.tblEMPREGADO Where tNO_EMPG IN(SELECT NomeMembro FROM MembroComissaoEleitoral(NOLOCK) WHERE IdCipa = " + cipa.Id + "))"
                        + " UNION"
                        + " SELECT MembroCipa.IdMembroCipa, IdCipa, IdEmpregado, NomeMembro, Numero, Estabilidade, IndTitularSuplente, IndGrupoMembro, IndStatus FROM MembroCipa"
                        + " Where IdCipa = " + cipa.Id + " AND IdEmpregado IS NULL AND NomeMembro NOT IN (SELECT NomeMembro FROM MembroComissaoEleitoral "
                        + " WHERE IdCipa = " + cipa.Id +")"
                        + " ORDER BY Numero";

                    DataSet todosIntegrantes = new MembroCipa().Get(strWhere);
                    zDt = new DataTable();
                    zDt.Columns.Add("IdEmprcImaegado");
                    zDt.Columns.Add("Empregado");
                    zDt.Columns.Add("Cargo");

                    ArrayList membros = new MembroComissaoEleitoral().Find("IdCipa=" + eventoCipa.IdCipa.Id);

                    foreach (DataRow row in todosIntegrantes.Tables[0].Rows)
                    {
                        Empregado empregado = new Empregado();
                        MembroCipa membroCipa = new MembroCipa();
                        if (row["IdEmpregado"] != DBNull.Value)
                        {
                            empregado.Find(Convert.ToInt32(row["IdEmpregado"]));
                            membroCipa.Find(Convert.ToInt32(row["IdMembroCipa"]));
                            string cargoCipa = membroCipa.GetNomeCargo();
                            zDt.Rows.Add(row["IdEmpregado"], empregado.tNO_EMPG, cargoCipa);
                        }
                        else
                        {
                            membroCipa.Find(Convert.ToInt32(row["IdMembroCipa"]));
                            string cargoCipa = membroCipa.GetNomeCargo();
                            zDt.Rows.Add(0, membroCipa.NomeMembro, cargoCipa);
                        }
                    }

                    zDs = new DataSet();
                    zDs.Tables.Add(zDt);
                    grd_Presidente_Vice.DataSource = zDs;
                    grd_Presidente_Vice.DataBind();
                    break;
                case 3:
                    grd_Presidente_Vice.Items.Clear();
                    grd_Presidente_Vice.DataSource = null;
                    grd_Presidente_Vice.DataBind();
                    grd_Presidente_Vice.Columns[1].Visible = false;
                    grd_Presidente_Vice.Columns[1].Width = 0;
                    grd_Presidente_Vice.Columns[0].Width = 420;
                    grd_Presidente_Vice.Width = 440;
                    strWhere = "nID_EMPR=" + cliente.Id + " AND hDT_DEM IS NULL AND tNO_EMPG NOT IN(SELECT NomeMembro FROM Opsa.dbo.MembroComissaoEleitoral (NOLOCK)  WHERE IdCipa= " + cipa.Id +")ORDER BY tNO_EMPG";
                    DataSet empregados = new Empregado().Get(strWhere);
                    zDt = new DataTable();
                    zDt.Columns.Add("IdEmprcImaegado");
                    zDt.Columns.Add("Empregado");
                    zDt.Columns.Add("Cargo");

                    foreach (DataRow row in empregados.Tables[0].Rows)
                    {
                        zDt.Rows.Add(row["nID_EMPREGADO"], row["tNO_EMPG"], "");
                    }

                    zDs = new DataSet();
                    zDs.Tables.Add(zDt);
                    grd_Presidente_Vice.DataSource = zDs;
                    grd_Presidente_Vice.DataBind();
                    break;
            }
        }

        protected void txtSindicato_TextChanged(object sender, EventArgs e)
        {
            cliente.IdSindicato.Find();
            txtSindicato.Text = cliente.IdSindicato.NomeAbreviado.Trim();
        }

        protected void txtEmailsAlerta_TextChanged(object sender, EventArgs e)
        {
            if (txtEmailsAlerta.Text.Trim() != "")
            {
                string[] emails = txtEmailsAlerta.Text.Split(';');
                foreach (string email in emails)
                {
                    if (email.Trim() != "")
                    {
                        try
                        {
                            var emailAddress = new System.Net.Mail.MailAddress(email);
                        }
                        catch
                        {
                            MsgBox1.Show("Ilitera.Net", "E - mail de inválido para alertas da CIPA!" + System.Environment.NewLine + email, null, new EO.Web.MsgBoxButton("OK"));
                            PopulaInicio(cipa, juridica);
                            string script = @"
                                    <script type='text/javascript'>
                                        setTimeout(function() {
                                            var contentElement = document.getElementById('ctl00_MainContent_MsgBox1_msg_t');
                                            if (contentElement) {
                                                contentElement.style.color = 'rgb(125, 123, 123)';
                                                contentElement.style.fontSize = '14px';
                                                contentElement.style.fontStyle = 'normal';
                                                contentElement.style.fontVariant = 'normal';
                                                contentElement.style.fontWeight = '500';
                                                contentElement.style.fontFamily = 'UniviaPro';
                                            }
                                        }, 100); 
                                        setTimeout(function() {
                                            var contentElement = document.getElementById('ctl00_MainContent_MsgBox1_headerHtml');
                                            if (contentElement) {
                                                contentElement.style.color = 'rgb(28, 148, 137)';
                                                contentElement.style.fontSize = '14px';
                                                contentElement.style.fontStyle = 'normal';
                                                contentElement.style.fontVariant = 'normal';
                                                contentElement.style.fontWeight = '500';
                                                contentElement.style.fontFamily = 'UniviaPro';
                                            }
                                        }, 100);
                                    </script>";
                            ClientScript.RegisterStartupScript(this.GetType(), "ApplyMsgBoxStyles", script);
                            return;
                        }

                    }
                }
                cipa.ListaEmailsAviso = txtEmailsAlerta.Text;
                cipa.Save();
            }
        }

        protected void txtTerminoMandato_TextChanged(object sender, EventArgs e)
        {
            txtTerminoMandato.Text = cipa.GetTerminoMandato().ToString("dd/MM/yyyy");
        }

        protected void dataCalendario_Comissao_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DateTime dataComissao = DateTime.ParseExact(dataCalendario_Comissao.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                cipa.ComissaoEleitoral = dataComissao;
                cipa.Save();
            }
            catch (Exception ex)
            {
                MsgBox1.Show("Ilitera.Net", "Data inválida", null, new EO.Web.MsgBoxButton("OK"));
            }
            finally
            {
                dataCalendario_Comissao.Text = cipa.ComissaoEleitoral.ToString("dd/MM/yyyy");
            }
        }

        protected void subir_Click(object sender, ImageClickEventArgs e)
        {
            grd_Membros.DataSource = null;
            grd_Presidente_Vice.DataSource = null;
            EventoCipa eventoCipa = EventoCipa.GetEventoCipa(cipa, EventoBase.ComissaoEleitoral);
            eventoCipa.IdCipa.Find();
            for (int i = 0; i < grd_Presidente_Vice.Items.Count; i++)
            {
                MembroComissaoEleitoral membroComissaoEleitoral = new MembroComissaoEleitoral();
                membroComissaoEleitoral.Inicialize();
                membroComissaoEleitoral.NomeMembro = Convert.ToString(grd_Presidente_Vice.Items[i].Cells["Empregado"].Value);
                if (grd_Presidente_Vice.Items[i].Cells["Cargo"].Value.ToString().Trim() != "")
                {
                    membroComissaoEleitoral.NomeCargo = Convert.ToString(grd_Presidente_Vice.Items[i].Cells["Cargo"].Value);
                }

                membroComissaoEleitoral.IdCipa.Id = eventoCipa.IdCipa.Id;
                membroComissaoEleitoral.Save();

            }
            DataSet membros = new MembroComissaoEleitoral().Get("IdCipa=" + eventoCipa.IdCipa.Id);
            DataTable zDt = new DataTable();
            zDt.Columns.Add("IdEmprcImaegado");
            zDt.Columns.Add("NomeMembro");
            zDt.Columns.Add("NomeCargo");

            foreach (DataRow row in membros.Tables[0].Rows)
            {
                zDt.Rows.Add(row["IdMembroComissaoEleitoral"], row["NomeMembro"], row["NomeCargo"]);

            }

            DataSet zDs = new DataSet();
            zDs.Tables.Add(zDt);
            grd_Membros.DataSource = zDs;
            grd_Membros.DataBind();

            PopulaGridPresidenteVice();
            PopulaCandidatos();
        }

        protected void direita_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                EventoCipa eventoCipa = EventoCipa.GetEventoCipa(cipa, EventoBase.ComissaoEleitoral);
                eventoCipa.IdCipa.Find();
                MembroComissaoEleitoral membroComissaoEleitoral = new MembroComissaoEleitoral();
                membroComissaoEleitoral.Inicialize();
                membroComissaoEleitoral.NomeMembro = Convert.ToString(grd_Presidente_Vice.Items[grd_Presidente_Vice.SelectedItemIndex].Cells["Empregado"].Value);
                if (grd_Presidente_Vice.Items[grd_Presidente_Vice.SelectedItemIndex].Cells["Cargo"].Value.ToString().Trim() != "")
                {
                    membroComissaoEleitoral.NomeCargo = Convert.ToString(grd_Presidente_Vice.Items[grd_Presidente_Vice.SelectedItemIndex].Cells["Cargo"].Value);
                }

                membroComissaoEleitoral.IdCipa.Id = eventoCipa.IdCipa.Id;
                membroComissaoEleitoral.Save();
                DataSet membros = new MembroComissaoEleitoral().Get("IdCipa=" + eventoCipa.IdCipa.Id);
                DataTable zDt = new DataTable();
                zDt.Columns.Add("IdEmprcImaegado");
                zDt.Columns.Add("NomeMembro");
                zDt.Columns.Add("NomeCargo");

                foreach (DataRow row in membros.Tables[0].Rows)
                {
                    zDt.Rows.Add(row["IdMembroComissaoEleitoral"], row["NomeMembro"], row["NomeCargo"]);

                }

                DataSet zDs = new DataSet();
                zDs.Tables.Add(zDt);
                grd_Membros.DataSource = zDs;
                grd_Membros.DataBind();

                PopulaGridPresidenteVice();
                PopulaCandidatos();
            }
            catch (ArgumentOutOfRangeException)
            {
                MsgBox1.Show("Ilitera.Net", "Nenhum item foi selecionado", null, new EO.Web.MsgBoxButton("OK"));
                string script = @"
                                    <script type='text/javascript'>
                                        setTimeout(function() {
                                            var contentElement = document.getElementById('ctl00_MainContent_MsgBox1_msg_t');
                                            if (contentElement) {
                                                contentElement.style.color = 'rgb(125, 123, 123)';
                                                contentElement.style.fontSize = '14px';
                                                contentElement.style.fontStyle = 'normal';
                                                contentElement.style.fontVariant = 'normal';
                                                contentElement.style.fontWeight = '500';
                                                contentElement.style.fontFamily = 'UniviaPro';
                                            }
                                        }, 100); 
                                        setTimeout(function() {
                                            var contentElement = document.getElementById('ctl00_MainContent_MsgBox1_headerHtml');
                                            if (contentElement) {
                                                contentElement.style.color = 'rgb(28, 148, 137)';
                                                contentElement.style.fontSize = '14px';
                                                contentElement.style.fontStyle = 'normal';
                                                contentElement.style.fontVariant = 'normal';
                                                contentElement.style.fontWeight = '500';
                                                contentElement.style.fontFamily = 'UniviaPro';
                                            }
                                        }, 100);
                                    </script>";
                ClientScript.RegisterStartupScript(this.GetType(), "ApplyMsgBoxStyles", script);
            }
        }

        protected void esquerda_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                EventoCipa eventoCipa = EventoCipa.GetEventoCipa(cipa, EventoBase.ComissaoEleitoral);
                eventoCipa.IdCipa.Find();
                MembroComissaoEleitoral membroComissaoEleitoral = new MembroComissaoEleitoral();
                membroComissaoEleitoral.Find(Convert.ToInt32(grd_Membros.Items[grd_Membros.SelectedItemIndex].Key));
                membroComissaoEleitoral.Delete();
                DataSet membros = new MembroComissaoEleitoral().Get("IdCipa=" + eventoCipa.IdCipa.Id);
                DataTable zDt = new DataTable();
                zDt.Columns.Add("IdEmprcImaegado");
                zDt.Columns.Add("NomeMembro");
                zDt.Columns.Add("NomeCargo");

                foreach (DataRow row in membros.Tables[0].Rows)
                {
                    zDt.Rows.Add(row["IdMembroComissaoEleitoral"], row["NomeMembro"], row["NomeCargo"]);

                }

                DataSet zDs = new DataSet();
                zDs.Tables.Add(zDt);
                grd_Membros.DataSource = zDs;
                grd_Membros.DataBind();

                PopulaGridPresidenteVice();
                PopulaCandidatos();
            }
            catch (ArgumentOutOfRangeException)
            {
                MsgBox1.Show("Ilitera.Net", "Nenhum item foi selecionado", null, new EO.Web.MsgBoxButton("OK"));
                string script = @"
                                    <script type='text/javascript'>
                                        setTimeout(function() {
                                            var contentElement = document.getElementById('ctl00_MainContent_MsgBox1_msg_t');
                                            if (contentElement) {
                                                contentElement.style.color = 'rgb(125, 123, 123)';
                                                contentElement.style.fontSize = '14px';
                                                contentElement.style.fontStyle = 'normal';
                                                contentElement.style.fontVariant = 'normal';
                                                contentElement.style.fontWeight = '500';
                                                contentElement.style.fontFamily = 'UniviaPro';
                                            }
                                        }, 100); 
                                        setTimeout(function() {
                                            var contentElement = document.getElementById('ctl00_MainContent_MsgBox1_headerHtml');
                                            if (contentElement) {
                                                contentElement.style.color = 'rgb(28, 148, 137)';
                                                contentElement.style.fontSize = '14px';
                                                contentElement.style.fontStyle = 'normal';
                                                contentElement.style.fontVariant = 'normal';
                                                contentElement.style.fontWeight = '500';
                                                contentElement.style.fontFamily = 'UniviaPro';
                                            }
                                        }, 100);
                                    </script>";
                ClientScript.RegisterStartupScript(this.GetType(), "ApplyMsgBoxStyles", script);
            }
        }

        protected void descer_Click(object sender, ImageClickEventArgs e)
        {
            EventoCipa eventoCipa = EventoCipa.GetEventoCipa(cipa, EventoBase.ComissaoEleitoral);
            eventoCipa.IdCipa.Find();
            for (int i = 0; i < grd_Membros.Items.Count; i++)
            {
                MembroComissaoEleitoral membroComissaoEleitoral = new MembroComissaoEleitoral();
                membroComissaoEleitoral.Find(Convert.ToInt32(grd_Membros.Items[i].Key));
                membroComissaoEleitoral.Delete();
            }
            DataSet membros = new MembroComissaoEleitoral().Get("IdCipa=" + eventoCipa.IdCipa.Id);
            DataTable zDt = new DataTable();
            zDt.Columns.Add("IdEmprcImaegado");
            zDt.Columns.Add("NomeMembro");
            zDt.Columns.Add("NomeCargo");

            foreach (DataRow row in membros.Tables[0].Rows)
            {
                zDt.Rows.Add(row["IdMembroComissaoEleitoral"], row["NomeMembro"], row["NomeCargo"]);

            }

            DataSet zDs = new DataSet();
            zDs.Tables.Add(zDt);
            grd_Membros.DataSource = zDs;
            grd_Membros.DataBind();

            PopulaGridPresidenteVice();
            PopulaCandidatos();
        }

        protected void dataCalendario_Edital_TextChanged(object sender, EventArgs e)
        {
            EventoCipa eventoCipa = new EventoCipa();
            try
            {
                DateTime dataEdital = DateTime.ParseExact(dataCalendario_Edital.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

                eventoCipa = EventoCipa.GetEventoCipa(cipa, EventoBase.Edital);
                eventoCipa.IdCipa.Find();
                eventoCipa.IdCipa.IdCliente.Find();
                eventoCipa.Find(eventoCipa.Id);

                eventoCipa.DataSolicitacao = dataEdital;
                eventoCipa.Save();
                cipa.Edital = dataEdital;
                cipa.Save();
            }
            catch (Exception ex)
            {
                MsgBox1.Show("Ilitera.Net", "Data inválida", null, new EO.Web.MsgBoxButton("OK"));
            }
            finally
            {
                dataCalendario_Edital.Text = cipa.Edital.ToString("dd/MM/yyyy");
            }
        }

        protected void btn_folha_inscricao_Click(object sender, EventArgs e)
        {

            EventoCipa eventoCipa = EventoCipa.GetEventoCipa(cipa, EventoBase.Edital);
            RptInscricaoDeCandidatos report = new RptInscricaoDeCandidatos();
            report = new DataSourceCipa(eventoCipa).GetReportInscricaoDeCandidatos();
            CreatePDFDocument(report, this.Response, "RptFolhaDeInscricao");
        }

        protected void txt_horario_local_TextChanged(object sender, EventArgs e)
        {
            EventoCipa eventoCipa = new EventoCipa();
            try
            {
                DateTime horario = DateTime.ParseExact(txt_horario_local.Text, "HH:mm", System.Globalization.CultureInfo.InvariantCulture);

                eventoCipa = EventoCipa.GetEventoCipa(cipa, EventoBase.Edital);
                eventoCipa.IdCipa.Find();
                eventoCipa.IdCipa.IdCliente.Find();
                eventoCipa.Find(eventoCipa.Id);

                eventoCipa.HoraInicio = horario.ToString("HH:mm");
                eventoCipa.Save();
            }
            catch (Exception)
            {
                MsgBox1.Show("Ilitera.Net", "Horário inválido", null, new EO.Web.MsgBoxButton("OK"));
            }
            finally
            {
                txt_horario_local.Text = eventoCipa.HoraInicio;
            }
        }

        protected void txt_local_TextChanged(object sender, EventArgs e)
        {
            EventoCipa eventoCipa = new EventoCipa();
            try
            {
                string local = txt_local.Text;

                eventoCipa = EventoCipa.GetEventoCipa(cipa, EventoBase.Edital);
                eventoCipa.Local = local;
                eventoCipa.Save();
            }
            catch (Exception)
            {
                MsgBox1.Show("Ilitera.Net", "Erro ao tentar alterar local", null, new EO.Web.MsgBoxButton("OK"));
            }
            finally
            {
                txt_local.Text = eventoCipa.Local;
            }
        }


        protected void btn_folha_comprovante_Click(object sender, EventArgs e)
        {

            EventoCipa eventoCipa = EventoCipa.GetEventoCipa(cipa, EventoBase.Edital);
            RptComprovanteDeInscricao report = new RptComprovanteDeInscricao();
            report = new DataSourceCipa(eventoCipa).GetReportComprovanteInscricao();
            CreatePDFDocument(report, this.Response, "RptComprovanteDeInscricao");
        }

        protected void btn_edital_Click(object sender, EventArgs e)
        {
            EventoCipa eventoCipa = EventoCipa.GetEventoCipa(cipa, EventoBase.Edital);
            ReportClass report = new RptEdital();
            report = new DataSourceCipa(eventoCipa).GetReport();
            CreatePDFDocument(report, this.Response, "RptEdital");
        }

        protected static void CreatePDFDocument(ReportClass report, HttpResponse response)
        {
            CreatePDFDocument(report, response, false, string.Empty);
        }

        protected static void CreatePDFDocument(ReportClass report, HttpResponse response, string DocumentName)
        {
            CreatePDFDocument(report, response, false, DocumentName);
        }

        protected static void CreatePDFDocument(ReportClass report, HttpResponse response, bool forceDownload, string DownloadfileName)
        {
            using (Stream pdfStream = report.ExportToStream(ExportFormatType.PortableDocFormat))
            {
                // Copy the content to a MemoryStream
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    byte[] buffer = new byte[4096];
                    int bytesRead;
                    while ((bytesRead = pdfStream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        memoryStream.Write(buffer, 0, bytesRead);
                    }

                    // Ensure the memory stream position is set to the beginning
                    memoryStream.Position = 0;

                    // Configure the HTTP response
                    response.Clear();
                    response.Buffer = true;
                    response.ContentType = "application/pdf";
                    response.AddHeader("Content-Disposition", "initial; filename=" + DownloadfileName + ".pdf");
                    response.BinaryWrite(memoryStream.ToArray());
                    response.End();
                }
            }
            //report.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, response, false, null);
        }

        protected void data_comunicacao_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DateTime dataComunicacao = DateTime.ParseExact(data_comunicacao.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                cipa.ComunicacaoSindicato = dataComunicacao;
                cipa.Save();
            }
            catch (Exception ex)
            {
                MsgBox1.Show("Ilitera.Net", "Data inválida", null, new EO.Web.MsgBoxButton("OK"));
            }
            finally
            {
                data_comunicacao.Text = cipa.ComunicacaoSindicato.ToString("dd/MM/yyyy");
            }
        }

        protected void btn_imprimir_publicacao_Click(object sender, ImageClickEventArgs e)
        {
            EventoCipa eventoCipa = EventoCipa.GetEventoCipa(cipa, EventoBase.Publicacao);
            ReportClass report = new ReportClass();
            report = new DataSourceCipa(eventoCipa).GetReport();
            CreatePDFDocument(report, this.Response);
        }


        protected void data_inscricoes_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DateTime dataInscricoes = DateTime.ParseExact(data_inscricoes.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                cipa.InicioInscricao = dataInscricoes;
                cipa.Save();
            }
            catch (Exception ex)
            {
                MsgBox1.Show("Ilitera.Net", "Data inválida", null, new EO.Web.MsgBoxButton("OK"));
            }
            finally
            {
                data_inscricoes.Text = cipa.InicioInscricao.ToString("dd/MM/yyyy");
            }
        }

        protected void txtDataTerminoInscricoes_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DateTime dataTermino = DateTime.ParseExact(txtDataTermInscricoes.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                cipa.TerminoInscricao = dataTermino;
                cipa.Save();
            }
            catch (Exception ex)
            {
                MsgBox1.Show("Ilitera.Net", "Data inválida", null, new EO.Web.MsgBoxButton("OK"));
            }
            finally
            {
                txtDataTermInscricoes.Text = cipa.TerminoInscricao.ToString("dd/MM/yyyy");
            }

        }

        protected void txtLocalizar_TextChanged(object sender, EventArgs e)
        {
            lstEmpregados.Items.Clear();
            string localizar = txtLocalizar.Text;
            System.Text.StringBuilder strEmpregados = new System.Text.StringBuilder();

            strEmpregados.Append("nID_EMPR =" + cipa.IdCliente.Id + " AND tNO_EMPG Like '%" + localizar + "%'");
            strEmpregados.Append(" AND hDT_DEM IS NULL");
            strEmpregados.Append(" ORDER BY tNO_EMPG ");

            DataSet zDs = new Empregado().Get(strEmpregados.ToString());

            for (int zCont = 0; zCont < zDs.Tables[0].Rows.Count; zCont++)
            {
                string texto = zDs.Tables[0].Rows[zCont][1].ToString();
                string valor = zDs.Tables[0].Rows[zCont][0].ToString();

                ListItem item = new ListItem(texto, valor);
                lstEmpregados.Items.Add(item);
            }
        }

        protected void btnSubir_Click(object sender, ImageClickEventArgs e)
        {
            EventoCipa eventoCipa = EventoCipa.GetEventoCipa(cipa, EventoBase.ListaDosCandidatos);
            eventoCipa.IdCipa.Find();
            for (int i = 0; i < lstEmpregados.Items.Count; i++)
            {
                ParticipantesEleicaoCipa candidato = new ParticipantesEleicaoCipa();
                candidato.Inicialize();
                candidato.IdCipa = cipa;
                candidato.IdEmpregado.Id = Convert.ToInt32(lstEmpregados.Items[i].Value);
                candidato.Save();

            }
            lstEmpregados.Items.Clear();
            Grid4.Items.Clear();
            System.Text.StringBuilder strMembros = new System.Text.StringBuilder();
            strMembros.Append("IdCipa=" + cipa.Id);
            strMembros.Append(" ORDER BY Votos DESC,");
            strMembros.Append(" (SELECT hDT_ADM FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEMPREGADO WHERE ");
            strMembros.Append(" nID_EMPREGADO = ParticipantesEleicaoCipa.IdEmpregado)");

            DataSet membros = new ParticipantesEleicaoCipa().Get(strMembros.ToString());
            DataTable zDt = new DataTable();
            zDt.Columns.Add("IdEmprcImaegado");
            zDt.Columns.Add("Editar");
            zDt.Columns.Add("Nome");
            zDt.Columns.Add("Setor");
            zDt.Columns.Add("Apelido");

            foreach (DataRow row in membros.Tables[0].Rows)
            {
                Empregado empregado = new Empregado();
                empregado.Find(Convert.ToInt32(row["IdEmpregado"]));
                EmpregadoFuncao empregadoFuncao = EmpregadoFuncao.GetEmpregadoFuncao(empregado);
                zDt.Rows.Add(row["IdParticipantesEleicaoCipa"], "Editar", empregado.tNO_EMPG, empregadoFuncao.GetNomeSetor(), empregado.tNO_APELIDO);
            }


            DataSet zDs = new DataSet();
            zDs.Tables.Add(zDt);
            Grid4.DataSource = zDs;
            Grid4.DataBind();
            txtLocalizar.Text = "";

            PopulaCriacaoComissaoEleitoral();
            PopulaAtaEleicaoPosse();
        }

        protected void btnDireita_Click(object sender, ImageClickEventArgs e)
        {
            if (lstEmpregados.SelectedItem != null)
            {
                EventoCipa eventoCipa = EventoCipa.GetEventoCipa(cipa, EventoBase.ListaDosCandidatos);
                eventoCipa.IdCipa.Find();
                foreach (ListItem item in lstEmpregados.Items)
                {
                    if (item.Selected)
                    {
                        ParticipantesEleicaoCipa candidato = new ParticipantesEleicaoCipa();
                        candidato.Inicialize();
                        candidato.IdCipa = cipa;
                        candidato.IdEmpregado.Id = Convert.ToInt32(item.Value);
                        candidato.Save();
                    }
                }

                lstEmpregados.Items.Clear();
                System.Text.StringBuilder strEmpregados = new System.Text.StringBuilder();

                strEmpregados.Append("nID_EMPR =" + cipa.IdCliente.Id);
                strEmpregados.Append(" AND hDT_DEM IS NULL");
                strEmpregados.Append(" ORDER BY tNO_EMPG ");

                DataSet zDs = new Empregado().Get(strEmpregados.ToString());

                for (int zCont = 0; zCont < zDs.Tables[0].Rows.Count; zCont++)
                {
                    string texto = zDs.Tables[0].Rows[zCont][1].ToString();
                    string valor = zDs.Tables[0].Rows[zCont][0].ToString();

                    ListItem item = new ListItem(texto, valor);
                    lstEmpregados.Items.Add(item);
                }
                lstEmpregados.DataBind();

                Grid4.Items.Clear();
                System.Text.StringBuilder strMembros = new System.Text.StringBuilder();
                strMembros.Append("IdCipa=" + cipa.Id);
                strMembros.Append(" ORDER BY Votos DESC,");
                strMembros.Append(" (SELECT hDT_ADM FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEMPREGADO WHERE ");
                strMembros.Append(" nID_EMPREGADO = ParticipantesEleicaoCipa.IdEmpregado)");

                DataSet membros = new ParticipantesEleicaoCipa().Get(strMembros.ToString());
                DataTable zDt = new DataTable();
                zDt.Columns.Add("IdEmprcImaegado");
                zDt.Columns.Add("Editar");
                zDt.Columns.Add("Nome");
                zDt.Columns.Add("Setor");
                zDt.Columns.Add("Apelido");

                foreach (DataRow row in membros.Tables[0].Rows)
                {
                    Empregado empregado = new Empregado();
                    empregado.Find(Convert.ToInt32(row["IdEmpregado"]));
                    EmpregadoFuncao empregadoFuncao = EmpregadoFuncao.GetEmpregadoFuncao(empregado);
                    zDt.Rows.Add(row["IdParticipantesEleicaoCipa"], "Editar", empregado.tNO_EMPG, empregadoFuncao.GetNomeSetor(), empregado.tNO_APELIDO);
                }


                zDs = new DataSet();
                zDs.Tables.Add(zDt);
                Grid4.DataSource = zDs;
                Grid4.DataBind();
                txtLocalizar.Text = "";

                PopulaCriacaoComissaoEleitoral();
                PopulaAtaEleicaoPosse();
            }else
            {
                MsgBox1.Show("Ilitera.Net", "Nenhum item foi selecionado", null, new EO.Web.MsgBoxButton("OK"));
                string script = @"
                                    <script type='text/javascript'>
                                        setTimeout(function() {
                                            var contentElement = document.getElementById('ctl00_MainContent_MsgBox1_msg_t');
                                            if (contentElement) {
                                                contentElement.style.color = 'rgb(125, 123, 123)';
                                                contentElement.style.fontSize = '14px';
                                                contentElement.style.fontStyle = 'normal';
                                                contentElement.style.fontVariant = 'normal';
                                                contentElement.style.fontWeight = '500';
                                                contentElement.style.fontFamily = 'UniviaPro';
                                            }
                                        }, 100); 
                                        setTimeout(function() {
                                            var contentElement = document.getElementById('ctl00_MainContent_MsgBox1_headerHtml');
                                            if (contentElement) {
                                                contentElement.style.color = 'rgb(28, 148, 137)';
                                                contentElement.style.fontSize = '14px';
                                                contentElement.style.fontStyle = 'normal';
                                                contentElement.style.fontVariant = 'normal';
                                                contentElement.style.fontWeight = '500';
                                                contentElement.style.fontFamily = 'UniviaPro';
                                            }
                                        }, 100);
                                    </script>";
                ClientScript.RegisterStartupScript(this.GetType(), "ApplyMsgBoxStyles", script);
            }
        }

        protected void btnEsquerda_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                EventoCipa eventoCipa = EventoCipa.GetEventoCipa(cipa, EventoBase.ListaDosCandidatos);
                eventoCipa.IdCipa.Find();

                ParticipantesEleicaoCipa participantesEleicaoCipa = new ParticipantesEleicaoCipa();
                participantesEleicaoCipa.Delete("IdParticipantesEleicaoCipa=" + Convert.ToInt32(Grid4.Items[Grid4.SelectedItemIndex].Key));


                lstEmpregados.Items.Clear();
                System.Text.StringBuilder strEmpregados = new System.Text.StringBuilder();

                strEmpregados.Append("nID_EMPR =" + cipa.IdCliente.Id);
                strEmpregados.Append(" AND hDT_DEM IS NULL");
                strEmpregados.Append(" ORDER BY tNO_EMPG ");

                DataSet zDs = new Empregado().Get(strEmpregados.ToString());

                for (int zCont = 0; zCont < zDs.Tables[0].Rows.Count; zCont++)
                {
                    string texto = zDs.Tables[0].Rows[zCont][1].ToString();
                    string valor = zDs.Tables[0].Rows[zCont][0].ToString();

                    ListItem item = new ListItem(texto, valor);
                    lstEmpregados.Items.Add(item);
                }
                lstEmpregados.DataBind();

                Grid4.Items.Clear();
                System.Text.StringBuilder strMembros = new System.Text.StringBuilder();
                strMembros.Append("IdCipa=" + cipa.Id);
                strMembros.Append(" ORDER BY Votos DESC,");
                strMembros.Append(" (SELECT hDT_ADM FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEMPREGADO WHERE ");
                strMembros.Append(" nID_EMPREGADO = ParticipantesEleicaoCipa.IdEmpregado)");

                DataSet membros = new ParticipantesEleicaoCipa().Get(strMembros.ToString());
                DataTable zDt = new DataTable();
                zDt.Columns.Add("IdEmprcImaegado");
                zDt.Columns.Add("Editar");
                zDt.Columns.Add("Nome");
                zDt.Columns.Add("Setor");
                zDt.Columns.Add("Apelido");

                foreach (DataRow row in membros.Tables[0].Rows)
                {
                    Empregado empregado = new Empregado();
                    empregado.Find(Convert.ToInt32(row["IdEmpregado"]));
                    EmpregadoFuncao empregadoFuncao = EmpregadoFuncao.GetEmpregadoFuncao(empregado);
                    zDt.Rows.Add(row["IdParticipantesEleicaoCipa"], "Editar", empregado.tNO_EMPG, empregadoFuncao.GetNomeSetor(), empregado.tNO_APELIDO);
                }

                bool linhaVazia = false;
                if (zDt.Rows.Count < 1)
                {
                    zDt.Rows.Add("", "", "", "", "");
                    linhaVazia = true;
                }

                zDs = new DataSet();
                zDs.Tables.Add(zDt);
                Grid4.DataSource = zDs;
                Grid4.DataBind();
                txtLocalizar.Text = "";

                if (linhaVazia)
                {
                    EO.Web.GridItem lastRow = Grid4.Items[Grid4.Items.Count - 1];
                    foreach (EO.Web.GridCell cell in lastRow.Cells)
                    {
                        cell.Style = "linhaVazia";
                    }
                }
                

                PopulaCriacaoComissaoEleitoral();
                PopulaAtaEleicaoPosse();
            }
            catch (ArgumentOutOfRangeException)
            {
                MsgBox1.Show("Ilitera.Net", "Nenhum item foi selecionado", null, new EO.Web.MsgBoxButton("OK"));
                string script = @"
                                    <script type='text/javascript'>
                                        setTimeout(function() {
                                            var contentElement = document.getElementById('ctl00_MainContent_MsgBox1_msg_t');
                                            if (contentElement) {
                                                contentElement.style.color = 'rgb(125, 123, 123)';
                                                contentElement.style.fontSize = '14px';
                                                contentElement.style.fontStyle = 'normal';
                                                contentElement.style.fontVariant = 'normal';
                                                contentElement.style.fontWeight = '500';
                                                contentElement.style.fontFamily = 'UniviaPro';
                                            }
                                        }, 100); 
                                        setTimeout(function() {
                                            var contentElement = document.getElementById('ctl00_MainContent_MsgBox1_headerHtml');
                                            if (contentElement) {
                                                contentElement.style.color = 'rgb(28, 148, 137)';
                                                contentElement.style.fontSize = '14px';
                                                contentElement.style.fontStyle = 'normal';
                                                contentElement.style.fontVariant = 'normal';
                                                contentElement.style.fontWeight = '500';
                                                contentElement.style.fontFamily = 'UniviaPro';
                                            }
                                        }, 100);
                                    </script>";
                ClientScript.RegisterStartupScript(this.GetType(), "ApplyMsgBoxStyles", script);
            }
        }


        protected void btnDescer_Click(object sender, ImageClickEventArgs e)
        {
            EventoCipa eventoCipa = EventoCipa.GetEventoCipa(cipa, EventoBase.ListaDosCandidatos);
            eventoCipa.IdCipa.Find();
            for (int i = 0; i < Grid4.Items.Count; i++)
            {
                ParticipantesEleicaoCipa participantesEleicaoCipa = new ParticipantesEleicaoCipa();
                participantesEleicaoCipa.Delete("IdParticipantesEleicaoCipa=" + Convert.ToInt32(Grid4.Items[i].Key));
            }


            lstEmpregados.Items.Clear();
            System.Text.StringBuilder strEmpregados = new System.Text.StringBuilder();

            strEmpregados.Append("nID_EMPR =" + cipa.IdCliente.Id);
            strEmpregados.Append(" AND hDT_DEM IS NULL");
            strEmpregados.Append(" ORDER BY tNO_EMPG ");

            DataSet zDs = new Empregado().Get(strEmpregados.ToString());

            for (int zCont = 0; zCont < zDs.Tables[0].Rows.Count; zCont++)
            {
                string texto = zDs.Tables[0].Rows[zCont][1].ToString();
                string valor = zDs.Tables[0].Rows[zCont][0].ToString();

                ListItem item = new ListItem(texto, valor);
                lstEmpregados.Items.Add(item);
            }
            lstEmpregados.DataBind();

            Grid4.Items.Clear();
            System.Text.StringBuilder strMembros = new System.Text.StringBuilder();
            strMembros.Append("IdCipa=" + cipa.Id);
            strMembros.Append(" ORDER BY Votos DESC,");
            strMembros.Append(" (SELECT hDT_ADM FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEMPREGADO WHERE ");
            strMembros.Append(" nID_EMPREGADO = ParticipantesEleicaoCipa.IdEmpregado)");

            DataSet membros = new ParticipantesEleicaoCipa().Get(strMembros.ToString());
            DataTable zDt = new DataTable();
            zDt.Columns.Add("IdEmprcImaegado");
            zDt.Columns.Add("Editar");
            zDt.Columns.Add("Nome");
            zDt.Columns.Add("Setor");
            zDt.Columns.Add("Apelido");

            foreach (DataRow row in membros.Tables[0].Rows)
            {
                Empregado empregado = new Empregado();
                empregado.Find(Convert.ToInt32(row["IdEmpregado"]));
                EmpregadoFuncao empregadoFuncao = EmpregadoFuncao.GetEmpregadoFuncao(empregado);
                zDt.Rows.Add(row["IdParticipantesEleicaoCipa"], "Editar", empregado.tNO_EMPG, empregadoFuncao.GetNomeSetor(), empregado.tNO_APELIDO);
            }


            bool linhaVazia = false;
            if (zDt.Rows.Count < 1)
            {
                zDt.Rows.Add("", "", "", "", "");
                linhaVazia = true;
            }

            zDs = new DataSet();
            zDs.Tables.Add(zDt);
            Grid4.DataSource = zDs;
            Grid4.DataBind();
            txtLocalizar.Text = "";

            if (linhaVazia)
            {
                EO.Web.GridItem lastRow = Grid4.Items[Grid4.Items.Count - 1];
                foreach (EO.Web.GridCell cell in lastRow.Cells)
                {
                    cell.Style = "linhaVazia";
                }
            }

            PopulaCriacaoComissaoEleitoral();
            PopulaAtaEleicaoPosse();
        }

        protected void txtDataCedula_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DateTime dataCedula = DateTime.ParseExact(txtDataCedula.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                cipa.CedulaVotacao = dataCedula;
                cipa.Save();
            }
            catch (Exception ex)
            {
                MsgBox1.Show("Ilitera.Net", "Data inválida", null, new EO.Web.MsgBoxButton("OK"));
            }
            finally
            {
                txtDataCedula.Text = cipa.CedulaVotacao.ToString("dd/MM/yyyy");
            }
        }

        protected void txtDataCalendario_TextChanged(object sender, EventArgs e)
        {
            EleicaoCipa eleicaoCipa = new EleicaoCipa();
            EventoCipa eventoCipa = EventoCipa.GetEventoCipa(cipa, EventoBase.Eleicao);
            if (eventoCipa.GetType() == typeof(EleicaoCipa))
            {
                eleicaoCipa = (EleicaoCipa)eventoCipa;
            }
            else
            {
                eleicaoCipa = new EleicaoCipa();
                eleicaoCipa.Find(eventoCipa.Id);
            }

            try
            {
                DateTime dataCalendario = DateTime.ParseExact(txtDataCalendario.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                eleicaoCipa.DataSolicitacao = dataCalendario;
                eleicaoCipa.Save();
            }
            catch (Exception ex)
            {
                MsgBox1.Show("Ilitera.Net", "Data inválida", null, new EO.Web.MsgBoxButton("OK"));
            }
            finally
            {
                txtDataCalendario.Text = eleicaoCipa.DataSolicitacao.ToString("dd/MM/yyyy");
            }
        }

        protected void txtHoraReuniao_TextChanged(object sender, EventArgs e)
        {
            EventoCipa eventoCipa = new EventoCipa();
            try
            {
                DateTime horario = DateTime.ParseExact(txtHoraReuniao.Text, "HH:mm", System.Globalization.CultureInfo.InvariantCulture);

                eventoCipa = EventoCipa.GetEventoCipa(cipa, EventoBase.Eleicao);
                eventoCipa.Find(eventoCipa.Id);
                eventoCipa.HoraInicio = horario.ToString("HH:mm");
                eventoCipa.Save();
            }
            catch (Exception)
            {
                MsgBox1.Show("Ilitera.Net", "Horário inválido", null, new EO.Web.MsgBoxButton("OK"));
            }
            finally
            {
                txtHoraReuniao.Text = eventoCipa.HoraInicio;
            }
        }

        protected void txtHoraReuniao2_TextChanged(object sender, EventArgs e)
        {
            EventoCipa eventoCipa = new EventoCipa();
            try
            {
                DateTime horario = DateTime.ParseExact(txtHoraReuniao2.Text, "HH:mm", System.Globalization.CultureInfo.InvariantCulture);

                eventoCipa = EventoCipa.GetEventoCipa(cipa, EventoBase.Eleicao);
                eventoCipa.Find(eventoCipa.Id);
                eventoCipa.HoraFim = horario.ToString("HH:mm");
                eventoCipa.Save();
            }
            catch (Exception)
            {
                MsgBox1.Show("Ilitera.Net", "Horário inválido", null, new EO.Web.MsgBoxButton("OK"));
            }
            finally
            {
                txtHoraReuniao2.Text = eventoCipa.HoraFim;
            }
        }

        protected void txtLocal_TextChanged(object sender, EventArgs e)
        {
            EventoCipa eventoCipa = new EventoCipa();
            EleicaoCipa eleicaoCipa = new EleicaoCipa();
            try
            {
                string local = txtLocal.Text;

                eventoCipa = EventoCipa.GetEventoCipa(cipa, EventoBase.Eleicao);
                eventoCipa.Find(eventoCipa.Id);
                if (eventoCipa.GetType() == typeof(EleicaoCipa))
                {
                    eleicaoCipa = (EleicaoCipa)eventoCipa;
                }
                else
                {
                    eleicaoCipa = new EleicaoCipa();
                    eleicaoCipa.Find(eventoCipa.Id);
                }
                eleicaoCipa.Local = local;
                eventoCipa.Local = local;
                eleicaoCipa.Save();
                eventoCipa.Save();
            }
            catch (Exception)
            {
                MsgBox1.Show("Ilitera.Net", "Local inválido", null, new EO.Web.MsgBoxButton("OK"));
            }
            finally
            {
                txtLocal.Text = eventoCipa.Local;
            }
        }

        protected void txtBranco_TextChanged(object sender, EventArgs e)
        {
            EleicaoCipa eleicaoCipa = new EleicaoCipa();
            EventoCipa eventoCipa = EventoCipa.GetEventoCipa(cipa, EventoBase.Eleicao);
            if (eventoCipa.GetType() == typeof(EleicaoCipa))
            {
                eleicaoCipa = (EleicaoCipa)eventoCipa;
            }
            else
            {
                eleicaoCipa = new EleicaoCipa();
                eleicaoCipa.Find(eventoCipa.Id);
            }
            eleicaoCipa.Find(eventoCipa.Id);
            eleicaoCipa.IdCipa.Find();
            eleicaoCipa.IdCipa.IdCliente.Find();
            eleicaoCipa.VotosBrancos = Convert.ToInt32(txtBranco.Text);
            eleicaoCipa.Save();
            PopulaAtaEleicaoPosse();
        }

        protected void txtNulo_TextChanged(object sender, EventArgs e)
        {
            EleicaoCipa eleicaoCipa = new EleicaoCipa();
            EventoCipa eventoCipa = EventoCipa.GetEventoCipa(cipa, EventoBase.Eleicao);
            if (eventoCipa.GetType() == typeof(EleicaoCipa))
            {
                eleicaoCipa = (EleicaoCipa)eventoCipa;
            }
            else
            {
                eleicaoCipa = new EleicaoCipa();
                eleicaoCipa.Find(eventoCipa.Id);
            }
            eleicaoCipa.Find(eventoCipa.Id);
            eleicaoCipa.IdCipa.Find();
            eleicaoCipa.IdCipa.IdCliente.Find();
            eleicaoCipa.VotosNulos = Convert.ToInt32(txtNulo.Text);
            eleicaoCipa.Save();
            PopulaAtaEleicaoPosse();
        }

        protected void btnAtaEleicao1_Click(object sender, EventArgs e)
        {
            EventoCipa eventoCipa = EventoCipa.GetEventoCipa(cipa, EventoBase.Eleicao);
            eventoCipa.Find(eventoCipa.Id);
            EleicaoCipa eleicaoCipa;
            if (eventoCipa.GetType() == typeof(EleicaoCipa))
            {
                eleicaoCipa = (EleicaoCipa)eventoCipa;
            }
            else
            {
                eleicaoCipa = new EleicaoCipa();
                eleicaoCipa.Find(eventoCipa.Id);
            }
            ReportClass report = new DataSourceEleicaoCIPA(eleicaoCipa).GetReport();

            CreatePDFDocument(report, this.Response, "RptEleicao_New");
        }

        protected void btnApurarVotos_Click(object sender, EventArgs e)
        {
            EleicaoCipa eleicaoCipa = new EleicaoCipa();
            EventoCipa eventoCipa = EventoCipa.GetEventoCipa(cipa, EventoBase.Eleicao);
            if (eventoCipa.GetType() == typeof(EleicaoCipa))
            {
                eleicaoCipa = (EleicaoCipa)eventoCipa;
            }
            else
            {
                eleicaoCipa = new EleicaoCipa();
                eleicaoCipa.Find(eventoCipa.Id);
            }

            eleicaoCipa.IdCipa.Find();
            eleicaoCipa.IdCipa.IdCliente.Find();
            DimensionamentoCipa dim = eleicaoCipa.IdCipa.IdCliente.GetDimensionamentoCipa();
            eleicaoCipa.Efetivo = dim.Efetivo;
            eleicaoCipa.Suplente = dim.Suplente;
            try
            {
                eleicaoCipa.ApuracaoVotos(eleicaoCipa.IdCipa.IdCliente.QtdEmpregados);
                MsgBox1.Show("Ilitera.Net", "Votos apurados", null, new EO.Web.MsgBoxButton("OK"));
            }
            catch (SqlException ex)
            {
                MsgBox1.Show("Ilitera.Net", "Para realizar a apuração, retire os membros das reuniões", null, new EO.Web.MsgBoxButton("OK"));
            }
            string script = @"
                                    <script type='text/javascript'>
                                        setTimeout(function() {
                                            var contentElement = document.getElementById('ctl00_MainContent_MsgBox1_msg_t');
                                            if (contentElement) {
                                                contentElement.style.color = 'rgb(125, 123, 123)';
                                                contentElement.style.fontSize = '14px';
                                                contentElement.style.fontStyle = 'normal';
                                                contentElement.style.fontVariant = 'normal';
                                                contentElement.style.fontWeight = '500';
                                                contentElement.style.fontFamily = 'UniviaPro';
                                            }
                                        }, 100); 
                                        setTimeout(function() {
                                            var contentElement = document.getElementById('ctl00_MainContent_MsgBox1_headerHtml');
                                            if (contentElement) {
                                                contentElement.style.color = 'rgb(28, 148, 137)';
                                                contentElement.style.fontSize = '14px';
                                                contentElement.style.fontStyle = 'normal';
                                                contentElement.style.fontVariant = 'normal';
                                                contentElement.style.fontWeight = '500';
                                                contentElement.style.fontFamily = 'UniviaPro';
                                            }
                                        }, 100);
                                    </script>";
            ClientScript.RegisterStartupScript(this.GetType(), "ApplyMsgBoxStyles", script);
            PopulaAtaEleicaoPosse();
        }

        protected void btnFolhaApuracao_Click(object sender, EventArgs e)
        {
            EleicaoCipa eleicaoCipa = new EleicaoCipa();
            EventoCipa eventoCipa = EventoCipa.GetEventoCipa(cipa, EventoBase.Eleicao);
            eventoCipa.Find(eventoCipa.Id);
            if (eventoCipa.GetType() == typeof(EleicaoCipa))
            {
                eleicaoCipa = (EleicaoCipa)eventoCipa;
            }
            else
            {
                eleicaoCipa = new EleicaoCipa();
                eleicaoCipa.Find(eventoCipa.Id);
            }
            DataSourceCipa dataSource = new DataSourceCipa(eleicaoCipa);
            List<CandidatosApurados> lista = new List<CandidatosApurados>();
            if (lista.Count > 0) lista.Clear();

            lista = PopularListaCandidatos();

            string where = "IdCipa=" + eleicaoCipa.IdCipa.Id
                        + " ORDER BY (SELECT tNO_EMPG FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEMPREGADO WHERE"
                        + " nID_EMPREGADO = ParticipantesEleicaoCipa.IdEmpregado)";

            DataSet test = new ParticipantesEleicaoCipa().Get(where);

            foreach (DataRow row in test.Tables[0].Rows)
            {
                int Id = Convert.ToInt32(row["IdEmpregado"]);
                int IdParticipantesEleicaoCipa = Convert.ToInt32(row["IdParticipantesEleicaoCipa"]);
                int IdCipa = Convert.ToInt32(row["IdCipa"]);
                int QtdeVotosRecebidoPeloCandidato = Convert.ToInt32(row["Votos"]);
                int isVicePresidente = Convert.ToInt32(row["isVicePresidente"]);

                CandidatosApurados candidato = lista.Find(c => c.IdEmpregado == Id);
                if (candidato != null)
                {
                    candidato.IdParticipantesEleicaoCipa = IdParticipantesEleicaoCipa;
                    candidato.IdCipa = IdCipa;
                    candidato.QtdeVotosRecebidoPeloCandidato = QtdeVotosRecebidoPeloCandidato;
                    candidato.IsVicePresidente = isVicePresidente == 1 ? true : false;
                }
            }

            lista = lista.OrderByDescending(c => c.QtdeVotosRecebidoPeloCandidato)
                                                      .ThenBy(c => c.DataAdmissao)
                                                      .ToList();
            DimensionamentoCipa dim = cipa.IdCliente.GetDimensionamentoCipa();
            //Representantes do Empregador
            string query = $@"
                        SELECT Titulo = 'Representantes do Empregador', Cargo = '', tNO_EMPG as Nome, Votos = 0
                        FROM (
                            SELECT *, 0 AS order_column
                            FROM MembroCipa a 
                            JOIN [{Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper()}].dbo.tblEMPREGADO b ON a.IdEmpregado = b.nID_EMPREGADO
                            WHERE a.IdCipa = {this.cipa.Id} AND IndGrupoMembro = 1 AND Numero = 0 
    
                            UNION ALL
    
                            SELECT *, 1 AS order_column
                            FROM (
                                    SELECT *
                                    FROM MembroCipa a 
                                    JOIN [{Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper()}].dbo.tblEMPREGADO b ON a.IdEmpregado = b.nID_EMPREGADO
                                    WHERE a.IdCipa = {this.cipa.Id} AND IndGrupoMembro = 1 AND Numero <> 0 AND a.IndTitularSuplente = 1
                            ) AS subquery2
    
                            UNION ALL
    
                            SELECT *, 2 AS order_column
                            FROM (
                                    SELECT *
                                    FROM MembroCipa a 
                                    JOIN [{Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper()}].dbo.tblEMPREGADO b ON a.IdEmpregado = b.nID_EMPREGADO
                                    WHERE a.IdCipa = {this.cipa.Id} AND IndGrupoMembro = 1 AND a.IndTitularSuplente = 2
                            ) AS subquery3
                        ) AS resultado
                        ORDER BY order_column, Numero ASC;";

            using (SqlConnection cnn = new SqlConnection(Ilitera.Data.SQLServer.EntitySQLServer.GetConnection()))
            {
                cnn.Open();
                SqlCommand sqlCommand = new SqlCommand(query, cnn);
                SqlDataReader reader = sqlCommand.ExecuteReader();
                int rowCount = 0;

                while (reader.Read())
                {
                    rowCount++;
                }
                if (rowCount < (dim.Efetivo + dim.Suplente))
                {
                    MsgBox1.Show("Ilitera.Net", "Ainda faltam representantes do empregador", null, new EO.Web.MsgBoxButton("OK"));
                    string script = @"
                                    <script type='text/javascript'>
                                        setTimeout(function() {
                                            var contentElement = document.getElementById('ctl00_MainContent_MsgBox1_msg_t');
                                            if (contentElement) {
                                                contentElement.style.color = 'rgb(125, 123, 123)';
                                                contentElement.style.fontSize = '14px';
                                                contentElement.style.fontStyle = 'normal';
                                                contentElement.style.fontVariant = 'normal';
                                                contentElement.style.fontWeight = '500';
                                                contentElement.style.fontFamily = 'UniviaPro';
                                            }
                                        }, 100); 
                                        setTimeout(function() {
                                            var contentElement = document.getElementById('ctl00_MainContent_MsgBox1_headerHtml');
                                            if (contentElement) {
                                                contentElement.style.color = 'rgb(28, 148, 137)';
                                                contentElement.style.fontSize = '14px';
                                                contentElement.style.fontStyle = 'normal';
                                                contentElement.style.fontVariant = 'normal';
                                                contentElement.style.fontWeight = '500';
                                                contentElement.style.fontFamily = 'UniviaPro';
                                            }
                                        }, 100);
                                    </script>";
                    ClientScript.RegisterStartupScript(this.GetType(), "ApplyMsgBoxStyles", script);
                    return;
                }
            }

            where = "IdCipa=" + eleicaoCipa.IdCipa.Id
                       + " ORDER BY (SELECT tNO_EMPG FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEMPREGADO WHERE"
                       + " nID_EMPREGADO = ParticipantesEleicaoCipa.IdEmpregado)";

            DataSet candidatos = new ParticipantesEleicaoCipa().Get(where);
            if (candidatos.Tables[0].Rows.Count < (dim.Efetivo + dim.Suplente))
            {
                MsgBox1.Show("Ilitera.Net", "Ainda faltam candidatos", null, new EO.Web.MsgBoxButton("OK"));
                string script = @"
                                    <script type='text/javascript'>
                                        setTimeout(function() {
                                            var contentElement = document.getElementById('ctl00_MainContent_MsgBox1_msg_t');
                                            if (contentElement) {
                                                contentElement.style.color = 'rgb(125, 123, 123)';
                                                contentElement.style.fontSize = '14px';
                                                contentElement.style.fontStyle = 'normal';
                                                contentElement.style.fontVariant = 'normal';
                                                contentElement.style.fontWeight = '500';
                                                contentElement.style.fontFamily = 'UniviaPro';
                                            }
                                        }, 100); 
                                        setTimeout(function() {
                                            var contentElement = document.getElementById('ctl00_MainContent_MsgBox1_headerHtml');
                                            if (contentElement) {
                                                contentElement.style.color = 'rgb(28, 148, 137)';
                                                contentElement.style.fontSize = '14px';
                                                contentElement.style.fontStyle = 'normal';
                                                contentElement.style.fontVariant = 'normal';
                                                contentElement.style.fontWeight = '500';
                                                contentElement.style.fontFamily = 'UniviaPro';
                                            }
                                        }, 100);
                                    </script>";
                ClientScript.RegisterStartupScript(this.GetType(), "ApplyMsgBoxStyles", script);
                return;
            }


            RptFolhaDeApuracao_New report = dataSource.GetReportFolhaDeApuracao(lista);
            CreatePDFDocument(report, this.Response, "RptApuracao");
        }

        private List<CandidatosApurados> PopularListaCandidatos()
        {
            List<CandidatosApurados> lista = new List<CandidatosApurados>();
            EleicaoCipa eleicaoCipa = new EleicaoCipa();
            EventoCipa eventoCipa = EventoCipa.GetEventoCipa(cipa, EventoBase.Eleicao);
            if (eventoCipa.GetType() == typeof(EleicaoCipa))
            {
                eleicaoCipa = (EleicaoCipa)eventoCipa;
            }
            else
            {
                eleicaoCipa = new EleicaoCipa();
                eleicaoCipa.Find(eventoCipa.Id);
            }
            string where = "nID_EMPREGADO IN (SELECT IdEmpregado FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.ParticipantesEleicaoCipa WHERE"
                            + " IdCipa=" + eleicaoCipa.IdCipa.Id + ")"
                            + " ORDER BY tNO_EMPG";

            DataSet ds = new Empregado().Get(where);


            foreach (DataRow row in ds.Tables[0].Rows)
            {
                lista.Add(new CandidatosApurados

                {
                    IdEmpregado = Convert.ToInt32(row["nID_EMPREGADO"]),
                    NomeEmpregado = Convert.ToString(row["tNO_EMPG"]),
                    DataAdmissao = Convert.ToDateTime(row["hDT_ADM"])
                }

                );
            };

            return lista;
        }

        protected void btnImprimirCedulas_Click(object sender, EventArgs e)
        {
            EleicaoCipa eleicaoCipa = new EleicaoCipa();
            EventoCipa eventoCipa = EventoCipa.GetEventoCipa(cipa, EventoBase.Eleicao);
            if (eventoCipa.GetType() == typeof(EleicaoCipa))
            {
                eleicaoCipa = (EleicaoCipa)eventoCipa;
            }
            else
            {
                eleicaoCipa = new EleicaoCipa();
                eleicaoCipa.Find(eventoCipa.Id);
            }

            int qtdCedulas = Convert.ToInt32(txtQtdCedulas.Text);
            if (qtdCedulas % 2 != 0)
            {
                qtdCedulas++;
            }

            eventoCipa.Find(eventoCipa.Id);
            RptCedulaDeVotacao report = new DataSourceCipa(eventoCipa).GetReportCedulaDeVotacao(qtdCedulas / 2, chkRegistro.Checked);
            CreatePDFDocument(report, this.Response, "RptCedulas");

        }

        protected void txtDataCandidatos_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DateTime dataCandidatos = DateTime.ParseExact(txtDataCandidatos.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                cipa.ListaDosCandidatos = dataCandidatos;
                cipa.Save();
            }
            catch (Exception ex)
            {
                MsgBox1.Show("Ilitera.Net", "Data inválida", null, new EO.Web.MsgBoxButton("OK"));
            }
            finally
            {
                txtDataCandidatos.Text = cipa.ListaDosCandidatos.ToString("dd/MM/yyyy");
            }
        }

        protected void txtDataCal_TextChanged(object sender, EventArgs e)
        {
            EventoCipa eventoCipa = EventoCipa.GetEventoCipa(cipa, EventoBase.Calendario);
            //ReuniaoCipa reuniaoCipa = new ReuniaoCipa(); ;
            try
            {
                DateTime dataCal = DateTime.ParseExact(txtDataCal.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

                eventoCipa.DataSolicitacao = dataCal;
                eventoCipa.Save();
            }
            catch (Exception ex)
            {
                MsgBox1.Show("Ilitera.Net", "Data inválida", null, new EO.Web.MsgBoxButton("OK"));
            }
            finally
            {
                txtDataCal.Text = eventoCipa.DataSolicitacao.ToString("dd/MM/yyyy");
            }
        }

        protected void txtDataPosse_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DateTime dataPosse = DateTime.ParseExact(txtDataPosse.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                cipa.Posse = dataPosse;
                cipa.Save();
            }
            catch (Exception ex)
            {
                MsgBox1.Show("Ilitera.Net", "Data inválida", null, new EO.Web.MsgBoxButton("OK"));
            }
            finally
            {
                txtDataPosse.Text = cipa.Posse.ToString("dd/MM/yyyy");
                txtTerminoMandato.Text = cipa.GetTerminoMandato().ToString("dd/MM/yyyy");
            }
        }

        protected void txtLocalizarCandidatos_TextChanged(object sender, EventArgs e)
        {
            string nome = txtLocalizaCandidatos.Text;
            EventoCipa eventoCipa = EventoCipa.GetEventoCipa(cipa, EventoBase.ComissaoEleitoral);
            string strWhere;
            Cipa cipaGestaoAtual = cliente.GetGestaoAtual();
            grd_Presidente_Vice.Columns[1].Visible = true;
            grd_Presidente_Vice.Columns[1].Width = 220;
            grd_Presidente_Vice.Columns[0].Width = 220;
            grd_Presidente_Vice.Width = 440;
            int selectedValue = Convert.ToInt32(rbMembros.SelectedValue);
            switch (selectedValue)
            {
                case 1:
                    grd_Presidente_Vice.Items.Clear();
                    grd_Presidente_Vice.DataSource = null;
                    grd_Presidente_Vice.DataBind();
                    strWhere = "IdCipa =" + cipaGestaoAtual.Id
                    + " AND Numero = 0 "
                    + " AND IndGrupoMembro IN (0, 1) "
                    + " AND IndStatus=" + ((int)MembroCipa.Status.Ativo).ToString()
                    + " AND IndTitularSuplente=" + (int)TitularSuplente.Titular
                    + " AND NomeMembro NOT IN(SELECT NomeMembro FROM MembroComissaoEleitoral WHERE IdCipa=" + eventoCipa.IdCipa.Id + " AND NomeMembro IS NOT NULL)"
                    + " AND IdEmpregado NOT IN(SELECT nID_EMPREGADO FROM Sied_Novo.dbo.tblEMPREGADO Where tNO_EMPG IN(SELECT NomeMembro FROM MembroComissaoEleitoral"
                    + " WHERE IdCipa = " + cipa.Id + " AND NomeMembro IS NOT NULL))"
                    + " ORDER BY Numero";

                    DataSet presidentes = new MembroCipa().Get(strWhere);
                    DataTable zDt = new DataTable();
                    zDt.Columns.Add("IdEmprcImaegado");
                    zDt.Columns.Add("Empregado");
                    zDt.Columns.Add("Cargo");

                    foreach (DataRow row in presidentes.Tables[0].Rows)
                    {
                        Empregado empregado = new Empregado();
                        empregado.Find(Convert.ToInt32(row["IdEmpregado"]));

                        MembroCipa membroCipa = new MembroCipa();
                        membroCipa.Find(Convert.ToInt32(row["IdMembroCipa"]));
                        string cargoCipa = membroCipa.GetNomeCargo();

                        if (nome == "" || empregado.tNO_EMPG.ToLower().Contains(nome.ToLower()))
                            zDt.Rows.Add(row["IdEmpregado"], empregado.tNO_EMPG, cargoCipa);
                    }

                    DataSet zDs = new DataSet();
                    zDs.Tables.Add(zDt);
                    grd_Presidente_Vice.DataSource = zDs;
                    grd_Presidente_Vice.DataBind();
                    break;
                case 2:
                    grd_Presidente_Vice.Items.Clear();
                    grd_Presidente_Vice.DataSource = null;
                    grd_Presidente_Vice.DataBind();
                    strWhere = "IdCipa =" + cipaGestaoAtual.Id
                        + " AND IndStatus=" + ((int)MembroCipa.Status.Ativo).ToString()
                        + " AND IdEmpregado Not IN (SELECT nID_EMPREGADO FROM Sied_Novo.dbo.tblEMPREGADO Where tNO_EMPG IN(SELECT NomeMembro FROM MembroComissaoEleitoral(NOLOCK) WHERE IdCipa = " + cipa.Id + "))"
                        + " UNION"
                        + " SELECT MembroCipa.IdMembroCipa, IdCipa, IdEmpregado, NomeMembro, Numero, Estabilidade, IndTitularSuplente, IndGrupoMembro, IndStatus FROM MembroCipa"
                        + " Where IdCipa = " + cipa.Id + " AND IdEmpregado IS NULL AND NomeMembro NOT IN (SELECT NomeMembro FROM MembroComissaoEleitoral "
                        + " WHERE IdCipa = " + cipa.Id + ")"
                        + " ORDER BY Numero";

                    DataSet todosIntegrantes = new MembroCipa().Get(strWhere);
                    zDt = new DataTable();
                    zDt.Columns.Add("IdEmprcImaegado");
                    zDt.Columns.Add("Empregado");
                    zDt.Columns.Add("Cargo");

                    foreach (DataRow row in todosIntegrantes.Tables[0].Rows)
                    {
                        Empregado empregado = new Empregado();
                        MembroCipa membroCipa = new MembroCipa();
                        if (row["IdEmpregado"] != DBNull.Value)
                        {
                            empregado.Find(Convert.ToInt32(row["IdEmpregado"]));
                            membroCipa.Find(Convert.ToInt32(row["IdMembroCipa"]));
                            string cargoCipa = membroCipa.GetNomeCargo();
                            if (nome == "" || empregado.tNO_EMPG.ToLower().Contains(nome.ToLower()))
                                zDt.Rows.Add(row["IdEmpregado"], empregado.tNO_EMPG, cargoCipa);
                        }
                        else
                        {
                            membroCipa.Find(Convert.ToInt32(row["IdMembroCipa"]));
                            string cargoCipa = membroCipa.GetNomeCargo();
                            if (nome == "" || membroCipa.NomeMembro.ToLower().Contains(nome.ToLower()))
                                zDt.Rows.Add(0, membroCipa.NomeMembro, cargoCipa);
                        }
                    }

                    zDs = new DataSet();
                    zDs.Tables.Add(zDt);
                    grd_Presidente_Vice.DataSource = zDs;
                    grd_Presidente_Vice.DataBind();
                    break;
                case 3:
                    grd_Presidente_Vice.Items.Clear();
                    grd_Presidente_Vice.DataSource = null;
                    grd_Presidente_Vice.DataBind();
                    grd_Presidente_Vice.Columns[1].Visible = false;
                    grd_Presidente_Vice.Columns[1].Width = 0;
                    grd_Presidente_Vice.Columns[0].Width = 420;
                    grd_Presidente_Vice.Width = 440;
                    strWhere = "nID_EMPR=" + cliente.Id + " AND tNO_EMPG LIKE '%" + nome + "%' AND tNO_EMPG NOT IN(SELECT NomeMembro FROM Opsa.dbo.MembroComissaoEleitoral (NOLOCK)  WHERE IdCipa=" + cipa.Id + ") AND hDT_DEM IS NULL  ORDER BY tNO_EMPG";
                    DataSet empregados = new Empregado().Get(strWhere);
                    zDt = new DataTable();
                    zDt.Columns.Add("IdEmprcImaegado");
                    zDt.Columns.Add("Empregado");
                    zDt.Columns.Add("Cargo");

                    foreach (DataRow row in empregados.Tables[0].Rows)
                    {
                        zDt.Rows.Add(row["nID_EMPREGADO"], row["tNO_EMPG"], "");
                    }

                    zDs = new DataSet();
                    zDs.Tables.Add(zDt);
                    grd_Presidente_Vice.DataSource = zDs;
                    grd_Presidente_Vice.DataBind();
                    break;
            }
        }

        protected void btn_imprimir_comunicacao_Click(object sender, EventArgs e)
        {
            EventoCipa eventoCipa = EventoCipa.GetEventoCipa(cipa, EventoBase.ComunicacaoSindicato);
            eventoCipa.Find(eventoCipa.Id);
            ReportClass report = new ReportClass();
            report = new DataSourceCipa(eventoCipa).GetReport();
            CreatePDFDocument(report, this.Response, "RptComunicaoSindicato");
        }

        protected void btnImpCal_Click(object sender, EventArgs e)
        {
            EventoCipa eventoCipa = EventoCipa.GetEventoCipa(cipa, EventoBase.Calendario);
            eventoCipa.Find(eventoCipa.Id);
            ReportClass report = new ReportClass();
            report = new DataSourceCipa(eventoCipa).GetReport();
            CreatePDFDocument(report, this.Response, "RptCalendarioReunioesOrdinarias");
        }

        protected void btnAtaPosse_Click(object sender, EventArgs e)
        {
            EventoCipa eventoCipa = EventoCipa.GetEventoCipa(cipa, EventoBase.Posse);
            eventoCipa.Find(eventoCipa.Id);
            ReportClass report = new ReportClass();
            report = new DataSourceCipa(eventoCipa).GetReport();
            CreatePDFDocument(report, this.Response, "RptAtaDaPosse_New");
        }


        protected void txtHoraPosse_TextChanged(object sender, EventArgs e)
        {
            EventoCipa eventoCipa = new EventoCipa();
            try
            {
                DateTime horario = DateTime.ParseExact(txtHoraPosse.Text, "HH:mm", System.Globalization.CultureInfo.InvariantCulture);

                eventoCipa = EventoCipa.GetEventoCipa(cipa, EventoBase.Posse);
                eventoCipa.HoraInicio = horario.ToString("HH:mm");
                eventoCipa.Save();
            }
            catch (Exception)
            {
                MsgBox1.Show("Ilitera.Net", "Horário inválido", null, new EO.Web.MsgBoxButton("OK"));
            }
            finally
            {
                txtHoraPosse.Text = eventoCipa.HoraInicio;
            }
        }

        protected void txtHoraPosse2_TextChanged(object sender, EventArgs e)
        {
            EventoCipa eventoCipa = new EventoCipa();
            try
            {
                DateTime horario = DateTime.ParseExact(txtHoraPosse2.Text, "HH:mm", System.Globalization.CultureInfo.InvariantCulture);

                eventoCipa = EventoCipa.GetEventoCipa(cipa, EventoBase.Posse);
                eventoCipa.HoraFim = horario.ToString("HH:mm");
                eventoCipa.Save();
            }
            catch (Exception)
            {
                MsgBox1.Show("Ilitera.Net", "Horário inválido", null, new EO.Web.MsgBoxButton("OK"));
            }
            finally
            {
                txtHoraPosse2.Text = eventoCipa.HoraFim;
            }
        }

        protected void txtLocalPosse_TextChanged(object sender, EventArgs e)
        {
            EventoCipa eventoCipa = new EventoCipa();
            try
            {
                string local = txtLocalPosse.Text;

                eventoCipa = EventoCipa.GetEventoCipa(cipa, EventoBase.Posse);
                eventoCipa.Local = local;
                eventoCipa.Save();
            }
            catch (Exception)
            {
                MsgBox1.Show("Ilitera.Net", "Local inválido", null, new EO.Web.MsgBoxButton("OK"));
            }
            finally
            {
                txtLocalPosse.Text = eventoCipa.Local;
            }
        }

        protected void txtHoraCal_TextChanged(object sender, EventArgs e)
        {
            EventoCipa eventoCipa = new EventoCipa();

            try
            {
                DateTime horario = DateTime.ParseExact(txtHoraCal.Text, "HH:mm", System.Globalization.CultureInfo.InvariantCulture);

                eventoCipa = EventoCipa.GetEventoCipa(cipa, EventoBase.Calendario);
                eventoCipa.HoraInicio = horario.ToString("HH:mm");
                eventoCipa.Save();
            }
            catch (Exception)
            {
                MsgBox1.Show("Ilitera.Net", "Local inválido", null, new EO.Web.MsgBoxButton("OK"));
            }
            finally
            {
                txtHoraCal.Text = eventoCipa.HoraInicio;
            }
        }

        protected void txtHoraCal2_TextChanged(object sender, EventArgs e)
        {
            EventoCipa eventoCipa = new EventoCipa();

            try
            {
                DateTime horario = DateTime.ParseExact(txtHoraCal2.Text, "HH:mm", System.Globalization.CultureInfo.InvariantCulture);

                eventoCipa = EventoCipa.GetEventoCipa(cipa, EventoBase.Calendario);
                eventoCipa.HoraFim = horario.ToString("HH:mm");
                eventoCipa.Save();
            }
            catch (Exception)
            {
                MsgBox1.Show("Ilitera.Net", "Local inválido", null, new EO.Web.MsgBoxButton("OK"));
            }
            finally
            {
                txtHoraCal2.Text = eventoCipa.HoraFim;
            }
        }

        protected void txtLocalCal_TextChanged(object sender, EventArgs e)
        {
            EventoCipa eventoCipa = new EventoCipa();

            try
            {
                string local = txtLocalCal.Text;
                eventoCipa = EventoCipa.GetEventoCipa(cipa, EventoBase.Calendario);
                eventoCipa.Local = local;
                eventoCipa.Save();
            }
            catch (Exception)
            {
                MsgBox1.Show("Ilitera.Net", "Local inválido", null, new EO.Web.MsgBoxButton("OK"));
            }
            finally
            {
                txtLocalCal.Text = eventoCipa.Local;
            }
        }

        protected void lstTexto_TextChanged(object sender, EventArgs e)
        {
            EventoCipa eventoCipa = new EventoCipa();
            eventoCipa = EventoCipa.GetEventoCipa(cipa, EventoBase.Eleicao);
            eventoCipa.Find(eventoCipa.Id);
            EleicaoCipa eleicaoCipa;
            if (eventoCipa.GetType() == typeof(EleicaoCipa))
            {
                eleicaoCipa = (EleicaoCipa)eventoCipa;
            }
            else
            {
                eleicaoCipa = new EleicaoCipa();
            }
            eleicaoCipa.Find(eventoCipa.Id);
            eleicaoCipa.FraseAdicional = lstTexto.Text;
            eleicaoCipa.Save();
        }


        protected void GridCandidatos_ItemCommand(object sender, GridCommandEventArgs e)
        {
            int codigo = Convert.ToInt32(e.Item.Key);
            EleicaoCipa eleicaoCipa = new EleicaoCipa();
            EventoCipa eventoCipa = EventoCipa.GetEventoCipa(cipa, EventoBase.Eleicao);
            eventoCipa.Find(eventoCipa.Id);
            if (eventoCipa.GetType() == typeof(EleicaoCipa))
            {
                eleicaoCipa = (EleicaoCipa)eventoCipa;
            }
            else
            {
                eleicaoCipa = new EleicaoCipa();
                eleicaoCipa.Find(eventoCipa.Id);
            }
            eleicaoCipa.Find(eleicaoCipa.Id);

            string where = "IdCipa=" + eleicaoCipa.IdCipa.Id
                       + " and IdEmpregado = " + codigo
                       + " ORDER BY (SELECT tNO_EMPG FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEMPREGADO WHERE"
                       + " nID_EMPREGADO = ParticipantesEleicaoCipa.IdEmpregado)";

            Empregado empregado = new Empregado();
            empregado.Find(codigo);
            DataSet candidato = new ParticipantesEleicaoCipa().Get(where);
            lblAlterarVotos.Text = "Insira a quantidade de votos recebidos por " + empregado.tNO_EMPG;
            lblAlterarVotos.Visible = true;
            txtAlterarVotos.Attributes["data-id"] = codigo.ToString();
            txtAlterarVotos.Attributes["data-id2"] = candidato.Tables[0].Rows[0].ItemArray[0].ToString();
            txtAlterarVotos.Attributes["data-id3"] = candidato.Tables[0].Rows[0].ItemArray[4].ToString();
            txtAlterarVotos.Text = candidato.Tables[0].Rows[0].ItemArray[3].ToString();
            txtAlterarVotos.Visible = true;
            btnAplicarVotos.Visible = true;
            btnCancelarVotos.Visible = true;
        }

        protected void Grid4_ItemCommand(object sender, GridCommandEventArgs e)
        {
            int id = Convert.ToInt32(e.Item.Key);
            ParticipantesEleicaoCipa participante = new ParticipantesEleicaoCipa();
            participante.Find(id);
            Empregado empregado = new Empregado();
            empregado.Find(participante.IdEmpregado.Id);
            lblApelido.Visible = true;
            lblApelido.Text = "Insira um novo apelido para " + empregado.tNO_EMPG;
            txtApelido.Attributes["data-id"] = empregado.Id.ToString();
            txtApelido.Visible = true;
            txtApelido.Text = empregado.tNO_APELIDO;
            btnApelidoAplicar.Visible = true;
            btnApelidoCancelar.Visible = true;
        }

        protected void btnApelidoCancelar_Click(object sender, EventArgs e)
        {
            lblApelido.Visible = false;
            lblApelido.Text = "";
            txtApelido.Attributes.Remove("data-id");
            txtApelido.Visible = false;
            txtApelido.Text = "";
            btnApelidoAplicar.Visible = false;
            btnApelidoCancelar.Visible = false;
        }

        protected void btnApelidoAplicar_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txtApelido.Attributes["data-id"]);
            Empregado empregado = new Empregado();
            empregado.Find(id);
            empregado.tNO_APELIDO = txtApelido.Text;
            empregado.Save();
            Grid4.DataSource = null;
            Grid4.DataBind();
            PopulaCandidatos();
            lblApelido.Visible = false;
            lblApelido.Text = "";
            txtApelido.Attributes.Remove("data-id");
            txtApelido.Visible = false;
            txtApelido.Text = "";
            btnApelidoAplicar.Visible = false;
            btnApelidoCancelar.Visible = false;
        }

        protected void btnCancelarVotos_Click(object sender, EventArgs e)
        {
            lblAlterarVotos.Text = "";
            lblAlterarVotos.Visible = false;
            txtAlterarVotos.Attributes.Remove("data-id");
            txtAlterarVotos.Attributes.Remove("data-id2");
            txtAlterarVotos.Attributes.Remove("data-id3");
            txtAlterarVotos.Text = "";
            txtAlterarVotos.Visible = false;
            btnAplicarVotos.Visible = false;
            btnCancelarVotos.Visible = false;
        }

        protected void btnAplicarVotos_Click(object sender, EventArgs e)
        {
            EleicaoCipa eleicaoCipa = new EleicaoCipa();
            EventoCipa eventoCipa = EventoCipa.GetEventoCipa(cipa, EventoBase.Eleicao);
            eventoCipa.Find(eventoCipa.Id);
            if (eventoCipa.GetType() == typeof(EleicaoCipa))
            {
                eleicaoCipa = (EleicaoCipa)eventoCipa;
            }
            else
            {
                eleicaoCipa = new EleicaoCipa();
                eleicaoCipa.Find(eventoCipa.Id);
            }
            eleicaoCipa.Find(eleicaoCipa.Id);
            ParticipantesEleicaoCipa candidatos = new ParticipantesEleicaoCipa();
            Empregado empregado = new Empregado();
            empregado.Find(Convert.ToInt32(txtAlterarVotos.Attributes["data-id"]));
            candidatos.Inicialize();
            candidatos.Id = Convert.ToInt32(txtAlterarVotos.Attributes["data-id2"]);
            candidatos.IdEmpregado = empregado;
            candidatos.IdCipa = cipa;
            candidatos.IsVicePresidente = Convert.ToBoolean(txtAlterarVotos.Attributes["data-id3"]);
            candidatos.Votos = Convert.ToInt32(txtAlterarVotos.Text);
            candidatos.Save();

            lblAlterarVotos.Text = "";
            lblAlterarVotos.Visible = false;
            txtAlterarVotos.Attributes.Remove("data-id");
            txtAlterarVotos.Attributes.Remove("data-id2");
            txtAlterarVotos.Attributes.Remove("data-id3");
            txtAlterarVotos.Text = "";
            txtAlterarVotos.Visible = false;
            btnAplicarVotos.Visible = false;
            btnCancelarVotos.Visible = false;

            GridCandidatos.Items.Clear();
            GridCandidatos.DataSource = null;
            GridCandidatos.DataBind();

            PopulaAtaEleicaoPosse();
        }

        protected void chkInativos1_CheckedChanged(object sender, EventArgs e)
        {
            Grid1.Items.Clear();
            Grid1.DataSource = null;
            Grid1.DataBind();
            EleicaoCipa eleicaoCipa = new EleicaoCipa();
            EventoCipa eventoCipa = EventoCipa.GetEventoCipa(cipa, EventoBase.Eleicao);
            eventoCipa.Find(eventoCipa.Id);
            if (eventoCipa.GetType() == typeof(EleicaoCipa))
            {
                eleicaoCipa = (EleicaoCipa)eventoCipa;
            }
            else
            {
                eleicaoCipa = new EleicaoCipa();
                eleicaoCipa.Find(eventoCipa.Id);
            }
            eleicaoCipa.Find(eleicaoCipa.Id);
            eleicaoCipa.IdCipa.Find();
            eleicaoCipa.IdCipa.IdCliente.Find();




            StringBuilder str = new StringBuilder();
            str.Append("IdCipa=" + eleicaoCipa.IdCipa.Id);
            str.Append(" AND IndGrupoMembro=" + (int)Ilitera.Opsa.Data.GrupoMembro.Empregado);
            if (chkInativos1.Checked)
                str.Append(" AND (IndStatus=" + (int)MembroCipa.Status.Afastado + " OR IndStatus=" + (int)MembroCipa.Status.Renunciou + ")");
            else
                str.Append(" AND IndStatus=" + (int)MembroCipa.Status.Ativo);
            str.Append(" ORDER BY IndTitularSuplente, Numero");

            DataSet empregadosEleitos = new MembroCipa().Get(str.ToString());
            DataTable zDt = new DataTable();
            zDt.Columns.Add("NomeEmpregado");
            zDt.Columns.Add("CargoCipa");
            zDt.Columns.Add("Estabilidade");

            if (empregadosEleitos.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in empregadosEleitos.Tables[0].Rows)
                {
                    Empregado empregado = new Empregado();
                    empregado.Find(Convert.ToInt32(row["IdEmpregado"]));
                    MembroCipa membroCipa = new MembroCipa();
                    membroCipa.Find(Convert.ToInt32(row["IdMembroCipa"]));
                    string cargoCipa = membroCipa.GetNomeCargo();
                    string dataEstabilidade = row["Estabilidade"].ToString();

                    zDt.Rows.Add(empregado.tNO_EMPG, cargoCipa, dataEstabilidade.Remove(10));
                }

                DataSet zDs = new DataSet();
                zDs.Tables.Add(zDt);
                Grid1.DataSource = zDs;
                Grid1.DataBind();
            }
            else
            {
                chkInativos1.Checked = false;
                MsgBox1.Show("Ilitera.Net", "Não há resultados", null, new EO.Web.MsgBoxButton("OK"));
                PopulaAtaEleicaoPosse();
            }
            string script = @"
                                    <script type='text/javascript'>
                                        setTimeout(function() {
                                            var contentElement = document.getElementById('ctl00_MainContent_MsgBox1_msg_t');
                                            if (contentElement) {
                                                contentElement.style.color = 'rgb(125, 123, 123)';
                                                contentElement.style.fontSize = '14px';
                                                contentElement.style.fontStyle = 'normal';
                                                contentElement.style.fontVariant = 'normal';
                                                contentElement.style.fontWeight = '500';
                                                contentElement.style.fontFamily = 'UniviaPro';
                                            }
                                        }, 100); 
                                        setTimeout(function() {
                                            var contentElement = document.getElementById('ctl00_MainContent_MsgBox1_headerHtml');
                                            if (contentElement) {
                                                contentElement.style.color = 'rgb(28, 148, 137)';
                                                contentElement.style.fontSize = '14px';
                                                contentElement.style.fontStyle = 'normal';
                                                contentElement.style.fontVariant = 'normal';
                                                contentElement.style.fontWeight = '500';
                                                contentElement.style.fontFamily = 'UniviaPro';
                                            }
                                        }, 100);
                                    </script>";
            ClientScript.RegisterStartupScript(this.GetType(), "ApplyMsgBoxStyles", script);
        }

        protected void chkInativos2_CheckedChanged(object sender, EventArgs e)
        {
            Grid2.Items.Clear();
            Grid2.DataSource = null;
            Grid2.DataBind();
            EleicaoCipa eleicaoCipa = new EleicaoCipa();
            EventoCipa eventoCipa = EventoCipa.GetEventoCipa(cipa, EventoBase.Eleicao);
            eventoCipa.Find(eventoCipa.Id);
            if (eventoCipa.GetType() == typeof(EleicaoCipa))
            {
                eleicaoCipa = (EleicaoCipa)eventoCipa;
            }
            else
            {
                eleicaoCipa = new EleicaoCipa();
                eleicaoCipa.Find(eventoCipa.Id);
            }
            eleicaoCipa.Find(eleicaoCipa.Id);
            eleicaoCipa.IdCipa.Find();
            eleicaoCipa.IdCipa.IdCliente.Find();


            StringBuilder str = new StringBuilder();
            str.Append("IdCipa=" + eleicaoCipa.IdCipa.Id);
            str.Append(" AND IndGrupoMembro=" + (int)GrupoMembro.Empregador);
            if (chkInativos2.Checked)
                str.Append(" AND (IndStatus=" + (int)MembroCipa.Status.Afastado + " OR IndStatus=" + (int)MembroCipa.Status.Renunciou + ")");
            else
                str.Append(" AND IndStatus=" + (int)MembroCipa.Status.Ativo);
            str.Append(" ORDER BY IndTitularSuplente, Numero");

            DataSet empregados = new MembroCipa().Get(str.ToString());
            DataTable zDt = new DataTable();
            zDt.Columns.Add("NomeEmpregado");
            zDt.Columns.Add("CargoCipa");

            if (empregados.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in empregados.Tables[0].Rows)
                {
                    Empregado empregado = new Empregado();
                    empregado.Find(Convert.ToInt32(row["IdEmpregado"]));
                    MembroCipa membroCipa = new MembroCipa();
                    membroCipa.Find(Convert.ToInt32(row["IdMembroCipa"]));
                    string cargoCipa = membroCipa.GetNomeCargo();
                    zDt.Rows.Add(empregado.tNO_EMPG, cargoCipa);
                }

                DataSet zDs = new DataSet();
                zDs.Tables.Add(zDt);
                Grid2.DataSource = zDs;
                Grid2.DataBind();
            }
            else
            {
                chkInativos2.Checked = false;
                MsgBox1.Show("Ilitera.Net", "Não há resultados", null, new EO.Web.MsgBoxButton("OK"));
                PopulaAtaEleicaoPosse();
            }
            string script = @"
                                    <script type='text/javascript'>
                                        setTimeout(function() {
                                            var contentElement = document.getElementById('ctl00_MainContent_MsgBox1_msg_t');
                                            if (contentElement) {
                                                contentElement.style.color = 'rgb(125, 123, 123)';
                                                contentElement.style.fontSize = '14px';
                                                contentElement.style.fontStyle = 'normal';
                                                contentElement.style.fontVariant = 'normal';
                                                contentElement.style.fontWeight = '500';
                                                contentElement.style.fontFamily = 'UniviaPro';
                                            }
                                        }, 100); 
                                        setTimeout(function() {
                                            var contentElement = document.getElementById('ctl00_MainContent_MsgBox1_headerHtml');
                                            if (contentElement) {
                                                contentElement.style.color = 'rgb(28, 148, 137)';
                                                contentElement.style.fontSize = '14px';
                                                contentElement.style.fontStyle = 'normal';
                                                contentElement.style.fontVariant = 'normal';
                                                contentElement.style.fontWeight = '500';
                                                contentElement.style.fontFamily = 'UniviaPro';
                                            }
                                        }, 100);
                                    </script>";
            ClientScript.RegisterStartupScript(this.GetType(), "ApplyMsgBoxStyles", script);
        }

        protected void btnAddIndicados_Click(object sender, EventArgs e)
        {
            string script = @"
                            <script type='text/javascript'>
                                var popup = window.open('AdicionarIndicado.aspx', 'AdicionarIndicado', 'width=905,height=600,scrollbars=yes,resizable=yes');
                                var timer = setInterval(function() { 
                                    if (popup.closed) {
                                        clearInterval(timer);
                                        __doPostBack('" + btnAddIndicados.ClientID + @"', '');
                                    }
                                }, 1000); // Verifica a cada segundo se a janela foi fechada
                            </script>";
            System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "AdicionarIndicado", script, false);
        }

        protected void cmd_GestaoAtual_Click(object sender, EventArgs e)
        {
            StringBuilder st = new StringBuilder();
            st.AppendFormat("void(window.open('MembrosCIPA.aspx', 'MembrosCIPA','scrollbars=yes, resizable=yes, toolbar=no, addressbar=no, menubar=no, width=825px, height=700px'));");

            System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), " ", st.ToString(), true);
        }

        protected void cmd_Membros_Click(object sender, EventArgs e)
        {
            StringBuilder st = new StringBuilder();
            st.AppendFormat("void(window.open('MembrosUltimasCipas.aspx', 'MembrosUltimasCipas','scrollbars=yes, resizable=yes, toolbar=no, addressbar=no, menubar=no, width=825px, height=1200px'));");

            System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), " ", st.ToString(), true);
        }

        protected void cmd_ReuniaoExtra_Click(object sender, EventArgs e)
        {
            StringBuilder st = new StringBuilder();
            st.AppendFormat("void(window.open('AddReuniao.aspx', 'AddReuniao','scrollbars=yes, resizable=yes, toolbar=no, addressbar=no, menubar=no, width=850px, height=1100px'));");

            System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), " ", st.ToString(), true);
        }

        protected void grdReunioes_ItemCommand(object sender, GridCommandEventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("window.open('AddReuniao.aspx?r={0}', 'CursoEmpresa', 'scrollbars=yes, resizable=yes, toolbar=no, addressbar=no, menubar=no, width=850px, height=1100px');", e.Item.Key);
            System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "OpenWindow", sb.ToString(), true);
        }

        protected void btnRecalcular_Click(object sender, EventArgs e)
        {
            try
            {
                cipa.SetCalendarioRecalculaReuniloes((int)EventoBase.Reuniao1);
                PopulaGrdReunioes();
            }
            catch (Exception ex)
            {
                MsgBox1.Show("Ilitera.Net", ex.Message, null, new EO.Web.MsgBoxButton("OK"));
            }
        }

        private void PopulaGrdReunioes()
        {
            grdReunioes.DataSource = null;
            grdReunioes.DataBind();

            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            dt.Columns.Add("IdReuniao");
            dt.Columns.Add("Reuniao");
            dt.Columns.Add("Data");
            dt.Columns.Add("Editar");

            PropertyInfo[] properties = typeof(Cipa).GetProperties();
            int i = 13;
            int numReuniao = 1;
            foreach (PropertyInfo property in properties)
            {
                if (property.Name.Contains("Reuniao"))
                {
                    DateTime dataReuniao = (DateTime)property.GetValue(cipa, null);
                    dt.Rows.Add(i, "Reunião " + numReuniao, dataReuniao.ToString("dd/MM/yyyy"), "Editar");
                    i++;
                    numReuniao++;
                }                
            }

            ds.Tables.Add(dt);
            grdReunioes.DataSource = ds;
            grdReunioes.DataBind();
        }

        protected void btnExtraordinarias_Click(object sender, EventArgs e)
        {
            StringBuilder st = new StringBuilder();
            st.AppendFormat("void(window.open('ReunioesExtraordinarias.aspx', 'ReunioesExtraordinarias','scrollbars=yes, resizable=yes, toolbar=no, addressbar=no, menubar=no, width=825px, height=1200px'));");

            System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), " ", st.ToString(), true);
        }
    }
}
