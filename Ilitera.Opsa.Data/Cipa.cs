using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;

using Ilitera.Data;
using Ilitera.Common;

namespace Ilitera.Opsa.Data
{
    [Database("opsa", "Cipa", "IdCipa")]
    public class Cipa : Ilitera.Opsa.Data.Documento
    {
        #region Properties

        private int _IdCipa;
        private bool _IsProxima = true;
        private DateTime _Edital;
        private DateTime _ComissaoEleitoral;
        private DateTime _ComunicacaoSindicato;
        private DateTime _InicioInscricao;
        private DateTime _CedulaVotacao;
        private DateTime _TerminoInscricao;
        private DateTime _Eleicao;
        private DateTime _Posse;
        private DateTime _Calendario;
        private DateTime _RegistroDRT;
        private DateTime _Publicacao;
        private DateTime _ListaDosCandidatos;
        private DateTime _Reuniao1;
        private DateTime _Reuniao2;
        private DateTime _Reuniao3;
        private DateTime _Reuniao4;
        private DateTime _Reuniao5;
        private DateTime _Reuniao6;
        private DateTime _Reuniao7;
        private DateTime _Reuniao8;
        private DateTime _Reuniao9;
        private DateTime _Reuniao10;
        private DateTime _Reuniao11;
        private DateTime _Reuniao12;
        private DateTime _TerminoMandato;
        private string _ListaEmailsAviso;

        private bool AtualizaCliente;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public Cipa()
        {

        }

        public override int Id
        {
            get { return _IdCipa; }
            set { _IdCipa = value; }
        }
        public bool IsProxima
        {
            get { return _IsProxima; }
            set { _IsProxima = value; }
        }
        public DateTime Edital
        {
            get { return _Edital; }
            set { _Edital = value; }
        }
        public DateTime ComissaoEleitoral
        {
            get { return _ComissaoEleitoral; }
            set { _ComissaoEleitoral = value; }
        }
        public DateTime ComunicacaoSindicato
        {
            get { return _ComunicacaoSindicato; }
            set { _ComunicacaoSindicato = value; }
        }
        public DateTime Publicacao
        {
            get { return _Publicacao; }
            set { _Publicacao = value; }
        }
        public DateTime InicioInscricao
        {
            get { return _InicioInscricao; }
            set { _InicioInscricao = value; }
        }
        public DateTime CedulaVotacao
        {
            get { return _CedulaVotacao; }
            set { _CedulaVotacao = value; }
        }
        public DateTime ListaDosCandidatos
        {
            get { return _ListaDosCandidatos; }
            set { _ListaDosCandidatos = value; }
        }
        public DateTime TerminoInscricao
        {
            get { return _TerminoInscricao; }
            set { _TerminoInscricao = value; }
        }
        public DateTime Eleicao
        {
            get { return _Eleicao; }
            set { _Eleicao = value; }
        }
        public DateTime Posse
        {
            get { return _Posse; }
            set
            {
                this._TerminoMandato = value.AddYears(1);
                _Posse = value;
            }
        }
        public DateTime Calendario
        {
            get { return _Calendario; }
            set { _Calendario = value; }
        }
        public DateTime RegistroDRT
        {
            get { return _RegistroDRT; }
            set { _RegistroDRT = value; }
        }
        public DateTime Reuniao1
        {
            get { return _Reuniao1; }
            set { _Reuniao1 = value; }
        }
        public DateTime Reuniao2
        {
            get { return _Reuniao2; }
            set { _Reuniao2 = value; }
        }
        public DateTime Reuniao3
        {
            get { return _Reuniao3; }
            set { _Reuniao3 = value; }
        }
        public DateTime Reuniao4
        {
            get { return _Reuniao4; }
            set { _Reuniao4 = value; }
        }
        public DateTime Reuniao5
        {
            get { return _Reuniao5; }
            set { _Reuniao5 = value; }
        }
        public DateTime Reuniao6
        {
            get { return _Reuniao6; }
            set { _Reuniao6 = value; }
        }
        public DateTime Reuniao7
        {
            get { return _Reuniao7; }
            set { _Reuniao7 = value; }
        }
        public DateTime Reuniao8
        {
            get { return _Reuniao8; }
            set { _Reuniao8 = value; }
        }
        public DateTime Reuniao9
        {
            get { return _Reuniao9; }
            set { _Reuniao9 = value; }
        }
        public DateTime Reuniao10
        {
            get { return _Reuniao10; }
            set { _Reuniao10 = value; }
        }
        public DateTime Reuniao11
        {
            get { return _Reuniao11; }
            set { _Reuniao11 = value; }
        }
        public DateTime Reuniao12
        {
            get { return _Reuniao12; }
            set
            {
                if (value != new DateTime()
                    && _TerminoMandato != new DateTime()
                    && value > _TerminoMandato)
                    _Reuniao12 = _TerminoMandato;
                else
                    _Reuniao12 = value;
            }
        }
        public string ListaEmailsAviso
        {
            get { return _ListaEmailsAviso; }
            set { _ListaEmailsAviso = value; }
        }
        public DateTime GetTerminoMandato()
        {
            return _TerminoMandato;
        }
        #endregion

