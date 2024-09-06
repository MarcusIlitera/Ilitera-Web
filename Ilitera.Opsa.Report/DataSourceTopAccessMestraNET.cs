using System;
using System.Data;
using Ilitera.Common;
using Ilitera.Opsa.Data;

namespace Ilitera.Opsa.Report
{
	public class DataSourceTopAccessMestraNET : DataSourceBase
	{
		private string Mes, Ano;
		
		public DataSourceTopAccessMestraNET(string Mes, string Ano)
		{
			this.Mes = Mes;
			this.Ano = Ano;
		}

        //public RptTopAcessMestraNET GetReport()
        //{
        //    RptTopAcessMestraNET report = new RptTopAcessMestraNET();
        //    report.SetDataSource(GetTopAcessos());
        //    report.Refresh();

        //    SetTempoProcessamento(report);

        //    return report;
        //}
	
		private DataSet GetTopAcessos()
		{
			int posicao = 1;
			DataSet ds = new DataSet();
			DataRow newRow;	
			DataTable table = new DataTable("Result");
			table.Columns.Add("Mes", typeof(string));
			table.Columns.Add("Ano", typeof(string));
			table.Columns.Add("NumAcessos", typeof(int));
			table.Columns.Add("NomeCompleto", typeof(string));
			ds.Tables.Add(table);
							
			DataSet dsAcessos = new Usuario().ExecuteDataset("EXEC  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.sps_ClienteAcessoMestraNET " + Mes + ", " + Ano);

			foreach(DataRow row in dsAcessos.Tables[0].Select())
			{
				newRow = ds.Tables[0].NewRow();	
				newRow["Mes"] = Mes;
				newRow["Ano"] = Ano;
				newRow["NumAcessos"] = row["NumAcesso"];
				newRow["NomeCompleto"]	= posicao.ToString() + "º. " + row["NomeCompleto"] + " / " + row["NomeAbreviado"];
				ds.Tables[0].Rows.Add(newRow);
				posicao += 1;
			}

			return ds;
		}
	}
}
