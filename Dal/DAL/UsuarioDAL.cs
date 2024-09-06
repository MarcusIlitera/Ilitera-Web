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
    public class UsuarioDAL
    {
        /// <summary>
        /// Método responsável por verificar e autorizar o usuário a logar no sistema
        /// </summary>
        /// <param name="usuario">Usuario informado</param>
        /// <param name="senha">Senha informada</param>
        /// <returns>Retornará a entidade Usuario com as propriedades devidamente preenchidas</returns>
        public Usuario AutenticarUsuaro(string usuario, string senha)
        {
            Usuario user = new Usuario();

            Database objDB = new SqlDatabase(ConfigurationManager.ConnectionStrings["ConexaoIliteraOpsa"].ConnectionString);

            var objCmd = objDB.GetStoredProcCommand("ili_sp_AutenticarUsuario");

            objDB.AddInParameter(objCmd, "NomeUsuario", System.Data.DbType.String, usuario);
            objDB.AddInParameter(objCmd, "DesSenha", System.Data.DbType.String, senha);

            var readerRetorno = objDB.ExecuteReader(objCmd);

            while (readerRetorno.Read())
            {
                user.IdUsuario = Convert.ToInt32(readerRetorno["IdUsuario"]);
                user.IdPessoa = Convert.ToInt32(readerRetorno["IdPessoa"]);
                user.NomeUsuario = readerRetorno["NomeUsuario"].ToString();

                if (readerRetorno["datCadastro"] != null && readerRetorno["datCadastro"].ToString().Trim() != String.Empty)
                    user.DataCadastro = Convert.ToDateTime(readerRetorno["datCadastro"]);

                if (readerRetorno["datUltLogin"] != null && readerRetorno["datUltLogin"].ToString().Trim() != String.Empty)
                    user.DataUltimoLogin = Convert.ToDateTime(readerRetorno["datUltLogin"]);

                if(readerRetorno["datUltLoginHist"] != null && readerRetorno["datUltLoginHist"].ToString().Trim() != String.Empty)
                    user.DataUltimoLoginHist = Convert.ToDateTime(readerRetorno["datUltLoginHist"]);

                if(readerRetorno["IdUsuarioPai"] != null && readerRetorno["IdUsuarioPai"].ToString() != String.Empty)
                    user.IdUsuarioPai = Convert.ToInt32(readerRetorno["IdUsuarioPai"]);

                user.Senha = readerRetorno["desSenha"].ToString();

                if(readerRetorno["datSenha"] != null && readerRetorno["datUltLoginHist"].ToString().Trim() != String.Empty)
                    user.DataSenha = Convert.ToDateTime(readerRetorno["datSenha"]);

                if(readerRetorno["numTentLogin"] != null && readerRetorno["numTentLogin"].ToString() != String.Empty)
                    user.NumeroTentativas = Convert.ToInt32(readerRetorno["numTentLogin"]);

                if(readerRetorno["numLogin"] != null && readerRetorno["numLogin"].ToString() != String.Empty)
                    user.NumeroLogin = Convert.ToInt32(readerRetorno["numLogin"]);
            }

            if (user != null)
            {

                objCmd.Parameters.Clear();

                objCmd.CommandText = "ili_sp_DadosPrestador";
                objCmd.CommandType = System.Data.CommandType.StoredProcedure;
                
                objDB.AddInParameter(objCmd, "IdPessoa", System.Data.DbType.Int32, user.IdPessoa);

                var retornoPrestador = objDB.ExecuteReader(objCmd);

                while (retornoPrestador.Read())
                {
                    user.IdPrestador = Convert.ToInt32(retornoPrestador["IdPrestador"]);
                }

                objCmd.Parameters.Clear();

                objDB.AddInParameter(objCmd, "IdUsuario", System.Data.DbType.Int32, user.IdUsuario);
                objDB.AddInParameter(objCmd, "DatUltLoginHist", System.Data.DbType.DateTime, user.DataUltimoLogin);
                objDB.AddInParameter(objCmd, "DatUltLogin", System.Data.DbType.DateTime, DateTime.Now);
                objDB.AddInParameter(objCmd, "NumLogin", System.Data.DbType.Int32, (user.NumeroLogin + 1));

                objCmd.CommandText = "ili_sp_AtualizaLogin";

                objDB.ExecuteNonQuery(objCmd);
            }


            return user;
        }

        public Usuario VerificarEmail(string nomeUsuario)
        {
            Usuario user = new Usuario();

            Database objDB = new SqlDatabase(ConfigurationManager.ConnectionStrings["ConexaoIliteraOpsa"].ConnectionString);

            var objCmd = objDB.GetStoredProcCommand("ili_sp_VerificarEmail");

            objDB.AddInParameter(objCmd, "nomeUsuario", System.Data.DbType.String, nomeUsuario);

            var readerRetorno = objDB.ExecuteReader(objCmd);

            while (readerRetorno.Read())
            {
                user.IdPessoa = Convert.ToInt32(readerRetorno["IdPessoa"]);
                user.IdUsuario = Convert.ToInt32(readerRetorno["IdUsuario"]);
                user.NomeUsuario = readerRetorno["NomeUsuario"].ToString();
                user.Senha = readerRetorno["desSenha"].ToString();
                user.Email = readerRetorno["Email"].ToString();                
            }

            readerRetorno.Close();

            return user;
        }

        public UsuarioDTO RetornarDados(int idUsuario)
        {
            UsuarioDTO user = new UsuarioDTO();

            Database objDB = new SqlDatabase(ConfigurationManager.ConnectionStrings["ConexaoIliteraOpsa"].ConnectionString);

            var objCmd = objDB.GetStoredProcCommand("ili_sp_retornarDadosUsuario");

            objDB.AddInParameter(objCmd, "idUsuario", System.Data.DbType.Int32, idUsuario);

            var readerRetorno = objDB.ExecuteReader(objCmd);

            while (readerRetorno.Read())
            {
                user.IdUsuario = Convert.ToInt32(readerRetorno["IdUsuario"]);
                user.NomeUsuario = readerRetorno["NomeUsuario"].ToString();
                user.DataCadastro = Convert.ToDateTime(readerRetorno["DataCadastro"]);
                user.DataUltimoLogin = Convert.ToDateTime(readerRetorno["datUltLoginHist"]);
                user.NumeroLogin = Convert.ToInt32(readerRetorno["NumLogin"]);
                user.NomeAbreviado = readerRetorno["NomeAbreviado"].ToString();
                user.Email = readerRetorno["Email"].ToString();
            }

            readerRetorno.Close();

            return user;
        }

        public bool AlterarSenha(string senha, string novaSenha, int idUsuario)
        {
            bool bRetorno = false;

            Database objDB = new SqlDatabase(ConfigurationManager.ConnectionStrings["ConexaoIliteraOpsa"].ConnectionString);

            var objCmd = objDB.GetStoredProcCommand("ili_sp_AlterarSenha");

            objDB.AddInParameter(objCmd, "senhaAtual", System.Data.DbType.String, senha);
            objDB.AddInParameter(objCmd, "senhaNova", System.Data.DbType.String, novaSenha);
            objDB.AddInParameter(objCmd, "idUsuario", System.Data.DbType.Int32, idUsuario);

            int qtdLinhas = objDB.ExecuteNonQuery(objCmd);

            if (qtdLinhas > 0)
            {
                bRetorno = true;
            }

            return bRetorno;
        }
    }
}
