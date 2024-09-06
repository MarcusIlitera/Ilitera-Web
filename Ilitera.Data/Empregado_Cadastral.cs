using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using Entities;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Configuration;
using Ilitera.Data.SQLServer;
using System.Data;
using System.Data.SqlClient;



namespace Ilitera.Data
{
    public class Empregado_Cadastral
    {

        public void Atualizar_Dados_Empregado(int xId, string xCTPS_Num, string xCTPS_Serie, string  xCTPS_UF, string xMatricula, string xRG, string xDataAdmissao, string xDataDemissao, string xDataNascimento, string xPISPASEP, string xCPF, string xApelidoEmpregado, string xNum_Foto, string xSexo, int xId_Usuario, string xBeneficiario, string xEndereco, string xNumero, string xComplemento, string xBairro, string xMunicipio, string xUF, string xCEP, string xEmail, string xTerceiro, string xObs )
        {


            using (SqlConnection cnn = new SqlConnection(Ilitera.Data.SQLServer.EntitySQLServer.GetConnection_Sied()))
            {

                string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection_Sied();

                Database objDB = new SqlDatabase(connString);

                var objCmd = objDB.GetStoredProcCommand("sp_Atualizar_Dados_Empregado");

                objDB.AddInParameter(objCmd, "xId", System.Data.DbType.Int32, xId);
                objDB.AddInParameter(objCmd, "xCTPS_Num", System.Data.DbType.String, xCTPS_Num);
                objDB.AddInParameter(objCmd, "xCTPS_Serie", System.Data.DbType.String, xCTPS_Serie);
                objDB.AddInParameter(objCmd, "xCTPS_UF", System.Data.DbType.String, xCTPS_UF);
                objDB.AddInParameter(objCmd, "xMatricula", System.Data.DbType.String, xMatricula);
                objDB.AddInParameter(objCmd, "xRG", System.Data.DbType.String, xRG);
                objDB.AddInParameter(objCmd, "xDataAdmissao", System.Data.DbType.String, xDataAdmissao);
                objDB.AddInParameter(objCmd, "xDataDemissao", System.Data.DbType.String, xDataDemissao);
                objDB.AddInParameter(objCmd, "xDataNascimento", System.Data.DbType.String, xDataNascimento);
                objDB.AddInParameter(objCmd, "xPISPASEP", System.Data.DbType.String, xPISPASEP);
                objDB.AddInParameter(objCmd, "xCPF", System.Data.DbType.String, xCPF);
                objDB.AddInParameter(objCmd, "xApelido", System.Data.DbType.String, xApelidoEmpregado);
                objDB.AddInParameter(objCmd, "xNumFoto", System.Data.DbType.String,  xNum_Foto  );
                objDB.AddInParameter(objCmd, "xSexo", System.Data.DbType.String, xSexo);
                objDB.AddInParameter(objCmd, "xBeneficiario", System.Data.DbType.String, xBeneficiario);
                
                objDB.AddInParameter(objCmd, "xEndereco", System.Data.DbType.String, xEndereco);
                objDB.AddInParameter(objCmd, "xNumero", System.Data.DbType.String, xNumero);
                objDB.AddInParameter(objCmd, "xComplemento", System.Data.DbType.String, xComplemento);
                objDB.AddInParameter(objCmd, "xBairro", System.Data.DbType.String, xBairro);
                objDB.AddInParameter(objCmd, "xMunicipio", System.Data.DbType.String, xMunicipio);
                objDB.AddInParameter(objCmd, "xUF", System.Data.DbType.String, xUF);
                objDB.AddInParameter(objCmd, "xCEP", System.Data.DbType.String, xCEP);
                objDB.AddInParameter(objCmd, "xEmail", System.Data.DbType.String, xEmail);
                objDB.AddInParameter(objCmd, "xTerceiro", System.Data.DbType.String, xTerceiro);
                objDB.AddInParameter(objCmd, "xObs", System.Data.DbType.String, xObs);

                var readerRetorno = objDB.ExecuteReader(objCmd);

                Log_Web("exec sp_Atualizar_Dados_Empregado " + xId.ToString() + " , " + xCTPS_Num + " , " + xCTPS_Serie + " , " + xCTPS_UF + " , " + xMatricula  + " , " + xRG  + " , " 
                        + xDataAdmissao + " , " +  xDataDemissao + " , " + xDataNascimento + " , " + xPISPASEP + " , " + xCPF + " , " + xApelidoEmpregado + " , " + xNum_Foto + " , " + xSexo + " , " + xBeneficiario + " , " + xEmail + " , " + xObs, xId_Usuario, "Alteração de dados de Colaborador - Web");

                return;

            }
        }


