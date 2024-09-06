using System;
using System.Data;
using System.Collections.Generic;
using System.Text;

using Ilitera.Common;
using Ilitera.Opsa.Data;

namespace Ilitera.Sied.Report
{
	public class DataSourcePlanilhaA4PCMSO : DataSourceBase
	{
		private LaudoTecnico laudoTecnico;
		private Cliente cliente;
		private Pcmso pcmso;

        private Int32 zObra;

        public DataSourcePlanilhaA4PCMSO(Cliente cliente)
		{
			this.cliente = cliente;
			this.cliente.Find();
			pcmso = cliente.GetUltimoPcmso();
			laudoTecnico = new LaudoTecnico(pcmso.IdLaudoTecnico.Id);
		}
		public DataSourcePlanilhaA4PCMSO(Pcmso pcmso)
		{
			this.pcmso = pcmso;
			laudoTecnico = new LaudoTecnico(pcmso.IdLaudoTecnico.Id);
			cliente = pcmso.IdCliente;
			cliente.Find();
		}

		public DataSourcePlanilhaA4PCMSO(int IdPcmso)
		{
			pcmso = new Pcmso();
			pcmso.Find(IdPcmso);
			laudoTecnico = new LaudoTecnico(pcmso.IdLaudoTecnico.Id);
			cliente = pcmso.IdCliente;
			cliente.Find();
		}	

		public RptPCMSOPlanilhaA4 GetReport()
		{
			if(pcmso.IsFromWeb)
				throw new Exception("PCMSO de terceiros, não realizado pela Mestra!");

			RptPCMSOPlanilhaA4 report = new RptPCMSOPlanilhaA4();	
			report.SetDataSource(GetDataSource());
            report.OpenSubreport("CoordenadorPcmso").SetDataSource(pcmso.GetDSCoordenador());
			report.Refresh();

            SetTempoProcessamento(report);

			return report;
		}


        public RptPCMSOPlanilhaA4_Prajna GetReport_Prajna()
        {
            if (pcmso.IsFromWeb)
                throw new Exception("PCMSO de terceiros, não realizado pela Mestra!");

            RptPCMSOPlanilhaA4_Prajna report = new RptPCMSOPlanilhaA4_Prajna();
            report.SetDataSource(GetDataSource());
            report.OpenSubreport("CoordenadorPcmso").SetDataSource(pcmso.GetDSCoordenador());
            report.Refresh();

            SetTempoProcessamento(report);

            return report;
        }



        public RptPCMSOPlanilhaAnalitica GetReport_Analitica()
        {
            if (pcmso.IsFromWeb)
                throw new Exception("PCMSO de terceiros, não realizado pela Ilitera!");

            RptPCMSOPlanilhaAnalitica report = new RptPCMSOPlanilhaAnalitica();
            report.SetDataSource(GetDataSourceAnalitico());
            report.OpenSubreport("CoordenadorPcmso").SetDataSource(pcmso.GetDSCoordenador());
            report.OpenSubreport("Acidentes").SetDataSource(GetDataSourceAcidentes());
            report.Refresh();

            SetTempoProcessamento(report);

            return report;
        }


