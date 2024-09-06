using System;
using System.Data;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

using Ilitera.Data;
using Ilitera.Common;

namespace Ilitera.Opsa.Data
{
    #region Enums

    public enum TipoCipa : int
    {
        NaoContratada,
        Contratada,
        ParcialmenteContratada
    }

    public enum TipoPcmsoContratada : int
    {
        NaoContratada,
        Contratada,
        SomenteSistema
    }

    public enum MedicoPCMSO : int
    {
        Mestra,
        Cliente,
        Clinica
    }

    public enum Clientes : int
    {
        JRA = -1912406176,
        PolenghiSVdeMinas = 1118,
        VendrameSymrise = 320283742,
        VendrameSymriseSorocaba = 1302500795,
        HPEmbalagens = 757
    }
    #endregion

    #region class Cliente

    [Database("opsa", "Cliente", "IdCliente")]
    public class Cliente : Ilitera.Common.Juridica
    {
        #region Properties
        private int _IdCliente;
        private int _CodigoAntigo;
        private Origem _IdOrigem;
        private string _OrigemOutros = string.Empty;
        private Sindicato _IdSindicato;
        private DRT _IdDRT;
        private DateTime _DataRegistroDRT = DateTime.Today;
        private string _NumeroRegistroDRT = string.Empty;
        private string _LocalAtividades = "Sala de Reuniões";
        private string _HoraAtividades = "09:00";
        private int _DiaAtividades;
        private int _QtdAutoclaves;
        private int _QtdCaldeiras;
        private int _QtdVasoPressao;
        private int _QtdEmpilhadeiras;
        private int _QtdPontes;
        private int _QtdTanques;
        private int _QtdPrensas;
        private int _ContrataCipa;
        private int _ContrataPCMSO;
        private int _QtdPericiaContratadaAno;
        private float _ValorPericiaRealizadaParte;
        private ContrataOrdemServico _ContrataOS;
        private DateTime _PosseCipa = DateTime.Today;
        private bool _RecargaExtintor;
        private bool _HasObraCivil;
        private bool _IsMineradora;
        //private string _FotoDiretorioPadrao = string.Empty;
        private string _ArqFotoEmpregInicio = "MVC-";
        private string _ArqFotoEmpregTermino = "S";
        private string _ArqFotoEmpregExtensao = ".JPG";
        private int _ArqFotoEmrpegQteDigitos = 5;
        private Prestador _IdRespEleicaoCIPA;
        private Prestador _IdRespPCMSO;
        private Prestador _IdRespPPP;
        private Prestador _IdRespPPP2;
        //private Prestador _IdRespLegal;
        //private Prestador _IdRespLTCAT;
        //private Prestador _IdRespNR564;
        private double _Turnover;
        private bool _HasExameComplementar;
        private string _ObservacaoPcmso = string.Empty;
        private DateTime _DataRetiradaProntuario;
        private DateTime _DataUltimoPeriodico;
        private RealizacaoPeriodico _IndRealizacaoPeriodico;
        private AtualizacaoPeriodico _IndAtualizacaoPeriodico;
        private bool _Permitir_Colaboradores;
        private bool _Exibir_Riscos_ASO;
        private bool _Exibir_Datas_Exames_ASO;
        private bool _Bloquear_Novo_Setor;
        private bool _Permitir_Edicao_Nome;
        private bool _CNPJ_Matriz;
        private bool _Bloquear_Data_Demissao;
        private bool _Bloquear_ASO_Planejamento;
        private bool _Endereco_Matriz;
        private string _Municipio_Data_ASO;
        private int _Carga_Horaria;
        private bool _Logo_Laudos;
        private bool _InibirGHE;
        private bool _GHEAnterior_MudancaFuncao;
        private bool _Microbiologia;
        private bool _Riscos_PPRA;

        private bool _Ativar_DesconsiderarCompl;
        private Int16 _Dias_Desconsiderar;

        private bool _FatorRH;
        private bool _Gerar_Complementares_Guia;
        private bool _PPP_Checar_Entrega_EPI;

        private string _Mail_Alerta_Absenteismo;

        private bool _PPP_CNPJ_Nome_Matriz;

        private string _Turnos = string.Empty;

        private Cipa m_GestaoAtual;
        private Cipa m_ProximaGestao;

        private bool _InibirDadosMatriz_Laudos;

        private bool _Bloquear_Data_Demissionais;
        private string _ESocial_Ambiente;

        private bool _Bloquear_2210;
        private bool _Bloquear_2220;
        private bool _Bloquear_2240;
        private bool _Envio_2220_Sem_Resultado;
        private bool _Envio_2240_Demitidos;

        private short _ESocial_Grupo;

        private bool _ESocial_Download_Cabecalho;

        private bool _Bloquear_Avaliacao_Clinica;

        private string _Contrato_Numero;
        private Int16 _ESocial_Dias_Apos_Admissao;

        private bool _Bloquear_Novo_Cargo;

        private bool _Bloquear_Dispensado;

        private bool _PGR_Personalizado;

        private bool _Demissional_Audiometria;

        private bool _Exames_Faltantes_Bloquear_ASO;

        private string _Alerta_web_Inserir_Colaborador;
        private string _Alerta_web_Alocar_Colaborador;
        private string _Alerta_web_Agendamento;

        private bool _PGR_Apenas_Funcoes_Ativas;

        private string _Alerta_Vacina_Email;
        private Int16 _Alerta_Vacina_Dias;

        private bool _Liberar_Grafico;

        private bool _Aptidoes_Demissional;
        private bool _Aptidoes_Retorno;

        private bool _PGR_Inserir_Vigencia;


        private bool _Desconsiderar_Afastados_Planejamento;


        public Cliente()
        {

        }

        public Cliente(int Id)
        {
            this.Find(Id);
        }

