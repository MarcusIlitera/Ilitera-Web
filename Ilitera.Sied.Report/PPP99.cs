using System;
using System.Text;
using System.Data;
using System.Collections;
using System.Collections.Generic;


using Ilitera.Common;
using Ilitera.Opsa.Data;


namespace Ilitera.Sied.Report
{
    public class PPP99 : DataSourceBase
    {
        DataSet dsEngenheiros;
        private Cliente cliente;
        private Empregado empregado;
        private List<EmpregadoFuncao> listEmpregadoFuncao;
        private DateTime dataEmissao;
        private DateTime dataInicioPPP = new DateTime(2004, 1, 1);
        private bool ViaNetPdt = false;
        private bool SomentePeriodoAposDataInicioPPP = true;
        private bool PeriodoDesvinculadoProfissiografia = true;
        //private bool IsGhePericulosicade = false;
        private Requisitos requisitos;
        private string Observacao;
        private string Preposto;
        private string xCA_Acidente = "";
        private string Assinatura = "N";
        private string Riscos = "N";

        private DateTime xData_Retroagido_1;
        private DateTime xData_Retroagido_2;
        private DateTime xData_Retroagido_Origem;
        private Int32 xRetroagido_Resp;

        

        #region class Requisitos

        public class Requisitos
        {
            private bool _R01 = true;

            public bool R01
            {
                get { return _R01; }
                set { _R01 = value; }
            }
            private bool _R02 = true;

            public bool R02
            {
                get { return _R02; }
                set { _R02 = value; }
            }

            private bool _R03 = true;

            public bool R03
            {
                get { return _R03; }
                set { _R03 = value; }
            }

            private bool _R04 = true;

            public bool R04
            {
                get { return _R04; }
                set { _R04 = value; }
            }

            private bool _R05 = true;

            public bool R05
            {
                get { return _R05; }
                set { _R05 = value; }
            }
        }
        #endregion

        #region Inicializar

        public PPP99(Empregado empregado, string zPreposto, string zAssinatura, string zRiscos)
            //: this(empregado, false, new DateTime(), true, false, new Requisitos(), string.Empty, zPreposto)
            : this(empregado, false, new DateTime(), false, true, new Requisitos(), string.Empty, zPreposto, zAssinatura, zRiscos)   // problema quando coloca para pegar antes de 2004, exibe uma lista enorme na monitora��o biol�gica, mesmo sendo recentemente admitido.
        {

        }

        public PPP99(   Empregado empregado, 
                        bool ViaNetPdt, 
                        DateTime dataEmissao, 
                        bool SomentePeriodoAposJan2004, 
                        bool PeriodoDesvinculadoProfissiografia,
                        Requisitos requisitos,
                        string Observacao,
                        string xPreposto,
                        string xAssinatura,
                        string xRiscos)
        {
            this.empregado = empregado;
            this.dataEmissao = dataEmissao;
            this.ViaNetPdt = ViaNetPdt;
            this.SomentePeriodoAposDataInicioPPP = SomentePeriodoAposJan2004;
            this.PeriodoDesvinculadoProfissiografia = PeriodoDesvinculadoProfissiografia;
            this.requisitos = requisitos;
            this.Observacao = Observacao;
            this.Preposto = xPreposto;
            this.Assinatura = xAssinatura;
            this.Riscos = xRiscos;

            if (Ilitera.Data.Table.IsWeb 
                && (empregado.nID_EMPR.Id == (int)Clientes.VendrameSymrise
                    || empregado.nID_EMPR.Id == (int)Clientes.VendrameSymriseSorocaba))
            {
                this.SomentePeriodoAposDataInicioPPP = false;
                this.PeriodoDesvinculadoProfissiografia = true;
            }

            Inicializar();
        }

        public void Inicializar()
        {
            if (this.empregado.mirrorOld == null)
                this.empregado.Find();

            System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");      

            if (SomentePeriodoAposDataInicioPPP
                && empregado.hDT_DEM != new DateTime()
                && empregado.hDT_DEM < dataInicioPPP)
                throw new Exception("Este empregado n�o possui o PPP! A sua data de demiss�o � inferior a " + dataInicioPPP.ToString("dd-MM-yyyy", ptBr) + "."
                    + " No PPP apenas constam informa��es referentes ao empregado a partir de " + dataInicioPPP.ToString("dd-MM-yyyy", ptBr) + "."
                    + " Em caso de maiores esclarecimentos, entre em contato com a Ilitera pelo email"
                    + " luizmario@ilitera.com.br");

            cliente = new Cliente();
            cliente.Find(empregado.nID_EMPR.Id);
        }

        #endregion

        #region GetReport

        public ReportPPP99 GetReportPPP()
        {
            return this.GetReportPPP("PDF");
        }

        public ReportPPP99 GetReportPPP(string TipoRelatorio)
        {
            ReportPPP99 report = new ReportPPP99();

            DataSet dsProfissiografia = DataSourceProfissiografia();

            report.OpenSubreport("Cat").SetDataSource(DataSourceCAT());
            report.OpenSubreport("Periodo").SetDataSource(dsProfissiografia);
            report.OpenSubreport("Profissiografia").SetDataSource(dsProfissiografia);
            report.OpenSubreport("Exposicao").SetDataSource(DataSourceExposicao());

            if (dsEngenheiros == null)
            {
                dsEngenheiros = new DataSet();
                dsEngenheiros.Tables.Add(GetTableEngenharia());
            }

            report.OpenSubreport("Engenheiros").SetDataSource(dsEngenheiros);
            report.OpenSubreport("Requisitos").SetDataSource(DataSourceRequisitos());
            report.OpenSubreport("Exames").SetDataSource(DataSourceExames());
            report.OpenSubreport("Coordenadores").SetDataSource(DataSourceCoordenadores());

            report.SetDataSource(DataSourcePPP(TipoRelatorio));

            report.Refresh();

            SetTempoProcessamento(report);

            return report;
        }


        public ReportPPP99_Global GetReportPPP_Global()
        {
            return this.GetReportPPP_Global("PDF");
        }

        public ReportPPP99_Global GetReportPPP_Global(string TipoRelatorio)
        {
            ReportPPP99_Global report = new ReportPPP99_Global();

            DataSet dsProfissiografia = DataSourceProfissiografia();

            report.OpenSubreport("Cat").SetDataSource(DataSourceCAT());
            report.OpenSubreport("Periodo").SetDataSource(dsProfissiografia);
            report.OpenSubreport("Profissiografia").SetDataSource(dsProfissiografia);
            report.OpenSubreport("Exposicao").SetDataSource(DataSourceExposicao());

            if (dsEngenheiros == null)
            {
                dsEngenheiros = new DataSet();
                dsEngenheiros.Tables.Add(GetTableEngenharia());
            }

            report.OpenSubreport("Engenheiros").SetDataSource(dsEngenheiros);
            report.OpenSubreport("Requisitos").SetDataSource(DataSourceRequisitos());
            report.OpenSubreport("Exames").SetDataSource(DataSourceExames());
            report.OpenSubreport("Coordenadores").SetDataSource(DataSourceCoordenadores());

            report.SetDataSource(DataSourcePPP(TipoRelatorio));

            report.Refresh();

            SetTempoProcessamento(report);

            return report;
        }


        #endregion

        #region DataSource

        #region PPP

        #region DataSourcePPP

