using System;
using System.Data;
using System.Data.SqlClient;
using Ilitera.Opsa.Data;
using Ilitera.Common;
using System.Collections;
using System.Drawing;
using Ilitera.Data;

namespace Ilitera.Sied.Report
{	
	public class DataSourceQuadroEPI : DataSourceBase
	{
		private LaudoTecnico laudoTecnico;
		private Cliente cliente;

		public DataSourceQuadroEPI(Cliente cliente)
		{
			this.cliente = cliente;
            this.laudoTecnico = LaudoTecnico.GetUltimoLaudo(cliente.Id);
		}

		public DataSourceQuadroEPI(LaudoTecnico laudoTecnico)
		{
			this.laudoTecnico = laudoTecnico;
            this.cliente = laudoTecnico.nID_EMPR;

            if(this.cliente.mirrorOld==null)
                this.cliente.Find();
		}

        public DataSourceQuadroEPI(Cliente cliente, LaudoTecnico laudoTecnico)
        {
            this.laudoTecnico = laudoTecnico;
            this.cliente = cliente;

            if (this.cliente.mirrorOld == null)
                this.cliente.Find();
        }







        public RptQuadroEPI GetReport()
        {
            RptQuadroEPI report = new RptQuadroEPI();

            report.Load();
            report.SetDataSource(GetEPI(laudoTecnico.Id));
            report.Refresh();

            report.Subreports[0].SetDataSource(GetDataSource(1));

            //report.SetDatabaseLogon(Ilitera.Data.SQLServer.EntitySQLServer.User,
            //                        Ilitera.Data.SQLServer.EntitySQLServer.Password);

            //report.SetDatabaseLogon("sa",
            //                        "Ilitera160583");

            //report.SetDatabaseLogon("sa",
            //            "Ilitera160583", "187.45.232.35\\SQLExpress","opsa");  //"187.45.232.35\\SQLExpress", "opsa" );

            //report.SetParameterValue("@LaudTec", laudoTecnico.Id);

            //SetTempoProcessamento(report);

            return report;
        }



        public RptQuadroEPI_PGR GetReportPGR()
        {
            RptQuadroEPI_PGR report = new RptQuadroEPI_PGR();

            report.Load();
            report.SetDataSource(GetEPI(laudoTecnico.Id));
            report.Refresh();

            report.Subreports[0].SetDataSource(GetDataSource(2));

            //report.SetDatabaseLogon(Ilitera.Data.SQLServer.EntitySQLServer.User,
            //                        Ilitera.Data.SQLServer.EntitySQLServer.Password);

            //report.SetDatabaseLogon("sa",
            //                        "Ilitera160583");

            //report.SetDatabaseLogon("sa",
            //            "Ilitera160583", "187.45.232.35\\SQLExpress","opsa");  //"187.45.232.35\\SQLExpress", "opsa" );

            //report.SetParameterValue("@LaudTec", laudoTecnico.Id);

            //SetTempoProcessamento(report);

            return report;
        }



        public RptQuadroEPI_PGRTR GetReportPGRTR()
        {
            RptQuadroEPI_PGRTR report = new RptQuadroEPI_PGRTR();

            report.Load();
            report.SetDataSource(GetEPI(laudoTecnico.Id));
            report.Refresh();

            report.Subreports[0].SetDataSource(GetDataSource(2));

            //report.SetDatabaseLogon(Ilitera.Data.SQLServer.EntitySQLServer.User,
            //                        Ilitera.Data.SQLServer.EntitySQLServer.Password);

            //report.SetDatabaseLogon("sa",
            //                        "Ilitera160583");

            //report.SetDatabaseLogon("sa",
            //            "Ilitera160583", "187.45.232.35\\SQLExpress","opsa");  //"187.45.232.35\\SQLExpress", "opsa" );

            //report.SetParameterValue("@LaudTec", laudoTecnico.Id);

            //SetTempoProcessamento(report);

            return report;
        }



        public DataSet GetEPI(int xLaudo)
        {

            DataSet ds;

            PPRA_EPI xEPI = new PPRA_EPI();

            ds = xEPI.Gerar_Relatorio(xLaudo);

            return ds;

        }

        private DataTable GetCarimboCNPJ()
        {
            DataTable table = new DataTable("CarimboCNPJ");

            table.Columns.Add("IdCliente", Type.GetType("System.Int32"));
            table.Columns.Add("NomeCliente", Type.GetType("System.String"));
            table.Columns.Add("LogradouroNumero", Type.GetType("System.String"));
            table.Columns.Add("BairroCEPCidadeEstado", Type.GetType("System.String"));
            table.Columns.Add("CNPJ", Type.GetType("System.String"));
            table.Columns.Add("Data", Type.GetType("System.String"));

            DataRow newRow;

            newRow = table.NewRow();
            newRow["IdCliente"] = cliente.Id;
            newRow["NomeCliente"] = cliente.GetCarimboCnpjHtml(laudoTecnico.hDT_LAUDO);
            table.Rows.Add(newRow);

            return table;
        }

        private DataSet GetDataSource( Int16 rLaudo)
        {
            //rLaudo = 1  PPRA   rLaudo = 2  PGR

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

            DataRow newRow;

            ArrayList list = CronogramaPPRA.GetCronograma(laudoTecnico);

            foreach (CronogramaPPRA cronogramaPPRA in list)
            {
                if (cronogramaPPRA.PlanejamentoAnual.Equals(string.Empty))
                    continue;

                newRow = ds.Tables["Result"].NewRow();

                System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");

                DateTime zData = laudoTecnico.hDT_LAUDO;

                //if (Ilitera.Data.SQLServer.EntitySQLServer.xDB2.IndexOf("Prajna") < 0)
                //{
                if (rLaudo == 2)
                {
                    if (zData < new DateTime(2022, 1, 3))
                    {
                        zData = new DateTime(2022, 1, 3);
                    }
                }


                newRow["CarimboCNPJ"] = cliente.GetCarimboCnpjHtml(laudoTecnico.hDT_LAUDO) + "<br>Data do Laudo:" + zData.ToString("dd/MM/yyyy", ptBr); ;

                ds.Tables["Result"].Rows.Add(newRow);
                break;
            }

            return ds;
        }

        
        //public RptQuadroEPI GetReport()
        //{
        //    RptQuadroEPI report = new RptQuadroEPI();	

        //    report.Load();
			
        //    report.Refresh();
			
        //    report.Subreports[0].SetDataSource(GetCarimboCNPJ());
			
        //    //report.SetDatabaseLogon(Ilitera.Data.SQLServer.EntitySQLServer.User,
        //    //                        Ilitera.Data.SQLServer.EntitySQLServer.Password);


        //    report.SetDatabaseLogon("sa",
        //                            "Ilitera6096");

        //    report.SetDatabaseLogon("sa",
        //                            "Ilitera6096");

        //    report.SetParameterValue("@LaudTec", laudoTecnico.Id);

        //    SetTempoProcessamento(report);

        //    return report;
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
	}
}