        public override int Id
        {
            get { return _IdCliente; }
            set { _IdCliente = value; }
        }
        public int CodigoAntigo
        {
            get { return _CodigoAntigo; }
            set { _CodigoAntigo = value; }
        }
        public Origem IdOrigem
        {
            get { return _IdOrigem; }
            set { _IdOrigem = value; }
        }
        public string OrigemOutros
        {
            get { return _OrigemOutros; }
            set { _OrigemOutros = value; }
        }
        public Sindicato IdSindicato
        {
            get { return _IdSindicato; }
            set { _IdSindicato = value; }
        }
        public DRT IdDRT
        {
            get { return _IdDRT; }
            set { _IdDRT = value; }
        }
        public DateTime DataRegistroDRT
        {
            get { return _DataRegistroDRT; }
            set { _DataRegistroDRT = value; }
        }
        public string NumeroRegistroDRT
        {
            get { return _NumeroRegistroDRT; }
            set { _NumeroRegistroDRT = value; }
        }
        public string LocalAtividades
        {
            get { return _LocalAtividades; }
            set { _LocalAtividades = value; }
        }
        public string HoraAtividades
        {
            get { return _HoraAtividades; }
            set { _HoraAtividades = value; }
        }
        public int DiaAtividades
        {
            get { return _DiaAtividades; }
            set { _DiaAtividades = value; }
        }
        public int QtdAutoclaves
        {
            get { return _QtdAutoclaves; }
            set { _QtdAutoclaves = value; }
        }
        public int QtdCaldeiras
        {
            get { return _QtdCaldeiras; }
            set { _QtdCaldeiras = value; }
        }
        public int QtdVasoPressao
        {
            get { return _QtdVasoPressao; }
            set { _QtdVasoPressao = value; }
        }
        public int QtdEmpilhadeiras
        {
            get { return _QtdEmpilhadeiras; }
            set { _QtdEmpilhadeiras = value; }
        }
        public int QtdPontes
        {
            get { return _QtdPontes; }
            set { _QtdPontes = value; }
        }
        public int QtdPrensas
        {
            get { return _QtdPrensas; }
            set { _QtdPrensas = value; }
        }
        public int QtdTanques
        {
            get { return _QtdTanques; }
            set { _QtdTanques = value; }
        }
        public int QtdPericiaContratadaAno
        {
            get { return _QtdPericiaContratadaAno; }
            set { _QtdPericiaContratadaAno = value; }
        }
        public float ValorPericiaRealizadaParte
        {
            get { return _ValorPericiaRealizadaParte; }
            set { _ValorPericiaRealizadaParte = value; }
        }
        public int ContrataCipa
        {
            get { return _ContrataCipa; }
            set { _ContrataCipa = value; }
        }
        public int ContrataPCMSO
        {
            get { return _ContrataPCMSO; }
            set { _ContrataPCMSO = value; }
        }
        public ContrataOrdemServico ContrataOS
        {
            get { return _ContrataOS; }
            set { _ContrataOS = value; }
        }
        public DateTime PosseCipa
        {
            get { return _PosseCipa; }
            set { _PosseCipa = value; }
        }
        public bool RecargaExtintor
        {
            get { return _RecargaExtintor; }
            set { _RecargaExtintor = value; }
        }
        public bool HasObraCivil
        {
            get { return _HasObraCivil; }
            set { _HasObraCivil = value; }
        }
        public bool IsMineradora
        {
            get { return _IsMineradora; }
            set { _IsMineradora = value; }
        }
        //public string FotoDiretorioPadrao
        //{
        //    get { return _FotoDiretorioPadrao; }
        //    set { _FotoDiretorioPadrao = value; }
        //}
        public string ArqFotoEmpregInicio
        {
            get { return _ArqFotoEmpregInicio; }
            set { _ArqFotoEmpregInicio = value; }
        }
        public string ArqFotoEmpregTermino
        {
            get { return _ArqFotoEmpregTermino; }
            set { _ArqFotoEmpregTermino = value; }
        }
        public string ArqFotoEmpregExtensao
        {
            get { return _ArqFotoEmpregExtensao; }
            set { _ArqFotoEmpregExtensao = value; }
        }
        public int ArqFotoEmrpegQteDigitos
        {
            get { return _ArqFotoEmrpegQteDigitos; }
            set { _ArqFotoEmrpegQteDigitos = value; }
        }
        public Prestador IdRespEleicaoCIPA
        {
            get { return _IdRespEleicaoCIPA; }
            set { _IdRespEleicaoCIPA = value; }
        }
        //public Prestador IdRespLegal
        //{
        //    get { return _IdRespLegal; }
        //    set { _IdRespLegal = value; }
        //}
        //public Prestador IdRespLTCAT
        //{
        //    get { return _IdRespLTCAT; }
        //    set { _IdRespLTCAT = value; }
        //}
        //public Prestador IdRespNR564
        //{
        //    get { return _IdRespNR564; }
        //    set { _IdRespNR564 = value; }
        //}
        public Prestador IdRespPCMSO
        {
            get { return _IdRespPCMSO; }
            set { _IdRespPCMSO = value; }
        }
        public Prestador IdRespPPP
        {
            get { return _IdRespPPP; }
            set { _IdRespPPP = value; }
        }
        public Prestador IdRespPPP2
        {
            get { return _IdRespPPP2; }
            set { _IdRespPPP2 = value; }
        }
        public double Turnover
        {
            get { return _Turnover; }
            set { _Turnover = value; }
        }
        public bool HasExameComplementar
        {
            get { return _HasExameComplementar; }
            set { _HasExameComplementar = value; }
        }
        public DateTime DataUltimoPeriodico
        {
            get { return _DataUltimoPeriodico; }
            set { _DataUltimoPeriodico = value; }
        }
        public DateTime DataRetiradaProntuario
        {
            get { return _DataRetiradaProntuario; }
            set { _DataRetiradaProntuario = value; }
        }
        public string ObservacaoPcmso
        {
            get { return _ObservacaoPcmso; }
            set { _ObservacaoPcmso = value; }
        }
        public RealizacaoPeriodico IndRealizacaoPeriodico
        {
            get { return _IndRealizacaoPeriodico; }
            set { _IndRealizacaoPeriodico = value; }
        }

        public AtualizacaoPeriodico IndAtualizacaoPeriodico
        {
            get { return _IndAtualizacaoPeriodico; }
            set { _IndAtualizacaoPeriodico = value; }
        }
        public string Turnos
        {
            get { return _Turnos; }
            set { _Turnos = value; }
        }

        public bool Permitir_Colaboradores
        {
            get { return _Permitir_Colaboradores; }
            set { _Permitir_Colaboradores = value; }
        }

        public bool Exibir_Riscos_ASO
        {
            get { return _Exibir_Riscos_ASO; }
            set { _Exibir_Riscos_ASO = value; }
        }

        public bool Exibir_Datas_Exames_ASO
        {
            get { return _Exibir_Datas_Exames_ASO; }
            set { _Exibir_Datas_Exames_ASO = value; }
        }

        public bool Bloquear_Novo_Setor
        {
            get { return _Bloquear_Novo_Setor; }
            set { _Bloquear_Novo_Setor = value; }
        }

        public bool Permitir_Edicao_Nome
        {
            get { return _Permitir_Edicao_Nome; }
            set { _Permitir_Edicao_Nome = value; }
        }


        public bool Bloquear_Data_Demissao
        {
            get { return _Bloquear_Data_Demissao; }
            set { _Bloquear_Data_Demissao = value; }
        }

        public bool CNPJ_Matriz
        {
            get { return _CNPJ_Matriz; }
            set { _CNPJ_Matriz = value; }
        }

        public bool Endereco_Matriz
        {
            get { return _Endereco_Matriz; }
            set { _Endereco_Matriz = value; }
        }

        public bool Bloquear_ASO_Planejamento
        {
            get { return _Bloquear_ASO_Planejamento; }
            set { _Bloquear_ASO_Planejamento = value; }
        }

        public string Municipio_Data_ASO
        {
            get { return _Municipio_Data_ASO; }
            set { _Municipio_Data_ASO = value; }
        }

        public int Carga_Horaria
        {
            get { return _Carga_Horaria; }
            set { _Carga_Horaria = value; }
        }
        
        public bool Logo_Laudos
        {
            get { return _Logo_Laudos; }
            set { _Logo_Laudos = value; }
        }

        public bool InibirGHE
        {
            get { return _InibirGHE; }
            set { _InibirGHE = value; }
        }

