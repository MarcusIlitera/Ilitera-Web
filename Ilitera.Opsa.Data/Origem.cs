using System;
using Ilitera.Data;
using System.Collections;

namespace Ilitera.Opsa.Data
{
	[Database("opsa","Origem","IdOrigem")]
	public class Origem : Ilitera.Data.Table 
	{
		private int _IdOrigem;
		private string _Descricao = string.Empty;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public Origem()
		{
		}

		public override int Id
		{
			get{return _IdOrigem;}
			set{_IdOrigem = value;}
		}

		[Obrigatorio(true, "Descrição é campo obrigatório!")]
		public string Descricao
		{
			get{return _Descricao;}
			set{_Descricao = value;}
		}

		public override string ToString()
		{
			if(this.Descricao==string.Empty)
				this.Find();

			return this.Descricao;
		}
	}
}