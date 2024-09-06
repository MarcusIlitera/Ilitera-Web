using System;
using System.Data;
using System.Text;
using System.IO;
using System.Collections;
using System.Collections.Generic;

using Ilitera.Data;
using Ilitera.Common;

namespace Ilitera.Opsa.Data
{

    [Database("sied_novo", "tblLAUDO_TEC", "nID_LAUD_TEC")]
    public class LaudoTecnico : Ilitera.Data.Table, Ilitera.Common.IFoto
    {
        private int _nID_LAUD_TEC;
        private Cliente _nID_EMPR;
        private DateTime _hDT_LAUDO = DateTime.Today;
        private Pedido _nID_PEDIDO;
        private string _tNO_DIR_FTO_FUNC = string.Empty;
        private string _tNO_ARQ_INIC_FTO_FUNC = string.Empty;
        private string _tNO_ARQ_TERM_FTO_FUNC = string.Empty;
        private string _tNO_ARQ_EXT_FTO_FUNC = string.Empty;
        private int _tNO_ARQ_TAM_FTO_FUNC = 3;
        private string _ConclusaoPPRA = string.Empty;
        private string _TituloTecSegTrabPPRA = string.Empty;
        private string _NumTecSegTrabPPRA = string.Empty;
        private string _TituloEngSegTrabPPRA = string.Empty;
        private string _NumEngSegTrabPPRA = string.Empty;
        private string _AcompanhantePPRA = string.Empty;
        private string _TituloAcompanhantePPRA = string.Empty;
        private DeclaracaoPadrao _nID_DECL_PDR;
        private string _mDS_DECL_EMPR = string.Empty;
        private string _mDS_DECL_EMPG = string.Empty;
        private Prestador _PrestadorID;
        private Prestador _PrestadorAE;
        private Prestador _ResponsavelLegal;
        private Prestador _nID_COMISSAO_1;
        private Prestador _nID_COMISSAO_2;
        private Prestador _nID_COMISSAO_3;
        private Prestador _nID_TEC_CMPO;
        private bool _bAE;
        private bool _bPADRAO;
        private bool _NR20;
        private Prestador _nID_IMPLEMENTACAO;
        private Prestador _PrestadorID2;
        private string _Historico_Versao;
        private bool _Profissionais_Ilitera;
        private int _nID_LAUD_TEC_Retroagido;
        private bool _Ergonomia_PGR;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public LaudoTecnico()
        {

        }

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public LaudoTecnico(int IdLaudoTecnico)
        {
            this.Find(IdLaudoTecnico);
        }
        public override int Id
        {
            get { return _nID_LAUD_TEC; }
            set { _nID_LAUD_TEC = value; }
        }
        public string TituloEngSegTrabPPRA
        {
            get { return _TituloEngSegTrabPPRA; }
            set { _TituloEngSegTrabPPRA = value; }
        }
        public string NumEngSegTrabPPRA
        {
            get { return _NumEngSegTrabPPRA; }
            set { _NumEngSegTrabPPRA = value; }
        }
        public string TituloTecSegTrabPPRA
        {
            get { return _TituloTecSegTrabPPRA; }
            set { _TituloTecSegTrabPPRA = value; }
        }
        public string NumTecSegTrabPPRA
        {
            get { return _NumTecSegTrabPPRA; }
            set { _NumTecSegTrabPPRA = value; }
        }
        public string ConclusaoPPRA
        {
            get { return _ConclusaoPPRA; }
            set { _ConclusaoPPRA = value; }
        }
        [Obrigatorio(true, "O técnico de campo é obrigatório")]
        public Prestador nID_TEC_CMPO
        {
            get { return _nID_TEC_CMPO; }
            set { _nID_TEC_CMPO = value; }
        }
        public bool bAE
        {
            get { return _bAE; }
            set { _bAE = value; }
        }
        public bool bPADRAO
        {
            get { return _bPADRAO; }
            set { _bPADRAO = value; }
        }
        public Cliente nID_EMPR
        {
            get { return _nID_EMPR; }
            set { _nID_EMPR = value; }
        }
        public DateTime hDT_LAUDO
        {
            get { return _hDT_LAUDO; }
            set { _hDT_LAUDO = value; }
        }
        public Pedido nID_PEDIDO
        {
            get { return _nID_PEDIDO; }
            set { _nID_PEDIDO = value; }
        }
        public Prestador PrestadorID
        {
            get { return _PrestadorID; }
            set { _PrestadorID = value; }
        }
        public Prestador PrestadorAE
        {
            get { return _PrestadorAE; }
            set { _PrestadorAE = value; }
        }
        public Prestador ResponsavelLegal
        {
            get { return _ResponsavelLegal; }
            set { _ResponsavelLegal = value; }
        }
        public string tNO_DIR_FTO_FUNC
        {
            get { return _tNO_DIR_FTO_FUNC; }
            set { _tNO_DIR_FTO_FUNC = value; }
        }
        public string mDS_DECL_EMPR
        {
            get { return _mDS_DECL_EMPR; }
            set { _mDS_DECL_EMPR = value; }
        }
        public string mDS_DECL_EMPG
        {
            get { return _mDS_DECL_EMPG; }
            set { _mDS_DECL_EMPG = value; }
        }
        public string AcompanhantePPRA
        {
            get { return _AcompanhantePPRA; }
            set { _AcompanhantePPRA = value; }
        }
        public string TituloAcompanhantePPRA
        {
            get { return _TituloAcompanhantePPRA; }
            set { _TituloAcompanhantePPRA = value; }
        }
        public DeclaracaoPadrao nID_DECL_PDR
        {
            get { return _nID_DECL_PDR; }
            set { _nID_DECL_PDR = value; }
        }
        public string tNO_ARQ_INIC_FTO_FUNC
        {
            get { return _tNO_ARQ_INIC_FTO_FUNC; }
            set { _tNO_ARQ_INIC_FTO_FUNC = value; }
        }
        public string tNO_ARQ_TERM_FTO_FUNC
        {
            get { return _tNO_ARQ_TERM_FTO_FUNC; }
            set { _tNO_ARQ_TERM_FTO_FUNC = value; }
        }
        public string tNO_ARQ_EXT_FTO_FUNC
        {
            get { return _tNO_ARQ_EXT_FTO_FUNC; }
            set { _tNO_ARQ_EXT_FTO_FUNC = value; }
        }
        public int tNO_ARQ_TAM_FTO_FUNC
        {
            get { return _tNO_ARQ_TAM_FTO_FUNC; }
            set { _tNO_ARQ_TAM_FTO_FUNC = value; }
        }

