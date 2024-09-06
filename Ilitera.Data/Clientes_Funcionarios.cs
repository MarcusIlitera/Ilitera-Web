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
    public class Clientes_Funcionarios
    {


        public DataSet Gerar_DS_Relatorio(int xIdCliente)
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


            strSQL.Append("select a.tNO_STR_EMPR as Setor, count(*) as Funcionarios, d.NomeAbreviado as Cliente, ");
            strSQL.Append("case ");
            strSQL.Append("when c.valor is null then 0 ");
            strSQL.Append("when c.valor is not null then count(*) * c.valor ");
            strSQL.Append("end ");
            strSQL.Append("as Valor_Setor ");
            strSQL.Append("from tblSetor as a join tblEmpregado_Funcao as b ");
            strSQL.Append("on ( a.nID_Setor = b.nID_Setor ) ");
            strSQL.Append("left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.contratoservico as c ");
            strSQL.Append("on ( b.nId_Empr = c.IdCliente  ");
            strSQL.Append("      and c.idservico in ");
            strSQL.Append("        ( select top 1 idservico FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.servico where descricao = 'Cobrança por Empregado' ) ");
            strSQL.Append(" ) ");
            strSQL.Append("left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.pessoa as d ");
            strSQL.Append("on ( b.nID_Empr = d.IdPessoa ) ");
            strSQL.Append("where b.nId_Empr = ");
            strSQL.Append(xIdCliente.ToString());
            strSQL.Append(" ");
            strSQL.Append("group by a.tNO_STR_EMPR, c.Valor, d.NomeAbreviado ");
            strSQL.Append("order by count(*) desc ");



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

            return m_ds;

        }




        public Int32 Retornar_CodBusca(Int32 zMin, Int32 zMax)
        {
            //string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection();
            StringBuilder strSQL = new StringBuilder();

            //DataSet m_ds;


            DataTable table = new DataTable("Result");
            table.Columns.Add("Codigo", Type.GetType("System.Int32"));


            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);

            strSQL.Append(" select MAX(codigo) as Codigo ");
            strSQL.Append(" from ");
            strSQL.Append(" ( ");

            strSQL.Append(" select isnull( max( isnull(codbusca," + zMin.ToString() + " ) ), " + zMin.ToString() + " )  as Codigo from examebase where CodBusca between " + zMin.ToString() + " and " + zMax.ToString() + " ");

            strSQL.Append(" union ");

            strSQL.Append(" select isnull( max( isnull(codbusca," + zMin.ToString() + " ) ), " + zMin.ToString() + " )  as Codigo from examebase_Faltou where CodBusca between " + zMin.ToString() + " and " + zMax.ToString() + " ");

            strSQL.Append("  ) as tx80 ");
                        

            using (SqlConnection cnn = new SqlConnection(Ilitera.Data.SQLServer.EntitySQLServer.GetConnection()))
            {
                SqlDataAdapter da;
                cnn.Open();


                da = new SqlDataAdapter(strSQL.ToString(), cnn);
                da.Fill(m_ds, "Result");

                cnn.Close();

                da.Dispose();
            }

            if (m_ds.Tables[0].Rows.Count > 0)
            {
                return System.Convert.ToInt32(m_ds.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return 0;
            }


        }



        public Int32 Retornar_CodBusca_Toxicologico(Int32 zMin, Int32 zMax)
        {
            //string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection();
            StringBuilder strSQL = new StringBuilder();

            //DataSet m_ds;


            DataTable table = new DataTable("Result");
            table.Columns.Add("Codigo", Type.GetType("System.Int32"));


            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);


            strSQL.Append(" select MAX(codigo) as Codigo ");
            strSQL.Append(" from ");
            strSQL.Append(" ( ");

            strSQL.Append(" select isnull( max( isnull(codbusca," + zMin.ToString() + " ) ), " + zMin.ToString() + " )  as Codigo from examebase where IdExameDicionario=100 and CodBusca between " + zMin.ToString() + " and " + zMax.ToString() + " ");

            strSQL.Append(" union ");

            strSQL.Append(" select isnull( max( isnull(codbusca," + zMin.ToString() + " ) ), " + zMin.ToString() + " )  as Codigo from examebase_Faltou where IdExameDicionario=100 and CodBusca between " + zMin.ToString() + " and " + zMax.ToString() + " ");

            strSQL.Append("  ) as tx80 ");

            using (SqlConnection cnn = new SqlConnection(Ilitera.Data.SQLServer.EntitySQLServer.GetConnection()))
            {
                SqlDataAdapter da;
                cnn.Open();


                da = new SqlDataAdapter(strSQL.ToString(), cnn);
                da.Fill(m_ds, "Result");

                cnn.Close();

                da.Dispose();
            }

            if (m_ds.Tables[0].Rows.Count > 0)
            {
                return System.Convert.ToInt32(m_ds.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return 0;
            }


        }





        public DataSet Checar_Absenteismo_Colaborador(int xIdColaborador, string xDataInicio)
        {
            //string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection();
            //StringBuilder strSQL = new StringBuilder();

            //DataSet m_ds;


            DataTable table = new DataTable("Result");
            table.Columns.Add("Dias", Type.GetType("System.Int16"));


            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);

            SqlCommand rCommand = new SqlCommand();

            rCommand.Parameters.Add("@xIdColaborador", SqlDbType.NChar).Value = xIdColaborador.ToString();
            rCommand.Parameters.Add("@xDataInicio", SqlDbType.NChar).Value = xDataInicio;


            rCommand.CommandText = " select sum(Dias) as Dias from "
            + " ( "
            + "   select datediff(dd, dataInicial, case when datavolta is null then dataprevista else datavolta end) as Dias "
            + "   from afastamento "
            + "   where idempregado = @xIdColaborador "
            + "  and datainicial >= dateadd(dd, -60, convert( smalldatetime,@xDataInicio,103 ))  "
            + "   ) as tx1 ";

            //strSQL.Append(" select sum(Dias) as Dias from ");
            //strSQL.Append(" ( ");
            //strSQL.Append("   select datediff(dd, dataInicial, case when datavolta is null then dataprevista else datavolta end) as Dias ");
            //strSQL.Append("   from afastamento ");
            //strSQL.Append("   where idempregado = " + xIdColaborador.ToString() + " ");
            //strSQL.Append("  and datainicial >= dateadd(dd, -60, convert( smalldatetime,'" + xDataInicio + "',103 ))  ");  
            //strSQL.Append("   ) as tx1 ");


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



        public DataSet Gerar_DS_Relatorio_Exames(int xIdCliente, string xD_Inicial, string xD_Final, string xCliente)
        {
            //string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection();
            StringBuilder strSQL = new StringBuilder();

            //DataSet m_ds;


            DataTable table = new DataTable("Result");
            table.Columns.Add("Exame", Type.GetType("System.String"));
            table.Columns.Add("Qtde", Type.GetType("System.Int32"));
            table.Columns.Add("svMin", Type.GetType("System.Single"));
            table.Columns.Add("svMax", Type.GetType("System.Single"));
            table.Columns.Add("svReal", Type.GetType("System.Single"));
            table.Columns.Add("Empresa", Type.GetType("System.String"));
            table.Columns.Add("Data1", Type.GetType("System.String"));
            table.Columns.Add("Data2", Type.GetType("System.String"));


            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);



            strSQL.Append("select Nome as exame, count(*) as Qtde, sum( vMin ) as svMin, sum( vMax ) as svMax, sum( vReal ) as svReal, ");
            strSQL.Append("'" + xCliente + "' as Empresa, '" + xD_Inicial.Substring(0, 10) + "' as Data1, '" + xD_Final.Substring(0, 10) + "' as Data2 ");
            strSQL.Append("from ");
            strSQL.Append("( ");
            strSQL.Append("    select IdEmpregado, Nome, DataExame, min( Valor_Minimo ) as Vmin, Max(  ");
            strSQL.Append("    Valor_Maximo ) as vMax, max( Valor_Real ) as vReal ");
            strSQL.Append("    from ");
            strSQL.Append("    ( ");

            strSQL.Append("		select  l.IdEmpregado, k.Nome, l.DataExame, null as Valor_Minimo, null as Valor_Maximo, max( a.ValorPadrao) as Valor_Real ");
            strSQL.Append("    from examebase as l  ");
            strSQL.Append("        left join examedicionario as k on l.idexamedicionario =  ");
            strSQL.Append("    k.idexamedicionario ");
            strSQL.Append("        left join ClinicaExameDicionario as a on k.IdExameDicionario =  ");
            strSQL.Append("    a.IdExameDicionario    	     ");
            strSQL.Append("        left join ClinicaClienteExameDicionario as c on  ");
            strSQL.Append("a.IdClinicaExameDicionario = 	c.IdClinicaExameDicionario 		 ");
            strSQL.Append("left join ClinicaCliente as d on l.idjuridica = d.idclinica  ");
            strSQL.Append("where idempregado in ");
            strSQL.Append("( ");
            
            strSQL.Append("SELECT nID_EMPREGADO  ");
            strSQL.Append("FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEMPREGADO_FUNCAO  ");
            strSQL.Append("WHERE nID_EMPR= " + xIdCliente.ToString() + " ) ");

            //strSQL.Append("and d.IdCliente = " + xIdCliente.ToString() + " ");
            strSQL.Append("and d.ClinicaPadrao is not null	 ");
            strSQL.Append("and l.DataExame between convert( smalldatetime, '" + xD_Inicial.Substring(0, 10) + "', 103 ) and 	convert( smalldatetime, '" + xD_Final.Substring(0, 10) + " 23:59', 103 )  ");
            strSQL.Append("group by l.IdEmpregado, k.Nome, l.DataExame ");

            strSQL.Append("		union ");

            strSQL.Append("select  l.IdEmpregado, k.Nome, l.DataExame, min(a.ValorPadrao) as  ");
            strSQL.Append("Valor_Minimo, max( a.ValorPadrao ) as Valor_Maximo, null as Valor_Real ");
            strSQL.Append("from examebase as l  ");
            strSQL.Append("left join examedicionario as k on l.idexamedicionario =  ");
            strSQL.Append("k.idexamedicionario ");
            strSQL.Append("left join ClinicaExameDicionario as a on k.IdExameDicionario =  ");
            strSQL.Append("a.IdExameDicionario    	     ");
            strSQL.Append("left join ClinicaClienteExameDicionario as c on  ");
            strSQL.Append("a.IdClinicaExameDicionario = 	c.IdClinicaExameDicionario 		 ");
            strSQL.Append("left join ClinicaCliente as d on d.IdClinica =  a.IdClinica  ");
            strSQL.Append("where idempregado in ");
            strSQL.Append("( ");

            strSQL.Append("SELECT nID_EMPREGADO  ");
            strSQL.Append("FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEMPREGADO_FUNCAO  ");
            strSQL.Append("WHERE nID_EMPR= " + xIdCliente.ToString() + "  ");

            strSQL.Append(") ");

            strSQL.Append("and a.ValorPadrao > 0 ");
            strSQL.Append("and d.IdCliente = " + xIdCliente.ToString() + "  ");
            strSQL.Append("and d.ClinicaPadrao is not null		 ");
            strSQL.Append("and l.DataExame between convert( smalldatetime, '" + xD_Inicial.Substring(0, 10) + "', 103 ) and 	convert( smalldatetime, '" + xD_Final.Substring(0, 10) + " 23:59', 103 )  ");
            strSQL.Append("group by l.IdEmpregado, k.Nome, l.DataExame ");


            strSQL.Append("		union ");

            strSQL.Append("select  l.IdEmpregado, k.Nome, l.DataExame, 0 as Valor_Minimo, 0 as Valor_Maximo, 0 as Valor_Real ");
            strSQL.Append("from examebase as l  ");
            strSQL.Append("left join examedicionario as k on l.idexamedicionario =  ");
            strSQL.Append("k.idexamedicionario ");
            strSQL.Append("left join ClinicaExameDicionario as a on k.IdExameDicionario =  ");
            strSQL.Append("a.IdExameDicionario    	     ");
            strSQL.Append("left join ClinicaClienteExameDicionario as c on  ");
            strSQL.Append("a.IdClinicaExameDicionario = 	c.IdClinicaExameDicionario 		 ");
            strSQL.Append("left join ClinicaCliente as d on c.IdClinicaCliente =  ");
            strSQL.Append("d.IdClinicaCliente  ");
            strSQL.Append("where idempregado in ");
            strSQL.Append("( ");

            strSQL.Append("SELECT nID_EMPREGADO  ");
            strSQL.Append("FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEMPREGADO_FUNCAO  ");
            strSQL.Append("WHERE nID_EMPR= " + xIdCliente.ToString() + "  ");

            strSQL.Append(") ");

            strSQL.Append("and k.idexamedicionario not in ");
            strSQL.Append(" (  SELECT IdExameDicionario ");
            strSQL.Append("    FROM ClinicaExameDicionario ");
            strSQL.Append("    WHERE IdClinica in ");
            strSQL.Append("     (  select IdClinica from ClinicaCliente where idcliente = " + xIdCliente.ToString() + "  ) ");
            strSQL.Append(" ) ");

            strSQL.Append("group by l.IdEmpregado, k.Nome, l.DataExame ");


            strSQL.Append("	) ");
            strSQL.Append("as tx1 ");
            strSQL.Append("group by IdEmpregado, Nome, DataExame ");
            strSQL.Append(") ");
            strSQL.Append("as tx2 ");
            strSQL.Append("group by Nome ");
            strSQL.Append("order by Nome ");



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






        public DataSet Gerar_DS_Relatorio_Empregados_Empr(int xIdCliente, string xD_Inicial, string xCliente)
        {
            //string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection();
            //StringBuilder strSQL = new StringBuilder();

            //DataSet m_ds;


            DataTable table = new DataTable("Result");
            table.Columns.Add("Colaborador", Type.GetType("System.String"));
            table.Columns.Add("GHE", Type.GetType("System.String"));
            table.Columns.Add("Setor", Type.GetType("System.String"));
            table.Columns.Add("Funcao", Type.GetType("System.String"));
            table.Columns.Add("tSexo", Type.GetType("System.String"));
            table.Columns.Add("Admissao", Type.GetType("System.String"));
            table.Columns.Add("Nascimento", Type.GetType("System.String"));
            table.Columns.Add("Idade", Type.GetType("System.String"));


            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);


            SqlCommand rCommand = new SqlCommand();

            rCommand.Parameters.Add("@xIdCliente", SqlDbType.NChar).Value = xIdCliente.ToString();



            rCommand.CommandText = " select tno_empg as Colaborador,   "
            + " case   "
            + "    when max( tno_Func ) is NULL then 'SEM GHE' "
            + "    else  max( tno_Func ) "
            + "    end "
            + " as GHE, tNO_STR_EMPR as  Setor, tNO_FUNC_Empr as Funcao, tsexo, Admissao, Nascimento, Idade    "
            + " from "
            + " ( "
            + "    select distinct a.tno_empg, tno_func, e.tno_str_empr, f.tno_func_empr, tsexo, convert( char(10), hdt_adm, 103 ) as Admissao, convert( char(10),hDT_NASC,103) as Nascimento, datediff(yyyy, hdt_nasc, getdate()) as Idade "
            + "    from tblempregado as a left join tblempregado_funcao as b "
            + "    on ( a.nid_empregado = b.nid_empregado ) "
            + "    left join tblfunc_empregado as c "
            + "    on ( b.nid_empregado_funcao = c.nid_empregado_funcao and c.nid_laud_tec  in ( select top 1 nID_LAUD_TEC from tblLAUDO_TEC where nid_empr = @xIdCliente order by hDT_LAUDO desc ) ) "
            + "    left join tblfunc as d "
            + "    on ( c.nid_func = d.nid_func and d.nid_laud_tec  in ( select top 1 nID_LAUD_TEC from tblLAUDO_TEC where nid_empr =  @xIdCliente  order by hDT_LAUDO desc ) ) "
            + "    left join tblSetor as e "
            + "    on ( b.nid_setor = e.nid_setor ) "
            + "    left join tblfuncao as f "
            + "    on ( b.nid_funcao = f.nid_funcao )    "
            + "    where ( a.nid_empr =  @xIdCliente  "
            + "        and b.nid_empr =  @xIdCliente  "
            + "        and a.hdt_dem is null   "
            + "     ) "
            + " ) "
            + " as tx1 "
            + " group by tno_empg, tNO_STR_EMPR, tNO_FUNC_Empr, tsexo, Admissao, Nascimento, Idade   "
            + " union "
            + " select tno_empg as Colaborador,   "
            + " case   "
            + "    when min( tno_Func ) is NULL then 'SEM GHE' "
            + "    else min( tno_Func ) "
            + "    end "
            + " as GHE, tNO_STR_EMPR as  Setor, tNO_FUNC_Empr as Funcao , tsexo, Admissao, Nascimento, Idade  "
            + " from "
            + " ( "
            + "    select distinct a.tno_empg, tno_func, e.tno_str_empr, f.tno_func_empr, tsexo, convert( char(10), hdt_adm, 103 ) as Admissao, convert( char(10),hDT_NASC,103) as Nascimento, datediff(yyyy, hdt_nasc, getdate()) as Idade "
            + "    from tblempregado as a left join tblempregado_funcao as b "
            + "    on ( a.nid_empregado = b.nid_empregado ) "
            + "    left join tblfunc_empregado as c "
            + "    on ( b.nid_empregado_funcao = c.nid_empregado_funcao and c.nid_laud_tec  in ( select top 1 nID_LAUD_TEC from tblLAUDO_TEC where nid_empr = @xIdCliente  order by hDT_LAUDO desc ) ) "
            + "    left join tblfunc as d "
            + "    on ( c.nid_func = d.nid_func and d.nid_laud_tec  in ( select top 1 nID_LAUD_TEC from tblLAUDO_TEC where nid_empr =  @xIdCliente   order by hDT_LAUDO desc ) ) "
            + "    left join tblSetor as e "
            + "    on ( b.nid_setor = e.nid_setor ) "
            + "    left join tblfuncao as f "
            + "    on ( b.nid_funcao = f.nid_funcao ) "
            + "    where ( a.nid_empr =  @xIdCliente "
            + "        and b.nid_empr =  @xIdCliente  "
            + "        and a.hdt_dem is null   "
            + "     ) "
            + " ) "
            + " as tx1 "
            + " group by tno_empg, tNO_STR_EMPR , tNO_FUNC_Empr, tsexo, Admissao, Nascimento, Idade  ";



            //strSQL.Append(" select tno_empg as Colaborador,   ");
            //strSQL.Append(" case   ");
            //strSQL.Append("    when max( tno_Func ) is NULL then 'SEM GHE' ");
            //strSQL.Append("    else  max( tno_Func ) ");
            //strSQL.Append("    end ");
            //strSQL.Append(" as GHE, tNO_STR_EMPR as  Setor, tNO_FUNC_Empr as Funcao, tsexo, Admissao, Nascimento, Idade    ");
            //strSQL.Append(" from ");
            //strSQL.Append(" ( ");
            //strSQL.Append("    select distinct a.tno_empg, tno_func, e.tno_str_empr, f.tno_func_empr, tsexo, convert( char(10), hdt_adm, 103 ) as Admissao, convert( char(10),hDT_NASC,103) as Nascimento, datediff(yyyy, hdt_nasc, getdate()) as Idade ");
            //strSQL.Append("    from tblempregado as a left join tblempregado_funcao as b ");
            //strSQL.Append("    on ( a.nid_empregado = b.nid_empregado ) ");
            //strSQL.Append("    left join tblfunc_empregado as c ");
            //strSQL.Append("    on ( b.nid_empregado_funcao = c.nid_empregado_funcao and c.nid_laud_tec  in ( select top 1 nID_LAUD_TEC from tblLAUDO_TEC where nid_empr = " +  xIdCliente + " order by hDT_LAUDO desc ) ) ");
            //strSQL.Append("    left join tblfunc as d ");
            //strSQL.Append("    on ( c.nid_func = d.nid_func and d.nid_laud_tec  in ( select top 1 nID_LAUD_TEC from tblLAUDO_TEC where nid_empr =  " + xIdCliente + "  order by hDT_LAUDO desc ) ) ");
            //strSQL.Append("    left join tblSetor as e ");
            //strSQL.Append("    on ( b.nid_setor = e.nid_setor ) ");
            //strSQL.Append("    left join tblfuncao as f ");
            //strSQL.Append("    on ( b.nid_funcao = f.nid_funcao )    ");
            //strSQL.Append("    where ( a.nid_empr =  " + xIdCliente + "  ");
            //strSQL.Append("        and b.nid_empr =  " + xIdCliente + "  ");
            //strSQL.Append("        and a.hdt_dem is null   ");
            //strSQL.Append("     ) ");
            //strSQL.Append(" ) ");
            //strSQL.Append(" as tx1 ");
            //strSQL.Append(" group by tno_empg, tNO_STR_EMPR, tNO_FUNC_Empr, tsexo, Admissao, Nascimento, Idade   ");
            //strSQL.Append(" union ");
            //strSQL.Append(" select tno_empg as Colaborador,   ");
            //strSQL.Append(" case   ");
            //strSQL.Append("    when min( tno_Func ) is NULL then 'SEM GHE' ");
            //strSQL.Append("    else min( tno_Func ) ");
            //strSQL.Append("    end ");
            //strSQL.Append(" as GHE, tNO_STR_EMPR as  Setor, tNO_FUNC_Empr as Funcao , tsexo, Admissao, Nascimento, Idade  ");
            //strSQL.Append(" from ");
            //strSQL.Append(" ( ");
            //strSQL.Append("    select distinct a.tno_empg, tno_func, e.tno_str_empr, f.tno_func_empr, tsexo, convert( char(10), hdt_adm, 103 ) as Admissao, convert( char(10),hDT_NASC,103) as Nascimento, datediff(yyyy, hdt_nasc, getdate()) as Idade ");
            //strSQL.Append("    from tblempregado as a left join tblempregado_funcao as b ");
            //strSQL.Append("    on ( a.nid_empregado = b.nid_empregado ) ");
            //strSQL.Append("    left join tblfunc_empregado as c ");
            //strSQL.Append("    on ( b.nid_empregado_funcao = c.nid_empregado_funcao and c.nid_laud_tec  in ( select top 1 nID_LAUD_TEC from tblLAUDO_TEC where nid_empr =  " + xIdCliente + "  order by hDT_LAUDO desc ) ) ");
            //strSQL.Append("    left join tblfunc as d ");
            //strSQL.Append("    on ( c.nid_func = d.nid_func and d.nid_laud_tec  in ( select top 1 nID_LAUD_TEC from tblLAUDO_TEC where nid_empr =  " + xIdCliente + "  order by hDT_LAUDO desc ) ) ");
            //strSQL.Append("    left join tblSetor as e ");
            //strSQL.Append("    on ( b.nid_setor = e.nid_setor ) ");
            //strSQL.Append("    left join tblfuncao as f ");
            //strSQL.Append("    on ( b.nid_funcao = f.nid_funcao ) ");
            //strSQL.Append("    where ( a.nid_empr =  " + xIdCliente + "  ");
            //strSQL.Append("        and b.nid_empr =  " + xIdCliente + "  ");
            //strSQL.Append("        and a.hdt_dem is null   ");
            //strSQL.Append("     ) ");
            //strSQL.Append(" ) ");
            //strSQL.Append(" as tx1 "); 
            //strSQL.Append(" group by tno_empg, tNO_STR_EMPR , tNO_FUNC_Empr, tsexo, Admissao, Nascimento, Idade  ");




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





        public DataSet Gerar_DS_Relatorio_Colaboradores(int xIdCliente, string xD_Inicial, string xCliente, string zEmp, string xFiltro, string xSit, string xTipo, string xDefic, string xAtivosEm, string xSemEmail, string xInconsistencia, string xClassif)
        {
            //string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection();
            StringBuilder strSQL = new StringBuilder();

            //DataSet m_ds;


            DataTable table = new DataTable("Result");
            table.Columns.Add("Colaborador", Type.GetType("System.String"));
            table.Columns.Add("GHE", Type.GetType("System.String"));
            table.Columns.Add("Setor", Type.GetType("System.String"));
            table.Columns.Add("Funcao", Type.GetType("System.String"));
            table.Columns.Add("tSexo", Type.GetType("System.String"));
            table.Columns.Add("Admissao", Type.GetType("System.String"));
            table.Columns.Add("Nascimento", Type.GetType("System.String"));
            table.Columns.Add("Idade", Type.GetType("System.String"));
            table.Columns.Add("DataInicial", Type.GetType("System.String"));
            table.Columns.Add("DataVolta", Type.GetType("System.String"));
            table.Columns.Add("DataPrevista", Type.GetType("System.String"));
            table.Columns.Add("Deficiencia", Type.GetType("System.String"));
            table.Columns.Add("CPF", Type.GetType("System.String"));
            table.Columns.Add("teMail", Type.GetType("System.String"));
            table.Columns.Add("Filtro", Type.GetType("System.String"));
            table.Columns.Add("RG", Type.GetType("System.String"));

            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);



            strSQL.Append(" select Colaborador, CPF, GHE, Setor, Funcao, tsexo, Admissao, Nascimento, idade, case when DataInicial is not null then 'Afastado desde ' + convert( char(10), DataInicial, 103 )      when hdt_dem is not null then 'Inativo'   when hdt_dem is null then 'Ativo'   end as DataInicial, RG, ");
            strSQL.Append(" case when nInd_Beneficiario = 1 then 'Beneficiário Reabilitado' when nInd_Beneficiario=2 then 'Portador de Deficiência Habilitada'   when nInd_Beneficiario=0 then 'Não aplicável' end as Deficiencia, ");
            strSQL.Append(" NomeAbreviado + '   CNPJ: ' + NomeCodigo as DataVolta, convert( char(10), getdate(), 103 ) as DataPrevista, temail, Inicio_Funcao, hdt_inicio   ");

            if (xInconsistencia == "G")  //sem ghe
            {
                strSQL.Append(" , '( Sem GHE )' as Filtro ");
            }
            else if (xInconsistencia == "F")  //sem classif.funcional
            {
                strSQL.Append(" , '( Sem Classif.Funcional )' as Filtro ");
            }
            else if (xInconsistencia == "C")  //sem CPF
            {
                strSQL.Append(" , '( Sem CPF )' as Filtro ");
            }
            else if (xInconsistencia == "D")  //classif.funcional encerrada e sem data demissão
            {
                strSQL.Append(" , '( Classif.Funcional encerrada e sem demissão )' as Filtro ");
            }


            strSQL.Append(" from ");
            strSQL.Append(" ( ");
            strSQL.Append(" select case when tcod_empr<>'' then tno_empg + ' (' + tcod_empr + ')' else tno_empg end as Colaborador,  tNO_CPF as CPF, tNO_Identidade as RG,  ");
            strSQL.Append(" case   ");
            strSQL.Append("    when max( tno_Func ) is NULL then 'SEM GHE' ");
            strSQL.Append("    else  max( tno_Func ) ");
            strSQL.Append("    end ");
            strSQL.Append(" as GHE, tNO_STR_EMPR as  Setor, tNO_FUNC_Empr as Funcao, tsexo, Admissao,  ");
            strSQL.Append(" Nascimento, Idade, nId_Empregado, nIND_BENEFICIARIO, hdt_dem, NomeAbreviado, NomeCodigo, temail, Inicio_Funcao, hdt_inicio    ");
            strSQL.Append(" from ");
            strSQL.Append(" ( ");
            strSQL.Append("    select distinct a.tno_empg, tcod_empr, tNO_CPF, tNO_Identidade, tno_func, e.tno_str_empr, f.tno_func_empr, tsexo, convert( char(10), hdt_adm, 103 ) as Admissao, convert( char(10),hDT_NASC,103) as Nascimento,  ");
            strSQL.Append(" datediff(yyyy, hdt_nasc, getdate()) as Idade, a.nId_Empregado, a.nInd_Beneficiario, hdt_dem, NomeAbreviado, NomeCodigo, temail, convert( char(10), hDt_Inicio, 103 ) as Inicio_Funcao, hdt_inicio ");
            strSQL.Append("    from tblempregado as a left join tblempregado_funcao as b ");
            strSQL.Append("    on ( a.nid_empregado = b.nid_empregado ) ");


            //condição para pegar ultimo apenas
            if (xTipo != "H")
            {
                strSQL.Append("    and b.nid_empr in ( " + xFiltro + " )  ");

                if (xInconsistencia == "F")  //sem classif.funcional
                    strSQL.Append(" left join (   ");
                else
                    strSQL.Append(" join (   ");

                strSQL.Append("    select convert( char(15), nId_Empregado ) + convert( char(10),max( hDt_Inicio ),103 ) as Chave  ");
                strSQL.Append("    from  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblempregado_Funcao  ");

                strSQL.Append(" where nid_empr in ( " + xFiltro + " )  ");


                strSQL.Append("    group by nId_Empregado  ");
                strSQL.Append(" ) as tx80 on tx80.Chave = convert( char(15), b.nId_Empregado ) + convert( char(10),b.hDt_Inicio,103 )  ");

            }

            strSQL.Append("    left join tblfunc_empregado as c ");
            strSQL.Append("    on ( b.nid_empregado_funcao = c.nid_empregado_funcao and c.nid_laud_tec in ( select  ");
            strSQL.Append(" top 1 nID_LAUD_TEC from tblLAUDO_TEC where nid_empr = b.nid_empr order by hDT_LAUDO desc ) ) ");
            strSQL.Append("    left join tblfunc as d ");
            strSQL.Append("    on ( c.nid_func = d.nid_func and d.nid_laud_tec in ( select top 1 nID_LAUD_TEC from  ");
            strSQL.Append(" tblLAUDO_TEC where nid_empr =  b.nid_empr  order by hDT_LAUDO desc ) ) ");
            strSQL.Append("    left join tblSetor as e ");
            strSQL.Append("    on ( b.nid_setor = e.nid_setor ) ");
            strSQL.Append("    left join tblfuncao as f ");
            strSQL.Append("    on ( b.nid_funcao = f.nid_funcao )   ");

            if (xInconsistencia == "F")  //sem classif.funcional
            {
                strSQL.Append("    left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.pessoa as t on ( a.nid_empr = t.idpessoa ) ");
                strSQL.Append("    where ( a.nid_empr in ( " + xFiltro + " )  ");
            }
            else
            {
                strSQL.Append("    left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.pessoa as t on ( b.nid_empr = t.idpessoa ) ");
                strSQL.Append("    where ( b.nid_empr in ( " + xFiltro + " )  ");
            }
            //strSQL.Append("    and a.nid_empr =  " + xIdCliente.ToString() + "  ");


            if (xClassif == "S")
            {
                strSQL.Append("  and ( b.nId_Empr = " + xIdCliente.ToString() + " and hDt_Termino is null )  ");
            }


            if (xAtivosEm != "0")
            {
                strSQL.Append("  and convert( smalldatetime, '" + xAtivosEm + "', 103 ) between hdt_Adm and case when hDt_Dem is null then '01-01-2050' else hdt_dem end  ");
            }

            if (xSemEmail == "S")
            {
                strSQL.Append("  and ( teMail is NULL or ltrim(rtrim(teMail)) = '' ) ");
            }

            if (xDefic == "S")
            {
                strSQL.Append("  and a.nInd_Beneficiario in (1,2) ");
            }

            if (xSit == "1")
            {
                strSQL.Append("    and a.hdt_dem is null   ");
            }
            else if (xSit == "2")
            {
                if (xInconsistencia != "D")  //classif.funcional encerrada e sem data demissão
                {
                    strSQL.Append("    and a.hdt_dem is not null "); // and b.hdt_termino is null  ");
                }
                else
                {
                    strSQL.Append("    and a.hdt_dem is null   ");
                }
            }

            strSQL.Append(" and t.isinativo = 0 ");

            strSQL.Append("     ) ");
            strSQL.Append(" ) ");
            strSQL.Append(" as tx1 ");
            strSQL.Append(" group by tno_empg, tcod_empr, tNO_CPF, tNO_Identidade, tNO_STR_EMPR, tNO_FUNC_Empr, tsexo, Admissao, Nascimento, Idade , nId_Empregado, nIND_BENEFICIARIO, hdt_dem, NomeAbreviado, NomeCodigo, temail, Inicio_Funcao, hdt_inicio   ");

            strSQL.Append(" union ");

            strSQL.Append(" select case when tcod_empr<>'' then tno_empg + ' (' + tcod_empr + ')' else tno_empg end as Colaborador, tNO_CPF as CPF, tNO_Identidade as RG,  ");
            strSQL.Append(" case   ");
            strSQL.Append("    when min( tno_Func ) is NULL then 'SEM GHE' ");
            strSQL.Append("    else min( tno_Func ) ");
            strSQL.Append("    end ");
            strSQL.Append(" as GHE, tNO_STR_EMPR as  Setor, tNO_FUNC_Empr as Funcao , tsexo, Admissao,  ");
            strSQL.Append(" Nascimento, Idade, nId_Empregado, nInd_Beneficiario, hdt_dem, NomeAbreviado, NomeCodigo, temail, Inicio_Funcao, hdt_inicio    ");
            strSQL.Append(" from ");
            strSQL.Append(" ( ");
            strSQL.Append("    select distinct a.tno_empg, a.tcod_empr, tNO_CPF, tNO_Identidade, tno_func, e.tno_str_empr, f.tno_func_empr, tsexo, convert( char(10), hdt_adm, 103 ) as Admissao, convert( char(10),hDT_NASC,103) as Nascimento,  ");
            strSQL.Append(" datediff(yyyy, hdt_nasc, getdate()) as Idade, a.nId_Empregado, a.nInd_Beneficiario, hdt_dem, NomeAbreviado, NomeCodigo, temail, convert( char(10), hDt_Inicio, 103 ) as Inicio_Funcao, hdt_inicio   ");
            strSQL.Append("    from tblempregado as a left join tblempregado_funcao as b ");
            strSQL.Append("    on ( a.nid_empregado = b.nid_empregado ) ");
            strSQL.Append("    left join tblfunc_empregado as c ");
            strSQL.Append("    on ( b.nid_empregado_funcao = c.nid_empregado_funcao and c.nid_laud_tec in ( select  ");
            strSQL.Append(" top 1 nID_LAUD_TEC from tblLAUDO_TEC where nid_empr = b.nid_empr  order by hDT_LAUDO desc ) ) ");


            //condição para pegar ultimo apenas
            if (xTipo != "H")
            {

                strSQL.Append("    and b.nid_empr in ( " + xFiltro + " )   ");

                if (xInconsistencia == "F")  //sem classif.funcional
                    strSQL.Append(" left join (   ");
                else
                    strSQL.Append(" join (   ");

                strSQL.Append("    select convert( char(15), nId_Empregado ) + convert( char(10),max( hDt_Inicio ),103 ) as Chave  ");
                strSQL.Append("    from  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblempregado_Funcao  ");

                strSQL.Append(" where nid_empr in ( " + xFiltro + " )  ");



                strSQL.Append("    group by nId_Empregado  ");
                strSQL.Append(" ) as tx80 on tx80.Chave = convert( char(15), b.nId_Empregado ) + convert( char(10),b.hDt_Inicio,103 )  ");

            }


            strSQL.Append("    left join tblfunc as d ");
            strSQL.Append("    on ( c.nid_func = d.nid_func and d.nid_laud_tec in ( select top 1 nID_LAUD_TEC from  ");
            strSQL.Append(" tblLAUDO_TEC where nid_empr = b.nid_empr  order by hDT_LAUDO desc ) ) ");
            strSQL.Append("    left join tblSetor as e ");
            strSQL.Append("    on ( b.nid_setor = e.nid_setor ) ");
            strSQL.Append("    left join tblfuncao as f ");
            strSQL.Append("    on ( b.nid_funcao = f.nid_funcao )   ");


            if (xInconsistencia == "F")  //sem classif.funcional
            {
                strSQL.Append("    left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.pessoa as t on ( a.nid_empr = t.idpessoa ) ");
                strSQL.Append("    where ( a.nid_empr in ( " + xFiltro + " )  ");
            }
            else
            {
                strSQL.Append("    left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.pessoa as t on ( b.nid_empr = t.idpessoa ) ");
                strSQL.Append("    where ( b.nid_empr in ( " + xFiltro + " )  ");
            }


            //strSQL.Append("        and a.nid_empr =  " + xIdCliente.ToString() + "  ");

            if (xClassif == "S")
            {
                strSQL.Append("  and ( b.nId_Empr = " + xIdCliente.ToString() + " and hDt_Termino is null )  ");
            }


            if (xAtivosEm != "0")
            {
                strSQL.Append("  and convert( smalldatetime, '" + xAtivosEm + "', 103 ) between hdt_Adm and case when hDt_Dem is null then '01-01-2050' else hdt_dem end   ");
            }

            if (xSemEmail == "S")
            {
                strSQL.Append("  and ( teMail is NULL or ltrim(rtrim(teMail)) = '' ) ");
            }

            if (xDefic == "S")
            {
                strSQL.Append("  and a.nInd_Beneficiario in (1,2) ");
            }

            if (xSit == "1")
            {
                strSQL.Append("    and a.hdt_dem is null   ");
            }
            else if (xSit == "2")
            {
                if (xInconsistencia != "D")  //classif.funcional encerrada e sem data demissão
                {
                    strSQL.Append("    and a.hdt_dem is not null "); // and b.hdt_termino is null  ");
                }
                else
                {
                    strSQL.Append("    and a.hdt_dem is null   ");
                }
            }

            strSQL.Append(" and t.isinativo = 0 ");

            strSQL.Append("     ) ");
            strSQL.Append(" ) ");
            strSQL.Append(" as tx1 ");
            strSQL.Append(" group by tno_empg, tcod_empr, tNO_CPF, tNO_Identidade, tNO_STR_EMPR , tNO_FUNC_Empr, tsexo, Admissao, Nascimento, Idade , nId_Empregado, nIND_BENEFICIARIO, hdt_dem, NomeAbreviado, NomeCodigo, temail, Inicio_Funcao, hdt_inicio   ");
            strSQL.Append(" ) ");
            strSQL.Append(" as tx99 left join  ");
            strSQL.Append(" (  ");
            strSQL.Append("   select idEmpregado, DataInicial, DataVolta, DataPrevista ");
            strSQL.Append("   from " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.afastamento where ");
            strSQL.Append("   convert( char(15), idempregado ) + convert( char(10), DataInicial ) in ");
            strSQL.Append("   ( ");
            strSQL.Append("     select convert( char(15), idempregado ) + convert( char(10), max(DataInicial) )  ");
            strSQL.Append("     from " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.afastamento ");
            strSQL.Append("     group by idempregado ");
            strSQL.Append("   ) ");
            strSQL.Append("   and DataInicial < getdate() and ( DataVolta >= getdate() or DataPrevista >= getdate() ) ");
            strSQL.Append("  ) as tx100 ");
            strSQL.Append(" on  tx99.nID_EMPREGADO = tx100.IdEmpregado ");

            if (xInconsistencia == "G")  //sem ghe
            {
                strSQL.Append(" where nId_Empregado in (  Select nId_Empregado ");
                strSQL.Append(" from tblempregado as a where nid_empregado in ");
                strSQL.Append(" ( ");
                strSQL.Append(" select nid_empregado from tblempregado_funcao where ");
                strSQL.Append(" convert(char(14), nid_empregado) + convert(char(10), hdt_inicio, 103) in ");
                strSQL.Append(" (select convert(char(14), nid_empregado) + convert(char(10), max(hdt_inicio), 103) ");
                strSQL.Append(" from tblEMPREGADO_FUNCAO ");
                strSQL.Append(" group by nid_empregado ");
                strSQL.Append(" )  ");
                strSQL.Append(" and nid_empregado_funcao not in ( ");
                strSQL.Append(" select nid_empregado_funcao from tblfunc_empregado where nid_func in (select ");
                strSQL.Append(" nid_func from tblfunc where nID_LAUD_TEC in (select nid_laud_tec from tblLAUDO_TEC )  ");
                strSQL.Append(" ) ");
                strSQL.Append(" ) ");
                strSQL.Append(" ) ");
                strSQL.Append(" ) ");
            }
            else if (xInconsistencia == "F")  //sem classif.funcional
            {
                strSQL.Append(" where nId_Empregado not in (  ");
                strSQL.Append(" select nId_Empregado from tblEmpregado_Funcao ");
                strSQL.Append(" ) ");
            }
            else if (xInconsistencia == "C")  //sem CPF
            {
                strSQL.Append(" where nId_Empregado in (  Select nId_Empregado ");
                strSQL.Append(" from tblempregado as a  ");
                strSQL.Append(" where ( tno_CPF is null or tno_CPF = '') ");
                strSQL.Append(" ) ");
            }
            else if (xInconsistencia == "D")  //classif.funcional encerrada e sem data demissão
            {
                strSQL.Append(" where nId_Empregado not in (  Select nId_Empregado ");
                strSQL.Append(" from tblempregado_Funcao as a  ");
                strSQL.Append(" where ( hDT_Termino is null ) ");
                strSQL.Append(" ) ");
            }

            strSQL.Append(" order by DataVolta, Colaborador, hdt_inicio desc ");

            //m_ds = new DataSet("Result");

            using (SqlConnection cnn = new SqlConnection(Ilitera.Data.SQLServer.EntitySQLServer.GetConnection_Sied()))
            {
                SqlDataAdapter da;
                cnn.Open();


                da = new SqlDataAdapter(strSQL.ToString(), cnn);
                da.SelectCommand.CommandTimeout = 1800;
                da.Fill(m_ds, "Result");

                cnn.Close();

                da.Dispose();
            }

            return m_ds;

        }



        public DataSet Gerar_DS_Relatorio_Colaboradores2(int xIdCliente, string xD_Inicial, string xCliente, string zEmp, string xFiltro, string xSit, string xTipo, string xDefic, string xAtivosEm, string xSemEmail, string xInconsistencia, string xClassif)
        {
            //string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection();
            StringBuilder strSQL = new StringBuilder();

            //DataSet m_ds;


            DataTable table = new DataTable("Result");
            table.Columns.Add("Colaborador", Type.GetType("System.String"));
            table.Columns.Add("Matricula", Type.GetType("System.String"));
            table.Columns.Add("GHE", Type.GetType("System.String"));
            table.Columns.Add("Setor", Type.GetType("System.String"));
            table.Columns.Add("Funcao", Type.GetType("System.String"));
            table.Columns.Add("tSexo", Type.GetType("System.String"));
            table.Columns.Add("Admissao", Type.GetType("System.String"));
            table.Columns.Add("Nascimento", Type.GetType("System.String"));
            table.Columns.Add("Idade", Type.GetType("System.String"));
            table.Columns.Add("DataInicial", Type.GetType("System.String"));
            table.Columns.Add("DataVolta", Type.GetType("System.String"));
            table.Columns.Add("DataPrevista", Type.GetType("System.String"));
            table.Columns.Add("Deficiencia", Type.GetType("System.String"));
            table.Columns.Add("CPF", Type.GetType("System.String"));
            table.Columns.Add("teMail", Type.GetType("System.String"));
            table.Columns.Add("RG", Type.GetType("System.String"));

            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);



            strSQL.Append(" select Colaborador, Matricula, CPF, RG, GHE, Setor, Funcao, tsexo, Admissao, Nascimento, idade, case when DataInicial is not null then 'Afastado desde ' + convert( char(10), DataInicial, 103 )      when hdt_dem is not null then 'Inativo'   when hdt_dem is null then 'Ativo'   end as DataInicial, ");
            strSQL.Append(" case when nInd_Beneficiario = 1 then 'Beneficiário Reabilitado' when nInd_Beneficiario=2 then 'Portador de Deficiência Habilitada'   when nInd_Beneficiario=0 then 'Não aplicável' end as Deficiencia, ");
            strSQL.Append(" NomeAbreviado + '   CNPJ: ' + NomeCodigo as DataVolta, convert( char(10), getdate(), 103 ) as DataPrevista, temail, Inicio_Funcao, hdt_inicio   ");
            strSQL.Append(" from ");
            strSQL.Append(" ( ");
            strSQL.Append(" select case when tcod_empr<>'' then  tcod_empr else '' end as Matricula, tno_empg as Colaborador,  tNO_CPF as CPF,  tno_Identidade as RG, ");
            strSQL.Append(" case   ");
            strSQL.Append("    when max( tno_Func ) is NULL then 'SEM GHE' ");
            strSQL.Append("    else  max( tno_Func ) ");
            strSQL.Append("    end ");
            strSQL.Append(" as GHE, tNO_STR_EMPR as  Setor, tNO_FUNC_Empr as Funcao, tsexo, Admissao,  ");
            strSQL.Append(" Nascimento, Idade, nId_Empregado, nIND_BENEFICIARIO, hdt_dem, NomeAbreviado, NomeCodigo, temail, Inicio_Funcao, hdt_inicio    ");
            strSQL.Append(" from ");
            strSQL.Append(" ( ");
            strSQL.Append("    select distinct a.tno_empg, tcod_empr, tNO_CPF, tno_Identidade, tno_func, e.tno_str_empr, f.tno_func_empr, tsexo, convert( char(10), hdt_adm, 103 ) as Admissao, convert( char(10),hDT_NASC,103) as Nascimento,  ");
            strSQL.Append(" datediff(yyyy, hdt_nasc, getdate()) as Idade, a.nId_Empregado, a.nInd_Beneficiario, hdt_dem, NomeAbreviado, NomeCodigo, temail, convert( char(10), hDt_Inicio, 103 ) as Inicio_Funcao, hdt_inicio ");
            strSQL.Append("    from tblempregado as a left join tblempregado_funcao as b ");
            strSQL.Append("    on ( a.nid_empregado = b.nid_empregado ) ");


            //condição para pegar ultimo apenas
            if (xTipo != "H")
            {
                strSQL.Append("    and b.nid_empr in ( " + xFiltro + " )  ");

                if (xInconsistencia == "F")  //sem classif.funcional
                    strSQL.Append(" left join (   ");
                else
                    strSQL.Append(" join (   ");

                strSQL.Append("    select convert( char(15), nId_Empregado ) + convert( char(10),max( hDt_Inicio ),103 ) as Chave  ");
                strSQL.Append("    from  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblempregado_Funcao  ");

                strSQL.Append(" where nid_empr in ( " + xFiltro + " )  ");


                strSQL.Append("    group by nId_Empregado  ");
                strSQL.Append(" ) as tx80 on tx80.Chave = convert( char(15), b.nId_Empregado ) + convert( char(10),b.hDt_Inicio,103 )  ");

            }

            strSQL.Append("    left join tblfunc_empregado as c ");
            strSQL.Append("    on ( b.nid_empregado_funcao = c.nid_empregado_funcao and c.nid_laud_tec in ( select  ");
            strSQL.Append(" top 1 nID_LAUD_TEC from tblLAUDO_TEC where nid_empr = b.nid_empr order by hDT_LAUDO desc ) ) ");
            strSQL.Append("    left join tblfunc as d ");
            strSQL.Append("    on ( c.nid_func = d.nid_func and d.nid_laud_tec in ( select top 1 nID_LAUD_TEC from  ");
            strSQL.Append(" tblLAUDO_TEC where nid_empr =  b.nid_empr  order by hDT_LAUDO desc ) ) ");
            strSQL.Append("    left join tblSetor as e ");
            strSQL.Append("    on ( b.nid_setor = e.nid_setor ) ");
            strSQL.Append("    left join tblfuncao as f ");
            strSQL.Append("    on ( b.nid_funcao = f.nid_funcao )   ");

            if (xInconsistencia == "F")  //sem classif.funcional
            {
                strSQL.Append("    left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.pessoa as t on ( a.nid_empr = t.idpessoa ) ");
                strSQL.Append("    where ( a.nid_empr in ( " + xFiltro + " )  ");
            }
            else
            {
                strSQL.Append("    left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.pessoa as t on ( b.nid_empr = t.idpessoa ) ");
                strSQL.Append("    where ( b.nid_empr in ( " + xFiltro + " )  ");
            }


            //strSQL.Append("    and a.nid_empr =  " + xIdCliente.ToString() + "  ");

            if ( xClassif == "S" )
            {
                strSQL.Append("  and ( b.nId_Empr = " + xIdCliente.ToString() + " and hDt_Termino is null )  ");
            }


            if (xAtivosEm != "0")
            {
                strSQL.Append("  and convert( smalldatetime, '" + xAtivosEm + "', 103 ) between hdt_Adm and case when hDt_Dem is null then '01-01-2050' else hdt_dem end  ");
            }

            if (xSemEmail == "S")
            {
                strSQL.Append("  and ( teMail is NULL or ltrim(rtrim(teMail)) = '' ) ");
            }

            if (xDefic == "S")
            {
                strSQL.Append("  and a.nInd_Beneficiario in (1,2) ");
            }

            if (xSit == "1")
            {
                strSQL.Append("    and a.hdt_dem is null   ");
            }
            else if (xSit == "2")
            {
                if (xInconsistencia != "D")  //classif.funcional encerrada e sem data demissão
                {
                    strSQL.Append("    and a.hdt_dem is not null "); // and b.hdt_termino is null  ");
                }
                else
                {
                    strSQL.Append("    and a.hdt_dem is null   ");
                }
            }

            strSQL.Append(" and t.isinativo = 0 ");

            strSQL.Append("     ) ");
            strSQL.Append(" ) ");
            strSQL.Append(" as tx1 ");
            strSQL.Append(" group by tno_empg, tcod_empr, tNO_CPF, tno_Identidade, tNO_STR_EMPR, tNO_FUNC_Empr, tsexo, Admissao, Nascimento, Idade , nId_Empregado, nIND_BENEFICIARIO, hdt_dem, NomeAbreviado, NomeCodigo, temail, Inicio_Funcao, hdt_inicio   ");

            strSQL.Append(" union ");

            strSQL.Append(" select case when tcod_empr<>'' then  tcod_empr else '' end as Matricula, tno_empg as Colaborador, tNO_CPF as CPF, tno_Identidade as RG,  ");
            strSQL.Append(" case   ");
            strSQL.Append("    when min( tno_Func ) is NULL then 'SEM GHE' ");
            strSQL.Append("    else min( tno_Func ) ");
            strSQL.Append("    end ");
            strSQL.Append(" as GHE, tNO_STR_EMPR as  Setor, tNO_FUNC_Empr as Funcao , tsexo, Admissao,  ");
            strSQL.Append(" Nascimento, Idade, nId_Empregado, nInd_Beneficiario, hdt_dem, NomeAbreviado, NomeCodigo, temail, Inicio_Funcao, hdt_inicio    ");
            strSQL.Append(" from ");
            strSQL.Append(" ( ");
            strSQL.Append("    select distinct a.tno_empg, a.tcod_empr, tNO_CPF, tno_Identidade, tno_func, e.tno_str_empr, f.tno_func_empr, tsexo, convert( char(10), hdt_adm, 103 ) as Admissao, convert( char(10),hDT_NASC,103) as Nascimento,  ");
            strSQL.Append(" datediff(yyyy, hdt_nasc, getdate()) as Idade, a.nId_Empregado, a.nInd_Beneficiario, hdt_dem, NomeAbreviado, NomeCodigo, temail, convert( char(10), hDt_Inicio, 103 ) as Inicio_Funcao, hdt_inicio   ");
            strSQL.Append("    from tblempregado as a left join tblempregado_funcao as b ");
            strSQL.Append("    on ( a.nid_empregado = b.nid_empregado ) ");
            strSQL.Append("    left join tblfunc_empregado as c ");
            strSQL.Append("    on ( b.nid_empregado_funcao = c.nid_empregado_funcao and c.nid_laud_tec in ( select  ");
            strSQL.Append(" top 1 nID_LAUD_TEC from tblLAUDO_TEC where nid_empr = b.nid_empr  order by hDT_LAUDO desc ) ) ");


            //condição para pegar ultimo apenas
            if (xTipo != "H")
            {

                strSQL.Append("    and b.nid_empr in ( " + xFiltro + " )   ");

                if (xInconsistencia == "F")  //sem classif.funcional
                    strSQL.Append(" left join (   ");
                else
                    strSQL.Append(" join (   ");

                strSQL.Append("    select convert( char(15), nId_Empregado ) + convert( char(10),max( hDt_Inicio ),103 ) as Chave  ");
                strSQL.Append("    from  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblempregado_Funcao  ");

                strSQL.Append(" where nid_empr in ( " + xFiltro + " )  ");



                strSQL.Append("    group by nId_Empregado  ");
                strSQL.Append(" ) as tx80 on tx80.Chave = convert( char(15), b.nId_Empregado ) + convert( char(10),b.hDt_Inicio,103 )  ");

            }


            strSQL.Append("    left join tblfunc as d ");
            strSQL.Append("    on ( c.nid_func = d.nid_func and d.nid_laud_tec in ( select top 1 nID_LAUD_TEC from  ");
            strSQL.Append(" tblLAUDO_TEC where nid_empr = b.nid_empr  order by hDT_LAUDO desc ) ) ");
            strSQL.Append("    left join tblSetor as e ");
            strSQL.Append("    on ( b.nid_setor = e.nid_setor ) ");
            strSQL.Append("    left join tblfuncao as f ");
            strSQL.Append("    on ( b.nid_funcao = f.nid_funcao )   ");


            if (xInconsistencia == "F")  //sem classif.funcional
            {
                strSQL.Append("    left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.pessoa as t on ( a.nid_empr = t.idpessoa ) ");
                strSQL.Append("    where ( a.nid_empr in ( " + xFiltro + " )  ");
            }
            else
            {
                strSQL.Append("    left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.pessoa as t on ( b.nid_empr = t.idpessoa ) ");
                strSQL.Append("    where ( b.nid_empr in ( " + xFiltro + " )  ");
            }

            //strSQL.Append("        and a.nid_empr =  " + xIdCliente.ToString() + "  ");

            if (xClassif == "S")
            {
                strSQL.Append("  and ( b.nId_Empr = " + xIdCliente.ToString() + " and hDt_Termino is null )  ");
            }


            if (xAtivosEm != "0")
            {
                strSQL.Append("  and convert( smalldatetime, '" + xAtivosEm + "', 103 ) between hdt_Adm and case when hDt_Dem is null then '01-01-2050' else hdt_dem end   ");
            }

            if (xSemEmail == "S")
            {
                strSQL.Append("  and ( teMail is NULL or ltrim(rtrim(teMail)) = '' ) ");
            }

            if (xDefic == "S")
            {
                strSQL.Append("  and a.nInd_Beneficiario in (1,2) ");
            }

            if (xSit == "1")
            {
                strSQL.Append("    and a.hdt_dem is null   ");
            }
            else if (xSit == "2")
            {
                if (xInconsistencia != "D")  //classif.funcional encerrada e sem data demissão
                {
                    strSQL.Append("    and a.hdt_dem is not null "); // and b.hdt_termino is null  ");
                }
                else
                {
                    strSQL.Append("    and a.hdt_dem is null   ");
                }
            }

            strSQL.Append(" and t.isinativo = 0 ");

            strSQL.Append("     ) ");
            strSQL.Append(" ) ");
            strSQL.Append(" as tx1 ");
            strSQL.Append(" group by tno_empg, tcod_empr, tNO_CPF,  tno_Identidade, tNO_STR_EMPR , tNO_FUNC_Empr, tsexo, Admissao, Nascimento, Idade , nId_Empregado, nIND_BENEFICIARIO, hdt_dem, NomeAbreviado, NomeCodigo, temail, Inicio_Funcao, hdt_inicio   ");
            strSQL.Append(" ) ");
            strSQL.Append(" as tx99 left join  ");
            strSQL.Append(" (  ");
            strSQL.Append("   select idEmpregado, DataInicial, DataVolta, DataPrevista ");
            strSQL.Append("   from " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.afastamento where ");
            strSQL.Append("   convert( char(15), idempregado ) + convert( char(10), DataInicial ) in ");
            strSQL.Append("   ( ");
            strSQL.Append("     select convert( char(15), idempregado ) + convert( char(10), max(DataInicial) )  ");
            strSQL.Append("     from " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.afastamento ");
            strSQL.Append("     group by idempregado ");
            strSQL.Append("   ) ");
            strSQL.Append("   and DataInicial < getdate() and ( DataVolta >= getdate() or DataPrevista >= getdate() ) ");
            strSQL.Append("  ) as tx100 ");
            strSQL.Append(" on  tx99.nID_EMPREGADO = tx100.IdEmpregado ");


            if (xInconsistencia == "G")  //sem ghe
            {
                strSQL.Append(" where nId_Empregado in (  Select nId_Empregado ");
                strSQL.Append(" from tblempregado as a where nid_empregado in ");
                strSQL.Append(" ( ");
                strSQL.Append(" select nid_empregado from tblempregado_funcao where ");
                strSQL.Append(" convert(char(14), nid_empregado) + convert(char(10), hdt_inicio, 103) in ");
                strSQL.Append(" (select convert(char(14), nid_empregado) + convert(char(10), max(hdt_inicio), 103) ");
                strSQL.Append(" from tblEMPREGADO_FUNCAO ");
                strSQL.Append(" group by nid_empregado ");
                strSQL.Append(" )  ");
                strSQL.Append(" and nid_empregado_funcao not in ( ");
                strSQL.Append(" select nid_empregado_funcao from tblfunc_empregado where nid_func in (select ");
                strSQL.Append(" nid_func from tblfunc where nID_LAUD_TEC in (select nid_laud_tec from tblLAUDO_TEC )  ");
                strSQL.Append(" ) ");
                strSQL.Append(" ) ");
                strSQL.Append(" ) ");
                strSQL.Append(" ) ");
            }
            else if (xInconsistencia == "F")  //sem classif.funcional
            {
                strSQL.Append(" where nId_Empregado not in (  ");
                strSQL.Append(" select nId_Empregado from tblEmpregado_Funcao ");
                strSQL.Append(" ) ");
            }
            else if (xInconsistencia == "C")  //sem CPF
            {
                strSQL.Append(" where nId_Empregado in (  Select nId_Empregado ");
                strSQL.Append(" from tblempregado as a  ");
                strSQL.Append(" where ( tno_CPF is null or tno_CPF = '') ");
                strSQL.Append(" ) ");
            }
            else if (xInconsistencia == "D")  //classif.funcional encerrada e sem data demissão
            {
                strSQL.Append(" where nId_Empregado not in (  Select nId_Empregado ");
                strSQL.Append(" from tblempregado_Funcao as a  ");
                strSQL.Append(" where ( hDT_Termino is null ) ");
                strSQL.Append(" ) ");
            }




            strSQL.Append(" order by DataVolta, Colaborador, hdt_inicio desc ");

            //m_ds = new DataSet("Result");

            using (SqlConnection cnn = new SqlConnection(Ilitera.Data.SQLServer.EntitySQLServer.GetConnection_Sied()))
            {
                SqlDataAdapter da;
                cnn.Open();

                
                da = new SqlDataAdapter(strSQL.ToString(), cnn);
                da.SelectCommand.CommandTimeout = 1800;
                da.Fill(m_ds, "Result");

                cnn.Close();

                da.Dispose();
            }

            return m_ds;

        }




        public DataSet Gerar_DS_Relatorio_Colaboradores_Aptidoes(int xIdCliente)
        {
            //string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection();
            StringBuilder strSQL = new StringBuilder();

            //DataSet m_ds;


            DataTable table = new DataTable("Result");
            table.Columns.Add("Colaborador", Type.GetType("System.String"));
            table.Columns.Add("GHE", Type.GetType("System.String"));
            table.Columns.Add("Funcao", Type.GetType("System.String"));
            table.Columns.Add("DataVolta", Type.GetType("System.String"));
            table.Columns.Add("DataPrevista", Type.GetType("System.String"));

            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);

            SqlCommand rCommand = new SqlCommand();

            rCommand.Parameters.Add("@xIdCliente", SqlDbType.NChar).Value = xIdCliente.ToString();


            rCommand.CommandText = " select distinct TFuncao as Funcao, Empresa as DataVolta, Colaborador, tno_CPF as CPF, "
            + "  case when apt_trabalho_altura = 1 then ' Trabalho Altura | ' else '' end + "
            + "  case when apt_espaco_confinado = 1 then ' Espaço Confinado | ' else '' end + "
            + "  case when apt_transporte = 1       then ' Transporte Motorizado | ' else '' end + "
            + "  case when apt_submerso = 1         then ' Ativ.Submersas | ' else ''  end + "
            + "  case when apt_eletricidade = 1     then ' Eletricidade | ' else ''  end + "
            + "  case when apt_aquaviario = 1       then ' Aquaviario | ' else ''  end + "
            + "  case when apt_alimento = 1         then ' Manip.Alimentos | ' else ''  end + "
            + "  case when apt_brigadista = 1       then ' Brigadista | ' else ''  end + "
            + "  case when apt_socorrista = 1       then ' Socorrista ' else ''  end as GHE, "
            + "   t80.Nome as DataPrevista "
            + "  from( "
            + "    select nId_Empregado, Empresa, Colaborador, tno_CPF, apt_Trabalho_Altura, apt_Espaco_Confinado, apt_Transporte, apt_Submerso, apt_Eletricidade, apt_Aquaviario, apt_Alimento, apt_Brigadista, apt_Socorrista, nid_empr,  "
            + "  case when apt_trabalho_altura = 1 then 2 else 0 end as Id2, "
            + "  case when apt_espaco_confinado = 1 then 1 else 0 end as Id1, "
            + "  case when apt_transporte = 1       then 3 else 0 end as Id3, "
            + "  case when apt_submerso = 1         then 4 else 0 end as Id4, "
            + "  case when apt_eletricidade = 1     then 5 else 0 end as Id5, "
            + "  case when apt_aquaviario = 1       then 6 else 0 end as Id6, "
            + "  case when apt_alimento = 1         then 7 else 0 end as Id7, "
            + "  case when apt_brigadista = 1       then 8 else 0 end as Id8, "
            + "  case when apt_socorrista = 1       then 9 else 0 end as Id9 "
            + "  from "
            + "   ( "
            + "    select b.nId_Empregado,c.NomeAbreviado as Empresa, b.Tno_Empg as Colaborador, apt_Trabalho_Altura, apt_Espaco_Confinado, apt_Transporte, apt_Submerso, apt_Eletricidade, apt_Aquaviario, apt_Alimento, apt_Brigadista, apt_Socorrista, b.nid_empr, b.tno_cpf "
            + "    from tblempregado_aptidao as a "
            + "    left join tblempregado as b on(a.nid_empregado = b.nid_empregado) "
            + "  left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.pessoa as c on(b.nid_empr = c.idpessoa) where b.hdt_dem is null and b.nid_empr = @xIdCliente   "
            + "    union "
            + "    select b.nId_Empregado, c.NomeAbreviado as Empresa, b.Tno_Empg as Colaborador, apt_Trabalho_Altura, apt_Espaco_Confinado, apt_Transporte, apt_Submerso, apt_Eletricidade, apt_Aquaviario, apt_Alimento, apt_Brigadista, apt_Socorrista, b.nid_empr, b.tno_cpf "
            + "    from tblfunc_aptidao as d "
            + "    left join tblfunc_empregado as e on(d.nid_func = e.nid_func) "
            + "    left join tblEMPREGADO_FUNCAO as f on(e.nid_empregado_Funcao = f.nid_empregado_Funcao) "
            + "    left join tblempregado as b on(f.nid_empregado = b.nid_empregado) "
            + "    left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.pessoa as c on(b.nid_empr = c.idpessoa) "
            + "    left join tblfunc as g on(d.nid_func = g.nid_func) "            
            + "    left join tbllaudo_tec as h on(g.nid_laud_tec = h.nid_laud_tec) "
            + "    where b.hdt_dem is null and b.nid_empr = @xIdCliente   and convert(char(12), h.nid_empr) + ' ' + convert(char(10), hdt_laudo, 103) in "
            + "    (select convert(char(12), nid_empr) + ' ' + convert(char(10), max(hdt_laudo), 103) "
            + "       from tbllaudo_tec "
            + "       group by nid_empr "
            + "    ) "
            + "  ) "
            + "  as tx33 "
            + "  ) as tx90 "
            + "  left join tblEMPREGADO_APTIDAO_PLANEJAMENTO as t1 on(t1.nid_empr = tx90.nid_empr and(id1 = t1.nid_aptidao or id2 = t1.nid_aptidao or id3 = t1.nid_aptidao or id4 = t1.nid_aptidao or id5 = t1.nid_aptidao or id6 = t1.nid_aptidao or id7 = t1.nid_aptidao or id8 = t1.nid_aptidao or id9 = t1.nid_aptidao)) "
            + "  left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.examedicionario as t80 on(t1.IdExameDicionario = t80.IdExameDicionario) "
            + "  left join ( "
            + "  select nId_Empregado, tno_func_empr as TFuncao from tblEmpregado_Funcao as EF left join tblFUNCAO as TF on(EF.nID_FUNCAO = TF.nID_FUNCAO) "
            + "  WHERE EF.nID_EMPREGADO = nID_EMPREGADO "
            + "  and convert(char(12), nId_Empregado) + ' ' + convert(char(10), hdt_Inicio, 103) in "
            + "  (select  convert(char(12), nId_Empregado) + ' ' + convert(char(10), max(hdt_Inicio), 103) from tblEmpregado_Funcao where nid_empr = @xIdCliente  group by nid_Empregado ) "
            + "  ) as TFUncao on(tx90.nid_Empregado = tFUncao.nID_EMPREGADO) "
            + "  where tx90.nid_empr = @xIdCliente "
            + "  order by Empresa, Colaborador, t80.Nome ";


            //strSQL.Append(" select distinct TFuncao as Funcao, Empresa as DataVolta, Colaborador, ");
            //strSQL.Append("  case when apt_trabalho_altura = 1 then ' Trabalho Altura | ' else '' end + ");
            //strSQL.Append("  case when apt_espaco_confinado = 1 then ' Espaço Confinado | ' else '' end + ");
            //strSQL.Append("  case when apt_transporte = 1       then ' Transporte Motorizado | ' else '' end + ");
            //strSQL.Append("  case when apt_submerso = 1         then ' Ativ.Submersas | ' else ''  end + ");
            //strSQL.Append("  case when apt_eletricidade = 1     then ' Eletricidade | ' else ''  end + ");
            //strSQL.Append("  case when apt_aquaviario = 1       then ' Aquaviario | ' else ''  end + ");
            //strSQL.Append("  case when apt_alimento = 1         then ' Manip.Alimentos | ' else ''  end + ");
            //strSQL.Append("  case when apt_brigadista = 1       then ' Brigadista | ' else ''  end + ");
            //strSQL.Append("  case when apt_socorrista = 1       then ' Socorrista ' else ''  end as GHE, ");
            //strSQL.Append("   t80.Nome as DataPrevista ");
            //strSQL.Append("  from( ");
            //strSQL.Append("    select nId_Empregado, Empresa, Colaborador, apt_Trabalho_Altura, apt_Espaco_Confinado, apt_Transporte, apt_Submerso, apt_Eletricidade, apt_Aquaviario, apt_Alimento, apt_Brigadista, apt_Socorrista, nid_empr, ");
            //strSQL.Append("  case when apt_trabalho_altura = 1 then 2 else 0 end as Id2, ");
            //strSQL.Append("  case when apt_espaco_confinado = 1 then 1 else 0 end as Id1, ");
            //strSQL.Append("  case when apt_transporte = 1       then 3 else 0 end as Id3, ");
            //strSQL.Append("  case when apt_submerso = 1         then 4 else 0 end as Id4, ");
            //strSQL.Append("  case when apt_eletricidade = 1     then 5 else 0 end as Id5, ");
            //strSQL.Append("  case when apt_aquaviario = 1       then 6 else 0 end as Id6, ");
            //strSQL.Append("  case when apt_alimento = 1         then 7 else 0 end as Id7, ");
            //strSQL.Append("  case when apt_brigadista = 1       then 8 else 0 end as Id8, ");
            //strSQL.Append("  case when apt_socorrista = 1       then 9 else 0 end as Id9 ");
            //strSQL.Append("  from ");
            //strSQL.Append("   ( ");
            //strSQL.Append("    select b.nId_Empregado,c.NomeAbreviado as Empresa, b.Tno_Empg as Colaborador, apt_Trabalho_Altura, apt_Espaco_Confinado, apt_Transporte, apt_Submerso, apt_Eletricidade, apt_Aquaviario, apt_Alimento, apt_Brigadista, apt_Socorrista, b.nid_empr ");
            //strSQL.Append("    from tblempregado_aptidao as a ");
            //strSQL.Append("    left join tblempregado as b on(a.nid_empregado = b.nid_empregado) ");
            //strSQL.Append("  left join opsa.dbo.pessoa as c on(b.nid_empr = c.idpessoa) where b.hdt_dem is null ");


            //strSQL.Append("    union ");

            //strSQL.Append("    select b.nId_Empregado, c.NomeAbreviado as Empresa, b.Tno_Empg as Colaborador, apt_Trabalho_Altura, apt_Espaco_Confinado, apt_Transporte, apt_Submerso, apt_Eletricidade, apt_Aquaviario, apt_Alimento, apt_Brigadista, apt_Socorrista, b.nid_empr ");
            //strSQL.Append("    from tblfunc_aptidao as d ");
            //strSQL.Append("    left join tblfunc_empregado as e on(d.nid_func = e.nid_func) ");
            //strSQL.Append("    left join tblEMPREGADO_FUNCAO as f on(e.nid_empregado_Funcao = f.nid_empregado_Funcao) ");
            //strSQL.Append("    left join tblempregado as b on(f.nid_empregado = b.nid_empregado) ");
            //strSQL.Append("    left join opsa.dbo.pessoa as c on(b.nid_empr = c.idpessoa) ");
            //strSQL.Append("    left join tblfunc as g on(d.nid_func = g.nid_func) ");
            //strSQL.Append("    left join tbllaudo_tec as h on(g.nid_laud_tec = h.nid_laud_tec) ");
            //strSQL.Append("    where b.hdt_dem is null and convert(char(12), h.nid_empr) + ' ' + convert(char(10), hdt_laudo, 103) in ");
            //strSQL.Append("    (select convert(char(12), nid_empr) + ' ' + convert(char(10), max(hdt_laudo), 103) ");
            //strSQL.Append("       from tbllaudo_tec ");

            //strSQL.Append("       group by nid_empr ");
            //strSQL.Append("    ) ");
            //strSQL.Append("  ) ");
            //strSQL.Append("  as tx33 ");
            //strSQL.Append("  ) as tx90 ");
            //strSQL.Append("  left join tblEMPREGADO_APTIDAO_PLANEJAMENTO as t1 on(t1.nid_empr = tx90.nid_empr and(id1 = t1.nid_aptidao or id2 = t1.nid_aptidao or id3 = t1.nid_aptidao or id4 = t1.nid_aptidao or id5 = t1.nid_aptidao or id6 = t1.nid_aptidao or id7 = t1.nid_aptidao or id8 = t1.nid_aptidao or id9 = t1.nid_aptidao)) ");
            //strSQL.Append("  left join opsa.dbo.examedicionario as t80 on(t1.IdExameDicionario = t80.IdExameDicionario) ");

            //strSQL.Append("  left join ( ");
            //strSQL.Append("  select nId_Empregado, tno_func_empr as TFuncao from tblEmpregado_Funcao as EF left join tblFUNCAO as TF on(EF.nID_FUNCAO = TF.nID_FUNCAO) ");
            //strSQL.Append("  WHERE EF.nID_EMPREGADO = nID_EMPREGADO ");
            //strSQL.Append("  and convert(char(12), nId_Empregado) + ' ' + convert(char(10), hdt_Inicio, 103) in ");
            //strSQL.Append("  (select  convert(char(12), nId_Empregado) + ' ' + convert(char(10), max(hdt_Inicio), 103) from tblEmpregado_Funcao group by nid_Empregado ) ");
            //strSQL.Append("  ) as TFUncao on(tx90.nid_Empregado = tFUncao.nID_EMPREGADO) ");


            //strSQL.Append("  where t80.Nome is not null and tx90.nid_empr = " + xIdCliente.ToString() + " " );
            //strSQL.Append("  order by Empresa, Colaborador, t80.Nome ");

            //m_ds = new DataSet("Result");

            using (SqlConnection cnn = new SqlConnection(Ilitera.Data.SQLServer.EntitySQLServer.GetConnection_Sied()))
            {
                cnn.Open();

                rCommand.Connection = cnn;
                SqlDataAdapter da;

                da = new SqlDataAdapter(rCommand);
                da.SelectCommand.CommandTimeout = 900;
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




        public DataSet Gerar_DS_Relatorio_PCD(int xIdCliente, string xD_Inicial, string xCliente, string zEmp, string xFiltro, string xD_Final, string xSit)
        {
            //string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection();
            StringBuilder strSQL = new StringBuilder();

            //DataSet m_ds;


            DataTable table = new DataTable("Result");
            table.Columns.Add("Colaborador", Type.GetType("System.String"));
            table.Columns.Add("GHE", Type.GetType("System.String"));
            table.Columns.Add("Setor", Type.GetType("System.String"));
            table.Columns.Add("Funcao", Type.GetType("System.String"));
            table.Columns.Add("tSexo", Type.GetType("System.String"));
            table.Columns.Add("Admissao", Type.GetType("System.String"));
            table.Columns.Add("Nascimento", Type.GetType("System.String"));
            table.Columns.Add("Idade", Type.GetType("System.String"));
            table.Columns.Add("DataInicial", Type.GetType("System.String"));
            table.Columns.Add("DataVolta", Type.GetType("System.String"));
            table.Columns.Add("DataPrevista", Type.GetType("System.String"));
            table.Columns.Add("Deficiencia", Type.GetType("System.String"));
            table.Columns.Add("CPF", Type.GetType("System.String"));
            table.Columns.Add("DataAtualizacao", Type.GetType("System.String"));

            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);



            strSQL.Append(" select Colaborador, CPF, GHE, Setor, Funcao, tsexo, Admissao, Nascimento, idade, case when DataInicial is not null then 'Afastado desde ' + convert( char(10), DataInicial, 103 )      when hdt_dem is not null then 'Inativo'   when hdt_dem is null then 'Ativo'   end as DataInicial, ");
            strSQL.Append(" case when nInd_Beneficiario = 1 then 'Beneficiário Reabilitado' when nInd_Beneficiario=2 then 'Portador de Deficiência Habilitada'   when nInd_Beneficiario=0 then 'Não aplicável' end as Deficiencia, ");
            strSQL.Append("NomeAbreviado as DataVolta, convert( char(10), getdate(), 103 ) as DataPrevista, DataAtualizacao ");
            strSQL.Append(" from ");
            strSQL.Append(" ( ");
            strSQL.Append(" select tno_empg as Colaborador, tNO_CPF as CPF,   ");
            strSQL.Append(" case   ");
            strSQL.Append("    when max( tno_Func ) is NULL then 'SEM GHE' ");
            strSQL.Append("    else  max( tno_Func ) ");
            strSQL.Append("    end ");
            strSQL.Append(" as GHE, tNO_STR_EMPR as  Setor, tNO_FUNC_Empr as Funcao, tsexo, Admissao,  ");
            strSQL.Append(" Nascimento, Idade, nId_Empregado, nIND_BENEFICIARIO, hdt_dem, NomeAbreviado, DataAtualizacao     ");
            strSQL.Append(" from ");
            strSQL.Append(" ( ");
            strSQL.Append("    select distinct a.tno_empg, tNO_CPF, tno_func, e.tno_str_empr, f.tno_func_empr, tsexo, convert( char(10), hdt_adm, 103 ) as Admissao, convert( char(10),hDT_NASC,103) as Nascimento,  ");
            strSQL.Append(" datediff(yyyy, hdt_nasc, getdate()) as Idade, a.nId_Empregado, a.nInd_Beneficiario, hdt_dem, NomeAbreviado ");
            strSQL.Append(" , (select top 1 CONVERT(char(10), Data_Atualizacao, 103) from tblEMPREGADO_BENEFICIARIO_LOG as V where V.nID_EMPREGADO = a.nID_EMPREGADO and V.nInd_Beneficiario = a.nIND_BENEFICIARIO order by Data_Atualizacao desc ) as DataAtualizacao ");
            strSQL.Append("    from tblempregado as a  ");

            strSQL.Append(" left join ");
            strSQL.Append("   ( ");
            strSQL.Append("     select * from tblEMPREGADO_FUNCAO ");
            strSQL.Append("     where CONVERT(char(14), nid_Empregado) + ' ' + CONVERT(char(10), hdt_inicio, 103) in ");
            strSQL.Append("     ( select CONVERT(char(14), nid_Empregado) + ' ' + CONVERT(char(10), max(hdt_inicio), 103) ");
            strSQL.Append("       from tblEMPREGADO_FUNCAO ");
            strSQL.Append("       where nid_empr in ( " + xFiltro + " ) ");
            strSQL.Append("       group by nID_EMPREGADO ");
            strSQL.Append("       ) ");
            strSQL.Append("      )  as b     on(a.nid_empregado = b.nid_empregado) ");

            strSQL.Append("    left join tblfunc_empregado as c ");
            strSQL.Append("    on ( b.nid_empregado_funcao = c.nid_empregado_funcao ) ");
            strSQL.Append("    left join tblfunc as d ");
            strSQL.Append("    on ( c.nid_func = d.nid_func  ) ");
            strSQL.Append("    left join tblSetor as e ");
            strSQL.Append("    on ( b.nid_setor = e.nid_setor ) ");
            strSQL.Append("    left join tblfuncao as f ");
            strSQL.Append("    on ( b.nid_funcao = f.nid_funcao )   ");
            strSQL.Append("    left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.pessoa as t on ( b.nid_empr = t.idpessoa ) ");
            strSQL.Append("    where ( b.nid_empr in ( " + xFiltro + " )  ");
            //strSQL.Append("    and a.nid_empr =  " + xIdCliente.ToString() + "  ");
            //strSQL.Append("    and a.hdt_dem is null   " );
            strSQL.Append("  and nIND_BENEFICIARIO in ( 1,2 )   ) ");

            if (xSit == "A")
                strSQL.Append("  and hdt_Dem is null ");
            else if (xSit == "I")
                strSQL.Append("  and hdt_Dem is not null ");

            strSQL.Append(" and (  hdt_Adm <= convert( smalldatetime,'" + xD_Final + "', 103 )  ) "); //and hdt_dem >= convert( smalldatetime, '" + xD_Inicial + "', 103 )  ) ");


            strSQL.Append(" ) ");
            strSQL.Append(" as tx1 ");
            strSQL.Append(" group by tno_empg, tNO_CPF, tNO_STR_EMPR, tNO_FUNC_Empr, tsexo, Admissao, Nascimento, Idade , nId_Empregado, nIND_BENEFICIARIO, hdt_dem, NomeAbreviado, DataAtualizacao ");

            strSQL.Append(" ) ");
            strSQL.Append(" as tx99 left join  ");
            strSQL.Append(" (  ");
            strSQL.Append("   select idEmpregado, DataInicial, DataVolta, DataPrevista ");
            strSQL.Append("   from " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.afastamento where ");
            strSQL.Append("   convert( char(15), idempregado ) + convert( char(10), DataInicial ) in ");
            strSQL.Append("   ( ");
            strSQL.Append("     select convert( char(15), idempregado ) + convert( char(10), max(DataInicial) )  ");
            strSQL.Append("     from " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.afastamento ");
            strSQL.Append("     group by idempregado ");
            strSQL.Append("   ) ");
            strSQL.Append("   and DataInicial < getdate() and ( DataVolta >= getdate() or DataPrevista >= getdate() ) ");
            strSQL.Append("  ) as tx100 ");
            strSQL.Append(" on  tx99.nID_EMPREGADO = tx100.IdEmpregado ");
            strSQL.Append(" order by DataVolta, Colaborador ");

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

            return m_ds;

        }




        //public DataSet Gerar_DS_Relatorio_PCD(int xIdCliente, string xD_Inicial, string xCliente, string zEmp, string xFiltro, string xD_Final, string xSit)
        //{
        //    //string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection();
        //    StringBuilder strSQL = new StringBuilder();

        //    //DataSet m_ds;


        //    DataTable table = new DataTable("Result");
        //    table.Columns.Add("Colaborador", Type.GetType("System.String"));
        //    table.Columns.Add("GHE", Type.GetType("System.String"));
        //    table.Columns.Add("Setor", Type.GetType("System.String"));
        //    table.Columns.Add("Funcao", Type.GetType("System.String"));
        //    table.Columns.Add("tSexo", Type.GetType("System.String"));
        //    table.Columns.Add("Admissao", Type.GetType("System.String"));
        //    table.Columns.Add("Nascimento", Type.GetType("System.String"));
        //    table.Columns.Add("Idade", Type.GetType("System.String"));
        //    table.Columns.Add("DataInicial", Type.GetType("System.String"));
        //    table.Columns.Add("DataVolta", Type.GetType("System.String"));
        //    table.Columns.Add("DataPrevista", Type.GetType("System.String"));
        //    table.Columns.Add("Deficiencia", Type.GetType("System.String"));
        //    table.Columns.Add("CPF", Type.GetType("System.String"));
        //    table.Columns.Add("DataAtualizacao", Type.GetType("System.String"));

        //    DataSet m_ds = new DataSet();
        //    m_ds.Tables.Add(table);



        //    strSQL.Append(" select Colaborador, CPF, GHE, Setor, Funcao, tsexo, Admissao, Nascimento, idade, case when DataInicial is not null then 'Afastado desde ' + convert( char(10), DataInicial, 103 )      when hdt_dem is not null then 'Inativo'   when hdt_dem is null then 'Ativo'   end as DataInicial, ");
        //    strSQL.Append(" case when nInd_Beneficiario = 1 then 'Beneficiário Reabilitado' when nInd_Beneficiario=2 then 'Portador de Deficiência Habilitada'   when nInd_Beneficiario=0 then 'Não aplicável' end as Deficiencia, ");
        //    strSQL.Append("NomeAbreviado as DataVolta, convert( char(10), getdate(), 103 ) as DataPrevista, DataAtualizacao ");
        //    strSQL.Append(" from ");
        //    strSQL.Append(" ( ");
        //    strSQL.Append(" select tno_empg as Colaborador, tNO_CPF as CPF,   ");
        //    strSQL.Append(" case   ");
        //    strSQL.Append("    when max( tno_Func ) is NULL then 'SEM GHE' ");
        //    strSQL.Append("    else  max( tno_Func ) ");
        //    strSQL.Append("    end ");
        //    strSQL.Append(" as GHE, tNO_STR_EMPR as  Setor, tNO_FUNC_Empr as Funcao, tsexo, Admissao,  ");
        //    strSQL.Append(" Nascimento, Idade, nId_Empregado, nIND_BENEFICIARIO, hdt_dem, NomeAbreviado, DataAtualizacao     ");
        //    strSQL.Append(" from ");
        //    strSQL.Append(" ( ");
        //    strSQL.Append("    select distinct a.tno_empg, tNO_CPF, tno_func, e.tno_str_empr, f.tno_func_empr, tsexo, convert( char(10), hdt_adm, 103 ) as Admissao, convert( char(10),hDT_NASC,103) as Nascimento,  ");
        //    strSQL.Append(" datediff(yyyy, hdt_nasc, getdate()) as Idade, a.nId_Empregado, a.nInd_Beneficiario, hdt_dem, NomeAbreviado ");
        //    strSQL.Append(" , (select top 1 CONVERT(char(10), Data_Atualizacao, 103) from tblEMPREGADO_BENEFICIARIO_LOG as V where V.nID_EMPREGADO = a.nID_EMPREGADO and V.nInd_Beneficiario = a.nIND_BENEFICIARIO order by Data_Atualizacao desc ) as DataAtualizacao ");
        //    strSQL.Append("    from tblempregado as a left join tblempregado_funcao as b ");
        //    strSQL.Append("    on ( a.nid_empregado = b.nid_empregado ) ");
        //    strSQL.Append("    left join tblfunc_empregado as c ");
        //    strSQL.Append("    on ( b.nid_empregado_funcao = c.nid_empregado_funcao and c.nid_laud_tec in ( select  ");
        //    strSQL.Append(" top 1 nID_LAUD_TEC from tblLAUDO_TEC where nid_empr = b.nid_empr order by hDT_LAUDO desc ) ) ");
        //    strSQL.Append("    left join tblfunc as d ");
        //    strSQL.Append("    on ( c.nid_func = d.nid_func and d.nid_laud_tec in ( select top 1 nID_LAUD_TEC from  ");
        //    strSQL.Append(" tblLAUDO_TEC where nid_empr =  b.nid_empr  order by hDT_LAUDO desc ) ) ");
        //    strSQL.Append("    left join tblSetor as e ");
        //    strSQL.Append("    on ( b.nid_setor = e.nid_setor ) ");
        //    strSQL.Append("    left join tblfuncao as f ");
        //    strSQL.Append("    on ( b.nid_funcao = f.nid_funcao )   ");
        //    strSQL.Append("    left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.pessoa as t on ( b.nid_empr = t.idpessoa ) ");
        //    strSQL.Append("    where ( b.nid_empr in ( " + xFiltro + " )  ");
        //    //strSQL.Append("    and a.nid_empr =  " + xIdCliente.ToString() + "  ");
        //    //strSQL.Append("    and a.hdt_dem is null   " );
        //    strSQL.Append("  and nIND_BENEFICIARIO in ( 1,2 )   ) ");

        //    if (xSit == "A")
        //        strSQL.Append("  and hdt_Dem is null ");
        //    else if (xSit == "I")
        //        strSQL.Append("  and hdt_Dem is not null ");

        //    strSQL.Append(" and (  hdt_Adm <= convert( smalldatetime,'" + xD_Final + "', 103 )  ) "); //and hdt_dem >= convert( smalldatetime, '" + xD_Inicial + "', 103 )  ) ");


        //    strSQL.Append(" ) ");
        //    strSQL.Append(" as tx1 ");
        //    strSQL.Append(" group by tno_empg, tNO_CPF, tNO_STR_EMPR, tNO_FUNC_Empr, tsexo, Admissao, Nascimento, Idade , nId_Empregado, nIND_BENEFICIARIO, hdt_dem, NomeAbreviado, DataAtualizacao ");

        //    strSQL.Append(" union ");

        //    strSQL.Append(" select tno_empg as Colaborador, tNO_CPF as CPF,  ");
        //    strSQL.Append(" case   ");
        //    strSQL.Append("    when min( tno_Func ) is NULL then 'SEM GHE' ");
        //    strSQL.Append("    else min( tno_Func ) ");
        //    strSQL.Append("    end ");
        //    strSQL.Append(" as GHE, tNO_STR_EMPR as  Setor, tNO_FUNC_Empr as Funcao , tsexo, Admissao,  ");
        //    strSQL.Append(" Nascimento, Idade, nId_Empregado, nInd_Beneficiario, hdt_dem, NomeAbreviado, DataAtualizacao  ");
        //    strSQL.Append(" from ");
        //    strSQL.Append(" ( ");
        //    strSQL.Append("    select distinct a.tno_empg, tNO_CPF, tno_func, e.tno_str_empr, f.tno_func_empr, tsexo, convert( char(10), hdt_adm, 103 ) as Admissao, convert( char(10),hDT_NASC,103) as Nascimento,  ");
        //    strSQL.Append(" datediff(yyyy, hdt_nasc, getdate()) as Idade, a.nId_Empregado, a.nInd_Beneficiario, hdt_dem, NomeAbreviado ");
        //    strSQL.Append(" , (select top 1 CONVERT(char(10), Data_Atualizacao, 103) from tblEMPREGADO_BENEFICIARIO_LOG as V where V.nID_EMPREGADO = a.nID_EMPREGADO and V.nInd_Beneficiario = a.nIND_BENEFICIARIO order by Data_Atualizacao desc ) as DataAtualizacao ");
        //    strSQL.Append("    from tblempregado as a left join tblempregado_funcao as b ");
        //    strSQL.Append("    on ( a.nid_empregado = b.nid_empregado ) ");
        //    strSQL.Append("    left join tblfunc_empregado as c ");
        //    strSQL.Append("    on ( b.nid_empregado_funcao = c.nid_empregado_funcao and c.nid_laud_tec in ( select  ");
        //    strSQL.Append(" top 1 nID_LAUD_TEC from tblLAUDO_TEC where nid_empr = b.nid_empr  order by hDT_LAUDO desc ) ) ");
        //    strSQL.Append("    left join tblfunc as d ");
        //    strSQL.Append("    on ( c.nid_func = d.nid_func and d.nid_laud_tec in ( select top 1 nID_LAUD_TEC from  ");
        //    strSQL.Append(" tblLAUDO_TEC where nid_empr = b.nid_empr  order by hDT_LAUDO desc ) ) ");
        //    strSQL.Append("    left join tblSetor as e ");
        //    strSQL.Append("    on ( b.nid_setor = e.nid_setor ) ");
        //    strSQL.Append("    left join tblfuncao as f ");
        //    strSQL.Append("    on ( b.nid_funcao = f.nid_funcao )   ");
        //    strSQL.Append("    left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.pessoa as t on ( b.nid_empr = t.idpessoa ) ");
        //    strSQL.Append("    where ( b.nid_empr in ( " + xFiltro + " ) ");
        //    //strSQL.Append("        and a.nid_empr =  " + xIdCliente.ToString() + "  ");
        //    //strSQL.Append("        and a.hdt_dem is null   " );
        //    strSQL.Append("  and nIND_BENEFICIARIO in ( 1,2 )   ) ");

        //    if (xSit == "A")
        //        strSQL.Append("  and hdt_Dem is null ");
        //    else if (xSit == "I")
        //        strSQL.Append("  and hdt_Dem is not null ");


        //    strSQL.Append(" and (  hdt_Adm <= convert( smalldatetime,'" + xD_Final + "', 103 )  ) "); //and hdt_dem >= convert( smalldatetime, '" + xD_Inicial + "', 103 )  ) ");


        //    strSQL.Append(" ) ");
        //    strSQL.Append(" as tx1 ");
        //    strSQL.Append(" group by tno_empg, tNO_CPF, tNO_STR_EMPR , tNO_FUNC_Empr, tsexo, Admissao, Nascimento, Idade , nId_Empregado, nIND_BENEFICIARIO, hdt_dem, NomeAbreviado, DataAtualizacao ");
        //    strSQL.Append(" ) ");
        //    strSQL.Append(" as tx99 left join  ");
        //    strSQL.Append(" (  ");
        //    strSQL.Append("   select idEmpregado, DataInicial, DataVolta, DataPrevista ");
        //    strSQL.Append("   from " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.afastamento where ");
        //    strSQL.Append("   convert( char(15), idempregado ) + convert( char(10), DataInicial ) in ");
        //    strSQL.Append("   ( ");
        //    strSQL.Append("     select convert( char(15), idempregado ) + convert( char(10), max(DataInicial) )  ");
        //    strSQL.Append("     from " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.afastamento ");
        //    strSQL.Append("     group by idempregado ");
        //    strSQL.Append("   ) ");
        //    strSQL.Append("   and DataInicial < getdate() and ( DataVolta >= getdate() or DataPrevista >= getdate() ) ");
        //    strSQL.Append("  ) as tx100 ");
        //    strSQL.Append(" on  tx99.nID_EMPREGADO = tx100.IdEmpregado ");
        //    strSQL.Append(" order by DataVolta, Colaborador ");

        //    //m_ds = new DataSet("Result");

        //    using (SqlConnection cnn = new SqlConnection(Ilitera.Data.SQLServer.EntitySQLServer.GetConnection_Sied()))
        //    {
        //        SqlDataAdapter da;
        //        cnn.Open();


        //        da = new SqlDataAdapter(strSQL.ToString(), cnn);
        //        da.Fill(m_ds, "Result");

        //        cnn.Close();

        //        da.Dispose();
        //    }

        //    return m_ds;

        //}


        //public DataSet Gerar_DS_Relatorio_Absenteismo(int xIdCliente, string xD_Inicial, string xD_Final, string xTipoRel, string xConsiderar, Int16 xEmp, string xStatus)
        //{
        //    //string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection();
        //    StringBuilder strSQL = new StringBuilder();

        //    //DataSet m_ds;


        //    DataTable table = new DataTable("Result");
        //    table.Columns.Add("nId_Empregado", Type.GetType("System.Int32"));            
        //    table.Columns.Add("Empregado", Type.GetType("System.String"));            
        //    table.Columns.Add("Setor", Type.GetType("System.String"));
        //    table.Columns.Add("Funcao", Type.GetType("System.String"));            
        //    table.Columns.Add("Admissao", Type.GetType("System.String"));
        //    table.Columns.Add("Nascimento", Type.GetType("System.String"));
        //    table.Columns.Add("Afastamento", Type.GetType("System.String"));
        //    table.Columns.Add("Retorno", Type.GetType("System.String"));
        //    table.Columns.Add("Tipo_Afastamento", Type.GetType("System.String"));
        //    table.Columns.Add("Cid1", Type.GetType("System.String"));
        //    table.Columns.Add("Cid2", Type.GetType("System.String"));
        //    table.Columns.Add("Cid3", Type.GetType("System.String"));
        //    table.Columns.Add("Cid4", Type.GetType("System.String"));
        //    table.Columns.Add("NumeroCat", Type.GetType("System.String"));
        //    table.Columns.Add("Data1", Type.GetType("System.String"));
        //    table.Columns.Add("Data2", Type.GetType("System.String"));
        //    table.Columns.Add("Registros", Type.GetType("System.Int16"));
        //    table.Columns.Add("Exame", Type.GetType("System.String"));
        //    table.Columns.Add("Tipo", Type.GetType("System.String"));

        //    DataSet m_ds = new DataSet();
        //    m_ds.Tables.Add(table);




        //    strSQL.Append(" select b.nId_Empregado, b.tNo_Empg as Empregado, r.Nomeabreviado + '  ' + r.NomeCodigo as CNPJ, f.NomeFuncao as Funcao, e.tNo_Str_Empr as Setor, 'Nascimento: ' + convert( char(10), b.hDt_Nasc, 103 ) as Nascimento,  ");
        //    strSQL.Append("        'Admissão: ' + convert( char(10), b.hDt_Adm, 103 ) as Admissao, 'Afastamento: ' + convert( char(10), a.DataInicial, 103 ) as Afastamento, 'Retorno: ' + convert( char(10), a.DataVolta, 103 ) as Retorno,  ");
        //    strSQL.Append(" 	   case   ");
        //    strSQL.Append(" 	   when( a.IndTipoAfastamento = 1 ) then 'Ocupacional'  ");
        //    strSQL.Append(" 	   when( a.IndTipoAfastamento <> 1 ) then 'Não-Ocupacional'  ");
        //    strSQL.Append(" 	   end as Tipo_Afastamento,  g.Descricao as Cid1,  h.Descricao as Cid2, i.Descricao as Cid3, j.Descricao as Cid4, m.NumeroCat, '" + xD_Inicial.Substring(0, 10) + "' as Data1, '" + xD_Final.Substring(0, 10) + "' as Data2, ");
        //    strSQL.Append(" 	   case " );
        //    strSQL.Append(" 	     when s.Descricao is null and a.Obs <> ''      then  'Obs: ' + a.Obs " );
        //    strSQL.Append(" 	     when s.Descricao is not null and a.Obs <> ''  then  'Motivo: ' + s.Descricao + '  Obs: ' + a.Obs " );
        //    strSQL.Append(" 	     when s.Descricao is not null and a.Obs = ''   then  'Motivo: ' + s.Descricao   " );
        //    strSQL.Append(" 	   end  as Tipo ");
        //    strSQL.Append(" from afastamento as a  ");
        //    strSQL.Append(" left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblempregado as b  ");
        //    strSQL.Append(" on ( a.idEmpregado = b.nId_Empregado )  ");
        //    strSQL.Append(" left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEmpregado_Funcao as c  ");
        //    strSQL.Append(" on ( b.nId_Empregado = c.nId_Empregado   ");

        //    if (xEmp == 1)
        //    {
        //        strSQL.Append(" and c.nId_Empr = " + xIdCliente.ToString() + " ) ");
        //    }
        //    else if (xEmp == 2)
        //    {
        //        strSQL.Append(" and c.nId_Empr in (  select idpessoa from juridica where idjuridica = " + xIdCliente.ToString() + " or idjuridicapai = " + xIdCliente.ToString() + " )  ) ");
        //    }
        //    else if (xEmp == 3)
        //    {
        //        strSQL.Append(" and c.nId_Empr in (  select idpessoa from juridica where idgrupoempresa in ( select idgrupoempresa from juridica where idpessoa = " + xIdCliente.ToString() + " ) )  ) ");
        //    }


        //    strSQL.Append(" join (   ");
        //    strSQL.Append("    select convert( char(15), nId_Empregado ) + convert( char(10),max( hDt_Inicio ),103 ) as Chave  ");
        //    strSQL.Append("    from  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblempregado_Funcao  ");

        //    if (xEmp == 1)
        //    {
        //        strSQL.Append(" where nId_Empr = " + xIdCliente.ToString() + " ");
        //    }
        //    else if (xEmp == 2)
        //    {
        //        strSQL.Append(" where nId_Empr in (  select idpessoa from juridica where idjuridica = " + xIdCliente.ToString() + " or idjuridicapai = " + xIdCliente.ToString() + " ) ");
        //    }
        //    else if (xEmp == 3)
        //    {
        //        strSQL.Append(" where nId_Empr in (  select idpessoa from juridica where idgrupoempresa in ( select idgrupoempresa from juridica where idpessoa = " + xIdCliente.ToString() + " ) ) ");
        //    }

        //    strSQL.Append("    group by nId_Empregado  ");
        //    strSQL.Append(" ) as tx80 on tx80.Chave = convert( char(15), c.nId_Empregado ) + convert( char(10),c.hDt_Inicio,103 )  ");
        //    strSQL.Append(" left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblSetor as e  ");
        //    strSQL.Append(" on ( c.nId_Setor = e.nId_Setor )  ");
        //    strSQL.Append(" left join Funcao as f  ");
        //    strSQL.Append(" on ( c.nID_FUNCAO = f.IdFuncao )  ");
        //    strSQL.Append(" left join Cid as g  ");
        //    strSQL.Append(" on ( a.IdCid = g.IdCid )  ");
        //    strSQL.Append(" left join Cid as h  ");
        //    strSQL.Append(" on ( a.IdCid2 = h.IdCid )  ");
        //    strSQL.Append(" left join Cid as i  ");
        //    strSQL.Append(" on ( a.IdCid3 = i.IdCid )  ");
        //    strSQL.Append(" left join Cid as j  ");
        //    strSQL.Append(" on ( a.IdCid4 = j.IdCid )  ");
        //    strSQL.Append(" left join acidente as k  ");
        //    strSQL.Append(" on ( a.idAcidente = k.IdAcidente )  ");
        //    strSQL.Append(" left join cat as m  ");
        //    strSQL.Append(" on ( k.IdCat = m.IdCat )  ");
        //    strSQL.Append(" left join pessoa as r  ");
        //    strSQL.Append(" on ( c.nId_Empr = r.IdPessoa )  ");
        //    strSQL.Append(" left join afastamentoTipo as s " );
        //    strSQL.Append(" on ( a.IdAfastamentoTipo = s.IdAfastamentoTipo ) ");

        //    if (xEmp == 1)
        //    {
        //        strSQL.Append(" where c.nId_Empr = " + xIdCliente.ToString() + " ");
        //    }
        //    else if (xEmp == 2)
        //    {
        //        strSQL.Append(" where c.nId_Empr in (  select idpessoa from juridica where idjuridica = " + xIdCliente.ToString() + " or idjuridicapai = " + xIdCliente.ToString() + " ) ");
        //    }
        //    else if (xEmp == 3)
        //    {
        //        strSQL.Append(" where c.nId_Empr in (  select idpessoa from juridica where idgrupoempresa in ( select idgrupoempresa from juridica where idpessoa = " + xIdCliente.ToString() + " ) ) ");
        //    }


        //    strSQL.Append(" and a.DataInicial between  convert( smalldatetime, '" + xD_Inicial + "', 103 ) and convert( smalldatetime, '" + xD_Final + "', 103 ) and b.tno_Empg is not null ");

        //    if (xConsiderar == "1")
        //    {
        //        strSQL.Append(" and a.DataVolta is null ");
        //    }
        //    else if (xConsiderar == "2")
        //    {
        //        strSQL.Append(" and a.DataVolta is not null ");
        //    }


        //    if (xStatus == "1")
        //    {
        //        strSQL.Append(" and b.hDt_Dem is null ");
        //    }
        //    else if (xStatus == "2")
        //    {
        //        strSQL.Append(" and b.hDt_Dem is not null ");
        //    }


        //    strSQL.Append(" order by b.tNo_Empg, a.DataInicial  ");




        //    //m_ds = new DataSet("Result");

        //    using (SqlConnection cnn = new SqlConnection(Ilitera.Data.SQLServer.EntitySQLServer.GetConnection()))
        //    {
        //        SqlDataAdapter da;
        //        cnn.Open();


        //        da = new SqlDataAdapter(strSQL.ToString(), cnn);
        //        da.Fill(m_ds, "Result");

        //        cnn.Close();

        //        da.Dispose();
        //    }

        //    return m_ds;

        //}



        public DataSet Gerar_DS_Relatorio_Absenteismo(int xIdCliente, string xD_Inicial, string xD_Final, string xTipoRel, string xConsiderar, Int16 xEmp, string xStatus, string xAtestados)
        {
            //string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection();
            StringBuilder strSQL = new StringBuilder();

            //DataSet m_ds;


            DataTable table = new DataTable("Result");
            table.Columns.Add("nId_Empregado", Type.GetType("System.Int32"));
            table.Columns.Add("Empregado", Type.GetType("System.String"));
            table.Columns.Add("Setor", Type.GetType("System.String"));
            table.Columns.Add("Funcao", Type.GetType("System.String"));
            table.Columns.Add("Admissao", Type.GetType("System.String"));
            table.Columns.Add("Nascimento", Type.GetType("System.String"));
            table.Columns.Add("Afastamento", Type.GetType("System.String"));
            table.Columns.Add("Retorno", Type.GetType("System.String"));
            table.Columns.Add("Tipo_Afastamento", Type.GetType("System.String"));
            table.Columns.Add("Cid1", Type.GetType("System.String"));
            table.Columns.Add("Cid2", Type.GetType("System.String"));
            table.Columns.Add("Cid3", Type.GetType("System.String"));
            table.Columns.Add("Cid4", Type.GetType("System.String"));
            table.Columns.Add("NumeroCat", Type.GetType("System.String"));
            table.Columns.Add("Data1", Type.GetType("System.String"));
            table.Columns.Add("Data2", Type.GetType("System.String"));
            table.Columns.Add("Registros", Type.GetType("System.Int16"));
            table.Columns.Add("Exame", Type.GetType("System.String"));
            table.Columns.Add("Tipo", Type.GetType("System.String"));
            table.Columns.Add("Horas_Diarias", Type.GetType("System.Double"));
            table.Columns.Add("Trab_Sabado", Type.GetType("System.String"));
            table.Columns.Add("Data_Afastamento", Type.GetType("System.String"));
            table.Columns.Add("Hora_Afastamento", Type.GetType("System.String"));
            table.Columns.Add("Resultado", Type.GetType("System.String"));
            table.Columns.Add("IdAfastamento", Type.GetType("System.String"));
            table.Columns.Add("Observacao", Type.GetType("System.String"));
            table.Columns.Add("Emitente_Atestado", Type.GetType("System.String"));
            

            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);




            strSQL.Append(" select b.nId_Empregado, b.tNo_Empg as Empregado,  case when b.tno_CPF is null then '-'  when b.tno_CPF = '' then '-'  else 'CPF ' + b.tno_CPF end as Tipo_Afastamento, r.Nomeabreviado + '  ' + r.NomeCodigo as CNPJ, f.NomeFuncao as Funcao, e.tNo_Str_Empr as Setor, 'Nascimento: ' + convert( char(10), b.hDt_Nasc, 103 ) as Nascimento,  ");
            strSQL.Append("        'Admissão: ' + convert( char(10), b.hDt_Adm, 103 ) as Admissao, 'Afastamento: ' + convert( char(10), a.DataInicial, 103 ) + ' '  + convert( char(8), a.DataInicial, 14 )  as Afastamento, 'Retorno: ' + convert( char(10), a.DataVolta, 103 ) + ' ' + convert( char(8), a.DataVolta, 14 ) as Retorno,  ");
            strSQL.Append(" 	   case   ");
            strSQL.Append(" 	   when( a.IndTipoAfastamento = 1 ) then 'Ocupacional'  ");
            strSQL.Append(" 	   when( a.IndTipoAfastamento <> 1 ) then 'Não-Ocupacional'  ");
            strSQL.Append(" 	     when s.Descricao is null and a.Obs <> ''      then  'Obs: ' + a.Obs ");
            strSQL.Append(" 	     when s.Descricao is not null and a.Obs <> ''  then  'Motivo: ' + s.Descricao + '  Obs: ' + a.Obs ");
            strSQL.Append(" 	     when s.Descricao is not null and a.Obs = ''   then  'Motivo: ' + s.Descricao   ");
            strSQL.Append(" 	   end  as Tipo, g.Descricao as Cid1,  h.Descricao as Cid2, i.Descricao as Cid3, j.Descricao as Cid4,");
            strSQL.Append(" 	   case when( t.nId_Tempo_Exp is null ) then  8 ");
            strSQL.Append(" 	        when( t.nId_Tempo_Exp is not null ) then  svl_hor_num ");
            strSQL.Append(" 	    end ");
            strSQL.Append(" 	    as Horas_Diarias, ");
            strSQL.Append(" 	    case when( t.nId_Tempo_Exp is null ) then  'N' ");
            strSQL.Append(" 	         when ( charindex('/Sab',t.tHora_Extenso ) > 0 ) then 'S' ");
            strSQL.Append("              when ( charindex('/Sab',t.tHora_Extenso ) < 0 ) then 'N' ");
            strSQL.Append(" 	    end ");
            strSQL.Append(" 	    as Trab_Sabado, convert( char(10), a.DataInicial, 103 ) as Data_Afastamento, convert( char(8), a.DataInicial, 14 ) as Hora_Afastamento, ");
            strSQL.Append(" 	    case when Atestado_Emitente is not null and Atestado_Emitente <> '' then 'Atestado: ' + Atestado_Emitente   + ' ' + case when Atestado_ideOC=1 then 'CRM ' when Atestado_ideOC=2 then 'CRO ' when Atestado_ideOC=3 then 'RMS '  end  +  isnull(Atestado_nrOC,'-') + '/' + isnull(Atestado_ufOC,'-') else '' end as Resultado, a.IdAfastamento, ");
            strSQL.Append(" 	    case when Atestado is not null and Atestado <> '' then 'Com Anexo Atestado'  else 'Sem Anexo Atestado' end as NumeroCat, '" + xD_Inicial + "' as Data1, '" + xD_Final + "' as Data2, isnull( a.Obs, '' ) as Observacao ");            
            strSQL.Append(" from afastamento as a  ");
            strSQL.Append(" left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblempregado as b  ");
            strSQL.Append(" on ( a.idEmpregado = b.nId_Empregado )  ");
            strSQL.Append(" left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEmpregado_Funcao as c  ");
            strSQL.Append(" on ( b.nId_Empregado = c.nId_Empregado   ");

            if (xEmp == 1)
            {
                strSQL.Append(" and c.nId_Empr = " + xIdCliente.ToString() + " ) ");
            }
            else if (xEmp == 2)
            {
                strSQL.Append(" and c.nId_Empr in (  select idpessoa from juridica where idjuridica = " + xIdCliente.ToString() + " or idjuridicapai = " + xIdCliente.ToString() + " )  ) ");
            }
            else if (xEmp == 3)
            {
                strSQL.Append(" and c.nId_Empr in (  select idpessoa from juridica where idgrupoempresa in ( select idgrupoempresa from juridica where idpessoa = " + xIdCliente.ToString() + " ) )  ) ");
            }
            else if (xEmp == 0)
            {
                strSQL.Append(" ) ");
            }


            strSQL.Append(" join (   ");
            strSQL.Append("    select convert( char(15), nId_Empregado ) + convert( char(10),max( hDt_Inicio ),103 ) as Chave  ");
            strSQL.Append("    from  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblempregado_Funcao  ");

            if (xEmp == 1)
            {
                strSQL.Append(" where nId_Empr = " + xIdCliente.ToString() + " ");
            }
            else if (xEmp == 2)
            {
                strSQL.Append(" where nId_Empr in (  select idpessoa from juridica where idjuridica = " + xIdCliente.ToString() + " or idjuridicapai = " + xIdCliente.ToString() + " ) ");
            }
            else if (xEmp == 3)
            {
                strSQL.Append(" where nId_Empr in (  select idpessoa from juridica where idgrupoempresa in ( select idgrupoempresa from juridica where idpessoa = " + xIdCliente.ToString() + " ) ) ");
            }
            else if (xEmp == 0)
            {
                strSQL.Append("  ");
            }

            strSQL.Append("    group by nId_Empregado  ");
            strSQL.Append(" ) as tx80 on tx80.Chave = convert( char(15), c.nId_Empregado ) + convert( char(10),c.hDt_Inicio,103 )  ");
            strSQL.Append(" left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblSetor as e  ");
            strSQL.Append(" on ( c.nId_Setor = e.nId_Setor )  ");
            strSQL.Append(" left join Funcao as f  ");
            strSQL.Append(" on ( c.nID_FUNCAO = f.IdFuncao )  ");
            strSQL.Append(" left join Cid as g  ");
            strSQL.Append(" on ( a.IdCid = g.IdCid )  ");
            strSQL.Append(" left join Cid as h  ");
            strSQL.Append(" on ( a.IdCid2 = h.IdCid )  ");
            strSQL.Append(" left join Cid as i  ");
            strSQL.Append(" on ( a.IdCid3 = i.IdCid )  ");
            strSQL.Append(" left join Cid as j  ");
            strSQL.Append(" on ( a.IdCid4 = j.IdCid )  ");
            strSQL.Append(" left join acidente as k  ");
            strSQL.Append(" on ( a.idAcidente = k.IdAcidente )  ");
            strSQL.Append(" left join cat as m  ");
            strSQL.Append(" on ( k.IdCat = m.IdCat )  ");
            strSQL.Append(" left join pessoa as r  ");
            strSQL.Append(" on ( c.nId_Empr = r.IdPessoa )  ");
            strSQL.Append(" left join afastamentoTipo as s ");
            strSQL.Append(" on ( a.IdAfastamentoTipo = s.IdAfastamentoTipo ) ");
            strSQL.Append(" left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tbltempo_exp as t ");
            strSQL.Append(" on ( c.nID_TEMPO_EXP = t.nid_tempo_exp ) ");

            if (xEmp == 1)
            {
                strSQL.Append(" where c.nId_Empr = " + xIdCliente.ToString() + " ");
            }
            else if (xEmp == 2)
            {
                strSQL.Append(" where c.nId_Empr in (  select idpessoa from juridica where idjuridica = " + xIdCliente.ToString() + " or idjuridicapai = " + xIdCliente.ToString() + " ) ");
            }
            else if (xEmp == 3)
            {
                strSQL.Append(" where c.nId_Empr in (  select idpessoa from juridica where idgrupoempresa in ( select idgrupoempresa from juridica where idpessoa = " + xIdCliente.ToString() + " ) ) ");
            }
            else if (xEmp == 0)
            {
                strSQL.Append(" where c.nId_Empr is not null  ");
            }

            strSQL.Append(" and a.DataInicial between  convert( smalldatetime, '" + xD_Inicial + "', 103 ) and convert( smalldatetime, '" + xD_Final + "', 103 ) and b.tno_Empg is not null ");

            if (xConsiderar == "1")
            {
                strSQL.Append(" and a.DataVolta is null ");
            }
            else if (xConsiderar == "2")
            {
                strSQL.Append(" and a.DataVolta is not null ");
            }


            if (xStatus == "1")
            {
                strSQL.Append(" and b.hDt_Dem is null ");
            }
            else if (xStatus == "2")
            {
                strSQL.Append(" and b.hDt_Dem is not null ");
            }

            if (xAtestados == "2")
            {
                strSQL.Append(" and Atestado <> '' ");
            }
            else if (xAtestados == "3")
            {
                strSQL.Append(" and Atestado = '' ");
            }



            strSQL.Append(" order by b.tNo_Empg, a.DataInicial  ");




            //m_ds = new DataSet("Result");

            using (SqlConnection cnn = new SqlConnection(Ilitera.Data.SQLServer.EntitySQLServer.GetConnection()))
            {
                SqlDataAdapter da;
                cnn.Open();


                da = new SqlDataAdapter(strSQL.ToString(), cnn);
                da.SelectCommand.CommandTimeout = 900;
                da.Fill(m_ds, "Result");

                cnn.Close();

                da.Dispose();
            }

            return m_ds;

        }



        public DataSet Gerar_DS_Relatorio_Absenteismo_Acidente(int xIdCliente, string xD_Inicial, string xD_Final, string xTipoRel, string xConsiderar, Int16 xEmp, string xStatus, string xAtestados)
        {
            //string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection();
            StringBuilder strSQL = new StringBuilder();

            //DataSet m_ds;


            DataTable table = new DataTable("Result");
            table.Columns.Add("nId_Empregado", Type.GetType("System.Int32"));
            table.Columns.Add("Empregado", Type.GetType("System.String"));
            table.Columns.Add("Setor", Type.GetType("System.String"));
            table.Columns.Add("Funcao", Type.GetType("System.String"));
            table.Columns.Add("Admissao", Type.GetType("System.String"));
            table.Columns.Add("Nascimento", Type.GetType("System.String"));
            table.Columns.Add("Afastamento", Type.GetType("System.String"));
            table.Columns.Add("Retorno", Type.GetType("System.String"));
            table.Columns.Add("Tipo_Afastamento", Type.GetType("System.String"));
            table.Columns.Add("CNPJ", Type.GetType("System.String"));
            table.Columns.Add("Cid1", Type.GetType("System.String"));
            table.Columns.Add("Cid2", Type.GetType("System.String"));
            table.Columns.Add("Cid3", Type.GetType("System.String"));
            table.Columns.Add("Cid4", Type.GetType("System.String"));
            table.Columns.Add("NumeroCat", Type.GetType("System.String"));
            table.Columns.Add("Data1", Type.GetType("System.String"));
            table.Columns.Add("Data2", Type.GetType("System.String"));
            table.Columns.Add("Registros", Type.GetType("System.Int16"));
            table.Columns.Add("Exame", Type.GetType("System.String"));
            table.Columns.Add("Tipo", Type.GetType("System.String"));
            table.Columns.Add("Horas_Diarias", Type.GetType("System.Double"));
            table.Columns.Add("Trab_Sabado", Type.GetType("System.String"));
            table.Columns.Add("Data_Afastamento", Type.GetType("System.String"));
            table.Columns.Add("Hora_Afastamento", Type.GetType("System.String"));
            table.Columns.Add("Resultado", Type.GetType("System.String"));
            table.Columns.Add("IdAfastamento", Type.GetType("System.String"));
            table.Columns.Add("Observacao", Type.GetType("System.String"));
            table.Columns.Add("DataAcidente", Type.GetType("System.String"));
            table.Columns.Add("Descr_Acidente", Type.GetType("System.String"));
            table.Columns.Add("Parte_Corpo", Type.GetType("System.String"));
            table.Columns.Add("Tipo_Local", Type.GetType("System.String"));
            table.Columns.Add("Descricao_Local", Type.GetType("System.String"));
            table.Columns.Add("Especificacao_Local", Type.GetType("System.String"));


            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);




            strSQL.Append(" select b.nId_Empregado, b.tNo_Empg as Empregado,  case when b.tno_CPF is null then '-'  when b.tno_CPF = '' then '-'  else b.tno_CPF end as Tipo_Afastamento, r.Nomeabreviado + '  ' + r.NomeCodigo as CNPJ, f.NomeFuncao as Funcao, e.tNo_Str_Empr as Setor, convert( char(10), b.hDt_Nasc, 103 ) as Nascimento,  ");
            strSQL.Append("        convert( char(10), b.hDt_Adm, 103 ) as Admissao, convert( char(10), a.DataInicial, 103 ) + ' '  + convert( char(8), a.DataInicial, 14 )  as Afastamento, convert( char(10), a.DataVolta, 103 ) + ' ' + convert( char(8), a.DataVolta, 14 ) as Retorno,  ");
            strSQL.Append(" 	   case   ");
            strSQL.Append(" 	   when( a.IndTipoAfastamento = 1 ) then 'Ocupacional'  ");
            strSQL.Append(" 	   when( a.IndTipoAfastamento <> 1 ) then 'Não-Ocupacional'  ");
            strSQL.Append(" 	     when s.Descricao is null and a.Obs <> ''      then  'Obs: ' + a.Obs ");
            strSQL.Append(" 	     when s.Descricao is not null and a.Obs <> ''  then  'Motivo: ' + s.Descricao + '  Obs: ' + a.Obs ");
            strSQL.Append(" 	     when s.Descricao is not null and a.Obs = ''   then  'Motivo: ' + s.Descricao   ");
            strSQL.Append(" 	   end  as Tipo, g.Descricao as Cid1,  h.Descricao as Cid2, i.Descricao as Cid3, j.Descricao as Cid4,");
            strSQL.Append(" 	   case when( t.nId_Tempo_Exp is null ) then  8 ");
            strSQL.Append(" 	        when( t.nId_Tempo_Exp is not null ) then  svl_hor_num ");
            strSQL.Append(" 	    end ");
            strSQL.Append(" 	    as Horas_Diarias, ");
            strSQL.Append(" 	    case when( t.nId_Tempo_Exp is null ) then  'N' ");
            strSQL.Append(" 	         when ( charindex('/Sab',t.tHora_Extenso ) > 0 ) then 'S' ");
            strSQL.Append("              when ( charindex('/Sab',t.tHora_Extenso ) < 0 ) then 'N' ");
            strSQL.Append(" 	    end ");
            strSQL.Append(" 	    as Trab_Sabado, convert( char(10), a.DataInicial, 103 ) as Data_Afastamento, convert( char(8), a.DataInicial, 14 ) as Hora_Afastamento, ");
            strSQL.Append(" 	    case when Atestado_Emitente is not null and Atestado_Emitente <> '' then 'Atestado: ' + Atestado_Emitente   + ' ' + case when Atestado_ideOC=1 then 'CRM ' when Atestado_ideOC=2 then 'CRO ' when Atestado_ideOC=3 then 'RMS '  end  +  isnull(Atestado_nrOC,'-') + '/' + isnull(Atestado_ufOC,'-') else '' end as Resultado, a.IdAfastamento, ");
            strSQL.Append(" 	    case when Atestado is not null and Atestado <> '' then 'Com Anexo Atestado'  else 'Sem Anexo Atestado' end as NumeroCat, '" + xD_Inicial + "' as Data1, '" + xD_Final + "' as Data2, isnull( a.Obs, '' ) as Observacao, ");
            strSQL.Append(" 	    convert(char(10), ac.DataAcidente, 103) as DataAcidente, ac.Descricao as Descr_Acidente, pc.Descricao as Parte_Corpo, ");
            strSQL.Append(" 	    case when ac.idtipolocal = 1 then 'Estabelecimento do Empregador no Brasil' when ac.idTipoLocal = 2 then 'Estabelecimento do Empregador no Exterior' when ac.idtipoLocal = 3 then 'Empresa onde o empregador presta serviço' ");
            strSQL.Append(" 	    when ac.idtipolocal = 4 then 'Via Pública'   when ac.idtipolocal = 5  then 'Área Rural'  when ac.idtipolocal = 6 then 'Embarcação' when ac.idtipolocal is null then '' else 'Outros' end as Tipo_Local, ");
            strSQL.Append(" 	    ac.dscLocal as Descricao_Local, ac.especLocal as Especificacao_Local ");
            strSQL.Append(" from afastamento as a  ");
            strSQL.Append(" left join acidente as ac on(a.idacidente = ac.idacidente) ");
            strSQL.Append(" left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tbl_eSocial_13_Parte_Corpo_Atingida as pc on(ac.Codigo_Parte_Corpo_Atingida = pc.Codigo) ");
            strSQL.Append(" left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblempregado as b  ");
            strSQL.Append(" on ( a.idEmpregado = b.nId_Empregado )  ");
            strSQL.Append(" left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEmpregado_Funcao as c  ");
            strSQL.Append(" on ( b.nId_Empregado = c.nId_Empregado   ");

            if (xEmp == 1)
            {
                strSQL.Append(" and c.nId_Empr = " + xIdCliente.ToString() + " ) ");
            }
            else if (xEmp == 2)
            {
                strSQL.Append(" and c.nId_Empr in (  select idpessoa from juridica where idjuridica = " + xIdCliente.ToString() + " or idjuridicapai = " + xIdCliente.ToString() + " )  ) ");
            }
            else if (xEmp == 3)
            {
                strSQL.Append(" and c.nId_Empr in (  select idpessoa from juridica where idgrupoempresa in ( select idgrupoempresa from juridica where idpessoa = " + xIdCliente.ToString() + " ) )  ) ");
            }
            else if (xEmp == 0)
            {
                strSQL.Append(" ) ");
            }


            strSQL.Append(" join (   ");
            strSQL.Append("    select convert( char(15), nId_Empregado ) + convert( char(10),max( hDt_Inicio ),103 ) as Chave  ");
            strSQL.Append("    from  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblempregado_Funcao  ");

            if (xEmp == 1)
            {
                strSQL.Append(" where nId_Empr = " + xIdCliente.ToString() + " ");
            }
            else if (xEmp == 2)
            {
                strSQL.Append(" where nId_Empr in (  select idpessoa from juridica where idjuridica = " + xIdCliente.ToString() + " or idjuridicapai = " + xIdCliente.ToString() + " ) ");
            }
            else if (xEmp == 3)
            {
                strSQL.Append(" where nId_Empr in (  select idpessoa from juridica where idgrupoempresa in ( select idgrupoempresa from juridica where idpessoa = " + xIdCliente.ToString() + " ) ) ");
            }
            else if (xEmp == 0)
            {
                strSQL.Append("  ");
            }

            strSQL.Append("    group by nId_Empregado  ");
            strSQL.Append(" ) as tx80 on tx80.Chave = convert( char(15), c.nId_Empregado ) + convert( char(10),c.hDt_Inicio,103 )  ");
            strSQL.Append(" left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblSetor as e  ");
            strSQL.Append(" on ( c.nId_Setor = e.nId_Setor )  ");
            strSQL.Append(" left join Funcao as f  ");
            strSQL.Append(" on ( c.nID_FUNCAO = f.IdFuncao )  ");
            strSQL.Append(" left join Cid as g  ");
            strSQL.Append(" on ( a.IdCid = g.IdCid )  ");
            strSQL.Append(" left join Cid as h  ");
            strSQL.Append(" on ( a.IdCid2 = h.IdCid )  ");
            strSQL.Append(" left join Cid as i  ");
            strSQL.Append(" on ( a.IdCid3 = i.IdCid )  ");
            strSQL.Append(" left join Cid as j  ");
            strSQL.Append(" on ( a.IdCid4 = j.IdCid )  ");
            strSQL.Append(" left join acidente as k  ");
            strSQL.Append(" on ( a.idAcidente = k.IdAcidente )  ");
            strSQL.Append(" left join cat as m  ");
            strSQL.Append(" on ( k.IdCat = m.IdCat )  ");
            strSQL.Append(" left join pessoa as r  ");
            strSQL.Append(" on ( c.nId_Empr = r.IdPessoa )  ");
            strSQL.Append(" left join afastamentoTipo as s ");
            strSQL.Append(" on ( a.IdAfastamentoTipo = s.IdAfastamentoTipo ) ");
            strSQL.Append(" left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tbltempo_exp as t ");
            strSQL.Append(" on ( c.nID_TEMPO_EXP = t.nid_tempo_exp ) ");

            if (xEmp == 1)
            {
                strSQL.Append(" where c.nId_Empr = " + xIdCliente.ToString() + " ");
            }
            else if (xEmp == 2)
            {
                strSQL.Append(" where c.nId_Empr in (  select idpessoa from juridica where idjuridica = " + xIdCliente.ToString() + " or idjuridicapai = " + xIdCliente.ToString() + " ) ");
            }
            else if (xEmp == 3)
            {
                strSQL.Append(" where c.nId_Empr in (  select idpessoa from juridica where idgrupoempresa in ( select idgrupoempresa from juridica where idpessoa = " + xIdCliente.ToString() + " ) ) ");
            }
            else if (xEmp == 0)
            {
                strSQL.Append(" where c.nId_Empr is not null  ");
            }

            strSQL.Append(" and a.DataInicial between  convert( smalldatetime, '" + xD_Inicial + "', 103 ) and convert( smalldatetime, '" + xD_Final + "', 103 ) and b.tno_Empg is not null ");

            if (xConsiderar == "1")
            {
                strSQL.Append(" and a.DataVolta is null ");
            }
            else if (xConsiderar == "2")
            {
                strSQL.Append(" and a.DataVolta is not null ");
            }


            if (xStatus == "1")
            {
                strSQL.Append(" and b.hDt_Dem is null ");
            }
            else if (xStatus == "2")
            {
                strSQL.Append(" and b.hDt_Dem is not null ");
            }

            if (xAtestados == "2")
            {
                strSQL.Append(" and Atestado <> '' ");
            }
            else if (xAtestados == "3")
            {
                strSQL.Append(" and Atestado = '' ");
            }



            strSQL.Append(" order by b.tNo_Empg, a.DataInicial  ");




            //m_ds = new DataSet("Result");

            using (SqlConnection cnn = new SqlConnection(Ilitera.Data.SQLServer.EntitySQLServer.GetConnection()))
            {
                SqlDataAdapter da;
                cnn.Open();


                da = new SqlDataAdapter(strSQL.ToString(), cnn);
                da.SelectCommand.CommandTimeout = 900;
                da.Fill(m_ds, "Result");

                cnn.Close();

                da.Dispose();
            }

            return m_ds;

        }




        public DataSet Gerar_DS_Relatorio_Absenteismo_Sumarizado(int xIdCliente, string xD_Inicial, string xD_Final, string xTipoRel, string xConsiderar, Int16 xEmp, string xStatus)
        {
            //string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection();
            StringBuilder strSQL = new StringBuilder();

            //DataSet m_ds;


            DataTable table = new DataTable("Result");
            table.Columns.Add("Empregado", Type.GetType("System.String"));
            table.Columns.Add("Setor", Type.GetType("System.String"));
            table.Columns.Add("Funcao", Type.GetType("System.String"));
            table.Columns.Add("Admissao", Type.GetType("System.String"));
            table.Columns.Add("Nascimento", Type.GetType("System.String"));
            table.Columns.Add("Afastamento", Type.GetType("System.String"));
            table.Columns.Add("Retorno", Type.GetType("System.String"));
            table.Columns.Add("Tipo_Afastamento", Type.GetType("System.String"));
            table.Columns.Add("Cid1", Type.GetType("System.String"));
            table.Columns.Add("Cid2", Type.GetType("System.String"));
            table.Columns.Add("Cid3", Type.GetType("System.String"));
            table.Columns.Add("Cid4", Type.GetType("System.String"));
            table.Columns.Add("NumeroCat", Type.GetType("System.String"));
            table.Columns.Add("Data1", Type.GetType("System.String"));
            table.Columns.Add("Data2", Type.GetType("System.String"));
            table.Columns.Add("Registros", Type.GetType("System.Int16"));
            table.Columns.Add("Exame", Type.GetType("System.String"));

            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);


            strSQL.Append(" select CNPJ, case  when CID is null then '-' else CID end as cid1, Setor, Funcao, count(*) as Registros, '" + xD_Inicial.Substring(0, 10) + "' as Data1, '" + xD_Final.Substring(0, 10) + "' as Data2   ");
            strSQL.Append(" from ( ");


            strSQL.Append("select CNPJ, cid1 as CID, setor, funcao ");
            strSQL.Append(" from ");
            strSQL.Append(" ( ");
            strSQL.Append(" select b.tNo_Empg as Empregado, r.Nomeabreviado + '  ' + r.NomeCodigo as CNPJ, f.NomeFuncao as Funcao, e.tNo_Str_Empr as Setor, 'Nascimento: ' + convert( char(10), b.hDt_Nasc, 103 ) as Nascimento,  ");
            strSQL.Append("        'Admissão: ' + convert( char(10), b.hDt_Adm, 103 ) as Admissao, 'Afastamento: ' + convert( char(10), a.DataInicial, 103 ) as Afastamento, 'Retorno: ' + convert( char(10), a.DataVolta, 103 ) as Retorno,  ");
            strSQL.Append(" 	   case   ");
            strSQL.Append(" 	   when( a.IndTipoAfastamento = 1 ) then 'Ocupacional'  ");
            strSQL.Append(" 	   when( a.IndTipoAfastamento <> 1 ) then 'Não-Ocupacional'  ");
            strSQL.Append(" 	   end as Tipo_Afastamento,  g.Descricao as Cid1,  h.Descricao as Cid2, i.Descricao as Cid3, j.Descricao as Cid4, m.NumeroCat, '" + xD_Inicial.Substring(0, 10) + "' as Data1, '" + xD_Final.Substring(0, 10) + "' as Data2   ");
            strSQL.Append(" from afastamento as a  ");
            strSQL.Append(" left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblempregado as b  ");
            strSQL.Append(" on ( a.idEmpregado = b.nId_Empregado )  ");
            strSQL.Append(" left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEmpregado_Funcao as c  ");
            strSQL.Append(" on ( b.nId_Empregado = c.nId_Empregado   ");

            if (xEmp == 1)
            {
                strSQL.Append(" and c.nId_Empr = " + xIdCliente.ToString() + " ) ");
            }
            else if (xEmp == 2)
            {
                strSQL.Append(" and c.nId_Empr in (  select idpessoa from juridica where idjuridica = " + xIdCliente.ToString() + " or idjuridicapai = " + xIdCliente.ToString() + " )  ) ");
            }
            else if (xEmp == 3)
            {
                strSQL.Append(" and c.nId_Empr in (  select idpessoa from juridica where idgrupoempresa in ( select idgrupoempresa from juridica where idpessoa = " + xIdCliente.ToString() + " ) )  ) ");
            }


            strSQL.Append(" join (   ");
            strSQL.Append("    select convert( char(15), nId_Empregado ) + convert( char(10),max( hDt_Inicio ),103 ) as Chave  ");
            strSQL.Append("    from  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblempregado_Funcao  ");

            if (xEmp == 1)
            {
                strSQL.Append(" where nId_Empr = " + xIdCliente.ToString() + " ");
            }
            else if (xEmp == 2)
            {
                strSQL.Append(" where nId_Empr in (  select idpessoa from juridica where idjuridica = " + xIdCliente.ToString() + " or idjuridicapai = " + xIdCliente.ToString() + " ) ");
            }
            else if (xEmp == 3)
            {
                strSQL.Append(" where nId_Empr in (  select idpessoa from juridica where idgrupoempresa in ( select idgrupoempresa from juridica where idpessoa = " + xIdCliente.ToString() + " ) ) ");
            }

            strSQL.Append("    group by nId_Empregado  ");
            strSQL.Append(" ) as tx80 on tx80.Chave = convert( char(15), c.nId_Empregado ) + convert( char(10),c.hDt_Inicio,103 )  ");
            strSQL.Append(" left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblSetor as e  ");
            strSQL.Append(" on ( c.nId_Setor = e.nId_Setor )  ");
            strSQL.Append(" left join Funcao as f  ");
            strSQL.Append(" on ( c.nID_FUNCAO = f.IdFuncao )  ");
            strSQL.Append(" left join Cid as g  ");
            strSQL.Append(" on ( a.IdCid = g.IdCid )  ");
            strSQL.Append(" left join Cid as h  ");
            strSQL.Append(" on ( a.IdCid2 = h.IdCid )  ");
            strSQL.Append(" left join Cid as i  ");
            strSQL.Append(" on ( a.IdCid3 = i.IdCid )  ");
            strSQL.Append(" left join Cid as j  ");
            strSQL.Append(" on ( a.IdCid4 = j.IdCid )  ");
            strSQL.Append(" left join acidente as k  ");
            strSQL.Append(" on ( a.idAcidente = k.IdAcidente )  ");
            strSQL.Append(" left join cat as m  ");
            strSQL.Append(" on ( k.IdCat = m.IdCat )  ");
            strSQL.Append(" left join pessoa as r  ");
            strSQL.Append(" on ( c.nId_Empr = r.IdPessoa )  ");

            if (xEmp == 1)
            {
                strSQL.Append(" where c.nId_Empr = " + xIdCliente.ToString() + " ");
            }
            else if (xEmp == 2)
            {
                strSQL.Append(" where c.nId_Empr in (  select idpessoa from juridica where idjuridica = " + xIdCliente.ToString() + " or idjuridicapai = " + xIdCliente.ToString() + " ) ");
            }
            else if (xEmp == 3)
            {
                strSQL.Append(" where c.nId_Empr in (  select idpessoa from juridica where idgrupoempresa in ( select idgrupoempresa from juridica where idpessoa = " + xIdCliente.ToString() + " ) ) ");
            }

            strSQL.Append(" and a.DataInicial between  convert( smalldatetime, '" + xD_Inicial + "', 103 ) and convert( smalldatetime, '" + xD_Final + "', 103 ) and b.tno_Empg is not null ");

            if (xConsiderar == "1")
            {
                strSQL.Append(" and a.DataVolta is null ");
            }
            else if (xConsiderar == "2")
            {
                strSQL.Append(" and a.DataVolta is not null ");
            }


            if (xStatus == "1")
            {
                strSQL.Append(" and b.hDt_Dem is null ");
            }
            else if (xStatus == "2")
            {
                strSQL.Append(" and b.hDt_Dem is not null ");
            }

            strSQL.Append(" ) as tx99 ");

            strSQL.Append(" union ");



            strSQL.Append("select CNPJ, cid2 as CID, setor, funcao ");
            strSQL.Append(" from ");
            strSQL.Append(" ( ");
            strSQL.Append(" select b.tNo_Empg as Empregado, r.Nomeabreviado + '  ' + r.NomeCodigo as CNPJ, f.NomeFuncao as Funcao, e.tNo_Str_Empr as Setor, 'Nascimento: ' + convert( char(10), b.hDt_Nasc, 103 ) as Nascimento,  ");
            strSQL.Append("        'Admissão: ' + convert( char(10), b.hDt_Adm, 103 ) as Admissao, 'Afastamento: ' + convert( char(10), a.DataInicial, 103 ) as Afastamento, 'Retorno: ' + convert( char(10), a.DataVolta, 103 ) as Retorno,  ");
            strSQL.Append(" 	   case   ");
            strSQL.Append(" 	   when( a.IndTipoAfastamento = 1 ) then 'Ocupacional'  ");
            strSQL.Append(" 	   when( a.IndTipoAfastamento <> 1 ) then 'Não-Ocupacional'  ");
            strSQL.Append(" 	   end as Tipo_Afastamento,  g.Descricao as Cid1,  h.Descricao as Cid2, i.Descricao as Cid3, j.Descricao as Cid4, m.NumeroCat, '" + xD_Inicial.Substring(0, 10) + "' as Data1, '" + xD_Final.Substring(0, 10) + "' as Data2   ");
            strSQL.Append(" from afastamento as a  ");
            strSQL.Append(" left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblempregado as b  ");
            strSQL.Append(" on ( a.idEmpregado = b.nId_Empregado )  ");
            strSQL.Append(" left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEmpregado_Funcao as c  ");
            strSQL.Append(" on ( b.nId_Empregado = c.nId_Empregado   ");

            if (xEmp == 1)
            {
                strSQL.Append(" and c.nId_Empr = " + xIdCliente.ToString() + " ) ");
            }
            else if (xEmp == 2)
            {
                strSQL.Append(" and c.nId_Empr in (  select idpessoa from juridica where idjuridica = " + xIdCliente.ToString() + " or idjuridicapai = " + xIdCliente.ToString() + " )  ) ");
            }
            else if (xEmp == 3)
            {
                strSQL.Append(" and c.nId_Empr in (  select idpessoa from juridica where idgrupoempresa in ( select idgrupoempresa from juridica where idpessoa = " + xIdCliente.ToString() + " ) )  ) ");
            }


            strSQL.Append(" join (   ");
            strSQL.Append("    select convert( char(15), nId_Empregado ) + convert( char(10),max( hDt_Inicio ),103 ) as Chave  ");
            strSQL.Append("    from  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblempregado_Funcao  ");

            if (xEmp == 1)
            {
                strSQL.Append(" where nId_Empr = " + xIdCliente.ToString() + " ");
            }
            else if (xEmp == 2)
            {
                strSQL.Append(" where nId_Empr in (  select idpessoa from juridica where idjuridica = " + xIdCliente.ToString() + " or idjuridicapai = " + xIdCliente.ToString() + " ) ");
            }
            else if (xEmp == 3)
            {
                strSQL.Append(" where nId_Empr in (  select idpessoa from juridica where idgrupoempresa in ( select idgrupoempresa from juridica where idpessoa = " + xIdCliente.ToString() + " ) ) ");
            }

            strSQL.Append("    group by nId_Empregado  ");
            strSQL.Append(" ) as tx80 on tx80.Chave = convert( char(15), c.nId_Empregado ) + convert( char(10),c.hDt_Inicio,103 )  ");
            strSQL.Append(" left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblSetor as e  ");
            strSQL.Append(" on ( c.nId_Setor = e.nId_Setor )  ");
            strSQL.Append(" left join Funcao as f  ");
            strSQL.Append(" on ( c.nID_FUNCAO = f.IdFuncao )  ");
            strSQL.Append(" left join Cid as g  ");
            strSQL.Append(" on ( a.IdCid = g.IdCid )  ");
            strSQL.Append(" left join Cid as h  ");
            strSQL.Append(" on ( a.IdCid2 = h.IdCid )  ");
            strSQL.Append(" left join Cid as i  ");
            strSQL.Append(" on ( a.IdCid3 = i.IdCid )  ");
            strSQL.Append(" left join Cid as j  ");
            strSQL.Append(" on ( a.IdCid4 = j.IdCid )  ");
            strSQL.Append(" left join acidente as k  ");
            strSQL.Append(" on ( a.idAcidente = k.IdAcidente )  ");
            strSQL.Append(" left join cat as m  ");
            strSQL.Append(" on ( k.IdCat = m.IdCat )  ");
            strSQL.Append(" left join pessoa as r  ");
            strSQL.Append(" on ( c.nId_Empr = r.IdPessoa )  ");

            if (xEmp == 1)
            {
                strSQL.Append(" where c.nId_Empr = " + xIdCliente.ToString() + " ");
            }
            else if (xEmp == 2)
            {
                strSQL.Append(" where c.nId_Empr in (  select idpessoa from juridica where idjuridica = " + xIdCliente.ToString() + " or idjuridicapai = " + xIdCliente.ToString() + " ) ");
            }
            else if (xEmp == 3)
            {
                strSQL.Append(" where c.nId_Empr in (  select idpessoa from juridica where idgrupoempresa in ( select idgrupoempresa from juridica where idpessoa = " + xIdCliente.ToString() + " ) ) ");
            }

            strSQL.Append(" and a.DataInicial between  convert( smalldatetime, '" + xD_Inicial + "', 103 ) and convert( smalldatetime, '" + xD_Final + "', 103 ) and b.tno_Empg is not null ");

            if (xConsiderar == "1")
            {
                strSQL.Append(" and a.DataVolta is null ");
            }
            else if (xConsiderar == "2")
            {
                strSQL.Append(" and a.DataVolta is not null ");
            }

            if (xStatus == "1")
            {
                strSQL.Append(" and b.hDt_Dem is null ");
            }
            else if (xStatus == "2")
            {
                strSQL.Append(" and b.hDt_Dem is not null ");
            }


            strSQL.Append(" ) as tx99 ");

            strSQL.Append(" union ");



            strSQL.Append("select CNPJ, cid3 as CID, setor, funcao ");
            strSQL.Append(" from ");
            strSQL.Append(" ( ");
            strSQL.Append(" select b.tNo_Empg as Empregado, r.Nomeabreviado + '  ' + r.NomeCodigo as CNPJ, f.NomeFuncao as Funcao, e.tNo_Str_Empr as Setor, 'Nascimento: ' + convert( char(10), b.hDt_Nasc, 103 ) as Nascimento,  ");
            strSQL.Append("        'Admissão: ' + convert( char(10), b.hDt_Adm, 103 ) as Admissao, 'Afastamento: ' + convert( char(10), a.DataInicial, 103 ) as Afastamento, 'Retorno: ' + convert( char(10), a.DataVolta, 103 ) as Retorno,  ");
            strSQL.Append(" 	   case   ");
            strSQL.Append(" 	   when( a.IndTipoAfastamento = 1 ) then 'Ocupacional'  ");
            strSQL.Append(" 	   when( a.IndTipoAfastamento <> 1 ) then 'Não-Ocupacional'  ");
            strSQL.Append(" 	   end as Tipo_Afastamento,  g.Descricao as Cid1,  h.Descricao as Cid2, i.Descricao as Cid3, j.Descricao as Cid4, m.NumeroCat, '" + xD_Inicial.Substring(0, 10) + "' as Data1, '" + xD_Final.Substring(0, 10) + "' as Data2   ");
            strSQL.Append(" from afastamento as a  ");
            strSQL.Append(" left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblempregado as b  ");
            strSQL.Append(" on ( a.idEmpregado = b.nId_Empregado )  ");
            strSQL.Append(" left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEmpregado_Funcao as c  ");
            strSQL.Append(" on ( b.nId_Empregado = c.nId_Empregado   ");

            if (xEmp == 1)
            {
                strSQL.Append(" and c.nId_Empr = " + xIdCliente.ToString() + " ) ");
            }
            else if (xEmp == 2)
            {
                strSQL.Append(" and c.nId_Empr in (  select idpessoa from juridica where idjuridica = " + xIdCliente.ToString() + " or idjuridicapai = " + xIdCliente.ToString() + " )  ) ");
            }
            else if (xEmp == 3)
            {
                strSQL.Append(" and c.nId_Empr in (  select idpessoa from juridica where idgrupoempresa in ( select idgrupoempresa from juridica where idpessoa = " + xIdCliente.ToString() + " ) )  ) ");
            }


            strSQL.Append(" join (   ");
            strSQL.Append("    select convert( char(15), nId_Empregado ) + convert( char(10),max( hDt_Inicio ),103 ) as Chave  ");
            strSQL.Append("    from  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblempregado_Funcao  ");

            if (xEmp == 1)
            {
                strSQL.Append(" where nId_Empr = " + xIdCliente.ToString() + " ");
            }
            else if (xEmp == 2)
            {
                strSQL.Append(" where nId_Empr in (  select idpessoa from juridica where idjuridica = " + xIdCliente.ToString() + " or idjuridicapai = " + xIdCliente.ToString() + " ) ");
            }
            else if (xEmp == 3)
            {
                strSQL.Append(" where nId_Empr in (  select idpessoa from juridica where idgrupoempresa in ( select idgrupoempresa from juridica where idpessoa = " + xIdCliente.ToString() + " ) ) ");
            }

            strSQL.Append("    group by nId_Empregado  ");
            strSQL.Append(" ) as tx80 on tx80.Chave = convert( char(15), c.nId_Empregado ) + convert( char(10),c.hDt_Inicio,103 )  ");
            strSQL.Append(" left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblSetor as e  ");
            strSQL.Append(" on ( c.nId_Setor = e.nId_Setor )  ");
            strSQL.Append(" left join Funcao as f  ");
            strSQL.Append(" on ( c.nID_FUNCAO = f.IdFuncao )  ");
            strSQL.Append(" left join Cid as g  ");
            strSQL.Append(" on ( a.IdCid = g.IdCid )  ");
            strSQL.Append(" left join Cid as h  ");
            strSQL.Append(" on ( a.IdCid2 = h.IdCid )  ");
            strSQL.Append(" left join Cid as i  ");
            strSQL.Append(" on ( a.IdCid3 = i.IdCid )  ");
            strSQL.Append(" left join Cid as j  ");
            strSQL.Append(" on ( a.IdCid4 = j.IdCid )  ");
            strSQL.Append(" left join acidente as k  ");
            strSQL.Append(" on ( a.idAcidente = k.IdAcidente )  ");
            strSQL.Append(" left join cat as m  ");
            strSQL.Append(" on ( k.IdCat = m.IdCat )  ");
            strSQL.Append(" left join pessoa as r  ");
            strSQL.Append(" on ( c.nId_Empr = r.IdPessoa )  ");

            if (xEmp == 1)
            {
                strSQL.Append(" where c.nId_Empr = " + xIdCliente.ToString() + " ");
            }
            else if (xEmp == 2)
            {
                strSQL.Append(" where c.nId_Empr in (  select idpessoa from juridica where idjuridica = " + xIdCliente.ToString() + " or idjuridicapai = " + xIdCliente.ToString() + " ) ");
            }
            else if (xEmp == 3)
            {
                strSQL.Append(" where c.nId_Empr in (  select idpessoa from juridica where idgrupoempresa in ( select idgrupoempresa from juridica where idpessoa = " + xIdCliente.ToString() + " ) ) ");
            }

            strSQL.Append(" and a.DataInicial between  convert( smalldatetime, '" + xD_Inicial + "', 103 ) and convert( smalldatetime, '" + xD_Final + "', 103 ) and b.tno_Empg is not null ");

            if (xConsiderar == "1")
            {
                strSQL.Append(" and a.DataVolta is null ");
            }
            else if (xConsiderar == "2")
            {
                strSQL.Append(" and a.DataVolta is not null ");
            }

            if (xStatus == "1")
            {
                strSQL.Append(" and b.hDt_Dem is null ");
            }
            else if (xStatus == "2")
            {
                strSQL.Append(" and b.hDt_Dem is not null ");
            }


            strSQL.Append(" ) as tx99 ");

            strSQL.Append(" union ");


            strSQL.Append("select CNPJ, cid4 as CID, setor, funcao ");
            strSQL.Append(" from ");
            strSQL.Append(" ( ");
            strSQL.Append(" select b.tNo_Empg as Empregado, r.Nomeabreviado + '  ' + r.NomeCodigo as CNPJ, f.NomeFuncao as Funcao, e.tNo_Str_Empr as Setor, 'Nascimento: ' + convert( char(10), b.hDt_Nasc, 103 ) as Nascimento,  ");
            strSQL.Append("        'Admissão: ' + convert( char(10), b.hDt_Adm, 103 ) as Admissao, 'Afastamento: ' + convert( char(10), a.DataInicial, 103 ) as Afastamento, 'Retorno: ' + convert( char(10), a.DataVolta, 103 ) as Retorno,  ");
            strSQL.Append(" 	   case   ");
            strSQL.Append(" 	   when( a.IndTipoAfastamento = 1 ) then 'Ocupacional'  ");
            strSQL.Append(" 	   when( a.IndTipoAfastamento <> 1 ) then 'Não-Ocupacional'  ");
            strSQL.Append(" 	   end as Tipo_Afastamento,  g.Descricao as Cid1,  h.Descricao as Cid2, i.Descricao as Cid3, j.Descricao as Cid4, m.NumeroCat, '" + xD_Inicial.Substring(0, 10) + "' as Data1, '" + xD_Final.Substring(0, 10) + "' as Data2   ");
            strSQL.Append(" from afastamento as a  ");
            strSQL.Append(" left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblempregado as b  ");
            strSQL.Append(" on ( a.idEmpregado = b.nId_Empregado )  ");
            strSQL.Append(" left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEmpregado_Funcao as c  ");
            strSQL.Append(" on ( b.nId_Empregado = c.nId_Empregado   ");

            if (xEmp == 1)
            {
                strSQL.Append(" and c.nId_Empr = " + xIdCliente.ToString() + " ) ");
            }
            else if (xEmp == 2)
            {
                strSQL.Append(" and c.nId_Empr in (  select idpessoa from juridica where idjuridica = " + xIdCliente.ToString() + " or idjuridicapai = " + xIdCliente.ToString() + " )  ) ");
            }
            else if (xEmp == 3)
            {
                strSQL.Append(" and c.nId_Empr in (  select idpessoa from juridica where idgrupoempresa in ( select idgrupoempresa from juridica where idpessoa = " + xIdCliente.ToString() + " ) )  ) ");
            }


            strSQL.Append(" join (   ");
            strSQL.Append("    select convert( char(15), nId_Empregado ) + convert( char(10),max( hDt_Inicio ),103 ) as Chave  ");
            strSQL.Append("    from  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblempregado_Funcao  ");

            if (xEmp == 1)
            {
                strSQL.Append(" where nId_Empr = " + xIdCliente.ToString() + " ");
            }
            else if (xEmp == 2)
            {
                strSQL.Append(" where nId_Empr in (  select idpessoa from juridica where idjuridica = " + xIdCliente.ToString() + " or idjuridicapai = " + xIdCliente.ToString() + " ) ");
            }
            else if (xEmp == 3)
            {
                strSQL.Append(" where nId_Empr in (  select idpessoa from juridica where idgrupoempresa in ( select idgrupoempresa from juridica where idpessoa = " + xIdCliente.ToString() + " ) ) ");
            }

            strSQL.Append("    group by nId_Empregado  ");
            strSQL.Append(" ) as tx80 on tx80.Chave = convert( char(15), c.nId_Empregado ) + convert( char(10),c.hDt_Inicio,103 )  ");
            strSQL.Append(" left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblSetor as e  ");
            strSQL.Append(" on ( c.nId_Setor = e.nId_Setor )  ");
            strSQL.Append(" left join Funcao as f  ");
            strSQL.Append(" on ( c.nID_FUNCAO = f.IdFuncao )  ");
            strSQL.Append(" left join Cid as g  ");
            strSQL.Append(" on ( a.IdCid = g.IdCid )  ");
            strSQL.Append(" left join Cid as h  ");
            strSQL.Append(" on ( a.IdCid2 = h.IdCid )  ");
            strSQL.Append(" left join Cid as i  ");
            strSQL.Append(" on ( a.IdCid3 = i.IdCid )  ");
            strSQL.Append(" left join Cid as j  ");
            strSQL.Append(" on ( a.IdCid4 = j.IdCid )  ");
            strSQL.Append(" left join acidente as k  ");
            strSQL.Append(" on ( a.idAcidente = k.IdAcidente )  ");
            strSQL.Append(" left join cat as m  ");
            strSQL.Append(" on ( k.IdCat = m.IdCat )  ");
            strSQL.Append(" left join pessoa as r  ");
            strSQL.Append(" on ( c.nId_Empr = r.IdPessoa )  ");

            if (xEmp == 1)
            {
                strSQL.Append(" where c.nId_Empr = " + xIdCliente.ToString() + " ");
            }
            else if (xEmp == 2)
            {
                strSQL.Append(" where c.nId_Empr in (  select idpessoa from juridica where idjuridica = " + xIdCliente.ToString() + " or idjuridicapai = " + xIdCliente.ToString() + " ) ");
            }
            else if (xEmp == 3)
            {
                strSQL.Append(" where c.nId_Empr in (  select idpessoa from juridica where idgrupoempresa in ( select idgrupoempresa from juridica where idpessoa = " + xIdCliente.ToString() + " ) ) ");
            }

            strSQL.Append(" and a.DataInicial between  convert( smalldatetime, '" + xD_Inicial + "', 103 ) and convert( smalldatetime, '" + xD_Final + "', 103 ) and b.tno_Empg is not null ");

            if (xConsiderar == "1")
            {
                strSQL.Append(" and a.DataVolta is null ");
            }
            else if (xConsiderar == "2")
            {
                strSQL.Append(" and a.DataVolta is not null ");
            }

            if (xStatus == "1")
            {
                strSQL.Append(" and b.hDt_Dem is null ");
            }
            else if (xStatus == "2")
            {
                strSQL.Append(" and b.hDt_Dem is not null ");
            }


            strSQL.Append(" ) as tx99 ");



            strSQL.Append(" union  ");

            strSQL.Append(" select CNPJ, Motivo as CID, setor, funcao  from  (   ");
            strSQL.Append(" 	   select b.tNo_Empg as Empregado, r.Nomeabreviado + '  ' + r.NomeCodigo as CNPJ, f.NomeFuncao as Funcao, e.tNo_Str_Empr as Setor, 'Nascimento: ' + convert( char(10), b.hDt_Nasc, 103 ) as Nascimento,          'Admissão: ' + convert( char(10), b.hDt_Adm, 103 ) as Admissao, 'Afastamento: ' + convert( char(10), a.DataInicial, 103 ) as Afastamento, 'Retorno: ' + convert( char(10), a.DataVolta, 103 ) as Retorno, ");
            strSQL.Append(" 	   case    	   when( a.IndTipoAfastamento = 1 ) then 'Ocupacional'   	   when( a.IndTipoAfastamento <> 1 ) then 'Não-Ocupacional'   	   end as Tipo_Afastamento,  s.Descricao as Motivo,  m.NumeroCat, '01/01/2014' as Data1, '01/12/2015' as Data2  ");
            strSQL.Append(" 	   from afastamento as a    ");
            strSQL.Append(" 	   left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblempregado as b   on ( a.idEmpregado = b.nId_Empregado )    ");
            strSQL.Append(" 	   left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEmpregado_Funcao as c   on ( b.nId_Empregado = c.nId_Empregado    ");

            if (xEmp == 1)
            {
                strSQL.Append(" and c.nId_Empr = " + xIdCliente.ToString() + " ) ");
            }
            else if (xEmp == 2)
            {
                strSQL.Append(" and c.nId_Empr in (  select idpessoa from juridica where idjuridica = " + xIdCliente.ToString() + " or idjuridicapai = " + xIdCliente.ToString() + " ) ) ");
            }
            else if (xEmp == 3)
            {
                strSQL.Append(" and c.nId_Empr in (  select idpessoa from juridica where idgrupoempresa in ( select idgrupoempresa from juridica where idpessoa = " + xIdCliente.ToString() + " ) ) ) ");
            }


            strSQL.Append(" 	   join (  ");
            strSQL.Append(" 	     select convert( char(15), nId_Empregado ) + convert( char(10),max( hDt_Inicio ),103 ) as Chave  ");
            strSQL.Append(" 	     from  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblempregado_Funcao    ");

            if (xEmp == 1)
            {
                strSQL.Append(" where nId_Empr = " + xIdCliente.ToString() + " ");
            }
            else if (xEmp == 2)
            {
                strSQL.Append(" where nId_Empr in (  select idpessoa from juridica where idjuridica = " + xIdCliente.ToString() + " or idjuridicapai = " + xIdCliente.ToString() + " ) ");
            }
            else if (xEmp == 3)
            {
                strSQL.Append(" where nId_Empr in (  select idpessoa from juridica where idgrupoempresa in ( select idgrupoempresa from juridica where idpessoa = " + xIdCliente.ToString() + " ) ) ");
            }



            strSQL.Append(" 		 group by nId_Empregado  ");
            strSQL.Append(" 		 ) as tx80 on tx80.Chave = convert( char(15), c.nId_Empregado ) + convert( char(10),c.hDt_Inicio,103 )    ");
            strSQL.Append(" 	   left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblSetor as e   on ( c.nId_Setor = e.nId_Setor )    ");
            strSQL.Append(" 	   left join Funcao as f   on ( c.nID_FUNCAO = f.IdFuncao )    ");
            strSQL.Append(" 	   left join acidente as k   on ( a.idAcidente = k.IdAcidente )    ");
            strSQL.Append(" 	   left join cat as m   on ( k.IdCat = m.IdCat )    ");
            strSQL.Append(" 	   left join pessoa as r   on ( c.nId_Empr = r.IdPessoa )    ");
            strSQL.Append(" 	   left join afastamentoTipo as s on ( a.IdAfastamentoTipo = s.IdAfastamentoTipo ) ");

            if (xEmp == 1)
            {
                strSQL.Append(" where c.nId_Empr = " + xIdCliente.ToString() + " ");
            }
            else if (xEmp == 2)
            {
                strSQL.Append(" where c.nId_Empr in (  select idpessoa from juridica where idjuridica = " + xIdCliente.ToString() + " or idjuridicapai = " + xIdCliente.ToString() + " ) ");
            }
            else if (xEmp == 3)
            {
                strSQL.Append(" where c.nId_Empr in (  select idpessoa from juridica where idgrupoempresa in ( select idgrupoempresa from juridica where idpessoa = " + xIdCliente.ToString() + " ) ) ");
            }

            strSQL.Append(" and a.DataInicial between  convert( smalldatetime, '" + xD_Inicial + "', 103 ) and convert( smalldatetime, '" + xD_Final + "', 103 ) and b.tno_Empg is not null and s.Descricao is not null ");

            if (xConsiderar == "1")
            {
                strSQL.Append(" and a.DataVolta is null ");
            }
            else if (xConsiderar == "2")
            {
                strSQL.Append(" and a.DataVolta is not null ");
            }

            if (xStatus == "1")
            {
                strSQL.Append(" and b.hDt_Dem is null ");
            }
            else if (xStatus == "2")
            {
                strSQL.Append(" and b.hDt_Dem is not null ");
            }

            strSQL.Append(" ) as tx69 ");

            strSQL.Append(" ) as tx100 ");
            //strSQL.Append(" where CID is not null " );
            strSQL.Append(" group by CNPJ, CID,setor, funcao ");
            strSQL.Append(" order by CNPJ, CID,setor, funcao ");



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




        public DataSet Gerar_DS_Relatorio_Ambulatoriais(int xIdCliente, string xD_Inicial, string xD_Final, string xTipoRel, string xConsiderar, Int16 xEmp)
        {
            //string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection();
            StringBuilder strSQL = new StringBuilder();

            //DataSet m_ds;


            DataTable table = new DataTable("Result");
            table.Columns.Add("Empregado", Type.GetType("System.String"));
            table.Columns.Add("Setor", Type.GetType("System.String"));
            table.Columns.Add("Funcao", Type.GetType("System.String"));
            table.Columns.Add("Admissao", Type.GetType("System.String"));
            table.Columns.Add("Nascimento", Type.GetType("System.String"));
            table.Columns.Add("Afastamento", Type.GetType("System.String"));
            table.Columns.Add("Retorno", Type.GetType("System.String"));
            table.Columns.Add("Tipo_Afastamento", Type.GetType("System.String"));
            table.Columns.Add("Cid1", Type.GetType("System.String"));
            table.Columns.Add("Cid2", Type.GetType("System.String"));
            table.Columns.Add("Cid3", Type.GetType("System.String"));
            table.Columns.Add("Cid4", Type.GetType("System.String"));
            table.Columns.Add("NumeroCat", Type.GetType("System.String"));
            table.Columns.Add("Data1", Type.GetType("System.String"));
            table.Columns.Add("Data2", Type.GetType("System.String"));
            table.Columns.Add("Registros", Type.GetType("System.Int16"));

            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);



            strSQL.Append(" select b.tNo_Empg as Empregado, r.Nomeabreviado + '  ' + r.NomeCodigo as CNPJ,  convert( char(10), d.DataExame, 103 ) as Retorno,  ");
            strSQL.Append(" case   ");
            strSQL.Append("   when a.Tipo='E' then 'Enfermagem'  ");
            strSQL.Append("   when a.Tipo='M' then 'Médico'  ");
            strSQL.Append("   when a.Tipo='O' then 'Outros'  ");
            strSQL.Append(" end as Tipo_Afastamento, e.NomeQueixa as Funcao, f.NomeProcedimento as Setor, '" + xD_Inicial.Substring(0, 10) + "' as Data1, '" + xD_Final.Substring(0, 10) + "' as Data2   ");
            strSQL.Append(" from cliniconaoocupacional as a   ");
            strSQL.Append(" join examebase as d on ( a.idexamebase = d.idexamebase )  ");
            strSQL.Append(" left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblempregado as b  ");
            strSQL.Append(" on ( d.idEmpregado = b.nId_Empregado )  ");
            strSQL.Append(" left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEmpregado_Funcao as c  ");
            strSQL.Append(" on ( b.nId_Empregado = c.nId_Empregado   ");

            if (xEmp == 1)
            {
                strSQL.Append(" and c.nId_Empr = " + xIdCliente.ToString() + " ) ");
            }
            else if (xEmp == 2)
            {
                strSQL.Append(" and c.nId_Empr in (  select idpessoa from juridica where idjuridica = " + xIdCliente.ToString() + " or idjuridicapai = " + xIdCliente.ToString() + " )  ) ");
            }
            else if (xEmp == 3)
            {
                strSQL.Append(" and c.nId_Empr in (  select idpessoa from juridica where idgrupoempresa in ( select idgrupoempresa from juridica where idpessoa = " + xIdCliente.ToString() + " ) )  ) ");
            }


            strSQL.Append(" join (   ");
            strSQL.Append("    select convert( char(15), nId_Empregado ) + convert( char(10),max( hDt_Inicio ),103 ) as Chave  ");
            strSQL.Append("    from  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblempregado_Funcao  ");

            if (xEmp == 1)
            {
                strSQL.Append(" where nId_Empr = " + xIdCliente.ToString() + " ");
            }
            else if (xEmp == 2)
            {
                strSQL.Append(" where nId_Empr in (  select idpessoa from juridica where idjuridica = " + xIdCliente.ToString() + " or idjuridicapai = " + xIdCliente.ToString() + " ) ");
            }
            else if (xEmp == 3)
            {
                strSQL.Append(" where nId_Empr in (  select idpessoa from juridica where idgrupoempresa in ( select idgrupoempresa from juridica where idpessoa = " + xIdCliente.ToString() + " ) ) ");
            }

            strSQL.Append("    group by nId_Empregado  ");
            strSQL.Append(" ) as tx80 on tx80.Chave = convert( char(15), c.nId_Empregado ) + convert( char(10),c.hDt_Inicio,103 )  ");


            strSQL.Append(" left join queixaclinica as e  ");
            strSQL.Append(" on ( a.idqueixaclinica = e.IdQueixaClinica )  ");
            strSQL.Append(" left join ProcedimentoClinico as f  ");
            strSQL.Append(" on ( a.idprocedimentoclinico = f.idprocedimentoclinico )  ");
            strSQL.Append(" left join pessoa as r  ");
            strSQL.Append(" on ( c.nId_Empr = r.IdPessoa )  ");


            if (xEmp == 1)
            {
                strSQL.Append(" where c.nId_Empr = " + xIdCliente.ToString() + " ");
            }
            else if (xEmp == 2)
            {
                strSQL.Append(" where c.nId_Empr in (  select idpessoa from juridica where idjuridica = " + xIdCliente.ToString() + " or idjuridicapai = " + xIdCliente.ToString() + " ) ");
            }
            else if (xEmp == 3)
            {
                strSQL.Append(" where c.nId_Empr in (  select idpessoa from juridica where idgrupoempresa in ( select idgrupoempresa from juridica where idpessoa = " + xIdCliente.ToString() + " ) ) ");
            }


            strSQL.Append(" and  d.DataExame between  convert( smalldatetime, '" + xD_Inicial + "', 103 ) and convert( smalldatetime, '" + xD_Final + "', 103 ) and b.tno_Empg is not null ");

            if (xConsiderar == "1")
            {
                strSQL.Append(" and b.hDt_Dem is null ");
            }
            else if (xConsiderar == "2")
            {
                strSQL.Append(" and b.hDt_Dem is not null ");
            }


            strSQL.Append(" order by r.Nomeabreviado + '  ' + r.NomeCodigo, b.tNo_Empg,  d.DataExame  ");


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




        public DataSet Gerar_DS_Relatorio_Vacinas(int xIdCliente, string xD_Inicial, string xD_Final, string xTipoRel, string xConsiderar, Int16 xEmp)
        {
            //string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection();
            StringBuilder strSQL = new StringBuilder();

            //DataSet m_ds;


            DataTable table = new DataTable("Result");
            table.Columns.Add("Empregado", Type.GetType("System.String"));
            table.Columns.Add("Setor", Type.GetType("System.String"));
            table.Columns.Add("Funcao", Type.GetType("System.String"));
            table.Columns.Add("Admissao", Type.GetType("System.String"));
            table.Columns.Add("Nascimento", Type.GetType("System.String"));
            table.Columns.Add("Afastamento", Type.GetType("System.String"));
            table.Columns.Add("Retorno", Type.GetType("System.String"));
            table.Columns.Add("Tipo_Afastamento", Type.GetType("System.String"));
            table.Columns.Add("Cid1", Type.GetType("System.String"));
            table.Columns.Add("Cid2", Type.GetType("System.String"));
            table.Columns.Add("Cid3", Type.GetType("System.String"));
            table.Columns.Add("Cid4", Type.GetType("System.String"));
            table.Columns.Add("NumeroCat", Type.GetType("System.String"));
            table.Columns.Add("Data1", Type.GetType("System.String"));
            table.Columns.Add("Data2", Type.GetType("System.String"));
            table.Columns.Add("Registros", Type.GetType("System.Int16"));

            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);



            strSQL.Append(" select *, '" + xD_Inicial + "' as Data1, '" + xD_Final + "' as Data2 from ");
            strSQL.Append(" ( ");
            strSQL.Append("    select convert( char(10), a.DataVacina, 103 ) as Retorno, b.nId_Empr, a.DataVacina, b.tNO_EMPG as Empregado, c.Dose as Setor, d.VacinaTipo as Funcao, r.Nomeabreviado + '  ' + r.NomeCodigo as CNPJ  ");
            strSQL.Append("    from vacina as a left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblempregado as b ");
            strSQL.Append("    on ( a.idempregado = b.nid_empregado ) ");
            strSQL.Append("    left join Vacina_Dose as c ");
            strSQL.Append("    on ( a.IdVacinaDose = c.IdVacinaDose ) ");
            strSQL.Append("    left join Vacina_Tipo as d ");
            strSQL.Append("    on ( c.IdVacinaTipo = d.IdVacinaTipo ) ");
            strSQL.Append("    left join Pessoa as r ");
            strSQL.Append("    on ( b.nId_Empr = r.IdPessoa ) ");
            strSQL.Append("    union ");
            strSQL.Append("    select  convert( char(10), DataExame, 103 ) as Retorno, b.nId_Empr, DataExame as DataVacina, b.tNo_Empg as Empregado, convert( varchar(100),Prontuario) as Setor, 'Anti-HBS' as Funcao, r.Nomeabreviado + '  ' + r.NomeCodigo as CNPJ ");
            strSQL.Append("    from examebase as a left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblempregado as b ");
            strSQL.Append("    on ( a.IdEmpregado = b.nid_Empregado ) ");
            strSQL.Append("    left join Pessoa as r ");
            strSQL.Append("    on ( b.nId_Empr = r.IdPessoa ) ");
            strSQL.Append("    where idexamedicionario = -1174768070  ");
            strSQL.Append(" ) as tx90 ");

            strSQL.Append(" where  DataVacina between  convert( smalldatetime, '" + xD_Inicial + "', 103 ) and convert( smalldatetime, '" + xD_Final + "', 103 ) and Empregado is not null ");

            if (xEmp == 1)
            {
                strSQL.Append(" and nId_Empr = " + xIdCliente.ToString() + "  ");
            }
            else if (xEmp == 2)
            {
                strSQL.Append(" and nId_Empr in (  select idpessoa from juridica where idjuridica = " + xIdCliente.ToString() + " or idjuridicapai = " + xIdCliente.ToString() + " )   ");
            }
            else if (xEmp == 3)
            {
                strSQL.Append(" and nId_Empr in (  select idpessoa from juridica where idgrupoempresa in ( select idgrupoempresa from juridica where idpessoa = " + xIdCliente.ToString() + " ) )   ");
            }


            if (xConsiderar == "0")
            {
                strSQL.Append(" and Funcao <> 'Anti-HBS' ");
            }


            strSQL.Append(" order by CNPJ, Empregado, DataVacina desc ");




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



        public DataSet Gerar_DS_Relatorio_Vacinas_Convocacao(int xIdCliente, string xTipoRel, string xConsiderar, Int16 xEmp)
        {
            //string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection();
            StringBuilder strSQL = new StringBuilder();

            //DataSet m_ds;


            DataTable table = new DataTable("Result");
            table.Columns.Add("Colaborador", Type.GetType("System.String"));
            table.Columns.Add("Admissao", Type.GetType("System.String"));
            table.Columns.Add("Matricula", Type.GetType("System.String"));
            table.Columns.Add("VacinaTipo", Type.GetType("System.String"));
            table.Columns.Add("Dose", Type.GetType("System.String"));
            table.Columns.Add("Data_Vacina", Type.GetType("System.String"));
            table.Columns.Add("Setor", Type.GetType("System.String"));
            table.Columns.Add("Funcao", Type.GetType("System.String"));
            table.Columns.Add("Titulagem", Type.GetType("System.String"));
            table.Columns.Add("Dias_Vacina", Type.GetType("System.Int32"));
            table.Columns.Add("Dias_Admissao", Type.GetType("System.Int32"));
            table.Columns.Add("nId_Setor", Type.GetType("System.Int32"));
            table.Columns.Add("IdVacinaTipo", Type.GetType("System.Int32"));

            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);


            strSQL.Append(" select d.tNO_STR_EMPR as Setor, b.VacinaTipo, c.Dose, f.tno_empg as Colaborador, convert(char(10), f.hdt_adm, 103) as Admissao, f.tCOD_EMPR as Matricula, ");
            strSQL.Append(" case when i.VacinaTipo + h.Dose = b.vacinatipo + c.dose then convert(char(10), g.DataVacina, 103 )  else '-' end as Data_Vacina ");
            strSQL.Append(" from Vacina_Setor  as a ");
            strSQL.Append(" left join Vacina_Tipo as b on(a.IdVacinaTipo = b.IdVacinaTipo) ");
            strSQL.Append(" left join vacina_dose as c on(b.IdVacinaTipo = c.IdVacinaTipo) ");
            strSQL.Append(" left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblsetor as d on(a.IdSetor = d.nID_SETOR) ");
            strSQL.Append(" left join ( ");
            strSQL.Append(" select nid_Empregado, nid_setor, nid_empr from " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEMPREGADO_FUNCAO where hdt_termino is null ");
            strSQL.Append("    ) as tx50 on(d.nid_setor = tx50.nid_setor) ");
            strSQL.Append(" left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblempregado as f on(tx50.nid_empregado = f.nid_empregado) ");
            strSQL.Append(" left join vacina as g on(g.idempregado = f.nid_empregado) ");
            strSQL.Append(" left join vacina_dose as h on(g.IdVacinaDose = h.IdVacinaDose) ");
            strSQL.Append(" left join vacina_tipo as i on(h.IdVacinaTipo = i.IdVacinaTipo) ");
            
            strSQL.Append(" where f.tno_empg is not null and f.hdt_dem is null ");

            if (xEmp == 1)
            {
                strSQL.Append(" and tx50.nId_Empr = " + xIdCliente.ToString() + "  ");
            }
            else if (xEmp == 2)
            {
                strSQL.Append(" and tx50.nId_Empr in (  select idpessoa from juridica where idjuridica = " + xIdCliente.ToString() + " or idjuridicapai = " + xIdCliente.ToString() + " )   ");
            }
            else if (xEmp == 3)
            {
                strSQL.Append(" and tx50.nId_Empr in (  select idpessoa from juridica where idgrupoempresa in ( select idgrupoempresa from juridica where idpessoa = " + xIdCliente.ToString() + " ) )   ");
            }


            if (xConsiderar != "0")
            {
                strSQL.Append(" and d.nId_Setor = " + xConsiderar + " ");
            }


            strSQL.Append(" order by f.tno_empg, d.tno_str_empr, b.VacinaTipo, c.dose ");



            //strSQL.Append(" select tno_Empg as Colaborador, Dose, VacinaTipo, convert( char(10),DataVacina, 103 ) as Data_Vacina, convert( char(10),hDt_ADM, 103 ) as Admissao, tCod_Empr as Matricula, tx90.nId_Empregado, tno_str_Empr as Setor, NomeFuncao as Funcao, tblSetor.nId_Setor, IdVacinaTipo, ");
            //strSQL.Append("        datediff(dd, hdt_adm, getdate()) as Dias_Admissao, ");

            ////se data de admissao e data vacina iguais, considerar 1,  pois zero indica que não houve vacina
            //strSQL.Append("        case         ");
            //strSQL.Append("        when datediff(dd, hdt_adm, DataVacina) = 0 then 1  ");
            //strSQL.Append("        else datediff(dd, hdt_adm, DataVacina)  ");
            //strSQL.Append("        end as Dias_Vacina,  ");

            //strSQL.Append("        Titulagem from ");
            //strSQL.Append(" ( ");
            //strSQL.Append("    select a.tNo_Empg, c.Dose, d.IdVacinaTipo, d.VacinaTipo, b.DataVacina, a.hDT_ADM, a.tCod_Empr, a.nId_Empregado, '' as Titulagem  ");
            //strSQL.Append("    from " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblempregado as a ");
            //strSQL.Append("    left join vacina as b on(a.nid_empregado = b.idempregado) ");
            //strSQL.Append("    left join vacina_dose as c on(b.IdVacinaDose = c.IdVacinaDose) ");
            //strSQL.Append("    left join vacina_tipo as d on(c.IdVacinaTipo = d.IdVacinaTipo) ");

            //strSQL.Append("  where a.hdt_dem is null ");

            //if (xEmp == 1)
            //{
            //    strSQL.Append(" and nId_Empr = " + xIdCliente.ToString() + "  ");
            //}
            //else if (xEmp == 2)
            //{
            //    strSQL.Append(" and nId_Empr in (  select idpessoa from juridica where idjuridica = " + xIdCliente.ToString() + " or idjuridicapai = " + xIdCliente.ToString() + " )   ");
            //}
            //else if (xEmp == 3)
            //{
            //    strSQL.Append(" and nId_Empr in (  select idpessoa from juridica where idgrupoempresa in ( select idgrupoempresa from juridica where idpessoa = " + xIdCliente.ToString() + " ) )   ");
            //}


            //strSQL.Append(" union ");

            //strSQL.Append("    select a.tNo_Empg, '' as Dose, 0 as IdVacinaTipo, 'Anti-HBS' as VacinaTipo, b.DataExame as DataVacina, a.hDT_ADM, a.tCod_Empr, a.nId_Empregado, convert( varchar(100),Prontuario ) as Titulagem  ");
            //strSQL.Append("    from " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblempregado as a ");
            //strSQL.Append("       left join examebase as b on(a.nid_empregado = b.idempregado) ");
            //strSQL.Append("       where b.idexamedicionario = -1174768070 ");


            //strSQL.Append("  and a.hdt_dem is null ");

            //if (xEmp == 1)
            //{
            //    strSQL.Append(" and nId_Empr = " + xIdCliente.ToString() + "  ");
            //}
            //else if (xEmp == 2)
            //{
            //    strSQL.Append(" and nId_Empr in (  select idpessoa from juridica where idjuridica = " + xIdCliente.ToString() + " or idjuridicapai = " + xIdCliente.ToString() + " )   ");
            //}
            //else if (xEmp == 3)
            //{
            //    strSQL.Append(" and nId_Empr in (  select idpessoa from juridica where idgrupoempresa in ( select idgrupoempresa from juridica where idpessoa = " + xIdCliente.ToString() + " ) )   ");
            //}

            //strSQL.Append(" ) as tx90 ");


            //strSQL.Append("  LEFT JOIN  ");
            //strSQL.Append("  ( SELECT nId_Empregado, nID_FUNCAO, nID_SETOR, nId_Empregado_Funcao  ");
            //strSQL.Append("    from " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.TBLEMPREGADO_FUNCAO  ");
            //strSQL.Append("    where convert( char(10),  hDt_Inicio, 103 ) + ' ' + convert( char(15),nID_EMPREGADO ) in  ");
            //strSQL.Append("    ( select convert( char(10),  max(hDt_Inicio), 103 ) + ' ' + convert( char(15),nID_EMPREGADO )   ");
            //strSQL.Append("      from " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEMPREGADO_FUNCAO   ");
            //strSQL.Append("      group by nId_Empregado  ");
            //strSQL.Append("     )  ");
            //strSQL.Append("  ) AS TX10 ");
            //strSQL.Append("  ON ( tx90.nID_EMPREGADO = TX10.nID_EMPREGADO )  ");


            //strSQL.Append("   left join   ( ");
            //strSQL.Append("      select nId_Empregado_Funcao, tNo_Func ");
            //strSQL.Append("	  from " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.TBLFunc_Empregado left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.TBLFunc on ( TBLFunc_Empregado.nId_Func = TBLFunc.nId_Func ) ");
            //strSQL.Append("	  where TBLFunc_Empregado.nID_LAUD_TEC in ");
            //strSQL.Append("	  (  ");
            //strSQL.Append("	     select nId_Laud_Tec from " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.TblLaudo_tec ");
            //strSQL.Append("		 where convert( char(15), nId_Empr ) + convert( char(10), hdt_laudo, 103 ) in ");
            //strSQL.Append("		 (   select convert( char(15), nId_Empr ) + convert( char(10), max(hdt_laudo), 103)  ");
            //strSQL.Append("		     from " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblLAUDO_TEC ");
            //strSQL.Append("			 group by nId_Empr     ");
            //strSQL.Append("          ) ");
            //strSQL.Append("	  )  ");
            //strSQL.Append("	)  as tx50   on tx50.nId_Empregado_Funcao = tx10.nId_Empregado_Funcao  ");

            //strSQL.Append("left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblsetor on  tx10.nID_SETOR = tblSETOR.nID_SETOR   ");
            //strSQL.Append("left join Funcao on tx10.nID_FUNCAO = Funcao.IdFuncao   ");


            //strSQL.Append(" where IdVacinaTipo is not null and tx10.nid_setor is not null ");

            //if (xConsiderar != "0")
            //{
            //    strSQL.Append(" and tblSetor.nId_Setor = " + xConsiderar + " ");

            //    //strSQL.Append(" and (  ");
            //    //strSQL.Append("            tx90.IdVacinaTipo not in (select distinct IdVacinaTipo from Vacina_Setor  where IdSetor in (select nId_Setor from " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblSetor where nId_Empr = 1353976976   ) )  ");
            //    //strSQL.Append("        or(convert(char(12), IdVacinaTipo) + ' ' + convert(char(12), tblSetor.nId_Setor)  in ");
            //    //strSQL.Append("             (select convert(char(12), IdVacinaTipo) + ' ' + convert(char(12), IdSetor)    from Vacina_Setor ");
            //    //strSQL.Append("             ) ");
            //    //strSQL.Append("           ) ");
            //    //strSQL.Append("   ) ");

            //}
            //// o problema aqui, é que todos os setores que não devem aparecer para hepatite, na checagem na hora de montar
            //// o relatório,  exibirá como necessária a primeira dose de hepatite.
            //// acho que será mais correto restringir na montagem do relatório


            //strSQL.Append(" order by tno_empg, VacinaTipo, Dose ");




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






        public DataSet Gerar_DS_Relatorio_Ambulatoriais_Sum(int xIdCliente, string xD_Inicial, string xD_Final, string xTipoRel, string xConsiderar, Int16 xEmp)
        {
            //string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection();
            StringBuilder strSQL = new StringBuilder();

            //DataSet m_ds;


            DataTable table = new DataTable("Result");
            table.Columns.Add("Empregado", Type.GetType("System.String"));
            table.Columns.Add("Setor", Type.GetType("System.String"));
            table.Columns.Add("Funcao", Type.GetType("System.String"));
            table.Columns.Add("Admissao", Type.GetType("System.String"));
            table.Columns.Add("Nascimento", Type.GetType("System.String"));
            table.Columns.Add("Afastamento", Type.GetType("System.String"));
            table.Columns.Add("Retorno", Type.GetType("System.String"));
            table.Columns.Add("Tipo_Afastamento", Type.GetType("System.String"));
            table.Columns.Add("Cid1", Type.GetType("System.String"));
            table.Columns.Add("Cid2", Type.GetType("System.String"));
            table.Columns.Add("Cid3", Type.GetType("System.String"));
            table.Columns.Add("Cid4", Type.GetType("System.String"));
            table.Columns.Add("NumeroCat", Type.GetType("System.String"));
            table.Columns.Add("Data1", Type.GetType("System.String"));
            table.Columns.Add("Data2", Type.GetType("System.String"));
            table.Columns.Add("Registros", Type.GetType("System.Int16"));
            table.Columns.Add("Exame", Type.GetType("System.String"));

            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);

            strSQL.Append(" select CNPJ, CID1, Tipo, Registros,  '" + xD_Inicial.Substring(0, 10) + "' as Data1, '" + xD_Final.Substring(0, 10) + "' as Data2   ");
            strSQL.Append(" from ( ");

            strSQL.Append(" select CNPJ, 'Tipo de Atendimento' as CID1, case when Tipo_Afastamento is null then '-' else Tipo_Afastamento end as Tipo, count(*) as Registros ");
            strSQL.Append(" from ( ");

            strSQL.Append(" select b.tNo_Empg as Empregado, r.Nomeabreviado + '  ' + r.NomeCodigo as CNPJ,  convert( char(10), d.DataExame, 103 ) as Retorno,  ");
            strSQL.Append(" case   ");
            strSQL.Append("   when a.Tipo='E' then 'Enfermagem'  ");
            strSQL.Append("   when a.Tipo='M' then 'Médico'  ");
            strSQL.Append("   when a.Tipo='O' then 'Outros'  ");
            strSQL.Append(" end as Tipo_Afastamento, e.NomeQueixa as Funcao, f.NomeProcedimento as Setor, '" + xD_Inicial.Substring(0, 10) + "' as Data1, '" + xD_Final.Substring(0, 10) + "' as Data2   ");
            strSQL.Append(" from cliniconaoocupacional as a   ");
            strSQL.Append(" join examebase as d on ( a.idexamebase = d.idexamebase )  ");
            strSQL.Append(" left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblempregado as b  ");
            strSQL.Append(" on ( d.idEmpregado = b.nId_Empregado )  ");
            strSQL.Append(" left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEmpregado_Funcao as c  ");
            strSQL.Append(" on ( b.nId_Empregado = c.nId_Empregado   ");

            if (xEmp == 1)
            {
                strSQL.Append(" and c.nId_Empr = " + xIdCliente.ToString() + " ) ");
            }
            else if (xEmp == 2)
            {
                strSQL.Append(" and c.nId_Empr in (  select idpessoa from juridica where idjuridica = " + xIdCliente.ToString() + " or idjuridicapai = " + xIdCliente.ToString() + " )  ) ");
            }
            else if (xEmp == 3)
            {
                strSQL.Append(" and c.nId_Empr in (  select idpessoa from juridica where idgrupoempresa in ( select idgrupoempresa from juridica where idpessoa = " + xIdCliente.ToString() + " ) )  ) ");
            }


            strSQL.Append(" join (   ");
            strSQL.Append("    select convert( char(15), nId_Empregado ) + convert( char(10),max( hDt_Inicio ),103 ) as Chave  ");
            strSQL.Append("    from  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblempregado_Funcao  ");

            if (xEmp == 1)
            {
                strSQL.Append(" where nId_Empr = " + xIdCliente.ToString() + " ");
            }
            else if (xEmp == 2)
            {
                strSQL.Append(" where nId_Empr in (  select idpessoa from juridica where idjuridica = " + xIdCliente.ToString() + " or idjuridicapai = " + xIdCliente.ToString() + " ) ");
            }
            else if (xEmp == 3)
            {
                strSQL.Append(" where nId_Empr in (  select idpessoa from juridica where idgrupoempresa in ( select idgrupoempresa from juridica where idpessoa = " + xIdCliente.ToString() + " ) ) ");
            }

            strSQL.Append("    group by nId_Empregado  ");
            strSQL.Append(" ) as tx80 on tx80.Chave = convert( char(15), c.nId_Empregado ) + convert( char(10),c.hDt_Inicio,103 )  ");


            strSQL.Append(" left join queixaclinica as e  ");
            strSQL.Append(" on ( a.idqueixaclinica = e.IdQueixaClinica )  ");
            strSQL.Append(" left join ProcedimentoClinico as f  ");
            strSQL.Append(" on ( a.idprocedimentoclinico = f.idprocedimentoclinico )  ");
            strSQL.Append(" left join pessoa as r  ");
            strSQL.Append(" on ( c.nId_Empr = r.IdPessoa )  ");


            if (xEmp == 1)
            {
                strSQL.Append(" where c.nId_Empr = " + xIdCliente.ToString() + " ");
            }
            else if (xEmp == 2)
            {
                strSQL.Append(" where c.nId_Empr in (  select idpessoa from juridica where idjuridica = " + xIdCliente.ToString() + " or idjuridicapai = " + xIdCliente.ToString() + " ) ");
            }
            else if (xEmp == 3)
            {
                strSQL.Append(" where c.nId_Empr in (  select idpessoa from juridica where idgrupoempresa in ( select idgrupoempresa from juridica where idpessoa = " + xIdCliente.ToString() + " ) ) ");
            }


            strSQL.Append(" and  d.DataExame between  convert( smalldatetime, '" + xD_Inicial + "', 103 ) and convert( smalldatetime, '" + xD_Final + "', 103 ) and b.tno_Empg is not null ");

            if (xConsiderar == "1")
            {
                strSQL.Append(" and b.hDt_Dem is null ");
            }
            else if (xConsiderar == "2")
            {
                strSQL.Append(" and b.hDt_Dem is not null ");
            }


            strSQL.Append(" ) as tx99 ");
            strSQL.Append(" group by CNPJ, Tipo_Afastamento ");

            strSQL.Append(" union ");

            strSQL.Append(" select CNPJ, 'Queixas' as CID1, case when Funcao is null then '-' else Funcao end as Tipo, count(*) as Registros ");
            strSQL.Append(" from ( ");

            strSQL.Append(" select b.tNo_Empg as Empregado, r.Nomeabreviado + '  ' + r.NomeCodigo as CNPJ,  convert( char(10), d.DataExame, 103 ) as Retorno,  ");
            strSQL.Append(" case   ");
            strSQL.Append("   when a.Tipo='E' then 'Enfermagem'  ");
            strSQL.Append("   when a.Tipo='M' then 'Médico'  ");
            strSQL.Append("   when a.Tipo='O' then 'Outros'  ");
            strSQL.Append(" end as Tipo_Afastamento, e.NomeQueixa as Funcao, f.NomeProcedimento as Setor, '" + xD_Inicial.Substring(0, 10) + "' as Data1, '" + xD_Final.Substring(0, 10) + "' as Data2   ");
            strSQL.Append(" from cliniconaoocupacional as a   ");
            strSQL.Append(" join examebase as d on ( a.idexamebase = d.idexamebase )  ");
            strSQL.Append(" left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblempregado as b  ");
            strSQL.Append(" on ( d.idEmpregado = b.nId_Empregado )  ");
            strSQL.Append(" left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEmpregado_Funcao as c  ");
            strSQL.Append(" on ( b.nId_Empregado = c.nId_Empregado   ");

            if (xEmp == 1)
            {
                strSQL.Append(" and c.nId_Empr = " + xIdCliente.ToString() + " ) ");
            }
            else if (xEmp == 2)
            {
                strSQL.Append(" and c.nId_Empr in (  select idpessoa from juridica where idjuridica = " + xIdCliente.ToString() + " or idjuridicapai = " + xIdCliente.ToString() + " )  ) ");
            }
            else if (xEmp == 3)
            {
                strSQL.Append(" and c.nId_Empr in (  select idpessoa from juridica where idgrupoempresa in ( select idgrupoempresa from juridica where idpessoa = " + xIdCliente.ToString() + " ) )  ) ");
            }


            strSQL.Append(" join (   ");
            strSQL.Append("    select convert( char(15), nId_Empregado ) + convert( char(10),max( hDt_Inicio ),103 ) as Chave  ");
            strSQL.Append("    from  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblempregado_Funcao  ");

            if (xEmp == 1)
            {
                strSQL.Append(" where nId_Empr = " + xIdCliente.ToString() + " ");
            }
            else if (xEmp == 2)
            {
                strSQL.Append(" where nId_Empr in (  select idpessoa from juridica where idjuridica = " + xIdCliente.ToString() + " or idjuridicapai = " + xIdCliente.ToString() + " ) ");
            }
            else if (xEmp == 3)
            {
                strSQL.Append(" where nId_Empr in (  select idpessoa from juridica where idgrupoempresa in ( select idgrupoempresa from juridica where idpessoa = " + xIdCliente.ToString() + " ) ) ");
            }

            strSQL.Append("    group by nId_Empregado  ");
            strSQL.Append(" ) as tx80 on tx80.Chave = convert( char(15), c.nId_Empregado ) + convert( char(10),c.hDt_Inicio,103 )  ");


            strSQL.Append(" left join queixaclinica as e  ");
            strSQL.Append(" on ( a.idqueixaclinica = e.IdQueixaClinica )  ");
            strSQL.Append(" left join ProcedimentoClinico as f  ");
            strSQL.Append(" on ( a.idprocedimentoclinico = f.idprocedimentoclinico )  ");
            strSQL.Append(" left join pessoa as r  ");
            strSQL.Append(" on ( c.nId_Empr = r.IdPessoa )  ");


            if (xEmp == 1)
            {
                strSQL.Append(" where c.nId_Empr = " + xIdCliente.ToString() + " ");
            }
            else if (xEmp == 2)
            {
                strSQL.Append(" where c.nId_Empr in (  select idpessoa from juridica where idjuridica = " + xIdCliente.ToString() + " or idjuridicapai = " + xIdCliente.ToString() + " ) ");
            }
            else if (xEmp == 3)
            {
                strSQL.Append(" where c.nId_Empr in (  select idpessoa from juridica where idgrupoempresa in ( select idgrupoempresa from juridica where idpessoa = " + xIdCliente.ToString() + " ) ) ");
            }


            strSQL.Append(" and  d.DataExame between  convert( smalldatetime, '" + xD_Inicial + "', 103 ) and convert( smalldatetime, '" + xD_Final + "', 103 ) and b.tno_Empg is not null ");

            if (xConsiderar == "1")
            {
                strSQL.Append(" and b.hDt_Dem is null ");
            }
            else if (xConsiderar == "2")
            {
                strSQL.Append(" and b.hDt_Dem is not null ");
            }

            strSQL.Append(" ) as tx98 ");
            strSQL.Append(" group by CNPJ, Funcao ");

            strSQL.Append(" union ");

            strSQL.Append(" select CNPJ, 'Procedimentos' as CID1, case when Setor is null then '-' else Setor end as Tipo, count(*) as Registros ");
            strSQL.Append(" from ( ");

            strSQL.Append(" select b.tNo_Empg as Empregado, r.Nomeabreviado + '  ' + r.NomeCodigo as CNPJ,  convert( char(10), d.DataExame, 103 ) as Retorno,  ");
            strSQL.Append(" case   ");
            strSQL.Append("   when a.Tipo='E' then 'Enfermagem'  ");
            strSQL.Append("   when a.Tipo='M' then 'Médico'  ");
            strSQL.Append("   when a.Tipo='O' then 'Outros'  ");
            strSQL.Append(" end as Tipo_Afastamento, e.NomeQueixa as Funcao, f.NomeProcedimento as Setor, '" + xD_Inicial.Substring(0, 10) + "' as Data1, '" + xD_Final.Substring(0, 10) + "' as Data2   ");
            strSQL.Append(" from cliniconaoocupacional as a   ");
            strSQL.Append(" join examebase as d on ( a.idexamebase = d.idexamebase )  ");
            strSQL.Append(" left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblempregado as b  ");
            strSQL.Append(" on ( d.idEmpregado = b.nId_Empregado )  ");
            strSQL.Append(" left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEmpregado_Funcao as c  ");
            strSQL.Append(" on ( b.nId_Empregado = c.nId_Empregado   ");

            if (xEmp == 1)
            {
                strSQL.Append(" and c.nId_Empr = " + xIdCliente.ToString() + " ) ");
            }
            else if (xEmp == 2)
            {
                strSQL.Append(" and c.nId_Empr in (  select idpessoa from juridica where idjuridica = " + xIdCliente.ToString() + " or idjuridicapai = " + xIdCliente.ToString() + " )  ) ");
            }
            else if (xEmp == 3)
            {
                strSQL.Append(" and c.nId_Empr in (  select idpessoa from juridica where idgrupoempresa in ( select idgrupoempresa from juridica where idpessoa = " + xIdCliente.ToString() + " ) )  ) ");
            }


            strSQL.Append(" join (   ");
            strSQL.Append("    select convert( char(15), nId_Empregado ) + convert( char(10),max( hDt_Inicio ),103 ) as Chave  ");
            strSQL.Append("    from  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblempregado_Funcao  ");

            if (xEmp == 1)
            {
                strSQL.Append(" where nId_Empr = " + xIdCliente.ToString() + " ");
            }
            else if (xEmp == 2)
            {
                strSQL.Append(" where nId_Empr in (  select idpessoa from juridica where idjuridica = " + xIdCliente.ToString() + " or idjuridicapai = " + xIdCliente.ToString() + " ) ");
            }
            else if (xEmp == 3)
            {
                strSQL.Append(" where nId_Empr in (  select idpessoa from juridica where idgrupoempresa in ( select idgrupoempresa from juridica where idpessoa = " + xIdCliente.ToString() + " ) ) ");
            }

            strSQL.Append("    group by nId_Empregado  ");
            strSQL.Append(" ) as tx80 on tx80.Chave = convert( char(15), c.nId_Empregado ) + convert( char(10),c.hDt_Inicio,103 )  ");


            strSQL.Append(" left join queixaclinica as e  ");
            strSQL.Append(" on ( a.idqueixaclinica = e.IdQueixaClinica )  ");
            strSQL.Append(" left join ProcedimentoClinico as f  ");
            strSQL.Append(" on ( a.idprocedimentoclinico = f.idprocedimentoclinico )  ");
            strSQL.Append(" left join pessoa as r  ");
            strSQL.Append(" on ( c.nId_Empr = r.IdPessoa )  ");


            if (xEmp == 1)
            {
                strSQL.Append(" where c.nId_Empr = " + xIdCliente.ToString() + " ");
            }
            else if (xEmp == 2)
            {
                strSQL.Append(" where c.nId_Empr in (  select idpessoa from juridica where idjuridica = " + xIdCliente.ToString() + " or idjuridicapai = " + xIdCliente.ToString() + " ) ");
            }
            else if (xEmp == 3)
            {
                strSQL.Append(" where c.nId_Empr in (  select idpessoa from juridica where idgrupoempresa in ( select idgrupoempresa from juridica where idpessoa = " + xIdCliente.ToString() + " ) ) ");
            }


            strSQL.Append(" and  d.DataExame between  convert( smalldatetime, '" + xD_Inicial + "', 103 ) and convert( smalldatetime, '" + xD_Final + "', 103 ) and b.tno_Empg is not null ");

            if (xConsiderar == "1")
            {
                strSQL.Append(" and b.hDt_Dem is null ");
            }
            else if (xConsiderar == "2")
            {
                strSQL.Append(" and b.hDt_Dem is not null ");
            }


            strSQL.Append(" ) as tx97 ");
            strSQL.Append(" group by CNPJ, Setor ");

            strSQL.Append(" ) as tx55 ");
            strSQL.Append(" order by CNPJ, CID1, Tipo ");


            //strSQL.Append(" order by r.Nomeabreviado + '  ' + r.NomeCodigo, b.tNo_Empg,  d.DataExame  ");


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




        public DataSet Gerar_DS_Relatorio_Vacinas_Sum(int xIdCliente, string xD_Inicial, string xD_Final, string xTipoRel, string xConsiderar, Int16 xEmp)
        {
            //string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection();
            StringBuilder strSQL = new StringBuilder();

            //DataSet m_ds;


            DataTable table = new DataTable("Result");
            table.Columns.Add("Empregado", Type.GetType("System.String"));
            table.Columns.Add("Setor", Type.GetType("System.String"));
            table.Columns.Add("Funcao", Type.GetType("System.String"));
            table.Columns.Add("Admissao", Type.GetType("System.String"));
            table.Columns.Add("Nascimento", Type.GetType("System.String"));
            table.Columns.Add("Afastamento", Type.GetType("System.String"));
            table.Columns.Add("Retorno", Type.GetType("System.String"));
            table.Columns.Add("Tipo_Afastamento", Type.GetType("System.String"));
            table.Columns.Add("Cid1", Type.GetType("System.String"));
            table.Columns.Add("Cid2", Type.GetType("System.String"));
            table.Columns.Add("Cid3", Type.GetType("System.String"));
            table.Columns.Add("Cid4", Type.GetType("System.String"));
            table.Columns.Add("NumeroCat", Type.GetType("System.String"));
            table.Columns.Add("Data1", Type.GetType("System.String"));
            table.Columns.Add("Data2", Type.GetType("System.String"));
            table.Columns.Add("Registros", Type.GetType("System.Int16"));
            table.Columns.Add("Exame", Type.GetType("System.String"));

            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);

            strSQL.Append(" select CNPJ, Tipo, count(*) as Registros, '" + xD_Inicial + "' as Data1, '" + xD_Final + "' as Data2 from ");
            strSQL.Append(" ( ");
            strSQL.Append("    select convert( char(10), a.DataVacina, 103 ) as Retorno, b.nId_Empr, a.DataVacina, b.tNO_EMPG as Empregado, c.Dose as Setor, d.VacinaTipo as Tipo, r.Nomeabreviado + '  ' + r.NomeCodigo as CNPJ  ");
            strSQL.Append("    from vacina as a left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblempregado as b ");
            strSQL.Append("    on ( a.idempregado = b.nid_empregado ) ");
            strSQL.Append("    left join Vacina_Dose as c ");
            strSQL.Append("    on ( a.IdVacinaDose = c.IdVacinaDose ) ");
            strSQL.Append("    left join Vacina_Tipo as d ");
            strSQL.Append("    on ( c.IdVacinaTipo = d.IdVacinaTipo ) ");
            strSQL.Append("    left join Pessoa as r ");
            strSQL.Append("    on ( b.nId_Empr = r.IdPessoa ) ");
            strSQL.Append("    union ");
            strSQL.Append("    select  convert( char(10), DataExame, 103 ) as Retorno, b.nId_Empr, DataExame as DataVacina, b.tNo_Empg as Empregado, convert( varchar(100),Prontuario) as Setor, 'Anti-HBS' as Tipo, r.Nomeabreviado + '  ' + r.NomeCodigo as CNPJ ");
            strSQL.Append("    from examebase as a left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblempregado as b ");
            strSQL.Append("    on ( a.IdEmpregado = b.nid_Empregado ) ");
            strSQL.Append("    left join Pessoa as r ");
            strSQL.Append("    on ( b.nId_Empr = r.IdPessoa ) ");
            strSQL.Append("    where idexamedicionario = -1174768070  ");
            strSQL.Append(" ) as tx90 ");

            strSQL.Append(" where  DataVacina between  convert( smalldatetime, '" + xD_Inicial + "', 103 ) and convert( smalldatetime, '" + xD_Final + "', 103 ) and Empregado is not null ");

            if (xEmp == 1)
            {
                strSQL.Append(" and nId_Empr = " + xIdCliente.ToString() + "  ");
            }
            else if (xEmp == 2)
            {
                strSQL.Append(" and nId_Empr in (  select idpessoa from juridica where idjuridica = " + xIdCliente.ToString() + " or idjuridicapai = " + xIdCliente.ToString() + " )   ");
            }
            else if (xEmp == 3)
            {
                strSQL.Append(" and nId_Empr in (  select idpessoa from juridica where idgrupoempresa in ( select idgrupoempresa from juridica where idpessoa = " + xIdCliente.ToString() + " ) )   ");
            }


            if (xConsiderar == "0")
            {
                strSQL.Append(" and Tipo <> 'Anti-HBS' ");
            }

            strSQL.Append(" group by CNPJ, Tipo ");
            strSQL.Append(" order by CNPJ, Tipo ");



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



        public DataSet Gerar_DS_Relatorio_GHE_Funcao_Exames(int xIdCliente, string xD_Inicial, string xCliente, string zEmp, string xFiltro)
        {
            //string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection();
            StringBuilder strSQL = new StringBuilder();

            //DataSet m_ds;


            DataTable table = new DataTable("Result");
            table.Columns.Add("Colaborador", Type.GetType("System.String"));
            table.Columns.Add("GHE", Type.GetType("System.String"));
            table.Columns.Add("Setor", Type.GetType("System.String"));
            table.Columns.Add("Funcao", Type.GetType("System.String"));
            table.Columns.Add("tSexo", Type.GetType("System.String"));
            table.Columns.Add("Admissao", Type.GetType("System.String"));
            table.Columns.Add("Nascimento", Type.GetType("System.String"));
            table.Columns.Add("Idade", Type.GetType("System.String"));
            table.Columns.Add("DataInicial", Type.GetType("System.String"));
            table.Columns.Add("DataVolta", Type.GetType("System.String"));
            table.Columns.Add("DataPrevista", Type.GetType("System.String"));
            table.Columns.Add("Deficiencia", Type.GetType("System.String"));

            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);



            strSQL.Append(" select distinct a.NomeAbreviado as DataVolta, b.tno_func as GHE, e.tno_func_empr as Funcao, h.Nome as Nascimento, convert( char(10), getdate(), 103 ) as DataPrevista ");
            strSQL.Append(" from " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.pessoa as a join  ");
            strSQL.Append(" ( select nid_laud_tec, nid_empr  ");
            strSQL.Append("   from tbllaudo_Tec ");
            strSQL.Append("   where convert( char(15), nid_empr ) + ' ' + convert( char(10), hdt_laudo, 103 ) in ");
            strSQL.Append("     ( select convert( char(15), nid_empr ) + ' ' + convert( char(10), max(hdt_laudo), 103 )  ");
            strSQL.Append(" 	  from tbllaudo_tec   ");
            strSQL.Append(" 	  where nid_empr in ( " + xFiltro + " )   ");
            strSQL.Append(" 	  group by nid_empr ");
            strSQL.Append("     ) ");
            strSQL.Append("   )	 as tx09  on a.idpessoa = tx09.nid_empr ");
            strSQL.Append("   left join tblfunc as b ");
            strSQL.Append("   on ( tx09.nid_laud_tec = b.nid_laud_tec ) ");
            strSQL.Append("   left join tblfunc_empregado as c ");
            strSQL.Append("   on ( b.nid_func = c.nid_func ) ");
            strSQL.Append("   left join tblempregado_funcao as d ");
            strSQL.Append("   on ( c.nid_empregado_funcao = d.nid_empregado_funcao ) ");
            strSQL.Append("   left join tblfuncao as e ");
            strSQL.Append("   on ( d.nid_funcao = e.nid_funcao ) ");
            strSQL.Append("   left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.pcmso as f ");
            strSQL.Append("   on ( tx09.nid_laud_tec = f.idLaudoTecnico ) ");
            strSQL.Append("   left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.pcmsoplanejamento as g ");
            strSQL.Append("   on ( f.IdPcmso = g.IdPcmso and b.nid_func = g.idghe ) ");
            strSQL.Append("   join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.examedicionario as h ");
            strSQL.Append("   on ( g.idExameDicionario = h.IdExameDicionario ) ");
            strSQL.Append("   where e.tno_func_empr is not null and a.isinativo = 0 ");
            strSQL.Append(" order by a.NomeAbreviado, b.tno_func, e.tno_func_empr, h.Nome ");



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

            return m_ds;

        }



        public DataSet Gerar_DS_Relatorio_Exames_Empr(int xIdCliente, string xD_Inicial, string xD_Final, string xCliente, string xExames, string xResult, int xEmp)
        {
            //string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection();
            StringBuilder strSQL = new StringBuilder();

            //DataSet m_ds;


            DataTable table = new DataTable("Result");
            table.Columns.Add("Exame", Type.GetType("System.String"));
            table.Columns.Add("Empregado", Type.GetType("System.String"));
            table.Columns.Add("DataExame", Type.GetType("System.String"));
            table.Columns.Add("Grupo", Type.GetType("System.String"));
            table.Columns.Add("Empresa", Type.GetType("System.String"));
            table.Columns.Add("Data1", Type.GetType("System.String"));
            table.Columns.Add("Data2", Type.GetType("System.String"));
            table.Columns.Add("Setor", Type.GetType("System.String"));
            table.Columns.Add("Funcao", Type.GetType("System.String"));
            table.Columns.Add("Tipo", Type.GetType("System.String"));
            table.Columns.Add("Resultado", Type.GetType("System.String"));
            table.Columns.Add("CPF", Type.GetType("System.String"));
            table.Columns.Add("RG", Type.GetType("System.String"));
            table.Columns.Add("CNPJ", Type.GetType("System.String"));
            table.Columns.Add("Retorno", Type.GetType("System.String"));
            table.Columns.Add("tObs", Type.GetType("System.String"));


            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);




            strSQL.Append("select IdExameBase, isnull(Nome,'')  as exame, isnull(tNo_Empg,'') as Empregado, 'CPF:' + isnull(tNO_CPF,'') as CPF, 'RG: ' + isnull(tno_Identidade,'') as RG, xDataExame as DataExame, isnull(Clinica,'') as CNPJ,'( ' +  Agendamento + ' )' as Retorno, ");
            strSQL.Append(" case when IndResultado = 1 then 'Normal'  when IndResultado = 2 then 'Alterado'  when IndResultado = 3 then 'Em espera'  when IndResultado = 0 then 'Sem Resultado'  end as Resultado, ");
            strSQL.Append("Grupo, Empresa, '" + xD_Inicial.Substring(0, 10) + "' as Data1, '" + xD_Final.Substring(0, 10) + "' as Data2, 'Setor: ' + isnull(Setor1,'') as Setor, 'Função: ' + isnull(Funcao1,'') as Funcao, 'GHE: ' + isnull(GHE,'') as Tipo, tObs ");
            strSQL.Append("from ");
            strSQL.Append("( ");


            strSQL.Append("select l.IdExameBase, l.IndResultado, l.IdEmpregado, e.tNo_Empg, e.tNO_CPF, e.tNo_Identidade, k.Nome , convert( char(10),l.DataExame,103 ) as xDataExame, tblSetor.tNO_STR_EMPR  as Setor1, Funcao.NomeFuncao as Funcao1, tx50.tNo_Func as GHE, z.NomeAbreviado as Clinica, isnull(pr.Descricao,'') as Grupo, isnull(p.NomeAbreviado,'') as Empresa, isnull(convert( char(10),max( v.Data_Hora_Criacao ),103 ),'') as Agendamento, isnull(e.tObs,'') as tObs   ");
            strSQL.Append("from examebase as l (nolock)   ");
            strSQL.Append("left join examedicionario as k  (nolock) on l.idexamedicionario =  ");
            strSQL.Append("k.idexamedicionario ");
            strSQL.Append("left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEmpregado as e  (nolock) on l.IdEmpregado = e.nId_Empregado ");
            strSQL.Append("left join Pessoa as z  (nolock) on (l.IdJuridica = z.IdPessoa) ");
            strSQL.Append("left join Guia_Encaminhamento as v  (nolock) on(v.Id_Empregado = l.IdEmpregado and convert(smalldatetime, case when isdate(v.Data)='' then '01/01/2000' else v.Data end, 103) = l.DataExame ) ");

            strSQL.Append("  LEFT JOIN  ");
            strSQL.Append("  ( SELECT nId_Empregado, nID_FUNCAO, nID_SETOR, nId_Empregado_Funcao  ");
            strSQL.Append("    from " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.TBLEMPREGADO_FUNCAO  (nolock)  ");
            strSQL.Append("    where convert( char(10),  hDt_Inicio, 103 ) + ' ' + convert( char(15),nID_EMPREGADO ) in  ");
            strSQL.Append("    ( select convert( char(10),  max(hDt_Inicio), 103 ) + ' ' + convert( char(15),nID_EMPREGADO )   ");
            strSQL.Append("      from " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEMPREGADO_FUNCAO   (nolock)  ");

            if (xEmp == 1)
            {
                strSQL.Append("WHERE nID_EMPR= " + xIdCliente.ToString() + "  ");
            }
            else if (xEmp == 2)
            {
                strSQL.Append("WHERE nID_EMPR in (  select idpessoa from juridica  (nolock) where idjuridica = " + xIdCliente.ToString() + " or idjuridicapai = " + xIdCliente.ToString() + " ) ");
            }
            else if (xEmp == 3)
            {
                strSQL.Append("WHERE nID_EMPR in (  select idpessoa from juridica  (nolock) where idgrupoempresa in ( select idgrupoempresa from juridica  (nolock) where idpessoa = " + xIdCliente.ToString() + " ) ) ");
            }
            else if (xEmp == 0 || xEmp == 4)
            {
                strSQL.Append("WHERE nID_EMPR is not null and nid_Empr in ( select idpessoa from pessoa where isinativo=0) ");
            }

            strSQL.Append("      group by nId_Empregado  ");
            strSQL.Append("     )  ");
            strSQL.Append("  ) AS TX10 ");
            strSQL.Append("  ON ( e.nID_EMPREGADO = TX10.nID_EMPREGADO )  ");


            strSQL.Append("   left join   ( ");
            strSQL.Append("      select nId_Empregado_Funcao, tNo_Func ");
            strSQL.Append("	  from " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.TBLFunc_Empregado  (nolock) left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.TBLFunc  (nolock) on ( TBLFunc_Empregado.nId_Func = TBLFunc.nId_Func ) ");
            strSQL.Append("	  where TBLFunc_Empregado.nID_LAUD_TEC in ");
            strSQL.Append("	  (  ");
            strSQL.Append("	     select nId_Laud_Tec from " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.TblLaudo_tec  (nolock) ");
            strSQL.Append("		 where convert( char(15), nId_Empr ) + convert( char(10), hdt_laudo, 103 ) in ");
            strSQL.Append("		 (   select convert( char(15), nId_Empr ) + convert( char(10), max(hdt_laudo), 103)  ");
            strSQL.Append("		     from " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblLAUDO_TEC  (nolock) ");
            strSQL.Append("			 group by nId_Empr     ");
            strSQL.Append("          ) ");
            strSQL.Append("	  )  ");
            strSQL.Append("	)  as tx50   on tx50.nId_Empregado_Funcao = tx10.nId_Empregado_Funcao  ");

            strSQL.Append("left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblsetor  (nolock) on  tx10.nID_SETOR = tblSETOR.nID_SETOR   ");
            strSQL.Append("left join Funcao  (nolock)  on tx10.nID_FUNCAO = Funcao.IdFuncao   ");
            strSQL.Append("  left join Pessoa as p  (nolock) on ( e.nID_EMPR = p.IdPessoa ) ");
            strSQL.Append("  left join Juridica as pq  (nolock) on ( pq.IdPessoa = p.IdPessoa ) ");
            strSQL.Append("  left join GrupoEmpresa as pr  (nolock) on ( pq.IdGrupoEmpresa = pr.IdGrupoEmpresa ) ");

            strSQL.Append("where idempregado in ");
            strSQL.Append("( ");

            strSQL.Append("SELECT nID_EMPREGADO  ");
            strSQL.Append("FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEMPREGADO_FUNCAO  (nolock)  ");
            //strSQL.Append("WHERE nID_EMPR= " + xIdCliente.ToString() + "  ");


            if (xEmp == 1)
            {
                strSQL.Append("WHERE nID_EMPR= " + xIdCliente.ToString() + "  ");
            }
            else if (xEmp == 2)
            {
                strSQL.Append("WHERE nID_EMPR in (  select idpessoa from juridica  (nolock) where idjuridica = " + xIdCliente.ToString() + " or idjuridicapai = " + xIdCliente.ToString() + " ) ");
            }
            else if (xEmp == 3)
            {
                strSQL.Append("WHERE nID_EMPR in (  select idpessoa from juridica  (nolock) where idgrupoempresa in ( select idgrupoempresa from juridica  (nolock) where idpessoa = " + xIdCliente.ToString() + " ) ) ");
            }
            else if (xEmp == 0 || xEmp == 4)
            {
                strSQL.Append("WHERE nID_EMPR is not null and nid_Empr in ( select idpessoa from pessoa where isinativo=0) ");
            }



            // strSQL.Append("SELECT nID_EMPREGADO  ");
            // strSQL.Append("FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEMPREGADO_FUNCAO  ");
            // strSQL.Append("WHERE nID_EMPR= " + xIdCliente.ToString() + "  ");

            strSQL.Append(") ");



            if (xExames == "C")
            {
                strSQL.Append(" and k.IdExameDicionario between 1 and 5 ");
            }
            else if (xExames == "P")
            {
                strSQL.Append(" and k.IdExameDicionario not between 1 and 5 ");
            }


            strSQL.Append("and l.DataExame between convert( smalldatetime, '" + xD_Inicial.Substring(0, 10) + "', 103 ) and 	convert( smalldatetime, '" + xD_Final.Substring(0, 10) + " 23:59', 103 )  ");

            //strSQL.Append(" and IndResultado not in ( 0, 3 ) ");
            strSQL.Append(" and IndResultado in " + xResult + " ");

            strSQL.Append("group by l.IdExameBase,l.IdEmpregado, e.tNo_Empg, e.tNO_CPF, e.tNo_Identidade, k.Nome, l.DataExame, tblSetor.tNO_STR_EMPR , Funcao.NomeFuncao, tx50.tNo_Func, l.IndResultado, z.NomeAbreviado, pr.Descricao, p.NomeAbreviado, e.tObs	 ");


            strSQL.Append("	) ");
            strSQL.Append("as tx1 ");
            strSQL.Append("group by IdExameBase, Nome, tNo_Empg, tNO_CPF, tNo_Identidade, xDataExame, Setor1, Funcao1, GHE, IndResultado, Clinica, Grupo, Empresa, Agendamento, tObs  ");
            strSQL.Append("order by Empresa, tNo_Empg, xDataExame, Nome ");



            //m_ds = new DataSet("Result");

            using (SqlConnection cnn = new SqlConnection(Ilitera.Data.SQLServer.EntitySQLServer.GetConnection()))
            {
                SqlDataAdapter da;
                cnn.Open();


                da = new SqlDataAdapter(strSQL.ToString(), cnn);
                da.SelectCommand.CommandTimeout = 1800;
                da.Fill(m_ds, "Result");

                cnn.Close();

                da.Dispose();
            }

            return m_ds;

        }


        //public DataSet Gerar_DS_Relatorio_Exames_Empr(int xIdCliente, string xD_Inicial, string xD_Final, string xCliente, string xExames, string xResult, int xEmp)
        //{
        //    //string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection();
        //    //stringBuilder strSQL = new StringBuilder();

        //    //DataSet m_ds;


        //    DataTable table = new DataTable("Result");
        //    table.Columns.Add("Exame", Type.GetType("System.String"));
        //    table.Columns.Add("Empregado", Type.GetType("System.String"));
        //    table.Columns.Add("DataExame", Type.GetType("System.String"));
        //    table.Columns.Add("Empresa", Type.GetType("System.String"));
        //    table.Columns.Add("Data1", Type.GetType("System.String"));
        //    table.Columns.Add("Data2", Type.GetType("System.String"));
        //    table.Columns.Add("Setor", Type.GetType("System.String"));
        //    table.Columns.Add("Funcao", Type.GetType("System.String"));
        //    table.Columns.Add("Tipo", Type.GetType("System.String"));
        //    table.Columns.Add("Resultado", Type.GetType("System.String"));
        //    table.Columns.Add("CPF", Type.GetType("System.String"));
        //    table.Columns.Add("RG", Type.GetType("System.String"));
        //    table.Columns.Add("CNPJ", Type.GetType("System.String"));
        //    table.Columns.Add("Retorno", Type.GetType("System.String"));


        //    DataSet m_ds = new DataSet();
        //    m_ds.Tables.Add(table);

        //    SqlCommand rCommand = new SqlCommand();

        //    rCommand.Parameters.Add("@xD_Inicial", SqlDbType.NChar).Value = xD_Inicial.Substring(0, 10);
        //    rCommand.Parameters.Add("@xD_Final", SqlDbType.NChar).Value = xD_Final.Substring(0, 10);
        //    rCommand.Parameters.Add("@xIdCliente", SqlDbType.NChar).Value = xIdCliente.ToString();
        //    rCommand.Parameters.Add("@xResult", SqlDbType.Text).Value = xResult.ToString();
        //    rCommand.Parameters.Add("@xDB1", SqlDbType.Text).Value = Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper();
        //    rCommand.Parameters.Add("@xDB2", SqlDbType.Text).Value = Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper();


        //    rCommand.CommandText = "select IdExameBase, Nome  as exame, tNo_Empg as Empregado, 'CPF:' + tNO_CPF as CPF, 'RG: ' + tno_Identidade as RG, xDataExame as DataExame, Clinica as CNPJ,'( ' +  Agendamento + ' )' as Retorno, "
        //     + " case when IndResultado = 1 then 'Normal'  when IndResultado = 2 then 'Alterado'  when IndResultado = 3 then 'Em espera'  when IndResultado = 0 then 'Sem Resultado'  end as Resultado, "
        //     + "Empresa, @xD_Inicial as Data1, @xD_Final as Data2, 'Setor: ' + Setor1 as Setor, 'Função: ' + Funcao1 as Funcao, 'GHE: ' + GHE as Tipo "
        //     + "from "
        //     + "( "


        //    + "select l.IdExameBase, l.IndResultado, l.IdEmpregado, e.tNo_Empg, e.tNO_CPF, e.tNo_Identidade, k.Nome , convert( char(10),l.DataExame,103 ) as xDataExame, tblSetor.tNO_STR_EMPR  as Setor1, Funcao.NomeFuncao as Funcao1, tx50.tNo_Func as GHE, z.NomeAbreviado as Clinica, p.NomeAbreviado as Empresa, convert( char(10),max( v.Data_Hora_Criacao ),103 ) as Agendamento   "
        //    + "from examebase as l  "
        //    + "left join examedicionario as k on l.idexamedicionario =  "
        //    + "k.idexamedicionario "
        //    + "left join [@xDB2].dbo.tblEmpregado as e on l.IdEmpregado = e.nId_Empregado "
        //    + "left join Pessoa as z on (l.IdJuridica = z.IdPessoa) "
        //     + "left join Guia_Encaminhamento as v on(v.Id_Empregado = l.IdEmpregado and convert(smalldatetime, case when isdate(v.Data)='' then '01/01/2000' else v.Data end, 103) = l.DataExame ) "

        //    + "  LEFT JOIN  "
        //    + "  ( SELECT nId_Empregado, nID_FUNCAO, nID_SETOR, nId_Empregado_Funcao  "
        //    + "    from [@xDB2].dbo.TBLEMPREGADO_FUNCAO  "
        //    + "    where convert( char(10),  hDt_Inicio, 103 ) + ' ' + convert( char(15),nID_EMPREGADO ) in  "
        //    + "    ( select convert( char(10),  max(hDt_Inicio), 103 ) + ' ' + convert( char(15),nID_EMPREGADO )   "
        //    + "      from [@xDB2].dbo.tblEMPREGADO_FUNCAO   ";

        //    if (xEmp == 1)
        //    {
        //        rCommand.CommandText = rCommand.CommandText + "WHERE nID_EMPR = @xIdCliente  ";
        //    }
        //    else if (xEmp == 2)
        //    {
        //        rCommand.CommandText = rCommand.CommandText + "WHERE nID_EMPR in (  select idpessoa from juridica where idjuridica = @xIdCliente or idjuridicapai = @xIdCliente ) ";
        //    }
        //    else if (xEmp == 3)
        //    {
        //        rCommand.CommandText = rCommand.CommandText + "WHERE nID_EMPR in (  select idpessoa from juridica where idgrupoempresa in ( select idgrupoempresa from juridica where idpessoa = @xIdCliente ) ) ";
        //    }
        //    else if (xEmp == 0)
        //    {
        //        rCommand.CommandText = rCommand.CommandText + "WHERE nID_EMPR is not null  ";
        //    }

        //    rCommand.CommandText = rCommand.CommandText + "      group by nId_Empregado  "
        //     + "     )  "
        //     + "  ) AS TX10 "
        //     + "  ON ( e.nID_EMPREGADO = TX10.nID_EMPREGADO )  "


        //     + "   left join   ( "
        //     + "      select nId_Empregado_Funcao, tNo_Func "
        //     + "	  from [@xDB2].dbo.TBLFunc_Empregado left join [xDB2].dbo.TBLFunc on ( TBLFunc_Empregado.nId_Func = TBLFunc.nId_Func ) "
        //     + " where TBLFunc_Empregado.nID_LAUD_TEC in "
        //     + "	  (  "
        //     + "	     select nId_Laud_Tec from [xDB2].dbo.TblLaudo_tec "
        //     + "		 where convert( char(15), nId_Empr ) + convert( char(10), hdt_laudo, 103 ) in "
        //     + "		 (   select convert( char(15), nId_Empr ) + convert( char(10), max(hdt_laudo), 103)  "
        //     + "		     from [@xDB2].dbo.tblLAUDO_TEC "
        //     + "			 group by nId_Empr     "
        //     + "          ) "
        //     + "	  )  "
        //     + "	)  as tx50   on tx50.nId_Empregado_Funcao = tx10.nId_Empregado_Funcao  "

        //     + "left join [@xDB2].dbo.tblsetor on  tx10.nID_SETOR = tblSETOR.nID_SETOR   "
        //     + "left join Funcao on tx10.nID_FUNCAO = Funcao.IdFuncao   "
        //     + "  left join Pessoa as p on ( e.nID_EMPR = p.IdPessoa ) "

        //     + "where idempregado in "
        //     + "( "

        //     + "SELECT nID_EMPREGADO  "
        //     + "FROM [@xDB2].dbo.tblEMPREGADO_FUNCAO  ";
        //    //strSQL.Append("WHERE nID_EMPR= " + xIdCliente.ToString() + "  ");


        //    if (xEmp == 1)
        //    {
        //        rCommand.CommandText = rCommand.CommandText + "WHERE nID_EMPR = @xIdCliente ";
        //    }
        //    else if (xEmp == 2)
        //    {
        //        rCommand.CommandText = rCommand.CommandText + "WHERE nID_EMPR in (  select idpessoa from juridica where idjuridica = @xIdCliente or idjuridicapai = @xIdCliente ) ";
        //    }
        //    else if (xEmp == 3)
        //    {
        //        rCommand.CommandText = rCommand.CommandText + "WHERE nID_EMPR in (  select idpessoa from juridica where idgrupoempresa in ( select idgrupoempresa from juridica where idpessoa = @xIdCliente ) ) ";
        //    }
        //    else if (xEmp == 0)
        //    {
        //        rCommand.CommandText = rCommand.CommandText + "WHERE nID_EMPR is not null  ";
        //    }



        //    // strSQL.Append("SELECT nID_EMPREGADO  ");
        //    // strSQL.Append("FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEMPREGADO_FUNCAO  ");
        //    // strSQL.Append("WHERE nID_EMPR= " + xIdCliente.ToString() + "  ");

        //    rCommand.CommandText = rCommand.CommandText + ") ";



        //    if (xExames == "C")
        //    {
        //        rCommand.CommandText = rCommand.CommandText + " and k.IdExameDicionario between 1 and 5 ";
        //    }
        //    else if (xExames == "P")
        //    {
        //        rCommand.CommandText = rCommand.CommandText + " and k.IdExameDicionario not between 1 and 5 ";
        //    }


        //    rCommand.CommandText = rCommand.CommandText + "and l.DataExame between convert( smalldatetime, @xD_Inicial, 103 ) and 	convert( smalldatetime, @xD_Final, 103 )  ";

        //    //strSQL.Append(" and IndResultado not in ( 0, 3 ) ");
        //    //strSQL.Append(" and IndResultado in ( @xResult ) ");
        //    rCommand.CommandText = rCommand.CommandText + " and @xResult like '%' + convert( char(1),IndResultado) + '%' ";

        //    rCommand.CommandText = rCommand.CommandText + "group by l.IdExameBase,l.IdEmpregado, e.tNo_Empg, e.tNO_CPF, e.tNo_Identidade, k.Nome, l.DataExame, tblSetor.tNO_STR_EMPR , Funcao.NomeFuncao, tx50.tNo_Func, l.IndResultado, z.NomeAbreviado, p.NomeAbreviado ";


        //    rCommand.CommandText = rCommand.CommandText + "	) ";
        //    rCommand.CommandText = rCommand.CommandText + "as tx1 ";
        //    rCommand.CommandText = rCommand.CommandText + "group by IdExameBase, Nome, tNo_Empg, tNO_CPF, tNo_Identidade, xDataExame, Setor1, Funcao1, GHE, IndResultado, Clinica, Empresa, Agendamento  ";
        //    rCommand.CommandText = rCommand.CommandText + "order by Empresa, tNo_Empg, xDataExame, Nome ";




        //    //m_ds = new DataSet("Result");

        //    using (SqlConnection cnn = new SqlConnection(Ilitera.Data.SQLServer.EntitySQLServer.GetConnection()))
        //    {
        //        cnn.Open();

        //        rCommand.Connection = cnn;
        //        //rCommand.CommandText = strSQL.ToString();

        //        SqlDataAdapter da;               


        //        da = new SqlDataAdapter(rCommand);
        //        da.Fill(m_ds, "Result");

        //        cnn.Close();

        //        da.Dispose();
        //    }

        //    return m_ds;

        //}





        public DataSet Gerar_DS_Relatorio_Exames_Empr_Sumarizado(int xIdCliente, string xD_Inicial, string xD_Final, int xEmp, string xExames, string xResultado)
        {
            //string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection();
            StringBuilder strSQL = new StringBuilder();

            //DataSet m_ds;


            DataTable table = new DataTable("Result");
            table.Columns.Add("Exame", Type.GetType("System.String"));
            table.Columns.Add("Empregado", Type.GetType("System.String"));
            table.Columns.Add("DataExame", Type.GetType("System.String"));
            table.Columns.Add("Empresa", Type.GetType("System.String"));
            table.Columns.Add("Data1", Type.GetType("System.String"));
            table.Columns.Add("Data2", Type.GetType("System.String"));
            table.Columns.Add("Setor", Type.GetType("System.String"));
            table.Columns.Add("Funcao", Type.GetType("System.String"));


            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);




            strSQL.Append("select  NomeAbreviado as Empresa, Nome as exame,");
            strSQL.Append("'" + xD_Inicial.Substring(0, 10) + "' as Data1, '" + xD_Final.Substring(0, 10) + "' as Data2, Exames as DataExame ");
            strSQL.Append("from ");
            strSQL.Append("( ");


            strSQL.Append("select  p.NomeAbreviado, k.Nome, count(*) as Exames ");
            strSQL.Append("from examebase as l  ");
            strSQL.Append("left join examedicionario as k on l.idexamedicionario =  ");
            strSQL.Append("k.idexamedicionario ");
            strSQL.Append("left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEmpregado as e on l.IdEmpregado = e.nId_Empregado ");

            strSQL.Append("  LEFT JOIN  ");
            strSQL.Append("  ( SELECT nId_Empregado, nID_FUNCAO, nID_SETOR    ");
            strSQL.Append("    from " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.TBLEMPREGADO_FUNCAO  ");
            strSQL.Append("    where convert( char(10),  hDt_Inicio, 103 ) + ' ' + convert( char(15),nID_EMPREGADO ) in  ");
            strSQL.Append("    ( select convert( char(10),  max(hDt_Inicio), 103 ) + ' ' + convert( char(15),nID_EMPREGADO )   ");
            strSQL.Append("      from " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEMPREGADO_FUNCAO   ");
            strSQL.Append("      group by nId_Empregado  ");
            strSQL.Append("     )  ");
            strSQL.Append("  ) AS TX10 ");
            strSQL.Append("  ON ( e.nID_EMPREGADO = TX10.nID_EMPREGADO )  ");
            strSQL.Append("  left join Pessoa as p on ( e.nID_EMPR = p.IdPessoa ) ");

            strSQL.Append("where idempregado in ");
            strSQL.Append("( ");

            strSQL.Append("SELECT nID_EMPREGADO  ");
            strSQL.Append("FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEMPREGADO_FUNCAO  ");

            if (xEmp == 1)
            {
                strSQL.Append("WHERE nID_EMPR= " + xIdCliente.ToString() + "  ");
            }
            else if (xEmp == 2)
            {
                strSQL.Append("WHERE nID_EMPR in (  select idpessoa from juridica where idjuridica = " + xIdCliente.ToString() + " or idjuridicapai = " + xIdCliente.ToString() + " ) ");
            }
            else if (xEmp == 3)
            {
                strSQL.Append("WHERE nID_EMPR in (  select idpessoa from juridica where idgrupoempresa in ( select idgrupoempresa from juridica where idpessoa = " + xIdCliente.ToString() + " ) ) ");
            }
            else if (xEmp == 0 || xEmp == 4)
            {
                strSQL.Append("WHERE nID_EMPR is not null  and nid_Empr in ( select idpessoa from pessoa where isinativo=0)  ");
            }


            strSQL.Append(") ");


            if (xExames == "C")
            {
                strSQL.Append(" and k.IdExameDicionario between 1 and 5 ");
            }
            else if (xExames == "P")
            {
                strSQL.Append(" and k.IdExameDicionario not between 1 and 5 ");
            }

            strSQL.Append("and l.DataExame between convert( smalldatetime, '" + xD_Inicial.Substring(0, 10) + "', 103 ) and 	convert( smalldatetime, '" + xD_Final.Substring(0, 10) + " 23:59', 103 )  ");

            //strSQL.Append(" and IndResultado not in ( 0, 3 ) ");
            strSQL.Append(" and IndResultado in " + xResultado + " ");

            strSQL.Append("group by p.NomeAbreviado, k.Nome ");

            if (xEmp != 1)
            {
                strSQL.Append("union ");


                strSQL.Append("select  'TOTAL DE EXAMES' as  NomeAbreviado, k.Nome, count(*) as Exames ");
                strSQL.Append("from examebase as l  ");
                strSQL.Append("left join examedicionario as k on l.idexamedicionario =  ");
                strSQL.Append("k.idexamedicionario ");
                strSQL.Append("left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEmpregado as e on l.IdEmpregado = e.nId_Empregado ");

                strSQL.Append("  LEFT JOIN  ");
                strSQL.Append("  ( SELECT nId_Empregado, nID_FUNCAO, nID_SETOR    ");
                strSQL.Append("    from " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.TBLEMPREGADO_FUNCAO  ");
                strSQL.Append("    where convert( char(10),  hDt_Inicio, 103 ) + ' ' + convert( char(15),nID_EMPREGADO ) in  ");
                strSQL.Append("    ( select convert( char(10),  max(hDt_Inicio), 103 ) + ' ' + convert( char(15),nID_EMPREGADO )   ");
                strSQL.Append("      from " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEMPREGADO_FUNCAO   ");
                strSQL.Append("      group by nId_Empregado  ");
                strSQL.Append("     )  ");
                strSQL.Append("  ) AS TX10 ");
                strSQL.Append("  ON ( e.nID_EMPREGADO = TX10.nID_EMPREGADO )  ");
                strSQL.Append("  left join Pessoa as p on ( e.nID_EMPR = p.IdPessoa ) ");

                strSQL.Append("where idempregado in ");
                strSQL.Append("( ");

                strSQL.Append("SELECT nID_EMPREGADO  ");
                strSQL.Append("FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEMPREGADO_FUNCAO  ");

                if (xEmp == 1)
                {
                    strSQL.Append("WHERE nID_EMPR= " + xIdCliente.ToString() + "  ");
                }
                else if (xEmp == 2)
                {
                    strSQL.Append("WHERE nID_EMPR in (  select idpessoa from juridica where idjuridica = " + xIdCliente.ToString() + " or idjuridicapai = " + xIdCliente.ToString() + " ) ");
                }
                else if (xEmp == 3)
                {
                    strSQL.Append("WHERE nID_EMPR in (  select idpessoa from juridica where idgrupoempresa in ( select idgrupoempresa from juridica where idpessoa = " + xIdCliente.ToString() + " ) ) ");
                }
                else if (xEmp == 0 || xEmp == 4)
                {
                    strSQL.Append("WHERE nID_EMPR is not null  and nid_Empr in ( select idpessoa from pessoa where isinativo=0) ");
                }

                strSQL.Append(") ");


                if (xExames == "C")
                {
                    strSQL.Append(" and k.IdExameDicionario between 1 and 5 ");
                }
                else if (xExames == "P")
                {
                    strSQL.Append(" and k.IdExameDicionario not between 1 and 5 ");
                }

                strSQL.Append("and l.DataExame between convert( smalldatetime, '" + xD_Inicial.Substring(0, 10) + "', 103 ) and 	convert( smalldatetime, '" + xD_Final.Substring(0, 10) + " 23:59', 103 )  ");

                //strSQL.Append(" and IndResultado not in ( 0, 3 ) ");
                strSQL.Append(" and IndResultado in " + xResultado + " ");

                strSQL.Append("group by k.Nome ");
            }


            strSQL.Append("	) ");
            strSQL.Append("as tx1 ");
            strSQL.Append("group by NomeAbreviado, Nome, Exames  ");
            strSQL.Append("order by NomeAbreviado, Nome ");



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



        public DataSet Gerar_DS_Relatorio_Exames_Empr_Sumarizado2(int xIdCliente, string xD_Inicial, string xD_Final, int xEmp, string xExames, string xResultado)
        {
            //string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection();
            StringBuilder strSQL = new StringBuilder();

            //DataSet m_ds;


            DataTable table = new DataTable("Result");
            table.Columns.Add("tNO_FUNC", Type.GetType("System.String"));
            table.Columns.Add("tNO_EPI", Type.GetType("System.String"));
            table.Columns.Add("tNO_X", Type.GetType("System.String"));
            table.Columns.Add("tNO_TOT", Type.GetType("System.Int32"));

            //table.Columns.Add("Exame", Type.GetType("System.String"));
            //table.Columns.Add("Empregado", Type.GetType("System.String"));
            //table.Columns.Add("DataExame", Type.GetType("System.String"));
            //table.Columns.Add("Empresa", Type.GetType("System.String"));
            table.Columns.Add("Data1", Type.GetType("System.String"));
            table.Columns.Add("Data2", Type.GetType("System.String"));
            //table.Columns.Add("Setor", Type.GetType("System.String"));
            //table.Columns.Add("Funcao", Type.GetType("System.String"));


            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);




            strSQL.Append("select  NomeAbreviado as tNO_FUNC, Nome as tNO_EPI, Exames as tNO_X,  Exames as tNO_TOT, ");
            strSQL.Append("'" + xD_Inicial.Substring(0, 10) + "' as Data1, '" + xD_Final.Substring(0, 10) + "' as Data2, Exames as DataExame ");
            strSQL.Append("from ");
            strSQL.Append("( ");


            strSQL.Append("select  p.NomeAbreviado, k.Nome, count(*) as Exames ");
            strSQL.Append("from examebase as l  ");
            strSQL.Append("left join examedicionario as k on l.idexamedicionario =  ");
            strSQL.Append("k.idexamedicionario ");
            strSQL.Append("left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEmpregado as e on l.IdEmpregado = e.nId_Empregado ");

            strSQL.Append("  LEFT JOIN  ");
            strSQL.Append("  ( SELECT nId_Empregado, nID_FUNCAO, nID_SETOR    ");
            strSQL.Append("    from " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.TBLEMPREGADO_FUNCAO  ");
            strSQL.Append("    where convert( char(10),  hDt_Inicio, 103 ) + ' ' + convert( char(15),nID_EMPREGADO ) in  ");
            strSQL.Append("    ( select convert( char(10),  max(hDt_Inicio), 103 ) + ' ' + convert( char(15),nID_EMPREGADO )   ");
            strSQL.Append("      from " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEMPREGADO_FUNCAO   ");
            strSQL.Append("      group by nId_Empregado  ");
            strSQL.Append("     )  ");
            strSQL.Append("  ) AS TX10 ");
            strSQL.Append("  ON ( e.nID_EMPREGADO = TX10.nID_EMPREGADO )  ");
            strSQL.Append("  left join Pessoa as p on ( e.nID_EMPR = p.IdPessoa ) ");

            strSQL.Append("where idempregado in ");
            strSQL.Append("( ");

            strSQL.Append("SELECT nID_EMPREGADO  ");
            strSQL.Append("FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEMPREGADO_FUNCAO  ");

            if (xEmp == 1)
            {
                strSQL.Append("WHERE nID_EMPR= " + xIdCliente.ToString() + "  ");
            }
            else if (xEmp == 2)
            {
                strSQL.Append("WHERE nID_EMPR in (  select idpessoa from juridica where idjuridica = " + xIdCliente.ToString() + " or idjuridicapai = " + xIdCliente.ToString() + " ) ");
            }
            else if (xEmp == 3)
            {
                strSQL.Append("WHERE nID_EMPR in (  select idpessoa from juridica where idgrupoempresa in ( select idgrupoempresa from juridica where idpessoa = " + xIdCliente.ToString() + " ) ) ");
            }
            else if (xEmp == 0 || xEmp == 4)
            {
                strSQL.Append("WHERE nID_EMPR is not null  and nid_Empr in ( select idpessoa from pessoa where isinativo=0)  ");
            }


            strSQL.Append(") ");


            if (xExames == "C")
            {
                strSQL.Append(" and k.IdExameDicionario between 1 and 5 ");
            }
            else if (xExames == "P")
            {
                strSQL.Append(" and k.IdExameDicionario not between 1 and 5 ");
            }

            strSQL.Append("and l.DataExame between convert( smalldatetime, '" + xD_Inicial.Substring(0, 10) + "', 103 ) and 	convert( smalldatetime, '" + xD_Final.Substring(0, 10) + " 23:59', 103 )  ");

            //strSQL.Append(" and IndResultado not in ( 0, 3 ) ");
            strSQL.Append(" and IndResultado in " + xResultado + " ");

            strSQL.Append("group by p.NomeAbreviado, k.Nome ");

            //if (xEmp != 1)
            //{
            //    strSQL.Append("union ");


            //    strSQL.Append("select  'TOTAL DE EXAMES' as  NomeAbreviado, k.Nome, count(*) as Exames ");
            //    strSQL.Append("from examebase as l  ");
            //    strSQL.Append("left join examedicionario as k on l.idexamedicionario =  ");
            //    strSQL.Append("k.idexamedicionario ");
            //    strSQL.Append("left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEmpregado as e on l.IdEmpregado = e.nId_Empregado ");

            //    strSQL.Append("  LEFT JOIN  ");
            //    strSQL.Append("  ( SELECT nId_Empregado, nID_FUNCAO, nID_SETOR    ");
            //    strSQL.Append("    from " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.TBLEMPREGADO_FUNCAO  ");
            //    strSQL.Append("    where convert( char(10),  hDt_Inicio, 103 ) + ' ' + convert( char(15),nID_EMPREGADO ) in  ");
            //    strSQL.Append("    ( select convert( char(10),  max(hDt_Inicio), 103 ) + ' ' + convert( char(15),nID_EMPREGADO )   ");
            //    strSQL.Append("      from " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEMPREGADO_FUNCAO   ");
            //    strSQL.Append("      group by nId_Empregado  ");
            //    strSQL.Append("     )  ");
            //    strSQL.Append("  ) AS TX10 ");
            //    strSQL.Append("  ON ( e.nID_EMPREGADO = TX10.nID_EMPREGADO )  ");
            //    strSQL.Append("  left join Pessoa as p on ( e.nID_EMPR = p.IdPessoa ) ");

            //    strSQL.Append("where idempregado in ");
            //    strSQL.Append("( ");

            //    strSQL.Append("SELECT nID_EMPREGADO  ");
            //    strSQL.Append("FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEMPREGADO_FUNCAO  ");

            //    if (xEmp == 1)
            //    {
            //        strSQL.Append("WHERE nID_EMPR= " + xIdCliente.ToString() + "  ");
            //    }
            //    else if (xEmp == 2)
            //    {
            //        strSQL.Append("WHERE nID_EMPR in (  select idpessoa from juridica where idjuridica = " + xIdCliente.ToString() + " or idjuridicapai = " + xIdCliente.ToString() + " ) ");
            //    }
            //    else if (xEmp == 3)
            //    {
            //        strSQL.Append("WHERE nID_EMPR in (  select idpessoa from juridica where idgrupoempresa in ( select idgrupoempresa from juridica where idpessoa = " + xIdCliente.ToString() + " ) ) ");
            //    }
            //    else if (xEmp == 0 || xEmp == 4)
            //    {
            //        strSQL.Append("WHERE nID_EMPR is not null ");
            //    }

            //    strSQL.Append(") ");


            //    if (xExames == "C")
            //    {
            //        strSQL.Append(" and k.IdExameDicionario between 1 and 5 ");
            //    }
            //    else if (xExames == "P")
            //    {
            //        strSQL.Append(" and k.IdExameDicionario not between 1 and 5 ");
            //    }

            //    strSQL.Append("and l.DataExame between convert( smalldatetime, '" + xD_Inicial.Substring(0, 10) + "', 103 ) and 	convert( smalldatetime, '" + xD_Final.Substring(0, 10) + " 23:59', 103 )  ");

            //    //strSQL.Append(" and IndResultado not in ( 0, 3 ) ");
            //    strSQL.Append(" and IndResultado in " + xResultado + " ");

            //    strSQL.Append("group by k.Nome ");
            //}


            strSQL.Append("	) ");
            strSQL.Append("as tx1 ");
            strSQL.Append("group by NomeAbreviado, Nome, Exames  ");
            strSQL.Append("order by NomeAbreviado, Nome ");



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




        public DataSet Gerar_DS_Relatorio_Mailing(int xIdCliente, string xD_Inicial, string xD_Final, int xEmp, int xAttach, int xSemAgendamento, int xSemEnvio, int xSemResultado)
        {
            //string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection();
            StringBuilder strSQL = new StringBuilder();

            //DataSet m_ds;


            DataTable table = new DataTable("Result");
            table.Columns.Add("Data_Inicial", Type.GetType("System.String"));
            table.Columns.Add("Data_Final", Type.GetType("System.String"));
            table.Columns.Add("Data_Mailing", Type.GetType("System.DateTime"));
            table.Columns.Add("Empresa", Type.GetType("System.String"));
            table.Columns.Add("Colaborador", Type.GetType("System.String"));
            table.Columns.Add("DataEnvio", Type.GetType("System.String"));
            table.Columns.Add("Data_Planejada", Type.GetType("System.String"));
            table.Columns.Add("Numero_Mailing", Type.GetType("System.Int16"));
            table.Columns.Add("Status_Envio", Type.GetType("System.String"));
            table.Columns.Add("eMail_Envio", Type.GetType("System.String"));
            table.Columns.Add("Status_Exame", Type.GetType("System.String"));
            table.Columns.Add("Resultado_Exame", Type.GetType("System.String"));
            table.Columns.Add("Data_Exame", Type.GetType("System.String"));
            table.Columns.Add("Data_Digitalizacao", Type.GetType("System.String"));
            table.Columns.Add("CPF", Type.GetType("System.String"));


            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);


            strSQL.Append("select distinct '" + xD_Inicial + "' as Data_Inicial, '" + xD_Final + "' as Data_Final, a.Data_Envio as Data_Mailing, c.NomeAbreviado as Empresa, b.tNo_Empg as Colaborador, convert(char(10), a.Data_Envio, 103) as DataEnvio, convert(char(10), a.Data_exame_Planejado, 103) as Data_Planejada, a.Numero_Mailing, b.tNO_CPF as CPF, ");
            strSQL.Append("        case when email_Envio = ''   then 'Não Enviado' ");
            strSQL.Append("             when email_envio <> '' then 'Enviado' ");
            strSQL.Append("        end as Status_Envio, eMail_Envio, ");
            strSQL.Append("		case when d.IdExameBase is null then    'Sem Exame Criado' ");
            strSQL.Append("             when d.IdExameBase is not null then 'Exame Criado' ");
            strSQL.Append("        end as Status_Exame, ");
            strSQL.Append("		case when d.IndResultado = 0 then 'Não Realizado' ");
            strSQL.Append("             when d.IndResultado = 1 then 'Apto' ");
            strSQL.Append("             when d.IndResultado = 1 then 'Inapto' ");
            strSQL.Append("             when d.IndResultado = 3 then 'Em espera' ");
            strSQL.Append("        end as Resultado_Exame, ");
            strSQL.Append("        convert(char(10), d.DataExame, 103) as Data_Exame, convert(char(10), e.DataDigitalizacao, 103) as Data_Digitalizacao ");
            strSQL.Append("from Envio_Email_Mailing as a ");
            strSQL.Append("left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblempregado as b on(a.nId_Empregado = b.nId_Empregado) ");
            strSQL.Append("left join pessoa as c on(a.nid_Empr = c.IdPessoa) ");
            strSQL.Append("left join examebase as d on(a.nId_Empregado = d.IdEmpregado and IdExameDicionario = 4 and datediff(day, a.data_exame_planejado, d.DataExame) between - 80 and 120) ");
            strSQL.Append("left join prontuariodigital as e on(d.IdExameBase = e.IdExameBase) ");

            strSQL.Append("where a.nid_empregado in ");
            strSQL.Append("( ");

            strSQL.Append("SELECT nID_EMPREGADO  ");
            strSQL.Append("FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEMPREGADO_FUNCAO  ");

            if (xEmp == 1)
            {
                strSQL.Append("WHERE nID_EMPR= " + xIdCliente.ToString() + "  ");
            }
            else if (xEmp == 2)
            {
                strSQL.Append("WHERE nID_EMPR in (  select idpessoa from juridica where idjuridica = " + xIdCliente.ToString() + " or idjuridicapai = " + xIdCliente.ToString() + " ) ");
            }
            else if (xEmp == 3)
            {
                strSQL.Append("WHERE nID_EMPR in (  select idpessoa from juridica where idgrupoempresa in ( select idgrupoempresa from juridica where idpessoa = " + xIdCliente.ToString() + " ) ) ");
            }


            strSQL.Append(") ");


            if (xAttach == 1)
            {
                strSQL.Append(" and ( e.DataDigitalizacao is null or datediff( day, d.DataExame, e.DataDigitalizacao ) > 40 ) ");
            }

            if (xSemAgendamento == 1)
            {
                strSQL.Append(" and d.IdExameBase is null ");
            }

            if (xSemEnvio == 1)
            {
                strSQL.Append(" and ( a.eMail_Envio is null or a.eMail_Envio = '' ) ");
            }

            if (xSemResultado == 1)
            {
                strSQL.Append(" and(d.IndResultado in (0, 3) and datediff(day, d.DataExame, getdate() ) > 40 ) ");
            }

            strSQL.Append("and a.Data_Envio between convert( smalldatetime, '" + xD_Inicial.Substring(0, 10) + "', 103 ) and 	convert( smalldatetime, '" + xD_Final.Substring(0, 10) + " 23:59', 103 )  ");

            strSQL.Append("order by c.NomeAbreviado, b.tNo_Empg");



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



        public Boolean Buscar_Planejamento_Exame_Colaborador(Int32 xIdEmpregado, Int32 xIdExameDicionario, string xData_Exame, int zDias_Desconsiderar)
        {


            //string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection();
            //StringBuilder strSQL = new StringBuilder();

            //DataSet m_ds;


            DataTable table = new DataTable("Result");
            table.Columns.Add("DataVencimento", Type.GetType("System.DateTime"));


            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);


            SqlCommand rCommand = new SqlCommand();

            rCommand.Parameters.Add("@xIdEmpregado", SqlDbType.NChar).Value = xIdEmpregado.ToString();
            rCommand.Parameters.Add("@xIdExameDicionario", SqlDbType.NChar).Value = xIdExameDicionario.ToString();
            rCommand.Parameters.Add("@xData_Exame", SqlDbType.NChar).Value = xData_Exame;

            //strSQL.Append("select top 1 DataVencimento from qryexameplanejamento ");
            //strSQL.Append("where idpcmso in ");
            //strSQL.Append("( select idpcmso from qryultimopcmso ) ");
            //strSQL.Append("and IdExameDicionario = " + xIdExameDicionario + " " );
            //strSQL.Append("and IdEmpregado = " + xIdEmpregado + " " );
            //strSQL.Append("and DataVencimento > convert( smalldatetime, '" + xData_Exame + "', 103 ) ");

            if (zDias_Desconsiderar == 0)
            {
                rCommand.CommandText = "select top 1 DataVencimento from qryexameplanejamento "
                + "where idpcmso in "
                + "( select idpcmso from qryultimopcmso ) "
                + "and IdExameDicionario = @xIdExameDicionario "
                + "and IdEmpregado = @xIdEmpregado "
                + "and DataVencimento > convert( smalldatetime, @xData_Exame, 103 ) ";
            }
            else
            {
                rCommand.CommandText = "select top 1 DataVencimento from qryexameplanejamento "
                + "where idpcmso in "
                + "( select idpcmso from qryultimopcmso ) "
                + "and IdExameDicionario = @xIdExameDicionario "
                + "and IdEmpregado = @xIdEmpregado "
                + "and DataVencimento > dateadd( dd, " + zDias_Desconsiderar + ", convert( smalldatetime, @xData_Exame, 103 ) ) ";
            }
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

            if (m_ds.Tables[0].Rows.Count > 0)
                return false;
            else
                return true;

        }



        public String Buscar_Data_Planejamento_Exame_Colaborador(Int32 xIdEmpregado, Int32 xIdExameDicionario, string xData_Exame)
        {


            //string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection();
            //StringBuilder strSQL = new StringBuilder();

            //DataSet m_ds;


            DataTable table = new DataTable("Result");
            table.Columns.Add("zData", Type.GetType("System.String"));

            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);

            SqlCommand rCommand = new SqlCommand();

            rCommand.Parameters.Add("@xIdEmpregado", SqlDbType.NChar).Value = xIdEmpregado.ToString();
            rCommand.Parameters.Add("@xIdExameDicionario", SqlDbType.NChar).Value = xIdExameDicionario.ToString();



            rCommand.CommandText = "select top 1 "
            + "case  "
            + "when DataUltima is null then convert( char(10),case when DataVencimento is not null then DataVencimento when DataVencimento is null then DataProxima end,103 ) + '|' + convert( char(10), DataAdmissao, 103 ) "
            + "when DataUltima is not null then convert( char(10),case when DataVencimento is not null then DataVencimento when DataVencimento is null then DataProxima end,103 ) + '|' + convert( char(10), DataUltima, 103 ) "
            + "end "
            + "as zData from qryexameplanejamento "
            + "where idpcmso in "
            + "( select idpcmso from qryultimopcmso ) "
            + "and IdExameDicionario = @xIdExameDicionario "
            + "and IdEmpregado = @xIdEmpregado "
            + "order by DataProxima desc";


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

            if (m_ds.Tables[0].Rows.Count > 0)
                return m_ds.Tables[0].Rows[0][0].ToString();
            else
                return "          |          ";

        }








        public DataSet Gerar_DS_Relatorio_Guias(int xIdCliente, string xD_Inicial, string xD_Final, string xConsiderar, Int16 xEmp)
        {
            //string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection();
            StringBuilder strSQL = new StringBuilder();

            //DataSet m_ds;


            DataTable table = new DataTable("Result");
            table.Columns.Add("CNPJ", Type.GetType("System.String"));
            table.Columns.Add("Exame", Type.GetType("System.String"));
            table.Columns.Add("Empregado", Type.GetType("System.String"));
            table.Columns.Add("DataExame", Type.GetType("System.String"));
            table.Columns.Add("Empresa", Type.GetType("System.String"));
            table.Columns.Add("Tipo", Type.GetType("System.String"));
            table.Columns.Add("Data1", Type.GetType("System.String"));
            table.Columns.Add("Data2", Type.GetType("System.String"));
            table.Columns.Add("Data_Criacao", Type.GetType("System.String"));
            table.Columns.Add("Registros", Type.GetType("System.Int16"));
            table.Columns.Add("CPF", Type.GetType("System.String"));
            table.Columns.Add("Funcao", Type.GetType("System.String"));
            table.Columns.Add("Matricula", Type.GetType("System.String"));

            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);

            strSQL.Append(" select distinct c.NomeAbreviado + '  CNPJ: ' + c.NomeCodigo as CNPJ, b.tCod_Empr as Matricula, b.tno_empg as Empregado, b.tNO_CPF as CPF, Data_hora_Criacao, ");
            strSQL.Append("        case Tipo  ");
            strSQL.Append("        When 'R' then 'Retorno' ");
            strSQL.Append("        When 'A' then 'Admissional' ");
            strSQL.Append("        When 'D' then 'Demissional' ");
            strSQL.Append("        When 'O' then 'Outros' ");
            strSQL.Append("        When 'P' then 'Periódico' ");
            strSQL.Append("        When 'M' then 'Mudança de Função' ");
            strSQL.Append("        end ");
            strSQL.Append("        as Tipo, ");
            strSQL.Append("       case ");
            strSQL.Append("           when ( Data is null or Data = '' ) and ( Hora is not null and Hora <> '' ) then 'Horário : ' + Hora ");
            strSQL.Append("           when ( Data is not null and Data <> '' ) and ( Hora is null or Hora = '' ) then Data ");
            strSQL.Append("           when ( Data is not null and Data <> '' ) and ( Hora is not null and Hora <> '' ) then Data + '  Horário : ' + Hora ");
            strSQL.Append("           when ( Data is null or Data = '' ) and ( Hora is null or Hora = '' ) then '' ");
            strSQL.Append("        end    ");
            strSQL.Append("        as DataExame,  ");
            strSQL.Append(" replace(replace(Exames, '/n', ' ' ), '|', ' ' ) as Exame, Clinica as Empresa, ");
            strSQL.Append("        '" + xD_Inicial + "' as Data1, '" + xD_Final + "' as Data2, convert( char(10), Data_Hora_Criacao, 103 ) as Data_Criacao, 0 as Registros, e.NomeAbreviado as Funcao   ");   //usei campo já existente no report Funcao, para por o usuário
            strSQL.Append(" from guia_encaminhamento as a left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblempregado as b ");
            strSQL.Append(" on ( a.Id_Empregado = b.nId_Empregado ) ");
            strSQL.Append(" left join pessoa as c on ( a.Id_Empresa = c.IdPessoa ) ");
            strSQL.Append(" left join usuario as d on(a.Id_Usuario = d.IdUsuario) ");
            strSQL.Append(" left join pessoa as e on(d.idpessoa = e.idpessoa) ");


            //strSQL.Append(" where a.Id_Empresa = " + xIdCliente.ToString() + " " );

            if (xEmp == 1)
            {
                strSQL.Append(" where a.Id_Empresa = " + xIdCliente.ToString() + " ");
            }
            else if (xEmp == 2)
            {
                strSQL.Append(" where a.Id_Empresa in (  select idpessoa from juridica where idjuridica = " + xIdCliente.ToString() + " or idjuridicapai = " + xIdCliente.ToString() + " ) ");
            }
            else if (xEmp == 3)
            {
                strSQL.Append(" where a.Id_Empresa in (  select idpessoa from juridica where idgrupoempresa in ( select idgrupoempresa from juridica where idpessoa = " + xIdCliente.ToString() + " ) ) ");
            }

            strSQL.Append(" and Data_hora_Criacao between convert( smalldatetime, '" + xD_Inicial + "', 103 ) and convert( smalldatetime, '" + xD_Final + "', 103 ) and b.tNo_Empg is not null ");
            strSQL.Append(" order by c.NomeAbreviado + '  CNPJ: ' + c.NomeCodigo , b.tno_Empg, Data_Hora_Criacao        ");


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




        public DataSet Gerar_DS_Relatorio_Guias_Sum(int xIdCliente, string xD_Inicial, string xD_Final, string xConsiderar, Int16 xEmp)
        {
            //string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection();
            StringBuilder strSQL = new StringBuilder();

            //DataSet m_ds;


            DataTable table = new DataTable("Result");
            table.Columns.Add("CNPJ", Type.GetType("System.String"));
            table.Columns.Add("Exame", Type.GetType("System.String"));
            table.Columns.Add("Empregado", Type.GetType("System.String"));
            table.Columns.Add("DataExame", Type.GetType("System.String"));
            table.Columns.Add("Empresa", Type.GetType("System.String"));
            table.Columns.Add("Tipo", Type.GetType("System.String"));
            table.Columns.Add("Data1", Type.GetType("System.String"));
            table.Columns.Add("Data2", Type.GetType("System.String"));
            table.Columns.Add("Data_Criacao", Type.GetType("System.String"));
            table.Columns.Add("Registros", Type.GetType("System.Int16"));

            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);

            strSQL.Append("select CNPJ, Tipo, Data1, Data2, count(*) as Registros ");
            strSQL.Append("from ");
            strSQL.Append("( ");
            strSQL.Append(" select distinct c.NomeAbreviado + '  CNPJ: ' + c.NomeCodigo as CNPJ, b.tno_empg as Empregado, Data_hora_Criacao, ");
            strSQL.Append("        case Tipo  ");
            strSQL.Append("        When 'R' then 'Retorno' ");
            strSQL.Append("        When 'A' then 'Admissional' ");
            strSQL.Append("        When 'D' then 'Demissional' ");
            strSQL.Append("        When 'O' then 'Outros' ");
            strSQL.Append("        When 'P' then 'Periódico' ");
            strSQL.Append("        When 'M' then 'Mudança de Função' ");
            strSQL.Append("        end ");
            strSQL.Append("        as Tipo, ");
            strSQL.Append("       case ");
            strSQL.Append("           when ( Data is null or Data = '' ) and ( Hora is not null and Hora <> '' ) then 'Horário : ' + Hora ");
            strSQL.Append("           when ( Data is not null and Data <> '' ) and ( Hora is null or Hora = '' ) then Data ");
            strSQL.Append("           when ( Data is not null and Data <> '' ) and ( Hora is not null and Hora <> '' ) then Data + '  Horário : ' + Hora ");
            strSQL.Append("           when ( Data is null or Data = '' ) and ( Hora is null or Hora = '' ) then '' ");
            strSQL.Append("        end    ");
            strSQL.Append("        as DataExame,  ");
            strSQL.Append(" replace(replace(Exames, '/n', ' ' ), '|', ' ' ) as Exame, Clinica as Empresa, ");
            strSQL.Append("        '" + xD_Inicial + "' as Data1, '" + xD_Final + "' as Data2, convert( char(10), Data_Hora_Criacao, 103 ) as Data_Criacao, 0 as Registros ");
            strSQL.Append(" from guia_encaminhamento as a left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblempregado as b ");
            strSQL.Append(" on ( a.Id_Empregado = b.nId_Empregado ) ");
            strSQL.Append(" left join pessoa as c on ( a.Id_Empresa = c.IdPessoa ) ");


            //strSQL.Append(" where a.Id_Empresa = " + xIdCliente.ToString() + " " );

            if (xEmp == 1)
            {
                strSQL.Append(" where a.Id_Empresa = " + xIdCliente.ToString() + " ");
            }
            else if (xEmp == 2)
            {
                strSQL.Append(" where a.Id_Empresa in (  select idpessoa from juridica where idjuridica = " + xIdCliente.ToString() + " or idjuridicapai = " + xIdCliente.ToString() + " ) ");
            }
            else if (xEmp == 3)
            {
                strSQL.Append(" where a.Id_Empresa in (  select idpessoa from juridica where idgrupoempresa in ( select idgrupoempresa from juridica where idpessoa = " + xIdCliente.ToString() + " ) ) ");
            }

            strSQL.Append(" and Data_hora_Criacao between convert( smalldatetime, '" + xD_Inicial + "', 103 ) and convert( smalldatetime, '" + xD_Final + "', 103 ) and b.tNo_Empg is not null ");

            strSQL.Append(" ) as tx99 ");
            strSQL.Append(" group by CNPJ, Tipo, Data1, Data2 ");
            strSQL.Append(" order by CNPJ, tipo ");


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


        public DataSet Gerar_DS_Relatorio_Guias_Bloqueio(int xIdCliente, string xD_Inicial, string xD_Final, Int16 xEmp, string xTipoRel)
        {
            //string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection();
            StringBuilder strSQL = new StringBuilder();

            //DataSet m_ds;


            DataTable table = new DataTable("Result");
            table.Columns.Add("Exame", Type.GetType("System.String"));
            table.Columns.Add("Empregado", Type.GetType("System.String"));
            table.Columns.Add("DataExame", Type.GetType("System.String"));
            table.Columns.Add("Empresa", Type.GetType("System.String"));
            table.Columns.Add("Tipo", Type.GetType("System.String"));
            table.Columns.Add("Data1", Type.GetType("System.String"));
            table.Columns.Add("Data2", Type.GetType("System.String"));
            table.Columns.Add("CNPJ", Type.GetType("System.String"));
            table.Columns.Add("RG", Type.GetType("System.String"));
            table.Columns.Add("Data_Criacao", Type.GetType("System.String"));
            table.Columns.Add("Retorno", Type.GetType("System.String"));
            table.Columns.Add("Registros", Type.GetType("System.Int16"));
            table.Columns.Add("Afastamento", Type.GetType("System.String"));
            table.Columns.Add("Tipo_Afastamento", Type.GetType("System.String"));
            table.Columns.Add("CPF", Type.GetType("System.String"));

            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);

            if (xTipoRel == "S")
            {
                strSQL.Append(" select CNPJ, Data1, Data2, count(*) as Registros,  Tipo_Afastamento ");
                strSQL.Append(" from ( ");
            }

            strSQL.Append(" select distinct 'Relatório ASO Gerados após bloqueio' as Tipo_Afastamento, e.NomeAbreviado + '   CNPJ: ' + e.NomeCodigo as CNPJ, 'RG ' + b.tNo_Identidade as RG, b.tno_empg as Empregado, tno_CPF as CPF, Data_hora_Criacao, ");
            strSQL.Append("        case Tipo  ");
            strSQL.Append("        When 'R' then 'Retorno' ");
            strSQL.Append("        When 'A' then 'Admissional' ");
            strSQL.Append("        When 'D' then 'Demissional' ");
            strSQL.Append("        When 'O' then 'Outros' ");
            strSQL.Append("        When 'P' then 'Periódico' ");
            strSQL.Append("        When 'M' then 'Mudança de Função' ");
            strSQL.Append("        end ");
            strSQL.Append("        as Tipo, ");
            strSQL.Append("       case ");
            strSQL.Append("           when ( Data is null or Data = '' ) and ( Hora is not null and Hora <> '' ) then 'Horário : ' + Hora ");
            strSQL.Append("           when ( Data is not null and Data <> '' ) and ( Hora is null or Hora = '' ) then Data ");
            strSQL.Append("           when ( Data is not null and Data <> '' ) and ( Hora is not null and Hora <> '' ) then Data + '  Horário : ' + Hora ");
            strSQL.Append("           when ( Data is null or Data = '' ) and ( Hora is null or Hora = '' ) then '' ");
            strSQL.Append("        end    ");
            strSQL.Append("        as DataExame,  ");
            strSQL.Append(" d.NomeAbreviado as Exame, Clinica as Empresa, ");
            strSQL.Append("        '" + xD_Inicial + "' as Data1, '" + xD_Final + "' as Data2, convert( char(10), Data_Hora_Criacao, 103 ) as Data_Criacao, 0 as Registros,  ");

            //utilizar de preferência datas salvas na tabela de guia de encaminhamento
            strSQL.Append(" case when ( a.vencimento_na_geracao is null ) then convert( char(10), max(f.DataVencimento), 103 )  ");
            strSQL.Append("      when ( a.vencimento_na_geracao is not null ) then convert( char(10), max(a.vencimento_na_geracao), 103 ) ");
            strSQL.Append(" end as Retorno,  ");
            strSQL.Append(" case when ( a.Data_Ultimo_Exame_na_Geracao  is null ) then convert( char(10), max(f.DataUltima), 103 )  ");
            strSQL.Append("      when ( a.Data_Ultimo_Exame_na_Geracao  is not null ) then convert( char(10),max(a.Data_Ultimo_Exame_na_Geracao), 103 ) ");
            strSQL.Append(" end as Afastamento   ");

            strSQL.Append(" from guia_encaminhamento as a left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblempregado as b ");
            strSQL.Append(" on ( a.Id_Empregado = b.nId_Empregado ) ");
            strSQL.Append(" left join Usuario as c on a.Id_Usuario = c.IdUsuario ");
            strSQL.Append(" left join pessoa as D on c.IdPessoa = d.IdPessoa ");
            strSQL.Append(" left join pessoa as E on a.Id_Empresa = e.IdPessoa ");
            strSQL.Append(" left join qryexameplanejamento as f on ( a.Id_Empregado = f.IdEmpregado ) "); // and f.NomeExame = 'Periódico' ) ");

            //vai exibir somente os que tiverem exame correspondente salvo.  Se excluiu exame, não aparece no relatório
            strSQL.Append(" join examebase as g on ( a.Id_Empregado = g.IdEmpregado and convert( smalldatetime,a.Data,103 ) = g.DataExame ) "); // and g.IdExameDicionario = 4 ) ");

            if (xEmp == 1)
            {
                strSQL.Append(" where a.Id_Empresa = " + xIdCliente.ToString() + " ");
            }
            else if (xEmp == 2)
            {
                strSQL.Append(" where a.Id_Empresa in (  select idpessoa from juridica where idjuridica = " + xIdCliente.ToString() + " or idjuridicapai = " + xIdCliente.ToString() + " ) ");
            }
            else if (xEmp == 3)
            {
                strSQL.Append(" where a.Id_Empresa in (  select idpessoa from juridica where idgrupoempresa in ( select idgrupoempresa from juridica where idpessoa = " + xIdCliente.ToString() + " ) ) ");
            }




            strSQL.Append(" and convert( smalldatetime,Data, 103 ) between  convert( smalldatetime, '" + xD_Inicial + "', 103 ) and convert( smalldatetime, '" + xD_Final + "', 103 ) and Aviso_Bloqueio = 'S' and b.tno_Empg is not null ");
            strSQL.Append(" group by e.NomeAbreviado, e.NomeCodigo , b.tno_Empg, tno_CPF, Data_Hora_Criacao, b.tNo_Identidade, tipo, hora,data, d.NomeAbreviado, clinica, a.Vencimento_na_Geracao, a.Data_Ultimo_Exame_na_Geracao   ");

            if (xTipoRel == "A")
            {
                strSQL.Append(" order by e.NomeAbreviado + '   CNPJ: ' + e.NomeCodigo , b.tno_Empg, Data_Hora_Criacao ");
            }
            else
            {
                strSQL.Append("  ) as tx10 ");
                strSQL.Append("  group by Tipo_Afastamento, CNPJ, Data1, Data2 ");
                strSQL.Append("order by CNPJ ");
            }


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



        public DataSet Gerar_DS_Relatorio_Guias_Bloqueio_Associadas(int xIdCliente, string xD_Inicial, string xD_Final, Int16 xEmp, string xTipoRel)
        {
            //string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection();
            StringBuilder strSQL = new StringBuilder();

            //DataSet m_ds;


            DataTable table = new DataTable("Result");
            table.Columns.Add("Exame", Type.GetType("System.String"));
            table.Columns.Add("Empregado", Type.GetType("System.String"));
            table.Columns.Add("DataExame", Type.GetType("System.String"));
            table.Columns.Add("Empresa", Type.GetType("System.String"));
            table.Columns.Add("Tipo", Type.GetType("System.String"));
            table.Columns.Add("Data1", Type.GetType("System.String"));
            table.Columns.Add("Data2", Type.GetType("System.String"));
            table.Columns.Add("CNPJ", Type.GetType("System.String"));
            table.Columns.Add("RG", Type.GetType("System.String"));
            table.Columns.Add("Data_Criacao", Type.GetType("System.String"));
            table.Columns.Add("Retorno", Type.GetType("System.String"));
            table.Columns.Add("Registros", Type.GetType("System.Int16"));
            table.Columns.Add("Afastamento", Type.GetType("System.String"));
            table.Columns.Add("Tipo_Afastamento", Type.GetType("System.String"));
            table.Columns.Add("CPF", Type.GetType("System.String"));

            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);

            if (xTipoRel == "S")
            {
                strSQL.Append(" select CNPJ, Data1, Data2, count(*) as Registros, Tipo_Afastamento ");
                strSQL.Append(" from ( ");
            }

            strSQL.Append(" select distinct 'Relatório de Guias Geradas Associadas à Exames' as Tipo_Afastamento, e.NomeAbreviado + '   CNPJ: ' + e.NomeCodigo as CNPJ, 'RG ' + b.tNo_Identidade as RG, b.tno_empg as Empregado, tNO_CPF as CPF, ");
            strSQL.Append("        case Tipo  ");
            strSQL.Append("        When 'R' then 'Retorno' ");
            strSQL.Append("        When 'A' then 'Admissional' ");
            strSQL.Append("        When 'D' then 'Demissional' ");
            strSQL.Append("        When 'O' then 'Outros' ");
            strSQL.Append("        When 'P' then 'Periódico' ");
            strSQL.Append("        When 'M' then 'Mudança de Função' ");
            strSQL.Append("        end ");
            strSQL.Append("        as Tipo, ");
            strSQL.Append("       case ");
            strSQL.Append("           when ( Data is null or Data = '' ) and ( max(Hora) is not null and max(Hora) <> '' ) then 'Horário : ' + max(Hora) ");
            strSQL.Append("           when ( Data is not null and Data <> '' ) and ( max(Hora) is null or max(Hora) = '' ) then Data ");
            strSQL.Append("           when ( Data is not null and Data <> '' ) and ( max(Hora) is not null and max(Hora) <> '' ) then Data + '  Horário : ' + max(Hora) ");
            strSQL.Append("           when ( Data is null or Data = '' ) and ( max(Hora) is null or max(Hora) = '' ) then '' ");
            strSQL.Append("        end    ");
            strSQL.Append("        as DataExame,  ");
            strSQL.Append(" d.NomeAbreviado as Exame, Clinica as Empresa, ");
            strSQL.Append("        '" + xD_Inicial + "' as Data1, '" + xD_Final + "' as Data2, convert( char(10), max(Data_Hora_Criacao), 103 ) as Data_Criacao, 0 as Registros,  ");

            //utilizar de preferência datas salvas na tabela de guia de encaminhamento
            strSQL.Append(" case when ( a.vencimento_na_geracao is null ) then convert( char(10), max(f.DataVencimento), 103 )  ");
            strSQL.Append("      when ( a.vencimento_na_geracao is not null ) then convert( char(10), max(a.vencimento_na_geracao), 103 ) ");
            strSQL.Append(" end as Retorno,  ");
            strSQL.Append(" case when ( a.Data_Ultimo_Exame_na_Geracao  is null ) then convert( char(10), max(f.DataUltima), 103 )  ");
            strSQL.Append("      when ( a.Data_Ultimo_Exame_na_Geracao  is not null ) then convert( char(10),max(a.Data_Ultimo_Exame_na_Geracao), 103 ) ");
            strSQL.Append(" end as Afastamento   ");

            strSQL.Append(" from guia_encaminhamento as a left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblempregado as b ");
            strSQL.Append(" on ( a.Id_Empregado = b.nId_Empregado ) ");
            strSQL.Append(" left join Usuario as c on a.Id_Usuario = c.IdUsuario ");
            strSQL.Append(" left join pessoa as D on c.IdPessoa = d.IdPessoa ");
            strSQL.Append(" left join pessoa as E on a.Id_Empresa = e.IdPessoa ");
            strSQL.Append(" left join qryexameplanejamento as f on ( a.Id_Empregado = f.IdEmpregado and substring(f.NomeExame,1,1) = a.Tipo and f.IdExameDicionario between 1 and 5 ) ");

            //vai exibir somente os que tiverem exame correspondente salvo.  Se excluiu exame, não aparece no relatório
            strSQL.Append(" join examebase as g on ( a.Id_Empregado = g.IdEmpregado and convert( smalldatetime,a.Data,103 ) = g.DataExame  and g.IdExameDicionario between 1 and 5 )  ");

            if (xEmp == 1)
            {
                strSQL.Append(" where a.Id_Empresa = " + xIdCliente.ToString() + " ");
            }
            else if (xEmp == 2)
            {
                strSQL.Append(" where a.Id_Empresa in (  select idpessoa from juridica where idjuridica = " + xIdCliente.ToString() + " or idjuridicapai = " + xIdCliente.ToString() + " ) ");
            }
            else if (xEmp == 3)
            {
                strSQL.Append(" where a.Id_Empresa in (  select idpessoa from juridica where idgrupoempresa in ( select idgrupoempresa from juridica where idpessoa = " + xIdCliente.ToString() + " ) ) ");
            }




            //strSQL.Append(" and convert( smalldatetime,Data, 103 ) between  convert( smalldatetime, '" + xD_Inicial + "', 103 ) and convert( smalldatetime, '" + xD_Final + "', 103 )  and b.tno_Empg is not null ");
            strSQL.Append(" and Data_Hora_Criacao between  convert( smalldatetime, '" + xD_Inicial + "', 103 ) and convert( smalldatetime, '" + xD_Final + "', 103 )  and b.tno_Empg is not null ");
            strSQL.Append(" group by e.NomeAbreviado, e.NomeCodigo , b.tno_Empg, tNO_CPF, b.tNo_Identidade, tipo, data, d.NomeAbreviado, clinica, a.Vencimento_na_Geracao, a.Data_Ultimo_Exame_na_Geracao   ");

            if (xTipoRel == "A")
            {
                strSQL.Append(" order by e.NomeAbreviado + '   CNPJ: ' + e.NomeCodigo , b.tno_Empg ");
            }
            else
            {
                strSQL.Append("  ) as tx10 ");
                strSQL.Append("  group by Tipo_Afastamento,CNPJ, Data1, Data2 ");
                strSQL.Append("order by CNPJ ");
            }


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






        public DataSet Gerar_DS_Relatorio_AON(int xIdEmpregado, string xTipo)
        {
            //string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection();
            StringBuilder strSQL = new StringBuilder();

            //DataSet m_ds;


            DataTable table = new DataTable("Result");
            table.Columns.Add("IdEmpregado", Type.GetType("System.Int32"));
            table.Columns.Add("Colaborador", Type.GetType("System.String"));
            table.Columns.Add("Evento", Type.GetType("System.String"));
            table.Columns.Add("Especialidade", Type.GetType("System.String"));
            table.Columns.Add("DataAtendimento", Type.GetType("System.String"));
            table.Columns.Add("Nascimento", Type.GetType("System.String"));
            table.Columns.Add("Sexo", Type.GetType("System.String"));
            table.Columns.Add("Admissao", Type.GetType("System.String"));
            table.Columns.Add("Descricao", Type.GetType("System.String"));


            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);

            strSQL.Append(" select a.IdEmpregado, Evento, Especialidade, convert( char(10), Data_Atendimento,103 ) as DataAtendimento, ");
            strSQL.Append(" tno_Empg as Colaborador, convert(char(10), hDt_nasc, 103) as Nascimento, ");
            strSQL.Append(" case when  tSexo = 'M' then 'Masculino' ");
            strSQL.Append("      when  tSexo <> 'M' then 'Feminino' ");
            strSQL.Append(" end as Sexo, convert(char(10), hdt_adm, 103) as Admissao, Descricao ");
            strSQL.Append(" from ");
            strSQL.Append(" exame_atendimento_plano_saude as a left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblempregado as b ");
            strSQL.Append(" on(a.idempregado = b.nid_empregado) ");
            strSQL.Append(" where a.IdEmpregado = " + xIdEmpregado.ToString() + " and Evento = '" + xTipo + "' ");
            strSQL.Append(" order by a.Data_Atendimento desc ");



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









        public DataSet Gerar_Lista_Exames(Int32 xIdEmpregado)
        {
            //string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection();
            //StringBuilder strSQL = new StringBuilder();

            //DataSet m_ds;


            DataTable table = new DataTable("Result");
            table.Columns.Add("Id", Type.GetType("System.Int32"));
            table.Columns.Add("Data", Type.GetType("System.String"));
            table.Columns.Add("Tipo", Type.GetType("System.String"));
            table.Columns.Add("Resultado", Type.GetType("System.String"));
            table.Columns.Add("Data2", Type.GetType("System.DateTime"));

            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);


            SqlCommand rCommand = new SqlCommand();

            rCommand.Parameters.Add("@xIdEmpregado", SqlDbType.NChar).Value = xIdEmpregado.ToString();

            rCommand.CommandText = " select * from "
            + " ( "
            + " select a.IdExameBase as Id, convert( char(10),a.DataExame,103 ) as Data, DataExame as Data2, "
            + "   case  "
            + "      when b.Nome = 'Não Ocupacional' then 'Ambulatorial' "
            + " 	 else b.Nome	  "
            + " 	 end as Tipo, "
            + "   case  "
            + "     when b.Nome = 'Não Ocupacional' then 'Realizado' "
            + "     when a.IndResultado = 0 then 'Não Realizado' "
            + " 	when a.IndResultado = 1 then 'Normal' "
            + " 	when a.IndResultado = 2 then 'Alterado' "
            + " 	when a.IndResultado = 3 then 'Em Espera'   "
            + "  end as Resultado "
            + " from examebase as a left join examedicionario as b "
            + " on ( a.idexamedicionario = b.idexamedicionario ) "
            + " left join prontuariodigital as c on ( a.idexamebase = c.idexamebase ) "
            + " where a.IdEmpregado = @xIdEmpregado and ( a.idexamedicionario between 1 and 7 or ( a.idexamedicionario not between 1 and 7 and c.arquivo is not null ) ) "
            + " union "
            + " select IdAfastamento as Id, convert( char(10),DataInicial, 103 ) as Data,  DataInicial as Data2, 'Absenteísmo' as Tipo, "
            + " case  "
            + "   when DataVolta is not null then  'Retorno ' + convert(char(10), DataVolta, 103 ) "
            + "   when DataPrevista is not null then 'Previsão ' + convert(char(10), DataPrevista, 103 ) "
            + "   else  'Sem Previsão' "
            + "   end as Resultado "
            + " from Afastamento  "
            + " where IdEmpregado = @xIdEmpregado "
            + " ) as tx10 "
            + " order by Data2 desc ";


            //strSQL.Append(" select * from ");
            //strSQL.Append(" ( ");
            //strSQL.Append(" select IdExameBase as Id, convert( char(10),a.DataExame,103 ) as Data, DataExame as Data2, ");
            //strSQL.Append("   case  ");
            //strSQL.Append("      when b.Nome = 'Não Ocupacional' then 'Ambulatorial' ");
            //strSQL.Append(" 	 else b.Nome	  ");
            //strSQL.Append(" 	 end as Tipo, ");
            //strSQL.Append("   case  ");
            //strSQL.Append("     when b.Nome = 'Não Ocupacional' then 'Realizado' ");
            //strSQL.Append("     when a.IndResultado = 0 then 'Não Realizado' ");
            //strSQL.Append(" 	when a.IndResultado = 1 then 'Normal' ");
            //strSQL.Append(" 	when a.IndResultado = 2 then 'Alterado' ");
            //strSQL.Append(" 	when a.IndResultado = 3 then 'Em Espera'   ");
            //strSQL.Append("  end as Resultado ");
            //strSQL.Append(" from examebase as a left join examedicionario as b ");
            //strSQL.Append(" on ( a.idexamedicionario = b.idexamedicionario ) ");
            //strSQL.Append(" where IdEmpregado = " + xIdEmpregado.ToString() + " ");
            //strSQL.Append(" union ");
            //strSQL.Append(" select IdAfastamento as Id, convert( char(10),DataInicial, 103 ) as Data,  DataInicial as Data2, 'Absenteísmo' as Tipo, ");
            //strSQL.Append(" case  ");
            //strSQL.Append("   when DataVolta is not null then  'Retorno ' + convert(char(10), DataVolta, 103 ) ");
            //strSQL.Append("   when DataPrevista is not null then 'Previsão ' + convert(char(10), DataPrevista, 103 ) ");
            //strSQL.Append("   else  'Sem Previsão' ");
            //strSQL.Append("   end as Resultado ");
            //strSQL.Append(" from Afastamento  ");
            //strSQL.Append(" where IdEmpregado = " + xIdEmpregado.ToString() + " ");
            //strSQL.Append(" ) as tx10 ");
            //strSQL.Append(" order by Data2 desc ");


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



        public DataSet Gerar_Lista_ExamesMedicos(Int32 xIdEmpregado)
        {
            //string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection();
            StringBuilder strSQL = new StringBuilder();

            //DataSet m_ds;


            DataTable table = new DataTable("Result");
            table.Columns.Add("IdHistorico", Type.GetType("System.Int32"));
            table.Columns.Add("ExamesOcupacionais", Type.GetType("System.String"));
            table.Columns.Add("ExamesAmbulatoriais", Type.GetType("System.String"));
            table.Columns.Add("Atestados", Type.GetType("System.String"));
            table.Columns.Add("AfastamentosINSS", Type.GetType("System.String"));
            table.Columns.Add("ProgramaSaude", Type.GetType("System.String"));
            table.Columns.Add("AtendimentoAssistencial", Type.GetType("System.String"));


            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);



            strSQL.Append(" select tx1.ExamesOcupacionais, tx2.ExamesAmbulatoriais, tx3.Atestados, tx4.AfastamentosINSS, tx5.ProgramaSaude, tx6.AtendimentoAssistencial, ");
            strSQL.Append(" tx1.IdExameOcupacional, tx2.IdExameAmbulatorial, tx3.IdAtestado, tx4.IdAfastamentoINSS, tx5.IdProgramaSaude, tx6.IdAtendimentoAssistencial  from ");


            strSQL.Append(" (select top 100 ROW_NUMBER() OVER(ORDER BY DataInicial ) as IdHistorico, '' as ExamesOcupacionais, '' as ExamesAmbulatoriais, '' as Atestados, ");
            strSQL.Append(" '' as AfastamentosINSS, ");
            strSQL.Append("  '' as ProgramaSaude, '' as AtendimentoAssistencial ");
            strSQL.Append(", 0 as IdExameOcupacional, 0 as IdExameAmbulatorial, 0 as IdAtestado, 0 as IdAfastamentoINSS, 0 as IdProgramaSaude, 0 as IdAtendimentoAssistencial  ");
            strSQL.Append(" from afastamento ");
            strSQL.Append(" ) as tx0 ");

            strSQL.Append(" full join ");

            strSQL.Append(" (  select ROW_NUMBER() OVER(ORDER BY a.DataExame desc ) as IdHistorico, '&nbsp&nbsp' + convert(char(10), a.DataExame, 103) + '.....' + b.Nome as ExamesOcupacionais, '' as ExamesAmbulatoriais, ");
            strSQL.Append("         '' as Atestados, '' as AfastamentosINSS, '' as ProgramaSaude, '' as AtendimentoAssistencial ");
            strSQL.Append(", a.IdExameBase as IdExameOcupacional, 0 as IdExameAmbulatorial, 0 as IdAtestado, 0 as IdAfastamentoINSS, 0 as IdProgramaSaude, 0 as IdAtendimentoAssistencial  ");
            strSQL.Append("   from examebase as a left join examedicionario as b ");
            strSQL.Append("   on (a.idexamedicionario = b.idexamedicionario) ");
            strSQL.Append("   where a.IdExameDicionario between 1 and 5 ");
            strSQL.Append("   and a.IndResultado not in (0, 3) and a.IdEmpregado = " + xIdEmpregado.ToString() + " ");
            strSQL.Append(" ) as tx1 on ( tx0.IdHistorico = tx1.IdHistorico ) ");

            strSQL.Append(" full join ");

            strSQL.Append(" (select ROW_NUMBER() OVER(ORDER BY a.DataExame desc ) as IdHistorico, '' as ExamesOcupacionais, ");
            strSQL.Append("   '&nbsp&nbsp' + convert(char(10), a.DataExame, 103) + '.....' + case when b.Tipo = 'E' then 'Enfermeira' when b.Tipo = 'M' then 'Médico' else 'Outros' end as ExamesAmbulatoriais, ");
            strSQL.Append("          '' as Atestados, '' as AfastamentosINSS, '' as ProgramaSaude, '' as AtendimentoAssistencial ");
            strSQL.Append(", 0 as IdExameOcupacional, a.IdExameBase as IdExameAmbulatorial, 0 as IdAtestado, 0 as IdAfastamentoINSS, 0 as IdProgramaSaude, 0 as IdAtendimentoAssistencial  ");
            strSQL.Append("   from examebase as a left join ClinicoNaoOcupacional as b ");
            strSQL.Append("   on (a.IdExameBase = b.IdExameBase) ");
            strSQL.Append("   where a.IdExameDicionario = 7 and a.IdEmpregado = " + xIdEmpregado.ToString() + " ");
            strSQL.Append(" ) as tx2 on(tx0.IdHistorico = tx2.IdHistorico) ");

            strSQL.Append(" full join ");

            strSQL.Append(" (select ROW_NUMBER() OVER(ORDER BY DataInicial desc ) as IdHistorico, '' as ExamesOcupacionais, '' as ExamesAmbulatoriais, ");
            strSQL.Append(" '&nbsp&nbsp' + convert(char(10), DataInicial, 103) + '.....' + case when IndTipoAfastamento = 1 then 'Ocupacional' when IndTipoAfastamento = 2 then 'Assistencial' else 'Outros' end as Atestados, ");
            strSQL.Append("  '' as AfastamentosINSS, '' as ProgramaSaude, '' as AtendimentoAssistencial ");
            strSQL.Append(", 0 as IdExameOcupacional, 0 as IdExameAmbulatorial, IdAfastamento as IdAtestado, 0 as IdAfastamentoINSS, 0 as IdProgramaSaude, 0 as IdAtendimentoAssistencial  ");
            strSQL.Append(" from afastamento ");
            strSQL.Append(" where ( INSS is NULL or INSS = 0 ) and  IdEmpregado = " + xIdEmpregado.ToString() + " ");
            strSQL.Append(" ) as tx3 on(tx0.IdHistorico = tx3.IdHistorico) ");

            strSQL.Append(" full join ");

            strSQL.Append(" (select ROW_NUMBER() OVER(ORDER BY DataInicial desc ) as IdHistorico, '' as ExamesOcupacionais, '' as ExamesAmbulatoriais, '' as Atestados, ");
            strSQL.Append(" '&nbsp&nbsp' + convert(char(10), DataInicial, 103) + '.....' + case when IndTipoAfastamento = 1 then 'Ocupacional' when IndTipoAfastamento = 2 then 'Assistencial' else 'Outros' end as AfastamentosINSS, ");
            strSQL.Append("  '' as ProgramaSaude, '' as AtendimentoAssistencial ");
            strSQL.Append(", 0 as IdExameOcupacional, 0 as IdExameAmbulatorial, 0 as IdAtestado, IdAfastamento as IdAfastamentoINSS, 0 as IdProgramaSaude, 0 as IdAtendimentoAssistencial  ");
            strSQL.Append(" from afastamento ");
            strSQL.Append(" where ( INSS is not NULL and INSS = 1 ) and  IdEmpregado = " + xIdEmpregado.ToString() + " ");
            strSQL.Append(" ) as tx4 on(tx0.IdHistorico = tx4.IdHistorico) ");


            strSQL.Append(" full join ");

            //strSQL.Append(" ( select 1 as IdHistorico, '' as ExamesOcupacionais, '' as ExamesAmbulatoriais, '' as Atestados, ");
            //strSQL.Append(" '' as AfastamentosINSS, ");
            //strSQL.Append("  '' as ProgramaSaude, '' as AtendimentoAssistencial ");
            //strSQL.Append(" ) as tx5 on(tx0.IdHistorico = tx5.IdHistorico) ");


            strSQL.Append(" ( select ROW_NUMBER() OVER(ORDER BY Data_Questionario desc) as IdHistorico, '' as ExamesOcupacionais, '' as ExamesAmbulatoriais, '' as Atestados, ");
            strSQL.Append("             '' as AfastamentosINSS, ");
            strSQL.Append("              '&nbsp&nbsp' + convert(char(10), Data_Questionario, 103) + '.....' + ");
            strSQL.Append("              case when IdProgramaSaude = 1 then 'Saúde Alimentar' ");
            strSQL.Append("                   when IdProgramaSaude = 2 then 'Qualidade de Vida' ");
            strSQL.Append("                   when IdProgramaSaude = 3 then 'Combate ao Sedentarismo' ");
            strSQL.Append("                   when IdProgramaSaude = 4 then 'Ergonomia' ");
            strSQL.Append("                   when IdProgramaSaude = 5 then 'Grupo de Hipertensão' ");
            strSQL.Append("                   when IdProgramaSaude = 6 then 'Grupo de Diabetes' ");
            strSQL.Append("                   when IdProgramaSaude = 7 then 'Controle de Hábitos e Vícios' ");
            strSQL.Append("                   when IdProgramaSaude = 8 then 'Controle de Stress' ");
            strSQL.Append("              end as ProgramaSaude, '' as AtendimentoAssistencial ");
            strSQL.Append(", 0 as IdExameOcupacional, 0 as IdExameAmbulatorial, 0 as IdAtestado, 0 as IdAfastamentoINSS, 0 as IdProgramaSaude, 0 as IdAtendimentoAssistencial  ");
            strSQL.Append("              from questionario ");
            strSQL.Append("              where Status = 2 and IdProgramaSaude > 0 and  IdEmpregado = " + xIdEmpregado.ToString() + " ");
            strSQL.Append(" ) as tx5 on(tx0.IdHistorico = tx5.IdHistorico) ");


            strSQL.Append(" full join ");

            //strSQL.Append(" (select ROW_NUMBER() OVER(ORDER BY Data_Atendimento desc ) as IdHistorico, '' as ExamesOcupacionais, '' as ExamesAmbulatoriais, '' as Atestados, ");
            //strSQL.Append(" '' as AfastamentosINSS, ");
            //strSQL.Append("  '' as ProgramaSaude, '&nbsp&nbsp' + convert(char(10), Data_Atendimento, 103) + '.....' + Evento as AtendimentoAssistencial ");
            //strSQL.Append(" from Exame_Atendimento_Plano_Saude ");
            //strSQL.Append(" where IdEmpregado = " + xIdEmpregado.ToString() + " ");
            //strSQL.Append(" ) as tx6 on(tx0.IdHistorico = tx6.IdHistorico) ");

            strSQL.Append(" (select ROW_NUMBER() OVER(ORDER BY Evento desc ) as IdHistorico, '' as ExamesOcupacionais, '' as ExamesAmbulatoriais, '' as Atestados, ");
            strSQL.Append(" '' as AfastamentosINSS, ");
            strSQL.Append("  '' as ProgramaSaude, '&nbsp&nbsp' + replicate( '0', 3 - len( ltrim(rtrim(convert( varchar(3),count(*)))) ) ) + ltrim(rtrim(convert( varchar(3),count(*)))) +  replicate('.', 15) +  evento  as AtendimentoAssistencial ");
            strSQL.Append(", 0 as IdExameOcupacional, 0 as IdExameAmbulatorial, 0 as IdAtestado, 0 as IdAfastamentoINSS, 0 as IdProgramaSaude, 0 as IdAtendimentoAssistencial  ");
            strSQL.Append(" from Exame_Atendimento_Plano_Saude ");
            strSQL.Append(" where IdEmpregado = " + xIdEmpregado.ToString() + " group by Evento ");
            strSQL.Append(" ) as tx6 on(tx0.IdHistorico = tx6.IdHistorico) ");


            strSQL.Append(" where tx1.ExamesOcupacionais <> '' or tx2.ExamesAmbulatoriais <>'' or tx3.Atestados<> '' or  tx4.AfastamentosINSS<> '' or  tx5.ProgramaSaude<> ''  or tx6.AtendimentoAssistencial <> '' ");

            strSQL.Append(" order by tx1.IdHistorico desc  ");



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






        public DataSet Busca_Questionario(Int32 xIdEmpregado)
        {
            //string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection();
            //StringBuilder strSQL = new StringBuilder();

            //DataSet m_ds;


            DataTable table = new DataTable("Result");
            table.Columns.Add("IdQuestionario", Type.GetType("System.Int32"));
            table.Columns.Add("DataQuestionario", Type.GetType("System.String"));
            table.Columns.Add("Tipo", Type.GetType("System.String"));


            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);

            SqlCommand rCommand = new SqlCommand();

            rCommand.Parameters.Add("@xIdEmpregado", SqlDbType.NChar).Value = xIdEmpregado.ToString();


            rCommand.CommandText = " select IdQuestionario, convert( char(10), Data_Questionario, 103 ) as DataQuestionario, "
            + " case when IdClassificacao = 1 then 'Caso Agudo com Complicação' "
            + "      when IdClassificacao = 2 then 'Caso Agudo sem Complicação' "
            + "      when IdClassificacao = 3 then 'Caso Crônico' "
            + " end as Tipo"
            + " from Questionario where IdEmpregado = @xIdEmpregado "
            + " order by Data_Questionario desc  ";


            //strSQL.Append(" select IdQuestionario, convert( char(10), Data_Questionario, 103 ) as DataQuestionario, ");
            //strSQL.Append(" case when IdClassificacao = 1 then 'Caso Agudo com Complicação' ");
            //strSQL.Append("      when IdClassificacao = 2 then 'Caso Agudo sem Complicação' ");
            //strSQL.Append("      when IdClassificacao = 3 then 'Caso Crônico' ");            
            //strSQL.Append(" end as Tipo");
            //strSQL.Append(" from Questionario where IdEmpregado = " + xIdEmpregado.ToString() + " ");
            //strSQL.Append(" order by Data_Questionario desc  ");


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





        public DataSet Trazer_Questoes_Pesos(Int32 zCliente)
        {
            //string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection();
            StringBuilder strSQL = new StringBuilder();

            //DataSet m_ds;


            DataTable table = new DataTable("Result");
            table.Columns.Add("IdAnamneseDinamica", Type.GetType("System.Int32"));
            table.Columns.Add("Sistema", Type.GetType("System.String"));
            table.Columns.Add("Questao", Type.GetType("System.String"));
            table.Columns.Add("Peso", Type.GetType("System.Int16"));
            table.Columns.Add("IdAnamneseQuestao", Type.GetType("System.Int32"));


            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);



            SqlCommand rCommand = new SqlCommand();

            rCommand.Parameters.Add("@zCliente", SqlDbType.NChar).Value = zCliente.ToString();

            rCommand.CommandText = " select a.IdAnamneseDinamica, c.Sistema, b.questao, a.Peso, a.IdAnamneseQuestao "
            + " from anamnese_dinamica as a left join anamnese_questao as b "
            + " on(a.idanamnesequestao = b.idanamnesequestao) "
            + " left join anamnese_sistema as c "
            + " on(b.idanamnesesistema = c.idanamnesesistema) "
            + " where idpessoa = @zCliente "
            + " order by c.sistema, b.questao ";


            //strSQL.Append(" select a.IdAnamneseDinamica, c.Sistema, b.questao, a.Peso, a.IdAnamneseQuestao ");
            //strSQL.Append(" from anamnese_dinamica as a left join anamnese_questao as b ");
            //strSQL.Append(" on(a.idanamnesequestao = b.idanamnesequestao) ");
            //strSQL.Append(" left join anamnese_sistema as c ");
            //strSQL.Append(" on(b.idanamnesesistema = c.idanamnesesistema) ");
            //strSQL.Append(" where idpessoa = " + zCliente.ToString() + " ");
            //strSQL.Append(" order by c.sistema, b.questao ");



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




        public DataSet Trazer_Anamnese_Exame(Int32 zIdExameBase)
        {
            //string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection();
            StringBuilder strSQL = new StringBuilder();

            //DataSet m_ds;


            DataTable table = new DataTable("Result");
            table.Columns.Add("IdAnamneseExame", Type.GetType("System.Int32"));
            table.Columns.Add("IdExameBase", Type.GetType("System.Int32"));
            table.Columns.Add("IdAnamneseDinamica", Type.GetType("System.Int32"));
            table.Columns.Add("Peso", Type.GetType("System.Int16"));
            table.Columns.Add("Sistema", Type.GetType("System.String"));
            table.Columns.Add("Questao", Type.GetType("System.String"));
            table.Columns.Add("Resultado", Type.GetType("System.String"));
            table.Columns.Add("Obs", Type.GetType("System.String"));

            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);

            SqlCommand rCommand = new SqlCommand();

            rCommand.Parameters.Add("@zIdExameBase", SqlDbType.NChar).Value = zIdExameBase.ToString();

            rCommand.CommandText = " select a.IdAnamneseExame, a.IdExameBase, a.IdAnamneseDinamica, a.Peso, d.Sistema, c.Questao, a.Obs , "
            + " case when a.Resultado = 'S' then 'Sim'  when a.Resultado = 'N' then 'Não' end as Resultado "
            + " from anamnese_Exame as a "
            + " left join anamnese_Dinamica as b on(a.IdAnamneseDinamica = b.IdAnamneseDinamica) "
            + " left join Anamnese_Questao as c on(b.IdAnamneseQuestao = c.IdAnamneseQuestao) "
            + " left join Anamnese_Sistema as d on(c.IdAnamneseSistema = d.IdAnamneseSistema) "
            + " where idexamebase = @zIdExameBase "
            + " order by d.Sistema, c.Questao ";

            //strSQL.Append(" select a.IdAnamneseExame, a.IdExameBase, a.IdAnamneseDinamica, a.Peso, d.Sistema, c.Questao , ");
            //strSQL.Append(" case when a.Resultado = 'S' then 'Sim'  when a.Resultado = 'N' then 'Não' end as Resultado ");
            //strSQL.Append(" from anamnese_Exame as a ");
            //strSQL.Append(" left join anamnese_Dinamica as b on(a.IdAnamneseDinamica = b.IdAnamneseDinamica) ");
            //strSQL.Append(" left join Anamnese_Questao as c on(b.IdAnamneseQuestao = c.IdAnamneseQuestao) ");
            //strSQL.Append(" left join Anamnese_Sistema as d on(c.IdAnamneseSistema = d.IdAnamneseSistema) ");
            //strSQL.Append(" where idexamebase = " + zIdExameBase.ToString() + " ");
            //strSQL.Append(" order by d.Sistema, c.Questao ");



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





        public void Atualizar_Anamnese_Dinamica_Obs(Int32 xId, string xObs)
        {


            using (SqlConnection cnn = new SqlConnection(Ilitera.Data.SQLServer.EntitySQLServer.GetConnection()))
            {

                string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection();

                Database objDB = new SqlDatabase(connString);

                var objCmd = objDB.GetStoredProcCommand("sp_Atualizar_Anamnese_Dinamica_Obs");

                objDB.AddInParameter(objCmd, "xId", System.Data.DbType.Int32, xId);
                objDB.AddInParameter(objCmd, "xObs", System.Data.DbType.String, xObs);

                var readerRetorno = objDB.ExecuteReader(objCmd);


                return;

            }
        }


        public void Atualizar_Anamnese_Dinamica(Int32 xId, string xResultado)
        {


            using (SqlConnection cnn = new SqlConnection(Ilitera.Data.SQLServer.EntitySQLServer.GetConnection()))
            {

                string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection();

                Database objDB = new SqlDatabase(connString);

                var objCmd = objDB.GetStoredProcCommand("sp_Atualizar_Anamnese_Dinamica");

                objDB.AddInParameter(objCmd, "xId", System.Data.DbType.Int32, xId);
                objDB.AddInParameter(objCmd, "xResultado", System.Data.DbType.String, xResultado);

                var readerRetorno = objDB.ExecuteReader(objCmd);


                return;

            }
        }



        public void Carregar_Anamnese_Dinamica(Int32 xIdPessoa, Int32 @xIdExameBase)
        {


            using (SqlConnection cnn = new SqlConnection(Ilitera.Data.SQLServer.EntitySQLServer.GetConnection()))
            {

                string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection();

                Database objDB = new SqlDatabase(connString);

                var objCmd = objDB.GetStoredProcCommand("sp_Carregar_Anamnese_Dinamica_Exame");

                objDB.AddInParameter(objCmd, "xIdPessoa", System.Data.DbType.Int32, xIdPessoa);
                objDB.AddInParameter(objCmd, "xIdExameBase", System.Data.DbType.Int32, xIdExameBase);

                var readerRetorno = objDB.ExecuteReader(objCmd);


                return;

            }
        }



        public DataSet Gerar_Lista_Cronograma(Int32 xIdCIPA)
        {
            //string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection();
            //StringBuilder strSQL = new StringBuilder();

            //DataSet m_ds;


            DataTable table = new DataTable("Result");
            table.Columns.Add("IdEvento", Type.GetType("System.Int32"));
            table.Columns.Add("Tipo", Type.GetType("System.String"));
            table.Columns.Add("DataEvento", Type.GetType("System.String"));


            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);

            SqlCommand rCommand = new SqlCommand();

            rCommand.Parameters.Add("@xIdCipa", SqlDbType.NChar).Value = xIdCIPA.ToString();

            rCommand.CommandText = "select * from "
            + "( "
            + "   select 1 as IdEvento, '01. Edital' as Tipo, case when Edital is null then '-' when Edital is not null then  convert(char(10), Edital, 103) end as DataEvento "
            + "   from cipa where idcipa = @xIdCIPA  "
            + "   union "
            + "   select 2 as IdEvento, '02. Comissão Eleitoral' as Tipo, case when ComissaoEleitoral is null then '-' when ComissaoEleitoral is not null then  convert(char(10), ComissaoEleitoral, 103) end as DataEvento "
            + "   from cipa where idcipa = @xIdCIPA "
            + "   union "
            + "   select 3 as IdEvento, '03. Inicio Inscrição' as Tipo, case when InicioInscricao is null then '-' when InicioInscricao is not null then  convert(char(10), InicioInscricao, 103) end as DataEvento "
            + "   from cipa where idcipa = @xIdCIPA  "
            + "   union "
            + "   select 4 as IdEvento, '04. Cédula Votação' as Tipo, case when CedulaVotacao is null then '-' when CedulaVotacao is not null then  convert(char(10), CedulaVotacao, 103) end as DataEvento "
            + "   from cipa where idcipa = @xIdCIPA  "
            + "   union "
            + "   select 5 as IdEvento, '05. Término Inscrição' as Tipo, case when TerminoInscricao is null then '-' when TerminoInscricao is not null then  convert(char(10), TerminoInscricao, 103) end as DataEvento "
            + "   from cipa where idcipa = @xIdCIPA  "
            + "   union "
            + "   select 6 as IdEvento, '06. Eleição' as Tipo, case when Eleicao is null then '-' when Eleicao is not null then  convert(char(10), Eleicao, 103) end as DataEvento "
            + "   from cipa where idcipa = @xIdCIPA  "
            + "   union "
            + "   select 7 as IdEvento, '07. Posse' as Tipo, case when Posse is null then '-' when Posse is not null then  convert(char(10), Posse, 103) end as DataEvento "
            + "   from cipa where idcipa = @xIdCIPA "
            + "   union "
            + "   select 8 as IdEvento, '08. Publicação' as Tipo, case when Publicacao is null then '-' when Publicacao is not null then  convert(char(10), Publicacao, 103) end as DataEvento "
            + "   from cipa where idcipa = @xIdCIPA "
            + "   union "
            + "   select 9 as IdEvento, '09. Calendário' as Tipo, case when Calendario is null then '-' when Calendario is not null then  convert(char(10), Calendario, 103) end as DataEvento "
            + "   from cipa where idcipa = @xIdCIPA  "
            + "   union "
            + "   select 10 as IdEvento, '10. Registro DRT' as Tipo, case when RegistroDRT is null then '-' when RegistroDRT is not null then  convert(char(10), RegistroDRT, 103) end as DataEvento "
            + "   from cipa where idcipa = @xIdCIPA "
            + ") as tx89 "
            + "order by Tipo ";


            //strSQL.Append("select * from ");
            //strSQL.Append("( ");
            //strSQL.Append("   select 1 as IdEvento, '01. Edital' as Tipo, case when Edital is null then '-' when Edital is not null then  convert(char(10), Edital, 103) end as DataEvento ");
            //strSQL.Append("   from cipa where idcipa = " + xIdCIPA.ToString() + "  " );

            //strSQL.Append("   union ");

            //strSQL.Append("   select 2 as IdEvento, '02. Comissão Eleitoral' as Tipo, case when ComissaoEleitoral is null then '-' when ComissaoEleitoral is not null then  convert(char(10), ComissaoEleitoral, 103) end as DataEvento ");
            //strSQL.Append("   from cipa where idcipa = " + xIdCIPA.ToString() + "  ");

            //strSQL.Append("   union ");

            //strSQL.Append("   select 3 as IdEvento, '03. Inicio Inscrição' as Tipo, case when InicioInscricao is null then '-' when InicioInscricao is not null then  convert(char(10), InicioInscricao, 103) end as DataEvento ");
            //strSQL.Append("   from cipa where idcipa = " + xIdCIPA.ToString() + "  ");

            //strSQL.Append("   union ");

            //strSQL.Append("   select 4 as IdEvento, '04. Cédula Votação' as Tipo, case when CedulaVotacao is null then '-' when CedulaVotacao is not null then  convert(char(10), CedulaVotacao, 103) end as DataEvento ");
            //strSQL.Append("   from cipa where idcipa = " + xIdCIPA.ToString() + "  ");

            //strSQL.Append("   union ");

            //strSQL.Append("   select 5 as IdEvento, '05. Término Inscrição' as Tipo, case when TerminoInscricao is null then '-' when TerminoInscricao is not null then  convert(char(10), TerminoInscricao, 103) end as DataEvento ");
            //strSQL.Append("   from cipa where idcipa = " + xIdCIPA.ToString() + "  ");

            //strSQL.Append("   union ");

            //strSQL.Append("   select 6 as IdEvento, '06. Eleição' as Tipo, case when Eleicao is null then '-' when Eleicao is not null then  convert(char(10), Eleicao, 103) end as DataEvento ");
            //strSQL.Append("   from cipa where idcipa = " + xIdCIPA.ToString() + "  ");

            //strSQL.Append("   union ");

            //strSQL.Append("   select 7 as IdEvento, '07. Posse' as Tipo, case when Posse is null then '-' when Posse is not null then  convert(char(10), Posse, 103) end as DataEvento ");
            //strSQL.Append("   from cipa where idcipa = " + xIdCIPA.ToString() + "  ");

            //strSQL.Append("   union ");

            //strSQL.Append("   select 8 as IdEvento, '08. Publicação' as Tipo, case when Publicacao is null then '-' when Publicacao is not null then  convert(char(10), Publicacao, 103) end as DataEvento ");
            //strSQL.Append("   from cipa where idcipa = " + xIdCIPA.ToString() + "  ");

            //strSQL.Append("   union ");

            //strSQL.Append("   select 9 as IdEvento, '09. Calendário' as Tipo, case when Calendario is null then '-' when Calendario is not null then  convert(char(10), Calendario, 103) end as DataEvento ");
            //strSQL.Append("   from cipa where idcipa = " + xIdCIPA.ToString() + "  ");

            //strSQL.Append("   union ");

            //strSQL.Append("   select 10 as IdEvento, '10. Registro DRT' as Tipo, case when RegistroDRT is null then '-' when RegistroDRT is not null then  convert(char(10), RegistroDRT, 103) end as DataEvento ");
            //strSQL.Append("   from cipa where idcipa = " + xIdCIPA.ToString() + "  ");

            //strSQL.Append(") as tx89 ");
            //strSQL.Append("order by Tipo ");





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




        public DataSet Gerar_DS_Relatorio_Absenteismo_Grafico(int xIdCliente, string xD_Inicial, string xD_Final, Int16 xEmp)
        {
            //string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection();
            StringBuilder strSQL = new StringBuilder();

            //DataSet m_ds;


            DataTable table = new DataTable("Result");
            table.Columns.Add("CNPJ", Type.GetType("System.String"));
            table.Columns.Add("CID", Type.GetType("System.String"));
            table.Columns.Add("Data1", Type.GetType("System.String"));
            table.Columns.Add("Data2", Type.GetType("System.String"));
            table.Columns.Add("Registros", Type.GetType("System.Int16"));


            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);


            strSQL.Append("select CNPJ, substring(CID,1,6), sum(Empregados) as Registros, '" + xD_Inicial.Substring(0, 10) + "' as Data1, '" + xD_Final.Substring(0, 10) + "' as Data2    from (  ");

            strSQL.Append("   select CNPJ, cid1 as CID, Empregados  from(  ");
            strSQL.Append("     select count(distinct b.nId_Empregado) as Empregados, r.Nomeabreviado + '  ' + r.NomeCodigo as CNPJ, g.Descricao as Cid1, '01/01/2015' as Data1, '30/05/2017' as Data2  ");
            strSQL.Append("     from afastamento as a  ");
            strSQL.Append(" left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblempregado as b  ");
            strSQL.Append(" on ( a.idEmpregado = b.nId_Empregado )  ");
            strSQL.Append(" left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEmpregado_Funcao as c  ");
            strSQL.Append(" on ( b.nId_Empregado = c.nId_Empregado   ");

            if (xEmp == 1)
            {
                strSQL.Append(" and c.nId_Empr = " + xIdCliente.ToString() + " ) ");
            }
            else if (xEmp == 2)
            {
                strSQL.Append(" and c.nId_Empr in (  select idpessoa from juridica where idjuridica = " + xIdCliente.ToString() + " or idjuridicapai = " + xIdCliente.ToString() + " )  ) ");
            }
            else if (xEmp == 3)
            {
                strSQL.Append(" and c.nId_Empr in (  select idpessoa from juridica where idgrupoempresa in ( select idgrupoempresa from juridica where idpessoa = " + xIdCliente.ToString() + " ) )  ) ");
            }
            else if (xEmp == 0)
            {
                strSQL.Append(" ) ");
            }

            strSQL.Append(" join (   ");
            strSQL.Append("    select convert( char(15), nId_Empregado ) + convert( char(10),max( hDt_Inicio ),103 ) as Chave  ");
            strSQL.Append("    from  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblempregado_Funcao  ");

            if (xEmp == 1)
            {
                strSQL.Append(" where nId_Empr = " + xIdCliente.ToString() + " ");
            }
            else if (xEmp == 2)
            {
                strSQL.Append(" where nId_Empr in (  select idpessoa from juridica where idjuridica = " + xIdCliente.ToString() + " or idjuridicapai = " + xIdCliente.ToString() + " ) ");
            }
            else if (xEmp == 3)
            {
                strSQL.Append(" where nId_Empr in (  select idpessoa from juridica where idgrupoempresa in ( select idgrupoempresa from juridica where idpessoa = " + xIdCliente.ToString() + " ) ) ");
            }
            else if (xEmp == 0)
            {
                strSQL.Append(" where nId_Empr is not null  and nid_Empr in ( select idpessoa from pessoa where isinativo=0) ");
            }


            strSQL.Append("    group by nId_Empregado  ");
            strSQL.Append(" ) as tx80 on tx80.Chave = convert( char(15), c.nId_Empregado ) + convert( char(10),c.hDt_Inicio,103 )  ");

            strSQL.Append("     left join Cid as g   on(a.IdCid = g.IdCid)  ");
            strSQL.Append("     left join acidente as k   on(a.idAcidente = k.IdAcidente)  ");
            strSQL.Append("     left join cat as m   on(k.IdCat = m.IdCat)  ");
            strSQL.Append("     left join pessoa as r   on(c.nId_Empr = r.IdPessoa)  ");

            if (xEmp == 1)
            {
                strSQL.Append(" where c.nId_Empr = " + xIdCliente.ToString() + " ");
            }
            else if (xEmp == 2)
            {
                strSQL.Append(" where c.nId_Empr in (  select idpessoa from juridica where idjuridica = " + xIdCliente.ToString() + " or idjuridicapai = " + xIdCliente.ToString() + " ) ");
            }
            else if (xEmp == 3)
            {
                strSQL.Append(" where c.nId_Empr in (  select idpessoa from juridica where idgrupoempresa in ( select idgrupoempresa from juridica where idpessoa = " + xIdCliente.ToString() + " ) ) ");
            }
            else if (xEmp == 0)
            {
                strSQL.Append(" where c.nId_Empr is not null ");
            }

            strSQL.Append(" and a.DataInicial between  convert( smalldatetime, '" + xD_Inicial + "', 103 ) and convert( smalldatetime, '" + xD_Final + "', 103 )  ");
            strSQL.Append("     and b.tno_Empg is not null  and g.Descricao is not null  ");
            strSQL.Append("     group by r.nomeAbreviado, r.NomeCodigo, g.Descricao  ");
            strSQL.Append("  ) as tx99  ");

            strSQL.Append("union  ");


            strSQL.Append("   select CNPJ, cid1 as CID, Empregados  from(  ");
            strSQL.Append("     select count(distinct b.nId_Empregado) as Empregados, r.Nomeabreviado + '  ' + r.NomeCodigo as CNPJ, g.Descricao as Cid1, '01/01/2015' as Data1, '30/05/2017' as Data2  ");
            strSQL.Append("     from afastamento as a  ");
            strSQL.Append(" left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblempregado as b  ");
            strSQL.Append(" on ( a.idEmpregado = b.nId_Empregado )  ");
            strSQL.Append(" left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEmpregado_Funcao as c  ");
            strSQL.Append(" on ( b.nId_Empregado = c.nId_Empregado   ");

            if (xEmp == 1)
            {
                strSQL.Append(" and c.nId_Empr = " + xIdCliente.ToString() + " ) ");
            }
            else if (xEmp == 2)
            {
                strSQL.Append(" and c.nId_Empr in (  select idpessoa from juridica where idjuridica = " + xIdCliente.ToString() + " or idjuridicapai = " + xIdCliente.ToString() + " )  ) ");
            }
            else if (xEmp == 3)
            {
                strSQL.Append(" and c.nId_Empr in (  select idpessoa from juridica where idgrupoempresa in ( select idgrupoempresa from juridica where idpessoa = " + xIdCliente.ToString() + " ) )  ) ");
            }
            else if (xEmp == 0)
            {
                strSQL.Append(" ) ");
            }

            strSQL.Append(" join (   ");
            strSQL.Append("    select convert( char(15), nId_Empregado ) + convert( char(10),max( hDt_Inicio ),103 ) as Chave  ");
            strSQL.Append("    from  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblempregado_Funcao  ");

            if (xEmp == 1)
            {
                strSQL.Append(" where nId_Empr = " + xIdCliente.ToString() + " ");
            }
            else if (xEmp == 2)
            {
                strSQL.Append(" where nId_Empr in (  select idpessoa from juridica where idjuridica = " + xIdCliente.ToString() + " or idjuridicapai = " + xIdCliente.ToString() + " ) ");
            }
            else if (xEmp == 3)
            {
                strSQL.Append(" where nId_Empr in (  select idpessoa from juridica where idgrupoempresa in ( select idgrupoempresa from juridica where idpessoa = " + xIdCliente.ToString() + " ) ) ");
            }
            else if (xEmp == 0)
            {
                strSQL.Append(" where nId_Empr is not null  and nid_Empr in ( select idpessoa from pessoa where isinativo=0) ");
            }


            strSQL.Append("    group by nId_Empregado  ");
            strSQL.Append(" ) as tx80 on tx80.Chave = convert( char(15), c.nId_Empregado ) + convert( char(10),c.hDt_Inicio,103 )  ");

            strSQL.Append("     left join Cid as g   on(a.IdCid2 = g.IdCid)  ");
            strSQL.Append("     left join acidente as k   on(a.idAcidente = k.IdAcidente)  ");
            strSQL.Append("     left join cat as m   on(k.IdCat = m.IdCat)  ");
            strSQL.Append("     left join pessoa as r   on(c.nId_Empr = r.IdPessoa)  ");

            if (xEmp == 1)
            {
                strSQL.Append(" where c.nId_Empr = " + xIdCliente.ToString() + " ");
            }
            else if (xEmp == 2)
            {
                strSQL.Append(" where c.nId_Empr in (  select idpessoa from juridica where idjuridica = " + xIdCliente.ToString() + " or idjuridicapai = " + xIdCliente.ToString() + " ) ");
            }
            else if (xEmp == 3)
            {
                strSQL.Append(" where c.nId_Empr in (  select idpessoa from juridica where idgrupoempresa in ( select idgrupoempresa from juridica where idpessoa = " + xIdCliente.ToString() + " ) ) ");
            }
            else if (xEmp == 0)
            {
                strSQL.Append(" where c.nId_Empr is not null ");
            }

            strSQL.Append(" and a.DataInicial between  convert( smalldatetime, '" + xD_Inicial + "', 103 ) and convert( smalldatetime, '" + xD_Final + "', 103 )  ");
            strSQL.Append("     and b.tno_Empg is not null  and g.Descricao is not null  ");
            strSQL.Append("     group by r.nomeAbreviado, r.NomeCodigo, g.Descricao  ");
            strSQL.Append("  ) as tx98  ");

            strSQL.Append("union  ");


            strSQL.Append("   select CNPJ, cid1 as CID, Empregados  from(  ");
            strSQL.Append("     select count(distinct b.nId_Empregado) as Empregados, r.Nomeabreviado + '  ' + r.NomeCodigo as CNPJ, g.Descricao as Cid1, '01/01/2015' as Data1, '30/05/2017' as Data2  ");
            strSQL.Append("     from afastamento as a  ");
            strSQL.Append(" left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblempregado as b  ");
            strSQL.Append(" on ( a.idEmpregado = b.nId_Empregado )  ");
            strSQL.Append(" left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEmpregado_Funcao as c  ");
            strSQL.Append(" on ( b.nId_Empregado = c.nId_Empregado   ");

            if (xEmp == 1)
            {
                strSQL.Append(" and c.nId_Empr = " + xIdCliente.ToString() + " ) ");
            }
            else if (xEmp == 2)
            {
                strSQL.Append(" and c.nId_Empr in (  select idpessoa from juridica where idjuridica = " + xIdCliente.ToString() + " or idjuridicapai = " + xIdCliente.ToString() + " )  ) ");
            }
            else if (xEmp == 3)
            {
                strSQL.Append(" and c.nId_Empr in (  select idpessoa from juridica where idgrupoempresa in ( select idgrupoempresa from juridica where idpessoa = " + xIdCliente.ToString() + " ) )  ) ");
            }
            else if (xEmp == 0)
            {
                strSQL.Append(" ) ");
            }

            strSQL.Append(" join (   ");
            strSQL.Append("    select convert( char(15), nId_Empregado ) + convert( char(10),max( hDt_Inicio ),103 ) as Chave  ");
            strSQL.Append("    from  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblempregado_Funcao  ");

            if (xEmp == 1)
            {
                strSQL.Append(" where nId_Empr = " + xIdCliente.ToString() + " ");
            }
            else if (xEmp == 2)
            {
                strSQL.Append(" where nId_Empr in (  select idpessoa from juridica where idjuridica = " + xIdCliente.ToString() + " or idjuridicapai = " + xIdCliente.ToString() + " ) ");
            }
            else if (xEmp == 3)
            {
                strSQL.Append(" where nId_Empr in (  select idpessoa from juridica where idgrupoempresa in ( select idgrupoempresa from juridica where idpessoa = " + xIdCliente.ToString() + " ) ) ");
            }
            else if (xEmp == 0)
            {
                strSQL.Append(" where nId_Empr is not null  and nid_Empr in ( select idpessoa from pessoa where isinativo=0) ");
            }


            strSQL.Append("    group by nId_Empregado  ");
            strSQL.Append(" ) as tx80 on tx80.Chave = convert( char(15), c.nId_Empregado ) + convert( char(10),c.hDt_Inicio,103 )  ");

            strSQL.Append("     left join Cid as g   on(a.IdCid3 = g.IdCid)  ");
            strSQL.Append("     left join acidente as k   on(a.idAcidente = k.IdAcidente)  ");
            strSQL.Append("     left join cat as m   on(k.IdCat = m.IdCat)  ");
            strSQL.Append("     left join pessoa as r   on(c.nId_Empr = r.IdPessoa)  ");

            if (xEmp == 1)
            {
                strSQL.Append(" where c.nId_Empr = " + xIdCliente.ToString() + " ");
            }
            else if (xEmp == 2)
            {
                strSQL.Append(" where c.nId_Empr in (  select idpessoa from juridica where idjuridica = " + xIdCliente.ToString() + " or idjuridicapai = " + xIdCliente.ToString() + " ) ");
            }
            else if (xEmp == 3)
            {
                strSQL.Append(" where c.nId_Empr in (  select idpessoa from juridica where idgrupoempresa in ( select idgrupoempresa from juridica where idpessoa = " + xIdCliente.ToString() + " ) ) ");
            }
            else if (xEmp == 0)
            {
                strSQL.Append(" where c.nId_Empr is not null ");
            }

            strSQL.Append(" and a.DataInicial between  convert( smalldatetime, '" + xD_Inicial + "', 103 ) and convert( smalldatetime, '" + xD_Final + "', 103 )  ");
            strSQL.Append("     and b.tno_Empg is not null  and g.Descricao is not null  ");
            strSQL.Append("     group by r.nomeAbreviado, r.NomeCodigo, g.Descricao  ");
            strSQL.Append("  ) as tx97  ");

            strSQL.Append("union  ");


            strSQL.Append("   select CNPJ, cid1 as CID, Empregados  from(  ");
            strSQL.Append("     select count(distinct b.nId_Empregado) as Empregados, r.Nomeabreviado + '  ' + r.NomeCodigo as CNPJ, g.Descricao as Cid1, '01/01/2015' as Data1, '30/05/2017' as Data2  ");
            strSQL.Append("     from afastamento as a  ");
            strSQL.Append(" left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblempregado as b  ");
            strSQL.Append(" on ( a.idEmpregado = b.nId_Empregado )  ");
            strSQL.Append(" left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEmpregado_Funcao as c  ");
            strSQL.Append(" on ( b.nId_Empregado = c.nId_Empregado   ");

            if (xEmp == 1)
            {
                strSQL.Append(" and c.nId_Empr = " + xIdCliente.ToString() + " ) ");
            }
            else if (xEmp == 2)
            {
                strSQL.Append(" and c.nId_Empr in (  select idpessoa from juridica where idjuridica = " + xIdCliente.ToString() + " or idjuridicapai = " + xIdCliente.ToString() + " )  ) ");
            }
            else if (xEmp == 3)
            {
                strSQL.Append(" and c.nId_Empr in (  select idpessoa from juridica where idgrupoempresa in ( select idgrupoempresa from juridica where idpessoa = " + xIdCliente.ToString() + " ) )  ) ");
            }
            else if (xEmp == 0)
            {
                strSQL.Append(" ) ");
            }

            strSQL.Append(" join (   ");
            strSQL.Append("    select convert( char(15), nId_Empregado ) + convert( char(10),max( hDt_Inicio ),103 ) as Chave  ");
            strSQL.Append("    from  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblempregado_Funcao  ");

            if (xEmp == 1)
            {
                strSQL.Append(" where nId_Empr = " + xIdCliente.ToString() + " ");
            }
            else if (xEmp == 2)
            {
                strSQL.Append(" where nId_Empr in (  select idpessoa from juridica where idjuridica = " + xIdCliente.ToString() + " or idjuridicapai = " + xIdCliente.ToString() + " ) ");
            }
            else if (xEmp == 3)
            {
                strSQL.Append(" where nId_Empr in (  select idpessoa from juridica where idgrupoempresa in ( select idgrupoempresa from juridica where idpessoa = " + xIdCliente.ToString() + " ) ) ");
            }
            else if (xEmp == 0)
            {
                strSQL.Append(" where nId_Empr is not null  and nid_Empr in ( select idpessoa from pessoa where isinativo=0) ");
            }


            strSQL.Append("    group by nId_Empregado  ");
            strSQL.Append(" ) as tx80 on tx80.Chave = convert( char(15), c.nId_Empregado ) + convert( char(10),c.hDt_Inicio,103 )  ");

            strSQL.Append("     left join Cid as g   on(a.IdCid4 = g.IdCid)  ");
            strSQL.Append("     left join acidente as k   on(a.idAcidente = k.IdAcidente)  ");
            strSQL.Append("     left join cat as m   on(k.IdCat = m.IdCat)  ");
            strSQL.Append("     left join pessoa as r   on(c.nId_Empr = r.IdPessoa)  ");

            if (xEmp == 1)
            {
                strSQL.Append(" where c.nId_Empr = " + xIdCliente.ToString() + " ");
            }
            else if (xEmp == 2)
            {
                strSQL.Append(" where c.nId_Empr in (  select idpessoa from juridica where idjuridica = " + xIdCliente.ToString() + " or idjuridicapai = " + xIdCliente.ToString() + " ) ");
            }
            else if (xEmp == 3)
            {
                strSQL.Append(" where c.nId_Empr in (  select idpessoa from juridica where idgrupoempresa in ( select idgrupoempresa from juridica where idpessoa = " + xIdCliente.ToString() + " ) ) ");
            }
            else if (xEmp == 0)
            {
                strSQL.Append(" where c.nId_Empr is not null ");
            }

            strSQL.Append(" and a.DataInicial between  convert( smalldatetime, '" + xD_Inicial + "', 103 ) and convert( smalldatetime, '" + xD_Final + "', 103 )  ");
            strSQL.Append("     and b.tno_Empg is not null  and g.Descricao is not null  ");
            strSQL.Append("     group by r.nomeAbreviado, r.NomeCodigo, g.Descricao  ");
            strSQL.Append("  ) as tx96  ");


            strSQL.Append("  ) as tx100  ");
            strSQL.Append("  group by CNPJ, CID  ");
            strSQL.Append("  order by CNPJ, sum( Empregados ) Desc  ");


            using (SqlConnection cnn = new SqlConnection(Ilitera.Data.SQLServer.EntitySQLServer.GetConnection()))
            {
                SqlDataAdapter da;
                cnn.Open();


                da = new SqlDataAdapter(strSQL.ToString(), cnn);
                da.SelectCommand.CommandTimeout = 900;
                da.Fill(m_ds, "Result");

                cnn.Close();

                da.Dispose();
            }

            return m_ds;



        }





        public DataSet Gerar_DS_Relatorio_Absenteismo_Grafico_Horas(int xIdCliente, string xD_Inicial, string xD_Final, Int16 xEmp)
        {
            //string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection();
            StringBuilder strSQL = new StringBuilder();

            //DataSet m_ds;


            DataTable table = new DataTable("Result");
            table.Columns.Add("Empregado", Type.GetType("System.String"));
            table.Columns.Add("Setor", Type.GetType("System.String"));
            table.Columns.Add("Funcao", Type.GetType("System.String"));
            table.Columns.Add("Admissao", Type.GetType("System.String"));
            table.Columns.Add("Nascimento", Type.GetType("System.String"));
            table.Columns.Add("Afastamento", Type.GetType("System.String"));
            table.Columns.Add("Retorno", Type.GetType("System.String"));
            table.Columns.Add("Tipo_Afastamento", Type.GetType("System.String"));
            table.Columns.Add("Cid1", Type.GetType("System.String"));
            table.Columns.Add("Cid2", Type.GetType("System.String"));
            table.Columns.Add("Cid3", Type.GetType("System.String"));
            table.Columns.Add("Cid4", Type.GetType("System.String"));
            table.Columns.Add("NumeroCat", Type.GetType("System.String"));
            table.Columns.Add("Data1", Type.GetType("System.String"));
            table.Columns.Add("Data2", Type.GetType("System.String"));
            table.Columns.Add("Registros", Type.GetType("System.Int16"));
            table.Columns.Add("Exame", Type.GetType("System.String"));

            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);


            strSQL.Append("select CNPJ, CID, sum(Empregados) as Registros, '" + xD_Inicial.Substring(0, 10) + "' as Data1, '" + xD_Final.Substring(0, 10) + "' as Data2, DInicial, HInicial, DVolta, HVolta, DPrevista, HPrevista   from (  ");

            strSQL.Append("   select CNPJ, cid1 as CID, Empregados, DInicial, HInicial, DVolta, HVolta, DPrevista, HPrevista  from(  ");
            strSQL.Append("     select count(distinct b.nId_Empregado) as Empregados, r.Nomeabreviado + '  ' + r.NomeCodigo as CNPJ, g.Descricao as Cid1, '01/01/2015' as Data1, '30/05/2017' as Data2,  ");
            strSQL.Append("     convert(char(10), a.DataInicial, 103) as DInicial, convert(char(8), a.DataInicial, 14) as HInicial, convert(char(10), a.DataVolta, 103) as DVolta, convert(char(8), a.DataVolta, 14) as HVolta, convert(char(10), a.DataPrevista, 103) as DPrevista, convert(char(8), a.DataPrevista, 14) as HPrevista ");
            strSQL.Append("     from afastamento as a  ");
            strSQL.Append(" left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblempregado as b  ");
            strSQL.Append(" on ( a.idEmpregado = b.nId_Empregado )  ");
            strSQL.Append(" left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEmpregado_Funcao as c  ");
            strSQL.Append(" on ( b.nId_Empregado = c.nId_Empregado   ");

            if (xEmp == 1)
            {
                strSQL.Append(" and c.nId_Empr = " + xIdCliente.ToString() + " ) ");
            }
            else if (xEmp == 2)
            {
                strSQL.Append(" and c.nId_Empr in (  select idpessoa from juridica where idjuridica = " + xIdCliente.ToString() + " or idjuridicapai = " + xIdCliente.ToString() + " )  ) ");
            }
            else if (xEmp == 3)
            {
                strSQL.Append(" and c.nId_Empr in (  select idpessoa from juridica where idgrupoempresa in ( select idgrupoempresa from juridica where idpessoa = " + xIdCliente.ToString() + " ) )  ) ");
            }
            else if (xEmp == 0)
            {
                strSQL.Append(" ) ");
            }

            strSQL.Append(" join (   ");
            strSQL.Append("    select convert( char(15), nId_Empregado ) + convert( char(10),max( hDt_Inicio ),103 ) as Chave  ");
            strSQL.Append("    from  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblempregado_Funcao  ");

            if (xEmp == 1)
            {
                strSQL.Append(" where nId_Empr = " + xIdCliente.ToString() + " ");
            }
            else if (xEmp == 2)
            {
                strSQL.Append(" where nId_Empr in (  select idpessoa from juridica where idjuridica = " + xIdCliente.ToString() + " or idjuridicapai = " + xIdCliente.ToString() + " ) ");
            }
            else if (xEmp == 3)
            {
                strSQL.Append(" where nId_Empr in (  select idpessoa from juridica where idgrupoempresa in ( select idgrupoempresa from juridica where idpessoa = " + xIdCliente.ToString() + " ) ) ");
            }
            else if (xEmp == 0)
            {
                strSQL.Append(" where nId_Empr is not null  and nid_Empr in ( select idpessoa from pessoa where isinativo=0) ");
            }


            strSQL.Append("    group by nId_Empregado  ");
            strSQL.Append(" ) as tx80 on tx80.Chave = convert( char(15), c.nId_Empregado ) + convert( char(10),c.hDt_Inicio,103 )  ");

            strSQL.Append("     left join Cid as g   on(a.IdCid = g.IdCid)  ");
            strSQL.Append("     left join acidente as k   on(a.idAcidente = k.IdAcidente)  ");
            strSQL.Append("     left join cat as m   on(k.IdCat = m.IdCat)  ");
            strSQL.Append("     left join pessoa as r   on(c.nId_Empr = r.IdPessoa)  ");

            if (xEmp == 1)
            {
                strSQL.Append(" where c.nId_Empr = " + xIdCliente.ToString() + " ");
            }
            else if (xEmp == 2)
            {
                strSQL.Append(" where c.nId_Empr in (  select idpessoa from juridica where idjuridica = " + xIdCliente.ToString() + " or idjuridicapai = " + xIdCliente.ToString() + " ) ");
            }
            else if (xEmp == 3)
            {
                strSQL.Append(" where c.nId_Empr in (  select idpessoa from juridica where idgrupoempresa in ( select idgrupoempresa from juridica where idpessoa = " + xIdCliente.ToString() + " ) ) ");
            }
            else if (xEmp == 0)
            {
                strSQL.Append(" where c.nId_Empr is not null ");
            }

            strSQL.Append(" and a.DataInicial between  convert( smalldatetime, '" + xD_Inicial + "', 103 ) and convert( smalldatetime, '" + xD_Final + "', 103 )  ");
            strSQL.Append("     and b.tno_Empg is not null  and g.Descricao is not null  ");
            strSQL.Append("     group by r.nomeAbreviado, r.NomeCodigo, g.Descricao, DataInicial, DataVolta, DataPrevista  ");
            strSQL.Append("  ) as tx99  ");

            strSQL.Append("union  ");


            strSQL.Append("   select CNPJ, cid1 as CID, Empregados, DInicial, HInicial, DVolta, HVolta, DPrevista, HPrevista  from(  ");
            strSQL.Append("     select count(distinct b.nId_Empregado) as Empregados, r.Nomeabreviado + '  ' + r.NomeCodigo as CNPJ, g.Descricao as Cid1, '01/01/2015' as Data1, '30/05/2017' as Data2,  ");
            strSQL.Append("     convert(char(10), a.DataInicial, 103) as DInicial, convert(char(8), a.DataInicial, 14) as HInicial, convert(char(10), a.DataVolta, 103) as DVolta, convert(char(8), a.DataVolta, 14) as HVolta, convert(char(10), a.DataPrevista, 103) as DPrevista, convert(char(8), a.DataPrevista, 14) as HPrevista ");
            strSQL.Append("     from afastamento as a  ");
            strSQL.Append(" left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblempregado as b  ");
            strSQL.Append(" on ( a.idEmpregado = b.nId_Empregado )  ");
            strSQL.Append(" left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEmpregado_Funcao as c  ");
            strSQL.Append(" on ( b.nId_Empregado = c.nId_Empregado   ");

            if (xEmp == 1)
            {
                strSQL.Append(" and c.nId_Empr = " + xIdCliente.ToString() + " ) ");
            }
            else if (xEmp == 2)
            {
                strSQL.Append(" and c.nId_Empr in (  select idpessoa from juridica where idjuridica = " + xIdCliente.ToString() + " or idjuridicapai = " + xIdCliente.ToString() + " )  ) ");
            }
            else if (xEmp == 3)
            {
                strSQL.Append(" and c.nId_Empr in (  select idpessoa from juridica where idgrupoempresa in ( select idgrupoempresa from juridica where idpessoa = " + xIdCliente.ToString() + " ) )  ) ");
            }
            else if (xEmp == 0)
            {
                strSQL.Append(" ) ");
            }

            strSQL.Append(" join (   ");
            strSQL.Append("    select convert( char(15), nId_Empregado ) + convert( char(10),max( hDt_Inicio ),103 ) as Chave  ");
            strSQL.Append("    from  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblempregado_Funcao  ");

            if (xEmp == 1)
            {
                strSQL.Append(" where nId_Empr = " + xIdCliente.ToString() + " ");
            }
            else if (xEmp == 2)
            {
                strSQL.Append(" where nId_Empr in (  select idpessoa from juridica where idjuridica = " + xIdCliente.ToString() + " or idjuridicapai = " + xIdCliente.ToString() + " ) ");
            }
            else if (xEmp == 3)
            {
                strSQL.Append(" where nId_Empr in (  select idpessoa from juridica where idgrupoempresa in ( select idgrupoempresa from juridica where idpessoa = " + xIdCliente.ToString() + " ) ) ");
            }
            else if (xEmp == 0)
            {
                strSQL.Append(" where nId_Empr is not null  and nid_Empr in ( select idpessoa from pessoa where isinativo=0) ");
            }


            strSQL.Append("    group by nId_Empregado  ");
            strSQL.Append(" ) as tx80 on tx80.Chave = convert( char(15), c.nId_Empregado ) + convert( char(10),c.hDt_Inicio,103 )  ");

            strSQL.Append("     left join Cid as g   on(a.IdCid2 = g.IdCid)  ");
            strSQL.Append("     left join acidente as k   on(a.idAcidente = k.IdAcidente)  ");
            strSQL.Append("     left join cat as m   on(k.IdCat = m.IdCat)  ");
            strSQL.Append("     left join pessoa as r   on(c.nId_Empr = r.IdPessoa)  ");

            if (xEmp == 1)
            {
                strSQL.Append(" where c.nId_Empr = " + xIdCliente.ToString() + " ");
            }
            else if (xEmp == 2)
            {
                strSQL.Append(" where c.nId_Empr in (  select idpessoa from juridica where idjuridica = " + xIdCliente.ToString() + " or idjuridicapai = " + xIdCliente.ToString() + " ) ");
            }
            else if (xEmp == 3)
            {
                strSQL.Append(" where c.nId_Empr in (  select idpessoa from juridica where idgrupoempresa in ( select idgrupoempresa from juridica where idpessoa = " + xIdCliente.ToString() + " ) ) ");
            }
            else if (xEmp == 0)
            {
                strSQL.Append(" where c.nId_Empr is not null ");
            }

            strSQL.Append(" and a.DataInicial between  convert( smalldatetime, '" + xD_Inicial + "', 103 ) and convert( smalldatetime, '" + xD_Final + "', 103 )  ");
            strSQL.Append("     and b.tno_Empg is not null  and g.Descricao is not null  ");
            strSQL.Append("     group by r.nomeAbreviado, r.NomeCodigo, g.Descricao, DataInicial, DataVolta, DataPrevista  ");
            strSQL.Append("  ) as tx98  ");

            strSQL.Append("union  ");


            strSQL.Append("   select CNPJ, cid1 as CID, Empregados, DInicial, HInicial, DVolta, HVolta, DPrevista, HPrevista  from(  ");
            strSQL.Append("     select count(distinct b.nId_Empregado) as Empregados, r.Nomeabreviado + '  ' + r.NomeCodigo as CNPJ, g.Descricao as Cid1, '01/01/2015' as Data1, '30/05/2017' as Data2,  ");
            strSQL.Append("     convert(char(10), a.DataInicial, 103) as DInicial, convert(char(8), a.DataInicial, 14) as HInicial, convert(char(10), a.DataVolta, 103) as DVolta, convert(char(8), a.DataVolta, 14) as HVolta, convert(char(10), a.DataPrevista, 103) as DPrevista, convert(char(8), a.DataPrevista, 14) as HPrevista ");
            strSQL.Append("     from afastamento as a  ");
            strSQL.Append(" left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblempregado as b  ");
            strSQL.Append(" on ( a.idEmpregado = b.nId_Empregado )  ");
            strSQL.Append(" left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEmpregado_Funcao as c  ");
            strSQL.Append(" on ( b.nId_Empregado = c.nId_Empregado   ");

            if (xEmp == 1)
            {
                strSQL.Append(" and c.nId_Empr = " + xIdCliente.ToString() + " ) ");
            }
            else if (xEmp == 2)
            {
                strSQL.Append(" and c.nId_Empr in (  select idpessoa from juridica where idjuridica = " + xIdCliente.ToString() + " or idjuridicapai = " + xIdCliente.ToString() + " )  ) ");
            }
            else if (xEmp == 3)
            {
                strSQL.Append(" and c.nId_Empr in (  select idpessoa from juridica where idgrupoempresa in ( select idgrupoempresa from juridica where idpessoa = " + xIdCliente.ToString() + " ) )  ) ");
            }
            else if (xEmp == 0)
            {
                strSQL.Append(" ) ");
            }

            strSQL.Append(" join (   ");
            strSQL.Append("    select convert( char(15), nId_Empregado ) + convert( char(10),max( hDt_Inicio ),103 ) as Chave  ");
            strSQL.Append("    from  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblempregado_Funcao  ");

            if (xEmp == 1)
            {
                strSQL.Append(" where nId_Empr = " + xIdCliente.ToString() + " ");
            }
            else if (xEmp == 2)
            {
                strSQL.Append(" where nId_Empr in (  select idpessoa from juridica where idjuridica = " + xIdCliente.ToString() + " or idjuridicapai = " + xIdCliente.ToString() + " ) ");
            }
            else if (xEmp == 3)
            {
                strSQL.Append(" where nId_Empr in (  select idpessoa from juridica where idgrupoempresa in ( select idgrupoempresa from juridica where idpessoa = " + xIdCliente.ToString() + " ) ) ");
            }
            else if (xEmp == 0)
            {
                strSQL.Append(" where nId_Empr is not null and nid_Empr in ( select idpessoa from pessoa where isinativo=0)  ");
            }


            strSQL.Append("    group by nId_Empregado  ");
            strSQL.Append(" ) as tx80 on tx80.Chave = convert( char(15), c.nId_Empregado ) + convert( char(10),c.hDt_Inicio,103 )  ");

            strSQL.Append("     left join Cid as g   on(a.IdCid3 = g.IdCid)  ");
            strSQL.Append("     left join acidente as k   on(a.idAcidente = k.IdAcidente)  ");
            strSQL.Append("     left join cat as m   on(k.IdCat = m.IdCat)  ");
            strSQL.Append("     left join pessoa as r   on(c.nId_Empr = r.IdPessoa)  ");

            if (xEmp == 1)
            {
                strSQL.Append(" where c.nId_Empr = " + xIdCliente.ToString() + " ");
            }
            else if (xEmp == 2)
            {
                strSQL.Append(" where c.nId_Empr in (  select idpessoa from juridica where idjuridica = " + xIdCliente.ToString() + " or idjuridicapai = " + xIdCliente.ToString() + " ) ");
            }
            else if (xEmp == 3)
            {
                strSQL.Append(" where c.nId_Empr in (  select idpessoa from juridica where idgrupoempresa in ( select idgrupoempresa from juridica where idpessoa = " + xIdCliente.ToString() + " ) ) ");
            }
            else if (xEmp == 0)
            {
                strSQL.Append(" where c.nId_Empr is not null ");
            }

            strSQL.Append(" and a.DataInicial between  convert( smalldatetime, '" + xD_Inicial + "', 103 ) and convert( smalldatetime, '" + xD_Final + "', 103 )  ");
            strSQL.Append("     and b.tno_Empg is not null  and g.Descricao is not null  ");
            strSQL.Append("     group by r.nomeAbreviado, r.NomeCodigo, g.Descricao, DataInicial, DataVolta, DataPrevista  ");
            strSQL.Append("  ) as tx97  ");

            strSQL.Append("union  ");


            strSQL.Append("   select CNPJ, cid1 as CID, Empregados, DInicial, HInicial, DVolta, HVolta, DPrevista, HPrevista  from(  ");
            strSQL.Append("     select count(distinct b.nId_Empregado) as Empregados, r.Nomeabreviado + '  ' + r.NomeCodigo as CNPJ, g.Descricao as Cid1, '01/01/2015' as Data1, '30/05/2017' as Data2,  ");
            strSQL.Append("     convert(char(10), a.DataInicial, 103) as DInicial, convert(char(8), a.DataInicial, 14) as HInicial, convert(char(10), a.DataVolta, 103) as DVolta, convert(char(8), a.DataVolta, 14) as HVolta, convert(char(10), a.DataPrevista, 103) as DPrevista, convert(char(8), a.DataPrevista, 14) as HPrevista ");
            strSQL.Append("     from afastamento as a  ");
            strSQL.Append(" left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblempregado as b  ");
            strSQL.Append(" on ( a.idEmpregado = b.nId_Empregado )  ");
            strSQL.Append(" left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEmpregado_Funcao as c  ");
            strSQL.Append(" on ( b.nId_Empregado = c.nId_Empregado   ");

            if (xEmp == 1)
            {
                strSQL.Append(" and c.nId_Empr = " + xIdCliente.ToString() + " ) ");
            }
            else if (xEmp == 2)
            {
                strSQL.Append(" and c.nId_Empr in (  select idpessoa from juridica where idjuridica = " + xIdCliente.ToString() + " or idjuridicapai = " + xIdCliente.ToString() + " )  ) ");
            }
            else if (xEmp == 3)
            {
                strSQL.Append(" and c.nId_Empr in (  select idpessoa from juridica where idgrupoempresa in ( select idgrupoempresa from juridica where idpessoa = " + xIdCliente.ToString() + " ) )  ) ");
            }
            else if (xEmp == 0)
            {
                strSQL.Append(" ) ");
            }

            strSQL.Append(" join (   ");
            strSQL.Append("    select convert( char(15), nId_Empregado ) + convert( char(10),max( hDt_Inicio ),103 ) as Chave  ");
            strSQL.Append("    from  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblempregado_Funcao  ");

            if (xEmp == 1)
            {
                strSQL.Append(" where nId_Empr = " + xIdCliente.ToString() + " ");
            }
            else if (xEmp == 2)
            {
                strSQL.Append(" where nId_Empr in (  select idpessoa from juridica where idjuridica = " + xIdCliente.ToString() + " or idjuridicapai = " + xIdCliente.ToString() + " ) ");
            }
            else if (xEmp == 3)
            {
                strSQL.Append(" where nId_Empr in (  select idpessoa from juridica where idgrupoempresa in ( select idgrupoempresa from juridica where idpessoa = " + xIdCliente.ToString() + " ) ) ");
            }
            else if (xEmp == 0)
            {
                strSQL.Append(" where nId_Empr is not null  and nid_Empr in ( select idpessoa from pessoa where isinativo=0) ");
            }


            strSQL.Append("    group by nId_Empregado  ");
            strSQL.Append(" ) as tx80 on tx80.Chave = convert( char(15), c.nId_Empregado ) + convert( char(10),c.hDt_Inicio,103 )  ");

            strSQL.Append("     left join Cid as g   on(a.IdCid4 = g.IdCid)  ");
            strSQL.Append("     left join acidente as k   on(a.idAcidente = k.IdAcidente)  ");
            strSQL.Append("     left join cat as m   on(k.IdCat = m.IdCat)  ");
            strSQL.Append("     left join pessoa as r   on(c.nId_Empr = r.IdPessoa)  ");

            if (xEmp == 1)
            {
                strSQL.Append(" where c.nId_Empr = " + xIdCliente.ToString() + " ");
            }
            else if (xEmp == 2)
            {
                strSQL.Append(" where c.nId_Empr in (  select idpessoa from juridica where idjuridica = " + xIdCliente.ToString() + " or idjuridicapai = " + xIdCliente.ToString() + " ) ");
            }
            else if (xEmp == 3)
            {
                strSQL.Append(" where c.nId_Empr in (  select idpessoa from juridica where idgrupoempresa in ( select idgrupoempresa from juridica where idpessoa = " + xIdCliente.ToString() + " ) ) ");
            }
            else if (xEmp == 0)
            {
                strSQL.Append(" where c.nId_Empr is not null ");
            }

            strSQL.Append(" and a.DataInicial between  convert( smalldatetime, '" + xD_Inicial + "', 103 ) and convert( smalldatetime, '" + xD_Final + "', 103 )  ");
            strSQL.Append("     and b.tno_Empg is not null  and g.Descricao is not null  ");
            strSQL.Append("     group by r.nomeAbreviado, r.NomeCodigo, g.Descricao, DataInicial, DataVolta, DataPrevista  ");
            strSQL.Append("  ) as tx96  ");


            strSQL.Append("  ) as tx100  ");
            strSQL.Append("  group by CNPJ, CID, DInicial, HInicial, DVolta, HVolta, DPrevista, HPrevista  ");
            strSQL.Append("  order by CNPJ, CID  ");


            using (SqlConnection cnn = new SqlConnection(Ilitera.Data.SQLServer.EntitySQLServer.GetConnection()))
            {
                SqlDataAdapter da;
                cnn.Open();


                da = new SqlDataAdapter(strSQL.ToString(), cnn);
                da.SelectCommand.CommandTimeout = 900;
                da.Fill(m_ds, "Result");

                cnn.Close();

                da.Dispose();
            }

            return m_ds;



        }



        public DataSet Gerar_DS_Relatorio_Laudos_Grafico(int xIdCliente, string xD_Inicial, string xD_Final, Int16 xEmp, string zTipo)
        {
            //string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection();
            StringBuilder strSQL = new StringBuilder();

            //DataSet m_ds;


            DataTable table = new DataTable("Result");
            table.Columns.Add("Situacao", Type.GetType("System.String"));
            table.Columns.Add("Tipo", Type.GetType("System.String"));
            table.Columns.Add("Empresa_Pai", Type.GetType("System.String"));
            table.Columns.Add("Empresa", Type.GetType("System.String"));
            table.Columns.Add("Data1", Type.GetType("System.String"));
            table.Columns.Add("Data2", Type.GetType("System.String"));
            table.Columns.Add("Total", Type.GetType("System.Int16"));
            table.Columns.Add("IdJuridicaPai", Type.GetType("System.Int32"));


            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);



            strSQL.Append("select Situacao, Tipo, Empresa_pai, '" + xD_Inicial.Substring(0, 10) + "' as Data1, '" + xD_Final.Substring(0, 10) + "' as Data2, sum(laudos) as Total, IdJuridicaPai from  (  ");

            if (zTipo == "ppravencer" || zTipo == "")
            {
                strSQL.Append("	select case when d.NomeAbreviado is null then b.NomeAbreviado else d.NomeAbreviado end as Empresa_Pai, b.NomeAbreviado, 'A Vencer' as Situacao, 'PPRA' as Tipo, ");
                strSQL.Append("	case when dateadd(year, 1, max(a.hDT_LAUDO)) >= getdate() then 1 else 0 end as Laudos, c.IdJuridicaPai ");
                strSQL.Append("	from pessoa as b ");
                strSQL.Append("	left join juridica as c on(b.idpessoa = c.idpessoa) ");
                strSQL.Append("	left join pessoa as d on(c.IdJuridicaPai = d.idpessoa) ");
                strSQL.Append("	left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tbllaudo_tec as a  on(a.nid_empr = b.idpessoa) ");
                strSQL.Append("	where b.isinativo = 0 ");

                if (xEmp == 1)
                {
                    strSQL.Append(" and b.IdPessoa = " + xIdCliente.ToString() + "  ");
                }
                else if (xEmp == 2)
                {
                    strSQL.Append(" and b.IdPessoa in (  select idpessoa from juridica where idjuridica = " + xIdCliente.ToString() + " or idjuridicapai = " + xIdCliente.ToString() + " )   ");
                }
                else if (xEmp == 3)
                {
                    strSQL.Append(" and b.IdPessoa in (  select idpessoa from juridica where idgrupoempresa in ( select idgrupoempresa from juridica where idpessoa = " + xIdCliente.ToString() + " ) )   ");
                }
                else if (xEmp == 0)
                {
                    strSQL.Append(" ) ");
                }

                strSQL.Append("     group by d.NomeAbreviado, b.nomeAbreviado, c.IdJuridicaPai  ");

            }

            if (zTipo == "")
                strSQL.Append(" union ");


            if (zTipo == "ppravencidos" || zTipo == "")
            {

                strSQL.Append("	select case when d.NomeAbreviado is null then b.NomeAbreviado else d.NomeAbreviado end as Empresa_Pai, b.NomeAbreviado, 'Vencidos' as Situacao, 'PPRA' as Tipo, ");
                strSQL.Append("	case when dateadd(year, 1, max(a.hDT_LAUDO)) >= getdate() then 0 else 1 end as Laudos, c.IdJuridicaPai ");
                strSQL.Append("	from pessoa as b ");
                strSQL.Append("	left join juridica as c on(b.idpessoa = c.idpessoa) ");
                strSQL.Append("	left join pessoa as d on(c.IdJuridicaPai = d.idpessoa) ");
                strSQL.Append("	left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tbllaudo_tec as a  on(a.nid_empr = b.idpessoa) ");
                strSQL.Append("	where b.isinativo = 0 ");

                if (xEmp == 1)
                {
                    strSQL.Append(" and b.IdPessoa = " + xIdCliente.ToString() + "  ");
                }
                else if (xEmp == 2)
                {
                    strSQL.Append(" and b.IdPessoa in (  select idpessoa from juridica where idjuridica = " + xIdCliente.ToString() + " or idjuridicapai = " + xIdCliente.ToString() + " )   ");
                }
                else if (xEmp == 3)
                {
                    strSQL.Append(" and b.IdPessoa in (  select idpessoa from juridica where idgrupoempresa in ( select idgrupoempresa from juridica where idpessoa = " + xIdCliente.ToString() + " ) )   ");
                }
                else if (xEmp == 0)
                {
                    strSQL.Append(" ) ");
                }

                strSQL.Append("     group by d.NomeAbreviado, b.nomeAbreviado, c.IdJuridicaPai  ");


            }

            if (zTipo == "")
                strSQL.Append(" union ");


            if (zTipo == "pcmsovencer" || zTipo == "")
            {

                strSQL.Append("	select case when d.NomeAbreviado is null then b.NomeAbreviado else d.NomeAbreviado end as Empresa_Pai, b.NomeAbreviado, 'A Vencer' as Situacao, 'PCMSO' as Tipo, ");
                strSQL.Append("	case when dateadd(year, 1, max(dataPCMSO)) >= getdate() then 1 else 0 end as Laudos, c.IdJuridicaPai ");
                strSQL.Append("	from pessoa as b ");
                strSQL.Append("	left join juridica as c on(b.idpessoa = c.idpessoa) ");
                strSQL.Append("	left join pessoa as d on(c.IdJuridicaPai = d.idpessoa) ");
                strSQL.Append("	left join documento as a  on(a.idCliente = b.idpessoa) ");
                strSQL.Append("	left join pcmso as p on(a.iddocumento = p.iddocumento) ");
                strSQL.Append("	where b.isinativo = 0 ");

                if (xEmp == 1)
                {
                    strSQL.Append(" and b.IdPessoa = " + xIdCliente.ToString() + "  ");
                }
                else if (xEmp == 2)
                {
                    strSQL.Append(" and b.IdPessoa in (  select idpessoa from juridica where idjuridica = " + xIdCliente.ToString() + " or idjuridicapai = " + xIdCliente.ToString() + " )   ");
                }
                else if (xEmp == 3)
                {
                    strSQL.Append(" and b.IdPessoa in (  select idpessoa from juridica where idgrupoempresa in ( select idgrupoempresa from juridica where idpessoa = " + xIdCliente.ToString() + " ) )   ");
                }
                else if (xEmp == 0)
                {
                    strSQL.Append(" ) ");
                }

                strSQL.Append("     group by d.NomeAbreviado, b.nomeAbreviado, c.IdJuridicaPai  ");


            }

            if (zTipo == "")
                strSQL.Append(" union ");


            if (zTipo == "pcmsovencidos" || zTipo == "")
            {

                strSQL.Append("	select case when d.NomeAbreviado is null then b.NomeAbreviado else d.NomeAbreviado end as Empresa_Pai, b.NomeAbreviado, 'Vencidos' as Situacao, 'PCMSO' as Tipo, ");
                strSQL.Append("	case when dateadd(year, 1, max(dataPCMSO)) >= getdate() then 0 else 1 end as Laudos, c.IdJuridicaPai ");
                strSQL.Append("	from pessoa as b ");
                strSQL.Append("	left join juridica as c on(b.idpessoa = c.idpessoa) ");
                strSQL.Append("	left join pessoa as d on(c.IdJuridicaPai = d.idpessoa) ");
                strSQL.Append("	left join documento as a  on(a.idCliente = b.idpessoa) ");
                strSQL.Append("	left join pcmso as p on(a.iddocumento = p.iddocumento) ");
                strSQL.Append("	where b.isinativo = 0 ");

                if (xEmp == 1)
                {
                    strSQL.Append(" and b.IdPessoa = " + xIdCliente.ToString() + "  ");
                }
                else if (xEmp == 2)
                {
                    strSQL.Append(" and b.IdPessoa in (  select idpessoa from juridica where idjuridica = " + xIdCliente.ToString() + " or idjuridicapai = " + xIdCliente.ToString() + " )   ");
                }
                else if (xEmp == 3)
                {
                    strSQL.Append(" and b.IdPessoa in (  select idpessoa from juridica where idgrupoempresa in ( select idgrupoempresa from juridica where idpessoa = " + xIdCliente.ToString() + " ) )   ");
                }
                else if (xEmp == 0)
                {
                    strSQL.Append(" ) ");
                }

                strSQL.Append("     group by d.NomeAbreviado, b.nomeAbreviado, c.IdJuridicaPai  ");
            }

            strSQL.Append(") ");
            strSQL.Append("as tx88 ");
            strSQL.Append("where idjuridicapai is not null ");
            strSQL.Append("group by situacao, tipo, empresa_pai, IdJuridicaPai ");
            strSQL.Append("order by empresa_pai, tipo, situacao ");


            using (SqlConnection cnn = new SqlConnection(Ilitera.Data.SQLServer.EntitySQLServer.GetConnection()))
            {
                SqlDataAdapter da;
                cnn.Open();


                da = new SqlDataAdapter(strSQL.ToString(), cnn);
                da.SelectCommand.CommandTimeout = 900;
                da.Fill(m_ds, "Result");

                cnn.Close();

                da.Dispose();
            }

            return m_ds;



        }



        public DataSet Gerar_DS_Relatorio_Laudos_SubGrafico(int xIdCliente, string xD_Inicial, string xD_Final, Int16 xEmp, string zTipo, Int32 zIdJuridicaPai)
        {
            //string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection();
            StringBuilder strSQL = new StringBuilder();

            //DataSet m_ds;


            DataTable table = new DataTable("Result");
            table.Columns.Add("Situacao", Type.GetType("System.String"));
            table.Columns.Add("Tipo", Type.GetType("System.String"));
            table.Columns.Add("Empresa_Pai", Type.GetType("System.String"));
            table.Columns.Add("Empresa", Type.GetType("System.String"));
            table.Columns.Add("Data1", Type.GetType("System.String"));
            table.Columns.Add("Data2", Type.GetType("System.String"));
            table.Columns.Add("Total", Type.GetType("System.Int16"));
            table.Columns.Add("IdPessoa", Type.GetType("System.Int32"));


            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);



            strSQL.Append("select Situacao, Tipo, Empresa_pai, '" + xD_Inicial.Substring(0, 10) + "' as Data1, '" + xD_Final.Substring(0, 10) + "' as Data2, sum(laudos) as Total, IdPessoa from  (  ");

            if (zTipo == "ppravencer" || zTipo == "")
            {
                strSQL.Append("	select b.NomeAbreviado as Empresa_Pai, b.NomeAbreviado, 'A Vencer' as Situacao, 'PPRA' as Tipo, ");
                strSQL.Append("	case when dateadd(year, 1, max(a.hDT_LAUDO)) >= getdate() then 1 else 0 end as Laudos, b.IdPessoa ");
                strSQL.Append("	from pessoa as b ");
                strSQL.Append("	left join juridica as c on(b.idpessoa = c.idpessoa) ");
                strSQL.Append("	left join pessoa as d on(c.IdJuridicaPai = d.idpessoa) ");
                strSQL.Append("	left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tbllaudo_tec as a  on(a.nid_empr = b.idpessoa) ");
                strSQL.Append("	where b.isinativo = 0 ");

                strSQL.Append(" and b.IdPessoa in (  select idpessoa from juridica where idjuridicapai = " + zIdJuridicaPai.ToString() + "   ) ");

                strSQL.Append("     group by d.NomeAbreviado, b.nomeAbreviado, b.IdPessoa  ");

            }

            if (zTipo == "")
                strSQL.Append(" union ");


            if (zTipo == "ppravencidos" || zTipo == "")
            {

                strSQL.Append("	select b.NomeAbreviado as Empresa_Pai, b.NomeAbreviado, 'Vencidos' as Situacao, 'PPRA' as Tipo, ");
                strSQL.Append("	case when dateadd(year, 1, max(a.hDT_LAUDO)) >= getdate() then 0 else 1 end as Laudos, b.IdPessoa ");
                strSQL.Append("	from pessoa as b ");
                strSQL.Append("	left join juridica as c on(b.idpessoa = c.idpessoa) ");
                strSQL.Append("	left join pessoa as d on(c.IdJuridicaPai = d.idpessoa) ");
                strSQL.Append("	left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tbllaudo_tec as a  on(a.nid_empr = b.idpessoa) ");
                strSQL.Append("	where b.isinativo = 0 ");

                strSQL.Append(" and b.IdPessoa in (  select idpessoa from juridica where idjuridicapai = " + zIdJuridicaPai.ToString() + "   ) ");

                strSQL.Append("     group by d.NomeAbreviado, b.nomeAbreviado, b.IdPessoa  ");


            }

            if (zTipo == "")
                strSQL.Append(" union ");


            if (zTipo == "pcmsovencer" || zTipo == "")
            {

                strSQL.Append("	select b.NomeAbreviado as Empresa_Pai, b.NomeAbreviado, 'A Vencer' as Situacao, 'PCMSO' as Tipo, ");
                strSQL.Append("	case when dateadd(year, 1, max(dataPCMSO)) >= getdate() then 1 else 0 end as Laudos, b.IdPessoa ");
                strSQL.Append("	from pessoa as b ");
                strSQL.Append("	left join juridica as c on(b.idpessoa = c.idpessoa) ");
                strSQL.Append("	left join pessoa as d on(c.IdJuridicaPai = d.idpessoa) ");
                strSQL.Append("	left join documento as a  on(a.idCliente = b.idpessoa) ");
                strSQL.Append("	left join pcmso as p on(a.iddocumento = p.iddocumento) ");
                strSQL.Append("	where b.isinativo = 0 ");

                strSQL.Append(" and b.IdPessoa in (  select idpessoa from juridica where idjuridicapai = " + zIdJuridicaPai.ToString() + "   ) ");

                strSQL.Append("     group by d.NomeAbreviado, b.nomeAbreviado, b.IdPessoa  ");


            }

            if (zTipo == "")
                strSQL.Append(" union ");


            if (zTipo == "pcmsovencidos" || zTipo == "")
            {

                strSQL.Append("	select b.NomeAbreviado as Empresa_Pai, b.NomeAbreviado, 'Vencidos' as Situacao, 'PCMSO' as Tipo, ");
                strSQL.Append("	case when dateadd(year, 1, max(dataPCMSO)) >= getdate() then 0 else 1 end as Laudos, b.IdPessoa ");
                strSQL.Append("	from pessoa as b ");
                strSQL.Append("	left join juridica as c on(b.idpessoa = c.idpessoa) ");
                strSQL.Append("	left join pessoa as d on(c.IdJuridicaPai = d.idpessoa) ");
                strSQL.Append("	left join documento as a  on(a.idCliente = b.idpessoa) ");
                strSQL.Append("	left join pcmso as p on(a.iddocumento = p.iddocumento) ");
                strSQL.Append("	where b.isinativo = 0 ");

                strSQL.Append(" and b.IdPessoa in (  select idpessoa from juridica where idjuridicapai = " + zIdJuridicaPai.ToString() + "   ) ");

                strSQL.Append("     group by d.NomeAbreviado, b.nomeAbreviado, b.IdPessoa  ");
            }

            strSQL.Append(") ");
            strSQL.Append("as tx88 ");
            strSQL.Append("group by situacao, tipo, empresa_pai, IdPessoa ");
            strSQL.Append("having sum(laudos) > 0 ");
            strSQL.Append("order by empresa_pai, tipo, situacao ");


            using (SqlConnection cnn = new SqlConnection(Ilitera.Data.SQLServer.EntitySQLServer.GetConnection()))
            {
                SqlDataAdapter da;
                cnn.Open();


                da = new SqlDataAdapter(strSQL.ToString(), cnn);
                da.SelectCommand.CommandTimeout = 900;
                da.Fill(m_ds, "Result");

                cnn.Close();

                da.Dispose();
            }

            return m_ds;



        }





        public String Trazer_GHE_Atual_Colaborador(Int32 zIdEmpregado_Funcao)
        {
            //string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection();
            StringBuilder strSQL = new StringBuilder();

            //DataSet m_ds;


            DataTable table = new DataTable("Result");
            table.Columns.Add("GHE", Type.GetType("System.String"));




            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);


            strSQL.Append(" select top 1 'Laudo ' + convert( char(10), b.hdt_laudo, 103 ) + ' - ' + d.tNo_Func as GHE ");
            strSQL.Append(" from tblFUNC_EMPREGADO as a ");
            strSQL.Append(" left join tblLaudo_Tec as b on(a.nID_LAUD_TEC = b.nID_LAUD_TEC) ");
            strSQL.Append(" left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.Pedido as c on(b.nID_PEDIDO = c.IdPedido) ");
            strSQL.Append(" left join tblFunc as d on(a.nid_Func = d.nId_Func) ");
            strSQL.Append(" where nID_EMPREGADO_FUNCAO = " + zIdEmpregado_Funcao.ToString() + " and c.IndStatus = 2 ");
            strSQL.Append(" order by hdt_laudo desc ");



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
                return "( Não alocado )";
            else
                return m_ds.Tables[0].Rows[0][0].ToString();

        }


        public String Trazer_Cod_GHE_Atual_Colaborador(Int32 zIdEmpregado)
        {
            //string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection();
            StringBuilder strSQL = new StringBuilder();

            //DataSet m_ds;


            DataTable table = new DataTable("Result");
            table.Columns.Add("GHE", Type.GetType("System.String"));




            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);


            strSQL.Append(" select top 1 d.nId_Func as GHE ");
            strSQL.Append(" from tblFUNC_EMPREGADO as a ");
            strSQL.Append(" left join tblLaudo_Tec as b on(a.nID_LAUD_TEC = b.nID_LAUD_TEC) ");
            strSQL.Append(" left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.Pedido as c on(b.nID_PEDIDO = c.IdPedido) ");
            strSQL.Append(" left join tblFunc as d on(a.nid_Func = d.nId_Func) ");
            strSQL.Append(" where nID_EMPREGADO_FUNCAO in ");
            strSQL.Append(" ( select top 1 nId_Empregado_Funcao from tblEMPREGADO_FUNCAO where nid_empregado = " + zIdEmpregado.ToString() + "  order by hdt_inicio desc )  and c.IndStatus = 2 ");
            strSQL.Append(" order by hdt_laudo desc ");



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
                return " ";
            else
                return m_ds.Tables[0].Rows[0][0].ToString().Trim();

        }



        public Boolean Retornar_Acesso_Repositorio_Consulta(Int32 xIdUsuario)
        {


            //string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection();
            //StringBuilder strSQL = new StringBuilder();

            //DataSet m_ds;


            DataTable table = new DataTable("Result");
            table.Columns.Add("IdUsuarioGrupo", Type.GetType("System.Int32"));


            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);


            SqlCommand rCommand = new SqlCommand();

            rCommand.Parameters.Add("@xIdUsuario", SqlDbType.NChar).Value = xIdUsuario.ToString();


            rCommand.CommandText = "select * from usuariogrupo " +
                                   " where idgrupo in (select idgrupo from grupo where NomeGrupo = 'Web - Repositório Consulta' ) " +
                                   " and idusuario = @xIdUsuario ";


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

            if (m_ds.Tables[0].Rows.Count > 0)
                return true;
            else
                return false;

        }



        public DataSet Trazer_Dados_Importacao(int xIdImportacao)
        {
            //string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection();
            StringBuilder strSQL = new StringBuilder();

            //DataSet m_ds;


            DataTable table = new DataTable("Result");
            table.Columns.Add("Colaborador", Type.GetType("System.String"));
            table.Columns.Add("NomeUsuario", Type.GetType("System.String"));
            table.Columns.Add("DataImportacao", Type.GetType("System.String"));
            table.Columns.Add("Cliente", Type.GetType("System.String"));
            table.Columns.Add("Status", Type.GetType("System.String"));
            table.Columns.Add("tno_Empg", Type.GetType("System.String"));
            table.Columns.Add("tno_CPF", Type.GetType("System.String"));
            table.Columns.Add("tnum_Ctps", Type.GetType("System.String"));
            table.Columns.Add("tser_ctps", Type.GetType("System.String"));
            table.Columns.Add("tuf_cpts", Type.GetType("System.String"));
            table.Columns.Add("tno_Identidade", Type.GetType("System.String"));
            table.Columns.Add("tSexo", Type.GetType("System.String"));
            table.Columns.Add("dt_Nasc", Type.GetType("System.String"));
            table.Columns.Add("dt_Adm", Type.GetType("System.String"));
            table.Columns.Add("temail", Type.GetType("System.String"));
            table.Columns.Add("temail_Resp", Type.GetType("System.String"));
            table.Columns.Add("tcod_Empr", Type.GetType("System.String"));
            table.Columns.Add("nNO_PIS_PASEP", Type.GetType("System.String"));
            table.Columns.Add("setor", Type.GetType("System.String"));
            table.Columns.Add("cargo", Type.GetType("System.String"));
            table.Columns.Add("funcao", Type.GetType("System.String"));

            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);



            strSQL.Append(" select b.tno_Empg as Colaborador, d.NomeUsuario, c.DataImportacao, e.NomeAbreviado as Cliente, a.Status,  ");
            strSQL.Append(" a.tno_empg, a.tno_CPF, a.tnum_Ctps, a.tser_ctps, a.tuf_cpts, a.tno_identidade, a.tsexo, ");
            strSQL.Append(" convert(char(10), a.hdt_nasc, 103) as dt_Nasc, convert(char(10), a.hdt_adm, 103) as dt_adm, ");
            strSQL.Append(" a.temail, a.teMail_Resp, a.tcod_empr, a.nNO_PIS_PASEP, setor, cargo, funcao ");
            strSQL.Append(" from tblImportacao_Detalhes as a ");
            strSQL.Append(" left join tblEmpregado as b on(a.nid_empregado = b.nid_empregado) ");
            strSQL.Append(" left join tblimportacao as c on(a.nId_Importacao = c.nId_Importacao) ");
            strSQL.Append(" left join  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.usuario as d on(c.idusuario = d.idusuario) ");
            strSQL.Append(" left join  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.pessoa as e on(c.nId_Empr = e.idpessoa) ");
            strSQL.Append(" where a.nid_importacao = " + xIdImportacao.ToString() + " ");
            strSQL.Append(" order by b.tno_Empg ");



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

            return m_ds;

        }




        public DataSet Trazer_Dados_Importacao_Transf(int xIdImportacao)
        {
            //string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection();
            StringBuilder strSQL = new StringBuilder();

            //DataSet m_ds;


            DataTable table = new DataTable("Result");
            table.Columns.Add("Colaborador", Type.GetType("System.String"));
            table.Columns.Add("NomeUsuario", Type.GetType("System.String"));
            table.Columns.Add("DataImportacao", Type.GetType("System.String"));
            table.Columns.Add("Cliente", Type.GetType("System.String"));
            table.Columns.Add("Status", Type.GetType("System.String"));
            table.Columns.Add("CPF", Type.GetType("System.String"));
            table.Columns.Add("Filial_Origem", Type.GetType("System.String"));
            table.Columns.Add("CNPJ_Origem", Type.GetType("System.String"));
            table.Columns.Add("Filial_Destino", Type.GetType("System.String"));
            table.Columns.Add("CNPJ_Destino", Type.GetType("System.String"));
            table.Columns.Add("DataInicial", Type.GetType("System.String"));
            table.Columns.Add("Setor", Type.GetType("System.String"));
            table.Columns.Add("Funcao", Type.GetType("System.String"));
            table.Columns.Add("Inativar_Origem", Type.GetType("System.String"));
            table.Columns.Add("DataDemissao", Type.GetType("System.String"));
            table.Columns.Add("GHE", Type.GetType("System.String"));

            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);



            strSQL.Append(" select b.tno_Empg as Colaborador, d.NomeUsuario, c.DataImportacao, e.NomeAbreviado as Cliente, a.Status,  ");
            strSQL.Append(" a.CPF, a.Filial_Origem, a.CNPJ_Origem, a.Filial_Destino, a.CNPJ_Destino, isnull( convert( char(10),Data_Inicial, 103 ), ' ' ) as DataInicial,  ");
            strSQL.Append(" Setor, Funcao, Inativar_Origem, isnull( convert( char(10),Data_Demissao, 103 ), ' ' ) as DataDemissao, GHE ");
            strSQL.Append(" from tblImportacaoTransf_Detalhes as a ");
            strSQL.Append(" left join tblEmpregado as b on(a.nidempregado_Origem = b.nid_empregado) ");
            strSQL.Append(" left join tblimportacaoTransf as c on(a.nId_ImportacaoTransf = c.nId_ImportacaoTransf) ");
            strSQL.Append(" left join  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.usuario as d on(c.idusuario = d.idusuario) ");
            strSQL.Append(" left join  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.pessoa as e on(a.IdEmpr_Origem = e.idpessoa) ");
            strSQL.Append(" where a.nid_importacaoTransf = " + xIdImportacao.ToString() + " ");
            strSQL.Append(" order by b.tno_Empg ");



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

            return m_ds;

        }



        public Int32 Trazer_Afastamento_Anterior_CID(int xId_Afastamento, string xDataInicial, string xCid)
        {
            //string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection();
            StringBuilder strSQL = new StringBuilder();

            //DataSet m_ds;

            DataTable table = new DataTable("Result");
            table.Columns.Add("Casos", Type.GetType("System.Int32"));


            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);


            strSQL.Append(" select count(*) as Casos ");
            strSQL.Append(" from afastamento where idempregado in ");
            strSQL.Append(" ( ");
            strSQL.Append("   select idempregado from afastamento ");
            strSQL.Append("   where idafastamento = " + xId_Afastamento.ToString() + " ");
            strSQL.Append(" ) ");
            strSQL.Append(" and datainicial between dateadd(dd, -60, convert(smalldatetime, '" + xDataInicial + "', 126)) and dateadd(dd, -1, convert(smalldatetime, '" + xDataInicial + "', 126)) ");
            strSQL.Append(" and( ");
            strSQL.Append("    idcid in " + xCid + "  or idcid2 in " + xCid + " or idcid3 in " + xCid + " or idcid4 in " + xCid + "  ");
            strSQL.Append("    ) ");

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

            if (m_ds.Tables[0].Rows.Count > 0)
            {
                return System.Convert.ToInt32(m_ds.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return 0;
            }


        }



        public void Excluir_Vacina_Setor(Int32 xIdVacina, Int32 xIdEmpresa)
        {



            using (SqlConnection cnn = new SqlConnection(Ilitera.Data.SQLServer.EntitySQLServer.GetConnection()))
            {
                cnn.Open();

                SqlCommand com = new SqlCommand("Delete from Vacina_Setor where IdVacinaTipo = " + xIdVacina.ToString() + " and IdSetor in ( select nId_Setor from " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblSetor where nId_Empr = " + xIdEmpresa.ToString() + " ) ", cnn);
                com.ExecuteNonQuery();
            }


            return;


            //using (SqlConnection cnn = new SqlConnection(Ilitera.Data.SQLServer.EntitySQLServer.GetConnection()))
            //{

            //    string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection();

            //    Database objDB = new SqlDatabase(connString);

            //    var objCmd = objDB.GetStoredProcCommand("sp_Excluir_Vacina_Setor");

            //    objDB.AddInParameter(objCmd, "xIdEmpresa", System.Data.DbType.Int32, xIdEmpresa);
            //    objDB.AddInParameter(objCmd, "xIdVacina", System.Data.DbType.Int32, xIdVacina);

            //    var readerRetorno = objDB.ExecuteReader(objCmd);

            //    return;

            //}

        }



        public DataSet Gerar_DS_Relatorio_Vacinas_CSV(int xIdCliente, string xD_Inicial, string xD_Final, string xTipoRel, string xConsiderar, Int16 xEmp)
        {
            //string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection();
            StringBuilder strSQL = new StringBuilder();

            //DataSet m_ds;
            DataTable table = new DataTable("Result");
            table.Columns.Add("Empregado", Type.GetType("System.String"));
            table.Columns.Add("DataVacina", Type.GetType("System.String"));
            table.Columns.Add("Vacina", Type.GetType("System.String"));
            table.Columns.Add("Dose", Type.GetType("System.String"));
            table.Columns.Add("Retorno", Type.GetType("System.String"));

            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);

            strSQL.Append(" SELECT Empregado, Dose, Vacina, CONVERT(char(10), CONVERT(date, DataVacina),103) as DataVacina FROM (");
            strSQL.Append(" SELECT CONVERT(char(10), a.DataVacina,103) AS Retorno, b.tNO_EMPG AS Empregado, c.Dose AS Dose, d.VacinaTipo AS Vacina, a.DataVacina AS DataVacina ");
            strSQL.Append("FROM vacina AS a ");
            strSQL.Append("LEFT JOIN " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblempregado AS b ON a.idempregado = b.nid_empregado ");
            strSQL.Append("LEFT JOIN Vacina_Dose AS c ON a.IdVacinaDose = c.IdVacinaDose ");
            strSQL.Append("LEFT JOIN Vacina_Tipo AS d ON c.IdVacinaTipo = d.IdVacinaTipo ");
            strSQL.Append("WHERE a.DataVacina BETWEEN CONVERT(smalldatetime, '" + xD_Inicial + "', 103) AND CONVERT(smalldatetime, '" + xD_Final + "', 103)");
            strSQL.Append("AND b.tNO_EMPG IS NOT NULL");

            if (xEmp == 1)
            {
                strSQL.Append(" and nId_Empr = " + xIdCliente.ToString() + "  ");
            }
            else if (xEmp == 2)
            {
                strSQL.Append(" and nId_Empr in (  select idpessoa from juridica where idjuridica = " + xIdCliente.ToString() + " or idjuridicapai = " + xIdCliente.ToString() + " )   ");
            }
            else if (xEmp == 3)
            {
                strSQL.Append(" and nId_Empr in (  select idpessoa from juridica where idgrupoempresa in ( select idgrupoempresa from juridica where idpessoa = " + xIdCliente.ToString() + " ) )   ");
            }
            strSQL.Append("UNION ");
            strSQL.Append("SELECT ");
            strSQL.Append("CONVERT(char(10), DataExame, 103) AS Retorno,");
            strSQL.Append("b.tNo_Empg AS Empregado,");

            strSQL.Append("CONVERT(varchar(100), Prontuario) AS Setor,");
            strSQL.Append("'Anti-HBS' AS Funcao,");
            strSQL.Append("DataExame AS DataVacina ");
            strSQL.Append("FROM examebase AS a ");

            strSQL.Append("LEFT JOIN SIED_NOVO.dbo.tblempregado AS b ON a.IdEmpregado = b.nid_Empregado  ");
            strSQL.Append("WHERE idexamedicionario = -1174768070  ");
            strSQL.Append(") AS tx90  ");
            strSQL.Append("ORDER BY Empregado, DataVacina DESC;");

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