        public DataSet DataSourcePPP(string TipoRelatorio)
        {
            DataSet ds = new DataSet();

            DataTable table = GetTablePPP();
            ds.Tables.Add(table);

            DataRow newRow = ds.Tables[0].NewRow();

            Endereco endereco = cliente.GetEndereco();

            newRow["ComAssinatura"] = this.Assinatura;


            if (Ilitera.Data.SQLServer.EntitySQLServer.xDB2.IndexOf("Prajna") > 0)
            {
                newRow["Base"] = "Prajna";
            }
            else
            {
                newRow["Base"] = "Ilitera";
            }

            if (cliente.PPP_CNPJ_Nome_Matriz == true)
            {
                cliente.IdJuridicaPai.Find();

                if (cliente.IdJuridicaPai.Id != 0)
                {
                    if (Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper().IndexOf("GLOBAL") > 0)
                    {
                        newRow["RAZAO_SOCIAL"] = cliente.IdJuridicaPai.NomeCompleto;

                        endereco = cliente.IdJuridicaPai.GetEndereco();

                        newRow["ENDERECO"] = endereco.GetEndereco();
                        newRow["CEP"] = endereco.Cep;
                        newRow["CIDADE"] = endereco.GetCidade();
                        newRow["ESTADO"] = endereco.GetEstado();

                    }
                    else
                    {
                        newRow["RAZAO_SOCIAL"] = cliente.IdJuridicaPai.NomeCompleto + " : " + cliente.NomeAbreviado;

                        newRow["ENDERECO"] = endereco.GetEndereco();
                        newRow["CEP"] = endereco.Cep;
                        newRow["CIDADE"] = endereco.GetCidade();
                        newRow["ESTADO"] = endereco.GetEstado();

                    }

                    if (cliente.CEI != null && cliente.CEI != "")
                        newRow["CNPJ"] = cliente.IdJuridicaPai.GetCnpj() + "  " + cliente.CEI;
                    else
                        newRow["CNPJ"] = cliente.IdJuridicaPai.GetCnpj();
                }
                else
                {
                    newRow["RAZAO_SOCIAL"] = cliente.NomeCompleto;
                    if (cliente.CEI != null && cliente.CEI != "")
                        newRow["CNPJ"] = cliente.GetCnpj() + "  " + cliente.CEI;
                    else
                        newRow["CNPJ"] = cliente.GetCnpj();

                    newRow["ENDERECO"] = endereco.GetEndereco();
                    newRow["CEP"] = endereco.Cep;
                    newRow["CIDADE"] = endereco.GetCidade();
                    newRow["ESTADO"] = endereco.GetEstado();


                }
            }
            else
            {
                newRow["RAZAO_SOCIAL"] = cliente.NomeCompleto;
                if (cliente.CEI != null && cliente.CEI != "")
                    newRow["CNPJ"] = cliente.GetCnpj() + "  " + cliente.CEI;
                else
                    newRow["CNPJ"] = cliente.GetCnpj();

                newRow["ENDERECO"] = endereco.GetEndereco();
                newRow["CEP"] = endereco.Cep;
                newRow["CIDADE"] = endereco.GetCidade();
                newRow["ESTADO"] = endereco.GetEstado();

            }


            newRow["nID_EMPREGADO"] = empregado.Id;
            /***********************************************/
            /*					CNAE					   */
            /***********************************************/

            System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");      

            cliente.IdCNAE.Find();
            newRow["CNAE"] = cliente.IdCNAE.Codigo;
            newRow["CBO"] = "";
            newRow["NOME_EMPREGADO"] = empregado.tNO_EMPG;
            //newRow["PIS_PASEP"] = empregado.nNO_PIS_PASEP.ToString(); ;
            newRow["PIS_PASEP"] = empregado.tNO_CPF.ToString(); ;
            newRow["DATA_NASCIMENTO"] = empregado.hDT_NASC.ToString("dd-MM-yyyy", ptBr); // Ilitera.Common.Utility.TratarDateTime(empregado.hDT_NASC);
            newRow["SEXO"] = empregado.tSEXO;
            newRow["ADMISSAO"] = empregado.hDT_ADM;
            newRow["NOME_FUNCAO"] = "";
            newRow["DESCRICAO_FUNCAO"] = "";
            newRow["REQUISITOS_FUNCAO"] = "";
            newRow["EXPOSICAO_AG_NCV"] = 0;
            /***********************************************/
            /*			   PREPOSTO DA EMPRESA  	   */
            /***********************************************/



            if (this.Preposto.Trim() != "" && this.Preposto.Trim() != "NIT01")
            {
                //newRow["NOME_PREPOSTO"] = this.Preposto;
                int rPosit = 0;
                int rPosit2 = 0;
                int rDiv = 0;
                string rPreposto = "";
                string rPreposto2 = "";
                string rNIT = "";
                string rNIT2 = "";

                rDiv = this.Preposto.IndexOf("|");

                rPreposto = this.Preposto.Substring(0,rDiv).Trim();
                rPreposto2 = this.Preposto.Substring(rDiv+1 ).Trim();


                rPosit = rPreposto.IndexOf("NIT01", 0);
                rPosit2 = rPreposto2.IndexOf("NIT01", 0);

                if (rPosit > 0)
                {
                    rNIT = rPreposto.Substring(rPosit + 5);
                    rPreposto = rPreposto.Substring(0, rPosit);                    
                }

                if (rPosit2 > 0)
                {
                    rNIT2 = rPreposto2.Substring(rPosit2 + 5);
                    rPreposto2 = rPreposto2.Substring(0, rPosit2);
                 }

                newRow["NOME_PREPOSTO"] = rPreposto + System.Environment.NewLine + rPreposto2;
                newRow["NIT_PREPOSTO"] = rNIT + System.Environment.NewLine + rNIT2;

            }
            else
            {
                string rNomeCompleto = "";
                string rNomeCodigo = "";
                string rTitulo = "";
                string rNumero = "";

                if (cliente.IdRespPPP.Id != 0)
                {
                    if (cliente.IdRespPPP.mirrorOld == null)
                        cliente.IdRespPPP.Find();

                    rNomeCompleto = cliente.IdRespPPP.NomeCompleto;
                    rNomeCodigo = cliente.IdRespPPP.CPF; //.NomeCodigo;
                    rTitulo = cliente.IdRespPPP.Titulo;
                    rNumero = cliente.IdRespPPP.Numero;
                }

                if (cliente.IdRespPPP2.Id != 0)
                {
                    if (cliente.IdRespPPP2.mirrorOld == null)
                        cliente.IdRespPPP2.Find();

                    rNomeCompleto = System.Environment.NewLine + cliente.IdRespPPP.NomeCompleto;
                    rNomeCodigo = System.Environment.NewLine + cliente.IdRespPPP.CPF; //.NomeCodigo;
                    rTitulo = System.Environment.NewLine + cliente.IdRespPPP.Titulo;
                    rNumero = System.Environment.NewLine + cliente.IdRespPPP.Numero;
                }

                newRow["NOME_PREPOSTO"] = rNomeCompleto;
                newRow["NIT_PREPOSTO"] = rNomeCodigo;
                newRow["TITULO_PREPOSTO"] = rTitulo;
                newRow["NUMERO_PREPOSTO"] = rNumero;


            }
            
            
            
            /***********************************************/
            /*					  ANO					   */
            /***********************************************/
            /*O ano da primeira data do 1�periodo e o ano da ultima data do ultimo periodo se demitido
             * e o ano atual se ele n�o tiver demitido
             * */
            EmpregadoFuncao emprFuncaoMin = new EmpregadoFuncao();
            emprFuncaoMin.FindMin("hDT_INICIO", "nID_EMPREGADO=" + empregado.Id);

            if (empregado.hDT_DEM != new DateTime() && empregado.hDT_DEM != new DateTime(1753, 1, 1))
                newRow["tANO"] = emprFuncaoMin.hDT_INICIO.Year + " a " + empregado.hDT_DEM.Year;
            else
                newRow["tANO"] = emprFuncaoMin.hDT_INICIO.Year + " a " + DateTime.Today.Year;
            /***********************************************/
            /*					  CTPS					   */
            /***********************************************/
            //newRow["CTPS"] = empregado.GetCTPS();
            newRow["CTPS"] = empregado.tCOD_EMPR.Trim();

            newRow["tDEMISSAO"] = Ilitera.Common.Utility.TratarData(empregado.hDT_DEM);
            /***********************************************/
            /*					  CATS					   */
            /***********************************************/
            ArrayList list = new CAT().Find("IdEmpregado=" + empregado.Id);

            if (list.Count == 0)
                newRow["CATS_EMITIDAS"] = "N�O";
            else
                newRow["CATS_EMITIDAS"] = "SIM";
            
            /***********************************************/
            /*					  FOTO					   */
            /***********************************************/
            if (TipoRelatorio == "PDF")
            {
                try
                {
                    string path = empregado.FotoEmpregado();

                    if (path!=string.Empty ) //&& System.IO.File.Exists(path))
                    {
                        if (path.ToUpper().IndexOf("FOTOSDOCSDIGITAIS") < 1)
                        {
                            path = path.Substring(0, path.IndexOf("\\") + 1) + "FOTOSDOCSDIGITAIS\\" + path.Substring(path.IndexOf("\\") + 1);
                        }

                        newRow["iFOTO"] = Ilitera.Common.Fotos.GetByteFoto_Uri(path);
                        newRow["ComFoto"] = true;
                    }
                    else
                        newRow["ComFoto"] = false;
                }
                catch
                {
                    newRow["ComFoto"] = false;
                }
            }
            else if (TipoRelatorio == "Word")
                newRow["ComFoto"] = false;

            /***********************************************/
            /*					 DATA					   */
            /***********************************************/
            

            if (empregado.hDT_DEM != new DateTime() && SomentePeriodoAposDataInicioPPP)
            {
                newRow["EMISSAO"] = empregado.hDT_DEM.ToString("dd-MM-yyyy", ptBr);
            }
            else
            {
                if (dataEmissao == new DateTime())
                    dataEmissao = DateTime.Today;

                newRow["EMISSAO"] = dataEmissao.ToString("dd-MM-yyyy", ptBr);
            }

            newRow["EXPOSICAO_AGENTE"] = "Ocasional/intermitente";

            if (xCA_Acidente.Trim() != "")
            {
                xCA_Acidente = "Embora o documento que d� origem aos dados do presente PPP tratar exclusivamente dos riscos qu�micos, f�sicos e biol�gicos, conforme determina as instru��es normativas pertinentes e emitidas pela Receita Federal do Brasil ou, por analogia, pela Norma Regulamentadora n� 9, do Minist�rio do Trabalho e Emprego, seguem os c�digos dos Certificados de Aprova��o dos EPI � Equipamentos de Prote��o Individual utilizados para prote��o de riscos mec�nicos: " + Environment.NewLine + xCA_Acidente;
            }


            //checar PPRAs retroagidos
            string xRetroagido = "";

            if (xData_Retroagido_Origem != new DateTime(1900, 1, 1))
            {
                if (xData_Retroagido_1 != new DateTime(1900, 1, 1))
                {
                    string xD1 = xData_Retroagido_1.ToString("dd/MM/yyyy", ptBr);
                    string xD2 = xData_Retroagido_2.ToString("dd/MM/yyyy", ptBr);
                    string xD_Origem = xData_Retroagido_Origem.ToString("dd/MM/yyyy", ptBr);

                    string xResp = "";
                    Prestador xPrest = new Prestador();
                    xPrest.Find(xRetroagido_Resp);

                    if (xPrest.Id != 0)
                    {
                        xResp = xPrest.NomeAbreviado;
                    }

                    if (xData_Retroagido_1 != xData_Retroagido_2)
                    {
                        xRetroagido = System.Environment.NewLine + "Em fun��o da inexist�ncia de levantamentos ambientais atualizados e pertinentes ao per�odo de " + xD1 + " a " + xD2 + ", os dados utilizados para elabora��o deste PPP, foram aqueles constantes no laudo de " + xD_Origem + ", de responsabilidade t�cnica de " + xResp + ", uma vez que a administra��o da empresa atesta que no per�odo em quest�o n�o houve altera��o significativa no processo produtivo e os riscos ambientais se mantiveram similares.";
                    }
                    else
                    {
                        xRetroagido = System.Environment.NewLine + "Em fun��o da inexist�ncia de levantamento ambiental atualizado e pertinente ao per�odo de " + xD1 + ", os dados utilizados para elabora��o deste PPP, foram aqueles constantes no laudo de " + xD_Origem + ", de responsabilidade t�cnica de " + xResp + ", uma vez que a administra��o da empresa atesta que no per�odo em quest�o n�o houve altera��o significativa no processo produtivo e os riscos ambientais se mantiveram similares.";
                    }
                }
            }






            if (Ilitera.Data.SQLServer.EntitySQLServer.xDB2.IndexOf("Prajna") > 0)
            {
                if (cliente.NomeAbreviado.IndexOf("STATKRA") >= 0) 
                {

                    newRow["OBSERVACAO"] = this.Observacao + xRetroagido;

                    GetEmpregadoFuncao();
                    int zAux = 0;

                    foreach (EmpregadoFuncao emprFunc in listEmpregadoFuncao)
                    {
                        zAux = zAux + 1;

                        if (zAux == listEmpregadoFuncao.Count)
                        {
                            LaudoTecnico laudoTecnico = new LaudoTecnico();
                            laudoTecnico = emprFunc.GetLaudoTecnico();

                            Ghe ghe = new Ghe();

                            GheEmpregado gheEmpregado = new GheEmpregado();
                            gheEmpregado.Find("nID_LAUD_TEC=" + laudoTecnico.Id + " AND nID_EMPREGADO_FUNCAO=" + emprFunc.Id);

                            ghe.Find(gheEmpregado.nID_FUNC.Id);

                            if ( ghe.Id != 0 )
                            {
                                if (ghe.tNO_FUNC.ToUpper().IndexOf("OPERACIONAL")>=0)
                                {
                                    newRow["OBSERVACAO"] = this.Observacao + xRetroagido + System.Environment.NewLine + "As atividades em �rea de risco el�trico s�o realizadas em car�ter habitual e permanente, n�o ocasional e nem intermitente";
                                }                                
                            }
                        }
                    }

                }
                else
                {
                    newRow["OBSERVACAO"] = this.Observacao + xRetroagido;
                }
            }
            else if (Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper().IndexOf("GLOBAL") > 0)
                newRow["OBSERVACAO"] = "1 - SE��O III � RESULTADO DO MONITORAMENTO BIOL�GICO, Conforme Memorando Circular Conjunto n� 02/INSS/DIRBEN/DIREP, de 15 de Janeiro de 2004 do INSS; que suspende a exig�ncia de preenchimento em fun��o da Resolu��o do Conselho Federal de Medicina. " + System.Environment.NewLine + "N�o houveram altera��es em layout e m�quinas" + System.Environment.NewLine + xRetroagido;  //\r\n\r\n" + this.Observacao + Environment.NewLine + xCA_Acidente;            
            else
                newRow["OBSERVACAO"] = ObservacaoPPP + "\r\n\r\n" + this.Observacao + Environment.NewLine + xCA_Acidente + xRetroagido;


            switch (empregado.nIND_BENEFICIARIO)
            {
                case (int)TipoBeneficiario.BeneficiarioReabilitado:
                    newRow["BR_PDH"] = "BR";
                    break;
                case (int)TipoBeneficiario.PortadorDeficiencia:
                    newRow["BR_PDH"] = "PDH";
                    break;
                case (int)TipoBeneficiario.NaoAplicavel:
                    newRow["BR_PDH"] = "NA";
                    break;
                default:
                    newRow["BR_PDH"] = "NA";
                    break;
            }

            if (empregado.nID_REGIME_REVEZAMENTO.Id == 0)
                newRow["REGIME_REVEZAMENTO"] = "NA";
            else
                newRow["REGIME_REVEZAMENTO"] = empregado.nID_REGIME_REVEZAMENTO.ToString();

            ds.Tables[0].Rows.Add(newRow);

            return ds;
        }
        #endregion

