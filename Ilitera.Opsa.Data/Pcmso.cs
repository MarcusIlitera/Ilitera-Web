using System;
//using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;

using Ilitera.Data;
using Ilitera.Common;
using System.Collections;

namespace Ilitera.Opsa.Data
{
    [Database("opsa", "Pcmso", "IdPcmso")]
    public class Pcmso : Ilitera.Opsa.Data.Documento
    {
        #region Properties

        private int _IdPcmso;
        private Prestador _IdCoordenador;
        private LaudoTecnico _IdLaudoTecnico;
        private DateTime _DataPcmso = DateTime.Today;
        private DateTime _TerminoPcmso;
        private bool _IsInformarProximoPeriodico;
        private DateTime _ProximoPeriodico;
        private string _TextoRTF = string.Empty;
        private bool _IsFinalizado; // Persit false -> Apagar no banco
        private bool _IsRelatorioAnualCalculado;
        private bool _IsFromWeb;
        private bool _IsRetirarAcoesSaude;
        private bool _IsDadosCoordenador;
        private bool _IsSemAssinatura;
        private int _NumFuncionarioMin;
        private int _NumFuncionarioMax;
        private int _IdPreposto;
        private bool _Risco_Acidente_PCMSO;

        #region Eventos

        public delegate void EventProgress(int val);
        public delegate void EventProgressIniciar(string nomeGhe, int val);

        public event EventProgressIniciar ProgressIniciar;
        public event EventProgress ProgressAtualizar;

        #endregion

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public Pcmso()
        {

        }
        public override int Id
        {
            get { return _IdPcmso; }
            set { _IdPcmso = value; }
        }
        [Obrigatorio(true, "Você deve selecionar um médico coordenador!")]
        public Prestador IdCoordenador
        {
            get { return _IdCoordenador; }
            set { _IdCoordenador = value; }
        }
        public LaudoTecnico IdLaudoTecnico
        {
            get { return _IdLaudoTecnico; }
            set { _IdLaudoTecnico = value; }
        }
        public DateTime DataPcmso
        {
            get { return _DataPcmso; }
            set { _DataPcmso = value; }
        }
        public DateTime TerminoPcmso
        {
            get { return _TerminoPcmso; }
            set { _TerminoPcmso = value; }
        }
        public bool IsInformarProximoPeriodico
        {
            get { return _IsInformarProximoPeriodico; }
            set { _IsInformarProximoPeriodico = value; }
        }
        public DateTime ProximoPeriodico
        {
            get { return _ProximoPeriodico; }
            set { _ProximoPeriodico = value; }
        }
        public string TextoRTF
        {
            get { return _TextoRTF; }
            set { _TextoRTF = value; }
        }
        [Persist(false)]
        public bool IsFinalizado
        {
            get
            {
                controlePedido = GetControlePedidoPcmsoConfiguracaoExames();

                if (controlePedido == null || controlePedido.Id == 0)
                    _IsFinalizado = false;
                else
                    _IsFinalizado = controlePedido.Termino != new DateTime();

                return _IsFinalizado;
            }
        }
        public bool IsRelatorioAnualCalculado
        {
            get { return _IsRelatorioAnualCalculado; }
            set { _IsRelatorioAnualCalculado = value; }
        }
        public bool IsFromWeb
        {
            get { return _IsFromWeb; }
            set { _IsFromWeb = value; }
        }
        public bool IsRetirarAcoesSaude
        {
            get { return _IsRetirarAcoesSaude; }
            set { _IsRetirarAcoesSaude = value; }
        }
        public bool IsDadosCoordenador
        {
            get { return _IsDadosCoordenador; }
            set { _IsDadosCoordenador = value; }
        }
        public bool IsSemAssinatura
        {
            get { return _IsSemAssinatura; }
            set { _IsSemAssinatura = value; }
        }
        public int NumFuncionarioMin
        {
            get { return _NumFuncionarioMin; }
            set { _NumFuncionarioMin = value; }
        }
        public int NumFuncionarioMax
        {
            get { return _NumFuncionarioMax; }
            set { _NumFuncionarioMax = value; }
        }
        public int IdPreposto
        {
            get { return _IdPreposto; }
            set { _IdPreposto = value; }
        }

        public bool Risco_Acidente_PCMSO
        {
            get { return _Risco_Acidente_PCMSO; }
            set { _Risco_Acidente_PCMSO = value; }
        }
        

        public override string ToString()
        {
            return _DataPcmso.ToString("dd-MM-yyyy");
        }
        #endregion

        #region Metodos

        #region Override

        public override void Validate()
        {
            if (this.Id == 0)
            {
                if (this.IdCliente.IdCNAE == null)
                    this.IdCliente.Find();

                if (!this.IsFromWeb)
                    if (this.IdPedido.Id == 0 && this.IdCliente.ContrataPCMSO.Equals((int)TipoPcmsoContratada.Contratada))
                        throw new Exception("Pedido é campo obrigatório!");

                if (!this.IsFromWeb)
                    if (this.IdLaudoTecnico.Id == 0 && this.IdCliente.ContrataPCMSO != (int)TipoPcmsoContratada.NaoContratada)
                        throw new Exception("Laudo Técnico é campo obrigatório!");

                int count = new Pcmso().ExecuteCount("IdCliente=" + this.IdCliente.Id
                                                + " AND TerminoPcmso IS NULL"
                                                + " AND IsFromWeb=0");
                if (count > 0)
                    throw new Exception("Coloque primeiro a data de término no último PCMSO antes de criar um novo!");

                this.IdDocumentoBase.Id = (int)Documentos.PCMSO;
            }

            if(this.TerminoPcmso != new DateTime()
                && this.TerminoPcmso <= this.DataPcmso)
                throw new Exception("A data de término do PCMSO não pode ser menor que a data de início!");

            if (IsFromWeb && this.IdPedido.Id != 0)
                throw new Exception("PCMSO com pedido não pode ser do tipo somente período!");

            this.IdPrestador.Id = this.IdCoordenador.Id;

            this.DataLevantamento = this.DataPcmso;

            base.Validate();
        }
        #endregion

        #region GetTerninoPcmso

        public DateTime GetTerninoPcmso()
        {
            if (this._TerminoPcmso == new DateTime() || this._TerminoPcmso == new DateTime(1753, 1, 1))
                return this.DataPcmso.AddYears(1).AddDays(-1);
            else
                return this._TerminoPcmso;
        }
        #endregion

        #region GetControlePedidoPcmsoConfiguracaoExames

        private ControlePedido controlePedido;

        public ControlePedido GetControlePedidoPcmsoConfiguracaoExames()
        {
            if (controlePedido == null)
            {
                controlePedido = new ControlePedido();

                if (this.IdPedido.Id == 0)
                    return controlePedido;

                string where = "IdControle=" + (int)Controles.PcmsoConfiguracaoExames
                                        + " AND IdPedido=" + this.IdPedido.Id;

                controlePedido.Find(where);

                if (controlePedido.Id == 0)
                {
                    ControlePedido.GerarDatario(this.IdPedido);

                    controlePedido = new ControlePedido();
                    controlePedido.Find(where);
                }
            }

            return controlePedido;
        }
        #endregion

        #region IsPCMSOFinalizado

        public bool IsPCMSOFinalizado()
        {
            string strWhere = "IdPedido IN (SELECT IdPedido FROM Documento WHERE"
                + " IdDocumento IN (SELECT IdDocumento FROM Pcmso WHERE IdPcmso=" + this.Id + "))";

            Pedido pedido = new Pedido();
            pedido.Find(strWhere);

            if (pedido.DataConclusao == new DateTime())
                return false;
            else
                return true;
        }
        #endregion

        #region ApagarPlanejamento
        public void ApagarPlanejamento()
        {
            if (this.IsFinalizado)
                throw new Exception("Esse PCMSO está finalizado, não podendo ser recalculado!");

            string strCmd = "IdPcmso=" + this.Id
                + " AND IdGhe IS NOT NULL"
                + " AND IdGHE NOT IN (SELECT nID_FUNC FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblFUNC WHERE"
                + " nID_LAUD_TEC IN (SELECT IdLaudoTecnico FROM Pcmso WHERE IdPcmso=" + this.Id + "))";

            new PcmsoGhe().Delete(strCmd);
            new PcmsoPlanejamento().Delete("Preventivo =0 AND " + strCmd);
        } 
        #endregion

        #region AtualizarSugestaoTodosGhes
        public void AtualizarSugestaoTodosGhes()
        {
            this.ApagarPlanejamento();

            List<Ghe> ghes = new Ghe().Find<Ghe>("nID_LAUD_TEC=" + this.IdLaudoTecnico.Id + " ORDER BY tNO_FUNC");

            foreach (Ghe ghe in ghes)
                ghe.CriarSugestaoExamesComplementares(this);
        } 
        #endregion

        #region ImportarConfiguracaoPcmsoAnterior

