using System;
using System.Collections;
using System.Data;
using Ilitera.Common;
using Ilitera.Opsa.Data;

namespace Ilitera.Auditoria.Report
{
	public class DataSourceIrregularidades : DataSourceBase
	{
		private Ilitera.Opsa.Data.Auditoria auditoria;
		private Cliente cliente;
		private string tipoListagem;

		public DataSourceIrregularidades(Ilitera.Opsa.Data.Auditoria auditoria, Cliente cliente) : this(auditoria, cliente, "Todas")
		{
		}

		public DataSourceIrregularidades(Ilitera.Opsa.Data.Auditoria auditoria, Cliente cliente, string tipoListagem)
		{
			this.auditoria = auditoria;
			this.cliente = cliente;
			this.tipoListagem = tipoListagem;
		}		

		public RptListaIrregularidades GetReportListaIrregularidades()
		{
			RptListaIrregularidades report = new RptListaIrregularidades();
			report.SetDataSource(GetListaIrregularidades());
			report.Refresh();

            SetTempoProcessamento(report);

			return report;
		}

		public RptListaIrregCompleta GetReportListaIrregCompleta()
		{
			RptListaIrregCompleta report = new RptListaIrregCompleta();
			report.SetDataSource(GetIrregularidadesDadosComletos());
			report.Refresh();

            SetTempoProcessamento(report);

			return report;
		}

		private DataSet GetListaIrregularidades()
		{
			DataSet ds = new DataSet();
			DataTable table = new DataTable("Result");
			table.Columns.Add("CodigoNorma", Type.GetType("System.String"));
			table.Columns.Add("Local", Type.GetType("System.String"));
			table.Columns.Add("Equipamento", Type.GetType("System.String"));
			table.Columns.Add("NomeEmpresa", Type.GetType("System.String"));
			table.Columns.Add("DataAuditoria", Type.GetType("System.String"));
			table.Columns.Add("TipoListagem", Type.GetType("System.String"));
			ds.Tables.Add(table);
			DataRow newRow;	
			
			ArrayList listIrregularidades = new ArrayList();

			switch (tipoListagem)
			{
				case "Todas":
                    listIrregularidades = new Irregularidade().FindIrregularidades(this.auditoria.Id.ToString());
					break;
				case "Regularizadas":
                    listIrregularidades = new Irregularidade().FindIrregularidades(this.auditoria.Id.ToString(), string.Empty, (int)IndIrregularidade.Regularizadas);
					break;
				case "Pendentes":
                    listIrregularidades = new Irregularidade().FindIrregularidades(this.auditoria.Id.ToString(), string.Empty, (int)IndIrregularidade.NaoRegularizadas);
					break;
			}

			foreach(Irregularidade irregularidade in listIrregularidades)
			{
				irregularidade.IdNorma.Find();

				newRow = ds.Tables[0].NewRow();							
				newRow["CodigoNorma"]	= irregularidade.IdNorma.CodigoItem;
				newRow["Local"]			= irregularidade.strLocalIrregularidade();
				newRow["Equipamento"]	= irregularidade.strAcoesExecutar();
				newRow["NomeEmpresa"]	= this.cliente.NomeAbreviado;
				newRow["DataAuditoria"] = "Data da Auditoria : " + this.auditoria.DataLevantamento.ToString("dd-MM-yyyy");
				newRow["TipoListagem"]	= tipoListagem;
				ds.Tables[0].Rows.Add(newRow);
			}
			return ds;
		}		

		private DataSet GetIrregularidadesDadosComletos()
		{
			DataSet ds = new DataSet();
			DataTable table = new DataTable("Result");		
			table.Columns.Add("NomeEmpresa", Type.GetType("System.String"));
			table.Columns.Add("DataAuditoria", Type.GetType("System.String"));
			table.Columns.Add("CodigoNorma", Type.GetType("System.String"));	
			table.Columns.Add("Local", Type.GetType("System.String"));
			table.Columns.Add("Equipamento", Type.GetType("System.String"));
			table.Columns.Add("DataInicio", Type.GetType("System.String"));
			table.Columns.Add("DataPrevisao", Type.GetType("System.String"));
			table.Columns.Add("DataFinal", Type.GetType("System.String"));
			table.Columns.Add("Orcamento", Type.GetType("System.String"));
			table.Columns.Add("CustoFinal", Type.GetType("System.String"));
			table.Columns.Add("RespAdm", Type.GetType("System.String"));
			table.Columns.Add("RespOper", Type.GetType("System.String"));
			table.Columns.Add("Objetivo", Type.GetType("System.String"));
			table.Columns.Add("Observacao", Type.GetType("System.String"));
			table.Columns.Add("TipoListagem", Type.GetType("System.String"));
			ds.Tables.Add(table);
			DataRow newRow;

			ArrayList listIrregularidades = new ArrayList();

			switch (tipoListagem)
			{
				case "Todas":
                    listIrregularidades = new Irregularidade().FindIrregularidades(this.auditoria.Id.ToString());
					break;
				case "Regularizadas":
                    listIrregularidades = new Irregularidade().FindIrregularidades(this.auditoria.Id.ToString(), string.Empty, (int)IndIrregularidade.Regularizadas);
					break;
				case "Pendentes":
                    listIrregularidades = new Irregularidade().FindIrregularidades(this.auditoria.Id.ToString(), string.Empty, (int)IndIrregularidade.NaoRegularizadas);
					break;
			}

			foreach(Irregularidade irregularidade in listIrregularidades)
			{
				irregularidade.IdNorma.Find();
				irregularidade.IdRespAdm.Find();
				irregularidade.IdRespOpe.Find();
				
				newRow = ds.Tables[0].NewRow();					
				newRow["NomeEmpresa"] = this.cliente.NomeAbreviado;
				newRow["DataAuditoria"] = "Data da Auditoria : " + this.auditoria.DataLevantamento.ToString("dd-MM-yyyy");
				newRow["CodigoNorma"] = irregularidade.IdNorma.CodigoItem;
				newRow["Local"] = irregularidade.strLocalIrregularidade();
				newRow["Equipamento"] = irregularidade.strAcoesExecutar();		
				if (irregularidade.DataInicioRegul != new DateTime() && irregularidade.DataInicioRegul != new DateTime(1753, 1, 1))
                    newRow["DataInicio"] = irregularidade.DataInicioRegul.ToString("dd-MM-yyyy");
				if (irregularidade.DataPrevisaoRegul != new DateTime() && irregularidade.DataPrevisaoRegul != new DateTime(1753, 1, 1))
					newRow["DataPrevisao"] = irregularidade.DataPrevisaoRegul.ToString("dd-MM-yyyy");
				if (irregularidade.DataFinalRegul != new DateTime() && irregularidade.DataFinalRegul != new DateTime(1753, 1, 1))
					newRow["DataFinal"] = irregularidade.DataFinalRegul.ToString("dd-MM-yyyy");
				if (irregularidade.Orcamento != 0)
                    newRow["Orcamento"] = irregularidade.Orcamento;
				if (irregularidade.CustoFinal != 0)
					newRow["CustoFinal"] = irregularidade.CustoFinal;
				newRow["RespAdm"] = irregularidade.IdRespAdm.NomeCompleto;
				newRow["RespOper"] = irregularidade.IdRespOpe.NomeCompleto;
				newRow["Objetivo"] = irregularidade.ObjetivoRegul;
				newRow["Observacao"] = irregularidade.ObservacaoRegul;
				newRow["TipoListagem"] = tipoListagem;
				ds.Tables[0].Rows.Add(newRow);
			}
			return ds;
		}				
	}
}
