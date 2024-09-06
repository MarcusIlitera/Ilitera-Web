using System;
using System.Text;
using System.Data;
using System.Collections;
using System.Collections.Generic;

using Ilitera.Data;

namespace Ilitera.Opsa.Data
{
    #region enum CodigoGFIP

    public enum CodigoGFIP : int
    {
        PPRA,// 00 para salubre, e 04 para insalubre

        /*
         * Para indivíduos com apenas um vínculo empregatício
         */
        Cod00,// Nunca esteve exposto
        Cod01,// já esteve exposto, atualmente neutralizado
        Cod02,// aposentadoria com 15 anos
        Cod03,// aposentadoria com 20 anos
        Cod04,// aposentadoria com 25 anos

        /*
         * Para indivíduos com mais de um vínculo empregatício
         */
        Cod05,// não exposição - múltiplos vínculos
        Cod06,// 15 anos - múltiplos vínculos
        Cod07,// 20 anos - múltiplos vínculos
        Cod08,// 25 anos - múltiplos vínculos
    }

    #endregion

    [Database("sied_novo", "tblFUNC", "nID_FUNC")]
    public class Ghe : Ilitera.Data.Table
    {
        public const string strInexistente = "Inexistente.";

        #region Privates

        private int _nID_FUNC;
        private LaudoTecnico _nID_LAUD_TEC;
        private string _tNO_FUNC = string.Empty;
        private string _tCOD_FUNC = string.Empty;
        private string _tDS_FUNC = string.Empty;
        private Ghe _nID_GHE_OLD;
        private string _tDS_CNCL_FUNC = string.Empty;
        private FraseGheAE _nID_CNCL_2_FUNC_AE;
        private bool _bCNCL_2_MANUAL_AE;
        private string _tDS_CNCL_2_FUNC_AE = string.Empty;
        private bool _bOCASIONAL_INTERM;
        private AtividadeTipo _nIND_ATIVIDADES;
        private TipoExposicaoOcupacional _nIND_TIPOEXPOSICAOOCUPACIONAL;
        private bool _bPERICULOSIDADE;
        private string _tDS_LOCAL_TRAB = string.Empty;
        private int _tNO_FOTO_FUNC;
        private string _tDS_FUNC_EPC_EXTE = string.Empty;
        private string _tDS_FUNC_EPC_RCM = string.Empty;
        private string _tDS_MED_CTL_CAR_ADM = string.Empty;
        private string _tDS_MED_CTL_CAR_EDC = string.Empty;
        private string _tDS_COMPL_DEMAIS_AGENTES = string.Empty;
        private TempoExposicao _nID_TEMPO_EXP;
        private int _nIND_GFIP;
        private float _nUMID;
        private float _nVELOC;
        private float _nLUX;
        private int _nTIPO_ATV_LUX;
        private float _nTEMP;
        private float _nIluminancia;
        private int _nTIPO_ILUMINANCIA;


        private bool _bImp_17_6_2;
        private string _tNormas_Producao;
        private string _tModo_Operatorio;
        private string _tExigencia_Tempo;
        private string _tDeterminacao_Conteudo;
        private string _tRitmo_Trabalho;
        private string _tConteudo_Tarefa;


        private bool _bImp_Recomendacoes;
        private string _tRecomendacoes;

        private string _Caracterizacao_Processos;

        #endregion

        #region enum AtividadeTipo
        public enum AtividadeTipo : int
        {
            NaoInformada,
            ExclusivamenteAdministrativa,
            PredominantementeAdministrativa,
            ExclusivamenteOperacional
        }
        #endregion

        #region Contructor

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public Ghe()
        {

        }
        #endregion

        #region Properties

        public override int Id
        {
            get { return _nID_FUNC; }
            set { _nID_FUNC = value; }
        }
        [Obrigatorio(true, "O Laudo Técnico é campo obrigatório!")]
        public LaudoTecnico nID_LAUD_TEC
        {
            get { return _nID_LAUD_TEC; }
            set { _nID_LAUD_TEC = value; }
        }
        [Obrigatorio(true, "O nome do Ghe é campo obrigatório!")]
        public string tNO_FUNC
        {
            get { return _tNO_FUNC; }
            set { _tNO_FUNC = value; }
        }
        public string tCOD_FUNC
        {
            get { return _tCOD_FUNC; }
            set { _tCOD_FUNC = value; }
        }
        public string tDS_FUNC
        {
            get { return _tDS_FUNC; }
            set { _tDS_FUNC = value; }
        }
        public Ghe nID_GHE_OLD
        {
            get { return _nID_GHE_OLD; }
            set { _nID_GHE_OLD = value; }
        }
        public string tDS_CNCL_FUNC
        {
            get { return _tDS_CNCL_FUNC; }
            set { _tDS_CNCL_FUNC = value; }
        }
        public FraseGheAE nID_CNCL_2_FUNC_AE
        {
            get { return _nID_CNCL_2_FUNC_AE; }
            set { _nID_CNCL_2_FUNC_AE = value; }
        }
        public bool bCNCL_2_MANUAL_AE
        {
            get { return _bCNCL_2_MANUAL_AE; }
            set { _bCNCL_2_MANUAL_AE = value; }
        }
        public string tDS_CNCL_2_FUNC_AE
        {
            get { return _tDS_CNCL_2_FUNC_AE; }
            set { _tDS_CNCL_2_FUNC_AE = value; }
        }
        public bool bOCASIONAL_INTERM
        {
            get { return _bOCASIONAL_INTERM; }
            set { _bOCASIONAL_INTERM = value; }
        }
        public AtividadeTipo nIND_ATIVIDADES
        {
            get { return _nIND_ATIVIDADES; }
            set { _nIND_ATIVIDADES = value; }
        }
        public TipoExposicaoOcupacional nIND_TIPOEXPOSICAOOCUPACIONAL
        {
            get { return _nIND_TIPOEXPOSICAOOCUPACIONAL; }
            set { _nIND_TIPOEXPOSICAOOCUPACIONAL = value; }
        }
        public bool bPERICULOSIDADE
        {
            get { return _bPERICULOSIDADE; }
            set { _bPERICULOSIDADE = value; }
        }  
        [Obrigatorio(true, "A jornada de trabalho é campo obrigatório")]
        public TempoExposicao nID_TEMPO_EXP
        {
            get { return _nID_TEMPO_EXP; }
            set { _nID_TEMPO_EXP = value; }
        }
        public string tDS_LOCAL_TRAB
        {
            get { return _tDS_LOCAL_TRAB; }
            set { _tDS_LOCAL_TRAB = value; }
        }
        public string tDS_FUNC_EPC_EXTE
        {
            get { return _tDS_FUNC_EPC_EXTE; }
            set { _tDS_FUNC_EPC_EXTE = value; }
        }
        public string tDS_FUNC_EPC_RCM
        {
            get { return _tDS_FUNC_EPC_RCM; }
            set { _tDS_FUNC_EPC_RCM = value; }
        }
        public string tDS_MED_CTL_CAR_ADM
        {
            get { return _tDS_MED_CTL_CAR_ADM; }
            set { _tDS_MED_CTL_CAR_ADM = value; }
        }
        public string tDS_MED_CTL_CAR_EDC
        {
            get { return _tDS_MED_CTL_CAR_EDC; }
            set { _tDS_MED_CTL_CAR_EDC = value; }
        }
        public string tDS_COMPL_DEMAIS_AGENTES
        {
            get { return _tDS_COMPL_DEMAIS_AGENTES; }
            set { _tDS_COMPL_DEMAIS_AGENTES = value; }
        }
        public int nIND_GFIP
        {
            get { return _nIND_GFIP; }
            set { _nIND_GFIP = value; }
        }
        public float nUMID
        {
            get { return _nUMID; }
            set { _nUMID = value; }
        }
        public float nVELOC
        {
            get { return _nVELOC; }
            set { _nVELOC = value; }
        }
        public float nLUX
        {
            get { return _nLUX; }
            set { _nLUX = value; }
        }
        public float nTEMP
        {
            get { return _nTEMP; }
            set { _nTEMP = value; }
        }
        public int tNO_FOTO_FUNC
        {
            get { return _tNO_FOTO_FUNC; }
            set { _tNO_FOTO_FUNC = value; }
        }
        public int nTIPO_ATV_LUX
        {
            get { return _nTIPO_ATV_LUX; }
            set { _nTIPO_ATV_LUX = value; }
        }

        public int nTIPO_ILUMINANCIA
        {
            get { return _nTIPO_ILUMINANCIA; }
            set { _nTIPO_ILUMINANCIA = value; }
        }

        public float nIluminancia
        {
            get { return _nIluminancia; }
            set { _nIluminancia = value; }
        }


        public bool bImp_17_6_2
        {
            get { return _bImp_17_6_2; }
            set { _bImp_17_6_2 = value; }
        }
        public string tNormas_Producao
        {
            get { return _tNormas_Producao; }
            set { _tNormas_Producao = value; }
        }
        public string tModo_Operatorio
        {
            get { return _tModo_Operatorio; }
            set { _tModo_Operatorio = value; }
        }
        public string tExigencia_Tempo
        {
            get { return _tExigencia_Tempo; }
            set { _tExigencia_Tempo = value; }
        }
        public string tDeterminacao_Conteudo
        {
            get { return _tDeterminacao_Conteudo; }
            set { _tDeterminacao_Conteudo = value; }
        }
        public string tRitmo_Trabalho
        {
            get { return _tRitmo_Trabalho; }
            set { _tRitmo_Trabalho = value; }
        }
        public string tConteudo_Tarefa
        {
            get { return _tConteudo_Tarefa; }
            set { _tConteudo_Tarefa = value; }
        }


        public bool bImp_Recomendacoes
        {
            get { return _bImp_Recomendacoes; }
            set { _bImp_Recomendacoes = value; }
        }
        public string tRecomendacoes
        {
            get { return _tRecomendacoes; }
            set { _tRecomendacoes = value; }
        }

        public string Caracterizacao_Processos
        {
            get { return _Caracterizacao_Processos; }
            set { _Caracterizacao_Processos = value; }
        }


        #endregion

        #region Override Metotos

        public override void Validate()
        {
            if (this.tNO_FUNC.Length > 70)
                throw new Exception("O nome do " + this.tNO_FUNC + " não pode ultrapassar de 70 caracteres!");

            base.Validate();
        }

        public override string ToString()
        {
            if (this.mirrorOld == null)
                this.Find();

            return this.tNO_FUNC;
        }

        #endregion

        #region Metodos

        #region GetTipoAtividade

        public string GetTipoAtividade()
        {
            string ret;

            if (this.nIND_ATIVIDADES == AtividadeTipo.ExclusivamenteAdministrativa)
                ret = "Exclusivamente administrativa";
            else if (this.nIND_ATIVIDADES == AtividadeTipo.ExclusivamenteOperacional)
                ret = "Exclusivamente operacional";
            else if (this.nIND_ATIVIDADES == AtividadeTipo.PredominantementeAdministrativa)
                ret = "Predominantemente administrativa";
            else
                ret = "Não Informada";

            return ret;
        }
        #endregion

        #region GFIP

        public int GFIP()
        {
            if (this.nIND_GFIP == (int)CodigoGFIP.PPRA) //Calculado
            {
                ArrayList listPPRA = new PPRA().Find("nID_FUNC=" + this.Id + " AND gINSALUBRE=1");

                foreach (PPRA ppra in listPPRA)
                {
                    if (ppra.AcimaLimiteTolerancia() && !ppra.gNET)
                        return (int)CodigoGFIP.Cod04;
                }

                return (int)CodigoGFIP.Cod00;
            }
            else
                return this.nIND_GFIP;
        }

        public string GetGFIP()
        {
            return GetGFIP(this.GFIP());
        }

        public static string GetGFIP(int codGFIP)
        {
            string ret = string.Empty;

            if (codGFIP == (int)CodigoGFIP.Cod00)
            {
                if (Ilitera.Data.SQLServer.EntitySQLServer.xDB2.IndexOf("Prajna") > 0)
                    ret = "-";
                else
                    ret = "(em branco)";
            }
            else if (codGFIP == (int)CodigoGFIP.Cod01)
                ret = "01";
            else if (codGFIP == (int)CodigoGFIP.Cod02)
                ret = "02";
            else if (codGFIP == (int)CodigoGFIP.Cod03)
                ret = "03";
            else if (codGFIP == (int)CodigoGFIP.Cod04)
                ret = "04";
            else if (codGFIP == (int)CodigoGFIP.Cod05)
                ret = "05";
            else if (codGFIP == (int)CodigoGFIP.Cod06)
                ret = "06";
            else if (codGFIP == (int)CodigoGFIP.Cod07)
                ret = "07";
            else if (codGFIP == (int)CodigoGFIP.Cod08)
                ret = "08";
            else
                ret = string.Empty;

            return ret;
        }

        public static CodigoGFIP GetGFIP(string strGFIP)
        {
            CodigoGFIP ret;

            if (strGFIP.ToLower() == "(em branco)" || strGFIP.ToLower() == "em branco")
                ret = CodigoGFIP.Cod00;
            else if (strGFIP == "01" || strGFIP == "1")
                ret = CodigoGFIP.Cod01;
            else if (strGFIP == "02" || strGFIP == "2")
                ret = CodigoGFIP.Cod02;
            else if (strGFIP == "03" || strGFIP == "3")
                ret = CodigoGFIP.Cod03;
            else if (strGFIP == "04" || strGFIP == "4")
                ret = CodigoGFIP.Cod04;
            else 
                ret = CodigoGFIP.PPRA;

            return ret;
        }

        #endregion

        #region Iluminância Recomendada

        public string GetIluminanciaRecomendada()
        {
            string iluminanciaRecomendada = "";

            if (this.nTIPO_ILUMINANCIA != 0)
            {
                DataSet zDs = new DataSet();

                PPRA_EPI xNHO = new PPRA_EPI();
                zDs = xNHO.Trazer_NHO_11(this.nTIPO_ILUMINANCIA);

                if (zDs.Tables[0].Rows.Count > 0)
                    iluminanciaRecomendada = zDs.Tables[0].Rows[0][4].ToString().Trim() + " E(lux)"; // | " + zDs.Tables[0].Rows[0][3].ToString().Trim() + " Irc/RA";
                else
                    iluminanciaRecomendada = "";


            }
            else
            {
                switch (this.nTIPO_ATV_LUX)
                {
                    case 1:
                        iluminanciaRecomendada = "20 - 30 -50";
                        break;
                    case 2:
                        iluminanciaRecomendada = "50 - 75 - 100";
                        break;
                    case 3:
                        iluminanciaRecomendada = "100 - 150 - 200";
                        break;
                    case 4:
                        iluminanciaRecomendada = "200 - 300 - 500";
                        break;
                    case 5:
                        iluminanciaRecomendada = "500 - 750 - 1000";
                        break;
                    case 6:
                        iluminanciaRecomendada = "1000 - 1500 - 2000";
                        break;
                    case 7:
                        iluminanciaRecomendada = "2000 - 3000 - 5000";
                        break;
                    case 8:
                        iluminanciaRecomendada = "5000 - 7500 - 10000";
                        break;
                    case 9:
                        iluminanciaRecomendada = "10000 - 15000 - 20000";
                        break;
                }
            }
            return iluminanciaRecomendada;
        }
        #endregion

        #region GetFotoParadigma

        public string GetFotoParadigma()
        {
            string path;

            if (this.nID_LAUD_TEC.mirrorOld == null)
                this.nID_LAUD_TEC.Find();

            path = Ilitera.Common.Fotos.PathFoto_Uri(this.nID_LAUD_TEC, this.tNO_FOTO_FUNC);

            if (path.IndexOf(".JPG") >= 0)
            {
                return path.Substring(0, path.IndexOf(".JPG")) + "LOW.JPG";
            }
            else
            {
                return path;
            }
        }

        #endregion

        #region SemAvaliacaoQuantitativa

        public bool IsAgentesQuimicosSemAvaliacaoQuantitativa()
        {
            string where = "nID_FUNC=" + this.Id 
                        + " AND nID_RSC IN(" + (int)Riscos.AgentesQuimicos
                        + ", " + (int)Riscos.AgentesQuimicosB
                        + ", " + (int)Riscos.AgentesQuimicosC
                        + ", " + (int)Riscos.AgentesQuimicosD
                        + ", " + (int)Riscos.AsbestosPoeirasMinerais
                        + ", " + (int)Riscos.ACGIH + ")"
                        + " AND IsNull(tVL_MED, 0) = 0";

            int count = new PPRA().ExecuteCount(where);

            return (count > 0);
        }

        #endregion

        #region IsPossueAvaliacaoQualitativa

        public bool IsPossueAvaliacaoQualitativa()
        {
            string where = "nID_FUNC=" + this.Id
                        + " AND nID_RSC IN (SELECT IdRisco FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.Risco WHERE Qualitativo = 1)";

            int count = new PPRA().ExecuteCount(where);

            return (count > 0);
        }

        #endregion

        #region IsPossueAvaliacaoQuantitativa

        public bool IsPossueAvaliacaoQuantitativa()
        {
            string where = "nID_FUNC=" + this.Id
                        + " AND nID_RSC IN(" + (int)Riscos.AgentesQuimicos
                        + ", " + (int)Riscos.AgentesQuimicosB
                        + ", " + (int)Riscos.AgentesQuimicosC
                        + ", " + (int)Riscos.AgentesQuimicosD
                        + ", " + (int)Riscos.AsbestosPoeirasMinerais
                        + ", " + (int)Riscos.ACGIH + ")";

            int count = new PPRA().ExecuteCount(where);

            return (count > 0);
        }

        #endregion

        #region Classificação Exposições Ocupacionais

        public enum TipoExposicaoOcupacional : int
        {
            Tipo1,
            Tipo2,
            Tipo3,
            Tipo4,
            Tipo5,
            Tipo6
        }

        public string GetStrTipoExposicaoAmbiental()
        {
            return "Tipo " + Convert.ToInt32(this.GetTipoExposicaoAmbiental() + 1).ToString();
        }

        public TipoExposicaoOcupacional GetTipoExposicaoAmbiental()
        {
            List<PPRA> ppras = new PPRA().Find<PPRA>("nID_FUNC=" + this.Id + " ORDER BY nID_RSC");

            bool IsAcima50PorCentoDose = false;

            bool IsInsalubre = false;

            bool IsQuantitativa = this.IsPossueAvaliacaoQuantitativa();

            bool IsQualitativa = this.IsPossueAvaliacaoQualitativa();

            bool IsSituacaoRiscoGraveIminente = false;

            foreach (PPRA ppra in ppras)
            {
                if (ppra.Acima50PorCentoDose())
                    IsAcima50PorCentoDose = true;

                if (ppra.AcimaLimiteTolerancia())
                    IsInsalubre = true;

                if (ppra.IsSituacaoRiscoGraveIminente())
                    IsSituacaoRiscoGraveIminente = true;
            }

            if (IsSituacaoRiscoGraveIminente)
                return TipoExposicaoOcupacional.Tipo6;
            else if (IsInsalubre && IsQualitativa && IsQuantitativa)
                return TipoExposicaoOcupacional.Tipo5;
            else if (IsInsalubre && IsQualitativa)
                return TipoExposicaoOcupacional.Tipo4;
            else if (IsInsalubre && IsQuantitativa)
                return TipoExposicaoOcupacional.Tipo3;
            else if (IsAcima50PorCentoDose)
                return TipoExposicaoOcupacional.Tipo2;
            else
                return TipoExposicaoOcupacional.Tipo1;
        }

        #endregion

        #region MarcaDaquaPPRAEmpregado

        public string GetMarcaDaquaPPRAEmpregado(Cliente cliente)
        {
            StringBuilder str = new StringBuilder();

            return str.ToString();
        }


        #endregion

        #region Conclusao

