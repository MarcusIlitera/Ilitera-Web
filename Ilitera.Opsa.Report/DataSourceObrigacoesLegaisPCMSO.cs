using System;
using System.Text;
using System.Data;
using System.Collections;
using System.Collections.Generic;

using Ilitera.Common;
using Ilitera.Opsa.Data;

namespace Ilitera.Opsa.Report
{
	public class DataSourceObrigacoesLegaisPCMSO: Ilitera.Common.DataSourceBase
	{
		#region Eventos

		public override event EventProgress ProgressIniciar;
		public override event EventProgress ProgressAtualizar;
		public override event EventProgressFinalizar ProgressFinalizar;

		#endregion

        #region Variaveis
        private bool IsSomenteAnualEmpresa = false;
        private bool ComEndereco = false;
        private int IdGrupoEmpresa;
        private int IdCliente;
        private List<Cliente> clientes;
        #endregion

        #region Construtor

        public DataSourceObrigacoesLegaisPCMSO()
        {
            PopularCliente();
        }

        public DataSourceObrigacoesLegaisPCMSO(bool IsSomenteAnualEmpresa)
        {
            this.IsSomenteAnualEmpresa = IsSomenteAnualEmpresa;

            PopularCliente();
        }

        public DataSourceObrigacoesLegaisPCMSO(int IdCliente, int IdGrupoEmpresa)
        {
            this.IdCliente = IdCliente;
            this.IdGrupoEmpresa = IdGrupoEmpresa;

            PopularCliente();
        }
        #endregion

        #region PopularCliente

        private void PopularCliente()
        {
            StringBuilder str = new StringBuilder();

            str.Append("ContrataPCMSO=" + (int)TipoPcmsoContratada.Contratada);

            if (IsSomenteAnualEmpresa)
            {
                str.Append(" AND IndAtualizacaoPeriodico=" + (int)Cliente.AtualizacaoPeriodico.Anual);
                str.Append(" AND IndRealizacaoPeriodico=" + (int)Cliente.RealizacaoPeriodico.Empresa);
            }

            if (IdGrupoEmpresa != 0 && IdCliente == 0)
                str.Append(" AND IdGrupoEmpresa = " + IdGrupoEmpresa);
            else if (IdGrupoEmpresa == 0 && IdCliente != 0)
                str.Append(" AND IdCliente = " + IdCliente);

            str.Append(" AND IdCliente IN (SELECT IdCliente FROM qryClienteAtivos)");
            str.Append(" AND IdCliente IN (SELECT IdCliente FROM ObrigacaoCliente WHERE IsContratada=1 AND IdObrigacao=" + (int)Obrigacoes.ExamesPeriodicos + ")");
            str.Append(" ORDER BY NomeAbreviado");

            clientes = new Cliente().Find<Cliente>(str.ToString());
        }
        #endregion

        #region GetReport

        public RptObrigacoesLegaisPCMSO GetReport()
		{
            return GetReport(GetDataSource());
		}

        public RptObrigacoesLegaisPCMSO GetReport(DataSet ds)
        {
            RptObrigacoesLegaisPCMSO report = new RptObrigacoesLegaisPCMSO();
            report.SetDataSource(ds);
            report.Refresh();
            return report;
        }

        public RptObrigacoesLegaisPCMSOComEndereco GetReportComEndereco()
        {
            return GetReportComEndereco(GetDSComEndereco());
        }

        public RptObrigacoesLegaisPCMSOComEndereco GetReportComEndereco(DataSet ds)
        {
            RptObrigacoesLegaisPCMSOComEndereco report = new RptObrigacoesLegaisPCMSOComEndereco();
            report.SetDataSource(ds);
            report.Refresh();
            return report;
        }
        #endregion

        #region GetDSComEndereco

        public DataSet GetDSComEndereco()
        {
            ComEndereco = true;

            return GetDataSource();
        }
        #endregion

