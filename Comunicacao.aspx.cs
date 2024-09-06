using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Ilitera.Opsa.Data;
using Ilitera.Common;
using Ilitera.Sied.Report;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Ilitera.PCMSO.Report;
using System.Text;
//using Ilitera.Net.Documentos;

using System.IO;
using System.Net;
using System.Xml;
using System.Xml.Linq;

using System.Configuration;


namespace Ilitera.Net
{
    /// <summary>
    /// 
    /// </summary>
    public partial class Comunicacao : System.Web.UI.Page
    {



        protected void Page_Load(object sender, System.EventArgs e)
        {
            // InicializaWebPageObjects();

            string xSolicitacao = "";

            Ilitera.Data.SQLServer.EntitySQLServer.xDB1 = ConfigurationManager.AppSettings["DB1"].ToString();
            Ilitera.Data.SQLServer.EntitySQLServer.xDB2 = ConfigurationManager.AppSettings["DB2"].ToString();
            Ilitera.Data.SQLServer.EntitySQLServer._LocalServer = ConfigurationManager.AppSettings["LocalServer"].ToString();

            
            try
            {
                txt_Status.Text = Request["Status"].ToString();
            }
            catch
            {
                txt_Status.Text = "";
            }

            if (txt_Status.Text.Trim() != "") return;




         
            Page.Response.ContentType = "text/xml";
            // Read XML posted via HTTP
            StreamReader reader = new StreamReader(Page.Request.InputStream);
            String xmlData = reader.ReadToEnd();

            if (xmlData == "")
            {
                xmlData = Session["XML"].ToString();
            }


            if (xmlData == "")
            {
                return;
            }

            XDocument doc = XDocument.Parse(xmlData);

            var points = doc.Descendants("Tipo");



            foreach (XElement current in points)
            {
                xSolicitacao = current.Element("Solicitacao").Value;
            }



            if (xSolicitacao == "Exame")
            {

                Executar_Exame(xmlData);

            }
            else if (xSolicitacao == "Afastamento")
            {

                Executar_Afastamento(xmlData);

            }
            else if (xSolicitacao == "Acidente")
            {

                Executar_Acidente(xmlData);

            }
            else if (xSolicitacao == "Convocacao")
            {

                Executar_Convocacao(xmlData);

            }
            else if (xSolicitacao == "CIPA")
            {

                Executar_CIPA(xmlData);

            }
            else if (xSolicitacao == "CIPA_EVENTO")
            {

                Executar_CIPA_Evento(xmlData);

            }

        }




        private void Executar_Exame(string xmlData)
        {


            ClinicaCliente rClinica = new ClinicaCliente();
            Empregado rEmpregado = new Empregado();
            Ilitera.Common.Pessoa rEmpresa = new Ilitera.Common.Pessoa();
            int zId = 0;

            string xCNPJ = "";
            string xEmpresa = "";
            string xCodUsuario = "";           
            string xTipoExame = "";
            string xColaborador = "";
            string xData = "";
            string xID = "";

            string xNIT = "";
            string xRG = "";
            string xCPF = "";
            string xDtAdmissao = "";
            string xDtNascimento = "";
            string xCTPS_Numero = "";
            string xCTPS_Serie = "";
            string xCTPS_UF = "";
            string xEmail = "";
            string xMatricula = "";
            string xSexo = "";
            string xFuncao = "";
            string xCargo = "";
            string xSetor = "";

            string rSelect = "";


            try
            {


                XDocument doc = XDocument.Parse(xmlData);
                

                var points = doc.Descendants("Tipo");

                foreach (XElement current in points)
                {
                    xEmpresa = current.Element("Empresa").Value;
                    xCNPJ = current.Element("CNPJ").Value;
                    xCodUsuario = current.Element("CodUsuario").Value;
                    xTipoExame = current.Element("TipoExame").Value;
                    xColaborador = current.Element("Colaborador").Value;
                    xData = current.Element("Data").Value;
                    xID = current.Element("ID").Value;
                }


                rSelect = " NomeAbreviado = '" + xEmpresa + "' and dbo.udf_getnumeric( nomecodigo ) = '" + xCNPJ + "' ";

                //pegar Id Empresa                    
                rEmpresa.Find(rSelect);

                //se não achar empresa,  emitir retorno avisando
                if (rEmpresa.Id == 0)
                {
                    txt_Status.Text = txt_Status.Text + "Erro: Empresa não localizada ( " + xEmpresa + "  )" + System.Environment.NewLine;
                }

                //quando for convocação ( mailing ) no último irá o link para chamar comunicação
                //pode ir com um ID pronto



                if (xTipoExame.ToUpper().Trim() != "ADMISSIONAL")
                {


                    //pegar Id Colaborador                       
                    rEmpregado.Find(" tNo_Empg='" + xColaborador + "'  and nId_Empr = " + rEmpresa.Id.ToString());

                    //se não achar empregado,  emitir retorno avisando
                    if (rEmpregado.Id == 0)
                    {
                        txt_Status.Text = txt_Status.Text + "Erro: Empregado não localizado ( " + xColaborador + "  )" + System.Environment.NewLine;
                    }


                    rClinica.Find(" IdCliente = " + rEmpresa.Id.ToString() + " and convert( char(12), IdCliente ) + ' ' + convert( char(12), IdClinica ) in ( select top 1 convert( char(12), IdCliente ) + ' ' + convert( char(12), IdClinica ) from ClinicaCliente where IdCliente = " + rEmpresa.Id.ToString() + " ) ");


                    //se não achar clinica,  emitir retorno avisando
                    if (rClinica.Id == 0)
                    {
                        txt_Status.Text = txt_Status.Text + "Erro: Clínica não localizada " + System.Environment.NewLine;
                    }


                    //pegar IdExame
                    if (xTipoExame.ToUpper().Trim() == "PERIODICO" || xTipoExame.ToUpper().Trim() == "PERIÓDICO")
                        xTipoExame = "4";
                    else if (xTipoExame.ToUpper().Trim() == "DEMISSIONAL")
                        xTipoExame = "2";
                    else if (xTipoExame.ToUpper().Trim().Substring(0, 5) == "MUDAN")
                        xTipoExame = "3";
                    else if (xTipoExame.ToUpper().Trim().Substring(0, 7) == "RETORNO")
                        xTipoExame = "5";


                    zId = rEmpregado.Id;

                    //if (txt_Status.Text == "")
                    //{
                    //    rClinica.IdClinica.Find();

                    //    Entities.Usuario user = (Entities.Usuario)Session["usuarioLogado"];
                    //    Response.Redirect("~\\PCMSO\\DadosEmpregado_Guia_Auto.aspx?IdEmpregado=" + rEmpregado.Id.ToString().Trim() + "&IdEmpresa=" + rEmpresa.Id.ToString().Trim() + "&IdUsuario=" + xCodUsuario + "&TipoExame=" + xTipoExame + "&Data=" + xData + "&IdClinica=" + rClinica.IdClinica.Id.ToString().Trim() + "&CodUsuario=" + xCodUsuario + "&xId=" + xID);
                    //}
                    //else
                    //{
                    //    Session["zErro"] = txt_Status.Text;
                    //    Response.Redirect("~/Comunicacao2.aspx");
                    //}

                }


                else  //Admissional
                {

                    if (txt_Status.Text == "")
                    {


                        foreach (XElement current in points)
                        {
                            xNIT = current.Element("NIT").Value;
                            xRG = current.Element("RG").Value;
                            xCPF = current.Element("CPF").Value;
                            xDtAdmissao = current.Element("DtAdmissao").Value;
                            xDtNascimento = current.Element("DtNascimento").Value;
                            xCTPS_Numero = current.Element("CTPS_Numero").Value;
                            xCTPS_Serie = current.Element("CTPS_Serie").Value;
                            xCTPS_UF = current.Element("CTPS_UF").Value;
                            xEmail = current.Element("Email").Value;
                            xMatricula = current.Element("Matricula").Value;
                            xSexo = current.Element("Sexo").Value;
                            xFuncao = current.Element("Funcao").Value;
                            xCargo = current.Element("Cargo").Value;
                            xSetor = current.Element("Setor").Value;
                        }


                        //Se colaborador já existir, cancelar                        
                        rEmpregado.Find(" tno_CPF='" + xCPF + "'  and nId_Empr = " + rEmpresa.Id.ToString());

                        
                        if (rEmpregado.Id != 0)
                        {
                            txt_Status.Text = txt_Status.Text + "Erro: Empregado já cadastrado ( " + xColaborador + "  )" + System.Environment.NewLine;
                        }


                        if ( txt_Status.Text == "" )
                        { 

                        //criar registro do colaborador
                        Ilitera.Data.Empregado_Cadastral xEmpregado = new Ilitera.Data.Empregado_Cadastral();
                        zId = xEmpregado.Inserir_Dados_Empregado(rEmpresa.Id.ToString().Trim(), xColaborador, xSexo.Substring(0, 1), xCTPS_Numero, xCTPS_Serie, xCTPS_UF, xMatricula, xRG, xDtAdmissao, "", xDtNascimento, xNIT, xCPF, "", System.Convert.ToInt32(xCodUsuario), "", xEmail);

                        //criar classif.funcional
                        xEmpregado.Inserir_Classificacao_Funcional(zId, rEmpresa.Id, xDtAdmissao, "", xCargo, xSetor, xFuncao, System.Convert.ToInt32(xCodUsuario), "");


                        //alocar GHE
                        EmpregadoFuncao xEmprFunc = new EmpregadoFuncao();
                        xEmprFunc.Find(" nId_Empregado = " + zId.ToString());

                        int colId = xEmprFunc.Id;

                        LaudoTecnico nLaudo = new LaudoTecnico();
                        nLaudo.Find(" nId_Empr = " + rEmpresa.Id.ToString() + " and convert( char(12), nId_Empr ) + ' ' + convert( char(10), hDt_Laudo, 103 ) in ( select convert( char(12), nId_Empr ) + ' ' + convert( char(10), max(hDt_Laudo), 103 ) from tblLaudo_Tec group by nId_Empr ) ");

                        Ghe nGHE = new Ghe();
                        nGHE.Find(" nId_Laud_Tec = " + nLaudo.Id.ToString() + " and tNO_FUNC = '" + xSetor + "' ");


                        Int32 zLaudo = nLaudo.Id;
                        Int32 zGHE = nGHE.Id;


                            if (colId != 0 && zLaudo != 0 && zGHE != 0)
                            {
                                GheEmpregado gheEmpregado = new GheEmpregado();
                                gheEmpregado.Find("nID_FUNC=" + zGHE + " AND nID_EMPREGADO_FUNCAO=" + colId);
                                if (gheEmpregado.Id == 0)
                                {
                                    gheEmpregado.Inicialize();
                                    gheEmpregado.nID_LAUD_TEC.Id = zLaudo;
                                    gheEmpregado.nID_EMPREGADO_FUNCAO.Id = colId;
                                }
                                gheEmpregado.nID_FUNC.Id = zGHE;
                                gheEmpregado.Save();



                                //criar exame
                                xTipoExame = "1";

                                rClinica.Find(" IdCliente = " + rEmpresa.Id.ToString() + " and convert( char(12), IdCliente ) + ' ' + convert( char(12), IdClinica ) in ( select top 1 convert( char(12), IdCliente ) + ' ' + convert( char(12), IdClinica ) from ClinicaCliente where IdCliente = " + rEmpresa.Id.ToString() + " ) ");

                            }
                        }

                    }

                }




            }
            catch (Exception ex)
            {
                txt_Status.Text = txt_Status.Text + "Erro: Carga do XML: " + ex.Message + System.Environment.NewLine;

                Session["zErro"] = txt_Status.Text;
                Response.Redirect("~/Comunicacao2.aspx");


            }
            finally
            {

                if (txt_Status.Text != "")
                {
                    Session["zErro"] = txt_Status.Text;
                    Response.Redirect("~/Comunicacao2.aspx");
                }
                else
                {
                    rClinica.IdClinica.Find();

                    Entities.Usuario user = (Entities.Usuario)Session["usuarioLogado"];
                    Response.Redirect("~\\PCMSO\\DadosEmpregado_Guia_Auto.aspx?IdEmpregado=" + zId.ToString().Trim() + "&IdEmpresa=" + rEmpresa.Id.ToString().Trim() + "&IdUsuario=" + xCodUsuario + "&TipoExame=" + xTipoExame + "&Data=" + xData + "&IdClinica=" + rClinica.IdClinica.Id.ToString().Trim() + "&CodUsuario=" + xCodUsuario + "&xId=" + xID);
                                       

                    Session["zErro"] = txt_Status.Text + " " +  "Processamento Finalizado";
                    Response.Redirect("~/Comunicacao2.aspx");
                }
            }


        }