        #region ObservacaoPPP

        private const string ObservacaoPPP = "O Perfil Profissiogr�fico Previdenci�rio objetiva registrar os dados dos levantamentos ambientais peri�dicos realizados no meio ambiente de trabalho das empresas e a verifica��o das condi��es de salubridade ou insalubridade a que o trabalhador est� submetido, visando a obten��o ou n�o do benef�cio da sua aposentadoria especial. "
                + "Os dados do PPP s�o preenchidos a partir do Laudo T�cnico de Condi��es Ambientais de Trabalho - LTCAT, que foi institu�do pela Lei n� 8.213/91, mas que somente foi regulamentado pelo Minist�rio da Previd�ncia e Assist�ncia Social � MPAS, passando a ser exigido a partir do m�s de mar�o de 1999, quando da institui��o das al�quotas adicionais do SAT � Seguro Acidente do Trabalho de 6%, 9% e 12% conforme crit�rio espec�fico. " 
                + "Em janeiro de 2004 a regulamenta��o do LTCAT e do PPP sofreu significativa altera��o, mas sua exig�ncia iniciou em mar�o de 1999. Para per�odos de trabalho anteriores, o trabalhador dever� recorrer � comprova��o de eventual direito � aposentadoria especial atrav�s da Justifica��o Administrativa � JA, que se trata de processo administrativo instaurado pelo pr�prio trabalhador junto ao INSS para comprova��o de per�odo trabalhado "  
                + "em condi��es insalubres e anterior � exig�ncia do perfil profissiogr�fico e da regulamenta��o do LTCAT que, inicialmente, ocorreu em mar�o de 1999."
                + "\r\n\r\n"
                + "As informa��es referentes � Se��o de Resultados de Monitora��o Biol�gica"
                + " n�o foram disponibilizadas pelo m�dico do trabalho � empresa, em cumprimento"
                + " � determina��o do Conselho Federal de Medicina que regulamentou o procedimento"
                + " �tico-m�dico relacionado ao Perfil Profissiogr�fico Previdenci�rio dispondo que"
                + " �� vedado ao m�dico do trabalho, sob pena de viola��o do sigilo m�dico profissional,"
                + " disponibilizar, � empresa ou ao empregador equiparado � empresa, as informa��es"
                + " exigidas no anexo XV da se��o III, �Se��o de Resultados de Monitora��o Biol�gica�,"
                + " campo 17 e seguintes, do PPP� (art. 2�, Resolu��o n� 1.715 de 08 de janeiro de 2004)"
                + " e Memorando-Circular Conjunto n�02 INSS de 15-01-04, que determina ao auditor fiscal,"
                + " abster-se da lavratura de auto de infra��o pelo n�o preenchimento do campo 17 e seguintes do PPP.";


        #endregion

        #region ObservacaoPPPElektro

        private const string ObservacaoPPPElektro = "O presente Perfil Profissiogr�fico Previdenci�rio - PPP"
            + " contempla todo o per�odo laborado pelo trabalhador na empresa, desde a sua admiss�o"
            + " at� a presente data, constituindo atualmente o �nico documento exig�vel para comprova��o"
            + " perante a Previd�ncia Social face ao disposto na norma legal que disp�e"
            + " �quando for apresentado o documento de trata o � 14, do art. 178, desta Instru��o Normativa "
            + "(Perfil Profissiogr�fico Previdenci�rio), contemplando tamb�m os per�odos laborados at�"
            + " 31 de dezembro de 2003 (v�spera da data do in�cio da vig�ncia do PPP), ser�o dispensados"
            + " os demais documentos referidos neste artigo� (art. 161, � 1�, IN/INSS n� 20/07)."
            + "\r\n\r\n"
            + "Importante ressaltar que �a concess�o de aposentadoria especial depender� de comprova��o"
            + " pelo segurado perante o INSS, do tempo de trabalho permanente, n�o ocasional nem"
            + " intermitente, exercido em condi��es especiais que prejudiquem � sa�de ou � integridade"
            + " f�sica (...). O segurado dever� comprovar a efetiva exposi��o aos agentes nocivos qu�micos,"
            + " f�sicos, biol�gicos, ou associa��o de agentes prejudiciais � sa�de ou � integridade f�sica"
            + " pelo per�odo equivalente ao exigido para a concess�o do benef�cio�"
            + " (art. 155, �� 1� e 2�, IN/INSS n� 20/07), sendo que, atualmente, a eletricidade n�o se"
            + " enquadra dentre os referidos agentes que permitem a concess�o de aposentadoria especial."
            + "\r\n\r\n"
            + "Entretanto, no exerc�cio da presente atividade laboral ocorreram exposi��es"
            + " habituais e permanentes ao agente eletricidade, atrav�s de trabalhos e/ou"
            + " opera��es em instala��es ou equipamentos el�tricos com tens�es superiores a 250 Volts,"
            + " submetidas a riscos de acidentes em condi��es de perigo de vida, motivo pelo qual,"
            + " com fundamento no c�digo 1.1.8 do quadro anexo ao Decreto n� 53.831/64, o enquadramento"
            + " dessa atividade laboral para obten��o de aposentadoria especial ser� poss�vel at� 05/03/1997,"
            + " face ao disposto no art. 170, IV, da vigente Instru��o Normativa INSS/PRES n� 20/07."
            + "\r\n\r\n"
            + "Ademais, como se pode verificar na se��o de registros ambientais deste documento,"
            + " n�o ocorreram exposi��es com ultrapassagem de limites de toler�ncia,"
            + " aos agentes ru�do ou calor oriundo de fontes artificiais, em conformidade com o art."
            + " 181, IN/INSS n� 20/07, bem como com nocividade a agentes qu�micos, todos dispostos no"
            + " anexo IV, do Decreto n� 3.048/99.";
        #endregion

        #region DescricaoAtividadeEletricistaElektro

        private const string DescricaoAtividadeEletricistaElektro = "Exerc�cio de atividades"
            + " laborais em instala��es, manuten��es, opera��es, inspe��es em equipamentos"
            + " de distribui��o de energia el�trica definidas por exposi��es permanentes ao"
            + " agente eletricidade e demais fatores de riscos abaixo referidos, atrav�s de"
            + " trabalhos e/ou opera��es em ambientes internos e externos a c�u aberto em"
            + " instala��es ou equipamentos el�tricos com tens�es superiores a 250 Volts,"
            + " submetidas a riscos de acidentes em condi��es de perigo de vida, cujo enquadramento"
            + " dessas atividades, para obten��o de aposentadoria especial, com fundamento no"
            + " C�digo 1.1.8 do quadro anexo ao Decreto n� 53.831/64, ser� poss�vel at� 05/03/1997,"
            + " face ao disposto no art. 170, IV, IN/INSS n� 20/07.";
        #endregion

        #region GetTablePPP

