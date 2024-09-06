using System;
using System.Data;
using System.Data.SqlClient;
using Ilitera.Opsa.Data;
using Ilitera.Common;
using System.Collections;
using System.Drawing;

namespace Ilitera.PCMSO.Report
{
    public class DataSourceNexoEdpidemiologico : DataSourceBase
	{
		private CNAE cnae;
        private bool IsSubCategoria;
        private Juridica juridica;

        public DataSourceNexoEdpidemiologico(Juridica juridica, CNAE cnae, bool IsSubCategoria)
		{
            this.juridica = juridica;

            this.cnae = cnae;

            this.IsSubCategoria = IsSubCategoria;
		}

        public RptNexoEpidemiologico GetReport()
		{
            RptNexoEpidemiologico report = new RptNexoEpidemiologico();	

			report.Load();
			
			report.Refresh();

            report.Subreports[0].SetDataSource(GetCarimboCNPJ());
			
			report.SetDatabaseLogon(Ilitera.Data.SQLServer.EntitySQLServer.User,
									Ilitera.Data.SQLServer.EntitySQLServer.Password);

			report.SetParameterValue("@IdCNAE", cnae.Id);
            report.SetParameterValue("@IsSubCategoria", IsSubCategoria);

            SetTempoProcessamento(report);
			
			return report;
		}

        private DataTable GetCarimboCNPJ()
        {
            DataTable table = new DataTable("CarimboCNPJ");

            table.Columns.Add("IdCliente", Type.GetType("System.Int32"));
            table.Columns.Add("NomeCliente", Type.GetType("System.String"));
            table.Columns.Add("LogradouroNumero", Type.GetType("System.String"));
            table.Columns.Add("BairroCEPCidadeEstado", Type.GetType("System.String"));
            table.Columns.Add("CNPJ", Type.GetType("System.String"));
            table.Columns.Add("Data", Type.GetType("System.String"));

            DataRow newRow;

            newRow = table.NewRow();
            newRow["NomeCliente"] = juridica.GetCarimboCnpjHtml(DateTime.Today);
            table.Rows.Add(newRow);

            return table;
        }
	}
}
