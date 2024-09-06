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
    public class PessoaDAL
    {
        public Pessoa RetornarPessoa(int IdPessoa)
        {
            Pessoa pessoa = new Pessoa();

            Database objDB = new SqlDatabase(ConfigurationManager.ConnectionStrings["ConexaoIliteraOpsa"].ConnectionString);

            var objCmd = objDB.GetStoredProcCommand("ili_sp_RetornarPessoa");

            objDB.AddInParameter(objCmd, "IdPessoa", System.Data.DbType.Int32, IdPessoa);

            var readerRetorno = objDB.ExecuteReader(objCmd);

            while (readerRetorno.Read())
            {
                pessoa.IdPessoa = Convert.ToInt32(readerRetorno["IdPessoa"]);
                pessoa.NomeCodigo = readerRetorno["NomeCodigo"].ToString();
                pessoa.NomeAbreviado = readerRetorno["NomeAbreviado"].ToString();
                pessoa.NomeCompleto = readerRetorno["NomeCompleto"].ToString();

                if(readerRetorno["IndTipoPessoa"] != null && readerRetorno["IndTipoPessoa"].ToString() != String.Empty)
                    pessoa.IndTipoPessoa = Convert.ToInt32(readerRetorno["IndTipoPessoa"]);

                pessoa.Email = readerRetorno["Email"].ToString();
                pessoa.Site = readerRetorno["Site"].ToString();

                if(readerRetorno["DataCadastro"] != null && readerRetorno["DataCadastro"].ToString() != String.Empty)
                    pessoa.DataCadastro = Convert.ToDateTime(readerRetorno["DataCadastro"]);
            }

            return pessoa;
        }
    }
}
