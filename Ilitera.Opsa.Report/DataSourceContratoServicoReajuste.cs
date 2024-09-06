using System;
using System.Data;
using System.Collections;
using Ilitera.Opsa.Data;

namespace Ilitera.Opsa.Report
{	
	public class DataSourceContratoServicoReajuste
	{
		public DataSourceContratoServicoReajuste()
		{			

		}

        public RptContratoServicoReajuste GetReport()
        {
            return this.GetReport(string.Empty);
        }

        public RptContratoServicoReajuste GetReport(string sRecordSelectionFormula)
		{
            RptContratoServicoReajuste report = new RptContratoServicoReajuste();

            report.Load();
            report.Refresh();

            report.SetDatabaseLogon(Ilitera.Data.SQLServer.EntitySQLServer.User,
                                    Ilitera.Data.SQLServer.EntitySQLServer.Password);

            if (sRecordSelectionFormula != string.Empty)
                report.RecordSelectionFormula = sRecordSelectionFormula;
                
			return report;
		}
	}
}