        public bool GHEAnterior_MudancaFuncao
        {
            get { return _GHEAnterior_MudancaFuncao; }
            set { _GHEAnterior_MudancaFuncao = value; }
        }

        public bool Microbiologia
        {
            get { return _Microbiologia; }
            set { _Microbiologia = value; }
        }

        public bool Riscos_PPRA
        {
            get { return _Riscos_PPRA; }
            set { _Riscos_PPRA = value; }
        }

        public bool Ativar_DesconsiderarCompl
        {
            get { return _Ativar_DesconsiderarCompl; }
            set { _Ativar_DesconsiderarCompl = value; }
        }

        public Int16 Dias_Desconsiderar
        {
            get { return _Dias_Desconsiderar; }
            set { _Dias_Desconsiderar = value; }
        }

        public bool FatorRH
        {
            get { return _FatorRH; }
            set { _FatorRH = value; }
        }

        public bool Gerar_Complementares_Guia
        {
            get { return _Gerar_Complementares_Guia; }
            set { _Gerar_Complementares_Guia = value; }
        }

        public bool PPP_Checar_Entrega_EPI
        {
            get { return _PPP_Checar_Entrega_EPI; }
            set { _PPP_Checar_Entrega_EPI = value; }
        }


        public string Mail_Alerta_Absenteismo
        {
            get { return _Mail_Alerta_Absenteismo; }
            set { _Mail_Alerta_Absenteismo = value; }
        }

        public bool PPP_CNPJ_Nome_Matriz
        {
            get { return _PPP_CNPJ_Nome_Matriz; }
            set { _PPP_CNPJ_Nome_Matriz = value; }
        }

        public bool InibirDadosMatriz_Laudos
        {
            get { return _InibirDadosMatriz_Laudos; }
            set { _InibirDadosMatriz_Laudos = value; }
        }



        public bool Bloquear_Data_Demissionais
        {
            get { return _Bloquear_Data_Demissionais; }
            set { _Bloquear_Data_Demissionais = value; }
        }

        public string ESocial_Ambiente
        {
            get { return _ESocial_Ambiente; }
            set { _ESocial_Ambiente = value; }
        }

        public bool Bloquear_2210
        {
            get { return _Bloquear_2210; }
            set { _Bloquear_2210 = value; }
        }

        public bool Bloquear_2220
        {
            get { return _Bloquear_2220; }
            set { _Bloquear_2220 = value; }
        }

        public bool Bloquear_2240
        {
            get { return _Bloquear_2240; }
            set { _Bloquear_2240 = value; }
        }

        public bool Envio_2220_Sem_Resultado
        {
            get { return _Envio_2220_Sem_Resultado; }
            set { _Envio_2220_Sem_Resultado = value; }
        }

        public bool Envio_2240_Demitidos
        {
            get { return _Envio_2240_Demitidos; }
            set { _Envio_2240_Demitidos = value; }
        }


        public short ESocial_Grupo
        {
            get { return _ESocial_Grupo; }
            set { _ESocial_Grupo = value; }
        }

        public bool ESocial_Download_Cabecalho
        {
            get { return _ESocial_Download_Cabecalho; }
            set { _ESocial_Download_Cabecalho = value; }
        }

        public bool Bloquear_Avaliacao_Clinica
        {
            get { return _Bloquear_Avaliacao_Clinica; }
            set { _Bloquear_Avaliacao_Clinica = value; }
        }

        public string Contrato_Numero
        {
            get { return _Contrato_Numero; }
            set { _Contrato_Numero = value; }
        }

        public Int16 ESocial_Dias_Apos_Admissao
        {
            get { return _ESocial_Dias_Apos_Admissao; }
            set { _ESocial_Dias_Apos_Admissao = value; }
        }

        public bool Bloquear_Novo_Cargo
        {
            get { return _Bloquear_Novo_Cargo; }
            set { _Bloquear_Novo_Cargo = value; }
        }

        public bool Bloquear_Dispensado
        {
            get { return _Bloquear_Dispensado; }
            set { _Bloquear_Dispensado = value; }
        }

        public bool PGR_Personalizado
        {
            get { return _PGR_Personalizado; }
            set { _PGR_Personalizado = value; }
        }

        public bool Demissional_Audiometria
        {
            get { return _Demissional_Audiometria; }
            set { _Demissional_Audiometria = value; }
        }

        public bool Exames_Faltantes_Bloquear_ASO
        {
            get { return _Exames_Faltantes_Bloquear_ASO; }
            set { _Exames_Faltantes_Bloquear_ASO = value; }
        }


        public string Alerta_web_Inserir_Colaborador
        {
            get { return _Alerta_web_Inserir_Colaborador; }
            set { _Alerta_web_Inserir_Colaborador = value; }
        }

        public string Alerta_web_Alocar_Colaborador
        {
            get { return _Alerta_web_Alocar_Colaborador; }
            set { _Alerta_web_Alocar_Colaborador = value; }
        }

        public string Alerta_web_Agendamento
        {
            get { return _Alerta_web_Agendamento; }
            set { _Alerta_web_Agendamento = value; }
        }

        public bool PGR_Apenas_Funcoes_Ativas
        {
            get { return _PGR_Apenas_Funcoes_Ativas; }
            set { _PGR_Apenas_Funcoes_Ativas = value; }
        }

        public string Alerta_Vacina_Email
        {
            get { return _Alerta_Vacina_Email; }
            set { _Alerta_Vacina_Email = value; }
        }


        public Int16 Alerta_Vacina_Dias
        {
            get { return _Alerta_Vacina_Dias; }
            set { _Alerta_Vacina_Dias = value; }
        }

        public bool Liberar_Grafico
        {
            get { return _Liberar_Grafico; }
            set { _Liberar_Grafico = value; }
        }

        public bool Aptidoes_Demissional
        {
            get { return _Aptidoes_Demissional; }
            set { _Aptidoes_Demissional = value; }
        }

        public bool Aptidoes_Retorno
        {
            get { return _Aptidoes_Retorno; }
            set { _Aptidoes_Retorno = value; }
        }


        public bool Desconsiderar_Afastados_Planejamento
        {
            get { return _Desconsiderar_Afastados_Planejamento; }
            set { _Desconsiderar_Afastados_Planejamento = value; }
        }

        public bool PGR_Inserir_Vigencia
        {
            get { return _PGR_Inserir_Vigencia; }
            set { _PGR_Inserir_Vigencia = value; }
        }


        #endregion

        #region Enums

        public enum RealizacaoPeriodico : int
        {
            Empresa,
            Mestra,
            Clinica
        }

        public enum AtualizacaoPeriodico : int
        {
            Anual,
            PorEmpregado
        }

        public enum ContrataOrdemServico : int
        {
            NaoContratada,
            Simplificada,
            Completa
        }

        #endregion

        #region Metodos

        #region Override

        public override string ToString()
        {
            if (this.mirrorOld == null)
                this.Find();

            return this.NomeAbreviado;
        }

