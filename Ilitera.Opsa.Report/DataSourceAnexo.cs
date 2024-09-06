using System;
using System.Data;
using System.Data.SqlClient;
using Ilitera.Opsa.Data;
using Ilitera.Common;
using System.Collections;
using System.Drawing;
using Ilitera.Data;

namespace Ilitera.Opsa.Report
{
    public class DataSourceAnexo : DataSourceBase
    {
        private string zArquivo;


        public DataSourceAnexo(string Arquivo)
        {
            this.zArquivo = Arquivo;

        }


        public RptAnexo GetReport()
        {
            RptAnexo report = new RptAnexo();

            report.Load();

            report.SetDataSource(GetDataSource());

            report.Refresh();

            //report.Subreports[0].SetDataSource(GetDataSource2());

            SetTempoProcessamento(report);

            return report;
        }




        private DataSet GetDataSource()
        {

            //string xLoc;
            //string xLoc2;

            DataSet zdS = new DataSet();
            DataTable table = new DataTable("Result");

            table.Columns.Add("Arquivo", Type.GetType("System.Byte[]"));

            DataRow newRow;


            zdS.Tables.Add(table);

            newRow = zdS.Tables[0].NewRow();

            newRow["Arquivo"] = Ilitera.Common.Fotos.GetByteFoto_Uri(zArquivo);

            zdS.Tables[0].Rows.Add(newRow);


            return zdS;


        }



   


    }

}
