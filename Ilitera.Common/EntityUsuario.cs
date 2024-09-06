using System;
using Ilitera.Data;

namespace Ilitera.Common
{
	/// <summary>
	/// Summary description for EntityUsuario.
	/// </summary>
	
	[Database("logdb", "EntityUsuario", "IdEntityUsuario")]
	public class EntityUsuario: Ilitera.Data.Table
	{
		private int _IdEntityUsuario;
		private int _IdUsuario;
		private DateTime _DataCommand;
		private int _IdBaseDados;
		private int _IdTabela;
		private int _IdObject;
		private int _IndCommand;
		private string _Command;
		private string _ProcessoRealizado;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public EntityUsuario()
		{

		}
		public override int Id
		{														  
			get	{return _IdEntityUsuario;}
			set {_IdEntityUsuario = value;}
		}
		public int IdUsuario
		{														  
			get	{return _IdUsuario;}
			set {_IdUsuario = value;}
		}
		public DateTime DataCommand
		{														  
			get	{return _DataCommand;}
			set {_DataCommand = value;}
		}
		public int IdBaseDados
		{														  
			get	{return _IdBaseDados;}
			set {_IdBaseDados = value;}
		}
		public int IdTabela
		{														  
			get	{return _IdTabela;}
			set {_IdTabela = value;}
		}
		public int IdObject
		{														  
			get	{return _IdObject;}
			set {_IdObject = value;}
		}
		public int IndCommand
		{														  
			get	{return _IndCommand;}
			set {_IndCommand = value;}
		}
		public string Command
		{														  
			get	{return _Command;}
			set {_Command = value;}
		}
		public string ProcessoRealizado
		{														  
			get	{return _ProcessoRealizado;}
			set {_ProcessoRealizado = value;}
		}
	}

    [Database("logdb", "Tabela", "IdTabela")]
    public class Tabela : Ilitera.Data.Table
    {
        private int _IdTabela;
        private int _IdBaseDados;
        private string _NomeTabela;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public Tabela()
        {

        }
        public override int Id
        {
            get { return _IdTabela; }
            set { _IdTabela = value; }
        }

        public int IdBaseDados
        {
            get { return _IdBaseDados; }
            set { _IdBaseDados = value; }
        }

        public string NomeTabela
        {
            get { return _NomeTabela; }
            set { _NomeTabela = value; }
        }

    }
}
