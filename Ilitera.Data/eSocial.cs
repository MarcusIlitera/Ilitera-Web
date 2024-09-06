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
    public class eSocial
    {


        public Int32 Buscar_Acidente_Reabertura(Int32 nIdCliente, string nRecibo)
        {

            StringBuilder strSQL = new StringBuilder();


            DataTable table = new DataTable("Result");

            table.Columns.Add("IdAcidente", Type.GetType("System.Int32"));


            strSQL.Append(" select nId_Chave as IdAcidente ");
            strSQL.Append(" from tbl_esocial_envio as a ");
            strSQL.Append(" join tbl_esocial_deposito as b on (a.IdeSocial_Deposito = b.IdeSocial_Deposito) ");
            strSQL.Append(" join tbl_esocial as c on (b.IdeSocial = c.IdeSocial) ");
            strSQL.Append(" where protocolo = '" + nRecibo +"' and evento = '2210' ");
            strSQL.Append(" and cpf in ");
            strSQL.Append(" (select tno_cpf from tblempregado where nid_empregado in ( select nid_empregado from tblempregado_funcao where nid_empr = " + nIdCliente.ToString() + " ) ) ");
            

            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);


            using (SqlConnection cnn = new SqlConnection(Ilitera.Data.SQLServer.EntitySQLServer.GetConnection_Sied()))
            {
                SqlDataAdapter da;
                cnn.Open();


                da = new SqlDataAdapter(strSQL.ToString(), cnn);
                da.Fill(m_ds, "Result");

                cnn.Close();

                da.Dispose();
            }


            if (m_ds.Tables[0].Rows.Count == 0) return 0;
            else return System.Convert.ToInt32( m_ds.Tables[0].Rows[0][0].ToString().Trim() );


        }



        public string Retornar_CNPJ_Matriz(Int32 nIdCliente)
        {

            StringBuilder strSQL = new StringBuilder();


            DataTable table = new DataTable("Result");

            table.Columns.Add("CNPJ", Type.GetType("System.String"));


            strSQL.Append(" select dbo.udf_getnumeric(SUBSTRING(NomeCodigo,1,20)) as CNPJ from pessoa where idpessoa in  ");
            strSQL.Append(" ( ");
            strSQL.Append("  select case when idjuridicapapel = 1 then idpessoa else idjuridicapai end as Id from juridica where idpessoa = " + nIdCliente.ToString());
            strSQL.Append(" ) ");

            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);


            using (SqlConnection cnn = new SqlConnection(Ilitera.Data.SQLServer.EntitySQLServer.GetConnection()))
            {
                SqlDataAdapter da;
                cnn.Open();


                da = new SqlDataAdapter(strSQL.ToString(), cnn);
                da.Fill(m_ds, "Result");

                cnn.Close();

                da.Dispose();
            }


            if (m_ds.Tables[0].Rows.Count == 0) return "";
            else return m_ds.Tables[0].Rows[0][0].ToString().Trim();


        }


        public string Retornar_CNPJ_Classif_Funcional_Atual(Int32 nIdEmpregado)
        {

            StringBuilder strSQL = new StringBuilder();


            DataTable table = new DataTable("Result");

            table.Columns.Add("CNPJ", Type.GetType("System.String"));


            strSQL.Append(" select dbo.udf_getnumeric(SUBSTRING(NomeCodigo,1,20)) as CNPJ from pessoa where idpessoa in  ");
            strSQL.Append(" ( ");
            strSQL.Append("    select Top 1 nid_Empr from " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEMPREGADO_FUNCAO  where nID_EMPREGADO = " + nIdEmpregado.ToString() + "  order by hDT_INICIO desc ");
            strSQL.Append(" ) ");

            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);


            using (SqlConnection cnn = new SqlConnection(Ilitera.Data.SQLServer.EntitySQLServer.GetConnection()))
            {
                SqlDataAdapter da;
                cnn.Open();


                da = new SqlDataAdapter(strSQL.ToString(), cnn);
                da.Fill(m_ds, "Result");

                cnn.Close();

                da.Dispose();
            }


            if (m_ds.Tables[0].Rows.Count == 0) return "";
            else return m_ds.Tables[0].Rows[0][0].ToString().Trim();


        }




        public DataSet Retornar_Eventos_Obra_2240(Int32 xIdEmpregado, Int32 xIdLaudo)
        {
            //string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection();
            StringBuilder strSQL = new StringBuilder();

            //DataSet m_ds;


            DataTable table = new DataTable("Result");

            table.Columns.Add("nId_Laud_Tec", Type.GetType("System.Int32"));
            table.Columns.Add("nId_Empregado", Type.GetType("System.Int32"));
            table.Columns.Add("IdPessoa", Type.GetType("System.Int32"));
            table.Columns.Add("IdGrupoEmpresa", Type.GetType("System.Int32"));
            table.Columns.Add("CNPJ", Type.GetType("System.String"));

            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);


            strSQL.Append(" select max(d.nId_Laud_Tec) as nId_Laud_tec, a.nId_Empregado, case when e.IdJuridicaPai is null then e.idpessoa else e.idjuridicapai end as IdGrupoEmpresa, b.nid_empr as IdPessoa, dbo.udf_getnumeric(f.NomeCodigo) as CNPJ ");
            strSQL.Append(" from tblempregado as a ");
            strSQL.Append(" left join tblempregado_funcao as b on(a.nID_EMPREGADO = b.nID_EMPREGADO) ");
            strSQL.Append(" left join tblfunc_empregado as c on(b.nID_EMPREGADO_FUNCAO = c.nID_EMPREGADO_FUNCAO) ");
            strSQL.Append(" left join tbllaudo_tec as d on(c.nID_LAUD_TEC = d.nID_LAUD_TEC) ");
            strSQL.Append(" left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.juridica as e on (d.nid_empr = e.idpessoa) ");
            strSQL.Append(" left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.Pessoa as f on (d.nid_empr = f.idpessoa) ");
            strSQL.Append(" where a.nid_empregado = " + xIdEmpregado.ToString() + " ");

            //considerar mes/ano igual
            //strSQL.Append(" and datepart(yyyy, d.hdt_laudo) * 100 + datepart(mm, d.hdt_laudo) = (select datepart(yyyy, hdt_laudo) * 100 + datepart( mm, hdt_laudo) from tbllaudo_tec where nid_laud_tec = " + xIdLaudo.ToString() + " ) ");

            strSQL.Append(" and datepart(yyyy, d.hdt_laudo) = (select datepart(yyyy, hdt_laudo) from tbllaudo_tec where nid_laud_tec = " + xIdLaudo.ToString() + " ) ");

            //strSQL.Append(" and d.hdt_laudo between(select hdt_laudo from tbllaudo_tec where nid_laud_tec = " + xIdLaudo.ToString() + " ) ");
            //strSQL.Append("                     and(select dateadd(yyyy, 1, hdt_laudo) from tbllaudo_tec where nid_laud_tec = " + xIdLaudo.ToString() + "  ) ");
            strSQL.Append(" and(e.idjuridicapai in (select nid_empr  from tbllaudo_tec where nid_laud_tec = " + xIdLaudo.ToString() + "  ) ");
            strSQL.Append("    or e.idjuridicapai in (select idjuridicapai from " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.juridica where idpessoa in (select nid_empr  from tbllaudo_tec where nid_laud_tec = " + xIdLaudo.ToString() + "  ) ) ");
            strSQL.Append("    or e.idpessoa in (select idjuridicapai from " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.juridica where idpessoa in (select nid_empr from tbllaudo_tec where nid_laud_tec = " + xIdLaudo.ToString() + "  ) ) ");
            strSQL.Append("    or e.idpessoa in (select nid_empr from tbllaudo_tec where nid_laud_tec = " + xIdLaudo.ToString() + "  )  ");
            strSQL.Append("    ) ");
            strSQL.Append(" AND d.nID_PEDIDO IN ( SELECT idpedido from " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.Pedido where DataConclusao is not null and IndStatus = 2 ) ");
            strSQL.Append(" group by a.nId_Empregado, e.IdJuridicaPai, b.nid_empr , f.NomeCodigo, e.idpessoa ");
            strSQL.Append(" order by e.IdJuridicaPai ");



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


        public DataSet Retornar_Mesmo_IDs_2240(Int32 xIdeSocial_Deposito)
        {
            //string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection();
            StringBuilder strSQL = new StringBuilder();

            //DataSet m_ds;


            DataTable table = new DataTable("Result");

            table.Columns.Add("IdeSocial_Deposito", Type.GetType("System.Int32"));

            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);


            strSQL.Append(" select IdeSocial_Deposito from tbl_eSocial_Deposito ");
            strSQL.Append(" where Id in (select Id from tbl_eSocial_Deposito where IdeSocial_Deposito = " + xIdeSocial_Deposito.ToString() + " ) ");
            //strSQL.Append(" and IdeSocial_Deposito <> " + xIdeSocial_Deposito.ToString() + " "); 


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



        public DataSet Retornar_Exames_2220(Int32 nId_Empregado, string zD1, string zD2, bool zEssence)
        {

            StringBuilder strSQL = new StringBuilder();


            DataTable table = new DataTable("Result");

            table.Columns.Add("IdExameBase", Type.GetType("System.Int32"));



            strSQL.Append(" SELECT IdExameBase  ");
            strSQL.Append(" FROM ExameBase (nolock) as a ");
            strSQL.Append(" left join ExameDicionario (nolock) as b on(a.IdExameDicionario = b.IdExameDicionario) ");
            strSQL.Append(" WHERE  IdEmpregado = " + nId_Empregado.ToString() + " and a.IdExameDicionario not between 1 and 5 ");
            strSQL.Append(" and DataExame between convert(smalldatetime, '" + zD1 + "', 103) and convert(smalldatetime, '" + zD2 + "', 103) ");

            //if (zEssence == false)
            //{
             //   strSQL.Append(" and IndResultado in (1, 2) ");
            //}

            strSQL.Append(" and codigo_esocial +' ' + convert(char(10), dataexame, 103) in ");
            strSQL.Append(" ( ");
            strSQL.Append(" select codigo_esocial + ' ' + convert(char(10), max(dataexame), 103) ");
            strSQL.Append(" FROM ExameBase (nolock) as av ");
            strSQL.Append(" left join ExameDicionario (nolock) as bv on(av.IdExameDicionario = bv.IdExameDicionario) ");
            strSQL.Append(" WHERE IdEmpregado = " + nId_Empregado.ToString() + " and av.IdExameDicionario not between 1 and 5 ");
            strSQL.Append(" and DataExame between convert(smalldatetime, '" + zD1 + "', 103) and convert(smalldatetime, '" + zD2 + "', 103) ");

            //if (zEssence == false)
            //{
            //    strSQL.Append(" and IndResultado in (1, 2) ");
            //}


            strSQL.Append(" group by codigo_esocial ");
            strSQL.Append(" ) ");

            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);


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


        public void Gerar_XML_2360(Int32 nIdCliente, string xArquivo)
        {

            StringBuilder strSQL = new StringBuilder();


            DataTable table = new DataTable("eSocial");


            strSQL.Append("select a.tno_func, b.tno_empg, d.DescricaoResumido as Risco, c.tVL_MED , e.tNO_AG_NCV as Agente_Nocivo, f.Descricao as Unidade, '03' as tpCondicao, ");
            strSQL.Append("case  ");
            strSQL.Append("  when d.DescricaoResumido = 'Ruído Contínuo' then 'F5.1' ");
            strSQL.Append("  when d.DescricaoResumido = 'Calor' then 'F2.2' ");
            strSQL.Append("  when d.DescricaoResumido = 'Radiações Não Ionizantes' and e.tNO_AG_NCV like 'Ultravioleta%' then 'F8.2' ");
            strSQL.Append("  when d.DescricaoResumido = 'Radiações Não Ionizantes'  then 'F8.5' ");
            strSQL.Append("  when d.DescricaoResumido like '%Agentes Químicos%' then 'Q7' ");
            strSQL.Append("end as Agente_Nocivo_Codigo ");
            strSQL.Append("FROM tblFUNC as a ");
            strSQL.Append("left join  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.qryEmpregadoFuncaoGhe as b ");
            strSQL.Append("on a.nID_FUNC = b.nID_FUNC and a.nID_LAUD_TEC = b.nID_LAUD_TEC ");
            strSQL.Append("left join tblPPRA1 as c ");
            strSQL.Append("on ( b.nID_FUNC = c.nID_FUNC ) ");
            strSQL.Append("left join  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.risco as d ");
            strSQL.Append("on ( c.nid_rsc = d.IdRisco ) ");
            strSQL.Append("left join tblAG_NCV as e ");
            strSQL.Append("on ( c.nID_AG_NCV = e.nID_AG_NCV ) ");
            strSQL.Append("left join  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.Unidade as f ");
            strSQL.Append("on  ( c.nID_UN = f.IdUnidade ) ");
            strSQL.Append("WHERE a.nID_LAUD_TEC=-319610945  ");
            strSQL.Append("ORDER BY tNO_FUNC, b.tno_empg ");




            //m_ds = new DataSet("Result");

            using (SqlConnection cnn = new SqlConnection(Ilitera.Data.SQLServer.EntitySQLServer.GetConnection_Sied()))
            {
                SqlDataAdapter da;
                cnn.Open();


                da = new SqlDataAdapter(strSQL.ToString(), cnn);
                da.Fill(table);


                table.WriteXml(xArquivo);
                cnn.Close();

                da.Dispose();
            }

            return;

        }





        public DataSet Trazer_2230(Int32 xIdEmpresa, Int32 xIdEmpregado, Int32 xIdEmpresaGrupo, string zData1, string zData2)
        {
            //string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection();
            StringBuilder strSQL = new StringBuilder();

            //DataSet m_ds;


            DataTable table = new DataTable("Result");

            table.Columns.Add("CPF", Type.GetType("System.String"));
            table.Columns.Add("PIS", Type.GetType("System.String"));
            table.Columns.Add("CNPJ", Type.GetType("System.String"));
            table.Columns.Add("Matricula", Type.GetType("System.String"));
            table.Columns.Add("CodMotAfast", Type.GetType("System.Int16"));
            table.Columns.Add("DtInicial", Type.GetType("System.String"));
            table.Columns.Add("DtVolta", Type.GetType("System.String"));
            table.Columns.Add("Dias", Type.GetType("System.Int16"));
            table.Columns.Add("Cid1", Type.GetType("System.String"));
            table.Columns.Add("Cid2", Type.GetType("System.String"));
            table.Columns.Add("Cid3", Type.GetType("System.String"));
            table.Columns.Add("Cid4", Type.GetType("System.String"));
            table.Columns.Add("Atestado_Emitente", Type.GetType("System.String"));
            table.Columns.Add("Atestado_ideOC", Type.GetType("System.String"));
            table.Columns.Add("Atestado_nrOC", Type.GetType("System.String"));
            table.Columns.Add("Atestado_ufOC", Type.GetType("System.String"));
            table.Columns.Add("Observacao", Type.GetType("System.String"));
            table.Columns.Add("IdAfastamento", Type.GetType("System.String"));


            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);


            strSQL.Append(" select a.IdAfastamento, dbo.udf_getnumeric(c.tno_CPF) as CPF, c.nNO_PIS_PASEP as PIS,  c.tCod_Empr as Matricula, dbo.udf_getnumeric(d.NomeCodigo) as CNPJ, a.Obs as Observacao, ");
            strSQL.Append("    case when a.IdAfastamentoTipo is null then 1 ");
            strSQL.Append("         when a.IdAfastamentoTipo is not null then e.Codigo_eSocial ");
            strSQL.Append("    end as CodMotAfast, convert(char(10), DataInicial, 126) as DtInicial, ");
            strSQL.Append("    case when DataVolta is null then '' when DataVolta is not null then convert(char(10), DataVolta, 126) end as DtVolta, case when DataInicial is null or DataVolta is null then 0  when datediff(dd, DataInicial, DataVolta) > 32000 then 32000 else datediff(dd, DataInicial, DataVolta) end as Dias, ");
            strSQL.Append("    case when b.CodigoCid is null then '' when b.CodigoCid is not null then b.CodigoCid end as Cid1, ");
            strSQL.Append("    case when f.CodigoCid is null then '' when f.CodigoCid is not null then f.CodigoCid end as Cid2, ");
            strSQL.Append("    case when g.CodigoCid is null then '' when g.CodigoCid is not null then g.CodigoCid end as Cid3, ");
            strSQL.Append("    case when h.CodigoCid is null then '' when h.CodigoCid is not null then h.CodigoCid end as Cid4, ");
            strSQL.Append("    Atestado_Emitente, Atestado_ideOC, Atestado_nrOC, Atestado_ufOC ");
            strSQL.Append(" from afastamento as a left join CID as b ");
            strSQL.Append(" on(a.idcid = b.IdCID) ");
            strSQL.Append(" left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblempregado as c ");
            strSQL.Append(" on(a.idempregado = c.nid_empregado) ");
            strSQL.Append(" left join pessoa as d ");
            strSQL.Append(" on(c.nid_empr = d.idpessoa) ");
            strSQL.Append(" left join afastamentotipo as e ");
            strSQL.Append(" on(a.IdAfastamentoTipo = e.idAfastamentoTipo) ");
            strSQL.Append(" left join CID as f ");
            strSQL.Append(" on(a.idcid2 = f.IdCID) ");
            strSQL.Append(" left join CID as g ");
            strSQL.Append(" on(a.idcid3 = g.IdCID) ");
            strSQL.Append(" left join CID as h ");
            strSQL.Append(" on(a.idcid4 = h.IdCID) ");
            strSQL.Append(" where IdEmpregado in ");

            if (xIdEmpresa != 0)
            {
                strSQL.Append(" (select nId_Empregado from  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEMPREGADO ");
                strSQL.Append(" where nid_Empr = " + xIdEmpresa.ToString() + " ");
                strSQL.Append(" ) ");
            }
            else if (xIdEmpregado != 0)
            {
                strSQL.Append(" ( " + xIdEmpregado.ToString() + " ) ");
            }
            else if (xIdEmpresaGrupo != 0)
            {
                strSQL.Append(" (select nId_Empregado from  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEMPREGADO ");
                strSQL.Append(" where nid_Empr in ( select IdPessoa from Juridica where IdGrupoEmpresa in ( select IdGrupoEmpresa from Juridica where Idpessoa =  " + xIdEmpresaGrupo.ToString() + " )  ) ");
                strSQL.Append(" ) ");
            }


            strSQL.Append(" and tno_CPF is not null  and tno_CPF <> ''  and a.DataInicial between convert( smalldatetime, '" + zData1 + "', 103 ) and convert( smalldatetime, '" + zData2 + "', 103 )  ");

            //strSQL.Append(" and DataVolta is not null ");
            strSQL.Append(" order by c.tno_CPF, datainicial desc ");



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




        public DataSet Trazer_2230(Int32 xIdAfastamento)
        {
            //string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection();
            StringBuilder strSQL = new StringBuilder();

            //DataSet m_ds;


            DataTable table = new DataTable("Result");

            table.Columns.Add("CPF", Type.GetType("System.String"));
            table.Columns.Add("PIS", Type.GetType("System.String"));
            table.Columns.Add("CNPJ", Type.GetType("System.String"));
            table.Columns.Add("Matricula", Type.GetType("System.String"));
            table.Columns.Add("CodMotAfast", Type.GetType("System.Int16"));
            table.Columns.Add("DtInicial", Type.GetType("System.String"));
            table.Columns.Add("DtVolta", Type.GetType("System.String"));
            table.Columns.Add("Dias", Type.GetType("System.Int16"));
            table.Columns.Add("Cid1", Type.GetType("System.String"));
            table.Columns.Add("Cid2", Type.GetType("System.String"));
            table.Columns.Add("Cid3", Type.GetType("System.String"));
            table.Columns.Add("Cid4", Type.GetType("System.String"));
            table.Columns.Add("Atestado_Emitente", Type.GetType("System.String"));
            table.Columns.Add("Atestado_ideOC", Type.GetType("System.String"));
            table.Columns.Add("Atestado_nrOC", Type.GetType("System.String"));
            table.Columns.Add("Atestado_ufOC", Type.GetType("System.String"));
            table.Columns.Add("Observacao", Type.GetType("System.String"));
            table.Columns.Add("IdAfastamento", Type.GetType("System.String"));


            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);


            strSQL.Append(" select a.IdAfastamento, dbo.udf_getnumeric(c.tno_CPF) as CPF, c.nNO_PIS_PASEP as PIS,  c.tCod_Empr as Matricula, dbo.udf_getnumeric(d.NomeCodigo) as CNPJ, a.Obs as Observacao, ");
            strSQL.Append("    case when a.IdAfastamentoTipo is null then 1 ");
            strSQL.Append("         when a.IdAfastamentoTipo is not null then e.Codigo_eSocial ");
            strSQL.Append("    end as CodMotAfast, convert(char(10), DataInicial, 126) as DtInicial, ");
            strSQL.Append("    case when DataVolta is null then '' when DataVolta is not null then convert(char(10), DataVolta, 126) end as DtVolta, case when DataInicial is null or DataVolta is null then 0  when datediff(dd, DataInicial, DataVolta) > 32000 then 32000 else datediff(dd, DataInicial, DataVolta) end as Dias, ");
            strSQL.Append("    case when b.IdCID is null then -9999 when b.IdCID is not null then b.IdCID end as Cid1, ");
            strSQL.Append("    case when f.IdCID is null then -9999 when f.IdCID is not null then f.IdCID end as Cid2, ");
            strSQL.Append("    case when g.IdCID is null then -9999 when g.IdCID is not null then g.IdCID end as Cid3, ");
            strSQL.Append("    case when h.IdCID is null then -9999 when h.IdCID is not null then h.IdCID end as Cid4, ");
            strSQL.Append("    Atestado_Emitente, Atestado_ideOC, Atestado_nrOC, Atestado_ufOC ");
            strSQL.Append(" from afastamento as a left join CID as b ");
            strSQL.Append(" on(a.idcid = b.IdCID) ");
            strSQL.Append(" left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblempregado as c ");
            strSQL.Append(" on(a.idempregado = c.nid_empregado) ");
            strSQL.Append(" left join pessoa as d ");
            strSQL.Append(" on(c.nid_empr = d.idpessoa) ");
            strSQL.Append(" left join afastamentotipo as e ");
            strSQL.Append(" on(a.IdAfastamentoTipo = e.idAfastamentoTipo) ");
            strSQL.Append(" left join CID as f ");
            strSQL.Append(" on(a.idcid2 = f.IdCID) ");
            strSQL.Append(" left join CID as g ");
            strSQL.Append(" on(a.idcid3 = g.IdCID) ");
            strSQL.Append(" left join CID as h ");
            strSQL.Append(" on(a.idcid4 = h.IdCID) ");
            strSQL.Append(" where IdAfastamento =" + xIdAfastamento.ToString() + " ");



            strSQL.Append(" and tno_CPF is not null  and tno_CPF <> ''  ");

            //strSQL.Append(" and DataVolta is not null ");
            strSQL.Append(" order by c.tno_CPF, datainicial desc ");



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



        public DataSet Trazer_1005(Int32 xIdEmpresaGrupo)
        {
            //string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection();
            StringBuilder strSQL = new StringBuilder();

            //DataSet m_ds;


            DataTable table = new DataTable("Result");

            table.Columns.Add("CNAE", Type.GetType("System.String"));
            table.Columns.Add("FAP", Type.GetType("System.String"));
            table.Columns.Add("nrInsc", Type.GetType("System.String"));
            table.Columns.Add("IdPessoa", Type.GetType("System.String"));


            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);


            strSQL.Append("select a.IdPessoa, c.Codigo as CNAE, c.FAP, dbo.udf_getnumeric(a.NomeCodigo) as nrInsc from pessoa as a ");
            strSQL.Append("left join juridica as b on  ( a.idpessoa = b.idpessoa) ");
            strSQL.Append("left join cnae as c on(b.IdCNAE = c.IdCNAE) ");
            strSQL.Append("where a.IdPessoa in ( select IdPessoa from Juridica where IdGrupoEmpresa in ( select IdGrupoEmpresa from Juridica where Idpessoa =  " + xIdEmpresaGrupo.ToString() + " )  ) ");
            strSQL.Append("and isinativo = 0 ");

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





        public DataSet Trazer_1060(Int32 xLaudo, Int32 xCodAmb)
        {
            //string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection();
            StringBuilder strSQL = new StringBuilder();

            //DataSet m_ds;


            DataTable table = new DataTable("Result");

            table.Columns.Add("CodAmb", Type.GetType("System.String"));
            table.Columns.Add("NmAmb", Type.GetType("System.String"));
            table.Columns.Add("NrInsc", Type.GetType("System.String"));
            table.Columns.Add("DscAmb", Type.GetType("System.String"));

            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);


            strSQL.Append("select nid_func as CodAmb, tno_Func as NmAmb, dbo.udf_getnumeric(c.NomeCodigo) as nrInsc, convert( varchar(max), tds_local_trab  COLLATE sql_latin1_general_cp1251_ci_as ) as DscAmb    ");
            strSQL.Append("from tblfunc as a  ");
            strSQL.Append("left join  tblLaudo_tec as b on ( a.nid_Laud_tec = b.nId_Laud_tec ) ");
            strSQL.Append("left join  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.Pessoa as c on ( b.nId_Empr = c.IdPessoa )  ");
            strSQL.Append("where a.nid_laud_tec = " + xLaudo.ToString() + " ");
            strSQL.Append(" and a.nId_Func = " + xCodAmb.ToString() );



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


        public DataSet Trazer_1060(Int32 xIdEmpresa)
        {
            //string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection();
            StringBuilder strSQL = new StringBuilder();

            //DataSet m_ds;


            DataTable table = new DataTable("Result");

            table.Columns.Add("CodAmb", Type.GetType("System.String"));
            table.Columns.Add("NmAmb", Type.GetType("System.String"));
            table.Columns.Add("NrInsc", Type.GetType("System.String"));
            table.Columns.Add("DscAmb", Type.GetType("System.String"));

            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);


            strSQL.Append("select nid_func as CodAmb, tno_Func as NmAmb, dbo.udf_getnumeric(c.NomeCodigo) as nrInsc, convert( varchar(max), tds_local_trab  COLLATE sql_latin1_general_cp1251_ci_as ) as DscAmb    ");
            strSQL.Append("from tblfunc as a  ");
            strSQL.Append("left join  tblLaudo_tec as b on ( a.nid_Laud_tec = b.nId_Laud_tec ) ");
            strSQL.Append("left join  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.Pessoa as c on ( b.nId_Empr = c.IdPessoa )  ");
            strSQL.Append("where a.nid_laud_tec in ");
            strSQL.Append("(select nid_laud_tec from tbllaudo_tec ");
            strSQL.Append("   where convert(char(12), nid_empr) + ' ' + convert(char(10), hdt_laudo, 103) in ");
            strSQL.Append("   (select convert(char(12), nid_empr) + ' ' + convert(char(10), max(hdt_laudo), 103) ");
            strSQL.Append("      from tblLAUDO_TEC ");
            strSQL.Append("      where nid_empr = " + xIdEmpresa.ToString() + " ");
            strSQL.Append("      group by nid_empr ");
            strSQL.Append("   ) ");
            strSQL.Append(") ");



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







        public DataSet Trazer_2210(Int32 xIdEmpresa, Int32 xIdEmpregado, Int32 xIdEmpresaGrupo, string zData1, string zData2)
        {
            //string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection();
            StringBuilder strSQL = new StringBuilder();

            //DataSet m_ds;


            DataTable table = new DataTable("Result");

            table.Columns.Add("nId_Empregado", Type.GetType("System.Int32"));
            table.Columns.Add("CPF", Type.GetType("System.String"));
            table.Columns.Add("PIS", Type.GetType("System.String"));
            table.Columns.Add("CNPJ", Type.GetType("System.String"));
            table.Columns.Add("CNPJ_Acidente", Type.GetType("System.String"));
            table.Columns.Add("Matricula", Type.GetType("System.String"));
            table.Columns.Add("DtAcidente", Type.GetType("System.String"));
            table.Columns.Add("tpAcid", Type.GetType("System.String"));
            table.Columns.Add("hrAcid", Type.GetType("System.String"));
            table.Columns.Add("hrsTrabAntesAcid", Type.GetType("System.String"));
            table.Columns.Add("tpCat", Type.GetType("System.String"));
            table.Columns.Add("IndCatObito", Type.GetType("System.String"));
            table.Columns.Add("dtObito", Type.GetType("System.String"));

            table.Columns.Add("IndComumPolicia", Type.GetType("System.String"));
            table.Columns.Add("CodSitGeradora", Type.GetType("System.String"));
            table.Columns.Add("IniciatCat", Type.GetType("System.String"));
            table.Columns.Add("Observacao", Type.GetType("System.String"));
            table.Columns.Add("TpLocal", Type.GetType("System.String"));
            table.Columns.Add("DscLocal", Type.GetType("System.String"));
            table.Columns.Add("dscLograd", Type.GetType("System.String"));
            table.Columns.Add("NrLograd", Type.GetType("System.String"));
            table.Columns.Add("CodMunic", Type.GetType("System.String"));
            table.Columns.Add("UF", Type.GetType("System.String"));
            table.Columns.Add("cnpjLocalAcid", Type.GetType("System.String"));
            table.Columns.Add("Pais", Type.GetType("System.String"));
            table.Columns.Add("CodPostal", Type.GetType("System.String"));
            table.Columns.Add("CodParteAting", Type.GetType("System.String"));
            table.Columns.Add("Lateralidade", Type.GetType("System.String"));
            table.Columns.Add("CodAgntCausador", Type.GetType("System.String"));

            table.Columns.Add("Bairro", Type.GetType("System.String"));
            table.Columns.Add("CEP", Type.GetType("System.String"));
            table.Columns.Add("Complemento", Type.GetType("System.String"));

            table.Columns.Add("CNES", Type.GetType("System.String"));
            table.Columns.Add("DtAtendimento", Type.GetType("System.String"));
            table.Columns.Add("hrAtendimento", Type.GetType("System.String"));
            table.Columns.Add("IntInternacao", Type.GetType("System.String"));
            table.Columns.Add("DurTrat", Type.GetType("System.String"));
            table.Columns.Add("IndAFast", Type.GetType("System.String"));
            table.Columns.Add("dscLesao", Type.GetType("System.String"));
            table.Columns.Add("dscCompLesao", Type.GetType("System.String"));
            table.Columns.Add("DiagnosticoProvavel", Type.GetType("System.String"));
            table.Columns.Add("CodCID", Type.GetType("System.String"));

            table.Columns.Add("nmEmit", Type.GetType("System.String"));
            table.Columns.Add("nrOC", Type.GetType("System.String"));
            table.Columns.Add("UfOC", Type.GetType("System.String"));
            table.Columns.Add("dtCatOrig", Type.GetType("System.String"));
            table.Columns.Add("nrCatOrig", Type.GetType("System.String"));
            table.Columns.Add("IdAcidente", Type.GetType("System.String"));

            table.Columns.Add("CNPJ_Terceiro", Type.GetType("System.String"));

            table.Columns.Add("CAEPF", Type.GetType("System.String"));
            table.Columns.Add("CNO", Type.GetType("System.String"));
            table.Columns.Add("IdPessoa", Type.GetType("System.String"));

            table.Columns.Add("Reabertura", Type.GetType("System.String"));
            table.Columns.Add("nrRecCatOrig", Type.GetType("System.String"));

            table.Columns.Add("nId_Empr", Type.GetType("System.String"));



            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);


            strSQL.Append(" select a.IdAcidente, dbo.udf_getnumeric(c.tno_CPF) as CPF, c.nNO_PIS_PASEP as PIS,  c.tCod_Empr as Matricula, dbo.udf_getnumeric(d.NomeCodigo) as CNPJ,  convert( char(10), DataAcidente, 126 ) as DtAcidente,  ");
            strSQL.Append("        codigo_acidente_trabalho as tpAcid, CONVERT(VARCHAR(5), DataAcidente, 108) as hrAcid,   hrsTrabAntesAcid,  ");
            strSQL.Append("        b.IndTipoCAT as tpCat,  case when b.hasMorte = 0 then 'N'  when b.hasMorte = 1 then 'S' end as IndCatObito,  ");
            strSQL.Append(" 	   case when b.DataObito is null then ''  when b.DataObito is not null then convert(char(10), b.DataObito, 126) end as dtObito,  ");
            strSQL.Append(" 	   case when hasregPolicial = 0 then 'N'  when hasregPolicial = 1 then 'S' end as IndComumPolicia,  ");
            strSQL.Append("        a.codigo_agente_Causador as CodSitGeradora, IdIniciativaCat as IniciatCat, Observacoes as Observacao, IdTipoLocal as TpLocal, DscLocal,  ");
            strSQL.Append("        a.Bairro, a.CEP, substring(a.Complemento,1,30) as Complemento, a.Logradouro as dscLograd, a.Nr_Logradouro as NrLograd, a.Municipio as CodMunic, a.UF as UF,  ");
            strSQL.Append(" 	   case when IdlocalAcidente is null then  dbo.udf_getnumeric(d.NomeCodigo)  when IdLocalAcidente is not null then dbo.udf_getnumeric(e.NomeCodigo) end as cnpjLocalAcid, '' as pais,  dbo.udf_getnumeric(CEP) as CodPostal,  ");
            strSQL.Append("        case when a.Codigo_Parte_Corpo_Atingida is null then '' else a.Codigo_Parte_Corpo_Atingida end as CodParteAting, IdLateralidade as lateralidade, a.Codigo_Situacao_Geradora as CodAgntCausador,  ");
            strSQL.Append(" 	   case when CNES is null then '' when CNES is not null then CNES end as CNES,  ");
            strSQL.Append(" 	   case when DataInternacao is null then '' when DataInternacao is not null then convert(char(10), DataInternacao, 126) end as DtAtendimento,  ");
            strSQL.Append(" 	   case when DataInternacao is null then '0000' when DataInternacao is not null then convert(varchar(5), DataInternacao, 108) end as hrAtendimento,  ");
            strSQL.Append(" 	   case when HasInternacao = 0 then 'N' when HasInternacao = 1 then 'S' end as IntInternacao, DuracaoInternacao as DurTrat,  ");
            strSQL.Append(" 	   case when hasAfastamento = 0 then 'N' when hasAfastamento = 1 then 'S' end as IndAFast, a.codigo_descricao_lesao as dscLesao, a.Descricao as dscCompLesao,  ");
            strSQL.Append(" 	   case when f.CodigoCid is null then '' when f.CodigoCid is not null then f.CodigoCid end as CodCID, MedicoInternacao as nmEmit, 1 as IdeOC, CRMInternacao as nrOC,  ");
            strSQL.Append("        UfInternacao as UfOC, convert(char(10), DataEmissao, 126) as dtCatOrig, NumeroCAT as nrCatOrig, DiagnosticoProvavel, c.nId_Empregado, dbo.udf_getnumeric(g.NomeCodigo) as CNPJ_Acidente, dbo.udf_getnumeric(isnull(CNPJ_Terceiro,'')) as CNPJ_Terceiro, dbo.udf_getnumeric(isnull( jur.CEI,'0')) as CAEPF, dbo.udf_getnumeric(isnull( jur.CNO,'0')) as CNO,d.IdPessoa,  ");
            strSQL.Append("        case when Reabertura=1 then 'Sim' else 'Não' end as Reabertura, nrRecCatOrig, d.IdPessoa as nId_Empr ");

            strSQL.Append(" from acidente as a left join cat as b  ");
            strSQL.Append(" on(a.idcat = b.idcat)  ");
            strSQL.Append(" left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblempregado as c  ");
            strSQL.Append(" on(a.idempregado = c.nid_empregado)  ");
            strSQL.Append(" left join pessoa as d ");
            strSQL.Append(" on(c.nid_empr = d.idpessoa) ");
            strSQL.Append(" left join juridica as jur  on(jur.idpessoa = d.idpessoa) ");
            strSQL.Append(" left join Pessoa as e ");
            strSQL.Append(" on(IdLocalAcidente = e.IdPessoa) ");
            strSQL.Append(" left join Cid as f ");
            strSQL.Append(" on(a.idcid = f.idcid) ");
            strSQL.Append(" left join Pessoa as g ");
            strSQL.Append(" on a.idjuridica = g.idpessoa ");


            //left join sied_novo_prajna_hom.dbo.tbl_eSocial_13_Parte_Corpo_Atingida as d
            //on(a.Codigo_Parte_Corpo_Atingida = d.Codigo)
            //left join SIED_NOVO_PRAJNA_Hom.dbo.tbl_eSocial_16_Situacao_Geradora_Acidente_Trabalho as e
            //on(a.codigo_agente_Causador = e.Codigo)
            //left join SIED_NOVO_PRAJNA_hom.dbo.tbl_eSocial_17_Natureza_Lesao as f
            //on(a.codigo_descricao_lesao = f.codigo)
            //left join SIED_NOVO_PRAJNA_Hom.dbo.tbl_eSocial_15_Agente_Situacao_Doenca_Profissional as g
            //on(a.Codigo_Situacao_Geradora = g.codigo)

            strSQL.Append(" where a.IdEmpregado in ");

            if (xIdEmpresa != 0)
            {
                strSQL.Append(" (select nId_Empregado from  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEMPREGADO ");
                strSQL.Append(" where nid_Empr = " + xIdEmpresa.ToString() + " ");
                strSQL.Append(" ) ");
            }
            else if (xIdEmpregado != 0)
            {
                strSQL.Append(" ( " + xIdEmpregado.ToString() + " ) ");
            }
            else if (xIdEmpresaGrupo != 0)
            {
                strSQL.Append(" (select nId_Empregado from  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEMPREGADO ");
                strSQL.Append(" where nid_Empr in ( select IdPessoa from Juridica where IdGrupoEmpresa in ( select IdGrupoEmpresa from Juridica where Idpessoa =  " + xIdEmpresaGrupo.ToString() + " )  ) ");
                strSQL.Append(" ) ");
            }


            strSQL.Append(" and a.IdCat is not null and tno_CPF is not null  and tno_CPF <> ''    and a.DataAcidente between convert( smalldatetime, '" + zData1 + "', 103 ) and convert( smalldatetime, '" + zData2 + "', 103 )  ");
            strSQL.Append(" order by c.tno_CPF, dataAcidente desc ");



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



        public DataSet Trazer_2210(Int32 xIdAcidente)
        {
            //string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection();
            StringBuilder strSQL = new StringBuilder();

            //DataSet m_ds;


            DataTable table = new DataTable("Result");

            table.Columns.Add("nId_Empregado", Type.GetType("System.Int32"));
            table.Columns.Add("CPF", Type.GetType("System.String"));
            table.Columns.Add("PIS", Type.GetType("System.String"));
            table.Columns.Add("CNPJ", Type.GetType("System.String"));
            table.Columns.Add("CNPJ_Acidente", Type.GetType("System.String"));
            table.Columns.Add("Matricula", Type.GetType("System.String"));
            table.Columns.Add("DtAcidente", Type.GetType("System.String"));
            table.Columns.Add("tpAcid", Type.GetType("System.String"));
            table.Columns.Add("hrAcid", Type.GetType("System.String"));
            table.Columns.Add("hrsTrabAntesAcid", Type.GetType("System.String"));
            table.Columns.Add("tpCat", Type.GetType("System.String"));
            table.Columns.Add("IndCatObito", Type.GetType("System.String"));
            table.Columns.Add("dtObito", Type.GetType("System.String"));

            table.Columns.Add("IndComumPolicia", Type.GetType("System.String"));
            table.Columns.Add("CodSitGeradora", Type.GetType("System.String"));
            table.Columns.Add("IniciatCat", Type.GetType("System.String"));
            table.Columns.Add("Observacao", Type.GetType("System.String"));
            table.Columns.Add("TpLocal", Type.GetType("System.String"));
            table.Columns.Add("DscLocal", Type.GetType("System.String"));
            table.Columns.Add("dscLograd", Type.GetType("System.String"));
            table.Columns.Add("NrLograd", Type.GetType("System.String"));
            table.Columns.Add("CodMunic", Type.GetType("System.String"));
            table.Columns.Add("UF", Type.GetType("System.String"));
            table.Columns.Add("cnpjLocalAcid", Type.GetType("System.String"));
            table.Columns.Add("Pais", Type.GetType("System.String"));
            table.Columns.Add("CodPostal", Type.GetType("System.String"));
            table.Columns.Add("CodParteAting", Type.GetType("System.String"));
            table.Columns.Add("Lateralidade", Type.GetType("System.String"));
            table.Columns.Add("CodAgntCausador", Type.GetType("System.String"));

            table.Columns.Add("Bairro", Type.GetType("System.String"));
            table.Columns.Add("CEP", Type.GetType("System.String"));
            table.Columns.Add("Complemento", Type.GetType("System.String"));

            table.Columns.Add("CNES", Type.GetType("System.String"));
            table.Columns.Add("DtAtendimento", Type.GetType("System.String"));
            table.Columns.Add("hrAtendimento", Type.GetType("System.String"));
            table.Columns.Add("IntInternacao", Type.GetType("System.String"));
            table.Columns.Add("DurTrat", Type.GetType("System.String"));
            table.Columns.Add("IndAFast", Type.GetType("System.String"));
            table.Columns.Add("dscLesao", Type.GetType("System.String"));
            table.Columns.Add("dscCompLesao", Type.GetType("System.String"));
            table.Columns.Add("DiagnosticoProvavel", Type.GetType("System.String"));
            table.Columns.Add("CodCID", Type.GetType("System.String"));

            table.Columns.Add("nmEmit", Type.GetType("System.String"));
            table.Columns.Add("nrOC", Type.GetType("System.String"));
            table.Columns.Add("UfOC", Type.GetType("System.String"));
            table.Columns.Add("dtCatOrig", Type.GetType("System.String"));
            table.Columns.Add("nrCatOrig", Type.GetType("System.String"));
            table.Columns.Add("IdAcidente", Type.GetType("System.String"));
            table.Columns.Add("IndTipoAcidente", Type.GetType("System.String"));

            table.Columns.Add("CNPJ_Terceiro", Type.GetType("System.String"));

            table.Columns.Add("CAEPF", Type.GetType("System.String"));
            table.Columns.Add("CNO", Type.GetType("System.String"));
            table.Columns.Add("IdPessoa", Type.GetType("System.String"));

            table.Columns.Add("Reabertura", Type.GetType("System.String"));
            table.Columns.Add("nrRecCatOrig", Type.GetType("System.String"));

            table.Columns.Add("nId_Empr", Type.GetType("System.String"));

            table.Columns.Add("UltimoDiaTrab", Type.GetType("System.String"));

            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);


            strSQL.Append(" select a.IdAcidente, a.IndTipoAcidente, dbo.udf_getnumeric(c.tno_CPF) as CPF, c.nNO_PIS_PASEP as PIS,  c.tCod_Empr as Matricula, dbo.udf_getnumeric(d.NomeCodigo) as CNPJ,  convert( char(10), DataAcidente, 126 ) as DtAcidente,  ");
            strSQL.Append("        codigo_acidente_trabalho as tpAcid, CONVERT(VARCHAR(5), DataAcidente, 108) as hrAcid, hrsTrabAntesAcid,  ");
            strSQL.Append("        isnull(b.IndTipoCAT,1) as tpCat,  case when b.hasMorte = 0 then 'N'  when b.hasMorte = 1 then 'S' end as IndCatObito,  ");
            strSQL.Append(" 	   case when b.DataObito is null then ''  when b.DataObito is not null then convert(char(10), b.DataObito, 126) end as dtObito,  ");
            strSQL.Append(" 	   case when hasregPolicial = 0 then 'N'  when hasregPolicial = 1 then 'S' end as IndComumPolicia,  ");
            strSQL.Append("        a.codigo_situacao_geradora as CodSitGeradora, IdIniciativaCat as IniciatCat, Observacoes as Observacao, case when IdTipoLocal=0 then 1 else IdTipoLocal end as TpLocal, DscLocal,  ");
            strSQL.Append("        a.Bairro, a.CEP, substring(a.Complemento,1,30) as Complemento, a.Logradouro as dscLograd, a.Nr_Logradouro as NrLograd, a.Municipio as CodMunic, a.UF as UF,  ");
            strSQL.Append(" 	   case when IdlocalAcidente is null then  dbo.udf_getnumeric(d.NomeCodigo)  when IdLocalAcidente is not null then dbo.udf_getnumeric(e.NomeCodigo) end as cnpjLocalAcid, '' as pais, CodPostal,  ");
            strSQL.Append("        case when a.Codigo_Parte_Corpo_Atingida is null then '' else a.Codigo_Parte_Corpo_Atingida end as CodParteAting, IdLateralidade as lateralidade, a.codigo_agente_Causador as CodAgntCausador,  ");
            strSQL.Append(" 	   case when CNES is null then '' when CNES is not null then CNES end as CNES,  ");
            strSQL.Append(" 	   case when DataInternacao is null then '' when DataInternacao is not null then convert(char(10), DataInternacao, 126) end as DtAtendimento,  ");
            strSQL.Append(" 	   case when DataInternacao is null then '0000' when DataInternacao is not null then convert(varchar(5), DataInternacao, 108) end as hrAtendimento,  ");
            strSQL.Append(" 	   case when HasInternacao = 0 then 'N' when HasInternacao = 1 then 'S' end as IntInternacao, DuracaoInternacao as DurTrat,  ");
            strSQL.Append(" 	   case when hasAfastamento = 0 then 'N' when hasAfastamento = 1 then 'S' end as IndAFast, a.codigo_descricao_lesao as dscLesao, a.Descricao as dscCompLesao,  ");
            strSQL.Append(" 	   case when f.CodigoCid is null then '' when f.CodigoCid is not null then f.CodigoCid end as CodCID, MedicoInternacao as nmEmit, 1 as IdeOC, CRMInternacao as nrOC,  ");
            strSQL.Append("        UfInternacao as UfOC, convert(char(10), DataEmissao, 126) as dtCatOrig, NumeroCAT as nrCatOrig, DiagnosticoProvavel, c.nId_Empregado, dbo.udf_getnumeric(isnull(g.NomeCodigo,'')) as CNPJ_Acidente, dbo.udf_getnumeric(isnull(CNPJ_Terceiro,'')) as CNPJ_Terceiro, dbo.udf_getnumeric(isnull( jur.CEI,'0')) as CAEPF, dbo.udf_getnumeric(isnull( jur.CNO,'0')) as CNO, d.IdPessoa,   ");
            strSQL.Append("        case when Reabertura=1 then 'Sim' else 'Não' end as Reabertura, nrRecCatOrig, d.IdPessoa as nId_Empr, ");
            strSQL.Append("        case when a.UltDiaTrab is null then convert( char(10), DataAcidente, 126 ) when datepart( yyyy,a.UltDiaTrab ) < 2020 then convert( char(10), DataAcidente, 126 ) else a.UltDiaTrab end as UltimoDiaTrab ");

            strSQL.Append(" from acidente as a left join cat as b  ");
            strSQL.Append(" on(a.idcat = b.idcat)  ");
            strSQL.Append(" left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblempregado as c  ");
            strSQL.Append(" on(a.idempregado = c.nid_empregado)  ");
            //strSQL.Append(" left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblempregado_funcao as ef on(c.nId_Empregado = ef.nId_Empregado) ");
            strSQL.Append(" left join pessoa as d ");
            strSQL.Append(" on c.nid_empr = d.idpessoa ");
            strSQL.Append(" left join juridica as jur  on(jur.idpessoa = d.idpessoa) ");
            strSQL.Append(" left join Pessoa as e ");
            strSQL.Append(" on IdLocalAcidente = e.IdPessoa  ");
            strSQL.Append(" left join Cid as f ");
            strSQL.Append(" on a.idcid = f.idcid ");
            strSQL.Append(" left join Pessoa as g ");
            strSQL.Append(" on a.idjuridica = g.idpessoa ");



            strSQL.Append(" where a.IdAcidente = " + xIdAcidente.ToString());

            strSQL.Append(" order by c.tno_CPF, dataAcidente desc ");



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






        public DataSet Trazer_2245(Int32 xIdEmpresa, Int32 xIdEmpregado, Int32 xIdEmpresaGrupo, string zData1, string zData2)
        {
            //string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection();
            StringBuilder strSQL = new StringBuilder();

            //DataSet m_ds;


            DataTable table = new DataTable("Result");

            table.Columns.Add("CPF", Type.GetType("System.String"));
            table.Columns.Add("PIS", Type.GetType("System.String"));
            table.Columns.Add("CNPJ", Type.GetType("System.String"));
            table.Columns.Add("Matricula", Type.GetType("System.String"));
            table.Columns.Add("DtTreiCap", Type.GetType("System.String"));
            table.Columns.Add("CodTreiCap", Type.GetType("System.String"));
            table.Columns.Add("durTreiCap", Type.GetType("System.String"));
            table.Columns.Add("ModTreiCap", Type.GetType("System.String"));
            table.Columns.Add("tpTreiCap", Type.GetType("System.String"));
            table.Columns.Add("CPFProf", Type.GetType("System.String"));
            table.Columns.Add("nmProf", Type.GetType("System.String"));
            table.Columns.Add("tpProf", Type.GetType("System.String"));
            table.Columns.Add("FormProf", Type.GetType("System.String"));
            table.Columns.Add("codCBO", Type.GetType("System.String"));
            table.Columns.Add("IdTreinamento", Type.GetType("System.String"));

            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);


            strSQL.Append(" select a.IdTreinamento, dbo.udf_getnumeric(c.tno_CPF) as CPF, c.nNO_PIS_PASEP as PIS,  c.tCod_Empr as Matricula, dbo.udf_getnumeric(d.NomeCodigo) as CNPJ, ");
            strSQL.Append(" f.Codigo_eSocial as CodTreiCap, convert(char(10), g.DataLevantamento, 126) as dtTreiCap, ");
            strSQL.Append(" a.Carga_Horaria as durTreiCap, '1' as ModTreiCap, tpTreiCap, codCBO, ");
            strSQL.Append(" case when h.CPF is null then '' else dbo.udf_getnumeric(h.CPF) end as CPFProf, j.NomeCompleto as nmProf, '1' as tpProf, h.titulo as FormProf ");
            strSQL.Append(" from treinamento as a ");
            strSQL.Append(" left join ParticipanteTreinamento as b on(a.idTreinamento = b.IdTreinamento) ");
            strSQL.Append(" left join  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblempregado as c on(b.idEmpregado = c.nid_empregado) ");
            strSQL.Append(" left join pessoa as d on(c.nid_empr = d.idpessoa) ");
            strSQL.Append(" left join TreinamentoDicionario as f on(a.IdTreinamentoDicionario = f.IdTreinamentoDicionario) ");
            strSQL.Append(" left join Documento as g on(a.idDocumento = g.IdDocumento) ");
            strSQL.Append(" left join prestador as h on(a.IdResponsavel = h.idprestador) ");
            strSQL.Append(" left join juridicapessoa as i on(h.idjuridicapessoa = i.idjuridicapessoa) ");
            strSQL.Append(" left join pessoa as j on(i.idpessoa = j.idpessoa) ");
            strSQL.Append(" where c.tno_cpf is not null ");

            strSQL.Append(" and g.DataLevantamento between convert( smalldatetime, '" + zData1 + "',103 ) and convert( smalldatetime,'" + zData2 + "', 103 ) ");


            strSQL.Append(" and c.nId_Empregado in ");

            if (xIdEmpresa != 0)
            {
                strSQL.Append(" (select nId_Empregado from  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEMPREGADO ");
                strSQL.Append(" where nid_Empr = " + xIdEmpresa.ToString() + " ");
                strSQL.Append(" ) ");
            }
            else if (xIdEmpregado != 0)
            {
                strSQL.Append(" ( " + xIdEmpregado.ToString() + " ) ");
            }
            else if (xIdEmpresaGrupo != 0)
            {
                strSQL.Append(" (select nId_Empregado from  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEMPREGADO ");
                strSQL.Append(" where nid_Empr in ( select IdPessoa from Juridica where IdGrupoEmpresa in ( select IdGrupoEmpresa from Juridica where Idpessoa =  " + xIdEmpresaGrupo.ToString() + " )  ) ");
                strSQL.Append(" ) ");
            }

            strSQL.Append(" order by c.tno_CPF, dataLevantamento desc ");



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




        public DataSet Trazer_2245(Int32 xIdParticipanteTreinamento)
        {
            //string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection();
            StringBuilder strSQL = new StringBuilder();

            //DataSet m_ds;


            DataTable table = new DataTable("Result");

            table.Columns.Add("CPF", Type.GetType("System.String"));
            table.Columns.Add("PIS", Type.GetType("System.String"));
            table.Columns.Add("CNPJ", Type.GetType("System.String"));
            table.Columns.Add("Matricula", Type.GetType("System.String"));
            table.Columns.Add("DtTreiCap", Type.GetType("System.String"));
            table.Columns.Add("CodTreiCap", Type.GetType("System.String"));
            table.Columns.Add("durTreiCap", Type.GetType("System.String"));
            table.Columns.Add("ModTreiCap", Type.GetType("System.String"));
            table.Columns.Add("tpTreiCap", Type.GetType("System.String"));
            table.Columns.Add("CPFProf", Type.GetType("System.String"));
            table.Columns.Add("nmProf", Type.GetType("System.String"));
            table.Columns.Add("tpProf", Type.GetType("System.String"));
            table.Columns.Add("FormProf", Type.GetType("System.String"));
            table.Columns.Add("codCBO", Type.GetType("System.String"));
            table.Columns.Add("IdParticanteTreinamento", Type.GetType("System.String"));
            
            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);


            strSQL.Append(" select b.IdParticipanteTreinamento, dbo.udf_getnumeric(c.tno_CPF) as CPF, c.nNO_PIS_PASEP as PIS,  c.tCod_Empr as Matricula, dbo.udf_getnumeric(d.NomeCodigo) as CNPJ, ");
            strSQL.Append(" f.Codigo_eSocial as CodTreiCap, convert(char(10), g.DataLevantamento, 126) as dtTreiCap, ");
            strSQL.Append(" a.Carga_Horaria as durTreiCap, '1' as ModTreiCap, tpTreiCap, codCBO, ");
            strSQL.Append(" case when h.CPF is null then '' else dbo.udf_getnumeric(h.CPF) end as CPFProf, j.NomeCompleto as nmProf, '1' as tpProf, h.titulo as FormProf ");
            strSQL.Append(" from treinamento as a ");
            strSQL.Append(" left join ParticipanteTreinamento as b on(a.idTreinamento = b.IdTreinamento) ");
            strSQL.Append(" left join  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblempregado as c on(b.idEmpregado = c.nid_empregado) ");
            strSQL.Append(" left join pessoa as d on(c.nid_empr = d.idpessoa) ");
            strSQL.Append(" left join TreinamentoDicionario as f on(a.IdTreinamentoDicionario = f.IdTreinamentoDicionario) ");
            strSQL.Append(" left join Documento as g on(a.idDocumento = g.IdDocumento) ");
            strSQL.Append(" left join prestador as h on(a.IdResponsavel = h.idprestador) ");
            strSQL.Append(" left join juridicapessoa as i on(h.idjuridicapessoa = i.idjuridicapessoa) ");
            strSQL.Append(" left join pessoa as j on(i.idpessoa = j.idpessoa) ");
            strSQL.Append(" where c.tno_cpf is not null ");

            
            strSQL.Append(" and b.IdParticipanteTreinamento = " + xIdParticipanteTreinamento.ToString() + " " );

          

            strSQL.Append(" order by c.tno_CPF, dataLevantamento desc ");



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











        public DataSet Trazer_2220(Int32 xIdEmpresa, Int32 xIdEmpregado, Int32 xIdEmpresaGrupo, string zData1, string zData2)
        {
            //string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection();
            StringBuilder strSQL = new StringBuilder();

            //DataSet m_ds;


            DataTable table = new DataTable("Result");

            table.Columns.Add("CPF", Type.GetType("System.String"));
            table.Columns.Add("PIS", Type.GetType("System.String"));
            table.Columns.Add("CNPJ", Type.GetType("System.String"));
            table.Columns.Add("Matricula", Type.GetType("System.String"));
            table.Columns.Add("tpASO", Type.GetType("System.String"));
            table.Columns.Add("resASO", Type.GetType("System.String"));
            table.Columns.Add("dtInitMonit", Type.GetType("System.String"));
            table.Columns.Add("dtFimMonit", Type.GetType("System.String"));
            table.Columns.Add("nId_Func", Type.GetType("System.Int32"));
            table.Columns.Add("NisResp", Type.GetType("System.String"));
            table.Columns.Add("NrConsClasse", Type.GetType("System.String"));
            table.Columns.Add("IdClinico", Type.GetType("System.Int32"));
            table.Columns.Add("Data_Exame", Type.GetType("System.String"));
            table.Columns.Add("NrCRM", Type.GetType("System.String"));
            table.Columns.Add("NmMed", Type.GetType("System.String"));
            table.Columns.Add("NITMed", Type.GetType("System.String"));
            table.Columns.Add("UFMed", Type.GetType("System.String"));
            table.Columns.Add("RespNome", Type.GetType("System.String"));
            table.Columns.Add("RespUF", Type.GetType("System.String"));
            table.Columns.Add("RespCPF", Type.GetType("System.String"));
            table.Columns.Add("IdExameBase", Type.GetType("System.String"));
            table.Columns.Add("nId_Empr", Type.GetType("System.Int32"));
            table.Columns.Add("IdPCMSO", Type.GetType("System.Int32"));
            table.Columns.Add("CAEPF", Type.GetType("System.String"));
            table.Columns.Add("Data_Admissao", Type.GetType("System.String"));

            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);


            strSQL.Append(" select ef.nId_Empr, em.IdExameBase, dbo.udf_getnumeric(c.tno_CPF) as CPF, c.nNO_PIS_PASEP as PIS,  c.tCod_Empr as Matricula, dbo.udf_getnumeric(d.NomeCodigo) as CNPJ,    ");
            strSQL.Append("  case when em.IdExameDicionario = 1 then '0'  when em.IdExameDicionario = 2 then '9' when em.IdExameDicionario = 3 then '3' when em.IdExameDicionario = 4 then '1'  when em.IdExameDicionario = 5 then '2' end as tpASO, ");
            strSQL.Append("  em.IndResultado as resASO, convert(char(10), p.datapcmso, 126) as dtInitMonit, ");
            strSQL.Append("  case when  p.TerminoPcmso is null then '' when p.TerminoPcmso is not null then convert( ");
            strSQL.Append(" char(10), p.terminopcmso, 126) end as dtFimMonit, fe.nid_Func, dbo.udf_getnumeric(z.NomeCodigo) as NisResp, w.Numero as nrConsClasse, w.uf as RespUF, z.NomeCompleto as RespNome, r.IdClinico, convert( char(10), Em.DataExame, 126 ) as Data_Exame, ");
            strSQL.Append(" case when rw.Numero is null then '' when rw.Numero is not null then rw.Numero end as NrCRM,   ");
            strSQL.Append(" case when rz.NomeCompleto is null then '' when rz.NomeCompleto is not null then rz.NomeCompleto end as NmMed, dbo.udf_getnumeric(rz.NomeCodigo) as NITMed, rw.UF as UFMed, dbo.udf_getnumeric(w.CPF) as RespCPF, p.IdPCMSO, dbo.udf_getnumeric(isnull( jur.CEI,'0')) as CAEPF, convert( char(10), c.hDt_Adm, 126 ) as Data_Admissao  ");

            strSQL.Append("  from examebase as em ");
            strSQL.Append("  left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblempregado as c  on(em.idempregado =c.nid_empregado) ");            
            strSQL.Append("  left join clinico as r on(em.IdExameBase = r.IdExameBase) ");
            strSQL.Append("  left join pcmso as p on(r.IdPcmso = p.IdPcmso) ");
            strSQL.Append("  left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblFUNC_EMPREGADO as FE  on(r.IdEmpregadoFuncao = fe.nID_EMPREGADO_FUNCAO and fe.nID_LAUD_TEC = p.IdLaudoTecnico) ");
            strSQL.Append("  left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEMPREGADO_FUNCAO as EF  on(r.IdEmpregadoFuncao = ef.nID_EMPREGADO_FUNCAO ) ");
            strSQL.Append("  left join pessoa as d  on(ef.nid_empr = d.idpessoa) ");
            strSQL.Append("  left join juridica as jur  on(jur.idpessoa = d.idpessoa) ");

            //            strSQL.Append("  left join pcmsoplanejamento as s on(s.idpcmso = p.idpcmso and s.IdGHE = fe.nID_FUNC) ");
            //            strSQL.Append("  left join examedicionario as t on(s.idexamedicionario = t.IdExameDicionario) ");
            strSQL.Append("  left join medico as v on(p.IdCoordenador = v.IdMedico) ");
            strSQL.Append("  left join prestador as w on(v.IdPrestador = w.IdPrestador) ");
            strSQL.Append("  left join juridicapessoa as x on(w.IdJuridicaPessoa = x.IdJuridicaPessoa) ");
            strSQL.Append("  left join pessoa as z on(x.IdPessoa = z.IdPessoa) ");
            strSQL.Append("  left join medico as rv on(em.IdMedico = rv.IdMedico) ");
            strSQL.Append("  left join prestador as rw on(rv.IdPrestador = rw.IdPrestador) ");
            strSQL.Append("  left join juridicapessoa as rx on(rw.IdJuridicaPessoa = rx.IdJuridicaPessoa) ");
            strSQL.Append("  left join pessoa as rz on(rx.IdPessoa = rz.IdPessoa) ");

            strSQL.Append(" where em.IdEmpregado in ");

            if (xIdEmpresa != 0)
            {
                strSQL.Append(" (select nId_Empregado from  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEMPREGADO_Funcao ");
                strSQL.Append(" where nid_Empr = " + xIdEmpresa.ToString() + " ");
                strSQL.Append(" ) ");
            }
            else if (xIdEmpregado != 0)
            {
                strSQL.Append(" ( " + xIdEmpregado.ToString() + " ) ");
            }
            else if (xIdEmpresaGrupo != 0)
            {
                strSQL.Append(" (select nId_Empregado from  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEMPREGADO_Funcao ");
                strSQL.Append(" where nid_Empr in ( select IdPessoa from Juridica where IdGrupoEmpresa in ( select IdGrupoEmpresa from Juridica where Idpessoa =  " + xIdEmpresaGrupo.ToString() + " )  ) ");
                strSQL.Append(" ) ");
            }


            strSQL.Append(" and em.idexameDicionario between  1 and 5 ");
            strSQL.Append(" and em.IndResultado not in (0,3 ) ");

            strSQL.Append(" and tno_CPF is not null  and tno_CPF <> '' and fe.nId_Func is not null  and em.DataExame between convert( smalldatetime, '" + zData1 + "', 103 ) and convert( smalldatetime, '" + zData2 + "', 103 )  ");

            //strSQL.Append(" and DataVolta is not null ");
            strSQL.Append(" order by c.tno_CPF, dataExame desc ");



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




        public DataSet Trazer_2220(Int32 xIdExameBase)
        {
            //string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection();
            StringBuilder strSQL = new StringBuilder();

            //DataSet m_ds;


            DataTable table = new DataTable("Result");

            table.Columns.Add("CPF", Type.GetType("System.String"));
            table.Columns.Add("PIS", Type.GetType("System.String"));
            table.Columns.Add("CNPJ", Type.GetType("System.String"));
            table.Columns.Add("Matricula", Type.GetType("System.String"));
            table.Columns.Add("tpASO", Type.GetType("System.String"));
            table.Columns.Add("resASO", Type.GetType("System.String"));
            table.Columns.Add("dtInitMonit", Type.GetType("System.String"));
            table.Columns.Add("dtFimMonit", Type.GetType("System.String"));
            table.Columns.Add("nId_Func", Type.GetType("System.Int32"));
            table.Columns.Add("NisResp", Type.GetType("System.String"));
            table.Columns.Add("NrConsClasse", Type.GetType("System.String"));
            table.Columns.Add("IdClinico", Type.GetType("System.Int32"));
            table.Columns.Add("Data_Exame", Type.GetType("System.String"));
            table.Columns.Add("NrCRM", Type.GetType("System.String"));
            table.Columns.Add("NmMed", Type.GetType("System.String"));
            table.Columns.Add("NITMed", Type.GetType("System.String"));
            table.Columns.Add("UFMed", Type.GetType("System.String"));
            table.Columns.Add("RespNome", Type.GetType("System.String"));
            table.Columns.Add("RespUF", Type.GetType("System.String"));
            table.Columns.Add("RespCPF", Type.GetType("System.String"));
            table.Columns.Add("IdExameBase", Type.GetType("System.String"));
            table.Columns.Add("nId_Empr", Type.GetType("System.Int32"));
            table.Columns.Add("IdPCMSO", Type.GetType("System.Int32"));
            table.Columns.Add("CAEPF", Type.GetType("System.String"));
            table.Columns.Add("Data_Admissao", Type.GetType("System.String"));

            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);


            strSQL.Append(" select ef.nId_Empr,  em.IdExameBase, dbo.udf_getnumeric(c.tno_CPF) as CPF, c.nNO_PIS_PASEP as PIS,  c.tCod_Empr as Matricula, dbo.udf_getnumeric(d.NomeCodigo) as CNPJ,    ");
            strSQL.Append("  case when em.IdExameDicionario = 1 then '0'  when em.IdExameDicionario = 2 then '9' when em.IdExameDicionario = 3 then '3' when em.IdExameDicionario = 4 then '1'  when em.IdExameDicionario = 5 then '2' end as tpASO, ");
            strSQL.Append("  em.IndResultado as resASO, convert(char(10), p.datapcmso, 126) as dtInitMonit, ");
            strSQL.Append("  case when  p.TerminoPcmso is null then '' when p.TerminoPcmso is not null then convert( ");
            strSQL.Append(" char(10), p.terminopcmso, 126) end as dtFimMonit, fe.nid_Func, dbo.udf_getnumeric(z.NomeCodigo) as NisResp, w.Numero as nrConsClasse, w.uf as RespUF, z.NomeCompleto as RespNome, r.IdClinico, convert( char(10), Em.DataExame, 126 ) as Data_Exame, ");
            strSQL.Append(" case when rw.Numero is null then '' when rw.Numero is not null then rw.Numero end as NrCRM,   ");
            strSQL.Append(" case when rz.NomeCompleto is null then '' when rz.NomeCompleto is not null then rz.NomeCompleto end as NmMed, dbo.udf_getnumeric(rz.NomeCodigo) as NITMed, rw.UF as UFMed, dbo.udf_getnumeric(w.CPF) as RespCPF, p.IdPCMSO, dbo.udf_getnumeric(isnull( jur.CEI,'0')) as CAEPF, convert( char(10), c.hDt_Adm, 126 ) as Data_Admissao  ");

            strSQL.Append("  from examebase as em ");
            strSQL.Append("  left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblempregado as c  on(em.idempregado =c.nid_empregado) ");            
            strSQL.Append("  left join clinico as r on(em.IdExameBase = r.IdExameBase) ");
            strSQL.Append("  left join pcmso as p on(r.IdPcmso = p.IdPcmso) ");
            strSQL.Append("  left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblFUNC_EMPREGADO as FE  on(r.IdEmpregadoFuncao = fe.nID_EMPREGADO_FUNCAO and fe.nID_LAUD_TEC = p.IdLaudoTecnico) ");
            strSQL.Append("  left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEMPREGADO_FUNCAO as EF  on(r.IdEmpregadoFuncao = ef.nID_EMPREGADO_FUNCAO ) ");
            strSQL.Append("  left join pessoa as d  on(ef.nid_empr = d.idpessoa) ");
            strSQL.Append("  left join juridica as jur  on(jur.idpessoa = d.idpessoa) ");
            //            strSQL.Append("  left join pcmsoplanejamento as s on(s.idpcmso = p.idpcmso and s.IdGHE = fe.nID_FUNC) ");
            //            strSQL.Append("  left join examedicionario as t on(s.idexamedicionario = t.IdExameDicionario) ");
            strSQL.Append("  left join medico as v on(p.IdCoordenador = v.IdMedico) ");
            strSQL.Append("  left join prestador as w on(v.IdPrestador = w.IdPrestador) ");
            strSQL.Append("  left join juridicapessoa as x on(w.IdJuridicaPessoa = x.IdJuridicaPessoa) ");
            strSQL.Append("  left join pessoa as z on(x.IdPessoa = z.IdPessoa) ");
            strSQL.Append("  left join medico as rv on(em.IdMedico = rv.IdMedico) ");
            strSQL.Append("  left join prestador as rw on(rv.IdPrestador = rw.IdPrestador) ");
            strSQL.Append("  left join juridicapessoa as rx on(rw.IdJuridicaPessoa = rx.IdJuridicaPessoa) ");
            strSQL.Append("  left join pessoa as rz on(rx.IdPessoa = rz.IdPessoa) ");

            strSQL.Append(" where em.IdExameBase = " + xIdExameBase.ToString() + " ");



            strSQL.Append(" and em.idexameDicionario between  1 and 5 ");
            strSQL.Append(" and em.IndResultado not in (0,3 ) ");

            strSQL.Append(" and tno_CPF is not null  and tno_CPF <> '' and fe.nId_Func is not null   ");

            //strSQL.Append(" and DataVolta is not null ");
            strSQL.Append(" order by c.tno_CPF, dataExame desc ");



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



        public DataSet Trazer_2221(Int32 xIdEmpresa, Int32 xIdEmpregado, Int32 xIdEmpresaGrupo, string zData1, string zData2)
        {
            //string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection();
            StringBuilder strSQL = new StringBuilder();

            //DataSet m_ds;


            DataTable table = new DataTable("Result");

            table.Columns.Add("CPF", Type.GetType("System.String"));
            table.Columns.Add("PIS", Type.GetType("System.String"));
            table.Columns.Add("CNPJ", Type.GetType("System.String"));
            table.Columns.Add("Matricula", Type.GetType("System.String"));
            table.Columns.Add("tpASO", Type.GetType("System.String"));
            table.Columns.Add("resASO", Type.GetType("System.String"));
            table.Columns.Add("dtInitMonit", Type.GetType("System.String"));
            table.Columns.Add("dtFimMonit", Type.GetType("System.String"));
            table.Columns.Add("nId_Func", Type.GetType("System.Int32"));
            table.Columns.Add("NisResp", Type.GetType("System.String"));
            table.Columns.Add("NrConsClasse", Type.GetType("System.String"));
            table.Columns.Add("IdClinico", Type.GetType("System.Int32"));
            table.Columns.Add("Data_Exame", Type.GetType("System.String"));
            table.Columns.Add("NrCRM", Type.GetType("System.String"));
            table.Columns.Add("NmMed", Type.GetType("System.String"));
            table.Columns.Add("NITMed", Type.GetType("System.String"));
            table.Columns.Add("UFMed", Type.GetType("System.String"));
            table.Columns.Add("RespNome", Type.GetType("System.String"));
            table.Columns.Add("RespUF", Type.GetType("System.String"));
            table.Columns.Add("CNPJLab", Type.GetType("System.String"));
            table.Columns.Add("IdExameBase", Type.GetType("System.String"));

            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);


            strSQL.Append(" select em.IdExameBase, dbo.udf_getnumeric(c.tno_CPF) as CPF, c.nNO_PIS_PASEP as PIS,  c.tCod_Empr as Matricula, dbo.udf_getnumeric(d.NomeCodigo) as CNPJ,    ");
            strSQL.Append(" dbo.udf_getnumeric(z.NomeCodigo) as NisResp, w.Numero as nrConsClasse, w.uf as RespUF, z.NomeCompleto as RespNome, r.IdClinico, convert( char(10), Em.DataExame, 126 ) as Data_Exame, ");
            strSQL.Append(" case when rw.Numero is null then '' when rw.Numero is not null then rw.Numero end as NrCRM,   ");
            strSQL.Append(" case when rz.NomeCompleto is null then '' when rz.NomeCompleto is not null then rz.NomeCompleto end as NmMed, dbo.udf_getnumeric(rz.NomeCodigo) as NITMed, rw.UF as UFMed, dbo.udf_getnumeric(cl.NomeCodigo) as CNPJLab ");

            strSQL.Append("  from examebase as em ");
            strSQL.Append("  left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblempregado as c  on(em.idempregado =c.nid_empregado) ");
            strSQL.Append("  left join examedicionario as ed on(em.idExameDicionario = ed.IdExameDicionario) ");
            strSQL.Append("  left join pessoa as d  on(c.nid_empr = d.idpessoa) ");
            strSQL.Append("  left join clinico as r on(em.IdExameBase = r.IdExameBase) ");
            strSQL.Append("  left join pcmso as p on(r.IdPcmso = p.IdPcmso) ");
            strSQL.Append("  left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblFUNC_EMPREGADO as FE  on(r.IdEmpregadoFuncao = fe.nID_EMPREGADO_FUNCAO and fe.nID_LAUD_TEC = p.IdLaudoTecnico) ");
            //            strSQL.Append("  left join pcmsoplanejamento as s on(s.idpcmso = p.idpcmso and s.IdGHE = fe.nID_FUNC) ");
            //            strSQL.Append("  left join examedicionario as t on(s.idexamedicionario = t.IdExameDicionario) ");
            strSQL.Append("  left join medico as v on(p.IdCoordenador = v.IdMedico) ");
            strSQL.Append("  left join prestador as w on(v.IdPrestador = w.IdPrestador) ");
            strSQL.Append("  left join juridicapessoa as x on(w.IdJuridicaPessoa = x.IdJuridicaPessoa) ");
            strSQL.Append("  left join pessoa as z on(x.IdPessoa = z.IdPessoa) ");
            strSQL.Append("  left join medico as rv on(em.IdMedico = rv.IdMedico) ");
            strSQL.Append("  left join prestador as rw on(rv.IdPrestador = rw.IdPrestador) ");
            strSQL.Append("  left join juridicapessoa as rx on(rw.IdJuridicaPessoa = rx.IdJuridicaPessoa) ");
            strSQL.Append("  left join pessoa as rz on(rx.IdPessoa = rz.IdPessoa) ");
            strSQL.Append("  left join pessoa as cl on(em.idjuridica = cl.idpessoa) ");


            strSQL.Append(" where em.IdEmpregado in ");

            if (xIdEmpresa != 0)
            {
                strSQL.Append(" (select nId_Empregado from  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEMPREGADO ");
                strSQL.Append(" where nid_Empr = " + xIdEmpresa.ToString() + " ");
                strSQL.Append(" ) ");
            }
            else if (xIdEmpregado != 0)
            {
                strSQL.Append(" ( " + xIdEmpregado.ToString() + " ) ");
            }
            else if (xIdEmpresaGrupo != 0)
            {
                strSQL.Append(" (select nId_Empregado from  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEMPREGADO ");
                strSQL.Append(" where nid_Empr in ( select IdPessoa from Juridica where IdGrupoEmpresa in ( select IdGrupoEmpresa from Juridica where Idpessoa =  " + xIdEmpresaGrupo.ToString() + " )  ) ");
                strSQL.Append(" ) ");
            }


            strSQL.Append(" and ed.Nome like '%toxic%' ");
            strSQL.Append(" and em.IndResultado not in (0,3 ) ");

            strSQL.Append(" and tno_CPF is not null  and tno_CPF <> ''   and em.DataExame between convert( smalldatetime, '" + zData1 + "', 103 ) and convert( smalldatetime, '" + zData2 + "', 103 )  ");

            //strSQL.Append(" and DataVolta is not null ");
            strSQL.Append(" order by c.tno_CPF, dataExame desc ");



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





        public DataSet Trazer_2221(Int32 xIdExameBase)
        {
            //string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection();
            StringBuilder strSQL = new StringBuilder();

            //DataSet m_ds;


            DataTable table = new DataTable("Result");

            table.Columns.Add("CPF", Type.GetType("System.String"));
            table.Columns.Add("PIS", Type.GetType("System.String"));
            table.Columns.Add("CNPJ", Type.GetType("System.String"));
            table.Columns.Add("Matricula", Type.GetType("System.String"));
            table.Columns.Add("resASO", Type.GetType("System.String"));
            table.Columns.Add("Data_Exame", Type.GetType("System.String"));
            table.Columns.Add("NrCRM", Type.GetType("System.String"));
            table.Columns.Add("NmMed", Type.GetType("System.String"));
            table.Columns.Add("UFMed", Type.GetType("System.String"));
            table.Columns.Add("IdExameBase", Type.GetType("System.String"));
            table.Columns.Add("nId_Empr", Type.GetType("System.Int32"));
            table.Columns.Add("CNPJLab", Type.GetType("System.String"));
            table.Columns.Add("CAEPF", Type.GetType("System.String"));
            table.Columns.Add("CodBusca", Type.GetType("System.String"));

            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);


            strSQL.Append(" select c.nId_Empr, em.IdExameBase, dbo.udf_getnumeric(c.tno_CPF) as CPF, c.nNO_PIS_PASEP as PIS,  c.tCod_Empr as Matricula, dbo.udf_getnumeric(d.NomeCodigo) as CNPJ, ");
            strSQL.Append(" em.IndResultado as resASO, em.CodBusca, ");
            strSQL.Append(" convert(char(10), Em.DataExame, 126) as Data_Exame, ");
            strSQL.Append(" case when rw.Numero is null then '' when rw.Numero is not null then rw.Numero end as NrCRM,    case when rz.NomeCompleto is null then '' when rz.NomeCompleto is not null then rz.NomeCompleto end as NmMed, ");
            strSQL.Append(" dbo.udf_getnumeric(ry.NomeCodigo) as CNPJLab, rw.UF as UFMed, dbo.udf_getnumeric(isnull( jur.CEI,'0')) as CAEPF ");

            strSQL.Append("  from examebase (nolock)  as em ");
            strSQL.Append("  left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblempregado (nolock)  as c  on(em.idempregado =c.nid_empregado) ");
            strSQL.Append("  left join pessoa (nolock)  as d  on (c.nid_empr = d.idpessoa) ");
            strSQL.Append("  left join juridica (nolock)  as jur  on(jur.idpessoa = d.idpessoa) ");
            strSQL.Append("  left join medico (nolock)  as rv on(em.IdMedico = rv.IdMedico) ");
            strSQL.Append("  left join prestador (nolock)  as rw on(rv.IdPrestador = rw.IdPrestador) ");
            strSQL.Append("  left join juridicapessoa (nolock)  as rx on(rw.IdJuridicaPessoa = rx.IdJuridicaPessoa) ");
            strSQL.Append("  left join pessoa (nolock)  as rz on(rx.IdPessoa = rz.IdPessoa) ");
            strSQL.Append("  left join Pessoa(nolock) as ry on(em.IdJuridica = ry.IdPessoa) ");


            strSQL.Append(" where em.IdExameBase = " + xIdExameBase.ToString() + " ");

            strSQL.Append(" and em.IndResultado not in (0,3 ) ");

            strSQL.Append(" and tno_CPF is not null  and tno_CPF <> '' ");

            //strSQL.Append(" and DataVolta is not null ");
            strSQL.Append(" order by c.tno_CPF, dataExame desc ");



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





        public DataSet Trazer_2240(Int32 xIdEmpresa, Int32 xIdEmpregado, Int32 xIdEmpresaGrupo, string zData1, string zData2, Int32 zIdUsuario, string znId_Laud_Tec, Int32 zId_Func_Empregado, string zObras)
        {
            //string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection();
            StringBuilder strSQL = new StringBuilder();

            //DataSet m_ds;


            DataTable table = new DataTable("Result");

            table.Columns.Add("CPF", Type.GetType("System.String"));
            table.Columns.Add("PIS", Type.GetType("System.String"));
            table.Columns.Add("CNPJ", Type.GetType("System.String"));
            table.Columns.Add("Matricula", Type.GetType("System.String"));
            table.Columns.Add("DtIniCondicao", Type.GetType("System.String"));
            table.Columns.Add("DtFimCondicao", Type.GetType("System.String"));
            table.Columns.Add("CodAmb", Type.GetType("System.Int32"));
            table.Columns.Add("IntConc", Type.GetType("System.String"));
            table.Columns.Add("LimTol", Type.GetType("System.String"));
            table.Columns.Add("tecMedicao", Type.GetType("System.String"));
            table.Columns.Add("dscAtivDes", Type.GetType("System.String"));
            table.Columns.Add("dscSetor", Type.GetType("System.String"));
            table.Columns.Add("NISResp", Type.GetType("System.String"));
            table.Columns.Add("CPFResp", Type.GetType("System.String"));
            table.Columns.Add("NmResp", Type.GetType("System.String"));
            table.Columns.Add("nrOC", Type.GetType("System.String"));
            table.Columns.Add("ufOC", Type.GetType("System.String"));
            table.Columns.Add("nId_Laud_Tec", Type.GetType("System.Int32"));
            table.Columns.Add("nId_PPRA", Type.GetType("System.Int32"));
            table.Columns.Add("Codigo_eSocial", Type.GetType("System.String"));
            table.Columns.Add("nId_Empr", Type.GetType("System.Int32"));
            table.Columns.Add("EPC", Type.GetType("System.String"));
            table.Columns.Add("tpAval", Type.GetType("System.String"));
            table.Columns.Add("Unidade_eSocial", Type.GetType("System.String"));
            table.Columns.Add("CodFatRis", Type.GetType("System.String"));
            table.Columns.Add("NomeFatRis", Type.GetType("System.String"));
            table.Columns.Add("Inserir_eSocial", Type.GetType("System.String"));
            table.Columns.Add("Limite_eSocial", Type.GetType("System.String"));
            table.Columns.Add("nID_TEC_CMPO", Type.GetType("System.Int32"));
            table.Columns.Add("nID_Comissao_3", Type.GetType("System.Int32"));
            table.Columns.Add("nID_Comissao_1", Type.GetType("System.Int32"));
            table.Columns.Add("CAEPF", Type.GetType("System.String"));
            table.Columns.Add("CNO", Type.GetType("System.String"));

            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);

            strSQL.Append(" Select * from ( ");

            strSQL.Append(" Select distinct dbo.udf_getnumeric(SUBSTRING(c.tno_CPF,1,11)) as CPF, CONVERT(VARCHAR(20),c.nNO_PIS_PASEP) as PIS,  dbo.udf_getnumeric(SUBSTRING(d.NomeCodigo,1,20)) as CNPJ,SUBSTRING(c.tCod_Empr,1,30) as Matricula, ");
            strSQL.Append(" case when ef.hdt_inicio > hdt_laudo then convert(char(10), ef.hdt_inicio, 126) else convert(char(10), hdt_Laudo, 126) end as DtIniCondicao, convert(char(10), ");
            strSQL.Append(" dateadd(dd, 728, hdt_Laudo), 126) as DtFimCondicao, CONVERT( VARCHAR(30),ghe.nID_FUNC) as CodAmb, isnull( ghe.tno_Func, '' ) as dscSetor,  ");  // isnull( ghe.tDS_Local_TRAB, '' ) as dscSetor,  ");
            strSQL.Append(" case when tp.nid_rsc = 0 and isnull(tp.tvl_med_Ruido_LTCAT,0)>0 then replace(CONVERT( VARCHAR(15),tp.tvl_med_Ruido_LTCAT), '.', ',' ) else replace(CONVERT( VARCHAR(15),tp.tvl_med), '.', ',' ) end as IntConc, ");
            strSQL.Append(" case when tp.svl_lim_tol = 0 and tp.ginsalubre = 1 and tp.nid_rsc=2 then replace(CONVERT( VARCHAR(15),tp.tvl_med), '.', ',' ) ");
            strSQL.Append("      when tp.svl_lim_tol = 0 and tp.ginsalubre = 0 and tp.nid_rsc=2 then replace(CONVERT( VARCHAR(15),tp.tvl_med+1), '.', ',' ) ");
            strSQL.Append("      else replace(CONVERT( VARCHAR(15),tp.svl_lim_tol), '.', ',' )  end as LimTol, ");
            strSQL.Append(" convert( varchar(max), SUBSTRING(eqp.tNO_EQP_MED COLLATE sql_latin1_general_cp1251_ci_as,1,40) ) as tecMedicao, replace( convert( varchar(max),func.DescricaoFuncao  COLLATE sql_latin1_general_cp1251_ci_as ), char(2),'' ) as dscAtivDes, dbo.udf_getnumeric(SUBSTRING(ps.NomeCodigo,1,20)) as NISResp, isnull(substring(pr.Numero,1,14), '') as nrOC, isnull(dbo.udf_getnumeric(pr.CPF), '' ) as CPFResp, isnull(tl.nID_TEC_CMPO,0) as nID_TEC_CMPO, isnull(tl.nId_Comissao_1,0) as nID_Comissao_1, isnull(tl.nId_Comissao_3, 0 ) as nID_Comissao_3,  ps.NomeAbreviado as NmResp, SUBSTRING(pr.UF,1,2) as ufOC, tl.nID_LAUD_TEC, tp.nID_PPRA,  ");
            strSQL.Append(" case when mt.Codigo_eSocial is not null and mt.Codigo_eSocial <> '0'  and ltrim(rtrim(mt.Codigo_eSocial)) <> ''  then SUBSTRING(mt.Codigo_eSocial,1,20) when an.Codigo_eSocial is not null and an.Codigo_eSocial <> '0'  and an.Codigo_eSocial <> ' ' and an.Codigo_eSocial <> '09.01.001'  then SUBSTRING(an.Codigo_eSocial,1,20)   when tp.codigo_esocial is not null and tp.Codigo_eSocial <> '0'  and tp.Codigo_eSocial <> ' ' and tp.Codigo_eSocial <> '09.01.001' then SUBSTRING(tp.Codigo_eSocial,1,20)  else '09.01.001'  end as CodFatRis, tl.nId_Empr, ");
            strSQL.Append(" case when tp.mDS_EPC_EXTE is null then '' when tp.mDS_EPC_EXTE is not null then convert( varchar(max), SUBSTRING(tp.mDS_EPC_EXTE COLLATE sql_latin1_general_cp1251_ci_as,1,70) ) end as EPC, getdate() as Criado_Em, " + zIdUsuario.ToString() + " as IdUsuario, ");
            strSQL.Append(" case when tp.tvl_med = 0 and tp.svl_lim_tol=0 then '2' else '1' end as tpAval, ");
            strSQL.Append(" case when tp.nid_rsc = 0 or charindex('dB', tvl_lim_tol) > 0 then 4 when tp.nid_rsc = 2 then 10 when un.Codigo_eSocial is null then 0 else un.Codigo_eSocial end as Unidade_eSocial, ");
            strSQL.Append(" case when isnull( an.Nome, '' ) <>'' and isnull( mt.tNO_MAT_PRM, '' ) <> '' then an.Nome + ' - ' + mt.tNO_MAT_PRM  when isnull( an.Nome, '' ) <>'' then isnull( an.Nome, '' ) else isnull( mt.tNO_MAT_PRM, '' ) end as NomeFatRis,  Limite_eSocial, Inserir_eSocial, hdt_laudo, tno_cpf, dbo.udf_getnumeric(isnull( jur.CEI,'0')) as CAEPF, dbo.udf_getnumeric(isnull( jur.CNO,'0')) as CNO  ");
            strSQL.Append(" from " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblfunc_empregado as em ");
            strSQL.Append(" left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblempregado_funcao as ef on(em.nId_Empregado_Funcao = ef.nId_Empregado_Funcao) ");
            strSQL.Append(" left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblempregado as c  on(ef.nId_empregado = c.nid_empregado) ");
            strSQL.Append(" left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblLAUDO_TEC as tl on(em.nid_laud_tec = tl.nid_laud_tec) ");
            strSQL.Append(" left join pessoa as d  on(ef.nid_empr = d.idpessoa) ");
            strSQL.Append(" left join juridica as jur  on(jur.idpessoa = d.idpessoa) ");
            strSQL.Append(" left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblFunc as GHE on(em.nid_Func = GHE.nID_FUNC) ");
            strSQL.Append(" left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblPPRA1 as tp on(em.nid_Func = tp.nID_FUNC and tl.nID_LAUD_TEC = tp.nID_LAUD_TEC) ");
            strSQL.Append(" left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEQP_MED as eqp on(tp.nid_eqp_med = eqp.nID_EQP_MED) ");
            strSQL.Append(" left join funcao as Func on(ef.nId_Funcao = func.IdFuncao) ");
            //strSQL.Append(" left join prestador as Pr on(tl.nID_TEC_CMPO = pr.IdPrestador) ");
            strSQL.Append(" left join prestador as Pr on(tl.PrestadorID = pr.IdPrestador) ");
            strSQL.Append(" left join juridicapessoa as jp on(pr.IdJuridicaPessoa = jp.IdJuridicaPessoa) ");
            strSQL.Append(" left join pessoa as ps on(jp.IdPessoa = ps.IdPessoa) ");
            strSQL.Append(" left join agentequimico as an on(an.idAgenteQuimico = tp.nID_AG_NCV) ");
            strSQL.Append(" left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblmat_prm as mt on(tp.nID_MAT_PRM = mt.nID_MAT_PRM) ");
            strSQL.Append(" left join unidade as un on( un.idUnidade = tp.nId_Un) ");
            strSQL.Append(" where c.nId_Empregado in ");

            if (xIdEmpresa != 0)
            {
                strSQL.Append(" (select nId_Empregado from  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEMPREGADO_Funcao ");
                strSQL.Append(" where nid_Empr = " + xIdEmpresa.ToString() + " ");
                strSQL.Append(" ) ");
            }
            else if (xIdEmpregado != 0)
            {
                strSQL.Append(" ( " + xIdEmpregado.ToString() + " ) ");
            }
            else if (xIdEmpresaGrupo != 0)
            {
                strSQL.Append(" (select nId_Empregado from  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEMPREGADO_Funcao ");
                strSQL.Append(" where nid_Empr in ( select IdPessoa from Juridica where IdGrupoEmpresa in ( select IdGrupoEmpresa from Juridica where Idpessoa =  " + xIdEmpresaGrupo.ToString() + " )  ) ");
                strSQL.Append(" ) ");
            }

            strSQL.Append(" and tl.nId_laud_Tec in ( " + znId_Laud_Tec + " ) ");

            strSQL.Append(" and tno_CPF is not null  and tp.nId_PPRA is not null and tno_CPF <> ''  and hdt_laudo is not null "); //   and hdt_Laudo between convert( smalldatetime, '" + zData1 + "', 103 ) and convert( smalldatetime, '" + zData2 + "', 103 )  ");

            //strSQL.Append(" and DataVolta is not null ");
            //strSQL.Append(" order by c.tno_CPF, hdt_Laudo desc ");


            //strSQL.Append("  and ef.nID_EMPREGADO_FUNCAO in  ");
            //strSQL.Append("   ( select nId_Empregado_Funcao from  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEmpregado_Funcao (nolock) ");
            //strSQL.Append("     where nid_empregado = " + xIdEmpregado.ToString() + " and convert(char(15), nId_Empr) + ' ' + convert(char(10), hDt_Inicio, 103) ");
            //strSQL.Append("     in ");
            //strSQL.Append("     ( ");
            //strSQL.Append("       select convert(char(15), nId_Empr) + ' ' + convert(char(10), max(hDt_Inicio), 103) from  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEMPREGADO_FUNCAO (nolock) ");
            //strSQL.Append("       where nid_empregado = " + xIdEmpregado.ToString() + " ");
            //strSQL.Append("       and nid_Empregado_Funcao in (select nId_Empregado_Funcao from  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblFunc_Empregado (nolock) where nid_laud_tec in ( " + znId_Laud_Tec + " ) ) ");
            //strSQL.Append("       group by nId_empr ");
            //strSQL.Append("     ) ");
            //strSQL.Append("    )  ");



            strSQL.Append("  and ef.nID_EMPREGADO_FUNCAO in  ");
            strSQL.Append("   ( select nId_Empregado_Funcao from  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEmpregado_Funcao (nolock) ");
            strSQL.Append("     where nid_empregado = " + xIdEmpregado.ToString() + " and convert(char(15), nId_Empr) + ' ' + convert(char(10), hDt_Inicio, 103) ");
            strSQL.Append("     in ");
            strSQL.Append("     ( ");
            strSQL.Append("       select convert(char(15), nId_Empr) + ' ' + convert(char(10), max(hDt_Inicio), 103) from  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEMPREGADO_FUNCAO (nolock) ");
            strSQL.Append("       where nid_empregado = " + xIdEmpregado.ToString() + " ");

            if (zObras == "S")
            {
                strSQL.Append("       and nid_Empregado_Funcao in (select nId_Empregado_Funcao from  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblFunc_Empregado (nolock) where nid_laud_tec in ( " + znId_Laud_Tec + " ) ) ");
            }
            else
            {
                strSQL.Append("       and nid_Empregado_Funcao in (select nId_Empregado_Funcao from  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblFunc_Empregado (nolock) where nId_Func_Empregado in ( " + zId_Func_Empregado + " ) ) ");
            }

            strSQL.Append("       group by nId_empr ");


            strSQL.Append("     ) ");
            strSQL.Append("    )  ");
            strSQL.Append(" ) AS TX95 ");

            strSQL.Append(" where ( dtinicondicao between convert( smalldatetime, '" + zData1 + "', 103 ) and convert( smalldatetime, '" + zData2 + "', 103 )  ");
            strSQL.Append("  or DtFimCondicao between convert( smalldatetime, '" + zData1 + "', 103 ) and convert( smalldatetime, '" + zData2 + "', 103 ) ) ");


            strSQL.Append(" order by CPF, CodFatRis, intConc desc ");



            ////inserir em tbl_Esocial_2240

            //using (SqlConnection cnn = new SqlConnection(Ilitera.Data.SQLServer.EntitySQLServer.GetConnection()))
            //{
            //    cnn.Open();7

            //    SqlCommand com = new SqlCommand("Insert into " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tbl_eSocial_2240 " + strSQL, cnn);
            //    com.ExecuteNonQuery();
            //}


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


        public DataSet Mensageria_2240(Int32 xIdEmpresa, Int32 xIdEmpregado, Int32 xIdEmpresaGrupo, string zData1, string zData2, Int32 zIdUsuario, string zTipoCarga, string zNome, string zGrupo, string zAmbiente)
        {
            //string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection();
            StringBuilder strSQL = new StringBuilder();

            //DataSet m_ds;


            DataTable table = new DataTable("Result");

            table.Columns.Add("CPF", Type.GetType("System.String"));
            table.Columns.Add("Demissao", Type.GetType("System.String"));
            table.Columns.Add("CNPJ", Type.GetType("System.String"));
            table.Columns.Add("Empresa", Type.GetType("System.String"));
            table.Columns.Add("Colaborador", Type.GetType("System.String"));
            table.Columns.Add("DtFimCondicao", Type.GetType("System.String"));
            table.Columns.Add("nId_Laud_Tec", Type.GetType("System.Int32"));
            table.Columns.Add("IdeSocial_Deposito", Type.GetType("System.Int32"));
            table.Columns.Add("IdeSocial", Type.GetType("System.Int32"));
            table.Columns.Add("IdPessoa", Type.GetType("System.Int32"));
            table.Columns.Add("NomeArquivo", Type.GetType("System.String"));
            table.Columns.Add("NomeUsuario", Type.GetType("System.String"));
            table.Columns.Add("Criado_Por", Type.GetType("System.String"));
            table.Columns.Add("Criado_Em", Type.GetType("System.String"));
            table.Columns.Add("Status", Type.GetType("System.String"));
            table.Columns.Add("IdEmpregado", Type.GetType("System.Int32"));
            table.Columns.Add("nId_Func_Empregado", Type.GetType("System.Int32"));
            table.Columns.Add("GHE", Type.GetType("System.String"));
            table.Columns.Add("Matricula", Type.GetType("System.String"));
            table.Columns.Add("checkbox", Type.GetType("System.Boolean"));
            table.Columns.Add("Enviado_Em", Type.GetType("System.String"));
            table.Columns.Add("Agendado", Type.GetType("System.String"));


            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);

            strSQL.Append("  Select  Empresa,Colaborador,CPF,IdEmpregado, Demissao, CNPJ, ");
            strSQL.Append("  convert(char(10), DtIniCondicao, 126) as DtIniCondicao, convert(char(10), DtFimCondicao, 126) as DtFimCondicao, nId_Func_Empregado, GHE, ");
            strSQL.Append("  nId_Laud_Tec, IdPessoa, IdeSocial_Deposito, IdeSocial, Criado_Por, Criado_Em, checkbox, NomeArquivo, Enviado_Em, Agendado ");
            strSQL.Append("  from(  ");


            strSQL.Append(" select distinct tx90.Empresa, Emp.tno_Empg as Colaborador, tx90.CPF, tx90.IdEmpregado,  tx90.Demissao, tx90.CNPJ, tx90.DtIniCondicao, tx90.DtFimCondicao, tx90.nId_Func_Empregado, GHE,  tx90.nId_Laud_Tec, tx90.IdPessoa, ");
            strSQL.Append("                 Dep.IdeSocial_Deposito, Dep.IdeSocial, Dep.NomeArquivo, case when Us.NomeUsuario is null then '(Pendente)' when Us.NomeUsuario is not null then Us.NomeUsuario end as Criado_Por, convert(char(10), eS.DataHora_Criacao, 103) as Criado_Em  , 0 as checkbox,  ");
            //strSQL.Append("                ( select top 1 ev.Data_Envio ");
            //strSQL.Append("   from " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tbl_eSocial_Envio as eV ");
            //strSQL.Append("   where Ev.IdeSocial_Deposito = Dep.IdeSocial_Deposito ");
            //strSQL.Append("   order by ev.Data_Envio desc ");
            //strSQL.Append("                ( select top 1 'Enviado em ' + convert( char(10),ev.Data_Envio,103) + ' - ' + case when processamento_retorno like '%<nrrecibo>%' then 'Recibo gerado' when processamento_retorno like '%Lote aguardando%' then 'Aguardando Processamento' else 'Não processado'  end as Status ");
            //strSQL.Append("   from " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tbl_eSocial_Envio as eV ");
            //strSQL.Append("   where Ev.IdeSocial_Deposito = Dep.IdeSocial_Deposito ");
            //strSQL.Append("   order by ev.Data_Envio desc ");
            //strSQL.Append(" )  as Enviado_Em, ");

            strSQL.Append("                ( select top 1 'Enviado em ' + convert( char(10),ev.Data_Envio,103) + ' - ' +  ");
            strSQL.Append("  case when processamento_retorno like '%<nrRecibo>%' then 'Recibo gerado ' + substring(processamento_retorno, charindex('<nrRecibo>', Processamento_Retorno) + 10, 23) ");
            strSQL.Append("  when processamento_retorno like '%Lote aguardando%'   or processamento_retorno like '%encontra-se em processamento%'   then 'Aguardando Processamento' ");
            strSQL.Append("  when processamento_retorno like '%Contrato de Trabalho%' then 'Contrato de trabalho não localizado' ");
            strSQL.Append("  when processamento_retorno like '%estrutura do arquivo XML%' then 'Problema na estrutura do XML' ");
            strSQL.Append("  when processamento_retorno like '%duplicidade%' then 'Duplicidade na carga do evento' ");
            strSQL.Append("  when processamento_retorno like '%cpftrab%' then 'CPF do colaborador inválido' ");
            strSQL.Append("  when processamento_retorno like '%perfil de procuração eletr%' then 'Erro na procuração eletrônica' ");
            strSQL.Append("  when processamento_retorno like '%cpfresp%' then 'CPF do responsável inválido' ");            
            strSQL.Append("  else 'Não processado'  end  ");
            strSQL.Append("   from " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tbl_eSocial_Envio as eV ");
            strSQL.Append("   where Ev.IdeSocial_Deposito = Dep.IdeSocial_Deposito ");
            strSQL.Append("   order by ev.Data_Envio desc ");
            strSQL.Append(" )  as Enviado_Em, ");

            strSQL.Append(" isnull((select top 1 'X' from " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tbl_eSocial_Schedule as eS where eS.IdeSocial_Deposito = Dep.IdeSocial_Deposito and Processado_Em is null), ' ' ) as Agendado ");

            strSQL.Append(" from (  ");


            strSQL.Append(" Select  d.IdPessoa, d.NomeAbreviado as Empresa, dbo.udf_getnumeric(SUBSTRING(c.tno_CPF,1,11)) as CPF, c.nId_Empregado as IdEmpregado, isnull(CONVERT(VARCHAR(10),hdt_dem, 103),'') as Demissao,  dbo.udf_getnumeric(SUBSTRING(d.NomeCodigo,1,20)) as CNPJ,SUBSTRING(c.tCod_Empr,1,30) as Matricula,em.nid_func_empregado, ghe.tno_func as GHE, ");
            strSQL.Append(" case when ef.hDT_INICIO > tl.hDT_LAUDO then ef.hdt_inicio else tl.hdt_laudo end as DtIniCondicao, dateadd(dd, 728, hdt_Laudo) as DtFimCondicao, CONVERT( VARCHAR(30),ghe.nID_FUNC) as CodAmb,  ");
            //strSQL.Append(" replace(CONVERT( VARCHAR(15),tp.tvl_med), '.', ',' ) as IntConc, replace(CONVERT( VARCHAR(15),tp.svl_lim_tol), '.', ',' ) as LimTol, SUBSTRING(eqp.tNO_EQP_MED,1,40) as tecMedicao,  ");
            //strSQL.Append("  case when an.Codigo_eSocial is not null then SUBSTRING(an.Codigo_eSocial,1,20)  when mt.Codigo_eSocial is not null then SUBSTRING(mt.Codigo_eSocial,1,20)  when tp.codigo_esocial is not null then SUBSTRING(tp.Codigo_eSocial,1,20)  when an.Codigo_eSocial is null and mt.Codigo_eSocial is null and tp.Codigo_eSocial is null then ''  end as CodFatRis, ");
            strSQL.Append(" tl.nID_LAUD_TEC, tl.nId_Empr, getdate() as Criado_Em, " + zIdUsuario.ToString() + " as IdUsuario ");
            //strSQL.Append(" case when tp.tvl_med = 0 and tp.svl_lim_tol=0 then '2' else '1' end as tpAval, ");
            //strSQL.Append(" case when tp.nid_rsc = 0 or charindex('dB', tvl_lim_tol) > 0 then 4 when tp.nid_rsc = 2 then 14 when un.Codigo_eSocial is null then 0 else un.Codigo_eSocial end as Unidade_eSocial ");
            strSQL.Append(" from " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblfunc_empregado as em ");


            //strSQL.Append(" left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblempregado_funcao as ef on(em.nId_Empregado_Funcao = ef.nId_Empregado_Funcao) ");

            strSQL.Append("  join ");
            strSQL.Append("  ( ");
            strSQL.Append("      select * from " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblempregado_funcao as tf (nolock) join cliente as tc (nolock) on (  tf.nID_EMPR = tc.IdCliente )  ");

            strSQL.Append("      where (hDT_TERMINO is null or hDT_TERMINO >= case when  esocial_grupo=1 then convert(smalldatetime, '13/10/2021', 103) when  esocial_grupo=2 or esocial_grupo = 3 then convert(smalldatetime, '10/01/2022', 103) else convert(smalldatetime, '11/07/2022', 103)  end ) ");

            strSQL.Append("      and nid_empr in (select idcliente from cliente where  (Esocial_Ambiente is not null and ESocial_Ambiente = " + zAmbiente + "  )    and Esocial_2240 = 1  ) ");
            strSQL.Append("   ) ");
            strSQL.Append("   as ef  on( em.nId_Empregado_Funcao = ef.nId_Empregado_Funcao) ");


            strSQL.Append(" left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblempregado as c  on(ef.nId_empregado = c.nid_empregado) ");
            strSQL.Append(" left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblLAUDO_TEC as tl on(em.nid_laud_tec = tl.nid_laud_tec) ");
            strSQL.Append(" left join pessoa as d  on(ef.nid_empr = d.idpessoa) ");
            strSQL.Append(" left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblFunc as GHE on(em.nid_Func = GHE.nID_FUNC) ");
            //strSQL.Append(" left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblPPRA1 as tp on(em.nid_Func = tp.nID_FUNC and tl.nID_LAUD_TEC = tp.nID_LAUD_TEC) ");
            //strSQL.Append(" left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEQP_MED as eqp on(tp.nid_eqp_med = eqp.nID_EQP_MED) ");
            strSQL.Append(" left join funcao as Func on(ef.nId_Funcao = func.IdFuncao) ");
            ///strSQL.Append(" left join prestador as Pr on(tl.nID_TEC_CMPO = pr.IdPrestador) ");
            //strSQL.Append(" left join juridicapessoa as jp on(pr.IdJuridicaPessoa = jp.IdJuridicaPessoa) ");
            //strSQL.Append(" left join pessoa as ps on(jp.IdPessoa = ps.IdPessoa) ");
            //strSQL.Append(" left join agentequimico as an on(an.idAgenteQuimico = tp.nID_AG_NCV) ");
            //strSQL.Append(" left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblmat_prm as mt on(tp.nID_MAT_PRM = mt.nID_MAT_PRM) ");
            //strSQL.Append(" left join unidade as un on( un.idUnidade = tp.nId_Un) ");
            strSQL.Append(" where c.nId_Empregado in ");

            if (zGrupo == "N")
            {
                strSQL.Append(" (select nId_Empregado from  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEMPREGADO_Funcao ");
                strSQL.Append(" where nid_Empr = " + xIdEmpresa.ToString() + " ");
                strSQL.Append(" ) ");
            }
            else
            {
                strSQL.Append(" (select nId_Empregado from  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEMPREGADO_Funcao ");
                strSQL.Append(" where nId_Empr=" + xIdEmpresa.ToString() + " or nid_Empr in ( select IdPessoa from Juridica where IdGrupoEmpresa in ( select IdGrupoEmpresa from Juridica where Idpessoa =  " + xIdEmpresa.ToString() + " )  ) ");
                strSQL.Append(" ) ");
            }


            strSQL.Append("  and tl.nId_Pedido in ( select IdPedido from Pedido where IndStatus = 2 ) ");

            //strSQL.Append(" and tno_CPF is not null  and tp.nId_PPRA is not null and tno_CPF <> ''  and gTerceiro=0  and hdt_laudo is not null  ");  // and hdt_Laudo between convert( smalldatetime, '" + zData1 + "', 103 ) and convert( smalldatetime, '" + zData2 + "', 103 )  ");
            strSQL.Append("  and gTerceiro=0  and hdt_laudo is not null  ");

            strSQL.Append(" and ( ef.hDT_TERMINO is null or ef.hDT_TERMINO >= convert( smalldatetime, '13/10/2021', 103 ) ) ");


            //colocar para pegar apenas ultimo laudo de cada nId_Empr
            strSQL.Append("  and tl.nId_Laud_tec in ( ");

            strSQL.Append("  select nid_Laud_tec ");
            strSQL.Append("  from " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblLaudo_tec as tl (nolock) ");
            strSQL.Append("  where convert(char(15), nid_empr ) +convert(char(10), hdt_laudo, 103) in ");
            strSQL.Append("  ( ");
            strSQL.Append("  select convert(char(15), nid_empr) + convert(char(10), max(hdt_laudo), 103) ");
            strSQL.Append("     from " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tbllaudo_tec (nolock) ");
            strSQL.Append("     where nid_empr in (select idpessoa from pessoa (nolock) where isinativo = 0 )  ");
            strSQL.Append("     and nId_Pedido in (select IdPedido from Pedido (nolock) where IndStatus = 2 ) ");
            strSQL.Append(" and ( hdt_laudo between convert( smalldatetime, '" + zData1 + "', 103 ) and convert( smalldatetime, '" + zData2 + "', 103 )  ");
            strSQL.Append("  or dateadd(dd, 728, hdt_Laudo) between convert( smalldatetime, '" + zData1 + "', 103 ) and convert( smalldatetime, '" + zData2 + "', 103 ) ) ");
            strSQL.Append("     group by nid_empr ");
            strSQL.Append("  ) ");

            strSQL.Append(") ");






            strSQL.Append(" ) ");
            strSQL.Append(" as tx90 ");
            strSQL.Append(" left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tbl_eSocial_Deposito as Dep  on(tx90.nId_Laud_Tec = Dep.nId_Chave and tx90.CPF = Dep.CPF  and tx90.nID_FUNC_EMPREGADO = Dep.nID_Func_Empregado and Dep.IdEsocial in ( select IdESocial from  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tbl_eSocial where Ambiente = '" + zAmbiente + "' )  ) ");
            strSQL.Append(" left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEmpregado as Emp on(tx90.CPF = Emp.tNO_CPF and tx90.IdEmpregado = emp.nID_EMPREGADO  ) ");
            strSQL.Append(" left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEmpregado_Funcao as EmpF on ( Emp.nId_Empregado = EmpF.nId_Empregado and EmpF.nID_EMPR = tx90.IdPessoa ) ");
            strSQL.Append(" left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tbl_eSocial as eS on(Dep.IdeSocial = eS.IdeSocial) ");
            strSQL.Append(" left join Usuario as Us on (eS.IdUsuario = Us.IdUsuario) ");

            //para carregar apenas empresa selecionada, lote dá problema se envia de várias obras
            //strSQL.Append(" and ef.nid_empr = " + xIdEmpresa.ToString() + " ");


            strSQL.Append(" group by emp.tno_empg, tx90.Empresa, tx90.CPF, tx90.IdEmpregado,  tx90.Demissao, tx90.CNPJ, tx90.DtIniCondicao, tx90.DtFimCondicao,  ");
            strSQL.Append(" tx90.nId_Func_Empregado, GHE,  ");
            strSQL.Append(" tx90.nId_Laud_Tec, tx90.IdPessoa, ");
            strSQL.Append(" Dep.IdeSocial_Deposito, Dep.IdeSocial, Dep.NomeArquivo, Us.NomeUsuario, eS.DataHora_Criacao ");

            strSQL.Append(" ) as tx99 ");

            if (zTipoCarga == "0")
            {
                strSQL.Append(" where Criado_Em is null ");
            }
            else if (zTipoCarga == "1")
            {
                strSQL.Append(" where Criado_Em is not null ");
            }
            else if (zTipoCarga == "2")
            {
                strSQL.Append(" where Enviado_Em is null and Criado_Em is not null ");
            }
            else if (zTipoCarga == "3")
            {
                strSQL.Append(" where Enviado_Em is not null ");
            }
            else
            {
                strSQL.Append(" where Colaborador is not null ");
            }

            if (zNome.Trim() != "")
            {
                strSQL.Append(" and Colaborador like '%" + zNome + "%'  ");
            }

            strSQL.Append(" and ( dtinicondicao between convert( smalldatetime, '" + zData1 + "', 103 ) and convert( smalldatetime, '" + zData2 + "', 103 )  ");
            strSQL.Append("  or DtFimCondicao between convert( smalldatetime, '" + zData1 + "', 103 ) and convert( smalldatetime, '" + zData2 + "', 103 ) ) ");

            strSQL.Append("  and CPF is not null  and CPF <> '' ");

            strSQL.Append(" order by Empresa, Colaborador, NomeArquivo ");




            using (SqlConnection cnn = new SqlConnection(Ilitera.Data.SQLServer.EntitySQLServer.GetConnection()))
            {
                SqlDataAdapter da;
                cnn.Open();


                da = new SqlDataAdapter(strSQL.ToString(), cnn);
                da.SelectCommand.CommandTimeout = 600;
                da.Fill(m_ds, "Result");

                cnn.Close();

                da.Dispose();
            }

            return m_ds;

        }






        public DataSet Mensageria_2240_CSV(Int32 xIdEmpresa, Int32 xIdEmpregado, Int32 xIdEmpresaGrupo, string zData1, string zData2, Int32 zIdUsuario, string zTipoCarga, string zNome, string zGrupo, string zAmbiente)
        {
            //string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection();
            StringBuilder strSQL = new StringBuilder();

            //DataSet m_ds;


            DataTable table = new DataTable("Result");


            table.Columns.Add("CNPJ", Type.GetType("System.String"));
            table.Columns.Add("Empresa", Type.GetType("System.String"));
            table.Columns.Add("Colaborador", Type.GetType("System.String"));
            table.Columns.Add("CPF", Type.GetType("System.String"));
            table.Columns.Add("GHE", Type.GetType("System.String"));
            //table.Columns.Add("DtFimCondicao", Type.GetType("System.String"));
            //table.Columns.Add("nId_Laud_Tec", Type.GetType("System.Int32"));
            //table.Columns.Add("IdeSocial_Deposito", Type.GetType("System.Int32"));
            //table.Columns.Add("IdeSocial", Type.GetType("System.Int32"));
            //table.Columns.Add("IdPessoa", Type.GetType("System.Int32"));
            //table.Columns.Add("NomeArquivo", Type.GetType("System.String"));
            //table.Columns.Add("NomeUsuario", Type.GetType("System.String"));
            table.Columns.Add("Criado_Por", Type.GetType("System.String"));
            table.Columns.Add("Criado_Em", Type.GetType("System.String"));
            table.Columns.Add("Processamento", Type.GetType("System.String"));
            //table.Columns.Add("IdEmpregado", Type.GetType("System.Int32"));
            //table.Columns.Add("checkbox", Type.GetType("System.Boolean"));
            table.Columns.Add("Enviado_Em", Type.GetType("System.String"));
            table.Columns.Add("Matricula", Type.GetType("System.String"));

            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);

            strSQL.Append(" Select  * from (  ");


            strSQL.Append(" select distinct tx90.Empresa, max(Emp.tno_Empg) as Colaborador, tx90.CPF, tx90.IdEmpregado,  tx90.Demissao, tx90.CNPJ, tx90.DtIniCondicao, tx90.DtFimCondicao, tx90.nId_Func_Empregado, GHE, tx90.nId_Laud_Tec, tx90.IdPessoa, max(tx90.Matricula) as Matricula, ");
            strSQL.Append("                 Dep.IdeSocial_Deposito, Dep.IdeSocial, Dep.NomeArquivo, case when Us.NomeUsuario is null then '(Pendente)' when Us.NomeUsuario is not null then Us.NomeUsuario end as Criado_Por, convert(char(10), eS.DataHora_Criacao, 103) as Criado_Em  , 0 as checkbox,  ");
            //strSQL.Append("                ( select top 1 ev.Data_Envio ");
            //strSQL.Append("   from " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tbl_eSocial_Envio as eV ");
            //strSQL.Append("   where Ev.IdeSocial_Deposito = Dep.IdeSocial_Deposito ");
            //strSQL.Append("   order by ev.Data_Envio desc ");

            strSQL.Append("                ( select top 1 'Enviado em ' + convert( char(10),ev.Data_Envio,103) + ' - ' +  ");
            strSQL.Append("  case when processamento_retorno like '%<nrRecibo>%' then 'Recibo gerado ' + substring(processamento_retorno, charindex('<nrRecibo>', Processamento_Retorno) + 10, 23) ");
            strSQL.Append("  when processamento_retorno like '%Lote aguardando%'  or processamento_retorno like '%encontra-se em processamento%'  then 'Aguardando Processamento' ");
            strSQL.Append("  when processamento_retorno like '%Contrato de Trabalho%' then 'Contrato de trabalho não localizado' ");
            strSQL.Append("  when processamento_retorno like '%estrutura do arquivo XML%' then 'Problema na estrutura do XML' ");
            strSQL.Append("  when processamento_retorno like '%duplicidade%' then 'Duplicidade na carga do evento' ");
            strSQL.Append("  when processamento_retorno like '%cpftrab%' then 'CPF do colaborador inválido' ");
            strSQL.Append("  when processamento_retorno like '%perfil de procuração eletr%' then 'Erro na procuração eletrônica' ");
            strSQL.Append("  when processamento_retorno like '%cpfresp%' then 'CPF do responsável inválido' ");            
            strSQL.Append("  else 'Não processado'  end  ");
            strSQL.Append("   from " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tbl_eSocial_Envio as eV ");
            strSQL.Append("   where Ev.IdeSocial_Deposito = Dep.IdeSocial_Deposito ");
            strSQL.Append("   order by ev.Data_Envio desc ");
            strSQL.Append(" )  as Enviado_Em ");


            strSQL.Append(" from (  ");


            strSQL.Append(" Select  d.IdPessoa, d.NomeAbreviado as Empresa, dbo.udf_getnumeric(SUBSTRING(c.tno_CPF,1,11)) as CPF, c.nId_Empregado as IdEmpregado, isnull(CONVERT(VARCHAR(10),hdt_dem, 103),'') as Demissao,  dbo.udf_getnumeric(SUBSTRING(d.NomeCodigo,1,20)) as CNPJ,SUBSTRING(c.tCod_Empr,1,30) as Matricula,em.nid_func_empregado, ghe.tno_func as GHE, ");
            strSQL.Append(" case when ef.hDT_INICIO > tl.hDT_LAUDO then ef.hdt_inicio else tl.hdt_laudo end as DtIniCondicao, convert(char(10), dateadd(dd, 728, hdt_Laudo), 126) as DtFimCondicao, CONVERT( VARCHAR(30),ghe.nID_FUNC) as CodAmb,  ");
            //strSQL.Append(" replace(CONVERT( VARCHAR(15),tp.tvl_med), '.', ',' ) as IntConc, replace(CONVERT( VARCHAR(15),tp.svl_lim_tol), '.', ',' ) as LimTol, SUBSTRING(eqp.tNO_EQP_MED,1,40) as tecMedicao,  ");
            //strSQL.Append("  case when an.Codigo_eSocial is not null then SUBSTRING(an.Codigo_eSocial,1,20)  when mt.Codigo_eSocial is not null then SUBSTRING(mt.Codigo_eSocial,1,20)  when tp.codigo_esocial is not null then SUBSTRING(tp.Codigo_eSocial,1,20)  when an.Codigo_eSocial is null and mt.Codigo_eSocial is null and tp.Codigo_eSocial is null then ''  end as CodFatRis, ");
            strSQL.Append(" tl.nID_LAUD_TEC, tl.nId_Empr, getdate() as Criado_Em, " + zIdUsuario.ToString() + " as IdUsuario ");
            //strSQL.Append(" case when tp.tvl_med = 0 and tp.svl_lim_tol=0 then '2' else '1' end as tpAval, ");
            //strSQL.Append(" case when tp.nid_rsc = 0 or charindex('dB', tvl_lim_tol) > 0 then 4 when tp.nid_rsc = 2 then 14 when un.Codigo_eSocial is null then 0 else un.Codigo_eSocial end as Unidade_eSocial ");
            strSQL.Append(" from " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblfunc_empregado as em ");


            //strSQL.Append(" left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblempregado_funcao as ef on(em.nId_Empregado_Funcao = ef.nId_Empregado_Funcao) ");

            strSQL.Append("  join ");
            strSQL.Append("  ( ");
            strSQL.Append("      select * from " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblempregado_funcao as tf (nolock) join cliente as tc (nolock) on (  tf.nID_EMPR = tc.IdCliente )  ");

            strSQL.Append("      where (hDT_TERMINO is null or hDT_TERMINO >= case when  esocial_grupo=1 then convert(smalldatetime, '13/10/2021', 103) when  esocial_grupo=2 or esocial_grupo = 3 then convert(smalldatetime, '10/01/2022', 103) else convert(smalldatetime, '11/07/2022', 103)  end ) ");

            strSQL.Append("      and nid_empr in (select idcliente from cliente where  (Esocial_Ambiente is not null and ESocial_Ambiente = " + zAmbiente + "  )    and Esocial_2240 = 1  ) ");
            strSQL.Append("   ) ");
            strSQL.Append("   as ef  on( em.nId_Empregado_Funcao = ef.nId_Empregado_Funcao) ");


            strSQL.Append(" left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblempregado as c  on(ef.nId_empregado = c.nid_empregado) ");
            strSQL.Append(" left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblLAUDO_TEC as tl on(em.nid_laud_tec = tl.nid_laud_tec) ");
            strSQL.Append(" left join pessoa as d  on(ef.nid_empr = d.idpessoa) ");
            strSQL.Append(" left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblFunc as GHE on(em.nid_Func = GHE.nID_FUNC) ");
            //strSQL.Append(" left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblPPRA1 as tp on(em.nid_Func = tp.nID_FUNC and tl.nID_LAUD_TEC = tp.nID_LAUD_TEC) ");
            //strSQL.Append(" left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEQP_MED as eqp on(tp.nid_eqp_med = eqp.nID_EQP_MED) ");
            strSQL.Append(" left join funcao as Func on(ef.nId_Funcao = func.IdFuncao) ");
            ///strSQL.Append(" left join prestador as Pr on(tl.nID_TEC_CMPO = pr.IdPrestador) ");
            //strSQL.Append(" left join juridicapessoa as jp on(pr.IdJuridicaPessoa = jp.IdJuridicaPessoa) ");
            //strSQL.Append(" left join pessoa as ps on(jp.IdPessoa = ps.IdPessoa) ");
            //strSQL.Append(" left join agentequimico as an on(an.idAgenteQuimico = tp.nID_AG_NCV) ");
            //strSQL.Append(" left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblmat_prm as mt on(tp.nID_MAT_PRM = mt.nID_MAT_PRM) ");
            //strSQL.Append(" left join unidade as un on( un.idUnidade = tp.nId_Un) ");
            strSQL.Append(" where c.nId_Empregado in ");

            if (zGrupo == "N")
            {
                strSQL.Append(" (select nId_Empregado from  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEMPREGADO_Funcao ");
                strSQL.Append(" where nid_Empr = " + xIdEmpresa.ToString() + " ");
                strSQL.Append(" ) ");
            }
            else
            {
                strSQL.Append(" (select nId_Empregado from  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEMPREGADO_Funcao ");
                strSQL.Append(" where nId_Empr=" + xIdEmpresa.ToString() + " or nid_Empr in ( select IdPessoa from Juridica where IdGrupoEmpresa in ( select IdGrupoEmpresa from Juridica where Idpessoa =  " + xIdEmpresa.ToString() + " )  ) ");
                strSQL.Append(" ) ");
            }

            strSQL.Append("  and tl.nId_Pedido in ( select IdPedido from Pedido where IndStatus = 2 ) ");

            //strSQL.Append(" and tno_CPF is not null  and tp.nId_PPRA is not null and tno_CPF <> ''  and gTerceiro=0  and hdt_laudo is not null "); //   and hdt_Laudo between convert( smalldatetime, '" + zData1 + "', 103 ) and convert( smalldatetime, '" + zData2 + "', 103 )  ");
            strSQL.Append("  and gTerceiro=0  and hdt_laudo is not null  ");

            strSQL.Append(" and ( ef.hDT_TERMINO is null or ef.hDT_TERMINO >= convert( smalldatetime, '13/10/2021', 103 ) ) ");


            //colocar para pegar apenas ultimo laudo de cada nId_Empr
            strSQL.Append("  and tl.nId_Laud_tec in ( ");

            strSQL.Append("  select nid_Laud_tec ");
            strSQL.Append("  from " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblLaudo_tec as tl (nolock) ");
            strSQL.Append("  where convert(char(15), nid_empr ) +convert(char(10), hdt_laudo, 103) in ");
            strSQL.Append("  ( ");
            strSQL.Append("  select convert(char(15), nid_empr) + convert(char(10), max(hdt_laudo), 103) ");
            strSQL.Append("     from " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tbllaudo_tec (nolock) ");
            strSQL.Append("     where nid_empr in (select idpessoa from pessoa (nolock) where isinativo = 0 )  ");
            strSQL.Append("     and nId_Pedido in (select IdPedido from Pedido (nolock) where IndStatus = 2 ) ");
            strSQL.Append(" and ( hdt_laudo between convert( smalldatetime, '" + zData1 + "', 103 ) and convert( smalldatetime, '" + zData2 + "', 103 )  ");
            strSQL.Append("  or dateadd(dd, 728, hdt_Laudo) between convert( smalldatetime, '" + zData1 + "', 103 ) and convert( smalldatetime, '" + zData2 + "', 103 ) ) ");
            strSQL.Append("     group by nid_empr ");
            strSQL.Append("  ) ");

            strSQL.Append(") ");



            //para carregar apenas empresa selecionada, lote dá problema se envia de várias obras
            //strSQL.Append(" and ef.nid_empr = " + xIdEmpresa.ToString() + " ");

            strSQL.Append(" ) ");
            strSQL.Append(" as tx90 ");
            strSQL.Append(" left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tbl_eSocial_Deposito as Dep  on(tx90.nId_Laud_Tec = Dep.nId_Chave and tx90.CPF = Dep.CPF and tx90.nID_FUNC_EMPREGADO = Dep.nID_Func_Empregado and Dep.IdEsocial in ( select IdESocial from  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tbl_eSocial where Ambiente = '" + zAmbiente + "' )  ) ");
            strSQL.Append(" left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEmpregado as Emp on(tx90.CPF = Emp.tNO_CPF and tx90.IdEmpregado = emp.nID_EMPREGADO  ) ");
            strSQL.Append(" left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEmpregado_Funcao as EmpF on ( Emp.nId_Empregado = EmpF.nId_Empregado and EmpF.nID_EMPR = tx90.IdPessoa ) ");
            strSQL.Append(" left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tbl_eSocial as eS on(Dep.IdeSocial = eS.IdeSocial) ");
            strSQL.Append(" left join Usuario as Us on (eS.IdUsuario = Us.IdUsuario) ");

            strSQL.Append(" group by tx90.Empresa, tx90.CPF, tx90.IdEmpregado,  tx90.Demissao, tx90.CNPJ, tx90.DtIniCondicao, tx90.DtFimCondicao,  ");
            strSQL.Append(" tx90.nId_Func_Empregado, GHE,  ");
            strSQL.Append(" tx90.nId_Laud_Tec, tx90.IdPessoa, ");
            strSQL.Append(" Dep.IdeSocial_Deposito, Dep.IdeSocial, Dep.NomeArquivo, Us.NomeUsuario, eS.DataHora_Criacao ");


            strSQL.Append(" ) as tx99 ");

            if (zTipoCarga == "0")
            {
                strSQL.Append(" where Criado_Em is null ");
            }
            else if (zTipoCarga == "1")
            {
                strSQL.Append(" where Criado_Em is not null ");
            }
            else if (zTipoCarga == "2")
            {
                strSQL.Append(" where Enviado_Em is null and Criado_Em is not null ");
            }
            else if (zTipoCarga == "3")
            {
                strSQL.Append(" where Enviado_Em is not null ");
            }
            else
            {
                strSQL.Append(" where Colaborador is not null ");
            }



            if (zNome.Trim() != "")
            {
                strSQL.Append(" and Colaborador like '%" + zNome + "%'  ");
            }


            strSQL.Append(" and ( dtinicondicao between convert( smalldatetime, '" + zData1 + "', 103 ) and convert( smalldatetime, '" + zData2 + "', 103 )  ");
            strSQL.Append("  or DtFimCondicao between convert( smalldatetime, '" + zData1 + "', 103 ) and convert( smalldatetime, '" + zData2 + "', 103 ) ) ");

            strSQL.Append("  and CPF is not null  and CPF <> '' ");

            strSQL.Append(" order by Empresa, Colaborador, NomeArquivo ");




            using (SqlConnection cnn = new SqlConnection(Ilitera.Data.SQLServer.EntitySQLServer.GetConnection()))
            {
                SqlDataAdapter da;
                cnn.Open();


                da = new SqlDataAdapter(strSQL.ToString(), cnn);
                da.SelectCommand.CommandTimeout = 600;
                da.Fill(m_ds, "Result");

                cnn.Close();

                da.Dispose();
            }

            return m_ds;

        }




        public DataSet Mensageria_2220(Int32 xIdEmpresa, Int32 xIdEmpregado, Int32 xIdEmpresaGrupo, string zData1, string zData2, Int32 zIdUsuario, string zTipoCarga, string zNome, string zGrupo, string zAmbiente)
        {
            //string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection();
            StringBuilder strSQL = new StringBuilder();

            //DataSet m_ds;


            DataTable table = new DataTable("Result");

            table.Columns.Add("CPF", Type.GetType("System.String"));
            table.Columns.Add("PIS", Type.GetType("System.String"));
            table.Columns.Add("CNPJ", Type.GetType("System.String"));
            table.Columns.Add("Empresa", Type.GetType("System.String"));
            table.Columns.Add("Colaborador", Type.GetType("System.String"));
            table.Columns.Add("DtFimCondicao", Type.GetType("System.String"));
            table.Columns.Add("DtIniCondicao", Type.GetType("System.String"));
            table.Columns.Add("IdExameBase", Type.GetType("System.Int32"));
            table.Columns.Add("IdeSocial_Deposito", Type.GetType("System.Int32"));
            table.Columns.Add("IdeSocial", Type.GetType("System.Int32"));
            table.Columns.Add("IdPessoa", Type.GetType("System.Int32"));
            table.Columns.Add("NomeArquivo", Type.GetType("System.String"));
            table.Columns.Add("NomeUsuario", Type.GetType("System.String"));
            table.Columns.Add("Criado_Por", Type.GetType("System.String"));
            table.Columns.Add("Criado_Em", Type.GetType("System.String"));
            table.Columns.Add("Status", Type.GetType("System.String"));
            table.Columns.Add("IdEmpregado", Type.GetType("System.Int32"));
            table.Columns.Add("Exame", Type.GetType("System.String"));
            table.Columns.Add("checkbox", Type.GetType("System.Boolean"));
            table.Columns.Add("Enviado_Em", Type.GetType("System.String"));
            table.Columns.Add("Agendado", Type.GetType("System.String"));


            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);


            strSQL.Append(" Select  * from (  ");

            strSQL.Append(" select distinct tx90.Exame,tx90.Empresa, Emp.tno_Empg as Colaborador, tx90.CPF, tx90.IdEmpregado,  tx90.PIS, tx90.CNPJ, tx90.DtIniCondicao, tx90.DtFimCondicao, tx90.IdExameBase, tx90.IdPessoa,  ");
            strSQL.Append("                 Dep.IdeSocial_Deposito, Dep.IdeSocial, Dep.NomeArquivo, case when Us.NomeUsuario is null then '(Pendente)' when Us.NomeUsuario is not null then Us.NomeUsuario end as Criado_Por, convert(char(10), eS.DataHora_Criacao, 103) as Criado_Em , 0 as checkbox,  ");
            //strSQL.Append("                ( select top 1 ev.Data_Envio ");
            //strSQL.Append("   from " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tbl_eSocial_Envio as eV ");
            //strSQL.Append("   where Ev.IdeSocial_Deposito = Dep.IdeSocial_Deposito ");
            //strSQL.Append("   order by ev.Data_Envio desc ");
            //strSQL.Append("                ( select top 1 'Enviado em ' + convert( char(10),ev.Data_Envio,103) + ' - ' + case when processamento_retorno like '%<nrrecibo>%' then 'Recibo gerado' when processamento_retorno like '%Lote aguardando%' then 'Aguardando Processamento' else 'Não processado'  end as Status ");
            //strSQL.Append("   from " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tbl_eSocial_Envio as eV ");
            //strSQL.Append("   where Ev.IdeSocial_Deposito = Dep.IdeSocial_Deposito ");
            //strSQL.Append("   order by ev.Data_Envio desc ");
            //strSQL.Append(" )  as Enviado_Em, ");

            strSQL.Append("                ( select top 1 'Enviado em ' + convert( char(10),ev.Data_Envio,103) + ' - ' +  ");
            strSQL.Append("  case when processamento_retorno like '%<nrRecibo>%' then 'Recibo gerado ' + substring(processamento_retorno, charindex('<nrRecibo>', Processamento_Retorno) + 10, 23) ");
            strSQL.Append("  when processamento_retorno like '%Lote aguardando%'  or processamento_retorno like '%encontra-se em processamento%'  then 'Aguardando Processamento' ");
            strSQL.Append("  when processamento_retorno like '%Contrato de Trabalho%' then 'Contrato de trabalho não localizado' ");
            strSQL.Append("  when processamento_retorno like '%estrutura do arquivo XML%' then 'Problema na estrutura do XML' ");
            strSQL.Append("  when processamento_retorno like '%duplicidade%' then 'Duplicidade na carga do evento' ");
            strSQL.Append("  when processamento_retorno like '%cpftrab%' then 'CPF do colaborador inválido' ");
            strSQL.Append("  when processamento_retorno like '%perfil de procuração eletr%' then 'Erro na procuração eletrônica' ");
            strSQL.Append("  when processamento_retorno like '%cpfresp%' then 'CPF do responsável inválido' ");
            strSQL.Append("  when processamento_retorno like '%tipo de exame ocupacional for diferente%' then 'Ordem dos exames clínicos inválida' ");
            strSQL.Append("  else 'Não processado'  end  ");
            strSQL.Append("   from " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tbl_eSocial_Envio as eV ");
            strSQL.Append("   where Ev.IdeSocial_Deposito = Dep.IdeSocial_Deposito ");
            strSQL.Append("   order by ev.Data_Envio desc ");
            strSQL.Append(" )  as Enviado_Em, ");

            strSQL.Append(" isnull((select top 1 'X' from " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tbl_eSocial_Schedule as eS where eS.IdeSocial_Deposito = Dep.IdeSocial_Deposito and Processado_Em is null), ' ' ) as Agendado ");

            strSQL.Append(" from (  ");

            strSQL.Append(" select em.IdExameBase, em.IdEmpregado, d.IdPessoa, dbo.udf_getnumeric(c.tno_CPF) as CPF, c.nNO_PIS_PASEP as PIS,  c.tCod_Empr as Matricula, dbo.udf_getnumeric(d.NomeCodigo) as CNPJ,    ");
            strSQL.Append("  case when em.IdExameDicionario = 1 then '0'  when em.IdExameDicionario = 2 then '9' when em.IdExameDicionario = 3 then '3' when em.IdExameDicionario = 4 then '1'  when em.IdExameDicionario = 5 then '2' end as tpASO, ");
            strSQL.Append("  em.IndResultado as resASO, convert(char(10), p.datapcmso, 126) as dtInitMonit, d.NomeAbreviado as Empresa, convert(char(10), DataExame, 103 ) as DtFimCondicao, convert(char(10), DataExame, 103 ) as DtIniCondicao, ");
            strSQL.Append("  case when  p.TerminoPcmso is null then '' when p.TerminoPcmso is not null then convert( ");
            strSQL.Append(" char(10), p.terminopcmso, 126) end as dtFimMonit, fe.nid_Func, dbo.udf_getnumeric(z.NomeCodigo) as NisResp, w.Numero as nrConsClasse, w.uf as RespUF, z.NomeCompleto as RespNome, r.IdClinico, convert( char(10), Em.DataExame, 126 ) as Data_Exame, ");
            strSQL.Append(" case when rw.Numero is null then '' when rw.Numero is not null then rw.Numero end as NrCRM, ed.Nome as Exame,  ");
            strSQL.Append(" case when rz.NomeCompleto is null then '' when rz.NomeCompleto is not null then rz.NomeCompleto end as NmMed, dbo.udf_getnumeric(rz.NomeCodigo) as NITMed, rw.UF as UFMed, dbo.udf_getnumeric(w.CPF) as RespCPF ");

            strSQL.Append("  from examebase as em ");
            strSQL.Append("  left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblempregado as c  on(em.idempregado =c.nid_empregado) ");            
            strSQL.Append("  left join clinico as r on(em.IdExameBase = r.IdExameBase) ");
            strSQL.Append("  left join pcmso as p on(r.IdPcmso = p.IdPcmso) ");
            strSQL.Append("  left join examedicionario as ed on(em.IdExameDicionario = ed.IdExameDIcionario) ");
            strSQL.Append("  left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblFUNC_EMPREGADO as FE  on(r.IdEmpregadoFuncao = fe.nID_EMPREGADO_FUNCAO and fe.nID_LAUD_TEC = p.IdLaudoTecnico) ");
            strSQL.Append("  left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEMPREGADO_Funcao as EF   on ( r.IdEmpregadoFuncao = ef.nid_empregado_Funcao ) ");
            strSQL.Append("  left join pessoa as d  on(ef.nid_empr = d.idpessoa) ");
            //            strSQL.Append("  left join pcmsoplanejamento as s on(s.idpcmso = p.idpcmso and s.IdGHE = fe.nID_FUNC) ");
            //            strSQL.Append("  left join examedicionario as t on(s.idexamedicionario = t.IdExameDicionario) ");
            strSQL.Append("  left join medico as v on(p.IdCoordenador = v.IdMedico) ");
            strSQL.Append("  left join prestador as w on(v.IdPrestador = w.IdPrestador) ");
            strSQL.Append("  left join juridicapessoa as x on(w.IdJuridicaPessoa = x.IdJuridicaPessoa) ");
            strSQL.Append("  left join pessoa as z on(x.IdPessoa = z.IdPessoa) ");
            strSQL.Append("  left join medico as rv on(em.IdMedico = rv.IdMedico) ");
            strSQL.Append("  left join prestador as rw on(rv.IdPrestador = rw.IdPrestador) ");
            strSQL.Append("  left join juridicapessoa as rx on(rw.IdJuridicaPessoa = rx.IdJuridicaPessoa) ");
            strSQL.Append("  left join pessoa as rz on(rx.IdPessoa = rz.IdPessoa) ");

            strSQL.Append(" where em.IdEmpregado in ");


            if (zGrupo == "N")
            {
                strSQL.Append(" (select nId_Empregado from  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEMPREGADO_Funcao ");
                strSQL.Append(" where nid_Empr = " + xIdEmpresa.ToString() + " ");
                strSQL.Append(" ) ");
            }
            else
            {
                strSQL.Append(" (select nId_Empregado from  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEMPREGADO_Funcao ");
                strSQL.Append(" where nId_Empr=" + xIdEmpresa.ToString() + " or nid_Empr in ( select IdPessoa from Juridica where IdGrupoEmpresa in ( select IdGrupoEmpresa from Juridica where Idpessoa =  " + xIdEmpresa.ToString() + " )  ) ");
                strSQL.Append(" ) ");
            }


            //flag para não carregar eSocial
            strSQL.Append(" and ( em.Tirar_eSocial is null or em.Tirar_eSocial = 0 ) ");

            strSQL.Append(" and em.idexameDicionario between  1 and 5 ");
            strSQL.Append(" and em.IndResultado not in (0,3 ) ");

            strSQL.Append(" and tno_CPF is not null  and tno_CPF <> ''  and gTerceiro=0  and fe.nId_Func is not null  and em.DataExame between convert( smalldatetime, '" + zData1 + "', 103 ) and convert( smalldatetime, '" + zData2 + "', 103 )  ");

            //para carregar apenas empresa selecionada, lote dá problema se envia de várias obras
            //strSQL.Append(" and ef.nid_empr = " + xIdEmpresa.ToString() + " ");



            strSQL.Append(" ) ");
            strSQL.Append(" as tx90 ");
            strSQL.Append(" left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tbl_eSocial_Deposito as Dep  on(tx90.IdExamebase = Dep.nId_Chave and tx90.CPF = Dep.CPF  and Dep.IdEsocial in ( select IdESocial from  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tbl_eSocial where Ambiente = '" + zAmbiente + "' )  ) ");
            strSQL.Append(" left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEmpregado as Emp on(tx90.CPF = Emp.tNO_CPF and tx90.IdEmpregado = emp.nID_EMPREGADO  ) ");
            strSQL.Append(" left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEmpregado_Funcao as EmpF on ( Emp.nId_Empregado = EmpF.nId_Empregado and EmpF.nID_EMPR = tx90.IdPessoa ) ");
            strSQL.Append(" left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tbl_eSocial as eS on(Dep.IdeSocial = eS.IdeSocial) ");
            strSQL.Append(" left join Usuario as Us on (eS.IdUsuario = Us.IdUsuario) ");


            strSQL.Append(" ) as tx99 ");

            if (zTipoCarga == "0")
            {
                strSQL.Append(" where Criado_Em is null ");
            }
            else if (zTipoCarga == "1")
            {
                strSQL.Append(" where Criado_Em is not null ");
            }
            else if (zTipoCarga == "2")
            {
                strSQL.Append(" where Enviado_Em is null and Criado_Em is not null ");
            }
            else if (zTipoCarga == "3")
            {
                strSQL.Append(" where Enviado_Em is not null ");
            }
            else
            {
                strSQL.Append(" where Colaborador is not null ");
            }

            if (zNome.Trim() != "")
            {
                strSQL.Append(" and Colaborador like '%" + zNome + "%'  ");
            }


            strSQL.Append(" order by Empresa, Colaborador, NomeArquivo ");




            using (SqlConnection cnn = new SqlConnection(Ilitera.Data.SQLServer.EntitySQLServer.GetConnection()))
            {
                SqlDataAdapter da;
                cnn.Open();


                da = new SqlDataAdapter(strSQL.ToString(), cnn);
                da.SelectCommand.CommandTimeout = 600;
                da.Fill(m_ds, "Result");

                cnn.Close();

                da.Dispose();
            }

            return m_ds;

        }




        public DataSet Mensageria_2220_CSV(Int32 xIdEmpresa, Int32 xIdEmpregado, Int32 xIdEmpresaGrupo, string zData1, string zData2, Int32 zIdUsuario, string zTipoCarga, string zNome, string zGrupo, string zAmbiente)
        {
            //string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection();
            StringBuilder strSQL = new StringBuilder();

            //DataSet m_ds;


            DataTable table = new DataTable("Result");

            table.Columns.Add("CPF", Type.GetType("System.String"));
            //table.Columns.Add("PIS", Type.GetType("System.String"));
            table.Columns.Add("CNPJ", Type.GetType("System.String"));
            table.Columns.Add("Empresa", Type.GetType("System.String"));
            table.Columns.Add("Colaborador", Type.GetType("System.String"));
            //table.Columns.Add("DtFimCondicao", Type.GetType("System.String"));
            //table.Columns.Add("DtIniCondicao", Type.GetType("System.String"));
            //table.Columns.Add("IdExameBase", Type.GetType("System.Int32"));
            //table.Columns.Add("IdeSocial_Deposito", Type.GetType("System.Int32"));
            //table.Columns.Add("IdeSocial", Type.GetType("System.Int32"));
            //table.Columns.Add("IdPessoa", Type.GetType("System.Int32"));
            //table.Columns.Add("NomeArquivo", Type.GetType("System.String"));
            //table.Columns.Add("NomeUsuario", Type.GetType("System.String"));
            table.Columns.Add("Criado_Por", Type.GetType("System.String"));
            table.Columns.Add("Criado_Em", Type.GetType("System.String"));
            table.Columns.Add("Status", Type.GetType("System.String"));
            //table.Columns.Add("IdEmpregado", Type.GetType("System.Int32"));
            table.Columns.Add("Exame", Type.GetType("System.String"));
            //table.Columns.Add("checkbox", Type.GetType("System.Boolean"));
            table.Columns.Add("Enviado_Em", Type.GetType("System.String"));
            table.Columns.Add("Matricula", Type.GetType("System.String"));



            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);


            strSQL.Append(" Select  * from (  ");

            strSQL.Append(" select distinct tx90.Exame,tx90.Empresa, Emp.tno_Empg as Colaborador, tx90.CPF, tx90.IdEmpregado,  tx90.PIS, tx90.CNPJ, tx90.DtIniCondicao, tx90.DtFimCondicao, tx90.IdExameBase, tx90.IdPessoa, tx90.Matricula,  ");
            strSQL.Append("                 Dep.IdeSocial_Deposito, Dep.IdeSocial, Dep.NomeArquivo, case when Us.NomeUsuario is null then '(Pendente)' when Us.NomeUsuario is not null then Us.NomeUsuario end as Criado_Por, convert(char(10), eS.DataHora_Criacao, 103) as Criado_Em , 0 as checkbox,  ");
            //strSQL.Append("                ( select top 1 ev.Data_Envio ");
            //strSQL.Append("   from " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tbl_eSocial_Envio as eV ");
            //strSQL.Append("   where Ev.IdeSocial_Deposito = Dep.IdeSocial_Deposito ");
            //strSQL.Append("   order by ev.Data_Envio desc ");

            //strSQL.Append("                ( select top 1 'Enviado em ' + convert( char(10),ev.Data_Envio,103) + ' - ' + case when processamento_retorno like '%<nrRecibo>%' then 'Recibo gerado'  when processamento_retorno like '%Lote aguardando%' then 'Aguardando Processamento' else 'Não processado'  end as Status ");
            //strSQL.Append("   from " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tbl_eSocial_Envio as eV ");
            //strSQL.Append("   where Ev.IdeSocial_Deposito = Dep.IdeSocial_Deposito ");
            //strSQL.Append("   order by ev.Data_Envio desc ");
            //strSQL.Append(" )  as Enviado_Em ");

            strSQL.Append("                ( select top 1 'Enviado em ' + convert( char(10),ev.Data_Envio,103) + ' - ' +  ");
            strSQL.Append("  case when processamento_retorno like '%<nrRecibo>%' then 'Recibo gerado ' + substring(processamento_retorno, charindex('<nrRecibo>', Processamento_Retorno) + 10, 23) ");
            strSQL.Append("  when processamento_retorno like '%Lote aguardando%'  or processamento_retorno like '%encontra-se em processamento%'  then 'Aguardando Processamento' ");
            strSQL.Append("  when processamento_retorno like '%Contrato de Trabalho%' then 'Contrato de trabalho não localizado' ");
            strSQL.Append("  when processamento_retorno like '%estrutura do arquivo XML%' then 'Problema na estrutura do XML' ");
            strSQL.Append("  when processamento_retorno like '%duplicidade%' then 'Duplicidade na carga do evento' ");
            strSQL.Append("  when processamento_retorno like '%cpftrab%' then 'CPF do colaborador inválido' ");
            strSQL.Append("  when processamento_retorno like '%perfil de procuração eletr%' then 'Erro na procuração eletrônica' ");
            strSQL.Append("  when processamento_retorno like '%cpfresp%' then 'CPF do responsável inválido' ");
            strSQL.Append("  when processamento_retorno like '%tipo de exame ocupacional for diferente%' then 'Ordem dos exames clínicos inválida' ");
            strSQL.Append("  else 'Não processado'  end  ");
            strSQL.Append("   from " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tbl_eSocial_Envio as eV ");
            strSQL.Append("   where Ev.IdeSocial_Deposito = Dep.IdeSocial_Deposito ");
            strSQL.Append("   order by ev.Data_Envio desc ");
            strSQL.Append(" )  as Enviado_Em ");

            strSQL.Append(" from (  ");

            strSQL.Append(" select em.IdExameBase, em.IdEmpregado, d.IdPessoa, dbo.udf_getnumeric(c.tno_CPF) as CPF, c.nNO_PIS_PASEP as PIS,  c.tCod_Empr as Matricula, d.NomeCodigo as CNPJ,    ");
            strSQL.Append("  case when em.IdExameDicionario = 1 then '0'  when em.IdExameDicionario = 2 then '9' when em.IdExameDicionario = 3 then '3' when em.IdExameDicionario = 4 then '1'  when em.IdExameDicionario = 5 then '2' end as tpASO, ");
            strSQL.Append("  em.IndResultado as resASO, convert(char(10), p.datapcmso, 126) as dtInitMonit, d.NomeAbreviado as Empresa, convert(char(10), DataExame, 103 ) as DtFimCondicao, convert(char(10), DataExame, 103 ) as DtIniCondicao, ");
            strSQL.Append("  case when  p.TerminoPcmso is null then '' when p.TerminoPcmso is not null then convert( ");
            strSQL.Append(" char(10), p.terminopcmso, 126) end as dtFimMonit, fe.nid_Func, dbo.udf_getnumeric(z.NomeCodigo) as NisResp, w.Numero as nrConsClasse, w.uf as RespUF, z.NomeCompleto as RespNome, r.IdClinico, convert( char(10), Em.DataExame, 126 ) as Data_Exame, ");
            strSQL.Append(" case when rw.Numero is null then '' when rw.Numero is not null then rw.Numero end as NrCRM, ed.Nome as Exame,  ");
            strSQL.Append(" case when rz.NomeCompleto is null then '' when rz.NomeCompleto is not null then rz.NomeCompleto end as NmMed, dbo.udf_getnumeric(rz.NomeCodigo) as NITMed, rw.UF as UFMed, dbo.udf_getnumeric(w.CPF) as RespCPF ");

            strSQL.Append("  from examebase as em ");
            strSQL.Append("  left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblempregado as c  on(em.idempregado =c.nid_empregado) ");
            strSQL.Append("  left join clinico as r on(em.IdExameBase = r.IdExameBase) ");
            strSQL.Append("  left join pcmso as p on(r.IdPcmso = p.IdPcmso) ");
            strSQL.Append("  left join examedicionario as ed on(em.IdExameDicionario = ed.IdExameDIcionario) ");
            strSQL.Append("  left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblFUNC_EMPREGADO as FE  on(r.IdEmpregadoFuncao = fe.nID_EMPREGADO_FUNCAO and fe.nID_LAUD_TEC = p.IdLaudoTecnico) ");
            strSQL.Append("  left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEMPREGADO_Funcao as EF   on ( r.IdEmpregadoFuncao = ef.nid_empregado_Funcao ) ");
            strSQL.Append("  left join pessoa as d  on(ef.nid_empr = d.idpessoa) ");
            //            strSQL.Append("  left join pcmsoplanejamento as s on(s.idpcmso = p.idpcmso and s.IdGHE = fe.nID_FUNC) ");
            //            strSQL.Append("  left join examedicionario as t on(s.idexamedicionario = t.IdExameDicionario) ");
            strSQL.Append("  left join medico as v on(p.IdCoordenador = v.IdMedico) ");
            strSQL.Append("  left join prestador as w on(v.IdPrestador = w.IdPrestador) ");
            strSQL.Append("  left join juridicapessoa as x on(w.IdJuridicaPessoa = x.IdJuridicaPessoa) ");
            strSQL.Append("  left join pessoa as z on(x.IdPessoa = z.IdPessoa) ");
            strSQL.Append("  left join medico as rv on(em.IdMedico = rv.IdMedico) ");
            strSQL.Append("  left join prestador as rw on(rv.IdPrestador = rw.IdPrestador) ");
            strSQL.Append("  left join juridicapessoa as rx on(rw.IdJuridicaPessoa = rx.IdJuridicaPessoa) ");
            strSQL.Append("  left join pessoa as rz on(rx.IdPessoa = rz.IdPessoa) ");

            strSQL.Append(" where em.IdEmpregado in ");


            if (zGrupo == "N")
            {
                strSQL.Append(" (select nId_Empregado from  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEMPREGADO_Funcao ");
                strSQL.Append(" where nid_Empr = " + xIdEmpresa.ToString() + " ");
                strSQL.Append(" ) ");
            }
            else
            {
                strSQL.Append(" (select nId_Empregado from  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEMPREGADO_Funcao ");
                strSQL.Append(" where nId_Empr=" + xIdEmpresa.ToString() + " or nid_Empr in ( select IdPessoa from Juridica where IdGrupoEmpresa in ( select IdGrupoEmpresa from Juridica where Idpessoa =  " + xIdEmpresa.ToString() + " )  ) ");
                strSQL.Append(" ) ");
            }


            //flag para não carregar eSocial
            strSQL.Append(" and ( em.Tirar_eSocial is null or em.Tirar_eSocial = 0 ) ");

            strSQL.Append(" and em.idexameDicionario between  1 and 5 ");
            strSQL.Append(" and em.IndResultado not in (0,3 ) ");

            strSQL.Append(" and tno_CPF is not null  and tno_CPF <> ''  and gTerceiro=0 and fe.nId_Func is not null  and em.DataExame between convert( smalldatetime, '" + zData1 + "', 103 ) and convert( smalldatetime, '" + zData2 + "', 103 )  ");

            //para carregar apenas empresa selecionada, lote dá problema se envia de várias obras
            //strSQL.Append(" and ef.nid_empr = " + xIdEmpresa.ToString() + " ");



            strSQL.Append(" ) ");
            strSQL.Append(" as tx90 ");
            strSQL.Append(" left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tbl_eSocial_Deposito as Dep  on(tx90.IdExamebase = Dep.nId_Chave and tx90.CPF = Dep.CPF and Dep.IdEsocial in ( select IdESocial from  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tbl_eSocial where Ambiente = '" + zAmbiente + "' ) ) ");
            strSQL.Append(" left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEmpregado as Emp on(tx90.CPF = Emp.tNO_CPF and tx90.IdEmpregado = emp.nID_EMPREGADO   ) ");
            strSQL.Append(" left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEmpregado_Funcao as EmpF on ( Emp.nId_Empregado = EmpF.nId_Empregado and EmpF.nID_EMPR = tx90.IdPessoa ) ");
            strSQL.Append(" left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tbl_eSocial as eS on(Dep.IdeSocial = eS.IdeSocial) ");
            strSQL.Append(" left join Usuario as Us on (eS.IdUsuario = Us.IdUsuario) ");

            strSQL.Append(" ) as tx99 ");

            if (zTipoCarga == "0")
            {
                strSQL.Append(" where Criado_Em is null ");
            }
            else if (zTipoCarga == "1")
            {
                strSQL.Append(" where Criado_Em is not null ");
            }
            else if (zTipoCarga == "2")
            {
                strSQL.Append(" where Enviado_Em is null and Criado_Em is not null ");
            }
            else if (zTipoCarga == "3")
            {
                strSQL.Append(" where Enviado_Em is not null ");
            }
            else
            {
                strSQL.Append(" where Colaborador is not null ");
            }



            if (zNome.Trim() != "")
            {
                strSQL.Append(" and Colaborador like '%" + zNome + "%'  ");
            }


            strSQL.Append(" order by Empresa, Colaborador, NomeArquivo ");




            using (SqlConnection cnn = new SqlConnection(Ilitera.Data.SQLServer.EntitySQLServer.GetConnection()))
            {
                SqlDataAdapter da;
                cnn.Open();


                da = new SqlDataAdapter(strSQL.ToString(), cnn);
                da.SelectCommand.CommandTimeout = 600;
                da.Fill(m_ds, "Result");

                cnn.Close();

                da.Dispose();
            }

            return m_ds;

        }




        public DataSet Mensageria_2221_CSV(Int32 xIdEmpresa, Int32 xIdEmpregado, Int32 xIdEmpresaGrupo, string zData1, string zData2, Int32 zIdUsuario, string zTipoCarga, string zNome, string zGrupo, string zAmbiente)
        {
            //string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection();
            StringBuilder strSQL = new StringBuilder();

            //DataSet m_ds;


            DataTable table = new DataTable("Result");

            table.Columns.Add("CPF", Type.GetType("System.String"));
            table.Columns.Add("PIS", Type.GetType("System.String"));
            table.Columns.Add("CNPJ", Type.GetType("System.String"));
            table.Columns.Add("Empresa", Type.GetType("System.String"));
            table.Columns.Add("Colaborador", Type.GetType("System.String"));
            table.Columns.Add("DtFimCondicao", Type.GetType("System.String"));
            table.Columns.Add("DtIniCondicao", Type.GetType("System.String"));
            table.Columns.Add("IdExameBase", Type.GetType("System.Int32"));
            table.Columns.Add("IdeSocial_Deposito", Type.GetType("System.Int32"));
            table.Columns.Add("IdeSocial", Type.GetType("System.Int32"));
            table.Columns.Add("IdPessoa", Type.GetType("System.Int32"));
            table.Columns.Add("NomeArquivo", Type.GetType("System.String"));
            table.Columns.Add("NomeUsuario", Type.GetType("System.String"));
            table.Columns.Add("Criado_Por", Type.GetType("System.String"));
            table.Columns.Add("Criado_Em", Type.GetType("System.String"));
            table.Columns.Add("IdEmpregado", Type.GetType("System.Int32"));
            table.Columns.Add("checkbox", Type.GetType("System.Boolean"));
            table.Columns.Add("Enviado_Em", Type.GetType("System.String"));
            table.Columns.Add("Exame", Type.GetType("System.String"));
            table.Columns.Add("Agendado", Type.GetType("System.String"));


            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);

            strSQL.Append(" Select  * from (  ");


            strSQL.Append(" select distinct tx90.IdExameBase, tx90.Empresa, '' as Exame, Emp.tno_Empg as Colaborador, tx90.CPF, tx90.IdEmpregado,  tx90.PIS, tx90.CNPJ, tx90.Data_Exame as DtIniCondicao, tx90.Data_Exame  as DtFimCondicao, tx90.IdPessoa,  ");
            strSQL.Append("                 ' ' as Agendado, Dep.IdeSocial_Deposito, Dep.IdeSocial, Dep.NomeArquivo, case when Us.NomeUsuario is null then '(Pendente)' when Us.NomeUsuario is not null then Us.NomeUsuario end as Criado_Por, convert(char(20), eS.DataHora_Criacao, 113) as Criado_Em  , 0 as checkbox,  ");
            strSQL.Append("                ( select top 1 ev.Data_Envio ");
            strSQL.Append("   from " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tbl_eSocial_Envio as eV ");
            strSQL.Append("   where Ev.IdeSocial_Deposito = Dep.IdeSocial_Deposito ");
            strSQL.Append("   order by ev.Data_Envio desc ");
            strSQL.Append(" )  as Enviado_Em ");

            strSQL.Append(" from (  ");


            strSQL.Append(" select d.IdPessoa, c.nId_Empregado as IdEmpregado, d.NomeAbreviado as Empresa, em.IdExameBase, dbo.udf_getnumeric(c.tno_CPF) as CPF, c.nNO_PIS_PASEP as PIS,  c.tCod_Empr as Matricula, dbo.udf_getnumeric(d.NomeCodigo) as CNPJ,    ");
            strSQL.Append(" dbo.udf_getnumeric(z.NomeCodigo) as NisResp, w.Numero as nrConsClasse, w.uf as RespUF, z.NomeCompleto as RespNome, r.IdClinico, convert( char(10), Em.DataExame, 126 ) as Data_Exame, ");
            strSQL.Append(" case when rw.Numero is null then '' when rw.Numero is not null then rw.Numero end as NrCRM,   ");
            strSQL.Append(" case when rz.NomeCompleto is null then '' when rz.NomeCompleto is not null then rz.NomeCompleto end as NmMed, dbo.udf_getnumeric(rz.NomeCodigo) as NITMed, rw.UF as UFMed, dbo.udf_getnumeric(cl.NomeCodigo) as CNPJLab ");

            strSQL.Append("  from examebase as em ");
            strSQL.Append("  left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblempregado as c  on(em.idempregado =c.nid_empregado) ");
            strSQL.Append("  left join examedicionario as ed on(em.idExameDicionario = ed.IdExameDicionario) ");
            strSQL.Append("  left join pessoa as d  on(c.nid_empr = d.idpessoa) ");
            strSQL.Append("  left join clinico as r on(em.IdExameBase = r.IdExameBase) ");
            strSQL.Append("  left join pcmso as p on(r.IdPcmso = p.IdPcmso) ");
            strSQL.Append("  left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblFUNC_EMPREGADO as FE  on(r.IdEmpregadoFuncao = fe.nID_EMPREGADO_FUNCAO and fe.nID_LAUD_TEC = p.IdLaudoTecnico) ");
            //            strSQL.Append("  left join pcmsoplanejamento as s on(s.idpcmso = p.idpcmso and s.IdGHE = fe.nID_FUNC) ");
            //            strSQL.Append("  left join examedicionario as t on(s.idexamedicionario = t.IdExameDicionario) ");
            strSQL.Append("  left join medico as v on(p.IdCoordenador = v.IdMedico) ");
            strSQL.Append("  left join prestador as w on(v.IdPrestador = w.IdPrestador) ");
            strSQL.Append("  left join juridicapessoa as x on(w.IdJuridicaPessoa = x.IdJuridicaPessoa) ");
            strSQL.Append("  left join pessoa as z on(x.IdPessoa = z.IdPessoa) ");
            strSQL.Append("  left join medico as rv on(em.IdMedico = rv.IdMedico) ");
            strSQL.Append("  left join prestador as rw on(rv.IdPrestador = rw.IdPrestador) ");
            strSQL.Append("  left join juridicapessoa as rx on(rw.IdJuridicaPessoa = rx.IdJuridicaPessoa) ");
            strSQL.Append("  left join pessoa as rz on(rx.IdPessoa = rz.IdPessoa) ");
            strSQL.Append("  left join pessoa as cl on(em.idjuridica = cl.idpessoa) ");


            strSQL.Append(" where em.IdEmpregado in ");

            if (zGrupo == "N")
            {
                strSQL.Append(" (select nId_Empregado from  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEMPREGADO ");
                strSQL.Append(" where nid_Empr = " + xIdEmpresa.ToString() + " ");
                strSQL.Append(" ) ");
            }
            else
            {
                strSQL.Append(" (select nId_Empregado from  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEMPREGADO ");
                strSQL.Append(" where nId_Empr=" + xIdEmpresa.ToString() + " or nid_Empr in ( select IdPessoa from Juridica where IdGrupoEmpresa in ( select IdGrupoEmpresa from Juridica where Idpessoa =  " + xIdEmpresa.ToString() + " )  ) ");
                strSQL.Append(" ) ");
            }



            strSQL.Append(" and ed.Nome like '%toxic%' ");
            strSQL.Append(" and em.IndResultado not in (0,3 ) ");

            strSQL.Append(" and tno_CPF is not null  and tno_CPF <> ''   and em.DataExame between convert( smalldatetime, '" + zData1 + "', 103 ) and convert( smalldatetime, '" + zData2 + "', 103 )  ");



            strSQL.Append(" ) ");
            strSQL.Append(" as tx90 ");
            strSQL.Append(" left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tbl_eSocial_Deposito as Dep  on(tx90.IdExameBase = Dep.nId_Chave and tx90.CPF = Dep.CPF) ");
            strSQL.Append(" left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEmpregado as Emp on(tx90.CPF = Emp.tNO_CPF and Emp.nID_EMPR = tx90.IdPessoa) ");
            strSQL.Append(" left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tbl_eSocial as eS on(Dep.IdeSocial = eS.IdeSocial) ");
            strSQL.Append(" left join Usuario as Us on (eS.IdUsuario = Us.IdUsuario) ");




            strSQL.Append(" ) as tx99 ");

            if (zTipoCarga == "0")
            {
                strSQL.Append(" where Criado_Em is null ");
            }
            else if (zTipoCarga == "1")
            {
                strSQL.Append(" where Criado_Em is not null ");
            }
            else if (zTipoCarga == "2")
            {
                strSQL.Append(" where Enviado_Em is null and Criado_Em is not null ");
            }
            else if (zTipoCarga == "3")
            {
                strSQL.Append(" where Enviado_Em is not null ");
            }
            else
            {
                strSQL.Append(" where Colaborador is not null ");
            }

            if (zNome.Trim() != "")
            {
                strSQL.Append(" and Colaborador like '%" + zNome + "%'  ");
            }

            strSQL.Append(" order by Empresa, Colaborador, NomeArquivo ");



            using (SqlConnection cnn = new SqlConnection(Ilitera.Data.SQLServer.EntitySQLServer.GetConnection()))
            {
                SqlDataAdapter da;
                cnn.Open();


                da = new SqlDataAdapter(strSQL.ToString(), cnn);
                da.SelectCommand.CommandTimeout = 600;
                da.Fill(m_ds, "Result");

                cnn.Close();

                da.Dispose();
            }

            return m_ds;

        }






        public DataSet Mensageria_2245(Int32 xIdEmpresa, Int32 xIdEmpregado, Int32 xIdEmpresaGrupo, string zData1, string zData2, Int32 zIdUsuario, string zTipoCarga, string zNome, string zGrupo)
        {
            //string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection();
            StringBuilder strSQL = new StringBuilder();

            //DataSet m_ds;


            DataTable table = new DataTable("Result");

            table.Columns.Add("CPF", Type.GetType("System.String"));
            table.Columns.Add("PIS", Type.GetType("System.String"));
            table.Columns.Add("CNPJ", Type.GetType("System.String"));
            table.Columns.Add("Empresa", Type.GetType("System.String"));
            table.Columns.Add("Colaborador", Type.GetType("System.String"));
            table.Columns.Add("DtFimCondicao", Type.GetType("System.String"));
            table.Columns.Add("DtIniCondicao", Type.GetType("System.String"));
            table.Columns.Add("IdParticipanteTreinamento", Type.GetType("System.Int32"));
            table.Columns.Add("IdeSocial_Deposito", Type.GetType("System.Int32"));
            table.Columns.Add("IdeSocial", Type.GetType("System.Int32"));
            table.Columns.Add("IdPessoa", Type.GetType("System.Int32"));
            table.Columns.Add("NomeArquivo", Type.GetType("System.String"));
            table.Columns.Add("NomeUsuario", Type.GetType("System.String"));
            table.Columns.Add("Criado_Por", Type.GetType("System.String"));
            table.Columns.Add("Criado_Em", Type.GetType("System.String"));
            table.Columns.Add("IdEmpregado", Type.GetType("System.Int32"));
            table.Columns.Add("Treinamento", Type.GetType("System.String"));
            table.Columns.Add("checkbox", Type.GetType("System.Boolean"));
            table.Columns.Add("Enviado_Em", Type.GetType("System.String"));


            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);

            strSQL.Append(" Select  * from (  ");


            strSQL.Append(" select distinct tx90.Treinamento, tx90.Empresa, Emp.tno_Empg as Colaborador, tx90.CPF, Emp.nId_Empregado as IdEmpregado,  tx90.PIS, tx90.CNPJ, tx90.DtIniCondicao, tx90.DtFimCondicao, tx90.IdParticipanteTreinamento, tx90.IdPessoa,  ");
            strSQL.Append("                 Dep.IdeSocial_Deposito, Dep.IdeSocial, Dep.NomeArquivo, case when Us.NomeUsuario is null then '(Pendente)' when Us.NomeUsuario is not null then Us.NomeUsuario end as Criado_Por, convert(char(20), eS.DataHora_Criacao, 113) as Criado_Em   , 0 as checkbox,  ");
            strSQL.Append("                ( select top 1 ev.Data_Envio ");
            strSQL.Append("   from " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tbl_eSocial_Envio as eV ");
            strSQL.Append("   where Ev.IdeSocial_Deposito = Dep.IdeSocial_Deposito ");
            strSQL.Append("   order by ev.Data_Envio desc ");
            strSQL.Append(" )  as Enviado_Em ");

            strSQL.Append(" from (  ");


            strSQL.Append(" select b.IdParticipanteTreinamento, dbo.udf_getnumeric(c.tno_CPF) as CPF, c.nNO_PIS_PASEP as PIS,  c.tCod_Empr as Matricula, dbo.udf_getnumeric(d.NomeCodigo) as CNPJ,  ");
            strSQL.Append(" f.Codigo_eSocial as CodTreiCap, convert(char(10), g.DataLevantamento, 126) as dtTreiCap, ");
            strSQL.Append(" a.Carga_Horaria as durTreiCap, '1' as ModTreiCap, tpTreiCap, codCBO, o.NomeReduzido as Treinamento, d.NomeAbreviado as Empresa, a.Periodo as DtIniCondicao, a.Periodo as DtFimCondicao, d.IdPessoa,  ");
            strSQL.Append(" case when h.CPF is null then '' else dbo.udf_getnumeric(h.CPF) end as CPFProf, j.NomeCompleto as nmProf, '1' as tpProf, h.titulo as FormProf ");
            strSQL.Append(" from treinamento as a ");
            strSQL.Append(" left join ParticipanteTreinamento as b on(a.idTreinamento = b.IdTreinamento) ");
            strSQL.Append(" left join  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblempregado as c on(b.idEmpregado = c.nid_empregado) ");
            strSQL.Append(" left join pessoa as d on(c.nid_empr = d.idpessoa) ");
            strSQL.Append(" left join TreinamentoDicionario as f on(a.IdTreinamentoDicionario = f.IdTreinamentoDicionario) ");
            strSQL.Append(" left join Documento as g on(a.idDocumento = g.IdDocumento) ");
            strSQL.Append(" left join prestador as h on(a.IdResponsavel = h.idprestador) ");
            strSQL.Append(" left join juridicapessoa as i on(h.idjuridicapessoa = i.idjuridicapessoa) ");
            strSQL.Append(" left join pessoa as j on(i.idpessoa = j.idpessoa) ");
            strSQL.Append(" left join obrigacao as o on (f.idobrigacao = o.idobrigacao ) ");
            strSQL.Append(" where c.tno_cpf is not null ");

            strSQL.Append(" and g.DataLevantamento between convert( smalldatetime, '" + zData1 + "',103 ) and convert( smalldatetime,'" + zData2 + "', 103 ) ");


            strSQL.Append(" and c.nId_Empregado in ");

            if (zGrupo == "N")
            {
                strSQL.Append(" (select nId_Empregado from  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEMPREGADO ");
                strSQL.Append(" where nid_Empr = " + xIdEmpresa.ToString() + " ");
                strSQL.Append(" ) ");
            }
            else
            {
                strSQL.Append(" (select nId_Empregado from  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEMPREGADO ");
                strSQL.Append(" where nId_Empr=" + xIdEmpresa.ToString() + " or nid_Empr in ( select IdPessoa from Juridica where IdGrupoEmpresa in ( select IdGrupoEmpresa from Juridica where Idpessoa =  " + xIdEmpresa.ToString() + " )  ) ");
                strSQL.Append(" ) ");
            }



            strSQL.Append(" ) ");
            strSQL.Append(" as tx90 ");
            strSQL.Append(" left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tbl_eSocial_Deposito as Dep  on(tx90.IdParticipanteTreinamento = Dep.nId_Chave and tx90.CPF = Dep.CPF) ");
            strSQL.Append(" left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEmpregado as Emp on(tx90.CPF = Emp.tNO_CPF and Emp.nID_EMPR = tx90.IdPessoa and tx90.IdEmpregado = emp.nID_EMPREGADO ) ");
            strSQL.Append(" left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tbl_eSocial as eS on(Dep.IdeSocial = eS.IdeSocial) ");
            strSQL.Append(" left join Usuario as Us on (eS.IdUsuario = Us.IdUsuario) ");


            strSQL.Append(" ) as tx99 ");

            if (zTipoCarga == "0")
            {
                strSQL.Append(" where Criado_Em is null ");
            }
            else if (zTipoCarga == "1")
            {
                strSQL.Append(" where Criado_Em is not null ");
            }
            else if (zTipoCarga == "2")
            {
                strSQL.Append(" where Enviado_Em is null and Criado_Em is not null ");
            }
            else if (zTipoCarga == "3")
            {
                strSQL.Append(" where Enviado_Em is not null ");
            }
            else
            {
                strSQL.Append(" where Colaborador is not null ");
            }

            if (zNome.Trim() != "")
            {
                strSQL.Append(" and Colaborador like '%" + zNome + "%'  ");
            }


            strSQL.Append(" order by empresa, colaborador, NomeArquivo ");




            using (SqlConnection cnn = new SqlConnection(Ilitera.Data.SQLServer.EntitySQLServer.GetConnection()))
            {
                SqlDataAdapter da;
                cnn.Open();


                da = new SqlDataAdapter(strSQL.ToString(), cnn);
                da.SelectCommand.CommandTimeout = 600;
                da.Fill(m_ds, "Result");

                cnn.Close();

                da.Dispose();
            }

            return m_ds;

        }





        public DataSet Mensageria_2230(Int32 xIdEmpresa, Int32 xIdEmpregado, Int32 xIdEmpresaGrupo, string zData1, string zData2, Int32 zIdUsuario, string zTipoCarga, string zNome, string zGrupo, string zAmbiente)
        {
            //string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection();
            StringBuilder strSQL = new StringBuilder();

            //DataSet m_ds;


            DataTable table = new DataTable("Result");

            table.Columns.Add("CPF", Type.GetType("System.String"));
            table.Columns.Add("PIS", Type.GetType("System.String"));
            table.Columns.Add("CNPJ", Type.GetType("System.String"));
            table.Columns.Add("Empresa", Type.GetType("System.String"));
            table.Columns.Add("Colaborador", Type.GetType("System.String"));
            table.Columns.Add("DtFimCondicao", Type.GetType("System.String"));
            table.Columns.Add("IdAfastamento", Type.GetType("System.Int32"));
            table.Columns.Add("IdeSocial_Deposito", Type.GetType("System.Int32"));
            table.Columns.Add("IdeSocial", Type.GetType("System.Int32"));
            table.Columns.Add("IdPessoa", Type.GetType("System.Int32"));
            table.Columns.Add("NomeArquivo", Type.GetType("System.String"));
            table.Columns.Add("NomeUsuario", Type.GetType("System.String"));
            table.Columns.Add("Criado_Por", Type.GetType("System.String"));
            table.Columns.Add("Criado_Em", Type.GetType("System.String"));
            table.Columns.Add("IdEmpregado", Type.GetType("System.Int32"));
            table.Columns.Add("checkbox", Type.GetType("System.Boolean"));
            table.Columns.Add("Enviado_Em", Type.GetType("System.String"));
            table.Columns.Add("Agendado", Type.GetType("System.String"));


            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);

            strSQL.Append(" Select  * from (  ");

            strSQL.Append(" select distinct tx90.IdAfastamento, tx90.Empresa, Emp.tno_Empg as Colaborador, tx90.CPF, tx90.IdEmpregado,  tx90.PIS, tx90.CNPJ, DtInicial as DtIniCondicao, DtInicial as DtFimCondicao, tx90.IdPessoa,  ");
            strSQL.Append("                 Dep.IdeSocial_Deposito, Dep.IdeSocial, Dep.NomeArquivo, case when Us.NomeUsuario is null then '(Pendente)' when Us.NomeUsuario is not null then Us.NomeUsuario end as Criado_Por, convert(char(10), eS.DataHora_Criacao, 103) as Criado_Em  , 0 as checkbox,  ");
            //strSQL.Append("                ( select top 1 ev.Data_Envio ");
            //strSQL.Append("   from " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tbl_eSocial_Envio as eV ");
            //strSQL.Append("   where Ev.IdeSocial_Deposito = Dep.IdeSocial_Deposito ");
            //strSQL.Append("   order by ev.Data_Envio desc ");
            strSQL.Append("                ( select top 1 'Enviado em ' + convert( char(10),ev.Data_Envio,103) + ' - ' + case when processamento_retorno like '%<nrrecibo>%' then 'Recibo gerado' else 'Não processado'  end as Status ");
            strSQL.Append("   from " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tbl_eSocial_Envio as eV ");
            strSQL.Append("   where Ev.IdeSocial_Deposito = Dep.IdeSocial_Deposito ");
            strSQL.Append("   order by ev.Data_Envio desc ");
            strSQL.Append(" )  as Enviado_Em, ");

            strSQL.Append(" isnull((select top 1 'X' from " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tbl_eSocial_Schedule as eS where eS.IdeSocial_Deposito = Dep.IdeSocial_Deposito and Processado_Em is null), ' ' ) as Agendado ");

            strSQL.Append(" from (  ");


            strSQL.Append(" select d.NomeAbreviado as Empresa, a.IdAfastamento, dbo.udf_getnumeric(c.tno_CPF) as CPF, c.nNO_PIS_PASEP as PIS,  c.tCod_Empr as Matricula, dbo.udf_getnumeric(d.NomeCodigo) as CNPJ, a.Obs as Observacao, ");
            strSQL.Append("    case when a.IdAfastamentoTipo is null then 1 ");
            strSQL.Append("         when a.IdAfastamentoTipo is not null then e.Codigo_eSocial ");
            strSQL.Append("    end as CodMotAfast, convert(char(10), DataInicial, 126) as DtInicial, ");
            strSQL.Append("    case when DataVolta is null then '' when DataVolta is not null then convert(char(10), DataVolta, 126) end as DtVolta, case when DataInicial is null or DataVolta is null then 0  when datediff(dd, DataInicial, DataVolta) > 32000 then 32000 else datediff(dd, DataInicial, DataVolta) end as Dias, ");
            strSQL.Append("    case when b.CodigoCid is null then '' when b.CodigoCid is not null then b.CodigoCid end as Cid1, ");
            strSQL.Append("    case when f.CodigoCid is null then '' when f.CodigoCid is not null then f.CodigoCid end as Cid2, ");
            strSQL.Append("    case when g.CodigoCid is null then '' when g.CodigoCid is not null then g.CodigoCid end as Cid3, ");
            strSQL.Append("    case when h.CodigoCid is null then '' when h.CodigoCid is not null then h.CodigoCid end as Cid4, ");
            strSQL.Append("    Atestado_Emitente, Atestado_ideOC, Atestado_nrOC, Atestado_ufOC, c.nId_Empregado as IdEmpregado, d.idpessoa ");
            strSQL.Append(" from afastamento as a left join CID as b ");
            strSQL.Append(" on(a.idcid = b.IdCID) ");
            strSQL.Append(" left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblempregado as c ");
            strSQL.Append(" on(a.idempregado = c.nid_empregado) ");
            strSQL.Append(" left join pessoa as d ");
            strSQL.Append(" on(c.nid_empr = d.idpessoa) ");
            strSQL.Append(" left join afastamentotipo as e ");
            strSQL.Append(" on(a.IdAfastamentoTipo = e.idAfastamentoTipo) ");
            strSQL.Append(" left join CID as f ");
            strSQL.Append(" on(a.idcid2 = f.IdCID) ");
            strSQL.Append(" left join CID as g ");
            strSQL.Append(" on(a.idcid3 = g.IdCID) ");
            strSQL.Append(" left join CID as h ");
            strSQL.Append(" on(a.idcid4 = h.IdCID) ");
            strSQL.Append(" where IdEmpregado in ");

            if (zGrupo == "N")
            {
                strSQL.Append(" (select nId_Empregado from  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEMPREGADO_Funcao ");
                strSQL.Append(" where nid_Empr = " + xIdEmpresa.ToString() + " ");
                strSQL.Append(" ) ");
            }
            else
            {
                strSQL.Append(" (select nId_Empregado from  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEMPREGADO_Funcao ");
                strSQL.Append(" where nId_Empr=" + xIdEmpresa.ToString() + " or nid_Empr in ( select IdPessoa from Juridica where IdGrupoEmpresa in ( select IdGrupoEmpresa from Juridica where Idpessoa =  " + xIdEmpresa.ToString() + " )  ) ");
                strSQL.Append(" ) ");
            }



            strSQL.Append(" and tno_CPF is not null  and tno_CPF <> ''  and a.DataInicial between convert( smalldatetime, '" + zData1 + "', 103 ) and convert( smalldatetime, '" + zData2 + "', 103 )  ");

            strSQL.Append(" ) ");
            strSQL.Append(" as tx90 ");
            strSQL.Append(" left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tbl_eSocial_Deposito as Dep  on(tx90.IdAfastamento = Dep.nId_Chave and tx90.CPF = Dep.CPF  and Dep.IdEsocial in ( select IdESocial from  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tbl_eSocial where Ambiente = '" + zAmbiente + "' )  ) ");
            strSQL.Append(" left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEmpregado as Emp on(tx90.CPF = Emp.tNO_CPF and Emp.nID_EMPR = tx90.IdPessoa and tx90.IdEmpregado = emp.nID_EMPREGADO ) ");
            strSQL.Append(" left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tbl_eSocial as eS on(Dep.IdeSocial = eS.IdeSocial) ");
            strSQL.Append(" left join Usuario as Us on (eS.IdUsuario = Us.IdUsuario) ");


            strSQL.Append(" ) as tx99 ");

            if (zTipoCarga == "0")
            {
                strSQL.Append(" where Criado_Em is null ");
            }
            else if (zTipoCarga == "1")
            {
                strSQL.Append(" where Criado_Em is not null ");
            }
            else if (zTipoCarga == "2")
            {
                strSQL.Append(" where Enviado_Em is null and Criado_Em is not null ");
            }
            else if (zTipoCarga == "3")
            {
                strSQL.Append(" where Enviado_Em is not null ");
            }
            else
            {
                strSQL.Append(" where Colaborador is not null ");
            }

            if (zNome.Trim() != "")
            {
                strSQL.Append(" and Colaborador like '%" + zNome + "%'  ");
            }

            strSQL.Append(" order by Empresa, Colaborador, NomeArquivo ");




            using (SqlConnection cnn = new SqlConnection(Ilitera.Data.SQLServer.EntitySQLServer.GetConnection()))
            {
                SqlDataAdapter da;
                cnn.Open();


                da = new SqlDataAdapter(strSQL.ToString(), cnn);
                da.SelectCommand.CommandTimeout = 600;
                da.Fill(m_ds, "Result");

                cnn.Close();

                da.Dispose();
            }

            return m_ds;

        }



        public DataSet Mensageria_2221(Int32 xIdEmpresa, Int32 xIdEmpregado, Int32 xIdEmpresaGrupo, string zData1, string zData2, Int32 zIdUsuario, string zTipoCarga, string zNome, string zGrupo)
        {
            //string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection();
            StringBuilder strSQL = new StringBuilder();

            //DataSet m_ds;


            DataTable table = new DataTable("Result");

            table.Columns.Add("CPF", Type.GetType("System.String"));
            table.Columns.Add("PIS", Type.GetType("System.String"));
            table.Columns.Add("CNPJ", Type.GetType("System.String"));
            table.Columns.Add("Empresa", Type.GetType("System.String"));
            table.Columns.Add("Colaborador", Type.GetType("System.String"));
            table.Columns.Add("DtFimCondicao", Type.GetType("System.String"));
            table.Columns.Add("DtIniCondicao", Type.GetType("System.String"));
            table.Columns.Add("IdExameBase", Type.GetType("System.Int32"));
            table.Columns.Add("IdeSocial_Deposito", Type.GetType("System.Int32"));
            table.Columns.Add("IdeSocial", Type.GetType("System.Int32"));
            table.Columns.Add("IdPessoa", Type.GetType("System.Int32"));
            table.Columns.Add("NomeArquivo", Type.GetType("System.String"));
            table.Columns.Add("NomeUsuario", Type.GetType("System.String"));
            table.Columns.Add("Criado_Por", Type.GetType("System.String"));
            table.Columns.Add("Criado_Em", Type.GetType("System.String"));
            table.Columns.Add("IdEmpregado", Type.GetType("System.Int32"));
            table.Columns.Add("checkbox", Type.GetType("System.Boolean"));
            table.Columns.Add("Enviado_Em", Type.GetType("System.String"));
            table.Columns.Add("Exame", Type.GetType("System.String"));
            table.Columns.Add("Agendado", Type.GetType("System.String"));


            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);

            strSQL.Append(" Select  * from (  ");


            strSQL.Append(" select distinct tx90.IdExameBase, tx90.Empresa, '' as Exame, Emp.tno_Empg as Colaborador, tx90.CPF, tx90.IdEmpregado,  tx90.PIS, tx90.CNPJ, tx90.Data_Exame as DtIniCondicao, tx90.Data_Exame  as DtFimCondicao, tx90.IdPessoa,  ");
            strSQL.Append("                 ' ' as Agendado, Dep.IdeSocial_Deposito, Dep.IdeSocial, Dep.NomeArquivo, case when Us.NomeUsuario is null then '(Pendente)' when Us.NomeUsuario is not null then Us.NomeUsuario end as Criado_Por, convert(char(20), eS.DataHora_Criacao, 113) as Criado_Em  , 0 as checkbox,  ");
            strSQL.Append("                ( select top 1 ev.Data_Envio ");
            strSQL.Append("   from " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tbl_eSocial_Envio as eV ");
            strSQL.Append("   where Ev.IdeSocial_Deposito = Dep.IdeSocial_Deposito ");
            strSQL.Append("   order by ev.Data_Envio desc ");
            strSQL.Append(" )  as Enviado_Em ");

            strSQL.Append(" from (  ");


            strSQL.Append(" select d.IdPessoa, c.nId_Empregado as IdEmpregado, d.NomeAbreviado as Empresa, em.IdExameBase, dbo.udf_getnumeric(c.tno_CPF) as CPF, c.nNO_PIS_PASEP as PIS,  c.tCod_Empr as Matricula, dbo.udf_getnumeric(d.NomeCodigo) as CNPJ,    ");
            strSQL.Append(" dbo.udf_getnumeric(z.NomeCodigo) as NisResp, w.Numero as nrConsClasse, w.uf as RespUF, z.NomeCompleto as RespNome, r.IdClinico, convert( char(10), Em.DataExame, 126 ) as Data_Exame, ");
            strSQL.Append(" case when rw.Numero is null then '' when rw.Numero is not null then rw.Numero end as NrCRM,   ");
            strSQL.Append(" case when rz.NomeCompleto is null then '' when rz.NomeCompleto is not null then rz.NomeCompleto end as NmMed, dbo.udf_getnumeric(rz.NomeCodigo) as NITMed, rw.UF as UFMed, dbo.udf_getnumeric(cl.NomeCodigo) as CNPJLab ");

            strSQL.Append("  from examebase as em ");
            strSQL.Append("  left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblempregado as c  on(em.idempregado =c.nid_empregado) ");
            strSQL.Append("  left join examedicionario as ed on(em.idExameDicionario = ed.IdExameDicionario) ");
            strSQL.Append("  left join pessoa as d  on(c.nid_empr = d.idpessoa) ");
            strSQL.Append("  left join clinico as r on(em.IdExameBase = r.IdExameBase) ");
            strSQL.Append("  left join pcmso as p on(r.IdPcmso = p.IdPcmso) ");
            strSQL.Append("  left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblFUNC_EMPREGADO as FE  on(r.IdEmpregadoFuncao = fe.nID_EMPREGADO_FUNCAO and fe.nID_LAUD_TEC = p.IdLaudoTecnico) ");
            //            strSQL.Append("  left join pcmsoplanejamento as s on(s.idpcmso = p.idpcmso and s.IdGHE = fe.nID_FUNC) ");
            //            strSQL.Append("  left join examedicionario as t on(s.idexamedicionario = t.IdExameDicionario) ");
            strSQL.Append("  left join medico as v on(p.IdCoordenador = v.IdMedico) ");
            strSQL.Append("  left join prestador as w on(v.IdPrestador = w.IdPrestador) ");
            strSQL.Append("  left join juridicapessoa as x on(w.IdJuridicaPessoa = x.IdJuridicaPessoa) ");
            strSQL.Append("  left join pessoa as z on(x.IdPessoa = z.IdPessoa) ");
            strSQL.Append("  left join medico as rv on(em.IdMedico = rv.IdMedico) ");
            strSQL.Append("  left join prestador as rw on(rv.IdPrestador = rw.IdPrestador) ");
            strSQL.Append("  left join juridicapessoa as rx on(rw.IdJuridicaPessoa = rx.IdJuridicaPessoa) ");
            strSQL.Append("  left join pessoa as rz on(rx.IdPessoa = rz.IdPessoa) ");
            strSQL.Append("  left join pessoa as cl on(em.idjuridica = cl.idpessoa) ");


            strSQL.Append(" where em.IdEmpregado in ");

            if (zGrupo == "N")
            {
                strSQL.Append(" (select nId_Empregado from  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEMPREGADO ");
                strSQL.Append(" where nid_Empr = " + xIdEmpresa.ToString() + " ");
                strSQL.Append(" ) ");
            }
            else
            {
                strSQL.Append(" (select nId_Empregado from  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEMPREGADO ");
                strSQL.Append(" where nId_Empr=" + xIdEmpresa.ToString() + " or nid_Empr in ( select IdPessoa from Juridica where IdGrupoEmpresa in ( select IdGrupoEmpresa from Juridica where Idpessoa =  " + xIdEmpresa.ToString() + " )  ) ");
                strSQL.Append(" ) ");
            }



            strSQL.Append(" and ed.Nome like '%toxic%' ");
            strSQL.Append(" and em.IndResultado not in (0,3 ) ");

            strSQL.Append(" and tno_CPF is not null  and tno_CPF <> ''   and em.DataExame between convert( smalldatetime, '" + zData1 + "', 103 ) and convert( smalldatetime, '" + zData2 + "', 103 )  ");



            strSQL.Append(" ) ");
            strSQL.Append(" as tx90 ");
            strSQL.Append(" left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tbl_eSocial_Deposito as Dep  on(tx90.IdExameBase = Dep.nId_Chave and tx90.CPF = Dep.CPF) ");
            strSQL.Append(" left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEmpregado as Emp on(tx90.CPF = Emp.tNO_CPF and Emp.nID_EMPR = tx90.IdPessoa) ");
            strSQL.Append(" left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tbl_eSocial as eS on(Dep.IdeSocial = eS.IdeSocial) ");
            strSQL.Append(" left join Usuario as Us on (eS.IdUsuario = Us.IdUsuario) ");




            strSQL.Append(" ) as tx99 ");

            if (zTipoCarga == "0")
            {
                strSQL.Append(" where Criado_Em is null ");
            }
            else if (zTipoCarga == "1")
            {
                strSQL.Append(" where Criado_Em is not null ");
            }
            else if (zTipoCarga == "2")
            {
                strSQL.Append(" where Enviado_Em is null and Criado_Em is not null ");
            }
            else if (zTipoCarga == "3")
            {
                strSQL.Append(" where Enviado_Em is not null ");
            }
            else
            {
                strSQL.Append(" where Colaborador is not null ");
            }

            if (zNome.Trim() != "")
            {
                strSQL.Append(" and Colaborador like '%" + zNome + "%'  ");
            }

            strSQL.Append(" order by Empresa, Colaborador, NomeArquivo ");



            using (SqlConnection cnn = new SqlConnection(Ilitera.Data.SQLServer.EntitySQLServer.GetConnection()))
            {
                SqlDataAdapter da;
                cnn.Open();


                da = new SqlDataAdapter(strSQL.ToString(), cnn);
                da.SelectCommand.CommandTimeout = 6000;
                da.Fill(m_ds, "Result");

                cnn.Close();

                da.Dispose();
            }

            return m_ds;

        }









        public DataSet Mensageria_2210(Int32 xIdEmpresa, Int32 xIdEmpregado, Int32 xIdEmpresaGrupo, string zData1, string zData2, Int32 zIdUsuario, string zTipoCarga, string zNome, string zGrupo, string zAmbiente)
        {
            //string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection();
            StringBuilder strSQL = new StringBuilder();

            //DataSet m_ds;


            DataTable table = new DataTable("Result");

            table.Columns.Add("CPF", Type.GetType("System.String"));
            table.Columns.Add("Reabertura", Type.GetType("System.Boolean"));
            table.Columns.Add("CNPJ", Type.GetType("System.String"));
            table.Columns.Add("Empresa", Type.GetType("System.String"));
            table.Columns.Add("Colaborador", Type.GetType("System.String"));
            table.Columns.Add("DtAcidente", Type.GetType("System.String"));
            table.Columns.Add("IdeSocial_Deposito", Type.GetType("System.Int32"));
            table.Columns.Add("IdeSocial", Type.GetType("System.Int32"));
            table.Columns.Add("IdPessoa", Type.GetType("System.Int32"));
            table.Columns.Add("NomeArquivo", Type.GetType("System.String"));
            table.Columns.Add("NomeUsuario", Type.GetType("System.String"));
            table.Columns.Add("Criado_Por", Type.GetType("System.String"));
            table.Columns.Add("Criado_Em", Type.GetType("System.String"));
            table.Columns.Add("Status", Type.GetType("System.String"));
            table.Columns.Add("IdEmpregado", Type.GetType("System.Int32"));
            table.Columns.Add("IdAcidente", Type.GetType("System.Int32"));
            table.Columns.Add("nrCatOrig", Type.GetType("System.String"));
            table.Columns.Add("checkbox", Type.GetType("System.Boolean"));
            table.Columns.Add("Enviado_Em", Type.GetType("System.String"));
            table.Columns.Add("Agendado", Type.GetType("System.String"));

            

            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);

            strSQL.Append(" Select  * from (  ");

            strSQL.Append(" select distinct tx90.IdAcidente, tx90.nrCatOrig, tx90.Empresa, Emp.tno_Empg as Colaborador, tx90.CPF, tx90.IdEmpregado,  tx90.PIS, tx90.CNPJ, tx90.DtAcidente, tx90.IdPessoa, tx90.DataAcidente,  ");
            strSQL.Append("                 Dep.IdeSocial_Deposito, Dep.IdeSocial, Dep.NomeArquivo, case when Us.NomeUsuario is null then '(Pendente)' when Us.NomeUsuario is not null then Us.NomeUsuario end as Criado_Por, convert(char(10), eS.DataHora_Criacao, 103) as Criado_Em , 0 as checkbox, tx90.Reabertura,  ");
            //strSQL.Append("                ( select top 1 ev.Data_Envio ");
            //strSQL.Append("   from " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tbl_eSocial_Envio as eV ");
            //strSQL.Append("   where Ev.IdeSocial_Deposito = Dep.IdeSocial_Deposito ");
            //strSQL.Append("   order by ev.Data_Envio desc ");

            //strSQL.Append("                ( select top 1 'Enviado em ' + convert( char(10),ev.Data_Envio,103) + ' - ' + case when processamento_retorno like '%<nrrecibo>%' then 'Recibo gerado' when processamento_retorno like '%Lote aguardando%' then 'Aguardando Processamento'  else 'Não processado'  end as Status ");
            //strSQL.Append("   from " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tbl_eSocial_Envio as eV ");
            //strSQL.Append("   where Ev.IdeSocial_Deposito = Dep.IdeSocial_Deposito ");
            //strSQL.Append("   order by ev.Data_Envio desc ");
            //strSQL.Append(" )  as Enviado_Em, ");

            strSQL.Append("                ( select top 1 'Enviado em ' + convert( char(10),ev.Data_Envio,103) + ' - ' +  ");
            strSQL.Append("  case when processamento_retorno like '%<nrRecibo>%' then 'Recibo gerado ' + substring(processamento_retorno, charindex('<nrRecibo>', Processamento_Retorno) + 10, 23) ");
            strSQL.Append("  when processamento_retorno like '%Lote aguardando%'  or processamento_retorno like '%encontra-se em processamento%'  then 'Aguardando Processamento' ");
            strSQL.Append("  when processamento_retorno like '%Contrato de Trabalho%' then 'Contrato de trabalho não localizado' ");
            strSQL.Append("  when processamento_retorno like '%estrutura do arquivo XML%' then 'Problema na estrutura do XML' ");
            strSQL.Append("  when processamento_retorno like '%duplicidade%' then 'Duplicidade na carga do evento' ");
            strSQL.Append("  when processamento_retorno like '%cpftrab%' then 'CPF do colaborador inválido' ");
            strSQL.Append("  when processamento_retorno like '%perfil de procuração eletr%' then 'Erro na procuração eletrônica' ");
            strSQL.Append("  when processamento_retorno like '%cpfresp%' then 'CPF do responsável inválido' ");            
            strSQL.Append("  else 'Não processado'  end  ");
            strSQL.Append("   from " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tbl_eSocial_Envio as eV ");
            strSQL.Append("   where Ev.IdeSocial_Deposito = Dep.IdeSocial_Deposito ");
            strSQL.Append("   order by ev.Data_Envio desc ");
            strSQL.Append(" )  as Enviado_Em, ");

            strSQL.Append(" isnull((select top 1 'X' from " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tbl_eSocial_Schedule as eS where eS.IdeSocial_Deposito = Dep.IdeSocial_Deposito and Processado_Em is null), ' ' ) as Agendado ");

            strSQL.Append(" from (  ");


            strSQL.Append(" select a.IdAcidente, dbo.udf_getnumeric(c.tno_CPF) as CPF, c.nNO_PIS_PASEP as PIS,  c.tCod_Empr as Matricula, dbo.udf_getnumeric(d.NomeCodigo) as CNPJ,  convert( char(10), DataAcidente, 126 ) as DtAcidente, DataAcidente,  ");
            strSQL.Append("        codigo_acidente_trabalho as tpAcid, CONVERT(VARCHAR(5), DataAcidente, 108) as hrAcid, hrsTrabAntesAcid,  ");
            strSQL.Append("        b.IndTipoCAT as tpCat,  case when b.hasMorte = 0 then 'N'  when b.hasMorte = 1 then 'S' end as IndCatObito,  ");
            strSQL.Append(" 	   case when b.DataObito is null then ''  when b.DataObito is not null then convert(char(10), b.DataObito, 126) end as dtObito,  ");
            strSQL.Append(" 	   case when hasregPolicial = 0 then 'N'  when hasregPolicial = 1 then 'S' end as IndComumPolicia,  ");
            strSQL.Append("        a.codigo_agente_Causador as CodSitGeradora, IdIniciativaCat as IniciatCat, Observacoes as Observacao, IdTipoLocal as TpLocal, DscLocal,  ");
            strSQL.Append("        a.Bairro, a.CEP, substring(a.Complemento,1,30) as Complemento, a.Logradouro as dscLograd, a.Nr_Logradouro as NrLograd, a.Municipio as CodMunic, a.UF as UF,  ");
            strSQL.Append(" 	   case when IdlocalAcidente is null then  dbo.udf_getnumeric(d.NomeCodigo)  when IdLocalAcidente is not null then dbo.udf_getnumeric(e.NomeCodigo) end as cnpjLocalAcid, '' as pais, CodPostal,  ");
            strSQL.Append("        case when a.Codigo_Parte_Corpo_Atingida is null then '' else a.Codigo_Parte_Corpo_Atingida end as CodParteAting, IdLateralidade as lateralidade, a.Codigo_Situacao_Geradora as CodAgntCausador,  ");
            strSQL.Append(" 	   case when CNES is null then '' when CNES is not null then CNES end as CNES,  ");
            strSQL.Append(" 	   case when DataInternacao is null then '' when DataInternacao is not null then convert(char(10), DataInternacao, 126) end as DtAtendimento,  ");
            strSQL.Append(" 	   case when DataInternacao is null then '0000' when DataInternacao is not null then convert(varchar(5), DataInternacao, 108) end as hrAtendimento,  ");
            strSQL.Append(" 	   case when HasInternacao = 0 then 'N' when HasInternacao = 1 then 'S' end as IntInternacao, DuracaoInternacao as DurTrat,  ");
            strSQL.Append(" 	   case when hasAfastamento = 0 then 'N' when hasAfastamento = 1 then 'S' end as IndAFast, a.codigo_descricao_lesao as dscLesao, a.Descricao as dscCompLesao,  ");
            strSQL.Append(" 	   case when f.CodigoCid is null then '' when f.CodigoCid is not null then f.CodigoCid end as CodCID, MedicoInternacao as nmEmit, 1 as IdeOC, CRMInternacao as nrOC,  ");
            strSQL.Append("        UfInternacao as UfOC, convert(char(10), DataEmissao, 126) as dtCatOrig, NumeroCAT as nrCatOrig, DiagnosticoProvavel, c.nId_Empregado as IdEmpregado, d.NomeAbreviado as Empresa, d.IdPessoa, Reabertura ");
            

            strSQL.Append(" from acidente as a left join cat as b  ");
            strSQL.Append(" on(a.idcat = b.idcat)  ");
            strSQL.Append(" left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblempregado as c  ");
            strSQL.Append(" on(a.idempregado = c.nid_empregado)  ");
            strSQL.Append(" left join pessoa as d ");
            strSQL.Append(" on(c.nid_empr = d.idpessoa) ");
            strSQL.Append(" left join Pessoa as e ");
            strSQL.Append(" on(IdLocalAcidente = e.IdPessoa) ");
            strSQL.Append(" left join Cid as f ");
            strSQL.Append(" on(a.idcid = f.idcid) ");


            strSQL.Append(" where a.IdEmpregado in ");

            if (zGrupo == "N")
            {
                strSQL.Append(" (select nId_Empregado from  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEMPREGADO_Funcao ");
                strSQL.Append(" where nid_Empr = " + xIdEmpresa.ToString() + " ");
                strSQL.Append(" ) ");
            }
            else
            {
                strSQL.Append(" (select nId_Empregado from  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEMPREGADO_Funcao ");
                strSQL.Append(" where nId_Empr=" + xIdEmpresa.ToString() + " or nid_Empr in ( select IdPessoa from Juridica where IdGrupoEmpresa in ( select IdGrupoEmpresa from Juridica where Idpessoa =  " + xIdEmpresa.ToString() + " )  ) ");
                strSQL.Append(" ) ");
            }


            //strSQL.Append(" and a.IdCat is not null and tno_CPF is not null  and tno_CPF <> ''    and a.DataAcidente between convert( smalldatetime, '" + zData1 + "', 103 ) and convert( smalldatetime, '" + zData2 + "', 103 )  ");
            strSQL.Append(" and tno_CPF is not null  and tno_CPF <> ''   and gTerceiro=0   and a.DataAcidente between convert( smalldatetime, '" + zData1 + "', 103 ) and convert( smalldatetime, '" + zData2 + "', 103 )  ");


            strSQL.Append(" ) ");
            strSQL.Append(" as tx90 ");

            strSQL.Append(" left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tbl_eSocial_Deposito as Dep  on(tx90.IdAcidente = Dep.nId_Chave and tx90.CPF = Dep.CPF and Dep.IdEsocial in ( select IdESocial from  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tbl_eSocial where Ambiente = '" + zAmbiente + "' ) ) ");
            strSQL.Append(" left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEmpregado as Emp on(tx90.CPF = Emp.tNO_CPF and Emp.nID_EMPR = tx90.IdPessoa and tx90.IdEmpregado = emp.nID_EMPREGADO ) ");
            strSQL.Append(" left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tbl_eSocial as eS on(Dep.IdeSocial = eS.IdeSocial) ");
            strSQL.Append(" left join Usuario as Us on (eS.IdUsuario = Us.IdUsuario) ");


            strSQL.Append(" ) as tx99 ");

            if (zTipoCarga == "0")
            {
                strSQL.Append(" where Criado_Em is null ");
            }
            else if (zTipoCarga == "1")
            {
                strSQL.Append(" where Criado_Em is not null ");
            }
            else if (zTipoCarga == "2")
            {
                strSQL.Append(" where Enviado_Em is null and Criado_Em is not null ");
            }
            else if (zTipoCarga == "3")
            {
                strSQL.Append(" where Enviado_Em is not null ");
            }
            else
            {
                strSQL.Append(" where Colaborador is not null ");
            }

            if (zNome.Trim() != "")
            {
                strSQL.Append(" and Colaborador like '%" + zNome + "%'  ");
            }


            strSQL.Append(" order by Empresa, Colaborador, dataAcidente desc ");



            using (SqlConnection cnn = new SqlConnection(Ilitera.Data.SQLServer.EntitySQLServer.GetConnection()))
            {
                SqlDataAdapter da;
                cnn.Open();


                da = new SqlDataAdapter(strSQL.ToString(), cnn);
                da.SelectCommand.CommandTimeout = 600;
                da.Fill(m_ds, "Result");

                cnn.Close();

                da.Dispose();
            }

            return m_ds;

        }




        public DataSet Mensageria_2210_CSV(Int32 xIdEmpresa, Int32 xIdEmpregado, Int32 xIdEmpresaGrupo, string zData1, string zData2, Int32 zIdUsuario, string zTipoCarga, string zNome, string zGrupo, string zAmbiente)
        {
            //string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection();
            StringBuilder strSQL = new StringBuilder();

            //DataSet m_ds;


            DataTable table = new DataTable("Result");

            table.Columns.Add("CPF", Type.GetType("System.String"));
            table.Columns.Add("Reabertura", Type.GetType("System.String"));
            //table.Columns.Add("PIS", Type.GetType("System.String"));
            table.Columns.Add("CNPJ", Type.GetType("System.String"));
            table.Columns.Add("Empresa", Type.GetType("System.String"));
            table.Columns.Add("Colaborador", Type.GetType("System.String"));
            table.Columns.Add("DtAcidente", Type.GetType("System.String"));
            table.Columns.Add("Matricula", Type.GetType("System.String"));            
            //table.Columns.Add("IdeSocial_Deposito", Type.GetType("System.Int32"));
            //table.Columns.Add("IdeSocial", Type.GetType("System.Int32"));
            //table.Columns.Add("IdPessoa", Type.GetType("System.Int32"));
            //table.Columns.Add("NomeArquivo", Type.GetType("System.String"));
            //table.Columns.Add("NomeUsuario", Type.GetType("System.String"));
            //table.Columns.Add("Criado_Por", Type.GetType("System.String"));
            //table.Columns.Add("Criado_Em", Type.GetType("System.String"));
            table.Columns.Add("Status", Type.GetType("System.String"));
            //table.Columns.Add("IdEmpregado", Type.GetType("System.Int32"));
            //table.Columns.Add("IdAcidente", Type.GetType("System.Int32"));
            //table.Columns.Add("nrCatOrig", Type.GetType("System.String"));
            //table.Columns.Add("checkbox", Type.GetType("System.Boolean"));
            table.Columns.Add("Enviado_Em", Type.GetType("System.String"));
            //table.Columns.Add("Agendado", Type.GetType("System.String"));


            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);

            strSQL.Append(" Select  * from (  ");

            strSQL.Append(" select distinct tx90.IdAcidente, tx90.nrCatOrig, tx90.Empresa, Emp.tno_Empg as Colaborador, tx90.CPF, tx90.IdEmpregado,  tx90.PIS, tx90.CNPJ, tx90.DtAcidente, tx90.IdPessoa, tx90.DataAcidente, tx90.Matricula,  ");
            strSQL.Append("                 Dep.IdeSocial_Deposito, Dep.IdeSocial, Dep.NomeArquivo, case when Us.NomeUsuario is null then '(Pendente)' when Us.NomeUsuario is not null then Us.NomeUsuario end as Criado_Por, convert(char(10), eS.DataHora_Criacao, 103) as Criado_Em , 0 as checkbox, tx90.Reabertura, ");
            //strSQL.Append("                ( select top 1 ev.Data_Envio ");
            //strSQL.Append("   from " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tbl_eSocial_Envio as eV ");
            //strSQL.Append("   where Ev.IdeSocial_Deposito = Dep.IdeSocial_Deposito ");
            //strSQL.Append("   order by ev.Data_Envio desc ");

            //strSQL.Append("                ( select top 1 'Enviado em ' + convert( char(10),ev.Data_Envio,103) + ' - ' + case when processamento_retorno like '%<nrrecibo>%' then 'Recibo gerado' when processamento_retorno like '%Lote aguardando%' then 'Aguardando Processamento'  else 'Não processado'  end as Status ");
            //strSQL.Append("   from " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tbl_eSocial_Envio as eV ");
            //strSQL.Append("   where Ev.IdeSocial_Deposito = Dep.IdeSocial_Deposito ");
            //strSQL.Append("   order by ev.Data_Envio desc ");
            //strSQL.Append(" )  as Enviado_Em, ");

            strSQL.Append("                ( select top 1 'Enviado em ' + convert( char(10),ev.Data_Envio,103) + ' - ' +  ");
            strSQL.Append("  case when processamento_retorno like '%<nrRecibo>%' then 'Recibo gerado ' + substring(processamento_retorno, charindex('<nrRecibo>', Processamento_Retorno) + 10, 23) ");
            strSQL.Append("  when processamento_retorno like '%Lote aguardando%'  or processamento_retorno like '%encontra-se em processamento%'  then 'Aguardando Processamento' ");
            strSQL.Append("  when processamento_retorno like '%Contrato de Trabalho%' then 'Contrato de trabalho não localizado' ");
            strSQL.Append("  when processamento_retorno like '%estrutura do arquivo XML%' then 'Problema na estrutura do XML' ");
            strSQL.Append("  when processamento_retorno like '%duplicidade%' then 'Duplicidade na carga do evento' ");
            strSQL.Append("  when processamento_retorno like '%cpftrab%' then 'CPF do colaborador inválido' ");
            strSQL.Append("  when processamento_retorno like '%perfil de procuração eletr%' then 'Erro na procuração eletrônica' ");
            strSQL.Append("  when processamento_retorno like '%cpfresp%' then 'CPF do responsável inválido' ");            
            strSQL.Append("  else 'Não processado'  end  ");
            strSQL.Append("   from " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tbl_eSocial_Envio as eV ");
            strSQL.Append("   where Ev.IdeSocial_Deposito = Dep.IdeSocial_Deposito ");
            strSQL.Append("   order by ev.Data_Envio desc ");
            strSQL.Append(" )  as Enviado_Em, ");

            strSQL.Append(" isnull((select top 1 'X' from " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tbl_eSocial_Schedule as eS where eS.IdeSocial_Deposito = Dep.IdeSocial_Deposito and Processado_Em is null), ' ' ) as Agendado ");

            strSQL.Append(" from (  ");


            strSQL.Append(" select a.IdAcidente, dbo.udf_getnumeric(c.tno_CPF) as CPF, c.nNO_PIS_PASEP as PIS,  c.tCod_Empr as Matricula, d.NomeCodigo as CNPJ,  convert( char(10), DataAcidente, 126 ) as DtAcidente, DataAcidente,  ");
            strSQL.Append("        codigo_acidente_trabalho as tpAcid, CONVERT(VARCHAR(5), DataAcidente, 108) as hrAcid, hrsTrabAntesAcid,  ");
            strSQL.Append("        b.IndTipoCAT as tpCat,  case when b.hasMorte = 0 then 'N'  when b.hasMorte = 1 then 'S' end as IndCatObito,  ");
            strSQL.Append(" 	   case when b.DataObito is null then ''  when b.DataObito is not null then convert(char(10), b.DataObito, 126) end as dtObito,  ");
            strSQL.Append(" 	   case when hasregPolicial = 0 then 'N'  when hasregPolicial = 1 then 'S' end as IndComumPolicia,  ");
            strSQL.Append("        a.codigo_agente_Causador as CodSitGeradora, IdIniciativaCat as IniciatCat, Observacoes as Observacao, IdTipoLocal as TpLocal, DscLocal,  ");
            strSQL.Append("        a.Bairro, a.CEP, substring(a.Complemento,1,30) as Complemento, a.Logradouro as dscLograd, a.Nr_Logradouro as NrLograd, a.Municipio as CodMunic, a.UF as UF,  ");
            strSQL.Append(" 	   case when IdlocalAcidente is null then  dbo.udf_getnumeric(d.NomeCodigo)  when IdLocalAcidente is not null then dbo.udf_getnumeric(e.NomeCodigo) end as cnpjLocalAcid, '' as pais,  dbo.udf_getnumeric(CEP) as CodPostal,  ");
            strSQL.Append("        case when a.Codigo_Parte_Corpo_Atingida is null then '' else a.Codigo_Parte_Corpo_Atingida end as CodParteAting, IdLateralidade as lateralidade, a.Codigo_Situacao_Geradora as CodAgntCausador,  ");
            strSQL.Append(" 	   case when CNES is null then '' when CNES is not null then CNES end as CNES,  ");
            strSQL.Append(" 	   case when DataInternacao is null then '' when DataInternacao is not null then convert(char(10), DataInternacao, 126) end as DtAtendimento,  ");
            strSQL.Append(" 	   case when DataInternacao is null then '0000' when DataInternacao is not null then convert(varchar(5), DataInternacao, 108) end as hrAtendimento,  ");
            strSQL.Append(" 	   case when HasInternacao = 0 then 'N' when HasInternacao = 1 then 'S' end as IntInternacao, DuracaoInternacao as DurTrat,  ");
            strSQL.Append(" 	   case when hasAfastamento = 0 then 'N' when hasAfastamento = 1 then 'S' end as IndAFast, a.codigo_descricao_lesao as dscLesao, a.Descricao as dscCompLesao,  ");
            strSQL.Append(" 	   case when f.CodigoCid is null then '' when f.CodigoCid is not null then f.CodigoCid end as CodCID, MedicoInternacao as nmEmit, 1 as IdeOC, CRMInternacao as nrOC,  ");
            strSQL.Append("        UfInternacao as UfOC, convert(char(10), DataEmissao, 126) as dtCatOrig, NumeroCAT as nrCatOrig, DiagnosticoProvavel, c.nId_Empregado as IdEmpregado, d.NomeAbreviado as Empresa, d.IdPessoa, ");
            strSQL.Append("        case when Reabertura=1 then 'Sim' else 'Não' end as Reabertura ");

            strSQL.Append(" from acidente as a left join cat as b  ");
            strSQL.Append(" on(a.idcat = b.idcat)  ");
            strSQL.Append(" left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblempregado as c  ");
            strSQL.Append(" on(a.idempregado = c.nid_empregado)  ");
            strSQL.Append(" left join pessoa as d ");
            strSQL.Append(" on(c.nid_empr = d.idpessoa) ");
            strSQL.Append(" left join Pessoa as e ");
            strSQL.Append(" on(IdLocalAcidente = e.IdPessoa) ");
            strSQL.Append(" left join Cid as f ");
            strSQL.Append(" on(a.idcid = f.idcid) ");


            strSQL.Append(" where a.IdEmpregado in ");

            if (zGrupo == "N")
            {
                strSQL.Append(" (select nId_Empregado from  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEMPREGADO_Funcao ");
                strSQL.Append(" where nid_Empr = " + xIdEmpresa.ToString() + " ");
                strSQL.Append(" ) ");
            }
            else
            {
                strSQL.Append(" (select nId_Empregado from  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEMPREGADO_Funcao ");
                strSQL.Append(" where nId_Empr=" + xIdEmpresa.ToString() + " or nid_Empr in ( select IdPessoa from Juridica where IdGrupoEmpresa in ( select IdGrupoEmpresa from Juridica where Idpessoa =  " + xIdEmpresa.ToString() + " )  ) ");
                strSQL.Append(" ) ");
            }


            //strSQL.Append(" and a.IdCat is not null and tno_CPF is not null  and tno_CPF <> ''    and a.DataAcidente between convert( smalldatetime, '" + zData1 + "', 103 ) and convert( smalldatetime, '" + zData2 + "', 103 )  ");
            strSQL.Append(" and tno_CPF is not null  and tno_CPF <> ''   and gTerceiro=0   and a.DataAcidente between convert( smalldatetime, '" + zData1 + "', 103 ) and convert( smalldatetime, '" + zData2 + "', 103 )  ");


            strSQL.Append(" ) ");
            strSQL.Append(" as tx90 ");

            strSQL.Append(" left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tbl_eSocial_Deposito as Dep  on(tx90.IdAcidente = Dep.nId_Chave and tx90.CPF = Dep.CPF and Dep.IdEsocial in ( select IdESocial from  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tbl_eSocial where Ambiente = '" + zAmbiente + "' ) ) ");
            strSQL.Append(" left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEmpregado as Emp on(tx90.CPF = Emp.tNO_CPF and Emp.nID_EMPR = tx90.IdPessoa and tx90.IdEmpregado = emp.nID_EMPREGADO ) ");
            strSQL.Append(" left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tbl_eSocial as eS on(Dep.IdeSocial = eS.IdeSocial) ");
            strSQL.Append(" left join Usuario as Us on (eS.IdUsuario = Us.IdUsuario) ");


            strSQL.Append(" ) as tx99 ");

            if (zTipoCarga == "0")
            {
                strSQL.Append(" where Criado_Em is null ");
            }
            else if (zTipoCarga == "1")
            {
                strSQL.Append(" where Criado_Em is not null ");
            }
            else if (zTipoCarga == "2")
            {
                strSQL.Append(" where Enviado_Em is null and Criado_Em is not null ");
            }
            else if (zTipoCarga == "3")
            {
                strSQL.Append(" where Enviado_Em is not null ");
            }
            else
            {
                strSQL.Append(" where Colaborador is not null ");
            }

            if (zNome.Trim() != "")
            {
                strSQL.Append(" and Colaborador like '%" + zNome + "%'  ");
            }


            strSQL.Append(" order by Empresa, Colaborador, dataAcidente desc ");



            using (SqlConnection cnn = new SqlConnection(Ilitera.Data.SQLServer.EntitySQLServer.GetConnection()))
            {
                SqlDataAdapter da;
                cnn.Open();


                da = new SqlDataAdapter(strSQL.ToString(), cnn);
                da.SelectCommand.CommandTimeout = 600;
                da.Fill(m_ds, "Result");

                cnn.Close();

                da.Dispose();
            }

            return m_ds;

        }





        public DataSet Mensageria_1060(Int32 xIdEmpresa, Int32 xIdEmpregado, Int32 xIdEmpresaGrupo, string zData1, string zData2, Int32 zIdUsuario, string zTipoCarga, string zNome, string zGrupo)
        {
            //string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection();
            StringBuilder strSQL = new StringBuilder();

            //DataSet m_ds;



            DataTable table = new DataTable("Result");

            table.Columns.Add("GHE", Type.GetType("System.String"));
            table.Columns.Add("PIS", Type.GetType("System.String"));
            table.Columns.Add("CNPJ", Type.GetType("System.String"));
            table.Columns.Add("Empresa", Type.GetType("System.String"));
            table.Columns.Add("Colaborador", Type.GetType("System.String"));
            table.Columns.Add("CodAmb", Type.GetType("System.String"));
            table.Columns.Add("NmAmb", Type.GetType("System.String"));
            table.Columns.Add("NrInsc", Type.GetType("System.String"));
            table.Columns.Add("DscAmb", Type.GetType("System.String"));
            table.Columns.Add("DtLaudo", Type.GetType("System.String"));
            table.Columns.Add("DtIniCondicao", Type.GetType("System.String"));
            table.Columns.Add("DtFimCondicao", Type.GetType("System.String"));
            table.Columns.Add("IdeSocial_Deposito", Type.GetType("System.Int32"));
            table.Columns.Add("IdeSocial", Type.GetType("System.Int32"));
            table.Columns.Add("IdPessoa", Type.GetType("System.Int32"));
            table.Columns.Add("NomeArquivo", Type.GetType("System.String"));
            table.Columns.Add("NomeUsuario", Type.GetType("System.String"));
            table.Columns.Add("Criado_Por", Type.GetType("System.String"));
            table.Columns.Add("Criado_Em", Type.GetType("System.String"));
            table.Columns.Add("IdEmpregado", Type.GetType("System.Int32"));
            table.Columns.Add("checkbox", Type.GetType("System.Boolean"));
            table.Columns.Add("Enviado_Em", Type.GetType("System.String"));
            table.Columns.Add("nId_Laud_Tec", Type.GetType("System.Int32"));


            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);


            strSQL.Append(" Select  * from (  ");

            strSQL.Append(" select distinct tx90.Empresa, CodAmb, '' as Colaborador, tx90.NmAmb, 0 as IdEmpregado,  '' as PIS, tx90.CNPJ, tx90.DtIniCondicao, tx90.DtFimCondicao, tx90.nId_Laud_Tec, tx90.IdPessoa,  ");
            strSQL.Append("                 Dep.IdeSocial_Deposito, Dep.IdeSocial, Dep.NomeArquivo, case when Us.NomeUsuario is null then '(Pendente)' when Us.NomeUsuario is not null then Us.NomeUsuario end as Criado_Por, convert(char(20), eS.DataHora_Criacao, 113) as Criado_Em, 0 as checkbox,  ");
            strSQL.Append("                ( select top 1 ev.Data_Envio ");
            strSQL.Append("   from " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tbl_eSocial_Envio as eV ");
            strSQL.Append("   where Ev.IdeSocial_Deposito = Dep.IdeSocial_Deposito ");
            strSQL.Append("   order by ev.Data_Envio desc ");
            strSQL.Append(" )  as Enviado_Em ");

            strSQL.Append(" from (  ");

            strSQL.Append("select nid_func as CodAmb, tno_Func as NmAmb, dbo.udf_getnumeric(c.NomeCodigo) as CNPJ, convert( varchar(max), tds_local_trab  COLLATE sql_latin1_general_cp1251_ci_as ) as DscAmb, ");
            strSQL.Append("convert(char(10), hdt_Laudo, 126) as DtIniCondicao, convert(char(10), dateadd(dd, 364, hdt_Laudo), 126) as DtFimCondicao, c.NomeAbreviado as Empresa, c.IdPessoa, b.nId_Laud_Tec   ");
            strSQL.Append("from tblfunc as a  ");
            strSQL.Append("left join  tblLaudo_tec as b on ( a.nid_Laud_tec = b.nId_Laud_tec ) ");
            strSQL.Append("left join  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.Pessoa as c on ( b.nId_Empr = c.IdPessoa )  ");
            strSQL.Append("where a.nid_laud_tec in ");
            strSQL.Append("( select nid_laud_tec from tbllaudo_tec ");

            if (zGrupo == "N")
            {
                strSQL.Append("      where nid_empr = " + xIdEmpresa.ToString() + " ");
            }
            else
            {
                strSQL.Append("      where ( nid_empr = " + xIdEmpresa.ToString() + " or nid_empr in ( select IdPessoa from  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.Juridica where idGrupoEmpresa in ( Select IdGrupoEmpresa from " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.Juridica  where idPessoa = " + xIdEmpresa.ToString() + " ) )  ) ");

            }
            strSQL.Append(") ");

            strSQL.Append("  and hdt_laudo is not null   and hdt_Laudo between convert( smalldatetime, '" + zData1 + "', 103 ) and convert( smalldatetime, '" + zData2 + "', 103 )  ");
            strSQL.Append("  and nId_Pedido in ( select IdPedido from " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.Pedido where IndStatus = 2 ) ");

            strSQL.Append(" ) ");
            strSQL.Append(" as tx90 ");
            strSQL.Append(" left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tbl_eSocial_Deposito as Dep  on ( tx90.CodAmb = Dep.nId_Chave ) ");
            strSQL.Append(" left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tbl_eSocial as eS on (Dep.IdeSocial = eS.IdeSocial) ");
            strSQL.Append(" left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.Usuario as Us on (eS.IdUsuario = Us.IdUsuario) ");

            strSQL.Append(" ) as tx99 ");

            if (zTipoCarga == "0")
            {
                strSQL.Append(" where Criado_Em is null ");
            }
            else if (zTipoCarga == "1")
            {
                strSQL.Append(" where Criado_Em is not null ");
            }
            else if (zTipoCarga == "2")
            {
                strSQL.Append(" where Enviado_Em is null and Criado_Em is not null ");
            }
            else if (zTipoCarga == "3")
            {
                strSQL.Append(" where Enviado_Em is not null ");
            }
            else
            {
                strSQL.Append(" where NmAmb is not null ");
            }

            if (zNome.Trim() != "")
            {
                strSQL.Append(" and NmAmb like '%" + zNome + "%'  ");
            }

            strSQL.Append(" order by Empresa, NmAmb, NomeArquivo ");



            using (SqlConnection cnn = new SqlConnection(Ilitera.Data.SQLServer.EntitySQLServer.GetConnection_Sied()))
            {
                SqlDataAdapter da;
                cnn.Open();


                da = new SqlDataAdapter(strSQL.ToString(), cnn);
                da.SelectCommand.CommandTimeout = 600;
                da.Fill(m_ds, "Result");

                cnn.Close();

                da.Dispose();
            }

            return m_ds;

        }




        public void XML_Deposito(string xArquivo, int xIdeSocial, int xSeq, string xXML, string xCPF, Int64 xNIT, string xCNPJ, string xDataIni, string xDataFim, Int32 xIdChave, string xID, Int32 xId_Func_Empregado)
        {

            //chaves para cada evento
            //nid_func      1060     1
            //IdAcidente    2210     1
            //IdExameBase / IdPCMSO  2220    1
            //IdExameBase  2221     1
            //IdAfastamento  2230   1
            //nid_laud_tec   2240   1
            //IdTreinamento  2245   1

            using (SqlConnection cnn = new SqlConnection(Ilitera.Data.SQLServer.EntitySQLServer.GetConnection_Sied()))
            {
                cnn.Open();

                //SqlCommand com = new SqlCommand("INSERT INTO tbl_eSocial_Deposito( IdEsocial_Deposito, IdeSocial, XMLData, NomeArquivo, DataHora, Seq, CPF, NIT, CNPJ, Dt_Ini, Dt_Fim, nId_Chave ) " +
                //                                "SELECT convert( int, rand() * 1000000000 )," + xIdeSocial.ToString() + " , CONVERT(XML, N'" + xXML + "' , 2 ), '" + xArquivo + "', GETDATE(), " + xSeq.ToString() + " , '" + xCPF + "', " + xNIT.ToString() + " , '" + xCNPJ + "', convert( smalldatetime,'" + xDataIni + "', 103 ), convert( smalldatetime, '" + xDataFim + "', 103 ),  " + xIdChave.ToString(), cnn);
                SqlCommand com = new SqlCommand("INSERT INTO tbl_eSocial_Deposito( IdEsocial_Deposito, IdeSocial, XMLData, NomeArquivo, DataHora, Seq, CPF, NIT, CNPJ, Dt_Ini, Dt_Fim, nId_Chave, ID, nId_Func_Empregado ) " +
                "SELECT convert( int, rand() * 1000000000 )," + xIdeSocial.ToString() + " , N'" + xXML + "', '" + xArquivo + "', GETDATE(), " + xSeq.ToString() + " , '" + xCPF + "', " + xNIT.ToString() + " , '" + xCNPJ + "', convert( smalldatetime,'" + xDataIni + "', 103 ), convert( smalldatetime, '" + xDataFim + "', 103 ), " + xIdChave.ToString() + ", '" + xID + "', " + xId_Func_Empregado.ToString() + " ", cnn);

                com.ExecuteNonQuery();
            }
            
            return;

        }


        public void XML_Deposito(string zCod, string xArquivo, int xIdeSocial, int xSeq, string xXML, string xCPF, Int64 xNIT, string xCNPJ, string xDataIni, string xDataFim, Int32 xIdChave, string xID, Int32 xId_Func_Empregado)
        {

            //chaves para cada evento
            //nid_func      1060     1
            //IdAcidente    2210     1
            //IdExameBase / IdPCMSO  2220    1
            //IdExameBase  2221     1
            //IdAfastamento  2230   1
            //nid_laud_tec   2240   1
            //IdTreinamento  2245   1

            using (SqlConnection cnn = new SqlConnection(Ilitera.Data.SQLServer.EntitySQLServer.GetConnection_Sied()))
            {
                cnn.Open();

                //SqlCommand com = new SqlCommand("INSERT INTO tbl_eSocial_Deposito( IdEsocial_Deposito, IdeSocial, XMLData, NomeArquivo, DataHora, Seq, CPF, NIT, CNPJ, Dt_Ini, Dt_Fim, nId_Chave ) " +
                //                                "SELECT convert( int, rand() * 1000000000 )," + xIdeSocial.ToString() + " , CONVERT(XML, N'" + xXML + "' , 2 ), '" + xArquivo + "', GETDATE(), " + xSeq.ToString() + " , '" + xCPF + "', " + xNIT.ToString() + " , '" + xCNPJ + "', convert( smalldatetime,'" + xDataIni + "', 103 ), convert( smalldatetime, '" + xDataFim + "', 103 ),  " + xIdChave.ToString(), cnn);

                SqlCommand comdel = new SqlCommand("Delete tbl_eSocial_Deposito where IdEsocial_Deposito = " + zCod, cnn);

                comdel.ExecuteNonQuery();


                SqlCommand com = new SqlCommand("INSERT INTO tbl_eSocial_Deposito( IdEsocial_Deposito, IdeSocial, XMLData, NomeArquivo, DataHora, Seq, CPF, NIT, CNPJ, Dt_Ini, Dt_Fim, nId_Chave, ID, nId_Func_Empregado ) " +
                "SELECT " + zCod + ", " + xIdeSocial.ToString() + " , N'" + xXML + "', '" + xArquivo + "', GETDATE(), " + xSeq.ToString() + " , '" + xCPF + "', " + xNIT.ToString() + " , '" + xCNPJ + "', convert( smalldatetime,'" + xDataIni + "', 103 ), convert( smalldatetime, '" + xDataFim + "', 103 ), " + xIdChave.ToString() + " , '" + xID + "' , " + xId_Func_Empregado.ToString() + " ", cnn);

                com.ExecuteNonQuery();
            }


            return;

        }



        public void XML_Deposito_Excluir(Int32 xIdeSocial_Deposito)
        {

        
            using (SqlConnection cnn = new SqlConnection(Ilitera.Data.SQLServer.EntitySQLServer.GetConnection_Sied()))
            {
                cnn.Open();

                SqlCommand com = new SqlCommand("Delete tbl_eSocial_Deposito where IdeSocial_Deposito = " + xIdeSocial_Deposito.ToString(), cnn);
                com.ExecuteNonQuery();
            }


            return;

        }


        public DataSet Trazer_XML(Int32 xIdEsocial_Deposito)
        {
            //string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection();
            string strSQL = "";

            DataSet ds = new DataSet();



            strSQL = " select XMLData from tbl_eSocial_Deposito where IdeSocial_Deposito = " + xIdEsocial_Deposito.ToString() ;
            


            using (SqlConnection cnn = new SqlConnection(Ilitera.Data.SQLServer.EntitySQLServer.GetConnection_Sied()))
            {               
                cnn.Open();
                
                SqlCommand cmd = new SqlCommand(strSQL, cnn);

                // Read the XML data into a XML reader.
                System.Xml.XmlReader xr = cmd.ExecuteXmlReader();

                // Read the data from the XML reader into the DataSet.
                
                ds.ReadXml(xr, XmlReadMode.Fragment);
                xr.Close();
                cnn.Close();
            }

            return ds;

        }



        public DataSet Trazer_XML2(Int32 xIdEsocial_Deposito)
        {
            //string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection();
            StringBuilder strSQL = new StringBuilder();

            //DataSet m_ds;


            DataTable table = new DataTable("Result");

            table.Columns.Add("XMLData", Type.GetType("System.String"));

            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);


            strSQL.Append(" select XMLData from tbl_eSocial_Deposito where IdeSocial_Deposito = " + xIdEsocial_Deposito.ToString());

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



        public Int32 Trazer_Cont_ID(string xChave)
        {
            //string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection();
            StringBuilder strSQL = new StringBuilder();

            //DataSet m_ds;


            DataTable table = new DataTable("Result");

            table.Columns.Add("ID", Type.GetType("System.String"));

            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);


            strSQL.Append(" select top 1 substring( ID,32,5) as ID from tbl_eSocial_Deposito where ID like '" + xChave + "%' order by ID desc ");

            using (SqlConnection cnn = new SqlConnection(Ilitera.Data.SQLServer.EntitySQLServer.GetConnection_Sied()))
            {
                SqlDataAdapter da;
                cnn.Open();


                da = new SqlDataAdapter(strSQL.ToString(), cnn);
                da.Fill(m_ds, "Result");

                cnn.Close();

                da.Dispose();
            }


            if (m_ds.Tables[0].Rows.Count < 1 ) return 1;
            else   return System.Convert.ToInt32( m_ds.Tables[0].Rows[0]["ID"].ToString()) + 1;

        }


        public string Trazer_Recibo(Int32 xIdChave)
        {
            //string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection();
            StringBuilder strSQL = new StringBuilder();

            //DataSet m_ds;


            DataTable table = new DataTable("Result");

            table.Columns.Add("NrRecibo", Type.GetType("System.String"));

            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);


            strSQL.Append(" select max(Protocolo) as nrRecibo from tbl_esocial_envio ");
            strSQL.Append(" where idesocial_deposito in ");
            strSQL.Append(" ( ");
            strSQL.Append(" select idesocial_deposito from tbl_esocial_deposito where nId_Chave = " + xIdChave.ToString() + " ");
            strSQL.Append(" ) ");
            strSQL.Append(" and Processamento_Retorno like '%nrRecibo%' ");

            using (SqlConnection cnn = new SqlConnection(Ilitera.Data.SQLServer.EntitySQLServer.GetConnection_Sied()))
            {
                SqlDataAdapter da;
                cnn.Open();


                da = new SqlDataAdapter(strSQL.ToString(), cnn);
                da.Fill(m_ds, "Result");

                cnn.Close();

                da.Dispose();
            }

            if (m_ds.Tables[0].Rows.Count < 1) return "";
            else return m_ds.Tables[0].Rows[0][0].ToString();

        }





        public DataSet Trazer_Evento(Int32 xIdEsocial_Evento)
        {
            //string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection();
            StringBuilder strSQL = new StringBuilder();

            //DataSet m_ds;


            DataTable table = new DataTable("Result");

            table.Columns.Add("XMLData", Type.GetType("System.String"));

            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);


            strSQL.Append(" select Processamento_Retorno as XMLData from tbl_eSocial_Envio where IdeSocial_Envio = " + xIdEsocial_Evento.ToString());

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


        public DataSet Trazer_Lote(Int32 xIdEsocial_Evento)
        {
            //string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection();
            StringBuilder strSQL = new StringBuilder();

            //DataSet m_ds;


            DataTable table = new DataTable("Result");

            table.Columns.Add("XMLData", Type.GetType("System.String"));

            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);


            strSQL.Append(" select Processamento_Lote as XMLData from tbl_eSocial_Envio where IdeSocial_Envio = " + xIdEsocial_Evento.ToString());

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



        public DataSet Trazer_Detalhes_Envio(Int32 xIdEsocial_Deposito)
        {
            //string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection();
            StringBuilder strSQL = new StringBuilder();

            //DataSet m_ds;


            DataTable table = new DataTable("Result");

            table.Columns.Add("IdeSocial_Envio", Type.GetType("System.String"));
            table.Columns.Add("Dt_Envio", Type.GetType("System.String"));
            table.Columns.Add("NomeUsuario", Type.GetType("System.String"));
            table.Columns.Add("Lote", Type.GetType("System.String"));            
            table.Columns.Add("Retorno", Type.GetType("System.String"));
            table.Columns.Add("Tipo_Envio", Type.GetType("System.String"));

            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);


            strSQL.Append(" select IdeSocial_Envio, convert( char(19), Data_Envio ) as Dt_Envio,  b.NomeUsuario, Tipo_Envio, ");
            strSQL.Append(" case when Processamento_Lote is null    then '(----------)' when Processamento_Lote = ''       then '(----------)' else '( XML Lote  )' end as Lote, ");
            strSQL.Append(" case when Processamento_Retorno is null then '(-----------)' when Processamento_Retorno = ''    then '(-----------)' else '(XML Retorno)' end as Retorno ");
            strSQL.Append(" from tbl_eSocial_Envio as a ");
            strSQL.Append(" left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.Usuario as b on(a.IdUsuario = b.IdUsuario) ");
            strSQL.Append(" where IdeSocial_Deposito = " + xIdEsocial_Deposito.ToString());
            strSQL.Append(" order by Data_Envio desc, tipo_envio desc " );

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



        public Int32 Retornar_Codigo_Usuario(string xUsuario, string xSenha)
        {
            //string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection();
            StringBuilder strSQL = new StringBuilder();

            //DataSet m_ds;


            DataTable table = new DataTable("Result");

            table.Columns.Add("IdUsuario", Type.GetType("System.Int32"));

            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);


            strSQL.Append(" SELECT top 1 IdUsuario ");
            strSQL.Append(" FROM Usuario (nolock) ");
            strSQL.Append(" left join pessoa (nolock) on (usuario.IdPessoa = pessoa.idpessoa) ");
            strSQL.Append(" left join juridicapessoa (nolock) on (pessoa.idpessoa = juridicapessoa.idpessoa) ");
            strSQL.Append(" left join prestador (nolock) on (juridicapessoa.IdJuridicaPessoa = prestador.IdJuridicaPessoa) ");
            strSQL.Append(" WHERE NomeUsuario = '" + xUsuario + "' AND desSenha = '"  + xSenha + "' " );
            strSQL.Append(" and pessoa.IsInativo = 0 and prestador.isinativo = 0 ");


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
                return System.Convert.ToInt32(m_ds.Tables[0].Rows[0][0]);
            

        }



        public bool Retornar_Acesso_Usuario_Empresa(Int32 xCodUsuario, Int32 xIdCliente)
        {
            //string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection();
            StringBuilder strSQL = new StringBuilder();

            //DataSet m_ds;


            DataTable table = new DataTable("Result");

            table.Columns.Add("IdUsuario", Type.GetType("System.Int32"));

            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);


            strSQL.Append(" select top 1 e.idcliente ");
            strSQL.Append(" from usuario (nolock)  as a ");
            strSQL.Append(" left join pessoa (nolock)  as b on(a.idpessoa = b.idpessoa) ");
            strSQL.Append(" left join juridicapessoa (nolock)  as c on(c.idpessoa = b.idpessoa) ");
            strSQL.Append(" left join prestador (nolock)  as d on(c.IdJuridicaPessoa = d.IdJuridicaPessoa) ");
            strSQL.Append(" left join prestadorcliente (nolock)  as e on(d.idprestador = e.idprestador) ");
            strSQL.Append(" where idusuario = " + xCodUsuario.ToString() +  " and IdCliente = " + xIdCliente.ToString() + " ");
            strSQL.Append(" and e.IdCliente in ( select idpessoa from juridica (nolock)  where idjuridicapapel in ( 1,18 ) ) ");
            strSQL.Append(" and e.IdCliente in ( select idpessoa from pessoa (nolock)  where isinativo = 0 ) ");

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
                return false;
            else
                return true;

        }



        public void Criar_Agendamento_Evento( Int32 xIdeSocial_Deposito, Int32 xIdUsuario)
        {

            //chaves para cada evento
            //nid_func      1060     1
            //IdAcidente    2210     1
            //IdExameBase / IdPCMSO  2220    1
            //IdExameBase  2221     1
            //IdAfastamento  2230   1
            //nid_laud_tec   2240   1
            //IdTreinamento  2245   1

            using (SqlConnection cnn = new SqlConnection(Ilitera.Data.SQLServer.EntitySQLServer.GetConnection_Sied()))
            {
                cnn.Open();

                //SqlCommand com = new SqlCommand("INSERT INTO tbl_eSocial_Deposito( IdEsocial_Deposito, IdeSocial, XMLData, NomeArquivo, DataHora, Seq, CPF, NIT, CNPJ, Dt_Ini, Dt_Fim, nId_Chave ) " +
                //                                "SELECT convert( int, rand() * 1000000000 )," + xIdeSocial.ToString() + " , CONVERT(XML, N'" + xXML + "' , 2 ), '" + xArquivo + "', GETDATE(), " + xSeq.ToString() + " , '" + xCPF + "', " + xNIT.ToString() + " , '" + xCNPJ + "', convert( smalldatetime,'" + xDataIni + "', 103 ), convert( smalldatetime, '" + xDataFim + "', 103 ),  " + xIdChave.ToString(), cnn);
                SqlCommand com = new SqlCommand("INSERT INTO tbl_eSocial_Schedule( IdEsocial_Schedule, IdEsocial_Deposito, Data_Agendamento, Processado_Em, Status, IdUsuario ) " +
                "SELECT convert( int, rand() * 1000000000 )," + xIdeSocial_Deposito.ToString() + " , getdate(), NULL, NULL, " + xIdUsuario.ToString() , cnn);

                com.ExecuteNonQuery();
            }


            return;

        }



        public Int16 Checar_Chave_eSocial(Int32 xChave, string xEvento, string xAmbiente)
        {
            //string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection();
            StringBuilder strSQL = new StringBuilder();

            //DataSet m_ds;


            DataTable table = new DataTable("Result");

            table.Columns.Add("ID", Type.GetType("System.String"));

            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);


            //strSQL.Append(" select top 1 IdeSocial_Deposito  from tbl_esocial_deposito where nid_chave = " + xChave.ToString() + " ");
            //strSQL.Append(" and ideSocial in (select idEsocial from tbl_Esocial where evento = '" + xEvento + "' ) ");

            strSQL.Append(" ( select IdeSocial from  tbl_esocial(nolock) where Ambiente = " + xAmbiente + " and Evento = '" + xEvento + "' and IdEsocial in ");
            strSQL.Append("  (  ");
            strSQL.Append("    ( select IdeSocial from  tbl_esocial_Deposito(nolock) where nid_chave = " + xChave.ToString() + " and IdeSocial_Deposito in ");
            strSQL.Append("      ( ");
            strSQL.Append("        select IdeSocial_Deposito from  tbl_esocial_envio where processamento_retorno like '%<nrRecibo>%' ");
            strSQL.Append("       ) ");
            strSQL.Append("    ) ");
            strSQL.Append("  ) ");
            strSQL.Append(" ) ");


            using (SqlConnection cnn = new SqlConnection(Ilitera.Data.SQLServer.EntitySQLServer.GetConnection_Sied()))
            {
                SqlDataAdapter da;
                cnn.Open();


                da = new SqlDataAdapter(strSQL.ToString(), cnn);
                da.Fill(m_ds, "Result");

                cnn.Close();

                da.Dispose();
            }


            if (m_ds.Tables[0].Rows.Count < 1) return 0;
            else return 1;

        }



        public Int16 Checar_Alocacao_eSocial(Int32 xChave, string xEvento, string xAmbiente)
        {
            //string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection();
            StringBuilder strSQL = new StringBuilder();

            //DataSet m_ds;


            DataTable table = new DataTable("Result");

            table.Columns.Add("ID", Type.GetType("System.String"));

            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);


            //strSQL.Append(" select top 1 IdeSocial_Deposito  from tbl_esocial_deposito where nid_Func_Empregado = " + xChave.ToString() + " ");
            //strSQL.Append(" and ideSocial in (select idEsocial from tbl_Esocial where evento = '" + xEvento + "' and Ambiente = '" + xAmbiente + "' ) ");

            strSQL.Append(" ( select IdeSocial from  tbl_esocial(nolock) where Ambiente = " + xAmbiente + " and Evento = '" + xEvento + "' and IdEsocial in ");
            strSQL.Append("  (  ");
            strSQL.Append("    ( select IdeSocial from  tbl_esocial_Deposito(nolock) where nid_Func_Empregado = " + xChave.ToString() + " ");
            strSQL.Append("    and  ( IdeSocial_Deposito in ");
            strSQL.Append("     ( ");
            strSQL.Append("        select IdeSocial_Deposito from  tbl_esocial_envio (nolock) where processamento_retorno like '%<nrRecibo>%' and processamento_retorno not like '%EXCLUSAO%'  ) ");
            strSQL.Append("        and IdeSocial_Deposito not in ( select IdeSocialDeposito from tbl_esocial_Exclusao ) ");
            strSQL.Append("     ) ");
            strSQL.Append("     ) ");
            strSQL.Append("  ) ");
            strSQL.Append(" ) ");


            using (SqlConnection cnn = new SqlConnection(Ilitera.Data.SQLServer.EntitySQLServer.GetConnection_Sied()))
            {
                SqlDataAdapter da;
                cnn.Open();


                da = new SqlDataAdapter(strSQL.ToString(), cnn);
                da.Fill(m_ds, "Result");

                cnn.Close();

                da.Dispose();
            }


            if (m_ds.Tables[0].Rows.Count < 1) return 0;
            else return 1;

        }



        public Int16 Checar_Alocacao_eSocial_Anterior(Int32 xChave, string xEvento, string xAmbiente, Int32 xLaudo)
        {
            //string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection();
            StringBuilder strSQL = new StringBuilder();

            //DataSet m_ds;


            DataTable table = new DataTable("Result");

            table.Columns.Add("Data_Inicial", Type.GetType("System.String"));

            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);


            //strSQL.Append(" select top 1 IdeSocial_Deposito  from tbl_esocial_deposito where nid_Func_Empregado = " + xChave.ToString() + " ");
            //strSQL.Append(" and ideSocial in (select idEsocial from tbl_Esocial where evento = '" + xEvento + "' and Ambiente = '" + xAmbiente + "' ) ");

            strSQL.Append(" select * from ");
            strSQL.Append("  ( ");
            strSQL.Append("    select hdt_laudo as Data_Inicial  ");
            strSQL.Append("    from tblEmpregado_Funcao (nolock) as a ");
            strSQL.Append("    join tblEmpregado (nolock) as b  on (a.nid_empregado = b.nid_empregado) ");
            strSQL.Append("    join tblfunc_empregado (nolock) as c on (a.nid_empregado_Funcao = c.nID_EMPREGADO_FUNCAO) ");
            strSQL.Append("    join tbl_eSocial_Deposito as d (nolock) on (c.nID_FUNC_EMPREGADO = d.nid_Func_Empregado) ");
            strSQL.Append("    join tbllaudo_tec (nolock) as e on (c.nID_LAUD_TEC = e.nID_LAUD_TEC) ");
            strSQL.Append("    where b.nid_Empregado in (select nid_Empregado from tblEmpregado_Funcao (nolock) where nid_Empregado_Funcao = " + xChave + " ) ");
            strSQL.Append("    and IdeSocial_Deposito in  ");
            strSQL.Append("    ( ");
            strSQL.Append("      select IdeSocial_Deposito from tbl_esocial_envio (nolock) where processamento_retorno like '%<nrRecibo>%' and processamento_retorno not like '%EXCLUSAO%' ");
            strSQL.Append("        and IdeSocial_Deposito not in ( select IdeSocialDeposito from tbl_esocial_Exclusao ) ");
            strSQL.Append("    ) ");
            strSQL.Append("    and IdeSocial in ");
            strSQL.Append("    ( ");
            strSQL.Append("       select IdeSocial from  tbl_esocial(nolock) where Ambiente = " + xAmbiente + " and Evento = '" + xEvento + "' ");
            strSQL.Append("    ) ");
            
            strSQL.Append(" ) as tx90 ");
            //strSQL.Append(" where Data_Inicial > ( select top 1 case when hdt_laudo <= convert( smalldatetime, '13/10/2020', 103 ) then convert( smalldatetime, '01/01/2040', 103 ) else hdt_laudo end  from tbllaudo_tec (nolock) where nid_laud_tec = " + xLaudo + ") ");

            strSQL.Append("   where Data_Inicial > ( ");
            strSQL.Append("      select  case when max(Dt1) <= convert(smalldatetime, '13/10/2020', 103) then convert(smalldatetime, '01/01/2040', 103)         else max(Dt1) end as Dt2 ");
            strSQL.Append("      from( ");
            strSQL.Append("       select top 1  ");
            strSQL.Append("   		case when hdt_laudo <= convert(smalldatetime, '13/10/2020', 103) then convert(smalldatetime, '01/01/2040', 103) ");
            strSQL.Append("   		else hdt_laudo end as Dt1 ");
            strSQL.Append("       from tbllaudo_tec (nolock) as a ");
            strSQL.Append("       where nid_laud_tec = " + xLaudo + " ");
            strSQL.Append("       union ");
            strSQL.Append("       select top 1  ");
            strSQL.Append("       hdt_inicio as Dt1 ");
            strSQL.Append("       from tblEmpregado_Funcao (nolock) as a ");
            strSQL.Append("       where nID_EMPREGADO_FUNCAO  = " + xChave + " ");
            strSQL.Append("       ) as tx99 ");
            strSQL.Append("    ) ");




            using (SqlConnection cnn = new SqlConnection(Ilitera.Data.SQLServer.EntitySQLServer.GetConnection_Sied()))
            {
                SqlDataAdapter da;
                cnn.Open();


                da = new SqlDataAdapter(strSQL.ToString(), cnn);
                da.Fill(m_ds, "Result");

                cnn.Close();

                da.Dispose();
            }


            if (m_ds.Tables[0].Rows.Count < 1) return 0;
            else return 1;

        }




        public DataSet Dados_Colaborador_eSocial(string xCPF)
        {
            //string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection();
            StringBuilder strSQL = new StringBuilder();

            //DataSet m_ds;


            DataTable table = new DataTable("Result");

            table.Columns.Add("DataEnvio", Type.GetType("System.String"));
            table.Columns.Add("Ambiente", Type.GetType("System.String"));
            table.Columns.Add("Evento", Type.GetType("System.String"));
            table.Columns.Add("Status", Type.GetType("System.String"));

            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);


            strSQL.Append(" select convert( char(10), data_Envio, 103 ) as DataEnvio, c.Evento,  ");
            strSQL.Append(" case when c.Ambiente = 1 then 'Produção         ' else 'Produção Restrita' end as Ambiente, ");
            strSQL.Append(" case when charindex('<nrRecibo>', Processamento_Retorno) > 0 then 'Recibo Gerado: ' + Protocolo ");
            strSQL.Append(" when charindex('encontra-se em processamento', Processamento_Retorno) > 0 then 'Aguardando Processamento' ");
            strSQL.Append("      when charindex('Contrato de Trabalho', Processamento_Retorno) > 0 then 'Contrato de trabalho não localizado' ");
            strSQL.Append("      when charindex('estrutura do arquivo XML', Processamento_Retorno) > 0 then 'Problema na estrutura do XML' ");
            strSQL.Append("      when charindex('duplicidade', Processamento_Retorno) > 0 then 'Duplicidade na carga do evento'  ");
            strSQL.Append("      else 'Não Processado' end as Status ");
            strSQL.Append(" from tbl_eSocial_Deposito (nolock)  as a ");
            strSQL.Append(" left join tbl_eSocial_Envio (nolock)  as b on(a.IdeSocial_Deposito = b.IdeSocial_Deposito) ");
            strSQL.Append(" left join tbl_esocial (nolock)  as c on(a.IdeSocial = c.IdeSocial) ");
            strSQL.Append(" where cpf = '" + xCPF + "' and data_Envio is not null ");
            strSQL.Append(" order by data_envio desc ");


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




        public Int16 Checar_Classif_Funcional_eSocial(Int32 xChave, string xEvento, string xAmbiente)
        {
            //string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection();
            StringBuilder strSQL = new StringBuilder();

            //DataSet m_ds;


            DataTable table = new DataTable("Result");

            table.Columns.Add("ID", Type.GetType("System.String"));

            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);


            //strSQL.Append(" select top 1 IdeSocial_Deposito  from tbl_esocial_deposito where nid_Func_Empregado = " + xChave.ToString() + " ");
            //strSQL.Append(" and ideSocial in (select idEsocial from tbl_Esocial where evento = '" + xEvento + "' and Ambiente = '" + xAmbiente + "' ) ");

            strSQL.Append(" ( select IdeSocial from  tbl_esocial(nolock) where Ambiente = " + xAmbiente + " and Evento = '" + xEvento + "' and IdEsocial in ");
            strSQL.Append("  (  ");
            strSQL.Append("    ( select IdeSocial from  tbl_esocial_Deposito(nolock) where nid_Func_Empregado in ( Select nId_Func_Empregado from tblFunc_Empregado where nId_Empregado_Funcao = " + xChave.ToString() + " ) and IdeSocial_Deposito in ");
            strSQL.Append("      ( ");
            strSQL.Append("        select IdeSocial_Deposito from  tbl_esocial_envio where processamento_retorno like '%<nrRecibo>%' ");
            strSQL.Append("       ) ");
            strSQL.Append("    ) ");
            strSQL.Append("  ) ");
            strSQL.Append(" ) ");


            using (SqlConnection cnn = new SqlConnection(Ilitera.Data.SQLServer.EntitySQLServer.GetConnection_Sied()))
            {
                SqlDataAdapter da;
                cnn.Open();


                da = new SqlDataAdapter(strSQL.ToString(), cnn);
                da.Fill(m_ds, "Result");

                cnn.Close();

                da.Dispose();
            }


            if (m_ds.Tables[0].Rows.Count < 1) return 0;
            else return 1;

        }



        public Int32 Retornar_Id_Func_Empregado_Simultaneos(Int32 nId_Laud_tec, Int32 nId_Func_Empregado_Raiz)
        {

            StringBuilder strSQL = new StringBuilder();


            DataTable table = new DataTable("Result");

            table.Columns.Add("nid_func_Empregado", Type.GetType("System.String"));


            strSQL.Append(" select top 1 nid_func_Empregado from tblfunc_empregado  ");
            strSQL.Append(" where nid_laud_tec = " + nId_Laud_tec.ToString() + " ");
            strSQL.Append(" and nID_EMPREGADO_FUNCAO in ");
            strSQL.Append(" ( ");
            strSQL.Append("    select nid_empregado_Funcao from tblEmpregado_Funcao where nid_Empregado in ");
            strSQL.Append("    ( ");
            strSQL.Append("      select nid_empregado from tblEmpregado_Funcao where nID_EMPREGADO_FUNCAO in ");
            strSQL.Append("      ( ");
            strSQL.Append("         select nID_EMPREGADO_FUNCAO  from tblfunc_empregado where nid_func_empregado = " + nId_Func_Empregado_Raiz + " ");
            strSQL.Append("      ) ");
            strSQL.Append("     ) ");
            strSQL.Append(" )  ");

            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);


            using (SqlConnection cnn = new SqlConnection(Ilitera.Data.SQLServer.EntitySQLServer.GetConnection_Sied()))
            {
                SqlDataAdapter da;
                cnn.Open();


                da = new SqlDataAdapter(strSQL.ToString(), cnn);
                da.Fill(m_ds, "Result");

                cnn.Close();

                da.Dispose();
            }


            if (m_ds.Tables[0].Rows.Count == 0) return 0;
            else return System.Convert.ToInt32(m_ds.Tables[0].Rows[0][0].ToString().Trim());


        }




    }

}