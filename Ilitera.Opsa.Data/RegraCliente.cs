using System;
using System.Data;
using System.Collections;
using System.Text;
using Ilitera.Data;

namespace Ilitera.Opsa.Data
{
	[Database("opsa","RegraCliente","IdRegraCliente")]

    

    public class RegraCliente : Ilitera.Data.Table, IRegra
	{
		private int _IdRegraCliente;
		private Obrigacao _IdObrigacao;
		private Cliente _IdCliente;
		private string _Observacao = string.Empty;
		private int _MinEmpregado;
		private int _DiasExecutar;	
		private int _DiasExecutarPrimeira;
		private int _IndTipoPeriodicidade= (int)IndTipoPeriodicidades.NaoRealizar;
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
        public RegraCliente()
        {

        }
		public override int Id
		{
			get{return _IdRegraCliente;}
			set{_IdRegraCliente = value;}
		}
		public Obrigacao IdObrigacao
		{
			get{return _IdObrigacao;}
			set{_IdObrigacao = value;}
		}
		public Cliente IdCliente
		{
			get{return _IdCliente;}
			set{_IdCliente = value;}
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

		public DataSet GetGridListaObrigacoes(Cliente cliente, bool bCliente, bool IsCipa)
		{
			DataSet ds = new DataSet();
			DataTable table = new DataTable("Result");
			table.Columns.Add("IdObrigacao", Type.GetType("System.Int32"));
			table.Columns.Add("Obrigação", Type.GetType("System.String"));
			table.Columns.Add("Aviso", Type.GetType("System.String"));
			table.Columns.Add("Ativo", Type.GetType("System.String"));
			table.Columns.Add("Alterada", Type.GetType("System.String"));
			ds.Tables.Add(table);
			DataRow newRow;
			StringBuilder where = new StringBuilder();
			where.Append("(IdObrigacao IN (SELECT IdObrigacao FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.RegraLei) OR");
			where.Append(" IdObrigacao IN (SELECT IdObrigacao FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.RegraCliente");
			where.Append(" WHERE IdCliente="+cliente.Id+")) AND ");
			if(bCliente)
				where.Append(" IdObrigacao IN (SELECT IdObrigacao FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.RegraCliente WHERE IdCliente="+cliente.Id+") AND ");
			if(IsCipa)
				where.Append(" IndObrigacaoTipo = 1 ORDER BY NomeReduzido");
			else
				where.Append(" IsInativo IS NOT NULL ORDER BY NomeReduzido");
			ArrayList listObrigacoes = new Obrigacao().Find(where.ToString());
			for(int i=0; i<listObrigacoes.Count;i++)
			{
				newRow = ds.Tables[0].NewRow();
				newRow["IdObrigacao"] = ((Obrigacao)listObrigacoes[i]).Id;
				newRow["Obrigação"] = ((Obrigacao)listObrigacoes[i]).NomeReduzido;
				newRow["Aviso"] = ((Obrigacao)listObrigacoes[i]).Aviso;
				newRow["Ativo"] = ((Obrigacao)listObrigacoes[i]).IsInativo?"":"X";
				newRow["Alterada"] = (this.Find("IdObrigacao="+((Obrigacao)listObrigacoes[i]).Id+" AND IdCliente="+cliente.Id).Count>0)?"X":"";
				ds.Tables[0].Rows.Add(newRow);
			}
			return ds;
		}
	}
}