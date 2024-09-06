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
    public class Laudo_Eletrico
    {


        public IDataReader Gerar_DS_Laudo(int xIdLaudo)
        {

            StringBuilder strSQL = new StringBuilder();
            string xSQL;

            string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection();

            Database objDB = new SqlDatabase(connString);

            strSQL.Append("select Descricao, convert( char(10), Data_Laudo,103 ) as xData, " ) ;
            strSQL.Append("Id_Responsavel, Id_Eletricista, Introducao, Conclusao, Id_Responsavel_Empresa, Indice, Objetivos, Normas, Generalidades from LaudoEletrico ");
            strSQL.Append("where IdLaudoEletrico = ");
            strSQL.Append(xIdLaudo.ToString());
            strSQL.Append(" ");

            xSQL = strSQL.ToString();

            
            var objCmd = objDB.GetSqlStringCommand(xSQL);
            var readerRetorno = objDB.ExecuteReader(objCmd);


            return readerRetorno;

        }


        public IDataReader Gerar_DS_Laudo_SPDA(int xIdLaudo)
        {

            StringBuilder strSQL = new StringBuilder();
            string xSQL;

            string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection();

            Database objDB = new SqlDatabase(connString);


            strSQL.Append("select Descricao, convert( char(10), Data_Laudo,103 ) as xData, Id_Responsavel, Id_Eletricista, Introducao, Conclusao, Recomendacoes, Observacoes, Id_Responsavel_Empresa, Croqui, Normas, Objetivos, Indice, Caracteristicas_Gerais from LaudoSPDA ");
            strSQL.Append("where IdLaudoSPDA = ");
            strSQL.Append(xIdLaudo.ToString());
            strSQL.Append(" ");

            xSQL = strSQL.ToString();


            var objCmd = objDB.GetSqlStringCommand(xSQL);
            var readerRetorno = objDB.ExecuteReader(objCmd);


            return readerRetorno;

        }



        public int SalvarLaudoEletrico(Int32 xLaudo, Int32 xIdPrestador, string xDescricao, string xData, Int32 xIdCliente, Int32 xIdEletricista, string xIntroducao, string xConclusao, Int32 xIdResponsavelEmpresa, string xIndice, string xObjetivos, string xGeneralidades, string xNormas)
        {
            string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection();
            int xStatus;

            xStatus = 0;

            //Database objDB = new SqlDatabase(ConfigurationManager.ConnectionStrings["ConexaoIliteraOpsa"].ConnectionString);
            Database objDB = new SqlDatabase(connString);

            var objCmd = objDB.GetStoredProcCommand("sp_Inserir_Laudo_Eletrico");

            objDB.AddInParameter(objCmd, "IdLaudo", System.Data.DbType.Int32, xLaudo);
            objDB.AddInParameter(objCmd, "IdPrestador", System.Data.DbType.Int32, xIdPrestador);
            objDB.AddInParameter(objCmd, "Descricao", System.Data.DbType.String, xDescricao);
            objDB.AddInParameter(objCmd, "Data_Laudo", System.Data.DbType.String, xData);
            objDB.AddInParameter(objCmd, "IdCliente", System.Data.DbType.Int32, xIdCliente);
            objDB.AddInParameter(objCmd, "IdEletricista", System.Data.DbType.Int32, xIdEletricista);
            objDB.AddInParameter(objCmd, "Introducao", System.Data.DbType.String, xIntroducao);
            objDB.AddInParameter(objCmd, "Conclusao", System.Data.DbType.String, xConclusao);
            objDB.AddInParameter(objCmd, "Objetivos", System.Data.DbType.String, xObjetivos);
            objDB.AddInParameter(objCmd, "Generalidades", System.Data.DbType.String, xGeneralidades);
            objDB.AddInParameter(objCmd, "Normas", System.Data.DbType.String, xNormas);
            objDB.AddInParameter(objCmd, "Indices", System.Data.DbType.String, xIndice);
            objDB.AddInParameter(objCmd, "IdResponsavelEmpresa", System.Data.DbType.Int32, xIdResponsavelEmpresa);

            var readerRetorno = objDB.ExecuteReader(objCmd);

            while (readerRetorno.Read())
            {
                xStatus = Convert.ToInt32(readerRetorno["Status"]);
            }


            return xStatus;
        }



        public int SalvarLaudo_SPDA(Int32 xLaudo, Int32 xIdPrestador, string xDescricao, string xData, Int32 xIdCliente, Int32 xIdEletricista, string xIntroducao, string xConclusao, string xRecomendacoes, string xObservacoes, Int32 xIdResponsavelEmpresa, string xCroqui, string xIndice, string xObjetivos, string xNormas, string xCaracteristicas_Gerais, string xCaracteristicas_Gerais_Foto1)
        {
            string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection();
            int xStatus;

            xStatus = 0;

            //Database objDB = new SqlDatabase(ConfigurationManager.ConnectionStrings["ConexaoIliteraOpsa"].ConnectionString);
            Database objDB = new SqlDatabase(connString);

            var objCmd = objDB.GetStoredProcCommand("sp_Inserir_Laudo_SPDA");

            objDB.AddInParameter(objCmd, "IdLaudo", System.Data.DbType.Int32, xLaudo);
            objDB.AddInParameter(objCmd, "IdPrestador", System.Data.DbType.Int32, xIdPrestador);
            objDB.AddInParameter(objCmd, "Descricao", System.Data.DbType.String, xDescricao);
            objDB.AddInParameter(objCmd, "Data_Laudo", System.Data.DbType.String, xData);
            objDB.AddInParameter(objCmd, "IdCliente", System.Data.DbType.Int32, xIdCliente);
            objDB.AddInParameter(objCmd, "IdEletricista", System.Data.DbType.Int32, xIdEletricista);
            objDB.AddInParameter(objCmd, "Introducao", System.Data.DbType.String, xIntroducao);
            objDB.AddInParameter(objCmd, "Conclusao", System.Data.DbType.String, xConclusao);
            objDB.AddInParameter(objCmd, "Recomendacoes", System.Data.DbType.String, xRecomendacoes);
            objDB.AddInParameter(objCmd, "Observacoes", System.Data.DbType.String, xObservacoes);
            objDB.AddInParameter(objCmd, "IdResponsavelEmpresa", System.Data.DbType.Int32, xIdResponsavelEmpresa);
            objDB.AddInParameter(objCmd, "Croqui", System.Data.DbType.String, xCroqui);
            objDB.AddInParameter(objCmd, "Indice", System.Data.DbType.String, xIndice);
            objDB.AddInParameter(objCmd, "Objetivos", System.Data.DbType.String, xObjetivos);
            objDB.AddInParameter(objCmd, "Normas", System.Data.DbType.String, xNormas);
            objDB.AddInParameter(objCmd, "Caracteristicas_Gerais", System.Data.DbType.String, xCaracteristicas_Gerais);
            objDB.AddInParameter(objCmd, "Caracteristicas_Gerais_Foto1", System.Data.DbType.String, xCaracteristicas_Gerais_Foto1);


            var readerRetorno = objDB.ExecuteReader(objCmd);

            while (readerRetorno.Read())
            {
                xStatus = Convert.ToInt32(readerRetorno["Status"]);
            }


            return xStatus;
        }



        public void Apagar_Laudo(int xIdLaudoEletrico)
        {


            using (SqlConnection cnn = new SqlConnection(Ilitera.Data.SQLServer.EntitySQLServer.GetConnection()))
            {

                string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection();

                Database objDB = new SqlDatabase(connString);

                var objCmd = objDB.GetStoredProcCommand("sp_Excluir_Laudo_Eletrico");

                objDB.AddInParameter(objCmd, "IdLaudoEletrico", System.Data.DbType.Int32, xIdLaudoEletrico);

                var readerRetorno = objDB.ExecuteReader(objCmd);

                return;

            }
        }



        public void Apagar_Laudo_SPDA(int xIdLaudoSPDA)
        {


            using (SqlConnection cnn = new SqlConnection(Ilitera.Data.SQLServer.EntitySQLServer.GetConnection()))
            {

                string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection();

                Database objDB = new SqlDatabase(connString);

                var objCmd = objDB.GetStoredProcCommand("sp_Excluir_Laudo_SPDA");

                objDB.AddInParameter(objCmd, "IdLaudoSPDA", System.Data.DbType.Int32, xIdLaudoSPDA);

                var readerRetorno = objDB.ExecuteReader(objCmd);

                return;

            }
        }



        public DataSet Trazer_Adequacoes(Int32 zIdLaudo)
        {

            StringBuilder strSQL = new StringBuilder();


            DataTable table = new DataTable("Result");
            table.Columns.Add("IdLaudoEletrico_Adequacao", Type.GetType("System.Int32"));
            table.Columns.Add("IdLaudoEletrico", Type.GetType("System.Int32"));
            table.Columns.Add("Titulo", Type.GetType("System.String"));
            table.Columns.Add("Descricao", Type.GetType("System.String"));
            table.Columns.Add("Foto", Type.GetType("System.String"));
            table.Columns.Add("Ordem", Type.GetType("System.Int16"));
            
            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);

            SqlCommand rCommand = new SqlCommand();

            rCommand.Parameters.Add("@zIdLaudo", SqlDbType.NChar).Value = zIdLaudo.ToString();

            rCommand.CommandText = "select * from LaudoEletrico_Adequacao "
            + " where IdLaudoEletrico = @zIdLaudo "
            + "order by Ordem";


            //strSQL.Append("select * from LaudoEletrico_Adequacao ");
            //strSQL.Append(" where IdLaudoEletrico = " + zIdLaudo.ToString() + " " );
            //strSQL.Append("order by Ordem");



            //m_ds = new DataSet("Result");

            using (SqlConnection cnn = new SqlConnection(Ilitera.Data.SQLServer.EntitySQLServer.GetConnection()))
            {
                cnn.Open();

                rCommand.Connection = cnn;
                SqlDataAdapter da;

                da = new SqlDataAdapter(rCommand);
                da.Fill(m_ds, "Result");

                //SqlDataAdapter da;
                //cnn.Open();
                //da = new SqlDataAdapter(strSQL.ToString(), cnn);
                //da.Fill(m_ds, "Result");

                cnn.Close();

                da.Dispose();
            }

            return m_ds;

        }



        public DataSet Trazer_Adequacoes_SPDA(Int32 zIdLaudo)
        {

            StringBuilder strSQL = new StringBuilder();


            DataTable table = new DataTable("Result");
            table.Columns.Add("IdLaudoSPDA_Adequacao", Type.GetType("System.Int32"));
            table.Columns.Add("IdLaudoSPDA", Type.GetType("System.Int32"));
            table.Columns.Add("Titulo", Type.GetType("System.String"));
            table.Columns.Add("Descricao", Type.GetType("System.String"));
            table.Columns.Add("Foto", Type.GetType("System.String"));
            table.Columns.Add("Ordem", Type.GetType("System.Int16"));

            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);

            SqlCommand rCommand = new SqlCommand();

            rCommand.Parameters.Add("@zIdLaudo", SqlDbType.NChar).Value = zIdLaudo.ToString();

            rCommand.CommandText = "select * from LaudoSPDA_Adequacao "
            + " where IdLaudoSPDA = @zIdLaudo "
            + "order by Ordem";

            //strSQL.Append("select * from LaudoSPDA_Adequacao ");
            //strSQL.Append(" where IdLaudoSPDA = " + zIdLaudo.ToString() + " ");
            //strSQL.Append("order by Ordem");



            //m_ds = new DataSet("Result");

            using (SqlConnection cnn = new SqlConnection(Ilitera.Data.SQLServer.EntitySQLServer.GetConnection()))
            {
                cnn.Open();

                rCommand.Connection = cnn;
                SqlDataAdapter da;

                da = new SqlDataAdapter(rCommand);
                da.Fill(m_ds, "Result");

                //SqlDataAdapter da;
                //cnn.Open();
                //da = new SqlDataAdapter(strSQL.ToString(), cnn);
                //da.Fill(m_ds, "Result");

                cnn.Close();

                da.Dispose();
            }

            return m_ds;

        }



        public DataSet Trazer_Dados_Adequacao(Int32 zAdequacao)
        {

            StringBuilder strSQL = new StringBuilder();


            DataTable table = new DataTable("Result");
            table.Columns.Add("IdLaudoEletrico_Adequacao", Type.GetType("System.Int32"));
            table.Columns.Add("IdLaudoEletrico", Type.GetType("System.Int32"));
            table.Columns.Add("Titulo", Type.GetType("System.String"));
            table.Columns.Add("Descricao", Type.GetType("System.String"));
            table.Columns.Add("Foto", Type.GetType("System.String"));
            table.Columns.Add("Ordem", Type.GetType("System.Int16"));
            table.Columns.Add("Item", Type.GetType("System.String"));

            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);

            SqlCommand rCommand = new SqlCommand();

            rCommand.Parameters.Add("@zAdequacao", SqlDbType.NChar).Value = zAdequacao.ToString();

            rCommand.CommandText = "select a.*, b.Item from "
            + "LaudoEletrico_Adequacao as a left join LaudoEletrico_Adequacao_Itens as b "
            + "on ( a.IdLaudoEletrico_Adequacao = b.IdLaudoEletrico_Adequacao )  "
            + "where a.IdLaudoEletrico_Adequacao = @zAdequacao "
            + "order by b.Item  ";

            //strSQL.Append("select a.*, b.Item from " );
            //strSQL.Append("LaudoEletrico_Adequacao as a left join LaudoEletrico_Adequacao_Itens as b " );
            //strSQL.Append("on ( a.IdLaudoEletrico_Adequacao = b.IdLaudoEletrico_Adequacao ) " );
            //strSQL.Append("where a.IdLaudoEletrico_Adequacao = " + zAdequacao.ToString() + " "  );          
            //strSQL.Append("order by b.Item  ");




            //m_ds = new DataSet("Result");

            using (SqlConnection cnn = new SqlConnection(Ilitera.Data.SQLServer.EntitySQLServer.GetConnection()))
            {
                cnn.Open();

                rCommand.Connection = cnn;
                SqlDataAdapter da;

                da = new SqlDataAdapter(rCommand);
                da.Fill(m_ds, "Result");

                //SqlDataAdapter da;
                //cnn.Open();
                //da = new SqlDataAdapter(strSQL.ToString(), cnn);
                //da.Fill(m_ds, "Result");

                cnn.Close();

                da.Dispose();
            }

            return m_ds;

        }



        public DataSet Trazer_Dados_Adequacao_SPDA(Int32 zAdequacao)
        {

            //StringBuilder strSQL = new StringBuilder();

            DataTable table = new DataTable("Result");
            table.Columns.Add("IdLaudoSPDA_Adequacao", Type.GetType("System.Int32"));
            table.Columns.Add("IdLaudoSPDA", Type.GetType("System.Int32"));
            table.Columns.Add("Titulo", Type.GetType("System.String"));
            table.Columns.Add("Descricao", Type.GetType("System.String"));
            table.Columns.Add("Foto", Type.GetType("System.String"));
            table.Columns.Add("Ordem", Type.GetType("System.Int16"));
            table.Columns.Add("Item", Type.GetType("System.String"));

            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);

            SqlCommand rCommand = new SqlCommand();

            rCommand.Parameters.Add("@zAdequacao", SqlDbType.NChar).Value = zAdequacao.ToString();

            rCommand.CommandText = "select a.*, b.Item from "
            + "LaudoSPDA_Adequacao as a left join LaudoSPDA_Adequacao_Itens as b "
            + "on ( a.IdLaudoSPDA_Adequacao = b.IdLaudoSPDA_Adequacao ) "
            + "where a.IdLaudoSPDA_Adequacao = @zAdequacao "
            + "order by b.Item  ";

            //strSQL.Append("select a.*, b.Item from ");
            //strSQL.Append("LaudoSPDA_Adequacao as a left join LaudoSPDA_Adequacao_Itens as b ");
            //strSQL.Append("on ( a.IdLaudoSPDA_Adequacao = b.IdLaudoSPDA_Adequacao ) ");
            //strSQL.Append("where a.IdLaudoSPDA_Adequacao = " + zAdequacao.ToString() + " ");
            //strSQL.Append("order by b.Item  ");
            

            //m_ds = new DataSet("Result");

            using (SqlConnection cnn = new SqlConnection(Ilitera.Data.SQLServer.EntitySQLServer.GetConnection()))
            {
                cnn.Open();

                rCommand.Connection = cnn;
                SqlDataAdapter da;

                da = new SqlDataAdapter(rCommand);
                da.Fill(m_ds, "Result");

                //SqlDataAdapter da;
                //cnn.Open();
                //da = new SqlDataAdapter(strSQL.ToString(), cnn);
                //da.Fill(m_ds, "Result");

                cnn.Close();

                da.Dispose();
            }

            return m_ds;

        }



        public DataSet Trazer_Adequacao(Int32 zLaudo)
        {

            StringBuilder strSQL = new StringBuilder();


            DataTable table = new DataTable("Result");
            table.Columns.Add("Titulo", Type.GetType("System.String"));
            table.Columns.Add("Descricao", Type.GetType("System.String"));
            table.Columns.Add("Foto", Type.GetType("System.String"));
            table.Columns.Add("Ordem", Type.GetType("System.Int16"));
            table.Columns.Add("Item", Type.GetType("System.String"));

            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);

            SqlCommand rCommand = new SqlCommand();

            rCommand.Parameters.Add("@zLaudo", SqlDbType.NChar).Value = zLaudo.ToString();

            rCommand.CommandText = "select distinct Titulo, Descricao, Foto, Ordem, Item  from "
            + "LaudoEletrico_Adequacao as a left join LaudoEletrico_Adequacao_Itens as b "
            + "on ( a.IdLaudoEletrico_Adequacao = b.IdLaudoEletrico_Adequacao ) "
            + "where a.IdLaudoEletrico = @zLaudo "
            + "order by a.Ordem, a.Titulo, a.Foto  ";

            //strSQL.Append("select distinct Titulo, Descricao, Foto, Ordem, Item  from ");
            //strSQL.Append("LaudoEletrico_Adequacao as a left join LaudoEletrico_Adequacao_Itens as b ");
            //strSQL.Append("on ( a.IdLaudoEletrico_Adequacao = b.IdLaudoEletrico_Adequacao ) ");
            //strSQL.Append("where a.IdLaudoEletrico = " + zLaudo.ToString() + " ");
            //strSQL.Append("order by a.Ordem, a.Titulo, a.Foto  ");


            //m_ds = new DataSet("Result");

            using (SqlConnection cnn = new SqlConnection(Ilitera.Data.SQLServer.EntitySQLServer.GetConnection()))
            {
                cnn.Open();

                rCommand.Connection = cnn;
                SqlDataAdapter da;

                da = new SqlDataAdapter(rCommand);
                da.Fill(m_ds, "Result");

                //SqlDataAdapter da;
                //cnn.Open();
                //da = new SqlDataAdapter(strSQL.ToString(), cnn);
                //da.Fill(m_ds, "Result");

                cnn.Close();

                da.Dispose();
            }

            return m_ds;

        }



        public DataSet Trazer_Adequacao_New(Int32 zLaudo, string zTensao)
        {

            //StringBuilder strSQL = new StringBuilder();
            
            DataTable table = new DataTable("Result");
            table.Columns.Add("Titulo", Type.GetType("System.String"));
            table.Columns.Add("Descricao", Type.GetType("System.String"));
            table.Columns.Add("Foto", Type.GetType("System.String"));
            table.Columns.Add("Ordem", Type.GetType("System.Int16"));
            table.Columns.Add("OrdemItem", Type.GetType("System.Int16"));
            table.Columns.Add("Item", Type.GetType("System.String"));
            table.Columns.Add("Foto2", Type.GetType("System.String"));
            table.Columns.Add("Foto3", Type.GetType("System.String"));
            table.Columns.Add("FotoTermografia", Type.GetType("System.String"));
            table.Columns.Add("Recomendacoes", Type.GetType("System.String"));
            table.Columns.Add("Situacao", Type.GetType("System.String"));
            table.Columns.Add("Irregularidade", Type.GetType("System.String"));

            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);


            SqlCommand rCommand = new SqlCommand();

            rCommand.Parameters.Add("@zLaudo", SqlDbType.NChar).Value = zLaudo.ToString();
            rCommand.Parameters.Add("@zTensao", SqlDbType.NChar).Value = zTensao;

            rCommand.CommandText = "select distinct Titulo, Descricao, Foto, a.Ordem,b.Ordem as OrdemItem, Irregularidade, Situacao, Recomendacoes, Foto2, Foto3, FotoTermografia  from "
            + "LaudoEletrico_Adequacao as a left join LaudoEletrico_Adequacao_Item as b "
            + "on ( a.IdLaudoEletrico_Adequacao = b.IdLaudoEletrico_Adequacao ) "
            + "where a.IdLaudoEletrico = @zLaudo and a.Tensao = @zTensao " 
            + "order by a.Ordem, b.Ordem, a.Titulo, a.Foto  ";

            //strSQL.Append("select distinct Titulo, Descricao, Foto, a.Ordem,b.Ordem as OrdemItem, Irregularidade, Situacao, Recomendacoes, Foto2, Foto3, FotoTermografia  from ");
            //strSQL.Append("LaudoEletrico_Adequacao as a left join LaudoEletrico_Adequacao_Item as b ");
            //strSQL.Append("on ( a.IdLaudoEletrico_Adequacao = b.IdLaudoEletrico_Adequacao ) ");
            //strSQL.Append("where a.IdLaudoEletrico = " + zLaudo.ToString() + " and a.Tensao = '" + zTensao + "' " );
            //strSQL.Append("order by a.Ordem, b.Ordem, a.Titulo, a.Foto  ");




            //m_ds = new DataSet("Result");

            using (SqlConnection cnn = new SqlConnection(Ilitera.Data.SQLServer.EntitySQLServer.GetConnection()))
            {
                cnn.Open();

                rCommand.Connection = cnn;
                SqlDataAdapter da;

                da = new SqlDataAdapter(rCommand);
                da.Fill(m_ds, "Result");

                //SqlDataAdapter da;
                //cnn.Open();
                //da = new SqlDataAdapter(strSQL.ToString(), cnn);
                //da.Fill(m_ds, "Result");

                cnn.Close();

                da.Dispose();
            }

            return m_ds;

        }


        public DataSet Trazer_Adequacao_New_SPDA(Int32 zLaudo)
        {

            StringBuilder strSQL = new StringBuilder();


            DataTable table = new DataTable("Result");
            table.Columns.Add("Titulo", Type.GetType("System.String"));
            table.Columns.Add("Descricao", Type.GetType("System.String"));
            table.Columns.Add("Foto", Type.GetType("System.String"));
            table.Columns.Add("Ordem", Type.GetType("System.Int16"));
            table.Columns.Add("OrdemItem", Type.GetType("System.Int16"));
            table.Columns.Add("Item", Type.GetType("System.String"));
            table.Columns.Add("Foto2", Type.GetType("System.String"));
            table.Columns.Add("Foto3", Type.GetType("System.String"));
            table.Columns.Add("FotoTermografia", Type.GetType("System.String"));
            table.Columns.Add("Recomendacoes", Type.GetType("System.String"));
            table.Columns.Add("Situacao", Type.GetType("System.String"));
            table.Columns.Add("Irregularidade", Type.GetType("System.String"));

            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);

            SqlCommand rCommand = new SqlCommand();

            rCommand.Parameters.Add("@zLaudo", SqlDbType.NChar).Value = zLaudo.ToString();

            rCommand.CommandText = "select distinct Titulo, Descricao, Foto, a.Ordem,b.Ordem as OrdemItem, Irregularidade, Situacao, Recomendacoes, Foto2, Foto3, FotoTermografia  from "
            + "SPDA_Adequacao2 as a left join SPDA_Adequacao_Item2 as b "
            + "on ( a.IdSPDA_Adequacao = b.IdSPDA_Adequacao ) "
            + "where a.IdSPDA = @zLaudo "
            + "order by a.Ordem, b.Ordem, a.Titulo, a.Foto  ";



            //strSQL.Append("select distinct Titulo, Descricao, Foto, a.Ordem,b.Ordem as OrdemItem, Irregularidade, Situacao, Recomendacoes, Foto2, Foto3, FotoTermografia  from ");
            //strSQL.Append("SPDA_Adequacao2 as a left join SPDA_Adequacao_Item2 as b ");
            //strSQL.Append("on ( a.IdSPDA_Adequacao = b.IdSPDA_Adequacao ) ");
            //strSQL.Append("where a.IdSPDA = " + zLaudo.ToString() + " ");
            //strSQL.Append("order by a.Ordem, b.Ordem, a.Titulo, a.Foto  ");




            //m_ds = new DataSet("Result");

            using (SqlConnection cnn = new SqlConnection(Ilitera.Data.SQLServer.EntitySQLServer.GetConnection()))
            {
                cnn.Open();

                rCommand.Connection = cnn;
                SqlDataAdapter da;

                da = new SqlDataAdapter(rCommand);
                da.Fill(m_ds, "Result");

                //SqlDataAdapter da;
                //cnn.Open();
                //da = new SqlDataAdapter(strSQL.ToString(), cnn);
                //da.Fill(m_ds, "Result");

                cnn.Close();

                da.Dispose();
            }

            return m_ds;

        }



        public DataSet Trazer_Periculosidade_New_SPDA(Int32 zLaudo)
        {

            StringBuilder strSQL = new StringBuilder();


            DataTable table = new DataTable("Result");
            table.Columns.Add("Titulo", Type.GetType("System.String"));
            table.Columns.Add("Descricao", Type.GetType("System.String"));
            table.Columns.Add("Foto", Type.GetType("System.String"));
            table.Columns.Add("Ordem", Type.GetType("System.Int16"));
            table.Columns.Add("OrdemItem", Type.GetType("System.Int16"));
            table.Columns.Add("Item", Type.GetType("System.String"));
            table.Columns.Add("Foto2", Type.GetType("System.String"));
            table.Columns.Add("Foto3", Type.GetType("System.String"));
            table.Columns.Add("FotoTermografia", Type.GetType("System.String"));
            table.Columns.Add("Recomendacoes", Type.GetType("System.String"));
            table.Columns.Add("Situacao", Type.GetType("System.String"));
            table.Columns.Add("Irregularidade", Type.GetType("System.String"));

            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);


            SqlCommand rCommand = new SqlCommand();

            rCommand.Parameters.Add("@zLaudo", SqlDbType.NChar).Value = zLaudo.ToString();

            rCommand.CommandText = "select Descricao as Titulo,Condicao_Risco as Descricao,  Foto1 as Foto, a.Ordem, 0 as OrdemItem, AmparoLegal as Irregularidade, Atividade_Perigo as Situacao, Incidencia as Recomendacoes, " 
            + "Foto2, Foto3, Foto4 as FotoTermografia, Condicao_Risco, Delimitacao, Conclusao, Observacoes, Recomendacoes as Recomendacoes2, Area_Risco, Condicao from "
            + "Periculosidade_Risco as a left join Periculosidade_Risco_Item as b "
            + "on ( a.IdPericulosidade_Risco = b.IdPericulosidade_Risco ) "
            + "where a.IdPericulosidade = @zLaudo "
            + "order by a.Ordem ";

            //strSQL.Append("select Descricao as Titulo,Condicao_Risco as Descricao,  Foto1 as Foto, a.Ordem, 0 as OrdemItem, AmparoLegal as Irregularidade, Atividade_Perigo as Situacao, Incidencia as Recomendacoes, " );
            //strSQL.Append("Foto2, Foto3, Foto4 as FotoTermografia, Condicao_Risco, Delimitacao, Conclusao, Observacoes, Recomendacoes as Recomendacoes2, Area_Risco, Condicao from ");
            //strSQL.Append("Periculosidade_Risco as a left join Periculosidade_Risco_Item as b ");
            //strSQL.Append("on ( a.IdPericulosidade_Risco = b.IdPericulosidade_Risco ) ");
            //strSQL.Append("where a.IdPericulosidade = " + zLaudo.ToString() + " ");
            //strSQL.Append("order by a.Ordem ");




            //m_ds = new DataSet("Result");

            using (SqlConnection cnn = new SqlConnection(Ilitera.Data.SQLServer.EntitySQLServer.GetConnection()))
            {
                cnn.Open();

                rCommand.Connection = cnn;
                SqlDataAdapter da;

                da = new SqlDataAdapter(rCommand);
                da.Fill(m_ds, "Result");

                //SqlDataAdapter da;
                //cnn.Open();
                //da = new SqlDataAdapter(strSQL.ToString(), cnn);
                //da.Fill(m_ds, "Result");

                cnn.Close();

                da.Dispose();
            }

            return m_ds;

        }


        public DataSet Trazer_Adequacao_SPDA(Int32 zLaudo)
        {

            StringBuilder strSQL = new StringBuilder();


            DataTable table = new DataTable("Result");
            table.Columns.Add("Titulo", Type.GetType("System.String"));
            table.Columns.Add("Descricao", Type.GetType("System.String"));
            table.Columns.Add("Foto", Type.GetType("System.String"));
            table.Columns.Add("Ordem", Type.GetType("System.Int16"));
            table.Columns.Add("Item", Type.GetType("System.String"));

            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);


            SqlCommand rCommand = new SqlCommand();

            rCommand.Parameters.Add("@zLaudo", SqlDbType.NChar).Value = zLaudo.ToString();

            rCommand.CommandText = "select distinct Titulo, Descricao, Foto, Ordem, Item  from "
            + "LaudoSPDA_Adequacao as a left join LaudoSPDA_Adequacao_Itens as b "
            + "on ( a.IdLaudoSPDA_Adequacao = b.IdLaudoSPDA_Adequacao ) "
            + "where a.IdLaudoSPDA = @zLaudo "
            + "order by a.Ordem, a.Titulo, a.Foto  ";

            //strSQL.Append("select distinct Titulo, Descricao, Foto, Ordem, Item  from ");
            //strSQL.Append("LaudoSPDA_Adequacao as a left join LaudoSPDA_Adequacao_Itens as b ");
            //strSQL.Append("on ( a.IdLaudoSPDA_Adequacao = b.IdLaudoSPDA_Adequacao ) ");
            //strSQL.Append("where a.IdLaudoSPDA = " + zLaudo.ToString() + " ");
            //strSQL.Append("order by a.Ordem, a.Titulo, a.Foto  ");




            //m_ds = new DataSet("Result");

            using (SqlConnection cnn = new SqlConnection(Ilitera.Data.SQLServer.EntitySQLServer.GetConnection()))
            {
                cnn.Open();

                rCommand.Connection = cnn;
                SqlDataAdapter da;

                da = new SqlDataAdapter(rCommand);
                da.Fill(m_ds, "Result");

                //SqlDataAdapter da;
                //cnn.Open();
                //da = new SqlDataAdapter(strSQL.ToString(), cnn);
                //da.Fill(m_ds, "Result");

                cnn.Close();

                da.Dispose();
            }

            return m_ds;

        }


        public Int32 Inserir_Adequacao(Int32 xIdLaudoEletrico, string xTitulo, string xDescricao, string xFoto, Int32 xOrdem)
        {
            Int32 xRet = 0;

            using (SqlConnection cnn = new SqlConnection(Ilitera.Data.SQLServer.EntitySQLServer.GetConnection()))
            {

                string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection();

                Database objDB = new SqlDatabase(connString);

                var objCmd = objDB.GetStoredProcCommand("sp_Inserir_Adequacao");

                objDB.AddInParameter(objCmd, "xIdLaudoEletrico", System.Data.DbType.Int32, xIdLaudoEletrico);
                objDB.AddInParameter(objCmd, "xTitulo", System.Data.DbType.String, xTitulo);
                objDB.AddInParameter(objCmd, "xDescricao", System.Data.DbType.String, xDescricao);
                objDB.AddInParameter(objCmd, "xFoto", System.Data.DbType.String, xFoto);
                objDB.AddInParameter(objCmd, "xOrdem", System.Data.DbType.Int16, xOrdem);

                var readerRetorno = objDB.ExecuteReader(objCmd);

                while (readerRetorno.Read())
                {
                    xRet = Convert.ToInt32(readerRetorno["Retorno"]);

                }


                return xRet;


            }
        }


        public Int32 Inserir_Adequacao_SPDA(Int32 xIdLaudoSPDA, string xTitulo, string xDescricao, string xFoto, Int32 xOrdem)
        {
            Int32 xRet = 0;

            using (SqlConnection cnn = new SqlConnection(Ilitera.Data.SQLServer.EntitySQLServer.GetConnection()))
            {

                string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection();

                Database objDB = new SqlDatabase(connString);

                var objCmd = objDB.GetStoredProcCommand("sp_Inserir_Adequacao_SPDA");

                objDB.AddInParameter(objCmd, "xIdLaudoSPDA", System.Data.DbType.Int32, xIdLaudoSPDA);
                objDB.AddInParameter(objCmd, "xTitulo", System.Data.DbType.String, xTitulo);
                objDB.AddInParameter(objCmd, "xDescricao", System.Data.DbType.String, xDescricao);
                objDB.AddInParameter(objCmd, "xFoto", System.Data.DbType.String, xFoto);
                objDB.AddInParameter(objCmd, "xOrdem", System.Data.DbType.Int16, xOrdem);

                var readerRetorno = objDB.ExecuteReader(objCmd);

                while (readerRetorno.Read())
                {
                    xRet = Convert.ToInt32(readerRetorno["Retorno"]);

                }


                return xRet;


            }
        }




        public void Atualizar_Adequacao(Int32 xIdLaudoEletrico_Adequacao, string xTitulo, string xDescricao, string xFoto, Int32 xOrdem)
        {
            //Int32 xRet = 0;

            using (SqlConnection cnn = new SqlConnection(Ilitera.Data.SQLServer.EntitySQLServer.GetConnection()))
            {

                string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection();

                Database objDB = new SqlDatabase(connString);

                var objCmd = objDB.GetStoredProcCommand("sp_Atualizar_Adequacao");

                objDB.AddInParameter(objCmd, "xIdLaudoEletrico_Adequacao", System.Data.DbType.Int32, xIdLaudoEletrico_Adequacao);
                objDB.AddInParameter(objCmd, "xTitulo", System.Data.DbType.String, xTitulo);
                objDB.AddInParameter(objCmd, "xDescricao", System.Data.DbType.String, xDescricao);
                objDB.AddInParameter(objCmd, "xFoto", System.Data.DbType.String, xFoto);
                objDB.AddInParameter(objCmd, "xOrdem", System.Data.DbType.Int16, xOrdem);

                var readerRetorno = objDB.ExecuteReader(objCmd);

                //while (readerRetorno.Read())
                //{
                //    xRet = Convert.ToInt32(readerRetorno["Retorno"]);

                //}


                //return xRet;
                return;

            }
        }



        public void Atualizar_Adequacao_SPDA(Int32 xIdLaudoSPDA_Adequacao, string xTitulo, string xDescricao, string xFoto, Int32 xOrdem)
        {
            //Int32 xRet = 0;

            using (SqlConnection cnn = new SqlConnection(Ilitera.Data.SQLServer.EntitySQLServer.GetConnection()))
            {

                string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection();

                Database objDB = new SqlDatabase(connString);

                var objCmd = objDB.GetStoredProcCommand("sp_Atualizar_Adequacao_SPDA");

                objDB.AddInParameter(objCmd, "xIdLaudoSPDA_Adequacao", System.Data.DbType.Int32, xIdLaudoSPDA_Adequacao);
                objDB.AddInParameter(objCmd, "xTitulo", System.Data.DbType.String, xTitulo);
                objDB.AddInParameter(objCmd, "xDescricao", System.Data.DbType.String, xDescricao);
                objDB.AddInParameter(objCmd, "xFoto", System.Data.DbType.String, xFoto);
                objDB.AddInParameter(objCmd, "xOrdem", System.Data.DbType.Int16, xOrdem);

                var readerRetorno = objDB.ExecuteReader(objCmd);

                //while (readerRetorno.Read())
                //{
                //    xRet = Convert.ToInt32(readerRetorno["Retorno"]);

                //}


                //return xRet;
                return;

            }
        }



        public void Excluir_Itens_Adequacao(Int32 xIdLaudoEletrico_Adequacao)
        {
            //Int32 xRet = 0;

            using (SqlConnection cnn = new SqlConnection(Ilitera.Data.SQLServer.EntitySQLServer.GetConnection()))
            {

                string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection();

                Database objDB = new SqlDatabase(connString);

                var objCmd = objDB.GetStoredProcCommand("sp_Excluir_Itens_Adequacao");

                objDB.AddInParameter(objCmd, "xIdLaudoEletrico_Adequacao", System.Data.DbType.Int32, xIdLaudoEletrico_Adequacao);

                var readerRetorno = objDB.ExecuteReader(objCmd);

                //while (readerRetorno.Read())
                //{
                //    xRet = Convert.ToInt32(readerRetorno["Retorno"]);

                //}


                //return xRet;
                return;

            }
        }




        public void Excluir_Itens_Adequacao_SPDA(Int32 xIdLaudoSPDA_Adequacao)
        {
            //Int32 xRet = 0;

            using (SqlConnection cnn = new SqlConnection(Ilitera.Data.SQLServer.EntitySQLServer.GetConnection()))
            {

                string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection();

                Database objDB = new SqlDatabase(connString);

                var objCmd = objDB.GetStoredProcCommand("sp_Excluir_Itens_Adequacao_SPDA");

                objDB.AddInParameter(objCmd, "xIdLaudoSPDA_Adequacao", System.Data.DbType.Int32, xIdLaudoSPDA_Adequacao);

                var readerRetorno = objDB.ExecuteReader(objCmd);

                //while (readerRetorno.Read())
                //{
                //    xRet = Convert.ToInt32(readerRetorno["Retorno"]);

                //}


                //return xRet;
                return;

            }
        }


        public void Excluir_Adequacao(Int32 xIdLaudoEletrico_Adequacao)
        {
            //Int32 xRet = 0;

            using (SqlConnection cnn = new SqlConnection(Ilitera.Data.SQLServer.EntitySQLServer.GetConnection()))
            {

                string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection();

                Database objDB = new SqlDatabase(connString);

                var objCmd = objDB.GetStoredProcCommand("sp_Excluir_Adequacao");

                objDB.AddInParameter(objCmd, "xIdLaudoEletrico_Adequacao", System.Data.DbType.Int32, xIdLaudoEletrico_Adequacao);

                var readerRetorno = objDB.ExecuteReader(objCmd);

                //while (readerRetorno.Read())
                //{
                //    xRet = Convert.ToInt32(readerRetorno["Retorno"]);

                //}


                //return xRet;
                return;

            }
        }


        public void Excluir_Adequacao_SPDA(Int32 xIdLaudoSPDA_Adequacao)
        {
            //Int32 xRet = 0;

            using (SqlConnection cnn = new SqlConnection(Ilitera.Data.SQLServer.EntitySQLServer.GetConnection()))
            {

                string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection();

                Database objDB = new SqlDatabase(connString);

                var objCmd = objDB.GetStoredProcCommand("sp_Excluir_Adequacao_SPDA");

                objDB.AddInParameter(objCmd, "xIdLaudoSPDA_Adequacao", System.Data.DbType.Int32, xIdLaudoSPDA_Adequacao);

                var readerRetorno = objDB.ExecuteReader(objCmd);

                //while (readerRetorno.Read())
                //{
                //    xRet = Convert.ToInt32(readerRetorno["Retorno"]);

                //}


                //return xRet;
                return;

            }
        }



        public void Inserir_Itens_Adequacao(Int32 xIdLaudoEletrico_Adequacao, string xItem)
        {
            //Int32 xRet = 0;

            using (SqlConnection cnn = new SqlConnection(Ilitera.Data.SQLServer.EntitySQLServer.GetConnection()))
            {

                string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection();

                Database objDB = new SqlDatabase(connString);

                var objCmd = objDB.GetStoredProcCommand("sp_Inserir_Item_Adequacao");

                objDB.AddInParameter(objCmd, "xIdLaudoEletrico_Adequacao", System.Data.DbType.Int32, xIdLaudoEletrico_Adequacao);
                objDB.AddInParameter(objCmd, "xItem", System.Data.DbType.String, xItem);

                var readerRetorno = objDB.ExecuteReader(objCmd);

                //while (readerRetorno.Read())
                //{
                //    xRet = Convert.ToInt32(readerRetorno["Retorno"]);

                //}


                //return xRet;
                return;

            }
        }



        public void Inserir_Itens_Adequacao_SPDA(Int32 xIdLaudoSPDA_Adequacao, string xItem)
        {
            //Int32 xRet = 0;

            using (SqlConnection cnn = new SqlConnection(Ilitera.Data.SQLServer.EntitySQLServer.GetConnection()))
            {

                string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection();

                Database objDB = new SqlDatabase(connString);

                var objCmd = objDB.GetStoredProcCommand("sp_Inserir_Item_Adequacao_SPDA");

                objDB.AddInParameter(objCmd, "xIdLaudoSPDA_Adequacao", System.Data.DbType.Int32, xIdLaudoSPDA_Adequacao);
                objDB.AddInParameter(objCmd, "xItem", System.Data.DbType.String, xItem);

                var readerRetorno = objDB.ExecuteReader(objCmd);

                //while (readerRetorno.Read())
                //{
                //    xRet = Convert.ToInt32(readerRetorno["Retorno"]);

                //}


                //return xRet;
                return;

            }
        }


        public void Inserir_Ponto_SPDA(int xIdLaudoSPDA, Single xValor, Int32 xOrdem)
        {


            using (SqlConnection cnn = new SqlConnection(Ilitera.Data.SQLServer.EntitySQLServer.GetConnection()))
            {

                string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection();

                Database objDB = new SqlDatabase(connString);

                var objCmd = objDB.GetStoredProcCommand("sp_Inserir_Ponto_SPDA");

                objDB.AddInParameter(objCmd, "xIdLaudoSPDA", System.Data.DbType.Int32, xIdLaudoSPDA);
                objDB.AddInParameter(objCmd, "xValor", System.Data.DbType.Single, xValor);
                objDB.AddInParameter(objCmd, "xOrdem", System.Data.DbType.Int32,xOrdem  );

                var readerRetorno = objDB.ExecuteReader(objCmd);

                return;

            }
        }


        public void Excluir_Ponto_SPDA(int xIdLaudoSPDA, Int32 xOrdem)
        {


            using (SqlConnection cnn = new SqlConnection(Ilitera.Data.SQLServer.EntitySQLServer.GetConnection()))
            {

                string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection();

                Database objDB = new SqlDatabase(connString);

                var objCmd = objDB.GetStoredProcCommand("sp_Excluir_Ponto_SPDA");

                objDB.AddInParameter(objCmd, "xIdLaudoSPDA", System.Data.DbType.Int32, xIdLaudoSPDA);                
                objDB.AddInParameter(objCmd, "xOrdem", System.Data.DbType.Int32, xOrdem);

                var readerRetorno = objDB.ExecuteReader(objCmd);

                return;

            }
        }


        public void Subir_Ponto_SPDA(int xIdLaudoSPDA, Int32 xOrdem, Int32 xOrdem_Original)
        {


            using (SqlConnection cnn = new SqlConnection(Ilitera.Data.SQLServer.EntitySQLServer.GetConnection()))
            {

                string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection();

                Database objDB = new SqlDatabase(connString);

                var objCmd = objDB.GetStoredProcCommand("sp_Subir_Ponto_SPDA");

                objDB.AddInParameter(objCmd, "xIdLaudoSPDA", System.Data.DbType.Int32, xIdLaudoSPDA);
                objDB.AddInParameter(objCmd, "xOrdem", System.Data.DbType.Int32, xOrdem);
                objDB.AddInParameter(objCmd, "xOrdem_Original", System.Data.DbType.Int32, xOrdem_Original);

                var readerRetorno = objDB.ExecuteReader(objCmd);

                return;

            }
        }

        public void Descer_Ponto_SPDA(int xIdLaudoSPDA, Int32 xOrdem, Int32 xOrdem_Original)
        {


            using (SqlConnection cnn = new SqlConnection(Ilitera.Data.SQLServer.EntitySQLServer.GetConnection()))
            {

                string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection();

                Database objDB = new SqlDatabase(connString);

                var objCmd = objDB.GetStoredProcCommand("sp_Descer_Ponto_SPDA");

                objDB.AddInParameter(objCmd, "xIdLaudoSPDA", System.Data.DbType.Int32, xIdLaudoSPDA);
                objDB.AddInParameter(objCmd, "xOrdem", System.Data.DbType.Int32, xOrdem);
                objDB.AddInParameter(objCmd, "xOrdem_Original", System.Data.DbType.Int32, xOrdem_Original);

                var readerRetorno = objDB.ExecuteReader(objCmd);

                return;

            }
        }


        public IDataReader Gerar_Dados_Pontos(int xIdLaudo)
        {

            StringBuilder strSQL = new StringBuilder();
            string xSQL;

            string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection();

            Database objDB = new SqlDatabase(connString);


            strSQL.Append("select Medida, Ordem from LaudoSPDA_Ponto ");
            strSQL.Append("where IdLaudoSPDA = ");
            strSQL.Append(xIdLaudo.ToString());
            strSQL.Append(" order by Ordem ");

            xSQL = strSQL.ToString();


            var objCmd = objDB.GetSqlStringCommand(xSQL);
            var readerRetorno = objDB.ExecuteReader(objCmd);


            return readerRetorno;

        }



    }

}