using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Text;

using Ilitera.Data;
using Ilitera.Opsa.Data;
using Ilitera.Common;

namespace Ilitera.Opsa.Report
{
	public class DataSourceBoleto
	{
        List<Faturamento> faturamentos;

        #region Construtor

        public DataSourceBoleto(Faturamento faturamento)
		{
            List<Faturamento> list = new List<Faturamento>(); ;

			list.Add(faturamento);

			this.faturamentos = list;
		}

        public DataSourceBoleto(List<Faturamento> faturamentos)
		{			
			this.faturamentos = faturamentos;
        }

        #endregion

        #region GetReport

        public RptBoleto033 GetReport()
		{
			RptBoleto033 report = new RptBoleto033();	

			report.SetDataSource(GetDataSource());
			report.OpenSubreport("Servicos").SetDataSource(GetDataSourceServicos());

            if (faturamentos.Count == 1 
                && (faturamentos[0]).GetDemostrativoBoleto()!= Faturamento.DemostrativoBoleto.Sem)
            {
                if (faturamentos[0].GetDemostrativoBoleto() == Faturamento.DemostrativoBoleto.ComAnalitico)
                {
                    DataSourceFaturamentoServicoAnalitico ds = new DataSourceFaturamentoServicoAnalitico(faturamentos[0]);
                    report.OpenSubreport("RptFaturamentoServicoAnalitico").SetDataSource(ds.GetDataSource());
                }
                else if (faturamentos[0].GetDemostrativoBoleto() == Faturamento.DemostrativoBoleto.ComCentroCusto)
                {
                    report.OpenSubreport("RptFaturamentoPorCentroCusto").SetDatabaseLogon(  Ilitera.Data.SQLServer.EntitySQLServer.User,
                                                                                            Ilitera.Data.SQLServer.EntitySQLServer.Password);
                }
                else
                {
                    DataSourceFaturamentoPedido ds = new DataSourceFaturamentoPedido(faturamentos[0]);
                    report.OpenSubreport("RptFaturamentoPedido").SetDataSource(ds.GetDataSource());
                }
            }

			report.Refresh();

			return report;
        }
        #endregion

        #region GetDataSource

