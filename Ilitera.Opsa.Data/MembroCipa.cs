using System;
using System.Collections;
using System.Data;
using System.Text;
using System.Data.SqlClient;

using Ilitera.Data;
using Ilitera.Common;

namespace Ilitera.Opsa.Data
{
    #region enum TitularSuplente

    public enum TitularSuplente : int
    {
        NA,
        Titular,
        Suplente,
    }
    #endregion

    #region enum GrupoMembro

    public enum GrupoMembro : int
    {
        Empregado,
        Empregador,
        Secretario,
        Outros
    }
    #endregion

    #region class ParticipantesEleicaoCipa

    [Database("opsa", "ParticipantesEleicaoCipa", "IdParticipantesEleicaoCipa")]
    public class ParticipantesEleicaoCipa : Ilitera.Data.Table
    {
        #region Properties

        private int _IdParticipantesEleicaoCipa;
        private Empregado _IdEmpregado;
        private Cipa _IdCipa;
        private int _Votos;
        private bool _IsVicePresidente;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public ParticipantesEleicaoCipa()
        {

        }
        public override int Id
        {
            get { return _IdParticipantesEleicaoCipa; }
            set { _IdParticipantesEleicaoCipa = value; }
        }
        public Empregado IdEmpregado
        {
            get { return _IdEmpregado; }
            set { _IdEmpregado = value; }
        }
        public Cipa IdCipa
        {
            get { return _IdCipa; }
            set { _IdCipa = value; }
        }
        public int Votos
        {
            get { return _Votos; }
            set { _Votos = value; }
        }
        public bool IsVicePresidente
        {
            get { return _IsVicePresidente; }
            set { _IsVicePresidente = value; }
        }

        public override string ToString()
        {
            return this.IdEmpregado.ToString();
        }
        #endregion

        #region GetNomeCedula

        public string GetNomeCedula(bool ComNumeroRegistro)
        {
            System.Text.StringBuilder str = new StringBuilder();

            str.Append(this.ToString());

            if (ComNumeroRegistro
                && this.IdEmpregado.Id != 0
                && this.IdEmpregado.tCOD_EMPR != string.Empty)
                str.Append(" (" + this.IdEmpregado.tCOD_EMPR + ")");

            if (this.IdEmpregado.Id != 0)
            {
                if (this.IdEmpregado.tNO_APELIDO != string.Empty)
                    str.Append(" - " + this.IdEmpregado.tNO_APELIDO);
                //else
                //    str.Append(" - sem apelido");

                EmpregadoFuncao empregadoFuncao = EmpregadoFuncao.GetEmpregadoFuncao(this.IdEmpregado);

                str.Append(" - " + empregadoFuncao.GetNomeSetor());
            }

            return str.ToString();
        }
        #endregion

        #region FindParticipanteVotadoMax

        public static ArrayList FindParticipanteVotadoMax(int IdCipa)
        {
            return new ParticipantesEleicaoCipa().FindMax("Votos", "IdCipa=" + IdCipa
                                + " AND IdEmpregado NOT IN (SELECT IdEmpregado FROM MembroCipa WHERE IdCipa=" + IdCipa
                                + " AND IndGrupoMembro<>" + (int)GrupoMembro.Secretario + ")");
        }
        #endregion

        #region VerificarIncricaoDeCandidatosReeleitos

        public void VerificarIncricaoDeCandidatosReeleitos()
        {
            if (this.Id != 0)
                return;

            if (this.IdCipa.IdCliente == null)
                this.IdCipa.Find();

            ArrayList list = this.IdCipa.IdCliente.GetUltimas3Cipa();

            StringBuilder str = new StringBuilder();
            str.Append("IdEmpregado=" + this.IdEmpregado.Id);
            str.Append(" AND IndTitularSuplente=" + (int)TitularSuplente.Titular);

            if (list.Count == 1)
                str.Append(" AND IdCipa=" + ((Cipa)list[0]).Id);
            else if (list.Count == 2)
                str.Append(" AND IdCipa IN (" + ((Cipa)list[0]).Id + "," + ((Cipa)list[1]).Id + ")");
            else if (list.Count == 3)
                str.Append(" AND IdCipa IN (" + ((Cipa)list[0]).Id + "," + ((Cipa)list[1]).Id + "," + ((Cipa)list[2]).Id + ")");
            else
                return;

            DataSet ds = new MembroCipa().Get(str.ToString());

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                throw new Exception("O candidato " + this.IdEmpregado.ToString() + " não pode participar dessa gestão, pois já foi reeleito!");
        }
        #endregion