        private DataSet GetDataSource()
        {
            DataSet ds = new DataSet();
            ds.Tables.Add(GetTable());

            DataRow newRow;

            List<Ghe> listGhe = new Ghe().Find<Ghe>("nID_LAUD_TEC=" + laudoTecnico.Id 
                                                    + " ORDER BY tNO_FUNC");

            Endereco endereco = cliente.GetEndereco();

            string sEndereco = endereco.GetEndereco();
            string sCidadeEstado = "CEP " + endereco.Cep + "  " + endereco.GetCidade() + " " + endereco.GetEstado();
            string zExames = "";

            bool AnalQuantAgQuim = ExisteAnaliseQuantAgQuim(laudoTecnico);
            bool AnalQuantAgFis = ExisteAnaliseQuantAgFis(laudoTecnico);


            bool bRiscoInsignificante = false;

            System.Collections.ArrayList listPPRA2 = new PPRA().Find("nID_LAUD_TEC=" + laudoTecnico.Id + " and Risco_Insignificante = 1 ");

            foreach (PPRA ppra2 in listPPRA2)
            {
                bRiscoInsignificante = true;
            }


            string dataLevantamento;

            if (pcmso != null)
                dataLevantamento = "Período " + pcmso.GetPeriodo();
            else
                dataLevantamento = "ANO " + laudoTecnico.hDT_LAUDO.ToString("dd-MM-yyyy");

            foreach (Ghe ghe in listGhe)
            {
                //Alterado para Planservi
                int numTrabExpostos = ghe.GetNumeroEmpregadosExpostos(false);

                if (numTrabExpostos == 0)
                    continue;

                string nomeGHE = ghe.tNO_FUNC;

                //string descricaoFuncao = ghe.FuncoesIntegrantes();
                string descricaoFuncao = "";

                if (cliente.PGR_Apenas_Funcoes_Ativas != null && cliente.PGR_Apenas_Funcoes_Ativas == true)
                    descricaoFuncao = ghe.FuncoesIntegrantes_Ativas_Apenas();
                else
                    descricaoFuncao = ghe.FuncoesIntegrantes();



                //List<PPRA> listPPRA = new PPRA().Find<PPRA>("nID_FUNC=" + ghe.Id + " ORDER BY nID_RSC");
                List<PPRA> listPPRA;

                

                if (pcmso.Risco_Acidente_PCMSO == true)
                    listPPRA = new PPRA().Find<PPRA>("nID_FUNC=" + ghe.Id + " ORDER BY ( case when nID_RSC < 100 and nid_RSC > 2 then nID_RSC + 200 else nID_RSC end ) ");
                else
                    listPPRA = new PPRA().Find<PPRA>("nID_FUNC=" + ghe.Id + " and ( nid_rsc <> 18 or ( nid_rsc = 18 and ( tds_mat_prm like '%trabalho em altura%' or tds_mat_prm like '%o confinado%' ) ) ) ORDER BY ( case when nID_RSC < 100 and nid_RSC > 2 then nID_RSC + 200 else nID_RSC end ) ");



                int i = 1;
                
                foreach (PPRA ppra in listPPRA)
                {
                    newRow = ds.Tables[0].NewRow();

                    newRow["CarimboCNPJ"] = cliente.GetCarimboCnpjHtml(laudoTecnico.hDT_LAUDO);

                    if (pcmso.DataPcmso.Year >= 2022)
                        newRow["TituloPrincipal"] = @"""O PCMSO deve ser elaborado considerando os riscos ocupacionais identificados e classificados pelo PGR.""(NR 7, item 7.5.1). ""A organização deve garantir que o PCMSO: a) descreva os possíveis agravos à saúde relacionados aos riscos ocupacionais identificados e classificados no PGR; b) contenha planejamento de exames médicos clínicos e complementares necessários, conforme os riscos ocupacionais identificados, atendendo ao determinado nos Anexos desta NR; c) contenha os critérios de interpretação e planejamento das condutas relacionadas aos achados dos exames médicos; d) seja conhecido e atendido por todos os médicos que realizarem os exames médicos ocupacionais dos empregados (NR 7, item 7.5.4, alíneas a, b, c, d)";
                    else
                        newRow["TituloPrincipal"] = @"""Esta NR estabelece a obrigatoriedade da elaboração e implementação, por parte de todos os empregadores e instituições que admitam trabalhadores como empregados, do PCMSO, com o objetivo de promoção e preservação da saúde do conjunto dos seus trabalhadores"" (NR 7, item 7.1.1). ""O PCMSO deverá ser planejado e implantado com base nos riscos à saúde dos trabalhadores, especialmente os identificados nas avaliações previstas nas demais NRs"" (NR 7, item 7.2.4) ""Embora o programa deva ter articulação com todas as NRs, a articulação básica deve ser com o PPRA previsto na NR 9"" (Nota Técnica SSST-01/10/1996). Os exames médicos admissionais, demissionais, periódicos, mudança de função e retorno ao trabalho compreendem: ""Avaliação clínica abrangendo anamnese ocupacional, exame físico e mental e exames complementares, realizados para os trabalhadores cujas atividades envolvem os riscos discriminados nos quadros I e II desta NR.A periodicidade de avaliação dos indicadores biológicos do Quadro I deverá ser, no mínimo, semestral, podendo ser reduzida a critério do médico coordenador"" (NR 7.4.2 e 7.4.2.1). ""Devem ser submetidos a exames audiométricos de referência e seqüenciais, no mínimo, todos os trabalhadores que exerçam ou exercerão suas atividades em ambientes cujos níveis de pressão sonora (nps)ultrapassem os limites de tolerância estabelecidos nos anexos I e II - NR 15, independentemente do uso de protetor auditivo.O exame audiométrico será realizado, no mínimo, no momento da admissão, no sexto mês após a mesma, anualmente a partir de então, e na demissão"" (NR 7 - Anexo I, item 3.1 e 3.4.1).";


                    newRow["Ghe"] = nomeGHE;
                    newRow["DataLevantamento"] = dataLevantamento;
                    newRow["DescricaoFuncao"] = descricaoFuncao;
                    newRow["TrabalhadoresExpostos"] = numTrabExpostos.ToString();
                    newRow["QtdAgentes"] = listPPRA.Count;
                    newRow["RiscosAmb"] = i.ToString() + ". " + ppra.GetReconhecimentoPPRA(false);

                    if (ppra.nID_RSC.Id == (int)Riscos.RuidoContinuo)
                    {
                        newRow["RiscosAmb_Concentracao"] = ppra.GetAvaliacaoQuantitativa();
                        newRow["RiscosAmb_LimiteTol"] = ghe.RuidosLimite();
                    }
                    else if (ppra.nID_RSC.Id == (int)Riscos.Calor)
                    {
                        newRow["RiscosAmb_Concentracao"] = ghe.CalorIBUTG();
                        newRow["RiscosAmb_LimiteTol"] = ghe.CalorLimite();
                    }
                    else
                    {
                        ppra.nID_AG_NCV.Find();

                        if (ppra.nID_AG_NCV.Codigo_eSocial != null && ppra.nID_AG_NCV.Nome.ToUpper().Trim() == "AUSÊNCIA DE FATOR DE RISCO") //  && ppra.nID_AG_NCV.Codigo_eSocial.Trim() == "09.01.001")
                        {
                            newRow["RiscosAmb_Concentracao"] = "";
                            newRow["RiscosAmb_LimiteTol"] = "";
                        }
                        else
                        {
                            newRow["RiscosAmb_Concentracao"] = ppra.GetAvaliacaoQuantitativa();
                            newRow["RiscosAmb_LimiteTol"] = ppra.GetLimiteTolerancia();
                        }
                    }
                    newRow["EPI"] = string.Empty;
                    newRow["EPC"] = string.Empty;
                    newRow["LimiteUltrapassado"] = ppra.gINSALUBRE;
                    newRow["bAnalQuantAgQuimico"] = AnalQuantAgQuim;
                    newRow["bAnalQuantCalor"] = AnalQuantAgFis;

                    //zExames = ghe.GetExamesComplementares(pcmso);
                    zExames = ghe.GetExamesComplementares(pcmso);

                    zExames = zExames + System.Environment.NewLine + ghe.GetExamesComplementares_Aptidao(pcmso, zExames);


                    newRow["ExameMedico"] = zExames ;
                    newRow["bRiscoInsignificante"] = bRiscoInsignificante;


                    //if (Ilitera.Data.SQLServer.EntitySQLServer.xDB2.IndexOf("Prajna") > 0)
                    //{
                    //    if (zExames.Trim() == "Exame complementar não indicado.")
                    //        newRow["Periodicidade"] = "Anual para menores de 18 anos e maiores de 45 anos e Bienal para trabalhadores entre 18 e 45 anos de idade.";
                    //    else
                    //        newRow["Periodicidade"] = ghe.GetExamesComplPeriodicidade_Quadro(pcmso);
                    //}
                    //else
                    //    newRow["Periodicidade"] = ghe.GetExamesComplPeriodicidade(pcmso);

                    if (Ilitera.Data.SQLServer.EntitySQLServer.xDB2.IndexOf("Prajna") > 0)
                    {
                        if (zExames.Trim() == "Exame complementar não indicado.")
                        {
                            PcmsoPlanejamento xPlan = new PcmsoPlanejamento();
                            xPlan.Find(" idGHE = " + ghe.Id.ToString() + " and IdExameDicionario = 4 ");

                            if (xPlan.DependeIdade == true)
                            {
                                PcmsoPlanejamentoIdade xPlanIdade = new PcmsoPlanejamentoIdade();
                                xPlanIdade.Find(" IdPCMSOPlanejamento = " + xPlan.Id.ToString() + " and AnoTermino = 45 ");

                                if (xPlanIdade.IndPeriodicidade == (int)Periodicidade.Ano && xPlanIdade.Intervalo != 2)
                                {
                                    newRow["Periodicidade"] = ghe.GetExamesComplPeriodicidade_Quadro(pcmso).Replace("Mudança de Função", "Mudança de Risco Ocupacional");
                                }
                                else
                                {
                                    newRow["Periodicidade"] = "Anual para menores de 18 anos e maiores de 45 anos e Bienal para trabalhadores entre 18 e 45 anos de idade.";
                                }
                            }
                            else
                            {
                                //if (xPlan.IndPeriodicidade == (int)Periodicidade.Ano && xPlan.Intervalo != 2)
                                //{
                                    newRow["Periodicidade"] = ghe.GetExamesComplPeriodicidade_Quadro(pcmso).Replace("Mudança de Função", "Mudança de Risco Ocupacional");
                                //}
                                //else
                                //{
                                //    newRow["Periodicidade"] = "Anual para menores de 18 anos e maiores de 45 anos e Bienal para trabalhadores entre 18 e 45 anos de idade.";
                                //}
                            }
                        }

                        //newRow["Periodicidade"] = "Anual para menores de 18 anos e maiores de 45 anos e Bienal para trabalhadores entre 18 e 45 anos de idade.";
                        else
                        {

                            string zPeriodicidade = ghe.GetExamesComplPeriodicidade_Quadro(pcmso);

                            zPeriodicidade = zPeriodicidade + System.Environment.NewLine + ghe.GetExamesComplPeriodicidade_Aptidao(pcmso, zPeriodicidade);
                            newRow["Periodicidade"] = zPeriodicidade.Replace("Mudança de Função", "Mudança de Risco Ocupacional");

                        }
                    }
                    else
                    {
                        string zPeriodicidade = ghe.GetExamesComplPeriodicidade(pcmso);

                        zPeriodicidade = zPeriodicidade + System.Environment.NewLine + ghe.GetExamesComplPeriodicidade_Aptidao(pcmso, zPeriodicidade);
                        newRow["Periodicidade"] = zPeriodicidade.Replace("Mudança de Função", "Mudança de Risco Ocupacional");

                    }



                    i++;

                    ds.Tables[0].Rows.Add(newRow);
                }

                if (listPPRA.Count == 0)   // se não há riscos
                {


                    newRow = ds.Tables[0].NewRow();


                    newRow["CarimboCNPJ"] = cliente.GetCarimboCnpjHtml(laudoTecnico.hDT_LAUDO);


                    if (pcmso.DataPcmso.Year >= 2022)
                        newRow["TituloPrincipal"] = @"""O PCMSO deve ser elaborado considerando os riscos ocupacionais identificados e classificados pelo PGR.""(NR 7, item 7.5.1). ""A organização deve garantir que o PCMSO: a) descreva os possíveis agravos à saúde relacionados aos riscos ocupacionais identificados e classificados no PGR; b) contenha planejamento de exames médicos clínicos e complementares necessários, conforme os riscos ocupacionais identificados, atendendo ao determinado nos Anexos desta NR; c) contenha os critérios de interpretação e planejamento das condutas relacionadas aos achados dos exames médicos; d) seja conhecido e atendido por todos os médicos que realizarem os exames médicos ocupacionais dos empregados (NR 7, item 7.5.4, alíneas a, b, c, d)";
                    else
                        newRow["TituloPrincipal"] = @"""Esta NR estabelece a obrigatoriedade da elaboração e implementação, por parte de todos os empregadores e instituições que admitam trabalhadores como empregados, do PCMSO, com o objetivo de promoção e preservação da saúde do conjunto dos seus trabalhadores"" (NR 7, item 7.1.1). ""O PCMSO deverá ser planejado e implantado com base nos riscos à saúde dos trabalhadores, especialmente os identificados nas avaliações previstas nas demais NRs"" (NR 7, item 7.2.4) ""Embora o programa deva ter articulação com todas as NRs, a articulação básica deve ser com o PPRA previsto na NR 9"" (Nota Técnica SSST-01/10/1996). Os exames médicos admissionais, demissionais, periódicos, mudança de função e retorno ao trabalho compreendem: ""Avaliação clínica abrangendo anamnese ocupacional, exame físico e mental e exames complementares, realizados para os trabalhadores cujas atividades envolvem os riscos discriminados nos quadros I e II desta NR.A periodicidade de avaliação dos indicadores biológicos do Quadro I deverá ser, no mínimo, semestral, podendo ser reduzida a critério do médico coordenador"" (NR 7.4.2 e 7.4.2.1). ""Devem ser submetidos a exames audiométricos de referência e seqüenciais, no mínimo, todos os trabalhadores que exerçam ou exercerão suas atividades em ambientes cujos níveis de pressão sonora (nps)ultrapassem os limites de tolerância estabelecidos nos anexos I e II - NR 15, independentemente do uso de protetor auditivo.O exame audiométrico será realizado, no mínimo, no momento da admissão, no sexto mês após a mesma, anualmente a partir de então, e na demissão"" (NR 7 - Anexo I, item 3.1 e 3.4.1).";

                    newRow["Ghe"] = nomeGHE;
                    newRow["DataLevantamento"] = dataLevantamento;
                    newRow["DescricaoFuncao"] = descricaoFuncao;
                    newRow["TrabalhadoresExpostos"] = numTrabExpostos.ToString();
                    newRow["QtdAgentes"] = 0;
                    newRow["RiscosAmb"] = "Sem riscos ocupacionais específicos";

                    newRow["RiscosAmb_Concentracao"] = "-";
                    newRow["RiscosAmb_LimiteTol"] = "-";

                    newRow["EPI"] = string.Empty;
                    newRow["EPC"] = string.Empty;
                    newRow["LimiteUltrapassado"] = false;
                    newRow["bAnalQuantAgQuimico"] = false;
                    newRow["bAnalQuantCalor"] = false;

                    newRow["ExameMedico"] = "Exame Complementar não indicado.";
                    newRow["bRiscoInsignificante"] = false;



                    PcmsoPlanejamento xPlan = new PcmsoPlanejamento();
                    xPlan.Find(" idGHE = " + ghe.Id.ToString() + " and IdExameDicionario = 4 ");

                    if (xPlan.Id != 0)
                    {

                        if (xPlan.DependeIdade == true)
                        {
                            PcmsoPlanejamentoIdade xPlanIdade = new PcmsoPlanejamentoIdade();
                            xPlanIdade.Find(" IdPCMSOPlanejamento = " + xPlan.Id.ToString() + " and AnoTermino < 50 and AnoTermino > 20 ");

                            //if (xPlanIdade.IndPeriodicidade == (int)Periodicidade.Ano && xPlanIdade.Intervalo != 2)
                            //{
                            //newRow["Periodicidade"] = ghe.GetExamesComplPeriodicidade_Quadro(pcmso);
                            //}
                            //else
                            //{
                            if (xPlanIdade.Id == 0)
                                newRow["Periodicidade"] = "Anual para menores de 18 anos e maiores de 45 anos e Bienal para trabalhadores entre 18 e 45 anos de idade.";
                            else
                                newRow["Periodicidade"] = "Anual para menores de " + xPlanIdade.AnoInicio.ToString().Trim() + " anos e maiores de " + xPlanIdade.AnoTermino.ToString().Trim() + " anos e Bienal para trabalhadores entre " + xPlanIdade.AnoInicio.ToString().Trim() + " e " + xPlanIdade.AnoTermino.ToString().Trim() + " anos de idade.";

                            //}
                        }
                        else
                        {
                            newRow["Periodicidade"] = ghe.GetPeriodicoPeriodicidade_Quadro(pcmso).Replace("Mudança de Função", "Mudança de Risco Ocupacional");
                            //if (xPlan.IndPeriodicidade == (int)Periodicidade.Ano && xPlan.Intervalo == 2)
                            //{
                            //    newRow["Periodicidade"] = "Periodicidade Bienal.";
                            //}
                            //else
                            //{
                            //   newRow["Periodicidade"] = "Periodicidade Anual. ";
                            //}
                        }
                    }
                    else
                    {
                        newRow["Periodicidade"] = "-";
                    }


                    i++;

                    ds.Tables[0].Rows.Add(newRow);
                }
            }
            return ds;
        }

