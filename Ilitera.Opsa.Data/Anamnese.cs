using System;
using Ilitera.Data;
using Ilitera.Common;
using System.Data.SqlClient;

namespace Ilitera.Opsa.Data
{
    public enum StatusAnamnese : int
    {
        NaoPreenchido,
        Nao,
        Sim
    }

    [Database("opsa", "Anamnese", "IdAnamnese")]
    public class Anamnese : Table
    {
        private int _IdAnamnese;
        private ExameBase _IdExameBase;
        private short _HasQueixasAtuais;
        private short _HasAfastamento;
        private short _HasTraumatismos;
        private short _HasMedicacoes;
        private short _HasCirurgia;
        private short _HasAntecedentes;
        private short _HasTabagismo;
        private short _HasDeficienciaFisica;
        private short _HasAlcoolismo;
        private short _HasDoencaCronica;
        private short _HasMudancaRiscoOcupacional;

        private short _HasBronquite;
        private short _HasDigestiva;
        private short _HasEstomago;
        private short _HasEnxerga;
        private short _HasDorCabeca;
        private short _HasDesmaio;
        private short _HasCoracao;
        private short _HasUrinaria;
        private short _HasDiabetes;
        private short _HasGripado;
        private short _HasEscuta;
        private short _HasDoresCosta;
        private short _HasReumatismo;
        private short _HasAlergia;
        private short _HasEsporte;
        private short _HasAcidentou;
        private short _Has_AF_Hipertensao;
        private short _Has_AF_Diabetes;
        private short _Has_AF_Coracao;
        private short _Has_AF_Derrames;
        private short _Has_AF_Obesidade;
        private short _Has_AF_Cancer;
        private short _Has_AF_Colesterol;
        private short _Has_AF_Psiquiatricos;

        private short _Has_Otologica_Obstrucao;
        private short _Has_Otologica_Cerumen;
        private short _Has_Otologica_Alteracao;

        private short _HasColesterol;
        private short _HasHipertensao;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public Anamnese()
        {

        }

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public Anamnese(ExameBase exameBase)
        {
            this.Find("IdExameBase=" + exameBase.Id);
        }
        public override int Id
        {
            get { return _IdAnamnese; }
            set { _IdAnamnese = value; }
        }
        public ExameBase IdExameBase
        {
            get { return _IdExameBase; }
            set { _IdExameBase = value; }
        }
        public short HasQueixasAtuais
        {
            get { return _HasQueixasAtuais; }
            set { _HasQueixasAtuais = value; }
        }
        public short HasAfastamento
        {
            get { return _HasAfastamento; }
            set { _HasAfastamento = value; }
        }
        public short HasTraumatismos
        {
            get { return _HasTraumatismos; }
            set { _HasTraumatismos = value; }
        }
        public short HasMedicacoes
        {
            get { return _HasMedicacoes; }
            set { _HasMedicacoes = value; }
        }
        public short HasCirurgia
        {
            get { return _HasCirurgia; }
            set { _HasCirurgia = value; }
        }
        public short HasAntecedentes
        {
            get { return _HasAntecedentes; }
            set { _HasAntecedentes = value; }
        }
        public short HasTabagismo
        {
            get { return _HasTabagismo; }
            set { _HasTabagismo = value; }
        }
        public short HasDeficienciaFisica
        {
            get { return _HasDeficienciaFisica; }
            set { _HasDeficienciaFisica = value; }
        }
        public short HasAlcoolismo
        {
            get { return _HasAlcoolismo; }
            set { _HasAlcoolismo = value; }
        }
        public short HasDoencaCronica
        {
            get { return _HasDoencaCronica; }
            set { _HasDoencaCronica = value; }
        }
        public short HasMudancaRiscoOcupacional
        {
            get { return _HasMudancaRiscoOcupacional; }
            set { _HasMudancaRiscoOcupacional = value; }
        }

        public short HasBronquite
        {
            get { return _HasBronquite; }
            set { _HasBronquite = value; }
        }

