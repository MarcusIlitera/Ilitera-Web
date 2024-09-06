using System;
using System.Text;
using System.Data;
using System.Collections;
using Ilitera.Common;
using Ilitera.Opsa.Data;

namespace Ilitera.Opsa.Report
{

	public class DataSourceFichaCadastralProspeccao
	{
		private Prospeccao prospeccao;

		public DataSourceFichaCadastralProspeccao(Prospeccao prospeccao)
		{
			this.prospeccao = prospeccao;
		}

		public RptFichaCadastralProspeccao GetReport()
		{
			RptFichaCadastralProspeccao report = new RptFichaCadastralProspeccao();	
			report.OpenSubreport("Filiais").SetDataSource(GetFiliais());
			report.OpenSubreport("Contatos").SetDataSource(GetContatos());
			report.OpenSubreport("HistoricoContatos").SetDataSource(GetAcoesComerciais());
			report.SetDataSource(GetDataSource());
			report.Refresh();
			return report;
		}

		private DataSet GetDataSource()
		{
			DataSet ds = new DataSet();
			DataTable table = new DataTable("Result");
			table.Columns.Add("NomeCompleto", Type.GetType("System.String"));
			table.Columns.Add("Endereco", Type.GetType("System.String"));
			table.Columns.Add("Bairro", Type.GetType("System.String"));
			table.Columns.Add("RamoAtividade", Type.GetType("System.String"));
			table.Columns.Add("Site", Type.GetType("System.String"));
			table.Columns.Add("Email", Type.GetType("System.String"));
			table.Columns.Add("CEP", Type.GetType("System.String"));
			table.Columns.Add("Cidade", Type.GetType("System.String"));
			table.Columns.Add("UF", Type.GetType("System.String"));			
			table.Columns.Add("CGC", Type.GetType("System.String"));
			table.Columns.Add("IE", Type.GetType("System.String"));
			table.Columns.Add("Observacao", Type.GetType("System.String"));		
			table.Columns.Add("Empregados", Type.GetType("System.String"));
			table.Columns.Add("CIPA", Type.GetType("System.String"));
			table.Columns.Add("VasosPressao", Type.GetType("System.String"));
			table.Columns.Add("Caldeira", Type.GetType("System.String"));
			table.Columns.Add("Empilhadeira", Type.GetType("System.String"));
			table.Columns.Add("PonteRolante", Type.GetType("System.String"));
			table.Columns.Add("Prensas", Type.GetType("System.String"));
			table.Columns.Add("NumeroSetores", Type.GetType("System.String"));
			table.Columns.Add("Turnover", Type.GetType("System.String"));
			table.Columns.Add("Ruido", Type.GetType("System.String"));
			table.Columns.Add("Quimico", Type.GetType("System.String"));
			table.Columns.Add("AssesoriaAtual", Type.GetType("System.String"));
			table.Columns.Add("ValorAtual", Type.GetType("System.String"));
			table.Columns.Add("VencimentoContratoAtual", Type.GetType("System.String"));
			table.Columns.Add("ValorSeguranca", Type.GetType("System.String"));
			table.Columns.Add("ValorMedicina", Type.GetType("System.String"));
			ds.Tables.Add(table);
			DataRow newRow;

			Endereco endereco = prospeccao.GetEndereco();

			newRow = ds.Tables[0].NewRow();
			newRow["NomeCompleto"]	= prospeccao.NomeCompleto;
			newRow["Endereco"]		= endereco.GetEndereco();
			newRow["Bairro"]		= endereco.Bairro;
			newRow["RamoAtividade"]	= prospeccao.Atividade;
			newRow["Site"]			= prospeccao.Site;
			newRow["Email"]			= prospeccao.Email;
			newRow["CEP"]			= endereco.Cep;
			newRow["Cidade"]		= endereco.GetCidade();
			newRow["UF"]			= endereco.GetEstado();
			newRow["CGC"]			= prospeccao.NomeCodigo;
			newRow["IE"]			= prospeccao.IE;
			newRow["Observacao"]	= prospeccao.Filiais;
			newRow["Empregados"]	= prospeccao.QtdEmpregados==0?string.Empty:prospeccao.QtdEmpregados.ToString();
			newRow["CIPA"]			= prospeccao.CIPA?"SIM":"NÃO";
			newRow["VasosPressao"]	= prospeccao.VasoPressao==0?string.Empty:prospeccao.VasoPressao.ToString();
			newRow["Caldeira"]		= prospeccao.Caldeira==0?string.Empty:prospeccao.Caldeira.ToString();
			newRow["Empilhadeira"]	= prospeccao.Empilhadeira==0?string.Empty:prospeccao.Empilhadeira.ToString();
			newRow["PonteRolante"]	= prospeccao.PonteRolante==0?string.Empty:prospeccao.PonteRolante.ToString();
			newRow["Prensas"]		= prospeccao.Prensas==0?string.Empty:prospeccao.Prensas.ToString();
			newRow["NumeroSetores"]	= prospeccao.NumeroSetores==0?string.Empty:prospeccao.NumeroSetores.ToString();
			newRow["Turnover"]		= prospeccao.Turnover==0F?string.Empty:prospeccao.Turnover.ToString("n");
			newRow["Ruido"]			= prospeccao.Ruido?"SIM":"NÃO";
			newRow["Quimico"]		= prospeccao.Quimico?"SIM":"NÃO";;
			newRow["AssesoriaAtual"]= prospeccao.AssesoriaAtual;
			newRow["ValorAtual"]	= prospeccao.ValorContrato==0F?string.Empty:prospeccao.ValorContrato.ToString("n");
			newRow["VencimentoContratoAtual"]	= prospeccao.VencimentoContrato==new DateTime()?string.Empty:prospeccao.VencimentoContrato.ToString("dd-MM-yyyy");
			newRow["ValorSeguranca"]			= prospeccao.ValorSeguranca==0F?string.Empty:prospeccao.ValorSeguranca.ToString("n");
			newRow["ValorMedicina"]				= prospeccao.ValorPCMSO==0F?string.Empty:prospeccao.ValorPCMSO.ToString("n");
			ds.Tables[0].Rows.Add(newRow);
			return ds;
		}	