        public void ImportarConfiguracaoPcmsoAnterior()
        {
            this.ApagarPlanejamento();

            List<Ghe> list = new Ghe().Find<Ghe>("nID_LAUD_TEC=" + this.IdLaudoTecnico.Id + " ORDER BY tNO_FUNC");

            foreach (Ghe ghe in list)
            {
                if (ghe.nID_GHE_OLD.Id != 0 && ghe.Id != ghe.nID_GHE_OLD.Id)
                {
                    if (ghe.nID_GHE_OLD.mirrorOld == null)
                        ghe.nID_GHE_OLD.Find();

                    ghe.CriarPcmsoGhe(this);

                    List<PcmsoPlanejamento> listPcmsoPlan = new PcmsoPlanejamento().Find<PcmsoPlanejamento>("IdGhe=" + ghe.nID_GHE_OLD.Id);

                    foreach (PcmsoPlanejamento pcmsoPlan in listPcmsoPlan)
                    {
                        System.Collections.ArrayList listPcmsoPlanIdade = new PcmsoPlanejamentoIdade().Find("IdPcmsoPlanejamento=" + pcmsoPlan.Id);

                        PcmsoPlanejamento pcmsoPlanejamento = PcmsoPlanejamento.GetPcmsoPlanejemanto(
                                                                            this,
                                                                            ghe,
                                                                            pcmsoPlan.IdExameDicionario,
                                                                            pcmsoPlan.Preventivo);

                        PcmsoPlanejamento.Adiciona(pcmsoPlanejamento, pcmsoPlan, listPcmsoPlanIdade);
                    }
                }
                else
                    ghe.CriarSugestaoExamesComplementares(this);
            }
        }

        #endregion

        #region GetPeriodo

        public string GetPeriodo()
        {
            return this._DataPcmso.ToString("dd-MM-yyyy")
                    + " a "
                    + this.GetTerninoPcmso().ToString("dd-MM-yyyy");
        }
        #endregion

        #region GetPcmso

        public static Pcmso GetPcmso(Pedido pedido)
        {
            Pcmso pcmso = new Pcmso();

            System.Collections.ArrayList list = pcmso.Find("IdPedido=" + pedido.Id 
                                                        + " AND IsFromWeb=0");

            if (list.Count > 1)
                throw new Exception("Este pedido pertence a mais de um PCMSO!");
            else if (pcmso.Id == 0 && pedido.DataConclusao != new DateTime())
                throw new Exception("PCMSO sem Pedido!");

            if (pcmso.Id == 0)
            {
                pcmso = new Pcmso();
                pcmso.Inicialize();
                pcmso.IdCliente.Id = pedido.IdCliente.Id;
                pcmso.IdCoordenador = pedido.IdCliente.GetCoordenadorPadraoPCMSO();
                pcmso.IdPedido = pedido;
                pcmso.IdLaudoTecnico.Id = LaudoTecnico.GetUltimoLaudo(pedido.IdCliente.Id).Id;
            }
            return pcmso;
        }
        #endregion

        #region GetDSCoordenador

        public DataSet GetDSCoordenador()
        {
            DataSet ds = new DataSet();
            DataTable table = new DataTable("Result");
            table.Columns.Add("IdPrestador", Type.GetType("System.Int32"));
            table.Columns.Add("Nome", Type.GetType("System.String"));
            table.Columns.Add("Titulo", Type.GetType("System.String"));
            table.Columns.Add("Numero", Type.GetType("System.String"));
            table.Columns.Add("TituloCoordenador", Type.GetType("System.String"));
            table.Columns.Add("Contato", Type.GetType("System.String"));
            table.Columns.Add("DataAssinatura", Type.GetType("System.String"));
            table.Columns.Add("SemAssinatura", Type.GetType("System.Boolean"));
            table.Columns.Add("iDocumento", Type.GetType("System.Byte[]"));
            table.Columns.Add("iAssinatura", Type.GetType("System.Byte[]"));
            ds.Tables.Add(table);
            DataRow newRow;

            if (this.IdCoordenador.mirrorOld == null)
                this.IdCoordenador.Find();

            newRow = ds.Tables[0].NewRow();

            newRow["IdPrestador"] = this.IdCoordenador.Id;
            newRow["Nome"] = this.IdCoordenador.NomeCompleto;
            //newRow["Titulo"] = this.IdCoordenador.Titulo;

            if (this.IdCoordenador.UF != null)
                newRow["Numero"] = this.IdCoordenador.Numero + "  " + this.IdCoordenador.UF;
            else
                newRow["Numero"] = this.IdCoordenador.Numero;


            //if (this.IdCoordenador.Id == (int)Medicos.DraMarcela
            //    || this.IdCoordenador.Id == (int)Medicos.DraRosana)
            //    newRow["TituloCoordenador"] = "Coordenadora do PCMSO";
            //else
                newRow["TituloCoordenador"] = "Médico(a) responsável pelo PCMSO";

            newRow["Contato"] = this.IdCoordenador.Contato;
            newRow["SemAssinatura"] = this.IsSemAssinatura;

            
            if (this.IdCoordenador.FotoAss != "")
            {

                newRow["iAssinatura"] = Ilitera.Common.Fotos.GetByteFoto_Uri(this.IdCoordenador.FotoAss);  

              // newRow["iAssinatura"] = Ilitera.Common.Fotos.GetByteFoto(this.IdCoordenador.FotoAss);  //está dando erro, perde as barras, por mais que eu mude - Wagner

            }

            ds.Tables[0].Rows.Add(newRow);

            return ds;
        }



        public DataSet GetDSPreposto(Int32 xIdPrestador)
        {
            DataSet ds = new DataSet();
            DataTable table = new DataTable("Result");
            table.Columns.Add("IdPrestador", Type.GetType("System.Int32"));
            table.Columns.Add("Nome", Type.GetType("System.String"));
            table.Columns.Add("Titulo", Type.GetType("System.String"));
            table.Columns.Add("Numero", Type.GetType("System.String"));
            table.Columns.Add("TituloCoordenador", Type.GetType("System.String"));
            table.Columns.Add("Contato", Type.GetType("System.String"));
            table.Columns.Add("DataAssinatura", Type.GetType("System.String"));
            table.Columns.Add("SemAssinatura", Type.GetType("System.Boolean"));
            table.Columns.Add("iDocumento", Type.GetType("System.Byte[]"));
            table.Columns.Add("iAssinatura", Type.GetType("System.Byte[]"));
            ds.Tables.Add(table);
            DataRow newRow;


            Prestador rPrestador = new Prestador();
            rPrestador.Find(xIdPrestador);

            newRow = ds.Tables[0].NewRow();

            newRow["IdPrestador"] = xIdPrestador;
            newRow["Nome"] = rPrestador.NomeCompleto;
            newRow["TituloCoordenador"] = "Representante Legal da Empresa";

            if (rPrestador.UF != null)
                newRow["Numero"] = rPrestador.Numero + "  " + rPrestador.UF;
            else
                newRow["Numero"] = rPrestador.Numero;

            newRow["Titulo"] = rPrestador.Titulo;

            newRow["Contato"] = rPrestador.Contato;

            newRow["SemAssinatura"] = false;

            if (rPrestador.FotoAss != "")
            {
                newRow["iAssinatura"] = Ilitera.Common.Fotos.GetByteFoto(rPrestador.FotoAss);
            }
            ds.Tables[0].Rows.Add(newRow);

            return ds;
        }


        #endregion

        #region GetPlanejamentoExamesAso

        public string GetPlanejamentoExamesAso(Clinico clinico, Ghe ghe, bool ExamesSomentePeriodoPcmso)
        {
            return clinico.GetPlanejamentoExamesAso(ghe, ExamesSomentePeriodoPcmso);
        }

        #endregion

        #region GetCondicoesAmostragem

        public string GetCondicoesAmostragem()
        {
            StringBuilder ret = new StringBuilder();

            string str = "IdExameDicionario IN (SELECT IdExameDicionario "
                        + " FROM PcmsoPlanejamento WHERE IdPcmso=" + this.Id
                        + " AND IdExameDicionario IN"
                        + " (SELECT IdExameDicionario FROM ExameDicionario WHERE"
                        + " IndExame=" + (int)IndTipoExame.Complementar
                        + " AND IsObservacao=0"
                        + "))"
                        + " ORDER BY Nome";

            List<ExameDicionario> list = new ExameDicionario().Find<ExameDicionario>(str);

            int i = 1;

            foreach (ExameDicionario exameDicionario in list)
            {
                if (exameDicionario.Amostragem != string.Empty)
                    ret.Append(i++.ToString()
                        + ". "
                        + exameDicionario.Nome
                        + " - "
                        + exameDicionario.Amostragem
                        + "\r\n"
                        + "\r\n");
            }

            return ret.ToString();
        }
        #endregion

        #region GetPlanejamentoExames

        public string GetPlanejamentoExames(int IdGhe)
        {
            StringBuilder str = new StringBuilder();

            string criteria = "IdPcmso=" + this.Id 
                            + " AND IdGhe=" + IdGhe 
                            + " AND Preventivo=0";

            List<PcmsoPlanejamento> list = new PcmsoPlanejamento().Find<PcmsoPlanejamento>(criteria);

            list.Sort();

            int i = 1;

            foreach (PcmsoPlanejamento pcmsoPlan in list)
            {
                if (pcmsoPlan.IdExameDicionario.mirrorOld == null)
                    pcmsoPlan.IdExameDicionario.Find();

                str.Append(i.ToString() + ". " + pcmsoPlan.IdExameDicionario.Descricao + "\n");
                i++;
            }

            return str.ToString();
        }
        #endregion

        #region GetPlanejamentoExamesPeriodicidade

