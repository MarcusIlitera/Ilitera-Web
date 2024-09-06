using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace Ilitera.Data
{
    public interface IEntity
	{
		void Inicialize(Object o, Type c);
		IDbConnection Conn(Type c);
        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]
        DataRow	Get(Type c, int Id);
        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]
        DataSet	Get(Type c, string strWhere);
		DataSet	GetIdNome(Type c, string sNome);
		DataSet	GetIdNome(Type c, string sNome, string sWhere);
        DataSet GetIdNome(Type c, string sNome, string sWhere, string sOrderBy);
        DataSet GetIdNome(Type c, string sNome, string sWhere, string sOrderBy, bool SemAcento);
		DataSet	GetIn(Type c, string sOrder, Type typeIn, bool IN, string strWhere);
		DataSet GetAll(Type c);
		DataSet GetAll(Type c, string sOrder);
		void Find(Object o, Type c, int Id);
		ArrayList Find(Object o, Type c, string strWhere);
		ArrayList FindAll(Object o, Type c);
		ArrayList FindIn(Object o, Type c, string sOrder, Type typeIn, bool IN, string strWhere);
		ArrayList FindMax(Object o, Type c, string Column, string Condition);
		ArrayList FindMin(Object o, Type c, string Column, string Condition);
		int Save(Object o, Type c, int Id, int idUsuario, string ProcessoRealizado);
		int Save(Object o, Type c, int Id, int idUsuario, IDbTransaction transaction, string ProcessoRealizado);
		void Save(Object o, Type c, DataSet ds, int idUsuario, string ProcessoRealizado);
		void Save(Object o, Type c, DataSet ds, int idUsuario);
		void Delete(Type c, int Id, int idUsuario, string ProcessoRealizado);
		void Delete(Type c, int Id, int idUsuario, IDbTransaction transaction, string ProcessoRealizado);
		void Delete(Type c, string where, int idUsuario, string ProcessoRealizado);
		void Popular(Object o, Type c, DataRow r);
        void Validate(Object o, Type c);
		DataSet ExecuteDataset(Type c, string sql);
        int ExecuteCount(Type c, string sql);
        string ExecuteScalar(Type c, string sql);
	}
}


