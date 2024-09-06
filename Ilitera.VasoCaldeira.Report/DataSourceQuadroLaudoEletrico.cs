using System;
using System.Data;
using System.Data.SqlClient;
using Ilitera.Opsa.Data;
using Ilitera.Common;
using System.Collections;
using System.Drawing;
using Ilitera.Data;

namespace Ilitera.VasoCaldeira.Report
{
    public class DataSourceQuadroLaudoEletrico : DataSourceBase
    {
        private int xIdLaudoEletrico;
        //private Cliente cliente;


        public RptCronogramaLaudoEletrico GetReportCronogramaPGR()
        {
            RptCronogramaLaudoEletrico report = new RptCronogramaLaudoEletrico();
            report.SetDataSource(GetDataSourceLaudoEletrico());
            report.Refresh();
            return report;
        }

        private DataSet GetDataSourceLaudoEletrico()
        {
            DataTable table = new DataTable("Result");
            table.Columns.Add("CarimboCNPJ", Type.GetType("System.String"));
            table.Columns.Add("PlanejamentoAnual", Type.GetType("System.String"));
            table.Columns.Add("Jan", Type.GetType("System.String"));
            table.Columns.Add("Fev", Type.GetType("System.String"));
            table.Columns.Add("Mar", Type.GetType("System.String"));
            table.Columns.Add("Abr", Type.GetType("System.String"));
            table.Columns.Add("Mai", Type.GetType("System.String"));
            table.Columns.Add("Jun", Type.GetType("System.String"));
            table.Columns.Add("Jul", Type.GetType("System.String"));
            table.Columns.Add("Ago", Type.GetType("System.String"));
            table.Columns.Add("Set", Type.GetType("System.String"));
            table.Columns.Add("Out", Type.GetType("System.String"));
            table.Columns.Add("Nov", Type.GetType("System.String"));
            table.Columns.Add("Dez", Type.GetType("System.String"));
            table.Columns.Add("Ano", Type.GetType("System.String"));
            table.Columns.Add("EstrategiaMetAcao", Type.GetType("System.String"));
            table.Columns.Add("FormaRegistro", Type.GetType("System.String"));

            DataSet ds = new DataSet();
            ds.Tables.Add(table);

            //DataRow newRow;

            //ArrayList list = CronogramaLaudoEletrico.GetCronograma(xIdLaudoEletrico);

            //foreach (CronogramaLaudoEletrico cronogramaLaudoEletrico in list)
            //{
            //    if (cronogramaLaudoEletrico.PlanejamentoAnual.Equals(string.Empty))
            //        continue;

            //    newRow = ds.Tables["Result"].NewRow();

            //    //newRow["CarimboCNPJ"] = cliente.GetCarimboCnpjHtml(laudoTecnico.hDT_LAUDO);
            //    newRow["PlanejamentoAnual"] = cronogramaLaudoEletrico.PlanejamentoAnual;
            //    newRow["Jan"] = MesCronograma(cronogramaLaudoEletrico.Mes01);
            //    newRow["Fev"] = MesCronograma(cronogramaLaudoEletrico.Mes02);
            //    newRow["Mar"] = MesCronograma(cronogramaLaudoEletrico.Mes03);
            //    newRow["Abr"] = MesCronograma(cronogramaLaudoEletrico.Mes04);
            //    newRow["Mai"] = MesCronograma(cronogramaLaudoEletrico.Mes05);
            //    newRow["Jun"] = MesCronograma(cronogramaLaudoEletrico.Mes06);
            //    newRow["Jul"] = MesCronograma(cronogramaLaudoEletrico.Mes07);
            //    newRow["Ago"] = MesCronograma(cronogramaLaudoEletrico.Mes08);
            //    newRow["Set"] = MesCronograma(cronogramaLaudoEletrico.Mes09);
            //    newRow["Out"] = MesCronograma(cronogramaLaudoEletrico.Mes10);
            //    newRow["Nov"] = MesCronograma(cronogramaLaudoEletrico.Mes11);
            //    newRow["Dez"] = MesCronograma(cronogramaLaudoEletrico.Mes12);
            //    newRow["Ano"] = cronogramaLaudoEletrico.Ano;
            //    newRow["EstrategiaMetAcao"] = cronogramaLaudoEletrico.MetodologiaAcao;
            //    newRow["FormaRegistro"] = cronogramaLaudoEletrico.FormaRegistro;

            //    ds.Tables["Result"].Rows.Add(newRow);
            //}

            return ds;
        }

        private string MesCronograma(bool bVal)
        {
            string mes = "";

            if (bVal)
            {
                mes = "•"; //"•"; 
            }

            return mes;
        }



