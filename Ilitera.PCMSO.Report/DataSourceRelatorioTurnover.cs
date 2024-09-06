using System;
using System.Text;
using System.Data;
using System.Collections;
using System.Collections.Generic;

using Ilitera.Common;
using Ilitera.Opsa.Data;

namespace Ilitera.PCMSO.Report
{
    public class DataSourceRelatorioTurnover : DataSourceBase
	{
        List<Cliente> clientes;
		private int anoExame;
        private bool PorEmpregados;
        private bool IncluirMudFuncaoRetTrab;

        #region Contructor

        public DataSourceRelatorioTurnover(int anoExame, bool PorEmpregados, bool IncluirMudFuncaoRetTrab)
		{
			this.anoExame = anoExame;
            this.PorEmpregados = PorEmpregados;
            this.IncluirMudFuncaoRetTrab = IncluirMudFuncaoRetTrab;

            this.clientes = new Cliente().Find<Cliente>("ContrataPCMSO=1"
                                                + " AND IdJuridicaPapel=" + (int)IndJuridicaPapel.Cliente
                                                + " AND IdCliente IN (SELECT IdCliente FROM qryClienteAtivos)"
                                                + " ORDER BY NomeAbreviado");
		}

        public DataSourceRelatorioTurnover(int anoExame, bool PorEmpregados, bool IncluirMudFuncaoRetTrab, GrupoEmpresa grupoEmpresa)
        {
            this.anoExame = anoExame;
            this.PorEmpregados = PorEmpregados;
            this.IncluirMudFuncaoRetTrab = IncluirMudFuncaoRetTrab;

            this.clientes = new Cliente().Find<Cliente>("ContrataPCMSO=1"
                                                + " AND IdCliente IN (SELECT IdCliente FROM qryClienteAtivos)"
                                                + " AND IdGrupoEmpresa=" + grupoEmpresa.Id
                                                + " ORDER BY NomeAbreviado");
        }
        #endregion

        #region GetReport

        public RptRelatorioTurnover GetReport()
		{
			RptRelatorioTurnover report = new RptRelatorioTurnover();
            report.SetDataSource(GetDataSource());
			report.Refresh();

            if(PorEmpregados)
                report.SummaryInfo.ReportComments = "* Dados retirados da admissão e demissão dos empregados";

            if (IncluirMudFuncaoRetTrab)
                report.SummaryInfo.ReportComments = "* Os exames realizados também incluem os exames de Mudança de Função e Retorno ao Trabalho.";

            SetTempoProcessamento(report);

            return report;
        }

        #endregion

        #region GetDataSource

