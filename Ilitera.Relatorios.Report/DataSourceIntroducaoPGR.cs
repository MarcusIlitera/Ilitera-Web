using System;
using Ilitera.Opsa.Data;
using System.Data;
using Ilitera.Common;
using System.Collections;
using System.Text;


namespace Ilitera.Relatorios.Report
{
	public class DataSourceIntroducaoPGR : DataSourceBase
	{
		private Cliente cliente;
        private LaudoTecnico laudoTecnico;
        private string ListaGHEs;
        private Int32 zObra;
        private bool Inibir_US;
        private string zIndice;
        private string zIndice2;

        public DataSourceIntroducaoPGR(Cliente cliente)
		{
			this.cliente = cliente;
            this.ListaGHEs = "";
            this.zObra = 0;
            this.Inibir_US = false;
           
            if (this.cliente.mirrorOld == null)
			    this.cliente.Find();
            
            this.laudoTecnico = LaudoTecnico.GetUltimoLaudo(this.cliente.Id);
		}

		public DataSourceIntroducaoPGR(LaudoTecnico laudoTecnico)
		{
			this.laudoTecnico = laudoTecnico;
            this.cliente = this.laudoTecnico.nID_EMPR;
            this.ListaGHEs = "";
            this.zObra = 0;
            this.Inibir_US = false;
           
            if(this.cliente.mirrorOld==null)
                this.cliente.Find();
		}


        public DataSourceIntroducaoPGR(LaudoTecnico laudoTecnico, string xListaGHE, Int32 xObra, bool xInibir_US)
        {
            this.laudoTecnico = laudoTecnico;
            this.cliente = this.laudoTecnico.nID_EMPR;
            this.ListaGHEs = xListaGHE;
            this.zObra = xObra;
            this.Inibir_US = xInibir_US;

            if (this.cliente.mirrorOld == null)
                this.cliente.Find();
        }


        public DataSourceIntroducaoPGR(Cliente cliente, LaudoTecnico laudoTecnico)
        {
            this.laudoTecnico = laudoTecnico;
            this.cliente = cliente;
            this.ListaGHEs = "";
            this.zObra = 0;

            if (this.cliente.mirrorOld == null)
                this.cliente.Find();
        }

		public DataSourceIntroducaoPGR(int IdLaudoTecnico)
		{
            this.laudoTecnico = new LaudoTecnico();
            this.laudoTecnico.Find(IdLaudoTecnico);
            this.ListaGHEs = "";
            this.zObra = 0;

			this.cliente = laudoTecnico.nID_EMPR;
            if (this.cliente.mirrorOld == null)
			    this.cliente.Find();
		}



        
        public RptIntroducaoPGR GetReport()
		{
			RptIntroducaoPGR report = new RptIntroducaoPGR();
			report.SetDataSource(GetIntroducaoPPRA());
            this.zIndice = "";
            this.zIndice2 = "";
            report.OpenSubreport("FotoFachada").SetDataSource(DataSourceFotoFachada());
			//report.OpenSubreport("Engenheiro").SetDataSource(GetEngenheiro());
			report.OpenSubreport("Prestador").SetDataSource(GetPrestador());
			report.Refresh();

            SetTempoProcessamento(report);

			return report;
		}

        public RptIntroducaoPGR_2 GetReport_2()
        {
            RptIntroducaoPGR_2 report = new RptIntroducaoPGR_2();

            report.SetDataSource(GetIntroducaoPGR_PAE());

            report.Refresh();

            SetTempoProcessamento(report);

            return report;
        }

        public RptIntroducaoPGR_3 GetReport_3()
        {
            RptIntroducaoPGR_3 report = new RptIntroducaoPGR_3();

            report.SetDataSource(GetIntroducaoPGR_PAE());

            report.Refresh();

            SetTempoProcessamento(report);

            return report;
        }


        public RptIntroducaoPGR GetReportIndice(string zIndice, string zIndice2)
        {
            RptIntroducaoPGR report = new RptIntroducaoPGR();
            this.zIndice = zIndice;
            this.zIndice2 = zIndice2;
            report.SetDataSource(GetIntroducaoPPRA());
            report.OpenSubreport("FotoFachada").SetDataSource(DataSourceFotoFachada());
            //report.OpenSubreport("Engenheiro").SetDataSource(GetEngenheiro());
            report.OpenSubreport("Prestador").SetDataSource(GetPrestador());
            report.Refresh();

            SetTempoProcessamento(report);

            return report;
        }

        public RptIntroducaoPGR_2 GetReportIndice_2(string zIndice, string zIndice2)
        {
            RptIntroducaoPGR_2 report = new RptIntroducaoPGR_2();

            report.SetDataSource(GetIntroducaoPGR_PAE());

            report.Refresh();

            SetTempoProcessamento(report);

            return report;
        }

        public RptIntroducaoPGR_3 GetReportIndice_3(string zIndice, string zIndice2)
        {
            RptIntroducaoPGR_3 report = new RptIntroducaoPGR_3();

            report.SetDataSource(GetIntroducaoPGR_PAE());

            report.Refresh();

            SetTempoProcessamento(report);

            return report;
        }





