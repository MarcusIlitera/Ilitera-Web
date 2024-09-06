using System;
using System.Text;
using System.Collections;
//using Entities;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Net;



namespace Ilitera.Data
{
    public class PPRA_EPI
    {


        public DataSet Trazer_Cronograma_PGR_Agrupado(Int32 xIdLaudo)
        {

            StringBuilder strSQL = new StringBuilder();


            DataTable table = new DataTable("Result");
            table.Columns.Add("GHE", Type.GetType("System.String"));
            table.Columns.Add("AcaoLocalTrab", Type.GetType("System.String"));
            table.Columns.Add("Mes01", Type.GetType("System.Boolean"));
            table.Columns.Add("Mes02", Type.GetType("System.Boolean"));
            table.Columns.Add("Mes03", Type.GetType("System.Boolean"));
            table.Columns.Add("Mes04", Type.GetType("System.Boolean"));
            table.Columns.Add("Mes05", Type.GetType("System.Boolean"));
            table.Columns.Add("Mes06", Type.GetType("System.Boolean"));
            table.Columns.Add("Mes07", Type.GetType("System.Boolean"));
            table.Columns.Add("Mes08", Type.GetType("System.Boolean"));
            table.Columns.Add("Mes09", Type.GetType("System.Boolean"));
            table.Columns.Add("Mes10", Type.GetType("System.Boolean"));
            table.Columns.Add("Mes11", Type.GetType("System.Boolean"));
            table.Columns.Add("Mes12", Type.GetType("System.Boolean"));
            table.Columns.Add("Ano", Type.GetType("System.String"));
            table.Columns.Add("Acao", Type.GetType("System.String"));
            table.Columns.Add("Serv", Type.GetType("System.String"));
            table.Columns.Add("FormaReg", Type.GetType("System.String"));
            table.Columns.Add("Prioridade", Type.GetType("System.String"));
            table.Columns.Add("Metodologia", Type.GetType("System.String"));



            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);


            strSQL.Append(" select distinct isnull(GHE,'') as GHE, Riscos_Local_Trab as AcaoLocalTrab, Mes01, Mes02, Mes03, Mes04, Mes05, Mes06, Mes07, Mes08, Mes09, Mes10, Mes11, Mes12, Ano, Serv, FormaReg, Prioridade, Metodologia, Acao ");
            strSQL.Append(" from ");
            strSQL.Append(" ( ");
            strSQL.Append("   SELECT b.tno_func as GHE, a.IdCRN_PGR, a.nID_LAUD_TEC, nQuestões, ");
            strSQL.Append("   case when substring(AcaoLocalTrab, charindex(b.tno_func, acaolocaltrab) + len(b.tno_func) + 1, 100) is null then ' ' + acaolocaltrab  ");
            strSQL.Append("   else substring(AcaoLocalTrab, charindex(b.tno_func, acaolocaltrab) + len(b.tno_func) + 1, 100) end as Riscos_Local_Trab,  ");
            strSQL.Append("   Mes01, Mes02, Mes03, Mes04, Mes05, Mes06, Mes07, Mes08, Mes09, Mes10, Mes11, Mes12, Acao, Ano, Serv, FormaReg, Prioridade, Metodologia ");
            strSQL.Append("   FROM tblCRN_PGR (nolock)  as a ");
            //strSQL.Append("   left join tblfunc as b on ( substring(a.acaolocaltrab, 1, 8) = substring(b.tno_func, 1, 8)  and b.nID_LAUD_TEC = " + xIdLaudo.ToString() + " )  ");
            strSQL.Append("   left join tblfunc (nolock)  as b on ( a.nId_Func = b.nId_Func  and b.nID_LAUD_TEC = " + xIdLaudo.ToString() + " )  ");
            strSQL.Append("   WHERE a.nID_LAUD_TEC = " + xIdLaudo.ToString() + "  ");  // + " and  ( b.nID_LAUD_TEC = " + xIdLaudo.ToString() + " or b.nID_LAUD_TEC is null ) ");         
            strSQL.Append(" ) as tx90 ");
            strSQL.Append(" order by Riscos_Local_Trab, GHE, prioridade, serv, metodologia, formareg ");


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


        public DataSet Trazer_Laudos_Consolidados(int nId_Cliente, int nIdJuridicaPai)
        {

            StringBuilder strSQL = new StringBuilder();


            DataTable table = new DataTable("Result");

            table.Columns.Add("nId_Laud_Tec", Type.GetType("System.Int32"));


            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);


