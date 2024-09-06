using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using Ilitera.Data;
using Ilitera.Common;

namespace Ilitera.Opsa.Data
{
    public enum StatusPedidos : int
    {
        Sugestao,
        Pendente,
        Executado,
        Cancelado
    }

    [Database("opsa", "Pedido", "IdPedido")]
    public class Pedido : Ilitera.Data.Table
    {
        #region Contrutor
        private int _IdPedido;
        private int _IndStatus;
        private Cliente _IdCliente;
        private Obrigacao _IdObrigacao;
        private EquipamentoBase _IdEquipamentoBase;
        private Prestador _IdPrestador;
        private PedidoGrupo _IdPedidoGrupo;
        private DateTime _DataSugestao;
        private DateTime _DataSolicitacao;
        private DateTime _DataConclusao;
        private DateTime _DataCancelamento;
        private DateTime _DataPagamento;
        private DateTime _DataVencimento;
        private DateTime _DataUltimo;
        private double _ValorPago;
        private string _Observacao = string.Empty;


        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public Pedido()
        {

        }
        #endregion

        #region Enum

        public enum FiltroObrigacao : int
        {
            Pedido,
            Cipa,
            Treinamento
        }

        #endregion

        #region Propriedades

        public override int Id
        {
            get { return _IdPedido; }
            set { _IdPedido = value; }
        }

        public int IndStatus
        {
            get { return _IndStatus; }
            set { _IndStatus = value; }
        }

        [Obrigatorio(true, "Cliente é campo obrigatório!")]
        public Cliente IdCliente
        {
            get { return _IdCliente; }
            set { _IdCliente = value; }
        }

        [Obrigatorio(true, "Obrigação é campo obrigatório!")]
        public Obrigacao IdObrigacao
        {
            get { return _IdObrigacao; }
            set { _IdObrigacao = value; }
        }
        public EquipamentoBase IdEquipamentoBase
        {
            get { return _IdEquipamentoBase; }
            set { _IdEquipamentoBase = value; }
        }

        public Prestador IdPrestador
        {
            get { return _IdPrestador; }
            set { _IdPrestador = value; }
        }

        public PedidoGrupo IdPedidoGrupo
        {
            get { return _IdPedidoGrupo; }
            set { _IdPedidoGrupo = value; }
        }

        public DateTime DataSugestao
        {
            get { return _DataSugestao; }
            set { _DataSugestao = value; }
        }

        public DateTime DataSolicitacao
        {
            get { return _DataSolicitacao; }
            set { _DataSolicitacao = value; }
        }

        public DateTime DataConclusao
        {
            get { return _DataConclusao; }
            set { _DataConclusao = value; }
        }

        public DateTime DataCancelamento
        {
            get { return _DataCancelamento; }
            set { _DataCancelamento = value; }
        }

        public DateTime DataPagamento
        {
            get { return _DataPagamento; }
            set { _DataPagamento = value; }
        }
        public DateTime DataVencimento
        {
            get { return _DataVencimento; }
            set { _DataVencimento = value; }
        }
        public DateTime DataUltimo
        {
            get { return _DataUltimo; }
            set { _DataUltimo = value; }
        }
        [Persist(false)]
        public double ValorPago
        {
            get { return _ValorPago; }
            set { _ValorPago = value; }
        }
        public string Observacao
        {
            get { return _Observacao; }
            set { _Observacao = value; }
        }
        public override string ToString()
        {
            if (this.IdObrigacao == null)
                this.Find();

            if (this.IdObrigacao.IdDocumentoBase == null)
                this.IdObrigacao.Find();

            return this.IdObrigacao.NomeReduzido;
        }

        #endregion

        #region Metodos

        #region GetNumeroPedido

        public string GetNumeroPedido()
        {
            if (this.IdPedidoGrupo == null)
                this.Find();

            if (this.IdPedidoGrupo.mirrorOld == null)
                this.IdPedidoGrupo.Find();

            return this.IdPedidoGrupo.Numero.ToString("#,##0;-#,##0;-");
        }
        #endregion

        #region GetSituacaoPedido

        public string GetSituacaoPedido()
        {
            string ret = string.Empty;

            if (this.Id == 0)
            {
                ret = "Sem Pedido";
                return ret;
            }

            if (this.mirrorOld == null)
                this.Find();

            if (this.DataSolicitacao == new DateTime())
                ret = "Sugestão";
            else if (this.DataCancelamento != new DateTime())
                ret = "Cancelado";
            else if (this.DataConclusao != new DateTime())
                ret = "Concluído";
            else if (this.DataSolicitacao != new DateTime()
           && this.DataConclusao == new DateTime()
           && this.DataCancelamento == new DateTime())
            {
                string strCmd = "USE " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + " "
                             + " SELECT dbo.fn_SituacaoPedido (" + this.Id
                                                                 + ", " + this.IdObrigacao.Id + ")";
                ret = new Pedido().ExecuteScalar(strCmd);
            }
            return ret;
        }
        #endregion

