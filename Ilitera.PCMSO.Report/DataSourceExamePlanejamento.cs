using System;
using System.Collections;
using System.Data;
using System.Text;
using System.Collections.Generic;

using Ilitera.Common;
using Ilitera.Opsa.Data;

namespace Ilitera.PCMSO.Report
{
    public class DataSourceExamePlanejamento : DataSourceBase
    {
        #region Variaveis

        private Pcmso pcmso;
        private int IndExameTipo;
        private int IdExameDicionario;
        private DateTime renovacaoAte;
        private bool bRevovacao;
        private DateTime vencimento;
        private bool Vencidos;
        private bool AdmitidosApos90dias;
        private int IdGrupoEmpresa;
        private bool TodosClientes;
        private DataSet dsGhe;
        private ConvocacaoExame convocacaoExame;


        #endregion

        #region Enum

        public enum TipoRelatorio : int
        {
            PorEmpregado,
            PorExame,
            ProgramaMedicoEspecial
        }
        #endregion

        #region Contrutor

        public DataSourceExamePlanejamento(Pcmso pcmso,
                                            int IndExameTipo,
                                            int IdExameDicionario,
                                            DateTime renovacaoAte,
                                            bool bRevovacao,
                                            DateTime vencimento,
                                            bool Vencidos,
                                            bool AdmitidosApos90dias,
                                            int IdGrupoEmpresa,
                                            bool TodosClientes)
            : this(pcmso)
        {
            this.renovacaoAte = renovacaoAte;
            this.bRevovacao = bRevovacao;
            this.vencimento = vencimento;
            this.Vencidos = Vencidos;
            this.IndExameTipo = IndExameTipo;
            this.IdExameDicionario = IdExameDicionario;
            this.AdmitidosApos90dias = AdmitidosApos90dias;
            this.IdGrupoEmpresa = IdGrupoEmpresa;
            this.TodosClientes = TodosClientes;
        }

    
        public DataSourceExamePlanejamento(Pcmso pcmso)
        {
            this.pcmso = pcmso;

            if (this.pcmso != null && this.pcmso.IsFromWeb)
                throw new Exception("PCMSO de terceiros, não realizado pela Mestra!");
        }


        public DataSourceExamePlanejamento(Pcmso pcmso, ConvocacaoExame convocacaoExame)
        {
            this.pcmso = pcmso;
            this.convocacaoExame = convocacaoExame;

            if (this.pcmso != null && this.pcmso.IsFromWeb)
                throw new Exception("PCMSO de terceiros, não realizado pela Mestra!");
        }

       
        #endregion

        #region Reports

        public RptExamePlanPorEmpregado GetReportExamePorEmpregado()
        {
            DataSet dsResult = GetDataSource();

            return GetReportExamePorEmpregado(dsResult);
        }

        public RptExamePlanPorEmpregado GetReportExamePorEmpregado(DataSet dsResult)
        {
            RptExamePlanPorEmpregado report = new RptExamePlanPorEmpregado();
            report.SetDataSource(GetDataSource(TipoRelatorio.PorEmpregado, dsResult));
            report.OpenSubreport("CoordenadorPcmso").SetDataSource(pcmso.GetDSCoordenador());
            report.Refresh();
            SetTempoProcessamento(report);
            return report;
        }

        public RptExamePlanPorData GetReportExamePorData()
        {
            DataSet dsResult = new ExameDicionario().ExecuteDataset(GetQuery());

            return GetReportExamePorData(dsResult);
        }

        public RptExamePlanPorData GetReportExamePorData(DataSet dsResult)
        {
            RptExamePlanPorData report = new RptExamePlanPorData();
            report.SetDataSource(GetDataSource(TipoRelatorio.PorExame, dsResult));
            report.OpenSubreport("CoordenadorPcmso").SetDataSource(pcmso.GetDSCoordenador());
            report.Refresh();
            SetTempoProcessamento(report);
            return report;
        }

        public RptExamePlanProgramaEspecial GetReportProgramaEspecial()
        {
            DataSet dsResult = new ExameDicionario().ExecuteDataset(GetQuery());

            RptExamePlanProgramaEspecial report = new RptExamePlanProgramaEspecial();
            report.SetDataSource(GetDataSource(TipoRelatorio.ProgramaMedicoEspecial, dsResult));
            report.OpenSubreport("CoordenadorPcmso").SetDataSource(pcmso.GetDSCoordenador());
            report.Refresh();
            SetTempoProcessamento(report);
            return report;
        }
        #endregion

