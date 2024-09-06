using System;

namespace Ilitera.Opsa.Report
{
    public class DataSourceContrato
    {
        public DataSourceContrato()
        {

        }

        public RptContrato GetReport(string sReportTitle)
        {
            return this.GetReport(sReportTitle, string.Empty);
        }

        public RptContrato GetReport(string sReportTitle, string sRecordSelectionFormula)
        {
            RptContrato report = new RptContrato();

            report.Load();

            report.Refresh();

            report.SetDatabaseLogon(Ilitera.Data.SQLServer.EntitySQLServer.User,
                                    Ilitera.Data.SQLServer.EntitySQLServer.Password);

            if (sRecordSelectionFormula != string.Empty)
                report.RecordSelectionFormula = sRecordSelectionFormula;

            report.SummaryInfo.ReportTitle = sReportTitle;

            return report;
        }

        public RptContratoListaCliente GetReportListaCliente()
        {
            return GetReportListaCliente("Contratos"," {qryContrato.IdJuridicaPapel}=" + (int)Ilitera.Common.IndJuridicaPapel.Cliente
                                                        + " AND not {qryContrato.IsInativo}"
                                                        + " AND ISNULL({qryContrato.DataRecisao})");
        }

        public RptContratoListaCliente GetReportListaCliente(string sReportTitle, string sRecordSelectionFormula)
        {
            RptContratoListaCliente report = new RptContratoListaCliente();

            report.Load();

            report.Refresh();

            report.SetDatabaseLogon(Ilitera.Data.SQLServer.EntitySQLServer.User,
                                    Ilitera.Data.SQLServer.EntitySQLServer.Password);

            if (sRecordSelectionFormula != string.Empty)
                report.RecordSelectionFormula = sRecordSelectionFormula;

            report.SummaryInfo.ReportTitle = sReportTitle;

            return report;
        }

        public RptContratoFluxo GetReportContratoFluxo()
        {
            return GetReportContratoFluxo(DateTime.Today.Year, false);
        }

        public RptContratoFluxo GetReportContratoFluxo(int Ano, bool Sintetico)
        {
            RptContratoFluxo report = new RptContratoFluxo();

            report.Load();

            report.Refresh();

            report.SetDatabaseLogon(Ilitera.Data.SQLServer.EntitySQLServer.User,
                                    Ilitera.Data.SQLServer.EntitySQLServer.Password);

            report.SetParameterValue("@Ano", Ano);

            report.ReportDefinition.Sections["Section3"].SectionFormat.EnableSuppress = Sintetico;

            return report;
        }
    }
}