        private void Executar_Afastamento(string xmlData)
        {

                        
            Empregado rEmpregado = new Empregado();
            Ilitera.Common.Pessoa rEmpresa = new Ilitera.Common.Pessoa();
            //int zId = 0;

            string xID = "";
            string xCNPJ = "";
            string xEmpresa = "";
            string xCodUsuario = "";
            string xColaborador = "";

            string xDataInicio = "";
            string xHoraInicio = "";
            string xPrevisaoRetorno = "";
            string xDataRetorno = "";
            string xHoraRetorno = "";

            string xObservacao = "";
            string xTipoAfastamento = "";
            string xEmitente_Atestado = "";
            string xResponsavel_Atestado = "";
            string xNrConselho_Atestado = "";
            string xUFConselho_Atestado = "";

            string xCID1 = "";
            string xCID2 = "";
            string xCID3 = "";
            string xCID4 = "";

            string xArquivo = "";
            string xConteudo_Arquivo = "";

            string rSelect = "";


            try
            {


                XDocument doc = XDocument.Parse(xmlData);


                var points = doc.Descendants("Tipo");

                foreach (XElement current in points)
                {
                    xEmpresa = current.Element("Empresa").Value;
                    xCNPJ = current.Element("CNPJ").Value;
                    xCodUsuario = current.Element("CodUsuario").Value;
                    xColaborador = current.Element("Colaborador").Value;
                    xID = current.Element("ID").Value;

                    xDataInicio = current.Element("DataInicio").Value;
                    xHoraInicio = current.Element("HoraInicio").Value;
                    xPrevisaoRetorno = current.Element("PrevisaoRetorno").Value;
                    xDataRetorno = current.Element("DataRetorno").Value;
                    xHoraRetorno = current.Element("HoraRetorno").Value;

                    xObservacao = current.Element("Observacao").Value;
                    xTipoAfastamento = current.Element("TipoAfastamento").Value;
                    xEmitente_Atestado = current.Element("Emitente_Atestado").Value;
                    xResponsavel_Atestado = current.Element("Responsavel_Atestado").Value;
                    xNrConselho_Atestado = current.Element("NrConselho_Atestado").Value;
                    xUFConselho_Atestado = current.Element("UFConselho_Atestado").Value;

                    xCID1 = current.Element("CID1").Value;
                    xCID2 = current.Element("CID2").Value;
                    xCID3 = current.Element("CID3").Value;
                    xCID4 = current.Element("CID4").Value;

                    xArquivo = current.Element("Arquivo").Value;
                    xConteudo_Arquivo = current.Element("Conteudo_Arquivo").Value;

                }



                System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");
                

                rSelect = " NomeAbreviado = '" + xEmpresa + "' and dbo.udf_getnumeric( nomecodigo ) = '" + xCNPJ + "' ";

                //pegar Id Empresa                    
                rEmpresa.Find(rSelect);

                //se não achar empresa,  emitir retorno avisando
                if (rEmpresa.Id == 0)
                {
                    txt_Status.Text = txt_Status.Text + "Erro: Empresa não localizada ( " + xEmpresa + "  )" + System.Environment.NewLine;
                }



                rEmpregado.Find(" tNo_Empg='" + xColaborador + "'  and nId_Empr = " + rEmpresa.Id.ToString());


                if (txt_Status.Text == "")
                {
                    //se não achar empregado,  emitir retorno avisando
                    if (rEmpregado.Id == 0)
                    {
                        txt_Status.Text = txt_Status.Text + "Erro: Empregado não localizado ( " + xColaborador + "  )" + System.Environment.NewLine;
                    }
                }




                //verificar se já existe registro de afastamento - pode ser apenas atualização da Data e Hora Retorno
                //do absenteísmo - nesse caso apenas atualizar o registro

                //  UPDATE Data e Hora Retorno
                Afastamento rAfastamento = new Afastamento();
                rAfastamento.Find(" IdEmpregado = " + rEmpregado.Id.ToString() + " and convert( char(10), DataInicial, 103 ) = '" + xDataInicio + "' ");

                if ( rAfastamento.Id != 0)
                {
                    //se tiver dataretorno,  dar update e finalizar processamento
                    if ( rAfastamento.DataVolta == new DateTime() && xDataRetorno != "")
                    {
                        rAfastamento.DataVolta = System.Convert.ToDateTime(xDataRetorno + " " + xHoraRetorno, ptBr);
                        rAfastamento.Save();
                        
                        txt_Status.Text = "Data de Retorno deste Afastamento salva para o colaborador " + xColaborador + " .";
                    }
                    else
                    {
                        txt_Status.Text = "Afastamento já existe para o colaborador " + xColaborador + " .";
                    }

                }


                







                if ( txt_Status.Text == "")
                {

                    Afastamento afastamento = new Afastamento();                    

                    afastamento.IdEmpregado = rEmpregado;
                    afastamento.DataInicial = System.Convert.ToDateTime(xDataInicio + ' ' + xHoraInicio, ptBr);

                    if (xPrevisaoRetorno != "")
                        afastamento.DataPrevista = System.Convert.ToDateTime(xPrevisaoRetorno, ptBr);
                    else
                        afastamento.DataPrevista = new DateTime();


                    if (xDataRetorno != "")
                        afastamento.DataVolta = System.Convert.ToDateTime(xDataRetorno + ' ' + xHoraRetorno, ptBr);
                    else
                        afastamento.DataVolta = new DateTime();


                    Acidente zAcidente = new Acidente();
                    afastamento.IdAcidente = zAcidente;


                    //validação
                    bool xEnvio_Alerta = false;

                    if (afastamento.DataVolta == null || afastamento.DataVolta.Year == 1)
                    {
                        if (afastamento.DataInicial.AddDays(15) < afastamento.DataPrevista)
                        {
                            xEnvio_Alerta = true;
                        }
                    }
                    else
                    {
                        if (afastamento.DataInicial.AddDays(15) < afastamento.DataVolta)
                        {
                            xEnvio_Alerta = true;
                        }
                    }


                    if (xEnvio_Alerta == false)
                    //checar se há atetados com mais de 15 dias afastados nos ultimos 60 dias
                    {                       

                        Ilitera.Data.Clientes_Funcionarios xAbs = new Ilitera.Data.Clientes_Funcionarios();
                        DataSet rDs = xAbs.Checar_Absenteismo_Colaborador(rEmpregado.Id, afastamento.DataInicial.ToString("dd/MM/yyyy", ptBr));

                        if (rDs.Tables[0].Rows.Count > 0)
                        {
                            if (rDs.Tables[0].Rows[0][0].ToString().Trim() != "")
                            {
                                if (System.Convert.ToInt16(rDs.Tables[0].Rows[0][0].ToString()) > 15)
                                {
                                    xEnvio_Alerta = true;
                                }
                            }
                        }
                    }


                    if (xEnvio_Alerta == true)
                    {
                        txt_Status.Text = "O colaborador " + xColaborador + " possui afastamentos anteriores com pelo menos 15 dias corridos ou com total de dias afastados de pelo menos 15 dias nos últimos 60 dias. Favor verificar se os atestados possuem a mesma patologia para possível encaminhamento ao INSS.";
                    }




                    if ( txt_Status.Text == "" &&  xCID1 != "")
                    {
                        CID rCID = new CID();
                        rCID.Find(" CodigoCID = '" + xCID1 + "'  ");
                        
                        if ( rCID.Id == 0)
                        {
                            txt_Status.Text = "Código CID fornecido Inválido: " + xCID1;
                        }
                        else
                        {
                            afastamento.IdCID = rCID;
                        }
                    }

                    if ( txt_Status.Text == "" &&  xCID2 != "")
                    {
                        CID rCID = new CID();
                        rCID.Find(" CodigoCID = '" + xCID2 + "'  ");

                        if (rCID.Id == 0)
                        {
                            txt_Status.Text = "Código CID fornecido Inválido: " + xCID2;
                        }
                        else
                        {
                            afastamento.IdCID2 = rCID.Id;
                        }
                    }

                    if (txt_Status.Text == "" && xCID3 != "")
                    {
                        CID rCID = new CID();
                        rCID.Find(" CodigoCID = '" + xCID3 + "'  ");

                        if (rCID.Id == 0)
                        {
                            txt_Status.Text = "Código CID fornecido Inválido: " + xCID3;
                        }
                        else
                        {
                            afastamento.IdCID3 = rCID.Id;
                        }
                    }

                    if (txt_Status.Text == "" && xCID4 != "")
                    {
                        CID rCID = new CID();
                        rCID.Find(" CodigoCID = '" + xCID4 + "'  ");

                        if (rCID.Id == 0)
                        {
                            txt_Status.Text = "Código CID fornecido Inválido: " + xCID4;
                        }
                        else
                        {
                            afastamento.IdCID4 = rCID.Id;
                        }
                    }

                    

                    if (txt_Status.Text == "")
                    {

                        afastamento.Obs = xObservacao.Trim();

                        //xEmitente_Atestado = current.Element("Emitente_Atestado").Value;
                        //xResponsavel_Atestado = current.Element("Responsavel_Atestado").Value;
                        //xNrConselho_Atestado = current.Element("NrConselho_Atestado").Value;
                        //xUFConselho_Atestado = current.Element("UFConselho_Atestado").Value;


                        if (xTipoAfastamento.ToUpper() == "OCUPACIONAL")
                        {
                            afastamento.IndTipoAfastamento = 1;
                        }
                        else
                        {
                            afastamento.IndTipoAfastamento = 2;
                        }


                        AfastamentoTipo xAT = new AfastamentoTipo();
                        xAT.Find(101);                        
                        afastamento.IdAfastamentoTipo = xAT;

                        afastamento.UsuarioId = System.Convert.ToInt32(xCodUsuario);

                        afastamento.Save();

                    }
                    

                    if (txt_Status.Text == "" && xConteudo_Arquivo != "")
                    {
                        //xArquivo terá a extensão
                        
                        byte[] arrBytes = Convert.FromBase64String(xConteudo_Arquivo);

                        //montar com diretório padrão da empresa, nome do colaborador, data e extensão - chave Arquivo
                        Cliente xCliente = new Cliente();
                        xCliente.Find(System.Convert.ToInt32(rEmpresa.Id));

                        string xArq = "";

                        //if (Session["Servidor_Web"].ToString().Trim().ToUpper() == "ILITERA")
                        xArq = "Atestado_" + xColaborador.Replace(" ", "_") + "_" + xDataInicio.Replace("/", "") + "." + xArquivo;


                        string uri = "ftp://54.94.157.244:21/" + xCliente.DiretorioPadrao.ToString().Trim().Replace(" ", "%20") + "/Prontuario/" + xArq;

                        //string uri = "ftp://54.94.157.244:21/5A%20GT/Prontuario/teste_xml.pdf";



                        FtpWebRequest request = (FtpWebRequest)WebRequest.Create(new Uri(uri));
                        request.Method = WebRequestMethods.Ftp.UploadFile;

                        request.Credentials = new NetworkCredential("lmm", "Lm160521");

                        request.UseBinary = true;
                        request.UsePassive = true;
                        request.ContentLength = arrBytes.Length;
                        Stream stream = request.GetRequestStream();
                        stream.Write(arrBytes, 0, arrBytes.Length);
                        stream.Close();
                        FtpWebResponse res = (FtpWebResponse)request.GetResponse();

                        afastamento.Atestado = @"I:\FotosDocsDigitais\" + xCliente.DiretorioPadrao.ToString().Trim() + "/Prontuario/" + xArq;

                        //salvar dados do atestado nos campos do afastamento
                        afastamento.Atestado_Emitente = xResponsavel_Atestado;

                        if (xEmitente_Atestado.Trim().ToUpper() == "CRM")
                           afastamento.Atestado_ideOC = "1";
                        else
                            afastamento.Atestado_ideOC = "2";

                        afastamento.Atestado_nrOC = xNrConselho_Atestado;
                        afastamento.Atestado_ufOC = xUFConselho_Atestado;

                        afastamento.Save();
                    }


                }


            }
            catch (Exception ex)
            {
                txt_Status.Text = txt_Status.Text + "Erro: Carga do XML: " + ex.Message + System.Environment.NewLine;

                Session["zErro"] = txt_Status.Text;
                Response.Redirect("~/Comunicacao2.aspx");


            }
            finally
            {

                if (txt_Status.Text != "")
                {
                    Session["zErro"] = txt_Status.Text;
                    Response.Redirect("~/Comunicacao2.aspx");
                }
                else
                {
                   
                   // Entities.Usuario user = (Entities.Usuario)Session["usuarioLogado"];
                   // Response.Redirect("~\\PCMSO\\CadAbsentismo_Auto.aspx?IdEmpregado=" + zId.ToString().Trim() + "&IdEmpresa=" + rEmpresa.Id.ToString().Trim() + "&IdUsuario=" + xCodUsuario + "&Data=" + xData + "&CodUsuario=" + xCodUsuario + "&xId=" + xID);


                    // 'CadAbsentismo.aspx?IdEmpregado=" + Session["Empregado"].ToString() + "&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + xUser.IdUsuario.ToString() + "&IdAcidente=0'

                    Session["zErro"] = txt_Status.Text + " " + "Processamento Finalizado";
                    Response.Redirect("~/Comunicacao2.aspx");
                }
            }


        }



