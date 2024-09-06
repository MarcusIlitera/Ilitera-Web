using System;
using System.Text;
using System.Data;
using System.Collections;
using Ilitera.Data;

namespace Ilitera.Opsa.Data
{
    [Database("sied_novo", "tblPPRA1", "nID_PPRA")]
    public class PPRA : Ilitera.Data.Table
    {
        #region Enum

        public enum AtividadeFisica : int
        {
            Leve = 1,
            Moderada,
            Pesada
        }

        public enum PenetracaoOrganismoAgenteQuimico : int
        {
            NaoInformada,
            Inalacao,
            ContatoDermal,
            InalacaoContatoDermal
        }

        public enum ModoExposicao : int
        {
            Permanente,
            OcasionalIntermitente,
            Eventual,
            HabitualPermanente,
            HabitualIntermitente
        }

        public enum FormasAgenteQuimico : int
        {
            NaoInformada,
            Poeiras,
            Fumos,
            Nevoas,
            Neblinas,
            Gases,
            Vapores
        }
        #endregion

        #region Properties

        private int _nID_PPRA;
        private LaudoTecnico _nID_LAUD_TEC;
        private Ghe _nID_FUNC;
        private Risco _nID_RSC;
        private AgenteQuimico _nID_AG_NCV;
        private MateriaPrima _nID_MAT_PRM;
        private int _nIND_PENETR_ORG_AG_NCV;
        private int _nIND_FORMA_AG_NCV;
        private EquipamentoMedicao _nID_EQP_MED;
        //		private string _tOBS_EQP_MED=string.Empty;
        private int _nID_ATV;
        private string _tPREFIXO_VL_MED = string.Empty;
        private decimal _tVL_MED;
        private Unidade _nID_UN;
        private string _tVL_LIM_TOL = string.Empty;
        private decimal _sVL_LIM_TOL;
        //		private string _tID_INTS=string.Empty;
        //		private string _tID_MOD_EXP = "Habitual e Permanente";
        private int _nIND_MOD_EXP;
        private TrajetoriaRiscos _nID_TRJ;
        private MeioPropagacao _nID_MEIO_PRPG;
        private string _mDS_FRS_RSC = string.Empty;
        private string _tDS_FTE_GER = string.Empty;
        private string _tDS_MAT_PRM = string.Empty;
        private string _tDS_DANO_REL_SAU = string.Empty;
        private string _mDS_EPC_RCM = string.Empty;
        private string _mDS_EPC_EXTE = string.Empty;
        private string _mDS_EPI_RCM = string.Empty;
        private string _mDS_EPI_EXTE = string.Empty;
        private string _tOBS_AG_NCV = string.Empty;
        private string _mDS_MED_CTL_CAR_ADM = string.Empty;
        private string _mDS_MED_CTL_CAR_EDC = string.Empty;
        private AtividadeAgenteBiologico _nID_TP_ATV;
        private bool _gINSALUBRE;
        private bool _gNET;
        private bool _gREMOVE_PCMSO;
        private bool _bDESC;
        private double _Mt;
        private double _Tt;
        private double _Md;
        private double _Td;
        private double _IBUTGt;
        private double _IBUTGd;
        private double _M;
        private double _IBUTG;
        private bool _Risco_Insignificante;
        private bool _Fontes_Artificiais;

        private string _tPREFIXO_VL_MED2 = string.Empty;
        private decimal _tVL_MED2;
        private Unidade _nID_UN2;
        private string _tVL_LIM_TOL2 = string.Empty;
        private decimal _sVL_LIM_TOL2;

        private string _Codigo_eSocial;

        private bool _Calculo_Limite_Calor;

        private bool _Inserir_eSocial;
        private bool _Limite_eSocial;

        private bool _PGR_Personalizar;
        private short _Probabilidade;
        private short _Severidade;
        private short _Nivel_Risco;
        private decimal _Probabilidade_Aut;
        private decimal _Severidade_Aut;
        private short _Nivel_Risco_Aut;

        private short _nId_Tempo_Exposicao;

        private string _Acao;
        private string _Metodologia;
        private string _Forma_Registro;
        private string _Prazo;

        private int _nId_Risco;

        private string _Periodo;
        private string _Afericao;
        private short _Prioridade;
        private bool _Cron_Jan;
        private bool _Cron_Fev;
        private bool _Cron_Mar;
        private bool _Cron_Abr;
        private bool _Cron_Mai;
        private bool _Cron_Jun;
        private bool _Cron_Jul;
        private bool _Cron_Ago;
        private bool _Cron_Set;
        private bool _Cron_Out;
        private bool _Cron_Nov;
        private bool _Cron_Dez;

        private bool _Inserir_ASO;

        private decimal _tVL_MED_Ruido_LTCAT;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public PPRA()
        {

        }
        public override int Id
        {
            get { return _nID_PPRA; }
            set { _nID_PPRA = value; }
        }
        //		public string tID_INTS
        //		{														  
        //			get	{return _tID_INTS;}
        //			set {_tID_INTS = value;}
        //		}
        public int nIND_PENETR_ORG_AG_NCV
        {
            get { return _nIND_PENETR_ORG_AG_NCV; }
            set { _nIND_PENETR_ORG_AG_NCV = value; }
        }
        public int nIND_FORMA_AG_NCV
        {
            get { return _nIND_FORMA_AG_NCV; }
            set { _nIND_FORMA_AG_NCV = value; }
        }
        public TrajetoriaRiscos nID_TRJ
        {
            get { return _nID_TRJ; }
            set { _nID_TRJ = value; }
        }
        public MeioPropagacao nID_MEIO_PRPG
        {
            get { return _nID_MEIO_PRPG; }
            set { _nID_MEIO_PRPG = value; }
        }
        public LaudoTecnico nID_LAUD_TEC
        {
            get { return _nID_LAUD_TEC; }
            set { _nID_LAUD_TEC = value; }
        }
        public string tDS_DANO_REL_SAU
        {
            get { return _tDS_DANO_REL_SAU; }
            set { _tDS_DANO_REL_SAU = value; }
        }
        public string mDS_FRS_RSC
        {
            get { return _mDS_FRS_RSC; }
            set { _mDS_FRS_RSC = value; }
        }
        public Ghe nID_FUNC
        {
            get { return _nID_FUNC; }
            set { _nID_FUNC = value; }
        }
        public Risco nID_RSC
        {
            get { return _nID_RSC; }
            set { _nID_RSC = value; }
        }
        public AgenteQuimico nID_AG_NCV
        {
            get { return _nID_AG_NCV; }
            set { _nID_AG_NCV = value; }
        }
        public MateriaPrima nID_MAT_PRM
        {
            get { return _nID_MAT_PRM; }
            set { _nID_MAT_PRM = value; }
        }
        public string tPREFIXO_VL_MED
        {
            get { return _tPREFIXO_VL_MED; }
            set { _tPREFIXO_VL_MED = value; }
        }
        public decimal tVL_MED
        {
            get { return _tVL_MED; }
            set { _tVL_MED = value; }
        }
        public Unidade nID_UN
        {
            get { return _nID_UN; }
            set { _nID_UN = value; }
        }
        public string tVL_LIM_TOL
        {
            get { return _tVL_LIM_TOL; }
            set { _tVL_LIM_TOL = value; }
        }
        public decimal sVL_LIM_TOL
        {
            get { return _sVL_LIM_TOL; }
            set { _sVL_LIM_TOL = value; }
        }
        public int nID_ATV
        {
            get { return _nID_ATV; }
            set { _nID_ATV = value; }
        }
        public EquipamentoMedicao nID_EQP_MED
        {
            get { return _nID_EQP_MED; }
            set { _nID_EQP_MED = value; }
        }
        //		public string tOBS_EQP_MED
        //		{														  
        //			get	{return _tOBS_EQP_MED;}
        //			set {_tOBS_EQP_MED = value;}
        //		}
        //		public  string tID_MOD_EXP
        //		{														  
        //			get	{return _tID_MOD_EXP;}
        //			set {_tID_MOD_EXP = value;}
        //		}
        public int nIND_MOD_EXP
        {
            get { return _nIND_MOD_EXP; }
            set { _nIND_MOD_EXP = value; }
        }
        public string tDS_FTE_GER
        {
            get { return _tDS_FTE_GER; }
            set { _tDS_FTE_GER = value; }
        }
        public string tDS_MAT_PRM
        {
            get { return _tDS_MAT_PRM; }
            set { _tDS_MAT_PRM = value; }
        }
        public string mDS_EPC_RCM
        {
            get { return _mDS_EPC_RCM; }
            set { _mDS_EPC_RCM = value; }
        }
        public string mDS_EPC_EXTE
        {
            get { return _mDS_EPC_EXTE; }
            set { _mDS_EPC_EXTE = value; }
        }
        public string mDS_EPI_EXTE
        {
            get { return _mDS_EPI_EXTE; }
            set { _mDS_EPI_EXTE = value; }
        }
        public string mDS_EPI_RCM
        {
            get { return _mDS_EPI_RCM; }
            set { _mDS_EPI_RCM = value; }
        }
        public string mDS_MED_CTL_CAR_ADM
        {
            get { return _mDS_MED_CTL_CAR_ADM; }
            set { _mDS_MED_CTL_CAR_ADM = value; }
        }
        public string mDS_MED_CTL_CAR_EDC
        {
            get { return _mDS_MED_CTL_CAR_EDC; }
            set { _mDS_MED_CTL_CAR_EDC = value; }
        }
        public string tOBS_AG_NCV
        {
            get { return _tOBS_AG_NCV; }
            set { _tOBS_AG_NCV = value; }
        }
        public AtividadeAgenteBiologico nID_TP_ATV
        {
            get { return _nID_TP_ATV; }
            set { _nID_TP_ATV = value; }
        }
        public bool gINSALUBRE
        {
            get { return _gINSALUBRE; }
            set { _gINSALUBRE = value; }
        }
        public bool gNET
        {
            get { return _gNET; }
            set { _gNET = value; }
        }
        public bool bDESC
        {
            get { return _bDESC; }
            set { _bDESC = value; }
        }
        public bool gREMOVE_PCMSO
        {
            get { return _gREMOVE_PCMSO; }
            set { _gREMOVE_PCMSO = value; }
        }
        public double Mt
        {
            get { return _Mt; }
            set { _Mt = value; }
        }
        public double Tt
        {
            get { return _Tt; }
            set { _Tt = value; }
        }
        public double Md
        {
            get { return _Md; }
            set { _Md = value; }
        }
        public double Td
        {
            get { return _Td; }
            set { _Td = value; }
        }
        public double IBUTGt
        {
            get { return _IBUTGt; }
            set { _IBUTGt = value; }
        }
        public double IBUTGd
        {
            get { return _IBUTGd; }
            set { _IBUTGd = value; }
        }
        public double M
        {
            get { return _M; }
            set { _M = value; }
        }
        public double IBUTG
        {
            get { return _IBUTG; }
            set { _IBUTG = value; }
        }

        public bool Risco_Insignificante
        {
            get { return _Risco_Insignificante; }
            set { _Risco_Insignificante = value; }
        }
        public bool Fontes_Artificiais
        {
            get { return _Fontes_Artificiais; }
            set { _Fontes_Artificiais = value; }
        }




        public string tPREFIXO_VL_MED2
        {
            get { return _tPREFIXO_VL_MED2; }
            set { _tPREFIXO_VL_MED2 = value; }
        }
        public decimal tVL_MED2
        {
            get { return _tVL_MED2; }
            set { _tVL_MED2 = value; }
        }
        public Unidade nID_UN2
        {
            get { return _nID_UN2; }
            set { _nID_UN2 = value; }
        }
        public string tVL_LIM_TOL2
        {
            get { return _tVL_LIM_TOL2; }
            set { _tVL_LIM_TOL2 = value; }
        }
        public decimal sVL_LIM_TOL2
        {
            get { return _sVL_LIM_TOL2; }
            set { _sVL_LIM_TOL2 = value; }
        }

        public string Codigo_eSocial
        {
            get { return _Codigo_eSocial; }
            set { _Codigo_eSocial = value; }
        }

        public bool Calculo_Limite_Calor
        {
            get { return _Calculo_Limite_Calor; }
            set { _Calculo_Limite_Calor = value; }
        }


        public bool Inserir_eSocial
        {
            get { return _Inserir_eSocial; }
            set { _Inserir_eSocial = value; }
        }

        public bool Limite_eSocial
        {
            get { return _Limite_eSocial; }
            set { _Limite_eSocial = value; }
        }

        public bool PGR_Personalizar
        {
            get { return _PGR_Personalizar; }
            set { _PGR_Personalizar = value; }
        }

        public short Probabilidade
        {
            get { return _Probabilidade; }
            set { _Probabilidade = value; }
        }

        public short Severidade
        {
            get { return _Severidade; }
            set { _Severidade = value; }
        }

        public short Nivel_Risco
        {
            get { return _Nivel_Risco; }
            set { _Nivel_Risco = value; }
        }

        public decimal Probabilidade_Aut
        {
            get { return _Probabilidade_Aut; }
            set { _Probabilidade_Aut = value; }
        }

        public decimal Severidade_Aut
        {
            get { return _Severidade_Aut; }
            set { _Severidade_Aut = value; }
        }

        public short Nivel_Risco_Aut
        {
            get { return _Nivel_Risco_Aut; }
            set { _Nivel_Risco_Aut = value; }
        }

        public short nId_Tempo_Exposicao
        {
            get { return _nId_Tempo_Exposicao; }
            set { _nId_Tempo_Exposicao = value; }
        }

        public string Acao
        {
            get { return _Acao; }
            set { _Acao = value; }
        }

        public string Metodologia
        {
            get { return _Metodologia; }
            set { _Metodologia = value; }
        }

        public string Forma_Registro
        {
            get { return _Forma_Registro; }
            set { _Forma_Registro = value; }
        }

        public string Prazo
        {
            get { return _Prazo; }
            set { _Prazo = value; }
        }

        public int nId_Risco
        {
            get { return _nId_Risco; }
            set { _nId_Risco = value; }
        }

        public string Periodo
        {
            get { return _Periodo; }
            set { _Periodo = value; }
        }

        public string Afericao
        {
            get { return _Afericao; }
            set { _Afericao = value; }
        }

        public short Prioridade
        {
            get { return _Prioridade; }
            set { _Prioridade = value; }
        }

        public bool Cron_Jan
        {
            get { return _Cron_Jan; }
            set { _Cron_Jan = value; }
        }

        public bool Cron_Fev
        {
            get { return _Cron_Fev; }
            set { _Cron_Fev = value; }
        }

        public bool Cron_Mar
        {
            get { return _Cron_Mar; }
            set { _Cron_Mar = value; }
        }

        public bool Cron_Abr
        {
            get { return _Cron_Abr; }
            set { _Cron_Abr = value; }
        }

        public bool Cron_Mai
        {
            get { return _Cron_Mai; }
            set { _Cron_Mai = value; }
        }

        public bool Cron_Jun
        {
            get { return _Cron_Jun; }
            set { _Cron_Jun = value; }
        }

        public bool Cron_Jul
        {
            get { return _Cron_Jul; }
            set { _Cron_Jul = value; }
        }

        public bool Cron_Ago
        {
            get { return _Cron_Ago; }
            set { _Cron_Ago = value; }
        }

        public bool Cron_Set
        {
            get { return _Cron_Set; }
            set { _Cron_Set = value; }
        }

        public bool Cron_Out
        {
            get { return _Cron_Out; }
            set { _Cron_Out = value; }
        }

        public bool Cron_Nov
        {
            get { return _Cron_Nov; }
            set { _Cron_Nov = value; }
        }

        public bool Cron_Dez
        {
            get { return _Cron_Dez; }
            set { _Cron_Dez = value; }
        }


        public bool Inserir_ASO
        {
            get { return _Inserir_ASO; }
            set { _Inserir_ASO = value; }
        }

        public decimal tVL_MED_Ruido_LTCAT
        {
            get { return _tVL_MED_Ruido_LTCAT; }
            set { _tVL_MED_Ruido_LTCAT = value; }
        }


        #endregion

        #region Metodos

        #region Override Table

        public override void Validate()
        {
            //if (this.nID_RSC.mirrorOld == null)
            //    this.nID_RSC.Find();

            //if (!this.nID_RSC.Qualitativo)
            //{
            //    if (this.nID_RSC.IsAgenteQuimico() && this.nID_UN.Id == 0)
            //        throw new Exception("A unidade é campo obrigatório!");

            //    if (this.nID_RSC.IsAgenteQuimico() && this.sVL_LIM_TOL == 0.0M)
            //        throw new Exception("O limite é campo obrigatório!");

            //}
            base.Validate();
        }
        #endregion