        private DataSet GetDataSource()
        {
            DataSet ds = new DataSet();
            ds.Tables.Add(GetDataTable());
            DataRow newRow;

            foreach (Faturamento faturamento in faturamentos)
            {
                faturamento.IdCobranca.Find();
                faturamento.IdCedente.Find();
                faturamento.IdSacado.Find();
                faturamento.IdContrato.Find();

                Endereco endereco = faturamento.IdSacado.GetEndereco();
                Endereco enderecoEntrega = faturamento.IdContrato.IdEnderecoEntrega;

                if (enderecoEntrega.Id != 0 && enderecoEntrega.mirrorOld == null)
                    enderecoEntrega.Find();

                double valorDocumento;
                if (faturamento.IdCedente.IndTipoNota == (int)TipoNota.Recibo)//RECIBO
                    valorDocumento = faturamento.ValorTotal - faturamento.ValorTotalImpostos();
                else
                    valorDocumento = faturamento.ValorTotal;

                string strCodigoBarras = faturamento.CodigoBarras("033", "9", valorDocumento, faturamento.Vencimento, faturamento.IdCedente.CodigoCedente, faturamento.Numero.ToString(), "00");
                string strLinhaDigitavel = faturamento.LinhaDigitavel(faturamento.Vencimento, faturamento.IdCedente.CodigoCedente, faturamento.Numero.ToString(), "00", "033", valorDocumento, strCodigoBarras);

                newRow = ds.Tables[0].NewRow();
                newRow["IdFaturamento"] = faturamento.Id;
                newRow["Valor_1"] = "0,00";
                newRow["TipoRecibo"] = faturamento.IdCedente.IndTipoNota == (int)TipoNota.Recibo;
                newRow["Cedente"] = faturamento.IdCedente.RazaoSocial;
                newRow["CnpjCedente"] = faturamento.IdCedente.CNPJ;
                newRow["Emissao"] = faturamento.Emissao.ToString("dd-MM-yyyy");
                newRow["NomeCompleto"] = faturamento.IdSacado.NomeCompleto;
                newRow["Endereco"] = endereco.GetEndereco();
                newRow["Cidade"] = endereco.GetCidade();
                newRow["UF"] = endereco.GetEstado();
                newRow["Cep"] = endereco.Cep;
                newRow["CNPJ"] = faturamento.IdSacado.NomeCodigo;
                newRow["InscricaoEstadual"] = faturamento.IdSacado.IE;
                newRow["InscricaoCCM"] = faturamento.IdSacado.CCM;
                newRow["CondicaoPagamento"] = "À VISTA - MÊS " + faturamento.Referencia;
                newRow["Vencimento"] = faturamento.Vencimento.ToString("dd-MM-yyyy");
                newRow["Valor_IR"] = (faturamento.ValorIR != 0) ? faturamento.ValorIR.ToString("n") : "-";
                newRow["Valor_PIS"] = (faturamento.ValorPisCofinsCsll != 0) ? Convert.ToSingle(faturamento.ValorTotal * 0.0065F).ToString("n") : "-";
                newRow["Valor_CONFINS"] = (faturamento.ValorPisCofinsCsll != 0) ? Convert.ToSingle(faturamento.ValorTotal * 0.0300F).ToString("n") : "-";
                newRow["Valor_CSLL"] = (faturamento.ValorPisCofinsCsll != 0) ? Convert.ToSingle(faturamento.ValorTotal * 0.0100F).ToString("n") : "-";
                newRow["Porcentagem_Impostos"] = faturamento.PctImpostos().ToString("p");
                newRow["Valor_TotalImpostos"] = faturamento.ValorTotalImpostos().ToString("n");
                newRow["Valor_TotalSemImpostos"] = valorDocumento.ToString("n");
                newRow["Valor_Total"] = faturamento.ValorTotal.ToString("n");
                newRow["Agencia_CodigoCedente"] = faturamento.IdCedente.CodigoCedente;
                newRow["DataDocumento"] = faturamento.DataEntrega == new DateTime() ? DateTime.Today.ToString("dd-MM-yyyy") : faturamento.DataEntrega.ToString("dd-MM-yyyy");
                newRow["NumeroDocumento"] = faturamento.Numero.ToString("0000000");
                newRow["EspecieDocumento"] = faturamento.IdCedente.Especie;
                newRow["Aceite"] = faturamento.IdCedente.Aceite;
                newRow["DataProcessamento"] = faturamento.Emissao.ToString("dd-MM-yyyy");
                newRow["NossoNumero"] = faturamento.FormataNossoNumero("105", faturamento.Numero.ToString());
                newRow["UsoBanco"] = faturamento.IdCedente.UsoBanco;
                newRow["Carteira"] = faturamento.IdCedente.Carteira;
                newRow["EspecieMoeda"] = "R$";
                newRow["Quantidade"] = "";
                newRow["Valor"] = "";
                newRow["ValorDocumento"] = valorDocumento.ToString("n");
                newRow["Instrucoes"] = faturamento.IdCobranca.Instrucao.ToString().Replace("0,1%", "R$ " + Convert.ToSingle(valorDocumento * 0.001F).ToString("n"));
                newRow["Desconto_Abatimentos"] = "";
                newRow["OutrasDeducoes"] = "";
                newRow["Mora_Multa"] = "";
                newRow["OutrosAcressimos"] = "";
                newRow["ValorCobrado"] = "";
                newRow["LinhaDigitavel"] = strLinhaDigitavel;
                newRow["CodBarras"] = strCodigoBarras;
                newRow["Motorista"] = faturamento.IdSacado.IdTransporte.Id;
                newRow["EntregaEndereco"] = enderecoEntrega.GetEnderecoCompletoPorLinha();
                newRow["BoletoComDemostrativo"] = faturamentos.Count == 1 ? (int)faturamento.GetDemostrativoBoleto() : (int)Faturamento.DemostrativoBoleto.Sem;

                ds.Tables[0].Rows.Add(newRow);
            }
            return ds;
        }


        #endregion

        #region GetDataSourceServicos

        private DataSet GetDataSourceServicos()
        {
            DataSet ds = new DataSet();

            ds.Tables.Add(GetDataTableServicos());

            DataRow newRow;

            foreach (Faturamento faturamento in faturamentos)
            {
                string where = "IdFaturamento=" + faturamento.Id
                    + " ORDER BY (SELECT IndTipoServico"
                                + " FROM Servico"
                                + " WHERE IdServico = FaturamentoServico.IdServico),"
                                + " (SELECT DescricaoNF"
                                + " FROM Servico"
                                + " WHERE IdServico = FaturamentoServico.IdServico)";

                ArrayList listServicos = new FaturamentoServico().Find(where);

                foreach (FaturamentoServico faturamentoServico in listServicos)
                {
                    newRow = ds.Tables[0].NewRow();

                    newRow["IdFaturamento"] = faturamento.Id;
                    newRow["Quantidade"] = "";
                    newRow["Unidade"] = "";
                    newRow["Descricao"] = faturamentoServico.GetDescricaoServico();
                    newRow["ValorUnitario"] = "";
                    newRow["ValorTotal"] = faturamentoServico.ValorTotal.ToString("n");

                    ds.Tables[0].Rows.Add(newRow);
                }
            }
            return ds;
        }
        #endregion

        #region GetDataTable