        public short HasDigestiva
        {
            get { return _HasDigestiva; }
            set { _HasDigestiva = value; }
        }
        public short HasEstomago
        {
            get { return _HasEstomago; }
            set { _HasEstomago = value; }
        }
        public short HasEnxerga
        {
            get { return _HasEnxerga; }
            set { _HasEnxerga = value; }
        }
        public short HasDorCabeca
        {
            get { return _HasDorCabeca; }
            set { _HasDorCabeca = value; }
        }
        public short HasDesmaio
        {
            get { return _HasDesmaio; }
            set { _HasDesmaio = value; }
        }
        public short HasCoracao
        {
            get { return _HasCoracao; }
            set { _HasCoracao = value; }
        }
        public short HasUrinaria
        {
            get { return _HasUrinaria; }
            set { _HasUrinaria = value; }
        }
        public short HasDiabetes
        {
            get { return _HasDiabetes; }
            set { _HasDiabetes = value; }
        }
        public short HasGripado
        {
            get { return _HasGripado; }
            set { _HasGripado = value; }
        }
        public short HasEscuta
        {
            get { return _HasEscuta; }
            set { _HasEscuta = value; }
        }
        public short HasDoresCosta
        {
            get { return _HasDoresCosta; }
            set { _HasDoresCosta = value; }
        }
        public short HasReumatismo
        {
            get { return _HasReumatismo; }
            set { _HasReumatismo = value; }
        }
        public short HasAlergia
        {
            get { return _HasAlergia; }
            set { _HasAlergia = value; }
        }
        public short HasEsporte
        {
            get { return _HasEsporte; }
            set { _HasEsporte = value; }
        }
        public short HasAcidentou
        {
            get { return _HasAcidentou; }
            set { _HasAcidentou = value; }
        }
        public short Has_AF_Hipertensao
        {
            get { return _Has_AF_Hipertensao; }
            set { _Has_AF_Hipertensao = value; }
        }
        public short Has_AF_Diabetes
        {
            get { return _Has_AF_Diabetes; }
            set { _Has_AF_Diabetes = value; }
        }
        public short Has_AF_Coracao
        {
            get { return _Has_AF_Coracao; }
            set { _Has_AF_Coracao = value; }
        }
        public short Has_AF_Derrames
        {
            get { return _Has_AF_Derrames; }
            set { _Has_AF_Derrames = value; }
        }
        public short Has_AF_Obesidade
        {
            get { return _Has_AF_Obesidade; }
            set { _Has_AF_Obesidade = value; }
        }
        public short Has_AF_Cancer
        {
            get { return _Has_AF_Cancer; }
            set { _Has_AF_Cancer = value; }
        }
        public short Has_AF_Colesterol
        {
            get { return _Has_AF_Colesterol; }
            set { _Has_AF_Colesterol = value; }
        }
        public short Has_AF_Psiquiatricos
        {
            get { return _Has_AF_Psiquiatricos; }
            set { _Has_AF_Psiquiatricos = value; }
        }

        public short Has_Otologica_Obstrucao
        {
            get { return _Has_Otologica_Obstrucao; }
            set { _Has_Otologica_Obstrucao = value; }
        }

        public short Has_Otologica_Cerumen
        {
            get { return _Has_Otologica_Cerumen; }
            set { _Has_Otologica_Cerumen = value; }
        }

        public short Has_Otologica_Alteracao
        {
            get { return _Has_Otologica_Alteracao; }
            set { _Has_Otologica_Alteracao = value; }
        }

        public short HasColesterol
        {
            get { return _HasColesterol; }
            set { _HasColesterol = value; }
        }

        public short HasHipertensao
        {
            get { return _HasHipertensao; }
            set { _HasHipertensao = value; }
        }




    }


    [Database("opsa", "ExameFisico", "IdExameFisico")]
    public class ExameFisico : Table
    {
        private int _IdExameFisico;
        private ExameBase _IdExameBase;
        private string _PressaoArterial = string.Empty;
        private int _Pulso;
        private float _Peso;
        private float _Altura;
        private short _hasPeleAnexosAlterado;
        private short _hasOsteoAlterado;
        private short _hasCabecaAlterado;
        private short _hasCoracaoAlterado;
        private short _hasPulmaoAlterado;
        private short _hasAbdomemAlterado;
        private short _hasMSAlterado;
        private short _hasMIAlterado;
        private DateTime _DataUltimaMenstruacao = new DateTime();
        private float _Temperatura=0;
        private string _Glicose;
        private string _AP_Penultima_Empresa;
        private string _AP_Penultima_Funcao;
        private string _AP_Penultima_Tempo;
        private string _AP_Ultima_Empresa;
        private string _AP_Ultima_Funcao;
        private string _AP_Ultima_Tempo;