            strSQL.Append("select nId_Laud_Tec from tbllaudo_tec ");
            strSQL.Append("where convert(char(12), nid_empr) + ' ' + convert(char(10), hdt_laudo, 103) in  ");
            strSQL.Append("( ");
            strSQL.Append("select convert(char(12), nid_empr) + ' ' + convert(char(10), max(hdt_laudo), 103)  from tblLaudo_tec  (nolock) ");
            strSQL.Append("  where nid_empr in (select idjuridica from " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.juridica (nolock)  where idjuridicapai = " + nIdJuridicaPai.ToString() + "  and idpessoa in (select idpessoa from " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.pessoa (nolock)  where isinativo = 0 ) ) ");
            strSQL.Append("  or nid_empr = " + nId_Cliente.ToString() + " ");
            strSQL.Append("  group by nid_empr ");
            strSQL.Append(") ");



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


        public DataSet Trazer_NHO_11(int xId)
        {

            StringBuilder strSQL = new StringBuilder();


            DataTable table = new DataTable("Result");
            table.Columns.Add("Id_NR17", Type.GetType("System.Int32"));
            table.Columns.Add("Grupo", Type.GetType("System.String"));
            table.Columns.Add("Tipo_Ambiente", Type.GetType("System.String"));
            table.Columns.Add("IRC_RA", Type.GetType("System.String"));
            table.Columns.Add("Elux", Type.GetType("System.String"));

            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);


            strSQL.Append("select *  ");
            strSQL.Append("from tbl_NR17_NHO11 (nolock)  ");
            strSQL.Append("where Id_NR17 = " + xId.ToString() + " ");
            strSQL.Append("order by convert(decimal, substring( grupo,1,2)), grupo, Tipo_Ambiente ");
            

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

        public DataSet Gerar_Relatorio(int xLaudo)
        {
            
            StringBuilder strSQL = new StringBuilder();

            
            DataTable table = new DataTable("Result");
            table.Columns.Add("tNO_FUNC", Type.GetType("System.String"));
            table.Columns.Add("tNO_EPI", Type.GetType("System.String"));
            table.Columns.Add("tNO_X", Type.GetType("System.String"));

            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);


            strSQL.Append("  SELECT tNO_FUNC, " );
		    strSQL.Append("  CASE tDS_CONDICAO  " );
			strSQL.Append("  WHEN NULL THEN tDS_EPI WHEN '' THEN tDS_EPI " );
			strSQL.Append("  ELSE   tDS_EPI COLLATE Latin1_General_CI_AS + ' ('+tDS_CONDICAO COLLATE Latin1_General_CI_AS+')'  " );
		    strSQL.Append("  END AS tNO_EPI,   " );
		    strSQL.Append("  char(149) AS tNO_X " );
            strSQL.Append("  FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.EPI_EXTE_LAUD_UNIAO " );
            strSQL.Append("  WHERE (nID_LAUD_TEC= " ) ;
            strSQL.Append(xLaudo.ToString());
            strSQL.Append(" ) AND nID_FUNC IN (SELECT nID_FUNC FROM tblFUNC_EMPREGADO (nolock)  WHERE nID_FUNC IS NOT NULL) ");
            strSQL.Append("  ORDER BY tNO_FUNC, tDS_EPI ");


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




        public DataSet Retornar_EPIs_Utilizados_Laudos(Int32 xEmpresa)
        {

            StringBuilder strSQL = new StringBuilder();


            DataTable table = new DataTable("Result");
            table.Columns.Add("Id", Type.GetType("System.Int32"));
            table.Columns.Add("Descricao", Type.GetType("System.String"));


            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);


            strSQL.Append("  select IdEpi as Id, Descricao from " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.epi ");
            strSQL.Append("  where IdEpi in ");
            strSQL.Append("  ( ");
            strSQL.Append("  	SELECT distinct nID_EPI ");
            strSQL.Append("  	FROM tblFUNC_EPI_EXTE  ");
            strSQL.Append("  	WHERE nID_FUNC in ");
            strSQL.Append("  	( ");
            strSQL.Append("  	   select nid_func from tblfunc ");
            strSQL.Append("  	   where nid_laud_tec in ");
            strSQL.Append("  	   ( ");
            strSQL.Append("  		  select nid_laud_tec from tbllaudo_tec ");
            strSQL.Append("  		  where nid_empr = " + xEmpresa.ToString() + " ");
            strSQL.Append("  	   ) ");
            strSQL.Append("  	) ");
            strSQL.Append("  	union all ");
            strSQL.Append("  	SELECT distinct nID_EPI ");
            strSQL.Append("  	FROM tblEPI_EXTE  ");
            strSQL.Append("  	WHERE nID_PPRA IN  ");
            strSQL.Append("  	 ( ");
            strSQL.Append("  	   select nId_PPRA   ");
            strSQL.Append("  	   FROM tblPPRA1  ");
            strSQL.Append("  	   WHERE nID_FUNC in ");
            strSQL.Append("  	   ( ");
            strSQL.Append("  		 select nid_func from tblfunc ");
            strSQL.Append("  		 where nid_laud_tec in ");
            strSQL.Append("  		 ( ");
            strSQL.Append("  			select nid_laud_tec from tbllaudo_tec ");
            strSQL.Append("  			where nid_empr = " + xEmpresa.ToString() + " ");
            strSQL.Append("  		 ) ");
            strSQL.Append("  	  )");
            strSQL.Append("  	)");
            strSQL.Append("  ) ");
            strSQL.Append("  or indEPI = 4 ");
            strSQL.Append(" order by Descricao  ");


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




        public DataSet Trazer_Admissao_FatorRH_TipoSanguineo(Int32 xIdPCMSO, Int32 xIdGHE, Int32 xIdEmpregado)
        {

            //StringBuilder strSQL = new StringBuilder();
            DataTable table = new DataTable("Result");
            table.Columns.Add("Exame", Type.GetType("System.String"));
            table.Columns.Add("Data_Realizado", Type.GetType("System.String"));

            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);

            SqlCommand rCommand = new SqlCommand();

            rCommand.Parameters.Add("@xIdPCMSO", SqlDbType.NChar).Value = xIdPCMSO.ToString();
            rCommand.Parameters.Add("@xIdGHE", SqlDbType.NChar).Value = xIdGHE.ToString();
            rCommand.Parameters.Add("@xIdEmpregado", SqlDbType.NChar).Value = xIdEmpregado.ToString();

            rCommand.CommandText = "select distinct b.Nome as Exame, case when max( c.DataExame ) is null then '  /  /    ' else convert( char(10), max( c.DataExame), 103 ) end as Data_Realizado "
            + "from PcmsoPlanejamento as a "
            + "left join Examedicionario as b on(a.IdExameDicionario = b.IdExameDicionario) "
            + "left join ExameBase as c on(a.IdExameDicionario = c.IdExameDicionario and c.IdEmpregado = @xIdEmpregado and c.IndResultado in (1, 2)) "
            + "where(b.nome like '%Fator RH' or b.nome like '%Fator RH e%' or b.nome like '%Tipo Sang%') "
            + "and a.naadmissao = 1 "
            + "and idpcmso = @xIdPCMSO "
            + "and idghe = @xIdGHE "
            + "group by b.Nome  ";



            //strSQL.Append("select distinct b.Nome as Exame, case when max( c.DataExame ) is null then '  /  /    ' else convert( char(10), max( c.DataExame), 103 ) end as Data_Realizado ");
            //strSQL.Append("from PcmsoPlanejamento as a ");
            //strSQL.Append("left join Examedicionario as b on(a.IdExameDicionario = b.IdExameDicionario) ");
            //strSQL.Append("left join ExameBase as c on(a.IdExameDicionario = c.IdExameDicionario and c.IdEmpregado = " + xIdEmpregado.ToString() + " and c.IndResultado in (1, 2)) ");
            //strSQL.Append("where(b.nome like '%Fator RH' or b.nome like '%Fator RH e%' or b.nome like '%Tipo Sang%') ");
            //strSQL.Append("and a.naadmissao = 1 ");
            //strSQL.Append("and idpcmso = " + xIdPCMSO.ToString() + " ");
            //strSQL.Append("and idghe = " + xIdGHE.ToString() + " ");
            //strSQL.Append("group by b.Nome  ");



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





        public DataSet Retornar_EPIs_Empregados(Int32 xEmpresa, Int32 xIdEPI)
        {

            StringBuilder strSQL = new StringBuilder();


            DataTable table = new DataTable("Result");
            table.Columns.Add("nId_Empregado", Type.GetType("System.Int32"));            


            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);


            strSQL.Append("select distinct b.nid_empregado ");
            strSQL.Append("from tblEMPREGADO_FUNCAO (nolock)  as a ");
            strSQL.Append("left join tblempregado (nolock)  as b on(a.nID_EMPREGADO = b.nID_EMPREGADO) ");
            strSQL.Append("where nID_EMPREGADO_FUNCAO in ");
            strSQL.Append("( ");
            strSQL.Append("  select nID_EMPREGADO_FUNCAO  from tblfunc_empregado (nolock)  ");
            strSQL.Append("  where nID_FUNC in ");
            strSQL.Append("  ( ");
            strSQL.Append("   select distinct nid_func from ");
            strSQL.Append("   ( ");
            strSQL.Append("      select nID_FUNC from tblfunc (nolock) ");
            strSQL.Append("      where nid_laud_tec in   	   ( ");
            strSQL.Append("        select top 1 nid_laud_tec from tbllaudo_tec  (nolock) ");
            strSQL.Append("        where nid_empr = " + xEmpresa.ToString() + " " );
            strSQL.Append("        and nid_pedido in (select idpedido from " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.pedido (nolock)  where dataconclusao is not null) ");
            strSQL.Append("        order by hdt_laudo desc) ");
            strSQL.Append("   and nid_func in ");
            strSQL.Append("   ( ");
            strSQL.Append("     select nid_func from tblFunc_EPI_Exte ");
            strSQL.Append("     where nID_EPI = " + xIdEPI.ToString() + " " );
            strSQL.Append("    ) ");
            strSQL.Append(" union ");
            strSQL.Append("  select nID_FUNC from tblfunc (nolock)  ");
            strSQL.Append("  where nid_laud_tec in   	   ( ");
            strSQL.Append("     select top 1 nid_laud_tec from tbllaudo_tec (nolock)  ");
            strSQL.Append("     where nid_empr = " + xEmpresa.ToString() + " " );
            strSQL.Append("     and nid_pedido in (select idpedido from " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.pedido (nolock)  where dataconclusao is not null ) ");
            strSQL.Append("     order by hdt_laudo desc       )  ");
            strSQL.Append("  and nid_func in ");
            strSQL.Append("  ( ");
            strSQL.Append("   select nid_func from tblppra1 (nolock)  ");
            strSQL.Append("   where nid_ppra in ");
            strSQL.Append("   ( ");
            strSQL.Append("      select nid_ppra from tblEPI_EXTE ");
            strSQL.Append("      where nID_EPI = " + xIdEPI.ToString() + " " );
            strSQL.Append("    ) ");
            strSQL.Append("   ) ");
            strSQL.Append(") ");
            strSQL.Append("as tx90 ");
            strSQL.Append(") ");
            strSQL.Append(") ");
            strSQL.Append("and hdt_dem is null ");
            


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





        public DataSet Retornar_EPIs_Utilizados_Ultimo_Laudo(Int32 xEmpresa)
        {

            StringBuilder strSQL = new StringBuilder();


            DataTable table = new DataTable("Result");
            table.Columns.Add("Id", Type.GetType("System.Int32"));
            table.Columns.Add("Descricao", Type.GetType("System.String"));


            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);


            strSQL.Append("  select  IdEpi as Id, Nome as Descricao from " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.epi ");
            strSQL.Append("  where IdEpi in ");
            strSQL.Append("  ( ");
            strSQL.Append("  	SELECT distinct nID_EPI ");
            strSQL.Append("  	FROM tblFUNC_EPI_EXTE  ");
            strSQL.Append("  	WHERE nID_FUNC in ");
            strSQL.Append("  	( ");
            strSQL.Append("  	   select nid_func from tblfunc (nolock)  ");
            strSQL.Append("  	   where nid_laud_tec in ");
            strSQL.Append("  	   ( ");
            strSQL.Append("  		  select top 1 nid_laud_tec from tbllaudo_tec (nolock)  ");
            strSQL.Append("  		  where nid_empr = " + xEmpresa.ToString() + " ");
            strSQL.Append("  		  and nid_pedido in (select idpedido from " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.pedido (nolock)  where dataconclusao is not null ) ");
            strSQL.Append("      	  order by hdt_laudo desc ");
            strSQL.Append("  	   ) ");
            strSQL.Append("  	) ");
            strSQL.Append("  	union all ");
            strSQL.Append("  	SELECT distinct nID_EPI ");
            strSQL.Append("  	FROM tblEPI_EXTE  ");
            strSQL.Append("  	WHERE nID_PPRA IN  ");
            strSQL.Append("  	 ( ");
            strSQL.Append("  	   select nId_PPRA   ");
            strSQL.Append("  	   FROM tblPPRA1  (nolock)  ");
            strSQL.Append("  	   WHERE nID_FUNC in ");
            strSQL.Append("  	   ( ");
            strSQL.Append("  		 select nid_func from tblfunc ");
            strSQL.Append("  		 where nid_laud_tec in ");
            strSQL.Append("  		 ( ");
            strSQL.Append("  			select top 1 nid_laud_tec from tbllaudo_tec (nolock)  ");
            strSQL.Append("  			where nid_empr = " + xEmpresa.ToString() + " ");
            strSQL.Append("  		    and nid_pedido in (select idpedido from " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.pedido (nolock)  where dataconclusao is not null ) ");
            strSQL.Append("      	    order by hdt_laudo desc ");
            strSQL.Append("  		 ) ");
            strSQL.Append("  	  )");
            strSQL.Append("  	)");
            strSQL.Append("  ) ");
            strSQL.Append(" order by Descricao  ");


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



        public DataSet Gerar_Relatorio_EPIs(int xLaudo)
        {

            StringBuilder strSQL = new StringBuilder();


            DataTable table = new DataTable("Result");
            table.Columns.Add("tNO_FUNC", Type.GetType("System.String"));
            table.Columns.Add("tNO_EPI", Type.GetType("System.String"));
            table.Columns.Add("tNO_X", Type.GetType("System.String"));
            table.Columns.Add("tNO_TOT", Type.GetType("System.Int32"));

            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);


            strSQL.Append("SELECT ITEM, SETOR as tNO_FUNC, tNO_EPI, COUNT( DISTINCT Setor2 + convert( char(20),nId_Empregado ) ) as tNO_X,  0 as tNO_TOT ");
            strSQL.Append("from ");
            strSQL.Append("( ");
            strSQL.Append("SELECT '1' AS ITEM, D.tNO_STR_EMPR as Setor, D.tNO_STR_EMPR as Setor2 ,E.tNO_EMPG, E.nId_Empregado, A.nId_Func, A.tNO_FUNC,    ");
            strSQL.Append("          CASE A.tDS_CONDICAO    WHEN NULL THEN A.tDS_EPI WHEN '' THEN A.tDS_EPI   ELSE   A.tDS_EPI COLLATE Latin1_General_CI_AS + ' ('+A.tDS_CONDICAO COLLATE Latin1_General_CI_AS+')'    END AS tNO_EPI ");
            strSQL.Append("   FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.EPI_EXTE_LAUD_UNIAO As A   ");
            strSQL.Append("   LEFT JOIN tblFunc_Empregado  (nolock) As B ");
            strSQL.Append("   On ( a.nId_FunC = b.nId_Func ) ");
            strSQL.Append("   LEFT JOIN tblEmpregado_Funcao (nolock)  as C ");
            strSQL.Append("   On ( b.nId_Empregado_Funcao = c.nId_Empregado_Funcao ) ");
            strSQL.Append("   LEFT JOIN tblSetor (nolock)  as D ");
            strSQL.Append("   On ( c.nID_Setor = d.nID_Setor ) ");
            strSQL.Append("   LEFT JOIN tblEmpregado (nolock)  as E ");
            strSQL.Append("   On ( c.nID_Empregado = E.nId_Empregado ) ");
            strSQL.Append("   WHERE (A.nID_LAUD_TEC= ");
            strSQL.Append(xLaudo.ToString());
            strSQL.Append("   ) ");
            strSQL.Append("   AND A.nID_FUNC IN (SELECT nID_FUNC FROM tblFUNC_EMPREGADO (nolock)  WHERE nID_FUNC IS NOT NULL)    ");
            strSQL.Append(" UNION ");
            strSQL.Append("SELECT '2' AS ITEM, 'TOTAL' as Setor, D.tNO_STR_EMPR as Setor2, E.tNO_EMPG, E.nId_Empregado, A.nId_Func, A.tNO_FUNC,    ");
            strSQL.Append("          CASE A.tDS_CONDICAO    WHEN NULL THEN A.tDS_EPI WHEN '' THEN A.tDS_EPI   ELSE   A.tDS_EPI COLLATE Latin1_General_CI_AS + ' ('+A.tDS_CONDICAO COLLATE Latin1_General_CI_AS+')'    END AS tNO_EPI ");
            strSQL.Append("   FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.EPI_EXTE_LAUD_UNIAO As A   ");
            strSQL.Append("   LEFT JOIN tblFunc_Empregado (nolock)  As B ");
            strSQL.Append("   On ( a.nId_FunC = b.nId_Func ) ");
            strSQL.Append("   LEFT JOIN tblEmpregado_Funcao (nolock)  as C ");
            strSQL.Append("   On ( b.nId_Empregado_Funcao = c.nId_Empregado_Funcao ) ");
            strSQL.Append("   LEFT JOIN tblSetor (nolock)  as D ");
            strSQL.Append("   On ( c.nID_Setor = d.nID_Setor ) ");
            strSQL.Append("   LEFT JOIN tblEmpregado (nolock)  as E ");
            strSQL.Append("   On ( c.nID_Empregado = E.nId_Empregado ) ");
            strSQL.Append("   WHERE (A.nID_LAUD_TEC= ");
            strSQL.Append(xLaudo.ToString());
            strSQL.Append("   ) ");
            strSQL.Append("   AND A.nID_FUNC IN (SELECT nID_FUNC FROM tblFUNC_EMPREGADO (nolock)  WHERE nID_FUNC IS NOT NULL)    ");


            strSQL.Append(") ");
            strSQL.Append("as TX1 ");
            strSQL.Append("group by ITEM, Setor, tNO_EPI ");
            strSQL.Append("order by ITEM, Setor, tNO_EPI ");



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






        public void Salvar_Dados_Uniforme(int xIdUniforme, string xUniforme, string xObs, string xMedida, int xIntervalo, int xPeriodicidade)
        {

            using (SqlConnection cnn = new SqlConnection(Ilitera.Data.SQLServer.EntitySQLServer.GetConnection()))
            {

                string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection();

                Database objDB = new SqlDatabase(connString);

                var objCmd = objDB.GetStoredProcCommand("sp_Atualizar_Uniforme");

                objDB.AddInParameter(objCmd, "xIdUniforme", System.Data.DbType.String, xIdUniforme);
                objDB.AddInParameter(objCmd, "xUniforme", System.Data.DbType.String, xUniforme);
                objDB.AddInParameter(objCmd, "xObs", System.Data.DbType.String, xObs);
                objDB.AddInParameter(objCmd, "xMedida", System.Data.DbType.String, xMedida);
                objDB.AddInParameter(objCmd, "xIntervalo", System.Data.DbType.String, xIntervalo);
                objDB.AddInParameter(objCmd, "xPeriodicidade", System.Data.DbType.String, xPeriodicidade);

                var readerRetorno = objDB.ExecuteReader(objCmd);

                return;

            }

        }


        public void Salvar_Dados_Tamanho(int xIdUniforme, string xTamanho, string xObs)
        {

            using (SqlConnection cnn = new SqlConnection(Ilitera.Data.SQLServer.EntitySQLServer.GetConnection()))
            {

                string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection();

                Database objDB = new SqlDatabase(connString);

                var objCmd = objDB.GetStoredProcCommand("sp_Atualizar_Tamanho_Uniforme");

                objDB.AddInParameter(objCmd, "xIdUniforme", System.Data.DbType.String, xIdUniforme);
                objDB.AddInParameter(objCmd, "xTamanho", System.Data.DbType.String, xTamanho);
                objDB.AddInParameter(objCmd, "xObs", System.Data.DbType.String, xObs);
                

                var readerRetorno = objDB.ExecuteReader(objCmd);

                return;

            }

        }



        public void Salvar_Dados_EPIs(int xIdUniforme, int xCodEPI)
        {

            using (SqlConnection cnn = new SqlConnection(Ilitera.Data.SQLServer.EntitySQLServer.GetConnection()))
            {

                string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection();

                Database objDB = new SqlDatabase(connString);

                var objCmd = objDB.GetStoredProcCommand("sp_Atualizar_EPI_Uniforme");

                objDB.AddInParameter(objCmd, "xIdUniforme", System.Data.DbType.String, xIdUniforme);
                objDB.AddInParameter(objCmd, "xCodEPI", System.Data.DbType.Int32, xCodEPI );
                

                var readerRetorno = objDB.ExecuteReader(objCmd);

                return;

            }

        }

    



        public int Criar_Dados_Uniforme(int xEmpresa, string xUniforme, string xObs, string xMedida, int xIntervalo, int xPeriodicidade)
        {

            using (SqlConnection cnn = new SqlConnection(Ilitera.Data.SQLServer.EntitySQLServer.GetConnection()))
            {

                string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection();

                Database objDB = new SqlDatabase(connString);

                var objCmd = objDB.GetStoredProcCommand("sp_Criar_Uniforme");

                objDB.AddInParameter(objCmd, "xEmpresa", System.Data.DbType.String, xEmpresa);
                objDB.AddInParameter(objCmd, "xUniforme", System.Data.DbType.String, xUniforme);
                objDB.AddInParameter(objCmd, "xObs", System.Data.DbType.String, xObs);
                objDB.AddInParameter(objCmd, "xMedida", System.Data.DbType.String, xMedida);
                objDB.AddInParameter(objCmd, "xIntervalo", System.Data.DbType.String, xIntervalo);
                objDB.AddInParameter(objCmd, "xPeriodicidade", System.Data.DbType.String, xPeriodicidade);

                var readerRetorno = objDB.ExecuteReader(objCmd);

                int xRet = 0;

                while (readerRetorno.Read())
                {
                    xRet = System.Convert.ToInt32(readerRetorno[0].ToString());
                }


                return xRet;

            }

        }





        public int Salvar_Dados_Empregado_Uniforme( Int32 xEmpregado, Int32  xUniforme_Medida, Int32 xIdUsuario, string xData, int xQtde)
        {

            using (SqlConnection cnn = new SqlConnection(Ilitera.Data.SQLServer.EntitySQLServer.GetConnection()))
            {

                string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection();

                Database objDB = new SqlDatabase(connString);

                var objCmd = objDB.GetStoredProcCommand("sp_Criar_Uniforme_Empregado");

                objDB.AddInParameter(objCmd, "xEmpregado", System.Data.DbType.Int32, xEmpregado);
                objDB.AddInParameter(objCmd, "xUniforme_Medida", System.Data.DbType.Int32, xUniforme_Medida);
                objDB.AddInParameter(objCmd, "xData", System.Data.DbType.String, xData);
                objDB.AddInParameter(objCmd, "xQtde", System.Data.DbType.String, xQtde);

                var readerRetorno = objDB.ExecuteReader(objCmd);

                int xRet = 0;

                while (readerRetorno.Read())
                {
                    xRet = System.Convert.ToInt32(readerRetorno[0].ToString());
                }


                Log_Web("exec sp_Criar_Uniforme_Empregado " + xEmpregado.ToString() + " , " + xUniforme_Medida, xIdUsuario, "Dados de Uniforme");


                return xRet;

            }

        }


        public void Excluir_Dados_Uniforme( Int32 xIdUniforme)
        {

            using (SqlConnection cnn = new SqlConnection(Ilitera.Data.SQLServer.EntitySQLServer.GetConnection()))
            {

                string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection();

                Database objDB = new SqlDatabase(connString);

                var objCmd = objDB.GetStoredProcCommand("sp_Excluir_Uniforme");

                objDB.AddInParameter(objCmd, "xIdUniforme", System.Data.DbType.Int32, xIdUniforme);

                var readerRetorno = objDB.ExecuteReader(objCmd);

                return;

            }

        }



        public void Excluir_Dados_Empregado_Uniforme( Int32 xIdEmpregado)
        {

            using (SqlConnection cnn = new SqlConnection(Ilitera.Data.SQLServer.EntitySQLServer.GetConnection()))
            {

                string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection();

                Database objDB = new SqlDatabase(connString);

                var objCmd = objDB.GetStoredProcCommand("sp_Excluir_Empregado_Uniforme");

                objDB.AddInParameter(objCmd, "xIdEmpregado", System.Data.DbType.String, xIdEmpregado);

                var readerRetorno = objDB.ExecuteReader(objCmd);

                return;

            }

        }





        public void Excluir_EPIs_Uniforme(int xIdUniforme)
        {

            using (SqlConnection cnn = new SqlConnection(Ilitera.Data.SQLServer.EntitySQLServer.GetConnection()))
            {

                string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection();

                Database objDB = new SqlDatabase(connString);

                var objCmd = objDB.GetStoredProcCommand("sp_Excluir_EPI_Uniforme");

                objDB.AddInParameter(objCmd, "xIdUniforme", System.Data.DbType.String, xIdUniforme);

                var readerRetorno = objDB.ExecuteReader(objCmd);

                return;

            }

        }



        public void Excluir_Tamanhos_Uniforme(int xIdUniforme)
        {

            using (SqlConnection cnn = new SqlConnection(Ilitera.Data.SQLServer.EntitySQLServer.GetConnection()))
            {

                string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection();

                Database objDB = new SqlDatabase(connString);

                var objCmd = objDB.GetStoredProcCommand("sp_Excluir_Tamanhos_Uniforme");

                objDB.AddInParameter(objCmd, "xIdUniforme", System.Data.DbType.String, xIdUniforme);

                var readerRetorno = objDB.ExecuteReader(objCmd);

                return;

            }

        }




        public DataSet Retornar_Tamanhos_Uniforme(int xEmpresa)
        {

            StringBuilder strSQL = new StringBuilder();


            DataTable table = new DataTable("Result");
            table.Columns.Add("Id_Uniforme", Type.GetType("System.Int32"));
            table.Columns.Add("Id_Uniforme_Medidas", Type.GetType("System.Int32"));
            table.Columns.Add("Uniforme", Type.GetType("System.String"));
            table.Columns.Add("Medida", Type.GetType("System.String"));
            table.Columns.Add("Valor", Type.GetType("System.String"));

            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);


            SqlCommand rCommand = new SqlCommand();

            rCommand.Parameters.Add("@xEmpresa", SqlDbType.NChar).Value = xEmpresa.ToString();

            rCommand.CommandText = "select a.Id_Uniforme, b.Id_Uniforme_Medidas,a.Uniforme, a.Medida, b.Valor " 
            +"from Uniforme as a " 
            + "left join Uniforme_Medidas as b " 
            + "on a.Id_Uniforme = b.Id_Uniforme " 
            + "where a.idPessoa = @xEmpresa "
            + "and b.valor is not null " 
            + "order by a.Id_Uniforme, Medida, Valor";

            //strSQL.Append("select a.Id_Uniforme, b.Id_Uniforme_Medidas,a.Uniforme, a.Medida, b.Valor " );
            //strSQL.Append("from Uniforme as a " );
            //strSQL.Append("left join Uniforme_Medidas as b " );
            //strSQL.Append("on a.Id_Uniforme = b.Id_Uniforme " );
            //strSQL.Append("where a.idPessoa = "  + xEmpresa.ToString() + "  ");
            //strSQL.Append("and b.valor is not null " );
            //strSQL.Append("order by a.Id_Uniforme, Medida, Valor"); 


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

        public String Retornar_Uniforme(int xUniforme)
        {

            //StringBuilder strSQL = new StringBuilder();


            DataTable table = new DataTable("Result");
            table.Columns.Add("Uniforme", Type.GetType("System.String"));

            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);

            SqlCommand rCommand = new SqlCommand();

            rCommand.Parameters.Add("@xUniforme", SqlDbType.NChar).Value = xUniforme.ToString();

            rCommand.CommandText = "select Uniforme "
            + "from Uniforme "
            + "where Id_Uniforme = @xUniforme  ";


            //strSQL.Append("select Uniforme ");
            //strSQL.Append("from Uniforme ");
            //strSQL.Append("where Id_Uniforme = " + xUniforme.ToString() + "  ");


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

            return m_ds.Tables[0].Rows[0][0].ToString().Trim() ;

        }




        public DataSet Retornar_Uniformes_Empregado(int xEmpregado)
        {

            //StringBuilder strSQL = new StringBuilder();

            DataTable table = new DataTable("Result");            
            table.Columns.Add("Id_Uniforme_Medidas", Type.GetType("System.Int32"));
            table.Columns.Add("Id_Uniforme", Type.GetType("System.Int32"));
            table.Columns.Add("Medida", Type.GetType("System.String"));
            table.Columns.Add("Uniforme", Type.GetType("System.String"));            
            table.Columns.Add("Valor", Type.GetType("System.String"));
            table.Columns.Add("DataFornecimento", Type.GetType("System.String"));
            table.Columns.Add("Qtde", Type.GetType("System.Int32"));

            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);

            SqlCommand rCommand = new SqlCommand();

            rCommand.Parameters.Add("@xEmpregado", SqlDbType.NChar).Value = xEmpregado.ToString();

            rCommand.CommandText = "select a.Id_Uniforme_Medidas, b.Id_Uniforme, c.Medida, c.Uniforme, Qtde, b.Valor, case when Data_Fornecimento is null then '01/01/2017' else convert(char(10), Data_Fornecimento,103 ) end as DataFornecimento " 
            + "from uniforme_empregado as a left join Uniforme_Medidas as b "
            + "on ( a.Id_Uniforme_Medidas = b.Id_Uniforme_Medidas ) "
            + "left join Uniforme as c "
            + "on ( b.Id_Uniforme = c.Id_Uniforme ) " 
            + "where nid_Empregado = @xEmpregado  " 
            + "order by c.Uniforme, c.Medida";

            //strSQL.Append("select a.Id_Uniforme_Medidas, b.Id_Uniforme, c.Medida, c.Uniforme, Qtde, b.Valor, case when Data_Fornecimento is null then '01/01/2017' else convert(char(10), Data_Fornecimento,103 ) end as DataFornecimento " );
            //strSQL.Append("from uniforme_empregado as a left join Uniforme_Medidas as b ");
            //strSQL.Append("on ( a.Id_Uniforme_Medidas = b.Id_Uniforme_Medidas ) ");
            //strSQL.Append("left join Uniforme as c ");
            //strSQL.Append("on ( b.Id_Uniforme = c.Id_Uniforme ) " );
            //strSQL.Append("where nid_Empregado = " + xEmpregado.ToString() + "  " );
            //strSQL.Append("order by c.Uniforme, c.Medida");


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



        public DataSet Retornar_Dados_Uniforme(int xUniforme)
        {

            StringBuilder strSQL = new StringBuilder();


            DataTable table = new DataTable("Result");
            table.Columns.Add("Uniforme", Type.GetType("System.String"));
            table.Columns.Add("Medida", Type.GetType("System.String"));
            table.Columns.Add("Obs", Type.GetType("System.String"));
            table.Columns.Add("Valor", Type.GetType("System.String"));
            table.Columns.Add("Obs_Medida", Type.GetType("System.String"));
            table.Columns.Add("IndPeriodicidade", Type.GetType("System.String"));
            table.Columns.Add("Intervalo", Type.GetType("System.String"));

            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);


            SqlCommand rCommand = new SqlCommand();

            rCommand.Parameters.Add("@xUniforme", SqlDbType.NChar).Value = xUniforme.ToString();

            rCommand.CommandText = "select a.Uniforme, a.Medida, a.Obs, b.Valor, b.Obs as Obs_Medida, isnull( IndPeriodicidade, '' ) as IndPeriodicidade, isnull( Intervalo, '' ) as Intervalo "
            + "from Uniforme as a left join Uniforme_Medidas as b "
            + "on ( a.Id_Uniforme = b.Id_Uniforme ) "
            + "where a.Id_Uniforme = @xUniforme order by b.Valor ";
            
            //strSQL.Append("select a.Uniforme, a.Medida, a.Obs, b.Valor, b.Obs as Obs_Medida " ) ;
            //strSQL.Append("from Uniforme as a left join Uniforme_Medidas as b " ) ;
            //strSQL.Append("on ( a.Id_Uniforme = b.Id_Uniforme ) ");
            //strSQL.Append("where a.Id_Uniforme = " + xUniforme.ToString() + " order by b.Valor ");



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



        public DataSet Retornar_EPIs_Uniforme(int xUniforme)
        {

           //StringBuilder strSQL = new StringBuilder();

            DataTable table = new DataTable("Result");
            table.Columns.Add("IdEPI", Type.GetType("System.Int32"));
            table.Columns.Add("EPI", Type.GetType("System.String"));

            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);


            SqlCommand rCommand = new SqlCommand();

            rCommand.Parameters.Add("@xUniforme", SqlDbType.NChar).Value = xUniforme.ToString();

            rCommand.CommandText = "select a.IdEPI, Nome as EPI  "
            + "from uniforme_EPI as a "
            + "left join EPI as b "
            + "on ( a.IdEPI = b.IdEPI ) "
            + "where a.Id_Uniforme = @xUniforme order by Nome ";

            //strSQL.Append("select a.IdEPI, Nome as EPI  " );
            //strSQL.Append("from uniforme_EPI as a " );
            //strSQL.Append("left join EPI as b " );
            //strSQL.Append("on ( a.IdEPI = b.IdEPI ) ");
            //strSQL.Append("where a.Id_Uniforme = " + xUniforme.ToString() + " order by Nome ");


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




        public DataSet Retornar_Anexos_PPRA(int xLaudo, string xTipo)
        {
            //por exemplo, xtipo = PDF,  exibirá apenas PDFs,  se for *,  exibirá todos ,  se for -PDF, exibirá todos, exceto PDFs
            //StringBuilder strSQL = new StringBuilder();
            DataTable table = new DataTable("Result");
            table.Columns.Add("Arquivo", Type.GetType("System.String"));
            table.Columns.Add("Descricao", Type.GetType("System.String"));

            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);


            SqlCommand rCommand = new SqlCommand();

            rCommand.Parameters.Add("@xLaudo", SqlDbType.NChar).Value = xLaudo.ToString();
            rCommand.Parameters.Add("@xTipo", SqlDbType.NChar).Value = xTipo.ToString();



            if (xTipo == "*")
            {
                rCommand.CommandText = "  SELECT Arquivo, Descricao from tblLaudo_TEC_Anexos where nId_Laud_Tec = @xLaudo order by Arquivo ";
                //strSQL.Append("  SELECT Arquivo, Descricao from tblLaudo_TEC_Anexos where nId_Laud_Tec =  " + xLaudo.ToString() + " order by Arquivo ");
            }
            else if (xTipo == "-PDF")
            {
                rCommand.CommandText = "  SELECT Arquivo, Descricao from tblLaudo_TEC_Anexos where nId_Laud_Tec =  @xLaudo and Arquivo not like '%.PDF'  order by Arquivo ";
                //strSQL.Append("  SELECT Arquivo, Descricao from tblLaudo_TEC_Anexos where nId_Laud_Tec =  " + xLaudo.ToString() + " and Arquivo not like '%.PDF'  order by Arquivo ");
            }
            else
            {
                rCommand.CommandText = "  SELECT Arquivo, Descricao from tblLaudo_TEC_Anexos where nId_Laud_Tec =  @xLaudo and Arquivo like '%.@xTipo' order by Arquivo ";
                //strSQL.Append("  SELECT Arquivo, Descricao from tblLaudo_TEC_Anexos where nId_Laud_Tec =  " + xLaudo.ToString() + " and Arquivo like '%." + xTipo + "'  order by Arquivo ");
            }



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





        //public DataSet Recibo_EPIs(int xIdEmpresa, string xData_Inicial, string xData_Final, string xEmpresa, string ArqFotoEmpregInicio, string ArqFotoEmpregTermino, int ArqFotoEmrpegQteDigitos, string ArqFotoEmpregExtensao, string xDiretorio_Padrao )
        //{
        //    StringBuilder strSQL = new StringBuilder();
        //    string xNome_Old = "";
        //    int xCont = 0;
        //    int xTotal = 0;
        //    string zFoto = "";

        //    string xArquivo;
        //    string xAux;


        //    DataRow newRow;

            
        //    DataTable table = new DataTable("Result2");
        //    table.Columns.Add("RAZAO_SOCIAL", Type.GetType("System.String"));
        //    table.Columns.Add("NOME_EMPREGADO", Type.GetType("System.String"));
        //    table.Columns.Add("SEXO", Type.GetType("System.String"));
        //    table.Columns.Add("DATA_NASCIMENTO", Type.GetType("System.String"));
        //    table.Columns.Add("DATA_ADMISSAO", Type.GetType("System.String"));
        //    table.Columns.Add("IDADE", Type.GetType("System.String"));
        //    table.Columns.Add("RG", Type.GetType("System.String"));
        //    table.Columns.Add("RE", Type.GetType("System.String"));
        //    table.Columns.Add("FUNCAO", Type.GetType("System.String"));
        //    table.Columns.Add("SETOR", Type.GetType("System.String"));
        //    table.Columns.Add("DATA_ENTREGA", Type.GetType("System.String"));
        //    table.Columns.Add("iFOTO", Type.GetType("System.Byte[]"));
        //    table.Columns.Add("ComFoto", Type.GetType("System.Boolean"));

        //    table.Columns.Add("NOME_EPI", Type.GetType("System.String"));
        //    table.Columns.Add("CA", Type.GetType("System.String"));
        //    table.Columns.Add("QTD_ENTREGUE", Type.GetType("System.String"));
        //    table.Columns.Add("DATA_FORNECIMENTO", Type.GetType("System.String"));

        //    DataSet m_ds = new DataSet();
        //    DataSet m_ds2 = new DataSet();


        //    m_ds.Tables.Add(table);



        //    DataTable table2 = new DataTable("Result");
        //    table2.Columns.Add("RAZAO_SOCIAL", Type.GetType("System.String"));
        //    table2.Columns.Add("NOME_EMPREGADO", Type.GetType("System.String"));
        //    table2.Columns.Add("SEXO", Type.GetType("System.String"));
        //    table2.Columns.Add("DATA_NASCIMENTO", Type.GetType("System.String"));
        //    table2.Columns.Add("DATA_ADMISSAO", Type.GetType("System.String"));
        //    table2.Columns.Add("IDADE", Type.GetType("System.String"));
        //    table2.Columns.Add("RG", Type.GetType("System.String"));
        //    table2.Columns.Add("RE", Type.GetType("System.String"));
        //    table2.Columns.Add("FUNCAO", Type.GetType("System.String"));
        //    table2.Columns.Add("SETOR", Type.GetType("System.String"));
        //    table2.Columns.Add("DATA_ENTREGA", Type.GetType("System.String"));
        //    table2.Columns.Add("iFOTO", Type.GetType("System.Byte[]"));
        //    //table2.Columns.Add("ComFoto", Type.GetType("System.Boolean"));
        //    table2.Columns.Add("ComFoto", Type.GetType("System.String"));

        //    table2.Columns.Add("NOME_EPI", Type.GetType("System.String"));
        //    table2.Columns.Add("NOME_EPI2", Type.GetType("System.String"));
        //    table2.Columns.Add("NOME_EPI3", Type.GetType("System.String"));
        //    table2.Columns.Add("NOME_EPI4", Type.GetType("System.String"));

        //    table2.Columns.Add("CA", Type.GetType("System.String"));
        //    table2.Columns.Add("CA2", Type.GetType("System.String"));
        //    table2.Columns.Add("CA3", Type.GetType("System.String"));
        //    table2.Columns.Add("CA4", Type.GetType("System.String"));

        //    table2.Columns.Add("QTD_ENTREGUE", Type.GetType("System.String"));
        //    table2.Columns.Add("QTD_ENTREGUE2", Type.GetType("System.String"));
        //    table2.Columns.Add("QTD_ENTREGUE3", Type.GetType("System.String"));
        //    table2.Columns.Add("QTD_ENTREGUE4", Type.GetType("System.String"));
        //    table2.Columns.Add("TOTAL", Type.GetType("System.String"));

        //    table2.Columns.Add("DATA_FORNECIMENTO", Type.GetType("System.String"));
        //    table2.Columns.Add("DATA_FORNECIMENTO2", Type.GetType("System.String"));
        //    table2.Columns.Add("DATA_FORNECIMENTO3", Type.GetType("System.String"));
        //    table2.Columns.Add("DATA_FORNECIMENTO4", Type.GetType("System.String"));
            
        //    m_ds2.Tables.Add(table2);


                       
            

        //    strSQL.Append("  select distinct c.nNO_FOTO, c.tNO_EMPG as Nome_Empregado, convert(char(10), hDT_ADM, 103 ) as Data_Admissao, " );
        //    strSQL.Append("         convert( char(10), hDT_NASC, 103 ) as Data_Nascimento, tSexo as Sexo,  " );
        //    strSQL.Append("         tNO_IDENTIDADE as RG, tNO_APELIDO as RE, e.Descricao as Nome_EPI,  ");
        //    strSQL.Append("         f.NumeroCA as CA,  convert ( char(10), DataRecebimento, 103 ) as Data_Fornecimento, QtdEntregue as Qtd_Entregue ");
        //    strSQL.Append("  from epicaentrega as a left join epicaentregadetalhe as b    " );
        //    strSQL.Append("  on a.IdEPICAEntrega = b.IdEPICAEntrega    " );
        //    strSQL.Append("  left join sied_novo.dbo.tblempregado as c " );
        //    strSQL.Append("  on a.IdEmpregado = c.nId_Empregado " );
        //    strSQL.Append("  left join epiclienteca as d " );
        //    strSQL.Append("  on b.IdEPIClienteCA = d.IdEPIClienteCa " );
        //    strSQL.Append("  left join EPI as e " );
        //    strSQL.Append("  on d.IdEPI = e.IdEPI ");
        //    strSQL.Append("  left join CA as f ");
        //    strSQL.Append("  on d.IdCA = f.IdCA ");
        //    strSQL.Append("  where a.IdEmpregado in " );
        //    strSQL.Append("  ( " );
        //    strSQL.Append("     select distinct e.nID_Empregado " );
        //    strSQL.Append("     from " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEmpregado e  " );
        //    strSQL.Append("     left join sied_novo.dbo.tblEmpregado_Funcao ef on e.nID_Empregado = ef.nID_Empregado  " );
        //    strSQL.Append("     where e.nID_EMPR = " + xIdEmpresa.ToString() + "  " );
        //    strSQL.Append("     and ( ef.nID_EMPR = " + xIdEmpresa.ToString() + "   or ef.nID_EMPR is null ) " );
        //    strSQL.Append("  ) " );
        //    strSQL.Append("  and DataRecebimento between convert( smalldatetime, '" + xData_Inicial + "', 103 ) and convert( smalldatetime, '" + xData_Final +  "', 103 ) ");
        //    strSQL.Append("order by c.tNO_EMPG");

            

        //    using (SqlConnection cnn = new SqlConnection(Ilitera.Data.SQLServer.EntitySQLServer.GetConnection()))
        //    {
        //        SqlDataAdapter da;
        //        cnn.Open();


        //        da = new SqlDataAdapter(strSQL.ToString(), cnn);
        //        da.Fill(m_ds, "Result2");
                
        //        cnn.Close();

        //        da.Dispose();
        //    }


        //    //reorganizar os dados em "Result",  jogando os EPIs nos lugares corretos

        //    DataTable dt = m_ds.Tables["Result2"];

        //    newRow = m_ds2.Tables["Result"].NewRow();
            

        //    foreach (DataRow dr in dt.Rows)
        //    {
                                              

        //        if (xNome_Old != dr["NOME_EMPREGADO"].ToString())
        //        {
        //            if (xNome_Old != "")
        //            {


        //                xAux = zFoto;

        //                if (xAux.Trim() == "0" || xAux.Trim() == "")
        //                {
        //                    newRow["ComFoto"] = "false";
        //                }
        //                else
        //                {


        //                    for (int zCont = xAux.Length; zCont < ArqFotoEmrpegQteDigitos; zCont++)
        //                    {
        //                        xAux = "0" + xAux;
        //                    }

        //                    xArquivo = "I:\\FOTOSDOCSDIGITAIS\\" + xDiretorio_Padrao + "\\Organogramas\\" + ArqFotoEmpregInicio + xAux + ArqFotoEmpregTermino + ArqFotoEmpregExtensao;

        //                    if (xArquivo.ToUpper().IndexOf("FOTOSDOCSDIGITAIS") < 1)
        //                    {
        //                        xArquivo = xArquivo.Substring(0, xArquivo.IndexOf("\\") + 1) + "FOTOSDOCSDIGITAIS\\" + xArquivo.Substring(xArquivo.IndexOf("\\") + 1);
        //                    }

        //                    string pathFoto = xArquivo;

        //                    if (pathFoto != "")
        //                    {

        //                        newRow["iFOTO"] = GetByteFoto_Uri(pathFoto);                                
        //                        newRow["ComFoto"] = "true";
        //                    }
        //                    else
        //                    {
        //                        newRow["ComFoto"] = false;
        //                    }
        //                }


        //                newRow["TOTAL"] = xTotal;                  

        //                m_ds2.Tables["Result"].Rows.Add(newRow);
        //                newRow = m_ds2.Tables["Result"].NewRow();
        //                xTotal = 0;
        //            }

        //            newRow["RAZAO_SOCIAL"] = xEmpresa;
        //            newRow["NOME_EMPREGADO"] = dr["NOME_EMPREGADO"].ToString();
        //            newRow["DATA_NASCIMENTO"] = dr["DATA_NASCIMENTO"].ToString();
        //            newRow["DATA_ADMISSAO"] = dr["DATA_ADMISSAO"].ToString();
        //            newRow["SEXO"] = dr["SEXO"].ToString();
        //            newRow["RG"] = dr["RG"].ToString();
        //            newRow["RE"] = dr["RE"].ToString();

        //            zFoto = dr["nNO_FOTO"].ToString();

        //            //newRow["FUNCAO"] = dr["FUNCAO"].ToString();
        //            //newRow["SETOR"] = dr["SETOR"].ToString();

        //            xNome_Old = dr["NOME_EMPREGADO"].ToString();
        //            newRow["NOME_EPI"] = dr["NOME_EPI"].ToString();
        //            newRow["CA"] = dr["CA"].ToString();
        //            newRow["QTD_ENTREGUE"] = dr["QTD_ENTREGUE"].ToString();
        //            newRow["DATA_FORNECIMENTO"] = dr["DATA_FORNECIMENTO"].ToString();
        //            xTotal = xTotal + System.Convert.ToInt32(dr["QTD_ENTREGUE"].ToString());
        //            xCont = 1;
        //        }
        //        else
        //        {
        //            xCont++;

        //            if ( xCont == 2 ) 
        //            {
        //               newRow["NOME_EPI2"] = dr["NOME_EPI"].ToString();
        //               newRow["CA2"] = dr["CA"].ToString();
        //               newRow["QTD_ENTREGUE2"] = dr["QTD_ENTREGUE"].ToString();
        //               newRow["DATA_FORNECIMENTO2"] = dr["DATA_FORNECIMENTO"].ToString();
        //               xTotal = xTotal + System.Convert.ToInt32( dr["QTD_ENTREGUE"].ToString());
        //            }
        //            else if ( xCont == 3 )
        //            {
        //                newRow["NOME_EPI3"] = dr["NOME_EPI"].ToString();
        //                newRow["CA3"] = dr["CA"].ToString();
        //                newRow["QTD_ENTREGUE3"] = dr["QTD_ENTREGUE"].ToString();
        //                newRow["DATA_FORNECIMENTO3"] = dr["DATA_FORNECIMENTO"].ToString();
        //                xTotal = xTotal + System.Convert.ToInt32(dr["QTD_ENTREGUE"].ToString());
        //            }
        //            else if ( xCont == 4 )
        //            {
        //                newRow["NOME_EPI4"] = dr["NOME_EPI"].ToString();
        //                newRow["CA4"] = dr["CA"].ToString();
        //                newRow["QTD_ENTREGUE4"] = dr["QTD_ENTREGUE"].ToString();
        //                newRow["DATA_FORNECIMENTO4"] = dr["DATA_FORNECIMENTO"].ToString();
        //                xTotal = xTotal + System.Convert.ToInt32(dr["QTD_ENTREGUE"].ToString());
        //            }
        //        }



        //        //string ArqFotoEmpregInicio
        //        //string ArqFotoEmpregTermino
        //        //int ArqFotoEmrpegQteDigitos
        //        //string ArqFotoEmpregExtensao 




        //        //try
        //        //{
        //        //    string pathFoto = "";

        //        //    if (pathFoto != string.Empty && System.IO.File.Exists(pathFoto))
        //        //    {
        //        //        newRow["iFOTO"] = GetByteFoto_Uri(pathFoto);
        //        //        newRow["ComFoto"] = true;
        //        //    }
        //        //    else
        //        //        newRow["ComFoto"] = false;
        //        //}
        //        //catch (Exception ex)
        //        //{
        //        //    newRow["ComFoto"] = false;
        //        //    System.Diagnostics.Trace.WriteLine(ex.Message.ToString());
        //        //}

                

        //    }

            
        //    if (xNome_Old != "")
        //    {

        //        xAux = zFoto;

        //        if (xAux.Trim() == "0" || xAux.Trim() == "")
        //        {
        //            newRow["ComFoto"] = "false";
        //        }
        //        else
        //        {


        //            for (int zCont = xAux.Length; zCont < ArqFotoEmrpegQteDigitos; zCont++)
        //            {
        //                xAux = "0" + xAux;
        //            }

        //            xArquivo = "I:\\FOTOSDOCSDIGITAIS\\" + xDiretorio_Padrao + "\\Organogramas\\" + ArqFotoEmpregInicio + xAux + ArqFotoEmpregTermino + ArqFotoEmpregExtensao;

        //            if (xArquivo.ToUpper().IndexOf("FOTOSDOCSDIGITAIS") < 1)
        //            {
        //                xArquivo = xArquivo.Substring(0, xArquivo.IndexOf("\\") + 1) + "FOTOSDOCSDIGITAIS\\" + xArquivo.Substring(xArquivo.IndexOf("\\") + 1);
        //            }

        //            string pathFoto = xArquivo;

        //            if (pathFoto != "")
        //            {

        //                newRow["iFOTO"] = GetByteFoto_Uri(pathFoto);
        //                newRow["ComFoto"] = "true";
        //            }
        //            else
        //            {
        //                newRow["ComFoto"] = false;
        //            }
        //        }



        //        m_ds2.Tables["Result"].Rows.Add(newRow);
        //    }

        //    return m_ds2;


        //}




        public DataSet Recibo_EPIs(int xIdEmpresa, string xData_Inicial, string xData_Final, string xEmpresa, string ArqFotoEmpregInicio, string ArqFotoEmpregTermino, int ArqFotoEmrpegQteDigitos, string ArqFotoEmpregExtensao, string xDiretorio_Padrao, byte[] dataLogo, int zIdEPICAEntrega, int zEmpr, string zLogotipo )
        {
            StringBuilder strSQL = new StringBuilder();
            string xNome_Old = "";
            
            int xTotal = 0;
            string zFoto = "";

            string xFuncao = "";
            //string xSetor = "";
                

            string xArquivo;
            string xAux;

            string zD1 = "";
            string zD2 = "";

            if (xData_Inicial.IndexOf(" ") > 0)
                zD1 = xData_Inicial.Substring(0, xData_Inicial.IndexOf(" "));
            else
                zD1 = xData_Inicial;


            if (xData_Final.IndexOf(" ") > 0)
                zD2 = xData_Final.Substring(0, xData_Final.IndexOf(" "));
            else
                zD2 = xData_Final;


            DataRow newRow;


            DataTable table = new DataTable("Result2");
            table.Columns.Add("RAZAO_SOCIAL", Type.GetType("System.String"));
            table.Columns.Add("NOME_EMPREGADO", Type.GetType("System.String"));
            table.Columns.Add("SEXO", Type.GetType("System.String"));
            table.Columns.Add("DATA_NASCIMENTO", Type.GetType("System.String"));
            table.Columns.Add("DATA_ADMISSAO", Type.GetType("System.String"));
            table.Columns.Add("IDADE", Type.GetType("System.String"));
            table.Columns.Add("RG", Type.GetType("System.String"));
            table.Columns.Add("RE", Type.GetType("System.String"));
            table.Columns.Add("FUNCAO", Type.GetType("System.String"));
            table.Columns.Add("SETOR", Type.GetType("System.String"));
            table.Columns.Add("DATA_ENTREGA", Type.GetType("System.String"));
            table.Columns.Add("FUNCAO_SETOR", Type.GetType("System.String"));
            table.Columns.Add("iFOTO", Type.GetType("System.Byte[]"));
            table.Columns.Add("ComFoto", Type.GetType("System.Boolean"));

            table.Columns.Add("NOME_EPI", Type.GetType("System.String"));
            table.Columns.Add("CA", Type.GetType("System.String"));
            table.Columns.Add("QTD_ENTREGUE", Type.GetType("System.String"));
            table.Columns.Add("DATA_FORNECIMENTO", Type.GetType("System.String"));

            DataSet m_ds = new DataSet();
            DataSet m_ds2 = new DataSet();


            m_ds.Tables.Add(table);



            DataTable table2 = new DataTable("Result");
            table2.Columns.Add("RAZAO_SOCIAL", Type.GetType("System.String"));
            table2.Columns.Add("NOME_EMPREGADO", Type.GetType("System.String"));
            table2.Columns.Add("SEXO", Type.GetType("System.String"));
            table2.Columns.Add("DATA_NASCIMENTO", Type.GetType("System.String"));
            table2.Columns.Add("DATA_ADMISSAO", Type.GetType("System.String"));
            table2.Columns.Add("IDADE", Type.GetType("System.String"));
            table2.Columns.Add("RG", Type.GetType("System.String"));
            table2.Columns.Add("RE", Type.GetType("System.String"));
            table2.Columns.Add("FUNCAO", Type.GetType("System.String"));
            table2.Columns.Add("SETOR", Type.GetType("System.String"));
            table2.Columns.Add("DATA_ENTREGA", Type.GetType("System.String"));
            table2.Columns.Add("iFOTO", Type.GetType("System.Byte[]"));
            table2.Columns.Add("iFOTOLogo", Type.GetType("System.Byte[]"));
            //table2.Columns.Add("ComFoto", Type.GetType("System.Boolean"));
            table2.Columns.Add("ComFoto", Type.GetType("System.String"));
            table2.Columns.Add("ComFotoLogo", Type.GetType("System.String"));

            table2.Columns.Add("NOME_EPI", Type.GetType("System.String"));
            table2.Columns.Add("NOME_EPI2", Type.GetType("System.String"));
            table2.Columns.Add("NOME_EPI3", Type.GetType("System.String"));
            table2.Columns.Add("NOME_EPI4", Type.GetType("System.String"));

            table2.Columns.Add("CA", Type.GetType("System.String"));
            table2.Columns.Add("CA2", Type.GetType("System.String"));
            table2.Columns.Add("CA3", Type.GetType("System.String"));
            table2.Columns.Add("CA4", Type.GetType("System.String"));

            table2.Columns.Add("QTD_ENTREGUE", Type.GetType("System.String"));
            table2.Columns.Add("QTD_ENTREGUE2", Type.GetType("System.String"));
            table2.Columns.Add("QTD_ENTREGUE3", Type.GetType("System.String"));
            table2.Columns.Add("QTD_ENTREGUE4", Type.GetType("System.String"));
            table2.Columns.Add("TOTAL", Type.GetType("System.String"));

            table2.Columns.Add("DATA_FORNECIMENTO", Type.GetType("System.String"));
            table2.Columns.Add("DATA_FORNECIMENTO2", Type.GetType("System.String"));
            table2.Columns.Add("DATA_FORNECIMENTO3", Type.GetType("System.String"));
            table2.Columns.Add("DATA_FORNECIMENTO4", Type.GetType("System.String"));

            table2.Columns.Add("Origem", Type.GetType("System.String"));
            table2.Columns.Add("Digital", Type.GetType("System.String"));


            m_ds2.Tables.Add(table2);


            

            strSQL.Append("  select distinct c.nNO_FOTO, c.tNO_EMPG as Nome_Empregado, convert(char(10), hDT_ADM, 103 ) as Data_Admissao, ");
            strSQL.Append("         convert( char(10), hDT_NASC, 103 ) as Data_Nascimento, tSexo as Sexo,  ");
            strSQL.Append("         tNO_IDENTIDADE as RG, tCOD_EMPR as RE, e.Descricao as Nome_EPI, isnull(b.Serial,'') as Serial, abs(b.IdEPICAEntregaDetalhe) as IdEPICAEntregaDetalhe, isnull( b.Origem, 'M') as Origem, isnull(Digital, '' ) as Digital, ");
            strSQL.Append("         f.NumeroCA as CA,  convert ( char(10), DataRecebimento, 103 ) as Data_Fornecimento, QtdEntregue as Qtd_Entregue, convert( smalldatetime, DataRecebimento, 103 ), ");
            strSQL.Append("         (  ");
            strSQL.Append("         select top 1 cf.NomeFuncao + '|' + df.tNO_STR_EMPR as Funcao_Setor ");
            strSQL.Append("         from " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEmpregado (nolock)  as af left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEmpregado_Funcao (nolock)  as bf ");
            strSQL.Append("         on ( af.nId_Empregado = bf.nId_Empregado ) ");
            strSQL.Append("         left join Funcao (nolock)  as cf ");
            strSQL.Append("         on ( bf.nId_Funcao = cf.IdFuncao ) ");
            strSQL.Append("         left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblSetor (nolock)  as df ");
            strSQL.Append("         on ( bf.nId_Setor = df.nId_Setor ) ");
            strSQL.Append("         where af.nID_EMPREGADO=c.nId_Empregado ");
            strSQL.Append("         order by bf.hDt_Inicio desc ");
            strSQL.Append("         ) as Funcao_Setor ");
            strSQL.Append("  from epicaentrega (nolock)  as a left join epicaentregadetalhe (nolock)  as b    ");
            strSQL.Append("  on a.IdEPICAEntrega = b.IdEPICAEntrega    ");
            strSQL.Append("  left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblempregado (nolock)  as c ");
            strSQL.Append("  on a.IdEmpregado = c.nId_Empregado ");
            strSQL.Append("  left join epiclienteca (nolock)  as d ");
            strSQL.Append("  on b.IdEPIClienteCA = d.IdEPIClienteCa ");
            strSQL.Append("  left join EPI (nolock)  as e ");
            strSQL.Append("  on d.IdEPI = e.IdEPI ");
            strSQL.Append("  left join CA (nolock)  as f ");
            strSQL.Append("  on d.IdCA = f.IdCA ");
            strSQL.Append("  where a.IdEmpregado in ");
            strSQL.Append("  ( ");
            strSQL.Append("     select distinct e.nID_Empregado ");
            strSQL.Append("     from " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEmpregado (nolock)  e  ");
            strSQL.Append("     left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEmpregado_Funcao (nolock)  ef on e.nID_Empregado = ef.nID_Empregado  ");
            //strSQL.Append("     where e.nID_EMPR = " + xIdEmpresa.ToString() + "  ");
            //strSQL.Append("     and ( ef.nID_EMPR = " + xIdEmpresa.ToString() + "   or ef.nID_EMPR is null ) ");
            strSQL.Append("     where  ( ef.nID_EMPR = " + xIdEmpresa.ToString() + "   or e.nID_EMPR = " + xIdEmpresa.ToString() + " ) ");
            strSQL.Append("  ) ");
            strSQL.Append("  and DataRecebimento between convert( smalldatetime, '" + zD1 + "', 103 ) and convert( smalldatetime, '" + zD2 + "', 103 ) ");
            strSQL.Append("and e.Descricao is not null ");

            if (zIdEPICAEntrega != 0)
            {
                strSQL.Append("and a.IDEPICAEntrega = " + zIdEPICAEntrega.ToString() + "  ");
            }


            if (zEmpr != 0)
            {
                strSQL.Append("and c.nId_Empregado = " + zEmpr.ToString() + "  ");
            }


            strSQL.Append("order by c.tNO_EMPG, convert( smalldatetime, DataRecebimento, 103 ) ");



            using (SqlConnection cnn = new SqlConnection(Ilitera.Data.SQLServer.EntitySQLServer.GetConnection()))
            {
                SqlDataAdapter da;
                cnn.Open();


                da = new SqlDataAdapter(strSQL.ToString(), cnn);
                da.Fill(m_ds, "Result2");

                cnn.Close();

                da.Dispose();
            }


            //reorganizar os dados em "Result",  jogando os EPIs nos lugares corretos

            DataTable dt = m_ds.Tables["Result2"];

            newRow = m_ds2.Tables["Result"].NewRow();


            foreach (DataRow dr in dt.Rows)
            {




                if (xNome_Old != "")
                {


                    xAux = zFoto;

                    if (xAux.Trim() == "0" || xAux.Trim() == "")
                    {
                        newRow["ComFoto"] = "false";
                    }
                    else
                    {


                        for (int zCont = xAux.Length; zCont < ArqFotoEmrpegQteDigitos; zCont++)
                        {
                            xAux = "0" + xAux;
                        }

                        xArquivo = "I:\\FOTOSDOCSDIGITAIS\\" + xDiretorio_Padrao + "\\Organogramas\\" + ArqFotoEmpregInicio + xAux + ArqFotoEmpregTermino + ArqFotoEmpregExtensao;

                        if (xArquivo.ToUpper().IndexOf("FOTOSDOCSDIGITAIS") < 1)
                        {
                            xArquivo = xArquivo.Substring(0, xArquivo.IndexOf("\\") + 1) + "FOTOSDOCSDIGITAIS\\" + xArquivo.Substring(xArquivo.IndexOf("\\") + 1);
                        }

                        string pathFoto = xArquivo;

                        if (pathFoto != "")
                        {

                            newRow["iFOTO"] = GetByteFoto_Uri(pathFoto);
                            newRow["ComFoto"] = "true";
                        }
                        else
                        {
                            newRow["ComFoto"] = false;
                        }
                    }


                    newRow["TOTAL"] = xTotal;

                    //if (dataLogo != null)
                    //{
                    //    newRow["iFotoLogo"] = dataLogo;
                    //    newRow["ComFotoLogo"] = "true";
                    //}
                    //else
                    //{
                    //    newRow["ComFotoLogo"] = "false";
                    //}

                    if (zLogotipo != "")
                    {
                        newRow["iFotoLogo"] = GetByteFoto_Uri(zLogotipo);
                        newRow["ComFotoLogo"] = "true";
                    }
                    else
                    {
                        newRow["ComFotoLogo"] = "false";
                    }


                    m_ds2.Tables["Result"].Rows.Add(newRow);
                    newRow = m_ds2.Tables["Result"].NewRow();
                    //xTotal = 0;
                }


                if (zLogotipo != "")
                {
                    newRow["iFotoLogo"] = GetByteFoto_Uri(zLogotipo);
                    newRow["ComFotoLogo"] = "true";
                }
                else
                {
                    newRow["ComFotoLogo"] = "false";
                }



                newRow["RAZAO_SOCIAL"] = xEmpresa;
                newRow["NOME_EMPREGADO"] = dr["NOME_EMPREGADO"].ToString();
                newRow["DATA_NASCIMENTO"] = dr["DATA_NASCIMENTO"].ToString();
                newRow["DATA_ADMISSAO"] = dr["DATA_ADMISSAO"].ToString();
                newRow["SEXO"] = dr["SEXO"].ToString();
                newRow["RG"] = dr["RG"].ToString();
                newRow["RE"] = dr["RE"].ToString();

                xFuncao = dr["FUNCAO_SETOR"].ToString();

                if (xFuncao.IndexOf("|") > 0)
                {
                    newRow["FUNCAO"] = xFuncao.Substring(0, xFuncao.IndexOf("|"));
                    newRow["SETOR"] = xFuncao.Substring(xFuncao.IndexOf("|") + 1);
                }
                else
                {
                    newRow["FUNCAO"] = "";
                    newRow["SETOR"] = "";
                }



                zFoto = dr["nNO_FOTO"].ToString();

                //newRow["FUNCAO"] = dr["FUNCAO"].ToString();
                //newRow["SETOR"] = dr["SETOR"].ToString();

                if (xNome_Old != dr["NOME_EMPREGADO"].ToString())
                {
                    xTotal = 0;
                }
                
                xNome_Old = dr["NOME_EMPREGADO"].ToString();

                if (dr["Origem"].ToString().Trim() == "B")
                {
                    newRow["CA2"] = "Serial: " + dr["Serial"].ToString().Trim() + System.Environment.NewLine +
                                    "ID " + dr["IdEPICAEntregaDetalhe"].ToString().Trim();
                    newRow["CA3"] = dr["Digital"].ToString().Trim();
                }
                else
                {
                    newRow["CA2"] = "";
                    newRow["CA3"] = "";
                }


                newRow["NOME_EPI"] = dr["NOME_EPI"].ToString();
                newRow["CA"] = dr["CA"].ToString();
                newRow["QTD_ENTREGUE"] = dr["QTD_ENTREGUE"].ToString();
                newRow["DATA_FORNECIMENTO"] = dr["DATA_FORNECIMENTO"].ToString();
                if (dr["QTD_ENTREGUE"].ToString().Trim() != "")
                {
                    xTotal = xTotal + System.Convert.ToInt32(dr["QTD_ENTREGUE"].ToString());
                }
                newRow["TOTAL"] = xTotal;
                //xCont = 1;
                //}
                //else
                //{
                //    xCont++;

                //    if (xCont == 2)
                //    {
                //        newRow["NOME_EPI2"] = dr["NOME_EPI"].ToString();
                //        newRow["CA2"] = dr["CA"].ToString();
                //        newRow["QTD_ENTREGUE2"] = dr["QTD_ENTREGUE"].ToString();
                //        newRow["DATA_FORNECIMENTO2"] = dr["DATA_FORNECIMENTO"].ToString();
                //        xTotal = xTotal + System.Convert.ToInt32(dr["QTD_ENTREGUE"].ToString());
                //    }
                //    else if (xCont == 3)
                //    {
                //        newRow["NOME_EPI3"] = dr["NOME_EPI"].ToString();
                //        newRow["CA3"] = dr["CA"].ToString();
                //        newRow["QTD_ENTREGUE3"] = dr["QTD_ENTREGUE"].ToString();
                //        newRow["DATA_FORNECIMENTO3"] = dr["DATA_FORNECIMENTO"].ToString();
                //        xTotal = xTotal + System.Convert.ToInt32(dr["QTD_ENTREGUE"].ToString());
                //    }
                //    else if (xCont == 4)
                //    {
                //        newRow["NOME_EPI4"] = dr["NOME_EPI"].ToString();
                //        newRow["CA4"] = dr["CA"].ToString();
                //        newRow["QTD_ENTREGUE4"] = dr["QTD_ENTREGUE"].ToString();
                //        newRow["DATA_FORNECIMENTO4"] = dr["DATA_FORNECIMENTO"].ToString();
                //        xTotal = xTotal + System.Convert.ToInt32(dr["QTD_ENTREGUE"].ToString());
                //    }
                //}



                //string ArqFotoEmpregInicio
                //string ArqFotoEmpregTermino
                //int ArqFotoEmrpegQteDigitos
                //string ArqFotoEmpregExtensao 




                //try
                //{
                //    string pathFoto = "";

                //    if (pathFoto != string.Empty && System.IO.File.Exists(pathFoto))
                //    {
                //        newRow["iFOTO"] = GetByteFoto_Uri(pathFoto);
                //        newRow["ComFoto"] = true;
                //    }
                //    else
                //        newRow["ComFoto"] = false;
                //}
                //catch (Exception ex)
                //{
                //    newRow["ComFoto"] = false;
                //    System.Diagnostics.Trace.WriteLine(ex.Message.ToString());
                //}



            }


            if (xNome_Old != "")
            {

                xAux = zFoto;

                if (xAux.Trim() == "0" || xAux.Trim() == "")
                {
                    newRow["ComFoto"] = "false";
                }
                else
                {


                    for (int zCont = xAux.Length; zCont < ArqFotoEmrpegQteDigitos; zCont++)
                    {
                        xAux = "0" + xAux;
                    }

                    xArquivo = "I:\\FOTOSDOCSDIGITAIS\\" + xDiretorio_Padrao + "\\Organogramas\\" + ArqFotoEmpregInicio + xAux + ArqFotoEmpregTermino + ArqFotoEmpregExtensao;

                    if (xArquivo.ToUpper().IndexOf("FOTOSDOCSDIGITAIS") < 1)
                    {
                        xArquivo = xArquivo.Substring(0, xArquivo.IndexOf("\\") + 1) + "FOTOSDOCSDIGITAIS\\" + xArquivo.Substring(xArquivo.IndexOf("\\") + 1);
                    }

                    string pathFoto = xArquivo;

                    if (pathFoto != "")
                    {

                        newRow["iFOTO"] = GetByteFoto_Uri(pathFoto);
                        newRow["ComFoto"] = "true";
                    }
                    else
                    {
                        newRow["ComFoto"] = false;
                    }
                }


                newRow["TOTAL"] = xTotal;

                if (dataLogo != null)
                {
                    newRow["iFotoLogo"] = dataLogo;
                    newRow["ComFotoLogo"] = "true";
                }
                else
                {
                    newRow["ComFotoLogo"] = "false";
                }


                m_ds2.Tables["Result"].Rows.Add(newRow);
            }

            return m_ds2;


        }





        public static byte[] GetByteFoto_Uri(string sURIFoto)
        {
            string xLoc = "";
            string xLoc2 = "";
            WebClient webclient = new WebClient();
            byte[] bytesArray = null;

            xLoc = sURIFoto.Replace("\\", "/");
            //xLoc2 = xLoc.Replace("I:", "http://54.94.157.244/driveI");
            //xLoc2 = xLoc.Replace("I:", "http://www.ilitera.net.br/driveI");
            xLoc2 = xLoc.Replace("http://www.ilitera.net.br/driveI", "I:");
            xLoc2 = xLoc2.Replace("/", "\\");

            //xLoc2 = xLoc.Replace("I:", "http://ilitera.dyndns.ws:8888/driveI");


            bytesArray = webclient.DownloadData(xLoc2);


            return bytesArray;

        }


        public static byte[] GetByteFoto_Uri(string sURIFoto, string xWeb_Server)
        {
            string xLoc = "";
            string xLoc2 = "";
            WebClient webclient = new WebClient();
            byte[] bytesArray = null;

            xLoc = sURIFoto.Replace("\\", "/");
            

            if (xWeb_Server == "ILITERA")
               xLoc2 = xLoc.Replace("I:", "http://ilitera.dyndns.ws:8888/driveI");
            else
               xLoc2 = xLoc.Replace("I:", "http://187.45.232.35/driveI");

            xLoc2 = xLoc.Replace("http://www.ilitera.net.br/driveI", "I:");
            xLoc2 = xLoc2.Replace("/", "\\");


            bytesArray = webclient.DownloadData(xLoc2);


            return bytesArray;

        }




        public DataSet Trazer_CAs(int xIdEmpregado)
        {

            StringBuilder strSQL = new StringBuilder();


            DataTable table = new DataTable("Result");
            table.Columns.Add("IdEmpregado", Type.GetType("System.String"));
            table.Columns.Add("Habitual_Eventual", Type.GetType("System.String"));
            table.Columns.Add("CA", Type.GetType("System.String"));
            table.Columns.Add("EPI", Type.GetType("System.String"));
            table.Columns.Add("DataEntrega", Type.GetType("System.String"));
            table.Columns.Add("Descricao", Type.GetType("System.String"));
            table.Columns.Add("Tipo", Type.GetType("System.String"));
            table.Columns.Add("Tamanho", Type.GetType("System.String"));
            table.Columns.Add("Conjunto", Type.GetType("System.String"));
            table.Columns.Add("Motivo", Type.GetType("System.String"));


            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);


            strSQL.Append("  SELECT a.IdEPI_Ca as IdEmpregado, case when a.Habitual_Eventual = 'H' then 'Habitual' when a.Habitual_Eventual = 'E' then 'Eventual' end as Frequencia, a.CA, b.Nome as EPI, a.dDTEntrega as DataEntrega, a.Descricao, a.Tipo, a.Tamanho, a.Conjunto, a.Motivo, convert( char(10),c.hDT_LAUDO,103) as Data_Laudo ");
            strSQL.Append("  FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEPI_CA (nolock)  as a left join  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.EPI as b on a.IdEPI = b.IdEPI ");
            strSQL.Append("  left join  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblLaudo_Tec  (nolock) as c on a.IdLaudoTecnico = c.nID_LAUD_TEC ");
            strSQL.Append("  WHERE  a.nIdEmpregado = ");
            strSQL.Append(xIdEmpregado.ToString());
            strSQL.Append("  ORDER BY a.dDTEntrega ");


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






        public DataSet Trazer_Exames_Clinicos_Sem_Resultado_Em_Espera(int xIdEmpregado)
        {
            //StringBuilder strSQL = new StringBuilder();
            DataTable table = new DataTable("Result");
            table.Columns.Add("IdExameBase", Type.GetType("System.String"));
            table.Columns.Add("Data_Exame", Type.GetType("System.String"));
            table.Columns.Add("Exame", Type.GetType("System.String"));

            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);

            SqlCommand rCommand = new SqlCommand();

            rCommand.Parameters.Add("@xIdEmpregado", SqlDbType.NChar).Value = xIdEmpregado.ToString();

            rCommand.CommandText = "select IdExameBase, convert( char(10), dataexame, 103 ) as Data_Exame, b.Nome as Exame "
            + "from examebase (nolock)  as a left join examedicionario (nolock)  as b on ( a.idexamedicionario = b.idexamedicionario ) "
            + "where IdExameBase in "
            + "( select IdExameBase from Clinico (nolock)  ) "
            + "and IndResultado in ( 0,3 ) "
            + "and a.IdEmpregado = @xIdEmpregado "
            + "order by dataexame desc ";

            //strSQL.Append("select IdExameBase, convert( char(10), dataexame, 103 ) as Data_Exame, b.Nome as Exame " ) ;
            //strSQL.Append("from examebase as a left join examedicionario as b on ( a.idexamedicionario = b.idexamedicionario ) " ) ;
            //strSQL.Append("where IdExameBase in " ) ;
            //strSQL.Append("( select IdExameBase from Clinico ) " ) ;
            //strSQL.Append("and IndResultado in ( 0,3 ) " ) ;
            //strSQL.Append("and a.IdEmpregado = ");
            //strSQL.Append(xIdEmpregado.ToString() + "  " );
            //strSQL.Append("order by dataexame desc ");


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





        public DataSet Trazer_Laudo_Periculosidade(int xEmpresa)
        {
            //StringBuilder strSQL = new StringBuilder();

            DataTable table = new DataTable("Result");
            
            table.Columns.Add("Descricao", Type.GetType("System.String"));
            table.Columns.Add("Path", Type.GetType("System.String"));


            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);


            SqlCommand rCommand = new SqlCommand();

            rCommand.Parameters.Add("@xEmpresa", SqlDbType.NChar).Value = xEmpresa.ToString();

            rCommand.CommandText = "SELECT Descricao, Path "
            + "FROM Laudo_Periculosidade_Server "
            + "WHERE IdCliente= @xEmpresa "
            + "ORDER BY Descricao ";


            //strSQL.Append("SELECT Descricao, Path ");
            //strSQL.Append("FROM Laudo_Periculosidade_Server ");
            //strSQL.Append("WHERE IdCliente=" + xEmpresa.ToString() + " ");
            //strSQL.Append("ORDER BY Descricao ");

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


        public DataSet Trazer_Mapa_Riscos(int xEmpresa)
        {
            //StringBuilder strSQL = new StringBuilder();

            DataTable table = new DataTable("Result");

            table.Columns.Add("Descricao", Type.GetType("System.String"));
            table.Columns.Add("Path", Type.GetType("System.String"));


            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);

            SqlCommand rCommand = new SqlCommand();

            rCommand.Parameters.Add("@xEmpresa", SqlDbType.NChar).Value = xEmpresa.ToString();

            rCommand.CommandText = "SELECT Descricao, Path "
            + "FROM Mapa_Risco_Server "
            + "WHERE IdCliente= @xEmpresa "
            + "ORDER BY Descricao ";

            //strSQL.Append("SELECT Descricao, Path ");
            //strSQL.Append("FROM Mapa_Risco_Server ");
            //strSQL.Append("WHERE IdCliente=" + xEmpresa.ToString() + " ");
            //strSQL.Append("ORDER BY Descricao ");

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


        public DataSet Trazer_PAE(int xEmpresa)
        {
            //StringBuilder strSQL = new StringBuilder();

            DataTable table = new DataTable("Result");

            table.Columns.Add("Descricao", Type.GetType("System.String"));
            table.Columns.Add("Path", Type.GetType("System.String"));


            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);

            SqlCommand rCommand = new SqlCommand();

            rCommand.Parameters.Add("@xEmpresa", SqlDbType.NChar).Value = xEmpresa.ToString();

            rCommand.CommandText = "SELECT Descricao, Path "
            + "FROM PAE_Risco_Server "
            + "WHERE IdCliente= @xEmpresa "
            + "ORDER BY Descricao ";

          
            using (SqlConnection cnn = new SqlConnection(Ilitera.Data.SQLServer.EntitySQLServer.GetConnection()))
            {
                cnn.Open();

                rCommand.Connection = cnn;
                SqlDataAdapter da;

                da = new SqlDataAdapter(rCommand);
                da.Fill(m_ds, "Result");

               

                cnn.Close();

                da.Dispose();
            }

            return m_ds;

        }



        public DataSet Trazer_RTI(int xEmpresa)
        {
            //StringBuilder strSQL = new StringBuilder();

            DataTable table = new DataTable("Result");

            table.Columns.Add("Descricao", Type.GetType("System.String"));
            table.Columns.Add("Path", Type.GetType("System.String"));


            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);

            SqlCommand rCommand = new SqlCommand();

            rCommand.Parameters.Add("@xEmpresa", SqlDbType.NChar).Value = xEmpresa.ToString();

            rCommand.CommandText = "SELECT Descricao, Path "
            + "FROM RTI_Risco_Server "
            + "WHERE IdCliente= @xEmpresa "
            + "ORDER BY Descricao ";


            using (SqlConnection cnn = new SqlConnection(Ilitera.Data.SQLServer.EntitySQLServer.GetConnection()))
            {
                cnn.Open();

                rCommand.Connection = cnn;
                SqlDataAdapter da;

                da = new SqlDataAdapter(rCommand);
                da.Fill(m_ds, "Result");



                cnn.Close();

                da.Dispose();
            }

            return m_ds;

        }



        public DataSet Trazer_SPDA(int xEmpresa)
        {
            //StringBuilder strSQL = new StringBuilder();

            DataTable table = new DataTable("Result");

            table.Columns.Add("Descricao", Type.GetType("System.String"));
            table.Columns.Add("Path", Type.GetType("System.String"));


            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);

            SqlCommand rCommand = new SqlCommand();

            rCommand.Parameters.Add("@xEmpresa", SqlDbType.NChar).Value = xEmpresa.ToString();

            rCommand.CommandText = "SELECT Descricao, Path "
            + "FROM SPDA_Risco_Server "
            + "WHERE IdCliente= @xEmpresa "
            + "ORDER BY Descricao ";


            using (SqlConnection cnn = new SqlConnection(Ilitera.Data.SQLServer.EntitySQLServer.GetConnection()))
            {
                cnn.Open();

                rCommand.Connection = cnn;
                SqlDataAdapter da;

                da = new SqlDataAdapter(rCommand);
                da.Fill(m_ds, "Result");



                cnn.Close();

                da.Dispose();
            }

            return m_ds;

        }


        public DataSet Trazer_Vasos_Caldeira(int xEmpresa)
        {

            StringBuilder strSQL = new StringBuilder();


            DataTable table = new DataTable("Result");
            table.Columns.Add("IdVasoPressao", Type.GetType("System.Int32"));
            table.Columns.Add("Numero_Localizacao", Type.GetType("System.String"));
            

            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);

            SqlCommand rCommand = new SqlCommand();

            rCommand.Parameters.Add("@xEmpresa", SqlDbType.NChar).Value = xEmpresa.ToString();

            rCommand.CommandText = "SELECT VasoPressao.IdVasoPressao, NumeroIdentificacao + ' - ' + Localizacao as Numero_Localizacao "
            + "FROM EquipamentoBase INNER JOIN VasoCaldeiraBase  " 
            + "ON EquipamentoBase.IdEquipamentoBase=VasoCaldeiraBase.IdVasoCaldeiraBase  " 
            + "INNER JOIN VasoPressao  " 
            + "ON VasoCaldeiraBase.IdVasoCaldeiraBase=VasoPressao.IdVasoPressao  " 
            + "WHERE IdCliente= @xEmpresa AND IsInativo=0  " 
            + "ORDER BY NumeroIdentificacao ";

            //strSQL.Append("SELECT VasoPressao.IdVasoPressao, NumeroIdentificacao + ' - ' + Localizacao as Numero_Localizacao ");
            //strSQL.Append("FROM EquipamentoBase INNER JOIN VasoCaldeiraBase  " ) ;
            //strSQL.Append("ON EquipamentoBase.IdEquipamentoBase=VasoCaldeiraBase.IdVasoCaldeiraBase  " ) ;
            //strSQL.Append("INNER JOIN VasoPressao  " ) ;
            //strSQL.Append("ON VasoCaldeiraBase.IdVasoCaldeiraBase=VasoPressao.IdVasoPressao  " ) ;
            //strSQL.Append("WHERE IdCliente=" + xEmpresa.ToString()  +  " AND IsInativo=0  " ) ;
            //strSQL.Append("ORDER BY NumeroIdentificacao ");


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




        public DataSet Trazer_Vasos_Projeto(int xVaso)
        {

            //StringBuilder strSQL = new StringBuilder();

            DataTable table = new DataTable("Result");
            table.Columns.Add("IdProjetoVasoCaldeira", Type.GetType("System.Int32"));
            table.Columns.Add("DataLevantamento", Type.GetType("System.String"));


            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);


            SqlCommand rCommand = new SqlCommand();

            rCommand.Parameters.Add("@xVaso", SqlDbType.NChar).Value = xVaso.ToString();

            rCommand.CommandText = "SELECT ProjetoVasoCaldeira.IdProjetoVasoCaldeira, convert( char(10),DataLevantamento,103 ) as DataLevantamento2 "
            + "FROM Documento INNER JOIN ProjetoVasoCaldeira " 
            + "ON Documento.IdDocumento=ProjetoVasoCaldeira.IdProjetoVasoCaldeira  " 
            + "WHERE IdVasoCaldeiraBase= @xVaso AND IndTipoProjeto =0 "
            + "Order by DataLevantamento ";


            //strSQL.Append("SELECT ProjetoVasoCaldeira.IdProjetoVasoCaldeira, convert( char(10),DataLevantamento,103 ) as DataLevantamento2 ");
            //strSQL.Append("FROM Documento INNER JOIN ProjetoVasoCaldeira " ) ;
            //strSQL.Append("ON Documento.IdDocumento=ProjetoVasoCaldeira.IdProjetoVasoCaldeira  " ) ;
            //strSQL.Append("WHERE IdVasoCaldeiraBase=" + xVaso.ToString()  + " AND IndTipoProjeto =0 ");
            //strSQL.Append("Order by DataLevantamento ");


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




        public DataSet Trazer_Vasos_Instalacao(int xVaso)
        {

            //StringBuilder strSQL = new StringBuilder();
            DataTable table = new DataTable("Result");
            table.Columns.Add("IdInspecaoVasoCaldeira", Type.GetType("System.Int32"));
            table.Columns.Add("DataLevantamento", Type.GetType("System.String"));


            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);



            SqlCommand rCommand = new SqlCommand();

            rCommand.Parameters.Add("@xVaso", SqlDbType.NChar).Value = xVaso.ToString();

            rCommand.CommandText = "SELECT InspecaoVasoCaldeira.IdInspecaoVasoCaldeira, convert( char(10),DataLevantamento,103 ) as DataLevantamento2 "
            + "FROM Documento INNER JOIN InspecaoVasoCaldeira  "
            + "ON Documento.IdDocumento=InspecaoVasoCaldeira.IdInspecaoVasoCaldeira  "
            + "WHERE IdVasoCaldeiraBase= @xVaso "
            + "ORDER BY DataLevantamento DESC ";


            //strSQL.Append("SELECT InspecaoVasoCaldeira.IdInspecaoVasoCaldeira, convert( char(10),DataLevantamento,103 ) as DataLevantamento2 " ) ;
            //strSQL.Append("FROM Documento INNER JOIN InspecaoVasoCaldeira  " ) ;
            //strSQL.Append("ON Documento.IdDocumento=InspecaoVasoCaldeira.IdInspecaoVasoCaldeira  " ) ;
            //strSQL.Append("WHERE IdVasoCaldeiraBase= " + xVaso.ToString() + " "  ) ;
            //strSQL.Append("ORDER BY DataLevantamento DESC ");

            
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




        public DataSet Trazer_Uniformes(int xIdEmpresa)
        {
           // StringBuilder strSQL = new StringBuilder();

            DataTable table = new DataTable("Result");
            table.Columns.Add("Id_Uniforme", Type.GetType("System.String"));
            table.Columns.Add("Uniforme", Type.GetType("System.String"));
            table.Columns.Add("Medida", Type.GetType("System.String"));
            table.Columns.Add("Obs", Type.GetType("System.String"));


            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);

            SqlCommand rCommand = new SqlCommand();

            rCommand.Parameters.Add("@xIdEmpresa", SqlDbType.NChar).Value = xIdEmpresa.ToString();

            rCommand.CommandText = "  SELECT Id_Uniforme, Uniforme, Medida, Obs "
            + "  FROM Uniforme "
            + "  Where IdPessoa = @xIdEmpresa " 
            + "  ORDER BY Uniforme ";

            //strSQL.Append("  SELECT Id_Uniforme, Uniforme, Medida, Obs ");
            //strSQL.Append("  FROM Uniforme ");
            //strSQL.Append("  Where IdPessoa = " + xIdEmpresa.ToString() + " " );
            //strSQL.Append("  ORDER BY Uniforme ");


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



        public void Atualizar_Preposto(int xIdPessoa, int xIdEmpresa)
        {
            
            using (SqlConnection cnn = new SqlConnection(Ilitera.Data.SQLServer.EntitySQLServer.GetConnection()))
            {

                string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection();

                Database objDB = new SqlDatabase(connString);

                var objCmd = objDB.GetStoredProcCommand("sp_Atualizar_Preposto");
                                
                objDB.AddInParameter(objCmd, "xIdPessoa", System.Data.DbType.String, xIdPessoa  );                
                objDB.AddInParameter(objCmd, "xIdEmpresa", System.Data.DbType.String, xIdEmpresa);

                var readerRetorno = objDB.ExecuteReader(objCmd);

                return;

            }

        }


        public void Atualizar_Preposto2(int xIdPessoa, int xIdEmpresa)
        {

            using (SqlConnection cnn = new SqlConnection(Ilitera.Data.SQLServer.EntitySQLServer.GetConnection()))
            {

                string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection();

                Database objDB = new SqlDatabase(connString);

                var objCmd = objDB.GetStoredProcCommand("sp_Atualizar_Preposto2");

                objDB.AddInParameter(objCmd, "xIdPessoa", System.Data.DbType.String, xIdPessoa);
                objDB.AddInParameter(objCmd, "xIdEmpresa", System.Data.DbType.String, xIdEmpresa);

                var readerRetorno = objDB.ExecuteReader(objCmd);

                return;

            }

        }




        public void Inserir_Dados_CA_EPI(int xIdLaudo, string xEPI, int xIdEmpregado, string xCA, string xDataEntrega, string xDescricao, string xConjunto, string xTamanho, string xTipo, string xMotivo, string xHabitual_Eventual)
        {

            using (SqlConnection cnn = new SqlConnection(Ilitera.Data.SQLServer.EntitySQLServer.GetConnection_Sied()))
            {

                string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection_Sied();

                Database objDB = new SqlDatabase(connString);

                var objCmd = objDB.GetStoredProcCommand("sp_Inserir_Dados_CA_EPI");

                objDB.AddInParameter(objCmd, "xIdLaudoTecnico", System.Data.DbType.String, xIdLaudo);
                objDB.AddInParameter(objCmd, "xEPI", System.Data.DbType.String, xEPI);
                objDB.AddInParameter(objCmd, "xIdEmpregado", System.Data.DbType.String, xIdEmpregado);
                objDB.AddInParameter(objCmd, "xCA", System.Data.DbType.String, xCA);
                objDB.AddInParameter(objCmd, "xDataEntrega", System.Data.DbType.String, xDataEntrega);
                objDB.AddInParameter(objCmd, "xDescricao", System.Data.DbType.String, xDescricao);
                objDB.AddInParameter(objCmd, "xConjunto", System.Data.DbType.String, xConjunto);
                objDB.AddInParameter(objCmd, "xTamanho", System.Data.DbType.String, xTamanho);
                objDB.AddInParameter(objCmd, "xTipo", System.Data.DbType.String, xTipo);
                objDB.AddInParameter(objCmd, "xMotivo", System.Data.DbType.String, xMotivo);
                objDB.AddInParameter(objCmd, "xHabitual_Eventual", System.Data.DbType.String, xHabitual_Eventual);

                var readerRetorno = objDB.ExecuteReader(objCmd);

                return;

            }

        }


        public void Excluir_CA_EPI(int xIdEPI_CA)
        {

            using (SqlConnection cnn = new SqlConnection(Ilitera.Data.SQLServer.EntitySQLServer.GetConnection_Sied()))
            {

                string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection_Sied();

                Database objDB = new SqlDatabase(connString);

                var objCmd = objDB.GetStoredProcCommand("sp_Excluir_CA_EPI");

                objDB.AddInParameter(objCmd, "xIdEPI_CA", System.Data.DbType.String, xIdEPI_CA);

                var readerRetorno = objDB.ExecuteReader(objCmd);

                return;

            }

        }




        public void Excluir_Exame_Clinico(int xIdExameBase)
        {

            using (SqlConnection cnn = new SqlConnection(Ilitera.Data.SQLServer.EntitySQLServer.GetConnection()))
            {

                string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection();

                Database objDB = new SqlDatabase(connString);

                var objCmd = objDB.GetStoredProcCommand("sp_Excluir_Exame_Clinico");

                objDB.AddInParameter(objCmd, "xIdExameBase", System.Data.DbType.String, xIdExameBase);

                var readerRetorno = objDB.ExecuteReader(objCmd);

                return;

            }

        }



        public DataSet Trazer_EPIs()
        {
            //StringBuilder strSQL = new StringBuilder();
            DataTable table = new DataTable("Result");
            table.Columns.Add("IdEPI", Type.GetType("System.String"));
            table.Columns.Add("Nome", Type.GetType("System.String"));

            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);

            SqlCommand rCommand = new SqlCommand();

            rCommand.CommandText = "  SELECT IdEPI, Nome from EPI where Nome is not null and Nome <> '' order by Nome ";

            //strSQL.Append("  SELECT IdEPI, Nome from EPI where Nome is not null and Nome <> '' order by Nome ");


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



        public DataSet Carregar_Setores( int xEmpresa )
        {

            //StringBuilder strSQL = new StringBuilder();
            DataTable table = new DataTable("Result");
            table.Columns.Add("nID_SETOR", Type.GetType("System.Int32"));
            table.Columns.Add("tNO_STR_EMPR", Type.GetType("System.String"));

            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);


            SqlCommand rCommand = new SqlCommand();

            rCommand.Parameters.Add("@xEmpresa", SqlDbType.NChar).Value = xEmpresa.ToString();

            //rCommand.CommandText = "Select 0 as nId_Setor, ' ' as tNO_STR_EMPR union SELECT tblSETOR.nID_SETOR, tNO_STR_EMPR FROM tblSETOR WHERE nID_EMPR = @xEmpresa ORDER BY tNO_STR_EMPR";
            rCommand.CommandText = "select 0 as nID_SETOR, ' ' as tNO_STR_EMPR union SELECT tblSETOR.nID_SETOR, tNO_STR_EMPR FROM tblSETOR (nolock)  WHERE nID_EMPR = @xEmpresa and tNO_STR_EMPR is not null ORDER BY tNO_STR_EMPR";

            //strSQL.Append("Select 0 as nId_Setor, ' ' as tNO_STR_EMPR union SELECT tblSETOR.nID_SETOR, tNO_STR_EMPR FROM tblSETOR WHERE nID_EMPR=" + xEmpresa.ToString() +  " ORDER BY tNO_STR_EMPR");


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



        public DataSet Carregar_Cargos(int xEmpresa)
        {

            //StringBuilder strSQL = new StringBuilder();

            DataTable table = new DataTable("Result");
            table.Columns.Add("nID_CARGO", Type.GetType("System.Int32"));
            table.Columns.Add("tNO_CARGO", Type.GetType("System.String"));

            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);

            SqlCommand rCommand = new SqlCommand();

            rCommand.Parameters.Add("@xEmpresa", SqlDbType.NChar).Value = xEmpresa.ToString();

            rCommand.CommandText = "select 0 as nID_CARGO, ' ' as tNO_CARGO union SELECT tblCARGO.nID_CARGO, tNO_CARGO FROM tblCARGO (nolock)  WHERE nID_EMPR= @xEmpresa ORDER BY tNO_CARGO";
            
            //strSQL.Append("select 0 as nID_CARGO, ' ' as tNO_CARGO union SELECT tblCARGO.nID_CARGO, tNO_CARGO FROM tblCARGO WHERE nID_EMPR=" + xEmpresa.ToString() + " ORDER BY tNO_CARGO");


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




        public DataSet Carregar_Funcoes(int xEmpresa)
        {

            //StringBuilder strSQL = new StringBuilder();

            DataTable table = new DataTable("Result");
            table.Columns.Add("IdFuncao", Type.GetType("System.Int32"));
            table.Columns.Add("NomeFuncao", Type.GetType("System.String"));

            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);

            SqlCommand rCommand = new SqlCommand();

            rCommand.Parameters.Add("@xEmpresa", SqlDbType.NChar).Value = xEmpresa.ToString();

            rCommand.CommandText = "Select 0 as IdFuncao, ' ' as NomeFuncao union SELECT Funcao.IdFuncao, NomeFuncao FROM Funcao (nolock)  WHERE IdCliente= @xEmpresa and NomeFuncao is not null and NomeFuncao<>''  order by NomeFuncao ";

            //strSQL.Append("Select 0 as IdFuncao, ' ' as NomeFuncao union SELECT Funcao.IdFuncao, NomeFuncao FROM Funcao WHERE IdCliente=" + xEmpresa.ToString() + " order by NomeFuncao ");


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


        public DataSet Carregar_Funcoes_Viterra(int xEmpresa)
        {

            //StringBuilder strSQL = new StringBuilder();

            DataTable table = new DataTable("Result");
            table.Columns.Add("IdFuncao", Type.GetType("System.Int32"));
            table.Columns.Add("NomeFuncao", Type.GetType("System.String"));

            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);

            SqlCommand rCommand = new SqlCommand();

            rCommand.Parameters.Add("@xEmpresa", SqlDbType.NChar).Value = xEmpresa.ToString();

            rCommand.CommandText = "Select 0 as IdFuncao, ' ' as NomeFuncao union SELECT Funcao.IdFuncao, case when CodigoFuncao <> '' then NomeFuncao + ' | ' + CodigoFuncao else NomeFuncao end as NomeFuncao FROM Funcao (nolock)  WHERE IdCliente= @xEmpresa and NomeFuncao is not null and NomeFuncao<>'' order by NomeFuncao ";

            //strSQL.Append("Select 0 as IdFuncao, ' ' as NomeFuncao union SELECT Funcao.IdFuncao, NomeFuncao FROM Funcao WHERE IdCliente=" + xEmpresa.ToString() + " order by NomeFuncao ");


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




        public string Trazer_CAs(int xIdEmpregado, int xIdLaudoTecnico, int xIdEPI)
        {
            string xRet = "";

            using (SqlConnection cnn = new SqlConnection(Ilitera.Data.SQLServer.EntitySQLServer.GetConnection_Sied()))
            {

                string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection_Sied();

                Database objDB = new SqlDatabase(connString);

                var objCmd = objDB.GetStoredProcCommand("sp_Buscar_CAs");


                objDB.AddInParameter(objCmd, "xIdEmpregado", System.Data.DbType.Int32, xIdEmpregado);
                objDB.AddInParameter(objCmd, "xIdLaudoTecnico", System.Data.DbType.String, xIdLaudoTecnico);
                objDB.AddInParameter(objCmd, "xIdEPI", System.Data.DbType.String,xIdEPI );

                var readerRetorno = objDB.ExecuteReader(objCmd);

                while (readerRetorno.Read())
                {
                    xRet = readerRetorno[0].ToString();
                }


                return xRet;


            }
        }


        public String Trazer_GHE_Atual(int xIdEmpregado, int xIdEmpresa)
        {

            StringBuilder strSQL = new StringBuilder();


            DataTable table = new DataTable("Result");
            table.Columns.Add("tNo_Func", Type.GetType("System.String"));


            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);


            strSQL.Append("  select tno_func from tblFunc " );
            strSQL.Append("  where nId_Func in ( " );
            strSQL.Append("     select nId_Func FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.qryEmpregadoFuncaoGHE  " );
            strSQL.Append("     where nID_Empregado = " + xIdEmpregado.ToString() + " " );
            strSQL.Append("  ) " );
            strSQL.Append("  and nId_Laud_tec in " );
            strSQL.Append("  ( " );
            strSQL.Append("  SELECT top 1 tblLAUDO_TEC.nID_LAUD_TEC " );
            strSQL.Append("     FROM tblLAUDO_TEC  " );
            strSQL.Append("     WHERE nID_EMPR= " + xIdEmpresa.ToString() + " ");
            strSQL.Append("     ORDER BY hDT_LAUDO DESC " );
            strSQL.Append("  ) ");
            
            
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

            if (m_ds.Tables[0].Rows.Count > 0)
                return m_ds.Tables[0].Rows[0][0].ToString();
            else
                return " ";


        }