        public RptIntroducaoPGR GetReportPGR()
        {
            RptIntroducaoPGR report = new RptIntroducaoPGR();
            report.SetDataSource(GetIntroducaoPPRA());
            report.OpenSubreport("FotoFachada").SetDataSource(DataSourceFotoFachada());
            report.OpenSubreport("Engenheiro").SetDataSource(GetEngenheiro());
            report.OpenSubreport("Prestador").SetDataSource(GetPrestador());
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

            table.Columns.Add("Obs", Type.GetType("System.String"));

            table.Columns.Add("RazaoSocial_Emp", Type.GetType("System.String"));
            table.Columns.Add("Endereco_Emp", Type.GetType("System.String"));
            table.Columns.Add("CidadeUF_Emp", Type.GetType("System.String"));
            table.Columns.Add("CEP_Emp", Type.GetType("System.String"));
            table.Columns.Add("CNPJ_Emp", Type.GetType("System.String"));
            table.Columns.Add("CNAE_Emp", Type.GetType("System.String"));
            table.Columns.Add("GrauRisco_Emp", Type.GetType("System.String"));
            table.Columns.Add("Atividade_Emp", Type.GetType("System.String"));
            table.Columns.Add("Base", Type.GetType("System.String"));

            table.Columns.Add("RazaoSocial_Obra", Type.GetType("System.String"));
            table.Columns.Add("Endereco_Obra", Type.GetType("System.String"));
            table.Columns.Add("CidadeUF_Obra", Type.GetType("System.String"));
            table.Columns.Add("CEP_Obra", Type.GetType("System.String"));
            table.Columns.Add("CNPJ_Obra", Type.GetType("System.String"));
            table.Columns.Add("CNAE_Obra", Type.GetType("System.String"));
            table.Columns.Add("GrauRisco_Obra", Type.GetType("System.String"));
            table.Columns.Add("DescricaoCNAE_Obra", Type.GetType("System.String"));

            table.Columns.Add("Implementador", Type.GetType("System.String"));
            table.Columns.Add("Implementador_Numero", Type.GetType("System.String"));
            table.Columns.Add("Implementador_Titulo", Type.GetType("System.String"));
            table.Columns.Add("Indice", Type.GetType("System.String"));
            table.Columns.Add("Indice2", Type.GetType("System.String"));
            table.Columns.Add("Historico_Versao", Type.GetType("System.String"));
            table.Columns.Add("Assinatura_Implementador", Type.GetType("System.Byte[]"));

            table.Columns.Add("PAE1", Type.GetType("System.String"));
            table.Columns.Add("PAE2", Type.GetType("System.String"));


            string xRazaoSocial_Emp = "";
            string xEndereco_Emp = "";
            string xCidadeUF_Emp = "";
            string xCEP_Emp = "";
            string xCNPJ_Emp = "";
            string xCNAE_Emp = "";
            string xGrauRisco_Emp = "";
            string xAtividade_Emp = "";

            string xRazaoSocial_Obra = "";
            string xEndereco_Obra = "";
            string xCidadeUF_Obra = "";
            string xCEP_Obra = "";
            string xCNPJ_Obra = "";
            string xCNAE_Obra = "";
            string xGrauRisco_Obra = "";
            string xDescricaoCNAE_Obra = "";


            string sDadosEmpresa = "";


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


            //pegar total de n�mero de trabalhadores expostos em todos os GHEs
            ArrayList listGhe = new Ghe().Find("nID_LAUD_TEC=" + laudoTecnico.Id + " ORDER BY tNO_FUNC");
            int numTrabExpostos = 0;


            Pcmso pcmso = new Pcmso();
            pcmso.Find("IdLaudoTecnico=" + laudoTecnico.Id.ToString());

            numTrabExpostos = pcmso.GetNumFuncionarios();



            //wagner - filtro de ghes selecionados - colocar

            Boolean zLoc = false;

            if (numTrabExpostos < 1 || ListaGHEs.Trim() != "")
            {
                numTrabExpostos = 0;
            }

            if (numTrabExpostos < 1 || ListaGHEs.Trim() != "")
            {
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
                        numTrabExpostos = numTrabExpostos + ghe.GetNumeroEmpregadosExpostos(false);
                    }
                }
            }

            //esse c�lculo n�o est� 100%,  n�o posso simplesmente somar os GHEs,  pois um funcion�rio com duas classif.funcionais para o mesmo laudo,
            //vai ser somado duas vezes nesta totaliza��o.   Acho que � pegar apenas o total de empregados alocados em GHEs, montando um select mesmo

            //se n�o tiver pcmso, ou se retornar zero, calcular por ghe


            System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");


