using System;
using Ilitera.Opsa.Data;
using System.Data;
using Ilitera.Common;
using System.Collections;
using System.Text;

namespace Ilitera.Relatorios.Report
{
	public class DataSourcePlanilhaA4PGR : DataSourceBase
	{
		private Cliente cliente;
		private LaudoTecnico laudoTecnico;
        private string ListaGHEs;
        private Int32 zObra;


		public bool ComGheSemEmpregado=false;
		
		public DataSourcePlanilhaA4PGR(Cliente cliente)
		{
			this.cliente = cliente;
            this.ListaGHEs = "";
            this.zObra = 0;

            if (this.cliente.mirrorOld == null)
			    this.cliente.Find();

            this.laudoTecnico = LaudoTecnico.GetUltimoLaudo(this.cliente.Id);
		}

		public DataSourcePlanilhaA4PGR(LaudoTecnico laudoTecnico)
		{
			this.laudoTecnico = laudoTecnico;
			this.cliente = laudoTecnico.nID_EMPR;
            this.ListaGHEs = "";
            this.zObra = 0;
            
            if (this.cliente.mirrorOld == null)
			    this.cliente.Find();
		}

        public DataSourcePlanilhaA4PGR(LaudoTecnico laudoTecnico, string xListaGHE, Int32 zObra)
        {
            this.laudoTecnico = laudoTecnico;
            this.cliente = laudoTecnico.nID_EMPR;
            this.ListaGHEs = xListaGHE;
            this.zObra = zObra;

            if (this.cliente.mirrorOld == null)
                this.cliente.Find();
        }

        public DataSourcePlanilhaA4PGR(Cliente cliente, LaudoTecnico laudoTecnico)
        {
            this.laudoTecnico = laudoTecnico;
            this.cliente = cliente;
            this.ListaGHEs = "";
            this.zObra = 0;

            if (this.cliente.mirrorOld == null)
                this.cliente.Find();
        }

		public DataSourcePlanilhaA4PGR(int IdLaudoTecnico)
		{
            this.laudoTecnico = new LaudoTecnico();
            this.laudoTecnico.Find(IdLaudoTecnico);
            this.ListaGHEs = ""; 
            this.zObra = 0;

			this.cliente = laudoTecnico.nID_EMPR;

            if (this.cliente.mirrorOld == null)
			    this.cliente.Find();
		}	

		public RptPGR_A4 GetReport()
		{
			RptPGR_A4 report = new RptPGR_A4();
			
			if(laudoTecnico==null)
				laudoTecnico = LaudoTecnico.GetUltimoLaudo(cliente.Id);

			report.SetDataSource(GetDataSource());
			report.Refresh();

            SetTempoProcessamento(report);

			return report;
		}


