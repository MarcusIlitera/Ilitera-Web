using System;
using System.Data;
using System.Text;
using System.Collections;
using Ilitera.Opsa.Data;
using Ilitera.Common;
using Ilitera.Opsa.Report;
using Ilitera.Data.SQLServer;
using Ilitera.Data;

namespace Ilitera.Opsa.Report
{
    public class DataSourcePCD: DataSourceBase
    {

        public DataSourcePCD()
        {

        }

        public rptPCD GetReport(int xId, string xD_Inicial, string xCliente, string zEmp, string xFiltro, string xD_Final, string xSit)
        {
            rptPCD report = new rptPCD();
            report.SetDataSource(GetPCD(xId, xD_Inicial, xCliente, zEmp, xFiltro, xD_Final, xSit));
            report.Refresh();
            //SetTempoProcessamento(report);
            return report;
            
        }


        public rptPCD_Sumarizado GetReport_Sum(int xId, string xD_Inicial, string xCliente, string zEmp, string xFiltro, string xD_Final, string xSit)
        {
            rptPCD_Sumarizado report = new rptPCD_Sumarizado();
            report.SetDataSource(GetPCD(xId, xD_Inicial, xCliente, zEmp, xFiltro, xD_Final, xSit));
            report.Refresh();
            //SetTempoProcessamento(report);
            return report;

        }


        public DataSet GetPCD(int zId, string zD_Inicial, string zCliente, string xEmp, string zFiltro, string zD_Final, string zSit )
        {

            DataSet ds;

            Clientes_Funcionarios xClientes_Funcionarios = new Clientes_Funcionarios();

            ds = xClientes_Funcionarios.Gerar_DS_Relatorio_PCD(zId, zD_Inicial, zCliente, xEmp, zFiltro, zD_Final, zSit);

            return ds;

        }

            
        
    }
}