        #region Save

        public override int Save()
        {
            VerificarIncricaoDeCandidatosReeleitos();

            return base.Save();
        }
        #endregion
    }
    #endregion

    #region class MembroCipa

    [Database("opsa", "MembroCipa", "IdMembroCipa")]
    public class MembroCipa : Ilitera.Data.Table
    {
        #region enum Status

        public enum Status : short
        {
            Ativo = 1,
            Afastado,
            Renunciou
        }
        #endregion

        #region Properties

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public MembroCipa()
        {

        }

        private int _IdMembroCipa;
        private Cipa _IdCipa;
        private Empregado _IdEmpregado;
        private string _NomeMembro = string.Empty;
        private int _Numero;
        private short _IndTitularSuplente;
        private DateTime _Estabilidade;
        private short _IndGrupoMembro;
        private int _IndStatus = (int)MembroCipa.Status.Ativo;

        public override int Id
        {
            get { return _IdMembroCipa; }
            set { _IdMembroCipa = value; }
        }
        public Cipa IdCipa
        {
            get { return _IdCipa; }
            set { _IdCipa = value; }
        }
        public Empregado IdEmpregado
        {
            get { return _IdEmpregado; }
            set { _IdEmpregado = value; }
        }
        public string NomeMembro
        {
            get { return _NomeMembro; }
            set { _NomeMembro = value; }
        }
        public int Numero
        {
            get { return _Numero; }
            set { _Numero = value; }
        }
        public DateTime Estabilidade
        {
            get { return _Estabilidade; }
            set { _Estabilidade = value; }
        }
        public short IndTitularSuplente
        {
            get { return _IndTitularSuplente; }
            set { _IndTitularSuplente = value; }
        }
        public short IndGrupoMembro
        {
            get { return _IndGrupoMembro; }
            set { _IndGrupoMembro = value; }
        }
        public int IndStatus
        {
            get { return _IndStatus; }
            set { _IndStatus = value; }
        }
        #endregion

        #region Metodos

        #region ToString

        public override string ToString()
        {
            string ret;

            if (this.IdEmpregado.Id == 0)
                ret = NomeMembro;
            else
            {
                if (this.IdEmpregado.nID_EMPR == null)
                    this.IdEmpregado.Find();

                ret = this.IdEmpregado.tNO_EMPG;
            }
            return ret.ToUpper();
        }
        #endregion

        #region Save

        public override int Save()
        {

            if (this.Id == 0)
            {
                VerificaUltimosMandatos();

                if (this.IndGrupoMembro != (int)GrupoMembro.Empregado)
                {
                    ArrayList list = new MembroCipa().Find(
                        "IdCipa=" + this.IdCipa.Id
                        + " AND IndGrupoMembro=" + this.IndGrupoMembro);

                    if (this.IndGrupoMembro == (int)GrupoMembro.Empregador)
                    {
                        if (this.IdCipa.IdCliente == null)
                            this.IdCipa.Find();

                        DimensionamentoCipa dim
                            = this.IdCipa.IdCliente.GetDimensionamentoCipa();

                        if (list.Count >= (dim.Efetivo + dim.Suplente))
                            throw new Exception("Limite de membros excedido!");

                        if (list.Count < dim.Efetivo)
                        {
                            this.Numero = list.Count;
                            this.IndTitularSuplente = (int)TitularSuplente.Titular;
                        }
                        else
                        {
                            this.Numero = list.Count - (dim.Efetivo - 1);
                            this.IndTitularSuplente = (int)TitularSuplente.Suplente;
                        }
                    }
                    else if (this.IndGrupoMembro == (int)GrupoMembro.Secretario)
                    {
                        if (list.Count == 0)
                            this.IndTitularSuplente = (int)TitularSuplente.Titular;
                        else if (list.Count == 1)
                            this.IndTitularSuplente = (int)TitularSuplente.Suplente;
                        else
                            throw new Exception("Limite de membros excedido!");
                    }
                }
            }

            return base.Save();
        }
        #endregion

        #region GetNomeCargo

        public string GetNomeCargo()
        {
            try
            {
                return GetNomeCargo(
                    this.IndGrupoMembro,
                    this.IndTitularSuplente,
                    this.Numero,
                    (int)this.IndStatus);
            }
            catch
            {
                return string.Empty;
            }
        }

