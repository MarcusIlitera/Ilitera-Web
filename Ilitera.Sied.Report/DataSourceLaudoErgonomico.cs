using System;
using System.Data;
using Ilitera.Opsa.Data;
using Ilitera.Common;
using System.Collections;
using System.Drawing;

namespace Ilitera.Sied.Report
{
	public class DataSourceLaudoErgonomico : DataSourceBase
	{
		private Cliente cliente;	
		private LaudoTecnico laudoTecnico;

		public DataSourceLaudoErgonomico(Cliente cliente)
		{
			this.cliente = cliente;
            this.laudoTecnico = LaudoTecnico.GetUltimoLaudo(cliente.Id);
		}

        public DataSourceLaudoErgonomico(LaudoTecnico laudoTecnico)
        {
            this.laudoTecnico = laudoTecnico;
            this.cliente = laudoTecnico.nID_EMPR;

            if (this.cliente.mirrorOld == null)
                this.cliente.Find();
        }

        public DataSourceLaudoErgonomico(int IdLaudoTecnico)
        {
            this.laudoTecnico = new LaudoTecnico();
            this.laudoTecnico.Find(IdLaudoTecnico);

            this.cliente = laudoTecnico.nID_EMPR;

            if (this.cliente.mirrorOld == null)
                this.cliente.Find();
        }	

		public RptErgonomia GetReport()
		{
            RptErgonomia report = new RptErgonomia();	
			report.OpenSubreport("FotoFachada").SetDataSource(DataSourceFotoFachada());
			report.OpenSubreport("Prestador").SetDataSource(GetPrestador(laudoTecnico.nID_COMISSAO_2, laudoTecnico.nID_COMISSAO_3));
			report.SetDataSource(GetLaudoErgonomico(false));
			report.Refresh();

            SetTempoProcessamento(report);

            return report;
		}


        public RptErgonomia_New GetReport_New()
        {
            RptErgonomia_New report = new RptErgonomia_New();
            report.OpenSubreport("FotoFachada").SetDataSource(DataSourceFotoFachada());
            report.OpenSubreport("Prestador").SetDataSource(GetPrestador(laudoTecnico.nID_COMISSAO_2, laudoTecnico.nID_COMISSAO_3));
            report.SetDataSource(GetLaudoErgonomico(true));
            report.Refresh();

            SetTempoProcessamento(report);

            return report;
        }



        private DataSet GetLaudoErgonomico(bool NewReport)
		{
			DataSet ds = new DataSet();
            DataTable table = GetTable();
			ds.Tables.Add(table);
			DataRow newRow;
			
			ArrayList listGhe = new Ghe().Find("nID_LAUD_TEC=" + laudoTecnico.Id 
												+ " ORDER BY tNO_FUNC");

            string strDadosEmpresa = cliente.GetDadosEmpresaHtml(laudoTecnico.hDT_LAUDO);
            
            foreach (Ghe ghe in listGhe)
            {
                newRow = ds.Tables[0].NewRow();

                newRow["Anexo1"] = "";
                newRow["Descr1"] = "";
                newRow["Anexo2"] = "";
                newRow["Descr2"] = "";
                newRow["Anexo3"] = "";
                newRow["Descr3"] = "";
                newRow["Anexo4"] = "";
                newRow["Descr4"] = "";
                newRow["Anexo5"] = "";
                newRow["Descr5"] = "";

                int rCont = 0;

                ArrayList list = new Ghe_Anexos().Find(" nId_Func=" + ghe.Id.ToString() + " order by Arquivo");

                foreach (Ghe_Anexos zAnexo in list)
                {
                    if (rCont == 0)
                    {
                        newRow["Anexo1"] = zAnexo.Arquivo;
                        newRow["Descr1"] = ghe.tNO_FUNC.ToUpper().Trim() + System.Environment.NewLine + zAnexo.Descricao;
                    }
                    else if (rCont == 1)
                    {
                        newRow["Anexo2"] = zAnexo.Arquivo;
                        newRow["Descr2"] = ghe.tNO_FUNC.ToUpper().Trim() + System.Environment.NewLine + zAnexo.Descricao;
                    }
                    else if (rCont == 2)
                    {
                        newRow["Anexo3"] = zAnexo.Arquivo;
                        newRow["Descr3"] = ghe.tNO_FUNC.ToUpper().Trim() + System.Environment.NewLine + zAnexo.Descricao;
                    }
                    else if (rCont == 3)
                    {
                        newRow["Anexo4"] = zAnexo.Arquivo;
                        newRow["Descr4"] = ghe.tNO_FUNC.ToUpper().Trim() + System.Environment.NewLine + zAnexo.Descricao;
                    }
                    else if (rCont == 4)
                    {
                        newRow["Anexo5"] = zAnexo.Arquivo;
                        newRow["Descr5"] = ghe.tNO_FUNC.ToUpper().Trim() + System.Environment.NewLine + zAnexo.Descricao;
                    }

                    rCont++;

                }





                newRow["DadosEmpresa"] = strDadosEmpresa;
                newRow["RazaoSocial"] = cliente.NomeCompleto;
                newRow["GHE"] = ghe.tNO_FUNC;

                if (ghe.tNO_FOTO_FUNC > 0)
                {
                    newRow["FotografiaParadigma"] = ghe.GetFotoParadigma();
                }
                else
                {
                    newRow["FotografiaParadigma"] = "";
                }

                
                newRow["DescricaoAtividadesGHE"] = ghe.tDS_FUNC;
                newRow["DemominacaoFuncoesGHE"] = ghe.FuncoesIntegrantes();
                newRow["DescricaoLocalTrabalho"] = ghe.tDS_LOCAL_TRAB;


                //newRow["IluminanciaMedia"] = ghe.nLUX + " Lux";
                if (ghe.nIluminancia != 0)
                    newRow["IluminanciaMedia"] = ghe.nIluminancia + " E(lux)";
                else
                    newRow["IluminanciaMedia"] = ghe.nLUX + " Lux";



                newRow["IluminanciaRecomendada"] = ghe.GetIluminanciaRecomendada();
                newRow["UmidadeRelativaDoAr"] = ghe.nUMID + " %";
                newRow["VelocidadeDoAr"] = ghe.nVELOC + " m/s";
                newRow["Temperatura"] = ghe.nTEMP + " ºC";


                if (ghe.bImp_17_6_2 == true)
                {
                    newRow["bImp_17_6_2"] = "1";
                    newRow["tNormas_Producao"] = ghe.tNormas_Producao.Trim();
                    newRow["tModo_Operacao"] = ghe.tModo_Operatorio.Trim();
                    newRow["tExigencia_Tempo"] = ghe.tExigencia_Tempo.Trim();
                    newRow["tDeterminacao_Conteudo"] = ghe.tDeterminacao_Conteudo.Trim();
                    newRow["tRitmo_Trabalho"] = ghe.tRitmo_Trabalho.Trim();
                    newRow["tConteudo_Tarefa"] = ghe.tConteudo_Tarefa.Trim();
                }
                else
                {
                    newRow["bImp_17_6_2"] = "0";
                    newRow["tNormas_Producao"] = "";
                    newRow["tModo_Operacao"] = "";
                    newRow["tExigencia_Tempo"] = "";
                    newRow["tDeterminacao_Conteudo"] = "";
                    newRow["tRitmo_Trabalho"] = "";
                    newRow["tConteudo_Tarefa"] = "";
                }




                if (ghe.bImp_Recomendacoes == true)
                {
                    newRow["bImp_Recomendacoes"] = "1";
                    if (ghe.tRecomendacoes == null)
                        newRow["tRecomendacoes"] = "";
                    else
                        newRow["tRecomendacoes"] = ghe.tRecomendacoes.Trim();
                }
                else
                {
                    newRow["bImp_Recomendacoes"] = "0";
                    newRow["tRecomendacoes"] = "";
                }


                int numTrabExpostos = ghe.GetNumeroEmpregadosExpostos(false);

                if (numTrabExpostos == 0)
                    continue;

                newRow["TrabalhadoresExpostos"] = numTrabExpostos;

                //ArrayList listNr17 = new ErgonomiaGhe().Find("nID_LAUD_TEC=" + laudoTecnico.Id + " AND nID_FUNC=" + ghe.Id);

                //foreach (ErgonomiaGhe ergonomiaGhe in listNr17)
                //    PopularGabarito(newRow, ergonomiaGhe);

                if (NewReport == false)
                {
                    ArrayList listNr17 = new ErgonomiaGhe().Find("nID_LAUD_TEC=" + laudoTecnico.Id + " AND nID_FUNC=" + ghe.Id);

                    foreach (ErgonomiaGhe ergonomiaGhe in listNr17)
                        PopularGabarito(newRow, ergonomiaGhe);
                }
                else
                {
                    ArrayList listNr17 = new ErgonomiaGhe_New().Find("nID_LAUD_TEC=" + laudoTecnico.Id + " AND nID_FUNC=" + ghe.Id);

                    foreach (ErgonomiaGhe_New ergonomiaGhe in listNr17)
                        PopularGabarito_New(newRow, ergonomiaGhe);
                }


                if (Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper().IndexOf("GLOBAL") > 0)
                {
                    newRow["Base"] = "GLOBAL";
                }
                else
                {
                    newRow["Base"] = "OUTRAS";
                }


                ds.Tables[0].Rows.Add(newRow);
            }
			return ds;
		}