        private string _PeleAnexosAlterado;
        private string _OsteoAlterado;
        private string _CabecaAlterado;
        private string _CoracaoAlterado;
        private string _PulmaoAlterado;
        private string _AbdomemAlterado;
        private string _MSAlterado;
        private string _MIAlterado;

        private string _CircunferenciaAbdominal;
        private short _hasColunaVertebral;
        private short _hasMVsemRA;
        private short _hasBRNF;
        private short _hasPlano;
        private short _hasDB;
        private short _hasRHA;
        private short _hasIndolor;
        private short _hasAbaulamentos;
        private string _MVsemRAAlterado;
        private string _BRNFAlterado;
        private string _PlanoAlterado;
        private string _DBAlterado;
        private string _RHAAlterado;
        private string _IndolorAlterado;
        private string _AbaulamentosAlterado;
        private string _ColunaVertebralAlterado;

        private string _OlhosAlterado;
        private string _MaosPunhosAlterado;
        private short _hasCabecaNormal ;
        private short _hasTireoide ;
        private short _hasDente ;
        private short _hasGarganta ;
        private short _hasAdenomegalia ;
        private short _hasOuvido ;
        private short _hasOlhosNormal ;
        private short _hasOculosLentes ;
        private short _hasPeleNormal ;
        private short _hasDescoradas ;
        private short _hasIctericas ;
        private short _hasCianoticas ;
        private short _hasDermatoses ;
        private short _hasMVAlterado ;
        private short _hasAdventicios ;
        private short _hasArritmias ;
        private short _hasSopros ;
        private short _hasCardioOutros ;
        private short _hasPalpacao ;
        private short _hasHepatoesplenomegalia ;
        private short _hasMassas ;
        private short _hasMINormal ;
        private short _hasEdemas ;
        private short _hasPulsos ;
        private short _hasArticular ;
        private short _hasVarizes ;
        private short _hasMSNormal ;
        private short _hasDorRotacao ;
        private short _hasPalpacaoBiceps ;
        private short _hasPalpacaoSubacromial ;
        private short _hasMaosNormal ;
        private short _hasTinelD ;
        private short _hasTinelE ;
        private short _hasPreensaoD ;
        private short _hasPreensaoE ;
        private short _hasFinkelsteinD ;
        private short _hasFinkelsteinE ;
        private short _hasPhalenD ;
        private short _hasPhalenE ;
        private short _hasNodulosD ;
        private short _hasNodulosE ;
        private short _hasCrepitacaoD ;
        private short _hasCrepitacaoE ;
        private short _hasEdemaD ;
        private short _hasEdemaE ;
        private short _hasColunaNormal ;
        private short _hasCifose ;
        private short _hasLordose ;
        private short _hasEscoliose ;
        private short _hasLasegue ;
        private short _hasLimitacaoFlexao ;
        private short _Diastolica;
        private short _Sistolica;
               

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public ExameFisico()
        {

        }

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public ExameFisico(ExameBase exameBase)
        {
            this.Find("IdExameBase=" + exameBase.Id);
        }
        public override int Id
        {
            get { return _IdExameFisico; }
            set { _IdExameFisico = value; }
        }
        public ExameBase IdExameBase
        {
            get { return _IdExameBase; }
            set { _IdExameBase = value; }
        }
        public string PressaoArterial
        {
            get { return _PressaoArterial; }
            set { _PressaoArterial = value; }
        }
        public int Pulso
        {
            get { return _Pulso; }
            set { _Pulso = value; }
        }
        public float Altura
        {
            get { return _Altura; }
            set { _Altura = value; }
        }
        public float Peso
        {
            get { return _Peso; }
            set { _Peso = value; }
        }
        public DateTime DataUltimaMenstruacao
        {
            get { return _DataUltimaMenstruacao; }
            set { _DataUltimaMenstruacao = value; }
        }
        public float Temperatura
        {
            get { return _Temperatura; }
            set { _Temperatura = value; }
        }
        public short hasPeleAnexosAlterado
        {
            get { return _hasPeleAnexosAlterado; }
            set { _hasPeleAnexosAlterado = value; }
        }
        public short hasOsteoAlterado
        {
            get { return _hasOsteoAlterado; }
            set { _hasOsteoAlterado = value; }
        }
        public short hasCabecaAlterado
        {
            get { return _hasCabecaAlterado; }
            set { _hasCabecaAlterado = value; }
        }
        public short hasCoracaoAlterado
        {
            get { return _hasCoracaoAlterado; }
            set { _hasCoracaoAlterado = value; }
        }
        public short hasPulmaoAlterado
        {
            get { return _hasPulmaoAlterado; }
            set { _hasPulmaoAlterado = value; }
        }
        public short hasAbdomemAlterado
        {
            get { return _hasAbdomemAlterado; }
            set { _hasAbdomemAlterado = value; }
        }
        public short hasMSAlterado
        {
            get { return _hasMSAlterado; }
            set { _hasMSAlterado = value; }
        }
        public short hasMIAlterado
        {
            get { return _hasMIAlterado; }
            set { _hasMIAlterado = value; }
        }
        public string Glicose
        {
            get { return _Glicose; }
            set { _Glicose = value; }
        }
        public string AP_Penultima_Empresa
        {
            get { return _AP_Penultima_Empresa; }
            set { _AP_Penultima_Empresa = value; }
        }
        public string AP_Penultima_Funcao
        {
            get { return _AP_Penultima_Funcao; }
            set { _AP_Penultima_Funcao = value; }
        }
        public string AP_Penultima_Tempo
        {
            get { return _AP_Penultima_Tempo; }
            set { _AP_Penultima_Tempo = value; }
        }
        public string AP_Ultima_Empresa
        {
            get { return _AP_Ultima_Empresa; }
            set { _AP_Ultima_Empresa = value; }
        }
        public string AP_Ultima_Funcao
        {
            get { return _AP_Ultima_Funcao; }
            set { _AP_Ultima_Funcao = value; }
        }
        public string AP_Ultima_Tempo
        {
            get { return _AP_Ultima_Tempo; }
            set { _AP_Ultima_Tempo = value; }
        }
        public string PeleAnexosAlterado
        {
            get { return _PeleAnexosAlterado; }
            set { _PeleAnexosAlterado = value; }
        }
        public string OsteoAlterado
        {
            get { return _OsteoAlterado; }
            set { _OsteoAlterado = value; }
        }
        public string CabecaAlterado
        {
            get { return _CabecaAlterado; }
            set { _CabecaAlterado = value; }
        }
        public string CoracaoAlterado
        {
            get { return _CoracaoAlterado; }
            set { _CoracaoAlterado = value; }
        }
        public string PulmaoAlterado
        {
            get { return _PulmaoAlterado; }
            set { _PulmaoAlterado = value; }
        }
        public string AbdomemAlterado
        {
            get { return _AbdomemAlterado; }
            set { _AbdomemAlterado = value; }
        }
        public string MSAlterado
        {
            get { return _MSAlterado; }
            set { _MSAlterado = value; }
        }
        public string MIAlterado
        {
            get { return _MIAlterado; }
            set { _MIAlterado = value; }
        }

