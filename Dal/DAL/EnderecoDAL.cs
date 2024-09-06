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
    public class EnderecoDAL
    {
        public Endereco RetornarEndereco(int IdPessoa)
        {
            Endereco endereco = new Endereco();

            Database objDB = new SqlDatabase(ConfigurationManager.ConnectionStrings["ConexaoIliteraOpsa"].ConnectionString);

            var objCmd = objDB.GetStoredProcCommand("ili_sp_RetornarEndereco");

            objDB.AddInParameter(objCmd, "IdPessoa", System.Data.DbType.Int32, IdPessoa);

            var readerRetorno = objDB.ExecuteReader(objCmd);

            while (readerRetorno.Read())
            {
                endereco.IdEndereco = Convert.ToInt32(readerRetorno["IdEndereco"]);
                endereco.Municipio = readerRetorno["Municipio"].ToString();
                endereco.UF = readerRetorno["UF"].ToString();
                endereco.CEP = readerRetorno["CEP"].ToString();

                if(readerRetorno["IdTipoLogradouro"] != null && readerRetorno["IdTipoLogradouro"].ToString() != String.Empty)
                    endereco.IdTipoLogradouro = Convert.ToInt32(readerRetorno["IdTipoLogradouro"]);

                endereco.Logradouro = readerRetorno["Logradouro"].ToString();
                endereco.Numero = readerRetorno["Numero"].ToString();
                endereco.Complemento = readerRetorno["Complemento"].ToString();
                endereco.Bairro = readerRetorno["Bairro"].ToString();
            }

            return endereco;
        }
    }
}
