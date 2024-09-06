using System;
using System.Data;
using System.Collections.Generic;
using System.Collections;
using System.Text;

using Ilitera.Common;
using Ilitera.Opsa.Data;

namespace Ilitera.Sied.Report
{	
	public class DataSourceClassificacaoFuncional : Ilitera.Common.DataSourceBase
	{
        private GrupoEmpresa grupoEmpresa;
		private Cliente cliente;
		private LaudoTecnico laudo;
        private LaudoTecnico laudo_Parametro;

		public override event EventProgress ProgressIniciar;
		public override event EventProgress ProgressAtualizar;
		public override event EventProgressFinalizar ProgressFinalizar;

        public DataSourceClassificacaoFuncional(GrupoEmpresa grupoEmpresa)
		{
            this.grupoEmpresa = grupoEmpresa;
            this.laudo_Parametro = null;
        }

		public DataSourceClassificacaoFuncional(Cliente cliente, LaudoTecnico laudo)
		{
			this.cliente = cliente;
			this.laudo = laudo;
            this.laudo_Parametro = laudo;
		}

		public DataSourceClassificacaoFuncional(Cliente cliente)
		{
			this.cliente = cliente;
			this.laudo = LaudoTecnico.GetUltimoLaudo(this.cliente.Id, true);
            this.laudo_Parametro = null;
        }

        #region GetReports

        public RptListaFuncoes GetReportListaFuncoes( Int16 xLaudo)
		{
            //xLaudo = 1  PPRA   xLaudo = 2  PGR

            RptListaFuncoes report = new RptListaFuncoes();
			report.SetDataSource(GetDataSourceFuncoes(xLaudo));
			report.Refresh();

            SetTempoProcessamento(report);

			return report;
		}

		public RptListaSetor GetReportListaSetor()
		{
			RptListaSetor report = new RptListaSetor();
			report.SetDataSource(GetDataSourceSetores());
			report.Refresh();

            SetTempoProcessamento(report);

			return report;
		}

        //public RptClassificacaoFuncional GetReportClassificacaoFuncional()
        //{			
        //    RptClassificacaoFuncional report = new RptClassificacaoFuncional();	
        //    report.SetDataSource(GetClassificacaoFuncional());
        //    report.Refresh();

        //    SetTempoProcessamento(report);

        //    return report;
        //}

        //public RptClassificacaoFuncional GetReportClassificacaoFuncional(string sFilter)
        //{
        //    RptClassificacaoFuncional report = new RptClassificacaoFuncional();
        //    report.SetDataSource(GetClassificacaoFuncional(sFilter));
        //    report.Refresh();

        //    SetTempoProcessamento(report);

        //    return report;
        //}

        //public RptClassificacaoFuncionalImportacao GetReportClassificacaoFuncionalImportacao(string sFilter)
        //{
        //    RptClassificacaoFuncionalImportacao report = new RptClassificacaoFuncionalImportacao();
        //    report.SetDataSource(GetClassificacaoFuncionalImportacao(sFilter));
        //    report.Refresh();
        //    return report;
        //}

        //public RptClassificacaoFuncionalPorGrupo GetReportClassificacaoFuncionalPorGrupo(string sFilter)
        //{
        //    RptClassificacaoFuncionalPorGrupo report = new RptClassificacaoFuncionalPorGrupo();
        //    report.SetDataSource(GetClassificacaoFuncionalPorGrupo(sFilter));
        //    report.Refresh();
        //    return report;
        //}
		
        //public RptClassificacaoFuncionalPorGHE GetReportClassificacaoFuncionalGhe(string sFilter)
        //{
        //    RptClassificacaoFuncionalPorGHE report = new RptClassificacaoFuncionalPorGHE();
        //    report.SetDataSource(GetClassificacaoFuncional(sFilter));
        //    report.Refresh();
        //    return report;
        //}

        //public RptClassificacaoFuncionalPorGHE GetReportClassificacaoFuncionalGhe()
        //{
        //    RptClassificacaoFuncionalPorGHE report = new RptClassificacaoFuncionalPorGHE();
        //    report.SetDataSource(GetClassificacaoFuncional());
        //    report.Refresh();
        //    return report;
        //}
        #endregion

        #region GetClassificacaoFuncional

        private DataSet GetClassificacaoFuncional()
        {
            return GetClassificacaoFuncional("nID_EMPR=" + cliente.Id + " AND hDT_DEM IS NULL");
        }

