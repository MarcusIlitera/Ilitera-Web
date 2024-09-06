using System;
using System.Data;
using System.Text;
using System.Collections.Generic;

using Ilitera.Data;
using Ilitera.Common;

namespace Ilitera.Opsa.Data
{
    public enum RegraTipo : int
    {
        Lei,
        Sindicato,
        Cliente
    }

    [Database("opsa", "ObrigacaoCliente", "IdObrigacaoCliente")]
    public class ObrigacaoCliente : Ilitera.Data.Table, IRegra
    {
        #region Properties
        private int _IdObrigacaoCliente;
        private Cliente _IdCliente;
        private Obrigacao _IdObrigacao;
        private Prestador _IdPrestador;
        private Compromisso _IdCompromisso;
        private bool _IsContratada = true;
        private bool _IsProForma = false;
        private DateTime _DataUltimo;
        private DateTime _DataVencimento;
        private DateTime _DataProximo;
        private Pedido _IdPedidoPendente;
        private short _IndRegraTipo;
        private string _Observacao = string.Empty;
        private int _MinEmpregado;
        private int _DiasExecutar;
        private int _DiasExecutarPrimeira;
        private int _IndTipoPeriodicidade;
        private DateTime _DataExecutar;
        private int _IndPeriodicidade;
        private int _Intervalo;
        private int _Dia;
        private int _Mes;
        private Obrigacao _IdObrigacaoBase;
        private Obrigacao _IdObrigacaoBasePrimeira;
        private short _IndFeriado;
        private short _IndFeriadoPrimeira;
        private short _IndFeriadoPeriodico;
        private short _IndPeriodoExecutar;
        private short _IndPeriodoExecutarPrimeira;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public ObrigacaoCliente()
        {

        }
        public override int Id
        {
            get { return _IdObrigacaoCliente; }
            set { _IdObrigacaoCliente = value; }
        }
        public Cliente IdCliente
        {
            get { return _IdCliente; }
            set { _IdCliente = value; }
        }
        public Obrigacao IdObrigacao
        {
            get { return _IdObrigacao; }
            set { _IdObrigacao = value; }
        }
        public Prestador IdPrestador
        {
            get { return _IdPrestador; }
            set { _IdPrestador = value; }
        }
        public Compromisso IdCompromisso
        {
            get { return _IdCompromisso; }
            set { _IdCompromisso = value; }
        }
        public bool IsContratada
        {
            get { return _IsContratada; }
            set { _IsContratada = value; }
        }
        public bool IsProForma
        {
            get { return _IsProForma; }
            set { _IsProForma = value; }
        }

        public DateTime DataUltimo
        {
            get { return _DataUltimo; }
            set { _DataUltimo = value; }
        }
        public DateTime DataVencimento
        {
            get { return _DataVencimento; }
            set { _DataVencimento = value; }
        }
        public DateTime DataProximo
        {
            get { return _DataProximo; }
            set { _DataProximo = value; }
        }
        public Pedido IdPedidoPendente
        {
            get { return _IdPedidoPendente; }
            set { _IdPedidoPendente = value; }
        }
        public short IndRegraTipo
        {
            get { return _IndRegraTipo; }
            set { _IndRegraTipo = value; }
        }
        public string Observacao
        {
            get { return _Observacao; }
            set { _Observacao = value; }
        }
        public int MinEmpregado
        {
            get { return _MinEmpregado; }
            set { _MinEmpregado = value; }
        }
        public int DiasExecutar
        {
            get { return _DiasExecutar; }
            set { _DiasExecutar = value; }
        }
        public int DiasExecutarPrimeira
        {
            get { return _DiasExecutarPrimeira; }
            set { _DiasExecutarPrimeira = value; }
        }
        public int IndTipoPeriodicidade
        {
            get { return _IndTipoPeriodicidade; }
            set { _IndTipoPeriodicidade = value; }
        }
        public DateTime DataExecutar
        {
            get { return _DataExecutar; }
            set { _DataExecutar = value; }
        }
        public int IndPeriodicidade
        {
            get { return _IndPeriodicidade; }
            set { _IndPeriodicidade = value; }
        }
        public int Intervalo
        {
            get { return _Intervalo; }
            set { _Intervalo = value; }
        }
        public int Dia
        {
            get { return _Dia; }
            set { _Dia = value; }
        }
        public int Mes
        {
            get { return _Mes; }
            set { _Mes = value; }
        }
        public Obrigacao IdObrigacaoBase
        {
            get { return _IdObrigacaoBase; }
            set { _IdObrigacaoBase = value; }
        }
        public Obrigacao IdObrigacaoBasePrimeira
        {
            get { return _IdObrigacaoBasePrimeira; }
            set { _IdObrigacaoBasePrimeira = value; }
        }
        public short IndFeriado
        {
            get { return _IndFeriado; }
            set { _IndFeriado = value; }
        }
        public short IndFeriadoPrimeira
        {
            get { return _IndFeriadoPrimeira; }
            set { _IndFeriadoPrimeira = value; }
        }
        public short IndFeriadoPeriodico
        {
            get { return _IndFeriadoPeriodico; }
            set { _IndFeriadoPeriodico = value; }
        }
        public short IndPeriodoExecutar
        {
            get { return _IndPeriodoExecutar; }
            set { _IndPeriodoExecutar = value; }
        }
        public short IndPeriodoExecutarPrimeira
        {
            get { return _IndPeriodoExecutarPrimeira; }
            set { _IndPeriodoExecutarPrimeira = value; }
        }

        #endregion

        #region OverrideBase

        public override void Validate()
        {
            base.Validate();

            if (this.Id != 0 && this.IdCompromisso.Id != 0)
            {
                if (this.IdPrestador.IdPessoa == null)
                    this.IdPrestador.Find();

                if (this.IdCompromisso.IdPessoa == null)
                    this.IdCompromisso.Find();

                if (this.IdPrestador.IdPessoa.Id != 0
                    && this.IdCompromisso.IdPessoa.Id != this.IdPrestador.IdPessoa.Id)
                {
                    this.IdCompromisso.IdPessoa.Id = this.IdPrestador.IdPessoa.Id;
                    this.IdCompromisso.Save();
                }
            }
        }

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public int Save(bool bVal)
        {
            int ret = base.Save();

            if (bVal)
            {
                try
                {
                    if (this.IsContratada)
                    {
                        if (this.IdObrigacao.mirrorOld == null)
                            this.IdObrigacao.Find();

                        if (this.IdCliente.mirrorOld == null)
                            this.IdCliente.Find();

                        this.AtualizarSugestaoPedidos();
                    }
                    else
                        this.ApagarSugestao();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                }
            }

            return ret;
        }

        public override void Delete()
        {
            base.Delete();
            this.ApagarSugestao();
        }

        #endregion

        #region GetObrigacaoCliente

        public static ObrigacaoCliente GetObrigacaoCliente(Cliente cliente, Obrigacao obrigacao)
        {
            ObrigacaoCliente obrigacaoCliente = new ObrigacaoCliente();

            obrigacaoCliente.Find("IdObrigacao=" + obrigacao.Id + " AND IdCliente=" + cliente.Id);

            if (obrigacaoCliente.Id == 0)
            {
                obrigacaoCliente.Inicialize();
                obrigacaoCliente.IdObrigacao = obrigacao;
                obrigacaoCliente.IdCliente = cliente;
            }

            return obrigacaoCliente;
        }

