using System;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Text;
using Ilitera.Data;
using Ilitera.Opsa.Data;
using Ilitera.Common;

namespace Ilitera.PCMSO.Report
{
    public class DataSourceProntuarioClinico : DataSourceBase
	{
		private Empregado empregado;
		private Endereco enderecoEmpresa;
		private Cliente cliente;
		private Municipio municipioEmpresa;
		private EmpregadoFuncao empregadoFuncao;
		private bool hasListagemExames = false, hasExamesClinico = false, hasExamesAudiom = false, hasExamesCompl = false, hasProntuarioDigital = false;


		public DataSourceProntuarioClinico(Empregado empregado)
		{
			this.empregado = empregado;
			this.cliente = empregado.nID_EMPR;
			this.cliente.Find();
			this.enderecoEmpresa = cliente.GetEndereco();
			this.municipioEmpresa = cliente.GetMunicipio();
			this.municipioEmpresa.IdUnidadeFederativa.Find();
			this.empregadoFuncao = EmpregadoFuncao.GetEmpregadoFuncao(empregado);
		}
	
		public RptProntuarioClinico GetReport()
		{
			RptProntuarioClinico report = new RptProntuarioClinico();
			report.OpenSubreport("ListagemExames").SetDataSource(GetDSListagemExames());
			report.OpenSubreport("ExameClinico").SetDataSource(GetDSExameClinico());
			report.OpenSubreport("ExameAudiometrico").SetDataSource(GetDSExameAudiometrico());
			report.OpenSubreport("ExameComplementar").SetDataSource(GetDSExameComplementar());
			report.OpenSubreport("ListagemProntuarioDigital").SetDataSource(GetDSProntuarioDigital());
			report.SetDataSource(GetDSProntuarioClinico());
			report.Refresh();
            SetTempoProcessamento(report);
			return report;
		}

        private DataSet GetDSProntuarioDigital()
        {
            DataSet ds = new DataSet();
            DataRow row;
            DataTable table = new DataTable("Result");

            table.Columns.Add("DataProntuarioDigital", Type.GetType("System.String"));
            table.Columns.Add("Descricao", Type.GetType("System.String"));
            table.Columns.Add("CliqueAqui", Type.GetType("System.String"));
            table.Columns.Add("PathArquivo", Type.GetType("System.String"));

            ds.Tables.Add(table);

            ArrayList prontuarioDigital = new ProntuarioDigital().Find("IdEmpregado=" + empregado.Id + " ORDER BY DataProntuario DESC");

            foreach (ProntuarioDigital prontuario in prontuarioDigital)
            {
                string PathArquivo = prontuario.GetArquivoUrl(this.cliente);

                row = ds.Tables[0].NewRow();

                row["DataProntuarioDigital"] = prontuario.DataProntuario.ToString("dd-MM-yyyy");
                row["Descricao"] = prontuario.GetDescricao();
                row["CliqueAqui"] = "Clique aqui para visualizar detalhes";
                row["PathArquivo"] = PathArquivo;

                ds.Tables[0].Rows.Add(row);

                hasProntuarioDigital = true;
            }

            return ds;
        }

