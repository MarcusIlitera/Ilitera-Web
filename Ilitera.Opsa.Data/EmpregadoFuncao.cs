using System;
using System.Data;
using System.Text;
using System.Collections;
using System.Collections.Generic;

using Ilitera.Data;
using Ilitera.Common;

namespace Ilitera.Opsa.Data
{
    [Database("sied_novo", "tblEMPREGADO_FUNCAO", "nID_EMPREGADO_FUNCAO")]
    public class EmpregadoFuncao : Ilitera.Data.Table
    {
        #region Properties

        private int _nID_EMPREGADO_FUNCAO;
        private Empregado _nID_EMPREGADO;
        private Funcao _nID_FUNCAO;
        private Cargo _nID_CARGO;
        private Setor _nID_SETOR;
        private DateTime _hDT_INICIO = DateTime.Today;
        private DateTime _hDT_TERMINO;
        private TempoExposicao _nID_TEMPO_EXP;
        private int _nIND_GFIP;
        private bool _bHOUVE_MUDANCA;
        private bool _bTEM_LAUDO = true;
        private Ghe _nID_GHE_AE;
        private Cliente _nID_EMPR;
        private string _tOBSERVACAO = string.Empty;
        private string _tDS_FUNCAO = string.Empty;
        private ImportacaoAutomatica _nID_IMPORTACAO_AUTOMATICA;


        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public EmpregadoFuncao()
        {

        }

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public EmpregadoFuncao(int Id)
        {
            this.Find(Id);
        }
        public override void Inicialize()
        {
            base.Inicialize();

            this.nID_TEMPO_EXP.Id = 15;
        }
        public override int Id
        {
            get { return _nID_EMPREGADO_FUNCAO; }
            set { _nID_EMPREGADO_FUNCAO = value; }
        }
        [Obrigatorio(true, "O empregado � obrigat�rio")]
        public Empregado nID_EMPREGADO
        {
            get { return _nID_EMPREGADO; }
            set { _nID_EMPREGADO = value; }
        }
        [Obrigatorio(true, "A Fun��o � obrigat�ria!")]
        public Funcao nID_FUNCAO
        {
            get { return _nID_FUNCAO; }
            set { _nID_FUNCAO = value; }
        }
        public Cargo nID_CARGO
        {
            get { return _nID_CARGO; }
            set { _nID_CARGO = value; }
        }
        [Obrigatorio(true, "O Setor � obrigat�rio!")]
        public Setor nID_SETOR
        {
            get { return _nID_SETOR; }
            set { _nID_SETOR = value; }
        }
        [Obrigatorio(true, "A Data de Inicio da classifica��o funcional � obrigat�ria!")]
        public DateTime hDT_INICIO
        {
            get { return _hDT_INICIO; }
            set { _hDT_INICIO = value; }
        }
        public DateTime hDT_TERMINO
        {
            get { return _hDT_TERMINO; }
            set { _hDT_TERMINO = value; }
        }
        public TempoExposicao nID_TEMPO_EXP
        {
            get { return _nID_TEMPO_EXP; }
            set { _nID_TEMPO_EXP = value; }
        }
        public int nIND_GFIP
        {
            get { return _nIND_GFIP; }
            set { _nIND_GFIP = value; }
        }
        public bool bHOUVE_MUDANCA
        {
            get { return _bHOUVE_MUDANCA; }
            set { _bHOUVE_MUDANCA = value; }
        }
        public bool bTEM_LAUDO
        {
            get { return _bTEM_LAUDO; }
            set { _bTEM_LAUDO = value; }
        }
        public Ghe nID_GHE_AE
        {
            get { return _nID_GHE_AE; }
            set { _nID_GHE_AE = value; }
        }
        public Cliente nID_EMPR
        {
            get { return _nID_EMPR; }
            set { _nID_EMPR = value; }
        }
        public string tDS_FUNCAO
        {
            get { return _tDS_FUNCAO; }
            set { _tDS_FUNCAO = value; }
        }
        public string tOBSERVACAO
        {
            get { return _tOBSERVACAO; }
            set { _tOBSERVACAO = value; }
        }
        public ImportacaoAutomatica nID_IMPORTACAO_AUTOMATICA
        {
            get { return _nID_IMPORTACAO_AUTOMATICA; }
            set { _nID_IMPORTACAO_AUTOMATICA = value; }
        }
        #endregion

        #region Override Base

        public int Save(bool ComValidade)
        {
            if (ComValidade)
                return this.Save();
            else
                return base.Save();
        }

