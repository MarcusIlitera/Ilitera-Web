using System;
using System.Collections;
using System.Data;
using System.Text;
using System.Collections.Generic;

using Ilitera.Common;
using Ilitera.Opsa.Data;

namespace Ilitera.PCMSO.Report
{
    public class DataSourceExamePlanejado : DataSourceBase
    {
        #region Variaveis

        private Pcmso pcmso;
        private int IndExameTipo;
        private int IdExameDicionario;
        private DateTime renovacaoAte;
        private bool bRevovacao;
        private DateTime vencimento;
        private bool Vencidos;
        private bool AdmitidosApos90dias;
        private int IdGrupoEmpresa;
        private bool TodosClientes;
        private DataSet dsGhe;
        private ConvocacaoExame convocacaoExame;
        private Int32 xTipo;

        #endregion

        #region Enum

        public enum TipoRelatorio : int
        {
            PorEmpregado,
            PorExame,
            ProgramaMedicoEspecial
        }
        #endregion

        #region Contrutor



        public DataSourceExamePlanejado(Pcmso pcmso,
                                            int IndExameTipo,
                                            int IdExameDicionario,
                                            DateTime renovacaoAte,
                                            bool bRevovacao,
                                            DateTime vencimento,
                                            bool Vencidos,
                                            bool AdmitidosApos90dias,
                                            int IdGrupoEmpresa,
                                            bool TodosClientes)
            : this(pcmso)
        {
            this.renovacaoAte = renovacaoAte;
            this.bRevovacao = bRevovacao;
            this.vencimento = vencimento;
            this.Vencidos = Vencidos;
            this.IndExameTipo = IndExameTipo;
            this.IdExameDicionario = IdExameDicionario;
            this.AdmitidosApos90dias = AdmitidosApos90dias;
            this.IdGrupoEmpresa = IdGrupoEmpresa;
            this.TodosClientes = TodosClientes;
        }

        public DataSourceExamePlanejado(Pcmso pcmso, Int32 xTipo)
        {
            this.pcmso = pcmso;
            this.xTipo = xTipo;

            if (this.pcmso != null && this.pcmso.IsFromWeb)
                throw new Exception("PCMSO de terceiros, não realizado pela Mestra!");
        }

        public DataSourceExamePlanejado(Pcmso pcmso)
        {
            this.pcmso = pcmso;

            if (this.pcmso != null && this.pcmso.IsFromWeb)
                throw new Exception("PCMSO de terceiros, não realizado pela Mestra!");
        }

        public DataSourceExamePlanejado(Pcmso pcmso, ConvocacaoExame convocacaoExame)
        {
            this.pcmso = pcmso;
            this.convocacaoExame = convocacaoExame;

            if (this.pcmso != null && this.pcmso.IsFromWeb)
                throw new Exception("PCMSO de terceiros, não realizado pela Mestra!");
        }

       
        #endregion

        #region Reports

        public RptExamePlanejados GetReportExamePorEmpregado()
        {
            DataSet dsResult = GetDataSource();

            return GetReportExamePorEmpregado(dsResult);
        }

        public RptExamePlanejados GetReportExamePorEmpregado(DataSet dsResult)
        {
            RptExamePlanejados report = new RptExamePlanejados();
            report.SetDataSource(GetDataSource(TipoRelatorio.PorEmpregado, dsResult));
            report.OpenSubreport("CoordenadorPcmso").SetDataSource(pcmso.GetDSCoordenador());
            report.Refresh();
            SetTempoProcessamento(report);
            return report;
        }

        public RptExamePlanPorData GetReportExamePorData()
        {
            DataSet dsResult = new ExameDicionario().ExecuteDataset(GetQuery());

            return GetReportExamePorData(dsResult);
        }

        public RptExamePlanPorData GetReportExamePorData(DataSet dsResult)
        {
            RptExamePlanPorData report = new RptExamePlanPorData();
            report.SetDataSource(GetDataSource(TipoRelatorio.PorExame, dsResult));
            report.OpenSubreport("CoordenadorPcmso").SetDataSource(pcmso.GetDSCoordenador());
            report.Refresh();
            SetTempoProcessamento(report);
            return report;
        }