            if (Ilitera.Data.SQLServer.EntitySQLServer.xDB2.IndexOf("Prajna") > 0 || Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToString().ToUpper().Trim() == "SIED_NOVO" || Ilitera.Data.SQLServer.EntitySQLServer.xDB2.IndexOf("Rophe") > 0 || Ilitera.Data.SQLServer.EntitySQLServer.xDB2.IndexOf("Safety") > 0 || Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper().IndexOf("GLOBAL") > 0 || Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper().IndexOf("VIA") > 0 || Ilitera.Data.SQLServer.EntitySQLServer._LocalServer.ToString().ToUpper().IndexOf("EY") > 0 || Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper().IndexOf("QTECK") > 0)
            {

                Cliente rCliente = new Cliente(laudoTecnico.nID_EMPR.Id);
                rCliente.IdJuridicaPai.Find();

                //pcmso.IdCliente.Find();
                //pcmso.IdCliente.IdJuridicaPai.Find();

                //if (pcmso.IdCliente.IdJuridicaPai.Id != 0)
                if (rCliente.IdJuridicaPai.Id != 0)
                {
                    //passar valor em campo, de forma a suprimir p�gina de identifica��o
                    Juridica zJuridica = new Juridica();

                    //zJuridica.Find(pcmso.IdCliente.IdJuridicaPai.Id);
                    zJuridica.Find(rCliente.IdJuridicaPai.Id);



                    //sDadosEmpresa = pcmso.IdCliente.GetDadosEmpresaHtml(DateTime.Now);
                    //sDadosEmpresa = rCliente.GetDadosEmpresaHtml(DateTime.Now);
                    sDadosEmpresa = rCliente.GetDadosEmpresaHtml(laudoTecnico.hDT_LAUDO);
                    //GetDadosEmpresaHtml_Obra_Prajna(pcmso.DataPcmso, numTrabExpostos);

                    xRazaoSocial_Emp = zJuridica.NomeCompleto;
                    xEndereco_Emp = zJuridica.GetEndereco().GetEnderecoCompleto().ToString();
                    xCidadeUF_Emp = zJuridica.GetEndereco().GetCidade().ToString() + " / " + zJuridica.GetEndereco().GetEstado().ToString();
                    xCEP_Emp = zJuridica.GetEndereco().Cep;
                    xCNPJ_Emp = Juridica.FormatarCnpj(zJuridica.GetCnpj().ToString());
                    xCNAE_Emp = zJuridica.IdCNAE.GetCodigo().ToString();
                    xGrauRisco_Emp = zJuridica.GetCnaeSesmt().GrauRisco.ToString();
                    xAtividade_Emp = zJuridica.IdCNAE.ToString();

                    Juridica zJuridica2 = new Juridica();
                    zJuridica2.Find(rCliente.Id);

                    Identificacao_Historico rInd = new Identificacao_Historico();
                    rInd.Find(" IdPessoa = " + rCliente.Id.ToString() + " and convert( smalldatetime, '" + laudoTecnico.hDT_LAUDO.ToString("dd/MM/yyyy", ptBr) + "',103 ) between Inicio_Vigencia and Termino_Vigencia ");

                    if (rInd.Id != 0)
                    {
                        xRazaoSocial_Obra = rInd.NomeCompleto;
                    }
                    else
                    {
                        xRazaoSocial_Obra = zJuridica2.NomeCompleto;
                    }


                    //ver se tem endere�o hist�rico
                    laudoTecnico.nID_EMPR.Find();

                    Endereco_Historico rEnd = new Endereco_Historico();
                    rEnd.Find(" IdPessoa = " + laudoTecnico.nID_EMPR.Id + " and  convert( smalldatetime, '" + laudoTecnico.hDT_LAUDO.ToString("dd/MM/yyyy", ptBr) + "',103 ) between Inicio_Vigencia and Termino_Vigencia ");

                    if (rEnd.Id != 0)
                    {
                        StringBuilder str = new StringBuilder();

                        TipoLogradouro rTp = new TipoLogradouro();
                        rTp.Find(rEnd.IdTipoLogradouro);

                        if (rTp.Id != 0)
                        {
                            str.Append(rTp.NomeAbreviado);
                        }

                        str.Append(" " + rEnd.Logradouro);

                        if (rEnd.Numero != string.Empty)
                            str.Append(" " + rEnd.Numero);

                        if (rEnd.Complemento != string.Empty && rEnd.Complemento.ToUpper() != "NULL")
                            str.Append(" " + rEnd.Complemento);

                        xEndereco_Obra = str.ToString();


                        xCidadeUF_Obra = rEnd.Municipio + " / " + rEnd.Uf;
                        xCEP_Obra = rEnd.Cep;
                    }
                    else
                    {
                        xEndereco_Obra = zJuridica2.GetEndereco().GetEnderecoCompleto().ToString();
                        xCidadeUF_Obra = zJuridica2.GetEndereco().GetCidade().ToString() + " / " + zJuridica2.GetEndereco().GetEstado().ToString();
                        xCEP_Obra = zJuridica2.GetEndereco().Cep;
                    }

                    if (rInd.Id != 0)
                    {
                        xCNPJ_Obra = Juridica.FormatarCnpj(rInd.CNPJ);
                    }
                    else
                    {
                        if (zJuridica2.CNO.Trim() != "")
                        {
                            xCNPJ_Obra = Juridica.FormatarCnpj(zJuridica2.GetCnpj().ToString()) + "    CNO ( " + zJuridica2.CNO.Trim() + " )";
                        }
                        else
                        {
                            xCNPJ_Obra = Juridica.FormatarCnpj(zJuridica2.GetCnpj().ToString());
                        }                        
                    }

                    xCNAE_Obra = zJuridica2.IdCNAE.GetCodigo().ToString();
                    xGrauRisco_Obra = zJuridica2.GetCnaeSesmt().GrauRisco.ToString();
                    xDescricaoCNAE_Obra = zJuridica2.IdCNAE.Descricao.ToString();

                }

            }


            if (laudoTecnico.Historico_Versao != null)
                newRow["Historico_Versao"] = laudoTecnico.Historico_Versao.Trim();
            else
                newRow["Historico_Versao"] = "";

            //Implementador           
            laudoTecnico.nID_IMPLEMENTACAO.Find();
            newRow = ds.Tables[0].NewRow();
            newRow["Implementador"] = laudoTecnico.nID_IMPLEMENTACAO.NomeCompleto;
            newRow["Implementador_Numero"] = laudoTecnico.nID_IMPLEMENTACAO.Numero;
            newRow["Implementador_Titulo"] = laudoTecnico.nID_IMPLEMENTACAO.Titulo;

            try
            {
                newRow["Assinatura_Implementador"] = Ilitera.Common.Fotos.GetByteFoto(laudoTecnico.nID_IMPLEMENTACAO.FotoAss);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }


            if (laudoTecnico.Historico_Versao != null)
                newRow["Historico_Versao"] = laudoTecnico.Historico_Versao.Trim();
            else
                newRow["Historico_Versao"] = "";


            newRow["Logotipo"] = xArq;

            newRow["Indice"] = ""; //this.zIndice;
            newRow["Indice2"] = ""; //this.zIndice2;