        public void Atualizar_Nome_Empregado(int xId, string xNome, Int32 xId_Usuario)
        {


            using (SqlConnection cnn = new SqlConnection(Ilitera.Data.SQLServer.EntitySQLServer.GetConnection_Sied()))
            {

                string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection_Sied();

                Database objDB = new SqlDatabase(connString);

                var objCmd = objDB.GetStoredProcCommand("sp_Atualizar_Nome_Empregado");

                objDB.AddInParameter(objCmd, "xId", System.Data.DbType.Int32, xId);
                objDB.AddInParameter(objCmd, "xNome", System.Data.DbType.String, xNome);

                var readerRetorno = objDB.ExecuteReader(objCmd);

                Log_Web("exec sp_Atualizar_Nome_Empregado " + xId.ToString() + " , " + xNome, xId_Usuario, "Alteração de dados de Colaborador - Web");

                return;

            }
        }


        public Int32 Inserir_Dados_Empregado(string xEmpresa, string xNome, string xSexo, string xCTPS_Num, string xCTPS_Serie, string xCTPS_UF, string xMatricula, string xRG, string xDataAdmissao, string xDataDemissao, string xDataNascimento, string xPISPASEP, string xCPF, string xApelidoEmpregado,  Int32 xIdUsuario, string xBeneficiario, string xEmail )
        {
            Int32 xRet = 0;

            using (SqlConnection cnn = new SqlConnection(Ilitera.Data.SQLServer.EntitySQLServer.GetConnection_Sied()))
            {

                string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection_Sied();

                Database objDB = new SqlDatabase(connString);

                var objCmd = objDB.GetStoredProcCommand("sp_Inserir_Dados_Empregado");

                objDB.AddInParameter(objCmd, "xEmpresa", System.Data.DbType.Int32, System.Convert.ToInt32( xEmpresa ));
                objDB.AddInParameter(objCmd, "xNome", System.Data.DbType.String, xNome);
                objDB.AddInParameter(objCmd, "xSexo", System.Data.DbType.String, xSexo);
                objDB.AddInParameter(objCmd, "xCTPS_Num", System.Data.DbType.String, xCTPS_Num);
                objDB.AddInParameter(objCmd, "xCTPS_Serie", System.Data.DbType.String, xCTPS_Serie);
                objDB.AddInParameter(objCmd, "xCTPS_UF", System.Data.DbType.String, xCTPS_UF);
                objDB.AddInParameter(objCmd, "xMatricula", System.Data.DbType.String, xMatricula);
                objDB.AddInParameter(objCmd, "xRG", System.Data.DbType.String, xRG);
                objDB.AddInParameter(objCmd, "xDataAdmissao", System.Data.DbType.String, xDataAdmissao);
                objDB.AddInParameter(objCmd, "xDataDemissao", System.Data.DbType.String, xDataDemissao);
                objDB.AddInParameter(objCmd, "xDataNascimento", System.Data.DbType.String, xDataNascimento);
                objDB.AddInParameter(objCmd, "xPISPASEP", System.Data.DbType.String, xPISPASEP);
                objDB.AddInParameter(objCmd, "xCPF", System.Data.DbType.String, xCPF);
                objDB.AddInParameter(objCmd, "xApelido", System.Data.DbType.String, xApelidoEmpregado);
                objDB.AddInParameter(objCmd, "xBeneficiario", System.Data.DbType.String, xBeneficiario);
                objDB.AddInParameter(objCmd, "xEmail", System.Data.DbType.String, xEmail);

                var readerRetorno = objDB.ExecuteReader(objCmd);

                while (readerRetorno.Read())
                {
                    xRet = Convert.ToInt32(readerRetorno["Retorno"]);

                }


                Log_Web("exec sp_Inserir_Dados_Empregado " + xEmpresa.ToString() + " , " + xNome + " , " +  xSexo + " , " + xCTPS_Num + " , " + xCTPS_Serie + " , " + xCTPS_UF + " , " + xMatricula + " , " + xRG + " , "
                        + xDataAdmissao + " , " + xDataDemissao + " , " + xDataNascimento + " , " + xPISPASEP + " , " + xCPF + " , " + xApelidoEmpregado + " , " + xBeneficiario, xIdUsuario, "Inserção de dados de Colaborador - Web");


                return xRet;
                

            }
        }