        private DataSet GetClassificacaoFuncional(string sFilter)
        {
            if (laudo == null)
                throw new Exception("O Laudo Técnico é obrigatório!");

            StringBuilder where = new StringBuilder();

            where.Append("SELECT nID_EMPREGADO, tNO_EMPG, tNO_FUNC, tNO_FUNC_EMPR, tNO_STR_EMPR, hDT_INICIO, hDT_TERMINO, nID_EMPR, nID_EMPR_EMPREGADO, nID_FUNCAO, nID_SETOR, nID_FUNC, nID_LAUD_TEC, nID_FUNC_EMPREGADO, nID_EMPREGADO_FUNCAO, hDT_DEM, LocalDeTrabalho, tNO_CARGO "
                        + " FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.qryEmpregadoFuncaoGhe "
                        + " WHERE nID_LAUD_TEC = " + laudo.Id);

            if (sFilter != string.Empty)
                where.Append(" AND nID_EMPREGADO IN (SELECT nID_EMPREGADO FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEMPREGADO WHERE " + sFilter + ")");
            
            where.Append(" ORDER BY tNO_EMPG");

            StringBuilder where2 = new StringBuilder();

            where2.Append("SELECT nID_EMPREGADO, tNO_EMPG, tNO_FUNC, tNO_FUNC_EMPR, tNO_STR_EMPR, hDT_INICIO, hDT_TERMINO, nID_EMPR, nID_EMPR_EMPREGADO, nID_FUNCAO, nID_SETOR, nID_FUNC, nID_LAUD_TEC, nID_FUNC_EMPREGADO, nID_EMPREGADO_FUNCAO, hDT_DEM, LocalDeTrabalho, tNO_CARGO "
                            + " FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.qryEmpregadoFuncaoSemGhe "
                            + " WHERE nID_EMPR = " + laudo.nID_EMPR.Id
                            + " AND (nID_EMPREGADO_FUNCAO NOT IN (SELECT nID_EMPREGADO_FUNCAO FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblFUNC_EMPREGADO WHERE nID_EMPREGADO_FUNCAO IS NOT NULL AND nID_LAUD_TEC=" + laudo.Id + "))");
            where2.Append(" AND nID_EMPREGADO NOT IN (SELECT IdEmpregado FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.Afastamento WHERE DataVolta IS NULL)");
            where2.Append(" AND hDT_TERMINO IS NULL");
            
            if (sFilter != string.Empty)
                where2.Append(" AND nID_EMPREGADO IN (SELECT nID_EMPREGADO FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEMPREGADO WHERE " + sFilter + ")");

            where2.Append(" ORDER BY tNO_EMPG");

            DataSet dsEmpregadoFuncaoGhe = new Empregado().ExecuteDataset(where.ToString());

            DataSet dsEmpregadoFuncaoSemGhe = new Empregado().ExecuteDataset(where2.ToString());

            dsEmpregadoFuncaoGhe.Merge(dsEmpregadoFuncaoSemGhe.Tables[0].Select());

            return GetClassificacaoFuncional(dsEmpregadoFuncaoGhe);
        }

        private DataSet GetClassificacaoFuncional(DataSet dsEmpregadoFuncaoGhe)
        {
            DataSet ds = new DataSet();
            ds.Tables.Add(GetDataTableClasFuncional());
            
            if (ProgressIniciar != null)
                ProgressIniciar(dsEmpregadoFuncaoGhe.Tables[0].Rows.Count);

            int OrdinalPosition = 1;

            DataRow newRow;

            foreach (DataRow row in dsEmpregadoFuncaoGhe.Tables[0].Rows)
            {
                newRow = ds.Tables[0].NewRow();

                newRow["NomeEmpresa"] = cliente.NomeAbreviado;
                newRow["NomeEmpregado"] = row["tNO_EMPG"];

                if(Convert.ToInt32(row["nID_EMPR"])!=Convert.ToInt32(row["nID_EMPR_EMPREGADO"]))
                    newRow["LocalDeTrabalho"] = row["LocalDeTrabalho"] + " (Alocado)";
                else
                    newRow["LocalDeTrabalho"] = row["LocalDeTrabalho"];

                newRow["InicioFuncao"] = GetDateTimeToString(row["hDT_INICIO"]);
                newRow["TerminoFuncao"] = GetDateTimeToString(row["hDT_TERMINO"]);
                newRow["Cargo"] = row["tNO_CARGO"];
                newRow["Setor"] = row["tNO_STR_EMPR"];
                newRow["Funcao"] = row["tNO_FUNC_EMPR"];
                newRow["GHE"] = row["tNO_FUNC"]; 

                ds.Tables[0].Rows.Add(newRow);

                if (ProgressAtualizar != null)
                    ProgressAtualizar(OrdinalPosition++);
            }

            DataSet dsGheSemEmpregados = new Ghe().Get("nID_LAUD_TEC = " + laudo.Id
                + " AND nID_FUNC NOT IN (SELECT nID_FUNC FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblFUNC_EMPREGADO) ");

            foreach(DataRow row in dsGheSemEmpregados.Tables[0].Rows)
            {
                newRow = ds.Tables[0].NewRow();

                newRow["NomeEmpresa"] = cliente.NomeAbreviado;
                newRow["NomeEmpregado"] = "* nenhum empregado";
                newRow["LocalDeTrabalho"] = string.Empty;
                newRow["InicioFuncao"] = System.DBNull.Value;
                newRow["TerminoFuncao"] = System.DBNull.Value;
                newRow["Cargo"] = string.Empty;
                newRow["Setor"] = string.Empty;
                newRow["Funcao"] = string.Empty;
                newRow["GHE"] = row["tNO_FUNC"]; 

                ds.Tables[0].Rows.Add(newRow);
            }

            if (ProgressFinalizar != null)
                ProgressFinalizar();

            return ds;
        }
        #endregion