        #region GetAgendamentoPedido

        public string GetAgendamentoPedido()
        {
            string ret;

            if (this.IdPedidoGrupo.mirrorOld == null)
                this.IdPedidoGrupo.Find();

            if (this.IdPedidoGrupo.IdCompromisso.Id == 0)
                ret = "Não Agendado";
            else
                ret = this.IdPedidoGrupo.IdCompromisso.GetDataComprimisso();

            return ret;
        }
        #endregion

        #region GetShortAgendamentoPedido

        public string GetShortAgendamentoPedido()
        {
            string ret;

            if (this.IdPedidoGrupo.mirrorOld == null)
                this.IdPedidoGrupo.Find();

            if (this.IdPedidoGrupo.IdCompromisso.Id == 0)
                ret = "-";
            else
                ret = this.IdPedidoGrupo.IdCompromisso.GetShortDataComprimisso();

            return ret;
        }
        #endregion

        #region GetIsAgendamentoPedido

        public bool GetIsAgendamentoPedido()
        {
            bool ret;

            if (this.IdPedidoGrupo.mirrorOld == null)
                this.IdPedidoGrupo.Find();

            if (this.IdPedidoGrupo.IdCompromisso.Id == 0)
                ret = false;
            else
                ret = this.IdPedidoGrupo.IdCompromisso.AConfirmar;

            return ret;
        }
        #endregion

        #region GetDataAgendamentoPedido

        public DateTime GetDataAgendamentoPedido()
        {
            DateTime ret;

            if (this.IdPedidoGrupo.mirrorOld == null)
                this.IdPedidoGrupo.Find();

            if (this.IdPedidoGrupo.IdCompromisso.Id == 0)
                ret = new DateTime();
            else
            {
                if (this.IdPedidoGrupo.IdCompromisso.mirrorOld == null)
                    this.IdPedidoGrupo.IdCompromisso.Find();

                ret = this.IdPedidoGrupo.IdCompromisso.DataInicio;
            }

            return ret;
        }
        #endregion

        #region Valida Pedido

        public override void Validate()
        {
            base.Validate();
        }

        private void VerificaPedido()
        {
            SetStatusPedido();

            ConcluirDocumentos();

            ExcluirCompromissosParaPedidosCancelados();

            SetPagamentoPedido();

            if (mirrorOld != null)
            {
                if (((Pedido)this.mirrorOld).DataConclusao != new DateTime()
                    && this.DataConclusao == new DateTime())
                    VerificaPedidoFaturamento();

                if (((Pedido)this.mirrorOld).DataCancelamento == new DateTime()
                && this.DataCancelamento != new DateTime())
                    VerificaPedidoFaturamento();
            }
        }

        private void VerificaPedidoFaturamento()
        {
            FaturamentoPedido faturamentoPedido = new FaturamentoPedido();
            faturamentoPedido.Find("IdPedido=" + this.Id);

            if (faturamentoPedido.Id != 0)
                throw new Exception("O pedido não pode ser modificado por estar faturado!");
        }

        private void SetStatusPedido()
        {
            if (this.DataSolicitacao == new DateTime() && this.DataConclusao == new DateTime())
                this.IndStatus = (int)StatusPedidos.Sugestao;
            else if (this.DataSolicitacao != new DateTime() && this.DataConclusao == new DateTime())
                this.IndStatus = (int)StatusPedidos.Pendente;
            else if (this.DataConclusao != new DateTime())
                this.IndStatus = (int)StatusPedidos.Executado;
        }

        #region ConcluirDocumentos

        private void ConcluirDocumentos()
        {
            if (this.DataConclusao == new DateTime())
                return;

            if (this.IdObrigacao.mirrorOld == null)
                this.IdObrigacao.Find();

            if (this.IdObrigacao.IdDocumentoBase.Id == 0)
                return;

            switch (this.IdObrigacao.IdDocumentoBase.Id)
            {
                case (int)Documentos.PPRA:
                    ConcluirDocumentoPPRA();
                    break;

                case (int)Documentos.Auditoria:
                case (int)Documentos.CIPA:
                case (int)Documentos.PCMSO:
                case (int)Documentos.ExamePeriodico:
                case (int)Documentos.VasosProjeto:
                case (int)Documentos.VasosInspecao:
                case (int)Documentos.CaldeiraProjeto:
                case (int)Documentos.CaldeiraInspecao:
                case (int)Documentos.Treinamentos:
                case (int)Documentos.MapaRisco:

                    Documento documento = new Documento();
                    documento.Find("IdPedido=" + this.Id);

                    if (documento.Id == 0)
                    {
                        throw new Exception("É necessário primeiro criar um novo "
                                            + this.IdObrigacao.IdDocumentoBase.ToString() + ", antes de concluir!");
                    }
                    else
                        this.DataConclusao = this.GetDataDocumento();

                    break;
            }

            if (this.IdObrigacao.IdDocumentoBase.Id == (int)Documentos.Treinamentos)
                ConcluirOutrosTreinamentosDoMesmoPedido();
        }
        #endregion

