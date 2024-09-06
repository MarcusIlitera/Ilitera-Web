using System;
using System.Data;
using System.Collections;
using Ilitera.Opsa.Data;
using Ilitera.Common;

namespace Ilitera.PCMSO.Report
{
    public class DataSourceOrdemDePagamentoWeb : DataSourceBase
	{
		public DataSourceOrdemDePagamentoWeb()
		{	
			
		}

		public RptOrdemDePagamentoWEB GetReport(int Mes, int Ano, bool isPago, int IdClinica)
		{
			RptOrdemDePagamentoWEB report = new RptOrdemDePagamentoWEB();
			report.OpenSubreport("DescricaoExames").SetDataSource(GetExames(Mes, Ano, isPago, IdClinica));
			report.SetDataSource(GetHeader(IdClinica));
			report.Refresh();

            SetTempoProcessamento(report);

			return report;
		}

		private DataTable GetTableHeader()
		{
			DataTable table = new DataTable("Result");	
			table.Columns.Add("NomeCliente", Type.GetType("System.String"));			
			table.Columns.Add("Endereco", Type.GetType("System.String"));
			table.Columns.Add("Bairro", Type.GetType("System.String"));
			table.Columns.Add("CEP", Type.GetType("System.String"));
			table.Columns.Add("Cidade", Type.GetType("System.String"));
			table.Columns.Add("UF", Type.GetType("System.String"));
			table.Columns.Add("CNPJ", Type.GetType("System.String"));
			table.Columns.Add("Banco", Type.GetType("System.String"));
			table.Columns.Add("Agencia", Type.GetType("System.String"));
			table.Columns.Add("ContaCorrente", Type.GetType("System.String"));
			table.Columns.Add("DiaDeposito", Type.GetType("System.String"));
			table.Columns.Add("Responsavel", Type.GetType("System.String"));
			table.Columns.Add("Titular", Type.GetType("System.String"));
			return table;
		}

		private DataTable GetTable()
		{
			DataTable table = new DataTable("Result");	
			table.Columns.Add("DataExame", Type.GetType("System.String"));
			table.Columns.Add("DataPagamento", Type.GetType("System.String"));		
			table.Columns.Add("NomeEmpregado", Type.GetType("System.String"));		
			table.Columns.Add("NomeEmpresa", Type.GetType("System.String"));		
			table.Columns.Add("NomeExame", Type.GetType("System.String"));
			table.Columns.Add("ValorExame", Type.GetType("System.Single"));
			table.Columns.Add("Titulo", Type.GetType("System.String"));
			return table;
		}

		private DataSet GetHeader(int IdClinica)
		{
			DataSet ds = new DataSet();
			ds.Tables.Add(GetTableHeader());
			DataRow newRow;

			Clinica clinica = new Clinica();
			clinica.Find(IdClinica);
			Endereco endereco = new Endereco();
			endereco.Find("IdPessoa="+clinica.Id);

			newRow = ds.Tables[0].NewRow();
			newRow["NomeCliente"] = clinica.NomeCompleto;		
			newRow["Endereco"] = endereco.GetEndereco();
			newRow["Bairro"] = endereco.Bairro;
			newRow["CEP"] = endereco.Cep;
			newRow["Cidade"] = endereco.GetCidade();
			newRow["UF"] = endereco.GetEstado();
			newRow["CNPJ"] = clinica.NomeCodigo;
			ds.Tables[0].Rows.Add(newRow);

            return ds;
		}
        
