using System;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Text;
using Ilitera.Data;
using Ilitera.Opsa.Data;
using Ilitera.Common;

namespace Ilitera.Opsa.Report
{	
	public class DataSourceSaidaDocumento
	{
		private Cliente cliente;

		public DataSourceSaidaDocumento(Cliente cliente)
		{
			this.cliente = cliente;
		}

		public RptSaidaDocumento GetReport()
		{
			RptSaidaDocumento report = new RptSaidaDocumento();
			report.SetDataSource(GetSaidaDocumento());
			report.Refresh();
			return report;
		}

		public DataSet GetSaidaDocumento()
		{
			DataSet ds = new DataSet();

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

			ds.Tables.Add(table);
	
			Endereco endereco = cliente.GetEndereco();
			ContatoTelefonico contatoTelefone = new ContatoTelefonico();
			contatoTelefone.Find("IdPessoa="+  cliente.Id 
				+ " AND IndTipoTelefone="+(short)ContatoTelefonico.TipoTelefone.Comercial);
			cliente.IdTransporte.Find();


			DataRow newRow = ds.Tables[0].NewRow();		

			newRow["NOME_FANTASIA"] = cliente.NomeAbreviado;
			newRow["RAZAO_SOCIAL"]	= cliente.NomeCompleto;
			newRow["ENDERECO"]		= endereco.GetEndereco();
			newRow["BAIRRO"]		= endereco.Bairro;
			newRow["CIDADE"]		= endereco.Municipio;
			newRow["ESTADO"]		= endereco.Uf;			
			newRow["CEP"]			= endereco.Cep;
			newRow["CONTATO"]		= contatoTelefone.Nome;
//			newRow["OBSERVACAO"]	= this.Observacao;
//			newRow["TELEFONE"]		= contatoTelefone.Numero;
//			newRow["MOTORISTA"]		=  cliente.IdTransporte.Descricao;
//			newRow["TRANSPORTE"]	=  this.GetTipoSaidaDocumento();

			ds.Tables[0].Rows.Add(newRow);	
	
			return ds;
		}
	}
}