        private DataSet GetDataSource()
		{
			DataSet ds = new DataSet();

            ds.Tables.Add(GetTable());

            DataSet dsTunover;

            if (PorEmpregados)
                dsTunover = GetDataSetEmpregados();
            else
                dsTunover = GetDataSetExamesRealizados();

            if (dsTunover.Tables.Count == 0)
                return ds;

            DataRow newRow;

            foreach (Cliente cliente in clientes)
            {
                //if (cliente.QtdEmpregados == 0)
                //    continue;

                DataRow[] rows = dsTunover.Tables[0].Select("IdCliente=" + cliente.Id);

                //if (rows.Length == 0)
                //    continue;

                DataRow row;

                if (rows.Length == 0)
                    row = null;
                else
                    row = rows[0];

                int qtdEmpregados = cliente.QtdEmpregados;

                int qtdExamesAno = GetTotalExamesAno(row);

                double TurnoverAnual = GetTornover( Convert.ToDouble(qtdExamesAno), 
                                                    Convert.ToDouble(qtdEmpregados)) / 12.0D;

                newRow = ds.Tables[0].NewRow();

                newRow["NomeCliente"] = cliente.NomeAbreviado;
                newRow["PeriodoAno"] = anoExame;
                newRow["QtdEmpregados"] = qtdEmpregados;

                if (row != null)
                {
                    newRow["QtdExamesRealizadosJan"] = GetValor(Convert.ToInt32(row["Jan"]));
                    newRow["PorcExamesRealizadosJan"] = GetValorPct(GetTornover(Convert.ToDouble(row["Jan"]), qtdEmpregados));
                    newRow["QtdExamesRealizadosFev"] = GetValor(Convert.ToInt32(row["Fev"]));
                    newRow["PorcExamesRealizadosFev"] = GetValorPct(GetTornover(Convert.ToDouble(row["Fev"]), qtdEmpregados));
                    newRow["QtdExamesRealizadosMar"] = GetValor(Convert.ToInt32(row["Mar"]));
                    newRow["PorcExamesRealizadosMar"] = GetValorPct(GetTornover(Convert.ToDouble(row["Mar"]), qtdEmpregados));
                    newRow["QtdExamesRealizadosAbr"] = GetValor(Convert.ToInt32(row["Abr"]));
                    newRow["PorcExamesRealizadosAbr"] = GetValorPct(GetTornover(Convert.ToDouble(row["Abr"]), qtdEmpregados));
                    newRow["QtdExamesRealizadosMai"] = GetValor(Convert.ToInt32(row["Mai"]));
                    newRow["PorcExamesRealizadosMai"] = GetValorPct(GetTornover(Convert.ToDouble(row["Mai"]), qtdEmpregados));
                    newRow["QtdExamesRealizadosJun"] = GetValor(Convert.ToInt32(row["Jun"]));
                    newRow["PorcExamesRealizadosJun"] = GetValorPct(GetTornover(Convert.ToDouble(row["Jun"]), qtdEmpregados));
                    newRow["QtdExamesRealizadosJul"] = GetValor(Convert.ToInt32(row["Jul"]));
                    newRow["PorcExamesRealizadosJul"] = GetValorPct(GetTornover(Convert.ToDouble(row["Jul"]), qtdEmpregados));
                    newRow["QtdExamesRealizadosAgo"] = GetValor(Convert.ToInt32(row["Ago"]));
                    newRow["PorcExamesRealizadosAgo"] = GetValorPct(GetTornover(Convert.ToDouble(row["Ago"]), qtdEmpregados));
                    newRow["QtdExamesRealizadosSet"] = GetValor(Convert.ToInt32(row["Set"]));
                    newRow["PorcExamesRealizadosSet"] = GetValorPct(GetTornover(Convert.ToDouble(row["Set"]), qtdEmpregados));
                    newRow["QtdExamesRealizadosOut"] = GetValor(Convert.ToInt32(row["Out"]));
                    newRow["PorcExamesRealizadosOut"] = GetValorPct(GetTornover(Convert.ToDouble(row["Out"]), qtdEmpregados));
                    newRow["QtdExamesRealizadosNov"] = GetValor(Convert.ToInt32(row["Nov"]));
                    newRow["PorcExamesRealizadosNov"] = GetValorPct(GetTornover(Convert.ToDouble(row["Nov"]), qtdEmpregados));
                    newRow["QtdExamesRealizadosDez"] = GetValor(Convert.ToInt32(row["Dez"]));
                    newRow["PorcExamesRealizadosDez"] = GetValorPct(GetTornover(Convert.ToDouble(row["Dez"]), qtdEmpregados));
                }
                else
                {
                    newRow["QtdExamesRealizadosJan"] = "-";
                    newRow["PorcExamesRealizadosJan"] = "-";
                    newRow["QtdExamesRealizadosFev"] = "-";
                    newRow["PorcExamesRealizadosFev"] = "-";
                    newRow["QtdExamesRealizadosMar"] = "-";
                    newRow["PorcExamesRealizadosMar"] = "-";
                    newRow["QtdExamesRealizadosAbr"] = "-";
                    newRow["PorcExamesRealizadosAbr"] = "-";
                    newRow["QtdExamesRealizadosMai"] = "-";
                    newRow["PorcExamesRealizadosMai"] = "-";
                    newRow["QtdExamesRealizadosJun"] = "-";
                    newRow["PorcExamesRealizadosJun"] = "-";
                    newRow["QtdExamesRealizadosJul"] = "-";
                    newRow["PorcExamesRealizadosJul"] = "-";
                    newRow["QtdExamesRealizadosAgo"] = "-";
                    newRow["PorcExamesRealizadosAgo"] = "-";
                    newRow["QtdExamesRealizadosSet"] = "-";
                    newRow["PorcExamesRealizadosSet"] = "-";
                    newRow["QtdExamesRealizadosOut"] = "-";
                    newRow["PorcExamesRealizadosOut"] = "-";
                    newRow["QtdExamesRealizadosNov"] = "-";
                    newRow["PorcExamesRealizadosNov"] = "-";
                    newRow["QtdExamesRealizadosDez"] = "-";
                    newRow["PorcExamesRealizadosDez"] = "-";
                }

                newRow["ExamesAno"] = qtdExamesAno;
                newRow["TurnoverAnual"] = TurnoverAnual;
                newRow["TurnoverCombinado"] = cliente.Turnover;

                ds.Tables[0].Rows.Add(newRow);
            }

			return ds;
        }

