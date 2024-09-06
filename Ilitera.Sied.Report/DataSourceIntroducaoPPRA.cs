using System;
using Ilitera.Opsa.Data;
using System.Data;
using Ilitera.Common;
using System.Collections;
using System.Text;

namespace Ilitera.Sied.Report
{
	public class DataSourceIntroducaoPPRA : DataSourceBase
	{
		private Cliente cliente;
        private LaudoTecnico laudoTecnico;
        private string zIndice="";
        private string zIndice2="";

        
        public DataSourceIntroducaoPPRA(Cliente cliente)
		{
			this.cliente = cliente;

            if (this.cliente.mirrorOld == null)
			    this.cliente.Find();
            
            this.laudoTecnico = LaudoTecnico.GetUltimoLaudo(this.cliente.Id);
		}

		public DataSourceIntroducaoPPRA(LaudoTecnico laudoTecnico)
		{
			this.laudoTecnico = laudoTecnico;
            this.cliente = this.laudoTecnico.nID_EMPR;

            if (this.cliente.mirrorOld==null)
                this.cliente.Find();
		}

        public DataSourceIntroducaoPPRA(Cliente cliente, LaudoTecnico laudoTecnico)
        {
            this.laudoTecnico = laudoTecnico;
            this.cliente = cliente;

            if (this.cliente.mirrorOld == null)
                this.cliente.Find();
        }

		public DataSourceIntroducaoPPRA(int IdLaudoTecnico)
		{
            this.laudoTecnico = new LaudoTecnico();
            this.laudoTecnico.Find(IdLaudoTecnico);

            this.cliente = laudoTecnico.nID_EMPR;
            if (this.cliente.mirrorOld == null)
			    this.cliente.Find();
		}	
		
		public RptIntroducaoPPRA GetReport()
		{
			RptIntroducaoPPRA report = new RptIntroducaoPPRA();
			report.SetDataSource(GetIntroducaoPPRA());
            this.zIndice = "";
            this.zIndice2 = "";
            report.OpenSubreport("FotoFachada").SetDataSource(DataSourceFotoFachada());
			report.OpenSubreport("Engenheiro").SetDataSource(GetEngenheiro());
			report.OpenSubreport("Prestador").SetDataSource(GetPrestador());
			report.Refresh();

            SetTempoProcessamento(report);

			return report;
		}