        public RptPGR_A4 GetReport_Consolidado()
        {
            RptPGR_A4 report = new RptPGR_A4();

            if (laudoTecnico == null)
                laudoTecnico = LaudoTecnico.GetUltimoLaudo(cliente.Id);

            report.SetDataSource(GetDataSource_Consolidado());
            report.Refresh();

            SetTempoProcessamento(report);

            return report;
        }

 
        private DataSet GetDataSource()
        {
            DataSet ds = new DataSet("DataSet");
            ds.Tables.Add(GetTable());
            DataRow newRow;

            bool bRiscoInsignificante = false;
            
            bool AnalQuantAgQuim = laudoTecnico.IsAgentesQuimicosSemAvaliacaoQuantitativa();

            bool AnalQuantAgFis = laudoTecnico.IsCalorEmOutroLocalDeDescanso();


            string strEquipMedicao = laudoTecnico.GetEquipamentos();


            ArrayList listPPRA2 = new PPRA().Find("nID_LAUD_TEC=" + laudoTecnico.Id + " and Risco_Insignificante = 1 ");

            foreach (PPRA ppra2 in listPPRA2)
            {
                bRiscoInsignificante = true;
            }



            ArrayList listGhe = new Ghe().Find("nID_LAUD_TEC=" + laudoTecnico.Id + " ORDER BY tNO_FUNC");

            Boolean zLoc = true;
            

            foreach (Ghe ghe in listGhe)
            {

                string strCaraterAdm_GHE = "";
                string strCaraterEdu_GHE = "";


                if (ListaGHEs.Trim() != "")
                {

                    if (ListaGHEs.IndexOf(ghe.Id.ToString().Trim()) < 0)
                    {
                        zLoc = false;
                    }
                    else
                    {
                        zLoc = true;
                    }

                }
                else
                {
                    zLoc = true;
                }


                if (zLoc == true)
                {

                    int numTrabExpostos = ghe.GetNumeroEmpregadosExpostos(false);

                    string nomeGHE = ghe.tNO_FUNC;

                    string descricaoFuncao = "";

                    if (cliente.PGR_Apenas_Funcoes_Ativas != null && cliente.PGR_Apenas_Funcoes_Ativas == true)
                        descricaoFuncao = ghe.FuncoesIntegrantes_Ativas_Apenas();
                    else
                        descricaoFuncao = ghe.FuncoesIntegrantes();


                    //if (descricaoFuncao.Trim() == "" || descricaoFuncao.Trim() == "-")
                    //{
                    if (descricaoFuncao.Trim() == "")
                    {
                        descricaoFuncao = descricaoFuncao + " - " + ghe.tDS_FUNC.ToString().Trim();
                    }
                    //}

                    //ArrayList listPPRA = new PPRA().Find("nID_FUNC=" + ghe.Id + " ORDER BY nID_RSC");

                    ArrayList listPPRA = new PPRA().Find("nID_FUNC=" + ghe.Id + " ORDER BY ( case when nID_RSC < 100 and nid_RSC > 2 then nID_RSC + 200 else nID_RSC end ) ");



                    //string strEpc = ghe.Epc();
                    //string strEpiAcientente = ghe.EpiAcidentes();

                    //string strCaraterAdm = ghe.GetMedidasControleAdministrativa();
                    //string strCaraterEdu = ghe.GetMedidasControleEducacional();

                    //if (strEpc.IndexOf("Inexistente") == -1)
                    //    sEPC.Append("EPC: \n" + strEpc + "\n");

                    //if
                    //    (strEpiAcientente.IndexOf("Inexistente") == -1)
                    //    sEPC.Append("EPI (riscos de acidentes):\n" + strEpiAcientente + "\n");

                    //if (strCaraterAdm.IndexOf("Inexistente") == -1)
                    //    sEPC.Append("\nMedidas de Caráter Administrativo:\n" + strCaraterAdm + "\n");

                    //if (strCaraterEdu.IndexOf("Inexistente") == -1)
                    //    sEPC.Append("Medidas de Caráter Educacional:\n" + strCaraterEdu + "\n");

                    //if (strEpc.IndexOf("Inexistente") != -1 && strEpiAcientente.IndexOf("Inexistente") != -1 && strCaraterAdm.IndexOf("Inexistente") != -1 && strCaraterEdu.IndexOf("Inexistente") != -1)
                    //{
                    //    //sEPC.Append("Inexistente");
                    //    sEPC.Append("Não aplicável");
                    //}

                    int i = 1;



                    foreach (PPRA ppra in listPPRA)
                    {

                        string strCaraterAdm = ppra.mDS_MED_CTL_CAR_ADM;
                        string strCaraterEdu = ppra.mDS_MED_CTL_CAR_EDC;

                        if (strCaraterAdm_GHE.ToUpper().IndexOf(strCaraterAdm.ToUpper().Trim()) < 0)
                        {
                            if (strCaraterAdm_GHE == "")
                                strCaraterAdm_GHE = strCaraterAdm;
                            else
                                strCaraterAdm_GHE = strCaraterAdm_GHE + System.Environment.NewLine + strCaraterAdm;
                        }

                        if (strCaraterEdu_GHE.ToUpper().IndexOf(strCaraterEdu.ToUpper().Trim()) < 0)
                        {
                            if (strCaraterEdu_GHE == "")
                                strCaraterEdu_GHE = strCaraterEdu;
                            else
                                strCaraterEdu_GHE = strCaraterEdu_GHE + System.Environment.NewLine + strCaraterEdu;
                        }


                    }






                    foreach (PPRA ppra in listPPRA)
                    {
                        string xTipo = "";

                        newRow = ds.Tables[0].NewRow();



                        ppra.nID_LAUD_TEC.Find();
                        DateTime zData = ppra.nID_LAUD_TEC.hDT_LAUDO;

                        //if (Ilitera.Data.SQLServer.EntitySQLServer.xDB2.IndexOf("Prajna") < 0)
                        //{
                            if (zData < new DateTime(2022, 1, 3))
                            {
                                zData = new DateTime(2022, 1, 3);
                            }
                       // }

                        if (this.zObra == 0)
                        {
                            System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");         
                            newRow["CarimboCNPJ"] = cliente.GetCarimboCnpjHtml(laudoTecnico.hDT_LAUDO) + "<br>Data do Laudo:" + zData.ToString("dd/MM/yyyy", ptBr );
                        }
                        else
                        {
                            LaudoTecnico zLaudo = new LaudoTecnico();

                            zLaudo.Find(" nId_Empr = " + this.zObra.ToString() + " order by hDt_Laudo Desc");

                            Ilitera.Common.Juridica xJuridica = new Ilitera.Common.Juridica();
                            xJuridica.Find(this.zObra);

                            System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");
                            //newRow["CarimboCNPJ"] = cliente.GetCarimboCnpjHtml(zLaudo.hDT_LAUDO, xJuridica);
                            newRow["CarimboCNPJ"] = cliente.GetCarimboCnpjHtml(zLaudo.hDT_LAUDO) + "<br>Data do Laudo:" + zData.ToString("dd/MM/yyyy", ptBr);
                        }
                                                
                        newRow["IdGHE"] = ghe.Id;
                        newRow["IdCliente"] = cliente.Id;
                        newRow["Ghe"] = nomeGHE;
                        newRow["DescricaoFuncao"] = descricaoFuncao;
                        newRow["TrabalhadoresExpostos"] = numTrabExpostos.ToString();

                        newRow["bRiscoInsignificante"] =  bRiscoInsignificante;


                        newRow["Cor"] = "";

                        ppra.nID_RSC.Find();


                        ppra.nID_AG_NCV.Find();

                        if (ppra.nID_AG_NCV.Codigo_eSocial != null && ppra.nID_AG_NCV.Nome.ToUpper().Trim() == "AUSÊNCIA DE FATOR DE RISCO" ) // && ppra.nID_AG_NCV.Codigo_eSocial.Trim() == "09.01.001")
                        {
                            newRow["Cor"] = "";
                        }
                        else
                        {

                            if (ppra.nID_RSC.IndRiscoTipo == RiscoTipo.Fisico)
                            {
                                newRow["Cor"] = "VERDE";
                            }
                            else if (ppra.nID_RSC.IndRiscoTipo == RiscoTipo.Biologico)
                            {
                                newRow["Cor"] = "MARROM";
                            }
                            else if (ppra.nID_RSC.IndRiscoTipo == RiscoTipo.Quimico)
                            {
                                newRow["Cor"] = "VERMELHO";
                            }
                            else if (ppra.nID_RSC.IndRiscoTipo == RiscoTipo.Acidentes)
                            {
                                newRow["Cor"] = "AZUL";
                            }
                            else if (ppra.nID_RSC.IndRiscoTipo == RiscoTipo.Ergonomico)
                            {
                                newRow["Cor"] = "AMARELO";
                            }
                        }

                        xTipo = ppra.nID_RSC.ToString().Trim();
                        if (xTipo == "")
                        {
                            xTipo = ppra.nID_RSC.DescricaoResumido.ToString().Trim();
                        }


                        //if (xTipo != "Acidentes")
                        newRow["RiscosAmb"] = i.ToString() + ". " + ppra.GetReconhecimentoPPRA(false);

                        //if (ppra.nID_RSC.Id == (int)Riscos.RuidoContinuo)
                        //{
                        //    newRow["RiscosAmb_Concentracao"] = ppra.GetAvaliacaoQuantitativa();
                        //    newRow["RiscosAmb_LimiteTol"] = ppra.GetLimiteTolerancia();
                        //}
                        //else if (ppra.nID_RSC.Id == (int)Riscos.Calor)
                        //{
                        //    newRow["RiscosAmb_Concentracao"] = ghe.CalorIBUTG();
                        //    newRow["RiscosAmb_LimiteTol"] = ghe.CalorLimite();
                        //}
                        //else 
                        //{
                        //if ( xTipo != "Acidentes") 

                        //{

                        ppra.nID_AG_NCV.Find();

                        if (ppra.nID_AG_NCV.Codigo_eSocial != null && ppra.nID_AG_NCV.Nome.ToUpper().Trim() == "AUSÊNCIA DE FATOR DE RISCO") //&& ppra.nID_AG_NCV.Codigo_eSocial.Trim() == "09.01.001")
                        {
                            newRow["RiscosAmb_Concentracao"] = "";
                            newRow["RiscosAmb_LimiteTol"] = "";
                        }
                        else
                        {
                            newRow["RiscosAmb_Concentracao"] = ppra.GetAvaliacaoQuantitativa();
                            newRow["RiscosAmb_LimiteTol"] = ppra.GetLimiteTolerancia();
                        }


                        //}
                        // }


                        if ( ppra.PGR_Personalizar==true)
                        {
                            newRow["Probabilidade"] = ppra.Probabilidade;
                            newRow["Severidade"] = ppra.Severidade;
                            newRow["NivelRisco"] = ppra.Nivel_Risco;
                        }
                        else
                        {
                            newRow["Probabilidade"] = ppra.Probabilidade_Aut;
                            newRow["Severidade"] = ppra.Severidade_Aut;
                            newRow["NivelRisco"] = ppra.Nivel_Risco_Aut;
                        }

                        ////Campo EPC
                        StringBuilder sEPC = new StringBuilder();

                        string strEpc = ppra.mDS_EPC_EXTE;
                        string strEpiAcientente = ghe.EpiAcidentes();

                        string strCaraterAdm = ppra.mDS_MED_CTL_CAR_ADM;
                        string strCaraterEdu = ppra.mDS_MED_CTL_CAR_EDC;

                        if (strEpc.IndexOf("Inexistente") == -1 && strEpc.Trim() != "")
                            sEPC.Append(strEpc + "\n");

                        //if  (strEpiAcientente.IndexOf("Inexistente") == -1)
                        //    sEPC.Append("EPI (riscos de acidentes):\n" + strEpiAcientente + "\n");

                        //if (strCaraterAdm.IndexOf("Inexistente") == -1)
                        //    sEPC.Append("\nMedidas de Caráter Administrativo:\n" + strCaraterAdm + "\n");

                        //if (strCaraterEdu.IndexOf("Inexistente") == -1)
                        //    sEPC.Append("Medidas de Caráter Educacional:\n" + strCaraterEdu + "\n");

                        if (strEpc.IndexOf("Inexistente") != -1 && strEpiAcientente.IndexOf("Inexistente") != -1 && strCaraterAdm.IndexOf("Inexistente") != -1 && strCaraterEdu.IndexOf("Inexistente") != -1)
                        {
                            //sEPC.Append("Inexistente");
                            sEPC.Append("Não aplicável");
                        }



                        newRow["MedidasEdc"] = strCaraterEdu_GHE;
                        newRow["MedidasAdm"] = strCaraterAdm_GHE;
                        newRow["EPC"] = sEPC.ToString();
                        newRow["EPI"] = ppra.GetEpi();
                        newRow["EPC"] = sEPC.ToString();
                        newRow["EQUIP_MEDICAO"] = strEquipMedicao;
                        newRow["LimiteUltrapassado"] = ppra.gINSALUBRE;
                        newRow["bAnalQuantAgQuimico"] = AnalQuantAgQuim;
                        newRow["bAnalQuantCalor"] = AnalQuantAgFis;
                        newRow["DanosSaude"] = ppra.tDS_DANO_REL_SAU;
                        newRow["PlanoAcao"] = ppra.Acao;


                        if (ghe.Caracterizacao_Processos != null && ghe.Caracterizacao_Processos.Trim() != "")
                        {
                            newRow["DescricaoLocalTrabalho"] = ghe.tDS_LOCAL_TRAB + System.Environment.NewLine + ghe.Caracterizacao_Processos.Trim();
                        }
                        else
                        {
                            newRow["DescricaoLocalTrabalho"] = ghe.tDS_LOCAL_TRAB;
                        }


                        newRow["FonteGeradora"] = GetFonteGeradora(ppra);

                        ppra.nID_EQP_MED.Find();
                        newRow["Metodologia"] = ppra.nID_EQP_MED.Nome;

                        if (ppra.nId_Tempo_Exposicao == 1)
                        {
                            newRow["TempoExposicao"] = "até 10% da Jornada de Trabalho";
                        }
                        else if (ppra.nId_Tempo_Exposicao == 2)
                        {
                            newRow["TempoExposicao"] = "entre 10,1% a 20% da Jornada de Trabalho";
                        }
                        else if (ppra.nId_Tempo_Exposicao == 3)
                        {
                            newRow["TempoExposicao"] = "entre 20,1% a 30% da Jornada de Trabalho";
                        }
                        else if (ppra.nId_Tempo_Exposicao == 4)
                        {
                            newRow["TempoExposicao"] = "entre 30,1% a 50% da Jornada de Trabalho";
                        }
                        else if (ppra.nId_Tempo_Exposicao == 5)
                        {
                            newRow["TempoExposicao"] = "entre 50,1% a 75% da Jornada de Trabalho";
                        }
                        else if (ppra.nId_Tempo_Exposicao == 6)
                        {
                            newRow["TempoExposicao"] = "entre 75,1% a 100% da Jornada de Trabalho";
                        }
                        else
                        {
                            newRow["TempoExposicao"] = "-";
                        }

                        if (numTrabExpostos != 0 || ComGheSemEmpregado)
                        {
                            i++;
                            ds.Tables[0].Rows.Add(newRow);
                        }

                        //para ajustar todos ghes da ambev
                        //ppra.mDS_FRS_RSC = ppra.GetReconhecimentoPPRAPadrao(false);
                        //ppra.Save();

                    }

                    if ( listPPRA.Count == 0 )   // se não há riscos
                    {

                        string xTipo = "";

                        newRow = ds.Tables[0].NewRow();

                        DateTime zData = this.laudoTecnico.hDT_LAUDO;

                        if (zData < new DateTime(2022, 1, 3))
                        {
                            zData = new DateTime(2022, 1, 3);
                        }

                        if (this.zObra == 0)
                        {
                            System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");
                            newRow["CarimboCNPJ"] = cliente.GetCarimboCnpjHtml(laudoTecnico.hDT_LAUDO) + "<br>Data do Laudo:" + zData.ToString("dd/MM/yyyy", ptBr);
                        }
                        else
                        {
                            LaudoTecnico zLaudo = new LaudoTecnico();

                            zLaudo.Find(" nId_Empr = " + this.zObra.ToString() + " order by hDt_Laudo Desc");

                            Ilitera.Common.Juridica xJuridica = new Ilitera.Common.Juridica();
                            xJuridica.Find(this.zObra);

                            System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");
                            //newRow["CarimboCNPJ"] = cliente.GetCarimboCnpjHtml(zLaudo.hDT_LAUDO, xJuridica);
                            newRow["CarimboCNPJ"] = cliente.GetCarimboCnpjHtml(zLaudo.hDT_LAUDO) + "<br>Data do Laudo:" + zData.ToString("dd/MM/yyyy", ptBr);
                        }

                        newRow["IdGHE"] = ghe.Id;
                        newRow["IdCliente"] = cliente.Id;
                        newRow["Ghe"] = nomeGHE;
                        newRow["DescricaoFuncao"] = descricaoFuncao;
                        newRow["TrabalhadoresExpostos"] = numTrabExpostos.ToString();

                        newRow["bRiscoInsignificante"] = bRiscoInsignificante;


                        newRow["Cor"] = "";



                        //if (xTipo != "Acidentes")
                        newRow["RiscosAmb"] = "Sem riscos ocupacionais específicos";


                        //{
                        newRow["RiscosAmb_Concentracao"] = "-";
                        newRow["RiscosAmb_LimiteTol"] = "-";
                        //}
                        // }
                        newRow["EPI"] = "-";
                        newRow["EPC"] = "-";
                        newRow["EQUIP_MEDICAO"] = "-";
                        newRow["LimiteUltrapassado"] = false;
                        newRow["bAnalQuantAgQuimico"] = false;
                        newRow["bAnalQuantCalor"] = false;

                        if (numTrabExpostos != 0 || ComGheSemEmpregado)
                        {
                            i++;
                            ds.Tables[0].Rows.Add(newRow);
                        }
                    }

                }
            }
             return ds;
        }