        private static int GetTotalExamesAno(DataRow row)
        {
            if (row == null)
                return 0;

            if (row["Jan"] == System.DBNull.Value)
                return 0;

            return Convert.ToInt32(row["Jan"])
                        + Convert.ToInt32(row["Fev"])
                        + Convert.ToInt32(row["Mar"])
                        + Convert.ToInt32(row["Abr"])
                        + Convert.ToInt32(row["Mai"])
                        + Convert.ToInt32(row["Jun"])
                        + Convert.ToInt32(row["Jul"])
                        + Convert.ToInt32(row["Ago"])
                        + Convert.ToInt32(row["Set"])
                        + Convert.ToInt32(row["Out"])
                        + Convert.ToInt32(row["Nov"])
                        + Convert.ToInt32(row["Dez"]);
        }

        #endregion

        #region GetTornover

        private double GetTornover(double NumAdmMaisNumDem, double NumFunciariosMesAnterior)
        {
            /*
             * http://www.via6.com/topico.php?cid=5296&tid=37123
             * 
             * Nº de admissões no mês + Nº de demissões no mês, este valor divide-se por 2 e 
             * seu resultado é dividido pelo Nº de funcionários do mês anterior, por fim 
             * multiplicamos por cem para encontrarmos o valr em percentil.
             **/

            if (NumFunciariosMesAnterior == 0)
                return 0;

            double turover = (NumAdmMaisNumDem / 2 / NumFunciariosMesAnterior) * 100.0D;

            return turover;
        }

        #endregion

        #region GetValor

        private string GetValor(int Val)
        {
            if (Val == 0)
                return "-";
            else
                return Val.ToString();
        }
        #endregion

        #region GetValorPct

        private string GetValorPct(double Val)
        {
            string ret;

            if (Val == 0)
                ret = "-";
            else
            {
                System.Globalization.NumberFormatInfo 
                    nfi = new System.Globalization.CultureInfo("pt-BR", false).NumberFormat;

                //Format the number to 1 decimal places.
                nfi.NumberDecimalDigits = 1;

                ret = Val.ToString("n", nfi);
            }

            return ret;
        }
        #endregion

        #region GetDataSetExamesRealizados

        private DataSet GetDataSetExamesRealizados()
        {
            StringBuilder query = new StringBuilder();

            query.Append("USE " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + " "
                        + " SELECT 	IdCliente,"
                        + " [1] AS Jan,   "
                        + " [2] As Fev,   "
                        + " [3] As Mar,   "
                        + " [4] As Abr,   "
                        + " [5] As Mai,   "
                        + " [6] As Jun,   "
                        + " [7] As Jul,   "
                        + " [8] As Ago,   "
                        + " [9] As [Set], "
                        + " [10] As [Out], "
                        + " [11] As Nov,  "
                        + " [12] AS Dez  "
                        + " FROM  (SELECT IdCliente, IdExameBase, Month(DataExame) AS Mes "
                        + " FROM qryExamesRealizados  "
                        + " WHERE YEAR(DataExame)=" + anoExame
                        + " 		AND IndResultado <>0 "
                        + " AND IdExameDicionario IN (" + (int)IndExameClinico.Admissional
                        + "," + (int)IndExameClinico.Demissional);

            if (IncluirMudFuncaoRetTrab)
                query.Append("," + (int)IndExameClinico.MudancaDeFuncao
                            + "," + (int)IndExameClinico.RetornoAoTrabalho);

            query.Append( ")"
                        + " 	) p "
                        + " PIVOT ( COUNT(IdExameBase) FOR Mes IN ( [1], [2], [3], [4], [5], [6], [7], [8], [9], [10], [11], [12] ) ) "
                        + " AS pvt");

            DataSet dsExamesRealizados;

            dsExamesRealizados = new Clinico().ExecuteDataset(query.ToString());

            return dsExamesRealizados;
        }

        #endregion

        #region GetEmpregados

