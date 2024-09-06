using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;

using Ilitera.Data;
using Ilitera.Common;

namespace Ilitera.Opsa.Data
{
    #region class EventoCipa

    [Database("opsa", "EventoCipa", "IdEventoCipa")]

    

    public class EventoCipa : Ilitera.Opsa.Data.Pedido
    {
        #region Properties

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]
        public EventoCipa()
        {

        }
        private int _IdEventoCipa;
        private Cipa _IdCipa;
        private EventoBaseCipa _IdEventoBaseCipa;
        private string _HoraInicio = "09:00";
        private string _HoraFim = string.Empty;
        private string _Local = string.Empty;
        private string _FraseAdicional = string.Empty;

        private FrasePadrao frasePadrao;
        

        public override int Id
        {
            get { return _IdEventoCipa; }
            set { _IdEventoCipa = value; }
        }
        public Cipa IdCipa
        {
            get { return _IdCipa; }
            set { _IdCipa = value; }
        }
        public EventoBaseCipa IdEventoBaseCipa
        {
            get { return _IdEventoBaseCipa; }
            set { _IdEventoBaseCipa = value; }
        }
        public string HoraInicio
        {
            get { return _HoraInicio; }
            set
            {
                if (value.Length > 5)
                    throw new Exception("Valor indevido!");
                _HoraInicio = value;
            }
        }
        public string HoraFim
        {
            get { return _HoraFim; }
            set
            {
                if (value.Length > 5)
                    throw new Exception("Valor indevido!");

                _HoraFim = value;
            }
        }
        public string Local
        {
            get { return _Local; }
            set { _Local = value; }
        }
        public string FraseAdicional
        {
            get { return _FraseAdicional; }
            set { _FraseAdicional = value; }
        }
        public override string ToString()
        {
            return DataSolicitacao.ToString("dd/MM/yyyy");
        }

        #endregion

        #region Save

        public int Save(bool bVal)
        {
            TransformaProximaCipaEmAtual();

            return base.Save();
        }

        public override int Save()
        {
            Obrigacao obrigacao = new Obrigacao();
            obrigacao.Find("IdEventoBaseCipa=" + this.IdEventoBaseCipa.Id + " AND IndObrigacaoTipo = 1");

            if (this.Id == 0 && this.IdCipa.IdCliente != null)
            {
                this.IdCliente = this.IdCipa.IdCliente;
                this.IdObrigacao = obrigacao;
            }

            if (this.IdCipa.Id != 0)
            {
                this.IdCipa.Find();
                this.IdCipa.SetDataEventoCipa(this.DataSolicitacao, this.IdEventoBaseCipa.Id);

                this.DataVencimento = this.DataSolicitacao;

                TransformaProximaCipaEmAtual();

                this.IdCipa.Save();
            }

            return base.Save();
        }

        private void TransformaProximaCipaEmAtual()
        {
            //Transforma a Proxima CIPA em CIPA atual
            if (this.IdEventoBaseCipa.Id == (int)EventoBase.Posse
                && this.DataConclusao != new DateTime())
            {
                if (this.IdCipa.mirrorOld == null)
                    this.IdCipa.Find();

                if (this.IdCipa.IsProxima)
                {
                    this.IdCipa.IsProxima = false;
                    this.IdCipa.DataLevantamento = this.DataSolicitacao;
                    this.IdCipa.SetAtualizaCliente(true);
                    this.IdCipa.Save();
                }
            }
        }
        #endregion

        #region GetEventoCipa

