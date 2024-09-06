using System;
using Ilitera.Opsa.Data;
using System.Data;
using Ilitera.Common;
using System.Collections;
using System.Text;

namespace Ilitera.Sied.Report
{
	public class DataSourcePPRA_NR20 : DataSourceBase
	{
		private Cliente cliente;
        private LaudoTecnico laudoTecnico;
        private string ListaGHEs;
        private Int32 zObra;
        private bool Inibir_US;
		
		public DataSourcePPRA_NR20(Cliente cliente)
		{
			this.cliente = cliente;
            this.ListaGHEs = "";
            this.zObra = 0;
            this.Inibir_US = false;
           
            if (this.cliente.mirrorOld == null)
			    this.cliente.Find();
            
            this.laudoTecnico = LaudoTecnico.GetUltimoLaudo(this.cliente.Id);
		}

		public DataSourcePPRA_NR20(LaudoTecnico laudoTecnico)
		{
			this.laudoTecnico = laudoTecnico;
            this.cliente = this.laudoTecnico.nID_EMPR;
            this.ListaGHEs = "";
            this.zObra = 0;
            this.Inibir_US = false;
           
            if(this.cliente.mirrorOld==null)
                this.cliente.Find();
		}


        public DataSourcePPRA_NR20(LaudoTecnico laudoTecnico, string xListaGHE, Int32 xObra, bool xInibir_US)
        {
            this.laudoTecnico = laudoTecnico;
            this.cliente = this.laudoTecnico.nID_EMPR;
            this.ListaGHEs = xListaGHE;
            this.zObra = xObra;
            this.Inibir_US = xInibir_US;

            if (this.cliente.mirrorOld == null)
                this.cliente.Find();
        }


        public DataSourcePPRA_NR20(Cliente cliente, LaudoTecnico laudoTecnico)
        {
            this.laudoTecnico = laudoTecnico;
            this.cliente = cliente;
            this.ListaGHEs = "";
            this.zObra = 0;

            if (this.cliente.mirrorOld == null)
                this.cliente.Find();
        }

		public DataSourcePPRA_NR20(int IdLaudoTecnico)
		{
            this.laudoTecnico = new LaudoTecnico();
            this.laudoTecnico.Find(IdLaudoTecnico);
            this.ListaGHEs = "";
            this.zObra = 0;

			this.cliente = laudoTecnico.nID_EMPR;
            if (this.cliente.mirrorOld == null)
			    this.cliente.Find();
		}	
		
		public RptPPRA_NR20_2 GetReport()
		{
			RptPPRA_NR20_2 report = new RptPPRA_NR20_2();
			report.SetDataSource(GetIntroducaoPPRA());
			//report.OpenSubreport("FotoFachada").SetDataSource(DataSourceFotoFachada());
			//report.OpenSubreport("Engenheiro").SetDataSource(GetEngenheiro());
			//report.OpenSubreport("Prestador").SetDataSource(GetPrestador());
			report.Refresh();

            SetTempoProcessamento(report);

			return report;
		}

     


