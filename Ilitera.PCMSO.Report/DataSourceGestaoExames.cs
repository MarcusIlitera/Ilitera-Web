using System;
using System.Data;
using System.Collections;
using System.Text;
using Ilitera.Common;
using Ilitera.Opsa.Data;

namespace Ilitera.PCMSO.Report
{
    public class DataSourceGestaoExames : DataSourceBase
    {
        private string PalavraChave;
        private bool EmpresaGrupo, EmpregadosAtivos, SemDataUltimo;
        private DateTime periodoDe, periodoAte, renovacaoAte, vencidosAte;
        private IndTipoExame indTipoExame;
        private ResultadoExame resultadoExame;
        private Cliente cliente;

        public DataSourceGestaoExames(Cliente cliente) : 
            this(cliente, string.Empty, false, IndTipoExame.Indefinido, new DateTime(), new DateTime(), ResultadoExame.Indefinido, false, new DateTime(), new DateTime(), false)
        {
        }
      
        public DataSourceGestaoExames(Cliente cliente, string PalavraChave, bool EmpresaGrupo, IndTipoExame indTipoExame, DateTime periodoDe, DateTime periodoAte, ResultadoExame resultadoExame, 
            bool EmpregadosAtivos, DateTime renovacaoAte, DateTime vencidosAte, bool SemDataUltimo)
        {
            this.PalavraChave = PalavraChave;
            this.EmpresaGrupo = EmpresaGrupo;
            this.EmpregadosAtivos = EmpregadosAtivos;
            this.periodoDe = periodoDe;
            this.periodoAte = periodoAte;
            this.indTipoExame = indTipoExame;
            this.resultadoExame = resultadoExame;
            this.cliente = cliente;
            this.renovacaoAte = renovacaoAte;
            this.vencidosAte = vencidosAte;
            this.SemDataUltimo = SemDataUltimo;
        }

        public RptListaExamesCadastrados GetReportListaExamesCadastrados()
        {
            RptListaExamesCadastrados report = new RptListaExamesCadastrados();
            report.OpenSubreport("ListaExames").SetDataSource(GetListaExamesCadastrados());
            report.SetDataSource(GetHeaderExamesCadastrados());
            report.Refresh();
            SetTempoProcessamento(report);
            return report;
        }

        public RptListaPlanejamentoExames GetReportListaPlanejamentoExames()
        {
            RptListaPlanejamentoExames report = new RptListaPlanejamentoExames();
            report.OpenSubreport("ListaExames").SetDataSource(GetListaPlanejamentoExames());
            report.SetDataSource(GetHeaderPlanejamentoExames());
            report.Refresh();
            SetTempoProcessamento(report);
            return report;
        }

        #region Planejamento Exames

        private DataSet GetHeaderPlanejamentoExames()
        {
            DataSet ds = new DataSet();
            ds.Tables.Add(GetTableHeader());

            DataRow newRowHeader = ds.Tables[0].NewRow();

            newRowHeader["NomeEmpresa"] = cliente.NomeAbreviado;
            newRowHeader["PalavraChave"] = PalavraChave;

            switch (indTipoExame)
            {
                case IndTipoExame.Audiometrico:
                    newRowHeader["TipoExame"] = "Audiométrico";
                    break;
                case IndTipoExame.Clinico:
                    newRowHeader["TipoExame"] = "Clínico";
                    break;
                case IndTipoExame.Complementar:
                    newRowHeader["TipoExame"] = "Complementar";
                    break;
                case IndTipoExame.NaoOcupacional:
                    newRowHeader["TipoExame"] = "Não Ocupacional";
                    break;
                case IndTipoExame.Indefinido:
                    newRowHeader["TipoExame"] = "Todos";
                    break;
            }

            newRowHeader["RenovacaoAte"] = (renovacaoAte.Equals(new DateTime())) ? "__-__-____" : renovacaoAte.ToString("dd-MM-yyyy");
            newRowHeader["VencidosAte"] = (vencidosAte.Equals(new DateTime())) ? "__-__-____" : vencidosAte.ToString("dd-MM-yyyy");
            newRowHeader["EmpresaGrupo"] = (EmpresaGrupo) ? "Sim" : "Não";
            newRowHeader["SemDataUltimo"] = (SemDataUltimo) ? "Sim" : "Não";

            ds.Tables[0].Rows.Add(newRowHeader);

            return ds;
        }