        #region Metodos

        #region override ToString
        public override string ToString()
        {
            return this.DataLevantamento.ToString("dd/MM/yyyy");
        }
        #endregion

        #region override Save

        public override int Save()
        {
            this.IdDocumentoBase.Id = (int)Documentos.CIPA;

            if (this.IdPedido.Id == 0)
                throw new Exception("Pedido é campo obrigatório!");

            if (AtualizaCliente)
            {
                this.IdCliente.Find();
                this.IdCliente.PosseCipa = this.DataLevantamento;
                this.IdCliente.Save();

                this.IdPedido.Find();
                this.IdPedido.DataConclusao = this.DataLevantamento;
                this.IdPedido.Save();

                AtualizaCliente = false;
            }
            return base.Save();
        }
        #endregion

        #region SetCalendario

        public void SetCalendario()
        {
            this.SetCalendario(new DateTime(), (int)EventoBase.ComissaoEleitoral);
        }

        public void SetCalendario(int eventoBase)
        {
            this.SetCalendario(new DateTime(), eventoBase);
        }

        public void SetCalendario(DateTime data, int eventoBase)
        {
            Cipa cipa;

            ObrigacaoCliente obrigacaoCliente;

            bool IsPrimeiraCipa;

            if (this.IdCliente.IdJuridicaPai == null)
                this.IdCliente.Find();

            Municipio municipio = this.IdCliente.GetMunicipio();

            if (data != new DateTime())
            {
                SetDataEventoCipa(data, eventoBase);
                eventoBase++;
            }

            cipa = this.IdCliente.GetGestaoAtual();

            IsPrimeiraCipa = (cipa.Id == 0);

            for (int num = 0; num < 2; num++)
            {
                for (int i = eventoBase;
                    eventoBase < (int)EventoBase.ReuniaoExtraordinaria;
                    eventoBase++)
                {
                    obrigacaoCliente = new ObrigacaoCliente();
                    obrigacaoCliente.Find("IdCliente=" + this.IdCliente.Id
                                        + " AND IdObrigacao=" + Obrigacao.GetObrigacaoCipa(eventoBase).Id);

                    if (obrigacaoCliente.Id == 0)
                    {
                        EventoBaseCipa eventoBaseCipa = new EventoBaseCipa();
                        eventoBaseCipa.Find(eventoBase);

                        throw new Exception("Obrigação não criada para o evento base " + eventoBaseCipa.ToString());
                    }

                    try
                    {
                        DateTime dataEventoCipa = GetDataParametroEventoBase(cipa,
                                                                            obrigacaoCliente,
                                                                            municipio,
                                                                            IsPrimeiraCipa,
                                                                            this.IdCliente.DiaAtividades);

                        SetDataEventoCipa(dataEventoCipa, eventoBase);
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Trace.WriteLine(ex.Message);
                    }
                }
                eventoBase = 1;
            }
        }
        #endregion

        #region SetCalendarioEventoBase

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public void SetCalendarioEventoBase()
        {
            EventoCipa eventoCipa;

            for (int i = 1; i <= (int)EventoBase.ReuniaoExtraordinaria; i++)
            {
                eventoCipa = new EventoCipa();
                eventoCipa.Find("IdCipa=" + this.Id
                    + " AND IdEventoBaseCipa=" + i);

                if (eventoCipa.Id != 0
                    && eventoCipa.DataSolicitacao != new DateTime())
                {
                    eventoCipa.DataSolicitacao = GetDataEventoCipa(i);
                    eventoCipa.Save(true);
                }
            }
        }

        public void SetCalendarioRecalculaReuniloes(int eventoBase)
        {
            if (this.IdCliente.IdJuridicaPai == null)
                this.IdCliente.Find();

            Cipa cipa = this.IdCliente.GetGestaoAtual();

            if (cipa.Posse == null)
            {
                throw new Exception("Não é possível recalcular! Não há data de posse");
            }
            else
            {
                SetDataEventoCipa(cipa.Posse, eventoBase);
            }
            SetCalendarioRecalculaReuniloes(cipa.Posse, eventoBase + 1);
            this.Save();
        }

        public void SetCalendarioRecalculaReuniloes(DateTime dataBase, int eventoBase)
        {
            if (eventoBase < 25)
            {
                SetDataEventoCipa(dataBase.AddMonths(1), eventoBase);
                SetCalendarioRecalculaReuniloes(dataBase.AddMonths(1), eventoBase + 1);
            }
            this.Save();
        }
        #endregion

        #region GetDataParametroEventoBase