        public RptIntroducaoPPRA GetReportIndice(string zIndice, string zIndice2)
        {
            RptIntroducaoPPRA report = new RptIntroducaoPPRA();
            this.zIndice = zIndice;
            this.zIndice2 = zIndice2;
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

            table.Columns.Add("RazaoSocial_Obra", Type.GetType("System.String"));
            table.Columns.Add("Endereco_Obra", Type.GetType("System.String"));
            table.Columns.Add("CidadeUF_Obra", Type.GetType("System.String"));
            table.Columns.Add("CEP_Obra", Type.GetType("System.String"));
            table.Columns.Add("CNPJ_Obra", Type.GetType("System.String"));
            table.Columns.Add("CNAE_Obra", Type.GetType("System.String"));
            table.Columns.Add("GrauRisco_Obra", Type.GetType("System.String"));
            table.Columns.Add("DescricaoCNAE_Obra", Type.GetType("System.String"));

            table.Columns.Add("Base", Type.GetType("System.String"));

            table.Columns.Add("Implementador", Type.GetType("System.String"));
            table.Columns.Add("Implementador_Numero", Type.GetType("System.String"));
            table.Columns.Add("Implementador_Titulo", Type.GetType("System.String"));
            table.Columns.Add("Indice", Type.GetType("System.String"));
            table.Columns.Add("Indice2", Type.GetType("System.String"));
            table.Columns.Add("Historico_Versao", Type.GetType("System.String"));
            table.Columns.Add("Assinatura_Implementador", Type.GetType("System.Byte[]"));

            DataSet ds = new DataSet();

			ds.Tables.Add(table);

			DataRow newRow = ds.Tables[0].NewRow();

            if (laudoTecnico.ResponsavelLegal.mirrorOld == null)
                laudoTecnico.ResponsavelLegal.Find();



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


            string xArq;

            //if (Ilitera.Data.SQLServer.EntitySQLServer.xDB2.IndexOf("Daiti") > 0)
            //{
            //    xArq = "http://54.94.157.244/driveI/fotosDocsDigitais/_Daiti/Relatorio_Empresa.jpg";                
            //}
            //else if (Ilitera.Data.SQLServer.EntitySQLServer.xDB2.IndexOf("Rophe") > 0)
            //{
            //    xArq = "http://54.94.157.244/driveI/fotosDocsDigitais/_Rophe/Relatorio_Empresa.jpg";                
            //}
            //else if (Ilitera.Data.SQLServer.EntitySQLServer.xDB2.IndexOf("Prajna") > 0)
            //{
            //    xArq = "http://54.94.157.244/driveI/fotosDocsDigitais/_Prajna/Relatorio_Empresa.jpg";                
            //}
            //else if (Ilitera.Data.SQLServer.EntitySQLServer.xDB2.IndexOf("Fiore") > 0)
            //{
            //    xArq = "http://54.94.157.244/driveI/fotosDocsDigitais/_Fiore/Relatorio_Empresa.jpg";                
            //}
            //else if (Ilitera.Data.SQLServer.EntitySQLServer.xDB2.IndexOf("Mappas") > 0)
            //{
            //    xArq = "http://54.94.157.244/driveI/fotosDocsDigitais/_Mappas/Relatorio_Empresa.jpg";                
            //}
            //else
            //{
            //    xArq = "http://54.94.157.244/driveI/fotosDocsDigitais/_Ilitera/Relatorio_Empresa.jpg";                
            //}


            if (Ilitera.Data.SQLServer.EntitySQLServer.xDB2.IndexOf("Daiti") > 0)
            {
                xArq = "http://www.ilitera.net.br/driveI/fotosDocsDigitais/_Daiti/Relatorio_Empresa.jpg";
            }
            else if (Ilitera.Data.SQLServer.EntitySQLServer.xDB2.IndexOf("Rophe") > 0)
            {
                xArq = "http://www.ilitera.net.br/driveI/fotosDocsDigitais/_Rophe/Relatorio_Empresa.jpg";
            }
            else if (Ilitera.Data.SQLServer.EntitySQLServer.xDB2.IndexOf("Prajna") > 0)
            {
                xArq = "http://www.ilitera.net.br/driveI/fotosDocsDigitais/_Prajna/Relatorio_Empresa.jpg";
            }
            else if (Ilitera.Data.SQLServer.EntitySQLServer.xDB2.IndexOf("Fiore") > 0)
            {
                xArq = "http://www.ilitera.net.br/driveI/fotosDocsDigitais/_Fiore/Relatorio_Empresa.jpg";
            }
            else if (Ilitera.Data.SQLServer.EntitySQLServer.xDB2.IndexOf("Mappas") > 0)
            {
                xArq = "http://www.ilitera.net.br/driveI/fotosDocsDigitais/_Mappas/Relatorio_Empresa.jpg";
            }
            else
            {
                xArq = "http://www.ilitera.net.br/driveI/fotosDocsDigitais/_Ilitera/Relatorio_Empresa.jpg";
            }


            
            xArq = xArq.Replace("http://www.ilitera.net.br/driveI", "I:");
            xArq = xArq.Replace("/", "\\");


            //pegar total de número de trabalhadores expostos em todos os GHEs
            ArrayList listGhe = new Ghe().Find("nID_LAUD_TEC=" + laudoTecnico.Id + " ORDER BY tNO_FUNC");
            int numTrabExpostos = 0;


            Pcmso pcmso = new Pcmso();
            pcmso.Find("IdLaudoTecnico=" +  laudoTecnico.Id.ToString() );

            numTrabExpostos = pcmso.GetNumFuncionarios();
                      
            //esse cálculo não está 100%,  não posso simplesmente somar os GHEs,  pois um funcionário com duas classif.funcionais para o mesmo laudo,
            //vai ser somado duas vezes nesta totalização.   Acho que é pegar apenas o total de empregados alocados em GHEs, montando um select mesmo

            //se não tiver pcmso, ou se retornar zero, calcular por ghe
            if (numTrabExpostos < 1)
            {
                foreach (Ghe ghe in listGhe)
                {
                    numTrabExpostos = numTrabExpostos + ghe.GetNumeroEmpregadosExpostos(false);
                }
            }


            System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");



            if (Ilitera.Data.SQLServer.EntitySQLServer.xDB2.IndexOf("Prajna") > 0  || Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToString().ToUpper().Trim() == "SIED_NOVO" || Ilitera.Data.SQLServer.EntitySQLServer.xDB2.IndexOf("SHO") > 0 || Ilitera.Data.SQLServer.EntitySQLServer.xDB2.IndexOf("Safety") > 0 || Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper().IndexOf("GLOBAL") > 0 || Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper().IndexOf("VIA") > 0 || Ilitera.Data.SQLServer.EntitySQLServer._LocalServer.ToString().ToUpper().IndexOf("EY") > 0 || Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper().IndexOf("QTECK") > 0)
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
                        xCNPJ_Obra = Juridica.FormatarCnpj(zJuridica2.GetCnpj().ToString());
                    }