		private DataSet GetContatos()
		{
			DataSet ds = new DataSet();
			DataTable table = new DataTable("Result");
			table.Columns.Add("tNomeContato", Type.GetType("System.String"));
			table.Columns.Add("tDDD", Type.GetType("System.String"));
			table.Columns.Add("tNumero", Type.GetType("System.String"));
			table.Columns.Add("tDepartamento", Type.GetType("System.String"));
			table.Columns.Add("tTipo", Type.GetType("System.String"));
			ds.Tables.Add(table);
			DataRow newRow;

			ArrayList listContato = new ContatoTelefonico().Find("IdPessoa="+prospeccao.Id+" ORDER BY IndTipoTelefone");

			foreach(ContatoTelefonico contato in listContato)
			{
				newRow = ds.Tables[0].NewRow();
				newRow["tNomeContato"]	= contato.Nome;
				newRow["tDDD"]			= contato.DDD;
				newRow["tNumero"]		= contato.Numero;
				newRow["tDepartamento"]	= contato.Departamento;
				newRow["tTipo"]			= contato.GetTipo();
				ds.Tables[0].Rows.Add(newRow);
			}
			if(listContato.Count<3)
			{
				for(int i = 0; i < (3 - listContato.Count); i++)
				{
					newRow = ds.Tables[0].NewRow();
					newRow["tNomeContato"]	= string.Empty;
					newRow["tDDD"]			= string.Empty;
					newRow["tNumero"]		= string.Empty;
					newRow["tDepartamento"]	= string.Empty;
					newRow["tTipo"]			= string.Empty;
					ds.Tables[0].Rows.Add(newRow);
				}
			}
			return ds;
		}