        public DateTime GetDataParametroEventoBase(Cipa cipa,
                                                    ObrigacaoCliente obrigacaoCliente,
                                                    Municipio municipio,
                                                    bool IsPrimeiraCipa,
                                                    int diaAtividade)
        {
            DateTime data;

            switch (obrigacaoCliente.IndTipoPeriodicidade)
            {
                case (int)IndTipoPeriodicidades.NaoRealizar:
                    data = new DateTime();
                    break;

                case (int)IndTipoPeriodicidades.ApenasUmaVez:
                    data = obrigacaoCliente.DataExecutar;
                    break;

                case (int)IndTipoPeriodicidades.Periodico:
                    if (obrigacaoCliente.IndPeriodicidade == (int)Periodicidade.Nenhuma)
                        data = new DateTime();
                    else
                    {
                        data = Pedido.GetUltimoPedido(obrigacaoCliente.IdObrigacao, obrigacaoCliente.IdCliente).DataConclusao;
                        data = ObrigacaoCliente.AddData(data, obrigacaoCliente.Intervalo, obrigacaoCliente.IndPeriodicidade);
                        data = ObrigacaoCliente.DiaMesData(data, obrigacaoCliente.Dia, obrigacaoCliente.Mes);
                        data = Feriado.AjustaData(data, municipio, obrigacaoCliente.IndFeriadoPeriodico);
                    }
                    break;

                case (int)IndTipoPeriodicidades.EventoBase:

                    if (obrigacaoCliente.IdObrigacaoBase.IdEventoBaseCipa == null)
                        obrigacaoCliente.IdObrigacaoBase.Find();

                    if (IsPrimeiraCipa)
                    {
                        if (obrigacaoCliente.IdObrigacaoBasePrimeira.IdEventoBaseCipa == null)
                            obrigacaoCliente.IdObrigacaoBasePrimeira.Find();

                        if (this.Edital == new DateTime())
                            this.Edital = DateTime.Today;

                        data = this.GetDataEventoCipa(obrigacaoCliente.IdObrigacaoBasePrimeira.IdEventoBaseCipa.Id);
                        data = ObrigacaoCliente.AddData(data, obrigacaoCliente.DiasExecutarPrimeira, obrigacaoCliente.IndPeriodoExecutarPrimeira);
                        data = Feriado.AjustaData(data, municipio, obrigacaoCliente.IndFeriadoPrimeira);
                    }
                    else
                    {
                        if (obrigacaoCliente.IdObrigacaoBase.IdEventoBaseCipa == null)
                            obrigacaoCliente.IdObrigacaoBase.Find();

                        //Pega a data do EventoBase se for Término do Mandato da CIPA Anterior
                        if (obrigacaoCliente.IdObrigacaoBase.IdEventoBaseCipa.Id == (int)EventoBase.TerminoMandato)
                            data = cipa.GetDataEventoCipa(obrigacaoCliente.IdObrigacaoBase.IdEventoBaseCipa.Id);
                        else
                            data = this.GetDataEventoCipa(obrigacaoCliente.IdObrigacaoBase.IdEventoBaseCipa.Id);

                        //A partir dela calcula a próxima data
                        data = ObrigacaoCliente.AddData(data,
                            obrigacaoCliente.DiasExecutar,
                            obrigacaoCliente.IndPeriodoExecutar);

                        if (obrigacaoCliente.IdObrigacao.IdDocumentoBase == null)
                            obrigacaoCliente.IdObrigacao.Find();

                        int IdEventoBaseCipa
                            = obrigacaoCliente.IdObrigacao.IdEventoBaseCipa.Id;

                        if (IdEventoBaseCipa == (int)EventoBase.Reuniao1 ||
                            IdEventoBaseCipa == (int)EventoBase.Reuniao2 ||
                            IdEventoBaseCipa == (int)EventoBase.Reuniao3 ||
                            IdEventoBaseCipa == (int)EventoBase.Reuniao4 ||
                            IdEventoBaseCipa == (int)EventoBase.Reuniao5 ||
                            IdEventoBaseCipa == (int)EventoBase.Reuniao6 ||
                            IdEventoBaseCipa == (int)EventoBase.Reuniao7 ||
                            IdEventoBaseCipa == (int)EventoBase.Reuniao8 ||
                            IdEventoBaseCipa == (int)EventoBase.Reuniao9 ||
                            IdEventoBaseCipa == (int)EventoBase.Reuniao10 ||
                            IdEventoBaseCipa == (int)EventoBase.Reuniao11 ||
                            IdEventoBaseCipa == (int)EventoBase.Reuniao12)
                        {
                            data = ObrigacaoCliente.DiaMesData(data,
                                diaAtividade, 0);
                        }

                        data = Feriado.AjustaData(data,
                                                municipio,
                                                obrigacaoCliente.IndFeriado);
                    }
                    break;
                default:
                    data = new DateTime();
                    break;
            }
            return data;
        }
        #endregion

        #region GetDataEventoCipa

        public DateTime GetDataEventoCipa(EventoBase eventoBase)
        {
            return GetDataEventoCipa((int)eventoBase);
        }