		private DataSet GetExames(int mes, int ano,bool IsExamesPagos, int IdClinica)
		{
			DataSet dsDescricaoExames = new DataSet();
			dsDescricaoExames.Tables.Add(GetTable());
			DataRow newRow;
			string stringMes = string.Empty;

			ArrayList list = new ArrayList();
			if(IsExamesPagos)
				list = new ExameBase().Find("MONTH(DataExame)='"+mes+"' AND YEAR(DataExame)='"+ano+"' AND IndResultado<>0 AND DataPagamento IS NOT Null AND IdJuridica = "+IdClinica+" ORDER BY DataExame");
			else
				list = new ExameBase().Find("MONTH(DataExame)='"+mes+"' AND YEAR(DataExame)='"+ano+"' AND IndResultado<>0 AND DataPagamento IS Null AND IdJuridica = "+IdClinica+" ORDER BY DataExame");

			switch (mes)
			{
				case 1:
					stringMes = "Janeiro";
					break;
				case 2:
					stringMes = "Fevereiro";
					break;
				case 3:
					stringMes = "Março";
					break;
				case 4:
					stringMes = "Abril";
					break;
				case 5:
					stringMes = "Maio";
					break;
				case 6:
					stringMes = "Junho";
					break;
				case 7:
					stringMes = "Julho";
					break;
				case 8:
					stringMes = "Agosto";
					break;
				case 9:
					stringMes = "Setembro";
					break;
				case 10:
					stringMes = "Outubro";
					break;
				case 11:
					stringMes = "Novembro";
					break;
				case 12:
					stringMes = "Dezembro";
					break;
			}
			
			if (IsExamesPagos)
				foreach(ExameBase exameBase in list)
				{				
					newRow = dsDescricaoExames.Tables[0].NewRow();					
					newRow["DataExame"] = exameBase.DataExame.ToString("dd-MM-yyyy");
					newRow["DataPagamento"] = exameBase.DataPagamento.ToString("dd-MM-yyyy");
					exameBase.IdEmpregado.Find();
					newRow["NomeEmpregado"] = exameBase.IdEmpregado.tNO_EMPG;
					exameBase.IdEmpregado.nID_EMPR.Find();
					newRow["NomeEmpresa"] = exameBase.IdEmpregado.nID_EMPR.NomeAbreviado;
					exameBase.IdExameDicionario.Find();
					newRow["NomeExame"] = exameBase.IdExameDicionario.Nome;
					if (exameBase.ValorPago == 0)
					{
						ClinicaExameDicionario cliExaDic = new ClinicaExameDicionario();
						cliExaDic.Find("IdClinica=" + IdClinica + " AND IdExameDicionario=" + exameBase.IdExameDicionario.Id);
						newRow["ValorExame"] = cliExaDic.ValorPadrao.ToString("N");
					}
					else
						newRow["ValorExame"] = exameBase.ValorPago.ToString("N");
					newRow["Titulo"] = "Exames Realizados e Pagos para o Mês de " + stringMes;
				
					dsDescricaoExames.Tables[0].Rows.Add(newRow);
				}
			else
				foreach(ExameBase exameBase in list)
				{				
					exameBase.IdEmpregado.Find();
					ClinicaCliente clinicaCliente = new ClinicaCliente();
					clinicaCliente.Find("IdClinica=" + IdClinica + " AND IdCliente=" + exameBase.IdEmpregado.nID_EMPR.Id);
					exameBase.IdExameDicionario.Find();
					newRow = dsDescricaoExames.Tables[0].NewRow();					
					newRow["DataExame"] = exameBase.DataExame.ToString("dd-MM-yyyy");
					newRow["DataPagamento"] = "-----";
					exameBase.IdEmpregado.Find();
					newRow["NomeEmpregado"] = exameBase.IdEmpregado.tNO_EMPG;
					exameBase.IdEmpregado.nID_EMPR.Find();
					newRow["NomeEmpresa"] = exameBase.IdEmpregado.nID_EMPR.NomeAbreviado;
					newRow["NomeExame"] = exameBase.IdExameDicionario.Nome;
					if (exameBase.ValorPago == 0)
					{
						ClinicaExameDicionario cliExaDic = new ClinicaExameDicionario();
						cliExaDic.Find("IdClinica=" + IdClinica + " AND IdExameDicionario=" + exameBase.IdExameDicionario.Id);
						newRow["ValorExame"] = cliExaDic.ValorPadrao.ToString("N");
					}
					else
						newRow["ValorExame"] = exameBase.ValorPago.ToString("N");
					newRow["Titulo"] = "Exames Realizados e Não Pagos para o Mês de " + stringMes;

					dsDescricaoExames.Tables[0].Rows.Add(newRow);
				}
	
			return dsDescricaoExames;
		}
	}
}