		private DataSet GetDSProntuarioClinico()
		{
			DataSet ds = new DataSet();
			DataRow row;
			DataTable table = new DataTable("Result");

			table.Columns.Add("NomeEmpresa", Type.GetType("System.String"));
			table.Columns.Add("EnderecoEmpresa", Type.GetType("System.String"));
			table.Columns.Add("CEPEmpresa", Type.GetType("System.String"));
			table.Columns.Add("CidadeEmpresa", Type.GetType("System.String"));
			table.Columns.Add("EstadoEmpresa", Type.GetType("System.String"));
			table.Columns.Add("CNPJ", Type.GetType("System.String"));
			table.Columns.Add("NomeEmpregado", Type.GetType("System.String"));
			table.Columns.Add("RG", Type.GetType("System.String"));
			table.Columns.Add("DataNascimento", Type.GetType("System.String"));
			table.Columns.Add("Idade", Type.GetType("System.String"));
			table.Columns.Add("Sexo", Type.GetType("System.String"));
			table.Columns.Add("Setor", Type.GetType("System.String"));
			table.Columns.Add("Funcao", Type.GetType("System.String"));
			table.Columns.Add("iFOTO", Type.GetType("System.Byte[]"));
			table.Columns.Add("ComFoto", Type.GetType("System.Boolean"));
			table.Columns.Add("ComListagemExames", Type.GetType("System.Boolean"));
			table.Columns.Add("ComExamesClinico", Type.GetType("System.Boolean"));
			table.Columns.Add("ComExamesAudiom", Type.GetType("System.Boolean"));
			table.Columns.Add("ComExamesCompl", Type.GetType("System.Boolean"));
			table.Columns.Add("ComProntuarioDigital", Type.GetType("System.Boolean"));

			ds.Tables.Add(table);
			row = ds.Tables[0].NewRow();

			row["NomeEmpresa"] = cliente.NomeCompleto;
			row["EnderecoEmpresa"] = enderecoEmpresa.GetEndereco();
			row["CEPEmpresa"] = enderecoEmpresa.Cep;
			row["CidadeEmpresa"] = municipioEmpresa.NomeCompleto;
			row["EstadoEmpresa"] = municipioEmpresa.IdUnidadeFederativa.NomeAbreviado;
			row["CNPJ"] = cliente.NomeCodigo;
			row["NomeEmpregado"] = empregado.tNO_EMPG;
			row["RG"] = empregado.tNO_IDENTIDADE;
			row["DataNascimento"] = empregado.hDT_NASC.ToString("dd-MM-yyyy");
			row["Idade"] = empregado.IdadeEmpregado().ToString();
			
            if (empregado.tSEXO == "M")
                row["Sexo"] = "Masculino";
			else if (empregado.tSEXO == "F")
				row["Sexo"] = "Feminino";
			
            row["Setor"] = empregadoFuncao.GetNomeSetor();
			row["Funcao"] = empregadoFuncao.GetNomeFuncao();

            try
            {
                string pathFoto = empregado.FotoEmpregado();

                if (pathFoto != string.Empty && System.IO.File.Exists(pathFoto))
                {
                    row["iFOTO"] = Ilitera.Common.Fotos.GetByteFoto_Uri(pathFoto);
                    row["ComFoto"] = true;
                }
                else
                    row["ComFoto"] = false;
            }
            catch
            {
                row["ComFoto"] = false;
            }

			row["ComListagemExames"] = hasListagemExames;
			row["ComExamesClinico"] = hasExamesClinico;
			row["ComExamesAudiom"] = hasExamesAudiom;
			row["ComExamesCompl"] = hasExamesCompl;
			row["ComProntuarioDigital"] = hasProntuarioDigital;

			ds.Tables[0].Rows.Add(row);

			return ds;
		}
		
		private DataSet GetDSListagemExames()
		{
			DataSet ds = new DataSet();
			DataRow row;
			DataTable table = new DataTable("Result");
			
			table.Columns.Add("DataExame", Type.GetType("System.String"));
			table.Columns.Add("DescricaoExame", Type.GetType("System.String"));
			table.Columns.Add("MedicoExame", Type.GetType("System.String"));

			ds.Tables.Add(table);

			ArrayList exame = new ExameBase().Find("IdEmpregado=" + empregado.Id + " ORDER BY DataExame DESC");

			foreach (ExameBase exameBase in exame)
			{
				exameBase.IdExameDicionario.Find();
				exameBase.IdMedico.Find();

				row = ds.Tables[0].NewRow();

				row["DataExame"] = exameBase.DataExame.ToString("dd-MM-yyyy");
				row["DescricaoExame"] = exameBase.IdExameDicionario.Nome;

                if( exameBase.IdMedico.Numero !=string.Empty)
				    row["MedicoExame"] = exameBase.IdMedico.NomeCompleto + " - " + exameBase.IdMedico.Numero;
                else
                    row["MedicoExame"] = exameBase.IdMedico.NomeCompleto;


				ds.Tables[0].Rows.Add(row);

				hasListagemExames = true;
			}

			return ds;
		}

		private DataSet GetDSExameClinico()
		{
			DataSet ds = new DataSet();
 			DataRow row;

            DataTable table = GetTableExameClinico();

			ds.Tables.Add(table);

			ArrayList clinico = new Clinico().Find("IdEmpregado=" + empregado.Id + " ORDER BY DataExame DESC");

			foreach (Clinico exameClinico in clinico)
			{
				Anamnese anamnese = new Anamnese(exameClinico);
				ExameFisico exameFisico = new ExameFisico(exameClinico);
				
                DataSet dsAdendo = new AdendoExame().Get("IdExameBase=" + exameClinico.Id + " ORDER BY Data DESC");

                if(exameClinico.IdExameDicionario.mirrorOld==null)
                    exameClinico.IdExameDicionario.Find();

				row = ds.Tables[0].NewRow();

                PopularRowExameClinico(row, exameClinico, anamnese, exameFisico, dsAdendo);

				ds.Tables[0].Rows.Add(row);

				hasExamesClinico = true;
			}

			return ds;
		}

