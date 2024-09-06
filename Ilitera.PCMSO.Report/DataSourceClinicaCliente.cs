using System;
using System.Data;
using System.Collections;
using Ilitera.Opsa.Data;
using Ilitera.Common;

namespace Ilitera.PCMSO.Report
{
    public class DataSourceClinicaCliente : DataSourceBase
    {
        public DataSourceClinicaCliente()
        {

        }

        public RptClinicaCliente GetReport()
        {
            RptClinicaCliente report = new RptClinicaCliente();
            report.SetDataSource(GetClinicaCliente());
            report.Refresh();
            SetTempoProcessamento(report);
            return report;
        }

        private DataSet GetClinicaCliente()
        {
            DataSet ds = new DataSet();
            DataTable table = GetTable();
            ds.Tables.Add(table);
            DataRow newRow;

            ArrayList listClinica = new Clinica().Find("IdJuridicaPapel=" + (int)IndJuridicaPapel.Clinica
                                                    + " AND IsInativo=0"
                                                    + " ORDER BY NomeAbreviado");

            foreach (Clinica clinica in listClinica)
            {
                ArrayList listExames = new ClinicaExameDicionario().Find("IdClinica=" + clinica.Id
                                        + " AND IdExameDicionario IN (SELECT IdExameDicionario FROM ExameDicionario WHERE"
                                        +" IndExame=" + (int)IndTipoExame.Clinico + ")");

                ArrayList list = new ClinicaCliente().Find("IdClinica=" + clinica.Id
                                                        + " AND IdCliente IN (SELECT IdCliente FROM qryClienteAtivos)");

                foreach (ClinicaCliente clinicaCliente in list)
                {
                    newRow = ds.Tables[0].NewRow();

                    newRow["NomeCliente"] = clinicaCliente.IdCliente.ToString();
                    newRow["NumeroEmpregados"] = clinicaCliente.IdCliente.QtdEmpregados.ToString();
                    newRow["NomeClinica"] = clinica.NomeAbreviado;

                    foreach (ClinicaExameDicionario clinicaExameDicionario in listExames)
                    {
                        if (clinicaExameDicionario.IdExameDicionario.mirrorOld == null)
                            clinicaExameDicionario.IdExameDicionario.Find();

                        if (clinicaExameDicionario.IdExameDicionario.Id == (int)IndExameClinico.Admissional)
                            newRow["Admissional"] = clinicaExameDicionario.ValorPadrao;
                        if (clinicaExameDicionario.IdExameDicionario.Id == (int)IndExameClinico.Demissional)
                            newRow["Demissional"] = clinicaExameDicionario.ValorPadrao;
                        if (clinicaExameDicionario.IdExameDicionario.Id == (int)IndExameClinico.Periodico)
                            newRow["Periodico"] = clinicaExameDicionario.ValorPadrao;
                        if (clinicaExameDicionario.IdExameDicionario.Id == (int)IndExameClinico.RetornoAoTrabalho)
                            newRow["RetornoTrabalho"] = clinicaExameDicionario.ValorPadrao;
                        if (clinicaExameDicionario.IdExameDicionario.Id == (int)IndExameClinico.MudancaDeFuncao)
                            newRow["MudancaFuncao"] = clinicaExameDicionario.ValorPadrao;
                    }
                    ds.Tables[0].Rows.Add(newRow);
                }
            }
            return ds;
        }

        private static DataTable GetTable()
        {
            DataTable table = new DataTable("Result");
            table.Columns.Add("NomeCliente", Type.GetType("System.String"));
            table.Columns.Add("NomeClinica", Type.GetType("System.String"));
            table.Columns.Add("Admissional", Type.GetType("System.Double"));
            table.Columns.Add("Demissional", Type.GetType("System.Double"));
            table.Columns.Add("Periodico", Type.GetType("System.Double"));
            table.Columns.Add("RetornoTrabalho", Type.GetType("System.Double"));
            table.Columns.Add("MudancaFuncao", Type.GetType("System.Double"));
            table.Columns.Add("NumeroEmpregados", Type.GetType("System.String"));
            return table;
        }
    }
}
