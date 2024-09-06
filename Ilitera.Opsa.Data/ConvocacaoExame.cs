using System;
using System.Data;
using Ilitera.Data;
using Ilitera.Common;

using System.Text;
using System.Collections;
using System.Collections.Generic;

namespace Ilitera.Opsa.Data
{
    [Database("opsa", "ConvocacaoExame", "IdConvocacaoExame")]
    public class ConvocacaoExame : Ilitera.Opsa.Data.Documento, IExameBase
    {
        #region Properties
        private int _IdConvocacaoExame;
        private DateTime _DataConvocacao = DateTime.Today;
        private Juridica _IdJuridica;
        private Medico _IdMedico;
        private ExameDicionario _IdExameDicionario;
        private string _Observacao = string.Empty;
        private bool _NaoRealizado = true;
        private DateTime _VencidosAte;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public ConvocacaoExame()
        {

        }
        public override int Id
        {
            get { return _IdConvocacaoExame; }
            set { _IdConvocacaoExame = value; }
        }
        public DateTime DataConvocacao
        {
            get { return _DataConvocacao; }
            set { _DataConvocacao = value; }
        }
        public Juridica IdJuridica
        {
            get { return _IdJuridica; }
            set { _IdJuridica = value; }
        }
        public Medico IdMedico
        {
            get { return _IdMedico; }
            set { _IdMedico = value; }
        }
        public ExameDicionario IdExameDicionario
        {
            get { return _IdExameDicionario; }
            set { _IdExameDicionario = value; }
        }
        public string Observacao
        {
            get { return _Observacao; }
            set { _Observacao = value; }
        }
        public bool NaoRealizado
        {
            get { return _NaoRealizado; }
            set { _NaoRealizado = value; }
        }
        public DateTime VencidosAte
        {
            get { return _VencidosAte; }
            set { _VencidosAte = value; }
        }
        #endregion

        #region ToString

        public override string ToString()
        {
            return this.IdPedido.GetNumeroPedido() 
                        + " - " 
                        + this.DataConvocacao.ToString("dd-MM-yyyy");
        }
        #endregion

        #region Override Base Table

        public override int Save()
        {
            this.DataLevantamento = this.DataConvocacao;
            this.IdDocumentoBase.Id = (int)Documentos.ExamePeriodico;

            if (this.IdPedido.Id != 0)
            {
                if (this.IdPedido.mirrorOld == null)
                    this.IdPedido.Find();

                this.IdPrestador.Id = this.IdPedido.IdPrestador.Id;
                this.IdExameDicionario.Id = (int)IndExameClinico.Periodico;
            }
            else if (this.IdPedido.Id == 0 && this.IdMedico.Id != 0)
                this.IdPrestador.Id = this.IdMedico.Id;

            if (this.IdPedido.Id == 0 && this.IdExameDicionario.Id == (int)IndExameClinico.Periodico)
                throw new Exception("Para o exame periódico é obrigatório o pedido!");

            return base.Save();
        }
        #endregion

        #region Metodos

        #region GetConvocacao

        public static ConvocacaoExame GetConvocacao(Pedido pedido)
        {
            if (pedido.Id == 0 || pedido.DataSolicitacao == new DateTime())
                throw new Exception("Selecione um pedido!");

            if (pedido.DataCancelamento != new DateTime())
                throw new Exception("Pedido cancelado não pode criar convocação!");

            ConvocacaoExame convocacao = new ConvocacaoExame();
            convocacao.Find("IdPedido=" + pedido.Id);

            if (convocacao.Id == 0)
            {
                if (pedido.mirrorOld == null)
                    pedido.Find();

                if (pedido.IdPrestador.mirrorOld == null)
                    pedido.IdPrestador.Find();

                convocacao.Inicialize();
                convocacao.IdPedido = pedido;
                convocacao.IdCliente = pedido.IdCliente;
                convocacao.IdPrestador.Id = pedido.IdPrestador.Id;
                convocacao.IdMedico.Id = pedido.IdPrestador.Id;
                convocacao.IdJuridica.Id = pedido.IdPrestador.IdJuridica.Id;
                convocacao.IdDocumentoBase.Id = (int)Documentos.ExamePeriodico;
                convocacao.IdExameDicionario.Id = (int)IndExameClinico.Periodico;
                convocacao.NaoRealizado = true;

                if (pedido.IdPedidoGrupo.mirrorOld == null)
                    pedido.IdPedidoGrupo.Find();

                if (pedido.IdPedidoGrupo.IdCompromisso.Id != 0)
                {
                    if (pedido.IdPedidoGrupo.IdCompromisso.mirrorOld == null)
                        pedido.IdPedidoGrupo.IdCompromisso.Find();

                    DateTime dataAgendamento = pedido.IdPedidoGrupo.IdCompromisso.DataInicio;

                    convocacao.DataConvocacao = new DateTime( dataAgendamento.Year, 
                                                              dataAgendamento.Month, 
                                                              dataAgendamento.Day);
                }
            }

            return convocacao;
        }
        #endregion