        public Prestador nID_COMISSAO_1
        {
            get { return _nID_COMISSAO_1; }
            set { _nID_COMISSAO_1 = value; }
        }
        public Prestador nID_COMISSAO_2
        {
            get { return _nID_COMISSAO_2; }
            set { _nID_COMISSAO_2 = value; }
        }
        public Prestador nID_COMISSAO_3
        {
            get { return _nID_COMISSAO_3; }
            set { _nID_COMISSAO_3 = value; }
        }
        public bool NR20
        {
            get { return _NR20; }
            set { _NR20 = value; }
        }
        public Prestador nID_IMPLEMENTACAO
        {
            get { return _nID_IMPLEMENTACAO; }
            set { _nID_IMPLEMENTACAO = value; }
        }
        public Prestador PrestadorID2
        {
            get { return _PrestadorID2; }
            set { _PrestadorID2 = value; }
        }
        public string Historico_Versao
        {
            get { return _Historico_Versao; }
            set { _Historico_Versao = value; }
        }

        public bool Profissionais_Ilitera
        {
            get { return _Profissionais_Ilitera; }
            set { _Profissionais_Ilitera = value; }
        }
        public Int32 nID_LAUD_TEC_Retroagido
        {
            get { return _nID_LAUD_TEC_Retroagido; }
            set { _nID_LAUD_TEC_Retroagido = value; }
        }
        public bool Ergonomia_PGR
        {
            get { return _Ergonomia_PGR; }
            set { _Ergonomia_PGR = value; }
        }


        [Persist(false)]
        public string FotoDiretorio
        {
            get 
            {
                if (this.nID_EMPR.mirrorOld == null)
                    this.nID_EMPR.Find();

                string root = Path.Combine(this.nID_EMPR.GetFotoDiretorioPadrao(), "Paradigmas");

                if (_tNO_DIR_FTO_FUNC.IndexOf(root) != 1)
                    return root + _tNO_DIR_FTO_FUNC;
                else
                    return _tNO_DIR_FTO_FUNC; 
            }
        }
        [Persist(false)]
        public string FotoExtensao
        {
            get { return _tNO_ARQ_EXT_FTO_FUNC; }
        }
        [Persist(false)]
        public string FotoInicio
        {
            get { return _tNO_ARQ_INIC_FTO_FUNC; }
        }
        [Persist(false)]
        public byte FotoTamanho
        {
            get { return Convert.ToByte(_tNO_ARQ_TAM_FTO_FUNC); }
        }
        [Persist(false)]
        public string FotoTermino
        {
            get { return tNO_ARQ_TERM_FTO_FUNC; }
        }
        public override string ToString()
        {
            if (this.mirrorOld == null)
                this.Find();

            return this.hDT_LAUDO.ToString("dd-MM-yyyy");
        }

        public override void Validate()
        {
            if (this.Id == 0
                || (this.mDS_DECL_EMPR == string.Empty
                && this.mDS_DECL_EMPG == string.Empty))
            {
                if (this.nID_DECL_PDR.Id != 0)
                {
                    this.nID_DECL_PDR.Find();
                    this.mDS_DECL_EMPR = this.nID_DECL_PDR.mDECL_EMPS;
                    this.mDS_DECL_EMPG = this.nID_DECL_PDR.mDECL_EMPG;
                }
            }

            this.bAE = (this.nID_PEDIDO.Id == 0);

            base.Validate();
        }

        public override void Delete()
        {
            if (this.nID_PEDIDO.Id != 0)
            {
                if (this.nID_PEDIDO.mirrorOld == null)
                    this.nID_PEDIDO.Find();

                if (this.nID_PEDIDO.DataCancelamento == new DateTime()
                    && this.nID_PEDIDO.DataConclusao != new DateTime())
                    throw new Exception("O laudo está com o pedido concluído.\n\nNão pode ser excluído!");

                FaturamentoPedido faturamentoPedido = new FaturamentoPedido();
                faturamentoPedido.Find("IdPedido=" + this.nID_PEDIDO.Id);

                if (faturamentoPedido.Id != 0)
                    throw new Exception("Pedido já faturado o laudo não pode ser excluído!");
            }

            Ilitera.Common.Usuario.Permissao(this.GetType());

            new CronogramaPPRA().Delete("IdLaudoTecnico=" + this.Id);

            new CronogramaErgonomia().Delete("nID_LAUD_TEC=" + this.Id);

            new DocumentoAlteracao().Delete("IdLaudoTecnico=" + this.Id);

            base.Delete();
        }