        public bool IsInsalubre()
        {
            int count = new PPRA().ExecuteCount("nID_FUNC=" + this.Id + " AND gINSALUBRE = 1");

            return (count > 0);
        }



        public bool IsAcimaLimiteTolerancia_Nao_Neutralizado()
        {
            int count = new PPRA().ExecuteCount("nID_FUNC=" + this.Id + " and ( ( nid_rsc=2 and ginsalubre=1 and gNet = 0 ) or ( nid_rsc <> 2 and  tVL_Med > 0 AND tVL_Med >= sVL_Lim_Tol  and gNet = 0  ))  " );

            if (count > 0)
            {
                //verificar EPIs
                if (EPIEficaz_Risco_Neutralizado(this.Id, "nID_FUNC=" + this.Id + " and ( ( nid_rsc=2 and ginsalubre=1 and gNet = 0 ) or ( nid_rsc <> 2 and  tVL_Med > 0 AND tVL_Med >= sVL_Lim_Tol  and gNet = 0  ))  ") == true)
                    return false;
                else
                    return true;
            }
            else
                return false;
        }


        public bool IsAcimaLimiteTolerancia_Neutralizado()
        {
            int count = new PPRA().ExecuteCount("nID_FUNC=" + this.Id + " and tVL_Med > 0 AND tVL_Med >= sVL_Lim_Tol  ");

            if (count > 0)
            {
                //verificar EPIs
                if (EPIEficaz_Risco_Neutralizado(this.Id, "nID_FUNC=" + this.Id + " and tVL_Med > 0 AND tVL_Med >= sVL_Lim_Tol  ") == true)
                    return true;
                else
                    return false;
            }
            else
                return false;
        }


        public bool EPIEficaz_Risco_Neutralizado(Int32 nIdGHE, string nFiltro)
        {
            bool Eficaz = true;

            ArrayList listPPRA = new PPRA().Find(nFiltro);


            foreach (PPRA xRisco in listPPRA)
            {
                if (xRisco.gNET == false)
                {
                    ArrayList listEpiRCM = new EpiRecomendado().Find("nID_PPRA=" + xRisco.Id);
                    ArrayList listEpiEXTE = new EpiExistente().Find("nID_PPRA=" + xRisco.Id);
                    if (listEpiRCM.Count > 0)
                    {
                        if (listEpiRCM.Count != listEpiEXTE.Count)
                        {
                            Eficaz = false;
                        }
                    }
                    else
                    {
                        Eficaz = false;
                    }

                }

            }

            return Eficaz;
        }



        public bool IsRuidoAbaixoLimiteTolerancia()
        {

            int count = new PPRA().ExecuteCount("nID_FUNC=" + this.Id + " and nId_RSC = 0  AND isnull(tVL_Med_Ruido_LTCAT, tVL_Med ) < sVL_Lim_Tol and isnull(tVL_Med_Ruido_LTCAT, tVL_Med ) >= 80 ");

            if (count == 0)
                return false;
            else
            {
                return true;
            }

        }

        public bool IsAvaliacoesQuantitativasAbaixoLimiteTolerancia()
        {

            int count = new PPRA().ExecuteCount("nID_FUNC=" + this.Id + " and nId_RSC <> 0 and tVL_Med > 0 AND tVL_Med < sVL_Lim_Tol ");

            if (count == 0)
                return false;
            else
            {
                return true;
            }

        }


        public bool IsRuidoAcimaLimiteTolerancia_Neutralizado()
        {

            int count = new PPRA().ExecuteCount("nID_FUNC=" + this.Id + " and nId_RSC = 0  AND isnull(tVL_Med_Ruido_LTCAT, tVL_Med ) >= sVL_Lim_Tol ");

            if (count == 0)
                return false;
            else
            {
                //verificar EPIs
                if (EPIEficaz_Risco_Neutralizado(this.Id, "nID_FUNC=" + this.Id + " and nId_RSC = 0  AND isnull(tVL_Med_Ruido_LTCAT, tVL_Med ) >= sVL_Lim_Tol  ") == true)
                    return true;
                else
                    return false;
            }

        }


        public bool IsNeutralizado()
        {
            int count = new PPRA().ExecuteCount("nID_FUNC=" + this.Id + " AND gINSALUBRE = 1 AND gNET = 0");

            return (count == 0);
        }

        private static string ConclusaoCodigoGfip04 //Galvani
        {
            get
            {
                return "Diante da análise técnica procedida, conclui-se que a exposição habitual e permanente aos agentes nocivos avaliados caracteriza nocividade e/ou insalubridade, não se podendo afirmar com rigor científico, com fundamento nos conhecimentos técnicos vigentes, que o uso obrigatório dos equipamentos de proteção individual fornecidos rigorosamente pela empresa, em conformidade com as normas legais trabalhistas, conferiu total proteção em relação à nocividade dos referidos agentes, de modo a evitar a ocorrência de exposições ambientais que tipificam condições especiais de trabalho e ensejam a concessão de aposentadoria especial, sendo ainda importante ressaltar, em relação à exposição ao ruído, o entendimento exarado na Súmula da Jurisprudência Predominante nº 9 aprovada pelo Conselho da Justiça Federal, que dispõe “o uso de equipamento de proteção individual - EPI, ainda que elimine a insalubridade, no caso de exposição ao ruído, não descaracteriza o tempo de serviço especial prestado” (Turma de Uniformização - julgamento 30/09/03 - Brasília 13/10/03 - Ministro Ari Pargendler - Presidente da Turma de Uniformização - DJU 05/11/03 P. 551).";
            }
        }

        public static string ConclusaoElektroExcluivamenteAdministrativa
        {
            get
            {
                return "Diante da análise técnica procedida, alcança-se a conclusão de que a presente exposição ocupacional se enquadra no subitem 9.1.2.1, da NR 9, pois não foram identificados riscos ambientais nas fases de antecipação ou reconhecimento que ultrapassassem níveis de ação, motivo pelo qual, o PPRA se resumiu às citadas fases/etapas, acrescidas do registro e divulgação dos dados, que, nesse caso, foi realizado através de inserção em sistemas informatizados, para se constituir em histórico técnico-administrativo de seu desenvolvimento, com acesso, impressão e divulgação imediata ao trabalhador interessado.";
            }
        }

        public static string ConclusaoElektroPredominantementeAdmSemPericulosidade
        {
            get
            {
                return "Diante da análise técnica procedida, se conclui que a presente exposição ocupacional a agentes ambientais descritos na NR 15 se encontra sem nocividade, sequer alcançando níveis de ação, se caracterizando como salubre por sua natureza, intensidade, concentrações, condições, métodos e tempos de trabalho, sendo importante ressaltar que, em trabalhos externos, exercidos de forma secundária, não tem o dever funcional de ingressar em áreas de risco de eletricidade para o cumprimento de suas obrigações laborais.";
            }
        }

        public static string ConclusaoElektroPredominantementeAdmComPericulosidade
        {
            get
            {
                return "Realizar atividades laborais predominantemente administrativas e operacional, de forma secundária, que, em síntese, consistem, respectivamente, em implantar a supervisão de religadores via celular; realizar digitalizações, gestões de processos e manutenção de sistema de automação; configurar modens; realizar ensaios e comissionamentos de religadores, bem como realizar atividades de fiscalização e inspeção de instalações elétricas energizadas ou não, com ingresso, de forma intermitente, em áreas de risco de eletricidade, com possibilidade de energização, caracterizadas como perigosas. Na fase de reconhecimento não foram identificados riscos ambientais prescritos na NR 15 cuja intensidade ou concentração tivessem alcançado níveis de ação de que trata o subitem 9.3.6, da NR 9.";
            }
        }

        public static string ConclusaoElektroExclusivamenteOperacional
        {
            get
            {
                return "Realizar atividades laborais exclusivamente operacional que, em síntese, consistem em realizar atividades de inspeção, manutenção e manobras em redes de distribuição em alta e baixa tensão, energizadas ou com possibilidade de energização, com ingresso, de forma habitual e/ou permanente, em áreas de risco de eletricidade cujas atividades são caracterizadas como perigosas, sendo que a exposição ao calor, nas atividades a céu aberto, é predominantemente oriunda de radiações solares, e não de fontes artificiais.";
            }
        }

        private static string ConclusaoParte1
        {
            get
            {
                return "A exposição aos agentes nocivos e o exercício da atividade ocorreram de modo habitual e permanente, de forma não ocasional e não intermitente sendo que os serviços prestados foram exercidos exclusivamente durante a jornada integral de trabalho.";
            }
        }

        private static string ConclusaoParte1_2
        {
            get
            {
                return "A exposição aos agentes nocivos e o exercício da atividade ocorreram de modo habitual e permanente, de forma ocasional e intermitente sendo que os serviços prestados foram exercidos exclusivamente durante a jornada integral de trabalho.";
            }
        }


        private string ConclusaoParte2(bool bInsalubre, bool bNeutralizado)
        {
            StringBuilder str = new StringBuilder();

            if (bInsalubre && bNeutralizado)
                str.Append("Em consequência da avaliação realizada, conclui-se que a efetiva exposição aos agentes nocivos avaliados, neutralizada pelo uso de Equipamentos de Proteção Individual, com fundamento no art. 191, II, da CLT, e na NR 6, item 6.5.1, não é prejudicial à saúde ou integridade física do trabalhador, sendo que a atividade realizada se caracteriza como salubre, por sua natureza, intensidade, condições e métodos de trabalho, bem como ao tempo de exposição aos seus efeitos.");
            else if (bInsalubre && !bNeutralizado)
                str.Append("Em conseqüência da avaliação realizada, conclui-se que a efetiva exposição ao agente nocivo, ou associação de agentes, não foi neutralizada pela adoção de tecnologia ou equipamento de proteção coletiva ou individual, sendo prejudicial à saúde ou integridade física do trabalhador, haja vista que a atividade realizada se caracteriza como insalubre, por sua natureza, intensidade, condições e métodos de trabalho, bem como ao tempo de exposição aos seus efeitos.");
            else if (!bInsalubre && bNeutralizado)
                str.Append("Em conseqüência da avaliação realizada, conclui-se que a efetiva exposição ao agente nocivo, ou associação de agentes, não é prejudicial à saúde ou integridade física do trabalhador, haja vista que a atividade realizada se caracteriza como salubre, por sua natureza, intensidade, condições e métodos de trabalho, bem como ao tempo de exposição aos seus efeitos.");
            else if (!bInsalubre && !bNeutralizado)
                str.Append("Em conseqüência da avaliação realizada, conclui-se que a efetiva exposição ao agente nocivo, ou associação de agentes, não é prejudicial à saúde ou integridade física do trabalhador, haja vista que a atividade realizada se caracteriza como salubre, por sua natureza, intensidade, condições e métodos de trabalho, bem como ao tempo de exposição aos seus efeitos.");

            return str.ToString();
        }

        public string Conclusao(Cliente cliente)
        {
            if (tDS_CNCL_FUNC != string.Empty)
                return tDS_CNCL_FUNC;
            else
                return Conclusao(cliente, this.IsInsalubre(), this.IsNeutralizado());
        }

        public string Conclusao(Cliente cliente, bool bInsalubre, bool bNeutralizado)
        {
            StringBuilder str = new StringBuilder();
            string zModos = "";

            if (bNeutralizado && this.nIND_GFIP == (int)CodigoGFIP.Cod04)//Galvani
                str.Append(ConclusaoCodigoGfip04);
            else
            {
                ArrayList list = new PPRA().Find("nID_FUNC=" + this.Id);


                Boolean zLoc = false;
                foreach (PPRA zAgente in list)
                {
                    if (zAgente.nIND_MOD_EXP == (int)Ilitera.Opsa.Data.PPRA.ModoExposicao.OcasionalIntermitente)
                    {
                        if (zModos.IndexOf("Ocasional e Intermitente,") < 0)
                        {
                            zModos = zModos + "Ocasional e Intermitente, ";
                            zLoc = true;
                        }
                    }
                    else if (zAgente.nIND_MOD_EXP == (int)Ilitera.Opsa.Data.PPRA.ModoExposicao.Permanente)
                    {
                        if (zModos.IndexOf("Permanente,") < 0)
                        {
                            zModos = zModos + "Permanente, ";
                            zLoc = true;
                        }
                    }
                    else if (zAgente.nIND_MOD_EXP == (int)Ilitera.Opsa.Data.PPRA.ModoExposicao.Eventual)
                    {
                        if (zModos.IndexOf("Eventual,") < 0)
                        {
                            zModos = zModos + "Eventual, ";
                            zLoc = true;
                        }
                    }
                    else if (zAgente.nIND_MOD_EXP == (int)Ilitera.Opsa.Data.PPRA.ModoExposicao.HabitualPermanente)
                    {
                        if (zModos.IndexOf("Habitual e Permanente,") < 0)
                        {
                            zModos = zModos + "Habitual e Permanente, ";
                            zLoc = true;
                        }
                    }
                    else if (zAgente.nIND_MOD_EXP == (int)Ilitera.Opsa.Data.PPRA.ModoExposicao.HabitualIntermitente)
                    {
                        if (zModos.IndexOf("Habitual e Intermitente,") < 0)
                        {
                            zModos = zModos + "Habitual e Intermitente, ";
                            zLoc = true;
                        }
                    }

                }

                if (zLoc == true)
                {
                    zModos = zModos.Substring(0, zModos.Length - 2);
                    str.Append(ConclusaoParte1_2.Replace("ocorreram de modo habitual e permanente, de forma ocasional e intermitente ", "ocorreram de modo " + zModos + ", "));
                }
                else
                {
                    str.Append(ConclusaoParte1);
                }

                str.Append(" ");
                str.Append(ConclusaoParte2(bInsalubre, bNeutralizado));
            }

            return str.ToString();
        }

        public string Conclusao_LTCAT(Cliente cliente)
        {
            if (tDS_CNCL_FUNC != string.Empty)
                return tDS_CNCL_FUNC;
            else
            {
                return Conclusao_LTCAT(cliente, this.IsAcimaLimiteTolerancia_Nao_Neutralizado(), this.IsRuidoAcimaLimiteTolerancia_Neutralizado(), this.IsAcimaLimiteTolerancia_Neutralizado(), this.IsRuidoAbaixoLimiteTolerancia(), this.IsAvaliacoesQuantitativasAbaixoLimiteTolerancia());
            }
        }


        public string Conclusao_LTCAT(Cliente cliente, bool bIsAcimaLimiteTolerancia_Nao_Neutralizado, bool bIsRuidoAcimaLimiteTolerancia_Neutralizado, bool bIsAcimaLimiteTolerancia_Neutralizado, bool bIsRuidoAbaixoLimiteTolerancia, bool bIsAvaliacoesQuantitativasAbaixoLimiteTolerancia)
        {
            StringBuilder str = new StringBuilder();
            string zModos = "";


            ArrayList list = new PPRA().Find("nID_FUNC=" + this.Id);


            Boolean zLoc = false;
            foreach (PPRA zAgente in list)
            {
                if (zAgente.nIND_MOD_EXP == (int)Ilitera.Opsa.Data.PPRA.ModoExposicao.OcasionalIntermitente)
                {
                    if (zModos.IndexOf("Ocasional e Intermitente,") < 0)
                    {
                        zModos = zModos + "Ocasional e Intermitente, ";
                        zLoc = true;
                    }
                }
                else if (zAgente.nIND_MOD_EXP == (int)Ilitera.Opsa.Data.PPRA.ModoExposicao.Permanente)
                {
                    if (zModos.IndexOf("Permanente,") < 0)
                    {
                        zModos = zModos + "Permanente, ";
                        zLoc = true;
                    }
                }
                else if (zAgente.nIND_MOD_EXP == (int)Ilitera.Opsa.Data.PPRA.ModoExposicao.Eventual)
                {
                    if (zModos.IndexOf("Eventual,") < 0)
                    {
                        zModos = zModos + "Eventual, ";
                        zLoc = true;
                    }
                }
                else if (zAgente.nIND_MOD_EXP == (int)Ilitera.Opsa.Data.PPRA.ModoExposicao.HabitualPermanente)
                {
                    if (zModos.IndexOf("Habitual e Permanente,") < 0)
                    {
                        zModos = zModos + "Habitual e Permanente, ";
                        zLoc = true;
                    }
                }
                else if (zAgente.nIND_MOD_EXP == (int)Ilitera.Opsa.Data.PPRA.ModoExposicao.HabitualIntermitente)
                {
                    if (zModos.IndexOf("Habitual e Intermitente,") < 0)
                    {
                        zModos = zModos + "Habitual e Intermitente, ";
                        zLoc = true;
                    }
                }

            }

            if (zLoc == true)
            {
                zModos = zModos.Substring(0, zModos.Length - 2);
                str.Append(ConclusaoParte1_2.Replace("ocorreram de modo habitual e permanente, de forma ocasional e intermitente ", "ocorreram de modo " + zModos + ", "));
            }
            else
            {
                str.Append(ConclusaoParte1);
            }


            str.Append(" ");
            str.Append(ConclusaoParte2_LTCAT(bIsAcimaLimiteTolerancia_Nao_Neutralizado, bIsRuidoAcimaLimiteTolerancia_Neutralizado, bIsAcimaLimiteTolerancia_Neutralizado, bIsRuidoAbaixoLimiteTolerancia, bIsAvaliacoesQuantitativasAbaixoLimiteTolerancia));


            return str.ToString();
        }


        private string ConclusaoParte2_LTCAT(bool bIsAcimaLimiteTolerancia_Nao_Neutralizado, bool bIsRuidoAcimaLimiteTolerancia_Neutralizado, bool bIsAcimaLimiteTolerancia_Neutralizado, bool bIsRuidoAbaixoLimiteTolerancia, bool bIsAvaliacoesQuantitativasAbaixoLimiteTolerancia)
        {
            StringBuilder str = new StringBuilder();


            if (bIsAcimaLimiteTolerancia_Nao_Neutralizado)
                str.Append("Em consequência da avaliação realizada, conclui-se que a efetiva exposição ao agente nocivo, ou associação de agentes, não foi neutralizada pela adoção de tecnologia ou equipamento de proteção coletiva ou individual, sendo prejudicial à saúde ou integridade física do trabalhador, haja vista que a atividade realizada se caracteriza como nociva, por sua natureza, intensidade, condições e métodos de trabalho, bem como ao tempo de exposição aos seus efeitos (permanência).");
            else if (bIsRuidoAcimaLimiteTolerancia_Neutralizado)
                str.Append("Em consequência da avaliação realizada, conclui-se que a efetiva exposição aos agentes nocivos avaliados, neutralizada pelo uso de Equipamentos de Proteção Individual, com fundamento no art. 191, II, da CLT, e na NR 6, item 6.5.1, não é prejudicial à saúde ou integridade física do trabalhador, sendo que a atividade é realizada sem nocividade, por sua natureza, intensidade, condições e métodos de trabalho, bem como ao tempo de exposição aos seus efeitos. No entanto, em relação ao agente nocivo ruído, o STF - Supremo Tribunal Federal decidiu que em nenhuma hipótese o EPI descaracterizará o direto da aposentadoria especial, em conformidade com o Tema nº 555 na Suprema Corte 'intitulada como Fornecimento de Equipamento de Proteção Individual - EPI como Fator de Descaracterização do Tempo de Serviço Especial'.");
            else if (bIsAcimaLimiteTolerancia_Neutralizado || bIsRuidoAbaixoLimiteTolerancia)
                str.Append("Em consequência da avaliação realizada, conclui-se que a efetiva exposição aos agentes nocivos avaliados, neutralizada pelo uso de Equipamentos de Proteção Individual, com fundamento no art. 191, II, da CLT, e na NR 6, item 6.5.1, não é prejudicial à saúde ou integridade física do trabalhador, sendo que a atividade é realizada sem nocividade, por sua natureza, intensidade, condições e métodos de trabalho, bem como ao tempo de exposição aos seus efeitos.");
            else if (bIsAvaliacoesQuantitativasAbaixoLimiteTolerancia)
                str.Append("Em consequência da avaliação realizada, conclui-se que a efetiva exposição ao agente nocivo, ou associação de agentes, não é prejudicial à saúde ou integridade física do trabalhador, haja vista que a atividade é realizada sem nocividade, por sua natureza, intensidade, condições e métodos de trabalho, bem como ao tempo de exposição aos seus efeitos.");
            else
                str.Append("Em consequência da avaliação realizada, conclui-se que a efetiva exposição ao agente nocivo, ou associação de agentes, não é prejudicial à saúde ou integridade física do trabalhador, haja vista que a atividade é realizada sem nocividade, por sua natureza, intensidade, condições e métodos de trabalho, bem como ao tempo de exposição aos seus efeitos.");

            return str.ToString();
        }