        public string GetPlanejamentoExamesPeriodicidade(int IdGhe)
        {
            string criteria = "IdPcmso=" + this.Id
                                + " AND IdGhe=" + IdGhe
                                + " AND Preventivo=0";

            List<PcmsoPlanejamento> 
                list = new PcmsoPlanejamento().Find<PcmsoPlanejamento>(criteria);

            list.Sort();

            int i = 1;

            StringBuilder ret = new StringBuilder();

            foreach (PcmsoPlanejamento pcmsoPlan in list)
            {
                if (pcmsoPlan.IdExameDicionario.mirrorOld == null)
                    pcmsoPlan.IdExameDicionario.Find();

                //Exclui o Periódico e o Exame Dermal 
                if (!(pcmsoPlan.IdExameDicionario.IsObservacao
                    || pcmsoPlan.IdExameDicionario.Id == (int)IndExameClinico.Periodico))
                {
                    ret.Append(i.ToString() + ". " + ExameDicionario.GetPeriodicidadeExame(pcmsoPlan) + "\n");
                    i++;
                }
            }

            return ret.ToString();
        }
        #endregion

        #region GetPlanejamentoExamesPreventivo

        public string GetPlanejamentoExamesPreventivo(int IdGhe)
        {
            string criteria = "IdPcmso=" + this.Id 
                            + " AND IdGhe=" + IdGhe 
                            + " AND Preventivo=1";

            List<PcmsoPlanejamento> list = new PcmsoPlanejamento().Find<PcmsoPlanejamento>(criteria);

            int i = 1;

            StringBuilder str = new StringBuilder();

            foreach (PcmsoPlanejamento pcmsoPlan in list)
            {
                pcmsoPlan.IdExameDicionario.Find();
                str.Append(i.ToString() + ". " + pcmsoPlan.IdExameDicionario.Nome + "\n");
                i++;
            }

            return str.ToString();
        }

        #endregion

        #region GetPlanejamentoExamesPeriodicidadePreventivo

        public string GetPlanejamentoExamesPeriodicidadePreventivo(int IdGhe)
        {
            string criteria = "IdPcmso=" + this.Id 
                                + " AND IdGhe=" + IdGhe 
                                + " AND Preventivo=1";

            List<PcmsoPlanejamento> list = new PcmsoPlanejamento().Find<PcmsoPlanejamento>(criteria);

            int i = 1;

            StringBuilder ret = new StringBuilder();

            foreach (PcmsoPlanejamento pcmsoPlan in list)
            {
                pcmsoPlan.IdExameDicionario.Find();
                ret.Append(i.ToString() + ". " + ExameDicionario.GetPeriodicidadeExame(pcmsoPlan) + "\n");
                i++;
            }

            return ret.ToString();
        }

        #endregion

        #region GetExamesComplementares

        public string GetExamesComplementares()
        {
            StringBuilder str = new StringBuilder();

            string strFind = "IdExameDicionario IN (SELECT IdExameDicionario"
                            + " FROM PcmsoPlanejamento WHERE IdPcmso=" + this.Id + ")"
                            + " ORDER BY Nome";

            List<ExameDicionario> list = new ExameDicionario().Find<ExameDicionario>(strFind);

            foreach (ExameDicionario exameDic in list)
            {
                string strFind2 = "IdExameDicionario=" + exameDic.Id
                                + " AND IdPcmsoPlanejamento IN"
                                + " (SELECT IdPcmsoPlanejamento FROM PcmsoPlanejamento WHERE"
                                + " IdPcmso=" + this.Id + ")";

                if (exameDic.Id == (int)IndExameClinico.Periodico)
                    continue;

                int qtdExames = new ExamePlanejamento().ExecuteCount(strFind2);

                if (qtdExames > 0)
                    str.Append(qtdExames + " - " + exameDic.Descricao + "; ");
            }

            return str.ToString();
        }
        #endregion

        #region GetNumeros

        #region GetNumFuncionarios

        public int GetNumFuncionarios()
        {
            StringBuilder str = new StringBuilder();

            str.Append(" nID_EMPREGADO IN ");
            //str.Append("(SELECT nID_EMPREGADO FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEMPREGADO_FUNCAO WHERE nID_EMPREGADO_FUNCAO IN ");
            str.Append("(SELECT nID_EMPREGADO FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEMPREGADO_FUNCAO WHERE hDT_Termino is null and nID_EMPREGADO_FUNCAO IN ");
            str.Append("(SELECT nID_EMPREGADO_FUNCAO FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblFUNC_EMPREGADO WHERE nID_LAUD_TEC=" + this.IdLaudoTecnico.Id + "))");

            int count = new Empregado().ExecuteCount(str.ToString());

            return count;
        }
        #endregion

        #region GetNumFuncionariosSexoMasculino

        public int GetNumFuncionariosSexoMasculino()
        {
            StringBuilder str = new StringBuilder();

            str.Append(" nID_EMPREGADO IN ");
            //str.Append("(SELECT nID_EMPREGADO FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEMPREGADO_FUNCAO WHERE nID_EMPREGADO_FUNCAO IN ");
            str.Append("(SELECT nID_EMPREGADO FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEMPREGADO_FUNCAO WHERE hDT_Termino is null and nID_EMPREGADO_FUNCAO IN ");
            str.Append("(SELECT nID_EMPREGADO_FUNCAO FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblFUNC_EMPREGADO WHERE nID_LAUD_TEC=" + this.IdLaudoTecnico.Id + "))");
            str.Append(" AND tSEXO = 'M'");

            int count = new Empregado().ExecuteCount(str.ToString());

            return count;
        }
        #endregion

        #region GetNumFuncionariosSexoFeminino

        public int GetNumFuncionariosSexoFeminino()
        {
            StringBuilder str = new StringBuilder();

            str.Append(" nID_EMPREGADO IN ");
            //str.Append("(SELECT nID_EMPREGADO FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEMPREGADO_FUNCAO WHERE nID_EMPREGADO_FUNCAO IN ");
            str.Append("(SELECT nID_EMPREGADO FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEMPREGADO_FUNCAO WHERE hDT_Termino is null and nID_EMPREGADO_FUNCAO IN ");
            str.Append("(SELECT nID_EMPREGADO_FUNCAO FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblFUNC_EMPREGADO WHERE nID_LAUD_TEC=" + this.IdLaudoTecnico.Id + "))");
            str.Append(" AND tSEXO = 'F'");

            int count = new Empregado().ExecuteCount(str.ToString());

            return count;
        }
        #endregion

        #region GetNumFuncionariosMenores18

        public int GetNumFuncionariosMenores18()
        {
            StringBuilder str = new StringBuilder();

            str.Append(" nID_EMPREGADO IN ");
            //str.Append("(SELECT nID_EMPREGADO FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEMPREGADO_FUNCAO WHERE nID_EMPREGADO_FUNCAO IN ");
            str.Append("(SELECT nID_EMPREGADO FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEMPREGADO_FUNCAO WHERE hDT_Termino is null and nID_EMPREGADO_FUNCAO IN ");
            str.Append("(SELECT nID_EMPREGADO_FUNCAO FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblFUNC_EMPREGADO WHERE nID_LAUD_TEC=" + this.IdLaudoTecnico.Id + "))");
            str.Append(" AND hDT_NASC IS NOT NULL ");
            str.Append(" AND DATEDIFF(Year, hDT_NASC , '" + this.GetTerninoPcmso().ToString("yyyy-MM-dd") + "')<=18");

            int count = new Empregado().ExecuteCount(str.ToString());

            return count;
        }
        #endregion

        #region GetNumFuncionariosMaiores45

        public int GetNumFuncionariosMaiores45()
        {
            StringBuilder str = new StringBuilder();

            str.Append(" nID_EMPREGADO IN ");
            //str.Append("(SELECT nID_EMPREGADO FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEMPREGADO_FUNCAO WHERE nID_EMPREGADO_FUNCAO IN ");
            str.Append("(SELECT nID_EMPREGADO FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEMPREGADO_FUNCAO WHERE hDT_Termino is null and nID_EMPREGADO_FUNCAO IN ");
            str.Append("(SELECT nID_EMPREGADO_FUNCAO FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblFUNC_EMPREGADO WHERE nID_LAUD_TEC=" + this.IdLaudoTecnico.Id + "))");
            str.Append(" AND hDT_NASC IS NOT NULL ");
            str.Append(" AND DATEDIFF(Year, hDT_NASC , '" + this.GetTerninoPcmso().ToString("yyyy-MM-dd") + "')>=45");

            int count = new Empregado().ExecuteCount(str.ToString());

            return count;
        }
        #endregion

        #region GetNumFuncionariosBeneficiarios

        public int GetNumFuncionariosBeneficiarios()
        {
            StringBuilder str = new StringBuilder();

            str.Append(" nID_EMPREGADO IN ");
            //str.Append("(SELECT nID_EMPREGADO FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEMPREGADO_FUNCAO WHERE nID_EMPREGADO_FUNCAO IN ");
            str.Append("(SELECT nID_EMPREGADO FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEMPREGADO_FUNCAO WHERE hDT_Termino is null and nID_EMPREGADO_FUNCAO IN ");
            str.Append("(SELECT nID_EMPREGADO_FUNCAO FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblFUNC_EMPREGADO WHERE nID_LAUD_TEC=" + this.IdLaudoTecnico.Id + "))");
            str.Append(" AND nIND_BENEFICIARIO IS NOT NULL");
            str.Append(" AND nIND_BENEFICIARIO IN (" + (int)TipoBeneficiario.BeneficiarioReabilitado + "," + (int)TipoBeneficiario.PortadorDeficiencia + ")");

            int count = new Empregado().ExecuteCount(str.ToString());

            return count;
        }
        #endregion