        #endregion

        #region CriarObrigacaoCliente

        #region CriaObrigacaoCliente()

        public static void CriaObrigacaoCliente()
        {
            string str = "Cliente.IdJuridica IN "
                        + "(SELECT IdCliente FROM qryClienteAtivos) "
                        + "  ORDER BY NomeAbreviado";

            List<Cliente> clientes = new Cliente().Find<Cliente>(str);

            foreach (Cliente cliente in clientes)
            {
                try
                {
                    CriaObrigacaoCliente(cliente);
                }
                catch { }
            }
        }
        #endregion

        #region CriaObrigacaoCliente(Cliente cliente)

        public static void CriaObrigacaoCliente(Cliente cliente)
        {
            CriaObrigacaoCliente(cliente, null);
        }
        #endregion

        #region CriaObrigacaoCliente(Cliente cliente, BackgroundWorker bw)

        public static void CriaObrigacaoCliente(Cliente cliente, System.ComponentModel.BackgroundWorker bw)
        {
            cliente.AtualizaQuantidadeEmpregado();

            new ObrigacaoCliente().Delete("IdCliente=" + cliente.Id 
                + " AND IdObrigacao IN (SELECT IdObrigacao FROM Obrigacao WHERE IsInativo=1)");

            StringBuilder str = new StringBuilder();

            str.Append("IsInativo=0");

            if (cliente.IsTomadora())
            {
                str.Append(" AND IdObrigacao IN ("
                    + (int)Obrigacoes.PPRA + ","
                    + (int)Obrigacoes.PCMSO + ","
                    + (int)Obrigacoes.ExamesPeriodicos + ")");
            }

            List<Obrigacao> obrigacoes = new Obrigacao().Find<Obrigacao>(str.ToString());

            if (bw != null)
                bw.ReportProgress(0, obrigacoes.Count);

            LimpaTodasAsSugestoes(cliente);

            int i = 0;

            foreach (Obrigacao obrigacao in obrigacoes)
            {
                if (bw != null && bw.CancellationPending)
                    return;

                if (bw != null)
                    bw.ReportProgress(i++);

                ObrigacaoCliente obrigacaoCliente = ObrigacaoCliente.GetObrigacaoCliente(cliente, obrigacao);
                obrigacaoCliente.Atualizar();
            }

            if (cliente.AtivarLocalDeTrabalho)
            {
                List<Cliente> listLocais = new Cliente().Find<Cliente>("IdJuridicaPai=" + cliente.Id);

                foreach (Cliente localDeTrabalho in listLocais)
                {
                    try
                    {
                        if (bw != null && bw.CancellationPending)
                            return;

                        //recursivo
                        CriaObrigacaoCliente(localDeTrabalho, bw);
                    }
                    catch { }
                }
            }
        }
        #endregion

        #region CriaObrigacaoCliente(Obrigacao obrigacao, BackgroundWorker bw)

        public static void CriaObrigacaoCliente(Obrigacao obrigacao, System.ComponentModel.BackgroundWorker bw)
        {
            if (!Obrigacao.Atualiza)
                return;

            List<Cliente> list = new Cliente().Find<Cliente>("IdCliente IN (SELECT IdCliente FROM qryClienteAtivos) ORDER BY NomeAbreviado");

            if (bw != null)
                bw.ReportProgress(0, list.Count);
  
            int i = 0;

            foreach (Cliente cliente in list)
            {
                if (bw != null && bw.CancellationPending)
                    return;

                if (cliente.AtivarLocalDeTrabalho)
                {
                    List<Cliente> listLocais = new Cliente().Find<Cliente>("IdJuridicaPai=" + cliente.Id);

                    foreach (Cliente locais in listLocais)
                    {
                        try
                        {
                            if (bw != null && bw.CancellationPending)
                                return;

                            ObrigacaoCliente obrigacaoCliente = ObrigacaoCliente.GetObrigacaoCliente(locais, obrigacao);

                            obrigacaoCliente.Atualizar();
                        }
                        catch { }
                    }
                }

                try
                {
                    ObrigacaoCliente obrigacaoCliente = ObrigacaoCliente.GetObrigacaoCliente(cliente, obrigacao);

                    obrigacaoCliente.Atualizar();
                }
                catch { }

                if (bw != null)
                    bw.ReportProgress(i++);
            }
        }
        #endregion

        #region Atualizar