        public DateTime GetDataEventoCipa(int eventoBase)
        {
            DateTime data = new DateTime();
            switch (eventoBase)
            {
                case (int)EventoBase.Eleicao:
                    data = this.Eleicao;
                    break;
                case (int)EventoBase.Edital:
                    data = this.Edital;
                    break;
                case (int)EventoBase.ComunicacaoSindicato:
                    data = this.ComunicacaoSindicato;
                    break;
                case (int)EventoBase.ComissaoEleitoral:
                    data = this.ComissaoEleitoral;
                    break;
                case (int)EventoBase.Publicacao:
                    data = this.Publicacao;
                    break;
                case (int)EventoBase.InicioInscricao:
                    data = this.InicioInscricao;
                    break;
                case (int)EventoBase.TerminoInscricao:
                    data = this.TerminoInscricao;
                    break;
                case (int)EventoBase.CedulaVotacao:
                    data = this.CedulaVotacao;
                    break;
                case (int)EventoBase.Posse:
                    data = this.Posse;
                    break;
                case (int)EventoBase.Calendario:
                    data = this.Calendario;
                    break;
                case (int)EventoBase.RegistroDRT:
                    data = this.RegistroDRT;
                    break;
                case (int)EventoBase.Reuniao1:
                    data = this.Reuniao1;
                    break;
                case (int)EventoBase.Reuniao2:
                    data = this.Reuniao2;
                    break;
                case (int)EventoBase.Reuniao3:
                    data = this.Reuniao3;
                    break;
                case (int)EventoBase.Reuniao4:
                    data = this.Reuniao4;
                    break;
                case (int)EventoBase.Reuniao5:
                    data = this.Reuniao5;
                    break;
                case (int)EventoBase.Reuniao6:
                    data = this.Reuniao6;
                    break;
                case (int)EventoBase.Reuniao7:
                    data = this.Reuniao7;
                    break;
                case (int)EventoBase.Reuniao8:
                    data = this.Reuniao8;
                    break;
                case (int)EventoBase.Reuniao9:
                    data = this.Reuniao9;
                    break;
                case (int)EventoBase.Reuniao10:
                    data = this.Reuniao10;
                    break;
                case (int)EventoBase.Reuniao11:
                    data = this.Reuniao11;
                    break;
                case (int)EventoBase.Reuniao12:
                    data = this.Reuniao12;
                    break;
                case (int)EventoBase.ListaDosCandidatos:
                    data = this.ListaDosCandidatos;
                    break;
                case (int)EventoBase.TerminoMandato:
                    data = this._TerminoMandato;
                    break;
            }
            return data;
        }
        #endregion

        #region SetDataEventoCipa

        public void SetDataEventoCipa(DateTime data, int eventoBase)
        {
            if (GetConcluidoEventoBase(eventoBase))
                return;

            switch (eventoBase)
            {
                case (int)EventoBase.Eleicao:
                    this.Eleicao = data;
                    break;
                case (int)EventoBase.Edital:
                    this.Edital = data;
                    break;
                case (int)EventoBase.ComunicacaoSindicato:
                    this.ComunicacaoSindicato = data;
                    break;
                case (int)EventoBase.ComissaoEleitoral:
                    this.ComissaoEleitoral = data;
                    break;
                case (int)EventoBase.Publicacao:
                    this.Publicacao = data;
                    break;
                case (int)EventoBase.InicioInscricao:
                    this.InicioInscricao = data;
                    break;
                case (int)EventoBase.TerminoInscricao:
                    this.TerminoInscricao = data;
                    break;
                case (int)EventoBase.CedulaVotacao:
                    this.CedulaVotacao = data;
                    break;
                case (int)EventoBase.Posse:
                    this.DataLevantamento = data;
                    this.Posse = data;
                    break;
                case (int)EventoBase.Calendario:
                    this.Calendario = data;
                    break;
                case (int)EventoBase.RegistroDRT:
                    this.RegistroDRT = data;
                    break;
                case (int)EventoBase.Reuniao1:
                    this.Reuniao1 = data;
                    break;
                case (int)EventoBase.Reuniao2:
                    this.Reuniao2 = data;
                    break;
                case (int)EventoBase.Reuniao3:
                    this.Reuniao3 = data;
                    break;
                case (int)EventoBase.Reuniao4:
                    this.Reuniao4 = data;
                    break;
                case (int)EventoBase.Reuniao5:
                    this.Reuniao5 = data;
                    break;
                case (int)EventoBase.Reuniao6:
                    this.Reuniao6 = data;
                    break;
                case (int)EventoBase.Reuniao7:
                    this.Reuniao7 = data;
                    break;
                case (int)EventoBase.Reuniao8:
                    this.Reuniao8 = data;
                    break;
                case (int)EventoBase.Reuniao9:
                    this.Reuniao9 = data;
                    break;
                case (int)EventoBase.Reuniao10:
                    this.Reuniao10 = data;
                    break;
                case (int)EventoBase.Reuniao11:
                    this.Reuniao11 = data;
                    break;
                case (int)EventoBase.Reuniao12:
                    this.Reuniao12 = data;
                    break;
                case (int)EventoBase.ListaDosCandidatos:
                    this.ListaDosCandidatos = data;
                    break;
            }
        }
        #endregion