		private DataSet GetIntroducaoPPRA()
		{
			DataTable table = new DataTable("Result");
			table.Columns.Add("DadosEmpresa", Type.GetType("System.String"));
			table.Columns.Add("CarimboCNPJ", Type.GetType("System.String"));
			table.Columns.Add("Data", Type.GetType("System.String"));
			table.Columns.Add("IsComissao", Type.GetType("System.Boolean"));
			table.Columns.Add("IsAntecipacaoBSH", Type.GetType("System.Boolean"));
            table.Columns.Add("RepresentanteLegal", Type.GetType("System.String"));
            table.Columns.Add("RepresentanteLegalTitulo", Type.GetType("System.String"));
            table.Columns.Add("Logotipo", Type.GetType("System.String"));

            table.Columns.Add("RazaoSocial_Emp", Type.GetType("System.String"));
            table.Columns.Add("Endereco_Emp", Type.GetType("System.String"));
            table.Columns.Add("CidadeUF_Emp", Type.GetType("System.String"));
            table.Columns.Add("CEP_Emp", Type.GetType("System.String"));
            table.Columns.Add("CNPJ_Emp", Type.GetType("System.String"));
            table.Columns.Add("CNAE_Emp", Type.GetType("System.String"));
            table.Columns.Add("GrauRisco_Emp", Type.GetType("System.String"));
            table.Columns.Add("Atividade_Emp", Type.GetType("System.String"));

            table.Columns.Add("RazaoSocial_Obra", Type.GetType("System.String"));
            table.Columns.Add("Endereco_Obra", Type.GetType("System.String"));
            table.Columns.Add("CidadeUF_Obra", Type.GetType("System.String"));
            table.Columns.Add("CEP_Obra", Type.GetType("System.String"));
            table.Columns.Add("CNPJ_Obra", Type.GetType("System.String"));
            table.Columns.Add("CNAE_Obra", Type.GetType("System.String"));
            table.Columns.Add("GrauRisco_Obra", Type.GetType("System.String"));
            table.Columns.Add("DescricaoCNAE_Obra", Type.GetType("System.String"));
            


           

			DataSet ds = new DataSet();

			ds.Tables.Add(table);

			DataRow newRow = ds.Tables[0].NewRow();

            if (laudoTecnico.ResponsavelLegal.mirrorOld == null)
                laudoTecnico.ResponsavelLegal.Find();


            string xArq;

            //if (cliente.Logo_Laudos == true)
            //{
            //    xArq = cliente.Logotipo;
            //}
            //else
            //{
                if (Ilitera.Data.SQLServer.EntitySQLServer.xDB2.IndexOf("Daiti") > 0)
                {
                    xArq = "I:\\fotosDocsDigitais\\_Daiti\\Relatorio_Empresa.jpg";
                }
                else if (Ilitera.Data.SQLServer.EntitySQLServer.xDB2.IndexOf("Rophe") > 0)
                {
                    xArq = "I:\\fotosDocsDigitais\\_Rophe\\Relatorio_Empresa.jpg";
                }
                else if (Ilitera.Data.SQLServer.EntitySQLServer.xDB2.IndexOf("Prajna") > 0)
                {
                    xArq = "I:\\fotosDocsDigitais\\_Prajna\\Relatorio_Empresa.jpg";
                }
                else if (Ilitera.Data.SQLServer.EntitySQLServer.xDB2.IndexOf("Fiore") > 0)
                {
                    xArq = "I:\\fotosDocsDigitais\\_Fiore\\Relatorio_Empresa.jpg";
                }
                else if (Ilitera.Data.SQLServer.EntitySQLServer.xDB2.IndexOf("Mappas") > 0)
                {
                    xArq = "I:\\fotosDocsDigitais\\_Mappas\\Relatorio_Empresa.jpg";
                }
                else
                {
                    xArq = "I:\\fotosDocsDigitais\\_Ilitera\\Relatorio_Empresa.jpg";
                }
            //}

            string xNR20 = "";
            

            DataSet rdS = new Laudo_NR20_Item().Get("nID_LAUD_TEC=" + laudoTecnico.Id + " order by Item");


            for (int zCont = 0; zCont < rdS.Tables[0].Rows.Count; zCont++)
            {


                xNR20 = xNR20 + "______________________________________________________________________________________________" + System.Environment.NewLine;

                xNR20 = xNR20 + "ITEM: " + rdS.Tables[0].Rows[zCont][2].ToString().Trim() + System.Environment.NewLine;


                xNR20 = xNR20 + System.Environment.NewLine + "a) Inventário e características dos inflamáveis ou líquidos combustíveis existentes no meio ambiente de trabalho da empresa:" + System.Environment.NewLine;

                ArrayList listInventario = new Laudo_NR20_Inventario().Find(" nId_Laud_Tec_Nr20_Item = " + rdS.Tables[0].Rows[zCont][0].ToString() + " order by Produto_Quimico");

                foreach (Laudo_NR20_Inventario xInventario in listInventario)
                {
                    xNR20 = xNR20 + System.Environment.NewLine + "- Produto Químico: " + xInventario.Produto_Quimico + System.Environment.NewLine +
                                    "   Tipo de Embalagem: " + xInventario.Tipo_Embalagem.Trim() + System.Environment.NewLine +
                                    "   Responsável pelo Armazenamento: " + xInventario.Responsavel_Armazenamento + System.Environment.NewLine +
                                    "   Quantidade Mínima: " + xInventario.Qtde_Minima.ToString() + "     Quantidade Máxima: " + xInventario.Qtde_Maxima.ToString() + System.Environment.NewLine;
                }


                ArrayList listEmpr = new Laudo_NR20_Empregado_Treinado().Find(" nId_Laud_Tec_Nr20_Item = " + rdS.Tables[0].Rows[zCont][0].ToString() + " order by Empregado_Treinado");

                xNR20 = xNR20 + System.Environment.NewLine + "    - Empregado(s) Treinado(s):" + System.Environment.NewLine;

                foreach (Laudo_NR20_Empregado_Treinado xEmpregado in listEmpr)
                {
                    xNR20 = xNR20 + xEmpregado.Empregado_Treinado + System.Environment.NewLine;
                }

                xNR20 = xNR20 + System.Environment.NewLine;
                xNR20 = xNR20 + "______________________________________________________________________________________________" + System.Environment.NewLine;





                xNR20 = xNR20 + System.Environment.NewLine + "b) Riscos específicos relativos aos locais e atividades com inflamáveis ou líquidos combustíveis existentes no meio ambiente de trabalho da empresa: " + System.Environment.NewLine;


                ArrayList listRiscos = new Laudo_NR20_Riscos_Acidentes().Find(" nId_Laud_Tec_Nr20_Item = " + rdS.Tables[0].Rows[zCont][0].ToString() + " order by Riscos_Acidentes");

                xNR20 = xNR20 + System.Environment.NewLine + "    - Risco(s) de Acidente:" + System.Environment.NewLine;

                foreach (Laudo_NR20_Riscos_Acidentes xRiscos in listRiscos)
                {
                    xNR20 = xNR20 + xRiscos.Riscos_Acidentes + System.Environment.NewLine;
                }


                ArrayList listRiscos2 = new Laudo_NR20_Riscos_Quimicos().Find(" nId_Laud_Tec_Nr20_Item = " + rdS.Tables[0].Rows[zCont][0].ToString() + " order by Riscos_Quimicos ");

                xNR20 = xNR20 + System.Environment.NewLine + "    - Risco(s) Químico:" + System.Environment.NewLine;

                foreach (Laudo_NR20_Riscos_Quimicos xRiscos in listRiscos2)
                {
                    xNR20 = xNR20 + xRiscos.Riscos_Quimicos + System.Environment.NewLine;
                }

                xNR20 = xNR20 + System.Environment.NewLine;
                xNR20 = xNR20 + "______________________________________________________________________________________________" + System.Environment.NewLine;


                xNR20 = xNR20 + System.Environment.NewLine + "c) Procedimentos e planos de prevenção de acidentes com inflamáveis ou líquidos combustíveis existentes no meio ambiente de trabalho da empresa: " + System.Environment.NewLine;


                ArrayList listPlanos = new Laudo_NR20_Planos_Prevencao().Find(" nId_Laud_Tec_Nr20_Item = " + rdS.Tables[0].Rows[zCont][0].ToString() + " order by Planos_Prevencao ");

                xNR20 = xNR20 + System.Environment.NewLine + "    - Plano(s) de Prevenção:" + System.Environment.NewLine;

                foreach (Laudo_NR20_Planos_Prevencao xPlanos in listPlanos)
                {
                    xNR20 = xNR20 + xPlanos.Planos_Prevencao + System.Environment.NewLine;
                }


                ArrayList listPlanos2 = new Laudo_NR20_Procedimentos_Prevencao().Find(" nId_Laud_Tec_Nr20_Item = " + rdS.Tables[0].Rows[zCont][0].ToString() + " order by Procedimentos");

                xNR20 = xNR20 + System.Environment.NewLine + "    - Procedimento(s) de Prevenção:" + System.Environment.NewLine;

                foreach (Laudo_NR20_Procedimentos_Prevencao xPlanos in listPlanos2)
                {
                    xNR20 = xNR20 + xPlanos.Procedimentos + System.Environment.NewLine;
                }

                xNR20 = xNR20 + System.Environment.NewLine;
                xNR20 = xNR20 + "______________________________________________________________________________________________" + System.Environment.NewLine;



                xNR20 = xNR20 + System.Environment.NewLine + "d) Medidas para atuação em situação de emergência com relação aos inflamáveis ou líquidos combustíveis existentes no meio ambiente de trabalho da empresa. " + System.Environment.NewLine;


                ArrayList listPlanos3 = new Laudo_NR20_Procedimentos_Emergencia().Find(" nId_Laud_Tec_Nr20_Item = " + rdS.Tables[0].Rows[zCont][0].ToString() + " order by Procedimentos ");

                xNR20 = xNR20 + System.Environment.NewLine + "    - Procedimento(s) de Emergência:" + System.Environment.NewLine;

                foreach (Laudo_NR20_Procedimentos_Emergencia xPlanos in listPlanos3)
                {
                    xNR20 = xNR20 + xPlanos.Procedimentos + System.Environment.NewLine;
                }

                xNR20 = xNR20 + System.Environment.NewLine;
                xNR20 = xNR20 + "______________________________________________________________________________________________" + System.Environment.NewLine + System.Environment.NewLine;

            }



            xNR20 = xNR20 +  "   - Conclusão: " + System.Environment.NewLine +
                "As informações constantes deste relatório foram obtidas no dia da execução do levantamento ambiental pertinente e que objetivava a elaboração deste PPRA. Contudo, as quantidades mencionadas são dinâmicas e poderão ser alteradas dirariamente em função do consumo e das aquisições de matérias-primas realizadas em função das necessidades produtivas da organização.";


            newRow["Atividade_Emp"] = xNR20;

            newRow["Logotipo"] = xArq;

            //laudoTecnico
            ds.Tables[0].Rows.Add(newRow);
			
			return ds;
		}