        public override int Save()
        {
            if (this.hDT_TERMINO != new DateTime() && this.hDT_INICIO > this.hDT_TERMINO)
                throw new Exception("A Data de T�rmino n�o pode ser menor ou igual que a Data de Inicio na Classifica��o Funcional!");

            if (this.hDT_TERMINO != new DateTime() && this.hDT_TERMINO >= DateTime.Now.AddMonths(3))
                throw new Exception("A Data de T�rmino n�o pode ser uma data futura superior a 3 meses na Classifica��o Funcional!");

            if (this.Id == 0)
            {
                try
                {
                    ArrayList list = new EmpregadoFuncao().Find("nID_EMPREGADO=" + this.nID_EMPREGADO.Id
                                                                + " AND hDT_TERMINO IS NULL"
                                                                + " ORDER BY hDT_INICIO DESC");

                    if (list.Count > 0)
                    {
                        if (this.hDT_INICIO > ((EmpregadoFuncao)list[0]).hDT_INICIO)
                        {
                            EmpregadoFuncao empregadoFuncao = ((EmpregadoFuncao)list[0]);
                            empregadoFuncao.hDT_TERMINO = this.hDT_INICIO.AddDays(-1);
                            empregadoFuncao.Save();
                        }
                    }
                }
                catch { }
            }

            return base.Save();
        }

        #endregion

        #region Metodos
        
        #region LaudoTecnico

        public LaudoTecnico GetLaudoTecnico()
        {
            LaudoTecnico laudo;

            if (this.Id == 0)
            {
                if (this.nID_EMPREGADO.nID_EMPR == null)
                    this.nID_EMPREGADO.Find();

                laudo = LaudoTecnico.GetUltimoLaudo(this.nID_EMPR.Id);
            }
            else
            {
                ArrayList listGheEmpregado = new GheEmpregado().Find(
                    "nID_EMPREGADO_FUNCAO=" + this.Id
                    + " AND nID_LAUD_TEC IN (SELECT nID_LAUD_TEC FROM tblLAUDO_TEC WHERE nID_EMPR=" + this.nID_EMPR.Id
                    + " AND (nID_PEDIDO IN (SELECT IdPedido FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.Pedido WHERE DataConclusao IS NOT NULL) OR bAE =1))"
                    + " ORDER BY (SELECT hDT_LAUDO FROM tblLAUDO_TEC WHERE nID_LAUD_TEC = tblFUNC_EMPREGADO.nID_LAUD_TEC) DESC");

                if (listGheEmpregado.Count > 0)
                    laudo = ((GheEmpregado)listGheEmpregado[0]).nID_LAUD_TEC;
                else
                    laudo = LaudoTecnico.GetUltimoLaudo(this.nID_EMPR.Id);
            }
            if (laudo.mirrorOld == null)
                laudo.Find();

            return laudo;
        }

        public LaudoTecnico GetLaudoTecnico_LTCAT()
        {
            LaudoTecnico laudo;

            if (this.Id == 0)
            {
                if (this.nID_EMPREGADO.nID_EMPR == null)
                    this.nID_EMPREGADO.Find();

                laudo = LaudoTecnico.GetUltimoLaudo(this.nID_EMPR.Id);
            }
            else
            {
                ArrayList listGheEmpregado = new GheEmpregado().Find(
                    "nID_EMPREGADO_FUNCAO=" + this.Id
                    + " AND nID_LAUD_TEC IN (SELECT nID_LAUD_TEC FROM tblLAUDO_TEC WHERE nID_EMPR=" + this.nID_EMPR.Id
                    + " AND (nID_PEDIDO IN (SELECT IdPedido FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.Pedido  WHERE DataConclusao IS NOT NULL) OR bAE =1))"
                    + " ORDER BY (SELECT hDT_LAUDO FROM tblLAUDO_TEC WHERE nID_LAUD_TEC = tblFUNC_EMPREGADO.nID_LAUD_TEC) DESC");

                //muitas vezes h� colaboradores alocados corretamente,  mas o laudo n�o finalizado impede a gera��o do LTCAT.  Deixar assim, ou n�o checar dataconclusao do pedido ?

                if (listGheEmpregado.Count > 0)
                    laudo = ((GheEmpregado)listGheEmpregado[0]).nID_LAUD_TEC;
                else
                    laudo = LaudoTecnico.GetUltimoLaudo(this.nID_EMPR.Id);
            }
            if (laudo.mirrorOld == null)
                laudo.Find();

            return laudo;
        }

        #endregion

        #region LocalDeTrabalho

        public string GetLocalDeTrabalho(Cliente cliente)
        {
            if (this.mirrorOld == null)
                this.Find();

            if (this.nID_EMPR.mirrorOld == null)
                this.nID_EMPR.Find();

            if (this.nID_EMPR.Id == cliente.Id)
                return "Pr�prio local de trabalho";
            else
                return this.nID_EMPR.NomeAbreviado;
        }

        #endregion
        
        #region EmpregadoFuncao

