using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Security;
using System.Security.Permissions;
using System.Reflection;

namespace Ilitera.Data
{

    

    public abstract class Table : IComparable, ICloneable, IDisposable
    {
        #region Variaveis

        private IEntity mirror;
        public Table mirrorOld;

        private IDbTransaction _Transaction;
        private int _UsuarioId;
        private string _UsuarioProcessoRealizado = string.Empty;

        public static int usuario = 0;
        public static bool IsWeb = false;
        public static bool IsOffLine = false;

        #endregion

        #region Construtor

        [StrongNameIdentityPermission(SecurityAction.LinkDemand, PublicKey = "0x0024000004800000940000000602000000240000525341310004000001000100EF4CB22E8E54CA1BB3659FF8FF61D92412ED8186273F9C2E8BC6A6AAC8523ABC5C0720E7A18EFCBF6DFACAA2B487924CAD8F1FB486151328F9E88482B0C450CBD9CCBC110149AAB284F6A3930F8D4F3A1B48191C9B33B88FB418C6F401425ED8F6D16608AEF59487B3BFDAD714CA434BD8779B3AC2AA5E11E0686BB2CA8FE4B5")]
        public Table()
        {
            mirror = new Ilitera.Data.SQLServer.EntitySQLServer();

            this.UsuarioId = usuario;
        }

        #endregion

        #region Dispose

        // the destructor
        ~Table()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);

