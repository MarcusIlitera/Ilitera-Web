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
    //[System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.InheritanceDemand, Flags = System.Security.Permissions.SecurityPermissionFlag.UnmanagedCode)]
    //[System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.LinkDemand, Flags = System.Security.Permissions.SecurityPermissionFlag.UnmanagedCode)]

    public class Clientes_Clinicas
    {

        //[System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.InheritanceDemand, Flags = System.Security.Permissions.SecurityPermissionFlag.UnmanagedCode)]
        //[System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.LinkDemand, Flags = System.Security.Permissions.SecurityPermissionFlag.UnmanagedCode)]

        public Boolean Salvar_tblEmpr(int xIdCliente)
        {


            using (SqlConnection cnn = new SqlConnection(Ilitera.Data.SQLServer.EntitySQLServer.GetConnection_Sied()))
            {

                string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection_Sied();

                Database objDB = new SqlDatabase(connString);

                var objCmd = objDB.GetStoredProcCommand("sp_Inserir_TBLEMPR");

                objDB.AddInParameter(objCmd, "IdCliente", System.Data.DbType.Int32, xIdCliente);

                var readerRetorno = objDB.ExecuteReader(objCmd);

                return true;
            }
        }


        //[System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.InheritanceDemand, Flags = System.Security.Permissions.SecurityPermissionFlag.UnmanagedCode)]
        //[System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.LinkDemand, Flags = System.Security.Permissions.SecurityPermissionFlag.UnmanagedCode)]






        public void Atualizar_Data_Resultado(int xIdExameBase, int xIdExameBase_Origem)
        {

            using (SqlConnection cnn = new SqlConnection(Ilitera.Data.SQLServer.EntitySQLServer.GetConnection()))
            {

                string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection();

                Database objDB = new SqlDatabase(connString);

                var objCmd = objDB.GetStoredProcCommand("sp_Atualizar_Data_Resultado");

                objDB.AddInParameter(objCmd, "xIdExameBase", System.Data.DbType.Int32, xIdExameBase);
                objDB.AddInParameter(objCmd, "xIdExameBase_Origem", System.Data.DbType.Int32, xIdExameBase_Origem);

                var readerRetorno = objDB.ExecuteReader(objCmd);

                return;
            }
        }






        public void Envio_eMail(Int32 xIdEmpregado, Int32 xIdEmpresa, string eMail, string xModulo )
        {


            using (SqlConnection cnn = new SqlConnection(Ilitera.Data.SQLServer.EntitySQLServer.GetConnection()))
            {

                string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection();

                Database objDB = new SqlDatabase(connString);

                var objCmd = objDB.GetStoredProcCommand("sp_Envio_Email");

                objDB.AddInParameter(objCmd, "xIdEmpregado", System.Data.DbType.Int32, xIdEmpregado);
                objDB.AddInParameter(objCmd, "xIdEmpresa", System.Data.DbType.Int32, xIdEmpresa);
                objDB.AddInParameter(objCmd, "xEmail", System.Data.DbType.String, eMail);
                objDB.AddInParameter(objCmd, "xModulo", System.Data.DbType.String, xModulo);

                var readerRetorno = objDB.ExecuteReader(objCmd);

                return ;
            }
        }



        public void Log_Anamnese(Int32 xIdEmpregado, Int32 xIdExameBase, Int32 xIdUsuario, string xOperacao)
        {


            using (SqlConnection cnn = new SqlConnection(Ilitera.Data.SQLServer.EntitySQLServer.GetConnection()))
            {

                string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection();

                Database objDB = new SqlDatabase(connString);

                var objCmd = objDB.GetStoredProcCommand("sp_Log_Anamnese");

                objDB.AddInParameter(objCmd, "xIdEmpregado", System.Data.DbType.Int32, xIdEmpregado);
                objDB.AddInParameter(objCmd, "xIdExameBase", System.Data.DbType.Int32, xIdExameBase);
                objDB.AddInParameter(objCmd, "xIdUsuario", System.Data.DbType.Int32, xIdUsuario);
                objDB.AddInParameter(objCmd, "xOperacao", System.Data.DbType.String, xOperacao);                

                var readerRetorno = objDB.ExecuteReader(objCmd);

                return;
            }
        }




        public DataSet Checar_Envio_Email(Int32 xIdEmpregado, Int32 xIdEmpresa, string eMail, string xModulo)
        {
            //string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection();
            //StringBuilder strSQL = new StringBuilder();

            //DataSet m_ds;
            

            DataTable table = new DataTable("Result");
            table.Columns.Add("Modulo", Type.GetType("System.String"));
            

            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);



            SqlCommand rCommand = new SqlCommand();

            rCommand.Parameters.Add("@xIdEmpregado", SqlDbType.NChar).Value = xIdEmpregado.ToString();
            rCommand.Parameters.Add("@xIdEmpresa", SqlDbType.NChar).Value = xIdEmpresa.ToString();
            rCommand.Parameters.Add("@xeMail", SqlDbType.NChar).Value = eMail;
            rCommand.Parameters.Add("@xModulo", SqlDbType.NChar).Value = xModulo;


            rCommand.CommandText = "select Modulo from Envio_eMail where nId_Empregado = @xIdEmpregado and nid_empr = @xIdEmpresa and email = @xeMail and Modulo = @xModulo "
            + " and datediff(mi, Data_Hora_Envio, getdate()) < 5 ";


            //strSQL.Append("select Modulo from Envio_eMail where nId_Empregado = " + xIdEmpregado.ToString()  +  " and nid_empr = " + xIdEmpresa.ToString() +  " and email = '" + eMail.Trim() +  "' and Modulo = '" +  xModulo.Trim() + "' ");
            //strSQL.Append(" and datediff(mi, Data_Hora_Envio, getdate()) < 5 ");
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



        public DataSet Gerar_DS_Relatorio(int xIdCliente)
        {
            //string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection();
            StringBuilder strSQL = new StringBuilder();

            //DataSet m_ds;



            DataTable table = new DataTable("Result");
            table.Columns.Add("Matriz", Type.GetType("System.String"));
            table.Columns.Add("Filial", Type.GetType("System.String"));
            table.Columns.Add("NomeAbreviado", Type.GetType("System.String"));
            table.Columns.Add("eMail", Type.GetType("System.String"));
            table.Columns.Add("Site", Type.GetType("System.String"));
            table.Columns.Add("Numero", Type.GetType("System.String"));
            table.Columns.Add("Nome", Type.GetType("System.String"));
            table.Columns.Add("Departamento", Type.GetType("System.String"));
            table.Columns.Add("Logr_1", Type.GetType("System.String"));
            table.Columns.Add("Logr_2", Type.GetType("System.String"));
            table.Columns.Add("Logr_3", Type.GetType("System.String"));
            table.Columns.Add("Telefone", Type.GetType("System.String"));


            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);


            SqlCommand rCommand = new SqlCommand();

            rCommand.Parameters.Add("@xIdCliente", SqlDbType.NChar).Value = xIdCliente.ToString();

            rCommand.CommandText = "select Filial, Matriz, h.NomeAbreviado, eMail, Site, j.Numero, Nome, Departamento, "
            + "k.NomeAbreviado + ' ' + j.Logradouro + ' n.' + j.Numero + ' ' + j.Complemento as Logr_1,  case when j.Bairro is null or j.Bairro = '' then j.Municipio + '-' + j.UF else 'Bairro : ' + j.Bairro + '  ' + j.Municipio + '-' + j.UF end as Logr_2, 'CEP:' + j.CEP + '  ' + j.CaixaPostal as Logr_3, "
            + "case when i.DDD is null or i.DDD = '' then i.Numero else '( ' + i.DDD + ' ) ' + i.Numero end as Telefone from "
            + " ( "
            + "   select * from "
            + " ( "
            + " select a.IdJuridica, a.IdJuridicaPai, b.NomeAbreviado as Matriz from  "
            + " juridica as a  left join pessoa as b "
            + " on a.idPessoa = b.idPessoa "
            + " where idjuridicapapel = 1 "
            + " and ( idjuridicapai = 0 or idjuridicapai is null ) "
            +" and isinativo = 0 "
            + " and ( DiretorioPadrao is not null and DiretorioPadrao <> '' ) "
            + " and ( a.IdJuridica = @xIdCliente "
            + "  ) "
            + " ) "
            + " as tx1 "
            + "      left join "
            + "      ( select c.IdJuridica as IdJuridica2, c.IdJuridicaPai as IdJuridicaPai2, d.NomeAbreviado as Filial from  "
            + " juridica as c  left join pessoa as d "
            + " on c.idPessoa = d.idPessoa "
            + " where idjuridicapapel <> 0  "
            + " and ( idjuridicapai is not null ) "
            + " and ( IdJuridicaPai = @xIdCliente "
            + "  ) "
            + " and isinativo = 0  "
            + " ) "
            + " as tx2 "
            + "    on tx1.IdJuridica = tx2.IdJuridicaPai2 "
            + "    )  "
            + "  as tx3 "
            + "    left join ClinicaCliente as e "
            + " on ( tx3.IdJuridica2 = e.IdCliente  ) "
            + " left join Clinica as f   on e.IdClinica = f.IdClinica "
            + " left join Juridica as g  on f.IdClinica = g.IdJuridica "
            + " left join Pessoa as h    on g.IdPessoa = h.IdPessoa "
            + " left join ContatoTelefonico as i on g.IdPessoa = i.IdPessoa "
            + " left join Endereco as j on g.IdPessoa = j.IdPessoa "
            + " left join tipoLogradouro as k on j.IdTipoLogradouro = k.IdTipoLogradouro "
            + " where h.NomeAbreviado is not null "
            + "   union "
            + "select  Filial, Matriz, h.NomeAbreviado, eMail, Site, j.Numero, Nome, Departamento, "
            + "k.NomeAbreviado + ' ' + j.Logradouro + ' n.' + j.Numero + ' ' + j.Complemento as Logr_1,  case when j.Bairro is null or j.Bairro = '' then j.Municipio + '-' + j.UF else 'Bairro : ' + j.Bairro + '  ' + j.Municipio + '-' + j.UF end as Logr_2, 'CEP:' + j.CEP + '  ' + j.CaixaPostal as Logr_3, "
            + "case when i.DDD is null or i.DDD = '' then i.Numero else '( ' + i.DDD + ' ) ' + i.Numero end as Telefone from "
            + " ( "
            + "      select * from "
            + " ( "
            + " select a.IdJuridica, a.IdJuridicaPai, b.NomeAbreviado as Matriz, 0 as IdJuridica2, 0 as IdJuridicaPai2, '' as Filial  "
            + " from  "
            + " juridica as a  left join pessoa as b "
            + " on a.idPessoa = b.idPessoa "
            + " where idjuridicapapel = 1 "
            + " and ( idjuridicapai = 0 or idjuridicapai is null ) "
            + " and isinativo = 0 "
            + " and ( DiretorioPadrao is not null and DiretorioPadrao <> '' ) "
            + " and ( a.IdJuridica = @xIdCliente "
            + " ) "
            + " ) "
            + " as tx1 "
            + "    )  "
            + " as tx4 "
            + "    left join ClinicaCliente as e "
            + " on ( tx4.IdJuridica = e.IdCliente  ) "
            + " left join Clinica as f  on e.IdClinica = f.IdClinica "
            + " left join Juridica as g  on f.IdClinica = g.IdJuridica "
            + " left join Pessoa as h  on g.IdPessoa = h.IdPessoa "
            + " left join ContatoTelefonico as i on g.IdPessoa = i.IdPessoa "
            + " left join Endereco as j on g.IdPessoa = j.IdPessoa "
            + " left join tipoLogradouro as k on j.IdTipoLogradouro = k.IdTipoLogradouro "
            + " where h.NomeAbreviado is not null "
            + " order by Matriz, Filial, NomeAbreviado  ";


            //strSQL.Append("select  Filial, Matriz, h.NomeAbreviado, eMail, Site, j.Numero, Nome, Departamento, ");
            //strSQL.Append("k.NomeAbreviado + ' ' + j.Logradouro + ' n.' + j.Numero + ' ' + j.Complemento as Logr_1,  case when j.Bairro is null or j.Bairro = '' then j.Municipio + '-' + j.UF else 'Bairro : ' + j.Bairro + '  ' + j.Municipio + '-' + j.UF end as Logr_2, 'CEP:' + j.CEP + '  ' + j.CaixaPostal as Logr_3, ");
            //strSQL.Append("case when i.DDD is null or i.DDD = '' then i.Numero else '( ' + i.DDD + ' ) ' + i.Numero end as Telefone from ");
            //strSQL.Append(" ( ");
            //strSQL.Append("   select * from ");
            //strSQL.Append(" ( ");
            //strSQL.Append(" select a.IdJuridica, a.IdJuridicaPai, b.NomeAbreviado as Matriz from  ");
            //strSQL.Append(" juridica as a  left join pessoa as b ");
            //strSQL.Append(" on a.idPessoa = b.idPessoa ");
            //strSQL.Append(" where idjuridicapapel = 1 ");
            //strSQL.Append(" and ( idjuridicapai = 0 or idjuridicapai is null ) ");
            //strSQL.Append(" and isinativo = 0 ");
            //strSQL.Append(" and ( DiretorioPadrao is not null and DiretorioPadrao <> '' ) ");
            //strSQL.Append(" and ( a.IdJuridica = ");
            //strSQL.Append(xIdCliente.ToString());
            //strSQL.Append("  ) ");
            //strSQL.Append(" ) ");
            //strSQL.Append(" as tx1 ");
            //strSQL.Append("      left join ");
            //strSQL.Append("      ( select c.IdJuridica as IdJuridica2, c.IdJuridicaPai as IdJuridicaPai2, d.NomeAbreviado as Filial from  ");
            //strSQL.Append(" juridica as c  left join pessoa as d ");
            //strSQL.Append(" on c.idPessoa = d.idPessoa ");
            //strSQL.Append(" where idjuridicapapel <> 0  ");
            //strSQL.Append(" and ( idjuridicapai is not null ) ");
            //strSQL.Append(" and ( IdJuridicaPai = ");
            //strSQL.Append(xIdCliente.ToString());
            //strSQL.Append("  ) ");
            //strSQL.Append(" and isinativo = 0  ");
            //strSQL.Append(" ) ");
            //strSQL.Append(" as tx2 ");
            //strSQL.Append("    on tx1.IdJuridica = tx2.IdJuridicaPai2 ");
            //strSQL.Append("    )  ");
            //strSQL.Append("  as tx3 ");
            //strSQL.Append("    left join ClinicaCliente as e ");
            //strSQL.Append(" on ( tx3.IdJuridica2 = e.IdCliente  ) ");
            //strSQL.Append(" left join Clinica as f   on e.IdClinica = f.IdClinica ");
            //strSQL.Append(" left join Juridica as g  on f.IdClinica = g.IdJuridica ");
            //strSQL.Append(" left join Pessoa as h    on g.IdPessoa = h.IdPessoa ");
            //strSQL.Append(" left join ContatoTelefonico as i on g.IdPessoa = i.IdPessoa ");
            //strSQL.Append(" left join Endereco as j on g.IdPessoa = j.IdPessoa ");
            //strSQL.Append(" left join tipoLogradouro as k on j.IdTipoLogradouro = k.IdTipoLogradouro ");
            //strSQL.Append(" where h.NomeAbreviado is not null ");

            //strSQL.Append("   union ");

            //strSQL.Append("select  Filial, Matriz, h.NomeAbreviado, eMail, Site, j.Numero, Nome, Departamento, ");
            //strSQL.Append("k.NomeAbreviado + ' ' + j.Logradouro + ' n.' + j.Numero + ' ' + j.Complemento as Logr_1,  case when j.Bairro is null or j.Bairro = '' then j.Municipio + '-' + j.UF else 'Bairro : ' + j.Bairro + '  ' + j.Municipio + '-' + j.UF end as Logr_2, 'CEP:' + j.CEP + '  ' + j.CaixaPostal as Logr_3, ");
            //strSQL.Append("case when i.DDD is null or i.DDD = '' then i.Numero else '( ' + i.DDD + ' ) ' + i.Numero end as Telefone from ");
            //strSQL.Append(" ( ");
            //strSQL.Append("      select * from ");
            //strSQL.Append(" ( ");
            //strSQL.Append(" select a.IdJuridica, a.IdJuridicaPai, b.NomeAbreviado as Matriz, 0 as IdJuridica2, 0 as IdJuridicaPai2, '' as Filial  ");
            //strSQL.Append(" from  ");
            //strSQL.Append(" juridica as a  left join pessoa as b ");
            //strSQL.Append(" on a.idPessoa = b.idPessoa ");
            //strSQL.Append(" where idjuridicapapel = 1 ");
            //strSQL.Append(" and ( idjuridicapai = 0 or idjuridicapai is null ) ");
            //strSQL.Append(" and isinativo = 0 ");
            //strSQL.Append(" and ( DiretorioPadrao is not null and DiretorioPadrao <> '' ) ");
            //strSQL.Append(" and ( a.IdJuridica = ");
            //strSQL.Append(xIdCliente.ToString());
            //strSQL.Append(" ) ");
            //strSQL.Append(" ) ");
            //strSQL.Append(" as tx1 ");
            //strSQL.Append("    )  ");
            //strSQL.Append(" as tx4 ");
            //strSQL.Append("    left join ClinicaCliente as e ");
            //strSQL.Append(" on ( tx4.IdJuridica = e.IdCliente  ) ");
            //strSQL.Append(" left join Clinica as f  on e.IdClinica = f.IdClinica ");
            //strSQL.Append(" left join Juridica as g  on f.IdClinica = g.IdJuridica ");
            //strSQL.Append(" left join Pessoa as h  on g.IdPessoa = h.IdPessoa ");
            //strSQL.Append(" left join ContatoTelefonico as i on g.IdPessoa = i.IdPessoa ");
            //strSQL.Append(" left join Endereco as j on g.IdPessoa = j.IdPessoa ");
            //strSQL.Append(" left join tipoLogradouro as k on j.IdTipoLogradouro = k.IdTipoLogradouro ");
            //strSQL.Append(" where h.NomeAbreviado is not null ");
            //strSQL.Append(" order by Matriz, Filial, NomeAbreviado  ");


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



        public String Retornar_Fone_Prestador(int xIdPrestador)
        {

            string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection();
            string xFone;

            xFone = "";

            //Database objDB = new SqlDatabase(ConfigurationManager.ConnectionStrings["ConexaoIliteraOpsa"].ConnectionString);
            Database objDB = new SqlDatabase(connString);

            var objCmd = objDB.GetStoredProcCommand("sp_Obter_Fone_Prestador");

            objDB.AddInParameter(objCmd, "IdPrestador", System.Data.DbType.Int32, xIdPrestador);

            var readerRetorno = objDB.ExecuteReader(objCmd);

            while (readerRetorno.Read())
            {
                xFone = Convert.ToString(readerRetorno["Fone"]);

            }


            return xFone;
        }



        public DataSet Retornar_Clinicas(string xFiltro, string xIdEmpresa)
        {
            //string connString = Mestra.Data.SQLServer.EntitySQLServer.GetConnection();
            StringBuilder strSQL = new StringBuilder();

            //DataSet m_ds;



            DataTable table = new DataTable("Result");
            table.Columns.Add("Id", Type.GetType("System.String"));
            table.Columns.Add("Nome", Type.GetType("System.String"));

            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);


            //SqlCommand rCommand = new SqlCommand();

            //rCommand.Parameters.Add("@xFiltro", SqlDbType.VarChar).Value = xFiltro;


            //if (xFiltro != "")
            //{
            //    rCommand.CommandText = "SELECT Juridica.IdJuridica AS Id, k.NomeAbreviado + '-' + j.NomeAbreviado + '-' + isnull( endereco.Bairro, '' ) + '-' +  Pessoa.NomeAbreviado COLLATE SQL_Latin1_General_Cp1251_CS_AS AS Nome FROM Pessoa INNER JOIN Juridica ON Pessoa.IdPessoa=Juridica.IdJuridica "
            //+ "left join Endereco on Pessoa.IdPessoa = Endereco.IdPessoa "
            //+ "left join Municipio on Endereco.IdMunicipio = Municipio.IdMunicipio "
            //+ "left join UnidadeFederativa on Municipio.IdUnidadeFederativa = UnidadeFederativa.IdUnidadeFederativa "
            //+ "left join LocalizacaoGeografica as j on Municipio.IdLocalizacaoGeografica = j.IdLocalizacaoGeografica "
            //+ "left join LocalizacaoGeografica as K on UnidadeFederativa.IdLocalizacaoGeografica = k.IdLocalizacaoGeografica "
            //+ "where @xFiltro "
            //+ " order by k.NomeAbreviado + '-' + j.NomeAbreviado + '-' + Pessoa.NomeAbreviado";
            //}
            //else
            //{
            //    rCommand.CommandText = "SELECT Juridica.IdJuridica AS Id, k.NomeAbreviado + '-' + j.NomeAbreviado + '-' + isnull( endereco.Bairro, '' ) + '-' +  Pessoa.NomeAbreviado COLLATE SQL_Latin1_General_Cp1251_CS_AS AS Nome FROM Pessoa INNER JOIN Juridica ON Pessoa.IdPessoa=Juridica.IdJuridica "
            //+ "left join Endereco on Pessoa.IdPessoa = Endereco.IdPessoa "
            //+ "left join Municipio on Endereco.IdMunicipio = Municipio.IdMunicipio "
            //+ "left join UnidadeFederativa on Municipio.IdUnidadeFederativa = UnidadeFederativa.IdUnidadeFederativa "
            //+ "left join LocalizacaoGeografica as j on Municipio.IdLocalizacaoGeografica = j.IdLocalizacaoGeografica "
            //+ "left join LocalizacaoGeografica as K on UnidadeFederativa.IdLocalizacaoGeografica = k.IdLocalizacaoGeografica "            
            //+ " order by k.NomeAbreviado + '-' + j.NomeAbreviado + '-' + Pessoa.NomeAbreviado";
            //}

            strSQL.Append("SELECT Juridica.IdJuridica AS Id, k.NomeAbreviado + '-' + j.NomeAbreviado + '-' + isnull( endereco.Bairro, '' ) + '-' +  Pessoa.NomeAbreviado COLLATE SQL_Latin1_General_Cp1251_CS_AS AS Nome FROM Pessoa  (nolock)  INNER JOIN Juridica (nolock)  ON Pessoa.IdPessoa=Juridica.IdJuridica ");
            strSQL.Append("left join Endereco (nolock)  on Pessoa.IdPessoa = Endereco.IdPessoa ");
            strSQL.Append("left join Municipio (nolock)  on Endereco.IdMunicipio = Municipio.IdMunicipio ");
            strSQL.Append("left join UnidadeFederativa (nolock)  on Municipio.IdUnidadeFederativa = UnidadeFederativa.IdUnidadeFederativa ");
            strSQL.Append("left join LocalizacaoGeografica (nolock)  as j on Municipio.IdLocalizacaoGeografica = j.IdLocalizacaoGeografica ");
            strSQL.Append("left join LocalizacaoGeografica (nolock)  as K on UnidadeFederativa.IdLocalizacaoGeografica = k.IdLocalizacaoGeografica ");
            strSQL.Append("left join ClinicaCliente (nolock)  as m on ( juridica.IdJuridica = m.IdClinica and m.IdCliente = " + xIdEmpresa + " ) ");

            if (xFiltro != "")
            {
                strSQL.Append("where " + xFiltro);
            }

            strSQL.Append(" order by m.ClinicaPadrao desc, k.NomeAbreviado + '-' + j.NomeAbreviado + '-' + Pessoa.NomeAbreviado");

            //m_ds = new DataSet("Result");

            using (SqlConnection cnn = new SqlConnection(Ilitera.Data.SQLServer.EntitySQLServer.GetConnection()))
            {
                //cnn.Open();

                //rCommand.Connection = cnn;
                //SqlDataAdapter da;

                //da = new SqlDataAdapter(rCommand);
                //da.Fill(m_ds, "Result");

                SqlDataAdapter da;
                cnn.Open();
                da = new SqlDataAdapter(strSQL.ToString(), cnn);
                da.Fill(m_ds, "Result");

                cnn.Close();

                da.Dispose();
            }

            return m_ds;

        }

        public DataSet Retornar_Clinicas_Valores(string xDInicial, string xDFinal)
        {
            //string connString = Mestra.Data.SQLServer.EntitySQLServer.GetConnection();
            StringBuilder strSQL = new StringBuilder();

            //DataSet m_ds;



            DataTable table = new DataTable("Result");
            table.Columns.Add("UF", Type.GetType("System.String"));
            table.Columns.Add("Cidade", Type.GetType("System.String"));
            table.Columns.Add("NomeClinica", Type.GetType("System.String"));
            table.Columns.Add("CNPJ", Type.GetType("System.String"));
            table.Columns.Add("RazaoSocial", Type.GetType("System.String"));
            table.Columns.Add("eMail", Type.GetType("System.String"));
            table.Columns.Add("Contato", Type.GetType("System.String"));
            table.Columns.Add("TipoLogradouro", Type.GetType("System.String"));
            table.Columns.Add("Logradouro", Type.GetType("System.String"));
            table.Columns.Add("Numero", Type.GetType("System.String"));
            table.Columns.Add("Bairro", Type.GetType("System.String"));
            table.Columns.Add("CEP", Type.GetType("System.String"));
            table.Columns.Add("Fone", Type.GetType("System.String"));
            table.Columns.Add("Horario", Type.GetType("System.String"));
            table.Columns.Add("Exame", Type.GetType("System.String"));
            table.Columns.Add("Valor", Type.GetType("System.String"));
            table.Columns.Add("IdExame", Type.GetType("System.String"));


            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);



            strSQL.Append(" select distinct * ");
            strSQL.Append(" from( ");
            strSQL.Append("   select distinct  h.NomeCodigo as CNPJ, ");
            strSQL.Append("   v.Nome as Exame, v.IdExameDicionario as IdExame, convert(varchar(12), convert(money, z.valorpadrao)) as Valor ");
            strSQL.Append("   from Clinica (nolock)  as f ");
            strSQL.Append("   left join Juridica (nolock)  as g  on f.IdClinica = g.IdJuridica ");
            strSQL.Append("   left join Pessoa (nolock)  as h    on g.IdPessoa = h.IdPessoa "); 
            strSQL.Append("   left join ContatoTelefonico (nolock)  as i on g.IdPessoa = i.IdPessoa ");
            strSQL.Append("   left join Endereco (nolock)  as j on g.IdPessoa = j.IdPessoa ");
            strSQL.Append("   left join tipoLogradouro (nolock)  as k on j.IdTipoLogradouro = k.IdTipoLogradouro ");
            strSQL.Append("   left join ClinicaExameDicionario (nolock)  as z on f.IdClinica = z.IdClinica ");
            strSQL.Append("   left join ExameDicionario as v on z.IdExameDicionario = v.IdExameDicionario ");
            strSQL.Append("    where j.Municipio is not null  and h.isinativo = 0  and idjuridicapapel = 8 and h.DataCadastro between convert( smalldatetime, '" + xDInicial + "',103 ) and convert( smalldatetime, '" + xDFinal + "', 103 ) ");

            strSQL.Append("    union ");


            strSQL.Append("    select   distinct h.NomeCodigo as CNPJ, ");
            strSQL.Append("    v.Nome as Exame, v.IdExameDicionario as IdExame, convert(varchar(12), convert(money, z.valorpadrao)) as Valor ");
            strSQL.Append("    from Clinica (nolock)  as f ");
            strSQL.Append("    left join Juridica (nolock)  as g  on f.IdClinica = g.IdJuridica ");
            strSQL.Append("    left join Pessoa (nolock)  as h  on g.IdPessoa = h.IdPessoa ");
            strSQL.Append("    left join ContatoTelefonico (nolock)  as i on g.IdPessoa = i.IdPessoa ");
            strSQL.Append("    left join Endereco (nolock)  as j on g.IdPessoa = j.IdPessoa ");
            strSQL.Append("    left join tipoLogradouro (nolock)  as k on j.IdTipoLogradouro = k.IdTipoLogradouro ");
            strSQL.Append("    left join ClinicaExameDicionario (nolock)  as z on f.IdClinica = z.IdClinica ");
            strSQL.Append("    left join ExameDicionario (nolock)  as v on z.IdExameDicionario = v.IdExameDicionario ");
            strSQL.Append("    where j.Municipio is not null  and h.isinativo = 0  and idjuridicapapel = 8 and h.DataCadastro between convert( smalldatetime, '" + xDInicial + "',103 ) and convert( smalldatetime, '" + xDFinal + "', 103 ) ");

            strSQL.Append("     ) as tx99 ");

            strSQL.Append("     where exame is not null and valor is not null and cnpj<>'' ");
            strSQL.Append("     order by CNPJ ");

       

          

            //m_ds = new DataSet("Result");

            using (SqlConnection cnn = new SqlConnection(Ilitera.Data.SQLServer.EntitySQLServer.GetConnection()))
            {
                //cnn.Open();

                //rCommand.Connection = cnn;
                //SqlDataAdapter da;

                //da = new SqlDataAdapter(rCommand);
                //da.Fill(m_ds, "Result");

                SqlDataAdapter da;
                cnn.Open();
                da = new SqlDataAdapter(strSQL.ToString(), cnn);
                da.Fill(m_ds, "Result");

                cnn.Close();

                da.Dispose();
            }

            return m_ds;

        }


        public DataSet Retornar_Dados_Clinicas(string xDInicial, string xDFinal, string xCNPJ)
        {
            //string connString = Mestra.Data.SQLServer.EntitySQLServer.GetConnection();
            StringBuilder strSQL = new StringBuilder();

            //DataSet m_ds;



            DataTable table = new DataTable("Result");
            table.Columns.Add("UF", Type.GetType("System.String"));
            table.Columns.Add("Cidade", Type.GetType("System.String"));
            table.Columns.Add("NomeClinica", Type.GetType("System.String"));
            table.Columns.Add("CNPJ", Type.GetType("System.String"));
            table.Columns.Add("RazaoSocial", Type.GetType("System.String"));
            table.Columns.Add("eMail", Type.GetType("System.String"));
            table.Columns.Add("Contato", Type.GetType("System.String"));
            table.Columns.Add("TipoLogradouro", Type.GetType("System.String"));
            table.Columns.Add("Logradouro", Type.GetType("System.String"));
            table.Columns.Add("Numero", Type.GetType("System.String"));
            table.Columns.Add("Bairro", Type.GetType("System.String"));
            table.Columns.Add("CEP", Type.GetType("System.String"));
            table.Columns.Add("Fone", Type.GetType("System.String"));
            table.Columns.Add("Horario", Type.GetType("System.String"));
            table.Columns.Add("Data_Cadastro", Type.GetType("System.String"));


            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);



            strSQL.Append(" select distinct uf, cidade, nomeclinica, cnpj, razaosocial, max(email) as email, max(contato) as contato, max( tipologradouro) as tipologradouro, ");
            strSQL.Append(" max(logradouro) as Logradouro, max(numero) as numero, max(bairro) as bairro, max(cep) as cep, max(fone) as fone, max(horario) as horario, max(Data_Cadastro) as Data_Cadastro ");
            strSQL.Append(" from( ");
            strSQL.Append("   select   distinct j.UF as UF, j.Municipio as Cidade, h.NomeAbreviado as NomeClinica, h.NomeCodigo as CNPJ, h.NomeCompleto as RazaoSocial, h.eMail, i.Nome as Contato, ");
            strSQL.Append("   k.NomeAbreviado as TipoLogradouro, j.Logradouro as Logradouro, j.Numero as Numero, ");
            strSQL.Append("   j.Bairro as Bairro, convert( char(10), h.DataCadastro,103) as Data_Cadastro, ");
            strSQL.Append("   isnull(j.CEP, '') as CEP, case when i.DDD is null or i.DDD = '' then i.Numero else '( ' + i.DDD + ' ) ' + i.Numero end as Fone, isnull(f.HorarioAtendimento, '' ) as Horario ");
            strSQL.Append("   from Clinica (nolock) as f ");
            strSQL.Append("   left join Juridica (nolock) as g  on f.IdClinica = g.IdJuridica ");
            strSQL.Append("   left join Pessoa (nolock)  as h    on g.IdPessoa = h.IdPessoa ");
            strSQL.Append("   left join ContatoTelefonico (nolock)  as i on g.IdPessoa = i.IdPessoa ");
            strSQL.Append("   left join Endereco (nolock)  as j on g.IdPessoa = j.IdPessoa ");
            strSQL.Append("   left join tipoLogradouro (nolock)  as k on j.IdTipoLogradouro = k.IdTipoLogradouro ");
            strSQL.Append("   where j.Municipio is not null  and h.isinativo = 0  and idjuridicapapel = 8 and h.DataCadastro between convert( smalldatetime, '" + xDInicial + "',103 ) and convert( smalldatetime, '" + xDFinal + "', 103 ) ");

            if ( xCNPJ != "")
            {
                strSQL.Append(" and f.IdClinica in ( select IdClinica from ClinicaCliente where IdCliente in ( select IdPessoa from Pessoa where dbo.udf_getnumeric( nomecodigo )  = '" + xCNPJ + "' and IsInativo = 0 ) )");
            }

            strSQL.Append("     ) as tx99 ");

            strSQL.Append("     group by uf, cidade, nomeclinica, cnpj, razaosocial ");
            strSQL.Append("     order by UF, Cidade, NomeClinica ");





            //m_ds = new DataSet("Result");

            using (SqlConnection cnn = new SqlConnection(Ilitera.Data.SQLServer.EntitySQLServer.GetConnection()))
            {
                //cnn.Open();

                //rCommand.Connection = cnn;
                //SqlDataAdapter da;

                //da = new SqlDataAdapter(rCommand);
                //da.Fill(m_ds, "Result");

                SqlDataAdapter da;
                cnn.Open();
                da = new SqlDataAdapter(strSQL.ToString(), cnn);
                da.Fill(m_ds, "Result");

                cnn.Close();

                da.Dispose();
            }

            return m_ds;

        }




        public DataSet Retornar_Vacinas(string xIdEmpregado)
        {
            //string connString = Mestra.Data.SQLServer.EntitySQLServer.GetConnection();
            StringBuilder strSQL = new StringBuilder();

            //DataSet m_ds;



            DataTable table = new DataTable("Result");
            table.Columns.Add("IdVacina", Type.GetType("System.String"));
            table.Columns.Add("Data_Vacina", Type.GetType("System.String"));
            table.Columns.Add("Dose", Type.GetType("System.String"));
            table.Columns.Add("VacinaTipo", Type.GetType("System.String"));

            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);

            SqlCommand rCommand = new SqlCommand();

            rCommand.Parameters.Add("@xIdEmpregado", SqlDbType.NChar).Value = xIdEmpregado.ToString();


            rCommand.CommandText = " select * from "
            + " ( "
            + "   select a.IdVacina, a.DataVacina as DtVacina, convert( char(10),a.DataVacina,103 ) as Data_Vacina,  b.Dose, c.VacinaTipo  "
            + "   from vacina as a  "
            + "   left join Vacina_Dose as b on ( a.IdVacinaDose = b.IdVacinaDose )  "
            + "   left join Vacina_Tipo as c on ( b.IdVacinaTipo = c.IdVacinaTipo )  "
            + "   where a.IdEmpregado = @xIdEmpregado "
            + "   union "
            + "   select 0 as IdVacina, DataExame as DtVacina, convert( char(10), DataExame, 103 ) as Data_Vacina, convert( varchar(100),Prontuario) as Dose, 'Anti-HBS' as VacinaTipo "
            + "   from examebase "
            + "   where idexamedicionario = -1174768070 "
            + "   and idempregado = @xIdEmpregado "
            + " ) "
            + " as tx10 "
            +" order by VacinaTipo, Dose, DtVacina desc  ";


            //strSQL.Append(" select * from ");
            //strSQL.Append(" ( ");
            //strSQL.Append("   select a.IdVacina, a.DataVacina as DtVacina, convert( char(10),a.DataVacina,103 ) as Data_Vacina,  b.Dose, c.VacinaTipo  ");
            //strSQL.Append("   from vacina as a  ");
            //strSQL.Append("   left join Vacina_Dose as b on ( a.IdVacinaDose = b.IdVacinaDose )  ");
            //strSQL.Append("   left join Vacina_Tipo as c on ( b.IdVacinaTipo = c.IdVacinaTipo )  ");
            //strSQL.Append("   where a.IdEmpregado = " + xIdEmpregado + " ");
            //strSQL.Append("   union ");
            //strSQL.Append("   select 0 as IdVacina, DataExame as DtVacina, convert( char(10), DataExame, 103 ) as Data_Vacina, convert( varchar(100),Prontuario) as Dose, 'Anti-HBS' as VacinaTipo ");
            //strSQL.Append("   from examebase ");
            //strSQL.Append("   where idexamedicionario = -1174768070 ");
            //strSQL.Append("   and idempregado = " + xIdEmpregado + " ");
            //strSQL.Append(" ) ");
            //strSQL.Append(" as tx10 ");
            //strSQL.Append(" order by VacinaTipo, Dose, DtVacina desc  ");

     

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



        public DataSet Trazer_Clinico_Testes_Especiais(string xIdExame)
        {
            //string connString = Mestra.Data.SQLServer.EntitySQLServer.GetConnection();
           // StringBuilder strSQL = new StringBuilder();

            //DataSet m_ds;
            

            DataTable table = new DataTable("Result");
            table.Columns.Add("IdClinicoTestesEspeciais", Type.GetType("System.Int32"));
            table.Columns.Add("Tipo", Type.GetType("System.String"));
            table.Columns.Add("Exame", Type.GetType("System.String"));
            table.Columns.Add("Resultado2", Type.GetType("System.String"));

            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);

            SqlCommand rCommand = new SqlCommand();

            rCommand.Parameters.Add("@xIdExame", SqlDbType.NChar).Value = xIdExame;
            

            rCommand.CommandText = " select a.IdClinicoTestesEspeciais, b.Descricao as Tipo, a.Exame,  "
            + " case when Resultado is null then 'Não/Negativo'  when Resultado = 'P' then 'Sim/Positivo'  when Resultado = 'N' then 'Não/Negativo' when Resultado = ' ' then 'Não/Negativo' end as Resultado2 "
            + " from Clinico_Testes_Especiais as a "
            + " join Clinico_Testes_Especiais_TipoExame as b on (a.tipoexame = b.tipoexame) "
            + " where a.IdClinico =  @xIdExame " 
            + " order by b.Descricao, a.tipoexame, ordem ";

            //strSQL.Append(" select a.IdClinicoTestesEspeciais, b.Descricao as Tipo, a.Exame,  ");
            //strSQL.Append(" case when Resultado is null then 'Não/Negativo'  when Resultado = 'P' then 'Sim/Positivo'  when Resultado = 'N' then 'Não/Negativo' when Resultado = ' ' then 'Não/Negativo' end as Resultado2 ");
            //strSQL.Append(" from Clinico_Testes_Especiais as a ");
            //strSQL.Append(" join Clinico_Testes_Especiais_TipoExame as b on (a.tipoexame = b.tipoexame) ");
            //strSQL.Append(" where a.IdClinico =  " + xIdExame + " " );
            //strSQL.Append(" order by b.Descricao, a.tipoexame, ordem ");



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



        public void Atualizar_Clinico_Testes_Especiais(Int32 xId, string xResultado)
        {


            using (SqlConnection cnn = new SqlConnection(Ilitera.Data.SQLServer.EntitySQLServer.GetConnection()))
            {

                string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection();

                Database objDB = new SqlDatabase(connString);

                var objCmd = objDB.GetStoredProcCommand("sp_Atualizar_Clinico_Testes_Especiais");

                objDB.AddInParameter(objCmd, "xId", System.Data.DbType.Int32, xId);
                objDB.AddInParameter(objCmd, "xResultado", System.Data.DbType.String, xResultado);

                var readerRetorno = objDB.ExecuteReader(objCmd);


                return;

            }
        }




        public DataSet Trazer_Analise_Laboratorial(Int32 zIdCliente)
        {
            //string connString = Mestra.Data.SQLServer.EntitySQLServer.GetConnection();
            StringBuilder strSQL = new StringBuilder();

            //DataSet m_ds;



            DataTable table = new DataTable("Result");
            table.Columns.Add("IdAnalise", Type.GetType("System.Int32"));
            table.Columns.Add("Tipo", Type.GetType("System.String"));
            table.Columns.Add("Manipulador", Type.GetType("System.String"));
            table.Columns.Add("Periodicidade", Type.GetType("System.Int16"));
            table.Columns.Add("PeriodicidadeAnalise", Type.GetType("System.Int16"));
            table.Columns.Add("ProximaAnalise", Type.GetType("System.DateTime"));
            table.Columns.Add("UltimaAnalise", Type.GetType("System.DateTime"));
            table.Columns.Add("Resultado", Type.GetType("System.String"));
            table.Columns.Add("Obs", Type.GetType("System.String"));

            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);


            SqlCommand rCommand = new SqlCommand();

            rCommand.Parameters.Add("@zIdCliente", SqlDbType.NChar).Value = zIdCliente.ToString();

            rCommand.CommandText = "select IdAnalise, b.Descricao as Tipo, c.Descricao as Manipulador, Periodicidade, PeriodicidadeAnalise, UltimaAnalise, ProximaAnalise, Resultado, Obs, convert(char(10), UltimaAnalise, 103 ) as Ultima_Analise "
            + "from tblAnalise_Laboratorial as a left join tblAnalise_Laboratorial_Tipo as b "
            + "on(a.IdTipoAnalise = b.IdTipoAnalise) "
            + "left join tblAnalise_Laboratorial_Manipulador as c "
            + "on(a.IdManipulador = c.IdManipulador) "
            + "where a.nid_Empr = @zIdCliente "
            + "order by b.Descricao, UltimaAnalise ";

            //strSQL.Append("select IdAnalise, b.Descricao as Tipo, c.Descricao as Manipulador, Periodicidade, PeriodicidadeAnalise, UltimaAnalise, ProximaAnalise, Resultado, Obs, convert(char(10), UltimaAnalise, 103 ) as Ultima_Analise ");
            //strSQL.Append("from tblAnalise_Laboratorial as a left join tblAnalise_Laboratorial_Tipo as b ");
            //strSQL.Append("on(a.IdTipoAnalise = b.IdTipoAnalise) ");
            //strSQL.Append("left join tblAnalise_Laboratorial_Manipulador as c ");
            //strSQL.Append("on(a.IdManipulador = c.IdManipulador) ");
            //strSQL.Append("where a.nid_Empr = " + zIdCliente.ToString() );
            //strSQL.Append("order by b.Descricao, UltimaAnalise ");


            //m_ds = new DataSet("Result");

            using (SqlConnection cnn = new SqlConnection(Ilitera.Data.SQLServer.EntitySQLServer.GetConnection_Sied()))
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




        public DataSet Trazer_Queixas()
        {

            StringBuilder strSQL = new StringBuilder();


            DataTable table = new DataTable("Result");
            table.Columns.Add("Queixa", Type.GetType("System.String"));
            table.Columns.Add("Abreviatura", Type.GetType("System.String"));
            table.Columns.Add("Ordem", Type.GetType("System.Int16"));

            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);


            strSQL.Append("select Queixa, Abreviatura, Ordem from tblQueixa order by Ordem ");



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

            return m_ds;            //m_ds = new DataSet("Result");


        }

        public DataSet Trazer_Medicacao()
        {

            StringBuilder strSQL = new StringBuilder();


            DataTable table = new DataTable("Result");
            table.Columns.Add("Medicacao", Type.GetType("System.String"));
            table.Columns.Add("Abreviatura", Type.GetType("System.String"));
            table.Columns.Add("Ordem", Type.GetType("System.Int16"));

            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);


            strSQL.Append("select Medicacao, Abreviatura, Ordem from tblMedicacao order by Ordem ");



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

            return m_ds;            //m_ds = new DataSet("Result");


        }




        public DataSet Trazer_Equipamentos(Int32 xIdEmpresa)
        {

            StringBuilder strSQL = new StringBuilder();


            DataTable table = new DataTable("Result");
            table.Columns.Add("IdEquipamento_Calibracao", Type.GetType("System.Int32"));
            table.Columns.Add("Dt_Aquisicao", Type.GetType("System.String"));
            table.Columns.Add("Equipamento", Type.GetType("System.String"));
            table.Columns.Add("Tipo_Equipamento", Type.GetType("System.String"));
            table.Columns.Add("Fabricante", Type.GetType("System.String"));
            table.Columns.Add("Numero_Serie", Type.GetType("System.String"));
            table.Columns.Add("Localizacao", Type.GetType("System.String"));
            table.Columns.Add("Dt_Prox_Monitoramento", Type.GetType("System.String"));
            table.Columns.Add("Dt_Manutencao", Type.GetType("System.String"));


            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);


            strSQL.Append(" select a.idEquipamento_Calibracao, convert( char(10), a.Data_Aquisicao, 103) as Dt_Aquisicao,  Equipamento, Tipo_Equipamento, Fabricante, Numero_Serie, Localizacao, ");
            strSQL.Append("        convert(char(10), Proximo_Monitoramento, 103) as Dt_Prox_Monitoramento, convert(char(10), max(b.Data_Manutencao), 103) as Dt_Manutencao ");
            strSQL.Append(" from tblEquipamento_Calibracao as a left join ");
            strSQL.Append(" tblEquipamento_Calibracao_manutencao as b on(a.IdEquipamento_Calibracao = b.IdEquipamento_Calibracao) ");
            strSQL.Append(" where nId_empr = " + xIdEmpresa.ToString() + " ");
            strSQL.Append(" group by a.Data_Aquisicao, a.equipamento, a.tipo_Equipamento, fabricante, numero_serie, Localizacao, Proximo_Monitoramento, a.IdEquipamento_Calibracao ");
            strSQL.Append(" order by a.Equipamento" );



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

            return m_ds;            //m_ds = new DataSet("Result");


        }


        public DataSet Trazer_Equipamentos_Completo(Int32 xIdEmpresa)
        {

            StringBuilder strSQL = new StringBuilder();


            DataTable table = new DataTable("Result");
            table.Columns.Add("IdEquipamento_Calibracao", Type.GetType("System.Int32"));
            table.Columns.Add("Dt_Aquisicao", Type.GetType("System.String"));
            table.Columns.Add("Equipamento", Type.GetType("System.String"));
            table.Columns.Add("Tipo_Equipamento", Type.GetType("System.String"));
            table.Columns.Add("Fabricante", Type.GetType("System.String"));
            table.Columns.Add("Numero_Serie", Type.GetType("System.String"));
            table.Columns.Add("Localizacao", Type.GetType("System.String"));
            table.Columns.Add("Dt_Prox_Monitoramento", Type.GetType("System.String"));
            table.Columns.Add("Dt_Manutencao", Type.GetType("System.String"));
            table.Columns.Add("Modelo", Type.GetType("System.String"));
            table.Columns.Add("Certificado", Type.GetType("System.String"));
            table.Columns.Add("Assistencia_Tecnica", Type.GetType("System.String"));
            table.Columns.Add("Plano_Manutencao_Preventiva", Type.GetType("System.String"));
            table.Columns.Add("TAG", Type.GetType("System.String"));
            table.Columns.Add("Faixa_Utilizacao", Type.GetType("System.String"));
            table.Columns.Add("Setor", Type.GetType("System.String"));
            table.Columns.Add("Tipo_Monitoramento", Type.GetType("System.String"));
                        

            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);


            strSQL.Append(" select a.idEquipamento_Calibracao, convert( char(10), a.Data_Aquisicao, 103) as Dt_Aquisicao,  Equipamento, Tipo_Equipamento, Fabricante, Numero_Serie, Localizacao, ");
            strSQL.Append("        convert(char(10), Proximo_Monitoramento, 103) as Dt_Prox_Monitoramento, convert(char(10), max(b.Data_Manutencao), 103) as Dt_Manutencao, ");
            strSQL.Append("        Modelo, Certificado, Assistencia_Tecnica, Plano_Manutencao_Preventiva, TAG, Faixa_Utilizacao, Setor, Tipo_Monitoramento ");
            strSQL.Append(" from tblEquipamento_Calibracao as a left join ");
            strSQL.Append(" tblEquipamento_Calibracao_manutencao as b on(a.IdEquipamento_Calibracao = b.IdEquipamento_Calibracao) ");
            strSQL.Append(" where nId_empr = " + xIdEmpresa.ToString() + " ");
            strSQL.Append(" group by a.Data_Aquisicao, a.equipamento, a.tipo_Equipamento, fabricante, numero_serie, Localizacao, Proximo_Monitoramento, a.IdEquipamento_Calibracao, ");
            strSQL.Append(" Modelo, Certificado, Assistencia_Tecnica, Plano_Manutencao_Preventiva, TAG, Faixa_Utilizacao, Setor, Tipo_Monitoramento ");
            strSQL.Append(" order by a.Equipamento");



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

            return m_ds;            //m_ds = new DataSet("Result");


        }



        public DataSet Retornar_Exames_Cliente_Clinica(string xClinica, string xIdEmpresa)
        {
            //string connString = Mestra.Data.SQLServer.EntitySQLServer.GetConnection();
            StringBuilder strSQL = new StringBuilder();

            //DataSet m_ds;



            DataTable table = new DataTable("Result");
            table.Columns.Add("Id", Type.GetType("System.Int32"));
            table.Columns.Add("Nome", Type.GetType("System.String"));

            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);




            strSQL.Append(" SELECT ExameDicionario.IdExameDicionario AS Id, Nome AS Nome  ");
            strSQL.Append(" FROM ExameDicionario WHERE  IdExameDicionario IN(SELECT IdExameDicionario FROM ClinicaExameDicionario  ");
            strSQL.Append(" WHERE IdClinica IN(SELECT IdClinica FROM ClinicaCliente WHERE IdCliente = " + xIdEmpresa + " and IdClinica = " + xClinica + ")) ");
            strSQL.Append(" ORDER BY len(Nome) desc ");


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



        public Int32 Retorna_Exames_PCMSO_GHE(Int32 xIdPCMSO)
        {

            StringBuilder strSQL = new StringBuilder();


            DataTable table = new DataTable("Result");
            table.Columns.Add("IdClinica", Type.GetType("System.Int32"));

            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);


            strSQL.Append(" select top 1 COUNT(*) as Tot from PcmsoPlanejamento where IdPcmso = " + xIdPCMSO.ToString() + "  group by IdGHE order by COUNT(*) desc ");

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

            if (m_ds.Tables[0].Rows.Count == 0)
                return 0;
            else
                return (Int32)m_ds.Tables[0].Rows[0]["Tot"];

        }


    }

}