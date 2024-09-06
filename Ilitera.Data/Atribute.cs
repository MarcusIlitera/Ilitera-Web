using System;

namespace Ilitera.Data
{
    //[System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.InheritanceDemand, Flags = System.Security.Permissions.SecurityPermissionFlag.UnmanagedCode)]
    //[System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.LinkDemand, Flags = System.Security.Permissions.SecurityPermissionFlag.UnmanagedCode)]

    [AttributeUsage(AttributeTargets.Class, AllowMultiple=false, Inherited=true)]
	public sealed class DatabaseAttribute : Attribute
	{
		private string _name;
		private string _table;
		private string _key = "Id";
		private string _where = string.Empty;
		private bool _identity = false;
		private string _NameProcess = string.Empty;

		public DatabaseAttribute(string Name, string Table)
		{
			_name = Name;
			_table = Table;
		}
		public DatabaseAttribute(string Name, string Table, string Key)
		{
			_name = Name;
			_table = Table;
			_key = Key;
		}
		public DatabaseAttribute(string Name, string Table, string Key, bool Identity)
		{
			_name = Name;
			_table = Table;
			_key = Key;
			_identity = Identity;
		}
		public DatabaseAttribute(string Name, string Table, string Key, string Where)
		{
			_name = Name;
			_table = Table;
			_key = Key;
			_where = Where;
		}
		public DatabaseAttribute(string Name, string Table, string Key, string Where, bool Identity)
		{
			_name = Name;
			_table = Table;
			_key = Key;
			_where = Where;			
			_identity = Identity;
		}
		public DatabaseAttribute(string Name, string Table, string Key, string Where, string NameProcess)
		{
			_name = Name;
			_table = Table;
			_key = Key;
			_where = Where;
			_NameProcess = NameProcess;
		}
		public string Name
		{
            get
            {
                if (_name.ToUpper() == "OPSA")
                {
                    return Ilitera.Data.SQLServer.EntitySQLServer.xDB1;  //Properties.Settings.Default.Db1.ToString();
                }
                else if (_name.ToUpper() == "SIED_NOVO")
                {
                    return Ilitera.Data.SQLServer.EntitySQLServer.xDB2; //return Properties.Settings.Default.Db2.ToString();
                }
                else
                {
                    return _name;
                }
                
            } //{return _name;}  // wagner - para parte da base de dados não ficar estática

		}

        
        public string Table
		{          
            get {return _table;}
		}
		public string Key
		{
			get {return _key;}
		}
		public bool Identity
		{
			get {return _identity;}
		}
		public string Where
		{
			get {return _where;}
		}
		public string NameProcess
		{
			get {return _NameProcess;}
		}
	}

	[AttributeUsage(AttributeTargets.Class, AllowMultiple=false, Inherited=true)]
	public sealed class QueryAttribute : Attribute
	{
		private string _Query;
		private string _Key = "Id";
		private string _Where = String.Empty;
		private string _OrderBy = String.Empty;
		public QueryAttribute(string Query, string Key, string Where, string OrderBy)
		{
			_Query = Query;
			_Key = Key;
			_Where = Where;
			_OrderBy = OrderBy;
		}
		public string Key
		{
			get {return _Key;}
		}
		public string Query
		{
			get {return _Query;}
		}		
		public string Where
		{
			get {return _Where;}
		}
		public string OrderBy
		{
			get {return _OrderBy;}
		}
	}

	[AttributeUsage(AttributeTargets.Property, AllowMultiple=false, Inherited=true)]
	public sealed class ObrigatorioAttribute : Attribute
	{
		private bool _bVal;
		private string _message = String.Empty;

		public ObrigatorioAttribute(bool bVal, string sMessage)
		{
			_bVal = bVal;
			_message = sMessage;
		}
		public ObrigatorioAttribute(bool bVal)
		{
			_bVal = bVal;
		}
		public ObrigatorioAttribute()
		{

		}
		public bool bVal
		{
			get {return _bVal;}
		}
		public string sMessage
		{
			get {return _message;}
		}
	}

	[AttributeUsage(AttributeTargets.Property, AllowMultiple=false, Inherited=true)]
	public sealed class PersistAttribute : Attribute
	{
		private bool _bVal=true;
		public PersistAttribute()
		{
		}
		public PersistAttribute(bool bVal)
		{
			_bVal = bVal;
		}
		public bool bVal
		{
			get {return _bVal;}
		}
	}

	[AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method |AttributeTargets.Property, AllowMultiple=false, Inherited=true)]
	public sealed class PermissaoAttribute : Attribute
	{
		public PermissaoAttribute()
		{

		}
	}
}