        #region SetAtualizaCliente
        public void SetAtualizaCliente(bool bVal)
        {
            AtualizaCliente = bVal;
        }
        #endregion

        #region GetConcluidoEventoBase

        public bool GetConcluidoEventoBase(int eventoBase)
        {
            EventoCipa eventoCipa = new EventoCipa();
            eventoCipa.Find("IdCipa=" + this.Id + " AND IdEventoBaseCipa=" + eventoBase);

            return eventoCipa.DataConclusao != new DateTime();
        }
        #endregion

        #region GetColorLayoutConcluido
        public System.Drawing.Color GetColorLayoutConcluido(EventoBase eventoBase)
        {
            System.Drawing.Color color;
            if (GetConcluidoEventoBase((int)eventoBase))
                color = System.Drawing.Color.Gray;
            else
            {
                DateTime data = this.GetDataEventoCipa((int)eventoBase);
                if (((System.TimeSpan)data.Subtract(DateTime.Today)).Days < 0)
                    color = System.Drawing.Color.Red;
                else
                    color = System.Drawing.Color.Black;
            }
            return color;
        }
        #endregion

        #region QtdCandidatos
        public int QtdCandidatos()
        {
            return new MembroCipa().ExecuteCount("IdCipa=" + this.Id);
        }
        #endregion

        #region SetConcluirEventoCipa

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public void SetConcluirEventoCipa(EventoBase eventoBase, bool bVal)
        {
            EventoCipa eventoCipa = new EventoCipa();
            eventoCipa.Find("IdCipa=" + this.Id + " AND IdEventoBaseCipa=" + ((int)eventoBase)).ToString();

            if (eventoCipa.Id == 0)
            {
                eventoCipa.DataSolicitacao = this.GetDataEventoCipa((int)eventoBase);
                eventoCipa.IdCipa = this;
                eventoCipa.IdEventoBaseCipa.Id = (int)eventoBase;
            }
            eventoCipa.DataConclusao = DateTime.Today;
            eventoCipa.Save();
        }
        #endregion

        #region GetHorarioEventoCipa

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public string GetHorarioEventoCipa(int eventoBase)
        {
            EventoCipa eventoCipa = new EventoCipa();
            eventoCipa.Find("IdCipa=" + this.Id + " AND IdEventoBaseCipa=" + eventoBase.ToString());

            return eventoCipa.GetHorario();
        }
        #endregion

        #region AdicionarEventosCipaAposImportacao

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public static void AdicionarEventosCipaAposImportacao()
        {
            List<Cipa> list = new Cipa().FindAll<Cipa>();

            foreach (Cipa cipa in list)
                cipa.AdicionarEventosCipa();
        }
        #endregion

        #region AdicionarEventosCipa

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public void AdicionarEventosCipa()
        {
            for (int i = 1; i < 25; i++)
            {
                try
                {
                    CriarEventoCipa(this, i);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                }
            }
        }
        #endregion

        #region CriarEventoCipa

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        private void CriarEventoCipa(Cipa cipa, int IdEventoBaseCipa)
        {
            EventoCipa eventoCipa = new EventoCipa();
            eventoCipa.Find("IdCipa=" + cipa.Id + " AND IdEventoBaseCipa=" + IdEventoBaseCipa);

            Obrigacao obrigacao = new Obrigacao();
            obrigacao.Find("IdEventoBaseCipa=" + IdEventoBaseCipa);

            DateTime dataSugestao = cipa.GetDataEventoCipa(IdEventoBaseCipa);

            if (cipa.IdCliente.mirrorOld == null)
                cipa.IdCliente.Find();

            if (dataSugestao != new DateTime() && dataSugestao <= DateTime.Today)
            {
                switch (IdEventoBaseCipa)
                {
                    case (int)EventoBase.Eleicao:

                        EleicaoCipa eleicao = new EleicaoCipa();

                        if (eventoCipa.Id != 0)
                            eleicao.Find(eventoCipa.Id);
                        else
                            eleicao.Inicialize();

                        eleicao.IdObrigacao.Id = obrigacao.Id;
                        eleicao.IdCliente.Id = cipa.IdCliente.Id;
                        eleicao.IdEventoBaseCipa.Id = IdEventoBaseCipa;
                        eleicao.IdCipa.Id = cipa.Id;
                        eleicao.Local = cipa.IdCliente.LocalAtividades;
                        eleicao.DataSugestao = new DateTime();
                        eleicao.DataSolicitacao = dataSugestao;
                        eleicao.DataCancelamento = new DateTime();
                        eleicao.DataPagamento = new DateTime();
                        eleicao.DataConclusao = dataSugestao;
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

                        eventoCipa.IdObrigacao.Id = obrigacao.Id;
                        eventoCipa.IdCliente.Id = cipa.IdCliente.Id;
                        eventoCipa.IdEventoBaseCipa.Id = IdEventoBaseCipa;
                        eventoCipa.IdCipa.Id = cipa.Id;
                        eventoCipa.Local = cipa.IdCliente.LocalAtividades;
                        eventoCipa.DataSugestao = new DateTime();
                        eventoCipa.DataSolicitacao = dataSugestao;
                        eventoCipa.DataCancelamento = new DateTime();
                        eventoCipa.DataPagamento = new DateTime();
                        eventoCipa.DataConclusao = dataSugestao;
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
                        ReuniaoCipa reuniao = new ReuniaoCipa();

                        if (eventoCipa.Id != 0)
                            reuniao.Find(eventoCipa.Id);
                        else
                            reuniao.Inicialize();

                        reuniao.IdObrigacao.Id = obrigacao.Id;
                        reuniao.IdCliente.Id = cipa.IdCliente.Id;
                        reuniao.IdEventoBaseCipa.Id = IdEventoBaseCipa;
                        reuniao.IdCipa.Id = cipa.Id;
                        reuniao.Local = cipa.IdCliente.LocalAtividades;
                        reuniao.DataSugestao = new DateTime();
                        reuniao.DataSolicitacao = dataSugestao;
                        reuniao.DataCancelamento = new DateTime();
                        reuniao.DataPagamento = new DateTime();
                        reuniao.DataConclusao = dataSugestao;
                        reuniao.Save(true);
                        ControlePedido.GerarDatario(reuniao);

                        ImportarAtaCipa(cipa, reuniao);
                        break;
                }
            }
        }

