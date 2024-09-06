using System;
using Ilitera.Opsa.Data;
using System.Data;
using Ilitera.Common;
using System.Collections;
using System.Text;

namespace Ilitera.SPDA.Report
{
	public class DataSourceLaudoSPDA2 : DataSourceBase
	{
		private Cliente cliente;
        private LaudoTecnico laudoTecnico;
        private LaudoSPDA LaudoSPDA;
		
		public DataSourceLaudoSPDA2(Cliente cliente, Int32 zLaudoSPDA)
		{
			this.cliente = cliente;

            this.LaudoSPDA = new LaudoSPDA();
            this.LaudoSPDA.Find(zLaudoSPDA);
           
            if (this.cliente.mirrorOld == null)
			    this.cliente.Find();
            
            this.laudoTecnico = LaudoTecnico.GetUltimoLaudo(this.cliente.Id);
		}



		
		public RptSPDA2 GetReport()
		{
			RptSPDA2 report = new RptSPDA2();
			report.SetDataSource(GetIntroducaoLaudo());
			//report.OpenSubreport("FotoFachada").SetDataSource(DataSourceFotoFachada());
			//report.OpenSubreport("Engenheiro").SetDataSource(GetEngenheiro());
            //report.OpenSubreport("Engenheiro2").SetDataSource(GetEngenheiro());
            //report.OpenSubreport("Engenheiro3").SetDataSource(GetEngenheiro());
            //report.OpenSubreport("Eletricista").SetDataSource(GetEletricista());
            //report.OpenSubreport("Eletricista2").SetDataSource(GetEletricista());
            //report.OpenSubreport("Responsavel_Empresa").SetDataSource(GetResponsavel_Empresa());
            report.OpenSubreport("rptLaudoEletrico2_Obs").SetDataSource(GetObs_New());
            //report.OpenSubreport("RptSPDA2_Obs_MT").SetDataSource(GetObs_New_MT());
            //report.OpenSubreport("RptSPDA2_Obs_AT").SetDataSource(GetObs_New_AT());
            //report.Subreports[8].SetDataSource(GetCronograma());
            //report.OpenSubreport("rptCronogramaLaudoSPDA").SetDataSource(GetCronograma());
			//report.OpenSubreport("Prestador").SetDataSource(GetPrestador());
			report.Refresh();

            SetTempoProcessamento(report);

			return report;
		}


        public RptSPDA2_Intro GetReport_Intro()
        {
            RptSPDA2_Intro report = new RptSPDA2_Intro();
            report.SetDataSource(GetIntroducaoLaudo());
            report.OpenSubreport("FotoFachada").SetDataSource(DataSourceFotoFachada());
            report.OpenSubreport("Engenheiro").SetDataSource(GetEngenheiro());
            //report.OpenSubreport("Engenheiro2").SetDataSource(GetEngenheiro());
            //report.OpenSubreport("Engenheiro3").SetDataSource(GetEngenheiro());
            //report.OpenSubreport("Eletricista").SetDataSource(GetEletricista());
            //report.OpenSubreport("Eletricista2").SetDataSource(GetEletricista());
            //report.OpenSubreport("Responsavel_Empresa").SetDataSource(GetResponsavel_Empresa());
            //report.Subreports[8].SetDataSource(GetCronograma());
            //report.OpenSubreport("rptCronogramaLaudoSPDA").SetDataSource(GetCronograma());
            //report.OpenSubreport("Prestador").SetDataSource(GetPrestador());
            report.Refresh();

            SetTempoProcessamento(report);

            return report;
        }


