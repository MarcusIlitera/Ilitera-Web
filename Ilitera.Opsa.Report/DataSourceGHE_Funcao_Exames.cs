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
    public class DataSourceGHE_Funcao_Exames: DataSourceBase
    {

        public DataSourceGHE_Funcao_Exames()
        {

        }

        public rptGHE_Funcao_Exames GetReport(int xId, string xD_Inicial, string xCliente, string zEmp, string xFiltro)
        {
            rptGHE_Funcao_Exames report = new rptGHE_Funcao_Exames();
            report.SetDataSource(GetClientesColaboradores(xId, xD_Inicial, xCliente, zEmp, xFiltro));
            report.Refresh();
            //SetTempoProcessamento(report);
            return report;
            
        }

             


        public DataSet GetClientesColaboradores(int zId, string zD_Inicial, string zCliente, string xEmp, string zFiltro)
        {

            DataSet ds;

            Clientes_Funcionarios xClientes_Funcionarios = new Clientes_Funcionarios();

            ds = xClientes_Funcionarios.Gerar_DS_Relatorio_GHE_Funcao_Exames(zId, zD_Inicial, zCliente, xEmp, zFiltro);

            return ds;

        }

            
        
    }
}