        public void Inserir_Classificacao_Funcional(int xId, int xEmpresa, string xData1, string xData2, string xCargo, string xSetor, string xFuncao, Int32 xIdUsuario, string xCC )
        {


            using (SqlConnection cnn = new SqlConnection(Ilitera.Data.SQLServer.EntitySQLServer.GetConnection_Sied()))
            {

                string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection_Sied();

                Database objDB = new SqlDatabase(connString);

                var objCmd = objDB.GetStoredProcCommand("sp_Inserir_Classificacao_Funcional");

                objDB.AddInParameter(objCmd, "xId", System.Data.DbType.Int32, xId);
                objDB.AddInParameter(objCmd, "xEmpresa", System.Data.DbType.Int32, xEmpresa);
                objDB.AddInParameter(objCmd, "xData_Inicio", System.Data.DbType.String, xData1);
                objDB.AddInParameter(objCmd, "xData_Termino", System.Data.DbType.String, xData2);
                objDB.AddInParameter(objCmd, "xCargo", System.Data.DbType.String, xCargo);
                objDB.AddInParameter(objCmd, "xSetor", System.Data.DbType.String, xSetor);
                objDB.AddInParameter(objCmd, "xFuncao", System.Data.DbType.String, xFuncao);
                objDB.AddInParameter(objCmd, "xCC", System.Data.DbType.String, xCC);

                var readerRetorno = objDB.ExecuteReader(objCmd);


                Log_Web("exec sp_Inserir_Classificacao_Funcional " + xId.ToString() + " , " + xEmpresa + " , " + xData1 + " , " + xData2 + " , " + xCargo + " , " + xSetor + " , "
                        + xFuncao , xIdUsuario, "Inserção de Classif.Funcional - Web");

                return;

            }
        }


        public void Inserir_Classificacao_Funcional2(int xId, int xEmpresa, string xData1, string xData2, string xCargo, string xSetor, string xFuncao, Int32 xIdUsuario, string xCC, string xCodigo)
        {


            using (SqlConnection cnn = new SqlConnection(Ilitera.Data.SQLServer.EntitySQLServer.GetConnection_Sied()))
            {

                string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection_Sied();

                Database objDB = new SqlDatabase(connString);

                var objCmd = objDB.GetStoredProcCommand("sp_Inserir_Classificacao_Funcional2");

                objDB.AddInParameter(objCmd, "xId", System.Data.DbType.Int32, xId);
                objDB.AddInParameter(objCmd, "xEmpresa", System.Data.DbType.Int32, xEmpresa);
                objDB.AddInParameter(objCmd, "xData_Inicio", System.Data.DbType.String, xData1);
                objDB.AddInParameter(objCmd, "xData_Termino", System.Data.DbType.String, xData2);
                objDB.AddInParameter(objCmd, "xCargo", System.Data.DbType.String, xCargo);
                objDB.AddInParameter(objCmd, "xSetor", System.Data.DbType.String, xSetor);
                objDB.AddInParameter(objCmd, "xFuncao", System.Data.DbType.String, xFuncao);
                objDB.AddInParameter(objCmd, "xCC", System.Data.DbType.String, xCC);
                objDB.AddInParameter(objCmd, "xCodigo", System.Data.DbType.String, xCodigo);

                var readerRetorno = objDB.ExecuteReader(objCmd);


                Log_Web("exec sp_Inserir_Classificacao_Funcional " + xId.ToString() + " , " + xEmpresa + " , " + xData1 + " , " + xData2 + " , " + xCargo + " , " + xSetor + " , "
                        + xFuncao, xIdUsuario, "Inserção de Classif.Funcional - Web");

                return;

            }
        }