        public static string GetNomeCargo(int indGrupoMembro,
                                    int indTitSupl,
                                    int num,
                                    int status)
        {
            string ret = string.Empty;

            if (status == (int)Status.Ativo)
            {
                if (indGrupoMembro == (int)GrupoMembro.Empregado)
                {
                    if (num == 0 && indTitSupl == (int)TitularSuplente.Titular)
                        ret = "Vice-Presidente";
                    else
                    {
                        if (indTitSupl == (int)TitularSuplente.Titular)
                            ret = num.ToString() + "º Membro";
                        else
                            ret = num.ToString() + "º Suplente";
                    }
                }
                else if (indGrupoMembro == (int)GrupoMembro.Empregador)
                {
                    if (num == 0 && indTitSupl == (int)TitularSuplente.Titular)
                        ret = "Presidente";
                    else
                    {
                        if (indTitSupl == (int)TitularSuplente.Titular)
                            ret = num.ToString() + "º Membro";
                        else
                            ret = num.ToString() + "º Suplente";
                    }
                }
                else if (indGrupoMembro == (int)GrupoMembro.Secretario)
                {
                    if (indTitSupl == (int)TitularSuplente.Titular)
                        ret = "Secretário(a)";
                    else if (indTitSupl == (int)TitularSuplente.Suplente)
                        ret = "Substituto Secretário(a)";
                }
                else
                    ret = string.Empty;
            }
            else if (status == (int)Status.Afastado)
                ret = "Afastado";
            else if (status == (int)Status.Renunciou)
                ret = "Renunciou";

            return ret;
        }
        #endregion

        #region GetNomeGrupoMembro

        public string GetNomeGrupoMembro()
        {
            string ret = string.Empty;
            if (this.IndGrupoMembro == (int)GrupoMembro.Empregador)
                ret = "Representantes do Empregador";
            else if (this.IndGrupoMembro == (int)GrupoMembro.Empregado)
                ret = "Representantes dos Empregados";
            else if (this.IndGrupoMembro == (int)GrupoMembro.Secretario)
                ret = "Secretaria";
            else
                ret = "Outros Participantes";
            return ret;
        }
        #endregion

        #region GetGrupoMembro

        public string GetGrupoMembro()
        {
            string ret = string.Empty;
            if (this.IndGrupoMembro == (int)GrupoMembro.Empregador)
                ret = "Representantes da Organização (Titulares e Suplentes)";
            else if (this.IndGrupoMembro == (int)GrupoMembro.Empregado)
                ret = "Representantes dos Empregados (Titulares e Suplentes)";
            else if (this.IndGrupoMembro == (int)GrupoMembro.Secretario)
                ret = "Secretaria";
            else
                ret = "Outros Participantes";
            return ret;
        }
        #endregion

        #region GetGrupoMembroParaReuniao

        public string GetGrupoMembroParaReuniao()
        {
            string ret = string.Empty;
            if (this.IndGrupoMembro == (int)GrupoMembro.Empregador)
                ret = "Representantes do Empregador (Designados)";
            else if (this.IndGrupoMembro == (int)GrupoMembro.Empregado)
                ret = "Representantes dos Empregados (Eleitos)";
            else if (this.IndGrupoMembro == (int)GrupoMembro.Secretario)
                ret = "Secretariado";
            else
                ret = "Outros Participantes";
            return ret;
        }
        #endregion

        #region ReordenaMembro

        public void ReordenaMembro(int IndGrupoMembro)
        {
            StringBuilder strWhere = new StringBuilder();
            strWhere.Append("IdCipa=" + this.IdCipa.Id);
            strWhere.Append(" AND IndGrupoMembro=" + IndGrupoMembro.ToString());
            strWhere.Append(" AND IndTitularSuplente=" + ((int)TitularSuplente.Titular).ToString());
            strWhere.Append(" AND IndStatus=" + ((int)MembroCipa.Status.Ativo).ToString());
            strWhere.Append(" ORDER BY Numero, ");
            strWhere.Append("(SELECT Votos FROM ParticipantesEleicaoCipa WHERE ");
            strWhere.Append(" IdCipa=" + this.IdCipa.Id);
            strWhere.Append(" AND ParticipantesEleicaoCipa.IdEmpregado = MembroCipa.IdEmpregado)");

            ArrayList list = new MembroCipa().Find(strWhere.ToString());

            for (int i = 0; i < list.Count; i++)
            {
                ((MembroCipa)list[i]).Numero = i;
                ((MembroCipa)list[i]).Save();
            }

            strWhere.Remove(0, strWhere.Length);
            strWhere.Append("IdCipa=" + this.IdCipa.Id);
            strWhere.Append(" AND IndGrupoMembro=" + IndGrupoMembro.ToString());
            strWhere.Append(" AND IndTitularSuplente=" + ((int)TitularSuplente.Suplente).ToString());
            strWhere.Append(" AND IndStatus=" + ((int)MembroCipa.Status.Ativo).ToString());
            strWhere.Append(" ORDER BY Numero");

            list = new MembroCipa().Find(strWhere.ToString());

            for (int i = 0; i < list.Count; i++)
            {
                ((MembroCipa)list[i]).Numero = i + 1;
                ((MembroCipa)list[i]).Save();
            }
        }
        #endregion