        #region IsUnidadeMultipa

        public bool IsUnidadeMultipla()
        {
            bool ret;
            switch (this.nID_RSC.Id)
            {
                case (int)Riscos.CondicoesHiperbaricas:
                case (int)Riscos.RadiacoesNaoIonizantes:
                case (int)Riscos.Frio:
                case (int)Riscos.Umidade:
                case (int)Riscos.Vibracoes:
                case (int)Riscos.AgentesQuimicosAnexo13:
                case (int)Riscos.AgentesBiologicos:
                    ret = false;
                    break;

                case (int)Riscos.RadiacoesIonizantes:
                case (int)Riscos.AgentesQuimicos:
                case (int)Riscos.AgentesQuimicosB:
                case (int)Riscos.AgentesQuimicosC:
                case (int)Riscos.AgentesQuimicosD:
                case (int)Riscos.ACGIH:
                case (int)Riscos.AsbestosPoeirasMinerais:
                    ret = true;
                    break;

                default:
                    ret = false;
                    break;
            }
            return ret;
        }
        #endregion

        #region AtualizaLimiteTolerancia

        public void AtualizaLimiteTolerancia()
        {
            switch (this.nID_RSC.Id)
            {
                case (int)Riscos.RuidoContinuo:
                case (int)Riscos.RuidoImpacto:
                    AtualizaLimireRuido();
                    break;

                case (int)Riscos.Calor:                   
                    AtualizaLimiteCalor();
                    break;

                case (int)Riscos.RadiacoesIonizantes:
                    AtualizaLimiteRadiacoesIonizantes();
                    break;

                case (int)Riscos.CondicoesHiperbaricas:
                case (int)Riscos.RadiacoesNaoIonizantes:
                case (int)Riscos.Frio:
                case (int)Riscos.Umidade:
                case (int)Riscos.Vibracoes:
                    AtualizaLimiteVibracoes();
                    break;

                case (int)Riscos.VibracoesVCI:
                    AtualizaLimiteVibracoesVCI();
                    break;



                case (int)Riscos.AgentesQuimicosAnexo13:
                case (int)Riscos.AgentesBiologicos:
                    this.gINSALUBRE = true;
                    break;

                case (int)Riscos.AgentesQuimicos:
                case (int)Riscos.AgentesQuimicosB:
                case (int)Riscos.AgentesQuimicosC:
                case (int)Riscos.AgentesQuimicosD:
                case (int)Riscos.ACGIH:
                case (int)Riscos.AsbestosPoeirasMinerais:
                    if (this.IsSilica())
                        AtualizaLimiteSilica();
                    else
                        AtualizaLimiteAgentesQuimicos();
                    break;
            }
        }

        #region AtualizaLimiteRadiacoesIonizantes

        private void AtualizaLimiteRadiacoesIonizantes()
        {
            if (this.tVL_MED != 0 && sVL_LIM_TOL != 0)
                this.gINSALUBRE = this.tVL_MED > this.sVL_LIM_TOL;
            else
                this.gINSALUBRE = true;
        }
        #endregion

        #region AtualizaLimireRuido

        private void AtualizaLimireRuido()
        {
            this.gINSALUBRE = (this.tVL_MED > 85);

            this.sVL_LIM_TOL = 85;
            this.tVL_LIM_TOL = "85 dB(A)";
        }

        #endregion

        #region AtualizaLimiteCalor

        private void AtualizaLimiteCalor()
        {
            if (this.Calculo_Limite_Calor == true)
            {
                if (this.bDESC)
                    AtualizaLimiteCalorOutroLocal_Nova();
                else
                    AtualizaLimiteCalorProprioLocal_Nova();
            }
            else
            {
                /* PERIODO DE DESCANSO FORA DO LOCAL DE TRABALHO*/
                if (this.bDESC)
                    AtualizaLimiteCalorOutroLocal();
                else
                    AtualizaLimiteCalorProprioLocal();
            }
        }


        private void AtualizaLimiteCalorOutroLocal_Nova()
        {
            int[] MW = new int[91];


            decimal IBUTG = 33.7M;

            int xPosit = 0;


            xPosit = this.nID_ATV;


            MW[0] = 100; MW[1] = 102; MW[2] = 104; MW[3] = 106;
            MW[4] = 108; MW[5] = 110; MW[6] = 112; MW[7] = 115;
            MW[8] = 117; MW[9] = 119; MW[10] = 122; MW[11] = 124;
            MW[12] = 127; MW[13] = 129; MW[14] = 132; MW[15] = 135;
            MW[16] = 137; MW[17] = 140; MW[18] = 143; MW[19] = 146;
            MW[20] = 149; MW[21] = 152; MW[22] = 155; MW[23] = 158;
            MW[24] = 161; MW[25] = 165; MW[26] = 168; MW[27] = 171;
            MW[28] = 175; MW[29] = 178; MW[30] = 182; MW[31] = 186;
            MW[32] = 189; MW[33] = 193; MW[34] = 197; MW[35] = 201;
            MW[36] = 205; MW[37] = 209; MW[38] = 214; MW[39] = 218;
            MW[40] = 222; MW[41] = 227; MW[42] = 231; MW[43] = 236;

            MW[44] = 241; MW[45] = 246; MW[46] = 251; MW[47] = 256;
            MW[48] = 261; MW[49] = 266; MW[50] = 272; MW[51] = 277;
            MW[52] = 283; MW[53] = 289; MW[54] = 294; MW[55] = 300;
            MW[56] = 306; MW[57] = 313; MW[58] = 319; MW[59] = 325;
            MW[60] = 332; MW[61] = 339; MW[62] = 346; MW[63] = 353;
            MW[64] = 360; MW[65] = 367; MW[66] = 374; MW[67] = 382;
            MW[68] = 390; MW[69] = 398; MW[70] = 406; MW[71] = 414;
            MW[72] = 422; MW[73] = 431; MW[74] = 440; MW[75] = 448;
            MW[76] = 458; MW[77] = 467; MW[78] = 476; MW[79] = 486;
            MW[80] = 496; MW[81] = 506; MW[82] = 516; MW[83] = 526;
            MW[84] = 537; MW[85] = 548; MW[86] = 559; MW[87] = 570;
            MW[88] = 582; MW[89] = 594; MW[90] = 606;




            double xValor_Atividade = this.M;

            int xPosit_Atividade = 0;


            for (int zCont = 90; zCont > 0; zCont--)
            {
                if (xValor_Atividade >= MW[zCont])
                {
                    if (xValor_Atividade == MW[zCont])
                        xPosit_Atividade = zCont;
                    else
                        xPosit_Atividade = zCont + 1;

                    break;
                }
            }


            IBUTG = IBUTG - (0.1M * xPosit_Atividade);



            this.sVL_LIM_TOL = IBUTG;
            this.tVL_LIM_TOL = IBUTG.ToString() + "°";

            if (this.tVL_MED >= this.sVL_LIM_TOL)
                this.gINSALUBRE = true;
            else
                this.gINSALUBRE = false;


            return;


        }

        private void AtualizaLimiteCalorProprioLocal_Nova()
        {
            int[] MW = new int[91];

            int[] xAtividade = new int[52];

            decimal IBUTG = 33.7M;

            int xPosit = 0;


            xPosit = this.nID_ATV;


            MW[0] = 100; MW[1] = 102; MW[2] = 104; MW[3] = 106;
            MW[4] = 108; MW[5] = 110; MW[6] = 112; MW[7] = 115;
            MW[8] = 117; MW[9] = 119; MW[10] = 122; MW[11] = 124;
            MW[12] = 127; MW[13] = 129; MW[14] = 132; MW[15] = 135;
            MW[16] = 137; MW[17] = 140; MW[18] = 143; MW[19] = 146;
            MW[20] = 149; MW[21] = 152; MW[22] = 155; MW[23] = 158;
            MW[24] = 161; MW[25] = 165; MW[26] = 168; MW[27] = 171;
            MW[28] = 175; MW[29] = 178; MW[30] = 182; MW[31] = 186;
            MW[32] = 189; MW[33] = 193; MW[34] = 197; MW[35] = 201;
            MW[36] = 205; MW[37] = 209; MW[38] = 214; MW[39] = 218;
            MW[40] = 222; MW[41] = 227; MW[42] = 231; MW[43] = 236;

            MW[44] = 241; MW[45] = 246; MW[46] = 251; MW[47] = 256;
            MW[48] = 261; MW[49] = 266; MW[50] = 272; MW[51] = 277;
            MW[52] = 283; MW[53] = 289; MW[54] = 294; MW[55] = 300;
            MW[56] = 306; MW[57] = 313; MW[58] = 319; MW[59] = 325;
            MW[60] = 332; MW[61] = 339; MW[62] = 346; MW[63] = 353;
            MW[64] = 360; MW[65] = 367; MW[66] = 374; MW[67] = 382;
            MW[68] = 390; MW[69] = 398; MW[70] = 406; MW[71] = 414;
            MW[72] = 422; MW[73] = 431; MW[74] = 440; MW[75] = 448;
            MW[76] = 458; MW[77] = 467; MW[78] = 476; MW[79] = 486;
            MW[80] = 496; MW[81] = 506; MW[82] = 516; MW[83] = 526;
            MW[84] = 537; MW[85] = 548; MW[86] = 559; MW[87] = 570;
            MW[88] = 582; MW[89] = 594; MW[90] = 606;



            xAtividade[0] = 100; xAtividade[1] = 126; xAtividade[2] = 153; xAtividade[3] = 171;
            xAtividade[4] = 162; xAtividade[5] = 198; xAtividade[6] = 234; xAtividade[7] = 216;
            xAtividade[8] = 252; xAtividade[9] = 288; xAtividade[10] = 324; xAtividade[11] = 441;
            xAtividade[12] = 603; xAtividade[13] = 126; xAtividade[14] = 153; xAtividade[15] = 180;
            xAtividade[16] = 198; xAtividade[17] = 189; xAtividade[18] = 225; xAtividade[19] = 261;
            xAtividade[20] = 243; xAtividade[21] = 279; xAtividade[22] = 315; xAtividade[23] = 351;
            xAtividade[24] = 468; xAtividade[25] = 630; xAtividade[26] = 198; xAtividade[27] = 252;
            xAtividade[28] = 297; xAtividade[29] = 360; xAtividade[30] = 333; xAtividade[31] = 450;
            xAtividade[32] = 787; xAtividade[33] = 873; xAtividade[34] = 990; xAtividade[35] = 324;
            xAtividade[36] = 378; xAtividade[37] = 540; xAtividade[38] = 486; xAtividade[39] = 738;
            xAtividade[40] = 243; xAtividade[41] = 252; xAtividade[42] = 324; xAtividade[43] = 522;
            xAtividade[44] = 648; xAtividade[45] = 279; xAtividade[46] = 400; xAtividade[47] = 320;
            xAtividade[48] = 349; xAtividade[49] = 391; xAtividade[50] = 495; xAtividade[51] = 524;




            float xValor_Atividade = xAtividade[xPosit - 1];

            int xPosit_Atividade = 0;


            for (int zCont = 90; zCont > 0; zCont--)
            {
                if (xValor_Atividade >= MW[zCont])
                {
                    if (xValor_Atividade == MW[zCont])
                        xPosit_Atividade = zCont;
                    else
                        xPosit_Atividade = zCont + 1;

                    break;
                }
            }


            IBUTG = IBUTG - (0.1M * xPosit_Atividade);



            this.sVL_LIM_TOL = IBUTG;
            this.tVL_LIM_TOL = IBUTG.ToString() + "°";

            if (this.tVL_MED >= this.sVL_LIM_TOL)
                this.gINSALUBRE = true;
            else
                this.gINSALUBRE = false;


            return;


        }



        #region AtualizaLimiteCalorOutroLocal

        private void AtualizaLimiteCalorOutroLocal()
        {
            this.M = ((Mt * Tt) + (Md * Td)) / 60;

            this.IBUTG = ((IBUTGt * Tt) + (IBUTGd * Td)) / 60;

            if      (this.M >= 0    && this.M <= 175) this.sVL_LIM_TOL = 30.5M;
            else if (this.M >= 176  && this.M <= 200) this.sVL_LIM_TOL = 30.0M;
            else if (this.M >= 201  && this.M <= 250) this.sVL_LIM_TOL = 28.5M;
            else if (this.M >= 251  && this.M <= 300) this.sVL_LIM_TOL = 27.5M;
            else if (this.M >= 301  && this.M <= 350) this.sVL_LIM_TOL = 26.5M;
            else if (this.M >= 351  && this.M <= 400) this.sVL_LIM_TOL = 26.0M;
            else if (this.M >= 401  && this.M <= 450) this.sVL_LIM_TOL = 25.5M;
            else if (this.M >= 451  && this.M <= 500) this.sVL_LIM_TOL = 25.0M;
            else                                      this.sVL_LIM_TOL = 1;

            this.gINSALUBRE = (Convert.ToDouble(this.sVL_LIM_TOL) < this.IBUTG);

            this.tVL_MED = Convert.ToDecimal(this.IBUTG);
        }

        #endregion

        #region AtualizaLimiteCalorProprioLocal

        private void AtualizaLimiteCalorProprioLocal()
        {
            float jornadaLimite = -1F;

            if (this.nID_ATV == (int)AtividadeFisica.Leve)
            {
                if (this.tVL_MED <= 30.0M)
                {
                    this.tVL_LIM_TOL = "Trabalho Contínuo";
                    jornadaLimite = -1;
                }
                else if (this.tVL_MED >= 30.1M && this.tVL_MED <= 30.6M)
                {
                    this.tVL_LIM_TOL = "45 min. trab. e 15 min. desc.";
                    jornadaLimite = 6;
                }
                else if (this.tVL_MED >= 30.7M && this.tVL_MED <= 31.4M)
                {
                    this.tVL_LIM_TOL = "30 min. trab. e 30 min. desc.";
                    jornadaLimite = 4;
                }
                else if (this.tVL_MED >= 31.5M && this.tVL_MED <= 32.2M)
                {
                    this.tVL_LIM_TOL = "15 min. trab. e 45 min. desc.";
                    jornadaLimite = 2;
                }
                else if (this.tVL_MED > 32.2M)
                {
                    this.tVL_LIM_TOL = "Não é permitido o trabalho.";
                    jornadaLimite = 0;
                }
            }
            else if (this.nID_ATV == (int)AtividadeFisica.Moderada)
            {
                if (this.tVL_MED <= 26.7M)
                {
                    this.tVL_LIM_TOL = "Trabalho Contínuo";
                    jornadaLimite = -1;
                }
                else if (this.tVL_MED >= 26.8M && this.tVL_MED <= 28M)
                {
                    this.tVL_LIM_TOL = "45 min. trab. e 15 min. desc.";
                    jornadaLimite = 6;
                }
                else if (this.tVL_MED >= 28.1M && this.tVL_MED <= 29.4M)
                {
                    this.tVL_LIM_TOL = "30 min. trab. e 30 min. desc.";
                    jornadaLimite = 4;
                }
                else if (this.tVL_MED >= 29.5M && this.tVL_MED <= 31.1M)
                {
                    this.tVL_LIM_TOL = "15 min. trab. e 45 min. desc.";
                    jornadaLimite = 2;
                }
                else if (this.tVL_MED > 31.1M)
                {
                    this.tVL_LIM_TOL = "Não é permitido o trabalho.";
                    jornadaLimite = 0;
                }
            }
            else if (this.nID_ATV == (int)AtividadeFisica.Pesada)
            {
                if (this.tVL_MED <= 25.0M)
                {
                    this.tVL_LIM_TOL = "Trabalho Contínuo";
                    jornadaLimite = -1;
                }
                else if (this.tVL_MED >= 25.1M && this.tVL_MED <= 25.9M)
                {
                    this.tVL_LIM_TOL = "45 min. trab. e 15 min. desc.";
                    jornadaLimite = 6;
                }
                else if (this.tVL_MED >= 26.0M && this.tVL_MED <= 27.9M)
                {
                    this.tVL_LIM_TOL = "30 min. trab. e 30 min. desc.";
                    jornadaLimite = 4;
                }
                else if (this.tVL_MED >= 28.0M && this.tVL_MED <= 30.0M)
                {
                    this.tVL_LIM_TOL = "15 min. trab. e 45 min. desc.";
                    jornadaLimite = 2;
                }
                else if (this.tVL_MED > 30.0M)
                {
                    this.tVL_LIM_TOL = "Não é permitido o trabalho.";
                    jornadaLimite = 0;
                }
            }

            if (this.nID_FUNC.mirrorOld == null)
                this.nID_FUNC.Find();

            if (this.nID_FUNC.nID_TEMPO_EXP.mirrorOld == null)
                this.nID_FUNC.nID_TEMPO_EXP.Find();

            if (jornadaLimite == -1)
                this.gINSALUBRE = false;
            else if (jornadaLimite < this.nID_FUNC.nID_TEMPO_EXP.sVL_HOR_NUM)
                this.gINSALUBRE = true;
            else
                this.gINSALUBRE = false;
        }
        #endregion