        private DataSet GetDataSource_Consolidado()
        {
            DataSet ds = new DataSet("DataSet");
            ds.Tables.Add(GetTable());
            DataRow newRow;

            bool bRiscoInsignificante = false;

            bool AnalQuantAgQuim = laudoTecnico.IsAgentesQuimicosSemAvaliacaoQuantitativa();

            bool AnalQuantAgFis = laudoTecnico.IsCalorEmOutroLocalDeDescanso();


            string strEquipMedicao = laudoTecnico.GetEquipamentos();


            ArrayList listPPRA2 = new PPRA().Find("nID_LAUD_TEC=" + laudoTecnico.Id + " and Risco_Insignificante = 1 ");

            foreach (PPRA ppra2 in listPPRA2)
            {
                bRiscoInsignificante = true;
            }


            this.laudoTecnico.nID_EMPR.Find();
            //this.laudoTecnico.nID_EMPR.IdJuridicaPai.Find();

            Ilitera.Data.PPRA_EPI rLaudos_Consolidados = new Ilitera.Data.PPRA_EPI();
            DataSet fLaudos = rLaudos_Consolidados.Trazer_Laudos_Consolidados(this.laudoTecnico.nID_EMPR.Id, this.laudoTecnico.nID_EMPR.Id);


            for (int rCont = 0; rCont < fLaudos.Tables[0].Rows.Count; rCont++)
            {

                Int32 nId_Laud_Tec = System.Convert.ToInt32( fLaudos.Tables[0].Rows[rCont][0]);

                ArrayList listGhe = new Ghe().Find("nID_LAUD_TEC=" + nId_Laud_Tec.ToString() + " ORDER BY tNO_FUNC");

                Boolean zLoc = true;


                foreach (Ghe ghe in listGhe)
                {

                    if (ListaGHEs.Trim() != "")
                    {

                        if (ListaGHEs.IndexOf(ghe.Id.ToString().Trim()) < 0)
                        {
                            zLoc = false;
                        }
                        else
                        {
                            zLoc = true;
                        }

                    }
                    else
                    {
                        zLoc = true;
                    }


                    if (zLoc == true)
                    {

                        int numTrabExpostos = ghe.GetNumeroEmpregadosExpostos(false);

                        string nomeGHE = ghe.tNO_FUNC;

                        string descricaoFuncao = ghe.FuncoesIntegrantes();

                        //if (descricaoFuncao.Trim() == "" || descricaoFuncao.Trim() == "-")
                        //{
                        if (descricaoFuncao.Trim() == "")
                        {
                            descricaoFuncao = descricaoFuncao + " - " + ghe.tDS_FUNC.ToString().Trim();
                        }
                        //}

                        //ArrayList listPPRA = new PPRA().Find("nID_FUNC=" + ghe.Id + " ORDER BY nID_RSC");

                        ArrayList listPPRA = new PPRA().Find("nID_FUNC=" + ghe.Id + " ORDER BY ( case when nID_RSC < 100 and nid_RSC > 2 then nID_RSC + 200 else nID_RSC end ) ");

                        //Campo EPC
                        StringBuilder sEPC = new StringBuilder();
                        string strEpc = ghe.Epc();
                        string strEpiAcientente = ghe.EpiAcidentes();

                        string strCaraterAdm = ghe.GetMedidasControleAdministrativa();
                        string strCaraterEdu = ghe.GetMedidasControleEducacional();

                        if (strEpc.IndexOf("Inexistente") == -1 && strEpc.Trim() != "")
                            sEPC.Append( strEpc + "\n");

                        if
                            (strEpiAcientente.IndexOf("Inexistente") == -1)
                            sEPC.Append("EPI (riscos de acidentes):\n" + strEpiAcientente + "\n");

                        if (strCaraterAdm.IndexOf("Inexistente") == -1)
                            sEPC.Append("\nMedidas de Caráter Administrativo:\n" + strCaraterAdm + "\n");

                        if (strCaraterEdu.IndexOf("Inexistente") == -1)
                            sEPC.Append("Medidas de Caráter Educacional:\n" + strCaraterEdu + "\n");

                        if (strEpc.IndexOf("Inexistente") != -1 && strEpiAcientente.IndexOf("Inexistente") != -1 && strCaraterAdm.IndexOf("Inexistente") != -1 && strCaraterEdu.IndexOf("Inexistente") != -1)
                        {
                            //sEPC.Append("Inexistente");
                            sEPC.Append("Não aplicável");
                        }

                        int i = 1;





                        foreach (PPRA ppra in listPPRA)
                        {
                            string xTipo = "";

                            newRow = ds.Tables[0].NewRow();



                            if (this.zObra == 0)
                            {
                                System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");
                                newRow["CarimboCNPJ"] = cliente.GetCarimboCnpjHtml(laudoTecnico.hDT_LAUDO) + "<br>Data do Laudo:" + laudoTecnico.hDT_LAUDO.ToString("dd/MM/yyyy", ptBr);
                            }
                            else
                            {
                                LaudoTecnico zLaudo = new LaudoTecnico();

                                zLaudo.Find(" nId_Empr = " + this.zObra.ToString() + " order by hDt_Laudo Desc");

                                Ilitera.Common.Juridica xJuridica = new Ilitera.Common.Juridica();
                                xJuridica.Find(this.zObra);

                                System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");
                                //newRow["CarimboCNPJ"] = cliente.GetCarimboCnpjHtml(zLaudo.hDT_LAUDO, xJuridica);
                                newRow["CarimboCNPJ"] = cliente.GetCarimboCnpjHtml(zLaudo.hDT_LAUDO) + "<br>Data do Laudo:" + zLaudo.hDT_LAUDO.ToString("dd/MM/yyyy", ptBr);
                            }

                            newRow["IdGHE"] = ghe.Id;
                            newRow["IdCliente"] = cliente.Id;
                            ghe.nID_LAUD_TEC.Find();
                            ghe.nID_LAUD_TEC.nID_EMPR.Find();
                            newRow["Ghe"] = ghe.nID_LAUD_TEC.nID_EMPR.NomeAbreviado.Trim() + " - " + nomeGHE;

                            newRow["DescricaoFuncao"] = descricaoFuncao;
                            newRow["TrabalhadoresExpostos"] = numTrabExpostos.ToString();

                            newRow["bRiscoInsignificante"] = bRiscoInsignificante;


                            newRow["Cor"] = "";

                            ppra.nID_RSC.Find();

                            ppra.nID_AG_NCV.Find();

                            if (ppra.nID_AG_NCV.Codigo_eSocial != null && ppra.nID_AG_NCV.Nome.ToUpper().Trim() == "AUSÊNCIA DE FATOR DE RISCO") //&& ppra.nID_AG_NCV.Codigo_eSocial.Trim() == "09.01.001")
                            {
                                newRow["Cor"] = "";
                            }
                            else
                            {
                                if (ppra.nID_RSC.IndRiscoTipo == RiscoTipo.Fisico)
                                {
                                    newRow["Cor"] = "VERDE";
                                }
                                else if (ppra.nID_RSC.IndRiscoTipo == RiscoTipo.Biologico)
                                {
                                    newRow["Cor"] = "MARROM";
                                }
                                else if (ppra.nID_RSC.IndRiscoTipo == RiscoTipo.Quimico)
                                {
                                    newRow["Cor"] = "VERMELHO";
                                }
                                else if (ppra.nID_RSC.IndRiscoTipo == RiscoTipo.Acidentes)
                                {
                                    newRow["Cor"] = "AZUL";
                                }
                                else if (ppra.nID_RSC.IndRiscoTipo == RiscoTipo.Ergonomico)
                                {
                                    newRow["Cor"] = "AMARELO";
                                }
                            }



                            xTipo = ppra.nID_RSC.ToString().Trim();
                            if (xTipo == "")
                            {
                                xTipo = ppra.nID_RSC.DescricaoResumido.ToString().Trim();
                            }


                            //if (xTipo != "Acidentes")
                            newRow["RiscosAmb"] = i.ToString() + ". " + ppra.GetReconhecimentoPPRA(false);

                            //if (ppra.nID_RSC.Id == (int)Riscos.RuidoContinuo)
                            //{
                            //    newRow["RiscosAmb_Concentracao"] = ppra.GetAvaliacaoQuantitativa();
                            //    newRow["RiscosAmb_LimiteTol"] = ppra.GetLimiteTolerancia();
                            //}
                            //else if (ppra.nID_RSC.Id == (int)Riscos.Calor)
                            //{
                            //    newRow["RiscosAmb_Concentracao"] = ghe.CalorIBUTG();
                            //    newRow["RiscosAmb_LimiteTol"] = ghe.CalorLimite();
                            //}
                            //else 
                            //{
                            //if ( xTipo != "Acidentes") 

                            //{
                            newRow["RiscosAmb_Concentracao"] = ppra.GetAvaliacaoQuantitativa();
                            newRow["RiscosAmb_LimiteTol"] = ppra.GetLimiteTolerancia();
                            //}
                            // }
                            newRow["EPI"] = ppra.GetEpi();
                            newRow["EPC"] = sEPC.ToString();
                            newRow["EQUIP_MEDICAO"] = strEquipMedicao;
                            newRow["LimiteUltrapassado"] = ppra.gINSALUBRE;
                            newRow["bAnalQuantAgQuimico"] = AnalQuantAgQuim;
                            newRow["bAnalQuantCalor"] = AnalQuantAgFis;

                            if (numTrabExpostos != 0 || ComGheSemEmpregado)
                            {
                                i++;
                                ds.Tables[0].Rows.Add(newRow);
                            }

                            //para ajustar todos ghes da ambev
                            //ppra.mDS_FRS_RSC = ppra.GetReconhecimentoPPRAPadrao(false);
                            //ppra.Save();

                        }

                        if (listPPRA.Count == 0)   // se não há riscos
                        {

                            string xTipo = "";

                            newRow = ds.Tables[0].NewRow();


                            if (this.zObra == 0)
                            {
                                System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");
                                newRow["CarimboCNPJ"] = cliente.GetCarimboCnpjHtml(laudoTecnico.hDT_LAUDO) + "<br>Data do Laudo:" + laudoTecnico.hDT_LAUDO.ToString("dd/MM/yyyy", ptBr);
                            }
                            else
                            {
                                LaudoTecnico zLaudo = new LaudoTecnico();

                                zLaudo.Find(" nId_Empr = " + this.zObra.ToString() + " order by hDt_Laudo Desc");

                                Ilitera.Common.Juridica xJuridica = new Ilitera.Common.Juridica();
                                xJuridica.Find(this.zObra);

                                System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");
                                //newRow["CarimboCNPJ"] = cliente.GetCarimboCnpjHtml(zLaudo.hDT_LAUDO, xJuridica);
                                newRow["CarimboCNPJ"] = cliente.GetCarimboCnpjHtml(zLaudo.hDT_LAUDO) + "<br>Data do Laudo:" + zLaudo.hDT_LAUDO.ToString("dd/MM/yyyy", ptBr);
                            }

                            newRow["IdGHE"] = ghe.Id;
                            newRow["IdCliente"] = cliente.Id;

                            ghe.nID_LAUD_TEC.Find();
                            ghe.nID_LAUD_TEC.nID_EMPR.Find();
                            newRow["Ghe"] = ghe.nID_LAUD_TEC.nID_EMPR.NomeAbreviado.Trim() + " - " + nomeGHE;

                            newRow["DescricaoFuncao"] = descricaoFuncao;
                            newRow["TrabalhadoresExpostos"] = numTrabExpostos.ToString();

                            newRow["bRiscoInsignificante"] = bRiscoInsignificante;


                            newRow["Cor"] = "";



                            //if (xTipo != "Acidentes")
                            newRow["RiscosAmb"] = "Sem riscos ocupacionais específicos";


                            //{
                            newRow["RiscosAmb_Concentracao"] = "-";
                            newRow["RiscosAmb_LimiteTol"] = "-";
                            //}
                            // }
                            newRow["EPI"] = "-";
                            newRow["EPC"] = "-";
                            newRow["EQUIP_MEDICAO"] = "-";
                            newRow["LimiteUltrapassado"] = false;
                            newRow["bAnalQuantAgQuimico"] = false;
                            newRow["bAnalQuantCalor"] = false;

                            if (numTrabExpostos != 0 || ComGheSemEmpregado)
                            {
                                i++;
                                ds.Tables[0].Rows.Add(newRow);
                            }
                        }

                    }
                }

            }
            return ds;
        }



