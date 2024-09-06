using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Drawing;

using Ilitera.Common;

namespace Ilitera.PCMSO.Report
{	
	public class DataSourceClientePCMSO: DataSourceBase
	{
		public DataSourceClientePCMSO()
		{

		}

        public RptClientePcmso GetReport()
		{
            RptClientePcmso report = new RptClientePcmso();	
			report.Load();
			report.Refresh();
			
			report.SetDatabaseLogon(Ilitera.Data.SQLServer.EntitySQLServer.User,
									Ilitera.Data.SQLServer.EntitySQLServer.Password);
            VerificarProvider(report);

			return report;
		}


	}
}