        #endregion

        #region AtualizaLimiteAgentesQuimicos

        private void AtualizaLimiteAgentesQuimicos()
        {
            if (this.nID_AG_NCV.Id == 0)
                return;

            LimiteToleranciaAgenteQuimico limite = new LimiteToleranciaAgenteQuimico();

            limite = this.nID_AG_NCV.LimiteToleranciaNR15(this.nID_UN);

            if (this.nID_UN.Id == 0 && limite.Id != 0)
                this.nID_UN.Id = limite.IdUnidade.Id;

            if (limite.Id == 0)
                throw new Exception("Valor do Limite de Tolerância não cadastrado para a unidade " + this.nID_UN.ToString() + ".");

            this.sVL_LIM_TOL = limite.ValorLimiteTolTWA;
            this.tVL_LIM_TOL = limite.ToString();

            //Verifica se está acima do limite de tolerârcia
            this.gINSALUBRE = (this.tVL_MED == 0) || (this.tVL_MED > limite.ValorLimiteTolTWA);
        }

        private void AtualizaLimiteSilica()
        {
            if (sVL_LIM_TOL == 0)
            {
                this.tVL_LIM_TOL = string.Empty;
                this.gINSALUBRE = false;
            }
            else
            {
                this.tVL_LIM_TOL = this.sVL_LIM_TOL + " " + this.nID_UN.ToString();
                this.gINSALUBRE = (this.tVL_MED == 0) || (this.tVL_MED > this.sVL_LIM_TOL);
            }
        }

        public bool IsSilica()//Limite variável
        {
            return this.nID_RSC.Id == (int)Riscos.AsbestosPoeirasMinerais
                        && (this.nID_AG_NCV.ToString().IndexOf("Sílica Livre Cristalizada") != -1
                        || this.nID_AG_NCV.Id == 7
                        || this.nID_AG_NCV.Id == 356);
        }
        #endregion

        #endregion





        private void AtualizaLimiteVibracoes()
        {

            this.nID_UN.Find();

            if (this.nID_UN.Descricao.ToUpper().IndexOf("AREN") >= 0)
            {
                this.tVL_LIM_TOL = "1.1 " + this.nID_UN.Descricao.Trim();
                if (System.Convert.ToSingle(this.tVL_MED) > 1.1)
                {
                    this.gINSALUBRE = true;
                }
                else
                    gINSALUBRE = false;
            }
            else if (this.nID_UN.Descricao.ToUpper().IndexOf("VDVR") >= 0)
            {
                this.tVL_LIM_TOL = "21 " + this.nID_UN.Descricao.Trim();
                if (System.Convert.ToSingle(this.tVL_MED) > 21)
                {
                    this.gINSALUBRE = true;
                }
                else
                    gINSALUBRE = false;
            }
            else
            {
                gINSALUBRE = false;
                this.tVL_LIM_TOL = "";
            }


        }


        private void AtualizaLimiteVibracoesVCI()
        {

            this.nID_UN.Find();

            if (this.nID_UN.Descricao.ToUpper().IndexOf("AREN") >= 0)
            {
                this.tVL_LIM_TOL = "1.1 " + this.nID_UN.Descricao.Trim();
                if (System.Convert.ToSingle(this.tVL_MED) > 1.1)
                {
                    this.gINSALUBRE = true;
                }
                else
                    gINSALUBRE = false;
            }
            else if (this.nID_UN.Descricao.ToUpper().IndexOf("VDVR") >= 0)
            {
                this.tVL_LIM_TOL = "21 " + this.nID_UN.Descricao.Trim();
                if (System.Convert.ToSingle(this.tVL_MED) > 21)
                {
                    this.gINSALUBRE = true;
                }
                else
                    gINSALUBRE = false;
            }
            else
            {
                gINSALUBRE = false;
                this.tVL_LIM_TOL = "";
            }


            this.nID_UN2.Find();

            if (this.nID_UN2.Descricao.ToUpper().IndexOf("AREN") >= 0)
            {
                this.tVL_LIM_TOL2 = "1.1 " + this.nID_UN2.Descricao.Trim();
                if (System.Convert.ToSingle(this.tVL_MED2) > 1.1)
                {
                    this.gINSALUBRE = true;
                }
                else
                    if (gINSALUBRE != true)
                    gINSALUBRE = false;
            }
            else if (this.nID_UN2.Descricao.ToUpper().IndexOf("VDVR") >= 0)
            {
                this.tVL_LIM_TOL2 = "21 " + this.nID_UN2.Descricao.Trim();
                if (System.Convert.ToSingle(this.tVL_MED2) > 21)
                {
                    this.gINSALUBRE = true;
                }
                else
                    if (gINSALUBRE != true)
                    gINSALUBRE = false;
            }
            else
            {
                gINSALUBRE = false;
                this.tVL_LIM_TOL2 = "";
            }



        }




        #region AtualizaNeutralizado

        public void AtualizaNeutralizado()
        {
            this.gNET = this.IsNeutralizado();
        }

        public bool IsNeutralizado()
        {
            if (!this.gINSALUBRE)
                return false;

            if (this.mDS_EPC_RCM != this.mDS_EPC_EXTE)
                return false;

            string where = "nID_PPRA=" + this.Id;

            int countEpiExte = new EpiExistente().ExecuteCount(where);
            int countEpiRec = new EpiRecomendado().ExecuteCount(where);

            if (countEpiExte == 0 || countEpiRec == 0)
                return false;

            if (countEpiExte != countEpiRec)
                return false;

            string query = "USE sied_novo SELECT COUNT(*)"
                        + " FROM tblEPI_EXTE INNER JOIN tblEPI_RCM ON"
                        + " tblEPI_EXTE.nID_PPRA = tblEPI_RCM.nID_PPRA"
                        + " AND tblEPI_EXTE.nID_RSC = tblEPI_RCM.nID_RSC"
                        + " AND tblEPI_EXTE.nID_EPI = tblEPI_RCM.nID_EPI"
                        + " WHERE (tblEPI_RCM.nID_PPRA = " + this.Id + ")";

            int countEpi = Convert.ToInt32(new EpiRecomendado().ExecuteScalar(query));

            return countEpi == countEpiRec;
        }

        #endregion

        #region NomeAgente e GrauInsalubridade

        public string GetGrauInsalubridade()
        {
            string strGrauInsalubridade = string.Empty;

            if (this.nID_RSC.mirrorOld == null)
                this.nID_RSC.Find();

            switch (this.nID_RSC.IndRiscoTipo)
            {
                case RiscoTipo.Quimico:
                    if (this.nID_AG_NCV.mirrorOld == null)
                        this.nID_AG_NCV.Find();

                    strGrauInsalubridade = this.nID_AG_NCV.GetNomeGrauInsalubridade();
                    break;
                default:
                    strGrauInsalubridade = this.nID_RSC.GetNomeGrauInsalubridade();
                    break;
            }

            return strGrauInsalubridade;
        }

        public string GetNomeAgente()
        {
            if (this.nID_RSC.Id != 0 && this.nID_RSC.mirrorOld == null)
                this.nID_RSC.Find();

            if (this.nID_AG_NCV.Id != 0 && this.nID_AG_NCV.mirrorOld == null)
                this.nID_AG_NCV.Find();

            string ret = string.Empty;

            if (this.nID_RSC.Id == 0)
                ret = "Ruído Contínuo ou Intermitente";
            else if (this.nID_RSC.Id == (int)Riscos.AsbestosPoeirasMinerais)
                ret = GetNomePoeirasMinerais();
            else if (this.nID_RSC.Id == (int)Riscos.AgentesQuimicos
                    || this.nID_RSC.Id == (int)Riscos.AgentesQuimicosB
                    || this.nID_RSC.Id == (int)Riscos.AgentesQuimicosC
                    || this.nID_RSC.Id == (int)Riscos.AgentesQuimicosD
                    || this.nID_RSC.Id == (int)Riscos.ACGIH)
                ret = GetNomeAgenteQuimico();
            else if (this.nID_RSC.Id == (int)Riscos.AgentesQuimicosAnexo13)
                ret = this.GetMateriaPrima();
            else if (this.nID_RSC.Id == (int)Riscos.AgentesBiologicos)
                ret = this.GetNomeAgenteBilologico();
            else if (this.nID_RSC.Id == (int)Riscos.Acidentes)
                ret = GetNomeAcidentes();
            else if (this.nID_AG_NCV.Id != 0)
            {
                this.nID_LAUD_TEC.Find();
                this.nID_LAUD_TEC.nID_EMPR.Find();
                if (this.nID_LAUD_TEC.nID_EMPR.NomeAbreviado.ToUpper().IndexOf("NCR") >= 0)
                    ret = GetNomeOutrosAgentesQuimicos_NCR();
                else
                    ret = GetNomeOutrosAgentesQuimicos();
            }

            else
            {
                if (Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper().IndexOf("PRAJNA") > 0)
                    ret = GetNomeOutros();
                else
                    ret = GetNomeOutros2();
            }

            return ret;
        }

        public string GetNomeAgenteResumido()
        {
            if (this.nID_RSC.Id != 0 && this.nID_RSC.mirrorOld == null)
                this.nID_RSC.Find();

            if (this.nID_AG_NCV.Id != 0 && this.nID_AG_NCV.mirrorOld == null)
                this.nID_AG_NCV.Find();

            string ret = string.Empty;

            if (this.nID_RSC.Id == 0)
                ret = "Ruído Contínuo ou Intermitente";
            else if (this.nID_RSC.Id == (int)Riscos.AgentesQuimicosAnexo13)
            {
                ret = this.GetMateriaPrima();

                if (ret == string.Empty)
                    ret = this.nID_AG_NCV.Nome;
            }
            else if (this.nID_AG_NCV.Id != 0)
                ret = this.nID_AG_NCV.Nome;
            else
                ret = this.nID_RSC.DescricaoResumido;

            return ret;
        }

        private string GetNomeAcidentes()
        {
            string ret = string.Empty;

            if (this.tDS_MAT_PRM != string.Empty)
                ret = this.tDS_MAT_PRM;
            else
                ret = this.nID_RSC.DescricaoResumido;

            return ret;
        }

        private string GetNomeOutros()
        {
            string ret = string.Empty;

            if (this.tDS_MAT_PRM != string.Empty)
                ret = this.nID_RSC.DescricaoResumido + " - exposição a " + this.tDS_MAT_PRM;
            else
                ret = this.nID_RSC.DescricaoResumido;

            return ret;
        }

        private string GetNomeOutros2()
        {
            string ret = string.Empty;

            if (this.tDS_MAT_PRM != string.Empty && this.tDS_MAT_PRM.ToUpper().IndexOf("ERG") < 0)
                ret = this.nID_RSC.DescricaoResumido + " - exposição a " + this.tDS_MAT_PRM;
            else
                ret = this.nID_RSC.DescricaoResumido;

            return ret;
        }


        private string GetNomeOutrosAgentesQuimicos()
        {
            string ret = string.Empty;

            if (this.tDS_FTE_GER != string.Empty && this.tDS_MAT_PRM == string.Empty)
                ret = this.nID_AG_NCV.Nome + " (" + this.tDS_FTE_GER + ")";
            else if (this.tDS_FTE_GER == string.Empty && this.tDS_MAT_PRM != string.Empty)
                ret = this.nID_AG_NCV.Nome + " (" + this.tDS_MAT_PRM + ")";
            else if (this.tDS_FTE_GER != string.Empty && this.tDS_MAT_PRM != string.Empty)
                ret = this.nID_AG_NCV.Nome + " (" + this.tDS_FTE_GER + " - " + this.tDS_MAT_PRM + ")";
            else
                ret = this.nID_AG_NCV.Nome;

            return ret;
        }

        private string GetNomeOutrosAgentesQuimicos_NCR()
        {
            string ret = string.Empty;

            if (this.tDS_FTE_GER != string.Empty && this.tDS_MAT_PRM == string.Empty)
                ret = this.nID_AG_NCV.Nome + " (" + this.tDS_FTE_GER + ")";
            else if (this.tDS_FTE_GER == string.Empty && this.tDS_MAT_PRM != string.Empty)
                ret = this.nID_AG_NCV.Nome + " (" + this.tDS_MAT_PRM + ")";
            else if (this.tDS_FTE_GER != string.Empty && this.tDS_MAT_PRM != string.Empty)
                ret = this.nID_AG_NCV.Nome + " (" + this.tDS_MAT_PRM + ")";
            else
                ret = this.nID_AG_NCV.Nome;

            return ret;
        }



        private string GetNomePoeirasMinerais()
        {
            string ret = string.Empty;

            if (this.tOBS_AG_NCV == string.Empty)
            {
                if (this.tDS_MAT_PRM != string.Empty)
                    ret = this.nID_AG_NCV.Nome + " - exposição a " + this.tDS_MAT_PRM;
                else
                    ret = this.nID_AG_NCV.Nome;
            }
            else
            {
                if (this.tDS_MAT_PRM != string.Empty)
                    ret = this.nID_AG_NCV.Nome + " (" + this.tOBS_AG_NCV + ")" + " - exposição a " + this.tDS_MAT_PRM;
                else
                    ret = this.nID_AG_NCV.Nome + " (" + this.tOBS_AG_NCV + ")";
            }
            return ret;
        }

        private string GetNomeAgenteQuimico()
        {
            if (this.mirrorOld == null)
                this.Find();

            if (this.nID_AG_NCV.mirrorOld == null)
                this.nID_AG_NCV.Find();

            StringBuilder ret = new StringBuilder();

            ret.Append(this.nID_AG_NCV.Nome);

            if (this.tOBS_AG_NCV != string.Empty)
            {
                if (this.tDS_MAT_PRM == string.Empty)
                {
                    if (ret.ToString().ToUpper().IndexOf(this.tOBS_AG_NCV.ToUpper()) == -1)
                        ret.Append(" (" + this.tOBS_AG_NCV + ")");
                }
                else
                {
                    bool FecharParenteses = false;

                    if (ret.ToString().ToUpper().IndexOf(this.tOBS_AG_NCV.ToUpper()) == -1)
                    {
                        ret.Append(" (" + this.tOBS_AG_NCV);
                        FecharParenteses = true;
                    }

                    if (ret.ToString().ToUpper().IndexOf(this.tDS_MAT_PRM.ToUpper()) == -1)
                        ret.Append(" (" + this.tDS_MAT_PRM + ")");

                    if (FecharParenteses)
                        ret.Append(")");
                }
            }
            else
            {
                if (ret.ToString().ToUpper().IndexOf(this.tDS_MAT_PRM.ToUpper()) == -1)
                    ret.Append(" (" + this.tDS_MAT_PRM + ")");
            }

            return ret.ToString();
        }
        #endregion

        #region DescricaoExposicao

        //public string GetDescricaoExposicao()
        //{
        //    if (this.mDS_FRS_RSC == string.Empty)
        //        return this.GetDescricaoExposicaoPadrao();
        //    else
        //        return this.mDS_FRS_RSC;
        //}

        //public string GetDescricaoExposicaoPadrao()
        //{
        //    string ret = string.Empty;

        //    if (this.nID_RSC.Id != 0 && this.nID_RSC.mirrorOld == null)
        //        this.nID_RSC.Find();

        //    if (this.nID_AG_NCV.Id != 0 && this.nID_AG_NCV.mirrorOld == null)
        //        this.nID_AG_NCV.Find();

        //    if (this.nID_RSC.Id == (int)Riscos.AgentesQuimicosAnexo13)
        //    {
        //        if (this.nID_MAT_PRM.Id != 0)
        //        {
        //            this.nID_MAT_PRM.Find();

        //            if (this.tDS_MAT_PRM == string.Empty)
        //                ret = this.nID_RSC.DescricaoResumido + " - " + this.nID_AG_NCV.Nome + " - exposição a " + this.nID_MAT_PRM.tNO_MAT_PRM + ".";
        //            else
        //                ret = this.nID_RSC.DescricaoResumido + " - " + this.nID_AG_NCV.Nome + " - " + this.nID_MAT_PRM.tDS_MAT_PRM + ".";
        //        }
        //        else if (this.nID_MAT_PRM.Id == 0 && this.tDS_MAT_PRM != string.Empty)
        //            ret = this.nID_AG_NCV.Nome + " - exposição a " + this.tDS_MAT_PRM;
        //        else
        //            ret = this.nID_AG_NCV.Nome;
        //    }
        //    else if (this.nID_RSC.Id == (int)Riscos.AgentesBiologicos)
        //    {
        //        this.nID_TP_ATV.Find();

