using System;
using System.Data;
using System.Collections;
using Ilitera.Opsa.Data;
using Ilitera.Common;

namespace Ilitera.PCMSO.Report
{
    public class DataSourceRelatorioAnualPorEmpregado : DataSourceBase
    {
        private Pcmso pcmso;

        public DataSourceRelatorioAnualPorEmpregado(Cliente cliente)
        {
            ArrayList list = new Pcmso().FindMax("DataPcmso", "IdCliente=" + cliente.Id);

            if (list.Count == 1)
                this.pcmso = (Pcmso)list[0];
            else
                throw new Exception("A empresa " + cliente.NomeAbreviado + " não possui ainda nenhum PCMSO realizado!");
        }

        public DataSourceRelatorioAnualPorEmpregado(Pcmso pcmso)
        {
            this.pcmso = pcmso;
            if (this.pcmso.IsFromWeb)
                throw new Exception("PCMSO de terceiros, não realizado pela Mestra!");
        }

        public RptRelatorioAnualPorEmpregado GetReport()
        {
            RptRelatorioAnualPorEmpregado report = new RptRelatorioAnualPorEmpregado();
            report.OpenSubreport("CoordenadorPcmso").SetDataSource(pcmso.GetDSCoordenador());
            report.SetDataSource(this.GetDataSouce());
            report.Refresh();
            SetTempoProcessamento(report);
            return report;
        }

        private DataTable GetTableRelatorioAnual()
        {
            DataTable table = new DataTable("Result");
            table.Columns.Add("CarimboCNPJ", Type.GetType("System.String"));
            table.Columns.Add("DataRelatorio", Type.GetType("System.String"));
            table.Columns.Add("GHE", Type.GetType("System.String"));
            table.Columns.Add("Empregado", Type.GetType("System.String"));
            table.Columns.Add("Funcao", Type.GetType("System.String"));
            table.Columns.Add("Inicio", Type.GetType("System.String"));
            table.Columns.Add("Termino", Type.GetType("System.String"));
            table.Columns.Add("DataExame", Type.GetType("System.String"));
            table.Columns.Add("NaturezaExame", Type.GetType("System.String"));
            return table;
        }

        public DataSet GetDataSouce()
        {
            bool ComRegistro = false;

            DataSet ds = new DataSet();

            ds.Tables.Add(GetTableRelatorioAnual());

            DataRow newRow;

            if (pcmso.IdCliente.mirrorOld == null)
                pcmso.IdCliente.Find();

            string carimboCNPJ = pcmso.IdCliente.GetCarimboCnpjHtml(this.pcmso.DataPcmso);
            string periodo = "Período " + pcmso.GetPeriodo();

            ArrayList list = new Ghe().Find("nID_LAUD_TEC=" + pcmso.IdLaudoTecnico.Id + " ORDER BY tNO_FUNC");

            foreach (Ghe ghe in list)
            {
                string str = "DataExame BETWEEN '" + pcmso.DataPcmso.ToString("yyyy-MM-dd") + "' AND '" + pcmso.GetTerninoPcmso().ToString("yyyy-MM-dd") + "'"
                                + " AND IndResultado<>" + (int)ResultadoExame.NaoRealizado
                                + " AND IndResultado<>" + (int)ResultadoExame.EmEspera
                                + " AND IdEmpregado IN (SELECT nID_EMPREGADO FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEMPREGADO_FUNCAO WHERE nID_EMPREGADO_FUNCAO IN (SELECT nID_EMPREGADO_FUNCAO FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblFUNC_EMPREGADO WHERE nID_FUNC = " + ghe.Id + "))"
                                + " ORDER BY (SELECT tNO_EMPG FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEMPREGADO WHERE IdEmpregado = nID_EMPREGADO), DataExame";

                ArrayList listExames = new ExameBase().Find(str);

                foreach (ExameBase exame in listExames)
                {
                    newRow = ds.Tables[0].NewRow();

                    exame.IdExameDicionario.Find();
                    exame.IdEmpregado.Find();
                    EmpregadoFuncao empreFunc = EmpregadoFuncao.GetEmpregadoFuncao(exame.IdEmpregado);

                    empreFunc.nID_FUNCAO.Find();

                    newRow["CarimboCNPJ"] = carimboCNPJ;
                    newRow["DataRelatorio"] = periodo;
                    newRow["GHE"] = ghe.tNO_FUNC;
                    newRow["Empregado"] = exame.IdEmpregado.tNO_EMPG;
                    newRow["Funcao"] = empreFunc.nID_FUNCAO.NomeFuncao;
                    newRow["Inicio"] = exame.IdEmpregado.hDT_ADM.ToString("dd-MM-yyyy");
                    newRow["Termino"] = exame.IdEmpregado.hDT_DEM != new DateTime() ? exame.IdEmpregado.hDT_DEM.ToString("dd-MM-yyyy") : "";
                    newRow["DataExame"] = exame.DataExame.ToString("dd-MM-yyyy");
                    newRow["NaturezaExame"] = exame.IdExameDicionario.Nome;

                    ComRegistro = true;

                    ds.Tables[0].Rows.Add(newRow);
                }
            }

            if (!ComRegistro)
            {
                newRow = ds.Tables[0].NewRow();

                ds.Tables[0].Rows.Add(newRow);
            }

            return ds;
        }
    }
}
