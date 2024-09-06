using System;
using System.Data;
using System.Collections;
using Ilitera.Opsa.Data;
using Ilitera.Common;

namespace Ilitera.Opsa.Report
{
	public class DataSourceEPI : DataSourceBase
	{
		public DataSourceEPI()
		{

		}
        
		public RptEntregaEPIEmpr GetReportEntregaEPIEmprTodos(Cliente cliente)
		{
			RptEntregaEPIEmpr report = new RptEntregaEPIEmpr();
			report.SetDataSource(GetEntregaEPIEmprTodos(cliente));
			report.Refresh();

            SetTempoProcessamento(report);

			return report;
		}

		public RptEntregaEPIEmpr GetReportEntregaEPIEmpr(int IdEmpregado)
		{
			RptEntregaEPIEmpr report = new RptEntregaEPIEmpr();
			report.SetDataSource(GetEntregaEPIEmprSelecionado(IdEmpregado));
			report.Refresh();

            SetTempoProcessamento(report);

			return report;
		}


        public RptEntregaEPIEmpr GetReportEntregaEPIEmpr(int IdEmpregado, string xD1, string xD2)
        {
            RptEntregaEPIEmpr report = new RptEntregaEPIEmpr();
            report.SetDataSource(GetEntregaEPIEmprSelecionado_Datas(IdEmpregado, xD1, xD2));
            report.Refresh();

            SetTempoProcessamento(report);

            return report;
        }

		public DataSet GetEntregaEPIEmprTodos(Cliente cliente)
		{
			DataSet ds = new DataSet();
			DataTable table = new DataTable("Result");
			table.Columns.Add("NomeCliente", Type.GetType("System.String"));
			table.Columns.Add("NomeEmpregado", Type.GetType("System.String"));
			table.Columns.Add("DataEntrega", Type.GetType("System.String"));
			table.Columns.Add("Epi", Type.GetType("System.String"));
			table.Columns.Add("CA", Type.GetType("System.String"));	
			table.Columns.Add("QtdEntregue", Type.GetType("System.String"));
			table.Columns.Add("Periodicidade", Type.GetType("System.String"));					
			ds.Tables.Add(table);
			DataRow newRow;	
			cliente.Find();
			//ArrayList listEPIEntrega = new EPICAEntrega().Find("IdEmpregado IN (SELECT nID_EMPREGADO FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEMPREGADO Where nID_EMPR = "+cliente.Id+") ORDER BY DataRecebimento");
            ArrayList listEPIEntrega = new EPICAEntrega().Find("IdEmpregado IN (SELECT nID_EMPREGADO FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEMPREGADO_Funcao Where nID_EMPR = " + cliente.Id + " ) ORDER BY DataRecebimento");
            foreach (EPICAEntrega epiEntrega in listEPIEntrega)
			{				
				epiEntrega.IdEmpregado.Find();
				ArrayList listEPICAEntregaDetalhe = new EPICAEntregaDetalhe().Find("IdEPICAEntrega = " + epiEntrega.Id
				+" ORDER BY (SELECT Nome FROM Epi WHERE IdEPI IN (SELECT IdEPI FROM EPIClienteCA WHERE EPIClienteCA.IdEPIClienteCA = EPICAEntregaDetalhe.IdEPIClienteCA))");
				foreach(EPICAEntregaDetalhe EPIEntrDet in listEPICAEntregaDetalhe)
				{
					newRow = ds.Tables[0].NewRow();							
					newRow["NomeCliente"] = cliente.NomeAbreviado;					
					newRow["NomeEmpregado"] = epiEntrega.IdEmpregado.tNO_EMPG;
					newRow["DataEntrega"]  = epiEntrega.DataRecebimento.ToString("dd-MM-yyyy");
					EPIEntrDet.IdEPIClienteCA.Find();
					EPIEntrDet.IdEPIClienteCA.IdEPI.Find();
					newRow["Epi"]	  = EPIEntrDet.IdEPIClienteCA.IdEPI.ToString();
					EPIEntrDet.IdEPIClienteCA.IdCA.Find();
					newRow["CA"]	= EPIEntrDet.IdEPIClienteCA.IdCA.NumeroCA;	
					newRow["QtdEntregue"]	= EPIEntrDet.QtdEntregue;	
					newRow["Periodicidade"]	= EPIEntrDet.IdEPIClienteCA.GetPeriodicidade();									
					ds.Tables[0].Rows.Add(newRow);
				}
			}
			
			if (ds.Tables[0].Rows.Count == 0)
				throw new Exception("Não é possível imprimir o Relatório Completo de Entrega de EPI de Todos os empregados! Não houve ainda nenhuma entrega de EPI a algum empregado!");
			
			return ds;
		}