        private static void PopularRowExameClinico(DataRow row, Clinico exameClinico, Anamnese anamnese, ExameFisico exameFisico, DataSet dsAdendo)
        {
            row["DataExame"] = exameClinico.DataExame.ToString("dd-MM-yyyy");
            row["TipoExame"] = exameClinico.IdExameDicionario.Nome;

            if (anamnese.HasQueixasAtuais == (int)StatusAnamnese.Sim)
                row["QueixasAtuais"] = "X";
            if (anamnese.HasAfastamento == (int)StatusAnamnese.Sim)
                row["AfastamentoTrabalho"] = "X";
            if (anamnese.HasAntecedentes == (int)StatusAnamnese.Sim)
                row["AntecedentesFamiliares"] = "X";
            if (anamnese.HasDoencaCronica == (int)StatusAnamnese.Sim)
                row["DoencaCronica"] = "X";
            if (anamnese.HasCirurgia == (int)StatusAnamnese.Sim)
                row["Cirurgia"] = "X";
            if (anamnese.HasTraumatismos == (int)StatusAnamnese.Sim)
                row["Trauma"] = "X";
            if (anamnese.HasDeficienciaFisica == (int)StatusAnamnese.Sim)
                row["DeficienciaFisica"] = "X";
            if (anamnese.HasMedicacoes == (int)StatusAnamnese.Sim)
                row["Medicamentos"] = "X";
            if (anamnese.HasTabagismo == (int)StatusAnamnese.Sim)
                row["Tabagista"] = "X";
            if (anamnese.HasAlcoolismo == (int)StatusAnamnese.Sim)
                row["Etilista"] = "X";
            if (exameFisico.hasPeleAnexosAlterado == (int)StatusAnamnese.Sim)
                row["PeleAnexos"] = "X";
            if (exameFisico.hasOsteoAlterado == (int)StatusAnamnese.Sim)
                row["OsteoMuscular"] = "X";
            if (exameFisico.hasCabecaAlterado == (int)StatusAnamnese.Sim)
                row["CabecaPescoco"] = "X";
            if (exameFisico.hasCoracaoAlterado == (int)StatusAnamnese.Sim)
                row["Coracao"] = "X";
            if (exameFisico.hasPulmaoAlterado == (int)StatusAnamnese.Sim)
                row["Pulmoes"] = "X";
            if (exameFisico.hasAbdomemAlterado == (int)StatusAnamnese.Sim)
                row["Abdomem"] = "X";
            if (exameFisico.hasMSAlterado == (int)StatusAnamnese.Sim)
                row["MS"] = "X";
            if (exameFisico.hasMIAlterado == (int)StatusAnamnese.Sim)
                row["MI"] = "X";

            row["PA"] = exameFisico.PressaoArterial;

            if (exameFisico.Pulso != 0)
                row["Pulso"] = exameFisico.Pulso.ToString();

            if (exameFisico.Altura != 0)
                row["Altura"] = exameFisico.Altura.ToString("F");

            if (exameFisico.Peso != 0)
                row["Peso"] = exameFisico.Peso.ToString();

            if (exameFisico.DataUltimaMenstruacao != new DateTime())
                row["DUM"] = exameFisico.DataUltimaMenstruacao.ToString("dd-MM-yyyy");

            row["ResultadoExame"] = exameClinico.GetResultadoExame();

            if (exameClinico.Prontuario != string.Empty)
                row["ObservacaoExame"] = "Observações do Exame: " + exameClinico.Prontuario;

            if (dsAdendo.Tables[0].Rows.Count > 0)
            {
                row["ObservacaoExame"] = row["ObservacaoExame"] + "\n\nAdendos ao Exame: ";

                foreach (DataRow rowAdendo in dsAdendo.Tables[0].Select())
                    row["ObservacaoExame"] = row["ObservacaoExame"] + Convert.ToDateTime(rowAdendo["Data"]).ToString("dd-MM-yyyy") + " - " + rowAdendo["Descricao"] + ". ";
            }
        }

