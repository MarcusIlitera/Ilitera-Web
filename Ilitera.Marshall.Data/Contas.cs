using System;
using Ilitera.Data;
using Ilitera.Common;

namespace Ilitera.Marshall.Data
{
	public enum PlanoDeContas : int
	{
		BanespaOPSA					= 10,  //1.1.2.01.01 Banespa - OPSA - 002963-4
		BanespaMestra				= 12,  //1.1.2.01.03 Banespa - Mestra - 002805-9
		ServicoDeSegTrabalholMensal	= 215, //3.1.1.03.01 Serviço de Seg. do Trabalhol Mensal
		ContasReceber				= 20,  //1.1.4.01.01 Contas a Receber
		JurosClientes				= 226, //3.1.2.01.03 Juros de Clientes
		IRRecuperar					= 345, //1.1.5.01.03 IR a Recuperar	
		PIS_CONFIS_CSLL_Recuperar	= 543  //1.1.5.01.05 PIS/CONFIS/CSLL a Recuperar
	}

	[Database("marshall", "Contas", "ID", true)]
	public class Contas: Ilitera.Data.Table
	{
		private int _ID;
		private string _Numero=string.Empty;
		private string _Descricao=string.Empty;
		private int _Tipo;
		private DateTime _Fechamento;
		private int _Grupo;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public Contas()
		{

		}
		public override int Id
		{
			get{return _ID;}
			set{_ID = value;}
		}
		public string Numero
		{
			get{return _Numero;}
			set{_Numero = value;}
		}
		public int Tipo
		{
			get{return _Tipo;}
			set{_Tipo = value;}
		}
		public string Descricao
		{
			get{return _Descricao;}
			set{_Descricao = value;}
		}
		public DateTime Fechamento
		{
			get{return _Fechamento;}
			set{_Fechamento = value;}
		}
		public int Grupo
		{
			get{return _Grupo;}
			set{_Grupo = value;}
		}
	}
}