        private static DataTable GetTable()
        {
            DataTable table = new DataTable("Result");
            table.Columns.Add("DadosEmpresa", Type.GetType("System.String"));
            table.Columns.Add("RazaoSocial", Type.GetType("System.String"));
            table.Columns.Add("GHE", Type.GetType("System.String"));
            table.Columns.Add("FotografiaParadigma", Type.GetType("System.String"));
            table.Columns.Add("DescricaoAtividadesGHE", Type.GetType("System.String"));
            table.Columns.Add("DemominacaoFuncoesGHE", Type.GetType("System.String"));
            table.Columns.Add("DescricaoLocalTrabalho", Type.GetType("System.String"));
            table.Columns.Add("IluminanciaMedia", Type.GetType("System.String"));
            table.Columns.Add("IluminanciaRecomendada", Type.GetType("System.String"));
            table.Columns.Add("UmidadeRelativaDoAr", Type.GetType("System.String"));
            table.Columns.Add("VelocidadeDoAr", Type.GetType("System.String"));
            table.Columns.Add("Temperatura", Type.GetType("System.String"));
            table.Columns.Add("TrabalhadoresExpostos", Type.GetType("System.String"));
            //Levantamento, Transporte e Descarga Individual de Materiais
            table.Columns.Add("Sim_17.2.2", Type.GetType("System.String"));
            table.Columns.Add("Nao_17.2.2", Type.GetType("System.String"));
            table.Columns.Add("Isento_17.2.2", Type.GetType("System.String"));
            table.Columns.Add("Sim_17.2.3", Type.GetType("System.String"));
            table.Columns.Add("Nao_17.2.3", Type.GetType("System.String"));
            table.Columns.Add("Isento_17.2.3", Type.GetType("System.String"));
            table.Columns.Add("Sim_17.2.4", Type.GetType("System.String"));
            table.Columns.Add("Nao_17.2.4", Type.GetType("System.String"));
            table.Columns.Add("Isento_17.2.4", Type.GetType("System.String"));
            table.Columns.Add("Sim_17.2.5", Type.GetType("System.String"));
            table.Columns.Add("Nao_17.2.5", Type.GetType("System.String"));
            table.Columns.Add("Isento_17.2.5", Type.GetType("System.String"));
            table.Columns.Add("Sim_17.2.6", Type.GetType("System.String"));
            table.Columns.Add("Nao_17.2.6", Type.GetType("System.String"));
            table.Columns.Add("Isento_17.2.6", Type.GetType("System.String"));
            table.Columns.Add("Sim_17.2.7", Type.GetType("System.String"));
            table.Columns.Add("Nao_17.2.7", Type.GetType("System.String"));
            table.Columns.Add("Isento_17.2.7", Type.GetType("System.String"));
            //Mobiliario e Equipamentos dos Postos de Trabalho
            table.Columns.Add("Sim_17.3.2a", Type.GetType("System.String"));
            table.Columns.Add("Nao_17.3.2a", Type.GetType("System.String"));
            table.Columns.Add("Isento_17.3.2a", Type.GetType("System.String"));
            table.Columns.Add("Sim_17.3.2bc", Type.GetType("System.String"));
            table.Columns.Add("Nao_17.3.2bc", Type.GetType("System.String"));
            table.Columns.Add("Isento_17.3.2bc", Type.GetType("System.String"));
            table.Columns.Add("Sim_17.3.1", Type.GetType("System.String"));
            table.Columns.Add("Nao_17.3.1", Type.GetType("System.String"));
            table.Columns.Add("Isento_17.3.1", Type.GetType("System.String"));
            table.Columns.Add("Sim_17.3.3ab", Type.GetType("System.String"));
            table.Columns.Add("Nao_17.3.3ab", Type.GetType("System.String"));
            table.Columns.Add("Isento_17.3.3ab", Type.GetType("System.String"));
            table.Columns.Add("Sim_17.3.3d", Type.GetType("System.String"));
            table.Columns.Add("Nao_17.3.3d", Type.GetType("System.String"));
            table.Columns.Add("Isento_17.3.3d", Type.GetType("System.String"));
            table.Columns.Add("Sim_17.3.5", Type.GetType("System.String"));
            table.Columns.Add("Nao_17.3.5", Type.GetType("System.String"));
            table.Columns.Add("Isento_17.3.5", Type.GetType("System.String"));
            //Condicoes Ambientais de Trabalho
            table.Columns.Add("Sim_17.4.2a", Type.GetType("System.String"));
            table.Columns.Add("Nao_17.4.2a", Type.GetType("System.String"));
            table.Columns.Add("Isento_17.4.2a", Type.GetType("System.String"));
            table.Columns.Add("Sim_17.4.2b", Type.GetType("System.String"));
            table.Columns.Add("Nao_17.4.2b", Type.GetType("System.String"));
            table.Columns.Add("Isento_17.4.2b", Type.GetType("System.String"));
            table.Columns.Add("Sim_17.4.3a", Type.GetType("System.String"));
            table.Columns.Add("Nao_17.4.3a", Type.GetType("System.String"));
            table.Columns.Add("Isento_17.4.3a", Type.GetType("System.String"));
            table.Columns.Add("Sim_17.4.3b", Type.GetType("System.String"));
            table.Columns.Add("Nao_17.4.3b", Type.GetType("System.String"));
            table.Columns.Add("Isento_17.4.3b", Type.GetType("System.String"));
            table.Columns.Add("Sim_17.4.3c", Type.GetType("System.String"));
            table.Columns.Add("Nao_17.4.3c", Type.GetType("System.String"));
            table.Columns.Add("Isento_17.4.3c", Type.GetType("System.String"));
            table.Columns.Add("Sim_17.4.3d", Type.GetType("System.String"));
            table.Columns.Add("Nao_17.4.3d", Type.GetType("System.String"));
            table.Columns.Add("Isento_17.4.3d", Type.GetType("System.String"));
            //Organizacao do Trabalho
            table.Columns.Add("Sim_17.6.3b", Type.GetType("System.String"));
            table.Columns.Add("Nao_17.6.3b", Type.GetType("System.String"));
            table.Columns.Add("Isento_17.6.3b", Type.GetType("System.String"));
            table.Columns.Add("Sim_17.6.3c", Type.GetType("System.String"));
            table.Columns.Add("Nao_17.6.3c", Type.GetType("System.String"));
            table.Columns.Add("Isento_17.6.3c", Type.GetType("System.String"));
            table.Columns.Add("Sim_17.6.4a", Type.GetType("System.String"));
            table.Columns.Add("Nao_17.6.4a", Type.GetType("System.String"));
            table.Columns.Add("Isento_17.6.4a", Type.GetType("System.String"));
            table.Columns.Add("Sim_17.6.4b", Type.GetType("System.String"));
            table.Columns.Add("Nao_17.6.4b", Type.GetType("System.String"));
            table.Columns.Add("Isento_17.6.4b", Type.GetType("System.String"));
            table.Columns.Add("Sim_17.6.4c", Type.GetType("System.String"));
            table.Columns.Add("Nao_17.6.4c", Type.GetType("System.String"));
            table.Columns.Add("Isento_17.6.4c", Type.GetType("System.String"));
            table.Columns.Add("Sim_17.6.4d", Type.GetType("System.String"));
            table.Columns.Add("Nao_17.6.4d", Type.GetType("System.String"));
            table.Columns.Add("Isento_17.6.4d", Type.GetType("System.String"));


            //adicionar novos campos, iniciando em 17.5.1 - depois adicionar no datasource do report
            table.Columns.Add("Sim_17.5.1", Type.GetType("System.String"));
            table.Columns.Add("Nao_17.5.1", Type.GetType("System.String"));
            table.Columns.Add("Isento_17.5.1", Type.GetType("System.String"));

            table.Columns.Add("Sim_17.5.1.1", Type.GetType("System.String"));
            table.Columns.Add("Nao_17.5.1.1", Type.GetType("System.String"));
            table.Columns.Add("Isento_17.5.1.1", Type.GetType("System.String"));

            table.Columns.Add("Sim_17.5.2a", Type.GetType("System.String"));
            table.Columns.Add("Nao_17.5.2a", Type.GetType("System.String"));
            table.Columns.Add("Isento_17.5.2a", Type.GetType("System.String"));

            table.Columns.Add("Sim_17.5.2b", Type.GetType("System.String"));
            table.Columns.Add("Nao_17.5.2b", Type.GetType("System.String"));
            table.Columns.Add("Isento_17.5.2b", Type.GetType("System.String"));

            table.Columns.Add("Sim_17.5.2.1", Type.GetType("System.String"));
            table.Columns.Add("Nao_17.5.2.1", Type.GetType("System.String"));
            table.Columns.Add("Isento_17.5.2.1", Type.GetType("System.String"));

            table.Columns.Add("Sim_17.5.3", Type.GetType("System.String"));
            table.Columns.Add("Nao_17.5.3", Type.GetType("System.String"));
            table.Columns.Add("Isento_17.5.3", Type.GetType("System.String"));

            table.Columns.Add("Sim_17.5.4a", Type.GetType("System.String"));
            table.Columns.Add("Nao_17.5.4a", Type.GetType("System.String"));
            table.Columns.Add("Isento_17.5.4a", Type.GetType("System.String"));

            table.Columns.Add("Sim_17.5.4b", Type.GetType("System.String"));
            table.Columns.Add("Nao_17.5.4b", Type.GetType("System.String"));
            table.Columns.Add("Isento_17.5.4b", Type.GetType("System.String"));

            table.Columns.Add("Sim_17.5.4c", Type.GetType("System.String"));
            table.Columns.Add("Nao_17.5.4c", Type.GetType("System.String"));
            table.Columns.Add("Isento_17.5.4c", Type.GetType("System.String"));

            table.Columns.Add("Sim_17.5.4d", Type.GetType("System.String"));
            table.Columns.Add("Nao_17.5.4d", Type.GetType("System.String"));
            table.Columns.Add("Isento_17.5.4d", Type.GetType("System.String"));

            table.Columns.Add("Sim_17.5.4e", Type.GetType("System.String"));
            table.Columns.Add("Nao_17.5.4e", Type.GetType("System.String"));
            table.Columns.Add("Isento_17.5.4e", Type.GetType("System.String"));

            table.Columns.Add("Sim_17.5.5", Type.GetType("System.String"));
            table.Columns.Add("Nao_17.5.5", Type.GetType("System.String"));
            table.Columns.Add("Isento_17.5.5", Type.GetType("System.String"));

            table.Columns.Add("Sim_17.6.1", Type.GetType("System.String"));
            table.Columns.Add("Nao_17.6.1", Type.GetType("System.String"));
            table.Columns.Add("Isento_17.6.1", Type.GetType("System.String"));

            table.Columns.Add("Sim_17.6.2", Type.GetType("System.String"));
            table.Columns.Add("Nao_17.6.2", Type.GetType("System.String"));
            table.Columns.Add("Isento_17.6.2", Type.GetType("System.String"));

            table.Columns.Add("Sim_17.6.3a", Type.GetType("System.String"));
            table.Columns.Add("Nao_17.6.3a", Type.GetType("System.String"));
            table.Columns.Add("Isento_17.6.3a", Type.GetType("System.String"));

            table.Columns.Add("Sim_17.6.3bx", Type.GetType("System.String"));
            table.Columns.Add("Nao_17.6.3bx", Type.GetType("System.String"));
            table.Columns.Add("Isento_17.6.3bx", Type.GetType("System.String"));

            table.Columns.Add("Sim_17.6.3cx", Type.GetType("System.String"));
            table.Columns.Add("Nao_17.6.3cx", Type.GetType("System.String"));
            table.Columns.Add("Isento_17.6.3cx", Type.GetType("System.String"));

            table.Columns.Add("Sim_17.6.3d", Type.GetType("System.String"));
            table.Columns.Add("Nao_17.6.3d", Type.GetType("System.String"));
            table.Columns.Add("Isento_17.6.3d", Type.GetType("System.String"));

            table.Columns.Add("Sim_17.6.3e", Type.GetType("System.String"));
            table.Columns.Add("Nao_17.6.3e", Type.GetType("System.String"));
            table.Columns.Add("Isento_17.6.3e", Type.GetType("System.String"));

            table.Columns.Add("Sim_17.6.5", Type.GetType("System.String"));
            table.Columns.Add("Nao_17.6.5", Type.GetType("System.String"));
            table.Columns.Add("Isento_17.6.5", Type.GetType("System.String"));

            table.Columns.Add("Sim_17.6.6a", Type.GetType("System.String"));
            table.Columns.Add("Nao_17.6.6a", Type.GetType("System.String"));
            table.Columns.Add("Isento_17.6.6a", Type.GetType("System.String"));

            table.Columns.Add("Sim_17.6.6b", Type.GetType("System.String"));
            table.Columns.Add("Nao_17.6.6b", Type.GetType("System.String"));
            table.Columns.Add("Isento_17.6.6b", Type.GetType("System.String"));

            table.Columns.Add("Sim_17.6.6c", Type.GetType("System.String"));
            table.Columns.Add("Nao_17.6.6c", Type.GetType("System.String"));
            table.Columns.Add("Isento_17.6.6c", Type.GetType("System.String"));

            table.Columns.Add("Sim_17.6.6d", Type.GetType("System.String"));
            table.Columns.Add("Nao_17.6.6d", Type.GetType("System.String"));
            table.Columns.Add("Isento_17.6.6d", Type.GetType("System.String"));

            table.Columns.Add("Sim_17.6.6dx", Type.GetType("System.String"));
            table.Columns.Add("Nao_17.6.6dx", Type.GetType("System.String"));
            table.Columns.Add("Isento_17.6.6dx", Type.GetType("System.String"));


            table.Columns.Add("Sim_17.6.7", Type.GetType("System.String"));
            table.Columns.Add("Nao_17.6.7", Type.GetType("System.String"));
            table.Columns.Add("Isento_17.6.7", Type.GetType("System.String"));


            table.Columns.Add("Sim_17.7.2.1", Type.GetType("System.String"));
            table.Columns.Add("Nao_17.7.2.1", Type.GetType("System.String"));
            table.Columns.Add("Isento_17.7.2.1", Type.GetType("System.String"));

            table.Columns.Add("Sim_17.7.3", Type.GetType("System.String"));
            table.Columns.Add("Nao_17.7.3", Type.GetType("System.String"));
            table.Columns.Add("Isento_17.7.3", Type.GetType("System.String"));

            table.Columns.Add("Sim_17.7.3.1", Type.GetType("System.String"));
            table.Columns.Add("Nao_17.7.3.1", Type.GetType("System.String"));
            table.Columns.Add("Isento_17.7.3.1", Type.GetType("System.String"));

            table.Columns.Add("Sim_17.7.3.2", Type.GetType("System.String"));
            table.Columns.Add("Nao_17.7.3.2", Type.GetType("System.String"));
            table.Columns.Add("Isento_17.7.3.2", Type.GetType("System.String"));

            table.Columns.Add("Sim_17.7.4", Type.GetType("System.String"));
            table.Columns.Add("Nao_17.7.4", Type.GetType("System.String"));
            table.Columns.Add("Isento_17.7.4", Type.GetType("System.String"));

            table.Columns.Add("Sim_17.7.5a", Type.GetType("System.String"));
            table.Columns.Add("Nao_17.7.5a", Type.GetType("System.String"));
            table.Columns.Add("Isento_17.7.5a", Type.GetType("System.String"));

            table.Columns.Add("Sim_17.7.5b", Type.GetType("System.String"));
            table.Columns.Add("Nao_17.7.5b", Type.GetType("System.String"));
            table.Columns.Add("Isento_17.7.5b", Type.GetType("System.String"));

            table.Columns.Add("Sim_17.7.6", Type.GetType("System.String"));
            table.Columns.Add("Nao_17.7.6", Type.GetType("System.String"));
            table.Columns.Add("Isento_17.7.6", Type.GetType("System.String"));

            table.Columns.Add("Sim_17.8.1", Type.GetType("System.String"));
            table.Columns.Add("Nao_17.8.1", Type.GetType("System.String"));
            table.Columns.Add("Isento_17.8.1", Type.GetType("System.String"));

            table.Columns.Add("Sim_17.8.2", Type.GetType("System.String"));
            table.Columns.Add("Nao_17.8.2", Type.GetType("System.String"));
            table.Columns.Add("Isento_17.8.2", Type.GetType("System.String"));

            table.Columns.Add("Sim_17.8.3", Type.GetType("System.String"));
            table.Columns.Add("Nao_17.8.3", Type.GetType("System.String"));
            table.Columns.Add("Isento_17.8.3", Type.GetType("System.String"));

            table.Columns.Add("Sim_17.8.4.1.1", Type.GetType("System.String"));
            table.Columns.Add("Nao_17.8.4.1.1", Type.GetType("System.String"));
            table.Columns.Add("Isento_17.8.4.1.1", Type.GetType("System.String"));

            table.Columns.Add("Sim_17.8.4.2", Type.GetType("System.String"));
            table.Columns.Add("Nao_17.8.4.2", Type.GetType("System.String"));
            table.Columns.Add("Isento_17.8.4.2", Type.GetType("System.String"));

            table.Columns.Add("Sim_17.8.4.2.1", Type.GetType("System.String"));
            table.Columns.Add("Nao_17.8.4.2.1", Type.GetType("System.String"));
            table.Columns.Add("Isento_17.8.4.2.1", Type.GetType("System.String"));
            

            table.Columns.Add("bImp_17_6_2", Type.GetType("System.String"));
            table.Columns.Add("tNormas_Producao", Type.GetType("System.String"));
            table.Columns.Add("tModo_Operacao", Type.GetType("System.String"));
            table.Columns.Add("tExigencia_Tempo", Type.GetType("System.String"));
            table.Columns.Add("tDeterminacao_Conteudo", Type.GetType("System.String"));
            table.Columns.Add("tRitmo_Trabalho", Type.GetType("System.String"));
            table.Columns.Add("tConteudo_Tarefa", Type.GetType("System.String"));


            table.Columns.Add("tRecomendacoes", Type.GetType("System.String"));
            table.Columns.Add("bImp_Recomendacoes", Type.GetType("System.String"));


            table.Columns.Add("Anexo1", Type.GetType("System.String"));
            table.Columns.Add("Descr1", Type.GetType("System.String"));
            table.Columns.Add("Anexo2", Type.GetType("System.String"));
            table.Columns.Add("Descr2", Type.GetType("System.String"));
            table.Columns.Add("Anexo3", Type.GetType("System.String"));
            table.Columns.Add("Descr3", Type.GetType("System.String"));
            table.Columns.Add("Anexo4", Type.GetType("System.String"));
            table.Columns.Add("Descr4", Type.GetType("System.String"));
            table.Columns.Add("Anexo5", Type.GetType("System.String"));
            table.Columns.Add("Descr5", Type.GetType("System.String"));
            table.Columns.Add("Base", Type.GetType("System.String"));

            return table;
        }