        public string CircunferenciaAbdominal
        {
            get { return _CircunferenciaAbdominal; }
            set { _CircunferenciaAbdominal = value; }
        }

        public short hasColunaVertebral
        {
            get { return _hasColunaVertebral; }
            set { _hasColunaVertebral = value; }
        }
        public short hasMVsemRA
        {
            get { return _hasMVsemRA; }
            set { _hasMVsemRA = value; }
        }
        public short hasBRNF
        {
            get { return _hasBRNF; }
            set { _hasBRNF = value; }
        }
        public short hasPlano
        {
            get { return _hasPlano; }
            set { _hasPlano = value; }
        }
        public short hasDB
        {
            get { return _hasDB; }
            set { _hasDB = value; }
        }
        public short hasRHA
        {
            get { return _hasRHA; }
            set { _hasRHA = value; }
        }

        public short hasIndolor
        {
            get { return _hasIndolor; }
            set { _hasIndolor = value; }
        }
        public short hasAbaulamentos
        {
            get { return _hasAbaulamentos; }
            set { _hasAbaulamentos = value; }
        }


        public string ColunaVertebralAlterado
        {
            get { return _ColunaVertebralAlterado; }
            set { _ColunaVertebralAlterado = value; }
        }
        public string MVsemRAAlterado
        {
            get { return _MVsemRAAlterado; }
            set { _MVsemRAAlterado = value; }
        }
        public string BRNFAlterado
        {
            get { return _BRNFAlterado; }
            set { _BRNFAlterado = value; }
        }
        public string PlanoAlterado
        {
            get { return _PlanoAlterado; }
            set { _PlanoAlterado = value; }
        }
        public string DBAlterado
        {
            get { return _DBAlterado; }
            set { _DBAlterado = value; }
        }
        public string RHAAlterado
        {
            get { return _RHAAlterado; }
            set { _RHAAlterado = value; }
        }
        public string IndolorAlterado
        {
            get { return _IndolorAlterado; }
            set { _IndolorAlterado = value; }
        }
        public string AbaulamentosAlterado
        {
            get { return _AbaulamentosAlterado; }
            set { _AbaulamentosAlterado = value; }
        }

