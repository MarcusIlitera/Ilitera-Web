using System;
using System.Data;
using System.Data.SqlClient;
using System.Xml;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Diagnostics;

namespace Ilitera.Data.SQLServer
{
    //[System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.InheritanceDemand, Flags = System.Security.Permissions.SecurityPermissionFlag.UnmanagedCode)]
    //[System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.LinkDemand, Flags = System.Security.Permissions.SecurityPermissionFlag.UnmanagedCode)]

    public class EntitySQLServer : EntityBase, IEntity
    {
        private SqlDataAdapter da;

        public static string xDB1 = "";
        public static string xDB2 = "";
        public static string _Database = "";
        private static string _Server = "";
        public static string _LocalServer = "";
        private static string _User = "";
        private static string _Password = "";

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]
        public EntitySQLServer()
        {
        }

        public IDbConnection Conn(Type c)
        {
            SqlConnection cnn = new SqlConnection(GetConnection());
            cnn.Open();
            return cnn;
        }

        //[System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.InheritanceDemand, Flags = System.Security.Permissions.SecurityPermissionFlag.UnmanagedCode)]
        //[System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.LinkDemand, Flags = System.Security.Permissions.SecurityPermissionFlag.UnmanagedCode)]

        public static string GetConnection()
        {
            SqlConnectionStringBuilder cnn = new SqlConnectionStringBuilder();

            //if (Table.IsWeb)
            //{
                cnn.Pooling = true;
                cnn.MaxPoolSize = 500;
                cnn.MinPoolSize = 0;
            //}

            //if (Table.IsOffLine)
            //{
            cnn.DataSource = _LocalServer;
            cnn.IntegratedSecurity = false;
            //cnn.UserID = "sa";
            cnn.UserID = "admin";
            cnn.Password = "Ilitera572160x4";
            //}
            //else
            //{
            //    cnn.DataSource = Server;
            //    cnn.UserID = User;
            //    cnn.Password = Password;
            //}

            cnn.PersistSecurityInfo = false;
            cnn.InitialCatalog = xDB1; //_Database;
            cnn.CurrentLanguage = "us_english";

            cnn.ConnectTimeout = 900;
            return cnn.ConnectionString;
        }


        //[System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.InheritanceDemand, Flags = System.Security.Permissions.SecurityPermissionFlag.UnmanagedCode)]
        //[System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.LinkDemand, Flags = System.Security.Permissions.SecurityPermissionFlag.UnmanagedCode)]

        public static string GetConnection_Sied()
        {
            SqlConnectionStringBuilder cnn = new SqlConnectionStringBuilder();

            //if (Table.IsWeb)
            //{
                cnn.Pooling = true;
                cnn.MaxPoolSize = 500;
                cnn.MinPoolSize = 0;
            //}

            //if (Table.IsOffLine)
            //{
            cnn.DataSource = _LocalServer;
            cnn.IntegratedSecurity = false;
            //cnn.UserID = "sa";
            cnn.UserID = "admin";
            cnn.Password = "Ilitera572160x4";
            
            //}
            //else
            //{
            //    cnn.DataSource = Server;
            //    cnn.UserID = User;
            //    cnn.Password = Password;
            //}

            cnn.PersistSecurityInfo = false;
            cnn.InitialCatalog = xDB2; //"sied_novo";
            cnn.CurrentLanguage = "us_english";

            cnn.ConnectTimeout = 900;

            return cnn.ConnectionString;
        }

        public static string Server
        {
            get { return _Server; }
        }

        public static string Database
        {
            get { return _Database; }
        }

        public static string User
        {
            get { return _User; }
        }

        public static string Password
        {
            get { return _Password; }
        }

        private DataSet ExecuteDataset(string strSQL)
        {
            Debug.WriteLine(strSQL);

            DataSet m_ds;

            m_ds = new DataSet("Result");

            using (SqlConnection cnn = new SqlConnection(GetConnection()))
            {
                cnn.Open();

                da = new SqlDataAdapter(strSQL, cnn);
                da.Fill(m_ds);

                cnn.Close();

                da.Dispose();
            }

            return m_ds;
        }