        private void Executar_Acidente(string xmlData)
        {


            ClinicaCliente rClinica = new ClinicaCliente();
            Empregado rEmpregado = new Empregado();
            Ilitera.Common.Pessoa rEmpresa = new Ilitera.Common.Pessoa();
            //int zId = 0;

            string xID = "";
            string xCNPJ = "";
            string xEmpresa = "";
            string xCodUsuario = "";
            string xColaborador = "";

            string xDataAcidente = "";
            string xHoraAcidente = "";
            string xTipoAcidente = "";
            string xSituacaoGeradora = "";
            string xParteCorpoAtingida = "";
            string xLateralidade = "";
            string xAgenteCausador = "";
            string xDescricaoLesao = "";
            string xDescricaoCompl = "";
            string xLocalAcidente = "";
            string xEnderecoLocal = "";
            string xNumeroLocal = "";
            string xMunicipioLocal = "";
            string xUFLocal = "";
            string xUnidadeLocal = "";
            string xEspecificacaoLocal = "";
            string xSetorAcidente = "";

            string xInternacao = "";
            string xDataAtendimento = "";
            string xHoraAtendimento = "";
            string xDuracaoTratamentoDias = "";
            string xCodigoCNES = "";
            string xReponsavel_Atestado = "";
            string xNrConselho_Atestado = "";
            string xUFConselho_Atestado = "";
            string xDiagnosticoProvavel = "";
            string xCID1 = "";
            string xCID2 = "";
            string xCID3 = "";
            string xCID4 = "";

            string xTransferidoOutroSetor = "";
            string xAposentadoInvalidez = "";
            string xPerdaMaterial = "";

            string xEmitenteCAT = "";
            string xDataEmissaoCAT = "";
            string xHoraEmissaoCAT = "";
            string xTipoCAT = "";
            string xNumeroCAT = "";
            string xIniciativa = "";
            string xBOPolicial = "";
            string xDataObito = "";

            string xDataInicioAbsenteismo = "";
            string xHoraInicioAbsenteismo = "";
            string xPrevisaoRetornoAbsenteismo = "";
            string xDataRetornoAbsenteismo = "";
            string xHoraRetornoAbsenteismo = "";
            string xObservacaoAbsenteismo = "";

            string rSelect = "";


            try
            {


                XDocument doc = XDocument.Parse(xmlData);


                var points = doc.Descendants("Tipo");

                foreach (XElement current in points)
                {
                    xEmpresa = current.Element("Empresa").Value;
                    xCNPJ = current.Element("CNPJ").Value;
                    xCodUsuario = current.Element("CodUsuario").Value;
                    xColaborador = current.Element("Colaborador").Value;
                    xID = current.Element("ID").Value;

                    xDataAcidente = current.Element("DataAcidente").Value;
                    xHoraAcidente = current.Element("HoraAcidente").Value;
                    xTipoAcidente = current.Element("TipoAcidente").Value;
                    xSituacaoGeradora = current.Element("SituacaoGeradora").Value;
                    xParteCorpoAtingida = current.Element("ParteCorpoAtingida").Value;
                    xLateralidade = current.Element("Lateralidade").Value;
                    xAgenteCausador = current.Element("AgenteCausador").Value;
                    xDescricaoLesao = current.Element("DescricaoLesao").Value;
                    xDescricaoCompl = current.Element("DescricaoCompl").Value;
                    xLocalAcidente = current.Element("LocalAcidente").Value;
                    xEnderecoLocal = current.Element("EnderecoLocal").Value;
                    xNumeroLocal = current.Element("NumeroLocal").Value;
                    xMunicipioLocal = current.Element("MunicipioLocal").Value;
                    xUFLocal = current.Element("UFLocal").Value;
                    xUnidadeLocal = current.Element("UnidadeLocal").Value;
                    xEspecificacaoLocal = current.Element("EspecificacaoLocal").Value;
                    xSetorAcidente = current.Element("SetorAcidente").Value;

                    xInternacao = current.Element("Internacao").Value;
                    xDataAtendimento = current.Element("DataAtendimento").Value;
                    xHoraAtendimento = current.Element("HoraAtendimento").Value;
                    xDuracaoTratamentoDias = current.Element("DuracaoTratamentoDias").Value;
                    xCodigoCNES = current.Element("CodigoCNES").Value;
                    xReponsavel_Atestado = current.Element("Responsavel_Atestado").Value;
                    xNrConselho_Atestado = current.Element("NrConselho_Atestado").Value;
                    xUFConselho_Atestado = current.Element("UFConselho_Atestado").Value;
                    xDiagnosticoProvavel = current.Element("DiagnosticoProvavel").Value;

                    xCID1 = current.Element("CID1").Value;
                    xCID2 = current.Element("CID2").Value;
                    xCID3 = current.Element("CID3").Value;
                    xCID4 = current.Element("CID4").Value;


                    xTransferidoOutroSetor = current.Element("TransferidoOutroSetor").Value;
                    xAposentadoInvalidez = current.Element("AposentadoInvalidez").Value;
                    xPerdaMaterial = current.Element("PerdaMaterial").Value;

                    xEmitenteCAT = current.Element("EmitenteCAT").Value;
                    xDataEmissaoCAT = current.Element("DataEmissaoCAT").Value;
                    xHoraEmissaoCAT = current.Element("HoraEmissaoCAT").Value;
                    xTipoCAT = current.Element("TipoCAT").Value;
                    xNumeroCAT = current.Element("NumeroCAT").Value;
                    xIniciativa = current.Element("Iniciativa").Value;
                    xBOPolicial = current.Element("BOPolicial").Value;
                    xDataObito = current.Element("DataObito").Value;

                    xDataInicioAbsenteismo = current.Element("DataInicioAbsenteismo").Value;
                    xHoraInicioAbsenteismo = current.Element("HoraInicioAbsenteismo").Value;
                    xPrevisaoRetornoAbsenteismo = current.Element("PrevisaoRetornoAbsenteismo").Value;
                    xDataRetornoAbsenteismo = current.Element("DataRetornoAbsenteismo").Value;
                    xHoraRetornoAbsenteismo = current.Element("HoraRetornoAbsenteismo").Value;
                    xObservacaoAbsenteismo = current.Element("ObservacaoAbsenteismo").Value;
                    
                }



                System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");


                rSelect = " NomeAbreviado = '" + xEmpresa + "' and dbo.udf_getnumeric( nomecodigo ) = '" + xCNPJ + "' ";

                //pegar Id Empresa                    
                rEmpresa.Find(rSelect);

                //se não achar empresa,  emitir retorno avisando
                if (rEmpresa.Id == 0)
                {
                    txt_Status.Text = txt_Status.Text + "Erro: Empresa não localizada ( " + xEmpresa + "  )" + System.Environment.NewLine;
                }



                rEmpregado.Find(" tNo_Empg='" + xColaborador + "'  and nId_Empr = " + rEmpresa.Id.ToString());


                if (txt_Status.Text == "")
                {
                    //se não achar empregado,  emitir retorno avisando
                    if (rEmpregado.Id == 0)
                    {
                        txt_Status.Text = txt_Status.Text + "Erro: Empregado não localizado ( " + xColaborador + "  )" + System.Environment.NewLine;
                    }
                }


                Acidente rAcidente = new Acidente();
                rAcidente.Find(" IdEmpregado = " + rEmpregado.Id.ToString() + " and convert( char(10), DataAcidente, 103 ) = '" + xDataAcidente + "' ");

                if (rAcidente.Id != 0)
                {
                    txt_Status.Text = "Acidente já existe para o colaborador " + xColaborador + " .";
                }


                if ( txt_Status.Text == "" )
                {
                    Acidente acidente = new Acidente();

                    acidente.IdEmpregado = rEmpregado;

                    acidente.DataAcidente = System.Convert.ToDateTime(xDataAcidente + " " + xHoraAcidente, ptBr);

                    xTipoAcidente = xTipoAcidente.Trim().ToUpper();
                    if (xTipoAcidente == "TIPICO" || xTipoAcidente == "TÍPICO") acidente.IndTipoAcidente = 1;
                    else if (xTipoAcidente == "DOENCA" || xTipoAcidente == "DOENÇA") acidente.IndTipoAcidente = 2;
                    else if (xTipoAcidente == "TRAJETO")  acidente.IndTipoAcidente = 3;
                    else acidente.IndTipoAcidente = 1;
                    
                    if ( xSituacaoGeradora!="")
                       acidente.Codigo_Situacao_Geradora = System.Convert.ToInt32(xSituacaoGeradora);

                    if ( xParteCorpoAtingida != "")
                       acidente.Codigo_Parte_Corpo_Atingida = System.Convert.ToInt32(xParteCorpoAtingida);

                    xLateralidade = xLateralidade.Trim().ToUpper();
                    if (xLateralidade == "ESQUERDA")
                        acidente.IdLateralidade = 1;
                    else if (xLateralidade == "DIREITA")
                        acidente.IdLateralidade = 2;
                    else if (xLateralidade == "AMBAS")
                        acidente.IdLateralidade = 3;
                    else
                        acidente.IdLateralidade = 0;

                    if ( xAgenteCausador != "" )
                       acidente.AgenteCausador = xAgenteCausador;
                    
                    if ( xDescricaoLesao != ""  )
                       acidente.Codigo_Descricao_Lesao = System.Convert.ToInt32(xDescricaoLesao);

                    acidente.Descricao = xDescricaoCompl;

                    LocalAcidente zLocalAcidente = new LocalAcidente();
                    zLocalAcidente.Find(System.Convert.ToInt32(xLocalAcidente));

                    acidente.IdLocalAcidente = zLocalAcidente;                   


                    acidente.Logradouro = xEnderecoLocal;
                    acidente.Nr_Logradouro = xNumeroLocal;
                    acidente.Municipio = xMunicipioLocal;
                    acidente.UF = xUFLocal;

                    acidente.EspecLocal = xEspecificacaoLocal;

                    acidente.indTipoSetor = 1;

                    //xUnidadeLocal
                    //xSetorAcidente



                    
                    if (xInternacao == "S")
                    {
                        acidente.DataInternacao = System.Convert.ToDateTime(xDataAtendimento + " " + xHoraAtendimento, ptBr);
                        acidente.HasInternacao = true;
                        acidente.CNES = xCodigoCNES;                        
                    }
                    else
                    {
                        acidente.HasInternacao = false;
                        acidente.CNES = "";                        
                    }


                    if (xDuracaoTratamentoDias != "")
                        acidente.DuracaoInternacao = System.Convert.ToInt16(xDuracaoTratamentoDias);


                    if (xReponsavel_Atestado != "")
                    {
                        acidente.MedicoInternacao = xReponsavel_Atestado;
                        acidente.CRMInternacao = xNrConselho_Atestado;
                        acidente.UFInternacao = xUFConselho_Atestado;
                        acidente.DiagnosticoProvavel = xDiagnosticoProvavel;
                    }


                    


                    if (txt_Status.Text == "" && xCID1 != "")
                    {
                        CID rCID = new CID();
                        rCID.Find(" CodigoCID = '" + xCID1 + "'  ");

                        if (rCID.Id == 0)
                        {
                            txt_Status.Text = "Código CID fornecido Inválido: " + xCID1;
                        }
                        else
                        {
                            acidente.IdCID = rCID;
                        }
                    }

                    if ( txt_Status.Text=="" && xCID2 != "")
                    {
                        CID rCID = new CID();
                        rCID.Find(" CodigoCID = '" + xCID2 + "'  ");

                        if (rCID.Id == 0)
                        {
                            txt_Status.Text = "Código CID fornecido Inválido: " + xCID2;
                        }
                        else
                        {
                            acidente.IdCID2 = rCID.Id;
                        }
                    }

                    if (txt_Status.Text == "" && xCID3 != "")
                    {
                        CID rCID = new CID();
                        rCID.Find(" CodigoCID = '" + xCID3 + "'  ");

                        if (rCID.Id == 0)
                        {
                            txt_Status.Text = "Código CID fornecido Inválido: " + xCID3;
                        }
                        else
                        {
                            acidente.IdCID3 = rCID.Id;
                        }
                    }

                    if (txt_Status.Text == "" && xCID4 != "")
                    {
                        CID rCID = new CID();
                        rCID.Find(" CodigoCID = '" + xCID4 + "'  ");

                        if (rCID.Id == 0)
                        {
                            txt_Status.Text = "Código CID fornecido Inválido: " + xCID4;
                        }
                        else
                        {
                            acidente.IdCID4 = rCID.Id;
                        }
                    }


                    if (xTransferidoOutroSetor == "S") acidente.isTransfSetor = true;
                    else acidente.isTransfSetor = false;

                    if (xAposentadoInvalidez == "S") acidente.isAposInval = true;
                    else acidente.isAposInval = false;

                    if (xPerdaMaterial != "")
                    {
                        try
                        {
                            acidente.PerdaMaterial = System.Convert.ToSingle(xPerdaMaterial);
                        }
                        catch //( Exception Ex)
                        {
                            acidente.PerdaMaterial = 0;
                        }
                    }
                    else
                    {
                        acidente.PerdaMaterial = 0;
                    }



                    if (xNumeroCAT != "")
                    {
                        CAT cat = new CAT();

                        cat.Inicialize();
                        cat.IdEmpregado = rEmpregado;


                        cat.NumeroCAT = xNumeroCAT;

                        cat.DataEmissao = System.Convert.ToDateTime( xDataEmissaoCAT  + " " + xHoraEmissaoCAT, ptBr);
                        cat.IndEmitente = Convert.ToInt32(xEmitenteCAT);
                        cat.IndTipoCAT = Convert.ToInt32(xTipoCAT);

                        if (xBOPolicial != "")
                        {
                            cat.hasRegPolicial = true;
                            cat.BO = xBOPolicial;
                        }
                        else
                        {
                            cat.hasRegPolicial = false;
                        }

                        if ( xDataObito != "")
                        {
                            cat.hasMorte = true;
                            cat.DataObito = System.Convert.ToDateTime( xDataObito , ptBr);
                        }
                        else
                        {
                            cat.hasMorte = false;
                        }

                        cat.UsuarioId = System.Convert.ToInt32(xCodUsuario);

                        cat.Save();
                        
                        acidente.IdCAT = cat;



                    }


                    Juridica xJuridica = new Juridica();
                    xJuridica.Find(rEmpresa.Id);

                    acidente.IdJuridica = xJuridica;


                    ArrayList emprFuncao = new EmpregadoFuncao().Find("nID_EMPREGADO=" + rEmpregado.Id
                    + " AND hDT_INICIO<='" + acidente.DataAcidente.ToString("yyyy-MM-dd")
                    + "' AND (hDT_TERMINO IS NULL OR hDT_TERMINO>='" + acidente.DataAcidente.ToString("yyyy-MM-dd") + "')");
                    
                    acidente.IdSetor = ((EmpregadoFuncao)emprFuncao[0]).nID_SETOR;


                    acidente.Save();



                    if (xDataInicioAbsenteismo != "")
                    {
                        acidente.hasAfastamento = true;

                        //salvar absenteismo
                        Afastamento absentismo = new Afastamento();

                        absentismo.Inicialize();
                        absentismo.IdEmpregado = rEmpregado;



                        absentismo.DataInicial = System.Convert.ToDateTime( xDataInicioAbsenteismo + " " + xHoraInicioAbsenteismo, ptBr);

                        if ( xPrevisaoRetornoAbsenteismo != "" )
                            absentismo.DataPrevista = System.Convert.ToDateTime(xPrevisaoRetornoAbsenteismo, ptBr); 
                        else
                            absentismo.DataPrevista = new DateTime();


                        if ( xDataRetornoAbsenteismo != "")
                            absentismo.DataVolta = System.Convert.ToDateTime( xDataRetornoAbsenteismo + " " + xHoraRetornoAbsenteismo, ptBr); 
                        else
                            absentismo.DataVolta = new DateTime();

                        absentismo.IndTipoAfastamento = (int)TipoAfastamento.Ocupacional;
                        absentismo.IdAcidente = acidente;


                        if (txt_Status.Text == "" && xCID1 != "")
                        {
                            CID rCID = new CID();
                            rCID.Find(" CodigoCID = '" + xCID1 + "'  ");

                            if (rCID.Id == 0)
                            {
                                txt_Status.Text = "Código CID fornecido Inválido: " + xCID1;
                            }
                            else
                            {
                                absentismo.IdCID = rCID;
                            }
                        }

                        if (txt_Status.Text == "" && xCID2 != "")
                        {
                            CID rCID = new CID();
                            rCID.Find(" CodigoCID = '" + xCID2 + "'  ");

                            if (rCID.Id == 0)
                            {
                                txt_Status.Text = "Código CID fornecido Inválido: " + xCID2;
                            }
                            else
                            {
                                absentismo.IdCID2 = rCID.Id;
                            }
                        }

                        if (txt_Status.Text == "" && xCID3 != "")
                        {
                            CID rCID = new CID();
                            rCID.Find(" CodigoCID = '" + xCID3 + "'  ");

                            if (rCID.Id == 0)
                            {
                                txt_Status.Text = "Código CID fornecido Inválido: " + xCID3;
                            }
                            else
                            {
                                absentismo.IdCID3 = rCID.Id;
                            }
                        }

                        if (txt_Status.Text == "" && xCID4 != "")
                        {
                            CID rCID = new CID();
                            rCID.Find(" CodigoCID = '" + xCID4 + "'  ");

                            if (rCID.Id == 0)
                            {
                                txt_Status.Text = "Código CID fornecido Inválido: " + xCID4;
                            }
                            else
                            {
                                absentismo.IdCID4 = rCID.Id;
                            }
                        }


                        absentismo.INSS = false;
                       
                        absentismo.UsuarioId = Convert.ToInt32(xCodUsuario);
                       
                        absentismo.Save();

                    }
                    else
                        acidente.hasAfastamento = false;

                    

                    
                    acidente.Save();

                }







            }
            catch (Exception ex)
            {
                txt_Status.Text = txt_Status.Text + "Erro: Carga do XML: " + ex.Message + System.Environment.NewLine;

                Session["zErro"] = txt_Status.Text;
                Response.Redirect("~/Comunicacao2.aspx");


            }
            finally
            {

                if (txt_Status.Text != "")
                {
                    Session["zErro"] = txt_Status.Text;
                    Response.Redirect("~/Comunicacao2.aspx");
                }
                else
                {
                    //rClinica.IdClinica.Find();

                    //Entities.Usuario user = (Entities.Usuario)Session["usuarioLogado"];
                    //Response.Redirect("~\\PCMSO\\DadosEmpregado_Guia_Auto.aspx?IdEmpregado=" + zId.ToString().Trim() + "&IdEmpresa=" + rEmpresa.Id.ToString().Trim() + "&IdUsuario=" + xCodUsuario + "&TipoExame=" + xTipoExame + "&Data=" + xData + "&IdClinica=" + rClinica.IdClinica.Id.ToString().Trim() + "&CodUsuario=" + xCodUsuario + "&xId=" + xID);


                    Session["zErro"] = txt_Status.Text + " " + "Processamento Finalizado";
                    Response.Redirect("~/Comunicacao2.aspx");
                }
            }


        }