        public string OlhosAlterado
        {
            get { return _OlhosAlterado; }
            set { _OlhosAlterado = value; }
        }
        public string MaosPunhosAlterado
        {
            get { return _MaosPunhosAlterado; }
            set { _MaosPunhosAlterado = value; }
        }


        public short hasCabecaNormal
        {
            get { return _hasCabecaNormal; }
            set { _hasCabecaNormal = value; }
        }
        public short hasTireoide
        {
            get { return _hasTireoide; }
            set { _hasTireoide = value; }
        }
        public short hasDente
        {
            get { return _hasDente; }
            set { _hasDente = value; }
        }

        public short hasGarganta
        {
            get { return _hasGarganta; }
            set { _hasGarganta = value; }
        }
        public short hasAdenomegalia
        {
            get { return _hasAdenomegalia; }
            set { _hasAdenomegalia = value; }
        }
        public short hasOuvido
        {
            get { return _hasOuvido; }
            set { _hasOuvido = value; }
        }
        public short hasOlhosNormal
        {
            get { return _hasOlhosNormal; }
            set { _hasOlhosNormal = value; }
        }
        public short hasOculosLentes
        {
            get { return _hasOculosLentes; }
            set { _hasOculosLentes = value; }
        }
        public short hasPeleNormal
        {
            get { return _hasPeleNormal; }
            set { _hasPeleNormal = value; }
        }
        public short hasDescoradas
        {
            get { return _hasDescoradas; }
            set { _hasDescoradas = value; }
        }
        public short hasIctericas
        {
            get { return _hasIctericas; }
            set { _hasIctericas = value; }
        }
        public short hasCianoticas
        {
            get { return _hasCianoticas; }
            set { _hasCianoticas = value; }
        }

        public short hasDermatoses
        {
            get { return _hasDermatoses; }
            set { _hasDermatoses = value; }
        }
        public short hasMVAlterado
        {
            get { return _hasMVAlterado; }
            set { _hasMVAlterado = value; }
        }
        public short hasAdventicios
        {
            get { return _hasAdventicios; }
            set { _hasAdventicios = value; }
        }

        public short hasArritmias
        {
            get { return _hasArritmias; }
            set { _hasArritmias = value; }
        }
        public short hasSopros
        {
            get { return _hasSopros; }
            set { _hasSopros= value; }
        }
        public short hasCardioOutros
        {
            get { return _hasCardioOutros; }
            set { _hasCardioOutros = value; }
        }

        public short hasPalpacao
        {
            get { return _hasPalpacao; }
            set { _hasPalpacao = value; }
        }
        public short hasHepatoesplenomegalia
        {
            get { return _hasHepatoesplenomegalia; }
            set { _hasHepatoesplenomegalia = value; }
        }
        public short hasMassas
        {
            get { return _hasMassas; }
            set { _hasMassas = value; }
        }