                    xCNAE_Obra = zJuridica2.IdCNAE.GetCodigo().ToString();
                    xGrauRisco_Obra = zJuridica2.GetCnaeSesmt().GrauRisco.ToString();
                    xDescricaoCNAE_Obra = zJuridica2.IdCNAE.Descricao.ToString();

                }

            }


            //Implementador           
            laudoTecnico.nID_IMPLEMENTACAO.Find();
            newRow = ds.Tables[0].NewRow();
            newRow["Implementador"] = laudoTecnico.nID_IMPLEMENTACAO.NomeCompleto;
            newRow["Implementador_Numero"] = laudoTecnico.nID_IMPLEMENTACAO.Numero;
            newRow["Implementador_Titulo"] = laudoTecnico.nID_IMPLEMENTACAO.Titulo;
            
            try
            {
                newRow["Assinatura_Implementador"] = Ilitera.Common.Fotos.GetByteFoto_Uri(laudoTecnico.nID_IMPLEMENTACAO.FotoAss);                
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

            newRow["Indice"] = this.zIndice;
            newRow["Indice2"] = this.zIndice2;

            
                if (Ilitera.Data.SQLServer.EntitySQLServer._LocalServer.ToString().ToUpper().IndexOf("EY") > 0 || Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper().IndexOf("VIA") > 0)
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

            //newRow["Data"] = laudoTecnico.hDT_LAUDO.ToString("dd-MM-yyyy");




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

            foreach (LaudoTecnico rLaudo in zList)
            {
                if (zData == "") zData = rLaudo.hDT_LAUDO.AddDays(-1).ToString("dd/MM/yyyy").Trim().Substring(0, 10).Replace("/", "-");
            }

            if (zData == "")
            {
                //if (laudoTecnico.hDT_LAUDO.AddDays(364).Year == 2020 || laudoTecnico.hDT_LAUDO.Year == 2020)
                if (laudoTecnico.hDT_LAUDO.AddDays(365).Day != laudoTecnico.hDT_LAUDO.Day)
                    newRow["Data"] = "de " + laudoTecnico.hDT_LAUDO.ToString("dd-MM-yyyy") + " até  " + laudoTecnico.hDT_LAUDO.AddDays(365).ToString("dd-MM-yyyy") + "        " + xRevisao;
                else
                    newRow["Data"] = "de " + laudoTecnico.hDT_LAUDO.ToString("dd-MM-yyyy") + " até  " + laudoTecnico.hDT_LAUDO.AddDays(364).ToString("dd-MM-yyyy") + "        " + xRevisao;
            }
            else
            {
                newRow["Data"] = "de " + laudoTecnico.hDT_LAUDO.ToString("dd-MM-yyyy") + " até  " + zData + "        " + xRevisao;
            }
            




            newRow["IsAntecipacaoBSH"] = false;
            newRow["IsComissao"] = !(laudoTecnico.nID_COMISSAO_1.Id == 0 &&
                                            laudoTecnico.nID_COMISSAO_2.Id != 0 &&
                                            laudoTecnico.nID_COMISSAO_3.Id == 0);
            //newRow["RepresentanteLegal"] = laudoTecnico.ResponsavelLegal.NomeCompleto;
            newRow["RepresentanteLegalTitulo"] = laudoTecnico.ResponsavelLegal.Titulo;


            if (laudoTecnico.nID_EMPR.IdJuridicaPai.Id != 0 && laudoTecnico.nID_EMPR.IdJuridicaPai.Id != laudoTecnico.nID_EMPR.Id)
            {
                newRow["RepresentanteLegal"] = "		A empresa " + laudoTecnico.nID_EMPR.IdJuridicaPai.NomeCompleto + ", unidade de serviço " + laudoTecnico.nID_EMPR.NomeAbreviado + ", CNPJ " + laudoTecnico.nID_EMPR.GetCnpj().ToString() +
                      ", CNAE " + laudoTecnico.nID_EMPR.IdCNAE.Codigo + ", localizada no endereço " + laudoTecnico.nID_EMPR.GetEndereco().IdTipoLogradouro.NomeAbreviado + " " +
                      laudoTecnico.nID_EMPR.GetEndereco().Logradouro + " " + laudoTecnico.nID_EMPR.GetEndereco().Numero + " - " + laudoTecnico.nID_EMPR.GetEndereco().GetCidade().ToString() + " / " + laudoTecnico.nID_EMPR.GetEndereco().Uf + ", por seu responsável legal, infra-mencionado, objetivando cumprir e fazer cumprir as normas de segurança do trabalho e em particular a determinação legal exarada através da Portaria MTE nº 25/94 - NR 9 que estabelece a obrigatoriedade da elaboração e implementação pelas empresas do PROGRAMA DE PREVENÇÃO DE RISCOS AMBIENTAIS - PPRA, resolve nomear equipe de pessoas, profissionais habilitados legalmente, que a critério da empresa são capazes de desenvolver o disposto na referida Norma Regulamentadora nº 9, bem como efetuar sempre que necessária e pelo menos uma vez ao ano, uma análise global para avaliação do seu desenvolvimento e estabelecimento de novas metas e prioridades. Portanto a responsabilidade técnica do presente documento passa a ser da empresa e dos profissionais abaixo mencionados e firmados." +
                      System.Environment.NewLine + "		Conforme item 9.3.8.1 - Deverá ser mantido pelo empregador ou instituição um regime de dados, estruturado de forma a constituir um histórico técnico e administrativo do desenvolvimento do PPRA.  Item 9.8.3.2  Os dados deverão ser mantidos por um período mínimo de 20 (vinte) anos.";
            }
            else
            {
                newRow["RepresentanteLegal"] = "		A empresa " + laudoTecnico.nID_EMPR.NomeCompleto + ", CNPJ " + laudoTecnico.nID_EMPR.GetCnpj().ToString() +
                      ", CNAE " + laudoTecnico.nID_EMPR.IdCNAE.Codigo + ", localizada no endereço " + laudoTecnico.nID_EMPR.GetEndereco().IdTipoLogradouro.NomeAbreviado + " " +
                      laudoTecnico.nID_EMPR.GetEndereco().Logradouro + " " + laudoTecnico.nID_EMPR.GetEndereco().Numero + " - " + laudoTecnico.nID_EMPR.GetEndereco().GetCidade().ToString() + " / " + laudoTecnico.nID_EMPR.GetEndereco().Uf + ", por seu responsável legal, infra-mencionado, objetivando cumprir e fazer cumprir as normas de segurança do trabalho e em particular a determinação legal exarada através da Portaria MTE nº 25/94 - NR 9 que estabelece a obrigatoriedade da elaboração e implementação pelas empresas do PROGRAMA DE PREVENÇÃO DE RISCOS AMBIENTAIS - PPRA, resolve nomear equipe de pessoas, profissionais habilitados legalmente, que a critério da empresa são capazes de desenvolver o disposto na referida Norma Regulamentadora nº 9, bem como efetuar sempre que necessária e pelo menos uma vez ao ano, uma análise global para avaliação do seu desenvolvimento e estabelecimento de novas metas e prioridades. Portanto a responsabilidade técnica do presente documento passa a ser da empresa e dos profissionais abaixo mencionados e firmados." +
                      System.Environment.NewLine + "		Conforme item 9.3.8.1 - Deverá ser mantido pelo empregador ou instituição um regime de dados, estruturado de forma a constituir um histórico técnico e administrativo do desenvolvimento do PPRA.  Item 9.8.3.2  Os dados deverão ser mantidos por um período mínimo de 20 (vinte) anos.";
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
					newRow["iAssinatura"] = Ilitera.Common.Fotos.GetByteFoto_Uri(laudoTecnico.nID_COMISSAO_2.FotoAss);
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
                //System.Diagnostics.Trace.WriteLine(ex.Message);
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
						newRow["iAssinatura"] = Ilitera.Common.Fotos.GetByteFoto_Uri(laudoTecnico.nID_COMISSAO_1.FotoAss);
					}
					catch (Exception ex)
					{
                        System.Diagnostics.Debug.WriteLine(ex.Message);
                        //System.Diagnostics.Trace.WriteLine(ex.Message);
					}

                    if (laudoTecnico.nID_COMISSAO_1.NomeCompleto.Trim() != "")
                    {
                        if (Ilitera.Data.SQLServer.EntitySQLServer.xDB2.IndexOf("Prajna") < 0)
                            newRow["TituloComissao"] = "PRESIDENTE";
                    }
                    else
                    {
                        newRow["TituloComissao"] = " ";
                    }

                    if (laudoTecnico.Profissionais_Ilitera == true)
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
						newRow["iAssinatura"]	= Ilitera.Common.Fotos.GetByteFoto_Uri(laudoTecnico.nID_COMISSAO_2.FotoAss);
					}
					catch (Exception ex)
					{
                        System.Diagnostics.Debug.WriteLine(ex.Message);
                        //System.Diagnostics.Trace.WriteLine(ex.Message);
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
                        ArrayList zList = new Cedente().Find("NomeCedente is not null and NomeCedente <> '' ");

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
						newRow["iAssinatura"] = Ilitera.Common.Fotos.GetByteFoto_Uri(laudoTecnico.nID_COMISSAO_3.FotoAss);
					}
					catch (Exception ex)
					{
                        System.Diagnostics.Debug.WriteLine(ex.Message);
                        //System.Diagnostics.Trace.WriteLine(ex.Message);
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
                    //System.Diagnostics.Trace.WriteLine(ex.Message);
				}
			}
			return ds;
		}	
	}
}