        private DataSet GetListaPlanejamentoExames()
        {
            Pcmso pcmso = cliente.GetUltimoPcmso();

            if (pcmso.Id.Equals(0))
                throw new Exception("A empresa " + cliente.NomeAbreviado + " ainda não possui nenhum PCMSO finalizado para o Planejamento de Exames. No caso de dúvida, por gentileza, entre em contato com a Ilitera.");
            
            DataSet ds = new DataSet();
            ds.Tables.Add(GetTableResult());

            DataRow newRowResult;
            StringBuilder sqlstm = new StringBuilder();

            sqlstm.Append("USE " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + " ");
            sqlstm.Append(" SELECT NomeEmpregado, NomeExame, DataUltima, DataVencimento, DataProxima, Preventivo FROM qryExamePlanejamento");

            if (EmpresaGrupo)
                sqlstm.Append(" WHERE IdPcmso IN (SELECT IdPcmso FROM qryUltimoPCMSO WHERE IdCliente IN (SELECT IdJuridica FROM Juridica WHERE IdGrupoEmpresa=" + cliente.IdGrupoEmpresa.Id + "))");
            else
                sqlstm.Append(" WHERE IdPcmso=" + pcmso.Id);

            if (!PalavraChave.Trim().Equals(string.Empty))
                sqlstm.Append(" AND UPPER(NomeEmpregado) COLLATE SQL_Latin1_General_Cp1251_CS_AS LIKE '%" + PalavraChave.Trim().ToUpper() + "%'");

            if (indTipoExame != IndTipoExame.Indefinido)
                sqlstm.Append(" AND IdExameDicionario IN (SELECT IdExameDicionario FROM ExameDicionario WHERE IndExame=" + (int)indTipoExame + ")");

            if (!renovacaoAte.Equals(new DateTime()))
                sqlstm.Append(" AND DataProxima <= '" + renovacaoAte.Year.ToString() + renovacaoAte.Month.ToString("00") + renovacaoAte.Day.ToString("00") + "'");

            if (!vencidosAte.Equals(new DateTime()))
                sqlstm.Append(" AND (DataVencimento <= '" + vencidosAte.Year.ToString() + vencidosAte.Month.ToString("00") + vencidosAte.Day.ToString("00") + "'"
                    + " OR DataVencimento IS NULL)");

            if (SemDataUltimo)
                sqlstm.Append(" AND DataUltima IS NULL");

            sqlstm.Append(" ORDER BY NomeEmpregado, NomeExame");

            DataSet dsqryPlanejamentoExames = new ExameBase().ExecuteDataset(sqlstm.ToString());

            foreach (DataRow rowExame in dsqryPlanejamentoExames.Tables[0].Select())
            {
                newRowResult = ds.Tables[0].NewRow();

                newRowResult["NomeEmpregado"] = rowExame["NomeEmpregado"].ToString();
                newRowResult["NomeExame"] = rowExame["NomeExame"].ToString();
                newRowResult["DataUltimo"] = (!rowExame["DataUltima"].ToString().Equals(string.Empty)) ? Convert.ToDateTime(rowExame["DataUltima"]).ToString("dd-MM-yyyy") : "__-__-____";
                newRowResult["DataVencimento"] = (!rowExame["DataVencimento"].ToString().Equals(string.Empty)) ? Convert.ToDateTime(rowExame["DataVencimento"]).ToString("dd-MM-yyyy") : "__-__-____";
                newRowResult["DataRenovacao"] = (!rowExame["DataProxima"].ToString().Equals(string.Empty)) ? Convert.ToDateTime(rowExame["DataProxima"]).ToString("dd-MM-yyyy") : "__-__-____";
                newRowResult["Preventivo"] = (Convert.ToBoolean(rowExame["Preventivo"])) ? "S" : "N";

                ds.Tables[0].Rows.Add(newRowResult);
            }

            return ds;
        }

