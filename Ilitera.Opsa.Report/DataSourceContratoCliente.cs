using System;
using System.Collections;
using System.Data;
using System.Drawing;
using Ilitera.Data;
using Ilitera.Opsa.Data;
using Ilitera.Common;

namespace Ilitera.Opsa.Report
{
    public class DataSourceContratoCliente
    {

        private Contrato contrato;

        public DataSourceContratoCliente(Contrato contrato)
        {
            this.contrato = contrato;
        }

        public RptContratoCliente GetReport()
        {
            RptContratoCliente report = new RptContratoCliente();
            report.OpenSubreport("Servicos").SetDataSource(GetDataSourceServicos());
            report.OpenSubreport("ServicosPorPedido").SetDataSource(GetDataSourceServicosPorPedido());
            report.SetDataSource(GetDataSource());
            report.Refresh();
            return report;
        }

        private DataSet GetDataSource()
        {
            DataSet ds = new DataSet();
            ds.Tables.Add(GetDataTable());
            DataRow newRow;

            contrato.IdCliente.Find();
            contrato.IdCedente.Find();
            contrato.IdSacado.Find();
            contrato.IdCobranca.Find();
            contrato.IdMotivoRecisao.Find();
            contrato.IdServico.Find();

            newRow = ds.Tables[0].NewRow();
            newRow["IdCliente"] = contrato.IdCliente.Id;
            newRow["IdContrato"] = contrato.Id;
            newRow["NomeCliente"] = contrato.IdCliente.NomeAbreviado;
            newRow["Inicio"] = contrato.Inicio.ToString("dd-MM-yyyy");
            newRow["Termino"] = contrato.Termino == new DateTime() ? "-" : contrato.Termino.ToString("dd-MM-yyyy");
            newRow["Descricao"] = contrato.Descricao;
            newRow["DiaVencimento"] = contrato.DiaVencimento;
            newRow["Carencia"] = contrato.DiaCarencia;
            newRow["PrimeiroVencimento"] = contrato.PrimeiroVencimento().ToString("dd-MM-yyyy");
            newRow["Renegociar"] = contrato.Renegociar ? "SIM" : "NÃO";
            newRow["RenegociarEm"] = contrato.DataRenegociar == new DateTime() ? "-" : contrato.DataRenegociar.ToString("dd-MM-yyyy"); ;
            newRow["DataRecisao"] = contrato.DataRecisao == new DateTime() ? "-" : contrato.DataRecisao.ToString("dd-MM-yyyy");
            newRow["MotivoRecisao"] = contrato.IdMotivoRecisao.Descricao;
            newRow["TipoDeContrato"] = contrato.IndTipoContrato.ToString();
            newRow["NotaFiscalEmNome"] = contrato.IdSacado.NomeAbreviado;
            newRow["Observacao"] = contrato.Observacao;
            newRow["EnderecoEntrega"] = contrato.IdSacado.GetEndereco(TipoEndereco.Entrega).GetEndereco();
            newRow["EmpresaCedente"] = contrato.IdCedente.NomeCedente;
            newRow["InstrucaoCobranca"] = contrato.IdCobranca.Descricao;
            newRow["AgruparServicos"] = contrato.AgruparServico ? "SIM - " + contrato.IdServico.Descricao : "NÃO";

            ds.Tables[0].Rows.Add(newRow);
            return ds;
        }


        private DataSet GetDataSourceServicos()
        {
            DataSet ds = new DataSet();
            ds.Tables.Add(GetDataTableServicos());
            DataRow newRow;
            ArrayList listContratoServico = new ContratoServico().Find("IdContrato=" + contrato.Id);
            foreach (ContratoServico contratoServico in listContratoServico)
            {
                contratoServico.IdCliente.Find();
                contratoServico.IdServico.Find();
                newRow = ds.Tables[0].NewRow();
                newRow["IdContrato"] = contrato.Id;
                newRow["NomeCliente"] = contratoServico.IdCliente.NomeAbreviado;
                newRow["NomeServico"] = contratoServico.IdServico.Descricao;
                newRow["Quantidade"] = contratoServico.QuantidadeUnidade();
                newRow["ValorUnitario"] = contratoServico.Valor;
                newRow["ValorTotal"] = contratoServico.ValorTotal();
                ds.Tables[0].Rows.Add(newRow);
            }
            return ds;
        }

