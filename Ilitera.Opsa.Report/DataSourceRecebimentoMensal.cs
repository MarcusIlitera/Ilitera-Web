using System;
using System.Collections;
using System.Data;
using System.Drawing;
using Ilitera.Data;
using Ilitera.Opsa.Data;
using Ilitera.Common;
using Ilitera.Marshall.Data;

namespace Ilitera.Opsa.Report
{
	public class DataSourceRecebimentoMensal
	{

		private ArrayList listFaturamentos;
		private int Mes;
		private int Ano;

		public DataSourceRecebimentoMensal(int Mes, int Ano)
		{
			this.Mes = Mes;
			this.Ano = Ano;
		}

		public RptRecebimentoMensal GetReport()
		{
			RptRecebimentoMensal report = new RptRecebimentoMensal();	
			report.SetDataSource(GetDataSource());
			report.Refresh();			
			return report;
		}

		private DataSet GetDataSource()
		{
			DataSet ds = new DataSet();
			ds.Tables.Add(this.GetDataTableControleFinanceiroMensal());
			DataRow newRow;		
			int i = 1;
			float totalRecebido = 0.0F;

			DataSet dsTotal = new ReceberPagar().ExecuteDataset("SELECT SUM(ValorPago) AS TotalRecebido "
												+ " FROM         marshall.dbo.ReceberPagar"
												+ " WHERE     (DataPagamento IS NOT NULL)"
												+ " AND (MONTH(DataPagamento) = "+Mes+")"
												+ " AND (YEAR(DataPagamento) = "+Ano+")");

			totalRecebido = Convert.ToSingle(dsTotal.Tables[0].Rows[0][0]);

			listFaturamentos  = new Faturamento().Find("IdFaturamento IN (SELECT CodigoLancamento FROM marshall.dbo.ReceberPagar WHERE DataPagamento IS NOT NULL AND Estorno IS NULL AND DC=0 AND Month(DataPagamento)="+Mes
				+" AND Year(DataPagamento)="+Ano+")"
				+ " ORDER BY (SELECT NomeAbreviado FROM Pessoa WHERE IdPessoa = IdSacado)");

			foreach(Faturamento faturamento in listFaturamentos)
			{
				Cliente cliente = new Cliente();
				cliente.Find(faturamento.IdSacado.Id);
				faturamento.IdCedente.Find();
				faturamento.IdContrato.Find();
				newRow = ds.Tables[0].NewRow();
				newRow["Titulo"]		= "Recebidos em "+Mes.ToString("00")+"/"+Ano;
				newRow["MicroEmpresa"]	= faturamento.IdCedente.NomeCedente;
				newRow["NomeEmpresa"]	= cliente.NomeAbreviado;
				newRow["Inicio"]		= faturamento.IdContrato.Inicio.ToString("dd-MM-yyyy");
				newRow["Documento"]		= faturamento.Numero;
				newRow["Vencimento"]	= faturamento.Vencimento.ToString("dd-MM-yyyy");
				newRow["SM"]			= faturamento.ValorSalarioMinimo().ToString();
				newRow["Seguranca"]		= faturamento.ValorSegurancaTrabalho();
				newRow["PCMSO"]			= faturamento.ValorPCMSO();
				newRow["Juridico"]		= faturamento.ValorJuridico();
				newRow["Total"]			= faturamento.ValorTotal.ToString("n");
				newRow["IRF"]			= faturamento.ValorTotalImpostos();
				newRow["TotalReceber"]	= faturamento.ValorTotal - faturamento.ValorTotalImpostos();
				newRow["TotalRecebido"]	= totalRecebido;
				ReceberPagar receber = new ReceberPagar();
				receber.Find("CodigoLancamento="+faturamento.Id);

				newRow["Pagamento"]		= receber.DataPagamento;
				newRow["ValorPago"]		= receber.ValorPago;

				ContatoTelefonico contatoTelefonico;
				ArrayList list = new Contato().Find("IdJuridica="+cliente.Id);
				
				if(list.Count!=0)
				{
					foreach(Contato contato in list)
					{
						contato.IdPessoa.Find();
						if(contato.IdPessoa.Email!=string.Empty)
						{
							contatoTelefonico		= contato.IdPessoa.GetContatoTelefonico();
							newRow["Contato"]		= contato.IdPessoa.NomeAbreviado;
							newRow["Telefone"]		= contatoTelefonico.Numero;
							newRow["Email"]			= contato.IdPessoa.Email;	
							newRow["Atualizado"]	= true;
						}
						else
						{
							contatoTelefonico		= cliente.GetContatoTelefonico();
							newRow["Contato"]		= contatoTelefonico.Nome;
							newRow["Telefone"]		= contatoTelefonico.Numero;
							newRow["Email"]			= cliente.Email;	
							newRow["Atualizado"]	= false;
						}
						break;
					}
				}
				else
				{
					contatoTelefonico		= cliente.GetContatoTelefonico();
					newRow["Contato"]		= contatoTelefonico.Nome;
					newRow["Telefone"]		= contatoTelefonico.Numero;
					newRow["Email"]			= cliente.Email;	
					newRow["Atualizado"]	= false;
				}

				i++;
				ds.Tables[0].Rows.Add(newRow);
			}
			return ds;
		}		


		private DataTable GetDataTableControleFinanceiroMensal()
		{
			DataSet ds = new DataSet();
			DataTable table = new DataTable("Result");
			table.Columns.Add("Titulo", Type.GetType("System.String"));
			table.Columns.Add("MicroEmpresa", Type.GetType("System.String"));
			table.Columns.Add("NomeEmpresa", Type.GetType("System.String"));
			table.Columns.Add("Inicio", Type.GetType("System.String"));
			table.Columns.Add("Documento", Type.GetType("System.String"));
			table.Columns.Add("SM", Type.GetType("System.String"));
			table.Columns.Add("Seguranca", Type.GetType("System.Single"));
			table.Columns.Add("PCMSO", Type.GetType("System.Single"));
			table.Columns.Add("Juridico", Type.GetType("System.Single"));
			table.Columns.Add("Total", Type.GetType("System.Single"));
			table.Columns.Add("IRF", Type.GetType("System.Single"));
			table.Columns.Add("TotalReceber", Type.GetType("System.Single"));
			table.Columns.Add("Vencimento", Type.GetType("System.String"));
			table.Columns.Add("Pagamento", Type.GetType("System.DateTime"));
			table.Columns.Add("ValorPago", Type.GetType("System.Single"));
			table.Columns.Add("TotalRecebido", Type.GetType("System.Single"));
			table.Columns.Add("Contato", Type.GetType("System.String"));
			table.Columns.Add("Telefone", Type.GetType("System.String"));
			table.Columns.Add("Email", Type.GetType("System.String"));
			table.Columns.Add("Atualizado", Type.GetType("System.Boolean"));
			return table;
		}
	}
}