        #region GetClassificacaoFuncionalImportacao

        private DataSet GetClassificacaoFuncionalImportacao()
        {
            return GetClassificacaoFuncional("nID_EMPR=" + cliente.Id + " AND hDT_DEM IS NULL");
        }

        private DataSet GetClassificacaoFuncionalImportacao(string sFilter)
        {
            StringBuilder where = new StringBuilder();

            where.Append("SELECT nID_EMPREGADO,"
                                + " nID_EMPR_EMPREGADO,"
                                + " tNO_EMPG,"
                                + " tCOD_EMPR,"
                                + " hDT_ADM,"
                                + " hDT_DEM,"
                                + " gTERCEIRO,"
                                + " tNO_FUNC_EMPR,"
                                + " tNO_STR_EMPR,"
                                + " hDT_INICIO,"
                                + " hDT_TERMINO,"
                                + " nID_EMPR,"
                                + " nID_FUNCAO,"
                                + " nID_SETOR,"
                                + " nIND_GFIP,"
                                + " Cliente,"
                                + " LocalDeTrabalho,"
                                + " NomeArquivo"
                        + " FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.qryEmpregadoFuncaoImportacao ");

            if (sFilter != string.Empty)
                where.Append(" WHERE nID_EMPREGADO IN"
                                                + " (SELECT nID_EMPREGADO"
                                                + " FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEMPREGADO"
                                                + " WHERE " + sFilter + ")");

            if (grupoEmpresa != null)
                where.Append(" AND IdGrupoEmpresa=" + grupoEmpresa.Id);

            where.Append(" ORDER BY tNO_EMPG, hDT_INICIO, hDT_TERMINO");

            DataSet ds = new Empregado().ExecuteDataset(where.ToString());

            return GetClassificacaoFuncionalImportacao(ds);
        }

        private DataSet GetClassificacaoFuncionalImportacao(DataSet dsEmpregado)
        {
            DataSet ds = new DataSet();
            ds.Tables.Add(GetDataTableClasFuncionalImportacao());

            if (ProgressIniciar != null)
                ProgressIniciar(dsEmpregado.Tables[0].Rows.Count);

            int OrdinalPosition = 1;

            DataRow newRow;

            foreach (DataRow row in dsEmpregado.Tables[0].Rows)
            {
                newRow = ds.Tables[0].NewRow();

                newRow["NomeEmpresa"] = row["Cliente"];

                newRow["DataAdmissao"] = GetDateTimeToString(row["hDT_ADM"]);

                if (row["tCOD_EMPR"] != System.DBNull.Value)
                    newRow["NomeEmpregado"] = row["tNO_EMPG"] + " (" + row["tCOD_EMPR"] + ")";
                else
                    newRow["NomeEmpregado"] = row["tNO_EMPG"];

                if (Convert.ToInt32(row["nID_EMPR"]) != Convert.ToInt32(row["nID_EMPR_EMPREGADO"]))
                    newRow["LocalDeTrabalho"] = row["LocalDeTrabalho"] + " (Alocado)";
                else
                    newRow["LocalDeTrabalho"] = row["LocalDeTrabalho"];

                newRow["InicioFuncao"] = GetDateTimeToString(row["hDT_INICIO"]);
                newRow["TerminoFuncao"] = GetDateTimeToString(row["hDT_TERMINO"]);
                newRow["Setor"] = row["tNO_STR_EMPR"];
                newRow["Funcao"] = row["tNO_FUNC_EMPR"];

                char[] seps = { '_', '.' };

                string[] split = System.IO.Path.GetFileName(Convert.ToString(row["NomeArquivo"])).Split(seps);

                if (split.Length == 5)
                    newRow["Arquivo"] = System.IO.Path.GetFileName(split[3]);
                else
                    newRow["Arquivo"] = "-";


                ds.Tables[0].Rows.Add(newRow);

                if (ProgressAtualizar != null)
                    ProgressAtualizar(OrdinalPosition++);
            }

            if (ProgressFinalizar != null)
                ProgressFinalizar();

            return ds;
        }
        #endregion