        public static EmpregadoFuncao GetEmpregadoFuncao(Cliente cliente, Empregado empregado)
        {
            ArrayList list = new ArrayList();

            EmpregadoFuncao emprFunc = new EmpregadoFuncao();

            list = emprFunc.FindMax("hDT_INICIO", "nID_EMPR=" + cliente.Id + " AND nID_EMPREGADO=" + empregado.Id);

            if (list.Count != 0)
                emprFunc = (EmpregadoFuncao)list[0];
            else
            {
                ArrayList preFunc = new ArrayList();

                preFunc = emprFunc.Find("nID_EMPREGADO=" + empregado.Id);

                if (preFunc.Count != 0)
                    emprFunc = (EmpregadoFuncao)preFunc[0];
            }
            return emprFunc;
        }

        public static EmpregadoFuncao GetEmpregadoFuncao(LaudoTecnico laudoTecnico, Empregado empregado)
        {
            ArrayList list = new ArrayList();

            EmpregadoFuncao emprFunc = new EmpregadoFuncao();

            //laudoTecnico.Find();
            //laudoTecnico.nID_EMPR.Find();

            string where = "nID_EMPREGADO=" + empregado.Id
                + " AND nID_EMPR=" + laudoTecnico.nID_EMPR.Id
                + " AND nID_EMPREGADO_FUNCAO IN (SELECT nID_EMPREGADO_FUNCAO FROM tblFUNC_EMPREGADO WHERE nID_LAUD_TEC = " + laudoTecnico.Id + ")";

            list = emprFunc.FindMax("hDT_INICIO", where);

            if (list.Count != 0)
                emprFunc = (EmpregadoFuncao)list[0];

            return emprFunc;
        }

        public static EmpregadoFuncao GetEmpregadoFuncao(Ghe ghe, Empregado empregado)
        {
            ArrayList list = new ArrayList();

            EmpregadoFuncao emprFunc = new EmpregadoFuncao();

            string where = "nID_EMPREGADO=" + empregado.Id
                        + " AND nID_EMPREGADO_FUNCAO IN (SELECT nID_EMPREGADO_FUNCAO FROM tblFUNC_EMPREGADO WHERE nID_FUNC=" + ghe.Id + ")";

            list = emprFunc.FindMax("hDT_INICIO", where);

            if (list.Count != 0)
                emprFunc = (EmpregadoFuncao)list[0];

            return emprFunc;
        }

        public static EmpregadoFuncao GetEmpregadoFuncao(Empregado empregado)
        {
            ArrayList list = new ArrayList();

            EmpregadoFuncao emprFunc = new EmpregadoFuncao();

            list = emprFunc.FindMax("hDT_INICIO", "nID_EMPREGADO=" + empregado.Id);

            if (list.Count != 0)
                emprFunc = (EmpregadoFuncao)list[0];

            return emprFunc;
        }

        public static EmpregadoFuncao GetEmpregadoFuncao(Empregado empregado, Int32 nId_Empr)
        {
            ArrayList list = new ArrayList();

            EmpregadoFuncao emprFunc = new EmpregadoFuncao();

            list = emprFunc.FindMax("hDT_INICIO", "nID_EMPREGADO=" + empregado.Id + " and nId_Empr = " + nId_Empr.ToString());

            if (list.Count != 0)
                emprFunc = (EmpregadoFuncao)list[0];
            else
            {
                list = emprFunc.FindMax("hDT_INICIO", "nID_EMPREGADO=" + empregado.Id );

                if (list.Count != 0)
                    emprFunc = (EmpregadoFuncao)list[0];

            }

            return emprFunc;
        }


        public ArrayList GetArrayEmpregadoFuncao(Empregado empregado)
        {
            ArrayList list = this.Find("nID_EMPREGADO=" + empregado.Id);
            return list;
        }

        #endregion

        #region GheEmpregado

        public Ghe GetGheEmpregado()
        {
            return this.GetGheEmpregado(false);
        }

        public Ghe GetGheEmpregado(bool isPedidoLaudoConcluido)
        {
            LaudoTecnico laudo = LaudoTecnico.GetUltimoLaudo(this.nID_EMPR.Id, isPedidoLaudoConcluido);

            return this.GetGheEmpregado(laudo);
        }

        public Ghe GetGheEmpregado(LaudoTecnico laudo)
        {
            List<GheEmpregado> list = new GheEmpregado().Find<GheEmpregado>("nID_LAUD_TEC=" + laudo.Id 
                                                            + " AND nID_EMPREGADO_FUNCAO=" + this.Id);
            Ghe ghe = new Ghe();

            if (list.Count > 0)
                ghe.Find((list[0]).nID_FUNC.Id);

            return ghe;
        }





        public int GetIdGheEmpregado(LaudoTecnico laudo)
        {
            DataSet ds = new GheEmpregado().GetIdNome("nID_FUNC", 
                                                    "nID_LAUD_TEC=" + laudo.Id
                                                     + " AND nID_EMPREGADO_FUNCAO=" + this.Id);
            int Id = 0;

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                Id =  Convert.ToInt32(ds.Tables[0].Rows[0][1]);

            return Id;
        }

        #endregion

        #region Periodo