        #region GetNumFuncionariosAfastados

        public int GetNumFuncionariosAfastados()
        {
            StringBuilder str = new StringBuilder();

            str.Append(" nID_EMPREGADO IN ");
            str.Append("(SELECT nID_EMPREGADO FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEMPREGADO_FUNCAO WHERE nID_EMPREGADO_FUNCAO IN ");            
            str.Append("(SELECT nID_EMPREGADO_FUNCAO FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblFUNC_EMPREGADO WHERE nID_LAUD_TEC=" + this.IdLaudoTecnico.Id + "))");
            str.Append(" AND nID_EMPREGADO IN");
            str.Append(" (SELECT IdEmpregado FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.Afastamento WHERE DataVolta IS NULL)");

            int count = new Empregado().ExecuteCount(str.ToString());

            return count;
        }
        #endregion

        #region GetNumFuncionariosAdmitidos

        public int GetNumFuncionariosAdmitidos(int IdGhe)
        {
            StringBuilder str = new StringBuilder();

            str.Append(" nID_EMPREGADO IN (SELECT nID_EMPREGADO FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEMPREGADO_FUNCAO WHERE nID_EMPR = " + this.IdCliente.Id + ")");
            str.Append(" AND hDT_ADM>='" + this.DataPcmso.ToString("yyyy-MM-dd") + "'");
            str.Append(" AND hDT_ADM<='" + this.GetTerninoPcmso().ToString("yyyy-MM-dd") + "'");
            str.Append(" AND gTERCEIRO=0");
            str.Append(" AND nID_EMPREGADO IN (SELECT nID_EMPREGADO FROM tblEMPREGADO_FUNCAO WHERE");
            str.Append(" nID_EMPREGADO_FUNCAO IN (SELECT nID_EMPREGADO_FUNCAO FROM dbo.tblFUNC_EMPREGADO WHERE nID_FUNC=" + IdGhe + "))");

            int count = new Empregado().ExecuteCount(str.ToString());

            return count;
        }
        #endregion

        #region GetNumFuncionariosDemitidos

        public int GetNumFuncionariosDemitidos(int IdGhe)
        {
            StringBuilder str = new StringBuilder();

            str.Append(" nID_EMPREGADO IN (SELECT nID_EMPREGADO FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEMPREGADO_FUNCAO WHERE nID_EMPR = " + this.IdCliente.Id + ")");
            str.Append(" AND hDT_DEM>='" + this.DataPcmso.ToString("yyyy-MM-dd") + "'");
            str.Append(" AND hDT_DEM<='" + this.GetTerninoPcmso().ToString("yyyy-MM-dd") + "'");
            str.Append(" AND gTERCEIRO=0");
            str.Append(" AND nID_EMPREGADO IN (SELECT nID_EMPREGADO FROM tblEMPREGADO_FUNCAO WHERE");
            str.Append(" nID_EMPREGADO_FUNCAO IN (SELECT nID_EMPREGADO_FUNCAO FROM dbo.tblFUNC_EMPREGADO WHERE nID_FUNC=" + IdGhe + "))");

            int count = new Empregado().ExecuteCount(str.ToString());

            return count;
        }
        #endregion

        #region GetNumeroPeriodicoPendetes
        //public int GetNumeroPeriodicoPendetes()
        //{
        //    int count;

        //    string strWhere = "IdExameDicionario=" + (int)IndExameClinico.Periodico
        //        + " AND DataExame BETWEEN '" + this.DataPcmso.ToString("yyyy-MM-dd")
        //        + "' AND '" + this.GetTerninoPcmso().ToString("yyyy-MM-dd") + "'"
        //        + " AND IndResultado=" + (int)ResultadoExame.EmEspera
        //        + " AND IdEmpregado IN (SELECT nID_EMPREGADO FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEMPREGADO WHERE"
        //        + " nID_EMPREGADO IN (SELECT nID_EMPREGADO FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEMPREGADO_FUNCAO WHERE nID_EMPR=" + this.IdCliente.Id + "))";

        //    count = new ExameBase().ExecuteCount(strWhere);

        //    return count;
        //}
        #endregion

        #region GetNumeroPeriodicosEmEspera

        public int GetNumeroPeriodicosEmEspera()
        {
            int count;

            string strWhere = "IdExameDicionario=" + (int)IndExameClinico.Periodico
                + " AND DataExame BETWEEN '" + this.DataPcmso.ToString("yyyy-MM-dd")
                + "' AND '" + this.GetTerninoPcmso().ToString("yyyy-MM-dd") + "'"
                + " AND IndResultado=" + (int)ResultadoExame.EmEspera
                + " AND IdEmpregado IN (SELECT nID_EMPREGADO FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEMPREGADO WHERE"
                + " nID_EMPREGADO IN (SELECT nID_EMPREGADO FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEMPREGADO_FUNCAO WHERE nID_EMPR=" + this.IdCliente.Id + "))";

            count = new ExameBase().ExecuteCount(strWhere);

            return count;
        }
        #endregion

        #region GetNumeroPeriodicosRealizados

        public int GetNumeroPeriodicosRealizados()
        {
            int count;

            string strWhere = "IdExameDicionario=" + (int)IndExameClinico.Periodico
                + " AND DataExame BETWEEN '" + this.DataPcmso.ToString("yyyy-MM-dd")
                + "' AND '" + this.GetTerninoPcmso().ToString("yyyy-MM-dd") + "'"
                + " AND IndResultado<>" + (int)ResultadoExame.NaoRealizado
                + " AND IndResultado<>" + (int)ResultadoExame.EmEspera
                + " AND IdEmpregado IN (SELECT nID_EMPREGADO FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEMPREGADO WHERE"
                + " nID_EMPREGADO IN (SELECT nID_EMPREGADO FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEMPREGADO_FUNCAO WHERE nID_EMPR=" + this.IdCliente.Id + "))";

            count = new ExameBase().ExecuteCount(strWhere);

            return count;
        }
        #endregion

        #region GetNumeroPeriodicosPendentes

        public int GetNumeroPeriodicosPendentes(DateTime dataProximo)
        {
            int count = 0;

            try
            {
                string strWhere = "IdExameDicionario=" + (int)IndExameClinico.Periodico
                                + " AND IdPcmsoPlanejamento IN (SELECT IdPcmsoPlanejamento"
                                + " FROM PcmsoPlanejamento WHERE IdPcmso = " + this.Id + ")"
                                + " AND DataProxima <='" + dataProximo.ToString("yyyy-MM-dd") + " 23:59:59.999'";

                count = new ExamePlanejamento().ExecuteCount(strWhere);
            }
            catch { }

            return count;
        }
        #endregion

        #endregion

        #region GetDataProximoPeriodico

        public DateTime GetDataProximoPeriodico()
        {
            DateTime dataProxima = new DateTime();

            if (this.Id == 0)
                return dataProxima;

            if (this.IdCliente.mirrorOld == null)
                this.IdCliente.Find();
            
            //Não tem data próxima pois a atualização vai ser por vencimento do exame
            if (this.IdCliente.IndAtualizacaoPeriodico == Cliente.AtualizacaoPeriodico.PorEmpregado)
            {
                dataProxima = new DateTime();
            }
            else if (this.IsInformarProximoPeriodico)
            {
                dataProxima = this.ProximoPeriodico;
            }
            else
            {
                dataProxima = this.IdCliente.GetDataVencimentoPeriodico();

                //Se está atrasado joga para o último dia do mês
                if (dataProxima < DateTime.Today)
                    dataProxima = Utility.UltimoDiaMes(DateTime.Today);
            }

            return new DateTime(dataProxima.Year, dataProxima.Month, dataProxima.Day);
        }
        #endregion

        #region GerarExamePlanejamento