        #region GetNumeroPeriodicosRealizados

        public int GetNumeroPeriodicosRealizados()
        {
            int numExames;

            string strWhere = "IdExameDicionario=" + (int)IndExameClinico.Periodico
                + " AND IdConvocacaoExame=" + this.Id
                + " AND IndResultado<>" + (int)ResultadoExame.NaoRealizado
                + " AND IndResultado<>" + (int)ResultadoExame.EmEspera
                + " AND IdEmpregado IN (SELECT nID_EMPREGADO FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEMPREGADO WHERE nID_EMPR=" + this.IdCliente.Id + ")";

            numExames = new ExameBase().ExecuteCount(strWhere);

            return numExames;
        }
        #endregion

        #region GetNumeroPeriodicosEmEspera

        public int GetNumeroPeriodicosEmEspera()
        {
            int numExames;

            string strWhere = "IdExameDicionario=" + (int)IndExameClinico.Periodico
                + " AND IdConvocacaoExame=" + this.Id
                + " AND IndResultado=" + (int)ResultadoExame.EmEspera
                + " AND IdEmpregado IN (SELECT nID_EMPREGADO FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEMPREGADO WHERE nID_EMPR=" + this.IdCliente.Id + ")";

            numExames = new ExameBase().ExecuteCount(strWhere);

            return numExames;
        }
        #endregion

        #region GetNumeroPeriodicosPendentes

        public int GetNumeroPeriodicosPendentes()
        {
            int numExames;

            string strWhere = "IdExameDicionario=" + (int)IndExameClinico.Periodico
                + " AND IdConvocacaoExame=" + this.Id
                + " AND IndResultado=" + (int)ResultadoExame.NaoRealizado
                + " AND IndResultado<>" + (int)ResultadoExame.EmEspera
                + " AND IdEmpregado IN (SELECT nID_EMPREGADO FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEMPREGADO WHERE nID_EMPR=" + this.IdCliente.Id + ")";

            numExames = new ExameBase().ExecuteCount(strWhere);

            return numExames;
        }
        #endregion

        #region GetNumeroPeriodicosFaltou

        public int GetNumeroPeriodicosFaltou()
        {
            int numero;

            string strWhere = "IdExameDicionario=" + (int)IndExameClinico.Periodico
                            + " AND IdConvocacaoExame=" + this.Id;

            numero = new ExameNaoRealizado().ExecuteCount(strWhere);

            return numero;
        }
        #endregion

        #region GetExames

        public enum ExamesSelecionados : int
        {
            NaoRealizados,
            Realizados,
            Todos
        }

        public List<Clinico> GetExames(Pcmso pcmso, ExamesSelecionados examesSelecionados)
        {
            StringBuilder str = new StringBuilder();
            str.Append("IdConvocacaoExame=" + this.Id);

            if (examesSelecionados == ExamesSelecionados.NaoRealizados)
                str.Append(" AND IndResultado=" + (int)ResultadoExame.NaoRealizado);
            else if (examesSelecionados == ExamesSelecionados.Realizados)
                str.Append(" AND IndResultado<>" + (int)ResultadoExame.NaoRealizado);

            str.Append(" ORDER BY (SELECT tNO_EMPG FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEMPREGADO WHERE nID_EMPREGADO = ExameBase.IdEmpregado)");

            List<Clinico> clinicos = new Clinico().Find<Clinico>(str.ToString());

            foreach (Clinico clinico in clinicos)
            {
                if (clinico.IdPcmso.Id == 0 || clinico.IdEmpregadoFuncao.Id == 0)
                {
                    clinico.IdPcmso = pcmso;
                    clinico.IdEmpregadoFuncao = EmpregadoFuncao.GetEmpregadoFuncao(this.IdCliente, clinico.IdEmpregado);
                    clinico.Save();
                }
            }

            return clinicos;
        }
        #endregion

