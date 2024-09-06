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
    public class DataSourceColaboradores: DataSourceBase
    {

        public DataSourceColaboradores()
        {

        }

        public rptColaboradores GetReport(int xId, string xD_Inicial, string xCliente, string zEmp, string xFiltro, string xSit, string xTipo, string xDefic, string xAtivosEm, string xSemEmail, string xInconsistencia, string xClassif )
        {
            rptColaboradores report = new rptColaboradores();
            report.SetDataSource(GetClientesColaboradores(xId, xD_Inicial, xCliente, zEmp, xFiltro, xSit, xTipo, xDefic, xAtivosEm, xSemEmail, xInconsistencia, xClassif));
            report.Refresh();
            //SetTempoProcessamento(report);
            return report;
            
        }


        public rptColaboradoresAptidoes GetReport_Aptidao(int xId)
        {
            rptColaboradoresAptidoes report = new rptColaboradoresAptidoes();
            report.SetDataSource(GetClientesColaboradoresAptidoes(xId));
            report.Refresh();
            //SetTempoProcessamento(report);
            return report;

        }


        public DataSet GetClientesColaboradores(int zId, string zD_Inicial, string zCliente, string xEmp, string zFiltro, string zSit, string zTipo, string xDefic, string xAtivosEm, string xSemEmail, string xInconsistencia, string xClassif)
        {

            DataSet ds;

            Ilitera.Data.Clientes_Funcionarios xClientes_Funcionarios = new Ilitera.Data.Clientes_Funcionarios();

            ds = xClientes_Funcionarios.Gerar_DS_Relatorio_Colaboradores(zId, zD_Inicial, zCliente, xEmp, zFiltro, zSit, zTipo, xDefic, xAtivosEm, xSemEmail, xInconsistencia, xClassif );

            return ds;

        }

        public DataSet GetClientesColaboradores2(int zId, string zD_Inicial, string zCliente, string xEmp, string zFiltro, string zSit, string zTipo, string xDefic, string xAtivosEm, string xSemEmail, string xInconsistencia, string xClassif)
        {

            DataSet ds;

            Clientes_Funcionarios xClientes_Funcionarios = new Clientes_Funcionarios();

            ds = xClientes_Funcionarios.Gerar_DS_Relatorio_Colaboradores2(zId, zD_Inicial, zCliente, xEmp, zFiltro, zSit, zTipo, xDefic, xAtivosEm, xSemEmail, xInconsistencia, xClassif);

            return ds;

        }


        public DataSet GetClientesColaboradoresAptidoes(int zId)
        {

            DataSet ds;

            Clientes_Funcionarios xClientes_Funcionarios = new Clientes_Funcionarios();

            ds = xClientes_Funcionarios.Gerar_DS_Relatorio_Colaboradores_Aptidoes(zId);

            return ds;

        }



    }
}