        public static EventoCipa GetEventoCipa(Cipa cipa, EventoBase eventoBase)
        {
            string criteria = "IdCipa=" + cipa.Id
                            + " AND IdEventoBaseCipa=" + (int)eventoBase;

            DateTime dataSugestao = cipa.GetDataEventoCipa((int)eventoBase);

            if (dataSugestao == new DateTime())
                dataSugestao = DateTime.Today;

            if (cipa.IdCliente.mirrorOld == null)
                cipa.IdCliente.Find();

            if (eventoBase == EventoBase.Eleicao)
            {
                EleicaoCipa eleicaoCipa = new EleicaoCipa();
                eleicaoCipa.Find(criteria);

                if (eleicaoCipa.Id != 0)
                {
                    eleicaoCipa.DataSolicitacao = dataSugestao;
                }
                else
                {
                    eleicaoCipa.Inicialize();
                    eleicaoCipa.IdObrigacao = EventoBaseCipa.GetObrigacao(eventoBase);
                    eleicaoCipa.IdEventoBaseCipa.Id = (int)EventoBase.Eleicao;
                    eleicaoCipa.IdCliente.Id = cipa.IdCliente.Id;
                    eleicaoCipa.IdCipa = cipa;
                    eleicaoCipa.HoraInicio = cipa.IdCliente.HoraAtividades;
                    eleicaoCipa.Local = cipa.IdCliente.LocalAtividades;
                    eleicaoCipa.DataSolicitacao = dataSugestao;
                }

                return eleicaoCipa;
            }
            else if (eventoBase == EventoBase.Reuniao1
                || eventoBase == EventoBase.Reuniao2
                || eventoBase == EventoBase.Reuniao3
                || eventoBase == EventoBase.Reuniao4
                || eventoBase == EventoBase.Reuniao5
                || eventoBase == EventoBase.Reuniao6
                || eventoBase == EventoBase.Reuniao7
                || eventoBase == EventoBase.Reuniao8
                || eventoBase == EventoBase.Reuniao9
                || eventoBase == EventoBase.Reuniao10
                || eventoBase == EventoBase.Reuniao11
                || eventoBase == EventoBase.Reuniao12
                || eventoBase == EventoBase.ReuniaoExtraordinaria)
            {
                ReuniaoCipa reuniaoCipa = new ReuniaoCipa();
                reuniaoCipa.Find(criteria);

                if (reuniaoCipa.Id != 0)
                {
                    reuniaoCipa.DataSolicitacao = dataSugestao;
                }
                else
                {
                    reuniaoCipa.Inicialize();
                    reuniaoCipa.IdObrigacao = EventoBaseCipa.GetObrigacao(eventoBase);
                    reuniaoCipa.IdCliente.Id = cipa.IdCliente.Id;
                    reuniaoCipa.IdCipa = cipa;
                    reuniaoCipa.IdEventoBaseCipa.Id = (int)eventoBase;

                    if (reuniaoCipa.DataSolicitacao == new DateTime())
                        reuniaoCipa.DataSolicitacao = DateTime.Today;
                    reuniaoCipa.HoraInicio = "";
                    reuniaoCipa.FraseAcidentes = "";
                }

                return reuniaoCipa;
            }
            else//Outros
            {
                EventoCipa eventoCipa = new EventoCipa();
                eventoCipa.Find(criteria);

                if (eventoCipa.Id != 0)
                {
                    eventoCipa.DataSolicitacao = dataSugestao;
                }
                else
                {
                    eventoCipa.Inicialize();
                    eventoCipa.IdObrigacao = EventoBaseCipa.GetObrigacao(eventoBase);
                    eventoCipa.IdCliente.Id = cipa.IdCliente.Id;
                    eventoCipa.IdCipa = cipa;
                    eventoCipa.IdEventoBaseCipa.Id = (int)eventoBase;
                    eventoCipa.DataSolicitacao = dataSugestao;
                    eventoCipa.HoraInicio = cipa.IdCliente.HoraAtividades;
                    eventoCipa.Local = cipa.IdCliente.LocalAtividades;
                }
                return eventoCipa;
            }
        }

        #endregion

        #region GetHorario

        public string GetHorario()
        {
            this.Find(this.Id);
            if (this.HoraFim == string.Empty)
                return this.HoraInicio;
            else
                return " das " + this.HoraInicio + " às " + this.HoraFim;
        }
        #endregion

        #region GetFrasePadraoAbertura

        public string GetFrasePadraoAbertura()
        {
            if (frasePadrao == null)
            {
                frasePadrao = new FrasePadrao();
                frasePadrao.Find("IdEventoBaseCipa=" + this.IdEventoBaseCipa.Id);
            }
            return frasePadrao.Abertura;
        }
        #endregion

        #region GetFrasePadraoEncerramento