        #endregion

        #region FirstUpper

        private static string FirstUpper(string stringUpper)
        {
            string ret = string.Empty;

            try
            {
                ret = stringUpper.Substring(0, 1).ToUpper()
                    + stringUpper.Substring(1, stringUpper.Length - 1);
            }
            catch { }

            return ret;
        }
        #endregion

        #region ImportarAtaCipa

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public static void ImportarAtaCipa(ReuniaoCipa reuniao)
        {
            ImportarAtaCipa(reuniao.IdCipa, reuniao);
        }

        private static void ImportarAtaCipa(Cipa cipa, ReuniaoCipa reuniao)
        {
            if (cipa.mirrorOld == null)
                cipa.Find();

            if (cipa.IdCliente.mirrorOld == null)
                cipa.IdCliente.Find();

            AtaPDT ataPDT = new AtaPDT();
            ataPDT.Find("DATA='" + reuniao.DataSolicitacao.ToString("yyyy-MM-dd") + "'"
                       + " AND REUNIAO=" + reuniao.GetNumeroReuniao()
                       + " AND CODCAD=" + cipa.IdCliente.CodigoAntigo);

            if (ataPDT.Id != 0)
            {
                Frase frase = new Frase();
                frase.Find(Convert.ToInt32(ataPDT.TEXTO_PADR));

                Ata ata = new Ata();
                ata.Find("IdReuniaoCipa=" + reuniao.Id);
                if (ata.Id == 0)
                    ata.Inicialize();
                ata.IdReuniaoCipa.Id = reuniao.Id;
                ata.IdFrase = frase;

                if (ataPDT.TXT_SEGMED == string.Empty)
                    ata.Texto = frase.Texto;
                else
                    ata.Texto = FirstUpper(ataPDT.TXT_SEGMED);

                ata.Save();

                reuniao.FraseAcidentes = FirstUpper(ataPDT.TXT_ACID);
                reuniao.Save();

                AddReunicaoPresenca(cipa, reuniao, ataPDT.PRESID);
                AddReunicaoPresenca(cipa, reuniao, ataPDT.SECRET);
                AddReunicaoPresenca(cipa, reuniao, ataPDT.VICE_PRES);
                AddReunicaoPresenca(cipa, reuniao, ataPDT.P_MEMBRO1);
                AddReunicaoPresenca(cipa, reuniao, ataPDT.P_MEMBRO2);
                AddReunicaoPresenca(cipa, reuniao, ataPDT.P_MEMBRO3);
                AddReunicaoPresenca(cipa, reuniao, ataPDT.P_MEMBRO4);
                AddReunicaoPresenca(cipa, reuniao, ataPDT.P_MEMBRO5);
                AddReunicaoPresenca(cipa, reuniao, ataPDT.E_MEMBRO1);
                AddReunicaoPresenca(cipa, reuniao, ataPDT.E_MEMBRO2);
                AddReunicaoPresenca(cipa, reuniao, ataPDT.E_MEMBRO3);
                AddReunicaoPresenca(cipa, reuniao, ataPDT.E_MEMBRO4);
                AddReunicaoPresenca(cipa, reuniao, ataPDT.E_MEMBRO5);
            }
        }
        #endregion

        #region AddReunicaoPresenca