        #region GetDataSource

        private DataSet GetDataSource()
        {
            DataTable table = GetTable();

            DataSet ds = new DataSet();
            ds.Tables.Add(table);

            Obrigacao obrigacao = new Obrigacao((int)Obrigacoes.ExamesPeriodicos);

            if (ProgressIniciar != null)
                ProgressIniciar(clientes.Count);

            int OrdinalPosition = 1;

            foreach (Cliente cliente in clientes)
            {
                try
                {
                    DataRow newRow = ds.Tables[0].NewRow();

                    PopularRow(newRow, obrigacao, cliente, ComEndereco);

                    ds.Tables[0].Rows.Add(newRow);

                    if (ProgressAtualizar != null)
                        ProgressAtualizar(OrdinalPosition++);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                }
            }

            if (ProgressFinalizar != null)
                ProgressFinalizar();

            return ds;
        }
        #endregion

        #region PopularRow

        public static void PopularRow(  DataRow newRow, 
                                        Obrigacao obrigacao, 
                                        Cliente cliente, 
                                        bool ComEndereco)
        {
            Pedido pedidoSugestao = Pedido.GetSugestao(obrigacao.Id, cliente.Id);
            Pedido pedidoPendente = Pedido.GetPedidoPendente(obrigacao.Id, cliente.Id);
            Pedido ultimoPedido = Pedido.GetUltimoPedido(obrigacao.Id, cliente.Id);
            Pedido penultimoPedido = Pedido.GetPenultimoPedido(obrigacao.Id, cliente.Id);

            ObrigacaoCliente obrigacaoCliente = ObrigacaoCliente.GetObrigacaoCliente(cliente, obrigacao);

            ConvocacaoExame convocacao = new ConvocacaoExame();

            if (pedidoPendente.Id == 0)
                convocacao.Find("IdPedido=" + ultimoPedido.Id);
            else
                convocacao.Find("IdPedido=" + pedidoPendente.Id);

            Pcmso pcmso = cliente.GetUltimoPcmso();

            DateTime dataUltimo = cliente.GetDataUltimoPeriodico();
            DateTime dataProximo = pcmso.GetDataProximoPeriodico(); 

            newRow["IdCliente"] = cliente.Id;
            newRow["CLIENTE"] = cliente.NomeAbreviado;
            newRow["GRUPO"] = cliente.IdGrupoEmpresa.Id != 0 ? cliente.IdGrupoEmpresa.ToString() : cliente.NomeAbreviado;
            newRow["QTD_EMPREGADOS"] = cliente.GetEmpregadosAtivos();
            newRow["OBRIGACAO"] = obrigacao.NomeReduzido;

            if (obrigacaoCliente.DataVencimento != new DateTime())
                newRow["VENCIMENTO"] = obrigacaoCliente.DataVencimento;

            newRow["PENULTIMO"] = (penultimoPedido.DataConclusao != new DateTime()) ? penultimoPedido.DataConclusao.ToString("dd-MM-yyyy") : "-";
            newRow["ULTIMO"] = dataUltimo != new DateTime() ? dataUltimo.ToString("dd-MM-yyyy") : "-";

            if (pedidoPendente.Id != 0)
            {
                newRow["STATUS"] = "Pendente";
                newRow["IdPedido"] = pedidoPendente.Id;
                newRow["PEDIDO_NUM"] = pedidoPendente.GetNumeroPedido();
                newRow["PEDIDO_DT"] = pedidoPendente.DataSolicitacao;
                newRow["PEDIDO_DT_AGEND"] = pedidoPendente.GetDataAgendamentoPedido();
                newRow["PEDIDO_AGENDAMENTOS"] = pedidoPendente.GetShortAgendamentoPedido();
                newRow["PEDIDO_PRESTADOR"] = pedidoPendente.IdPrestador.ToString();
                newRow["PEDIDO_IS_PREVISAO"] = pedidoPendente.GetIsAgendamentoPedido();
                newRow["PEDIDO_STATUS"] = pedidoPendente.GetSituacaoPedido();
                newRow["PEDIDO_OBSERVACAO"] = obrigacaoCliente.Observacao == string.Empty ? pedidoPendente.IdPedidoGrupo.Observacao : obrigacaoCliente.Observacao;
            }
            else
            {
                newRow["STATUS"] = pedidoSugestao.Id != 0 ? "Sugestão" : "Realizado";
                newRow["IdPedido"] = pedidoSugestao.Id;
                newRow["PEDIDO_NUM"] = string.Empty;
                newRow["PEDIDO_DT"] = System.DBNull.Value;
                newRow["PEDIDO_DT_AGEND"] = System.DBNull.Value;
                newRow["PEDIDO_AGENDAMENTOS"] = string.Empty;
                newRow["PEDIDO_PRESTADOR"] = string.Empty;
                newRow["PEDIDO_IS_PREVISAO"] = false;
                newRow["PEDIDO_STATUS"] = string.Empty; 
                newRow["PEDIDO_OBSERVACAO"] = obrigacaoCliente.Observacao;
            }

            if (dataProximo != new DateTime())
            {
                newRow["PROXIMO"] = dataProximo;
                newRow["MES"] = Convert.ToDateTime(dataProximo).Month;
            }

            if (pcmso.Id != 0)
            {
                newRow["VIGENCIA_PCMSO"] = pcmso.GetPeriodo();
                newRow["EXAMES_COMPLEMENTARES"] = pcmso.GetExamesComplementares();
            }

            newRow["EXAMES_EMPREGADOS"] = pcmso.GetNumeroPeriodicosPendentes(pcmso.GetDataProximoPeriodico());
            newRow["EXAMES_DIAS"] = GetDiasExame(Convert.ToDecimal(newRow["EXAMES_EMPREGADOS"]));
            newRow["EXAMES_CUSTO"] = Convert.ToDecimal(newRow["EXAMES_EMPREGADOS"]) 
                                    * cliente.GetCustoExamePeriodico();

            if (convocacao.Id != 0)
            {
                newRow["CONVOCACAO_DT"] = convocacao.DataConvocacao;
                newRow["CONVOCACAO_PRESTADOR"] = convocacao.IdPrestador.ToString();
                newRow["EXAMES_REALIZADOS"] = convocacao.GetNumeroPeriodicosRealizados();
                newRow["EXAMES_ESPERA"] = convocacao.GetNumeroPeriodicosEmEspera();
                newRow["EXAMES_PENDENTES"] = convocacao.GetNumeroPeriodicosPendentes()
                                            + convocacao.GetNumeroPeriodicosFaltou();
            }

            newRow["EMAIL"] = cliente.Email;
            newRow["CREDENCIADAS"] = cliente.GetClinicasCredenciadasParaPeriodicos();
            newRow["LOCAL_EXAME"] = cliente.GetLocalRealizacaoPeriodico();
            newRow["FORMA_ATUALIZACAO"] = cliente.GetFormaAtualizacaoPeriodico();

            if (ComEndereco)
            {
                newRow["ENDERECO"] = cliente.GetEndereco().GetEndereco();
                newRow["CIDADE"] = cliente.GetEndereco().GetCidade();
                newRow["ESTADO"] = cliente.GetEndereco().GetEstado();
                newRow["CEP"] = cliente.GetEndereco().Cep;
                newRow["TELEFONE"] = cliente.GetContatoTelefonico().GetDDDTelefone();
                newRow["CONTATO"] = cliente.GetContatoTelefonico().Nome;
            }
        }
        #endregion

