using System;
using System.Data;
using System.Collections;
using Ilitera.Common;
using Ilitera.Opsa.Data;

namespace Ilitera.Sied.Report
{	
	public class DataSourceCronogramaErgonomia : DataSourceBase
	{
		private Cliente cliente;
		private LaudoTecnico laudoTecnico;
		
		public DataSourceCronogramaErgonomia(Cliente cliente)
		{			
			this.cliente = cliente;
            
            if (this.cliente.mirrorOld == null)
			    this.cliente.Find();

            this.laudoTecnico = LaudoTecnico.GetUltimoLaudo(cliente.Id);
		}
      
        public DataSourceCronogramaErgonomia(LaudoTecnico laudoTecnico)
        {
            this.laudoTecnico = laudoTecnico;
            this.cliente = laudoTecnico.nID_EMPR;

            if (this.cliente.mirrorOld == null)
                this.cliente.Find();
        }

        public DataSourceCronogramaErgonomia(int IdLaudoTecnico)
        {
            this.laudoTecnico = new LaudoTecnico();
            this.laudoTecnico.Find(IdLaudoTecnico);

            this.cliente = laudoTecnico.nID_EMPR;

            if (this.cliente.mirrorOld == null)
                this.cliente.Find();
        }	

		public RptCronogramaErgonomia GetReport()
		{
			RptCronogramaErgonomia report = new RptCronogramaErgonomia();
			report.SetDataSource(GetDataSource());
			report.Refresh();

            SetTempoProcessamento(report);

			return report;
		}

		private DataSet GetDataSource()
		{				
			DataSet ds = new DataSet();
			DataTable table = new DataTable("Result");	
			table.Columns.Add("CarimboCNPJ", Type.GetType("System.String"));	
			table.Columns.Add("Riscos", Type.GetType("System.String"));			
			table.Columns.Add("Grupos", Type.GetType("System.String"));		
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
			table.Columns.Add("Servicos", Type.GetType("System.String"));
			ds.Tables.Add(table);
			DataRow newRow;
			
			ArrayList listCronograma = new CronogramaErgonomia().Find("nID_LAUD_TEC="+ laudoTecnico.Id);
			
			foreach(CronogramaErgonomia cronogramaErgonomia in listCronograma)
			{
				newRow = ds.Tables[0].NewRow();
                newRow["CarimboCNPJ"] = cliente.GetCarimboCnpjHtml(laudoTecnico.hDT_LAUDO);
				newRow["Riscos"] = cronogramaErgonomia.AcaoLocalTrab;

                string xGrupos = cronogramaErgonomia.GetNomeGhe_New();

                if (xGrupos.Trim() == "")
                {
                    xGrupos = cronogramaErgonomia.GetNomeGhe();
                }

                newRow["Grupos"] = xGrupos;



                newRow["Jan"] = MesCronograma(cronogramaErgonomia.Mes01);
				newRow["Fev"] = MesCronograma(cronogramaErgonomia.Mes02);
				newRow["Mar"] = MesCronograma(cronogramaErgonomia.Mes03);
				newRow["Abr"] = MesCronograma(cronogramaErgonomia.Mes04);
				newRow["Mai"] = MesCronograma(cronogramaErgonomia.Mes05);
				newRow["Jun"] = MesCronograma(cronogramaErgonomia.Mes06);
				newRow["Jul"] = MesCronograma(cronogramaErgonomia.Mes07);
				newRow["Ago"] = MesCronograma(cronogramaErgonomia.Mes08);
				newRow["Set"] = MesCronograma(cronogramaErgonomia.Mes09);
				newRow["Out"] = MesCronograma(cronogramaErgonomia.Mes10);
				newRow["Nov"] = MesCronograma(cronogramaErgonomia.Mes11);
				newRow["Dez"] = MesCronograma(cronogramaErgonomia.Mes12);
				newRow["Ano"] = cronogramaErgonomia.Ano;
				newRow["Servicos"] = cronogramaErgonomia.Serv;
				ds.Tables[0].Rows.Add(newRow);
			}

            if (listCronograma.Count == 0)
            {
                newRow = ds.Tables[0].NewRow();
                newRow["CarimboCNPJ"] = cliente.GetCarimboCnpjHtml(laudoTecnico.hDT_LAUDO);
                ds.Tables[0].Rows.Add(newRow);
            }
			return ds;
		}

		private string MesCronograma(bool bVal)
		{
			string mes = "";

			if(bVal)
                mes = "•";
            //mes = "Ä";

			return mes;
		}
	}
}
