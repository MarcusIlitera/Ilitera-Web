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
    public class DataSourceAmbulatoriais : DataSourceBase
    {

        public DataSourceAmbulatoriais()
        {

        }



        public rptAmbulatoriais_Analitico GetReport_Bloqueio(int xId, string xD_Inicial, string xD_Final, string xCliente, Int16 xEmp, string xConsiderar)
        {
            rptAmbulatoriais_Analitico report = new rptAmbulatoriais_Analitico();
            report.SetDataSource(GetGuiasAmb(xId, xD_Inicial, xD_Final, xCliente, xEmp, "A", xConsiderar));
            report.Refresh();
            //SetTempoProcessamento(report);
            return report;

        }


        public rptAmbulatoriais_Sumarizado GetReport_Bloqueio_Sum(int xId, string xD_Inicial, string xD_Final, string xCliente, Int16 xEmp, string xConsiderar)
        {
            rptAmbulatoriais_Sumarizado report = new rptAmbulatoriais_Sumarizado();
            report.SetDataSource(GetGuiasAmb_Sum(xId, xD_Inicial, xD_Final, xCliente, xEmp, "S", xConsiderar));
            report.Refresh();
            //SetTempoProcessamento(report);
            return report;

        }


  
        public DataSet GetGuiasAmb(int zId, string zD_Inicial, string zD_Final, string zCliente, Int16 zEmp, string xTipoRel, string zConsiderar)
        {

            DataSet ds;

            Clientes_Funcionarios xGuias = new Clientes_Funcionarios();

            ds = xGuias.Gerar_DS_Relatorio_Ambulatoriais(zId, zD_Inicial, zD_Final, xTipoRel, zConsiderar, zEmp);

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



        public DataSet GetGuiasAmb_Sum(int zId, string zD_Inicial, string zD_Final, string zCliente, Int16 zEmp, string xTipoRel, string zConsiderar)
        {

            DataSet ds;

            Clientes_Funcionarios xGuias = new Clientes_Funcionarios();


            ds = xGuias.Gerar_DS_Relatorio_Ambulatoriais(zId, zD_Inicial, zD_Final, xTipoRel, zConsiderar, zEmp);

            Int32 xReg = (Int16)ds.Tables[0].Rows.Count;

            ds = xGuias.Gerar_DS_Relatorio_Ambulatoriais_Sum(zId, zD_Inicial, zD_Final, xTipoRel, zConsiderar, zEmp);

            //if (xTipoRel == "A")
            //{
            //    Int16 xReg = (Int16)ds.Tables[0].Rows.Count;

                for (Int16 zAux = 0; zAux < ds.Tables[0].Rows.Count; zAux++)
                {
                    ds.Tables[0].Rows[zAux]["Exame"] = "Total Geral de Ocorrências:   " + xReg.ToString().Trim();
                }
            //}

            return ds;

        }


    }
}