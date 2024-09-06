using System;
using Ilitera.Opsa.Data;
using System.Data;
using Ilitera.Common;
using System.Collections;
using System.Text;

namespace Ilitera.Sied.Report
{
	public class DataSourcePlanilhaA4PPRA_Novo : DataSourceBase
	{
		private Cliente cliente;
		private LaudoTecnico laudoTecnico;
        private string ListaGHEs;
        private Int32 zObra;

		public bool ComGheSemEmpregado=false;
		
		public DataSourcePlanilhaA4PPRA_Novo(Cliente cliente)
		{
			this.cliente = cliente;
            this.ListaGHEs = "";
            this.zObra = 0;

            if (this.cliente.mirrorOld == null)
			    this.cliente.Find();

            this.laudoTecnico = LaudoTecnico.GetUltimoLaudo(this.cliente.Id);
		}

		public DataSourcePlanilhaA4PPRA_Novo(LaudoTecnico laudoTecnico)
		{
			this.laudoTecnico = laudoTecnico;
			this.cliente = laudoTecnico.nID_EMPR;
            this.ListaGHEs = "";
            this.zObra = 0;
            
            if (this.cliente.mirrorOld == null)
			    this.cliente.Find();
		}

        public DataSourcePlanilhaA4PPRA_Novo(LaudoTecnico laudoTecnico, string xListaGHE, Int32 zObra)
        {
            this.laudoTecnico = laudoTecnico;
            this.cliente = laudoTecnico.nID_EMPR;
            this.ListaGHEs = xListaGHE;
            this.zObra = zObra;

            if (this.cliente.mirrorOld == null)
                this.cliente.Find();
        }

        public DataSourcePlanilhaA4PPRA_Novo(Cliente cliente, LaudoTecnico laudoTecnico)
        {
            this.laudoTecnico = laudoTecnico;
            this.cliente = cliente;
            this.ListaGHEs = "";
            this.zObra = 0;

            if (this.cliente.mirrorOld == null)
                this.cliente.Find();
        }

        public DataSourcePlanilhaA4PPRA_Novo(int IdLaudoTecnico)
		{
            this.laudoTecnico = new LaudoTecnico();
            this.laudoTecnico.Find(IdLaudoTecnico);
            this.ListaGHEs = ""; 
            this.zObra = 0;

			this.cliente = laudoTecnico.nID_EMPR;

            if (this.cliente.mirrorOld == null)
			    this.cliente.Find();
		}

        public RptPPRA_A4_Novo GetReport()
		{
            RptPPRA_A4_Novo report = new RptPPRA_A4_Novo();
			
			if(laudoTecnico==null)
				laudoTecnico = LaudoTecnico.GetUltimoLaudo(cliente.Id);

			report.SetDataSource(GetDataSource());
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
                    int numTrabExpostos_Ativos = ghe.GetNumeroEmpregadosExpostos_Ativos();


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

                    if (strEpc.IndexOf("Inexistente") == -1)
                        sEPC.Append("EPC: \n" + strEpc + "\n");

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

                        System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");

                        if (this.zObra == 0)
                        {                            
                            newRow["CarimboCNPJ"] = cliente.GetCarimboCnpjHtml(laudoTecnico.hDT_LAUDO) + "<br>Data do Laudo:" + laudoTecnico.hDT_LAUDO.ToString("dd/MM/yyyy", ptBr); ;
                        }
                        else
                        {
                            LaudoTecnico zLaudo = new LaudoTecnico();

                            zLaudo.Find(" nId_Empr = " + this.zObra.ToString() + " order by hDt_Laudo Desc");

                            Ilitera.Common.Juridica xJuridica = new Ilitera.Common.Juridica();
                            xJuridica.Find(this.zObra);
                                                     
                            //newRow["CarimboCNPJ"] = cliente.GetCarimboCnpjHtml(zLaudo.hDT_LAUDO, xJuridica);
                            newRow["CarimboCNPJ"] = cliente.GetCarimboCnpjHtml(zLaudo.hDT_LAUDO) + "<br>Data do Laudo:" + laudoTecnico.hDT_LAUDO.ToString("dd/MM/yyyy", ptBr); ;
                        }

                        newRow["IdGHE"] = ghe.Id;
                        newRow["IdCliente"] = cliente.Id;
                        newRow["Ghe"] = nomeGHE;

                        if ( ghe.tDS_LOCAL_TRAB.ToString().Trim() != "" )
                            newRow["Conclusao"] = "Descrição do local avaliado: " + ghe.tDS_LOCAL_TRAB.ToString().Trim() + System.Environment.NewLine + System.Environment.NewLine + ghe.Conclusao(cliente); 
                        else
                           newRow["Conclusao"] = ghe.Conclusao(cliente); 


                        newRow["DescricaoFuncao"] = descricaoFuncao;
                        newRow["TrabalhadoresExpostos"] = numTrabExpostos.ToString();

                        newRow["bRiscoInsignificante"] =  bRiscoInsignificante;

                        xTipo = ppra.nID_RSC.ToString().Trim();
                        if (xTipo == "")
                        {
                            xTipo = ppra.nID_RSC.DescricaoResumido.ToString().Trim();
                        }




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
                        // }
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

                        //para ajustar todos ghes da ambev
                        //ppra.mDS_FRS_RSC = ppra.GetReconhecimentoPPRAPadrao(false);
                        //ppra.Save();

                    }

                    if (listPPRA.Count == 0)   // se não há riscos
                    {

                        //string xTipo = "";

                        newRow = ds.Tables[0].NewRow();


                        System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");

                        if (this.zObra == 0)
                        {
                            newRow["CarimboCNPJ"] = cliente.GetCarimboCnpjHtml(laudoTecnico.hDT_LAUDO) + "<br>Data do Laudo:" + laudoTecnico.hDT_LAUDO.ToString("dd/MM/yyyy", ptBr); ;
                        }
                        else
                        {
                            LaudoTecnico zLaudo = new LaudoTecnico();

                            zLaudo.Find(" nId_Empr = " + this.zObra.ToString() + " order by hDt_Laudo Desc");

                            Ilitera.Common.Juridica xJuridica = new Ilitera.Common.Juridica();
                            xJuridica.Find(this.zObra);

                            //newRow["CarimboCNPJ"] = cliente.GetCarimboCnpjHtml(zLaudo.hDT_LAUDO, xJuridica);
                            newRow["CarimboCNPJ"] = cliente.GetCarimboCnpjHtml(zLaudo.hDT_LAUDO) + "<br>Data do Laudo:" + laudoTecnico.hDT_LAUDO.ToString("dd/MM/yyyy", ptBr); ;
                        }



                        newRow["IdGHE"] = ghe.Id;
                        newRow["IdCliente"] = cliente.Id;
                        newRow["Ghe"] = nomeGHE;

                        if (ghe.tDS_LOCAL_TRAB.ToString().Trim() != "")
                            newRow["Conclusao"] = "Descrição do local avaliado: " + ghe.tDS_LOCAL_TRAB.ToString().Trim() + System.Environment.NewLine + System.Environment.NewLine + ghe.Conclusao(cliente);
                        else
                            newRow["Conclusao"] = ghe.Conclusao(cliente);

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
            table.Columns.Add("Conclusao", Type.GetType("System.String"));
            table.Columns.Add("Cor", Type.GetType("System.String"));
            return table;
        }
	}
}