        private void PopularGabarito(DataRow newRow, ErgonomiaGhe ergonomiaGhe)
        {
            //Levantamento, Transporte e Descarga Individual de Materiais
            if (ergonomiaGhe.nID_NRs == 519)
            {
                newRow["Sim_17.2.2"] = RespNR17(ergonomiaGhe.nRSP, 1);
                newRow["Nao_17.2.2"] = RespNR17(ergonomiaGhe.nRSP, 2);
                newRow["Isento_17.2.2"] = RespNR17(ergonomiaGhe.nRSP, 3);
            }
            else if (ergonomiaGhe.nID_NRs == 520)
            {
                newRow["Sim_17.2.3"] = RespNR17(ergonomiaGhe.nRSP, 1);
                newRow["Nao_17.2.3"] = RespNR17(ergonomiaGhe.nRSP, 2);
                newRow["Isento_17.2.3"] = RespNR17(ergonomiaGhe.nRSP, 3);
            }
            else if (ergonomiaGhe.nID_NRs == 1712)
            {
                newRow["Sim_17.2.4"] = RespNR17(ergonomiaGhe.nRSP, 1);
                newRow["Nao_17.2.4"] = RespNR17(ergonomiaGhe.nRSP, 2);
                newRow["Isento_17.2.4"] = RespNR17(ergonomiaGhe.nRSP, 3);
            }
            else if (ergonomiaGhe.nID_NRs == 521)
            {
                newRow["Sim_17.2.5"] = RespNR17(ergonomiaGhe.nRSP, 1);
                newRow["Nao_17.2.5"] = RespNR17(ergonomiaGhe.nRSP, 2);
                newRow["Isento_17.2.5"] = RespNR17(ergonomiaGhe.nRSP, 3);
            }
            else if (ergonomiaGhe.nID_NRs == 522)
            {
                newRow["Sim_17.2.6"] = RespNR17(ergonomiaGhe.nRSP, 1);
                newRow["Nao_17.2.6"] = RespNR17(ergonomiaGhe.nRSP, 2);
                newRow["Isento_17.2.6"] = RespNR17(ergonomiaGhe.nRSP, 3);
            }
            else if (ergonomiaGhe.nID_NRs == 523)
            {
                newRow["Sim_17.2.7"] = RespNR17(ergonomiaGhe.nRSP, 1);
                newRow["Nao_17.2.7"] = RespNR17(ergonomiaGhe.nRSP, 2);
                newRow["Isento_17.2.7"] = RespNR17(ergonomiaGhe.nRSP, 3);
            }
            //Mobiliario e Equipamento dos postos de trabalho
            else if (ergonomiaGhe.nID_NRs == 525)
            {
                newRow["Sim_17.3.2a"] = RespNR17(ergonomiaGhe.nRSP, 1);
                newRow["Nao_17.3.2a"] = RespNR17(ergonomiaGhe.nRSP, 2);
                newRow["Isento_17.3.2a"] = RespNR17(ergonomiaGhe.nRSP, 3);
            }
            else if (ergonomiaGhe.nID_NRs == 526)
            {
                newRow["Sim_17.3.2bc"] = RespNR17(ergonomiaGhe.nRSP, 1);
                newRow["Nao_17.3.2bc"] = RespNR17(ergonomiaGhe.nRSP, 2);
                newRow["Isento_17.3.2bc"] = RespNR17(ergonomiaGhe.nRSP, 3);
            }
            else if (ergonomiaGhe.nID_NRs == 524)
            {
                newRow["Sim_17.3.1"] = RespNR17(ergonomiaGhe.nRSP, 1);
                newRow["Nao_17.3.1"] = RespNR17(ergonomiaGhe.nRSP, 2);
                newRow["Isento_17.3.1"] = RespNR17(ergonomiaGhe.nRSP, 3);
            }
            else if (ergonomiaGhe.nID_NRs == 529)
            {
                newRow["Sim_17.3.3ab"] = RespNR17(ergonomiaGhe.nRSP, 1);
                newRow["Nao_17.3.3ab"] = RespNR17(ergonomiaGhe.nRSP, 2);
                newRow["Isento_17.3.3ab"] = RespNR17(ergonomiaGhe.nRSP, 3);
            }
            else if (ergonomiaGhe.nID_NRs == 532)
            {
                newRow["Sim_17.3.3d"] = RespNR17(ergonomiaGhe.nRSP, 1);
                newRow["Nao_17.3.3d"] = RespNR17(ergonomiaGhe.nRSP, 2);
                newRow["Isento_17.3.3d"] = RespNR17(ergonomiaGhe.nRSP, 3);
            }
            else if (ergonomiaGhe.nID_NRs == 534)
            {
                newRow["Sim_17.3.5"] = RespNR17(ergonomiaGhe.nRSP, 1);
                newRow["Nao_17.3.5"] = RespNR17(ergonomiaGhe.nRSP, 2);
                newRow["Isento_17.3.5"] = RespNR17(ergonomiaGhe.nRSP, 3);
            }
            //Condicoes Ambientais de Trabalho
            else if (ergonomiaGhe.nID_NRs == 535)
            {
                newRow["Sim_17.4.2a"] = RespNR17(ergonomiaGhe.nRSP, 1);
                newRow["Nao_17.4.2a"] = RespNR17(ergonomiaGhe.nRSP, 2);
                newRow["Isento_17.4.2a"] = RespNR17(ergonomiaGhe.nRSP, 3);
            }
            else if (ergonomiaGhe.nID_NRs == 536)
            {
                newRow["Sim_17.4.2b"] = RespNR17(ergonomiaGhe.nRSP, 1);
                newRow["Nao_17.4.2b"] = RespNR17(ergonomiaGhe.nRSP, 2);
                newRow["Isento_17.4.2b"] = RespNR17(ergonomiaGhe.nRSP, 3);
            }
            else if (ergonomiaGhe.nID_NRs == 537)
            {
                newRow["Sim_17.4.3a"] = RespNR17(ergonomiaGhe.nRSP, 1);
                newRow["Nao_17.4.3a"] = RespNR17(ergonomiaGhe.nRSP, 2);
                newRow["Isento_17.4.3a"] = RespNR17(ergonomiaGhe.nRSP, 3);
            }
            else if (ergonomiaGhe.nID_NRs == 538)
            {
                newRow["Sim_17.4.3b"] = RespNR17(ergonomiaGhe.nRSP, 1);
                newRow["Nao_17.4.3b"] = RespNR17(ergonomiaGhe.nRSP, 2);
                newRow["Isento_17.4.3b"] = RespNR17(ergonomiaGhe.nRSP, 3);
            }
            else if (ergonomiaGhe.nID_NRs == 539)
            {
                newRow["Sim_17.4.3c"] = RespNR17(ergonomiaGhe.nRSP, 1);
                newRow["Nao_17.4.3c"] = RespNR17(ergonomiaGhe.nRSP, 2);
                newRow["Isento_17.4.3c"] = RespNR17(ergonomiaGhe.nRSP, 3);
            }
            else if (ergonomiaGhe.nID_NRs == 540)
            {
                newRow["Sim_17.4.3d"] = RespNR17(ergonomiaGhe.nRSP, 1);
                newRow["Nao_17.4.3d"] = RespNR17(ergonomiaGhe.nRSP, 2);
                newRow["Isento_17.4.3d"] = RespNR17(ergonomiaGhe.nRSP, 3);
            }
            //Organizacao do Trabalho
            else if (ergonomiaGhe.nID_NRs == 548)
            {
                newRow["Sim_17.6.3b"] = RespNR17(ergonomiaGhe.nRSP, 1);
                newRow["Nao_17.6.3b"] = RespNR17(ergonomiaGhe.nRSP, 2);
                newRow["Isento_17.6.3b"] = RespNR17(ergonomiaGhe.nRSP, 3);
            }
            else if (ergonomiaGhe.nID_NRs == 549)
            {
                newRow["Sim_17.6.3c"] = RespNR17(ergonomiaGhe.nRSP, 1);
                newRow["Nao_17.6.3c"] = RespNR17(ergonomiaGhe.nRSP, 2);
                newRow["Isento_17.6.3c"] = RespNR17(ergonomiaGhe.nRSP, 3);
            }
            else if (ergonomiaGhe.nID_NRs == 550)
            {
                newRow["Sim_17.6.4a"] = RespNR17(ergonomiaGhe.nRSP, 1);
                newRow["Nao_17.6.4a"] = RespNR17(ergonomiaGhe.nRSP, 2);
                newRow["Isento_17.6.4a"] = RespNR17(ergonomiaGhe.nRSP, 3);
            }
            else if (ergonomiaGhe.nID_NRs == 551)
            {
                newRow["Sim_17.6.4b"] = RespNR17(ergonomiaGhe.nRSP, 1);
                newRow["Nao_17.6.4b"] = RespNR17(ergonomiaGhe.nRSP, 2);
                newRow["Isento_17.6.4b"] = RespNR17(ergonomiaGhe.nRSP, 3);
            }
            else if (ergonomiaGhe.nID_NRs == 552)
            {
                newRow["Sim_17.6.4c"] = RespNR17(ergonomiaGhe.nRSP, 1);
                newRow["Nao_17.6.4c"] = RespNR17(ergonomiaGhe.nRSP, 2);
                newRow["Isento_17.6.4c"] = RespNR17(ergonomiaGhe.nRSP, 3);
            }
            else if (ergonomiaGhe.nID_NRs == 553)
            {
                newRow["Sim_17.6.4d"] = RespNR17(ergonomiaGhe.nRSP, 1);
                newRow["Nao_17.6.4d"] = RespNR17(ergonomiaGhe.nRSP, 2);
                newRow["Isento_17.6.4d"] = RespNR17(ergonomiaGhe.nRSP, 3);
            }
        }


