using System;
using Ilitera.Opsa.Data;
using System.Data;
using Ilitera.Common;
using System.Collections;
using System.Text;


namespace Ilitera.Sied.Report
{
	public class DataSourcePCA : DataSourceBase
	{
		private Cliente cliente;
        private LaudoTecnico laudoTecnico;
		
		public DataSourcePCA(Cliente cliente)
		{
			this.cliente = cliente;
           
            if (this.cliente.mirrorOld == null)
			    this.cliente.Find();
            
            this.laudoTecnico = LaudoTecnico.GetUltimoLaudo(this.cliente.Id);
		}

		public DataSourcePCA(LaudoTecnico laudoTecnico)
		{
			this.laudoTecnico = laudoTecnico;
            this.cliente = this.laudoTecnico.nID_EMPR;
           
            if(this.cliente.mirrorOld==null)
                this.cliente.Find();
		}

        public DataSourcePCA(Cliente cliente, LaudoTecnico laudoTecnico)
        {
            this.laudoTecnico = laudoTecnico;
            this.cliente = cliente;

            if (this.cliente.mirrorOld == null)
                this.cliente.Find();
        }

		public DataSourcePCA(int IdLaudoTecnico)
		{
            this.laudoTecnico = new LaudoTecnico();
            this.laudoTecnico.Find(IdLaudoTecnico);

			this.cliente = laudoTecnico.nID_EMPR;
            if (this.cliente.mirrorOld == null)
			    this.cliente.Find();
		}	
		
		public RptPCA GetReport()
		{
			RptPCA report = new RptPCA();
			report.SetDataSource(GetIntroducaoPPRA());
			report.OpenSubreport("FotoFachada").SetDataSource(DataSourceFotoFachada());
			report.OpenSubreport("Engenheiro").SetDataSource(GetEngenheiro());
			//report.OpenSubreport("Prestador").SetDataSource(GetPrestador());
			report.Refresh();

            SetTempoProcessamento(report);

			return report;
		}

        public RptPCA_PGR GetReport_PCAPGR()
        {
            RptPCA_PGR report = new RptPCA_PGR();
            report.SetDataSource(GetIntroducaoPPRA());
            report.OpenSubreport("FotoFachada").SetDataSource(DataSourceFotoFachada());
            report.OpenSubreport("Engenheiro").SetDataSource(GetEngenheiro());
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
            table.Columns.Add("Base", Type.GetType("System.String"));

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

            newRow["Logotipo"] = xArq;
            //newRow["Logotipo"] =  Ilitera.Common.Fotos.GetByteFoto_Uri(xArq);

            newRow["DadosEmpresa"] = cliente.GetDadosEmpresaHtml(laudoTecnico.hDT_LAUDO);
            newRow["CarimboCNPJ"] = cliente.GetCarimboCnpjHtml(laudoTecnico.hDT_LAUDO);
            newRow["Data"] =  laudoTecnico.hDT_LAUDO.ToString("dd-MM-yyyy");  //+ " a  " + laudoTecnico.hDT_LAUDO.AddDays(364).ToString("dd-MM-yyyy");
            newRow["IsAntecipacaoBSH"] = false;
            newRow["IsComissao"] = !(laudoTecnico.nID_COMISSAO_1.Id == 0 &&
                                            laudoTecnico.nID_COMISSAO_2.Id != 0 &&
                                            laudoTecnico.nID_COMISSAO_3.Id == 0);
            newRow["RepresentanteLegal"] = laudoTecnico.nID_EMPR.GetEndereco().GetCidadeEstado().ToString(); //laudoTecnico.ResponsavelLegal.NomeCompleto;
            newRow["RepresentanteLegalTitulo"] = laudoTecnico.ResponsavelLegal.Titulo;


            if (Ilitera.Data.SQLServer.EntitySQLServer.xDB2.IndexOf("Prajna") > 0)
            {
                newRow["Base"] = "PRAJNA";
            }
            else if (Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper().IndexOf("GLOBAL") > 0)
            {
                newRow["Base"] = "GLOBAL";
            }
            else
            {
                newRow["Base"] = "OUTROS";
            }



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
				//newRow["FotoFachada"] = Ilitera.Common.Fotos.GetByteFoto(cliente.GetFotoFachada());
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
				newRow = ds.Tables[0].NewRow();	
				laudoTecnico.nID_COMISSAO_2.Find();

                ArrayList list2 = new Pcmso().Find(" IdLaudoTecnico = " + laudoTecnico.Id.ToString() + " order by DataPcmso Desc ");

                if (list2.Count > 0)
                {
                    foreach (Pcmso xpcmso in list2)
                    {
                        newRow = ds.Tables[0].NewRow();

                        xpcmso.IdCoordenador.Find();
                        newRow["Nome"] = xpcmso.IdCoordenador.NomeCompleto;
                        newRow["Numero"] = xpcmso.IdCoordenador.Numero;
                        newRow["Titulo"] = xpcmso.IdCoordenador.Titulo;

                        try
                        {
                            //newRow["iAssinatura"] = Ilitera.Common.Fotos.GetByteFoto(laudoTecnico.nID_COMISSAO_2.FotoAss);
                            newRow["iAssinatura"] = Ilitera.Common.Fotos.GetByteFoto(xpcmso.IdCoordenador.FotoAss);
                            // newRow["iAssinatura"] = Ilitera.Common.Fotos.GetByteFoto(laudoTecnico.nID_TEC_CMPO.FotoAss);
                        }
                        catch (Exception ex)
                        {
                            System.Diagnostics.Debug.WriteLine(ex.Message);
                        }

                        ds.Tables[0].Rows.Add(newRow);

                        break;
                    }
                }



                //Pcmso xpcmso = new Pcmso();

                //xpcmso.Find(" IdLaudoTecnico = " + laudoTecnico.Id.ToString());


                //newRow = ds.Tables[0].NewRow();
                ////newRow["Nome"]			= laudoTecnico.nID_COMISSAO_2.NomeCompleto;
                ////newRow["Numero"]		= laudoTecnico.nID_COMISSAO_2.Numero;
                ////newRow["Titulo"]		= laudoTecnico.nID_COMISSAO_2.Titulo;					

                //newRow["Nome"] = xpcmso.IdCoordenador.NomeCompleto;
                //newRow["Numero"] = xpcmso.IdCoordenador.Numero;
                //newRow["Titulo"] = xpcmso.IdCoordenador.Titulo;

                ////newRow["Nome"] = laudoTecnico.nID_TEC_CMPO.NomeCompleto;
                ////newRow["Numero"] = laudoTecnico.nID_TEC_CMPO.Numero;
                ////newRow["Titulo"] = laudoTecnico.nID_TEC_CMPO.Titulo;			



                //try
                //{
                //    //newRow["iAssinatura"] = Ilitera.Common.Fotos.GetByteFoto(laudoTecnico.nID_COMISSAO_2.FotoAss);
                //    newRow["iAssinatura"] = Ilitera.Common.Fotos.GetByteFoto_Uri(xpcmso.IdCoordenador.FotoAss);
                //    // newRow["iAssinatura"] = Ilitera.Common.Fotos.GetByteFoto(laudoTecnico.nID_TEC_CMPO.FotoAss);
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
						newRow["iAssinatura"] =  Ilitera.Common.Fotos.GetByteFoto_Uri(laudoTecnico.nID_COMISSAO_1.FotoAss);
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