        #region VerificaUltimosMandatos

        private void VerificaUltimosMandatos()
        {
            if (this.IndGrupoMembro == (int)GrupoMembro.Outros)
                return;

            if (this.IdCipa.mirrorOld == null)
                this.IdCipa.Find();

            string where = "IdCliente=" + this.IdCipa.IdCliente.Id
                        + " AND Posse IS NOT NULL"
                        + " ORDER BY Posse Desc";

            ArrayList listCipa = new Cipa().Find(where);

            if (listCipa.Count < 2)
                return;

            string where1 = "IdCipa=" + ((Cipa)listCipa[0]).Id
                            + " AND IndGrupoMembro=" + (int)GrupoMembro.Empregador
                            + " AND IndTitularSuplente=1"
                            + " AND IdEmpregado=" + this.IdEmpregado.Id;

            string where2 = "IdCipa=" + ((Cipa)listCipa[1]).Id
                            + " AND IndGrupoMembro=" + (int)GrupoMembro.Empregador
                            + " AND IndTitularSuplente=1"
                            + " AND IdEmpregado=" + this.IdEmpregado.Id;

            if (new MembroCipa().ExecuteCount(where1) > 0
                && new MembroCipa().ExecuteCount(where2) > 0)
                throw new Exception("Candidato Inválido!\n\nEste candidato participou das duas últimas cipas.");
        }
        #endregion

        #region UpSuplenteProcess

        private void UpSuplenteProcess(MembroCipa membroAtual,
                                        ArrayList alSuplentes,
                                        int indMotivoDesligamento,
                                        int IdUsuario,
                                        IDbTransaction transaction)
        {
            ArrayList alPartiEleicaoVotoMax = ParticipantesEleicaoCipa.FindParticipanteVotadoMax(this.IdCipa.Id);

            membroAtual.ChangeToNewMembro(this.IdEmpregado.Id, indMotivoDesligamento, IdUsuario, transaction);

            if (alSuplentes.Count > 1)
            {
                int SuplenteCount = alSuplentes.Count;
                int indalSuplente = 1;

                foreach (MembroCipa mcSuplente in alSuplentes)
                {
                    if (SuplenteCount > 1)
                    {
                        SuplenteCount--;
                        mcSuplente.IdEmpregado.Id
                            = ((MembroCipa)alSuplentes[indalSuplente]).IdEmpregado.Id;

                        mcSuplente.Transaction = transaction;
                        mcSuplente.UsuarioId = IdUsuario;
                        mcSuplente.Save();

                        indalSuplente++;
                    }
                }

                if (alPartiEleicaoVotoMax.Count == 0)
                    ((MembroCipa)alSuplentes[alSuplentes.Count - 1]).IdEmpregado.Id = 0;
                else
                    ((MembroCipa)alSuplentes[alSuplentes.Count - 1]).IdEmpregado.Id
                        = ((ParticipantesEleicaoCipa)alPartiEleicaoVotoMax[0]).IdEmpregado.Id;

                ((MembroCipa)alSuplentes[alSuplentes.Count - 1]).UsuarioId = IdUsuario;
                ((MembroCipa)alSuplentes[alSuplentes.Count - 1]).Transaction = transaction;
                ((MembroCipa)alSuplentes[alSuplentes.Count - 1]).Save();
            }
            else
            {
                if (alPartiEleicaoVotoMax.Count == 0)
                    ((MembroCipa)alSuplentes[alSuplentes.Count - 1]).IdEmpregado.Id = 0;
                else
                    ((MembroCipa)alSuplentes[alSuplentes.Count - 1]).IdEmpregado.Id
                        = ((ParticipantesEleicaoCipa)alPartiEleicaoVotoMax[0]).IdEmpregado.Id;

                ((MembroCipa)alSuplentes[alSuplentes.Count - 1]).UsuarioId = IdUsuario;
                ((MembroCipa)alSuplentes[alSuplentes.Count - 1]).Transaction = transaction;
                ((MembroCipa)alSuplentes[alSuplentes.Count - 1]).Save();
            }
        }
        #endregion