		private DataSet GetAcoesComerciais()
		{
			DataSet ds = new DataSet();
			DataTable table = new DataTable("Result");
			table.Columns.Add("TipoContato", Type.GetType("System.String"));
			table.Columns.Add("DataContato", typeof(System.DateTime));
			table.Columns.Add("Prestador", Type.GetType("System.String"));
			table.Columns.Add("Status", Type.GetType("System.String"));
			table.Columns.Add("Agenda", Type.GetType("System.String"));
			table.Columns.Add("Descricao", Type.GetType("System.String"));
			ds.Tables.Add(table);
			DataRow newRow;

			ArrayList listTelemarketing = new Telemarketing().Find("IdProspeccao="+prospeccao.Id+" ORDER BY DataTelemarketing");

			foreach(Telemarketing telemarketing in listTelemarketing)
			{
				telemarketing.IdTelemarketingAcao.Find();
				telemarketing.IdPrestador.Find();
				telemarketing.IdTelemarketingStatus.Find();
				telemarketing.IdCompromisso.Find();
				newRow = ds.Tables[0].NewRow();
				newRow["TipoContato"]	= telemarketing.IdTelemarketingAcao.Descricao;
				newRow["DataContato"]	= telemarketing.DataTelemarketing;
				newRow["Prestador"]		= telemarketing.IdPrestador.NomeAbreviado;
				newRow["Status"]		= telemarketing.IdTelemarketingStatus.Descricao;
				newRow["Agenda"]		= telemarketing.IdCompromisso.GetDataComprimisso();
				newRow["Descricao"]		= telemarketing.Descricao;
				ds.Tables[0].Rows.Add(newRow);
			}
			if(listTelemarketing.Count<13)
			{
				for(int i = 0; i < (13 - listTelemarketing.Count); i++)
				{
					newRow = ds.Tables[0].NewRow();
					newRow["TipoContato"]	= string.Empty;
					newRow["DataContato"]	= System.DBNull.Value;
					newRow["Prestador"]		= string.Empty;
					newRow["Status"]		= string.Empty;
					newRow["Agenda"]		= string.Empty;
					newRow["Descricao"]		= string.Empty;
					ds.Tables[0].Rows.Add(newRow);
				}
			}
			return ds;
		}

		private DataSet GetFiliais()
		{
			DataSet ds = new DataSet();
			DataTable table = new DataTable("Result");
			table.Columns.Add("IdProspeccao", Type.GetType("System.Int32"));
			table.Columns.Add("NomeFilial", Type.GetType("System.String"));
			table.Columns.Add("Empregados", Type.GetType("System.String"));
			table.Columns.Add("CIPA", Type.GetType("System.String"));
			table.Columns.Add("VasoPressao", Type.GetType("System.String"));
			table.Columns.Add("Caldeiras", Type.GetType("System.String"));
			table.Columns.Add("NumeroSetores", Type.GetType("System.String"));
			ds.Tables.Add(table);
			DataRow newRow;

			ArrayList listProspeccaoFilial = new ProspeccaoFilial().Find("IdProspeccao="+prospeccao.Id+" ORDER BY NomeFilial");

			foreach(ProspeccaoFilial filial in listProspeccaoFilial)
			{
				newRow = ds.Tables[0].NewRow();
				newRow["IdProspeccao"]	= prospeccao.Id;
				newRow["NomeFilial"]	= filial.NomeFilial;
				newRow["Empregados"]	= filial.QtdEmpregado.ToString();
				newRow["CIPA"]			= filial.TemCipa?"SIM":"NÃO";;
				newRow["VasoPressao"]	= filial.VasoPressao.ToString();
				newRow["Caldeiras"]		= filial.Caldeira.ToString();
				newRow["NumeroSetores"]	= filial.NumeroSetor.ToString();
				ds.Tables[0].Rows.Add(newRow);
			}
			if(listProspeccaoFilial.Count<3)
			{
				for(int i = 0; i < (3 - listProspeccaoFilial.Count); i++)
				{
					newRow = ds.Tables[0].NewRow();
					newRow["IdProspeccao"]	= prospeccao.Id;
					newRow["NomeFilial"]	= string.Empty;
					newRow["Empregados"]	= string.Empty;
					newRow["CIPA"]			= string.Empty;
					newRow["VasoPressao"]	= string.Empty;
					newRow["Caldeiras"]		= string.Empty;
					newRow["NumeroSetores"]	= string.Empty;
					ds.Tables[0].Rows.Add(newRow);
				}
			}
			return ds;
		}
	}
}