        private static DataTable GetTableExameClinico()
        {
            DataTable table = new DataTable("Result");
            table.Columns.Add("DataExame", Type.GetType("System.String"));
            table.Columns.Add("TipoExame", Type.GetType("System.String"));
            table.Columns.Add("QueixasAtuais", Type.GetType("System.String"));
            table.Columns.Add("AfastamentoTrabalho", Type.GetType("System.String"));
            table.Columns.Add("AntecedentesFamiliares", Type.GetType("System.String"));
            table.Columns.Add("DoencaCronica", Type.GetType("System.String"));
            table.Columns.Add("Cirurgia", Type.GetType("System.String"));
            table.Columns.Add("Trauma", Type.GetType("System.String"));
            table.Columns.Add("DeficienciaFisica", Type.GetType("System.String"));
            table.Columns.Add("Medicamentos", Type.GetType("System.String"));
            table.Columns.Add("Tabagista", Type.GetType("System.String"));
            table.Columns.Add("Etilista", Type.GetType("System.String"));
            table.Columns.Add("PeleAnexos", Type.GetType("System.String"));
            table.Columns.Add("OsteoMuscular", Type.GetType("System.String"));
            table.Columns.Add("CabecaPescoco", Type.GetType("System.String"));
            table.Columns.Add("Coracao", Type.GetType("System.String"));
            table.Columns.Add("Pulmoes", Type.GetType("System.String"));
            table.Columns.Add("Abdomem", Type.GetType("System.String"));
            table.Columns.Add("MS", Type.GetType("System.String"));
            table.Columns.Add("MI", Type.GetType("System.String"));
            table.Columns.Add("PA", Type.GetType("System.String"));
            table.Columns.Add("Pulso", Type.GetType("System.String"));
            table.Columns.Add("Altura", Type.GetType("System.String"));
            table.Columns.Add("Peso", Type.GetType("System.String"));
            table.Columns.Add("DUM", Type.GetType("System.String"));
            table.Columns.Add("ResultadoExame", Type.GetType("System.String"));
            table.Columns.Add("ObservacaoExame", Type.GetType("System.String"));
            return table;
        }

		private DataSet GetDSExameAudiometrico()
		{
			DataRow row;
            DataTable table = GetTableExameAudiometrico();

			DataSet ds = new DataSet();
			ds.Tables.Add(table);

			ArrayList audiometrico = new Audiometria().Find("IdEmpregado=" + empregado.Id + " ORDER BY DataExame DESC");

			foreach (Audiometria audiometria in audiometrico)
			{
				audiometria.IdAudiometro.Find();

				AudiometriaAudiograma audiogramaOD = audiometria.GetAudiograma(Orelha.Direita);
				AudiometriaAudiograma audiogramaOE = audiometria.GetAudiograma(Orelha.Esquerda);
				
				row = ds.Tables[0].NewRow();

                PopularRowExameAudiometrico(row, audiometria, audiogramaOD, audiogramaOE);

				ds.Tables[0].Rows.Add(row);

				hasExamesAudiom = true;
			}

			return ds;
		}

        private static void PopularRowExameAudiometrico(DataRow row, Audiometria audiometria, AudiometriaAudiograma audiogramaOD, AudiometriaAudiograma audiogramaOE)
        {
            row["DataExame"] = audiometria.DataExame.ToString("dd-MM-yyyy");

            row["AAOE025"] = audiogramaOE.Aereo250;
            row["AAOD025"] = audiogramaOD.Aereo250;

            row["AAOE05"] = audiogramaOE.Aereo500;
            row["AAOD05"] = audiogramaOD.Aereo500;

            row["AAOE1"] = audiogramaOE.Aereo1000;
            row["AAOD1"] = audiogramaOD.Aereo1000;

            row["AAOE2"] = audiogramaOE.Aereo2000;
            row["AAOD2"] = audiogramaOD.Aereo2000;

            row["AAOE3"] = audiogramaOE.Aereo3000;
            row["AAOD3"] = audiogramaOD.Aereo3000;

            row["AAOE4"] = audiogramaOE.Aereo4000;
            row["AAOD4"] = audiogramaOD.Aereo4000;

            row["AAOE6"] = audiogramaOE.Aereo6000;
            row["AAOD6"] = audiogramaOD.Aereo6000;

            row["AAOE8"] = audiogramaOE.Aereo8000;
            row["AAOD8"] = audiogramaOD.Aereo8000;

            row["OAOE05"] = audiogramaOE.Osseo500;
            row["OAOD05"] = audiogramaOD.Osseo500;

            row["OAOE1"] = audiogramaOE.Osseo1000;
            row["OAOD1"] = audiogramaOD.Osseo1000;

            row["OAOE2"] = audiogramaOE.Osseo2000;
            row["OAOD2"] = audiogramaOD.Osseo2000;

            row["OAOE3"] = audiogramaOE.Osseo3000;
            row["OAOD3"] = audiogramaOD.Osseo3000;

            row["OAOE4"] = audiogramaOE.Osseo4000;
            row["OAOD4"] = audiogramaOD.Osseo4000;

            row["OAOE6"] = audiogramaOE.Osseo6000;
            row["OAOD6"] = audiogramaOD.Osseo6000;

            if (audiogramaOE.IsReferencial)
                row["ReferencialOE"] = "Sim";
            else
                row["ReferencialOE"] = "Não";

            if (audiogramaOD.IsReferencial)
                row["ReferencialOD"] = "Sim";
            else
                row["ReferencialOD"] = "Não";

            row["Tipo"] = audiometria.GetAudiometriaTipo();

            row["Audiometro"] = audiometria.IdAudiometro.Nome;

            if (audiogramaOE.IsAnormal)
                row["ResultadoOE"] = "Alterado";
            else
                row["ResultadoOE"] = "Normal";

            if (audiogramaOD.IsAnormal)
                row["ResultadoOD"] = "Alterado";
            else
                row["ResultadoOD"] = "Normal";
        }