        #region ConcluirDocumentoPPRA

        private void ConcluirDocumentoPPRA()
        {
            LaudoTecnico laudo = new LaudoTecnico();
            laudo.Find("nID_PEDIDO=" + this.Id);

            if (laudo.Id == 0)
                throw new Exception("Pedido não vinculado a nenhum SIED, não pode ser concluído!");
            else
                this.DataConclusao = this.GetDataDocumento();

            if (this.IdCliente.mirrorOld == null)
                this.IdCliente.Find();
        }
        #endregion

        #region ExcluirCompromissosParaPedidosCancelados

        private void ExcluirCompromissosParaPedidosCancelados()
        {
            //Exclui os compromissos relacionados as pedidos que foram cancelados
            if (this.IdPedidoGrupo.Id != 0 && this.DataCancelamento != new DateTime())
            {
                if (this.IdPedidoGrupo.IdCompromisso == null)
                    this.IdPedidoGrupo.Find();

                if (this.IdPedidoGrupo.IdCompromisso.Id != 0)
                {
                    int IdCompromisso = this.IdPedidoGrupo.IdCompromisso.Id;

                    this.IdPedidoGrupo.IdCompromisso = new Compromisso();
                    this.IdPedidoGrupo.Save();

                    Compromisso compromisso = new Compromisso();
                    compromisso.Find(IdCompromisso);
                    compromisso.Delete();
                }
            }
        }
        #endregion

        #region SetPagamentoPedido

        private void SetPagamentoPedido()
        {
            //Para pagamento do Pedido			
            if (this.Id == 0 && this.DataPagamento != new DateTime() && this.DataPagamento != new DateTime(1753, 1, 1))
                throw new Exception("Primeiro salve o pedido e depois coloque sua data de pagamento!");

            PagamentoPrestador pgto = new PagamentoPrestador();
            pgto.Find("IdPedido=" + this.Id);

            if (this.DataPagamento != new DateTime()
                && this.DataPagamento != new DateTime(1753, 1, 1))
            {
                if (pgto.Id == 0)
                {
                    pgto.Inicialize();
                    pgto.IdPedido.Id = this.Id;
                    pgto.IdPrestador.Id = this.IdPrestador.Id;
                    pgto.IdCliente.Id = this.IdCliente.Id;
                    pgto.IdObrigacao.Id = this.IdObrigacao.Id;
                }
                pgto.Data = this.DataPagamento;
                pgto.Valor = this.ValorPago;
                pgto.Save();
            }
            else
            {
                if (pgto.Id != 0)
                    pgto.Delete();
            }
        }
        #endregion

        #region ConcluirOutrosTreinamentosDoMesmoPedido

        private void ConcluirOutrosTreinamentosDoMesmoPedido()
        {
            //Concluir os outros treinamentos do mesmo pedido
            if (this.IdObrigacao.IdDocumentoBase.Id != (int)Documentos.Treinamentos)
                return;

            if (this.DataConclusao == new DateTime())
                return;

            string criteria = "IdPedidoGrupo=" + this.IdPedidoGrupo.Id
                            + " AND IdPedido<>" + this.Id
                            + " AND IdPedido NOT IN (SELECT IdPedido FROM Documento)"
                            + " AND IdObrigacao IN (SELECT IdObrigacao"
                            + " FROM Obrigacao"
                            + " WHERE IdDocumentoBase=" + (int)Documentos.Treinamentos + ")";

            List<Pedido> pedidos = new Pedido().Find<Pedido>(criteria);

            foreach (Pedido pedido in pedidos)
            {
                pedido.DataConclusao = this.DataConclusao;
                pedido.Save();
            }
        }
        #endregion

        #region GetDataDocumento

        public DateTime GetDataDocumento()
        {
            DateTime dataDocumento = DateTime.Today;

            if (this.IdObrigacao.IdDocumentoBase == null)
                this.IdObrigacao.Find();

            switch (this.IdObrigacao.IdDocumentoBase.Id)
            {
                case (int)Documentos.PPRA:
                    {
                        LaudoTecnico laudo = new LaudoTecnico();

                        ArrayList list = laudo.Find("nID_PEDIDO=" + this.Id);

                        if (list.Count > 1)
                            throw new Exception("Pedido com mais de um levantamento!");

                        dataDocumento = laudo.hDT_LAUDO;
                        break;
                    }
                case (int)Documentos.Auditoria:
                case (int)Documentos.CIPA:
                case (int)Documentos.ExamePeriodico:
                case (int)Documentos.PCMSO:
                case (int)Documentos.Treinamentos:
                case (int)Documentos.VasosInspecao:
                case (int)Documentos.VasosProjeto:
                case (int)Documentos.CaldeiraInspecao:
                case (int)Documentos.CaldeiraProjeto:
                case (int)Documentos.MapaRisco:
                    {
                        Documento documento = new Documento();

                        ArrayList list = documento.Find("IdPedido=" + this.Id);

                        if (list.Count > 1)
                            throw new Exception("Pedido com mais de um " + this.IdObrigacao.IdDocumentoBase.ToString() + "!");

                        dataDocumento = documento.DataLevantamento;
                        break;
                    }
            }

            return new DateTime(dataDocumento.Year, dataDocumento.Month, dataDocumento.Day);
        }
        #endregion

