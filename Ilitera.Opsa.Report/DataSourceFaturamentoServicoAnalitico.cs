using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using Ilitera.Data;
using Ilitera.Opsa.Data;
using Ilitera.Common;

namespace Ilitera.Opsa.Report
{
	public class DataSourceFaturamentoServicoAnalitico
	{
		private Faturamento faturamento;

        public DataSourceFaturamentoServicoAnalitico(Faturamento faturamento)
		{
			this.faturamento = faturamento;
		}

		public RptFaturamentoServicoAnalitico GetReport()
		{
            RptFaturamentoServicoAnalitico report = new RptFaturamentoServicoAnalitico();	
			report.SetDataSource(GetDataSource());
			report.Refresh();			
			return report;
		}

        public RptFaturamentoPorCentroCusto GetReportPorCentroCusto()
        {
            RptFaturamentoPorCentroCusto report = new RptFaturamentoPorCentroCusto();
            report.Load();
            report.Refresh();

            Ilitera.Common.DataSourceBase.VerificarProvider(report);

            report.SetDatabaseLogon(Ilitera.Data.SQLServer.EntitySQLServer.User,
                                    Ilitera.Data.SQLServer.EntitySQLServer.Password);
            report.SetParameterValue("@IdFaturamento", faturamento.Id);

            return report;
        }

        public DataSet GetDataSource()
        {
            DataSet ds = new DataSet();

            ds.Tables.Add(GetDataTable());

            AddFaturamentoServicoAnalitico(ds);

            //AddFaturamentoExameBase(ds);

            return ds;
        }

        private void AddFaturamentoServicoAnalitico(DataSet ds)
        {
            string criteria = "IdFaturamento=" + faturamento.Id + " ORDER BY IdCliente, IdServico";

            List<FaturamentoServicoAnalitico>
                list = new FaturamentoServicoAnalitico().Find<FaturamentoServicoAnalitico>(criteria);

            DataRow newRow;

            foreach (FaturamentoServicoAnalitico analitico in list)
            {
                newRow = ds.Tables[0].NewRow();

                newRow["IdFaturamento"] = faturamento.Id;
                newRow["ClienteSacado"] = faturamento.IdSacado.ToString();
                newRow["Emissao"] = faturamento.Emissao.ToString("dd-MM-yyyy");
                newRow["Vencimento"] = faturamento.Vencimento.ToString("dd-MM-yyyy"); ;
                newRow["Numero"] = faturamento.Numero.ToString("00000");
                newRow["ValorTotal"] = faturamento.ValorTotal.ToString("n");
                newRow["NomeServico"] = analitico.IdServico.GetNomeTipoServico().TrimStart();
                //newRow["Empregados"] = GetEmpregados(analitico);
                newRow["Unidade"] = analitico.IdCliente.ToString();
                newRow["Quantidade"] = analitico.Quantidade;
                newRow["ValorUnitario"] = analitico.ValorUnitario;
                newRow["Total"] = analitico.ValorTotal;

                ds.Tables[0].Rows.Add(newRow);
            }
        }

        //private void AddFaturamentoExameBase(DataSet ds)
        //{
        //    DataRow newRow;

        //    string criteria = "IdFaturamento=" + faturamento.Id
        //        + " ORDER BY (SELECT DataExame FROM ExameBase WHERE ExameBase.IdExameBase = FaturamentoExameBase.IdExameBase)";

        //    List<FaturamentoExameBase> list = new FaturamentoExameBase().Find<FaturamentoExameBase>(criteria);

        //    foreach (FaturamentoExameBase faturamentoExameBase in list)
        //    {
        //        newRow = ds.Tables[0].NewRow();

        //        newRow["IdFaturamento"] = faturamento.Id;
        //        newRow["ClienteSacado"] = faturamento.IdSacado.ToString();
        //        newRow["Emissao"] = faturamento.Emissao.ToString("dd-MM-yyyy");
        //        newRow["Vencimento"] = faturamento.Vencimento.ToString("dd-MM-yyyy"); ;
        //        newRow["Numero"] = faturamento.Numero.ToString("00000");
        //        newRow["ValorTotal"] = faturamento.ValorTotal.ToString("n");
        //        newRow["NomeServico"] = "Exames";

        //        newRow["Unidade"] = faturamentoExameBase.IdCliente.ToString();
        //        newRow["Quantidade"] = 1;
        //        newRow["ValorUnitario"] = faturamentoExameBase.ValorUnitario;
        //        newRow["Total"] = faturamentoExameBase.ValorUnitario;


        //        ds.Tables[0].Rows.Add(newRow);
        //    }
        //}

        private string GetEmpregados(FaturamentoServicoAnalitico analitico)
        {
            if (analitico.IdServico.mirrorOld == null)
                analitico.IdServico.Find();

            //if (analitico.IdServico.IndTipoServico != (int)UnidadeServico.EmpregadosAtivos)
            //    return string.Empty;

            string criteria = "nID_EMPREGADO IN (SELECT IdEmpregado"
                                                + " FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.FaturamentoServicoAnaliticoEmpregado"
                                                + " WHERE IdFaturamentoServicoAnalitico=" + analitico.Id + ")";

            DataSet ds = new Empregado().GetIdNome("tNO_EMPG", criteria);

            System.Text.StringBuilder ret = new System.Text.StringBuilder();

            int i = 1;

            foreach (DataRow row in ds.Tables[0].Rows)
                ret.Append(Convert.ToString(Convert.ToString(i++) + ") " + row["Nome"]) + "; ");

            return ret.ToString();
        }

		private static DataTable GetDataTable()
		{
			DataTable table = new DataTable("Result");

			table.Columns.Add("IdFaturamento", Type.GetType("System.Int32"));
            table.Columns.Add("ClienteSacado", Type.GetType("System.String"));
			table.Columns.Add("Emissao", Type.GetType("System.String"));
			table.Columns.Add("Vencimento", Type.GetType("System.String"));
			table.Columns.Add("Numero", Type.GetType("System.String"));
			table.Columns.Add("ValorTotal", Type.GetType("System.String"));
			table.Columns.Add("NomeServico", Type.GetType("System.String"));
            table.Columns.Add("Empregados", Type.GetType("System.String"));
			table.Columns.Add("Unidade", Type.GetType("System.String"));
            table.Columns.Add("Quantidade", Type.GetType("System.Single"));
			table.Columns.Add("ValorUnitario", Type.GetType("System.Single"));
            table.Columns.Add("Total", Type.GetType("System.Single"));

			return table;
		}
	}
}