        public string GetPeriodo()
        {
            string ret;

            if (this.mirrorOld == null)
                this.Find();

            if (this.hDT_TERMINO == new DateTime())
                ret = this.hDT_INICIO.ToString("dd-MM-yyyy") + " a __-__-____";
            else
            {
                if (this.hDT_TERMINO.Year >= 2023)
                {
                    ret = this.hDT_INICIO.ToString("dd-MM-yyyy")
                        + " a 31-12-2022";
                }
                else
                {
                    ret = this.hDT_INICIO.ToString("dd-MM-yyyy")
                        + " a "
                        + this.hDT_TERMINO.ToString("dd-MM-yyyy");
                }
            }

            return ret;
        }

        public string GetPeriodoAE()
        {
            string ret;

            if (this.mirrorOld == null)
                this.Find();

            if (this.hDT_TERMINO == new DateTime() || this.hDT_TERMINO >= new DateTime(2003, 12, 29))
                ret = this.hDT_INICIO.ToString("dd-MM-yyyy") + " a 29-12-2003";
            else
                ret = this.hDT_INICIO.ToString("dd-MM-yyyy")
                    + " a "
                    + this.hDT_TERMINO.ToString("dd-MM-yyyy");

            return ret;
        }

        public string GetPeriodoParaPPP(DateTime dataInicioPPP)
        {
            StringBuilder str = new StringBuilder();

            if (this.hDT_INICIO > dataInicioPPP)
                str.Append(this.hDT_INICIO.ToString("dd-MM-yyyy"));
            else
                str.Append(dataInicioPPP.ToString("dd-MM-yyyy"));

            str.Append("\n a \n");

            if (this.hDT_TERMINO == new DateTime() || this.hDT_TERMINO == new DateTime(1753, 1, 1))
                str.Append("__-__-____");
            else
                str.Append(this.hDT_TERMINO.ToString("dd-MM-yyyy"));

            return str.ToString();
        }

        private bool IsPertenceAoPeriodo(DateTime Inicio, DateTime Termino)
        {
            bool ret = false;

            if (this.hDT_TERMINO == new DateTime())
                ret = Utility.intersects(Inicio, Termino, this.hDT_INICIO, DateTime.Today);
            else
                ret = Utility.intersects(Inicio, Termino, this.hDT_INICIO, this.hDT_TERMINO);

            return ret;
        }

        public bool IsPertenceAoPeriodo85()
        {
            bool ret = false;

            ret = this.IsPertenceAoPeriodo(new DateTime(2003, 11, 19), DateTime.Today);

            return ret;
        }

        public bool IsPertenceAoPeriodo90()
        {
            bool ret = false;

            ret = this.IsPertenceAoPeriodo(new DateTime(1997, 3, 6), new DateTime(2003, 11, 18));

            return ret;
        }

        public bool IsPertenceAoPeriodo80()
        {
            bool ret = false;

            ret = this.IsPertenceAoPeriodo(new DateTime(), new DateTime(1997, 3, 5));

            return ret;
        }

        #endregion

        #region CBO

        public static string GetNumeroCBO(Empregado empregado)
        {
            string ret;

            EmpregadoFuncao emprFunc = EmpregadoFuncao.GetEmpregadoFuncao(empregado);

            ret = emprFunc.GetNumeroCBO();

            return ret;
        }

        public string GetNumeroCBO()
        {
            string ret = string.Empty;

            if (this.nID_FUNCAO != null && this.nID_FUNCAO.Id != 0)
            {
                if (this.nID_FUNCAO.mirrorOld == null)
                    this.nID_FUNCAO.Find();

                ret = this.nID_FUNCAO.NumeroCBO.ToString();
            }

            return ret;
        }

        #endregion

        #region Jornada

        public string GetJornada()
        {
            string ret = string.Empty;

            if (this.nID_TEMPO_EXP != null && this.nID_TEMPO_EXP.Id != 0)
            {
                if (this.nID_TEMPO_EXP.mirrorOld == null)
                    this.nID_TEMPO_EXP.Find();

                ret = this.nID_TEMPO_EXP.tHORA_EXTENSO_SEMANAL;
            }

            return ret;
        }
        
        public static string GetJornada(Empregado empregado)
        {
            string ret = string.Empty;

            EmpregadoFuncao emprFunc = EmpregadoFuncao.GetEmpregadoFuncao(empregado);

            if (emprFunc.nID_TEMPO_EXP != null && emprFunc.nID_TEMPO_EXP.Id != 0)
            {
                emprFunc.nID_TEMPO_EXP.Find();

                ret = emprFunc.nID_TEMPO_EXP.tHORA;
            }

            return ret;
        }

        #endregion

        #region Cargo