        public void Inserir_Classificacao_Funcional_Jornada(int xId, int xEmpresa, string xData1, string xData2, string xCargo, string xSetor, string xFuncao, Int32 xIdUsuario, string xCC, string xJornada)
        {


            using (SqlConnection cnn = new SqlConnection(Ilitera.Data.SQLServer.EntitySQLServer.GetConnection_Sied()))
            {

                string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection_Sied();

                Database objDB = new SqlDatabase(connString);

                var objCmd = objDB.GetStoredProcCommand("sp_Inserir_Classificacao_Funcional_Jornada");

                objDB.AddInParameter(objCmd, "xId", System.Data.DbType.Int32, xId);
                objDB.AddInParameter(objCmd, "xEmpresa", System.Data.DbType.Int32, xEmpresa);
                objDB.AddInParameter(objCmd, "xData_Inicio", System.Data.DbType.String, xData1);
                objDB.AddInParameter(objCmd, "xData_Termino", System.Data.DbType.String, xData2);
                objDB.AddInParameter(objCmd, "xCargo", System.Data.DbType.String, xCargo);
                objDB.AddInParameter(objCmd, "xSetor", System.Data.DbType.String, xSetor);
                objDB.AddInParameter(objCmd, "xFuncao", System.Data.DbType.String, xFuncao);
                objDB.AddInParameter(objCmd, "xJornada", System.Data.DbType.String, xJornada);
                objDB.AddInParameter(objCmd, "xCC", System.Data.DbType.String, xCC);

                var readerRetorno = objDB.ExecuteReader(objCmd);


                Log_Web("exec sp_Inserir_Classificacao_Funcional_Jornada " + xId.ToString() + " , " + xEmpresa + " , " + xData1 + " , " + xData2 + " , " + xCargo + " , " + xSetor + " , "
                        + xFuncao, xIdUsuario, "Inserção de Classif.Funcional - Web");

                return;

            }
        }


        public void Inserir_Classificacao_Funcional_Jornada2(int xId, int xEmpresa, string xData1, string xData2, string xCargo, string xSetor, string xFuncao, Int32 xIdUsuario, string xCC, string xJornada, string xCodigo)
        {


            using (SqlConnection cnn = new SqlConnection(Ilitera.Data.SQLServer.EntitySQLServer.GetConnection_Sied()))
            {

                string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection_Sied();

                Database objDB = new SqlDatabase(connString);

                var objCmd = objDB.GetStoredProcCommand("sp_Inserir_Classificacao_Funcional_Jornada2");

                objDB.AddInParameter(objCmd, "xId", System.Data.DbType.Int32, xId);
                objDB.AddInParameter(objCmd, "xEmpresa", System.Data.DbType.Int32, xEmpresa);
                objDB.AddInParameter(objCmd, "xData_Inicio", System.Data.DbType.String, xData1);
                objDB.AddInParameter(objCmd, "xData_Termino", System.Data.DbType.String, xData2);
                objDB.AddInParameter(objCmd, "xCargo", System.Data.DbType.String, xCargo);
                objDB.AddInParameter(objCmd, "xSetor", System.Data.DbType.String, xSetor);
                objDB.AddInParameter(objCmd, "xFuncao", System.Data.DbType.String, xFuncao);
                objDB.AddInParameter(objCmd, "xJornada", System.Data.DbType.String, xJornada);
                objDB.AddInParameter(objCmd, "xCC", System.Data.DbType.String, xCC);
                objDB.AddInParameter(objCmd, "xCodigo", System.Data.DbType.String, xCodigo);

                var readerRetorno = objDB.ExecuteReader(objCmd);


                Log_Web("exec sp_Inserir_Classificacao_Funcional_Jornada2 " + xId.ToString() + " , " + xEmpresa + " , " + xData1 + " , " + xData2 + " , " + xCargo + " , " + xSetor + " , "
                        + xFuncao + " , " + xJornada + " , " + xCC + " , " + xCodigo, xIdUsuario, "Inserção de Classif.Funcional - Web");

                return;

            }
        }


