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


            //pegar total de número de trabalhadores expostos em todos os GHEs
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

            //esse cálculo não está 100%,  não posso simplesmente somar os GHEs,  pois um funcionário com duas classif.funcionais para o mesmo laudo,
            //vai ser somado duas vezes nesta totalização.   Acho que é pegar apenas o total de empregados alocados em GHEs, montando um select mesmo

            //se não tiver pcmso, ou se retornar zero, calcular por ghe


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
                    //passar valor em campo, de forma a suprimir página de identificação
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


                    //ver se tem endereço histórico
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

            if (laudoTecnico.ConclusaoPPRA.ToUpper().IndexOf("REVISÃO") >= 0)
            {
                xRevisao = "Revisão: " + laudoTecnico.ConclusaoPPRA.Substring(laudoTecnico.ConclusaoPPRA.ToUpper().IndexOf("REVISÃO") + 8);
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
            //        newRow["Data"] = "de " + laudoTecnico.hDT_LAUDO.ToString("dd-MM-yyyy") + " até  " + laudoTecnico.hDT_LAUDO.AddDays(365).ToString("dd-MM-yyyy") + "        " + xRevisao;
            //    else
            //        newRow["Data"] = "de " + laudoTecnico.hDT_LAUDO.ToString("dd-MM-yyyy") + " até  " + laudoTecnico.hDT_LAUDO.AddDays(364).ToString("dd-MM-yyyy") + "        " + xRevisao;
            //}
            //else
            //{
            //    newRow["Data"] = "de " + laudoTecnico.hDT_LAUDO.ToString("dd-MM-yyyy") + " até  " + zData + "         " + xRevisao;
            //}


            //newRow["Data"] = zData + "    " + xRevisao; ; //"03 de janeiro de 2022";; // "03 de janeiro de 2022";


            if (cliente.PGR_Inserir_Vigencia == true)
            {
                if (laudoTecnico.hDT_LAUDO.Day < 10)
                    zData = "Vigência do programa  " + "0" + laudoTecnico.hDT_LAUDO.Day.ToString().Trim() + " de " + Ilitera.Common.Utility.GetMesExtenso(laudoTecnico.hDT_LAUDO.Month) + " de " + laudoTecnico.hDT_LAUDO.Year.ToString().Trim() + "  a  " + "0" + laudoTecnico.hDT_LAUDO.Day.ToString().Trim() + " de " + Ilitera.Common.Utility.GetMesExtenso(laudoTecnico.hDT_LAUDO.Month) + " de " + laudoTecnico.hDT_LAUDO.AddYears(2).Year.ToString().Trim();
                else
                    zData = "Vigência do programa  " + laudoTecnico.hDT_LAUDO.Day.ToString().Trim() + " de " + Ilitera.Common.Utility.GetMesExtenso(laudoTecnico.hDT_LAUDO.Month) + " de " + laudoTecnico.hDT_LAUDO.Year.ToString().Trim() + "  a  " + laudoTecnico.hDT_LAUDO.Day.ToString().Trim() + " de " + Ilitera.Common.Utility.GetMesExtenso(laudoTecnico.hDT_LAUDO.Month) + " de " + laudoTecnico.hDT_LAUDO.AddYears(2).Year.ToString().Trim();

                newRow["Data"] = zData;
            }
            else
            {
                newRow["Data"] = "Data de Elaboração do PGR e início de suas Atualizações " + zData + "    " + xRevisao;  //"03 de janeiro de 2022";
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
                newRow["RepresentanteLegal"] = "		A empresa " + laudoTecnico.nID_EMPR.IdJuridicaPai.NomeCompleto + ", unidade de serviço " + laudoTecnico.nID_EMPR.NomeAbreviado + ", CNPJ " + laudoTecnico.nID_EMPR.GetCnpj().ToString() +
                      ", CNAE " + laudoTecnico.nID_EMPR.IdCNAE.Codigo + ", localizada no endereço " + laudoTecnico.nID_EMPR.GetEndereco().IdTipoLogradouro.NomeAbreviado + " " +
                      laudoTecnico.nID_EMPR.GetEndereco().Logradouro + " " + laudoTecnico.nID_EMPR.GetEndereco().Numero + " - " + laudoTecnico.nID_EMPR.GetEndereco().GetCidade().ToString() + " / " + laudoTecnico.nID_EMPR.GetEndereco().Uf + ", por seu responsável legal, infra-mencionado, objetivando cumprir e fazer cumprir as normas de segurança do trabalho e em particular a determinação legal exarada através da Portaria SEPRT nº 6.730/2020 – NR 1 que estabelece a obrigatoriedade da elaboração e implementação pelas empresas do PGR - PROGRAMA DE GERENCIAMENTO DE RISCOS, resolve nomear equipe de pessoas, que a critério da empresa são capazes de desenvolver o disposto na referida Norma Regulamentadora nº 1, bem como efetuar sempre que necessária e pelo menos uma vez a cada dois anos ou a cada três anos, caso a organização possua certificações do sistema de gestão de SST, uma revisão para aferição de seus resultados e estabelecimento de plano de ação para o novo período. Portando, a responsabilidade técnica do presente documento passa a ser da empresa e dos profissionais abaixo mencionados e firmados. Ainda, conforme o item 1.5.7.3.3.1, o histórico das atualizações deve ser mantido por um período mínimo de 20 (vinte) anos ou pelo período estabelecido em normatização específica que venha a vigorar.";
                //+ System.Environment.NewLine + "		 Conforme o item 1.5.7.3.3.1 – o histórico das atualizações deve ser mantido por um período mínimo de 20 (vinte) anos ou pelo período estabelecido em normatização específica. ";
            }
            else
            {
                newRow["RepresentanteLegal"] = "		A empresa " + laudoTecnico.nID_EMPR.NomeCompleto + ", CNPJ " + laudoTecnico.nID_EMPR.GetCnpj().ToString() +
                      ", CNAE " + laudoTecnico.nID_EMPR.IdCNAE.Codigo + ", localizada no endereço " + laudoTecnico.nID_EMPR.GetEndereco().IdTipoLogradouro.NomeAbreviado + " " +
                      laudoTecnico.nID_EMPR.GetEndereco().Logradouro + " " + laudoTecnico.nID_EMPR.GetEndereco().Numero + " - " + laudoTecnico.nID_EMPR.GetEndereco().GetCidade().ToString() + " / " + laudoTecnico.nID_EMPR.GetEndereco().Uf + ", por seu responsável legal, infra-mencionado, objetivando cumprir e fazer cumprir as normas de segurança do trabalho e em particular a determinação legal exarada através da Portaria SEPRT nº 6.730/2020 – NR 1 que estabelece a obrigatoriedade da elaboração e implementação pelas empresas do PGR - PROGRAMA DE GERENCIAMENTO DE RISCOS, resolve nomear equipe de pessoas, que a critério da empresa são capazes de desenvolver o disposto na referida Norma Regulamentadora nº 1, bem como efetuar sempre que necessária e pelo menos uma vez a cada dois anos ou a cada três anos, caso a organização possua certificações do sistema de gestão de SST, uma revisão para aferição de seus resultados e estabelecimento de plano de ação para o novo período. Portando, a responsabilidade técnica do presente documento passa a ser da empresa e dos profissionais abaixo mencionados e firmados. Ainda, conforme o item 1.5.7.3.3.1, o histórico das atualizações deve ser mantido por um período mínimo de 20 (vinte) anos ou pelo período estabelecido em normatização específica que venha a vigorar.";
                //+ System.Environment.NewLine + "		 Conforme o item 1.5.7.3.3.1 – o histórico das atualizações deve ser mantido por um período mínimo de 20 (vinte) anos ou pelo período estabelecido em normatização específica. ";

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
                xPadrao = "1. Introdução" + System.Environment.NewLine + System.Environment.NewLine +
                          "O plano foi desenvolvido de forma despertar a consciência de todos para a necessidade de prevenir acidentes e a propiciar respostas rápidas e eficientes em eventuais situações emergenciais que tenham potencial para causar repercussões externas aos limites da empresa, possibilitando assim a minimização de eventuais danos às pessoas e ao patrimônio, bem como impactos ao meio ambiente.";

                xPadrao = xPadrao + System.Environment.NewLine + System.Environment.NewLine +
                          "2. Procedimentos" + System.Environment.NewLine + System.Environment.NewLine +
                          "Os procedimentos aqui apresentados estão fundamentados com base nos riscos de acidentes e potenciais necessidades de atuação e resposta às emergências já identificadas, em relação às características de arranjo físico, disposição das edificações e acessibilidade das instalações da empresa." + System.Environment.NewLine + System.Environment.NewLine +
                          "Além da definição dos procedimentos emergenciais, o presente plano possui uma estrutura específica de forma a: " + System.Environment.NewLine +
                          "• Definir a sequência de ações para desencadeamento deste PAE - Plano de Atendimento a Emergências;" + System.Environment.NewLine +
                          "• Promover a integração das ações de resposta às emergências com os agentes de socorro externo - sabidamente SAMU e Corpo de Bombeiros, possibilitando assim o desencadeamento de atividades integradas e coordenadas, de modo que os melhores resultados em termos de preservação da integridade física e da vida das pessoas, possam ser alcançados.";

                xPadrao = xPadrao + System.Environment.NewLine + System.Environment.NewLine +
                         "3. Avaliação de riscos e Prevenção" + System.Environment.NewLine + System.Environment.NewLine +
                         "3.1 Avaliação de riscos " + System.Environment.NewLine +
                         "A avaliação de riscos aprovada no projeto técnico que embasou a aprovação do nosso AVCB - Auto de Vistoria do Corpo de Bombeiros ou CLCB - Certificado de Licença do Corpo de Bombeiros, o qual encontra-se aprovado e vigente." + System.Environment.NewLine + System.Environment.NewLine +
                         "3.2 Medidas de Prevenção " + System.Environment.NewLine +
                         "Como forma de resposta a emergências de princípio de incêndio nestas áreas a empresa dispõe de recursos materiais para o combate a incêndio.";


                xPadrao = xPadrao + System.Environment.NewLine + System.Environment.NewLine +
                         "4 Acionamento do Plano";


                xPadrao = xPadrao + System.Environment.NewLine + System.Environment.NewLine +
                         "4.1 Comunicação de Emergência" + System.Environment.NewLine +
                         "Qualquer funcionário que identificar situação de emergência como princípio de incêndio em qualquer área da empresa deverá imediatamente comunicar o brigadista mais próximo. Caso não consiga localizar um brigadista, essa mesma pessoa deverá acionar uma botoeira de emergência." + System.Environment.NewLine +
                         "No caso de situação emergencial, será convocada a equipe da brigada de emergência, treinada, para reconhecer e iniciar as primeiras ações de atendimento a emergências.";

                xPadrao = xPadrao + System.Environment.NewLine + System.Environment.NewLine +
                         "4.2 Acionamento da Brigada de Incêndio" + System.Environment.NewLine +
                         "Imediatamente após o primeiro acionamento do alarme da emergência, o que indica que o local da emergência ainda é desconhecido, o líder da brigada e os brigadistas deverão reunir-se no ponto de encontro da brigada para avaliação da situação e tomada de decisão." + System.Environment.NewLine +
                         "Os líderes da brigada deverão participar juntamente com o técnico de segurança do trabalho e/ou gerente geral, da avaliação da necessidade da mobilização de auxílio externo." + System.Environment.NewLine +
                         "O acionamento do SAMU, Corpo de Bombeiros ou Polícia Militar, deverá ser feito pela equipe de segurança do trabalho, gerência ou de segurança patrimonial, através dos telefones indicados abaixo. " + System.Environment.NewLine +
                         "•     SAMU 192" + System.Environment.NewLine +
                         "•     Corpo de bombeiros 193";

                xPadrao = xPadrao + System.Environment.NewLine + System.Environment.NewLine +
                         "Procurar relatar calmamente o acidente, explicando realmente o que está acontecendo, bem como o local correto da ocorrência - escritório, produção, manutenção etc. - Assim, a equipe de atendimento a emergência poderá vir preparada para atender adequadamente a ocorrência. " + System.Environment.NewLine +
                         "Sendo confirmada a necessidade de abandono, a Brigada acionará novamente o alarme, iniciando o abandono da área.";

                xPadrao = xPadrao + System.Environment.NewLine + System.Environment.NewLine +
                         "4.3 Abandono da Área" + System.Environment.NewLine +
                         "Imediatamente após acionamento do alarme de abandono, todos os funcionários que não fazem parte da brigada de emergência deverão:" + System.Environment.NewLine +
                         "• Seguir imediatamente, sem qualquer atraso, as orientações da brigada, dirigirem-se com calma até o ponto de encontro, colocando-se em fila, a partir das orientações do brigadista." + System.Environment.NewLine +
                         "• Aguardar a ordem de deslocamento." + System.Environment.NewLine +
                         "• Acompanhar o brigadista, mantendo a calma e conservando uma distância segura da pessoa a frente, até chegar ao local de concentração." + System.Environment.NewLine +
                         "• Nunca postergar a saída ou movimentar-se no sentido contrário ao deslocamento para abandono, a fim de não comprometer o abandono nem interromper a fila." + System.Environment.NewLine +
                         "• Não utilizar telefone, deixando as linhas livres para as comunicações de emergência." + System.Environment.NewLine +
                         "• Caso o funcionário esteja com algum visitante, orientá-lo a seguir as orientações da Brigada de Emergência e acompanhar o sentido do abandono até o ponto de concentração." + System.Environment.NewLine +
                         "• Caso haja pessoas com mobilidade reduzida permanente ou temporária, a equipe de evacuação deverá conduzir a remoção, de acordo com o nível do pavimento em que essa(s) pessoa(s) estiver(em) localizada(s) no momento da ocorrência, utilizando técnicas de transporte de apoio e privilegiando a remoção pela plataforma dedicada e específica para movimentação de Pessoas Portadoras de Necessidades Especiais.";

                xPadrao = xPadrao + System.Environment.NewLine + System.Environment.NewLine +
                         "4.4 Combate a Emergência" + System.Environment.NewLine +
                         "A brigada, deverá iniciar o combate a emergência utilizando os recursos e equipamentos específicos para cada tipo de emergência.";


                xPadrao = xPadrao + System.Environment.NewLine + System.Environment.NewLine +
                         "5 Treinamentos" + System.Environment.NewLine +
                         "Os treinamentos do PAE ou de capacitação de pessoas para a atuação em situações de emergência devem ser avaliados e documentados de forma a subsidiar a atualização e aprimoramento do plano." + System.Environment.NewLine +
                         "Periodicamente, em horários pré-definidos, efetuar treinamento teórico e prático do plano de emergência." + System.Environment.NewLine +
                         "Os recursos utilizados em treinamentos ou no atendimento a eventuais emergências deverão ser prontamente repostos." + System.Environment.NewLine +
                         "Os funcionários, que não fazem parte da brigada, deverão ser treinados periodicamente em exercício prático denominado simulado de abandono, para que recebam orientação adequada sobre como reagir a emergências.";


                xPadrao = xPadrao + System.Environment.NewLine + System.Environment.NewLine +
                        "6 Simulação de Abandono e Análise Crítica da Simulação" + System.Environment.NewLine +
                        "Após o simulado do abandono de área, a brigada de emergência, em conjunto com seus funcionários deverá analisar o plano de modo geral, para que sejam tomadas providências de forma a evitar possíveis erros." + System.Environment.NewLine +
                        "• Reunir semestralmente para rever e reavaliar o plano de abandono." + System.Environment.NewLine +
                        "• Designar funcionários para todas as funções." + System.Environment.NewLine +
                        "• Manter listagens das pessoas, planilha de dados, plantas de emergência e organogramas atualizados em locais de fácil acesso." + System.Environment.NewLine +
                        "• Simular exercícios de abandono e controlar seu tempo de acordo com a planta e contingente de pessoas." + System.Environment.NewLine +
                        "• Efetuar a simulação quando houver o maior número de pessoas na planta, anotando o comportamento e observações pertinentes.";

                xPadrao = xPadrao + System.Environment.NewLine + System.Environment.NewLine +
                        "7 Conclusão" + System.Environment.NewLine +
                        "Os procedimentos de combate às emergências foram estabelecidos com base nas possíveis consequências decorrentes dos cenários acidentais e estão associados aos eventuais danos e efeitos decorrentes de incêndio, explosões, choque elétrico, acidentes pessoais etc.";
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
                        newRow["TituloComissao"] = "RELATOR - ASSISTENTE TÉCNICO";
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
                        newRow["TituloComissao"] = "REVISOR - ASSISTENTE TÉCNICO";						
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