        public RptExamePlanProgramaEspecial GetReportProgramaEspecial()
        {
            DataSet dsResult = new ExameDicionario().ExecuteDataset(GetQuery());

            RptExamePlanProgramaEspecial report = new RptExamePlanProgramaEspecial();
            report.SetDataSource(GetDataSource(TipoRelatorio.ProgramaMedicoEspecial, dsResult));
            report.OpenSubreport("CoordenadorPcmso").SetDataSource(pcmso.GetDSCoordenador());
            report.Refresh();
            SetTempoProcessamento(report);
            return report;
        }
        #endregion

        #region Query

        private string GetQuery()
        {
            StringBuilder str = new StringBuilder();

            if (xTipo == 1)
            {
                str.Append("USE " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + " ");
                str.Append("SELECT NomeEmpregado, convert( char(10),DataAdmissao,103 ) as Data_Admissao, NomeExame  as NomeExame, convert( char(10),DataVencimento, 103 ) as Data_Proximo_Exame, Sexo, convert( char(10),DataNascEmpregado, 103 ) as Data_Nascimento, IdadeEmpregado as Idade, convert( char(10),DataUltima , 103 ) as Data_Ultimo_Exame, f.NomeFuncao as Funcao, e.tNo_Str_Empr as Setor ");
                str.Append("FROM qryExamePlanejamento as a  ");

                str.Append(" left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblempregado as b  ");
                str.Append(" on ( a.idEmpregado = b.nId_Empregado )  ");
                str.Append(" left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEmpregado_Funcao as c  ");
                str.Append(" on ( b.nId_Empregado = c.nId_Empregado   ");

                str.Append(" and c.nId_Empr = " + this.pcmso.IdCliente.Id.ToString() + " ) ");

                str.Append(" join (   ");
                str.Append("    select convert( char(15), nId_Empregado ) + convert( char(10),max( hDt_Inicio ),103 ) as Chave  ");
                str.Append("    from  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblempregado_Funcao  ");

                str.Append(" where nId_Empr = " + this.pcmso.IdCliente.Id.ToString() + " ");

                str.Append("    group by nId_Empregado  ");
                str.Append(" ) as tx80 on tx80.Chave = convert( char(15), c.nId_Empregado ) + convert( char(10),c.hDt_Inicio,103 )  ");
                str.Append(" left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblSetor as e  ");
                str.Append(" on ( c.nId_Setor = e.nId_Setor )  ");
                str.Append(" left join Funcao as f  ");
                str.Append(" on ( c.nID_FUNCAO = f.IdFuncao )  ");                


                str.Append("WHERE " + GetFiltro() + " and datavencimento is not null and datademissao is null ");
                str.Append("order by datavencimento, nomeEmpregado ");
            }
            else
            {
                str.Append("USE " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + " ");
                str.Append("SELECT NomeEmpregado, convert( char(10),DataAdmissao,103 ) as Data_Admissao, NomeExame as NomeExame, convert( char(10),DataVencimento, 103 ) as Data_Proximo_Exame, Sexo, convert( char(10),DataNascEmpregado, 103 ) as Data_Nascimento, IdadeEmpregado as Idade, convert( char(10),DataUltima , 103 ) as Data_Ultimo_Exame, f.NomeFuncao as Funcao, e.tNo_Str_Empr as Setor ");
                str.Append("FROM qryExamePlanejamento as a ");

                str.Append(" left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblempregado as b  ");
                str.Append(" on ( a.idEmpregado = b.nId_Empregado )  ");
                str.Append(" left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEmpregado_Funcao as c  ");
                str.Append(" on ( b.nId_Empregado = c.nId_Empregado   ");

                str.Append(" and c.nId_Empr = " + this.pcmso.IdCliente.Id.ToString() + " ) ");

                str.Append(" join (   ");
                str.Append("    select convert( char(15), nId_Empregado ) + convert( char(10),max( hDt_Inicio ),103 ) as Chave  ");
                str.Append("    from  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblempregado_Funcao  ");

                str.Append(" where nId_Empr = " + this.pcmso.IdCliente.Id.ToString() + " ");

                str.Append("    group by nId_Empregado  ");
                str.Append(" ) as tx80 on tx80.Chave = convert( char(15), c.nId_Empregado ) + convert( char(10),c.hDt_Inicio,103 )  ");
                str.Append(" left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblSetor as e  ");
                str.Append(" on ( c.nId_Setor = e.nId_Setor )  ");
                str.Append(" left join Funcao as f  ");
                str.Append(" on ( c.nID_FUNCAO = f.IdFuncao )  ");

                str.Append("WHERE " + GetFiltro() + " and datavencimento is null and datademissao is null ");
                str.Append("order by datavencimento, nomeEmpregado ");
            }
            
            return str.ToString();
        }