        private static DataTable GetTablePPP()
        {
            DataTable table = new DataTable("Result");
            table.Columns.Add("nID_EMPREGADO", Type.GetType("System.Int32"));
            table.Columns.Add("RAZAO_SOCIAL", Type.GetType("System.String"));
            table.Columns.Add("ENDERECO", Type.GetType("System.String"));
            table.Columns.Add("CEP", Type.GetType("System.String"));
            table.Columns.Add("CIDADE", Type.GetType("System.String"));
            table.Columns.Add("ESTADO", Type.GetType("System.String"));
            table.Columns.Add("CNPJ", Type.GetType("System.String"));
            table.Columns.Add("CNAE", Type.GetType("System.String"));
            table.Columns.Add("CBO", Type.GetType("System.String"));
            table.Columns.Add("NOME_EMPREGADO", Type.GetType("System.String"));
            table.Columns.Add("PIS_PASEP", Type.GetType("System.String"));
            table.Columns.Add("DATA_NASCIMENTO", Type.GetType("System.DateTime"));
            table.Columns.Add("SEXO", Type.GetType("System.String"));
            table.Columns.Add("ADMISSAO", Type.GetType("System.DateTime"));
            table.Columns.Add("NOME_FUNCAO", Type.GetType("System.String"));
            table.Columns.Add("DESCRICAO_FUNCAO", Type.GetType("System.String"));
            table.Columns.Add("REQUISITOS_FUNCAO", Type.GetType("System.String"));
            table.Columns.Add("EXPOSICAO_AG_NCV", Type.GetType("System.Int32"));
            table.Columns.Add("NOME_PREPOSTO", Type.GetType("System.String"));
            table.Columns.Add("NIT_PREPOSTO", Type.GetType("System.String"));
            table.Columns.Add("TITULO_PREPOSTO", Type.GetType("System.String"));
            table.Columns.Add("NUMERO_PREPOSTO", Type.GetType("System.String"));
            table.Columns.Add("tANO", Type.GetType("System.String"));
            table.Columns.Add("CTPS", Type.GetType("System.String"));
            table.Columns.Add("tDEMISSAO", Type.GetType("System.String"));
            table.Columns.Add("CATS_EMITIDAS", Type.GetType("System.String"));
            table.Columns.Add("iFOTO", Type.GetType("System.Byte[]"));
            table.Columns.Add("EMISSAO", Type.GetType("System.String"));
            table.Columns.Add("EXPOSICAO_AGENTE", Type.GetType("System.String"));
            table.Columns.Add("OBSERVACAO", Type.GetType("System.String"));
            table.Columns.Add("BR_PDH", Type.GetType("System.String"));
            table.Columns.Add("REGIME_REVEZAMENTO", Type.GetType("System.String"));
            table.Columns.Add("ComFoto", Type.GetType("System.Boolean"));
            table.Columns.Add("ComAssinatura", Type.GetType("System.String"));
            table.Columns.Add("Base", Type.GetType("System.String"));
            return table;
        }
        #endregion

        #endregion

        #region CAT

        private DataSet DataSourceCAT()
        {
            DataSet ds = new DataSet();
            ds.Tables.Add(GetTableCAT());

            DataRow newRow;

            ArrayList listCats = new CAT().Find("IdEmpregado=" + empregado.Id + " ORDER BY NumeroCAT");

            System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");      
           
            foreach(CAT cat in listCats)
            {
                newRow = ds.Tables[0].NewRow();

                string zRecibo = "";

                Acidente rAcidente = new Acidente();
                rAcidente.Find(" IdCat = " + cat.Id);

                if (rAcidente.Id != 0)
                {
                    //ver se tem recibo do eSocial
                    Ilitera.Data.eSocial xRecibo = new Ilitera.Data.eSocial();
                    zRecibo = xRecibo.Trazer_Recibo(rAcidente.Id);
                }

                newRow["ID"] = cat.Id;
                newRow["nID_EMPREGADO"] = empregado.Id;
                newRow["dDT_CAT"] = cat.DataEmissao.ToString("dd-MM-yyyy");

                if (zRecibo == "") newRow["tNO_CAT"] = cat.NumeroCAT;
                else newRow["tNO_CAT"] = zRecibo;

                newRow["tCAT_EMITIDA"] = "SIM";

                ds.Tables[0].Rows.Add(newRow);
            }

            if (listCats.Count == 0)
            {
                for (int i = 0; i < 3; i++)
                {
                    newRow = ds.Tables[0].NewRow();
                    newRow["nID_EMPREGADO"] = empregado.Id;
                    newRow["tCAT_EMITIDA"] = "N�O";
                    if (i == 0)
                    {
                        newRow["dDT_CAT"] = "N�o houve registro de CAT";
                        newRow["tNO_CAT"] = "";
                    }
                    else
                    {
                        newRow["dDT_CAT"] = "";
                        newRow["tNO_CAT"] = "N�o houve registro de CAT";
                    }
                    ds.Tables[0].Rows.Add(newRow);
                }
            }

            int adiciona;

            if (listCats.Count == 1)
                adiciona = 2;
            else
                adiciona = Convert.ToInt32(((System.Math.Round(Convert.ToDouble(listCats.Count / 3.0D))) * 3.0D) - listCats.Count);

            for (int i = 0; i < adiciona; i++)
            {
                newRow = ds.Tables[0].NewRow();
                newRow["nID_EMPREGADO"] = empregado.Id;
                newRow["tCAT_EMITIDA"] = "SIM";
                newRow["dDT_CAT"] = "__-__-____";
                newRow["tNO_CAT"] = "-";
                ds.Tables[0].Rows.Add(newRow);
            }

            return ds;
        }

        private static DataTable GetTableCAT()
        {
            DataTable table = new DataTable("Result");
            table.Columns.Add("ID", Type.GetType("System.Int32"));
            table.Columns.Add("nID_EMPREGADO", Type.GetType("System.Int32"));
            table.Columns.Add("dDT_CAT", Type.GetType("System.String"));
            table.Columns.Add("tNO_CAT", Type.GetType("System.String"));
            table.Columns.Add("tCAT_EMITIDA", Type.GetType("System.String"));
            return table;
        }

        #endregion

        #region Profissiografia

        private DataSet DataSourceProfissiografia()
        {
            DataSet ds = new DataSet();
            ds.Tables.Add(GetTableProfissiografia());

            Boolean Ajuste_Data = false;
            DateTime zAjuste = new DateTime();

            DataRow newRow;

            int i = 1;

            GetEmpregadoFuncao();

            foreach (EmpregadoFuncao emprFunc in listEmpregadoFuncao)
            {

                LaudoTecnico laudoTecnico = new LaudoTecnico();
                //laudoTecnico.FindMax("hDT_LAUDO", "nID_EMPR=" + cliente.Id + " AND nID_LAUD_TEC IN (SELECT nID_LAUD_TEC FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblFUNC_EMPREGADO)");
                laudoTecnico = emprFunc.GetLaudoTecnico();

                Ghe ghe = new Ghe();

                GheEmpregado gheEmpregado = new GheEmpregado();
                gheEmpregado.Find("nID_LAUD_TEC=" + laudoTecnico.Id + " AND nID_EMPREGADO_FUNCAO=" + emprFunc.Id);

                ghe.Find(gheEmpregado.nID_FUNC.Id);
                
                //if (Ilitera.Data.SQLServer.EntitySQLServer.xDB2.IndexOf("Prajna") > 0)
                //{
                //    if (this.dataInicioPPP < laudoTecnico.hDT_LAUDO && Ajuste_Data == false)
                //    {
                //        //this.dataInicioPPP = laudoTecnico.hDT_LAUDO;
                //        zAjuste = laudoTecnico.hDT_LAUDO;
                //        Ajuste_Data = true;
                //    }
                //}


                if (Ilitera.Data.SQLServer.EntitySQLServer.xDB2.IndexOf("Prajna") > 0)
                {

                    DateTime zData = new DateTime();

                    zData = laudoTecnico.hDT_LAUDO;


                    ArrayList gheEmpregado2 = new GheEmpregado().Find(" nID_EMPREGADO_FUNCAO=" + emprFunc.Id);
                    foreach (GheEmpregado xGhe in gheEmpregado2)
                    {
                        LaudoTecnico zLaudo = new LaudoTecnico();
                        zLaudo.Find(xGhe.nID_LAUD_TEC.Id);

                        if (zData > zLaudo.hDT_LAUDO) zData = zLaudo.hDT_LAUDO;
                    }


                    if (this.dataInicioPPP < zData && Ajuste_Data == false)
                    {
                        //this.dataInicioPPP = laudoTecnico.hDT_LAUDO;
                        zAjuste = zData;
                        Ajuste_Data = true;
                    }
                }


                newRow = ds.Tables[0].NewRow();

                newRow["nID_EMPR"] = cliente.Id;
                newRow["nID_EMPREGADO"] = empregado.Id;

                newRow["tPERIODO"] = SomentePeriodoAposDataInicioPPP ? emprFunc.GetPeriodoParaPPP(this.dataInicioPPP) : emprFunc.GetPeriodo();                

                newRow["tNO_STR_EMPR"] = emprFunc.GetNomeSetor();

                //newRow["tNO_FUNC_EMPR"] = emprFunc.nID_CARGO.Id == 0 ? "NA" : emprFunc.GetCargoEmpregado();
                                           
                //newRow["tNO_CARGO"] = emprFunc.GetNomeFuncao();

                newRow["tNO_CARGO"] = emprFunc.nID_CARGO.Id == 0 ? "NA" : emprFunc.GetCargoEmpregado();
                newRow["tNO_FUNC_EMPR"] = emprFunc.GetNomeFuncao();

                newRow["tCBO"] = emprFunc.GetNumeroCBO();
                newRow["tDS_REQUISITO"] = emprFunc.GetRequisitoFuncao();
                newRow["NUMERO_PERIODO"] = i.ToString("##");

                //LaudoTecnico laudoTecnico = new LaudoTecnico();
                ////laudoTecnico.FindMax("hDT_LAUDO", "nID_EMPR=" + cliente.Id + " AND nID_LAUD_TEC IN (SELECT nID_LAUD_TEC FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblFUNC_EMPREGADO)");
                //laudoTecnico = emprFunc.GetLaudoTecnico();

                //Ghe ghe = new Ghe();
                
                //GheEmpregado gheEmpregado = new GheEmpregado();
                //gheEmpregado.Find("nID_LAUD_TEC=" + laudoTecnico.Id + " AND nID_EMPREGADO_FUNCAO=" + emprFunc.Id);

                //ghe.Find(gheEmpregado.nID_FUNC.Id);

                newRow["tDS_FUNC_EMPR"] = emprFunc.GetDescricaoFuncao();
                newRow["JORNADA"] = emprFunc.GetJornada();
                newRow["DESCRICAO_LOCAL_TRABALHO"] = emprFunc.GetDescricaoLocalTrabalho();
                newRow["CONCLUSAO_LTCAT"] = ghe.Conclusao(cliente);
                
                if (emprFunc.nIND_GFIP == (int)CodigoGFIP.PPRA)
                    newRow["tCOD_GFIP"] = ghe.GetGFIP();
                else
                    newRow["tCOD_GFIP"] = Ghe.GetGFIP(Convert.ToInt32((emprFunc.nIND_GFIP)));

                if (emprFunc.nID_EMPR.Id == cliente.Id)
                {
                    if (cliente.CEI != null && cliente.CEI != "")
                        newRow["tCNPJ_CEI"] = cliente.GetCnpj() + System.Environment.NewLine + cliente.CEI;
                    else
                        newRow["tCNPJ_CEI"] = cliente.GetCnpj();
                }
                else
                {
                    emprFunc.nID_EMPR.Find();
                    if (emprFunc.nID_EMPR.CEI != null && emprFunc.nID_EMPR.CEI != "")
                        newRow["tCNPJ_CEI"] = emprFunc.nID_EMPR.GetCnpj() + System.Environment.NewLine + emprFunc.nID_EMPR.CEI;
                    else
                        newRow["tCNPJ_CEI"] = emprFunc.nID_EMPR.GetCnpj();
                }

                newRow["NumeroPeriodo"] = i + "�";

                ds.Tables[0].Rows.Add(newRow);

                i++;
            }

            if (Ilitera.Data.SQLServer.EntitySQLServer.xDB2.IndexOf("Prajna") > 0)
            {
                this.dataInicioPPP = zAjuste;
            }

            return ds;
        }