        public void GerarExamePlanejamento()
        {
            //Limpa o banco de dados antes de gerar os exames
            new Pcmso().ExecuteDataset("DELETE PcmsoPlanejamento WHERE IdGhe IS NOT NULL AND IdGhe NOT IN (SELECT nID_FUNC FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblFUNC WHERE nID_LAUD_TEC IN (SELECT IdLaudoTecnico FROM Pcmso))");
            new Pcmso().ExecuteDataset("DELETE PcmsoGhe WHERE IdGhe IS NOT NULL AND IdGhe NOT IN (SELECT nID_FUNC FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblFUNC WHERE nID_LAUD_TEC IN (SELECT IdLaudoTecnico FROM Pcmso))");
            new Pcmso().ExecuteDataset("DELETE ExamePlanejamento WHERE IdPcmso=" + this.Id);

            if (this.IdCliente.IsInativo)
                return;

            if (this.IdCliente.mirrorOld == null)
                this.IdCliente.Find();

            DateTime dataProxima = this.GetDataProximoPeriodico();

            //Cria os exames
            StringBuilder criteria = new StringBuilder();

            criteria.Append("IdPcmso=" + this.Id);
            criteria.Append(" AND IdGHE IN (SELECT nID_FUNC FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblFUNC WHERE nID_LAUD_TEC = " + this.IdLaudoTecnico.Id + ")");
            criteria.Append(" ORDER BY (SELECT tNO_FUNC FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblFUNC WHERE nID_FUNC = IdGhe)");

            List<PcmsoGhe> pcmsoGhes = new PcmsoGhe().Find<PcmsoGhe>(criteria.ToString());

            Pedido ultimoPedido = Pedido.GetUltimoPedido((int)Obrigacoes.ExamesPeriodicos, 
                                                        this.IdCliente.Id);

            foreach (PcmsoGhe pcmsoGhe in pcmsoGhes)
            {
                Ghe ghe = new Ghe();
                ghe.Find(pcmsoGhe.IdGhe.Id);

                bool bPossueRisco = ghe.DoseUltrapassadaPcmso();

                List<PcmsoPlanejamento> listPcmsoPlanejamento
                    = new PcmsoPlanejamento().Find<PcmsoPlanejamento>("IdPcmso=" + this.Id
                                                                    + " AND IdGhe=" + ghe.Id
                                                                    + " AND Periodico=1");
                List<Empregado> empregados;

                //Planserve - Com Funcionarios Inativos
                if (this.TerminoPcmso != new DateTime())
                    empregados = ghe.GetEmpregadosExpostos(false, false);
                else
                    empregados = ghe.GetEmpregadosExpostos(true, false);

                int OrdinalPosition = 1;

                if (ProgressIniciar != null)
                    ProgressIniciar(ghe.tNO_FUNC, empregados.Count * listPcmsoPlanejamento.Count);

                foreach (PcmsoPlanejamento pcmsoPlanejamento in listPcmsoPlanejamento)
                {
                    ExameDicionario exameDicionario = new ExameDicionario();
                    exameDicionario.Find(pcmsoPlanejamento.IdExameDicionario.Id);

                    foreach (Empregado empregado in empregados)
                    {
                        empregado.GerarExamePlanejamento(this,
                                                         pcmsoPlanejamento,
                                                         exameDicionario,
                                                         ultimoPedido,
                                                         dataProxima,
                                                         bPossueRisco, "N");

                        if (ProgressAtualizar != null)
                            ProgressAtualizar(OrdinalPosition++);
                    }
                }
            }
        }