        #region GetEmpregadosNaoRealizaramExame

        public List<Empregado> GetEmpregadosNaoRealizaramExame()
        {
            StringBuilder str = new StringBuilder();

            str.Append("nID_EMPREGADO IN "
                        + "(SELECT IdEmpregado"
                        + " FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.ExameBase"
                        + " WHERE IdExameDicionario=" + (int)IndExameClinico.Periodico
                                + " AND IndResultado=" + (int)ResultadoExame.NaoRealizado
                                + " AND IdConvocacaoExame=" + this.Id + ")");

            str.Append(" AND nID_EMPREGADO NOT IN (SELECT IdEmpregado"
                                    + " FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.ExameBase"
                                    + " WHERE IdExameDicionario = 4"
                                    + " AND IndResultado <> 0"
                                    + " AND DataExame >'" + this.DataConvocacao.ToString("yyyy-MM-dd") + "'"
                                    + ")");

            str.Append(" AND nID_EMPREGADO NOT IN (SELECT IdEmpregado"
                                                + " FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.Afastamento"
                                                + " WHERE DataVolta IS NULL)");
            str.Append(" AND hDT_DEM IS NULL");
            str.Append(" AND gTERCEIRO=0");
            str.Append(" ORDER BY tNO_EMPG");

            return new Empregado().Find<Empregado>(str.ToString());
        }
        #endregion

        #endregion
    }

    #region class SolicitacaoComplementar

    [Database("opsa", "SolicitacaoComplementar", "IdSolicitacaoComplementar")]
    public class SolicitacaoComplementar : Ilitera.Data.Table
    {
        #region Properties
        private int _IdSolicitacaoComplementar;
        private DateTime _DataSolicitacao = DateTime.Now;
        private int _IndExameClinico;
        private Medico _IdMedicoResponsavel;
        private Clinica _IdClinica;
        private Empregado _IdEmpregado;


        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public SolicitacaoComplementar()
        {

        }

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public SolicitacaoComplementar(int Id)
        {
            this.Find(Id);
        }
        public override int Id
        {
            get { return _IdSolicitacaoComplementar; }
            set { _IdSolicitacaoComplementar = value; }
        }
        public DateTime DataSolicitacao
        {
            get { return _DataSolicitacao; }
            set { _DataSolicitacao = value; }
        }
        public int IndExameClinico
        {
            get { return _IndExameClinico; }
            set { _IndExameClinico = value; }
        }
        public Medico IdMedicoResponsavel
        {
            get { return _IdMedicoResponsavel; }
            set { _IdMedicoResponsavel = value; }
        }
        public Clinica IdClinica
        {
            get { return _IdClinica; }
            set { _IdClinica = value; }
        }
        public Empregado IdEmpregado
        {
            get { return _IdEmpregado; }
            set { _IdEmpregado = value; }
        }
        #endregion
    }
    #endregion

    #region class ComplementaresSolicitados

    [Database("opsa", "ComplementaresSolicitados", "IdComplementaresSolicitados")]
    public class ComplementaresSolicitados : Ilitera.Data.Table
    {
        #region Properties

        private int _IdComplementaresSolicitados;
        private SolicitacaoComplementar _IdSolicitacaoComplementar;
        private ExameBase _IdExameBase;


        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public ComplementaresSolicitados()
        {

        }

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public ComplementaresSolicitados(int Id)
        {
            this.Find(Id);
        }
        public override int Id
        {
            get { return _IdComplementaresSolicitados; }
            set { _IdComplementaresSolicitados = value; }
        }
        public SolicitacaoComplementar IdSolicitacaoComplementar
        {
            get { return _IdSolicitacaoComplementar; }
            set { _IdSolicitacaoComplementar = value; }
        }
        public ExameBase IdExameBase
        {
            get { return _IdExameBase; }
            set { _IdExameBase = value; }
        }

        #endregion

        #region Metodos
        public ArrayList FindExames(SolicitacaoComplementar solicitacaoComplementar)
        {
            string where = "IdSolicitacaoComplementar=" + solicitacaoComplementar.Id
                + " ORDER BY (SELECT ExameDicionario.Nome FROM ExameBase"
                +" INNER JOIN ExameDicionario"
                +" ON ExameBase.IdExameDicionario=ExameDicionario.IdExameDicionario"
                +" WHERE ExameBase.IdExameBase=ComplementaresSolicitados.IdExameBase)";

            return this.Find(where);
        }
        #endregion
    }
    #endregion
}