        public void Atualizar()
        {
            Cliente cliente = this.IdCliente;

            if (cliente.mirrorOld == null)
                cliente.Find();

            Obrigacao obrigacao = this.IdObrigacao;

            if (obrigacao.mirrorOld == null)
                obrigacao.Find();

            if (obrigacao.IsInativo)
            {
                if (this.Id != 0)
                    this.Delete();

                return;
            }

            if (cliente.IsTomadora())
            {
                if (!(obrigacao.Id == (int)Obrigacoes.PPRA
                    || obrigacao.Id == (int)Obrigacoes.PCMSO
                    || obrigacao.Id == (int)Obrigacoes.ExamesPeriodicos)
                    || cliente.IdJuridicaPapel.Id == (int)IndJuridicaPapel.TerceiroAutonomo)
                {

                    if (this.Id != 0)
                        this.Delete();

                    return;
                }
            }
            
            switch (obrigacao.IndIncidencia)
            {
                case (int)Incidencia.SolicitarElaboracao:
                    if (this.Id != 0) this.Delete();
                    break;

                case (int)Incidencia.Todas:
                    this.AtualizarRegras();
                    break;

                case (int)Incidencia.HouverPericulosidade:
                    break;

                case (int)Incidencia.NaoPossuirCipa:
                    if (!cliente.HasCipa() && cliente.ContrataCipa != (int)TipoCipa.NaoContratada)
                        this.AtualizarRegras();
                    else
                    {
                        if (this.Id != 0)
                            this.Delete();
                    }
                    break;

                case (int)Incidencia.PossuirAutoclave:
                    if (cliente.QtdAutoclaves > 0)
                        this.AtualizarRegras();
                    else
                    {
                        if (this.Id != 0)
                            this.Delete();
                    }
                    break;

                case (int)Incidencia.PossuirCaldeira:
                    if (cliente.QtdCaldeiras > 0)
                        this.AtualizarRegras();
                    else
                    {
                        if (this.Id != 0)
                            this.Delete();
                    }
                    break;

                case (int)Incidencia.PossuirCipa:
                    if (cliente.ContrataCipa == (int)TipoCipa.Contratada && cliente.HasCipa())
                        this.AtualizarRegras();
                    else
                    {
                        if (this.Id != 0)
                            this.Delete();
                    }
                    break;

                case (int)Incidencia.PossuirEmpilhadeira:
                    if (cliente.QtdEmpilhadeiras > 0)
                        this.AtualizarRegras();
                    else
                    {
                        if (this.Id != 0)
                            this.Delete();
                    }
                    break;

                case (int)Incidencia.PossuirObraCivil:
                    if (cliente.HasObraCivil)
                        this.AtualizarRegras();
                    else
                    {
                        if (this.Id != 0)
                            this.Delete();
                    }
                    break;

                case (int)Incidencia.PossuirPonteRolante:
                    if (cliente.QtdPontes > 0)
                        this.AtualizarRegras();
                    else
                    {
                        if (this.Id != 0)
                            this.Delete();
                    }
                    break;

                case (int)Incidencia.PossuirSESMT:
                    if (cliente.HasSesmt())
                        this.AtualizarRegras();
                    else
                    {
                        if (this.Id != 0)
                            this.Delete();
                    }
                    break;

                case (int)Incidencia.PossuirVasosPressao:
                    if (cliente.QtdVasoPressao > 0)
                        this.AtualizarRegras();
                    else
                    {
                        if (this.Id != 0)
                            this.Delete();
                    }
                    break;

                case (int)Incidencia.Prensas:
                    if (cliente.QtdPrensas > 0)
                        this.AtualizarRegras();
                    else
                    {
                        if (this.Id != 0)
                            this.Delete();
                    }
                    break;

                case (int)Incidencia.Galvanoplastia:
                    if (cliente.QtdTanques > 0)
                        this.AtualizarRegras();
                    else
                    {
                        if (this.Id != 0)
                            this.Delete();
                    }
                    break;

                case (int)Incidencia.Mineradora:
                    if (cliente.IsMineradora)
                        this.AtualizarRegras();
                    else
                    {
                        if (this.Id != 0)
                            this.Delete();
                    }
                    break;

                case (int)Incidencia.ContrataPCMSO:
                    if (cliente.ContrataPCMSO != (int)TipoPcmsoContratada.NaoContratada)
                    {
                        if (cliente.ContrataPCMSO == (int)TipoPcmsoContratada.SomenteSistema
                            && (this.IdObrigacao.Id == (int)Obrigacoes.PcmsoRetiradaProntuario
                                || this.IdObrigacao.Id == (int)Obrigacoes.ExamesPeriodicos)
                            )
                        {
                            if (this.Id != 0)
                                this.Delete();
                        }
                        else
                            this.AtualizarRegras();
                    }
                    else
                    {
                        if (this.Id != 0)
                            this.Delete();
                    }
                    break;

                case (int)Incidencia.ContrataOrdemServico:
                    if (cliente.ContrataOS == Cliente.ContrataOrdemServico.Simplificada)
                        this.AtualizarRegras();
                    else
                    {
                        if (this.Id != 0)
                            this.Delete();
                    }
                    break;

                case (int)Incidencia.UsarEPI:
                    this.AtualizarRegras();
                    break;

                case (int)Incidencia.AvaliacaoQuantitativa:
                    this.AtualizarRegras();
                    break;

                default:
                    if (this.Id != 0)
                        this.Delete();
                    break;
            }
        }
        #endregion

        #region AtualizarRegras

        private void AtualizarRegras()
        {
            if (this.IdCliente.mirrorOld == null)
                this.IdCliente.Find();

            IRegra regra = null;

            RegraCliente regraCliente = new RegraCliente();
            regraCliente.Find("IdObrigacao=" + this.IdObrigacao.Id + " AND IdCliente=" + this.IdCliente.Id);

            if (regraCliente.Id == 0)
            {
                RegraSindicato regraSindicato = new RegraSindicato();
                regraSindicato.Find("IdObrigacao=" + this.IdObrigacao.Id + " AND IdSindicato=" + this.IdCliente.IdSindicato.Id);
                if (regraSindicato.Id == 0)
                {
                    RegraLei regraLei = new RegraLei();
                    regraLei.Find("IdObrigacao=" + this.IdObrigacao.Id);
                    if (regraLei.Id != 0)
                    {
                        regra = regraLei;
                        this.IndRegraTipo = (short)RegraTipo.Lei;
                    }
                }
                else
                {
                    regra = regraSindicato;
                    this.IndRegraTipo = (short)RegraTipo.Sindicato;
                }
            }
            else
            {
                regra = regraCliente;
                this.IndRegraTipo = (short)RegraTipo.Cliente;
            }
            if (regra != null && regra.MinEmpregado != 0)
            {
                if (this.IdCliente != null && this.IdCliente.QtdEmpregados < regra.MinEmpregado)
                {
                    this.Delete();
                    return;
                }
            }
            if (regra != null)
            {
                this.Intervalo = regra.Intervalo;
                this.Dia = regra.Dia;
                this.DiasExecutar = regra.DiasExecutar;
                this.DiasExecutarPrimeira = regra.DiasExecutarPrimeira;
                this.IdObrigacaoBase = regra.IdObrigacaoBase;
                this.IdObrigacaoBasePrimeira = regra.IdObrigacaoBasePrimeira;
                this.IndFeriado = regra.IndFeriado;
                this.IndFeriadoPrimeira = regra.IndFeriadoPrimeira;
                this.IndFeriadoPeriodico = regra.IndFeriadoPeriodico;
                this.IndTipoPeriodicidade = regra.IndTipoPeriodicidade;
                this.DiasExecutar = regra.DiasExecutar;
                this.IndPeriodicidade = regra.IndPeriodicidade;
                this.IndPeriodoExecutar = regra.IndPeriodoExecutar;
                this.IndPeriodoExecutarPrimeira = regra.IndPeriodoExecutarPrimeira;
                this.Mes = regra.Mes;
                this.MinEmpregado = regra.MinEmpregado;
                //this.Observacao = regra.Observacao;
            }
            this.Save(true);
        }
        #endregion

        #endregion

        #region SusgestaoPedido