        public static LaudoTecnico GetUltimoLaudo(int IdCliente)
        {
            return GetUltimoLaudo(IdCliente, true);
        }

        public static LaudoTecnico GetUltimoLaudo(int IdCliente, bool IsPedidoConcluido)
        {
            StringBuilder str = new StringBuilder();

            str.Append("nID_EMPR =" + IdCliente);
            str.Append(" AND hDT_LAUDO<='" + DateTime.Today.ToString("yyyy-MM-dd") + "'");

            if (IsPedidoConcluido)
                str.Append(" AND (nID_PEDIDO IN (SELECT IdPedido FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.Pedido WHERE DataConclusao IS NOT NULL) OR bAE =1)");

            LaudoTecnico laudo = new LaudoTecnico();

            ArrayList listlaudo = new LaudoTecnico().FindMax("hDT_LAUDO", str.ToString());

            if (listlaudo.Count == 0)
                return laudo;
            else
                return (LaudoTecnico)listlaudo[0];
        }

        public DateTime GetDataTermino()
        {
            string criteria = "nID_EMPR =" + this.nID_EMPR.Id
                              + " ORDER BY hDT_LAUDO";

            List<LaudoTecnico> laudos = new LaudoTecnico().Find<LaudoTecnico>(criteria);

            return GetDataTermino(laudos);
        }

        public DateTime GetDataTermino(List<LaudoTecnico> laudos)
        {
            DateTime ret = new DateTime();

            bool bVal = false;

            foreach (LaudoTecnico laudo in laudos)
            {
                if (this.Id == laudo.Id)
                {
                    bVal = true;
                    continue;
                }
                if (bVal)
                {
                    ret = laudo.hDT_LAUDO.AddDays(-1);
                    break;
                }
            }
            if (ret == new DateTime())
                ret = this.hDT_LAUDO.AddYears(1).AddDays(-1);

            return ret;
        }

        public LaudoTecnico GetLaudoAtual(int idEmpregadoFuncao)
        {
            LaudoTecnico laudo = new LaudoTecnico();

            ArrayList laudoatual = new LaudoTecnico().Find("nID_LAUD_TEC IN (SELECT nID_LAUD_TEC FROM tblFUNC_EMPREGADO WHERE nID_EMPREGADO_FUNCAO=" + idEmpregadoFuncao + ")"
                + " AND (nID_PEDIDO IN (SELECT IdPedido FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.Pedido WHERE DataConclusao IS NOT NULL) OR bAE =1)"
                + " ORDER BY hDT_LAUDO DESC");

            if (laudoatual.Count == 0)
                return laudo;
            else
                return (LaudoTecnico)laudoatual[0];
        }

        public LaudoTecnico GetPenultimoLaudo(int idEmpresa)
        {
            if (idEmpresa == 1085) //Federal Mogul
            {
                //improvisado
                LaudoTecnico laudo = new LaudoTecnico();
                ArrayList listlaudo = new LaudoTecnico().Find("nID_EMPR =" + idEmpresa + " ORDER BY hDT_LAUDO DESC");
                if (listlaudo.Count == 0)
                    return laudo;
                else
                {
                    if (listlaudo.Count > 1)
                    {
                        if (((LaudoTecnico)listlaudo[0])._hDT_LAUDO.Year == 2003)
                            return (LaudoTecnico)listlaudo[0];
                        else
                            return (LaudoTecnico)listlaudo[1];
                    }
                    else
                        return (LaudoTecnico)listlaudo[0];
                }
            }
            else
                return GetUltimoLaudo(idEmpresa);
        }

        public bool IsAgentesQuimicosSemAvaliacaoQuantitativa()
        {
            string where = "nID_LAUD_TEC=" + this.Id
                            + " AND nID_RSC IN(" + (int)Riscos.AgentesQuimicos
                                                + ", " + (int)Riscos.AgentesQuimicosB
                                                + ", " + (int)Riscos.AgentesQuimicosC
                                                + ", " + (int)Riscos.AgentesQuimicosD
                                                + ", " + (int)Riscos.AsbestosPoeirasMinerais
                                                + ", " + (int)Riscos.ACGIH + ")"
                            + " AND IsNull(tVL_MED, 0) = 0";

            int count = new PPRA().ExecuteCount(where);

            return count > 0;
        }

        public bool IsCalorEmOutroLocalDeDescanso()
        {
            string where = "nID_LAUD_TEC=" + this.Id
                          + " AND nID_RSC = " + (int)Riscos.Calor
                          + " AND bDESC = 1";

            int count = new PPRA().ExecuteCount(where);

            return count > 0;
        }