        //rodar planejamento de exames específico para empregado, após salvar exame complementar ou clínico
        public void GerarExamePlanejamento(Int32 xIdEmpregado)
        {

            Int32 xIdPCMSO = 0;
            Int32 xIdGHE = 0;
            Int32 xIdCliente = 0;

            Ilitera.Data.PPRA_EPI xValores = new Ilitera.Data.PPRA_EPI();
            DataSet rDs = xValores.Trazer_PCMSO_GHE_Empregado(xIdEmpregado);

            if (rDs.Tables[0].Rows.Count == 0) return;

            xIdPCMSO = System.Convert.ToInt32(rDs.Tables[0].Rows[0]["IdPCMSO"].ToString());
            xIdGHE = System.Convert.ToInt32(rDs.Tables[0].Rows[0]["IdGHE"].ToString());
            xIdCliente = System.Convert.ToInt32(rDs.Tables[0].Rows[0]["IdCliente"].ToString());


            //Limpa o banco de dados antes de gerar os exames
            new Pcmso().ExecuteDataset("DELETE ExamePlanejamento WHERE IdPcmso=" + xIdPCMSO.ToString() + " and IdEmpregado = " + xIdEmpregado.ToString());

            this.Find(xIdPCMSO);

            if (this.IdCliente.IsInativo)
                return;

            if (this.IdCliente.mirrorOld == null)
                this.IdCliente.Find();

            DateTime dataProxima = this.GetDataProximoPeriodico();

            //Cria os exames
            StringBuilder criteria = new StringBuilder();

            criteria.Append("IdPcmso=" + xIdPCMSO.ToString() + " ");
            criteria.Append(" AND IdGHE IN ( " + xIdGHE.ToString() + "  ) ");
            criteria.Append(" ORDER BY (SELECT tNO_FUNC FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblFUNC WHERE nID_FUNC = IdGhe)");

            List<PcmsoGhe> pcmsoGhes = new PcmsoGhe().Find<PcmsoGhe>(criteria.ToString());

            Pedido ultimoPedido = Pedido.GetUltimoPedido((int)Obrigacoes.ExamesPeriodicos,
                                                        this.IdCliente.Id);


            //não está localizando pcmsoGHE - problema está no IdGHE
            foreach (PcmsoGhe pcmsoGhe in pcmsoGhes)
            {
                Ghe ghe = new Ghe();
                ghe.Find(pcmsoGhe.IdGhe.Id);

                bool bPossueRisco = ghe.DoseUltrapassadaPcmso();

                List<PcmsoPlanejamento> listPcmsoPlanejamento
                    = new PcmsoPlanejamento().Find<PcmsoPlanejamento>("IdPcmso=" + xIdPCMSO.ToString()
                                                                    + " AND IdGhe=" + xIdGHE.ToString()
                                                                    + " AND Periodico=1");
                List<Empregado> empregados;

                //Planserve - Com Funcionarios Inativos
                //if (this.TerminoPcmso != new DateTime())
                //    empregados = ghe.GetEmpregadosExpostos(false, false);
                //else
                //empregados = ghe.GetEmpregadosExpostos(true, false);

                pcmsoGhe.Find();
                pcmsoGhe.IdPcmso.Find();
                pcmsoGhe.IdPcmso.IdCliente.Find();

                if (pcmsoGhe.IdPcmso.IdCliente.Desconsiderar_Afastados_Planejamento == true)
                    empregados = ghe.GetEmpregadosExpostos(true, true);
                else
                    empregados = ghe.GetEmpregadosExpostos(true, false);

                int OrdinalPosition = 1;

                if (ProgressIniciar != null)
                    ProgressIniciar(ghe.tNO_FUNC, empregados.Count * listPcmsoPlanejamento.Count);

                foreach (PcmsoPlanejamento pcmsoPlanejamento in listPcmsoPlanejamento)
                {
                    ExameDicionario exameDicionario = new ExameDicionario();
                    exameDicionario.Find(pcmsoPlanejamento.IdExameDicionario.Id);

                    if (exameDicionario.Nome.ToUpper().IndexOf("AVALIAÇÃO CL") < 0)
                    {

                        foreach (Empregado empregado in empregados)
                        {

                            if (empregado.Id == xIdEmpregado)
                            {

                                //se afastado, deixar sem data de proximo exame,
                                //e ter indicação no módulo de pendências 

                                string zConsiderar_Mudanca_Funcao = "S";

                                //if (Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper().IndexOf("PRAJNA") > 0) zConsiderar_Mudanca_Funcao = "S"; 

                                empregado.GerarExamePlanejamento(this,
                                                                 pcmsoPlanejamento,
                                                                 exameDicionario,
                                                                 ultimoPedido,
                                                                 dataProxima,
                                                                 bPossueRisco,
                                                                 zConsiderar_Mudanca_Funcao);

                                if (ProgressAtualizar != null)
                                    ProgressAtualizar(OrdinalPosition++);







                                //ver aptidoes desse colaborador
                                try
                                {

                                    GHE_Aptidao zAptidao = new GHE_Aptidao();
                                    zAptidao.Find(" nId_Func = " + pcmsoGhe.IdGhe.Id.ToString());


                                    Empregado_Aptidao xAptidao = new Empregado_Aptidao();
                                    xAptidao.Find(" nId_Empregado = " + empregado.Id.ToString());


                                    if (xAptidao.Id != 0 || zAptidao.Id != 0)
                                    {
                                        if ((xAptidao.apt_Alimento == true || xAptidao.apt_Aquaviario == true || xAptidao.apt_Eletricidade == true || xAptidao.apt_Espaco_Confinado == true ||
                                             xAptidao.apt_Submerso == true || xAptidao.apt_Trabalho_Altura == true || xAptidao.apt_Transporte == true || xAptidao.apt_Brigadista == true || xAptidao.apt_Socorrista == true || xAptidao.apt_PPR == true) ||
                                             (zAptidao.apt_Alimento == true || zAptidao.apt_Aquaviario == true || zAptidao.apt_Eletricidade == true || zAptidao.apt_Espaco_Confinado == true ||
                                            zAptidao.apt_Submerso == true || zAptidao.apt_Trabalho_Altura == true || zAptidao.apt_Transporte == true || zAptidao.apt_Brigadista == true || zAptidao.apt_Socorrista == true || zAptidao.apt_PPR == true))
                                        {

                                            Empregado_Aptidao nAptidao = new Empregado_Aptidao();

                                            //juntando aptidao do empregado com do PPRA-GHE
                                            if (xAptidao.Id != 0 && zAptidao.Id != 0)
                                            {
                                                nAptidao.apt_Alimento = xAptidao.apt_Alimento || zAptidao.apt_Alimento;
                                                nAptidao.apt_Aquaviario = xAptidao.apt_Aquaviario || zAptidao.apt_Aquaviario;
                                                nAptidao.apt_Brigadista = xAptidao.apt_Brigadista || zAptidao.apt_Brigadista;
                                                nAptidao.apt_Eletricidade = xAptidao.apt_Eletricidade || zAptidao.apt_Eletricidade;
                                                nAptidao.apt_Espaco_Confinado = xAptidao.apt_Espaco_Confinado || zAptidao.apt_Espaco_Confinado;
                                                nAptidao.apt_Socorrista = xAptidao.apt_Socorrista || zAptidao.apt_Socorrista;
                                                nAptidao.apt_Submerso = xAptidao.apt_Submerso || zAptidao.apt_Submerso;
                                                nAptidao.apt_Trabalho_Altura = xAptidao.apt_Trabalho_Altura || zAptidao.apt_Trabalho_Altura;
                                                nAptidao.apt_Transporte = xAptidao.apt_Transporte || zAptidao.apt_Transporte;
                                                nAptidao.apt_PPR = xAptidao.apt_PPR || zAptidao.apt_PPR;
                                            }
                                            else if (xAptidao.Id != 0)
                                            {
                                                nAptidao.apt_Alimento = xAptidao.apt_Alimento;
                                                nAptidao.apt_Aquaviario = xAptidao.apt_Aquaviario;
                                                nAptidao.apt_Brigadista = xAptidao.apt_Brigadista;
                                                nAptidao.apt_Eletricidade = xAptidao.apt_Eletricidade;
                                                nAptidao.apt_Espaco_Confinado = xAptidao.apt_Espaco_Confinado;
                                                nAptidao.apt_Socorrista = xAptidao.apt_Socorrista;
                                                nAptidao.apt_Submerso = xAptidao.apt_Submerso;
                                                nAptidao.apt_Trabalho_Altura = xAptidao.apt_Trabalho_Altura;
                                                nAptidao.apt_Transporte = xAptidao.apt_Transporte;
                                                nAptidao.apt_PPR = xAptidao.apt_PPR;
                                            }
                                            else if (zAptidao.Id != 0)
                                            {
                                                nAptidao.apt_Alimento = zAptidao.apt_Alimento;
                                                nAptidao.apt_Aquaviario = zAptidao.apt_Aquaviario;
                                                nAptidao.apt_Brigadista = zAptidao.apt_Brigadista;
                                                nAptidao.apt_Eletricidade = zAptidao.apt_Eletricidade;
                                                nAptidao.apt_Espaco_Confinado = zAptidao.apt_Espaco_Confinado;
                                                nAptidao.apt_Socorrista = zAptidao.apt_Socorrista;
                                                nAptidao.apt_Submerso = zAptidao.apt_Submerso;
                                                nAptidao.apt_Trabalho_Altura = zAptidao.apt_Trabalho_Altura;
                                                nAptidao.apt_Transporte = zAptidao.apt_Transporte;
                                                nAptidao.apt_PPR = zAptidao.apt_PPR;
                                            }

                                            Clinico clinico = new Clinico();
                                            clinico.IdPcmso = pcmsoGhe.IdPcmso;
                                            clinico.IdEmpregado = empregado;
                                            clinico.IdEmpregadoFuncao = EmpregadoFuncao.GetEmpregadoFuncao(empregado);

                                            ConvocacaoExame zConv = new ConvocacaoExame();
                                            zConv.Id = 0;
                                            clinico.IdConvocacaoExame = zConv;

                                            ExameDicionario zExame = new ExameDicionario();
                                            zExame.Find(" IdExameDicionario = 4");
                                            clinico.IdExameDicionario = zExame;

                                            //gerar planejamento dos exames em cima do nAptidao
                                            clinico.complementares = clinico.ExamesComplementares_Aptidao(nAptidao);


                                            zConsiderar_Mudanca_Funcao = "S";


                                            foreach (Ilitera.Opsa.Data.Clinico.ExameComplementar complementar in clinico.complementares)
                                            {
                                                PcmsoPlanejamento rPlan = new PcmsoPlanejamento();

                                                rPlan.Find(" IdPCMSO = " + pcmsoGhe.IdPcmso.Id.ToString() + " and IdGHE = " + pcmsoGhe.IdGhe.Id.ToString() + " and IdExameDicionario = " + complementar.empr_apt_Planejamento.IdExameDicionario.Id.ToString() + " ");


                                                //se planejamento para esse exame já existe, vai valer o que estava no PCMSO
                                                //if (rPlan.Id != 0 )  //&& rPlan.IndPeriodicidade != complementar.empr_apt_Planejamento.IndPeriodicidade && rPlan.Intervalo != complementar.empr_apt_Planejamento.Intervalo)
                                                // {

                                                //}


                                                if (rPlan.Id == 0 && complementar.empr_apt_Planejamento.IdExameDicionario != null)
                                                {
                                                    //criar planejamento temporário
                                                    rPlan = new PcmsoPlanejamento();
                                                    rPlan.IdPcmso = pcmsoGhe.IdPcmso;
                                                    rPlan.IdExameDicionario = complementar.empr_apt_Planejamento.IdExameDicionario;
                                                    rPlan.IdGHE = pcmsoGhe.IdGhe;
                                                    rPlan.IndPeriodicidade = complementar.empr_apt_Planejamento.IndPeriodicidade;
                                                    rPlan.IndPeriodicidadeAposAdmissao = complementar.empr_apt_Planejamento.IndPeriodicidadeAposAdmissao;
                                                    rPlan.Intervalo = complementar.empr_apt_Planejamento.Intervalo;
                                                    rPlan.IntervaloAposAdmissao = complementar.empr_apt_Planejamento.IntervaloAposAdmissao;
                                                    rPlan.NaAdmissao = complementar.empr_apt_Planejamento.NaAdmissao;
                                                    rPlan.NaDemissao = complementar.empr_apt_Planejamento.NaDemissao;
                                                    rPlan.NaMudancaFuncao = complementar.empr_apt_Planejamento.NaMudancaFuncao;
                                                    rPlan.NoRetornoTrabalho = complementar.empr_apt_Planejamento.NoRetornoTrabalho;
                                                    rPlan.Periodico = complementar.empr_apt_Planejamento.Periodico;
                                                    rPlan.DependeIdade = complementar.empr_apt_Planejamento.DependeIdade;
                                                    rPlan.Preventivo = false;
                                                    rPlan.Temporario = true;
                                                    rPlan.Save();

                                                    //planejamento por idade temporário
                                                    if (complementar.empr_apt_Planejamento.DependeIdade == true)
                                                    {

                                                        ArrayList list = new Empregado_Aptidao_Planejamento_Idade().Find("nIdPlanejamento = " + complementar.empr_apt_Planejamento.Id.ToString());

                                                        foreach (Empregado_Aptidao_Planejamento_Idade rPlanIdade in list)
                                                        {
                                                            PcmsoPlanejamentoIdade pcmsoPlanejamentoIdadeNew = new PcmsoPlanejamentoIdade();
                                                            pcmsoPlanejamentoIdadeNew.Inicialize();
                                                            pcmsoPlanejamentoIdadeNew.IdPcmsoPlanejamento = rPlan;
                                                            pcmsoPlanejamentoIdadeNew.IndPeriodicidade = rPlanIdade.IndPeriodicidade;
                                                            pcmsoPlanejamentoIdadeNew.Intervalo = rPlanIdade.Intervalo;
                                                            pcmsoPlanejamentoIdadeNew.IndSexoIdade = rPlanIdade.IndSexoIdade;
                                                            pcmsoPlanejamentoIdadeNew.AnoInicio = rPlanIdade.AnoInicio;
                                                            pcmsoPlanejamentoIdadeNew.AnoTermino = rPlanIdade.AnoTermino;
                                                            pcmsoPlanejamentoIdadeNew.Temporario = true;
                                                            pcmsoPlanejamentoIdadeNew.Save();
                                                        }



                                                    }




                                                    empregado.GerarExamePlanejamento(this,
                                                                                 rPlan,
                                                                                 complementar.empr_apt_Planejamento.IdExameDicionario,
                                                                                 ultimoPedido,
                                                                                 dataProxima,
                                                                                 bPossueRisco,
                                                                                 zConsiderar_Mudanca_Funcao);



                                                    //excluir planejamentos por idade e planejamento temporários criados apenas para gerar os registros de planejamento de exames.

                                                    ArrayList list2 = new PcmsoPlanejamentoIdade().Find("IdPCMSOPlanejamento = " + rPlan.Id);

                                                    foreach (PcmsoPlanejamentoIdade rPlanIdade2 in list2)
                                                    {
                                                        rPlanIdade2.Delete();
                                                    }

                                                    rPlan.Delete();

                                                }



                                            }

                                        }
                                    }


                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex.Message);
                                }
                                finally
                                {

                                }





                            }

                        }




                    }
                }


            }





        }



        #endregion

        #region GerarExamePlanejamentoTodasEmpresas