        public string GetFrasePadraoEncerramento()
        {
            if (frasePadrao == null)
            {
                frasePadrao = new FrasePadrao();
                frasePadrao.Find("IdEventoBaseCipa=" + this.IdEventoBaseCipa.Id);
            }
            return frasePadrao.Encerramento;
        }
        #endregion
    }
    #endregion

    #region class EleicaoCipa

    [Database("opsa", "EleicaoCipa", "IdEleicaoCipa")]
    public class EleicaoCipa : EventoCipa
    {
        #region Properties

        private int _IdEleicaoCipa;
        private int _VotosNulos;
        private int _VotosBrancos;
        private int _TotalDeVotos;
        private int _Efetivo;
        private int _Suplente;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public EleicaoCipa()
        {
            this.IdEventoBaseCipa = new EventoBaseCipa(EventoBase.Eleicao);
        }
        public override int Id
        {
            get { return _IdEleicaoCipa; }
            set { _IdEleicaoCipa = value; }
        }
        public int VotosNulos
        {
            get { return _VotosNulos; }
            set { _VotosNulos = value; }
        }
        public int VotosBrancos
        {
            get { return _VotosBrancos; }
            set { _VotosBrancos = value; }
        }
        public int TotalDeVotos
        {
            get { return _TotalDeVotos; }
            set { _TotalDeVotos = value; }
        }
        public int Efetivo
        {
            get { return _Efetivo; }
            set { _Efetivo = value; }
        }
        public int Suplente
        {
            get { return _Suplente; }
            set { _Suplente = value; }
        }
        #endregion

        #region ApuracaoVotos

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public void ApuracaoVotos(int numEmpregados)
        {
            if (this.IdCipa.IdCliente == null)
                this.IdCipa.Find();

            new MembroCipa().Delete("IdCipa=" + this.IdCipa.Id
                + " AND IndGrupoMembro=" + (int)GrupoMembro.Empregado);

            string criteria = "IdCipa=" + this.IdCipa.Id
                            + " ORDER BY IsVicePresidente DESC, Votos DESC,"
                            + " (SELECT hDT_ADM FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEMPREGADO WHERE "
                            + " nID_EMPREGADO = ParticipantesEleicaoCipa.IdEmpregado)";

            List<ParticipantesEleicaoCipa>
                participantes = new ParticipantesEleicaoCipa().Find<ParticipantesEleicaoCipa>(criteria);

            int i = 0;
            int totalDevotos = 0;

            for (i = 0; i < participantes.Count && i < this.Efetivo; i++)
            {
                totalDevotos += participantes[i].Votos;

                MembroCipa membroCipa = new MembroCipa();
                membroCipa.Inicialize();
                membroCipa.IdEmpregado = participantes[i].IdEmpregado;
                membroCipa.IdCipa = this.IdCipa;
                membroCipa.Numero = i;
                membroCipa.IndTitularSuplente = (short)TitularSuplente.Titular;
                membroCipa.Estabilidade = this.IdCipa.Posse.AddYears(2);
                membroCipa.IndStatus = (int)MembroCipa.Status.Ativo;
                membroCipa.Save();
            }

            int g = 0;

            for (g = 0; ((i + g) < participantes.Count && (i + g) < (this.Efetivo + this.Suplente)); g++)
            {
                totalDevotos += participantes[i + g].Votos;

                MembroCipa membroCipa = new MembroCipa();
                membroCipa.Inicialize();
                membroCipa.IdEmpregado.Id = participantes[i + g].IdEmpregado.Id;
                membroCipa.IdCipa = this.IdCipa;
                membroCipa.Numero = g + 1;
                membroCipa.IndTitularSuplente = (short)TitularSuplente.Suplente;
                membroCipa.Estabilidade = this.IdCipa.Posse.AddYears(2);
                membroCipa.IndStatus = (int)MembroCipa.Status.Ativo;
                membroCipa.Save();
            }

            int h = i + g;

            for (; h < participantes.Count; h++)
                totalDevotos += participantes[h].Votos;

            totalDevotos = totalDevotos + VotosNulos + VotosBrancos;

            this.TotalDeVotos = totalDevotos;

            if (this.TotalDeVotos <= (numEmpregados * 0.5))
                throw new Exception("Total de votos inferior a 50% mais um do total de empregados!");
        }
        #endregion
    }
    #endregion
}