        private DataSet GetDataSetEmpregados()
        {
            StringBuilder query = new StringBuilder();

            query.Append("USE sied_novo "
                        + " SELECT 	IdCliente,"
                        + " [1] AS Jan,   "
                        + " [2] As Fev,   "
                        + " [3] As Mar,   "
                        + " [4] As Abr,   "
                        + " [5] As Mai,   "
                        + " [6] As Jun,   "
                        + " [7] As Jul,   "
                        + " [8] As Ago,   "
                        + " [9] As [Set], "
                        + " [10] As [Out], "
                        + " [11] As Nov,  "
                        + " [12] AS Dez  "
                        + " FROM  ("
                        + " SELECT nID_EMPR as IdCliente, nID_EMPREGADO, Month(hDT_ADM) AS Mes  "
                        + "         FROM tblEMPREGADO   "
                        + "         WHERE YEAR(hDT_ADM)=" + anoExame
                        + " UNION ALL "
                        + " SELECT nID_EMPR as IdCliente, nID_EMPREGADO, Month(hDT_DEM) AS Mes  "
                        + "         FROM tblEMPREGADO   "
                        + "         WHERE YEAR(hDT_DEM)=" + anoExame
                        + ") p  "
                        + " PIVOT ( COUNT(nID_EMPREGADO) "
                        + "		FOR Mes IN ( [1], [2], [3], [4], [5], [6], [7], [8], [9], [10], [11], [12] ) "
                        + ")  AS pvt");

            return new Clinico().ExecuteDataset(query.ToString());
        }

        #endregion

        #region GetTable

        private static DataTable GetTable()
        {
            DataTable table = new DataTable("Result");
            table.Columns.Add("NomeCliente", Type.GetType("System.String"));
            table.Columns.Add("PeriodoAno", Type.GetType("System.String"));
            table.Columns.Add("QtdEmpregados", Type.GetType("System.Int32"));
            table.Columns.Add("QtdExamesRealizadosJan", Type.GetType("System.String"));
            table.Columns.Add("QtdExamesRealizadosFev", Type.GetType("System.String"));
            table.Columns.Add("QtdExamesRealizadosMar", Type.GetType("System.String"));
            table.Columns.Add("QtdExamesRealizadosAbr", Type.GetType("System.String"));
            table.Columns.Add("QtdExamesRealizadosMai", Type.GetType("System.String"));
            table.Columns.Add("QtdExamesRealizadosJun", Type.GetType("System.String"));
            table.Columns.Add("QtdExamesRealizadosJul", Type.GetType("System.String"));
            table.Columns.Add("QtdExamesRealizadosAgo", Type.GetType("System.String"));
            table.Columns.Add("QtdExamesRealizadosSet", Type.GetType("System.String"));
            table.Columns.Add("QtdExamesRealizadosOut", Type.GetType("System.String"));
            table.Columns.Add("QtdExamesRealizadosNov", Type.GetType("System.String"));
            table.Columns.Add("QtdExamesRealizadosDez", Type.GetType("System.String"));
            table.Columns.Add("PorcExamesRealizadosJan", Type.GetType("System.String"));
            table.Columns.Add("PorcExamesRealizadosFev", Type.GetType("System.String"));
            table.Columns.Add("PorcExamesRealizadosMar", Type.GetType("System.String"));
            table.Columns.Add("PorcExamesRealizadosAbr", Type.GetType("System.String"));
            table.Columns.Add("PorcExamesRealizadosMai", Type.GetType("System.String"));
            table.Columns.Add("PorcExamesRealizadosJun", Type.GetType("System.String"));
            table.Columns.Add("PorcExamesRealizadosJul", Type.GetType("System.String"));
            table.Columns.Add("PorcExamesRealizadosAgo", Type.GetType("System.String"));
            table.Columns.Add("PorcExamesRealizadosSet", Type.GetType("System.String"));
            table.Columns.Add("PorcExamesRealizadosOut", Type.GetType("System.String"));
            table.Columns.Add("PorcExamesRealizadosNov", Type.GetType("System.String"));
            table.Columns.Add("PorcExamesRealizadosDez", Type.GetType("System.String"));
            table.Columns.Add("ExamesAno", Type.GetType("System.Int32"));
            table.Columns.Add("TurnoverAnual", Type.GetType("System.Double"));
            table.Columns.Add("TurnoverCombinado", Type.GetType("System.Double"));
            return table;
        }
        #endregion
    }
}
