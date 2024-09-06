using System;
using System.Data;
using System.Collections;
using Ilitera.Opsa.Data;
using Ilitera.Common;

namespace Ilitera.PCMSO.Report
{
    public class DataSourceOrdemDePagamento : DataSourceBase
	{
		private PagamentoClinica pagamentoClinica;

		public DataSourceOrdemDePagamento(PagamentoClinica pagamentoClinica)
		{	
			this.pagamentoClinica = pagamentoClinica;

			this.pagamentoClinica .IdClinica.Find();
		}

		public RptOrdemDePagamento GetReport()
		{
			RptOrdemDePagamento report = new RptOrdemDePagamento();
            report.OpenSubreport("Header").SetDataSource(GetHeader(this.pagamentoClinica .IdClinica));
			report.SetDataSource(GetExamesPagos());
			report.Refresh();
            SetTempoProcessamento(report);
            return report;
		}

		private DataTable GetTableHeader()
		{
			DataTable tableHeader = new DataTable("Result");	
			tableHeader.Columns.Add("NomeCliente", Type.GetType("System.String"));			
			tableHeader.Columns.Add("Endereco", Type.GetType("System.String"));
			tableHeader.Columns.Add("Bairro", Type.GetType("System.String"));
			tableHeader.Columns.Add("CEP", Type.GetType("System.String"));
			tableHeader.Columns.Add("Cidade", Type.GetType("System.String"));
			tableHeader.Columns.Add("UF", Type.GetType("System.String"));
			tableHeader.Columns.Add("CNPJ", Type.GetType("System.String"));
			tableHeader.Columns.Add("Banco", Type.GetType("System.String"));
			tableHeader.Columns.Add("Agencia", Type.GetType("System.String"));
			tableHeader.Columns.Add("ContaCorrente", Type.GetType("System.String"));
			tableHeader.Columns.Add("DiaDeposito", Type.GetType("System.String"));
			tableHeader.Columns.Add("Responsavel", Type.GetType("System.String"));
			tableHeader.Columns.Add("Titular", Type.GetType("System.String"));
			return tableHeader;
		}

		private DataTable GetTable()
		{
			DataTable table = new DataTable("Result");	
			table.Columns.Add("DataExame", Type.GetType("System.String"));
			table.Columns.Add("DataPagamento", Type.GetType("System.String"));		
			table.Columns.Add("NomeEmpregado", Type.GetType("System.String"));		
			table.Columns.Add("NomeEmpresa", Type.GetType("System.String"));		
			table.Columns.Add("NomeExame", Type.GetType("System.String"));
			table.Columns.Add("ValorOutros", Type.GetType("System.Single"));
			table.Columns.Add("ValorImposto", Type.GetType("System.Single"));
			table.Columns.Add("ValorExame", Type.GetType("System.Single"));
			table.Columns.Add("DescricaoOutros", Type.GetType("System.String"));
			table.Columns.Add("Titulo", Type.GetType("System.String"));
			return table;
		}

		public DataSet GetHeader(Clinica clinica)
		{			
			DataSet ds = new DataSet();
			ds.Tables.Add(GetTableHeader());			

			DataRow newRowHeader = ds.Tables[0].NewRow();
			
			Endereco endereco = clinica.GetEndereco();

			newRowHeader["NomeCliente"]		= clinica.NomeCompleto;		
			newRowHeader["Endereco"]		= endereco.GetEndereco();
			newRowHeader["Bairro"]			= endereco.Bairro;
			newRowHeader["CEP"]				= endereco.Cep;
			newRowHeader["Cidade"]			= endereco.GetCidade();
			newRowHeader["UF"]				= endereco.GetEstado();
			newRowHeader["CNPJ"]			= clinica.NomeCodigo;		

			ArrayList listCC = new ContaCorrente().Find("IdPessoa="+clinica.Id);
			
			if(listCC.Count>0)
			{
				((ContaCorrente)listCC[0]).IdBanco.Find();
                
                newRowHeader["Banco"] = ((ContaCorrente)listCC[0]).IdBanco.Nome;
                newRowHeader["Agencia"] = ((ContaCorrente)listCC[0]).NumeroAgencia;
                newRowHeader["ContaCorrente"] = ((ContaCorrente)listCC[0]).Numero;
                newRowHeader["DiaDeposito"] = clinica.ObservacaoDeposito;
                newRowHeader["Responsavel"] = ((ContaCorrente)listCC[0]).Responsavel;
                newRowHeader["Titular"] = ((ContaCorrente)listCC[0]).Titular;
			}
			ds.Tables[0].Rows.Add(newRowHeader);

			return ds;
		}

		public DataSet GetExamesPagos()
		{			
			DataRow newRow;	
			DataSet ds = new DataSet();
			ds.Tables.Add(GetTable());

            ArrayList listExames = new ExameBase().Find("IdPagamentoClinica IS NOT NULL "
                + " AND IdPagamentoClinica = " + pagamentoClinica.Id);

			foreach(ExameBase exameBase in listExames)
			{	
				exameBase.IdEmpregado.Find();
				exameBase.IdExameDicionario.Find();
				exameBase.IdEmpregado.nID_EMPR.Find();
				
				newRow = ds.Tables[0].NewRow();

                newRow["DataExame"] = exameBase.DataExame.ToString("dd-MM-yyyy");
                newRow["DataPagamento"] = pagamentoClinica.DataPagamento.ToString("dd-MM-yyyy");
                newRow["NomeEmpregado"] = exameBase.IdEmpregado.tNO_EMPG;
                newRow["NomeEmpresa"] = exameBase.IdEmpregado.nID_EMPR.NomeAbreviado;
                newRow["NomeExame"] = exameBase.IdExameDicionario.Nome;
                newRow["ValorOutros"] = pagamentoClinica.ValorOutros;
                newRow["ValorImposto"] = pagamentoClinica.ValorImposto;
                newRow["DescricaoOutros"] = pagamentoClinica.DescricaoOutros;
                newRow["ValorExame"] = exameBase.ValorPago;

                ds.Tables[0].Rows.Add(newRow);
			}				
			return ds;
		}
	}
}