        //Metodo usado no MestraService
        public static void GerarExamePlanejamentoTodasEmpresas()
        {
            string str = "ContrataPCMSO IN ("
                        + (int)TipoPcmsoContratada.Contratada  + ", "   
                        + (int)TipoPcmsoContratada.SomenteSistema + ")"
                        + " AND IdCliente IN (SELECT IdCliente FROM qryClienteAtivos)"
                        + " AND IdCliente IN (SELECT IdCliente FROM Documento WHERE IdDocumentoBase=" + (int)Documentos.PCMSO
                        + " AND IdPedido IS NOT NULL AND IdPedido IN (SELECT IdPedido FROM Pedido WHERE IndStatus=" + (int)StatusPedidos.Executado + "))"
                        + " ORDER BY NomeAbreviado";

            //ArrayList listCliente = new Cliente().Find(str);

            List<Cliente> listCliente = new Cliente().Find<Cliente>(str);

            foreach (Cliente cliente in listCliente)
            {
                try
                {
                    Pcmso pcmso = cliente.GetUltimoPcmso();

                    if (pcmso.Id != 0)
                    {
                        try
                        {
                            pcmso.GerarExamePlanejamento();
                        }
                        catch (Exception ex)
                        {
                            System.Diagnostics.Trace.WriteLine(ex.Message);
                        }
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Trace.WriteLine(ex.Message);
                }
            }
        }
        #endregion


        public ArrayList GerarCronogramaPadrao()
        {
            ArrayList list = new ArrayList();

            //Padrão 01            
            CronogramaPCMSO cronogramaPCMSO1 = new CronogramaPCMSO();
            cronogramaPCMSO1.Inicialize();
            cronogramaPCMSO1.IdPcmso = this;
            cronogramaPCMSO1.Prazo = this.DataPcmso.AddMonths(1);
            cronogramaPCMSO1.PlanejamentoAnual = "1) Renovação do PCMSO.";
            cronogramaPCMSO1.MetodologiaAcao = "Solicitar o médico responsável pelo PCMSO a renovação do documento, mediante a reavaliação das condições dos ambientes do trabalho apresentadas na renovação do PGR.";
            cronogramaPCMSO1.FormaRegistro = "PCMSO";
            //cronogramaPCMSO1.PopularPrazo();
            cronogramaPCMSO1.Mes10 = true;
            cronogramaPCMSO1.Mes11 = true;
            cronogramaPCMSO1.Ano = this.DataPcmso.Year.ToString();
            cronogramaPCMSO1.Save();
            list.Add(cronogramaPCMSO1);

            //Padrão 02
            CronogramaPCMSO cronogramaPCMSO2 = new CronogramaPCMSO();
            cronogramaPCMSO2.Inicialize();
            cronogramaPCMSO2.IdPcmso = this;
            cronogramaPCMSO2.Prazo = this.DataPcmso.AddYears(1);
            cronogramaPCMSO2.PlanejamentoAnual = "2) Emissão de Relatório Analítico.";
            cronogramaPCMSO2.MetodologiaAcao = "Elaborar relatório analítico, contendo, no mínimo: o número de exames clínicos realizados, o número e tipos de exames complementares realizados, estatística de resultados anormais dos exames complementares, categorizados por tipo do exame e por unidade operacional, setor ou função, incidência e prevalência de doenças relacionadas ao trabalho, categorizadas por unidade operacional, setor ou função, informações sobre o número, tipo de eventos e doenças informadas nas CAT, emitidas pela organização, referentes a seus empregados e análise comparativa em relação ao relatório anterior e discussão sobre as variações nos resultados.";
            cronogramaPCMSO2.FormaRegistro = "Relatório Anual de Exames";
            //cronogramaPCMSO2.PopularPrazo();
            cronogramaPCMSO2.Mes10 = true;
            cronogramaPCMSO2.Mes11 = true;
            cronogramaPCMSO2.Ano = this.DataPcmso.Year.ToString();
            cronogramaPCMSO2.Save();
            list.Add(cronogramaPCMSO2);

            //Padrão 03
            CronogramaPCMSO cronogramaPCMSO3 = new CronogramaPCMSO();
            cronogramaPCMSO3.Inicialize();
            cronogramaPCMSO3.IdPcmso = this;
            cronogramaPCMSO3.Prazo = this.DataPcmso.AddMonths(1);
            cronogramaPCMSO3.PlanejamentoAnual = "3) Realização de Exames.";
            cronogramaPCMSO3.MetodologiaAcao = "Os exames médicos devem seguir as diretrizes deste PCMSO e serem realizados na admissão, anualmente ou a cada dois anos, no retorno ao trabalho, na mudança de riscos ocupacionais e na demissão, quando aplicável.";
            cronogramaPCMSO3.FormaRegistro = "PMI - Prontuário Médico Individual, composto por ficha clínica, ASO - Atestado de Saúde Ocupacional e resultados dos exames complementares.";
            cronogramaPCMSO3.Mes12 = true;
            cronogramaPCMSO3.Mes01 = true;
            cronogramaPCMSO3.Ano = this.DataPcmso.Year + " e " + this.DataPcmso.AddYears(1).Year;
            cronogramaPCMSO3.Save();
            list.Add(cronogramaPCMSO3);

            ////Padrão 04
            //CronogramaPCMSO cronogramaPCMSO4 = new CronogramaPCMSO();
            //cronogramaPCMSO4.Inicialize();
            //cronogramaPCMSO4.IdPcmso = this;
            //cronogramaPCMSO4.Prazo = this.DataPcmso.AddMonths(1);
            //cronogramaPCMSO4.PlanejamentoAnual = "4) Semana Interna de Prevenção de Acidentes - SIPAT.";
            //cronogramaPCMSO4.MetodologiaAcao = "";
            //cronogramaPCMSO4.FormaRegistro = "";
            //cronogramaPCMSO4.Mes04 = true;
            //cronogramaPCMSO4.Mes05 = true;
            //cronogramaPCMSO4.Ano = this.DataPcmso.AddYears(1).Year.ToString();
            //cronogramaPCMSO4.Save();
            //list.Add(cronogramaPCMSO4);
            return list;
        }


        public ArrayList GerarCronogramaPadraoPCMSOAnt(Pcmso rpcmso)
        {
            //ArrayList list = new ArrayList();

            Pcmso xPcmsoAnt = new Pcmso();
            System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");

            rpcmso.IdCliente.Find();
            ArrayList list = new Pcmso().Find("IdCliente=" + rpcmso.IdCliente.Id + " and DataPCMSO < convert( smalldatetime, '" + rpcmso.DataPcmso.ToString("dd/MM/yyyy", ptBr) + "', 103 )  ORDER BY DataPcmso DESC");

            if (list.Count > 0)
            {

                foreach (Pcmso p in list)
                {
                    if (p != null)
                    {
                        xPcmsoAnt = p;
                        ArrayList list2 = CronogramaPCMSO.GetCronograma(xPcmsoAnt);

                        if (list2.Count > 0)
                        {

                            for (int zCont = 0; zCont < list2.Count; zCont++)
                            {

                                CronogramaPCMSO cronogramaPCMSO1 = new CronogramaPCMSO();
                                cronogramaPCMSO1.Inicialize();
                                cronogramaPCMSO1.IdPcmso = rpcmso;
                                cronogramaPCMSO1.Prazo = rpcmso.DataPcmso.AddMonths(1);
                                //object zRow = list2[zCont];
                                cronogramaPCMSO1.PlanejamentoAnual = ((CronogramaPCMSO)list2[zCont]).PlanejamentoAnual;
                                cronogramaPCMSO1.MetodologiaAcao = ((CronogramaPCMSO)list2[zCont]).MetodologiaAcao;
                                cronogramaPCMSO1.FormaRegistro = ((CronogramaPCMSO)list2[zCont]).FormaRegistro;
                                //cronogramaPCMSO1.PopularPrazo();
                                //cronogramaPCMSO1.Mes10 = true;
                                //cronogramaPCMSO1.Mes11 = true;
                                cronogramaPCMSO1.Ano = rpcmso.DataPcmso.Year.ToString();
                                cronogramaPCMSO1.Save();
                                list.Add(cronogramaPCMSO1);

                            }

                            break;
                        }
                        else
                        {

                            //Padrão 01            
                            CronogramaPCMSO cronogramaPCMSO1 = new CronogramaPCMSO();
                            cronogramaPCMSO1.Inicialize();
                            cronogramaPCMSO1.IdPcmso = this;
                            cronogramaPCMSO1.Prazo = this.DataPcmso.AddMonths(1);
                            cronogramaPCMSO1.PlanejamentoAnual = "1) Renovação do PCMSO.";
                            cronogramaPCMSO1.MetodologiaAcao = "Solicitar o médico coordenador do PCMSO a renovação do documento, mediante a reavaliação das condições dos ambientes do trabalho apresentadas na renovação do PPRA.";
                            cronogramaPCMSO1.FormaRegistro = "PCMSO";
                            //cronogramaPCMSO1.PopularPrazo();
                            cronogramaPCMSO1.Mes10 = true;
                            cronogramaPCMSO1.Mes11 = true;
                            cronogramaPCMSO1.Ano = this.DataPcmso.Year.ToString();
                            cronogramaPCMSO1.Save();
                            list.Add(cronogramaPCMSO1);

                            //Padrão 02
                            CronogramaPCMSO cronogramaPCMSO2 = new CronogramaPCMSO();
                            cronogramaPCMSO2.Inicialize();
                            cronogramaPCMSO2.IdPcmso = this;
                            cronogramaPCMSO2.Prazo = this.DataPcmso.AddYears(1);
                            cronogramaPCMSO2.PlanejamentoAnual = "2) Emissão de Relatório Anual.";
                            cronogramaPCMSO2.MetodologiaAcao = "Emitir o Relatório Anual de Exames contendo o número de exames realizados, a natureza dos exames, o número de exames que apresentaram anormalidades e o número de exames previstos para o ano seguinte.";
                            cronogramaPCMSO2.FormaRegistro = "Relatório Anual de Exames";
                            //cronogramaPCMSO2.PopularPrazo();
                            cronogramaPCMSO2.Mes10 = true;
                            cronogramaPCMSO2.Mes11 = true;
                            cronogramaPCMSO2.Ano = this.DataPcmso.Year.ToString();
                            cronogramaPCMSO2.Save();
                            list.Add(cronogramaPCMSO2);

                            //Padrão 03
                            CronogramaPCMSO cronogramaPCMSO3 = new CronogramaPCMSO();
                            cronogramaPCMSO3.Inicialize();
                            cronogramaPCMSO3.IdPcmso = this;
                            cronogramaPCMSO3.Prazo = this.DataPcmso.AddMonths(1);
                            cronogramaPCMSO3.PlanejamentoAnual = "3) Realização de Exames Clínicos.";
                            cronogramaPCMSO3.MetodologiaAcao = "Os exames clínicos devem ser realizados na admissão, anualmente ou bianualmente, no retorno ao trabalho, na mudança de função e na demissão, segundo as diretrizes do PCMSO.";
                            cronogramaPCMSO3.FormaRegistro = "ASO - Atestado de Saúde Ocupacional";
                            cronogramaPCMSO3.Mes12 = true;
                            cronogramaPCMSO3.Mes01 = true;
                            cronogramaPCMSO3.Ano = this.DataPcmso.Year + " e " + this.DataPcmso.AddYears(1).Year;
                            cronogramaPCMSO3.Save();
                            list.Add(cronogramaPCMSO3);
                            break;
                        }
                    }
                }
            }
            else
            {
                //Padrão 01            
                CronogramaPCMSO cronogramaPCMSO1 = new CronogramaPCMSO();
                cronogramaPCMSO1.Inicialize();
                cronogramaPCMSO1.IdPcmso = this;
                cronogramaPCMSO1.Prazo = this.DataPcmso.AddMonths(1);
                cronogramaPCMSO1.PlanejamentoAnual = "1) Renovação do PCMSO.";
                cronogramaPCMSO1.MetodologiaAcao = "Solicitar o médico coordenador do PCMSO a renovação do documento, mediante a reavaliação das condições dos ambientes do trabalho apresentadas na renovação do PPRA.";
                cronogramaPCMSO1.FormaRegistro = "PCMSO";
                //cronogramaPCMSO1.PopularPrazo();
                cronogramaPCMSO1.Mes10 = true;
                cronogramaPCMSO1.Mes11 = true;
                cronogramaPCMSO1.Ano = this.DataPcmso.Year.ToString();
                cronogramaPCMSO1.Save();
                list.Add(cronogramaPCMSO1);

                //Padrão 02
                CronogramaPCMSO cronogramaPCMSO2 = new CronogramaPCMSO();
                cronogramaPCMSO2.Inicialize();
                cronogramaPCMSO2.IdPcmso = this;
                cronogramaPCMSO2.Prazo = this.DataPcmso.AddYears(1);
                cronogramaPCMSO2.PlanejamentoAnual = "2) Emissão de Relatório Anual.";
                cronogramaPCMSO2.MetodologiaAcao = "Emitir o Relatório Anual de Exames contendo o número de exames realizados, a natureza dos exames, o número de exames que apresentaram anormalidades e o número de exames previstos para o ano seguinte.";
                cronogramaPCMSO2.FormaRegistro = "Relatório Anual de Exames";
                //cronogramaPCMSO2.PopularPrazo();
                cronogramaPCMSO2.Mes10 = true;
                cronogramaPCMSO2.Mes11 = true;
                cronogramaPCMSO2.Ano = this.DataPcmso.Year.ToString();
                cronogramaPCMSO2.Save();
                list.Add(cronogramaPCMSO2);

                //Padrão 03
                CronogramaPCMSO cronogramaPCMSO3 = new CronogramaPCMSO();
                cronogramaPCMSO3.Inicialize();
                cronogramaPCMSO3.IdPcmso = this;
                cronogramaPCMSO3.Prazo = this.DataPcmso.AddMonths(1);
                cronogramaPCMSO3.PlanejamentoAnual = "3) Realização de Exames Clínicos.";
                cronogramaPCMSO3.MetodologiaAcao = "Os exames clínicos devem ser realizados na admissão, anualmente ou bianualmente, no retorno ao trabalho, na mudança de função e na demissão, segundo as diretrizes do PCMSO.";
                cronogramaPCMSO3.FormaRegistro = "ASO - Atestado de Saúde Ocupacional";
                cronogramaPCMSO3.Mes12 = true;
                cronogramaPCMSO3.Mes01 = true;
                cronogramaPCMSO3.Ano = this.DataPcmso.Year + " e " + this.DataPcmso.AddYears(1).Year;
                cronogramaPCMSO3.Save();
                list.Add(cronogramaPCMSO3);

            }

            return list;
        }



        #region GetCoordenadoresPCMSO

        public static List<Medico> GetCoordenadoresPCMSO(Cliente cliente)
        {
            List<Medico> medicosMestra = GetMedicosMestra();
            List<Medico> medicosClinica = GetMedicosClinica(cliente);
            List<Medico> medicosCliente = GetMedicosCliente(cliente);

            List<Medico> ret = new List<Medico>();

            foreach (Medico medico in medicosClinica)
                ret.Add(medico);

            foreach (Medico medico in medicosCliente)
                ret.Add(medico);

            foreach (Medico medico in medicosMestra)
                ret.Add(medico);

            return ret;
        }

        private static List<Medico> GetMedicosMestra()
        {
            return new Medico().Find<Medico>("IdMedico=" + (int)Medicos.DraRosana);
        }

        private static List<Medico> GetMedicosCliente(Cliente cliente)
        {
            //Médico do Proprio Cliente
            StringBuilder criteria = new StringBuilder();

            criteria.Append("IsInativo=0");

            if (cliente.IdGrupoEmpresa.Id != 0)
                criteria.Append(" AND IdMedico IN (SELECT IdJuridicaPessoa"
                                + " FROM JuridicaPessoa"
                                + " WHERE IdJuridica IN"
                                + " (SELECT IdJuridica"
                                + " FROM Juridica"
                                + " WHERE IdGrupoEmpresa=" + cliente.IdGrupoEmpresa.Id
                                + " AND IdJuridica IN (SELECT IdCliente FROM Cliente)"
                                + "))");
            else
                criteria.Append(" AND IdJuridica =" + cliente.Id);

            return new Medico().Find<Medico>(criteria.ToString());
        }

        private static List<Medico> GetMedicosClinica(Cliente cliente)
        {
            StringBuilder criteria = new StringBuilder();

            criteria.Append("IsInativo=0"
                + " AND IdMedico IN (SELECT IdJuridicaPessoa FROM JuridicaPessoa WHERE IdJuridica "
                + "	IN (SELECT IdClinica FROM ClinicaCliente WHERE");

            if (cliente.IdGrupoEmpresa.Id != 0)
                criteria.Append(" IdCliente IN (SELECT IdJuridica FROM Juridica WHERE"
                            + " IdGrupoEmpresa =" + cliente.IdGrupoEmpresa.Id + ")");
            else
                criteria.Append(" IdCliente =" + cliente.Id);

            criteria.Append(" AND IdClinica IN (SELECT IdClinica FROM Clinica WHERE IsClinicaInterna=1)))");
            criteria.Append(" AND IdMedico NOT IN (SELECT IdJuridicaPessoa FROM JuridicaPessoa WHERE IdJuridica = 310)");

            return new Medico().Find<Medico>(criteria.ToString());
        }
        #endregion




        #endregion
    }