        #region ChangeToNextSuplenteEmpregados

        public void ChangeToNextSuplenteEmpregados(MembroCipa membroSuplenteAtual, int indMotivoDesligamento, int IdUsuario, IDbTransaction transaction)
        {
            ArrayList alQtdSuplentesRestantes = MembroCipa.GetListaSuplentesRestantes(membroSuplenteAtual);

            this.UpSuplenteProcess(membroSuplenteAtual, alQtdSuplentesRestantes, indMotivoDesligamento, IdUsuario, transaction);
        }
        #endregion

        #region ChangeToMembroTitularEmpregados

        public void ChangeToMembroTitularEmpregados(MembroCipa membroTitularAtual, int indMotivoDesligamento, int IdUsuario, IDbTransaction transaction)
        {
            ArrayList alSuplenteDisponivel = MembroCipa.GetListaSuplentes(this.IdCipa, (int)GrupoMembro.Empregado);

            this.UpSuplenteProcess(membroTitularAtual, alSuplenteDisponivel, indMotivoDesligamento, IdUsuario, transaction);
        }
        #endregion

        #region ChangeToSecretarioTitular

        public void ChangeToSecretarioTitular(MembroCipa SecreAtual, int indMotivoDesligamento, int IdUsuario, IDbTransaction transaction)
        {
            SecreAtual.ChangeToNewMembro(this.IdEmpregado.Id, indMotivoDesligamento, IdUsuario, transaction);

            this.IdEmpregado.Id = 0;
            this.UsuarioId = IdUsuario;
            this.Transaction = transaction;

            this.Save();
        }
        #endregion

        #region ChangeToVicePresidente

        public void ChangeToVicePresidente(MembroCipa vicePresiAtual,
                                            int indMotivoDesligamento,
                                            int IdUsuario,
                                            IDbTransaction transaction)
        {
            ArrayList alSuplenteDisponivel = MembroCipa.GetListaSuplentes(this.IdCipa, (int)GrupoMembro.Empregado);

            if (alSuplenteDisponivel.Count == 0)
                throw new Exception("Nenhum Suplente disponível para completar esta operação!");
            else
            {
                vicePresiAtual.ChangeToNewMembro(this.IdEmpregado.Id, indMotivoDesligamento, IdUsuario, transaction);

                this.IdEmpregado.Id = ((MembroCipa)alSuplenteDisponivel[0]).IdEmpregado.Id;
                this.UsuarioId = IdUsuario;
                this.Transaction = transaction;

                this.Save();

                int SuplenteCount = alSuplenteDisponivel.Count;
                int indalSuplente = 1;

                foreach (MembroCipa mcSuplente in alSuplenteDisponivel)
                    if (SuplenteCount > 1)
                    {
                        SuplenteCount--;
                        mcSuplente.IdEmpregado.Id
                            = ((MembroCipa)alSuplenteDisponivel[indalSuplente]).IdEmpregado.Id;
                        mcSuplente.UsuarioId = IdUsuario;
                        mcSuplente.Transaction = transaction;
                        mcSuplente.Save();
                        indalSuplente++;
                    }

                ArrayList alPartiEleicaoComVoto = ParticipantesEleicaoCipa.FindParticipanteVotadoMax(this.IdCipa.Id);

                if (alPartiEleicaoComVoto.Count == 0)
                    ((MembroCipa)alSuplenteDisponivel[alSuplenteDisponivel.Count - 1]).IdEmpregado.Id = 0;
                else
                    ((MembroCipa)alSuplenteDisponivel[alSuplenteDisponivel.Count - 1]).IdEmpregado.Id
                        = ((ParticipantesEleicaoCipa)alPartiEleicaoComVoto[0]).IdEmpregado.Id;

                ((MembroCipa)alSuplenteDisponivel[alSuplenteDisponivel.Count - 1]).UsuarioId = IdUsuario;
                ((MembroCipa)alSuplenteDisponivel[alSuplenteDisponivel.Count - 1]).Transaction = transaction;
                ((MembroCipa)alSuplenteDisponivel[alSuplenteDisponivel.Count - 1]).Save();
            }
        }
        #endregion

