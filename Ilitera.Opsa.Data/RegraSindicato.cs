using System;
using System.Data;
using Ilitera.Data;
using System.Collections;

namespace Ilitera.Opsa.Data
{
	[Database("opsa","RegraSindicato","IdRegraSindicato")]
    
    public class RegraSindicato : Ilitera.Data.Table, IRegra
	{
		private int _IdRegraSindicato;
		private Sindicato _IdSindicato;
		private Obrigacao _IdObrigacao;
		private string _Observacao = string.Empty;
		private int _MinEmpregado;
		private int _DiasExecutar;	
		private int _DiasExecutarPrimeira;
		private int _IndTipoPeriodicidade = (int)IndTipoPeriodicidades.NaoRealizar;
		private DateTime _DataExecutar;
		private int _IndPeriodicidade;
		private int _Intervalo;
		private int _Dia;
		private int _Mes;
		private  Obrigacao _IdObrigacaoBase;
		private  Obrigacao _IdObrigacaoBasePrimeira;
		private short _IndFeriado;
		private short _IndFeriadoPrimeira;
		private short _IndFeriadoPeriodico;
		private short _IndPeriodoExecutar;
		private short _IndPeriodoExecutarPrimeira;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]
        public RegraSindicato()
        {

        }
		public override int Id
		{
			get{return _IdRegraSindicato;}
			set{_IdRegraSindicato = value;}
		}
		public Sindicato IdSindicato
		{
			get{return _IdSindicato;}
			set{_IdSindicato = value;}
		}
		public Obrigacao IdObrigacao
		{
			get{return _IdObrigacao;}
			set{_IdObrigacao = value;}
		}
		public string Observacao
		{
			get{return _Observacao;}
			set{_Observacao = value;}
		}
		public int MinEmpregado
		{
			get{return _MinEmpregado;}
			set{_MinEmpregado = value;}
		}
		public int DiasExecutar
		{
			get{return _DiasExecutar;}
			set{_DiasExecutar = value;}
		}
		public int DiasExecutarPrimeira
		{
			get{return _DiasExecutarPrimeira;}
			set{_DiasExecutarPrimeira = value;}
		}
		public int IndTipoPeriodicidade
		{
			get{return _IndTipoPeriodicidade;}
			set{_IndTipoPeriodicidade = value;}
		}
		public DateTime DataExecutar
		{
			get{return _DataExecutar;}
			set{_DataExecutar = value;}
		}
		public int IndPeriodicidade
		{
			get{return _IndPeriodicidade;}
			set{_IndPeriodicidade = value;}
		}
		public int Intervalo
		{
			get{return _Intervalo;}
			set{_Intervalo = value;}
		}
		public int Dia
		{
			get{return _Dia;}
			set{_Dia = value;}
		}
		public int Mes
		{
			get{return _Mes;}
			set{_Mes = value;}
		}
		public Obrigacao IdObrigacaoBase
		{
			get{return _IdObrigacaoBase;}
			set{_IdObrigacaoBase = value;}
		}
		public Obrigacao IdObrigacaoBasePrimeira
		{
			get{return _IdObrigacaoBasePrimeira;}
			set{_IdObrigacaoBasePrimeira = value;}
		}
		public short IndFeriado
		{
			get{ return _IndFeriado;}
			set{_IndFeriado = value;}
		}
		public short IndFeriadoPrimeira
		{
			get{ return _IndFeriadoPrimeira;}
			set{_IndFeriadoPrimeira = value;}
		}
		public short IndFeriadoPeriodico
		{
			get{ return _IndFeriadoPeriodico;}
			set{_IndFeriadoPeriodico = value;}
		}
		public short IndPeriodoExecutar
		{
			get{ return _IndPeriodoExecutar;}
			set{_IndPeriodoExecutar = value;}
		}
		public short IndPeriodoExecutarPrimeira
		{
			get{ return _IndPeriodoExecutarPrimeira;}
			set{_IndPeriodoExecutarPrimeira = value;}
		}

		public DataSet GetGridListaObrigacoesSindicato(Sindicato sindicato, bool bSindicato, bool bCipa)
		{
			DataSet ds = new DataSet();
			DataTable table = new DataTable("Result");
			table.Columns.Add("IdObrigacao", Type.GetType("System.Int32"));
			table.Columns.Add("Obrigação", Type.GetType("System.String"));
			table.Columns.Add("Aviso", Type.GetType("System.String"));
			table.Columns.Add("Alterada", Type.GetType("System.String"));
			ds.Tables.Add(table);
			DataRow newRow;
			string where =	"(IdObrigacao IN (SELECT IdObrigacao FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.RegraLei) OR"+
							" IdObrigacao IN (SELECT IdObrigacao FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.RegraSindicato "
								+ "WHERE IdSindicato="+sindicato.Id+")) AND ";
			if(bSindicato)
				where = "IdObrigacao IN (SELECT IdObrigacao FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.RegraSindicato WHERE IdSindicato="+sindicato.Id+") AND ";
			if(bCipa)
				where = where + " IndObrigacaoTipo = 1 ORDER BY NomeReduzido";
			else
				where = where + " IsInativo IS NOT NULL ORDER BY NomeReduzido";
			ArrayList listObrigacoes = new Obrigacao().Find(where);
			for(int i=0; i<listObrigacoes.Count;i++)
			{
				newRow = ds.Tables[0].NewRow();
				newRow["IdObrigacao"] = ((Obrigacao)listObrigacoes[i]).Id;
				newRow["Obrigação"] = ((Obrigacao)listObrigacoes[i]).NomeReduzido;
				newRow["Aviso"] = ((Obrigacao)listObrigacoes[i]).Aviso;
				if(this.Find("IdObrigacao="+((Obrigacao)listObrigacoes[i]).Id+" AND IdSindicato="+sindicato.Id).Count>0)
					newRow["Alterada"] = "X";
				else
					newRow["Alterada"] = "";
				ds.Tables[0].Rows.Add(newRow);
			}
			return ds;
		}
	}
}