    [Database("opsa", "Pcmso_Anexos", "IdPcmso_Anexo")]
    public class PCMSO_Anexo : Ilitera.Data.Table
    {
        private int _IdPcmso_Anexo;
        private int _IdPcmso;
        private string _Arquivo;
        private string _Descricao;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public PCMSO_Anexo()
        {
        }
        public override int Id
        {
            get { return _IdPcmso_Anexo; }
            set { _IdPcmso_Anexo = value; }
        }
        public int IdPcmso
        {
            get { return _IdPcmso; }
            set { _IdPcmso = value; }
        }
        public string Arquivo
        {
            get { return _Arquivo; }
            set { _Arquivo = value; }
        }
        public string Descricao
        {
            get { return _Descricao; }
            set { _Descricao = value; }
        }
        //public override int Save()
        //{
        //    return base.Save();
        //}
    }


    [Database("opsa", "Pcmso_Historico", "IdPcmso_Historico")]
    public class PCMSO_Historico : Ilitera.Data.Table
    {
        private int _IdPcmso_Historico;
        private int _IdPcmso;
        private string _Historico;


        public PCMSO_Historico()
        {
        }
        public override int Id
        {
            get { return _IdPcmso_Historico; }
            set { _IdPcmso_Historico = value; }
        }
        public int IdPcmso
        {
            get { return _IdPcmso; }
            set { _IdPcmso = value; }
        }
        public string Historico
        {
            get { return _Historico; }
            set { _Historico = value; }
        }
        //public override int Save()
        //{
        //    return base.Save();
        //}
    }


}