        #region AtualizarSugestaoPedidos

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        private void AtualizarSugestaoPedidos()
        {
            if (!this.IsContratada || this.Id == 0)
            {
                this.ApagarSugestao();
                return;
            }
            //Quadros III/IV/V só a partir de 2005
            if (this.IdObrigacao.Id == (int)Obrigacoes.Quadros_III_IV_V
                && DateTime.Today.Year == 2004)
            {
                this.ApagarSugestao();
                return;
            }

            if (this.IdObrigacao.mirrorOld == null)
                this.IdObrigacao.Find();

            if (this.IdCliente.mirrorOld == null)
                this.IdCliente.Find();

            //Só para clientes novos
            if (this.IdCliente.DataCadastro <= new DateTime(2004, 11, 23)
                && (    this.IdObrigacao.Id == (int)Obrigacoes.PastaCIPA
                    ||  this.IdObrigacao.Id == (int)Obrigacoes.TreinamentoSistemaMestra))
            {
                this.ApagarSugestao();
                return;
            }

            //Necessita de PPRA concluído
            if (this.IdObrigacao.Id == (int)Obrigacoes.TreinamentoSistemaMestra)
            {
                Pedido ultimoPedidoPpra = Pedido.GetUltimoPedido((int)Obrigacoes.PPRA, 
                                                                 this.IdCliente.Id);

                if (ultimoPedidoPpra.Id == 0)
                {
                    this.ApagarSugestao();
                    return;
                }
            }

            if (this.IdObrigacao.IndObrigacaoTipo == (int)ObrigacaoTipo.Cipa)
            {
                this.AtualizarSugestaoCipa();
            }
            else
            {               
                if (this.IdObrigacao.Id == (int)Obrigacoes.PCMSO)
                    this.AtualizarSugestaoPcmso();
                else if (this.IdObrigacao.Id == (int)Obrigacoes.OrdemServico)
                    this.AtualizarSugestaoOrdemServico();
                else if (this.IdObrigacao.Id == (int)Obrigacoes.ExamesPeriodicos)
                    this.AtualizarSugestaoExamePeriodico();
                else if (this.IdObrigacao.Id == (int)Obrigacoes.ExamesVencidos)
                    this.AtualizarSugestaoExameVencidos();
                else if (this.IdObrigacao.Id == (int)Obrigacoes.CaldeiraProjeto)
                    this.AtualizarSugestaoProjetoCaldeira();
                else if (this.IdObrigacao.Id == (int)Obrigacoes.VasosProjeto)
                    this.AtualizarSugestaoProjetoVaso();
                else if (this.IdObrigacao.Id == (int)Obrigacoes.CaldeiraInspecao)
                    this.AtualizarSugestaoInspecaoCaldeira();
                else if (this.IdObrigacao.Id == (int)Obrigacoes.VasosInspecao)
                    this.AtualizarSugestaoInspecaoVaso();
                else
                    this.AtualizarSugestaoNaoCipa();
            }
        }

        #endregion

        #region ExamePeriodico

        private void AtualizarSugestaoExamePeriodico()
        {
            this.ApagarSugestao();

            Pcmso pcmso = this.IdCliente.GetUltimoPcmso();

            if (pcmso.Id != 0)
            {
                Pedido ultimoPedido = Pedido.GetUltimoPedido((int)Obrigacoes.ExamesPeriodicos,
                                                             this.IdCliente.Id);
                this.DataUltimo = ultimoPedido.DataConclusao;
                this.DataVencimento = this.IdCliente.GetDataVencimentoPeriodico(ultimoPedido);
                this.IdPedidoPendente = Pedido.GetPedidoPendente((int)Obrigacoes.ExamesPeriodicos,
                                                                 this.IdCliente.Id);
                if (this.IdPedidoPendente.Id == 0
                    && this.IdCliente.IndAtualizacaoPeriodico == Cliente.AtualizacaoPeriodico.Anual)
                    AtualizarSugestaoExamePeriodico(pcmso);
            }

            this.Save();
        }

        private void AtualizarSugestaoExamePeriodico(Pcmso pcmso)
        {
            DateTime dataProxima = pcmso.GetDataProximoPeriodico();

            StringBuilder str = new StringBuilder();
            str.Append("IdPcmso = " + pcmso.Id)
                .Append(" AND IdExameDicionario=" + (int)IndExameClinico.Periodico)
                .Append(" AND Convert(datetime, Convert(varchar, DataProxima, 103), 103) = '"
                        + dataProxima.ToString("yyyy-MM-dd") + "'")
                .Append(" ORDER BY DataVencimento");

            List<ExamePlanejamento> list = new ExamePlanejamento().Find<ExamePlanejamento>(str.ToString());

            if (list.Count == 0 || this.DataVencimento.AddDays(-this.IdObrigacao.Aviso) > DateTime.Today)
                return;

            Pedido pedido = new Pedido();
            pedido.Inicialize();
            pedido.DataSugestao = this.DataVencimento.AddDays(-this.IdObrigacao.Aviso);
            pedido.DataVencimento = this.DataVencimento;
            pedido.DataUltimo = this.DataUltimo;
            pedido.IdCliente.Id = this.IdCliente.Id;
            pedido.IdObrigacao.Id = (int)Obrigacoes.ExamesPeriodicos;
            pedido.Observacao = list.Count.ToString("000") + " exames periódicos.\n";
            pedido.Save();

            foreach (ExamePlanejamento examePlan in list)
            {
                examePlan.IdExameDicionario.Find();

                PedidoEmpregado pedidoEmpregado = new PedidoEmpregado();
                pedidoEmpregado.Inicialize();
                pedidoEmpregado.IdPedido.Id = pedido.Id;
                pedidoEmpregado.IdEmpregado.Id = examePlan.IdEmpregado.Id;
                pedidoEmpregado.DataVencimento = examePlan.DataProxima;
                pedidoEmpregado.Observacao = examePlan.IdExameDicionario.Nome;
                pedidoEmpregado.Save();
            }
        }
        #endregion

        #region ExameVencidos

        private void AtualizarSugestaoExameVencidos()
        {
            this.ApagarSugestao();

            Pcmso pcmso = this.IdCliente.GetUltimoPcmso();

            if (pcmso.Id != 0)
                AtualizarSugestaoExameVencidos(pcmso);
        }

        private void AtualizarSugestaoExameVencidos(Pcmso pcmso)
        {
            DateTime dataSugestao = DateTime.Today;

            StringBuilder str = new StringBuilder();
            str.Append("IdPcmsoPlanejamento IN ");
            str.Append("(SELECT IdPcmsoPlanejamento FROM PcmsoPlanejamento WHERE IdPcmso = " + pcmso.Id + ")");
            str.Append(" AND (DataVencimento IS NULL OR DataVencimento<='" + dataSugestao.ToString("yyyy-MM-dd") + "')");
            str.Append(" ORDER BY DataVencimento");

            List<ExamePlanejamento> list = new ExamePlanejamento().Find<ExamePlanejamento>(str.ToString());

            if (list.Count == 0)
                return;

            Pedido sugestao = new Pedido();
            sugestao.Inicialize();
            sugestao.DataSugestao = dataSugestao;
            sugestao.DataVencimento = ((ExamePlanejamento)list[0]).DataProxima;
            sugestao.IdCliente.Id = this.IdCliente.Id;
            sugestao.IdObrigacao.Id = (int)Obrigacoes.ExamesVencidos;
            sugestao.Observacao = list.Count.ToString() + " exames vencidos.\n";
            sugestao.Save();

            foreach (ExamePlanejamento examePlan in list)
            {
                examePlan.IdExameDicionario.Find();

                PedidoEmpregado pedidoEmpregado = new PedidoEmpregado();
                pedidoEmpregado.Inicialize();
                pedidoEmpregado.IdPedido.Id = sugestao.Id;
                pedidoEmpregado.IdEmpregado.Id = examePlan.IdEmpregado.Id;
                pedidoEmpregado.DataVencimento = examePlan.DataVencimento;
                pedidoEmpregado.Observacao = examePlan.IdExameDicionario.Nome;
                pedidoEmpregado.Save();
            }
        }
        #endregion

        #region VasoCaldeira

        private void AtualizarSugestaoProjetoVaso()
        {
            string where = "IdCliente=" + this.IdCliente.Id
            + " AND IndVasoCaldeira = " + (int)VasoCaldeiraBase.VasoCaldeiraTipo.VasoPressao
            + " AND IsInativo = 0";

            List<VasoCaldeiraBase> list = new VasoCaldeiraBase().Find<VasoCaldeiraBase>(where);

            SugestaoVasoCaldeira(list);

            foreach (VasoCaldeiraBase equipamentoBase in list)
            {
                try
                {
                    AtualizarSugestaoProjetoVasoCaldeira(equipamentoBase);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                }
            }

            this.Save();
        }

