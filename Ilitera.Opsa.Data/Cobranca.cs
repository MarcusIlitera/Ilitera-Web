using System;
using Ilitera.Data;
using Ilitera.Common;

namespace Ilitera.Opsa.Data
{
	[Database("opsa","Cobranca","IdCobranca")]
	public class Cobranca: Ilitera.Data.Table 
	{
		private int _IdCobranca;
		private string _Descricao = string.Empty;
		private int _MultaInicioDias;
		private double _MultaValor;
		private bool _MultaPct;
		private double _JurosValor;
		private bool _JurosPct;
		private int _JurosPeriodicidade;
		private double _DescontoValor;
		private bool _DescontoPct;
		private int _DescontoPeriodicidade;
		private string _Instrucao = string.Empty;


        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public Cobranca()
		{
		}
		public override int Id
		{
			get{return _IdCobranca;}
			set{_IdCobranca = value;}
		}
		public string Descricao
		{
			get{return _Descricao;}
			set{_Descricao = value;}
		}
		public int MultaInicioDias
		{
			get{return _MultaInicioDias;}
			set{_MultaInicioDias = value;}
		}
		public double MultaValor
		{
			get{return _MultaValor;}
			set{_MultaValor = value;}
		}
		public bool MultaPct
		{
			get{return _MultaPct;}
			set{_MultaPct = value;}
		}
		public double JurosValor
		{
			get{return _JurosValor;}
			set{_JurosValor = value;}
		}
		public bool JurosPct
		{
			get{return _JurosPct;}
			set{_JurosPct = value;}
		}
		public int JurosPeriodicidade
		{
			get{return _JurosPeriodicidade;}
			set{_JurosPeriodicidade = value;}
		}
		public double DescontoValor
		{
			get{return _DescontoValor;}
			set{_DescontoValor = value;}
		}
		public bool DescontoPct
		{
			get{return _DescontoPct;}
			set{_DescontoPct = value;}
		}
		public int DescontoPeriodicidade
		{
			get{return _DescontoPeriodicidade;}
			set{_DescontoPeriodicidade = value;}
		}
		public string Instrucao
		{
			get{return _Instrucao;}
			set{_Instrucao = value;}
		}
	}
}
