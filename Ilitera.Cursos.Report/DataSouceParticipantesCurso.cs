using System;
using System.Data;
using System.Collections;
using Ilitera.Opsa.Data;
using Ilitera.Common;

namespace Ilitera.Cursos.Report
{
	public class DataSouceParticipantesCurso : DataSourceBase
	{
		private Treinamento treinamento;

		public DataSouceParticipantesCurso(Treinamento treinamento)
		{
			this.treinamento = treinamento;
		}

		public RptParticipantesCurso GetReport()
		{
			RptParticipantesCurso report = new RptParticipantesCurso();
			report.SetDataSource(GetParticipantesCurso());
			report.Refresh();

            SetTempoProcessamento(report);

			return report;
		}

		private DataSet GetParticipantesCurso()
		{
			DataRow newRow;

            DataSet ds = new DataSet();
			ds.Tables.Add(GetTable());

			ArrayList alParticipantes = new ParticipanteTreinamento().Find("IdTreinamento=" + treinamento.Id);

			foreach(ParticipanteTreinamento participante in alParticipantes)
			{
				newRow = ds.Tables[0].NewRow();

                newRow["Titulo"] =  GetTitulo();
				newRow["DataTreinamento"] = treinamento.Periodo;
				newRow["NomeEmpresa"] = treinamento.IdCliente.ToString();
				newRow["NomeParticipante"] = participante.ToString();
                newRow["Identidade"] = participante.GetIdentidadeParticipante();

				ds.Tables[0].Rows.Add(newRow);
			}

			DataRow[] rows = ds.Tables[0].Select(string.Empty, "NomeParticipante");

            DataSet sortedDS = new DataSet();
            sortedDS.Merge(rows);

            return sortedDS;
		}

        private string GetTitulo()
        {
            string ret;

            if (treinamento.IdTreinamentoDicionario.mirrorOld == null)
                treinamento.IdTreinamentoDicionario.Find();

            if (treinamento.IdTreinamentoDicionario.IdObrigacao.mirrorOld == null)
                treinamento.IdTreinamentoDicionario.IdObrigacao.Find();


            if (treinamento.IsFromCliente)
                ret = "Participantes do  '" + treinamento.IdTreinamentoDicionario.Nome + "'";
            else
                ret =  "Participantes do  '" + treinamento.IdTreinamentoDicionario.IdObrigacao.Nome + "'";

            return ret;
        }

        private static DataTable GetTable()
        {
            DataTable table = new DataTable("Result");
            table.Columns.Add("Titulo", typeof(string));
            table.Columns.Add("DataTreinamento", typeof(string));
            table.Columns.Add("NomeEmpresa", typeof(string));
            table.Columns.Add("NomeParticipante", typeof(string));
            table.Columns.Add("Identidade", typeof(string));
            return table;
        }	
	}
}
