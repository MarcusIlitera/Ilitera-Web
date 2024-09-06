using System;
using Ilitera.Opsa.Data;
using System.Data;
using Ilitera.Common;
using System.Collections;
using System.Text;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;





namespace Ilitera.PCMSO.Report
{
    public class DataSourceSAE : DataSourceBase
    {

        private ExameFisicoAmbulatorial zExame = new ExameFisicoAmbulatorial();

        public DataSourceSAE( ExameFisicoAmbulatorial xExame)
        {
            zExame = xExame;
        }


        public rptSAE GetReport()
        {
            rptSAE report = new rptSAE();
            report.SetDataSource(GetIntroducaoSAE());
            //report.OpenSubreport("FotoFachada").SetDataSource(DataSourceFotoFachada());
            report.Refresh();

            SetTempoProcessamento(report);

            return report;
        }

        private DataSet GetIntroducaoSAE()
        {


            DataTable table = new DataTable("Result");

            
            table.Columns.Add("Nome", Type.GetType("System.String"));
            table.Columns.Add("GPN", Type.GetType("System.String"));
            table.Columns.Add("Data", Type.GetType("System.String"));
            table.Columns.Add("Horario", Type.GetType("System.String"));
            table.Columns.Add("Idade", Type.GetType("System.String"));
            table.Columns.Add("Fone", Type.GetType("System.String"));
            table.Columns.Add("eMail", Type.GetType("System.String"));
            table.Columns.Add("PCD_Sim", Type.GetType("System.String"));
            table.Columns.Add("PCD_Quais", Type.GetType("System.String"));

           
            table.Columns.Add("MA_01", Type.GetType("System.String"));
            table.Columns.Add("PA", Type.GetType("System.String"));
            table.Columns.Add("Temperatura", Type.GetType("System.String"));
            table.Columns.Add("FC", Type.GetType("System.String"));
            table.Columns.Add("Queixa", Type.GetType("System.String"));
            table.Columns.Add("Outras_Queixas", Type.GetType("System.String"));

            table.Columns.Add("Alergia_Sim", Type.GetType("System.String"));
            table.Columns.Add("Alergia_Nao", Type.GetType("System.String"));
            table.Columns.Add("Alergia_Quais", Type.GetType("System.String"));
            table.Columns.Add("Medicacao_Sim", Type.GetType("System.String"));
            table.Columns.Add("Medicacao_Nao", Type.GetType("System.String"));
            table.Columns.Add("Medicacao_Quais", Type.GetType("System.String"));

            table.Columns.Add("Historico_Doencas", Type.GetType("System.String"));
            table.Columns.Add("DE_01", Type.GetType("System.String"));
            table.Columns.Add("DE_02", Type.GetType("System.String"));
            table.Columns.Add("DE_03", Type.GetType("System.String"));
            table.Columns.Add("DE_04", Type.GetType("System.String"));
            table.Columns.Add("DE_05", Type.GetType("System.String"));
            table.Columns.Add("DE_06", Type.GetType("System.String"));
            table.Columns.Add("DE_07", Type.GetType("System.String"));
            table.Columns.Add("DE_08", Type.GetType("System.String"));
            table.Columns.Add("DE_09", Type.GetType("System.String"));
            table.Columns.Add("DE_10", Type.GetType("System.String"));
            table.Columns.Add("DE_11", Type.GetType("System.String"));
            table.Columns.Add("DE_12", Type.GetType("System.String"));
            table.Columns.Add("DE_13", Type.GetType("System.String"));
            table.Columns.Add("DE_14", Type.GetType("System.String"));
            table.Columns.Add("DE_15", Type.GetType("System.String"));
            table.Columns.Add("DE_16", Type.GetType("System.String"));
            table.Columns.Add("DE_17", Type.GetType("System.String"));
            table.Columns.Add("DE_18", Type.GetType("System.String"));
            table.Columns.Add("DE_19", Type.GetType("System.String"));
            table.Columns.Add("DE_20", Type.GetType("System.String"));
            table.Columns.Add("DE_21", Type.GetType("System.String"));
            table.Columns.Add("DE_22", Type.GetType("System.String"));
            table.Columns.Add("DE_23", Type.GetType("System.String"));
            table.Columns.Add("DE_Outros", Type.GetType("System.String"));

            table.Columns.Add("PA_01", Type.GetType("System.String"));
            table.Columns.Add("PA_02", Type.GetType("System.String"));
            table.Columns.Add("PA_03", Type.GetType("System.String"));
            table.Columns.Add("PA_04", Type.GetType("System.String"));
            table.Columns.Add("PA_05", Type.GetType("System.String"));
            table.Columns.Add("PA_06", Type.GetType("System.String"));
            table.Columns.Add("PA_07", Type.GetType("System.String"));
            table.Columns.Add("PA_08", Type.GetType("System.String"));
            table.Columns.Add("PA_09", Type.GetType("System.String"));
            table.Columns.Add("PA_10", Type.GetType("System.String"));
            table.Columns.Add("PA_11", Type.GetType("System.String"));
            table.Columns.Add("PA_Outros", Type.GetType("System.String"));


            table.Columns.Add("Implementacao", Type.GetType("System.String"));
            table.Columns.Add("Avaliacao", Type.GetType("System.String"));
            table.Columns.Add("MA_02", Type.GetType("System.String"));
            table.Columns.Add("MA_03", Type.GetType("System.String"));
            table.Columns.Add("PCD_Nao", Type.GetType("System.String"));

            DataSet ds = new DataSet();

            ds.Tables.Add(table);

            DataRow newRow = ds.Tables[0].NewRow();


            //imagem  left 390  top 169

            zExame.IdEmpregado.Find();
            newRow["Nome"] = zExame.IdEmpregado.tNO_EMPG;

            newRow["Idade"] = zExame.IdEmpregado.IdadeEmpregado();
            newRow["eMail"] = zExame.IdEmpregado.teMail;
            newRow["Fone"] = zExame.IdEmpregado.tTELEFONE;


            if (zExame.IdEmpregado.nIND_BENEFICIARIO.ToString().Trim() == "1" || zExame.IdEmpregado.nIND_BENEFICIARIO.ToString().Trim() == "2")
            {
                newRow["PCD_Sim"] = "█";  //chr 219   height 150   width 120
                newRow["PCD_Nao"] = "";

                if (zExame.IdEmpregado.nIND_BENEFICIARIO.ToString().Trim() == "1")
                    newRow["PCD_Quais"] = "Beneficiário Reabilitado";
                else
                    newRow["PCD_Quais"] = "Pessoa com deficiência";

            }
            else
            {
                newRow["PCD_Nao"] = "█";  //chr 219   height 150   width 120
                newRow["PCD_Sim"] = "";
            }

            System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");
            newRow["Data"] = zExame.DataExame.ToString("dd/MM/yyyy", ptBr);


            if (zExame.Sistolica.ToString().Trim() != "" && zExame.Sistolica.ToString().Trim() != "0")
                newRow["PA"] = zExame.Sistolica.ToString().Trim() + " x " + zExame.Diastolica.ToString().Trim(); //zExame.PressaoArterial;
            else
                newRow["PA"] = "";

            newRow["Temperatura"] = zExame.Temperatura;
            newRow["FC"] = zExame.Pulso;

            newRow["Queixa"] = zExame.Queixa_Principal;
            newRow["Outras_Queixas"] = zExame.Outras_Queixas;

            newRow["Historico_Doencas"] = zExame.Historico_Doencas;

            newRow["Implementacao"] = zExame.Implementacao;
            newRow["Avaliacao"] = zExame.Avaliacao_Enfermagem;

            newRow["DE_Outros"] = zExame.Diagnostico_Enfermagem_Outros;
            newRow["PA_Outros"] = zExame.Planejamento_Enfermagem_Outros;


            if ( zExame.Alergia_Medicacao.Trim() == "S")
            {
                newRow["Alergia_Sim"] = "█";  //chr 219   height 150   width 120
                newRow["Alergia_Nao"] = "";
            }
            else
            {
                newRow["Alergia_Nao"] = "█"; 
                newRow["Alergia_Sim"] = "";
            }

            newRow["Alergia_Quais"] = zExame.Alergia_Medicacao_Quais;


            if (zExame.Medicacao_Continua.Trim() == "S")
            {
                newRow["Medicacao_Sim"] = "█";  //chr 219   height 150   width 120
                newRow["Medicacao_Nao"] = "";
            }
            else
            {
                newRow["Medicacao_Nao"] = "█";
                newRow["Medicacao_Sim"] = "";
            }

            newRow["Medicacao_Quais"] = zExame.Medicacao_Continua_Quais;




            if (zExame.Motivo_Atendimento.Trim() == "T" )
                newRow["MA_01"] = "█";
            else if (zExame.Motivo_Atendimento.Trim() == "A")
                newRow["MA_02"] = "█";
            else if (zExame.Motivo_Atendimento.Trim() == "E")
                newRow["MA_03"] = "█";
            else
                newRow["MA_02"] = "█";

            //<asp:ListItem Value="AS">Ansiedade</asp:ListItem>
            //              <asp:ListItem Value="CA">Confusão Aguda</asp:ListItem>
            //              <asp:ListItem Value="DC">Débito Cardíaco Dimínuido</asp:ListItem>
            //              <asp:ListItem Value="DA">Dor Aguda</asp:ListItem>
            //              <asp:ListItem Value="FA">Fadiga</asp:ListItem>
            //              <asp:ListItem Value="HI">Hipotermia</asp:ListItem>
            //              <asp:ListItem Value="MI">Manutenção Ineficaz da Saúde</asp:ListItem>
            //              <asp:ListItem Value="MF">Mobilidade Física Prejudicada</asp:ListItem>
            //              <asp:ListItem Value="NS">Náusea</asp:ListItem>
            //              <asp:ListItem Value="PR">Padrão Respiratório Ineficaz</asp:ListItem>
            //              <asp:ListItem Value="RI">Risco de Infecção</asp:ListItem>
            //              <asp:ListItem Value="CE">Controle Eficaz do Regime Terapêutico</asp:ListItem>
            //              <asp:ListItem Value="CS">Comportamento de saúde propenso a risco</asp:ListItem>
            //              <asp:ListItem Value="DP">Deambulação Prejudicada</asp:ListItem>
            //              <asp:ListItem Value="DR">Diarréia</asp:ListItem>
            //              <asp:ListItem Value="EV">Estilo de Vida Sedentário</asp:ListItem>
            //              <asp:ListItem Value="HP">Hipertermia</asp:ListItem>
            //              <asp:ListItem Value="IP">Integridade da Pele Prejudicada</asp:ListItem>
            //              <asp:ListItem Value="MD">Medo</asp:ListItem>
            //              <asp:ListItem Value="ND">Nutrição desiquilibrada</asp:ListItem>
            //              <asp:ListItem Value="PS">Padrão de Sono Prejudicado</asp:ListItem>
            //              <asp:ListItem Value="RG">Risco de Glicemia Instável</asp:ListItem>
            //              <asp:ListItem Value="SE">Sobrecarga do Estresse</asp:ListItem>

            if (zExame.Diagnostico_Enfermagem.IndexOf("AS") >= 0)
               newRow["DE_01"] = "█";
            
            if (zExame.Diagnostico_Enfermagem.IndexOf("CA") >= 0)
                newRow["DE_02"] = "█";

            if (zExame.Diagnostico_Enfermagem.IndexOf("DC") >= 0)
                newRow["DE_03"] = "█";

            if (zExame.Diagnostico_Enfermagem.IndexOf("DA") >= 0)
                newRow["DE_04"] = "█";

            if (zExame.Diagnostico_Enfermagem.IndexOf("FA") >= 0)
                newRow["DE_05"] = "█";

            if (zExame.Diagnostico_Enfermagem.IndexOf("HI") >= 0)
                newRow["DE_06"] = "█";

            if (zExame.Diagnostico_Enfermagem.IndexOf("MI") >= 0)
                newRow["DE_07"] = "█";

            if (zExame.Diagnostico_Enfermagem.IndexOf("MF") >= 0)
                newRow["DE_08"] = "█";

            if (zExame.Diagnostico_Enfermagem.IndexOf("NS") >= 0)
                newRow["DE_09"] = "█";

            if (zExame.Diagnostico_Enfermagem.IndexOf("PR") >= 0)
                newRow["DE_10"] = "█";

            if (zExame.Diagnostico_Enfermagem.IndexOf("RI") >= 0)
                newRow["DE_11"] = "█";

            if (zExame.Diagnostico_Enfermagem.IndexOf("CE") >= 0)
                newRow["DE_12"] = "█";

            if (zExame.Diagnostico_Enfermagem.IndexOf("CS") >= 0)
                newRow["DE_13"] = "█";

            if (zExame.Diagnostico_Enfermagem.IndexOf("DP") >= 0)
                newRow["DE_14"] = "█";

            if (zExame.Diagnostico_Enfermagem.IndexOf("DR") >= 0)
                newRow["DE_15"] = "█";

            if (zExame.Diagnostico_Enfermagem.IndexOf("EV") >= 0)
                newRow["DE_16"] = "█";

            if (zExame.Diagnostico_Enfermagem.IndexOf("HP") >= 0)
                newRow["DE_17"] = "█";

            if (zExame.Diagnostico_Enfermagem.IndexOf("IP") >= 0)
                newRow["DE_18"] = "█";

            if (zExame.Diagnostico_Enfermagem.IndexOf("MD") >= 0)
                newRow["DE_19"] = "█";

            if (zExame.Diagnostico_Enfermagem.IndexOf("ND") >= 0)
                newRow["DE_20"] = "█";

            if (zExame.Diagnostico_Enfermagem.IndexOf("PS") >= 0)
                newRow["DE_21"] = "█";

            if (zExame.Diagnostico_Enfermagem.IndexOf("RG") >= 0)
                newRow["DE_22"] = "█";

            if (zExame.Diagnostico_Enfermagem.IndexOf("SE") >= 0)
                newRow["DE_23"] = "█";



              //<asp:ListItem Value="EA">Encaminhar para avaliação médica</asp:ListItem>
              //              <asp:ListItem Value="OR">Orientar/acompanhar repouso em leito de observação</asp:ListItem>
              //              <asp:ListItem Value="OP">Oferecer apoio psicológico</asp:ListItem>
              //              <asp:ListItem Value="RC">Realizar curativo oclusivo na região</asp:ListItem>
              //              <asp:ListItem Value="RT">Realizar técnica asséptica IM/EV</asp:ListItem>
              //              <asp:ListItem Value="OE">Observar Edema</asp:ListItem>
              //              <asp:ListItem Value="ED">Esclarecer dúvidas do cliente em relação a queixa</asp:ListItem>
              //              <asp:ListItem Value="PA">Proporcionar ambiente calmo e confortável</asp:ListItem>
              //              <asp:ListItem Value="RL">Reduzir ou evitar ambientes com excesso de ruído ou iluminação</asp:ListItem>
              //              <asp:ListItem Value="OD">Orientar dieta fracionada e/ou hipossódica</asp:ListItem>
              //              <asp:ListItem Value="EF">Estimar atividade física</asp:ListItem>

            if (zExame.Planejamento_Enfermagem.IndexOf("EA") >= 0)
                newRow["PA_01"] = "█";

            if (zExame.Planejamento_Enfermagem.IndexOf("OR") >= 0)
                newRow["PA_02"] = "█";

            if (zExame.Planejamento_Enfermagem.IndexOf("OP") >= 0)
                newRow["PA_03"] = "█";

            if (zExame.Planejamento_Enfermagem.IndexOf("RC") >= 0)
                newRow["PA_04"] = "█";

            if (zExame.Planejamento_Enfermagem.IndexOf("RT") >= 0)
                newRow["PA_05"] = "█";

            if (zExame.Planejamento_Enfermagem.IndexOf("OE") >= 0)
                newRow["PA_06"] = "█";

            if (zExame.Planejamento_Enfermagem.IndexOf("ED") >= 0)
                newRow["PA_07"] = "█";

            if (zExame.Planejamento_Enfermagem.IndexOf("PA") >= 0)
                newRow["PA_08"] = "█";

            if (zExame.Planejamento_Enfermagem.IndexOf("RL") >= 0)
                newRow["PA_09"] = "█";

            if (zExame.Planejamento_Enfermagem.IndexOf("OD") >= 0)
                newRow["PA_10"] = "█";

            if (zExame.Planejamento_Enfermagem.IndexOf("EF") >= 0)
                newRow["PA_11"] = "█";






            ds.Tables[0].Rows.Add(newRow);

            return ds;
        }



