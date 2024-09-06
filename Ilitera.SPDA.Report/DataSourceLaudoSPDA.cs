using System;
using Ilitera.Opsa.Data;
using System.Data;
using Ilitera.Common;
using System.Collections;
using System.Text;

namespace Ilitera.SPDA.Report
{
	public class DataSourceLaudoSPDA : DataSourceBase
	{
		private Cliente cliente;
        private LaudoTecnico laudoTecnico;
        private LaudoSPDA laudoSPDA;
		
		public DataSourceLaudoSPDA(Cliente cliente, Int32 zLaudoSPDA)
		{
			this.cliente = cliente;

            this.laudoSPDA = new LaudoSPDA();
            this.laudoSPDA.Find(zLaudoSPDA);
           
            if (this.cliente.mirrorOld == null)
			    this.cliente.Find();
            
            this.laudoTecnico = LaudoTecnico.GetUltimoLaudo(this.cliente.Id);
		}


		
		public RptLaudoSPDA GetReport()
		{
            RptLaudoSPDA report = new RptLaudoSPDA();
			report.SetDataSource(GetIntroducaoPPRA());
			report.OpenSubreport("FotoFachada").SetDataSource(DataSourceFotoFachada());
			report.OpenSubreport("Engenheiro").SetDataSource(GetEngenheiro());
            report.OpenSubreport("rtpPontos").SetDataSource(GetPontos());
            report.OpenSubreport("rptLaudoEletrico2_Obs").SetDataSource(GetObs());
            
			report.Refresh();

            SetTempoProcessamento(report);

			return report;
		}


        public RptLaudoSPDA_Conclusao GetReport_Conclusao()
        {
            RptLaudoSPDA_Conclusao report = new RptLaudoSPDA_Conclusao();
            report.SetDataSource(GetConclusao());
            report.OpenSubreport("Engenheiro3").SetDataSource(GetEngenheiro());
            report.OpenSubreport("Eletricista2").SetDataSource(GetEletricista());
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
            table.Columns.Add("NR1023", Type.GetType("System.String"));
            table.Columns.Add("NR1024a", Type.GetType("System.String"));
            table.Columns.Add("NR1024b", Type.GetType("System.String"));
            table.Columns.Add("NR1024c", Type.GetType("System.String"));
            table.Columns.Add("NR1024d", Type.GetType("System.String"));
            table.Columns.Add("NR1024e", Type.GetType("System.String"));
            table.Columns.Add("NR1024f", Type.GetType("System.String"));
            table.Columns.Add("NR1024g", Type.GetType("System.String"));
            table.Columns.Add("NR1025a", Type.GetType("System.String"));
            table.Columns.Add("NR1025b", Type.GetType("System.String"));
            table.Columns.Add("NR1088a", Type.GetType("System.String"));
            table.Columns.Add("NR1088b", Type.GetType("System.String"));
            table.Columns.Add("NR1088c", Type.GetType("System.String"));
            table.Columns.Add("PRa", Type.GetType("System.String"));
            table.Columns.Add("PRb", Type.GetType("System.String"));
            table.Columns.Add("PRc", Type.GetType("System.String"));
            table.Columns.Add("Introducao", Type.GetType("System.String"));
            table.Columns.Add("Conclusao", Type.GetType("System.String"));
            table.Columns.Add("Texto_Inicial", Type.GetType("System.String"));
            table.Columns.Add("Croqui", Type.GetType("System.Byte[]"));
            table.Columns.Add("Observacoes", Type.GetType("System.String"));
            table.Columns.Add("Recomendacoes", Type.GetType("System.String"));
            table.Columns.Add("Engenheiro_Eletricista", Type.GetType("System.String"));

			DataSet ds = new DataSet();

			ds.Tables.Add(table);

			DataRow newRow = ds.Tables[0].NewRow();

            if (laudoTecnico.ResponsavelLegal.mirrorOld == null)
                laudoTecnico.ResponsavelLegal.Find();


            string xArq;

            if (cliente.Logo_Laudos == true)
            {
                xArq = cliente.Logotipo;
            }
            else
            {
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
            }

            if (this.laudoSPDA.PRa == "C") newRow["PRa"] = "•";
            else if (this.laudoSPDA.PRa == "I") newRow["PRa"] = " •";
            else if (this.laudoSPDA.PRa == "P") newRow["PRa"] = "  •";

            if (this.laudoSPDA.PRb == "C") newRow["PRb"] = "•";
            else if (this.laudoSPDA.PRb == "I") newRow["PRb"] = " •";
            else if (this.laudoSPDA.PRb == "P") newRow["PRb"] = "  •";


            newRow["Introducao"] = this.laudoSPDA.Introducao;

            newRow["Conclusao"] = this.laudoSPDA.Conclusao;

            
            newRow["Logotipo"] = xArq;
            newRow["DadosEmpresa"] = cliente.GetDadosEmpresaHtml(laudoTecnico.hDT_LAUDO);
            newRow["CarimboCNPJ"] = cliente.GetCarimboCnpjHtml(laudoTecnico.hDT_LAUDO);
            //newRow["Data"] =  laudoTecnico.hDT_LAUDO.ToString("dd-MM-yyyy");  //+ " a  " + laudoTecnico.hDT_LAUDO.AddDays(364).ToString("dd-MM-yyyy");
            newRow["Data"] = this.laudoSPDA.Data_Laudo.ToString("dd-MM-yyyy");
            newRow["IsAntecipacaoBSH"] = false;
            newRow["IsComissao"] = !(laudoTecnico.nID_COMISSAO_1.Id == 0 &&
                                            laudoTecnico.nID_COMISSAO_2.Id != 0 &&
                                            laudoTecnico.nID_COMISSAO_3.Id == 0);
            newRow["RepresentanteLegal"] = laudoTecnico.nID_EMPR.GetEndereco().GetCidadeEstado().ToString(); //laudoTecnico.ResponsavelLegal.NomeCompleto;
            newRow["RepresentanteLegalTitulo"] = laudoTecnico.ResponsavelLegal.Titulo;

            newRow["Recomendacoes"] = this.laudoSPDA.Recomendacoes;
            newRow["Observacoes"] = this.laudoSPDA.Observacoes;


            if (this.laudoSPDA.Croqui.Trim() != "")
            {
                newRow["Croqui"] = Ilitera.Common.Fotos.GetByteFoto_Uri("I:\\fotosdocsdigitais\\" + cliente.DiretorioPadrao + "\\vasopressao\\" + this.laudoSPDA.Croqui.Trim());
            }


            Prestador xPrestador = new Prestador();
            xPrestador.Find(this.laudoSPDA.Id_Responsavel.Id);

           

            newRow["Engenheiro_Eletricista"] =  "   " + xPrestador.NomeCompleto + ", " + xPrestador.Titulo + ", inscrito no CREA/SP sob o n° " + xPrestador.Numero + ", infra-assinado, tendo em vista dispor a norma legal que “os estabelecimentos com carga instalada superior a 75 kW devem constituir e manter: (...) b) documentação das inspeções e medições do sistema de proteção contra descargas atmosféricas e aterramentos elétricos” (NR 10, subitem 10.2.4 “b”), vem pelo presente documento apresentar: ";

            ds.Tables[0].Rows.Add(newRow);
			
			return ds;
		}


