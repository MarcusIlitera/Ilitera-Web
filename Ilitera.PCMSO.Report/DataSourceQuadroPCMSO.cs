using System;
using System.Data;
using System.Data.SqlClient;
using Ilitera.Opsa.Data;
using Ilitera.Common;
using System.Collections;
using System.Drawing;
using Ilitera.Data;

namespace Ilitera.PCMSO.Report
{	
	public class DataSourceQuadroPCMSO : DataSourceBase
	{
		private Pcmso pcmso;
        private Cliente cliente;


        public RptCronogramaPCMSO GetReportCronogramaPGR()
        {
            RptCronogramaPCMSO report = new RptCronogramaPCMSO();
            report.SetDataSource(GetDataSourcePCMSO());
            report.Refresh();
            return report;
        }

        private DataSet GetDataSourcePCMSO()
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
            table.Columns.Add("Prioridade", Type.GetType("System.Int16"));
            table.Columns.Add("Titulo_Principal", Type.GetType("System.String"));

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

            ArrayList list = CronogramaPCMSO.GetCronograma(pcmso);
            
            foreach (CronogramaPCMSO cronogramaPCMSO in list)
            {
                if (cronogramaPCMSO.PlanejamentoAnual.Equals(string.Empty))
                    continue;
                
                newRow = ds.Tables["Result"].NewRow();

                if (pcmso.DataPcmso.Year >= 2022)
                    newRow["Titulo_Principal"] = @"""Esta Norma Regulamentadora - NR estabelece diretrizes e requisitos para o desenvolvimento do Programa de Controle Médico de Saúde Ocupacional -PCMSO nas organizações, com o objetivo de proteger e preservar a saúde de seus empregados em relação aos riscos ocupacionais, conforme avaliação de riscos do Programa de Gerenciamento de Risco -PGR da organização. Esta Norma se aplica às organizações e aos órgãos públicos da administração direta e indireta, bem como aos órgãos dos poderes legislativo e judiciário e ao Ministério Público, que possuam empregados regidos pela Consolidação das Leis do Trabalho - CLT.""";
                else
                    newRow["Titulo_Principal"] = @"""Esta Norma Regulamentadora - NR estabelece a obrigatoriedade de elaboração e implementação, por parte de todos os empregadores e instituições que admitam trabalhadores como empregados, do Programa de Controle Médico de Saúde Ocupacional -PCMSO, com o objetivo de promoção e preservação da saúde do conjunto dos seus trabalhadores.Esta NR estabelece os parâmetros mínimos e diretrizes gerais a serem observados na execução do PCMSO, podendo os mesmos ser ampliados mediante negociação coletiva de trabalho."" (itens 7.1.1 e 7.1.2 da NR 7)";


                //newRow["CarimboCNPJ"] = cliente.GetCarimboCnpjHtml(laudoTecnico.hDT_LAUDO);
                newRow["PlanejamentoAnual"] = cronogramaPCMSO.PlanejamentoAnual;
                newRow["Jan"] = MesCronograma(cronogramaPCMSO.Mes01);
                newRow["Fev"] = MesCronograma(cronogramaPCMSO.Mes02);
                newRow["Mar"] = MesCronograma(cronogramaPCMSO.Mes03);
                newRow["Abr"] = MesCronograma(cronogramaPCMSO.Mes04);
                newRow["Mai"] = MesCronograma(cronogramaPCMSO.Mes05);
                newRow["Jun"] = MesCronograma(cronogramaPCMSO.Mes06);
                newRow["Jul"] = MesCronograma(cronogramaPCMSO.Mes07);
                newRow["Ago"] = MesCronograma(cronogramaPCMSO.Mes08);
                newRow["Set"] = MesCronograma(cronogramaPCMSO.Mes09);
                newRow["Out"] = MesCronograma(cronogramaPCMSO.Mes10);
                newRow["Nov"] = MesCronograma(cronogramaPCMSO.Mes11);
                newRow["Dez"] = MesCronograma(cronogramaPCMSO.Mes12);
                newRow["Ano"] = cronogramaPCMSO.Ano;
                newRow["EstrategiaMetAcao"] = cronogramaPCMSO.MetodologiaAcao;
                newRow["FormaRegistro"] = cronogramaPCMSO.FormaRegistro;
                newRow["Logotipo"] = xArq;
                newRow["Prioridade"] = cronogramaPCMSO.Prioridade;

                ds.Tables["Result"].Rows.Add(newRow);
            }

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

		

		public DataSourceQuadroPCMSO(Pcmso pcmso)
		{
            this.pcmso = pcmso;
            this.cliente = pcmso.IdCliente;

            if(this.cliente.mirrorOld==null)
                this.cliente.Find();
		}

        public DataSourceQuadroPCMSO(Cliente cliente, Pcmso pcmso)
        {
            this.pcmso = pcmso;
            this.cliente = cliente;

            if (this.cliente.mirrorOld == null)
                this.cliente.Find();
        }

		public RptQuadroPCMSO GetReport()
		{
			RptQuadroPCMSO report = new RptQuadroPCMSO();	

			report.Load();
            report.SetDataSource(GetEPI(pcmso.Id));
			report.Refresh();
			
			//report.Subreports[0].SetDataSource(GetDataSource());
			
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