        private void Executar_Convocacao(string xmlData)
        {


            ClinicaCliente rClinica = new ClinicaCliente();
            Empregado rEmpregado = new Empregado();
            Ilitera.Common.Pessoa rEmpresa = new Ilitera.Common.Pessoa();
            int zId = 0;

            string xCNPJ = "";
            string xEmpresa = "";
            string xCodUsuario = "";
            string xTipoExame = "";
            string xColaborador = "";
            string xData = "";
            string xID = "";
            string xIdConvocacao = "";

            string rSelect = "";


            try
            {


                XDocument doc = XDocument.Parse(xmlData);


                var points = doc.Descendants("Tipo");

                foreach (XElement current in points)
                {
                    xEmpresa = current.Element("Empresa").Value;
                    xCNPJ = current.Element("CNPJ").Value;
                    xCodUsuario = current.Element("CodUsuario").Value;
                    xTipoExame = current.Element("TipoExame").Value;
                    xColaborador = current.Element("Colaborador").Value;
                    xData = current.Element("Data").Value;
                    xID = current.Element("ID").Value;
                    xIdConvocacao = current.Element("Token").Value;   //estará salvo em tblConvocacao
                }


                rSelect = " NomeAbreviado = '" + xEmpresa + "' and dbo.udf_getnumeric( nomecodigo ) = '" + xCNPJ + "' ";

                //pegar Id Empresa                    
                rEmpresa.Find(rSelect);

                //se não achar empresa,  emitir retorno avisando
                if (rEmpresa.Id == 0)
                {
                    txt_Status.Text = txt_Status.Text + "Erro: Empresa não localizada ( " + xEmpresa + "  )" + System.Environment.NewLine;
                }

                //quando for convocação ( mailing ) no último irá o link para chamar comunicação
                //pode ir com um ID pronto




                //pegar Id Colaborador                       
                rEmpregado.Find(" tNo_Empg='" + xColaborador + "'  and nId_Empr = " + rEmpresa.Id.ToString());

                //se não achar empregado,  emitir retorno avisando
                if (rEmpregado.Id == 0)
                {
                    txt_Status.Text = txt_Status.Text + "Erro: Empregado não localizado ( " + xColaborador + "  )" + System.Environment.NewLine;
                }


                rClinica.Find(" IdCliente = " + rEmpresa.Id.ToString() + " and convert( char(12), IdCliente ) + ' ' + convert( char(12), IdClinica ) in ( select top 1 convert( char(12), IdCliente ) + ' ' + convert( char(12), IdClinica ) from ClinicaCliente where IdCliente = " + rEmpresa.Id.ToString() + " ) ");


                //se não achar clinica,  emitir retorno avisando
                if (rClinica.Id == 0)
                {
                    txt_Status.Text = txt_Status.Text + "Erro: Clínica não localizada " + System.Environment.NewLine;
                }


                //pegar IdExame
                if (xTipoExame.ToUpper().Trim() == "PERIODICO" || xTipoExame.ToUpper().Trim() == "PERIÓDICO")
                    xTipoExame = "4";
                else if (xTipoExame.ToUpper().Trim() == "DEMISSIONAL")
                    xTipoExame = "2";
                else if (xTipoExame.ToUpper().Trim().Substring(0, 5) == "MUDAN")
                    xTipoExame = "3";
                else if (xTipoExame.ToUpper().Trim().Substring(0, 7) == "RETORNO")
                    xTipoExame = "5";


                zId = rEmpregado.Id;


                //validar token - deve existir registro com dados do e-mail de convocação






            }
            catch (Exception ex)
            {
                txt_Status.Text = txt_Status.Text + "Erro: Carga do XML: " + ex.Message + System.Environment.NewLine;

                Session["zErro"] = txt_Status.Text;
                Response.Redirect("~/Comunicacao2.aspx");


            }
            finally
            {

                if (txt_Status.Text != "")
                {
                    Session["zErro"] = txt_Status.Text;
                    Response.Redirect("~/Comunicacao2.aspx");
                }
                else
                {
                    rClinica.IdClinica.Find();

                    Entities.Usuario user = (Entities.Usuario)Session["usuarioLogado"];
                    Response.Redirect("~\\PCMSO\\DadosEmpregado_Guia_Auto.aspx?IdEmpregado=" + zId.ToString().Trim() + "&IdEmpresa=" + rEmpresa.Id.ToString().Trim() + "&IdUsuario=" + xCodUsuario + "&TipoExame=" + xTipoExame + "&Data=" + xData + "&IdClinica=" + rClinica.IdClinica.Id.ToString().Trim() + "&CodUsuario=" + xCodUsuario + "&xId=" + xID);


                    Session["zErro"] = txt_Status.Text + " " + "Processamento Finalizado";
                    Response.Redirect("~/Comunicacao2.aspx");
                }
            }


        }






