using System;
using System.Data;
using System.Collections;
using Ilitera.Opsa.Data;
using Ilitera.Common;

namespace Ilitera.OrdemServico.Report
{	
	public class DataSourceConjuntoProc : DataSourceBase
	{
		public DataSourceConjuntoProc()
		{			
		}

		public ReportConjuntoDeProcedimentos GetReportConjProc(Cliente cliente)
		{
			ReportConjuntoDeProcedimentos report = new ReportConjuntoDeProcedimentos();
			report.SetDataSource(GetConjProc(cliente));
            report.Refresh();

            SetTempoProcessamento(report);

			return report;
		}

		private DataSet GetConjProc(Cliente cliente)
		{
			DataSet ds = new DataSet();
			DataTable table = new DataTable("Result");
			table.Columns.Add("NomeEmpresa", Type.GetType("System.String"));
			table.Columns.Add("NomeConjProc", Type.GetType("System.String"));	
			table.Columns.Add("NumeroPOPS", Type.GetType("System.String"));	
			table.Columns.Add("NomeProcedimento", Type.GetType("System.String"));
			ds.Tables.Add(table);
			DataRow newRow;	
			ArrayList listConjunto = new Conjunto().Find("IdCliente=" + cliente.Id);
			foreach(Conjunto conjunto in listConjunto)
			{				
				ArrayList listProc = new Procedimento().Find("IdProcedimento IN (SELECT IdProcedimento FROM ConjuntoProcedimento WHERE IdConjunto=" + conjunto.Id + ")"
					+" ORDER BY Numero");

				foreach(Procedimento procedimento in listProc)
				{					
					newRow = ds.Tables[0].NewRow();						
					newRow["NomeEmpresa"] = cliente.NomeAbreviado;
					newRow["NomeConjProc"]  = conjunto.Nome;
					newRow["NumeroPOPS"]  = procedimento.Numero.ToString("0000");
					newRow["NomeProcedimento"]  = procedimento.Nome;
					ds.Tables[0].Rows.Add(newRow);				
				}
			}
			return ds;
		}
	}
}