		private DataSet GetEntregaEPIEmprSelecionado(int IdEmpregado)
		{
			DataSet ds = new DataSet();
			DataTable table = new DataTable("Result");
			table.Columns.Add("NomeCliente", Type.GetType("System.String"));
			table.Columns.Add("NomeEmpregado", Type.GetType("System.String"));
			table.Columns.Add("DataEntrega", Type.GetType("System.String"));
			table.Columns.Add("Epi", Type.GetType("System.String"));
			table.Columns.Add("CA", Type.GetType("System.String"));	
			table.Columns.Add("QtdEntregue", Type.GetType("System.String"));
			table.Columns.Add("Periodicidade", Type.GetType("System.String"));					
			ds.Tables.Add(table);
			DataRow newRow;	
			ArrayList listEPIEntrega = new EPICAEntrega().Find("IdEmpregado = " + IdEmpregado + " ORDER BY DataRecebimento");
			foreach (EPICAEntrega epiCAEntrega in listEPIEntrega)
			{
				epiCAEntrega.IdEmpregado.Find();
				epiCAEntrega.IdEmpregado.nID_EMPR.Find();
				ArrayList listEPICAEntregaDetalhe = new EPICAEntregaDetalhe().Find("IdEPICAEntrega = " + epiCAEntrega.Id
					+" ORDER BY (SELECT Nome FROM Epi WHERE IdEPI IN (SELECT IdEPI FROM EPIClienteCA WHERE EPIClienteCA.IdEPIClienteCA = EPICAEntregaDetalhe.IdEPIClienteCA))");
				foreach(EPICAEntregaDetalhe EPIEntrDet in listEPICAEntregaDetalhe)
				{
					newRow = ds.Tables[0].NewRow();							
					newRow["NomeCliente"] = epiCAEntrega.IdEmpregado.nID_EMPR.NomeAbreviado;					
					newRow["NomeEmpregado"] = epiCAEntrega.IdEmpregado.tNO_EMPG;
					newRow["DataEntrega"]  = epiCAEntrega.DataRecebimento.ToString("dd-MM-yyyy");
					EPIEntrDet.IdEPIClienteCA.Find();
					EPIEntrDet.IdEPIClienteCA.IdEPI.Find();
					newRow["Epi"]	  = EPIEntrDet.IdEPIClienteCA.IdEPI.ToString();
					EPIEntrDet.IdEPIClienteCA.IdCA.Find();
					newRow["CA"]	= EPIEntrDet.IdEPIClienteCA.IdCA.NumeroCA;	
					newRow["QtdEntregue"]	= EPIEntrDet.QtdEntregue;	
					newRow["Periodicidade"]	= EPIEntrDet.IdEPIClienteCA.GetPeriodicidade();									
					ds.Tables[0].Rows.Add(newRow);
				}
			}

			if (ds.Tables[0].Rows.Count == 0)
				throw new Exception("Não é possível imprimir o Relatório Completo de Entrega de EPI! Não houve nenhuma entrega de EPI ao empregado selecionado!");

			return ds;
		}


        private DataSet GetEntregaEPIEmprSelecionado_Datas(int IdEmpregado, string xD1, string xD2)
        {
            DataSet ds = new DataSet();
            DataTable table = new DataTable("Result");
            table.Columns.Add("NomeCliente", Type.GetType("System.String"));
            table.Columns.Add("NomeEmpregado", Type.GetType("System.String"));
            table.Columns.Add("DataEntrega", Type.GetType("System.String"));
            table.Columns.Add("Epi", Type.GetType("System.String"));
            table.Columns.Add("CA", Type.GetType("System.String"));
            table.Columns.Add("QtdEntregue", Type.GetType("System.String"));
            table.Columns.Add("Periodicidade", Type.GetType("System.String"));
            ds.Tables.Add(table);
            DataRow newRow;
            ArrayList listEPIEntrega = new EPICAEntrega().Find("IdEmpregado = " + IdEmpregado + " ORDER BY DataRecebimento");
            foreach (EPICAEntrega epiCAEntrega in listEPIEntrega)
            {
                epiCAEntrega.IdEmpregado.Find();
                epiCAEntrega.IdEmpregado.nID_EMPR.Find();
                ArrayList listEPICAEntregaDetalhe = new EPICAEntregaDetalhe().Find("IdEPICAEntrega in " 
                    + "(  select IdEPICAEntrega from EPICAEntrega where IdEPICAEntrega = " + epiCAEntrega.Id + " and ( DataRecebimento between convert( smalldatetime, '" + xD1 + "', 103 ) and convert( smalldatetime, '" + xD2 + "', 103 ))  ) " 
                    + " ORDER BY (SELECT Nome FROM Epi WHERE IdEPI IN (SELECT IdEPI FROM EPIClienteCA WHERE EPIClienteCA.IdEPIClienteCA = EPICAEntregaDetalhe.IdEPIClienteCA))");
                foreach (EPICAEntregaDetalhe EPIEntrDet in listEPICAEntregaDetalhe)
                {
                    newRow = ds.Tables[0].NewRow();
                    newRow["NomeCliente"] = epiCAEntrega.IdEmpregado.nID_EMPR.NomeAbreviado;
                    newRow["NomeEmpregado"] = epiCAEntrega.IdEmpregado.tNO_EMPG;
                    newRow["DataEntrega"] = epiCAEntrega.DataRecebimento.ToString("dd-MM-yyyy");
                    EPIEntrDet.IdEPIClienteCA.Find();
                    EPIEntrDet.IdEPIClienteCA.IdEPI.Find();
                    newRow["Epi"] = EPIEntrDet.IdEPIClienteCA.IdEPI.ToString();
                    EPIEntrDet.IdEPIClienteCA.IdCA.Find();
                    newRow["CA"] = EPIEntrDet.IdEPIClienteCA.IdCA.NumeroCA;
                    newRow["QtdEntregue"] = EPIEntrDet.QtdEntregue;
                    newRow["Periodicidade"] = EPIEntrDet.IdEPIClienteCA.GetPeriodicidade();
                    ds.Tables[0].Rows.Add(newRow);
                }
            }

            if (ds.Tables[0].Rows.Count == 0)
                throw new Exception("Não é possível imprimir o Relatório Completo de Entrega de EPI! Não houve nenhuma entrega de EPI ao empregado selecionado!");

            return ds;
        }

	}
}