        private static DataTable GetTable()
        {
            DataTable table = new DataTable("Result");
            table.Columns.Add("CarimboCNPJ", Type.GetType("System.String"));
            table.Columns.Add("Ghe", Type.GetType("System.String"));
            table.Columns.Add("DataLevantamento", Type.GetType("System.String"));
            table.Columns.Add("DescricaoFuncao", Type.GetType("System.String"));
            table.Columns.Add("TrabalhadoresExpostos", Type.GetType("System.String"));
            table.Columns.Add("QtdAgentes", Type.GetType("System.Int32"));
            table.Columns.Add("RiscosAmb", Type.GetType("System.String"));
            table.Columns.Add("RiscosAmb_Concentracao", Type.GetType("System.String"));
            table.Columns.Add("RiscosAmb_LimiteTol", Type.GetType("System.String"));
            table.Columns.Add("EPI", Type.GetType("System.String"));
            table.Columns.Add("EPC", Type.GetType("System.String"));
            table.Columns.Add("ExameMedico", Type.GetType("System.String"));
            table.Columns.Add("Periodicidade", Type.GetType("System.String"));
            table.Columns.Add("LimiteUltrapassado", Type.GetType("System.Boolean"));
            table.Columns.Add("bAnalQuantAgQuimico", Type.GetType("System.Boolean"));
            table.Columns.Add("bAnalQuantCalor", Type.GetType("System.Boolean"));
            table.Columns.Add("bRiscoInsignificante", Type.GetType("System.Boolean"));

            table.Columns.Add("Exame", Type.GetType("System.String"));
            table.Columns.Add("Ano_Atual", Type.GetType("System.String"));
            table.Columns.Add("Ano_Anterior", Type.GetType("System.String"));
            table.Columns.Add("Exames_Realizados_Atual", Type.GetType("System.String"));
            table.Columns.Add("Exames_Realizados_Anterior", Type.GetType("System.String"));
            table.Columns.Add("Anormais_Anterior", Type.GetType("System.String"));
            table.Columns.Add("Anormais_Anterior_Porcentagem", Type.GetType("System.String"));
            table.Columns.Add("Anormais_Atual", Type.GetType("System.String"));
            table.Columns.Add("Anormais_Atual_Porcentagem", Type.GetType("System.String"));
            table.Columns.Add("Variacao_Pontos_Percentuais", Type.GetType("System.String"));
            table.Columns.Add("Variacao_Porcentagem", Type.GetType("System.String"));

            table.Columns.Add("DataEvento", Type.GetType("System.String"));
            table.Columns.Add("TipoEvento", Type.GetType("System.String"));
            table.Columns.Add("CID", Type.GetType("System.String"));

            table.Columns.Add("IdGHE", Type.GetType("System.String"));
            table.Columns.Add("nId_Func", Type.GetType("System.String"));

            table.Columns.Add("TituloPrincipal", Type.GetType("System.String"));

            table.Columns.Add("Doencas", Type.GetType("System.String"));
            table.Columns.Add("Prevalencia", Type.GetType("System.String"));
            table.Columns.Add("Incidencia", Type.GetType("System.String"));
            table.Columns.Add("Nota", Type.GetType("System.String"));
            table.Columns.Add("Notas", Type.GetType("System.String"));

            return table;
        }
		