        private void AtualizarSugestaoProjetoCaldeira()
        {
            string where = "IdCliente=" + this.IdCliente.Id
                + " AND IndVasoCaldeira = " + (int)VasoCaldeiraBase.VasoCaldeiraTipo.Caldeira
                + " AND IsInativo = 0";

            List<VasoCaldeiraBase> list = new VasoCaldeiraBase().Find<VasoCaldeiraBase>(where);

            SugestaoVasoCaldeira(list);

            foreach (VasoCaldeiraBase equipamentoBase in list)
            {
                try
                {
                    AtualizarSugestaoProjetoVasoCaldeira(equipamentoBase);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                }
            }

            this.Save();
        }

        private void AtualizarSugestaoProjetoVasoCaldeira(VasoCaldeiraBase equipamentoBase)
        {
            Pedido ultimoPedido = Pedido.GetUltimoPedido(this.IdObrigacao.Id, this.IdCliente.Id, equipamentoBase.Id);

            this.DataUltimo = ultimoPedido.DataConclusao;
            this.DataVencimento = this.GetDataSugestao(ultimoPedido);
            this.IdPedidoPendente = Pedido.GetPedidoPendente(this.IdObrigacao.Id, this.IdCliente.Id, equipamentoBase.Id);

            if (this.IdPedidoPendente.Id != 0 || ultimoPedido.DataConclusao == new DateTime())
            {
                if (this.IdPedidoPendente.Id == 0)
                    CriaSugestaoPedido(this.DataVencimento, this.DataVencimento, new DateTime(), equipamentoBase.Id, equipamentoBase.GetNumeroIdentificacao());
                else
                    this.ApagarSugestao();
            }
            else
                this.ApagarSugestao();
        }

        private void AtualizarSugestaoInspecaoVaso()
        {
            string where = "IdCliente=" + this.IdCliente.Id
            + " AND IndVasoCaldeira = " + (int)VasoCaldeiraBase.VasoCaldeiraTipo.VasoPressao
            + " AND IsInativo = 0";

            List<VasoCaldeiraBase> list = new VasoCaldeiraBase().Find<VasoCaldeiraBase>(where);

            SugestaoVasoCaldeira(list);

            foreach (VasoCaldeiraBase equipamentoBase in list)
            {
                try
                {
                    AtualizarSugestaoInspecaoVasoCaldeira(equipamentoBase);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                }
            }

            this.Save();
        }

        private void AtualizarSugestaoInspecaoCaldeira()
        {
            string where = "IdCliente=" + this.IdCliente.Id
            + " AND IndVasoCaldeira = " + (int)VasoCaldeiraBase.VasoCaldeiraTipo.Caldeira
            + " AND IsInativo = 0";

            List<VasoCaldeiraBase> list = new VasoCaldeiraBase().Find<VasoCaldeiraBase>(where);

            SugestaoVasoCaldeira(list);

            foreach (VasoCaldeiraBase equipamentoBase in list)
            {
                try
                {
                    AtualizarSugestaoInspecaoVasoCaldeira(equipamentoBase);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                }
            }

            this.Save();
        }

        private void AtualizarSugestaoInspecaoVasoCaldeira(VasoCaldeiraBase equipamentoBase)
        {
            Pedido ultimoPedido = Pedido.GetUltimoPedido(this.IdObrigacao.Id, this.IdCliente.Id, equipamentoBase.Id);

            this.DataUltimo = ultimoPedido.DataConclusao;
            this.DataVencimento = this.GetDataSugestao(ultimoPedido);
            this.IdPedidoPendente = Pedido.GetPedidoPendente(this.IdObrigacao.Id, this.IdCliente.Id, equipamentoBase.Id);

            if (this.IdPedidoPendente.Id != 0 || ultimoPedido.DataConclusao == new DateTime())
            {
                if (this.IdPedidoPendente.Id == 0)
                    CriaSugestaoPedido(this.DataVencimento, this.DataVencimento, new DateTime(), equipamentoBase.Id, equipamentoBase.GetNumeroIdentificacao());
                else
                    this.ApagarSugestao();
            }
            else
            {
                InspecaoVasoCaldeira inspecao = new InspecaoVasoCaldeira();
                inspecao.Find("IdVasoCaldeiraBase=" + equipamentoBase.Id);

                if (inspecao.ProximoExameExterno.AddDays(-this.IdObrigacao.Aviso) <= DateTime.Today)
                {
                    this.DataVencimento = inspecao.ProximoExameExterno;

                    CriaSugestaoPedido(this.DataVencimento.AddDays(-this.IdObrigacao.Aviso), this.DataVencimento, inspecao.DataLevantamento, equipamentoBase.Id, equipamentoBase.GetNumeroIdentificacao());
                }
                else if (inspecao.ProximoExameInterno.AddDays(-this.IdObrigacao.Aviso) <= DateTime.Today)
                {
                    this.DataVencimento = inspecao.ProximoExameInterno;

                    CriaSugestaoPedido(this.DataVencimento.AddDays(-this.IdObrigacao.Aviso), this.DataVencimento, inspecao.DataLevantamento, equipamentoBase.Id, equipamentoBase.GetNumeroIdentificacao());
                }
                else if (inspecao.ProximoTesteHidrostatico.AddDays(-this.IdObrigacao.Aviso) <= DateTime.Today)
                {
                    this.DataVencimento = inspecao.ProximoTesteHidrostatico;

                    CriaSugestaoPedido(this.DataVencimento.AddDays(-this.IdObrigacao.Aviso), this.DataVencimento, inspecao.DataLevantamento, equipamentoBase.Id, equipamentoBase.GetNumeroIdentificacao());
                }
                else
                    this.ApagarSugestao();
            }
        }

        private void SugestaoVasoCaldeira(List<VasoCaldeiraBase> list)
        {
            if (list.Count == 0)
            {
                Pedido ultimoPedido = Pedido.GetUltimoPedido(this.IdObrigacao.Id, this.IdCliente.Id);

                this.DataUltimo = ultimoPedido.DataConclusao;
                this.DataVencimento = this.GetDataSugestao(ultimoPedido);
                this.IdObrigacao.SugestaoParaPendente = false;

                CriaSugestaoPedido(this.DataVencimento, this.DataUltimo, new DateTime());
            }
            else
            {
                this.ApagarSugestao();

                Pedido pendente = Pedido.GetPedidoPendente(this.IdObrigacao.Id, this.IdCliente.Id);
                if (pendente.Id != 0)
                {
                    pendente.IdEquipamentoBase.Id = ((VasoCaldeiraBase)list[0]).Id;
                    pendente.Save();
                }

                this.IdObrigacao.SugestaoParaPendente = true;
            }
        }

        #endregion

        #region Pcmso

