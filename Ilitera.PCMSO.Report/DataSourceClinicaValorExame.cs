using System;
using System.Collections.Generic;
using System.Text;

using Ilitera.Common;

namespace Ilitera.PCMSO.Report
{
    public class DataSourceClinicaValorExame: DataSourceBase
    {
        private GrupoEmpresa grupoEmpresa;

        public DataSourceClinicaValorExame(GrupoEmpresa grupoEmpresa)
        {
            this.grupoEmpresa = grupoEmpresa;
        }

        public RptClinicaValorExamePorGrupo GetReport()
        {
            RptClinicaValorExamePorGrupo report = new RptClinicaValorExamePorGrupo();

            report.Load();

            report.Refresh();

            VerificarProvider(report);

            report.SetDatabaseLogon(Ilitera.Data.SQLServer.EntitySQLServer.User,
                                    Ilitera.Data.SQLServer.EntitySQLServer.Password);

            report.SetParameterValue("@IdGrupoEmpresa", grupoEmpresa.Id);

            report.SummaryInfo.ReportTitle = "Grupo Empresa: " + grupoEmpresa.ToString();

            SetTempoProcessamento(report);

            return report;
        }
    }
}