		public bool ExisteAnaliseQuantAgQuim(LaudoTecnico laudoTecnico)
		{
            int count = new PPRA().ExecuteCount("nID_LAUD_TEC=" + laudoTecnico.Id
                                                        + " AND nID_RSC IN(9,10,11,12,13,16)"
                                                        + " AND IsNull(tVL_MED,0) = 0");
            return count > 0;
		}

		public bool ExisteAnaliseQuantAgFis(LaudoTecnico laudoTecnico)
		{
            int count = new PPRA().ExecuteCount("nID_LAUD_TEC=" + laudoTecnico.Id
                                                + " AND nID_RSC=2"
                                                + " AND bDESC = 1");
            
            return count > 0;
		}


        private DataSet GetDataSourceAnalitico()
        {
            DataSet ds = new DataSet();
            ds.Tables.Add(GetTable());

            DataRow newRow;

            List<Ghe> listGhe = new Ghe().Find<Ghe>("nID_LAUD_TEC=" + laudoTecnico.Id
                                                    + " ORDER BY tNO_FUNC");

            Endereco endereco = cliente.GetEndereco();

            string sEndereco = endereco.GetEndereco();
            string sCidadeEstado = "CEP " + endereco.Cep + "  " + endereco.GetCidade() + " " + endereco.GetEstado();





            //obter PCMSO_Anterior
            Int32 IdPcmso_Anterior = 0;

            Ilitera.Data.PPRA_EPI xDados = new Ilitera.Data.PPRA_EPI();
            IdPcmso_Anterior = xDados.Trazer_IdPCMSO_Anterior(pcmso.Id);


            string xData_Ant;
            string xData_Atual;

            string rData_Atual;

            string xAno_Atual = pcmso.DataPcmso.Year.ToString().Trim();

            xData_Ant = (pcmso.DataPcmso.Year - 1).ToString().Trim() + " a " + (pcmso.DataPcmso.Year).ToString().Trim();
            xData_Atual = (pcmso.DataPcmso.Year).ToString().Trim() + " a " + (pcmso.DataPcmso.Year + 1).ToString().Trim();

            System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");

            rData_Atual = (pcmso.DataPcmso).ToString("dd/MM/yyyy", ptBr).Trim().Replace("/", "-") + " a " + (pcmso.DataPcmso.AddYears(1)).ToString("dd/MM/yyyy", ptBr).Trim().Replace("/", "-");


            Ilitera.Data.PPRA_EPI xAnalitico = new Ilitera.Data.PPRA_EPI();
            DataSet rDs = xAnalitico.Trazer_Dados_PCMSO_Analitico(pcmso.Id, IdPcmso_Anterior);

            System.Globalization.NumberFormatInfo percentageFormat = new System.Globalization.NumberFormatInfo { PercentPositivePattern = 1, PercentNegativePattern = 1 };



            string xNotas = "";

            int xNota1 = 0;
            int xNota2 = 0;
            int xNota3 = 0;


            foreach (Ghe ghe in listGhe)
            {


                //Alterado para Planservi
                int numTrabExpostos = ghe.GetNumeroEmpregadosExpostos(false);

                if (numTrabExpostos == 0)
                    continue;

                string nomeGHE = ghe.tNO_FUNC;

                //string descricaoFuncao = ghe.FuncoesIntegrantes();
                string descricaoFuncao = "";

                if (cliente.PGR_Apenas_Funcoes_Ativas != null && cliente.PGR_Apenas_Funcoes_Ativas == true)
                    descricaoFuncao = ghe.FuncoesIntegrantes_Ativas_Apenas();
                else
                    descricaoFuncao = ghe.FuncoesIntegrantes();



                // List<PPRA> listPPRA = new PPRA().Find<PPRA>("nID_FUNC=" + ghe.Id + " ORDER BY nID_RSC");
                List<PPRA> listPPRA = new PPRA().Find<PPRA>("nID_FUNC=" + ghe.Id + " ORDER BY ( case when nID_RSC < 100 and nid_RSC > 2 then nID_RSC + 200 else nID_RSC end ) ");


                int i = 1;

                newRow = ds.Tables[0].NewRow();
                string zCarimbo;

                if (this.zObra == 0)
                {
                    zCarimbo = cliente.GetCarimboCnpjHtml(laudoTecnico.hDT_LAUDO);
                }
                else
                {
                    LaudoTecnico zLaudo = new LaudoTecnico();

                    zLaudo.Find(" nId_Empr = " + this.zObra.ToString() + " order by hDt_Laudo Desc");

                    Ilitera.Common.Juridica xJuridica = new Ilitera.Common.Juridica();
                    xJuridica.Find(this.zObra);

                    //newRow["CarimboCNPJ"] = cliente.GetCarimboCnpjHtml(zLaudo.hDT_LAUDO, xJuridica);
                    zCarimbo = cliente.GetCarimboCnpjHtml(zLaudo.hDT_LAUDO);
                }



                if (pcmso.DataPcmso.Year >= 2022)
                    newRow["TituloPrincipal"] = @"""O PCMSO deve ser elaborado considerando os riscos ocupacionais identificados e classificados pelo PGR.""(NR 7, item 7.5.1). ""A organização deve garantir que o PCMSO: a) descreva os possíveis agravos à saúde relacionados aos riscos ocupacionais identificados e classificados no PGR; b) contenha planejamento de exames médicos clínicos e complementares necessários, conforme os riscos ocupacionais identificados, atendendo ao determinado nos Anexos desta NR; c) contenha os critérios de interpretação e planejamento das condutas relacionadas aos achados dos exames médicos; d) seja conhecido e atendido por todos os médicos que realizarem os exames médicos ocupacionais dos empregados (NR 7, item 7.5.4, alíneas a, b, c, d)";
                else
                    newRow["TituloPrincipal"] = @"""Esta NR estabelece a obrigatoriedade da elaboração e implementação, por parte de todos os empregadores e instituições que admitam trabalhadores como empregados, do PCMSO, com o objetivo de promoção e preservação da saúde do conjunto dos seus trabalhadores"" (NR 7, item 7.1.1). ""O PCMSO deverá ser planejado e implantado com base nos riscos à saúde dos trabalhadores, especialmente os identificados nas avaliações previstas nas demais NRs"" (NR 7, item 7.2.4) ""Embora o programa deva ter articulação com todas as NRs, a articulação básica deve ser com o PPRA previsto na NR 9"" (Nota Técnica SSST-01/10/1996). Os exames médicos admissionais, demissionais, periódicos, mudança de função e retorno ao trabalho compreendem: ""Avaliação clínica abrangendo anamnese ocupacional, exame físico e mental e exames complementares, realizados para os trabalhadores cujas atividades envolvem os riscos discriminados nos quadros I e II desta NR.A periodicidade de avaliação dos indicadores biológicos do Quadro I deverá ser, no mínimo, semestral, podendo ser reduzida a critério do médico coordenador"" (NR 7.4.2 e 7.4.2.1). ""Devem ser submetidos a exames audiométricos de referência e seqüenciais, no mínimo, todos os trabalhadores que exerçam ou exercerão suas atividades em ambientes cujos níveis de pressão sonora (nps)ultrapassem os limites de tolerância estabelecidos nos anexos I e II - NR 15, independentemente do uso de protetor auditivo.O exame audiométrico será realizado, no mínimo, no momento da admissão, no sexto mês após a mesma, anualmente a partir de então, e na demissão"" (NR 7 - Anexo I, item 3.1 e 3.4.1).";



                newRow["Ghe"] = nomeGHE;
                newRow["IdGHE"] = ghe.Id.ToString().Trim();

                newRow["DescricaoFuncao"] = descricaoFuncao;
                newRow["TrabalhadoresExpostos"] = numTrabExpostos.ToString();


                newRow["Ano_Atual"] = xData_Atual;
                newRow["Ano_Anterior"] = xData_Ant;


                Ilitera.Data.PPRA_EPI rAnalitico = new Ilitera.Data.PPRA_EPI();
                Int32 zDoencas = xAnalitico.Trazer_Dados_PCMSO_Analitico_Doencas(pcmso.Id, ghe.Id);

                Int32 zDoencas_Prevalencia = xAnalitico.Trazer_Dados_PCMSO_Analitico_Doencas_Prevalencia(ghe.Id);


                newRow["Doencas"] = zDoencas.ToString().Trim();

                if (numTrabExpostos > 0)
                {
                    newRow["Incidencia"] = ((float)zDoencas / (float)numTrabExpostos).ToString("P2", percentageFormat);
                    newRow["Prevalencia"] = ((float)zDoencas_Prevalencia / (float)numTrabExpostos).ToString("P2", percentageFormat);
                }
                else
                {
                    newRow["Incidencia"] = "0.00%";
                    newRow["Prevalencia"] = "0.00%";
                }






                string xGHE_Exame = "";
                string xExame = "";


                int xExames_Realizados_Atual = 0;
                int xExames_Realizados_Anterior = 0;
                int xAnormais_Anterior = 0;
                int xAnormais_Atual = 0;

                float xAnormais_Anterior_Porcentagem = 0;
                float xAnormais_Atual_Porcentagem = 0;

                bool xFirst = true;
                bool xCompleto = true;


                xNotas = "";


                //procurar no DataSet o GHE com nome igual
                for (int zCont = 0; zCont < rDs.Tables[0].Rows.Count; zCont++)
                {
                    if (rDs.Tables[0].Rows[zCont]["GHE"].ToString().Trim().ToUpper() == nomeGHE.Trim().ToUpper())
                    {

                        if (xFirst == true)
                        {
                            xGHE_Exame = nomeGHE + rDs.Tables[0].Rows[zCont]["Exame"].ToString().Trim();
                            xExame = rDs.Tables[0].Rows[zCont]["Exame"].ToString().Trim();
                            xFirst = false;
                        }


                        if (nomeGHE + rDs.Tables[0].Rows[zCont]["Exame"].ToString().Trim() != xGHE_Exame)
                        {
                            newRow = ds.Tables[0].NewRow();

                            newRow["Exame"] = xExame.Replace("Mudança de Função", "Mudança de Risco");

                            newRow["CarimboCNPJ"] = zCarimbo;
                            newRow["Ghe"] = nomeGHE;

                            newRow["DescricaoFuncao"] = descricaoFuncao;
                            newRow["TrabalhadoresExpostos"] = numTrabExpostos.ToString();

                            newRow["Doencas"] = zDoencas.ToString().Trim();


                            if (numTrabExpostos > 0)
                            {
                                newRow["Incidencia"] = ((float)zDoencas / (float)numTrabExpostos).ToString("P2", percentageFormat);
                                newRow["Prevalencia"] = ((float)zDoencas_Prevalencia / (float)numTrabExpostos).ToString("P2", percentageFormat);
                            }
                            else
                            {
                                newRow["Incidencia"] = "0.00%";
                                newRow["Prevalencia"] = "0.00%";
                            }


                            newRow["Ano_Atual"] = xData_Atual;
                            newRow["Ano_Anterior"] = xData_Ant;


                            newRow["Exames_Realizados_Atual"] = xExames_Realizados_Atual.ToString();
                            newRow["Exames_Realizados_Anterior"] = xExames_Realizados_Anterior.ToString();
                            newRow["Anormais_Anterior"] = xAnormais_Anterior.ToString();
                            newRow["Anormais_Atual"] = xAnormais_Atual.ToString();




                            xAnormais_Anterior_Porcentagem = 0;

                            if (xExames_Realizados_Anterior > 0)
                            {
                                newRow["Anormais_Anterior_Porcentagem"] = ((float)xAnormais_Anterior / (float)xExames_Realizados_Anterior).ToString("P2", percentageFormat);
                                xAnormais_Anterior_Porcentagem = ((float)xAnormais_Anterior / (float)xExames_Realizados_Anterior);
                            }
                            else
                                newRow["Anormais_Anterior_Porcentagem"] = "0.00%";


                            xAnormais_Atual_Porcentagem = 0;

                            if (xExames_Realizados_Atual > 0)
                            {
                                newRow["Anormais_Atual_Porcentagem"] = ((float)xAnormais_Atual / (float)xExames_Realizados_Atual).ToString("P2", percentageFormat);
                                xAnormais_Atual_Porcentagem = ((float)xAnormais_Atual / (float)xExames_Realizados_Atual);
                            }
                            else
                                newRow["Anormais_Atual_Porcentagem"] = "0.00%";



                            newRow["Variacao_Pontos_Percentuais"] = (((xAnormais_Atual_Porcentagem) - (xAnormais_Anterior_Porcentagem)) * 100).ToString("0.00") + " pp";

                            if (xAnormais_Anterior_Porcentagem != 0)
                                newRow["Variacao_Porcentagem"] = (((xAnormais_Atual_Porcentagem) - (xAnormais_Anterior_Porcentagem)) / xAnormais_Anterior_Porcentagem).ToString("P2", percentageFormat);
                            else
                                newRow["Variacao_Porcentagem"] = "0.00%";



                            if ((((xAnormais_Atual_Porcentagem) - (xAnormais_Anterior_Porcentagem)) * 100) == 0)
                            {
                                // montar string com Notas,  quebra de linha após cada uma
                                if (xNotas.IndexOf("Nota 1") < 0)
                                {
                                    xNota1 = xNota1 + 1;
                                    xNotas = "Nota 1";
                                }
                            }
                            else
                            {
                                if (xExame.ToUpper().IndexOf("AUDIOMETRIA") >= 0)
                                {
                                    if (xNotas.IndexOf("Nota 2") < 0)
                                    {
                                        xNota2 = xNota2 + 1;
                                        xNotas = "Nota 2";
                                    }
                                }
                                else if (xExame.ToUpper().IndexOf("AUDIOMETRIA") < 0 &&
                                         xExame.ToUpper().IndexOf("PERIÓDICO") < 0 &&
                                         xExame.ToUpper().IndexOf("ADMISSIONAL") < 0 &&
                                         xExame.ToUpper().IndexOf("DEMISSIONAL") < 0 &&
                                         xExame.ToUpper().IndexOf("MUDANÇA DE FUN") < 0 &&
                                         xExame.ToUpper().IndexOf("RETORNO AO TRAB") < 0)
                                {
                                    if (xNotas.IndexOf("Nota 3") < 0)
                                    {
                                        xNota3 = xNota3 + 1;
                                        xNotas = "Nota 3";
                                    }
                                }
                            }

                            newRow["Nota"] = xNotas;

                            xCompleto = true;

                            ds.Tables[0].Rows.Add(newRow);

                            xGHE_Exame = nomeGHE + rDs.Tables[0].Rows[zCont]["Exame"].ToString().Trim();
                            xExame = rDs.Tables[0].Rows[zCont]["Exame"].ToString().Trim();

                            xExames_Realizados_Atual = 0;
                            xExames_Realizados_Anterior = 0;
                            xAnormais_Anterior = 0;
                            xAnormais_Atual = 0;
                        }


                        if (rDs.Tables[0].Rows[zCont]["Ano"].ToString().Trim() == xAno_Atual)
                        {
                            if (rDs.Tables[0].Rows[zCont]["IndResultado"].ToString().Trim() != "1")
                            {
                                xAnormais_Atual = xAnormais_Atual + System.Convert.ToInt16(rDs.Tables[0].Rows[zCont]["Numero_Exames"].ToString());
                            }

                            xExames_Realizados_Atual = xExames_Realizados_Atual + System.Convert.ToInt16(rDs.Tables[0].Rows[zCont]["Numero_Exames"].ToString());

                        }
                        else
                        {
                            if (rDs.Tables[0].Rows[zCont]["IndResultado"].ToString().Trim() != "1")
                            {
                                xAnormais_Anterior = xAnormais_Anterior + System.Convert.ToInt16(rDs.Tables[0].Rows[zCont]["Numero_Exames"].ToString());
                            }

                            xExames_Realizados_Anterior = xExames_Realizados_Anterior + System.Convert.ToInt16(rDs.Tables[0].Rows[zCont]["Numero_Exames"].ToString());
                        }

                        xCompleto = false;



                    }
                }


                // inserir última linha
                if (xCompleto == false)
                {
                    newRow = ds.Tables[0].NewRow();

                    newRow["Exame"] = xExame.Replace("Mudança de Função", "Mudança de Risco");

                    newRow["CarimboCNPJ"] = zCarimbo;
                    newRow["Ghe"] = nomeGHE;

                    newRow["DescricaoFuncao"] = descricaoFuncao;
                    newRow["TrabalhadoresExpostos"] = numTrabExpostos.ToString();

                    newRow["Doencas"] = zDoencas.ToString().Trim();


                    if (numTrabExpostos > 0)
                    {
                        newRow["Incidencia"] = ((float)zDoencas / (float)numTrabExpostos).ToString("P2", percentageFormat);
                        newRow["Prevalencia"] = ((float)zDoencas_Prevalencia / (float)numTrabExpostos).ToString("P2", percentageFormat);
                    }
                    else
                    {
                        newRow["Incidencia"] = "0.00%";
                        newRow["Prevalencia"] = "0.00%";
                    }


                    newRow["Ano_Atual"] = xData_Atual;
                    newRow["Ano_Anterior"] = xData_Ant;


                    newRow["Exames_Realizados_Atual"] = xExames_Realizados_Atual.ToString();
                    newRow["Exames_Realizados_Anterior"] = xExames_Realizados_Anterior.ToString();
                    newRow["Anormais_Anterior"] = xAnormais_Anterior.ToString();
                    newRow["Anormais_Atual"] = xAnormais_Atual.ToString();




                    xAnormais_Anterior_Porcentagem = 0;

                    if (xExames_Realizados_Anterior > 0)
                    {
                        newRow["Anormais_Anterior_Porcentagem"] = ((float)xAnormais_Anterior / (float)xExames_Realizados_Anterior).ToString("P2", percentageFormat);
                        xAnormais_Anterior_Porcentagem = ((float)xAnormais_Anterior / (float)xExames_Realizados_Anterior);
                    }
                    else
                        newRow["Anormais_Anterior_Porcentagem"] = "0.00%";


                    xAnormais_Atual_Porcentagem = 0;

                    if (xExames_Realizados_Atual > 0)
                    {
                        newRow["Anormais_Atual_Porcentagem"] = ((float)xAnormais_Atual / (float)xExames_Realizados_Atual).ToString("P2", percentageFormat);
                        xAnormais_Atual_Porcentagem = ((float)xAnormais_Atual / (float)xExames_Realizados_Atual);
                    }
                    else
                        newRow["Anormais_Atual_Porcentagem"] = "0.00%";



                    newRow["Variacao_Pontos_Percentuais"] = ((xAnormais_Atual_Porcentagem) - (xAnormais_Anterior_Porcentagem) * 100).ToString("0.00") + " pp";

                    if (xAnormais_Anterior_Porcentagem != 0)
                        newRow["Variacao_Porcentagem"] = (((xAnormais_Atual_Porcentagem) - (xAnormais_Anterior_Porcentagem)) / xAnormais_Anterior_Porcentagem).ToString("P2", percentageFormat);
                    else
                        newRow["Variacao_Porcentagem"] = "0.00%";




                    if ((((xAnormais_Atual_Porcentagem) - (xAnormais_Anterior_Porcentagem)) * 100) == 0)
                    {
                        // montar string com Notas,  quebra de linha após cada uma
                        if (xNotas.IndexOf("Nota 1") < 0)
                        {
                            xNota1 = xNota1 + 1;
                            xNotas = "Nota 1";
                        }
                    }
                    else
                    {
                        if (xExame.ToUpper().IndexOf("AUDIOMETRIA") >= 0)
                        {
                            if (xNotas.IndexOf("Nota 2") < 0)
                            {
                                xNota2 = xNota2 + 1;
                                xNotas = "Nota 2";
                            }
                        }
                        else if (xExame.ToUpper().IndexOf("AUDIOMETRIA") < 0 &&
                                 xExame.ToUpper().IndexOf("PERIÓDICO") < 0 &&
                                 xExame.ToUpper().IndexOf("ADMISSIONAL") < 0 &&
                                 xExame.ToUpper().IndexOf("DEMISSIONAL") < 0 &&
                                 xExame.ToUpper().IndexOf("MUDANÇA DE FUN") < 0 &&
                                 xExame.ToUpper().IndexOf("RETORNO AO TRAB") < 0)
                        {
                            if (xNotas.IndexOf("Nota 3") < 0)
                            {
                                xNota3 = xNota3 + 1;
                                xNotas = "Nota 3";
                            }
                        }
                    }

                    newRow["Nota"] = xNotas;

                    ds.Tables[0].Rows.Add(newRow);

                }


            }


            string rNotas = "";

            if (xNota1 == 1)
            {
                rNotas = "Nota 01: " + System.Convert.ToChar(32) + "No período de " + rData_Atual + " não houve variação no resultado apresentado." + System.Convert.ToChar(32);
            }
            else if (xNota1 > 1)
            {
                rNotas = "Nota 01: " + System.Convert.ToChar(32) + "No período de " + rData_Atual + " não houve variação nos resultados apresentados." + System.Convert.ToChar(32);
            }


            if (xNota2 == 1)
            {
                rNotas = rNotas + System.Environment.NewLine + "Nota 02: " + System.Convert.ToChar(32) + "No período de " + rData_Atual + " o resultado alterado do exame audiométrico não é sugestivo de PAINPSE Perda Auditiva Induzida por Níveis de Pressão Sonora Elevado, pois não se enquadra nos critérios definidos no item 5.2 do Anexo ll da NR 7, conforme leitura e interpretação das curvas dos audiogramas da audiometria analisada." + System.Convert.ToChar(32);
            }
            else if (xNota2 > 1)
            {
                rNotas = rNotas + System.Environment.NewLine + "Nota 02: " + System.Convert.ToChar(32) + "No período de " + rData_Atual + " os resultados alterados dos exames audiométricos não são sugestivos de PAINPSE Perda Auditiva Induzida por Níveis de Pressão Sonora Elevado, pois não se enquadram nos critérios definidos no item 5.2 do Anexo ll da NR 7, conforme leitura e interpretação das curvas dos audiogramas das audiometrias analisadas." + System.Convert.ToChar(32);
            }



            if (xNota3 == 1)
            {
                rNotas = rNotas + System.Environment.NewLine + "Nota 03: " + System.Convert.ToChar(32) + "No período de " + rData_Atual + " o resultado alterado do exame não é sugestivo de doença ocupacional, pois o indicador envolvido não se altera em função dos riscos aos quais os trabalhadores estão submetidos e que foram reconhecidos no PGR.A presente conclusão foi obtida através da análise dos PMI Prontuário Médico Individual dos trabalhadores envolvidos." + System.Convert.ToChar(32);
            }
            else if (xNota3 > 1)
            {
                rNotas = rNotas + System.Environment.NewLine + "Nota 03: " + System.Convert.ToChar(32) + "No período de " + rData_Atual + " os resultados alterados dos exames não são sugestivos de doença ocupacional, pois o indicador envolvido não se altera em função dos riscos aos quais os trabalhadores estão submetidos e que foram reconhecidos no PGR.A presente conclusão foi obtida através da análise dos PMI Prontuário Médico Individual dos trabalhadores envolvidos." + System.Convert.ToChar(32);
            }



            for (int zCont = 0; zCont < ds.Tables[0].Rows.Count; zCont++)
            {
                ds.Tables[0].Rows[zCont]["Notas"] = rNotas;
                ds.Tables[0].Rows[zCont]["Periodicidade"] = "Período " + rData_Atual;
            }

            return ds;
        }




