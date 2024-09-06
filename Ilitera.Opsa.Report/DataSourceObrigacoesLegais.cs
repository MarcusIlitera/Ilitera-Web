using System;
using System.Text;
using System.Data;
using System.Collections;
using Ilitera.Common;
using Ilitera.Opsa.Data;

namespace Ilitera.Opsa.Report
{
	public class DataSourceObrigacoesLegais
	{
		private Obrigacao obrigacao;
		private Prestador prestador;
        private Cliente cliente;
        private GrupoEmpresa grupoEmpresa;
        private bool PorPrestador = false;
		private int iOrdenar;

        public DataSourceObrigacoesLegais(Obrigacao obrigacao, GrupoEmpresa grupoEmpresa, int iOrdenar)
        {
            this.grupoEmpresa = grupoEmpresa;
            this.obrigacao = obrigacao;
            this.iOrdenar = iOrdenar;
        }

        public DataSourceObrigacoesLegais(Obrigacao obrigacao, Cliente cliente, int iOrdenar)
        {
            this.cliente = cliente;
            this.obrigacao = obrigacao;
            this.iOrdenar = iOrdenar;
        }

		public DataSourceObrigacoesLegais(Obrigacao obrigacao, int iOrdenar)
		{
			this.obrigacao = obrigacao;
			this.iOrdenar = iOrdenar;
		}

		public enum Ordenar: int
		{
			NomeCliente,
			CEP,
			Eleicao
		}

		public RptObrigacoesLegais GetReport()
		{
			DataRow[] dataRows;

			DataSet dataSouce = GetDataSource();
			
            if(iOrdenar==(int)Ordenar.Eleicao)
			{
				dataRows = dataSouce.Tables[0].Select(string.Empty, "Eleicao");
				dataSouce = new DataSet();
				dataSouce.Merge(dataRows);
			}
			
            RptObrigacoesLegais report = new RptObrigacoesLegais();	
			report.SetDataSource(dataSouce);
			report.Refresh();
			
            return report;
		}

		public RptObrigacoesLegaisComCaracterizacao GetReportComCaracteriazacao()
		{
			DataRow[] dataRows;
			DataSet dataSouce = GetDataSource();
			
			if(iOrdenar==(int)Ordenar.Eleicao)
			{
				dataRows = dataSouce.Tables[0].Select(string.Empty, "Eleicao");
				dataSouce = new DataSet();
				dataSouce.Merge(dataRows);
			}
			
			RptObrigacoesLegaisComCaracterizacao report = new RptObrigacoesLegaisComCaracterizacao();	
			report.SetDataSource(dataSouce);
			report.Refresh();
			
			return report;
		}

		public RptObrigacoesLegaisPorPrestador GetReportPorPrestador(Prestador prestador)
		{
			this.prestador = prestador;
            this.PorPrestador = true;

			RptObrigacoesLegaisPorPrestador report = new RptObrigacoesLegaisPorPrestador();	
			report.SetDataSource(GetDataSource());
			report.Refresh();
			return report;
		}

		public RptObrigacoesLegaisPorPrestador GetReportPorPrestador()
		{
			RptObrigacoesLegaisPorPrestador report = new RptObrigacoesLegaisPorPrestador();	
			report.SetDataSource(GetDataSource());
            report.SummaryInfo.ReportTitle = "Todos Prestadores";
			report.Refresh();
			return report;
		}