        public void Atualizar_Classificacao_Funcional(int xId, int xEmpresa, string xData1, string xData2, string xCargo, string xSetor, string xFuncao, Int32 xIdUsuario, string xCC)
        {


            using (SqlConnection cnn = new SqlConnection(Ilitera.Data.SQLServer.EntitySQLServer.GetConnection_Sied()))
            {
                //try
                //{

                    string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection_Sied();

                    Database objDB = new SqlDatabase(connString);

                    var objCmd = objDB.GetStoredProcCommand("sp_Atualizar_Classificacao_Funcional");

                    objDB.AddInParameter(objCmd, "xId", System.Data.DbType.Int32, xId);
                    objDB.AddInParameter(objCmd, "xEmpresa", System.Data.DbType.Int32, xEmpresa);
                    objDB.AddInParameter(objCmd, "xData_Inicio", System.Data.DbType.String, xData1);
                    objDB.AddInParameter(objCmd, "xData_Termino", System.Data.DbType.String, xData2);
                    objDB.AddInParameter(objCmd, "xCargo", System.Data.DbType.String, xCargo);
                    objDB.AddInParameter(objCmd, "xSetor", System.Data.DbType.String, xSetor);
                    objDB.AddInParameter(objCmd, "xFuncao", System.Data.DbType.String, xFuncao);
                    objDB.AddInParameter(objCmd, "xCC", System.Data.DbType.String, xCC);

                    var readerRetorno = objDB.ExecuteReader(objCmd);


                    Log_Web("exec sp_Atualizar_Classificacao_Funcional " + xId.ToString() + " , " + xEmpresa + " , " + xData1 + " , " + xData2 + " , " + xCargo + " , " + xSetor + " , "
                           + xFuncao + " , " + xCC, xIdUsuario, "Atualização de Classif.Funcional - Web");

                //}
                //catch (Exception ex)
                //{
                //    string zErro = "";

                //    zErro = ex.ToString();
                //}


                return;

            }
        }




        public void Atualizar_Classificacao_Funcional2(int xId, int xEmpresa, string xData1, string xData2, string xCargo, string xSetor, string xFuncao, Int32 xIdUsuario, string xCC, string xJornada)
        {


            using (SqlConnection cnn = new SqlConnection(Ilitera.Data.SQLServer.EntitySQLServer.GetConnection_Sied()))
            {
                //try
                //{

                string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection_Sied();

                Database objDB = new SqlDatabase(connString);

                var objCmd = objDB.GetStoredProcCommand("sp_Atualizar_Classificacao_Funcional2");

                objDB.AddInParameter(objCmd, "xId", System.Data.DbType.Int32, xId);
                objDB.AddInParameter(objCmd, "xEmpresa", System.Data.DbType.Int32, xEmpresa);
                objDB.AddInParameter(objCmd, "xData_Inicio", System.Data.DbType.String, xData1);
                objDB.AddInParameter(objCmd, "xData_Termino", System.Data.DbType.String, xData2);
                objDB.AddInParameter(objCmd, "xCargo", System.Data.DbType.String, xCargo);
                objDB.AddInParameter(objCmd, "xSetor", System.Data.DbType.String, xSetor);
                objDB.AddInParameter(objCmd, "xFuncao", System.Data.DbType.String, xFuncao);
                objDB.AddInParameter(objCmd, "xCC", System.Data.DbType.String, xCC);
                objDB.AddInParameter(objCmd, "xJornada", System.Data.DbType.String, xJornada);

                var readerRetorno = objDB.ExecuteReader(objCmd);


                Log_Web("exec sp_Atualizar_Classificacao_Funcional2 " + xId.ToString() + " , " + xEmpresa + " , " + xData1 + " , " + xData2 + " , " + xCargo + " , " + xSetor + " , "
                       + xFuncao + " , " + xCC, xIdUsuario, "Atualização de Classif.Funcional - Web");

                //}
                //catch (Exception ex)
                //{
                //    string zErro = "";

                //    zErro = ex.ToString();
                //}


                return;

            }
        }