        //        if (this.nID_TP_ATV.Id == 0 || this.nID_TP_ATV.tDS_TP_ATV == string.Empty)
        //            ret = this.nID_RSC.DescricaoResumido + " - " + this.tDS_FTE_GER;
        //        else
        //            ret = this.nID_RSC.DescricaoResumido + " - " + this.nID_TP_ATV.tDS_TP_ATV;
        //    }
        //    else
        //        ret = this.GetNomeAgente();

        //    return ret;
        //}

        #endregion

        #region AvaliacaoQuantitativa

        public bool IsSemAvaliacaoQuantitativa()
        {
            if (this.nID_RSC.Id != 0 && this.nID_RSC.mirrorOld == null)
                this.nID_RSC.Find();

            if (this.nID_RSC.Qualitativo)
                return false;

            return (this.tVL_MED == 0);
        }

        public string GetLimiteFoiUltrapassado()
        {
            string ret;


            this.nID_RSC.Find();

            if (!this.nID_RSC.Qualitativo && this.Risco_Insignificante == true)
            {
                ret = "Não Aplicável";
            }
            else if (this.IsSemAvaliacaoQuantitativa())
                ret = "*";
            else if (this.gINSALUBRE)
                ret = "SIM";
            else
            {
                if (this.tVL_MED > 0 && this.tVL_MED >= this.sVL_LIM_TOL)
                {
                    ret = "SIM";
                }
                else
                {
                    ret = "NÃO";
                }
            }


            return ret;
        }

        public string GetAvaliacaoQuantitativa()
        {
            string ret = string.Empty;

            if (this.nID_RSC.Id != 0 && this.nID_RSC.mirrorOld == null)
                this.nID_RSC.Find();

            //if (!this.nID_RSC.Qualitativo)
            //{
            if (this.nID_RSC.Id == (int)Riscos.Calor)
            {
                if (this.tVL_LIM_TOL != "")
                {
                    if (this.tVL_LIM_TOL.ToUpper().IndexOf("TRAB") > 0 && this.tVL_LIM_TOL.ToUpper().IndexOf("DESC") > 0)
                    {

                        string zIBUTGt = "";
                        string zIBUTGd = "";

                        if (this.IBUTGt != 0)
                            zIBUTGt = this.IBUTGt.ToString() + "ºC ";

                        if (this.IBUTGd != 0)
                            zIBUTGd = this.IBUTGd.ToString() + "ºC ";

                        if (this.bDESC == true)
                        {
                            string zret = "";

                            if (this.IBUTG != 0)
                                zret = "IBUTG " + this.IBUTG.ToString() + "ºC " + System.Environment.NewLine + "M=" + this.M.ToString("#,###") + "kcal/h" + System.Environment.NewLine + System.Environment.NewLine + "Regime de Trabalho:  ";
                            else
                                zret = "IBUTG " + this.tVL_MED.ToString() + "ºC " + System.Environment.NewLine + "M=" + this.M.ToString("#,###") + "kcal/h" + System.Environment.NewLine + System.Environment.NewLine + "Regime de Trabalho:  ";


                            if (this.tVL_LIM_TOL.ToUpper().IndexOf("TRABALHO") > 0)
                            {
                                ret = zret + this.tVL_LIM_TOL.Substring(0, this.tVL_LIM_TOL.ToUpper().IndexOf("TRABALHO") + 8) + " " + this.tVL_LIM_TOL.Substring(this.tVL_LIM_TOL.ToUpper().IndexOf("TRABALHO") + 8);
                            }
                            else if (this.tVL_LIM_TOL.ToUpper().IndexOf("TRAB.") > 0)
                            {
                                ret = zret + this.tVL_LIM_TOL.Substring(0, this.tVL_LIM_TOL.ToUpper().IndexOf("TRAB.") + 5) + " " + this.tVL_LIM_TOL.Substring(this.tVL_LIM_TOL.ToUpper().IndexOf("TRAB.") + 5);
                            }
                            else if (this.tVL_LIM_TOL.ToUpper().IndexOf("TRAB") > 0)
                            {
                                ret = zret + this.tVL_LIM_TOL.Substring(0, this.tVL_LIM_TOL.ToUpper().IndexOf("TRAB") + 4) + " " + this.tVL_LIM_TOL.Substring(this.tVL_LIM_TOL.ToUpper().IndexOf("TRAB") + 4);
                            }

                            if (this.tVL_LIM_TOL.ToUpper().IndexOf("DESCANSO") > 0)
                            {
                                ret = ret.Substring(0, ret.ToUpper().IndexOf("DESCANSO") + 8) + " " + ret.Substring(ret.ToUpper().IndexOf("DESCANSO") + 8);
                            }
                            else if (this.tVL_LIM_TOL.ToUpper().IndexOf("DESC.") > 0)
                            {
                                ret = ret.Substring(0, ret.ToUpper().IndexOf("DESC.") + 5) + " " + ret.Substring(ret.ToUpper().IndexOf("DESC.") + 5);
                            }
                            else if (this.tVL_LIM_TOL.ToUpper().IndexOf("DESC") > 0)
                            {
                                ret = ret.Substring(0, ret.ToUpper().IndexOf("DESC") + 4) + " " + ret.Substring(ret.ToUpper().IndexOf("DESC") + 4);
                            }

                            ret = ret + System.Environment.NewLine;

                            ret = ret + System.Environment.NewLine + "Trabalho " + zIBUTGt + System.Environment.NewLine + this.Mt.ToString("#,###") + "kcal/h";
                            ret = ret + System.Environment.NewLine + "Descanso " + zIBUTGd + System.Environment.NewLine + this.Md.ToString("#,###") + "kcal/h";
                        }
                        else
                        {

                            if (this.IBUTG != 0)
                                ret = "IBUTG " + this.IBUTG.ToString() + "ºC   ";
                            else
                                ret = "IBUTG " + this.tVL_MED.ToString() + "ºC  ";


                            if (this.nID_ATV == 1)
                                ret = ret + System.Environment.NewLine + "Atividade Leve";
                            else if (this.nID_ATV == 2)
                                ret = ret + System.Environment.NewLine + "Atividade Moderada";
                            else if (this.nID_ATV == 3)
                                ret = ret + System.Environment.NewLine + "Atividade Pesada";

                        }
                        //ret = this.IBUTGt.ToString() + " ºC min.trab.   " + this.IBUTGd.ToString() + " ºC min.desc.";                         

                        if (ret.Trim() != "")
                        {
                            return ret;
                        }
                    }

                }
            }



            if (!this.nID_RSC.Qualitativo)
            {

                if (this.Risco_Insignificante == true)
                {
                    ret = "***";
                }
                else if (this.tVL_MED != 0 || this.tPREFIXO_VL_MED != string.Empty)
                {
                    if (this.nID_UN.Id != 0)
                    {
                        if (this.nID_UN.mirrorOld == null)
                            this.nID_UN.Find();

                        if (this.nID_RSC.Id == (int)Riscos.VibracoesVCI)
                        {
                            this.nID_UN2.Find();

                            if (this.tVL_MED == 0 && this.tPREFIXO_VL_MED != string.Empty)
                                ret = this.tPREFIXO_VL_MED + System.Environment.NewLine + this.tPREFIXO_VL_MED2;
                            else
                                ret = this.tPREFIXO_VL_MED + " " + this.tVL_MED + " " + this.nID_UN.Descricao + System.Environment.NewLine + this.tPREFIXO_VL_MED2 + " " + this.tVL_MED2 + " " + this.nID_UN2.Descricao;

                        }
                        else
                        {
                            if (this.tVL_MED == 0 && this.tPREFIXO_VL_MED != string.Empty)
                                ret = this.tPREFIXO_VL_MED;
                            else
                                ret = this.tPREFIXO_VL_MED + " " + this.tVL_MED + " " + this.nID_UN.Descricao;
                        }
                    }
                    else
                    {
                        if (this.nID_RSC.Id == (int)Riscos.Calor)
                        {
                            ret = "IBUTG " + System.Decimal.Round(this.tVL_MED, 2) + " ºC";

                            if (this.Calculo_Limite_Calor == false)
                            {
                                if (this.nID_ATV == 1)
                                    ret = ret + System.Environment.NewLine + "Atividade Leve";
                                else if (this.nID_ATV == 2)
                                    ret = ret + System.Environment.NewLine + "Atividade Moderada";
                                else if (this.nID_ATV == 3)
                                    ret = ret + System.Environment.NewLine + "Atividade Pesada";
                            }


                        }
                        else if (this.nID_RSC.Id == (int)Riscos.RuidoContinuo || this.nID_RSC.Id == (int)Riscos.RuidoImpacto)
                            ret = this.tPREFIXO_VL_MED + " " + this.tVL_MED + " " + "dB(A)";
                        else
                            ret = this.tPREFIXO_VL_MED + " " + this.tVL_MED;
                    }
                }
                else
                    ret = "*";
            }
            else
                ret = "—";

            return ret.TrimStart(' ');
        }




        public string GetAvaliacaoQuantitativa_LTCAT()
        {
            string ret = string.Empty;

            if (this.nID_RSC.Id != 0 && this.nID_RSC.mirrorOld == null)
                this.nID_RSC.Find();

            //if (!this.nID_RSC.Qualitativo)
            //{
            if (this.nID_RSC.Id == (int)Riscos.Calor)
            {
                if (this.tVL_LIM_TOL != "")
                {
                    if (this.tVL_LIM_TOL.ToUpper().IndexOf("TRAB") > 0 && this.tVL_LIM_TOL.ToUpper().IndexOf("DESC") > 0)
                    {

                        string zIBUTGt = "";
                        string zIBUTGd = "";

                        if (this.IBUTGt != 0)
                            zIBUTGt = this.IBUTGt.ToString() + "ºC ";

                        if (this.IBUTGd != 0)
                            zIBUTGd = this.IBUTGd.ToString() + "ºC ";

                        if (this.bDESC == true)
                        {
                            string zret = "";

                            if (this.IBUTG != 0)
                                zret = "IBUTG " + this.IBUTG.ToString() + "ºC " + System.Environment.NewLine + "M=" + this.M.ToString("#,###") + "kcal/h" + System.Environment.NewLine + System.Environment.NewLine + "Regime de Trabalho:  ";
                            else
                                zret = "IBUTG " + this.tVL_MED.ToString() + "ºC " + System.Environment.NewLine + "M=" + this.M.ToString("#,###") + "kcal/h" + System.Environment.NewLine + System.Environment.NewLine + "Regime de Trabalho:  ";


                            if (this.tVL_LIM_TOL.ToUpper().IndexOf("TRABALHO") > 0)
                            {
                                ret = zret + this.tVL_LIM_TOL.Substring(0, this.tVL_LIM_TOL.ToUpper().IndexOf("TRABALHO") + 8) + " " + this.tVL_LIM_TOL.Substring(this.tVL_LIM_TOL.ToUpper().IndexOf("TRABALHO") + 8);
                            }
                            else if (this.tVL_LIM_TOL.ToUpper().IndexOf("TRAB.") > 0)
                            {
                                ret = zret + this.tVL_LIM_TOL.Substring(0, this.tVL_LIM_TOL.ToUpper().IndexOf("TRAB.") + 5) + " " + this.tVL_LIM_TOL.Substring(this.tVL_LIM_TOL.ToUpper().IndexOf("TRAB.") + 5);
                            }
                            else if (this.tVL_LIM_TOL.ToUpper().IndexOf("TRAB") > 0)
                            {
                                ret = zret + this.tVL_LIM_TOL.Substring(0, this.tVL_LIM_TOL.ToUpper().IndexOf("TRAB") + 4) + " " + this.tVL_LIM_TOL.Substring(this.tVL_LIM_TOL.ToUpper().IndexOf("TRAB") + 4);
                            }

                            if (this.tVL_LIM_TOL.ToUpper().IndexOf("DESCANSO") > 0)
                            {
                                ret = ret.Substring(0, ret.ToUpper().IndexOf("DESCANSO") + 8) + " " + ret.Substring(ret.ToUpper().IndexOf("DESCANSO") + 8);
                            }
                            else if (this.tVL_LIM_TOL.ToUpper().IndexOf("DESC.") > 0)
                            {
                                ret = ret.Substring(0, ret.ToUpper().IndexOf("DESC.") + 5) + " " + ret.Substring(ret.ToUpper().IndexOf("DESC.") + 5);
                            }
                            else if (this.tVL_LIM_TOL.ToUpper().IndexOf("DESC") > 0)
                            {
                                ret = ret.Substring(0, ret.ToUpper().IndexOf("DESC") + 4) + " " + ret.Substring(ret.ToUpper().IndexOf("DESC") + 4);
                            }

                            ret = ret + System.Environment.NewLine;

                            ret = ret + System.Environment.NewLine + "Trabalho " + zIBUTGt + System.Environment.NewLine + this.Mt.ToString("#,###") + "kcal/h";
                            ret = ret + System.Environment.NewLine + "Descanso " + zIBUTGd + System.Environment.NewLine + this.Md.ToString("#,###") + "kcal/h";
                        }
                        else
                        {

                            if (this.IBUTG != 0)
                                ret = "IBUTG " + this.IBUTG.ToString() + "ºC   ";
                            else
                                ret = "IBUTG " + this.tVL_MED.ToString() + "ºC  ";


                            if (this.nID_ATV == 1)
                                ret = ret + System.Environment.NewLine + "Atividade Leve";
                            else if (this.nID_ATV == 2)
                                ret = ret + System.Environment.NewLine + "Atividade Moderada";
                            else if (this.nID_ATV == 3)
                                ret = ret + System.Environment.NewLine + "Atividade Pesada";

                        }
                        //ret = this.IBUTGt.ToString() + " ºC min.trab.   " + this.IBUTGd.ToString() + " ºC min.desc.";                         

                        if (ret.Trim() != "")
                        {
                            return ret;
                        }
                    }

                }
            }



            if (!this.nID_RSC.Qualitativo)
            {

                if (this.Risco_Insignificante == true)
                {
                    ret = "***";
                }
                else if (this.tVL_MED != 0 || this.tPREFIXO_VL_MED != string.Empty)
                {
                    if (this.nID_UN.Id != 0)
                    {
                        if (this.nID_UN.mirrorOld == null)
                            this.nID_UN.Find();

                        if (this.nID_RSC.Id == (int)Riscos.VibracoesVCI)
                        {
                            this.nID_UN2.Find();

                            if (this.tVL_MED == 0 && this.tPREFIXO_VL_MED != string.Empty)
                                ret = this.tPREFIXO_VL_MED + System.Environment.NewLine + this.tPREFIXO_VL_MED2;
                            else
                                ret = this.tPREFIXO_VL_MED + " " + this.tVL_MED + " " + this.nID_UN.Descricao + System.Environment.NewLine + this.tPREFIXO_VL_MED2 + " " + this.tVL_MED2 + " " + this.nID_UN2.Descricao;

                        }
                        else
                        {
                            if (this.tVL_MED == 0 && this.tPREFIXO_VL_MED != string.Empty)
                                ret = this.tPREFIXO_VL_MED;
                            else
                                ret = this.tPREFIXO_VL_MED + " " + this.tVL_MED + " " + this.nID_UN.Descricao;
                        }
                    }
                    else
                    {
                        if (this.nID_RSC.Id == (int)Riscos.Calor)
                        {
                            ret = "IBUTG " + System.Decimal.Round(this.tVL_MED, 2) + " ºC";

                            if (this.Calculo_Limite_Calor == false)
                            {
                                if (this.nID_ATV == 1)
                                    ret = ret + System.Environment.NewLine + "Atividade Leve";
                                else if (this.nID_ATV == 2)
                                    ret = ret + System.Environment.NewLine + "Atividade Moderada";
                                else if (this.nID_ATV == 3)
                                    ret = ret + System.Environment.NewLine + "Atividade Pesada";
                            }


                        }
                        else if (this.nID_RSC.Id == (int)Riscos.RuidoContinuo || this.nID_RSC.Id == (int)Riscos.RuidoImpacto)
                            ret = this.tPREFIXO_VL_MED + " " + this.tVL_MED_Ruido_LTCAT + " " + "dB(A)";
                        else
                            ret = this.tPREFIXO_VL_MED + " " + this.tVL_MED;
                    }
                }
                else
                    ret = "*";
            }
            else
                ret = "—";

            return ret.TrimStart(' ');
        }



