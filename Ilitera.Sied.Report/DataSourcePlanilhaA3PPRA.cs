using System;
using Ilitera.Opsa.Data;
using System.Data;
using Ilitera.Common;
using System.Collections;
using System.Text;

namespace Ilitera.Sied.Report
{
    public class DataSourcePlanilhaA3PPRA
    {
        private Cliente cliente;
        private LaudoTecnico laudoTecnico;

        public DataSourcePlanilhaA3PPRA(Cliente cliente)
        {
            this.cliente = cliente;
            this.cliente.Find();
            laudoTecnico = LaudoTecnico.GetUltimoLaudo(this.cliente.Id);
        }

        public DataSourcePlanilhaA3PPRA(LaudoTecnico laudoTecnico)
        {
            this.laudoTecnico = laudoTecnico;
            cliente = this.laudoTecnico.nID_EMPR;
            cliente.Find();
        }

        public DataSourcePlanilhaA3PPRA(int IdLaudoTecnico)
        {
            laudoTecnico = new LaudoTecnico();
            laudoTecnico.Find(IdLaudoTecnico);
            this.cliente = laudoTecnico.nID_EMPR;
            this.cliente.Find();
        }

        public RptPlanilhaPPRA GetReport()
        {
            RptPlanilhaPPRA report = new RptPlanilhaPPRA();
            report.SetDataSource(GetPlanilhaPPRA());
            report.Refresh();
            return report;
        }

        private DataSet GetPlanilhaPPRA()
        {
            DataSet ds = new DataSet();
            DataTable table = new DataTable("Result");
            table.Columns.Add("CarimboCNPJ", Type.GetType("System.String"));
            table.Columns.Add("Ghe", Type.GetType("System.String"));
            table.Columns.Add("DataLevantamento", Type.GetType("System.String"));
            table.Columns.Add("DescricaoFuncao", Type.GetType("System.String"));
            table.Columns.Add("TrabalhadoresExpostos", Type.GetType("System.String"));
            table.Columns.Add("RiscosFisicos", Type.GetType("System.String"));
            table.Columns.Add("RiscosQuimicosQuantitativo", Type.GetType("System.String"));
            table.Columns.Add("RiscosQuimicosQualitativo", Type.GetType("System.String"));
            table.Columns.Add("RiscosBiologicos", Type.GetType("System.String"));
            table.Columns.Add("RiscosFisicosRuidoDb", Type.GetType("System.String"));
            table.Columns.Add("RiscosFisicosRuidoLimiteTolerancia", Type.GetType("System.String"));
            table.Columns.Add("RiscosFisicosCalorIBUTG", Type.GetType("System.String"));
            table.Columns.Add("RiscosFisicosCalorLimiteTolerancia", Type.GetType("System.String"));
            table.Columns.Add("RiscosQuimicosValor", Type.GetType("System.String"));
            table.Columns.Add("RiscosQuimicosLimiteTolerancia", Type.GetType("System.String"));
            table.Columns.Add("TempoDeExposicao", Type.GetType("System.String"));
            table.Columns.Add("EPI", Type.GetType("System.String"));
            table.Columns.Add("EPC", Type.GetType("System.String"));
            table.Columns.Add("ExameMedico", Type.GetType("System.String"));
            table.Columns.Add("Periodicidade", Type.GetType("System.String"));
            table.Columns.Add("bRuidoUltrapassado", Type.GetType("System.Boolean"));
            table.Columns.Add("bAnalQuantAgQuimico", Type.GetType("System.Boolean"));
            table.Columns.Add("bAnalQuantCalor", Type.GetType("System.Boolean"));
            ds.Tables.Add(table);
            DataRow newRow;

            ArrayList listGhe = new Ghe().Find("nID_LAUD_TEC=" + laudoTecnico.Id + " ORDER BY tNO_FUNC");

            bool AnalQuantAgQuim = ExisteAnaliseQuantAgQuim(laudoTecnico);
            bool AnalQuantAgFis = ExisteAnaliseQuantAgFis(laudoTecnico);

            string dataLevantamento = "ANO " + laudoTecnico.hDT_LAUDO.Year.ToString();

            foreach (Ghe ghe in listGhe)
            {
                ghe.nID_TEMPO_EXP.Find();
                newRow = ds.Tables[0].NewRow();

                int numTrabExpostos = ghe.GetNumeroEmpregadosExpostos(false);

                if (numTrabExpostos == 0)
                    continue;

                newRow["CarimboCNPJ"] = cliente.GetCarimboCnpjHtml(laudoTecnico.hDT_LAUDO);
                newRow["Ghe"] = ghe.tNO_FUNC;
                newRow["DataLevantamento"] = dataLevantamento;
                newRow["DescricaoFuncao"] = ghe.FuncoesIntegrantes();
                newRow["TrabalhadoresExpostos"] = numTrabExpostos.ToString();
                newRow["RiscosFisicos"] = ghe.RiscosFisicos();
                newRow["RiscosQuimicosQuantitativo"] = ghe.RiscosQuimicosQuantitativo();
                newRow["RiscosQuimicosQualitativo"] = ghe.RiscosQuimicosQualitativo();
                newRow["RiscosBiologicos"] = ghe.RiscosBiologicos();
                newRow["RiscosFisicosRuidoDb"] = ghe.RuidosdBA();
                newRow["RiscosFisicosRuidoLimiteTolerancia"] = ghe.RuidosLimite();
                newRow["RiscosFisicosCalorIBUTG"] = ghe.CalorIBUTG();
                newRow["RiscosFisicosCalorLimiteTolerancia"] = ghe.CalorLimite();
                newRow["RiscosQuimicosValor"] = ghe.RiscosQuimicosQuantitativoValor();
                newRow["RiscosQuimicosLimiteTolerancia"] = ghe.RiscosQuimicosQuantitativoLimite();
                newRow["TempoDeExposicao"] = ghe.nID_TEMPO_EXP.tHORA;
                newRow["EPI"] = ghe.Epi();
                newRow["EPC"] = ghe.Epc();
                newRow["bRuidoUltrapassado"] = ghe.RuidoUltrapassado();
                newRow["bAnalQuantAgQuimico"] = AnalQuantAgQuim;
                newRow["bAnalQuantCalor"] = AnalQuantAgFis;
                ds.Tables[0].Rows.Add(newRow);
            }
            return ds;
        }

        public bool ExisteAnaliseQuantAgQuim(LaudoTecnico laudoTecnico)
        {
            ArrayList listQuimico = new PPRA().Find("nID_LAUD_TEC=" + laudoTecnico.Id + " AND nID_RSC IN(9,10,11,12,13,16) AND IsNull(tVL_MED,0) = 0");
            return listQuimico.Count > 0;
        }

        public bool ExisteAnaliseQuantAgFis(LaudoTecnico laudoTecnico)
        {
            ArrayList listFisico = new PPRA().Find("nID_LAUD_TEC=" + laudoTecnico.Id + " AND nID_RSC=2 AND bDESC = 1");
            return listFisico.Count > 0;
        }
    }
}