        private string GetFiltro()
        {
            StringBuilder str = new StringBuilder();

            if (!TodosClientes && IdGrupoEmpresa == 0 && pcmso != null && pcmso.Id != 0)
                str.Append(" IdPcmso=" + pcmso.Id);
            else if (!TodosClientes && IdGrupoEmpresa != 0)
                str.Append(" IdPcmso IN (SELECT IdPcmso FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.qryUltimoPCMSO WHERE IdCliente IN (SELECT IdJuridica FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.Juridica WHERE IdGrupoEmpresa=" + IdGrupoEmpresa + ")) ");
            else if (TodosClientes)
                str.Append(" IdPcmso IN (SELECT IdPcmso FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.qryUltimoPCMSO) " );
            else
                str.Append(" IdPcmso=0 ");

            return str.ToString();
        }
        #endregion

        #region GetNumeroEmpregados

        public int GetNumeroEmpregados()
        {
            string sql = "SELECT COUNT(DISTINCT IdEmpregado)"
                    + " FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.ExamePlanejamento"
                    + " WHERE " + GetFiltro();

            int count = Convert.ToInt32(new Empregado().ExecuteScalar(sql));

            return count;
        }
        #endregion

        #region DataSource

        public DataSet GetDataSource()
        {
            DataSet dsResult = new ExameDicionario().ExecuteDataset(GetQuery());

            return dsResult;
        }

        public DataSet GetDataSource(TipoRelatorio tipoRelatorio, DataSet dsResult)
        {
            DataSet ds = new DataSet();

            ds.Tables.Add(GetDataTableExamePlanejados());


            string strCarimboCNPJ = this.pcmso.IdCliente.GetCarimboCnpjHtml(this.pcmso.DataPcmso);

            string strDataPcmso;

            if (tipoRelatorio == TipoRelatorio.ProgramaMedicoEspecial)
                strDataPcmso = pcmso.DataPcmso.ToString("dd-MM-yyyy");
            else
                strDataPcmso = "Período " + pcmso.GetPeriodo();

            DataRow newRow;

            foreach (DataRow row in dsResult.Tables[0].Rows)
            {

                newRow = ds.Tables[0].NewRow();
                newRow["CarimboCNPJ"] = strCarimboCNPJ;
                newRow["DataPcmso"] = strDataPcmso;
                
                newRow["NomeEmpregado"] = row["NomeEmpregado"];

                newRow["Data_Admissao"] = row["Data_Admissao"];
                newRow["Sexo"] = row["Sexo"];
                newRow["Data_Nascimento"] = row["Data_Nascimento"];
                newRow["Idade"] = row["Idade"];
                newRow["NomeExame"] = row["NomeExame"];

                newRow["Data_Ultimo_Exame"] = row["Data_Ultimo_Exame"];
                newRow["Data_Proximo_Exame"] = row["Data_Proximo_Exame"];

                newRow["Funcao"] = row["Funcao"];
                newRow["Setor"] = row["Setor"];

                ds.Tables[0].Rows.Add(newRow);
            }
            return ds;
        }

