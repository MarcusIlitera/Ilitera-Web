using System;
using System.Data;
using System.Collections;
using System.Text;

using Ilitera.Opsa.Data;
using Ilitera.Common;

namespace Ilitera.Sied.Report
{	
	public class DataSourceEmpregado : DataSourceBase
	{
        private Cliente cliente;
        private ArrayList alEmpregado;
        private Empregado.CriteriaBuilder criteria;

        #region EventProgress
        public override event EventProgress ProgressIniciar;
        public override event EventProgress ProgressAtualizar;
        public override event EventProgressFinalizar ProgressFinalizar;
        #endregion

        #region Constructor

        public DataSourceEmpregado(Empregado.CriteriaBuilder criteria)
        {
            this.cliente = criteria.cliente;
            this.criteria = criteria;
        }
        
        public DataSourceEmpregado(Cliente cliente, ArrayList listEmpregado)
		{
            this.cliente = cliente;
            this.alEmpregado = listEmpregado;
        }
        #endregion

        #region GetReport

        public RptListaDeEmpregados GetReport()
        {
            RptListaDeEmpregados report = new RptListaDeEmpregados();

            if (alEmpregado == null)
                report.SetDataSource(GetDSListaEmpregados());
            else
                report.SetDataSource(GetListaEmpregados());

            report.Refresh();

            SetTempoProcessamento(report);

            return report;
        }
        #endregion

        #region GetNomeAbreviado

        DataSet dsCliente;

        private string GetNomeAbreviado(int IdCliente)
        {
            if (dsCliente == null)
                dsCliente = new Cliente().GetIdNome("NomeAbreviado", GetCriteriaCliente());

            DataRow[] rows = dsCliente.Tables[0].Select("Id=" + IdCliente);

            if (rows.Length == 0)
                return string.Empty;

            return Convert.ToString(rows[0]["Nome"]);
        }

        private string GetCriteriaCliente()
        {
            string where;

            if (criteria.IndNivel == Empregado.CriteriaBuilder.Nivel.Empresa)
                where = "IdCliente=" + cliente.Id;
            else if (criteria.IndNivel == Empregado.CriteriaBuilder.Nivel.TodosGrupo)
                where = "IdGrupoEmpresa=" + cliente.IdGrupoEmpresa.Id;
            else if (criteria.IndNivel == Empregado.CriteriaBuilder.Nivel.TodosPcmso)
                where = "ContrataPCMSO<>0";
            else if (criteria.IndNivel == Empregado.CriteriaBuilder.Nivel.TodosClientes)
                where = "IdCliente IN (SELECT IdCliente FROM qryClienteAtivos)";
            else
                where = "IdCliente=" + cliente.Id;
            return where;
        }
        #endregion

        #region GetRegimeRevezamento

        private DataSet dsRegimeRevezamento;

        private string GetRegimeRevezamento(int IdRegimeRevezamento)
        {
            if (dsRegimeRevezamento == null)
                dsRegimeRevezamento = new RegimeRevezamento().GetIdNome("Descricao");

            DataRow[] rows = dsRegimeRevezamento.Tables[0].Select("Id=" + IdRegimeRevezamento);

            if (rows.Length == 0)
                return string.Empty;

            return Convert.ToString(rows[0]["Nome"]);
        }
        #endregion

        #region GetDSListaEmpregados

