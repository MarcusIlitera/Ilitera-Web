using System;
using Ilitera.Data;
using Ilitera.Common;

namespace Ilitera.Opsa.Data
{
	public enum RequisitosLegais: int
	{
		NR7 = 1,	
		NR15 = -1031162396,
		ACGIH = 3,
		NIOSH = 4,
		IDLH = 5,
		Portaria_34 = 6,
		OSHA = 7
	}

	[Database("opsa", "RequisitoLegal", "IdRequisitoLegal")]
	public class RequisitoLegal : Ilitera.Data.Table
	{
		private int	_IdRequisitoLegal;
		private string _NomeRequisito = string.Empty;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public RequisitoLegal()
		{		
	
		}

		public override int Id
		{														  
			get{return _IdRequisitoLegal;}
			set	{_IdRequisitoLegal = value;}
		}
		[Obrigatorio(true, "Nome do Requisito é campo obrigatório.")]
		public string NomeRequisito
		{														  
			get{return _NomeRequisito;}
			set	{_NomeRequisito = value;}
		}
	}
}