        #endregion

        #region GetControlePedido

        public ControlePedido GetControlePedido()
        {
            return GetControlePedido(false, false);
        }

        public ControlePedido GetControlePedido(bool IsProducao)
        {
            return GetControlePedido(IsProducao, false);
        }

        private ControlePedido GetControlePedido(bool IsProducao, bool bVal)
        {
            StringBuilder str = new StringBuilder();
            str.Append("IdPedido=" + this.Id + " AND Termino IS NULL");
            if (IsProducao)
            {
                str.Append(" AND IdControle IN (SELECT IdControle FROM ControleObrigacao");
                str.Append(" WHERE IdObrigacao=" + this.IdObrigacao.Id);
                str.Append(" AND IsProducao=1)");
            }
            str.Append(" ORDER BY Ordem");

            ControlePedido controlePedido;

            ArrayList listControlePedido = new ControlePedido().Find(str.ToString());

            if (listControlePedido.Count > 0)
                controlePedido = ((ControlePedido)listControlePedido[0]);
            else
            {
                if (!bVal)
                {
                    ControlePedido.GerarDatario(this);
                    controlePedido = this.GetControlePedido(IsProducao, true);
                }
                else
                    controlePedido = new ControlePedido();
            }
            return controlePedido;
        }

        #endregion

        #region Save

        public override int Save()
        {
            bool bAtualiza = false;

            bAtualiza = mirrorOld != null
                        && ((Pedido)this.mirrorOld).DataConclusao != this.DataConclusao
                        && this.IdObrigacao.IsPPRA();

            VerificaPedido();

            int ret = base.Save();

            if (bAtualiza)
            {
                //Atualizar PCMSO
                ObrigacaoCliente obrigacaoClientePCMSO = ObrigacaoCliente.GetObrigacaoCliente(this.IdCliente, 
                                                          new Obrigacao((int)Obrigacoes.PCMSO));
                obrigacaoClientePCMSO.Atualizar();

                //Atualizar Ordem de Serviço
                ObrigacaoCliente obrigacaoClienteOS = ObrigacaoCliente.GetObrigacaoCliente(this.IdCliente, 
                                                        new Obrigacao((int)Obrigacoes.OrdemServico));
                obrigacaoClienteOS.Atualizar();
            }

            return ret;
        }
        #endregion

        #endregion

        #region Metodos Staticos

        public static Pedido GetUltimoPedido(Obrigacao obrigacao, Cliente cliente)
        {
            return GetUltimoPedido(obrigacao.Id, cliente.Id);
        }

        public static Pedido GetPenultimoPedido(int IdObrigacao, int IdCliente)
        {
            StringBuilder str = new StringBuilder();

            str.Append("DataConclusao IS NOT NULL");
            str.Append(" AND DataCancelamento IS NULL");
            str.Append(" AND IdCliente=" + IdCliente);
            str.Append(" AND IdObrigacao=" + IdObrigacao);
            str.Append(" ORDER BY DataConclusao DESC");

            ArrayList listPedido = new Pedido().Find(str.ToString());

            if (listPedido.Count < 2)
                return new Pedido();
            else
                return (Pedido)listPedido[1];
        }

        public static Pedido GetUltimoPedido(int IdObrigacao, int IdCliente, int IdEquipamentoBase)
        {
            StringBuilder str = new StringBuilder();
            str.Append("DataConclusao IS NOT NULL");
            str.Append(" AND DataCancelamento IS NULL");
            str.Append(" AND IdCliente=" + IdCliente);
            str.Append(" AND IdObrigacao=" + IdObrigacao);

            if (IdEquipamentoBase != 0)
                str.Append(" AND IdEquipamentoBase=" + IdEquipamentoBase);

            ArrayList listPedido = new Pedido().FindMax("DataConclusao", str.ToString());

            if (listPedido.Count == 0)
                return new Pedido();
            else
                return (Pedido)listPedido[0];
        }

        public static Pedido GetUltimoPedido(int IdObrigacao, int IdCliente)
        {
            return GetUltimoPedido(IdObrigacao, IdCliente, 0);
        }