        public void Atualizar_Classificacao_Funcional4(int xId, int xEmpresa, string xData1, string xData2, string xCargo, string xSetor, string xFuncao, Int32 xIdUsuario, string xCC, string xJornada, string xCodigo)
        {


            using (SqlConnection cnn = new SqlConnection(Ilitera.Data.SQLServer.EntitySQLServer.GetConnection_Sied()))
            {
                //try
                //{

                string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection_Sied();

                Database objDB = new SqlDatabase(connString);

                var objCmd = objDB.GetStoredProcCommand("sp_Atualizar_Classificacao_Funcional4");

                objDB.AddInParameter(objCmd, "xId", System.Data.DbType.Int32, xId);
                objDB.AddInParameter(objCmd, "xEmpresa", System.Data.DbType.Int32, xEmpresa);
                objDB.AddInParameter(objCmd, "xData_Inicio", System.Data.DbType.String, xData1);
                objDB.AddInParameter(objCmd, "xData_Termino", System.Data.DbType.String, xData2);
                objDB.AddInParameter(objCmd, "xCargo", System.Data.DbType.String, xCargo);
                objDB.AddInParameter(objCmd, "xSetor", System.Data.DbType.String, xSetor);
                objDB.AddInParameter(objCmd, "xFuncao", System.Data.DbType.String, xFuncao);
                objDB.AddInParameter(objCmd, "xCC", System.Data.DbType.String, xCC);
                objDB.AddInParameter(objCmd, "xJornada", System.Data.DbType.String, xJornada);
                objDB.AddInParameter(objCmd, "xCodigo", System.Data.DbType.String, xCodigo);

                var readerRetorno = objDB.ExecuteReader(objCmd);


                Log_Web("exec sp_Atualizar_Classificacao_Funcional4 " + xId.ToString() + " , " + xEmpresa + " , " + xData1 + " , " + xData2 + " , " + xCargo + " , " + xSetor + " , "
                       + xFuncao + " , " + xCC + " , " + xJornada + " , " + xCodigo, xIdUsuario, "Atualização de Classif.Funcional - Web");

                //}
                //catch (Exception ex)
                //{
                //    string zErro = "";

                //    zErro = ex.ToString();
                //}


                return;

            }
        }


        public void Apagar_Classificacao_Funcional(int xId, int xEmpresa, string xCargo, string xSetor, string xFuncao, Int32 xIdUsuario)
        {


            using (SqlConnection cnn = new SqlConnection(Ilitera.Data.SQLServer.EntitySQLServer.GetConnection_Sied()))
            {

                string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection_Sied();

                Database objDB = new SqlDatabase(connString);

                var objCmd = objDB.GetStoredProcCommand("sp_Excluir_Classificacao_Funcional");

                objDB.AddInParameter(objCmd, "xId", System.Data.DbType.Int32, xId);
                objDB.AddInParameter(objCmd, "xEmpresa", System.Data.DbType.Int32, xEmpresa);
                objDB.AddInParameter(objCmd, "xCargo", System.Data.DbType.String, xCargo.Trim());
                objDB.AddInParameter(objCmd, "xSetor", System.Data.DbType.String, xSetor.Trim());
                objDB.AddInParameter(objCmd, "xFuncao", System.Data.DbType.String, xFuncao.Trim());

                var readerRetorno = objDB.ExecuteReader(objCmd);

                Log_Web("exec sp_Excluir_Classificacao_Funcional " + xId.ToString() + " , " + xEmpresa + " , " + xCargo + " , " + xSetor + " , "
                        + xFuncao, xIdUsuario, "Exclusão de Classif.Funcional - Web");


                return;

            }
        }



        public void Atualizar_Descricao_Funcao(int xFuncao, string xDescricao, string xCBO, Int32 xIdUsuario)
        {


            using (SqlConnection cnn = new SqlConnection(Ilitera.Data.SQLServer.EntitySQLServer.GetConnection()))
            {

                string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection();

                Database objDB = new SqlDatabase(connString);

                var objCmd = objDB.GetStoredProcCommand("sp_Atualizar_Descricao_Funcao");

                objDB.AddInParameter(objCmd, "xFuncao", System.Data.DbType.Int32, xFuncao);
                objDB.AddInParameter(objCmd, "xDescricao", System.Data.DbType.String, xDescricao);
                objDB.AddInParameter(objCmd, "xCBO", System.Data.DbType.String, xCBO);

                var readerRetorno = objDB.ExecuteReader(objCmd);

                Log_Web("exec sp_Atualizar_Descricao_Funcao " + xFuncao.ToString() + " , " + xDescricao + " , " + xCBO, xIdUsuario, "Atualização de Descrição/Função - Web");


                return;

            }
        }




