using System;
using System.Data;
using System.Collections;
using Ilitera.Opsa.Data;
using Ilitera.Common;

namespace Ilitera.Cursos.Report
{
	public class DataSourceFichaEmpregado : DataSourceBase
	{
		private Empregado empregado;

		public DataSourceFichaEmpregado(Empregado empregado)
		{
			this.empregado = empregado;
			this.empregado.nID_EMPR.Find();
		}

		public RptFichaEmpregado GetReport()
		{
			RptFichaEmpregado report = new RptFichaEmpregado();
			report.OpenSubreport("TreinamentosSolicitados").SetDataSource(GetTreinamentosSolicitados());
			report.SetDataSource(GetExamesRealizados());
			report.Refresh();

            SetTempoProcessamento(report);

			return report;
		}

		private DataSet GetExamesRealizados()
		{
			DataSet ds = new DataSet();
			DataTable table = new DataTable("Result");
			table.Columns.Add("NomeEmpresa", Type.GetType("System.String"));
			table.Columns.Add("NomeEmpregado", Type.GetType("System.String"));			
			table.Columns.Add("NomeCurso", Type.GetType("System.String"));
			table.Columns.Add("Periodo", typeof(string));		
			ds.Tables.Add(table);
			DataRow newRow;

			ArrayList alTreinamentosRealizados = new Treinamento().Find("IdTreinamento IN (SELECT IdTreinamento FROM ParticipanteTreinamento WHERE IdEmpregado=" + empregado.Id + ")"
				+" ORDER BY DataLevantamento DESC");

			foreach (Treinamento treinamentoRealizado in alTreinamentosRealizados)
			{
				treinamentoRealizado.IdTreinamentoDicionario.Find();
				
				newRow = ds.Tables[0].NewRow();
				newRow["NomeEmpresa"] = empregado.nID_EMPR.NomeCompleto;
				newRow["NomeEmpregado"] = empregado.tNO_EMPG;
				
				if (treinamentoRealizado.IsFromCliente)
					newRow["NomeCurso"] = treinamentoRealizado.IdTreinamentoDicionario.Nome;
				else
				{
					treinamentoRealizado.IdTreinamentoDicionario.IdObrigacao.Find();

					newRow["NomeCurso"] = "*" + treinamentoRealizado.IdTreinamentoDicionario.IdObrigacao.Nome;
				}

				newRow["Periodo"] = treinamentoRealizado.Periodo;
				ds.Tables[0].Rows.Add(newRow);
			}

			if (alTreinamentosRealizados.Count.Equals(0))
			{
				newRow = ds.Tables[0].NewRow();
				newRow["NomeEmpresa"] = empregado.nID_EMPR.NomeCompleto;
				newRow["NomeEmpregado"] = empregado.tNO_EMPG;
				ds.Tables[0].Rows.Add(newRow);
			}

			return ds;
		}

		private DataSet GetTreinamentosSolicitados()
		{
			DataSet ds = new DataSet();
			DataTable table = new DataTable("Result");
			table.Columns.Add("NomeEmpregado", Type.GetType("System.String"));			
			table.Columns.Add("NomeCurso", Type.GetType("System.String"));						
			ds.Tables.Add(table);
			DataRow newRow;	

			ArrayList listTreinamento = new TreinamentoDicionarioEmpregado().Find("IdEmpregado=" + empregado.Id);

			foreach(TreinamentoDicionarioEmpregado treinamentoEmpregado in listTreinamento)
			{
				treinamentoEmpregado.IdTreinamentoDicionario.Find();

				newRow = ds.Tables[0].NewRow();							
				newRow["NomeEmpregado"] = empregado.tNO_EMPG;
								
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