        public ArrayList FindEPI()
        {
            DataSet ds = new Ghe().Get("nID_LAUD_TEC =" + this.Id);
            ArrayList epitotal = new ArrayList();

            foreach (DataRow ghe in ds.Tables[0].Select())
            {
                ArrayList listGheEpiExistente = new GheEpiExistente().Find("nID_FUNC=" + ghe["nID_FUNC"]);
                ArrayList listRiscoEpiExistente = new EpiExistente().FindIn(new PPRA().GetType(), "", true, "nID_FUNC=" + ghe["nID_FUNC"]);

                foreach (GheEpiExistente gheepi in listGheEpiExistente)
                {
                    if (!epitotal.Contains(gheepi.nID_EPI))
                        epitotal.Add(gheepi.nID_EPI);
                }

                foreach (EpiExistente riscoepi in listRiscoEpiExistente)
                {
                    if (!epitotal.Contains(riscoepi.nID_EPI))
                        epitotal.Add(riscoepi.nID_EPI);
                }
            }

            epitotal.Sort();

            return epitotal;
        }

        public DataTable GetEPI()
        {
            DataSet ds = new Ghe().Get("nID_LAUD_TEC =" + this.Id);
            
            DataTable table = new DataTable();
            table.Columns.Add("Id", typeof(string));
            table.Columns.Add("Descricao", typeof(string));

            DataTable tableOrdered = new DataTable();
            tableOrdered.Columns.Add("Id", typeof(string));
            tableOrdered.Columns.Add("Descricao", typeof(string));

            DataRow newRow, newRowOrdered;

            foreach (DataRow ghe in ds.Tables[0].Select())
            {
                ArrayList listGheEpiExistente = new GheEpiExistente().Find("nID_FUNC=" + ghe["nID_FUNC"]);
                ArrayList listEpiExistente = new EpiExistente().FindIn(new PPRA().GetType(), "", true, "nID_FUNC=" + ghe["nID_FUNC"]);

                foreach (GheEpiExistente gheEpiExte in listGheEpiExistente)
                {
                    gheEpiExte.nID_EPI.Find();

                    newRow = table.NewRow();
                    newRow["Id"] = gheEpiExte.nID_EPI.Id.ToString();

                    //if (gheEpiExte.tDS_CONDICAO.Equals(string.Empty))
                        newRow["Descricao"] = gheEpiExte.nID_EPI.ToString();
                    //else
                    //    newRow["Descricao"] = gheEpiExte.nID_EPI.ToString() + " (" + gheEpiExte.tDS_CONDICAO + ")";

                    table.Rows.Add(newRow);
                }

                foreach (EpiExistente epiExte in listEpiExistente)
                {
                    epiExte.nID_EPI.Find();

                    newRow = table.NewRow();
                    newRow["Id"] = epiExte.nID_EPI.Id.ToString();

                    //if (epiExte.tDS_CONDICAO.Equals(string.Empty))
                        newRow["Descricao"] = epiExte.nID_EPI.ToString();
                    //else
                    //    newRow["Descricao"] = epiExte.nID_EPI.ToString() + " (" + epiExte.tDS_CONDICAO + ")";

                    table.Rows.Add(newRow);
                }
            }

            //listar EPI tipo 4 = pandemia
            ArrayList PandEPI = new Epi().Find("IndEPI = 4 order by Descricao ");
            foreach (Epi epi in PandEPI)
            {                
                newRow = table.NewRow();
                newRow["Id"] = epi.Id.ToString();
                newRow["Descricao"] = epi.ToString();

                table.Rows.Add(newRow);
            }



            DataRow[] rows = table.Select("", "Descricao");
            StringBuilder strCheck = new StringBuilder();

            foreach (DataRow row in rows)
                if (strCheck.ToString().IndexOf(row["Descricao"].ToString()).Equals(-1))
                {
                    newRowOrdered = tableOrdered.NewRow();

                    newRowOrdered["Id"] = row["Id"].ToString();
                    newRowOrdered["Descricao"] = row["Descricao"].ToString();

                    tableOrdered.Rows.Add(newRowOrdered);

                    strCheck.Append(row["Descricao"].ToString());
                }

            return tableOrdered;
        }

        public string GetEquipamentos()
        {
            //string str = "IdEquipamentoMedicao IN (SELECT nID_EQP_MED FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblPPRA1 WHERE nID_LAUD_TEC=" + this.Id + " and nId_Func in ( select nId_Func from " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblFunc ) )";
            string str = "IdEquipamentoMedicao IN (SELECT nID_EQP_MED FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblPPRA1 WHERE nID_LAUD_TEC=" + this.Id + "  )";

            ArrayList list = new EquipamentoMedicao().Find(str);

            return EquipamentoMedicao.GetListaEquipamentoMedicao(list, this.hDT_LAUDO);
        }

