using System;
using System.Data;
using System.Collections;
using Ilitera.Common;
using Ilitera.Opsa.Data;

namespace Ilitera.Relatorios.Report
{
	public class DataSourceDocumentoBasePGR : DataSourceBase
	{

		private Cliente cliente;
		private LaudoTecnico laudoTecnico;
        private Int32 zObra;

		public DataSourceDocumentoBasePGR(Cliente cliente)
		{		
			this.cliente = cliente;
            this.zObra = 0;

			if(this.cliente.mirrorOld==null)
				this.cliente.Find();

			this.laudoTecnico = LaudoTecnico.GetUltimoLaudo(this.cliente.Id);
		}

        public DataSourceDocumentoBasePGR(LaudoTecnico laudoTecnico)
        {
            this.laudoTecnico = laudoTecnico;
            this.zObra = 0;
            this.cliente = laudoTecnico.nID_EMPR;

            if (this.cliente.mirrorOld == null)
                this.cliente.Find();
        }

		public DataSourceDocumentoBasePGR(LaudoTecnico laudoTecnico, Int32 xObra)
		{
			this.laudoTecnico = laudoTecnico;
            this.zObra = xObra;
			this.cliente = laudoTecnico.nID_EMPR;

			if(this.cliente.mirrorOld==null)
				this.cliente.Find();
		}

        public DataSourceDocumentoBasePGR(Cliente cliente, LaudoTecnico laudoTecnico)
        {
            this.laudoTecnico = laudoTecnico;
            this.zObra = 0;
            this.cliente = cliente;

            if (this.cliente.mirrorOld == null)
                this.cliente.Find();
        }

		public DataSourceDocumentoBasePGR(int IdLaudoTecnico, Int32  xObra)
		{
			this.laudoTecnico = new LaudoTecnico();
			this.laudoTecnico.Find(IdLaudoTecnico);
			this.zObra = 0;
			this.cliente = laudoTecnico.nID_EMPR;

			if(this.cliente.mirrorOld==null)
				this.cliente.Find();
		}	

		public RptDocumentoBasePGR GetReport()
		{
			RptDocumentoBasePGR report = new RptDocumentoBasePGR();
			report.SetDataSource(GetDataSource());
			report.Refresh();

            SetTempoProcessamento(report);

			return report;
		}


        public RptDocumentoBasePGR GetReportPGR()
        {
            RptDocumentoBasePGR report = new RptDocumentoBasePGR();
            report.SetDataSource(GetDataSource());
            report.Refresh();

            SetTempoProcessamento(report);

            return report;
        }

     