        private void PopularGabarito_New(DataRow newRow, ErgonomiaGhe_New ergonomiaGhe)
        {
            //Levantamento, Transporte e Descarga Individual de Materiais
            if (ergonomiaGhe.nID_NRs == 1)
            {
                newRow["Sim_17.2.2"] = RespNR17(ergonomiaGhe.nRSP, 1);
                newRow["Nao_17.2.2"] = RespNR17(ergonomiaGhe.nRSP, 2);
                newRow["Isento_17.2.2"] = RespNR17(ergonomiaGhe.nRSP, 3);
            }
            else if (ergonomiaGhe.nID_NRs == 2)
            {
                newRow["Sim_17.2.3"] = RespNR17(ergonomiaGhe.nRSP, 1);
                newRow["Nao_17.2.3"] = RespNR17(ergonomiaGhe.nRSP, 2);
                newRow["Isento_17.2.3"] = RespNR17(ergonomiaGhe.nRSP, 3);
            }
            else if (ergonomiaGhe.nID_NRs == 3)
            {
                newRow["Sim_17.2.4"] = RespNR17(ergonomiaGhe.nRSP, 1);
                newRow["Nao_17.2.4"] = RespNR17(ergonomiaGhe.nRSP, 2);
                newRow["Isento_17.2.4"] = RespNR17(ergonomiaGhe.nRSP, 3);
            }
            else if (ergonomiaGhe.nID_NRs == 4)
            {
                newRow["Sim_17.2.5"] = RespNR17(ergonomiaGhe.nRSP, 1);
                newRow["Nao_17.2.5"] = RespNR17(ergonomiaGhe.nRSP, 2);
                newRow["Isento_17.2.5"] = RespNR17(ergonomiaGhe.nRSP, 3);
            }
            else if (ergonomiaGhe.nID_NRs == 5)
            {
                newRow["Sim_17.2.6"] = RespNR17(ergonomiaGhe.nRSP, 1);
                newRow["Nao_17.2.6"] = RespNR17(ergonomiaGhe.nRSP, 2);
                newRow["Isento_17.2.6"] = RespNR17(ergonomiaGhe.nRSP, 3);
            }
            else if (ergonomiaGhe.nID_NRs == 6)
            {
                newRow["Sim_17.2.7"] = RespNR17(ergonomiaGhe.nRSP, 1);
                newRow["Nao_17.2.7"] = RespNR17(ergonomiaGhe.nRSP, 2);
                newRow["Isento_17.2.7"] = RespNR17(ergonomiaGhe.nRSP, 3);
            }
            //Mobiliario e Equipamento dos postos de trabalho
            else if (ergonomiaGhe.nID_NRs == 7)
            {
                newRow["Sim_17.3.2a"] = RespNR17(ergonomiaGhe.nRSP, 1);
                newRow["Nao_17.3.2a"] = RespNR17(ergonomiaGhe.nRSP, 2);
                newRow["Isento_17.3.2a"] = RespNR17(ergonomiaGhe.nRSP, 3);
            }
            else if (ergonomiaGhe.nID_NRs == 8)
            {
                newRow["Sim_17.3.2bc"] = RespNR17(ergonomiaGhe.nRSP, 1);
                newRow["Nao_17.3.2bc"] = RespNR17(ergonomiaGhe.nRSP, 2);
                newRow["Isento_17.3.2bc"] = RespNR17(ergonomiaGhe.nRSP, 3);
            }
            else if (ergonomiaGhe.nID_NRs == 9)
            {
                newRow["Sim_17.3.1"] = RespNR17(ergonomiaGhe.nRSP, 1);
                newRow["Nao_17.3.1"] = RespNR17(ergonomiaGhe.nRSP, 2);
                newRow["Isento_17.3.1"] = RespNR17(ergonomiaGhe.nRSP, 3);
            }
            else if (ergonomiaGhe.nID_NRs == 10)
            {
                newRow["Sim_17.3.3ab"] = RespNR17(ergonomiaGhe.nRSP, 1);
                newRow["Nao_17.3.3ab"] = RespNR17(ergonomiaGhe.nRSP, 2);
                newRow["Isento_17.3.3ab"] = RespNR17(ergonomiaGhe.nRSP, 3);
            }
            else if (ergonomiaGhe.nID_NRs == 11)
            {
                newRow["Sim_17.3.3d"] = RespNR17(ergonomiaGhe.nRSP, 1);
                newRow["Nao_17.3.3d"] = RespNR17(ergonomiaGhe.nRSP, 2);
                newRow["Isento_17.3.3d"] = RespNR17(ergonomiaGhe.nRSP, 3);
            }
            else if (ergonomiaGhe.nID_NRs == 12)
            {
                newRow["Sim_17.3.5"] = RespNR17(ergonomiaGhe.nRSP, 1);
                newRow["Nao_17.3.5"] = RespNR17(ergonomiaGhe.nRSP, 2);
                newRow["Isento_17.3.5"] = RespNR17(ergonomiaGhe.nRSP, 3);
            }
            //Condicoes Ambientais de Trabalho
            else if (ergonomiaGhe.nID_NRs == 13)
            {
                newRow["Sim_17.4.2a"] = RespNR17(ergonomiaGhe.nRSP, 1);
                newRow["Nao_17.4.2a"] = RespNR17(ergonomiaGhe.nRSP, 2);
                newRow["Isento_17.4.2a"] = RespNR17(ergonomiaGhe.nRSP, 3);
            }
            else if (ergonomiaGhe.nID_NRs == 14)
            {
                newRow["Sim_17.4.2b"] = RespNR17(ergonomiaGhe.nRSP, 1);
                newRow["Nao_17.4.2b"] = RespNR17(ergonomiaGhe.nRSP, 2);
                newRow["Isento_17.4.2b"] = RespNR17(ergonomiaGhe.nRSP, 3);
            }
            else if (ergonomiaGhe.nID_NRs == 15)
            {
                newRow["Sim_17.4.3a"] = RespNR17(ergonomiaGhe.nRSP, 1);
                newRow["Nao_17.4.3a"] = RespNR17(ergonomiaGhe.nRSP, 2);
                newRow["Isento_17.4.3a"] = RespNR17(ergonomiaGhe.nRSP, 3);
            }
            else if (ergonomiaGhe.nID_NRs == 16)
            {
                newRow["Sim_17.4.3b"] = RespNR17(ergonomiaGhe.nRSP, 1);
                newRow["Nao_17.4.3b"] = RespNR17(ergonomiaGhe.nRSP, 2);
                newRow["Isento_17.4.3b"] = RespNR17(ergonomiaGhe.nRSP, 3);
            }
            else if (ergonomiaGhe.nID_NRs == 17)
            {
                newRow["Sim_17.4.3c"] = RespNR17(ergonomiaGhe.nRSP, 1);
                newRow["Nao_17.4.3c"] = RespNR17(ergonomiaGhe.nRSP, 2);
                newRow["Isento_17.4.3c"] = RespNR17(ergonomiaGhe.nRSP, 3);
            }
            else if (ergonomiaGhe.nID_NRs == 18)
            {
                newRow["Sim_17.4.3d"] = RespNR17(ergonomiaGhe.nRSP, 1);
                newRow["Nao_17.4.3d"] = RespNR17(ergonomiaGhe.nRSP, 2);
                newRow["Isento_17.4.3d"] = RespNR17(ergonomiaGhe.nRSP, 3);
            }
            //Organizacao do Trabalho
            else if (ergonomiaGhe.nID_NRs == 19)
            {
                newRow["Sim_17.6.3b"] = RespNR17(ergonomiaGhe.nRSP, 1);
                newRow["Nao_17.6.3b"] = RespNR17(ergonomiaGhe.nRSP, 2);
                newRow["Isento_17.6.3b"] = RespNR17(ergonomiaGhe.nRSP, 3);
            }
            else if (ergonomiaGhe.nID_NRs == 20)
            {
                newRow["Sim_17.6.4b"] = RespNR17(ergonomiaGhe.nRSP, 1);
                newRow["Nao_17.6.4b"] = RespNR17(ergonomiaGhe.nRSP, 2);
                newRow["Isento_17.6.4b"] = RespNR17(ergonomiaGhe.nRSP, 3);
            }
            else if (ergonomiaGhe.nID_NRs == 21)
            {
                newRow["Sim_17.5.1"] = RespNR17(ergonomiaGhe.nRSP, 1);
                newRow["Nao_17.5.1"] = RespNR17(ergonomiaGhe.nRSP, 2);
                newRow["Isento_17.5.1"] = RespNR17(ergonomiaGhe.nRSP, 3);
            }
            else if (ergonomiaGhe.nID_NRs == 22)
            {
                newRow["Sim_17.5.1.1"] = RespNR17(ergonomiaGhe.nRSP, 1);
                newRow["Nao_17.5.1.1"] = RespNR17(ergonomiaGhe.nRSP, 2);
                newRow["Isento_17.5.1.1"] = RespNR17(ergonomiaGhe.nRSP, 3);
            }
            else if (ergonomiaGhe.nID_NRs == 23)
            {
                newRow["Sim_17.5.2a"] = RespNR17(ergonomiaGhe.nRSP, 1);
                newRow["Nao_17.5.2a"] = RespNR17(ergonomiaGhe.nRSP, 2);
                newRow["Isento_17.5.2a"] = RespNR17(ergonomiaGhe.nRSP, 3);
            }
            else if (ergonomiaGhe.nID_NRs == 24)
            {
                newRow["Sim_17.5.2b"] = RespNR17(ergonomiaGhe.nRSP, 1);
                newRow["Nao_17.5.2b"] = RespNR17(ergonomiaGhe.nRSP, 2);
                newRow["Isento_17.5.2b"] = RespNR17(ergonomiaGhe.nRSP, 3);
            }
            else if (ergonomiaGhe.nID_NRs == 25)
            {
                newRow["Sim_17.5.2.1"] = RespNR17(ergonomiaGhe.nRSP, 1);
                newRow["Nao_17.5.2.1"] = RespNR17(ergonomiaGhe.nRSP, 2);
                newRow["Isento_17.5.2.1"] = RespNR17(ergonomiaGhe.nRSP, 3);
            }
            else if (ergonomiaGhe.nID_NRs == 26)
            {
                newRow["Sim_17.5.3"] = RespNR17(ergonomiaGhe.nRSP, 1);
                newRow["Nao_17.5.3"] = RespNR17(ergonomiaGhe.nRSP, 2);
                newRow["Isento_17.5.3"] = RespNR17(ergonomiaGhe.nRSP, 3);
            }
            else if (ergonomiaGhe.nID_NRs == 27)
            {
                newRow["Sim_17.5.4a"] = RespNR17(ergonomiaGhe.nRSP, 1);
                newRow["Nao_17.5.4a"] = RespNR17(ergonomiaGhe.nRSP, 2);
                newRow["Isento_17.5.4a"] = RespNR17(ergonomiaGhe.nRSP, 3);
            }
            else if (ergonomiaGhe.nID_NRs == 28)
            {
                newRow["Sim_17.5.4b"] = RespNR17(ergonomiaGhe.nRSP, 1);
                newRow["Nao_17.5.4b"] = RespNR17(ergonomiaGhe.nRSP, 2);
                newRow["Isento_17.5.4b"] = RespNR17(ergonomiaGhe.nRSP, 3);
            }
            else if (ergonomiaGhe.nID_NRs == 29)
            {
                newRow["Sim_17.5.4c"] = RespNR17(ergonomiaGhe.nRSP, 1);
                newRow["Nao_17.5.4c"] = RespNR17(ergonomiaGhe.nRSP, 2);
                newRow["Isento_17.5.4c"] = RespNR17(ergonomiaGhe.nRSP, 3);
            }
            else if (ergonomiaGhe.nID_NRs == 30)
            {
                newRow["Sim_17.5.4d"] = RespNR17(ergonomiaGhe.nRSP, 1);
                newRow["Nao_17.5.4d"] = RespNR17(ergonomiaGhe.nRSP, 2);
                newRow["Isento_17.5.4d"] = RespNR17(ergonomiaGhe.nRSP, 3);
            }
            else if (ergonomiaGhe.nID_NRs == 31)
            {
                newRow["Sim_17.5.4e"] = RespNR17(ergonomiaGhe.nRSP, 1);
                newRow["Nao_17.5.4e"] = RespNR17(ergonomiaGhe.nRSP, 2);
                newRow["Isento_17.5.4e"] = RespNR17(ergonomiaGhe.nRSP, 3);
            }
            else if (ergonomiaGhe.nID_NRs == 32)
            {
                newRow["Sim_17.5.5"] = RespNR17(ergonomiaGhe.nRSP, 1);
                newRow["Nao_17.5.5"] = RespNR17(ergonomiaGhe.nRSP, 2);
                newRow["Isento_17.5.5"] = RespNR17(ergonomiaGhe.nRSP, 3);
            }
            else if (ergonomiaGhe.nID_NRs == 33)
            {
                newRow["Sim_17.6.1"] = RespNR17(ergonomiaGhe.nRSP, 1);
                newRow["Nao_17.6.1"] = RespNR17(ergonomiaGhe.nRSP, 2);
                newRow["Isento_17.6.1"] = RespNR17(ergonomiaGhe.nRSP, 3);
            }
            else if (ergonomiaGhe.nID_NRs == 34)
            {
                newRow["Sim_17.6.2"] = RespNR17(ergonomiaGhe.nRSP, 1);
                newRow["Nao_17.6.2"] = RespNR17(ergonomiaGhe.nRSP, 2);
                newRow["Isento_17.6.2"] = RespNR17(ergonomiaGhe.nRSP, 3);
            }
            else if (ergonomiaGhe.nID_NRs == 35)
            {
                newRow["Sim_17.6.3a"] = RespNR17(ergonomiaGhe.nRSP, 1);
                newRow["Nao_17.6.3a"] = RespNR17(ergonomiaGhe.nRSP, 2);
                newRow["Isento_17.6.3a"] = RespNR17(ergonomiaGhe.nRSP, 3);
            }
            else if (ergonomiaGhe.nID_NRs == 36)
            {
                newRow["Sim_17.6.3bx"] = RespNR17(ergonomiaGhe.nRSP, 1);
                newRow["Nao_17.6.3bx"] = RespNR17(ergonomiaGhe.nRSP, 2);
                newRow["Isento_17.6.3bx"] = RespNR17(ergonomiaGhe.nRSP, 3);
            }
            else if (ergonomiaGhe.nID_NRs == 37)
            {
                newRow["Sim_17.6.3cx"] = RespNR17(ergonomiaGhe.nRSP, 1);
                newRow["Nao_17.6.3cx"] = RespNR17(ergonomiaGhe.nRSP, 2);
                newRow["Isento_17.6.3cx"] = RespNR17(ergonomiaGhe.nRSP, 3);
            }
            else if (ergonomiaGhe.nID_NRs == 38)
            {
                newRow["Sim_17.6.3d"] = RespNR17(ergonomiaGhe.nRSP, 1);
                newRow["Nao_17.6.3d"] = RespNR17(ergonomiaGhe.nRSP, 2);
                newRow["Isento_17.6.3d"] = RespNR17(ergonomiaGhe.nRSP, 3);
            }
            else if (ergonomiaGhe.nID_NRs == 39)
            {
                newRow["Sim_17.6.3e"] = RespNR17(ergonomiaGhe.nRSP, 1);
                newRow["Nao_17.6.3e"] = RespNR17(ergonomiaGhe.nRSP, 2);
                newRow["Isento_17.6.3e"] = RespNR17(ergonomiaGhe.nRSP, 3);
            }
            else if (ergonomiaGhe.nID_NRs == 40)
            {
                newRow["Sim_17.6.5"] = RespNR17(ergonomiaGhe.nRSP, 1);
                newRow["Nao_17.6.5"] = RespNR17(ergonomiaGhe.nRSP, 2);
                newRow["Isento_17.6.5"] = RespNR17(ergonomiaGhe.nRSP, 3);
            }
            else if (ergonomiaGhe.nID_NRs == 41)
            {
                newRow["Sim_17.6.6a"] = RespNR17(ergonomiaGhe.nRSP, 1);
                newRow["Nao_17.6.6a"] = RespNR17(ergonomiaGhe.nRSP, 2);
                newRow["Isento_17.6.6a"] = RespNR17(ergonomiaGhe.nRSP, 3);
            }
            else if (ergonomiaGhe.nID_NRs == 42)
            {
                newRow["Sim_17.6.6b"] = RespNR17(ergonomiaGhe.nRSP, 1);
                newRow["Nao_17.6.6b"] = RespNR17(ergonomiaGhe.nRSP, 2);
                newRow["Isento_17.6.6b"] = RespNR17(ergonomiaGhe.nRSP, 3);
            }
            else if (ergonomiaGhe.nID_NRs == 43)
            {
                newRow["Sim_17.6.6c"] = RespNR17(ergonomiaGhe.nRSP, 1);
                newRow["Nao_17.6.6c"] = RespNR17(ergonomiaGhe.nRSP, 2);
                newRow["Isento_17.6.6c"] = RespNR17(ergonomiaGhe.nRSP, 3);
            }
            else if (ergonomiaGhe.nID_NRs == 44)
            {
                newRow["Sim_17.6.6dx"] = RespNR17(ergonomiaGhe.nRSP, 1);
                newRow["Nao_17.6.6dx"] = RespNR17(ergonomiaGhe.nRSP, 2);
                newRow["Isento_17.6.6dx"] = RespNR17(ergonomiaGhe.nRSP, 3);
            }
            else if (ergonomiaGhe.nID_NRs == 45)
            {
                newRow["Sim_17.6.6d"] = RespNR17(ergonomiaGhe.nRSP, 1);
                newRow["Nao_17.6.6d"] = RespNR17(ergonomiaGhe.nRSP, 2);
                newRow["Isento_17.6.6d"] = RespNR17(ergonomiaGhe.nRSP, 3);
            }
            else if (ergonomiaGhe.nID_NRs == 46)
            {
                newRow["Sim_17.6.7"] = RespNR17(ergonomiaGhe.nRSP, 1);
                newRow["Nao_17.6.7"] = RespNR17(ergonomiaGhe.nRSP, 2);
                newRow["Isento_17.6.7"] = RespNR17(ergonomiaGhe.nRSP, 3);
            }
            else if (ergonomiaGhe.nID_NRs == 47)
            {
                newRow["Sim_17.7.2.1"] = RespNR17(ergonomiaGhe.nRSP, 1);
                newRow["Nao_17.7.2.1"] = RespNR17(ergonomiaGhe.nRSP, 2);
                newRow["Isento_17.7.2.1"] = RespNR17(ergonomiaGhe.nRSP, 3);
            }
            else if (ergonomiaGhe.nID_NRs == 48)
            {
                newRow["Sim_17.7.3"] = RespNR17(ergonomiaGhe.nRSP, 1);
                newRow["Nao_17.7.3"] = RespNR17(ergonomiaGhe.nRSP, 2);
                newRow["Isento_17.7.3"] = RespNR17(ergonomiaGhe.nRSP, 3);
            }
            else if (ergonomiaGhe.nID_NRs == 49)
            {
                newRow["Sim_17.7.3.1"] = RespNR17(ergonomiaGhe.nRSP, 1);
                newRow["Nao_17.7.3.1"] = RespNR17(ergonomiaGhe.nRSP, 2);
                newRow["Isento_17.7.3.1"] = RespNR17(ergonomiaGhe.nRSP, 3);
            }
            else if (ergonomiaGhe.nID_NRs == 50)
            {
                newRow["Sim_17.7.3.2"] = RespNR17(ergonomiaGhe.nRSP, 1);
                newRow["Nao_17.7.3.2"] = RespNR17(ergonomiaGhe.nRSP, 2);
                newRow["Isento_17.7.3.2"] = RespNR17(ergonomiaGhe.nRSP, 3);
            }
            else if (ergonomiaGhe.nID_NRs == 51)
            {
                newRow["Sim_17.7.4"] = RespNR17(ergonomiaGhe.nRSP, 1);
                newRow["Nao_17.7.4"] = RespNR17(ergonomiaGhe.nRSP, 2);
                newRow["Isento_17.7.4"] = RespNR17(ergonomiaGhe.nRSP, 3);
            }
            else if (ergonomiaGhe.nID_NRs == 52)
            {
                newRow["Sim_17.7.5a"] = RespNR17(ergonomiaGhe.nRSP, 1);
                newRow["Nao_17.7.5a"] = RespNR17(ergonomiaGhe.nRSP, 2);
                newRow["Isento_17.7.5a"] = RespNR17(ergonomiaGhe.nRSP, 3);
            }
            else if (ergonomiaGhe.nID_NRs == 53)
            {
                newRow["Sim_17.7.5b"] = RespNR17(ergonomiaGhe.nRSP, 1);
                newRow["Nao_17.7.5b"] = RespNR17(ergonomiaGhe.nRSP, 2);
                newRow["Isento_17.7.5b"] = RespNR17(ergonomiaGhe.nRSP, 3);
            }
            else if (ergonomiaGhe.nID_NRs == 54)
            {
                newRow["Sim_17.7.6"] = RespNR17(ergonomiaGhe.nRSP, 1);
                newRow["Nao_17.7.6"] = RespNR17(ergonomiaGhe.nRSP, 2);
                newRow["Isento_17.7.6"] = RespNR17(ergonomiaGhe.nRSP, 3);
            }
            else if (ergonomiaGhe.nID_NRs == 55)
            {
                newRow["Sim_17.8.1"] = RespNR17(ergonomiaGhe.nRSP, 1);
                newRow["Nao_17.8.1"] = RespNR17(ergonomiaGhe.nRSP, 2);
                newRow["Isento_17.8.1"] = RespNR17(ergonomiaGhe.nRSP, 3);
            }
            else if (ergonomiaGhe.nID_NRs == 56)
            {
                newRow["Sim_17.8.2"] = RespNR17(ergonomiaGhe.nRSP, 1);
                newRow["Nao_17.8.2"] = RespNR17(ergonomiaGhe.nRSP, 2);
                newRow["Isento_17.8.2"] = RespNR17(ergonomiaGhe.nRSP, 3);
            }
            else if (ergonomiaGhe.nID_NRs == 57)
            {
                newRow["Sim_17.8.3"] = RespNR17(ergonomiaGhe.nRSP, 1);
                newRow["Nao_17.8.3"] = RespNR17(ergonomiaGhe.nRSP, 2);
                newRow["Isento_17.8.3"] = RespNR17(ergonomiaGhe.nRSP, 3);
            }
            else if (ergonomiaGhe.nID_NRs == 58)
            {
                newRow["Sim_17.8.4.1.1"] = RespNR17(ergonomiaGhe.nRSP, 1);
                newRow["Nao_17.8.4.1.1"] = RespNR17(ergonomiaGhe.nRSP, 2);
                newRow["Isento_17.8.4.1.1"] = RespNR17(ergonomiaGhe.nRSP, 3);
            }
            else if (ergonomiaGhe.nID_NRs == 59)
            {
                newRow["Sim_17.8.4.2"] = RespNR17(ergonomiaGhe.nRSP, 1);
                newRow["Nao_17.8.4.2"] = RespNR17(ergonomiaGhe.nRSP, 2);
                newRow["Isento_17.8.4.2"] = RespNR17(ergonomiaGhe.nRSP, 3);
            }
            else if (ergonomiaGhe.nID_NRs == 60)
            {
                newRow["Sim_17.8.4.2.1"] = RespNR17(ergonomiaGhe.nRSP, 1);
                newRow["Nao_17.8.4.2.1"] = RespNR17(ergonomiaGhe.nRSP, 2);
                newRow["Isento_17.8.4.2.1"] = RespNR17(ergonomiaGhe.nRSP, 3);
            }




        }