        public override void Validate()
        {
            if (this.ArqFotoEmrpegQteDigitos > 6)
                throw new Exception("A quantidade de digitos não pode ser maior que 6!");

            if (this.CodigoAntigo != 0)
            {
                StringBuilder where = new StringBuilder();
                where.Append("CodigoAntigo=" + this.CodigoAntigo);

                if (this.Id != 0)
                    where.Append(" AND IdCliente<>" + this.Id);

                List<Cliente> clientes = new Cliente().Find<Cliente>(where.ToString());

                if (clientes.Count > 0)
                    throw new Exception(String.Format("O código do pdtdos já está sendo usado para o cliente: {0}",
                                                       clientes[0].NomeAbreviado));
            }

            base.Validate();
        }

        public override int Save()
        {
            ConcluirPedidoPendenteRetiradaProntuario();

            return base.Save();
        }


        //public int Save(ref CNAE zCNAE, ref GrupoEmpresa zGrupoEmpresa, ref JuridicaPapel zJuridicaPapel)
        //{
        //    ConcluirPedidoPendenteRetiradaProntuario();

        //    this.IdCNAE   = zCNAE;
        //    this.IdGrupoEmpresa = zGrupoEmpresa;
        //    this.IdJuridicaPapel = zJuridicaPapel;

        //    this.IdCNAE.Find();
        //    this.IdGrupoEmpresa.Find();
        //    this.IdJuridicaPapel.Find();
               

        //    return base.Save();
        //}

        #endregion

        #region Quantidades

        public int GetEmpregadosAtivos()
        {
            int ret = 0;

            try
            {
                if (this.IdJuridicaPai == null)
                    this.Find();

                string strCmd = "USE " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + " SELECT dbo.fn_QtdEmpregados ("
                                + this.Id
                                + ", " + Convert.ToInt32(this.AtivarLocalDeTrabalho)
                                + ")";

                ret = Convert.ToInt32(new Juridica().ExecuteScalar(strCmd));
            }
            catch
            {
                ret = 0;
            }
            return ret;
        }

        public int GetNumeroLocaisDeTrabalho()
        {
            int ret = 0;

            try
            {
                string strCmd = "USE " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + " EXEC sps_NumeroLocaisDeTrabalho " + this.Id;

                ret = Convert.ToInt32(new Juridica().ExecuteScalar(strCmd));
            }
            catch
            {
                ret = 0;
            }
            //+ 1 referente a própria unidade pai
            return ret + 1;
        }


        public float GetMediaAnualEmpregados(int ano, bool Alocados)
        {
            int MediaAnualEmpregCalc = 0;

            for (int i = 2; i <= 13; i++)
            {
                DateTime data;

                if (i == 13)
                    data = new DateTime(ano + 1, 01, 01);
                else
                    data = new DateTime(ano, i, 01);

                StringBuilder str = new StringBuilder();

                if (Alocados)
                    str.Append("nID_EMPREGADO IN (SELECT nID_EMPREGADO FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEMPREGADO_FUNCAO WHERE nID_EMPR = " + this.Id + ")");
                else
                    str.Append("nID_EMPR=" + this.Id);

                str.Append(" AND hDT_ADM <'" + data.ToString("yyyy-MM-dd") + "'");
                str.Append(" AND (hDT_DEM IS NULL OR hDT_DEM >= '" + data.ToString("yyyy-MM-dd") + "')");
                str.Append(" AND gTERCEIRO=0");

                int NumEmpregados = new Empregado().ExecuteCount(str.ToString());

                MediaAnualEmpregCalc += NumEmpregados;
            }

            return Convert.ToSingle(MediaAnualEmpregCalc) / 12F;
        }

        public int GetQuantidadeExamesAdmDemAnual()
        {
            string criteria = "DataExame BETWEEN '" + DateTime.Today.AddYears(-1).ToString("yyyy-MM-dd") + " 00:00:00.000'"
                    + " AND '" + DateTime.Today.ToString("yyyy-MM-dd") + " 23:59:59.999'"
                    + " AND IndResultado<>0"
                    + " AND IdExameDicionario IN (" + (int)IndExameClinico.Admissional
                    + "," + (int)IndExameClinico.Demissional + ")"
                    + " AND IdEmpregado IN (SELECT nID_EMPREGADO FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEMPREGADO WHERE nID_EMPR=" + this.Id + ")";

            int ret = new ExameBase().ExecuteCount(criteria);

            return ret;
        }

        #endregion

        #region IsCobrarExames

        public bool IsCobrarExames()
        {
            return  this.Id == (int)Clientes.HPEmbalagens;
        }

        #endregion

        #region LocalRealizacaoPeriodico

        public string GetLocalRealizacaoPeriodico()
        {
            return GetLocalRealizacaoPeriodico(this.IndRealizacaoPeriodico);
        }

        public static string GetLocalRealizacaoPeriodico(RealizacaoPeriodico local)
        {
            string ret;

            if (local == RealizacaoPeriodico.Clinica)
                ret = "Em Clínica";
            else if (local == RealizacaoPeriodico.Empresa)
                ret = "Na Empresa";
            else if (local == RealizacaoPeriodico.Mestra)
                ret = "Na Ilitera";
            else
                ret = "-";

            return ret;
        }

        #endregion

        #region FormaAtualizacaoPeriodico

        public string GetFormaAtualizacaoPeriodico()
        {
            return GetFormaAtualizacaoPeriodico(this.IndAtualizacaoPeriodico);
        }

        public static string GetFormaAtualizacaoPeriodico(AtualizacaoPeriodico forma)
        {
            string ret;

            if (forma == AtualizacaoPeriodico.Anual)
                ret = "Anual";
            else if (forma == AtualizacaoPeriodico.PorEmpregado)
                ret = "Por Empregado (Vencimento Exame)";
            else
                ret = "-";

            return ret;
        }

        #endregion

        #region GetClinicasCredenciadas

        public string GetClinicasCredenciadasParaPeriodicos()
        {
            List<Clinica> clinicas = GetListClinicasCredenciadasParaPeriodicos();

            StringBuilder ret = new StringBuilder();

            if (clinicas.Count > 10)
                ret.Append(clinicas.Count + ") Clínicas Credenciadas");
            else
            {
                foreach (Clinica clinica in clinicas)
                    ret.Append(clinica.NomeAbreviado + "; ");

                if (ret.Length > 0)
                    ret.Remove(ret.ToString().Length - 2, 2);
            }

            return ret.ToString();
        }

        public List<Clinica> GetListClinicasCredenciadasParaPeriodicos()
        {
            string criteria = "IsInativo=0"
              + " AND IdJuridicaPapel=" + (int)IndJuridicaPapel.Clinica
              + " AND IdClinica <>" + (int)Empresas.MestraPaulista
              + " AND IsClinicaInterna = 0"
              + " AND IdClinica IN (SELECT IdClinica "
                                    + "FROM ClinicaExameDicionario"
                                    + " WHERE IdExameDicionario=" + (int)IndExameClinico.Periodico
                                    + " AND IdClinicaExameDicionario IN (SELECT IdClinicaExameDicionario"
                                                                        + " FROM dbo.ClinicaClienteExameDicionario"
                                                                        + " WHERE IsAutorizado=1)"
                                    + ")"
              + " AND IdClinica IN (SELECT IdClinica"
                                + " FROM ClinicaCliente"
                                + " WHERE IdCliente=" + this.Id + ")"
              + " ORDER BY NomeAbreviado";

            List<Clinica> clinicas = new Clinica().Find<Clinica>(criteria);

            return clinicas;
        }
        #endregion