        private void Log_Web(string Command,
                            int IdUsuario,
                            string ProcessoRealizado)
        {

            using (SqlConnection cnn = new SqlConnection(Ilitera.Data.SQLServer.EntitySQLServer.GetConnection()))
            {

                string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection();



                string strLog = "USE logdb exec dbo.sps_AddLog_OPSA "
                                + IdUsuario + ","
                                + "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',"
                                + "'0',"
                                + "'0',"
                                + "0,"
                                + "1,"
                                + "'" + Command.Replace("'", "''") + "',"
                                + "'" + ProcessoRealizado + "'";

                try
                {
                    //Debug.WriteLine(strLog);
                    cnn.ConnectionString = connString;
                    cnn.Open();

                    SqlCommand cmd = new SqlCommand();

                    cmd.CommandText = strLog;

                    cmd.Connection = cnn;

                    cmd.ExecuteNonQuery(); //DESENVOLVIMENTO ILITERA - linha ao lado comentada (acessava tab de logs) 29/07/10
                }
                catch (Exception ex)
                {
                    string zErro = "";
                    zErro = ex.Message;
                }

            }

        }


        public bool Criar_Alocacao_Funcao_GHE(Int32 xIdCliente, string xFuncao, Int32 xIdEmpregado)
        {
            //string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection();
            StringBuilder strSQL = new StringBuilder();

            //DataSet m_ds;


            DataTable table = new DataTable("Result");
            table.Columns.Add("Setor", Type.GetType("System.String"));
            table.Columns.Add("Funcionarios", Type.GetType("System.Int16"));
            table.Columns.Add("Valor_Setor", Type.GetType("System.Single"));


            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);


            strSQL.Append(" select top 1  ");
            strSQL.Append(" ( select top 1 nid_func_empregado -1 ");
            strSQL.Append("   from tblfunc_empregado (nolock) ");
            strSQL.Append("   where nid_func_empregado between -2145000000 and 2145000000 ");
            strSQL.Append("   and nid_func_empregado - 1 not in ");
            strSQL.Append("   (select nid_func_empregado from tblfunc_empregado (nolock) ) ");
            strSQL.Append("   order by abs(nid_func_empregado) ) as nid_Empregado_Funcao, c.nid_laud_tec, ");
            strSQL.Append(" (select top 1 nid_empregado_funcao from tblempregado_funcao (nolock) where nid_empregado = " + xIdEmpregado.ToString() + " and nID_FUNCAO in ");
            strSQL.Append("    (select nid_funcao from tblfuncao where tno_func_empr = '" + xFuncao + "' and nid_empr = " + xIdCliente.ToString() + ") ");
            strSQL.Append(" ) as nid_Empregado_Funcao, nid_Func ");
            strSQL.Append(" from tblfuncao_ghe(nolock) as a ");
            strSQL.Append(" join tblfunc  (nolock) as b on(a.ghe = b.tno_func) ");
            strSQL.Append(" join tbllaudo_tec(nolock) as c on(b.nid_laud_tec = c.nid_laud_tec) ");
            strSQL.Append(" where a.nid_Empr = " + xIdCliente.ToString() + " ");
            strSQL.Append(" and Funcao = '" + xFuncao + "' ");
            strSQL.Append(" and c.nid_laud_tec in ");
            strSQL.Append(" (select top 1 nid_laud_tec from tbllaudo_tec  (nolock) where nid_empr = " + xIdCliente.ToString() + " order by hdt_laudo desc ) ");



            //m_ds = new DataSet("Result");

            using (SqlConnection cnn = new SqlConnection(Ilitera.Data.SQLServer.EntitySQLServer.GetConnection_Sied()))
            {
                SqlDataAdapter da;
                cnn.Open();


                da = new SqlDataAdapter(strSQL.ToString(), cnn);
                da.Fill(m_ds, "Result");

                cnn.Close();

                da.Dispose();
            }


            if (m_ds.Tables[0].Rows.Count == 0)
            {
                return false;
            }
            else
            {
                //fazer insercao

                using (SqlConnection cnn = new SqlConnection(Ilitera.Data.SQLServer.EntitySQLServer.GetConnection_Sied()))
                {
                    cnn.Open();

                    SqlCommand com = new SqlCommand("INSERT INTO tblFunc_Empregado ( nId_Func_Empregado, nId_laud_tec, nId_Empregado_Funcao, nId_func ) " +
                    strSQL.ToString(), cnn);

                    com.ExecuteNonQuery();
                }


                return true;
            }

        }