        private void ExecuteNonQuery(string strSQL)
        {
            using (SqlConnection cnn = new SqlConnection(GetConnection()))
            {
                cnn.Open();

                ExecuteNonQuery(strSQL, cnn, null);

                cnn.Close();
            }
        }

        private void ExecuteNonQuery(string strSQL,
                                        SqlConnection cnn,
                                        SqlTransaction transaction)
        {
            Debug.WriteLine(strSQL);

            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = strSQL;

            if (cnn != null)
                cmd.Connection = cnn;
            else
            {
                cmd.Connection = transaction.Connection;
                cmd.Transaction = transaction;
            }

            cmd.ExecuteNonQuery();
            cmd.Dispose();
        }

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]
        public DataRow Get(Type c, int Id)
        {
            string filter = dbAttribute(c).Table
                            + "." + dbAttribute(c).Key
                            + "=" + Id;

            DataSet ds = Get(c, filter);

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                return ds.Tables[0].Rows[0];
            else
                return null;
        }


        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]
        public DataSet Get(Type c, string filter)
        {
            string sql = ClauseSelect(filter, c);

            DataSet ds = ExecuteDataset(sql);

            return ds;
        }


        public DataSet GetIdNome(Type c, string sNome)
        {
            string strSelect = "USE " + dbAttribute(c).Name
                                + " SELECT " + dbAttribute(c).Table + "."
                                             + dbAttribute(c).Key + " AS Id, "
                                             + sNome + " COLLATE SQL_Latin1_General_Cp1251_CS_AS AS Nome"
                                + " FROM " + ClauseFrom(c)
                                + " ORDER BY " + sNome;

            return ExecuteDataset(strSelect);
        }


        public DataSet GetIdNome(Type c, string sNome, string sWhere)
        {
            return GetIdNome(c, sNome, sWhere, sNome);
        }

        public DataSet GetIdNome(Type c, string sNome, string sWhere, string sOrderBy)
        {
            return this.GetIdNome(c, sNome, sWhere, sOrderBy, false);
        }

        public DataSet GetIdNome(Type c, string sNome, string sWhere, string sOrderBy, bool SemAcento)
        {
            StringBuilder strSelect = new StringBuilder();

            strSelect.Append("USE " + dbAttribute(c).Name
                                + " SELECT " + dbAttribute(c).Table + "."
                                             + dbAttribute(c).Key + " AS Id, ");
            if (SemAcento)
                strSelect.Append(sNome + " COLLATE SQL_Latin1_General_Cp1251_CS_AS AS Nome");
            else
                strSelect.Append(sNome + " AS Nome");

            strSelect.Append(" FROM " + ClauseFrom(c)
                            + " WHERE " + sWhere
                            + " ORDER BY " + sOrderBy);

            return ExecuteDataset(strSelect.ToString());
        }

        public DataSet GetIn(Type c, string sOrder, Type typeIn, bool IN, string filter)
        {
            DataSet dsRet = Get(typeIn, filter);
            DataRow[] dr = null;

            if (dsRet.Tables.Count > 0)
                dr = dsRet.Tables[0].Select();

            StringBuilder str = new StringBuilder();

            if (dr != null && dr.Length > 0)
            {
                str.Append(dbAttribute(typeIn).Key);

                if (IN)
                    str.Append(" IN (");
                else
                    str.Append(" NOT IN (");

                foreach (DataRow row in dr)
                    str.Append(row[dbAttribute(typeIn).Key] + ",");

                str.Remove(str.Length - 1, 1);
                str.Append(")");
            }

            DataSet ds = new DataSet("Result");
            DataRow[] dr_c;

            if (str.ToString() != string.Empty)
            {
                dr_c = Get(c, str.ToString()).Tables[0].Select();
                ds.Merge(dr_c);
            }
            return ds;
        }


        public DataSet GetAll(Type c)
        {
            return Get(c, ClauseWhereAll(c));
        }

        public DataSet GetAll(Type c, string sOrder)
        {
            return Get(c, ClauseWhereAll(c) + " ORDER BY " + sOrder);
        }

        public void Find(Object o, Type c, int Id)
        {
            DataRow r = Get(c, Id);

            if (r != null)
            {
                Popular(o, c, r);
            }
            else
            {
                Inicialize(o, c);
            }
        }

        public ArrayList Find(Object o, Type c, string filter)
        {
            DataSet ds = Get(c, filter);

            ArrayList list = new ArrayList();

            if (ds.Tables[0].Rows.Count == 1)
            {
                Popular(o, c, ds.Tables[0].Rows[0]);
                list.Add(o);
                return list;
            }
            else
                Inicialize(o, c);

            foreach (DataRow r in ds.Tables[0].Rows)
            {
                Ilitera.Data.Table obj = (Ilitera.Data.Table)Activator.CreateInstance(c);
                Popular(obj, c, r);
                list.Add(obj);
            }

            return list;
        }


        public ArrayList FindIn(Object o, Type c, string sOrder, Type typeIn, bool IN, string filter)
        {
            ArrayList list = new ArrayList();
            DataSet ds;
            DataRow[] dr = null;

            ds = GetIn(c, sOrder, typeIn, IN, filter);

            if (ds.Tables.Count > 0)
                dr = ds.Tables[0].Select();

            if (dr != null && dr.Length == 1)
            {
                if (c.BaseType != typeof(Table))
                    Find(o, c.BaseType, Convert.ToInt32(dr[0][dbAttribute(c).Key]));

                Popular(o, c, dr[0]);
                list.Add(o);

                return list;
            }
            else
            {
                Inicialize(o, c);
            }
            if (dr != null)
            {
                foreach (DataRow r in dr)
                {
                    Ilitera.Data.Table obj = (Ilitera.Data.Table)Activator.CreateInstance(c);
                    Popular(obj, c, r);
                    list.Add(obj);
                }
            }
            return list;
        }


        public ArrayList FindAll(Object o, Type c)
        {
            return Find(o, c, ClauseWhereAll(c));
        }

        public ArrayList FindMax(Object o, Type c, string Column, string Condition)
        {
            if (c.BaseType != typeof(Table))
                throw new Exception("FindMax não pode ser usado em classes que são extendidas!");

            StringBuilder sWhere = new StringBuilder();

            sWhere.Append(Condition);
            sWhere.Append(" AND ");
            sWhere.Append(Column);
            sWhere.Append("= (SELECT MAX(");
            sWhere.Append(Column);
            sWhere.Append(") FROM ");
            sWhere.Append(dbAttribute(c).Name);
            sWhere.Append(".dbo.");
            sWhere.Append(dbAttribute(c).Table);
            sWhere.Append(" WHERE ");
            sWhere.Append(Condition);
            sWhere.Append(")");

            return Find(o, c, sWhere.ToString());
        }

        public ArrayList FindMin(Object o, Type c, string Column, string Condition)
        {
            if (c.BaseType != typeof(Table))
                throw new Exception("FindMin não pode ser usado!");

            StringBuilder sWhere = new StringBuilder();

            sWhere.Append(Condition);
            sWhere.Append(" AND ");
            sWhere.Append(Column);
            sWhere.Append("= (SELECT MIN(");
            sWhere.Append(Column);
            sWhere.Append(") FROM ");
            sWhere.Append(dbAttribute(c).Name);
            sWhere.Append(".dbo.");
            sWhere.Append(dbAttribute(c).Table);
            sWhere.Append(" WHERE ");
            sWhere.Append(Condition);
            sWhere.Append(")");

            return Find(o, c, sWhere.ToString());
        }

        public void Save(Object o,
                        Type c,
                        DataSet ds,
                        int IdUsuario,
                        string ProcessoRealizado)
        {
            da.Update(ds);
        }

        public void Save(Object o, Type c, DataSet ds, int IdUsuario)
        {
            this.Save(o, c, ds, IdUsuario, string.Empty);
        }

        public int Save(Object o,
                        Type c,
                        int Id,
                        int IdUsuario,
                        string ProcessoRealizado)
        {
            using (SqlConnection cnn = new SqlConnection(GetConnection()))
            {
                cnn.Open();

                if (c.BaseType.ToString() == "Ilitera.Data.Table")
                {
                    return this.Save(o, c, Id, IdUsuario, cnn, null, ProcessoRealizado);
                }
                else
                {
                    SqlTransaction transaction
                        = cnn.BeginTransaction(IsolationLevel.ReadCommitted);
                    try
                    {
                        Debug.WriteLine("");
                        Debug.WriteLine(@"\\\\\\\\\\\\\\\\\\\  Begin Transaction \\\\\\\\\\\\\\\\\");

                        Id = this.Save(o, c, Id, IdUsuario, null, transaction, ProcessoRealizado);

                        transaction.Commit();

                        Debug.WriteLine(@"\\\\\\\\\\\\\\\\\\\  Commit Transaction \\\\\\\\\\\\\\\\\");
                        Debug.WriteLine("");

                    }
                    catch (SqlException ex)
                    {
                        transaction.Rollback();

                        Debug.WriteLine(@"\\\\\\\\\\\\\\\\\\\  Rollback Transaction \\\\\\\\\\\\\\\\\");
                        Debug.WriteLine("");

                        throw ex;
                    }
                }
            }
            return Id;
        }

        public int Save(Object o,
                        Type c,
                        int Id,
                        int IdUsuario,
                        IDbTransaction trans,
                        string ProcessoRealizado)
        {
            return Save(o, c, Id, IdUsuario, null, (SqlTransaction)trans, ProcessoRealizado);
        }

        public int Save(Object o,
                        Type c,
                        int Id,
                        int IdUsuario,
                        IDbTransaction trans)
        {
            return this.Save(o, c, Id, IdUsuario, (SqlTransaction)trans, string.Empty);
        }

        public int Save(Object o,
                        Type c,
                        int Id,
                        int IdUsuario,
                        SqlConnection connection,
                        SqlTransaction transaction,
                        string ProcessoRealizado)
        {
            if (Id != 0)
                Update(o, c, Id, IdUsuario, connection, transaction, ProcessoRealizado);
            else
                Id = Insert(o, c, IdUsuario, connection, transaction, ProcessoRealizado);

            return Id;
        }

        private int Insert(Object o,
                            Type c,
                            int IdUsuario,
                            SqlConnection connection,
                            SqlTransaction transaction,
                            string ProcessoRealizado)
        {
            bool hasSuper = false;

            int IdFK = 0;

            if (c.BaseType != typeof(Table)) hasSuper = true;

            if (hasSuper)
                IdFK = Insert(o, c.BaseType, IdUsuario, connection, transaction, string.Empty);

            PropertyInfo[] properties = c.GetProperties(BindingFlags.Public
                                                    | BindingFlags.Instance
                                                    | BindingFlags.DeclaredOnly);

            StringBuilder sColumn = new StringBuilder();

            StringBuilder sValue = new StringBuilder();

            foreach (PropertyInfo propInfo in properties)
            {
                PersistAttribute pa = (PersistAttribute)Attribute.GetCustomAttribute(propInfo, typeof(PersistAttribute));

                if ((pa == null || pa.bVal) && propInfo.CanWrite)
                {
                    if ((propInfo.Name == "Id"))
                    {
                        if (!dbAttribute(c).Identity)
                        {
                            if (IdFK == 0) //Faz com que a classe pai tenha o mesmo Id que a filha
                                IdFK = new Random().Next(int.MinValue, int.MaxValue);

                            sValue.Append(", " + IdFK.ToString());
                            sColumn.Append(", " + dbAttribute(c).Key);
                        }
                        if (hasSuper)
                        {
                            sValue.Append(", " + IdFK.ToString());
                            sColumn.Append(", " + dbAttribute(c.BaseType).Key);
                        }
                    }
                    else
                    {
                        string sType = propInfo.PropertyType.ToString();

                        if (Table.IsTable(propInfo.PropertyType))
                        {
                            Type obj = propInfo.PropertyType;

                            Table idType = (Table)c.InvokeMember(propInfo.Name, BindingFlags.GetProperty, null, o, new Object[] { });

                            int id = 0;

                            if (idType != null)
                                id = (int)obj.InvokeMember("Id", BindingFlags.GetProperty, null, idType, new Object[] { });

                            if (id == 0 && propInfo.Name != "nID_RSC")
                                sValue.Append(", " + " NULL ");
                            else
                                sValue.Append(", " + id.ToString());
                        }
                        else if (sType == "System.Single" || sType == "System.Decimal" || sType == "System.Double")
                        {
                            sValue.Append(", " + (c.InvokeMember(propInfo.Name, BindingFlags.GetProperty, null, o, new Object[] { })).ToString().Replace(",", "."));
                        }
                        else if (sType == "System.String")
                        {
                            string sVal = string.Empty;

                            if (c.InvokeMember(propInfo.Name, BindingFlags.GetProperty, null, o, new Object[] { }) != null)
                                sVal = (c.InvokeMember(propInfo.Name, BindingFlags.GetProperty, null, o, new Object[] { })).ToString().Replace("'", "''");

                            sValue.Append(", " + "'" + sVal.Trim() + "'");
                        }
                        else if (sType == "System.DateTime")
                        {
                            DateTime sVal = ((System.DateTime)(c.InvokeMember(propInfo.Name, BindingFlags.GetProperty, null, o, new Object[0])));

                            if (sVal == new DateTime() || sVal == new DateTime(1753, 1, 1))
                                sValue.Append(", NULL ");
                            else
                                sValue.Append(", " + "'" + sVal.ToString("yyyy-MM-dd HH:mm:ss") + "'");
                        }
                        else if (sType == "System.TimeSpan")
                        {
                            TimeSpan sVal = ((System.TimeSpan)(c.InvokeMember(propInfo.Name, BindingFlags.GetProperty, null, o, new Object[0])));

                            sValue.Append(", " + sVal.Ticks);
                        }
                        else if (sType == "System.Boolean")
                        {
                            sValue.Append(", " + System.Convert.ToInt16((c.InvokeMember(propInfo.Name, BindingFlags.GetProperty, null, o, new Object[] { }))).ToString());
                        }
                        else if (propInfo.PropertyType.BaseType == typeof(Enum))
                        {
                            sValue.Append(", " + (int)(c.InvokeMember(propInfo.Name, BindingFlags.GetProperty, null, o, new Object[] { })));
                        }
                        else
                        {
                            sValue.Append(", " + (c.InvokeMember(propInfo.Name, BindingFlags.GetProperty, null, o, new Object[] { })).ToString());
                        }

                        sColumn.Append(", " + propInfo.Name);
                    }
                }
            }

            string sqlCmd = "USE " + dbAttribute(c).Name
                            + " INSERT INTO " + dbAttribute(c).Table
                            + "(" + sColumn.ToString().Substring(2)
                            + ") VALUES (" + sValue.ToString().Substring(2)
                            + ")";
            try
            {
                ExecuteNonQuery(sqlCmd, connection, transaction);
            }
            catch (SqlException ex)
            {
                Debug.WriteLine(ex.Message);

                if (ex.Message.IndexOf("PRIMARY KEY") >= 1 && !dbAttribute(c).Identity)
                    IdFK = Insert(o, c, IdUsuario, connection, transaction, ProcessoRealizado);
                else
                    throw ex;
            }

            //Comentando linha de chamada da tabela de log
            //21/08/2010
            Log(c, sqlCmd, IndCommands.Insert, IdFK, IdUsuario, connection, transaction, ProcessoRealizado);

            return IdFK;
        }


        private int Update(Object o,
                            Type c,
                            int Id,
                            int IdUsuario,
                            SqlConnection connection,
                            SqlTransaction transaction,
                            string ProcessoRealizado)
        {
            bool alterado = false;

            PropertyInfo[] properties = c.GetProperties(BindingFlags.Public
                                                        | BindingFlags.Instance
                                                        | BindingFlags.DeclaredOnly);

            StringBuilder sColumn = new StringBuilder();

            string sKey = "";

            if (c.BaseType != typeof(Table))
                Update(o, c.BaseType, Id, IdUsuario, connection, transaction, ProcessoRealizado);

            foreach (PropertyInfo propInfo in properties)
            {
                PersistAttribute pa
                    = (PersistAttribute)Attribute.GetCustomAttribute(propInfo,
                                typeof(PersistAttribute));

                if ((pa == null || pa.bVal) && propInfo.CanWrite)
                {
                    if (propInfo.Name == "Id")
                    {
                        sKey = dbAttribute(c).Key + "=" + Id.ToString();
                    }
                    else
                    {
                        //Só atualiza se o valor da coluna for diferente
                        if (((Table)o).mirrorOld != null && ((Table)o).EqualsProperty(c, propInfo))
                            continue;

                        alterado = true;

                        string sValue = "";

                        string sType = propInfo.PropertyType.ToString();

                        string sTypeBase = propInfo.PropertyType.BaseType.ToString();

                        if (Table.IsTable(propInfo.PropertyType))
                        {
                            Type obj = propInfo.PropertyType;

                            Table idType = (Table)c.InvokeMember(propInfo.Name,
                                                                BindingFlags.GetProperty,
                                                                null,
                                                                o,
                                                                new Object[] { });

                            int idMember = (int)(obj.InvokeMember("Id",
                                                                BindingFlags.GetProperty,
                                                                null,
                                                                idType,
                                                                new Object[] { }));

                            if (idMember == 0 && propInfo.Name != "nID_RSC")//Riscos
                                sValue = (" NULL ");
                            else
                                sValue = idMember.ToString();
                        }
                        else if (sType == "System.Single" || sType == "System.Decimal" || sType == "System.Double")
                        {
                            sValue = (c.InvokeMember(propInfo.Name, BindingFlags.GetProperty, null, o, new Object[] { })).ToString().Replace(",", ".");
                        }
                        else if (sType == "System.String")
                        {
                            string sVal = string.Empty;

                            if (c.InvokeMember(propInfo.Name, BindingFlags.GetProperty, null, o, new Object[] { }) != null)
                                sVal = (c.InvokeMember(propInfo.Name, BindingFlags.GetProperty, null, o, new Object[] { })).ToString();

                            sValue = "'" + sVal.Replace("'", "''").Trim() + "'";
                        }
                        else if (sType == "System.DateTime")
                        {
                            DateTime sVal = ((System.DateTime)(c.InvokeMember(propInfo.Name, BindingFlags.GetProperty, null, o, new Object[0])));

                            if (sVal == new DateTime() || sVal == new DateTime(1753, 1, 1))
                                sValue = " NULL ";
                            else
                                sValue = "'" + sVal.ToString("yyyy-MM-dd HH:mm:ss") + "'";
                        }
                        else if (sType == "System.Boolean")
                        {
                            sValue = System.Convert.ToInt16((c.InvokeMember(propInfo.Name, BindingFlags.GetProperty, null, o, new Object[] { }))).ToString();
                        }
                        else if (sTypeBase == "System.Enum")
                        {
                            sValue = ((int)(c.InvokeMember(propInfo.Name, BindingFlags.GetProperty, null, o, new Object[] { }))).ToString();
                        }
                        else
                        {
                            sValue = (c.InvokeMember(propInfo.Name, BindingFlags.GetProperty, null, o, new Object[] { })).ToString();
                        }

                        sColumn.Append(propInfo.Name + "=" + sValue + ", ");
                    }
                }
            }

            if (alterado && properties.Length > 1)//A primeira propriedade é o Id
            {
                string sqlCmd = "USE " + dbAttribute(c).Name
                              + " UPDATE " + dbAttribute(c).Table
                              + " SET " + sColumn.ToString().Substring(0, sColumn.Length - 2)
                              + " WHERE " + sKey;

                ExecuteNonQuery(sqlCmd, connection, transaction);

                Log(c, sqlCmd, IndCommands.Update, Id, IdUsuario, connection, transaction, ProcessoRealizado);
            }

            return Id;
        }

        public void Delete(Type c,
                            int Id,
                            int IdUsuario,
                            string ProcessoRealizado)
        {
            using (SqlConnection cnn = new SqlConnection(GetConnection()))
            {
                cnn.Open();

                SqlTransaction transaction
                    = cnn.BeginTransaction(IsolationLevel.ReadCommitted);

                Debug.WriteLine("");
                Debug.WriteLine(@"\\\\\\\\\\\\\\\\\\\  Begin Transaction \\\\\\\\\\\\\\\\\");

                try
                {
                    DeleteCommand(c, Id, IdUsuario, transaction, ProcessoRealizado);

                    transaction.Commit();

                    Debug.WriteLine(@"\\\\\\\\\\\\\\\\\\\  Commit Transaction \\\\\\\\\\\\\\\\\");
                    Debug.WriteLine("");

                }
                catch (SqlException ex)
                {
                    transaction.Rollback();

                    Debug.WriteLine(@"\\\\\\\\\\\\\\\\\\\  Rollback Transaction \\\\\\\\\\\\\\\\\");
                    Debug.WriteLine("");

                    throw ex;
                }
            }
        }

        public void Delete(Type c,
                            int Id,
                            int IdUsuario,
                            IDbTransaction trans,
                            string ProcessoRealizado)
        {
            DeleteCommand(c, Id, IdUsuario, (SqlTransaction)trans, ProcessoRealizado);
        }

        private void DeleteCommand(Type c,
                                    int Id,
                                    int IdUsuario,
                                    SqlTransaction transaction,
                                    string ProcessoRealizado)
        {
            string sKey = dbAttribute(c).Key + "=" + Id.ToString();

            string sqlCmd = "USE " + dbAttribute(c).Name
                                + " DELETE " + dbAttribute(c).Table
                                + " WHERE " + sKey;

            ExecuteNonQuery(sqlCmd, null, transaction);

            if (c.BaseType != typeof(Table))
                DeleteCommand(c.BaseType, Id, 0, transaction, ProcessoRealizado);

            Log(c, sqlCmd, IndCommands.Delete, Id, IdUsuario, null, transaction, ProcessoRealizado);
        }


        public void Delete(Type c,
                            string where,
                            int IdUsuario,
                            string ProcessoRealizado)
        {
            if (c.BaseType != typeof(Table))
            {
                Delete(c.BaseType, where, ClauseFrom(c), 0, string.Empty);
            }
            else
            {
                string sqlCmd = "USE " + dbAttribute(c).Name
                                + " DELETE " + dbAttribute(c).Table
                                + " WHERE " + where;

                ExecuteNonQuery(sqlCmd);

                using (SqlConnection cnn = new SqlConnection(GetConnection()))
                {
                    cnn.Open();
                    Log(c, sqlCmd, IndCommands.Delete, 0, IdUsuario, cnn, null, ProcessoRealizado);
                }
            }
        }

        private void Delete(Type c,
                            string where,
                            string from,
                            int IdUsuario,
                            string ProcessoRealizado)
        {
            if (c.BaseType != typeof(Table))
            {
                Delete(c.BaseType, where, from, 0, string.Empty);
            }
            else
            {
                string sqlCmd = "USE " + dbAttribute(c).Name
                    + " DELETE " + dbAttribute(c).Table
                    + " WHERE " + dbAttribute(c).Key
                    + " IN (SELECT " + dbAttribute(c).Table
                    + "." + dbAttribute(c).Key
                    + " FROM " + from
                    + " WHERE " + where + ")";

                ExecuteNonQuery(sqlCmd);

                using (SqlConnection cnn = new SqlConnection(GetConnection()))
                {
                    cnn.Open();
                    Log(c, sqlCmd, IndCommands.Delete, 0, IdUsuario, cnn, null, ProcessoRealizado);
                }
            }
        }

        public DataSet ExecuteDataset(Type c, string sql)
        {
            return ExecuteDataset(sql);
        }

        private void Log(Type c,
                            string Command,
                            IndCommands IndCommand,
                            int Id,
                            int IdUsuario,
                            SqlConnection cnn,
                            SqlTransaction transaction,
                            string ProcessoRealizado)
        {

            if (IdUsuario == 0 || c.FullName == "Ilitera.Data.EntityUsuario")
                return;

            string strProcessoRealizado = string.Empty;

            if (ProcessoRealizado != string.Empty)
            {
                strProcessoRealizado = ProcessoRealizado;
            }
            else if (dbAttribute(c).NameProcess != string.Empty)
            {
                if (IndCommand == IndCommands.Delete)
                    strProcessoRealizado = "Exclusão do cadastro de " + dbAttribute(c).NameProcess;
                else if (IndCommand == IndCommands.Insert)
                    strProcessoRealizado = "Cadastro de " + dbAttribute(c).NameProcess;
                else if (IndCommand == IndCommands.Update)
                    strProcessoRealizado = "Edição do cadastro de " + dbAttribute(c).NameProcess;
            }

            string strLog = "USE logdb exec dbo.sps_AddLog_" + dbAttribute(c).Name + " "
                            + IdUsuario + ","
                            + "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',"
                            + "'" + dbAttribute(c).Name + "',"
                            + "'" + dbAttribute(c).Table + "',"
                            + Id + ","
                            + (int)IndCommand + ","
                            + "'" + Command.Replace("'", "''") + "',"
                            + "'" + strProcessoRealizado + "  Web'";

            try
            {
                //Debug.WriteLine(strLog);

                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = strLog;

                if (cnn != null)
                    cmd.Connection = cnn;
                else
                {
                    cmd.Connection = transaction.Connection;
                    cmd.Transaction = transaction;
                }

                cmd.ExecuteNonQuery(); //DESENVOLVIMENTO ILITERA - linha ao lado comentada (acessava tab de logs) 29/07/10
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\\\\\\\\\\\\\\\\\\\  ERROR LOG \\\\\\\\\\\\\\\\\");
                Debug.WriteLine(ex.Message);
                Debug.WriteLine(@"\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\");
            }
        }

        public int ExecuteCount(Type c, string where)
        {
            string sqlCmd = "USE " + dbAttribute(c).Name
                + " SELECT COUNT(*) FROM " + ClauseFrom(c)
                + " WHERE " + where;

            Debug.WriteLine(sqlCmd);

            using (SqlConnection cnn = new SqlConnection(GetConnection()))
            using (SqlCommand cmd = new SqlCommand(sqlCmd, cnn))
            {
                cnn.Open();
                cmd.CommandType = CommandType.Text;

                //Data: 12/09/2010 - Gambi para nao dar erro de logdb
                //O sistema pergunta se o comando existe o logdb, se existir ele nao
                //executa o métdoo
                if (!sqlCmd.Contains("logdb"))
                {
                    return (int)cmd.ExecuteScalar();
                }
                else
                {
                    return 0;
                }
            }
        }

        public string ExecuteScalar(Type c, string sqlCmd)
        {
            Debug.WriteLine(sqlCmd);

            using (SqlConnection cnn = new SqlConnection(GetConnection()))
            using (SqlCommand cmd = new SqlCommand(sqlCmd, cnn))
            {
                cnn.Open();
                cmd.CommandType = CommandType.Text;

                return Convert.ToString(cmd.ExecuteScalar());
            }
        }
    }
}