            if (Ilitera.Data.SQLServer.EntitySQLServer._LocalServer.ToString().ToUpper().IndexOf("EY") > 0 || Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToString().ToUpper().IndexOf("SHO") > 0 || Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper().IndexOf("VIA") > 0)
            {
                this.laudoTecnico.nID_EMPR.Find();
                newRow["Obs"] = this.laudoTecnico.nID_EMPR.Observacao;
            }
            else
                newRow["Obs"] = "";

            

            if (sDadosEmpresa.Trim() == "") newRow["DadosEmpresa"] = cliente.GetDadosEmpresaHtml(laudoTecnico.hDT_LAUDO, numTrabExpostos);
            else newRow["DadosEmpresa"] = sDadosEmpresa;




            if (cliente.ToString().Trim() != "John Deere SP - Alphaville") newRow["CarimboCNPJ"] = cliente.GetCarimboCnpjHtml(laudoTecnico.hDT_LAUDO);
            else newRow["CarimboCNPJ"] = "";





            //if (this.zObra == 0)
            //{
            //    newRow["DadosEmpresa"] = cliente.GetDadosEmpresaHtml(laudoTecnico.hDT_LAUDO, numTrabExpostos);
            //    //newRow["CarimboCNPJ"] = cliente.GetCarimboCnpjHtml(laudoTecnico.hDT_LAUDO);
            //    if (cliente.ToString().Trim() != "John Deere SP - Alphaville") newRow["CarimboCNPJ"] = cliente.GetCarimboCnpjHtml(laudoTecnico.hDT_LAUDO) + "<br>Data do Laudo:" + laudoTecnico.hDT_LAUDO.ToString("dd/MM/yyyy", ptBr);
            //    else newRow["CarimboCNPJ"] = "";

            //}
            //else
            //{
            //    LaudoTecnico zLaudo = new LaudoTecnico();

            //    zLaudo.Find(" nId_Empr = " + this.zObra.ToString() + " order by hDt_Laudo Desc");

            //    Ilitera.Common.Juridica xJuridica = new Ilitera.Common.Juridica();
            //    xJuridica.Find(this.zObra);

            //    if (this.Inibir_US == true)
            //    {

            //        Cliente zCliente = new Cliente(this.zObra);
                    
            //        newRow["DadosEmpresa"] = zCliente.GetDadosEmpresaHtml(zLaudo.hDT_LAUDO, numTrabExpostos, this.Inibir_US);
            //        newRow["CarimboCNPJ"] = zCliente.GetCarimboCnpjHtml(zLaudo.hDT_LAUDO) + "<br>Data do Laudo:" + zLaudo.hDT_LAUDO.ToString("dd/MM/yyyy", ptBr); ;
            //    }
            //    else
            //    {
            //        newRow["DadosEmpresa"] = cliente.GetDadosEmpresaHtml(zLaudo.hDT_LAUDO, numTrabExpostos, xJuridica);
            //        //newRow["CarimboCNPJ"] = cliente.GetCarimboCnpjHtml(zLaudo.hDT_LAUDO, xJuridica);
            //        newRow["CarimboCNPJ"] = cliente.GetCarimboCnpjHtml(zLaudo.hDT_LAUDO) + "<br>Data do Laudo:" + zLaudo.hDT_LAUDO.ToString("dd/MM/yyyy", ptBr); ;
            //    }

            //}


            //if (sDadosEmpresa.Trim() != "")
            //    newRow["DadosEmpresa"] = sDadosEmpresa;



            string xRevisao = "";

            if (laudoTecnico.ConclusaoPPRA.ToUpper().IndexOf("REVIS�O") >= 0)
            {
                xRevisao = "Revis�o: " + laudoTecnico.ConclusaoPPRA.Substring(laudoTecnico.ConclusaoPPRA.ToUpper().IndexOf("REVIS�O") + 8);
            }



            IFormatProvider theCultureInfo = new System.Globalization.CultureInfo("pt-Br", true);

            ArrayList zList = new LaudoTecnico().Find("nID_EMPR=" + laudoTecnico.nID_EMPR.Id.ToString().Trim()
                + " AND (nID_PEDIDO IN (SELECT IdPedido FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.Pedido WHERE DataConclusao IS NOT NULL) OR bAE =1)"
                + " AND hDT_Laudo > convert( smalldatetime, '" + laudoTecnico.hDT_LAUDO.ToString("dd/MM/yyyy").Trim().Substring(0, 10) + "' , 103 ) "
                + " ORDER BY hDT_LAUDO");

            string zData = "";

            if (laudoTecnico.hDT_LAUDO < new DateTime(2022, 1, 3))
            {
                //DateTime rData = new DateTime(2022, 1, 3);
                //zData = rData.ToString("dd/MM/yyyy").Trim().Substring(0, 10).Replace("/", "-");
                zData = "03 de janeiro de 2022";
            }
            else
            {
                //zData = rLaudo.hDT_LAUDO.AddDays(-1).ToString("dd/MM/yyyy").Trim().Substring(0, 10).Replace("/", "-");
                if (laudoTecnico.hDT_LAUDO.Day < 10)
                    zData = "0" + laudoTecnico.hDT_LAUDO.Day.ToString().Trim() + " de " + Ilitera.Common.Utility.GetMesExtenso(laudoTecnico.hDT_LAUDO.Month) + " de " + laudoTecnico.hDT_LAUDO.Year.ToString().Trim();
                else
                    zData = laudoTecnico.hDT_LAUDO.Day.ToString().Trim() + " de " + Ilitera.Common.Utility.GetMesExtenso(laudoTecnico.hDT_LAUDO.Month) + " de " + laudoTecnico.hDT_LAUDO.Year.ToString().Trim();
            }