        private DataSet GetDSListaEmpregados()
        {
            DataSet ds = new DataSet();
            ds.Tables.Add(GetTable());

            DataRow newRow;
            DataSet dsEmpregado = new Empregado().Get(criteria.CriteriaString);

            if (ProgressIniciar != null)
                ProgressIniciar(dsEmpregado.Tables[0].Rows.Count);

            int OrdinalPosition = 1;

            foreach (DataRow row in dsEmpregado.Tables[0].Rows)
            {
                newRow = ds.Tables[0].NewRow();

                newRow["NomeEmpresa"] = GetNomeAbreviado(GetInt32(row["nID_EMPR"]));
                newRow["Nome"] = Convert.ToString(row["tNO_EMPG"]);
                newRow["RG"] = Convert.ToString(row["tNO_IDENTIDADE"]);
                newRow["PIS"] = (GetInt64(row["nNO_PIS_PASEP"]) != 0) ? GetInt64(row["nNO_PIS_PASEP"]).ToString() : string.Empty;
                newRow["DataNascimento"] = GetDateTimeToString(row["hDT_NASC"]);
                newRow["DataAdmissao"] = GetDateTimeToString(row["hDT_ADM"]);
                newRow["DataDemissao"] = GetDateTimeToString(row["hDT_DEM"]);
                newRow["CTPSSerieUF"] = Empregado.GetCTPS(Convert.ToString(row["tNUM_CTPS"]), Convert.ToString(row["tSER_CTPS"]), Convert.ToString(row["tUF_CTPS"]));
                newRow["Sexo"] = Convert.ToString(row["tSEXO"]);
                newRow["NumeroFoto"] = (GetInt32(row["nNO_FOTO"]) != 0) ? Convert.ToString(GetInt32(row["nNO_FOTO"])) : string.Empty;
                newRow["Registro"] = Convert.ToString(row["tCOD_EMPR"]);
                newRow["TipoBeneficiario"] = Empregado.GetTipoBeneficiario(GetInt32(row["nIND_BENEFICIARIO"]));
                newRow["RegimeRevezamento"] = GetRegimeRevezamento(GetInt32(row["nID_REGIME_REVEZAMENTO"]));

                ds.Tables[0].Rows.Add(newRow);

                if (ProgressAtualizar != null)
                    ProgressAtualizar(OrdinalPosition++);
            }

            if (ProgressFinalizar != null)
                ProgressFinalizar();

            return ds;
        }
        #endregion

        #region GetListaEmpregados

        private DataSet GetListaEmpregados()
        {
            DataSet ds = new DataSet();
            ds.Tables.Add(GetTable());

            DataRow newRow;

            foreach (Empregado empregado in alEmpregado)
            {
                newRow = ds.Tables[0].NewRow();

                newRow["NomeEmpresa"] = cliente.NomeAbreviado;
                newRow["Nome"] = empregado.tNO_EMPG;
                newRow["RG"] = empregado.tNO_IDENTIDADE;
                newRow["PIS"] = (empregado.nNO_PIS_PASEP != 0) ? Convert.ToString(empregado.nNO_PIS_PASEP) : string.Empty;
                newRow["DataNascimento"] = Ilitera.Common.Utility.TratarData(empregado.hDT_NASC);
                newRow["DataAdmissao"] = Ilitera.Common.Utility.TratarData(empregado.hDT_ADM);
                newRow["DataDemissao"] = Ilitera.Common.Utility.TratarData(empregado.hDT_DEM);
                newRow["CTPSSerieUF"] = empregado.GetCTPS();
                newRow["Sexo"] = empregado.tSEXO;
                newRow["NumeroFoto"] = (empregado.nNO_FOTO != 0) ? Convert.ToString(empregado.nNO_FOTO) : string.Empty;
                newRow["Registro"] = empregado.tCOD_EMPR;
                newRow["TipoBeneficiario"] = empregado.GetTipoBeneficiario();
                newRow["RegimeRevezamento"] = empregado.nID_REGIME_REVEZAMENTO.ToString();

                ds.Tables[0].Rows.Add(newRow);
            }
            return ds;
        }
        #endregion

        #region GetTable

        private static DataTable GetTable()
        {
            DataTable table = new DataTable("Result");
            table.Columns.Add("NomeEmpresa", Type.GetType("System.String"));
            table.Columns.Add("Nome", Type.GetType("System.String"));
            table.Columns.Add("RG", Type.GetType("System.String"));
            table.Columns.Add("PIS", Type.GetType("System.String"));
            table.Columns.Add("DataNascimento", Type.GetType("System.String"));
            table.Columns.Add("DataAdmissao", Type.GetType("System.String"));
            table.Columns.Add("DataDemissao", Type.GetType("System.String"));
            table.Columns.Add("CTPSSerieUF", Type.GetType("System.String"));
            table.Columns.Add("Sexo", Type.GetType("System.String"));
            table.Columns.Add("NumeroFoto", Type.GetType("System.String"));
            table.Columns.Add("Registro", Type.GetType("System.String"));
            table.Columns.Add("TipoBeneficiario", Type.GetType("System.String"));
            table.Columns.Add("RegimeRevezamento", Type.GetType("System.String"));
            return table;
        }

        #endregion
    }
}
