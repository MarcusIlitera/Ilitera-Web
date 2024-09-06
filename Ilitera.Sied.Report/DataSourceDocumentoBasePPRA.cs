using System;
using System.Data;
using System.Collections;
using Ilitera.Common;
using Ilitera.Opsa.Data;

namespace Ilitera.Sied.Report
{
	public class DataSourceDocumentoBasePPRA : DataSourceBase
	{
		private Cliente cliente;
		private LaudoTecnico laudoTecnico;

		public DataSourceDocumentoBasePPRA(Cliente cliente)
		{		
			this.cliente = cliente;

			if(this.cliente.mirrorOld==null)
				this.cliente.Find();

			this.laudoTecnico = LaudoTecnico.GetUltimoLaudo(this.cliente.Id);
		}

		public DataSourceDocumentoBasePPRA(LaudoTecnico laudoTecnico)
		{
			this.laudoTecnico = laudoTecnico;

			this.cliente = laudoTecnico.nID_EMPR;

			if(this.cliente.mirrorOld==null)
				this.cliente.Find();
		}

        public DataSourceDocumentoBasePPRA(Cliente cliente, LaudoTecnico laudoTecnico)
        {
            this.laudoTecnico = laudoTecnico;

            this.cliente = cliente;

            if (this.cliente.mirrorOld == null)
                this.cliente.Find();
        }

		public DataSourceDocumentoBasePPRA(int IdLaudoTecnico)
		{
			this.laudoTecnico = new LaudoTecnico();
			this.laudoTecnico.Find(IdLaudoTecnico);
			
			this.cliente = laudoTecnico.nID_EMPR;

			if(this.cliente.mirrorOld==null)
				this.cliente.Find();
		}	

		public RptDocumentoBasePPRA GetReport()
		{
			RptDocumentoBasePPRA report = new RptDocumentoBasePPRA();
			report.SetDataSource(GetDataSource());
			report.Refresh();

            SetTempoProcessamento(report);

			return report;
		}


        public RptDocumentoBasePGR2 GetReportPGR()
        {
            RptDocumentoBasePGR2 report = new RptDocumentoBasePGR2();
            report.SetDataSource(GetDataSource());
            report.Refresh();

            SetTempoProcessamento(report);

            return report;
        }

        public RptCronogramaPGR GetReportCronogramaPGR()
        {
            RptCronogramaPGR report = new RptCronogramaPGR();
            report.SetDataSource(GetDataSource());
            report.Refresh();
            return report;
        }

		private DataSet GetDataSource()
		{			
			DataTable table = new DataTable("Result");		
			table.Columns.Add("CarimboCNPJ", Type.GetType("System.String"));			
			table.Columns.Add("PlanejamentoAnual", Type.GetType("System.String"));			
			table.Columns.Add("Jan", Type.GetType("System.String"));
			table.Columns.Add("Fev", Type.GetType("System.String"));
			table.Columns.Add("Mar", Type.GetType("System.String"));
			table.Columns.Add("Abr", Type.GetType("System.String"));
			table.Columns.Add("Mai", Type.GetType("System.String"));
			table.Columns.Add("Jun", Type.GetType("System.String"));
			table.Columns.Add("Jul", Type.GetType("System.String"));
			table.Columns.Add("Ago", Type.GetType("System.String"));
			table.Columns.Add("Set", Type.GetType("System.String"));
			table.Columns.Add("Out", Type.GetType("System.String"));
			table.Columns.Add("Nov", Type.GetType("System.String"));
			table.Columns.Add("Dez", Type.GetType("System.String"));
			table.Columns.Add("Ano", Type.GetType("System.String"));
			table.Columns.Add("EstrategiaMetAcao", Type.GetType("System.String"));
			table.Columns.Add("FormaRegistro", Type.GetType("System.String"));
            table.Columns.Add("Prioridade", Type.GetType("System.Int16"));

            DataSet ds = new DataSet();
			ds.Tables.Add(table);

			DataRow newRow;

			ArrayList list = CronogramaPPRA.GetCronograma(laudoTecnico);
			
			foreach(CronogramaPPRA cronogramaPPRA in list)
			{
				if(cronogramaPPRA.PlanejamentoAnual.Equals(string.Empty))
					continue;
				
				newRow = ds.Tables["Result"].NewRow();

                newRow["CarimboCNPJ"] = cliente.GetCarimboCnpjHtml(laudoTecnico.hDT_LAUDO);
				newRow["PlanejamentoAnual"] = cronogramaPPRA.PlanejamentoAnual;
				newRow["Jan"] = MesCronograma(cronogramaPPRA.Mes01);
				newRow["Fev"] = MesCronograma(cronogramaPPRA.Mes02);
				newRow["Mar"] = MesCronograma(cronogramaPPRA.Mes03);
				newRow["Abr"] = MesCronograma(cronogramaPPRA.Mes04);
				newRow["Mai"] = MesCronograma(cronogramaPPRA.Mes05);
				newRow["Jun"] = MesCronograma(cronogramaPPRA.Mes06);
				newRow["Jul"] = MesCronograma(cronogramaPPRA.Mes07);
				newRow["Ago"] = MesCronograma(cronogramaPPRA.Mes08);
				newRow["Set"] = MesCronograma(cronogramaPPRA.Mes09);
				newRow["Out"] = MesCronograma(cronogramaPPRA.Mes10);
				newRow["Nov"] = MesCronograma(cronogramaPPRA.Mes11);
				newRow["Dez"] = MesCronograma(cronogramaPPRA.Mes12);
				newRow["Ano"] = cronogramaPPRA.Ano;
				newRow["EstrategiaMetAcao"] = cronogramaPPRA.MetodologiaAcao;
				newRow["FormaRegistro"] = cronogramaPPRA.FormaRegistro;
                newRow["Prioridade"] = cronogramaPPRA.Prioridade;

                ds.Tables["Result"].Rows.Add(newRow);
			}
								
			return ds;
		}

        private string MesCronograma(bool bVal)
        {
            string mes = "";

            if (bVal)
            {
                mes = "•"; //"•"; 
            }

            return mes;
        }
	}
}
