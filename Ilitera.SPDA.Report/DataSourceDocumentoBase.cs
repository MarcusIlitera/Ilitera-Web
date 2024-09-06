using System;
using System.Data;
using System.Collections;
using Ilitera.Common;
using Ilitera.Opsa.Data;

namespace Ilitera.SPDA.Report
{
	public class DataSourceDocumentoBase : DataSourceBase
	{
		private Cliente cliente;
        private Int32 IdLaudoTecnico;
		

		public DataSourceDocumentoBase(Cliente cliente, Int32 idLaudoTecnico)
		{		
			this.cliente = cliente;
            this.IdLaudoTecnico = idLaudoTecnico;

			if(this.cliente.mirrorOld==null)
				this.cliente.Find();

		}


		public RptDocumentoBase GetReport()
		{
			RptDocumentoBase report = new RptDocumentoBase();
			report.SetDataSource(GetDataSource());
			report.Refresh();

            SetTempoProcessamento(report);

			return report;
		}

        //public RptCronogramaPGR GetReportCronogramaPGR()
        //{
        //    RptCronogramaPGR report = new RptCronogramaPGR();
        //    report.SetDataSource(GetDataSource());
        //    report.Refresh();
        //    return report;
        //}

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
			
			DataSet ds = new DataSet();
			ds.Tables.Add(table);

			DataRow newRow;


            ArrayList list = CronogramaLaudoEletrico.GetCronograma(this.IdLaudoTecnico);
			//ArrayList list = CronogramaPPRA.GetCronograma(laudoTecnico);
			
			foreach(CronogramaLaudoEletrico cronogramaLaudo in list)
			{
				if(cronogramaLaudo.PlanejamentoAnual.Equals(string.Empty))
					continue;
				
				newRow = ds.Tables["Result"].NewRow();
                 
                newRow["CarimboCNPJ"] = cliente.GetCarimboCnpjHtml( System.Convert.ToDateTime( "01/01/2014"));
               

				newRow["PlanejamentoAnual"] = cronogramaLaudo.PlanejamentoAnual;
                newRow["Jan"] = MesCronograma(cronogramaLaudo.Mes01);
                newRow["Fev"] = MesCronograma(cronogramaLaudo.Mes02);
                newRow["Mar"] = MesCronograma(cronogramaLaudo.Mes03);
                newRow["Abr"] = MesCronograma(cronogramaLaudo.Mes04);
                newRow["Mai"] = MesCronograma(cronogramaLaudo.Mes05);
                newRow["Jun"] = MesCronograma(cronogramaLaudo.Mes06);
                newRow["Jul"] = MesCronograma(cronogramaLaudo.Mes07);
                newRow["Ago"] = MesCronograma(cronogramaLaudo.Mes08);
                newRow["Set"] = MesCronograma(cronogramaLaudo.Mes09);
                newRow["Out"] = MesCronograma(cronogramaLaudo.Mes10);
                newRow["Nov"] = MesCronograma(cronogramaLaudo.Mes11);
                newRow["Dez"] = MesCronograma(cronogramaLaudo.Mes12);
                newRow["Ano"] = cronogramaLaudo.Ano;
                newRow["EstrategiaMetAcao"] = cronogramaLaudo.MetodologiaAcao;
                newRow["FormaRegistro"] = cronogramaLaudo.FormaRegistro;

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