        #region ChangeToNewMembro

        public void ChangeToNewMembro(int newIdMembro,
                                    int indMotivoDesligamento,
                                    int IdUsuario,
                                    IDbTransaction transaction)
        {
            this.IndStatus = indMotivoDesligamento;

            this.Transaction = transaction;
            this.UsuarioId = IdUsuario;
            this.UsuarioProcessoRealizado = "Alteração do Empregado da CIPA";

            this.Save();

            MembroCipa newMembroCipa = new MembroCipa();
            newMembroCipa.Inicialize();

            newMembroCipa.IdCipa = this.IdCipa;
            newMembroCipa.IdEmpregado.Id = newIdMembro;
            newMembroCipa.Numero = this.Numero;
            newMembroCipa.IndTitularSuplente = this.IndTitularSuplente;
            newMembroCipa.IndGrupoMembro = this.IndGrupoMembro;
            newMembroCipa.Estabilidade = this.Estabilidade;
            newMembroCipa.IndStatus = (int)MembroCipa.Status.Ativo;

            newMembroCipa.UsuarioId = IdUsuario;
            newMembroCipa.Transaction = transaction;

            newMembroCipa.Save();
        }
        #endregion

        #endregion

        #region static  Metodos

        #region SubstituirMembro

        public static void SubstituirMembro(MembroCipa membroCipa,
                                            MembroCipa suplenteCipa,
                                            MembroCipa.Status status, GrupoMembro grupo)
        {
            membroCipa.IndStatus = (int)status;
            membroCipa.Save();

            if (status == MembroCipa.Status.Ativo)
                suplenteCipa.IndTitularSuplente = (int)TitularSuplente.Suplente;
            else
                suplenteCipa.IndTitularSuplente = (int)TitularSuplente.Titular;

            suplenteCipa.Estabilidade = membroCipa.Estabilidade;
            suplenteCipa.Numero = membroCipa.Numero;
            suplenteCipa.Save();

            membroCipa.ReordenaMembro((int)grupo);
            suplenteCipa.ReordenaMembro((int)grupo);
        }
        #endregion

        #region ArrayList GetListaSuplentesRestantes

        public static ArrayList GetListaSuplentesRestantes(MembroCipa mcSuplenteAtual)
        {
            StringBuilder st = new StringBuilder();

            st.Append("IdCipa=" + mcSuplenteAtual.IdCipa.Id);
            st.Append(" AND IndTitularSuplente=" + (int)TitularSuplente.Suplente);
            st.Append(" AND IndGrupoMembro=" + (int)GrupoMembro.Empregado);
            st.Append(" AND IndStatus=" + (int)MembroCipa.Status.Ativo);
            st.Append(" AND Numero>" + mcSuplenteAtual.Numero);
            st.Append(" ORDER BY Numero");

            return new MembroCipa().Find(st.ToString());
        }
        #endregion

        #region ArrayList GetListaSuplentes

        public static ArrayList GetListaSuplentes(Cipa cipa, int IndGrupoMembro)
        {
            return GetListaSuplentes(cipa, IndGrupoMembro, true);
        }
        #endregion

        #region ArrayList GetListaSuplentes

        public static ArrayList GetListaSuplentes(Cipa cipa, int IndGrupoMembro, bool IsAtivo)
        {
            StringBuilder st = new StringBuilder();
            st.Append("IdCipa=" + cipa.Id);
            st.Append(" AND IndGrupoMembro=" + IndGrupoMembro.ToString());
            st.Append(" AND IndTitularSuplente=" + ((int)TitularSuplente.Suplente).ToString());
            if (IsAtivo)
                st.Append(" AND IndStatus=" + ((int)MembroCipa.Status.Ativo).ToString());
            else
                st.Append(" AND IndStatus<>" + ((int)MembroCipa.Status.Ativo).ToString());
            st.Append(" ORDER BY Numero");

            ArrayList list = new MembroCipa().Find(st.ToString());

            return list;
        }
        #endregion

        #region ArrayList GetListaIntegrantes