        public String Checar_Duplicidade_Preposto(string xNome, string xNIT)
        {

            StringBuilder strSQL = new StringBuilder();


            DataTable table = new DataTable("Result");
            table.Columns.Add("IdPessoa", Type.GetType("System.String"));


            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);

            SqlCommand rCommand = new SqlCommand();

            rCommand.Parameters.Add("@xNome", SqlDbType.NChar).Value = xNome;
            rCommand.Parameters.Add("@xNIT", SqlDbType.NChar).Value = xNIT;  //agora será o CPF

            rCommand.CommandText =
              "  select a.IdPessoa from Pessoa (nolock)  as a "
            + "  join JuridicaPessoa (nolock)  as b on ( a.IdPessoa = b.IdPessoa ) "
            + "  join Prestador (nolock)  as c on ( b.IdJuridicaPessoa = c.IdJuridicaPessoa ) "
            + "  where ( a.NomeCompleto = @xNome or a.NomeAbreviado = @xNome ) "
            + "  and c.CPF = @xNIT ";
            //+ "  and NomeCodigo = @xNIT ";



            //strSQL.Append("  select IdPessoa from Pessoa ");
            //strSQL.Append("  where ( NomeCompleto = '" + xNome.Trim() + "' or NomeAbreviado = '" + xNome.Trim() + "' ) " );
            //strSQL.Append("  and NomeCodigo = '" + xNIT.Trim() + "' ");


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