            // tell the GC that the Finalize process no longer needs
            // to be run for this object.
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // Clean up all managed resources
            }

            // Clean up all native resources

        }
        #endregion

        #region Entity

        public virtual void Inicialize()
        {
            mirror.Inicialize(this, this.GetType());
        }
        
        public abstract int Id
        {
            
            get;
            
            set;
        }

        [Persist(false)]
        public int UsuarioId
        {
            get { return _UsuarioId; }
            set { _UsuarioId = value; }
        }

        [Persist(false)]
        public string UsuarioProcessoRealizado
        {
            get { return _UsuarioProcessoRealizado; }
            set { _UsuarioProcessoRealizado = value; }
        }

        [Persist(false)]
        public IDbTransaction Transaction
        {
            get { return _Transaction; }
            set { _Transaction = value; }
        }

        public DataRow Get(int Id)
        {
            return mirror.Get(this.GetType(), Id);
        }

        public DataSet Get(string filter)
        {
            return mirror.Get(this.GetType(), filter);
        }

        public DataSet GetIn(Type typeIn, string sOrder, bool IN, string filter)
        {
            return mirror.GetIn(this.GetType(), sOrder, typeIn, IN, filter);
        }

        public DataSet GetIdNome(string sNome)
        {
            return mirror.GetIdNome(this.GetType(), sNome);
        }

        public DataSet GetIdNome(string sNome, string sWhere)
        {
            return mirror.GetIdNome(this.GetType(), sNome, sWhere);
        }

        public DataSet GetIdNome(string sNome, string sWhere, string sOrderBy)
        {
            return mirror.GetIdNome(this.GetType(), sNome, sWhere, sOrderBy);
        }

        public DataSet GetIdNome(string sNome, string sWhere, string sOrderBy, bool SemAcento)
        {
            return mirror.GetIdNome(this.GetType(), sNome, sWhere, sOrderBy, SemAcento);
        }

        public DataSet GetAll()
        {
            return mirror.GetAll(this.GetType());
        }

        public DataSet GetAll(string sOrder)
        {
            return mirror.GetAll(this.GetType(), sOrder);
        }

        public void Find()
        {
            this.Find(Id);
        }

        public void Find(int Id)
        {
            if (Id == 0)
                Inicialize();
            else
            {
                mirror.Find(this, this.GetType(), Id);

                mirrorOld = (Table)this.Clone();
            }
        }

        public ArrayList Find(string filter)
        {
            ArrayList list = mirror.Find(this, this.GetType(), filter);

            if (Id != 0 && list.Count == 1)
            {
                try
                {
                    mirrorOld = (Table)this.Clone();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                }
            }

            return list;
        }

        public List<T> Find<T>(string filter) where T : new()
        {
            List<T> ret = new List<T>();

            DataSet ds = mirror.Get(this.GetType(), filter);

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                T obj = new T();
                mirror.Popular(obj, this.GetType(), row);
                ret.Add(obj);
            }

            return ret;
        }

        public ArrayList FindAll()
        {
            ArrayList list = mirror.FindAll(this, this.GetType());

            if (Id != 0 && list.Count == 1)
            {
                try
                {
                    mirrorOld = (Table)this.Clone();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                }
            }

            return list;
        }

        public List<T> FindAll<T>() where T : new()
        {
            List<T> ret = new List<T>();

            DataSet ds = mirror.GetAll(this.GetType());

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                T obj = new T();
                mirror.Popular(obj, this.GetType(), row);
                ret.Add(obj);
            }

            return ret;
        }

        public List<T> FindAll<T>(string sOrder) where T : new()
        {
            List<T> ret = new List<T>();

            DataSet ds = mirror.GetAll(this.GetType(), sOrder);

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                T obj = new T();
                mirror.Popular(obj, this.GetType(), row);
                ret.Add(obj);
            }

            return ret;
        }

        public ArrayList FindIn(Type typeIn,
                                string sOrder,
                                bool IN,
                                string filter)
        {
            ArrayList list = mirror.FindIn(this, this.GetType(), sOrder, typeIn, IN, filter);

            if (Id != 0 && list.Count == 1)
            {
                try
                {
                    mirrorOld = (Table)this.Clone();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                    //System.Diagnostics.Debug.WriteLine(ex.Message);
                }
            }
            return list;
        }

        public List<T> FindIn<T>(Type typeIn,
                                 string sOrder,
                                 bool IN,
                                 string filter) where T : new()
        {
            List<T> ret = new List<T>();

            DataSet ds = mirror.GetIn(this.GetType(), sOrder, typeIn, IN, filter);

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                T obj = new T();
                mirror.Popular(obj, this.GetType(), row);
                ret.Add(obj);
            }

            return ret;
        }

        public ArrayList FindMax(string Column, string Condition)
        {
            ArrayList list = mirror.FindMax(this, this.GetType(), Column, Condition);

            if (Id != 0 && list.Count == 1)
            {
                try
                {
                    mirrorOld = (Table)this.Clone();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                    //System.Diagnostics.Debug.WriteLine(ex.Message);
                }
            }
            return list;
        }

        public ArrayList FindMin(string Column, string Condition)
        {
            ArrayList list = mirror.FindMin(this, this.GetType(), Column, Condition);

            if (Id != 0 && list.Count == 1)
            {
                try
                {
                    mirrorOld = (Table)this.Clone();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                }
            }
            return list;
        }

        public virtual void Validate()
        {
            mirror.Validate(this, this.GetType());
        }

        public virtual int Save()
        {
            Validate();

            if (this._Transaction == null || this._Transaction.Connection == null)
                this.Id = mirror.Save(this, this.GetType(), this.Id, this.UsuarioId, _UsuarioProcessoRealizado);
            else
                this.Id = mirror.Save(this, this.GetType(), this.Id, this.UsuarioId, this._Transaction, _UsuarioProcessoRealizado);

            mirrorOld = (Table)this.Clone();

            return this.Id;
        }

        public virtual void Delete()
        {
            if (this._Transaction == null || this._Transaction.Connection == null)
                mirror.Delete(this.GetType(), this.Id, this.UsuarioId, this._UsuarioProcessoRealizado);
            else
                mirror.Delete(this.GetType(), this.Id, this.UsuarioId, this._Transaction, this._UsuarioProcessoRealizado);
        }

        public virtual void Delete(string where)
        {
            mirror.Delete(this.GetType(), where, this._UsuarioId, this._UsuarioProcessoRealizado);
        }

        public void Popular(DataRow row)
        {
            mirror.Popular(this, this.GetType(), row);
        }

        public DataSet ExecuteDataset(string strSQL)
        {
            return mirror.ExecuteDataset(this.GetType(), strSQL);
        }

        public int ExecuteCount(string where)
        {
            return mirror.ExecuteCount(this.GetType(), where);
        }

        public string ExecuteScalar(string sqlCmd)
        {
            return mirror.ExecuteScalar(this.GetType(), sqlCmd);
        }
        #endregion

        #region GetTransaction

        public IDbTransaction GetTransaction()
        {
            IDbConnection cnn = mirror.Conn(this.GetType());

            this._Transaction = cnn.BeginTransaction();

            return this._Transaction;
        }
        #endregion

        #region Clone

        public object Clone()
        {
            Type c = this.GetType();

            object newObjClone = Activator.CreateInstance(c);

            MemberInfo[] iprop
                = c.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (PropertyInfo ipropInfo in iprop)
            {
                PersistAttribute pa
                    = (PersistAttribute)Attribute.GetCustomAttribute(ipropInfo, typeof(PersistAttribute));

                if ((pa == null || pa.bVal) && ipropInfo.CanWrite)
                {
                    //Pega o tipo da coluna
                    Type type = ((PropertyInfo)ipropInfo).PropertyType;

                    if (type.ToString() == "System.String"
                        || type.BaseType.ToString() == "System.ValueType"
                        || type.BaseType.ToString() == "System.Enum")
                    {
                        c.InvokeMember(ipropInfo.Name,
                            BindingFlags.SetProperty,
                            null,
                            newObjClone,
                            new Object[]{c.InvokeMember(ipropInfo.Name, 
											BindingFlags.GetProperty, 
											null, 
											this, 
											new Object[] {})});
                    }
                    else if (IsTable(type))
                    {
                        //Pega a referencia do objeto
                        object o = c.InvokeMember(ipropInfo.Name, BindingFlags.GetProperty, null, this, new Object[] { });
                        //Pega o valor do objeto para duplicar
                        int OId = (int)type.InvokeMember("Id", BindingFlags.GetProperty, null, o, new Object[] { });
                        //Cria uma nova instancia 
                        Object newCol = Activator.CreateInstance(type);
                        //Define o id no novo objeto
                        type.InvokeMember("Id", BindingFlags.SetProperty, null, newCol, new Object[] { OId });
                        //Seta a nova coluna no objeto clone
                        c.InvokeMember(ipropInfo.Name, BindingFlags.SetProperty, null, newObjClone, new Object[] { newCol });
                    }
                }
            }
            return newObjClone;
        }
        #endregion

        #region HashCode
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        #endregion

        #region Equals

        public override bool Equals(object obj)
        {
            if (obj == null) return false;

            Type c = this.GetType();

            if (c != obj.GetType()) return false;

            MemberInfo[] iprop
                = c.GetProperties(BindingFlags.Public
                                    | BindingFlags.Instance);

            foreach (PropertyInfo ipropInfo in iprop)
            {
                PersistAttribute pa
                    = (PersistAttribute)Attribute.GetCustomAttribute(ipropInfo,
                    typeof(PersistAttribute));

                if (pa != null && !pa.bVal)
                    continue;

                if (!EqualsProperty(obj, c, ipropInfo))
                    return false;
            }
            return true;
        }

        #endregion

        #region EqualsProperty

        public bool EqualsProperty(Type c, PropertyInfo ipropInfo)
        {
            return this.EqualsProperty(mirrorOld, c, ipropInfo);
        }

        public bool EqualsProperty(object obj, Type c, PropertyInfo ipropInfo)
        {

            object a = c.InvokeMember(ipropInfo.Name,
                                        BindingFlags.GetProperty,
                                        null,
                                        this,
                                        new Object[] { });

            object b = c.InvokeMember(ipropInfo.Name,
                                        BindingFlags.GetProperty,
                                        null,
                                        obj,
                                        new Object[] { });

            Type type = ((PropertyInfo)ipropInfo).PropertyType;

            if (a == null || b == null)
            {
                return false;
            }
            else if (type.ToString() == "System.String"
                || type.BaseType.ToString() == "System.ValueType"
                || type.BaseType.ToString() == "System.Enum")
            {
                if (!Convert.Equals(a, b))
                    return false;
            }
            else if (IsTable(type))
            {
                int a_Id = (int)type.InvokeMember("Id",
                                                    BindingFlags.GetProperty,
                                                    null,
                                                    a,
                                                    new Object[] { });

                int b_Id = (int)type.InvokeMember("Id",
                                                    BindingFlags.GetProperty,
                                                    null,
                                                    b,
                                                    new Object[] { });

                if (a_Id != b_Id)
                    return false;
            }
            return true;
        }
        #endregion

        #region IsTable

        public static bool IsTable(Type type)
        {
            if (type == typeof(Table) || type.BaseType == typeof(Table))
                return true;

            if (type == typeof(System.Object) || type.BaseType == typeof(System.Object))
                return false;
            else
                return IsTable(type.BaseType);
        }
        #endregion

        #region CompareTo

        public virtual int CompareTo(object obj)
        {
            if (this.ToString().CompareTo(obj.ToString()) > 0)
                return 1;

            if (this.ToString().CompareTo(obj.ToString()) < 0)
                return -1;

            return 0;
        }
        #endregion
    }
}


