using System;
using System.Data;
using Ilitera.Common;
using Ilitera.Opsa.Data;

namespace Ilitera.Opsa.Report
{
	public class DataSourceEmpresas
	{
		public DataSourceEmpresas()
		{

		}

		public RptListaJuridicaPorCep GetReportListaJuridicaCep(DataSet dsEmpresas)
		{
			RptListaJuridicaPorCep report = new RptListaJuridicaPorCep();
			report.SetDataSource(GetListaEmpresa(dsEmpresas));
			report.Refresh();			
			return report;
		}

		public RptListaJuridicaPorMotorista GetReportListaJuridicaMotorista(DataSet dsEmpresas)
		{
			RptListaJuridicaPorMotorista report = new RptListaJuridicaPorMotorista();
			report.SetDataSource(GetListaEmpresa(dsEmpresas));
			report.Refresh();			
			return report;
		}

		private DataSet GetListaEmpresa(DataSet dsEmpresas)
		{
			DataSet ds = new DataSet();
			DataTable table = new DataTable("Result");		
			table.Columns.Add("Titulo", Type.GetType("System.String"));
			table.Columns.Add("Empresa", Type.GetType("System.String"));
			table.Columns.Add("Contato", Type.GetType("System.String"));
			table.Columns.Add("Telefone", Type.GetType("System.String"));
			table.Columns.Add("Cep", Type.GetType("System.String"));
			table.Columns.Add("Motorista", Type.GetType("System.String"));
			ds.Tables.Add(table);
			
			DataRow newRow;		
			
			foreach(DataRow row in dsEmpresas.Tables[0].Rows)
			{
				Juridica juridica = new Juridica();
				juridica.Find(Convert.ToInt32(row["Id"]));	

				Endereco endereco = juridica.GetEndereco();
				juridica.IdTransporte.Find();
				ContatoTelefonico contatoTelefonico = new ContatoTelefonico();
				contatoTelefonico.Find("IdPessoa=" + Convert.ToInt32(row["Id"]) + " AND IndTipoTelefone=" + (short)Ilitera.Common.ContatoTelefonico.TipoTelefone.Comercial);
				
				newRow = ds.Tables[0].NewRow();	

				switch(juridica.IdJuridicaPapel.Id)
				{
                    case (int)IndJuridicaPapel.Cliente:
                        {
                            if (juridica.IsInativo)
                                newRow["Titulo"] = "Listagem de Clientes Inativos";
                            else
                                newRow["Titulo"] = "Listagem de Clientes Ativos";
                        }
                        break;
					case (int)IndJuridicaPapel.Sindicato :
						newRow["Titulo"] = "Listagem de Sindicatos";
						break;
					case (int)IndJuridicaPapel.Clinica :
						newRow["Titulo"] = "Listagem de Clínicas";
						break;
					case (int)IndJuridicaPapel.ClinicaOutras :
						newRow["Titulo"] = "Listagem de Clínicas Outras";
						break;
					default :
						newRow["Titulo"] = "Listagem de Empresas";
						break;
				}
				newRow["Empresa"]	= row["Nome"];
				newRow["Contato"]	= contatoTelefonico.Nome;
				newRow["Telefone"]	= contatoTelefonico.Numero;		
				newRow["Cep"]		= endereco.Cep;
				newRow["Motorista"]	= (juridica.IdTransporte.Descricao==string.Empty)?"Sem Motorista":juridica.IdTransporte.Descricao;
				
				ds.Tables[0].Rows.Add(newRow);
			}
			return ds;
		}
	}
}