            if (m_ds.Tables[0].Rows.Count > 0)
                return m_ds.Tables[0].Rows[0][0].ToString();
            else
                return "0";


        }







        public DataSet Lista_CBO(string xFiltro)
        {

            StringBuilder strSQL = new StringBuilder();


            DataTable table = new DataTable("Result");
            table.Columns.Add("Id", Type.GetType("System.Int32"));
            table.Columns.Add("CBO", Type.GetType("System.String"));
            table.Columns.Add("CBONOME", Type.GetType("System.String"));


            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);


            strSQL.Append("USE cbo   ");
            strSQL.Append("SELECT CBO, Id, CBONOME from ( ");
            strSQL.Append("SELECT '0' as CBO, '0' as Id, '( Sem CBO ) ' as CBONOME  ");
            strSQL.Append(" union ");            
            strSQL.Append("SELECT CBO, TableCBO.Id, CBO + '  ' + NOME as CBONOME ");
            strSQL.Append("FROM TableCBO   ");
            if (xFiltro != "")
            {
                strSQL.Append("WHERE (NOME Like'%" + xFiltro + "%' OR  CBO Like '%" + xFiltro + "%')   ");
            }
            strSQL.Append(" ) as tx1 ");
            strSQL.Append("ORDER BY CBONOME  ");


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



        public Int32 Retorna_Preposto( int xIdEmpresa)
        {
            //StringBuilder strSQL = new StringBuilder();
            DataTable table = new DataTable("Result");
            table.Columns.Add("IdRespPPP", Type.GetType("System.Int32"));
            

            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);

            SqlCommand rCommand = new SqlCommand();

            rCommand.Parameters.Add("@xIdEmpresa", SqlDbType.NChar).Value = xIdEmpresa.ToString();

            rCommand.CommandText = "SELECT IdRespPPP from Cliente where IdCliente = @xIdEmpresa ";


            //strSQL.Append("SELECT IdRespPPP from Cliente where IdCliente = " + xIdEmpresa.ToString());
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


