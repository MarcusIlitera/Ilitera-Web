using System;
using Ilitera.Opsa.Data;
using System.Data;
using Ilitera.Common;
using System.Collections;
using System.Text;

namespace Ilitera.Sied.Report
{
	public class DataSourcePlanilhaA4PPRA_PCA : DataSourceBase
	{
		private Cliente cliente;
		private LaudoTecnico laudoTecnico;
		public bool ComGheSemEmpregado=false;
		
		public DataSourcePlanilhaA4PPRA_PCA(Cliente cliente)
		{
			this.cliente = cliente;

            if (this.cliente.mirrorOld == null)
			    this.cliente.Find();

            this.laudoTecnico = LaudoTecnico.GetUltimoLaudo(this.cliente.Id);
		}

		public DataSourcePlanilhaA4PPRA_PCA(LaudoTecnico laudoTecnico)
		{
			this.laudoTecnico = laudoTecnico;
			this.cliente = laudoTecnico.nID_EMPR;
            
            if (this.cliente.mirrorOld == null)
			    this.cliente.Find();
		}

        public DataSourcePlanilhaA4PPRA_PCA(Cliente cliente, LaudoTecnico laudoTecnico)
        {
            this.laudoTecnico = laudoTecnico;
            this.cliente = cliente;

            if (this.cliente.mirrorOld == null)
                this.cliente.Find();
        }

        public DataSourcePlanilhaA4PPRA_PCA(int IdLaudoTecnico)
		{
            this.laudoTecnico = new LaudoTecnico();
            this.laudoTecnico.Find(IdLaudoTecnico);

			this.cliente = laudoTecnico.nID_EMPR;

            if (this.cliente.mirrorOld == null)
			    this.cliente.Find();
		}	

		public RptPPRA_A4_PCA GetReport()
		{
			RptPPRA_A4_PCA report = new RptPPRA_A4_PCA();
			
			if(laudoTecnico==null)
				laudoTecnico = LaudoTecnico.GetUltimoLaudo(cliente.Id);

			report.SetDataSource(GetDataSource());
			report.Refresh();

            SetTempoProcessamento(report);

			return report;
		}