        private DataSet GetPrestador(Prestador prestador, Prestador prestador2)
        {
            DataTable table = new DataTable("Result");
            table.Columns.Add("IdPrestador", Type.GetType("System.Int32"));
            table.Columns.Add("Nome", Type.GetType("System.String"));
            table.Columns.Add("Numero", Type.GetType("System.String"));
            table.Columns.Add("Titulo", Type.GetType("System.String"));
            table.Columns.Add("Contato", Type.GetType("System.String"));
            table.Columns.Add("DataAssinatura", Type.GetType("System.String"));
            table.Columns.Add("iDocumento", Type.GetType("System.Byte[]"));
            table.Columns.Add("iAssinatura", Type.GetType("System.Byte[]"));
            table.Columns.Add("Nome2", Type.GetType("System.String"));
            table.Columns.Add("Numero2", Type.GetType("System.String"));
            table.Columns.Add("Titulo2", Type.GetType("System.String"));
            table.Columns.Add("iAssinatura2", Type.GetType("System.Byte[]"));

            DataSet ds = new DataSet();
            ds.Tables.Add(table);
            
            DataRow newRow = ds.Tables[0].NewRow();

            if (prestador.mirrorOld == null)
                prestador.Find();

            if (laudoTecnico.nID_EMPR.IdCNAE == null)
                laudoTecnico.nID_EMPR.Find();

            Endereco endereco = laudoTecnico.nID_EMPR.GetEndereco();

            newRow["IdPrestador"] = prestador.Id;
            newRow["DataAssinatura"] = endereco.GetCidade() + ", " + laudoTecnico.hDT_LAUDO.ToString("dd \"de\" MMMM \"de\" yyyy") + ".";
            newRow["Nome"] = prestador.NomeCompleto;
            newRow["Numero"] = prestador.Numero;
            newRow["Titulo"] = prestador.Titulo;
            newRow["Contato"] = prestador.Contato;

            try
            {
                newRow["iDocumento"] = Ilitera.Common.Fotos.GetByteFoto_Uri(prestador.FotoRG);
            }
            catch { }

            newRow["Nome2"] = prestador2.NomeCompleto;
            newRow["Numero2"] = prestador2.Numero;
            newRow["Titulo2"] = prestador2.Titulo;


            try
            {
                newRow["iAssinatura2"] = Ilitera.Common.Fotos.GetByteFoto_Uri(prestador2.FotoAss);
            }
            catch { }

            

            try
            {
                newRow["iAssinatura"] = Ilitera.Common.Fotos.GetByteFoto_Uri(prestador.FotoAss);
            }
            catch { }

            ds.Tables[0].Rows.Add(newRow);

            return ds;
        }

