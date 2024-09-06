using System;
using System.Threading;
using System.Collections;
using Ilitera.Data;

namespace Ilitera.Opsa.Data
{
	/// <summary>
	/// Summary description for AgrupamentoDePedido.
	/// </summary>
	[Database("opsa","Agrupamento","IdAgrupamento")]
	public class Agrupamento : Ilitera.Data.Table
	{
		private int _IdAgrupamento;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public Agrupamento()
		{
		}
		public override int Id
		{
			get{return _IdAgrupamento;}
			set{_IdAgrupamento = value;}
		}
	}
}

