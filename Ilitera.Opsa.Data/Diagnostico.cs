using System;
using System.Data;
using System.Collections;
using Ilitera.Data;
using Ilitera.Common;


namespace Ilitera.Opsa.Data
{
	[Database("opsa", "Diagnostico", "IdDiagnostico")]
	public class Diagnostico: Ilitera.Data.Table
	{
		private int _IdDiagnostico;
		private string _Nome;


        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public Diagnostico()
		{

		}
		public override int Id
		{
			get{return _IdDiagnostico;}
			set{_IdDiagnostico = value;}
		}
		public string Nome
		{
			get{return _Nome;}
			set{_Nome = value;}
		}
		public override string ToString()
		{
			if(this.mirrorOld==null)
				this.Find();

			return Nome;
		}

	}
}