        public RptPPRA_A4_PCA_PGR GetReport_PCAPGR()
        {
            RptPPRA_A4_PCA_PGR report = new RptPPRA_A4_PCA_PGR();

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

            string strEquipMedicao = ""; // laudoTecnico.GetEquipamentos();

            ArrayList listGhe = new Ghe().Find("nID_LAUD_TEC=" + laudoTecnico.Id + " ORDER BY tNO_FUNC");

            foreach (Ghe ghe in listGhe)
            {
                int numTrabExpostos = ghe.GetNumeroEmpregadosExpostos(false);

                string nomeGHE = ghe.tNO_FUNC;

                string descricaoFuncao = "";

                if (cliente.PGR_Apenas_Funcoes_Ativas != null && cliente.PGR_Apenas_Funcoes_Ativas == true)
                    descricaoFuncao = ghe.FuncoesIntegrantes_Ativas_Apenas();
                else
                    descricaoFuncao = ghe.FuncoesIntegrantes();


                string xRiscos = "";
                string xConcentracao = "";

                ArrayList listPPRA = new PPRA().Find("nID_FUNC=" + ghe.Id + " ORDER BY nID_RSC");

                //Campo EPC
                StringBuilder sEPC = new StringBuilder();
                //string strEpc = ghe.Epc();
                //string strEpiAcientente = ghe.EpiAcidentes();

                //string strCaraterAdm = ghe.GetMedidasControleAdministrativa();
                //string strCaraterEdu = ghe.GetMedidasControleEducacional();

                //if (strEpc.IndexOf("Inexistente") == -1)
                //    sEPC.Append("EPC: \n" + strEpc + "\n");

                //if (strEpiAcientente.IndexOf("Inexistente") == -1)
                //    sEPC.Append("EPI (Riscos de Acidentes):\n" + strEpiAcientente + "\n");

                //if (strCaraterAdm.IndexOf("Inexistente") == -1)
                //    sEPC.Append("Medidas de Caráter Administrativo:\n" + strCaraterAdm + "\n");

                //if (strCaraterEdu.IndexOf("Inexistente") == -1)
                //    sEPC.Append("Medidas de Caráter Educacional:\n" + strCaraterEdu + "\n");

                //if (strEpc.IndexOf("Inexistente") != -1 && strEpiAcientente.IndexOf("Inexistente") != -1 && strCaraterAdm.IndexOf("Inexistente") != -1 && strCaraterEdu.IndexOf("Inexistente") != -1)
                //{
                //    //sEPC.Append("Inexistente");
                //    sEPC.Append("Não aplicável");
                //}

                int i = 1;
                bool zLoc = false;

                foreach (PPRA ppra in listPPRA)
                {
                    string xTipo = "";

                    xRiscos = i.ToString() + ". " + ppra.GetReconhecimentoPPRA(false);
                    xConcentracao = ppra.GetAvaliacaoQuantitativa();


                    //ArrayList listEpi = ppra.GetListaEPI();

                    //zLoc = false;

                    //foreach (string epi in listEpi)
                    //{

                    //    Epi zEpi = new Epi();

                    //    if (epi.IndexOf("(") > 0)
                    //    {
                    //        zEpi.Find(" Nome like '" + epi.Substring(0, epi.IndexOf("(") - 2).Trim() + "%' or Descricao like '" + epi.Substring(0, epi.IndexOf("(") - 2).Trim() + "%' ");
                    //    }
                    //    else
                    //    {
                    //        zEpi.Find(" Nome = '" + epi + "' or Descricao = '" + epi + "' ");
                    //    }


                    //    if (zEpi.IndEpi == 1)
                    //        zLoc = true;

                    //}



                    ArrayList listEpi = ppra.GetListaEPI_Id();

                    zLoc = false;

                    foreach (string epi in listEpi)
                    {

                        Epi zEpi = new Epi();

                        zEpi.Find(" IdEpi = " + epi + " ");

                        if (zEpi.IndEpi == 1)
                            zLoc = true;

                    }


                    //       if (xEPI.ToUpper().IndexOf("MÁSCARA") >= 0 || xEPI.ToUpper().IndexOf("MASCARA") >= 0)
                    if (zLoc == true)
                    {
                        newRow = ds.Tables[0].NewRow();
                        newRow["CarimboCNPJ"] = cliente.GetCarimboCnpjHtml(laudoTecnico.hDT_LAUDO);
                        newRow["IdGHE"] = ghe.Id;
                        newRow["IdCliente"] = cliente.Id;
                        newRow["Ghe"] = nomeGHE;
                        newRow["DescricaoFuncao"] = descricaoFuncao;
                        newRow["TrabalhadoresExpostos"] = numTrabExpostos.ToString();

                        xTipo = ppra.nID_RSC.ToString().Trim();
                        if (xTipo == "")
                        {
                            xTipo = ppra.nID_RSC.DescricaoResumido.ToString().Trim();
                        }


                        //if (xTipo != "Acidentes")
                        newRow["RiscosAmb"] = xRiscos;

                        if (ppra.nID_RSC.Id == (int)Riscos.RuidoContinuo)
                        {
                            newRow["RiscosAmb_Concentracao"] = xConcentracao;
                            newRow["RiscosAmb_LimiteTol"] = ppra.GetLimiteTolerancia();
                        }
                        else if (ppra.nID_RSC.Id == (int)Riscos.Calor)
                        {
                            newRow["RiscosAmb_Concentracao"] = ghe.CalorIBUTG();
                            newRow["RiscosAmb_LimiteTol"] = ghe.CalorLimite();
                        }
                        else
                        {
                            //if ( xTipo != "Acidentes")
                            //{
                            newRow["RiscosAmb_Concentracao"] = ppra.GetAvaliacaoQuantitativa();
                            newRow["RiscosAmb_LimiteTol"] = ppra.GetLimiteTolerancia();
                            //}
                        }

                        newRow["EPI"] = ppra.GetEpi();

                        sEPC = new StringBuilder();

                        //newRow["EPC"] = sEPC.ToString();
                        if (ppra.mDS_EPC_EXTE.Trim() != "")
                            sEPC.Append("EPC: \n" + ppra.mDS_EPC_EXTE.Trim() + "\n");

                        if (ppra.mDS_MED_CTL_CAR_ADM.Trim() != "")
                            sEPC.Append("Medidas de Caráter Administrativo:\n" + ppra.mDS_MED_CTL_CAR_ADM + "\n");

                        if (ppra.mDS_MED_CTL_CAR_EDC.Trim() != "")
                            sEPC.Append("Medidas de Caráter Educacional:\n" + ppra.mDS_MED_CTL_CAR_EDC.Trim() + "\n");

                        if (sEPC.ToString().Trim() == "")
                            sEPC.Append("Não aplicável");

                        newRow["EPC"] = sEPC;

                        newRow["EQUIP_MEDICAO"] = strEquipMedicao;
                        newRow["LimiteUltrapassado"] = ppra.gINSALUBRE;
                        newRow["bAnalQuantAgQuimico"] = AnalQuantAgQuim;
                        newRow["bAnalQuantCalor"] = AnalQuantAgFis;

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
            return table;
        }
	}
}