        #region GetClassificacaoFuncionalPorGrupo

        private DataSet GetClassificacaoFuncionalPorGrupo()
        {
            return GetClassificacaoFuncional("IdGrupoEmpresa=" + grupoEmpresa.Id);
        }

        private DataSet GetClassificacaoFuncionalPorGrupo(string sFilter)
        {
            StringBuilder where = new StringBuilder();

            where.Append("SELECT nID_EMPREGADO,"
                                + " nID_EMPR_EMPREGADO,"
                                + " tNO_EMPG,"
                                + " tCOD_EMPR,"
                                + " hDT_ADM,"
                                + " hDT_DEM,"
                                + " gTERCEIRO,"
                                + " tNO_FUNC_EMPR,"
                                + " tNO_STR_EMPR,"
                                + " hDT_INICIO,"
                                + " hDT_TERMINO,"
                                + " nID_EMPR,"
                                + " nID_FUNCAO,"
                                + " nID_SETOR,"
                                + " nIND_GFIP,"
                                + " Cliente,"
                                + " LocalDeTrabalho,"
                                + " NomeArquivo"
                        + " FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.qryEmpregadoFuncaoImportacao ");

            if (sFilter != string.Empty)
                where.Append(" WHERE nID_EMPREGADO IN"
                                                + " (SELECT nID_EMPREGADO"
                                                + " FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEMPREGADO"
                                                + " WHERE " + sFilter + ")");
            if (grupoEmpresa != null)
                where.Append(" AND IdGrupoEmpresa=" + grupoEmpresa.Id);

            where.Append(" ORDER BY tNO_EMPG, hDT_INICIO, hDT_TERMINO");

            DataSet ds = new Empregado().ExecuteDataset(where.ToString());

            return GetClassificacaoFuncionalPorGrupo(ds);
        }

        private DataSet GetClassificacaoFuncionalPorGrupo(DataSet dsEmpregado)
        {
            DataSet ds = new DataSet();
            ds.Tables.Add(GetDataTableClasFuncionalPorGrupo());

            if (ProgressIniciar != null)
                ProgressIniciar(dsEmpregado.Tables[0].Rows.Count);

            int OrdinalPosition = 1;

            DataRow newRow;

            foreach (DataRow row in dsEmpregado.Tables[0].Rows)
            {
                newRow = ds.Tables[0].NewRow();

                newRow["NomeEmpresa"] = row["Cliente"];
                newRow["DataAdmissao"] = GetDateTimeToString(row["hDT_ADM"]);
                newRow["NomeEmpregado"] = row["tNO_EMPG"];
                newRow["LocalDeTrabalho"] = row["LocalDeTrabalho"];
                newRow["InicioFuncao"] = GetDateTimeToString(row["hDT_INICIO"]);
                newRow["TerminoFuncao"] = GetDateTimeToString(row["hDT_TERMINO"]);
                newRow["Setor"] = row["tNO_STR_EMPR"];
                newRow["Funcao"] = row["tNO_FUNC_EMPR"];
                newRow["IdEmpregado"] = row["nID_EMPREGADO"];

                ds.Tables[0].Rows.Add(newRow);

                if (ProgressAtualizar != null)
                    ProgressAtualizar(OrdinalPosition++);
            }
            if (ProgressFinalizar != null)
                ProgressFinalizar();

            return ds;
        }
        #endregion

        #region GetDataSourceFuncoes

