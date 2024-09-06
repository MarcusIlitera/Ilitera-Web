using System;
using System.Collections;
using System.Data;
using Ilitera.Opsa.Data;
using Ilitera.Common;
using System.Text;

namespace Ilitera.Opsa.Report
{	
	public class DataSourceListaEmpregado : DataSourceBase
	{
		private Cliente cliente = null;
		private string tipoSort;
		private string mes;
		private string NomeMes;
		private bool isAlocado;
		private string strFiltro;

		public DataSourceListaEmpregado(Cliente cliente, string tipoSort, string mes, bool isAlocado, string strFiltro)
		{
			this.cliente = cliente;
			if (mes.Length > 13)
			{
				if (tipoSort == "NA" || tipoSort == "DA")
					this.tipoSort = "SemDataA";
				else if (tipoSort == "NN" || tipoSort == "DN")
					this.tipoSort = "SemDataN";
			}
			else
				this.tipoSort = tipoSort;
			this.mes = mes.Substring(0, 2);
			this.NomeMes = mes.Substring(3);
			this.isAlocado = isAlocado;
			this.strFiltro = strFiltro.Trim();
		}

		public RptListaEmpregados GetReportListaEmpregado()
		{
			RptListaEmpregados report = new RptListaEmpregados();
			report.SetDataSource(GetListaEmpregado());
			report.Refresh();

            SetTempoProcessamento(report);

			return report;
		}

		private DataSet GetListaEmpregado()
		{
			DataSet ds = new DataSet();
			DataTable table = new DataTable("Result");
			table.Columns.Add("TipoRela", Type.GetType("System.String"));
			table.Columns.Add("MesReferencia", Type.GetType("System.String"));
			table.Columns.Add("NumEmpregado", Type.GetType("System.String"));
			table.Columns.Add("NomeEmpresa", Type.GetType("System.String"));			
			table.Columns.Add("NomeEmpregado", Type.GetType("System.String"));
			table.Columns.Add("Data", Type.GetType("System.String"));			
			ds.Tables.Add(table);
			DataRow newRow;
			DataSet dsEmpregado = new DataSet();
			StringBuilder strSQL = new StringBuilder();
			
			if (!isAlocado)
			{
				strSQL.Append("nID_EMPR=" + cliente.Id);
				strSQL.Append(" AND gTERCEIRO=0");
				strSQL.Append(" AND hDT_DEM IS NULL");
			
				if (!strFiltro.Equals(string.Empty))
					strSQL.Append(" AND UPPER(tNO_EMPG) COLLATE SQL_Latin1_General_Cp1251_CS_AS LIKE '%" + strFiltro.ToUpper() + "%'");
				
				if(tipoSort == "NA")
					strSQL.Append(" AND MONTH(hDT_ADM)='" + mes + "' ORDER BY tNO_EMPG");
				else if ( tipoSort == "NN")
					strSQL.Append(" AND MONTH(hDT_NASC)='" + mes + "' ORDER BY tNO_EMPG");
				else if (tipoSort == "DA")
					strSQL.Append(" AND MONTH(hDT_ADM)='" + mes + "' ORDER BY hDT_ADM");
				else if (tipoSort == "DN")
					strSQL.Append(" AND MONTH(hDT_NASC)='" + mes + "' ORDER BY hDT_NASC");
				else if (tipoSort == "SemDataA")
					strSQL.Append(" AND hDT_ADM IS NULL ORDER BY tNO_EMPG");
				else if (tipoSort == "SemDataN")
					strSQL.Append(" AND hDT_NASC IS NULL ORDER BY tNO_EMPG");

				dsEmpregado = new Empregado().Get(strSQL.ToString());
			}
			else
			{
				if (strFiltro.Equals(string.Empty))
					strSQL.Append("EXEC " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.sps_EmpregadoFuncaoMes " + cliente.Id + ", NULL, " + mes + ", '" + tipoSort + "'");
				else
					strSQL.Append("EXEC " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.sps_EmpregadoFuncaoMes " + cliente.Id + ", '" + strFiltro.ToUpper() + "', " + mes + ", '" + tipoSort + "'");

				dsEmpregado = new Empregado().ExecuteDataset(strSQL.ToString());
			}

			if (tipoSort == "NA" || tipoSort == "DA")
				foreach(DataRow rowEmpregado in dsEmpregado.Tables[0].Select())
				{
					newRow = ds.Tables[0].NewRow();
					newRow["TipoRela"] = this.tipoSort;
					newRow["MesReferencia"] = this.NomeMes;
					newRow["NumEmpregado"] = dsEmpregado.Tables[0].Rows.Count.ToString();
					newRow["NomeEmpresa"] = this.cliente.NomeCompleto;
					newRow["NomeEmpregado"] = rowEmpregado["tNO_EMPG"];
					newRow["Data"] = Convert.ToDateTime(rowEmpregado["hDT_ADM"]).ToString("dd-MM-yyyy");
					ds.Tables[0].Rows.Add(newRow);
				}
			else if (tipoSort == "NN" || tipoSort == "DN")
				foreach(DataRow rowEmpregado in dsEmpregado.Tables[0].Select())
				{
					newRow = ds.Tables[0].NewRow();
					newRow["TipoRela"] = this.tipoSort;
					newRow["MesReferencia"] = this.NomeMes;
					newRow["NumEmpregado"] = dsEmpregado.Tables[0].Rows.Count.ToString();
					newRow["NomeEmpresa"] = this.cliente.NomeCompleto;
					newRow["NomeEmpregado"] = rowEmpregado["tNO_EMPG"];
					newRow["Data"] = Convert.ToDateTime(rowEmpregado["hDT_NASC"]).ToString("dd-MM-yyyy");
					ds.Tables[0].Rows.Add(newRow);
				}
			else
				foreach(DataRow rowEmpregado in dsEmpregado.Tables[0].Select())
				{
					newRow = ds.Tables[0].NewRow();
					newRow["TipoRela"] = this.tipoSort;
					newRow["MesReferencia"] = "-------";
					newRow["NumEmpregado"] = dsEmpregado.Tables[0].Rows.Count.ToString();
					newRow["NomeEmpresa"] = this.cliente.NomeCompleto;
					newRow["NomeEmpregado"] = rowEmpregado["tNO_EMPG"];
					newRow["Data"] = "Sem Data";
					ds.Tables[0].Rows.Add(newRow);
				}

			return ds;
		}		
	}
}