        public ArrayList GerarCronogramaPadrao()
        {
            ArrayList list = new ArrayList();

            //Padrão 01
            CronogramaPPRA cronogramaPPRA1 = new CronogramaPPRA();
            cronogramaPPRA1.Inicialize();
            cronogramaPPRA1.IdLaudoTecnico = this;
            cronogramaPPRA1.Prazo = this.hDT_LAUDO.AddMonths(1);
            cronogramaPPRA1.PlanejamentoAnual = "1) Elaboração de Laudo Técnico Individual de Condições Ambientais do Trabalho - LTCAT, identificando, para cada atividade laboral dos trabalhadores da empresa, as condições ambientais de trabalho e o registro dos agentes nocivos através de metodologia de análise da exposição aos riscos ambientais, realizada através das etapas de reconhecimento, avaliação e conseqüente controle da ocorrência de riscos ambientais existentes no ambiente de trabalho, com o objetivo de alcançar a conclusão de que a exposição à estes são, ou não, prejudiciais à saúde ou à integridade física dos trabalhadores, conforme disposição legal que servirá de relevante informação para a elaboração do PCMSO.";
            cronogramaPPRA1.MetodologiaAcao = "A elaboração do Laudo Técnico Individual de Condições Ambientais de Trabalho ficará a cargo de engenheiro de segurança do trabalho que verificará se as atividades ou operações executadas no processo produtivo da empresa, por sua natureza, condições ou métodos de trabalho, expõem os empregados à agentes nocivos à saúde acima dos limites de tolerância fixados em razão da natureza e da intensidade do agente e do tempo de exposição aos seus efeitos.";
            cronogramaPPRA1.FormaRegistro = "PPRA - LTCAT Individual.";
            cronogramaPPRA1.PopularPrazo();
            cronogramaPPRA1.Save();
            list.Add(cronogramaPPRA1);

            //Padrão 02
            CronogramaPPRA cronogramaPPRA2 = new CronogramaPPRA();
            cronogramaPPRA2.Inicialize();
            cronogramaPPRA2.IdLaudoTecnico = this;
            cronogramaPPRA2.Prazo = this.hDT_LAUDO.AddYears(1);
            cronogramaPPRA2.PlanejamentoAnual = "2) Gerenciamento adequado do controle da exposição aos riscos ambientais através do fornecimento adequado e rigorosa fiscalização sobre a obrigatoriedade do uso dos Equipamentos de Proteção Individual - EPI indicados no PPRA que atenuam, reduzem, neutralizam, bem como conferem proteção eficaz aos trabalhadores em relação à nocividade dos agentes, reduzindo seus efeitos à limites legais de tolerância, conforme certificado de aprovação emitido pelo Ministério do Trabalho.";
            cronogramaPPRA2.MetodologiaAcao = "Os encarregados responsáveis pelo processo produtivo da empresa, fiscalizarão diariamente o uso adequado dos equipamentos de proteção individual pelos empregados.";
            cronogramaPPRA2.FormaRegistro = "PPRA, Ordens de Serviço, diálogos diários de conscientização executados pelos responsáveis/chefes do processo produtivo sobre segurança e saúde no trabalho.";
            cronogramaPPRA2.PopularPrazo();
            cronogramaPPRA2.Save();
            list.Add(cronogramaPPRA2);

            //Padrão 03
            CronogramaPPRA cronogramaPPRA3 = new CronogramaPPRA();
            cronogramaPPRA3.Inicialize();
            cronogramaPPRA3.IdLaudoTecnico = this;
            cronogramaPPRA3.Prazo = this.hDT_LAUDO.AddMonths(1);
            cronogramaPPRA3.PlanejamentoAnual = "3) Inspeção diária a ser executada pelos encarregados/chefes responsáveis pelo processo produtivo da empresa com o objetivo de instruir os empregados quanto às precauções a tomar no sentido de evitar acidentes ou doenças ocupacionais, adotando, de imediato, se necessário, medidas para eliminar ou neutralizar as condições inseguras e os riscos profissionais que possam originar-se nos locais de trabalho.";
            cronogramaPPRA3.MetodologiaAcao = "Controle dos riscos ambientais e ocupacionais objetivando que todos assumam parcelas de responsabilidade na prevenção de acidentes e doenças do trabalho, possibilitam a determinação e aplicação de meios preventivos antes da ocorrência de acidentes; ajudam a fixar nos empregados a disciplina prevencionista consciente.";
            cronogramaPPRA3.FormaRegistro = "PPRA, Ordens de Serviço, diálogos diários de conscientização executados pelos responsáveis/chefes do processo produtivo sobre segurança e saúde no trabalho.";
            cronogramaPPRA3.Mes01 = true;
            cronogramaPPRA3.Mes04 = true;
            cronogramaPPRA3.Mes07 = true;
            cronogramaPPRA3.Mes10 = true;
            cronogramaPPRA3.Ano = this.hDT_LAUDO.Year + " e " + this.hDT_LAUDO.AddYears(1).Year;
            cronogramaPPRA3.Save();

            list.Add(cronogramaPPRA3);

            return list;
        }

        public DataSet GetLaudosTecnicos_Data(string IdCliente)
        {
            DataSet ds = this.Get("nID_EMPR=" + IdCliente
                + " AND (nID_PEDIDO IN (SELECT IdPedido FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.Pedido WHERE DataConclusao IS NOT NULL) OR bAE =1)"
                + " ORDER BY hDT_LAUDO DESC");

            return GetDataSourceLaudoTecnico_Data(ds);
        }

        public DataSet GetLaudosTecnicos_Data_PGR(string IdCliente)
        {
            DataSet ds = this.Get("nID_EMPR=" + IdCliente + " and datepart(yyyy, hdt_Laudo ) >= 2021 "
                + " AND (nID_PEDIDO IN (SELECT IdPedido FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.Pedido WHERE DataConclusao IS NOT NULL) OR bAE =1)"
                + " ORDER BY hDT_LAUDO DESC");

            return GetDataSourceLaudoTecnico_Data(ds);
        }


