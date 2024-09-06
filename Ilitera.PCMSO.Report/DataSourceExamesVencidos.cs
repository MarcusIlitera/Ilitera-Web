using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;

using Ilitera.Common;
using Ilitera.Opsa.Data;

namespace Ilitera.PCMSO.Report
{
    public class DataSourceExamesVencidos : DataSourceBase
	{
		public DataSourceExamesVencidos()
		{			


		}

		public RptVencimentoExames GetReportExameVencidos()
		{
			RptVencimentoExames report = new RptVencimentoExames();
			report.SetDataSource(GetExamesVencidos());
			report.Refresh();
            SetTempoProcessamento(report);
			return report;
		}

		private DataSet GetExamesVencidos()
		{
			DataSet ds = new DataSet();
            DataTable table = GetTable();	
			ds.Tables.Add(table);
			DataRow newRow;

            List<Cliente> clientes = new Cliente().Find<Cliente>("ContrataPCMSO=1"
                                                            + " AND IdCliente IN (SELECT IdCliente FROM qryClienteAtivos)"
                                                            + " ORDER BY NomeAbreviado");

            foreach (Cliente cliente in clientes)
			{
				newRow = ds.Tables[0].NewRow();

                newRow["NomeCliente"] = cliente.NomeAbreviado;
                newRow["QtdEmpregados"] = cliente.QtdEmpregados;
				newRow["Turnover"] = cliente.Turnover.ToString();
				
                Pcmso pcmso = cliente.GetUltimoPcmso();

                int countExamesPlan = new ExamePlanejamento().ExecuteCount("IdPcmsoPlanejamento IN "
                    + " (SELECT IdPcmsoPlanejamento FROM PcmsoPlanejamento WHERE"
                    + " IdPcmso=" + pcmso.Id + ")"
                    + " AND IdExameDicionario IN"
                    + " (SELECT IdExameDicionario FROM ExameDicionario WHERE Periodico=1)");
				
                if(countExamesPlan > 0)
				{
                    int countExamesVencidos = new ExamePlanejamento().ExecuteCount("DataVencimento < DataProxima"
                                    + " AND IdPcmsoPlanejamento IN"
                                    + " (SELECT IdPcmsoPlanejamento FROM PcmsoPlanejamento WHERE"
                                    + " IdPcmso=" + pcmso.Id + ")"
                                    + " AND IdExameDicionario IN"
                                    + " (SELECT IdExameDicionario FROM ExameDicionario WHERE Periodico=1)");
					
                    newRow["ExamesVencidos"] = countExamesVencidos;				
					newRow["ExamesRealizados"] = countExamesPlan - countExamesVencidos;
					newRow["PorcExmesVencidos"] = ((Convert.ToDouble(countExamesVencidos) * 100) / Convert.ToDouble(countExamesPlan)).ToString("N") +  " % ";
					newRow["PorcExmesRealizados"] = ((Convert.ToDouble((countExamesPlan - countExamesVencidos)) * 100) / Convert.ToDouble(countExamesPlan)).ToString("N") +  " % ";;
				}
				else
				{
					newRow["ExamesVencidos"] = " - ";				
					newRow["ExamesRealizados"] = " - ";
					newRow["PorcExmesVencidos"] = " - ";
					newRow["PorcExmesRealizados"] = " - ";
				}
				ds.Tables[0].Rows.Add(newRow);			
			}
			return ds;
		}

        private static DataTable GetTable()
        {
            DataTable table = new DataTable("Result");
            table.Columns.Add("NomeCliente", Type.GetType("System.String"));
            table.Columns.Add("QtdEmpregados", Type.GetType("System.String"));
            table.Columns.Add("Turnover", Type.GetType("System.String"));
            table.Columns.Add("ExamesVencidos", Type.GetType("System.String"));
            table.Columns.Add("ExamesRealizados", Type.GetType("System.String"));
            table.Columns.Add("PorcExmesVencidos", Type.GetType("System.String"));
            table.Columns.Add("PorcExmesRealizados", Type.GetType("System.String"));
            return table;
        }		
	}
}