            //foreach (LaudoTecnico rLaudo in zList)
            //{
            //    if (zData == "") zData = rLaudo.hDT_LAUDO.AddDays(-1).ToString("dd/MM/yyyy").Trim().Substring(0, 10).Replace("/", "-");
            //}

            //if (zData == "")
            //{
            //    //if (laudoTecnico.hDT_LAUDO.AddDays(364).Year == 2020 || laudoTecnico.hDT_LAUDO.Year == 2020)
            //    if (laudoTecnico.hDT_LAUDO.AddDays(365).Day != laudoTecnico.hDT_LAUDO.Day)
            //        newRow["Data"] = "de " + laudoTecnico.hDT_LAUDO.ToString("dd-MM-yyyy") + " at�  " + laudoTecnico.hDT_LAUDO.AddDays(365).ToString("dd-MM-yyyy") + "        " + xRevisao;
            //    else
            //        newRow["Data"] = "de " + laudoTecnico.hDT_LAUDO.ToString("dd-MM-yyyy") + " at�  " + laudoTecnico.hDT_LAUDO.AddDays(364).ToString("dd-MM-yyyy") + "        " + xRevisao;
            //}
            //else
            //{
            //    newRow["Data"] = "de " + laudoTecnico.hDT_LAUDO.ToString("dd-MM-yyyy") + " at�  " + zData + "         " + xRevisao;
            //}


            //newRow["Data"] = zData + "    " + xRevisao; ; //"03 de janeiro de 2022";; // "03 de janeiro de 2022";


            if (cliente.PGR_Inserir_Vigencia == true)
            {
                if (laudoTecnico.hDT_LAUDO.Day < 10)
                    zData = "Vig�ncia do programa  " + "0" + laudoTecnico.hDT_LAUDO.Day.ToString().Trim() + " de " + Ilitera.Common.Utility.GetMesExtenso(laudoTecnico.hDT_LAUDO.Month) + " de " + laudoTecnico.hDT_LAUDO.Year.ToString().Trim() + "  a  " + "0" + laudoTecnico.hDT_LAUDO.Day.ToString().Trim() + " de " + Ilitera.Common.Utility.GetMesExtenso(laudoTecnico.hDT_LAUDO.Month) + " de " + laudoTecnico.hDT_LAUDO.AddYears(2).Year.ToString().Trim();
                else
                    zData = "Vig�ncia do programa  " + laudoTecnico.hDT_LAUDO.Day.ToString().Trim() + " de " + Ilitera.Common.Utility.GetMesExtenso(laudoTecnico.hDT_LAUDO.Month) + " de " + laudoTecnico.hDT_LAUDO.Year.ToString().Trim() + "  a  " + laudoTecnico.hDT_LAUDO.Day.ToString().Trim() + " de " + Ilitera.Common.Utility.GetMesExtenso(laudoTecnico.hDT_LAUDO.Month) + " de " + laudoTecnico.hDT_LAUDO.AddYears(2).Year.ToString().Trim();

                newRow["Data"] = zData;
            }
            else
            {
                newRow["Data"] = "Data de Elabora��o do PGR e in�cio de suas Atualiza��es " + zData + "    " + xRevisao;  //"03 de janeiro de 2022";
            }





            newRow["IsAntecipacaoBSH"] = false;
            newRow["IsComissao"] = !(laudoTecnico.nID_COMISSAO_1.Id == 0 &&
                                            laudoTecnico.nID_COMISSAO_2.Id != 0 &&
                                            laudoTecnico.nID_COMISSAO_3.Id == 0);

            //newRow["RepresentanteLegal"] = laudoTecnico.ResponsavelLegal.NomeCompleto;
            newRow["RepresentanteLegalTitulo"] = laudoTecnico.ResponsavelLegal.Titulo;

            laudoTecnico.nID_EMPR.Find();
            laudoTecnico.nID_EMPR.IdCNAE.Find();
            laudoTecnico.nID_EMPR.IdJuridicaPai.Find();