        private DataSet GetConclusao()
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
            table.Columns.Add("PRa", Type.GetType("System.String"));
            table.Columns.Add("PRb", Type.GetType("System.String"));            
            table.Columns.Add("Introducao", Type.GetType("System.String"));
            table.Columns.Add("Conclusao", Type.GetType("System.String"));
            table.Columns.Add("Texto_Inicial", Type.GetType("System.String"));

            DataSet ds = new DataSet();

            ds.Tables.Add(table);

            DataRow newRow = ds.Tables[0].NewRow();

            if (laudoTecnico.ResponsavelLegal.mirrorOld == null)
                laudoTecnico.ResponsavelLegal.Find();


            string xArq;

            if (cliente.Logo_Laudos == true)
            {
                xArq = cliente.Logotipo;
            }
            else
            {
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
            }



            newRow["Conclusao"] = this.laudoSPDA.Conclusao;

            newRow["Logotipo"] = xArq;
            newRow["DadosEmpresa"] = cliente.GetDadosEmpresaHtml(laudoTecnico.hDT_LAUDO);
            newRow["CarimboCNPJ"] = cliente.GetCarimboCnpjHtml(laudoTecnico.hDT_LAUDO);
            newRow["Data"] = laudoTecnico.hDT_LAUDO.ToString("dd-MM-yyyy");  //+ " a  " + laudoTecnico.hDT_LAUDO.AddDays(364).ToString("dd-MM-yyyy");
            newRow["IsAntecipacaoBSH"] = false;
            newRow["IsComissao"] = !(laudoTecnico.nID_COMISSAO_1.Id == 0 &&
                                            laudoTecnico.nID_COMISSAO_2.Id != 0 &&
                                            laudoTecnico.nID_COMISSAO_3.Id == 0);
            newRow["RepresentanteLegal"] = laudoTecnico.nID_EMPR.GetEndereco().GetCidadeEstado().ToString(); //laudoTecnico.ResponsavelLegal.NomeCompleto;
            newRow["RepresentanteLegalTitulo"] = laudoTecnico.ResponsavelLegal.Titulo;

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
				//newRow = ds.Tables[0].NewRow();

                Prestador xPrestador = new Prestador();
                xPrestador.Find(this.laudoSPDA.Id_Responsavel.Id);

