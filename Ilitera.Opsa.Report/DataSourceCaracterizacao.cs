using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Drawing;
using Ilitera.Opsa.Data;
using Ilitera.Common;

namespace Ilitera.Opsa.Report
{	
	public class DataSourceCaracterizacao
	{
		public DataSourceCaracterizacao()
		{


		}

		public RptCaracterizacao GetReport()
		{
			RptCaracterizacao report = new RptCaracterizacao();	
			
			report.Load();
			
			report.Refresh();
			
			report.SetDatabaseLogon(Ilitera.Data.SQLServer.EntitySQLServer.User,
									Ilitera.Data.SQLServer.EntitySQLServer.Password);

			return report;
		}

		public RptCaracterizacaoCliente GetReportDocumento()
		{
			RptCaracterizacaoCliente report = new RptCaracterizacaoCliente();	
			
			report.Load();
			
			report.Refresh();

			report.SetDatabaseLogon(Ilitera.Data.SQLServer.EntitySQLServer.User,
									Ilitera.Data.SQLServer.EntitySQLServer.Password);

			return report;
		}
	}
}
