using System;
using System.Data;
using System.Data.SqlClient;
using System.Xml;
using System.Collections;
using System.Reflection;
using System.Text;

namespace Ilitera.Data
{
    //[System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.InheritanceDemand, Flags = System.Security.Permissions.SecurityPermissionFlag.UnmanagedCode)]
    //[System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.LinkDemand, Flags = System.Security.Permissions.SecurityPermissionFlag.UnmanagedCode)]

    public class EntityBase
    {
        #region IndCommands

        public enum IndCommands : int
        {
            Select,
            Insert,
            Update,
            Delete
        }
        #endregion

        #region Contructor
        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]
        public EntityBase()
        {

        }
        #endregion

        #region dbAttribute
        protected DatabaseAttribute dbAttribute(Type c)
        {
            return (DatabaseAttribute)Attribute.GetCustomAttribute(c, typeof(DatabaseAttribute));
        }
        #endregion

        #region ConvertDataReaderToDataSet

        public static DataSet ConvertDataReaderToDataSet(SqlDataReader reader)
        {
            DataSet dataSet = new DataSet("Result");
            do
            {
                // Create new data table

                DataTable schemaTable = reader.GetSchemaTable();
                DataTable dataTable = new DataTable();

                if (schemaTable != null)
                {
                    // A query returning records was executed

                    for (int i = 0; i < schemaTable.Rows.Count; i++)
                    {
                        DataRow dataRow = schemaTable.Rows[i];
                        // Create a column name that is unique in the data table
                        string columnName = (string)dataRow["ColumnName"]; //+ "<C" + i + "/>";
                        // Add the column definition to the data table
                        DataColumn column = new DataColumn(columnName, (Type)dataRow["DataType"]);
                        dataTable.Columns.Add(column);
                    }

                    dataSet.Tables.Add(dataTable);

                    // Fill the data table we just created

                    while (reader.Read())
                    {
                        DataRow dataRow = dataTable.NewRow();

                        for (int i = 0; i < reader.FieldCount; i++)
                            dataRow[i] = reader.GetValue(i);

                        dataTable.Rows.Add(dataRow);
                    }
                }
                else
                {
                    // No records were returned

                    DataColumn column = new DataColumn("RowsAffected");
                    dataTable.Columns.Add(column);
                    dataSet.Tables.Add(dataTable);
                    DataRow dataRow = dataTable.NewRow();
                    dataRow[0] = reader.RecordsAffected;
                    dataTable.Rows.Add(dataRow);
                }
            }
            while (reader.NextResult());
            return dataSet;
        }
        #endregion

        #region Inicialize

        public virtual void Inicialize(Object o, Type c)
        {
            if (c.BaseType != typeof(Table))
                Inicialize(o, c.BaseType);

            c.InvokeMember("Id", BindingFlags.SetProperty, null, o, new Object[] { 0 });

            PropertyInfo[] properties = c.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);

