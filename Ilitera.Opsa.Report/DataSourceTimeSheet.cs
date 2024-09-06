using System;
using System.Collections.Generic;
using System.Text;

namespace Ilitera.Opsa.Report
{
    public class DataSourceTimeSheet
    {
        public DataSourceTimeSheet()
        {
            
        }

        public RptTimeSheet GetReport()
        {
            DateTime dataInicio = Ilitera.Common.Utility.PrimeiroDiaMes(DateTime.Today.AddMonths(-3));

            RptTimeSheet report = new RptTimeSheet();

            report.Load();

            report.Refresh();

            Ilitera.Common.DataSourceBase.VerificarProvider(report);

            report.SetDatabaseLogon(Ilitera.Data.SQLServer.EntitySQLServer.User,
                                    Ilitera.Data.SQLServer.EntitySQLServer.Password);

            report.SummaryInfo.ReportTitle = "Total de Horas Apontadas no TimeSheet";

            report.RecordSelectionFormula = GetSelecionFormula(dataInicio);

            report.Subreports["NaoPreenchem"].SetDatabaseLogon(Ilitera.Data.SQLServer.EntitySQLServer.User,
                                                        Ilitera.Data.SQLServer.EntitySQLServer.Password);

            report.SetParameterValue("@DataIncio", dataInicio, "NaoPreenchem");

            return report;
        }

        private string GetSelecionFormula(DateTime dataInicio)
        {

            StringBuilder sb = new StringBuilder();
            sb.Append("{qryTimeSheet.DataInicio} >=" 
                + " Date (" + dataInicio.Year + ", " + dataInicio.Month + ", " + dataInicio.Day + ")");

            //if(IsAprovados)
            //    sb.Append(" AND NOT ISNULL({qryTimeSheet.DataAprovacao})");

            return sb.ToString();
        }
    }
}
