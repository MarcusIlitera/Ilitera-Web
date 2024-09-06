using System;
using System.Collections.Generic;
using System.Text;

namespace Ilitera.Opsa.Report
{
    public class DataSourceTimeSheetCliente
    {
        private bool IsSintetico;
        private bool IsAprovados;

        public DataSourceTimeSheetCliente(bool IsSintetico, bool IsAprovados)
        {
            this.IsSintetico = IsSintetico;
            this.IsAprovados = IsAprovados;
        }

        public RptTimeSheetCliente GetReport()
        {
            RptTimeSheetCliente report = new RptTimeSheetCliente();

            report.Load();

            report.Refresh();

            Ilitera.Common.DataSourceBase.VerificarProvider(report);

            report.SetDatabaseLogon(Ilitera.Data.SQLServer.EntitySQLServer.User,
                                    Ilitera.Data.SQLServer.EntitySQLServer.Password);

            report.SummaryInfo.ReportTitle = "Total de Horas Apontadas no TimeSheet";

            if (IsSintetico)
                report.SummaryInfo.ReportComments = "Sintético";

            report.RecordSelectionFormula = GetSelecionFormula();

            return report;
        }

        private string GetSelecionFormula()
        {
            DateTime dataInicio = Ilitera.Common.Utility.PrimeiroDiaMes(DateTime.Today.AddMonths(-5));

            StringBuilder sb = new StringBuilder();
            sb.Append("{qryTimeSheetClienteValorHora.DataInicio} >=" 
                + " Date (" + dataInicio.Year + ", " + dataInicio.Month + ", " + dataInicio.Day + ")");

            if(IsAprovados)
                sb.Append(" AND NOT ISNULL({qryTimeSheetClienteValorHora.DataAprovacao})");

            return sb.ToString();
        }
    }
}