        public string GetStrValorMedido()
        {
            string ret = string.Empty;

            if (this.nID_RSC.Id != 0 && this.nID_RSC.mirrorOld == null)
                this.nID_RSC.Find();

            if (!this.nID_RSC.Qualitativo)
            {
                if (this.tVL_MED != 0)
                    ret = this.tVL_MED.ToString();
                else
                    ret = "*";
            }
            else
                ret = "—";

            return ret.TrimStart(' ');
        }

        public string GetStrUnidade()
        {
            string ret = string.Empty;

            if (this.nID_RSC.Id != 0 && this.nID_RSC.mirrorOld == null)
                this.nID_RSC.Find();

            if (!this.nID_RSC.Qualitativo)
            {
                if (this.nID_UN.Id != 0)
                {
                    if (this.nID_UN.mirrorOld == null)
                        this.nID_UN.Find();

                    ret = this.nID_UN.Descricao;
                }
                else
                {
                    if (this.nID_RSC.Id == (int)Riscos.Calor)
                        ret = "ºC";
                    else if (this.nID_RSC.Id == (int)Riscos.RuidoContinuo 
                            || this.nID_RSC.Id == (int)Riscos.RuidoImpacto)
                        ret = "dB(A)";
                }
            }

            return ret.TrimStart(' ');
        }

        public string GetAvaliacaoQuantitativaPPP()
        {
            string ret;

            if (this.nID_RSC.Qualitativo)
                ret = "NA"; // + "\n" + "(Análise Qualitativa)";
            else
                ret = this.GetAvaliacaoQuantitativa_LTCAT();

            return ret;
        }

        public string GetAvaliacaoQuantitativaMapaRisco()
        {
            string ret;

            if (this.nID_RSC.Qualitativo)
                ret = "Qualitativa";
            else
                ret = this.GetAvaliacaoQuantitativa();

            return ret;
        }

        #endregion

        #region LimiteTolerancia

        public string GetLimiteTolerancia()
        {
            string ret;

            if (!this.nID_RSC.Qualitativo)
            {
                if (this.nID_RSC.Id == (int)Riscos.RuidoContinuo
                    || this.nID_RSC.Id == (int)Riscos.RuidoImpacto)
                {
                    ret = this.sVL_LIM_TOL + " " + "dB(A)";
                }
                else if (this.nID_RSC.Id == (int)Riscos.Calor)
                {
                    if (!this.bDESC)
                    {

                        //if (this.tVL_LIM_TOL.ToUpper().Trim() == "TRABALHO CONTÍNUO")
                        //{
                        if (this.Calculo_Limite_Calor == false)
                        {
                            if (this.nID_ATV == (int)AtividadeFisica.Leve)
                                ret = "IBUTG: 30,0ºC"; // + " " + System.Environment.NewLine + this.tVL_LIM_TOL.ToString();
                            else if (this.nID_ATV == (int)AtividadeFisica.Moderada)
                                ret = "IBUTG: 26,7ºC"; // + " " + System.Environment.NewLine + this.tVL_LIM_TOL.ToString();
                            else if (this.nID_ATV == (int)AtividadeFisica.Pesada)
                                ret = "IBUTG: 25,0ºC"; // + " " + System.Environment.NewLine + this.tVL_LIM_TOL.ToString();
                            else
                                ret = "—";
                        }
                        else
                        {
                            ret = "IBUTG: " + this.sVL_LIM_TOL.ToString() + "ºC";
                        }                        //}
                        //else if (this.tVL_LIM_TOL.ToUpper().Trim() == "15 MIN. TRAB. E 45 MIN. DESC.")
                        //{

                        //    if (this.nID_ATV == (int)AtividadeFisica.Leve)
                        //        ret = "IBUTG: 31,5 à 32,2ºC"; //+ " " + System.Environment.NewLine +  this.tVL_LIM_TOL.ToString();
                        //    else if (this.nID_ATV == (int)AtividadeFisica.Moderada)
                        //        ret = "IBUTG: 29,5 à 31,1ºC"; //+ " " + System.Environment.NewLine + this.tVL_LIM_TOL.ToString();
                        //    else if (this.nID_ATV == (int)AtividadeFisica.Pesada)
                        //        ret = "IBUTG: 28,0 à 30,0ºC";/// + " " + System.Environment.NewLine + this.tVL_LIM_TOL.ToString();
                        //    else
                        //        ret = "—";
                        //}
                        //else if (this.tVL_LIM_TOL.ToUpper().Trim() == "30 MIN. TRAB. E 30 MIN. DESC.")
                        //{
                        //    if (this.nID_ATV == (int)AtividadeFisica.Leve)
                        //        ret = "IBUTG: 30,7 à 31,4ºC"; // + " " + System.Environment.NewLine + this.tVL_LIM_TOL.ToString();
                        //    else if (this.nID_ATV == (int)AtividadeFisica.Moderada)
                        //        ret = "IBUTG: 28,1 à 29,4ºC"; // + " " + System.Environment.NewLine + this.tVL_LIM_TOL.ToString();
                        //    else if (this.nID_ATV == (int)AtividadeFisica.Pesada)
                        //        ret = "IBUTG: 26,0 à 27,9ºC"; // + " " + System.Environment.NewLine + this.tVL_LIM_TOL.ToString();
                        //    else
                        //        ret = "—";
                        //}
                        //else if (this.tVL_LIM_TOL.ToUpper().Trim() == "45 MIN. TRAB. E 15 MIN. DESC.")
                        //{
                        //    if (this.nID_ATV == (int)AtividadeFisica.Leve)
                        //        ret = "IBUTG: 30,1 à 30,6ºC"; // + " " + System.Environment.NewLine + this.tVL_LIM_TOL.ToString();
                        //    else if (this.nID_ATV == (int)AtividadeFisica.Moderada)
                        //        ret = "IBUTG: 26,8 à 28,0ºC"; // + " " + System.Environment.NewLine + this.tVL_LIM_TOL.ToString();
                        //    else if (this.nID_ATV == (int)AtividadeFisica.Pesada)
                        //        ret = "IBUTG: 25,1 à 25,9ºC"; // + " " + System.Environment.NewLine + this.tVL_LIM_TOL.ToString();
                        //    else
                        //        ret = "—";
                        //}
                        //else
                        //{
                        //    if (this.nID_ATV == (int)AtividadeFisica.Leve)
                        //        ret = "IBUTG: acima de 32,2ºC";
                        //    else if (this.nID_ATV == (int)AtividadeFisica.Moderada)
                        //        ret = "IBUTG: acima de 31,1ºC";
                        //    else if (this.nID_ATV == (int)AtividadeFisica.Pesada)
                        //        ret = "IBUTG: acima de 30,0ºC";
                        //    else
                        //        ret = "—";
                        //}

                    }
                    else
                        ret = "IBUTG " + this.sVL_LIM_TOL.ToString() + "ºC";
                }
                else if (this.nID_AG_NCV.Id == 0)
                {
                    if (this.nID_RSC.Id == 106)
                    {
                        if (this.tVL_LIM_TOL == "0")
                            ret = "";
                        else
                            ret = tVL_LIM_TOL + System.Environment.NewLine + tVL_LIM_TOL2;
                    }
                    else
                    {
                        if (this.tVL_LIM_TOL == "0")
                            ret = "";
                        else
                            ret = tVL_LIM_TOL;
                    }
                }
                else if (this.nID_RSC.Id == 106)
                {
                    this.nID_UN2.Find();
                    if (sVL_LIM_TOL == 0)
                        ret = "*";
                    else
                        ret = sVL_LIM_TOL + " " + this.nID_UN.ToString() + System.Environment.NewLine + sVL_LIM_TOL2 + " " + this.nID_UN2.ToString();
                }
                else
                {
                    if (sVL_LIM_TOL == 0)
                        ret = "*";
                    else
                        ret = sVL_LIM_TOL + " " + this.nID_UN.ToString();
                }
            }
            else
                ret = "—";

            return ret;
        }

        public string GetLimiteToleranciaSemUnidade()
        {
            string limite = this.GetLimiteTolerancia();

            string[] valor = limite.Split(' ');

            if (valor.Length > 0)
                return valor[0];
            else
                return string.Empty;
        }

        #endregion

        #region PPP

        public string GetTipoAgentePPP()
        {
            string ret;

            this.nID_RSC.Find();

            if (this.nID_RSC.Id == (int)Riscos.RuidoContinuo)
                ret = "F";
            else if (this.nID_RSC.Id == (int)Riscos.AgentesQuimicos
                     || this.nID_RSC.Id == (int)Riscos.AgentesQuimicosB
                     || this.nID_RSC.Id == (int)Riscos.AgentesQuimicosC
                     || this.nID_RSC.Id == (int)Riscos.AgentesQuimicosD
                     || this.nID_RSC.Id == (int)Riscos.AsbestosPoeirasMinerais
                     || this.nID_RSC.Id == (int)Riscos.AgentesQuimicosAnexo13
                     || this.nID_RSC.Id == (int)Riscos.ACGIH)
                ret = "Q";
            else if (this.nID_RSC.Id == (int)Riscos.AgentesBiologicos)
                ret = "B";
            else
            {
                //if (this.nID_RSC.mirrorOld == null)
                //this.Find();
                this.nID_RSC.Find();

                if (this.nID_RSC.IndRiscoTipo.Equals(RiscoTipo.Fisico))
                    ret = "F";
                else if (this.nID_RSC.IndRiscoTipo.Equals(RiscoTipo.Quimico))
                    ret = "Q";
                else if (this.nID_RSC.IndRiscoTipo.Equals(RiscoTipo.Biologico))
                    ret = "B";
                else
                    ret = "—";
            }

            return ret;
        }

        public string GetTecnicaUtilizadaPPP()
        {
            string ret;

            if (this.nID_RSC.Id != 0 && this.nID_RSC.mirrorOld == null)
                this.nID_RSC.Find();

            if (this.nID_RSC.Id != 0 && this.nID_RSC.Qualitativo)
                ret = "Análise Qualitativa"; //"NA";
            else if (this.nID_RSC.Id == (int)Riscos.RuidoContinuo
                || this.nID_RSC.Id == (int)Riscos.RuidoImpacto)
            {
                ret = this.nID_EQP_MED.ToString(); //ret = "Dosimetria de ruído\n" + this.nID_EQP_MED.ToString();

            }
            else if (this.nID_RSC.Id == (int)Riscos.Calor)
            {
                ret = "Índice de Bulbo Úmido Termômetro de Globo\n" + this.nID_EQP_MED.ToString();

            }
            else
            {
                ret = this.nID_EQP_MED.ToString();
                
            }

            return ret;
        }


        public string GetTecnicaUtilizadaPPP_Metodologia()
        {
            string ret;

            if (this.nID_RSC.Id != 0 && this.nID_RSC.mirrorOld == null)
                this.nID_RSC.Find();

            if (this.nID_EQP_MED.Descricao.ToString().Trim()== "")
            {
                ret = "";
                return ret;
            }


            if (this.nID_RSC.Id != 0 && this.nID_RSC.Qualitativo)
                ret = ""; //"NA";
            else if (this.nID_RSC.Id == (int)Riscos.RuidoContinuo
                || this.nID_RSC.Id == (int)Riscos.RuidoImpacto)
            {                
                ret = this.nID_EQP_MED.ToString().Trim() + ":" + this.nID_EQP_MED.Descricao.ToString().Trim() + System.Environment.NewLine; //ret = "Dosimetria de ruído\n" + this.nID_EQP_MED.ToString();
            }
            else if (this.nID_RSC.Id == (int)Riscos.Calor)
            {
                ret = "Índice de Bulbo Úmido Termômetro de Globo\n" + this.nID_EQP_MED.ToString().Trim() + ":" + this.nID_EQP_MED.Descricao.ToString().Trim() + System.Environment.NewLine; 

            }
            else
            {
                ret = this.nID_EQP_MED.ToString() + ":" + this.nID_EQP_MED.Descricao.ToString().Trim() + System.Environment.NewLine;

            }

            if (ret.ToUpper().IndexOf("QUALITATIVO") > 0) ret = "";

            return ret;
        }


        public string GetEpiEficazPPP()
        {
            string ret;

            ArrayList listEpiRCM = new EpiRecomendado().Find("nID_PPRA=" + this.Id);

            if (listEpiRCM.Count == 0)
                ret = "NA";
            else
                ret = this.EPIEficaz() ? "S" : "N";

            return ret;
        }

        public string GetEpcEficazPPP()
        {
            string ret;

            if (this.mDS_EPC_EXTE == string.Empty && this.mDS_EPC_RCM == string.Empty)
                ret = "NA";
            else
                ret = this.EPCEficaz() ? "S" : "N";

            return ret;
        }

        //public string GetCA_PPP(Cliente cliente, int xIdEmpregado, DateTime DataExposicao)
        //{

        //    string ret="";
        //    string xAux = "";

        //    ArrayList listEPI_EXTE = new EpiExistente().Find("nID_PPRA=" + this.Id);
        //    ArrayList listEPI_RCM = new EpiRecomendado().Find("nID_PPRA=" + this.Id);

        //    StringBuilder strCAs = new StringBuilder();

        //    foreach (EpiExistente epiExistente in listEPI_EXTE)
        //    {

        //        string whereCA = "IdEPI=" + epiExistente.nID_EPI.Id + " AND IdCliente=" + cliente.Id;

        //        ArrayList listEPIClienteCA = new EPIClienteCA().Find(whereCA);

        //        foreach (EPIClienteCA epiClienteCA in listEPIClienteCA)
        //        {
        //            //colocar checagem de data de validade ?
        //            epiClienteCA.IdCA.Find();

        //            bool xInserir = true;

        //            PPRA_EPI xEPI2 = new Ilitera.Data.PPRA_EPI();
        //            DataSet zDs = xEPI2.DataEntrega_EPICA(cliente.Id, epiExistente.nID_EPI.Id, xIdEmpregado);


        //            if ( zDs.Tables[0].Rows.Count > 0)
        //            {

        //                DateTime zDataRecebimento = new DateTime();

        //                if (zDs.Tables[0].Rows[0][0].ToString() != "")
        //                {
        //                    zDataRecebimento = System.Convert.ToDateTime(zDs.Tables[0].Rows[0][0]);

        //                    if (zDataRecebimento > DataExposicao)
        //                    {
        //                        xInserir = false;
        //                    }
        //                }

        //            }


        //            //EPICAEntregaDetalhe xEntregaDetalhe = new EPICAEntregaDetalhe();
        //            //xEntregaDetalhe.Find(" IdEpiClienteCA = " + epiClienteCA.Id.ToString());

        //            //if (xEntregaDetalhe.Id != 0)
        //            //{

        //            //    EPICAEntrega xEntrega = new EPICAEntrega();
        //            //    xEntrega.Find(" IdEPICAEntrega = " + xEntregaDetalhe.Id.ToString());

        //            //    if (xEntrega.Id != 0)
        //            //    {
        //            //        if (xEntrega.DataRecebimento > DataExposicao)
        //            //        {
        //            //            xInserir = false;
        //            //        }
        //            //    }

        //            //}


        //            if (xInserir == true)
        //            {
        //                if (strCAs.ToString().IndexOf(epiClienteCA.IdCA.NumeroCA.ToString("00000")) < 0)   //para não entrar repetido
        //                {
        //                    strCAs.Append(epiClienteCA.IdCA.NumeroCA.ToString("00000") + "\n");
        //                }
        //            }                   
        //        }


        //        //buscar CA para nIdEmpregado, IdPCMSO e IdEPI em " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEPI_CA - Wagner
        //        Ilitera.Data.PPRA_EPI xEPI = new Ilitera.Data.PPRA_EPI();
        //        xAux = xEPI.Trazer_CAs(xIdEmpregado, this.nID_LAUD_TEC.Id, epiExistente.nID_EPI.Id);

        //        if (strCAs.ToString().IndexOf(xAux) < 0)   //para não entrar repetido
        //        {
        //            strCAs.Append(xAux + "\n");
        //        }


        //    }

        //    if (strCAs.ToString().Length > 0)
        //        ret = strCAs.ToString();
        //    else
        //    {
        //        /*if(ppra.Acima50PorCentoDose())*/
        //        if (listEPI_RCM.Count == 0)
        //            ret = "NA";
        //        else
        //            ret = "";
        //    }

        //    if (ret.Trim() == "") ret = "NA";

        //    return ret;
        //}

