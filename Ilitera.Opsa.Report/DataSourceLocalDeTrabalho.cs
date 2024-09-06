using System;
using System.Data;
using System.Data.SqlClient;
using Ilitera.Opsa.Data;
using Ilitera.Common;
using System.Collections;
using System.Drawing;

namespace Ilitera.Opsa.Report
{	
	public class DataSourceLocalDeTrabalho
	{
		private GrupoEmpresa grupoEmpresa;
		private Cliente cliente;

		public DataSourceLocalDeTrabalho(Cliente cliente): this (cliente.IdGrupoEmpresa)
		{
			this.cliente = cliente;
		}

		public DataSourceLocalDeTrabalho(GrupoEmpresa grupoEmpresa)
		{
			this.grupoEmpresa = grupoEmpresa;
		}

		public RptLocalDeTrabalho GetReport()
		{
			RptLocalDeTrabalho report = new RptLocalDeTrabalho();	
			
			report.Load();
			
			report.Refresh();
			
			report.SetDatabaseLogon(Ilitera.Data.SQLServer.EntitySQLServer.User,
									Ilitera.Data.SQLServer.EntitySQLServer.Password,
									Ilitera.Data.SQLServer.EntitySQLServer.Server,
									Ilitera.Data.SQLServer.EntitySQLServer.Database);

            if (cliente == null)
            {
                report.SetParameterValue("@Cliente", 0);

                report.SummaryInfo.ReportTitle = grupoEmpresa.Descricao;
            }
            else
            {
                report.SetParameterValue("@Cliente", cliente.Id);

                report.SummaryInfo.ReportTitle = cliente.NomeAbreviado;
            }

			report.SetParameterValue("@GrupoEmpresa", grupoEmpresa.Id);

			return report;
		}
	}
}