        public string ConclusaoAEParte1()
        {
            StringBuilder str = new StringBuilder();

            str.Append(ConclusaoParte1);

            return str.ToString();
        }

        public string ConclusaoAEParte2()
        {
            return this.ConclusaoAEParte2(this.IsInsalubre(), this.IsNeutralizado());
        }

        public string ConclusaoAEParte2(bool bInsalubre, bool bNeutralizado)
        {
            StringBuilder str = new StringBuilder();

            if (this.bCNCL_2_MANUAL_AE)//Manual
            {
                str.Append(this.tDS_CNCL_2_FUNC_AE);
            }
            else
            {
                if (bNeutralizado && this.nIND_GFIP == (int)CodigoGFIP.Cod04)
                {
                    str.Append(ConclusaoCodigoGfip04);
                }
                else
                {
                    //???? Não faz sentido (verificar)
                    if (this.bOCASIONAL_INTERM)
                        str.Append(ConclusaoParte2(false, true));
                    else
                        str.Append(ConclusaoParte2(bInsalubre, bNeutralizado));
                }
            }

            return str.ToString();
        }

        public string ConclusaoAE()
        {
            return this.ConclusaoAE(this.IsInsalubre(), this.IsNeutralizado());
        }

        public string ConclusaoAE(bool bInsalubre, bool bNeutralizado)
        {
            StringBuilder str = new StringBuilder();

            str.Append(ConclusaoAEParte1());
            str.Append(" ");
            str.Append(ConclusaoAEParte2(bInsalubre, bNeutralizado));

            return str.ToString();
        }

        #endregion

        #region Equipamentos

        public string Equipamentos()
        {
            if (this.nID_LAUD_TEC.mirrorOld == null)
                this.nID_LAUD_TEC.Find();

            return Equipamentos(this.nID_LAUD_TEC.hDT_LAUDO);
        }

        public string Equipamentos(DateTime dataLaudo)
        {
            string str = "IdEquipamentoMedicao IN (SELECT nID_EQP_MED FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblPPRA1 WHERE nID_FUNC=" + this.Id + ")";

            ArrayList list = new EquipamentoMedicao().Find(str);

            return EquipamentoMedicao.GetListaEquipamentoMedicao(list, dataLaudo);
        }

        public string EquipamentosHTML(DateTime dataLaudo, bool zFonte_Artificial)
        {
            StringBuilder str = new StringBuilder();
            string xRisco = "";

            ArrayList list = new EquipamentoMedicao().Find("IdEquipamentoMedicao IN"
                + " (SELECT nID_EQP_MED FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblPPRA1 WHERE"
                + "  nID_FUNC=" + this.Id + ")");

            foreach (EquipamentoMedicao equip in list)
            {
                str.Append("<p align='justify'><Font color='navy'>");

                equip.IdRisco.Find();

                if (equip.Descricao.Trim() == "") xRisco = equip.Nome.Trim();
                else xRisco = equip.Descricao.Trim();

                if (equip.Nome != "Vide Laudo" && str.ToString().IndexOf(xRisco) == -1)
                {
                    if (equip.IdRisco.Id == 0 && equip.IdAgenteQuimico.Id != 0)
                    {
                        equip.IdAgenteQuimico.Find();
                        str.Append("<b>");
                        str.Append(equip.IdAgenteQuimico.Nome);
                        str.Append("</b> - ");
                    }
                    else if (equip.IdRisco.Id == 0 && equip.IdAgenteQuimico.Id == 0)
                    {
                        str.Append("<b>Ruído Contínuo</b> - ");
                    }
                    else if (equip.IdRisco.Id == (int)Riscos.Calor)
                    {
                        str.Append("<b>");
                        str.Append(equip.IdRisco.DescricaoResumido);
                        str.Append("</b> - ");

                        str.Append(xRisco);

                        if (zFonte_Artificial == true)
                        {
                            str.Append("<b>");
                            str.Append(" ausência de fontes artificiais");
                            str.Append("</b>. Observância NHO-06 Fundacentro");
                        }
                        else
                        {
                            str.Append(" Observância NHO-06 Fundacentro");
                        }
                    }
                    else
                    {
                        if (equip.IdRisco.DescricaoResumido != null)
                        {
                            str.Append("<b>");
                            str.Append(equip.IdRisco.DescricaoResumido);
                            str.Append("</b> - ");
                        }
                    }

                    if (str.ToString().IndexOf(xRisco) == -1)
                        str.Append(equip.Descricao.Replace("DATA_CALIBRACAO", equip.GetDataCalibracao(dataLaudo)));
                }
                str.Append("</Font></p><br>");
            }
            return str.ToString();
        }

        #endregion

        #region PPRA

        #region FuncoesIntegrantes
        public string FuncoesIntegrantes()
        {
            StringBuilder str = new StringBuilder();

            string where = "IdFuncao IN (SELECT nID_FUNCAO FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEMPREGADO_FUNCAO WHERE"
                + " nID_EMPREGADO_FUNCAO IN (SELECT nID_EMPREGADO_FUNCAO FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblFUNC_EMPREGADO WHERE"
                + " nID_FUNC=" + this.Id
                + ")) ORDER BY NomeFuncao";

            List<Funcao> funcoes = new Funcao().Find<Funcao>(where);

            foreach (Funcao funcao in funcoes)
                str.Append(funcao.NomeFuncao + ", ");

            if (str.Length > 0)
                return str.ToString(0, str.Length - 2);
            else
                return "-";
        }

        public string FuncoesIntegrantes_Ativas_Apenas()
        {
            StringBuilder str = new StringBuilder();

            string where = "IdFuncao IN (SELECT nID_FUNCAO FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEMPREGADO_FUNCAO WHERE HDT_Termino is null and "
                + " nID_EMPREGADO_FUNCAO IN (SELECT nID_EMPREGADO_FUNCAO FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblFUNC_EMPREGADO WHERE"
                + " nID_FUNC=" + this.Id
                + "  )) ORDER BY NomeFuncao";

            List<Funcao> funcoes = new Funcao().Find<Funcao>(where);

            if (funcoes.Count >= 5)
            {
                foreach (Funcao funcao in funcoes)
                    str.Append(funcao.NomeFuncao + ", ");

                if (str.Length > 0)
                    return str.ToString(0, str.Length - 2);
                else
                    return "-";
            }
            else
            {
                foreach (Funcao funcao in funcoes)
                    str.Append(funcao.NomeFuncao + System.Environment.NewLine);

                if (str.Length > 0)
                    return str.ToString(0, str.Length - 1);
                else
                    return "-";

            }
        }

        #endregion

        #region RiscosOcupacionais
        public string RiscosOcupacionais()
        {
            StringBuilder str = new StringBuilder();

            List<PPRA> listPPRA = this.GetListaRiscosPPRA();

            foreach (PPRA ppra in listPPRA)
                str.Append(ppra.GetNomeAgente() + "\n");

            if (listPPRA.Count == 0)
                str.Append("Risco ocupacional inexistente"); // "Sem risco ocupacional específico");

            return str.ToString();
        } 
        #endregion

        #region RiscosAmbientaisAso

        private string strRiscosAmbientaisAso = string.Empty;

        public string RiscosAmbientaisAso()
        {
            if (strRiscosAmbientaisAso != string.Empty)
                return strRiscosAmbientaisAso;

            StringBuilder str = new StringBuilder();
            str.Append("nID_FUNC=" + this.Id);
            str.Append(" AND gREMOVE_PCMSO = 0");
            str.Append(" AND (gINSALUBRE = 0 OR nID_RSC=" + (int)Riscos.RuidoContinuo + ")");
            str.Append(" ORDER BY nID_RSC");

            ArrayList listPPRA = new PPRA().Find(str.ToString());

            StringBuilder ret = new StringBuilder();

            foreach (PPRA ppra in listPPRA)
                ret.Append(ppra.GetNomeAgente() + "\n");

            if (listPPRA.Count == 0)
                ret.Append("Risco ocupacional inexistente");  // "Sem risco ocupacional específico");

            strRiscosAmbientaisAso = ret.ToString();

            return strRiscosAmbientaisAso;
        }
        #endregion

        #region RiscosOcupacionaisAso

        private string strRiscosOcupacionaisAso = string.Empty;

        public string RiscosOcupacionaisAso( bool Exibir_Intensidade, bool Exibir_Dispensado, bool Exibir_Riscos_PPRA)
        {
            if (strRiscosOcupacionaisAso != string.Empty)
            {
                if (strRiscosOcupacionaisAso.IndexOf("\r\n\r\n\r\n**Dis") > 0)
                    return strRiscosOcupacionaisAso.Replace("\r\n\r\n**Dis", "**Dis");
                else if (strRiscosOcupacionaisAso.IndexOf("\r\n\r\n**Dis") > 0)
                    return strRiscosOcupacionaisAso.Replace("\r\n**Dis", "**Dis");
                else if (strRiscosOcupacionaisAso.IndexOf("\r\n\r\n\r\n*Dis") > 0)
                    return strRiscosOcupacionaisAso.Replace("\r\n\r\n*Dis", "*Dis");
                else if (strRiscosOcupacionaisAso.IndexOf("\r\n\r\n*Dis") > 0)
                    return strRiscosOcupacionaisAso.Replace("\r\n*Dis", "*Dis");
                else if (strRiscosOcupacionaisAso.IndexOf("\r\n\r\n\r\nDis") > 0)
                    return strRiscosOcupacionaisAso.Replace("\r\n\r\nDis", "Dis");
                else if (strRiscosOcupacionaisAso.IndexOf("\r\n\r\nDis") > 0)
                    return strRiscosOcupacionaisAso.Replace("\r\nDis", "Dis");
                else
                    return strRiscosOcupacionaisAso;
            }
            //return strRiscosOcupacionaisAso;



            StringBuilder ret = new StringBuilder();
            string quebraLinha;
            int zAux = 0;
            Boolean xFrase = false;

            string where2 = "nID_FUNC=" + this.Id + " AND gREMOVE_PCMSO = 0 and nId_Rsc in ( 0, 2 ) and gInsalubre = 0 "
                        + " ORDER BY nID_RSC";


            List<PPRA> listPPRA2 = new PPRA().Find<PPRA>(where2);

            quebraLinha = "\n";

            foreach (PPRA ppra in listPPRA2)
            {

                //LaudoTecnico xLaudo = new LaudoTecnico();
                //xLaudo.Find(" nId_laud_tec = " + ppra.nID_LAUD_TEC.ToString() + " ");                

                //if (xLaudo.nID_EMPR.Exibir_Riscos_ASO == true)
                //{

                    if (!(ppra.AcimaLimiteTolerancia()
                       || ppra.Acima50PorCentoDose()))
                    {

                        if (ppra.nID_RSC.ToString().ToUpper().IndexOf("CALOR") >= 0)
                        {
                            //ret.Append("**" + ppra.GetNomeAgente() + "  " + ppra.tVL_MED + " ºC IBUTG " + quebraLinha);
                            if ( Exibir_Intensidade == true )
                               ret.Append("**" + ppra.nID_RSC.ToString() + "  " + ppra.tVL_MED + " ºC IBUTG " + quebraLinha);
                            else
                                ret.Append("**" + ppra.nID_RSC.ToString() + "  " + quebraLinha);
                        }
                        else
                        {
                            //ret.Append("**" + ppra.GetNomeAgente() + "  " + ppra.tVL_MED + " dB " + quebraLinha);
                            if (ppra.tVL_MED > 0)
                            {
                                if ( Exibir_Intensidade == true )
                                   ret.Append("**" + ppra.nID_RSC.ToString() + "  " + ppra.tVL_MED + " dB " + quebraLinha);
                                else
                                    ret.Append("**" + ppra.nID_RSC.ToString() + "  " + quebraLinha);
                            }
                            else if (ppra.tVL_MED == 0)
                            {
                                ret.Append("**" + ppra.nID_RSC.ToString() + "  " + quebraLinha);
                            }
                        }


                        xFrase = true;
                        zAux++;
                    }

                //}
            }



            string where = "nID_FUNC=" + this.Id
                        + " AND gREMOVE_PCMSO = 0"
                        + " ORDER BY nID_RSC"
                        + " , (SELECT Nome FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.AgenteQuimico WHERE IdAgenteQuimico = nID_AG_NCV)";

            List<PPRA> listPPRA = new PPRA().Find<PPRA>(where);

            

            if (listPPRA.Count < ( 10 - zAux ))
                quebraLinha = "\n";
            else
                quebraLinha = "; ";


            


            foreach (PPRA ppra in listPPRA)
            {
                ppra.nID_RSC.Find();

                if (ppra.AcimaLimiteTolerancia()
                 || ppra.Acima50PorCentoDose()
                 || ppra.nID_RSC.ToString().IndexOf("Anexo 13") >= 0
                 || ppra.Inserir_no_ASO())
                {
                    string xTexto = ppra.GetNomeAgente();

                    if (ret.ToString().IndexOf(xTexto) < 0)
                    {
                        ret.Append(" " + ppra.GetNomeAgente() + quebraLinha);
                        zAux++;
                    }

                }
            }

            if (ret.Length > quebraLinha.Length)
                ret.Remove(ret.ToString().Length - quebraLinha.Length, quebraLinha.Length);


            string xRiscoPPRA = "";

            if (Exibir_Riscos_PPRA == true)
            {
                string where3 = "nID_FUNC=" + this.Id
                            + " ORDER BY nID_RSC";

                List<PPRA> listPPRA3 = new PPRA().Find<PPRA>(where3);



                if (listPPRA.Count < (10 - zAux))
                    quebraLinha = "\n";
                else
                    quebraLinha = "; ";
                                               

                foreach (PPRA ppra in listPPRA3)
                {

                    ppra.nID_AG_NCV.Find();

                    if (ppra.nID_AG_NCV.Nome.ToUpper().Trim() != "AUSÊNCIA DE FATOR DE RISCO")
                    {


                        ppra.nID_RSC.Find();

                        if (ppra.nID_RSC.IndRiscoTipo == RiscoTipo.Fisico)
                        {
                            if (xRiscoPPRA.ToString().IndexOf("Risco Físico") < 0) xRiscoPPRA = xRiscoPPRA + " Risco Físico /";
                        }
                        else if (ppra.nID_RSC.IndRiscoTipo == RiscoTipo.Biologico)
                        {
                            if (xRiscoPPRA.ToString().IndexOf("Risco Biológico") < 0) xRiscoPPRA = xRiscoPPRA + " Risco Biológico /";
                        }
                        else if (ppra.nID_RSC.IndRiscoTipo == RiscoTipo.Quimico)
                        {
                            if (xRiscoPPRA.ToString().IndexOf("Risco Químico") < 0) xRiscoPPRA = xRiscoPPRA + " Risco Químico /";
                        }
                        else if (ppra.nID_RSC.IndRiscoTipo == RiscoTipo.Acidentes)
                        {
                            if (Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper().IndexOf("GLOBAL") < 0)
                            {
                                if (ppra.gREMOVE_PCMSO == false && xRiscoPPRA.ToString().IndexOf("Risco Acidente") < 0) xRiscoPPRA = xRiscoPPRA + "Risco Acidentes " + System.Environment.NewLine;
                            }
                            else
                            {
                                if (ppra.gREMOVE_PCMSO == false && xRiscoPPRA.ToString().ToUpper().IndexOf(ppra.mDS_FRS_RSC.ToUpper()) < 0) xRiscoPPRA = xRiscoPPRA + ppra.mDS_FRS_RSC + " " + System.Environment.NewLine;
                            }
                        }
                        else if (ppra.nID_RSC.IndRiscoTipo == RiscoTipo.Ergonomico)
                        {
                            if (Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper().IndexOf("GLOBAL") < 0)
                            {
                                if (ppra.gREMOVE_PCMSO == false && xRiscoPPRA.ToString().IndexOf("Risco Ergonômico") < 0) xRiscoPPRA = xRiscoPPRA + "Risco Ergonômico " + System.Environment.NewLine;
                            }
                            else
                            {
                                if (ppra.gREMOVE_PCMSO == false && xRiscoPPRA.ToString().ToUpper().IndexOf(ppra.mDS_FRS_RSC.ToUpper()) < 0) xRiscoPPRA = xRiscoPPRA + ppra.mDS_FRS_RSC + " " + System.Environment.NewLine;
                            }

                        }

                    }
                }

                if (xRiscoPPRA != "")
                {
                    xRiscoPPRA = "(" + xRiscoPPRA.Substring(0, xRiscoPPRA.Length - 1) + ")";
                    xRiscoPPRA = quebraLinha + xRiscoPPRA;
                }


                ret.Append(xRiscoPPRA);
            }


            if (listPPRA.Count == 0 && xRiscoPPRA == "")
                ret.Append("Risco ocupacional inexistente"); // "Sem risco ocupacional específico");



            if (Exibir_Dispensado == false)
            {
                if (xFrase == true)
                {
                    for (int zCont = 10 - zAux; zCont >= 0; zCont--)
                    {
                        ret.Append(System.Environment.NewLine);
                    }
                    ret.Append("**Dispensado de exames complementares");
                }
            }
            else
            {
                ret.Replace("**", "");
            }


            strRiscosOcupacionaisAso = ret.ToString();

            if (strRiscosOcupacionaisAso.IndexOf("\r\n\r\n\r\n**Dis") > 0)
                return strRiscosOcupacionaisAso.Replace("\r\n\r\n**Dis", "**Dis");
            else if (strRiscosOcupacionaisAso.IndexOf("\r\n\r\n**Dis") > 0)
                return strRiscosOcupacionaisAso.Replace("\r\n**Dis", "**Dis");
            else if (strRiscosOcupacionaisAso.IndexOf("\r\n\r\n\r\n*Dis") > 0)
                return strRiscosOcupacionaisAso.Replace("\r\n\r\n*Dis", "*Dis");
            else if (strRiscosOcupacionaisAso.IndexOf("\r\n\r\n*Dis") > 0)
                return strRiscosOcupacionaisAso.Replace("\r\n*Dis", "*Dis");
            else if (strRiscosOcupacionaisAso.IndexOf("\r\n\r\n\r\nDis") > 0)
                return strRiscosOcupacionaisAso.Replace("\r\n\r\nDis", "Dis");
            else if (strRiscosOcupacionaisAso.IndexOf("\r\n\r\nDis") > 0)
                return strRiscosOcupacionaisAso.Replace("\r\nDis", "Dis");
            else
                return strRiscosOcupacionaisAso;

        }