        public string GetCA_PPP(Cliente cliente, int xIdEmpregado, DateTime DataPPRA, DateTime Periodo1, DateTime Periodo2)
        {

            string ret = "";
            string xAux = "";

            ArrayList listEPI_EXTE = new EpiExistente().Find("nID_PPRA=" + this.Id);
            ArrayList listEPI_RCM = new EpiRecomendado().Find("nID_PPRA=" + this.Id);

            StringBuilder strCAs = new StringBuilder();

            foreach (EpiExistente epiExistente in listEPI_EXTE)
            {

                string whereCA = "IdEPI=" + epiExistente.nID_EPI.Id + " AND IdCliente=" + cliente.Id;

                ArrayList listEPIClienteCA = new EPIClienteCA().Find(whereCA);

                foreach (EPIClienteCA epiClienteCA in listEPIClienteCA)
                {

                    epiClienteCA.IdCA.Find();

                    bool xInserir = true;

                    cliente.Find();

                    if (cliente.PPP_Checar_Entrega_EPI == true)
                    {
                        //checar se tem entrega deste EPI
                        PPRA_EPI xEPI2 = new Ilitera.Data.PPRA_EPI();
                        DataSet zDs = xEPI2.DataEntrega_EPICA(cliente.Id, epiExistente.nID_EPI.Id, xIdEmpregado, epiClienteCA.IdCA.Id);

                        //devo trabalhar checando PEriodo1 e Perido2,  que é a exposição, junto com DataPPRA

                        //epiClienteCA.IdCA.Validade

                        if (zDs.Tables[0].Rows.Count > 0)
                        {

                            DateTime zDataRecebimento = new DateTime();



                            if (zDs.Tables[0].Rows[0][0].ToString() != "")
                            {
                                zDataRecebimento = System.Convert.ToDateTime(zDs.Tables[0].Rows[0][0]);

                                //if ( ( zDataRecebimento >= Periodo1 && zDataRecebimento <= Periodo2))
                                if ((zDataRecebimento >= Periodo1 && zDataRecebimento <= Periodo2) || (zDataRecebimento < Periodo1 && epiClienteCA.IdCA.Validade >= Periodo1))
                                {
                                    if (epiClienteCA.IdCA.Validade >= Periodo1)
                                        xInserir = true;
                                    else
                                        xInserir = false;
                                }
                                else if (zDataRecebimento > Periodo2)
                                {
                                    xInserir = false;
                                }
                                else if (zDataRecebimento < Periodo1)
                                {
                                    xInserir = false;
                                }
                            }
                            else
                            {
                                xInserir = false;
                            }

                        }
                        else
                        {
                            xInserir = false;
                        }
                    }
                    else
                    {

                        if (epiClienteCA.IdCA.Validade >= Periodo1)
                            xInserir = true;
                        else
                            xInserir = false;

                    }

                    //EPICAEntregaDetalhe xEntregaDetalhe = new EPICAEntregaDetalhe();
                    //xEntregaDetalhe.Find(" IdEpiClienteCA = " + epiClienteCA.Id.ToString());

                    //if (xEntregaDetalhe.Id != 0)
                    //{

                    //    EPICAEntrega xEntrega = new EPICAEntrega();
                    //    xEntrega.Find(" IdEPICAEntrega = " + xEntregaDetalhe.Id.ToString());

                    //    if (xEntrega.Id != 0)
                    //    {
                    //        if (xEntrega.DataRecebimento > DataExposicao)
                    //        {
                    //            xInserir = false;
                    //        }
                    //    }

                    //}


                    if (xInserir == true)
                    {
                        if (strCAs.ToString().IndexOf(epiClienteCA.IdCA.NumeroCA.ToString("00000")) < 0)   //para não entrar repetido
                        {
                            strCAs.Append(epiClienteCA.IdCA.NumeroCA.ToString("00000") + "\n");
                        }
                    }
                }


                //buscar CA para nIdEmpregado, IdPCMSO e IdEPI em " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEPI_CA - Wagner
                Ilitera.Data.PPRA_EPI xEPI = new Ilitera.Data.PPRA_EPI();
                xAux = xEPI.Trazer_CAs(xIdEmpregado, this.nID_LAUD_TEC.Id, epiExistente.nID_EPI.Id);

                if (strCAs.ToString().IndexOf(xAux) < 0)   //para não entrar repetido
                {
                    strCAs.Append(xAux + "\n");
                }


            }

            if (strCAs.ToString().Length > 0)
                ret = strCAs.ToString();
            else
            {
                /*if(ppra.Acima50PorCentoDose())*/
                if (listEPI_RCM.Count == 0)
                    ret = "NA";
                else
                    ret = "";
            }

            if (ret.Trim() == "") ret = "NA";

            return ret;
        }





        public string GetCA_PPP_Acidentes(Cliente cliente, int xIdEmpregado)
        {
            string ret;
            string xAux;


            ArrayList listEPI_EXTE = new GheEpiExistente().Find("nID_Func=" + this.nID_FUNC.Id);
            ArrayList listEPI_RCM = new GheEpiRecomendado().Find("nID_Func=" + this.nID_FUNC.Id);

            StringBuilder strCAs = new StringBuilder();

            foreach (GheEpiExistente epiExistente in listEPI_EXTE)
            {

                string whereCA = "IdEPI=" + epiExistente.nID_EPI.Id + " AND IdCliente=" + cliente.Id;

                ArrayList listEPIClienteCA = new EPIClienteCA().Find(whereCA);

                foreach (EPIClienteCA epiClienteCA in listEPIClienteCA)
                {
                    epiClienteCA.IdCA.Find();
                    if (strCAs.ToString().IndexOf(epiClienteCA.IdCA.NumeroCA.ToString("00000")) < 0)   //para não entrar repetido
                    {
                        strCAs.Append(epiClienteCA.IdCA.NumeroCA.ToString("00000") + "\n");
                    }                    
                }

                //buscar CA para nIdEmpregado, IdPCMSO e IdEPI em sied_novo.dbo.tblEPI_CA - Wagner
                Ilitera.Data.PPRA_EPI xEPI = new Ilitera.Data.PPRA_EPI();
                xAux = xEPI.Trazer_CAs(xIdEmpregado, this.nID_LAUD_TEC.Id, epiExistente.nID_EPI.Id);

                if (strCAs.ToString().IndexOf(xAux) < 0)   //para não entrar repetido
                {
                    if (strCAs.ToString().IndexOf("/") >= 0)
                    {
                        strCAs.Append(xAux + " / ");
                    }
                    else
                    {
                        strCAs.Append(" / " + xAux + " / ");
                    }
                }

            }

            if (strCAs.ToString().Length > 0)
                ret = strCAs.ToString();
            else
            {
                /*if(ppra.Acima50PorCentoDose())*/
                if (listEPI_RCM.Count == 0)
                    ret = "NA";
                else
                    ret = "";
            }


            if (ret.Trim() == "") ret = "NA";


            return ret;
        }

        #endregion

        #region AcimaLimiteTolerancia / Acima50PorCentoDose

        public bool AcimaLimiteTolerancia()
        {
            return this.gINSALUBRE;
        }

        public bool Inserir_no_ASO()
        {
            return this.Inserir_ASO;
        }


        public bool Acima50PorCentoDose()
        {
            bool bVal = false;

            switch (this.nID_RSC.Id)
            {
                case (int)Riscos.RuidoContinuo:
                    bVal = (this.tVL_MED >= 80.0M);
                    break;

                case (int)Riscos.AgentesQuimicos:
                case (int)Riscos.AgentesQuimicosB:
                case (int)Riscos.AgentesQuimicosC:
                case (int)Riscos.AgentesQuimicosD:
                case (int)Riscos.ACGIH:
                case (int)Riscos.AsbestosPoeirasMinerais:

                    this._nID_AG_NCV.Find();
                    if (this._nID_AG_NCV.Nome.ToUpper() == "AUSÊNCIA DE FATOR DE RISCO")
                        bVal = false;
                    else if (this.tVL_MED == 0.0M)
                        bVal = true; //this.gINSALUBRE;
                    else if (this.tVL_MED != 0.0M && this.sVL_LIM_TOL != 0.0M)
                        bVal = (this.tVL_MED > (this.sVL_LIM_TOL / 2.0M));

                    break;

                default:
                    bVal = this.gINSALUBRE;
                    break;
            }
            return bVal;
        }

        public bool IsSituacaoRiscoGraveIminente()
        {
            bool bVal = false;

            switch (this.nID_RSC.Id)
            {
                case (int)Riscos.RuidoContinuo:
                    bVal = (this.tVL_MED >= 115.0M);
                    break;

                case (int)Riscos.RuidoImpacto:
                    bVal = (this.tVL_MED >= 140.0M);
                    break;

                case (int)Riscos.AgentesQuimicos:
                case (int)Riscos.AgentesQuimicosB:
                case (int)Riscos.AgentesQuimicosC:
                case (int)Riscos.AgentesQuimicosD:
                case (int)Riscos.ACGIH:
                case (int)Riscos.AsbestosPoeirasMinerais:

                    LimiteToleranciaAgenteQuimico limiteTol = new LimiteToleranciaAgenteQuimico();
                    limiteTol.Find("IdAgenteQuimico=" + this.nID_AG_NCV.Id
                                + " AND IdUnidade=" + this.nID_UN.Id);

                    if (limiteTol.Id != 0 && limiteTol.ValorLimiteTolTeto != 0)
                        bVal = (this.tVL_MED >= limiteTol.ValorLimiteTolTeto);
                    else if (limiteTol.Id != 0 && limiteTol.ValorLimiteTolTWA != 0)
                        bVal = (this.tVL_MED >= (limiteTol.ValorLimiteTolTWA * FatorDesvio(limiteTol.ValorLimiteTolTWA, this.nID_UN)));
                    else if (this.tVL_MED != 0.0M && this.sVL_LIM_TOL != 0.0M)
                        bVal = (this.tVL_MED >= (this.sVL_LIM_TOL * FatorDesvio(this.sVL_LIM_TOL, this.nID_UN)));

                    break;
            }
            return bVal;
        }

        private static decimal FatorDesvio(decimal limite, Unidade unidade)
        {
            decimal ret = 0.0M;

            if (unidade.Id == (int)Unidade.Unidades.ppm
                || unidade.Id == (int)Unidade.Unidades.mg_m3)
            {
                if (limite > 0 && limite < 1)
                    ret = 3M;
                else if (limite >= 1 && limite < 10)
                    ret = 2M;
                else if (limite >= 10 && limite < 100)
                    ret = 1.5M;
                else if (limite >= 100 && limite < 1000)
                    ret = 1.25M;
                else if (limite >= 1000)
                    ret = 1.1M;
            }

            return ret;
        }

        #endregion

        #region EPI/EPC Eficaz

        public bool GetEficaz()
        {
            if (!this.gINSALUBRE)
                return true;
            else if (gINSALUBRE && !gNET)
                return false;
            else if (gINSALUBRE && gNET)
                return true;
            else
                return false;
        }


        public bool EPIEficaz()
        {
            ArrayList listEpiRCM = new EpiRecomendado().Find("nID_PPRA=" + this.Id);
            ArrayList listEpiEXTE = new EpiExistente().Find("nID_PPRA=" + this.Id);
            if (listEpiRCM.Count > 0)
            {
                if (listEpiRCM.Count == listEpiEXTE.Count)
                    return true;
                else
                    return false;
            }
            else
                return false;
        }


        public bool EPCEficaz()
        {
            if (this._mDS_EPC_RCM != string.Empty)
            {
                if (this._mDS_EPC_RCM == this._mDS_EPC_EXTE)
                    return true;
                else
                    return false;
            }
            else
                return false;
        }

