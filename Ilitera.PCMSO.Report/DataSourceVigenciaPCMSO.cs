using System;
using System.Data;
using System.Collections;

using Ilitera.Common;
using Ilitera.Opsa.Data;

namespace Ilitera.PCMSO.Report
{
    public class DataSourceVigenciaPCMSO : DataSourceBase
	{
		public DataSourceVigenciaPCMSO()
		{			

		}

		public RptVigenciaPcmso GetReportVigenciaPCMSO()
		{
			RptVigenciaPcmso report = new RptVigenciaPcmso();
			report.SetDataSource(GetVigenciaPCMSO());
			report.Refresh();
            SetTempoProcessamento(report);
			return report;
		}

		private DataSet GetVigenciaPCMSO()
		{
			DataSet ds = new DataSet();
			DataTable table = new DataTable("Result");
			table.Columns.Add("Ordem", Type.GetType("System.String"));
			table.Columns.Add("NomeCliente", Type.GetType("System.String"));
			table.Columns.Add("ConfQtdEmpregados", Type.GetType("System.String"));
			table.Columns.Add("ConfTurnover", Type.GetType("System.String"));
			table.Columns.Add("PCMSOInicio", Type.GetType("System.DateTime"));
			table.Columns.Add("PCMSOTermino", Type.GetType("System.DateTime"));
			table.Columns.Add("PCMSOMedico", Type.GetType("System.String"));
			table.Columns.Add("Status", Type.GetType("System.String"));			
			ds.Tables.Add(table);
			DataRow newRow;

            ArrayList listCliente = new Cliente().Find("ContrataPCMSO = 1"
                + " AND IdCliente IN (SELECT IdCliente FROM qryClienteAtivos)"
                + " ORDER BY NomeAbreviado");

            int i = 0;

            foreach(Cliente cliente in listCliente)
            {
                newRow = ds.Tables[0].NewRow();

                newRow["Ordem"] = i + 1;
                newRow["NomeCliente"] = cliente.NomeAbreviado;
                newRow["ConfQtdEmpregados"] = cliente.QtdEmpregados;
                newRow["ConfTurnover"] = cliente.Turnover.ToString();

                Pcmso pcmso = cliente.GetUltimoPcmso();

                if (pcmso.Id != 0)
                {
                    newRow["PCMSOInicio"] = pcmso.DataPcmso;
                    newRow["PCMSOTermino"] = pcmso.GetTerninoPcmso();
                    newRow["PCMSOMedico"] = pcmso.IdCoordenador.ToString();
                }

                if (pcmso.IsFinalizado)
                    newRow["Status"] = "Finalizado";
                else
                    newRow["Status"] = "-";

                ds.Tables[0].Rows.Add(newRow);
            }
			return ds;
		}		
	}
}
