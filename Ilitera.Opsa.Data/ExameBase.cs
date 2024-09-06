using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Collections;
using System.Collections.Generic;

using Ilitera.Data;
using Ilitera.Common;

namespace Ilitera.Opsa.Data
{
    #region enums

    public enum ResultadoExame: short 
	{
		NaoRealizado, 
		Normal, 
		Alterado,
		EmEspera,
        Indefinido
	}


	public enum IndExameClinico: int
	{
		Admissional	= 1,
		Demissional,
		MudancaDeFuncao,
		Periodico,
		RetornoAoTrabalho
    }
    #endregion

    #region interface IExameBase

    public interface IExameBase 
	{
		ExameDicionario IdExameDicionario
		{
			get; 
			set; 
		}

		Juridica IdJuridica
		{
			get; 
			set; 
		}

		Medico IdMedico
		{
			get; 
			set; 
		}
    }
    #endregion

    #region class ExameBase

    [Database("opsa", "ExameBase", "IdExameBase")]
    public class ExameBase : Ilitera.Data.Table, IExameBase
    {
        #region Properties

        private int _IdExameBase;
        private Empregado _IdEmpregado;
        private ExameDicionario _IdExameDicionario;
        private Juridica _IdJuridica;
        private Medico _IdMedico;
        private DateTime _DataCriacao = new DateTime();
        private DateTime _DataExame = DateTime.Now;
        private int _IndResultado;
        private string _Prontuario = string.Empty;
        private ConvocacaoExame _IdConvocacaoExame;
        private Compromisso _IdCompromisso;
        private DateTime _DataPagamento;
        private double _ValorPago;
        private DateTime _DataCancelamento;
        private PagamentoClinica _IdPagamentoClinica;
        private bool _IsFromConvocacaoWeb;
        private string _HoraAgendamento = string.Empty;
        public Clinica clinica;
        private bool _IsNetPDT;
        private string _ObservacaoResultado = string.Empty;
        private DateTime _DataDemissao;
        private int _CodBusca;
        private bool _Tirar_eSocial;
        private bool _LiberarPagamento;


        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public ExameBase()
        {
        }

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public ExameBase(int Id)
        {
            this.Find(Id);
        }

        public override int Id
        {
            get { return _IdExameBase; }
            set { _IdExameBase = value; }
        }

        public string ObservacaoResultado
        {
            get { return _ObservacaoResultado; }
            set { _ObservacaoResultado = value; }
        }
        public Empregado IdEmpregado
        {
            get { return _IdEmpregado; }
            set { _IdEmpregado = value; }
        }
        [Obrigatorio(true, "O Tipo do Exame é obrigatório")]
        public ExameDicionario IdExameDicionario
        {
            get { return _IdExameDicionario; }
            set { _IdExameDicionario = value; }
        }
        [Obrigatorio(true, "A Clinica é obrigatória!")]
        public Juridica IdJuridica
        {
            get { return _IdJuridica; }
            set { _IdJuridica = value; }
        }
        public DateTime DataCriacao
        {
            get { return _DataCriacao; }
            set { _DataCriacao = value; }
        }
        [Obrigatorio(true, "A Data do Exame é obrigatória")]
        public DateTime DataExame
        {
            get { return _DataExame; }
            set { _DataExame = value; }
        }
        public Medico IdMedico
        {
            get { return _IdMedico; }
            set { _IdMedico = value; }
        }
        public int IndResultado
        {
            get { return _IndResultado; }
            set { _IndResultado = value; }
        }
        public string Prontuario
        {
            get { return _Prontuario; }
            set { _Prontuario = value; }
        }
        public ConvocacaoExame IdConvocacaoExame
        {
            get { return _IdConvocacaoExame; }
            set { _IdConvocacaoExame = value; }
        }
        public Compromisso IdCompromisso
        {
            get { return _IdCompromisso; }
            set { _IdCompromisso = value; }
        }
        public DateTime DataPagamento
        {
            get { return _DataPagamento; }
            set { _DataPagamento = value; }
        }
        public double ValorPago
        {
            get { return _ValorPago; }
            set { _ValorPago = value; }
        }
        public DateTime DataCancelamento
        {
            get { return _DataCancelamento; }
            set { _DataCancelamento = value; }
        }
        public PagamentoClinica IdPagamentoClinica
        {
            get { return _IdPagamentoClinica; }
            set { _IdPagamentoClinica = value; }
        }
        public bool IsFromConvocacaoWeb
        {
            get { return _IsFromConvocacaoWeb; }
            set { _IsFromConvocacaoWeb = value; }
        }
        public DateTime DataDemissao
        {
            get { return _DataDemissao; }
            set { _DataDemissao = value; }
        }
        public int CodBusca
        {
            get { return _CodBusca; }
            set { _CodBusca = value; }
        }

        public bool Tirar_eSocial
        {
            get { return _Tirar_eSocial; }
            set { _Tirar_eSocial = value; }
        }

        public bool LiberarPagamento
        {
            get { return _LiberarPagamento; }
            set { _LiberarPagamento = value; }
        }



        [Persist(false)]
        public string HoraAgendamento
        {
            get
            {
                if (this.IdCompromisso != null && this.IdCompromisso.Id != 0)
                {
                    this.IdCompromisso.Find();
                    _HoraAgendamento = this.IdCompromisso.DataInicio.ToString("t");
                }
                return _HoraAgendamento;
            }
            set
            {
                _HoraAgendamento = value;
            }
        }
        [Persist(false)]
        public bool IsNetPDT
        {
            get { return _IsNetPDT; }
            set { _IsNetPDT = value; }
        }
        #endregion

        #region Metodos

        #region override ToString

        public override string ToString()
        {
            if (this.IdEmpregado.mirrorOld == null)
                this.IdEmpregado.Find();

            if (this.IdExameDicionario.mirrorOld == null)
                this.IdExameDicionario.Find();

            if (this.IdEmpregado.nID_EMPR.mirrorOld == null)
                this.IdEmpregado.nID_EMPR.Find();

            return this.DataExame.ToString("dd-MM-yyyy")
                        + " - " + IdEmpregado.nID_EMPR.NomeAbreviado
                        + " - " + this.IdEmpregado.tNO_EMPG
                        + " - " + this.IdExameDicionario.Nome;
        }
        #endregion

        #region override Delete

        //Necessário para clientes que não são PCMSO contratada no Ilitera.NET
        public void Delete(bool forcedDelete)
        {
            if (!forcedDelete)
                if (this.IndResultado != (int)ResultadoExame.NaoRealizado && this.IndResultado != (int)ResultadoExame.EmEspera)
                    throw new Exception("Esse exame não pode ser excluído por ele já ter sido realizado!");

            base.Delete();

            if (this.IdCompromisso.Id != 0)
            {
                this.IdCompromisso.UsuarioId = this.UsuarioId;
                this.IdCompromisso.Find();
                this.IdCompromisso.Transaction = this.Transaction;
                this.IdCompromisso.Delete();
            }
        }

        public override void Delete()
        {
            if (DateTime.Now <= this.DataCriacao.AddHours(2))
                this.Delete(true);
            else
                this.Delete(false);
        }

        public override void Delete(string where)
        {
            throw new Exception("Esse exame não pode ser excluído!");
        }
        #endregion

        #region override Validate

        public override void Validate()
        {
            if (this.DataExame > DateTime.Now.AddYears(1))
                throw new Exception("Data do exame inválida!");

            base.Validate();
        }
        #endregion

        #region override Save

        public override int Save()
        {
            if (this.IdExameDicionario.mirrorOld == null)
                this.IdExameDicionario.Find();
            
            if (this.DataCriacao.Equals(new DateTime()) && !this.IndResultado.Equals((int)ResultadoExame.NaoRealizado) && 
                !this.IdExameDicionario.IndExame.Equals((int)IndTipoExame.NaoOcupacional))
                this.DataCriacao = DateTime.Now;

            if (this.DataCriacao.Equals(new DateTime()) && this.IdExameDicionario.IndExame.Equals((int)IndTipoExame.NaoOcupacional))
                this.DataCriacao = DateTime.Now;
            
            return base.Save();
        }
        #endregion

        #region GetDataEmpregadoTipoExame

        public string GetDataEmpregadoTipoExame()
        {
            if (this.IdEmpregado.mirrorOld == null)
                this.IdEmpregado.Find();

            if (this.IdExameDicionario.mirrorOld == null)
                this.IdExameDicionario.Find();

            return this.DataExame.ToString("dd-MM-yyyy")
                        + " - " + this.IdEmpregado.tNO_EMPG
                        + " - " + this.IdExameDicionario.Descricao;
        }
        #endregion

        #region GetDescricaoExame

        public string GetDescricaoExame()
        {
            if (this.IdEmpregado.mirrorOld == null)
                this.IdEmpregado.Find();

            if (this.IdExameDicionario.mirrorOld == null)
                this.IdExameDicionario.Find();

            return this.DataExame.ToString("dd-MM-yyyy") 
                    + " - " + this.IdExameDicionario.Nome 
                    + " - " + this.IdEmpregado.tNO_EMPG;
        }
        #endregion

        #region Faltou

        public void Faltou(ExameNaoRealizado.MotivoFalta indMotivoFalta)
        {
            this.Faltou(Usuario.Login(), indMotivoFalta);
        }

        public void Faltou(Usuario usuario, ExameNaoRealizado.MotivoFalta indMotivoFalta)
        {
            IDbTransaction transaction = this.GetTransaction();

            try
            {
                ExameNaoRealizado exameNaoRealizado = new ExameNaoRealizado();
                exameNaoRealizado.Inicialize();

                exameNaoRealizado.IdEmpregado.Id = this.IdEmpregado.Id;
                exameNaoRealizado.IdExameDicionario.Id = this.IdExameDicionario.Id;
                exameNaoRealizado.IdJuridica.Id = this.IdJuridica.Id;
                exameNaoRealizado.IdMedico.Id = this.IdMedico.Id;
                exameNaoRealizado.IdConvocacaoExame = this.IdConvocacaoExame;
                exameNaoRealizado.IndMotivoFalta = indMotivoFalta;
                exameNaoRealizado.DataExame = this.DataExame;

                exameNaoRealizado.UsuarioId = usuario.Id;
                exameNaoRealizado.Transaction = transaction;
                exameNaoRealizado.Save();

                this.UsuarioId = usuario.Id;
                this.Delete();

                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }
        }
        #endregion

        #region IsClinico

        public bool IsClinico()
        {
            return ExameDicionario.IsClinico(this.IdExameDicionario.Id);
        }
        #endregion

        #region GetTipoExame

        public string GetTipoExame()
        {
            if (this.IdExameDicionario.mirrorOld == null)
                this.IdExameDicionario.Find();

            if (this.IsClinico())
                return "Clínico - " + this.IdExameDicionario.Nome;
            else
                return "Complementar - " + this.IdExameDicionario.Nome;
        }
        #endregion

        #region GetResultadoExame

        public string GetResultadoExame()
        {
            string ret = string.Empty;

            if (this.IndResultado == (int)ResultadoExame.NaoRealizado)
            {
                if (this.IdExameDicionario.Id == (int)Exames.NaoOcupacional)
                    ret = string.Empty;
                else
                    ret = "Não Realizado";
            }
            else if (this.IndResultado == (int)ResultadoExame.Normal)
            {
                if (this.IdExameDicionario.Id <= 5 && this.IdExameDicionario.Id >= 1)
                    ret = "Apto";
                else
                    ret = "Normal";
            }
            else if (this.IndResultado == (int)ResultadoExame.Alterado)
            {
                if (this.IdExameDicionario.Id <= 5 && this.IdExameDicionario.Id >= 1)
                    ret = "Inapto";
                else
                    ret = "Alterado";
            }
            else if (this.IndResultado == (int)ResultadoExame.EmEspera)
                ret = "Em Espera";

            return ret;
        }
        #endregion

        #region GetResultadoExamePPP

        public string GetResultadoExamePPP()
        {
            string ret;
            if (this.IndResultado == (int)ResultadoExame.NaoRealizado)
                ret = "Não Realizado";
            else if (this.IndResultado == (int)ResultadoExame.Normal)
                ret = "Normal";
            else
                ret = "Alterado";
            return ret;
        }
        #endregion

        #region static GetUltimoExame

        public static ExameBase GetUltimoExame(Clinico clinico, int IdExameDicionario, bool ExamesSomentePeriodoPcmso)
        {
            StringBuilder whereExame = new StringBuilder();

            if (clinico.IdEmpregado != null)
            {
                whereExame.Append("IdEmpregado=" + clinico.IdEmpregado.Id);
            }
            else
            {
                whereExame.Append("IdEmpregado=" + clinico.IdEmpregadoFuncao.nID_EMPREGADO.Id);
            }
            whereExame.Append(" AND IdExameDicionario=" + IdExameDicionario + " and IndResultado not in (0,3 ) "); // <> 0 ");

            //wagner - ajuste para aso não exibir data complementares maior que data do ASO
            whereExame.Append(" AND DataExame <= '" + clinico.DataExame.ToString("yyyy-MM-dd") + " 23:59'");


            if (ExamesSomentePeriodoPcmso ) //&& clinico.IdConvocacaoExame.Id != 0)
            {
                if (clinico.IdPcmso.mirrorOld == null)
                    clinico.IdPcmso.Find();

                whereExame.Append(" AND DataExame BETWEEN '" + clinico.IdPcmso.DataPcmso.ToString("yyyy-MM-dd") + "'");
                whereExame.Append(" AND '" + clinico.IdPcmso.GetTerninoPcmso().ToString("yyyy-MM-dd") + " 23:59'");
            }

            ExameBase ultimoExame = new ExameBase();
            ultimoExame.FindMax("DataExame", whereExame.ToString());

            return ultimoExame;
        }
        #endregion

        #region static GetDataUltimo

        public static DateTime GetDataUltimo(Clinico clinico, int IdExameDicionario, bool ExamesSomentePeriodoPcmso)
        {
            ExameBase ultimoExame = GetUltimoExame(clinico, IdExameDicionario, ExamesSomentePeriodoPcmso);

            if (ultimoExame.Id == 0)
                return new DateTime();
            else
                return ultimoExame.DataExame;
        }
        #endregion

        #region static GetExamesClinicosAgendados

        public static DataSet GetExamesClinicosAgendados(Cliente cliente, bool isARealizar)
        {
            return GetExamesClinicosAgendados(cliente, isARealizar, 0, 0);
        }
        
        public static DataSet GetExamesClinicosAgendados(Cliente cliente, bool isARealizar, int Mes, int Ano)
        {
            StringBuilder sqlstm = new StringBuilder();

            sqlstm.Append("SELECT IdExameBase, IdEmpregado, IdJuridica, IdCompromisso, DataExame, DataCompromisso, NomeEmpregado, ");
            sqlstm.Append("NomeExame, NomeClinica, IndResultado, NomeAlocado FROM qryExameClinicoAgendadoWeb WHERE ");

            if (!Mes.Equals(0))
                sqlstm.Append("MONTH(DataExame)=" + Mes + " AND ");
            if (!Ano.Equals(0))
                sqlstm.Append("YEAR(DataExame)=" + Ano + " AND ");
            
            if (isARealizar)
            {
                sqlstm.Append("(IdCompromisso IN (SELECT IdCompromisso FROM Compromisso WHERE DataInicio>'" + DateTime.Now.ToString("yyyy-MM-dd") + " " + DateTime.Now.ToString("T") + "')");
                sqlstm.Append(" OR (DataExame>='" + DateTime.Now.ToString("yyyy-MM-dd") + "' AND IdCompromisso IS NULL))");
            }
            else
            {
                sqlstm.Append("(IdCompromisso IN (SELECT IdCompromisso FROM Compromisso WHERE DataInicio<='" + DateTime.Now.ToString("yyyy-MM-dd") + " " + DateTime.Now.ToString("T") + "')");
                sqlstm.Append(" OR (DataExame<'" + DateTime.Now.ToString("yyyy-MM-dd") + "' AND IdCompromisso IS NULL))");
            }

            sqlstm.Append(" AND IdEmpregado IN (SELECT " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEMPREGADO.nID_EMPREGADO FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEMPREGADO_FUNCAO INNER JOIN"
                + " " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEMPREGADO ON " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEMPREGADO_FUNCAO.nID_EMPREGADO = " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEMPREGADO.nID_EMPREGADO"
                + " WHERE " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEMPREGADO_FUNCAO.nID_EMPR=" + cliente.Id
                + " OR " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEMPREGADO.nID_EMPR=" + cliente.Id + ")");

            sqlstm.Append(" ORDER BY DataExame");

            return new ExameBase().ExecuteDataset(sqlstm.ToString());
        }
        #endregion

        #region GetExamesASeremRealizadosClinica

        public DataSet GetExamesASeremRealizadosClinica(int IdClinica)
        {
            Clinica clinica = new Clinica(IdClinica);

            if (clinica.NecessitaAgendamento)
                return this.Get("IdCompromisso IN (SELECT IdCompromisso FROM Compromisso WHERE DataInicio>'" + DateTime.Now.ToString("yyyy-MM-dd") + " " + DateTime.Now.ToString("T") + "') AND IdJuridica=" + IdClinica + " ORDER BY DataExame");
            else
                return this.Get("DataExame>='" + DateTime.Now.ToString("yyyy-MM-dd") + "' AND IdJuridica=" + IdClinica + " AND IdCompromisso IS NULL ORDER BY DataExame");
        }
        #endregion

        #region GetExamesRealizados

        public DataSet GetExamesRealizados(int IdClinica, int Mes, int Ano, bool Pagos)
        {
            StringBuilder st = new StringBuilder();

            st.Append("IdJuridica=" + IdClinica 
                        + " AND IdMedico<>" + (int)Medicos.PcmsoNaoContratada
                        + " AND MONTH(DataExame)=" + Mes
                        + " AND YEAR(DataExame)=" + Ano
                        + " AND IndResultado<>0");
            if (Pagos)
                st.Append(" AND DataPagamento IS NOT NULL");
            else
                st.Append(" AND DataPagamento IS NULL");

            st.Append(" ORDER BY DataExame");

            return this.Get(st.ToString());
        }

        public DataSet GetExamesRealizados(int IdClinica, int Mes, int Ano)
        {
            return this.Get("IdJuridica=" + IdClinica 
                            + " AND IdMedico <>" + (int)Medicos.PcmsoNaoContratada
                            + " AND MONTH(DataExame)=" + Mes
                            + " AND YEAR(DataExame)=" + Ano
                            + " AND IndResultado<>0"
                            + " ORDER BY DataExame");
        }
        #endregion

        #endregion
    }
    #endregion

    #region class Clinico

    [Database("opsa", "Clinico","IdClinico")]
    public class Clinico : Ilitera.Opsa.Data.ExameBase
    {
        #region Properties

        private int _IdClinico;
        private bool _DescExameCompl;
        private EmpregadoFuncao _IdEmpregadoFuncao;
        private Pcmso _IdPcmso;
        private bool _EncaminhamentoEspecialista;
        private bool _DescExameComplPreventivo;

        private string _apt_Espaco_Confinado2;
        private string _apt_Trabalho_Altura2;
        private string _apt_Transporte2;
        private string _apt_Submerso2;
        private string _apt_Eletricidade2;
        private string _apt_Aquaviario2;
        private string _apt_Alimento2;
        private string _apt_Outras2;
        
        private string _apt_Brigadista2;
        private string _apt_Socorrista2;
        private string _apt_Respirador2;

        private string _apt_Radiacao2;

        private bool _Paciente_Critico;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public Clinico()
        {

        }

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public Clinico(int Id)
        {
            this.Find(Id);
        }
        public override int Id
        {
            get { return _IdClinico; }
            set { _IdClinico = value; }
        }
        public EmpregadoFuncao IdEmpregadoFuncao
        {
            get { return _IdEmpregadoFuncao; }
            set { _IdEmpregadoFuncao = value; }
        }
        public Pcmso IdPcmso
        {
            get { return _IdPcmso; }
            set { _IdPcmso = value; }
        }
        public bool DescExameComplPreventivo
        {
            get { return _DescExameComplPreventivo; }
            set { _DescExameComplPreventivo = value; }
        }
        public bool DescExameCompl
        {
            get { return _DescExameCompl; }
            set { _DescExameCompl = value; }
        }
        public bool EncaminhamentoEspecialista
        {
            get { return _EncaminhamentoEspecialista; }
            set { _EncaminhamentoEspecialista = value; }
        }
        public string apt_Espaco_Confinado2
        {
            get { return _apt_Espaco_Confinado2; }
            set { _apt_Espaco_Confinado2 = value; }
        }
        public string apt_Trabalho_Altura2
        {
            get { return _apt_Trabalho_Altura2; }
            set { _apt_Trabalho_Altura2 = value; }
        }
        public string apt_Transporte2
        {
            get { return _apt_Transporte2; }
            set { _apt_Transporte2 = value; }
        }
        public string apt_Submerso2
        {
            get { return _apt_Submerso2; }
            set { _apt_Submerso2 = value; }
        }
        public string apt_Eletricidade2
        {
            get { return _apt_Eletricidade2; }
            set { _apt_Eletricidade2 = value; }
        }
        public string apt_Aquaviario2
        {
            get { return _apt_Aquaviario2; }
            set { _apt_Aquaviario2 = value; }
        }
        public string apt_Alimento2
        {
            get { return _apt_Alimento2; }
            set { _apt_Alimento2 = value; }
        }
        public string apt_Outras2
        {
            get { return _apt_Outras2; }
            set { _apt_Outras2 = value; }
        }
        public string apt_Brigadista2
        {
            get { return _apt_Brigadista2; }
            set { _apt_Brigadista2 = value; }
        }
        public string apt_Socorrista2
        {
            get { return _apt_Socorrista2; }
            set { _apt_Socorrista2 = value; }
        }
        public string apt_Respirador2
        {
            get { return _apt_Respirador2; }
            set { _apt_Respirador2 = value; }
        }
        public bool Paciente_Critico
        {
            get { return _Paciente_Critico; }
            set { _Paciente_Critico = value; }
        }
        public string apt_Radiacao2
        {
            get { return _apt_Radiacao2; }
            set { _apt_Radiacao2 = value; }
        }


        #endregion

        #region Validate

        public override void Validate()
        {
            ValidaExameBase();

            base.Validate();
        }

        private void ValidaExameBase()
        {
            //Usuario usuario = new Usuario();

            //if (this.UsuarioId == 0)
            //    usuario.Find(Usuario.Login().Id);
            //else
            //    usuario.Find(UsuarioId);

            //if (this.Id.Equals(0) && this.IdExameDicionario.Id.Equals((int)IndExameClinico.Admissional))
            //{
            //    string where = "IdExameDicionario=" + (int)IndExameClinico.Admissional
            //                + " AND IndResultado<>" + (int)ResultadoExame.Alterado
            //                + " AND IdEmpregado=" + this.IdEmpregado.Id.ToString();

            //    int count = new Clinico().ExecuteCount(where);

            //    if (count > 0)
            //        throw new Exception("Já existe um Exame Admissional cadastrado com o resultado Apto, Em Espera ou Não Realizado para este empregado!");
            //}

            //if (this.Id.Equals(0) && this.IdExameDicionario.Id.Equals((int)IndExameClinico.Demissional))
            //{
            //    string where = "IdExameDicionario=" + (int)IndExameClinico.Demissional
            //                + " AND IndResultado<>" + (int)ResultadoExame.Alterado
            //                + " AND IndResultado<>" + (int)ResultadoExame.EmEspera
            //                + " AND IdEmpregado=" + this.IdEmpregado.Id.ToString();

            //    int count = new Clinico().ExecuteCount(where);

            //    if (count > 0)
            //        throw new Exception("Já existe um Exame Demissional cadastrado com o resultado Apto, Em Espera ou Não Realizado para este empregado!");
            //}

            //Agendamento de exames
            if (this.IndResultado == (int)ResultadoExame.NaoRealizado)
            {
                if (clinica == null)
                {
                    clinica = new Clinica();
                    clinica.Find(this.IdJuridica.Id);
                }
                //if (clinica.NecessitaAgendamento
                //    && this.HoraAgendamento != string.Empty 
                //    && this.HoraAgendamento != "--:--")
                //    AddCompromisso(usuario);
            }
        }
        #endregion

        #region Dispose

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.complementares != null)
                    this.complementares = null;
            }

            base.Dispose(disposing);
        }
        #endregion

        #region ValidarDadosAso

        public void ValidarDadosAso()
        {
            if (this.Id == 0)
                throw new Exception("O exame é obrigatório!");

            if (this.IdEmpregado.mirrorOld == null)
                this.IdEmpregado.Find();

            if (this.IdEmpregadoFuncao.mirrorOld == null)
                this.IdEmpregadoFuncao.Find();

            if (this.IdEmpregado.Id == 0)
                throw new Exception("O empregado é campo obrigatório!");

            if (this.IdEmpregado.hDT_NASC == new DateTime())
                throw new Exception("O empregado '" + this.IdEmpregado.tNO_EMPG + "' não possui data de nascimento!");

            //if (this.IdEmpregado.tNO_IDENTIDADE == string.Empty)
            //    throw new Exception("O empregado '" + this.IdEmpregado.tNO_EMPG + "' não possui identidade!");

            if (this.IdEmpregado.tSEXO.Trim() == string.Empty)
                throw new Exception("Sexo não informado do empregado '" + this.IdEmpregado.tNO_EMPG + "'");

            if (this.IdEmpregadoFuncao.Id == 0)
                throw new Exception("O empregado '" + this.IdEmpregado.tNO_EMPG + "' não possui Classificação Funcional!");

            if (this.IdEmpregadoFuncao.nID_SETOR.Id == 0)
                throw new Exception("É necessário a indicação do Setor na Classificação Funcional! do empregado '" + this.IdEmpregado.tNO_EMPG + "'");

            if (this.IdEmpregadoFuncao.nID_FUNCAO.Id == 0)
                throw new Exception("É necessário a indicação da Função na Classificação Funcional! do empregado '" + this.IdEmpregado.tNO_EMPG + "'");
        }
        #endregion

        #region List<ExameComplementar>

        public struct ExameComplementar
        {
            public string Exame;
            public bool Preventivo;
            public DateTime dataUltimo;
            public string Periodicidade;
            public Int32 IdExameDicionario;
            public Empregado_Aptidao_Planejamento empr_apt_Planejamento;
        }

        private List<ExameComplementar> _complementares;

        [Persist(false)]
        public List<ExameComplementar> complementares
        {
            get
            {
                if (this._complementares == null)
                    this._complementares = this.ExamesComplementares(false);

                return _complementares;

            }
            set { _complementares = value; }
        }


        private List<ExameComplementar> ExamesComplementares_Mudanca_Funcao(Ghe zGhe1, Ghe zGhe2, bool ExamesSomentePeriodoPcmso)
        {
            //if (this.IdPcmso.mirrorOld == null)
            //    this.IdPcmso.Find();


            //Ghe ghe = this.IdEmpregadoFuncao.GetGheEmpregado(this.IdPcmso.IdLaudoTecnico);

            string wherePcmsoPlan = this.GetWhereExamesComplementares_Mudanca_Funcao(zGhe1, "Admissional");

            List<PcmsoPlanejamento> listPcmsoPlan = new PcmsoPlanejamento().Find<PcmsoPlanejamento>(wherePcmsoPlan);

            List<ExameComplementar> ret = new List<ExameComplementar>();

            bool rAvaliacao_Clinica = true;

            foreach (PcmsoPlanejamento pcmsoPlan in listPcmsoPlan)
            {
                ExameComplementar complementar = new ExameComplementar();
                complementar.Exame = pcmsoPlan.IdExameDicionario.ToString();
                complementar.Preventivo = pcmsoPlan.Preventivo;
                complementar.dataUltimo = ExameBase.GetDataUltimo(this, pcmsoPlan.IdExameDicionario.Id, ExamesSomentePeriodoPcmso);
                complementar.Periodicidade = ExameDicionario.GetPeriodicidadeExame(pcmsoPlan);
                complementar.IdExameDicionario = pcmsoPlan.IdExameDicionario.Id;

                if (complementar.Exame.ToUpper().IndexOf("AVALIACAO CL") >= 0 || complementar.Exame.ToUpper().IndexOf("AVALIAÇÃO CL") >= 0)
                {
                    rAvaliacao_Clinica = false;
                }


                ret.Add(complementar);
            }



            string wherePcmsoPlan2 = this.GetWhereExamesComplementares_Mudanca_Funcao(zGhe2, "Demissional");

            List<PcmsoPlanejamento> listPcmsoPlan2 = new PcmsoPlanejamento().Find<PcmsoPlanejamento>(wherePcmsoPlan2);

            //List<ExameComplementar> ret = new List<ExameComplementar>();

            foreach (PcmsoPlanejamento pcmsoPlan in listPcmsoPlan2)
            {
                bool kLoc = false;

                for (int kCont = 0; kCont < ret.Count; kCont++)
                {
                    if (ret[kCont].Exame == pcmsoPlan.IdExameDicionario.ToString())
                    {
                        kLoc = true;
                        break;
                    }
                }

                if (kLoc == false)
                {
                    ExameComplementar complementar = new ExameComplementar();
                    complementar.Exame = pcmsoPlan.IdExameDicionario.ToString();
                    complementar.Preventivo = pcmsoPlan.Preventivo;
                    complementar.dataUltimo = ExameBase.GetDataUltimo(this, pcmsoPlan.IdExameDicionario.Id, ExamesSomentePeriodoPcmso);
                    complementar.Periodicidade = ExameDicionario.GetPeriodicidadeExame(pcmsoPlan);
                    complementar.IdExameDicionario = pcmsoPlan.IdExameDicionario.Id;

                    if (complementar.Exame.ToUpper().IndexOf("AVALIACAO CL") >= 0 || complementar.Exame.ToUpper().IndexOf("AVALIAÇÃO CL") >= 0)
                    {
                        rAvaliacao_Clinica = false;
                    }

                   

                    ret.Add(complementar);
                }
            }


            if (rAvaliacao_Clinica == true)
            {
                ExameDicionario rEx = new ExameDicionario();
                rEx.Find("UPPER(Nome) like '%AVALIAÇÃO CL%'");
                if (rEx.Id == 0)
                {
                    rEx.Find("UPPER(Nome) like '%AVALIACAO CL%'");
                }

                if (rEx.Id != 0)
                {
                    ExameComplementar complementar = new ExameComplementar();
                    complementar.Exame = "Avaliação Clínica";
                    complementar.Preventivo = false;
                    //complementar.dataUltimo = "";
                    complementar.Periodicidade = "Periódico(Anual)";
                    complementar.IdExameDicionario = rEx.Id;

                    ret.Add(complementar);
                }
            }




            return ret;
        }



        private List<ExameComplementar> ExamesComplementares(bool ExamesSomentePeriodoPcmso)
        {
            if (this.IdPcmso.mirrorOld == null)
                this.IdPcmso.Find();

            Ghe ghe = this.IdEmpregadoFuncao.GetGheEmpregado(this.IdPcmso.IdLaudoTecnico);

            string wherePcmsoPlan = this.GetWhereExamesComplementares(ghe);

            List<PcmsoPlanejamento> listPcmsoPlan = new PcmsoPlanejamento().Find<PcmsoPlanejamento>(wherePcmsoPlan);

            List<ExameComplementar> ret = new List<ExameComplementar>();

            bool rAvaliacao_Clinica = true;

            foreach (PcmsoPlanejamento pcmsoPlan in listPcmsoPlan)
            {
                bool rLoc = true;

                if (pcmsoPlan.DependeIdade == true)
                {
                    rLoc = false;

                    List<PcmsoPlanejamentoIdade> listPcmsoPlanIdade = new PcmsoPlanejamentoIdade().Find<PcmsoPlanejamentoIdade>(" IdPcmsoPlanejamento = " + pcmsoPlan.Id);
                    foreach (PcmsoPlanejamentoIdade pcmsoPlanIdade in listPcmsoPlanIdade)
                    {
                        int xIdade = this.IdEmpregado.IdadeEmpregado();

                        if (xIdade >= pcmsoPlanIdade.AnoInicio && xIdade <= pcmsoPlanIdade.AnoTermino)
                            rLoc = true;

                    }

                }

                if (rLoc == true)
                {
                    ExameComplementar complementar = new ExameComplementar();
                    complementar.Exame = pcmsoPlan.IdExameDicionario.ToString();
                    complementar.Preventivo = pcmsoPlan.Preventivo;
                    complementar.dataUltimo = ExameBase.GetDataUltimo(this, pcmsoPlan.IdExameDicionario.Id, ExamesSomentePeriodoPcmso);
                    complementar.Periodicidade = ExameDicionario.GetPeriodicidadeExame(pcmsoPlan);
                    complementar.IdExameDicionario = pcmsoPlan.IdExameDicionario.Id;

                    if (complementar.Exame.ToUpper().IndexOf("AVALIACAO CL") >= 0 || complementar.Exame.ToUpper().IndexOf("AVALIAÇÃO CL") >= 0)
                    {
                        rAvaliacao_Clinica = false;
                    }


                    ret.Add(complementar);
                }
            }


            if (rAvaliacao_Clinica == true)
            {
                ExameDicionario rEx = new ExameDicionario();
                rEx.Find("UPPER(Nome) like '%AVALIAÇÃO CL%'");
                if (rEx.Id == 0)
                {
                    rEx.Find("UPPER(Nome) like '%AVALIACAO CL%'");
                }

                if (rEx.Id != 0)
                {
                    ExameComplementar complementar = new ExameComplementar();
                    complementar.Exame = "Avaliação Clínica";
                    complementar.Preventivo = false;
                    //complementar.dataUltimo = "";
                    complementar.Periodicidade = "Periódico(Anual)";
                    complementar.IdExameDicionario = rEx.Id;

                    ret.Add(complementar);
                }
            }

            return ret;
        }


        public List<ExameComplementar> ExamesComplementares_Aptidao(Ilitera.Opsa.Data.Empregado_Aptidao rAptidao)
        {
            if (this.IdPcmso.mirrorOld == null)
                this.IdPcmso.Find();

            //Ghe ghe = this.IdEmpregadoFuncao.GetGheEmpregado(this.IdPcmso.IdLaudoTecnico);

            string wherePcmsoPlan = this.GetWhereExamesComplementares_Aptidao(rAptidao);

            //List<PcmsoPlanejamento> listPcmsoPlan = new PcmsoPlanejamento().Find<PcmsoPlanejamento>(wherePcmsoPlan);
            List<Ilitera.Opsa.Data.Empregado_Aptidao_Planejamento> listPcmsoPlan = new Ilitera.Opsa.Data.Empregado_Aptidao_Planejamento().Find<Ilitera.Opsa.Data.Empregado_Aptidao_Planejamento>(wherePcmsoPlan);

            List<ExameComplementar> ret = new List<ExameComplementar>();

            foreach (Ilitera.Opsa.Data.Empregado_Aptidao_Planejamento pcmsoPlan in listPcmsoPlan)
            {
                bool zLoc = false;

                for (int zCont = 0; zCont < ret.Count; zCont++)
                {
                    if (ret[zCont].Exame.Trim() == pcmsoPlan.IdExameDicionario.ToString().Trim())
                        zLoc = true;
                }

                if (zLoc == false)
                {
                    ExameComplementar complementar = new ExameComplementar();
                    complementar.Exame = pcmsoPlan.IdExameDicionario.ToString();
                    complementar.Preventivo = false;
                    complementar.dataUltimo = ExameBase.GetDataUltimo(this, pcmsoPlan.IdExameDicionario.Id, false);
                    complementar.Periodicidade = ExameDicionario.GetPeriodicidadeExame(pcmsoPlan);
                    complementar.IdExameDicionario = pcmsoPlan.IdExameDicionario.Id;
                    complementar.empr_apt_Planejamento = pcmsoPlan;

                    ret.Add(complementar);
                }
            }

            return ret;
        }




        private List<ExameComplementar> ExamesComplementares(bool ExamesSomentePeriodoPcmso, string zTipo)
        {
            if (this.IdPcmso.mirrorOld == null)
                this.IdPcmso.Find();

            Ghe ghe = this.IdEmpregadoFuncao.GetGheEmpregado(this.IdPcmso.IdLaudoTecnico);

            string wherePcmsoPlan = this.GetWhereExamesComplementares(ghe, zTipo);

            List<PcmsoPlanejamento> listPcmsoPlan = new PcmsoPlanejamento().Find<PcmsoPlanejamento>(wherePcmsoPlan);

            List<ExameComplementar> ret = new List<ExameComplementar>();

            foreach (PcmsoPlanejamento pcmsoPlan in listPcmsoPlan)
            {
                ExameComplementar complementar = new ExameComplementar();
                complementar.Exame = pcmsoPlan.IdExameDicionario.ToString();
                complementar.Preventivo = pcmsoPlan.Preventivo;
                complementar.dataUltimo = ExameBase.GetDataUltimo(this, pcmsoPlan.IdExameDicionario.Id, ExamesSomentePeriodoPcmso);
                complementar.Periodicidade = ExameDicionario.GetPeriodicidadeExame(pcmsoPlan);
                complementar.IdExameDicionario = pcmsoPlan.IdExameDicionario.Id;

                ret.Add(complementar);
            }

            return ret;
        }


        #region GetWhereExamesComplementares

        public string GetWhereExamesComplementares(Ghe ghe)
        {
            StringBuilder ret = new StringBuilder();

            //Mostra também os preventicos devido a Polengui Goiatuba
            ret.Append("IdPcmso=" + this.IdPcmso.Id + " AND IdGhe=" + ghe.Id);

            if (this.IdExameDicionario != null)
            {
                if ((int)IndExameClinico.Admissional == this.IdExameDicionario.Id)
                    ret.Append(" AND NaAdmissao=1");
                else if ((int)IndExameClinico.Demissional == this.IdExameDicionario.Id)
                    ret.Append(" AND NaDemissao=1");
                else if ((int)IndExameClinico.MudancaDeFuncao == this.IdExameDicionario.Id)
                    ret.Append(" AND NaMudancaFuncao=1");
                else if ((int)IndExameClinico.Periodico == this.IdExameDicionario.Id)
                    ret.Append(" AND Periodico=1");
                else if ((int)IndExameClinico.RetornoAoTrabalho == this.IdExameDicionario.Id)
                    ret.Append(" AND NoRetornoTrabalho=1");
            }

            ret.Append(" AND IdExameDicionario <>" + (int)IndExameClinico.Periodico);
            ret.Append(" AND IdExameDicionario NOT IN (SELECT IdExameDicionario FROM ExameDicionario WHERE IsObservacao = 1)");

            ret.Append(" ORDER BY (SELECT Nome FROM ExameDicionario WHERE ExameDicionario.IdExameDicionario = PcmsoPlanejamento.IdExameDicionario)");

            return ret.ToString();
        }


        private string GetWhereExamesComplementares(Ghe ghe, string zTipo)
        {
            StringBuilder ret = new StringBuilder();

            //Mostra também os preventicos devido a Polengui Goiatuba
            ret.Append("IdPcmso=" + this.IdPcmso.Id + " AND IdGhe=" + ghe.Id);

            
            if (zTipo == "A") ret.Append(" AND NaAdmissao=1");
            else if (zTipo == "D") ret.Append(" AND NaDemissao=1");
            else if (zTipo == "M") ret.Append(" AND NaMudancaFuncao=1");
            else if (zTipo == "P") ret.Append(" AND Periodico=1");
            else if (zTipo == "R") ret.Append(" AND NoRetornoTrabalho=1");

            ret.Append(" AND IdExameDicionario <>" + (int)IndExameClinico.Periodico);
            ret.Append(" AND IdExameDicionario NOT IN (SELECT IdExameDicionario FROM ExameDicionario WHERE IsObservacao = 1)");

            ret.Append(" ORDER BY (SELECT Nome FROM ExameDicionario WHERE ExameDicionario.IdExameDicionario = PcmsoPlanejamento.IdExameDicionario)");

            return ret.ToString();
        }




        private string GetWhereExamesComplementares_Aptidao(Ilitera.Opsa.Data.Empregado_Aptidao vAptidao)
        {
            StringBuilder ret = new StringBuilder();


            Empregado gEmpregado = new Empregado(vAptidao.nId_Empregado);
            gEmpregado.nID_EMPR.Find();

            //ret.Append(" nId_Empr = " + gEmpregado.nID_EMPR.Id.ToString() + " ");  // nId_Aptidao=" + vAptidao.);

            this.IdEmpregadoFuncao.Find();
            this.IdEmpregadoFuncao.nID_EMPR.Find();
            if (this.IdEmpregadoFuncao.nID_EMPR.Id != 0)
            {
                ret.Append(" nId_Empr = " + this.IdEmpregadoFuncao.nID_EMPR.Id.ToString() + " ");  // nId_Aptidao=" + vAptidao.);
            }
            else
            {
                ret.Append(" nId_Empr = " + gEmpregado.nID_EMPR.Id.ToString() + " ");  // nId_Aptidao=" + vAptidao.);
            }


                string vRet = " AND  ( ";


            if (vAptidao.apt_Alimento == true)
                vRet = vRet + " nId_Aptidao = " + (int)Ilitera.Opsa.Data.Empregado_Aptidao.Aptidao.Alimento + " or ";

            if (vAptidao.apt_Aquaviario == true)
                vRet = vRet + " nId_Aptidao = " + (int)Ilitera.Opsa.Data.Empregado_Aptidao.Aptidao.Aquaviario + " or ";

            if (vAptidao.apt_Eletricidade == true)
                vRet = vRet + " nId_Aptidao = " + (int)Ilitera.Opsa.Data.Empregado_Aptidao.Aptidao.Eletricidade + " or ";

            if (vAptidao.apt_Espaco_Confinado == true)
                vRet = vRet + " nId_Aptidao = " + (int)Ilitera.Opsa.Data.Empregado_Aptidao.Aptidao.EspacoConfinado + " or ";

            if (vAptidao.apt_Submerso == true)
                vRet = vRet + " nId_Aptidao = " + (int)Ilitera.Opsa.Data.Empregado_Aptidao.Aptidao.Submerso + " or ";

            if (vAptidao.apt_Trabalho_Altura == true)
                vRet = vRet + " nId_Aptidao = " + (int)Ilitera.Opsa.Data.Empregado_Aptidao.Aptidao.TrabalhoAltura + " or ";

            if (vAptidao.apt_Transporte == true)
                vRet = vRet + " nId_Aptidao = " + (int)Ilitera.Opsa.Data.Empregado_Aptidao.Aptidao.Transportes + " or ";

            if (vAptidao.apt_Brigadista == true)
                vRet = vRet + " nId_Aptidao = " + (int)Ilitera.Opsa.Data.Empregado_Aptidao.Aptidao.Brigadista + " or ";

            if (vAptidao.apt_PPR == true)
                vRet = vRet + " nId_Aptidao = " + (int)Ilitera.Opsa.Data.Empregado_Aptidao.Aptidao.PPR + " or ";

            if (vAptidao.apt_Socorrista == true)
                vRet = vRet + " nId_Aptidao = " + (int)Ilitera.Opsa.Data.Empregado_Aptidao.Aptidao.Socorrista + " or ";

            if (vAptidao.apt_Radiacao == true)
                vRet = vRet + " nId_Aptidao = " + (int)Ilitera.Opsa.Data.Empregado_Aptidao.Aptidao.RadiacaoIonizante + " or ";

            if (vAptidao.apt_Trabalho_Bordo== true)
                vRet = vRet + " nId_Aptidao = " + (int)Ilitera.Opsa.Data.Empregado_Aptidao.Aptidao.Trabalho_Bordo + " or ";

            if (vRet != " AND  ( ")
            {
                vRet = vRet.Substring(0, vRet.Length - 3);
                ret.Append(vRet + " ) ");
            }

            
            if ((int)IndExameClinico.Admissional == this.IdExameDicionario.Id)
                ret.Append(" AND NaAdmissao=1");
            else if ((int)IndExameClinico.Demissional == this.IdExameDicionario.Id)
                ret.Append(" AND NaDemissao=1");
            else if ((int)IndExameClinico.MudancaDeFuncao == this.IdExameDicionario.Id)
                ret.Append(" AND NaMudancaFuncao=1");
            else if ((int)IndExameClinico.Periodico == this.IdExameDicionario.Id)
                ret.Append(" AND Periodico=1");
            else if ((int)IndExameClinico.RetornoAoTrabalho == this.IdExameDicionario.Id)
                ret.Append(" AND NoRetornoTrabalho=1");

            ret.Append(" AND IdExameDicionario <>" + (int)IndExameClinico.Periodico);
            //ret.Append(" AND IdExameDicionario NOT IN (SELECT IdExameDicionario FROM ExameDicionario WHERE IsObservacao = 1)");

            ret.Append(" ORDER BY (SELECT Nome FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.ExameDicionario WHERE ExameDicionario.IdExameDicionario = tblEmpregado_Aptidao_Planejamento.IdExameDicionario)");

            return ret.ToString();
        }



        private string GetWhereExamesComplementares_Mudanca_Funcao(Ghe ghe, string xTipo)
        {
            StringBuilder ret = new StringBuilder();

            ArrayList list2 = new Pcmso().Find(" IdLaudoTecnico = " + ghe.nID_LAUD_TEC.Id.ToString() + " order by DataPcmso Desc ");

            Pcmso zPcmso = new Pcmso();

            foreach (Pcmso xpcmso in list2)
            {
                zPcmso.Find(xpcmso.Id);

                if (xTipo == "Admissional")   //pegar primeiro PCMSO, o mais recente para admissional
                {
                    break;
                }

            }


            //zPcmso.Find("  IdLaudoTecnico = " + ghe.nID_LAUD_TEC.Id.ToString() + " ");

            //Mostra também os preventicos devido a Polengui Goiatuba
            ret.Append("IdPcmso=" + zPcmso.Id + " AND IdGhe=" + ghe.Id);

            if (xTipo == "Admissional")
                ret.Append(" AND NaAdmissao=1");
            else if (xTipo == "Demissional")
                ret.Append(" AND NaDemissao=1");


            ret.Append(" AND IdExameDicionario <>" + (int)IndExameClinico.Periodico);
            ret.Append(" AND IdExameDicionario NOT IN (SELECT IdExameDicionario FROM ExameDicionario WHERE IsObservacao = 1)");

            ret.Append(" ORDER BY (SELECT Nome FROM ExameDicionario WHERE ExameDicionario.IdExameDicionario = PcmsoPlanejamento.IdExameDicionario)");

            return ret.ToString();
        }


        #endregion

        #endregion

        #region GetExamesComplementares

        public DataSet GetExamesComplementares()
        {
            DataRow newRow;

            DataSet ds = new DataSet();
            ds.Tables.Add(GetDataTableExamesComplementares());

            foreach (ExameComplementar complementar in complementares)
            {
                newRow = ds.Tables[0].NewRow();

                newRow["Exame"] = complementar.Exame;
                newRow["Preventivo"] = complementar.Preventivo ? "X" : "";
                newRow["Data"] = complementar.dataUltimo != new DateTime() ? complementar.dataUltimo.ToString("dd-MM-yyyy") : string.Empty;
                newRow["Periodicidade"] = complementar.Periodicidade;

                ds.Tables[0].Rows.Add(newRow);
            }

            DataRow[] rows = ds.Tables[0].Select("Exame=Exame", "Exame");

            DataSet retDs = new DataSet();
            retDs.Merge(rows);

            if (retDs.Tables.Count == 0)
                retDs.Tables.Add(GetDataTableExamesComplementares());

            return retDs;
        }

        private static DataTable GetDataTableExamesComplementares()
        {
            DataTable table = new DataTable("Result");
            table.Columns.Add("Exame", Type.GetType("System.String"));
            table.Columns.Add("Data", Type.GetType("System.String"));
            table.Columns.Add("Preventivo", Type.GetType("System.String"));
            table.Columns.Add("Periodicidade", Type.GetType("System.String"));
            return table;
        }
        #endregion

        #region GetExamesComplementaresSomentePreventivos

        public DataSet GetExamesComplementaresSomentePreventivos()
        {
            DataRow newRow;

            DataSet ds = new DataSet();
            ds.Tables.Add(GetDataTableExamesComplementares());

            foreach (ExameComplementar complementar in ExamesComplementaresPreventivos())
            {
                newRow = ds.Tables[0].NewRow();

                newRow["Exame"] = complementar.Exame;
                newRow["Preventivo"] = complementar.Preventivo ? "X" : "";
                newRow["Data"] = complementar.dataUltimo != new DateTime() ? complementar.dataUltimo.ToString("dd-MM-yyyy") : string.Empty;
                newRow["Periodicidade"] = complementar.Periodicidade;

                ds.Tables[0].Rows.Add(newRow);
            }

            DataRow[] rows = ds.Tables[0].Select("Exame=Exame", "Exame");

            DataSet retDs = new DataSet();
            retDs.Merge(rows);

            return retDs;
        }
        #endregion

        #region ExamesComplementaresPreventivos

        public System.Collections.IEnumerable ExamesComplementaresPreventivos()
        {
            foreach (ExameComplementar complementar in complementares)
                if (complementar.Preventivo)
                    yield return complementar;
        }
        #endregion

        #region ExamesComplementaresNaoCadastrados

        public System.Collections.IEnumerable ExamesComplementaresNaoCadastrados()
        {
            foreach (ExameComplementar complementar in complementares)
                if (complementar.dataUltimo == new DateTime())
                    yield return complementar;
        }
        #endregion

        #region PossueExamesComplementaresNaoCadastrados

        public bool PossueExamesComplementaresNaoCadastrados()
        {
            bool ret = false;

            foreach (ExameComplementar complementar in this.ExamesComplementaresNaoCadastrados())
            {
                ret = true;
                break;
            }

            return ret;
        }
        #endregion




        #region GetPlanejamentoExamesAso


        public string GetPlanejamentoExamesAso_Formatado_Mudanca_Funcao(Ghe ghe1, Ghe ghe2, bool ExamesSomentePeriodoPcmso, bool Exibir_Datas, string xDataBranco, bool xDesconsiderarCompl)
        {
            StringBuilder ret = new StringBuilder();

            string xExame;
            string xEspacos;

            int xMeses = 0;

            bool TemPreventivo = false;

            int i = 1;

            string quebraLinha;

            quebraLinha = "\n";

            xEspacos = "                                                ";
            xExame = "";

            complementares = ExamesComplementares_Mudanca_Funcao(ghe1, ghe2, ExamesSomentePeriodoPcmso);

            foreach (ExameComplementar complementar in complementares)
            {
                //toxicológico - não sair no ASO
                if (complementar.IdExameDicionario == 100)
                {
                    continue;
                }

                //Preventivo somente se tiver exames feitos
                if (complementar.Preventivo)
                {
                    if (complementar.dataUltimo != new DateTime())
                    {
                        //ret.Append(i++.ToString()
                        //            + ". "
                        //            + complementar.Exame
                        //            + " - "
                        //            + complementar.dataUltimo.ToString("dd-MM-yyyy")
                        //            + " *"
                        //            + quebraLinha);
                        xExame = complementar.Exame.Trim();

                        if (xExame.Length > 0)
                        {
                            if (xExame.Length < xEspacos.Length)
                            {
                                xExame = xExame + xEspacos.Substring(1, xEspacos.Length - xExame.Length);
                            }

                            xMeses = -12;

                            IdExameDicionario.Find();

                            if (IdExameDicionario.Descricao.ToString() == "Periódico" || IdExameDicionario.Descricao.ToString() == "Mudança de Função")
                            {
                                if (complementar.Periodicidade.IndexOf("Periódico (2 anos)") >= 0)
                                {
                                    xMeses = -24;
                                }
                                else if (complementar.Periodicidade.IndexOf("Periódico (3 anos)") >= 0)
                                {
                                    xMeses = -36;
                                }
                                else if (complementar.Periodicidade.IndexOf("Periódico (4 anos)") >= 0)
                                {
                                    xMeses = -48;
                                }
                                else if (complementar.Periodicidade.IndexOf("Periódico (5 anos)") >= 0)
                                {
                                    xMeses = -60;
                                }
                                else if (complementar.Periodicidade.IndexOf("Anual") > 0)
                                {
                                    xMeses = -12;
                                }
                                else if (complementar.Periodicidade.IndexOf("6 meses") > 0 || complementar.Periodicidade.ToUpper().IndexOf("SEMESTRAL") > 0)
                                {
                                    xMeses = -6;
                                }
                            }

                            this.IdEmpregado.Find();

                            if (complementar.Periodicidade.ToUpper().IndexOf("6 MESES DA ADMISSÃO") > 0)
                            {
                                if (this.DataExame <= this.IdEmpregado.hDT_ADM.AddMonths(6))
                                {
                                    xMeses = -6;
                                }
                                else if (this.DataExame > this.IdEmpregado.hDT_ADM.AddMonths(6) && this.DataExame <= this.IdEmpregado.hDT_ADM.AddMonths(12))
                                {
                                    if (complementar.dataUltimo >= this.IdEmpregado.hDT_ADM.AddMonths(6))
                                        xMeses = -12;
                                    else
                                        xMeses = -6;
                                }
                                else if (this.DataExame > this.IdEmpregado.hDT_ADM.AddMonths(12) && this.DataExame <= this.IdEmpregado.hDT_ADM.AddMonths(18))
                                {
                                    if (complementar.dataUltimo < this.IdEmpregado.hDT_ADM.AddMonths(6))
                                        xMeses = -6;
                                    else
                                        xMeses = -12;
                                }
                            }


                            //audiometria - regra única - 120 dias
                            //this.IdExameDicionario.Find();
                            //if (this.IdExameDicionario.Id == 2 && xExame.ToUpper().Trim() == "AUDIOMETRIA")
                            //{
                            //    if (complementar.dataUltimo.AddDays(120) < this.DataExame)
                            //    {
                            //        string zData = "  /  /    ";
                            //        xExame = xExame.Substring(0, 38) + zData;
                            //    }
                            //    else
                            //    {
                            //        System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");
                            //        string zData = complementar.dataUltimo.ToString("dd/MM/yyyy");
                            //        xExame = xExame.Substring(0, 38) + zData;
                            //    }
                            //}
                            //else 
                            if (complementar.dataUltimo >= this.DataExame.AddMonths(xMeses))
                            {
                                string zData;

                                //if (Exibir_Datas == true) zData = complementar.dataUltimo.ToString("dd/MM/yyyy");
                                //else zData = "  /  /    ";

                                if (Exibir_Datas == true)
                                {

                                    if (xDataBranco != "" && xDataBranco != "  /  /    ")
                                    {
                                        System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");

                                        if (xDesconsiderarCompl == false)
                                        {
                                            if (complementar.dataUltimo < System.Convert.ToDateTime(xDataBranco, ptBr))
                                                zData = "  /  /    ";
                                            else
                                                zData = complementar.dataUltimo.ToString("dd/MM/yyyy");
                                        }
                                        else
                                        {
                                            //if (System.Convert.ToDateTime(xDataBranco, ptBr) <= complementar.dataUltimo)
                                            if (this.DataExame.Date >= System.Convert.ToDateTime(xDataBranco, ptBr))
                                                zData = "  /  /    ";
                                            else
                                                zData = complementar.dataUltimo.ToString("dd/MM/yyyy");
                                        }
                                    }
                                    else
                                    {
                                        zData = complementar.dataUltimo.ToString("dd/MM/yyyy");
                                    }
                                }
                                else
                                {
                                    zData = "  /  /    ";
                                }

                                xExame = xExame.Substring(0, 38) + zData;
                            }
                            else
                            {
                                if (xExame.Substring(39, 4) == "    ")
                                {
                                    xExame = xExame.Substring(0, 38) + "/ " + xExame.Substring(41, 2) + " /    "; //"/" + xExame.Substring(43);
                                }
                                else
                                {
                                    xExame = xExame + "";
                                }

                            }

                        }

                        //ret.Append(i++.ToString()
                        //            + ". "
                        //            + xExame + "_"
                        //            + " *"
                        //            + quebraLinha);

                        if (i < 10)
                        {
                            ret.Append(i++.ToString()
                                        + ". "
                                        + xExame);
                        }
                        else
                        {
                            ret.Append(i++.ToString()
                                        + "."
                                        + xExame);
                        }

                        if (i <= 10) ret.Append("   ");
                        else ret.Append("  ");

                        if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("GLOBAL") > 0)
                        {
                            if (i % 2 == 1 || complementares.Count <= 12)
                            {
                                ret.Append(quebraLinha);
                            }
                            else
                            {
                                ret.Append("   ");
                            }
                        }
                        else
                        {
                            if (i % 2 == 1 || complementares.Count <= 9)
                            {
                                ret.Append(quebraLinha);
                            }
                            else
                            {
                                ret.Append("   ");
                            }
                        }


                        TemPreventivo = true;
                    }
                    else if (!this.DescExameComplPreventivo)
                    {
                        //ret.Append(i++.ToString()
                        //            + ". "
                        //            + complementar.Exame
                        //            + "  ___/___/______"
                        //            + " *"
                        //            + quebraLinha);
                        xExame = complementar.Exame.Trim();

                        if (xExame.Length > 0)
                        {
                            if (xExame.Length < xEspacos.Length)
                            {
                                xExame = xExame + xEspacos.Substring(1, xEspacos.Length - xExame.Length);
                            }
                            
                            xMeses = -12;

                            IdExameDicionario.Find();

                            if (IdExameDicionario.Descricao.ToString() == "Periódico" || IdExameDicionario.Descricao.ToString() == "Mudança de Função")
                            {
                                if (complementar.Periodicidade.IndexOf("Periódico (2 anos)") >= 0)
                                {
                                    xMeses = -24;
                                }
                                else if (complementar.Periodicidade.IndexOf("Periódico (3 anos)") >= 0)
                                {
                                    xMeses = -36;
                                }
                                else if (complementar.Periodicidade.IndexOf("Periódico (4 anos)") >= 0)
                                {
                                    xMeses = -48;
                                }
                                else if (complementar.Periodicidade.IndexOf("Periódico (5 anos)") >= 0)
                                {
                                    xMeses = -60;
                                }
                                else if (complementar.Periodicidade.IndexOf("Anual") > 0)
                                {
                                    xMeses = -12;
                                }
                                else if (complementar.Periodicidade.IndexOf("6 meses") > 0 || complementar.Periodicidade.ToUpper().IndexOf("SEMESTRAL") > 0)
                                {
                                    xMeses = -6;
                                }
                            }


                            this.IdEmpregado.Find();

                            if (complementar.Periodicidade.ToUpper().IndexOf("6 MESES DA ADMISSÃO") > 0)
                            {
                                if (this.DataExame <= this.IdEmpregado.hDT_ADM.AddMonths(6))
                                {
                                    xMeses = -6;
                                }
                                else if (this.DataExame > this.IdEmpregado.hDT_ADM.AddMonths(6) && this.DataExame <= this.IdEmpregado.hDT_ADM.AddMonths(12))
                                {
                                    if (complementar.dataUltimo >= this.IdEmpregado.hDT_ADM.AddMonths(6))
                                        xMeses = -12;
                                    else
                                        xMeses = -6;
                                }
                                else if (this.DataExame > this.IdEmpregado.hDT_ADM.AddMonths(12) && this.DataExame <= this.IdEmpregado.hDT_ADM.AddMonths(18))
                                {
                                    if (complementar.dataUltimo < this.IdEmpregado.hDT_ADM.AddMonths(6))
                                        xMeses = -6;
                                    else
                                        xMeses = -12;
                                }
                            }


                            //audiometria - regra única - 120 dias
                            //this.IdExameDicionario.Find();
                            //if (this.IdExameDicionario.Id == 2 && xExame.ToUpper().Trim() == "AUDIOMETRIA")
                            //{
                            //    if (complementar.dataUltimo.AddDays(120) < this.DataExame)
                            //    {
                            //        string zData = "  /  /    ";
                            //        xExame = xExame.Substring(0, 38) + zData;
                            //    }
                            //    else
                            //    {
                            //        System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");
                            //        string zData = complementar.dataUltimo.ToString("dd/MM/yyyy");
                            //        xExame = xExame.Substring(0, 38) + zData;
                            //    }
                            //}
                            //else 
                            if (complementar.dataUltimo >= this.DataExame.AddMonths(xMeses))
                            {
                                string zData;

                                //if (Exibir_Datas == true) zData = complementar.dataUltimo.ToString("dd/MM/yyyy");
                                //else zData = "  /  /    ";


                                if (Exibir_Datas == true)
                                {

                                    if (xDataBranco != "" && xDataBranco != "  /  /    ")
                                    {
                                        System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");

                                        if (xDesconsiderarCompl == false)
                                        {
                                            if (complementar.dataUltimo < System.Convert.ToDateTime(xDataBranco, ptBr))
                                                zData = "  /  /    ";
                                            else
                                                zData = complementar.dataUltimo.ToString("dd/MM/yyyy");
                                        }
                                        else
                                        {
                                            //if (System.Convert.ToDateTime(xDataBranco, ptBr) <= complementar.dataUltimo)
                                            if (this.DataExame.Date >= System.Convert.ToDateTime(xDataBranco, ptBr))
                                                zData = "  /  /    ";
                                            else
                                                zData = complementar.dataUltimo.ToString("dd/MM/yyyy");
                                        }
                                    }
                                    else
                                    {
                                        zData = complementar.dataUltimo.ToString("dd/MM/yyyy");
                                    }
                                }
                                else
                                {
                                    zData = "  /  /    ";
                                }

                                xExame = xExame.Substring(0, 38) + zData;
                            }
                            else
                            {
                                if (xExame.Substring(39, 4) == "    ")
                                {
                                    xExame = xExame.Substring(0, 38) + "/ " + xExame.Substring(41, 2) + " /    "; //+ xExame.Substring(43);
                                }
                                else
                                {
                                    xExame = xExame + "";
                                }
                            }

                        }


                        if (i < 10)
                        {
                            ret.Append(i++.ToString()
                                        + ". "
                                        + xExame);
                        }
                        else
                        {
                            ret.Append(i++.ToString()
                                        + "."
                                        + xExame);
                        }

                        if (i <= 10) ret.Append("   ");
                        else ret.Append("  ");

                        if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("GLOBAL") > 0)
                        {
                            if (i % 2 == 1 || complementares.Count <= 12)
                            {
                                ret.Append(quebraLinha);
                            }
                            else
                            {
                                ret.Append("   ");
                            }
                        }
                        else
                        {
                            if (i % 2 == 1 || complementares.Count <= 9)
                            {
                                ret.Append(quebraLinha);
                            }
                            else
                            {
                                ret.Append("   ");
                            }
                        }



                        TemPreventivo = true;
                    }
                }
                else
                {
                    //if (complementar.dataUltimo == new DateTime())
                    //    ret.Append(i++.ToString() + ". " + complementar.Exame + quebraLinha);
                    //else
                    //{
                    //ret.Append(i++.ToString()
                    //            + ". "
                    //            + complementar.Exame
                    //            + " - " + complementar.dataUltimo.ToString("dd-MM-yyyy")
                    //            + quebraLinha);


                    xExame = complementar.Exame.Trim();

                    if (xExame.Length > 0)
                    {
                        if (xExame.Length < xEspacos.Length)
                        {
                            xExame = xExame + xEspacos.Substring(1, xEspacos.Length - xExame.Length);
                        }

                        xMeses = -12;

                        IdExameDicionario.Find();

                        if (IdExameDicionario.Descricao.ToString() == "Periódico" || IdExameDicionario.Descricao.ToString() == "Mudança de Função")
                        {
                            if (complementar.Periodicidade.IndexOf("Periódico (2 anos)") >= 0)
                            {
                                xMeses = -24;
                            }
                            else if (complementar.Periodicidade.IndexOf("Periódico (3 anos)") >= 0)
                            {
                                xMeses = -36;
                            }
                            else if (complementar.Periodicidade.IndexOf("Periódico (4 anos)") >= 0)
                            {
                                xMeses = -48;
                            }
                            else if (complementar.Periodicidade.IndexOf("Periódico (5 anos)") >= 0)
                            {
                                xMeses = -60;
                            }
                            else if (complementar.Periodicidade.IndexOf("Anual") > 0)
                            {
                                xMeses = -12;
                            }
                            else if (complementar.Periodicidade.IndexOf("6 meses") > 0 || complementar.Periodicidade.ToUpper().IndexOf("SEMESTRAL") > 0)
                            {
                                xMeses = -6;
                            }

                        }

                        this.IdEmpregado.Find();

                        if (complementar.Periodicidade.ToUpper().IndexOf("6 MESES DA ADMISSÃO") > 0)
                        {
                            if (this.DataExame <= this.IdEmpregado.hDT_ADM.AddMonths(6))
                            {
                                xMeses = -6;
                            }
                            else if (this.DataExame > this.IdEmpregado.hDT_ADM.AddMonths(6) && this.DataExame <= this.IdEmpregado.hDT_ADM.AddMonths(12))
                            {
                                if (complementar.dataUltimo >= this.IdEmpregado.hDT_ADM.AddMonths(6))
                                    xMeses = -12;
                                else
                                    xMeses = -6;
                            }
                            else if (this.DataExame > this.IdEmpregado.hDT_ADM.AddMonths(12) && this.DataExame <= this.IdEmpregado.hDT_ADM.AddMonths(18))
                            {
                                if (complementar.dataUltimo < this.IdEmpregado.hDT_ADM.AddMonths(6))
                                    xMeses = -6;
                                else
                                    xMeses = -12;
                            }
                        }


                        //audiometria - regra única - 120 dias
                        //this.IdExameDicionario.Find();
                        //if (this.IdExameDicionario.Id == 2 && xExame.ToUpper().Trim() == "AUDIOMETRIA")
                        //{
                        //    if (complementar.dataUltimo.AddDays(120) < this.DataExame)
                        //    {
                        //        string zData = "  /  /    ";
                        //        xExame = xExame.Substring(0, 38) + zData;
                        //    }
                        //    else
                        //    {
                        //        System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");
                        //        string zData = complementar.dataUltimo.ToString("dd/MM/yyyy");
                        //        xExame = xExame.Substring(0, 38) + zData;
                        //    }
                        //}
                        //else 
                        if (complementar.dataUltimo >= this.DataExame.AddMonths(xMeses))
                        {
                            string zData;

                            //if (Exibir_Datas == true) zData = complementar.dataUltimo.ToString("dd/MM/yyyy");
                            //else zData = "  /  /    ";

                            if (Exibir_Datas == true)
                            {

                                if (xDataBranco != "" && xDataBranco != "  /  /    ")
                                {
                                    System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");

                                    if (xDesconsiderarCompl == false)
                                    {
                                        if (complementar.dataUltimo < System.Convert.ToDateTime(xDataBranco, ptBr))
                                            zData = "  /  /    ";
                                        else
                                            zData = complementar.dataUltimo.ToString("dd/MM/yyyy");
                                    }
                                    else
                                    {
                                        //if (System.Convert.ToDateTime(xDataBranco, ptBr) <= complementar.dataUltimo)
                                        if (this.DataExame.Date >= System.Convert.ToDateTime(xDataBranco, ptBr))
                                            zData = "  /  /    ";
                                        else
                                            zData = complementar.dataUltimo.ToString("dd/MM/yyyy");
                                    }
                                }
                                else
                                {
                                    zData = complementar.dataUltimo.ToString("dd/MM/yyyy");
                                }
                            }
                            else
                            {
                                zData = "  /  /    ";
                            }

                            xExame = xExame.Substring(0, 38) + zData;
                        }
                        else
                        {
                            if (xExame.Substring(39, 4) == "    ")
                            {
                                xExame = xExame.Substring(0, 38) + "/ " + xExame.Substring(41, 2) + " /    "; //+ xExame.Substring(43);
                            }
                            else
                            {
                                xExame = xExame + "";
                            }

                        }
                    }


                    //ret.Append(i++.ToString()
                    //            + ". "
                    //            + xExame + "_"
                    //            + quebraLinha);


                    if (i < 10)
                    {
                        ret.Append(i++.ToString()
                                    + ". "
                                    + xExame);
                    }
                    else
                    {
                        ret.Append(i++.ToString()
                                    + "."
                                    + xExame);
                    }


                    if (i <= 10) ret.Append("   ");
                    else ret.Append("  ");

                    if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("GLOBAL") > 0)
                    {
                        if (i % 2 == 1 || complementares.Count <= 12)
                        {
                            ret.Append(quebraLinha);
                        }
                        else
                        {
                            ret.Append("   ");
                        }
                    }
                    else
                    {
                        if (i % 2 == 1 || complementares.Count <= 9)
                        {
                            ret.Append(quebraLinha);
                        }
                        else
                        {
                            ret.Append("   ");
                        }
                    }





                    //}
                }

            }

            if (TemPreventivo)
            {
                if (complementares.Count > 9)
                {

                    if (i > 0 && i % 2 == 1) i = i + 2;

                    i = i / 2;
                    for (int zCont = i; zCont < 10; zCont++)
                        ret.Append(System.Environment.NewLine);

                    //ret.Append(System.Environment.NewLine + "* (este exame é uma exigência do cliente, sem relação com o seu risco ocupacional. Portanto, a sua realização não é obrigatória e caso exista alguma alteração, o mesmo não deverá ser levado em conta na sua aptidão laboral.)");   //por iniciativa da empresa)");
                    //ret.Append(System.Environment.NewLine + "* (exame realizado por solicitação do cliente, sem relação com risco ocupacional deste trabalhador, sua realização não é obrigatória e caso tenha resultado anormal, não deverá ser considerado na aptidão ou inaptidão objeto deste exame.)");
                }
                
            }


            return ret.ToString();
        }



        public string GetPlanejamentoExamesAso_Formatado_Mudanca_Funcao_Desconsiderar(Ghe ghe1, Ghe ghe2, bool ExamesSomentePeriodoPcmso, bool Exibir_Datas, Clinico clinico, int zDias_Desconsiderar)
        {
            StringBuilder ret = new StringBuilder();

            string xExame;
            string xEspacos;

            int xMeses = 0;

            bool TemPreventivo = false;

            int i = 1;

            string quebraLinha;

            quebraLinha = "\n";

            xEspacos = "                                                ";
            xExame = "";

            complementares = ExamesComplementares_Mudanca_Funcao(ghe1, ghe2, ExamesSomentePeriodoPcmso);


            string xDataBranco = "";
            bool xDesconsiderarCompl = false;

            //complementares = ExamesComplementares(ExamesSomentePeriodoPcmso);

            foreach (ExameComplementar complementar in complementares)
            {
                //toxicológico - não sair no ASO
                if (complementar.IdExameDicionario == 100)
                {
                    continue;
                }


                System.Globalization.CultureInfo ptBr2 = new System.Globalization.CultureInfo("pt-Br");

                ExamePlanejamento xPlanej = new ExamePlanejamento();
                clinico.IdEmpregado.Find();
                clinico.IdExameDicionario.Find();
                clinico.IdPcmso.Find();

                xPlanej.Find(" IdEmpregado =" + clinico.IdEmpregado.Id.ToString() + " and IdExameDicionario = " + complementar.IdExameDicionario.ToString() + " and IdPCMSO = " + clinico.IdPcmso.Id.ToString());

                if (xPlanej.Id == 0)
                {
                    xDataBranco = clinico.DataExame.AddDays(zDias_Desconsiderar * (-1)).ToString("dd/MM/yyyy", ptBr2);
                }
                else
                {
                    if (xPlanej.DataVencimento == new DateTime())
                        xDataBranco = this.DataExame.ToString("dd/MM/yyyy", ptBr2);  //"  /  /    ";
                    else
                        xDataBranco = xPlanej.DataVencimento.AddDays(zDias_Desconsiderar * (-1)).ToString("dd/MM/yyyy", ptBr2);
                }

                xDesconsiderarCompl = true;

                //Preventivo somente se tiver exames feitos
                if (complementar.Preventivo)
                {
                    if (complementar.dataUltimo != new DateTime())
                    {
                        //ret.Append(i++.ToString()
                        //            + ". "
                        //            + complementar.Exame
                        //            + " - "
                        //            + complementar.dataUltimo.ToString("dd-MM-yyyy")
                        //            + " *"
                        //            + quebraLinha);
                        xExame = complementar.Exame.Trim();

                        if (xExame.Length > 0)
                        {
                            if (xExame.Length < xEspacos.Length)
                            {
                                xExame = xExame + xEspacos.Substring(1, xEspacos.Length - xExame.Length);
                            }

                            xMeses = -12;

                            IdExameDicionario.Find();

                            if (IdExameDicionario.Descricao.ToString() == "Periódico" || IdExameDicionario.Descricao.ToString() == "Mudança de Função")
                            {
                                if (complementar.Periodicidade.IndexOf("Periódico (2 anos)") >= 0)
                                {
                                    xMeses = -24;
                                }
                                else if (complementar.Periodicidade.IndexOf("Periódico (3 anos)") >= 0)
                                {
                                    xMeses = -36;
                                }
                                else if (complementar.Periodicidade.IndexOf("Periódico (4 anos)") >= 0)
                                {
                                    xMeses = -48;
                                }
                                else if (complementar.Periodicidade.IndexOf("Periódico (5 anos)") >= 0)
                                {
                                    xMeses = -60;
                                }
                                else if (complementar.Periodicidade.IndexOf("Anual") > 0)
                                {
                                    xMeses = -12;
                                }
                                else if (complementar.Periodicidade.IndexOf("6 meses") > 0 || complementar.Periodicidade.ToUpper().IndexOf("SEMESTRAL") > 0)
                                {
                                    xMeses = -6;
                                }
                            }

                            this.IdEmpregado.Find();

                            if (complementar.Periodicidade.ToUpper().IndexOf("6 MESES DA ADMISSÃO") > 0)
                            {
                                if (this.DataExame <= this.IdEmpregado.hDT_ADM.AddMonths(6))
                                {
                                    xMeses = -6;
                                }
                                else if (this.DataExame > this.IdEmpregado.hDT_ADM.AddMonths(6) && this.DataExame <= this.IdEmpregado.hDT_ADM.AddMonths(12))
                                {
                                    if (complementar.dataUltimo >= this.IdEmpregado.hDT_ADM.AddMonths(6))
                                        xMeses = -12;
                                    else
                                        xMeses = -6;
                                }
                                else if (this.DataExame > this.IdEmpregado.hDT_ADM.AddMonths(12) && this.DataExame <= this.IdEmpregado.hDT_ADM.AddMonths(18))
                                {
                                    if (complementar.dataUltimo < this.IdEmpregado.hDT_ADM.AddMonths(6))
                                        xMeses = -6;
                                    else
                                        xMeses = -12;
                                }
                            }


                            //audiometria - regra única - 120 dias
                            //this.IdExameDicionario.Find();
                            //if (this.IdExameDicionario.Id == 2 && xExame.ToUpper().Trim() == "AUDIOMETRIA")
                            //{
                            //    if (complementar.dataUltimo.AddDays(120) < this.DataExame)
                            //    {
                            //        string zData = "  /  /    ";
                            //        xExame = xExame.Substring(0, 38) + zData;
                            //    }
                            //    else
                            //    {
                            //        System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");
                            //        string zData = complementar.dataUltimo.ToString("dd/MM/yyyy");
                            //        xExame = xExame.Substring(0, 38) + zData;
                            //    }
                            //}
                            //else 
                            if (complementar.dataUltimo >= this.DataExame.AddMonths(xMeses))
                            {
                                string zData;

                                //if (Exibir_Datas == true) zData = complementar.dataUltimo.ToString("dd/MM/yyyy");
                                //else zData = "  /  /    ";

                                if (Exibir_Datas == true)
                                {

                                    if (xDataBranco != "" && xDataBranco != "  /  /    ")
                                    {
                                        System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");

                                        if (xDesconsiderarCompl == false)
                                        {
                                            if (complementar.dataUltimo < System.Convert.ToDateTime(xDataBranco, ptBr))
                                                zData = "  /  /    ";
                                            else
                                                zData = complementar.dataUltimo.ToString("dd/MM/yyyy");
                                        }
                                        else
                                        {
                                            //if (System.Convert.ToDateTime(xDataBranco, ptBr) <= complementar.dataUltimo)
                                            if (this.DataExame.Date >= System.Convert.ToDateTime(xDataBranco, ptBr))
                                                zData = "  /  /    ";
                                            else
                                                zData = complementar.dataUltimo.ToString("dd/MM/yyyy");
                                        }
                                    }
                                    else
                                    {
                                        zData = complementar.dataUltimo.ToString("dd/MM/yyyy");
                                    }
                                }
                                else
                                {
                                    zData = "  /  /    ";
                                }

                                xExame = xExame.Substring(0, 38) + zData;
                            }
                            else
                            {
                                if (xExame.Substring(39, 4) == "    ")
                                {
                                    xExame = xExame.Substring(0, 38) + "/ " + xExame.Substring(41, 2) + " /    "; //"/" + xExame.Substring(43);
                                }
                                else
                                {
                                    xExame = xExame + "";
                                }

                            }

                        }

                        //ret.Append(i++.ToString()
                        //            + ". "
                        //            + xExame + "_"
                        //            + " *"
                        //            + quebraLinha);

                        if (i < 10)
                        {
                            ret.Append(i++.ToString()
                                        + ". "
                                        + xExame);
                        }
                        else
                        {
                            ret.Append(i++.ToString()
                                        + "."
                                        + xExame);
                        }

                        if (i <= 10) ret.Append("   ");
                        else ret.Append("  ");

                        if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("GLOBAL") > 0)
                        {
                            if (i % 2 == 1 || complementares.Count <= 12)
                            {
                                ret.Append(quebraLinha);
                            }
                            else
                            {
                                ret.Append("   ");
                            }
                        }
                        else
                        {
                            if (i % 2 == 1 || complementares.Count <= 9)
                            {
                                ret.Append(quebraLinha);
                            }
                            else
                            {
                                ret.Append("   ");
                            }
                        }


                        TemPreventivo = true;
                    }
                    else if (!this.DescExameComplPreventivo)
                    {
                        //ret.Append(i++.ToString()
                        //            + ". "
                        //            + complementar.Exame
                        //            + "  ___/___/______"
                        //            + " *"
                        //            + quebraLinha);
                        xExame = complementar.Exame.Trim();

                        if (xExame.Length > 0)
                        {
                            if (xExame.Length < xEspacos.Length)
                            {
                                xExame = xExame + xEspacos.Substring(1, xEspacos.Length - xExame.Length);
                            }

                            xMeses = -12;

                            IdExameDicionario.Find();

                            if (IdExameDicionario.Descricao.ToString() == "Periódico" || IdExameDicionario.Descricao.ToString() == "Mudança de Função")
                            {
                                if (complementar.Periodicidade.IndexOf("Periódico (2 anos)") >= 0)
                                {
                                    xMeses = -24;
                                }
                                else if (complementar.Periodicidade.IndexOf("Periódico (3 anos)") >= 0)
                                {
                                    xMeses = -36;
                                }
                                else if (complementar.Periodicidade.IndexOf("Periódico (4 anos)") >= 0)
                                {
                                    xMeses = -48;
                                }
                                else if (complementar.Periodicidade.IndexOf("Periódico (5 anos)") >= 0)
                                {
                                    xMeses = -60;
                                }
                                else if (complementar.Periodicidade.IndexOf("Anual") > 0)
                                {
                                    xMeses = -12;
                                }
                                else if (complementar.Periodicidade.IndexOf("6 meses") > 0 || complementar.Periodicidade.ToUpper().IndexOf("SEMESTRAL") > 0)
                                {
                                    xMeses = -6;
                                }
                            }


                            this.IdEmpregado.Find();

                            if (complementar.Periodicidade.ToUpper().IndexOf("6 MESES DA ADMISSÃO") > 0)
                            {
                                if (this.DataExame <= this.IdEmpregado.hDT_ADM.AddMonths(6))
                                {
                                    xMeses = -6;
                                }
                                else if (this.DataExame > this.IdEmpregado.hDT_ADM.AddMonths(6) && this.DataExame <= this.IdEmpregado.hDT_ADM.AddMonths(12))
                                {
                                    if (complementar.dataUltimo >= this.IdEmpregado.hDT_ADM.AddMonths(6))
                                        xMeses = -12;
                                    else
                                        xMeses = -6;
                                }
                                else if (this.DataExame > this.IdEmpregado.hDT_ADM.AddMonths(12) && this.DataExame <= this.IdEmpregado.hDT_ADM.AddMonths(18))
                                {
                                    if (complementar.dataUltimo < this.IdEmpregado.hDT_ADM.AddMonths(6))
                                        xMeses = -6;
                                    else
                                        xMeses = -12;
                                }
                            }

                            //audiometria - regra única - 120 dias
                            //this.IdExameDicionario.Find();
                            //if (this.IdExameDicionario.Id == 2 && xExame.ToUpper().Trim() == "AUDIOMETRIA")
                            //{
                            //    if (complementar.dataUltimo.AddDays(120) < this.DataExame)
                            //    {
                            //        string zData = "  /  /    ";
                            //        xExame = xExame.Substring(0, 38) + zData;
                            //    }
                            //    else
                            //    {
                            //        System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");
                            //        string zData = complementar.dataUltimo.ToString("dd/MM/yyyy");
                            //        xExame = xExame.Substring(0, 38) + zData;
                            //    }
                            //}
                            //else 
                            if (complementar.dataUltimo >= this.DataExame.AddMonths(xMeses))
                            {
                                string zData;

                                //if (Exibir_Datas == true) zData = complementar.dataUltimo.ToString("dd/MM/yyyy");
                                //else zData = "  /  /    ";


                                if (Exibir_Datas == true)
                                {

                                    if (xDataBranco != "" && xDataBranco != "  /  /    ")
                                    {
                                        System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");

                                        if (xDesconsiderarCompl == false)
                                        {
                                            if (complementar.dataUltimo < System.Convert.ToDateTime(xDataBranco, ptBr))
                                                zData = "  /  /    ";
                                            else
                                                zData = complementar.dataUltimo.ToString("dd/MM/yyyy");
                                        }
                                        else
                                        {
                                            //if (System.Convert.ToDateTime(xDataBranco, ptBr) <= complementar.dataUltimo)
                                            if (this.DataExame.Date >= System.Convert.ToDateTime(xDataBranco, ptBr))
                                                zData = "  /  /    ";
                                            else
                                                zData = complementar.dataUltimo.ToString("dd/MM/yyyy");
                                        }
                                    }
                                    else
                                    {
                                        zData = complementar.dataUltimo.ToString("dd/MM/yyyy");
                                    }
                                }
                                else
                                {
                                    zData = "  /  /    ";
                                }

                                xExame = xExame.Substring(0, 38) + zData;
                            }
                            else
                            {
                                if (xExame.Substring(39, 4) == "    ")
                                {
                                    xExame = xExame.Substring(0, 38) + "/ " + xExame.Substring(41, 2) + " /    "; //+ xExame.Substring(43);
                                }
                                else
                                {
                                    xExame = xExame + "";
                                }
                            }

                        }


                        if (i < 10)
                        {
                            ret.Append(i++.ToString()
                                        + ". "
                                        + xExame);
                        }
                        else
                        {
                            ret.Append(i++.ToString()
                                        + "."
                                        + xExame);
                        }


                        if (i <= 10) ret.Append("   ");
                        else ret.Append("  ");

                        if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("GLOBAL") > 0)
                        {
                            if (i % 2 == 1 || complementares.Count <= 12)
                            {
                                ret.Append(quebraLinha);
                            }
                            else
                            {
                                ret.Append("   ");
                            }
                        }
                        else
                        {
                            if (i % 2 == 1 || complementares.Count <= 9)
                            {
                                ret.Append(quebraLinha);
                            }
                            else
                            {
                                ret.Append("   ");
                            }
                        }


                        TemPreventivo = true;
                    }
                }
                else
                {
                    //if (complementar.dataUltimo == new DateTime())
                    //    ret.Append(i++.ToString() + ". " + complementar.Exame + quebraLinha);
                    //else
                    //{
                    //ret.Append(i++.ToString()
                    //            + ". "
                    //            + complementar.Exame
                    //            + " - " + complementar.dataUltimo.ToString("dd-MM-yyyy")
                    //            + quebraLinha);


                    xExame = complementar.Exame.Trim();

                    if (xExame.Length > 0)
                    {
                        if (xExame.Length < xEspacos.Length)
                        {
                            xExame = xExame + xEspacos.Substring(1, xEspacos.Length - xExame.Length);
                        }

                        xMeses = -12;

                        IdExameDicionario.Find();

                        if (IdExameDicionario.Descricao.ToString() == "Periódico" || IdExameDicionario.Descricao.ToString() == "Mudança de Função")
                        {
                            if (complementar.Periodicidade.IndexOf("Periódico (2 anos)") >= 0)
                            {
                                xMeses = -24;
                            }
                            else if (complementar.Periodicidade.IndexOf("Periódico (3 anos)") >= 0)
                            {
                                xMeses = -36;
                            }
                            else if (complementar.Periodicidade.IndexOf("Periódico (4 anos)") >= 0)
                            {
                                xMeses = -48;
                            }
                            else if (complementar.Periodicidade.IndexOf("Periódico (5 anos)") >= 0)
                            {
                                xMeses = -60;
                            }
                            else if (complementar.Periodicidade.IndexOf("Anual") > 0)
                            {
                                xMeses = -12;
                            }
                            else if (complementar.Periodicidade.IndexOf("6 meses") > 0 || complementar.Periodicidade.ToUpper().IndexOf("SEMESTRAL") > 0)
                            {
                                xMeses = -6;
                            }

                        }


                        this.IdEmpregado.Find();

                        if (complementar.Periodicidade.ToUpper().IndexOf("6 MESES DA ADMISSÃO") > 0)
                        {
                            if (this.DataExame <= this.IdEmpregado.hDT_ADM.AddMonths(6))
                            {
                                xMeses = -6;
                            }
                            else if (this.DataExame > this.IdEmpregado.hDT_ADM.AddMonths(6) && this.DataExame <= this.IdEmpregado.hDT_ADM.AddMonths(12))
                            {
                                if (complementar.dataUltimo >= this.IdEmpregado.hDT_ADM.AddMonths(6))
                                    xMeses = -12;
                                else
                                    xMeses = -6;
                            }
                            else if (this.DataExame > this.IdEmpregado.hDT_ADM.AddMonths(12) && this.DataExame <= this.IdEmpregado.hDT_ADM.AddMonths(18))
                            {
                                if (complementar.dataUltimo < this.IdEmpregado.hDT_ADM.AddMonths(6))
                                    xMeses = -6;
                                else
                                    xMeses = -12;
                            }
                        }



                        ////audiometria - regra única - 120 dias
                        //this.IdExameDicionario.Find();
                        //if (this.IdExameDicionario.Id == 2 && xExame.ToUpper().Trim() == "AUDIOMETRIA")
                        //{
                        //    if (complementar.dataUltimo.AddDays(120) < this.DataExame)
                        //    {
                        //        string zData = "  /  /    ";
                        //        xExame = xExame.Substring(0, 38) + zData;
                        //    }
                        //    else
                        //    {
                        //        System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");
                        //        string zData = complementar.dataUltimo.ToString("dd/MM/yyyy");
                        //        xExame = xExame.Substring(0, 38) + zData;
                        //    }
                        //}
                        //else 
                        if (complementar.dataUltimo >= this.DataExame.AddMonths(xMeses))
                        {
                            string zData;

                            //if (Exibir_Datas == true) zData = complementar.dataUltimo.ToString("dd/MM/yyyy");
                            //else zData = "  /  /    ";

                            if (Exibir_Datas == true)
                            {

                                if (xDataBranco != "" && xDataBranco != "  /  /    ")
                                {
                                    System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");

                                    if (xDesconsiderarCompl == false)
                                    {
                                        if (complementar.dataUltimo < System.Convert.ToDateTime(xDataBranco, ptBr))
                                            zData = "  /  /    ";
                                        else
                                            zData = complementar.dataUltimo.ToString("dd/MM/yyyy");
                                    }
                                    else
                                    {
                                        //if (System.Convert.ToDateTime(xDataBranco, ptBr) <= complementar.dataUltimo)
                                        if (this.DataExame.Date >= System.Convert.ToDateTime(xDataBranco, ptBr))
                                            zData = "  /  /    ";
                                        else
                                            zData = complementar.dataUltimo.ToString("dd/MM/yyyy");
                                    }
                                }
                                else
                                {
                                    zData = complementar.dataUltimo.ToString("dd/MM/yyyy");
                                }
                            }
                            else
                            {
                                zData = "  /  /    ";
                            }

                            xExame = xExame.Substring(0, 38) + zData;
                        }
                        else
                        {
                            if (xExame.Substring(39, 4) == "    ")
                            {
                                xExame = xExame.Substring(0, 38) + "/ " + xExame.Substring(41, 2) + " /    "; //+ xExame.Substring(43);
                            }
                            else
                            {
                                xExame = xExame + "";
                            }

                        }
                    }


                    //ret.Append(i++.ToString()
                    //            + ". "
                    //            + xExame + "_"
                    //            + quebraLinha);


                    if (i < 10)
                    {
                        ret.Append(i++.ToString()
                                    + ". "
                                    + xExame);
                    }
                    else
                    {
                        ret.Append(i++.ToString()
                                    + "."
                                    + xExame);
                    }


                    if (i <= 10) ret.Append("   ");
                    else ret.Append("  ");

                    if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("GLOBAL") > 0)
                    {
                        if (i % 2 == 1 || complementares.Count <= 12)
                        {
                            ret.Append(quebraLinha);
                        }
                        else
                        {
                            ret.Append("   ");
                        }
                    }
                    else
                    {
                        if (i % 2 == 1 || complementares.Count <= 9)
                        {
                            ret.Append(quebraLinha);
                        }
                        else
                        {
                            ret.Append("   ");
                        }
                    }





                    //}
                }

            }

            if (TemPreventivo)
            {
                if (complementares.Count > 9)
                {

                    if (i > 0 && i % 2 == 1) i = i + 2;

                    i = i / 2;
                    for (int zCont = i; zCont < 10; zCont++)
                        ret.Append(System.Environment.NewLine);

                    //ret.Append(System.Environment.NewLine + "* (este exame é uma exigência do cliente, sem relação com o seu risco ocupacional. Portanto, a sua realização não é obrigatória e caso exista alguma alteração, o mesmo não deverá ser levado em conta na sua aptidão laboral.)");   //por iniciativa da empresa)");
                    //ret.Append(System.Environment.NewLine + "* (exame realizado por solicitação do cliente, sem relação com risco ocupacional deste trabalhador, sua realização não é obrigatória e caso tenha resultado anormal, não deverá ser considerado na aptidão ou inaptidão objeto deste exame.)");
                }

            }


            return ret.ToString();
        }





        public string GetPlanejamentoExamesAso(Ghe ghe, bool ExamesSomentePeriodoPcmso)
        {
            StringBuilder ret = new StringBuilder();


            bool TemPreventivo = false;

            int i = 1;

            string quebraLinha;

            if (complementares.Count > 10)
                quebraLinha = "; ";
            else
                quebraLinha = "\n";

            
            complementares = ExamesComplementares(ExamesSomentePeriodoPcmso);

            foreach (ExameComplementar complementar in complementares)
            {
                //Preventivo somente se tiver exames feitos
                if (complementar.Preventivo)
                {
                    if (complementar.dataUltimo != new DateTime())
                    {
                        ret.Append(i++.ToString()
                                    + ". "
                                    + complementar.Exame
                                    + " - "
                                    + complementar.dataUltimo.ToString("dd-MM-yyyy")
                                    + "  "
                                    + quebraLinha);
                        TemPreventivo = true;
                    }
                    else if (!this.DescExameComplPreventivo)
                    {
                        ret.Append(i++.ToString()
                                    + ". "
                                    + complementar.Exame
                                    + "  ___/___/______"
                                    + "  "
                                    + quebraLinha);
                        TemPreventivo = true;
                    }
                }
                else
                {
                    if (complementar.dataUltimo == new DateTime())
                        ret.Append(i++.ToString() + ". " + complementar.Exame + quebraLinha);
                    else
                        ret.Append(i++.ToString()
                                    + ". "
                                    + complementar.Exame
                                    + " - " + complementar.dataUltimo.ToString("dd-MM-yyyy")
                                    + quebraLinha);


                }

            }

//            if (TemPreventivo)
//                ret.Append("* (por iniciativa da empresa)");

            if (TemPreventivo)
            {
                if (i > 0 && i % 2 == 1) i = i + 2;

                i = i / 2;
                for (int zCont = i; zCont < 10; zCont++)
                    ret.Append(System.Environment.NewLine);

                //ret.Append(System.Environment.NewLine + "* (este exame é uma exigência do cliente, sem relação com o seu risco ocupacional. Portanto, a sua realização não é obrigatória e caso exista alguma alteração, o mesmo não deverá ser levado em conta na sua aptidão laboral.)");   //por iniciativa da empresa)");
                //ret.Append(System.Environment.NewLine + "* (exame realizado por solicitação do cliente, sem relação com risco ocupacional deste trabalhador, sua realização não é obrigatória e caso tenha resultado anormal, não deverá ser considerado na aptidão ou inaptidão objeto deste exame.)");
            }

            return ret.ToString();
        }






        public string GetPlanejamentoExamesAso_Formatado_Mudanca_Funcao_Guia(Ghe ghe1, Ghe ghe2, bool ExamesSomentePeriodoPcmso)
        {
            StringBuilder ret = new StringBuilder();

            //string xExame;
            //string xEspacos;

            //int xMeses = 0;

            bool TemPreventivo = false;

            int i = 1;

            string quebraLinha;

            quebraLinha = "\n";

            //xEspacos = "                                                ";
            //xExame = "";

            complementares = ExamesComplementares_Mudanca_Funcao(ghe1, ghe2, ExamesSomentePeriodoPcmso);

            foreach (ExameComplementar complementar in complementares)
            {
                //Preventivo somente se tiver exames feitos
                if (complementar.Preventivo)
                {
                    if (complementar.dataUltimo != new DateTime())
                    {
                        ret.Append(i++.ToString()
                                    + ". "
                                    + complementar.Exame
                                    + " - "
                                    + complementar.dataUltimo.ToString("dd-MM-yyyy")
                                    + "  "
                                    + quebraLinha);
                        TemPreventivo = true;
                    }
                    else if (!this.DescExameComplPreventivo)
                    {
                        ret.Append(i++.ToString()
                                    + ". "
                                    + complementar.Exame
                                    + "  ___/___/______"
                                    + "  "
                                    + quebraLinha);
                        TemPreventivo = true;
                    }
                }
                else
                {
                    if (complementar.dataUltimo == new DateTime())
                        ret.Append(i++.ToString() + ". " + complementar.Exame + quebraLinha);
                    else
                        ret.Append(i++.ToString()
                                    + ". "
                                    + complementar.Exame
                                    + " - " + complementar.dataUltimo.ToString("dd-MM-yyyy")
                                    + quebraLinha);


                }

            }

            //            if (TemPreventivo)
            //                ret.Append("* (por iniciativa da empresa)");

            if (TemPreventivo)
            {
                if (i > 0 && i % 2 == 1) i = i + 2;

                i = i / 2;
                for (int zCont = i; zCont < 10; zCont++)
                    ret.Append(System.Environment.NewLine);

                //ret.Append(System.Environment.NewLine + "* (este exame é uma exigência do cliente, sem relação com o seu risco ocupacional. Portanto, a sua realização não é obrigatória e caso exista alguma alteração, o mesmo não deverá ser levado em conta na sua aptidão laboral.)");   //por iniciativa da empresa)");
                //ret.Append(System.Environment.NewLine + "* (exame realizado por solicitação do cliente, sem relação com risco ocupacional deste trabalhador, sua realização não é obrigatória e caso tenha resultado anormal, não deverá ser considerado na aptidão ou inaptidão objeto deste exame.)");
            }

            return ret.ToString();
        }



        public string GetPlanejamentoExamesAso_Guia(Ghe ghe, bool ExamesSomentePeriodoPcmso)
        {
            StringBuilder ret = new StringBuilder();


            bool TemPreventivo = false;

            int i = 1;

            string quebraLinha;

            if (complementares.Count > 10)
                quebraLinha = "; ";
            else
                quebraLinha = "\n";


            complementares = ExamesComplementares(ExamesSomentePeriodoPcmso);

            foreach (ExameComplementar complementar in complementares)
            {
                //toxicológico - não sair no ASO
                if (complementar.IdExameDicionario == 100)
                {
                    continue;
                }

                //Preventivo somente se tiver exames feitos
                if (complementar.Preventivo)
                {
                    if (complementar.dataUltimo != new DateTime())
                    {
                        ret.Append(i++.ToString()
                                    + ". "
                                    + complementar.Exame
                                    + quebraLinha);
                        TemPreventivo = true;
                    }
                    else if (!this.DescExameComplPreventivo)
                    {
                        ret.Append(i++.ToString()
                                    + ". "
                                    + complementar.Exame
                                    + quebraLinha);
                        TemPreventivo = true;
                    }
                }
                else
                {
                    if (complementar.dataUltimo == new DateTime())
                        ret.Append(i++.ToString() + ". " + complementar.Exame + quebraLinha);
                    else
                        ret.Append(i++.ToString()
                                    + ". "
                                    + complementar.Exame
                                    + quebraLinha);


                }

            }

//            if (TemPreventivo)
//                ret.Append("* (por iniciativa da empresa)");

            if (TemPreventivo)
            {
                if (i > 0 && i % 2 == 1) i = i + 2;

                i = i / 2;
                for (int zCont = i; zCont < 10; zCont++)
                    ret.Append(System.Environment.NewLine);

                //ret.Append(System.Environment.NewLine + "* (este exame é uma exigência do cliente, sem relação com o seu risco ocupacional. Portanto, a sua realização não é obrigatória e caso exista alguma alteração, o mesmo não deverá ser levado em conta na sua aptidão laboral.)");   //por iniciativa da empresa)");
                //ret.Append(System.Environment.NewLine + "* (exame realizado por solicitação do cliente, sem relação com risco ocupacional deste trabalhador, sua realização não é obrigatória e caso tenha resultado anormal, não deverá ser considerado na aptidão ou inaptidão objeto deste exame.)");
            }


            return ret.ToString();
        }




        public string GetPlanejamentoExamesAso_Guia(Ghe ghe, bool ExamesSomentePeriodoPcmso, string zTipo)
        {
            StringBuilder ret = new StringBuilder();


            bool TemPreventivo = false;

            int i = 1;
            
            string quebraLinha;

            if (complementares.Count > 10)
                quebraLinha = "; ";
            else
                quebraLinha = "\n";


            complementares = ExamesComplementares(ExamesSomentePeriodoPcmso, zTipo);

            foreach (ExameComplementar complementar in complementares)
            {
                //toxicológico - não sair no ASO
                if (complementar.IdExameDicionario == 100)
                {
                    continue;
                }

                //Preventivo somente se tiver exames feitos
                if (complementar.Preventivo)
                {
                    if (complementar.dataUltimo != new DateTime())
                    {
                        ret.Append(i++.ToString()
                                    + ". "
                                    + complementar.Exame
                                    + quebraLinha);
                        TemPreventivo = true;
                    }
                    else if (!this.DescExameComplPreventivo)
                    {
                        ret.Append(i++.ToString()
                                    + ". "
                                    + complementar.Exame
                                    + quebraLinha);
                        TemPreventivo = true;
                    }
                }
                else
                {
                    if (complementar.dataUltimo == new DateTime())
                        ret.Append(i++.ToString() + ". " + complementar.Exame + quebraLinha);
                    else
                        ret.Append(i++.ToString()
                                    + ". "
                                    + complementar.Exame
                                    + quebraLinha);


                }

            }

            //            if (TemPreventivo)
            //                ret.Append("* (por iniciativa da empresa)");

            if (TemPreventivo)
            {
                if (i > 0 && i % 2 == 1) i = i + 2;

                i = i / 2;
                for (int zCont = i; zCont < 10; zCont++)
                    ret.Append(System.Environment.NewLine);

                //ret.Append(System.Environment.NewLine + "* (este exame é uma exigência do cliente, sem relação com o seu risco ocupacional. Portanto, a sua realização não é obrigatória e caso exista alguma alteração, o mesmo não deverá ser levado em conta na sua aptidão laboral.)");   //por iniciativa da empresa)");
                //ret.Append(System.Environment.NewLine + "* (exame realizado por solicitação do cliente, sem relação com risco ocupacional deste trabalhador, sua realização não é obrigatória e caso tenha resultado anormal, não deverá ser considerado na aptidão ou inaptidão objeto deste exame.)");
            }


            return ret.ToString();
        }



        public string GetPlanejamentoExamesAso_Guia_Aptidao(Ilitera.Opsa.Data.Empregado_Aptidao zAptidao, bool Exibir_Datas, string xDataBranco, string xExamesOcupacionais)
        {
            StringBuilder ret = new StringBuilder();

            //string xExame;
            //string xEspacos;

            //int xMeses = 0;

            bool TemPreventivo = false;

            int i = 1;

            string quebraLinha;

            quebraLinha = "\n";

            //xEspacos = "                                                ";
            //xExame = "";

            //pegar numeração já existente de exames
            complementares = ExamesComplementares(true);
            if (complementares.Count > 0)
            {
                //i = i + complementares.Count;
                for (int fCont = 0; fCont < complementares.Count; fCont++)
                {
                    if (complementares[fCont].IdExameDicionario != 100) //não considerar toxicológico
                    {
                        i = i + 1;
                    }
                }
            }


            complementares = ExamesComplementares_Aptidao(zAptidao);

            foreach (ExameComplementar complementar in complementares)
            {

                if (xExamesOcupacionais.IndexOf(complementar.Exame) < 0)
                {

                    //Preventivo somente se tiver exames feitos
                    if (complementar.Preventivo)
                    {
                        if (complementar.dataUltimo != new DateTime())
                        {
                            ret.Append(i++.ToString()
                                        + ". "
                                        + complementar.Exame
                                        + quebraLinha);
                            TemPreventivo = true;
                        }
                        else if (!this.DescExameComplPreventivo)
                        {
                            ret.Append(i++.ToString()
                                        + ". "
                                        + complementar.Exame
                                        + quebraLinha);
                            TemPreventivo = true;
                        }
                    }
                    else
                    {
                        if (complementar.dataUltimo == new DateTime())
                            ret.Append(i++.ToString() + ". " + complementar.Exame + quebraLinha);
                        else
                            ret.Append(i++.ToString()
                                        + ". "
                                        + complementar.Exame
                                        + quebraLinha);


                    }
                }

            }

            //            if (TemPreventivo)
            //                ret.Append("* (por iniciativa da empresa)");

            if (TemPreventivo)
            {
                if (i > 0 && i % 2 == 1) i = i + 2;

                i = i / 2;
                for (int zCont = i; zCont < 10; zCont++)
                    ret.Append(System.Environment.NewLine);

                //ret.Append(System.Environment.NewLine + "* (este exame é uma exigência do cliente, sem relação com o seu risco ocupacional. Portanto, a sua realização não é obrigatória e caso exista alguma alteração, o mesmo não deverá ser levado em conta na sua aptidão laboral.)");   //por iniciativa da empresa)");
                //ret.Append(System.Environment.NewLine + "* (exame realizado por solicitação do cliente, sem relação com risco ocupacional deste trabalhador, sua realização não é obrigatória e caso tenha resultado anormal, não deverá ser considerado na aptidão ou inaptidão objeto deste exame.)");
            }


            return ret.ToString();
        }



        public string GetPlanejamentoExamesAso_Formatado_Asterisco(Ghe ghe, bool ExamesSomentePeriodoPcmso, bool Exibir_Datas, string xDataBranco, bool xDesconsiderarCompl)
        {
            StringBuilder ret = new StringBuilder();

            string xExame;
            string xEspacos;

            int xMeses = 0;

            bool TemPreventivo = false;

            int i = 1;

            string quebraLinha;

            quebraLinha = "\n";

            xEspacos = "                                                ";
            xExame = "";

            complementares = ExamesComplementares(ExamesSomentePeriodoPcmso);

            foreach (ExameComplementar complementar in complementares)
            {
                //toxicológico - não sair no ASO
                if (complementar.IdExameDicionario == 100)
                {
                    continue;
                }

                //Preventivo somente se tiver exames feitos
                if (complementar.Preventivo)
                {
                    if (complementar.dataUltimo != new DateTime())
                    {
                        //ret.Append(i++.ToString()
                        //            + ". "
                        //            + complementar.Exame
                        //            + " - "
                        //            + complementar.dataUltimo.ToString("dd-MM-yyyy")
                        //            + " *"
                        //            + quebraLinha);
                        xExame = complementar.Exame.Trim();

                        if (xExame.Length > 0)
                        {
                            if (xExame.Length < xEspacos.Length)
                            {
                                xExame = xExame + xEspacos.Substring(1, xEspacos.Length - xExame.Length);
                            }
                            
                            xMeses = -12;

                            IdExameDicionario.Find();

                            if (IdExameDicionario.Descricao.ToString() == "Periódico")
                            {
                                if (complementar.Periodicidade.IndexOf("Periódico (2 anos)") >= 0)
                                {
                                    xMeses = -24;
                                }
                                else if (complementar.Periodicidade.IndexOf("Periódico (3 anos)") >= 0)
                                {
                                    xMeses = -36;
                                }
                                else if (complementar.Periodicidade.IndexOf("Periódico (4 anos)") >= 0)
                                {
                                    xMeses = -48;
                                }
                                else if (complementar.Periodicidade.IndexOf("Periódico (5 anos)") >= 0)
                                {
                                    xMeses = -60;
                                }
                                else if (complementar.Periodicidade.IndexOf("Anual") > 0)
                                {
                                    xMeses = -12;
                                }
                                else if (complementar.Periodicidade.IndexOf("6 meses") > 0 || complementar.Periodicidade.ToUpper().IndexOf("SEMESTRAL") > 0)
                                {
                                    xMeses = -6;
                                }

                            }

                            this.IdEmpregado.Find();

                            if (complementar.Periodicidade.ToUpper().IndexOf("6 MESES DA ADMISSÃO") > 0)
                            {
                                if (this.DataExame <= this.IdEmpregado.hDT_ADM.AddMonths(6))
                                {
                                    xMeses = -6;
                                }
                                else if (this.DataExame > this.IdEmpregado.hDT_ADM.AddMonths(6) && this.DataExame <= this.IdEmpregado.hDT_ADM.AddMonths(12))
                                {
                                    if (complementar.dataUltimo >= this.IdEmpregado.hDT_ADM.AddMonths(6))
                                        xMeses = -12;
                                    else
                                        xMeses = -6;
                                }
                                else if (this.DataExame > this.IdEmpregado.hDT_ADM.AddMonths(12) && this.DataExame <= this.IdEmpregado.hDT_ADM.AddMonths(18))
                                {
                                    if (complementar.dataUltimo < this.IdEmpregado.hDT_ADM.AddMonths(6))
                                        xMeses = -6;
                                    else
                                        xMeses = -12;
                                }
                            }


                            //audiometria - regra única - 120 dias
                            //this.IdExameDicionario.Find();
                            //if (this.IdExameDicionario.Id == 2 && xExame.ToUpper().Trim() == "AUDIOMETRIA")
                            //{
                            //    if (complementar.dataUltimo.AddDays(120) < this.DataExame)
                            //    {
                            //        string zData = "  /  /    ";
                            //        xExame = xExame.Substring(0, 38) + zData;
                            //    }
                            //    else
                            //    {
                            //        System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");
                            //        string zData = complementar.dataUltimo.ToString("dd/MM/yyyy");
                            //        xExame = xExame.Substring(0, 38) + zData;
                            //    }
                            //}
                            //else 
                            if (complementar.dataUltimo >= this.DataExame.AddMonths(xMeses))
                            {
                                string zData;

                                //if (Exibir_Datas == true)
                                //{
                                //   zData = complementar.dataUltimo.ToString("dd/MM/yyyy");
                                //}
                                //else
                                //{
                                //    zData = "  /  /    ";
                                //}

                                if (Exibir_Datas == true)
                                {

                                    if (xDataBranco != "" && xDataBranco != "  /  /    ")
                                    {
                                        System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");

                                        if (xDesconsiderarCompl == false)
                                        {
                                            if (complementar.dataUltimo < System.Convert.ToDateTime(xDataBranco, ptBr))
                                                zData = "  /  /    ";
                                            else
                                                zData = complementar.dataUltimo.ToString("dd/MM/yyyy");
                                        }
                                        else
                                        {
                                            //if (System.Convert.ToDateTime(xDataBranco, ptBr) <= complementar.dataUltimo)
                                            if (this.DataExame.Date >= System.Convert.ToDateTime(xDataBranco, ptBr))
                                                zData = "  /  /    ";
                                            else
                                                zData = complementar.dataUltimo.ToString("dd/MM/yyyy");
                                            
                                        }
                                    }
                                    else
                                    {
                                        zData = complementar.dataUltimo.ToString("dd/MM/yyyy");
                                    }
                                }
                                else
                                {
                                    zData = "  /  /    ";
                                }

                                xExame = xExame.Substring(0, 38) + zData;
                            }
                            else
                            {
                                if (xExame.Substring(39, 4) == "    ")
                                {
                                    xExame = xExame.Substring(0, 38) + "/ " + xExame.Substring(41, 2) + " /    "; //"/" + xExame.Substring(43);
                                }
                                else
                                {
                                    xExame = xExame + "";
                                }

                            }

                        }

                        //ret.Append(i++.ToString()
                        //            + ". "
                        //            + xExame + "_"
                        //            + " *"
                        //            + quebraLinha);

                        if (i < 10)
                        {
                            ret.Append(i++.ToString()
                                        + ". "
                                        + xExame);
                        }
                        else
                        {
                            ret.Append(i++.ToString()
                                        + "."
                                        + xExame);
                        }

                        if (i <= 10) ret.Append("  *");
                        else ret.Append(" *");

                        if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("GLOBAL") > 0)
                        {
                            if (i % 2 == 1 || complementares.Count <= 12)
                            {
                                ret.Append(quebraLinha);
                            }
                            else
                            {
                                ret.Append("   ");
                            }
                        }
                        else
                        {
                            if (i % 2 == 1 || complementares.Count <= 9)
                            {
                                ret.Append(quebraLinha);
                            }
                            else
                            {
                                ret.Append("   ");
                            }
                        }


                        TemPreventivo = true;
                    }
                    else if (!this.DescExameComplPreventivo)
                    {
                        //ret.Append(i++.ToString()
                        //            + ". "
                        //            + complementar.Exame
                        //            + "  ___/___/______"
                        //            + " *"
                        //            + quebraLinha);
                        xExame = complementar.Exame.Trim();

                        if (xExame.Length > 0)
                        {
                            if (xExame.Length < xEspacos.Length)
                            {
                                xExame = xExame + xEspacos.Substring(1, xEspacos.Length - xExame.Length);
                            }
                            
                            xMeses = -12;

                            IdExameDicionario.Find();

                            if (IdExameDicionario.Descricao.ToString() == "Periódico")
                            {
                                if (complementar.Periodicidade.IndexOf("Periódico (2 anos)") >= 0)
                                {
                                    xMeses = -24;
                                }
                                else if (complementar.Periodicidade.IndexOf("Periódico (3 anos)") >= 0)
                                {
                                    xMeses = -36;
                                }
                                else if (complementar.Periodicidade.IndexOf("Periódico (4 anos)") >= 0)
                                {
                                    xMeses = -48;
                                }
                                else if (complementar.Periodicidade.IndexOf("Periódico (5 anos)") >= 0)
                                {
                                    xMeses = -60;
                                }
                                else if (complementar.Periodicidade.IndexOf("Anual") > 0)
                                {
                                    xMeses = -12;
                                }
                                else if (complementar.Periodicidade.IndexOf("6 meses") > 0 || complementar.Periodicidade.ToUpper().IndexOf("SEMESTRAL") > 0)
                                {
                                    xMeses = -6;
                                }

                            }


                            this.IdEmpregado.Find();

                            if (complementar.Periodicidade.ToUpper().IndexOf("6 MESES DA ADMISSÃO") > 0)
                            {
                                if (this.DataExame <= this.IdEmpregado.hDT_ADM.AddMonths(6))
                                {
                                    xMeses = -6;
                                }
                                else if (this.DataExame > this.IdEmpregado.hDT_ADM.AddMonths(6) && this.DataExame <= this.IdEmpregado.hDT_ADM.AddMonths(12))
                                {
                                    if (complementar.dataUltimo >= this.IdEmpregado.hDT_ADM.AddMonths(6))
                                        xMeses = -12;
                                    else
                                        xMeses = -6;
                                }
                                else if (this.DataExame > this.IdEmpregado.hDT_ADM.AddMonths(12) && this.DataExame <= this.IdEmpregado.hDT_ADM.AddMonths(18))
                                {
                                    if (complementar.dataUltimo < this.IdEmpregado.hDT_ADM.AddMonths(6))
                                        xMeses = -6;
                                    else
                                        xMeses = -12;
                                }
                            }


                            //audiometria - regra única - 120 dias
                            //this.IdExameDicionario.Find();
                            //if (this.IdExameDicionario.Id == 2 && xExame.ToUpper().Trim() == "AUDIOMETRIA")
                            //{
                            //    if (complementar.dataUltimo.AddDays(120) < this.DataExame)
                            //    {
                            //        string zData = "  /  /    ";
                            //        xExame = xExame.Substring(0, 38) + zData;
                            //    }
                            //    else
                            //    {
                            //        System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");
                            //        string zData = complementar.dataUltimo.ToString("dd/MM/yyyy");
                            //        xExame = xExame.Substring(0, 38) + zData;
                            //    }
                            //}
                            //else 
                            if (complementar.dataUltimo >= this.DataExame.AddMonths(xMeses))
                            {
                                string zData;

                                //if (Exibir_Datas == true)
                                //{
                                //    zData = complementar.dataUltimo.ToString("dd/MM/yyyy");
                                //}
                                //else
                                //{
                                //    zData = "  /  /    ";
                                //}

                                if (Exibir_Datas == true)
                                {

                                    if (xDataBranco != "" && xDataBranco != "  /  /    ")
                                    {
                                        System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");

                                        if (xDesconsiderarCompl == false)
                                        {
                                            if (complementar.dataUltimo < System.Convert.ToDateTime(xDataBranco, ptBr))
                                                zData = "  /  /    ";
                                            else
                                                zData = complementar.dataUltimo.ToString("dd/MM/yyyy");
                                        }
                                        else
                                        {
                                            //if (System.Convert.ToDateTime(xDataBranco, ptBr) <= complementar.dataUltimo)
                                            if (this.DataExame.Date >= System.Convert.ToDateTime(xDataBranco, ptBr))
                                                zData = "  /  /    ";
                                            else
                                                zData = complementar.dataUltimo.ToString("dd/MM/yyyy");
                                        }
                                    }
                                    else
                                    {
                                        zData = complementar.dataUltimo.ToString("dd/MM/yyyy");
                                    }
                                }
                                else
                                {
                                    zData = "  /  /    ";
                                }

                                xExame = xExame.Substring(0, 38) + zData;
                            }
                            else
                            {
                                if (xExame.Substring(39, 4) == "    ")
                                {
                                    xExame = xExame.Substring(0, 38) + "/ " + xExame.Substring(41, 2) + " /    "; //+ xExame.Substring(43);
                                }
                                else
                                {
                                    xExame = xExame + "";
                                }
                            }

                        }


                        if (i < 10)
                        {
                            ret.Append(i++.ToString()
                                        + ". "
                                        + xExame);
                        }
                        else
                        {
                            ret.Append(i++.ToString()
                                        + "."
                                        + xExame);
                        }

                        if (i <= 10) ret.Append("  *");
                        else ret.Append(" *");

                        if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("GLOBAL") > 0)
                        {
                            if (i % 2 == 1 || complementares.Count <= 12)
                            {
                                ret.Append(quebraLinha);
                            }
                            else
                            {
                                ret.Append("   ");
                            }
                        }
                        else
                        {
                            if (i % 2 == 1 || complementares.Count <= 9)
                            {
                                ret.Append(quebraLinha);
                            }
                            else
                            {
                                ret.Append("   ");
                            }
                        }



                        TemPreventivo = true;
                    }
                }
                else
                {
                    //if (complementar.dataUltimo == new DateTime())
                    //    ret.Append(i++.ToString() + ". " + complementar.Exame + quebraLinha);
                    //else
                    //{
                    //ret.Append(i++.ToString()
                    //            + ". "
                    //            + complementar.Exame
                    //            + " - " + complementar.dataUltimo.ToString("dd-MM-yyyy")
                    //            + quebraLinha);


                    xExame = complementar.Exame.Trim();

                    if (xExame.Length > 0)
                    {
                        if (xExame.Length < xEspacos.Length)
                        {
                            xExame = xExame + xEspacos.Substring(1, xEspacos.Length - xExame.Length);
                        }

                        xMeses = -12;

                        IdExameDicionario.Find();

                        if (IdExameDicionario.Descricao.ToString() == "Periódico")
                        {
                            if (complementar.Periodicidade.IndexOf("Periódico (2 anos)") >= 0)
                            {
                                xMeses = -24;
                            }
                            else if (complementar.Periodicidade.IndexOf("Periódico (3 anos)") >= 0)
                            {
                                xMeses = -36;
                            }
                            else if (complementar.Periodicidade.IndexOf("Periódico (4 anos)") >= 0)
                            {
                                xMeses = -48;
                            }
                            else if (complementar.Periodicidade.IndexOf("Periódico (5 anos)") >= 0)
                            {
                                xMeses = -60;
                            }
                            else if (complementar.Periodicidade.IndexOf("Anual") > 0)
                            {
                                xMeses = -12;
                            }
                            else if (complementar.Periodicidade.IndexOf("6 meses") > 0 || complementar.Periodicidade.ToUpper().IndexOf("SEMESTRAL") > 0)
                            {
                                xMeses = -6;
                            }

                        }

                        this.IdEmpregado.Find();

                        if (complementar.Periodicidade.ToUpper().IndexOf("6 MESES DA ADMISSÃO") > 0)
                        {
                            if (this.DataExame <= this.IdEmpregado.hDT_ADM.AddMonths(6))
                            {
                                xMeses = -6;
                            }
                            else if (this.DataExame > this.IdEmpregado.hDT_ADM.AddMonths(6) && this.DataExame <= this.IdEmpregado.hDT_ADM.AddMonths(12))
                            {
                                if (complementar.dataUltimo >= this.IdEmpregado.hDT_ADM.AddMonths(6))
                                    xMeses = -12;
                                else
                                    xMeses = -6;
                            }
                            else if (this.DataExame > this.IdEmpregado.hDT_ADM.AddMonths(12) && this.DataExame <= this.IdEmpregado.hDT_ADM.AddMonths(18))
                            {
                                if (complementar.dataUltimo < this.IdEmpregado.hDT_ADM.AddMonths(6))
                                    xMeses = -6;
                                else
                                    xMeses = -12;
                            }
                        }



                        //audiometria - regra única - 120 dias
                        //this.IdExameDicionario.Find();
                        //if (this.IdExameDicionario.Id == 2 && xExame.ToUpper().Trim() == "AUDIOMETRIA")
                        //{
                        //    if (complementar.dataUltimo.AddDays(120) < this.DataExame)
                        //    {
                        //        string zData = "  /  /    ";
                        //        xExame = xExame.Substring(0, 38) + zData;
                        //    }
                        //    else
                        //    {
                        //        System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");
                        //        string zData = complementar.dataUltimo.ToString("dd/MM/yyyy");
                        //        xExame = xExame.Substring(0, 38) + zData;
                        //    }
                        //}
                        //else 
                        if (complementar.dataUltimo >= this.DataExame.AddMonths(xMeses))
                        {
                            string zData;

                            //if (Exibir_Datas == true)
                            //{
                            //    zData = complementar.dataUltimo.ToString("dd/MM/yyyy");
                            //}
                            //else
                            //{
                            //    zData = "  /  /    ";
                            //}

                            if (Exibir_Datas == true)
                            {

                                if (xDataBranco != "" && xDataBranco != "  /  /    ")
                                {
                                    System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");

                                    if (xDesconsiderarCompl == false)
                                    {
                                        if (complementar.dataUltimo < System.Convert.ToDateTime(xDataBranco, ptBr))
                                            zData = "  /  /    ";
                                        else
                                            zData = complementar.dataUltimo.ToString("dd/MM/yyyy");
                                    }
                                    else
                                    {
                                        //if (System.Convert.ToDateTime(xDataBranco, ptBr) <= complementar.dataUltimo)
                                        if (this.DataExame.Date >= System.Convert.ToDateTime(xDataBranco, ptBr))
                                            zData = "  /  /    ";
                                        else
                                            zData = complementar.dataUltimo.ToString("dd/MM/yyyy");
                                    }
                                }
                                else
                                {
                                    zData = complementar.dataUltimo.ToString("dd/MM/yyyy");
                                }
                            }
                            else
                            {
                                zData = "  /  /    ";
                            }

                            xExame = xExame.Substring(0, 38) + zData;
                        }
                        else
                        {
                            if (xExame.Substring(39, 4) == "    ")
                            {
                                xExame = xExame.Substring(0, 38) + "/ " + xExame.Substring(41, 2) + " /    "; //+ xExame.Substring(43);
                            }
                            else
                            {
                                xExame = xExame + "";
                            }

                        }
                    }


                    //ret.Append(i++.ToString()
                    //            + ". "
                    //            + xExame + "_"
                    //            + quebraLinha);


                    if (i < 10)
                    {
                        ret.Append(i++.ToString()
                                    + ". "
                                    + xExame);
                    }
                    else
                    {
                        ret.Append(i++.ToString()
                                    + "."
                                    + xExame);
                    }


                    if (i <= 10) ret.Append("   ");
                    else ret.Append("  ");

                    if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("GLOBAL") > 0)
                    {
                        if (i % 2 == 1 || complementares.Count <= 12)
                        {
                            ret.Append(quebraLinha);
                        }
                        else
                        {
                            ret.Append("   ");
                        }
                    }
                    else
                    {
                        if (i % 2 == 1 || complementares.Count <= 9)
                        {
                            ret.Append(quebraLinha);
                        }
                        else
                        {
                            ret.Append("   ");
                        }
                    }





                    //}
                }

            }

            if (TemPreventivo)
            {
                if (complementares.Count > 9)
                {
                    if (i > 0 && i % 2 == 1) i = i + 2;

                    i = i / 2;
                    for (int zCont = i; zCont < 10; zCont++)
                        ret.Append(System.Environment.NewLine);

                    //ret.Append(System.Environment.NewLine + "* (este exame é uma exigência do cliente, sem relação com o seu risco ocupacional. Portanto, a sua realização não é obrigatória e caso exista alguma alteração, o mesmo não deverá ser levado em conta na sua aptidão laboral.)");   //por iniciativa da empresa)");
                    //ret.Append(System.Environment.NewLine + "* (exame realizado por solicitação do cliente, sem relação com risco ocupacional deste trabalhador, sua realização não é obrigatória e caso tenha resultado anormal, não deverá ser considerado na aptidão ou inaptidão objeto deste exame.)");
                    ret.Append(System.Environment.NewLine + "* exame solicitado pelo cliente");
                }
                else
                {
                    ret.Append(System.Environment.NewLine + "* exame solicitado pelo cliente");
                }
            }


            return ret.ToString();
        }




        public string GetPlanejamentoExamesAso_Formatado_Asterisco_Desconsiderar(Ghe ghe, bool ExamesSomentePeriodoPcmso, bool Exibir_Datas, Clinico clinico, int zDias_Desconsiderar)
        {
            StringBuilder ret = new StringBuilder();

            string xExame;
            string xEspacos;

            int xMeses = 0;

            bool TemPreventivo = false;

            int i = 1;

            string quebraLinha;

            quebraLinha = "\n";

            xEspacos = "                                                ";
            xExame = "";



            string xDataBranco = "";
            bool xDesconsiderarCompl = false;

            complementares = ExamesComplementares(ExamesSomentePeriodoPcmso);

            foreach (ExameComplementar complementar in complementares)
            {
                //toxicológico - não sair no ASO
                if (complementar.IdExameDicionario == 100)
                {
                    continue;
                }


                System.Globalization.CultureInfo ptBr2 = new System.Globalization.CultureInfo("pt-Br");

                ExamePlanejamento xPlanej = new ExamePlanejamento();
                clinico.IdEmpregado.Find();
                clinico.IdExameDicionario.Find();
                clinico.IdPcmso.Find();

                xPlanej.Find(" IdEmpregado =" + clinico.IdEmpregado.Id.ToString() + " and IdExameDicionario = " + complementar.IdExameDicionario.ToString() + " and IdPCMSO = " + clinico.IdPcmso.Id.ToString());

                if (xPlanej.Id == 0)
                {
                    xDataBranco = clinico.DataExame.AddDays(zDias_Desconsiderar * (-1)).ToString("dd/MM/yyyy", ptBr2);
                }
                else
                {
                    if (xPlanej.DataVencimento == new DateTime())
                        xDataBranco = this.DataExame.ToString("dd/MM/yyyy", ptBr2);  //"  /  /    ";
                    else
                        xDataBranco = xPlanej.DataVencimento.AddDays(zDias_Desconsiderar * (-1)).ToString("dd/MM/yyyy", ptBr2);
                }

                xDesconsiderarCompl = true;
                //Preventivo somente se tiver exames feitos
                if (complementar.Preventivo)
                {
                    if (complementar.dataUltimo != new DateTime())
                    {
                        //ret.Append(i++.ToString()
                        //            + ". "
                        //            + complementar.Exame
                        //            + " - "
                        //            + complementar.dataUltimo.ToString("dd-MM-yyyy")
                        //            + " *"
                        //            + quebraLinha);
                        xExame = complementar.Exame.Trim();

                        if (xExame.Length > 0)
                        {
                            if (xExame.Length < xEspacos.Length)
                            {
                                xExame = xExame + xEspacos.Substring(1, xEspacos.Length - xExame.Length);
                            }

                            xMeses = -12;

                            IdExameDicionario.Find();

                            if (IdExameDicionario.Descricao.ToString() == "Periódico")
                            {
                                if (complementar.Periodicidade.IndexOf("Periódico (2 anos)") >= 0)
                                {
                                    xMeses = -24;
                                }
                                else if (complementar.Periodicidade.IndexOf("Periódico (3 anos)") >= 0)
                                {
                                    xMeses = -36;
                                }
                                else if (complementar.Periodicidade.IndexOf("Periódico (4 anos)") >= 0)
                                {
                                    xMeses = -48;
                                }
                                else if (complementar.Periodicidade.IndexOf("Periódico (5 anos)") >= 0)
                                {
                                    xMeses = -60;
                                }
                                else if (complementar.Periodicidade.IndexOf("Anual") > 0)
                                {
                                    xMeses = -12;
                                }
                                else if (complementar.Periodicidade.IndexOf("6 meses") > 0 || complementar.Periodicidade.ToUpper().IndexOf("SEMESTRAL") > 0)
                                {
                                    xMeses = -6;
                                }

                            }


                            this.IdEmpregado.Find();

                            if (complementar.Periodicidade.ToUpper().IndexOf("6 MESES DA ADMISSÃO") > 0)
                            {
                                if (this.DataExame <= this.IdEmpregado.hDT_ADM.AddMonths(6))
                                {
                                    xMeses = -6;
                                }
                                else if (this.DataExame > this.IdEmpregado.hDT_ADM.AddMonths(6) && this.DataExame <= this.IdEmpregado.hDT_ADM.AddMonths(12))
                                {
                                    if (complementar.dataUltimo >= this.IdEmpregado.hDT_ADM.AddMonths(6))
                                        xMeses = -12;
                                    else
                                        xMeses = -6;
                                }
                                else if (this.DataExame > this.IdEmpregado.hDT_ADM.AddMonths(12) && this.DataExame <= this.IdEmpregado.hDT_ADM.AddMonths(18))
                                {
                                    if (complementar.dataUltimo < this.IdEmpregado.hDT_ADM.AddMonths(6))
                                        xMeses = -6;
                                    else
                                        xMeses = -12;
                                }
                            }


                            //audiometria - regra única - 120 dias
                            //this.IdExameDicionario.Find();
                            //if (this.IdExameDicionario.Id == 2 && xExame.ToUpper().Trim() == "AUDIOMETRIA")
                            //{
                            //    if (complementar.dataUltimo.AddDays(120) < this.DataExame)
                            //    {
                            //        string zData = "  /  /    ";
                            //        xExame = xExame.Substring(0, 38) + zData;
                            //    }
                            //    else
                            //    {
                            //        System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");
                            //        string zData = complementar.dataUltimo.ToString("dd/MM/yyyy");
                            //        xExame = xExame.Substring(0, 38) + zData;
                            //    }
                            //}
                            //else 
                            if (complementar.dataUltimo >= this.DataExame.AddMonths(xMeses))
                            {
                                string zData;

                                //if (Exibir_Datas == true)
                                //{
                                //   zData = complementar.dataUltimo.ToString("dd/MM/yyyy");
                                //}
                                //else
                                //{
                                //    zData = "  /  /    ";
                                //}

                                if (Exibir_Datas == true)
                                {

                                    if (xDataBranco != "" && xDataBranco != "  /  /    ")
                                    {
                                        System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");

                                        if (xDesconsiderarCompl == false)
                                        {
                                            if (complementar.dataUltimo < System.Convert.ToDateTime(xDataBranco, ptBr))
                                                zData = "  /  /    ";
                                            else
                                                zData = complementar.dataUltimo.ToString("dd/MM/yyyy");
                                        }
                                        else
                                        {
                                            //if (System.Convert.ToDateTime(xDataBranco, ptBr) <= complementar.dataUltimo)
                                            if (this.DataExame.Date >= System.Convert.ToDateTime(xDataBranco, ptBr))
                                                zData = "  /  /    ";
                                            else
                                                zData = complementar.dataUltimo.ToString("dd/MM/yyyy");
                                        }
                                    }
                                    else
                                    {
                                        zData = complementar.dataUltimo.ToString("dd/MM/yyyy");
                                    }
                                }
                                else
                                {
                                    zData = "  /  /    ";
                                }

                                xExame = xExame.Substring(0, 38) + zData;
                            }
                            else
                            {
                                if (xExame.Substring(39, 4) == "    ")
                                {
                                    xExame = xExame.Substring(0, 38) + "/ " + xExame.Substring(41, 2) + " /    "; //"/" + xExame.Substring(43);
                                }
                                else
                                {
                                    xExame = xExame + "";
                                }

                            }

                        }

                        //ret.Append(i++.ToString()
                        //            + ". "
                        //            + xExame + "_"
                        //            + " *"
                        //            + quebraLinha);

                        if (i < 10)
                        {
                            ret.Append(i++.ToString()
                                        + ". "
                                        + xExame);
                        }
                        else
                        {
                            ret.Append(i++.ToString()
                                        + "."
                                        + xExame);
                        }


                        if (i <= 10) ret.Append("  *");
                        else ret.Append(" *");

                        if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("GLOBAL") > 0)
                        {
                            if (i % 2 == 1 || complementares.Count <= 12)
                            {
                                ret.Append(quebraLinha);
                            }
                            else
                            {
                                ret.Append("   ");
                            }
                        }
                        else
                        {
                            if (i % 2 == 1 || complementares.Count <= 9)
                            {
                                ret.Append(quebraLinha);
                            }
                            else
                            {
                                ret.Append("   ");
                            }
                        }


                        TemPreventivo = true;
                    }
                    else if (!this.DescExameComplPreventivo)
                    {
                        //ret.Append(i++.ToString()
                        //            + ". "
                        //            + complementar.Exame
                        //            + "  ___/___/______"
                        //            + " *"
                        //            + quebraLinha);
                        xExame = complementar.Exame.Trim();

                        if (xExame.Length > 0)
                        {
                            if (xExame.Length < xEspacos.Length)
                            {
                                xExame = xExame + xEspacos.Substring(1, xEspacos.Length - xExame.Length);
                            }

                            xMeses = -12;

                            IdExameDicionario.Find();

                            if (IdExameDicionario.Descricao.ToString() == "Periódico")
                            {
                                if (complementar.Periodicidade.IndexOf("Periódico (2 anos)") >= 0)
                                {
                                    xMeses = -24;
                                }
                                else if (complementar.Periodicidade.IndexOf("Periódico (3 anos)") >= 0)
                                {
                                    xMeses = -36;
                                }
                                else if (complementar.Periodicidade.IndexOf("Periódico (4 anos)") >= 0)
                                {
                                    xMeses = -48;
                                }
                                else if (complementar.Periodicidade.IndexOf("Periódico (5 anos)") >= 0)
                                {
                                    xMeses = -60;
                                }
                                else if (complementar.Periodicidade.IndexOf("Anual") > 0)
                                {
                                    xMeses = -12;
                                }
                                else if (complementar.Periodicidade.IndexOf("6 meses") > 0 || complementar.Periodicidade.ToUpper().IndexOf("SEMESTRAL") > 0)
                                {
                                    xMeses = -6;
                                }

                            }


                            this.IdEmpregado.Find();

                            if (complementar.Periodicidade.ToUpper().IndexOf("6 MESES DA ADMISSÃO") > 0)
                            {
                                if (this.DataExame <= this.IdEmpregado.hDT_ADM.AddMonths(6))
                                {
                                    xMeses = -6;
                                }
                                else if (this.DataExame > this.IdEmpregado.hDT_ADM.AddMonths(6) && this.DataExame <= this.IdEmpregado.hDT_ADM.AddMonths(12))
                                {
                                    if (complementar.dataUltimo >= this.IdEmpregado.hDT_ADM.AddMonths(6))
                                        xMeses = -12;
                                    else
                                        xMeses = -6;
                                }
                                else if (this.DataExame > this.IdEmpregado.hDT_ADM.AddMonths(12) && this.DataExame <= this.IdEmpregado.hDT_ADM.AddMonths(18))
                                {
                                    if (complementar.dataUltimo < this.IdEmpregado.hDT_ADM.AddMonths(6))
                                        xMeses = -6;
                                    else
                                        xMeses = -12;
                                }
                            }


                            ////audiometria - regra única - 120 dias
                            //this.IdExameDicionario.Find();
                            //if (this.IdExameDicionario.Id == 2 && xExame.ToUpper().Trim() == "AUDIOMETRIA")
                            //{
                            //    if (complementar.dataUltimo.AddDays(120) < this.DataExame)
                            //    {
                            //        string zData = "  /  /    ";
                            //        xExame = xExame.Substring(0, 38) + zData;
                            //    }
                            //    else
                            //    {
                            //        System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");
                            //        string zData = complementar.dataUltimo.ToString("dd/MM/yyyy");
                            //        xExame = xExame.Substring(0, 38) + zData;
                            //    }
                            //}
                            //else 
                            if (complementar.dataUltimo >= this.DataExame.AddMonths(xMeses))
                            {
                                string zData;

                                //if (Exibir_Datas == true)
                                //{
                                //    zData = complementar.dataUltimo.ToString("dd/MM/yyyy");
                                //}
                                //else
                                //{
                                //    zData = "  /  /    ";
                                //}

                                if (Exibir_Datas == true)
                                {

                                    if (xDataBranco != "" && xDataBranco != "  /  /    ")
                                    {
                                        System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");

                                        if (xDesconsiderarCompl == false)
                                        {
                                            if (complementar.dataUltimo < System.Convert.ToDateTime(xDataBranco, ptBr))
                                                zData = "  /  /    ";
                                            else
                                                zData = complementar.dataUltimo.ToString("dd/MM/yyyy");
                                        }
                                        else
                                        {
                                            //if (System.Convert.ToDateTime(xDataBranco, ptBr) <= complementar.dataUltimo)
                                            if (this.DataExame.Date >= System.Convert.ToDateTime(xDataBranco, ptBr))
                                                zData = "  /  /    ";
                                            else
                                                zData = complementar.dataUltimo.ToString("dd/MM/yyyy");
                                        }
                                    }
                                    else
                                    {
                                        zData = complementar.dataUltimo.ToString("dd/MM/yyyy");
                                    }
                                }
                                else
                                {
                                    zData = "  /  /    ";
                                }

                                xExame = xExame.Substring(0, 38) + zData;
                            }
                            else
                            {
                                if (xExame.Substring(39, 4) == "    ")
                                {
                                    xExame = xExame.Substring(0, 38) + "/ " + xExame.Substring(41, 2) + " /    "; //+ xExame.Substring(43);
                                }
                                else
                                {
                                    xExame = xExame + "";
                                }
                            }

                        }


                        if (i < 10)
                        {
                            ret.Append(i++.ToString()
                                        + ". "
                                        + xExame);
                        }
                        else
                        {
                            ret.Append(i++.ToString()
                                        + "."
                                        + xExame);
                        }


                        if (i <= 10) ret.Append("  *");
                        else ret.Append(" *");

                        if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("GLOBAL") > 0)
                        {
                            if (i % 2 == 1 || complementares.Count <= 12)
                            {
                                ret.Append(quebraLinha);
                            }
                            else
                            {
                                ret.Append("   ");
                            }
                        }
                        else
                        {
                            if (i % 2 == 1 || complementares.Count <= 9)
                            {
                                ret.Append(quebraLinha);
                            }
                            else
                            {
                                ret.Append("   ");
                            }
                        }


                        TemPreventivo = true;
                    }
                }
                else
                {
                    //if (complementar.dataUltimo == new DateTime())
                    //    ret.Append(i++.ToString() + ". " + complementar.Exame + quebraLinha);
                    //else
                    //{
                    //ret.Append(i++.ToString()
                    //            + ". "
                    //            + complementar.Exame
                    //            + " - " + complementar.dataUltimo.ToString("dd-MM-yyyy")
                    //            + quebraLinha);


                    xExame = complementar.Exame.Trim();

                    if (xExame.Length > 0)
                    {
                        if (xExame.Length < xEspacos.Length)
                        {
                            xExame = xExame + xEspacos.Substring(1, xEspacos.Length - xExame.Length);
                        }

                        xMeses = -12;

                        IdExameDicionario.Find();

                        if (IdExameDicionario.Descricao.ToString() == "Periódico")
                        {
                            if (complementar.Periodicidade.IndexOf("Periódico (2 anos)") >= 0)
                            {
                                xMeses = -24;
                            }
                            else if (complementar.Periodicidade.IndexOf("Periódico (3 anos)") >= 0)
                            {
                                xMeses = -36;
                            }
                            else if (complementar.Periodicidade.IndexOf("Periódico (4 anos)") >= 0)
                            {
                                xMeses = -48;
                            }
                            else if (complementar.Periodicidade.IndexOf("Periódico (5 anos)") >= 0)
                            {
                                xMeses = -60;
                            }
                            else if (complementar.Periodicidade.IndexOf("Anual") > 0)
                            {
                                xMeses = -12;
                            }
                            else if (complementar.Periodicidade.IndexOf("6 meses") > 0 || complementar.Periodicidade.ToUpper().IndexOf("SEMESTRAL") > 0)
                            {
                                xMeses = -6;
                            }

                        }


                        this.IdEmpregado.Find();

                        if (complementar.Periodicidade.ToUpper().IndexOf("6 MESES DA ADMISSÃO") > 0)
                        {
                            if (this.DataExame <= this.IdEmpregado.hDT_ADM.AddMonths(6))
                            {
                                xMeses = -6;
                            }
                            else if (this.DataExame > this.IdEmpregado.hDT_ADM.AddMonths(6) && this.DataExame <= this.IdEmpregado.hDT_ADM.AddMonths(12))
                            {
                                if (complementar.dataUltimo >= this.IdEmpregado.hDT_ADM.AddMonths(6))
                                    xMeses = -12;
                                else
                                    xMeses = -6;
                            }
                            else if (this.DataExame > this.IdEmpregado.hDT_ADM.AddMonths(12) && this.DataExame <= this.IdEmpregado.hDT_ADM.AddMonths(18))
                            {
                                if (complementar.dataUltimo < this.IdEmpregado.hDT_ADM.AddMonths(6))
                                    xMeses = -6;
                                else
                                    xMeses = -12;
                            }
                        }



                        //audiometria - regra única - 120 dias
                        //this.IdExameDicionario.Find();
                        //if (this.IdExameDicionario.Id == 2 && xExame.ToUpper().Trim() == "AUDIOMETRIA")
                        //{
                        //    if (complementar.dataUltimo.AddDays(120) < this.DataExame)
                        //    {
                        //        string zData = "  /  /    ";
                        //        xExame = xExame.Substring(0, 38) + zData;
                        //    }
                        //    else
                        //    {
                        //        System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");
                        //        string zData = complementar.dataUltimo.ToString("dd/MM/yyyy");
                        //        xExame = xExame.Substring(0, 38) + zData;
                        //    }
                        //}
                        //else 
                        if (complementar.dataUltimo >= this.DataExame.AddMonths(xMeses))
                        {
                            string zData;

                            //if (Exibir_Datas == true)
                            //{
                            //    zData = complementar.dataUltimo.ToString("dd/MM/yyyy");
                            //}
                            //else
                            //{
                            //    zData = "  /  /    ";
                            //}

                            if (Exibir_Datas == true)
                            {

                                if (xDataBranco != "" && xDataBranco != "  /  /    ")
                                {
                                    System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");

                                    if (xDesconsiderarCompl == false)
                                    {
                                        if (complementar.dataUltimo < System.Convert.ToDateTime(xDataBranco, ptBr))
                                            zData = "  /  /    ";
                                        else
                                            zData = complementar.dataUltimo.ToString("dd/MM/yyyy");
                                    }
                                    else
                                    {
                                        //if (System.Convert.ToDateTime(xDataBranco, ptBr) <= complementar.dataUltimo)
                                        if (this.DataExame.Date >= System.Convert.ToDateTime(xDataBranco, ptBr))
                                            zData = "  /  /    ";
                                        else
                                            zData = complementar.dataUltimo.ToString("dd/MM/yyyy");
                                    }
                                }
                                else
                                {
                                    zData = complementar.dataUltimo.ToString("dd/MM/yyyy");
                                }
                            }
                            else
                            {
                                zData = "  /  /    ";
                            }

                            xExame = xExame.Substring(0, 38) + zData;
                        }
                        else
                        {
                            if (xExame.Substring(39, 4) == "    ")
                            {
                                xExame = xExame.Substring(0, 38) + "/ " + xExame.Substring(41, 2) + " /    "; //+ xExame.Substring(43);
                            }
                            else
                            {
                                xExame = xExame + "";
                            }

                        }
                    }


                    //ret.Append(i++.ToString()
                    //            + ". "
                    //            + xExame + "_"
                    //            + quebraLinha);


                    if (i < 10)
                    {
                        ret.Append(i++.ToString()
                                    + ". "
                                    + xExame);
                    }
                    else
                    {
                        ret.Append(i++.ToString()
                                    + "."
                                    + xExame);
                    }


                    if (i <= 10) ret.Append("   ");
                    else ret.Append("  ");

                    if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("GLOBAL") > 0)
                    {
                        if (i % 2 == 1 || complementares.Count <= 12)
                        {
                            ret.Append(quebraLinha);
                        }
                        else
                        {
                            ret.Append("   ");
                        }
                    }
                    else
                    {
                        if (i % 2 == 1 || complementares.Count <= 9)
                        {
                            ret.Append(quebraLinha);
                        }
                        else
                        {
                            ret.Append("   ");
                        }
                    }





                    //}
                }

            }

            if (TemPreventivo)
            {
                if (complementares.Count > 9)
                {
                    if (i > 0 && i % 2 == 1) i = i + 2;

                    i = i / 2;
                    for (int zCont = i; zCont < 10; zCont++)
                        ret.Append(System.Environment.NewLine);

                    //ret.Append(System.Environment.NewLine + "* (este exame é uma exigência do cliente, sem relação com o seu risco ocupacional. Portanto, a sua realização não é obrigatória e caso exista alguma alteração, o mesmo não deverá ser levado em conta na sua aptidão laboral.)");   //por iniciativa da empresa)");
                    //ret.Append(System.Environment.NewLine + "* (exame realizado por solicitação do cliente, sem relação com risco ocupacional deste trabalhador, sua realização não é obrigatória e caso tenha resultado anormal, não deverá ser considerado na aptidão ou inaptidão objeto deste exame.)");
                    ret.Append(System.Environment.NewLine + "* exame solicitado pelo cliente");
                }
                else
                {
                    ret.Append(System.Environment.NewLine + "* exame solicitado pelo cliente");
                }
            }


            return ret.ToString();
        }




        public string GetPlanejamentoExamesAso_Formatado(Ghe ghe, bool ExamesSomentePeriodoPcmso, bool Exibir_Datas, string xDataBranco, bool xDesconsiderarCompl)
        {
            StringBuilder ret = new StringBuilder();

            string xExame;
            string xEspacos;

            int xMeses = 0;

            bool TemPreventivo = false;

            int i = 1;

            string quebraLinha;

            quebraLinha = "\n";

            xEspacos = "                                                ";
            xExame = "";

            complementares = ExamesComplementares(ExamesSomentePeriodoPcmso);

            foreach (ExameComplementar complementar in complementares)
            {
                //toxicológico - não sair no ASO
                if (complementar.IdExameDicionario == 100)
                {
                    continue;
                }

                //Preventivo somente se tiver exames feitos
                if (complementar.Preventivo)
                {
                    if (complementar.dataUltimo != new DateTime())
                    {
                        //ret.Append(i++.ToString()
                        //            + ". "
                        //            + complementar.Exame
                        //            + " - "
                        //            + complementar.dataUltimo.ToString("dd-MM-yyyy")
                        //            + " *"
                        //            + quebraLinha);
                        xExame = complementar.Exame.Trim();

                        if (xExame.Length > 0)
                        {
                            if (xExame.Length < xEspacos.Length)
                            {
                                xExame = xExame + xEspacos.Substring(1, xEspacos.Length - xExame.Length);
                            }

                            
                            xMeses = -12;

                            IdExameDicionario.Find();

                            if (IdExameDicionario.Descricao.ToString() == "Periódico" || IdExameDicionario.Descricao.ToString() == "Mudança de Função")
                            {
                                if (complementar.Periodicidade.IndexOf("Periódico (2 anos)") >= 0)
                                {
                                    xMeses = -24;
                                }
                                else if (complementar.Periodicidade.IndexOf("Periódico (3 anos)") >= 0)
                                {
                                    xMeses = -36;
                                }
                                else if (complementar.Periodicidade.IndexOf("Periódico (4 anos)") >= 0)
                                {
                                    xMeses = -48;
                                }
                                else if (complementar.Periodicidade.IndexOf("Periódico (5 anos)") >= 0)
                                {
                                    xMeses = -60;
                                }
                                else if (complementar.Periodicidade.IndexOf("Anual") > 0)
                                {
                                    xMeses = -12;
                                }
                                else if (complementar.Periodicidade.IndexOf("6 meses") > 0 || complementar.Periodicidade.ToUpper().IndexOf("SEMESTRAL") > 0)
                                {
                                    xMeses = -6;
                                }

                            }


                            this.IdEmpregado.Find();

                            if (complementar.Periodicidade.ToUpper().IndexOf("6 MESES DA ADMISSÃO") > 0)
                            {
                                if (this.DataExame <= this.IdEmpregado.hDT_ADM.AddMonths(6))
                                {
                                    xMeses = -6;
                                }
                                else if (this.DataExame > this.IdEmpregado.hDT_ADM.AddMonths(6) && this.DataExame <= this.IdEmpregado.hDT_ADM.AddMonths(12))
                                {
                                    if (complementar.dataUltimo >= this.IdEmpregado.hDT_ADM.AddMonths(6))
                                        xMeses = -12;
                                    else
                                        xMeses = -6;
                                }
                                else if (this.DataExame > this.IdEmpregado.hDT_ADM.AddMonths(12) && this.DataExame <= this.IdEmpregado.hDT_ADM.AddMonths(18))
                                {
                                    if (complementar.dataUltimo < this.IdEmpregado.hDT_ADM.AddMonths(6))
                                        xMeses = -6;
                                    else
                                        xMeses = -12;
                                }
                            }


                            //audiometria - regra única - 120 dias
                            //this.IdExameDicionario.Find();
                            //if (this.IdExameDicionario.Id == 2 && xExame.ToUpper().Trim() == "AUDIOMETRIA")
                            //{
                            //    if (complementar.dataUltimo.AddDays(120) < this.DataExame)
                            //    {
                            //        string zData = "  /  /    ";
                            //        xExame = xExame.Substring(0, 38) + zData;
                            //    }
                            //    else
                            //    {
                            //        System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");
                            //        string zData = complementar.dataUltimo.ToString("dd/MM/yyyy");
                            //        xExame = xExame.Substring(0, 38) + zData;
                            //    }
                            //}

                            //else 
                            if (complementar.dataUltimo >= this.DataExame.AddMonths(xMeses))
                            {
                                string zData;

                                //if (Exibir_Datas == true) zData = complementar.dataUltimo.ToString("dd/MM/yyyy");
                                //else zData = "  /  /    ";

                                if (Exibir_Datas == true)
                                {

                                    if (xDataBranco != "" && xDataBranco != "  /  /    ")
                                    {
                                        System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");

                                        if (xDesconsiderarCompl == false)
                                        {
                                            if (complementar.dataUltimo < System.Convert.ToDateTime(xDataBranco, ptBr))
                                                zData = "  /  /    ";
                                            else
                                                zData = complementar.dataUltimo.ToString("dd/MM/yyyy");
                                        }
                                        else
                                        {
                                            //if (System.Convert.ToDateTime(xDataBranco, ptBr) <= complementar.dataUltimo)
                                            if (this.DataExame.Date >= System.Convert.ToDateTime(xDataBranco, ptBr))
                                                zData = "  /  /    ";
                                            else
                                                zData = complementar.dataUltimo.ToString("dd/MM/yyyy");
                                        }
                                    }
                                    else
                                    {
                                        zData = complementar.dataUltimo.ToString("dd/MM/yyyy");
                                    }
                                }
                                else
                                {
                                    zData = "  /  /    ";
                                }

                                xExame = xExame.Substring(0, 38) + zData;
                            }
                            else
                            {
                                if (xExame.Substring(39, 4) == "    ")
                                {
                                    xExame = xExame.Substring(0, 40) + "/" + xExame.Substring(41, 2) + "/    "; //"/" + xExame.Substring(43);
                                }
                                else
                                {
                                    xExame = xExame + "";
                                }

                            }

                        }

                        //ret.Append(i++.ToString()
                        //            + ". "
                        //            + xExame + "_"
                        //            + " *"
                        //            + quebraLinha);

                        if (i < 10)
                        {
                            ret.Append(i++.ToString()
                                        + ". "
                                        + xExame);
                        }
                        else
                        {
                            ret.Append(i++.ToString()
                                        + "."
                                        + xExame);
                        }



                        if (i <= 10) ret.Append("   ");
                        else ret.Append("  ");

                        if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("GLOBAL") > 0)
                        {
                            if (i % 2 == 1 || complementares.Count <= 12)
                            {
                                ret.Append(quebraLinha);
                            }
                            else
                            {
                                ret.Append("   ");
                            }
                        }
                        else
                        {
                            if (i % 2 == 1 || complementares.Count <= 9)
                            {
                                ret.Append(quebraLinha);
                            }
                            else
                            {
                                ret.Append("   ");
                            }
                        }


                        TemPreventivo = true;
                    }
                    else if (!this.DescExameComplPreventivo)
                    {
                        //ret.Append(i++.ToString()
                        //            + ". "
                        //            + complementar.Exame
                        //            + "  ___/___/______"
                        //            + " *"
                        //            + quebraLinha);
                        xExame = complementar.Exame.Trim();

                        if (xExame.Length > 0)
                        {
                            if (xExame.Length < xEspacos.Length)
                            {
                                xExame = xExame + xEspacos.Substring(1, xEspacos.Length - xExame.Length);
                            }
                            
                            xMeses = -12;

                            IdExameDicionario.Find();

                            if (IdExameDicionario.Descricao.ToString() == "Periódico" || IdExameDicionario.Descricao.ToString() == "Mudança de Função")
                            {
                                if (complementar.Periodicidade.IndexOf("Periódico (2 anos)") >= 0)
                                {
                                    xMeses = -24;
                                }
                                else if (complementar.Periodicidade.IndexOf("Periódico (3 anos)") >= 0)
                                {
                                    xMeses = -36;
                                }
                                else if (complementar.Periodicidade.IndexOf("Periódico (4 anos)") >= 0)
                                {
                                    xMeses = -48;
                                }
                                else if (complementar.Periodicidade.IndexOf("Periódico (5 anos)") >= 0)
                                {
                                    xMeses = -60;
                                }
                                else if (complementar.Periodicidade.IndexOf("Anual") > 0)
                                {
                                    xMeses = -12;
                                }
                                else if (complementar.Periodicidade.IndexOf("6 meses") > 0 || complementar.Periodicidade.ToUpper().IndexOf("SEMESTRAL") > 0)
                                {
                                    xMeses = -6;
                                }

                            }


                            this.IdEmpregado.Find();

                            if (complementar.Periodicidade.ToUpper().IndexOf("6 MESES DA ADMISSÃO") > 0)
                            {
                                if (this.DataExame <= this.IdEmpregado.hDT_ADM.AddMonths(6))
                                {
                                    xMeses = -6;
                                }
                                else if (this.DataExame > this.IdEmpregado.hDT_ADM.AddMonths(6) && this.DataExame <= this.IdEmpregado.hDT_ADM.AddMonths(12))
                                {
                                    if (complementar.dataUltimo >= this.IdEmpregado.hDT_ADM.AddMonths(6))
                                        xMeses = -12;
                                    else
                                        xMeses = -6;
                                }
                                else if (this.DataExame > this.IdEmpregado.hDT_ADM.AddMonths(12) && this.DataExame <= this.IdEmpregado.hDT_ADM.AddMonths(18))
                                {
                                    if (complementar.dataUltimo < this.IdEmpregado.hDT_ADM.AddMonths(6))
                                        xMeses = -6;
                                    else
                                        xMeses = -12;
                                }
                            }


                            ////audiometria - regra única - 120 dias
                            //this.IdExameDicionario.Find();
                            //if (this.IdExameDicionario.Id == 2 && xExame.ToUpper().Trim() == "AUDIOMETRIA")
                            //{
                            //    if (complementar.dataUltimo.AddDays(120) < this.DataExame)
                            //    {
                            //        string zData = "  /  /    ";
                            //        xExame = xExame.Substring(0, 38) + zData;
                            //    }
                            //    else
                            //    {
                            //        System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");
                            //        string zData = complementar.dataUltimo.ToString("dd/MM/yyyy");
                            //        xExame = xExame.Substring(0, 38) + zData;
                            //    }
                            //}
                            //else 
                            if (complementar.dataUltimo >= this.DataExame.AddMonths(xMeses))
                            {
                                string zData;

                                //if (Exibir_Datas == true) zData = complementar.dataUltimo.ToString("dd/MM/yyyy");
                                //else zData = "  /  /    ";

                                if (Exibir_Datas == true)
                                {

                                    if (xDataBranco != "" && xDataBranco != "  /  /    ")
                                    {
                                        System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");

                                        if (xDesconsiderarCompl == false)
                                        {
                                            if (complementar.dataUltimo < System.Convert.ToDateTime(xDataBranco, ptBr))
                                                zData = "  /  /    ";
                                            else
                                                zData = complementar.dataUltimo.ToString("dd/MM/yyyy");
                                        }
                                        else
                                        {
                                            //if (System.Convert.ToDateTime(xDataBranco, ptBr) <= complementar.dataUltimo)
                                            if (this.DataExame.Date >= System.Convert.ToDateTime(xDataBranco, ptBr))
                                                zData = "  /  /    ";
                                            else
                                                zData = complementar.dataUltimo.ToString("dd/MM/yyyy");
                                        }
                                    }
                                    else
                                    {
                                        zData = complementar.dataUltimo.ToString("dd/MM/yyyy");
                                    }
                                }
                                else
                                {
                                    zData = "  /  /    ";
                                }

                                xExame = xExame.Substring(0, 38) + zData;
                            }
                            else
                            {
                                if (xExame.Substring(39, 4) == "    ")
                                {
                                    xExame = xExame.Substring(0, 40) + "/" + xExame.Substring(41, 2) + "/    "; //+ xExame.Substring(43);
                                }
                                else
                                {
                                    xExame = xExame + "";
                                }
                            }

                        }


                        if (i < 10)
                        {
                            ret.Append(i++.ToString()
                                        + ". "
                                        + xExame);
                        }
                        else
                        {
                            ret.Append(i++.ToString()
                                        + "."
                                        + xExame);
                        }


                        if (i <= 10) ret.Append("   ");
                        else ret.Append("  ");

                        if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("GLOBAL") > 0)
                        {
                            if (i % 2 == 1 || complementares.Count <= 12)
                            {
                                ret.Append(quebraLinha);
                            }
                            else
                            {
                                ret.Append("   ");
                            }
                        }
                        else
                        {
                            if (i % 2 == 1 || complementares.Count <= 9)
                            {
                                ret.Append(quebraLinha);
                            }
                            else
                            {
                                ret.Append("   ");
                            }
                        }



                        TemPreventivo = true;
                    }
                }
                else
                {
                    //if (complementar.dataUltimo == new DateTime())
                    //    ret.Append(i++.ToString() + ". " + complementar.Exame + quebraLinha);
                    //else
                    //{
                    //ret.Append(i++.ToString()
                    //            + ". "
                    //            + complementar.Exame
                    //            + " - " + complementar.dataUltimo.ToString("dd-MM-yyyy")
                    //            + quebraLinha);


                    xExame = complementar.Exame.Trim();

                    if (xExame.Length > 0)
                    {
                        if (xExame.Length < xEspacos.Length)
                        {
                            xExame = xExame + xEspacos.Substring(1, xEspacos.Length - xExame.Length);
                        }

                        xMeses = -12;

                        IdExameDicionario.Find();

                        if (IdExameDicionario.Descricao.ToString() == "Periódico" || IdExameDicionario.Descricao.ToString() == "Mudança de Função")
                        {
                            if (complementar.Periodicidade.IndexOf("Periódico (2 anos)") >= 0)
                            {
                                xMeses = -24;
                            }
                            else if (complementar.Periodicidade.IndexOf("Periódico (3 anos)") >= 0)
                            {
                                xMeses = -36;
                            }
                            else if (complementar.Periodicidade.IndexOf("Periódico (4 anos)") >= 0)
                            {
                                xMeses = -48;
                            }
                            else if (complementar.Periodicidade.IndexOf("Periódico (5 anos)") >= 0)
                            {
                                xMeses = -60;
                            }
                            else if (complementar.Periodicidade.IndexOf("Anual") > 0)
                            {
                                xMeses = -12;
                            }
                            else if (complementar.Periodicidade.IndexOf("6 meses") > 0 || complementar.Periodicidade.ToUpper().IndexOf("SEMESTRAL") > 0)
                            {
                                xMeses = -6;
                            }

                            
                        }


                        this.IdEmpregado.Find();

                        if (complementar.Periodicidade.ToUpper().IndexOf("6 MESES DA ADMISSÃO") > 0)
                        {
                            if (this.DataExame <= this.IdEmpregado.hDT_ADM.AddMonths(6))
                            {
                                xMeses = -6;
                            }
                            else if (this.DataExame > this.IdEmpregado.hDT_ADM.AddMonths(6) && this.DataExame <= this.IdEmpregado.hDT_ADM.AddMonths(12))
                            {
                                if (complementar.dataUltimo >= this.IdEmpregado.hDT_ADM.AddMonths(6))
                                    xMeses = -12;
                                else
                                    xMeses = -6;
                            }
                            else if (this.DataExame > this.IdEmpregado.hDT_ADM.AddMonths(12) && this.DataExame <= this.IdEmpregado.hDT_ADM.AddMonths(18))
                            {
                                if (complementar.dataUltimo < this.IdEmpregado.hDT_ADM.AddMonths(6))
                                    xMeses = -6;
                                else
                                    xMeses = -12;
                            }
                        }



                        //audiometria - regra única - 120 dias
                        //this.IdExameDicionario.Find();
                        //if (this.IdExameDicionario.Id == 2 && xExame.ToUpper().Trim() == "AUDIOMETRIA")
                        //{
                        //    if (complementar.dataUltimo.AddDays(120) < this.DataExame)
                        //    {
                        //        string zData = "  /  /    ";
                        //        xExame = xExame.Substring(0, 38) + zData;
                        //    }
                        //    else
                        //    {
                        //        System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");
                        //        string zData = complementar.dataUltimo.ToString("dd/MM/yyyy");
                        //        xExame = xExame.Substring(0, 38) + zData;
                        //    }
                        //}
                        //else 
                        if (complementar.dataUltimo >= this.DataExame.AddMonths(xMeses))
                        {
                            string zData;

                            //if (Exibir_Datas == true) zData = complementar.dataUltimo.ToString("dd/MM/yyyy");
                            //else zData = "  /  /    ";

                            if (Exibir_Datas == true)
                            {

                                if (xDataBranco != "" && xDataBranco != "  /  /    ")
                                {
                                    System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");

                                    if (xDesconsiderarCompl == false)
                                    {
                                        if (complementar.dataUltimo < System.Convert.ToDateTime(xDataBranco, ptBr))
                                            zData = "  /  /    ";
                                        else
                                            zData = complementar.dataUltimo.ToString("dd/MM/yyyy");
                                    }
                                    else
                                    {
                                        //if (System.Convert.ToDateTime(xDataBranco, ptBr) <= complementar.dataUltimo)
                                        if (this.DataExame.Date >= System.Convert.ToDateTime(xDataBranco, ptBr))
                                            zData = "  /  /    ";
                                        else
                                            zData = complementar.dataUltimo.ToString("dd/MM/yyyy");
                                    }
                                }
                                else
                                {
                                    zData = complementar.dataUltimo.ToString("dd/MM/yyyy");
                                }
                            }
                            else
                            {
                                zData = "  /  /    ";
                            }
                            xExame = xExame.Substring(0, 38) + zData;
                        }
                        else
                        {
                            if (xExame.Substring(39, 4) == "    ")
                            {
                                xExame = xExame.Substring(0, 40) + "/" + xExame.Substring(41, 2) + "/    "; //+ xExame.Substring(43);
                            }
                            else
                            {
                                xExame = xExame + "";
                            }

                        }
                    }


                    //ret.Append(i++.ToString()
                    //            + ". "
                    //            + xExame + "_"
                    //            + quebraLinha);


                    if (i < 10)
                    {
                        ret.Append(i++.ToString()
                                    + ". "
                                    + xExame);
                    }
                    else
                    {
                        ret.Append(i++.ToString()
                                    + "."
                                    + xExame);
                    }



                    if (i <= 10) ret.Append("   ");
                    else ret.Append("  ");

                    if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("GLOBAL") > 0)
                    {
                        if (i % 2 == 1 || complementares.Count <= 12)
                        {
                            ret.Append(quebraLinha);
                        }
                        else
                        {
                            ret.Append("   ");
                        }
                    }
                    else
                    {
                        if (i % 2 == 1 || complementares.Count <= 9)
                        {
                            ret.Append(quebraLinha);
                        }
                        else
                        {
                            ret.Append("   ");
                        }
                    }





                    //}
                }

            }

            //if (TemPreventivo)
            //    ret.Append("* (por iniciativa da empresa)");

            if (TemPreventivo)
            {
                //if (complementares.Count > 9)                {

                    //if (i > 0 && i % 2 == 1) i = i + 2;

                    //i = i / 2;
                    //for (int zCont = i; zCont < 10; zCont++)
                        ret.Append(System.Environment.NewLine);

                    ////ret.Append(System.Environment.NewLine + "* (este exame é uma exigência do cliente, sem relação com o seu risco ocupacional. Portanto, a sua realização não é obrigatória e caso exista alguma alteração, o mesmo não deverá ser levado em conta na sua aptidão laboral.)");   //por iniciativa da empresa)");
                    ////ret.Append(System.Environment.NewLine + "* (exame realizado por solicitação do cliente, sem relação com risco ocupacional deste trabalhador, sua realização não é obrigatória e caso tenha resultado anormal, não deverá ser considerado na aptidão ou inaptidão objeto deste exame.)");
               // }
            }


            return ret.ToString();
        }



        public string GetPlanejamentoExamesAso_Formatado_Global(Ghe ghe, bool ExamesSomentePeriodoPcmso, bool Exibir_Datas, string xDataBranco, bool xDesconsiderarCompl)
        {
            StringBuilder ret = new StringBuilder();

            string xExame;
            string xEspacos;

            int xMeses = 0;

            bool TemPreventivo = false;

            int i = 1;

            string quebraLinha;

            quebraLinha = "\n";

            xEspacos = "                                                ";
            xExame = "";

            complementares = ExamesComplementares(ExamesSomentePeriodoPcmso);

            foreach (ExameComplementar complementar in complementares)
            {
                //toxicológico - não sair no ASO
                if (complementar.IdExameDicionario == 100)
                {
                    continue;
                }


                //Preventivo somente se tiver exames feitos
                if (complementar.Preventivo)
                {
                    if (complementar.dataUltimo != new DateTime())
                    {
                        //ret.Append(i++.ToString()
                        //            + ". "
                        //            + complementar.Exame
                        //            + " - "
                        //            + complementar.dataUltimo.ToString("dd-MM-yyyy")
                        //            + " *"
                        //            + quebraLinha);
                        xExame = complementar.Exame.Trim();

                        if (xExame.Length > 0)
                        {
                            if (xExame.Length < xEspacos.Length)
                            {
                                xExame = xExame + xEspacos.Substring(1, xEspacos.Length - xExame.Length);
                            }



                            xMeses = -12;

                            IdExameDicionario.Find();

                            if (IdExameDicionario.Descricao.ToString() == "Periódico" || IdExameDicionario.Descricao.ToString() == "Mudança de Função" || IdExameDicionario.Descricao.ToUpper().ToString() == "DEMISSIONAL")
                            {
                                if (complementar.Periodicidade.IndexOf("Periódico (2 anos)") >= 0)
                                {
                                    xMeses = -24;
                                }
                                else if (complementar.Periodicidade.IndexOf("Periódico (3 anos)") >= 0)
                                {
                                    xMeses = -36;
                                }
                                else if (complementar.Periodicidade.IndexOf("Periódico (4 anos)") >= 0)
                                {
                                    xMeses = -48;
                                }
                                else if (complementar.Periodicidade.IndexOf("Periódico (5 anos)") >= 0)
                                {
                                    xMeses = -60;
                                }
                                else if (complementar.Periodicidade.IndexOf("Anual") > 0)
                                {
                                    xMeses = -12;
                                }
                                else if (complementar.Periodicidade.IndexOf("6 meses") > 0 || complementar.Periodicidade.ToUpper().IndexOf("SEMESTRAL") > 0)
                                {
                                    xMeses = -6;
                                }
                            }

                            this.IdEmpregado.Find();

                            if (complementar.Periodicidade.ToUpper().IndexOf("6 MESES DA ADMISSÃO") > 0)
                            {
                                if (this.DataExame <= this.IdEmpregado.hDT_ADM.AddMonths(6))
                                {
                                    xMeses = -6;
                                }
                                else if (this.DataExame > this.IdEmpregado.hDT_ADM.AddMonths(6) && this.DataExame <= this.IdEmpregado.hDT_ADM.AddMonths(12))
                                {
                                    if (complementar.dataUltimo >= this.IdEmpregado.hDT_ADM.AddMonths(6))
                                        xMeses = -12;
                                    else
                                        xMeses = -6;
                                }
                                else if (this.DataExame > this.IdEmpregado.hDT_ADM.AddMonths(12) && this.DataExame <= this.IdEmpregado.hDT_ADM.AddMonths(18))
                                {
                                    if (complementar.dataUltimo < this.IdEmpregado.hDT_ADM.AddMonths(6))
                                        xMeses = -6;
                                    else
                                        xMeses = -12;
                                }
                            }

                            if (complementar.dataUltimo >= this.DataExame.AddMonths(xMeses))
                            {
                                string zData;

                                if (complementar.IdExameDicionario == 6 || complementar.IdExameDicionario == -1518213838 || complementar.IdExameDicionario == 392974906
                                     || complementar.IdExameDicionario == 2144374867 || complementar.IdExameDicionario == -4 || complementar.IdExameDicionario == 2144374841)
                                {
                                    zData = "  /  /    ";
                                }
                                else
                                {
                                    if (Exibir_Datas == true)
                                    {

                                        if (xDataBranco != "" && xDataBranco != "  /  /    ")
                                        {
                                            System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");

                                            if (xDesconsiderarCompl == false)
                                            {
                                                if (complementar.dataUltimo < System.Convert.ToDateTime(xDataBranco, ptBr))
                                                    zData = "  /  /    ";
                                                else
                                                    zData = complementar.dataUltimo.ToString("dd/MM/yyyy");
                                            }
                                            else
                                            {
                                                //if (System.Convert.ToDateTime(xDataBranco, ptBr) <= complementar.dataUltimo)
                                                if (this.DataExame.Date >= System.Convert.ToDateTime(xDataBranco, ptBr))
                                                    zData = "  /  /    ";
                                                else
                                                    zData = complementar.dataUltimo.ToString("dd/MM/yyyy");
                                            }
                                        }
                                        else
                                        {
                                            zData = complementar.dataUltimo.ToString("dd/MM/yyyy");
                                        }
                                    }
                                    else
                                    {
                                        zData = "  /  /    ";
                                    }
                                }

                                xExame = xExame.Substring(0, 38) + zData;
                            }
                            else
                            {
                                if (xExame.Substring(39, 4) == "    ")
                                {
                                    xExame = xExame.Substring(0, 40) + "/" + xExame.Substring(41, 2) + "/    "; //"/" + xExame.Substring(43);
                                }
                                else
                                {
                                    xExame = xExame + "";
                                }

                            }

                        }

                        //ret.Append(i++.ToString()
                        //            + ". "
                        //            + xExame + "_"
                        //            + " *"
                        //            + quebraLinha);

                        if (i < 10)
                        {
                            ret.Append(i++.ToString()
                                        + ". "
                                        + xExame);
                        }
                        else
                        {
                            ret.Append(i++.ToString()
                                        + "."
                                        + xExame);
                        }


                        if (i <= 12) ret.Append("   ");
                        else ret.Append("  ");

                        if (i % 2 == 1 || complementares.Count <= 11)
                        {
                            ret.Append(quebraLinha);
                        }
                        else
                        {
                            ret.Append("   ");
                        }


                        TemPreventivo = true;
                    }
                    else if (!this.DescExameComplPreventivo)
                    {
                        //ret.Append(i++.ToString()
                        //            + ". "
                        //            + complementar.Exame
                        //            + "  ___/___/______"
                        //            + " *"
                        //            + quebraLinha);
                        xExame = complementar.Exame.Trim();

                        if (xExame.Length > 0)
                        {
                            if (xExame.Length < xEspacos.Length)
                            {
                                xExame = xExame + xEspacos.Substring(1, xEspacos.Length - xExame.Length);
                            }


                            xMeses = -12;

                            IdExameDicionario.Find();

                            if (IdExameDicionario.Descricao.ToString() == "Periódico" || IdExameDicionario.Descricao.ToString() == "Mudança de Função" || IdExameDicionario.Descricao.ToUpper().ToString() == "DEMISSIONAL")
                            {
                                if (complementar.Periodicidade.IndexOf("Periódico (2 anos)") >= 0)
                                {
                                    xMeses = -24;
                                }
                                else if (complementar.Periodicidade.IndexOf("Periódico (3 anos)") >= 0)
                                {
                                    xMeses = -36;
                                }
                                else if (complementar.Periodicidade.IndexOf("Periódico (4 anos)") >= 0)
                                {
                                    xMeses = -48;
                                }
                                else if (complementar.Periodicidade.IndexOf("Periódico (5 anos)") >= 0)
                                {
                                    xMeses = -60;
                                }
                                else if (complementar.Periodicidade.IndexOf("Anual") > 0)
                                {
                                    xMeses = -12;
                                }
                                else if (complementar.Periodicidade.IndexOf("6 meses") > 0 || complementar.Periodicidade.ToUpper().IndexOf("SEMESTRAL") > 0)
                                {
                                    xMeses = -6;
                                }
                            }

                            this.IdEmpregado.Find();

                            if (complementar.Periodicidade.ToUpper().IndexOf("6 MESES DA ADMISSÃO") > 0)
                            {
                                if (this.DataExame <= this.IdEmpregado.hDT_ADM.AddMonths(6))
                                {
                                    xMeses = -6;
                                }
                                else if (this.DataExame > this.IdEmpregado.hDT_ADM.AddMonths(6) && this.DataExame <= this.IdEmpregado.hDT_ADM.AddMonths(12))
                                {
                                    if (complementar.dataUltimo >= this.IdEmpregado.hDT_ADM.AddMonths(6))
                                        xMeses = -12;
                                    else
                                        xMeses = -6;
                                }
                                else if (this.DataExame > this.IdEmpregado.hDT_ADM.AddMonths(12) && this.DataExame <= this.IdEmpregado.hDT_ADM.AddMonths(18))
                                {
                                    if (complementar.dataUltimo < this.IdEmpregado.hDT_ADM.AddMonths(6))
                                        xMeses = -6;
                                    else
                                        xMeses = -12;
                                }
                            }


                            if (complementar.dataUltimo >= this.DataExame.AddMonths(xMeses))
                            {
                                string zData;

                                if (complementar.IdExameDicionario == 6 || complementar.IdExameDicionario == -1518213838 || complementar.IdExameDicionario == 392974906
                                    || complementar.IdExameDicionario == 2144374867 || complementar.IdExameDicionario == -4 || complementar.IdExameDicionario == 2144374841)
                                {
                                    zData = "  /  /    ";
                                }
                                else
                                {

                                    if (Exibir_Datas == true)
                                    {

                                        if (xDataBranco != "" && xDataBranco != "  /  /    ")
                                        {
                                            System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");

                                            if (xDesconsiderarCompl == false)
                                            {
                                                if (complementar.dataUltimo < System.Convert.ToDateTime(xDataBranco, ptBr))
                                                    zData = "  /  /    ";
                                                else
                                                    zData = complementar.dataUltimo.ToString("dd/MM/yyyy");
                                            }
                                            else
                                            {
                                                //if (System.Convert.ToDateTime(xDataBranco, ptBr) <= complementar.dataUltimo)
                                                if (this.DataExame.Date >= System.Convert.ToDateTime(xDataBranco, ptBr))
                                                    zData = "  /  /    ";
                                                else
                                                    zData = complementar.dataUltimo.ToString("dd/MM/yyyy");
                                            }
                                        }
                                        else
                                        {
                                            zData = complementar.dataUltimo.ToString("dd/MM/yyyy");
                                        }
                                    }
                                    else
                                    {
                                        zData = "  /  /    ";
                                    }
                                }

                                xExame = xExame.Substring(0, 38) + zData;
                            }
                            else
                            {
                                if (xExame.Substring(39, 4) == "    ")
                                {
                                    xExame = xExame.Substring(0, 40) + "/" + xExame.Substring(41, 2) + "/    "; //"/" + xExame.Substring(43);
                                }
                                else
                                {
                                    xExame = xExame + "";
                                }
                            }

                        }

                        if (i < 10)
                        {
                            ret.Append(i++.ToString()
                                        + ". "
                                        + xExame);
                        }
                        else
                        {
                            ret.Append(i++.ToString()
                                        + "."
                                        + xExame);
                        }


                        if (i <= 12) ret.Append("   ");
                        else ret.Append("  ");

                        if (i % 2 == 1 || complementares.Count <= 11)
                        {
                            ret.Append(quebraLinha);
                        }
                        else
                        {
                            ret.Append("   ");
                        }



                        TemPreventivo = true;
                    }
                }
                else
                {


                    xExame = complementar.Exame.Trim();

                    if (xExame.Length > 0)
                    {
                        if (xExame.Length < xEspacos.Length)
                        {
                            xExame = xExame + xEspacos.Substring(1, xEspacos.Length - xExame.Length);
                        }

                        xMeses = -12;

                        IdExameDicionario.Find();

                        if (IdExameDicionario.Descricao.ToString() == "Periódico" || IdExameDicionario.Descricao.ToString() == "Mudança de Função" || IdExameDicionario.Descricao.ToUpper().ToString() == "DEMISSIONAL")
                        {
                            if (complementar.Periodicidade.IndexOf("Periódico (2 anos)") >= 0)
                            {
                                xMeses = -24;
                            }
                            else if (complementar.Periodicidade.IndexOf("Periódico (3 anos)") >= 0)
                            {
                                xMeses = -36;
                            }
                            else if (complementar.Periodicidade.IndexOf("Periódico (4 anos)") >= 0)
                            {
                                xMeses = -48;
                            }
                            else if (complementar.Periodicidade.IndexOf("Periódico (5 anos)") >= 0)
                            {
                                xMeses = -60;
                            }
                            else if (complementar.Periodicidade.IndexOf("Anual") > 0)
                            {
                                xMeses = -12;
                            }
                            else if (complementar.Periodicidade.IndexOf("6 meses") > 0 || complementar.Periodicidade.ToUpper().IndexOf("SEMESTRAL") > 0)
                            {
                                xMeses = -6;
                            }
                        }

                        this.IdEmpregado.Find();

                        if (complementar.Periodicidade.ToUpper().IndexOf("6 MESES DA ADMISSÃO") > 0)
                        {
                            if (this.DataExame <= this.IdEmpregado.hDT_ADM.AddMonths(6))
                            {
                                xMeses = -6;
                            }
                            else if (this.DataExame > this.IdEmpregado.hDT_ADM.AddMonths(6) && this.DataExame <= this.IdEmpregado.hDT_ADM.AddMonths(12))
                            {
                                if (complementar.dataUltimo >= this.IdEmpregado.hDT_ADM.AddMonths(6))
                                    xMeses = -12;
                                else
                                    xMeses = -6;
                            }
                            else if (this.DataExame > this.IdEmpregado.hDT_ADM.AddMonths(12) && this.DataExame <= this.IdEmpregado.hDT_ADM.AddMonths(18))
                            {
                                if (complementar.dataUltimo < this.IdEmpregado.hDT_ADM.AddMonths(6))
                                    xMeses = -6;
                                else
                                    xMeses = -12;
                            }
                        }



                        if (complementar.dataUltimo >= this.DataExame.AddMonths(xMeses))
                        {
                            string zData;

                            //                                if (Exibir_Datas == true) zData = complementar.dataUltimo.ToString("dd/MM/yyyy");
                            //                                else zData = "  /  /    ";

                            if (complementar.IdExameDicionario == 6 || complementar.IdExameDicionario == -1518213838 || complementar.IdExameDicionario == 392974906
                                    || complementar.IdExameDicionario == 2144374867 || complementar.IdExameDicionario == -4 || complementar.IdExameDicionario == 2144374841)
                            {
                                zData = "  /  /    ";
                            }
                            else
                            {

                                if (Exibir_Datas == true)
                                {

                                    if (xDataBranco != "" && xDataBranco != "  /  /    ")
                                    {
                                        System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");

                                        if (xDesconsiderarCompl == false)
                                        {
                                            if (complementar.dataUltimo < System.Convert.ToDateTime(xDataBranco, ptBr))
                                                zData = "  /  /    ";
                                            else
                                                zData = complementar.dataUltimo.ToString("dd/MM/yyyy");
                                        }
                                        else
                                        {
                                            //if (System.Convert.ToDateTime(xDataBranco, ptBr) <= complementar.dataUltimo)
                                            if (this.DataExame.Date >= System.Convert.ToDateTime(xDataBranco, ptBr))
                                                zData = "  /  /    ";
                                            else
                                                zData = complementar.dataUltimo.ToString("dd/MM/yyyy");
                                        }
                                    }
                                    else
                                    {
                                        zData = complementar.dataUltimo.ToString("dd/MM/yyyy");
                                    }
                                }
                                else
                                {
                                    zData = "  /  /    ";
                                }
                            }

                            xExame = xExame.Substring(0, 38) + zData;
                        }
                        else
                        {
                            if (xExame.Substring(39, 4) == "    ")
                            {
                                xExame = xExame.Substring(0, 40) + "/" + xExame.Substring(41, 2) + "/    "; //"/" + xExame.Substring(43);
                            }
                            else
                            {
                                xExame = xExame + "";
                            }

                        }
                    }



                    if (i < 10)
                    {
                        ret.Append(i++.ToString()
                                    + ". "
                                    + xExame);
                    }
                    else
                    {
                        ret.Append(i++.ToString()
                                    + "."
                                    + xExame);
                    }


                    if (i <= 12) ret.Append("   ");
                    else ret.Append("  ");

                    if (i % 2 == 1 || complementares.Count <= 11)
                    {
                        ret.Append(quebraLinha);
                    }
                    else
                    {
                        ret.Append("   ");
                    }


                    //}
                }

            }

            if (TemPreventivo)
            {

                ret.Append(System.Environment.NewLine);

            }


            return ret.ToString();
        }






        public string GetPlanejamentoExamesAso_Formatado_Desconsiderar_Global(Ghe ghe, bool ExamesSomentePeriodoPcmso, bool Exibir_Datas, Clinico clinico, int zDias_Desconsiderar)
        {
            StringBuilder ret = new StringBuilder();

            string xExame;
            string xEspacos;



            int xMeses = 0;

            bool TemPreventivo = false;

            int i = 1;

            string quebraLinha;

            quebraLinha = "\n";

            xEspacos = "                                                ";
            xExame = "";


            string xDataBranco = "";
            bool xDesconsiderarCompl = false;

            complementares = ExamesComplementares(ExamesSomentePeriodoPcmso);

            foreach (ExameComplementar complementar in complementares)
            {
                //toxicológico - não sair no ASO
                if (complementar.IdExameDicionario == 100)
                {
                    continue;
                }


                System.Globalization.CultureInfo ptBr2 = new System.Globalization.CultureInfo("pt-Br");

                ExamePlanejamento xPlanej = new ExamePlanejamento();
                clinico.IdEmpregado.Find();
                clinico.IdExameDicionario.Find();
                clinico.IdPcmso.Find();

                xPlanej.Find(" IdEmpregado =" + clinico.IdEmpregado.Id.ToString() + " and IdExameDicionario = " + complementar.IdExameDicionario.ToString() + " and IdPCMSO = " + clinico.IdPcmso.Id.ToString());

                if (xPlanej.Id == 0)
                {
                    xDataBranco = clinico.DataExame.AddDays(zDias_Desconsiderar * (-1)).ToString("dd/MM/yyyy", ptBr2);
                }
                else
                {
                    if (xPlanej.DataVencimento == new DateTime())
                        xDataBranco = this.DataExame.ToString("dd/MM/yyyy", ptBr2);  //"  /  /    ";
                    else
                        xDataBranco = xPlanej.DataVencimento.AddDays(zDias_Desconsiderar * (-1)).ToString("dd/MM/yyyy", ptBr2);
                }

                xDesconsiderarCompl = true;




                //Preventivo somente se tiver exames feitos
                if (complementar.Preventivo)
                {
                    if (complementar.dataUltimo != new DateTime())
                    {
                        //ret.Append(i++.ToString()
                        //            + ". "
                        //            + complementar.Exame
                        //            + " - "
                        //            + complementar.dataUltimo.ToString("dd-MM-yyyy")
                        //            + " *"
                        //            + quebraLinha);
                        xExame = complementar.Exame.Trim();

                        if (xExame.Length > 0)
                        {
                            if (xExame.Length < xEspacos.Length)
                            {
                                xExame = xExame + xEspacos.Substring(1, xEspacos.Length - xExame.Length);
                            }



                            xMeses = -12;

                            IdExameDicionario.Find();

                            if (IdExameDicionario.Descricao.ToString() == "Periódico" || IdExameDicionario.Descricao.ToString() == "Mudança de Função" || IdExameDicionario.Descricao.ToUpper().ToString() == "DEMISSIONAL")
                            {
                                if (complementar.Periodicidade.IndexOf("Periódico (2 anos)") >= 0)
                                {
                                    xMeses = -24;
                                }
                                else if (complementar.Periodicidade.IndexOf("Periódico (3 anos)") >= 0)
                                {
                                    xMeses = -36;
                                }
                                else if (complementar.Periodicidade.IndexOf("Periódico (4 anos)") >= 0)
                                {
                                    xMeses = -48;
                                }
                                else if (complementar.Periodicidade.IndexOf("Periódico (5 anos)") >= 0)
                                {
                                    xMeses = -60;
                                }
                                else if (complementar.Periodicidade.IndexOf("Anual") > 0)
                                {
                                    xMeses = -12;
                                }
                                else if (complementar.Periodicidade.IndexOf("6 meses") > 0 || complementar.Periodicidade.ToUpper().IndexOf("SEMESTRAL") > 0)
                                {
                                    xMeses = -6;
                                }
                            }

                            this.IdEmpregado.Find();

                            if (complementar.Periodicidade.ToUpper().IndexOf("6 MESES DA ADMISSÃO") > 0)
                            {
                                if (this.DataExame <= this.IdEmpregado.hDT_ADM.AddMonths(6))
                                {
                                    xMeses = -6;
                                }
                                else if (this.DataExame > this.IdEmpregado.hDT_ADM.AddMonths(6) && this.DataExame <= this.IdEmpregado.hDT_ADM.AddMonths(12))
                                {
                                    if (complementar.dataUltimo >= this.IdEmpregado.hDT_ADM.AddMonths(6))
                                        xMeses = -12;
                                    else
                                        xMeses = -6;
                                }
                                else if (this.DataExame > this.IdEmpregado.hDT_ADM.AddMonths(12) && this.DataExame <= this.IdEmpregado.hDT_ADM.AddMonths(18))
                                {
                                    if (complementar.dataUltimo < this.IdEmpregado.hDT_ADM.AddMonths(6))
                                        xMeses = -6;
                                    else
                                        xMeses = -12;
                                }
                            }



                            if (complementar.dataUltimo >= this.DataExame.AddMonths(xMeses))
                            {
                                string zData;

                                if (complementar.IdExameDicionario == 6 || complementar.IdExameDicionario == -1518213838 || complementar.IdExameDicionario == 392974906
                                   || complementar.IdExameDicionario == 2144374867 || complementar.IdExameDicionario == -4 || complementar.IdExameDicionario == 2144374841)
                                {
                                    zData = "  /  /    ";
                                }
                                else
                                {

                                    if (Exibir_Datas == true)
                                    {

                                        if (xDataBranco != "" && xDataBranco != "  /  /    ")
                                        {
                                            System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");

                                            if (xDesconsiderarCompl == false)
                                            {
                                                if (complementar.dataUltimo < System.Convert.ToDateTime(xDataBranco, ptBr))
                                                    zData = "  /  /    ";
                                                else
                                                    zData = complementar.dataUltimo.ToString("dd/MM/yyyy");
                                            }
                                            else
                                            {
                                                //if (System.Convert.ToDateTime(xDataBranco, ptBr) <= complementar.dataUltimo)
                                                if (this.DataExame.Date >= System.Convert.ToDateTime(xDataBranco, ptBr))
                                                    zData = "  /  /    ";
                                                else
                                                    zData = complementar.dataUltimo.ToString("dd/MM/yyyy");
                                            }
                                        }
                                        else
                                        {
                                            zData = complementar.dataUltimo.ToString("dd/MM/yyyy");
                                        }
                                    }
                                    else
                                    {
                                        zData = "  /  /    ";
                                    }
                                }

                                xExame = xExame.Substring(0, 38) + zData;
                            }
                            else
                            {
                                if (xExame.Substring(39, 4) == "    ")
                                {
                                    xExame = xExame.Substring(0, 40) + "/" + xExame.Substring(41, 2) + "/    "; //"/" + xExame.Substring(43);
                                }
                                else
                                {
                                    xExame = xExame + "";
                                }

                            }

                        }



                        if (i < 10)
                        {
                            ret.Append(i++.ToString()
                                        + ". "
                                        + xExame);
                        }
                        else
                        {
                            ret.Append(i++.ToString()
                                        + "."
                                        + xExame);
                        }


                        if (i <= 12) ret.Append("   ");
                        else ret.Append("  ");

                        if (i % 2 == 1 || complementares.Count <= 11)
                        {
                            ret.Append(quebraLinha);
                        }
                        else
                        {
                            ret.Append("   ");
                        }


                        TemPreventivo = true;
                    }
                    else if (!this.DescExameComplPreventivo)
                    {

                        xExame = complementar.Exame.Trim();

                        if (xExame.Length > 0)
                        {
                            if (xExame.Length < xEspacos.Length)
                            {
                                xExame = xExame + xEspacos.Substring(1, xEspacos.Length - xExame.Length);
                            }


                            xMeses = -12;

                            IdExameDicionario.Find();

                            if (IdExameDicionario.Descricao.ToString() == "Periódico" || IdExameDicionario.Descricao.ToString() == "Mudança de Função" || IdExameDicionario.Descricao.ToUpper().ToString() == "DEMISSIONAL")
                            {
                                if (complementar.Periodicidade.IndexOf("Periódico (2 anos)") >= 0)
                                {
                                    xMeses = -24;
                                }
                                else if (complementar.Periodicidade.IndexOf("Periódico (3 anos)") >= 0)
                                {
                                    xMeses = -36;
                                }
                                else if (complementar.Periodicidade.IndexOf("Periódico (4 anos)") >= 0)
                                {
                                    xMeses = -48;
                                }
                                else if (complementar.Periodicidade.IndexOf("Periódico (5 anos)") >= 0)
                                {
                                    xMeses = -60;
                                }
                                else if (complementar.Periodicidade.IndexOf("Anual") > 0)
                                {
                                    xMeses = -12;
                                }
                                else if (complementar.Periodicidade.IndexOf("6 meses") > 0 || complementar.Periodicidade.ToUpper().IndexOf("SEMESTRAL") > 0)
                                {
                                    xMeses = -6;
                                }
                            }


                            this.IdEmpregado.Find();

                            if (complementar.Periodicidade.ToUpper().IndexOf("6 MESES DA ADMISSÃO") > 0)
                            {
                                if (this.DataExame <= this.IdEmpregado.hDT_ADM.AddMonths(6))
                                {
                                    xMeses = -6;
                                }
                                else if (this.DataExame > this.IdEmpregado.hDT_ADM.AddMonths(6) && this.DataExame <= this.IdEmpregado.hDT_ADM.AddMonths(12))
                                {
                                    if (complementar.dataUltimo >= this.IdEmpregado.hDT_ADM.AddMonths(6))
                                        xMeses = -12;
                                    else
                                        xMeses = -6;
                                }
                                else if (this.DataExame > this.IdEmpregado.hDT_ADM.AddMonths(12) && this.DataExame <= this.IdEmpregado.hDT_ADM.AddMonths(18))
                                {
                                    if (complementar.dataUltimo < this.IdEmpregado.hDT_ADM.AddMonths(6))
                                        xMeses = -6;
                                    else
                                        xMeses = -12;
                                }
                            }



                            if (complementar.dataUltimo >= this.DataExame.AddMonths(xMeses))
                            {
                                string zData;

                                if (complementar.IdExameDicionario == 6 || complementar.IdExameDicionario == -1518213838 || complementar.IdExameDicionario == 392974906
                                   || complementar.IdExameDicionario == 2144374867 || complementar.IdExameDicionario == -4 || complementar.IdExameDicionario == 2144374841)
                                {
                                    zData = "  /  /    ";
                                }
                                else
                                {

                                    if (Exibir_Datas == true)
                                    {

                                        if (xDataBranco != "" && xDataBranco != "  /  /    ")
                                        {
                                            System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");

                                            if (xDesconsiderarCompl == false)
                                            {
                                                if (complementar.dataUltimo < System.Convert.ToDateTime(xDataBranco, ptBr))
                                                    zData = "  /  /    ";
                                                else
                                                    zData = complementar.dataUltimo.ToString("dd/MM/yyyy");
                                            }
                                            else
                                            {
                                                //if (System.Convert.ToDateTime(xDataBranco, ptBr) <= complementar.dataUltimo)
                                                if (this.DataExame.Date >= System.Convert.ToDateTime(xDataBranco, ptBr))
                                                    zData = "  /  /    ";
                                                else
                                                    zData = complementar.dataUltimo.ToString("dd/MM/yyyy");
                                            }
                                        }
                                        else
                                        {
                                            zData = complementar.dataUltimo.ToString("dd/MM/yyyy");
                                        }
                                    }
                                    else
                                    {
                                        zData = "  /  /    ";
                                    }
                                }


                                xExame = xExame.Substring(0, 38) + zData;
                            }
                            else
                            {
                                if (xExame.Substring(39, 4) == "    ")
                                {
                                    xExame = xExame.Substring(0, 40) + "/" + xExame.Substring(41, 2) + "/    "; //"/" + xExame.Substring(43);
                                }
                                else
                                {
                                    xExame = xExame + "";
                                }
                            }

                        }

                        if (i < 10)
                        {
                            ret.Append(i++.ToString()
                                        + ". "
                                        + xExame);
                        }
                        else
                        {
                            ret.Append(i++.ToString()
                                        + "."
                                        + xExame);
                        }


                        if (i <= 12) ret.Append("   ");
                        else ret.Append("  ");

                        if (i % 2 == 1 || complementares.Count <= 11)
                        {
                            ret.Append(quebraLinha);
                        }
                        else
                        {
                            ret.Append("   ");
                        }



                        TemPreventivo = true;
                    }
                }
                else
                {


                    xExame = complementar.Exame.Trim();

                    if (xExame.Length > 0)
                    {
                        if (xExame.Length < xEspacos.Length)
                        {
                            xExame = xExame + xEspacos.Substring(1, xEspacos.Length - xExame.Length);
                        }

                        xMeses = -12;

                        IdExameDicionario.Find();

                        if (IdExameDicionario.Descricao.ToString() == "Periódico" || IdExameDicionario.Descricao.ToString() == "Mudança de Função" || IdExameDicionario.Descricao.ToUpper().ToString() == "DEMISSIONAL")
                        {
                            if (complementar.Periodicidade.IndexOf("Periódico (2 anos)") >= 0)
                            {
                                xMeses = -24;
                            }
                            else if (complementar.Periodicidade.IndexOf("Periódico (3 anos)") >= 0)
                            {
                                xMeses = -36;
                            }
                            else if (complementar.Periodicidade.IndexOf("Periódico (4 anos)") >= 0)
                            {
                                xMeses = -48;
                            }
                            else if (complementar.Periodicidade.IndexOf("Periódico (5 anos)") >= 0)
                            {
                                xMeses = -60;
                            }
                            else if (complementar.Periodicidade.IndexOf("Anual") > 0)
                            {
                                xMeses = -12;
                            }
                            else if (complementar.Periodicidade.IndexOf("6 meses") > 0 || complementar.Periodicidade.ToUpper().IndexOf("SEMESTRAL") > 0)
                            {
                                xMeses = -6;
                            }
                        }

                        this.IdEmpregado.Find();

                        if (complementar.Periodicidade.ToUpper().IndexOf("6 MESES DA ADMISSÃO") > 0)
                        {
                            if (this.DataExame <= this.IdEmpregado.hDT_ADM.AddMonths(6))
                            {
                                xMeses = -6;
                            }
                            else if (this.DataExame > this.IdEmpregado.hDT_ADM.AddMonths(6) && this.DataExame <= this.IdEmpregado.hDT_ADM.AddMonths(12))
                            {
                                if (complementar.dataUltimo >= this.IdEmpregado.hDT_ADM.AddMonths(6))
                                    xMeses = -12;
                                else
                                    xMeses = -6;
                            }
                            else if (this.DataExame > this.IdEmpregado.hDT_ADM.AddMonths(12) && this.DataExame <= this.IdEmpregado.hDT_ADM.AddMonths(18))
                            {
                                if (complementar.dataUltimo < this.IdEmpregado.hDT_ADM.AddMonths(6))
                                    xMeses = -6;
                                else
                                    xMeses = -12;
                            }
                        }



                        if (complementar.dataUltimo >= this.DataExame.AddMonths(xMeses))
                        {
                            string zData;

                            //                                if (Exibir_Datas == true) zData = complementar.dataUltimo.ToString("dd/MM/yyyy");
                            //                                else zData = "  /  /    ";


                            if (complementar.IdExameDicionario == 6 || complementar.IdExameDicionario == -1518213838 || complementar.IdExameDicionario == 392974906
                                   || complementar.IdExameDicionario == 2144374867 || complementar.IdExameDicionario == -4 || complementar.IdExameDicionario == 2144374841)
                            {
                                zData = "  /  /    ";
                            }
                            else
                            {

                                if (Exibir_Datas == true)
                                {

                                    if (xDataBranco != "" && xDataBranco != "  /  /    ")
                                    {
                                        System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");

                                        if (xDesconsiderarCompl == false)
                                        {
                                            if (complementar.dataUltimo < System.Convert.ToDateTime(xDataBranco, ptBr))
                                                zData = "  /  /    ";
                                            else
                                                zData = complementar.dataUltimo.ToString("dd/MM/yyyy");
                                        }
                                        else
                                        {
                                            //if (System.Convert.ToDateTime(xDataBranco, ptBr) <= complementar.dataUltimo)
                                            if (this.DataExame.Date >= System.Convert.ToDateTime(xDataBranco, ptBr))
                                                zData = "  /  /    ";
                                            else
                                                zData = complementar.dataUltimo.ToString("dd/MM/yyyy");
                                        }
                                    }
                                    else
                                    {
                                        zData = complementar.dataUltimo.ToString("dd/MM/yyyy");
                                    }
                                }
                                else
                                {
                                    zData = "  /  /    ";
                                }
                            }

                            xExame = xExame.Substring(0, 38) + zData;
                        }
                        else
                        {
                            if (xExame.Substring(39, 4) == "    ")
                            {
                                xExame = xExame.Substring(0, 40) + "/" + xExame.Substring(41, 2) + "/    "; //"/" + xExame.Substring(43);
                            }
                            else
                            {
                                xExame = xExame + "";
                            }

                        }
                    }


                    //ret.Append(i++.ToString()
                    //            + ". "
                    //            + xExame + "_"
                    //            + quebraLinha);


                    if (i < 10)
                    {
                        ret.Append(i++.ToString()
                                    + ". "
                                    + xExame);
                    }
                    else
                    {
                        ret.Append(i++.ToString()
                                    + "."
                                    + xExame);
                    }


                    if (i <= 12) ret.Append("   ");
                    else ret.Append("  ");

                    if (i % 2 == 1 || complementares.Count <= 11)
                    {
                        ret.Append(quebraLinha);
                    }
                    else
                    {
                        ret.Append("   ");
                    }





                    //}
                }

            }

            if (TemPreventivo)
            {

                ret.Append(System.Environment.NewLine);

            }


            return ret.ToString();
        }




        public string GetPlanejamentoExamesAso_Formatado_Desconsiderar(Ghe ghe, bool ExamesSomentePeriodoPcmso, bool Exibir_Datas, Clinico clinico, int zDias_Desconsiderar)
        {
            StringBuilder ret = new StringBuilder();

            string xExame;
            string xEspacos;

            int xMeses = 0;

            bool TemPreventivo = false;

            int i = 1;

            string quebraLinha;

            quebraLinha = "\n";

            xEspacos = "                                                ";
            xExame = "";

            complementares = ExamesComplementares(ExamesSomentePeriodoPcmso);


            string xDataBranco = "";
            bool xDesconsiderarCompl = false;

            complementares = ExamesComplementares(ExamesSomentePeriodoPcmso);

            foreach (ExameComplementar complementar in complementares)
            {
                //toxicológico - não sair no ASO
                if (complementar.IdExameDicionario == 100)
                {
                    continue;
                }

                System.Globalization.CultureInfo ptBr2 = new System.Globalization.CultureInfo("pt-Br");

                ExamePlanejamento xPlanej = new ExamePlanejamento();
                clinico.IdEmpregado.Find();
                clinico.IdExameDicionario.Find();
                clinico.IdPcmso.Find();

                xPlanej.Find(" IdEmpregado =" + clinico.IdEmpregado.Id.ToString() + " and IdExameDicionario = " + complementar.IdExameDicionario.ToString() + " and IdPCMSO = " + clinico.IdPcmso.Id.ToString());

                if (xPlanej.Id == 0)
                {
                    xDataBranco = clinico.DataExame.AddDays(zDias_Desconsiderar * (-1)).ToString("dd/MM/yyyy", ptBr2);
                }
                else
                {
                    if (xPlanej.DataVencimento == new DateTime())
                        xDataBranco = this.DataExame.ToString("dd/MM/yyyy", ptBr2);  //= "  /  /    ";
                    else
                        xDataBranco = xPlanej.DataVencimento.AddDays(zDias_Desconsiderar * (-1)).ToString("dd/MM/yyyy", ptBr2);
                }

                xDesconsiderarCompl = true;

                //Preventivo somente se tiver exames feitos
                if (complementar.Preventivo)
                {
                    if (complementar.dataUltimo != new DateTime())
                    {
                        //ret.Append(i++.ToString()
                        //            + ". "
                        //            + complementar.Exame
                        //            + " - "
                        //            + complementar.dataUltimo.ToString("dd-MM-yyyy")
                        //            + " *"
                        //            + quebraLinha);
                        xExame = complementar.Exame.Trim();

                        if (xExame.Length > 0)
                        {
                            if (xExame.Length < xEspacos.Length)
                            {
                                xExame = xExame + xEspacos.Substring(1, xEspacos.Length - xExame.Length);
                            }


                            xMeses = -12;

                            IdExameDicionario.Find();

                            if (IdExameDicionario.Descricao.ToString() == "Periódico" || IdExameDicionario.Descricao.ToString() == "Mudança de Função")
                            {
                                if (complementar.Periodicidade.IndexOf("Periódico (2 anos)") >= 0)
                                {
                                    xMeses = -24;
                                }
                                else if (complementar.Periodicidade.IndexOf("Periódico (3 anos)") >= 0)
                                {
                                    xMeses = -36;
                                }
                                else if (complementar.Periodicidade.IndexOf("Periódico (4 anos)") >= 0)
                                {
                                    xMeses = -48;
                                }
                                else if (complementar.Periodicidade.IndexOf("Periódico (5 anos)") >= 0)
                                {
                                    xMeses = -60;
                                }
                                else if (complementar.Periodicidade.IndexOf("Anual") > 0)
                                {
                                    xMeses = -12;
                                }
                                else if (complementar.Periodicidade.IndexOf("6 meses") > 0 || complementar.Periodicidade.ToUpper().IndexOf("SEMESTRAL") > 0)
                                {
                                    xMeses = -6;
                                }

                            }


                            this.IdEmpregado.Find();

                            if (complementar.Periodicidade.ToUpper().IndexOf("6 MESES DA ADMISSÃO") > 0)
                            {
                                if (this.DataExame <= this.IdEmpregado.hDT_ADM.AddMonths(6))
                                {
                                    xMeses = -6;
                                }
                                else if (this.DataExame > this.IdEmpregado.hDT_ADM.AddMonths(6) && this.DataExame <= this.IdEmpregado.hDT_ADM.AddMonths(12))
                                {
                                    if (complementar.dataUltimo >= this.IdEmpregado.hDT_ADM.AddMonths(6))
                                        xMeses = -12;
                                    else
                                        xMeses = -6;
                                }
                                else if (this.DataExame > this.IdEmpregado.hDT_ADM.AddMonths(12) && this.DataExame <= this.IdEmpregado.hDT_ADM.AddMonths(18))
                                {
                                    if (complementar.dataUltimo < this.IdEmpregado.hDT_ADM.AddMonths(6))
                                        xMeses = -6;
                                    else
                                        xMeses = -12;
                                }
                            }


                            ////audiometria - regra única - 120 dias
                            //this.IdExameDicionario.Find();
                            //if (this.IdExameDicionario.Id == 2 && xExame.ToUpper().Trim() == "AUDIOMETRIA")
                            //{
                            //    if (complementar.dataUltimo.AddDays(120) < this.DataExame)
                            //    {
                            //        string zData = "  /  /    ";
                            //        xExame = xExame.Substring(0, 38) + zData;
                            //    }
                            //    else
                            //    {
                            //        System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");
                            //        string zData = complementar.dataUltimo.ToString("dd/MM/yyyy");
                            //        xExame = xExame.Substring(0, 38) + zData;
                            //    }
                            //}
                            //else 
                            if (complementar.dataUltimo >= this.DataExame.AddMonths(xMeses))
                            {
                                string zData;

                                //if (Exibir_Datas == true) zData = complementar.dataUltimo.ToString("dd/MM/yyyy");
                                //else zData = "  /  /    ";

                                if (Exibir_Datas == true)
                                {

                                    if (xDataBranco != "" && xDataBranco != "  /  /    ")
                                    {
                                        System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");

                                        if (xDesconsiderarCompl == false)
                                        {
                                            if (complementar.dataUltimo < System.Convert.ToDateTime(xDataBranco, ptBr))
                                                zData = "  /  /    ";
                                            else
                                                zData = complementar.dataUltimo.ToString("dd/MM/yyyy");
                                        }
                                        else
                                        {
                                            //if (System.Convert.ToDateTime(xDataBranco, ptBr) <= complementar.dataUltimo)
                                            if (this.DataExame.Date >= System.Convert.ToDateTime(xDataBranco, ptBr))
                                                zData = "  /  /    ";
                                            else
                                                zData = complementar.dataUltimo.ToString("dd/MM/yyyy");
                                        }
                                    }
                                    else
                                    {
                                        zData = complementar.dataUltimo.ToString("dd/MM/yyyy");
                                    }
                                }
                                else
                                {
                                    zData = "  /  /    ";
                                }

                                xExame = xExame.Substring(0, 38) + zData;
                            }
                            else
                            {
                                if (xExame.Substring(39, 4) == "    ")
                                {
                                    xExame = xExame.Substring(0, 40) + "/" + xExame.Substring(41, 2) + "/    "; //"/" + xExame.Substring(43);
                                }
                                else
                                {
                                    xExame = xExame + "";
                                }

                            }

                        }

                        //ret.Append(i++.ToString()
                        //            + ". "
                        //            + xExame + "_"
                        //            + " *"
                        //            + quebraLinha);

                        if (i < 10)
                        {
                            ret.Append(i++.ToString()
                                        + ". "
                                        + xExame);
                        }
                        else
                        {
                            ret.Append(i++.ToString()
                                        + "."
                                        + xExame);
                        }



                        if (i <= 10) ret.Append("   ");
                        else ret.Append("  ");

                        if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("GLOBAL") > 0)
                        {
                            if (i % 2 == 1 || complementares.Count <= 12)
                            {
                                ret.Append(quebraLinha);
                            }
                            else
                            {
                                ret.Append("   ");
                            }
                        }
                        else
                        {
                            if (i % 2 == 1 || complementares.Count <= 9)
                            {
                                ret.Append(quebraLinha);
                            }
                            else
                            {
                                ret.Append("   ");
                            }
                        }


                        TemPreventivo = true;
                    }
                    else if (!this.DescExameComplPreventivo)
                    {
                        //ret.Append(i++.ToString()
                        //            + ". "
                        //            + complementar.Exame
                        //            + "  ___/___/______"
                        //            + " *"
                        //            + quebraLinha);
                        xExame = complementar.Exame.Trim();

                        if (xExame.Length > 0)
                        {
                            if (xExame.Length < xEspacos.Length)
                            {
                                xExame = xExame + xEspacos.Substring(1, xEspacos.Length - xExame.Length);
                            }

                            xMeses = -12;

                            IdExameDicionario.Find();

                            if (IdExameDicionario.Descricao.ToString() == "Periódico" || IdExameDicionario.Descricao.ToString() == "Mudança de Função")
                            {
                                if (complementar.Periodicidade.IndexOf("Periódico (2 anos)") >= 0)
                                {
                                    xMeses = -24;
                                }
                                else if (complementar.Periodicidade.IndexOf("Periódico (3 anos)") >= 0)
                                {
                                    xMeses = -36;
                                }
                                else if (complementar.Periodicidade.IndexOf("Periódico (4 anos)") >= 0)
                                {
                                    xMeses = -48;
                                }
                                else if (complementar.Periodicidade.IndexOf("Periódico (5 anos)") >= 0)
                                {
                                    xMeses = -60;
                                }
                                else if (complementar.Periodicidade.IndexOf("Anual") > 0)
                                {
                                    xMeses = -12;
                                }
                                else if (complementar.Periodicidade.IndexOf("6 meses") > 0 || complementar.Periodicidade.ToUpper().IndexOf("SEMESTRAL") > 0)
                                {
                                    xMeses = -6;
                                }

                            }


                            this.IdEmpregado.Find();

                            if (complementar.Periodicidade.ToUpper().IndexOf("6 MESES DA ADMISSÃO") > 0)
                            {
                                if (this.DataExame <= this.IdEmpregado.hDT_ADM.AddMonths(6))
                                {
                                    xMeses = -6;
                                }
                                else if (this.DataExame > this.IdEmpregado.hDT_ADM.AddMonths(6) && this.DataExame <= this.IdEmpregado.hDT_ADM.AddMonths(12))
                                {
                                    if (complementar.dataUltimo >= this.IdEmpregado.hDT_ADM.AddMonths(6))
                                        xMeses = -12;
                                    else
                                        xMeses = -6;
                                }
                                else if (this.DataExame > this.IdEmpregado.hDT_ADM.AddMonths(12) && this.DataExame <= this.IdEmpregado.hDT_ADM.AddMonths(18))
                                {
                                    if (complementar.dataUltimo < this.IdEmpregado.hDT_ADM.AddMonths(6))
                                        xMeses = -6;
                                    else
                                        xMeses = -12;
                                }
                            }


                            //audiometria - regra única - 120 dias
                            //this.IdExameDicionario.Find();
                            //if (this.IdExameDicionario.Id == 2 && xExame.ToUpper().Trim() == "AUDIOMETRIA")
                            //{
                            //    if (complementar.dataUltimo.AddDays(120) < this.DataExame)
                            //    {
                            //        string zData = "  /  /    ";
                            //        xExame = xExame.Substring(0, 38) + zData;
                            //    }
                            //    else
                            //    {
                            //        System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");
                            //        string zData = complementar.dataUltimo.ToString("dd/MM/yyyy");
                            //        xExame = xExame.Substring(0, 38) + zData;
                            //    }
                            //}
                            //else 
                            if (complementar.dataUltimo >= this.DataExame.AddMonths(xMeses))
                            {
                                string zData;

                                //if (Exibir_Datas == true) zData = complementar.dataUltimo.ToString("dd/MM/yyyy");
                                //else zData = "  /  /    ";

                                if (Exibir_Datas == true)
                                {

                                    if (xDataBranco != "" && xDataBranco != "  /  /    ")
                                    {
                                        System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");

                                        if (xDesconsiderarCompl == false)
                                        {
                                            if (complementar.dataUltimo < System.Convert.ToDateTime(xDataBranco, ptBr))
                                                zData = "  /  /    ";
                                            else
                                                zData = complementar.dataUltimo.ToString("dd/MM/yyyy");
                                        }
                                        else
                                        {
                                            //if (System.Convert.ToDateTime(xDataBranco, ptBr) <= complementar.dataUltimo)
                                            if (this.DataExame.Date >= System.Convert.ToDateTime(xDataBranco, ptBr))
                                                zData = "  /  /    ";
                                            else
                                                zData = complementar.dataUltimo.ToString("dd/MM/yyyy");
                                        }
                                    }
                                    else
                                    {
                                        zData = complementar.dataUltimo.ToString("dd/MM/yyyy");
                                    }
                                }
                                else
                                {
                                    zData = "  /  /    ";
                                }

                                xExame = xExame.Substring(0, 38) + zData;
                            }
                            else
                            {
                                if (xExame.Substring(39, 4) == "    ")
                                {
                                    xExame = xExame.Substring(0, 40) + "/" + xExame.Substring(41, 2) + "/    "; //+ xExame.Substring(43);
                                }
                                else
                                {
                                    xExame = xExame + "";
                                }
                            }

                        }


                        if (i < 10)
                        {
                            ret.Append(i++.ToString()
                                        + ". "
                                        + xExame);
                        }
                        else
                        {
                            ret.Append(i++.ToString()
                                        + "."
                                        + xExame);
                        }



                        if (i <= 10) ret.Append("   ");
                        else ret.Append("  ");

                        if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("GLOBAL") > 0)
                        {
                            if (i % 2 == 1 || complementares.Count <= 12)
                            {
                                ret.Append(quebraLinha);
                            }
                            else
                            {
                                ret.Append("   ");
                            }
                        }
                        else
                        {
                            if (i % 2 == 1 || complementares.Count <= 9)
                            {
                                ret.Append(quebraLinha);
                            }
                            else
                            {
                                ret.Append("   ");
                            }
                        }



                        TemPreventivo = true;
                    }
                }
                else
                {
                    //if (complementar.dataUltimo == new DateTime())
                    //    ret.Append(i++.ToString() + ". " + complementar.Exame + quebraLinha);
                    //else
                    //{
                    //ret.Append(i++.ToString()
                    //            + ". "
                    //            + complementar.Exame
                    //            + " - " + complementar.dataUltimo.ToString("dd-MM-yyyy")
                    //            + quebraLinha);


                    xExame = complementar.Exame.Trim();

                    if (xExame.Length > 0)
                    {
                        if (xExame.Length < xEspacos.Length)
                        {
                            xExame = xExame + xEspacos.Substring(1, xEspacos.Length - xExame.Length);
                        }

                        xMeses = -12;

                        IdExameDicionario.Find();

                        if (IdExameDicionario.Descricao.ToString() == "Periódico" || IdExameDicionario.Descricao.ToString() == "Mudança de Função")
                        {
                            if (complementar.Periodicidade.IndexOf("Periódico (2 anos)") >= 0)
                            {
                                xMeses = -24;
                            }
                            else if (complementar.Periodicidade.IndexOf("Periódico (3 anos)") >= 0)
                            {
                                xMeses = -36;
                            }
                            else if (complementar.Periodicidade.IndexOf("Periódico (4 anos)") >= 0)
                            {
                                xMeses = -48;
                            }
                            else if (complementar.Periodicidade.IndexOf("Periódico (5 anos)") >= 0)
                            {
                                xMeses = -60;
                            }
                            else if (complementar.Periodicidade.IndexOf("Anual") > 0)
                            {
                                xMeses = -12;
                            }
                            else if (complementar.Periodicidade.IndexOf("6 meses") > 0 || complementar.Periodicidade.ToUpper().IndexOf("SEMESTRAL") > 0)
                            {
                                xMeses = -6;
                            }


                        }

                        this.IdEmpregado.Find();

                        if (complementar.Periodicidade.ToUpper().IndexOf("6 MESES DA ADMISSÃO") > 0)
                        {
                            if (this.DataExame <= this.IdEmpregado.hDT_ADM.AddMonths(6))
                            {
                                xMeses = -6;
                            }
                            else if (this.DataExame > this.IdEmpregado.hDT_ADM.AddMonths(6) && this.DataExame <= this.IdEmpregado.hDT_ADM.AddMonths(12))
                            {
                                if (complementar.dataUltimo >= this.IdEmpregado.hDT_ADM.AddMonths(6))
                                    xMeses = -12;
                                else
                                    xMeses = -6;
                            }
                            else if (this.DataExame > this.IdEmpregado.hDT_ADM.AddMonths(12) && this.DataExame <= this.IdEmpregado.hDT_ADM.AddMonths(18))
                            {
                                if (complementar.dataUltimo < this.IdEmpregado.hDT_ADM.AddMonths(6))
                                    xMeses = -6;
                                else
                                    xMeses = -12;
                            }
                        }



                        //audiometria - regra única - 120 dias
                        //this.IdExameDicionario.Find();
                        //if (this.IdExameDicionario.Id == 2 && xExame.ToUpper().Trim() == "AUDIOMETRIA")
                        //{
                        //    if (complementar.dataUltimo.AddDays(120) < this.DataExame)
                        //    {
                        //        string zData = "  /  /    ";
                        //        xExame = xExame.Substring(0, 38) + zData;
                        //    }
                        //    else
                        //    {
                        //        System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");
                        //        string zData = complementar.dataUltimo.ToString("dd/MM/yyyy");
                        //        xExame = xExame.Substring(0, 38) + zData;
                        //    }
                        //}
                        //else 
                        if (complementar.dataUltimo >= this.DataExame.AddMonths(xMeses))
                        {
                            string zData;

                            //if (Exibir_Datas == true) zData = complementar.dataUltimo.ToString("dd/MM/yyyy");
                            //else zData = "  /  /    ";

                            if (Exibir_Datas == true)
                            {

                                if (xDataBranco != "" && xDataBranco != "  /  /    ")
                                {
                                    System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");

                                    if (xDesconsiderarCompl == false)
                                    {
                                        if (complementar.dataUltimo < System.Convert.ToDateTime(xDataBranco, ptBr))
                                            zData = "  /  /    ";
                                        else
                                            zData = complementar.dataUltimo.ToString("dd/MM/yyyy");
                                    }
                                    else
                                    {
                                        //if (System.Convert.ToDateTime(xDataBranco, ptBr) <= complementar.dataUltimo)
                                        if (this.DataExame.Date >= System.Convert.ToDateTime(xDataBranco, ptBr))
                                            zData = "  /  /    ";
                                        else
                                            zData = complementar.dataUltimo.ToString("dd/MM/yyyy");
                                    }
                                }
                                else
                                {
                                    zData = complementar.dataUltimo.ToString("dd/MM/yyyy");
                                }
                            }
                            else
                            {
                                zData = "  /  /    ";
                            }
                            xExame = xExame.Substring(0, 38) + zData;
                        }
                        else
                        {
                            if (xExame.Substring(39, 4) == "    ")
                            {
                                xExame = xExame.Substring(0, 40) + "/" + xExame.Substring(41, 2) + "/    "; //+ xExame.Substring(43);
                            }
                            else
                            {
                                xExame = xExame + "";
                            }

                        }
                    }


                    //ret.Append(i++.ToString()
                    //            + ". "
                    //            + xExame + "_"
                    //            + quebraLinha);


                    if (i < 10)
                    {
                        ret.Append(i++.ToString()
                                    + ". "
                                    + xExame);
                    }
                    else
                    {
                        ret.Append(i++.ToString()
                                    + "."
                                    + xExame);
                    }


                    if (i <= 10) ret.Append("   ");
                    else ret.Append("  ");

                    if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("GLOBAL") > 0)
                    {
                        if (i % 2 == 1 || complementares.Count <= 12)
                        {
                            ret.Append(quebraLinha);
                        }
                        else
                        {
                            ret.Append("   ");
                        }
                    }
                    else
                    {
                        if (i % 2 == 1 || complementares.Count <= 9)
                        {
                            ret.Append(quebraLinha);
                        }
                        else
                        {
                            ret.Append("   ");
                        }
                    }





                    //}
                }

            }

            //if (TemPreventivo)
            //    ret.Append("* (por iniciativa da empresa)");

            if (TemPreventivo)
            {
                //if (complementares.Count > 9)                {

                //if (i > 0 && i % 2 == 1) i = i + 2;

                //i = i / 2;
                //for (int zCont = i; zCont < 10; zCont++)
                ret.Append(System.Environment.NewLine);

                ////ret.Append(System.Environment.NewLine + "* (este exame é uma exigência do cliente, sem relação com o seu risco ocupacional. Portanto, a sua realização não é obrigatória e caso exista alguma alteração, o mesmo não deverá ser levado em conta na sua aptidão laboral.)");   //por iniciativa da empresa)");
                ////ret.Append(System.Environment.NewLine + "* (exame realizado por solicitação do cliente, sem relação com risco ocupacional deste trabalhador, sua realização não é obrigatória e caso tenha resultado anormal, não deverá ser considerado na aptidão ou inaptidão objeto deste exame.)");
                // }
            }


            return ret.ToString();
        }




        public string GetPlanejamentoExamesAso_Formatado_Aptidao(Ilitera.Opsa.Data.Empregado_Aptidao zAptidao, bool Exibir_Datas, string xDataBranco, string xExamesOcupacionais, bool xDesconsiderarCompl, Clinico clinico)
        {
            StringBuilder ret = new StringBuilder();

            string xExame;
            string xEspacos;

            int xMeses = 0;

            bool TemPreventivo = false;

            int i = 1;

            string quebraLinha;

            quebraLinha = "\n";

            xEspacos = "                                                ";
            xExame = "";

            //pegar numeração já existente de exames
            clinico.IdExameDicionario.Find();
            if (clinico.IdExameDicionario.Id == 3)  //mudança de função
            {
                for (int rCont = 1; rCont < 40; rCont++)
                {
                    if (xExamesOcupacionais.IndexOf(rCont.ToString().Trim() + ".") >= 0)
                    {
                        i = rCont + 1;
                    }
                }
            }
            else
            {
                complementares = ExamesComplementares(true);
                if (complementares.Count > 0)
                {
                    //i = i + complementares.Count;
                    for (int fCont = 0; fCont < complementares.Count; fCont++)
                    {
                        if (complementares[fCont].IdExameDicionario != 100) //não considerar toxicológico
                        {
                            i = i + 1;
                        }
                    }
                }
            }

            complementares = ExamesComplementares_Aptidao(zAptidao);

            foreach (ExameComplementar complementar in complementares)
            {
                //toxicológico - não sair no ASO
                if (complementar.IdExameDicionario == 100)
                {
                    continue;
                }


                if (xExamesOcupacionais.IndexOf(complementar.Exame) < 0)
                {
                    //Preventivo somente se tiver exames feitos
                    if (complementar.Preventivo)
                    {
                        if (complementar.dataUltimo != new DateTime())
                        {
                            //ret.Append(i++.ToString()
                            //            + ". "
                            //            + complementar.Exame
                            //            + " - "
                            //            + complementar.dataUltimo.ToString("dd-MM-yyyy")
                            //            + " *"
                            //            + quebraLinha);
                            xExame = complementar.Exame.Trim();

                            if (xExame.Length > 0)
                            {
                                if (xExame.Length < xEspacos.Length)
                                {
                                    xExame = xExame + xEspacos.Substring(1, xEspacos.Length - xExame.Length);
                                }



                                xMeses = -12;

                                if (IdExameDicionario.Descricao.ToString() == "Periódico")
                                {
                                    if (complementar.Periodicidade.IndexOf("Periódico (2 anos)") >= 0)
                                    {
                                        xMeses = -24;
                                    }
                                    else if (complementar.Periodicidade.IndexOf("Periódico (3 anos)") >= 0)
                                    {
                                        xMeses = -36;
                                    }
                                    else if (complementar.Periodicidade.IndexOf("Periódico (4 anos)") >= 0)
                                    {
                                        xMeses = -48;
                                    }
                                    else if (complementar.Periodicidade.IndexOf("Periódico (5 anos)") >= 0)
                                    {
                                        xMeses = -60;
                                    }
                                    else if (complementar.Periodicidade.IndexOf("Anual") > 0)
                                    {
                                        xMeses = -12;
                                    }
                                    else if (complementar.Periodicidade.IndexOf("6 meses") > 0 || complementar.Periodicidade.ToUpper().IndexOf("SEMESTRAL") > 0)
                                    {
                                        xMeses = -6;
                                    }
                                }

                                this.IdEmpregado.Find();

                                if (complementar.Periodicidade.ToUpper().IndexOf("6 MESES DA ADMISSÃO") > 0)
                                {
                                    if (this.DataExame <= this.IdEmpregado.hDT_ADM.AddMonths(6))
                                    {
                                        xMeses = -6;
                                    }
                                    else if (this.DataExame > this.IdEmpregado.hDT_ADM.AddMonths(6) && this.DataExame <= this.IdEmpregado.hDT_ADM.AddMonths(12))
                                    {
                                        if (complementar.dataUltimo >= this.IdEmpregado.hDT_ADM.AddMonths(6))
                                            xMeses = -12;
                                        else
                                            xMeses = -6;
                                    }
                                    else if (this.DataExame > this.IdEmpregado.hDT_ADM.AddMonths(12) && this.DataExame <= this.IdEmpregado.hDT_ADM.AddMonths(18))
                                    {
                                        if (complementar.dataUltimo < this.IdEmpregado.hDT_ADM.AddMonths(6))
                                            xMeses = -6;
                                        else
                                            xMeses = -12;
                                    }
                                }


                                //audiometria - regra única - 120 dias
                                //this.IdExameDicionario.Find();
                                //if (this.IdExameDicionario.Id == 2 && xExame.ToUpper().Trim() == "AUDIOMETRIA")
                                //{
                                //    if (complementar.dataUltimo.AddDays(120) < this.DataExame)
                                //    {
                                //        string zData = "  /  /    ";
                                //        xExame = xExame.Substring(0, 38) + zData;
                                //    }
                                //    else
                                //    {
                                //        System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");
                                //        string zData = complementar.dataUltimo.ToString("dd/MM/yyyy");
                                //        xExame = xExame.Substring(0, 38) + zData;
                                //    }
                                //}
                                //else 
                                if (complementar.dataUltimo >= this.DataExame.AddMonths(xMeses))
                                {
                                    string zData;

                                    if (Exibir_Datas == true)
                                    {

                                        if (xDataBranco != "" && xDataBranco != "  /  /    ")
                                        {
                                            System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");

                                            if (xDesconsiderarCompl == false)
                                            {
                                                if (complementar.dataUltimo < System.Convert.ToDateTime(xDataBranco, ptBr))
                                                    zData = "  /  /    ";
                                                else
                                                    zData = complementar.dataUltimo.ToString("dd/MM/yyyy");
                                            }
                                            else
                                            {
                                                //if (System.Convert.ToDateTime(xDataBranco, ptBr) <= complementar.dataUltimo)
                                                if (this.DataExame.Date >= System.Convert.ToDateTime(xDataBranco, ptBr))
                                                    zData = "  /  /    ";
                                                else
                                                    zData = complementar.dataUltimo.ToString("dd/MM/yyyy");
                                            }
                                        }
                                        else
                                        {
                                            zData = complementar.dataUltimo.ToString("dd/MM/yyyy");
                                        }
                                    }
                                    else
                                    {
                                        zData = "  /  /    ";
                                    }

                                    xExame = xExame.Substring(0, 38) + zData;
                                }
                                else
                                {
                                    if (xExame.Substring(39, 4) == "    ")
                                    {
                                        xExame = xExame.Substring(0, 40) + "/" + xExame.Substring(41, 2) + "/    "; //"/" + xExame.Substring(43);
                                    }
                                    else
                                    {
                                        xExame = xExame + "";
                                    }

                                }
                            }

                            //ret.Append(i++.ToString()
                            //            + ". "
                            //            + xExame + "_"
                            //            + " *"
                            //            + quebraLinha);

                            if (i < 10)
                            {
                                ret.Append(i++.ToString()
                                            + ". "
                                            + xExame);
                            }
                            else
                            {
                                ret.Append(i++.ToString()
                                            + "."
                                            + xExame);
                            }



                            if (i <= 10) ret.Append("   ");
                            else ret.Append("  ");

                            if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("GLOBAL") > 0)
                            {
                                if (i % 2 == 1 || complementares.Count <= 12)
                                {
                                    ret.Append(quebraLinha);
                                }
                                else
                                {
                                    ret.Append("   ");
                                }
                            }
                            else
                            {
                                if (i % 2 == 1 || complementares.Count <= 9)
                                {
                                    ret.Append(quebraLinha);
                                }
                                else
                                {
                                    ret.Append("   ");
                                }
                            }


                            TemPreventivo = true;
                        }
                        else if (!this.DescExameComplPreventivo)
                        {
                            //ret.Append(i++.ToString()
                            //            + ". "
                            //            + complementar.Exame
                            //            + "  ___/___/______"
                            //            + " *"
                            //            + quebraLinha);
                            xExame = complementar.Exame.Trim();

                            if (xExame.Length > 0)
                            {
                                if (xExame.Length < xEspacos.Length)
                                {
                                    xExame = xExame + xEspacos.Substring(1, xEspacos.Length - xExame.Length);
                                }


                                xMeses = -12;

                                if (IdExameDicionario.Descricao.ToString() == "Periódico")
                                {
                                    if (complementar.Periodicidade.IndexOf("Periódico (2 anos)") >= 0)
                                    {
                                        xMeses = -24;
                                    }
                                    else if (complementar.Periodicidade.IndexOf("Periódico (3 anos)") >= 0)
                                    {
                                        xMeses = -36;
                                    }
                                    else if (complementar.Periodicidade.IndexOf("Periódico (4 anos)") >= 0)
                                    {
                                        xMeses = -48;
                                    }
                                    else if (complementar.Periodicidade.IndexOf("Periódico (5 anos)") >= 0)
                                    {
                                        xMeses = -60;
                                    }
                                    else if (complementar.Periodicidade.IndexOf("Anual") > 0)
                                    {
                                        xMeses = -12;
                                    }
                                    else if (complementar.Periodicidade.IndexOf("6 meses") > 0 || complementar.Periodicidade.ToUpper().IndexOf("SEMESTRAL") > 0)
                                    {
                                        xMeses = -6;
                                    }
                                }

                                this.IdEmpregado.Find();

                                if (complementar.Periodicidade.ToUpper().IndexOf("6 MESES DA ADMISSÃO") > 0)
                                {
                                    if (this.DataExame <= this.IdEmpregado.hDT_ADM.AddMonths(6))
                                    {
                                        xMeses = -6;
                                    }
                                    else if (this.DataExame > this.IdEmpregado.hDT_ADM.AddMonths(6) && this.DataExame <= this.IdEmpregado.hDT_ADM.AddMonths(12))
                                    {
                                        if (complementar.dataUltimo >= this.IdEmpregado.hDT_ADM.AddMonths(6))
                                            xMeses = -12;
                                        else
                                            xMeses = -6;
                                    }
                                    else if (this.DataExame > this.IdEmpregado.hDT_ADM.AddMonths(12) && this.DataExame <= this.IdEmpregado.hDT_ADM.AddMonths(18))
                                    {
                                        if (complementar.dataUltimo < this.IdEmpregado.hDT_ADM.AddMonths(6))
                                            xMeses = -6;
                                        else
                                            xMeses = -12;
                                    }
                                }



                                ////audiometria - regra única - 120 dias
                                //this.IdExameDicionario.Find();
                                //if (this.IdExameDicionario.Id == 2 && xExame.ToUpper().Trim() == "AUDIOMETRIA")
                                //{
                                //    if (complementar.dataUltimo.AddDays(120) < this.DataExame)
                                //    {
                                //        string zData = "  /  /    ";
                                //        xExame = xExame.Substring(0, 38) + zData;
                                //    }
                                //    else
                                //    {
                                //        System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");
                                //        string zData = complementar.dataUltimo.ToString("dd/MM/yyyy");
                                //        xExame = xExame.Substring(0, 38) + zData;
                                //    }
                                //}
                                //else 
                                if (complementar.dataUltimo >= this.DataExame.AddMonths(xMeses))
                                {
                                    string zData;

                                    if (Exibir_Datas == true)
                                    {

                                        if (xDataBranco != "" && xDataBranco != "  /  /    ")
                                        {
                                            System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");

                                            if (xDesconsiderarCompl == false)
                                            {
                                                if (complementar.dataUltimo < System.Convert.ToDateTime(xDataBranco, ptBr))
                                                    zData = "  /  /    ";
                                                else
                                                    zData = complementar.dataUltimo.ToString("dd/MM/yyyy");
                                            }
                                            else
                                            {
                                                //if (System.Convert.ToDateTime(xDataBranco, ptBr) <= complementar.dataUltimo)
                                                if (this.DataExame.Date >= System.Convert.ToDateTime(xDataBranco, ptBr))
                                                    zData = "  /  /    ";
                                                else
                                                    zData = complementar.dataUltimo.ToString("dd/MM/yyyy");
                                            }
                                        }
                                        else
                                        {
                                            zData = complementar.dataUltimo.ToString("dd/MM/yyyy");
                                        }
                                    }
                                    else
                                    {
                                        zData = "  /  /    ";
                                    }

                                    xExame = xExame.Substring(0, 38) + zData;
                                }
                                else
                                {
                                    if (xExame.Substring(39, 4) == "    ")
                                    {
                                        xExame = xExame.Substring(0, 40) + "/" + xExame.Substring(41, 2) + "/    "; //"/" + xExame.Substring(43);
                                    }
                                    else
                                    {
                                        xExame = xExame + "";
                                    }

                                }
                            }


                            if (i < 10)
                            {
                                ret.Append(i++.ToString()
                                            + ". "
                                            + xExame);
                            }
                            else
                            {
                                ret.Append(i++.ToString()
                                            + "."
                                            + xExame);
                            }



                            if (i <= 10) ret.Append("   ");
                            else ret.Append("  ");

                            if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("GLOBAL") > 0)
                            {
                                if (i % 2 == 1 || complementares.Count <= 12)
                                {
                                    ret.Append(quebraLinha);
                                }
                                else
                                {
                                    ret.Append("   ");
                                }
                            }
                            else
                            {
                                if (i % 2 == 1 || complementares.Count <= 9)
                                {
                                    ret.Append(quebraLinha);
                                }
                                else
                                {
                                    ret.Append("   ");
                                }
                            }



                            TemPreventivo = true;
                        }
                    }
                    else
                    {
                        //if (complementar.dataUltimo == new DateTime())
                        //    ret.Append(i++.ToString() + ". " + complementar.Exame + quebraLinha);
                        //else
                        //{
                        //ret.Append(i++.ToString()
                        //            + ". "
                        //            + complementar.Exame
                        //            + " - " + complementar.dataUltimo.ToString("dd-MM-yyyy")
                        //            + quebraLinha);


                        xExame = complementar.Exame.Trim();

                        if (xExame.Length > 0)
                        {
                            if (xExame.Length < xEspacos.Length)
                            {
                                xExame = xExame + xEspacos.Substring(1, xEspacos.Length - xExame.Length);
                            }

                            xMeses = -12;

                            if (IdExameDicionario.Descricao.ToString() == "Periódico")
                            {
                                if (complementar.Periodicidade.IndexOf("Periódico (2 anos)") >= 0)
                                {
                                    xMeses = -24;
                                }
                                else if (complementar.Periodicidade.IndexOf("Periódico (3 anos)") >= 0)
                                {
                                    xMeses = -36;
                                }
                                else if (complementar.Periodicidade.IndexOf("Periódico (4 anos)") >= 0)
                                {
                                    xMeses = -48;
                                }
                                else if (complementar.Periodicidade.IndexOf("Periódico (5 anos)") >= 0)
                                {
                                    xMeses = -60;
                                }
                                else if (complementar.Periodicidade.IndexOf("Anual") > 0)
                                {
                                    xMeses = -12;
                                }
                                else if (complementar.Periodicidade.IndexOf("6 meses") > 0 || complementar.Periodicidade.ToUpper().IndexOf("SEMESTRAL") > 0)
                                {
                                    xMeses = -6;
                                }
                            }


                            this.IdEmpregado.Find();

                            if (complementar.Periodicidade.ToUpper().IndexOf("6 MESES DA ADMISSÃO") > 0)
                            {
                                if (this.DataExame <= this.IdEmpregado.hDT_ADM.AddMonths(6))
                                {
                                    xMeses = -6;
                                }
                                else if (this.DataExame > this.IdEmpregado.hDT_ADM.AddMonths(6) && this.DataExame <= this.IdEmpregado.hDT_ADM.AddMonths(12))
                                {
                                    if (complementar.dataUltimo >= this.IdEmpregado.hDT_ADM.AddMonths(6))
                                        xMeses = -12;
                                    else
                                        xMeses = -6;
                                }
                                else if (this.DataExame > this.IdEmpregado.hDT_ADM.AddMonths(12) && this.DataExame <= this.IdEmpregado.hDT_ADM.AddMonths(18))
                                {
                                    if (complementar.dataUltimo < this.IdEmpregado.hDT_ADM.AddMonths(6))
                                        xMeses = -6;
                                    else
                                        xMeses = -12;
                                }
                            }



                            ////audiometria - regra única - 120 dias
                            //this.IdExameDicionario.Find();
                            //if (this.IdExameDicionario.Id == 2 && xExame.ToUpper().Trim() == "AUDIOMETRIA")
                            //{
                            //    if (complementar.dataUltimo.AddDays(120) < this.DataExame)
                            //    {
                            //        string zData = "  /  /    ";
                            //        xExame = xExame.Substring(0, 38) + zData;
                            //    }
                            //    else
                            //    {
                            //        System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");
                            //        string zData = complementar.dataUltimo.ToString("dd/MM/yyyy");
                            //        xExame = xExame.Substring(0, 38) + zData;
                            //    }
                            //}
                            //else 
                            if (complementar.dataUltimo >= this.DataExame.AddMonths(xMeses))
                            {
                                string zData;

                                //                                if (Exibir_Datas == true) zData = complementar.dataUltimo.ToString("dd/MM/yyyy");
                                //                                else zData = "  /  /    ";

                                if (Exibir_Datas == true)
                                {

                                    if (xDataBranco != "" && xDataBranco != "  /  /    ")
                                    {
                                        System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");

                                        if (xDesconsiderarCompl == false)
                                        {
                                            if (complementar.dataUltimo < System.Convert.ToDateTime(xDataBranco, ptBr))
                                                zData = "  /  /    ";
                                            else
                                                zData = complementar.dataUltimo.ToString("dd/MM/yyyy");
                                        }
                                        else
                                        {
                                            //if (System.Convert.ToDateTime(xDataBranco, ptBr) <= complementar.dataUltimo)
                                            if (this.DataExame.Date >= System.Convert.ToDateTime(xDataBranco, ptBr))
                                                zData = "  /  /    ";
                                            else
                                                zData = complementar.dataUltimo.ToString("dd/MM/yyyy");
                                        }
                                    }
                                    else
                                    {
                                        zData = complementar.dataUltimo.ToString("dd/MM/yyyy");
                                    }
                                }
                                else
                                {
                                    zData = "  /  /    ";
                                }

                                xExame = xExame.Substring(0, 38) + zData;
                            }
                            else
                            {
                                if (xExame.Substring(39, 4) == "    ")
                                {
                                    xExame = xExame.Substring(0, 40) + "/" + xExame.Substring(41, 2) + "/    "; //"/" + xExame.Substring(43);
                                }
                                else
                                {
                                    xExame = xExame + "";
                                }

                            }
                        }


                        //ret.Append(i++.ToString()
                        //            + ". "
                        //            + xExame + "_"
                        //            + quebraLinha);


                        if (i < 10)
                        {
                            ret.Append(i++.ToString()
                                        + ". "
                                        + xExame);
                        }
                        else
                        {
                            ret.Append(i++.ToString()
                                        + "."
                                        + xExame);
                        }



                        if (i <= 10) ret.Append("   ");
                        else ret.Append("  ");

                        if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("GLOBAL") > 0)
                        {
                            if (i % 2 == 1 || complementares.Count <= 12)
                            {
                                ret.Append(quebraLinha);
                            }
                            else
                            {
                                ret.Append("   ");
                            }
                        }
                        else
                        {
                            if (i % 2 == 1 || complementares.Count <= 9)
                            {
                                ret.Append(quebraLinha);
                            }
                            else
                            {
                                ret.Append("   ");
                            }
                        }





                        //}
                    }

                }

            }

            if (TemPreventivo)
            {
                if (complementares.Count > 9)
                {

                    if (i > 0 && i % 2 == 1) i = i + 2;

                    i = i / 2;
                    for (int zCont = i; zCont < 10; zCont++)
                        ret.Append(System.Environment.NewLine);

                    //ret.Append(System.Environment.NewLine + "* (este exame é uma exigência do cliente, sem relação com o seu risco ocupacional. Portanto, a sua realização não é obrigatória e caso exista alguma alteração, o mesmo não deverá ser levado em conta na sua aptidão laboral.)");   //por iniciativa da empresa)");
                    //ret.Append(System.Environment.NewLine + "* (exame realizado por solicitação do cliente, sem relação com risco ocupacional deste trabalhador, sua realização não é obrigatória e caso tenha resultado anormal, não deverá ser considerado na aptidão ou inaptidão objeto deste exame.)");
                }
            }
            //if (TemPreventivo)
            //{
            //    if (i > 0 && i % 2 == 1) i = i + 2;

            //    i = i / 2;
            //    for (int zCont = i; zCont < 10; zCont++)
            //        ret.Append(System.Environment.NewLine);

            //    //ret.Append(System.Environment.NewLine + "* (este exame é uma exigência do cliente, sem relação com o seu risco ocupacional. Portanto, a sua realização não é obrigatória e caso exista alguma alteração, o mesmo não deverá ser levado em conta na sua aptidão laboral.)");   //por iniciativa da empresa)");
            //    //ret.Append(System.Environment.NewLine + "* (exame realizado por solicitação do cliente, sem relação com risco ocupacional deste trabalhador, sua realização não é obrigatória e caso tenha resultado anormal, não deverá ser considerado na aptidão ou inaptidão objeto deste exame.)");
            //}


            return ret.ToString();
        }






        public string GetPlanejamentoExamesAso_Formatado_Aptidao_Desconsiderar(Ilitera.Opsa.Data.Empregado_Aptidao zAptidao, bool Exibir_Datas, string xExamesOcupacionais, Clinico clinico, int zDias_Desconsiderar)
        {
            StringBuilder ret = new StringBuilder();

            string xExame;
            string xEspacos;

            int xMeses = 0;

            bool TemPreventivo = false;

            int i = 1;

            string quebraLinha;

            string xDataBranco = "";
            bool xDesconsiderarCompl = false;


            quebraLinha = "\n";

            xEspacos = "                                                ";
            xExame = "";

            //pegar numeração já existente de exames
            clinico.IdExameDicionario.Find();
            if (clinico.IdExameDicionario.Id == 3)  //mudança de função
            {
                for (int rCont = 1; rCont < 40; rCont++)
                {
                    if (xExamesOcupacionais.IndexOf(rCont.ToString().Trim() + ".") >= 0)
                    {
                        i = rCont + 1;
                    }
                }
            }
            else
            {
                complementares = ExamesComplementares(true);

                if (complementares.Count > 0)
                {
                    //i = i + complementares.Count;
                    for (int fCont = 0; fCont < complementares.Count; fCont++)
                    {
                        if (complementares[fCont].IdExameDicionario != 100) //não considerar toxicológico
                        {
                            i = i + 1;
                        }
                    }
                }
            }

            complementares = ExamesComplementares_Aptidao(zAptidao);

            foreach (ExameComplementar complementar in complementares)
            {


                System.Globalization.CultureInfo ptBr2 = new System.Globalization.CultureInfo("pt-Br");

                ExamePlanejamento xPlanej = new ExamePlanejamento();
                clinico.IdEmpregado.Find();
                int zIdEmpregado = clinico.IdEmpregado.Id;
                //clinico.IdExameDicionario.Find();
                //int zIdExameDic = clinico.IdExameDicionario.Id;

                int zIdExameDic = complementar.IdExameDicionario;
                clinico.IdPcmso.Find();

                xPlanej.Find(" IdEmpregado =" + zIdEmpregado.ToString() + " and IdExameDicionario = " + zIdExameDic.ToString() + " and IdPCMSO = " + clinico.IdPcmso.Id.ToString());

                if (xPlanej.Id == 0)
                {
                    xDataBranco = clinico.DataExame.AddDays(zDias_Desconsiderar * (-1)).ToString("dd/MM/yyyy", ptBr2);
                }
                else
                {
                    if (xPlanej.DataVencimento == new DateTime())
                        xDataBranco = this.DataExame.ToString("dd/MM/yyyy", ptBr2);  //"  /  /    ";
                    else
                        xDataBranco = xPlanej.DataVencimento.AddDays(zDias_Desconsiderar * (-1)).ToString("dd/MM/yyyy", ptBr2);
                }

                xDesconsiderarCompl = true;

                if (xExamesOcupacionais.IndexOf(complementar.Exame) < 0)
                {
                    //Preventivo somente se tiver exames feitos
                    if (complementar.Preventivo)
                    {
                        if (complementar.dataUltimo != new DateTime())
                        {
                            //ret.Append(i++.ToString()
                            //            + ". "
                            //            + complementar.Exame
                            //            + " - "
                            //            + complementar.dataUltimo.ToString("dd-MM-yyyy")
                            //            + " *"
                            //            + quebraLinha);
                            xExame = complementar.Exame.Trim();

                            if (xExame.Length > 0)
                            {
                                if (xExame.Length < xEspacos.Length)
                                {
                                    xExame = xExame + xEspacos.Substring(1, xEspacos.Length - xExame.Length);
                                }



                                xMeses = -12;

                                if (IdExameDicionario.Descricao.ToString() == "Periódico")
                                {
                                    if (complementar.Periodicidade.IndexOf("Periódico (2 anos)") >= 0)
                                    {
                                        xMeses = -24;
                                    }
                                    else if (complementar.Periodicidade.IndexOf("Periódico (3 anos)") >= 0)
                                    {
                                        xMeses = -36;
                                    }
                                    else if (complementar.Periodicidade.IndexOf("Periódico (4 anos)") >= 0)
                                    {
                                        xMeses = -48;
                                    }
                                    else if (complementar.Periodicidade.IndexOf("Periódico (5 anos)") >= 0)
                                    {
                                        xMeses = -60;
                                    }
                                    else if (complementar.Periodicidade.IndexOf("Anual") > 0)
                                    {
                                        xMeses = -12;
                                    }
                                    else if (complementar.Periodicidade.IndexOf("6 meses") > 0 || complementar.Periodicidade.ToUpper().IndexOf("SEMESTRAL") > 0)
                                    {
                                        xMeses = -6;
                                    }
                                }

                                this.IdEmpregado.Find();

                                if (complementar.Periodicidade.ToUpper().IndexOf("6 MESES DA ADMISSÃO") > 0)
                                {
                                    if (this.DataExame <= this.IdEmpregado.hDT_ADM.AddMonths(6))
                                    {
                                        xMeses = -6;
                                    }
                                    else if (this.DataExame > this.IdEmpregado.hDT_ADM.AddMonths(6) && this.DataExame <= this.IdEmpregado.hDT_ADM.AddMonths(12))
                                    {
                                        if (complementar.dataUltimo >= this.IdEmpregado.hDT_ADM.AddMonths(6))
                                            xMeses = -12;
                                        else
                                            xMeses = -6;
                                    }
                                    else if (this.DataExame > this.IdEmpregado.hDT_ADM.AddMonths(12) && this.DataExame <= this.IdEmpregado.hDT_ADM.AddMonths(18))
                                    {
                                        if (complementar.dataUltimo < this.IdEmpregado.hDT_ADM.AddMonths(6))
                                            xMeses = -6;
                                        else
                                            xMeses = -12;
                                    }
                                }


                                //audiometria - regra única - 120 dias
                                //this.IdExameDicionario.Find();
                                //if (this.IdExameDicionario.Id == 2 && xExame.ToUpper().Trim() == "AUDIOMETRIA")
                                //{
                                //    if (complementar.dataUltimo.AddDays(120) < this.DataExame)
                                //    {
                                //        string zData = "  /  /    ";
                                //        xExame = xExame.Substring(0, 38) + zData;
                                //    }
                                //    else
                                //    {
                                //        System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");
                                //        string zData = complementar.dataUltimo.ToString("dd/MM/yyyy");
                                //        xExame = xExame.Substring(0, 38) + zData;
                                //    }
                                //}
                                //else 
                                if (complementar.dataUltimo >= this.DataExame.AddMonths(xMeses))
                                {
                                    string zData;

                                    if (Exibir_Datas == true)
                                    {

                                        if (xDataBranco != "" && xDataBranco != "  /  /    ")
                                        {
                                            System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");

                                            if (xDesconsiderarCompl == false)
                                            {
                                                if (complementar.dataUltimo < System.Convert.ToDateTime(xDataBranco, ptBr))
                                                    zData = "  /  /    ";
                                                else
                                                    zData = complementar.dataUltimo.ToString("dd/MM/yyyy");
                                            }
                                            else
                                            {
                                                //if (System.Convert.ToDateTime(xDataBranco, ptBr) <= complementar.dataUltimo)
                                                if (this.DataExame.Date >= System.Convert.ToDateTime(xDataBranco, ptBr))
                                                    zData = "  /  /    ";
                                                else
                                                    zData = complementar.dataUltimo.ToString("dd/MM/yyyy");
                                            }
                                        }
                                        else
                                        {
                                            zData = complementar.dataUltimo.ToString("dd/MM/yyyy");
                                        }
                                    }
                                    else
                                    {
                                        zData = "  /  /    ";
                                    }

                                    xExame = xExame.Substring(0, 38) + zData;
                                }
                                else
                                {
                                    if (xExame.Substring(39, 4) == "    ")
                                    {
                                        xExame = xExame.Substring(0, 40) + "/" + xExame.Substring(41, 2) + "/    "; //"/" + xExame.Substring(43);
                                    }
                                    else
                                    {
                                        xExame = xExame + "";
                                    }

                                }
                            }

                            //ret.Append(i++.ToString()
                            //            + ". "
                            //            + xExame + "_"
                            //            + " *"
                            //            + quebraLinha);

                            if (i < 10)
                            {
                                ret.Append(i++.ToString()
                                            + ". "
                                            + xExame);
                            }
                            else
                            {
                                ret.Append(i++.ToString()
                                            + "."
                                            + xExame);
                            }


                            if (i <= 10) ret.Append("   ");
                            else ret.Append("  ");

                            if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("GLOBAL") > 0)
                            {
                                if (i % 2 == 1 || complementares.Count <= 12)
                                {
                                    ret.Append(quebraLinha);
                                }
                                else
                                {
                                    ret.Append("   ");
                                }
                            }
                            else
                            {
                                if (i % 2 == 1 || complementares.Count <= 9)
                                {
                                    ret.Append(quebraLinha);
                                }
                                else
                                {
                                    ret.Append("   ");
                                }
                            }


                            TemPreventivo = true;
                        }
                        else if (!this.DescExameComplPreventivo)
                        {
                            //ret.Append(i++.ToString()
                            //            + ". "
                            //            + complementar.Exame
                            //            + "  ___/___/______"
                            //            + " *"
                            //            + quebraLinha);
                            xExame = complementar.Exame.Trim();

                            if (xExame.Length > 0)
                            {
                                if (xExame.Length < xEspacos.Length)
                                {
                                    xExame = xExame + xEspacos.Substring(1, xEspacos.Length - xExame.Length);
                                }


                                xMeses = -12;

                                if (IdExameDicionario.Descricao.ToString() == "Periódico")
                                {
                                    if (complementar.Periodicidade.IndexOf("Periódico (2 anos)") >= 0)
                                    {
                                        xMeses = -24;
                                    }
                                    else if (complementar.Periodicidade.IndexOf("Periódico (3 anos)") >= 0)
                                    {
                                        xMeses = -36;
                                    }
                                    else if (complementar.Periodicidade.IndexOf("Periódico (4 anos)") >= 0)
                                    {
                                        xMeses = -48;
                                    }
                                    else if (complementar.Periodicidade.IndexOf("Periódico (5 anos)") >= 0)
                                    {
                                        xMeses = -60;
                                    }
                                    else if (complementar.Periodicidade.IndexOf("Anual") > 0)
                                    {
                                        xMeses = -12;
                                    }
                                    else if (complementar.Periodicidade.IndexOf("6 meses") > 0 || complementar.Periodicidade.ToUpper().IndexOf("SEMESTRAL") > 0)
                                    {
                                        xMeses = -6;
                                    }
                                }


                                this.IdEmpregado.Find();

                                if (complementar.Periodicidade.ToUpper().IndexOf("6 MESES DA ADMISSÃO") > 0)
                                {
                                    if (this.DataExame <= this.IdEmpregado.hDT_ADM.AddMonths(6))
                                    {
                                        xMeses = -6;
                                    }
                                    else if (this.DataExame > this.IdEmpregado.hDT_ADM.AddMonths(6) && this.DataExame <= this.IdEmpregado.hDT_ADM.AddMonths(12))
                                    {
                                        if (complementar.dataUltimo >= this.IdEmpregado.hDT_ADM.AddMonths(6))
                                            xMeses = -12;
                                        else
                                            xMeses = -6;
                                    }
                                    else if (this.DataExame > this.IdEmpregado.hDT_ADM.AddMonths(12) && this.DataExame <= this.IdEmpregado.hDT_ADM.AddMonths(18))
                                    {
                                        if (complementar.dataUltimo < this.IdEmpregado.hDT_ADM.AddMonths(6))
                                            xMeses = -6;
                                        else
                                            xMeses = -12;
                                    }
                                }


                                ////audiometria - regra única - 120 dias
                                //this.IdExameDicionario.Find();
                                //if (this.IdExameDicionario.Id == 2 && xExame.ToUpper().Trim() == "AUDIOMETRIA")
                                //{
                                //    if (complementar.dataUltimo.AddDays(120) < this.DataExame)
                                //    {
                                //        string zData = "  /  /    ";
                                //        xExame = xExame.Substring(0, 38) + zData;
                                //    }
                                //    else
                                //    {
                                //        System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");
                                //        string zData = complementar.dataUltimo.ToString("dd/MM/yyyy");
                                //        xExame = xExame.Substring(0, 38) + zData;
                                //    }
                                //}
                                //else 
                                if (complementar.dataUltimo >= this.DataExame.AddMonths(xMeses))
                                {
                                    string zData;

                                    if (Exibir_Datas == true)
                                    {

                                        if (xDataBranco != "" && xDataBranco != "  /  /    ")
                                        {
                                            System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");

                                            if (xDesconsiderarCompl == false)
                                            {
                                                if (complementar.dataUltimo < System.Convert.ToDateTime(xDataBranco, ptBr))
                                                    zData = "  /  /    ";
                                                else
                                                    zData = complementar.dataUltimo.ToString("dd/MM/yyyy");
                                            }
                                            else
                                            {
                                                //if (System.Convert.ToDateTime(xDataBranco, ptBr) <= complementar.dataUltimo)
                                                if (this.DataExame.Date >= System.Convert.ToDateTime(xDataBranco, ptBr))
                                                    zData = "  /  /    ";
                                                else
                                                    zData = complementar.dataUltimo.ToString("dd/MM/yyyy");
                                            }
                                        }
                                        else
                                        {
                                            zData = complementar.dataUltimo.ToString("dd/MM/yyyy");
                                        }
                                    }
                                    else
                                    {
                                        zData = "  /  /    ";
                                    }

                                    xExame = xExame.Substring(0, 38) + zData;
                                }
                                else
                                {
                                    if (xExame.Substring(39, 4) == "    ")
                                    {
                                        xExame = xExame.Substring(0, 40) + "/" + xExame.Substring(41, 2) + "/    "; //"/" + xExame.Substring(43);
                                    }
                                    else
                                    {
                                        xExame = xExame + "";
                                    }

                                }
                            }


                            if (i < 10)
                            {
                                ret.Append(i++.ToString()
                                            + ". "
                                            + xExame);
                            }
                            else
                            {
                                ret.Append(i++.ToString()
                                            + "."
                                            + xExame);
                            }


                            if (i <= 10) ret.Append("   ");
                            else ret.Append("  ");

                            if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("GLOBAL") > 0)
                            {
                                if (i % 2 == 1 || complementares.Count <= 12)
                                {
                                    ret.Append(quebraLinha);
                                }
                                else
                                {
                                    ret.Append("   ");
                                }
                            }
                            else
                            {
                                if (i % 2 == 1 || complementares.Count <= 9)
                                {
                                    ret.Append(quebraLinha);
                                }
                                else
                                {
                                    ret.Append("   ");
                                }
                            }



                            TemPreventivo = true;
                        }
                    }
                    else
                    {
                        //if (complementar.dataUltimo == new DateTime())
                        //    ret.Append(i++.ToString() + ". " + complementar.Exame + quebraLinha);
                        //else
                        //{
                        //ret.Append(i++.ToString()
                        //            + ". "
                        //            + complementar.Exame
                        //            + " - " + complementar.dataUltimo.ToString("dd-MM-yyyy")
                        //            + quebraLinha);


                        xExame = complementar.Exame.Trim();

                        if (xExame.Length > 0)
                        {
                            if (xExame.Length < xEspacos.Length)
                            {
                                xExame = xExame + xEspacos.Substring(1, xEspacos.Length - xExame.Length);
                            }

                            xMeses = -12;

                            if (IdExameDicionario.Descricao.ToString() == "Periódico")
                            {
                                if (complementar.Periodicidade.IndexOf("Periódico (2 anos)") >= 0)
                                {
                                    xMeses = -24;
                                }
                                else if (complementar.Periodicidade.IndexOf("Periódico (3 anos)") >= 0)
                                {
                                    xMeses = -36;
                                }
                                else if (complementar.Periodicidade.IndexOf("Periódico (4 anos)") >= 0)
                                {
                                    xMeses = -48;
                                }
                                else if (complementar.Periodicidade.IndexOf("Periódico (5 anos)") >= 0)
                                {
                                    xMeses = -60;
                                }
                                else if (complementar.Periodicidade.IndexOf("Anual") > 0)
                                {
                                    xMeses = -12;
                                }
                                else if (complementar.Periodicidade.IndexOf("6 meses") > 0 || complementar.Periodicidade.ToUpper().IndexOf("SEMESTRAL") > 0)
                                {
                                    xMeses = -6;
                                }
                            }


                            this.IdEmpregado.Find();

                            if (complementar.Periodicidade.ToUpper().IndexOf("6 MESES DA ADMISSÃO") > 0)
                            {
                                if (this.DataExame <= this.IdEmpregado.hDT_ADM.AddMonths(6))
                                {
                                    xMeses = -6;
                                }
                                else if (this.DataExame > this.IdEmpregado.hDT_ADM.AddMonths(6) && this.DataExame <= this.IdEmpregado.hDT_ADM.AddMonths(12))
                                {
                                    if (complementar.dataUltimo >= this.IdEmpregado.hDT_ADM.AddMonths(6))
                                        xMeses = -12;
                                    else
                                        xMeses = -6;
                                }
                                else if (this.DataExame > this.IdEmpregado.hDT_ADM.AddMonths(12) && this.DataExame <= this.IdEmpregado.hDT_ADM.AddMonths(18))
                                {
                                    if (complementar.dataUltimo < this.IdEmpregado.hDT_ADM.AddMonths(6))
                                        xMeses = -6;
                                    else
                                        xMeses = -12;
                                }
                            }



                            //audiometria - regra única - 120 dias
                            //this.IdExameDicionario.Find();
                            //if (this.IdExameDicionario.Id == 2 && xExame.ToUpper().Trim() == "AUDIOMETRIA")
                            //{
                            //    if (complementar.dataUltimo.AddDays(120) < this.DataExame)
                            //    {
                            //        string zData = "  /  /    ";
                            //        xExame = xExame.Substring(0, 38) + zData;
                            //    }
                            //    else
                            //    {
                            //        System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");
                            //        string zData = complementar.dataUltimo.ToString("dd/MM/yyyy");
                            //        xExame = xExame.Substring(0, 38) + zData;
                            //    }
                            //}
                            //else 
                            if (complementar.dataUltimo >= this.DataExame.AddMonths(xMeses))
                            {
                                string zData;

                                //                                if (Exibir_Datas == true) zData = complementar.dataUltimo.ToString("dd/MM/yyyy");
                                //                                else zData = "  /  /    ";

                                if (Exibir_Datas == true)
                                {

                                    if (xDataBranco != "" && xDataBranco != "  /  /    ")
                                    {
                                        System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");

                                        if (xDesconsiderarCompl == false)
                                        {
                                            if (complementar.dataUltimo < System.Convert.ToDateTime(xDataBranco, ptBr))
                                                zData = "  /  /    ";
                                            else
                                                zData = complementar.dataUltimo.ToString("dd/MM/yyyy");
                                        }
                                        else
                                        {
                                            //if (System.Convert.ToDateTime(xDataBranco, ptBr) <= complementar.dataUltimo)
                                            if (this.DataExame.Date >= System.Convert.ToDateTime(xDataBranco, ptBr))
                                                zData = "  /  /    ";
                                            else
                                                zData = complementar.dataUltimo.ToString("dd/MM/yyyy");
                                        }
                                    }
                                    else
                                    {
                                        zData = complementar.dataUltimo.ToString("dd/MM/yyyy");
                                    }
                                }
                                else
                                {
                                    zData = "  /  /    ";
                                }

                                xExame = xExame.Substring(0, 38) + zData;
                            }
                            else
                            {
                                if (xExame.Substring(39, 4) == "    ")
                                {
                                    xExame = xExame.Substring(0, 40) + "/" + xExame.Substring(41, 2) + "/    "; //"/" + xExame.Substring(43);
                                }
                                else
                                {
                                    xExame = xExame + "";
                                }

                            }
                        }


                        //ret.Append(i++.ToString()
                        //            + ". "
                        //            + xExame + "_"
                        //            + quebraLinha);


                        if (i < 10)
                        {
                            ret.Append(i++.ToString()
                                        + ". "
                                        + xExame);
                        }
                        else
                        {
                            ret.Append(i++.ToString()
                                        + "."
                                        + xExame);
                        }


                        if (i <= 10) ret.Append("   ");
                        else ret.Append("  ");

                        if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("GLOBAL") > 0)
                        {
                            if (i % 2 == 1 || complementares.Count <= 12)
                            {
                                ret.Append(quebraLinha);
                            }
                            else
                            {
                                ret.Append("   ");
                            }
                        }
                        else
                        {
                            if (i % 2 == 1 || complementares.Count <= 9)
                            {
                                ret.Append(quebraLinha);
                            }
                            else
                            {
                                ret.Append("   ");
                            }
                        }





                        //}
                    }

                }

            }

            if (TemPreventivo)
            {
                if (complementares.Count > 9)
                {

                    if (i > 0 && i % 2 == 1) i = i + 2;

                    i = i / 2;
                    for (int zCont = i; zCont < 10; zCont++)
                        ret.Append(System.Environment.NewLine);

                    //ret.Append(System.Environment.NewLine + "* (este exame é uma exigência do cliente, sem relação com o seu risco ocupacional. Portanto, a sua realização não é obrigatória e caso exista alguma alteração, o mesmo não deverá ser levado em conta na sua aptidão laboral.)");   //por iniciativa da empresa)");
                    //ret.Append(System.Environment.NewLine + "* (exame realizado por solicitação do cliente, sem relação com risco ocupacional deste trabalhador, sua realização não é obrigatória e caso tenha resultado anormal, não deverá ser considerado na aptidão ou inaptidão objeto deste exame.)");
                }
            }
            //if (TemPreventivo)
            //{
            //    if (i > 0 && i % 2 == 1) i = i + 2;

            //    i = i / 2;
            //    for (int zCont = i; zCont < 10; zCont++)
            //        ret.Append(System.Environment.NewLine);

            //    //ret.Append(System.Environment.NewLine + "* (este exame é uma exigência do cliente, sem relação com o seu risco ocupacional. Portanto, a sua realização não é obrigatória e caso exista alguma alteração, o mesmo não deverá ser levado em conta na sua aptidão laboral.)");   //por iniciativa da empresa)");
            //    //ret.Append(System.Environment.NewLine + "* (exame realizado por solicitação do cliente, sem relação com risco ocupacional deste trabalhador, sua realização não é obrigatória e caso tenha resultado anormal, não deverá ser considerado na aptidão ou inaptidão objeto deste exame.)");
            //}


            return ret.ToString();
        }


        //public string GetPlanejamentoExamesAso_Formatado(Ghe ghe, bool ExamesSomentePeriodoPcmso)
        //{
        //    StringBuilder ret = new StringBuilder();

        //    string xExame;
        //    string xEspacos;

        //    string xAux = "";

        //    int xMeses = 0;

        //    bool TemPreventivo = false;

        //    int i = 1;

        //    string quebraLinha;


        //    quebraLinha = "\n";

        //    xEspacos = "                                                ";
        //    xExame = "";

        //    complementares = ExamesComplementares(ExamesSomentePeriodoPcmso);

        //    foreach (ExameComplementar complementar in complementares)
        //    {
        //        //Preventivo somente se tiver exames feitos
        //        if (complementar.Preventivo)
        //        {
        //            if (complementar.dataUltimo != new DateTime())
        //            {
        //                //ret.Append(i++.ToString()
        //                //            + ". "
        //                //            + complementar.Exame
        //                //            + " - "
        //                //            + complementar.dataUltimo.ToString("dd-MM-yyyy")
        //                //            + " *"
        //                //            + quebraLinha);
        //                xExame = complementar.Exame.Trim();

        //                if (xExame.Length > 0)
        //                {
        //                    if (xExame.Length < xEspacos.Length)
        //                    {
        //                        xExame = xExame + xEspacos.Substring(1, xEspacos.Length - xExame.Length);
        //                    }


        //                    xMeses = -12;

        //                    if (IdExameDicionario.Descricao.ToString() == "Periódico")
        //                    {
        //                        if (complementar.Periodicidade.IndexOf("Periódico (2 anos)") >= 0)
        //                        {
        //                            xMeses = -24;
        //                        }
        //                        else if (complementar.Periodicidade.IndexOf("Periódico (3 anos)") >= 0)
        //                        {
        //                            xMeses = -36;
        //                        }
        //                        else if (complementar.Periodicidade.IndexOf("Periódico (4 anos)") >= 0)
        //                        {
        //                            xMeses = -48;
        //                        }
        //                        else if (complementar.Periodicidade.IndexOf("Periódico (5 anos)") >= 0)
        //                        {
        //                            xMeses = -60;
        //                        }
        //                    }


        //                    xAux = "1-" + complementar.Periodicidade.ToString() + "-" + complementar.Periodicidade.IndexOf("Periódico (5 anos)").ToString() + " - " + (complementar.dataUltimo >= this.DataExame.AddMonths(xMeses)).ToString();

        //                    if (complementar.dataUltimo >= this.DataExame.AddMonths(xMeses))
        //                    {
        //                        string zData;

        //                        zData = complementar.dataUltimo.ToString("dd/MM/yyyy");

        //                        xExame = xExame.Substring(0, 36) + zData;
        //                    }
        //                    else
        //                    {
        //                        if (xExame.Substring(39, 4) == "    ")
        //                        {
        //                            xExame = xExame.Substring(0, 38) + "/" + xExame.Substring(39, 4) + "/" + xExame.Substring(43);
        //                        }

        //                    }

        //                }
        //                ret.Append(i++.ToString()
        //                            + ". "
        //                            + xExame + "_"
        //                            + " *"
        //                            + quebraLinha);

        //                TemPreventivo = true;
        //            }
        //            else if (!this.DescExameComplPreventivo)
        //            {
        //                //ret.Append(i++.ToString()
        //                //            + ". "
        //                //            + complementar.Exame
        //                //            + "  ___/___/______"
        //                //            + " *"
        //                //            + quebraLinha);
        //                xExame = complementar.Exame.Trim();

        //                if (xExame.Length > 0)
        //                {
        //                    if (xExame.Length < xEspacos.Length)
        //                    {
        //                        xExame = xExame + xEspacos.Substring(1, xEspacos.Length - xExame.Length);
        //                    }


        //                    xMeses = -12;

        //                    if (IdExameDicionario.Descricao.ToString() == "Periódico")
        //                    {
        //                        if (complementar.Periodicidade.IndexOf("Periódico (2 anos)") >= 0)
        //                        {
        //                            xMeses = -24;
        //                        }
        //                        else if (complementar.Periodicidade.IndexOf("Periódico (3 anos)") >= 0)
        //                        {
        //                            xMeses = -36;
        //                        }
        //                        else if (complementar.Periodicidade.IndexOf("Periódico (4 anos)") >= 0)
        //                        {
        //                            xMeses = -48;
        //                        }
        //                        else if (complementar.Periodicidade.IndexOf("Periódico (5 anos)") >= 0)                                                                            
        //                        {
        //                            xMeses = -60;
        //                        }
        //                    }

        //                    xAux = "2-" + complementar.Periodicidade.ToString() + "-" + complementar.Periodicidade.IndexOf("Periódico (5 anos)").ToString() + " - " + (complementar.dataUltimo >= this.DataExame.AddMonths(xMeses)).ToString();

        //                    if (complementar.dataUltimo >= this.DataExame.AddMonths(xMeses)) 

        //                    {
        //                        string zData;

        //                        zData = complementar.dataUltimo.ToString("dd/MM/yyyy");

        //                        xExame = xExame.Substring(0, 36) + zData;
        //                    }
        //                    else
        //                    {
        //                        if (xExame.Substring(39, 4) == "    ")
        //                        {
        //                            xExame = xExame.Substring(0, 38) + "/" + xExame.Substring(39, 4) + "/" + xExame.Substring(43);
        //                        }

        //                    }

        //                }

        //                ret.Append(i++.ToString()
        //                            + ". "
        //                            + xExame + "_"
        //                            + " *"
        //                            + quebraLinha);

        //                TemPreventivo = true;
        //            }
        //        }
        //        else
        //        {
        //            //if (complementar.dataUltimo == new DateTime())
        //            //    ret.Append(i++.ToString() + ". " + complementar.Exame + quebraLinha);
        //            //else
        //            //{
        //                //ret.Append(i++.ToString()
        //                //            + ". "
        //                //            + complementar.Exame
        //                //            + " - " + complementar.dataUltimo.ToString("dd-MM-yyyy")
        //                //            + quebraLinha);


        //                xExame = complementar.Exame.Trim();

        //                if (xExame.Length > 0)
        //                {
        //                    if (xExame.Length < xEspacos.Length)
        //                    {
        //                        xExame = xExame + xEspacos.Substring(1, xEspacos.Length - xExame.Length);
        //                    }


        //                    xMeses = -12;

        //                    if (IdExameDicionario.Descricao.ToString() == "Periódico")
        //                    {
        //                        xAux = "z";
        //                        if (complementar.Periodicidade.IndexOf("Periódico (2 anos)") >= 0)
        //                        {
        //                            xMeses = -24;
        //                        }
        //                        else if (complementar.Periodicidade.IndexOf("Periódico (3 anos)") >= 0)
        //                        {
        //                            xMeses = -36;
        //                        }
        //                        else if (complementar.Periodicidade.IndexOf("Periódico (4 anos)") >= 0)
        //                        {
        //                            xMeses = -48;
        //                        }
        //                        else if (complementar.Periodicidade.IndexOf("Periódico (5 anos)") >= 0)
        //                        {
        //                            xMeses = -60;
        //                        }
        //                    }


        //                    //xAux = xAux + "3-" + complementar.Periodicidade.ToString() + "-" + complementar.Periodicidade.IndexOf("Periódico (5 anos)").ToString() + " - " + (complementar.dataUltimo >= this.DataExame.AddMonths(xMeses)).ToString() + " - " + IdExameDicionario.Descricao.ToString(); 
        //                    xAux = xAux + "3-" + " - " + IdExameDicionario.Descricao.ToString() + "--" + IdExameDicionario.ToString(); 

        //                    if (complementar.dataUltimo >= this.DataExame.AddMonths(xMeses)) 

        //                    {
        //                        string zData;

        //                        zData = complementar.dataUltimo.ToString("dd/MM/yyyy");

        //                        xExame = xExame.Substring(0, 36) + zData;
        //                    }
        //                    else
        //                    {
        //                        if (xExame.Substring(39, 4) == "    ")
        //                        {
        //                            xExame = xExame.Substring(0, 38) + "/" + xExame.Substring(39, 4) + "/" + xExame.Substring(43);
        //                        }

        //                    }

        //                }


        //                ret.Append( i++.ToString()
        //                            + ". "
        //                            + xExame + "_"
        //                            + quebraLinha);
        //            //}
        //        }

        //    }

        //    if (TemPreventivo)
        //        ret.Append("* (por iniciativa da empresa)");

        //    return ret.ToString();

        //}
        #endregion

        #region AddCompromisso

        private void AddCompromisso(Usuario usuario)
        {
            if (this.IdCompromisso.Id == 0)
                this.IdCompromisso.Inicialize();

            if (this.IdMedico.mirrorOld == null)
                this.IdMedico.Find();

            if (this.IdEmpregado.mirrorOld == null)
                this.IdEmpregado.Find();

            if (this.IdEmpregado.nID_EMPR.mirrorOld == null)
                this.IdEmpregado.nID_EMPR.Find();

            if (this.IdExameDicionario.mirrorOld == null)
                this.IdExameDicionario.Find();

            int hora = Convert.ToInt32(this.HoraAgendamento.Substring(0, this.HoraAgendamento.IndexOf(":")));
            int minuto = Convert.ToInt32(this.HoraAgendamento.Substring(this.HoraAgendamento.IndexOf(":") + 1, this.HoraAgendamento.Length - (this.HoraAgendamento.IndexOf(":") + 1)));

            System.Text.StringBuilder str = new System.Text.StringBuilder();
            str.Append("Médico: " + this.IdMedico.NomeCompleto);
            str.Append("\r\n");
            str.Append("Empresa: " + this.IdEmpregado.nID_EMPR.NomeAbreviado);
            str.Append("\r\n");
            str.Append("Empregado: " + this.IdEmpregado.tNO_EMPG);
            str.Append("\r\n");

            this.IdCompromisso.Assunto = this.IdEmpregado.nID_EMPR.NomeAbreviado + " - " + this.IdExameDicionario.Nome;
            this.IdCompromisso.Descricao = str.ToString();
            this.IdCompromisso.AvisoConflito = true;
            this.IdCompromisso.IdCategoria.Id = (int)Categorias.Medico;
            this.IdCompromisso.IdPessoa = this.IdMedico.IdPessoa;
            this.IdCompromisso.IdPessoaContato = clinica;
            this.IdCompromisso.IdPessoaCriador = usuario.IdPessoa;
            this.IdCompromisso.DataInicio = new DateTime(this.DataExame.Year, this.DataExame.Month, this.DataExame.Day, hora, minuto, 0);
            this.IdCompromisso.DataTermino = this.IdCompromisso.DataInicio.AddMinutes(clinica.DuracaoExame);
            this.IdCompromisso.Save();

            this.DataExame = this.IdCompromisso.DataInicio;

        }
        #endregion

        #region TransfereExame

        public static void TransfereExame(int IdClienteDe, int IdClientePara)
        {
            List<Empregado> empregadosDe = new Empregado().Find<Empregado>("nID_EMPR=" + IdClienteDe 
                              +" AND nID_EMPREGADO IN (SELECT IdEmpregado FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.ExameBase)"
                                                                            + " ORDER BY tNO_EMPG");
            foreach (Empregado empregadoDe in empregadosDe)
            {
                Empregado empregadoPara = new Empregado();
                empregadoPara.Find("nID_EMPR=" + IdClientePara 
                                + " AND tNO_EMPG='" + empregadoDe.tNO_EMPG + "'");

                if (empregadoPara.Id == 0)
                    continue;

                List<ExameBase> exames = new ExameBase().Find<ExameBase>("IdEmpregado=" + empregadoDe.Id);

                foreach (ExameBase exame in exames)
                {
                    exame.IdEmpregado.Id = empregadoPara.Id;
                    exame.Save();
                }
            }
        }


        #endregion
    }
    #endregion

    #region class ExameNaoRealizado

    [Database("opsa","ExameNaoRealizado","IdExameNaoRealizado")]
	public class ExameNaoRealizado : Ilitera.Data.Table
	{
     	private int _IdExameNaoRealizado;	
		private Empregado _IdEmpregado;
		private ExameDicionario _IdExameDicionario;
		private Juridica _IdJuridica;
		private Medico _IdMedico;
        private ConvocacaoExame _IdConvocacaoExame;
        private MotivoFalta _IndMotivoFalta;
		private DateTime _DataExame;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public ExameNaoRealizado()
		{

		}

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public ExameNaoRealizado(int Id)
		{
			this.Find(Id);
		}

		public override int Id
		{
			get{return _IdExameNaoRealizado;}
			set{_IdExameNaoRealizado = value;}
		}
		public Empregado IdEmpregado
		{
			get{return _IdEmpregado;}
			set{_IdEmpregado = value;}
		}
		public ExameDicionario IdExameDicionario
		{
			get{return _IdExameDicionario;}
			set{_IdExameDicionario = value;}
		}
		public Juridica IdJuridica
		{
			get{return _IdJuridica;}
			set{_IdJuridica = value;}
		}
		public Medico IdMedico
		{
			get{return _IdMedico;}
			set{_IdMedico = value;}
		}
        public ConvocacaoExame IdConvocacaoExame
        {
            get { return _IdConvocacaoExame; }
            set { _IdConvocacaoExame = value; }
        }
        public MotivoFalta IndMotivoFalta
        {
            get { return _IndMotivoFalta; }
            set { _IndMotivoFalta = value; }
        }
		public DateTime DataExame
		{
			get{return _DataExame;}
			set{_DataExame = value;}
		}

        public enum MotivoFalta : int
        {
            AusenciaPorMotivoNaoJustificado,
            FaltaAoServico,
            Ferias,
            TrabalhosExternos,
            DemissaoNaoInformada,
            AfastamentoNaoInformada
        }

        public string GetMotivoFalta()
        {
            return GetMotivoFalta(this.IndMotivoFalta);
        }

        public static string GetMotivoFalta(MotivoFalta indMotivoFalta)
        {
            string ret;

            switch (indMotivoFalta)
            {
                case MotivoFalta.FaltaAoServico:
                    ret = "falta ao serviço";
                    break;
                case MotivoFalta.Ferias:
                    ret = "férias";
                    break;
                case MotivoFalta.TrabalhosExternos:
                    ret = "trabalhos externos";
                    break;
                case MotivoFalta.AusenciaPorMotivoNaoJustificado:
                    ret = "ausência por motivo não justificado";
                    break;
                case MotivoFalta.DemissaoNaoInformada:
                    ret = "demissão não informada no cadastro";
                    break;
                case MotivoFalta.AfastamentoNaoInformada:
                    ret = "afastamento não informado no cadastro";
                    break;
                default:
                    ret = string.Empty;
                    break;
            }

            return ret;
        }
    }
    #endregion

    #region class AdendoExame

    [Database("opsa", "AdendoExame", "IdAdendoExame", "", "Adendo ao Exame")]
	public class AdendoExame : Ilitera.Data.Table
	{
		private int _IdAdendoExame;	
		private ExameBase _IdExameBase;
		private Medico _IdMedico;
		private DateTime _Data;
		private string _Descricao = string.Empty;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public AdendoExame()
		{
		}

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public AdendoExame(int Id)
		{
			this.Find(Id);
		}

		public override int Id
		{
			get{return _IdAdendoExame;}
			set{_IdAdendoExame = value;}
		}
		public ExameBase IdExameBase
		{
			get{return _IdExameBase;}
			set{_IdExameBase = value;}
		}
		public Medico IdMedico
		{
			get{return _IdMedico;}
			set{_IdMedico = value;}
		}
		public DateTime Data
		{
			get{return _Data;}
			set{_Data = value;}
		}
		[Obrigatorio(true, "A Descrição do Adendo ao Exame é Obrigatória!")]
		public string Descricao
		{
			get{return _Descricao;}
			set{_Descricao = value;}
		}

		public override string ToString()
		{
			if(this.IdMedico.IdJuridica==null)
				this.IdMedico.Find();

			System.Text.StringBuilder str = new StringBuilder();
			str.Append(this.Data.ToString("dd-MM-yyyy hh:mm"));
			str.Append(" - ");
			str.Append(this.IdMedico.ToString());
			str.Append("\r\n");
			str.Append(this.Descricao);

			return str.ToString ();
		}

		public DataSet ListaAdendoExames(string strWhere)
		{	
			DataSet ds = new DataSet();
			DataTable table = new DataTable("Result");
			table.Columns.Add("Id", Type.GetType("System.String"));
			table.Columns.Add("IdExameBase", Type.GetType("System.String"));
			table.Columns.Add("Data", Type.GetType("System.String"));
			table.Columns.Add("Medico", Type.GetType("System.String"));
			table.Columns.Add("Descricao", Type.GetType("System.String"));
			ds.Tables.Add(table);
			DataRow newRow;

			ArrayList list = this.Find(strWhere);
			for(int i = 0; i<list.Count; i++)
			{
				((AdendoExame)list[i]).IdMedico.Find();
				newRow = ds.Tables[0].NewRow();
				newRow["Id"]			= ((AdendoExame)list[i]).Id.ToString();
				newRow["IdExameBase"]	= ((AdendoExame)list[i]).IdExameBase.Id.ToString();
				newRow["Data"]			= ((AdendoExame)list[i]).Data.ToString("dd-MM-yyyy");
				newRow["Medico"]		= ((AdendoExame)list[i]).IdMedico.NomeCompleto;
				newRow["Descricao"]		= ((AdendoExame)list[i]).Descricao;
				ds.Tables[0].Rows.Add(newRow);
			}
			return ds;
		}
    }
    #endregion

    #region class Complementar

    [Database("opsa", "ComplementarUnidadeMedida", "IdComplementarUnidadeMedida", "", "Unidade Medida")]
    public class ComplementarUnidadeMedida : Ilitera.Data.Table
    {
        private int _IdComplementarUnidadeMedida;
        private string _UnidadeMedida;

        public ComplementarUnidadeMedida()
        {

        }

        public ComplementarUnidadeMedida(int Id)
        {
            this.Find(Id);
        }

        public override int Id
        {
            get { return _IdComplementarUnidadeMedida; }
            set { _IdComplementarUnidadeMedida = value; }
        }

        public string UnidadeMedida
        {
            get { return _UnidadeMedida; }
            set { _UnidadeMedida = value; }
        }

    }




    [Database("opsa", "Complementar", "IdComplementar", "", "Exame Complementar")]
    public class Complementar : Ilitera.Opsa.Data.ExameBase
    {
        private int _IdComplementar;
        private float _Resultado;
        private float _Referencia;
        private float _IBPM;
        private int _IdComplementarUnidadeMedida;


        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public Complementar()
        {

        }

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public Complementar(int Id)
        {
            this.Find(Id);
        }

        public override int Id
        {
            get { return _IdComplementar; }
            set { _IdComplementar = value; }
        }

        public float Resultado
        {
            get { return _Resultado; }
            set { _Resultado = value; }
        }

        public float Referencia
        {
            get { return _Referencia; }
            set { _Referencia = value; }
        }

        public float IBPM
        {
            get { return _IBPM; }
            set { _IBPM = value; }
        }

        public int IdComplementarUnidadeMedida
        {
            get { return _IdComplementarUnidadeMedida; }
            set { _IdComplementarUnidadeMedida = value; }
        }


        public string GetAnotacaoPadrao()
        {
            string ret = string.Empty;

            if (this.IdExameDicionario.Id == (int)Exames.HemogramaCompleto
                || this.IdExameDicionario.Id == (int)Exames.HemogramaCompletoContPaquetas)
                ret = "HB: \r\n"
                    + "HT: \r\n"
                    + "Leucócitos: \r\n"
                    + "Plaquetas: \r\n";
            else if (this.IdExameDicionario.Id == (int)Exames.ProvaFuncaoHepatica)
                ret = "TGO: \r\n"
                    + "TGP: \r\n"
                    + "GGT: \r\n";
            else if (this.IdExameDicionario.Id == (int)Exames.ProvaFuncaoRenal)
                ret = "Uréia: \r\n"
                    + "Creatinina: \r\n";

            return ret;
        }

        public DataSet ListaExamesComplementar(string strWhere)
        {
            DataSet ds = new DataSet();
            DataTable table = new DataTable("Result");
            table.Columns.Add("Id", Type.GetType("System.String"));
            table.Columns.Add("Data", Type.GetType("System.String"));
            table.Columns.Add("Tipo", Type.GetType("System.String"));
            table.Columns.Add("Descricao", Type.GetType("System.String"));
            table.Columns.Add("Medico", Type.GetType("System.String"));
            ds.Tables.Add(table);
            DataRow newRow;
            ArrayList list = this.Find(strWhere);
            for (int i = 0; i < list.Count; i++)
            {
                ((Complementar)list[i]).IdExameDicionario.Find();
                ((Complementar)list[i]).IdMedico.Find();
                newRow = ds.Tables[0].NewRow();
                newRow["Id"] = ((Complementar)list[i]).Id.ToString();
                newRow["Data"] = ((Complementar)list[i]).DataExame.ToString("dd-MM-yyyy");
                newRow["Tipo"] = ((Complementar)list[i]).IdExameDicionario.Nome;
                newRow["Descricao"] = ((Complementar)list[i]).GetResultadoExame();
                newRow["Medico"] = ((Complementar)list[i]).IdMedico.NomeCompleto;
                ds.Tables[0].Rows.Add(newRow);
            }
            return ds;
        }
    }
    #endregion

    #region class ClinicoNaoOcupacional

    [Database("opsa", "ClinicoNaoOcupacional", "IdClinicoNaoOcupacional", "", "Exame Clínico não Ocupacional")]
    public class ClinicoNaoOcupacional : Ilitera.Opsa.Data.ExameBase
    {
        private int _IdClinicoNaoOcupacional;
        private QueixaClinica _IdQueixaClinica;
        private ProcedimentoClinico _IdProcedimentoClinico;
        private string _Tipo;
        private bool _Alerta_Medico;
        private bool _Paciente_Critico;

        private string _Motivo_Atendimento="";
        private string _Responsavel_Atendimento = "";
        private string _Historico_Doencas = "";
        private string _Queixa_Principal = "";
        private string _Outras_Queixas = "";
        private string _Alergia_Medicacao = "";
        private string _Alergia_Medicacao_Quais = "";
        private string _Medicacao_Continua = "";
        private string _Medicacao_Continua_Quais = "";
        private string _Diagnostico_Enfermagem = "";
        private string _Diagnostico_Enfermagem_Outros = "";
        private string _Planejamento_Enfermagem = "";
        private string _Planejamento_Enfermagem_Outros = "";
        private string _Implementacao = "";
        private string _Avaliacao_Enfermagem = "";
        private string _Hora_Atendimento = "";
        private float _SaturacaoOxigenio=0;
        private string _Conduzido_PS = "";
        private string _ProntoSocorro = "";
        private bool _Confidencial;
        private int _IdUsuario_Confidencial;




        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public ClinicoNaoOcupacional()
        {
            this.Inicialize();
            this.IdExameDicionario.Id = (int)Exames.NaoOcupacional;
        }

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public ClinicoNaoOcupacional(int Id)
        {
            this.Find(Id);
        }
        public override int Id
        {
            get { return _IdClinicoNaoOcupacional; }
            set { _IdClinicoNaoOcupacional = value; }
        }
        public QueixaClinica IdQueixaClinica
        {
            get { return _IdQueixaClinica; }
            set { _IdQueixaClinica = value; }
        }
        public ProcedimentoClinico IdProcedimentoClinico
        {
            get { return _IdProcedimentoClinico; }
            set { _IdProcedimentoClinico = value; }
        }
        public String Tipo
        {
            get { return _Tipo; }
            set { _Tipo = value; }
        }
        public bool Alerta_Medico
        {
            get { return _Alerta_Medico; }
            set { _Alerta_Medico = value; }
        }
        public bool Paciente_Critico
        {
            get { return _Paciente_Critico; }
            set { _Paciente_Critico = value; }
        }

        public string Motivo_Atendimento
        {
            get { return _Motivo_Atendimento; }
            set { _Motivo_Atendimento = value; }
        }

        public string Responsavel_Atendimento
        {
            get { return _Responsavel_Atendimento; }
            set { _Responsavel_Atendimento = value; }
        }

        public string Historico_Doencas
        {
            get { return _Historico_Doencas; }
            set { _Historico_Doencas = value; }
        }

        public string Queixa_Principal
        {
            get { return _Queixa_Principal; }
            set { _Queixa_Principal = value; }
        }

        public string Outras_Queixas
        {
            get { return _Outras_Queixas; }
            set { _Outras_Queixas = value; }
        }

        public string Alergia_Medicacao
        {
            get { return _Alergia_Medicacao; }
            set { _Alergia_Medicacao = value; }
        }

        public string Alergia_Medicacao_Quais
        {
            get { return _Alergia_Medicacao_Quais; }
            set { _Alergia_Medicacao_Quais = value; }
        }

        public string Medicacao_Continua
        {
            get { return _Medicacao_Continua; }
            set { _Medicacao_Continua = value; }
        }

        public string Medicacao_Continua_Quais
        {
            get { return _Medicacao_Continua_Quais; }
            set { _Medicacao_Continua_Quais = value; }
        }

        public string Diagnostico_Enfermagem
        {
            get { return _Diagnostico_Enfermagem; }
            set { _Diagnostico_Enfermagem = value; }
        }

        public string Diagnostico_Enfermagem_Outros
        {
            get { return _Diagnostico_Enfermagem_Outros; }
            set { _Diagnostico_Enfermagem_Outros = value; }
        }

        public string Planejamento_Enfermagem
        {
            get { return _Planejamento_Enfermagem; }
            set { _Planejamento_Enfermagem = value; }
        }

        public string Planejamento_Enfermagem_Outros
        {
            get { return _Planejamento_Enfermagem_Outros; }
            set { _Planejamento_Enfermagem_Outros = value; }
        }

        public string Implementacao
        {
            get { return _Implementacao; }
            set { _Implementacao = value; }
        }

        public string Avaliacao_Enfermagem
        {
            get { return _Avaliacao_Enfermagem; }
            set { _Avaliacao_Enfermagem = value; }
        }

        public string Hora_Atendimento
        {
            get { return _Hora_Atendimento; }
            set { _Hora_Atendimento = value; }
        }

        public float SaturacaoOxigenio
        {
            get { return _SaturacaoOxigenio; }
            set { _SaturacaoOxigenio = value; }
        }

        public string Conduzido_PS
        {
            get { return _Conduzido_PS; }
            set { _Conduzido_PS = value; }
        }

        public string ProntoSocorro
        {
            get { return _ProntoSocorro; }
            set { _ProntoSocorro = value; }
        }

        public bool Confidencial
        {
            get { return _Confidencial; }
            set { _Confidencial = value; }
        }

        public int IdUsuario_Confidencial
        {
            get { return _IdUsuario_Confidencial; }
            set { _IdUsuario_Confidencial = value; }
        }

    }

    public class ExameFisicoAmbulatorial
    {
        private ClinicoNaoOcupacional clinicoNaoOcupacional;
        private ExameFisico exameFisico;

        public ExameFisicoAmbulatorial()
        {
            clinicoNaoOcupacional = new ClinicoNaoOcupacional();
            exameFisico = new ExameFisico();
            exameFisico.Inicialize();
        }
        public int Id
        {
            get { return clinicoNaoOcupacional.Id; }
            set { clinicoNaoOcupacional.Id = value; }
        }
        public ExameFisicoAmbulatorial(int Id)
        {
            clinicoNaoOcupacional = new ClinicoNaoOcupacional();
            exameFisico = new ExameFisico();
            this.Find(Id);
        }
        public void Find(int Id)
        {
            clinicoNaoOcupacional.Find(Id);
            exameFisico.Find("IdExameBase=" + clinicoNaoOcupacional.Id);
        }
        public Empregado IdEmpregado
        {
            get { return clinicoNaoOcupacional.IdEmpregado; }
            set { clinicoNaoOcupacional.IdEmpregado = value; }
        }
        public Juridica IdJuridica
        {
            get { return clinicoNaoOcupacional.IdJuridica; }
            set { clinicoNaoOcupacional.IdJuridica = value; }
        }
        public Medico IdMedico
        {
            get { return clinicoNaoOcupacional.IdMedico; }
            set { clinicoNaoOcupacional.IdMedico = value; }
        }
        public DateTime DataExame
        {
            get { return clinicoNaoOcupacional.DataExame; }
            set { clinicoNaoOcupacional.DataExame = value; }
        }
        public float Altura
        {
            get { return exameFisico.Altura; }
            set { exameFisico.Altura = value; }
        }
        public float Peso
        {
            get { return exameFisico.Peso; }
            set { exameFisico.Peso = value; }
        }
        public string PressaoArterial
        {
            get { return exameFisico.PressaoArterial; }
            set { exameFisico.PressaoArterial = value; }
        }
        public short Sistolica
        {
            get { return exameFisico.Sistolica; }
            set { exameFisico.Sistolica = value; }
        }
        public short Diastolica
        {
            get { return exameFisico.Diastolica; }
            set { exameFisico.Diastolica = value; }
        }

        public int Pulso
        {
            get { return exameFisico.Pulso; }
            set { exameFisico.Pulso = value; }
        }
        public string Glicose
        {
            get { return exameFisico.Glicose; }
            set { exameFisico.Glicose = value; }
        }
        public float Temperatura
        {
            get { return exameFisico.Temperatura; }
            set { exameFisico.Temperatura = value; }
        }
        public QueixaClinica IdQueixaClinica
        {
            get { return clinicoNaoOcupacional.IdQueixaClinica; }
            set { clinicoNaoOcupacional.IdQueixaClinica = value; }
        }
        public string Tipo
        {
            get { return clinicoNaoOcupacional.Tipo; }
            set { clinicoNaoOcupacional.Tipo = value; }
        }
        public ProcedimentoClinico IdProcedimentoClinico
        {
            get { return clinicoNaoOcupacional.IdProcedimentoClinico; }
            set { clinicoNaoOcupacional.IdProcedimentoClinico = value; }
        }
        public string Prontuario
        {
            get { return clinicoNaoOcupacional.Prontuario; }
            set { clinicoNaoOcupacional.Prontuario = value; }
        }
        public bool Alerta_Medico
        {
            get { return clinicoNaoOcupacional.Alerta_Medico; }
            set { clinicoNaoOcupacional.Alerta_Medico = value; }
        }
        public bool Paciente_Critico
        {
            get { return clinicoNaoOcupacional.Paciente_Critico; }
            set { clinicoNaoOcupacional.Paciente_Critico = value; }
        }
        public string Motivo_Atendimento
        {
            get { return clinicoNaoOcupacional.Motivo_Atendimento; }
            set { clinicoNaoOcupacional.Motivo_Atendimento = value; }
        }

        public string Responsavel_Atendimento
        {
            get { return clinicoNaoOcupacional.Responsavel_Atendimento; }
            set { clinicoNaoOcupacional.Responsavel_Atendimento = value; }
        }

        public string Historico_Doencas
        {
            get { return clinicoNaoOcupacional.Historico_Doencas; }
            set { clinicoNaoOcupacional.Historico_Doencas = value; }
        }

        public string Queixa_Principal
        {
            get { return clinicoNaoOcupacional.Queixa_Principal; }
            set { clinicoNaoOcupacional.Queixa_Principal = value; }
        }

        public string Outras_Queixas
        {
            get { return clinicoNaoOcupacional.Outras_Queixas; }
            set { clinicoNaoOcupacional.Outras_Queixas = value; }
        }

        public string Alergia_Medicacao
        {
            get { return clinicoNaoOcupacional.Alergia_Medicacao; }
            set { clinicoNaoOcupacional.Alergia_Medicacao = value; }
        }

        public string Alergia_Medicacao_Quais
        {
            get { return clinicoNaoOcupacional.Alergia_Medicacao_Quais; }
            set { clinicoNaoOcupacional.Alergia_Medicacao_Quais = value; }
        }

        public string Medicacao_Continua
        {
            get { return clinicoNaoOcupacional.Medicacao_Continua; }
            set { clinicoNaoOcupacional.Medicacao_Continua = value; }
        }

        public string Medicacao_Continua_Quais
        {
            get { return clinicoNaoOcupacional.Medicacao_Continua_Quais; }
            set { clinicoNaoOcupacional.Medicacao_Continua_Quais = value; }
        }

        public string Diagnostico_Enfermagem
        {
            get { return clinicoNaoOcupacional.Diagnostico_Enfermagem; }
            set { clinicoNaoOcupacional.Diagnostico_Enfermagem = value; }
        }

        public string Diagnostico_Enfermagem_Outros
        {
            get { return clinicoNaoOcupacional.Diagnostico_Enfermagem_Outros; }
            set { clinicoNaoOcupacional.Diagnostico_Enfermagem_Outros = value; }
        }

        public string Planejamento_Enfermagem
        {
            get { return clinicoNaoOcupacional.Planejamento_Enfermagem; }
            set { clinicoNaoOcupacional.Planejamento_Enfermagem = value; }
        }

        public string Planejamento_Enfermagem_Outros
        {
            get { return clinicoNaoOcupacional.Planejamento_Enfermagem_Outros; }
            set { clinicoNaoOcupacional.Planejamento_Enfermagem_Outros = value; }
        }

        public string Implementacao
        {
            get { return clinicoNaoOcupacional.Implementacao; }
            set { clinicoNaoOcupacional.Implementacao = value; }
        }

        public string Avaliacao_Enfermagem
        {
            get { return clinicoNaoOcupacional.Avaliacao_Enfermagem; }
            set { clinicoNaoOcupacional.Avaliacao_Enfermagem = value; }
        }

        public string Hora_Atendimento
        {
            get { return clinicoNaoOcupacional.Hora_Atendimento; }
            set { clinicoNaoOcupacional.Hora_Atendimento = value; }
        }

        public float SaturacaoOxigenio
        {
            get { return clinicoNaoOcupacional.SaturacaoOxigenio; }
            set { clinicoNaoOcupacional.SaturacaoOxigenio = value; }
        }

        public string Conduzido_PS
        {
            get { return clinicoNaoOcupacional.Conduzido_PS; }
            set { clinicoNaoOcupacional.Conduzido_PS = value; }
        }

        public string ProntoSocorro
        {
            get { return clinicoNaoOcupacional.ProntoSocorro; }
            set { clinicoNaoOcupacional.ProntoSocorro = value; }
        }

        public bool  Confidencial
        {
            get { return clinicoNaoOcupacional.Confidencial; }
            set { clinicoNaoOcupacional.Confidencial = value; }
        }

        public int IdUsuario_Confidencial
        {
            get { return clinicoNaoOcupacional.IdUsuario_Confidencial; }
            set { clinicoNaoOcupacional.IdUsuario_Confidencial = value; }
        }


        public int Save(int IdUsuario)
        {
            clinicoNaoOcupacional.UsuarioId = IdUsuario;
            clinicoNaoOcupacional.Save();

            if (clinicoNaoOcupacional.IdEmpregado.mirrorOld == null)
                clinicoNaoOcupacional.IdEmpregado.Find();
                
            if (clinicoNaoOcupacional.IdEmpregado.nID_EMPR.mirrorOld == null)
                clinicoNaoOcupacional.IdEmpregado.nID_EMPR.Find();

            if (!clinicoNaoOcupacional.IdEmpregado.nID_EMPR.ContrataPCMSO.Equals((int)TipoPcmsoContratada.NaoContratada))
            {
                exameFisico.IdExameBase = clinicoNaoOcupacional;
                exameFisico.UsuarioId = IdUsuario;
                exameFisico.Save();
            }

            return clinicoNaoOcupacional.Id;
        }
        public void Delete(int IdUsuario)
        {
            clinicoNaoOcupacional.UsuarioId = IdUsuario;
            clinicoNaoOcupacional.Delete();
        }
    }
    #endregion



    #region class Clinico_Testes_Especiais

    [Database("opsa", "Clinico_Testes_Especiais", "IdClinicoTestesEspeciais")]
    public class Clinico_Testes_Especiais : Ilitera.Data.Table
    {


        private int _IdClinicoTestesEspeciais;
        private int _IdClinico;
        private string _Exame;
        private string _Resultado;
        private Int16 _TipoExame;
        private Int16 _Ordem;


        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public Clinico_Testes_Especiais()
        {

        }

        public override int Id
        {
            get { return _IdClinicoTestesEspeciais; }
            set { _IdClinicoTestesEspeciais = value; }
        }

        public int IdClinico
        {
            get { return _IdClinico; }
            set { _IdClinico = value; }
        }
        public string Exame
        {
            get { return _Exame; }
            set { _Exame = value; }
        }
        public string Resultado
        {
            get { return _Resultado; }
            set { _Resultado = value; }
        }
        public Int16 TipoExame
        {
            get { return _TipoExame; }
            set { _TipoExame = value; }
        }
        public Int16 Ordem
        {
            get { return _Ordem; }
            set { _Ordem = value; }
        }

    }

    [Database("opsa", "Clinico_Testes_Especiais_Padrao", "IdClinicoTestesEspeciais_Padrao")]
    public class Clinico_Testes_Especiais_Padrao : Ilitera.Data.Table
    {

        private int _IdClinicoTestesEspeciais_Padrao;
        private int _IdPessoa;
        private string _Exame;
        private Int16 _TipoExame;
        private Int16 _Ordem;


        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public Clinico_Testes_Especiais_Padrao()
        {

        }

        public override int Id
        {
            get { return _IdClinicoTestesEspeciais_Padrao; }
            set { _IdClinicoTestesEspeciais_Padrao = value; }
        }

        public int IdPessoa
        {
            get { return _IdPessoa; }
            set { _IdPessoa = value; }
        }
        public string Exame
        {
            get { return _Exame; }
            set { _Exame = value; }
        }
        public Int16 TipoExame
        {
            get { return _TipoExame; }
            set { _TipoExame = value; }
        }
        public Int16 Ordem
        {
            get { return _Ordem; }
            set { _Ordem = value; }
        }

    }
    #endregion






    #region class ExameBaseSaidaDocumento

    [Database("opsa", "ExameBaseSaidaDocumento", "IdExameBaseSaidaDocumento")]
    public class ExameBaseSaidaDocumento : Ilitera.Data.Table
    {
        private int _IdExameBaseSaidaDocumento;
        private ExameBase _IdExameBase;
        private SaidaDocumento _IdSaidaDocumento;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public ExameBaseSaidaDocumento()
        {

        }
        public override int Id
        {
            get { return _IdExameBaseSaidaDocumento; }
            set { _IdExameBaseSaidaDocumento = value; }
        }
        public ExameBase IdExameBase
        {
            get { return _IdExameBase; }
            set { _IdExameBase = value; }
        }
        public SaidaDocumento IdSaidaDocumento
        {
            get { return _IdSaidaDocumento; }
            set { _IdSaidaDocumento = value; }
        }
    }
    #endregion

    #region class qryExamesRealizados

    [Database("opsa", "qryExamesRealizados", "IdExameBase")]

    

    public class qryExamesRealizados : Ilitera.Data.Table
    {
        #region Properties

        private int _IdExameBase;


        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]
        public qryExamesRealizados()
        {

        }


        public override int Id
        {
            get { return _IdExameBase; }
            set { _IdExameBase = value; }
        }

        private Cliente _IdCliente;

        public Cliente IdCliente
        {
            get { return _IdCliente; }
            set { _IdCliente = value; }
        }
        private Clinica _IdClinica;

        public Clinica IdClinica
        {
            get { return _IdClinica; }
            set { _IdClinica = value; }
        }
        private GrupoEmpresa _IdGrupoEmpresa;

        public GrupoEmpresa IdGrupoEmpresa
        {
            get { return _IdGrupoEmpresa; }
            set { _IdGrupoEmpresa = value; }
        }
        private TipoPcmsoContratada _ContrataPCMSO;

        public TipoPcmsoContratada ContrataPCMSO
        {
            get { return _ContrataPCMSO; }
            set { _ContrataPCMSO = value; }
        }
        private string _Cliente;

        public string Cliente
        {
            get { return _Cliente; }
            set { _Cliente = value; }
        }
        //private string _Clinica;
        //private string _Empregado;
        //private DateTime _DataExame;
        //private DateTime _DataPagamento;
        //private double _ValorPago;
        //private ExameDicionario _IdExameDicionario;
        //private string _Tipo;
        //private IndTipoExame _IndExame;
        //private Medico _IdMedico;
        //private string _Medico;
        //private ResultadoExame _IndResultado;
        //private DateTime _Agendamento;
        //private Compromisso _IdCompromisso;
        //private bool _IsFromConvocacaoWeb;
        //private int _QtdEmpregados;
        //private Empregado _IdEmpregado;
        //private string _NomeAlocado;

        #endregion

        #region GetNumeroEmpregados

        public static int GetNumeroEmpregados(Filtro filtro)
        {
            filtro.ComOrderBy = false;

            string sql = "nID_EMPREGADO IN (SELECT IdEmpregado"
                    + " FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.qryExamesRealizados"
                    + " WHERE " + filtro.CriteriaString + ")";

            int count = new Empregado().ExecuteCount(sql);

            return count;
        }
        #endregion

        #region GetExamesRealizados

        public static DataSet GetExamesRealizados(Filtro filtro)
        {
            string sql = "SELECT IdExameBase,"
                    + " Cliente,"
                    + " DataExame,"
                    + " Agendamento,"
                    + " DataPagamento,"
                    + " ValorPago,"
                    + " IsFromConvocacaoWeb,"
                    + " Tipo,"
                    + " Empregado,"
                    + " Clinica,"
                    + " Medico "
            + "FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.qryExamesRealizados "
            + "WHERE " + filtro.CriteriaString;

            DataSet ds = new qryExamesRealizados().ExecuteDataset(sql);

            return ds;
        }

        public static DataSet GetExamesRealizados(Filtro filtro, out int QtdEmpregados)
        {
            DataSet ds = GetExamesRealizados(filtro);

            QtdEmpregados = GetNumeroEmpregados(filtro);

            return ds;
        }

        #endregion

        #region class Filtro

        public class Filtro
        {
            StringBuilder sql;
            Sintax sintax;

            public Filtro(Sintax sintax, bool ComOrderBy)
            {
                this.sintax = sintax;
                this.ComOrderBy = ComOrderBy;
            }

            #region Enum

            public enum Sintax : int
            {
                SQL,
                Crystal
            }
            #endregion

            #region CriteriaString 

            public string CriteriaString
            {
                get 
                {
                    sql = new StringBuilder();

                    AddTodos();
                    AddLocal();
                    AddPorContratoServicoPrestado();
                    AddClinicoMedico();
                    AddTipoExame();
                    AddDataExame();
                    AddPagamento();
                    AddSaidaDocumento();
                    AddResultado();
                    AddAgendado();
                    AddEncaminhadoEspecialista();
                    AddEmpregadosAtivos();

                    if (ComOrderBy)
                        AddOrderBy();

                    return sql.ToString();
                }
            }

            #endregion

            #region Todos

            private void AddTodos()
            {
                if (sintax == Sintax.SQL)
                    sql.Append("(IdExameBase = IdExameBase)");
                else
                    sql.Append("{qryExamesRealizados.ContrataPCMSO} = 1");
            }
            #endregion

            #region Local

            private Cliente _cliente;

            public Cliente cliente
            {
                get { return _cliente; }
                set { _cliente = value; }
            }
            private bool _TodosClientes;

            public bool TodosClientes
            {
                get { return _TodosClientes; }
                set { _TodosClientes = value; }
            }
            private bool _TodosGrupo;

            public bool TodosGrupo
            {
                get { return _TodosGrupo; }
                set { _TodosGrupo = value; }
            }
            private bool _PorLocalDeTrabalho;

            public bool PorLocalDeTrabalho
            {
                get { return _PorLocalDeTrabalho; }
                set { _PorLocalDeTrabalho = value; }
            }

            private void AddLocal()
            {
                if (!TodosClientes && !TodosGrupo)
                {
                    if (cliente.IdJuridicaPapel.IsLocalDeTrabalho() || PorLocalDeTrabalho)
                    {
                        if (sintax == Sintax.SQL)
                            sql.Append(" AND IdEmpregado IN (SELECT nID_EMPREGADO FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEMPREGADO WHERE nID_EMPREGADO IN (SELECT nID_EMPREGADO FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEMPREGADO_FUNCAO WHERE nID_EMPR =" + cliente.Id + "))");
                        else
                            throw new Exception("Não disponivel para Locais de Trabalho");
                    }
                    else
                    {
                        if (sintax == Sintax.SQL)
                            sql.Append(" AND IdCliente = " + cliente.Id);
                        else if (sintax == Sintax.Crystal)
                            sql.Append(" AND {qryExamesRealizados.IdCliente} = " + cliente.Id);
                    }
                }
                else
                {
                    if (TodosGrupo)//Por Grupo
                    {
                        if (sintax == Sintax.SQL)
                            sql.Append(" AND IdGrupoEmpresa=" + cliente.IdGrupoEmpresa.Id);
                        else if (sintax == Sintax.Crystal)
                            sql.Append(" AND {qryExamesRealizados.IdGrupoEmpresa} = " + cliente.IdGrupoEmpresa.Id);
                    }
                    else//Todos Grupos
                    {
                        if (sintax == Sintax.SQL)
                            sql.Append(" AND ContrataPCMSO = 1");
                    }
                }
            }
            #endregion

            #region PorContratoServicoPrestado

            public enum ServicoPrestado : int
            {
                Todos = -1,
                EmAberto = 0,
                Faturados = 1
            }

            private ServicoPrestado _IndServicoPrestado;

            public ServicoPrestado IndServicoPrestado
            {
                get { return _IndServicoPrestado; }
                set { _IndServicoPrestado = value; }
            }

            private void AddPorContratoServicoPrestado()
            {
                if (IndServicoPrestado == ServicoPrestado.Todos)
                    return;

                if (sintax == Sintax.SQL)
                {
                    sql.Append(" AND IdExameBase ");

                    if (IndServicoPrestado == ServicoPrestado.EmAberto)
                        sql.Append(" NOT IN ");
                    else
                        sql.Append(" IN ");

                    sql.Append("(SELECT IdExameBase FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.FaturamentoExameBase) ");
                }
                else if (sintax == Sintax.Crystal)
                    throw new Exception("Não disponivel para Contratos de Serviços Prestados");
            }
            #endregion

            #region ClinicoMedico

            private IExameBase _exameBase;

            public IExameBase exameBase
            {
                get { return _exameBase; }
                set { _exameBase = value; }
            }

            private void AddClinicoMedico()
            {
                if (exameBase == null)
                    return;

                if (exameBase.IdMedico.Id == 0 && exameBase.IdJuridica.Id != 0)
                {
                    if (sintax == Sintax.SQL)
                        sql.Append(" AND IdClinica = " + exameBase.IdJuridica.Id);
                    else
                        sql.Append(" AND {qryExamesRealizados.IdClinica} = " + exameBase.IdJuridica.Id);
                }

                else if (exameBase.IdMedico.Id != 0)
                {
                    if (sintax == Sintax.SQL)
                        sql.Append(" AND IdMedico = " + exameBase.IdMedico.Id);
                    else
                        sql.Append(" AND {qryExamesRealizados.IdMedico} = " + exameBase.IdMedico.Id);
                }
            }
            #endregion

            #region TipoExame

            public enum TipoExame : int
            {
                Todos = -1,
                Clinico = 0,
                Audiometrico = 1,
                Complementar = 2,
                NaoOcupacional = 3
            }

            private TipoExame _IndFiltroTipoExame;

            public TipoExame IndFiltroTipoExame
            {
                get { return _IndFiltroTipoExame; }
                set { _IndFiltroTipoExame = value; }
            }

            public enum ExameDic : int
            {
                Todos = 0,
                TodosMenosPeriodico = -1
            }

            private int _IdExameDicionario;

            public int IdExameDicionario
            {
                get { return _IdExameDicionario; }
                set { _IdExameDicionario = value; }
            }

            private void AddTipoExame()
            {
                if (IndFiltroTipoExame == TipoExame.Clinico)
                {
                    if (sintax == Sintax.SQL)
                        sql.Append(" AND IndExame=" + (int)IndTipoExame.Clinico);
                    else
                        sql.Append(" AND {qryExamesRealizados.IndExame} = " + (int)IndTipoExame.Clinico);

                    if (IdExameDicionario == (int)ExameDic.Todos)
                        return;

                    if (IdExameDicionario == (int)ExameDic.TodosMenosPeriodico)
                        if (sintax == Sintax.SQL)
                            sql.Append(" AND IdExameDicionario <> " + (int)IndExameClinico.Periodico);
                        else
                            sql.Append(" AND {qryExamesRealizados.IdExameDicionario} <> " + (int)IndExameClinico.Periodico);
                    else
                        if (sintax == Sintax.SQL)
                            sql.Append(" AND IdExameDicionario = " + IdExameDicionario.ToString());
                        else
                            sql.Append(" AND {qryExamesRealizados.IdExameDicionario} = " + IdExameDicionario.ToString());
                }
                else if (IndFiltroTipoExame == TipoExame.Complementar)
                {
                    if (sintax == Sintax.SQL)
                        sql.Append(" AND IndExame=" + (int)IndTipoExame.Complementar);
                    else
                        sql.Append(" AND {qryExamesRealizados.IndExame} =" + (int)IndTipoExame.Complementar);

                    if (IdExameDicionario == (int)ExameDic.Todos)
                        return;

                    if (sintax == Sintax.SQL)
                        sql.Append(" AND IdExameDicionario = " + IdExameDicionario.ToString());
                    else
                        sql.Append(" AND {qryExamesRealizados.IdExameDicionario} = " + IdExameDicionario.ToString());
                }
                else if (IndFiltroTipoExame == TipoExame.Audiometrico)
                {
                    if (sintax == Sintax.SQL)
                        sql.Append(" AND IdExameDicionario = " + (int)Exames.Audiometria);
                    else
                        sql.Append(" AND {qryExamesRealizados.IdExameDicionario} = " + (int)Exames.Audiometria);

                    if (IdExameDicionario == (int)ExameDic.Todos)
                        return;
                }
                else if (IndFiltroTipoExame == TipoExame.NaoOcupacional)
                {
                    if (sintax == Sintax.SQL)
                        sql.Append(" AND IndExame=" + (int)IndTipoExame.NaoOcupacional);
                    else
                        sql.Append(" AND {qryExamesRealizados.IndExame} =" + (int)IndTipoExame.NaoOcupacional);
                }
            }
            #endregion

            #region DataExame

            private DateTime _DataExameDe;

            public DateTime DataExameDe
            {
                get { return _DataExameDe; }
                set { _DataExameDe = value; }
            }
            private DateTime _DataExameAte;

            public DateTime DataExameAte
            {
                get { return _DataExameAte; }
                set { _DataExameAte = value; }
            }

            private void AddDataExame()
            {
                if (sintax == Sintax.SQL)
                {
                    if (DataExameDe != new DateTime() && DataExameAte != new DateTime())
                        sql.Append(" AND DataExame BETWEEN  '" + DataExameDe.ToString("yyyy-MM-dd") + "' AND '" + DataExameAte.ToString("yyyy-MM-dd") + " 23:59:000'");
                    else if (DataExameDe != new DateTime())
                        sql.Append(" AND DataExame >= '" + DataExameDe.ToString("yyyy-MM-dd") + "'");
                    else if (DataExameAte != new DateTime())
                        sql.Append(" AND DataExame <= '" + DataExameAte.ToString("yyyy-MM-dd") + "'");
                }
                else
                {
                    if (DataExameDe != new DateTime())
                        sql.Append(" AND {qryExamesRealizados.DataExame} >="
                                    + " Date (" + DataExameDe.Year
                                    + ", " + DataExameDe.Month
                                    + ", " + DataExameDe.Day + ")");

                    if (DataExameAte != new DateTime())
                        sql.Append(" AND {qryExamesRealizados.DataExame} <="
                                        + " Date (" + DataExameAte.Year
                                        + ", " + DataExameAte.Month
                                        + ", " + DataExameAte.Day + ")");
                }
            }
            #endregion

            #region Pagamento

            public enum Pagamento : int
            {
                Todos,
                EmAberto,
                Pagos
            }

            private Pagamento _IndPagamento;

            public Pagamento IndPagamento
            {
                get { return _IndPagamento; }
                set { _IndPagamento = value; }
            }

            private DateTime _DataPgtoDe;

            public DateTime DataPgtoDe
            {
                get { return _DataPgtoDe; }
                set { _DataPgtoDe = value; }
            }
            private DateTime _DataPgtoAte;

            public DateTime DataPgtoAte
            {
                get { return _DataPgtoAte; }
                set { _DataPgtoAte = value; }
            }

            private void AddPagamento()
            {
                if (sintax == Sintax.SQL)
                {
                    if (IndPagamento == Pagamento.EmAberto)
                        sql.Append(" AND DataPagamento IS NULL");
                    else if (IndPagamento == Pagamento.Pagos)
                    {
                        sql.Append(" AND DataPagamento IS NOT NULL");

                        if (DataPgtoDe != new DateTime() && DataPgtoAte != new DateTime())
                            sql.Append(" AND DataPagamento BETWEEN  '" + DataPgtoDe.ToString("yyyy-MM-dd")
                                                        + "' AND '" + DataPgtoAte.ToString("yyyy-MM-dd") + " 23:59:000'");
                        else if (DataPgtoDe != new DateTime())
                            sql.Append(" AND DataPagamento >= '" + DataPgtoDe.ToString("yyyy-MM-dd") + "'");
                        else if (DataPgtoAte != new DateTime())
                            sql.Append(" AND DataPagamento <= '" + DataPgtoAte.ToString("yyyy-MM-dd") + "'");
                    }
                }
                else
                {
                    if (IndPagamento == Pagamento.EmAberto)
                        sql.Append(" AND ISNULL({qryExamesRealizados.DataPagamento})");
                    else if (IndPagamento == Pagamento.Pagos)
                    {
                        sql.Append(" AND NOT ISNULL({qryExamesRealizados.DataPagamento})");

                        if (DataPgtoDe != new DateTime())
                            sql.Append(" AND {qryExamesRealizados.DataPagamento} >="
                                          + " Date (" + DataPgtoDe.Year
                                          + ", " + DataPgtoDe.Month
                                          + ", " + DataPgtoDe.Day + ")");

                        if (DataPgtoAte != new DateTime())
                            sql.Append(" AND {qryExamesRealizados.DataPagamento} <="
                                        + " Date (" + DataPgtoAte.Year
                                        + ", " + DataPgtoAte.Month
                                        + ", " + DataPgtoAte.Day + ")");
                    }
                }
            }
            #endregion

            #region SaidaDocumento

            public enum SaidaDocumento : int
            {
                Todos,
                NaoEnviados,
                Enviados
            }

            private SaidaDocumento _IndSaidaDocumento;

            public SaidaDocumento IndSaidaDocumento
            {
                get { return _IndSaidaDocumento; }
                set { _IndSaidaDocumento = value; }
            }

            private void AddSaidaDocumento()
            {
                if (sintax == Sintax.SQL)
                {
                    if (IndSaidaDocumento == SaidaDocumento.NaoEnviados)
                        sql.Append(" AND IdExameBase NOT IN (SELECT IdExameBase FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.ExameBaseSaidaDocumento)");
                    else if (IndSaidaDocumento == SaidaDocumento.Enviados)
                        sql.Append(" AND IdExameBase IN (SELECT IdExameBase FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.ExameBaseSaidaDocumento)");
                }
            }
            #endregion

            #region Resultado

            public enum Resultado : int
            {
                Realizados = -2,
                Todos = -1,
                Apto = 1,
                Inapto = 2,
                SemResultado = 0,
                EmEspera = 3,
            }

            private Resultado _IndResultado;

            public Resultado IndResultado
            {
                get { return _IndResultado; }
                set { _IndResultado = value; }
            }

            private void AddResultado()
            {
                if (sintax == Sintax.SQL)
                {
                    if (IndResultado == Resultado.Realizados)
                        sql.Append(" AND IndResultado<>" + (int)ResultadoExame.NaoRealizado);
                    else if (IndResultado != Resultado.Todos)
                        sql.Append(" AND IndResultado=" + (int)IndResultado);
                }
                else
                {
                    if (IndResultado == Resultado.Realizados)
                        sql.Append(" AND {qryExamesRealizados.IndResultado} <> " + (int)ResultadoExame.NaoRealizado);
                    else if (IndResultado != Resultado.Todos)
                        sql.Append(" AND {qryExamesRealizados.IndResultado} = " + (int)IndResultado);
                }
            }
            #endregion

            #region Agendado

            public enum Agendamento : int
            {
                Todos = -1,
                Interno = 0,
                Web = 1
            }

            private Agendamento _IndAgendamento;

            public Agendamento IndAgendamento
            {
                get { return _IndAgendamento; }
                set { _IndAgendamento = value; }
            }

            private void AddAgendado()
            {
                if (IndAgendamento == Agendamento.Todos)
                    return;

                if (sintax == Sintax.SQL)
                {
                    sql.Append(" AND IdCompromisso IS NOT NULL AND IdCompromisso <> 0");
                    sql.Append(" AND IsFromConvocacaoWeb=" + (int)IndAgendamento);
                }
                else
                {
                    sql.Append(" AND NOT ISNULL({qryExamesRealizados.IdCompromisso}) AND {qryExamesRealizados.IdCompromisso} <> 0");
                    sql.Append(" AND {qryExamesRealizados.IsFromConvocacaoWeb} = " + Convert.ToBoolean((int)IndAgendamento));
                }
            }
            #endregion

            #region EncaminhadoEspecialista

            private bool _EncaminhadoEspecialista;

            public bool EncaminhadoEspecialista
            {
                get { return _EncaminhadoEspecialista; }
                set { _EncaminhadoEspecialista = value; }
            }

            private void AddEncaminhadoEspecialista()
            {
                if (!EncaminhadoEspecialista)
                    return;

                if (sintax == Sintax.SQL)
                    sql.Append(" AND IdExameBase IN (SELECT IdExameBase FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.Clinico WHERE EncaminhamentoEspecialista=1)");
                else
                    throw new Exception("Não disponivel para Encaminhados ao Especialista!");
            }
            #endregion

            #region EmpregadosAtivos

            private bool _EmpregadosAtivos;

            public bool EmpregadosAtivos
            {
                get { return _EmpregadosAtivos; }
                set { _EmpregadosAtivos = value; }
            }

            private void AddEmpregadosAtivos()
            {
                if (!EmpregadosAtivos)
                    return;

                if (sintax == Sintax.SQL)
                    sql.Append(" AND IdEmpregado IN (SELECT nID_EMPREGADO FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEMPREGADO WHERE hDT_DEM IS NULL AND gTERCEIRO = 0)");
                else
                    throw new Exception("Não disponivel para Empregados Ativos!");
            }
            #endregion

            #region OrderBy

            private bool _ComOrderBy;

            public bool ComOrderBy
            {
                get { return _ComOrderBy; }
                set { _ComOrderBy = value; }
            }

            private void AddOrderBy()
            {
                if (sintax == Sintax.SQL)
                    sql.Append(" ORDER BY DataExame");
            }
            #endregion
        }

        #endregion
    }

    #endregion


    [Database("opsa", "Questionario", "IdQuestionario", "", "Questionario")]
    public class Questionario : Ilitera.Data.Table
    {
        private int _IdQuestionario;
        private int _IdEmpregado;
        private int _IdClassificacao;
        private DateTime _Data_Questionario;
        private int _IdProgramaSaude;
        private int _IdCID;
        private string _Especialista;
        private string _Tipo_Orientacao;
        private bool _Encaminhar_Cronico;
        private bool _Atualizar_Nao_Critico;
        private bool _Tratamento_Alto_Custo;
        private Int16 _Status;
        private Int16 _Retorno_Dias;
        private string _Obs;
        private int _IdCID2;
        private int _IdCID3;
        private int _IdCID4;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public Questionario()
        {
        }
        public override int Id
        {
            get { return _IdQuestionario; }
            set { _IdQuestionario = value; }
        }
        public int IdEmpregado
        {
            get { return _IdEmpregado; }
            set { _IdEmpregado = value; }
        }
        public int IdClassificacao
        {
            get { return _IdClassificacao; }
            set { _IdClassificacao = value; }
        }
        public DateTime Data_Questionario
        {
            get { return _Data_Questionario; }
            set { _Data_Questionario = value; }
        }
        public int IdProgramaSaude
        {
            get { return _IdProgramaSaude; }
            set { _IdProgramaSaude = value; }
        }
        public int IdCID
        {
            get { return _IdCID; }
            set { _IdCID = value; }
        }
        public String Especialista
        {
            get { return _Especialista; }
            set { _Especialista = value; }
        }
        public String Tipo_Orientacao
        {
            get { return _Tipo_Orientacao; }
            set { _Tipo_Orientacao = value; }
        }
        public bool Encaminhar_Cronico
        {
            get { return _Encaminhar_Cronico; }
            set { _Encaminhar_Cronico = value; }
        }
        public bool Atualizar_Nao_Critico
        {
            get { return _Atualizar_Nao_Critico; }
            set { _Atualizar_Nao_Critico = value; }
        }
        public bool Tratamento_Alto_Custo
        {
            get { return _Tratamento_Alto_Custo; }
            set { _Tratamento_Alto_Custo = value; }
        }
        public Int16 Status
        {
            get { return _Status; }
            set { _Status = value; }
        }
        public Int16 Retorno_Dias
        {
            get { return _Retorno_Dias; }
            set { _Retorno_Dias = value; }
        }
        public String Obs
        {
            get { return _Obs; }
            set { _Obs = value; }
        }
        public int IdCID2
        {
            get { return _IdCID2; }
            set { _IdCID2 = value; }
        }
        public int IdCID3
        {
            get { return _IdCID3; }
            set { _IdCID3 = value; }
        }
        public int IdCID4
        {
            get { return _IdCID4; }
            set { _IdCID4 = value; }
        }



    }



    [Database("opsa", "Toxicologico_Sorteio", "Id_Toxicologico_Sorteio")]
    public class ToxicologicoSorteio : Ilitera.Data.Table
    {

        private int _Id_Toxicologico_Sorteio;
        private int _IdCliente;
        private DateTime _Data_Criacao;
        private string _Codigo_Sorteio;
        private Int16 _Colaboradores_Sorteados;
        private int _IdUsuario;
        private int _IdJuridica;
        private bool _Finalizado;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public ToxicologicoSorteio()
        {

        }

        public override int Id
        {
            get { return _Id_Toxicologico_Sorteio; }
            set { _Id_Toxicologico_Sorteio = value; }
        }
        public int IdCliente
        {
            get { return _IdCliente; }
            set { _IdCliente = value; }
        }
        public DateTime Data_Criacao
        {
            get { return _Data_Criacao; }
            set { _Data_Criacao = value; }
        }
        public string Codigo_Sorteio
        {
            get { return _Codigo_Sorteio; }
            set { _Codigo_Sorteio = value; }
        }
        public Int16 Colaboradores_Sorteados
        {
            get { return _Colaboradores_Sorteados; }
            set { _Colaboradores_Sorteados = value; }
        }
        public int IdUsuario
        {
            get { return _IdUsuario; }
            set { _IdUsuario = value; }
        }
        public int IdJuridica
        {
            get { return _IdJuridica; }
            set { _IdJuridica = value; }
        }
        public bool Finalizado
        {
            get { return _Finalizado; }
            set { _Finalizado = value; }
        }

    }



    [Database("opsa", "Toxicologico_Sorteio_Colaborador", "Id_Toxicologico_Sorteio_Colaborador")]
    public class ToxicologicoSorteio_Colaborador : Ilitera.Data.Table
    {
        private int _Id_Toxicologico_Sorteio_Colaborador;
        private int _Id_Toxicologico_Sorteio;
        private int _IdEmpregado;
        private bool _Sorteado;
        private DateTime _Data_Sorteio;
        private DateTime _Data_Exame;
        private int _IdExameBase;
        private int _IdUsuario_Liberacao_Novo_Sorteio;
        private DateTime _Data_Liberacao_Novo_Sorteio;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public ToxicologicoSorteio_Colaborador()
        {

        }

        public override int Id
        {
            get { return _Id_Toxicologico_Sorteio_Colaborador; }
            set { _Id_Toxicologico_Sorteio_Colaborador = value; }
        }
        public int Id_Toxicologico_Sorteio
        {
            get { return _Id_Toxicologico_Sorteio; }
            set { _Id_Toxicologico_Sorteio = value; }
        }
        public int IdEmpregado
        {
            get { return _IdEmpregado; }
            set { _IdEmpregado = value; }
        }
        public bool Sorteado
        {
            get { return _Sorteado; }
            set { _Sorteado = value; }
        }
        public DateTime Data_Sorteio
        {
            get { return _Data_Sorteio; }
            set { _Data_Sorteio = value; }
        }
        public DateTime Data_Exame
        {
            get { return _Data_Exame; }
            set { _Data_Exame = value; }
        }
        public int IdExameBase
        {
            get { return _IdExameBase; }
            set { _IdExameBase = value; }
        }
        public int IdUsuario_Liberacao_Novo_Sorteio
        {
            get { return _IdUsuario_Liberacao_Novo_Sorteio; }
            set { _IdUsuario_Liberacao_Novo_Sorteio = value; }
        }
        public DateTime Data_Liberacao_Novo_Sorteio
        {
            get { return _Data_Liberacao_Novo_Sorteio; }
            set { _Data_Liberacao_Novo_Sorteio = value; }
        }

    }


}


