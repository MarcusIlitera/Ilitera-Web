using System;
using System.Data;
using System.Collections;
using Ilitera.Opsa.Data;
using Ilitera.Common;

namespace Ilitera.Cursos.Report
{
	public class DataSourceConfiguracaoEmpregados : DataSourceBase
	{
		private Cliente cliente;

		public DataSourceConfiguracaoEmpregados(Cliente cliente)
		{
			this.cliente = cliente;
		}

		public RptConfiguracaoEmpregados GetReport()
		{
			RptConfiguracaoEmpregados report = new RptConfiguracaoEmpregados();
			report.SetDataSource(GetConfiguracaoEmpregados());
			report.Refresh();

            SetTempoProcessamento(report);

			return report;
		}

		private DataSet GetConfiguracaoEmpregados()
		{
			DataSet ds = new DataSet();
			DataTable table = new DataTable("Result");
			table.Columns.Add("NomeEmpresa", Type.GetType("System.String"));
			table.Columns.Add("NomeEmpregado", Type.GetType("System.String"));			
			table.Columns.Add("NomeCurso", Type.GetType("System.String"));						
			ds.Tables.Add(table);
			DataRow newRow;	

			ArrayList listTreinamento = new TreinamentoDicionarioEmpregado().Find("IdEmpregado IN (SELECT nID_EMPREGADO FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEMPREGADO WHERE nID_EMPR=" + cliente.Id
				+" AND gTERCEIRO=0 AND hDT_DEM IS NULL)");

			foreach(TreinamentoDicionarioEmpregado treinamentoEmpregado in listTreinamento)
			{
				treinamentoEmpregado.IdEmpregado.Find();
				treinamentoEmpregado.IdTreinamentoDicionario.Find();

				newRow = ds.Tables[0].NewRow();							
				newRow["NomeEmpresa"] = cliente.NomeCompleto;
				newRow["NomeEmpregado"] = treinamentoEmpregado.IdEmpregado.tNO_EMPG;
								
				if (!treinamentoEmpregado.IdTreinamentoDicionario.IdCliente.Id.Equals(310)) //Not from Mestra
					newRow["NomeCurso"] = treinamentoEmpregado.IdTreinamentoDicionario.Nome;
				else
				{
					treinamentoEmpregado.IdTreinamentoDicionario.IdObrigacao.Find();
                    
					newRow["NomeCurso"] = "*" + treinamentoEmpregado.IdTreinamentoDicionario.IdObrigacao.Nome;
				}

				ds.Tables[0].Rows.Add(newRow);
			}
			return ds;
		}
	}
}