        private DataSet GetDataSourceFuncoes(Int16 zLaudo)
        {
            //zLaudo = 1  PPRA   zLaudo = 2  PGR

            DataSet ds = new DataSet();
            DataTable table = new DataTable("Result");
            table.Columns.Add("NomeEmpresa", Type.GetType("System.String"));
            table.Columns.Add("NomeFuncao", Type.GetType("System.String"));
            table.Columns.Add("Descricao", Type.GetType("System.String"));
            table.Columns.Add("CBO", Type.GetType("System.String"));
            table.Columns.Add("NumeroEmpregado", Type.GetType("System.String"));
            ds.Tables.Add(table);
            
            DataRow newRow;
            DataSet dsFuncao;

            if (laudo_Parametro != null)
            {
                dsFuncao = new Funcao().Get(" idFuncao in " +
                                                               " ( " +
                                                               "   select distinct nid_funcao from " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblempregado_funcao " +
                                                               "   where hdt_Termino is null and nid_empregado_funcao in " +
                                                               "   ( " +
                                                               "     select nid_empregado_funcao from " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblfunc_empregado " +
                                                               "     where nid_laud_tec = " + laudo.Id.ToString() + " " +
                                                               "   ) " +
                                                               " )  order by NomeFuncao");

                //dsFuncao = new Funcao().Get("IdCliente=" + cliente.Id +  "and idFuncao in " + 
                //                                                               " ( " +
                //                                                               "   select distinct nid_funcao from " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblempregado_funcao " + 
                //                                                               "   where nid_empregado_funcao in " + 
                //                                                               "   ( " +
                //                                                               "     select nid_empregado_funcao from " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblfunc_empregado " + 
                //                                                               "     where nid_laud_tec = " + laudo.Id.ToString() + " " +
                //                                                               "   ) " + 
                //                                                               " )  order by NomeFuncao");                    
            }
            else
            {
                dsFuncao = new Funcao().Get("IdCliente=" + cliente.Id + " ORDER BY NomeFuncao");
            }

            if (ProgressIniciar != null)
                ProgressIniciar(dsFuncao.Tables[0].Rows.Count);

            int OrdinalPosition = 1;

            foreach (DataRow row in dsFuncao.Tables[0].Rows)
            {
                newRow = ds.Tables[0].NewRow();

                DateTime zData = laudo.hDT_LAUDO;

                if (laudo_Parametro != null)
                {
                    if (zLaudo == 2)
                    {
                        if (zData < new DateTime(2022, 1, 3))
                        {
                            zData = new DateTime(2022, 1, 3);
                        }
                    }

                    newRow["NomeEmpresa"] = "LISTA DE FUNÇÕES - " + cliente.NomeAbreviado + " -  Laudo: " + zData.ToString("dd/MM/yyyy");
                    newRow["NumeroEmpregado"] = new EmpregadoFuncao().ExecuteCount("nID_FUNCAO=" + Convert.ToInt32(row["IdFuncao"])
                                                                                  + " AND nID_EMPR=" + cliente.Id + " and hdt_Termino is null  and nid_empregado_funcao in " +
                                                                                    " ( " +
                                                                                    "   select nid_empregado_funcao from  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblfunc_empregado " +
                                                                                    "   where nid_laud_tec = " + laudo.Id + " " +
                                                                                    " ) ");
                }
                else
                {
                    newRow["NomeEmpresa"] = "LISTA DE FUNÇÕES - " + cliente.NomeAbreviado;
                    newRow["NumeroEmpregado"] = new EmpregadoFuncao().ExecuteCount("nID_FUNCAO=" + Convert.ToInt32(row["IdFuncao"])
                                                                                  + " AND nID_EMPR=" + cliente.Id + " and hdt_Termino is null ");
                }

                newRow["NomeFuncao"] = row["NomeFuncao"];
                newRow["Descricao"] = row["DescricaoFuncao"];
                newRow["CBO"] = row["NumeroCBO"];



                if (ProgressAtualizar != null)
                    ProgressAtualizar(OrdinalPosition++);

                ds.Tables[0].Rows.Add(newRow);
            }

            if (ProgressFinalizar != null)
                ProgressFinalizar();

            return ds;
        }
        #endregion

        #region GetDataSourceSetores