            foreach (PropertyInfo propInfo in properties)
            {
                bool CanWrite = propInfo.CanWrite;
                PersistAttribute pa = (PersistAttribute)Attribute.GetCustomAttribute(propInfo, typeof(PersistAttribute));

                if (!CanWrite || (pa != null && !pa.bVal))
                    continue;

                if (Table.IsTable(propInfo.PropertyType))
                {
                    Type obj = propInfo.PropertyType;

                    Object idType = Activator.CreateInstance(obj);

                    c.InvokeMember(propInfo.Name, BindingFlags.SetProperty, null, o, new Object[] { idType });
                }
                else if (propInfo.PropertyType == typeof(string))
                {
                    string sVal = (string)c.InvokeMember(propInfo.Name, BindingFlags.GetProperty, null, o, new Object[] { });

                    if (sVal == null)
                        c.InvokeMember(propInfo.Name, BindingFlags.SetProperty, null, o, new Object[] { string.Empty });
                }
            }
        }
        #endregion

        #region Popular

        public virtual void Popular(Object o, Type c, DataRow r)
        {
            PropertyInfo[] properties = c.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (PropertyInfo propInfo in properties)
            {
                string sType;
                string sTypeBase;

                sType = propInfo.PropertyType.ToString();

                if (propInfo.PropertyType.IsInterface)
                    sTypeBase = sType;
                else
                    sTypeBase = propInfo.PropertyType.BaseType.ToString();

                PersistAttribute pa = (PersistAttribute)Attribute.GetCustomAttribute(propInfo, typeof(PersistAttribute));

                try
                {
                    if ((pa == null || pa.bVal) && propInfo.CanWrite)
                    {
                        if (propInfo.Name == "Id")
                            c.InvokeMember("Id", BindingFlags.SetProperty, null, o, new Object[] { Convert.ToInt32(r[dbAttribute(c).Key]) });
                        else
                        {
                            if (r[propInfo.Name] != System.DBNull.Value)
                            {
                                if (Table.IsTable(propInfo.PropertyType))
                                {
                                    int idColumn = Convert.ToInt32(r[propInfo.Name]);

                                    Type obj = propInfo.PropertyType;

                                    Object idType = Activator.CreateInstance(obj);

                                    obj.InvokeMember("Id", BindingFlags.SetProperty, null, idType, new Object[] { idColumn });

                                    c.InvokeMember(propInfo.Name, BindingFlags.SetProperty, null, o, new Object[] { idType });
                                }
                                else if (sType == "System.String")
                                    c.InvokeMember(propInfo.Name, BindingFlags.SetProperty, null, o, new Object[] { r[propInfo.Name].ToString() });
                                else if (sType == "System.Char")
                                    c.InvokeMember(propInfo.Name, BindingFlags.SetProperty, null, o, new Object[] { Convert.ToChar(r[propInfo.Name]) });
                                else if (sType == "System.Byte")
                                    c.InvokeMember(propInfo.Name, BindingFlags.SetProperty, null, o, new Object[] { Convert.ToByte(r[propInfo.Name]) });
                                else if (sType == "System.SByte")
                                    c.InvokeMember(propInfo.Name, BindingFlags.SetProperty, null, o, new Object[] { Convert.ToSByte(r[propInfo.Name]) });
                                else if (sType == "System.Boolean")
                                    c.InvokeMember(propInfo.Name, BindingFlags.SetProperty, null, o, new Object[] { Convert.ToBoolean(Convert.ToInt16(r[propInfo.Name])) });
                                else if (sType == "System.Single")
                                    c.InvokeMember(propInfo.Name, BindingFlags.SetProperty, null, o, new Object[] { Convert.ToSingle(r[propInfo.Name]) });
                                else if (sType == "System.Double")
                                    c.InvokeMember(propInfo.Name, BindingFlags.SetProperty, null, o, new Object[] { Convert.ToDouble(r[propInfo.Name]) });
                                else if (sType == "System.Decimal")
                                    c.InvokeMember(propInfo.Name, BindingFlags.SetProperty, null, o, new Object[] { Convert.ToDecimal(r[propInfo.Name]) });
                                else if (sType == "System.Int16")
                                    c.InvokeMember(propInfo.Name, BindingFlags.SetProperty, null, o, new Object[] { Convert.ToInt16(r[propInfo.Name]) });
                                else if (sType == "System.Int32")
                                    c.InvokeMember(propInfo.Name, BindingFlags.SetProperty, null, o, new Object[] { Convert.ToInt32(r[propInfo.Name]) });
                                else if (sType == "System.Int64")
                                    c.InvokeMember(propInfo.Name, BindingFlags.SetProperty, null, o, new Object[] { Convert.ToInt64(r[propInfo.Name]) });
                                else if (sType == "System.UInt16")
                                    c.InvokeMember(propInfo.Name, BindingFlags.SetProperty, null, o, new Object[] { Convert.ToUInt16(r[propInfo.Name]) });
                                else if (sType == "System.UInt32")
                                    c.InvokeMember(propInfo.Name, BindingFlags.SetProperty, null, o, new Object[] { Convert.ToUInt32(r[propInfo.Name]) });
                                else if (sType == "System.UInt64")
                                    c.InvokeMember(propInfo.Name, BindingFlags.SetProperty, null, o, new Object[] { Convert.ToUInt64(r[propInfo.Name]) });
                                else if (sType == "System.DateTime")
                                    c.InvokeMember(propInfo.Name, BindingFlags.SetProperty, null, o, new Object[] { Convert.ToDateTime(r[propInfo.Name]) });
                                else if (sType == "System.TimeSpan")
                                    c.InvokeMember(propInfo.Name, BindingFlags.SetProperty, null, o, new Object[] { new TimeSpan(Convert.ToInt64(r[propInfo.Name])) });
                                else if (sTypeBase == "System.Enum")
                                    c.InvokeMember(propInfo.Name, BindingFlags.SetProperty, null, o, new Object[] { Enum.ToObject(propInfo.PropertyType, Convert.ToInt32(r[propInfo.Name])) });
                                else
                                    c.InvokeMember(propInfo.Name, BindingFlags.SetProperty, null, o, new Object[] { r[propInfo.Name].ToString() });
                            }
                            else
                            {
                                if (sTypeBase.IndexOf("System") == -1)
                                {
                                    Type obj = ((PropertyInfo)propInfo).PropertyType;

                                    Object idType = Activator.CreateInstance(obj);

                                    c.InvokeMember(propInfo.Name, BindingFlags.SetProperty, null, o, new Object[] { idType });
                                }
                            }
                        }
                    }
                }
                catch
                {
                    if (sTypeBase.IndexOf("System") == -1)
                    {
                        Type obj = ((PropertyInfo)propInfo).PropertyType;

                        Object idType = Activator.CreateInstance(obj);

                        c.InvokeMember(propInfo.Name, BindingFlags.SetProperty, null, o, new Object[] { idType });
                    }
                }
            }
        }
        #endregion

        #region Validade

        public void Validate(Object o, Type c)
        {
            ObrigatorioAttribute obrigatorio;

            if (c.BaseType != typeof(Table))
                Validate(o, c.BaseType);

            PropertyInfo[] properties = c.GetProperties(BindingFlags.Public
                                                | BindingFlags.Instance
                                                | BindingFlags.DeclaredOnly);

            foreach (PropertyInfo propInfo in properties)
            {
                obrigatorio = (ObrigatorioAttribute)Attribute.GetCustomAttribute(propInfo, typeof(ObrigatorioAttribute));

                if (obrigatorio != null && (!(propInfo.Name == "Id") && obrigatorio.bVal))
                {
                    string sType = propInfo.PropertyType.ToString();
                    string sTypeBase = propInfo.PropertyType.BaseType.ToString();

                    if (Table.IsTable(propInfo.PropertyType))
                    {
                        Type obj = propInfo.PropertyType;

                        Table idType = (Table)c.InvokeMember(propInfo.Name, BindingFlags.GetProperty, null, o, new Object[] { });

                        int id = (int)obj.InvokeMember("Id", BindingFlags.GetProperty, null, idType, new Object[] { });

                        if (id == 0)
                            throw new Exception(obrigatorio.sMessage);
                    }
                    else if (sType == "System.String")
                    {
                        string sVal = (c.InvokeMember(propInfo.Name, BindingFlags.GetProperty, null, o, new Object[] { })).ToString();

                        if (sVal == String.Empty || sVal == null)
                            throw new Exception(obrigatorio.sMessage);
                    }
                    else if (sType == "System.DateTime")
                    {
                        DateTime sVal = ((System.DateTime)(c.InvokeMember(propInfo.Name, BindingFlags.GetProperty, null, o, new Object[0])));

                        if (sVal == new DateTime() || sVal == new DateTime(1753, 1, 1))
                            throw new Exception(obrigatorio.sMessage);
                    }
                    else if (sType == "System.Char")
                    {

                    }
                    else if (sType == "System.Byte")
                    {
                        if ((System.Byte)(c.InvokeMember(propInfo.Name, BindingFlags.GetProperty, null, o, new Object[] { })) == 0)
                            throw new Exception(obrigatorio.sMessage);
                    }
                    else if (sType == "System.SByte")
                    {
                        if ((System.Single)(c.InvokeMember(propInfo.Name, BindingFlags.GetProperty, null, o, new Object[] { })) == 0)
                            throw new Exception(obrigatorio.sMessage);
                    }
                    else if (sType == "System.Single")
                    {
                        if ((System.Single)(c.InvokeMember(propInfo.Name, BindingFlags.GetProperty, null, o, new Object[] { })) == 0.0F)
                            throw new Exception(obrigatorio.sMessage);
                    }
                    else if (sType == "System.Double")
                    {
                        if ((System.Double)(c.InvokeMember(propInfo.Name, BindingFlags.GetProperty, null, o, new Object[] { })) == 0.0D)
                            throw new Exception(obrigatorio.sMessage);
                    }
                    else if (sType == "System.Int16")
                    {
                        if ((System.Int16)(c.InvokeMember(propInfo.Name, BindingFlags.GetProperty, null, o, new Object[] { })) == 0)
                            throw new Exception(obrigatorio.sMessage);
                    }
                    else if (sType == "System.Int32")
                    {
                        if ((System.Int32)(c.InvokeMember(propInfo.Name, BindingFlags.GetProperty, null, o, new Object[] { })) == 0)
                            throw new Exception(obrigatorio.sMessage);
                    }
                    else if (sType == "System.Int64")
                    {
                        if ((System.Int64)(c.InvokeMember(propInfo.Name, BindingFlags.GetProperty, null, o, new Object[] { })) == 0)
                            throw new Exception(obrigatorio.sMessage);
                    }
                    else if (sType == "System.UInt16")
                    {
                        if ((System.UInt16)(c.InvokeMember(propInfo.Name, BindingFlags.GetProperty, null, o, new Object[] { })) == 0)
                            throw new Exception(obrigatorio.sMessage);
                    }
                    else if (sType == "System.UInt32")
                    {
                        if ((System.UInt32)(c.InvokeMember(propInfo.Name, BindingFlags.GetProperty, null, o, new Object[] { })) == 0)
                            throw new Exception(obrigatorio.sMessage);
                    }
                    else if (sType == "System.UInt64")
                    {
                        if ((System.UInt64)(c.InvokeMember(propInfo.Name, BindingFlags.GetProperty, null, o, new Object[] { })) == 0)
                            throw new Exception(obrigatorio.sMessage);
                    }
                    else if (sTypeBase == "System.Enum")
                    {
                        if ((int)(c.InvokeMember(propInfo.Name, BindingFlags.GetProperty, null, o, new Object[] { })) == 0)
                            throw new Exception(obrigatorio.sMessage);
                    }
                }
            }
        }
        #endregion

        #region ClauseFrom

        protected string ClauseFrom(Type c)
        {
            StringBuilder sFrom = new StringBuilder();

            if (c.BaseType != typeof(Table))
                sFrom.Append(ClauseFrom(c.BaseType));

            if (c.BaseType.ToString() == "Ilitera.Data.Table")
            {
                sFrom.Append(dbAttribute(c).Table + " (NOLOCK) " );
            }
            else
            {
                sFrom.Append(" INNER JOIN " + dbAttribute(c).Table + " (NOLOCK) "
                    + " ON " + dbAttribute(c.BaseType).Table + "." + dbAttribute(c.BaseType).Key
                    + "=" + dbAttribute(c).Table + "." + dbAttribute(c).Key);

            }

            return sFrom.ToString();
        }
        #endregion

        #region ClauseSelect

        protected virtual string ClauseSelect(string strWhere, Type c)
        {
            string sFrom = ClauseFrom(c);

            PropertyInfo[] iprop = c.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            DataSet ds = new DataSet();

            StringBuilder sColumn = new StringBuilder();

            sColumn.Append(dbAttribute(c).Table.ToString() + "." + dbAttribute(c).Key.ToString());

            for (int j = 0; j <= iprop.Length - 1; j++)
            {
                PersistAttribute pa = (PersistAttribute)Attribute.GetCustomAttribute(iprop[j], typeof(PersistAttribute));

                if ((pa == null || pa.bVal) && iprop[j].CanWrite)
                {
                    if (iprop[j].Name.Equals("Id"))
                    {
                        if (dbAttribute(c).Where != String.Empty)
                            if (strWhere != string.Empty)
                                strWhere = dbAttribute(c).Where + " AND " + strWhere;
                            else
                                strWhere = dbAttribute(c).Where;
                    }
                    else
                    {
                        sColumn.Append(", " + iprop[j].Name);
                    }
                }
            }

            StringBuilder str = new StringBuilder();
            str.Append("USE " + dbAttribute(c).Name);
            str.Append(" SELECT " + sColumn.ToString());
            str.Append(" FROM " + sFrom);

            if (strWhere != string.Empty)
                str.Append(" WHERE " + strWhere);

            return str.ToString();
        }
        #endregion

        #region ClauseWhereAll

        protected virtual string ClauseWhereAll(Type c)
        {
            return dbAttribute(c).Table + "." + dbAttribute(c).Key 
                + "=" 
                + dbAttribute(c).Table + "." + dbAttribute(c).Key;
        }
        #endregion
    }
}