        public string GetCargoEmpregado()
        {
            string ret = string.Empty;

            Cargo xCargo;
            int zCargo;

            xCargo = this.nID_CARGO;
            zCargo = this.nID_CARGO.Id;

                   

            //if (this.nID_CARGO != null && this.nID_CARGO.Id != 0)
            if ( xCargo != null && zCargo != 0 )
            {
                if (this.mirrorOld == null)
                {
                    this.nID_CARGO.Find();
                    ret = this.nID_CARGO.tNO_CARGO;
                }
                else
                {
                    ret = xCargo.ToString();
                }
            }

            return ret;
        }
        #endregion

        #region Funcao

        public string GetNomeFuncao()
        {
            string ret = string.Empty;

            if (this.nID_FUNCAO != null && this.nID_FUNCAO.Id != 0)
            {
                if (this.nID_FUNCAO.mirrorOld == null)
                    this.nID_FUNCAO.Find();

                ret = this.nID_FUNCAO.NomeFuncao;
            }

            return ret;
        }

        public string GetRequisitoFuncao()
        {
            string ret = string.Empty;

            if (this.nID_FUNCAO != null && this.nID_FUNCAO.Id != 0)
            {
                if (this.nID_FUNCAO.mirrorOld == null)
                    this.nID_FUNCAO.Find();

                if (this.nID_FUNCAO.RequisitoFuncao != string.Empty)
                    ret = this.nID_FUNCAO.RequisitoFuncao;
                else
                    ret = this.nID_FUNCAO.GetDefaultRequisitoFuncao();
            }

            return ret;
        }

        public string GetDescricaoFuncao()
        {
            string ret = string.Empty;

            if (this.nID_FUNCAO != null && this.nID_FUNCAO.Id != 0 && this.tDS_FUNCAO == string.Empty)
            {
                if (this.nID_FUNCAO.mirrorOld == null)
                    this.nID_FUNCAO.Find();

                ret = this.nID_FUNCAO.DescricaoFuncao;
            }
            else
                ret = this.tDS_FUNCAO;

            return ret;
        }

        #endregion

        #region Setor

        public string GetNomeSetor()
        {
            string ret = string.Empty;

            if (this.nID_SETOR != null && this.nID_SETOR.Id != 0)
            {
                if (this.nID_SETOR.mirrorOld == null)
                    this.nID_SETOR.Find();

                ret = this.nID_SETOR.tNO_STR_EMPR;
            }

            return ret;
        }

        public string GetDescricaoLocalTrabalho()
        {
            string ret = string.Empty;

            if (this.nID_SETOR != null && this.nID_SETOR.Id != 0)
            {
                if (this.nID_SETOR.mirrorOld == null)
                    this.nID_SETOR.Find();

                ret = this.nID_SETOR.mDS_STR_EMPR;
            }

            return ret;
        }

        #endregion

        #region Conclusao Previdenciario

