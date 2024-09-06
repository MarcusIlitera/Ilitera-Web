using System;
using System.Data;
using System.Collections;
using Ilitera.Data;
using Ilitera.Common;

namespace Ilitera.Opsa.Data
{
	public interface IRegra
	{
		string Observacao
		{
			get;
			set;
		}
		int MinEmpregado
		{
			get;
			set;
		}
		int DiasExecutar
		{
			get;
			set;
		}
		int DiasExecutarPrimeira
		{
			get;
			set;
		}
		int IndTipoPeriodicidade
		{
			get;
			set;
		}
		DateTime DataExecutar
		{
			get;
			set;
		}
		int IndPeriodicidade
		{
			get;
			set;
		}
		int Intervalo
		{
			get;
			set;
		}
		int Dia
		{
			get;
			set;
		}
		int Mes
		{
			get;
			set;
		}
		Obrigacao IdObrigacaoBase
		{
			get;
			set;
		}
		Obrigacao IdObrigacaoBasePrimeira
		{
			get;
			set;
		}
		short IndFeriado
		{
			get;
			set;
		}
		short IndFeriadoPrimeira
		{
			get;
			set;
		}
		short IndFeriadoPeriodico
		{
			get;
			set;
		}
		short IndPeriodoExecutar
		{
			get;
			set;
		}
		short IndPeriodoExecutarPrimeira
		{
			get;
			set;
		}
	}

	public enum IndTipoPeriodicidades : int
	{
		ApenasUmaVez,
		Periodico,
		EventoBase,
		NaoRealizar
	}

	[Database("opsa","RegraLei","IdRegraLei")]
	public class RegraLei : Ilitera.Data.Table, IRegra
	{
		private int _IdRegraLei;
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

        public RegraLei()
		{
		}
		public override int Id
		{
			get{return _IdRegraLei;}
			set{_IdRegraLei = value;}
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

		private static string VerificaPeriodo(short iPeriodo)
		{
			string sPeriodo = string.Empty;
			switch(iPeriodo)
			{
				case 0 : sPeriodo = "Nenhuma";
					break;
				case 1 : sPeriodo = "dia";
					break;
				case 2 : sPeriodo = "semana";
					break;
				case 3 : sPeriodo = "mês";
					break;
				case 4 : sPeriodo = "ano";
					break;
			}
			return sPeriodo;
		}

		public static string GetPeridicidade(IRegra regra)
		{
			System.Text.StringBuilder ret = new System.Text.StringBuilder();
			
            if(regra.IndTipoPeriodicidade ==(int)IndTipoPeriodicidades.Periodico)
			{
				if(regra.DiasExecutar==0 && regra.Mes==0)
					ret.Append("Período em " + regra.Intervalo + " " + VerificaPeriodo((short)regra.IndPeriodicidade));		
				else if(regra.Dia==0 && regra.Mes!=0)
					ret.Append("Período em " + regra.Intervalo + " " + VerificaPeriodo((short)regra.IndPeriodicidade)
						+" no mes "+regra.Mes.ToString("00"));		
				else if(regra.Dia!=0 && regra.Mes==0)
					ret.Append("Período em " + regra.Intervalo + " " + VerificaPeriodo((short)regra.IndPeriodicidade)
						+" no dia "+regra.Dia.ToString("00"));
				else
					ret.Append("Período a cada " + regra.Intervalo + " " + VerificaPeriodo((short)regra.IndPeriodicidade)
						+" em "+regra.Dia.ToString("00") + "/"+regra.Mes.ToString("00"));		
			}
			else if(regra.IdObrigacaoBase.Id!=0 && regra.IndTipoPeriodicidade ==(int)IndTipoPeriodicidades.EventoBase)
			{
				regra.IdObrigacaoBase.Find();
				ret.Append("A partir da "+ regra.IdObrigacaoBase.Nome);
				if(regra.IndPeriodoExecutar != 0 || regra.DiasExecutar != 0)
					ret.Append(" em " + regra.DiasExecutar + " " + VerificaPeriodo(regra.IndPeriodoExecutar));
			}
			else if(regra.IndTipoPeriodicidade ==(int)IndTipoPeriodicidades.NaoRealizar)
				ret.Append("Nenhuma");
			else if(regra.IndTipoPeriodicidade ==(int)IndTipoPeriodicidades.ApenasUmaVez)
				ret.Append("Apenas uma vez");
			else
				ret.Append("-");

            return ret.ToString();
		}

	}
}