        private void AtualizarSugestaoPcmso()
        {
            Pedido ultimoPedido = Pedido.GetUltimoPedido(this.IdObrigacao, this.IdCliente);

            this.DataUltimo = ultimoPedido.DataConclusao;
            this.DataVencimento = this.GetDataSugestao(ultimoPedido);

            LaudoTecnico laudo = LaudoTecnico.GetUltimoLaudo(this.IdCliente.Id);

            LaudoTecnico laudoPendente = LaudoTecnico.GetUltimoLaudo(this.IdCliente.Id, false);

            Pcmso pcmso = this.IdCliente.GetUltimoPcmso();

            if (pcmso.Id == 0)
                pcmso.Inicialize();

            if (laudo.Id != 0 && laudo.Id != laudoPendente.Id && pcmso.IdLaudoTecnico.Id == laudoPendente.Id)
                this.ApagarSugestao();
            else if (!laudo.bAE && laudo.Id != 0 && pcmso.IdLaudoTecnico.Id != laudo.Id)
            {
                //verificar se tem pedido pendente
                this.IdPedidoPendente = Pedido.GetPedidoPendente(this.IdObrigacao.Id, this.IdCliente.Id);

                if (this.IdPedidoPendente.Id == 0)
                    CriaSugestaoPedido(laudo.hDT_LAUDO, ultimoPedido.DataConclusao, this.DataVencimento);
                else
                    this.ApagarSugestao();
            }
            else
                this.ApagarSugestao();

            this.Save();
        }

        #endregion

        #region Ordens de Serviço

        private void AtualizarSugestaoOrdemServico()
        {
            Pedido ultimoPedido = Pedido.GetUltimoPedido(this.IdObrigacao, this.IdCliente);

            this.DataUltimo = ultimoPedido.DataConclusao;
            this.DataVencimento = this.GetDataSugestao(ultimoPedido);

            LaudoTecnico laudo = LaudoTecnico.GetUltimoLaudo(this.IdCliente.Id);

            LaudoTecnico laudoPendente = LaudoTecnico.GetUltimoLaudo(this.IdCliente.Id, false);

            Documento documento = this.IdCliente.GetUltimoDocumento(Documentos.OrdemServicoNr1_7);

            OrdemServicoNR1_7 ordemServico = new OrdemServicoNR1_7();
            ordemServico.Find(documento.Id);

            if (ordemServico.Id == 0)
                ordemServico.Inicialize();

            if (laudo.Id != 0
                && laudo.Id != laudoPendente.Id
                && ordemServico.IdLaudoTecnico.Id == laudoPendente.Id)
            {
                this.ApagarSugestao();
            }
            else if (!laudo.bAE 
                    && laudo.Id != 0 
                    && ordemServico.IdLaudoTecnico.Id != laudo.Id)
            {
                //verificar se tem pedido pendente
                this.IdPedidoPendente = Pedido.GetPedidoPendente(this.IdObrigacao.Id, this.IdCliente.Id);

                if (this.IdPedidoPendente.Id == 0)
                    CriaSugestaoPedido(laudo.hDT_LAUDO, ultimoPedido.DataConclusao, this.DataVencimento);
                else
                    this.ApagarSugestao();
            }
            else
                this.ApagarSugestao();

            this.Save();
        }

        #endregion

        #region Cipa

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        private void AtualizarSugestaoCipa()
        {
            Pedido ultimoPedido = Pedido.GetUltimoPedido(this.IdObrigacao, this.IdCliente);

            if (ultimoPedido.Id != 0)
                this.DataUltimo = ultimoPedido.DataConclusao;
            else
                this.DataUltimo = new DateTime();

            Cipa cipa = this.GetCipa();

            DateTime dtSugestao = new DateTime();

            //Verifica se já tem o evento Cipa
            EventoCipa eventoCipa = new EventoCipa();
            eventoCipa.Find("IdCipa=" + cipa.Id + " AND " + "IdEventoBaseCipa=" + this.IdObrigacao.IdEventoBaseCipa.Id);

            if (eventoCipa.Id == 0)
            {
                dtSugestao = cipa.GetDataEventoCipa(this.IdObrigacao.IdEventoBaseCipa.Id);
            }
            else
            {
                if (eventoCipa.DataSolicitacao == new DateTime() && eventoCipa.DataConclusao == new DateTime())
                    dtSugestao = cipa.GetDataEventoCipa(this.IdObrigacao.IdEventoBaseCipa.Id);
            }

            this.IdPedidoPendente = ultimoPedido;
            this.DataVencimento = dtSugestao;

            /******************************************************/
            /*                     SUGESTÃO                       */
            /******************************************************/
            if (dtSugestao != new DateTime() && dtSugestao.AddDays(-this.IdObrigacao.Aviso) <= DateTime.Today)
                CriaSugestaoCipa(cipa, dtSugestao);
            else
            {
                if (eventoCipa.DataSolicitacao == new DateTime() && eventoCipa.DataConclusao == new DateTime())
                    this.ApagarSugestao();
            }

            this.Save();
        }

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        private void CriaSugestaoCipa(Cipa cipa, DateTime dataSugestao)
        {
            Pedido pedido = Pedido.GetSugestao(this.IdObrigacao.Id, this.IdCliente.Id);

            switch (this.IdObrigacao.IdEventoBaseCipa.Id)
            {
                case (int)EventoBase.Eleicao:
                    EleicaoCipa eleicao = new EleicaoCipa();
                    eleicao.Find(pedido.Id);

                    if (eleicao.Id == 0)
                        eleicao.Inicialize();

                    eleicao.IdObrigacao = this.IdObrigacao;
                    eleicao.IdCliente = this.IdCliente;
                    eleicao.IdEventoBaseCipa = this.IdObrigacao.IdEventoBaseCipa;
                    eleicao.IdCipa = cipa;
                    eleicao.DataSugestao = new DateTime();
                    eleicao.DataVencimento = dataSugestao;
                    eleicao.DataSolicitacao = dataSugestao.AddDays(-this.IdObrigacao.Aviso);
                    eleicao.DataConclusao = new DateTime();
                    eleicao.DataCancelamento = new DateTime();
                    eleicao.DataPagamento = new DateTime();
                    eleicao.Save(true);

                    ControlePedido.GerarDatario(eleicao);
                    break;

                case (int)EventoBase.Calendario:
                case (int)EventoBase.CedulaVotacao:
                case (int)EventoBase.ComissaoEleitoral:
                case (int)EventoBase.ComunicacaoSindicato:
                case (int)EventoBase.Edital:
                case (int)EventoBase.InicioInscricao:
                case (int)EventoBase.ListaDosCandidatos:
                case (int)EventoBase.Posse:
                case (int)EventoBase.Publicacao:
                case (int)EventoBase.RegistroDRT:
                case (int)EventoBase.TerminoInscricao:
                    EventoCipa eventoCipa = new EventoCipa();
                    eventoCipa.Find(pedido.Id);

                    if (eventoCipa.Id == 0)
                        eventoCipa.Inicialize();

                    eventoCipa.IdObrigacao = this.IdObrigacao;
                    eventoCipa.IdCliente = this.IdCliente;
                    eventoCipa.IdEventoBaseCipa = this.IdObrigacao.IdEventoBaseCipa;
                    eventoCipa.IdCipa = cipa;
                    eventoCipa.DataSugestao = new DateTime();
                    eventoCipa.DataVencimento = dataSugestao;
                    eventoCipa.DataSolicitacao = dataSugestao.AddDays(-this.IdObrigacao.Aviso);
                    eventoCipa.DataConclusao = new DateTime();
                    eventoCipa.DataCancelamento = new DateTime();
                    eventoCipa.DataPagamento = new DateTime();
                    eventoCipa.Save(true);

                    ControlePedido.GerarDatario(eventoCipa);
                    break;

                case (int)EventoBase.Reuniao1:
                case (int)EventoBase.Reuniao2:
                case (int)EventoBase.Reuniao3:
                case (int)EventoBase.Reuniao4:
                case (int)EventoBase.Reuniao5:
                case (int)EventoBase.Reuniao6:
                case (int)EventoBase.Reuniao7:
                case (int)EventoBase.Reuniao8:
                case (int)EventoBase.Reuniao9:
                case (int)EventoBase.Reuniao10:
                case (int)EventoBase.Reuniao11:
                case (int)EventoBase.Reuniao12:
                case (int)EventoBase.ReuniaoExtraordinaria:
                    ReuniaoCipa reunicao = new ReuniaoCipa();
                    reunicao.Find(pedido.Id);

                    if (reunicao.Id == 0)
                        reunicao.Inicialize();

                    reunicao.IdObrigacao = this.IdObrigacao;
                    reunicao.IdCliente = this.IdCliente;
                    reunicao.IdEventoBaseCipa = this.IdObrigacao.IdEventoBaseCipa;
                    reunicao.IdCipa = cipa;
                    reunicao.DataSugestao = new DateTime();
                    reunicao.DataVencimento = dataSugestao;
                    reunicao.DataSolicitacao = dataSugestao.AddDays(-this.IdObrigacao.Aviso);
                    reunicao.DataConclusao = new DateTime();
                    reunicao.DataCancelamento = new DateTime();
                    reunicao.DataPagamento = new DateTime();

                    reunicao.Save(true);

                    ControlePedido.GerarDatario(reunicao);
                    break;
            }
        }