		private DataSet GetDataSource()
		{
            DataSet ds = new DataSet();
            DataTable table = new DataTable("Result");
            table.Columns.Add("CarimboCNPJ", Type.GetType("System.String"));
            table.Columns.Add("PlanejamentoAnual", Type.GetType("System.String"));
            table.Columns.Add("Grupos", Type.GetType("System.String"));
            table.Columns.Add("Jan", Type.GetType("System.String"));
            table.Columns.Add("Fev", Type.GetType("System.String"));
            table.Columns.Add("Mar", Type.GetType("System.String"));
            table.Columns.Add("Abr", Type.GetType("System.String"));
            table.Columns.Add("Mai", Type.GetType("System.String"));
            table.Columns.Add("Jun", Type.GetType("System.String"));
            table.Columns.Add("Jul", Type.GetType("System.String"));
            table.Columns.Add("Ago", Type.GetType("System.String"));
            table.Columns.Add("Set", Type.GetType("System.String"));
            table.Columns.Add("Out", Type.GetType("System.String"));
            table.Columns.Add("Nov", Type.GetType("System.String"));
            table.Columns.Add("Dez", Type.GetType("System.String"));
            table.Columns.Add("Ano", Type.GetType("System.String"));
            table.Columns.Add("Acao", Type.GetType("System.String"));
            table.Columns.Add("Servicos", Type.GetType("System.String"));
            table.Columns.Add("FormaRegistro", Type.GetType("System.String"));
            table.Columns.Add("EstrategiaMetAcao", Type.GetType("System.String"));
            table.Columns.Add("Prioridade", Type.GetType("System.String"));
            table.Columns.Add("nQuestoes", Type.GetType("System.Int16"));
            table.Columns.Add("Afericao", Type.GetType("System.String"));
            table.Columns.Add("Prazo", Type.GetType("System.String"));
            ds.Tables.Add(table);
            DataRow newRow;

            if (cliente.PGR_Personalizado == false)
            {

                Ilitera.Data.PPRA_EPI CronPGR = new Data.PPRA_EPI();
                DataSet rDs = CronPGR.Trazer_Cronograma_PGR_Agrupado(laudoTecnico.Id);


                string zOldAcao = "";
                string zGHE = "";

                newRow = ds.Tables[0].NewRow();

                if (rDs.Tables[0].Rows.Count > 0)
                {
                    zOldAcao = rDs.Tables[0].Rows[0]["AcaoLocalTrab"].ToString();
                    zGHE = rDs.Tables[0].Rows[0]["GHE"].ToString().Trim();

                    newRow["CarimboCNPJ"] = cliente.GetCarimboCnpjHtml(laudoTecnico.hDT_LAUDO);
                }



                for (int rCont = 0; rCont < rDs.Tables[0].Rows.Count; rCont++)
                {

                    if (rDs.Tables[0].Rows[rCont]["AcaoLocalTrab"].ToString() == zOldAcao)
                    {
                        if (zGHE.IndexOf(rDs.Tables[0].Rows[rCont]["GHE"].ToString().Trim()) < 0)
                        {
                            zGHE = zGHE + System.Environment.NewLine + rDs.Tables[0].Rows[rCont]["GHE"].ToString().Trim();
                        }

                        newRow["Jan"] = MesCronograma((bool)rDs.Tables[0].Rows[rCont]["Mes01"]);
                        newRow["Fev"] = MesCronograma((bool)rDs.Tables[0].Rows[rCont]["Mes02"]);
                        newRow["Mar"] = MesCronograma((bool)rDs.Tables[0].Rows[rCont]["Mes03"]);
                        newRow["Abr"] = MesCronograma((bool)rDs.Tables[0].Rows[rCont]["Mes04"]);
                        newRow["Mai"] = MesCronograma((bool)rDs.Tables[0].Rows[rCont]["Mes05"]);
                        newRow["Jun"] = MesCronograma((bool)rDs.Tables[0].Rows[rCont]["Mes06"]);
                        newRow["Jul"] = MesCronograma((bool)rDs.Tables[0].Rows[rCont]["Mes07"]);
                        newRow["Ago"] = MesCronograma((bool)rDs.Tables[0].Rows[rCont]["Mes08"]);
                        newRow["Set"] = MesCronograma((bool)rDs.Tables[0].Rows[rCont]["Mes09"]);
                        newRow["Out"] = MesCronograma((bool)rDs.Tables[0].Rows[rCont]["Mes10"]);
                        newRow["Nov"] = MesCronograma((bool)rDs.Tables[0].Rows[rCont]["Mes11"]);
                        newRow["Dez"] = MesCronograma((bool)rDs.Tables[0].Rows[rCont]["Mes12"]);
                        newRow["Ano"] = rDs.Tables[0].Rows[rCont]["Ano"].ToString();

                        newRow["FormaRegistro"] = rDs.Tables[0].Rows[rCont]["FormaReg"].ToString();
                        newRow["EstrategiaMetAcao"] = rDs.Tables[0].Rows[rCont]["Metodologia"].ToString();
                        newRow["Prioridade"] = rDs.Tables[0].Rows[rCont]["Prioridade"].ToString();

                        //newRow["nQuestoes"] = rDs.Tables[0].Rows[rCont]["nQuestões"].ToString();
                        newRow["Afericao"] = rDs.Tables[0].Rows[rCont]["Acao"].ToString();
                        newRow["Prazo"] = rDs.Tables[0].Rows[rCont]["Serv"].ToString();

                    }
                    else
                    {

                        newRow["PlanejamentoAnual"] = zOldAcao + System.Environment.NewLine + zGHE;

                        ds.Tables[0].Rows.Add(newRow);

                        zOldAcao = rDs.Tables[0].Rows[rCont]["AcaoLocalTrab"].ToString();
                        zGHE = rDs.Tables[0].Rows[rCont]["GHE"].ToString().Trim();

                        newRow = ds.Tables[0].NewRow();

                        newRow["CarimboCNPJ"] = cliente.GetCarimboCnpjHtml(laudoTecnico.hDT_LAUDO);
                        newRow["Jan"] = MesCronograma((bool)rDs.Tables[0].Rows[rCont]["Mes01"]);
                        newRow["Fev"] = MesCronograma((bool)rDs.Tables[0].Rows[rCont]["Mes02"]);
                        newRow["Mar"] = MesCronograma((bool)rDs.Tables[0].Rows[rCont]["Mes03"]);
                        newRow["Abr"] = MesCronograma((bool)rDs.Tables[0].Rows[rCont]["Mes04"]);
                        newRow["Mai"] = MesCronograma((bool)rDs.Tables[0].Rows[rCont]["Mes05"]);
                        newRow["Jun"] = MesCronograma((bool)rDs.Tables[0].Rows[rCont]["Mes06"]);
                        newRow["Jul"] = MesCronograma((bool)rDs.Tables[0].Rows[rCont]["Mes07"]);
                        newRow["Ago"] = MesCronograma((bool)rDs.Tables[0].Rows[rCont]["Mes08"]);
                        newRow["Set"] = MesCronograma((bool)rDs.Tables[0].Rows[rCont]["Mes09"]);
                        newRow["Out"] = MesCronograma((bool)rDs.Tables[0].Rows[rCont]["Mes10"]);
                        newRow["Nov"] = MesCronograma((bool)rDs.Tables[0].Rows[rCont]["Mes11"]);
                        newRow["Dez"] = MesCronograma((bool)rDs.Tables[0].Rows[rCont]["Mes12"]);
                        newRow["Ano"] = rDs.Tables[0].Rows[rCont]["Ano"].ToString();

                        newRow["FormaRegistro"] = rDs.Tables[0].Rows[rCont]["FormaReg"].ToString();
                        newRow["EstrategiaMetAcao"] = rDs.Tables[0].Rows[rCont]["Metodologia"].ToString();
                        newRow["Prioridade"] = rDs.Tables[0].Rows[rCont]["Prioridade"].ToString();

                        //newRow["nQuestoes"] = rDs.Tables[0].Rows[rCont]["nQuestões"].ToString();
                        newRow["Afericao"] = rDs.Tables[0].Rows[rCont]["Acao"].ToString();
                        newRow["Prazo"] = rDs.Tables[0].Rows[rCont]["Serv"].ToString();

                    }

                }



                if (rDs.Tables[0].Rows.Count == 0)
                {
                    newRow = ds.Tables[0].NewRow();
                    newRow["CarimboCNPJ"] = cliente.GetCarimboCnpjHtml(laudoTecnico.hDT_LAUDO);
                    ds.Tables[0].Rows.Add(newRow);
                }
                else
                {
                    newRow["PlanejamentoAnual"] = zOldAcao + System.Environment.NewLine + zGHE;

                    ds.Tables[0].Rows.Add(newRow);

                }

            }
            else
            {

                //ArrayList listCronograma = new CronogramaPGR().Find("nID_LAUD_TEC=" + laudoTecnico.Id + " order by nQuestões ");
                ArrayList listCronograma = new CronogramaPGR().Find("nID_LAUD_TEC=" + laudoTecnico.Id + " order by case when nquestões not between 1 and 5 then nQuestões+100 else nQuestões end, AcaoLocalTrab  ");

                foreach (CronogramaPGR cronogramaPGR in listCronograma)
                {
                    newRow = ds.Tables[0].NewRow();
                    newRow["CarimboCNPJ"] = cliente.GetCarimboCnpjHtml(laudoTecnico.hDT_LAUDO);
                    newRow["PlanejamentoAnual"] = cronogramaPGR.AcaoLocalTrab;
                    // newRow["Grupos"] = cronogramaPCA.GetNomeGhe();
                    newRow["Jan"] = MesCronograma(cronogramaPGR.Mes01);
                    newRow["Fev"] = MesCronograma(cronogramaPGR.Mes02);
                    newRow["Mar"] = MesCronograma(cronogramaPGR.Mes03);
                    newRow["Abr"] = MesCronograma(cronogramaPGR.Mes04);
                    newRow["Mai"] = MesCronograma(cronogramaPGR.Mes05);
                    newRow["Jun"] = MesCronograma(cronogramaPGR.Mes06);
                    newRow["Jul"] = MesCronograma(cronogramaPGR.Mes07);
                    newRow["Ago"] = MesCronograma(cronogramaPGR.Mes08);
                    newRow["Set"] = MesCronograma(cronogramaPGR.Mes09);
                    newRow["Out"] = MesCronograma(cronogramaPGR.Mes10);
                    newRow["Nov"] = MesCronograma(cronogramaPGR.Mes11);
                    newRow["Dez"] = MesCronograma(cronogramaPGR.Mes12);
                    newRow["Ano"] = cronogramaPGR.Ano;
                    //newRow["Servicos"] = cronogramaPGR.Serv;
                    //newRow["Acao"] = cronogramaPGR.Acao;
                    newRow["FormaRegistro"] = cronogramaPGR.FormaReg;
                    newRow["EstrategiaMetAcao"] = cronogramaPGR.Metodologia;
                    newRow["Prioridade"] = cronogramaPGR.Prioridade;

                    newRow["nQuestoes"] = cronogramaPGR.nQuestões;
                    newRow["Afericao"] = cronogramaPGR.Acao;
                    newRow["Prazo"] = cronogramaPGR.Serv;

                    ds.Tables[0].Rows.Add(newRow);
                }

                if (listCronograma.Count == 0)
                {
                    newRow = ds.Tables[0].NewRow();
                    newRow["CarimboCNPJ"] = cliente.GetCarimboCnpjHtml(laudoTecnico.hDT_LAUDO);
                    ds.Tables[0].Rows.Add(newRow);
                }

            }

            return ds;


        }

        private string MesCronograma(bool bVal)
		{
			string mes = "";

            if (bVal)   
            {
                mes = "•"; //"•"; 
            }

			return mes;
		}
	}
}