        private static void AddReunicaoPresenca(Cipa cipa,
                                                ReuniaoCipa reuniao,
                                                string nomeMembro)
        {
            if (nomeMembro == string.Empty)
                return;

            MembroCipa membroCipa = new MembroCipa();
            membroCipa.Find("IdCipa=" + cipa.Id
                            + " AND IndTitularSuplente=" + (int)TitularSuplente.Titular
                            + " AND NomeMembro='" + nomeMembro + "'");

            if (membroCipa.Id == 0)
                membroCipa.Find("IdCipa=" + cipa.Id
                + " AND IndTitularSuplente=" + (int)TitularSuplente.Suplente
                + " AND NomeMembro='" + nomeMembro + "'");

            if (membroCipa.Id == 0)
                membroCipa.Find("IdCipa=" + cipa.Id + " AND NomeMembro='" + nomeMembro + "'");

            if (membroCipa.Id == 0)
            {
                membroCipa.Inicialize();
                membroCipa.IdCipa.Id = cipa.Id;
                membroCipa.NomeMembro = nomeMembro;
                membroCipa.IndGrupoMembro = (int)GrupoMembro.Outros;
                membroCipa.Save();
            }

            ReuniaoPresencaCipa reuniaoPresencaCipa = new ReuniaoPresencaCipa();
            reuniaoPresencaCipa.Find("IdReuniaoCipa=" + reuniao.Id
                                      + " AND IdMembroCipa=" + membroCipa.Id);

            if (reuniaoPresencaCipa.Id == 0)
            {
                reuniaoPresencaCipa.Inicialize();
                reuniaoPresencaCipa.IdReuniaoCipa.Id = reuniao.Id;
                reuniaoPresencaCipa.IdMembroCipa.Id = membroCipa.Id;
                reuniaoPresencaCipa.Save();
            }
        }
        #endregion

        #region GetGestaoProxima

        public Cipa GetGestaoProxima()
        {
            Cipa gestaoProxima = new Cipa();
            ArrayList listCipa = new Cipa().Find("IdCliente=" + this.Id
                + " AND DataLevantamento IS NOT NULL"
                + " ORDER BY DataLevantamento DESC");

            for (int i = 0; i < listCipa.Count; i++)
            {
                if (((Cipa)listCipa[i]).Id == this.Id
                    && i != (listCipa.Count - 1))
                    gestaoProxima = (Cipa)listCipa[i + 1];
            }

            return gestaoProxima;
        }
        #endregion

        #region GetGestaoAnterior

        public Cipa GetGestaoAnterior()
        {
            Cipa gestaoAnterior = new Cipa();
            ArrayList listCipa = new Cipa().Find("IdCliente=" + this.Id
                + " AND DataLevantamento IS NOT NULL"
                + " ORDER BY DataLevantamento DESC");

            for (int i = 0; i < listCipa.Count; i++)
            {
                if (((Cipa)listCipa[i]).Id == this.Id
                    && i != 0)
                    gestaoAnterior = (Cipa)listCipa[i - 1];
            }

            return gestaoAnterior;
        }
        #endregion

        #region Listas

        #region ListaGridIntegrantesCipa

        public DataSet ListaGridIntegrantesCipa(int IndGrupoMembro)
        {
            return ListaGridIntegrantesCipa(IndGrupoMembro, true);
        }

        public DataSet ListaGridIntegrantesCipa(int IndGrupoMembro, bool IsAtivo)
        {
            ArrayList list = new ArrayList();
            DataSet ds = new DataSet();
            DataTable table = new DataTable("Result");
            table.Columns.Add("Id", Type.GetType("System.String"));
            table.Columns.Add("Cargo", Type.GetType("System.String"));
            table.Columns.Add("Nome", Type.GetType("System.String"));
            table.Columns.Add("Estabilidade", Type.GetType("System.String"));
            ds.Tables.Add(table);
            DataRow newRow;

            list = MembroCipa.GetListaIntegrantes(this, IndGrupoMembro, IsAtivo);

            if (IndGrupoMembro == (short)GrupoMembro.Secretario)
                list.AddRange(MembroCipa.GetListaSuplentes(this, IndGrupoMembro));

            for (int i = 0; i < list.Count; i++)
            {
                newRow = ds.Tables[0].NewRow();

                newRow["Id"] = ((MembroCipa)list[i]).Id;
                newRow["Cargo"] = ((MembroCipa)list[i]).GetNomeCargo();
                newRow["Nome"] = ((MembroCipa)list[i]).ToString();

                if (IndGrupoMembro == (int)GrupoMembro.Empregado)
                    newRow["Estabilidade"] = Ilitera.Common.Utility.TratarData(((MembroCipa)list[i]).Estabilidade);

                ds.Tables[0].Rows.Add(newRow);
            }
            return ds;
        }
        #endregion

        #region ListaGridSuplentesCipa

