using System;
using System.Data;
using Ilitera.Common;
using Ilitera.Opsa.Data;
using System.Text;

namespace Ilitera.Opsa.Report
{
	public class DataSourceListaCompromisso : DataSourceBase
	{
		private string Dia, Mes, Ano;
		private Prestador prestador;
		private bool showParticular;
		
		public DataSourceListaCompromisso(string Dia, string Mes, string Ano, Prestador prestador, bool showParticular)
		{
			this.Dia = Dia;
			this.Mes = Mes;
			this.Ano = Ano;
			this.prestador = prestador;
			this.showParticular = showParticular;
		}

		public RptListaCompromisso GetReport()
		{
			RptListaCompromisso report = new RptListaCompromisso();
			report.SetDataSource(GetListaCompromisso());
			report.Refresh();

            SetTempoProcessamento(report);

			return report;
		}

		private DataSet GetListaCompromisso()
		{
			DataSet ds = new DataSet();
			DataRow newRow;	
			DataTable table = new DataTable("Result");
			table.Columns.Add("Dia", typeof(string));
			table.Columns.Add("Mes", typeof(string));
			table.Columns.Add("Ano", typeof(string));
			table.Columns.Add("NomePrestador", typeof(string));
			table.Columns.Add("Compromisso", typeof(string));
			ds.Tables.Add(table);

			StringBuilder sqlstmCompromisso = new StringBuilder();
			sqlstmCompromisso.Append("IdPessoa=" + prestador.IdPessoa.Id);
			sqlstmCompromisso.Append(" AND DataInicio>='" + Ano + "-" + Mes + "-" + Dia + " 00:00:00.000'");
			sqlstmCompromisso.Append(" AND DataInicio<='" + Ano + "-" + Mes + "-" + Dia + " 23:59:59.999'");
			sqlstmCompromisso.Append(" AND HorarioDisponivel=0");
			if (!showParticular)
				sqlstmCompromisso.Append(" AND Particular=0");
			sqlstmCompromisso.Append(" ORDER BY DataInicio");

			DataSet dsCompromisso = new Compromisso().Get(sqlstmCompromisso.ToString());

			foreach(DataRow row in dsCompromisso.Tables[0].Select())
			{
				StringBuilder strCompromisso = new StringBuilder();
				strCompromisso.Append("<Font color='#004000'><b>");
				strCompromisso.Append(Convert.ToDateTime(row["DataInicio"]).ToString("t"));
				strCompromisso.Append(" às ");
				strCompromisso.Append(Convert.ToDateTime(row["DataTermino"]).ToString("t"));
				strCompromisso.Append("</b> - ");
				strCompromisso.Append(row["Assunto"].ToString().Trim());
				if (!row["Descricao"].ToString().Trim().Equals(string.Empty))
				{
					strCompromisso.Append(" - ");
					strCompromisso.Append(row["Descricao"].ToString().Trim());
				}
				strCompromisso.Append("</Font>");
		
				newRow = ds.Tables[0].NewRow();	
				newRow["Dia"] = Dia;
				newRow["Mes"] = Mes;
				newRow["Ano"] = Ano;
				newRow["NomePrestador"] = prestador.NomeCompleto;
				newRow["Compromisso"] = strCompromisso.ToString();
		
				ds.Tables[0].Rows.Add(newRow);
			}

			return ds;
		}
	}
}
