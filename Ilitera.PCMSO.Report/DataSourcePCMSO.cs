using System;
using System.Collections;
using System.Data;
using System.Text;
using System.Drawing;
using Ilitera.Data;
using Ilitera.Opsa.Data;
using Ilitera.Common;

namespace Ilitera.PCMSO.Report
{
    public class DataSourcePCMSO : DataSourceBase
	{
		private Pcmso pcmso;
        private Int32 zObra;
        private string zMedicosCoordenadores;

        public DataSourcePCMSO(Cliente cliente)
        {
            this.zMedicosCoordenadores = "S";

            ArrayList list = new Pcmso().FindMax("DataPcmso", "IdCliente=" + cliente.Id);

            if (list.Count == 1)
                this.pcmso = (Pcmso)list[0];
            else
                throw new Exception("A empresa " + cliente.NomeAbreviado + " não possui ainda nenhum PCMSO realizado!");
        }

		public DataSourcePCMSO(Pcmso pcmso)
		{
			this.pcmso = pcmso;
            this.zMedicosCoordenadores = "S";

            if (this.pcmso.IsFromWeb)
				throw new Exception("PCMSO de terceiros, não realizado pela Mestra!");
		}


        public DataSourcePCMSO(Pcmso pcmso, string zListaGHE, Int32 zObra)
        {
            this.pcmso = pcmso;            
            this.zObra = zObra;
            this.zMedicosCoordenadores = "S";

            if (this.pcmso.IsFromWeb)
                throw new Exception("PCMSO de terceiros, não realizado pela Ilitera!");
        }

		public RptPCMSO GetReport()
		{
			RptPCMSO report = new RptPCMSO();	
			report.SetDataSource(GetPCMSO());
            report.OpenSubreport("CoordenadorPcmso").SetDataSource(pcmso.GetDSCoordenador());
            //report.OpenSubreport("CoordenadorPcmso2").SetDataSource(pcmso.GetDSCoordenador());
			report.Refresh();
            SetTempoProcessamento(report);
			return report;
		}

        public RptPCMSO GetReport(string zMedicos)
        {
            
            this.zMedicosCoordenadores = zMedicos;

            RptPCMSO report = new RptPCMSO();
            report.SetDataSource(GetPCMSO());
            report.OpenSubreport("CoordenadorPcmso").SetDataSource(pcmso.GetDSCoordenador());
            //report.OpenSubreport("CoordenadorPcmso2").SetDataSource(pcmso.GetDSCoordenador());
            report.Refresh();
            SetTempoProcessamento(report);
            return report;
        }



        public RptPCMSO_Preposto GetReport_Preposto(string zMedicos, Int32 xIdPreposto)
        {
            this.zMedicosCoordenadores = zMedicos;

            RptPCMSO_Preposto report = new RptPCMSO_Preposto();
            report.SetDataSource(GetPCMSO());
            report.OpenSubreport("CoordenadorPcmso").SetDataSource(pcmso.GetDSCoordenador());
            report.OpenSubreport("Preposto").SetDataSource(pcmso.GetDSPreposto(xIdPreposto));
            report.Refresh();
            SetTempoProcessamento(report);
            return report;
        }