        private static DataTable GetTableProfissiografia()
        {
            DataTable table = new DataTable("Result");
            table.Columns.Add("nID_EMPR", Type.GetType("System.Int32"));
            table.Columns.Add("nID_EMPREGADO", Type.GetType("System.Int32"));
            table.Columns.Add("tPERIODO", Type.GetType("System.String"));
            table.Columns.Add("tNO_STR_EMPR", Type.GetType("System.String"));
            table.Columns.Add("tNO_CARGO", Type.GetType("System.String"));
            table.Columns.Add("tNO_FUNC_EMPR", Type.GetType("System.String"));
            table.Columns.Add("tCBO", Type.GetType("System.String"));
            table.Columns.Add("tDS_REQUISITO", Type.GetType("System.String"));
            table.Columns.Add("tDS_FUNC_EMPR", Type.GetType("System.String"));
            table.Columns.Add("NUMERO_PERIODO", Type.GetType("System.String"));
            table.Columns.Add("JORNADA", Type.GetType("System.String"));
            table.Columns.Add("DESCRICAO_LOCAL_TRABALHO", Type.GetType("System.String"));
            table.Columns.Add("CONCLUSAO_LTCAT", Type.GetType("System.String"));
            table.Columns.Add("tCOD_GFIP", Type.GetType("System.String"));
            table.Columns.Add("tCNPJ_CEI", Type.GetType("System.String"));
            table.Columns.Add("NumeroPeriodo", Type.GetType("System.String"));
            return table;
        }

        #endregion

        #region Coordenadores

        private DataSet DataSourceCoordenadores()
        {
            DataRow newRow;
            DataSet ds = new DataSet();
            ds.Tables.Add(GetTableCoordenadores());




            GetEmpregadoFuncao();

            string Rwhere = "";
            Rwhere = "IdLaudoTecnico in ( -1 , ";

            foreach (EmpregadoFuncao emprFunc in listEmpregadoFuncao)
            {
                //LaudoTecnico laudoTecnico = new LaudoTecnico();
                //laudoTecnico.FindMax("hDT_LAUDO", "nID_EMPR=" + cliente.Id + " AND nID_LAUD_TEC IN (SELECT nID_LAUD_TEC FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblFUNC_EMPREGADO)");
                //laudoTecnico = emprFunc.GetLaudoTecnico();

                ArrayList listGheEmpregado = new GheEmpregado().Find(
                  "nID_EMPREGADO_FUNCAO=" + emprFunc.Id
                  + " AND nID_LAUD_TEC IN (SELECT nID_LAUD_TEC FROM tblLAUDO_TEC WHERE nID_EMPR=" + emprFunc.nID_EMPR.Id
                  + " AND (nID_PEDIDO IN (SELECT IdPedido FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.Pedido WHERE DataConclusao IS NOT NULL) OR bAE =1))"
                  + " ORDER BY (SELECT hDT_LAUDO FROM tblLAUDO_TEC WHERE nID_LAUD_TEC = tblFUNC_EMPREGADO.nID_LAUD_TEC) DESC");


                foreach (GheEmpregado laudotecnico in listGheEmpregado)
                {
                    laudotecnico.nID_LAUD_TEC.Find();
                    Rwhere = Rwhere + laudotecnico.nID_LAUD_TEC.Id.ToString() + " , ";
                }
            }

            Rwhere = Rwhere.Substring(0, Rwhere.Length - 2) + " ) order by DataPCMSO ";

            ArrayList listPcmso = new Pcmso().Find(Rwhere.ToString());



            //StringBuilder where = new StringBuilder();
            //where.Append("IdCliente=" + cliente.Id);

            ////if (SomentePeriodoAposDataInicioPPP)
            //    where.Append(" AND ((TerminoPcmso>='" + dataInicioPPP.ToString("yyyy-MM-dd") + "'"
            //                        + " AND TerminoPcmso>='" + empregado.hDT_ADM.ToString("yyyy-MM-dd")+"')"
            //                        + " OR TerminoPcmso IS NULL)");
            
            //where.Append(" ORDER BY DataPcmso");

            //ArrayList listPcmso = new Pcmso().Find(where.ToString());

            //foreach (Pcmso pcmso in listPcmso)
            //{
            //    if (empregado.hDT_DEM != new DateTime() && pcmso.DataPcmso > empregado.hDT_DEM)
            //        continue;

            //    pcmso.IdCoordenador.Find();

            //    newRow = ds.Tables[0].NewRow();
            //    newRow["nID_EMPREGADO"] = empregado.Id;
            //    newRow["tPERIODO"] = GetPeriodoPPP(pcmso);

            //    if ( pcmso.IdCoordenador.NomeCodigo.ToString().Trim() == "" )
            //        newRow["tNIT"] = "-";
            //    else
            //        newRow["tNIT"] = pcmso.IdCoordenador.NomeCodigo;

            //    newRow["tRESISTRO_CONSELHO"] = pcmso.IdCoordenador.Numero;
            //    newRow["tNOME_PROFISSIONAL"] = pcmso.IdCoordenador.NomeCompleto;
            //    ds.Tables[0].Rows.Add(newRow);
            //}

            if (listPcmso.Count > 0)
            {

                foreach (Pcmso pcmso in listPcmso)
                {
                    if (empregado.hDT_DEM != new DateTime() && pcmso.DataPcmso > empregado.hDT_DEM)
                        continue;

                    pcmso.IdCoordenador.Find();

                    newRow = ds.Tables[0].NewRow();
                    newRow["nID_EMPREGADO"] = empregado.Id;
                    newRow["tPERIODO"] = GetPeriodoPPP(pcmso);

                    //if (pcmso.IdCoordenador.NomeCodigo.ToString().Trim() == "")
                    //    newRow["tNIT"] = "-";
                    //else
                    //    newRow["tNIT"] = pcmso.IdCoordenador.NomeCodigo;

                    if (pcmso.IdCoordenador.CPF.Trim() == "")
                        newRow["tNIT"] = "-";
                    else
                        newRow["tNIT"] = pcmso.IdCoordenador.CPF.Trim();


                    newRow["tRESISTRO_CONSELHO"] = pcmso.IdCoordenador.Numero;
                    newRow["tNOME_PROFISSIONAL"] = pcmso.IdCoordenador.NomeCompleto;
                    ds.Tables[0].Rows.Add(newRow);
                }

                newRow = ds.Tables[0].NewRow();
                ds.Tables[0].Rows.Add(newRow);
            }
            else
            {
                System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");

                string zD1 = empregado.hDT_ADM.ToString("dd/MM/yyyy", ptBr);
                string zD2 = "";

                if (empregado.hDT_DEM.ToString() == "01/01/0001 00:00:00" || empregado.hDT_DEM.ToString() == "1/1/0001 0:00:00" || empregado.hDT_DEM.Year < 2)
                {
                    zD2 = DateTime.Now.ToString("dd/MM/yyyy", ptBr);
                }
                else
                {
                    if ( empregado.hDT_DEM == null )
                        zD2 = DateTime.Now.ToString("dd/MM/yyyy", ptBr);
                    else
                        zD2 = empregado.hDT_DEM.ToString("dd/MM/yyyy", ptBr);
                }


                DataSet zDs = new Laudo_Monitoracao_Biologica().Get(" nId_Laud_Tec in ( select nId_Laud_Tec from tblLaudo_Tec where nid_Empr = " + cliente.Id + " ) and ( " +
                    " case when Data_Final is not null then Data_Final  " +
                    "      when Data_Final is null     then getdate() " +
                    "end >= convert( smalldatetime, '" + zD1 + "', 103 ) and Data_Inicial  <= convert( smalldatetime, '" + zD2 + "', 103 ) )  order by Data_Inicial ");


                for (int zCont = 0; zCont < zDs.Tables[0].Rows.Count; zCont++)
                {

                    newRow = ds.Tables[0].NewRow();
                    newRow["nID_EMPREGADO"] = empregado.Id;

                    //formatar data

                    DateTime z1 = (DateTime)zDs.Tables[0].Rows[zCont]["Data_Inicial"];

                    if (z1 < empregado.hDT_ADM) z1 = empregado.hDT_ADM;


                    DateTime z2;
                    string zz2 = "";
                    if (zDs.Tables[0].Rows[zCont]["Data_Final"].ToString().Trim() == "")
                    {
                        if (empregado.hDT_DEM.ToString() == "01/01/0001 00:00:00" && empregado.hDT_DEM.Year > 1)
                        {
                            zz2 = "__/__/____";
                        }
                        else
                        {
                            zz2 = empregado.hDT_DEM.ToString("dd-MM-yyyy", ptBr);
                        }
                    }
                    else
                    {
                        z2 = (DateTime)zDs.Tables[0].Rows[zCont]["Data_Final"];

                        if (empregado.hDT_DEM.ToString() != "01/01/0001 00:00:00" && empregado.hDT_DEM.Year > 1)
                        {
                            if (empregado.hDT_DEM < z2)
                            {
                                zz2 = empregado.hDT_DEM.ToString("dd-MM-yyyy", ptBr);
                            }
                            else
                            {
                                zz2 = z2.ToString("dd-MM-yyyy", ptBr);
                            }
                        }
                        else
                        {
                            zz2 = z2.ToString("dd-MM-yyyy", ptBr);
                        }
                    }

                    newRow["tPERIODO"] = z1.ToString("dd-MM-yyyy", ptBr) + " a " + zz2;

                    newRow["tNIT"] = zDs.Tables[0].Rows[zCont]["NIT"].ToString().Trim();

                    newRow["tRESISTRO_CONSELHO"] = zDs.Tables[0].Rows[zCont]["Registro"].ToString().Trim();
                    newRow["tNOME_PROFISSIONAL"] = zDs.Tables[0].Rows[zCont]["Nome"].ToString().Trim();
                    ds.Tables[0].Rows.Add(newRow);
                }

                newRow = ds.Tables[0].NewRow();
                ds.Tables[0].Rows.Add(newRow);




            }


            return ds;
        }