        public DataSet GetLaudosTecnicos(string IdCliente)
        {
            DataSet ds = this.Get("nID_EMPR=" + IdCliente
                + " AND (nID_PEDIDO IN (SELECT IdPedido FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.Pedido WHERE DataConclusao IS NOT NULL) OR bAE =1)"
                + " ORDER BY hDT_LAUDO DESC");

            return GetDataSourceLaudoTecnico(ds);
        }

        public DataSet GetLaudosTecnicos(EmpregadoFuncao empregadoFuncao)
        {
            DataSet ds = this.Get("(nID_PEDIDO IN (SELECT IdPedido FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.Pedido WHERE DataConclusao IS NOT NULL) OR bAE =1)"
                + " AND nID_LAUD_TEC IN (SELECT nID_LAUD_TEC FROM tblFUNC_EMPREGADO WHERE nID_EMPREGADO_FUNCAO=" + empregadoFuncao.Id + ")"
                + " ORDER BY hDT_LAUDO DESC");

            return GetDataSourceLaudoTecnico(ds);
        }

        private DataSet GetDataSourceLaudoTecnico(DataSet dsLaudos)
        {
            DataSet dsData = new DataSet();
            DataTable table = new DataTable("Default");
            table.Columns.Add("IdLaudoTecnico", Type.GetType("System.String"));
            table.Columns.Add("DataLaudo", Type.GetType("System.String"));
            dsData.Tables.Add(table);
            DataRow rowData;

            foreach (DataRow row in dsLaudos.Tables[0].Select())
            {
                rowData = dsData.Tables[0].NewRow();

                rowData["IdLaudoTecnico"] = row["nID_LAUD_TEC"].ToString();
                rowData["DataLaudo"] = row["hDT_LAUDO"].ToString().Substring(0, row["hDT_LAUDO"].ToString().IndexOf(" "));

                dsData.Tables[0].Rows.Add(rowData);
            }
            return dsData;
        }

        private DataSet GetDataSourceLaudoTecnico_Data(DataSet dsLaudos)
        {
            DataSet dsData = new DataSet();
            DataTable table = new DataTable("Default");
            table.Columns.Add("IdLaudoTecnico", Type.GetType("System.String"));
            table.Columns.Add("DataLaudo", Type.GetType("System.String"));
            dsData.Tables.Add(table);
            DataRow rowData;

            System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");

            foreach (DataRow row in dsLaudos.Tables[0].Select())
            {
                rowData = dsData.Tables[0].NewRow();

                rowData["IdLaudoTecnico"] = row["nID_LAUD_TEC"].ToString();

                DateTime z1 = (DateTime)row["hDT_LAUDO"];

                rowData["DataLaudo"] = z1.ToString("dd/MM/yyyy", ptBr);

                dsData.Tables[0].Rows.Add(rowData);
            }
            return dsData;
        }


        public enum ImportacaoTipo : int
        {
            Nenhuma,
            Parcial,
            Completa
        }

        public enum EmpresaTipo : int
        {
            ProprioLocal,
            Grupo,
            LocalDeTrabalho
        }