        #region Query

        private string GetQuery()
        {
            StringBuilder str = new StringBuilder();

            //str.Append("USE "  + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + " ");
            //str.Append(" SELECT IdPcmsoPlanejamento, IdCliente, IdEmpregado, DataPcmso,");
            //str.Append(" DataAdmissao, Fantasia as Fantasia, NomeEmpregado, Funcao, NomeExame as NomeExame, NomeGhe,");
            //str.Append(" DataVencimento, Sexo, DataNascEmpregado,");
            //str.Append(" IdadeEmpregado, ExposicaoRisco, DataProxima,");
            //str.Append(" DataUltima, Intervalo, IndPeriodicidade, IdGhe, Preventivo");
            //str.Append(" FROM qryExamePlanejamento WHERE");

            str.Append(" SELECT IdPcmsoPlanejamento, qryExamePlanejamento.IdCliente, IdEmpregado, qryExamePlanejamento.DataPcmso,");
            str.Append(" DataAdmissao, Fantasia, NomeEmpregado, e.NomeFuncao as Funcao, NomeExame, NomeGhe,");
            str.Append(" DataVencimento, Sexo, DataNascEmpregado,");
            str.Append(" IdadeEmpregado, ExposicaoRisco, DataProxima,");
            str.Append(" DataUltima, Intervalo, IndPeriodicidade, IdGhe, Preventivo");
            str.Append(" FROM qryExamePlanejamento ");
            str.Append(" left join pcmso as b on(qryExamePlanejamento.idpcmso = b.idpcmso) ");
            str.Append(" left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2 + ".dbo.tblEMPREGADO_FUNCAO as c on(qryExamePlanejamento.idcliente = c.nid_empr and qryExamePlanejamento.idEmpregado = c.nID_EMPREGADO) ");
            str.Append(" join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2 + ".dbo.TblFunc_Empregado as d on(c.nid_empregado_funcao = d.nid_empregado_Funcao and b.IdLaudoTecnico = d.nID_LAUD_TEC) ");
            str.Append(" left join funcao as e on(c.nID_FUNCAO = e.IdFuncao) ");

            str.Append(" WHERE ");

            str.Append(GetFiltro());

            str.Append("  and convert(char(12), IdEmpregado ) +' ' + convert(char(10), c.hdt_inicio, 103) in ");
            str.Append(" ( ");
            str.Append("    select convert(char(12), nId_Empregado) + ' ' + convert(char(10), max(hdt_inicio), 103) ");
            str.Append("    from " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2 + ".dbo.tblempregado_Funcao as ra ");
            str.Append("    join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2 + ".dbo.TblFunc_Empregado as rb on (ra.nid_empregado_funcao = rb.nid_empregado_Funcao) ");
            str.Append("    where rb.nID_LAUD_TEC = b.IdLaudoTecnico ");
            str.Append("    group by nId_Empregado ");
            str.Append(" ) ");


            str.Append(" and ( DataDemissao is null or DataDemissao > qryExamePlanejamento.TerminoPCMSO ) ");

            //wagner - teste
            str.Append(" and idpcmsoplanejamento in ");
            str.Append(" ( ");
            str.Append("    select d.idpcmsoplanejamento ");
            str.Append("    from " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblempregado_funcao as a left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblfunc_empregado as b ");
            str.Append(" on a.nid_empregado_funcao = b.nid_empregado_funcao ");
            str.Append("    left join pcmsoplanejamento as c ");
            str.Append("    on b.nid_func = c.idghe ");
            str.Append("    left join exameplanejamento as d ");
            str.Append("    on c.idpcmsoplanejamento = d.idpcmsoplanejamento ");
            str.Append("    where a.nid_empregado = qryExamePlanejamento.IdEmpregado ");
            str.Append("    and hdt_inicio in ");
            str.Append("    ( ");
            str.Append("       select top 1 hdt_inicio ");
            str.Append("       from " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblempregado_funcao as a left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblfunc_empregado as b ");
            str.Append("       on a.nid_empregado_funcao = b.nid_empregado_funcao ");
            str.Append("       left join pcmsoplanejamento as c ");
            str.Append("       on b.nid_func = c.idghe ");
            str.Append("       left join exameplanejamento as d ");
            str.Append("       on c.idpcmsoplanejamento = d.idpcmsoplanejamento ");
            str.Append("       where a.nid_empregado = qryExamePlanejamento.IdEmpregado ");
            str.Append("      order by a.hdt_inicio desc ");
            str.Append("   ) ");
            str.Append(" ) ");


            return str.ToString();
        }

        private string GetFiltro()
        {
            StringBuilder str = new StringBuilder();

            if (!TodosClientes && IdGrupoEmpresa == 0 && pcmso != null && pcmso.Id != 0)
                str.Append(" qryExamePlanejamento.IdPcmso=" + pcmso.Id);
            else if (!TodosClientes && IdGrupoEmpresa != 0)
                str.Append(" qryExamePlanejamento.IdPcmso IN (SELECT IdPcmso FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.qryUltimoPCMSO WHERE IdCliente IN (SELECT IdJuridica FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.Juridica WHERE IdGrupoEmpresa=" + IdGrupoEmpresa + "))");
            else if (TodosClientes)
                str.Append(" qryExamePlanejamento.IdPcmso IN (SELECT IdPcmso FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.qryUltimoPCMSO)");
            else
                str.Append(" qryExamePlanejamento.IdPcmso=0");

            if (convocacaoExame != null)
            {
                str.Append(" AND IdEmpregado IN "
                            + "(SELECT IdEmpregado"
                            + " FROM ExameBase"
                            + " WHERE IdConvocacaoExame IS NOT NULL"
                            + " AND IdConvocacaoExame=" + convocacaoExame.Id
                            + " AND IndResultado=" + (int)ResultadoExame.NaoRealizado 
                            + ")");
            }
            else
            {
                if (IndExameTipo != 0 && IndExameTipo != -1)
                    str.Append(" AND IdExameDicionario IN (SELECT IdExameDicionario FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.ExameDicionario WHERE IndExame=" + IndExameTipo + ")");

                if (IdExameDicionario != 0)
                    str.Append(" AND IdExameDicionario=" + IdExameDicionario);

                if (bRevovacao)
                    str.Append(" AND (DataProxima <='" + renovacaoAte.ToString("yyyy-MM-dd") + " 23:59:59.999' OR DataProxima IS NULL)");

                if (Vencidos)
                    str.Append(" AND (DataVencimento <'" + vencimento.ToString("yyyy-MM-dd") + " 23:59:59.999' OR DataVencimento IS NULL)");

                if (AdmitidosApos90dias)
                    str.Append(" AND IdEmpregado IN (SELECT nID_EMPREGADO FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEMPREGADO WHERE hDT_ADM <='" + DateTime.Today.AddDays(-90).ToString("yyyy-MM-dd") + "')");




            }

            return str.ToString();
        }
        #endregion

        #region GetNumeroEmpregados

        public int GetNumeroEmpregados()
        {
            string sql = "SELECT COUNT(DISTINCT IdEmpregado)"
                    + " FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.ExamePlanejamento"
                    + " WHERE " + GetFiltro();

            int count = Convert.ToInt32(new Empregado().ExecuteScalar(sql));

            return count;
        }
        #endregion

        #region DataSource

        public DataSet GetDataSource()
        {
            DataSet dsResult = new ExameDicionario().ExecuteDataset(GetQuery());

            return dsResult;
        }

        public DataSet GetDataSource(TipoRelatorio tipoRelatorio, DataSet dsResult)
        {
            DataSet ds = new DataSet();

            ds.Tables.Add(GetDataTableExamePlan());

            int empregExame = 0;

            string nomeExame = string.Empty;

            DateTime dataExame = new DateTime();

            string strCarimboCNPJ = this.pcmso.IdCliente.GetCarimboCnpjHtml(this.pcmso.DataPcmso);

            string strDataPcmso;

            if (tipoRelatorio == TipoRelatorio.ProgramaMedicoEspecial)
                strDataPcmso = pcmso.DataPcmso.ToString("dd-MM-yyyy");
            else
                strDataPcmso = "Período " + pcmso.GetPeriodo();

            DataRow newRow;

            foreach (DataRow row in dsResult.Tables[0].Rows)
            {

                Empregado empregado;
                empregado = new Empregado();
                empregado.Find(" tblEMPREGADO.nID_EMPREGADO = " + row["IdEmpregado"].ToString());

                Clinico exame = new Clinico();
                exame.IdEmpregadoFuncao = EmpregadoFuncao.GetEmpregadoFuncao(empregado);
                exame.IdPcmso = exame.IdEmpregadoFuncao.nID_EMPR.GetUltimoPcmso();

                //exame.UsuarioId = System.Convert.ToInt32(Request["IdUsuario"].ToString());


                List<Ghe> ghes = new Ghe().Find<Ghe>("nID_LAUD_TEC=" + pcmso.IdLaudoTecnico.Id);

                Ghe ghe;

                if (ghes == null || ghes.Count == 0)
                    ghe = exame.IdEmpregadoFuncao.GetGheEmpregado(pcmso.IdLaudoTecnico);
                else
                {
                    int IdGhe = exame.IdEmpregadoFuncao.GetIdGheEmpregado(pcmso.IdLaudoTecnico);

                    ghe = ghes.Find(delegate(Ghe g) { return g.Id == IdGhe; });
                }


                newRow = ds.Tables[0].NewRow();
                newRow["CarimboCNPJ"] = strCarimboCNPJ;
                newRow["DataPcmso"] = strDataPcmso;
                newRow["IdEmpregado"] = row["IdEmpregado"];
                newRow["NomeEmpregado"] = row["NomeEmpregado"];
                newRow["Funcao"] = row["Funcao"];
                //newRow["Risco"] = GetRiscosOcupacionais(Convert.ToInt32(row["IdGhe"]));

                if (ghe == null)
                {
                    newRow["Risco"] = "-";
                    newRow["NomeGhe"] = "-";
                }
                else
                {
                    newRow["Risco"] = GetRiscosOcupacionais(ghe.Id);
                    newRow["NomeGhe"] = ghe.tNO_FUNC;
                }

                newRow["DataAdmissao"] = row["DataAdmissao"] != System.DBNull.Value ? Convert.ToDateTime(row["DataAdmissao"]).ToString("dd-MM-yyyy") : "-";
                newRow["Sexo"] = row["Sexo"];
                newRow["DataNascEmpregado"] = row["DataNascEmpregado"] != System.DBNull.Value ? Convert.ToDateTime(row["DataNascEmpregado"]).ToString("dd-MM-yyyy") : "-";
                newRow["IdadeEmpregado"] = row["IdadeEmpregado"];
                newRow["NomeExame"] = Convert.ToBoolean(row["Preventivo"]) ? row["NomeExame"] + " (por iniciativa da empresa)" : row["NomeExame"];
                //newRow["NomeGhe"] = row["NomeGhe"];
                newRow["Periodicidade"] = ExameDicionario.GetIndPeriodicidade(Convert.ToInt32(row["Intervalo"]), Convert.ToInt32(row["IndPeriodicidade"]));
                newRow["ExposicaoRisco"] = row["ExposicaoRisco"];
                newRow["DataUltima"] = row["DataUltima"] != System.DBNull.Value ? Convert.ToDateTime(row["DataUltima"]).ToString("dd-MM-yyyy") : "-";
                newRow["DataVencimento"] = row["DataVencimento"] != System.DBNull.Value ? Convert.ToDateTime(row["DataVencimento"]).ToString("dd-MM-yyyy") : "-"; ;
                //newRow["DataProxima"] = row["DataProxima"];

                if (!(empregExame == Convert.ToInt32(row["IdEmpregado"])
                    && nomeExame == Convert.ToString(row["NomeExame"])
                    && dataExame == Convert.ToDateTime(row["DataProxima"])))
                    ds.Tables[0].Rows.Add(newRow);

                empregExame = Convert.ToInt32(row["IdEmpregado"]);
                nomeExame = Convert.ToString(row["NomeExame"]);
                dataExame = Convert.ToDateTime(row["DataProxima"]);
            }
            return ds;
        }

        private string GetRiscosOcupacionais(int IdGhe)
        {
            if (dsGhe == null)
                GetDataSourceGhe();

            DataRow[] rows = dsGhe.Tables[0].Select("IdGhe=" + IdGhe);

            if (rows.Length > 0)
                return (string)rows[0]["Risco"];
            else
                return string.Empty;
        }

        private DataSet GetDataSourceGhe()
        {
            dsGhe = new DataSet();
            DataTable table = GetTableGhe();
            dsGhe.Tables.Add(table);

            ArrayList listGhe = new Ghe().Find("nID_FUNC IN (SELECT IdGhe FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.PcmsoPlanejamento WHERE IdPcmso=" + this.pcmso.Id + ")");

            DataRow newRow;

            foreach (Ghe ghe in listGhe)
            {
                newRow = dsGhe.Tables[0].NewRow();
                newRow["IdGhe"] = ghe.Id; ;
                newRow["Nome"] = ghe.tNO_FUNC;
                newRow["Risco"] = ghe.RiscosOcupacionais();
                dsGhe.Tables[0].Rows.Add(newRow);
            }
            return dsGhe;
        }

        private DataSet GetDataSourceCoordenador(Prestador prestador)
        {
            DataSet ds = new DataSet();
            DataTable table = GetTableCoordenador();
            ds.Tables.Add(table);
            DataRow newRow;
            prestador.Find();
            newRow = ds.Tables[0].NewRow();
            newRow["IdPrestador"] = prestador.Id;
            newRow["DataAssinatura"] = "";
            newRow["Nome"] = prestador.NomeCompleto;
            newRow["Titulo"] = prestador.Titulo;
            newRow["Numero"] = prestador.Numero;
            newRow["Contato"] = prestador.Contato;
            ds.Tables[0].Rows.Add(newRow);
            return ds;
        }

        #endregion

        #region Table

        private static DataTable GetDataTableExamePlan()
        {
            DataTable table = new DataTable("Result");
            table.Columns.Add("CarimboCNPJ", Type.GetType("System.String"));
            table.Columns.Add("DataPcmso", Type.GetType("System.String"));
            table.Columns.Add("IdEmpregado", Type.GetType("System.Int32"));
            table.Columns.Add("NomeEmpregado", Type.GetType("System.String"));
            table.Columns.Add("Funcao", Type.GetType("System.String"));
            table.Columns.Add("Risco", Type.GetType("System.String"));
            table.Columns.Add("DataAdmissao", Type.GetType("System.String"));
            table.Columns.Add("Sexo", Type.GetType("System.String"));
            table.Columns.Add("DataNascEmpregado", Type.GetType("System.String"));
            table.Columns.Add("IdadeEmpregado", Type.GetType("System.String"));
            table.Columns.Add("NomeExame", Type.GetType("System.String"));
            table.Columns.Add("NomeGhe", Type.GetType("System.String"));
            table.Columns.Add("Periodicidade", Type.GetType("System.String"));
            table.Columns.Add("ExposicaoRisco", Type.GetType("System.String"));
            table.Columns.Add("DataUltima", Type.GetType("System.String"));
            table.Columns.Add("DataVencimento", Type.GetType("System.String"));
            table.Columns.Add("DataProxima", typeof(System.DateTime));
            return table;
        }

        private static DataTable GetTableCoordenador()
        {
            DataTable table = new DataTable("Result");
            table.Columns.Add("IdPrestador", Type.GetType("System.Int32"));
            table.Columns.Add("Nome", Type.GetType("System.String"));
            table.Columns.Add("Numero", Type.GetType("System.String"));
            table.Columns.Add("Titulo", Type.GetType("System.String"));
            table.Columns.Add("Contato", Type.GetType("System.String"));
            table.Columns.Add("DataAssinatura", Type.GetType("System.String"));
            table.Columns.Add("iDocumento", Type.GetType("System.Byte[]"));
            table.Columns.Add("iAssinatura", Type.GetType("System.Byte[]"));
            return table;
        }

        private static DataTable GetTableGhe()
        {
            DataTable table = new DataTable("Result");
            table.Columns.Add("IdGhe", Type.GetType("System.Int32"));
            table.Columns.Add("Nome", Type.GetType("System.String"));
            table.Columns.Add("Risco", Type.GetType("System.String"));
            return table;
        }

        #endregion
    }
}