        public short hasMINormal
        {
            get { return _hasMINormal; }
            set { _hasMINormal = value; }
        }
        public short hasEdemas
        {
            get { return _hasEdemas; }
            set { _hasEdemas = value; }
        }
        public short hasPulsos
        {
            get { return _hasPulsos; }
            set { _hasPulsos = value; }
        }

        public short hasArticular
        {
            get { return _hasArticular; }
            set { _hasArticular = value; }
        }
        public short hasVarizes
        {
            get { return _hasVarizes; }
            set { _hasVarizes = value; }
        }
        public short hasMSNormal
        {
            get { return _hasMSNormal; }
            set { _hasMSNormal = value; }
        }

        public short hasDorRotacao
        {
            get { return _hasDorRotacao; }
            set { _hasDorRotacao = value; }
        }
        public short hasPalpacaoBiceps
        {
            get { return _hasPalpacaoBiceps; }
            set { _hasPalpacaoBiceps = value; }
        }
        public short hasPalpacaoSubacromial
        {
            get { return _hasPalpacaoSubacromial; }
            set { _hasPalpacaoSubacromial = value; }
        }

        public short hasMaosNormal
        {
            get { return _hasMaosNormal; }
            set { _hasMaosNormal = value; }
        }
        public short hasTinelD
        {
            get { return _hasTinelD; }
            set { _hasTinelD = value; }
        }
        public short hasTinelE
        {
            get { return _hasTinelE; }
            set { _hasTinelE = value; }
        }

        public short hasPreensaoD
        {
            get { return _hasPreensaoD; }
            set { _hasPreensaoD = value; }
        }
        public short hasPreensaoE
        {
            get { return _hasPreensaoE; }
            set { _hasPreensaoE = value; }
        }

        public short hasFinkelsteinD
        {
            get { return _hasFinkelsteinD; }
            set { _hasFinkelsteinD = value; }
        }
        public short hasFinkelsteinE
        {
            get { return _hasFinkelsteinE; }
            set { _hasFinkelsteinE = value; }
        }

        public short hasPhalenD
        {
            get { return _hasPhalenD; }
            set { _hasPhalenD = value; }
        }
        public short hasPhalenE
        {
            get { return _hasPhalenE; }
            set { _hasPhalenE = value; }
        }

        public short hasNodulosD
        {
            get { return _hasNodulosD; }
            set { _hasNodulosD = value; }
        }
        public short hasNodulosE
        {
            get { return _hasNodulosE; }
            set { _hasNodulosE = value; }
        }

        public short hasCrepitacaoD
        {
            get { return _hasCrepitacaoD; }
            set { _hasCrepitacaoD = value; }
        }
        public short hasCrepitacaoE
        {
            get { return _hasCrepitacaoE; }
            set { _hasCrepitacaoE = value; }
        }

        public short hasEdemaD
        {
            get { return _hasEdemaD; }
            set { _hasEdemaD = value; }
        }
        public short hasEdemaE
        {
            get { return _hasEdemaE; }
            set { _hasEdemaE = value; }
        }

        public short hasColunaNormal
        {
            get { return _hasColunaNormal; }
            set { _hasColunaNormal = value; }
        }
        public short hasCifose
        {
            get { return _hasCifose; }
            set { _hasCifose = value; }
        }

        public short hasLordose
        {
            get { return _hasLordose; }
            set { _hasLordose = value; }
        }
        public short hasEscoliose
        {
            get { return _hasEscoliose; }
            set { _hasEscoliose = value; }
        }

        public short hasLasegue
        {
            get { return _hasLasegue; }
            set { _hasLasegue = value; }
        }
        public short hasLimitacaoFlexao
        {
            get { return _hasLimitacaoFlexao; }
            set { _hasLimitacaoFlexao = value; }
        }
        public short Sistolica
        {
            get { return _Sistolica; }
            set { _Sistolica = value; }
        }
        public short Diastolica
        {
            get { return _Diastolica; }
            set { _Diastolica = value; }
        }



    }

