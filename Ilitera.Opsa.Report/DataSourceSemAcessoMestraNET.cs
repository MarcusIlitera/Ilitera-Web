using System;
using System.Data;
using Ilitera.Common;
using Ilitera.Opsa.Data;

namespace Ilitera.Opsa.Report
{
	public class DataSourceSemAcessoMestraNET : DataSourceBase
	{
		private string Mes, Ano, NumAcessoLimite;
		
		public DataSourceSemAcessoMestraNET(string Mes, string Ano, string NumAcessoLimite)
		{
			this.Mes = Mes;
			this.Ano = Ano;
			this.NumAcessoLimite = NumAcessoLimite;
		}

		public RptClienteSemAcessoMestraNET GetReport()
		{
			RptClienteSemAcessoMestraNET report = new RptClienteSemAcessoMestraNET();
			report.SetDataSource(GetClienteSemAcesso());
			report.Refresh();

            SetTempoProcessamento(report);

			return report;
		}

		private DataSet GetClienteSemAcesso()
		{
			DataSet ds = new DataSet();
			DataRow newRow;	
			DataTable table = new DataTable("Result");
			table.Columns.Add("Mes", typeof(string));
			table.Columns.Add("Ano", typeof(string));
			table.Columns.Add("NumAcessoLimite", typeof(string));
			table.Columns.Add("NomeCompleto", typeof(string));
			ds.Tables.Add(table);
					
			DataSet dsSemAcesso = new Cliente().ExecuteDataset("EXEC  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.sps_ClienteSemAcessoMestraNET " + Mes + ", " + Ano + ", " + NumAcessoLimite);

			foreach(DataRow row in dsSemAcesso.Tables[0].Select())
			{
				newRow = ds.Tables[0].NewRow();	
				newRow["Mes"] = Mes;
				newRow["Ano"] = Ano;
				newRow["NumAcessoLimite"] = NumAcessoLimite;
				newRow["NomeCompleto"] = row["NomeAbreviado"] + " / " + row["NomeCompleto"];
				ds.Tables[0].Rows.Add(newRow);
			}

			return ds;
		}
	}
}