        #endregion

        #region Exames Cadastrados

        private DataSet GetListaExamesCadastrados()
        {
            DataSet ds = new DataSet();
            ds.Tables.Add(GetTableResult());

            DataRow newRowResult;
            StringBuilder sqlstm = new StringBuilder();

            sqlstm.Append("USE " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + " ");
            sqlstm.Append(" SELECT Empregado, DataExame, Tipo, IndResultado FROM qryExamesRealizados");

            if (EmpresaGrupo)
                sqlstm.Append(" WHERE IdGrupoEmpresa=" + cliente.IdGrupoEmpresa.Id);
            else
                sqlstm.Append(" WHERE IdEmpregado IN (SELECT nID_EMPREGADO FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.qryEmpregadoFuncao WHERE Alocado IS NOT NULL AND Alocado=" + cliente.Id + ")");

            if (!PalavraChave.Trim().Equals(string.Empty))
                sqlstm.Append(" AND UPPER(Empregado) COLLATE SQL_Latin1_General_Cp1251_CS_AS LIKE '%" + PalavraChave.Trim().ToUpper() + "%'");
            
            if (indTipoExame != IndTipoExame.Indefinido)
                sqlstm.Append(" AND IndExame=" + (int)indTipoExame);

            if (!periodoDe.Equals(new DateTime()) && !periodoAte.Equals(new DateTime()))
                sqlstm.Append(" AND (DataExame BETWEEN '" + periodoDe.Year.ToString() + periodoDe.Month.ToString("00") + periodoDe.Day.ToString("00") + "'"
                    + " AND '" + periodoAte.Year.ToString() + periodoAte.Month.ToString("00") + periodoAte.Day.ToString("00") + "')");
            else if (!periodoDe.Equals(new DateTime()))
                sqlstm.Append(" AND DataExame >= '" + periodoDe.Year.ToString() + periodoDe.Month.ToString("00") + periodoDe.Day.ToString("00") + "'");
            else if (!periodoAte.Equals(new DateTime()))
                sqlstm.Append(" AND DataExame <= '" + periodoAte.Year.ToString() + periodoAte.Month.ToString("00") + periodoAte.Day.ToString("00") + "'");

            if (resultadoExame != ResultadoExame.Indefinido)
                sqlstm.Append(" AND IndResultado=" + (int)resultadoExame);

            if (EmpregadosAtivos)
                sqlstm.Append(" AND IdEmpregado IN (SELECT nID_EMPREGADO FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEMPREGADO WHERE hDT_DEM IS NULL)");

            sqlstm.Append(" ORDER BY Empregado, DataExame DESC");

            DataSet dsqryExames = new ExameBase().ExecuteDataset(sqlstm.ToString());

            foreach (DataRow rowExame in dsqryExames.Tables[0].Select())
            {
                newRowResult = ds.Tables[0].NewRow();

                newRowResult["NomeEmpregado"] = rowExame["Empregado"];
                newRowResult["NomeExame"] = rowExame["Tipo"];
                newRowResult["DataExame"] = Convert.ToDateTime(rowExame["DataExame"]).ToString("dd-MM-yyyy");

                switch (Convert.ToInt32(rowExame["IndResultado"]))
                {
                    case (int)ResultadoExame.Alterado:
                        newRowResult["Resultado"] = "Inapto";
                        break;
                    case (int)ResultadoExame.EmEspera:
                        newRowResult["Resultado"] = "Em Espera";
                        break;
                    case (int)ResultadoExame.NaoRealizado:
                        newRowResult["Resultado"] = "Não Realizado";
                        break;
                    case (int)ResultadoExame.Normal:
                        newRowResult["Resultado"] = "Apto";
                        break;
                }

                ds.Tables[0].Rows.Add(newRowResult);
            }

            return ds;
        }