        public RptPPRA_NR20_2 GetReport_New()
        {
            RptPPRA_NR20_2 report = new RptPPRA_NR20_2();
            report.SetDataSource(GetIntroducaoNR20());
            //report.OpenSubreport("FotoFachada").SetDataSource(DataSourceFotoFachada());
            //report.OpenSubreport("Engenheiro").SetDataSource(GetEngenheiro());
            //report.OpenSubreport("Prestador").SetDataSource(GetPrestador());
            report.Refresh();

            SetTempoProcessamento(report);

            return report;
        }




        private DataSet GetIntroducaoNR20()
        {
            DataTable table = new DataTable("Result");
            table.Columns.Add("Item", Type.GetType("System.String"));
            table.Columns.Add("Titulo", Type.GetType("System.String"));
            table.Columns.Add("SubTitulo", Type.GetType("System.String"));
            table.Columns.Add("Logotipo", Type.GetType("System.String"));
            table.Columns.Add("Coluna1", Type.GetType("System.String"));
            table.Columns.Add("Coluna2", Type.GetType("System.String"));
            table.Columns.Add("Coluna3", Type.GetType("System.String"));
            table.Columns.Add("Coluna4", Type.GetType("System.String"));
            table.Columns.Add("Coluna5", Type.GetType("System.String"));
            table.Columns.Add("PrimeiroRegistro", Type.GetType("System.String"));
            

            DataSet ds = new DataSet();

            ds.Tables.Add(table);

            
            if (laudoTecnico.ResponsavelLegal.mirrorOld == null)
                laudoTecnico.ResponsavelLegal.Find();


            string xArq;

            //if (cliente.Logo_Laudos == true)
            //{
            //    xArq = cliente.Logotipo;
            //}
            //else
            //{
                if (Ilitera.Data.SQLServer.EntitySQLServer.xDB2.IndexOf("Daiti") > 0)
                {
                    xArq = "I:\\fotosDocsDigitais\\_Daiti\\Relatorio_Empresa.jpg";
                }
                else if (Ilitera.Data.SQLServer.EntitySQLServer.xDB2.IndexOf("Rophe") > 0)
                {
                    xArq = "I:\\fotosDocsDigitais\\_Rophe\\Relatorio_Empresa.jpg";
                }
                else if (Ilitera.Data.SQLServer.EntitySQLServer.xDB2.IndexOf("Prajna") > 0)
                {
                    xArq = "I:\\fotosDocsDigitais\\_Prajna\\Relatorio_Empresa.jpg";
                }
                else if (Ilitera.Data.SQLServer.EntitySQLServer.xDB2.IndexOf("Fiore") > 0)
                {
                    xArq = "I:\\fotosDocsDigitais\\_Fiore\\Relatorio_Empresa.jpg";
                }
                else if (Ilitera.Data.SQLServer.EntitySQLServer.xDB2.IndexOf("Mappas") > 0)
                {
                    xArq = "I:\\fotosDocsDigitais\\_Mappas\\Relatorio_Empresa.jpg";
                }
                else
                {
                    xArq = "I:\\fotosDocsDigitais\\_Ilitera\\Relatorio_Empresa.jpg";
                }
            //}

            

            //string xNR20 = "";


            DataSet rdS = new Laudo_NR20_Item().Get("nID_LAUD_TEC=" + laudoTecnico.Id + " order by Item");

            DataRow newRow;

            for (int zCont = 0; zCont < rdS.Tables[0].Rows.Count; zCont++)
            {

                bool xPrimeiro = true;
                
                                    
                ArrayList listInventario = new Laudo_NR20_Inventario().Find(" nId_Laud_Tec_Nr20_Item = " + rdS.Tables[0].Rows[zCont][0].ToString() + " order by Produto_Quimico");

                //inventário
                foreach (Laudo_NR20_Inventario xInventario in listInventario)
                {
                    newRow = ds.Tables[0].NewRow();
                    newRow["Item"] = rdS.Tables[0].Rows[zCont][2].ToString().Trim();
                    newRow["Titulo"] = "a) Inventário e características dos inflamáveis ou líquidos combustíveis existentes no meio ambiente de trabalho da empresa:";
                    newRow["SubTitulo"] = "Inventário";
                    newRow["Coluna1"] = xInventario.Produto_Quimico;
                    newRow["Coluna2"] = xInventario.Tipo_Embalagem.Trim();
                    newRow["Coluna3"] = xInventario.Responsavel_Armazenamento;
                    newRow["Coluna4"] = xInventario.Qtde_Minima.ToString();
                    newRow["Coluna5"] = xInventario.Qtde_Maxima.ToString();
                    newRow["Logotipo"] = xArq;

                    if (xPrimeiro) newRow["PrimeiroRegistro"] = "S";
                    else newRow["PrimeiroRegistro"] = "N";

                    xPrimeiro = false;

                    ds.Tables[0].Rows.Add(newRow);
                }




                ArrayList listEmpr = new Laudo_NR20_Empregado_Treinado().Find(" nId_Laud_Tec_Nr20_Item = " + rdS.Tables[0].Rows[zCont][0].ToString() + " order by Empregado_Treinado");

                //empregados treinados
                foreach (Laudo_NR20_Empregado_Treinado xEmpregado in listEmpr)
                {
                    newRow = ds.Tables[0].NewRow();
                    newRow["Item"] = rdS.Tables[0].Rows[zCont][2].ToString().Trim();
                    newRow["Titulo"] = "a) Inventário e características dos inflamáveis ou líquidos combustíveis existentes no meio ambiente de trabalho da empresa:";
                    newRow["SubTitulo"] = "Empregados Treinados";
                    newRow["Coluna1"] = xEmpregado.Empregado_Treinado;
                    newRow["Coluna2"] = "";
                    newRow["Coluna3"] = "";
                    newRow["Coluna4"] = "";
                    newRow["Coluna5"] = "";
                    newRow["PrimeiroRegistro"] = "N";
                    newRow["Logotipo"] = xArq;
                    ds.Tables[0].Rows.Add(newRow);
                }





                 ArrayList listRiscos = new Laudo_NR20_Riscos_Acidentes().Find(" nId_Laud_Tec_Nr20_Item = " + rdS.Tables[0].Rows[zCont][0].ToString() + " order by Riscos_Acidentes");

                
                //Riscos de Acidente
                foreach (Laudo_NR20_Riscos_Acidentes xRiscos in listRiscos)
                {
                    newRow = ds.Tables[0].NewRow();
                    newRow["Item"] = rdS.Tables[0].Rows[zCont][2].ToString().Trim();
                    newRow["Titulo"] = "b) Riscos específicos relativos aos locais e atividades com inflamáveis ou líquidos combustíveis existentes no meio ambiente de trabalho da empresa: ";
                    newRow["SubTitulo"] = "Riscos de Acidentes";
                    newRow["Coluna1"] = xRiscos.Riscos_Acidentes;
                    newRow["Coluna2"] = "";
                    newRow["Coluna3"] = "";
                    newRow["Coluna4"] = "";
                    newRow["Coluna5"] = "";
                    newRow["PrimeiroRegistro"] = "N";
                    newRow["Logotipo"] = xArq;
                    ds.Tables[0].Rows.Add(newRow);
                }


                ArrayList listRiscos2 = new Laudo_NR20_Riscos_Quimicos().Find(" nId_Laud_Tec_Nr20_Item = " + rdS.Tables[0].Rows[zCont][0].ToString() + " order by Riscos_Quimicos ");                

                //riscos químicos
                foreach (Laudo_NR20_Riscos_Quimicos xRiscos in listRiscos2)
                {
                    newRow = ds.Tables[0].NewRow();
                    newRow["Item"] = rdS.Tables[0].Rows[zCont][2].ToString().Trim();
                    newRow["Titulo"] = "b) Riscos específicos relativos aos locais e atividades com inflamáveis ou líquidos combustíveis existentes no meio ambiente de trabalho da empresa: ";
                    newRow["SubTitulo"] = "Riscos Químicos";
                    newRow["Coluna1"] = xRiscos.Riscos_Quimicos;
                    newRow["Coluna2"] = "";
                    newRow["Coluna3"] = "";
                    newRow["Coluna4"] = "";
                    newRow["Coluna5"] = "";
                    newRow["PrimeiroRegistro"] = "N";
                    newRow["Logotipo"] = xArq;
                    ds.Tables[0].Rows.Add(newRow);                
                }





                ArrayList listPlanos = new Laudo_NR20_Planos_Prevencao().Find(" nId_Laud_Tec_Nr20_Item = " + rdS.Tables[0].Rows[zCont][0].ToString() + " order by Planos_Prevencao ");

                //planos de prevenção
                foreach (Laudo_NR20_Planos_Prevencao xPlanos in listPlanos)
                {
                    newRow = ds.Tables[0].NewRow();
                    newRow["Item"] = rdS.Tables[0].Rows[zCont][2].ToString().Trim();
                    newRow["Titulo"] = "c) Procedimentos e planos de prevenção de acidentes com inflamáveis ou líquidos combustíveis existentes no meio ambiente de trabalho da empresa: ";
                    newRow["SubTitulo"] = "Planos de Prevenção";
                    newRow["Coluna1"] = xPlanos.Planos_Prevencao;
                    newRow["Coluna2"] = "";
                    newRow["Coluna3"] = "";
                    newRow["Coluna4"] = "";
                    newRow["Coluna5"] = "";
                    newRow["PrimeiroRegistro"] = "N";
                    newRow["Logotipo"] = xArq;
                    ds.Tables[0].Rows.Add(newRow);                                    
                }



                ArrayList listPlanos2 = new Laudo_NR20_Procedimentos_Prevencao().Find(" nId_Laud_Tec_Nr20_Item = " + rdS.Tables[0].Rows[zCont][0].ToString() + " order by Procedimentos");
                
                //procedimentos de prevenção
                foreach (Laudo_NR20_Procedimentos_Prevencao xPlanos in listPlanos2)
                {
                    newRow = ds.Tables[0].NewRow();
                    newRow["Item"] = rdS.Tables[0].Rows[zCont][2].ToString().Trim();
                    newRow["Titulo"] = "c) Procedimentos e planos de prevenção de acidentes com inflamáveis ou líquidos combustíveis existentes no meio ambiente de trabalho da empresa: ";
                    newRow["SubTitulo"] = "Procedimentos de Prevenção";
                    newRow["Coluna1"] = xPlanos.Procedimentos;
                    newRow["Coluna2"] = "";
                    newRow["Coluna3"] = "";
                    newRow["Coluna4"] = "";
                    newRow["Coluna5"] = "";
                    newRow["PrimeiroRegistro"] = "N";
                    newRow["Logotipo"] = xArq;
                    ds.Tables[0].Rows.Add(newRow);                                                                            
                }


                
                ArrayList listPlanos3 = new Laudo_NR20_Procedimentos_Emergencia().Find(" nId_Laud_Tec_Nr20_Item = " + rdS.Tables[0].Rows[zCont][0].ToString() + " order by Procedimentos ");

                //procedimentos de emergência
                foreach (Laudo_NR20_Procedimentos_Emergencia xPlanos in listPlanos3)
                {
                    newRow = ds.Tables[0].NewRow();
                    newRow["Item"] = rdS.Tables[0].Rows[zCont][2].ToString().Trim();
                    newRow["Titulo"] = "d) Medidas para atuação em situação de emergência com relação aos inflamáveis ou líquidos combustíveis existentes no meio ambiente de trabalho da empresa. ";
                    newRow["SubTitulo"] = "Procedimentos de Emergência";
                    newRow["Coluna1"] = xPlanos.Procedimentos;
                    newRow["Coluna2"] = "";
                    newRow["Coluna3"] = "";
                    newRow["Coluna4"] = "";
                    newRow["Coluna5"] = "";
                    newRow["PrimeiroRegistro"] = "N";
                    newRow["Logotipo"] = xArq;
                    ds.Tables[0].Rows.Add(newRow);                                                                                                
                }

            }



            //xNR20 = xNR20 + "   - Conclusão: " + System.Environment.NewLine +
            //    "As informações constantes deste relatório foram obtidas no dia da execução do levantamento ambiental pertinente e que objetivava a elaboração deste PPRA. Contudo, as quantidades mencionadas são dinâmicas e poderão ser alteradas dirariamente em função do consumo e das aquisições de matérias-primas realizadas em função das necessidades produtivas da organização.";



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


                if (this.zObra == 0)
                {
                    newRow["FotoFachada"] = Ilitera.Common.Fotos.GetByteFoto(cliente.GetFotoFachada());
                }
                else
                {
                    Cliente xCliente = new Cliente(this.zObra);
                    newRow["FotoFachada"] = Ilitera.Common.Fotos.GetByteFoto(xCliente.GetFotoFachada());                    
                }


			}
			catch (Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(ex.Message);
				System.Diagnostics.Trace.WriteLine(ex.Message);
			}
			ds.Tables[0].Rows.Add(newRow);
			return ds;
		}