        public string GetConclusaoPrevidenciario()
        {
            string ret = string.Empty;

            if (this.nID_SETOR.ToString().ToLower() == "produ��o"
                && this.hDT_INICIO < new DateTime(2003, 11, 19)
                && (this.nIND_GFIP == (int)CodigoGFIP.Cod00
                || this.nIND_GFIP == (int)CodigoGFIP.Cod01
                || this.nIND_GFIP == (int)CodigoGFIP.Cod05))
                ret = "Aus�ncia de exposi��o e com nocividade aos agentes nocivos qu�micos e f�sicos reconhecidos e avaliados nesta demonstra��o ambiental. O agente ru�do n�o ultrapassou o limite de toler�ncia de 90 dB(A) at� 18/11/2003, quando foi alterado pelo Decreto n� 4.882/03 para 85 dB(A). Ap�s, a exposi��o ao ru�do continuou com aus�ncia de nocividade pela utiliza��o de equipamentos de prote��o individual que neutralizaram e reduzaram seus efeitos ao limite legal de toler�ncia (85 dB(A)), tornando-a n�o prejudicial � sa�de ou � integridade f�sica. Concluo, portanto, pela n�o caracteriza��o da nocividade que integra o n�cleo da hip�tese de incid�ncia do fato gerador da contribui��o adicional prevista no art. 57, � 6�, Lei n� 8.213/91, face � an�lise t�cnica procedida na exposi��o laboral aos riscos ambientais arrolados exclusivamente no Anexo IV,  do Decreto n� 3.048/99. "
                      + "\n\nDiante do exposto, com fundamento no art. 406, da IN/INSS n� 100/03 (per�odo de vig�ncia de 18/12/2003 a 13/07/2005), no art. 383, � 2�, da IN/MPS SRP n� 3/05, que a revogou, atesto, atrav�s desta demonstra��o ambiental, a n�o ocorr�ncia de condi��es especiais de trabalho que gerem direito � aposentadoria especial, haja vista a ado��o de medidas eficazes de prote��o individual que neutralizaram e/ou reduziram o grau de exposi��o ao ru�do dos trabalhadores a n�veis legais de toler�ncia de forma a afastar a concess�o de aposentadoria especial. ";
            else if (this.nID_SETOR.ToString().ToLower() == "produ��o"
                && this.hDT_INICIO >= new DateTime(2003, 11, 19)
                && (this.nIND_GFIP == (int)CodigoGFIP.Cod00
                || this.nIND_GFIP == (int)CodigoGFIP.Cod01
                || this.nIND_GFIP == (int)CodigoGFIP.Cod05))
                ret = "A exposi��o aos agentes nocivos e o exerc�cio da atividade ocorreram de modo habitual e permanente, de forma n�o ocasional e n�o intermitente sendo que os servi�os prestados foram exercidos exclusivamente durante a jornada integral de trabalho. Em conseq��ncia da avalia��o realizada, conclui-se que a efetiva exposi��o aos agentes nocivos avaliados, foi neutralizada pelo uso de Equipamentos de Prote��o Individual, com fundamento no art. 191, II, da CLT, e na NR 6, item 6.6.1, motivo pelo qual, a exposi��o se apresenta com aus�ncia de nocividade, n�o sendo prejudicial � sa�de ou � integridade f�sica, por sua natureza, intensidade, condi��es e m�todos de trabalho, bem como ao tempo de exposi��o aos seus efeitos.  "
                    + "\n\nDiante do exposto, com fundamento no art. 406, da IN/INSS n� 100/03 (per�odo de vig�ncia de 18/12/2003 a 13/07/2005), no art. 383, � 2�, da IN/MPS SRP n� 3/05, que a revogou, atesto, atrav�s desta demonstra��o ambiental, a n�o ocorr�ncia de condi��es especiais de trabalho que gerem direito � aposentadoria especial, haja vista a ado��o de medidas eficazes de prote��o individual que neutralizaram e/ou reduziram o grau de exposi��o ao ru�do dos trabalhadores a n�veis legais de toler�ncia de forma a afastar a concess�o de aposentadoria especial. ";
            else if (this.nID_SETOR.ToString().ToLower() == "produ��o"
                    && this.nIND_GFIP == (int)CodigoGFIP.Cod04)
                ret = "A exposi��o aos agentes nocivos e o exerc�cio da atividade ocorreram de modo habitual e permanente, de forma n�o ocasional e n�o intermitente sendo que os servi�os prestados foram exercidos exclusivamente durante a jornada integral de trabalho. Em conseq��ncia da avalia��o t�cnica realizada, haja vista o trabalho penoso executado durante todo o per�odo laboral, n�o se pode afirmar com rigor cient�fico, com fundamento nos conhecimentos t�cnicos vigentes, que o uso dos EPI mencionados atenuou, reduziu, neutralizou ou conferiu total prote��o em rela��o � nocividade do agente ru�do de modo a evitar a ocorr�ncia de exposi��es potencialmente prejudiciais � sa�de ou a integridade f�sica do trabalhador."
                    + "\n\nDiante do exposto, com fundamento no art. 406, da IN/INSS n� 100/03 (per�odo de vig�ncia de 18/12/2003 a 13/07/2005), no art. 383, � 2�, da IN/MPS SRP n� 3/05, que a revogou, atesto, atrav�s desta demonstra��o ambiental, a ocorr�ncia de condi��es especiais de trabalho que geram direito � aposentadoria especial, motivo pelo qual, a empresa declarou em GFIP, no campo ocorr�ncia, o c�digo 4 � exposi��o a agente nocivo (aposentadoria especial aos 25 anos de trabalho). ";
            else if (this.nID_SETOR.ToString().ToLower() == "administra��o"
                && (this.nIND_GFIP == (int)CodigoGFIP.Cod00
                    || this.nIND_GFIP == (int)CodigoGFIP.Cod01
                    || this.nIND_GFIP == (int)CodigoGFIP.Cod05))
                ret = "Diante da an�lise t�cnica procecida, conclui-se que a efetiva exposi��o ao agente nocivo, ou associa��o de agentes, n�o � prejudicial � sa�de ou � integridade f�sica do trabalhador, haja vista que a atividade realizada, exclusivamente administrativa, se apresenta com aus�ncia de exposi��o a riscos ambientais com nocividade. Concluo, portanto, pela n�o caracteriza��o da perman�ncia e nocividade que se constituem no n�cleo da hip�tese de incid�ncia do fato gerador da contribui��o adicional prevista no art. 57, � 6�, Lei n� 8.213/91, face � an�lise t�cnica procedida na exposi��o laboral aos riscos ambientais arrolados exclusivamente no Anexo IV, do Decreto n� 3.048/99.  "; 
            else
                ret = "-";

            return ret;
        }