        public string RiscosOcupacionaisAso_Prajna(bool Exibir_Intensidade, bool Exibir_Dispensado, bool Exibir_Riscos_PPRA)
        {
            if (strRiscosOcupacionaisAso != string.Empty)
            {
                if (strRiscosOcupacionaisAso.IndexOf("\r\n\r\n\r\n**Dis") > 0)
                    return strRiscosOcupacionaisAso.Replace("\r\n\r\n**Dis", "**Dis");
                else if (strRiscosOcupacionaisAso.IndexOf("\r\n\r\n**Dis") > 0)
                    return strRiscosOcupacionaisAso.Replace("\r\n**Dis", "**Dis");
                else if (strRiscosOcupacionaisAso.IndexOf("\r\n\r\n\r\n*Dis") > 0)
                    return strRiscosOcupacionaisAso.Replace("\r\n\r\n*Dis", "*Dis");
                else if (strRiscosOcupacionaisAso.IndexOf("\r\n\r\n*Dis") > 0)
                    return strRiscosOcupacionaisAso.Replace("\r\n*Dis", "*Dis");
                else if (strRiscosOcupacionaisAso.IndexOf("\r\n\r\n\r\nDis") > 0)
                    return strRiscosOcupacionaisAso.Replace("\r\n\r\nDis", "Dis");
                else if (strRiscosOcupacionaisAso.IndexOf("\r\n\r\nDis") > 0)
                    return strRiscosOcupacionaisAso.Replace("\r\nDis", "Dis");
                else
                    return strRiscosOcupacionaisAso;
            }
            //return strRiscosOcupacionaisAso;




            StringBuilder ret = new StringBuilder();
            string quebraLinha;
            int zAux = 0;
            Boolean xFrase = false;

            string where2 = "nID_FUNC=" + this.Id + " AND gREMOVE_PCMSO = 0 and nId_Rsc in ( 0, 2 ) and gInsalubre = 0 "
                        + " ORDER BY nID_RSC";


            List<PPRA> listPPRA2 = new PPRA().Find<PPRA>(where2);


            string xRiscoFisico = "";
            string xRiscoBiologico = "";
            string xRiscoQuimico = "";
            string xRiscoAcidente = "";
            string xRiscoErgonomico = "";

            quebraLinha = "\n";

            foreach (PPRA ppra in listPPRA2)
            {

                //LaudoTecnico xLaudo = new LaudoTecnico();
                //xLaudo.Find(" nId_laud_tec = " + ppra.nID_LAUD_TEC.ToString() + " ");                

                //if (xLaudo.nID_EMPR.Exibir_Riscos_ASO == true)
                //{

                if (!(ppra.AcimaLimiteTolerancia()
                   || ppra.Acima50PorCentoDose()))
                {

                    if (ppra.nID_RSC.ToString().ToUpper().IndexOf("CALOR") >= 0)
                    {
                        //ret.Append("**" + ppra.GetNomeAgente() + "  " + ppra.tVL_MED + " ºC IBUTG " + quebraLinha);
                        if (Exibir_Intensidade == true)
                            ret.Append("**" + ppra.nID_RSC.ToString() + "  " + ppra.tVL_MED + " ºC IBUTG " + quebraLinha);
                        else
                            ret.Append("**" + ppra.nID_RSC.ToString() + quebraLinha);
                    }
                    else
                    {
                        //ret.Append("**" + ppra.GetNomeAgente() + "  " + ppra.tVL_MED + " dB " + quebraLinha);
                        if (ppra.tVL_MED > 0)
                        {
                            if (Exibir_Intensidade == true)
                                ret.Append("**" + ppra.nID_RSC.ToString() + "  " + ppra.tVL_MED + " dB " + quebraLinha);
                            else
                                ret.Append("**" + ppra.nID_RSC.ToString() + quebraLinha);
                        }
                        else if (ppra.tVL_MED == 0)
                        {
                            ret.Append("**" + ppra.nID_RSC.ToString() + quebraLinha);
                        }
                    }


                    xFrase = true;
                    zAux++;
                }

                //}
            }



            if (Exibir_Riscos_PPRA == true)
            {
                xRiscoFisico = xRiscoFisico + ret.ToString().Trim();
            }




            string where = "nID_FUNC=" + this.Id
                        + " AND gREMOVE_PCMSO = 0"
                        + " ORDER BY nID_RSC"
                        + " , (SELECT Nome FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.AgenteQuimico WHERE IdAgenteQuimico = nID_AG_NCV)";

            List<PPRA> listPPRA = new PPRA().Find<PPRA>(where);



            if (listPPRA.Count < (10 - zAux))
                quebraLinha = "\n";
            else
                quebraLinha = "; ";



            foreach (PPRA ppra in listPPRA)
            {

                if (Exibir_Riscos_PPRA == true)
                {
                    ppra.nID_RSC.Find();

                    if (ppra.AcimaLimiteTolerancia()
                        || ppra.Acima50PorCentoDose()
                        || ppra.nID_RSC.ToString().IndexOf("Anexo 13") >= 0
                        || ppra.Inserir_no_ASO() )
                    {
                                                
                        string rAgente = ppra.GetNomeAgente().Trim();

                        ppra.nID_RSC.Find();

                        if (ppra.nID_RSC.IndRiscoTipo == RiscoTipo.Fisico)
                        {
                            if (xRiscoFisico.ToUpper().IndexOf(rAgente.ToUpper()) < 0)
                                xRiscoFisico = xRiscoFisico + rAgente + " " + quebraLinha;
                        }
                        else if (ppra.nID_RSC.IndRiscoTipo == RiscoTipo.Biologico)
                        {
                            if (xRiscoBiologico.ToUpper().IndexOf(rAgente.ToUpper()) < 0)
                                xRiscoBiologico = xRiscoBiologico + rAgente + " " + quebraLinha;
                        }
                        else if (ppra.nID_RSC.IndRiscoTipo == RiscoTipo.Quimico)
                        {
                            if (xRiscoQuimico.ToUpper().IndexOf(rAgente.ToUpper()) < 0)
                                xRiscoQuimico = xRiscoQuimico + rAgente + " " + quebraLinha;
                        }
                        else if (ppra.nID_RSC.IndRiscoTipo == RiscoTipo.Acidentes)
                        {
                            if (xRiscoAcidente.ToUpper().IndexOf(rAgente.ToUpper()) < 0)
                                xRiscoAcidente = xRiscoAcidente + rAgente + " " + quebraLinha;
                        }
                        else if (ppra.nID_RSC.IndRiscoTipo == RiscoTipo.Ergonomico)
                        {
                            if (xRiscoErgonomico.ToUpper().IndexOf(rAgente.ToUpper()) < 0)
                                xRiscoErgonomico = xRiscoErgonomico + rAgente + " " + quebraLinha;
                        }


                        zAux++;
                    }
                }
                else
                {

                    ppra.nID_RSC.Find();

                    if (ppra.AcimaLimiteTolerancia()
                        || ppra.Acima50PorCentoDose()
                        || ppra.nID_RSC.ToString().IndexOf("Anexo 13") >= 0
                        || ppra.Inserir_no_ASO())
                    {
                        ret.Append(ppra.GetNomeAgente() + quebraLinha);
                        zAux++;
                    }
                }
            }



            if (ret.Length > quebraLinha.Length)
                ret.Remove(ret.ToString().Length - quebraLinha.Length, quebraLinha.Length);


            string xRiscoPPRA = "";

            if (Exibir_Riscos_PPRA == true)
            {


                if (xRiscoFisico != "")
                {
                    if ( xRiscoFisico.Substring( xRiscoFisico.Length-1,1) == quebraLinha)
                       xRiscoPPRA = xRiscoPPRA + "Risco Físico ( " + xRiscoFisico.Substring(0, xRiscoFisico.Length - 1) + " )" + System.Environment.NewLine;
                    else
                       xRiscoPPRA = xRiscoPPRA + "Risco Físico ( " + xRiscoFisico + " )" + System.Environment.NewLine;
                }

                if (xRiscoBiologico != "")
                {
                    if (xRiscoBiologico.Substring(xRiscoBiologico.Length-1, 1) == quebraLinha)
                        xRiscoPPRA = xRiscoPPRA + "Risco Biológico ( " + xRiscoBiologico.Substring(0, xRiscoBiologico.Length - 1) + " )" + System.Environment.NewLine;
                    else
                        xRiscoPPRA = xRiscoPPRA + "Risco Biológico ( " + xRiscoBiologico + " )" + System.Environment.NewLine;
                }

                if (xRiscoQuimico != "")
                {
                    if (xRiscoQuimico.Substring(xRiscoQuimico.Length-1, 1) == quebraLinha)
                        xRiscoPPRA = xRiscoPPRA + "Risco Químico ( " + xRiscoQuimico.Substring(0, xRiscoQuimico.Length - 1) + " )" + System.Environment.NewLine;
                    else
                        xRiscoPPRA = xRiscoPPRA + "Risco Químico ( " + xRiscoQuimico + " )" + System.Environment.NewLine;
                }

                if (xRiscoAcidente != "")
                {
                    if (xRiscoAcidente.Substring(xRiscoAcidente.Length-1, 1) == quebraLinha)
                        xRiscoPPRA = xRiscoPPRA + "Risco Acidente ( " + xRiscoAcidente.Substring(0, xRiscoAcidente.Length - 1) + " )" + System.Environment.NewLine;
                    else
                        xRiscoPPRA = xRiscoPPRA + "Risco Acidente ( " + xRiscoAcidente + " )" + System.Environment.NewLine;
                }

                if (xRiscoErgonomico != "")
                {
                    if (xRiscoErgonomico.Substring(xRiscoErgonomico.Length-1, 1) == quebraLinha)
                        xRiscoPPRA = xRiscoPPRA + "Risco Ergonômico ( " + xRiscoErgonomico.Substring(0, xRiscoErgonomico.Length - 1) + " )" + System.Environment.NewLine;
                    else
                        xRiscoPPRA = xRiscoPPRA + "Risco Ergonômico ( " + xRiscoErgonomico + " )" + System.Environment.NewLine;
                }



                string where3 = "nID_FUNC=" + this.Id
                            + " ORDER BY nID_RSC";

                List<PPRA> listPPRA3 = new PPRA().Find<PPRA>(where3);



                //if (listPPRA.Count < (10 - zAux))
                //    quebraLinha = "\n";
                //else
                //    quebraLinha = "; ";


                foreach (PPRA ppra in listPPRA3)
                {
                    ppra.nID_AG_NCV.Find();

                    if (ppra.nID_AG_NCV.Nome.ToUpper().Trim() != "AUSÊNCIA DE FATOR DE RISCO")
                    {
                        
                        ppra.nID_RSC.Find();

                        if (ppra.nID_RSC.IndRiscoTipo == RiscoTipo.Fisico)
                        {
                            if (ppra.gREMOVE_PCMSO == false && xRiscoPPRA.ToString().IndexOf("Risco Físico") < 0) xRiscoPPRA = xRiscoPPRA + "Risco Físico " + System.Environment.NewLine;
                        }
                        else if (ppra.nID_RSC.IndRiscoTipo == RiscoTipo.Biologico)
                        {
                            if (ppra.gREMOVE_PCMSO == false && xRiscoPPRA.ToString().IndexOf("Risco Biológico") < 0) xRiscoPPRA = xRiscoPPRA + "Risco Biológico " + System.Environment.NewLine;
                        }
                        else if (ppra.nID_RSC.IndRiscoTipo == RiscoTipo.Quimico)
                        {
                            if (ppra.gREMOVE_PCMSO == false && xRiscoPPRA.ToString().IndexOf("Risco Químico") < 0) xRiscoPPRA = xRiscoPPRA + "Risco Químico " + System.Environment.NewLine;
                        }
                        else if (ppra.nID_RSC.IndRiscoTipo == RiscoTipo.Acidentes)
                        {
                            if (ppra.gREMOVE_PCMSO == false && xRiscoPPRA.ToString().IndexOf("Risco Acidente") < 0) xRiscoPPRA = xRiscoPPRA + "Risco Acidentes " + System.Environment.NewLine;
                        }
                        else if (ppra.nID_RSC.IndRiscoTipo == RiscoTipo.Ergonomico)
                        {
                            if (ppra.gREMOVE_PCMSO == false && xRiscoPPRA.ToString().IndexOf("Risco Ergonômico") < 0) xRiscoPPRA = xRiscoPPRA + "Risco Ergonômico " + System.Environment.NewLine;
                        }
                    }
                }

                //if (xRiscoPPRA != "")
                //{
                //    xRiscoPPRA = "(" + xRiscoPPRA.Substring(0, xRiscoPPRA.Length - 1) + ")";
                //    xRiscoPPRA = quebraLinha + xRiscoPPRA;
                //}


                ret.Length = 0;
                ret.Append(xRiscoPPRA);
            }






            if (listPPRA.Count == 0 && xRiscoPPRA == "")
                ret.Append("Risco ocupacional inexistente"); // "Sem risco ocupacional específico");


            if (Exibir_Dispensado == true)
            {
                if (xFrase == true)
                {
                    for (int zCont = 10 - zAux; zCont >= 0; zCont--)
                    {
                        ret.Append(System.Environment.NewLine);
                    }
                    ret.Append("**Dispensado de exames complementares");
                }
            }

            



            strRiscosOcupacionaisAso = ret.ToString();

            if (strRiscosOcupacionaisAso.IndexOf("\r\n\r\n\r\n**Dis") > 0)
                return strRiscosOcupacionaisAso.Replace("\r\n\r\n**Dis", "**Dis");
            else if (strRiscosOcupacionaisAso.IndexOf("\r\n\r\n**Dis") > 0)
                return strRiscosOcupacionaisAso.Replace("\r\n**Dis", "**Dis");
            else if (strRiscosOcupacionaisAso.IndexOf("\r\n\r\n\r\n*Dis") > 0)
                return strRiscosOcupacionaisAso.Replace("\r\n\r\n*Dis", "*Dis");
            else if (strRiscosOcupacionaisAso.IndexOf("\r\n\r\n*Dis") > 0)
                return strRiscosOcupacionaisAso.Replace("\r\n*Dis", "*Dis");
            else if (strRiscosOcupacionaisAso.IndexOf("\r\n\r\n\r\nDis") > 0)
                return strRiscosOcupacionaisAso.Replace("\r\n\r\nDis", "Dis");
            else if (strRiscosOcupacionaisAso.IndexOf("\r\n\r\nDis") > 0)
                return strRiscosOcupacionaisAso.Replace("\r\nDis", "Dis");
            else
                return strRiscosOcupacionaisAso;

        }


        #endregion
        #region RiscosOcupacionaisAsoNomeGenerico

        private string strRiscosOcupacionaisAsoNomeGenerico = string.Empty;

        public string RiscosOcupacionaisAsoNomeGenerico()
        {
            if (strRiscosOcupacionaisAsoNomeGenerico != string.Empty)
                return strRiscosOcupacionaisAsoNomeGenerico;

            StringBuilder str = new StringBuilder();
            str.Append("nID_FUNC=" + this.Id);
            str.Append(" AND gREMOVE_PCMSO = 0 AND (gINSALUBRE = 1 OR nID_RSC IN (17, 18))");
            str.Append(" ORDER BY nID_RSC");

            ArrayList listPPRA = new PPRA().Find(str.ToString());

            StringBuilder ret = new StringBuilder();

            foreach (PPRA ppra in listPPRA)
            {
                ppra.nID_RSC.Find();

                string strNome = ppra.nID_RSC.GetRiscoTipo();

                if (ret.ToString().IndexOf(strNome) == -1)
                    ret.Append(strNome + "\n");
            }

            if (listPPRA.Count == 0)
                ret.Append("Risco ocupacional inexistente"); // "Sem risco ocupacional específico");

            strRiscosOcupacionaisAsoNomeGenerico = ret.ToString();

            return strRiscosOcupacionaisAsoNomeGenerico;
        }
        #endregion

        #region RiscosFisicos
        public string RiscosFisicos()
        {
            StringBuilder str = new StringBuilder();

            List<PPRA> listFisico = new PPRA().Find<PPRA>("nID_RSC<9"
                                                        + " AND nID_FUNC=" + this.Id
                                                        + " ORDER BY nID_RSC");
            foreach (PPRA ppra in listFisico)
            {
                str.Append(ppra.GetNomeAgente() + ", ");
                str.Append('\n');
            }

            if (str.Length > 2)
                str.Remove(str.Length - 3, 3);
            else if (listFisico.Count == 0)
                str.Append(Ghe.strInexistente);

            return str.ToString();
        } 
        #endregion

        #region RiscosQuimicosQuantitativo
        public string RiscosQuimicosQuantitativo()
        {
            StringBuilder str = new StringBuilder();

            List<PPRA> listQuimico = new PPRA().Find<PPRA>("nID_FUNC=" + this.Id
                + " AND nID_RSC IN(9,10,11,12,13,16)"
                + " ORDER BY (SELECT Nome FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.AgenteQuimico WHERE IdAgenteQuimico = nID_AG_NCV)");

            int i = 1;

            foreach (PPRA ppra in listQuimico)
            {
                ppra.nID_AG_NCV.Find();
                str.Append(Convert.ToString(i++) + " - " + ppra.GetNomeAgente());
                str.Append('\n');
            }

            if (listQuimico.Count == 0)
                str.Append(Ghe.strInexistente);

            return str.ToString();
        } 
        #endregion

        #region RiscosQuimicosQuantitativoValor
        public string RiscosQuimicosQuantitativoValor()
        {
            StringBuilder str = new StringBuilder();

            List<PPRA> listQuimico = new PPRA().Find<PPRA>("nID_FUNC=" + this.Id
            + " AND nID_RSC IN(9,10,11,12,13,16)"
            + " ORDER BY (SELECT Nome FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.AgenteQuimico WHERE IdAgenteQuimico = nID_AG_NCV)");

            int i = 1;

            foreach (PPRA ppra in listQuimico)
            {
                ppra.nID_AG_NCV.Find();
                str.Append(Convert.ToString(i++) + " - " + ppra.GetAvaliacaoQuantitativa());
                str.Append('\n');
            }

            if (str.Length > 2)
                str.Remove(str.Length - 1, 1);
            else if (listQuimico.Count == 0)
                str.Append(Ghe.strInexistente);

            return str.ToString();
        } 
        #endregion

        #region RiscosQuimicosQuantitativoLimite

        public string RiscosQuimicosQuantitativoLimite()
        {
            StringBuilder str = new StringBuilder();

            List<PPRA> listQuimico = new PPRA().Find<PPRA>("nID_FUNC=" + this.Id
                + " AND nID_RSC IN(9,10,11,12,13,16)"
                + " ORDER BY (SELECT Nome FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.AgenteQuimico WHERE IdAgenteQuimico = nID_AG_NCV)");

            int i = 1;

            foreach (PPRA ppra in listQuimico)
            {
                ppra.nID_AG_NCV.Find();
                str.Append(Convert.ToString(i++) + " - " + ppra.tVL_LIM_TOL);
                str.Append('\n');
            }

            if (str.Length > 2)
                str.Remove(str.Length - 1, 1);
            else if (listQuimico.Count == 0)
                str.Append(Ghe.strInexistente);

            return str.ToString();
        } 
        #endregion

        #region RiscosQuimicosQualitativo
        public string RiscosQuimicosQualitativo()
        {
            StringBuilder str = new StringBuilder();

            List<PPRA> listQuimico = new PPRA().Find<PPRA>("nID_FUNC=" + this.Id
                                                        + " AND nID_RSC IN(14)"
                                                        + " ORDER BY nID_RSC");

            foreach (PPRA ppra in listQuimico)
            {
                str.Append(ppra.GetNomeAgente());
                str.Append('\n');
            }

            if (str.Length > 2)
                str.Remove(str.Length - 1, 1);
            else if (listQuimico.Count == 0)
                str.Append(Ghe.strInexistente);

            return str.ToString();
        } 
        #endregion