        private void Executar_CIPA(string xmlData)
        {


            Empregado rEmpregado = new Empregado();
            Ilitera.Common.Pessoa rEmpresa = new Ilitera.Common.Pessoa();

            //int zId = 0;

            string xID = "";
            string xCNPJ = "";
            string xEmpresa = "";
            string xCodUsuario = "";
            string xColaborador = "";
            string xIndicado = "";

            string xDataInicioCIPA = "";

            string xPapel = "";

            string rSelect = "";


            try
            {


                XDocument doc = XDocument.Parse(xmlData);


                var points = doc.Descendants("Tipo");

                foreach (XElement current in points)
                {
                    xEmpresa = current.Element("Empresa").Value;
                    xCNPJ = current.Element("CNPJ").Value;
                    xCodUsuario = current.Element("CodUsuario").Value;
                    xColaborador = current.Element("Colaborador").Value;
                    xID = current.Element("ID").Value;

                    xDataInicioCIPA = current.Element("DataInicioCIPA").Value;

                    xPapel = current.Element("Papel").Value;
                    xIndicado = current.Element("Indicado").Value;

                }




                System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");


                rSelect = " NomeAbreviado = '" + xEmpresa + "' and dbo.udf_getnumeric( nomecodigo ) = '" + xCNPJ + "' ";

                //pegar Id Empresa                    
                rEmpresa.Find(rSelect);

                //se não achar empresa,  emitir retorno avisando
                if (rEmpresa.Id == 0)
                {
                    txt_Status.Text = txt_Status.Text + "Erro: Empresa não localizada ( " + xEmpresa + "  )" + System.Environment.NewLine;
                }




                rEmpregado.Find(" tNo_Empg='" + xColaborador + "'  and nId_Empr = " + rEmpresa.Id.ToString());

                //se não achar empregado,  emitir retorno avisando
                if (rEmpregado.Id == 0)
                {
                    txt_Status.Text = txt_Status.Text + "Erro: Empregado não localizado ( " + xColaborador + "  )" + System.Environment.NewLine;
                }



                if (txt_Status.Text == "")
                {

                    //ver se CIPA com a data Inicial já existe, senão criar ( preciso ver como criar o calendário, quantos dias para cada evento )
                    //Poderia ver possibilidade de no repositório CIPA web,  poder anexar arquivo a cada evento,  não só à reuniões, acho que isso não complicado.
                    Cipa rCIPA = new Cipa();

                    rCIPA.Find("  IdCliente =" + rEmpresa.Id.ToString() + " and convert( char(10), Edital, 103 ) = '" + xDataInicioCIPA + "' ");

                    if (rCIPA.Id == 0)
                    {
                        Cliente rCliente = new Cliente();
                        rCliente.Find(rEmpresa.Id);

                        Prestador rPrestador = new Prestador();
                        rPrestador.Find(" prestador.idjuridicapessoa in (select idjuridicapessoa from juridicapessoa where idpessoa in   (select idpessoa from usuario where idusuario = " + xCodUsuario + " ))");

                        if (rPrestador.Id == 0)
                        {
                            txt_Status.Text = txt_Status.Text + "Erro: erro na salva da CIPA (1)" + System.Environment.NewLine;
                        }


                        Obrigacao rObrigacao = new Obrigacao();

                        if (txt_Status.Text == "")
                        {                            
                            rObrigacao.Find(" nome = 'CIPA - Criação e Elaboração' ");

                            if (rObrigacao.Id == 0)
                            {
                                txt_Status.Text = txt_Status.Text + "Erro: erro na salva da CIPA (2)" + System.Environment.NewLine;
                            }
                        }


                        Compromisso rCompromisso = new Compromisso();
                        rCompromisso.Find(0);

                        PedidoGrupo rPedidoGrupo = new PedidoGrupo();

                        if (txt_Status.Text == "")
                        {
                            rPedidoGrupo.DataSolicitacao = System.DateTime.Now;
                            rPedidoGrupo.IdCliente = rCliente;
                            rPedidoGrupo.Solicitante = "";
                            rPedidoGrupo.IdCompromisso = rCompromisso;
                            rPedidoGrupo.IdPrestador = rPrestador;
                            rPedidoGrupo.Numero = PedidoGrupo.GetNumeroPedido();
                            rPedidoGrupo.Save();

                            if (rPedidoGrupo.Id == 0)
                            {
                                txt_Status.Text = txt_Status.Text + "Erro: erro na salva da CIPA (3)" + System.Environment.NewLine;
                            }

                        }


                        EquipamentoBase rEqpto = new EquipamentoBase();
                        rEqpto.Find(0);

                        Pedido rPedido = new Pedido();


                        if (txt_Status.Text == "")
                        {
                            
                            rPedido.IdCliente = rCliente;
                            rPedido.IdPrestador = rPrestador;
                            rPedido.DataSolicitacao = System.DateTime.Now;
                            rPedido.DataSugestao = System.DateTime.Now;
                            rPedido.IdEquipamentoBase = rEqpto;
                            rPedido.IdObrigacao = rObrigacao;
                            rPedido.IdPedidoGrupo = rPedidoGrupo;
                            rPedido.Save();


                            if (rPedido.Id == 0)
                            {
                                txt_Status.Text = txt_Status.Text + "Erro: erro na salva da CIPA (4)" + System.Environment.NewLine;
                            }

                        }



                        DocumentoBase rDoc = new DocumentoBase();

                        if (txt_Status.Text == "")
                        {                            
                            rDoc.Find("NomeDocumento = 'CIPA'");

                            if (rDoc.Id == 0)
                            {
                                txt_Status.Text = txt_Status.Text + "Erro: erro na salva da CIPA (5)" + System.Environment.NewLine;
                            }
                        }




                        //criar CIPA
                        rCIPA = new Cipa();

                        if (txt_Status.Text == "")
                        {
                            rCIPA.Edital = System.Convert.ToDateTime(xDataInicioCIPA, ptBr);
                            rCIPA.IdCliente = rCliente;
                            rCIPA.IdPedido = rPedido;
                            rCIPA.IdPrestador = rPrestador;
                            rCIPA.Edital = System.Convert.ToDateTime(xDataInicioCIPA, ptBr);
                            rCIPA.IdDocumentoBase = rDoc;
                            rCIPA.DataLevantamento = System.Convert.ToDateTime(xDataInicioCIPA, ptBr);
                            rCIPA.ComissaoEleitoral = System.Convert.ToDateTime(xDataInicioCIPA, ptBr);
                            rCIPA.Save();


                            if (rCIPA.Id == 0)
                            {
                                txt_Status.Text = txt_Status.Text + "Erro: erro na salva da CIPA (6)" + System.Environment.NewLine;
                            }

                        }

                    }




                    //membrocipa-> GetNomeCargo()
                    //indstatus  1 - ativo   2 - afastado   3 - renunciou
                    //indgrupomembro  0 - empregado    1 - empregador    2 - secretario
                    //indtitularsuplente      1 - titular    2 - suplente
                    //estabilidade    data estabilidade
                    //numero

                    // Presidente         ativo, empregador, num=0, titular
                    // Vice-Presidente    ativo, empregado, num=0, titular
                    // numero membro      ativo, empregador/empregado, num>0, titular
                    // numero suplente    ativo, empregador/empregado, num>0, suplente
                    // secretario         ativo, secretario,titular 
                    // substituto secretario   ativo, secretario,suplente

                    MembroCipa rMembro = new MembroCipa();
                    rMembro.IdCipa = rCIPA;
                    rMembro.IdEmpregado = rEmpregado;
                    rMembro.IndStatus = 1;

                    if (xPapel.ToUpper().IndexOf("PRESIDENTE") >= 0 && xPapel.ToUpper().IndexOf("VICE") >= 0)
                    {
                        rMembro.Numero = 0;
                        rMembro.IndGrupoMembro = 0;
                        rMembro.IndTitularSuplente = 1;
                    }
                    else if (xPapel.ToUpper().IndexOf("PRESIDENTE") >= 0)
                    {
                        rMembro.Numero = 0;
                        rMembro.IndGrupoMembro = 1;
                        rMembro.IndTitularSuplente = 1;
                    }
                    else if (xPapel.ToUpper().IndexOf("SECRETÁRIO") >= 0 || xPapel.ToUpper().IndexOf("SECRETARIO") >= 0)
                    {
                        rMembro.Numero = 0;
                        rMembro.IndGrupoMembro = 2;

                        if (xPapel.ToUpper().IndexOf("SUBST") >= 0)
                            rMembro.IndTitularSuplente = 2;
                        else
                            rMembro.IndTitularSuplente = 1;
                    }
                    else
                    {
                        if (xIndicado.ToUpper().Trim() == "EMPREGADOR")
                            rMembro.IndGrupoMembro = 1;
                        else 
                            rMembro.IndGrupoMembro = 0;
                        

                        if (xPapel.ToUpper().IndexOf("SUPLENTE") >= 0)
                            rMembro.IndTitularSuplente = 2;
                        else
                            rMembro.IndTitularSuplente = 1;

                        string xNum = System.Text.RegularExpressions.Regex.Match(xPapel, @"\d+").Value;

                        if (xNum.Trim() == "")
                            xNum = "1";


                        rMembro.Numero = System.Convert.ToInt16(xNum);
                        
                    }

                    rMembro.NomeMembro = xColaborador;


                    //antes de salvar procurar para ver se já existe esse colaborador como membro
                    MembroCipa zMembroBusca = new MembroCipa();
                    zMembroBusca.Find(" IdCipa = " + rCIPA.Id.ToString() + " and IdEmpregado = " + rMembro.IdEmpregado.Id.ToString());

                    if (zMembroBusca.Id != 0)
                    {
                        txt_Status.Text = txt_Status.Text + "Erro: Colaborador já é membro da CIPA" + System.Environment.NewLine;
                    }
                    else
                    {
                        rMembro.Save();
                    }

                    
                    
                }


            }
            catch (Exception ex)
            {
                txt_Status.Text = txt_Status.Text + "Erro: Carga do XML: " + ex.Message + System.Environment.NewLine;

                Session["zErro"] = txt_Status.Text;
                Response.Redirect("~/Comunicacao2.aspx");


            }
            finally
            {

                if (txt_Status.Text != "")
                {
                    Session["zErro"] = txt_Status.Text;
                    Response.Redirect("~/Comunicacao2.aspx");
                }
                else
                {

                    // Entities.Usuario user = (Entities.Usuario)Session["usuarioLogado"];
                    // Response.Redirect("~\\PCMSO\\CadAbsentismo_Auto.aspx?IdEmpregado=" + zId.ToString().Trim() + "&IdEmpresa=" + rEmpresa.Id.ToString().Trim() + "&IdUsuario=" + xCodUsuario + "&Data=" + xData + "&CodUsuario=" + xCodUsuario + "&xId=" + xID);


                    // 'CadAbsentismo.aspx?IdEmpregado=" + Session["Empregado"].ToString() + "&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + xUser.IdUsuario.ToString() + "&IdAcidente=0'

                    Session["zErro"] = txt_Status.Text + " " + "Processamento Finalizado";
                    Response.Redirect("~/Comunicacao2.aspx");
                }
            }


        }


