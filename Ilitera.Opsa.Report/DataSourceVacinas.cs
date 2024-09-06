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
    public class DataSourceVacinas : DataSourceBase
    {

        public DataSourceVacinas()
        {

        }


        public rptVacinas_Atrasadas GetReport_Convocacao(int xId, string xCliente, Int16 xEmp, string xConsiderar)
        {
            rptVacinas_Atrasadas report = new rptVacinas_Atrasadas();
            report.SetDataSource(GetGuiasConv(xId, xCliente, xEmp, "A", xConsiderar));
            report.Refresh();
            //SetTempoProcessamento(report);
            return report;

        }



        public rptVacinas_Analitico GetReport_Bloqueio(int xId, string xD_Inicial, string xD_Final, string xCliente, Int16 xEmp, string xConsiderar)
        {
            rptVacinas_Analitico report = new rptVacinas_Analitico();
            report.SetDataSource(GetGuiasAmb(xId, xD_Inicial, xD_Final, xCliente, xEmp, "A", xConsiderar));
            report.Refresh();
            //SetTempoProcessamento(report);
            return report;

        }


        public rptVacinas_Sumarizado GetReport_Bloqueio_Sum(int xId, string xD_Inicial, string xD_Final, string xCliente, Int16 xEmp, string xConsiderar)
        {
            rptVacinas_Sumarizado report = new rptVacinas_Sumarizado();
            report.SetDataSource(GetGuiasAmb_Sum(xId, xD_Inicial, xD_Final, xCliente, xEmp, "S", xConsiderar));
            report.Refresh();
            //SetTempoProcessamento(report);
            return report;

        }


  
        public DataSet GetGuiasAmb(int zId, string zD_Inicial, string zD_Final, string zCliente, Int16 zEmp, string xTipoRel, string zConsiderar)
        {

            DataSet ds;

            Clientes_Funcionarios xGuias = new Clientes_Funcionarios();

            ds = xGuias.Gerar_DS_Relatorio_Vacinas(zId, zD_Inicial, zD_Final, xTipoRel, zConsiderar, zEmp);

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




        public DataSet GetGuiasConv(int zId, string zCliente, Int16 zEmp, string xTipoRel, string zConsiderar)
        {
            DataSet ds;

            Clientes_Funcionarios xGuias = new Clientes_Funcionarios();

            ds = xGuias.Gerar_DS_Relatorio_Vacinas_Convocacao(zId, xTipoRel, zConsiderar, zEmp);

            return ds;

        }




        //private static DataTable GetTablePreventivo()
        //{
        //    DataTable table = new DataTable("Result");
        //    table.Columns.Add("Colaborador", Type.GetType("System.String"));
        //    table.Columns.Add("Matricula", Type.GetType("System.String"));
        //    table.Columns.Add("Setor", Type.GetType("System.String"));
        //    table.Columns.Add("Funcao", Type.GetType("System.String"));
        //    table.Columns.Add("Admissao", Type.GetType("System.String"));
        //    table.Columns.Add("Data_Vacina", Type.GetType("System.String"));
        //    table.Columns.Add("VacinaTipo", Type.GetType("System.String"));
        //    table.Columns.Add("Dose", Type.GetType("System.String"));
        //    table.Columns.Add("Dias_Vacina", Type.GetType("System.Int32"));
        //    table.Columns.Add("Dias_Admissao", Type.GetType("System.Int32"));
        //    table.Columns.Add("Vacina_Preventiva", Type.GetType("System.String"));
        //    table.Columns.Add("nId_Setor", Type.GetType("System.Int32"));
        //    table.Columns.Add("IdVacinaTipo", Type.GetType("System.Int32"));
        //    return table;
        //}



        private double Obter_Titulagem(string xTitulagem)
        {
            double xRet = 10;
            string xAux = "";


            for(int zCont = 0; zCont < xTitulagem.Length; zCont++ )
            {
                if ("0123456789,".IndexOf(xTitulagem.Substring(zCont, 1)) >= 0)
                {
                    xAux = xAux + xTitulagem.Substring(zCont, 1);
                }
                else if (".".IndexOf(xTitulagem.Substring(zCont, 1)) >= 0)
                {
                    xAux = xAux + ",";
                }
                
            }

            double num;
            if (!double.TryParse(xAux, out num))
                xRet = 10;
            else
                xRet = System.Convert.ToDouble(xAux);


            return xRet;

        }


        public DataSet GetGuiasAmb_Sum(int zId, string zD_Inicial, string zD_Final, string zCliente, Int16 zEmp, string xTipoRel, string zConsiderar)
        {

            DataSet ds;

            Clientes_Funcionarios xGuias = new Clientes_Funcionarios();


            ds = xGuias.Gerar_DS_Relatorio_Ambulatoriais(zId, zD_Inicial, zD_Final, xTipoRel, zConsiderar, zEmp);

            Int32 xReg = (Int16)ds.Tables[0].Rows.Count;

            ds = xGuias.Gerar_DS_Relatorio_Vacinas_Sum(zId, zD_Inicial, zD_Final, xTipoRel, zConsiderar, zEmp);

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