        #region RiscosBiologicos
        public string RiscosBiologicos()
        {
            StringBuilder str = new StringBuilder();

            List<PPRA> listBiologico = new PPRA().Find<PPRA>("gINSALUBRE=1"
                                                            + " AND nID_FUNC=" + this.Id
                                                            + " AND nID_RSC=" + (int)Riscos.AgentesBiologicos);

            foreach (PPRA ppra in listBiologico)
            {
                str.Append(ppra.GetNomeAgente() + ", ");
                str.Append('\n');
            }

            if (str.Length > 2)
                str.Remove(str.Length - 3, 3);
            else if (listBiologico.Count == 0)
                str.Append(Ghe.strInexistente);

            return str.ToString();
        } 
        #endregion

        #region GetDemaisAgentesReconhecimento
        public string GetDemaisAgentesReconhecimento(bool IsHtml)
        {
            StringBuilder str = new StringBuilder();

            List<PPRA> listDemais = new PPRA().Find<PPRA>("nID_RSC NOT IN("
                                                        + (int)Riscos.RuidoContinuo + ","
                                                        + (int)Riscos.Calor + ","
                                                        + (int)Riscos.AgentesQuimicos + ","
                                                        + (int)Riscos.AgentesQuimicosB + ","
                                                        + (int)Riscos.AgentesQuimicosC + ","
                                                        + (int)Riscos.AgentesQuimicosD + ","
                                                        + (int)Riscos.Acidentes + ","
                                                        + (int)Riscos.Ergonomicos + ","
                                                        + (int)Riscos.ACGIH + ","
                                                        + (int)Riscos.AsbestosPoeirasMinerais + ") "
                                                        + " AND nID_FUNC=" + this.Id
                                                        + " ORDER BY nID_RSC");

            foreach (PPRA ppra in listDemais)
            {
                str.Append(ppra.GetReconhecimentoPPRA(IsHtml));

                if (IsHtml)
                    str.Append("<br>");
            }

            if (this.tDS_COMPL_DEMAIS_AGENTES != string.Empty)
            {
                if (IsHtml)
                    str.Append("<p align='justify'><Font color='navy'>");

                str.Append(this.tDS_COMPL_DEMAIS_AGENTES);

                if (IsHtml)
                    str.Append("</Font></p>");
            }

            if (listDemais.Count == 0 && this.tDS_COMPL_DEMAIS_AGENTES == string.Empty)
            {
                if (IsHtml)
                    str.Append("<Font color='navy'>Inexistente</Font>");
                else
                    str.Append(Ghe.strInexistente);
            }

            return str.ToString();
        } 
        #endregion

        #region GetRuidoContinuo
        public PPRA GetRuidoContinuo()
        {
            List<PPRA> listFisico = new PPRA().Find<PPRA>("nID_RSC=" + (int)Riscos.RuidoContinuo
                                                        + " AND nID_FUNC=" + this.Id
                                                        + " ORDER BY nID_RSC");

            if (listFisico.Count == 0)
                return new PPRA();
            else
                return (PPRA)listFisico[0];
        } 
        #endregion

        #region RuidosdBA
        public string RuidosdBA()
        {
            StringBuilder str = new StringBuilder();

            PPRA ppra = this.GetRuidoContinuo();

            str.Append(ppra.tVL_MED);
            str.Append('\n');

            if (ppra.Id == 0)
                str.Append("-");

            return str.ToString();
        } 
        #endregion

        #region DoseUltrapassada
        public bool DoseUltrapassada()
        {
            bool ret = false;

            List<PPRA> list = new PPRA().Find<PPRA>("nID_FUNC=" + this.Id);

            foreach (PPRA ppra in list)
            {
                if (ppra.Acima50PorCentoDose())
                    ret = true;
            }

            return ret;
        } 
        #endregion

        #region DoseUltrapassadaPcmso
        public bool DoseUltrapassadaPcmso()
        {
            bool ret = false;

            List<PPRA> list = new PPRA().Find<PPRA>("nID_FUNC=" + this.Id
                                                + " AND gREMOVE_PCMSO=0");

            foreach (PPRA ppra in list)
            {
                if (ppra.Acima50PorCentoDose())
                    ret = true;
            }

            return ret;
        } 
        #endregion

        #region RuidoUltrapassado

        public bool RuidoUltrapassado()
        {
            int count = new PPRA().ExecuteCount("nID_RSC=0"
                                                + " AND nID_FUNC=" + this.Id
                                                + " AND gINSALUBRE = 1");

            return count > 0;
        }
        #endregion

        #region RuidosLimite
        public string RuidosLimite()
        {
            StringBuilder str = new StringBuilder();

            List<PPRA> list = new PPRA().Find<PPRA>("nID_RSC=0"
                                                    + " AND nID_FUNC=" + this.Id);

            foreach (PPRA ppra in list)
            {
                str.Append(ppra.GetLimiteTolerancia());
                break;
            }

            return str.ToString();
        } 
        #endregion

        #region CalorIBUTG
        public string CalorIBUTG()
        {
            StringBuilder str = new StringBuilder();

            List<PPRA> listFisico = new PPRA().Find<PPRA>("nID_RSC=2"
                                                            + " AND nID_FUNC=" + this.Id);

            foreach (PPRA ppra in listFisico)
            {
                if (!ppra.bDESC || ppra.Calculo_Limite_Calor == true)
                {
                    if (ppra.tVL_MED != 0)
                        str.Append(ppra.tVL_MED + " ºC");
                    else
                        str.Append(" *");

                    str.Append('\n');
                }
                else
                {
                    str.Append("IBUTG**= " + ppra.IBUTG);
                    str.Append('\n');
                    str.Append("Tt = " + ppra.Tt + " min");
                    str.Append('\n');
                    str.Append("IBUTGt = " + ppra.IBUTGt);
                    str.Append('\n');
                    str.Append("Mt = " + ppra.Mt + " kcal/h");
                    str.Append('\n');
                    str.Append("Td = " + ppra.Td + " min");
                    str.Append('\n');
                    str.Append("Md = " + ppra.Md + " kcal/h");
                    str.Append('\n');
                    str.Append("IBUTGd = " + ppra.IBUTGd);
                    str.Append('\n');
                }
            }
            if (listFisico.Count == 0)
                str.Append("-");

            return str.ToString();
        } 
        #endregion

        #region TipoAtividade
        public string TipoAtividade()
        {
            StringBuilder str = new StringBuilder();

            List<PPRA> listFisico = new PPRA().Find<PPRA>("nID_RSC=2 AND nID_FUNC=" + this.Id);

            foreach (PPRA ppra in listFisico)
            {
                if (!ppra.bDESC)
                {
                    if (ppra.nID_ATV == 1)
                        str.Append("Leve");
                    else if (ppra.nID_ATV == 2)
                        str.Append("Moderada");
                    else if (ppra.nID_ATV == 3)
                        str.Append("Pesada");
                    else
                        str.Append("-");
                    str.Append('\n');
                }
                else
                {
                    str.Append("-");
                    str.Append('\n');
                }
            }

            if (listFisico.Count == 0)
                str.Append("-");

            return str.ToString();
        } 
        #endregion

        #region CalorLimite
        public string CalorLimite()
        {
            StringBuilder str = new StringBuilder();

            List<PPRA> listFisico = new PPRA().Find<PPRA>("nID_RSC=2 AND nID_FUNC=" + this.Id);

            foreach (PPRA ppra in listFisico)
            {
                if (!ppra.bDESC)
                {
                    if (ppra.Calculo_Limite_Calor == true)
                    {
                        str.Append(ppra.sVL_LIM_TOL + " ºC");
                        str.Append('\n');
                    }
                    else
                    {
                        if (ppra.nID_ATV == 1)
                            str.Append("até 30,0 para atividade leve");
                        else if (ppra.nID_ATV == 2)
                            str.Append("até 26,7 para atividade moderada");
                        else if (ppra.nID_ATV == 3)
                            str.Append("até 25,0 para atividade pesada");
                        else
                            str.Append("-");
                        str.Append('\n');
                    }
                }
                else
                {
                    str.Append(ppra.sVL_LIM_TOL);
                    str.Append('\n');
                }
            }

            if (listFisico.Count == 0)
                str.Append("-");

            return str.ToString();
        } 
        #endregion

        #endregion

        #region Ordem de Servico

        public string GetRiscoAcidente(string strConcatenar)
        {
            StringBuilder str = new StringBuilder();

            ArrayList list = new RiscoAcidenteGhe().Find("IdGhe=" + this.Id + " ORDER BY Ordem");

            foreach (RiscoAcidenteGhe risco in list)
            {
                str.Append(risco.Descricao);
                str.Append(strConcatenar);
            }

            if (list.Count == 0)
                str.Append(Ghe.strInexistente);
            else
                str.Replace(strConcatenar, ".", str.Length - strConcatenar.Length, strConcatenar.Length);

            return str.ToString();
        }

        public string GetProcedimentoInstrutivoHtml()
        {
            StringBuilder str = new StringBuilder();

            ArrayList list = new ProcedimentoInstrutivoGhe().Find("IdGhe=" + this.Id + " ORDER BY Ordem");

            str.Append("<Font color='#000080'>");

            foreach (ProcedimentoInstrutivoGhe procedimentoInstrutivoGhe in list)
            {
                str.Append("<b>" + Convert.ToInt32((procedimentoInstrutivoGhe.Ordem + 1)).ToString("00") + ") </b>" + procedimentoInstrutivoGhe.Descricao);
                str.Append("<br>&nbsp;<br>");
            }

            str.Append("</Font>");

            return str.ToString();
        }

        #endregion

        #region Medidas de Controle

        #region EPI

        public ArrayList GetListaEPI()
        {
            ArrayList list = new ArrayList();
            //ArrayList listGheEpiExistente = new GheEpiExistente().Find("nID_FUNC=" + this.Id);
            ArrayList listEpiExistente = new EpiExistente().FindIn(new PPRA().GetType(), "", true, "nID_FUNC=" + this.Id + " and nId_Rsc not in ( 17,18) ");

            //foreach (GheEpiExistente gheEpiExte in listGheEpiExistente)
            //{
            //    gheEpiExte.nID_EPI.Find();
            //    if (gheEpiExte.tDS_CONDICAO == string.Empty)
            //        list.Add(gheEpiExte.nID_EPI.ToString());
            //    else
            //        list.Add(gheEpiExte.nID_EPI.ToString() + " (" + gheEpiExte.tDS_CONDICAO + ")");
            //}

            foreach (EpiExistente epiExte in listEpiExistente)
            {
                epiExte.nID_EPI.Find();

                if (epiExte.tDS_CONDICAO == string.Empty)
                    list.Add(epiExte.nID_EPI.ToString());
                else
                    list.Add(epiExte.nID_EPI.ToString() + " (" + epiExte.tDS_CONDICAO + ")");
            }

            list.Sort();

            return list;
        }

        public DataSet GetEPI()
        {
            DataSet ds = new DataSet();
            DataSet dsOrdered = new DataSet();

            DataTable table = new DataTable("Default");
            table.Columns.Add("Id", Type.GetType("System.String"));
            table.Columns.Add("Descricao", Type.GetType("System.String"));

            DataTable tableOrdered = new DataTable("DefaultOrdered");
            tableOrdered.Columns.Add("Id", Type.GetType("System.String"));
            tableOrdered.Columns.Add("Descricao", Type.GetType("System.String"));

            ds.Tables.Add(table);
            dsOrdered.Tables.Add(tableOrdered);

            DataRow row, newRowOrdered;

            ArrayList listGheEpiExistente = new GheEpiExistente().Find("nID_FUNC=" + this.Id);
            ArrayList listEpiExistente = new EpiExistente().FindIn(new PPRA().GetType(), "", true, "nID_FUNC=" + this.Id);
            
            foreach (GheEpiExistente gheEpiExte in listGheEpiExistente)
            {
                gheEpiExte.nID_EPI.Find();

                row = ds.Tables[0].NewRow();
                row["Id"] = gheEpiExte.nID_EPI.Id.ToString();

                if (gheEpiExte.tDS_CONDICAO == string.Empty)
                    row["Descricao"] = gheEpiExte.nID_EPI.ToString();
                else
                    row["Descricao"] = gheEpiExte.nID_EPI.ToString() + " (" + gheEpiExte.tDS_CONDICAO + ")";

                ds.Tables[0].Rows.Add(row);
            }
            foreach (EpiExistente epiExte in listEpiExistente)
            {
                epiExte.nID_EPI.Find();

                row = ds.Tables[0].NewRow();
                row["Id"] = epiExte.nID_EPI.Id.ToString();

                if (epiExte.tDS_CONDICAO == string.Empty)
                    row["Descricao"] = epiExte.nID_EPI.ToString();
                else
                    row["Descricao"] = epiExte.nID_EPI.ToString() + " (" + epiExte.tDS_CONDICAO + ")";

                ds.Tables[0].Rows.Add(row);
            }

            DataRow[] rows = ds.Tables[0].Select("", "Descricao");
            StringBuilder strCheck = new StringBuilder();

            foreach (DataRow oldrow in rows)
            {
                if (strCheck.ToString().IndexOf(oldrow["Descricao"].ToString()) == -1)
                {
                    newRowOrdered = dsOrdered.Tables[0].NewRow();

                    newRowOrdered["Id"] = oldrow["Id"].ToString();
                    newRowOrdered["Descricao"] = oldrow["Descricao"].ToString();

                    dsOrdered.Tables[0].Rows.Add(newRowOrdered);

                    strCheck.Append(oldrow["Descricao"].ToString());
                }
            }

            return dsOrdered;
        }



        public DataSet GetEPI_com_Acidentes()
        {
            DataSet ds = new DataSet();
            DataSet dsOrdered = new DataSet();

            DataTable table = new DataTable("Default");
            table.Columns.Add("Id", Type.GetType("System.String"));
            table.Columns.Add("Descricao", Type.GetType("System.String"));

            DataTable tableOrdered = new DataTable("DefaultOrdered");
            tableOrdered.Columns.Add("Id", Type.GetType("System.String"));
            tableOrdered.Columns.Add("Descricao", Type.GetType("System.String"));

            ds.Tables.Add(table);
            dsOrdered.Tables.Add(tableOrdered);

            DataRow row, newRowOrdered;

            ArrayList listGheEpiExistente = new GheEpiExistente().Find("nID_FUNC=" + this.Id);
            ArrayList listEpiExistente = new EpiExistente().FindIn(new PPRA().GetType(), "", true, "nID_FUNC=" + this.Id);

            //ArrayList listEpiExistente_Ac = new EpiExistente().FindIn(new PPRA().GetType(), "", true, "nID_FUNC=" + this.Id + " AND nID_RSC=" + (int)Riscos.Acidentes);

            DataSet ds_Ac = new GheEpiRecomendado().Get("nID_FUNC=" + this.Id);

            StringBuilder str = new StringBuilder();


            //foreach (EpiExistente epiExte_Ac in listEpiExistente_Ac)
            //{
            //    epiExte_Ac.nID_EPI.Find();

            //    row = ds.Tables[0].NewRow();
            //    row["Id"] = epiExte_Ac.nID_EPI.Id.ToString();

            //    if (epiExte_Ac.tDS_CONDICAO == string.Empty)
            //        row["Descricao"] = epiExte_Ac.nID_EPI.ToString();
            //    else
            //        row["Descricao"] = epiExte_Ac.nID_EPI.ToString() + " (" + epiExte_Ac.tDS_CONDICAO + ")";

            //    ds.Tables[0].Rows.Add(row);
            //}


            for (int zCont = 0; zCont < ds_Ac.Tables[0].Rows.Count; zCont++)
            {
                row = ds.Tables[0].NewRow();
                row["Id"] = ds_Ac.Tables[0].Rows[zCont]["nId_Epi"].ToString();

                Epi zEpi = new Epi(System.Convert.ToInt32(ds_Ac.Tables[0].Rows[zCont]["nId_Epi"].ToString()));

                if (ds_Ac.Tables[0].Rows[zCont]["bCondicao"].ToString() != "True")
                    row["Descricao"] = zEpi.Descricao;
                else
                    row["Descricao"] = zEpi.Descricao + " (" + ds_Ac.Tables[0].Rows[zCont]["tDS_Condicao"].ToString().Trim() + ")";

                ds.Tables[0].Rows.Add(row);
            }


            foreach (GheEpiExistente gheEpiExte in listGheEpiExistente)
            {
                gheEpiExte.nID_EPI.Find();

                row = ds.Tables[0].NewRow();
                row["Id"] = gheEpiExte.nID_EPI.Id.ToString();

                if (gheEpiExte.tDS_CONDICAO == string.Empty)
                    row["Descricao"] = gheEpiExte.nID_EPI.ToString();
                else
                    row["Descricao"] = gheEpiExte.nID_EPI.ToString() + " (" + gheEpiExte.tDS_CONDICAO + ")";

                ds.Tables[0].Rows.Add(row);
            }
            foreach (EpiExistente epiExte in listEpiExistente)
            {
                epiExte.nID_EPI.Find();

                row = ds.Tables[0].NewRow();
                row["Id"] = epiExte.nID_EPI.Id.ToString();

                if (epiExte.tDS_CONDICAO == string.Empty)
                    row["Descricao"] = epiExte.nID_EPI.ToString();
                else
                    row["Descricao"] = epiExte.nID_EPI.ToString() + " (" + epiExte.tDS_CONDICAO + ")";

                ds.Tables[0].Rows.Add(row);
            }

            DataRow[] rows = ds.Tables[0].Select("", "Descricao");
            StringBuilder strCheck = new StringBuilder();

            foreach (DataRow oldrow in rows)
            {
                if (strCheck.ToString().IndexOf(oldrow["Descricao"].ToString()) == -1)
                {
                    newRowOrdered = dsOrdered.Tables[0].NewRow();

                    newRowOrdered["Id"] = oldrow["Id"].ToString();
                    newRowOrdered["Descricao"] = oldrow["Descricao"].ToString();

                    dsOrdered.Tables[0].Rows.Add(newRowOrdered);

                    strCheck.Append(oldrow["Descricao"].ToString());
                }
            }

            return dsOrdered;
        }
        

        
        public string Epi()
        {
            return this.Epi("\n");
        }

        public string Epi(string strConcatenar)
        {
            StringBuilder str = new StringBuilder();

            ArrayList listEpi = this.GetListaEPI();

            foreach (string epi in listEpi)
            {
                if (str.ToString().ToUpper().IndexOf(epi.ToString().ToUpper()) == -1)
                {
                    str.Append(epi.ToString());
                    str.Append(strConcatenar);
                }
            }

            if (listEpi.Count == 0)
                str.Append(Ghe.strInexistente);
            else
                str.Replace(strConcatenar, ".", str.Length - strConcatenar.Length, strConcatenar.Length);

            return str.ToString();
        }

        public string EpiAcidentes()
        {
            return this.EpiAcidentes("\n");
        }

        public string EpiAcidentes(string strConcatenar)
        {
            ArrayList listEpi = new ArrayList();
            ArrayList listGheEpiExistente = new GheEpiExistente().Find("nID_FUNC=" + this.Id);
            ArrayList listEpiExistente = new EpiExistente().FindIn(new PPRA().GetType(), "", true, "nID_FUNC=" + this.Id + " AND nID_RSC=" + (int)Riscos.Acidentes);

            StringBuilder str = new StringBuilder();

            foreach (GheEpiExistente gheEpiExte in listGheEpiExistente)
            {
                gheEpiExte.nID_EPI.Find();

                if (gheEpiExte.tDS_CONDICAO == string.Empty)
                    listEpi.Add(gheEpiExte.nID_EPI.ToString());
                else
                    listEpi.Add(gheEpiExte.nID_EPI.ToString() + " (" + gheEpiExte.tDS_CONDICAO + ")");
            }

            //Global não quer que repita EPIs do risco, apenas apareça da aba Acidentes
            if (Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper().IndexOf("GLOBAL") < 0)
            {
                foreach (EpiExistente epiExte in listEpiExistente)
                {
                    epiExte.nID_EPI.Find();

                    if (epiExte.tDS_CONDICAO == string.Empty)
                        listEpi.Add(epiExte.nID_EPI.ToString());
                    else
                        listEpi.Add(epiExte.nID_EPI.ToString() + " (" + epiExte.tDS_CONDICAO + ")");
                }

            }


            listEpi.Sort();

            foreach (string epi in listEpi)
            {
                if (str.ToString().IndexOf(epi.ToString()) == -1)
                {
                    str.Append(epi.ToString());
                    str.Append(strConcatenar);
                }
            }

            if (listEpi.Count == 0)
                str.Append(Ghe.strInexistente);
            else
                str.Replace(strConcatenar, ".", str.Length - strConcatenar.Length, strConcatenar.Length);

            return str.ToString();
        }