        public static Pedido GetSugestao(int IdObrigacao, int IdCliente)
        {
            StringBuilder str = new StringBuilder();
            str.Append("DataSolicitacao IS NULL");
            str.Append(" AND DataConclusao IS NULL");
            str.Append(" AND DataCancelamento IS NULL");
            str.Append(" AND IdCliente=" + IdCliente);
            str.Append(" AND IdObrigacao=" + IdObrigacao);

            ArrayList list = new Pedido().Find(str.ToString());

            if (list.Count == 0)
                return new Pedido();
            else
                return (Pedido)list[0];
        }

        public static Pedido GetPedidoPendente(int IdObrigacao, int IdCliente, int IdEquipamentoBase)
        {
            StringBuilder str = new StringBuilder();
            str.Append("DataSolicitacao IS NOT NULL");
            str.Append(" AND DataConclusao IS NULL");
            str.Append(" AND DataCancelamento IS NULL");
            str.Append(" AND IdCliente=" + IdCliente);
            str.Append(" AND IdObrigacao=" + IdObrigacao);

            if (IdEquipamentoBase != 0)
                str.Append(" AND IdEquipamentoBase=" + IdEquipamentoBase);
            else
                str.Append(" AND IdEquipamentoBase IS NULL");

            ArrayList list = new Pedido().Find(str.ToString());

            if (list.Count == 0)
                return new Pedido();
            else
                return (Pedido)list[0];
        }

        public static Pedido GetPedidoPendente(int IdObrigacao, int IdCliente)
        {
            return GetPedidoPendente(IdObrigacao, IdCliente, 0);
        }

        #endregion

        #region Consultas GridPedidos

        /***
         * Não terminado
         * Juntas as consulta em um método só
         * igual as CriteriaBuilderEmpregado
         */

        #region GetGridPedidosSQL

        public static string GetGridPedidosSQL( StatusPedidos status,
                                                bool Atrasados,
                                                FiltroObrigacao filtroObrigacao,
                                                Prestador prestador,
                                                Obrigacao obrigacao,
                                                Controle controle,
                                                Cliente cliente,
                                                bool TodosGrupo,
                                                bool PeriodicoAnualEmpresa,
                                                bool PorPeriodo,
                                                DateTime de,
                                                DateTime ate)
        {
            string ret = string.Empty;

            switch (status)
            {
                case StatusPedidos.Sugestao:
                    break;
                case StatusPedidos.Pendente:
                    break;
                case StatusPedidos.Executado:
                    break;
                case StatusPedidos.Cancelado:
                    break;
                default:
                    break;
            }
            return ret;
        }
        #endregion

        #region GetGridPedidosSugestaoSQL

        public static DataSet GetGridPedidosSugestaoSQL(bool Atrasados,
                                                        FiltroObrigacao filtroObrigacao,
                                                        Obrigacao obrigacao,
                                                        Cliente cliente,
                                                        bool TodosGrupo,
                                                        bool PeriodicoAnualEmpresa,
                                                        bool PorPeriodo,
                                                        DateTime de,
                                                        DateTime ate)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("USE " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + " ");
            sql.Append(" SELECT  IdPedido, IdCliente, Cliente, Obrigação, Prestador, Posse, Último, Sugestão, Vencimento, Observação As Observacao");
            sql.Append(" FROM qryPedidosSugestao");
            sql.Append(" WHERE");
            
            if (cliente.Id != 0)
            {
                if (TodosGrupo)
                    sql.Append(" IdCliente IN (SELECT IdJuridica FROM Juridica WHERE IdGrupoEmpresa =" + cliente.IdGrupoEmpresa.Id + ")");
                else
                    sql.Append(" IdCliente=" + cliente.Id);
            }
            else
                sql.Append(" IdCliente=IdCliente");

            if (PeriodicoAnualEmpresa)
            {
                sql.Append(" AND IdCliente IN (SELECT IdCliente FROM Cliente WHERE ");
                sql.Append(" IndAtualizacaoPeriodico=" + (int)Cliente.AtualizacaoPeriodico.Anual);
                sql.Append(" AND IndRealizacaoPeriodico=" + (int)Cliente.RealizacaoPeriodico.Empresa);
                sql.Append(")");
            }

            if (Atrasados)
                sql.Append(" AND Vencimento <='" + DateTime.Today.ToString("yyyy-MM-dd") + "'");

            if (filtroObrigacao == FiltroObrigacao.Cipa)
                sql.Append(" AND IndObrigacaoTipo=" + (int)ObrigacaoTipo.Cipa);
            else if (filtroObrigacao == FiltroObrigacao.Treinamento)
                sql.Append(" AND IndObrigacaoTipo=" + (int)ObrigacaoTipo.Treinamento);
            else
                sql.Append(" AND IndObrigacaoTipo<>" + (int)ObrigacaoTipo.Cipa);

            if (obrigacao != null && obrigacao.Id != 0)
                sql.Append(" AND IdObrigacao =" + obrigacao.Id);