        public string GetConclusaoInsalubridade()
        {
            string ret = string.Empty;

            ret = "Analisando o trabalho atual desenvolvido pelo empregado em quest�o, concluo que sua atividade � salubre, pois a exposi��o aos agentes nocivos e o exerc�cio da atividade ocorreram de modo habitual e permanente, de forma n�o ocasional e n�o intermitente sendo que os servi�os prestados foram exercidos exclusivamente durante a jornada integral de trabalho. Em conseq��ncia da avalia��o realizada, conclui-se que a efetiva exposi��o aos agentes nocivos avaliados, foi neutralizada pelo uso de Equipamentos de Prote��o Individual, com fundamento no art. 191, II, da CLT, e na NR 6, item 6.6.1 e 6.7.1, motivo pelo qual, a exposi��o se apresenta com aus�ncia de nocividade, n�o sendo prejudicial � sa�de ou � integridade f�sica, por sua natureza, intensidade, condi��es e m�todos de trabalho, bem como ao tempo de exposi��o aos seus efeitos.  "
                + "\nDiante do exposto, com fundamento no art. 406, da IN/INSS n� 100/03 (per�odo de vig�ncia de 18/12/2003 a 13/07/2005), no art. 383, � 2�, da IN/MPS SRP n� 3/05, que a revogou, atesto, atrav�s desta demonstra��o ambiental, a n�o ocorr�ncia de condi��es especiais de trabalho que gerem direito � aposentadoria especial, haja vista a ado��o de medidas eficazes de prote��o individual que neutralizaram e/ou reduziram o grau de exposi��o ao ru�do dos trabalhadores a n�veis legais de toler�ncia de forma a afastar a concess�o de aposentadoria especial.  O exerc�cio da atividade analisada n�o se enquadra no art.192 da consolida��o das leis trabalhistas, reda��o dada pelos artigos inclusos na Se��o XIII Das Atividades Insalubre ou Perigosas, da CLT Consolida��o das Leis do Trabalho, que determina a percep��o de adicional de insalubridade.";

            
            return ret;
        }
        

        
        #endregion 

        #region ListaLocaisDeTrabalho

        #region GetListaLocaisDeTrabalho

        public ArrayList GetListaLocaisDeTrabalho(string Pesquisar)
        {
            if (this.nID_EMPREGADO.nID_EMPR == null)
                this.nID_EMPREGADO.Find();

            if (this.nID_EMPR.mirrorOld == null)
                this.nID_EMPR.Find();

            return GetListaLocaisDeTrabalho(this.nID_EMPREGADO.nID_EMPR, this.nID_EMPR, Pesquisar);
        }

        public static ArrayList GetListaLocaisDeTrabalho(Cliente clienteEmpregado, Cliente clienteClassFunc, string Pesquisar)
        {
            if (clienteEmpregado.mirrorOld == null)
                clienteEmpregado.Find();

            if (clienteEmpregado.IdGrupoEmpresa.mirrorOld == null)
                clienteEmpregado.IdGrupoEmpresa.Find();

            StringBuilder str = new StringBuilder();

            str.Append("IdCliente<>" + clienteClassFunc.Id);

            if (clienteEmpregado.AtivarLocalDeTrabalho
                && clienteEmpregado.IdJuridicaPapel.Id != (int)IndJuridicaPapel.TerceiroAutonomo)
            {
                str.Append(" AND IdJuridicaPai=" + clienteEmpregado.Id);

                str.Append(" AND IdJuridicaPapel IN (SELECT IdJuridicaPapel FROM JuridicaPapel WHERE IsLocalTrabalho=1)");

                if (Pesquisar != string.Empty)
                    str.Append(" AND NomeAbreviado Like '%" + Pesquisar + "%'");
            }
            else if (clienteEmpregado.IdJuridicaPapel.Id == (int)IndJuridicaPapel.TerceiroAutonomo)
            {
                str.Append(" AND IdJuridicaPai=" + clienteEmpregado.IdJuridicaPai.Id);

                if (Pesquisar != string.Empty)
                    str.Append(" AND NomeAbreviado Like '%" + Pesquisar + "%'");
            }
            else if (clienteEmpregado.IdGrupoEmpresa.Id == 0)
                str.Append(" AND IdCliente=" + clienteEmpregado.Id);
            else
                str.Append(" AND IdGrupoEmpresa=" + clienteEmpregado.IdGrupoEmpresa.Id);

            ArrayList list = new Cliente().Find(str.ToString());

            if (clienteClassFunc.mirrorOld == null)
                clienteClassFunc.Find();

            list.Add(clienteClassFunc);

            if (clienteEmpregado.IdJuridicaPapel.Id == (int)IndJuridicaPapel.TerceiroAutonomo)
            {
                Cliente clientePai = new Cliente();
                clientePai.Find(clienteEmpregado.IdJuridicaPai.Id);

                list.Add(clientePai);
            }

            list.Sort();

            return list;
        }

        #endregion

        #region FindListaLocaisDeTrabalho