        private static DataTable GetTableExameAudiometrico()
        {
            DataTable table = new DataTable("Result");

            table.Columns.Add("DataExame", Type.GetType("System.String"));
            table.Columns.Add("AAOE025", Type.GetType("System.String"));
            table.Columns.Add("AAOD025", Type.GetType("System.String"));
            table.Columns.Add("AAOE05", Type.GetType("System.String"));
            table.Columns.Add("AAOD05", Type.GetType("System.String"));
            table.Columns.Add("AAOE1", Type.GetType("System.String"));
            table.Columns.Add("AAOD1", Type.GetType("System.String"));
            table.Columns.Add("AAOE2", Type.GetType("System.String"));
            table.Columns.Add("AAOD2", Type.GetType("System.String"));
            table.Columns.Add("AAOE3", Type.GetType("System.String"));
            table.Columns.Add("AAOD3", Type.GetType("System.String"));
            table.Columns.Add("AAOE4", Type.GetType("System.String"));
            table.Columns.Add("AAOD4", Type.GetType("System.String"));
            table.Columns.Add("AAOE6", Type.GetType("System.String"));
            table.Columns.Add("AAOD6", Type.GetType("System.String"));
            table.Columns.Add("AAOE8", Type.GetType("System.String"));
            table.Columns.Add("AAOD8", Type.GetType("System.String"));
            table.Columns.Add("OAOE05", Type.GetType("System.String"));
            table.Columns.Add("OAOD05", Type.GetType("System.String"));
            table.Columns.Add("OAOE1", Type.GetType("System.String"));
            table.Columns.Add("OAOD1", Type.GetType("System.String"));
            table.Columns.Add("OAOE2", Type.GetType("System.String"));
            table.Columns.Add("OAOD2", Type.GetType("System.String"));
            table.Columns.Add("OAOE3", Type.GetType("System.String"));
            table.Columns.Add("OAOD3", Type.GetType("System.String"));
            table.Columns.Add("OAOE4", Type.GetType("System.String"));
            table.Columns.Add("OAOD4", Type.GetType("System.String"));
            table.Columns.Add("OAOE6", Type.GetType("System.String"));
            table.Columns.Add("OAOD6", Type.GetType("System.String"));
            table.Columns.Add("ReferencialOE", Type.GetType("System.String"));
            table.Columns.Add("ReferencialOD", Type.GetType("System.String"));
            table.Columns.Add("Tipo", Type.GetType("System.String"));
            table.Columns.Add("Audiometro", Type.GetType("System.String"));
            table.Columns.Add("ResultadoOE", Type.GetType("System.String"));
            table.Columns.Add("ResultadoOD", Type.GetType("System.String"));
            return table;
        }

		private DataSet GetDSExameComplementar()
		{
			DataRow row;
			DataSet ds = new DataSet();
			DataTable table = new DataTable("Result");
            
			table.Columns.Add("DataExame", Type.GetType("System.String"));
			table.Columns.Add("ResultadoExame", Type.GetType("System.String"));
			table.Columns.Add("ObservacaoExame", Type.GetType("System.String"));
			table.Columns.Add("TipoExame", Type.GetType("System.String"));
			
			ds.Tables.Add(table);

			ArrayList complementar = new Complementar().Find("IdEmpregado=" + empregado.Id 
				+ " ORDER BY DataExame DESC");

			foreach (Complementar exameComplementar in complementar)
			{
				exameComplementar.IdExameDicionario.Find();

				row = ds.Tables[0].NewRow();
				
				row["DataExame"]		= exameComplementar.DataExame.ToString("dd-MM-yyyy");
				row["ResultadoExame"]	= exameComplementar.GetResultadoExame();
				row["ObservacaoExame"]	= exameComplementar.Prontuario;
				row["TipoExame"]		= exameComplementar.IdExameDicionario.Nome;
				
				ds.Tables[0].Rows.Add(row);
				
				hasExamesCompl = true;
			}
			
			return ds;
		}
	}
}