        private DataSet GetPCMSO()
        {
            DataTable table = new DataTable("Result");
            table.Columns.Add("DadosEmpresa", Type.GetType("System.String"));
            table.Columns.Add("FotoFachada", Type.GetType("System.String"));
            table.Columns.Add("Logotipo", Type.GetType("System.String"));
            table.Columns.Add("Vigencia", Type.GetType("System.String"));
            table.Columns.Add("NumFuncTotal", Type.GetType("System.String"));
            table.Columns.Add("NumFuncTotalMin", Type.GetType("System.String"));
            table.Columns.Add("NumFuncTotalMax", Type.GetType("System.String"));
            table.Columns.Add("NumFuncSexoMasc", Type.GetType("System.String"));
            table.Columns.Add("NumFuncSexoFem", Type.GetType("System.String"));
            table.Columns.Add("NumFuncMenores18", Type.GetType("System.String"));
            table.Columns.Add("NumFunc18a45", Type.GetType("System.String"));
            table.Columns.Add("NumFuncMaiores45", Type.GetType("System.String"));
            table.Columns.Add("NumFuncBeneficiario", Type.GetType("System.String"));
            table.Columns.Add("TurnosTrabalho", Type.GetType("System.String"));
            table.Columns.Add("AfastadosTrabalho", Type.GetType("System.String"));
            table.Columns.Add("NomeMedico", Type.GetType("System.String"));
            table.Columns.Add("EnderecoMedico", Type.GetType("System.String"));
            table.Columns.Add("FotoCRMMedico", Type.GetType("System.String"));
            table.Columns.Add("CRM", Type.GetType("System.String"));
            table.Columns.Add("Titulo", Type.GetType("System.String"));
            table.Columns.Add("Texto", Type.GetType("System.String"));
            table.Columns.Add("Texto2", Type.GetType("System.String"));
            table.Columns.Add("Texto3", Type.GetType("System.String"));
            table.Columns.Add("RetirarAcoesSaude", Type.GetType("System.Boolean"));
            table.Columns.Add("ComDadosCoordenador", Type.GetType("System.Boolean"));
            table.Columns.Add("SemAssinatura", Type.GetType("System.Boolean"));
            table.Columns.Add("CondicoesAmostragem", Type.GetType("System.String"));

            table.Columns.Add("Base", Type.GetType("System.String"));

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

            table.Columns.Add("Historico_Versao", Type.GetType("System.String"));
            table.Columns.Add("Titulo_Principal", Type.GetType("System.String"));

            DataSet ds = new DataSet();
            ds.Tables.Add(table);
            DataRow newRow;

            ArrayList list = new PcmsoDocumento().Find("IdPcmso=" + pcmso.Id + " ORDER BY NumOrdem");

            pcmso.IdCliente.Find();

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

            //if (Ilitera.Data.SQLServer.EntitySQLServer.xDB2.IndexOf("Prajna") > 0 || Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToString().ToUpper().Trim() == "SIED_NOVO" || Ilitera.Data.SQLServer.EntitySQLServer.xDB2.IndexOf("Rophe") > 0 || Ilitera.Data.SQLServer.EntitySQLServer._LocalServer.ToString().ToUpper().IndexOf("EY") > 0)
            if (Ilitera.Data.SQLServer.EntitySQLServer.xDB2.IndexOf("Prajna") > 0 || Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToString().ToUpper().Trim() == "SIED_NOVO" || Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper().IndexOf("SAFETY") > 0 || Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper().IndexOf("GLOBAL") > 0 || Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper().IndexOf("VIA") > 0 || Ilitera.Data.SQLServer.EntitySQLServer._LocalServer.ToString().ToUpper().IndexOf("EY") > 0)
            { 

                pcmso.IdCoordenador.Find();

                if (this.zObra == 0)
                {
                    sDadosEmpresa = pcmso.IdCliente.GetDadosEmpresaHtml(pcmso.DataPcmso);
                }
                else
                {
                    Ilitera.Common.Juridica xJuridica = new Ilitera.Common.Juridica();
                    xJuridica.Find(this.zObra);

                    sDadosEmpresa = pcmso.IdCliente.GetDadosEmpresaHtml(pcmso.DataPcmso, 0, xJuridica);
                }


                if (pcmso.IdCliente.IdJuridicaPai.Id != 0)
                {
                    //passar valor em campo, de forma a suprimir página de identificação

                    sDadosEmpresa = pcmso.IdCliente.GetDadosEmpresaHtml_Obra_Prajna(pcmso.DataPcmso, 0);

                    Juridica zJuridica = new Juridica();

                    zJuridica.Find(pcmso.IdCliente.IdJuridicaPai.Id);

                    xRazaoSocial_Emp = zJuridica.NomeCompleto;
                    xEndereco_Emp = zJuridica.GetEndereco().GetEnderecoCompleto().ToString();
                    xCidadeUF_Emp = zJuridica.GetEndereco().GetCidade().ToString() + " / " + zJuridica.GetEndereco().GetEstado().ToString();
                    xCEP_Emp = zJuridica.GetEndereco().Cep;
                    xCNPJ_Emp = Juridica.FormatarCnpj(zJuridica.GetCnpj().ToString());
                    xCNAE_Emp = zJuridica.IdCNAE.GetCodigo().ToString();
                    xGrauRisco_Emp = zJuridica.GetCnaeSesmt().GrauRisco.ToString();
                    xAtividade_Emp = zJuridica.IdCNAE.ToString();

                    Juridica zJuridica2 = new Juridica();
                    zJuridica2.Find(pcmso.IdCliente.Id);


                    pcmso.IdCliente.Find();

                    System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");

                    Identificacao_Historico rInd = new Identificacao_Historico();
                    rInd.Find(" IdPessoa = " + pcmso.IdCliente.Id.ToString() + " and convert( smalldatetime, '" + pcmso.DataPcmso.ToString("dd/MM/yyyy", ptBr) + "',103 ) between Inicio_Vigencia and Termino_Vigencia ");

                    if (rInd.Id != 0)
                    {
                        xRazaoSocial_Obra = rInd.NomeCompleto;
                    }
                    else
                    {
                        xRazaoSocial_Obra = zJuridica2.NomeCompleto;
                    }



                    //ver se tem endereço histórico
                    pcmso.IdCliente.Find();

                    Endereco_Historico rEnd = new Endereco_Historico();
                    rEnd.Find(" IdPessoa = " + pcmso.IdCliente.Id + " and convert( smalldatetime, '" + pcmso.DataPcmso.ToString("dd/MM/yyyy", ptBr) + "',103 ) between Inicio_Vigencia and Termino_Vigencia ");

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
            else
            {

                pcmso.IdCoordenador.Find();

                if (this.zObra == 0)
                {
                    sDadosEmpresa = pcmso.IdCliente.GetDadosEmpresaHtml(pcmso.DataPcmso);
                }
                else
                {
                    Ilitera.Common.Juridica xJuridica = new Ilitera.Common.Juridica();
                    xJuridica.Find(this.zObra);

                    sDadosEmpresa = pcmso.IdCliente.GetDadosEmpresaHtml(pcmso.DataPcmso, 0, xJuridica);
                }

            }


            string sFotoFachada = Ilitera.Common.Fotos.PathFoto_Uri(pcmso.IdCliente.GetFotoFachada());

            string sVigencia = pcmso.DataPcmso.ToString("dd-MM-yyyy")
                                + "\n a \n"
                                + pcmso.GetTerninoPcmso().ToString("dd-MM-yyyy");

            string sNumFuncTotal = pcmso.GetNumFuncionarios().ToString();
            string sNumFuncTotalMin = pcmso.NumFuncionarioMin.ToString();
            string sNumFuncTotalMax = pcmso.NumFuncionarioMax.ToString(); 
            string sNumFuncSexoMasc = pcmso.GetNumFuncionariosSexoMasculino().ToString();
            string sNumFuncSexoFem = pcmso.GetNumFuncionariosSexoFeminino().ToString();
            string sNumFuncMenores18 = pcmso.GetNumFuncionariosMenores18().ToString();
            string sNumFunc18a45 = (pcmso.GetNumFuncionarios() - pcmso.GetNumFuncionariosMenores18() - pcmso.GetNumFuncionariosMaiores45()).ToString();
            string sNumFuncMaiores45 = pcmso.GetNumFuncionariosMaiores45().ToString();
            string sNumFuncBeneficiario = pcmso.GetNumFuncionariosBeneficiarios().ToString();
            string sAfastadosTrabalho = pcmso.GetNumFuncionariosAfastados().ToString();


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


            foreach (PcmsoDocumento pcmsoDocumento in list)
            {
                newRow = ds.Tables[0].NewRow();

                PCMSO_Historico xHist = new PCMSO_Historico();
                xHist.Find(" IdPcmso = " + pcmso.Id.ToString());

                if (xHist.Id != 0)
                {
                    newRow["Historico_Versao"] = xHist.Historico;
                }
                else
                {
                    newRow["Historico_Versao"] = "";
                }

                if (pcmso.DataPcmso.Year >= 2022)
                    newRow["Titulo_Principal"] = @"""Esta Norma Regulamentadora - NR estabelece diretrizes e requisitos para o desenvolvimento do Programa de Controle Médico de Saúde Ocupacional -PCMSO nas organizações, com o objetivo de proteger e preservar a saúde de seus empregados em relação aos riscos ocupacionais, conforme avaliação de riscos do Programa de Gerenciamento de Risco -PGR da organização. Esta Norma se aplica às organizações e aos órgãos públicos da administração direta e indireta, bem como aos órgãos dos poderes legislativo e judiciário e ao Ministério Público, que possuam empregados regidos pela Consolidação das Leis do Trabalho - CLT.""";
                else
                    newRow["Titulo_Principal"] = @"""Esta Norma Regulamentadora - NR estabelece a obrigatoriedade de elaboração e implementação, por parte de todos os empregadores e instituições que admitam trabalhadores como empregados, do Programa de Controle Médico de Saúde Ocupacional -PCMSO, com o objetivo de promoção e preservação da saúde do conjunto dos seus trabalhadores.Esta NR estabelece os parâmetros mínimos e diretrizes gerais a serem observados na execução do PCMSO, podendo os mesmos ser ampliados mediante negociação coletiva de trabalho."" (itens 7.1.1 e 7.1.2 da NR 7)";

                newRow["DadosEmpresa"] = sDadosEmpresa;
                newRow["FotoFachada"] = sFotoFachada;
                newRow["Logotipo"] = xArq;
                newRow["Vigencia"] = sVigencia;
                newRow["NumFuncTotal"] = sNumFuncTotal;
                newRow["NumFuncTotalMin"] = sNumFuncTotalMin;
                newRow["NumFuncTotalMax"] = sNumFuncTotalMax;
                newRow["NumFuncSexoMasc"] = sNumFuncSexoMasc;
                newRow["NumFuncSexoFem"] = sNumFuncSexoFem;
                newRow["NumFuncMenores18"] = sNumFuncMenores18;
                newRow["NumFuncMaiores45"] = sNumFuncMaiores45;
                newRow["NumFunc18a45"] = sNumFunc18a45;
                newRow["NumFuncBeneficiario"] = sNumFuncBeneficiario;
                newRow["AfastadosTrabalho"] = sAfastadosTrabalho;
                newRow["TurnosTrabalho"] = pcmso.IdCliente.Turnos;
                newRow["NomeMedico"] = pcmso.IdCoordenador.NomeCompleto;
                newRow["EnderecoMedico"] = pcmso.IdCoordenador.GetEnderecoTelefoneEmail();
                newRow["FotoCRMMedico"] = pcmso.IdCoordenador.FotoRG;
                newRow["CRM"] = pcmso.IdCoordenador.Numero;
                newRow["Titulo"] = pcmsoDocumento.NumOrdem.ToString() + " - " + pcmsoDocumento.Titulo;
                newRow["Texto"] = pcmsoDocumento.Texto + "\n\r\t";
                newRow["RetirarAcoesSaude"] = pcmso.IsRetirarAcoesSaude;
                newRow["ComDadosCoordenador"] = pcmso.IsDadosCoordenador;


                if (Ilitera.Data.SQLServer.EntitySQLServer._LocalServer.ToString().ToUpper().IndexOf("EY") > 0 || Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper().IndexOf("VIA") > 0)
                {
                    this.pcmso.IdCliente.Find();
                    newRow["CondicoesAmostragem"] = this.pcmso.IdCliente.Observacao;
                }
                else
                    newRow["CondicoesAmostragem"] = "";  // pcmso.GetCondicoesAmostragem();



                newRow["SemAssinatura"] = pcmso.IsSemAssinatura;


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
                    newRow["Base"] = "PRAJNA";
                else if (Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper().IndexOf("GLOBAL") > 0)
                {
                    newRow["Base"] = "GLOBAL";
                }
                else
                    newRow["Base"] = "OUTRAS";

                ds.Tables[0].Rows.Add(newRow);
            }


            //se for adicionar médicos examinadores
            if (this.zMedicosCoordenadores == "S" || this.zMedicosCoordenadores == "T")
            {
                newRow = ds.Tables[0].NewRow();
                //xExisteReg = true;

                DataSet rDs = new DataSet();
                string zCabecalho = "{\\rtf1\\ansi\\ansicpg1252\\deff0\\deflang1046{\\fonttbl{\\f0\\fnil Courier New;}{\\f1\\fnil\\fcharset0 Courier New;}{\\f2\\fnil\\fcharset0 Tahoma;}}\r\n\\viewkind4\\uc1\\pard\\f0\\fs14";
                string zRodape = "\\f2\\fs17\\par\r\n}";
                string zQuebraLinha = "\\par\r\n";
                string zMedicos = "";
                string zMedicos2 = "";
                string zMedicos3 = "";
                
                string xMedico = "";
                string xCRM = "";
                string xUF = "";
                string xAudiometria = "";

                newRow["Titulo"] = (list.Count + 1).ToString() + " - MÉDICOS EXAMINADORES";


                Ilitera.Data.PPRA_EPI rMedicos = new Ilitera.Data.PPRA_EPI();

                if (this.zMedicosCoordenadores == "S")
                {
                    rDs = rMedicos.Trazer_Medicos_Coordenadores(pcmso.IdCliente.Id, false);
                }
                else
                {
                    rDs = rMedicos.Trazer_Medicos_Coordenadores(pcmso.IdCliente.Id, true);
                }

                for (int zCont = 0; zCont < rDs.Tables[0].Rows.Count; zCont++)
                {
                    xMedico = rDs.Tables[0].Rows[zCont][0].ToString().Trim();
                    xCRM = rDs.Tables[0].Rows[zCont][1].ToString().Trim();
                    xUF = rDs.Tables[0].Rows[zCont][2].ToString().Trim();
                    xAudiometria = rDs.Tables[0].Rows[zCont][3].ToString().Trim();

                    if (xMedico.Length > 34) xMedico = xMedico.Substring(0, 35);
                    else
                    {
                        for (int zAux = xMedico.Length; zAux <= 34; zAux++)
                        {
                            xMedico = xMedico + " ";
                        }
                    }


                    if (xCRM.Length > 9) xCRM = xCRM.Substring(0, 10);
                    else
                    {
                        for (int zAux = xCRM.Length; zAux <= 9; zAux++)
                        {
                            xCRM = " " + xCRM;
                        }
                    }


                    if (xUF.Length > 2) xUF = xUF.Substring(0, 2);
                    else
                    {
                        for (int zAux = xUF.Length; zAux <= 2; zAux++)
                        {
                            xUF = xUF + " ";
                        }
                    }


                    string xEntidade = " CRM ";

                    if (xAudiometria == "1")
                    {
                        xEntidade = " CRF ";
                    }



                    if (zCont < 560)
                    {
                        zMedicos = zMedicos + xMedico + xEntidade + xCRM + " " + xUF + " ";
                        if ((zCont + 1) % 2 == 0) zMedicos = zMedicos + zQuebraLinha;
                    }
                    else if (zCont < 1120)
                    {
                        zMedicos2 = zMedicos2 + xMedico + xEntidade + xCRM + " " + xUF + " ";
                        if ((zCont + 1) % 2 == 0) zMedicos2 = zMedicos2 + zQuebraLinha;
                    }
                    else
                    {
                        zMedicos3 = zMedicos3 + xMedico + xEntidade + xCRM + " " + xUF + " ";
                        if ((zCont + 1) % 2 == 0) zMedicos3 = zMedicos3 + zQuebraLinha;
                    }




                }



                if (zMedicos.Trim() != "")
                {
                    //newRow["Texto"] = zCabecalho + "EU, DR. PAULO PRATSCHER, MÉDICO COORDENADOR DESTE PCMSO, AUTORIZO OS MÉDICOS CONFORME ABAIXO A REALIZAR OS EXAMES MÉDICOS OCUPACIONAIS DESTA EMPRESA." + zQuebraLinha + zQuebraLinha + zMedicos + zRodape + "\n\r\t";
                    if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().IndexOf("Prajna") >= 0)
                    {
                        if (pcmso.IdCoordenador.NomeCompleto.ToUpper().IndexOf("DR.") >= 0)
                            newRow["Texto"] = zCabecalho + "Eu, " + pcmso.IdCoordenador.NomeCompleto.Trim() + ",  Médico Responsável pelo PCMSO, autorizo os médicos abaixo relacionados, bem como todos os médicos autorizados pelos responsáveis técnicos das clínicas que realizam exames ocupacionais dessa empresa, a realizarem todos os exames ocupacionais previstos nesse PCMSO." + zQuebraLinha + zQuebraLinha + zMedicos + zRodape + "\n\r\t";
                        else
                            newRow["Texto"] = zCabecalho + "Eu, Dr(a). " + pcmso.IdCoordenador.NomeCompleto.Trim() + ",  Médico Responsável pelo PCMSO, autorizo os médicos abaixo relacionados, bem como todos os médicos autorizados pelos responsáveis técnicos das clínicas que realizam exames ocupacionais dessa empresa, a realizarem todos os exames ocupacionais previstos nesse PCMSO." + zQuebraLinha + zQuebraLinha + zMedicos + zRodape + "\n\r\t";
                    }
                    else
                    {
                        if (pcmso.IdCoordenador.NomeCompleto.ToUpper().IndexOf("DR.") >= 0)
                            newRow["Texto"] = zCabecalho + "EU, " + pcmso.IdCoordenador.NomeCompleto.ToUpper().Trim() + ", MÉDICO RESPONSÁVEL PELO PCMSO, AUTORIZO OS MÉDICOS CONFORME ABAIXO A REALIZAR OS EXAMES MÉDICOS OCUPACIONAIS DESTA EMPRESA." + zQuebraLinha + zQuebraLinha + zMedicos + zRodape + "\n\r\t";
                        else
                            newRow["Texto"] = zCabecalho + "EU, DR(A). " + pcmso.IdCoordenador.NomeCompleto.ToUpper().Trim() + ", MÉDICO RESPONSÁVEL PELO PCMSO, AUTORIZO OS MÉDICOS CONFORME ABAIXO A REALIZAR OS EXAMES MÉDICOS OCUPACIONAIS DESTA EMPRESA." + zQuebraLinha + zQuebraLinha + zMedicos + zRodape + "\n\r\t";
                    }

                    if (zMedicos2 != "")
                        newRow["Texto2"] = zCabecalho + zMedicos2 + zRodape;
                    else
                        newRow["Texto2"] = "";

                    if (zMedicos3 != "")
                        newRow["Texto3"] = zCabecalho + zMedicos3 + zRodape;
                    else
                        newRow["Texto3"] = "";


                    if (pcmso.DataPcmso.Year >= 2022)
                        newRow["Titulo_Principal"] = @"""Esta Norma Regulamentadora - NR estabelece diretrizes e requisitos para o desenvolvimento do Programa de Controle Médico de Saúde Ocupacional -PCMSO nas organizações, com o objetivo de proteger e preservar a saúde de seus empregados em relação aos riscos ocupacionais, conforme avaliação de riscos do Programa de Gerenciamento de Risco -PGR da organização. Esta Norma se aplica às organizações e aos órgãos públicos da administração direta e indireta, bem como aos órgãos dos poderes legislativo e judiciário e ao Ministério Público, que possuam empregados regidos pela Consolidação das Leis do Trabalho - CLT.""";
                    else
                        newRow["Titulo_Principal"] = @"""Esta Norma Regulamentadora - NR estabelece a obrigatoriedade de elaboração e implementação, por parte de todos os empregadores e instituições que admitam trabalhadores como empregados, do Programa de Controle Médico de Saúde Ocupacional -PCMSO, com o objetivo de promoção e preservação da saúde do conjunto dos seus trabalhadores.Esta NR estabelece os parâmetros mínimos e diretrizes gerais a serem observados na execução do PCMSO, podendo os mesmos ser ampliados mediante negociação coletiva de trabalho."" (itens 7.1.1 e 7.1.2 da NR 7)";


                    newRow["DadosEmpresa"] = sDadosEmpresa;
                    newRow["FotoFachada"] = sFotoFachada;
                    newRow["Logotipo"] = xArq;


                    ds.Tables[0].Rows.Add(newRow);
                }

            }
                        
            return ds;
        }		
	}
}