        private static DataTable GetTable()
        {
            DataTable table = new DataTable("Result");
            table.Columns.Add("CarimboCNPJ", Type.GetType("System.String"));
            table.Columns.Add("IdGHE", Type.GetType("System.Int32"));
            table.Columns.Add("IdCliente", Type.GetType("System.Int32"));
            table.Columns.Add("Ghe", Type.GetType("System.String"));
            table.Columns.Add("DescricaoFuncao", Type.GetType("System.String"));
            table.Columns.Add("TrabalhadoresExpostos", Type.GetType("System.String"));
            table.Columns.Add("RiscosAmb", Type.GetType("System.String"));
            table.Columns.Add("RiscosAmb_Concentracao", Type.GetType("System.String"));
            table.Columns.Add("RiscosAmb_LimiteTol", Type.GetType("System.String"));
            table.Columns.Add("LimiteUltrapassado", Type.GetType("System.Boolean"));
            table.Columns.Add("EPI", Type.GetType("System.String"));
            table.Columns.Add("EPC", Type.GetType("System.String"));
            table.Columns.Add("EQUIP_MEDICAO", Type.GetType("System.String"));
            table.Columns.Add("bAnalQuantAgQuimico", Type.GetType("System.Boolean"));
            table.Columns.Add("bAnalQuantCalor", Type.GetType("System.Boolean"));
            table.Columns.Add("bRiscoInsignificante", Type.GetType("System.Boolean"));
            table.Columns.Add("Cor", Type.GetType("System.String"));
            table.Columns.Add("Probabilidade", Type.GetType("System.String"));
            table.Columns.Add("Severidade", Type.GetType("System.String"));
            table.Columns.Add("NivelRisco", Type.GetType("System.String"));
            table.Columns.Add("MedidasAdm", Type.GetType("System.String"));
            table.Columns.Add("MedidasEdc", Type.GetType("System.String"));
            table.Columns.Add("DanosSaude", Type.GetType("System.String"));
            table.Columns.Add("PlanoAcao", Type.GetType("System.String"));
            table.Columns.Add("TempoExposicao", Type.GetType("System.String"));
            table.Columns.Add("DescricaoLocalTrabalho", Type.GetType("System.String"));
            table.Columns.Add("FonteGeradora", Type.GetType("System.String"));
            table.Columns.Add("Metodologia", Type.GetType("System.String"));
            return table;
        }


        private string GetFonteGeradora( PPRA xPPRA)
        {
            StringBuilder str = new StringBuilder();
            if (xPPRA.tDS_FTE_GER != string.Empty)
                str.Append(xPPRA.tDS_FTE_GER.ToLower().Replace(".", "") + ".");
            else
            {
                if (xPPRA.nID_RSC.Id == (int)Riscos.RuidoContinuo)
                    str.Append(" Movimentação de máquinas, equipamentos e pessoas.");
                else if (xPPRA.nID_RSC.Id == (int)Riscos.Calor)
                    str.Append(" Irradiação solar indireta.");
            }


            return str.ToString();
        }
    }
}