        #endregion

        #region EPC

        public string Epc()
        {
            StringBuilder str = new StringBuilder();

            if (this.tDS_FUNC_EPC_EXTE != string.Empty)
                str.Append(this.tDS_FUNC_EPC_EXTE + "\n");

            ArrayList listPPRA = new PPRA().Find("nID_FUNC=" + this.Id);

            foreach (PPRA ppra in listPPRA)
            {
                if (ppra.mDS_EPC_EXTE != string.Empty && str.ToString().IndexOf(ppra.mDS_EPC_EXTE) == -1)
                {
                    str.Append(ppra.mDS_EPC_EXTE);
                    str.Append('\n');
                }
            }

            if (str.ToString() == string.Empty)
                str.Append(Ghe.strInexistente);

            return str.ToString();
        }

        public string Epc_LTCAT()
        {
            StringBuilder str = new StringBuilder();

            //if (this.tDS_FUNC_EPC_EXTE != string.Empty)
            //    str.Append(this.tDS_FUNC_EPC_EXTE + "\n");

            ArrayList listPPRA = new PPRA().Find("nID_FUNC=" + this.Id + " and nId_Rsc not in (17,18) ");

            foreach (PPRA ppra in listPPRA)
            {
                if (ppra.mDS_EPC_EXTE != string.Empty && str.ToString().IndexOf(ppra.mDS_EPC_EXTE) == -1)
                {
                    str.Append(ppra.mDS_EPC_EXTE);
                    str.Append('\n');
                }
            }

            str = str.Replace("Inexistente.", "");

            if (str.ToString() == string.Empty)
                str.Append(Ghe.strInexistente);

            return str.ToString();
        }

        #endregion

        #region MedidasControle, Administrativa e Educacional

        public string GetMedidasControle()
        {
            StringBuilder str = new StringBuilder();
            str.Append("EPI: ");
            str.Append("\r\n");
            str.Append(this.Epi("; "));
            str.Append("\r\n");
            str.Append("\r\n");
            str.Append("EPC: ");
            str.Append("\r\n");
            str.Append(this.Epc());
            str.Append("\r\n");
            str.Append("\r\n");
            str.Append("Administrativa: ");
            str.Append("\r\n");
            str.Append(this.GetMedidasControleAdministrativa());
            str.Append("\r\n");
            str.Append("\r\n");
            str.Append("Educacional: ");
            str.Append("\r\n");
            str.Append(this.GetMedidasControleEducacional());
            return str.ToString();
        }

        public string GetMedidasControleAdministrativa()
        {
            return GetMedidasControleAdministrativa("\r\n");
        }

        public string GetMedidasControleAdministrativa(string concat)
        {
            StringBuilder str = new StringBuilder();

            if (this.tDS_MED_CTL_CAR_ADM != string.Empty)
                str.Append(this.tDS_MED_CTL_CAR_ADM + concat);

            ArrayList listPPRA = new PPRA().Find("nID_FUNC=" + this.Id);

            foreach (PPRA ppra in listPPRA)
            {
                if (ppra.mDS_MED_CTL_CAR_ADM != string.Empty && str.ToString().IndexOf(ppra.mDS_MED_CTL_CAR_ADM) == -1)
                {
                    str.Append(ppra.mDS_MED_CTL_CAR_ADM);
                    str.Append(concat);
                }
            }
            if (str.ToString() == string.Empty)
                str.Append(Ghe.strInexistente);

            return str.ToString();
        }

        public string GetMedidasControleEducacional()
        {
            return GetMedidasControleEducacional("\r\n");
        }

        public string GetMedidasControleEducacional(string concat)
        {
            StringBuilder str = new StringBuilder();

            if (this.tDS_MED_CTL_CAR_EDC != string.Empty)
                str.Append(this.tDS_MED_CTL_CAR_EDC + concat);
            
            ArrayList listPPRA = new PPRA().Find("nID_FUNC=" + this.Id);
            
            foreach (PPRA ppra in listPPRA)
            {
                if (ppra.mDS_MED_CTL_CAR_EDC != string.Empty && str.ToString().IndexOf(ppra.mDS_MED_CTL_CAR_EDC) == -1)
                {
                    str.Append(ppra.mDS_MED_CTL_CAR_EDC);
                    str.Append(concat);
                }
            }
            if (str.ToString() == string.Empty)
                str.Append(Ghe.strInexistente);

            return str.ToString();
        }

        #endregion

        

        #region EmpregadosExpostos

        public List<Empregado> GetEmpregadosExposto()
        {
            return GetEmpregadosExpostos(false, false);
        }

        public List<Empregado> GetEmpregadosExpostos(bool SomenteAtivos)
        {
            return GetEmpregadosExpostos(SomenteAtivos, true);
        }

        public List<Empregado> GetEmpregadosExpostos(bool SomenteAtivos, bool SemAfastados)
        {
            StringBuilder criteria = new StringBuilder();

            criteria.Append("nID_EMPREGADO = nID_EMPREGADO");

            if (SomenteAtivos)
                criteria.Append(" AND hDT_DEM IS NULL");

            criteria.Append(" AND nID_EMPREGADO IN (SELECT nID_EMPREGADO FROM tblEMPREGADO_FUNCAO WHERE");

            if (SomenteAtivos)//Planserve todos do Ghe
                criteria.Append(" hDT_TERMINO IS NULL AND");

             criteria.Append(" nID_EMPREGADO_FUNCAO IN"
                            + " (SELECT nID_EMPREGADO_FUNCAO FROM tblFUNC_EMPREGADO"
                            + " WHERE nID_FUNC=" + this.Id + "))");

             if (SemAfastados)//Para os exames do PCMSO (Não gerar com os afastados)
                criteria.Append(" AND nID_EMPREGADO NOT IN "
                                + " (SELECT IdEmpregado FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.Afastamento"
                                + " WHERE DataVolta IS NULL)");

            criteria.Append(" ORDER BY tNO_EMPG");

            List<Empregado> list = new Empregado().Find<Empregado>(criteria.ToString());

            return list;
        }

        public int GetNumeroEmpregadosExpostos(bool SomenteAtivos)
        {
            int ret;

            StringBuilder criteria = new StringBuilder();

            criteria.Append("nID_EMPREGADO IN "
                        + " (SELECT nID_EMPREGADO FROM tblEMPREGADO_FUNCAO WHERE hDt_Termino is null and nID_EMPREGADO_FUNCAO IN "
                        + " (SELECT nID_EMPREGADO_FUNCAO FROM tblFUNC_EMPREGADO WHERE nID_EMPREGADO_FUNCAO IN "
                        + " (SELECT nID_EMPREGADO_FUNCAO FROM dbo.tblEMPREGADO_FUNCAO WHERE nID_FUNC = " + this.Id + "))) ");


            //criteria.Append("nID_EMPREGADO IN "
            //            + " (SELECT nID_EMPREGADO FROM tblEMPREGADO_FUNCAO WHERE nID_EMPREGADO_FUNCAO IN "
            //            + " (SELECT nID_EMPREGADO_FUNCAO FROM tblFUNC_EMPREGADO WHERE nID_EMPREGADO_FUNCAO IN "
            //            + " (SELECT nID_EMPREGADO_FUNCAO FROM dbo.tblEMPREGADO_FUNCAO WHERE nID_FUNC = " + this.Id + "))) ");

            if (SomenteAtivos)
                criteria.Append(" AND hDT_DEM IS NULL"); // Obs: Planvserv

            ret = new Empregado().ExecuteCount(criteria.ToString());

            return ret;
        }


        public int GetNumeroEmpregadosExpostos_Ativos()
        {
            int ret;

            StringBuilder criteria = new StringBuilder();

            criteria.Append("nID_EMPREGADO IN "
                        + " (SELECT nID_EMPREGADO FROM tblEMPREGADO_FUNCAO WHERE nID_EMPREGADO_FUNCAO IN "
                        + " (SELECT nID_EMPREGADO_FUNCAO FROM tblFUNC_EMPREGADO WHERE nID_EMPREGADO_FUNCAO IN "
                        + " (SELECT nID_EMPREGADO_FUNCAO FROM dbo.tblEMPREGADO_FUNCAO WHERE nID_FUNC = " + this.Id + "))) ");



            ret = new Empregado().ExecuteCount(criteria.ToString());

            return ret;
        }


        #endregion

        #region PCMSO

        #region GetListaRiscosPPRA

        public List<PPRA> GetListaRiscosPPRA()//Para o PCMSO
        {
            List<PPRA> listReturn = new List<PPRA>();

            List<PPRA> listPPRA = new PPRA().Find<PPRA>("nID_FUNC=" + this.Id
                                                        + " AND gREMOVE_PCMSO=0"
                                                        + " ORDER BY nID_RSC");
            bool bVal = false;

            foreach (PPRA ppra in listPPRA)
            {
                switch (ppra.nID_RSC.Id)
                {
                    case (int)Riscos.AgentesQuimicos:
                    case (int)Riscos.AgentesQuimicosB:
                    case (int)Riscos.AgentesQuimicosC:
                    case (int)Riscos.AgentesQuimicosD:
                    case (int)Riscos.AsbestosPoeirasMinerais:
                    case (int)Riscos.ACGIH:

                        if (ppra.tVL_MED == 0.0M)
                            bVal = true;
                        else
                            bVal = ppra.Acima50PorCentoDose();
                        break;

                    case (int)Riscos.AgentesQuimicosAnexo13:
                        bVal = true;
                        break;

                    default:
                        bVal = ppra.Acima50PorCentoDose();
                        break;
                }
                if (bVal)
                    listReturn.Add(ppra);
            }

            return listReturn;
        }
        #endregion

        #region GetListaExamesComplementares

        private List<ExameDicionario> GetListaExamesComplementares(PPRA ppra)
        {
            string str;

            List<ExameDicionario> listExameDic;

            if (ppra.nID_RSC.Id == (int)Riscos.RuidoContinuo)
            {
                str = "DependeAgente = 1 AND IdRisco IS NULL";
            }
            else if (ppra.nID_RSC.Id == (int)Riscos.AgentesQuimicosAnexo13)
            {
                str = "DependeAgente = 1"
                    + " AND IdExameDicionario IN"
                    + " (SELECT IdExameDicionario FROM ExameDicionarioAgenteQuimico WHERE"
                    + " IdMateriaPrima=" + ppra.nID_MAT_PRM.Id + ")";
            }
            else if (ppra.nID_RSC.Id == (int)Riscos.AgentesQuimicos
                       || ppra.nID_RSC.Id == (int)Riscos.AgentesQuimicosB
                       || ppra.nID_RSC.Id == (int)Riscos.AgentesQuimicosC
                       || ppra.nID_RSC.Id == (int)Riscos.AgentesQuimicosD
                       || ppra.nID_RSC.Id == (int)Riscos.ACGIH
                       || ppra.nID_RSC.Id == (int)Riscos.AsbestosPoeirasMinerais
                       || ppra.nID_RSC.Id == (int)Riscos.Ergonomicos
                       || ppra.nID_RSC.Id == (int)Riscos.Acidentes)
            {
                str = "DependeAgente = 1"
                    + " AND IdExameDicionario IN"
                    + " (SELECT IdExameDicionario FROM ExameDicionarioAgenteQuimico WHERE"
                    + " IdAgenteQuimico=" + ppra.nID_AG_NCV.Id + ")";
            }
            else
            {
                str = "DependeAgente = 1 AND IdRisco=" + ppra.nID_RSC.Id;
            }

            listExameDic = new ExameDicionario().Find<ExameDicionario>(str);

            return listExameDic;
        }
        #endregion

        #region CriarSugestaoExamesComplementares

        public void CriarSugestaoExamesComplementares(Pcmso pcmso)
        {
            if(pcmso.IsFinalizado)
                throw new Exception("Esse PCMSO está finalizado, não podendo ser recalculado!");

            CriarPcmsoGhe(pcmso);

            new PcmsoPlanejamento().Delete("IdGhe=" + this.Id + " AND Preventivo=0");

            ExameDicionario periodico = new ExameDicionario();
            periodico.Find((int)IndExameClinico.Periodico);

            //Adiciona o exame periódico
            PcmsoPlanejamento.Adiciona(pcmso, this, periodico, false);

            //Adiciona os exames complementares
            List<PPRA> listPPRA = this.GetListaRiscosPPRA();

            foreach (PPRA ppra in listPPRA)
            {
                List<ExameDicionario> listExameDic = GetListaExamesComplementares(ppra);

                foreach (ExameDicionario exameDicionario in listExameDic)
                    PcmsoPlanejamento.Adiciona(pcmso, this, exameDicionario, false);
            }
        }
        #endregion

        #region CriarPcmsoGhe

        public void CriarPcmsoGhe(Pcmso pcmso)
        {
            //Cria o pcmsoGhe
            PcmsoGhe pcmsoGhe = new PcmsoGhe();
            pcmsoGhe.Find("IdPcmso=" + pcmso.Id + " AND IdGhe=" + this.Id);

            if (pcmsoGhe.Id != 0 && pcmsoGhe.IsHouveAtualizacaoManual)
                throw new Exception("Houve atualização manual! esse sugestão não pode ser aplicada.");

            if (pcmsoGhe.Id == 0)
            {
                pcmsoGhe.Inicialize();
                pcmsoGhe.IdGhe.Id = this.Id;
                pcmsoGhe.IdPcmso.Id = pcmso.Id;
                pcmsoGhe.Save();
            }
        }
        #endregion

        #region GetExamesComplementaresParaSolicitacao

        public List<ExameDicionario> GetExamesComplementaresParaSolicitacao(Pcmso pcmso)
        {
            List<ExameDicionario> listExames = new List<ExameDicionario>();

            PcmsoGhe pcmsoGhe = new PcmsoGhe();
            pcmsoGhe.Find("IdPcmso=" + pcmso.Id + " AND IdGhe=" + this.Id);

            if (pcmso.Id != 0 && pcmsoGhe.Id == 0)
                throw new Exception("O PCMSO para esse PPRA não foi realizado!");

            string where = "IdPcmso =" + pcmso.Id
                            + " AND IdGhe =" + this.Id
                            + " AND IdExameDicionario <>" + (int)IndExameClinico.Periodico;

            List<PcmsoPlanejamento> listPlanejamento = new PcmsoPlanejamento().Find<PcmsoPlanejamento>(where);

            listPlanejamento.Sort();

            foreach (PcmsoPlanejamento pcmsoPlan in listPlanejamento)
            {
                pcmsoPlan.IdExameDicionario.Find();

                if (pcmsoPlan.IdExameDicionario.IsObservacao)
                    continue;

                listExames.Add(pcmsoPlan.IdExameDicionario);
            }

            return listExames;
        }
        #endregion

        #region GetExamesComplementaresAbreviado

        public string GetExamesComplementaresAbreviado(Pcmso pcmso)
        {
            StringBuilder str = new StringBuilder();

            if (pcmso.Id == 0)
                return str.ToString();

            PcmsoGhe pcmsoGhe = new PcmsoGhe();
            pcmsoGhe.Find("IdPcmso=" + pcmso.Id + " AND IdGhe=" + this.Id);

            if (pcmso.Id != 0 && pcmsoGhe.Id == 0)
            {
                str.Append("O PCMSO para esse PPRA não foi realizado!");

                return str.ToString();
            }

            string criteria = "IdPcmso=" + pcmso.Id
                            + " AND IdGhe=" + this.Id
                            + " AND IdExameDicionario<>" + (int)IndExameClinico.Periodico;

            List<PcmsoPlanejamento> listPlanejamento = new PcmsoPlanejamento().Find<PcmsoPlanejamento>(criteria);

            listPlanejamento.Sort();

            int i = 1;

            foreach (PcmsoPlanejamento pcmsoPlan in listPlanejamento)
            {
                if (pcmsoPlan.IdExameDicionario.mirrorOld == null)
                    pcmsoPlan.IdExameDicionario.Find();

                str.Append(i.ToString() + ". ");
                str.Append(pcmsoPlan.IdExameDicionario.Nome);
                str.Append("\r\n");

                i++;
            }

            if (str.ToString() == string.Empty)
                str.Append("Exame complementar não indicado.");

            return str.ToString();
        }
        #endregion

        #region GetExamesComplementares

        public string GetExamesComplementares(Pcmso pcmso)
        {
            StringBuilder str = new StringBuilder();

            if (pcmso.Id == 0)
                return str.ToString();

            PcmsoGhe pcmsoGhe = new PcmsoGhe();
            pcmsoGhe.Find("IdPcmso=" + pcmso.Id + " AND IdGhe=" + this.Id);

            //if (pcmso.Id != 0 && pcmsoGhe.Id == 0)
            //{
                //this.CriaSugestaoExamesComplementares(pcmso);
                //pcmsoGhe.Find("IdPcmso=" + pcmso.Id + " AND IdGhe=" + this.Id);
                //if (pcmsoGhe.Id == 0)
                //{
                    //str.Append("Exame complementar não indicado.");
                    //return str.ToString();
                //}
            //    throw new Exception("Exames do PCMSO não está configurado para o " + this.ToString() + ".");
            //}

            string criteria = "IdPcmso=" + pcmso.Id
                            + " AND IdGhe=" + this.Id
                            + " AND IdExameDicionario<>" + (int)IndExameClinico.Periodico;

            List<PcmsoPlanejamento> listPlanejamento = new PcmsoPlanejamento().Find<PcmsoPlanejamento>(criteria);

            listPlanejamento.Sort();

            int i = 1;

            foreach (PcmsoPlanejamento pcmsoPlan in listPlanejamento)
            {
                pcmsoPlan.IdExameDicionario.Find();

                if (pcmsoPlan.IdExameDicionario.Id == (int)Exames.Audiometria)
                {
                    PPRA ppra = new PPRA();
                    ppra.Find("nID_FUNC=" + this.Id + " AND nID_RSC=" + (int)Riscos.RuidoContinuo);
                    
                    if (ppra.tVL_MED < 80.0M)
                    {
                        str.Append(i.ToString() + ". ");
                        
                        if (pcmsoPlan.Preventivo)
                        {
                            if (pcmsoPlan.Observacao == string.Empty)
                                str.Append(@"Exame Audiométrico por iniciativa da empresa.   (cód eSocial:0281)");
                            else
                                str.Append("Exame Audiométrico  (cód eSocial:0281 " +  pcmsoPlan.Observacao + ")");
                        }
                        else
                            str.Append(@"Exame Audiométrico. (cód eSocial:0281)");

                        str.Append("\r\n");
                    }
                    else if (ppra.tVL_MED >= 80.0M && ppra.tVL_MED <= 85.0M)
                    {
                        str.Append(i.ToString() + ". ");
                        
                        if (pcmsoPlan.Preventivo)
                        {
                            if (pcmsoPlan.Observacao == string.Empty)
                                str.Append(@"Exame Audiométrico por iniciativa da empresa.  (cód eSocial:0281)");
                            else
                                str.Append("Exame Audiométrico  (cód eSocial:0281  " + pcmsoPlan.Observacao + ")");
                        }
                        else
                            str.Append(@"Exame Audiométrico indicado.  (cód eSocial:0281)");
                        
                        //str.Append(@" Nível de Ação caracterizado: Deverão ser objeto de controle médico as situações que apresentem exposição ocupacional acima dos níveis de ação, sendo que, para o ruído, a dose é de 0,5 (dose superior a 50% = 80 dB(A)). (NR 9.3.6.2 ""b"").");
                        str.Append("\r\n");
                    }
                    else if (ppra.tVL_MED > 85.0M)
                    {
                        str.Append(i.ToString() + ". ");
                        str.Append(@"Exame Audiométrico obrigatório.  (cód eSocial:0281)");
                        str.Append(" Os níveis de pressão sonora/ruído ultrapassam o limite de tolerância – 85 dB(A) (NR 7, Anexo I, item 3.1).");
                        str.Append("\r\n");
                    }
                }
                else
                {
                    str.Append(i.ToString() + ". ");
                    //str.Append(pcmsoPlan.IdExameDicionario.Descricao);

                    pcmsoPlan.IdExameDicionario.Find();

                    if (pcmsoPlan.IdExameDicionario.Codigo_eSocial != "")
                    {
                        str.Append(pcmsoPlan.IdExameDicionario.Descricao + " (cód.eSocial:" + pcmsoPlan.IdExameDicionario.Codigo_eSocial + ")");
                    }
                    else
                    {
                        str.Append(pcmsoPlan.IdExameDicionario.Descricao);
                    }

                    if (pcmsoPlan.IdExameDicionario.Id == 100)
                    {
                        str.Append(" ( por sorteio randômico )");
                    }
                    else
                    {
                        if (pcmsoPlan.Preventivo)
                        {
                            if (pcmsoPlan.Observacao == string.Empty)
                                str.Append(" *");
                            //str.Append(" (por medidas preventivas)");                
                            else
                                str.Append(" (" + pcmsoPlan.Observacao + ")");
                        }
                    }


                    //checar se têm idade mínima
                    Ilitera.Data.PPRA_EPI xMinimo = new Ilitera.Data.PPRA_EPI();
                    Int16 xIdadeMinima = xMinimo.Trazer_PCMSO_Exame_Idade_Minima(pcmsoPlan.Id);

                    if (xIdadeMinima > 0)
                    {
                        str.Append(" ( Exame aplicável para trabalhadores acima de " + xIdadeMinima.ToString() + " anos )");
                    }


                    str.Append("\r\n");
                }

                i++;
            }

            if (str.ToString() == string.Empty)
                str.Append("Exame complementar não indicado.");
            else if (str.ToString().IndexOf("*") > 0)
            {                
                str.Append("\r\n");
                if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper().IndexOf("PRAJNA") > 0)
                {
                    str.Append("* Exame(s) Preventivo(s).");
                }
                else
                {
                    str.Append("* Exame(s) solicitado(s) pelo cliente (Preventivo).");
                }

            }


