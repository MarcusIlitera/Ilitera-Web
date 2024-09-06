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
	public class DataSourceControleFinanceiro
	{
		List<Faturamento> listFaturamentos;

		public DataSourceControleFinanceiro(int Mes, int Ano)
		{
            string criteria = "Referencia='" 
                            + Mes.ToString("00") 
                            + "/" 
                            + Ano.ToString("0000") 
                            + "'"
					        + " ORDER BY (SELECT NomeAbreviado FROM Pessoa WHERE IdPessoa = IdSacado)";

            listFaturamentos = new Faturamento().Find<Faturamento>(criteria);
		}

        public DataSourceControleFinanceiro(List<Faturamento> listFaturamentos)
		{
			this.listFaturamentos = listFaturamentos;
		}

		public RptControleFinanceiroMensal GetReport()
		{
			RptControleFinanceiroMensal report = new RptControleFinanceiroMensal();	
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
            foreach (Faturamento faturamento in listFaturamentos)
            {
                Cliente cliente = new Cliente();
                cliente.Find(faturamento.IdSacado.Id);
                faturamento.IdCedente.Find();
                faturamento.IdContrato.Find();
                newRow = ds.Tables[0].NewRow();
                newRow["MicroEmpresa"] = faturamento.IdCedente.NomeCedente;
                newRow["NomeEmpresa"] = cliente.NomeAbreviado;
                newRow["Inicio"] = faturamento.IdContrato.Inicio.ToString("dd-MM-yyyy");
                newRow["Documento"] = faturamento.Numero;
                newRow["Vencimento"] = faturamento.Vencimento.ToString("dd-MM-yyyy");
                newRow["SM"] = faturamento.ValorSalarioMinimo().ToString();
                newRow["Seguranca"] = faturamento.ValorSegurancaTrabalho();
                newRow["PCMSO"] = faturamento.ValorPCMSO();
                newRow["Juridico"] = faturamento.ValorJuridico();
                newRow["Total"] = faturamento.ValorTotal.ToString("n");
                newRow["IRF"] = faturamento.ValorTotalImpostos();
                newRow["TotalReceber"] = faturamento.ValorTotal - faturamento.ValorTotalImpostos();

                ContatoTelefonico contatoTelefonico;
                ArrayList list = new Contato().Find("IdJuridica=" + cliente.Id);

                if (list.Count != 0)
                {
                    foreach (Contato contato in list)
                    {
                        contato.IdPessoa.Find();
                        if (contato.IdPessoa.Email != string.Empty)
                        {
                            contatoTelefonico = contato.IdPessoa.GetContatoTelefonico();
                            newRow["Contato"] = contato.IdPessoa.NomeAbreviado;
                            newRow["Telefone"] = contatoTelefonico.Numero;
                            newRow["Email"] = contato.IdPessoa.Email;
                            newRow["Atualizado"] = true;
                        }
                        else
                        {
                            contatoTelefonico = cliente.GetContatoTelefonico();
                            newRow["Contato"] = contatoTelefonico.Nome;
                            newRow["Telefone"] = contatoTelefonico.Numero;
                            newRow["Email"] = cliente.Email;
                            newRow["Atualizado"] = false;
                        }
                        break;
                    }
                }
                else
                {
                    contatoTelefonico = cliente.GetContatoTelefonico();
                    newRow["Contato"] = contatoTelefonico.Nome;
                    newRow["Telefone"] = contatoTelefonico.Numero;
                    newRow["Email"] = cliente.Email;
                    newRow["Atualizado"] = false;
                }
                if (faturamento.DataEntrega != new DateTime())
                    newRow["Entrega"] = faturamento.DataEntrega.ToString("dd-MM-yyyy");
                else
                    newRow["Entrega"] = "-";
                if (faturamento.IndEntregaBoleto == (int)Faturamento.EntregaBoleto.NaoEntregue)
                    newRow["Transporte"] = "-";
                else if (faturamento.IndEntregaBoleto == (int)Faturamento.EntregaBoleto.Motorista)
                    newRow["Transporte"] = "M";
                else if (faturamento.IndEntregaBoleto == (int)Faturamento.EntregaBoleto.Correio)
                    newRow["Transporte"] = "C";
                else if (faturamento.IndEntregaBoleto == (int)Faturamento.EntregaBoleto.Email)
                    newRow["Transporte"] = "E";
                else if (faturamento.IndEntregaBoleto == (int)Faturamento.EntregaBoleto.Internet)
                    newRow["Transporte"] = "I";
                if (faturamento.DataAcesso != new DateTime())
                    newRow["Acessado"] = faturamento.DataAcesso.ToString("dd-MM-yyyy");
                else
                    newRow["Acessado"] = "-";
                i++;
                ds.Tables[0].Rows.Add(newRow);
            }
            return ds;
        }		


		private DataTable GetDataTableControleFinanceiroMensal()
		{
			DataSet ds = new DataSet();
			DataTable table = new DataTable("Result");
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
			table.Columns.Add("Entrega", Type.GetType("System.String"));
			table.Columns.Add("Acessado", Type.GetType("System.String"));
			table.Columns.Add("Transporte", Type.GetType("System.String"));
			table.Columns.Add("Contato", Type.GetType("System.String"));
			table.Columns.Add("Telefone", Type.GetType("System.String"));
			table.Columns.Add("Email", Type.GetType("System.String"));
			table.Columns.Add("Atualizado", Type.GetType("System.Boolean"));
			return table;
		}
	}
}