        public RptSPDA2_Final GetReport_Final()
        {
            RptSPDA2_Final report = new RptSPDA2_Final();
            report.SetDataSource(GetIntroducaoPPRA());
            report.OpenSubreport("rtpPontos").SetDataSource(GetPontos());

            report.Refresh();

            SetTempoProcessamento(report);

            return report;
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
            xReader = zAdeq.Gerar_Dados_Pontos( LaudoSPDA.Id);

            float zTot = 0;
            float zTot_Ordem = 0;

            while (xReader.Read())
            {

                zTot_Ordem++;

                zTot = zTot + System.Convert.ToSingle(xReader["Medida"]);

            }

            zTot = zTot / zTot_Ordem;


            float zMedida = 0;
            DataRow newRow;
            xReader = zAdeq.Gerar_Dados_Pontos(LaudoSPDA.Id);

            while (xReader.Read())
            {

                newRow = ds.Tables[0].NewRow();

                zOrdem++;

                zMedida = System.Convert.ToSingle(xReader["Medida"]);

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

            if (LaudoSPDA.PRa == "C") newRow["PRa"] = "•";
            else if (LaudoSPDA.PRa == "I") newRow["PRa"] = " •";
            else if (LaudoSPDA.PRa == "P") newRow["PRa"] = "  •";

            if (LaudoSPDA.PRb == "C") newRow["PRb"] = "•";
            else if (LaudoSPDA.PRb == "I") newRow["PRb"] = " •";
            else if (LaudoSPDA.PRb == "P") newRow["PRb"] = "  •";


            newRow["Introducao"] = LaudoSPDA.Introducao;

            newRow["Conclusao"] = LaudoSPDA.Conclusao;


            newRow["Logotipo"] = xArq;
            newRow["DadosEmpresa"] = cliente.GetDadosEmpresaHtml(laudoTecnico.hDT_LAUDO);
            newRow["CarimboCNPJ"] = cliente.GetCarimboCnpjHtml(laudoTecnico.hDT_LAUDO);
            //newRow["Data"] =  laudoTecnico.hDT_LAUDO.ToString("dd-MM-yyyy");  //+ " a  " + laudoTecnico.hDT_LAUDO.AddDays(364).ToString("dd-MM-yyyy");
            newRow["Data"] = LaudoSPDA.Data_Laudo.ToString("dd-MM-yyyy");
            newRow["IsAntecipacaoBSH"] = false;
            newRow["IsComissao"] = !(laudoTecnico.nID_COMISSAO_1.Id == 0 &&
                                            laudoTecnico.nID_COMISSAO_2.Id != 0 &&
                                            laudoTecnico.nID_COMISSAO_3.Id == 0);
            newRow["RepresentanteLegal"] = laudoTecnico.nID_EMPR.GetEndereco().GetCidadeEstado().ToString(); //laudoTecnico.ResponsavelLegal.NomeCompleto;
            newRow["RepresentanteLegalTitulo"] = laudoTecnico.ResponsavelLegal.Titulo;

            newRow["Recomendacoes"] = LaudoSPDA.Recomendacoes;
            newRow["Observacoes"] = LaudoSPDA.Observacoes;




            ds.Tables[0].Rows.Add(newRow);

            return ds;
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


		private DataSet GetIntroducaoLaudo()
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
            table.Columns.Add("Indice", Type.GetType("System.String"));
            table.Columns.Add("Objetivos", Type.GetType("System.String"));
            table.Columns.Add("Generalidades", Type.GetType("System.String"));
            table.Columns.Add("Observacoes", Type.GetType("System.String"));
            table.Columns.Add("Normas", Type.GetType("System.String"));
            table.Columns.Add("Texto_Inicial", Type.GetType("System.String"));
            table.Columns.Add("Inspecao_BT", Type.GetType("System.String"));
            table.Columns.Add("Inspecao_MT", Type.GetType("System.String"));
            table.Columns.Add("Inspecao_AT", Type.GetType("System.String"));
            table.Columns.Add("BT_Foto1", Type.GetType("System.Byte[]"));
            table.Columns.Add("BT_Foto2", Type.GetType("System.Byte[]"));
            table.Columns.Add("MT_Foto1", Type.GetType("System.Byte[]"));
            table.Columns.Add("MT_Foto2", Type.GetType("System.Byte[]"));
            table.Columns.Add("AT_Foto1", Type.GetType("System.Byte[]"));
            table.Columns.Add("AT_Foto2", Type.GetType("System.Byte[]"));
            table.Columns.Add("BT_Foto1_Nome", Type.GetType("System.String"));
            table.Columns.Add("BT_Foto2_Nome", Type.GetType("System.String"));
            table.Columns.Add("MT_Foto1_Nome", Type.GetType("System.String"));
            table.Columns.Add("MT_Foto2_Nome", Type.GetType("System.String"));
            table.Columns.Add("AT_Foto1_Nome", Type.GetType("System.String"));
            table.Columns.Add("AT_Foto2_Nome", Type.GetType("System.String"));

            table.Columns.Add("Anexo_1023_Foto", Type.GetType("System.Byte[]"));
            table.Columns.Add("Anexo_1024_Foto", Type.GetType("System.Byte[]"));
            table.Columns.Add("Anexo_1025_Foto", Type.GetType("System.Byte[]"));
            table.Columns.Add("Anexo_1088_Foto", Type.GetType("System.Byte[]"));
            table.Columns.Add("Anexo_PR_Foto", Type.GetType("System.Byte[]"));
            
            table.Columns.Add("Anexo_1023", Type.GetType("System.String"));
            table.Columns.Add("Anexo_1024", Type.GetType("System.String"));
            table.Columns.Add("Anexo_1025", Type.GetType("System.String"));
            table.Columns.Add("Anexo_1088", Type.GetType("System.String"));
            table.Columns.Add("Anexo_PR", Type.GetType("System.String"));

            table.Columns.Add("Anexo_Generalidade1", Type.GetType("System.Byte[]"));
            table.Columns.Add("Anexo_Generalidade2", Type.GetType("System.Byte[]"));

            table.Columns.Add("Descr_Anexo_Generalidade1", Type.GetType("System.String"));
            table.Columns.Add("Descr_Anexo_Generalidade2", Type.GetType("System.String"));



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

            
            //if ( this.LaudoSPDA.NR1023=="C" )       newRow["NR1023"] = "•";
            //else if (this.LaudoSPDA.NR1023 == "I") newRow["NR1023"] = " •";
            //else if (this.LaudoSPDA.NR1023 == "P") newRow["NR1023"] = "  •";


            //if (this.LaudoSPDA.NR1024a == "C") newRow["NR1024a"] = "•";
            //else if (this.LaudoSPDA.NR1024a == "I") newRow["NR1024a"] = " •";
            //else if (this.LaudoSPDA.NR1024a == "P") newRow["NR1024a"] = "  •";

            //if (this.LaudoSPDA.NR1024b == "C") newRow["NR1024b"] = "•";
            //else if (this.LaudoSPDA.NR1024b == "I") newRow["NR1024b"] = " •";
            //else if (this.LaudoSPDA.NR1024b == "P") newRow["NR1024b"] = "  •";

            //if (this.LaudoSPDA.NR1024c == "C") newRow["NR1024c"] = "•";
            //else if (this.LaudoSPDA.NR1024c == "I") newRow["NR1024c"] = " •";
            //else if (this.LaudoSPDA.NR1024c == "P") newRow["NR1024c"] = "  •";

            //if (this.LaudoSPDA.NR1024d == "C") newRow["NR1024d"] = "•";
            //else if (this.LaudoSPDA.NR1024d == "I") newRow["NR1024d"] = " •";
            //else if (this.LaudoSPDA.NR1024d == "P") newRow["NR1024d"] = "  •";

            //if (this.LaudoSPDA.NR1024e == "C") newRow["NR1024e"] = "•";
            //else if (this.LaudoSPDA.NR1024e == "I") newRow["NR1024e"] = " •";
            //else if (this.LaudoSPDA.NR1024e == "P") newRow["NR1024e"] = "  •";

            //if (this.LaudoSPDA.NR1024f == "C") newRow["NR1024f"] = "•";
            //else if (this.LaudoSPDA.NR1024f == "I") newRow["NR1024f"] = " •";
            //else if (this.LaudoSPDA.NR1024f == "P") newRow["NR1024f"] = "  •";

            //if (this.LaudoSPDA.NR1024g == "C") newRow["NR1024g"] = "•";
            //else if (this.LaudoSPDA.NR1024g == "I") newRow["NR1024g"] = " •";
            //else if (this.LaudoSPDA.NR1024g == "P") newRow["NR1024g"] = "  •";


            //if (this.LaudoSPDA.NR1025a == "C") newRow["NR1025a"] = "•";
            //else if (this.LaudoSPDA.NR1025a == "I") newRow["NR1025a"] = " •";
            //else if (this.LaudoSPDA.NR1025a == "P") newRow["NR1025a"] = "  •";

            //if (this.LaudoSPDA.NR1025b == "C") newRow["NR1025b"] = "•";
            //else if (this.LaudoSPDA.NR1025b == "I") newRow["NR1025b"] = " •";
            //else if (this.LaudoSPDA.NR1025b == "P") newRow["NR1025b"] = "  •";

            //if (this.LaudoSPDA.NR1088a == "C") newRow["NR1088a"] = "•";
            //else if (this.LaudoSPDA.NR1088a == "I") newRow["NR1088a"] = " •";
            //else if (this.LaudoSPDA.NR1088a == "P") newRow["NR1088a"] = "  •";

            //if (this.LaudoSPDA.NR1088b == "C") newRow["NR1088b"] = "•";
            //else if (this.LaudoSPDA.NR1088b == "I") newRow["NR1088b"] = " •";
            //else if (this.LaudoSPDA.NR1088b == "P") newRow["NR1088b"] = "  •";

            //if (this.LaudoSPDA.NR1088c == "C") newRow["NR1088c"] = "•";
            //else if (this.LaudoSPDA.NR1088c == "I") newRow["NR1088c"] = " •";
            //else if (this.LaudoSPDA.NR1088c == "P") newRow["NR1088c"] = "  •";

            //if (this.LaudoSPDA.PRa == "C") newRow["PRa"] = "•";
            //else if (this.LaudoSPDA.PRa == "I") newRow["PRa"] = " •";
            //else if (this.LaudoSPDA.PRa == "P") newRow["PRa"] = "  •";

            //if (this.LaudoSPDA.PRb == "C") newRow["PRb"] = "•";
            //else if (this.LaudoSPDA.PRb == "I") newRow["PRb"] = " •";
            //else if (this.LaudoSPDA.PRb == "P") newRow["PRb"] = "  •";

            //if (this.LaudoSPDA.PRc == "C") newRow["PRc"] = "•";
            //else if (this.LaudoSPDA.PRc == "I") newRow["PRc"] = " •";
            //else if (this.LaudoSPDA.PRc == "P") newRow["PRc"] = "  •";

            newRow["Introducao"] = this.LaudoSPDA.Introducao;

            newRow["Conclusao"] = this.LaudoSPDA.Conclusao;

            newRow["Objetivos"] = this.LaudoSPDA.Objetivos;
            newRow["Normas"] = this.LaudoSPDA.Normas;

            newRow["Generalidades"] = this.LaudoSPDA.Caracteristicas_gerais;

            if (this.LaudoSPDA.Generalidades == null) newRow["Observacoes"] = "";
            else newRow["Observacoes"] = this.LaudoSPDA.Generalidades;

            newRow["Indice"] = this.LaudoSPDA.Indice;

            newRow["Texto_Inicial"] = "A empresa " + cliente.NomeCompleto +" ,por seu responsável legal, infra-mencionado, objetivando cumprir e fazer cumprir as normas de segurança do trabalho e em particular a determinação legal exarada através da Portaria MTE № 598/2004 - NR-10 que estabelece a obrigatoriedade da elaboração e implementação do Sistema de Gestão de Segurança e Saúde do Trabalho - SGSST - para os trabalhadores que atuam com energia elétrica, resolve nomear equipe de pessoas, consubstanciada na elaboração do programa que a critério deste empregador são capazes de desenvolver o disposto na referida Norma Regulamentadora - NR-10 - Segurança em Instalações e Serviços em Eletricidade, bem como efetuar sempre que necessário e pelo menos uma vez ao ano, uma análise global para avaliação do seu desenvolvimento, reavaliação dos ajustes necessários e estabelecimento de novas metas e prioridades com base no ciclo do PDCA (Planejamento, Desenvolvimento, Controle e Auditoria).";


            newRow["Logotipo"] = xArq;
            newRow["DadosEmpresa"] = cliente.GetDadosEmpresaHtml(laudoTecnico.hDT_LAUDO);
            newRow["CarimboCNPJ"] = cliente.GetCarimboCnpjHtml(laudoTecnico.hDT_LAUDO);
            //newRow["Data"] =  laudoTecnico.hDT_LAUDO.ToString("dd-MM-yyyy");  //+ " a  " + laudoTecnico.hDT_LAUDO.AddDays(364).ToString("dd-MM-yyyy");
            newRow["Data"] = this.LaudoSPDA.Data_Laudo.ToString("dd-MM-yyyy");
            newRow["IsAntecipacaoBSH"] = false;
            newRow["IsComissao"] = !(laudoTecnico.nID_COMISSAO_1.Id == 0 &&
                                            laudoTecnico.nID_COMISSAO_2.Id != 0 &&
                                            laudoTecnico.nID_COMISSAO_3.Id == 0);
            newRow["RepresentanteLegal"] = laudoTecnico.nID_EMPR.GetEndereco().GetCidadeEstado().ToString(); //laudoTecnico.ResponsavelLegal.NomeCompleto;
            newRow["RepresentanteLegalTitulo"] = laudoTecnico.ResponsavelLegal.Titulo;

            newRow["Inspecao_BT"] = LaudoSPDA.Inspecao_Detalhada;
            //newRow["Inspecao_MT"] = LaudoSPDA.Inspecao_MT;
            //newRow["Inspecao_AT"] = LaudoSPDA.Inspecao_AT;

            newRow["BT_Foto1_Nome"] = LaudoSPDA.Inspecao_Detalhada_Foto1.Trim();
            newRow["BT_Foto2_Nome"] = LaudoSPDA.Inspecao_Detalhada_Foto2.Trim();
            //newRow["MT_Foto1_Nome"] = LaudoSPDA.MT_Foto1.Trim();
            //newRow["MT_Foto2_Nome"] = LaudoSPDA.MT_Foto2.Trim();
            //newRow["AT_Foto1_Nome"] = LaudoSPDA.AT_Foto1.Trim();
            //newRow["AT_Foto2_Nome"] = LaudoSPDA.AT_Foto2.Trim();


            if (LaudoSPDA.Inspecao_Detalhada_Foto1.ToString().Trim() != "")
            {
                newRow["BT_Foto1"] = Ilitera.Common.Fotos.GetByteFoto_Uri("I:\\fotosdocsdigitais\\" + cliente.DiretorioPadrao + "\\vasopressao\\" + LaudoSPDA.Inspecao_Detalhada_Foto1.ToString().Trim());
            }
            if (LaudoSPDA.Inspecao_Detalhada_Foto2.ToString().Trim() != "")
            {
                newRow["BT_Foto2"] = Ilitera.Common.Fotos.GetByteFoto_Uri("I:\\fotosdocsdigitais\\" + cliente.DiretorioPadrao + "\\vasopressao\\" + LaudoSPDA.Inspecao_Detalhada_Foto2.ToString().Trim());
            }

            //if (LaudoSPDA.MT_Foto1.ToString().Trim() != "")
            //{
            //    newRow["MT_Foto1"] = Ilitera.Common.Fotos.GetByteFoto_Uri("I:\\fotosdocsdigitais\\" + cliente.DiretorioPadrao + "\\vasopressao\\" + LaudoSPDA.MT_Foto1.ToString().Trim());
            //}
            //if (LaudoSPDA.MT_Foto2.ToString().Trim() != "")
            //{
            //    newRow["MT_Foto2"] = Ilitera.Common.Fotos.GetByteFoto_Uri("I:\\fotosdocsdigitais\\" + cliente.DiretorioPadrao + "\\vasopressao\\" + LaudoSPDA.MT_Foto2.ToString().Trim());
            //}
            //if (LaudoSPDA.AT_Foto1.ToString().Trim() != "")
            //{
            //    newRow["AT_Foto1"] = Ilitera.Common.Fotos.GetByteFoto_Uri("I:\\fotosdocsdigitais\\" + cliente.DiretorioPadrao + "\\vasopressao\\" + LaudoSPDA.AT_Foto1.ToString().Trim());
            //}
            //if (LaudoSPDA.AT_Foto2.ToString().Trim() != "")
            //{
            //    newRow["AT_Foto2"] = Ilitera.Common.Fotos.GetByteFoto_Uri("I:\\fotosdocsdigitais\\" + cliente.DiretorioPadrao + "\\vasopressao\\" + LaudoSPDA.AT_Foto2.ToString().Trim());
            //}



            //newRow["Anexo_1023"] = LaudoSPDA.Anexo_1023.Trim();
            //newRow["Anexo_1024"] = LaudoSPDA.Anexo_1024.Trim();
            //newRow["Anexo_1025"] = LaudoSPDA.Anexo_1025.Trim();
            //newRow["Anexo_1088"] = LaudoSPDA.Anexo_1088.Trim();
            //newRow["Anexo_PR"] = LaudoSPDA.Anexo_PR.Trim();

            //if (LaudoSPDA.Anexo_1023.ToString().Trim() != "")
            //{
            //    newRow["Anexo_1023_Foto"] = Ilitera.Common.Fotos.GetByteFoto_Uri("I:\\fotosdocsdigitais\\" + cliente.DiretorioPadrao + "\\vasopressao\\" + LaudoSPDA.Anexo_1023.ToString().Trim());
            //}

            //if (LaudoSPDA.Anexo_1024.ToString().Trim() != "")
            //{
            //    newRow["Anexo_1024_Foto"] = Ilitera.Common.Fotos.GetByteFoto_Uri("I:\\fotosdocsdigitais\\" + cliente.DiretorioPadrao + "\\vasopressao\\" + LaudoSPDA.Anexo_1024.ToString().Trim());
            //}

            //if (LaudoSPDA.Anexo_1025.ToString().Trim() != "")
            //{
            //    newRow["Anexo_1025_Foto"] = Ilitera.Common.Fotos.GetByteFoto_Uri("I:\\fotosdocsdigitais\\" + cliente.DiretorioPadrao + "\\vasopressao\\" + LaudoSPDA.Anexo_1025.ToString().Trim());
            //}

            //if (LaudoSPDA.Anexo_1088.ToString().Trim() != "")
            //{
            //    newRow["Anexo_1088_Foto"] = Ilitera.Common.Fotos.GetByteFoto_Uri("I:\\fotosdocsdigitais\\" + cliente.DiretorioPadrao + "\\vasopressao\\" + LaudoSPDA.Anexo_1088.ToString().Trim());
            //}

            //if (LaudoSPDA.Anexo_PR.ToString().Trim() != "")
            //{
            //    newRow["Anexo_PR_Foto"] = Ilitera.Common.Fotos.GetByteFoto_Uri("I:\\fotosdocsdigitais\\" + cliente.DiretorioPadrao + "\\vasopressao\\" + LaudoSPDA.Anexo_PR.ToString().Trim());
            //}



            if (LaudoSPDA.Descr_Anexo_Caracteristica1 != null )      newRow["Descr_Anexo_Generalidade1"] = LaudoSPDA.Descr_Anexo_Caracteristica1.Trim();

            if (LaudoSPDA.Descr_Anexo_Caracteristica2 != null )      newRow["Descr_Anexo_Generalidade2"] = LaudoSPDA.Descr_Anexo_Caracteristica2.Trim();


            if (LaudoSPDA.Anexo_Caracteristica1 != null)
            {
                if (LaudoSPDA.Anexo_Caracteristica1.ToString().Trim() != "")
                {
                    newRow["Anexo_Generalidade1"] = Ilitera.Common.Fotos.GetByteFoto_Uri("I:\\fotosdocsdigitais\\" + cliente.DiretorioPadrao + "\\vasopressao\\" + LaudoSPDA.Anexo_Caracteristica1.ToString().Trim());
                }
            }

            if (LaudoSPDA.Anexo_Caracteristica2 != null)
            {
                if (LaudoSPDA.Anexo_Caracteristica2.ToString().Trim() != "")
                {
                    newRow["Anexo_Generalidade2"] = Ilitera.Common.Fotos.GetByteFoto_Uri("I:\\fotosdocsdigitais\\" + cliente.DiretorioPadrao + "\\vasopressao\\" + LaudoSPDA.Anexo_Caracteristica2.ToString().Trim());
                }
            }

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



            newRow["Conclusao"] = this.LaudoSPDA.Conclusao;

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
                xPrestador.Find(this.LaudoSPDA.Id_Responsavel.Id);

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
                xPrestador.Find(this.LaudoSPDA.Id_Eletricista);
             

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
                //xPrestador.Find(this.LaudoSPDA.Id_Eletricista);


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
                //    newRow["iAssinatura"] = Ilitera.Common.Fotos.GetByteFoto_Uri(xPrestador.FotoAss);
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
                xPrestador.Find(this.LaudoSPDA.Id_Responsavel_Empresa);


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


            zDs = zAdeq.Trazer_Adequacao(this.LaudoSPDA.Id);

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



        private DataSet GetObs_New()
        {
            DataSet ds = new DataSet();
            DataTable table = new DataTable("Result");
            table.Columns.Add("Titulo", Type.GetType("System.String"));
            table.Columns.Add("Descricao", Type.GetType("System.String"));
            //            table.Columns.Add("Foto", Type.GetType("System.Byte[]"));
            table.Columns.Add("Foto_Adeq", Type.GetType("System.Byte[]"));
            table.Columns.Add("Foto_Adeq2", Type.GetType("System.Byte[]"));
            table.Columns.Add("Foto_Adeq3", Type.GetType("System.Byte[]"));
            table.Columns.Add("Foto_Termografia", Type.GetType("System.Byte[]"));
            table.Columns.Add("Ordem", Type.GetType("System.String"));
            table.Columns.Add("OrdemItem", Type.GetType("System.String"));
            table.Columns.Add("Irregularidade", Type.GetType("System.String"));
            table.Columns.Add("Recomendacoes", Type.GetType("System.String"));
            table.Columns.Add("Situacao", Type.GetType("System.String"));
            ds.Tables.Add(table);



            Ilitera.Data.Laudo_Eletrico zAdeq = new Ilitera.Data.Laudo_Eletrico();

            DataSet zDs = new DataSet();
            //string zDescricao = "";
            string zOrdem = "";


            zDs = zAdeq.Trazer_Adequacao_New_SPDA(this.LaudoSPDA.Id);

            DataRow newRow;
            try
            {


                for (int zCont = 0; zCont < zDs.Tables[0].Rows.Count; zCont++)
                {


                    newRow = ds.Tables[0].NewRow();

                    newRow["Titulo"] = zDs.Tables[0].Rows[zCont]["Titulo"].ToString();
                    newRow["Irregularidade"] = zDs.Tables[0].Rows[zCont]["Irregularidade"].ToString();
                    newRow["Recomendacoes"] = zDs.Tables[0].Rows[zCont]["Recomendacoes"].ToString();
                    newRow["Situacao"] = zDs.Tables[0].Rows[zCont]["Situacao"].ToString();


                    if (zOrdem != zDs.Tables[0].Rows[zCont]["Ordem"].ToString())
                    {

                        zOrdem = zDs.Tables[0].Rows[zCont]["Ordem"].ToString();


                        if (zDs.Tables[0].Rows[zCont]["Foto"].ToString().Trim() != "")
                        {
                            newRow["Foto_Adeq"] = Ilitera.Common.Fotos.GetByteFoto_Uri("I:\\fotosdocsdigitais\\" + cliente.DiretorioPadrao + "\\vasopressao\\" + zDs.Tables[0].Rows[zCont]["Foto"].ToString());
                        }

                        if (zDs.Tables[0].Rows[zCont]["Foto2"].ToString().Trim() != "")
                        {
                            newRow["Foto_Adeq2"] = Ilitera.Common.Fotos.GetByteFoto_Uri("I:\\fotosdocsdigitais\\" + cliente.DiretorioPadrao + "\\vasopressao\\" + zDs.Tables[0].Rows[zCont]["Foto2"].ToString());
                        }

                        if (zDs.Tables[0].Rows[zCont]["Foto3"].ToString().Trim() != "")
                        {
                            newRow["Foto_Adeq3"] = Ilitera.Common.Fotos.GetByteFoto_Uri("I:\\fotosdocsdigitais\\" + cliente.DiretorioPadrao + "\\vasopressao\\" + zDs.Tables[0].Rows[zCont]["Foto3"].ToString());
                        }

                        if (zDs.Tables[0].Rows[zCont]["FotoTermografia"].ToString().Trim() != "")
                        {
                            newRow["Foto_Termografia"] = Ilitera.Common.Fotos.GetByteFoto_Uri("I:\\fotosdocsdigitais\\" + cliente.DiretorioPadrao + "\\vasopressao\\" + zDs.Tables[0].Rows[zCont]["FotoTermografia"].ToString());
                        }


                    }

                    newRow["Ordem"] = zDs.Tables[0].Rows[zCont]["Ordem"].ToString(); 

                    ds.Tables[0].Rows.Add(newRow);



                }



            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Trace.WriteLine(ex.Message);
            }

            return ds;
        }




        private DataSet GetObs_New_MT()
        {
            DataSet ds = new DataSet();
            DataTable table = new DataTable("Result");
            table.Columns.Add("Titulo", Type.GetType("System.String"));
            table.Columns.Add("Descricao", Type.GetType("System.String"));
            //            table.Columns.Add("Foto", Type.GetType("System.Byte[]"));
            table.Columns.Add("Foto_Adeq", Type.GetType("System.Byte[]"));
            table.Columns.Add("Foto_Adeq2", Type.GetType("System.Byte[]"));
            table.Columns.Add("Foto_Adeq3", Type.GetType("System.Byte[]"));
            table.Columns.Add("Foto_Termografia", Type.GetType("System.Byte[]"));
            table.Columns.Add("Ordem", Type.GetType("System.String"));
            table.Columns.Add("OrdemItem", Type.GetType("System.String"));
            table.Columns.Add("Irregularidade", Type.GetType("System.String"));
            table.Columns.Add("Recomendacoes", Type.GetType("System.String"));
            table.Columns.Add("Situacao", Type.GetType("System.String"));
            ds.Tables.Add(table);



            Ilitera.Data.Laudo_Eletrico zAdeq = new Ilitera.Data.Laudo_Eletrico();

            DataSet zDs = new DataSet();
            //string zDescricao = "";
            string zOrdem = "";


            zDs = zAdeq.Trazer_Adequacao_New(this.LaudoSPDA.Id, "M");

            DataRow newRow;
            try
            {


                for (int zCont = 0; zCont < zDs.Tables[0].Rows.Count; zCont++)
                {


                    newRow = ds.Tables[0].NewRow();

                    newRow["Titulo"] = zDs.Tables[0].Rows[zCont]["Titulo"].ToString();
                    newRow["Irregularidade"] = zDs.Tables[0].Rows[zCont]["Irregularidade"].ToString();
                    newRow["Recomendacoes"] = zDs.Tables[0].Rows[zCont]["Recomendacoes"].ToString();
                    newRow["Situacao"] = zDs.Tables[0].Rows[zCont]["Situacao"].ToString();


                    if (zOrdem != zDs.Tables[0].Rows[zCont]["Ordem"].ToString())
                    {

                        zOrdem = zDs.Tables[0].Rows[zCont]["Ordem"].ToString();

                        if (zDs.Tables[0].Rows[zCont]["Foto"].ToString().Trim() != "")
                        {
                            newRow["Foto_Adeq"] = Ilitera.Common.Fotos.GetByteFoto_Uri("I:\\fotosdocsdigitais\\" + cliente.DiretorioPadrao + "\\vasopressao\\" + zDs.Tables[0].Rows[zCont]["Foto"].ToString());
                        }

                        if (zDs.Tables[0].Rows[zCont]["Foto2"].ToString().Trim() != "")
                        {
                            newRow["Foto_Adeq2"] = Ilitera.Common.Fotos.GetByteFoto_Uri("I:\\fotosdocsdigitais\\" + cliente.DiretorioPadrao + "\\vasopressao\\" + zDs.Tables[0].Rows[zCont]["Foto2"].ToString());
                        }

                        if (zDs.Tables[0].Rows[zCont]["Foto3"].ToString().Trim() != "")
                        {
                            newRow["Foto_Adeq3"] = Ilitera.Common.Fotos.GetByteFoto_Uri("I:\\fotosdocsdigitais\\" + cliente.DiretorioPadrao + "\\vasopressao\\" + zDs.Tables[0].Rows[zCont]["Foto3"].ToString());
                        }

                        if (zDs.Tables[0].Rows[zCont]["FotoTermografia"].ToString().Trim() != "")
                        {
                            newRow["Foto_Termografia"] = Ilitera.Common.Fotos.GetByteFoto_Uri("I:\\fotosdocsdigitais\\" + cliente.DiretorioPadrao + "\\vasopressao\\" + zDs.Tables[0].Rows[zCont]["FotoTermografia"].ToString());
                        }

                    }


                    newRow["Ordem"] = zDs.Tables[0].Rows[zCont]["Ordem"].ToString(); 

                    ds.Tables[0].Rows.Add(newRow);



                }



            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Trace.WriteLine(ex.Message);
            }

            return ds;
        }



        private DataSet GetObs_New_AT()
        {
            DataSet ds = new DataSet();
            DataTable table = new DataTable("Result");
            table.Columns.Add("Titulo", Type.GetType("System.String"));
            table.Columns.Add("Descricao", Type.GetType("System.String"));
            //            table.Columns.Add("Foto", Type.GetType("System.Byte[]"));
            table.Columns.Add("Foto_Adeq", Type.GetType("System.Byte[]"));
            table.Columns.Add("Foto_Adeq2", Type.GetType("System.Byte[]"));
            table.Columns.Add("Foto_Adeq3", Type.GetType("System.Byte[]"));
            table.Columns.Add("Foto_Termografia", Type.GetType("System.Byte[]"));
            table.Columns.Add("Ordem", Type.GetType("System.String"));
            table.Columns.Add("OrdemItem", Type.GetType("System.String"));
            table.Columns.Add("Irregularidade", Type.GetType("System.String"));
            table.Columns.Add("Recomendacoes", Type.GetType("System.String"));
            table.Columns.Add("Situacao", Type.GetType("System.String"));
            ds.Tables.Add(table);



            Ilitera.Data.Laudo_Eletrico zAdeq = new Ilitera.Data.Laudo_Eletrico();

            DataSet zDs = new DataSet();
            //string zDescricao = "";
            string zOrdem = "";


            zDs = zAdeq.Trazer_Adequacao_New(this.LaudoSPDA.Id, "A");

            DataRow newRow;
            try
            {


                for (int zCont = 0; zCont < zDs.Tables[0].Rows.Count; zCont++)
                {


                    newRow = ds.Tables[0].NewRow();

                    newRow["Titulo"] = zDs.Tables[0].Rows[zCont]["Titulo"].ToString();
                    newRow["Irregularidade"] = zDs.Tables[0].Rows[zCont]["Irregularidade"].ToString();
                    newRow["Recomendacoes"] = zDs.Tables[0].Rows[zCont]["Recomendacoes"].ToString();
                    newRow["Situacao"] = zDs.Tables[0].Rows[zCont]["Situacao"].ToString();


                    if (zOrdem != zDs.Tables[0].Rows[zCont]["Ordem"].ToString())
                    {

                        zOrdem = zDs.Tables[0].Rows[zCont]["Ordem"].ToString();


                        if (zDs.Tables[0].Rows[zCont]["Foto"].ToString().Trim() != "")
                        {
                            newRow["Foto_Adeq"] = Ilitera.Common.Fotos.GetByteFoto_Uri("I:\\fotosdocsdigitais\\" + cliente.DiretorioPadrao + "\\vasopressao\\" + zDs.Tables[0].Rows[zCont]["Foto"].ToString());
                        }

                        if (zDs.Tables[0].Rows[zCont]["Foto2"].ToString().Trim() != "")
                        {
                            newRow["Foto_Adeq2"] = Ilitera.Common.Fotos.GetByteFoto_Uri("I:\\fotosdocsdigitais\\" + cliente.DiretorioPadrao + "\\vasopressao\\" + zDs.Tables[0].Rows[zCont]["Foto2"].ToString());
                        }

                        if (zDs.Tables[0].Rows[zCont]["Foto3"].ToString().Trim() != "")
                        {
                            newRow["Foto_Adeq3"] = Ilitera.Common.Fotos.GetByteFoto_Uri("I:\\fotosdocsdigitais\\" + cliente.DiretorioPadrao + "\\vasopressao\\" + zDs.Tables[0].Rows[zCont]["Foto3"].ToString());
                        }

                        if (zDs.Tables[0].Rows[zCont]["FotoTermografia"].ToString().Trim() != "")
                        {
                            newRow["Foto_Termografia"] = Ilitera.Common.Fotos.GetByteFoto_Uri("I:\\fotosdocsdigitais\\" + cliente.DiretorioPadrao + "\\vasopressao\\" + zDs.Tables[0].Rows[zCont]["FotoTermografia"].ToString());
                        }


                    }

                    newRow["Ordem"] = zDs.Tables[0].Rows[zCont]["Ordem"].ToString(); 

                    ds.Tables[0].Rows.Add(newRow);



                }



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
