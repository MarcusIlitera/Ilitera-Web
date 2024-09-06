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
	public class DataSourceCronograma : DataSourceBase
	{
	
        private Cliente cliente;


        public RptCronogramaLaudoEletrico GetReportCronogramaPGR()
        {
            RptCronogramaLaudoEletrico report = new RptCronogramaLaudoEletrico();
            report.SetDataSource(GetDataSourceCronograma());
            report.Refresh();
            return report;
        }

        public RptCronogramaLaudoEletrico GetReportCronogramaPGR(Int32 xCliente)
        {
            cliente = new Cliente(xCliente);

            RptCronogramaLaudoEletrico report = new RptCronogramaLaudoEletrico();
            report.SetDataSource(GetDataSourceCronograma());
            report.Refresh();
            return report;
        }

        private DataSet GetDataSourceCronograma()
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
            table.Columns.Add("Logotipo", Type.GetType("System.String"));

            DataSet ds = new DataSet();
            ds.Tables.Add(table);


            string xArq;

            if (cliente.Logo_Laudos == true)
            {
                xArq = cliente.Logotipo;
            }
            else
            {
                if (Ilitera.Data.SQLServer.EntitySQLServer.xDB2.IndexOf("Daiti") > 0)
                {
                    xArq = "I:\\fotosDocsDigitais\\_Daiti\\Relatorio_Empresa.jpg";
                }
                else if (Ilitera.Data.SQLServer.EntitySQLServer.xDB2.IndexOf("Rophe") > 0)
                {
                    xArq = "I:\\fotosDocsDigitais\\_Rophe\\Relatorio_Empresa.jpg";
                }
                else if (Ilitera.Data.SQLServer.EntitySQLServer.xDB2.IndexOf("Prajna") > 0)
                {
                    xArq = "I:\\fotosDocsDigitais\\_Prajna\\Relatorio_Empresa.jpg";
                }
                else if (Ilitera.Data.SQLServer.EntitySQLServer.xDB2.IndexOf("Fiore") > 0)
                {
                    xArq = "I:\\fotosDocsDigitais\\_Fiore\\Relatorio_Empresa.jpg";
                }
                else if (Ilitera.Data.SQLServer.EntitySQLServer.xDB2.IndexOf("Mappas") > 0)
                {
                    xArq = "I:\\fotosDocsDigitais\\_Mappas\\Relatorio_Empresa.jpg";
                }
                else
                {
                    xArq = "I:\\fotosDocsDigitais\\_Ilitera\\Relatorio_Empresa.jpg";
                }
            }


            DataRow newRow;

            //ArrayList list = CronogramaPCMSO.GetCronograma(pcmso);
            
            //foreach (CronogramaPCMSO cronogramaPCMSO in list)
            //{
            //    if (cronogramaPCMSO.PlanejamentoAnual.Equals(string.Empty))
            //        continue;

                newRow = ds.Tables["Result"].NewRow();

                //newRow["CarimboCNPJ"] = cliente.GetCarimboCnpjHtml(laudoTecnico.hDT_LAUDO);
                //newRow["PlanejamentoAnual"] = cronogramaPCMSO.PlanejamentoAnual;
                //newRow["Jan"] = MesCronograma(cronogramaPCMSO.Mes01);
                //newRow["Fev"] = MesCronograma(cronogramaPCMSO.Mes02);
                //newRow["Mar"] = MesCronograma(cronogramaPCMSO.Mes03);
                //newRow["Abr"] = MesCronograma(cronogramaPCMSO.Mes04);
                //newRow["Mai"] = MesCronograma(cronogramaPCMSO.Mes05);
                //newRow["Jun"] = MesCronograma(cronogramaPCMSO.Mes06);
                //newRow["Jul"] = MesCronograma(cronogramaPCMSO.Mes07);
                //newRow["Ago"] = MesCronograma(cronogramaPCMSO.Mes08);
                //newRow["Set"] = MesCronograma(cronogramaPCMSO.Mes09);
                //newRow["Out"] = MesCronograma(cronogramaPCMSO.Mes10);
                //newRow["Nov"] = MesCronograma(cronogramaPCMSO.Mes11);
                //newRow["Dez"] = MesCronograma(cronogramaPCMSO.Mes12);
                //newRow["Ano"] = cronogramaPCMSO.Ano;
                //newRow["EstrategiaMetAcao"] = cronogramaPCMSO.MetodologiaAcao;
                //newRow["FormaRegistro"] = cronogramaPCMSO.FormaRegistro;

                newRow["CarimboCNPJ"] = " ";
                newRow["Logotipo"] = xArq;

                newRow["Jan"] = "x";
                newRow["Fev"] = "x";
                newRow["Mar"] = "x";
                newRow["Abr"] = "x";
                newRow["Mai"] = "x";
                newRow["Jun"] = "x";
                newRow["Jul"] = "x";
                newRow["Ago"] = "x";
                newRow["Set"] = "x";
                newRow["Out"] = "x";
                newRow["Nov"] = "x";
                newRow["Dez"] = "x";
                newRow["PlanejamentoAnual"] = "2013";
                newRow["Ano"] = "2013";
                newRow["EstrategiaMetAcao"] = "teste";
                newRow["FormaRegistro"] = "teste2";

                ds.Tables["Result"].Rows.Add(newRow);



                newRow = ds.Tables["Result"].NewRow();

                //newRow["CarimboCNPJ"] = cliente.GetCarimboCnpjHtml(laudoTecnico.hDT_LAUDO);
                //newRow["PlanejamentoAnual"] = cronogramaPCMSO.PlanejamentoAnual;
                //newRow["Jan"] = MesCronograma(cronogramaPCMSO.Mes01);
                //newRow["Fev"] = MesCronograma(cronogramaPCMSO.Mes02);
                //newRow["Mar"] = MesCronograma(cronogramaPCMSO.Mes03);
                //newRow["Abr"] = MesCronograma(cronogramaPCMSO.Mes04);
                //newRow["Mai"] = MesCronograma(cronogramaPCMSO.Mes05);
                //newRow["Jun"] = MesCronograma(cronogramaPCMSO.Mes06);
                //newRow["Jul"] = MesCronograma(cronogramaPCMSO.Mes07);
                //newRow["Ago"] = MesCronograma(cronogramaPCMSO.Mes08);
                //newRow["Set"] = MesCronograma(cronogramaPCMSO.Mes09);
                //newRow["Out"] = MesCronograma(cronogramaPCMSO.Mes10);
                //newRow["Nov"] = MesCronograma(cronogramaPCMSO.Mes11);
                //newRow["Dez"] = MesCronograma(cronogramaPCMSO.Mes12);
                //newRow["Ano"] = cronogramaPCMSO.Ano;
                //newRow["EstrategiaMetAcao"] = cronogramaPCMSO.MetodologiaAcao;
                //newRow["FormaRegistro"] = cronogramaPCMSO.FormaRegistro;

                newRow["CarimboCNPJ"] = " ";
                newRow["Logotipo"] = xArq;

                newRow["Jan"] = "x";
                newRow["Fev"] = "x";
                newRow["Mar"] = "x";
                newRow["Abr"] = "x";
                newRow["Mai"] = "x";
                newRow["Jun"] = "x";
                newRow["Jul"] = "x";
                newRow["Ago"] = "x";
                newRow["Set"] = "x";
                newRow["Out"] = "x";
                newRow["Nov"] = "x";
                newRow["Dez"] = "x";
                newRow["PlanejamentoAnual"] = "2014";
                newRow["Ano"] = "2014";
                newRow["EstrategiaMetAcao"] = "teste";
                newRow["FormaRegistro"] = "teste2";

                ds.Tables["Result"].Rows.Add(newRow);
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

		

		

	

	}
}