        #region GetCustoExamePeriodico

        public decimal GetCustoExamePeriodico()
        {
            decimal ret;

            if (this.IndRealizacaoPeriodico == RealizacaoPeriodico.Clinica)
            {
                List<Clinica> clinicas = GetListClinicasCredenciadasParaPeriodicos();

                if (clinicas.Count == 0)
                    return 0.0M;

                ClinicaExameDicionario clinicaPeriodico = new ClinicaExameDicionario();
                clinicaPeriodico.Find("IdClinica=" + clinicas[0].Id
                                     + " AND IdExameDicionario=" + (int)IndExameClinico.Periodico);

                ret = Convert.ToDecimal(clinicaPeriodico.ValorPadrao);
            }
            else
                ret = 18.0M;

            return ret;
        }

        #endregion

        #region Caldeira e Vasos

        public bool hasCaldeiras()
        {
            int count = new Caldeira().ExecuteCount("IdCliente=" + this.Id + " AND IsInativo=0");

            return (count > 0);
        }

        public bool hasVasos()
        {
            int count = new VasoPressao().ExecuteCount("IdCliente=" + this.Id + " AND IsInativo=0");

            return (count > 0);
        }

        #endregion

        #region Pcmso

        #region GetUltimoPcmso

        public Pcmso GetUltimoPcmso()
        {
            Pcmso pcmso = new Pcmso();

            string where = "SELECT IdPcmso"
                        + " FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.qryUltimoPcmso"
                        + " WHERE IdCliente =" + this.Id;

            DataSet ds = new Pcmso().ExecuteDataset(where);

            if (ds.Tables[0].Rows.Count != 0)
                pcmso.Find(Convert.ToInt32(ds.Tables[0].Rows[0][0]));

            return pcmso;
        }
        #endregion

        #region GetCoordenadorPadraoPCMSO

        public Medico GetCoordenadorPadraoPCMSO()
        {
            if (this.mirrorOld == null)
                this.Find();

            Medico medico = new Medico();

            if (this.IdRespPCMSO.Id != 0)
                medico.Find(this.IdRespPCMSO.Id);

            if (medico.Id == 0)
            {
                if (this.ContrataPCMSO == (int)TipoPcmsoContratada.SomenteSistema)
                {
                    Pcmso pcmso = GetUltimoPcmso();

                    if (pcmso.Id != 0 && pcmso.IdCoordenador.Id != 0)
                        medico.Find(pcmso.IdCoordenador.Id);
                }
                else if (this.ContrataPCMSO == (int)TipoPcmsoContratada.Contratada)
                {
                    if (this.ContrataPCMSO == (int)TipoPcmsoContratada.Contratada)
                        medico.Find((int)Medicos.DraRosana);
                }
            }

            return medico;
        }
        #endregion

        #region GetDataUltimoPeriodico

        public DateTime GetDataUltimoPeriodico()
        {
            DateTime ultimoExame;

            Pedido ultimoPedido = Pedido.GetUltimoPedido((int)Obrigacoes.ExamesPeriodicos, this.Id);

            ConvocacaoExame convocacao = new ConvocacaoExame();

            if (ultimoPedido.Id != 0)
                convocacao.Find("IdPedido=" + ultimoPedido.Id);

            if (convocacao.Id != 0)
                ultimoExame = convocacao.DataConvocacao;
            else if (ultimoPedido.Id != 0)
                ultimoExame = ultimoPedido.DataConclusao;
            else
                ultimoExame = this.DataUltimoPeriodico;

            return new DateTime( ultimoExame.Year,
                                 ultimoExame.Month,
                                 ultimoExame.Day);
        }
        #endregion

        #region GetDataVencimentoPeriodico

        public DateTime GetDataVencimentoPeriodico()
        {
            Pedido ultimoPedido = Pedido.GetUltimoPedido((int)Obrigacoes.ExamesPeriodicos,
                                                        this.Id);

            return GetDataVencimentoPeriodico(ultimoPedido);
        }

        public DateTime GetDataVencimentoPeriodico(Pedido ultimoPedido)
        {
            DateTime dtSugestao = new DateTime();

            if (ultimoPedido.Id != 0)
            {
                dtSugestao = ultimoPedido.DataConclusao.AddYears(1);
            }
            else if (this.DataUltimoPeriodico != new DateTime())
            {
                dtSugestao = this.DataUltimoPeriodico.AddYears(1);
            }
            else
            {
                Pcmso pcmso = this.GetUltimoPcmso();

                if (pcmso.Id != 0)
                    dtSugestao = pcmso.DataPcmso.AddMonths(2);
            }

            return new DateTime(dtSugestao.Year, dtSugestao.Month, dtSugestao.Day);
        }

        #endregion

        #region ConcluirPedidoPendenteRetiradoProntuario

        public static void ConcluirPedidoPendenteRetiradoProntuario()
        {
            List<Cliente> clientes = new Cliente().Find<Cliente>("IdCliente IN (SELECT IdCliente FROM qryClienteAtivos)"
                                                + " AND ContrataPCMSO <>" + (int)TipoPcmsoContratada.NaoContratada
                                                + " ORDER BY NomeAbreviado");

            foreach (Cliente cliente in clientes)
            {
                //System.Diagnostics.Debug.WriteLine(cliente.NomeAbreviado);

                cliente.ConcluirPedidoPendenteRetiradaProntuario();
            }
        }
        #endregion

        #region ConcluirPedidoPendenteRetiradaProntuario

        protected void ConcluirPedidoPendenteRetiradaProntuario()
        {
            if (this.mirrorOld == null)
                return;

            //Concluir o pedido pendente de retirada de prontuário
            if (this.ContrataPCMSO == (int)TipoPcmsoContratada.Contratada
                && this.DataRetiradaProntuario != new DateTime()
                && this.DataRetiradaProntuario != ((Cliente)this.mirrorOld).DataRetiradaProntuario)
            {
                Pedido pedido = Pedido.GetPedidoPendente((int)Obrigacoes.PcmsoRetiradaProntuario, this.Id);
                if (pedido.Id != 0)
                {
                    pedido.DataConclusao = this.DataRetiradaProntuario;
                    pedido.Save();
                }
            }
        }
        #endregion

        #endregion

        #region GetUltimoDocumento

        public Documento GetUltimoDocumento(Documentos docs)
        {
            Documento documento = new Documento();

            string where = "IdDocumentoBase=" + (int)docs + " AND IdCliente =" + this.Id;

            ArrayList list = new Documento().FindMax("DataLevantamento", where);

            if (list.Count != 0)
                documento.Find(((Documento)list[0]).Id);

            return documento;
        }

        #endregion

        #region Cipa