        private void Executar_CIPA_Evento(string xmlData)
        {


            Empregado rEmpregado = new Empregado();
            Ilitera.Common.Pessoa rEmpresa = new Ilitera.Common.Pessoa();

            //int zId = 0;

            string xID = "";
            string xCNPJ = "";
            string xEmpresa = "";
            string xCodUsuario = "";
            string xEvento = "";
            string xDescricao = "";
            string xTipoReuniao = "";                        

            string xArquivo = "";
            string xConteudo_Arquivo = "";

            string xDataEvento = "";
            string xDataInicioCIPA = "";

            string rSelect = "";


            try
            {


                XDocument doc = XDocument.Parse(xmlData);


                var points = doc.Descendants("Tipo");

                foreach (XElement current in points)
                {
                    xEmpresa = current.Element("Empresa").Value;
                    xCNPJ = current.Element("CNPJ").Value;
                    xCodUsuario = current.Element("CodUsuario").Value;

                    xID = current.Element("ID").Value;

                    xDataInicioCIPA = current.Element("DataInicioCIPA").Value;
                    xDataEvento = current.Element("DataEvento").Value;

                    xEvento = current.Element("Evento").Value;
                    xDescricao = current.Element("Descricao").Value;
                    xTipoReuniao = current.Element("TipoReuniao").Value;

                    xArquivo = current.Element("Arquivo").Value;
                    xConteudo_Arquivo = current.Element("Conteudo_Arquivo").Value;

                }



                System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");


                rSelect = " NomeAbreviado = '" + xEmpresa + "' and dbo.udf_getnumeric( nomecodigo ) = '" + xCNPJ + "' ";

                //pegar Id Empresa                    
                rEmpresa.Find(rSelect);

                //se não achar empresa,  emitir retorno avisando
                if (rEmpresa.Id == 0)
                {
                    txt_Status.Text = txt_Status.Text + "Erro: Empresa não localizada ( " + xEmpresa + "  )" + System.Environment.NewLine;
                }






                if (txt_Status.Text == "")
                {



                    //ver se CIPA com a data Inicial já existe, senão criar ( preciso ver como criar o calendário, quantos dias para cada evento )
                    //Poderia ver possibilidade de no repositório CIPA web,  poder anexar arquivo a cada evento,  não só à reuniões, acho que isso não complicado.
                    Cipa rCIPA = new Cipa();

                    rCIPA.Find("  IdCliente =" + rEmpresa.Id.ToString() + " and convert( char(10), Edital, 103 ) = '" + xDataInicioCIPA + "' ");

                    if (rCIPA.Id == 0)
                    {
                        Cliente rCliente = new Cliente();
                        rCliente.Find(rEmpresa.Id);

                        Prestador rPrestador = new Prestador();
                        rPrestador.Find(" prestador.idjuridicapessoa in (select idjuridicapessoa from juridicapessoa where idpessoa in   (select idpessoa from usuario where idusuario = " + xCodUsuario + " ))");

                        if (rPrestador.Id == 0)
                        {
                            txt_Status.Text = txt_Status.Text + "Erro: erro na salva da CIPA (1)" + System.Environment.NewLine;
                        }


                        Obrigacao rObrigacao = new Obrigacao();

                        if (txt_Status.Text == "")
                        {
                            rObrigacao.Find(" nome = 'CIPA - Criação e Elaboração' ");

                            if (rObrigacao.Id == 0)
                            {
                                txt_Status.Text = txt_Status.Text + "Erro: erro na salva da CIPA (2)" + System.Environment.NewLine;
                            }
                        }


                        Compromisso rCompromisso = new Compromisso();
                        rCompromisso.Find(0);

                        PedidoGrupo rPedidoGrupo = new PedidoGrupo();

                        if (txt_Status.Text == "")
                        {
                            rPedidoGrupo.DataSolicitacao = System.DateTime.Now;
                            rPedidoGrupo.IdCliente = rCliente;
                            rPedidoGrupo.Solicitante = "";
                            rPedidoGrupo.IdCompromisso = rCompromisso;
                            rPedidoGrupo.IdPrestador = rPrestador;
                            rPedidoGrupo.Numero = PedidoGrupo.GetNumeroPedido();
                            rPedidoGrupo.Save();

                            if (rPedidoGrupo.Id == 0)
                            {
                                txt_Status.Text = txt_Status.Text + "Erro: erro na salva da CIPA (3)" + System.Environment.NewLine;
                            }

                        }


                        EquipamentoBase rEqpto = new EquipamentoBase();
                        rEqpto.Find(0);

                        Pedido rPedido = new Pedido();


                        if (txt_Status.Text == "")
                        {

                            rPedido.IdCliente = rCliente;
                            rPedido.IdPrestador = rPrestador;
                            rPedido.DataSolicitacao = System.DateTime.Now;
                            rPedido.DataSugestao = System.DateTime.Now;
                            rPedido.IdEquipamentoBase = rEqpto;
                            rPedido.IdObrigacao = rObrigacao;
                            rPedido.IdPedidoGrupo = rPedidoGrupo;
                            rPedido.Save();


                            if (rPedido.Id == 0)
                            {
                                txt_Status.Text = txt_Status.Text + "Erro: erro na salva da CIPA (4)" + System.Environment.NewLine;
                            }

                        }



                        DocumentoBase rDoc = new DocumentoBase();

                        if (txt_Status.Text == "")
                        {
                            rDoc.Find("NomeDocumento = 'CIPA'");

                            if (rDoc.Id == 0)
                            {
                                txt_Status.Text = txt_Status.Text + "Erro: erro na salva da CIPA (5)" + System.Environment.NewLine;
                            }
                        }




                        //criar CIPA
                        rCIPA = new Cipa();

                        if (txt_Status.Text == "")
                        {
                            rCIPA.Edital = System.Convert.ToDateTime(xDataInicioCIPA, ptBr);
                            rCIPA.IdCliente = rCliente;
                            rCIPA.IdPedido = rPedido;
                            rCIPA.IdPrestador = rPrestador;
                            rCIPA.Edital = System.Convert.ToDateTime(xDataInicioCIPA, ptBr);
                            rCIPA.IdDocumentoBase = rDoc;
                            rCIPA.DataLevantamento = System.Convert.ToDateTime(xDataInicioCIPA, ptBr);
                            rCIPA.ComissaoEleitoral = System.Convert.ToDateTime(xDataInicioCIPA, ptBr);
                            rCIPA.Save();


                            if (rCIPA.Id == 0)
                            {
                                txt_Status.Text = txt_Status.Text + "Erro: erro na salva da CIPA (6)" + System.Environment.NewLine;
                            }

                        }

                    }


                    if (txt_Status.Text == "")  //criar evento
                    {

                        CipaRepositorio xRepositorio = new CipaRepositorio();

                        xRepositorio.IdCipa = rCIPA.Id;

                        xRepositorio.Descricao = xDescricao;

                        xRepositorio.DataHora = System.Convert.ToDateTime(xDataEvento, ptBr);

                        xRepositorio.Evento = xEvento;

                        if (xTipoReuniao.Trim().ToUpper() == "ORDINÁRIA" || xTipoReuniao.Trim().ToUpper() == "ORDINARIA")
                            xRepositorio.TipoReuniao = "O";
                        else
                            xRepositorio.TipoReuniao = "E";

                        xRepositorio.Save();





                        if (txt_Status.Text == "" && xConteudo_Arquivo != "")
                        {
                            //xArquivo terá a extensão

                            byte[] arrBytes = Convert.FromBase64String(xConteudo_Arquivo);

                            //montar com diretório padrão da empresa, nome do colaborador, data e extensão - chave Arquivo
                            Cliente xCliente = new Cliente();
                            xCliente.Find(System.Convert.ToInt32(rEmpresa.Id));

                            string xArq = "";
                            Random xRand = new Random();


                            //if (Session["Servidor_Web"].ToString().Trim().ToUpper() == "ILITERA")
                            xArq = "CIPA_" + xDataEvento.Replace("/", "") + "_" + xRand.Next().ToString() + "." + xArquivo;


                            string uri = "ftp://54.94.157.244:21/" + xCliente.DiretorioPadrao.ToString().Trim().Replace(" ", "%20") + "/Prontuario/" + xArq;

                            //string uri = "ftp://54.94.157.244:21/5A%20GT/Prontuario/teste_xml.pdf";



                            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(new Uri(uri));
                            request.Method = WebRequestMethods.Ftp.UploadFile;

                            request.Credentials = new NetworkCredential("lmm", "Lm160521");

                            request.UseBinary = true;
                            request.UsePassive = true;
                            request.ContentLength = arrBytes.Length;
                            Stream stream = request.GetRequestStream();
                            stream.Write(arrBytes, 0, arrBytes.Length);
                            stream.Close();
                            FtpWebResponse res = (FtpWebResponse)request.GetResponse();

                            xRepositorio.Anexo = @"I:\FotosDocsDigitais\" + xCliente.DiretorioPadrao.ToString().Trim() + "/Prontuario/" + xArq;

                            //salvar dados do atestado nos campos do afastamento

                            xRepositorio.Save();
                        }


                    }

                }

            }
            catch (Exception ex)
            {
                txt_Status.Text = txt_Status.Text + "Erro: Carga do XML: " + ex.Message + System.Environment.NewLine;

                Session["zErro"] = txt_Status.Text;
                Response.Redirect("~/Comunicacao2.aspx");


            }
            finally
            {

                if (txt_Status.Text != "")
                {
                    Session["zErro"] = txt_Status.Text;
                    Response.Redirect("~/Comunicacao2.aspx");
                }
                else
                {

                    // Entities.Usuario user = (Entities.Usuario)Session["usuarioLogado"];
                    // Response.Redirect("~\\PCMSO\\CadAbsentismo_Auto.aspx?IdEmpregado=" + zId.ToString().Trim() + "&IdEmpresa=" + rEmpresa.Id.ToString().Trim() + "&IdUsuario=" + xCodUsuario + "&Data=" + xData + "&CodUsuario=" + xCodUsuario + "&xId=" + xID);


                    // 'CadAbsentismo.aspx?IdEmpregado=" + Session["Empregado"].ToString() + "&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + xUser.IdUsuario.ToString() + "&IdAcidente=0'

                    Session["zErro"] = txt_Status.Text + " " + "Processamento Finalizado";
                    Response.Redirect("~/Comunicacao2.aspx");
                }
            }


        }



        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
        }
        #endregion
    }
}