            return str.ToString();
        }



        public string GetExamesComplPeriodicidade_Aptidao(Pcmso pcmso, string vExames)
        {
            StringBuilder str = new StringBuilder();

            if (pcmso.Id == 0)
                return str.ToString();

            int zAux = 0;

            for (zAux = 20; zAux > 0; zAux--)
            {
                if (vExames.IndexOf(zAux.ToString().Trim() + ".") >= 0)
                    break;
            }


            PcmsoGhe pcmsoGhe = new PcmsoGhe();
            pcmsoGhe.Find("IdPcmso=" + pcmso.Id + " AND IdGhe=" + this.Id);


            Int32 zIdGHE = this.Id;
            GHE_Aptidao zAptidao = new GHE_Aptidao();
            zAptidao.Find("nId_Func = " + zIdGHE.ToString());

            string xAptidao = "";


            if (zAptidao.Id != 0)
            {



                this.nID_LAUD_TEC.Find();
                this.nID_LAUD_TEC.nID_EMPR.Find();



                if (zAptidao.apt_Alimento == true)
                {

                    string criteria = "nId_Aptidao=" + (Int16)Empregado_Aptidao.Aptidao.Alimento
                    + " AND nId_Empr =" + this.nID_LAUD_TEC.nID_EMPR.Id;

                    ArrayList list = new Empregado_Aptidao_Planejamento().Find(criteria);
                    list.Sort();

                    foreach (Empregado_Aptidao_Planejamento emprPlan in list)
                    {
                        zAux = zAux + 1;
                        emprPlan.IdExameDicionario.Find();
                        xAptidao = xAptidao + " " + zAux.ToString() + ". " + ExameDicionario.GetPeriodicidadeExame(emprPlan) + System.Environment.NewLine;
                    }

                }

                if (zAptidao.apt_Aquaviario == true)
                {

                    string criteria = "nId_Aptidao=" + (Int16)Empregado_Aptidao.Aptidao.Aquaviario
                    + " AND nId_Empr =" + this.nID_LAUD_TEC.nID_EMPR.Id;

                    ArrayList list = new Empregado_Aptidao_Planejamento().Find(criteria);
                    list.Sort();

                    foreach (Empregado_Aptidao_Planejamento emprPlan in list)
                    {
                        zAux = zAux + 1;
                        emprPlan.IdExameDicionario.Find();
                        xAptidao = xAptidao + " " + zAux.ToString() + ". " + ExameDicionario.GetPeriodicidadeExame(emprPlan) + System.Environment.NewLine;
                    }

                }


                if (zAptidao.apt_Eletricidade == true)
                {

                    string criteria = "nId_Aptidao=" + (Int16)Empregado_Aptidao.Aptidao.Eletricidade
                    + " AND nId_Empr =" + this.nID_LAUD_TEC.nID_EMPR.Id;

                    ArrayList list = new Empregado_Aptidao_Planejamento().Find(criteria);
                    list.Sort();

                    foreach (Empregado_Aptidao_Planejamento emprPlan in list)
                    {
                        zAux = zAux + 1;
                        emprPlan.IdExameDicionario.Find();
                        xAptidao = xAptidao + " " + zAux.ToString() + ". " + ExameDicionario.GetPeriodicidadeExame(emprPlan) + System.Environment.NewLine;
                    }

                }


                if (zAptidao.apt_Espaco_Confinado == true)
                {
                    string criteria = "nId_Aptidao=" + (Int16)Empregado_Aptidao.Aptidao.EspacoConfinado
                    + " AND nId_Empr =" + this.nID_LAUD_TEC.nID_EMPR.Id;

                    ArrayList list = new Empregado_Aptidao_Planejamento().Find(criteria);
                    list.Sort();

                    foreach (Empregado_Aptidao_Planejamento emprPlan in list)
                    {
                        zAux = zAux + 1;
                        emprPlan.IdExameDicionario.Find();
                        xAptidao = xAptidao + " " + zAux.ToString() + ". " + ExameDicionario.GetPeriodicidadeExame(emprPlan) + System.Environment.NewLine;
                    }

                }


                if (zAptidao.apt_Submerso == true)
                {
                    string criteria = "nId_Aptidao=" + (Int16)Empregado_Aptidao.Aptidao.Submerso
                    + " AND nId_Empr =" + this.nID_LAUD_TEC.nID_EMPR.Id;

                    ArrayList list = new Empregado_Aptidao_Planejamento().Find(criteria);
                    list.Sort();

                    foreach (Empregado_Aptidao_Planejamento emprPlan in list)
                    {
                        zAux = zAux + 1;
                        emprPlan.IdExameDicionario.Find();
                        xAptidao = xAptidao + " " + zAux.ToString() + ". " + ExameDicionario.GetPeriodicidadeExame(emprPlan) + System.Environment.NewLine;
                    }

                }


                if (zAptidao.apt_Trabalho_Altura == true)
                {
                    string criteria = "nId_Aptidao=" + (Int16)Empregado_Aptidao.Aptidao.TrabalhoAltura
                    + " AND nId_Empr =" + this.nID_LAUD_TEC.nID_EMPR.Id;

                    ArrayList list = new Empregado_Aptidao_Planejamento().Find(criteria);
                    list.Sort();

                    foreach (Empregado_Aptidao_Planejamento emprPlan in list)
                    {
                        zAux = zAux + 1;
                        emprPlan.IdExameDicionario.Find();
                        xAptidao = xAptidao + " " + zAux.ToString() + ". " + ExameDicionario.GetPeriodicidadeExame(emprPlan) + System.Environment.NewLine;
                    }

                }


                if (zAptidao.apt_Transporte == true)
                {
                    string criteria = "nId_Aptidao=" + (Int16)Empregado_Aptidao.Aptidao.Transportes
                    + " AND nId_Empr =" + this.nID_LAUD_TEC.nID_EMPR.Id;

                    ArrayList list = new Empregado_Aptidao_Planejamento().Find(criteria);
                    list.Sort();

                    foreach (Empregado_Aptidao_Planejamento emprPlan in list)
                    {
                        zAux = zAux + 1;
                        emprPlan.IdExameDicionario.Find();
                        xAptidao = xAptidao + " " + zAux.ToString() + ". " + ExameDicionario.GetPeriodicidadeExame(emprPlan) + System.Environment.NewLine;
                    }

                }


                if (zAptidao.apt_Brigadista == true)
                {
                    string criteria = "nId_Aptidao=" + (Int16)Empregado_Aptidao.Aptidao.Brigadista
                    + " AND nId_Empr =" + this.nID_LAUD_TEC.nID_EMPR.Id;

                    ArrayList list = new Empregado_Aptidao_Planejamento().Find(criteria);
                    list.Sort();

                    foreach (Empregado_Aptidao_Planejamento emprPlan in list)
                    {
                        zAux = zAux + 1;
                        emprPlan.IdExameDicionario.Find();
                        xAptidao = xAptidao + " " + zAux.ToString() + ". " + ExameDicionario.GetPeriodicidadeExame(emprPlan) + System.Environment.NewLine;
                    }

                }


                if (zAptidao.apt_PPR == true)
                {
                    string criteria = "nId_Aptidao=" + (Int16)Empregado_Aptidao.Aptidao.PPR
                    + " AND nId_Empr =" + this.nID_LAUD_TEC.nID_EMPR.Id;

                    ArrayList list = new Empregado_Aptidao_Planejamento().Find(criteria);
                    list.Sort();

                    foreach (Empregado_Aptidao_Planejamento emprPlan in list)
                    {
                        zAux = zAux + 1;
                        emprPlan.IdExameDicionario.Find();
                        xAptidao = xAptidao + " " + zAux.ToString() + ". " + ExameDicionario.GetPeriodicidadeExame(emprPlan) + System.Environment.NewLine;
                    }

                }


                if (zAptidao.apt_Radiacao == true)
                {
                    string criteria = "nId_Aptidao=" + (Int16)Empregado_Aptidao.Aptidao.RadiacaoIonizante
                    + " AND nId_Empr =" + this.nID_LAUD_TEC.nID_EMPR.Id;

                    ArrayList list = new Empregado_Aptidao_Planejamento().Find(criteria);
                    list.Sort();

                    foreach (Empregado_Aptidao_Planejamento emprPlan in list)
                    {
                        zAux = zAux + 1;
                        emprPlan.IdExameDicionario.Find();
                        xAptidao = xAptidao + " " + zAux.ToString() + ". " + ExameDicionario.GetPeriodicidadeExame(emprPlan) + System.Environment.NewLine;
                    }

                }


                if (zAptidao.apt_Socorrista == true)
                {
                    string criteria = "nId_Aptidao=" + (Int16)Empregado_Aptidao.Aptidao.Socorrista
                    + " AND nId_Empr =" + this.nID_LAUD_TEC.nID_EMPR.Id;

                    ArrayList list = new Empregado_Aptidao_Planejamento().Find(criteria);
                    list.Sort();

                    foreach (Empregado_Aptidao_Planejamento emprPlan in list)
                    {
                        zAux = zAux + 1;
                        emprPlan.IdExameDicionario.Find();
                        xAptidao = xAptidao + " " + zAux.ToString() + ". " + ExameDicionario.GetPeriodicidadeExame(emprPlan) + System.Environment.NewLine;
                    }

                }




            }


            return xAptidao;


        }


        public string GetExamesComplementares_Aptidao(Pcmso pcmso, string vExames)
        {
            StringBuilder str = new StringBuilder();

            if (pcmso.Id == 0)
                return str.ToString();

            int zAux = 0;

            for (zAux = 20; zAux > 0; zAux--)
            {
                if (vExames.IndexOf(zAux.ToString().Trim() + ".") >= 0)
                    break;
            }


            PcmsoGhe pcmsoGhe = new PcmsoGhe();
            pcmsoGhe.Find("IdPcmso=" + pcmso.Id + " AND IdGhe=" + this.Id);


            Int32 zIdGHE = this.Id;
            GHE_Aptidao zAptidao = new GHE_Aptidao();
            zAptidao.Find("nId_Func = " + zIdGHE.ToString());

            string xAptidao = "";


            if (zAptidao.Id != 0)
            {



                this.nID_LAUD_TEC.Find();
                this.nID_LAUD_TEC.nID_EMPR.Find();



                if (zAptidao.apt_Alimento == true)
                {

                    string criteria = "nId_Aptidao=" + (Int16)Empregado_Aptidao.Aptidao.Alimento
                    + " AND nId_Empr =" + this.nID_LAUD_TEC.nID_EMPR.Id;

                    ArrayList list = new Empregado_Aptidao_Planejamento().Find(criteria);
                    list.Sort();

                    if (list.Count > 0) xAptidao = xAptidao + "Exames prescritos para manipulação de alimentos " + System.Environment.NewLine; ;

                    foreach (Empregado_Aptidao_Planejamento emprPlan in list)
                    {
                        zAux = zAux + 1;
                        emprPlan.IdExameDicionario.Find();
                        xAptidao = xAptidao + "  " + zAux.ToString() + ". " + emprPlan.IdExameDicionario.Nome + System.Environment.NewLine;
                    }

                }

                if (zAptidao.apt_Aquaviario == true)
                {

                    string criteria = "nId_Aptidao=" + (Int16)Empregado_Aptidao.Aptidao.Aquaviario
                    + " AND nId_Empr =" + this.nID_LAUD_TEC.nID_EMPR.Id;

                    ArrayList list = new Empregado_Aptidao_Planejamento().Find(criteria);
                    list.Sort();

                    if (list.Count > 0) xAptidao = xAptidao + "Exames prescritos para serviços aquaviários (NR 30) " + System.Environment.NewLine; ;

                    foreach (Empregado_Aptidao_Planejamento emprPlan in list)
                    {
                        zAux = zAux + 1;
                        emprPlan.IdExameDicionario.Find();
                        xAptidao = xAptidao + "  " + zAux.ToString() + ". " + emprPlan.IdExameDicionario.Nome + System.Environment.NewLine;
                    }

                }


                if (zAptidao.apt_Eletricidade == true)
                {

                    string criteria = "nId_Aptidao=" + (Int16)Empregado_Aptidao.Aptidao.Eletricidade
                    + " AND nId_Empr =" + this.nID_LAUD_TEC.nID_EMPR.Id;

                    ArrayList list = new Empregado_Aptidao_Planejamento().Find(criteria);
                    list.Sort();

                    if (list.Count > 0) xAptidao = xAptidao + "Exames prescritos para serviços em eletricidade (NR 10) " + System.Environment.NewLine; ;

                    foreach (Empregado_Aptidao_Planejamento emprPlan in list)
                    {
                        zAux = zAux + 1;
                        emprPlan.IdExameDicionario.Find();
                        xAptidao = xAptidao + "  " + zAux.ToString() + ". " + emprPlan.IdExameDicionario.Nome + System.Environment.NewLine;
                    }

                }


                if (zAptidao.apt_Espaco_Confinado == true)
                {

                    string criteria = "nId_Aptidao=" + (Int16)Empregado_Aptidao.Aptidao.EspacoConfinado
                    + " AND nId_Empr =" + this.nID_LAUD_TEC.nID_EMPR.Id;

                    ArrayList list = new Empregado_Aptidao_Planejamento().Find(criteria);
                    list.Sort();

                    if (list.Count > 0) xAptidao = xAptidao + "Exames prescritos para trabalho em espaços confinados (NR 33) " + System.Environment.NewLine; ;

                    foreach (Empregado_Aptidao_Planejamento emprPlan in list)
                    {
                        zAux = zAux + 1;
                        emprPlan.IdExameDicionario.Find();
                        xAptidao = xAptidao + "  " + zAux.ToString() + ". " + emprPlan.IdExameDicionario.Nome + System.Environment.NewLine;
                    }

                }


                if (zAptidao.apt_Submerso == true)
                {

                    string criteria = "nId_Aptidao=" + (Int16)Empregado_Aptidao.Aptidao.Submerso
                    + " AND nId_Empr =" + this.nID_LAUD_TEC.nID_EMPR.Id;

                    ArrayList list = new Empregado_Aptidao_Planejamento().Find(criteria);
                    list.Sort();

                    if (list.Count > 0) xAptidao = xAptidao + "Exames prescritos para atividades submersas (NR 15) " + System.Environment.NewLine; ;

                    foreach (Empregado_Aptidao_Planejamento emprPlan in list)
                    {
                        zAux = zAux + 1;
                        emprPlan.IdExameDicionario.Find();
                        xAptidao = xAptidao + "  " + zAux.ToString() + ". " + emprPlan.IdExameDicionario.Nome + System.Environment.NewLine;
                    }

                }


                if (zAptidao.apt_Trabalho_Altura == true)
                {

                    string criteria = "nId_Aptidao=" + (Int16)Empregado_Aptidao.Aptidao.TrabalhoAltura
                    + " AND nId_Empr =" + this.nID_LAUD_TEC.nID_EMPR.Id;

                    ArrayList list = new Empregado_Aptidao_Planejamento().Find(criteria);
                    list.Sort();

                    if (list.Count > 0) xAptidao = xAptidao + "Exames prescritos para trabalho em altura (NR 35) " + System.Environment.NewLine; ;

                    foreach (Empregado_Aptidao_Planejamento emprPlan in list)
                    {
                        zAux = zAux + 1;
                        emprPlan.IdExameDicionario.Find();
                        xAptidao = xAptidao + "  " + zAux.ToString() + ". " + emprPlan.IdExameDicionario.Nome + System.Environment.NewLine;
                    }

                }


                if (zAptidao.apt_Transporte == true)
                {

                    string criteria = "nId_Aptidao=" + (Int16)Empregado_Aptidao.Aptidao.Transportes
                    + " AND nId_Empr =" + this.nID_LAUD_TEC.nID_EMPR.Id;

                    ArrayList list = new Empregado_Aptidao_Planejamento().Find(criteria);
                    list.Sort();

                    if (list.Count > 0) xAptidao = xAptidao + "Exames prescritos para operar equiptos de transporte motorizados (NR 11) " + System.Environment.NewLine; ;

                    foreach (Empregado_Aptidao_Planejamento emprPlan in list)
                    {
                        zAux = zAux + 1;
                        emprPlan.IdExameDicionario.Find();
                        xAptidao = xAptidao + "  " + zAux.ToString() + ". " + emprPlan.IdExameDicionario.Nome + System.Environment.NewLine;
                    }

                }


                if (zAptidao.apt_Brigadista == true)
                {

                    string criteria = "nId_Aptidao=" + (Int16)Empregado_Aptidao.Aptidao.Brigadista
                    + " AND nId_Empr =" + this.nID_LAUD_TEC.nID_EMPR.Id;

                    ArrayList list = new Empregado_Aptidao_Planejamento().Find(criteria);
                    list.Sort();

                    if (list.Count > 0) xAptidao = xAptidao + "Exames prescritos para trabalho como Brigadista " + System.Environment.NewLine; ;

                    foreach (Empregado_Aptidao_Planejamento emprPlan in list)
                    {
                        zAux = zAux + 1;
                        emprPlan.IdExameDicionario.Find();
                        xAptidao = xAptidao + "  " + zAux.ToString() + ". " + emprPlan.IdExameDicionario.Nome + System.Environment.NewLine;
                    }

                }


                if (zAptidao.apt_PPR == true)
                {

                    string criteria = "nId_Aptidao=" + (Int16)Empregado_Aptidao.Aptidao.PPR
                    + " AND nId_Empr =" + this.nID_LAUD_TEC.nID_EMPR.Id;

                    ArrayList list = new Empregado_Aptidao_Planejamento().Find(criteria);
                    list.Sort();

                    if (list.Count > 0) xAptidao = xAptidao + "Exames prescritos para colaborador em PPR " + System.Environment.NewLine; ;

                    foreach (Empregado_Aptidao_Planejamento emprPlan in list)
                    {
                        zAux = zAux + 1;
                        emprPlan.IdExameDicionario.Find();
                        xAptidao = xAptidao + "  " + zAux.ToString() + ". " + emprPlan.IdExameDicionario.Nome + System.Environment.NewLine;
                    }

                }


                if (zAptidao.apt_Socorrista == true)
                {

                    string criteria = "nId_Aptidao=" + (Int16)Empregado_Aptidao.Aptidao.Socorrista
                    + " AND nId_Empr =" + this.nID_LAUD_TEC.nID_EMPR.Id;

                    ArrayList list = new Empregado_Aptidao_Planejamento().Find(criteria);
                    list.Sort();

                    if (list.Count > 0) xAptidao = xAptidao + "Exames prescritos para trabalho como Socorrista " + System.Environment.NewLine; ;

                    foreach (Empregado_Aptidao_Planejamento emprPlan in list)
                    {
                        zAux = zAux + 1;
                        emprPlan.IdExameDicionario.Find();
                        xAptidao = xAptidao + "  " + zAux.ToString() + ". " + emprPlan.IdExameDicionario.Nome + System.Environment.NewLine;
                    }

                }


            }


            return xAptidao;


        }