        private static string GetExamesComplementares_Preparo(string zTipo, Empregado empregado, Clinica zClinica, string zExames_Guia)
        {
            List<Empregado.ExameComplementar> examesComplementares = new List<Empregado.ExameComplementar>();

            switch (zTipo)
            {
                case "A":
                    examesComplementares = empregado.ListExamesComplementaresIndicadosNaAdmissao();
                    break;
                case "D":
                    examesComplementares = empregado.ListExamesComplementaresIndicadosNaDemissao();
                    break;
                case "M":
                    examesComplementares = empregado.ListExamesComplementaresIndicadosNaMudancaFuncao();
                    break;
                case "P":
                    examesComplementares = empregado.ListExamesComplementaresIndicadosNoPeriodico();
                    break;
                case "R":
                    examesComplementares = empregado.ListExamesComplementaresIndicadosNoRetornoTrabalho();
                    break;
            }

            StringBuilder str = new StringBuilder();

            foreach (Empregado.ExameComplementar exameComplementar in examesComplementares)
            {

                ClinicaExameDicionario zClinicaExameDic = new ClinicaExameDicionario();
                zClinicaExameDic.Find(" IdClinica = " + zClinica.Id + " and IdExameDicionario = " + exameComplementar.IdExameDicionario);

                if (zExames_Guia.ToUpper().IndexOf(zClinicaExameDic.ToString().ToUpper().Trim()) >= 0)
                {
                    if (zClinicaExameDic.Preparo != null)
                    {

                        if (zClinicaExameDic.Preparo.ToString().Trim() != "")
                        {
                            str.Append(System.Environment.NewLine + exameComplementar.NomeExame + ": " + System.Environment.NewLine + zClinicaExameDic.Preparo + System.Environment.NewLine);

                        }
                    }
                }
            }

            if (str.Length > 0)
                str.Remove(str.Length - 2, 2);

            //Clinico exameClinico = new Clinico(exame.Id);

            //if (exameClinico.DescExameCompl)
            //    str.Append(" (Todos os exames complementares indicados foram desconsiderados pela empresa)");

            return str.ToString();
        }


    }
}