            if (PorPeriodo)
            {
                sql.Append(" AND Sugestão BETWEEN '" + de.ToString("yyyy-MM-dd") + "'");
                sql.Append(" AND '" + ate.ToString("yyyy-MM-dd") + "'");
            }

            if (cliente.Id != 0)
                sql.Append(" ORDER BY Sugestão, Obrigação");
            else
                sql.Append(" ORDER BY Cliente, Obrigação");

            return new Pedido().ExecuteDataset(sql.ToString());
        }
        #endregion

        #region GetGridPedidosPendentesSQL

        public static DataSet GetGridPedidosPendentesSQL(FiltroObrigacao filtroObrigacao,
                                                            Prestador prestador,
                                                            Obrigacao obrigacao,
                                                            Controle controle,
                                                            Cliente cliente,
                                                            bool TodosGrupo,
                                                            bool PeriodicoAnualEmpresa,
                                                            bool PorPeriodo,
                                                            DateTime de,
                                                            DateTime ate)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("USE " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + " ");
            sql.Append(" SELECT IdPedido, IdCliente, Cliente, Numero, Obrigação, Prestador, Solicitação, IdControle, Situação, C, Vencimento, Observacao, Equipamento ");
            sql.Append(" FROM dbo.qryPedidosPendentes");
            sql.Append(" WHERE");

            if (filtroObrigacao == FiltroObrigacao.Cipa)
                sql.Append(" IndObrigacaoTipo=" + (int)ObrigacaoTipo.Cipa);
            else if (filtroObrigacao == FiltroObrigacao.Treinamento)
                sql.Append(" IndObrigacaoTipo=" + (int)ObrigacaoTipo.Treinamento);
            else
                sql.Append(" IndObrigacaoTipo<>" + (int)ObrigacaoTipo.Cipa);
            
            if (cliente.Id != 0)
            {
                if (TodosGrupo)
                    sql.Append(" AND IdCliente IN (SELECT IdJuridica FROM Juridica WHERE IdGrupoEmpresa =" + cliente.IdGrupoEmpresa.Id + ")");
                else
                    sql.Append(" AND IdCliente=" + cliente.Id);
            }

            if (PeriodicoAnualEmpresa)
            {
                sql.Append(" AND IdCliente IN (SELECT IdCliente FROM Cliente WHERE ");
                sql.Append(" IndAtualizacaoPeriodico=" + (int)Cliente.AtualizacaoPeriodico.Anual);
                sql.Append(" AND IndRealizacaoPeriodico=" + (int)Cliente.RealizacaoPeriodico.Empresa);
                sql.Append(")");
            }
            
            if (obrigacao != null && obrigacao.Id != 0)
                sql.Append(" AND IdObrigacao =" + obrigacao.Id);

            if (prestador != null && prestador.Id != 0)
                sql.Append(" AND IdPrestador =" + prestador.Id);

            if (controle != null && controle.Id != 0)
                sql.Append(" AND IdControle =" + controle.Id);

            if (PorPeriodo)
            {
                sql.Append(" AND Solicitação BETWEEN '" + de.ToString("yyyy-MM-dd") + "'");
                sql.Append(" AND '" + ate.ToString("yyyy-MM-dd") + "'");
            }