        public Cipa GetGestaoAtual()
        {
            Documento documento = new Documento();
            documento.FindMax("DataLevantamento", "IdCliente=" + this.Id
                + " AND IdDocumentoBase=" + (int)Documentos.CIPA
                + " AND IdDocumento IN (SELECT IdDocumento FROM Cipa WHERE IsProxima = 0)");

            if (documento.Id == 0)
                documento.FindMax("DataLevantamento", "IdCliente=" + this.Id
                                + " AND IdDocumentoBase=" + (int)Documentos.CIPA);

            m_GestaoAtual = new Cipa();
            m_GestaoAtual.Find(documento.Id);

            if (m_GestaoAtual.Id == 0)
                m_GestaoAtual.Inicialize();

            return m_GestaoAtual;
        }

        public Cipa GetProximaGestao()
        {
            int count = new Cipa().ExecuteCount("IdCliente=" + this.Id + " AND IsProxima = 0");

            m_ProximaGestao = new Cipa();

            if (count != 0)
            {
                m_ProximaGestao.Find("IdCliente=" + this.Id + " AND IsProxima = 1");

                if (m_ProximaGestao.Id == 0)
                    m_ProximaGestao.Inicialize();
            }

            return m_ProximaGestao;
        }

        public void CriarNovaGestaoCipa(Pedido pedido)
        {
            Cipa cipaAtual = GetGestaoAtual();
            Cipa cipaProxima = GetProximaGestao();

            if (cipaAtual.Id != 0 && cipaProxima.Id != 0)
            {
                if (cipaProxima.Posse != new DateTime())
                {
                    EventoCipa eventoCipa = new EventoCipa();
                    eventoCipa.Find("IdCipa=" + cipaProxima.Id
                        + " AND " + "IdEventoBaseCipa=" + (int)EventoBase.Posse);

                    if (eventoCipa.DataConclusao == new DateTime())
                        throw new Exception("A posse não foi concluída! Conclua a posse e tente novamente.");
                }
            }
            Cipa novaCipa = new Cipa();
            novaCipa.Inicialize();
            novaCipa.IdCliente = this;
            novaCipa.IdPedido.Id = pedido.Id;
            novaCipa.IdPrestador.Id = pedido.IdPrestador.Id;
            novaCipa.IdDocumentoBase.Id = (int)Documentos.CIPA;
            novaCipa.DataLevantamento = pedido.DataSolicitacao;
            novaCipa.SetCalendario();
            novaCipa.Save();

            //Já concluí o pedido
            pedido.DataConclusao = cipaProxima.Posse;
            pedido.Save();
        }

        public ArrayList GetUltimas3Cipa()
        {
            ArrayList listCipa = new Cipa().Find("IdCliente=" + this.Id
                                                + " AND IsProxima = 0"
                                                + " AND DataLevantamento IS NOT NULL"
                                                + " ORDER BY DataLevantamento Desc");

            if (listCipa.Count > 3)
                listCipa.RemoveRange(3, listCipa.Count - 3);

            return listCipa;
        }

        public bool VisualizaCipa()
        {
            return this.ContrataCipa != (int)TipoCipa.NaoContratada && this.HasCipa();
        }

        public DateTime GetDataPosse()
        {
            DateTime ret = new DateTime();

            if (this.HasCipa() && this.ContrataCipa == (int)TipoCipa.Contratada)
                ret = this.GetGestaoAtual().Posse;

            return ret;
        }

        #endregion

        #region Valores

        public string GetSalarioMinimo()
        {
            string ret = string.Empty;

            float val = 0F;

            string criteria = "IdCliente=" + this.Id
                + " AND IdServico IN (SELECT IdServico FROM Servico WHERE IndTipoServico="
                + (int)TipoServico.SegurancaTrabalho + ")";

            List<ContratoServico> list = new ContratoServico().Find<ContratoServico>(criteria);

            foreach (ContratoServico contratoServico in list)
            {
                if (contratoServico.IdIndice.Id == (int)Indices.SM)
                    val = contratoServico.ValorEmIndice();
                else
                    val = contratoServico.ValorTotal();
            }

            if (val == 0F)
                ret = string.Empty;

            if (val <= 20F)
                ret = val.ToString();
            else
                ret = val.ToString("n");

            return ret;
        }

        public float GetValorPorEmpregadoPCMSO()
        {
            if (this.ContrataPCMSO == (int)TipoPcmsoContratada.NaoContratada)
                return 0F;

            string criteria = "IdCliente=" + this.Id
                + " AND IdServico IN (SELECT IdServico"
                + " FROM Servico"
                + " WHERE IndTipoServico=" + (int)TipoServico.Pcmso + ")";

            List<ContratoServico> list = new ContratoServico().Find<ContratoServico>(criteria);

            float ret = 0F;

            foreach (ContratoServico contratoServico in list)
            {
                if (contratoServico.IndUnidade == (int)UnidadeServico.Manual)
                {
                    if (contratoServico.Quantidade == 1 
                        && this.QtdEmpregados != 0)
                        ret = contratoServico.Valor / this.QtdEmpregados;
                    else
                        ret = contratoServico.Valor;
                }
                else if (contratoServico.IndUnidade == (int)UnidadeServico.LocaisDeTrabalho)
                    ret = 0F;
                else
                    ret = contratoServico.Valor;
            }

            return ret;
        }
        #endregion

        #region Lista

        public static ArrayList Lista(Usuario usuario)
        {
            ArrayList list;

            list = new Cliente().FindIn(typeof(Juridica), 
                                        "NomeAbreviado", 
                                        true, 
                                        "IdJuridicaPapel=1 AND IsInativo=0");

            return list;
        }

        public DataSet Lista(Usuario usuario, string strBusca)
        {
            return this.Lista(usuario, strBusca, false, string.Empty, (int)IndLocaisTrabalho.LocaisAtivos);
        }

