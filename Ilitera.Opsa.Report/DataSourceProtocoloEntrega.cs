using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;

using Ilitera.Common;
using Ilitera.Opsa.Data;

namespace Ilitera.Opsa.Report
{
    public class DataSourceProtocoloEntrega: DataSourceBase
    {
        private SaidaDocumento saidaDocumento;

        private List<SaidaDocumento> saidas;

        public DataSourceProtocoloEntrega(SaidaDocumento saidaDocumento)
        {
            this.saidaDocumento = saidaDocumento;

            this.saidas = saidaDocumento.GetListDocumentosSaida();
        }

        public RptProtocoloEntrega GetReport()
        {
            if (saidas.Count == 0)
                throw new Exception("Não a obrigação selecionada");

            RptProtocoloEntrega report = new RptProtocoloEntrega();
            report.SetDataSource(GetDadosEmpresa());
            report.Refresh();
            return report;
        }

        public DataSet GetDadosEmpresa()
        {
            DataSet ds = new DataSet();

            DataTable table = GetTable();

            ds.Tables.Add(table);

            if (saidaDocumento.IdCliente.mirrorOld == null)
                saidaDocumento.IdCliente.Find();

            Endereco endereco = saidaDocumento.IdCliente.GetEndereco();

            ContatoTelefonico contatoTelefone = saidaDocumento.IdCliente.GetContatoTelefonico();

            PopularRow(ds, endereco, contatoTelefone);

            return ds;
        }

        private void PopularRow(DataSet ds, Endereco endereco, ContatoTelefonico contatoTelefone)
        {
            DataRow newRow;

            if (saidaDocumento.IsClinicoSaidaDocumento)
            {
                List<ExameBase> exames = saidaDocumento.GetExames();

                foreach (ExameBase exame in exames)
                {
                    newRow = ds.Tables[0].NewRow();

                    PopularRow(newRow, endereco, contatoTelefone);

                    newRow["NOME_OBRIGACAO"] = exame.GetDataEmpregadoTipoExame();

                    ds.Tables[0].Rows.Add(newRow);
                }

                InserirLinasEmBranco(exames.Count, ds, endereco, contatoTelefone);
            }
            else
            {
                foreach (SaidaDocumento saida in saidas)
                {
                    newRow = ds.Tables[0].NewRow();

                    PopularRow(newRow, endereco, contatoTelefone);

                    newRow["NOME_OBRIGACAO"] = saida.GetDescricaoDocumento();

                    ds.Tables[0].Rows.Add(newRow);
                }

                InserirLinasEmBranco(saidas.Count, ds, endereco, contatoTelefone);
            }
        }

        private void InserirLinasEmBranco(int TotalRegistros, DataSet ds, Endereco endereco, ContatoTelefonico contatoTelefone)
        {
            DataRow newRow;

            int numLinhas = 10;

            if (TotalRegistros < numLinhas)
            {
                for (int i = TotalRegistros; i < numLinhas; i++)
                {
                    newRow = ds.Tables[0].NewRow();

                    PopularRow(newRow, endereco, contatoTelefone);

                    ds.Tables[0].Rows.Add(newRow);
                }
            }
        }

        private void PopularRow(DataRow newRow, Endereco endereco, ContatoTelefonico contatoTelefone)
        {
            newRow["NOME_FANTASIA"] = saidaDocumento.IdCliente.NomeAbreviado;
            newRow["RAZAO_SOCIAL"] = saidaDocumento.IdCliente.NomeCompleto;
            newRow["ENDERECO"] = endereco.GetEndereco();
            newRow["BAIRRO"] = endereco.Bairro;
            newRow["CIDADE"] = endereco.Municipio;
            newRow["ESTADO"] = endereco.Uf;
            newRow["CEP"] = endereco.Cep;
            newRow["CONTATO"] = contatoTelefone.Nome;
            if (saidaDocumento.IdObrigacao.Id != 0)
                newRow["OBSERVACAO"] = saidaDocumento.Observacao;
            newRow["TELEFONE"] = contatoTelefone.Numero;
            newRow["MOTORISTA"] = saidaDocumento.IdCliente.IdTransporte.ToString();
            newRow["TRANSPORTE"] = saidaDocumento.GetTipoSaidaDocumento();
        }

        private static DataTable GetTable()
        {
            DataTable table = new DataTable("Result");

            table.Columns.Add("NOME_FANTASIA", Type.GetType("System.String"));
            table.Columns.Add("RAZAO_SOCIAL", Type.GetType("System.String"));
            table.Columns.Add("ENDERECO", Type.GetType("System.String"));
            table.Columns.Add("BAIRRO", Type.GetType("System.String"));
            table.Columns.Add("CIDADE", Type.GetType("System.String"));
            table.Columns.Add("ESTADO", Type.GetType("System.String"));
            table.Columns.Add("CEP", Type.GetType("System.String"));
            table.Columns.Add("CONTATO", Type.GetType("System.String"));
            table.Columns.Add("OBSERVACAO", Type.GetType("System.String"));
            table.Columns.Add("TELEFONE", Type.GetType("System.String"));
            table.Columns.Add("MOTORISTA", Type.GetType("System.String"));
            table.Columns.Add("TRANSPORTE", Type.GetType("System.String"));
            table.Columns.Add("NOME_OBRIGACAO", Type.GetType("System.String"));

            return table;
        }
    }
}
