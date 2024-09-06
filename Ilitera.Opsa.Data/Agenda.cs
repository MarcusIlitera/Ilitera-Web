using System;
using System.Data;
using System.Data.OleDb;
using System.Collections;
using Ilitera.Data;
using Ilitera.Common;

namespace Ilitera.Opsa.Data
{
	[Database("opsa", "Agenda", "IdAgenda", "", "Agenda PABX")]
	public class Agenda: Ilitera.Data.Table
	{
		private int _IdAgenda;
		private string _Nome;
		private string _Numero;
		private bool _IsInterno;


        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public Agenda()
		{
		}
		public override int Id
		{
            get { return _IdAgenda; }
            set { _IdAgenda = value; }
		}
        public string Nome
		{
            get { return _Nome; }
            set { _Nome = value; }
		}
        public string Numero
		{
            get { return _Numero; }
            set { _Numero = value; }
		}
        public bool IsInterno
		{
            get { return _IsInterno; }
            set { _IsInterno = value; }
		}

        public static void AtualizaAgendaPABX()
        {

        }
	}
}