		private DataSet GetDataSource()
		{
            DataTable table = GetTable();

			DataSet ds = new DataSet();
			ds.Tables.Add(table);

			StringBuilder str = new StringBuilder();
            str.Append("USE " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + " SELECT IdCliente, IdJuridicaPai,  Fantasia, JuridicaPai,"
                        + " Cep, QtdEmpregados, PosseCipa, Documento, TipoLogradouro,"
                        + " Logradouro, Numero, Complemento, Bairro, Cidade, Uf,"
                        + " ContrataCIPA, HasCIPA, DataSolicitacao, DataUltimo,"
                        + " DataVencimento, DataProximo, IdObrigacao, AtivarLocalDeTrabalho, RespEleicaoCipa "
			            + " FROM qryObrigacoesLegais");

            str.Append(" WHERE IdObrigacao=" + obrigacao.Id);
			
			if(PorPrestador)
			{
				if(prestador==null || prestador.Id==0)
					str.Append(" AND IdCliente IN (SELECT IdCliente FROM ObrigacaoCliente WHERE IdPrestador IS NOT NULL)");
				else
					str.Append(" AND IdCliente IN (SELECT IdCliente FROM ObrigacaoCliente WHERE IdPrestador IS NOT NULL AND IdPrestador="+prestador.Id+")");
			}

            if(grupoEmpresa != null && grupoEmpresa.Id!=0)
                str.Append(" AND IdGrupoEmpresa=" + grupoEmpresa.Id);

            if (cliente != null && cliente.Id != 0)
                str.Append(" AND IdCliente=" + cliente.Id);

            if(iOrdenar==(int)DataSourceObrigacoesLegais.Ordenar.NomeCliente)
				str.Append(" ORDER BY Fantasia");
			else if(iOrdenar==(int)DataSourceObrigacoesLegais.Ordenar.CEP)
				str.Append(" ORDER BY Cep");

			DataSet dsCliente = new Cliente().ExecuteDataset(str.ToString());	
			
            DataRow newRow;

            foreach (DataRow row in dsCliente.Tables[0].Rows)
            {
                newRow = ds.Tables[0].NewRow();

                bool ComUnidade = row["AtivarLocalDeTrabalho"]!=System.DBNull.Value && Convert.ToBoolean(row["AtivarLocalDeTrabalho"]);

                if (row["IdJuridicaPai"] != System.DBNull.Value && Convert.ToInt32(row["IdJuridicaPai"]) != 0)
                {
                    newRow["CLIENTE"] = row["JuridicaPai"];
                    newRow["UNIDADE"] = row["Fantasia"];
                    newRow["IS_UNIDADE"] = true;
                }
                else
                {
                    newRow["CLIENTE"] = row["Fantasia"];
                    
                    if(ComUnidade)
                        newRow["UNIDADE"] = row["Fantasia"];

                    newRow["IS_UNIDADE"] = false;
                }
                newRow["COM_UNIDADE"] = ComUnidade;
                newRow["CEP"] = row["Cep"];
                //newRow["VALOR_SM"] = cl.GetSalarioMinimo();
                //newRow["VALOR_PCMSO"] = cl.GetValorPorEmpregadoPCMSO();
                newRow["QTD_EMPREGADOS"] = row["QtdEmpregados"];
                newRow["DOCUMENTO"] = row["Documento"];

                if (prestador != null && prestador.Id != 0)
                    newRow["PRESTADOR"] = prestador.NomeAbreviado;
                else
                    newRow["PRESTADOR"] = "Não Possui Prestador Configurado";

                newRow["ENDERECO"] = Endereco.GetEndereco(Convert.ToString(row["TipoLogradouro"]),
                                                            Convert.ToString(row["Logradouro"]),
                                                            Convert.ToString(row["Numero"]),
                                                            Convert.ToString(row["Complemento"]));
                newRow["BAIRRO"] = row["Bairro"];
                newRow["CIDADE"] = row["Cidade"];
                newRow["UF"] = row["Uf"];

                //if(cipa.Id!=0 && cipa.Eleicao!=new DateTime())
                //    newRow["ELEICAO"] = cipa.Eleicao;

                newRow["ELEICAO_RESPONSAVEL"] = Convert.ToString(row["RespEleicaoCipa"]);
                newRow["OBRIGACAO"] = obrigacao.NomeReduzido;
                newRow["PPRA_ULTIMO"] = (row["DataUltimo"] != System.DBNull.Value) ? Convert.ToDateTime(row["DataUltimo"]).ToString("dd-MM-yyyy") : "-";
                newRow["PPRA_VENCIMENTO"] = row["DataVencimento"];
                newRow["PPRA_PEDIDO"] = (row["DataSolicitacao"] != System.DBNull.Value) ? Convert.ToDateTime(row["DataSolicitacao"]).ToString("dd-MM-yyyy") : "-";
                newRow["TEM_CIPA"] = Convert.ToBoolean(row["HasCipa"]) ? "X" : "";
                newRow["CIPA_CONTRATADA"] = Convert.ToBoolean(row["ContrataCipa"]) ? "X" : "";

                ds.Tables[0].Rows.Add(newRow);
            }
			return ds;
		}

        private static DataTable GetTable()
        {
            DataTable table = new DataTable("Result");
            table.Columns.Add("CLIENTE", Type.GetType("System.String"));
            table.Columns.Add("UNIDADE", Type.GetType("System.String"));
            table.Columns.Add("IS_UNIDADE", Type.GetType("System.Boolean"));
            table.Columns.Add("COM_UNIDADE", Type.GetType("System.Boolean"));
            table.Columns.Add("CEP", Type.GetType("System.String"));
            table.Columns.Add("VALOR_SM", Type.GetType("System.String"));
            table.Columns.Add("VALOR_PCMSO", Type.GetType("System.Single"));
            table.Columns.Add("VALOR_MOEDA", Type.GetType("System.Single"));
            table.Columns.Add("QTD_EMPREGADOS", Type.GetType("System.Int32"));
            table.Columns.Add("ELEICAO", Type.GetType("System.DateTime"));
            table.Columns.Add("ELEICAO_RESPONSAVEL", Type.GetType("System.String"));
            table.Columns.Add("OBRIGACAO", Type.GetType("System.String"));
            table.Columns.Add("PPRA_ULTIMO", Type.GetType("System.String"));
            table.Columns.Add("PPRA_VENCIMENTO", Type.GetType("System.DateTime"));
            table.Columns.Add("PPRA_PEDIDO", Type.GetType("System.String"));
            table.Columns.Add("DOCUMENTO", Type.GetType("System.String"));
            table.Columns.Add("ENDERECO", Type.GetType("System.String"));
            table.Columns.Add("PRESTADOR", Type.GetType("System.String"));
            table.Columns.Add("BAIRRO", Type.GetType("System.String"));
            table.Columns.Add("CIDADE", Type.GetType("System.String"));
            table.Columns.Add("UF", Type.GetType("System.String"));
            table.Columns.Add("QUADRADO", Type.GetType("System.String"));
            table.Columns.Add("TEM_CIPA", Type.GetType("System.String"));
            table.Columns.Add("CIPA_CONTRATADA", Type.GetType("System.String"));
            return table;
        }	
	}
}
