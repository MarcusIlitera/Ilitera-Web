using System;
using System.Collections;
using System.Data;
using System.Drawing;
using Ilitera.Data;
using Ilitera.Opsa.Data;
using Ilitera.Common;
using Ilitera.VasoCaldeira.Report;

namespace Ilitera.VasoCaldeira.Report
{
    public class DataSourceLaudoEletrico : DataSourceBase
	{
		private int xIdLaudoEletrico;

		public DataSourceLaudoEletrico(int LaudoEletrico)
		{
			this.xIdLaudoEletrico= LaudoEletrico;
			
			if(this.xIdLaudoEletrico==0)
				throw new Exception("Laudo Elétrico deve estar salvo.");
		}

		public RptLaudoEletrico GetReport()
		{
			RptLaudoEletrico report = new RptLaudoEletrico();	
			report.SetDataSource(GetLaudoEletrico());
            //report.OpenSubreport("CoordenadorPcmso2").SetDataSource(pcmso.GetDSCoordenador());
			report.Refresh();
            SetTempoProcessamento(report);
			return report;
		}

        private DataSet GetLaudoEletrico()
        {
            DataTable table = new DataTable("Result");
            table.Columns.Add("Titulo", Type.GetType("System.String"));
            table.Columns.Add("Texto", Type.GetType("System.String"));

            DataSet ds = new DataSet();
            ds.Tables.Add(table);
            //DataRow newRow;

            ////pega NGA nesta linha
            //ArrayList list = new LaudoEletricoDocumento().Find("IdLaudoEletrico=" +  xIdLaudoEletrico + " ORDER BY NumOrdem");


            //foreach (LaudoEletricoDocumento laudoEletrico in list)
            //{
            //    newRow = ds.Tables[0].NewRow();

            //    newRow["Titulo"] = laudoEletrico.NumOrdem.ToString() + " - " + laudoEletrico.Titulo;
            //    newRow["Texto"] = laudoEletrico.Texto + "\n\r\t";
               
            //    ds.Tables[0].Rows.Add(newRow);
            //}


            return ds;
        }		
	}
}
