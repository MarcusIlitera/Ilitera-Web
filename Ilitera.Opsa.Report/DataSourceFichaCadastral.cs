using System;
using System.Text;
using System.Data;
using System.Collections;
using Ilitera.Common;
using Ilitera.Opsa.Data;

namespace Ilitera.Opsa.Report
{

	public class DataSourceFichaCadastral
	{
		private Juridica juridica;
		private Endereco endereco;

		public DataSourceFichaCadastral(Juridica juridica)
		{
			this.juridica = juridica;
			this.endereco = this.juridica.GetEndereco();
		}

		public RptFichaCadastral GetReport()
		{
			RptFichaCadastral report = new RptFichaCadastral();		
			report.SetDataSource(DataSourceRptFichaCadastral());
			report.OpenSubreport("Contatos").SetDataSource(DataSourceRptContatos());
			report.Refresh();
			return report;
		}

		private DataSet DataSourceRptFichaCadastral()
		{
			DataTable table = new DataTable("Result");
			table.Columns.Add("tNomeCompleto", Type.GetType("System.String"));
			table.Columns.Add("tEndereco", Type.GetType("System.String"));
			table.Columns.Add("tBairro", Type.GetType("System.String"));
			table.Columns.Add("tRamoAtividade", Type.GetType("System.String"));
			table.Columns.Add("tSite", Type.GetType("System.String"));
			table.Columns.Add("tEmail", Type.GetType("System.String"));
			table.Columns.Add("tCEP", Type.GetType("System.String"));
			table.Columns.Add("tCidade", Type.GetType("System.String"));
			table.Columns.Add("tUF", Type.GetType("System.String"));			
			table.Columns.Add("tCGC", Type.GetType("System.String"));
			table.Columns.Add("tIE", Type.GetType("System.String"));
			table.Columns.Add("tObservacao", Type.GetType("System.String"));		
			
			DataSet ds = new DataSet();
			ds.Tables.Add(table);
			
			DataRow newRow = ds.Tables[0].NewRow();
			
			newRow["tNomeCompleto"]		= juridica.NomeCompleto;
			newRow["tEndereco"]			= endereco.GetEndereco();
			newRow["tBairro"]			= endereco.Bairro;
			newRow["tRamoAtividade"]	= juridica.Atividade;
			newRow["tSite"]				= juridica.Site;
			newRow["tEmail"]			= juridica.Email;
			newRow["tCEP"]				= endereco.Cep;
			newRow["tCidade"]			= endereco.GetCidade();
			newRow["tUF"]				= endereco.GetEstado();
			newRow["tCGC"]				= juridica.NomeCodigo;
			newRow["tIE"]				= juridica.IE;
			newRow["tObservacao"]		= juridica.Observacao;

			ds.Tables[0].Rows.Add(newRow);

			return ds;
		}

		public DataSet DataSourceRptContatos()
		{
			DataTable table = new DataTable("Result");
			table.Columns.Add("tNomeContato", Type.GetType("System.String"));
			table.Columns.Add("tDDD", Type.GetType("System.String"));
			table.Columns.Add("tNumero", Type.GetType("System.String"));
			table.Columns.Add("tDepartamento", Type.GetType("System.String"));
			table.Columns.Add("tTipo", Type.GetType("System.String"));
			
			DataSet ds = new DataSet();
			ds.Tables.Add(table);

			DataRow newRow;
			
			ArrayList listContatos = new ArrayList();
			
			ContatoTelefonico contatoTelefonicoPadrao = new ContatoTelefonico();			
			contatoTelefonicoPadrao = juridica.GetContatoTelefonico();
			
			if(contatoTelefonicoPadrao.Id != 0)
				listContatos.Add(contatoTelefonicoPadrao);
			
			ContatoTelefonico contatoTelefonicoSegundo = new ContatoTelefonico();
			contatoTelefonicoSegundo = juridica.GetContatoTelefonico2();
			
			if(contatoTelefonicoSegundo.Id != 0)
				listContatos.Add(contatoTelefonicoSegundo);
			
			ContatoTelefonico contatoFax = new ContatoTelefonico();
			contatoFax = juridica.GetFax();
			
			if(contatoFax.Id != 0)
				listContatos.Add(contatoFax);
			
			for(int i=0; i < listContatos.Count; i++)
			{
				newRow = ds.Tables[0].NewRow();
				newRow["tNomeContato"]	= ((ContatoTelefonico)listContatos[i]).Nome;
				newRow["tDDD"]			= ((ContatoTelefonico)listContatos[i]).DDD;
				newRow["tNumero"]		= ((ContatoTelefonico)listContatos[i]).Numero;
				newRow["tDepartamento"] = ((ContatoTelefonico)listContatos[i]).Departamento;
				
				switch(((ContatoTelefonico)listContatos[i]).IndTipoTelefone)
				{
					case 0: newRow["tTipo"] = "Padrão";
						break;
					case 1: newRow["tTipo"] = "Segundo";
						break;
					case 2: newRow["tTipo"] = "Fax";
						break;
				}				
				ds.Tables[0].Rows.Add(newRow);
			}			
			return ds;
		}
	}
}