            return new Pedido().ExecuteDataset(sql.ToString());
        }
        #endregion

        #region GetGridPedidosExecutadosSQL

        public static DataSet GetGridPedidosExecutadosSQL(FiltroObrigacao filtroObrigacao,
                                                            Prestador prestador,
                                                            Obrigacao obrigacao,
                                                            Cliente cliente,
                                                            bool TodosGrupo, 
                                                            bool PeriodicoAnualEmpresa,
                                                            bool PorPeriodo,
                                                            DateTime de,
                                                            DateTime ate)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("USE " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + " ");
            sql.Append(" SELECT IdPedido, IdCliente, Cliente, Numero, Obrigação, Prestador, Conclusão, Pagamento, Vencimento");
            sql.Append(" FROM qryPedidosExecutados");
            sql.Append(" WHERE ");

            if (cliente.Id != 0)
            {
                if (TodosGrupo)
                    sql.Append(" IdCliente IN (SELECT IdJuridica FROM Juridica WHERE IdGrupoEmpresa =" + cliente.IdGrupoEmpresa.Id + ")");
                else
                    sql.Append(" IdCliente=" + cliente.Id);
            }
            else
                sql.Append(" IdCliente=IdCliente");

            if (PeriodicoAnualEmpresa)
            {
                sql.Append(" AND IdCliente IN (SELECT IdCliente FROM Cliente WHERE ");
                sql.Append(" IndAtualizacaoPeriodico=" + (int)Cliente.AtualizacaoPeriodico.Anual);
                sql.Append(" AND IndRealizacaoPeriodico=" + (int)Cliente.RealizacaoPeriodico.Empresa);
                sql.Append(")");
            }

            if (filtroObrigacao == FiltroObrigacao.Cipa)
                sql.Append(" AND IndObrigacaoTipo=" + (int)ObrigacaoTipo.Cipa);
            else if (filtroObrigacao == FiltroObrigacao.Treinamento)
                sql.Append(" AND IndObrigacaoTipo=" + (int)ObrigacaoTipo.Treinamento);
            else
                sql.Append(" AND IndObrigacaoTipo<>" + (int)ObrigacaoTipo.Cipa);

            if (obrigacao != null && obrigacao.Id != 0)
                sql.Append(" AND IdObrigacao =" + obrigacao.Id);

            if (prestador != null && prestador.Id != 0)
                sql.Append(" AND IdPrestador =" + prestador.Id);

            if (PorPeriodo)
            {
                sql.Append(" AND Conclusão BETWEEN '" + de.ToString("yyyy-MM-dd") + "'");
                sql.Append(" AND '" + ate.ToString("yyyy-MM-dd") + "'");
            }

            if (cliente.Id != 0)
                sql.Append(" ORDER BY Conclusão DESC, Obrigação");
            else
                sql.Append(" ORDER BY Cliente, Obrigação");

            return new Pedido().ExecuteDataset(sql.ToString());
        }
        #endregion

        #region GetGridPedidosCanceladosSQL

        public static DataSet GetGridPedidosCanceladosSQL(FiltroObrigacao filtroObrigacao,
                                                            Prestador prestador,
                                                            Obrigacao obrigacao,
                                                            Cliente cliente,
                                                            bool TodosGrupo,
                                                            bool PeriodicoAnualEmpresa,
                                                            bool PorPeriodo,
                                                            DateTime de,
                                                            DateTime ate)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("USE " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + " ");
            sql.Append(" SELECT  IdPedido, IdCliente, Cliente, Numero, Obrigação, Prestador, Conclusão, Cancelado");
            sql.Append(" FROM qryPedidosCancelados");
            sql.Append(" WHERE ");

            if (cliente.Id != 0)
            {
                if (TodosGrupo)
                    sql.Append(" IdCliente IN (SELECT IdJuridica FROM Juridica WHERE IdGrupoEmpresa =" + cliente.IdGrupoEmpresa.Id + ")");
                else
                    sql.Append(" IdCliente=" + cliente.Id);
            }
            else
                sql.Append(" IdCliente = IdCliente");

            if (PeriodicoAnualEmpresa)
            {
                sql.Append(" AND IdCliente IN (SELECT IdCliente FROM Cliente WHERE ");
                sql.Append(" IndAtualizacaoPeriodico=" + (int)Cliente.AtualizacaoPeriodico.Anual);
                sql.Append(" AND IndRealizacaoPeriodico=" + (int)Cliente.RealizacaoPeriodico.Empresa);
                sql.Append(")");
            }

            if (filtroObrigacao == FiltroObrigacao.Cipa)
                sql.Append(" AND IndObrigacaoTipo=" + (int)ObrigacaoTipo.Cipa);
            else if (filtroObrigacao == FiltroObrigacao.Treinamento)
                sql.Append(" AND IndObrigacaoTipo=" + (int)ObrigacaoTipo.Treinamento);
            else
                sql.Append(" AND IndObrigacaoTipo<>" + (int)ObrigacaoTipo.Cipa);

            if (obrigacao != null && obrigacao.Id != 0)
                sql.Append(" AND IdObrigacao =" + obrigacao.Id);

            if (prestador != null && prestador.Id != 0)
                sql.Append(" AND IdPrestador =" + prestador.Id);

            if (PorPeriodo)
            {
                sql.Append(" AND Cancelado BETWEEN '" + de.ToString("yyyy-MM-dd") + "'");
                sql.Append(" AND '" + ate.ToString("yyyy-MM-dd") + "'");
            }

            if (cliente.Id != 0)
                sql.Append(" ORDER BY Cancelado DESC");
            else
                sql.Append(" ORDER BY Cliente, Obrigação, Cancelado DESC");

            return new Pedido().ExecuteDataset(sql.ToString());
        }
        #endregion

        #endregion
    }

    #region PedidoPDT

    [Database("pdtdos", "PEDIDOS", "ID")]
    public class PedidoPDT : Ilitera.Data.Table
    {
        private int _ID;
        private Cadastro _CODCAD;
        private Prestador _PRESTAD;
        private Atividade _COD_ATIV;
        private DateTime _DT_SOLICIT;
        private DateTime _DT_CONCLUS;
        private DateTime _DT_PAGTO;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public PedidoPDT()
        {

        }
        public override int Id
        {
            get { return _ID; }
            set { _ID = value; }
        }
        public Cadastro CODCAD
        {
            get { return _CODCAD; }
            set { _CODCAD = value; }
        }
        public Prestador PRESTAD
        {
            get { return _PRESTAD; }
            set { _PRESTAD = value; }
        }
        public Atividade COD_ATIV
        {
            get { return _COD_ATIV; }
            set { _COD_ATIV = value; }
        }
        public DateTime DT_SOLICIT
        {
            get { return _DT_SOLICIT; }
            set { _DT_SOLICIT = value; }
        }
        public DateTime DT_CONCLUS
        {
            get { return _DT_CONCLUS; }
            set { _DT_CONCLUS = value; }
        }
        public DateTime DT_PAGTO
        {
            get { return _DT_PAGTO; }
            set { _DT_PAGTO = value; }
        }

        public static DataSet GetGridPedidos(bool Pendentes, Prestador prestador, Atividade atividade, Cadastro cadastro)
        {
            DataSet ds = new DataSet();
            DataTable table = new DataTable("Result");
            table.Columns.Add("NUM.", Type.GetType("System.Int32"));
            table.Columns.Add("ATIVIDADE", Type.GetType("System.String"));
            table.Columns.Add("PRESTADOR", Type.GetType("System.String"));
            table.Columns.Add("SOLICIT.", Type.GetType("System.DateTime"));
            table.Columns.Add("CONCLUSÃO", Type.GetType("System.DateTime"));
            table.Columns.Add("PGTO", Type.GetType("System.DateTime"));
            ds.Tables.Add(table);
            DataRow newRow;
            StringBuilder where = new StringBuilder();
            where.Append("CODCAD=" + cadastro.Id);
            if (Pendentes)
                where.Append(" AND DT_CONCLUS IS NULL");
            else
                where.Append(" AND DT_CONCLUS IS NOT NULL");
            if (atividade != null && atividade.Id != 0)
                where.Append(" AND COD_ATIV =" + atividade.Id);
            if (prestador != null && prestador.Id != 0)
                where.Append(" AND PRESTAD =" + prestador.Id);
            if (Pendentes)
                where.Append(" ORDER BY DT_SOLICIT");
            else
                where.Append(" ORDER BY DT_CONCLUS");
            ArrayList list = new PedidoPDT().Find(where.ToString());
            for (int i = 0; i < list.Count; i++)
            {
                ((PedidoPDT)list[i]).CODCAD.Find();
                ((PedidoPDT)list[i]).COD_ATIV.Find();
                ((PedidoPDT)list[i]).PRESTAD.Find();
                newRow = ds.Tables[0].NewRow();
                newRow["NUM."] = ((PedidoPDT)list[i]).Id;
                newRow["ATIVIDADE"] = ((PedidoPDT)list[i]).COD_ATIV.ToString();
                newRow["PRESTADOR"] = ((PedidoPDT)list[i]).PRESTAD.NomeAbreviado;
                newRow["SOLICIT."] = Ilitera.Common.Utility.TratarDateTime(((PedidoPDT)list[i]).DT_SOLICIT);
                newRow["CONCLUSÃO"] = Ilitera.Common.Utility.TratarDateTime(((PedidoPDT)list[i]).DT_CONCLUS);
                newRow["PGTO"] = Ilitera.Common.Utility.TratarDateTime(((PedidoPDT)list[i]).DT_PAGTO);
                ds.Tables[0].Rows.Add(newRow);
            }
            return ds;
        }
    }

    #endregion

    #region CadastroPDT

    [Database("pdtdos", "CADASTRO", "CODIGO")]
    public class Cadastro : Ilitera.Data.Table
    {
        private int _CODIGO;
        private string _TIPO;
        private string _NOME;
        private string _RAZAO_SOC;
        private string _ENDERECO;
        private string _BAIRRO;
        private string _CIDADE;
        private string _ESTADO;
        private string _CEP;
        private string _CGC;
        private string _INSCR;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public Cadastro()
        {
        }
        public override int Id
        {
            get { return _CODIGO; }
            set { _CODIGO = value; }
        }
        public string TIPO
        {
            get { return _TIPO; }
            set { _TIPO = value; }
        }
        public string NOME
        {
            get { return _NOME; }
            set { _NOME = value; }
        }
        public string RAZAO_SOC
        {
            get { return _RAZAO_SOC; }
            set { _RAZAO_SOC = value; }
        }
        public string ENDERECO
        {
            get { return _ENDERECO; }
            set { _ENDERECO = value; }
        }
        public string BAIRRO
        {
            get { return _BAIRRO; }
            set { _BAIRRO = value; }
        }
        public string CIDADE
        {
            get { return _CIDADE; }
            set { _CIDADE = value; }
        }
        public string ESTADO
        {
            get { return _ESTADO; }
            set { _ESTADO = value; }
        }
        public string CEP
        {
            get { return _CEP; }
            set { _CEP = value; }
        }
        public string CGC
        {
            get { return _CGC; }
            set { _CGC = value; }
        }
        public string INSCR
        {
            get { return _INSCR; }
            set { _INSCR = value; }
        }
    }
    #endregion
}