        public DataSet Grafico_Anamnese(string xCampo, string xEmpresa, string xD1, string xD2, Int16 xEmp)
        {
            //string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection();
            StringBuilder strSQL = new StringBuilder();

            //DataSet m_ds;


            DataTable table = new DataTable("Result");
            table.Columns.Add("Indicador", Type.GetType("System.String"));
            table.Columns.Add("Valor", Type.GetType("System.String"));

            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);


            
            strSQL.Append(" select " + xCampo + " as Indicador, count(*) as Valor ");
            strSQL.Append(" from anamnese as a (nolock) ");
            strSQL.Append(" join examebase as b (nolock)on(a.idexamebase = b.idexamebase) ");
            strSQL.Append(" where " + xCampo + " is not null and " + xCampo + " in ( 1,2 ) and idempregado in ( ");
            strSQL.Append("   select nId_empregado from " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblempregado  ");


            if (xEmp == 1)
            {
                strSQL.Append(" WHERE nID_EMPR= " + xEmpresa + "  ");
            }
            else if (xEmp == 2)
            {
                strSQL.Append(" WHERE nID_EMPR in (  select idpessoa from juridica  (nolock) where idjuridica = " + xEmpresa + " or idjuridicapai = " + xEmpresa + " ) ");
            }
            else if (xEmp == 3)
            {
                strSQL.Append(" WHERE nID_EMPR in (  select idpessoa from juridica  (nolock) where idgrupoempresa in ( select idgrupoempresa from juridica  (nolock) where idpessoa = " + xEmpresa + " ) ) ");
            }

            strSQL.Append("   ) ");
            strSQL.Append(" and DataExame between convert(smalldatetime, '" + xD1 + " 00:01', 103 ) and convert(smalldatetime, '" + xD2 + " 23:59', 103 ) ");
            strSQL.Append(" group by " + xCampo );



            //m_ds = new DataSet("Result");

            using (SqlConnection cnn = new SqlConnection(Ilitera.Data.SQLServer.EntitySQLServer.GetConnection()))
            {
                SqlDataAdapter da;
                cnn.Open();


                da = new SqlDataAdapter(strSQL.ToString(), cnn);
                da.Fill(m_ds, "Result");

                cnn.Close();

                da.Dispose();
            }

            return m_ds;

        }


        public DataSet Grafico_Exame_Fisico(string xCampo, string xEmpresa, string xD1, string xD2, Int16 xEmp)
        {
            //string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection();
            StringBuilder strSQL = new StringBuilder();

            //DataSet m_ds;


            DataTable table = new DataTable("Result");
            table.Columns.Add("Indicador", Type.GetType("System.String"));
            table.Columns.Add("Valor", Type.GetType("System.String"));

            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);



            strSQL.Append(" select  " + xCampo + "  as Indicador, count(*) as Valor ");
            strSQL.Append(" from examefisico as a (nolock) ");
            strSQL.Append(" join examebase as b (nolock)on(a.idexamebase = b.idexamebase) ");
            strSQL.Append(" where " + xCampo + " is not null and " + xCampo + " in (1,2 ) and idempregado in ( ");
            strSQL.Append("   select nId_empregado from " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblempregado  ");


            if (xEmp == 1)
            {
                strSQL.Append(" WHERE nID_EMPR= " + xEmpresa + "  ");
            }
            else if (xEmp == 2)
            {
                strSQL.Append(" WHERE nID_EMPR in (  select idpessoa from juridica  (nolock) where idjuridica = " + xEmpresa + " or idjuridicapai = " + xEmpresa + " ) ");
            }
            else if (xEmp == 3)
            {
                strSQL.Append(" WHERE nID_EMPR in (  select idpessoa from juridica  (nolock) where idgrupoempresa in ( select idgrupoempresa from juridica  (nolock) where idpessoa = " + xEmpresa + " ) ) ");
            }

            strSQL.Append("   ) ");
            strSQL.Append(" and DataExame between convert(smalldatetime, '" + xD1 + " 00:01', 103 ) and convert(smalldatetime, '" + xD2 + " 23:59', 103 ) ");
            strSQL.Append(" group by " + xCampo );



            //m_ds = new DataSet("Result");

            using (SqlConnection cnn = new SqlConnection(Ilitera.Data.SQLServer.EntitySQLServer.GetConnection()))
            {
                SqlDataAdapter da;
                cnn.Open();


                da = new SqlDataAdapter(strSQL.ToString(), cnn);
                da.Fill(m_ds, "Result");

                cnn.Close();

                da.Dispose();
            }

            return m_ds;

        }




    }

}