    //Pattern Façade
    public class ExameClinicoFacade
    {
        private Clinico exameBase;
        private Ilitera.Opsa.Data.Anamnese anamnese;
        private ExameFisico exameFisico;

        public ExameClinicoFacade()
        {
            exameBase = new Clinico();
            anamnese = new Anamnese();
            exameFisico = new ExameFisico();
        }

        public int Id
        {
            get { return exameBase.Id; }
            set { exameBase.Id = value; }
        }


        public string apt_Espaco_Confinado
        {
            get { return exameBase.apt_Espaco_Confinado2; }
            set { exameBase.apt_Espaco_Confinado2 = value; }
        }
        public string apt_Trabalho_Altura
        {
            get { return exameBase.apt_Trabalho_Altura2; }
            set { exameBase.apt_Trabalho_Altura2 = value; }
        }
        public string apt_Transporte
        {
            get { return exameBase.apt_Transporte2; }
            set { exameBase.apt_Transporte2 = value; }
        }
        public string apt_Submerso
        {
            get { return exameBase.apt_Submerso2; }
            set { exameBase.apt_Submerso2 = value; }
        }
        public string apt_Eletricidade
        {
            get { return exameBase.apt_Eletricidade2; }
            set { exameBase.apt_Eletricidade2 = value; }
        }
        public string apt_Aquaviario
        {
            get { return exameBase.apt_Aquaviario2; }
            set { exameBase.apt_Aquaviario2 = value; }
        }
        public string apt_Alimento
        {
            get { return exameBase.apt_Alimento2; }
            set { exameBase.apt_Alimento2 = value; }
        }
        public string apt_Brigadista
        {
            get { return exameBase.apt_Brigadista2; }
            set { exameBase.apt_Brigadista2 = value; }
        }
        public string apt_Socorrista
        {
            get { return exameBase.apt_Socorrista2; }
            set { exameBase.apt_Socorrista2 = value; }
        }
        public string apt_Respirador
        {
            get { return exameBase.apt_Respirador2; }
            set { exameBase.apt_Respirador2 = value; }
        }
        public string apt_Radiacao
        {
            get { return exameBase.apt_Radiacao2; }
            set { exameBase.apt_Radiacao2 = value; }
        }

        public bool Paciente_Critico
        {
            get { return exameBase.Paciente_Critico; }
            set { exameBase.Paciente_Critico = value; }
        }


        public string Observacao
        {
            get { return exameBase.ObservacaoResultado; }
            set { exameBase.ObservacaoResultado = value; }
        }

        public DateTime DataExame
        {
            get { return exameBase.DataExame; }
            set { exameBase.DataExame = value; }
        }
        public EmpregadoFuncao IdEmpregadoFuncao
        {
            get { return exameBase.IdEmpregadoFuncao; }
            set { exameBase.IdEmpregadoFuncao = value; }
        }
        public Pcmso IdPcmso
        {
            get { return exameBase.IdPcmso; }
            set { exameBase.IdPcmso = value; }
        }
        public Medico IdMedico
        {
            get { return exameBase.IdMedico; }
            set { exameBase.IdMedico = value; }
        }

        public int IndResultado
        {
            get { return exameBase.IndResultado; }
            set { exameBase.IndResultado = value; }
        }

        public bool Tirar_eSocial
        {
            get { return exameBase.Tirar_eSocial; }
            set { exameBase.Tirar_eSocial = value; }
        }

        public string Prontuario
        {
            get { return exameBase.Prontuario; }
            set { exameBase.Prontuario = value; }
        }

        public Juridica IdJuridica
        {
            get { return exameBase.IdJuridica; }
            set { exameBase.IdJuridica = value; }
        }

        public ConvocacaoExame IdConvocacaoExame
        {
            get { return exameBase.IdConvocacaoExame; }
            set { exameBase.IdConvocacaoExame = value; }
        }

        public ExameDicionario IdExameDicionario
        {
            get { return exameBase.IdExameDicionario; }
            set { exameBase.IdExameDicionario = value; }
        }

        public Empregado IdEmpregado
        {
            get { return exameBase.IdEmpregado; }
            set { exameBase.IdEmpregado = value; }
        }