        public DataSet Lista(Usuario usuario, string srtBusca, bool locaisTrabalho, string strBuscaLocaisTrabalho, int indLocaisTrabalho)
        {
            string strWhere = string.Empty;
            Prestador prestador = new Prestador();

            if (usuario.NomeUsuario == "Admin")
                strWhere = " " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.Juridica.IdJuridicaPapel = " + (int)IndJuridicaPapel.Cliente
                    + " AND  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.Pessoa.IsInativo=0"
                    + " AND UPPER(NomeAbreviado) COLLATE SQL_Latin1_General_Cp1251_CS_AS LIKE '%" + srtBusca.ToUpper() + "%' ORDER BY NomeAbreviado";
            else
            {
                prestador.FindByPessoa(usuario.IdPessoa);

                if (!prestador.IsInativo)
                {
                    prestador.IdJuridica.Find();

                    if (prestador.IndTipoPrestador == (int)TipoPrestador.Medico)
                    {
                        //TRATAMENTO PARA MEDICO
                        if (prestador.IdJuridica.Id == (int)Empresas.MestraPaulista) // Medico da Mestra
                        {
                            strWhere = " " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.Juridica.IdJuridicaPapel = " + (int)IndJuridicaPapel.Cliente
                                + " AND  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.Pessoa.IsInativo=0";

                            if (prestador.hasVisibilidadeGrupo)
                                strWhere += " AND  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.Pessoa.IdPessoa IN (SELECT IdCliente FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.PrestadorCliente WHERE IdPrestador=" + prestador.Id + ")";
                            else
                                strWhere += " AND  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.Cliente.ContrataPCMSO <> " + (int)TipoPcmsoContratada.NaoContratada; // Lista todas as empresas com PCMSO contratada e apenas sistema
                        }
                        else if (prestador.IdJuridica.IdJuridicaPapel.Id == (int)IndJuridicaPapel.Clinica)
                        {
                            Clinica clinica = new Clinica();
                            clinica.Find("Clinica.IdJuridica=" + prestador.IdJuridica.Id);
                            strWhere = " " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.Juridica.IdJuridicaPapel = " + (int)IndJuridicaPapel.Cliente
                                + " AND  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.Pessoa.IsInativo=0"
                                + " AND  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.Pessoa.IdPessoa IN (SELECT IdCliente FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.ClinicaCliente WHERE IdClinica=" + clinica.Id + ")";
                        }
                        else if (prestador.IdJuridica.IdJuridicaPapel.Id == (int)IndJuridicaPapel.Cliente)
                        {
                            strWhere = " " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.Juridica.IdJuridicaPapel = " + (int)IndJuridicaPapel.Cliente
                                + " AND  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.Pessoa.IsInativo=0";

                            if (prestador.hasVisibilidadeGrupo)
                                strWhere += " AND  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.Pessoa.IdPessoa IN (SELECT IdCliente FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.PrestadorCliente WHERE IdPrestador=" + prestador.Id + ")";
                            else
                                strWhere += " AND  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.Pessoa.IdPessoa=" + prestador.IdJuridica.Id + "";
                        }
                        else if (prestador.IdJuridica.IsLocalDeTrabalho())
                        {
                            strWhere = " " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.Juridica.IdJuridicaPapel = " + prestador.IdJuridica.IdJuridicaPapel.Id
                                + " AND  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.Pessoa.IsInativo=0"
                                + " AND  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.Juridica.IdJuridicaPai IN (SELECT IdCliente FROM qryClienteAtivos)";

                            if (prestador.hasVisibilidadeGrupo)
                                strWhere += " AND  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.Pessoa.IdPessoa IN (SELECT IdCliente FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.PrestadorCliente WHERE IdPrestador=" + prestador.Id + ")";
                            else
                                strWhere += " AND  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.Pessoa.IdPessoa=" + prestador.IdJuridica.Id + "";
                        }
                        else
                            return new DataSet();
                    }
                    else if (prestador.IndTipoPrestador == (int)TipoPrestador.ContatoEmpresa ||
                        prestador.IndTipoPrestador == (int)TipoPrestador.Engenheiro)
                    {
                        //Tratamento para RH
                        if (prestador.IdJuridica.Id == (int)Empresas.MestraPaulista) // Prestador da Mestra
                        {
                            strWhere = " " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.Juridica.IdJuridicaPapel = " + (int)IndJuridicaPapel.Cliente
                                + " AND  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.Pessoa.IsInativo=0";

                            if (prestador.hasVisibilidadeGrupo)
                                strWhere += " AND  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.Pessoa.IdPessoa IN (SELECT IdCliente FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.PrestadorCliente WHERE IdPrestador=" + prestador.Id + ")";
                            else
                                strWhere += string.Empty; // Lista todas as empresas
                        }
                        else if (prestador.IdJuridica.IdJuridicaPapel.Id == (int)IndJuridicaPapel.Cliente)
                        {
                            strWhere = " " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.Juridica.IdJuridicaPapel = " + (int)IndJuridicaPapel.Cliente
                                + " AND  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.Pessoa.IsInativo=0";

                            if (prestador.hasVisibilidadeGrupo)
                                strWhere += " AND  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.Pessoa.IdPessoa IN (SELECT IdCliente FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.PrestadorCliente WHERE IdPrestador=" + prestador.Id + ")";
                            else
                                strWhere += " AND  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.Pessoa.IdPessoa=" + prestador.IdJuridica.Id + "";
                        }
                        else if (prestador.IdJuridica.IdJuridicaPapel.Id == (int)IndJuridicaPapel.Clinica)
                        {
                            Clinica clinica = new Clinica();
                            clinica.Find("Clinica.IdJuridica=" + prestador.IdJuridica.Id);
                            strWhere = " " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.Juridica.IdJuridicaPapel = " + (int)IndJuridicaPapel.Cliente
                                + " AND  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.Pessoa.IsInativo=0"
                                + " AND  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.Pessoa.IdPessoa IN (SELECT IdCliente FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.ClinicaCliente WHERE IdClinica=" + clinica.Id + ")";
                        }
                        else if (prestador.IdJuridica.IsLocalDeTrabalho())
                        {
                            strWhere = " " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.Juridica.IdJuridicaPapel = " + prestador.IdJuridica.IdJuridicaPapel.Id
                                + " AND  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.Pessoa.IsInativo=0"
                                + " AND  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.Juridica.IdJuridicaPai IN (SELECT IdCliente FROM qryClienteAtivos)";

                            if (prestador.hasVisibilidadeGrupo)
                                strWhere += " AND  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.Pessoa.IdPessoa IN (SELECT IdCliente FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.PrestadorCliente WHERE IdPrestador=" + prestador.Id + ")";
                            else
                                strWhere += " AND  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.Pessoa.IdPessoa=" + prestador.IdJuridica.Id + "";
                        }
                        else
                            return new DataSet();
                    }

                    strWhere += " AND UPPER(NomeAbreviado) COLLATE SQL_Latin1_General_Cp1251_CS_AS LIKE '%" + srtBusca.ToUpper() + "%' ORDER BY NomeAbreviado";
                }
                else
                    return new DataSet();
            }
            return ListaEmpresas(strWhere, locaisTrabalho, strBuscaLocaisTrabalho, indLocaisTrabalho, prestador);
        }

