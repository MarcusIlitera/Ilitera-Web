using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entities;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Configuration;

namespace DAL
{
    public class GrupoEmpresaDAL
    {
        public GrupoEmpresa RetornarGrupoEmpresa(int IdGrupoEmpresa)
        {
            GrupoEmpresa grupoEmpresa = new GrupoEmpresa();

            Database objDB = new SqlDatabase(ConfigurationManager.ConnectionStrings["ConexaoIliteraOpsa"].ConnectionString);

            var objCmd = objDB.GetStoredProcCommand("ili_sp_RetornarGrupoEmpresa");

            objDB.AddInParameter(objCmd, "IdGrupoEmpresa", System.Data.DbType.Int32, IdGrupoEmpresa);

            var readerRetorno = objDB.ExecuteReader(objCmd);

            while (readerRetorno.Read())
            {
                grupoEmpresa.IdGrupoEmpresa = Convert.ToInt32(readerRetorno["IdGrupoEmpresa"]);
                grupoEmpresa.Descricao = readerRetorno["Descricao"].ToString();
            }

            return grupoEmpresa;
        }
    }
}