        private Cipa GetCipa()
        {
            Cipa cipa;

            switch (this.IdObrigacao.IdEventoBaseCipa.Id)
            {
                case (int)EventoBase.Eleicao:
                case (int)EventoBase.CedulaVotacao:
                case (int)EventoBase.ComissaoEleitoral:
                case (int)EventoBase.ComunicacaoSindicato:
                case (int)EventoBase.Edital:
                case (int)EventoBase.InicioInscricao:
                case (int)EventoBase.ListaDosCandidatos:
                case (int)EventoBase.Posse:
                case (int)EventoBase.Publicacao:
                case (int)EventoBase.TerminoInscricao:
                case (int)EventoBase.Calendario:
                    cipa = this.IdCliente.GetProximaGestao();
                    break;
                case (int)EventoBase.RegistroDRT:
                case (int)EventoBase.Reuniao1:
                case (int)EventoBase.Reuniao2:
                case (int)EventoBase.Reuniao3:
                case (int)EventoBase.Reuniao4:
                case (int)EventoBase.Reuniao5:
                case (int)EventoBase.Reuniao6:
                case (int)EventoBase.Reuniao7:
                case (int)EventoBase.Reuniao8:
                case (int)EventoBase.Reuniao9:
                case (int)EventoBase.Reuniao10:
                case (int)EventoBase.Reuniao11:
                case (int)EventoBase.Reuniao12:
                    cipa = this.IdCliente.GetGestaoAtual();
                    break;
                default:
                    cipa = this.IdCliente.GetGestaoAtual();
                    break;
            }

            return cipa;
        }

        #endregion

        #region NaoCipa

        private void AtualizarSugestaoNaoCipa()
        {
            ////////////////////////////////////////////////////
            ///               ÚLTIMO PEDIDO
            ///////////////////////////////////////////////////
            Pedido ultimoPedido = Pedido.GetUltimoPedido(this.IdObrigacao, this.IdCliente);

            if (ultimoPedido.Id != 0)
                this.DataUltimo = ultimoPedido.DataConclusao;
            else
                this.DataUltimo = new DateTime();

            ////////////////////////////////////////////////////
            ///               PEDIDO PENDENTE
            ///////////////////////////////////////////////////
            Pedido pedidoPendente = Pedido.GetPedidoPendente(this.IdObrigacao.Id, this.IdCliente.Id);

            if (pedidoPendente.Id != 0)
                this.IdPedidoPendente = pedidoPendente;
            else
                this.IdPedidoPendente.Id = 0;

            ////////////////////////////////////////////////////
            ///               VENCIMENTO
            ///////////////////////////////////////////////////
            DateTime dtSugestao = this.GetDataSugestao(ultimoPedido);
            this.DataVencimento = dtSugestao;

            ////////////////////////////////////////////////////
            ///           CRIAR SUGESTÃO PEDIDO
            ///////////////////////////////////////////////////

            if (this.IdPedidoPendente.Id == 0
                && dtSugestao != new DateTime()
                && dtSugestao.AddDays(1) > this.IdCliente.DataCadastro//Corrigido bug da hora
                && dtSugestao.AddDays(-this.IdObrigacao.Aviso) <= DateTime.Today)
            {
                this.IdPedidoPendente = CriaSugestaoPedido(dtSugestao,
                                                            ultimoPedido.DataConclusao,
                                                            dtSugestao);
            }
            else
            {
                if (ultimoPedido.Id != 0 && ultimoPedido.DataVencimento != dtSugestao)
                {
                    ultimoPedido.DataVencimento = dtSugestao;
                    ultimoPedido.Save();
                }

                this.ApagarSugestao();
            }

            this.Save();
        }

        #endregion

        #region DataSugestao