        private DataSet GetDataSourceAcidentes()
        {

            DataRow newRow;

            DataSet ds = new DataSet();
            ds.Tables.Add(GetTable());

            DataSet rDs = new DataSet();
            Ilitera.Data.PPRA_EPI zAcid = new Ilitera.Data.PPRA_EPI();
            rDs = zAcid.Trazer_Acidentes_Analitico_PCMSO(pcmso.Id);


            for (int rCont = 0; rCont < rDs.Tables[0].Rows.Count; rCont++)
            {
                newRow = ds.Tables[0].NewRow();

                newRow["GHE"] = rDs.Tables[0].Rows[rCont]["GHE"].ToString().Trim();
                //newRow["IdGHE"] = rDs.Tables[0].Rows[rCont]["IdGHE"].ToString().Trim();
                //newRow["nId_Func"] = rDs.Tables[0].Rows[rCont]["IdGHE"].ToString().Trim();
                newRow["DataEvento"] = rDs.Tables[0].Rows[rCont]["DataEvento"].ToString().Trim();
                newRow["TipoEvento"] = rDs.Tables[0].Rows[rCont]["TipoEvento"].ToString().Trim();
                newRow["CID"] = rDs.Tables[0].Rows[rCont]["CID"].ToString().Trim();

                ds.Tables[0].Rows.Add(newRow);
            }


            return ds;

        }



    }
}