        public DataSet ListaGridSuplentesCipa(int IndGrupoMembro)
        {
            ArrayList list = new ArrayList();
            DataSet ds = new DataSet();
            DataTable table = new DataTable("Result");
            table.Columns.Add("Id", Type.GetType("System.String"));
            table.Columns.Add("Cargo", Type.GetType("System.String"));
            table.Columns.Add("Nome", Type.GetType("System.String"));
            table.Columns.Add("Estabilidade", Type.GetType("System.String"));
            ds.Tables.Add(table);

            DataRow newRow;

            list = MembroCipa.GetListaSuplentes(this, IndGrupoMembro);

            for (int i = 0; i < list.Count; i++)
            {
                newRow = ds.Tables[0].NewRow();
                newRow["Id"] = ((MembroCipa)list[i]).Id;
                newRow["Cargo"] = ((MembroCipa)list[i]).GetNomeCargo();
                newRow["Nome"] = ((MembroCipa)list[i]).ToString();

                if (IndGrupoMembro == (int)GrupoMembro.Empregado)
                    newRow["Estabilidade"] = ((MembroCipa)list[i]).Estabilidade.ToString("dd-MM-yyyy");

                ds.Tables[0].Rows.Add(newRow);
            }
            return ds;
        }
        #endregion

        #region ListaGridDemaisVotados

        public DataSet ListaGridDemaisVotados()
        {
            ArrayList list = new ArrayList();
            DataSet ds = new DataSet();
            DataTable table = new DataTable("Result");
            table.Columns.Add("Id", Type.GetType("System.String"));
            table.Columns.Add("Nome", Type.GetType("System.String"));
            ds.Tables.Add(table);
            DataRow newRow;
            list = new ParticipantesEleicaoCipa().Find("IdCipa=" + this.Id
                    + " AND IdMembro NOT IN (SELECT IdMembro FROM MembroCipa WHERE IdCipa=" + this.Id + " AND IndGrupoMembro=" + (int)GrupoMembro.Empregado + ")"
                    + " ORDER BY Votos");
            for (int i = 0; i < list.Count; i++)
            {
                newRow = ds.Tables[0].NewRow();
                newRow["Id"] = ((ParticipantesEleicaoCipa)list[i]).Id;
                newRow["Nome"] = ((ParticipantesEleicaoCipa)list[i]).ToString();
                ds.Tables[0].Rows.Add(newRow);
            }
            return ds;
        }
        #endregion

        #region ListaGridTitularesSuplentesCipa

        public DataSet ListaGridTitularesSuplentesCipa(int IndGrupoMembro)
        {
            ArrayList list = new ArrayList();
            DataSet ds = new DataSet();
            DataTable table = new DataTable("Result");
            table.Columns.Add("InicioEstabilidade", Type.GetType("System.String"));
            table.Columns.Add("Cargo", Type.GetType("System.String"));
            table.Columns.Add("Nome", Type.GetType("System.String"));
            table.Columns.Add("TerminoEstabilidade", Type.GetType("System.String"));
            ds.Tables.Add(table);
            DataRow newRow;
            list = MembroCipa.GetListaIntegrantes(this, IndGrupoMembro, true);
            list.AddRange(MembroCipa.GetListaSuplentes(this, IndGrupoMembro));

            for (int i = 0; i < list.Count; i++)
            {
                newRow = ds.Tables[0].NewRow();
                newRow["Cargo"] = ((MembroCipa)list[i]).GetNomeCargo();
                newRow["Nome"] = ((MembroCipa)list[i]).ToString();
                newRow["TerminoEstabilidade"] = ((MembroCipa)list[i]).Estabilidade.ToString("dd-MM-yyyy");
                newRow["InicioEstabilidade"] = this.Posse.ToString("dd-MM-yyyy");
                ds.Tables[0].Rows.Add(newRow);
            }
            return ds;
        }
        #endregion

        #endregion

        #endregion
    }



    [Database("opsa", "CipaRepositorio", "IdCipaRepositorio")]
    public class CipaRepositorio : Ilitera.Data.Table
    {
        #region Properties

        private int _IdCipaRepositorio;
        private int _IdCipa;
        private DateTime _DataHora;
        private string _TipoReuniao;
        private string _Descricao;
        private string _Anexo;
        private string _Evento = string.Empty;



        public delegate void EventProgress(int val);
        public delegate void EventProgressIniciar(string nomeGhe, int val);

        //public event EventProgressIniciar ProgressIniciar;
        //public event EventProgress ProgressAtualizar;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public CipaRepositorio()
        {

        }

        public override int Id
        {
            get { return _IdCipaRepositorio; }
            set { _IdCipaRepositorio = value; }
        }
        public int IdCipa
        {
            get { return _IdCipa; }
            set { _IdCipa = value; }
        }
        public DateTime DataHora
        {
            get { return _DataHora; }
            set { _DataHora = value; }
        }
        public string TipoReuniao
        {
            get { return _TipoReuniao; }
            set { _TipoReuniao = value; }
        }
        public string Descricao
        {
            get { return _Descricao; }
            set { _Descricao = value; }
        }
        public string Anexo
        {
            get { return _Anexo; }
            set { _Anexo = value; }
        }
        public string Evento
        {
            get { return _Evento; }
            set { _Evento = value; }
        }

    }
    #endregion

}