                //laudoTecnico.nID_COMISSAO_2.Find();					


				newRow = ds.Tables[0].NewRow();	
//				newRow["Nome"]			= laudoTecnico.nID_COMISSAO_2.NomeCompleto;
//				newRow["Numero"]		= laudoTecnico.nID_COMISSAO_2.Numero;
//				newRow["Titulo"]		= laudoTecnico.nID_COMISSAO_2.Titulo;					

                newRow["Nome"] = xPrestador.NomeCompleto;
                newRow["Numero"] = xPrestador.Numero;
                newRow["Titulo"] = xPrestador.Titulo;					


				try
				{
					newRow["iAssinatura"] = Ilitera.Common.Fotos.GetByteFoto_Uri(xPrestador.FotoAss);
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





        private DataSet GetEletricista()
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
                //newRow = ds.Tables[0].NewRow();

                Prestador xPrestador = new Prestador();
                xPrestador.Find(this.laudoSPDA.Id_Eletricista);
             

                newRow = ds.Tables[0].NewRow();
         
                newRow["Nome"] = xPrestador.NomeCompleto;
                newRow["Numero"] = xPrestador.Numero;
                newRow["Titulo"] = xPrestador.Titulo;


                try
                {
                    newRow["iAssinatura"] = Ilitera.Common.Fotos.GetByteFoto_Uri(xPrestador.FotoAss);
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




        private DataSet GetCronograma()
        {
            DataSet ds = new DataSet();
            DataTable table = new DataTable("Result");
            table.Columns.Add("PlanejamentoAnual", Type.GetType("System.String"));
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
            table.Columns.Add("EstrategiaMetAcao", Type.GetType("System.String"));
            table.Columns.Add("FormaRegistro", Type.GetType("System.String"));
            table.Columns.Add("Ano", Type.GetType("System.Int32"));
          
            ds.Tables.Add(table);
            DataRow newRow;
            try
            {
                //Comissao 2
                newRow = ds.Tables[0].NewRow();

                //Prestador xPrestador = new Prestador();
                //xPrestador.Find(this.laudoeletrico.Id_Eletricista);


                newRow = ds.Tables[0].NewRow();

                newRow["Jan"] = "x";
                newRow["Fev"] = "x";
                newRow["Mar"] = "x";
                newRow["Abr"] = "x";
                newRow["Mai"] = "x";
                newRow["Jun"] = "x";
                newRow["Jul"] = "x";
                newRow["Ago"] = "x";
                newRow["Set"] = "x";
                newRow["Out"] = "x";
                newRow["Nov"] = "x";
                newRow["Dez"] = "x";
                newRow["PlanejamentoAnual"] = "2013";
                newRow["Ano"] = 2013;


                //try
                //{
                //    newRow["iAssinatura"] = Ilitera.Common.Fotos.GetByteFoto(xPrestador.FotoAss);
                //}
                //catch (Exception ex)
                //{
                //    System.Diagnostics.Debug.WriteLine(ex.Message);
                //}

                ds.Tables[0].Rows.Add(newRow);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Trace.WriteLine(ex.Message);
            }

            return ds;
        }


        private DataSet GetResponsavel_Empresa()
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

                Prestador xPrestador = new Prestador();
                xPrestador.Find(this.laudoSPDA.Id_Responsavel_Empresa);


                newRow = ds.Tables[0].NewRow();

                newRow["Nome"] = xPrestador.NomeCompleto;                
                newRow["Titulo"] = this.cliente.NomeCompleto;


                try
                {
                    newRow["iAssinatura"] = Ilitera.Common.Fotos.GetByteFoto_Uri(xPrestador.FotoAss);
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

        
        private DataSet GetObs()
        {
            DataSet ds = new DataSet();
            DataTable table = new DataTable("Result");
            table.Columns.Add("Titulo", Type.GetType("System.String"));
            table.Columns.Add("Descricao", Type.GetType("System.String"));            
//            table.Columns.Add("Foto", Type.GetType("System.Byte[]"));
            table.Columns.Add("Foto_Adeq", Type.GetType("System.Byte[]"));
            table.Columns.Add("Ordem", Type.GetType("System.String"));
            ds.Tables.Add(table);



            Ilitera.Data.Laudo_Eletrico zAdeq = new Ilitera.Data.Laudo_Eletrico();

            DataSet zDs = new DataSet();
            string zDescricao = "";
            int zOrdem = 0;


            zDs = zAdeq.Trazer_Adequacao_SPDA(this.laudoSPDA.Id);

            DataRow newRow;
            try
            {


                for (int zCont = 0; zCont < zDs.Tables[0].Rows.Count; zCont++)
                {


                    if (zDs.Tables[0].Rows[zCont][1].ToString() != zDescricao)
                    {
                        zOrdem = 0;
                        newRow = ds.Tables[0].NewRow();

                        newRow["Titulo"] = zDs.Tables[0].Rows[zCont][0].ToString();
                        newRow["Descricao"] = zDs.Tables[0].Rows[zCont][1].ToString();
                        if (zDs.Tables[0].Rows[zCont][2].ToString().Trim() != "")
                        {
                            newRow["Foto_Adeq"] = Ilitera.Common.Fotos.GetByteFoto_Uri("I:\\fotosdocsdigitais\\" + cliente.DiretorioPadrao + "\\vasopressao\\" + zDs.Tables[0].Rows[zCont][2].ToString());
                        }
                       

                        newRow["Ordem"] = "";

                        ds.Tables[0].Rows.Add(newRow);

                        zDescricao = zDs.Tables[0].Rows[zCont][1].ToString();
                    }

                    if (zDs.Tables[0].Rows[zCont][4].ToString().Trim() != "")
                    {
                        zOrdem = zOrdem + 1;
                        newRow = ds.Tables[0].NewRow();
                        newRow["Titulo"] = zDs.Tables[0].Rows[zCont][0].ToString();
                        newRow["Descricao"] = zDs.Tables[0].Rows[zCont][4].ToString();
                        newRow["Ordem"] = zOrdem;

                        ds.Tables[0].Rows.Add(newRow);
                    }

                }



            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Trace.WriteLine(ex.Message);
            }

            return ds;
        }



        private DataSet GetPontos()
        {
            DataSet ds = new DataSet();
            DataTable table = new DataTable("Result");
            table.Columns.Add("Medida", Type.GetType("System.String"));
            table.Columns.Add("Ordem", Type.GetType("System.String"));
            table.Columns.Add("Tot_Medida", Type.GetType("System.String"));
            table.Columns.Add("Tot_Ordem", Type.GetType("System.String"));
          
            
            ds.Tables.Add(table);


            IDataReader xReader;
         
            int zOrdem = 0;

            Ilitera.Data.Laudo_Eletrico zAdeq = new Ilitera.Data.Laudo_Eletrico();
            xReader = zAdeq.Gerar_Dados_Pontos(this.laudoSPDA.Id);

            float zTot = 0;
            float zTot_Ordem = 0;

            while (xReader.Read())
            {

                zTot_Ordem++;

                zTot = zTot + System.Convert.ToSingle( xReader["Medida"] );

            }

            zTot = zTot / zTot_Ordem;


            float zMedida = 0;
            DataRow newRow;
            xReader = zAdeq.Gerar_Dados_Pontos(this.laudoSPDA.Id);

            while (xReader.Read())
            {

                newRow = ds.Tables[0].NewRow();

                zOrdem++;

                zMedida = System.Convert.ToSingle( xReader["Medida"] );

                newRow["Tot_Medida"] = zTot.ToString("#.00");
                if (zTot_Ordem < 10)
                {
                    newRow["Tot_Ordem"] = "0" + zTot_Ordem.ToString().Trim();
                }
                else
                {
                    newRow["Tot_Ordem"] = zTot_Ordem.ToString().Trim();
                }


                newRow["Medida"] = zMedida.ToString("#.0");

                if (zOrdem < 10)
                {
                    newRow["Ordem"] = "0" + zOrdem.ToString().Trim();
                }
                else
                {
                    newRow["Ordem"] = zOrdem.ToString().Trim();
                }

                ds.Tables[0].Rows.Add(newRow);

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
						newRow["iAssinatura"] = Ilitera.Common.Fotos.GetByteFoto_Uri(laudoTecnico.nID_COMISSAO_1.FotoAss);
					}
					catch (Exception ex)
					{
						System.Diagnostics.Debug.WriteLine(ex.Message);
						System.Diagnostics.Trace.WriteLine(ex.Message);
					}
					newRow["TituloComissao"]= "PRESIDENTE";		
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
						newRow["iAssinatura"]	= Ilitera.Common.Fotos.GetByteFoto_Uri(laudoTecnico.nID_COMISSAO_2.FotoAss);
					}
					catch (Exception ex)
					{
						System.Diagnostics.Debug.WriteLine(ex.Message);
						System.Diagnostics.Trace.WriteLine(ex.Message);
					}
					newRow["TituloComissao"]= "RELATOR - ASSISTENTE TÉCNICO";	
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
						newRow["iAssinatura"] = Ilitera.Common.Fotos.GetByteFoto_Uri(laudoTecnico.nID_COMISSAO_3.FotoAss);
					}
					catch (Exception ex)
					{
						System.Diagnostics.Debug.WriteLine(ex.Message);
						System.Diagnostics.Trace.WriteLine(ex.Message);
					}
					newRow["TituloComissao"]= "REVISOR - ASSISTENTE TÉCNICO";						
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
