using System;
using System.Data;
using System.Collections.Generic;
using Ilitera.Opsa.Data;
using Ilitera.Common;

namespace Ilitera.PCMSO.Report
{
    public class DataSourceClinicaExame : DataSourceBase
	{
		public DataSourceClinicaExame()
		{			

		}

		public RptClinicaExame GetReportClinicaExame(bool isFromWeb)
		{
			RptClinicaExame report = new RptClinicaExame();
			report.SetDataSource(GetClinicaExame());
			report.Refresh();
            SetTempoProcessamento(report);
			return report;
		}

		public DataSet GetClinicaExame()
		{
			DataSet ds = new DataSet();
            DataTable table = GetTable();
			ds.Tables.Add(table);
			DataRow newRow;

            string criteria = "IdJuridicaPapel =" + (int)IndJuridicaPapel.Clinica
                            + " AND IsInativo=0"
                            + " AND FazClinico = 1"
                            + " AND IsClinicaInterna = 0"
                            + " ORDER BY NomeAbreviado";

            List<Clinica> clinicas = new Clinica().Find<Clinica>(criteria);

            foreach (Clinica clinica in clinicas)
			{
				newRow = ds.Tables[0].NewRow();			
				
				newRow["NomeCinica"] = clinica.NomeAbreviado;

                string criteriaExame = "IdClinica=" + clinica.Id
                    + " AND IdExameDicionario IN (SELECT IdExameDicionario"
                    + " FROM ExameDicionario WHERE"
                    + " IndExame=" + (int)IndTipoExame.Clinico + ")";

                List<ClinicaExameDicionario> 
                    exames = new ClinicaExameDicionario().Find<ClinicaExameDicionario>(criteriaExame);
				
                foreach(ClinicaExameDicionario clinicaExameDicionario in exames)
				{
                    if (clinicaExameDicionario.IdExameDicionario.mirrorOld == null)
                        clinicaExameDicionario.IdExameDicionario.Find();

                    if (clinicaExameDicionario.IdExameDicionario.Id == (int)IndExameClinico.Admissional)
						newRow["Admissional"] = clinicaExameDicionario.ValorPadrao;
                    else if (clinicaExameDicionario.IdExameDicionario.Id == (int)IndExameClinico.Demissional)
						newRow["Demissional"]  = clinicaExameDicionario.ValorPadrao;
                    else if (clinicaExameDicionario.IdExameDicionario.Id == (int)IndExameClinico.Periodico)
						newRow["Periodico"]  = clinicaExameDicionario.ValorPadrao;
                    else if (clinicaExameDicionario.IdExameDicionario.Id == (int)IndExameClinico.RetornoAoTrabalho)
						newRow["RetornoTrabalho"]  = clinicaExameDicionario.ValorPadrao;
                    else if (clinicaExameDicionario.IdExameDicionario.Id == (int)IndExameClinico.MudancaDeFuncao)
						newRow["MudancaFuncao"]  = clinicaExameDicionario.ValorPadrao;					
				}
				ds.Tables[0].Rows.Add(newRow);
			}
			return ds;
		}

        private static DataTable GetTable()
        {
            DataTable table = new DataTable("Result");
            table.Columns.Add("NomeCinica", Type.GetType("System.String"));
            table.Columns.Add("Admissional", Type.GetType("System.Double"));
            table.Columns.Add("Demissional", Type.GetType("System.Double"));
            table.Columns.Add("Periodico", Type.GetType("System.Double"));
            table.Columns.Add("RetornoTrabalho", Type.GetType("System.Double"));
            table.Columns.Add("MudancaFuncao", Type.GetType("System.Double"));
            return table;
        }		
	}
}