        private static DataTable GetDataTable()
		{
			DataSet ds = new DataSet();
			DataTable table = new DataTable("Result");
			table.Columns.Add("IdFaturamento", Type.GetType("System.Int32"));
			table.Columns.Add("TipoRecibo", Type.GetType("System.Boolean"));
			table.Columns.Add("Emissao", Type.GetType("System.String"));
			table.Columns.Add("NomeCompleto", Type.GetType("System.String"));
			table.Columns.Add("Endereco", Type.GetType("System.String"));
			table.Columns.Add("Cidade", Type.GetType("System.String"));
			table.Columns.Add("UF", Type.GetType("System.String"));
			table.Columns.Add("Cep", Type.GetType("System.String"));
			table.Columns.Add("CNPJ", Type.GetType("System.String"));
			table.Columns.Add("InscricaoEstadual", Type.GetType("System.String"));
			table.Columns.Add("InscricaoCCM", Type.GetType("System.String"));
			table.Columns.Add("CondicaoPagamento", Type.GetType("System.String"));
			table.Columns.Add("Vencimento", Type.GetType("System.String"));
			table.Columns.Add("Valor_1", Type.GetType("System.String"));
			table.Columns.Add("Valor_5", Type.GetType("System.String"));
			table.Columns.Add("Valor_6", Type.GetType("System.String"));
			table.Columns.Add("Valor_IR", Type.GetType("System.String"));
			table.Columns.Add("Valor_PIS", Type.GetType("System.String"));
			table.Columns.Add("Valor_CONFINS", Type.GetType("System.String"));
			table.Columns.Add("Valor_CSLL", Type.GetType("System.String"));
			table.Columns.Add("Valor_TotalImpostos", Type.GetType("System.String"));
			table.Columns.Add("Valor_TotalSemImpostos", Type.GetType("System.String"));
			table.Columns.Add("Valor_Total", Type.GetType("System.String"));
			table.Columns.Add("Porcentagem_Impostos", Type.GetType("System.String"));
			table.Columns.Add("Descricao_5", Type.GetType("System.String"));
			table.Columns.Add("Descricao_6", Type.GetType("System.String"));
			table.Columns.Add("EntregaEndereco", Type.GetType("System.String"));
			table.Columns.Add("EntregaTelefone", Type.GetType("System.String"));
			table.Columns.Add("Cedente", Type.GetType("System.String"));
			table.Columns.Add("CnpjCedente", Type.GetType("System.String"));
			table.Columns.Add("Agencia_CodigoCedente", Type.GetType("System.String"));
			table.Columns.Add("DataDocumento", Type.GetType("System.String"));
			table.Columns.Add("NumeroDocumento", Type.GetType("System.String"));
			table.Columns.Add("EspecieDocumento", Type.GetType("System.String"));
			table.Columns.Add("Aceite", Type.GetType("System.String"));
			table.Columns.Add("DataProcessamento", Type.GetType("System.String"));
			table.Columns.Add("NossoNumero", Type.GetType("System.String"));
			table.Columns.Add("UsoBanco", Type.GetType("System.String"));
			table.Columns.Add("Carteira", Type.GetType("System.String"));
			table.Columns.Add("EspecieMoeda", Type.GetType("System.String"));
			table.Columns.Add("Quantidade", Type.GetType("System.String"));
			table.Columns.Add("Valor", Type.GetType("System.String"));
			table.Columns.Add("ValorDocumento", Type.GetType("System.String"));
			table.Columns.Add("Instrucoes", Type.GetType("System.String"));
			table.Columns.Add("Desconto_Abatimentos", Type.GetType("System.String"));
			table.Columns.Add("OutrasDeducoes", Type.GetType("System.String"));
			table.Columns.Add("Mora_Multa", Type.GetType("System.String"));
			table.Columns.Add("OutrosAcressimos", Type.GetType("System.String"));
			table.Columns.Add("ValorCobrado", Type.GetType("System.String"));
			table.Columns.Add("LinhaDigitavel", Type.GetType("System.String"));
			table.Columns.Add("CodBarras", Type.GetType("System.String"));
			table.Columns.Add("Motorista", Type.GetType("System.String"));
            table.Columns.Add("BoletoComDemostrativo", Type.GetType("System.Int32"));
            
			return table;
        }
        #endregion

        #region GetDataTableServicos

        private static DataTable GetDataTableServicos()
		{
			DataSet ds = new DataSet();

			DataTable table = new DataTable("Result");

			table.Columns.Add("IdFaturamento", Type.GetType("System.Int32"));
			table.Columns.Add("Quantidade", Type.GetType("System.String"));
			table.Columns.Add("Unidade", Type.GetType("System.String"));
			table.Columns.Add("Descricao", Type.GetType("System.String"));
			table.Columns.Add("ValorUnitario", Type.GetType("System.String"));
			table.Columns.Add("ValorTotal", Type.GetType("System.String"));

			return table;
        }
        #endregion
    }
}