        public string GetPeriodoPPP(Pcmso pcmso)
        {
            StringBuilder str = new StringBuilder();

            System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");      

            if (SomentePeriodoAposDataInicioPPP && pcmso.DataPcmso < dataInicioPPP)
                str.Append(dataInicioPPP.ToString("dd-MM-yyyy", ptBr));
            else
            {
                if( empregado.hDT_ADM > pcmso.DataPcmso)
                    str.Append(empregado.hDT_ADM.ToString("dd-MM-yyyy", ptBr));
                else
                    str.Append(pcmso.DataPcmso.ToString("dd-MM-yyyy", ptBr));
            }

            str.Append(" a ");

            if (pcmso.TerminoPcmso == new DateTime())
            {
                if (empregado.hDT_DEM == new DateTime())
                    str.Append("__-__-____");
                else
                    str.Append(empregado.hDT_DEM.ToString("dd-MM-yyyy", ptBr));
            }
            else
            {
                if (empregado.hDT_DEM != new DateTime()
                    && pcmso.TerminoPcmso > empregado.hDT_DEM)
                    str.Append(empregado.hDT_DEM.ToString("dd-MM-yyyy", ptBr));
                else
                    str.Append(pcmso.TerminoPcmso.ToString("dd-MM-yyyy", ptBr));
            }

            return str.ToString();
        }

        private static DataTable GetTableCoordenadores()
        {
            DataTable table = new DataTable("Result");
            table.Columns.Add("nID_EMPREGADO", Type.GetType("System.Int32"));
            table.Columns.Add("tPERIODO", Type.GetType("System.String"));
            table.Columns.Add("tNIT", Type.GetType("System.String"));
            table.Columns.Add("tRESISTRO_CONSELHO", Type.GetType("System.String"));
            table.Columns.Add("tNOME_PROFISSIONAL", Type.GetType("System.String"));
            return table;
        }

        #endregion

        #region Exposicao

        #region DataSourceExposicao

        private DataSet DataSourceExposicao()
        {
            DataSet dsExposicao = new DataSet();

            dsExposicao.Tables.Add(GetTableExposicao());

            int numPeriodo = 1;

            GetEmpregadoFuncao();


            xData_Retroagido_1 = new DateTime(2050, 1, 1);
            xData_Retroagido_2 = new DateTime(1900, 1, 1);
            xData_Retroagido_Origem = new DateTime(1900, 1, 1);


            foreach (EmpregadoFuncao emprFuncao in listEmpregadoFuncao)
            {
                List<LaudoTecnico> listLaudo = GetLaudoTecnicos(emprFuncao);

                foreach (LaudoTecnico laudoTecnico in listLaudo)
                {
                    Ghe ghe = emprFuncao.GetGheEmpregado(laudoTecnico);

                    //if (ghe.bPERICULOSIDADE)
                        //IsGhePericulosicade = true;
                    //else
                      //  IsGhePericulosicade = false;

                    if (ghe.Id == 0)
                        throw new Exception("� obrigat�rio que o empregado perten�a a um GHE em cada uma de suas Classifica��es Funcionais e respectivos Laudos T�cnicos!");



                    //verificar se � retroagido
                    if (laudoTecnico.nID_LAUD_TEC_Retroagido != 0)
                    {
                        LaudoTecnico nLaudo = new LaudoTecnico();
                        nLaudo.Find(laudoTecnico.nID_LAUD_TEC_Retroagido);

                        if (nLaudo.Id != 0)
                        {
                            xData_Retroagido_Origem = nLaudo.hDT_LAUDO;
                            if (nLaudo.nID_COMISSAO_3 != null && nLaudo.nID_COMISSAO_3.Id != 0)
                            {
                                xRetroagido_Resp = nLaudo.nID_COMISSAO_3.Id;
                            }
                            else if (nLaudo.nID_COMISSAO_2 != null && nLaudo.nID_COMISSAO_2.Id != 0)
                            {
                                xRetroagido_Resp = nLaudo.nID_COMISSAO_2.Id;
                            }
                            else if (nLaudo.nID_COMISSAO_1 != null && nLaudo.nID_COMISSAO_1.Id != 0)
                            {
                                xRetroagido_Resp = nLaudo.nID_COMISSAO_1.Id;
                            }
                            else
                                xRetroagido_Resp = 0;

                        }

                        if (laudoTecnico.hDT_LAUDO < xData_Retroagido_1)
                            xData_Retroagido_1 = laudoTecnico.hDT_LAUDO;

                        if (laudoTecnico.hDT_LAUDO > xData_Retroagido_2)
                            xData_Retroagido_2 = laudoTecnico.hDT_LAUDO;
                    }




                    string sPeriodoExposicao;

                    if (PeriodoDesvinculadoProfissiografia)
                        sPeriodoExposicao = GetPeriodoExposicaoPPP(emprFuncao, laudoTecnico);
                    else
                        sPeriodoExposicao = GetPeriodoExposicaoPPP(emprFuncao, laudoTecnico, listLaudo);
  
                    PopularDataSourceEngenheiro(laudoTecnico.PrestadorID2, sPeriodoExposicao);
                    
                    GheEmpregado gheEmpregado = new GheEmpregado();

                    gheEmpregado.Find("nID_LAUD_TEC=" + laudoTecnico.Id + " AND nID_EMPREGADO_FUNCAO=" + emprFuncao.Id);

                    PopularDataSourceExposicao(dsExposicao, gheEmpregado.nID_FUNC, numPeriodo, sPeriodoExposicao, emprFuncao.nID_EMPR);

                    numPeriodo += 1;
                }

                //numPeriodo += 1;
            }

            return dsExposicao;
        }
        #endregion

        #region PopularDataSourceExposicao

        private void PopularDataSourceExposicao(DataSet dsExposicao,
                                                Ghe ghe,
                                                int numPeriodo,
                                                string sPeriodo,
                                                Cliente clienteAlocado)
        {
            DataRow newRow;
            string xMetodologia = "";

            string where = "nID_FUNC=" + ghe.Id + " AND tPERIODO = '" + sPeriodo + "'";

            if (dsExposicao.Tables[0].Select(where).Length != 0)
                return;

            //List<PPRA> listPPRA = new PPRA().Find<PPRA>("nID_FUNC=" + ghe.Id
            //                                    + " AND nID_FUNC IS NOT NULL ORDER BY nID_RSC");

            List<PPRA> listPPRA;

            if ((empregado.hDT_DEM.Year < 2022 && empregado.hDT_DEM.Year > 100) || this.Riscos == "S")
            {
                listPPRA = new PPRA().Find<PPRA>("nID_FUNC=" + ghe.Id
                                       + " AND nID_RSC NOT IN ( 17,18 ) AND nID_FUNC IS NOT NULL ORDER BY nID_RSC");
            }
            else
            {
                //inibir riscos acidentes e ergonomicos
                listPPRA = new PPRA().Find<PPRA>("nID_FUNC=" + ghe.Id
                            + " and nID_RSC not IN ( 17,18 ) "
                            + " and  (  ( nID_Rsc in (9, 14, 16) and  tVL_MED > 0 and  tvl_med > sVL_Lim_Tol "
                            + "           and nid_ag_ncv in (select idagentequimico from " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1 + ".dbo.agentequimico where codigo_esocial <> '09.01.001' ) ) "
                            + "       or nId_Rsc not in (9,14,16 ) )  "
                            + " AND nID_FUNC IS NOT NULL "
                            + " ORDER BY nID_RSC ");
            }

            if (ghe.mirrorOld == null)
                ghe.Find();


            if (listPPRA.Count > 0)
            {
                //PPRA zppra = new PPRA();

                //zppra = listPPRA[0];
                string xCA = "";


                xCA = listPPRA[0].GetCA_PPP_Acidentes(clienteAlocado, empregado.Id);

                if (xCA.Trim() != "NA" && xCA.Trim() != "")
                {
                    xCA = xCA.Substring(0, xCA.Length - 2);

                    if (xCA_Acidente.IndexOf(listPPRA[0].nID_LAUD_TEC.ToString()) < 0)
                        xCA_Acidente = xCA_Acidente + "Data do Laudo: " + listPPRA[0].nID_LAUD_TEC.ToString() + "   Lista de CA: " + xCA + Environment.NewLine;
                }




                foreach (PPRA ppra in listPPRA)
                {
                    newRow = dsExposicao.Tables[0].NewRow();

                    if (ppra.mirrorOld == null)
                        ppra.Find();

                    newRow["ID"] = cliente.Id;
                    newRow["nID_FUNC"] = ghe.Id;
                    newRow["nID_EMPREGADO"] = empregado.Id;
                    newRow["tPERIODO"] = sPeriodo;
                    newRow["NumeroPeriodoRef"] = numPeriodo.ToString() + "�";

                    ppra.nID_AG_NCV.Find();

                    if (ppra.nID_AG_NCV.Nome == "Aus�ncia de Fator de Risco")
                        newRow["tTIPO_AGENTE"] = "";
                    else
                        newRow["tTIPO_AGENTE"] = ppra.GetTipoAgentePPP();


                    newRow["tAGENTE"] = ppra.GetNomeAgente();
                    newRow["tINTENSIDADE"] = ppra.GetAvaliacaoQuantitativaPPP();
                    newRow["tTIPO_EXPOSICAO"] = ppra.GetModoExposicao();

                    newRow["tEPI_EPC_TEC_UTIL"] = ppra.GetTecnicaUtilizadaPPP();

                    xMetodologia = ppra.GetTecnicaUtilizadaPPP_Metodologia();

                    if (Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper().IndexOf("PRAJNA") < 0)
                    {
                        if (this.Observacao.IndexOf(xMetodologia) < 0 && xMetodologia != "")
                            this.Observacao = this.Observacao + xMetodologia + System.Environment.NewLine;
                    }


                    //                newRow["tEPI_EFICAZ"] = ppra.GetEpiEficazPPP();
                    string tEPI = ppra.GetEpiEficazPPP();

                    newRow["tEPI_EFICAZ"] = tEPI;

                    if (tEPI.Trim() == "S")
                        newRow["MedidasProtecao"] = "Sim";
                    else
                        newRow["MedidasProtecao"] = "NA";



                    newRow["tEPC_EFICAZ"] = ppra.GetEpcEficazPPP();
                    // trazer dados da tabela " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEPI_CA - Wagner




                    if (cliente.NomeAbreviado.ToUpper().IndexOf("JAGUAR") >= 0)
                    {
                        newRow["tEPI_CA"] = "Vide anexo";
                    }
                    else
                    {
                        ppra.nID_LAUD_TEC.Find();

                        System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");

                        DateTime rPeriodo1 = new DateTime();
                        rPeriodo1 = System.Convert.ToDateTime(sPeriodo.Substring(0, 10), ptBr);

                        DateTime rPeriodo2 = new DateTime();

                        if (sPeriodo.Substring(15, 10) == "__-__-____")
                            rPeriodo2 = System.DateTime.Now;
                        else
                            rPeriodo2 = System.Convert.ToDateTime(sPeriodo.Substring(15, 10), ptBr);


                        newRow["tEPI_CA"] = ppra.GetCA_PPP(clienteAlocado, empregado.Id, ppra.nID_LAUD_TEC.hDT_LAUDO, rPeriodo1, rPeriodo2);
                    }


                    if (ppra.Risco_Insignificante == false) dsExposicao.Tables[0].Rows.Add(newRow);
                }
            }
            else
            {
                newRow = dsExposicao.Tables[0].NewRow();

                newRow["ID"] = cliente.Id;
                newRow["nID_FUNC"] = ghe.Id;
                newRow["nID_EMPREGADO"] = empregado.Id;
                newRow["tPERIODO"] = sPeriodo;
                newRow["NumeroPeriodoRef"] = numPeriodo.ToString() + "�";


                newRow["tTIPO_AGENTE"] = "NA";

                newRow["tAGENTE"] = "Aus�ncia de Riscos";
                newRow["tINTENSIDADE"] = "NA";
                newRow["tTIPO_EXPOSICAO"] = "NA";

                newRow["tEPI_EPC_TEC_UTIL"] = "NA";

                newRow["tEPI_EFICAZ"] = "NA";

                newRow["MedidasProtecao"] = "NA";

                newRow["tEPC_EFICAZ"] = "NA";

                newRow["tEPI_CA"] = "NA";


                dsExposicao.Tables[0].Rows.Add(newRow);
            }



        }
        #endregion