        #endregion

        #region GetExamesComplPeriodicidade

        public string GetExamesComplPeriodicidade(Pcmso pcmso)
        {
            StringBuilder str = new StringBuilder();

            if (pcmso.Id == 0)
                return str.ToString();

            string criteria = "IdPcmso=" + pcmso.Id
                            + " AND IdGhe=" + this.Id
                            + " AND IdExameDicionario<>" + (int)IndExameClinico.Periodico;

            List<PcmsoPlanejamento> listPcmso = new PcmsoPlanejamento().Find<PcmsoPlanejamento>(criteria);

            listPcmso.Sort();

            int i = 1;

            foreach (PcmsoPlanejamento pcmsoPlan in listPcmso)
            {
                pcmsoPlan.IdExameDicionario.Find();

                //if (!pcmsoPlan.Preventivo && pcmsoPlan.IdExameDicionario.Id == 6)//Audiometria
                //    str.Append(i.ToString() + ". " + "Na admissão e após 6 meses. Depois, anualmente e na demissão. (NR 7 – Anexo I, 3.4.1)" + "\r\n");
                //else
                str.Append(i.ToString() + ". " + ExameDicionario.GetPeriodicidadeExame(pcmsoPlan) + "\r\n");


                if (Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper().IndexOf("GLOBAL") > 0)
                {
                    if (pcmsoPlan.IdExameDicionario.Nome.ToUpper().IndexOf("ELETROCARDIO") >= 0 ||
                                           pcmsoPlan.IdExameDicionario.Nome.ToUpper().IndexOf("ELETROENCEF") >= 0 ||
                                           pcmsoPlan.IdExameDicionario.Nome.ToUpper().IndexOf("ESPIROMETRIA") >= 0 ||
                                           pcmsoPlan.IdExameDicionario.Nome.ToUpper().IndexOf("GLICEMIA") >= 0 ||
                                           pcmsoPlan.IdExameDicionario.Nome.ToUpper().IndexOf("HEMOGRAMA") >= 0 ||
                                           pcmsoPlan.IdExameDicionario.Nome.ToUpper().IndexOf("RADIOGRAFIA T") >= 0 ||
                                           pcmsoPlan.IdExameDicionario.Nome.ToUpper().IndexOf("ACIDO HIPURICO - URINÁRIO") >= 0 ||
                                           pcmsoPlan.IdExameDicionario.Nome.ToUpper().IndexOf("ÁCIDO METILHIP") >= 0 ||
                                           pcmsoPlan.IdExameDicionario.Nome.ToUpper().IndexOf("ANTI HBS") >= 0 ||
                                           pcmsoPlan.IdExameDicionario.Nome.ToUpper().IndexOf("ANTI HCV") >= 0 ||
                                           pcmsoPlan.IdExameDicionario.Nome.ToUpper().IndexOf("CÁDMIO") >= 0 ||
                                           pcmsoPlan.IdExameDicionario.Nome.ToUpper().IndexOf("CHUMBO URIN") >= 0 ||
                                           pcmsoPlan.IdExameDicionario.Nome.ToUpper().IndexOf("CROMO - URIN") >= 0 ||
                                           pcmsoPlan.IdExameDicionario.Nome.ToUpper().IndexOf("GAMA GT") >= 0 ||
                                           pcmsoPlan.IdExameDicionario.Nome.ToUpper().IndexOf("HBS AG") >= 0 ||
                                           pcmsoPlan.IdExameDicionario.Nome.ToUpper().IndexOf("MANGANÊS - URINÁRIO") >= 0 ||
                                           pcmsoPlan.IdExameDicionario.Nome.ToUpper().IndexOf("P-AMINOFENOL") >= 0 ||
                                           pcmsoPlan.IdExameDicionario.Nome.ToUpper().IndexOf("SANGUE - PLAQUETAS") >= 0)
                    {
                        //tenho que checar se demissão estava marcada para esse exame - está mandando a mensagem abaixo mesmo sem a demissão
                        // talvez ver se xPosit está bem próximo do final, senão não colocar a mensagem nem *


                        int xPosit = str.ToString().LastIndexOf("Na Demissão");

                        if (xPosit > str.Length - 50)
                        {

                            if (xPosit >= 0)
                            {
                                str.Insert(xPosit + 11, '*');
                            }
                                                        
                            str.Append("(*A ser realizado desde que esteja fora da janela de previsão no exame periódico)" + "\r\n");

                            //pediram para colocar asteriscos no retorno ao trabalo e mudança de função
                            xPosit = str.ToString().LastIndexOf("Retorno ao Trabalho");
                            if (xPosit > str.Length - 180)
                            {
                                if (xPosit >= 0)
                                {
                                    str.Insert(xPosit + 19, '*');
                                }
                            }

                            xPosit = str.ToString().LastIndexOf("Mudança de Função");
                            if (xPosit > str.Length - 180)
                            {
                                if (xPosit >= 0)
                                {
                                    str.Insert(xPosit + 17, '*');
                                }
                            }

                        }
                    }

                 
                }

                i++;
            }

            if (str.ToString() == string.Empty)
                str.Append("");

            return str.ToString();
        }



        public string GetExamesComplPeriodicidade_Quadro(Pcmso pcmso)
        {
            StringBuilder str = new StringBuilder();

            if (pcmso.Id == 0)
                return str.ToString();

            string criteria = "IdPcmso=" + pcmso.Id
                            + " AND IdGhe=" + this.Id
                            + " AND IdExameDicionario<>" + (int)IndExameClinico.Periodico;

            List<PcmsoPlanejamento> listPcmso = new PcmsoPlanejamento().Find<PcmsoPlanejamento>(criteria);

            listPcmso.Sort();

            int i = 1;

            foreach (PcmsoPlanejamento pcmsoPlan in listPcmso)
            {
                pcmsoPlan.IdExameDicionario.Find();

                str.Append(i.ToString() + ". " + ExameDicionario.GetPeriodicidadeExame(pcmsoPlan) + "\r\n");

                if (Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper().IndexOf("GLOBAL") > 0)
                {
                    if (pcmsoPlan.IdExameDicionario.Nome.ToUpper().IndexOf("ELETROCARDIO") >= 0 ||
                                           pcmsoPlan.IdExameDicionario.Nome.ToUpper().IndexOf("ELETROENCEF") >= 0 ||
                                           pcmsoPlan.IdExameDicionario.Nome.ToUpper().IndexOf("ESPIROMETRIA") >= 0 ||
                                           pcmsoPlan.IdExameDicionario.Nome.ToUpper().IndexOf("GLICEMIA") >= 0 ||
                                           pcmsoPlan.IdExameDicionario.Nome.ToUpper().IndexOf("HEMOGRAMA") >= 0 ||
                                           pcmsoPlan.IdExameDicionario.Nome.ToUpper().IndexOf("RADIOGRAFIA T") >= 0 ||
                                           pcmsoPlan.IdExameDicionario.Nome.ToUpper().IndexOf("ACIDO HIPURICO - URINÁRIO") >= 0 ||
                                           pcmsoPlan.IdExameDicionario.Nome.ToUpper().IndexOf("ÁCIDO METILHIP") >= 0 ||
                                           pcmsoPlan.IdExameDicionario.Nome.ToUpper().IndexOf("ANTI HBS") >= 0 ||
                                           pcmsoPlan.IdExameDicionario.Nome.ToUpper().IndexOf("ANTI HCV") >= 0 ||
                                           pcmsoPlan.IdExameDicionario.Nome.ToUpper().IndexOf("CÁDMIO") >= 0 ||
                                           pcmsoPlan.IdExameDicionario.Nome.ToUpper().IndexOf("CHUMBO URIN") >= 0 ||
                                           pcmsoPlan.IdExameDicionario.Nome.ToUpper().IndexOf("CROMO - URIN") >= 0 ||
                                           pcmsoPlan.IdExameDicionario.Nome.ToUpper().IndexOf("GAMA GT") >= 0 ||
                                           pcmsoPlan.IdExameDicionario.Nome.ToUpper().IndexOf("HBS AG") >= 0 ||
                                           pcmsoPlan.IdExameDicionario.Nome.ToUpper().IndexOf("MANGANÊS - URINÁRIO") >= 0 ||
                                           pcmsoPlan.IdExameDicionario.Nome.ToUpper().IndexOf("P-AMINOFENOL") >= 0 ||
                                           pcmsoPlan.IdExameDicionario.Nome.ToUpper().IndexOf("SANGUE - PLAQUETAS") >= 0)
                    {
                        //tenho que checar se demissão estava marcada para esse exame - está mandando a mensagem abaixo mesmo sem a demissão
                        // talvez ver se xPosit está bem próximo do final, senão não colocar a mensagem nem *


                        int xPosit = str.ToString().LastIndexOf("Na Demissão");

                        if (xPosit > str.Length - 50)
                        {

                            if (xPosit >= 0)
                            {
                                str.Insert(xPosit + 11, '*');
                            }


                            str.Append("(*A ser realizado desde que esteja fora da janela de previsão no exame periódico)" + "\r\n");

                            //pediram para colocar asteriscos no retorno ao trabalo e mudança de função
                            xPosit = str.ToString().LastIndexOf("Retorno ao Trabalho");
                            if (xPosit > str.Length - 180)
                            {
                                if (xPosit >= 0)
                                {
                                    str.Insert(xPosit + 19, '*');
                                }
                            }

                            xPosit = str.ToString().LastIndexOf("Mudança de Função");
                            if (xPosit > str.Length - 180)
                            {
                                if (xPosit >= 0)
                                {
                                    str.Insert(xPosit + 17, '*');
                                }
                            }
                        }
                    }
                                       
                }

                i++;
            }


            if (str.ToString() == string.Empty)
                str.Append("");

            return str.ToString();
        }



        public string GetPeriodicoPeriodicidade_Quadro(Pcmso pcmso)
        {
            StringBuilder str = new StringBuilder();

            if (pcmso.Id == 0)
                return str.ToString();

            string criteria = "IdPcmso=" + pcmso.Id
                            + " AND IdGhe=" + this.Id
                            + " AND IdExameDicionario=" + (int)IndExameClinico.Periodico;

            List<PcmsoPlanejamento> listPcmso = new PcmsoPlanejamento().Find<PcmsoPlanejamento>(criteria);

            listPcmso.Sort();

            int i = 1;

            foreach (PcmsoPlanejamento pcmsoPlan in listPcmso)
            {
                pcmsoPlan.IdExameDicionario.Find();

                str.Append(i.ToString() + ". " + ExameDicionario.GetPeriodicidadeExame(pcmsoPlan) + "\r\n");

                i++;
            }

            if (str.ToString() == string.Empty)
                str.Append("");

            return str.ToString();
        }


        #endregion

        #endregion

        #region MapaRisco

        public string GetNomeGheMapaRisco()
        {
            return "<P align='center'>" + @"<FONT color=""#003300"" size=4>" + this.tNO_FUNC + "</FONT>";
        }

        public string GetMedidasControleParaMapaRisco()
        {
            string strEpi = this.Epi("; ");
            string strEpc = this.Epc();

            StringBuilder str = new StringBuilder();
            str.Append("<P align='center'>");
           
            if (strEpi != Ghe.strInexistente)
            {
                str.Append(@"<FONT color=""#003300"" size=3>" + strEpi + "</FONT>");
                str.Append("<br>");
            }
            
            if (strEpc != Ghe.strInexistente)
            {
                str.Append("<br>&nbsp;<br>"); 
                str.Append(@"<FONT color=""#003300"" size=3>" + strEpc + "</FONT>");
            }

            return str.ToString();
        }

        public string GetReconhecimentoMapaRisco(ArrayList listPPRA)
        {
            StringBuilder str = new StringBuilder();
            str.Append("<P align='left'>");

            int i = 1;

            foreach (PPRA ppra in listPPRA)
            {
                str.Append(@"<FONT color=""#003300"" size=3>" + i++ + ") " + ppra.GetNomeAgenteResumido() + "</FONT>");
                str.Append("<br>");
            }

            if (listPPRA.Count == 0)
                str.Append("-");

            return str.ToString();
        }

        public string GetAvaliacaoMapaRisco(ArrayList listPPRA)
        {
            StringBuilder str = new StringBuilder();
            str.Append("<P align='left'>");

            int i = 1;

            foreach (PPRA ppra in listPPRA)
            {
                if (ppra.gINSALUBRE && !ppra.nID_RSC.IsQualitativo() && ppra.tVL_MED != 0)
                    str.Append(@"<FONT color=""#ff0033"" size=3>" + i++ + ") " + ppra.GetAvaliacaoQuantitativaMapaRisco() + "</FONT>");
                else
                    str.Append(@"<FONT color=""#003300"" size=3>" + i++ + ") " + ppra.GetAvaliacaoQuantitativaMapaRisco() + "</FONT>");
                
                str.Append("<br>");
            }

            if (listPPRA.Count == 0)
                str.Append("-");

            return str.ToString();
        }


        public string GetLimiteToleranciaMapaRisco(ArrayList listPPRA)
        {
            StringBuilder str = new StringBuilder();
            str.Append("<P align='left'>");

            int i = 1;

            foreach (PPRA ppra in listPPRA)
            {
                str.Append(@"<FONT color=""#003300"" size=3>" + i++ + ") " + ppra.GetLimiteTolerancia() + "</FONT>");
                str.Append("<br>");
            }

            if (listPPRA.Count == 0)
                str.Append("-");

            return str.ToString();
        }

        #endregion

        #endregion
        #endregion
    }


    [Database("sied_novo", "tblFUNC_eSocial", "nID_FUNC_eSocial")]
    public class Ghe_eSocial : Ilitera.Data.Table
    {

        private int _nID_FUNC_eSocial;
        private int _nID_FUNC;
        private string _Codigo = string.Empty;

        public Ghe_eSocial()
        {

        }

        public override int Id
        {
            get { return _nID_FUNC_eSocial; }
            set { _nID_FUNC_eSocial = value; }
        }
        public int nID_Func
        {
            get { return _nID_FUNC; }
            set { _nID_FUNC = value; }
        }

        public string Codigo
        {
            get { return _Codigo; }
            set { _Codigo = value; }
        }
    }






    [Database("sied_novo", "tblFUNC_APTIDAO", "nID_FUNC_APTIDAO")]
    public class GHE_Aptidao : Ilitera.Data.Table
    {

        public enum Aptidao : int
        {
            EspacoConfinado = 1,
            TrabalhoAltura,
            Transportes,
            Submerso,
            Eletricidade,
            Aquaviario,
            Alimento,
            Brigadista,
            Socorrista,
            PPR
        }

        private int _nID_FUNC_APTIDAO;
        private int _nID_FUNC;
        private bool _apt_Espaco_Confinado;
        private bool _apt_Trabalho_Altura;
        private bool _apt_Transporte;
        private bool _apt_Submerso;
        private bool _apt_Eletricidade;
        private bool _apt_Aquaviario;
        private bool _apt_Alimento;
        private bool _apt_Brigadista;
        private bool _apt_Socorrista;
        private bool _apt_PPR;
        private bool _apt_Radiacao;
        private bool _apt_Trabalho_Bordo;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public GHE_Aptidao()
        {

        }

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public GHE_Aptidao(int id)
        {
            this.Find(id);
        }

        public override int Id
        {
            get { return _nID_FUNC_APTIDAO; }
            set { _nID_FUNC_APTIDAO = value; }
        }

        public int nId_FUNC
        {
            get { return _nID_FUNC; }
            set { _nID_FUNC = value; }
        }

        public bool apt_Espaco_Confinado
        {
            get { return _apt_Espaco_Confinado; }
            set { _apt_Espaco_Confinado = value; }
        }
        public bool apt_Trabalho_Altura
        {
            get { return _apt_Trabalho_Altura; }
            set { _apt_Trabalho_Altura = value; }
        }
        public bool apt_Transporte
        {
            get { return _apt_Transporte; }
            set { _apt_Transporte = value; }
        }
        public bool apt_Submerso
        {
            get { return _apt_Submerso; }
            set { _apt_Submerso = value; }
        }
        public bool apt_Eletricidade
        {
            get { return _apt_Eletricidade; }
            set { _apt_Eletricidade = value; }
        }
        public bool apt_Aquaviario
        {
            get { return _apt_Aquaviario; }
            set { _apt_Aquaviario = value; }
        }
        public bool apt_Alimento
        {
            get { return _apt_Alimento; }
            set { _apt_Alimento = value; }
        }
        public bool apt_Brigadista
        {
            get { return _apt_Brigadista; }
            set { _apt_Brigadista = value; }
        }
        public bool apt_Socorrista
        {
            get { return _apt_Socorrista; }
            set { _apt_Socorrista = value; }
        }
        public bool apt_PPR
        {
            get { return _apt_PPR; }
            set { _apt_PPR = value; }
        }
        public bool apt_Radiacao
        {
            get { return _apt_Radiacao; }
            set { _apt_Radiacao = value; }
        }
        public bool apt_Trabalho_Bordo
        {
            get { return _apt_Trabalho_Bordo; }
            set { _apt_Trabalho_Bordo = value; }
        }

    }



    [Database("sied_novo", "tblFUNC_Anexos", "nID_FUNC_Anexos")]
    public class Ghe_Anexos : Ilitera.Data.Table
    {

        private int _nID_FUNC_Anexos;
        private int _nID_FUNC;
        private string _Descricao = string.Empty;
        private string _Arquivo = string.Empty;

        public Ghe_Anexos()
        {

        }

        public override int Id
        {
            get { return _nID_FUNC_Anexos; }
            set { _nID_FUNC_Anexos = value; }
        }
        public int nID_Func
        {
            get { return _nID_FUNC; }
            set { _nID_FUNC = value; }
        }

        public string Descricao
        {
            get { return _Descricao; }
            set { _Descricao = value; }
        }
        public string Arquivo
        {
            get { return _Arquivo; }
            set { _Arquivo = value; }
        }
    }



}