using System;
using System.Text;
using System.Data;
using System.Collections;
using Ilitera.Common;
using Ilitera.Opsa.Data;

namespace Ilitera.Opsa.Report
{

	public class DataSourceMalaDiretaImposto
	{
		private Prospeccao prospeccao;

		public DataSourceMalaDiretaImposto(Prospeccao prospeccao)
		{
			this.prospeccao = prospeccao;
		}

		public RptMalaDiretaImposto GetReport()
		{
			RptMalaDiretaImposto report = new RptMalaDiretaImposto();	
			report.SetDataSource(GetDataSource());
			report.Refresh();
			return report;
		}

		private DataSet GetDataSource()
		{
			DataSet ds = new DataSet();
			DataTable table = new DataTable("Result");
			table.Columns.Add("NomeCompleto", Type.GetType("System.String"));
			table.Columns.Add("Endereco", Type.GetType("System.String"));
			table.Columns.Add("BairroCidadeUF", Type.GetType("System.String"));
			table.Columns.Add("NumeroEmpregados" , Type.GetType("System.Int32"));
			table.Columns.Add("SalarioMedio", Type.GetType("System.Decimal"));
			ds.Tables.Add(table);

			Endereco endereco = prospeccao.GetEndereco();

			DataRow newRow = ds.Tables[0].NewRow();
			newRow["NomeCompleto"]		= prospeccao.NomeCompleto;
			newRow["Endereco"]			= endereco.GetEnderecoCompletoPorLinha();
			newRow["BairroCidadeUF"]	= string.Empty;
			newRow["NumeroEmpregados"]	= prospeccao.QtdEmpregados;
			newRow["SalarioMedio"]		= (prospeccao.SalarioMedio==0)?600M:prospeccao.SalarioMedio;
			ds.Tables[0].Rows.Add(newRow);

			return ds;
		}	
	}
}