        private DataSet GetDataSourceServicosPorPedido()
        {
            DataSet ds = new DataSet();
            ds.Tables.Add(GetDataTableServicosPorPedido());
            DataRow newRow;
            
            ArrayList listContratoServico = new ContratoServico().Find("IdContrato=" + contrato.Id);
            
            foreach (ContratoServico contratoServico in listContratoServico)
            {
                contratoServico.IdCliente.Find();
                contratoServico.IdServico.Find();
               
                ArrayList listContratoServicoObrigacao = new ContratoServicoObrigacao().Find("IdContratoServico=" + contratoServico.Id);
                
                foreach (ContratoServicoObrigacao contratoServicoObrigacao in listContratoServicoObrigacao)
                {
                    contratoServicoObrigacao.IdObrigacao.Find();

                    newRow = ds.Tables[0].NewRow();

                    newRow["IdContrato"] = contrato.Id;
                    newRow["NomeCliente"] = contratoServico.IdCliente.NomeAbreviado;
                    newRow["NomeServico"] = contratoServico.IdServico.Descricao;
                    newRow["NomeObrigacaoExame"] = contratoServicoObrigacao.IdObrigacao.NomeReduzido;

                    if (contratoServicoObrigacao.MesFaturamento == 0)
                        newRow["MesFaturamento"] = "-";
                    else
                        newRow["MesFaturamento"] = contratoServicoObrigacao.MesFaturamento.ToString("00");

                    newRow["Valor"] = contratoServicoObrigacao.ValorUnitario.ToString("n");

                    ds.Tables[0].Rows.Add(newRow);
                }
                ArrayList listContratoServicoExameDicionario = new ContratoServicoExameDicionario().Find("IdContratoServico=" + contratoServico.Id);

                foreach (ContratoServicoExameDicionario contratoServicoExameDicionario in listContratoServicoExameDicionario)
                {
                    contratoServicoExameDicionario.IdExameDicionario.Find();

                    newRow = ds.Tables[0].NewRow();

                    newRow["IdContrato"] = contrato.Id;
                    newRow["NomeCliente"] = contratoServico.IdCliente.NomeAbreviado;
                    newRow["NomeServico"] = contratoServico.IdServico.Descricao;
                    newRow["NomeObrigacaoExame"] = "Exame - " + contratoServicoExameDicionario.IdExameDicionario.Nome;
                    newRow["MesFaturamento"] = "-";
                    newRow["Valor"] = contratoServicoExameDicionario.ValorUnitario;

                    ds.Tables[0].Rows.Add(newRow);
                }
            }
            return ds;
        }

        private DataTable GetDataTable()
        {
            DataTable table = new DataTable("Result");
            table.Columns.Add("IdCliente", Type.GetType("System.Int32"));
            table.Columns.Add("IdContrato", Type.GetType("System.Int32"));
            table.Columns.Add("NomeCliente", Type.GetType("System.String"));
            table.Columns.Add("NomeContrato", Type.GetType("System.String"));
            table.Columns.Add("Inicio", Type.GetType("System.String"));
            table.Columns.Add("Termino", Type.GetType("System.String"));
            table.Columns.Add("Descricao", Type.GetType("System.String"));
            table.Columns.Add("DiaVencimento", Type.GetType("System.String"));
            table.Columns.Add("Carencia", Type.GetType("System.String"));
            table.Columns.Add("PrimeiroVencimento", Type.GetType("System.String"));
            table.Columns.Add("Renegociar", Type.GetType("System.String"));
            table.Columns.Add("RenegociarEm", Type.GetType("System.String"));
            table.Columns.Add("DataRecisao", Type.GetType("System.String"));
            table.Columns.Add("MotivoRecisao", Type.GetType("System.String"));
            table.Columns.Add("TipoDeContrato", Type.GetType("System.String"));
            table.Columns.Add("NotaFiscalEmNome", Type.GetType("System.String"));
            table.Columns.Add("Observacao", Type.GetType("System.String"));
            table.Columns.Add("EnderecoEntrega", Type.GetType("System.String"));
            table.Columns.Add("EmpresaCedente", Type.GetType("System.String"));
            table.Columns.Add("InstrucaoCobranca", Type.GetType("System.String"));
            table.Columns.Add("AgruparServicos", Type.GetType("System.String"));
            return table;
        }

        private DataTable GetDataTableServicos()
        {
            DataTable table = new DataTable("Result");
            table.Columns.Add("IdContrato", Type.GetType("System.Int32"));
            table.Columns.Add("NomeCliente", Type.GetType("System.String"));
            table.Columns.Add("NomeServico", Type.GetType("System.String"));
            table.Columns.Add("Quantidade", Type.GetType("System.Single"));
            table.Columns.Add("ValorUnitario", Type.GetType("System.Single"));
            table.Columns.Add("ValorTotal", Type.GetType("System.Single"));
            return table;
        }

        private DataTable GetDataTableServicosPorPedido()
        {
            DataTable table = new DataTable("Result");
            table.Columns.Add("IdContrato", Type.GetType("System.Int32"));
            table.Columns.Add("NomeCliente", Type.GetType("System.String"));
            table.Columns.Add("NomeServico", Type.GetType("System.String"));
            table.Columns.Add("NomeObrigacaoExame", Type.GetType("System.String"));
            table.Columns.Add("MesFaturamento", Type.GetType("System.String"));
            table.Columns.Add("Valor", Type.GetType("System.Single"));
            return table;
        }
    }
}