        public DataSourceQuadroLaudoEletrico(int IdLaudoEletrico)
        {
            this.xIdLaudoEletrico = IdLaudoEletrico;
            //this.cliente = pcmso.IdCliente;

            //if (this.cliente.mirrorOld == null)
              //  this.cliente.Find();
        }


        //public RptQuadroLaudoEletrico GetReport()
        //{
        //    RptQuadroLaudoEletrico report = new RptQuadroLaudoEletrico();

        //    report.Load();
        //    report.SetDataSource(GetEPI(xIdLaudoEletrico));
        //    report.Refresh();

        //    //report.Subreports[0].SetDataSource(GetDataSource());

        //    //report.SetDatabaseLogon(Ilitera.Data.SQLServer.EntitySQLServer.User,
        //    //                        Ilitera.Data.SQLServer.EntitySQLServer.Password);

        //    //report.SetDatabaseLogon("sa",
        //    //                        "Ilitera160583");

        //    //report.SetDatabaseLogon("sa",
        //    //            "Ilitera160583", "187.45.232.35\\SQLExpress","opsa");  //"187.45.232.35\\SQLExpress", "opsa" );

        //    //report.SetParameterValue("@LaudTec", laudoTecnico.Id);

        //    //SetTempoProcessamento(report);

        //    return report;
        //}


        //public DataSet GetEPI(int xLaudo)
        //{

        //    DataSet ds;

        //    PPRA_EPI xEPI = new PPRA_EPI();

        //    ds = xEPI.Gerar_Relatorio(xLaudo);

        //    return ds;

        //}

        //private DataTable GetCarimboCNPJ()
        //{
        //    DataTable table = new DataTable("CarimboCNPJ");

        //    table.Columns.Add("IdCliente", Type.GetType("System.Int32"));
        //    table.Columns.Add("NomeCliente", Type.GetType("System.String"));
        //    table.Columns.Add("LogradouroNumero", Type.GetType("System.String"));
        //    table.Columns.Add("BairroCEPCidadeEstado", Type.GetType("System.String"));
        //    table.Columns.Add("CNPJ", Type.GetType("System.String"));
        //    table.Columns.Add("Data", Type.GetType("System.String"));

        //    DataRow newRow;

        //    newRow = table.NewRow();
        //    newRow["IdCliente"] = cliente.Id;
        //    newRow["NomeCliente"] = cliente.GetCarimboCnpjHtml(laudoTecnico.hDT_LAUDO);
        //    table.Rows.Add(newRow);

        //    return 	table;
        //}

        //private DataSet GetDataSource()
        //{
        //    DataTable table = new DataTable("Result");
        //    table.Columns.Add("CarimboCNPJ", Type.GetType("System.String"));
        //    table.Columns.Add("PlanejamentoAnual", Type.GetType("System.String"));
        //    table.Columns.Add("Jan", Type.GetType("System.String"));
        //    table.Columns.Add("Fev", Type.GetType("System.String"));
        //    table.Columns.Add("Mar", Type.GetType("System.String"));
        //    table.Columns.Add("Abr", Type.GetType("System.String"));
        //    table.Columns.Add("Mai", Type.GetType("System.String"));
        //    table.Columns.Add("Jun", Type.GetType("System.String"));
        //    table.Columns.Add("Jul", Type.GetType("System.String"));
        //    table.Columns.Add("Ago", Type.GetType("System.String"));
        //    table.Columns.Add("Set", Type.GetType("System.String"));
        //    table.Columns.Add("Out", Type.GetType("System.String"));
        //    table.Columns.Add("Nov", Type.GetType("System.String"));
        //    table.Columns.Add("Dez", Type.GetType("System.String"));
        //    table.Columns.Add("Ano", Type.GetType("System.String"));
        //    table.Columns.Add("EstrategiaMetAcao", Type.GetType("System.String"));
        //    table.Columns.Add("FormaRegistro", Type.GetType("System.String"));

        //    DataSet ds = new DataSet();
        //    ds.Tables.Add(table);

        //    DataRow newRow;

        //    ArrayList list = CronogramaPPRA.GetCronograma(laudoTecnico);

        //    foreach (CronogramaPPRA cronogramaPPRA in list)
        //    {
        //        if (cronogramaPPRA.PlanejamentoAnual.Equals(string.Empty))
        //            continue;

        //        newRow = ds.Tables["Result"].NewRow();

        //        //newRow["CarimboCNPJ"] = cliente.GetCarimboCnpjHtml(laudoTecnico.hDT_LAUDO);

        //        ds.Tables["Result"].Rows.Add(newRow);
        //        break;
        //    }

        //    return ds;
        //}


    }
}