            if (laudoTecnico.nID_EMPR.IdJuridicaPai.Id != 0 && laudoTecnico.nID_EMPR.IdJuridicaPai.Id != laudoTecnico.nID_EMPR.Id)
            {
                newRow["RepresentanteLegal"] = "		A empresa " + laudoTecnico.nID_EMPR.IdJuridicaPai.NomeCompleto + ", unidade de servi�o " + laudoTecnico.nID_EMPR.NomeAbreviado + ", CNPJ " + laudoTecnico.nID_EMPR.GetCnpj().ToString() +
                      ", CNAE " + laudoTecnico.nID_EMPR.IdCNAE.Codigo + ", localizada no endere�o " + laudoTecnico.nID_EMPR.GetEndereco().IdTipoLogradouro.NomeAbreviado + " " +
                      laudoTecnico.nID_EMPR.GetEndereco().Logradouro + " " + laudoTecnico.nID_EMPR.GetEndereco().Numero + " - " + laudoTecnico.nID_EMPR.GetEndereco().GetCidade().ToString() + " / " + laudoTecnico.nID_EMPR.GetEndereco().Uf + ", por seu respons�vel legal, infra-mencionado, objetivando cumprir e fazer cumprir as normas de seguran�a do trabalho e em particular a determina��o legal exarada atrav�s da Portaria SEPRT n� 6.730/2020 � NR 1 que estabelece a obrigatoriedade da elabora��o e implementa��o pelas empresas do PGR - PROGRAMA DE GERENCIAMENTO DE RISCOS, resolve nomear equipe de pessoas, que a crit�rio da empresa s�o capazes de desenvolver o disposto na referida Norma Regulamentadora n� 1, bem como efetuar sempre que necess�ria e pelo menos uma vez a cada dois anos ou a cada tr�s anos, caso a organiza��o possua certifica��es do sistema de gest�o de SST, uma revis�o para aferi��o de seus resultados e estabelecimento de plano de a��o para o novo per�odo. Portando, a responsabilidade t�cnica do presente documento passa a ser da empresa e dos profissionais abaixo mencionados e firmados. Ainda, conforme o item 1.5.7.3.3.1, o hist�rico das atualiza��es deve ser mantido por um per�odo m�nimo de 20 (vinte) anos ou pelo per�odo estabelecido em normatiza��o espec�fica que venha a vigorar.";
                //+ System.Environment.NewLine + "		 Conforme o item 1.5.7.3.3.1 � o hist�rico das atualiza��es deve ser mantido por um per�odo m�nimo de 20 (vinte) anos ou pelo per�odo estabelecido em normatiza��o espec�fica. ";
            }
            else
            {
                newRow["RepresentanteLegal"] = "		A empresa " + laudoTecnico.nID_EMPR.NomeCompleto + ", CNPJ " + laudoTecnico.nID_EMPR.GetCnpj().ToString() +
                      ", CNAE " + laudoTecnico.nID_EMPR.IdCNAE.Codigo + ", localizada no endere�o " + laudoTecnico.nID_EMPR.GetEndereco().IdTipoLogradouro.NomeAbreviado + " " +
                      laudoTecnico.nID_EMPR.GetEndereco().Logradouro + " " + laudoTecnico.nID_EMPR.GetEndereco().Numero + " - " + laudoTecnico.nID_EMPR.GetEndereco().GetCidade().ToString() + " / " + laudoTecnico.nID_EMPR.GetEndereco().Uf + ", por seu respons�vel legal, infra-mencionado, objetivando cumprir e fazer cumprir as normas de seguran�a do trabalho e em particular a determina��o legal exarada atrav�s da Portaria SEPRT n� 6.730/2020 � NR 1 que estabelece a obrigatoriedade da elabora��o e implementa��o pelas empresas do PGR - PROGRAMA DE GERENCIAMENTO DE RISCOS, resolve nomear equipe de pessoas, que a crit�rio da empresa s�o capazes de desenvolver o disposto na referida Norma Regulamentadora n� 1, bem como efetuar sempre que necess�ria e pelo menos uma vez a cada dois anos ou a cada tr�s anos, caso a organiza��o possua certifica��es do sistema de gest�o de SST, uma revis�o para aferi��o de seus resultados e estabelecimento de plano de a��o para o novo per�odo. Portando, a responsabilidade t�cnica do presente documento passa a ser da empresa e dos profissionais abaixo mencionados e firmados. Ainda, conforme o item 1.5.7.3.3.1, o hist�rico das atualiza��es deve ser mantido por um per�odo m�nimo de 20 (vinte) anos ou pelo per�odo estabelecido em normatiza��o espec�fica que venha a vigorar.";
                //+ System.Environment.NewLine + "		 Conforme o item 1.5.7.3.3.1 � o hist�rico das atualiza��es deve ser mantido por um per�odo m�nimo de 20 (vinte) anos ou pelo per�odo estabelecido em normatiza��o espec�fica. ";

            }


            newRow["RazaoSocial_Emp"] = xRazaoSocial_Emp;
            newRow["RazaoSocial_Obra"] = xRazaoSocial_Obra;

            newRow["Endereco_Emp"] = xEndereco_Emp;
            newRow["CidadeUF_Emp"] = xCidadeUF_Emp;
            newRow["CEP_Emp"] = xCEP_Emp;
            newRow["CNPJ_Emp"] = xCNPJ_Emp;
            newRow["CNAE_Emp"] = xCNAE_Emp;
            newRow["GrauRisco_Emp"] = xGrauRisco_Emp;
            newRow["Atividade_Emp"] = xAtividade_Emp;

            newRow["Endereco_Obra"] = xEndereco_Obra;
            newRow["CidadeUF_Obra"] = xCidadeUF_Obra;
            newRow["CEP_Obra"] = xCEP_Obra;
            newRow["CNPJ_Obra"] = xCNPJ_Obra;
            newRow["CNAE_Obra"] = xCNAE_Obra;
            newRow["GrauRisco_Obra"] = xGrauRisco_Obra;
            newRow["DescricaoCNAE_Obra"] = xDescricaoCNAE_Obra;


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