        private string RespNR17(int resp, int opcao)
        {
            string resposta = "";

            if (resp == opcao)
                resposta = "X";

            return resposta;
        }

        private DataSet DataSourceAssinatura()
        {
            DataTable table = new DataTable("Result");
            table.Columns.Add("iAssinatura", Type.GetType("System.Byte[]"));
            
            DataSet ds = new DataSet();
            ds.Tables.Add(table);
            
            DataRow newRow = ds.Tables[0].NewRow();
            
            if(laudoTecnico.PrestadorID.mirrorOld==null)
                laudoTecnico.PrestadorID.Find();
            
            if (laudoTecnico.PrestadorID.FotoAss != string.Empty)
                newRow["iAssinatura"] = Ilitera.Common.Fotos.GetByteFoto_Uri(laudoTecnico.PrestadorID.FotoAss);
           
            ds.Tables[0].Rows.Add(newRow);
            
            return ds;
        }

		private DataSet DataSourceFotoFachada()
		{
			DataSet ds = new DataSet();
			DataTable table = new DataTable("Result");
			table.Columns.Add("FotoFachada", Type.GetType("System.Byte[]"));
			ds.Tables.Add(table);
			DataRow newRow;
			newRow = ds.Tables[0].NewRow();
			try
			{
				newRow["FotoFachada"] = Ilitera.Common.Fotos.GetByteFoto_Uri(cliente.GetFotoFachada());
			}
			catch (Exception ex)
			{
                System.Diagnostics.Debug.WriteLine(ex.Message);
                //System.Diagnostics.Trace.WriteLine(ex.Message);
			}
			ds.Tables[0].Rows.Add(newRow);
			return ds;
		}
	}
}
