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
    public class DataSourceClientesExames : DataSourceBase
    {

        public  DataSourceClientesExames()
        {

        }

        public rptClientesExames GetReport(int xId, string xD_Inicial, string xD_Final, string xCliente, string xExames, string xResult, int xEmp)
        {
            rptClientesExames report = new rptClientesExames();
            report.SetDataSource(GetClientesExames(xId, xD_Inicial, xD_Final, xCliente, xExames, xResult, xEmp));
            report.Refresh();
            //SetTempoProcessamento(report);
            return report;
            
        }



        public rptClientesExames_Sumarizado GetReport_Sumarizado(int xId, string xD_Inicial, string xD_Final, int xEmp, string xExames, string xResultado)
        {
            rptClientesExames_Sumarizado report = new rptClientesExames_Sumarizado();
            report.SetDataSource(GetClientesExames_Sumarizado(xId, xD_Inicial, xD_Final, xEmp, xExames, xResultado));
            report.Refresh();
            //SetTempoProcessamento(report);
            return report;

        }


        public rptMailing GetReport_Mailing(int xId, string xD_Inicial, string xD_Final, Int16 zEmp , Int16 zAttach, Int16 zSemAgendamento, Int16 zSemEnvio, Int16 zSemResultado)
        {
            rptMailing report = new rptMailing();
            report.SetDataSource(GetMailing(xId, xD_Inicial, xD_Final, zEmp, zAttach, zSemAgendamento, zSemEnvio, zSemResultado) );
            report.Refresh();
            //SetTempoProcessamento(report);
            return report;

        }




        public rptClientesExames_Sumarizado_2 GetReport_Sumarizado2(int xId, string xD_Inicial, string xD_Final, int xEmp, string xExames, string xResultado)
        {
            rptClientesExames_Sumarizado_2 report = new rptClientesExames_Sumarizado_2();

            report.Load();
            report.SetDataSource(GetExames(xId, xD_Inicial, xD_Final, xEmp, xExames, xResultado));
            report.Refresh();

         

            return report;
        }


        public DataSet GetExames(int zId, string zD_Inicial, string zD_Final, int xEmp, string xExames, string xResultado)
        {

            DataSet ds;

            Clientes_Funcionarios xClientes_Funcionarios = new Clientes_Funcionarios();

            ds = xClientes_Funcionarios.Gerar_DS_Relatorio_Exames_Empr_Sumarizado2(zId, zD_Inicial, zD_Final, xEmp, xExames, xResultado);
            return ds;

        }




        public DataSet GetClientesExames(int zId, string zD_Inicial, string zD_Final, string zCliente, string xExames, string xResult, int xEmp)
        {

            DataSet ds;

            Clientes_Funcionarios xClientes_Funcionarios = new Clientes_Funcionarios();

            ds = xClientes_Funcionarios.Gerar_DS_Relatorio_Exames_Empr(zId, zD_Inicial, zD_Final, zCliente, xExames, xResult, xEmp);

            return ds;

        }


        public DataSet GetClientesExames_Sumarizado(int zId, string zD_Inicial, string zD_Final, int xEmp, string xExames, string xResultado)
        {

            DataSet ds;

            Clientes_Funcionarios xClientes_Funcionarios = new Clientes_Funcionarios();

            ds = xClientes_Funcionarios.Gerar_DS_Relatorio_Exames_Empr_Sumarizado(zId, zD_Inicial, zD_Final, xEmp, xExames, xResultado);

            return ds;

        }


        public DataSet GetMailing(int zId, string zD_Inicial, string zD_Final,  Int16 zEmp, Int16 zAttach, Int16 zSemAgendamento, Int16 zSemEnvio, Int16 zSemResultado)
        {

            DataSet ds;

            Clientes_Funcionarios xClientes_Funcionarios = new Clientes_Funcionarios();

            ds = xClientes_Funcionarios.Gerar_DS_Relatorio_Mailing(zId, zD_Inicial, zD_Final, zEmp, zAttach, zSemAgendamento, zSemEnvio, zSemResultado);

            return ds;

        }

    }
}