        private DataSet ListaEmpresas(string srtWhere, bool locaisTrabalho, string strBuscaLocaisTrabalho, int indLocaisTrabalho, Prestador prestador)
        {
            StringBuilder sql = new StringBuilder();

            sql.Append("SELECT																");
            sql.Append("	 " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.Pessoa.NomeAbreviado,									");
            sql.Append("	 " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.Pessoa.NomeCompleto,									");
            sql.Append("	 " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.Pessoa.IdPessoa AS Id,									");
            sql.Append("	 " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.Pessoa.IndTipoPessoa,									");
            sql.Append("	 " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.Cliente.IdCliente,										");
            sql.Append("	 " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.Juridica.AtivarLocalDeTrabalho							");
            sql.Append(" FROM																");
            sql.Append(" " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.Pessoa INNER JOIN											");
            sql.Append(" " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.Juridica ON  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.Pessoa.IdPessoa =  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.Juridica.IdPessoa INNER JOIN				");
            sql.Append(" " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.Cliente ON  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.Juridica.IdJuridica =  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.Cliente.IdJuridica INNER JOIN			");
            sql.Append(" " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.JuridicaPapel ON  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.Juridica.IdJuridicaPapel =  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.JuridicaPapel.IdJuridicaPapel");
            sql.Append(" WHERE ");
            sql.Append(srtWhere);

            DataSet dsEmpresas = new DataSet();
            dsEmpresas = this.ExecuteDataset(sql.ToString());

            if (locaisTrabalho)
            {
                srtWhere = srtWhere.Replace("ORDER BY NomeAbreviado", string.Empty);
                StringBuilder sqlLocais = new StringBuilder();

                sqlLocais.Append("IdJuridicaPai IN (SELECT  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.Pessoa.IdPessoa AS IdJuridicaPai FROM");
                sqlLocais.Append("  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.Pessoa INNER JOIN");
                sqlLocais.Append("  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.Juridica ON  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.Pessoa.IdPessoa =  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.Juridica.IdPessoa INNER JOIN");
                sqlLocais.Append("  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.Cliente ON  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.Juridica.IdJuridica =  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.Cliente.IdJuridica INNER JOIN");
                sqlLocais.Append("  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.JuridicaPapel ON  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.Juridica.IdJuridicaPapel =  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.JuridicaPapel.IdJuridicaPapel");
                sqlLocais.Append(" WHERE");
                sqlLocais.Append(" " + srtWhere + ")");

                if (!prestador.Id.Equals(0))
                {
                    if (prestador.IndTipoPrestador == (int)TipoPrestador.Medico)
                    {
                        if (prestador.IdJuridica.Id == (int)Empresas.MestraPaulista)
                        {
                            if (prestador.hasVisibilidadeGrupo)
                                sqlLocais.Append(" AND  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.Juridica.IdJuridica IN (SELECT IdCliente FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.PrestadorCliente WHERE IdPrestador=" + prestador.Id + ")");
                        }
                        else if (prestador.IdJuridica.IdJuridicaPapel.Id == (int)IndJuridicaPapel.Clinica)
                        {
                            Clinica clinica = new Clinica();
                            clinica.Find("Clinica.IdJuridica=" + prestador.IdJuridica.Id);

                            sqlLocais.Append(" AND  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.Juridica.IdJuridica IN (SELECT IdCliente FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.ClinicaCliente WHERE IdClinica=" + clinica.Id + ")");
                        }
                        else if (prestador.IdJuridica.IdJuridicaPapel.Id == (int)IndJuridicaPapel.Cliente)
                            sqlLocais.Append(" AND  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.Juridica.IdJuridica IN (SELECT IdCliente FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.PrestadorCliente WHERE IdPrestador=" + prestador.Id + ")");
                    }
                    else if (prestador.IndTipoPrestador == (int)TipoPrestador.ContatoEmpresa || prestador.IndTipoPrestador == (int)TipoPrestador.Engenheiro)
                    {
                        if (prestador.IdJuridica.Id == (int)Empresas.MestraPaulista)
                        {
                            if (prestador.hasVisibilidadeGrupo)
                                sqlLocais.Append(" AND  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.Juridica.IdJuridica IN (SELECT IdCliente FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.PrestadorCliente WHERE IdPrestador=" + prestador.Id + ")");
                        }
                        else if (prestador.IdJuridica.IdJuridicaPapel.Id == (int)IndJuridicaPapel.Clinica)
                        {
                            Clinica clinica = new Clinica();
                            clinica.Find("Clinica.IdJuridica=" + prestador.IdJuridica.Id);

                            sqlLocais.Append(" AND  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.Juridica.IdJuridica IN (SELECT IdCliente FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.ClinicaCliente WHERE IdClinica=" + clinica.Id + ")");
                        }
                        else if (prestador.IdJuridica.IdJuridicaPapel.Id == (int)IndJuridicaPapel.Cliente)
                            sqlLocais.Append(" AND  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.Juridica.IdJuridica IN (SELECT IdCliente FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.PrestadorCliente WHERE IdPrestador=" + prestador.Id + ")");
                    }
                }
                
                sqlLocais.Append(" AND IdJuridicaPapel IN (SELECT IdJuridicaPapel FROM JuridicaPapel WHERE IsLocalTrabalho=1)");
                sqlLocais.Append(" AND UPPER(NomeAbreviado) COLLATE SQL_Latin1_General_Cp1251_CS_AS LIKE '%" + strBuscaLocaisTrabalho.ToUpper() + "%'");

                switch (indLocaisTrabalho)
                {
                    case (int)IndLocaisTrabalho.LocaisAtivos:
                        sqlLocais.Append(" AND IsInativo=0");
                        break;
                    case (int)IndLocaisTrabalho.LocaisInativos:
                        sqlLocais.Append(" AND IsInativo=1");
                        break;
                }

                sqlLocais.Append(" ORDER BY NomeAbreviado");

                DataSet dsLocaisTrabalho = new Cliente().Get(sqlLocais.ToString());

                if (dsLocaisTrabalho.Tables[0].Rows.Count > 0)
                {
                    DataTable tableLocalTrabalho = new DataTable();
                    tableLocalTrabalho.Columns.Add("IdJuridicaPai", typeof(int));
                    tableLocalTrabalho.Columns.Add("IdLocalTrabalho", typeof(string));
                    tableLocalTrabalho.Columns.Add("NomeAbreviado", typeof(string));
                    DataRow newRow;

                    foreach (DataRow dsLocaisTrabalhoRow in dsLocaisTrabalho.Tables[0].Select())
                    {
                        newRow = tableLocalTrabalho.NewRow();
                        newRow["IdJuridicaPai"] = dsLocaisTrabalhoRow["IdJuridicaPai"];
                        newRow["IdLocalTrabalho"] = dsLocaisTrabalhoRow["IdCliente"].ToString();
                        newRow["NomeAbreviado"] = dsLocaisTrabalhoRow["NomeAbreviado"];
                        tableLocalTrabalho.Rows.Add(newRow);
                    }

                    dsEmpresas.Tables.Add(tableLocalTrabalho);
                    dsEmpresas.Relations.Add("Empresa_LocalTrabalho",
                        dsEmpresas.Tables[0].Columns["Id"],
                        dsEmpresas.Tables[1].Columns["IdJuridicaPai"], false);
                }
            }

            return dsEmpresas;
        }
        #endregion

        #region AtualizaQuantidadeEmpregado

        public void AtualizaQuantidadeEmpregado()
        {
            if (this.mirrorOld != null && !this.Equals(this.mirrorOld))
                throw new Exception("Há atualizações pendentes!\nA quantidade de empregados não pode ser recalculada.");

            new Juridica().ExecuteDataset("USE " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + " "
                    + " EXEC dbo.spu_AtualizaQtdEmpregadosCliente " + this.Id);

            this.Find();
        }

        #endregion

        #endregion
    }
    #endregion

    #region class qryClienteAtivos
    [Database("opsa", "qryClienteAtivos", "IdCliente")]
    public class qryClienteAtivos : Ilitera.Data.Table
    {
        private int _IdCliente;
        private string _NomeAbreviado = string.Empty;


        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public qryClienteAtivos()
        {

        }
        public override int Id
        {
            get { return _IdCliente; }
            set { _IdCliente = value; }
        }

        public string NomeAbreviado
        {
            get { return _NomeAbreviado; }
            set { _NomeAbreviado = value; }
        }
    }
    #endregion
}