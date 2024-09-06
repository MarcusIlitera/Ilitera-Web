using System;
using System.Data;
using System.Text;
using System.Collections.Generic;

using Ilitera.Opsa.Data;
using Ilitera.Common;

namespace Ilitera.PCMSO.Report
{
    public class DataSourceRelatorioAnual : DataSourceBase
    {
        private Pcmso pcmso;

        public DataSourceRelatorioAnual(Cliente cliente)
        {
            this.pcmso = cliente.GetUltimoPcmso();

            if (this.pcmso.Id == 0)
                throw new Exception("A empresa " + cliente.NomeAbreviado + " não possui ainda nenhum PCMSO realizado!");

            if (this.pcmso.IsFromWeb)
                throw new Exception("PCMSO de terceiros, não realizado pela Mestra!");
        }

        public DataSourceRelatorioAnual(Pcmso pcmso)
        {
            this.pcmso = pcmso;

            if (this.pcmso.IsFromWeb)
                throw new Exception("PCMSO de terceiros, não realizado pela Mestra!");
        }

        public RptRelatorioAnual GetReport()
        {
            RptRelatorioAnual report = new RptRelatorioAnual();
            report.OpenSubreport("CoordenadorPcmso").SetDataSource(pcmso.GetDSCoordenador());
            report.SetDataSource(this.GetRelatorioAnual());
            report.Refresh();
            SetTempoProcessamento(report);
            return report;
        }

        private DataTable GetTableRelatorioAnual()
        {
            DataTable table = new DataTable("Result");
            table.Columns.Add("CarimboCNPJ", Type.GetType("System.String"));
            table.Columns.Add("TrabalhadoresExpostos", Type.GetType("System.String"));
            table.Columns.Add("GHE", Type.GetType("System.String"));
            table.Columns.Add("Funcoes", Type.GetType("System.String"));
            table.Columns.Add("NaturezaExame", Type.GetType("System.String"));
            table.Columns.Add("NumExamesRealizados", Type.GetType("System.String"));
            table.Columns.Add("NumResultadosAnormais", Type.GetType("System.String"));
            table.Columns.Add("PctResultadosAnormais", Type.GetType("System.String"));
            table.Columns.Add("NumExamesAnoSeguinte", Type.GetType("System.String"));
            table.Columns.Add("DataRelatorio", Type.GetType("System.String"));
            table.Columns.Add("ResponsavelNome", Type.GetType("System.String"));
            table.Columns.Add("ResponsavelNumero", Type.GetType("System.String"));
            table.Columns.Add("ResponsavelTitulo", Type.GetType("System.String"));
            table.Columns.Add("ResponsavelAssinatura", Type.GetType("System.Byte[]"));
            return table;
        }

        private int GetNumeroExames(ExameDicionario exameDicionario, Pcmso pcmso, Ghe ghe)
        {
            int numExames;

            string str = "IdExameDicionario=" + exameDicionario.Id
                        + " AND DataExame BETWEEN '" + pcmso.DataPcmso.ToString("yyyy-MM-dd")
                        + "' AND '" + pcmso.GetTerninoPcmso().ToString("yyyy-MM-dd") + "'"
                        + " AND IndResultado<>" + (int)ResultadoExame.NaoRealizado
                        + " AND IndResultado<>" + (int)ResultadoExame.EmEspera
                        + " AND IdEmpregado "
                        + " IN (SELECT nID_EMPREGADO FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEMPREGADO_FUNCAO WHERE nID_EMPREGADO_FUNCAO "
                        + " IN (SELECT nID_EMPREGADO_FUNCAO FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblFUNC_EMPREGADO WHERE nID_FUNC = " + ghe.Id + ")) ";

            numExames = new ExameBase().ExecuteCount(str);

            return numExames;
        }

        private int GetNumeroExamesInaptos(ExameDicionario exameDicionario, Pcmso pcmso, Ghe ghe)
        {
            int numExames;

            string str = "IdExameDicionario=" + exameDicionario.Id
                + " AND DataExame BETWEEN '" + pcmso.DataPcmso.ToString("yyyy-MM-dd")
                + "' AND '" + pcmso.GetTerninoPcmso().ToString("yyyy-MM-dd") + "'"
                + " AND IndResultado=" + (int)ResultadoExame.Alterado
                + " AND IdEmpregado "
                + " IN (SELECT nID_EMPREGADO FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEMPREGADO_FUNCAO WHERE nID_EMPREGADO_FUNCAO "
                + " IN (SELECT nID_EMPREGADO_FUNCAO FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblFUNC_EMPREGADO WHERE nID_FUNC = " + ghe.Id + ")) ";

            numExames = new ExameBase().ExecuteCount(str);

            return numExames;
        }