        public void ImportaLaudoTecnico(LaudoTecnico laudoDo,
                                        int importacaoTipo,
                                        int empresaTipo,
                                        ArrayList listGhe,
                                        bool NovoLaudo)
        {
            if (importacaoTipo == (int)ImportacaoTipo.Nenhuma)
                return;
            
            if (NovoLaudo)
            {
                this.PrestadorID = laudoDo.PrestadorID;
                this.PrestadorAE = laudoDo.PrestadorAE;

                this.nID_COMISSAO_1 = laudoDo.nID_COMISSAO_1;
                this.nID_COMISSAO_2 = laudoDo.nID_COMISSAO_2;
                this.nID_COMISSAO_3 = laudoDo.nID_COMISSAO_3;

                this.mDS_DECL_EMPG = laudoDo.mDS_DECL_EMPG;
                this.mDS_DECL_EMPR = laudoDo.mDS_DECL_EMPR;
                this.ConclusaoPPRA = laudoDo.ConclusaoPPRA;

                this.NumEngSegTrabPPRA = laudoDo.NumEngSegTrabPPRA;
                this.NumTecSegTrabPPRA = laudoDo.NumTecSegTrabPPRA;

                this.AcompanhantePPRA = laudoDo.AcompanhantePPRA;
                this.TituloAcompanhantePPRA = laudoDo.TituloAcompanhantePPRA;

                this.TituloEngSegTrabPPRA = laudoDo.TituloEngSegTrabPPRA;
                this.TituloTecSegTrabPPRA = laudoDo.TituloTecSegTrabPPRA;

                this.tNO_DIR_FTO_FUNC = laudoDo.tNO_DIR_FTO_FUNC;
                this.tNO_ARQ_EXT_FTO_FUNC = laudoDo.tNO_ARQ_EXT_FTO_FUNC;
                this.tNO_ARQ_INIC_FTO_FUNC = laudoDo.tNO_ARQ_INIC_FTO_FUNC;
                this.tNO_ARQ_TERM_FTO_FUNC = laudoDo.tNO_ARQ_TERM_FTO_FUNC;

                this.Save();
            }

            //Importar GHE's
            if (listGhe == null)
                listGhe = new Ghe().Find("nID_LAUD_TEC=" + laudoDo.Id);

            foreach (Ghe gheDo in listGhe)
            {
                Ghe ghe = (Ghe)gheDo.Clone();
                ghe.Id = 0;
                ghe.nID_LAUD_TEC.Id = this.Id;
                ghe.nID_GHE_OLD.Id = gheDo.Id;
                ghe.Save();
                
                //Importar EPI Acidentes Existente
                ArrayList listGheEpiExte = new GheEpiExistente().Find("nID_FUNC=" + gheDo.Id + " AND nID_EPI IN (SELECT nID_EPI FROM tblEPI)");
                foreach (GheEpiExistente gheEpiExistenteDo in listGheEpiExte)
                {
                    GheEpiExistente gheEpiExistente = (GheEpiExistente)gheEpiExistenteDo.Clone();
                    gheEpiExistente.Id = 0;
                    gheEpiExistente.nID_FUNC.Id = ghe.Id;
                    gheEpiExistente.Save();
                }

                //Importar EPI Acidentes Recomendado
                ArrayList listGheEpiRcm = new GheEpiRecomendado().Find("nID_FUNC=" + gheDo.Id + " AND nID_EPI IN (SELECT nID_EPI FROM tblEPI)");
                foreach (GheEpiRecomendado gheEpiRecomendadoDo in listGheEpiRcm)
                {
                    GheEpiRecomendado gheEpiRecomendado = (GheEpiRecomendado)gheEpiRecomendadoDo.Clone();
                    gheEpiRecomendado.Id = 0;
                    gheEpiRecomendado.nID_FUNC.Id = ghe.Id;
                    gheEpiRecomendado.Save();
                }

                /*IMPORTA ERGONOMIA DESSE GHE*/
                ArrayList listErgonomiaGhe
                    = new ErgonomiaGhe().Find("nID_FUNC=" + gheDo.Id
                    + " AND nID_LAUD_TEC=" + laudoDo.Id);

                foreach (ErgonomiaGhe ergonomiaGheDo in listErgonomiaGhe)
                {
                    ErgonomiaGhe ergonomiaGhe = (ErgonomiaGhe)ergonomiaGheDo.Clone();
                    ergonomiaGhe.Id = 0;
                    ergonomiaGhe.nID_FUNC.Id = ghe.Id;
                    ergonomiaGhe.nID_LAUD_TEC.Id = this.Id;
                    ergonomiaGhe.Save();
                }

                /*IMPORTAR ORDER DE SERVIÇO DO GHE*/
                ArrayList listAcidenteGhe = new RiscoAcidenteGhe().Find("IdGhe=" + gheDo.Id);

                foreach (RiscoAcidenteGhe riscoAcidenteGheDo in listAcidenteGhe)
                {
                    RiscoAcidenteGhe riscoAcidenteGhe = (RiscoAcidenteGhe)riscoAcidenteGheDo.Clone();
                    riscoAcidenteGhe.Id = 0;
                    riscoAcidenteGhe.IdGhe.Id = ghe.Id;
                    riscoAcidenteGhe.Save();
                }

                ArrayList listProcIntrutivoGhe = new ProcedimentoInstrutivoGhe().Find("IdGhe=" + gheDo.Id);

                foreach (ProcedimentoInstrutivoGhe procInstrutivoGheDo in listProcIntrutivoGhe)
                {
                    ProcedimentoInstrutivoGhe procInstrutivoGhe = (ProcedimentoInstrutivoGhe)procInstrutivoGheDo.Clone();
                    procInstrutivoGhe.Id = 0;
                    procInstrutivoGhe.IdGhe.Id = ghe.Id;
                    procInstrutivoGhe.Save();
                }

                /*IMPORTA OS EMPREGADOS DO GHE*/
                if (NovoLaudo)
                {
                   if (empresaTipo == (int)EmpresaTipo.ProprioLocal 
                            || laudoDo.nID_EMPR.Id == this.nID_EMPR.Id)
                    {
                        /*Importar Empregados desse GHE*/
                        ArrayList listGheEmpregado
                            = new GheEmpregado().Find("nID_FUNC=" + gheDo.Id
                            + " AND nID_LAUD_TEC=" + laudoDo.Id
                            + " AND nID_EMPREGADO_FUNCAO IN (SELECT nID_EMPREGADO_FUNCAO FROM tblEMPREGADO_FUNCAO "
                            + " WHERE nID_EMPREGADO IN (SELECT nID_EMPREGADO FROM tblEMPREGADO WHERE hDT_DEM IS NULL)"
                            + ")");

                        foreach (GheEmpregado gheEmpregadoDo in listGheEmpregado)
                        {
                            if (gheEmpregadoDo.nID_EMPREGADO_FUNCAO.mirrorOld == null)
                                gheEmpregadoDo.nID_EMPREGADO_FUNCAO.Find();

                            if (gheEmpregadoDo.nID_EMPREGADO_FUNCAO.hDT_INICIO > this.hDT_LAUDO.AddYears(1))
                                continue;

                            GheEmpregado gheEmpregado = (GheEmpregado)gheEmpregadoDo.Clone();
                            gheEmpregado.Id = 0;
                            gheEmpregado.nID_FUNC.Id = ghe.Id;
                            gheEmpregado.nID_LAUD_TEC.Id = this.Id;
                            gheEmpregado.Save();
                        }
                    }
                    else if (empresaTipo == (int)EmpresaTipo.LocalDeTrabalho)
                    {
                        ArrayList listEmpregadoFuncao = new EmpregadoFuncao().Find(
                            "nID_EMPR=" + this.nID_EMPR.Id
                            + " AND nID_FUNCAO IN (SELECT nID_FUNCAO "
                            + " FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEMPREGADO_FUNCAO "
                            + " WHERE nID_EMPREGADO_FUNCAO IN (SELECT nID_EMPREGADO_FUNCAO "
                            + " FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblFUNC_EMPREGADO WHERE nID_FUNC = " + gheDo.Id + "))");

                        foreach (EmpregadoFuncao empregadoFuncao in listEmpregadoFuncao)
                        {
                            GheEmpregado gheEmpregado = new GheEmpregado();
                            gheEmpregado.Inicialize();
                            gheEmpregado.nID_FUNC.Id = ghe.Id;
                            gheEmpregado.nID_LAUD_TEC.Id = this.Id;
                            gheEmpregado.nID_EMPREGADO_FUNCAO.Id = empregadoFuncao.Id;
                            gheEmpregado.Save();
                        }
                    }
                }

                if (importacaoTipo != (int)ImportacaoTipo.Completa)
                    continue;

                /*IMPORTA OS LEVANTAMENTOS DE RISCO*/
                ArrayList listPPRA = new PPRA().Find("nID_FUNC=" + gheDo.Id + " AND nID_LAUD_TEC=" + laudoDo.Id);

                foreach (PPRA ppraDo in listPPRA)
                {
                    PPRA ppra = (PPRA)ppraDo.Clone();
                    ppra.Id = 0;
                    ppra.nID_FUNC.Id = ghe.Id;
                    ppra.nID_LAUD_TEC.Id = this.Id;
                    ppra.Save();

                    /*IMPORTA EPI RECOMENDADO*/
                    ArrayList listEpiRecomendado = new EpiRecomendado().Find("nID_PPRA=" + ppraDo.Id);
                    foreach (EpiRecomendado epiRecomendadoDo in listEpiRecomendado)
                    {
                        EpiRecomendado epiRecomendado = (EpiRecomendado)epiRecomendadoDo.Clone();
                        epiRecomendado.Id = 0;
                        epiRecomendado.nID_PPRA.Id = ppra.Id;
                        epiRecomendado.Save();
                    }

                    /*IMPORTA EPI EXISTENTE*/
                    ArrayList listEpiExistente = new EpiExistente().Find("nID_PPRA=" + ppraDo.Id);
                    foreach (EpiExistente epiExistenteDo in listEpiExistente)
                    {
                        EpiExistente epiExistente = (EpiExistente)epiExistenteDo.Clone();
                        epiExistente.Id = 0;
                        epiExistente.nID_PPRA.Id = ppra.Id;
                        epiExistente.Save();
                    }
                    try
                    {
                        ppra.AtualizaLimiteTolerancia();
                        ppra.AtualizaNeutralizado();
                        ppra.Save();
                    }
                    catch { }
                }
            }
        }
    }