		private DataSet GetEngenheiro()
		{
			DataSet ds = new DataSet();
			DataTable table = new DataTable("Result");		
			table.Columns.Add("Nome", Type.GetType("System.String"));
			table.Columns.Add("Numero", Type.GetType("System.String"));
			table.Columns.Add("Titulo", Type.GetType("System.String"));			
			table.Columns.Add("iAssinatura", Type.GetType("System.Byte[]"));
			table.Columns.Add("TituloComissao", Type.GetType("System.String"));	
			ds.Tables.Add(table);
			DataRow newRow;		
			try
			{
				//Comissao 2
				newRow = ds.Tables[0].NewRow();	
				laudoTecnico.nID_COMISSAO_2.Find();					
				newRow = ds.Tables[0].NewRow();	
				newRow["Nome"]			= laudoTecnico.nID_COMISSAO_2.NomeCompleto;
				newRow["Numero"]		= laudoTecnico.nID_COMISSAO_2.Numero;
				newRow["Titulo"]		= laudoTecnico.nID_COMISSAO_2.Titulo;					
				try
				{
					newRow["iAssinatura"] = Ilitera.Common.Fotos.GetByteFoto(laudoTecnico.nID_COMISSAO_2.FotoAss);
				}
				catch (Exception ex)
				{
					System.Diagnostics.Debug.WriteLine(ex.Message);
				}
	
				ds.Tables[0].Rows.Add(newRow);
			}
			catch (Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(ex.Message);
				System.Diagnostics.Trace.WriteLine(ex.Message);
			}

			return ds;
		}