        private DataSet GetHeaderExamesCadastrados()
        {
            DataSet ds = new DataSet();
            ds.Tables.Add(GetTableHeader());

            DataRow newRowHeader = ds.Tables[0].NewRow();

            newRowHeader["NomeEmpresa"] = cliente.NomeAbreviado;
            newRowHeader["PalavraChave"] = PalavraChave;

            switch (indTipoExame)
            {
                case IndTipoExame.Audiometrico:
                    newRowHeader["TipoExame"] = "Audiométrico";
                    break;
                case IndTipoExame.Clinico:
                    newRowHeader["TipoExame"] = "Clínico";
                    break;
                case IndTipoExame.Complementar:
                    newRowHeader["TipoExame"] = "Complementar";
                    break;
                case IndTipoExame.NaoOcupacional:
                    newRowHeader["TipoExame"] = "Não Ocupacional";
                    break;
                case IndTipoExame.Indefinido:
                    newRowHeader["TipoExame"] = "Todos";
                    break;
            }

            if (!periodoDe.Equals(new DateTime()) && !periodoAte.Equals(new DateTime()))
                newRowHeader["Periodo"] = periodoDe.ToString("dd-MM-yyyy") + " até " + periodoAte.ToString("dd-MM-yyyy");
            else if (!periodoDe.Equals(new DateTime()))
                newRowHeader["Periodo"] = periodoDe.ToString("dd-MM-yyyy") + " até __-__-____";
            else if (!periodoAte.Equals(new DateTime()))
                newRowHeader["Periodo"] = "__-__-____ até " + periodoAte.ToString("dd-MM-yyyy");
            else
                newRowHeader["Periodo"] = "__-__-____ até __-__-____";

            switch (resultadoExame)
            {
                case ResultadoExame.Alterado:
                    newRowHeader["Resultado"] = "Inapto";
                    break;
                case ResultadoExame.EmEspera:
                    newRowHeader["Resultado"] = "Em Espera";
                    break;
                case ResultadoExame.NaoRealizado:
                    newRowHeader["Resultado"] = "Não Realizado";
                    break;
                case ResultadoExame.Normal:
                    newRowHeader["Resultado"] = "Apto";
                    break;
                case ResultadoExame.Indefinido:
                    newRowHeader["Resultado"] = "Todos";
                    break;
            }

            newRowHeader["EmpresaGrupo"] = (EmpresaGrupo) ? "Sim" : "Não";
            newRowHeader["EmpregadosAtivos"] = (EmpregadosAtivos) ? "Sim" : "Não";

            ds.Tables[0].Rows.Add(newRowHeader);

            return ds;
        }

        #endregion

        private DataTable GetTableHeader()
        {
            DataTable table = new DataTable("Header");
            table.Columns.Add("NomeEmpresa", typeof(string));
            table.Columns.Add("PalavraChave", typeof(string));
            table.Columns.Add("TipoExame", typeof(string));
            table.Columns.Add("Periodo", typeof(string));
            table.Columns.Add("Resultado", typeof(string));
            table.Columns.Add("EmpresaGrupo", typeof(string));
            table.Columns.Add("EmpregadosAtivos", typeof(string));
            table.Columns.Add("RenovacaoAte", typeof(string));
            table.Columns.Add("VencidosAte", typeof(string));
            table.Columns.Add("SemDataUltimo", typeof(string));
            return table;
        }

        private DataTable GetTableResult()
        {
            DataTable table = new DataTable("Result");
            table.Columns.Add("NomeEmpregado", typeof(string));
            table.Columns.Add("NomeExame", typeof(string));
            table.Columns.Add("DataExame", typeof(string));
            table.Columns.Add("Resultado", typeof(string));
            table.Columns.Add("DataUltimo", typeof(string));
            table.Columns.Add("DataVencimento", typeof(string));
            table.Columns.Add("DataRenovacao", typeof(string));
            table.Columns.Add("Preventivo", typeof(string));
            return table;
        }
    }
}
