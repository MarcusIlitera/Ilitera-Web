using System;
using System.Data;
using System.Collections;
using Ilitera.Opsa.Data;
using Ilitera.Common;

namespace Ilitera.Cursos.Report
{
	public class DataSourceCursoEmpresa : DataSourceBase
	{
		private Cliente cliente;

		public DataSourceCursoEmpresa(Cliente cliente)
		{
			this.cliente = cliente;
		}

		public RptCursoEmpresa GetReport()
		{
			RptCursoEmpresa report = new RptCursoEmpresa();
			report.SetDataSource(GetCursoEmpresa());
			report.Refresh();

            SetTempoProcessamento(report);

			return report;
		}

		private DataSet GetCursoEmpresa()
		{
			DataSet ds = new DataSet();
			DataTable table = new DataTable("Result");
			table.Columns.Add("NomeEmpresa", Type.GetType("System.String"));
			table.Columns.Add("DataCurso", Type.GetType("System.String"));			
			table.Columns.Add("NomeCurso", Type.GetType("System.String"));						
			ds.Tables.Add(table);
			DataRow newRow;	
			ArrayList listTreinamento = new Treinamento().Find("IdCliente=" + cliente.Id + " ORDER BY DataLevantamento DESC");
			foreach(Treinamento treinamento in listTreinamento)
			{
				treinamento.IdTreinamentoDicionario.Find();

				newRow = ds.Tables[0].NewRow();							
				newRow["NomeEmpresa"] = cliente.NomeCompleto;
				newRow["DataCurso"] = treinamento.Periodo;
				
				if (treinamento.IsFromCliente)
					newRow["NomeCurso"] = treinamento.IdTreinamentoDicionario.Nome;
				else
				{
					treinamento.IdTreinamentoDicionario.IdObrigacao.Find();
                    
					newRow["NomeCurso"] = treinamento.IdTreinamentoDicionario.IdObrigacao.Nome;
				}

				ds.Tables[0].Rows.Add(newRow);
			}
			return ds;
		}
	}
}