        private DataSet GetIntroducaoPGR_PAE()
        {
            DataTable table = new DataTable("Result");
            table.Columns.Add("PAE1", Type.GetType("System.String"));
            table.Columns.Add("PAE2", Type.GetType("System.String"));
            table.Columns.Add("Base", Type.GetType("System.String"));


            DataSet ds = new DataSet();

            ds.Tables.Add(table);

            DataRow newRow = ds.Tables[0].NewRow();

            PAE xHist = new PAE();
            xHist.Find(" nId_Laud_Tec = " + this.laudoTecnico.Id.ToString());

            string xPadrao = "";

            if (xHist.Id != 0)
            {
                xPadrao = xHist.TextoPAE;
            }
            else
            {
                xPadrao = "";
            }


            if (xPadrao.Trim() == "")
            {
                xPadrao = "1. Introdu��o" + System.Environment.NewLine + System.Environment.NewLine +
                          "O plano foi desenvolvido de forma despertar a consci�ncia de todos para a necessidade de prevenir acidentes e a propiciar respostas r�pidas e eficientes em eventuais situa��es emergenciais que tenham potencial para causar repercuss�es externas aos limites da empresa, possibilitando assim a minimiza��o de eventuais danos �s pessoas e ao patrim�nio, bem como impactos ao meio ambiente.";

                xPadrao = xPadrao + System.Environment.NewLine + System.Environment.NewLine +
                          "2. Procedimentos" + System.Environment.NewLine + System.Environment.NewLine +
                          "Os procedimentos aqui apresentados est�o fundamentados com base nos riscos de acidentes e potenciais necessidades de atua��o e resposta �s emerg�ncias j� identificadas, em rela��o �s caracter�sticas de arranjo f�sico, disposi��o das edifica��es e acessibilidade das instala��es da empresa." + System.Environment.NewLine + System.Environment.NewLine +
                          "Al�m da defini��o dos procedimentos emergenciais, o presente plano possui uma estrutura espec�fica de forma a: " + System.Environment.NewLine +
                          "� Definir a sequ�ncia de a��es para desencadeamento deste PAE - Plano de Atendimento a Emerg�ncias;" + System.Environment.NewLine +
                          "� Promover a integra��o das a��es de resposta �s emerg�ncias com os agentes de socorro externo - sabidamente SAMU e Corpo de Bombeiros, possibilitando assim o desencadeamento de atividades integradas e coordenadas, de modo que os melhores resultados em termos de preserva��o da integridade f�sica e da vida das pessoas, possam ser alcan�ados.";

                xPadrao = xPadrao + System.Environment.NewLine + System.Environment.NewLine +
                         "3. Avalia��o de riscos e Preven��o" + System.Environment.NewLine + System.Environment.NewLine +
                         "3.1 Avalia��o de riscos " + System.Environment.NewLine +
                         "A avalia��o de riscos aprovada no projeto t�cnico que embasou a aprova��o do nosso AVCB - Auto de Vistoria do Corpo de Bombeiros ou CLCB - Certificado de Licen�a do Corpo de Bombeiros, o qual encontra-se aprovado e vigente." + System.Environment.NewLine + System.Environment.NewLine +
                         "3.2 Medidas de Preven��o " + System.Environment.NewLine +
                         "Como forma de resposta a emerg�ncias de princ�pio de inc�ndio nestas �reas a empresa disp�e de recursos materiais para o combate a inc�ndio.";


                xPadrao = xPadrao + System.Environment.NewLine + System.Environment.NewLine +
                         "4 Acionamento do Plano";


                xPadrao = xPadrao + System.Environment.NewLine + System.Environment.NewLine +
                         "4.1 Comunica��o de Emerg�ncia" + System.Environment.NewLine +
                         "Qualquer funcion�rio que identificar situa��o de emerg�ncia como princ�pio de inc�ndio em qualquer �rea da empresa dever� imediatamente comunicar o brigadista mais pr�ximo. Caso n�o consiga localizar um brigadista, essa mesma pessoa dever� acionar uma botoeira de emerg�ncia." + System.Environment.NewLine +
                         "No caso de situa��o emergencial, ser� convocada a equipe da brigada de emerg�ncia, treinada, para reconhecer e iniciar as primeiras a��es de atendimento a emerg�ncias.";

                xPadrao = xPadrao + System.Environment.NewLine + System.Environment.NewLine +
                         "4.2 Acionamento da Brigada de Inc�ndio" + System.Environment.NewLine +
                         "Imediatamente ap�s o primeiro acionamento do alarme da emerg�ncia, o que indica que o local da emerg�ncia ainda � desconhecido, o l�der da brigada e os brigadistas dever�o reunir-se no ponto de encontro da brigada para avalia��o da situa��o e tomada de decis�o." + System.Environment.NewLine +
                         "Os l�deres da brigada dever�o participar juntamente com o t�cnico de seguran�a do trabalho e/ou gerente geral, da avalia��o da necessidade da mobiliza��o de aux�lio externo." + System.Environment.NewLine +
                         "O acionamento do SAMU, Corpo de Bombeiros ou Pol�cia Militar, dever� ser feito pela equipe de seguran�a do trabalho, ger�ncia ou de seguran�a patrimonial, atrav�s dos telefones indicados abaixo. " + System.Environment.NewLine +
                         "�     SAMU 192" + System.Environment.NewLine +
                         "�     Corpo de bombeiros 193";

                xPadrao = xPadrao + System.Environment.NewLine + System.Environment.NewLine +
                         "Procurar relatar calmamente o acidente, explicando realmente o que est� acontecendo, bem como o local correto da ocorr�ncia - escrit�rio, produ��o, manuten��o etc. - Assim, a equipe de atendimento a emerg�ncia poder� vir preparada para atender adequadamente a ocorr�ncia. " + System.Environment.NewLine +
                         "Sendo confirmada a necessidade de abandono, a Brigada acionar� novamente o alarme, iniciando o abandono da �rea.";

                xPadrao = xPadrao + System.Environment.NewLine + System.Environment.NewLine +
                         "4.3 Abandono da �rea" + System.Environment.NewLine +
                         "Imediatamente ap�s acionamento do alarme de abandono, todos os funcion�rios que n�o fazem parte da brigada de emerg�ncia dever�o:" + System.Environment.NewLine +
                         "� Seguir imediatamente, sem qualquer atraso, as orienta��es da brigada, dirigirem-se com calma at� o ponto de encontro, colocando-se em fila, a partir das orienta��es do brigadista." + System.Environment.NewLine +
                         "� Aguardar a ordem de deslocamento." + System.Environment.NewLine +
                         "� Acompanhar o brigadista, mantendo a calma e conservando uma dist�ncia segura da pessoa a frente, at� chegar ao local de concentra��o." + System.Environment.NewLine +
                         "� Nunca postergar a sa�da ou movimentar-se no sentido contr�rio ao deslocamento para abandono, a fim de n�o comprometer o abandono nem interromper a fila." + System.Environment.NewLine +
                         "� N�o utilizar telefone, deixando as linhas livres para as comunica��es de emerg�ncia." + System.Environment.NewLine +
                         "� Caso o funcion�rio esteja com algum visitante, orient�-lo a seguir as orienta��es da Brigada de Emerg�ncia e acompanhar o sentido do abandono at� o ponto de concentra��o." + System.Environment.NewLine +
                         "� Caso haja pessoas com mobilidade reduzida permanente ou tempor�ria, a equipe de evacua��o dever� conduzir a remo��o, de acordo com o n�vel do pavimento em que essa(s) pessoa(s) estiver(em) localizada(s) no momento da ocorr�ncia, utilizando t�cnicas de transporte de apoio e privilegiando a remo��o pela plataforma dedicada e espec�fica para movimenta��o de Pessoas Portadoras de Necessidades Especiais.";

                xPadrao = xPadrao + System.Environment.NewLine + System.Environment.NewLine +
                         "4.4 Combate a Emerg�ncia" + System.Environment.NewLine +
                         "A brigada, dever� iniciar o combate a emerg�ncia utilizando os recursos e equipamentos espec�ficos para cada tipo de emerg�ncia.";


                xPadrao = xPadrao + System.Environment.NewLine + System.Environment.NewLine +
                         "5 Treinamentos" + System.Environment.NewLine +
                         "Os treinamentos do PAE ou de capacita��o de pessoas para a atua��o em situa��es de emerg�ncia devem ser avaliados e documentados de forma a subsidiar a atualiza��o e aprimoramento do plano." + System.Environment.NewLine +
                         "Periodicamente, em hor�rios pr�-definidos, efetuar treinamento te�rico e pr�tico do plano de emerg�ncia." + System.Environment.NewLine +
                         "Os recursos utilizados em treinamentos ou no atendimento a eventuais emerg�ncias dever�o ser prontamente repostos." + System.Environment.NewLine +
                         "Os funcion�rios, que n�o fazem parte da brigada, dever�o ser treinados periodicamente em exerc�cio pr�tico denominado simulado de abandono, para que recebam orienta��o adequada sobre como reagir a emerg�ncias.";


                xPadrao = xPadrao + System.Environment.NewLine + System.Environment.NewLine +
                        "6 Simula��o de Abandono e An�lise Cr�tica da Simula��o" + System.Environment.NewLine +
                        "Ap�s o simulado do abandono de �rea, a brigada de emerg�ncia, em conjunto com seus funcion�rios dever� analisar o plano de modo geral, para que sejam tomadas provid�ncias de forma a evitar poss�veis erros." + System.Environment.NewLine +
                        "� Reunir semestralmente para rever e reavaliar o plano de abandono." + System.Environment.NewLine +
                        "� Designar funcion�rios para todas as fun��es." + System.Environment.NewLine +
                        "� Manter listagens das pessoas, planilha de dados, plantas de emerg�ncia e organogramas atualizados em locais de f�cil acesso." + System.Environment.NewLine +
                        "� Simular exerc�cios de abandono e controlar seu tempo de acordo com a planta e contingente de pessoas." + System.Environment.NewLine +
                        "� Efetuar a simula��o quando houver o maior n�mero de pessoas na planta, anotando o comportamento e observa��es pertinentes.";

                xPadrao = xPadrao + System.Environment.NewLine + System.Environment.NewLine +
                        "7 Conclus�o" + System.Environment.NewLine +
                        "Os procedimentos de combate �s emerg�ncias foram estabelecidos com base nas poss�veis consequ�ncias decorrentes dos cen�rios acidentais e est�o associados aos eventuais danos e efeitos decorrentes de inc�ndio, explos�es, choque el�trico, acidentes pessoais etc.";
            }

            newRow["PAE1"] = xPadrao;




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
            table.Columns.Add("PrestadorIlitera", Type.GetType("System.String"));
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
            table.Columns.Add("PrestadorIlitera", Type.GetType("System.String"));
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
                        if (Ilitera.Data.SQLServer.EntitySQLServer.xDB2.IndexOf("Prajna") < 0)
                            newRow["TituloComissao"] = "PRESIDENTE";		
                    }
                    
