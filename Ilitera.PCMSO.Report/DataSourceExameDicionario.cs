using System;
using System.Collections;
using System.Data;
using System.Text;
using Ilitera.Opsa.Data;
using Ilitera.Common;

namespace Ilitera.PCMSO.Report
{
	public class DataSourceExameDicionario: DataSourceBase
	{
		public DataSourceExameDicionario()
		{

		}

		public RptExamesDicionarios GetReportExameDicionario()
		{
			RptExamesDicionarios report = new RptExamesDicionarios();
			report.SetDataSource(GetExameDicionario());
			report.Refresh();
            SetTempoProcessamento(report);
			return report;
		}
		
		private DataTable GetDataTableExameDicionario()
		{
			DataTable table = new DataTable("Result");			
			table.Columns.Add("NomeAbreviado", Type.GetType("System.String"));
			table.Columns.Add("RequisitoLegal", Type.GetType("System.String"));
			table.Columns.Add("Tipo", Type.GetType("System.String"));
			table.Columns.Add("Toxocologia", Type.GetType("System.String"));
			table.Columns.Add("AreaMedica", Type.GetType("System.String"));
			table.Columns.Add("Sexo", Type.GetType("System.String"));
			table.Columns.Add("Periodicidade", Type.GetType("System.String"));
			table.Columns.Add("Riscos", Type.GetType("System.String"));
			table.Columns.Add("Agentes", Type.GetType("System.String"));
			table.Columns.Add("MatBiologico", Type.GetType("System.String"));
			table.Columns.Add("IndBiologico", Type.GetType("System.String"));
			table.Columns.Add("VR", Type.GetType("System.String"));
			table.Columns.Add("IBMP", Type.GetType("System.String"));
			table.Columns.Add("MetodosAnalise", Type.GetType("System.String"));
			table.Columns.Add("Valor", Type.GetType("System.String"));
			table.Columns.Add("Amostragem", Type.GetType("System.String"));
			table.Columns.Add("ConsMedicas", Type.GetType("System.String"));
			table.Columns.Add("Descricao", Type.GetType("System.String"));
			return table;
		}

		private DataSet GetExameDicionario()
		{
			DataSet ds = new DataSet();
			ds.Tables.Add(GetDataTableExameDicionario());			
			DataRow newRow;
			ArrayList listExameDicionario = new ExameDicionario().FindAll();
			listExameDicionario.Sort();
			foreach(ExameDicionario exameDicionario in listExameDicionario)
			{		
				newRow = ds.Tables[0].NewRow();
				newRow["NomeAbreviado"] = exameDicionario.Nome;
				exameDicionario.IdRequisitoLegal.Find();
				newRow["RequisitoLegal"] = exameDicionario.IdRequisitoLegal.NomeRequisito;
				newRow["Tipo"] = ExameDicionario.GetAreaMedica(exameDicionario.IndExame);
				newRow["Toxocologia"] = ExameDicionario.GetToxicologia(exameDicionario.IsToxicologico);
				newRow["AreaMedica"] = ExameDicionario.GetAreaMedica(exameDicionario.IndSaude);
				newRow["Sexo"] = ExameDicionario.GetAplicavelSexo(exameDicionario.IndSexo);
				newRow["Periodicidade"] = ExameDicionario.GetIndPeriodicidade(exameDicionario.IntervaloAposAdmissao, exameDicionario.IndPeriodicidade);
				newRow["Riscos"] = exameDicionario.GetRiscos();
				newRow["Agentes"] = exameDicionario.Agente;
				newRow["MatBiologico"] = exameDicionario.MaterialBiologico;
				newRow["IndBiologico"] = exameDicionario.IndicadorBiologico;
				newRow["VR"] = exameDicionario.VR;
				newRow["IBMP"] = exameDicionario.IBMP;
				newRow["MetodosAnalise"] = exameDicionario.Metodo;
				newRow["Valor"] = exameDicionario.Preco;
				newRow["Amostragem"] = exameDicionario.Amostragem;
				newRow["ConsMedicas"] = exameDicionario.AnaliseMedicaConsideracoes;
				newRow["Descricao"] = exameDicionario.AnaliseMadicaDescricao;
				ds.Tables[0].Rows.Add(newRow);
			}
			return ds;
		}

	}
}
