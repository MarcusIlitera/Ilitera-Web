using System;
using Ilitera.Data;
using System.Data;
using System.Drawing;
using Ilitera.Common;
using System.Collections;

namespace Ilitera.Opsa.Data
{

	public enum IndCargosSESMT : int
	{
		TecnicoSegurancaTrabalho=1,
		EngenheiroSegurancaTrabalho,
		AuxiliarEnfermagem,
		EnfermeiroTrabalho,
		MedicoTrabalho
	}

	[Database("opsa", "IntegrantesSESMT", "IdIntegrantesSESMT")]
	public class IntegrantesSESMT: Ilitera.Common.JuridicaPessoa
	{
		private int	_IdIntegrantesSESMT;
		private int _IndCargoSESMT;
		private DateTime _Inicio;
		private DateTime _Termino;
		public IntegrantesSESMT()
		{
			this.IndPessoaPapel = (int)PessoaPapeis.IntegranteSESMT;
		}
		public IntegrantesSESMT(string NomePessoa)
		{
			this.IdPessoa = new Pessoa();
			this.IdPessoa.NomeAbreviado = NomePessoa;
			this.IndPessoaPapel = (int)PessoaPapeis.IntegranteSESMT;
		}
		public override int Id
		{														  
			get{return _IdIntegrantesSESMT;}
			set	{_IdIntegrantesSESMT = value;}
		}
		public int IndCargoSESMT
		{														  
			get{return _IndCargoSESMT;}
			set	{_IndCargoSESMT = value;}
		}
		public DateTime Inicio
		{														  
			get{return _Inicio;}
			set	{_Inicio = value;}
		}
		public DateTime Termino
		{														  
			get{return _Termino;}
			set	{_Termino = value;}
		}

		public string GetCargoSESMT()
		{
			string ret = string.Empty;
			switch (this.IndCargoSESMT)
			{
				case (int)IndCargosSESMT.TecnicoSegurancaTrabalho:
					ret = "Técnico de Sergurança do Trabalho";
					break;
				case (int)IndCargosSESMT.EngenheiroSegurancaTrabalho:
					ret = "Engenheiro de Segurança do Trabalho";
					break;
				case (int)IndCargosSESMT.AuxiliarEnfermagem:
					ret = "Auxiliar Enfermagem";
					break;
				case (int)IndCargosSESMT.EnfermeiroTrabalho:
					ret = "Enfermeiro do Trabalho";
					break;
				case (int)IndCargosSESMT.MedicoTrabalho:
					ret = " Médico do Trabalho";
					break;
			}
			return ret;
		}

		public static DataSet GetGridIntegrantesSESMT(Cliente cliente)
		{
			DataSet ds = new DataSet();
			DataTable table = new DataTable("Result");
			table.Columns.Add("Id", Type.GetType("System.Int32"));
			table.Columns.Add("Cargo", Type.GetType("System.String"));
			table.Columns.Add("Nome", Type.GetType("System.String"));
			table.Columns.Add("Inicio", Type.GetType("System.DateTime"));
			table.Columns.Add("Termino", Type.GetType("System.DateTime"));
			ds.Tables.Add(table);
			DataRow newRow;
			ArrayList list = new IntegrantesSESMT().Find(" " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.JuridicaPessoa.IdJuridicaPessoa IN(SELECT IdJuridicaPessoa FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.JuridicaPessoa WHERE  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.JuridicaPessoa.IdJuridica="+cliente.Id+" AND IndPessoaPapel="+(int)PessoaPapeis.IntegranteSESMT+")");
			for(int i=0; i<list.Count;i++)
			{
				((IntegrantesSESMT)list[i]).IdJuridica.Find();
				((IntegrantesSESMT)list[i]).IdPessoa.Find();
				newRow = ds.Tables[0].NewRow();
				newRow["Id"] = ((IntegrantesSESMT)list[i]).Id;
				newRow["Cargo"] = ((IntegrantesSESMT)list[i]).GetCargoSESMT();
				newRow["Nome"] = ((IntegrantesSESMT)list[i]).IdPessoa.NomeCompleto;
				newRow["Inicio"] = Ilitera.Common.Utility.TratarDateTime(((IntegrantesSESMT)list[i]).Inicio);
				newRow["Termino"] =  Ilitera.Common.Utility.TratarDateTime(((IntegrantesSESMT)list[i]).Termino);
				ds.Tables[0].Rows.Add(newRow);
			}
			return ds;
		}
	}
}
