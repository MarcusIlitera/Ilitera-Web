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
    public class EmpresaDAL
    {
        public List<EmpresaDTO> RetornarEmpresas()
        {
            List<EmpresaDTO> lstEmpresa = new List<EmpresaDTO>();

            Database objDB = new SqlDatabase(ConfigurationManager.ConnectionStrings["ConexaoIliteraOpsa"].ConnectionString);
                        
            var objCmd = objDB.GetStoredProcCommand("ili_sp_RetornarEmpresas");
            objCmd.CommandTimeout = 900;

            var readerRetorno = objDB.ExecuteReader(objCmd);

            while (readerRetorno.Read())
            {
                EmpresaDTO empresa = new EmpresaDTO();

                empresa.IdJuridica = Convert.ToInt32(readerRetorno["IdJuridica"]);

                if(readerRetorno["IdPessoa"] != null && readerRetorno["IdPessoa"].ToString() != String.Empty)
                    empresa.IdPessoa = Convert.ToInt32(readerRetorno["IdPessoa"]);

                if(readerRetorno["QtdEmpregados"] != null && readerRetorno["QtdEmpregados"].ToString() != String.Empty)
                    empresa.QtdEmpregados = Convert.ToInt32(readerRetorno["QtdEmpregados"]);

                empresa.NomeAbreviado = readerRetorno["NomeAbreviado"].ToString();

                if(readerRetorno["DataCadastro"] != null && readerRetorno["DataCadastro"].ToString() != String.Empty)
                    empresa.DataCadastro = Convert.ToDateTime(readerRetorno["DataCadastro"]);

                lstEmpresa.Add(empresa);
            }

            return lstEmpresa;
        }

        public List<EmpresaDTO> RetornarEmpresas(int idPrestador, int idJuridicaPapel, string nomeFuncionario, string xAtivo, string xOrdem)
        {
            List<EmpresaDTO> lstEmpresa = new List<EmpresaDTO>();

            Database objDB = new SqlDatabase(ConfigurationManager.ConnectionStrings["ConexaoIliteraOpsa"].ConnectionString);

            //Database objDBFiliais = new SqlDatabase(ConfigurationManager.ConnectionStrings["ConexaoIliteraOpsa"].ConnectionString);

            var objCmd = objDB.GetStoredProcCommand("ili_sp_RetornarEmpresasPrestador");

            objDB.AddInParameter(objCmd, "IdPrestador", System.Data.DbType.Int32, idPrestador);
            objDB.AddInParameter(objCmd, "IdJuridicaPapel", System.Data.DbType.Int32, idJuridicaPapel);
            objDB.AddInParameter(objCmd, "NomeFuncionario", System.Data.DbType.String, nomeFuncionario);
            objDB.AddInParameter(objCmd, "Ativo", System.Data.DbType.String, xAtivo);
            objDB.AddInParameter(objCmd, "Ordem", System.Data.DbType.String, xOrdem);

            objCmd.CommandTimeout = 900;
           

            var readerRetorno = objDB.ExecuteReader(objCmd);

            while (readerRetorno.Read())
            {
                EmpresaDTO empresa = new EmpresaDTO();

                empresa.NomeAbreviado = readerRetorno["NomeAbreviado"].ToString();
                empresa.NomeCompleto = readerRetorno["NomeCompleto"].ToString();

                if(readerRetorno["IdPessoa"] != null && readerRetorno["IdPessoa"].ToString() != String.Empty)
                    empresa.IdPessoa = Convert.ToInt32(readerRetorno["IdPessoa"]);

                if(readerRetorno["IndTipoPessoa"] != null && readerRetorno["IndTipoPessoa"].ToString() != String.Empty)
                    empresa.IndTipoPessoa = Convert.ToInt32(readerRetorno["IndTipoPessoa"]);

                if(readerRetorno["IdCliente"] != null && readerRetorno["IdCliente"].ToString() != String.Empty)
                    empresa.IdCliente = Convert.ToInt32(readerRetorno["IdCliente"]);

                if(readerRetorno["AtivarLocalDeTrabalho"] != null && readerRetorno["AtivarLocalDeTrabalho"].ToString() != String.Empty)
                    empresa.AtivarLocalDeTrabalho = Convert.ToInt32(readerRetorno["AtivarLocalDeTrabalho"]);

                if(readerRetorno["IdJuridica"] != null && readerRetorno["IdJuridica"].ToString() != null)
                    empresa.IdJuridica = Convert.ToInt32(readerRetorno["IdJuridica"]);

                if(readerRetorno["QtdEmpregados"] != null && readerRetorno["QtdEmpregados"].ToString() != null)
                    empresa.QtdEmpregados = Convert.ToInt32(readerRetorno["QtdEmpregados"]);

                if(readerRetorno["DataCadastro"] != null && readerRetorno["DataCadastro"].ToString() != null)
                    empresa.DataCadastro = Convert.ToDateTime(readerRetorno["DataCadastro"]);

                empresa.IdJuridicaPai = 0;

                bool zRepetido = false;
                for ( int rLoc=0; rLoc<lstEmpresa.Count;rLoc++)
                {
                    if ( lstEmpresa[rLoc].IdPessoa == empresa.IdPessoa )
                    {
                        zRepetido = true;
                        break;
                    }
                }

                if (zRepetido == false)
                {

                    lstEmpresa.Add(empresa);

                    if (lstEmpresa.Count >= 1000)
                    {
                        return lstEmpresa;
                    }

                    if (empresa.AtivarLocalDeTrabalho > 0)
                    {
                        Database objDBFiliais = new SqlDatabase(ConfigurationManager.ConnectionStrings["ConexaoIliteraOpsa"].ConnectionString);

                        var objCmdFilial = objDBFiliais.GetStoredProcCommand("ili_sp_RetornarEmpresasAtivarLocalTrabalho");

                        objDBFiliais.AddInParameter(objCmdFilial, "IdJuridica", System.Data.DbType.Int32, empresa.IdJuridica);
                        objDBFiliais.AddInParameter(objCmdFilial, "IdPrestador", System.Data.DbType.Int32, idPrestador);
                        objDBFiliais.AddInParameter(objCmdFilial, "Ativo", System.Data.DbType.String, xAtivo);

                        objCmdFilial.CommandTimeout = 900;
                        var readerRetornoFilial = objDBFiliais.ExecuteReader(objCmdFilial);

                        while (readerRetornoFilial.Read())
                        {
                            EmpresaDTO empresaFilial = new EmpresaDTO();

                            empresaFilial.NomeAbreviado = "...." + readerRetornoFilial["NomeAbreviado"].ToString();
                            empresaFilial.NomeCompleto = "...." + readerRetornoFilial["NomeCompleto"].ToString();

                            if (readerRetornoFilial["IdPessoa"] != null && readerRetornoFilial["IdPessoa"].ToString() != String.Empty)
                                empresaFilial.IdPessoa = Convert.ToInt32(readerRetornoFilial["IdPessoa"]);

                            if (readerRetornoFilial["IndTipoPessoa"] != null && readerRetornoFilial["IndTipoPessoa"].ToString() != String.Empty)
                                empresaFilial.IndTipoPessoa = Convert.ToInt32(readerRetornoFilial["IndTipoPessoa"]);

                            if (readerRetornoFilial["IdCliente"] != null && readerRetornoFilial["IdCliente"].ToString() != String.Empty)
                                empresaFilial.IdCliente = Convert.ToInt32(readerRetornoFilial["IdCliente"]);

                            if (readerRetornoFilial["AtivarLocalDeTrabalho"] != null && readerRetornoFilial["AtivarLocalDeTrabalho"].ToString() != String.Empty)
                                empresaFilial.AtivarLocalDeTrabalho = Convert.ToInt32(readerRetornoFilial["AtivarLocalDeTrabalho"]);

                            if (readerRetornoFilial["IdJuridica"] != null && readerRetornoFilial["IdJuridica"].ToString() != String.Empty)
                                empresaFilial.IdJuridica = Convert.ToInt32(readerRetornoFilial["IdJuridica"]);

                            if (readerRetornoFilial["QtdEmpregados"] != null && readerRetornoFilial["QtdEmpregados"].ToString() != String.Empty)
                                empresaFilial.QtdEmpregados = Convert.ToInt32(readerRetornoFilial["QtdEmpregados"]);

                            if (readerRetornoFilial["DataCadastro"] != null && readerRetornoFilial["DataCadastro"].ToString() != String.Empty)
                                empresaFilial.DataCadastro = Convert.ToDateTime(readerRetornoFilial["DataCadastro"]);

                            if (readerRetornoFilial["IdJuridicaPai"] != null && readerRetornoFilial["IdJuridicaPai"].ToString() != String.Empty)
                                empresaFilial.IdJuridicaPai = Convert.ToInt32(readerRetornoFilial["IdJuridicaPai"]);

                            lstEmpresa.Add(empresaFilial);

                            if (lstEmpresa.Count >= 1000)
                            {
                                return lstEmpresa;
                            }
                        }
                        readerRetornoFilial.Dispose();
                        objCmdFilial.Dispose();
                        //objDBFiliais = null;
                        //readerRetornoFilial = null;
                    }

                }
            }

            return lstEmpresa;


        }



        public System.Data.IDataReader RetornarEmpresas_Nome2(int idPrestador, int idJuridicaPapel, string nomeFuncionario, string Colaborador, string zStatus)
        {
            List<EmpresaDTO> lstEmpresa = new List<EmpresaDTO>();

            Database objDB = new SqlDatabase(ConfigurationManager.ConnectionStrings["ConexaoIliteraOpsa"].ConnectionString);

            var objCmd = objDB.GetStoredProcCommand("ili_sp_RetornarEmpresasPrestador_Nome_Empresa");

            objDB.AddInParameter(objCmd, "IdPrestador", System.Data.DbType.Int32, idPrestador);
            objDB.AddInParameter(objCmd, "IdJuridicaPapel", System.Data.DbType.Int32, idJuridicaPapel);
            objDB.AddInParameter(objCmd, "NomeFuncionario", System.Data.DbType.String, nomeFuncionario);
            objDB.AddInParameter(objCmd, "Colaborador", System.Data.DbType.String, Colaborador);
            objDB.AddInParameter(objCmd, "Status", System.Data.DbType.String, zStatus);
            objCmd.CommandTimeout = 900;

            var readerRetorno = objDB.ExecuteReader(objCmd);

            return readerRetorno;


        }





        public List<EmpresaDTO> RetornarEmpresas_Nome(int idPrestador, int idJuridicaPapel, string nomeFuncionario, string Colaborador)
        {
            List<EmpresaDTO> lstEmpresa = new List<EmpresaDTO>();

            Database objDB = new SqlDatabase(ConfigurationManager.ConnectionStrings["ConexaoIliteraOpsa"].ConnectionString);

            var objCmd = objDB.GetStoredProcCommand("ili_sp_RetornarEmpresasPrestador_Nome");

            objDB.AddInParameter(objCmd, "IdPrestador", System.Data.DbType.Int32, idPrestador);
            objDB.AddInParameter(objCmd, "IdJuridicaPapel", System.Data.DbType.Int32, idJuridicaPapel);
            objDB.AddInParameter(objCmd, "NomeFuncionario", System.Data.DbType.String, nomeFuncionario);
            objDB.AddInParameter(objCmd, "Colaborador", System.Data.DbType.String, Colaborador);
            
            objCmd.CommandTimeout = 900;

            var readerRetorno = objDB.ExecuteReader(objCmd);
            

            while (readerRetorno.Read())
            {
                EmpresaDTO empresa = new EmpresaDTO();

                empresa.NomeAbreviado = readerRetorno["NomeAbreviado"].ToString();
                empresa.NomeCompleto = readerRetorno["NomeCompleto"].ToString();

                if (readerRetorno["IdPessoa"] != null && readerRetorno["IdPessoa"].ToString() != String.Empty)
                    empresa.IdPessoa = Convert.ToInt32(readerRetorno["IdPessoa"]);

                if (readerRetorno["IndTipoPessoa"] != null && readerRetorno["IndTipoPessoa"].ToString() != String.Empty)
                    empresa.IndTipoPessoa = Convert.ToInt32(readerRetorno["IndTipoPessoa"]);

                if (readerRetorno["IdCliente"] != null && readerRetorno["IdCliente"].ToString() != String.Empty)
                    empresa.IdCliente = Convert.ToInt32(readerRetorno["IdCliente"]);

                if (readerRetorno["AtivarLocalDeTrabalho"] != null && readerRetorno["AtivarLocalDeTrabalho"].ToString() != String.Empty)
                    empresa.AtivarLocalDeTrabalho = Convert.ToInt32(readerRetorno["AtivarLocalDeTrabalho"]);

                if (readerRetorno["IdJuridica"] != null && readerRetorno["IdJuridica"].ToString() != null)
                    empresa.IdJuridica = Convert.ToInt32(readerRetorno["IdJuridica"]);

                if (readerRetorno["QtdEmpregados"] != null && readerRetorno["QtdEmpregados"].ToString() != null)
                    empresa.QtdEmpregados = Convert.ToInt32(readerRetorno["QtdEmpregados"]);

                if (readerRetorno["DataCadastro"] != null && readerRetorno["DataCadastro"].ToString() != null)
                    empresa.DataCadastro = Convert.ToDateTime(readerRetorno["DataCadastro"]);

                empresa.IdJuridicaPai = 0;

                lstEmpresa.Add(empresa);

                if (lstEmpresa.Count >= 1150)
                {
                    return lstEmpresa;
                }


            }


            var objCmd2 = objDB.GetStoredProcCommand("ili_sp_RetornarEmpresasPrestador");

            objDB.AddInParameter(objCmd2, "IdPrestador", System.Data.DbType.Int32, idPrestador);
            objDB.AddInParameter(objCmd2, "IdJuridicaPapel", System.Data.DbType.Int32, idJuridicaPapel);
            objDB.AddInParameter(objCmd2, "NomeFuncionario", System.Data.DbType.String, nomeFuncionario);
            objDB.AddInParameter(objCmd2, "Ativo", System.Data.DbType.String, "T");

            var readerRetorno2 = objDB.ExecuteReader(objCmd2);

            while (readerRetorno2.Read())
            {
                EmpresaDTO empresa2 = new EmpresaDTO();
                
                if (readerRetorno2["AtivarLocalDeTrabalho"] != null && readerRetorno2["AtivarLocalDeTrabalho"].ToString() != String.Empty)
                    empresa2.AtivarLocalDeTrabalho = Convert.ToInt32(readerRetorno2["AtivarLocalDeTrabalho"]);


                if (readerRetorno2["IdJuridica"] != null && readerRetorno2["IdJuridica"].ToString() != null)
                    empresa2.IdJuridica = Convert.ToInt32(readerRetorno2["IdJuridica"]);


                if (empresa2.AtivarLocalDeTrabalho > 0)
                {
                    Database objDBFiliais = new SqlDatabase(ConfigurationManager.ConnectionStrings["ConexaoIliteraOpsa"].ConnectionString);

                    var objCmdFilial = objDBFiliais.GetStoredProcCommand("ili_sp_RetornarEmpresasAtivarLocalTrabalho_Nome");

                    objDBFiliais.AddInParameter(objCmdFilial, "IdJuridica", System.Data.DbType.Int32, empresa2.IdJuridica);
                    objDBFiliais.AddInParameter(objCmdFilial, "IdPrestador", System.Data.DbType.Int32, idPrestador);
                    objDBFiliais.AddInParameter(objCmdFilial, "Colaborador", System.Data.DbType.String, Colaborador);

                    var readerRetornoFilial = objDBFiliais.ExecuteReader(objCmdFilial);

                    while (readerRetornoFilial.Read())
                    {
                        EmpresaDTO empresaFilial = new EmpresaDTO();

                        empresaFilial.NomeAbreviado = readerRetornoFilial["NomeAbreviado"].ToString();
                        empresaFilial.NomeCompleto = readerRetornoFilial["NomeCompleto"].ToString();

                        if (readerRetornoFilial["IdPessoa"] != null && readerRetornoFilial["IdPessoa"].ToString() != String.Empty)
                            empresaFilial.IdPessoa = Convert.ToInt32(readerRetornoFilial["IdPessoa"]);

                        if (readerRetornoFilial["IndTipoPessoa"] != null && readerRetornoFilial["IndTipoPessoa"].ToString() != String.Empty)
                            empresaFilial.IndTipoPessoa = Convert.ToInt32(readerRetornoFilial["IndTipoPessoa"]);

                        if (readerRetornoFilial["IdCliente"] != null && readerRetornoFilial["IdCliente"].ToString() != String.Empty)
                            empresaFilial.IdCliente = Convert.ToInt32(readerRetornoFilial["IdCliente"]);

                        if (readerRetornoFilial["AtivarLocalDeTrabalho"] != null && readerRetornoFilial["AtivarLocalDeTrabalho"].ToString() != String.Empty)
                            empresaFilial.AtivarLocalDeTrabalho = Convert.ToInt32(readerRetornoFilial["AtivarLocalDeTrabalho"]);

                        if (readerRetornoFilial["IdJuridica"] != null && readerRetornoFilial["IdJuridica"].ToString() != String.Empty)
                            empresaFilial.IdJuridica = Convert.ToInt32(readerRetornoFilial["IdJuridica"]);

                        if (readerRetornoFilial["QtdEmpregados"] != null && readerRetornoFilial["QtdEmpregados"].ToString() != String.Empty)
                            empresaFilial.QtdEmpregados = Convert.ToInt32(readerRetornoFilial["QtdEmpregados"]);

                        if (readerRetornoFilial["DataCadastro"] != null && readerRetornoFilial["DataCadastro"].ToString() != String.Empty)
                            empresaFilial.DataCadastro = Convert.ToDateTime(readerRetornoFilial["DataCadastro"]);

                        if (readerRetornoFilial["IdJuridicaPai"] != null && readerRetornoFilial["IdJuridicaPai"].ToString() != String.Empty)
                            empresaFilial.IdJuridicaPai = Convert.ToInt32(readerRetornoFilial["IdJuridicaPai"]);

                        lstEmpresa.Add(empresaFilial);

                        if (lstEmpresa.Count >= 1150)
                        {
                            return lstEmpresa;
                        }

                    }
                }
            }

            return lstEmpresa;
        }





        public System.Data.IDataReader RetornarEmpresas_Matricula2(int idPrestador, int idJuridicaPapel, string nomeFuncionario, string Matricula, string zStatus)
        {
            List<EmpresaDTO> lstEmpresa = new List<EmpresaDTO>();

            Database objDB = new SqlDatabase(ConfigurationManager.ConnectionStrings["ConexaoIliteraOpsa"].ConnectionString);

            var objCmd = objDB.GetStoredProcCommand("ili_sp_RetornarEmpresasPrestador_Matricula2");

            objDB.AddInParameter(objCmd, "IdPrestador", System.Data.DbType.Int32, idPrestador);
            objDB.AddInParameter(objCmd, "IdJuridicaPapel", System.Data.DbType.Int32, idJuridicaPapel);
            objDB.AddInParameter(objCmd, "NomeFuncionario", System.Data.DbType.String, nomeFuncionario);
            objDB.AddInParameter(objCmd, "Matricula", System.Data.DbType.String, Matricula);
            objDB.AddInParameter(objCmd, "Status", System.Data.DbType.String, zStatus);
            objCmd.CommandTimeout = 900;

            var readerRetorno = objDB.ExecuteReader(objCmd);

            return readerRetorno;


        }

        public List<EmpresaDTO> RetornarEmpresas_Matricula(int idPrestador, int idJuridicaPapel, string nomeFuncionario, string Matricula)
        {
            List<EmpresaDTO> lstEmpresa = new List<EmpresaDTO>();

            Database objDB = new SqlDatabase(ConfigurationManager.ConnectionStrings["ConexaoIliteraOpsa"].ConnectionString);

            var objCmd = objDB.GetStoredProcCommand("ili_sp_RetornarEmpresasPrestador_Matricula");

            objDB.AddInParameter(objCmd, "IdPrestador", System.Data.DbType.Int32, idPrestador);
            objDB.AddInParameter(objCmd, "IdJuridicaPapel", System.Data.DbType.Int32, idJuridicaPapel);
            objDB.AddInParameter(objCmd, "NomeFuncionario", System.Data.DbType.String, nomeFuncionario);
            objDB.AddInParameter(objCmd, "Matricula", System.Data.DbType.String, Matricula);

            objCmd.CommandTimeout = 900;

            var readerRetorno = objDB.ExecuteReader(objCmd);


            while (readerRetorno.Read())
            {
                EmpresaDTO empresa = new EmpresaDTO();

                empresa.NomeAbreviado = readerRetorno["NomeAbreviado"].ToString();
                empresa.NomeCompleto = readerRetorno["NomeCompleto"].ToString();

                if (readerRetorno["IdPessoa"] != null && readerRetorno["IdPessoa"].ToString() != String.Empty)
                    empresa.IdPessoa = Convert.ToInt32(readerRetorno["IdPessoa"]);

                if (readerRetorno["IndTipoPessoa"] != null && readerRetorno["IndTipoPessoa"].ToString() != String.Empty)
                    empresa.IndTipoPessoa = Convert.ToInt32(readerRetorno["IndTipoPessoa"]);

                if (readerRetorno["IdCliente"] != null && readerRetorno["IdCliente"].ToString() != String.Empty)
                    empresa.IdCliente = Convert.ToInt32(readerRetorno["IdCliente"]);

                if (readerRetorno["AtivarLocalDeTrabalho"] != null && readerRetorno["AtivarLocalDeTrabalho"].ToString() != String.Empty)
                    empresa.AtivarLocalDeTrabalho = Convert.ToInt32(readerRetorno["AtivarLocalDeTrabalho"]);

                if (readerRetorno["IdJuridica"] != null && readerRetorno["IdJuridica"].ToString() != null)
                    empresa.IdJuridica = Convert.ToInt32(readerRetorno["IdJuridica"]);

                if (readerRetorno["QtdEmpregados"] != null && readerRetorno["QtdEmpregados"].ToString() != null)
                    empresa.QtdEmpregados = Convert.ToInt32(readerRetorno["QtdEmpregados"]);

                if (readerRetorno["DataCadastro"] != null && readerRetorno["DataCadastro"].ToString() != null)
                    empresa.DataCadastro = Convert.ToDateTime(readerRetorno["DataCadastro"]);

                empresa.IdJuridicaPai = 0;

                lstEmpresa.Add(empresa);

                if (lstEmpresa.Count >= 1150)
                {
                    return lstEmpresa;
                }


            }


            var objCmd2 = objDB.GetStoredProcCommand("ili_sp_RetornarEmpresasPrestador");

            objDB.AddInParameter(objCmd2, "IdPrestador", System.Data.DbType.Int32, idPrestador);
            objDB.AddInParameter(objCmd2, "IdJuridicaPapel", System.Data.DbType.Int32, idJuridicaPapel);
            objDB.AddInParameter(objCmd2, "NomeFuncionario", System.Data.DbType.String, nomeFuncionario);
            objDB.AddInParameter(objCmd2, "Ativo", System.Data.DbType.String, "T");

            var readerRetorno2 = objDB.ExecuteReader(objCmd2);

            while (readerRetorno2.Read())
            {
                EmpresaDTO empresa2 = new EmpresaDTO();

                if (readerRetorno2["AtivarLocalDeTrabalho"] != null && readerRetorno2["AtivarLocalDeTrabalho"].ToString() != String.Empty)
                    empresa2.AtivarLocalDeTrabalho = Convert.ToInt32(readerRetorno2["AtivarLocalDeTrabalho"]);


                if (readerRetorno2["IdJuridica"] != null && readerRetorno2["IdJuridica"].ToString() != null)
                    empresa2.IdJuridica = Convert.ToInt32(readerRetorno2["IdJuridica"]);


                if (empresa2.AtivarLocalDeTrabalho > 0)
                {
                    Database objDBFiliais = new SqlDatabase(ConfigurationManager.ConnectionStrings["ConexaoIliteraOpsa"].ConnectionString);

                    var objCmdFilial = objDBFiliais.GetStoredProcCommand("ili_sp_RetornarEmpresasAtivarLocalTrabalho_Matricula");

                    objDBFiliais.AddInParameter(objCmdFilial, "IdJuridica", System.Data.DbType.Int32, empresa2.IdJuridica);
                    objDBFiliais.AddInParameter(objCmdFilial, "IdPrestador", System.Data.DbType.Int32, idPrestador);
                    objDBFiliais.AddInParameter(objCmdFilial, "Matricula", System.Data.DbType.String, Matricula);

                    var readerRetornoFilial = objDBFiliais.ExecuteReader(objCmdFilial);

                    while (readerRetornoFilial.Read())
                    {
                        EmpresaDTO empresaFilial = new EmpresaDTO();

                        empresaFilial.NomeAbreviado = readerRetornoFilial["NomeAbreviado"].ToString();
                        empresaFilial.NomeCompleto = readerRetornoFilial["NomeCompleto"].ToString();

                        if (readerRetornoFilial["IdPessoa"] != null && readerRetornoFilial["IdPessoa"].ToString() != String.Empty)
                            empresaFilial.IdPessoa = Convert.ToInt32(readerRetornoFilial["IdPessoa"]);

                        if (readerRetornoFilial["IndTipoPessoa"] != null && readerRetornoFilial["IndTipoPessoa"].ToString() != String.Empty)
                            empresaFilial.IndTipoPessoa = Convert.ToInt32(readerRetornoFilial["IndTipoPessoa"]);

                        if (readerRetornoFilial["IdCliente"] != null && readerRetornoFilial["IdCliente"].ToString() != String.Empty)
                            empresaFilial.IdCliente = Convert.ToInt32(readerRetornoFilial["IdCliente"]);

                        if (readerRetornoFilial["AtivarLocalDeTrabalho"] != null && readerRetornoFilial["AtivarLocalDeTrabalho"].ToString() != String.Empty)
                            empresaFilial.AtivarLocalDeTrabalho = Convert.ToInt32(readerRetornoFilial["AtivarLocalDeTrabalho"]);

                        if (readerRetornoFilial["IdJuridica"] != null && readerRetornoFilial["IdJuridica"].ToString() != String.Empty)
                            empresaFilial.IdJuridica = Convert.ToInt32(readerRetornoFilial["IdJuridica"]);

                        if (readerRetornoFilial["QtdEmpregados"] != null && readerRetornoFilial["QtdEmpregados"].ToString() != String.Empty)
                            empresaFilial.QtdEmpregados = Convert.ToInt32(readerRetornoFilial["QtdEmpregados"]);

                        if (readerRetornoFilial["DataCadastro"] != null && readerRetornoFilial["DataCadastro"].ToString() != String.Empty)
                            empresaFilial.DataCadastro = Convert.ToDateTime(readerRetornoFilial["DataCadastro"]);

                        if (readerRetornoFilial["IdJuridicaPai"] != null && readerRetornoFilial["IdJuridicaPai"].ToString() != String.Empty)
                            empresaFilial.IdJuridicaPai = Convert.ToInt32(readerRetornoFilial["IdJuridicaPai"]);

                        lstEmpresa.Add(empresaFilial);

                        if (lstEmpresa.Count >= 1150)
                        {
                            return lstEmpresa;
                        }

                    }
                }
            }

            return lstEmpresa;


        }




        public EmpresaDTO RetornarDadosEmpresa(int idPessoa)
        {
            EmpresaDTO empresaDto = new EmpresaDTO();

            Database objDB = new SqlDatabase(ConfigurationManager.ConnectionStrings["ConexaoIliteraOpsa"].ConnectionString);

            var objCmd = objDB.GetStoredProcCommand("ili_sp_RetornarEmpresasDadosCadastrais");

            objDB.AddInParameter(objCmd, "IdPessoa", System.Data.DbType.Int32, idPessoa);

            var readerRetorno = objDB.ExecuteReader(objCmd);

            while (readerRetorno.Read())
            {
                empresaDto.IdJuridica = Convert.ToInt32(readerRetorno["IdJuridica"]);

                if(readerRetorno["IdPessoa"] != null && readerRetorno["IdPessoa"].ToString() != String.Empty)
                    empresaDto.IdPessoa = Convert.ToInt32(readerRetorno["IdPessoa"]);

                empresaDto.NomeCodigo = readerRetorno["NomeCodigo"].ToString();
                empresaDto.NomeAbreviado = readerRetorno["NomeAbreviado"].ToString();
                empresaDto.NomeCompleto = readerRetorno["NomeCompleto"].ToString();

                if (readerRetorno["IndTipoPessoa"] != null && readerRetorno["IndTipoPessoa"].ToString() != String.Empty)
                    empresaDto.IndTipoPessoa = Convert.ToInt32(readerRetorno["IndTipoPessoa"]);
                
                empresaDto.Email = readerRetorno["Email"].ToString();
                empresaDto.Site = readerRetorno["Site"].ToString();

                if(readerRetorno["DataCadastro"] != null && readerRetorno["DataCadastro"].ToString() != String.Empty)
                    empresaDto.DataCadastro = Convert.ToDateTime(readerRetorno["DataCadastro"]);

                empresaDto.CNPJ = readerRetorno["CNPJ"].ToString();
                empresaDto.Atividade = readerRetorno["Atividade"].ToString();
                empresaDto.IE = readerRetorno["IE"].ToString();
                empresaDto.CCM = readerRetorno["CCM"].ToString();

                if(readerRetorno["QtdEmpregados"] != null && readerRetorno["QtdEmpregados"].ToString() != String.Empty)
                    empresaDto.QtdEmpregados = Convert.ToInt32(readerRetorno["QtdEmpregados"]);

                if(readerRetorno["IdGrupoEmpresa"] != null && readerRetorno["IdGrupoEmpresa"].ToString() != String.Empty)
                    empresaDto.IdGrupoEmpresa = Convert.ToInt32(readerRetorno["IdGrupoEmpresa"]);

                empresaDto.Observacao = readerRetorno["Observacao"].ToString();
                empresaDto.Diretor = readerRetorno["Diretor"].ToString();

                if (readerRetorno["IdCNAE"] != null && readerRetorno["IdCNAE"].ToString() != String.Empty)
                    empresaDto.IdCnae = Convert.ToInt32(readerRetorno["IdCNAE"]);
            }

            readerRetorno.Close();

            return empresaDto;
        }


        public string RetornarDadosPreposto(int idEmpresa)
        {

            string xRet="";

            Database objDB = new SqlDatabase(ConfigurationManager.ConnectionStrings["ConexaoIliteraOpsa"].ConnectionString);

            var objCmd = objDB.GetStoredProcCommand("ili_sp_RetornarPreposto");

            objDB.AddInParameter(objCmd, "IdEmpresa", System.Data.DbType.Int32, idEmpresa);

            var readerRetorno = objDB.ExecuteReader(objCmd);

            while (readerRetorno.Read())
            {
                xRet = readerRetorno["Preposto"].ToString();
            }

            readerRetorno.Close();

            return xRet;
        }


        public string RetornarDadosPreposto2(int idEmpresa)
        {

            string xRet = "";

            Database objDB = new SqlDatabase(ConfigurationManager.ConnectionStrings["ConexaoIliteraOpsa"].ConnectionString);

            var objCmd = objDB.GetStoredProcCommand("ili_sp_RetornarPreposto2");

            objDB.AddInParameter(objCmd, "IdEmpresa", System.Data.DbType.Int32, idEmpresa);

            var readerRetorno = objDB.ExecuteReader(objCmd);

            while (readerRetorno.Read())
            {
                xRet = readerRetorno["Preposto"].ToString();
            }

            readerRetorno.Close();

            return xRet;
        }


    }
}
