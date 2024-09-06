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
    public class DataSourceEmpregados : DataSourceBase
    {

        public DataSourceEmpregados()
        {

        }

        public rptEmpregados GetReport(int xId, string xD_Inicial, string xCliente)
        {
            rptEmpregados report = new rptEmpregados();
            report.SetDataSource(GetClientesEmpregados(xId, xD_Inicial, xCliente));
            report.Refresh();
            //SetTempoProcessamento(report);
            return report;
            
        }



        //public rptClientesExames_Sumarizado GetReport_Sumarizado(int xId, string xD_Inicial, string xD_Final, int xEmp)
        //{
        //    rptClientesExames_Sumarizado report = new rptClientesExames_Sumarizado();
        //    report.SetDataSource(GetClientesExames_Sumarizado(xId, xD_Inicial, xD_Final, xEmp));
        //    report.Refresh();
        //    //SetTempoProcessamento(report);
        //    return report;

        //}


        public DataSet GetClientesEmpregados(int zId, string zD_Inicial, string zCliente)
        {

            DataSet ds;

            Clientes_Funcionarios xClientes_Funcionarios = new Clientes_Funcionarios();

            ds = xClientes_Funcionarios.Gerar_DS_Relatorio_Empregados_Empr(zId, zD_Inicial, zCliente);

            return ds;

        }


        //public DataSet GetClientesExames_Sumarizado(int zId, string zD_Inicial, string zD_Final, int xEmp)
        //{

        //    DataSet ds;

        //    Clientes_Funcionarios xClientes_Funcionarios = new Clientes_Funcionarios();

        //    ds = xClientes_Funcionarios.Gerar_DS_Relatorio_Exames_Empr_Sumarizado(zId, zD_Inicial, zD_Final, xEmp);

        //    return ds;

        //}




    }
}