        public List<Cliente> FindListaLocaisDeTrabalho(string Pesquisar)
        {
            if (this.nID_EMPREGADO.mirrorOld == null)
                this.nID_EMPREGADO.Find();

            if (this.nID_EMPREGADO.nID_EMPR.mirrorOld == null)
                this.nID_EMPREGADO.nID_EMPR.Find();

            if (this.nID_EMPREGADO.nID_EMPR.IdGrupoEmpresa.mirrorOld == null)
                this.nID_EMPREGADO.nID_EMPR.IdGrupoEmpresa.Find();

            return FindListaLocaisDeTrabalho(this.nID_EMPREGADO.nID_EMPR, Pesquisar);
        }

        public static List<Cliente> FindListaLocaisDeTrabalho(Cliente clienteEmpregado, 
                                                                string Pesquisar)
        {
            StringBuilder str = new StringBuilder();

            str.Append("((IdJuridicaPai IN (SELECT IdJuridica FROM Juridica WHERE IdGrupoEmpresa=" + clienteEmpregado.IdGrupoEmpresa.Id);
            str.Append(" AND IdJuridicaPapel=" + (int)IndJuridicaPapel.Cliente + " AND IsInativo=0)");
            str.Append(" AND IdJuridicaPapel IN (SELECT IdJuridicaPapel FROM JuridicaPapel WHERE IsLocalTrabalho=1 OR IdJuridicaPapel=" + (int)IndJuridicaPapel.AposentEspecial + "))");
            str.Append(" OR (IdGrupoEmpresa=" + clienteEmpregado.IdGrupoEmpresa.Id 
                            + " AND IdJuridicaPapel=" + (int)IndJuridicaPapel.Cliente + "))");

            str.Append(" AND IsInativo=0 AND NomeCodigo IS NOT NULL AND NomeCodigo<>''");

            if (!Pesquisar.Trim().Equals(string.Empty))
            {
                str.Append(" AND (UPPER(NomeCompleto) COLLATE SQL_Latin1_General_Cp1251_CS_AS LIKE '%" + Pesquisar.ToUpper() + "%'");
                str.Append(" OR UPPER(NomeAbreviado) COLLATE SQL_Latin1_General_Cp1251_CS_AS LIKE '%" + Pesquisar.ToUpper() + "%')");
            }

            str.Append(" ORDER BY NomeAbreviado");

            return new Cliente().Find<Cliente>(str.ToString());
        }
        #endregion

        #endregion

        #region ListaEpi

        public static ArrayList GetListaEpi(Empregado empregado)
        {
            ArrayList listEpi = new ArrayList();
            ArrayList listEpiReturn = new ArrayList();

            string Verifica = string.Empty;

            try
            {
                EmpregadoFuncao empregadoFuncao = EmpregadoFuncao.GetEmpregadoFuncao(empregado);

                Ghe ghe = empregadoFuncao.GetGheEmpregado();

                if (ghe.Id != 0)
                {
                    ArrayList listGheEpiExistente = new GheEpiExistente().Find("nID_FUNC=" + ghe.Id);
                    ArrayList listEpiExistente = new EpiExistente().FindIn(new PPRA().GetType(), "", true, "nID_FUNC=" + ghe.Id);

                    foreach (GheEpiExistente gheEpiExte in listGheEpiExistente)
                    {
                        gheEpiExte.nID_EPI.Find();
                        listEpi.Add(gheEpiExte.nID_EPI);
                    }

                    foreach (EpiExistente epiExte in listEpiExistente)
                    {
                        epiExte.nID_EPI.Find();
                        listEpi.Add(epiExte.nID_EPI);
                    }

                    listEpi.Sort();

                    foreach (Epi epi in listEpi)
                    {
                        if (Verifica != epi.ToString())
                        {
                            Verifica = epi.ToString();
                            listEpiReturn.Add(epi);
                        }
                    }

                    listEpiReturn.Sort();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return listEpiReturn;
        }

        public static ArrayList GetListaEpiRiscos(Empregado empregado)
        {
            ArrayList listEpi = new ArrayList();
            ArrayList listEpiReturn = new ArrayList();

            string Verifica = string.Empty;

            EmpregadoFuncao empregadoFuncao = EmpregadoFuncao.GetEmpregadoFuncao(empregado);

            Ghe ghe = empregadoFuncao.GetGheEmpregado();

            if (ghe.Id != 0)
            {
                ArrayList listEpiExistente = new EpiExistente().FindIn(new PPRA().GetType(), "", true, "nID_FUNC=" + ghe.Id);

                foreach (EpiExistente epiExte in listEpiExistente)
                {
                    epiExte.nID_EPI.Find();
                    listEpi.Add(epiExte.nID_EPI);
                }

                listEpi.Sort();

                foreach (Epi epi in listEpi)
                {
                    if (Verifica != epi.ToString())
                    {
                        Verifica = epi.ToString();
                        listEpiReturn.Add(epi);
                    }
                }
                listEpiReturn.Sort();
            }
            return listEpiReturn;
        }
        #endregion

        #region EnviarConfirmacaoEmail

        public void EnviarConfirmacaoEmail(Usuario usuario)
        {

        }
        
        #endregion

        #endregion
    }
}