            if (m_ds.Tables[0].Rows.Count > 0)
                if (m_ds.Tables[0].Rows[0][0].ToString().Trim() == "")
                {
                    return 0; 
                } 
                else
                {
                    return System.Convert.ToInt32(m_ds.Tables[0].Rows[0][0].ToString());
                }
            else
                return 0;
            

        }



        public Int32 Retorna_Preposto2(int xIdEmpresa)
        {

           // StringBuilder strSQL = new StringBuilder();
           
            DataTable table = new DataTable("Result");
            table.Columns.Add("IdRespPPP2", Type.GetType("System.Int32"));
            
            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);

            SqlCommand rCommand = new SqlCommand();

            rCommand.Parameters.Add("@xIdEmpresa", SqlDbType.NChar).Value = xIdEmpresa.ToString();

            rCommand.CommandText = "SELECT IdRespPPP2 from Cliente where IdCliente = @xIdEmpresa ";

            //strSQL.Append("SELECT IdRespPPP2 from Cliente where IdCliente = " + xIdEmpresa.ToString());
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


            if (m_ds.Tables[0].Rows.Count > 0)
                if (m_ds.Tables[0].Rows[0][0].ToString().Trim() == "")
                {
                    return 0;
                }
                else
                {
                    return System.Convert.ToInt32(m_ds.Tables[0].Rows[0][0].ToString());
                }
            else
                return 0;


        }



        public DataSet Lista_Prestadores()
        {

            //StringBuilder strSQL = new StringBuilder();

            DataTable table = new DataTable("Result");
            table.Columns.Add("IdPessoa", Type.GetType("System.Int32"));
            table.Columns.Add("Nome_Codigo", Type.GetType("System.String"));
            

            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);

            SqlCommand rCommand = new SqlCommand();

            //rCommand.Parameters.Add("@zIdCliente", SqlDbType.NChar).Value = zIdCliente.ToString();

            rCommand.CommandText = "SELECT IdPessoa, Nome_Codigo FROM ( "
            + "SELECT 0 as IdPessoa, '- Não definido -' as Nome_Codigo "
            + "union "
            + "Select a.IdPessoa, a.NomeCompleto + ' - ' + isnull(d.CPF,' ') as Nome_Codigo "  //b.NomeCodigo as Nome_Codigo "
            + "FROM qryPrestador as a "
            + "left join Pessoa (nolock)  as b on ( a.IdPessoa = b.IdPessoa ) "
            + "join JuridicaPessoa (nolock)  as c on ( b.IdPessoa = c.IdPessoa ) "
            + "join Prestador (nolock)  as d on ( c.IdJuridicaPessoa = d.IdJuridicaPessoa ) "
            + "WHERE a.IdJuridica=310  "
            + " AND a.IsInativo=0 "
            + " and a.NomeCompleto is not null and a.NomeCompleto <> '' "
            + " ) as tx1 "            
            + " order by NOME_CODIGO ";

            //strSQL.Append("SELECT IdPessoa, Nome_Codigo FROM ( ");
            //strSQL.Append("SELECT 0 as IdPessoa, '- Não definido -' as Nome_Codigo ");
            //strSQL.Append("union ");
            //strSQL.Append("Select a.IdPessoa, a.NomeCompleto + ' - ' + b.NomeCodigo as Nome_Codigo ");
            //strSQL.Append("FROM qryPrestador as a ");
            //strSQL.Append("left join Pessoa as b on ( a.IdPessoa = b.IdPessoa ) ");
            //strSQL.Append("WHERE IdJuridica=310  ");
            //strSQL.Append(" AND a.IsInativo=0 ");
            //strSQL.Append(" ) as tx1 ");
            //strSQL.Append(" order by NOME_CODIGO ");
            
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

        public DataSet Trazer_Lista_GHEs(int xEmpresa)
        {
            //StringBuilder strSQL = new StringBuilder();

            DataTable table = new DataTable("Result");
            table.Columns.Add("nID_FUNC", Type.GetType("System.Int32"));
            table.Columns.Add("tNO_FUNC", Type.GetType("System.String"));
            

            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);

            SqlCommand rCommand = new SqlCommand();

            rCommand.Parameters.Add("@xEmpresa", SqlDbType.NChar).Value = xEmpresa.ToString();

            rCommand.CommandText = "  SELECT tblFUNC.nID_FUNC, tNO_FUNC "
            + "  FROM tblFUNC  "
            + "  WHERE nId_Laud_tec in "
            + "  ( "
            + "    SELECT top 1 tblLAUDO_TEC.nID_LAUD_TEC  "
            + "    FROM tblLAUDO_TEC  (nolock)  "
            + "    WHERE nID_EMPR= @xEmpresa "
            + "    ORDER BY hDT_LAUDO DESC "
            + "  ) "
            + "  order by tNo_func ";


            //strSQL.Append("  SELECT tblFUNC.nID_FUNC, tNO_FUNC " ) ;
            //strSQL.Append("  FROM tblFUNC  " ) ;
            //strSQL.Append("  WHERE nId_Laud_tec in " ) ;
            //strSQL.Append("  ( " ) ;
            //strSQL.Append("    SELECT top 1 tblLAUDO_TEC.nID_LAUD_TEC " ) ;
            //strSQL.Append("    FROM tblLAUDO_TEC  " ) ;
            //strSQL.Append("    WHERE nID_EMPR= " + xEmpresa.ToString() + " " ) ;
            //strSQL.Append("    ORDER BY hDT_LAUDO DESC " ) ;
            //strSQL.Append("  ) " ) ;
            //strSQL.Append("  order by tNo_func ");


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


        public void Salvar_GHE(int xIdGHE, int xEmpregado, int xEmpresa)
        {

            using (SqlConnection cnn = new SqlConnection(Ilitera.Data.SQLServer.EntitySQLServer.GetConnection_Sied()))
            {

                string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection_Sied();

                Database objDB = new SqlDatabase(connString);

                var objCmd = objDB.GetStoredProcCommand("sp_Atualizar_GHE");

                objDB.AddInParameter(objCmd, "xIdGHE", System.Data.DbType.String, xIdGHE);
                objDB.AddInParameter(objCmd, "xEmpregado", System.Data.DbType.String, xEmpregado);
                objDB.AddInParameter(objCmd, "xEmpresa", System.Data.DbType.String, xEmpresa);

                var readerRetorno = objDB.ExecuteReader(objCmd);

                return;

            }

        }


        public void Salvar_GHE_Classif_Funcional(int xId_Empregado_Funcao, int xIdGHE, int xEmpregado, int xEmpresa, string xData_Laudo)
        {

            using (SqlConnection cnn = new SqlConnection(Ilitera.Data.SQLServer.EntitySQLServer.GetConnection_Sied()))
            {

                string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection_Sied();

                Database objDB = new SqlDatabase(connString);

                var objCmd = objDB.GetStoredProcCommand("sp_Salvar_GHE_Classif_Funcional");

                objDB.AddInParameter(objCmd, "xId_Empregado_Funcao", System.Data.DbType.String, xId_Empregado_Funcao);
                objDB.AddInParameter(objCmd, "xIdGHE", System.Data.DbType.String, xIdGHE);
                objDB.AddInParameter(objCmd, "xEmpregado", System.Data.DbType.String, xEmpregado);
                objDB.AddInParameter(objCmd, "xEmpresa", System.Data.DbType.String, xEmpresa);
                objDB.AddInParameter(objCmd, "xDataLaudo", System.Data.DbType.String, xData_Laudo  );

                var readerRetorno = objDB.ExecuteReader(objCmd);

                
                return;

            }

        }


        public void Salvar_Preposto(int xEmpresa, string xNome, string XNIT)
        {

            using (SqlConnection cnn = new SqlConnection(Ilitera.Data.SQLServer.EntitySQLServer.GetConnection()))
            {

                string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection();

                Database objDB = new SqlDatabase(connString);

                var objCmd = objDB.GetStoredProcCommand("sp_Salvar_Preposto");

                objDB.AddInParameter(objCmd, "xIdEmpresa", System.Data.DbType.Int32, xEmpresa);
                objDB.AddInParameter(objCmd, "xNome", System.Data.DbType.String, xNome);
                objDB.AddInParameter(objCmd, "xNIT", System.Data.DbType.String, XNIT);

                var readerRetorno = objDB.ExecuteReader(objCmd);

                return;

            }

        }


        public void Excluir_GHE_Classif_Funcional( int xId_Empregado_Funcao, int xEmpregado, int xEmpresa)
        {

            using (SqlConnection cnn = new SqlConnection(Ilitera.Data.SQLServer.EntitySQLServer.GetConnection_Sied()))
            {

                string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection_Sied();

                Database objDB = new SqlDatabase(connString);

                var objCmd = objDB.GetStoredProcCommand("sp_Excluir_GHE_Classif_Funcional");

                objDB.AddInParameter(objCmd, "xId_Empregado_Funcao", System.Data.DbType.String, xId_Empregado_Funcao);                
                objDB.AddInParameter(objCmd, "xEmpregado", System.Data.DbType.String, xEmpregado);
                objDB.AddInParameter(objCmd, "xEmpresa", System.Data.DbType.String, xEmpresa);

                var readerRetorno = objDB.ExecuteReader(objCmd);

                return;

            }

        }

        public void Excluir_GHE_Classif_Funcional_Laudo(int xId_Empregado_Funcao, int xEmpregado, int xEmpresa, string xData_Laudo)
        {

            using (SqlConnection cnn = new SqlConnection(Ilitera.Data.SQLServer.EntitySQLServer.GetConnection_Sied()))
            {

                string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection_Sied();

                Database objDB = new SqlDatabase(connString);

                var objCmd = objDB.GetStoredProcCommand("sp_Excluir_GHE_Classif_Funcional_Laudo");

                objDB.AddInParameter(objCmd, "xId_Empregado_Funcao", System.Data.DbType.String, xId_Empregado_Funcao);
                objDB.AddInParameter(objCmd, "xEmpregado", System.Data.DbType.String, xEmpregado);
                objDB.AddInParameter(objCmd, "xEmpresa", System.Data.DbType.String, xEmpresa);
                objDB.AddInParameter(objCmd, "xData_Laudo", System.Data.DbType.String, xData_Laudo);

                var readerRetorno = objDB.ExecuteReader(objCmd);

                return;

            }

        }


        public DataSet Trazer_Laudos_GHEs(int xAnoInicial, int xAnoFinal, int xIdEmpresa, string xFinalizados)
        {

            StringBuilder strSQL = new StringBuilder();


            DataTable table = new DataTable("Result");
            table.Columns.Add("Laudo", Type.GetType("System.String"));
            table.Columns.Add("GHE", Type.GetType("System.String"));
            table.Columns.Add("nId_Laud_Tec", Type.GetType("System.Int32"));
            table.Columns.Add("nId_Func", Type.GetType("System.Int32"));

            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);


            strSQL.Append("SELECT a.nID_LAUD_TEC,  convert( char(10),a.hDT_LAUDO, 103 ) as Laudo, b.nID_FUNC, b.tNO_FUNC as GHE " );
            strSQL.Append("from tblLAUDO_TEC (nolock)  as a left join tblFUNC (nolock)  as b ");
            strSQL.Append("on ( a.nID_LAUD_TEC = b.nID_LAUD_TEC ) " );
            strSQL.Append("left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.Pedido (nolock)  as c on ( a.nId_Pedido = c.IdPedido ) ");
            strSQL.Append("where a.nID_EMPR = " + xIdEmpresa.ToString() + " " );
            strSQL.Append("and datepart( yyyy, hdt_laudo) between " + xAnoInicial.ToString() + " and " + xAnoFinal.ToString() + " ");

            if (xFinalizados == "S")
                strSQL.Append(" and c.dataconclusao is not null ");

            strSQL.Append("order by a.hDT_LAUDO desc, a.nID_LAUD_TEC, b.tNO_FUNC ");



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



        public DataSet Trazer_Laudos_GHEs_Colaborador(Int32 xColaborador)
        {

            StringBuilder strSQL = new StringBuilder();


            DataTable table = new DataTable("Result");
            table.Columns.Add("Laudo", Type.GetType("System.String"));
            table.Columns.Add("GHE", Type.GetType("System.String"));
            table.Columns.Add("nId_Laudo_Tec", Type.GetType("System.Int32"));
            table.Columns.Add("nId_Func", Type.GetType("System.Int32"));

            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);

            SqlCommand rCommand = new SqlCommand();

            rCommand.Parameters.Add("@xColaborador", SqlDbType.NChar).Value = xColaborador.ToString();

            rCommand.CommandText = "SELECT a.nID_LAUD_TEC,  convert( char(10),a.hDT_LAUDO, 103 ) as Laudo, b.nID_FUNC, b.tNO_FUNC as GHE "
            + "from tblLAUDO_TEC (nolock)  as a left join tblFUNC (nolock)  as b "
            + "on ( a.nID_LAUD_TEC = b.nID_LAUD_TEC ) "
            + "left join tblFUNC_EMPREGADO (nolock)  as c on ( b.nID_FUNC = c.nID_FUNC ) "
            + "left join tblEMPREGADO_Funcao (nolock)  as d on ( c.nID_EMPREGADO_FUNCAO = d.nID_EMPREGADO_FUNCAO ) "
            + "where d.nID_EMPREGADO = @xColaborador "
            + "order by d.hDT_INICIO desc,a.hDT_LAUDO desc   ";

            //strSQL.Append("SELECT a.nID_LAUD_TEC,  convert( char(10),a.hDT_LAUDO, 103 ) as Laudo, b.nID_FUNC, b.tNO_FUNC as GHE ");
            //strSQL.Append("from tblLAUDO_TEC as a left join tblFUNC as b ");
            //strSQL.Append("on ( a.nID_LAUD_TEC = b.nID_LAUD_TEC ) ");
            //strSQL.Append("left join tblFUNC_EMPREGADO as c on ( b.nID_FUNC = c.nID_FUNC ) ");
            //strSQL.Append("left join tblEMPREGADO_Funcao as d on ( c.nID_EMPREGADO_FUNCAO = d.nID_EMPREGADO_FUNCAO ) ");
            //strSQL.Append("where d.nID_EMPREGADO = " + xColaborador.ToString() + " ");
            //strSQL.Append("order by a.hDT_LAUDO desc, d.hDT_INICIO desc  ");



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




        public DataSet Trazer_Laudos_GHEs_Salvos(int nId_Empregado_Funcao)
        {

            StringBuilder strSQL = new StringBuilder();


            DataTable table = new DataTable("Result");
            table.Columns.Add("Laudo", Type.GetType("System.String"));
            table.Columns.Add("GHE", Type.GetType("System.String"));
            table.Columns.Add("nId_Laudo_Tec", Type.GetType("System.Int32"));
            table.Columns.Add("nId_Func", Type.GetType("System.Int32"));

            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);

            SqlCommand rCommand = new SqlCommand();

            rCommand.Parameters.Add("@nId_Empregado_Funcao", SqlDbType.NChar).Value = nId_Empregado_Funcao.ToString();

            rCommand.CommandText =  "select a.nID_LAUD_TEC,  convert( char(10),d.hDT_LAUDO, 103 ) as Laudo, a.nID_FUNC, c.tNO_FUNC as GHE  "
            + "from tblFunc_Empregado (nolock)  as a left join tblEmpregado_Funcao (nolock)  as b "
            + "on ( a.nID_Empregado_Funcao = b.nID_Empregado_Funcao ) "
            + "left join tblFunc  (nolock) as c "
            + "on ( a.nId_Func = c.nId_Func ) "
            + "left join tblLaudo_Tec  (nolock) as d "
            + "on ( a.nId_Laud_Tec = d.nId_Laud_Tec ) "
            + "where a.nId_Empregado_FUncao = @nId_Empregado_Funcao " 
            + "and d.nID_LAUD_TEC is not null "
            + "order by d.hDT_LAUDO desc ";

            //strSQL.Append("select a.nID_LAUD_TEC,  convert( char(10),d.hDT_LAUDO, 103 ) as Laudo, a.nID_FUNC, c.tNO_FUNC as GHE  ");
            //strSQL.Append("from tblFunc_Empregado as a left join tblEmpregado_Funcao as b ");
            //strSQL.Append("on ( a.nID_Empregado_Funcao = b.nID_Empregado_Funcao ) ");
            //strSQL.Append("left join tblFunc as c ");
            //strSQL.Append("on ( a.nId_Func = c.nId_Func ) ");
            //strSQL.Append("left join tblLaudo_Tec as d ");
            //strSQL.Append("on ( a.nId_Laud_Tec = d.nId_Laud_Tec ) ");
            //strSQL.Append("where a.nId_Empregado_FUncao = " + nId_Empregado_Funcao.ToString() + " " );
            //strSQL.Append("and d.nID_LAUD_TEC is not null ");
            //strSQL.Append("order by d.hDT_LAUDO desc ");



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


        public DataSet Lista_Exames_Pendentes(string zData, string zCliente, string zColaborador, Boolean zTudo,  string zData2, string xTodasEmpresas, Int32 xIdPessoa)
        {

            StringBuilder strSQL = new StringBuilder();

            DataTable table = new DataTable("Result");
            table.Columns.Add("Empresa", Type.GetType("System.String"));
            table.Columns.Add("Colaborador", Type.GetType("System.String"));
            table.Columns.Add("Exame", Type.GetType("System.String"));
            table.Columns.Add("DtProxima", Type.GetType("System.String"));
            table.Columns.Add("GHE", Type.GetType("System.String"));
            table.Columns.Add("DtUltima", Type.GetType("System.String"));
            table.Columns.Add("Data_Ultimo_Espera", Type.GetType("System.String"));
            table.Columns.Add("Complementares", Type.GetType("System.String"));
            table.Columns.Add("UF", Type.GetType("System.String"));
            table.Columns.Add("CPF", Type.GetType("System.String"));


            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);






            strSQL.Append("select distinct tx1.NomeAbreviado as Empresa, tx1.tNo_Empg as Colaborador, tx1.tNo_CPF as CPF, Descricao as Exame, convert( char(10),tx7.DataVencimento, 103 ) as DtProxima, tx6.tNO_Func as GHE, convert(char(10),tx7.DataUltima,103 ) as DtUltima,  ");
            strSQL.Append("   case when tx10.Data_Exame is null then null    ");
            strSQL.Append("        when  tx10.Data_Exame is not null then convert( char(10),tx10.Data_Exame,103 )    ");
            strSQL.Append("      end as Data_Ultimo_Espera,   ");
            strSQL.Append("      case when tx50.Complementares is null then 'Não'  ");
            strSQL.Append("           when tx50.Complementares < 1 then 'Não'  ");
            strSQL.Append("           when tx50.Complementares is not null then 'Sim'  ");
            strSQL.Append("   end as Complementares, u.NomeAbreviado as UF    ");

            strSQL.Append("from ");
            strSQL.Append("( ");
            strSQL.Append("    select d.NomeAbreviado, a.DataUltima, a.DataVencimento, convert( char(10),a.DataProxima, 103 ) as DtProxima, a.DataProxima, c.Descricao, b.tNO_EMPG, b.tNO_CPF, a.IdExameDicionario, a.IdEmpregado, a.IdPcmso, b.nID_EMPR ");
            strSQL.Append("    from ExamePlanejamento as a (nolock)  ");
            strSQL.Append("    join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblempregado as b  (nolock) ");
            strSQL.Append("    on ( a.IdEmpregado = b.nID_EMPREGADO ) ");
            strSQL.Append("    join ExameDicionario as c  (nolock) ");
            strSQL.Append("    on ( a.IdExameDicionario = c.IdExameDicionario ) ");
            strSQL.Append("    join Pessoa as d  (nolock) ");
            strSQL.Append("    on ( b.nID_EMPR = d.IdPessoa  ");

            //if (zCliente!= "")  //testar para melhor desempenho
            //{
            ////    //strSQL.Append("   and d.IdPessoa in (  Select Idpessoa from pessoa where NomeAbreviado like '%" + zCliente.Trim() + "%'  ) ");
            //    strSQL.Append("   and d.IdPessoa in ( " + zCliente.ToString() + "  ) ");
            //}

            strSQL.Append("  ) ");

            //strSQL.Append("    and IdPcmso in " );
            strSQL.Append("    where c.nome not in ('Avaliação Clínica' ) ");
            strSQL.Append("    and IdPcmso in ");   // and ?? como se fizesse parte do join
            strSQL.Append("    ( ");
            strSQL.Append("      select distinct idpcmso ");
            strSQL.Append("      from pcmso as a  (nolock) left join documento as b  (nolock) ");
            strSQL.Append("      on ( a.iddocumento = b.iddocumento ) ");
            strSQL.Append("      where  convert( char(15),idcliente ) + ' ' + convert( char(10),datapcmso,103 ) ");
            strSQL.Append("      in ");
            strSQL.Append("      ( ");
            strSQL.Append("         select convert( char(15),idcliente ) + ' ' + convert( char(10),max(datapcmso),103 )  as xData ");
            strSQL.Append("         from pcmso as a  (nolock) left join documento as b  (nolock) ");
            strSQL.Append("         on ( a.iddocumento = b.iddocumento ) ");

            //if (xTodasEmpresas == "N")  //testar para melhor desempenho
            //{
                strSQL.Append("         where idcliente in (  " + zCliente + " ) ");
                strSQL.Append("         and idcliente in ");
                strSQL.Append("(  ");
                strSQL.Append("select idcliente from prestadorcliente  (nolock)  ");
                strSQL.Append("where idprestador in  ");
                strSQL.Append("(  ");
                strSQL.Append("  select idprestador from prestador  (nolock) where idjuridicapessoa in  ");
                strSQL.Append("  (  ");
                strSQL.Append("    select idjuridicapessoa from juridicapessoa  (nolock) where idpessoa = " + xIdPessoa.ToString() + "  ");
                strSQL.Append("  )  ");
                strSQL.Append(")  ");
                strSQL.Append(")  ");
            //}
            //else
            //{
            //    strSQL.Append("         where idcliente in ");
            //    strSQL.Append("(  ");
            //    strSQL.Append("select idcliente from prestadorcliente  (nolock)  ");
            //    strSQL.Append("where idprestador in  ");
            //    strSQL.Append("(  ");
            //    strSQL.Append("  select idprestador from prestador  (nolock) where idjuridicapessoa in  ");
            //    strSQL.Append("  (  ");
            //    strSQL.Append("    select idjuridicapessoa from juridicapessoa (nolock)  where idpessoa = " + xIdPessoa.ToString() + "  ");
            //    strSQL.Append("  )  ");
            //    strSQL.Append(")  ");
            //    strSQL.Append(")  ");
            //}


            strSQL.Append("         group by idcliente ");
            strSQL.Append("       ) ");
            strSQL.Append("     ) ");

            //if (zCliente.Trim() != "")
            //{
            //    strSQL.Append("and NomeAbreviado like '%" + zCliente.Trim() + "%' ");
            //}

            strSQL.Append(") ");
            strSQL.Append("as tx1 ");
            strSQL.Append("left join ExameBase as tx2  (nolock) ");
            strSQL.Append("on (  tx1.IdEmpregado = tx2.IdEmpregado and  tx1.IdExameDicionario = tx2.IdExameDicionario  ");
            strSQL.Append("      and DataExame between dateadd(dd,1,tx1.DataUltima) and tx1.DataProxima  ");

            if (zCliente != "")  //testar para melhor desempenho
            {
                strSQL.Append("      and tx1.nId_Empr in (  " + zCliente.ToString() + "  ) ");
            }

            strSQL.Append("    ) ");

            strSQL.Append("  join Pcmso as tx3  (nolock) on ( tx3.IdPCMSO = tx1.IdPCMSO )  ");
            strSQL.Append("  left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEMPREGADO_FUNCAO as tx4  (nolock) on ( tx1.IdEmpregado = tx4.nID_EMPREGADO ) ");  //and tx1.nID_EMPR = tx4.nID_EMPR ) ");
            strSQL.Append("  left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblFUNC_EMPREGADO as tx5  (nolock) on ( tx3.IdLaudoTecnico = tx5.nID_LAUD_TEC and tx4.nID_EMPREGADO_FUNCAO = tx5.nID_EMPREGADO_FUNCAO ) ");
            strSQL.Append("  left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblFUNC as tx6  (nolock) on ( tx5.nID_FUNC = tx6.nID_FUNC ) ");
            strSQL.Append("  left join qryExamePlanejamento as tx7  (nolock) on ( tx1.IdExameDicionario = tx7.IdExameDicionario and tx1.IdEmpregado = tx7.IdEmpregado and tx1.IdPCMSO = tx7.IdPCMSO  )  ");

            strSQL.Append("  left join endereco as R  (nolock) on tx1.nId_Empr = R.IdPessoa ");
            strSQL.Append("  left join municipio as S (nolock)  on R.IdMunicipio = S.IdMunicipio ");
            strSQL.Append("  left join UnidadeFederativa as T (nolock)  on s.IdUnidadeFederativa = t.IdUnidadeFederativa ");
            strSQL.Append("  left join LocalizacaoGeografica as U (nolock)  on T.IdLocalizacaoGeografica = U.IdLocalizacaoGeografica ");


            strSQL.Append("  left join (    SELECT IdEmpregado, convert( char(10),DataExame, 103 ) as Data_Exame, IdExameDicionario, DataExame  ");
            strSQL.Append("                 from ExameBase  (nolock)  ");
            strSQL.Append("                 where IdExameDicionario in ( 1,2,3,4,5 ) and IndResultado in ( 3 )  ");
            strSQL.Append("                 and convert( char(10),  DataExame, 103 ) + ' ' + convert( char(15),IDEMPREGADO ) in   (  ");
            strSQL.Append("                    select convert( char(10),  max(DataExame), 103 ) + ' ' + convert( char(15),IDEMPREGADO )  ");
            strSQL.Append("              	  from examebase   (nolock)  ");
            strSQL.Append("                    where IdExameDicionario in ( 1,2,3,4,5 ) and IndResultado in ( 3 )  ");
            strSQL.Append("              	   group by IdEmpregado      ");
            strSQL.Append("              	  )   ");
            strSQL.Append("              	) AS TX10   ");
            strSQL.Append("              ON ( tx1.IDEMPREGADO = TX10.IDEMPREGADO ) ");

            strSQL.Append("   left join (  ");
            strSQL.Append("      select idpcmso, idghe, count(*) as Complementares  ");
            strSQL.Append("      from PcmsoPlanejamento   (nolock)   ");
            strSQL.Append("      where idexamedicionario not in ( 1,2,3,4,5 )  ");
            strSQL.Append("      group by idpcmso, idghe   ");
            strSQL.Append("      ) as tx50   ");
            strSQL.Append("   ON ( tx1.IdPcmso = tx50.IdPcmso and tx50.idghe = tx6.nId_Func )   ");


            strSQL.Append("where tx1.NomeAbreviado is not null  ");

            strSQL.Append("and tx6.tNO_Func is not null ");


            strSQL.Append("and convert( char(14), tx1.IdEmpregado ) + ' ' + convert( char(10),tx4.hdt_inicio, 103 )  in  ");
            strSQL.Append("   ( ");
            strSQL.Append("      select convert( char(14), nId_Empregado ) + ' ' + convert( char(10), max(hdt_inicio), 103 )   ");
            strSQL.Append("      from " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblempregado_Funcao  (nolock)  ");

            //if (xTodasEmpresas=="N")  //testar para melhor desempenho
           // {
                strSQL.Append("         where nid_empr in (  " + zCliente + " ) ");
                //strSQL.Append("         and nid_empr in ");
                //strSQL.Append("(  ");
                //strSQL.Append("select idcliente from prestadorcliente  (nolock)  ");
                //strSQL.Append("where idprestador in  ");
                //strSQL.Append("(  ");
                //strSQL.Append("  select idprestador from prestador  (nolock) where idjuridicapessoa in  ");
                //strSQL.Append("  (  ");
                //strSQL.Append("    select idjuridicapessoa from juridicapessoa  (nolock) where idpessoa = " + xIdPessoa.ToString() + "  ");
                //strSQL.Append("  )  ");
                //strSQL.Append(")  ");
                //strSQL.Append(")  ");
            //}
            //else
            //{
            //    strSQL.Append("         where nid_empr in ");
            //    strSQL.Append("(  ");
            //    strSQL.Append("select idcliente from prestadorcliente  (nolock)  ");
            //    strSQL.Append("where idprestador in  ");
            //    strSQL.Append("(  ");
            //    strSQL.Append("  select idprestador from prestador  (nolock) where idjuridicapessoa in  ");
            //    strSQL.Append("  (  ");
            //    strSQL.Append("    select idjuridicapessoa from juridicapessoa  (nolock) where idpessoa = " + xIdPessoa.ToString() + "  ");
            //    strSQL.Append("  )  ");
            //    strSQL.Append(")  ");
            //    strSQL.Append(")  ");
            //}

            strSQL.Append("	     group by nId_Empregado ");
            strSQL.Append("   ) ");



            //if (zCliente.Trim() != "")
            //{
            //    strSQL.Append("and NomeAbreviado like '%" + zCliente.Trim() + "%' ");
            //}
            if (zColaborador.Trim() != "")
            {
                strSQL.Append("and tno_empg like '%" + zColaborador.Trim() + "%' ");
            }

            if (zTudo == false)
            {       
                strSQL.Append("and  ( tx7.DataVencimento is not null and tx7.DataVencimento between convert( smalldatetime,'" + zData.Substring(0, 10) + "', 103 ) and convert( smalldatetime,'" + zData2.Substring(0, 10) + "', 103 )  ) ");
            }

            strSQL.Append("order by tx1.NomeAbreviado, tNo_Empg     ");



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



        public void Salvar_Dados_Guia_Encaminhamento(int xIdEmpresa, int xIdEmpregado, string xTipo, string xExames, string xData, string xHora, string xClinica, Int32 xIdUsuario, string xAviso_Bloqueio, string xVencimento, string xUltimo_Exame, string xObs)
        {

            using (SqlConnection cnn = new SqlConnection(Ilitera.Data.SQLServer.EntitySQLServer.GetConnection()))
            {

                string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection();

                Database objDB = new SqlDatabase(connString);

                var objCmd = objDB.GetStoredProcCommand("sp_Inserir_Guia_Encaminhamento");

                objDB.AddInParameter(objCmd, "xIdEmpresa", System.Data.DbType.Int32, xIdEmpresa);
                objDB.AddInParameter(objCmd, "xIdEmpregado", System.Data.DbType.Int32, xIdEmpregado);
                objDB.AddInParameter(objCmd, "xTipo", System.Data.DbType.String, xTipo);
                objDB.AddInParameter(objCmd, "xExames", System.Data.DbType.String, xExames);
                objDB.AddInParameter(objCmd, "xData", System.Data.DbType.String, xData);
                objDB.AddInParameter(objCmd, "xHora", System.Data.DbType.String, xHora);
                objDB.AddInParameter(objCmd, "xClinica", System.Data.DbType.String, xClinica);
                objDB.AddInParameter(objCmd, "xIdUsuario", System.Data.DbType.Int32, xIdUsuario);
                objDB.AddInParameter(objCmd, "xAviso_Bloqueio", System.Data.DbType.String, xAviso_Bloqueio);
                objDB.AddInParameter(objCmd, "Vencimento", System.Data.DbType.String, xVencimento);
                objDB.AddInParameter(objCmd, "Ultimo_Exame", System.Data.DbType.String, xUltimo_Exame);
                objDB.AddInParameter(objCmd, "Obs", System.Data.DbType.String, xObs);

                var readerRetorno = objDB.ExecuteReader(objCmd);

                return;

            }

        }



        public void Guia_Demissao_Colaborador(int xIdEmpregado, string xData)
        {

            using (SqlConnection cnn = new SqlConnection(Ilitera.Data.SQLServer.EntitySQLServer.GetConnection_Sied()))
            {

                string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection_Sied();

                Database objDB = new SqlDatabase(connString);

                var objCmd = objDB.GetStoredProcCommand("sp_Guia_Demissional");
                                
                objDB.AddInParameter(objCmd, "xIdEmpregado", System.Data.DbType.Int32, xIdEmpregado);
                objDB.AddInParameter(objCmd, "xData", System.Data.DbType.String, xData);
                

                var readerRetorno = objDB.ExecuteReader(objCmd);

                return;

            }

        }



        public void Guia_Demissao_Colaborador_Obras(int xIdEmpregado, string xData)
        {

            using (SqlConnection cnn = new SqlConnection(Ilitera.Data.SQLServer.EntitySQLServer.GetConnection_Sied()))
            {

                string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection_Sied();

                Database objDB = new SqlDatabase(connString);

                var objCmd = objDB.GetStoredProcCommand("sp_Guia_Demissional_Obras");

                objDB.AddInParameter(objCmd, "xIdEmpregado", System.Data.DbType.Int32, xIdEmpregado);
                objDB.AddInParameter(objCmd, "xData", System.Data.DbType.String, xData);


                var readerRetorno = objDB.ExecuteReader(objCmd);

                return;

            }

        }





        private void Log_Web(   string Command,
                            int IdUsuario,                                                      
                            string ProcessoRealizado)
        {

            using (SqlConnection cnn = new SqlConnection(Ilitera.Data.SQLServer.EntitySQLServer.GetConnection()))
            {

                string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection();
                cnn.ConnectionString = connString;
                cnn.Open();
                              


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

                    SqlCommand cmd = new SqlCommand(strLog, cnn);

                    //cmd.CommandText = strLog;

                    //cmd.Connection = cnn;

                    cmd.ExecuteNonQuery(); //DESENVOLVIMENTO ILITERA - linha ao lado comentada (acessava tab de logs) 29/07/10
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                }

            }
        }


        public void Ajustar_Numeracao_Ordem_Servico(int xIdCliente)
        {

            using (SqlConnection cnn = new SqlConnection(Ilitera.Data.SQLServer.EntitySQLServer.GetConnection()))
            {

                string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection();

                Database objDB = new SqlDatabase(connString);

                var objCmd = objDB.GetStoredProcCommand("sp_Ajustar_Numeracao_Ordem_Servico");

                objDB.AddInParameter(objCmd, "xIdCliente", System.Data.DbType.String,xIdCliente);

                var readerRetorno = objDB.ExecuteReader(objCmd);

                return;

            }

        }


        public DataSet Retornar_CIPAS(int xEmpresa)
        {

            StringBuilder strSQL = new StringBuilder();


            DataTable table = new DataTable("Result");
            table.Columns.Add("IdCipa", Type.GetType("System.Int32"));
            table.Columns.Add("xComissaoEleitoral", Type.GetType("System.String"));

            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);


            SqlCommand rCommand = new SqlCommand();

            rCommand.Parameters.Add("@xEmpresa", SqlDbType.NChar).Value = xEmpresa.ToString();

            rCommand.CommandText = "select IdCipa, "
            + "case when Posse is null then convert( char(10), ComissaoEleitoral, 103 )  when Posse is not null then convert( char(10), ComissaoEleitoral, 103 ) + '*' end as xComissaoEleitoral, ComissaoEleitoral "
            + "from CIPA  (nolock) "
            + "where iddocumento in ( "
            + "  Select iddocumento from documento (nolock)  where IdCliente = @xEmpresa "
            + " ) "
            + "and ComissaoEleitoral is not null "
            + "order by ComissaoEleitoral";


            //case when a.Habitual_Eventual = 'H' then 'Habitual' when a.Habitual_Eventual = 'E' then 'Eventual' end as Frequencia

            //strSQL.Append("select IdCipa, ");
            //strSQL.Append("case when Posse is null then convert( char(10), ComissaoEleitoral, 103 )  when Posse is not null then convert( char(10), ComissaoEleitoral, 103 ) + '*' end as xComissaoEleitoral, ComissaoEleitoral ");
            //strSQL.Append("from CIPA ");
            //strSQL.Append("where iddocumento in ( ");
            //strSQL.Append("  Select iddocumento from documento where IdCliente = " + xEmpresa.ToString() + "  ");
            //strSQL.Append(" ) ");
            //strSQL.Append("and ComissaoEleitoral is not null ");
            //strSQL.Append("order by ComissaoEleitoral");


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


        public DataSet Retornar_Candidatos_CIPA(int xCipa)
        {

            StringBuilder strSQL = new StringBuilder();


            DataTable table = new DataTable("Result");
            table.Columns.Add("IdParticipantesEleicaoCipa", Type.GetType("System.Int32"));
            table.Columns.Add("tNo_Empg", Type.GetType("System.String"));

            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);


            strSQL.Append("SELECT a.IdParticipantesEleicaoCipa, b.tNo_Empg " ); 
            strSQL.Append("FROM ParticipantesEleicaoCipa as a left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEmpregado as b ");
            strSQL.Append("on ( a.IdEmpregado = b.nId_Empregado ) ");
            strSQL.Append("WHERE a.IdCipa= " + xCipa.ToString() + " " );
            strSQL.Append("order by tNo_Empg ");


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




        public DataSet Gerar_Lista_EPI(Int32 xIdEmpresa)
        {
            //string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection();
            StringBuilder strSQL = new StringBuilder();

            //DataSet m_ds;


            DataTable table = new DataTable("Result");
            table.Columns.Add("Colaborador", Type.GetType("System.String"));
            table.Columns.Add("GHE", Type.GetType("System.String"));
            table.Columns.Add("Qtde_Entregue", Type.GetType("System.Int32"));
            table.Columns.Add("EPI", Type.GetType("System.String"));
            table.Columns.Add("CA", Type.GetType("System.String"));
            table.Columns.Add("IdEPI", Type.GetType("System.Int32"));
            table.Columns.Add("IdEPIClienteCA", Type.GetType("System.Int32"));
            table.Columns.Add("IdCA", Type.GetType("System.Int32"));
            table.Columns.Add("nId_Empregado", Type.GetType("System.Int32"));            
            table.Columns.Add("Ultimo_Receb", Type.GetType("System.String"));
            table.Columns.Add("Intervalo", Type.GetType("System.String"));
            table.Columns.Add("Periodo", Type.GetType("System.String"));
            table.Columns.Add("Proximo_Recebimento", Type.GetType("System.String"));
            table.Columns.Add("Data_Recebimento", Type.GetType("System.String"));

            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);

            strSQL.Append(" select nId_Empregado, Colaborador, GHE, Qtde_Entregue, EPI, CA, IdEPI, IdEPIClienteCA, IdCA, convert( char(10), Ultimo_Receb2, 103 ) as Data_Recebimento, convert( char(10), Proximo_Recebimento2, 103 ) as Proximo_Recebimento, Intervalo, Periodo from ");
            strSQL.Append("( ");
            strSQL.Append("  select IdEPIClienteCA, tno_empg as Colaborador, tno_func as GHE, qtdEntregue as Qtde_Entregue, EPI, CA, IdEPI, IdCA, nId_Empregado,   ");
            strSQL.Append("  Ultimo_Recebimento as Ultimo_Receb2, ");
            strSQL.Append("     NumPeriodicidade as Intervalo,  ");
            strSQL.Append("     case when Periodicidade = 0 then 'Dias' ");
            strSQL.Append("          when Periodicidade = 1 then 'Mes(es)' ");
            strSQL.Append("          when Periodicidade = 2 then 'Ano(s)' ");
            strSQL.Append("    end as Periodo, ");
            strSQL.Append("     case when Periodicidade = 0 then DATEADD( dd, NumPeriodicidade,  ");
            strSQL.Append("  Ultimo_Recebimento ) ");
            strSQL.Append("          when Periodicidade = 1 then DATEADD( mm, NumPeriodicidade,  ");
            strSQL.Append("  Ultimo_Recebimento ) ");
            strSQL.Append("          when Periodicidade = 2 then DATEADD( yyyy, NumPeriodicidade,  ");
            strSQL.Append("  Ultimo_Recebimento ) ");
            strSQL.Append("    end as Proximo_Recebimento2 ");
            strSQL.Append("  from ");
            strSQL.Append("  ( ");
            strSQL.Append("    select distinct b.IdEPIClienteCA, h.nId_Empregado, h.tNo_Empg, r.tno_func, a.qtdentregue, b.idepi, j.Nome as EPI, m.NumeroCA  ");
            strSQL.Append("  as CA,  ");
            strSQL.Append("    b.idca, b.NumPeriodicidade, b.periodicidade,  ");
            strSQL.Append("    c.IdEmpregado,  ");
            strSQL.Append("    		case when max(c.DataRecebimento ) is null then convert( smalldatetime, convert( char(10),getdate(),103 ), 103 ) ");
            strSQL.Append("    		    when max(c.DataRecebimento ) is not null then max(c.DataRecebimento ) ");
            strSQL.Append("    		end as Ultimo_Recebimento,      ");
            strSQL.Append("    		case when max(c.DataRecebimento ) is null then convert( smalldatetime, convert( char(10),getdate(),103 ), 103 ) ");
            strSQL.Append("    		    when max(c.DataRecebimento ) is not null then max(c.DataRecebimento ) ");
            strSQL.Append("    		end as Data_Recebimento      ");
            strSQL.Append("    		from   " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblppra1 (nolock)  as e  ");
            strSQL.Append("    	    left join  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblepi_exte (nolock)  as d on e.nid_ppra = d.nid_ppra   ");
            strSQL.Append("    		left join  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tbllaudo_tec (nolock)  as k on ( e.nid_laud_tec = k.nId_laud_tec and    k.nid_empr = " + xIdEmpresa + " )  ");
            strSQL.Append("			left join  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblfunc_Empregado (nolock)  as f on e.nid_func =f.nid_func  ");
            strSQL.Append("    		left join  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblempregado_Funcao (nolock)  as g on ( f.nId_Empregado_Funcao = g.nId_Empregado_Funcao and g.nid_empr = " + xIdEmpresa + " )  ");
            strSQL.Append("    		left join  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblempregado as h (nolock)  on g.nId_Empregado = h.nId_Empregado  ");
            strSQL.Append("    		left join  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblFunc (nolock)  as r on ( e.nid_func = r.nid_func )   ");
            strSQL.Append("    		left join epiclienteca (nolock)  as b on  b.idEpi = d.nid_Epi  and b.idcliente = " + xIdEmpresa + " and b.IdCliente = g.nId_Empr  ");
		    strSQL.Append("    		left join epicaentregadetalhe (nolock)  as a on a.IdEPIClienteCA = b.IdEPIClienteCA   ");
		    strSQL.Append("    		left join epicaentrega (nolock)  as c on a.IdEPICAEntrega = c.IdEPICAEntrega       and c.IdEmpregado = g.nId_Empregado ");
		    strSQL.Append("    		left join epi (nolock)  as j on ( b.idepi = j.idepi ) ");
		    strSQL.Append("    		left join ca (nolock)  as m on ( b.idca = m.idca ) ");
            strSQL.Append("    where b.idcliente = " + xIdEmpresa + " and k.nid_empr = " + xIdEmpresa + " and h.tno_empg is not null and h.nid_empregado is not null  and c.idempregado is not null ");
            strSQL.Append("  and h.hDT_DEM is null ");
            strSQL.Append("      and k.nid_laud_tec in (  ");
            strSQL.Append("        select top 1 nid_laud_tec  from  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tbllaudo_tec  (nolock) where nid_empr = " + xIdEmpresa + " ");
            strSQL.Append("        and nid_pedido in ( select idpedido from pedido (nolock)  where indstatus = 2 and idcliente = " + xIdEmpresa + " " );
            strSQL.Append("	   ) ");
            strSQL.Append("      order by hdt_laudo desc ");
            strSQL.Append("      ) ");
            strSQL.Append("    group by b.IdEPIClienteCA, h.nId_Empregado, h.tNo_Empg, r.tno_func, a.qtdentregue, b.idepi, j.Nome, m.NumeroCA, b.idca,  ");
            strSQL.Append("   b.NumPeriodicidade, b.periodicidade, c.IdEmpregado ");
            strSQL.Append("  )  ");
            strSQL.Append("as tx99 ");
            strSQL.Append(") ");
            strSQL.Append("as tx100 ");
            strSQL.Append("where Proximo_Recebimento2 < dateadd( dd, 365, getdate())  ");
            strSQL.Append("order by Proximo_Recebimento2 , Colaborador, Epi, CA  ");





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



        public DataSet Gerar_Lista_EPI_Todos_CA(Int32 xIdEmpresa)
        {
            //string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection();
            StringBuilder strSQL = new StringBuilder();

            //DataSet m_ds;


            DataTable table = new DataTable("Result");
            table.Columns.Add("Colaborador", Type.GetType("System.String"));
            table.Columns.Add("GHE", Type.GetType("System.String"));
            table.Columns.Add("Qtde_Entregue", Type.GetType("System.Int32"));
            table.Columns.Add("EPI", Type.GetType("System.String"));
            table.Columns.Add("CA", Type.GetType("System.String"));
            table.Columns.Add("IdEPI", Type.GetType("System.Int32"));
            table.Columns.Add("IdEPIClienteCA", Type.GetType("System.Int32"));
            table.Columns.Add("IdCA", Type.GetType("System.Int32"));
            table.Columns.Add("nId_Empregado", Type.GetType("System.Int32"));
            table.Columns.Add("Ultimo_Receb", Type.GetType("System.String"));
            table.Columns.Add("Intervalo", Type.GetType("System.String"));
            table.Columns.Add("Periodo", Type.GetType("System.String"));
            table.Columns.Add("Proximo_Recebimento", Type.GetType("System.String"));
            table.Columns.Add("Data_Recebimento", Type.GetType("System.String"));

            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);


            strSQL.Append("select nid_empregado, colaborador, ghe, max(qtde_entregue) as qtde_entregue, epi, ca, idepi, idepiclienteca, idca, max(data_recebimento) as data_recebimento, max(proximo_recebimento) as proximo_recebimento, intervalo, periodo ");
            strSQL.Append("from ");
            strSQL.Append("( ");

            strSQL.Append(" select nId_Empregado, Colaborador, GHE, Qtde_Entregue, EPI, CA, IdEPI, IdEPIClienteCA, IdCA, convert( char(10), Ultimo_Receb2, 103 ) as Data_Recebimento, convert( char(10), Proximo_Recebimento2, 103 ) as Proximo_Recebimento, Intervalo, Periodo from ");
            strSQL.Append("( ");
            strSQL.Append("  select IdEPIClienteCA, tno_empg as Colaborador, tno_func as GHE, qtdEntregue as Qtde_Entregue, EPI, CA, IdEPI, IdCA, nId_Empregado,   ");
            strSQL.Append("  Ultimo_Recebimento as Ultimo_Receb2, ");
            strSQL.Append("     NumPeriodicidade as Intervalo,  ");
            strSQL.Append("     case when Periodicidade = 0 then 'Dias' ");
            strSQL.Append("          when Periodicidade = 1 then 'Mes(es)' ");
            strSQL.Append("          when Periodicidade = 2 then 'Ano(s)' ");
            strSQL.Append("    end as Periodo, ");
            strSQL.Append("     case when Periodicidade = 0 then DATEADD( dd, NumPeriodicidade,  ");
            strSQL.Append("  Ultimo_Recebimento ) ");
            strSQL.Append("          when Periodicidade = 1 then DATEADD( mm, NumPeriodicidade,  ");
            strSQL.Append("  Ultimo_Recebimento ) ");
            strSQL.Append("          when Periodicidade = 2 then DATEADD( yyyy, NumPeriodicidade,  ");
            strSQL.Append("  Ultimo_Recebimento ) ");
            strSQL.Append("    end as Proximo_Recebimento2 ");
            strSQL.Append("  from ");
            strSQL.Append("  ( ");
            strSQL.Append("    select distinct b.IdEPIClienteCA, h.nId_Empregado, h.tNo_Empg, r.tno_func, a.qtdentregue, b.idepi, j.Nome as EPI, m.NumeroCA  ");
            strSQL.Append("  as CA,  ");
            strSQL.Append("    b.idca, b.NumPeriodicidade, b.periodicidade,  ");
            strSQL.Append("    c.IdEmpregado,  ");
            strSQL.Append("    		case when max(c.DataRecebimento ) is null then convert( smalldatetime, convert( char(10),getdate(),103 ), 103 ) ");
            strSQL.Append("    		    when max(c.DataRecebimento ) is not null then max(c.DataRecebimento ) ");
            strSQL.Append("    		end as Ultimo_Recebimento,      ");
            strSQL.Append("    		case when max(c.DataRecebimento ) is null then convert( smalldatetime, convert( char(10),getdate(),103 ), 103 ) ");
            strSQL.Append("    		    when max(c.DataRecebimento ) is not null then max(c.DataRecebimento ) ");
            strSQL.Append("    		end as Data_Recebimento      ");
            strSQL.Append("    		from   " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblppra1 (nolock)  as e  ");
            strSQL.Append("    	    left join  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblepi_exte (nolock)  as d on e.nid_ppra = d.nid_ppra   ");
            strSQL.Append("    		left join  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tbllaudo_tec (nolock)  as k on ( e.nid_laud_tec = k.nId_laud_tec and    k.nid_empr = " + xIdEmpresa + " )  ");
            strSQL.Append("			left join  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblfunc_Empregado (nolock)  as f on e.nid_func =f.nid_func  ");
            strSQL.Append("    		left join  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblempregado_Funcao (nolock)  as g on ( f.nId_Empregado_Funcao = g.nId_Empregado_Funcao and g.nid_empr = " + xIdEmpresa + " )  ");
            strSQL.Append("    		left join  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblempregado (nolock)  as h on g.nId_Empregado = h.nId_Empregado  ");
            strSQL.Append("    		left join  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblFunc (nolock)  as r on ( e.nid_func = r.nid_func )   ");
            strSQL.Append("    		left join epiclienteca (nolock)  as b on  b.idEpi = d.nid_Epi  and b.idcliente = " + xIdEmpresa + " and b.IdCliente = g.nId_Empr  ");
            strSQL.Append("    		left join epicaentregadetalhe (nolock)  as a on a.IdEPIClienteCA = b.IdEPIClienteCA   ");
            strSQL.Append("    		left join epicaentrega (nolock)  as c on a.IdEPICAEntrega = c.IdEPICAEntrega       and c.IdEmpregado = g.nId_Empregado ");
            strSQL.Append("    		left join epi (nolock)  as j on ( b.idepi = j.idepi ) ");
            strSQL.Append("    		left join ca (nolock)  as m on ( b.idca = m.idca ) ");
            strSQL.Append("    where b.idcliente = " + xIdEmpresa + " and k.nid_empr = " + xIdEmpresa + " and h.tno_empg is not null and h.nid_empregado is not null  and c.idempregado is not null ");
            strSQL.Append("  and h.hDT_DEM is null ");
            strSQL.Append("      and k.nid_laud_tec in (  ");
            strSQL.Append("        select top 1 nid_laud_tec from  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tbllaudo_tec (nolock)  where nid_empr = " + xIdEmpresa + " ");
            strSQL.Append("        and nid_pedido in ( select idpedido from pedido (nolock)  where indstatus = 2 and idcliente = " + xIdEmpresa + " ");
            strSQL.Append("	   ) ");
            strSQL.Append("      order by hdt_laudo desc ");
            strSQL.Append("      ) ");
            strSQL.Append("    group by b.IdEPIClienteCA, h.nId_Empregado, h.tNo_Empg, r.tno_func, a.qtdentregue, b.idepi, j.Nome, m.NumeroCA, b.idca,  ");
            strSQL.Append("   b.NumPeriodicidade, b.periodicidade, c.IdEmpregado ");
            strSQL.Append("  )  ");
            strSQL.Append("as tx99 ");
            strSQL.Append(") ");
            strSQL.Append("as tx100 ");
            strSQL.Append("where Proximo_Recebimento2 < dateadd( dd, 365, getdate())  ");

            strSQL.Append("union ");



            strSQL.Append(" select nId_Empregado, Colaborador, GHE, Qtde_Entregue, EPI, CA, IdEPI, IdEPIClienteCA, IdCA, convert( char(10), Ultimo_Receb2, 103 ) as Data_Recebimento, '' as Proximo_Recebimento, Intervalo, Periodo from ");
            strSQL.Append("( ");
            strSQL.Append("  select IdEPIClienteCA, tno_empg as Colaborador, tno_func as GHE, 0 as Qtde_Entregue, EPI, CA, IdEPI, IdCA, nId_Empregado,  ");
            strSQL.Append("  Ultimo_Recebimento as Ultimo_Receb2, ");
            strSQL.Append("     NumPeriodicidade as Intervalo,  ");
            strSQL.Append("     case when Periodicidade = 0 then 'Dias' ");
            strSQL.Append("          when Periodicidade = 1 then 'Mes(es)' ");
            strSQL.Append("          when Periodicidade = 2 then 'Ano(s)' ");
            strSQL.Append("    end as Periodo, ");
            strSQL.Append("     case when Periodicidade = 0 then DATEADD( dd, NumPeriodicidade,  ");
            strSQL.Append("  Ultimo_Recebimento ) ");
            strSQL.Append("          when Periodicidade = 1 then DATEADD( mm, NumPeriodicidade,  ");
            strSQL.Append("  Ultimo_Recebimento ) ");
            strSQL.Append("          when Periodicidade = 2 then DATEADD( yyyy, NumPeriodicidade,  ");
            strSQL.Append("  Ultimo_Recebimento ) ");
            strSQL.Append("    end as Proximo_Recebimento2 ");
            strSQL.Append("  from ");
            strSQL.Append("  ( ");
            strSQL.Append("    select distinct b.IdEPIClienteCA, h.nId_Empregado, h.tNo_Empg, r.tno_func, 0 as qtdentregue, b.idepi, j.Nome as EPI, m.NumeroCA  ");
            strSQL.Append("  as CA,  ");
            strSQL.Append("    b.idca, b.NumPeriodicidade, b.periodicidade,  ");
            strSQL.Append("    ''  as Ultimo_Recebimento, '' as Data_Recebimento   ");
            strSQL.Append("    		from   " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblppra1 (nolock)  as e  ");
            strSQL.Append("    	    left join  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblepi_exte (nolock)  as d on e.nid_ppra = d.nid_ppra   ");
            strSQL.Append("    		left join  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tbllaudo_tec (nolock)  as k on ( e.nid_laud_tec = k.nId_laud_tec and    k.nid_empr = " + xIdEmpresa + " )  ");
            strSQL.Append("			left join  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblfunc_Empregado (nolock)  as f on e.nid_func =f.nid_func  ");
            strSQL.Append("    		left join  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblempregado_Funcao (nolock)  as g on ( f.nId_Empregado_Funcao = g.nId_Empregado_Funcao and g.nid_empr = " + xIdEmpresa + " )  ");
            strSQL.Append("    		left join  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblempregado (nolock)  as h on g.nId_Empregado = h.nId_Empregado  ");
            strSQL.Append("    		left join  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblFunc (nolock)  as r on ( e.nid_func = r.nid_func )   ");
            strSQL.Append("    		left join epiclienteca (nolock)  as b on  b.idEpi = d.nid_Epi  and b.idcliente = " + xIdEmpresa + " and b.IdCliente = g.nId_Empr  ");
            strSQL.Append("    		left join epi (nolock)  as j on ( b.idepi = j.idepi ) ");
            strSQL.Append("    		left join ca  (nolock) as m on ( b.idca = m.idca ) ");
            strSQL.Append("    where b.idcliente = " + xIdEmpresa + " and k.nid_empr = " + xIdEmpresa + " and h.tno_empg is not null and h.nid_empregado is not null ");  //  and c.idempregado is not null ");
            strSQL.Append("    and h.nid_empregado in ");
            strSQL.Append("   (select idempregado from epicaentrega where idEpiCAEntrega in (select IdEPICAEntrega from epicaentregadetalhe (nolock)  where idepiclienteca in (select idepiclienteca from epiclienteca where idcliente = " + xIdEmpresa.ToString() + "  and idepi = b.idepi ) ) ) ");
            strSQL.Append("  and h.hDT_DEM is null ");
            strSQL.Append("      and k.nid_laud_tec in (  ");
            strSQL.Append("        select top 1 nid_laud_tec from  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tbllaudo_tec (nolock)  where nid_empr = " + xIdEmpresa + " ");
            strSQL.Append("        and nid_pedido in ( select idpedido from pedido (nolock)  where indstatus = 2 and idcliente = " + xIdEmpresa + " ");
            strSQL.Append("	   ) ");
            strSQL.Append("      order by hdt_laudo desc ");
            strSQL.Append("      ) ");
            strSQL.Append("    group by b.IdEPIClienteCA, h.nId_Empregado, h.tNo_Empg, r.tno_func,  b.idepi, j.Nome, m.NumeroCA, b.idca,     b.NumPeriodicidade, b.periodicidade    ");
            strSQL.Append("  )  ");
            strSQL.Append("as tx99 ");
            strSQL.Append(") ");
            strSQL.Append("as tx100 ");
            strSQL.Append("where Proximo_Recebimento2 < dateadd( dd, 365, getdate())  ");

            //strSQL.Append("order by Proximo_Recebimento2 , Colaborador, Epi, CA  ");

            strSQL.Append(" ) as tsx88 ");
            strSQL.Append(" group by nid_empregado, colaborador, ghe, epi, ca, idepi, idepiclienteca, idca, intervalo, periodo ");
            strSQL.Append(" order by epi, colaborador ");




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




        public DataSet Gerar_Lista_EPI_Baseado_Entrega(Int32 xIdEmpresa)
        {
            //string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection();
            StringBuilder strSQL = new StringBuilder();

            //DataSet m_ds;


            DataTable table = new DataTable("Result");
            table.Columns.Add("Colaborador", Type.GetType("System.String"));
            table.Columns.Add("Qtde_Entregue", Type.GetType("System.Int32"));
            table.Columns.Add("EPI", Type.GetType("System.String"));
            table.Columns.Add("CA", Type.GetType("System.String"));
            table.Columns.Add("Intervalo", Type.GetType("System.String"));
            table.Columns.Add("Periodo", Type.GetType("System.String"));
            table.Columns.Add("Proximo_Recebimento", Type.GetType("System.String"));
            table.Columns.Add("Data_Recebimento", Type.GetType("System.String"));
            table.Columns.Add("nId_Empregado", Type.GetType("System.Int32"));

            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);

            strSQL.Append(" select nId_Empregado, Colaborador, EPI, qtdEntregue, convert( char(10), max( datarecebimento ), 103 ) as Data_Recebimento, numeroca, intervalo, periodo, convert( char(10), max( proximo_recebimento2 ), 103 ) as Proximo_Recebimento from ");
            strSQL.Append(" ( ");
            strSQL.Append("   select e.nId_Empregado, e.tno_empg as Colaborador, f.Descricao as EPI, a.qtdEntregue, b.DataRecebimento, d.NumeroCA,  ");
            strSQL.Append("      NumPeriodicidade as Intervalo, ");
            strSQL.Append("      case when Periodicidade = 0 then 'Dias' ");
            strSQL.Append("           when Periodicidade = 1 then 'Mes(es)'  ");
            strSQL.Append("           when Periodicidade = 2 then 'Ano(s)' ");
            strSQL.Append("     end as Periodo,  ");
            strSQL.Append("      case when Periodicidade = 0 then DATEADD( dd, NumPeriodicidade, b.DataRecebimento )  ");
            strSQL.Append("           when Periodicidade = 1 then DATEADD( mm, NumPeriodicidade, b.DataRecebimento )  ");
            strSQL.Append("           when Periodicidade = 2 then DATEADD( yyyy, NumPeriodicidade,  b.DataRecebimento )  ");
            strSQL.Append("     end as Proximo_Recebimento2  ");
            strSQL.Append("   from epicaentregadetalhe (nolock)  as a left join epicaentrega as b ");
            strSQL.Append("   on ( a.idepicaentrega = b.idepicaentrega ) ");
            strSQL.Append("   left join epiclienteca (nolock)  as c on ( a.idepiclienteca = c.idepiclienteca ) ");
            strSQL.Append("   join CA as d on ( c.idca = d.idca ) ");
            strSQL.Append("   join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblempregado (nolock)  as e on ( b.idempregado = e.nid_empregado ) ");
            strSQL.Append("   join epi (nolock)  as f on ( c.idepi = f.idepi ) ");
            strSQL.Append("   left join epi_alerta_lista (nolock)  as g on ( f.idepi = g.idepi and g.idpessoa = " + xIdEmpresa.ToString() + "  ) ");
            strSQL.Append("   where b.idempregado in ");
            strSQL.Append("   ( select nid_empregado from " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblempregado (nolock)  where nid_empr = " + xIdEmpresa.ToString() + " and hdt_dem is null) ");
            strSQL.Append("   and g.IdEpi is not null ");
            strSQL.Append(" ) as tx91 ");
            strSQL.Append(" where proximo_recebimento2 >= dateadd( mm, -3, getdate() ) ");
            strSQL.Append(" group by nId_Empregado, Colaborador, EPI, qtdEntregue,  numeroca, intervalo, periodo ");
            strSQL.Append(" order by max( proximo_recebimento2 ) , Colaborador  ");

          



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


        public DataSet Retornar_Repositorio(Int32 xIdEmpresa, string tipo, DateTime[] data, string descricao)
        {
            //string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection();
            //StringBuilder strSQL = new StringBuilder();

            //DataSet m_ds;

            DataTable table = new DataTable("Result");
            table.Columns.Add("IdEPI", Type.GetType("System.Int32"));
            table.Columns.Add("EPI", Type.GetType("System.String"));
            table.Columns.Add("Selecao", Type.GetType("System.String"));


            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);


            SqlCommand rCommand = new SqlCommand();

            rCommand.Parameters.Add("@xIdEmpresa", SqlDbType.NChar).Value = xIdEmpresa.ToString();

            rCommand.CommandText = " select IdRepositorio, IdCliente, TipoDocumento, DataHora, Descricao, Anexo, "
            + " case when TipoDocumento='V' then 'Vaso de Pressão' when TipoDocumento='C' then 'Caldeira' "
            + "      when TipoDocumento='S' then 'SPDA'            when TipoDocumento='E' then 'Laudo Elétrico' "
            + "      when TipoDocumento='P' then 'Periculosidade ' when TipoDocumento='T' then 'Treinamento' "
            + "      when TipoDocumento='M' then 'PCMSO          ' when TipoDocumento='A' then 'PPRA' "
            + "      when TipoDocumento='L' then 'LTCAT          ' when TipoDocumento='I' then 'Insalubridade' "
            + "      when TipoDocumento='O' then 'Outros'          when TipoDocumento='R' then 'Relat.Gerencial' "
            + "      when TipoDocumento='B' then 'ASO'             when TipoDocumento='Z' then 'PCA' "
            + "      when TipoDocumento='X' then 'PPR'             when TipoDocumento='1' then 'Laudo Ergonômico' "
            + "      when TipoDocumento='D' then 'PPP'             when TipoDocumento='N' then 'Relat.Anual' "
            + "      when TipoDocumento='Y' then 'Banco de Dados'  when TipoDocumento='H' then 'Lista Campanhas' "
            + "      when TipoDocumento='W' then 'PGR'             when TipoDocumento='5' then 'PGRTR'  "
            + "      when TipoDocumento='G' then 'Guia Encaminhamento'   when TipoDocumento='J' then 'COVID'    end as Tipo_Documento  "
            + " from Repositorio"
            + " where IdCliente = @xIdEmpresa ";
            if(tipo != "")
            {
                rCommand.Parameters.Add("@xTipoDocumento", SqlDbType.NChar).Value = tipo.ToString();
                rCommand.CommandText += " and TipoDocumento = @xTipoDocumento ";
            }else if (data != null && data.Length >= 2 && data[0] != null && data[1] != null)
            {
                //rCommand.Parameters.AddWithValue("@xDataInicio", data[0] );
                //rCommand.Parameters.AddWithValue("@xDataFim",  data[1]);
                rCommand.Parameters.Add("@xData", SqlDbType.DateTime).Value = data[0];
                rCommand.Parameters.Add("@xDataFim", SqlDbType.DateTime).Value = data[1];
                rCommand.CommandText += " and DataHora Between @xData and @xDataFim";
            }else if (descricao != "")
            {
                rCommand.Parameters.AddWithValue("@xDescricao", "%" + descricao.ToString() + "%");
                rCommand.CommandText += " and Descricao Like @xDescricao";
            }else
            {

            }
            rCommand.CommandText += " order by TipoDocumento, DataHora ";


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



        public DataSet Retornar_Repositorio_Tipo(Int32 xIdEmpresa, string xTipo)
        {
            //string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection();
            StringBuilder strSQL = new StringBuilder();

            //DataSet m_ds;

            DataTable table = new DataTable("Result");
            table.Columns.Add("IdRepositorio", Type.GetType("System.Int32"));
            table.Columns.Add("IdCliente", Type.GetType("System.Int32"));
            table.Columns.Add("TipoDocumento", Type.GetType("System.String"));
            table.Columns.Add("DataHora", Type.GetType("System.String"));
            table.Columns.Add("Descricao", Type.GetType("System.String"));
            table.Columns.Add("Anexo", Type.GetType("System.String"));


            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);


            //SqlCommand rCommand = new SqlCommand();

            //rCommand.Parameters.Add("@xIdEmpresa", SqlDbType.NChar).Value = xIdEmpresa.ToString();
            //rCommand.Parameters.Add("@xTipo", SqlDbType.NChar).Value = xTipo;

            strSQL.Append(" select IdRepositorio, IdCliente, TipoDocumento, DataHora, Descricao, Anexo ");
            strSQL.Append("   from Repositorio" );
            strSQL.Append("   where IdCliente = " + xIdEmpresa.ToString() + " and TipoDocumento in ( " + xTipo + " ) " );
            strSQL.Append("   order by TipoDocumento, DataHora " );


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




        public DataSet Gerar_Lista_Apenas_EPI_Baseado_Entrega(Int32 xIdEmpresa)
        {
            //string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection();
            StringBuilder strSQL = new StringBuilder();

            //DataSet m_ds;


            DataTable table = new DataTable("Result");
            table.Columns.Add("IdEPI", Type.GetType("System.Int32"));
            table.Columns.Add("EPI", Type.GetType("System.String"));
            table.Columns.Add("Selecao", Type.GetType("System.String"));


            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);

            strSQL.Append(" select distinct IdEPI, EPI, Selecao " ); 
            strSQL.Append(" from ");
            strSQL.Append(" ( ");
            strSQL.Append("   select f.IdEPI, e.nId_Empregado, e.tno_empg as Colaborador, f.Descricao as EPI, a.qtdEntregue, b.DataRecebimento, d.NumeroCA,  ");
            strSQL.Append("      NumPeriodicidade as Intervalo, ");
            strSQL.Append("      case when Periodicidade = 0 then 'Dias' ");
            strSQL.Append("           when Periodicidade = 1 then 'Mes(es)'  ");
            strSQL.Append("           when Periodicidade = 2 then 'Ano(s)' ");
            strSQL.Append("     end as Periodo,  ");
            strSQL.Append("      case when Periodicidade = 0 then DATEADD( dd, NumPeriodicidade, b.DataRecebimento )  ");
            strSQL.Append("           when Periodicidade = 1 then DATEADD( mm, NumPeriodicidade, b.DataRecebimento )  ");
            strSQL.Append("           when Periodicidade = 2 then DATEADD( yyyy, NumPeriodicidade,  b.DataRecebimento )  ");
            strSQL.Append("     end as Proximo_Recebimento2, case when g.IdEPI is null then ' '  when g.IdEPI is not null then 'X' end as Selecao   ");
            strSQL.Append("   from epicaentregadetalhe (nolock)  as a left join epicaentrega (nolock)  as b ");
            strSQL.Append("   on ( a.idepicaentrega = b.idepicaentrega ) ");
            strSQL.Append("   left join epiclienteca (nolock)  as c on ( a.idepiclienteca = c.idepiclienteca ) ");
            strSQL.Append("   join CA (nolock)  as d on ( c.idca = d.idca ) ");
            strSQL.Append("   join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblempregado (nolock)  as e on ( b.idempregado = e.nid_empregado ) ");
            strSQL.Append("   join epi (nolock)  as f on ( c.idepi = f.idepi ) ");
            strSQL.Append("   left join epi_alerta_lista (nolock)  as g on ( f.idepi = g.idepi and g.idpessoa = " + xIdEmpresa.ToString() + "  ) ");
            strSQL.Append("   where b.idempregado in ");
            strSQL.Append("   ( select nid_empregado from " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblempregado (nolock)  where nid_empr = " + xIdEmpresa.ToString() + " and hdt_dem is null) ");            
            strSQL.Append(" ) as tx91 ");
            strSQL.Append(" where proximo_recebimento2 >= dateadd( mm, -3, getdate() ) ");
            strSQL.Append(" order by EPI  ");





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



        public DataSet Carregar_EPI_Alerta(Int32 xIdEmpresa)
        {
            //string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection();
            StringBuilder strSQL = new StringBuilder();

            //DataSet m_ds;


            DataTable table = new DataTable("Result");
            table.Columns.Add("Dias", Type.GetType("System.Int32"));
            table.Columns.Add("email1", Type.GetType("System.String"));
            table.Columns.Add("email2", Type.GetType("System.String"));
            table.Columns.Add("Somente_EPI", Type.GetType("System.Boolean"));


            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);


            SqlCommand rCommand = new SqlCommand();

            rCommand.Parameters.Add("@xIdEmpresa", SqlDbType.NChar).Value = xIdEmpresa.ToString();

            rCommand.CommandText = " select Dias, email1, email2, Somente_EPI "
            + " from EPI_Alerta "
            + " where idpessoa = @xIdEmpresa ";

            //strSQL.Append(" select Dias, email1, email2 ");
            //strSQL.Append(" from EPI_Alerta ");
            //strSQL.Append(" where idpessoa = " + xIdEmpresa.ToString() + " ");


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


        public DataSet Carregar_Uniforme_Alerta(Int32 xIdEmpresa)
        {
            //string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection();
            StringBuilder strSQL = new StringBuilder();

            //DataSet m_ds;


            DataTable table = new DataTable("Result");
            table.Columns.Add("Dias", Type.GetType("System.Int32"));
            table.Columns.Add("email1", Type.GetType("System.String"));
            table.Columns.Add("email2", Type.GetType("System.String"));            


            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);


            SqlCommand rCommand = new SqlCommand();

            rCommand.Parameters.Add("@xIdEmpresa", SqlDbType.NChar).Value = xIdEmpresa.ToString();

            rCommand.CommandText = " select Dias, email1, email2 "
            + " from Uniforme_Alerta "
            + " where idpessoa = @xIdEmpresa ";

           
            using (SqlConnection cnn = new SqlConnection(Ilitera.Data.SQLServer.EntitySQLServer.GetConnection()))
            {
                cnn.Open();

                rCommand.Connection = cnn;
                SqlDataAdapter da;

                da = new SqlDataAdapter(rCommand);
                da.Fill(m_ds, "Result");

              
                cnn.Close();

                da.Dispose();
            }

            return m_ds;

        }



        public void Inserir_EPI_Alerta_Lista(Int32 xIdPessoa, Int32 xIdEPI)
        {

            using (SqlConnection cnn = new SqlConnection(Ilitera.Data.SQLServer.EntitySQLServer.GetConnection()))
            {

                string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection();

                Database objDB = new SqlDatabase(connString);

                var objCmd = objDB.GetStoredProcCommand("sp_Inserir_EPI_Alerta_Lista");

                objDB.AddInParameter(objCmd, "xIdPessoa", System.Data.DbType.Int32, xIdPessoa);
                objDB.AddInParameter(objCmd, "xIdEPI", System.Data.DbType.Int32, xIdEPI);

                var readerRetorno = objDB.ExecuteReader(objCmd);

                return;

            }

        }


        public void Inserir_EPI_Alerta(Int32 xIdPessoa, string xEmail1, string xEmail2, Int32 xDias, int xEPI_Apenas)
        {

            using (SqlConnection cnn = new SqlConnection(Ilitera.Data.SQLServer.EntitySQLServer.GetConnection()))
            {

                string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection();

                Database objDB = new SqlDatabase(connString);

                var objCmd = objDB.GetStoredProcCommand("sp_Inserir_EPI_Alerta");

                objDB.AddInParameter(objCmd, "xIdPessoa", System.Data.DbType.Int32, xIdPessoa);
                objDB.AddInParameter(objCmd, "xEmail1", System.Data.DbType.String, xEmail1);
                objDB.AddInParameter(objCmd, "xEmail2", System.Data.DbType.String, xEmail2);
                objDB.AddInParameter(objCmd, "xDias", System.Data.DbType.Int32, xDias);
                objDB.AddInParameter(objCmd, "xEPI_Apenas", System.Data.DbType.Int16, xEPI_Apenas);

                var readerRetorno = objDB.ExecuteReader(objCmd);

                return;

            }

        }


        public void Inserir_Uniforme_Alerta(Int32 xIdPessoa, string xEmail1, string xEmail2, Int32 xDias)
        {

            using (SqlConnection cnn = new SqlConnection(Ilitera.Data.SQLServer.EntitySQLServer.GetConnection()))
            {

                string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection();

                Database objDB = new SqlDatabase(connString);

                var objCmd = objDB.GetStoredProcCommand("sp_Inserir_Uniforme_Alerta");

                objDB.AddInParameter(objCmd, "xIdPessoa", System.Data.DbType.Int32, xIdPessoa);
                objDB.AddInParameter(objCmd, "xEmail1", System.Data.DbType.String, xEmail1);
                objDB.AddInParameter(objCmd, "xEmail2", System.Data.DbType.String, xEmail2);
                objDB.AddInParameter(objCmd, "xDias", System.Data.DbType.Int32, xDias);                

                var readerRetorno = objDB.ExecuteReader(objCmd);

                return;

            }

        }


        public void Excluir_EPI_Alerta(int xIdPessoa)
        {

            using (SqlConnection cnn = new SqlConnection(Ilitera.Data.SQLServer.EntitySQLServer.GetConnection()))
            {

                string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection();

                Database objDB = new SqlDatabase(connString);

                var objCmd = objDB.GetStoredProcCommand("sp_Excluir_EPI_Alerta");

                objDB.AddInParameter(objCmd, "xIdPessoa", System.Data.DbType.Int32, xIdPessoa);

                var readerRetorno = objDB.ExecuteReader(objCmd);

                return;

            }

        }


        public DataSet Trazer_Medicos_Coordenadores(Int32 zIdCliente, bool zTodos)
        {

            StringBuilder strSQL = new StringBuilder();


            DataTable table = new DataTable("Result");
            table.Columns.Add("Medico", Type.GetType("System.String"));
            table.Columns.Add("CRM", Type.GetType("System.String"));
            table.Columns.Add("Estado", Type.GetType("System.String"));
            table.Columns.Add("Audiometria", Type.GetType("System.String"));


            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);


            strSQL.Append("SELECT     distinct Pessoa.NomeAbreviado as Medico, Prestador.numero as CRM, case when Prestador.UF is null then '' else Prestador.UF end as Estado, ");
            strSQL.Append(" case when tx45.IdMedico is null then 0 else 1 end as Audiometria ");
            strSQL.Append("FROM       dbo.JuridicaPessoa (nolock) ");
            strSQL.Append("           INNER JOIN dbo.Prestador (nolock) ON dbo.JuridicaPessoa.IdJuridicaPessoa =  dbo.Prestador.IdJuridicaPessoa ");
            strSQL.Append("           INNER JOIN   dbo.Medico (nolock) ON dbo.Prestador.IdPrestador = dbo.Medico.IdPrestador ");
            strSQL.Append("           INNER JOIN dbo.Pessoa (nolock) ON dbo.JuridicaPessoa.IdPessoa = dbo.Pessoa.IdPessoa ");
            strSQL.Append("           LEFT JOIN (     select distinct IdMedico from    Examebase (nolock)   where idexamedicionario = 6 and indresultado in ( 1,2 )   ) as tx45 on ( medico.idmedico = tx45.idmedico ) ");
            strSQL.Append("					  where prestador.isinativo = 0  and Pessoa.NomeAbreviado is not null ");
            strSQL.Append("					  and ");
            strSQL.Append("					  juridicapessoa.idjuridica  in ");
            strSQL.Append("					  ( ");
            strSQL.Append("					    SELECT IdClinica FROM ClinicaCliente (nolock) ");
            strSQL.Append("						WHERE IdCliente= " + zIdCliente.ToString() + "  ");
            strSQL.Append("						AND IdClinica IN (SELECT  ");
            strSQL.Append("IdJuridica FROM Juridica WHERE IdJuridicaPapel=8 AND IdPessoa IN (SELECT IdPessoa  ");
            strSQL.Append("FROM Pessoa )) ");  // WHERE IsInativo=0))   " );



            if (zTodos == false)
            {
                strSQL.Append("						and IdClinica in ( select  ");
                strSQL.Append("IdJuridica from ExameBase where IdEmpregado in  ");
                strSQL.Append("						   ( select nId_Empregado from  ");
                strSQL.Append(" " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.TblEmpregado_Funcao where nId_Empr = " + zIdCliente.ToString() + "  ) ");
                strSQL.Append("						   and DataExame >= dateadd(  ");
                strSQL.Append("year, -1, getdate()) ");
                strSQL.Append("						   ) ");
            }

            strSQL.Append("					   ) ");
            strSQL.Append("order by NomeAbreviado ");



            //m_ds = new DataSet("Result");

            using (SqlConnection cnn = new SqlConnection(Ilitera.Data.SQLServer.EntitySQLServer.GetConnection()))
            {
                SqlDataAdapter da;
                cnn.Open();


                da = new SqlDataAdapter(strSQL.ToString(), cnn);
                da.SelectCommand.CommandTimeout = 2400;
                da.Fill(m_ds, "Result");

                cnn.Close();

                da.Dispose();
            }

            return m_ds;

        }


        public DataSet DataEntrega_EPICA(Int32 xIdCliente, Int32 xIdEpi, Int32 xIdEmpregado, Int32 xIdCA)
        {
            //string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection();
            StringBuilder strSQL = new StringBuilder();

            //DataSet m_ds;


            DataTable table = new DataTable("Result");
            table.Columns.Add("DtRecebimento", Type.GetType("System.DateTime"));


            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);

            SqlCommand rCommand = new SqlCommand();

            rCommand.Parameters.Add("@xIdCliente", SqlDbType.NChar).Value = xIdCliente.ToString();
            rCommand.Parameters.Add("@xIdEpi", SqlDbType.NChar).Value = xIdEpi.ToString();
            rCommand.Parameters.Add("@xIdEmpregado", SqlDbType.NChar).Value = xIdEmpregado.ToString();
            rCommand.Parameters.Add("@xIdCA", SqlDbType.NChar).Value = xIdCA.ToString();

            rCommand.CommandText = " SELECT min(DataRecebimento) as DtRecebimento "
            + " FROM EPIClienteCA (nolock)  as a left join epicaentregadetalhe (nolock)  as b "
            + " on(a.idepiclienteca = b.idepiclienteca) "
            + " left join EPICAEntrega (nolock)  as c "
            + " on(b.idepicaentrega = c.IdEPICAEntrega) "
            + " WHERE IdEPI = @xIdEpi "
            + " AND IdCliente = @xIdCliente "
            + " and IdEmpregado = @xIdEmpregado and IdCA = @xIdCA "
            + " and datarecebimento is not null ";

            //strSQL.Append(" SELECT min( DataRecebimento ) as DtRecebimento ");
            //strSQL.Append(" FROM EPIClienteCA as a left join epicaentregadetalhe as b ");
            //strSQL.Append(" on(a.idepiclienteca = b.idepiclienteca) ");
            //strSQL.Append(" left join EPICAEntrega as c ");
            //strSQL.Append(" on(b.idepicaentrega = c.IdEPICAEntrega) ");
            //strSQL.Append(" WHERE IdEPI = " + xIdEpi.ToString() + " ");
            //strSQL.Append(" AND IdCliente = " + xIdCliente.ToString() + " ");
            //strSQL.Append(" and IdEmpregado = " + xIdEmpregado.ToString() + " and IdCA = " + xIdCA.ToString());
            //strSQL.Append(" and datarecebimento is not null ");


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


        public DataSet Retornar_Laudos(Int32 xCliente)
        {

            StringBuilder strSQL = new StringBuilder();


            DataTable table = new DataTable("Result");
            table.Columns.Add("Laudo", Type.GetType("System.String"));
            table.Columns.Add("DataLaudo", Type.GetType("System.String"));            

            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);


            strSQL.Append(" select * from ");
            strSQL.Append(" ( ");
            strSQL.Append("   select 'PCMSO' as Laudo, convert(char(10), DataPCMSO, 103) as DataLaudo ");
            strSQL.Append("   from " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.pcmso (nolock)  as a ");
            strSQL.Append("   left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.documento (nolock)  as b on(a.iddocumento = b.iddocumento) ");
            strSQL.Append("   left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.pedido (nolock)  as c on(b.idpedido = c.idpedido) ");
            strSQL.Append("   where c.dataConclusao is not null and DataCancelamento is null ");
            strSQL.Append("   and b.IdCliente = " + xCliente.ToString() + " ");
            strSQL.Append("   union ");
            strSQL.Append("   select 'PPRA' as Laudo, convert(char(10), a.hDT_LAUDO, 103) as DataLaudo ");
            strSQL.Append(" from tblLAUDO_TEC (nolock)  as a ");
            strSQL.Append("   left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.pedido (nolock)  as c on(a.nID_PEDIDO = c.idpedido) ");
            strSQL.Append("   where c.dataConclusao is not null and DataCancelamento is null ");
            strSQL.Append("   and a.nId_empr = " + xCliente.ToString() + " ");
            strSQL.Append(" ) as tx90 ");
            strSQL.Append(" order by Laudo, DataLaudo ");



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




        public DataSet Retornar_Setores(Int32 xCliente)
        {

            StringBuilder strSQL = new StringBuilder();


            DataTable table = new DataTable("Result");
            table.Columns.Add("Setor", Type.GetType("System.String"));
            

            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);


            strSQL.Append(" select tNo_Str_Empr as Setor from tblSetor (nolock)  where nId_Empr = " + xCliente.ToString() + " order by tno_str_empr ");


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


        public DataSet Retornar_Cargos(Int32 xCliente)
        {

            StringBuilder strSQL = new StringBuilder();


            DataTable table = new DataTable("Result");
            table.Columns.Add("Cargo", Type.GetType("System.String"));


            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);


            strSQL.Append(" select NomeFuncao as Cargo from Funcao (nolock)  where Idcliente = " + xCliente.ToString() + " order by NomeFuncao ");


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


        public DataSet Retornar_Anexos_Erg(int xLaudo, string xTipo)
        {
            //por exemplo, xtipo = PDF,  exibirá apenas PDFs,  se for *,  exibirá todos ,  se for -PDF, exibirá todos, exceto PDFs
            StringBuilder strSQL = new StringBuilder();


            DataTable table = new DataTable("Result");
            table.Columns.Add("Arquivo", Type.GetType("System.String"));
            table.Columns.Add("Descricao", Type.GetType("System.String"));

            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);



            if (xTipo == "*")
            {
                strSQL.Append("  SELECT Arquivo, Descricao from tblErg_Anexos where nId_Laud_Tec =  " + xLaudo.ToString() + " order by Arquivo ");
            }
            else if (xTipo == "-PDF")
            {
                strSQL.Append("  SELECT Arquivo, Descricao from tblErg_Anexos where nId_Laud_Tec =  " + xLaudo.ToString() + " and Arquivo not like '%.PDF'  order by Arquivo ");
            }
            else
            {
                strSQL.Append("  SELECT Arquivo, Descricao from tblErg_Anexos where nId_Laud_Tec =  " + xLaudo.ToString() + " and Arquivo like '%." + xTipo + "'  order by Arquivo ");
            }



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


        public Int32 Trazer_IdPCMSO_Anterior(Int32 nIdPCMSO)
        {

            StringBuilder strSQL = new StringBuilder();


            DataTable table = new DataTable("Result");
            table.Columns.Add("IdPCMSO", Type.GetType("System.Int32"));

            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);


            strSQL.Append(" select top 1 IdPCMSO from pcmso (nolock)  ");
            strSQL.Append(" where iddocumento in ( ");
            strSQL.Append("  select iddocumento from documento (nolock)  where idcliente in ( ");
            strSQL.Append("     select idcliente from documento (nolock)  where iddocumento in (select iddocumento from pcmso where idpcmso = " + nIdPCMSO.ToString() + " ) ");
            strSQL.Append("  ) ");
            strSQL.Append(" ) ");
            strSQL.Append(" and datapcmso < (select datapcmso from pcmso (nolock)  where idpcmso = " + nIdPCMSO.ToString() + " ) ");
            strSQL.Append(" order by datapcmso desc ");

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
                return (int)0;
            else
                return System.Convert.ToInt32(m_ds.Tables[0].Rows[0][0].ToString());

        }


        public DataSet Trazer_Acidentes_Analitico_PCMSO(Int32 zIdPCMSO)
        {

            StringBuilder strSQL = new StringBuilder();



            DataTable table = new DataTable("Result");
            table.Columns.Add("GHE", Type.GetType("System.String"));
            table.Columns.Add("IdGHE", Type.GetType("System.String"));
            table.Columns.Add("DataEvento", Type.GetType("System.String"));
            table.Columns.Add("TipoEvento", Type.GetType("System.String"));
            table.Columns.Add("CID", Type.GetType("System.String"));

            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);


            strSQL.Append("select * from ");
            strSQL.Append(" ( ");

            strSQL.Append(" select distinct i.tNo_Func as GHE,  convert( char(10), DataAcidente, 103 ) as DataEvento,  ");
            strSQL.Append(" case when indtipoacidente = 1 then 'Típico' ");
            strSQL.Append("      when indtipoacidente = 2 then 'Doença' ");
            strSQL.Append("      when indtipoacidente = 3 then 'Trajeto' end as TipoEvento, ");
            strSQL.Append(" ltrim(rtrim(isnull(b.CodigoCID, ' ') + ' ' + isnull(c.CodigoCID, '') + ' ' + isnull(d.CodigoCID, '') + ' ' + isnull(e.CodigoCID, ''))) as CID ");
            strSQL.Append(" from acidente (nolock)  as a ");
            strSQL.Append(" left join cid (nolock)  as b on(a.idcid = b.idcid) ");
            strSQL.Append(" left join cid (nolock)  as c on(a.idcid2 = c.idcid) ");
            strSQL.Append(" left join cid (nolock)  as d on(a.idcid3 = d.idcid) ");
            strSQL.Append(" left join cid (nolock)  as e on(a.idcid4 = e.idcid) ");
            strSQL.Append(" left join  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblempregado (nolock)  as f on(a.idEmpregado = f.nID_EMPREGADO) ");
            strSQL.Append(" left join  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEmpregado_Funcao (nolock)  as g on(f.nid_Empregado = g.nid_Empregado) ");
            strSQL.Append(" left join  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblFunc_Empregado (nolock)  as h on(g.nID_EMPREGADO_FUNCAO = h.nID_EMPREGADO_FUNCAO) ");
            strSQL.Append(" left join  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblFunc (nolock)  as i on ( h.nID_FUNC = i.nID_FUNC ) ");

            strSQL.Append(" where dataacidente between(select datapcmso from pcmso (nolock)  where idpcmso = " + zIdPCMSO.ToString() + " ) ");
            strSQL.Append("                       and(select dateadd(yyyy, +1, datapcmso) from pcmso (nolock)  where idpcmso = " + zIdPCMSO.ToString() + " ) ");
            strSQL.Append(" and idempregado in ( ");
            strSQL.Append("             select nid_Empregado from " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEmpregado (nolock)  where nId_Empr in ");
            strSQL.Append("    ( select idCliente from documento where iddocumento in (select iddocumento from pcmso (nolock)  where idpcmso = " + zIdPCMSO.ToString() + " ) ) ");
            strSQL.Append("   ) ");

            strSQL.Append(" union all ");

            strSQL.Append("  select tno_func as GHE, '-' as DataEvento,  '-' as TipoEvento, '-' as Cid ");
            strSQL.Append("  from PcmsoGHE as a ");
            strSQL.Append("  join   " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblFUNC (nolock)  as b on (a.IdGhe = b.nID_FUNC) ");
            strSQL.Append("  where IdPcmso = " + zIdPCMSO.ToString() + " ");
            strSQL.Append("  and tNO_FUNC not in ");
            strSQL.Append("  ( ");


            strSQL.Append(" select distinct i.tNo_Func as GHE ");
            strSQL.Append(" from acidente (nolock)  as a ");
            strSQL.Append(" left join cid (nolock)  as b on(a.idcid = b.idcid) ");
            strSQL.Append(" left join cid (nolock)  as c on(a.idcid2 = c.idcid) ");
            strSQL.Append(" left join cid (nolock)  as d on(a.idcid3 = d.idcid) ");
            strSQL.Append(" left join cid (nolock)  as e on(a.idcid4 = e.idcid) ");
            strSQL.Append(" left join  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblempregado (nolock)  as f on(a.idEmpregado = f.nID_EMPREGADO) ");
            strSQL.Append(" left join  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEmpregado_Funcao (nolock)  as g on(f.nid_Empregado = g.nid_Empregado) ");
            strSQL.Append(" left join  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblFunc_Empregado (nolock)  as h on(g.nID_EMPREGADO_FUNCAO = h.nID_EMPREGADO_FUNCAO) ");
            strSQL.Append(" left join  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblFunc (nolock)  as i on ( h.nID_FUNC = i.nID_FUNC ) ");

            strSQL.Append(" where dataacidente between(select datapcmso from pcmso (nolock)  where idpcmso = " + zIdPCMSO.ToString() + " ) ");
            strSQL.Append("                       and(select dateadd(yyyy, +1, datapcmso) from pcmso (nolock)  where idpcmso = " + zIdPCMSO.ToString() + " ) ");
            strSQL.Append(" and idempregado in ( ");
            strSQL.Append("             select nid_Empregado from " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEmpregado (nolock)  where nId_Empr in ");
            strSQL.Append("    ( select idCliente from documento (nolock)  where iddocumento in (select iddocumento from pcmso where idpcmso = " + zIdPCMSO.ToString() + " ) ) ");
            strSQL.Append("   ) ");

            strSQL.Append("    ) ");
            strSQL.Append(" ) as tx90 ");
            strSQL.Append(" order by GHE ");




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

        public DataSet Trazer_Dados_PCMSO_Analitico(Int32 zIdPCMSO, Int32 zIdPCMSO_Ant)
        {

            StringBuilder strSQL = new StringBuilder();


            DataTable table = new DataTable("Result");
            table.Columns.Add("Ano", Type.GetType("System.Int32"));
            table.Columns.Add("GHE", Type.GetType("System.String"));
            table.Columns.Add("Exame", Type.GetType("System.String"));
            table.Columns.Add("Colaboradores_Expostos", Type.GetType("System.String"));
            table.Columns.Add("Numero_Exames", Type.GetType("System.String"));
            table.Columns.Add("IndResultado", Type.GetType("System.String"));


            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);


            strSQL.Append(" select Ano, GHE, Exame, Colaboradores_Expostos, Numero_Exames, IndResultado  ");
            strSQL.Append(" from ");
            strSQL.Append(" ( ");

            // clinicos
            strSQL.Append("     select  datepart(yyyy, d.datapcmso) as Ano, i.tNO_FUNC as GHE, g.nome as Exame, ");
            strSQL.Append("     (select count(distinct nid_Empregado) from " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEmpregado_Funcao where nid_Empregado_Funcao in (select nid_Empregado_Funcao from " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblFunc_Empregado where nid_Func = i.nid_Func )) as Colaboradores_Expostos  ,  ");
            strSQL.Append("     count(distinct a.idexamebase) as Numero_Exames, IndResultado ");
            strSQL.Append("     from examebase (nolock)  as a ");
            strSQL.Append("     join clinico (nolock)  as b on(a.IdExameBase = b.IdExameBase) ");
            strSQL.Append("     join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblempregado_Funcao (nolock)  as c on(a.idempregado = c.nid_empregado) ");
            strSQL.Append("     join pcmso (nolock)  as d on(b.idpcmso = d.idpcmso) ");
            strSQL.Append("     join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tbllaudo_tec (nolock)  as e on(e.nID_LAUD_TEC = d.IdLaudoTecnico) ");
            strSQL.Append("     join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblfunc_empregado (nolock)  as f on(e.nid_laud_tec = f.nID_LAUD_TEC and c.nID_EMPREGADO_FUNCAO = f.nID_EMPREGADO_FUNCAO) ");
            strSQL.Append("     join examedicionario (nolock)  as g on(a.IdExameDicionario = g.IdExameDicionario) ");
            strSQL.Append("     join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblfunc (nolock)  as i on(f.nID_FUNC = i.nID_FUNC) ");
            strSQL.Append("     where b.idpcmso = " + zIdPCMSO.ToString() + "  and a.IndResultado in (1,2 ) ");
            strSQL.Append("     group by datepart(yyyy, d.datapcmso), i.tNO_FUNC, g.nome, i.nid_Func, IndResultado ");

            strSQL.Append(" union ");

            strSQL.Append("     select datepart(yyyy, d.datapcmso) as Ano, i.tNO_FUNC as GHE, g.nome as Exame, ");
            strSQL.Append("     (select count(distinct nid_Empregado) from " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEmpregado_Funcao where nid_Empregado_Funcao in (select nid_Empregado_Funcao from " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblFunc_Empregado where nid_Func = i.nid_Func ) ) as Colaboradores_Expostos  ,  ");
            strSQL.Append("     count(distinct a.idexamebase) as Numero_Exames, IndResultado ");
            strSQL.Append("     from examebase (nolock)  as a ");
            strSQL.Append("     join clinico (nolock)  as b on(a.IdExameBase = b.IdExameBase) ");
            strSQL.Append("     join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblempregado_Funcao (nolock)  as c on(a.idempregado = c.nid_empregado) ");
            strSQL.Append("     join pcmso  (nolock) as d on(b.idpcmso = d.idpcmso) ");
            strSQL.Append("     join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tbllaudo_tec (nolock)  as e on(e.nID_LAUD_TEC = d.IdLaudoTecnico) ");
            strSQL.Append("     join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblfunc_empregado (nolock)  as f on(e.nid_laud_tec = f.nID_LAUD_TEC and c.nID_EMPREGADO_FUNCAO = f.nID_EMPREGADO_FUNCAO) ");
            strSQL.Append("     join examedicionario (nolock)  as g on(a.IdExameDicionario = g.IdExameDicionario) ");
            strSQL.Append("     join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblfunc (nolock)  as i on(f.nID_FUNC = i.nID_FUNC) ");
            strSQL.Append("     where b.idpcmso =  " + zIdPCMSO_Ant.ToString() + "  and a.IndResultado in (1,2 ) ");
            strSQL.Append("     group by datepart(yyyy, d.datapcmso), i.tNO_FUNC, g.nome, i.nid_Func, IndResultado ");

            strSQL.Append("   union ");

            //complementares
            strSQL.Append("     select datepart(yyyy, d.datapcmso) as Ano, i.tNO_FUNC as GHE, g.nome as Exame, ");
            strSQL.Append("     (select count(distinct nid_Empregado) from " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEmpregado_Funcao where nid_Empregado_Funcao in (select nid_Empregado_Funcao from " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblFunc_Empregado where nid_Func = i.nid_Func ) ) as Colaboradores_Expostos  ,  ");
            strSQL.Append("     count(distinct a.idexamebase) as Numero_Exames, IndResultado ");
            strSQL.Append("     from examebase (nolock)  as a ");
            //strSQL.Append("     join complementar as b on(a.IdExameBase = b.IdExameBase) ");
            strSQL.Append("     join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblempregado_Funcao (nolock)  as c on(a.idempregado = c.nid_empregado) ");
            strSQL.Append("     join pcmso (nolock)  as d on(a.dataexame between d.datapcmso and dateadd(yyyy, 1, d.datapcmso)) ");
            strSQL.Append("     join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tbllaudo_tec (nolock)  as e on(e.nID_LAUD_TEC = d.IdLaudoTecnico) ");
            strSQL.Append("     join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblfunc_empregado (nolock)  as f on(e.nid_laud_tec = f.nID_LAUD_TEC and c.nID_EMPREGADO_FUNCAO = f.nID_EMPREGADO_FUNCAO) ");
            strSQL.Append("     join examedicionario (nolock)  as g on(a.IdExameDicionario = g.IdExameDicionario) ");
            strSQL.Append("     join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblfunc (nolock)  as i on(f.nID_FUNC = i.nID_FUNC) ");
            strSQL.Append("     where d.idpcmso =  " + zIdPCMSO.ToString() + "   and a.IndResultado in (1,2 )  and a.IdExameDicionario not between 1 and 5    ");
            strSQL.Append("     group by datepart(yyyy, d.datapcmso), i.tNO_FUNC, g.nome, i.nid_Func, IndResultado ");

            strSQL.Append("   union ");


            strSQL.Append("     select datepart(yyyy, d.datapcmso) as Ano, i.tNO_FUNC as GHE, g.nome as Exame, ");
            strSQL.Append("     (select count(distinct nid_Empregado) from " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEmpregado_Funcao where nid_Empregado_Funcao in (select nid_Empregado_Funcao from " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblFunc_Empregado where nid_Func = i.nid_Func ) ) as Colaboradores_Expostos  ,  ");
            strSQL.Append("     count(distinct a.idexamebase) as Numero_Exames, IndResultado ");
            strSQL.Append("     from examebase (nolock)  as a ");
            //strSQL.Append("     join complementar as b on(a.IdExameBase = b.IdExameBase) ");
            strSQL.Append("     join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblempregado_Funcao (nolock)  as c on(a.idempregado = c.nid_empregado) ");
            strSQL.Append("     join pcmso (nolock)  as d on(a.dataexame between d.datapcmso and dateadd(yyyy, 1, d.datapcmso)) ");
            strSQL.Append("     join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tbllaudo_tec (nolock)  as e on(e.nID_LAUD_TEC = d.IdLaudoTecnico) ");
            strSQL.Append("     join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblfunc_empregado (nolock)  as f on(e.nid_laud_tec = f.nID_LAUD_TEC and c.nID_EMPREGADO_FUNCAO = f.nID_EMPREGADO_FUNCAO) ");
            strSQL.Append("     join examedicionario (nolock)  as g on(a.IdExameDicionario = g.IdExameDicionario) ");
            strSQL.Append("     join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblfunc (nolock)  as i on(f.nID_FUNC = i.nID_FUNC) ");
            strSQL.Append("     where d.idpcmso =  " + zIdPCMSO_Ant.ToString() + "  and a.IndResultado in (1,2 )  and a.IdExameDicionario not between 1 and 5    ");
            strSQL.Append("     group by datepart(yyyy, d.datapcmso), i.tNO_FUNC, g.nome, i.nid_Func, IndResultado ");
            strSQL.Append(" ) as tx90 ");
            strSQL.Append(" order by GHE, Exame, Ano ");



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


        public Int32 Trazer_Dados_PCMSO_Analitico_Doencas(Int32 zIdPCMSO, Int32 zIdGHE)
        {

            StringBuilder strSQL = new StringBuilder();


            DataTable table = new DataTable("Result");
            table.Columns.Add("Total", Type.GetType("System.Int32"));


            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);


            strSQL.Append(" select COUNT(*) as Total ");
            strSQL.Append(" from Acidente (nolock)  ");
            strSQL.Append(" where DataAcidente between ");
            strSQL.Append(" (select top 1 datapcmso from Pcmso (nolock)  where idPCMSO = " + zIdPCMSO.ToString() + "  ) and ( select top 1 dateadd(yyyy, 1, datapcmso) from Pcmso where idPCMSO = " + zIdPCMSO.ToString() + " ) ");
            strSQL.Append(" and IndTipoAcidente = 2  ");
            strSQL.Append(" and idempregado in ");
            strSQL.Append("  ( select nId_Empregado from " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEMPREGADO_FUNCAO (nolock)  where nID_EMPREGADO_FUNCAO in ");
            strSQL.Append("      (select nID_EMPREGADO_FUNCAO from " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblFUNC_EMPREGADO (nolock)  where nID_FUNC = " + zIdGHE.ToString() + "  ) ");
            strSQL.Append("  ) ");



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
                return System.Convert.ToInt32(m_ds.Tables[0].Rows[0][0].ToString());


        }



        public Int32 Trazer_Dados_PCMSO_Analitico_Doencas_Prevalencia(Int32 zIdGHE)
        {

            StringBuilder strSQL = new StringBuilder();


            DataTable table = new DataTable("Result");
            table.Columns.Add("Total", Type.GetType("System.Int32"));


            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);


            strSQL.Append(" select COUNT(*) as Total ");
            strSQL.Append(" from Acidente (nolock)  ");
            strSQL.Append(" where DataAcidente between ");
            strSQL.Append(" convert( smalldatetime, '01/01/2022', 103 ) and getdate() ");
            strSQL.Append(" and IndTipoAcidente = 2  ");
            strSQL.Append(" and idempregado in ");
            strSQL.Append("  ( select nId_Empregado from " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEMPREGADO_FUNCAO (nolock)  where nID_EMPREGADO_FUNCAO in ");
            strSQL.Append("      (select nID_EMPREGADO_FUNCAO from " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblFUNC_EMPREGADO (nolock)  where nID_FUNC = " + zIdGHE.ToString() + "  ) ");
            strSQL.Append("  ) ");



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
                return System.Convert.ToInt32(m_ds.Tables[0].Rows[0][0].ToString());


        }


        public Int16 Trazer_PCMSO_Exame_Idade_Minima(Int32 zIdPCMSOPlanejamento)
        {

            StringBuilder strSQL = new StringBuilder();


            DataTable table = new DataTable("Result");
            table.Columns.Add("Minimo", Type.GetType("System.Int32"));


            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);


            strSQL.Append(" select minimo from ");
            strSQL.Append(" ( ");
            strSQL.Append("   select MIN(anoinicio) as Minimo ");
            strSQL.Append("   from PcmsoPlanejamentoIdade (nolock) as a ");
            strSQL.Append("   join PcmsoPlanejamento (nolock) as b on (a.IdPcmsoPlanejamento = b.IdPcmsoPlanejamento) ");
            strSQL.Append("   join ExameDicionario (nolock) as c on (b.IdExameDicionario = c.IdExameDicionario) ");
            strSQL.Append("   where b.IdExameDicionario not between 1 and 5 ");
            strSQL.Append("   and b.IdPcmsoPlanejamento = " + zIdPCMSOPlanejamento.ToString() + " ");
            strSQL.Append("   and b.DependeIdade = 1 ");
            strSQL.Append("   group by a.IdPcmsoPlanejamento ");
            strSQL.Append(" ) as tx55 ");
            strSQL.Append(" where minimo > 18 ");




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
                return System.Convert.ToInt16(m_ds.Tables[0].Rows[0][0].ToString());


        }


        public DataSet Retornar_Aptidoes_Empregado(Int32 xIdEmpregado)
        {

            StringBuilder strSQL = new StringBuilder();


            DataTable table = new DataTable("Result");
            table.Columns.Add("x1", Type.GetType("System.String"));
            table.Columns.Add("x2", Type.GetType("System.String"));
            table.Columns.Add("x3", Type.GetType("System.String"));
            table.Columns.Add("x4", Type.GetType("System.String"));
            table.Columns.Add("x5", Type.GetType("System.String"));
            table.Columns.Add("x6", Type.GetType("System.String"));
            table.Columns.Add("x7", Type.GetType("System.String"));
            table.Columns.Add("x8", Type.GetType("System.String"));
            table.Columns.Add("x9", Type.GetType("System.String"));
            table.Columns.Add("x10", Type.GetType("System.String"));
            table.Columns.Add("x11", Type.GetType("System.String"));



            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);


            strSQL.Append(" select isnull(max(apt1),'') as x1, ");
            strSQL.Append("        isnull(max(apt2),'') as x2, ");
            strSQL.Append("        isnull(max(apt3),'') as x3, ");
            strSQL.Append("        isnull(max(apt4),'') as x4, ");
            strSQL.Append("        isnull(max(apt5),'') as x5, ");
            strSQL.Append("        isnull(MAX(apt6),'') as x6, ");
            strSQL.Append("        isnull(MAX(apt7),'') as x7, ");
            strSQL.Append("        isnull(MAX(apt8),'') as x8, ");
            strSQL.Append("        isnull(MAX(apt9),'') as x9, ");
            strSQL.Append("        isnull(MAX(apt10),'') as x10, ");
            strSQL.Append("        isnull(MAX(apt11),'') as x11 ");
            strSQL.Append(" from ");
            strSQL.Append(" ( ");
            strSQL.Append("   select case when cast(apt_trabalho_altura as int) = 1 then 'Trabalho em Altura' else '' end as apt1, ");
            strSQL.Append("          case when cast(apt_espaco_confinado as int) = 1 then 'Espaços Confinados'  else '' end as apt2, ");
            strSQL.Append(" 		 case when cast(apt_Transporte as int) = 1 then 'Equip.Transp.Motorizado' else '' end as apt3, ");
            strSQL.Append(" 		 case when cast(apt_Submerso as int) = 1 then 'Atividades Submersas' else '' end as apt4, ");
            strSQL.Append(" 		 case when cast(apt_Eletricidade as int) = 1 then 'Serviços com Eletricidade' else '' end as apt5, ");
            strSQL.Append(" 		 case when cast(apt_Aquaviario as int) = 1 then 'Serviços aquaviários' else '' end as apt6, ");
            strSQL.Append(" 		 case when cast(apt_Alimento as int) = 1 then 'Manipulação de alimentos' else '' end as apt7, ");
            strSQL.Append(" 		 case when cast(apt_Brigadista as int) = 1 then 'Brigadista'  else '' end as apt8, ");
            strSQL.Append(" 		 case when cast(apt_Socorrista as int) = 1 then 'Socorrista' else '' end as apt9, ");
            strSQL.Append(" 		 case when cast(apt_Trabalho_Bordo as int) = 1 then 'com Condições Físicas e Mentais para Trabalho a Bordo' else '' end as apt10, ");
            strSQL.Append(" 		 case when cast(apt_Radiacao as int) = 1 then 'Radiação' else '' end as apt11 ");
            strSQL.Append("   from tblEMPREGADO_APTIDAO (nolock)  where nID_EMPREGADO = " + xIdEmpregado.ToString() + " " );

            strSQL.Append("   union ");

            strSQL.Append("   select case when cast(apt_trabalho_altura as int) = 1 then 'Trabalho em Altura' else '' end as apt1, ");
            strSQL.Append("          case when cast(apt_espaco_confinado as int) = 1 then 'Espaços Confinados'  else '' end as apt2, ");
            strSQL.Append(" 		 case when cast(apt_Transporte as int) = 1 then 'Equip.Transp.Motorizado' else '' end as apt3, ");
            strSQL.Append(" 		 case when cast(apt_Submerso as int) = 1 then 'Atividades Submersas'  else '' end as apt4, ");
            strSQL.Append(" 		 case when cast(apt_Eletricidade as int) = 1 then 'Serviços com Eletricidade' else '' end as apt5, ");
            strSQL.Append(" 		 case when cast(apt_Aquaviario as int) = 1 then 'Serviços aquaviários' else '' end as apt6, ");
            strSQL.Append(" 		 case when cast(apt_Alimento as int) = 1 then 'Manipulação de alimentos' else '' end as apt7, ");
            strSQL.Append(" 		 case when cast(apt_Brigadista as int) = 1 then 'Brigadista'  else '' end as apt8, ");
            strSQL.Append(" 		 case when cast(apt_Socorrista as int) = 1 then 'Socorrista' else '' end as apt9, ");
            strSQL.Append(" 		 case when cast(apt_Trabalho_Bordo as int) = 1 then 'com Condições Físicas e Mentais para Trabalho a Bordo' else '' end as apt10, ");
            strSQL.Append(" 		 case when cast(apt_Radiacao as int) = 1 then 'Radiação' else '' end as apt11 ");
            strSQL.Append("   from tblFUNC_APTIDAO (nolock)  ");
            strSQL.Append("   where nID_FUNC in ");
            strSQL.Append("   ( ");
            strSQL.Append("      select nID_FUNC  from tblFUNC_EMPREGADO (nolock)  ");
            strSQL.Append("       where nID_EMPREGADO_FUNCAO in ");
            strSQL.Append("       (select top 1 nID_EMPREGADO_FUNCAO from tblEMPREGADO_FUNCAO (nolock)  where nID_EMPREGADO = " + xIdEmpregado.ToString() + "  order by hDT_INICIO desc) ");
            strSQL.Append("   ) ");
            strSQL.Append(" ) as tx98 ");


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


        public DataSet Trazer_PCMSO_GHE_Empregado(Int32 xIdEmpregado)
        {

            StringBuilder strSQL = new StringBuilder();


            DataTable table = new DataTable("Result");
            table.Columns.Add("IdPCMSO", Type.GetType("System.Int32"));
            table.Columns.Add("IdGHE", Type.GetType("System.Int32"));
            table.Columns.Add("IdCliente", Type.GetType("System.Int32"));

            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);


            //strSQL.Append(" select top 1 c.IdPCMSO, b.IdGHE, d.IdCliente ");
            //strSQL.Append(" from ExamePlanejamento (nolock)  as a ");
            //strSQL.Append(" join PcmsoPlanejamento (nolock)  as b on (a.IdPcmsoPlanejamento = b.IdPcmsoPlanejamento) ");
            //strSQL.Append(" join pcmso (nolock)  as c on (a.IdPcmso = c.IdPcmso) ");
            //strSQL.Append(" join documento (nolock)  as d on (c.IdDocumento = d.IdDocumento ) ");
            //strSQL.Append(" where a.idempregado = " + xIdEmpregado.ToString() + " " );
            //strSQL.Append(" order by datapcmso desc ");


            strSQL.Append(" select top 1 c.IdPCMSO, b.IdGHE, d.IdCliente ");
            strSQL.Append("  from PcmsoPlanejamento (nolock) as b ");
            strSQL.Append("  left join pcmso (nolock) as c on (b.IdPcmso = c.IdPcmso) ");
            strSQL.Append("  left join documento (nolock) as d on (c.IdDocumento = d.IdDocumento) ");
            strSQL.Append("  where idghe in ");
            strSQL.Append("  (select top 1 nid_func from " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblfunc_Empregado (nolock) as a join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tbllaudo_tec (nolock) as b on (a.nID_LAUD_TEC = b.nID_LAUD_TEC) ");
            strSQL.Append("    where nID_EMPREGADO_FUNCAO in ");
            strSQL.Append("    (select top 1 nid_empregado_funcao from  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblempregado_funcao (nolock) where nid_empregado = " + xIdEmpregado.ToString() + " order by hdt_inicio desc )  ");
            strSQL.Append("    order by hdt_laudo desc ");
            strSQL.Append("  ) ");
            strSQL.Append("  order by datapcmso desc ");




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



        public DataSet Trazer_Colaboradores_Toxicologico(Int32 xIdEmpresa, Int32 xIdSorteio)
        {

            StringBuilder strSQL = new StringBuilder();


            DataTable table = new DataTable("Result");
            table.Columns.Add("nId_Empregado", Type.GetType("System.Int32"));
            table.Columns.Add("tNo_Empg", Type.GetType("System.String"));
            table.Columns.Add("Funcao", Type.GetType("System.String"));
            table.Columns.Add("Data_Sorteio", Type.GetType("System.String"));
            table.Columns.Add("Sorteado", Type.GetType("System.String"));

            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);

            strSQL.Append(" SELECT * from (  ");
            strSQL.Append(" SELECT nID_EMPREGADO, tNO_EMPG,  ");
            strSQL.Append(" (select top 1 BF.NomeFuncao  from tblEmpregado_Funcao (nolock) as AF join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.Funcao (nolock) as BF on (AF.nid_Funcao = BF.IdFuncao) where AF.nID_Empregado = a.nID_EMPREGADO order by hdt_Inicio desc) as Funcao, ");
            strSQL.Append(" isnull((select top 1 CONVERT(char(10), AG.Data_Sorteio, 103) from " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.Toxicologico_Sorteio_Colaborador  (nolock) as AG where AG.IdEmpregado = a.nID_EMPREGADO order by Data_Sorteio desc), '-') as Data_Sorteio, ");
            strSQL.Append(" case when isnull((select top 1 AH.Sorteado from " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.Toxicologico_Sorteio_Colaborador (nolock) as AH where AH.IdEmpregado = a.nID_EMPREGADO order by Data_Sorteio desc ),  0) = 0 then 'Não' else 'Sim' end as Sorteado ");
            strSQL.Append(" FROM tblEMPREGADO (NOLOCK) as a ");
            strSQL.Append(" left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.Toxicologico_Sorteio_Colaborador(nolock) as b on(a.nID_EMPREGADO = b.IdEmpregado) ");
            strSQL.Append(" WHERE  a.hdt_Dem is null and nId_Empr = " + xIdEmpresa.ToString() + " ");
            //strSQL.Append(" WHERE  nId_Empr = " + xIdEmpresa.ToString() +  " and tEmail <> '' ");

            if (xIdSorteio != 0)
            {
                strSQL.Append(" and nId_Empregado not in ( ");
                strSQL.Append(" Select IdEmpregado from " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.Toxicologico_Sorteio_Colaborador(nolock) where Id_Toxicologico_Sorteio = " + xIdSorteio.ToString() + " ) ");
            }


            strSQL.Append(" ) as tx90 where Funcao like '%Motorist%' ");
            strSQL.Append(" order by tNo_Empg ");
            

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



        public DataSet Trazer_Colaboradores_Toxicologico_Participantes(Int32 xIdEmpresa, Int32 xIdSorteio)
        {

            StringBuilder strSQL = new StringBuilder();


            DataTable table = new DataTable("Result");
            table.Columns.Add("Id_Toxicologico_Sorteio_Colaborador", Type.GetType("System.Int32"));
            table.Columns.Add("nId_Empregado", Type.GetType("System.Int32"));
            table.Columns.Add("tNo_Empg", Type.GetType("System.String"));
            table.Columns.Add("Funcao", Type.GetType("System.String"));

            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);


            strSQL.Append(" SELECT Id_Toxicologico_Sorteio_Colaborador, nID_EMPREGADO, tNO_EMPG,  ");
            strSQL.Append(" (select top 1 BF.NomeFuncao  from tblEmpregado_Funcao (nolock) as AF join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.Funcao (nolock) as BF on (AF.nid_Funcao = BF.IdFuncao) where AF.nID_Empregado = a.nID_EMPREGADO order by hdt_Inicio desc) as Funcao ");
            strSQL.Append(" FROM tblEMPREGADO (NOLOCK) as a ");
            strSQL.Append(" join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.Toxicologico_Sorteio_Colaborador(nolock) as b on(a.nID_EMPREGADO = b.IdEmpregado) ");
            strSQL.Append(" WHERE  nId_Empr = " + xIdEmpresa.ToString() + " and Sorteado = 0 and Id_Toxicologico_Sorteio = " + xIdSorteio.ToString() + " ");
            strSQL.Append(" order by tNo_Empg ");


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

        public DataSet Trazer_Colaboradores_Toxicologico_Sorteados(Int32 xIdEmpresa, Int32 xIdSorteio)
        {

            StringBuilder strSQL = new StringBuilder();


            DataTable table = new DataTable("Result");
            table.Columns.Add("Id_Toxicologico_Sorteio_Colaborador", Type.GetType("System.Int32"));
            table.Columns.Add("nId_Empregado", Type.GetType("System.Int32"));
            table.Columns.Add("tNo_Empg", Type.GetType("System.String"));
            table.Columns.Add("Funcao", Type.GetType("System.String"));

            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);


            strSQL.Append(" SELECT Id_Toxicologico_Sorteio_Colaborador, nID_EMPREGADO, tNO_EMPG,  ");
            strSQL.Append(" (select top 1 BF.NomeFuncao  from tblEmpregado_Funcao (nolock) as AF join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.Funcao (nolock) as BF on (AF.nid_Funcao = BF.IdFuncao) where AF.nID_Empregado = a.nID_EMPREGADO order by hdt_Inicio desc) as Funcao ");
            strSQL.Append(" FROM tblEMPREGADO (NOLOCK) as a ");
            strSQL.Append(" join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.Toxicologico_Sorteio_Colaborador(nolock) as b on(a.nID_EMPREGADO = b.IdEmpregado) ");
            strSQL.Append(" WHERE  nId_Empr = " + xIdEmpresa.ToString() + " and Sorteado = 1 and Id_Toxicologico_Sorteio = " + xIdSorteio.ToString() + " ");
            strSQL.Append(" order by tNo_Empg ");


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



    }

}