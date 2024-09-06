using System;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Text;

using Ilitera.Data;
using Ilitera.Common;
using Ilitera.Opsa.Data;

namespace Ilitera.PCMSO.Report
{
    public class DataSourceProntuariosDigitalizados : DataSourceBase
	{
		private Cliente cliente;
		private Endereco endereco;

        public DataSourceProntuariosDigitalizados(Cliente cliente)
        {
            this.cliente = cliente;
            this.endereco = this.cliente.GetEndereco();
        }

		public RptProntuariosDigitalizados GetReport(DataSet dsProntuario)
		{
			RptProntuariosDigitalizados report = new RptProntuariosDigitalizados();
			report.OpenSubreport("DadosCliente").SetDataSource(GetDadosCliente());
			report.SetDataSource(GetProntuarioDigital(dsProntuario));
			report.Refresh();
            SetTempoProcessamento(report);
			return report;
		}
	
		public RptProntuariosDigitalizados GetReport()
		{
			RptProntuariosDigitalizados report = new RptProntuariosDigitalizados();
			report.OpenSubreport("DadosCliente").SetDataSource(GetDadosCliente());
			report.SetDataSource(GetProntuarioDigital());
			report.Refresh();
            SetTempoProcessamento(report);
			return report;
		}

        private DataSet GetProntuarioDigital()
        {
            DataSet ds = new DataSet();
            ds.Tables.Add(GetTable());

            DataRow row;

            StringBuilder str = new StringBuilder();
            str.Append("IdEmpregado IN (SELECT nID_EMPREGADO FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEMPREGADO WHERE nID_EMPR=" + cliente.Id + ")");

            str.Append(" ORDER BY DataProntuario DESC");

            ArrayList prontuarioDigital = new ProntuarioDigital().Find(str.ToString());

            foreach (ProntuarioDigital prontuario in prontuarioDigital)
            {
                string PathArquivo = prontuario.Arquivo;
                PathArquivo = PathArquivo.Replace("I:", "http://localhost/DocsDigitais");
                PathArquivo = PathArquivo.Replace(@"\", "/");
                row = ds.Tables[0].NewRow();
                row["NomeEmpregado"] = prontuario.IdEmpregado.ToString();
                row["DataDigitalizacao"] = prontuario.DataDigitalizacao.ToString("dd-MM-yyyy");
                row["DataProntuarioDigital"] = prontuario.DataProntuario.ToString("dd-MM-yyyy");
                row["Descricao"] = prontuario.GetDescricao();
                row["CliqueAqui"] = "Clique aqui para visualizar detalhes";
                row["PathArquivo"] = PathArquivo;
                ds.Tables[0].Rows.Add(row);
            }

            return ds;
        }

		private DataSet GetProntuarioDigital(DataSet dsProntuario)
		{	
			DataSet ds = new DataSet();
			ds.Tables.Add(GetTable());

			DataRow row;

			foreach (DataRow row2 in dsProntuario.Tables[0].Rows)
			{
				string PathArquivo = ProntuarioDigital.GetArquivo(cliente, Convert.ToString(row2["Arquivo"]));

                PathArquivo = PathArquivo.Replace(@"\\", @"/");
                PathArquivo = PathArquivo.Replace(@"I:", "http://" + EnvironmentUtitity.Domain + @"/DocsDigitais");
				PathArquivo = PathArquivo.Replace(@"\", "/");

                string NomeEmpregado = Convert.ToString(row2["Arquivo"]).Substring(0, Convert.ToString(row2["Arquivo"]).IndexOf("-")).Trim();

                row = ds.Tables[0].NewRow();

                row["NomeEmpregado"] = NomeEmpregado;
                row["DataDigitalizacao"] = Convert.ToDateTime(row2["DataDigitalizacao"]).ToString("dd-MM-yyyy");
                row["DataProntuarioDigital"] = Convert.ToDateTime(row2["DataProntuario"]).ToString("dd-MM-yyyy");
                row["Descricao"] = ProntuarioDigital.GetDescricao(Convert.ToInt32(row2["IndTipoDocumento"]), Convert.ToString(row2["Descricao"]));
                row["CliqueAqui"] = "Clique aqui para visualizar detalhes"; ;
                row["PathArquivo"] = PathArquivo;

                ds.Tables[0].Rows.Add(row);
			}
			
			return ds;
		}

        private static DataTable GetTable()
        {
            DataTable table = new DataTable("Result");
            table.Columns.Add("NomeEmpregado", Type.GetType("System.String"));
            table.Columns.Add("DataDigitalizacao", Type.GetType("System.String"));
            table.Columns.Add("DataProntuarioDigital", Type.GetType("System.String"));
            table.Columns.Add("Descricao", Type.GetType("System.String"));
            table.Columns.Add("CliqueAqui", Type.GetType("System.String"));
            table.Columns.Add("PathArquivo", Type.GetType("System.String"));
            return table;
        }

        private DataSet GetDadosCliente()
        {
            DataTable table = new DataTable("Result");
            table.Columns.Add("NomeCliente", Type.GetType("System.String"));
            table.Columns.Add("LogradouroNumero", Type.GetType("System.String"));
            table.Columns.Add("BairroCidadeEstado", Type.GetType("System.String"));
            table.Columns.Add("CEP", Type.GetType("System.String"));
            table.Columns.Add("CNPJ", Type.GetType("System.String"));

            DataSet ds = new DataSet();
            ds.Tables.Add(table);

            DataRow newRow;

            newRow = ds.Tables[0].NewRow();

            newRow["NomeCliente"] = cliente.NomeCompleto;
            newRow["LogradouroNumero"] = endereco.GetEndereco();
            newRow["BairroCidadeEstado"] = endereco.Bairro + " - " + endereco.GetCidade() + " - " + endereco.GetEstado();
            newRow["CEP"] = "CEP " + endereco.Cep;
            newRow["CNPJ"] = "CNPJ " + cliente.NomeCodigo;

            ds.Tables[0].Rows.Add(newRow);

            return ds;
        }
	}
}