            #region GetTableExposicao

        private DataTable GetTableExposicao()
        {
            DataTable table = new DataTable("Result");

            table.Columns.Add("ID", Type.GetType("System.Int32"));
            table.Columns.Add("nID_FUNC", Type.GetType("System.Int32"));
            table.Columns.Add("nID_EMPREGADO", Type.GetType("System.Int32"));
            table.Columns.Add("tPERIODO", Type.GetType("System.String"));
            table.Columns.Add("tTIPO_AGENTE", Type.GetType("System.String"));
            table.Columns.Add("tAGENTE", Type.GetType("System.String"));
            table.Columns.Add("tINTENSIDADE", Type.GetType("System.String"));
            table.Columns.Add("tTIPO_EXPOSICAO", Type.GetType("System.String"));
            table.Columns.Add("tEPI_EPC_TEC_UTIL", Type.GetType("System.String"));
            table.Columns.Add("tEPI_EFICAZ", Type.GetType("System.String"));
            table.Columns.Add("tEPC_EFICAZ", Type.GetType("System.String"));
            table.Columns.Add("tGFIP", Type.GetType("System.String"));
            table.Columns.Add("tEPI_CA", Type.GetType("System.String"));
            table.Columns.Add("NumeroPeriodoRef", Type.GetType("System.String"));
            table.Columns.Add("MedidasProtecao", Type.GetType("System.String"));

            return table;
        }
        #endregion

        #endregion

        #region Engenharia

        #region PopularDataSourceEngenheiro

        private void PopularDataSourceEngenheiro(Prestador engenheiro, string sPeriodo)
        {
            if (dsEngenheiros == null)
            {
                dsEngenheiros = new DataSet();
                dsEngenheiros.Tables.Add(GetTableEngenharia());
            }

            if (engenheiro.Id == 0)
                return;

            DataRow newRow;

            if (engenheiro.mirrorOld == null)
                engenheiro.Find();

            if (engenheiro.IdPessoa.mirrorOld == null)
                engenheiro.IdPessoa.Find();

            newRow = dsEngenheiros.Tables[0].NewRow();

            newRow["nID_EMPREGADO"] = empregado.Id;
            newRow["tPERIODO"] = sPeriodo.Replace("\n", string.Empty);

            //if ( engenheiro.IdPessoa.NomeCodigo.ToString().Trim() == "" )
            //    newRow["tNIT"] = "-";
            //else
            //    newRow["tNIT"] = engenheiro.IdPessoa.NomeCodigo;

            if (engenheiro.CPF.Trim() == "")
                newRow["tNIT"] = "-";
            else
                newRow["tNIT"] = engenheiro.CPF.Trim();


            newRow["tRESISTRO_CONSELHO"] = engenheiro.Numero;
            newRow["tNOME_PROFISSIONAL"] = engenheiro.NomeCompleto;

            string where = "nID_EMPREGADO=" + newRow["nID_EMPREGADO"]
                            + " AND tPERIODO = '" + newRow["tPERIODO"] + "'"
                            + " AND tNIT = '" + newRow["tNIT"] + "'"
                            + " AND tRESISTRO_CONSELHO = '" + newRow["tRESISTRO_CONSELHO"] + "'"
                            + " AND tNOME_PROFISSIONAL = '" + newRow["tNOME_PROFISSIONAL"] + "'";

            if (dsEngenheiros.Tables[0].Select(where).Length == 0)
                dsEngenheiros.Tables[0].Rows.Add(newRow);
        }
        #endregion

        #region GetTableEngenharia

        private DataTable GetTableEngenharia()
        {
            DataTable table = new DataTable("Result");
            table.Columns.Add("nID_EMPREGADO", Type.GetType("System.Int32"));
            table.Columns.Add("tPERIODO", Type.GetType("System.String"));
            table.Columns.Add("tNIT", Type.GetType("System.String"));
            table.Columns.Add("tRESISTRO_CONSELHO", Type.GetType("System.String"));
            table.Columns.Add("tNOME_PROFISSIONAL", Type.GetType("System.String"));
            return table;
        }
        #endregion

        #endregion

        #region Requisitos

        private DataSet DataSourceRequisitos()
        {
            DataSet ds = new DataSet();

            DataTable table = GetTableRequisitos();
            ds.Tables.Add(table);

            DataRow newRow = ds.Tables[0].NewRow();

            newRow["nID_EMPREGADO"] = empregado.Id;

            if (empregado.UsaEPI())
            {
                newRow["R01"] = requisitos.R01 ? "S" : "N";
                newRow["R02"] = requisitos.R02 ? "S" : "N";
                newRow["R03"] = requisitos.R03 ? "S" : "N";
                newRow["R04"] = requisitos.R04 ? "S" : "N";
                newRow["R05"] = requisitos.R05 ? "S" : "N";
            }
            else
            {
                newRow["R01"] = "NA";
                newRow["R02"] = "NA";
                newRow["R03"] = "NA";
                newRow["R04"] = "NA";
                newRow["R05"] = "NA";
            }

            if (Ilitera.Data.SQLServer.EntitySQLServer.xDB2.IndexOf("Rophe") > 0) newRow["R01"] = "ALPHA";

            ds.Tables[0].Rows.Add(newRow);

            return ds;
        }

        private DataTable GetTableRequisitos()
        {
            DataTable table = new DataTable("Result");

            table.Columns.Add("nID_EMPREGADO", Type.GetType("System.Int32"));
            table.Columns.Add("R01", Type.GetType("System.String"));
            table.Columns.Add("R02", Type.GetType("System.String"));
            table.Columns.Add("R03", Type.GetType("System.String"));
            table.Columns.Add("R04", Type.GetType("System.String"));
            table.Columns.Add("R05", Type.GetType("System.String"));

            return table;
        }

        #endregion

        #region Exames