        private DateTime GetDataSugestao(Pedido ultimoPedido)
        {
            DateTime dataCadastro = new DateTime(this.IdCliente.DataCadastro.Year,
                                                this.IdCliente.DataCadastro.Month,
                                                this.IdCliente.DataCadastro.Day);

            DateTime dtSugestao = new DateTime();

            if (this.IndTipoPeriodicidade == (int)IndTipoPeriodicidades.NaoRealizar)
            {
                dtSugestao = new DateTime();
            }
            else if (this.IndTipoPeriodicidade == (int)IndTipoPeriodicidades.ApenasUmaVez)
            {
                if (this.DataUltimo == new DateTime())
                {
                    if (this.DataExecutar != new DateTime())
                        dtSugestao = this.DataExecutar;
                    else
                        dtSugestao = dataCadastro;
                }
            }
            else if (this.IndTipoPeriodicidade == (int)IndTipoPeriodicidades.Periodico)
            {
                dtSugestao = new DateTime(ultimoPedido.DataConclusao.Year,
                                            ultimoPedido.DataConclusao.Month,
                                            ultimoPedido.DataConclusao.Day);

                if (dtSugestao == new DateTime())
                {
                    dtSugestao = dataCadastro;
                    dtSugestao = DiaMesData(dtSugestao, this.Dia, this.Mes);
                }
                else
                {
                    dtSugestao = AddData(dtSugestao, this.Intervalo, this.IndPeriodicidade);
                    dtSugestao = DiaMesData(dtSugestao, this.Dia, this.Mes);
                }

                dtSugestao = Feriado.AjustaData(dtSugestao, this.IdCliente.GetMunicipio(), this.IndFeriadoPeriodico);
            }
            else if (this.IndTipoPeriodicidade == (int)IndTipoPeriodicidades.EventoBase)
            {
                if (this.IdObrigacaoBase != null)
                    this.IdObrigacaoBase.Find();

                DateTime dataUltima = this.GetCipa().GetDataEventoCipa(this.IdObrigacaoBase.IdEventoBaseCipa.Id);

                if (dataUltima == new DateTime() && this.IndPeriodoExecutarPrimeira != (int)Periodicidade.Nenhuma)
                {
                    dtSugestao = AddData(DateTime.Today, this.DiasExecutarPrimeira, this.IndPeriodoExecutarPrimeira);
                    dtSugestao = Feriado.AjustaData(dtSugestao, this.IdCliente.GetMunicipio(), this.IndFeriadoPrimeira);
                }
                else if (dataUltima != new DateTime() && this.DiasExecutar != (int)Periodicidade.Nenhuma)
                {
                    dtSugestao = AddData(dataUltima, this.DiasExecutar, this.IndPeriodoExecutar);
                    dtSugestao = Feriado.AjustaData(dtSugestao, this.IdCliente.GetMunicipio(), this.IndFeriado);

                    if (this.IdObrigacaoBase.Id == (int)Obrigacoes.Posse)
                    {
                        DateTime dataUltimaBase = this.IdCliente.GetDataPosse();

                        if (dataUltimaBase != new DateTime() && dtSugestao > dataUltimaBase)
                            dtSugestao = dataUltimaBase;
                    }
                }
            }

            return dtSugestao;
        }

        #endregion

        #region CriarSugestaoPedido

        private Pedido CriaSugestaoPedido(DateTime dataSugestao, DateTime dataUltimo, DateTime dataVencimento)
        {
            return CriaSugestaoPedido(dataSugestao, dataVencimento, dataUltimo, 0, string.Empty);
        }

        private Pedido CriaSugestaoPedido(DateTime dataSugestao,
                                            DateTime dataVencimento,
                                            DateTime dataUltimo,
                                            int IdEquipamentoBase,
                                            string strObservacao)
        {
            int Aviso = this.IdObrigacao.Aviso;

            Prestador prestador = (this.IdPrestador.Id != 0 ? this.IdPrestador : this.IdObrigacao.IdPrestador);

            Pedido pedido = new Pedido();
            pedido.Find("DataSolicitacao IS NULL AND DataConclusao IS NULL AND DataCancelamento IS NULL AND IdObrigacao=" + this.IdObrigacao.Id + " AND IdCliente=" + this.IdCliente.Id);

            if (pedido.Id == 0)
                pedido.Inicialize();

            //Para criar o numero do Pedido
            if (this.IdObrigacao.SugestaoParaPendente
                || this.IdCliente.IdJuridicaPapel.Id == (int)IndJuridicaPapel.Tomadora
                || this.IdCliente.IdJuridicaPapel.Id == (int)IndJuridicaPapel.TerceiroAutonomo)
            {
                if (dataUltimo == new DateTime())
                    pedido.DataSolicitacao = dataSugestao;
                else
                    pedido.DataSolicitacao = dataSugestao.AddDays(-Aviso);

                if (pedido.IdPedidoGrupo.Id == 0)
                {
                    PedidoGrupo pedidoGrupo = new PedidoGrupo();
                    pedidoGrupo.Inicialize();
                    pedidoGrupo.IdCliente.Id = this.IdCliente.Id;
                    pedidoGrupo.IdPrestador.Id = prestador.Id;
                    pedidoGrupo.DataSolicitacao = pedido.DataSolicitacao;
                    pedidoGrupo.Numero = PedidoGrupo.GetNumeroPedido();
                    pedido.IdPedidoGrupo.Id = pedidoGrupo.Save();
                }
            }

            pedido.IdCliente.Id = this.IdCliente.Id;
            pedido.IdObrigacao.Id = this.IdObrigacao.Id;
            pedido.IdPrestador.Id = prestador.Id;
            pedido.IdEquipamentoBase.Id = IdEquipamentoBase;
            pedido.DataSugestao = dataUltimo == new DateTime() ? dataSugestao : dataSugestao.AddDays(-Aviso);
            pedido.DataVencimento = dataVencimento;
            pedido.DataUltimo = dataUltimo;
            pedido.DataConclusao = new DateTime();
            pedido.DataCancelamento = new DateTime();
            pedido.DataPagamento = new DateTime();
            pedido.Observacao = strObservacao;
            pedido.Save();

            if (pedido.IdPedidoGrupo.Id != 0)
                ControlePedido.GerarDatario(pedido);

            return pedido;
        }

        private void ApagarSugestao()
        {
            new Pedido().Delete("DataSolicitacao IS NULL"
                                + " AND DataConclusao IS NULL"
                                + " AND DataCancelamento IS NULL"
                                + " AND IdObrigacao = " + this.IdObrigacao.Id
                                + " AND IdCliente = " + this.IdCliente.Id);
        }

        #endregion

        #region Metodos Staticos

        private static void LimpaTodasAsSugestoes(Cliente cliente)
        {
            StringBuilder str = new StringBuilder();
            str.Append("DataSolicitacao IS NULL");
            str.Append(" AND DataConclusao IS NULL ");
            str.Append(" AND DataCancelamento IS NULL ");
            str.Append(" AND IdCliente=" + cliente.Id);
            str.Append(" AND Pedido.IdPedido NOT IN (SELECT IdPedido FROM Documento) ");
            str.Append(" AND Pedido.IdObrigacao NOT IN (SELECT IdObrigacao FROM Obrigacao WHERE IndObrigacaoTipo=1) ");
            new Pedido().Delete(str.ToString());
        }

        public static DateTime DiaMesData(DateTime data, int dia, int mes)
        {
            if (dia == 0 && mes == 0)
                return data;
            else if (dia != 0 && mes == 0)
                return new DateTime(data.Year, data.Month, dia);
            else if (dia == 0 && mes != 0)
                return new DateTime(data.Year, mes, data.Day);
            else if (dia != 0 && mes != 0)
                return new DateTime(data.Year, mes, dia);
            else
                return data;
        }

        public static DateTime AddData(DateTime data, int qtd, int period)
        {
            switch (period)
            {
                case (int)Periodicidade.Dia:
                    return data.AddDays(qtd);
                case (int)Periodicidade.Semana:
                    return data.AddDays(qtd * 7);
                case (int)Periodicidade.Mes:
                    return data.AddMonths(qtd);
                case (int)Periodicidade.Semestral:
                    return data.AddMonths(qtd * 6);
                case (int)Periodicidade.Ano:
                    return data.AddYears(qtd);
                default:
                    return data;
            }
        }

        #endregion

        #endregion
    }
}
