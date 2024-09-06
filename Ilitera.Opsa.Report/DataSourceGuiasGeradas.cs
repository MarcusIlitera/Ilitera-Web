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
    public class DataSourceGuiasGeradas : DataSourceBase
    {

        public DataSourceGuiasGeradas()
        {

        }

        public rptGuiasGeradas GetReport(int xId, string xD_Inicial, string xD_Final, string xCliente, string xConsiderar, Int16 xEmp)
        {
            rptGuiasGeradas report = new rptGuiasGeradas();
            report.SetDataSource(GetGuias(xId, xD_Inicial, xD_Final, xCliente, xConsiderar, xEmp));
            report.Refresh();
            //SetTempoProcessamento(report);
            return report;
            
        }



        public rptGuias_Sumarizado GetReport_Sum(int xId, string xD_Inicial, string xD_Final, string xCliente, string xConsiderar, Int16 xEmp)
        {
            rptGuias_Sumarizado report = new rptGuias_Sumarizado();
            report.SetDataSource(GetGuias_Sum(xId, xD_Inicial, xD_Final, xCliente, xConsiderar, xEmp));
            report.Refresh();
            //SetTempoProcessamento(report);
            return report;

        }



        public rptGuias_Bloqueio GetReport_Bloqueio(int xId, string xD_Inicial, string xD_Final, string xCliente, Int16 xEmp)
        {
            rptGuias_Bloqueio report = new rptGuias_Bloqueio();
            report.SetDataSource(GetGuiasBloqueio(xId, xD_Inicial, xD_Final, xCliente, xEmp, "A"));
            report.Refresh();
            //SetTempoProcessamento(report);
            return report;

        }


        public rptGuias_Bloqueio_Sumarizado GetReport_Bloqueio_Sum(int xId, string xD_Inicial, string xD_Final, string xCliente, Int16 xEmp)
        {
            rptGuias_Bloqueio_Sumarizado report = new rptGuias_Bloqueio_Sumarizado();
            report.SetDataSource(GetGuiasBloqueio(xId, xD_Inicial, xD_Final, xCliente, xEmp, "S"));
            report.Refresh();
            //SetTempoProcessamento(report);
            return report;

        }


        public rptGuias_Bloqueio GetReport_Bloqueio_Associadas(int xId, string xD_Inicial, string xD_Final, string xCliente, Int16 xEmp)
        {
            rptGuias_Bloqueio report = new rptGuias_Bloqueio();
            report.SetDataSource(GetGuiasBloqueio_Associadas(xId, xD_Inicial, xD_Final, xCliente, xEmp, "A"));
            report.Refresh();
            //SetTempoProcessamento(report);
            return report;

        }


        public rptGuias_Bloqueio_Sumarizado GetReport_Bloqueio_Sum_Associadas(int xId, string xD_Inicial, string xD_Final, string xCliente, Int16 xEmp)
        {
            rptGuias_Bloqueio_Sumarizado report = new rptGuias_Bloqueio_Sumarizado();
            report.SetDataSource(GetGuiasBloqueio_Associadas(xId, xD_Inicial, xD_Final, xCliente, xEmp, "S"));
            report.Refresh();
            //SetTempoProcessamento(report);
            return report;

        }

        public DataSet GetGuias(int zId, string zD_Inicial, string zD_Final, string zCliente, string xConsiderar, Int16 xEmp)
        {

            DataSet ds;

            Clientes_Funcionarios xGuias = new Clientes_Funcionarios();

            ds = xGuias.Gerar_DS_Relatorio_Guias(zId, zD_Inicial, zD_Final, xConsiderar, xEmp);

            Int16 xReg = (Int16)ds.Tables[0].Rows.Count;

            for (Int16 zAux = 0; zAux < ds.Tables[0].Rows.Count; zAux++)
            {
                ds.Tables[0].Rows[zAux]["Registros"] = xReg;
            }
               
            
            return ds;

        }



 


        public DataSet GetGuias_Sum(int zId, string zD_Inicial, string zD_Final, string zCliente, string xConsiderar, Int16 xEmp)
        {

            DataSet ds;

            Clientes_Funcionarios xGuias = new Clientes_Funcionarios();

            ds = xGuias.Gerar_DS_Relatorio_Guias_Sum(zId, zD_Inicial, zD_Final, xConsiderar, xEmp);

            //Int16 xReg = (Int16)ds.Tables[0].Rows.Count;

            //for (Int16 zAux = 0; zAux < ds.Tables[0].Rows.Count; zAux++)
            //{
            //    ds.Tables[0].Rows[zAux]["Registros"] = xReg;
            //}


            return ds;

        }



        public DataSet GetGuiasBloqueio(int zId, string zD_Inicial, string zD_Final, string zCliente, Int16 zEmp, string xTipoRel)
        {

            DataSet ds;

            Clientes_Funcionarios xGuias = new Clientes_Funcionarios();

            ds = xGuias.Gerar_DS_Relatorio_Guias_Bloqueio(zId, zD_Inicial, zD_Final, zEmp, xTipoRel);

            if (xTipoRel == "A")
            {
                Int16 xReg = (Int16)ds.Tables[0].Rows.Count;

                for (Int16 zAux = 0; zAux < ds.Tables[0].Rows.Count; zAux++)
                {
                    ds.Tables[0].Rows[zAux]["Registros"] = xReg;
                }
            }



            return ds;

        }


        public DataSet GetGuiasBloqueio_Associadas(int zId, string zD_Inicial, string zD_Final, string zCliente, Int16 zEmp, string xTipoRel)
        {

            DataSet ds;

            Clientes_Funcionarios xGuias = new Clientes_Funcionarios();

            ds = xGuias.Gerar_DS_Relatorio_Guias_Bloqueio_Associadas(zId, zD_Inicial, zD_Final, zEmp, xTipoRel);

            if (xTipoRel == "A")
            {
                Int16 xReg = (Int16)ds.Tables[0].Rows.Count;

                for (Int16 zAux = 0; zAux < ds.Tables[0].Rows.Count; zAux++)
                {
                    ds.Tables[0].Rows[zAux]["Registros"] = xReg;
                }
            }



            return ds;

        }



    }
}