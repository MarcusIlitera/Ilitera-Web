using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;

using Ilitera.Data;
using Ilitera.Opsa.Data;
using Ilitera.Common;

namespace Ilitera.Opsa.Report
{
	public class DataSourceFaturamentoPedido
	{
		private Faturamento faturamento;

		public DataSourceFaturamentoPedido(Faturamento faturamento)
		{
			this.faturamento = faturamento;
		}

		public RptFaturamentoPedido GetReport()
		{
			RptFaturamentoPedido report = new RptFaturamentoPedido();	
			report.SetDataSource(GetDataSource());
			report.Refresh();			
			return report;
		}

        public DataSet GetDataSource()
        {
            DataSet ds = new DataSet();

            ds.Tables.Add(GetDataTable());

            AddFaturamentoServicoAnalitico(ds);
            AddFaturamentoPedido(ds);
            AddFaturamentoExameBase(ds);

            return ds;
        }

        #region AddFaturamentoPedido

        private void AddFaturamentoPedido(DataSet ds)
        {
            DataRow newRow;

            string criteria = "IdFaturamento=" + faturamento.Id;

            List<FaturamentoPedido> list = new FaturamentoPedido().Find<FaturamentoPedido>(criteria);

            foreach (FaturamentoPedido faturamentoPedido in list)
            {
                faturamentoPedido.IdServico.Find();
                faturamentoPedido.IdPedido.Find();
                faturamentoPedido.IdPedido.IdObrigacao.Find();
                faturamentoPedido.IdCliente.Find();

                newRow = ds.Tables[0].NewRow();

                newRow["IdFaturamento"] = faturamento.Id;
                newRow["ClienteSacado"] = faturamento.IdSacado.ToString();
                newRow["Emissao"] = faturamento.Emissao.ToString("dd-MM-yyyy");
                newRow["Vencimento"] = faturamento.Vencimento.ToString("dd-MM-yyyy"); ;
                newRow["Numero"] = faturamento.Numero.ToString("00000");
                newRow["ValorTotal"] = faturamento.ValorTotal.ToString("n");
                newRow["NomeServico"] = faturamentoPedido.IdServico.GetNomeTipoServico();
                newRow["Unidade"] = faturamentoPedido.IdCliente.NomeAbreviado;

                if (faturamentoPedido.IdPedido.DataConclusao != new DateTime())
                    newRow["DataRealizacao"] = faturamentoPedido.IdPedido.DataConclusao;

                newRow["Descricao"] = faturamentoPedido.GetDescricaoFatura();
                newRow["ValorUnitario"] = faturamentoPedido.ValorUnitario;

                ds.Tables[0].Rows.Add(newRow);
            }
        }
        #endregion

        #region AddFaturamentoExameBase

        private void AddFaturamentoExameBase(DataSet ds)
        {
            DataRow newRow;

            string criteria = "IdFaturamento=" + faturamento.Id
                + " ORDER BY (SELECT DataExame FROM ExameBase WHERE ExameBase.IdExameBase = FaturamentoExameBase.IdExameBase)";

            List<FaturamentoExameBase> list = new FaturamentoExameBase().Find<FaturamentoExameBase>(criteria);

            foreach (FaturamentoExameBase faturamentoExameBase in list)
            {
                faturamentoExameBase.IdServico.Find();
                faturamentoExameBase.IdExameBase.Find();
                faturamentoExameBase.IdCliente.Find();

                newRow = ds.Tables[0].NewRow();

                newRow["IdFaturamento"] = faturamento.Id;
                newRow["ClienteSacado"] = faturamento.IdSacado.ToString();
                newRow["Emissao"] = faturamento.Emissao.ToString("dd-MM-yyyy");
                newRow["Vencimento"] = faturamento.Vencimento.ToString("dd-MM-yyyy"); ;
                newRow["Numero"] = faturamento.Numero.ToString("00000");
                newRow["ValorTotal"] = faturamento.ValorTotal.ToString("n");
                newRow["NomeServico"] = "PCMSO - Exames";
                newRow["Unidade"] = faturamentoExameBase.IdCliente.NomeAbreviado;
                newRow["DataRealizacao"] = faturamentoExameBase.IdExameBase.DataExame;
                newRow["Descricao"] = faturamentoExameBase.IdExameBase.GetDescricaoExame();
                newRow["ValorUnitario"] = faturamentoExameBase.ValorUnitario;

                ds.Tables[0].Rows.Add(newRow);
            }
        }
        #endregion

        #region AddFaturamentoServicoAnalitico

        private void AddFaturamentoServicoAnalitico(DataSet ds)
        {
            DataRow newRow;

            string criteria = "IdFaturamento=" + faturamento.Id + " ORDER BY IdCliente, IdServico";

            List<FaturamentoServicoAnalitico>
                list = new FaturamentoServicoAnalitico().Find<FaturamentoServicoAnalitico>(criteria);

            foreach (FaturamentoServicoAnalitico analitico in list)
            {
                newRow = ds.Tables[0].NewRow();

                newRow["IdFaturamento"] = faturamento.Id;
                newRow["ClienteSacado"] = faturamento.IdSacado.ToString();
                newRow["Emissao"] = faturamento.Emissao.ToString("dd-MM-yyyy");
                newRow["Vencimento"] = faturamento.Vencimento.ToString("dd-MM-yyyy"); ;
                newRow["Numero"] = faturamento.Numero.ToString("00000");
                newRow["ValorTotal"] = faturamento.ValorTotal.ToString("n");
                newRow["NomeServico"] = " Serviços do Contrato";
                newRow["Unidade"] = analitico.IdCliente.ToString();
                newRow["Descricao"] = analitico.IdServico.GetNomeTipoServico().TrimStart() 
                                    + " (" 
                                    + analitico.Quantidade.ToString() 
                                    + " x " 
                                    + analitico.ValorUnitario.ToString("n") 
                                    + ")";
                newRow["ValorUnitario"] = analitico.Quantidade * analitico.ValorUnitario;

                ds.Tables[0].Rows.Add(newRow);
            }
        }
        #endregion

        #region GetDataTable

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
			table.Columns.Add("Unidade", Type.GetType("System.String"));
			table.Columns.Add("DataRealizacao", Type.GetType("System.DateTime"));
			table.Columns.Add("Descricao", Type.GetType("System.String"));
			table.Columns.Add("ValorUnitario", Type.GetType("System.Single"));

			return table;
        }
        #endregion
    }
}