        public ArrayList GetListaEPI()
        {
            ArrayList list = new ArrayList();
            ArrayList listEpiExistente = new EpiExistente().Find("nID_PPRA=" + this.Id);
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

        public ArrayList GetListaEPI_Id()
        {
            ArrayList list = new ArrayList();
            ArrayList listEpiExistente = new EpiExistente().Find("nID_PPRA=" + this.Id);
            foreach (EpiExistente epiExte in listEpiExistente)
            {
                epiExte.nID_EPI.Find();
                list.Add(epiExte.nID_EPI.Id.ToString());
            }
            list.Sort();
            return list;
        }


        public string GetEpi()
        {
            return this.GetEpi("\n");
        }

        public string GetEpi(string strConcatenar)
        {
            StringBuilder str = new StringBuilder();

            ArrayList listEpi = this.GetListaEPI();

            foreach (string epi in listEpi)
            {
                if (str.ToString().IndexOf(epi.ToString()) == -1)
                {
                    str.Append(epi.ToString());
                    str.Append(strConcatenar);
                }
            }
            if (listEpi.Count == 0)
            {
                //str.Append("Inexistente");
                str.Append("Não aplicável");
            }

            return str.ToString();
        }
        #endregion

        #endregion

        #region Reconhecimento PPRA

        #region GetReconhecimentoPPRA

        public string GetReconhecimentoPPRA(bool IsHTML)
        {
            if (this.mDS_FRS_RSC == string.Empty)
                return this.GetReconhecimentoPPRAPadrao(IsHTML);
            else
            {
                StringBuilder str = new StringBuilder();

                if (IsHTML)
                    str.Append("<p align='justify'><Font color='navy'>");

                str.Append(this.mDS_FRS_RSC);

                if (IsHTML)
                    str.Append("</Font></p>");

                return str.ToString();
            }
        }
        #endregion

        #region GetReconhecimentoPPRAPadrao bak

        //public string GetReconhecimentoPPRAPadrao()
        //{
        //    StringBuilder str = new StringBuilder();

        //    switch (this.nID_RSC.Id)
        //    {
        //        case (int)Riscos.RuidoContinuo:
        //        case (int)Riscos.RuidoImpacto:
        //            str.Append(this.GetNomeAgente());
        //            str.Append(" - exposição " + this.GetModoExposicao());
        //            str.Append(" durante a jornada de trabalho,");
        //            str.Append(" cuja intensidade foi obtida através de avaliação quantitativa por dosimetria.");
        //            str.Append(GetFonteGeradora());
        //            str.Append(" Trajetória e meios de propagação: transmissão onidirecional por via aérea.");
        //            break;

        //        case (int)Riscos.Calor:
        //            str.Append("Calor");
        //            str.Append(" - exposição " + this.GetModoExposicao());
        //            str.Append(" durante a jornada de trabalho,");
        //            str.Append(" cuja intensidade foi obtida através de avaliação quantitativa com utilização de monitor stress térmico (termômetro de globo digital).");
        //            str.Append(GetFonteGeradora());
        //            str.Append(" Trajetória e meios de propagação: radiação onidirecional por via aérea.");
        //            break;

        //        case (int)Riscos.RadiacoesNaoIonizantes:
        //            this.nID_AG_NCV.Find();
        //            str.Append("Radiação não Ionizante");
        //            str.Append(" - exposição " + this.GetModoExposicao());
        //            str.Append(" a " + this.nID_AG_NCV.Nome.ToLower() + ",");
        //            str.Append(" durante a jornada de trabalho,");
        //            str.Append(" por contato dermal, constatada na avaliação qualitativa realizada através de inspeção no local de trabalho.");
        //            str.Append(GetFonteGeradora());
        //            str.Append(" Trajetória e meios de propagação: transmissão através de ondas eletro magnéticas.");
        //            break;

        //        case (int)Riscos.Frio:
        //            str.Append("Frio");
        //            str.Append(" - exposição " + this.GetModoExposicao());
        //            str.Append(" durante a jornada de trabalho,");
        //            str.Append(" cuja intensidade foi obtida através de avaliação qualitativa realizada através de inspeção no local de trabalho.");
        //            str.Append(GetFonteGeradora());
        //            str.Append(" Trajetória e meios de propagação: transmissão onidirecional por via aérea a baixas temperaturas oriundas de locais artificialmente frios.");
        //            break;

        //        case (int)Riscos.Umidade:
        //            str.Append("Umidade");
        //            str.Append(" - exposição " + this.GetModoExposicao());
        //            str.Append(" durante a jornada de trabalho,");
        //            str.Append(" a umidade excessiva, por contato dermal constatada na avaliação qualitativa realizada através de inspeção no local de trabalho.");
        //            str.Append(GetFonteGeradora());
        //            str.Append(" Trajetória e meios de propagação: contato direto com a água.");
        //            break;

        //        case (int)Riscos.AsbestosPoeirasMinerais:
        //            this.nID_AG_NCV.Find();
        //            str.Append("Poeira (aerodispersóide)");
        //            str.Append(" - exposição " + this.GetModoExposicao());
        //            str.Append(" por inalação a " + this.nID_AG_NCV.Nome + ", ");
        //            str.Append(" durante a jornada de trabalho,");
        //            str.Append(" constatada na avaliação quantitativa realizada através de inspeção no local de trabalho.");
        //            str.Append(GetFonteGeradora());
        //            str.Append(" Trajetória e meios de propagação: rupturas de partículas produzidas mecanicamente, em suspensão dispersas no ambiente");
        //            break;

        //        case (int)Riscos.AgentesQuimicosAnexo13:
        //            this.nID_AG_NCV.Find();
        //            str.Append(this.GetMateriaPrima());
        //            str.Append(" - exposição " + this.GetModoExposicao());
        //            str.Append(this.GetPenetracaoOrganismo());
        //            str.Append(" durante a jornada de trabalho,");
        //            str.Append(" constatada na avaliação qualitativa realizada através de inspeção no local de trabalho.");
        //            str.Append(this.GetFonteGeradora());
        //            str.Append(this.GetTrajetoriaMeioPropagacao());
        //            break;

        //        case (int)Riscos.ACGIH:
        //        case (int)Riscos.AgentesQuimicos:
        //        case (int)Riscos.AgentesQuimicosB:
        //        case (int)Riscos.AgentesQuimicosC:
        //        case (int)Riscos.AgentesQuimicosD:
        //            str.Append(this.GetNomeAgente());
        //            str.Append(" - exposição " + this.GetModoExposicao());
        //            str.Append(this.GetPenetracaoOrganismo());
        //            str.Append(" durante a jornada de trabalho,");
        //            str.Append(" cuja concentração foi obtida através de avaliação quantitativa.");
        //            str.Append(this.GetFonteGeradora());
        //            str.Append(this.GetTrajetoriaMeioPropagacao());
        //            break;

        //        case (int)Riscos.AgentesBiologicos:
        //            this.nID_TP_ATV.Find();
        //            str.Append("Agentes Biológicos");
        //            str.Append(" - exposição " + this.GetModoExposicao() + " a microorganismos infecto-contagiosos nos trabalhos ou operações em contato com: ");
        //            str.Append(this.tDS_MAT_PRM);
        //            str.Append(" Constatada na avaliação qualitativa realizada através de inspeção no local de trabalho.");
        //            str.Append(GetFonteGeradora());
        //            str.Append(" Trajetória e meios de propagação: transmissão via aérea por inalação e/ou contato dermal.");
        //            break;

        //        default:
        //            str.Append(this.GetNomeAgente());
        //            str.Append(" - exposição " + this.GetModoExposicao());
        //            str.Append(" durante a jornada de trabalho,");
        //            str.Append(" constatada na avaliação qualitativa realizada através de inspeção no local de trabalho.");
        //            str.Append(this.GetFonteGeradora());
        //            str.Append(this.GetTrajetoriaMeioPropagacao());
        //            break;
        //    }
        //    return str.ToString();
        //}
        #endregion

        #region GetReconhecimentoPPRAPadrao
        public string GetReconhecimentoPPRAPadrao(bool IsHTML)
        {
            if (this.Id == 0)
                return string.Empty;

            StringBuilder str = new StringBuilder();

            if (IsHTML)
                str.Append("<p align='justify'><Font color='navy'>");


            this.nID_AG_NCV.Find();
            if (this.nID_AG_NCV.Codigo_eSocial != null && this.nID_AG_NCV.Nome.ToUpper().Trim() == "AUSÊNCIA DE FATOR DE RISCO") //  && this.nID_AG_NCV.Codigo_eSocial.Trim() == "09.01.001")
            {
                str.Append(this.nID_AG_NCV.Nome);

                if (IsHTML)
                    str.Append("</b>");

                str.Append(" (código eSocial:" + this.nID_AG_NCV.Codigo_eSocial.Trim() + ") ");

            }
            else
            {


                switch (this.nID_RSC.Id)
                {
                    case (int)Riscos.RuidoContinuo:
                    case (int)Riscos.RuidoImpacto:
                        if (IsHTML)
                            str.Append("<b>");
                        str.Append(this.GetNomeAgente());

                        if (this.Codigo_eSocial != null && this.Codigo_eSocial.Trim() != "" && this.Codigo_eSocial.Trim() != "0")
                        {
                            str.Append(" (código eSocial:" + this.Codigo_eSocial.Trim() + ") ");
                        }

                        if (IsHTML)
                            str.Append("</b>");
                        str.Append(" - exposição " + this.GetModoExposicao());
                        str.Append(" durante a jornada de trabalho,");
                        if (this.nID_EQP_MED.ToString().Trim() != "")
                        {
                            str.Append(" cuja intensidade foi obtida através de avaliação quantitativa com utilização de " + this.nID_EQP_MED.ToString() + " .");
                        }
                        else
                        {
                            str.Append(" cuja intensidade foi obtida através de avaliação quantitativa por dosimetria .");
                        }
                        str.Append(GetFonteGeradora());
                        //str.Append(" Trajetória e meios de propagação: transmissão onidirecional por via aérea.");
                        str.Append(this.GetTrajetoriaMeioPropagacao().ToString()); //radiação onidirecional por via aérea.");
                        break;

                    case (int)Riscos.Calor:
                        if (IsHTML)
                            str.Append("<b>");

                        str.Append("Calor");

                        if (IsHTML)
                            str.Append("</b>");

                        if (this.Codigo_eSocial != null && this.Codigo_eSocial.Trim() != "" && this.Codigo_eSocial.Trim() != "0")
                        {
                            str.Append(" (código eSocial:" + this.Codigo_eSocial.Trim() + ") ");
                        }

                        str.Append(" - exposição " + this.GetModoExposicao());
                        str.Append(" durante a jornada de trabalho,");

                        if (this.nID_EQP_MED.ToString().Trim() != "")
                        {
                            str.Append(" cuja intensidade foi obtida através de avaliação quantitativa com utilização de " + this.nID_EQP_MED.ToString() + " .");
                        }
                        else
                        {
                            str.Append(" cuja intensidade foi obtida através de avaliação quantitativa com utilização de monitor stress térmico (termômetro de globo digital).");
                        }
                        str.Append(GetFonteGeradora());
                        str.Append(this.GetTrajetoriaMeioPropagacao().ToString()); //radiação onidirecional por via aérea.");

                        if (this.Calculo_Limite_Calor == false)
                        {
                            if (this.nID_ATV == 1)
                                str.Append(" Tipo de Atividade: trabalho leve");
                            else if (this.nID_ATV == 2)
                                str.Append(" Tipo de Atividade: trabalho moderado");
                            else if (this.nID_ATV == 3)
                                str.Append(" Tipo de Atividade: trabalho pesado");
                            else
                                str.Append("");
                        }
                        else
                        {

                            if (this.nID_ATV == 1)
                                str.Append(" Tipo de Atividade: Sentado - 100(W) - Em Repouso");
                            else if (this.nID_ATV == 2)
                                str.Append(" Tipo de Atividade: Sentado - 126 (W) - Trabalho leve com as mãos");
                            else if (this.nID_ATV == 3)
                                str.Append(" Tipo de Atividade: Sentado - 153 (W) - Trabalho Moderado com as mãos");
                            else if (this.nID_ATV == 4)
                                str.Append(" Tipo de Atividade: Sentado - 171 (W) - Trabalho pesado com as mãos");
                            else if (this.nID_ATV == 5)
                                str.Append(" Tipo de Atividade: Sentado - 162 (W) - Trabalho leve com um braço");
                            else if (this.nID_ATV == 6)
                                str.Append(" Tipo de Atividade: Sentado - 198 (W) - Trabalho moderado com um braço");
                            else if (this.nID_ATV == 7)
                                str.Append(" Tipo de Atividade: Sentado - 234 (W) - Trabalho pesado com um braço");
                            else if (this.nID_ATV == 8)
                                str.Append(" Tipo de Atividade: Sentado - 216 (W) - Trabalho leve com dois braços");
                            else if (this.nID_ATV == 9)
                                str.Append(" Tipo de Atividade: Sentado - 252 (W) - Trabalho moderado com dois braços");
                            else if (this.nID_ATV == 10)
                                str.Append(" Tipo de Atividade: Sentado - 288 (W) - Trabalho pesado com dois braços");
                            else if (this.nID_ATV == 11)
                                str.Append(" Tipo de Atividade: Sentado - 324 (W) - Trabalho leve com braços e pernas");
                            else if (this.nID_ATV == 12)
                                str.Append(" Tipo de Atividade: Sentado - 441 (W) - Trabalho moderado com braços e pernas");
                            else if (this.nID_ATV == 13)
                                str.Append(" Tipo de Atividade: Sentado - 603 (W) - Trabalho pesado com braços e pernas");
                            else if (this.nID_ATV == 14)
                                str.Append(" Tipo de Atividade: Em pé, agachado ou ajoelhado - 126 (W) - Em repouso");
                            else if (this.nID_ATV == 15)
                                str.Append(" Tipo de Atividade: Em pé, agachado ou ajoelhado - 153 (W) - Trabalho leve com as mãos");
                            else if (this.nID_ATV == 16)
                                str.Append(" Tipo de Atividade: Em pé, agachado ou ajoelhado - 180 (W) - Trabalho Moderado com as mãos");
                            else if (this.nID_ATV == 17)
                                str.Append(" Tipo de Atividade: Em pé, agachado ou ajoelhado - 198 (W) - Trabalho pesado com as mãos");
                            else if (this.nID_ATV == 18)
                                str.Append(" Tipo de Atividade: Em pé, agachado ou ajoelhado - 189 (W) - Trabalho leve com um braço");
                            else if (this.nID_ATV == 19)
                                str.Append(" Tipo de Atividade: Em pé, agachado ou ajoelhado - 225 (W) - Trabalho moderado com um braço");
                            else if (this.nID_ATV == 20)
                                str.Append(" Tipo de Atividade: Em pé, agachado ou ajoelhado - 261 (W) - Trabalho pesado com um braço");
                            else if (this.nID_ATV == 21)
                                str.Append(" Tipo de Atividade: Em pé, agachado ou ajoelhado - 243 (W) - Trabalho leve com dois braços");
                            else if (this.nID_ATV == 22)
                                str.Append(" Tipo de Atividade: Em pé, agachado ou ajoelhado - 279 (W) - Trabalho moderado com dois braços");
                            else if (this.nID_ATV == 23)
                                str.Append(" Tipo de Atividade: Em pé, agachado ou ajoelhado - 315 (W) - Trabalho pesado com dois braços");
                            else if (this.nID_ATV == 24)
                                str.Append(" Tipo de Atividade: Em pé, agachado ou ajoelhado - 351 (W) - Trabalho leve com o corpo");
                            else if (this.nID_ATV == 25)
                                str.Append(" Tipo de Atividade: Em pé, agachado ou ajoelhado - 468 (W) - Trabalho moderado com o corpo");
                            else if (this.nID_ATV == 26)
                                str.Append(" Tipo de Atividade: Em pé, agachado ou ajoelhado - 630 (W) - Trabalho pesado com o corpo");
                            else if (this.nID_ATV == 27)
                                str.Append(" Tipo de Atividade: Em pé, em movimento - 198 (W) - Andando no Plano - Sem Carga - 2 km/h");
                            else if (this.nID_ATV == 28)
                                str.Append(" Tipo de Atividade: Em pé, em movimento - 252 (W) - Andando no Plano - Sem Carga - 3 km/h");
                            else if (this.nID_ATV == 29)
                                str.Append(" Tipo de Atividade: Em pé, em movimento - 297 (W) - Andando no Plano - Sem Carga - 4 km/h");
                            else if (this.nID_ATV == 30)
                                str.Append(" Tipo de Atividade: Em pé, em movimento - 360 (W) - Andando no Plano - Sem Carga - 5 km/h");
                            else if (this.nID_ATV == 31)
                                str.Append(" Tipo de Atividade: Em pé, em movimento - 333 (W) - Andando no Plano - Com Carga - 10Kg, 4 km/h");
                            else if (this.nID_ATV == 32)
                                str.Append(" Tipo de Atividade: Em pé, em movimento - 450 (W) - Andando no Plano - Com Carga - 30Kg, 4 km/h");
                            else if (this.nID_ATV == 33)
                                str.Append(" Tipo de Atividade: Em pé, em movimento - 787 (W) - Correndo no Plano - 9 km/h");
                            else if (this.nID_ATV == 34)
                                str.Append(" Tipo de Atividade: Em pé, em movimento - 873 (W) - Correndo no Plano - 12 km/h");
                            else if (this.nID_ATV == 35)
                                str.Append(" Tipo de Atividade: Em pé, em movimento - 990 (W) - Correndo no Plano - 15 km/h");
                            else if (this.nID_ATV == 36)
                                str.Append(" Tipo de Atividade: Em pé, em movimento - 324 (W) - Subindo rampa - Sem Carga - com 5° de inclinação, 4 km/h");
                            else if (this.nID_ATV == 37)
                                str.Append(" Tipo de Atividade: Em pé, em movimento - 378 (W) - Subindo rampa - Sem Carga - com 15° de inclinação, 3 km/h");
                            else if (this.nID_ATV == 38)
                                str.Append(" Tipo de Atividade: Em pé, em movimento - 540 (W) - Subindo rampa - Sem Carga - com 25° de inclinação, 3 km/h");
                            else if (this.nID_ATV == 39)
                                str.Append(" Tipo de Atividade: Em pé, em movimento - 486 (W) - Subindo rampa - Com Carga de 20kg - com 15° de inclinação, 4 km/h");
                            else if (this.nID_ATV == 40)
                                str.Append(" Tipo de Atividade: Em pé, em movimento - 738 (W) - Subindo rampa - Com Carga de 20kg - com 25° de inclinação, 4 km/h");
                            else if (this.nID_ATV == 41)
                                str.Append(" Tipo de Atividade: Em pé, em movimento - 243 (W) - Descendo rampa - Sem Carga - com 5° de inclinação, 5 km/h");
                            else if (this.nID_ATV == 42)
                                str.Append(" Tipo de Atividade: Em pé, em movimento - 252 (W) - Descendo rampa - Sem Carga - com 15° de inclinação, 5 km/h");
                            else if (this.nID_ATV == 43)
                                str.Append(" Tipo de Atividade: Em pé, em movimento - 324 (W) - Descendo rampa - Sem Carga - com 25° de inclinação, 5 km/h");
                            else if (this.nID_ATV == 44)
                                str.Append(" Tipo de Atividade: Em pé, em movimento - 522 (W) - Subindo escada (80 degraus/min, altura 0,17m) - Sem Carga");
                            else if (this.nID_ATV == 45)
                                str.Append(" Tipo de Atividade: Em pé, em movimento - 648 (W) - Subindo escada (80 degraus/min, altura 0,17m) - Com Carga 20kg");
                            else if (this.nID_ATV == 46)
                                str.Append(" Tipo de Atividade: Em pé, em movimento - 279 (W) - Descendo escada (80 degraus/min, altura 0,17m) - Sem Carga");
                            else if (this.nID_ATV == 47)
                                str.Append(" Tipo de Atividade: Em pé, em movimento - 400 (W) - Descendo escada (80 degraus/min, altura 0,17m) - Com Carga 20kg");
                            else if (this.nID_ATV == 48)
                                str.Append(" Tipo de Atividade: Em pé, em movimento - 320 (W) - Trabalho moderado de braços (ex.:varrer, trabalho em almoxarifado");
                            else if (this.nID_ATV == 49)
                                str.Append(" Tipo de Atividade: Em pé, em movimento - 349 (W) - Trabalho moderado de levantar ou empurrar");
                            else if (this.nID_ATV == 50)
                                str.Append(" Tipo de Atividade: Em pé, em movimento - 391 (W) - Trabalho de empurrar carrinhos de mão, no mesmo plano, com carga");
                            else if (this.nID_ATV == 51)
                                str.Append(" Tipo de Atividade: Em pé, em movimento - 495 (W) - Trabalho carregar pesos ou com movimentoos vigorosos com os braços (ex.:foice)");
                            else if (this.nID_ATV == 52)
                                str.Append(" Tipo de Atividade: Em pé, em movimento - 524 (W) - Trabalho pesado de levantar, empurrar ou arrastar pesos (ex.:abertura valas)");
                            else
                                str.Append("");

                        }


                        if (this.bDESC == false)
                            str.Append("   Descanso Térmico: no próprio local de trabalho");
                        else
                            str.Append("   Descanso Térmico: em outro local de trabalho");



                        break;




                    case (int)Riscos.RadiacoesNaoIonizantes:
                        this.nID_AG_NCV.Find();
                        if (IsHTML)
                            str.Append("<b>");

                        str.Append("Radiação não Ionizante");

                        if (IsHTML)
                            str.Append("</b>");

                        if (this.Codigo_eSocial != null && this.Codigo_eSocial.Trim() != "" && this.Codigo_eSocial.Trim() != "0")
                        {
                            str.Append(" (código eSocial:" + this.Codigo_eSocial.Trim() + ") ");
                        }

                        str.Append(" - exposição " + this.GetModoExposicao());
                        str.Append(" a " + this.nID_AG_NCV.Nome.ToLower() + ",");
                        str.Append(" durante a jornada de trabalho,");
                        str.Append(" por contato dermal, constatada na avaliação qualitativa realizada através de inspeção no local de trabalho.");
                        str.Append(GetFonteGeradora());
                        str.Append(" Trajetória e meios de propagação: transmissão através de ondas eletro magnéticas.");
                        break;

                    case (int)Riscos.Frio:
                        if (IsHTML)
                            str.Append("<b>");

                        str.Append("Frio");

                        if (IsHTML)
                            str.Append("</b>");

                        if (this.Codigo_eSocial != null && this.Codigo_eSocial.Trim() != "" && this.Codigo_eSocial.Trim() != "0")
                        {
                            str.Append(" (código eSocial:" + this.Codigo_eSocial.Trim() + ") ");
                        }

                        str.Append(" - exposição " + this.GetModoExposicao());
                        str.Append(" durante a jornada de trabalho,");
                        str.Append(" cuja intensidade foi obtida através de avaliação qualitativa realizada através de inspeção no local de trabalho.");
                        str.Append(GetFonteGeradora());
                        str.Append(" Trajetória e meios de propagação: transmissão omnidirecional por via aérea a baixas temperaturas oriundas de locais artificialmente frios.");
                        break;

                    case (int)Riscos.Umidade:

                        if (IsHTML)
                            str.Append("<b>");

                        str.Append("Umidade");

                        if (IsHTML)
                            str.Append("</b>");

                        if (this.Codigo_eSocial != null && this.Codigo_eSocial.Trim() != "" && this.Codigo_eSocial.Trim() != "0")
                        {
                            str.Append(" (código eSocial:" + this.Codigo_eSocial.Trim() + ") ");
                        }

                        str.Append(" - exposição " + this.GetModoExposicao());
                        str.Append(" durante a jornada de trabalho,");
                        str.Append(" a umidade excessiva, por contato dermal constatada na avaliação qualitativa realizada através de inspeção no local de trabalho.");
                        str.Append(GetFonteGeradora());
                        str.Append(" Trajetória e meios de propagação: contato direto com a água.");
                        break;

                    case (int)Riscos.AsbestosPoeirasMinerais:
                        this.nID_AG_NCV.Find();

                        if (IsHTML)
                            str.Append("<b>");

                        str.Append("Poeira (aerodispersóide)");



                        if (this.nID_AG_NCV.Codigo_eSocial != null && this.nID_AG_NCV.Codigo_eSocial.Trim() != "" && this.nID_AG_NCV.Codigo_eSocial.Trim() != "0")
                        {
                            str.Append(" (código eSocial:" + this.nID_AG_NCV.Codigo_eSocial.Trim() + ") ");
                        }

                        if (IsHTML)
                            str.Append("</b>");

                        str.Append(" - exposição " + this.GetModoExposicao());
                        str.Append(" por inalação a " + this.nID_AG_NCV.Nome + ", ");
                        str.Append(" durante a jornada de trabalho,");
                        //str.Append(" constatada através de avaliação quantitativa.");

                        this.nID_EQP_MED.Find();
                        if (this.nID_EQP_MED.Nome.Trim() == "" || this.nID_EQP_MED.Nome.Trim().ToUpper().IndexOf("QUALITATIVA") > 0)
                        {
                            str.Append(" constatada através de avaliação qualitativa.");
                        }
                        else
                        {
                            str.Append(" constatada através de avaliação quantitativa.");
                        }

                        str.Append(GetFonteGeradora());
                        str.Append(" Trajetória e meios de propagação: rupturas de partículas produzidas mecanicamente, em suspensão dispersas no ambiente");
                        break;

                    case (int)Riscos.AgentesQuimicosAnexo13:
                        if (this.nID_AG_NCV.mirrorOld == null)
                            this.nID_AG_NCV.Find();

                        if (IsHTML)
                            str.Append("<b>");
                        str.Append(this.GetMateriaPrima());


                        this.nID_EQP_MED.Find();

                        if (this.nID_AG_NCV.Codigo_eSocial != null && this.nID_AG_NCV.Codigo_eSocial.Trim() != "" && this.nID_AG_NCV.Codigo_eSocial.Trim() != "0")
                        {
                            str.Append(" (código eSocial:" + this.nID_AG_NCV.Codigo_eSocial.Trim() + ") ");
                        }


                        if (IsHTML)
                            str.Append("</b>");

                        str.Append(" - exposição " + this.GetModoExposicao());
                        str.Append(this.GetPenetracaoOrganismo());
                        str.Append(" durante a jornada de trabalho,");
                        str.Append(" constatada na avaliação qualitativa realizada através de inspeção no local de trabalho.");
                        str.Append(this.GetFonteGeradora());
                        str.Append(this.GetTrajetoriaMeioPropagacao());
                        break;

                    case (int)Riscos.ACGIH:
                    case (int)Riscos.AgentesQuimicos:
                    case (int)Riscos.AgentesQuimicosB:
                    case (int)Riscos.AgentesQuimicosC:
                    case (int)Riscos.AgentesQuimicosD:
                        if (this.nID_AG_NCV.mirrorOld == null)
                            this.nID_AG_NCV.Find();

                        if (IsHTML)
                            str.Append("<b>");



                        if (this.nID_AG_NCV.Codigo_eSocial != null && this.nID_AG_NCV.Codigo_eSocial.Trim() != "" && this.nID_AG_NCV.Codigo_eSocial.Trim() != "0")
                        {
                            str.Append(this.GetNomeAgente() + " (código eSocial:" + this.nID_AG_NCV.Codigo_eSocial.Trim() + ") ");
                        }
                        else
                        {
                            str.Append(this.GetNomeAgente());
                        }

                        if (IsHTML)
                            str.Append("</b>");

                        str.Append(" - exposição " + this.GetModoExposicao());
                        str.Append(this.GetPenetracaoOrganismo());
                        str.Append(" durante a jornada de trabalho,");

                        this.nID_EQP_MED.Find();

                        if (this.nID_EQP_MED.Nome.Trim() == "" || this.nID_EQP_MED.Nome.Trim().ToUpper().IndexOf("QUALITATIVA") > 0)
                        {
                            str.Append(" cuja concentração foi obtida através de avaliação qualitativa.");
                        }
                        else
                        {
                            str.Append(" cuja concentração foi obtida através de avaliação quantitativa.");
                        }
                        str.Append(this.GetFonteGeradora());
                        str.Append(this.GetTrajetoriaMeioPropagacao());
                        break;

                    case (int)Riscos.AgentesBiologicos:
                        this.nID_TP_ATV.Find();

                        if (IsHTML)
                            str.Append("<b>");

                        str.Append("Agentes Biológicos");

                        if (IsHTML)
                            str.Append("</b>");

                        if (this.Codigo_eSocial != null && this.Codigo_eSocial.Trim() != "" && this.Codigo_eSocial.Trim() != "0")
                        {
                            str.Append(" (código eSocial:" + this.Codigo_eSocial.Trim() + ") ");
                        }


                        str.Append(" - exposição " + this.GetModoExposicao() + " a microorganismos infecto-contagiosos nos trabalhos ou operações em contato com: ");
                        str.Append(this.tDS_MAT_PRM);
                        str.Append(" Constatada na avaliação qualitativa realizada através de inspeção no local de trabalho.");
                        str.Append(GetFonteGeradora());
                        str.Append(" Trajetória e meios de propagação: transmissão via aérea por inalação e/ou contato dermal.");
                        break;


                    case (int)Riscos.VibracoesVCI:
                        if (IsHTML)
                            str.Append("<b>");

                        str.Append(this.GetNomeAgente());

                        if (IsHTML)
                            str.Append("</b>");

                        if (this.Codigo_eSocial != null && this.Codigo_eSocial.Trim() != "" && this.Codigo_eSocial.Trim() != "0")
                        {
                            str.Append(" (código eSocial:" + this.Codigo_eSocial.Trim() + ") ");
                        }


                        str.Append(" - " + this.tVL_MED.ToString() + " " + this.nID_UN.ToString() + " | " + this.tVL_MED2.ToString() + " " + this.nID_UN2.ToString());
                        str.Append(" - exposição " + this.GetModoExposicao());
                        str.Append(" durante a jornada de trabalho,");
                        str.Append(" constatada na avaliação quantitativa realizada através de inspeção no local de trabalho.");
                        str.Append(this.GetFonteGeradora());
                        str.Append(this.GetTrajetoriaMeioPropagacao());
                        break;

                    case (int)Riscos.Vibracoes:
                        if (IsHTML)
                            str.Append("<b>");

                        str.Append(this.GetNomeAgente());

                        if (IsHTML)
                            str.Append("</b>");

                        if (this.Codigo_eSocial != null && this.Codigo_eSocial.Trim() != "" && this.Codigo_eSocial.Trim() != "0")
                        {
                            str.Append(" (código eSocial:" + this.Codigo_eSocial.Trim() + ") ");
                        }


                        str.Append(" - " + this.tVL_MED.ToString() + " " + this.nID_UN.ToString() );
                        str.Append(" - exposição " + this.GetModoExposicao());
                        str.Append(" durante a jornada de trabalho,");
                        str.Append(" constatada na avaliação quantitativa realizada através de inspeção no local de trabalho.");
                        str.Append(this.GetFonteGeradora());
                        str.Append(this.GetTrajetoriaMeioPropagacao());
                        break;


                    default:
                        if (IsHTML)
                            str.Append("<b>");

                        str.Append(this.GetNomeAgente());

                        if (IsHTML)
                            str.Append("</b>");

                        if (this.Codigo_eSocial != null && this.Codigo_eSocial.Trim() != "" && this.Codigo_eSocial.Trim() != "0")
                        {
                            str.Append(" (código eSocial:" + this.Codigo_eSocial.Trim() + ") ");
                        }

                        str.Append(" - exposição " + this.GetModoExposicao());
                        str.Append(" durante a jornada de trabalho,");
                        str.Append(" constatada na avaliação qualitativa realizada através de inspeção no local de trabalho.");
                        str.Append(this.GetFonteGeradora());
                        str.Append(this.GetTrajetoriaMeioPropagacao());
                        break;
                }
            }

            if (IsHTML)
                str.Append("</Font></p>");

            return str.ToString();
        }
        #endregion

        #region GetPenetracaoOrganismo

        private string GetPenetracaoOrganismo()
        {
            string ret;

            if (this.nIND_PENETR_ORG_AG_NCV == (int)PenetracaoOrganismoAgenteQuimico.NaoInformada)
                ret = string.Empty;
            else if (this.nIND_PENETR_ORG_AG_NCV == (int)PenetracaoOrganismoAgenteQuimico.Inalacao)
                ret = " por inalação,";
            else if (this.nIND_PENETR_ORG_AG_NCV == (int)PenetracaoOrganismoAgenteQuimico.ContatoDermal)
                ret = " por contato dermal,";
            else if (this.nIND_PENETR_ORG_AG_NCV == (int)PenetracaoOrganismoAgenteQuimico.InalacaoContatoDermal)
                ret = " por inalação e contato dermal,";
            else
                ret = string.Empty;

            return ret;
        }
        #endregion

        #region GetModoExposicao

        public string GetModoExposicao()
        {
            StringBuilder str = new StringBuilder();

            if (this.nIND_MOD_EXP == (int)ModoExposicao.Permanente)
                str.Append("Permanente");
            else if (this.nIND_MOD_EXP == (int)ModoExposicao.OcasionalIntermitente)
                str.Append("Ocasional e Intermitente");
            else if (this.nIND_MOD_EXP == (int)ModoExposicao.HabitualPermanente)
                str.Append("Habitual e Permanente");
            else if (this.nIND_MOD_EXP == (int)ModoExposicao.HabitualIntermitente)
                str.Append("Habitual e Intermitente");
            else
                str.Append("Eventual");

            return str.ToString().ToLower();
        }
        #endregion

        #region GetJornadaTrabalho

        private string GetJornadaTrabalho()
        {
            return " durante a jornada de trabalho,";
        }
        #endregion

        #region GetAvaliacao

        private string GetAvaliacao()
        {
            StringBuilder str = new StringBuilder();
            if (this.nID_RSC.Qualitativo)
                str.Append(" constatada na avaliação qualitativa realizada através de inspeção no local de trabalho.");
            else
            {
                //cuja intensidade/temperatura/concentração
                str.Append(" foi obtida através de avaliação quantitativa");
                if (this.nID_EQP_MED.Id == 0)
                    str.Append(".");
                else
                {
                    this.nID_EQP_MED.Find();
                    if (this.nID_EQP_MED.Id != 0)
                        str.Append(" com utilização de " + this.nID_EQP_MED.Nome + ".");
                    else
                        str.Append(".");
                }
            }
            return str.ToString();
        }
        #endregion

        #region GetFonteGeradora

        private string GetFonteGeradora()
        {
            StringBuilder str = new StringBuilder();
            if (this.tDS_FTE_GER != string.Empty)
                str.Append(" Fonte geradora: " + this.tDS_FTE_GER.ToLower().Replace(".", "") + ".");
            else
            {
                if (this.nID_RSC.Id == (int)Riscos.RuidoContinuo)
                    str.Append(" Fonte geradora: movimentação de máquinas, equipamentos e pessoas.");
                else if (this.nID_RSC.Id == (int)Riscos.Calor)
                    str.Append(" Fonte geradora: irradiação solar indireta.");
            }

            if (this.tDS_DANO_REL_SAU != string.Empty)
            {
                //str.Append("  Danos Relacionado à saúde: " + this.tDS_DANO_REL_SAU.Replace(".", "") + ".");
                str.Append("  Possíveis Lesões ou Agravos à Saúde: " + this.tDS_DANO_REL_SAU.Replace(".", "") + ".");
            }

            return str.ToString();
        }
        #endregion

        #region GetMateriaPrima

        private string GetMateriaPrima()
        {
            StringBuilder str = new StringBuilder();

            if (this.nID_MAT_PRM.Id == 0 && this.tDS_MAT_PRM != string.Empty)
            {
                str.Append(this.tDS_MAT_PRM);
            }
            else if (this.nID_MAT_PRM.Id != 0)
            {
                if (this.nID_MAT_PRM.mirrorOld == null)
                    this.nID_MAT_PRM.Find();


                if (this.nID_MAT_PRM.Codigo_eSocial != "09.01.001" && this.nID_MAT_PRM.Codigo_eSocial.Trim() != "")
                {
                    str.Append(this.nID_MAT_PRM.tNO_MAT_PRM + "(código eSocial:" + this.nID_MAT_PRM.Codigo_eSocial + ")");
                }
                else
                {
                    str.Append(this.nID_MAT_PRM.tNO_MAT_PRM);
                }
            }
            return str.ToString();
        }
        #endregion

        #region GetTrajetoriaMeioPropagacao

        private string GetTrajetoriaMeioPropagacao()
        {
            StringBuilder str = new StringBuilder();

            if (this.nID_TRJ.Id != 0 && this.nID_MEIO_PRPG.Id != 0)
                str.Append(" Trajetória e meios de propagação: ");
            if (this.nID_TRJ.Id != 0)
            {
                this.nID_TRJ.Find();
                str.Append(this.nID_TRJ.tDS_TRAJ_RSC.ToLower().Replace(".", string.Empty) + " ");
            }
            if (this.nID_MEIO_PRPG.Id != 0)
            {
                this.nID_MEIO_PRPG.Find();
                str.Append(this.nID_MEIO_PRPG.tDS_MEIO_PROP.ToLower().Replace(".", string.Empty) + ".");
            }
            return str.ToString();
        }
        #endregion

        #region GetNomeAgenteBilologico

        private string GetNomeAgenteBilologico()
        {
            string ret = string.Empty;

            if (tDS_MAT_PRM != string.Empty && tDS_FTE_GER == string.Empty)
                ret = this.nID_RSC.DescricaoResumido + " - " + this.tDS_MAT_PRM;
            else if (tDS_MAT_PRM != string.Empty && tDS_FTE_GER != string.Empty)
            {
                if (this.tDS_MAT_PRM.IndexOf(this.tDS_FTE_GER) == -1)
                    ret = this.nID_RSC.DescricaoResumido + " - " + this.tDS_MAT_PRM + " (" + this.tDS_FTE_GER + ")";
                else
                    ret = this.nID_RSC.DescricaoResumido + " - " + this.tDS_MAT_PRM;
            }
            else if (tDS_MAT_PRM == string.Empty && tDS_FTE_GER != string.Empty)
                ret = this.nID_RSC.DescricaoResumido + " (" + this.tDS_FTE_GER + ")";
            else if (tDS_MAT_PRM == string.Empty && tDS_FTE_GER == string.Empty)
                ret = this.nID_RSC.DescricaoResumido;
            else
                ret = this.nID_RSC.DescricaoResumido;

            return ret;
        }
        #endregion

        #endregion
    }
}