        public static ArrayList GetListaIntegrantes(Cipa cipa, int IndGrupoMembro)
        {
            return GetListaIntegrantes(cipa, IndGrupoMembro, true);
        }
        public static ArrayList GetListaIntegrantes(Cipa cipa, int IndGrupoMembro, bool IsAtivo)
        {
            StringBuilder strWhere = new StringBuilder();
            strWhere.Append("IdCipa=" + cipa.Id);
            strWhere.Append(" AND IndGrupoMembro=" + IndGrupoMembro.ToString());
            strWhere.Append(" AND IndTitularSuplente=" + ((int)TitularSuplente.Titular).ToString());
            if (IsAtivo)
                strWhere.Append(" AND IndStatus=" + ((int)MembroCipa.Status.Ativo).ToString());
            else
                strWhere.Append(" AND IndStatus<>" + ((int)MembroCipa.Status.Ativo).ToString());
            strWhere.Append(" ORDER BY Numero");
            ArrayList list = new MembroCipa().Find(strWhere.ToString());
            return list;
        }
        #endregion

        #region DataSet GetMembrosParaReuniao

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public static DataSet GetMembrosParaReuniao(ReuniaoCipa reuniaoCipa, Cliente cliente)
        {
            StringBuilder sqlstm = new StringBuilder();

            sqlstm.Append("IdCliente=" + cliente.Id);
            sqlstm.Append(" AND IdCipa=" + reuniaoCipa.IdCipa.Id);
            sqlstm.Append(" AND IndStatus=" + (int)MembroCipa.Status.Ativo);
            sqlstm.Append(" AND IdMembro NOT IN (SELECT IdMembro FROM ReuniaoPresencaCipa WHERE IdReuniaoCipa=" + reuniaoCipa.Id + ")");

            return new MembroCipa().Get(sqlstm.ToString());
        }

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public static DataSet GetMembrosParaReuniao(ReuniaoCipa reuniaoCipa, int indTitularSuplente)
        {
            StringBuilder sqlstm = new StringBuilder();

            sqlstm.Append("IdCipa=" + reuniaoCipa.IdCipa.Id);
            sqlstm.Append(" AND IndTitularSuplente=" + indTitularSuplente);
            sqlstm.Append(" AND IndStatus=" + (int)MembroCipa.Status.Ativo);
            sqlstm.Append(" AND IdMembro NOT IN (SELECT IdMembro FROM ReuniaoPresencaCipa WHERE IdReuniaoCipa=" + reuniaoCipa.Id + ")");

            return new MembroCipa().Get(sqlstm.ToString());
        }
        #endregion

        #region DataSet GetMembrosParaVicePresi

        public static DataSet GetMembrosParaVicePresi(MembroCipa mcVicePresidenteAtual)
        {
            StringBuilder sqlstm = new StringBuilder();

            sqlstm.Append(" IdCliente=" + mcVicePresidenteAtual.IdCipa.IdCliente.Id);
            sqlstm.Append(" AND IdCipa=" + mcVicePresidenteAtual.IdCipa.Id);
            sqlstm.Append(" AND IdMembroCipa<>" + mcVicePresidenteAtual.Id);
            sqlstm.Append(" AND IndGrupoMembro=" + (int)GrupoMembro.Empregado);
            sqlstm.Append(" AND IndTitularSuplente=" + (int)TitularSuplente.Titular);
            sqlstm.Append(" AND IndStatus=" + (int)MembroCipa.Status.Ativo);

            return new MembroCipa().Get(sqlstm.ToString());
        }
        #endregion

        #region DataSet GetSuplParaSecretario

        public static DataSet GetSuplParaSecretario(MembroCipa mcSecreAtual)
        {
            StringBuilder sqlstm = new StringBuilder();

            sqlstm.Append("IdCliente=" + mcSecreAtual.IdCipa.IdCliente.Id);
            sqlstm.Append(" AND IdCipa=" + mcSecreAtual.IdCipa.Id);
            sqlstm.Append(" AND IndTitularSuplente=" + (int)TitularSuplente.Suplente);
            sqlstm.Append(" AND IndGrupoMembro=" + (int)GrupoMembro.Secretario);
            sqlstm.Append(" AND IndStatus=" + (int)MembroCipa.Status.Ativo);

            return new MembroCipa().Get(sqlstm.ToString());
        }
        #endregion

        #region DataSet GetAllMembrosParaSuplSecre

        public static DataSet GetAllMembrosParaSuplSecre(MembroCipa mcSuplSecreAtual, int IdCliente)
        {
            StringBuilder sqlstm = new StringBuilder();

            sqlstm.Append("IdCliente=" + IdCliente);
            sqlstm.Append(" AND IdCipa=" + mcSuplSecreAtual.IdCipa.Id);
            sqlstm.Append(" AND IdMembro IS NOT NULL");
            sqlstm.Append(" AND IndGrupoMembro=" + (int)GrupoMembro.Secretario);
            sqlstm.Append(" AND IndStatus=" + (int)MembroCipa.Status.Ativo + ")");

            return new MembroCipa().Get(sqlstm.ToString());
        }
        #endregion

