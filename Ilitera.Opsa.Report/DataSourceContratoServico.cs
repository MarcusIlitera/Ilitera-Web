using System;

namespace Ilitera.Opsa.Report
{
	public class DataSourceContratoServico
	{

		public DataSourceContratoServico()
		{

		}

        public RptContratoServico GetReport()
        {
            return this.GetReport(string.Empty);
        }

        public RptContratoServico GetReport(string sRecordSelectionFormula)
		{
            RptContratoServico report = new RptContratoServico();

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
