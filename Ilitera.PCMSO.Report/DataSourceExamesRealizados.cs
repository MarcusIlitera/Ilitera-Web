using System;
using System.Data;
using System.Collections;

using Ilitera.Common;
using Ilitera.Opsa.Data;

namespace Ilitera.PCMSO.Report
{
    public class DataSourceExamesRealizados : DataSourceBase
	{
		public DataSourceExamesRealizados()
		{			

		}

        public RptExamesRealizados GetReport(string sReportTitle, string sRecordSelectionFormula)
		{
            RptExamesRealizados report = new RptExamesRealizados();

            report.Load();

            report.Refresh();

            report.SetDatabaseLogon(Ilitera.Data.SQLServer.EntitySQLServer.User,
                                    Ilitera.Data.SQLServer.EntitySQLServer.Password,
                                    Ilitera.Data.SQLServer.EntitySQLServer.Server,
                                    Ilitera.Data.SQLServer.EntitySQLServer.Database);

            report.SummaryInfo.ReportTitle = sReportTitle;

            report.RecordSelectionFormula = sRecordSelectionFormula;

            SetTempoProcessamento(report);

			return report;
		}	
	}
}
