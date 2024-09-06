using System;
using System.Data;
using System.Text;
using System.Collections;
using Ilitera.Opsa.Data;
using Ilitera.Common;

namespace Ilitera.PCMSO.Report
{
    public class DataSourceClienteClinica : DataSourceBase
	{

		public DataSourceClienteClinica()
		{				

		}

		public RptClienteClinica GetReport()
		{
			RptClienteClinica report = new RptClienteClinica();
			report.SetDataSource(GetDataSource());
			report.Refresh();
            SetTempoProcessamento(report);
			return report;
		}

        private DataSet GetDataSource()
        {
            DataSet ds = new DataSet();
            ds.Tables.Add(GetTable());
            DataRow newRow;

            ArrayList listCliente = new Cliente().Find("ContrataPCMSO=1"
                                                    + " AND IdJuridicaPapel =" + (int)IndJuridicaPapel.Cliente
                                                    + " AND IsInativo=0"
                                                    + " ORDER BY NomeAbreviado");

            foreach (Cliente cliente in listCliente)
            {
                ArrayList listClinicas = new ClinicaCliente().Find("IdCliente=" + cliente.Id);

                foreach (ClinicaCliente clinicaCliente in listClinicas)
                {
                    clinicaCliente.IdClinica.Find();

                    Endereco enderecoClinica = clinicaCliente.IdClinica.GetEndereco();

                    StringBuilder telefone = new StringBuilder();
                    StringBuilder contato = new StringBuilder();

                    ArrayList listContatosTelefonicos = new ContatoTelefonico().Find("IdPessoa=" + clinicaCliente.IdClinica.Id 
                                                                                    + " ORDER BY IndTipoTelefone");
                    foreach (ContatoTelefonico contatoTelefonico in listContatosTelefonicos)
                    {
                        telefone.Append(contatoTelefonico.GetDDDTelefone() + " \n");
                        contato.Append(contatoTelefonico.Nome + " \n");
                    }

                    newRow = ds.Tables[0].NewRow();

                    newRow["NomeCliente"] = cliente.NomeAbreviado;
                    newRow["NomeClinica"] = clinicaCliente.IdClinica.NomeAbreviado;
                    newRow["NumeroEmpregados"] = cliente.QtdEmpregados;
                    newRow["ClinicaPadrao"] = clinicaCliente.ClinicaPadrao;
                    newRow["Endereco"] = enderecoClinica.GetEnderecoCompletoPorLinhaSemCep();
                    newRow["Email"] = clinicaCliente.IdClinica.Email;
                    newRow["ContatoTelefonico"] = telefone.ToString();
                    newRow["Contato"] = contato.ToString();

                    ds.Tables[0].Rows.Add(newRow);
                }
            }
            return ds;
        }

        private static DataTable GetTable()
        {
            DataTable table = new DataTable("Result");
            table.Columns.Add("NomeCliente", Type.GetType("System.String"));
            table.Columns.Add("NomeClinica", Type.GetType("System.String"));
            table.Columns.Add("Endereco", Type.GetType("System.String"));
            table.Columns.Add("Email", Type.GetType("System.String"));
            table.Columns.Add("ContatoTelefonico", Type.GetType("System.String"));
            table.Columns.Add("ClinicaPadrao", Type.GetType("System.Boolean"));
            table.Columns.Add("NumeroEmpregados", Type.GetType("System.String"));
            table.Columns.Add("Contato", Type.GetType("System.String"));
            return table;
        }
	}
}
