using System;
using Ilitera.Opsa.Data;
using System.Data;
using Ilitera.Common;
using System.Collections;
using System.Text;

namespace Ilitera.Sied.Report
{
	public class DataSourcePlanilhaA4PPRA : DataSourceBase
	{
        private Cliente cliente;
		private LaudoTecnico laudoTecnico;
		public bool ComGheSemEmpregado=false;
		
		public DataSourcePlanilhaA4PPRA(Cliente cliente)
		{
			this.cliente = cliente;

            if (this.cliente.mirrorOld == null)
			    this.cliente.Find();

            this.laudoTecnico = LaudoTecnico.GetUltimoLaudo(this.cliente.Id);
		}

		public DataSourcePlanilhaA4PPRA(LaudoTecnico laudoTecnico)
		{
			this.laudoTecnico = laudoTecnico;
			this.cliente = laudoTecnico.nID_EMPR;
            
            if (this.cliente.mirrorOld == null)
			    this.cliente.Find();
		}

        public DataSourcePlanilhaA4PPRA(Cliente cliente, LaudoTecnico laudoTecnico)
        {
            this.laudoTecnico = laudoTecnico;
            this.cliente = cliente;

            if (this.cliente.mirrorOld == null)
                this.cliente.Find();
        }

		public DataSourcePlanilhaA4PPRA(int IdLaudoTecnico)
		{
            this.laudoTecnico = new LaudoTecnico();
            this.laudoTecnico.Find(IdLaudoTecnico);

			this.cliente = laudoTecnico.nID_EMPR;

            if (this.cliente.mirrorOld == null)
			    this.cliente.Find();
		}	

		public RptPPRA_A4 GetReport()
		{
			RptPPRA_A4 report = new RptPPRA_A4();
			
			if(laudoTecnico==null)
				laudoTecnico = LaudoTecnico.GetUltimoLaudo(cliente.Id);

			report.SetDataSource(GetDataSource());
			report.Refresh();

            SetTempoProcessamento(report);

			return report;
		}


        public RptPPRA_A4_Prajna GetReport_Prajna()
        {
            RptPPRA_A4_Prajna report = new RptPPRA_A4_Prajna();

            if (laudoTecnico == null)
                laudoTecnico = LaudoTecnico.GetUltimoLaudo(cliente.Id);

            report.SetDataSource(GetDataSource());
            report.Refresh();

            SetTempoProcessamento(report);

            return report;
        }

        public RptPPRA_PGR GetReportPGR()
        {
            RptPPRA_PGR report = new RptPPRA_PGR();

            if (laudoTecnico == null)
                laudoTecnico = LaudoTecnico.GetUltimoLaudo(cliente.Id);

            report.SetDataSource(GetDataSource());
            report.Refresh();

            return report;
        }

        private DataSet GetDataSource()
        {
            DataSet ds = new DataSet("DataSet");
            ds.Tables.Add(GetTable());
            DataRow newRow;

            bool AnalQuantAgQuim = laudoTecnico.IsAgentesQuimicosSemAvaliacaoQuantitativa();

            bool AnalQuantAgFis = laudoTecnico.IsCalorEmOutroLocalDeDescanso();

            bool bRiscoInsignificante = false;

            string strEquipMedicao = laudoTecnico.GetEquipamentos();

            System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");


            ArrayList listPPRA2 = new PPRA().Find("nID_LAUD_TEC=" + laudoTecnico.Id + " and Risco_Insignificante = 1 ");

            foreach (PPRA ppra2 in listPPRA2)
            {
                bRiscoInsignificante = true;
            }


            ArrayList listGhe = new Ghe().Find("nID_LAUD_TEC=" + laudoTecnico.Id + " ORDER BY tNO_FUNC");

            foreach (Ghe ghe in listGhe)
            {
                int numTrabExpostos = ghe.GetNumeroEmpregadosExpostos(false);
                int numTrabExpostos_Ativos = ghe.GetNumeroEmpregadosExpostos_Ativos();

                string nomeGHE = ghe.tNO_FUNC;
                
                string descricaoFuncao = ghe.FuncoesIntegrantes();

                //ArrayList listPPRA = new PPRA().Find("nID_FUNC=" + ghe.Id + " ORDER BY nID_RSC");

                ArrayList listPPRA = new PPRA().Find("nID_FUNC=" + ghe.Id + " ORDER BY ( case when nID_RSC < 100 and nid_RSC > 2 then nID_RSC + 200 else nID_RSC end ) ");


                //Campo EPC
                StringBuilder sEPC = new StringBuilder();
                string strEpc = ghe.Epc();
                string strEpiAcientente = ghe.EpiAcidentes();

                string strCaraterAdm = ghe.GetMedidasControleAdministrativa();
                string strCaraterEdu = ghe.GetMedidasControleEducacional();

                if (strEpc.IndexOf("Inexistente") == -1)
                    sEPC.Append("EPC: \n" + strEpc + "\n");

                if (strEpiAcientente.IndexOf("Inexistente") == -1)
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
                    newRow = ds.Tables[0].NewRow();
                    newRow["CarimboCNPJ"] = cliente.GetCarimboCnpjHtml(laudoTecnico.hDT_LAUDO) + "<br>Data do Laudo:" + laudoTecnico.hDT_LAUDO.ToString("dd/MM/yyyy", ptBr); ;
                    newRow["IdGHE"] = ghe.Id;
                    newRow["IdCliente"] = cliente.Id;
                    newRow["Ghe"] = nomeGHE;
                    newRow["DescricaoFuncao"] = descricaoFuncao;
                    newRow["TrabalhadoresExpostos"] = numTrabExpostos.ToString();

                    newRow["bRiscoInsignificante"] = bRiscoInsignificante;

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
                    //}


                    newRow["Cor"] = "";

                    ppra.nID_RSC.Find();


                    ppra.nID_AG_NCV.Find();

                    if (ppra.nID_AG_NCV.Codigo_eSocial != null && ppra.nID_AG_NCV.Nome.ToUpper().Trim() == "AUSÊNCIA DE FATOR DE RISCO") //  && ppra.nID_AG_NCV.Codigo_eSocial.Trim() == "09.01.001")
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


                    newRow["EPI"] = ppra.GetEpi();
                    newRow["EPC"] = sEPC.ToString();
                    newRow["EQUIP_MEDICAO"] = strEquipMedicao;
                    newRow["LimiteUltrapassado"] = ppra.gINSALUBRE;
                    newRow["bAnalQuantAgQuimico"] = AnalQuantAgQuim;
                    newRow["bAnalQuantCalor"] = AnalQuantAgFis;

                    if (numTrabExpostos_Ativos != 0 || ComGheSemEmpregado)
                    {
                        i++;
                        ds.Tables[0].Rows.Add(newRow);
                    }
                }

                if (listPPRA.Count == 0)   // se não há riscos
                {
                                      

                    newRow = ds.Tables[0].NewRow();


                    newRow = ds.Tables[0].NewRow();
                    newRow["CarimboCNPJ"] = cliente.GetCarimboCnpjHtml(laudoTecnico.hDT_LAUDO) + "<br>Data do Laudo:" + laudoTecnico.hDT_LAUDO.ToString("dd/MM/yyyy", ptBr); ;

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

                    if (numTrabExpostos_Ativos != 0 || ComGheSemEmpregado)
                    {
                        i++;
                        ds.Tables[0].Rows.Add(newRow);
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

            return table;
        }
	}
}