        private int GetNumeroExamesCalculado(ExameDicionario exameDicionario, Pcmso pcmso, Ghe ghe)
        {
            int numExames;

            if (exameDicionario.Id == (int)IndExameClinico.Admissional)
                numExames = pcmso.GetNumFuncionariosAdmitidos(ghe.Id);
            else if (exameDicionario.Id == (int)IndExameClinico.Demissional)
                numExames = pcmso.GetNumFuncionariosDemitidos(ghe.Id);
            else if (exameDicionario.Id == (int)IndExameClinico.MudancaDeFuncao)
                numExames = GetNumeroExames(exameDicionario, pcmso, ghe);
            else if (exameDicionario.Id == (int)IndExameClinico.RetornoAoTrabalho)
                numExames = GetNumeroExames(exameDicionario, pcmso, ghe);
            else
                numExames = ghe.GetNumeroEmpregadosExpostos(true);

            return numExames;
        }

        private DataSet GetRelatorioAnual()
        {
            DataSet ds = new DataSet();
            ds.Tables.Add(GetTableRelatorioAnual());

            DataRow newRow;

            List<Ghe> ghes = new Ghe().Find<Ghe>("nID_LAUD_TEC=" + pcmso.IdLaudoTecnico.Id 
                                                   + " ORDER BY tNO_FUNC");
            foreach (Ghe ghe in ghes)
            {
                newRow = ds.Tables[0].NewRow();

                int numTrabExpostos = ghe.GetNumeroEmpregadosExpostos(false);
                int numTrabExpostosAtivos = ghe.GetNumeroEmpregadosExpostos(true);

                if (numTrabExpostos == 0)
                    continue;

                newRow["DataRelatorio"] = "Período " + pcmso.GetPeriodo();
                newRow["CarimboCNPJ"] = pcmso.IdCliente.GetCarimboCnpjHtml(this.pcmso.DataPcmso);
                newRow["GHE"] = ghe.tNO_FUNC;
                newRow["Funcoes"] = ghe.FuncoesIntegrantes();
                newRow["TrabalhadoresExpostos"] = numTrabExpostos.ToString();

                string criteria = "IsObservacao=0"
                            + " AND IdExameDicionario IN (1,2,3,5)"
                            + " OR (IsObservacao=0 AND IdExameDicionario IN"
                            + " (SELECT IdExameDicionario FROM PcmsoPlanejamento WHERE"
                            + " IdPcmso=" + pcmso.Id + " AND IdGhe=" + ghe.Id + "))"
                            + " ORDER BY IndExame, IdExameDicionario";

                List<ExameDicionario> examesDic = new ExameDicionario().Find<ExameDicionario>(criteria.ToString());

                foreach (ExameDicionario exameDicionario in examesDic)
                {
                    float numExames;

                    if (pcmso.IsRelatorioAnualCalculado)
                        numExames = Convert.ToSingle(GetNumeroExamesCalculado(exameDicionario, pcmso, ghe));
                    else
                        numExames = Convert.ToSingle(GetNumeroExames(exameDicionario, pcmso, ghe));

                    float numInaptos = Convert.ToSingle(GetNumeroExamesInaptos(exameDicionario, pcmso, ghe));

                    if (numExames == 0 && (exameDicionario.Id == (int)IndExameClinico.Admissional
                                        || exameDicionario.Id == (int)IndExameClinico.Demissional
                                        || exameDicionario.Id == (int)IndExameClinico.MudancaDeFuncao
                                        || exameDicionario.Id == (int)IndExameClinico.RetornoAoTrabalho))
                        continue;

                    newRow["NaturezaExame"] = Convert.ToString(newRow["NaturezaExame"]) + exameDicionario.Nome + "\n";
                    newRow["NumExamesRealizados"] = Convert.ToString(newRow["NumExamesRealizados"]) + numExames.ToString() + "\n";
                    newRow["NumResultadosAnormais"] = Convert.ToString(newRow["NumResultadosAnormais"]) + numInaptos.ToString() + "\n";

                    if (numExames != 0)
                        newRow["PctResultadosAnormais"] = Convert.ToString(newRow["PctResultadosAnormais"]) + Convert.ToSingle((numInaptos / numExames)).ToString("p") + "\n";
                    else
                        newRow["PctResultadosAnormais"] = Convert.ToString(newRow["PctResultadosAnormais"]) + 0.0F.ToString("p") + "\n";

                    if (exameDicionario.Id == (int)IndExameClinico.Admissional
                        || exameDicionario.Id == (int)IndExameClinico.Demissional
                        || exameDicionario.Id == (int)IndExameClinico.MudancaDeFuncao
                        || exameDicionario.Id == (int)IndExameClinico.RetornoAoTrabalho)
                        newRow["NumExamesAnoSeguinte"] = Convert.ToString(newRow["NumExamesAnoSeguinte"]) + "Indeterminado" + "\n";
                    else
                        newRow["NumExamesAnoSeguinte"] = Convert.ToString(newRow["NumExamesAnoSeguinte"]) + numTrabExpostosAtivos.ToString() + "\n";
                }

                if (examesDic.Count == 0)
                {
                    newRow["NaturezaExame"] = "-";
                    newRow["NumExamesRealizados"] = "-";
                    newRow["NumResultadosAnormais"] = "-";
                    newRow["PctResultadosAnormais"] = "-";
                    newRow["NumExamesAnoSeguinte"] = "-";
                }

                ds.Tables[0].Rows.Add(newRow);
            }
            return ds;
        }
    }
}