        public Ilitera.Opsa.Data.Anamnese Anamnese
        {
            get { return anamnese; }
            set { anamnese = value; }
        }

        public Ilitera.Opsa.Data.ExameFisico Fisico
        {
            get { return exameFisico; }
            set { exameFisico = value; }
        }

        public DateTime DataDemissao
        {
            get { return exameBase.DataDemissao; }
            set { exameBase.DataDemissao = value; }
        }
        public int CodBusca
        {
            get { return exameBase.CodBusca; }
            set { exameBase.CodBusca = value; }
        }



        public void Inicialize()
        {
            exameBase.Inicialize();
        }

        public void Find(int Id)
        {
            exameBase.Find(Id);
            anamnese.Find("IdExameBase=" + exameBase.Id);
            exameFisico.Find("IdExameBase=" + exameBase.Id);
        }

        public int Save(int IdUsuario)
        {
            exameBase.UsuarioId = IdUsuario;

            int Id = exameBase.Save();

            exameBase.IdEmpregado.Find();
            exameBase.IdEmpregado.nID_EMPR.Find();

            //if (exameBase.IdEmpregado.nID_EMPR.ContrataPCMSO != (int)TipoPcmsoContratada.NaoContratada)
            //{
            anamnese.IdExameBase = exameBase;
            anamnese.Save();

            exameFisico.IdExameBase = exameBase;
            exameFisico.Save();
            // }
            return Id;
        }

        public void Delete(int IdUsuario)
        {
            exameBase.UsuarioId = IdUsuario;
            exameBase.Delete();
        }

        public void Delete(bool forcedDelete, int IdUsuario) //Necessário para clientes que não são PCMSO contratada no Ilitera.NET
        {
            exameBase.UsuarioId = IdUsuario;
            exameBase.Delete(forcedDelete);
        }

        public void Faltou(Usuario usuario)
        {
            exameBase.Faltou(usuario, ExameNaoRealizado.MotivoFalta.AusenciaPorMotivoNaoJustificado);
        }
    }

    [Database("opsa", "ProcedimentoClinico", "IdProcedimentoClinico", "", "Procedimento Clínico")]
    public class ProcedimentoClinico : Table
    {
        private int _IdProcedimentoClinico;
        private Cliente _IdCliente;
        private string _NomeProcedimento;
        private string _DescricaoProcedimento;


        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public ProcedimentoClinico()
        {
        }

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public ProcedimentoClinico(int Id)
        {
            this.Find(Id);
        }
        public override int Id
        {
            get { return _IdProcedimentoClinico; }
            set { _IdProcedimentoClinico = value; }
        }
        public Cliente IdCliente
        {
            get { return _IdCliente; }
            set { _IdCliente = value; }
        }
        [Obrigatorio(true, "O Nome do Procedimento Clínico deve ser informado!")]
        public string NomeProcedimento
        {
            get { return _NomeProcedimento; }
            set { _NomeProcedimento = value; }
        }
        public string DescricaoProcedimento
        {
            get { return _DescricaoProcedimento; }
            set { _DescricaoProcedimento = value; }
        }
    }

    [Database("opsa", "QueixaClinica", "IdQueixaClinica", "", "Queixa Clínica")]
    public class QueixaClinica : Table
    {
        private int _IdQueixaClinica;
        private Cliente _IdCliente;
        private string _NomeQueixa;
        private string _DescricaoQueixa;


        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public QueixaClinica()
        {
        }

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public QueixaClinica(int Id)
        {
            this.Find(Id);
        }
        public override int Id
        {
            get { return _IdQueixaClinica; }
            set { _IdQueixaClinica = value; }
        }
        public Cliente IdCliente
        {
            get { return _IdCliente; }
            set { _IdCliente = value; }
        }
        [Obrigatorio(true, "O Nome da Queixa Clínica deve ser informado!")]
        public string NomeQueixa
        {
            get { return _NomeQueixa; }
            set { _NomeQueixa = value; }
        }
        public string DescricaoQueixa
        {
            get { return _DescricaoQueixa; }
            set { _DescricaoQueixa = value; }
        }
    }
}