        private string GetRiscosOcupacionais(int IdGhe)
        {
            if (dsGhe == null)
                GetDataSourceGhe();

            DataRow[] rows = dsGhe.Tables[0].Select("IdGhe=" + IdGhe);

            if (rows.Length > 0)
                return (string)rows[0]["Risco"];
            else
                return string.Empty;
        }

        private DataSet GetDataSourceGhe()
        {
            dsGhe = new DataSet();
            DataTable table = GetTableGhe();
            dsGhe.Tables.Add(table);

            ArrayList listGhe = new Ghe().Find("nID_FUNC IN (SELECT IdGhe FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.PcmsoPlanejamento WHERE IdPcmso=" + this.pcmso.Id + ")");

            DataRow newRow;

            foreach (Ghe ghe in listGhe)
            {
                newRow = dsGhe.Tables[0].NewRow();
                newRow["IdGhe"] = ghe.Id; ;
                newRow["Nome"] = ghe.tNO_FUNC;
                newRow["Risco"] = ghe.RiscosOcupacionais();
                dsGhe.Tables[0].Rows.Add(newRow);
            }
            return dsGhe;
        }

        private DataSet GetDataSourceCoordenador(Prestador prestador)
        {
            DataSet ds = new DataSet();
            DataTable table = GetTableCoordenador();
            ds.Tables.Add(table);
            DataRow newRow;
            prestador.Find();
            newRow = ds.Tables[0].NewRow();
            newRow["IdPrestador"] = prestador.Id;
            newRow["DataAssinatura"] = "";
            newRow["Nome"] = prestador.NomeCompleto;
            newRow["Titulo"] = prestador.Titulo;
            newRow["Numero"] = prestador.Numero;
            newRow["Contato"] = prestador.Contato;
            ds.Tables[0].Rows.Add(newRow);
            return ds;
        }

        #endregion

        #region Table

        private static DataTable GetDataTableExamePlanejados()
        {
            DataTable table = new DataTable("Result");
            table.Columns.Add("CarimboCNPJ", Type.GetType("System.String"));
            table.Columns.Add("DataPcmso", Type.GetType("System.String"));            
            table.Columns.Add("NomeEmpregado", Type.GetType("System.String"));
            table.Columns.Add("Data_Admissao", Type.GetType("System.String"));
            table.Columns.Add("Sexo", Type.GetType("System.String"));
            table.Columns.Add("Data_Nascimento", Type.GetType("System.String"));
            table.Columns.Add("Idade", Type.GetType("System.String"));
            table.Columns.Add("NomeExame", Type.GetType("System.String"));            
            table.Columns.Add("Data_Proximo_Exame", Type.GetType("System.String"));
            table.Columns.Add("Data_Ultimo_Exame", Type.GetType("System.String"));
            table.Columns.Add("Setor", Type.GetType("System.String"));
            table.Columns.Add("Funcao", Type.GetType("System.String"));
            return table;
        }

        private static DataTable GetTableCoordenador()
        {
            DataTable table = new DataTable("Result");
            table.Columns.Add("IdPrestador", Type.GetType("System.Int32"));
            table.Columns.Add("Nome", Type.GetType("System.String"));
            table.Columns.Add("Numero", Type.GetType("System.String"));
            table.Columns.Add("Titulo", Type.GetType("System.String"));
            table.Columns.Add("Contato", Type.GetType("System.String"));
            table.Columns.Add("DataAssinatura", Type.GetType("System.String"));
            table.Columns.Add("iDocumento", Type.GetType("System.Byte[]"));
            table.Columns.Add("iAssinatura", Type.GetType("System.Byte[]"));
            return table;
        }

        private static DataTable GetTableGhe()
        {
            DataTable table = new DataTable("Result");
            table.Columns.Add("IdGhe", Type.GetType("System.Int32"));
            table.Columns.Add("Nome", Type.GetType("System.String"));
            table.Columns.Add("Risco", Type.GetType("System.String"));
            return table;
        }

        #endregion
    }
}