        #region DataSet GetSuplParaMembroTitEmpregados

        public static DataSet GetSuplParaMembroTitEmpregados(MembroCipa mcTitularAtual)
        {
            StringBuilder sqlstm = new StringBuilder();

            sqlstm.Append("IdCliente=" + mcTitularAtual.IdCipa.IdCliente.Id);
            sqlstm.Append(" AND IdCipa=" + mcTitularAtual.IdCipa.Id);
            sqlstm.Append(" AND IndGrupoMembro=" + (int)GrupoMembro.Empregado);
            sqlstm.Append(" AND IndTitularSuplente=" + (int)TitularSuplente.Suplente);
            sqlstm.Append(" AND IndStatus=" + (int)MembroCipa.Status.Ativo);
            sqlstm.Append(" AND Numero=(SELECT MIN(Numero) FROM MembroCipa WHERE IdCipa=" + mcTitularAtual.IdCipa.Id);
            sqlstm.Append(" AND IndGrupoMembro=" + (int)GrupoMembro.Empregado);
            sqlstm.Append(" AND IndTitularSuplente=" + (int)TitularSuplente.Suplente);
            sqlstm.Append(" AND IndStatus=" + (int)MembroCipa.Status.Ativo + ")");

            return new MembroCipa().Get(sqlstm.ToString());
        }
        #endregion

        #region DataSet GetNextSuplenteEmpregados

        public static DataSet GetNextSuplenteEmpregados(MembroCipa mcSuplenteAtual)
        {
            StringBuilder st = new StringBuilder();

            st.Append("IdCliente=" + mcSuplenteAtual.IdCipa.IdCliente.Id);
            st.Append(" AND IdCipa=" + mcSuplenteAtual.IdCipa.Id);
            st.Append(" AND IndTitularSuplente=" + (int)TitularSuplente.Suplente);
            st.Append(" AND IndGrupoMembro=" + (int)GrupoMembro.Empregado);
            st.Append(" AND IndStatus=" + (int)MembroCipa.Status.Ativo);
            st.Append(" AND Numero=" + (mcSuplenteAtual.Numero + 1) + ")");

            DataSet ds = new MembroCipa().Get(st.ToString());

            if (ds.Tables[0].Rows.Count == 0)
            {
                ArrayList al = ParticipantesEleicaoCipa.FindParticipanteVotadoMax(mcSuplenteAtual.IdCipa.Id);

                if (al.Count == 0)
                    return ds;
                else
                {
                    st.Remove(0, st.Length);
                    st.Append("IdEmpregado=" + ((ParticipantesEleicaoCipa)al[0]).IdEmpregado.Id);

                    return new MembroCipa().Get(st.ToString());
                }
            }
            else
                return new MembroCipa().Get(st.ToString());
        }
        #endregion

        #endregion
    }
    #endregion

    #region class MembroComissaoEleitoral

    [Database("opsa", "MembroComissaoEleitoral", "IdMembroComissaoEleitoral")]
    public class MembroComissaoEleitoral : Ilitera.Data.Table
    {
        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public MembroComissaoEleitoral()
        {

        }
        private int _IdMembroComissaoEleitoral;
        private Empregado _IdEmpregado;
        private Cipa _IdCipa;
        private string _NomeMembro = string.Empty;
        private string _NomeCargo = string.Empty;

        public override int Id
        {
            get { return _IdMembroComissaoEleitoral; }
            set { _IdMembroComissaoEleitoral = value; }
        }
        public Empregado IdEmpregado
        {
            get { return _IdEmpregado; }
            set { _IdEmpregado = value; }
        }
        public Cipa IdCipa
        {
            get { return _IdCipa; }
            set { _IdCipa = value; }
        }
        public string NomeCargo
        {
            get { return _NomeCargo; }
            set { _NomeCargo = value; }
        }
        public string NomeMembro
        {
            get { return _NomeMembro; }
            set { _NomeMembro = value; }
        }
        public override string ToString()
        {
            if (this.mirrorOld == null)
                this.Find();

            if (this.IdEmpregado.Id != 0)
                return IdEmpregado.ToString();
            else
                return this.NomeMembro;
        }
    }
    #endregion
}