        public DataSet DataSourceExames()
        {
            DataSet ds = new DataSet();

            DataTable table = GetTableExames();
            ds.Tables.Add(table);

            DataRow newRow = ds.Tables[0].NewRow();


            if (Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper().IndexOf("PRAJNA") > 0)
            {
                newRow["nID_EMPREGADO"] = empregado.Id;
                newRow["tNATUREZA"] = "EXAMES M�DICOS CL�NICOS E COMPLEMENTARES (Quadros I e II, da NR-07)   Informa��es n�o disponibilizadas conforme Resolu��o CFM n.1715, de 08/janeiro/2004 entando � disposi��o do Perito M�dico do INSS mediante solicita��o formal.";
                ds.Tables[0].Rows.Add(newRow);
            }
            else if (empregado.nID_EMPR.Id != 1136 && (Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper().IndexOf("GLOBAL") < 0)) // Verifica se � a Ariston ou global
            {
                newRow["nID_EMPREGADO"] = empregado.Id;
                newRow["tNATUREZA"] = "Vide observa��es ao final do relat�rio";

                ds.Tables[0].Rows.Add(newRow);
            }
            else
            {
                ArrayList listTodosExames = new ExameBase().Find("IdEmpregado=" + empregado.Id + " ORDER BY DataExame");

                foreach (ExameBase exameBase in listTodosExames)
                {
                    newRow = ds.Tables[0].NewRow();

                    exameBase.IdExameDicionario.Find();

                    if (exameBase.IdExameDicionario.IndExame == (int)IndTipoExame.Clinico)
                    {

                        newRow["nID_EMPREGADO"] = empregado.Id;
                        newRow["dDT_EXAME"] = exameBase.DataExame;
                        newRow["tNO_EXAME_TIPO"] = exameBase.IdExameDicionario.Nome.Substring(0, 1);
                        newRow["tNATUREZA"] = "Cl�nico";

                        if (exameBase.IdExameDicionario.Id == (int)IndExameClinico.Admissional)
                            newRow["tEXAME_RS"] = "R";
                        else
                            newRow["tEXAME_RS"] = "S";

                        newRow["tRESULTADO"] = "Vide observa��es abaixo";

                        ds.Tables[0].Rows.Add(newRow);
                    }
                    else if (exameBase.IdExameDicionario.IndExame == (int)IndTipoExame.Complementar)
                    {
                        newRow["nID_EMPREGADO"] = empregado.Id;
                        newRow["dDT_EXAME"] = exameBase.DataExame;
                        newRow["tNO_EXAME_TIPO"] = "Complementar";
                        newRow["tNATUREZA"] = exameBase.IdExameDicionario.Nome;
                        ArrayList listExameCompl = new Complementar().Find("IdEmpregado=" + empregado.Id + " AND IdExameDicionario=" + exameBase.IdExameDicionario.Id + " ORDER BY DataExame");
                        if (listExameCompl.Count == 1)
                            newRow["tEXAME_RS"] = "R";
                        else if (listExameCompl.Count == 1 || exameBase.IdExameDicionario.Id == ((Complementar)listExameCompl[0]).IdExameDicionario.Id)
                            newRow["tEXAME_RS"] = "R";
                        else
                            newRow["tEXAME_RS"] = "S";
                        newRow["tRESULTADO"] = "Vide Observa��es abaixo";
                        ds.Tables[0].Rows.Add(newRow);
                    }
                    else if (exameBase.IdExameDicionario.IndExame == (int)IndTipoExame.Audiometrico)
                    {
                        Audiometria audiometria = new Audiometria(exameBase.Id);
                        newRow["nID_EMPREGADO"] = empregado.Id;
                        newRow["dDT_EXAME"] = audiometria.DataExame;
                        newRow["tNO_EXAME_TIPO"] = audiometria.GetAudiometriaTipo().Substring(0, 1); ;
                        newRow["tNATUREZA"] = "Audiometria OD";
                        if (audiometria.IndAudiometriaTipo == (int)AudiometriaTipo.Admissional)
                            newRow["tEXAME_RS"] = "R";
                        else
                            newRow["tEXAME_RS"] = "S";
                        newRow["tRESULTADO"] = "Vide Observa��es abaixo";
                        ds.Tables[0].Rows.Add(newRow);
                        newRow = ds.Tables[0].NewRow();
                        newRow["nID_EMPREGADO"] = empregado.Id;
                        newRow["dDT_EXAME"] = audiometria.DataExame;
                        newRow["tNO_EXAME_TIPO"] = audiometria.GetAudiometriaTipo().Substring(0, 1);
                        newRow["tNATUREZA"] = "Audiometria OE";
                        if (audiometria.IndAudiometriaTipo == (int)AudiometriaTipo.Admissional)
                            newRow["tEXAME_RS"] = "R";
                        else
                            newRow["tEXAME_RS"] = "S";
                        newRow["tRESULTADO"] = "Vide Observa��es abaixo";
                        ds.Tables[0].Rows.Add(newRow);
                    }
                }
                if (ds.Tables[0].Rows.Count == 0)
                {
                    newRow["nID_EMPREGADO"] = empregado.Id;
                    newRow["tRESULTADO"] = "Vide Observa��es abaixo";
                    ds.Tables[0].Rows.Add(newRow);
                }
            }
            return ds;
        }

        private static DataTable GetTableExames()
        {
            DataTable table = new DataTable("Result");
            table.Columns.Add("nID_EMPREGADO", Type.GetType("System.Int32"));
            table.Columns.Add("dDT_EXAME", Type.GetType("System.DateTime"));
            table.Columns.Add("tNO_EXAME_TIPO", Type.GetType("System.String"));
            table.Columns.Add("tNATUREZA", Type.GetType("System.String"));
            table.Columns.Add("tEXAME_RS", Type.GetType("System.String"));
            table.Columns.Add("tRESULTADO", Type.GetType("System.String"));
            return table;
        }

        #endregion

        #region Metodos

        #region GetEmpregadoFuncao

        private List<EmpregadoFuncao> GetEmpregadoFuncao()
        {
            if (listEmpregadoFuncao == null)
            {
                StringBuilder str = new StringBuilder();

                str.Append("nID_EMPREGADO=" + empregado.Id);

                //if (!ViaNetPdt)
                //{
                //    str.Append(" AND nID_EMPREGADO_FUNCAO IN (SELECT nID_EMPREGADO_FUNCAO FROM tblFUNC_EMPREGADO WHERE nID_LAUD_TEC");
                //    str.Append(" IN (SELECT nID_LAUD_TEC FROM tblLAUDO_TEC WHERE nID_PEDIDO");
                //    str.Append(" IN (SELECT IdPedido FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.Pedido WHERE DataConclusao IS NOT NULL) OR bAE=1))");
                //}

                if (SomentePeriodoAposDataInicioPPP)
                    str.Append(" AND (hDT_TERMINO>='" + dataInicioPPP.ToString("yyyy-MM-dd") + "' OR hDT_TERMINO IS NULL)");

                //PPP eletr�nico - n�o pegar classif.funcionais ap�s 31/12/2022
                str.Append(" AND (hDT_INICIO<'2023-01-01')");

                str.Append(" ORDER BY hDT_INICIO");

                listEmpregadoFuncao = new EmpregadoFuncao().Find<EmpregadoFuncao>(str.ToString());

                if (listEmpregadoFuncao.Count == 0)
                    throw new Exception("O empregado deve possuir pelo menos 1 Classifica��o Funcional e pertencer a um GHE!" + System.Environment.NewLine + "Para per�odos trabalhados a partir de 01/01/2023, o PPP dever� ser emitido exclusivamente em meio eletr�nico.");
            }

            return listEmpregadoFuncao;
        }
        #endregion

        #region GetLaudoTecnicos

        private List<LaudoTecnico> GetLaudoTecnicos(EmpregadoFuncao emprFuncao)
        {
            StringBuilder str = new StringBuilder();

            if (SomentePeriodoAposDataInicioPPP)
                str.Append("DATEADD(year, 1, hDT_LAUDO)>='" + dataInicioPPP.ToString("yyyy-MM-dd") + "' AND ");

            str.Append(" DATEPART(yyyy, hDT_LAUDO)< 2023 AND ");

            str.Append("nID_LAUD_TEC IN (SELECT nID_LAUD_TEC FROM tblFUNC_EMPREGADO WHERE nID_EMPREGADO_FUNCAO=" + emprFuncao.Id + ")");

            //if (!ViaNetPdt)
            //    str.Append(" AND (nID_PEDIDO IN (SELECT IdPedido FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.Pedido WHERE DataConclusao IS NOT NULL) OR bAE=1)");

            str.Append(" ORDER BY hDT_LAUDO");

            List<LaudoTecnico> laudos = new LaudoTecnico().Find<LaudoTecnico>(str.ToString());

            List<LaudoTecnico> listLaudos = new List<LaudoTecnico>();

            foreach (LaudoTecnico laudo in laudos)
            {
                if (SomentePeriodoAposDataInicioPPP)
                {
                    if (laudo.GetDataTermino(laudos) >= dataInicioPPP)
                        listLaudos.Add(laudo);
                }
                else
                    listLaudos.Add(laudo);
            }

            return listLaudos;
        }
        #endregion

        #region GetPeriodoExposicaoPPP

        public string GetPeriodoExposicaoPPP(EmpregadoFuncao empregadoFuncao,
                                                    LaudoTecnico laudoTecnico)
        {
            DateTime dataInicio;
            DateTime dataTermino;

            dataInicio = laudoTecnico.hDT_LAUDO;
            dataTermino = laudoTecnico.GetDataTermino();

            System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");      

            if (dataTermino > empregadoFuncao.hDT_TERMINO
                && empregadoFuncao.hDT_TERMINO != new DateTime())
                dataTermino = empregadoFuncao.hDT_TERMINO;

            if (dataTermino >= DateTime.Today
                && empregadoFuncao.nID_EMPREGADO.hDT_DEM == new DateTime()
                && empregadoFuncao.hDT_TERMINO == new DateTime())
                dataTermino = new DateTime();

            StringBuilder periodo = new StringBuilder();

            periodo.Append(dataInicio.ToString("dd-MM-yyyy", ptBr));
            periodo.Append("\n a \n");

            if (dataTermino == new DateTime())
                periodo.Append("__-__-____");
            else
            {
                if (dataTermino.Year >= 2023)
                {
                    periodo.Append("31-12-2022");
                }
                else
                {
                    periodo.Append(dataTermino.ToString("dd-MM-yyyy"));
                }
            }


            return periodo.ToString();
        }
        #endregion

        #region GetPeriodoExposicaoPPP

        public string GetPeriodoExposicaoPPP(EmpregadoFuncao empregadoFuncao,
                                            LaudoTecnico laudoTecnico,
                                            List<LaudoTecnico> sortedLaudos)
        {
            DateTime dataInicio = new DateTime();
            DateTime dataTermino = new DateTime();
            System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");      

            if (SomentePeriodoAposDataInicioPPP)
            {
                if (laudoTecnico.hDT_LAUDO < dataInicioPPP)
                {
                    if (empregado.hDT_ADM < dataInicioPPP)
                        dataInicio = dataInicioPPP;
                    else if (empregado.hDT_ADM >= dataInicioPPP)
                        dataInicio = empregado.hDT_ADM;
                }
                else if (laudoTecnico.hDT_LAUDO >= dataInicioPPP)
                {
                    dataInicio = laudoTecnico.hDT_LAUDO;

                    if (laudoTecnico.Id == ((LaudoTecnico)sortedLaudos[0]).Id
                        && empregadoFuncao.hDT_INICIO >= dataInicioPPP)
                        dataInicio = empregadoFuncao.hDT_INICIO;
                }
            }
            else
            {
                if (laudoTecnico.Id == ((LaudoTecnico)sortedLaudos[0]).Id)
                    dataInicio = empregadoFuncao.hDT_INICIO;
                else
                    dataInicio = laudoTecnico.hDT_LAUDO;
            }

            dataTermino = laudoTecnico.GetDataTermino(sortedLaudos);

            if (dataTermino > empregadoFuncao.hDT_TERMINO && empregadoFuncao.hDT_TERMINO != new DateTime())
                dataTermino = empregadoFuncao.hDT_TERMINO;

            if (dataTermino >= DateTime.Today
                && empregadoFuncao.nID_EMPREGADO.hDT_DEM == new DateTime()
                && empregadoFuncao.hDT_TERMINO == new DateTime())
                dataTermino = new DateTime();

            StringBuilder periodo = new StringBuilder();

            periodo.Append(dataInicio.ToString("dd-MM-yyyy", ptBr));
            periodo.Append("\n a \n");

            if (dataTermino == new DateTime())
                periodo.Append("__-__-____");
            else
            {
                if (dataTermino.Year >= 2023)
                {
                    periodo.Append("31-12-2022");
                }
                else
                {
                    periodo.Append(dataTermino.ToString("dd-MM-yyyy"));
                }
            }


            return periodo.ToString();
        }
        #endregion

        #endregion

        #endregion


    }

}