		private DataSet GetPrestador()
		{
			DataSet ds = new DataSet();
			DataTable table = new DataTable("Result");		
			table.Columns.Add("Nome", Type.GetType("System.String"));
			table.Columns.Add("Numero", Type.GetType("System.String"));
			table.Columns.Add("Titulo", Type.GetType("System.String"));			
			table.Columns.Add("iAssinatura", Type.GetType("System.Byte[]"));
			table.Columns.Add("TituloComissao", Type.GetType("System.String"));			
			ds.Tables.Add(table);
			DataRow newRow;				
			if(this.laudoTecnico.Id != 0)
			{
				try
				{                    
					//Comissao 1
					newRow = ds.Tables[0].NewRow();		
					laudoTecnico.nID_COMISSAO_1.Find();										
					newRow["Nome"]			= laudoTecnico.nID_COMISSAO_1.NomeCompleto;
					newRow["Numero"]		= laudoTecnico.nID_COMISSAO_1.Numero;
					newRow["Titulo"]		= laudoTecnico.nID_COMISSAO_1.Titulo;				
					try
					{
						newRow["iAssinatura"] = Ilitera.Common.Fotos.GetByteFoto(laudoTecnico.nID_COMISSAO_1.FotoAss);
					}
					catch (Exception ex)
					{
						System.Diagnostics.Debug.WriteLine(ex.Message);
						System.Diagnostics.Trace.WriteLine(ex.Message);
					}

                    if (laudoTecnico.nID_COMISSAO_1.NomeCompleto.Trim() != "")
                    {
                        newRow["TituloComissao"] = "PRESIDENTE";		
                    }
                    else
                    {
                        newRow["TituloComissao"] = " ";
                    }                    
					
					ds.Tables[0].Rows.Add(newRow);
					//Comissao 2
					newRow = ds.Tables[0].NewRow();	
					laudoTecnico.nID_COMISSAO_2.Find();					
					newRow = ds.Tables[0].NewRow();					
					newRow["Nome"]			= laudoTecnico.nID_COMISSAO_2.NomeCompleto;
					newRow["Numero"]		= laudoTecnico.nID_COMISSAO_2.Numero;
					newRow["Titulo"]		= laudoTecnico.nID_COMISSAO_2.Titulo;
					try
					{
						newRow["iAssinatura"]	= Ilitera.Common.Fotos.GetByteFoto(laudoTecnico.nID_COMISSAO_2.FotoAss);
					}
					catch (Exception ex)
					{
						System.Diagnostics.Debug.WriteLine(ex.Message);
						System.Diagnostics.Trace.WriteLine(ex.Message);
					}

                    if (laudoTecnico.nID_COMISSAO_2.NomeCompleto.Trim() != "")
                    {
                        newRow["TituloComissao"] = "RELATOR - ASSISTENTE TÉCNICO";
                    }
                    else
                    {
                        newRow["TituloComissao"] = " ";
                    }

					ds.Tables[0].Rows.Add(newRow);
					//Comissao 3
					newRow = ds.Tables[0].NewRow();	
					laudoTecnico.nID_COMISSAO_3.Find();					
					newRow = ds.Tables[0].NewRow();					
					newRow["Nome"]			= laudoTecnico.nID_COMISSAO_3.NomeCompleto;
					newRow["Numero"]		= laudoTecnico.nID_COMISSAO_3.Numero;
					newRow["Titulo"]		= laudoTecnico.nID_COMISSAO_3.Titulo;					
					try
					{
						newRow["iAssinatura"] = Ilitera.Common.Fotos.GetByteFoto(laudoTecnico.nID_COMISSAO_3.FotoAss);
					}
					catch (Exception ex)
					{
						System.Diagnostics.Debug.WriteLine(ex.Message);
						System.Diagnostics.Trace.WriteLine(ex.Message);
					}

                    if (laudoTecnico.nID_COMISSAO_3.NomeCompleto.Trim() != "")
                    {
                        newRow["TituloComissao"] = "REVISOR - ASSISTENTE TÉCNICO";						
                    }
                    else
                    {
                        newRow["TituloComissao"] = " ";
                    }

					
					ds.Tables[0].Rows.Add(newRow);
				}
				catch (Exception ex)
				{
					System.Diagnostics.Debug.WriteLine(ex.Message);
					System.Diagnostics.Trace.WriteLine(ex.Message);
				}
			}
			return ds;
		}	
	}
}