        #region GetDiasExame

        public static decimal GetDiasExame(decimal numExames)
        {
            decimal dec = Math.Round(numExames / 50, 1, MidpointRounding.AwayFromZero);

            int num = Convert.ToInt32(dec);

            decimal dif = dec - num;

            decimal ret;

            if(num == 0)
                ret = 0.5M;
            else if (dif < 0.0M)
                ret = num;
            else if (dif < 0.5M)
                ret = num + 0.5M;
            else
                ret = num;

            return ret;
        }
        #endregion

        #region GetTable

        private static DataTable GetTable()
        {
            DataTable table = new DataTable("Result");
            table.Columns.Add("IdCliente", Type.GetType("System.Int32"));
            table.Columns.Add("CLIENTE", Type.GetType("System.String"));
            table.Columns.Add("GRUPO", Type.GetType("System.String"));
            table.Columns.Add("ENDERECO", Type.GetType("System.String"));
            table.Columns.Add("CIDADE", Type.GetType("System.String"));
            table.Columns.Add("ESTADO", Type.GetType("System.String"));
            table.Columns.Add("CEP", Type.GetType("System.String"));
            table.Columns.Add("TELEFONE", Type.GetType("System.String"));
            table.Columns.Add("CONTATO", Type.GetType("System.String"));
            table.Columns.Add("EMAIL", Type.GetType("System.String"));
            table.Columns.Add("OBRIGACAO", Type.GetType("System.String"));
            table.Columns.Add("QTD_EMPREGADOS", Type.GetType("System.Int32"));
            table.Columns.Add("VIGENCIA_PCMSO", Type.GetType("System.String"));
            table.Columns.Add("PENULTIMO", Type.GetType("System.String"));
            table.Columns.Add("ULTIMO", Type.GetType("System.String"));
            table.Columns.Add("VENCIMENTO", Type.GetType("System.DateTime"));
            table.Columns.Add("PROXIMO", Type.GetType("System.DateTime"));
            table.Columns.Add("MES", Type.GetType("System.Int32"));
            table.Columns.Add("STATUS", Type.GetType("System.String"));
            table.Columns.Add("IdPedido", Type.GetType("System.Int32"));
            table.Columns.Add("PEDIDO_NUM", Type.GetType("System.String"));
            table.Columns.Add("PEDIDO_DT", Type.GetType("System.DateTime"));
            table.Columns.Add("PEDIDO_DT_AGEND", Type.GetType("System.DateTime"));
            table.Columns.Add("PEDIDO_AGENDAMENTOS", Type.GetType("System.String"));
            table.Columns.Add("PEDIDO_PRESTADOR", Type.GetType("System.String"));
            table.Columns.Add("PEDIDO_IS_PREVISAO", Type.GetType("System.Boolean"));
            table.Columns.Add("PEDIDO_STATUS", Type.GetType("System.String"));
            table.Columns.Add("PEDIDO_OBSERVACAO", Type.GetType("System.String"));
            table.Columns.Add("CONVOCACAO_DT", Type.GetType("System.DateTime"));
            table.Columns.Add("CONVOCACAO_PRESTADOR", Type.GetType("System.String"));
            table.Columns.Add("EXAMES_DIAS", Type.GetType("System.Decimal"));
            table.Columns.Add("EXAMES_EMPREGADOS", Type.GetType("System.Int32"));
            table.Columns.Add("EXAMES_CUSTO", Type.GetType("System.Decimal"));
            table.Columns.Add("EXAMES_REALIZADOS", Type.GetType("System.Int32"));
            table.Columns.Add("EXAMES_PENDENTES", Type.GetType("System.Int32"));
            table.Columns.Add("EXAMES_ESPERA", Type.GetType("System.Int32"));
            table.Columns.Add("EXAMES_COMPLEMENTARES", Type.GetType("System.String"));
            table.Columns.Add("CREDENCIADAS", Type.GetType("System.String"));
            table.Columns.Add("LOCAL_EXAME", Type.GetType("System.String"));
            table.Columns.Add("FORMA_ATUALIZACAO", Type.GetType("System.String"));
            return table;
        }
        #endregion
    }
}