    [Database("sied_novo", "tbl_Laudos", "nId_Laudo")]
    public class tbl_Laudos : Ilitera.Data.Table
    {
        private int _nId_Laudo;
        private int _nId_Chave;
        private string _Laudo = string.Empty;
        private int _nId_Empr;
        private DateTime _Data_Laudo;
        private string _Path = string.Empty;

        public tbl_Laudos()
        {

        }
        public override int Id
        {
            get { return _nId_Laudo; }
            set { _nId_Laudo = value; }
        }
        public int nId_Chave
        {
            get { return _nId_Chave; }
            set { _nId_Chave = value; }
        }
        public string Laudo
        {
            get { return _Laudo; }
            set { _Laudo = value; }
        }
        public int nId_Empr
        {
            get { return _nId_Empr; }
            set { _nId_Empr = value; }
        }
        public DateTime Data_Laudo
        {
            get { return _Data_Laudo; }
            set { _Data_Laudo = value; }
        }
        public string Path
        {
            get { return _Path; }
            set { _Path = value; }
        }

    }


    [Database("sied_novo", "tblErg_Documento", "nId_Erg_Documento")]
    public class LaudoErgonomicoDocumento : Ilitera.Data.Table
    {
        private int _nId_Erg_Documento;
        private int _nId_Laud_Tec;
        private string _Titulo = string.Empty;
        private string _Texto = string.Empty;

        public LaudoErgonomicoDocumento()
        {

        }
        public override int Id
        {
            get { return _nId_Erg_Documento; }
            set { _nId_Erg_Documento = value; }
        }
        public int nId_Laud_Tec
        {
            get { return _nId_Laud_Tec; }
            set { _nId_Laud_Tec = value; }
        }
        public string Titulo
        {
            get { return _Titulo; }
            set { _Titulo = value; }
        }
        public string Texto
        {
            get { return _Texto; }
            set { _Texto = value; }
        }

    }



    [Database("sied_novo", "tblPAE", "nIdPAE")]
    public class PAE : Ilitera.Data.Table
    {
        private int _nIdPAE;
        private int _nId_Laud_Tec;
        private string _TextoPAE = string.Empty;


        public PAE()
        {

        }
        public override int Id
        {
            get { return _nIdPAE; }
            set { _nIdPAE = value; }
        }
        public int nId_Laud_Tec
        {
            get { return _nId_Laud_Tec; }
            set { _nId_Laud_Tec = value; }
        }
        public string TextoPAE
        {
            get { return _TextoPAE; }
            set { _TextoPAE = value; }
        }

    }


}
