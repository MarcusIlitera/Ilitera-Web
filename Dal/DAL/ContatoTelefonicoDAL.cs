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
    public class ContatoTelefonicoDAL
    {
        public List<ContatoTelefonico> RetornarTelefones(int IdPessoa)
        {
            List<ContatoTelefonico> lstContatoTelefonico = new List<ContatoTelefonico>();

            Database objDB = new SqlDatabase(ConfigurationManager.ConnectionStrings["ConexaoIliteraOpsa"].ConnectionString);

            var objCmd = objDB.GetStoredProcCommand("ili_sp_ContatoTelefonico");

            objDB.AddInParameter(objCmd, "IdPessoa", System.Data.DbType.Int32, IdPessoa);            

            var readerRetorno = objDB.ExecuteReader(objCmd);

            while (readerRetorno.Read())
            {
                ContatoTelefonico contatoTelefonico = new ContatoTelefonico();

                contatoTelefonico.IdContatoTelefonico = Convert.ToInt32(readerRetorno["IdContatoTelefonico"]);
                
                if(readerRetorno["IdPessoa"] != null && readerRetorno["IdPessoa"].ToString() != String.Empty)
                    contatoTelefonico.IdPessoa = Convert.ToInt32(readerRetorno["IdPessoa"]);

                if(readerRetorno["IndTipoTelefone"] != null && readerRetorno["IndTipoTelefone"].ToString() != String.Empty)
                    contatoTelefonico.IndTipoTelefone = Convert.ToInt32(readerRetorno["IndTipoTelefone"]);

                contatoTelefonico.Numero = readerRetorno["Numero"].ToString();
                contatoTelefonico.DDD = readerRetorno["DDD"].ToString();
                contatoTelefonico.Nome = readerRetorno["Nome"].ToString();
                contatoTelefonico.Departamento = readerRetorno["Departamento"].ToString();

                lstContatoTelefonico.Add(contatoTelefonico);
            }

            readerRetorno.Close();

            return lstContatoTelefonico;
        }
    }
}