                    {
                        newRow["TituloComissao"] = " ";
                    }                    

                    if ( laudoTecnico.Profissionais_Ilitera==true)
                    {
                        ArrayList zList = new Cedente().Find("NomeCedente is not null and NomeCedente <> '' ");
                        
                        foreach (Cedente rCed in zList)
                        {
                            newRow["PrestadorIlitera"] = rCed.NomeCedente;
                        }                        
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
                        newRow["TituloComissao"] = "RELATOR - ASSISTENTE T�CNICO";
                    }
                    else
                    {
                        newRow["TituloComissao"] = " ";
                    }
                    if (laudoTecnico.Profissionais_Ilitera == true)
                    {
                        ArrayList zList = new Cedente().Find("NomeCedente is not null  and NomeCedente <> '' ");

                        foreach (Cedente rCed in zList)
                        {
                            newRow["PrestadorIlitera"] = rCed.NomeCedente;
                        }
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
                        newRow["TituloComissao"] = "REVISOR - ASSISTENTE T�CNICO";						
                    }
                    else
                    {
                        newRow["TituloComissao"] = " ";
                    }

                    if (laudoTecnico.Profissionais_Ilitera == true)
                    {
                        ArrayList zList = new Cedente().Find("NomeCedente is not null  and NomeCedente <> '' ");

                        foreach (Cedente rCed in zList)
                        {
                            newRow["PrestadorIlitera"] = rCed.NomeCedente;
                        }
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