        private DataSet GetDataSourceSetores()
        {
            DataSet ds = new DataSet();
            DataTable table = new DataTable("Result");
            table.Columns.Add("NomeEmpresa", Type.GetType("System.String"));
            table.Columns.Add("NomeSetor", Type.GetType("System.String"));
            table.Columns.Add("Descricao", Type.GetType("System.String"));
            ds.Tables.Add(table);

            DataRow newRow;
            List<Setor> listSetor;

            if (laudo_Parametro != null)
            {
                listSetor = new Setor().Find<Setor>("nID_EMPR=" + cliente.Id + "and nid_setor in " + 
                                                                               " ( " + 
                                                                               "   select distinct nid_setor from tblempregado_funcao " + 
                                                                               "   where nid_empregado_funcao in " + 
                                                                               "   ( " + 
                                                                               "     select nid_empregado_funcao from tblfunc_empregado " + 
                                                                               "     where nid_laud_tec = " + laudo.Id.ToString() + " " +
                                                                               "   ) " + 
                                                                               " )  order by tno_str_Empr");
            }
            else
            {
                listSetor = new Setor().Find<Setor>("nID_EMPR=" + cliente.Id + "  order by tno_str_Empr");
            }


            if (ProgressIniciar != null)
                ProgressIniciar(listSetor.Count);

            int OrdinalPosition = 1;

            foreach (Setor setor in listSetor)
            {
                newRow = ds.Tables[0].NewRow();

                if (laudo_Parametro != null)
                {
                    newRow["NomeEmpresa"] = "LISTA DE SETORES - " + cliente.NomeAbreviado +" -  Laudo: " + laudo.hDT_LAUDO.ToString("dd/MM/yyyy");
                }
                else
                {
                    newRow["NomeEmpresa"] = "LISTA DE SETORES - " + cliente.NomeAbreviado;
                }

                
                newRow["NomeSetor"] = setor.tNO_STR_EMPR;
                newRow["Descricao"] = setor.mDS_STR_EMPR;

                if (ProgressAtualizar != null)
                    ProgressAtualizar(OrdinalPosition++);

                ds.Tables[0].Rows.Add(newRow);
            }

            if (ProgressFinalizar != null)
                ProgressFinalizar();

            return ds;
        }
        #endregion

        #region GetTable

        private static DataTable GetDataTableClasFuncional()
		{
			DataTable table = new DataTable("Result");
			table.Columns.Add("NomeEmpresa", Type.GetType("System.String"));
			table.Columns.Add("NomeEmpregado", Type.GetType("System.String"));
			table.Columns.Add("LocalDeTrabalho", Type.GetType("System.String"));
			table.Columns.Add("InicioFuncao", Type.GetType("System.String"));
			table.Columns.Add("TerminoFuncao", Type.GetType("System.String"));
			table.Columns.Add("Cargo", Type.GetType("System.String"));
			table.Columns.Add("Setor", Type.GetType("System.String"));
			table.Columns.Add("Funcao", Type.GetType("System.String"));
			table.Columns.Add("GHE", Type.GetType("System.String"));		
			return table;
		}

        private static DataTable GetDataTableClasFuncionalImportacao()
        {
            DataTable table = new DataTable("Result");
            table.Columns.Add("NomeEmpresa", Type.GetType("System.String"));
            table.Columns.Add("NomeEmpregado", Type.GetType("System.String"));
            table.Columns.Add("DataAdmissao", Type.GetType("System.String"));
            table.Columns.Add("LocalDeTrabalho", Type.GetType("System.String"));
            table.Columns.Add("InicioFuncao", Type.GetType("System.String"));
            table.Columns.Add("TerminoFuncao", Type.GetType("System.String"));
            table.Columns.Add("Setor", Type.GetType("System.String"));
            table.Columns.Add("Funcao", Type.GetType("System.String"));
            table.Columns.Add("Arquivo", Type.GetType("System.String"));
            return table;
        }

        private static DataTable GetDataTableClasFuncionalPorGrupo()
        {
            DataTable table = new DataTable("Result");
            table.Columns.Add("NomeEmpresa", Type.GetType("System.String"));
            table.Columns.Add("NomeEmpregado", Type.GetType("System.String"));
            table.Columns.Add("DataAdmissao", Type.GetType("System.String"));
            table.Columns.Add("LocalDeTrabalho", Type.GetType("System.String"));
            table.Columns.Add("InicioFuncao", Type.GetType("System.String"));
            table.Columns.Add("TerminoFuncao", Type.GetType("System.String"));
            table.Columns.Add("Setor", Type.GetType("System.String"));
            table.Columns.Add("Funcao", Type.GetType("System.String"));
            table.Columns.Add("IdEmpregado", Type.GetType("System.Int32"));
            return table;
        }
        #endregion
    }
}
