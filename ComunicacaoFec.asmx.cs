using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Ilitera.Opsa.Data;
using System.Data;
using System.IO;
using System.Net;
using System.Xml;
using Ilitera.Common;
using System.Net.Mail;
using Ilitera.Data;
using System.Xml.Linq;

using System.Text;

using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

using Ilitera.Sied.Report;
using Ilitera.PCMSO.Report;


namespace Ilitera.Net
{
    /// <summary>
    /// Summary description for ComunicacaoFec
    /// </summary>
    //[WebService(Namespace = "http://tempuri.org/")]
    [WebService(Namespace = "https://www.ilitera.net.br/essence_hom")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [System.Web.Script.Services.ScriptService()]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class ComunicacaoFec : System.Web.Services.WebService
    {

        //chamar via URL - http://localhost:46870/comunicacao.asmx/HelloWorld

        //  https://www.ilitera.net.br/essence_hom/comunicacao.asmx?op=HelloWorld




        //[WebMethod]
        //public string HelloWorld(string xTeste, string xTeste2)
        //{
        //    return "Hello World" + " "+ xTeste + " " + xTeste2;
        //}

        //exemplos

        //  https://stackoverflow.com/questions/31374512/post-parameters-during-asmx-web-service-call



        //outro exemplo
        //https://stackoverflow.com/questions/11450836/http-post-using-web-service


        //para retornar uM XML, por exemplo, com arquivos de atestados e CATs -  usar na chamada     public XMLDocument ....
        // https://stackoverflow.com/questions/2784818/how-do-i-return-pure-xml-from-asmx-web-service






        [WebMethod]
        public XmlDocument Retornar_Clinicas(string CEP, string Numero_Clinicas, string Valor_Maximo)
        {


            ClinicaCliente rClinica = new ClinicaCliente();
            Empregado rEmpregado = new Empregado();
            Ilitera.Common.Pessoa rEmpresa = new Ilitera.Common.Pessoa();
            //int zId = 0;




            //string rSelect = "";
            string txt_Status = "";

            XmlDocument xRet = new XmlDocument();


            try
            {

                Ilitera.Data.Comunicacao xBusca = new Ilitera.Data.Comunicacao();
                DataSet xDs = new DataSet();

                xDs = xBusca.Trazer_Clinicas_CEP(CEP, Numero_Clinicas, Valor_Maximo);
                                

                if (xDs.Tables[0].Rows.Count == 0)
                {
                    txt_Status = txt_Status + "Erro: Clínicas não localizadas por CEP" + System.Environment.NewLine;
                }



                //montar XML de retorno com clínicas
                if ( txt_Status=="")
                {

                    xDs.DataSetName = "Retorno";
                    xDs.Tables[0].TableName = "Clinicas";

                    string xstrXML = xDs.GetXml();

                    xRet.LoadXml(xstrXML);
                    

                }


                

            }
            catch (Exception ex)
            {
                txt_Status = txt_Status + "Erro: Carga do XML: " + ex.Message + System.Environment.NewLine;

                //Session["zErro"] = txt_Status.Text;
                //Response.Redirect("~/Comunicacao2.aspx");
                
            }
            finally
            {

                if (txt_Status != "")
                {

                    //montar XMLDocument com erro
                    string xRetorno = @"<?xml version=""1.0"" encoding=""UTF-8""?><Erro>" + txt_Status + "</Erro>";

                    xRet.LoadXml(xRetorno);


                }

            }


            return xRet;

        }


        [WebMethod]
        public XmlDocument Retornar_Clinicas_Cliente(string CNPJ, string CEP, string Numero_Clinicas, string Valor_Maximo)
        {


            ClinicaCliente rClinica = new ClinicaCliente();
            Empregado rEmpregado = new Empregado();
            Ilitera.Common.Pessoa rEmpresa = new Ilitera.Common.Pessoa();
            //int zId = 0;



            string rSelect = "";
            string txt_Status = "";

            XmlDocument xRet = new XmlDocument();


           
            


                try
                {


                rEmpresa = new Ilitera.Common.Pessoa();

                rSelect = " dbo.udf_getnumeric( nomecodigo ) = '" + CNPJ + "' and isinativo = 0 ";

                //pegar Id Empresa                    
                rEmpresa.Find(rSelect);

                //se não achar empresa,  emitir retorno avisando
                if (rEmpresa.Id == 0)
                {
                    txt_Status = txt_Status + "Erro: Empresa não localizada ( " + CNPJ + "  )" + System.Environment.NewLine;
                }


                DataSet xDs = new DataSet();

                if (txt_Status == "")
                {

                    Ilitera.Data.Comunicacao xBusca = new Ilitera.Data.Comunicacao();
                

                    xDs = xBusca.Trazer_Clinicas_CEP(rEmpresa.Id, CEP, Numero_Clinicas, Valor_Maximo);


                    if (xDs.Tables[0].Rows.Count == 0)
                    {
                        txt_Status = txt_Status + "Erro: Clínicas não localizadas por CEP" + System.Environment.NewLine;
                    }

                }


                //montar XML de retorno com clínicas
                if (txt_Status == "")
                {

                    xDs.DataSetName = "Retorno";
                    xDs.Tables[0].TableName = "Clinicas";

                    string xstrXML = xDs.GetXml();

                    xRet.LoadXml(xstrXML);


                }




            }
            catch (Exception ex)
            {
                txt_Status = txt_Status + "Erro: Carga do XML: " + ex.Message + System.Environment.NewLine;

                //Session["zErro"] = txt_Status.Text;
                //Response.Redirect("~/Comunicacao2.aspx");

            }
            finally
            {

                if (txt_Status != "")
                {

                    //montar XMLDocument com erro
                    string xRetorno = @"<?xml version=""1.0"" encoding=""UTF-8""?><Erro>" + txt_Status + "</Erro>";

                    xRet.LoadXml(xRetorno);


                }

            }


            return xRet;

        }





        [WebMethod]
        public XmlDocument Retornar_Laudos(string xCNPJ)
        {


            Ilitera.Common.Pessoa rEmpresa = new Ilitera.Common.Pessoa();
            //int zId = 0;




            string rSelect = "";
            string txt_Status = "";

            XmlDocument xRet = new XmlDocument();


            try
            {


                rEmpresa = new Ilitera.Common.Pessoa();

                rSelect = " dbo.udf_getnumeric( nomecodigo ) = '" + xCNPJ + "' and isinativo = 0 "; ;

                //pegar Id Empresa                    
                rEmpresa.Find(rSelect);

                //se não achar empresa,  emitir retorno avisando
                if (rEmpresa.Id == 0)
                {
                    txt_Status = txt_Status + "Erro: Empresa não localizada ( " + xCNPJ + "  )" + System.Environment.NewLine;                    
                }



                if (txt_Status == "")
                {

                    System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");

                    Ilitera.Data.PPRA_EPI xLaudos = new Ilitera.Data.PPRA_EPI();

                    DataSet xDs = xLaudos.Retornar_Laudos(rEmpresa.Id);


                    //if (xDs.Tables[0].Rows.Count == 0)
                    //{
                    //    txt_Status = txt_Status + "Empresa sem laudos concluídos." + System.Environment.NewLine;
                    //}



                    //montar XML de retorno com clínicas
                    if (txt_Status == "")
                    {

                        xDs.DataSetName = "Retorno";
                        xDs.Tables[0].TableName = "Laudos";

                        string xstrXML = xDs.GetXml();

                        xRet.LoadXml(xstrXML);


                    }


                }

            }
            catch (Exception ex)
            {
                txt_Status = txt_Status + "Erro: Carga do XML: " + ex.Message + System.Environment.NewLine + ex.InnerException + System.Environment.NewLine;

                //Session["zErro"] = txt_Status.Text;
                //Response.Redirect("~/Comunicacao2.aspx");

            }
            finally
            {

                if (txt_Status != "")
                {

                    //montar XMLDocument com erro
                    string xRetorno = @"<?xml version=""1.0"" encoding=""UTF-8""?><Erro>" + txt_Status + "</Erro>";

                    xRet.LoadXml(xRetorno);


                }

            }


            return xRet;

        }




        [WebMethod]
        public XmlDocument Retornar_Setores(string xCNPJ)
        {


            Ilitera.Common.Pessoa rEmpresa = new Ilitera.Common.Pessoa();
            //int zId = 0;




            string rSelect = "";
            string txt_Status = "";

            XmlDocument xRet = new XmlDocument();


            try
            {


                rEmpresa = new Ilitera.Common.Pessoa();

                rSelect = " dbo.udf_getnumeric( nomecodigo ) = '" + xCNPJ + "' and isinativo = 0 "; 

                //pegar Id Empresa                    
                rEmpresa.Find(rSelect);

                //se não achar empresa,  emitir retorno avisando
                if (rEmpresa.Id == 0)
                {
                    txt_Status = txt_Status + "Erro: Empresa não localizada ( " + xCNPJ + "  )" + System.Environment.NewLine;
                }



                if (txt_Status == "")
                {

                    System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");

                    Ilitera.Data.PPRA_EPI xLaudos = new Ilitera.Data.PPRA_EPI();

                    DataSet xDs = xLaudos.Retornar_Setores(rEmpresa.Id);


                    //if (xDs.Tables[0].Rows.Count == 0)
                    //{
                    //    txt_Status = txt_Status + "Empresa sem laudos concluídos." + System.Environment.NewLine;
                    //}



                    //montar XML de retorno com clínicas
                    if (txt_Status == "")
                    {

                        xDs.DataSetName = "Retorno";
                        xDs.Tables[0].TableName = "Setores";

                        string xstrXML = xDs.GetXml();

                        xstrXML = xstrXML.Replace("<Setores>", "");
                        xstrXML = xstrXML.Replace("</Setores>", "");


                        xRet.LoadXml(xstrXML);


                    }


                }

            }
            catch (Exception ex)
            {
                txt_Status = txt_Status + "Erro: Carga do XML: " + ex.Message + System.Environment.NewLine + ex.InnerException + System.Environment.NewLine;

                //Session["zErro"] = txt_Status.Text;
                //Response.Redirect("~/Comunicacao2.aspx");

            }
            finally
            {

                if (txt_Status != "")
                {

                    //montar XMLDocument com erro
                    string xRetorno = @"<?xml version=""1.0"" encoding=""UTF-8""?><Erro>" + txt_Status + "</Erro>";

                    xRet.LoadXml(xRetorno);


                }

            }


            return xRet;

        }





        [WebMethod]
        public XmlDocument Retornar_Cargos(string xCNPJ)
        {


            Ilitera.Common.Pessoa rEmpresa = new Ilitera.Common.Pessoa();
            //int zId = 0;




            string rSelect = "";
            string txt_Status = "";

            XmlDocument xRet = new XmlDocument();


            try
            {


                rEmpresa = new Ilitera.Common.Pessoa();

                rSelect = " dbo.udf_getnumeric( nomecodigo ) = '" + xCNPJ + "' and isinativo = 0 "; 

                //pegar Id Empresa                    
                rEmpresa.Find(rSelect);

                //se não achar empresa,  emitir retorno avisando
                if (rEmpresa.Id == 0)
                {
                    txt_Status = txt_Status + "Erro: Empresa não localizada ( " + xCNPJ + "  )" + System.Environment.NewLine;
                }



                if (txt_Status == "")
                {

                    System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");

                    Ilitera.Data.PPRA_EPI xLaudos = new Ilitera.Data.PPRA_EPI();

                    DataSet xDs = xLaudos.Retornar_Cargos(rEmpresa.Id);


                    //if (xDs.Tables[0].Rows.Count == 0)
                    //{
                    //    txt_Status = txt_Status + "Empresa sem laudos concluídos." + System.Environment.NewLine;
                    //}



                    //montar XML de retorno com clínicas
                    if (txt_Status == "")
                    {

                        xDs.DataSetName = "Retorno";
                        xDs.Tables[0].TableName = "Cargos";

                        string xstrXML = xDs.GetXml();

                        xstrXML = xstrXML.Replace("<Cargos>", "");
                        xstrXML = xstrXML.Replace("</Cargos>", "");

                        xRet.LoadXml(xstrXML);


                    }


                }

            }
            catch (Exception ex)
            {
                txt_Status = txt_Status + "Erro: Carga do XML: " + ex.Message + System.Environment.NewLine + ex.InnerException + System.Environment.NewLine;

                //Session["zErro"] = txt_Status.Text;
                //Response.Redirect("~/Comunicacao2.aspx");

            }
            finally
            {

                if (txt_Status != "")
                {

                    //montar XMLDocument com erro
                    string xRetorno = @"<?xml version=""1.0"" encoding=""UTF-8""?><Erro>" + txt_Status + "</Erro>";

                    xRet.LoadXml(xRetorno);


                }

            }


            return xRet;

        }



        private XmlDocument GetXmlDocument(XDocument document)
        {
            using (XmlReader xmlReader = document.CreateReader())
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(xmlReader);
                if (document.Declaration != null)
                {
                    XmlDeclaration dec = xmlDoc.CreateXmlDeclaration(document.Declaration.Version,
                        document.Declaration.Encoding, document.Declaration.Standalone);
                    xmlDoc.InsertBefore(dec, xmlDoc.FirstChild);
                }
                return xmlDoc;
            }
        }




        [WebMethod]
        public XmlDocument Solicitar_Laudo(string CNPJ, string Laudo, string DataLaudo, string TipoRetorno)
        {

            Ilitera.Common.Pessoa rEmpresa = new Ilitera.Common.Pessoa();

            //int zId = 0;
                      

            string rSelect = "";

            string xRetorno = "";

            string txt_Status = "";

            XmlDocument zRet = new XmlDocument();


            try
            {


              

                xRetorno = "";


                if (CNPJ.Length > 14)
                {
                    txt_Status = txt_Status + " Campo CNPJ inválido |";
                    xRetorno = xRetorno + " Campo CNPJ Inválido |";
                }
                if (Laudo.Length < 1 || Laudo.Length > 6)
                {
                    txt_Status = txt_Status + " Campo Laudo inválido |";
                    xRetorno = xRetorno + " Campo Laudo Inválido |";
                }
                if (TipoRetorno.Length < 1 || TipoRetorno.Length > 10)
                {
                    txt_Status = txt_Status + " Campo Tipo Retorno inválido |";
                    xRetorno = xRetorno + " Campo Tipo Retorno Inválido |";
                }
                if (DataLaudo.Length != 10)
                {
                    txt_Status = txt_Status + " Campo Data Laudo inválido |";
                    xRetorno = xRetorno + " Campo Data Laudo Inválido |";
                }
                if (Validar_Data(DataLaudo) == false)
                {
                    txt_Status = txt_Status + " Campo Data Laudo inválido |";
                    xRetorno = xRetorno + " Campo Data Laudo Inválido |";
                }

                if (xRetorno != "")
                    xRetorno = "50 ( Validação de campos ): " + xRetorno;






                if (txt_Status == "")
                {

                    rEmpresa = new Ilitera.Common.Pessoa();

                    rSelect = " dbo.udf_getnumeric( nomecodigo ) = '" + CNPJ + "' and isinativo = 0 "; 

                    //pegar Id Empresa                    
                    rEmpresa.Find(rSelect);

                    //se não achar empresa,  emitir retorno avisando
                    if (rEmpresa.Id == 0)
                    {
                        txt_Status = txt_Status + "Erro: Empresa não localizada ( " + CNPJ + "  )" + System.Environment.NewLine;
                        xRetorno = "03 (Empresa não localizada (" + CNPJ + ")";
                    }

                }



                



                if (txt_Status == "")
                {




                    if (TipoRetorno.ToUpper().Trim() == "LINK")
                    {

                        tbl_Laudos zLaudos = new tbl_Laudos();
                        zLaudos.Find(" nId_Empr = " + rEmpresa.Id.ToString() + " and convert( char(10),Data_Laudo,103) = '" + DataLaudo + "' and Laudo = '" + Laudo + "' ");

                        if (zLaudos.Id == 0)
                        {
                            //mandar arquivo de teste

                            //zLaudos.Find(432849178);

                             txt_Status = txt_Status + "Erro: Laudo não localizado ( " + CNPJ + " Data: + " + DataLaudo + " +  Tipo: " + Laudo + "  )" + System.Environment.NewLine;
                             xRetorno = "04 (Laudo não localizado (" + CNPJ + ")";
                        }

                        if (txt_Status == "")
                        {
                            string xLink = zLaudos.Path.Trim();

                            // http://www.ilitera.net.br/driveI/laudos/

                            //xLink = xLink.ToUpper().Replace("I:\\LAUDOS\\", "http://www.ilitera.net.br/driveI/laudos/");
                            xLink = xLink.ToUpper().Replace("I:\\LAUDOS\\", "https://www.ilitera.net.br/laudos/");


                            //string xArq = "I:\\temp\\Retorno_Link_Laudos_" + System.DateTime.Now.Year.ToString().Trim() + System.DateTime.Now.Month.ToString().Trim() + System.DateTime.Now.Day.ToString().Trim() + System.DateTime.Now.Hour.ToString().Trim() + System.DateTime.Now.Minute.ToString().Trim() + System.DateTime.Now.Second.ToString().Trim() + ".xml";


                            zRet = GetXmlDocument(
                            new XDocument(
                                 new XElement("Tipo",                                    
                                    new XElement("CNPJ", CNPJ),
                                    new XElement("Laudo", Laudo),
                                    new XElement("Link_Laudo", xLink),
                                    new XElement("Conteudo_Arquivo", "")
                                    )
                                  ));   //.Save(xArq);

                            //enviar XML para endereço de retorno

                        }

                    }
                    else if (TipoRetorno.ToUpper().Trim() == "ARQUIVO")
                    {

                        tbl_Laudos zLaudos = new tbl_Laudos();
                        zLaudos.Find(" nId_Empr = " + rEmpresa.Id.ToString() + " and convert( char(10),Data_Laudo,103) = '" + DataLaudo + "' and Laudo = '" + Laudo + "' ");

                        if (zLaudos.Id == 0)
                        {
                            //mandar arquivo de teste

                            //zLaudos.Find(432849178);
                            

                            txt_Status = txt_Status + "Erro: Laudo não localizado ( " + CNPJ + " Data: + " + DataLaudo + " +  Tipo: " + Laudo + "  )" + System.Environment.NewLine;
                            xRetorno = "04 (Laudo não localizado (" + CNPJ + ")";
                        }

                        if (txt_Status == "")
                        {
                            string xLink = zLaudos.Path.Trim();

                            byte[] arrBytes = File.ReadAllBytes(xLink);
                            string strXml = Convert.ToBase64String(arrBytes);
                            //File.WriteAllText(@"i:\temp\teste_xml.txt", strXml);

                            //string xArq = "I:\\temp\\Retorno_Link_Laudos_" + System.DateTime.Now.Year.ToString().Trim() + System.DateTime.Now.Month.ToString().Trim() + System.DateTime.Now.Day.ToString().Trim() + System.DateTime.Now.Hour.ToString().Trim() + System.DateTime.Now.Minute.ToString().Trim() + System.DateTime.Now.Second.ToString().Trim() + ".xml";

                            zRet = GetXmlDocument(
                            new XDocument(
                                 new XElement("Tipo",                                    
                                    new XElement("CNPJ", CNPJ),
                                    new XElement("Laudo", Laudo),
                                    new XElement("Link_Laudo", ""),
                                    new XElement("Conteudo_Arquivo", strXml)
                                    )
                                  ));//.Save(xArq);

                            //enviar XML para endereço de retorno

                        }




                    }



                    //retornar XML com seguinte estrutura
                    //CNPJ
                    //LAUDO ( PPRA/PCMSO )
                    //Laudo
                    //Link_Laudo
                    //Conteudo_Arquivo  ( PDF do laudo )      Posso mesmo deixar o PDF armazenado no servidor ( em Paradigmas ou diretório específico i:\Laudos ) 
                    //                                        e enviar Link, é mais rápido.  Vou deixar pronto as duas alternativas.
                    //                                        Posso criar processo noturno para criar os laudos nesta pasta,  assim fica mais fácil jogar no XML
                    //                                        Vou precisar de tabela de controle  
                    //                                        tbl_Laudos
                    //                                        Id_Laudo
                    //                                        nId_Chave ( nId_laud_Tec ou IdPCMSO )
                    //                                        Laudo  ( PPRA/PCMSO )
                    //                                        nId_Empr
                    //                                        Data_Laudo
                    //                                        Path  ( nome do arquivo PPRA_ + nId_Empr + _ + YYYYMMDD.pdf    

                    //if xTipo_Retorno == "Link"
                    //if xTipo_Retorno == "Arquivo"




                    //                    select* from
                    //                    (
                    //                       select nId_Laud_Tec as Chave, 'PPRA' as Laudo, nId_Empr, hDt_Laudo as Data_Laudo

                    //                       from tbllaudo_tec

                    //                       where nId_Laud_Tec not in

                    //                       (select nId_Chave from tbl_Laudos where Laudo = 'PPRA' )
                    //   and nId_Pedido in
                    //   (select IdPedido from opsa_prajna_Hom.dbo.pedido where IndStatus = 2 and DataConclusao is not null and DataCancelamento is null )
                    //   and nId_Empr in
                    //   (select IdPessoa from opsa_prajna_hom.dbo.Pessoa where isinativo = 0 )
                    //  union
                    //   select IdPCMSO as Chave, 'PCMSO' as Laudo, IdCliente as nId_Empr, DataPCMSO as Data_Laudo
                    //   from opsa_prajna_hom.dbo.PCMSO as a
                    //   left join opsa_prajna_Hom.dbo.Documento as b on(a.IdDocumento = b.IdDocumento)
                    //   where IdPCMSO not in
                    //   (select nId_Chave from tbl_Laudos where Laudo = 'PCMSO' )
                    //   and IdPedido in
                    //   (select IdPedido from opsa_prajna_Hom.dbo.pedido where IndStatus = 2 and DataConclusao is not null and DataCancelamento is null )
                    //   and IdCliente in
                    //   (select IdPessoa from opsa_prajna_hom.dbo.Pessoa where isinativo = 0 )
                    //) as tx90
                    //where datepart(yyyy, Data_Laudo) >= 2018


                }




                xRetorno = "01 Processamento concluído sem erros";


            }
            catch (Exception ex)
            {
                txt_Status = txt_Status + "Erro: Carga do XML: " + ex.Message + System.Environment.NewLine;

               
            }
            finally
            {

                if (txt_Status != "")
                {
                    //montar XMLDocument com erro
                    string xRet = @"<?xml version=""1.0"" encoding=""UTF-8""?><Erro>" + txt_Status + "</Erro>";

                    zRet.LoadXml(xRet);


                }
               
            }


            return zRet;


        }




        [WebMethod]
        public XmlDocument Executar_Convocacao(string CNPJ, string CodUsuario, string TipoExame, string CPF, string Data, string ID, string xIdClinica)
        {

            Clinica rClinica = new Clinica();
            Empregado rEmpregado = new Empregado();
            Ilitera.Common.Pessoa rEmpresa = new Ilitera.Common.Pessoa();
            int zId = 0;


            string rSelect = "";

            string txt_Status = "";

            string xId = ID;


            //int xAux;
            string xExames = "";
            string xExames2 = "";
            string xExames3 = "";
            string xExames4 = "";
            string xTipo = "";
            string xBasico = "0";
            string xObs = "";
            //int xCont = 0;
            //string xEnvio_Email;

            string xImpDt = "S";

            XmlDocument zRet = new XmlDocument();


            System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");

            //xEnvio_Email = "N";


            try
            {


                rSelect = " dbo.udf_getnumeric( nomecodigo ) = '" + CNPJ + "' and isinativo = 0 ";

                //pegar Id Empresa                    
                rEmpresa.Find(rSelect);

                //se não achar empresa,  emitir retorno avisando
                if (rEmpresa.Id == 0)
                {
                    txt_Status = txt_Status + "Erro: Empresa não localizada ( " + CNPJ + "  )" + System.Environment.NewLine;
                }

                //quando for convocação ( mailing ) no último irá o link para chamar comunicação
                //pode ir com um ID pronto



                int xTipoExame = 4;

                if (txt_Status == "")
                {
                    //pegar Id Colaborador                       
                    rEmpregado.Find(" tNo_CPF='" + CPF + "'  and nId_Empr = " + rEmpresa.Id.ToString());

                    //se não achar empregado,  emitir retorno avisando
                    if (rEmpregado.Id == 0)
                    {
                        txt_Status = txt_Status + "Erro: Empregado não localizado ( " + CPF + "  )" + System.Environment.NewLine;
                    }

                    if (txt_Status == "")
                    {
                        if (rEmpregado.teMail.Trim() == "")
                        {
                            txt_Status = txt_Status + "Erro: Colaborador sem e-mail cadastrado." + System.Environment.NewLine;
                        }
                    }



                    if (txt_Status == "")
                    {

                        rClinica.Find(System.Convert.ToInt32(xIdClinica));

                        //se não achar clinica,  emitir retorno avisando
                        if (rClinica.Id == 0)
                        {
                            txt_Status = txt_Status + "Erro: Clínica não localizada " + System.Environment.NewLine;
                        }

                        if (txt_Status == "")
                        {
                            if (rClinica.Email.Trim() == "")
                            {
                                txt_Status = txt_Status + "Erro: Clínica sem e-mail cadastrado." + System.Environment.NewLine;
                            }
                        }

                    }




                    if (TipoExame.ToUpper().Trim() == "PERIODICO" || TipoExame.ToUpper().Trim() == "PERIÓDICO")
                    {
                        TipoExame = "4";
                        xTipoExame = 4;
                    }
                    else if (TipoExame.ToUpper().Trim() == "DEMISSIONAL")
                    {
                        TipoExame = "2";
                        xTipoExame = 2;
                    }
                    else if (TipoExame.ToUpper().Trim().Substring(0, 5) == "MUDAN")
                    {
                        TipoExame = "3";
                        xTipoExame = 3;
                    }
                    else if (TipoExame.ToUpper().Trim().Substring(0, 7) == "RETORNO")
                    {
                        TipoExame = "5";
                        xTipoExame = 5;
                    }
                    else if (TipoExame.ToUpper().Trim().Substring(0, 6) == "ADMISS")
                    {
                        TipoExame = "1";
                        xTipoExame = 1;
                    }


                    zId = rEmpregado.Id;
                }


                //validar token - deve existir registro com dados do e-mail de convocação



                //código que estaria na outra página
                if (txt_Status.Trim() == "")
                {

                    string xEmail = rClinica.Email.Trim();
                    //xEnvio_Email = "S";






                    Guid strAux = Guid.NewGuid();



                    xObs = rClinica.Observacao;  //  txt_Obs.Text.Trim();


                    //chamar popula para pegar string com exames da guia
                    string lst_Exames = PopularValueListClinicaClienteExameDicionario(rClinica.Id.ToString(), xTipoExame.ToString().Trim(), rEmpresa.Id.ToString(), rEmpregado.Id.ToString(), CodUsuario, Data);


                    if (lst_Exames.Length >= 2)
                    {
                        if (lst_Exames.Substring(0, 2) == "-1")  //retornou erro
                            txt_Status = lst_Exames;
                    }




                    if (txt_Status.Trim() == "")
                    {

                        xExames = lst_Exames;


                        ////exames na guia, carregar 
                        //for (xAux = 0; xAux < lst_Exames.Items.Count; xAux++)
                        //{
                        //    if (lst_Exames.Items[xAux].Selected == true)
                        //    {
                        //        xCont++;

                        //        if (xCont < 6)
                        //        {
                        //            xExames = xExames + " /n " + lst_Exames.Items[xAux].Text;
                        //        }
                        //        else if (xCont < 11)
                        //        {
                        //            xExames2 = xExames2 + " /n " + lst_Exames.Items[xAux].Text;
                        //        }
                        //        else if (xCont < 16)
                        //        {
                        //            xExames3 = xExames3 + " /n " + lst_Exames.Items[xAux].Text;
                        //        }
                        //        else
                        //        {
                        //            xExames4 = xExames4 + " /n " + lst_Exames.Items[xAux].Text;
                        //        }
                        //    }

                    }

                    //if (chk_Basico.Checked == true)
                    //{
                    //    xBasico = "1";
                    //}


                    xTipo = xTipoExame.ToString();




                    ExameBase rexame = new ExameBase();


                    rexame.Find(" IDEMPREGADO = " + rEmpregado.Id.ToString() + " and IdExameDicionario = " + xTipoExame.ToString() + " and convert( char(10),DataExame,103 ) = '" + Data + "' ");



                    if (rexame.Id != 0)
                    {
                        //MsgBox1.Show("Ilitera.Net", "ASO já foi criado para este tipo de exame e data.", null,
                        //new EO.Web.MsgBoxButton("OK"));

                        //TextWriter tw = new StreamWriter(lbl_Arq.Text);
                        //tw.WriteLine("ASO já foi criado para este exame e data");
                        //tw.Close();
                        txt_Status = "ASO já foi criado para este exame e data";

                    }

                }



                if (txt_Status.Trim() == "")
                {
                    Cliente cliente = new Cliente();
                    cliente.Find(System.Convert.ToInt32(rEmpresa.Id.ToString()));


                    string xAptidao = "";








                    //pegar data de planejamento + "|" + Data Ultimo exame
                    string rData = "";
                    Ilitera.Data.Clientes_Funcionarios xPlan2 = new Ilitera.Data.Clientes_Funcionarios();
                    rData = xPlan2.Buscar_Data_Planejamento_Exame_Colaborador(rEmpregado.Id, 4, Data);


                    Ilitera.Data.PPRA_EPI xGuia = new Ilitera.Data.PPRA_EPI();
                    xGuia.Salvar_Dados_Guia_Encaminhamento(rEmpresa.Id, rEmpregado.Id, xTipoExame.ToString(), xExames, Data, "", rClinica.NomeAbreviado, System.Convert.ToInt32(CodUsuario), "N", rData.Substring(0, 10), rData.Substring(11), "");



                    //se demissional da Prajna,  colocar data do ASO na data de demissão de colaborador
                    if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().Trim().ToUpper() == "OPSA")
                    {
                        if (xTipo.ToUpper().Trim() == "D")
                        {
                            //System.Convert.ToInt32(Request["IdEmpregado"].ToString())
                            //txt_Data.Text
                            Ilitera.Data.PPRA_EPI xDem = new Ilitera.Data.PPRA_EPI();
                            xDem.Guia_Demissao_Colaborador_Obras(System.Convert.ToInt32(rEmpregado.Id.ToString()), Data);

                        }
                    }
                    else if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().IndexOf("Prajna") > 0)
                    {
                        if (xTipo.ToUpper().Trim() == "D")
                        {
                            //System.Convert.ToInt32(Request["IdEmpregado"].ToString())
                            //txt_Data.Text
                            Ilitera.Data.PPRA_EPI xDem = new Ilitera.Data.PPRA_EPI();

                            //xDem.Guia_Demissao_Colaborador_Obras(System.Convert.ToInt32(Request["IdEmpregado"].ToString()), txt_Data.Text);                         
                            xDem.Guia_Demissao_Colaborador_Obras(System.Convert.ToInt32(rEmpregado.Id.ToString()), Data);


                        }

                    }





                    // Depois testar criação de complementares - talvez eu possa colocar isso junto com código que adiciona exames em lst_Exames






                    //// criar exames complementares do ASO, se estes não existirem ainda.  Colocar como em espera
                    //if (cliente.Gerar_Complementares_Guia == true)
                    //{


                    //    Ilitera.Opsa.Data.Empregado nEmpregado = new Ilitera.Opsa.Data.Empregado(System.Convert.ToInt32(rEmpregado.Id.ToString()));

                    //    Ilitera.Common.Juridica xClin = new Ilitera.Common.Juridica();
                    //    xClin.Find(" IdJuridica = " + rClinica.Id.ToString() );

                    //    for (int nCont = 0; nCont < lst_IdExames.Items.Count; nCont++)
                    //    {

                    //        //checar se exame já existe
                    //        Int32 xIdExameDicionario = System.Convert.ToInt32(lst_IdExames.Items[nCont].ToString());


                    //        Complementar xCompl = new Complementar();
                    //        xCompl.Find(" IdEmpregado = " + rEmpregado.Id.ToString() + " and IdExameDicionario = " + xIdExameDicionario.ToString() + " and convert( char(10), DataExame, 103 ) = '" + Data + "'");

                    //        if (xCompl.Id == 0)
                    //        {

                    //            ExameDicionario xED = new ExameDicionario();
                    //            xED.Find(" IdExameDicionario = " + xIdExameDicionario.ToString());

                    //            if (xED.Nome.ToUpper().Trim() == "AUDIOMETRIA")
                    //            {
                    //                Audiometria xAud = new Audiometria();
                    //                xAud.IdExameDicionario = xED;
                    //                xAud.IdEmpregado = nEmpregado;
                    //                xAud.DataExame = System.Convert.ToDateTime(Data, ptBr);
                    //                xAud.IndResultado = (int)Ilitera.Opsa.Data.ResultadoExame.EmEspera;
                    //                xAud.IdJuridica = xClin;

                    //                PagamentoClinica xPag = new PagamentoClinica();
                    //                xAud.IdPagamentoClinica = xPag;

                    //                xAud.IndAudiometriaTipo = 0;

                    //                Medico rMedico = new Medico();
                    //                xAud.IdMedico = rMedico;

                    //                ConvocacaoExame xConv = new ConvocacaoExame();
                    //                xAud.IdConvocacaoExame = xConv;

                    //                Audiometro xAudiometro = new Audiometro();
                    //                xAud.IdAudiometro = xAudiometro;

                    //                Ilitera.Common.Compromisso xcompr = new Ilitera.Common.Compromisso();
                    //                xAud.IdCompromisso = xcompr;

                    //                xAud.Save();
                    //            }
                    //            else
                    //            {
                    //                xCompl.IdExameDicionario = xED;
                    //                xCompl.IdEmpregado = nEmpregado;
                    //                xCompl.DataExame = System.Convert.ToDateTime(Data, ptBr);
                    //                xCompl.IndResultado = (int)Ilitera.Opsa.Data.ResultadoExame.EmEspera;
                    //                xCompl.IdJuridica = xClin;
                    //                xCompl.Save();
                    //            }

                    //        }



                    //    }


                    //}




                    //   Response.Redirect("~\\DadosEmpresa\\RelatorioGuiaASO_Auto.aspx?IliteraSystem=" + strAux.ToString().Substring(0, 5)
                    //   + "&IdEmpresa=" + Request["IdEmpresa"] + "&IdEmpregado=" + Request["IdEmpregado"] + "&E1=" + xExames + "&E2=" + xExames2 + "&E3=" + xExames3 + "&E4=" + xExames4 + "&IdClinica=" + cmb_Clinicas.SelectedValue.ToString() + "&H_E=" + txt_Hora.Text + "&D_E=" + txt_Data.Text + "&Tipo=" + xTipo + "&Basico=" + xBasico + "&Mail=" + xEnvio_Email + "&ImpDt=" + xImpDt + "&Apt=" + xAptidao + "&CodUsuario=" + Request["IdUsuario"].ToString().Trim() + "&xId=" + Request["xId"].ToString().Trim() + "&xIdExame=" + xIdExame + "&xArq=" + Request["xArq"].ToString().Trim());



                    //parte de geração das guias

                    string xId_Empregado;
                    string xId_Empresa;
                    string xId_Clinica;

                    string xData_Exame;
                    string xHora_Exame;

                    string xApt;
                    string xDtDemissao;
                    //string xID;
                    string xId_Exame;



                    //InicializaWebPageObjects();
                    //Page.Response.Cache.SetCacheability(HttpCacheability.NoCache);





                    xId_Empregado = rEmpregado.Id.ToString();
                    xId_Empresa = rEmpresa.Id.ToString();
                    xId_Clinica = rClinica.Id.ToString();
                    xData_Exame = Data;
                    xHora_Exame = "";




                    xApt = xAptidao;

                    xId_Exame = TipoExame;


                    xDtDemissao = "";




                    Ilitera.Opsa.Data.Empregado empregado = new Ilitera.Opsa.Data.Empregado();


                    //criando ASO
                    xTipoExame = System.Convert.ToInt16(xTipo);



                    Cliente zCliente = new Cliente();
                    zCliente.Find(System.Convert.ToInt32(xId_Empresa));



                    ExameClinicoFacade exame = new ExameClinicoFacade();

                    exame.Prontuario = "";
                    //exame.Observacao = "";

                    empregado.Find(" tblEMPREGADO.nID_EMPREGADO = " + rEmpregado.Id.ToString());

                    exame.IdEmpregado = empregado;

                    exame.IdEmpregadoFuncao = EmpregadoFuncao.GetEmpregadoFuncao(empregado);
                    exame.IdPcmso = exame.IdEmpregadoFuncao.nID_EMPR.GetUltimoPcmso();

                    exame.apt_Espaco_Confinado = "";
                    exame.apt_Trabalho_Altura = "";
                    exame.apt_Transporte = "";
                    exame.apt_Submerso = "";
                    exame.apt_Eletricidade = "";
                    exame.apt_Alimento = "";
                    exame.apt_Brigadista = "";
                    exame.apt_Socorrista = "";



                    ExameDicionario xExameDicionario = new ExameDicionario();

                    xExameDicionario.Find(xTipoExame);

                    Juridica xJuridica = new Juridica();
                    xJuridica.Find(Convert.ToInt32(rClinica.Id));

                    exame.IdExameDicionario = xExameDicionario;
                    exame.IdJuridica = xJuridica;

                    //exame.IdExameDicionario.Find( xTipoExame );

                    //exame.IdJuridica.Find(Convert.ToInt32(Request["IdClinica"].ToString()));


                    exame.DataExame = System.Convert.ToDateTime(xData_Exame, ptBr);

                    if (xTipoExame == 2 && xDtDemissao.Trim() != "") exame.DataDemissao = System.Convert.ToDateTime(xDtDemissao, ptBr);

                    exame.IndResultado = 3;


                    Medico xMedico = new Medico();
                    xMedico.Find(1111);  // -2133369037);

                    exame.IdMedico = xMedico;

                    //Usuario xusuario = new Usuario();
                    //xusuario.Find(System.Convert.ToInt32(Request["IdUsuario"].ToString()));

                    //exame.UsuarioId = 


                    //Entities.Usuario usuario = (Entities.Usuario)Session["usuarioLogado"];

                    Entities.Usuario usuario = new Entities.Usuario();
                    usuario.IdUsuario = System.Convert.ToInt32(System.Convert.ToInt32(CodUsuario));


                    int zStatus = 0;
                    try
                    {
                        zStatus = exame.Save(System.Convert.ToInt32(System.Convert.ToInt32(CodUsuario)));
                    }
                    catch (Exception ex)
                    {
                        if (ex.Message.ToUpper() != "MÉTODO NÃO-ESTÁTICO REQUER UM DESTINO." && ex.Message.ToUpper() != "NON-STATIC METHOD REQUIRES A TARGET.")
                            throw new Exception(ex.Message.ToString());


                    }

                    //exame.Save();

                    //if ( cliente.Gerar_Complementares_Guia==true)  // criar exames complementares do ASO, se estes não existirem ainda.  Colocar como em espera
                    //{






                    //}




                    Clinico exame2 = new Clinico();
                    exame2.Find(exame.Id);


                    exame2.apt_Trabalho_Altura2 = "0";
                    exame2.apt_Espaco_Confinado2 = "0";
                    exame2.apt_Transporte2 = "0";
                    exame2.apt_Submerso2 = "0";
                    exame2.apt_Eletricidade2 = "0";
                    exame2.apt_Aquaviario2 = "0";
                    exame2.apt_Alimento2 = "0";
                    exame2.apt_Brigadista2 = "0";
                    exame2.apt_Socorrista2 = "0";


                    if (xApt.IndexOf("A") >= 0) exame2.apt_Trabalho_Altura2 = "1";
                    if (xApt.IndexOf("C") >= 0) exame2.apt_Espaco_Confinado2 = "1";
                    if (xApt.IndexOf("T") >= 0) exame2.apt_Transporte2 = "1";
                    if (xApt.IndexOf("S") >= 0) exame2.apt_Submerso2 = "1";
                    if (xApt.IndexOf("E") >= 0) exame2.apt_Eletricidade2 = "1";
                    if (xApt.IndexOf("Q") >= 0) exame2.apt_Aquaviario2 = "1";
                    if (xApt.IndexOf("M") >= 0) exame2.apt_Alimento2 = "1";
                    if (xApt.IndexOf("B") >= 0) exame2.apt_Brigadista2 = "1";
                    if (xApt.IndexOf("R") >= 0) exame2.apt_Socorrista2 = "1";






                    Int16 zTamanho = 3;

                    exame2.IdEmpregado.Find();
                    exame2.IdEmpregado.nID_EMPR.Find();
                    exame2.IdEmpregado.nID_EMPR.IdGrupoEmpresa.Find();

                    if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("PRAJNA") > 0)//exame2.IdEmpregado.nID_EMPR.IdGrupoEmpresa.Descricao.ToUpper().Trim() == "CAPGEMINI"  )  // && Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("_HOM") > 0)  //.Id == -905238295)  // Capgemini
                    {
                        zTamanho = 4;
                    }


                    //string xTitulo = "Kit Guia/ASO/PCI - Convocação"; ;

                    //if (xId_Exame != "4")  //apenas guia de complementar
                    //{
                    //    zTamanho = 1;
                    //    xTitulo = "Kit Guia Complementar - Convocação";
                    //}


                    CrystalDecisions.CrystalReports.Engine.ReportClass[] reports = new ReportClass[zTamanho];

                    //Cliente zCliente = new Cliente();
                    //zCliente.Find(System.Convert.ToInt32(xId_Empresa));





                    //se for apenas guia de encaminhamento - convocação exames complementares

                    exame2.IdEmpregadoFuncao.Find();


                    if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().IndexOf("Prajna") > 0)
                    {
                        //RptGuia_Prajna report0 = new DataSourceGuia_Prajna(cliente, System.Convert.ToInt32(xId_Empregado), System.Convert.ToInt32(xId_Empresa), System.Convert.ToInt32(xId_Clinica), xExames, xExames2, xExames3, xExames4, xData_Exame, xHora_Exame, xTipo, xBasico, xObs, xImpDt).GetReport();
                        RptGuia_Nova_Prajna report0 = new DataSourceGuia_Prajna(cliente, System.Convert.ToInt32(xId_Empregado), System.Convert.ToInt32(xId_Empresa), System.Convert.ToInt32(xId_Clinica), xExames, xExames2, xExames3, xExames4, xData_Exame, xHora_Exame, xTipo, xBasico, xObs, xImpDt, exame2.IdEmpregadoFuncao.Id).GetReport_Nova();
                        //CreatePDFDocument(report, this.Response);
                        reports[0] = report0;
                    }
                    else
                    {
                        //RptGuia report0 = new DataSourceGuia(cliente, System.Convert.ToInt32(xId_Empregado), System.Convert.ToInt32(xId_Empresa), System.Convert.ToInt32(xId_Clinica), xExames, xExames2, xExames3, xExames4, xData_Exame, xHora_Exame, xTipo, xBasico, xObs, xImpDt).GetReport();
                        RptGuia_Nova report0 = new DataSourceGuia(cliente, System.Convert.ToInt32(xId_Empregado), System.Convert.ToInt32(xId_Empresa), System.Convert.ToInt32(xId_Clinica), xExames, xExames2, xExames3, xExames4, xData_Exame, xHora_Exame, xTipo, xBasico, xObs, xImpDt, exame2.IdEmpregadoFuncao.Id).GetReport_Nova();
                        //CreatePDFDocument(report, this.Response);
                        reports[0] = report0;
                    }










                    RptAso report2 = new DataSourceExameAsoPci(exame2).GetReport();

                    //tenho o ID do ASO para colocar no registro da guia gerada ?
                    //dar um update ?




                    //Funcionalidade funcionalidade = Funcionalidade.GetFuncionalidade(FuncionalidadeTipo.Relatorio, report.GetType());
                    //ImpressaoDocUsuario.InsereRegistro(usuario, cliente, Convert.ToInt64(report.SummaryInfo.KeywordsInReport), true, funcionalidade);

                    //CreatePDFDocument(report, this.Response);


                    //Ilitera.Opsa.Data.Empregado empregado = new Ilitera.Opsa.Data.Empregado();

                    //Clinico exame2 = new Clinico(Convert.ToInt32(Request["IdExame"]));

                    reports[1] = report2;

                    Juridica xClin = new Juridica();
                    xClin.Find(rClinica.Id);

                    string xClinNome = "";

                    if (xClin != null) xClinNome = xClin.NomeAbreviado.ToUpper().Trim();


                    if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().IndexOf("Daiti") > 0 || Ilitera.Data.SQLServer.EntitySQLServer.xDB2.Trim() == "Sied_Novo" || xClinNome.IndexOf("DAITI") >= 0 || xClinNome.IndexOf("IPATINGA") >= 0)
                    {
                        RptPci report3 = new DataSourceExameAsoPci(exame2).GetReportPci_Antigo();
                        reports[2] = report3;
                    }
                    else
                    {

                        exame2.IdEmpregado.Find();
                        exame2.IdEmpregado.nID_EMPR.Find();
                        exame2.IdEmpregado.nID_EMPR.IdGrupoEmpresa.Find();

                        //if (exame.IdEmpregado.nID_EMPR.IdGrupoEmpresa.Descricao.ToUpper().Trim() == "CAPGEMINI" && Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("_HOM") > 0)  //.Id == -905238295)  // Capgemini

                        if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("PRAJNA") > 0) //  && exame2.IdEmpregado.nID_EMPR.IdGrupoEmpresa.Descricao.ToUpper().Trim() == "CAPGEMINI")  // && Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("_HOM") > 0)  //.Id == -905238295)  // Capgemini
                        {
                            RptPci_Novo_Capgemini report3 = new DataSourceExameAsoPci(exame2).GetReportPciCapgemini();
                            reports[2] = report3;
                        }
                        else
                        {
                            RptPci_Novo report3 = new DataSourceExameAsoPci(exame2).GetReportPci();
                            reports[2] = report3;
                        }
                    }
                    //RptPci report3 = new DataSourceExameAsoPci(exame2).GetReportPci();


                    exame2.IdEmpregado.Find();
                    exame2.IdEmpregado.nID_EMPR.Find();
                    exame2.IdEmpregado.nID_EMPR.IdGrupoEmpresa.Find();


                    if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("PRAJNA") > 0) //exame2.IdEmpregado.nID_EMPR.IdGrupoEmpresa.Descricao.ToUpper().Trim() == "CAPGEMINI"  ) // && Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("_HOM") > 0)  //.Id == -905238295)  // Capgemini
                    {
                        Int32 xIdExame = exame2.Id;

                        Carregar_Dados_Anamnese_Exame(exame2.Id);

                        RptAnamnese report4 = new DataSourceExameAnamnese(xIdExame, true).GetReport();
                        reports[3] = report4;

                    }






                    //CreatePDFMerged(reports, this.Response, "", false, xID);

                    //HttpResponse response = this.Response;
                    string watermark = "";
                    bool RenumerarPaginas = false;



                    Stream[] streams = new Stream[reports.Length];

                    int i = 0;



                    foreach (CrystalDecisions.CrystalReports.Engine.ReportClass report in reports)
                    {
                        if (RenumerarPaginas)
                            report.ReportDefinition.ReportObjects["PaginaNdeN1"].ObjectFormat.EnableSuppress = true;

                        streams[i] = report.ExportToStream(ExportFormatType.PortableDocFormat);

                        report.Close();

                        i++;
                    }


                    //CreatePDFMerged(streams, response, watermark, RenumerarPaginas, xId);





                    MemoryStream reportStream = PdfSharpUtility.MergeReports(streams, string.Empty, watermark, RenumerarPaginas);



                    string xPath = "I:\\temp\\guia_" + xId.Trim() + ".pdf";

                    //if (xEnvio_Email == "S")
                    //{

                    //if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().IndexOf("Prajna") > 0)
                    //{




                    FileStream fileStream = File.Create(xPath, (int)reportStream.Length);
                    // Initialize the bytes array with the stream length and then fill it with data
                    byte[] bytesInStream = new byte[reportStream.Length];
                    reportStream.Read(bytesInStream, 0, bytesInStream.Length);
                    // Use write method to write to the file specified above
                    fileStream.Write(bytesInStream, 0, bytesInStream.Length);

                    fileStream.Flush();

                    fileStream.Dispose();
                    fileStream = null;


                    //retornar o ASO criado

                    string xLink = xPath;

                    byte[] arrBytes = File.ReadAllBytes(xLink);
                    string strXml = Convert.ToBase64String(arrBytes);
                    //File.WriteAllText(@"i:\temp\teste_xml.txt", strXml);

                    //string xArq = "I:\\temp\\Retorno_Link_Laudos_" + System.DateTime.Now.Year.ToString().Trim() + System.DateTime.Now.Month.ToString().Trim() + System.DateTime.Now.Day.ToString().Trim() + System.DateTime.Now.Hour.ToString().Trim() + System.DateTime.Now.Minute.ToString().Trim() + System.DateTime.Now.Second.ToString().Trim() + ".xml";

                    zRet = GetXmlDocument(
                    new XDocument(
                         new XElement(
                            new XElement("Conteudo_Arquivo", strXml)
                            )
                          ));//.Save(xArq);








                    Clinica xClinica = new Clinica(exame.IdJuridica.Id);

                    Cliente xCliente = new Cliente(exame.IdEmpregado.nID_EMPR.Id);
                    string xEmpresa = "";

                    if (xCliente.IdJuridicaPai.Id != 0 && xCliente.IdJuridicaPai.Id != xCliente.Id)
                    {
                        if (xCliente.IdJuridicaPai.ToString().ToUpper().IndexOf("KNOX") > 0)
                        {
                            xEmpresa = xCliente.IdJuridicaPai.ToString();
                        }
                        else
                        {
                            xEmpresa = xCliente.IdJuridicaPai.ToString() + "   Unidade: " + xCliente.NomeAbreviado.ToString();
                        }
                    }
                    else
                    {
                        xEmpresa = xCliente.GetNomeEmpresa();
                    }



                    string xCorpo = "<p style='font-family:Tahoma;font-size:18px;text-align:center;text-decoration:underline;color:black> </p>" +
                                     "<p style='font-family:Tahoma;font-size:18px;text-align:center;text-decoration:underline;color:black> DADOS DO AGENDAMENTO: </p><br>" +
                                     "<p><font size='3' face='Tahoma'>Nome: " + exame.IdEmpregado.tNO_EMPG + "<br>" +
                                     "RG: " + exame.IdEmpregado.tNO_IDENTIDADE.ToString() + "<br>" +
                                     "Data de Nascimento: " + exame.IdEmpregado.hDT_NASC.ToString("dd/MM/yyyy") + "<br><br>" +
                                     "Empresa:  " + xEmpresa + "<br>" +
                                     "CNPJ: " + exame.IdEmpregado.nID_EMPR.GetCnpj() + "<br>" +
                                     "Cargo: " + exame.IdEmpregadoFuncao.nID_FUNCAO.ToString() + "<br>" +
                                      "Setor: " + exame.IdEmpregadoFuncao.nID_SETOR.ToString() + "<br>" +
                                      "Unidade de Trabalho: " + exame.IdEmpregadoFuncao.GetLocalDeTrabalho(exame.IdEmpregado.nID_EMPR) + "<br>" +
                                      "Clinica: " + exame.IdJuridica.NomeAbreviado + "<br>" +
                                      "Endereço da clinica: " + xClinica.GetEndereco().IdTipoLogradouro.NomeCompleto + " " + xClinica.GetEndereco().Logradouro + " " + xClinica.GetEndereco().Numero + " " + xClinica.GetEndereco().Bairro + " " + xClinica.GetEndereco().Municipio + "/" + xClinica.GetEndereco().Uf + "   Fone: " + exame.IdJuridica.GetContatoTelefonico() + "<br>" +
                                      "Tipo de Exame: " + exame.IdExameDicionario.Nome + " <br>" +
                                      "Data do Exame: " + exame.DataExame.ToString("dd/MM/yyyy") + " </font></p></body>";


                    //string xCorpo = "<p style='font-family:Tahoma;font-size:18px;text-align:center;text-decoration:underline;color:black> </p>" +
                    //                 "<p style='font-family:Tahoma;font-size:18px;text-align:center;text-decoration:underline;color:black> DADOS DO AGENDAMENTO: </p><br>" +
                    //                 "<p><font size='3' face='Tahoma'>Nome: " + exame.IdEmpregado.tNO_EMPG + "<br>" +                             
                    //                  "Data do Exame: " + exame.DataExame.ToString("dd/MM/yyyy") + " </font></p></body>";




                    string zEmail;
                    string zEmail_Copia;


                    zEmail = rEmpregado.teMail.Trim();

                    zEmail_Copia = rClinica.Email.Trim();

                    if (rEmpregado.teMail_Resp.Trim() != "")
                    {
                        zEmail_Copia = zEmail_Copia + ";" + rEmpregado.teMail_Resp.Trim();
                    }



                    Envio_Email_Prajna(zEmail, zEmail_Copia, "Guia de Encaminhamento", xCorpo, xPath, "Guia / ASO / PCI", exame.IdEmpregado.Id, exame.IdEmpregado.nID_EMPR.Id, exame, CodUsuario, rEmpresa.Id);





                }



            }
            catch (Exception ex)
            {
                txt_Status = txt_Status + "Erro: Carga do XML: " + ex.Message + System.Environment.NewLine;


            }
            finally
            {

                if (txt_Status != "")
                {
                    //montar XMLDocument com erro
                    string xRet = @"<?xml version=""1.0"" encoding=""UTF-8""?><Erro>" + txt_Status + "</Erro>";

                    zRet.LoadXml(xRet);


                }

            }


            return zRet;

        }








        [WebMethod]
        public XmlDocument Retornar_ASO(string CNPJ, string CodUsuario, string TipoExame, string CPF, string Data, string ID, string xIdClinica)
        {

            Clinica rClinica = new Clinica();
            Empregado rEmpregado = new Empregado();
            Ilitera.Common.Pessoa rEmpresa = new Ilitera.Common.Pessoa();
            int zId = 0;

            XmlDocument zRet = new XmlDocument();

            string rSelect = "";

            string txt_Status = "";

            string xId = ID;


            //int xAux;
            string xExames = "";
            //string xExames2 = "";
            //string xExames3 = "";
            //string xExames4 = "";
            string xTipo = "";
            //string xBasico = "0";
            string xObs = "";
            //int xCont = 0;
            //string xEnvio_Email = "N";

            //string xImpDt = "S";

            Int32 rExame_Existe = 0;


            System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");



            try
            {


                rSelect = " dbo.udf_getnumeric( nomecodigo ) = '" + CNPJ + "' ";

                //pegar Id Empresa                    
                rEmpresa.Find(rSelect);

                //se não achar empresa,  emitir retorno avisando
                if (rEmpresa.Id == 0)
                {
                    txt_Status = txt_Status + "Erro: Empresa não localizada ( " + CNPJ + "  )" + System.Environment.NewLine;
                }

                //quando for convocação ( mailing ) no último irá o link para chamar comunicação
                //pode ir com um ID pronto



                int xTipoExame = 4;

                if (txt_Status == "")
                {
                    //pegar Id Colaborador                       
                    rEmpregado.Find(" tNo_CPF='" + CPF + "'  and nId_Empr = " + rEmpresa.Id.ToString());
                    
                    //se não achar empregado,  emitir retorno avisando
                    if (rEmpregado.Id == 0)
                    {
                        txt_Status = txt_Status + "Erro: Empregado não localizado ( " + CPF + "  )" + System.Environment.NewLine;
                    }

                    if (txt_Status == "")
                    {
                        if (rEmpregado.teMail.Trim() == "")
                        {
                            txt_Status = txt_Status + "Erro: Colaborador sem e-mail cadastrado." + System.Environment.NewLine;
                        }
                    }



                    if (txt_Status == "")
                    {

                        rClinica.Find(System.Convert.ToInt32(xIdClinica));

                        //se não achar clinica,  emitir retorno avisando
                        if (rClinica.Id == 0)
                        {
                            txt_Status = txt_Status + "Erro: Clínica não localizada " + System.Environment.NewLine;
                        }

                        if (txt_Status == "")
                        {
                            if (rClinica.Email.Trim() == "")
                            {
                                txt_Status = txt_Status + "Erro: Clínica sem e-mail cadastrado." + System.Environment.NewLine;
                            }
                        }

                    }




                    if (TipoExame.ToUpper().Trim() == "PERIODICO" || TipoExame.ToUpper().Trim() == "PERIÓDICO")
                    {
                        TipoExame = "4";
                        xTipoExame = 4;
                    }
                    else if (TipoExame.ToUpper().Trim() == "DEMISSIONAL")
                    {
                        TipoExame = "2";
                        xTipoExame = 2;
                    }
                    else if (TipoExame.ToUpper().Trim().Substring(0, 5) == "MUDAN")
                    {
                        TipoExame = "3";
                        xTipoExame = 3;
                    }
                    else if (TipoExame.ToUpper().Trim().Substring(0, 7) == "RETORNO")
                    {
                        TipoExame = "5";
                        xTipoExame = 5;
                    }
                    else if (TipoExame.ToUpper().Trim().Substring(0, 6) == "ADMISS")
                    {
                        TipoExame = "1";
                        xTipoExame = 1;
                    }


                    zId = rEmpregado.Id;
                }


                //validar token - deve existir registro com dados do e-mail de convocação



                //código que estaria na outra página
                if (txt_Status.Trim() == "")
                {

                    string xEmail = rClinica.Email.Trim();
                    //xEnvio_Email = "S";






                    Guid strAux = Guid.NewGuid();



                    xObs = rClinica.Observacao;  //  txt_Obs.Text.Trim();


                    //chamar popula para pegar string com exames da guia
                    string lst_Exames = PopularValueListClinicaClienteExameDicionario(rClinica.Id.ToString(), xTipoExame.ToString().Trim(), rEmpresa.Id.ToString(), rEmpregado.Id.ToString(), CodUsuario, Data);


                    if (lst_Exames.Length >= 2)
                    {
                        if (lst_Exames.Substring(0, 2) == "-1")  //retornou erro
                            txt_Status = lst_Exames;
                    }




                    if (txt_Status.Trim() == "")
                    {

                        xExames = lst_Exames;


                        ////exames na guia, carregar 
                        //for (xAux = 0; xAux < lst_Exames.Items.Count; xAux++)
                        //{
                        //    if (lst_Exames.Items[xAux].Selected == true)
                        //    {
                        //        xCont++;

                        //        if (xCont < 6)
                        //        {
                        //            xExames = xExames + " /n " + lst_Exames.Items[xAux].Text;
                        //        }
                        //        else if (xCont < 11)
                        //        {
                        //            xExames2 = xExames2 + " /n " + lst_Exames.Items[xAux].Text;
                        //        }
                        //        else if (xCont < 16)
                        //        {
                        //            xExames3 = xExames3 + " /n " + lst_Exames.Items[xAux].Text;
                        //        }
                        //        else
                        //        {
                        //            xExames4 = xExames4 + " /n " + lst_Exames.Items[xAux].Text;
                        //        }
                        //    }

                    }

                    //if (chk_Basico.Checked == true)
                    //{
                    //    xBasico = "1";
                    //}


                    xTipo = xTipoExame.ToString();

                    


                    ExameBase rexame = new ExameBase();


                    rexame.Find(" IDEMPREGADO = " + rEmpregado.Id.ToString() + " and IdExameDicionario = " + xTipoExame.ToString() + " and convert( char(10),DataExame,103 ) = '" + Data + "' ");



                    if (rexame.Id != 0)
                    {

                        rExame_Existe = rexame.Id;

                    }
                    else
                    {
                        txt_Status = "Não há exame criado para geração desse ASO.";
                    }

                }







                if (txt_Status.Trim() == "")
                {
                    Cliente cliente = new Cliente();
                    cliente.Find(System.Convert.ToInt32(rEmpresa.Id.ToString()));


                    string xAptidao = "";
                    


                    //pegar data de planejamento + "|" + Data Ultimo exame
                    string rData = "";
                    Ilitera.Data.Clientes_Funcionarios xPlan2 = new Ilitera.Data.Clientes_Funcionarios();
                    rData = xPlan2.Buscar_Data_Planejamento_Exame_Colaborador(rEmpregado.Id, 4, Data);


                   // Ilitera.Data.PPRA_EPI xGuia = new Ilitera.Data.PPRA_EPI();
                    //xGuia.Salvar_Dados_Guia_Encaminhamento(rEmpresa.Id, rEmpregado.Id, xTipoExame.ToString(), xExames, Data, "", rClinica.NomeAbreviado, System.Convert.ToInt32(CodUsuario), "N", rData.Substring(0, 10), rData.Substring(11), "");



                    //se demissional da Prajna,  colocar data do ASO na data de demissão de colaborador
                    if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().Trim().ToUpper() == "OPSA")
                    {
                        if (xTipo.ToUpper().Trim() == "D")
                        {
                            //System.Convert.ToInt32(Request["IdEmpregado"].ToString())
                            //txt_Data.Text
                            Ilitera.Data.PPRA_EPI xDem = new Ilitera.Data.PPRA_EPI();
                            xDem.Guia_Demissao_Colaborador_Obras(System.Convert.ToInt32(rEmpregado.Id.ToString()), Data);

                        }
                    }
                    else if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().IndexOf("Prajna") > 0)
                    {
                        if (xTipo.ToUpper().Trim() == "D")
                        {
                            //System.Convert.ToInt32(Request["IdEmpregado"].ToString())
                            //txt_Data.Text
                            Ilitera.Data.PPRA_EPI xDem = new Ilitera.Data.PPRA_EPI();

                            //xDem.Guia_Demissao_Colaborador_Obras(System.Convert.ToInt32(Request["IdEmpregado"].ToString()), txt_Data.Text);                         
                            xDem.Guia_Demissao_Colaborador_Obras(System.Convert.ToInt32(rEmpregado.Id.ToString()), Data);


                        }

                    }





                    // Depois testar criação de complementares - talvez eu possa colocar isso junto com código que adiciona exames em lst_Exames






                    //// criar exames complementares do ASO, se estes não existirem ainda.  Colocar como em espera
                    //if (cliente.Gerar_Complementares_Guia == true)
                    //{


                    //    Ilitera.Opsa.Data.Empregado nEmpregado = new Ilitera.Opsa.Data.Empregado(System.Convert.ToInt32(rEmpregado.Id.ToString()));

                    //    Ilitera.Common.Juridica xClin = new Ilitera.Common.Juridica();
                    //    xClin.Find(" IdJuridica = " + rClinica.Id.ToString() );

                    //    for (int nCont = 0; nCont < lst_IdExames.Items.Count; nCont++)
                    //    {

                    //        //checar se exame já existe
                    //        Int32 xIdExameDicionario = System.Convert.ToInt32(lst_IdExames.Items[nCont].ToString());


                    //        Complementar xCompl = new Complementar();
                    //        xCompl.Find(" IdEmpregado = " + rEmpregado.Id.ToString() + " and IdExameDicionario = " + xIdExameDicionario.ToString() + " and convert( char(10), DataExame, 103 ) = '" + Data + "'");

                    //        if (xCompl.Id == 0)
                    //        {

                    //            ExameDicionario xED = new ExameDicionario();
                    //            xED.Find(" IdExameDicionario = " + xIdExameDicionario.ToString());

                    //            if (xED.Nome.ToUpper().Trim() == "AUDIOMETRIA")
                    //            {
                    //                Audiometria xAud = new Audiometria();
                    //                xAud.IdExameDicionario = xED;
                    //                xAud.IdEmpregado = nEmpregado;
                    //                xAud.DataExame = System.Convert.ToDateTime(Data, ptBr);
                    //                xAud.IndResultado = (int)Ilitera.Opsa.Data.ResultadoExame.EmEspera;
                    //                xAud.IdJuridica = xClin;

                    //                PagamentoClinica xPag = new PagamentoClinica();
                    //                xAud.IdPagamentoClinica = xPag;

                    //                xAud.IndAudiometriaTipo = 0;

                    //                Medico rMedico = new Medico();
                    //                xAud.IdMedico = rMedico;

                    //                ConvocacaoExame xConv = new ConvocacaoExame();
                    //                xAud.IdConvocacaoExame = xConv;

                    //                Audiometro xAudiometro = new Audiometro();
                    //                xAud.IdAudiometro = xAudiometro;

                    //                Ilitera.Common.Compromisso xcompr = new Ilitera.Common.Compromisso();
                    //                xAud.IdCompromisso = xcompr;

                    //                xAud.Save();
                    //            }
                    //            else
                    //            {
                    //                xCompl.IdExameDicionario = xED;
                    //                xCompl.IdEmpregado = nEmpregado;
                    //                xCompl.DataExame = System.Convert.ToDateTime(Data, ptBr);
                    //                xCompl.IndResultado = (int)Ilitera.Opsa.Data.ResultadoExame.EmEspera;
                    //                xCompl.IdJuridica = xClin;
                    //                xCompl.Save();
                    //            }

                    //        }



                    //    }


                    //}




                    //   Response.Redirect("~\\DadosEmpresa\\RelatorioGuiaASO_Auto.aspx?IliteraSystem=" + strAux.ToString().Substring(0, 5)
                    //   + "&IdEmpresa=" + Request["IdEmpresa"] + "&IdEmpregado=" + Request["IdEmpregado"] + "&E1=" + xExames + "&E2=" + xExames2 + "&E3=" + xExames3 + "&E4=" + xExames4 + "&IdClinica=" + cmb_Clinicas.SelectedValue.ToString() + "&H_E=" + txt_Hora.Text + "&D_E=" + txt_Data.Text + "&Tipo=" + xTipo + "&Basico=" + xBasico + "&Mail=" + xEnvio_Email + "&ImpDt=" + xImpDt + "&Apt=" + xAptidao + "&CodUsuario=" + Request["IdUsuario"].ToString().Trim() + "&xId=" + Request["xId"].ToString().Trim() + "&xIdExame=" + xIdExame + "&xArq=" + Request["xArq"].ToString().Trim());



                    //parte de geração das guias

                    string xId_Empregado;
                    string xId_Empresa;
                    string xId_Clinica;

                    string xData_Exame;
                    //string xHora_Exame;

                    string xApt;
                    string xDtDemissao;
                    //string xID;
                    string xId_Exame;



                    //InicializaWebPageObjects();
                    //Page.Response.Cache.SetCacheability(HttpCacheability.NoCache);





                    xId_Empregado = rEmpregado.Id.ToString();
                    xId_Empresa = rEmpresa.Id.ToString();
                    xId_Clinica = rClinica.Id.ToString();
                    xData_Exame = Data;
                    //xHora_Exame = "";




                    xApt = xAptidao;

                    xId_Exame = TipoExame;


                    xDtDemissao = "";




                    Ilitera.Opsa.Data.Empregado empregado = new Ilitera.Opsa.Data.Empregado();


                    //criando ASO
                    xTipoExame = System.Convert.ToInt16(xTipo);



                    Cliente zCliente = new Cliente();
                    zCliente.Find(System.Convert.ToInt32(xId_Empresa));


                    ExameClinicoFacade exame = new ExameClinicoFacade();


                    if (rExame_Existe == 0)
                    {
                                                

                        exame.Prontuario = "";
                        //exame.Observacao = "";

                        empregado.Find(" tblEMPREGADO.nID_EMPREGADO = " + rEmpregado.Id.ToString());

                        exame.IdEmpregado = empregado;

                        exame.IdEmpregadoFuncao = EmpregadoFuncao.GetEmpregadoFuncao(empregado);
                        exame.IdPcmso = exame.IdEmpregadoFuncao.nID_EMPR.GetUltimoPcmso();

                        exame.apt_Espaco_Confinado = "";
                        exame.apt_Trabalho_Altura = "";
                        exame.apt_Transporte = "";
                        exame.apt_Submerso = "";
                        exame.apt_Eletricidade = "";
                        exame.apt_Alimento = "";
                        exame.apt_Brigadista = "";
                        exame.apt_Socorrista = "";



                        ExameDicionario xExameDicionario = new ExameDicionario();

                        xExameDicionario.Find(xTipoExame);

                        Juridica xJuridica = new Juridica();
                        xJuridica.Find(Convert.ToInt32(rClinica.Id));

                        exame.IdExameDicionario = xExameDicionario;
                        exame.IdJuridica = xJuridica;

                        //exame.IdExameDicionario.Find( xTipoExame );

                        //exame.IdJuridica.Find(Convert.ToInt32(Request["IdClinica"].ToString()));


                        exame.DataExame = System.Convert.ToDateTime(xData_Exame, ptBr);

                        if (xTipoExame == 2 && xDtDemissao.Trim() != "") exame.DataDemissao = System.Convert.ToDateTime(xDtDemissao, ptBr);

                        exame.IndResultado = 3;


                        Medico xMedico = new Medico();
                        xMedico.Find(1111);  // -2133369037);

                        exame.IdMedico = xMedico;

                        //Usuario xusuario = new Usuario();
                        //xusuario.Find(System.Convert.ToInt32(Request["IdUsuario"].ToString()));

                        //exame.UsuarioId = 


                        //Entities.Usuario usuario = (Entities.Usuario)Session["usuarioLogado"];

                        Entities.Usuario usuario = new Entities.Usuario();
                        usuario.IdUsuario = System.Convert.ToInt32(System.Convert.ToInt32(CodUsuario));


                        int zStatus = 0;
                        try
                        {
                            zStatus = exame.Save(System.Convert.ToInt32(System.Convert.ToInt32(CodUsuario)));
                        }
                        catch (Exception ex)
                        {
                            if (ex.Message.ToUpper() != "MÉTODO NÃO-ESTÁTICO REQUER UM DESTINO." && ex.Message.ToUpper() != "NON-STATIC METHOD REQUIRES A TARGET.")
                                throw new Exception(ex.Message.ToString());


                        }

                        //exame.Save();

                        //if ( cliente.Gerar_Complementares_Guia==true)  // criar exames complementares do ASO, se estes não existirem ainda.  Colocar como em espera
                        //{






                        //}


                    }
                    else
                    {

                        exame.Find(rExame_Existe);


                    }




                    Clinico exame2 = new Clinico();
                    exame2.Find(exame.Id);


                    exame2.apt_Trabalho_Altura2 = "0";
                    exame2.apt_Espaco_Confinado2 = "0";
                    exame2.apt_Transporte2 = "0";
                    exame2.apt_Submerso2 = "0";
                    exame2.apt_Eletricidade2 = "0";
                    exame2.apt_Aquaviario2 = "0";
                    exame2.apt_Alimento2 = "0";
                    exame2.apt_Brigadista2 = "0";
                    exame2.apt_Socorrista2 = "0";


                    if (xApt.IndexOf("A") >= 0) exame2.apt_Trabalho_Altura2 = "1";
                    if (xApt.IndexOf("C") >= 0) exame2.apt_Espaco_Confinado2 = "1";
                    if (xApt.IndexOf("T") >= 0) exame2.apt_Transporte2 = "1";
                    if (xApt.IndexOf("S") >= 0) exame2.apt_Submerso2 = "1";
                    if (xApt.IndexOf("E") >= 0) exame2.apt_Eletricidade2 = "1";
                    if (xApt.IndexOf("Q") >= 0) exame2.apt_Aquaviario2 = "1";
                    if (xApt.IndexOf("M") >= 0) exame2.apt_Alimento2 = "1";
                    if (xApt.IndexOf("B") >= 0) exame2.apt_Brigadista2 = "1";
                    if (xApt.IndexOf("R") >= 0) exame2.apt_Socorrista2 = "1";






              

                    exame2.IdEmpregado.Find();
                    exame2.IdEmpregado.nID_EMPR.Find();
                    exame2.IdEmpregado.nID_EMPR.IdGrupoEmpresa.Find();

                    int zTamanho = 1;



                    //string xTitulo = "Kit Guia/ASO/PCI - Convocação"; ;

                    //if (xId_Exame != "4")  //apenas guia de complementar
                    //{
                    //    zTamanho = 1;
                    //    xTitulo = "Kit Guia Complementar - Convocação";
                    //}


                    CrystalDecisions.CrystalReports.Engine.ReportClass[] reports = new ReportClass[zTamanho];

                    //Cliente zCliente = new Cliente();
                    //zCliente.Find(System.Convert.ToInt32(xId_Empresa));









                    RptAso report2 = new DataSourceExameAsoPci(exame2).GetReport();

                    //tenho o ID do ASO para colocar no registro da guia gerada ?
                    //dar um update ?




                    //Funcionalidade funcionalidade = Funcionalidade.GetFuncionalidade(FuncionalidadeTipo.Relatorio, report.GetType());
                    //ImpressaoDocUsuario.InsereRegistro(usuario, cliente, Convert.ToInt64(report.SummaryInfo.KeywordsInReport), true, funcionalidade);

                    //CreatePDFDocument(report, this.Response);


                    //Ilitera.Opsa.Data.Empregado empregado = new Ilitera.Opsa.Data.Empregado();

                    //Clinico exame2 = new Clinico(Convert.ToInt32(Request["IdExame"]));

                    reports[0] = report2;

                  



                    //CreatePDFMerged(reports, this.Response, "", false, xID);

                    //HttpResponse response = this.Response;
                    string watermark = "";
                    bool RenumerarPaginas = false;



                    Stream[] streams = new Stream[reports.Length];

                    int i = 0;



                    foreach (CrystalDecisions.CrystalReports.Engine.ReportClass report in reports)
                    {
                        if (RenumerarPaginas)
                            report.ReportDefinition.ReportObjects["PaginaNdeN1"].ObjectFormat.EnableSuppress = true;

                        streams[i] = report.ExportToStream(ExportFormatType.PortableDocFormat);

                        report.Close();

                        i++;
                    }


                    //CreatePDFMerged(streams, response, watermark, RenumerarPaginas, xId);





                    MemoryStream reportStream = PdfSharpUtility.MergeReports(streams, string.Empty, watermark, RenumerarPaginas);



                    string xPath = "I:\\temp\\ASO_" + xId.Trim() + ".pdf";

                    //if (xEnvio_Email == "S")
                    //{

                    //if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().IndexOf("Prajna") > 0)
                    //{




                    FileStream fileStream = File.Create(xPath, (int)reportStream.Length);
                    // Initialize the bytes array with the stream length and then fill it with data
                    byte[] bytesInStream = new byte[reportStream.Length];
                    reportStream.Read(bytesInStream, 0, bytesInStream.Length);
                    // Use write method to write to the file specified above
                    fileStream.Write(bytesInStream, 0, bytesInStream.Length);

                    fileStream.Flush();

                    fileStream.Dispose();
                    fileStream = null;



                    //retornar o ASO criado

                    string xLink = xPath;

                    byte[] arrBytes = File.ReadAllBytes(xLink);
                    string strXml = Convert.ToBase64String(arrBytes);
                    //File.WriteAllText(@"i:\temp\teste_xml.txt", strXml);

                    //string xArq = "I:\\temp\\Retorno_Link_Laudos_" + System.DateTime.Now.Year.ToString().Trim() + System.DateTime.Now.Month.ToString().Trim() + System.DateTime.Now.Day.ToString().Trim() + System.DateTime.Now.Hour.ToString().Trim() + System.DateTime.Now.Minute.ToString().Trim() + System.DateTime.Now.Second.ToString().Trim() + ".xml";

                    zRet = GetXmlDocument(
                    new XDocument(
                         new XElement(
                            new XElement("Conteudo_Arquivo", strXml)
                            )
                          ));//.Save(xArq);





                }



            }
            catch (Exception ex)
            {
                txt_Status = txt_Status + "Erro: Carga do XML: " + ex.Message + System.Environment.NewLine;


            }
            finally
            {

                if (txt_Status != "")
                {
                    //montar XMLDocument com erro
                    string xRet = @"<?xml version=""1.0"" encoding=""UTF-8""?><Erro>" + txt_Status + "</Erro>";

                    zRet.LoadXml(xRet);


                }

            }


            return zRet;

        }





        [WebMethod]
        public XmlDocument Retornar_Status_Exame(string CNPJ, string TipoExame, string CPF, string Data )
        {

            Clinica rClinica = new Clinica();
            Empregado rEmpregado = new Empregado();
            Ilitera.Common.Pessoa rEmpresa = new Ilitera.Common.Pessoa();
            int zId = 0;

            XmlDocument zRet = new XmlDocument();

            string rSelect = "";

            string txt_Status = "";

            

            //int xAux;
            //string xExames = "";
            //string xExames2 = "";
            //string xExames3 = "";
            //string xExames4 = "";
            string xTipo = "";
            //string xBasico = "0";
            //string xObs = "";
            //int xCont = 0;
            //string xEnvio_Email = "N";

            //string xImpDt = "S";

            //Int32 rExame_Existe = 0;


            System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");



            try
            {


                rSelect = " dbo.udf_getnumeric( nomecodigo ) = '" + CNPJ + "' ";

                //pegar Id Empresa                    
                rEmpresa.Find(rSelect);

                //se não achar empresa,  emitir retorno avisando
                if (rEmpresa.Id == 0)
                {
                    txt_Status = txt_Status + "Erro: Empresa não localizada ( " + CNPJ + "  )" + System.Environment.NewLine;
                }

                //quando for convocação ( mailing ) no último irá o link para chamar comunicação
                //pode ir com um ID pronto



                int xTipoExame = 4;

                if (txt_Status == "")
                {
                    //pegar Id Colaborador                       
                    rEmpregado.Find(" tNo_CPF='" + CPF + "'  and nId_Empr = " + rEmpresa.Id.ToString());

                    //se não achar empregado,  emitir retorno avisando
                    if (rEmpregado.Id == 0)
                    {
                        txt_Status = txt_Status + "Erro: Empregado não localizado ( " + CPF + "  )" + System.Environment.NewLine;
                    }

                    if (txt_Status == "")
                    {
                        if (rEmpregado.teMail.Trim() == "")
                        {
                            txt_Status = txt_Status + "Erro: Colaborador sem e-mail cadastrado." + System.Environment.NewLine;
                        }
                    }



                   



                    if (TipoExame.ToUpper().Trim() == "PERIODICO" || TipoExame.ToUpper().Trim() == "PERIÓDICO")
                    {
                        TipoExame = "4";
                        xTipoExame = 4;
                    }
                    else if (TipoExame.ToUpper().Trim() == "DEMISSIONAL")
                    {
                        TipoExame = "2";
                        xTipoExame = 2;
                    }
                    else if (TipoExame.ToUpper().Trim().Substring(0, 5) == "MUDAN")
                    {
                        TipoExame = "3";
                        xTipoExame = 3;
                    }
                    else if (TipoExame.ToUpper().Trim().Substring(0, 7) == "RETORNO")
                    {
                        TipoExame = "5";
                        xTipoExame = 5;
                    }
                    else if (TipoExame.ToUpper().Trim().Substring(0, 6) == "ADMISS")
                    {
                        TipoExame = "1";
                        xTipoExame = 1;
                    }


                    zId = rEmpregado.Id;
                }


                //validar token - deve existir registro com dados do e-mail de convocação



                //código que estaria na outra página
                if (txt_Status.Trim() == "")
                {
                    
                    xTipo = xTipoExame.ToString();


                    ExameBase rexame = new ExameBase();


                    rexame.Find(" IDEMPREGADO = " + rEmpregado.Id.ToString() + " and IdExameDicionario = " + xTipoExame.ToString() + " and convert( char(10),DataExame,103 ) = '" + Data + "' ");



                    if (rexame.Id != 0)
                    {

                        string zResultado;

     
                        if ( rexame.IndResultado==0)
                        {
                            zResultado = "Não Realizado";
                        }
                        else if (rexame.IndResultado == 1)
                        {
                            zResultado = "Normal";
                        }
                        else if (rexame.IndResultado == 2)
                        {
                            zResultado = "Alterado";
                        }
                        else if (rexame.IndResultado == 3)
                        {
                            zResultado = "Em Espera";
                        }
                        else
                        {
                            zResultado = "Indefinido";
                        }


                        zRet = GetXmlDocument(
                        new XDocument(
                             new XElement(
                                new XElement("Status_Exame", zResultado)
                                )
                              ));//.Save(xArq);


                        

                    }
                    else
                    {
                        txt_Status = "Não há exame criado para geração desse ASO.";
                    }

                }


                             

                



            }
            catch (Exception ex)
            {
                txt_Status = txt_Status + "Erro: Carga do XML: " + ex.Message + System.Environment.NewLine;


            }
            finally
            {

                if (txt_Status != "")
                {
                    //montar XMLDocument com erro
                    string xRet = @"<?xml version=""1.0"" encoding=""UTF-8""?><Erro>" + txt_Status + "</Erro>";

                    zRet.LoadXml(xRet);


                }

            }


            return zRet;

        }






        [WebMethod]
        public XmlDocument Retornar_Digitalizado(string CNPJ, string CodUsuario, string TipoExame, string CPF, string Data, string ID)
        {

            Clinica rClinica = new Clinica();
            Empregado rEmpregado = new Empregado();
            Ilitera.Common.Pessoa rEmpresa = new Ilitera.Common.Pessoa();
            int zId = 0;

            XmlDocument zRet = new XmlDocument();

            string rSelect = "";

            string txt_Status = "";

            string xId = ID;


            //int xAux;
            //string xExames = "";
            //string xExames2 = "";
            //string xExames3 = "";
            //string xExames4 = "";
            string xTipo = "";
            //string xBasico = "0";
            //string xObs = "";
            //int xCont = 0;
            //string xEnvio_Email = "N";

            //string xImpDt = "S";

            Int32 rExame_Existe = 0;


            System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");



            try
            {


                rSelect = " dbo.udf_getnumeric( nomecodigo ) = '" + CNPJ + "' ";

                //pegar Id Empresa                    
                rEmpresa.Find(rSelect);

                //se não achar empresa,  emitir retorno avisando
                if (rEmpresa.Id == 0)
                {
                    txt_Status = txt_Status + "Erro: Empresa não localizada ( " + CNPJ + "  )" + System.Environment.NewLine;
                }

                //quando for convocação ( mailing ) no último irá o link para chamar comunicação
                //pode ir com um ID pronto



                int xTipoExame = 4;

                if (txt_Status == "")
                {
                    //pegar Id Colaborador                       
                    rEmpregado.Find(" tNo_CPF='" + CPF + "'  and nId_Empr = " + rEmpresa.Id.ToString());

                    //se não achar empregado,  emitir retorno avisando
                    if (rEmpregado.Id == 0)
                    {
                        txt_Status = txt_Status + "Erro: Empregado não localizado ( " + CPF + "  )" + System.Environment.NewLine;
                    }

              
                                  
                    if (TipoExame.ToUpper().Trim() == "PERIODICO" || TipoExame.ToUpper().Trim() == "PERIÓDICO")
                    {
                        TipoExame = "4";
                        xTipoExame = 4;
                    }
                    else if (TipoExame.ToUpper().Trim() == "DEMISSIONAL")
                    {
                        TipoExame = "2";
                        xTipoExame = 2;
                    }
                    else if (TipoExame.ToUpper().Trim().Substring(0, 5) == "MUDAN")
                    {
                        TipoExame = "3";
                        xTipoExame = 3;
                    }
                    else if (TipoExame.ToUpper().Trim().Substring(0, 7) == "RETORNO")
                    {
                        TipoExame = "5";
                        xTipoExame = 5;
                    }
                    else if (TipoExame.ToUpper().Trim().Substring(0, 6) == "ADMISS")
                    {
                        TipoExame = "1";
                        xTipoExame = 1;
                    }


                    zId = rEmpregado.Id;
                }


                //ver se tem ASO com resultado e se tem digitalizado


                xTipo = xTipoExame.ToString();




                ExameBase rexame = new ExameBase();


                rexame.Find(" IDEMPREGADO = " + rEmpregado.Id.ToString() + " and IdExameDicionario = " + xTipoExame.ToString() + " and convert( char(10),DataExame,103 ) = '" + Data + "' ");



                if (rexame.Id != 0)
                {

                    rExame_Existe = rexame.Id;

                    if ( rexame.IndResultado !=1 && rexame.IndResultado != 2)
                    {
                        txt_Status = "Exame não tem o resultado.";
                    }

                }
                else
                {
                    txt_Status = "Não há exame criado para geração desse ASO.";
                }




                if ( txt_Status == "")
                {

                    ProntuarioDigital xProntuario = new ProntuarioDigital();

                    xProntuario.Find(" IdExameBase = " + rexame.Id.ToString());

                    if ( xProntuario.Id == 0)
                    {
                        txt_Status = "Exame não foi digitalizado.";
                    }
                    else
                    {



                        string xPath = xProntuario.Arquivo;


                        string xExtensao = xPath.Substring( xPath.Length-3, 3 );




                        string xLink = xPath;
                        string strXml = "";

                        try
                        {

                            byte[] arrBytes = File.ReadAllBytes(xLink);
                            strXml = Convert.ToBase64String(arrBytes);

                        }
                        catch ( Exception Ex)
                        {

                            txt_Status = "Erro ao carregar prontuário: " + Ex.Message;

                        }


                        if (txt_Status == "")
                        {
                            zRet = GetXmlDocument(
                            new XDocument(
                                new XElement("Arquivo",
                                    new XElement("Conteudo_Arquivo", strXml),
                                    new XElement("Extensao", xExtensao)                                    
                                  ) ) );//.Save(xArq);
                        }

                    }



                }






            }
            catch (Exception ex)
            {
                txt_Status = txt_Status + "Erro: Carga do XML: " + ex.Message + System.Environment.NewLine;


            }
            finally
            {

                if (txt_Status != "")
                {
                    //montar XMLDocument com erro
                    string xRet = @"<?xml version=""1.0"" encoding=""UTF-8""?><Erro>" + txt_Status + "</Erro>";

                    zRet.LoadXml(xRet);


                }

            }


            return zRet;

        }



        private void Carregar_Dados_Anamnese_Exame(Int32 zExame)
        {


            if (zExame == 0)
            {
                return;
            }



            List<Anamnese_Exame> AnExame = new Anamnese_Exame().Find<Anamnese_Exame>(" IdExameBase = " + zExame);


            Clinico vExame = new Clinico();
            vExame.Find(zExame);


            if (AnExame.Count == 0)
            {
                //trazer padrão para cliente
                vExame.IdEmpregado.Find();
                vExame.IdEmpregado.nID_EMPR.Find();
                List<Anamnese_Dinamica> anExamePadrao = new Anamnese_Dinamica().Find<Anamnese_Dinamica>(" IdPessoa = " + vExame.IdEmpregado.nID_EMPR.Id);


                if (anExamePadrao.Count == 0)
                {
                    return;
                }
                else
                {
                    Ilitera.Data.Clientes_Funcionarios xAnam = new Ilitera.Data.Clientes_Funcionarios();
                    xAnam.Carregar_Anamnese_Dinamica(vExame.IdEmpregado.nID_EMPR.Id, zExame);


                    //foreach (Anamnese_Dinamica zPadrao in anExamePadrao)
                    //{
                    //    Anamnese_Exame rTestes = new Anamnese_Exame();

                    //    rTestes.IdAnamneseDinamica = zPadrao.Id;
                    //    rTestes.IdExameBase = vExame.Id;
                    //    rTestes.Resultado = "N";
                    //    rTestes.Peso = zPadrao.Peso;
                    //    rTestes.Save();
                    //}

                }

            }


            return;


        }



        protected void Envio_Email_Prajna(string xPara, string xCopia, string xSubject, string xBody, string xAttach, string xModulo, Int32 xIdEmpregado, Int32 xIdEmpresaExameClinicoFacade, ExameClinicoFacade exame, string xCodUsuario, Int32 xIdEmpresa)
        {

            string xDestinatario = "";

            //enviar e-mail para clinica e prajna

            MailMessage objEmail = new MailMessage();

            //rementente do email           
            objEmail.From = new MailAddress("agendamento@5aessence.com.br");


            //para
            objEmail.To.Add(xPara);

            objEmail.CC.Add("agendamento@5aessence.com.br");

            string[] stringSeparators2 = new string[] { ";" };
            string[] result2;

            result2 = xCopia.Split(stringSeparators2, StringSplitOptions.RemoveEmptyEntries);

            foreach (string s in result2)
            {
                objEmail.CC.Add(s);
            }



            xDestinatario = xPara + ";" + xCopia;

            //cópia para usuário logado
            //Entities.Usuario usuario = (Entities.Usuario)Session["usuarioLogado"];
            Entities.Usuario usuario = new Entities.Usuario();
            usuario.IdUsuario = System.Convert.ToInt32(xCodUsuario);


         
            objEmail.Priority = MailPriority.Normal;
            objEmail.IsBodyHtml = true;

            objEmail.Subject = xSubject;
            objEmail.Body = xBody;
            objEmail.SubjectEncoding = Encoding.GetEncoding("ISO-8859-1");
            objEmail.BodyEncoding = Encoding.GetEncoding("ISO-8859-1");

            Attachment xItem = new Attachment(xAttach);
            objEmail.Attachments.Add(xItem);

            SmtpClient objSmtp = new SmtpClient();
            //objSmtp.Host = "mail.exchange.locaweb.com.br";
            //objSmtp.Port = 587;
            //objSmtp.Credentials = new NetworkCredential("agendamento@essencenet.com.br", "kr.prj1705");
            
            objSmtp.EnableSsl = true;

            //objSmtp.Host = "outlook.office.com";
            objSmtp.Host = "smtp.office365.com";
            objSmtp.Port = 587;
            objSmtp.Credentials = new NetworkCredential("agendamento@5aessence.com.br", "Agend_5060");

            objSmtp.Send(objEmail);

            Ilitera.Data.Clientes_Clinicas xEnvio = new Ilitera.Data.Clientes_Clinicas();
            xEnvio.Envio_eMail(xIdEmpregado, xIdEmpresa, xDestinatario, "Kit Guia / ASO / PCI");

            return;

        }




        protected void Envio_Email_Ilitera(string xPara, string xCopia, string xSubject, string xBody, string xAttach, string xModulo, Int32 xIdEmpregado, Int32 xIdEmpresa)
        {
            string xDestinatario = "";
            //enviar e-mail para clinica e prajna

            MailMessage objEmail = new MailMessage();

            //rementente do email
            objEmail.From = new MailAddress("agendamento.sp.sto@ilitera.com.br");


            //caixa-postal de onde será enviado o e-mail
            //objEmail.ReplyTo = new MailAddress("email@seusite.com.br");

            //para
            //objEmail.To.Add("lasdowsky@gmail.com");
            //string xEmail = "";

            //xEmail = exame.IdJuridica.Email.ToString().Trim();

            //if (xEmail == "")
            //{
            //    throw new Exception("Clínica não possui e-mail cadastrado.");
            //}

            //if (xEmail.IndexOf(";") > 0) xEmail = xEmail.Substring(0, xEmail.IndexOf(";"));

            //objEmail.To.Add(xEmail);

            objEmail.To.Add(xPara);
            //xDestinatario = xEmail + "; atendimento@ilitera.com.br ;";

            //objEmail.Bcc.Add("oculto@provedor.com.br");

            //objEmail.CC.Add("atendimento@ilitera.com.br");
            //objEmail.CC.Add("atendimento2@ilitera.com.br");

            objEmail.Priority = MailPriority.Normal;
            objEmail.IsBodyHtml = true;

            objEmail.Subject = xSubject;
            objEmail.Body = xBody;
            objEmail.SubjectEncoding = Encoding.GetEncoding("ISO-8859-1");
            objEmail.BodyEncoding = Encoding.GetEncoding("ISO-8859-1");

            Attachment xItem = new Attachment(xAttach);
            objEmail.Attachments.Add(xItem);

            SmtpClient objSmtp = new SmtpClient();
            objSmtp.Host = "smtp.ilitera.com.br";
            objSmtp.Port = 587;
            objSmtp.Credentials = new NetworkCredential("agendamento.sp.sto@ilitera.com.br", "bibi6096");


            Ilitera.Data.Clientes_Clinicas xEnvio = new Ilitera.Data.Clientes_Clinicas();
            //checar se não é e-mail duplicado

            //DataSet rDs = xEnvio.Checar_Envio_Email(xIdEmpregado, xIdEmpresa, xDestinatario, "Kit Guia / ASO / PCI");

            //if (rDs.Tables[0].Rows.Count == 0)
            //{
            objSmtp.Send(objEmail);
            xEnvio.Envio_eMail(xIdEmpregado, xIdEmpresa, xDestinatario, "Kit Guia / ASO / PCI");
            //}


            return;

        }



        private string PopularValueListClinicaClienteExameDicionario(string xValue, string xIdExame, string xIdEmpresa, string xIdEmpregado, string xCodUsuario, string xData)
        {

            string lst_Exames = "";

            DataSet dsExames = new ExameDicionario().GetIdNome("Nome", " IdExameDicionario IN (SELECT IdExameDicionario FROM ClinicaExameDicionario WHERE IdClinica IN (SELECT IdClinica FROM ClinicaCliente WHERE IdCliente=" + xIdEmpresa + " and IdClinica = " + xValue + " ))");
            DataSet ds = new ClinicaExameDicionario().Get("IdClinica IN (SELECT IdClinica FROM ClinicaCliente WHERE IdCliente=" + xIdEmpresa + " " + " and IdClinica = " + xValue + " ) " + " AND IDCLINICAEXAMEDICIONARIO IN " +
             "( " +
             "   SELECT IdClinicaExameDicionario " +
             "   FROM ClinicaClienteExameDicionario  " +
             "    WHERE IdClinicaCliente IN ( " +
             "      SELECT IdClinicaCliente FROM ClinicaCliente " +
             "      WHERE IdCliente=" + xIdEmpresa + " " + " and IdClinica = " + xValue + " and IsAutorizado = 1 ) ) ");


            ////carregar dados da clinica
            Clinica xClinica = new Clinica(System.Convert.ToInt32(xValue));



            //pegar exames de PCMSO do funcionário
            Ilitera.Opsa.Data.Empregado empregado = new Ilitera.Opsa.Data.Empregado();
            empregado.Find(" tblEMPREGADO.nID_EMPREGADO = " + xIdEmpregado);

            Clinico exame = new Clinico();
            exame.IdEmpregado = empregado;
            exame.IdEmpregadoFuncao = EmpregadoFuncao.GetEmpregadoFuncao(empregado);
            exame.IdPcmso = exame.IdEmpregadoFuncao.nID_EMPR.GetUltimoPcmso();

            exame.UsuarioId = System.Convert.ToInt32(xCodUsuario);

            Pcmso pcmso = new Pcmso();
            pcmso = exame.IdPcmso;

            bool zClinico = false;


            if (pcmso.IdLaudoTecnico != null)
            {
                List<Ghe> ghes = new Ghe().Find<Ghe>("nID_LAUD_TEC=" + pcmso.IdLaudoTecnico.Id);

                Ghe ghe;

                if (ghes == null || ghes.Count == 0)
                    ghe = exame.IdEmpregadoFuncao.GetGheEmpregado(pcmso.IdLaudoTecnico);
                else
                {
                    int IdGhe = exame.IdEmpregadoFuncao.GetIdGheEmpregado(pcmso.IdLaudoTecnico);

                    ghe = ghes.Find(delegate (Ghe g) { return g.Id == IdGhe; });
                }



                if (ghe == null)
                {
                    return "-10   Colaborador não está alocado em GHE,  não é possível criar Guia de Encaminhamento/ASO.";
                }



                bool zDesconsiderar = false;
                string xDataBranco = "";

                string sExamesOcupacionais = "";

                Cliente cliente = new Cliente();
                cliente.Find(System.Convert.ToInt32(xIdEmpresa));


                //Wagner 04/07/2018 - ver exames complementares que estão com data ( ver se opção exibir data complementares está ativa )
                // esses exames com datas não precisam aparecer na guia, pois não precisam ser solicitados.

                //se for para desconsiderar data de complementares a partir de certa data, usar o esquema que já existe com xDataBranco
                if (xDataBranco == "")
                {
                    if (cliente.Ativar_DesconsiderarCompl == true)
                    {
                        if (cliente.Dias_Desconsiderar > 0)
                        {
                            zDesconsiderar = true;
                        }
                    }
                }
                else
                {
                    zDesconsiderar = false;
                }


                //Clinico clinico = new Clinico();

                //clinico.IdPcmso = pcmso;
                //clinico.IdEmpregadoFuncao = EmpregadoFuncao.GetEmpregadoFuncao(pcmso.IdLaudoTecnico, empregado);


                //clinico.UsuarioId = System.Convert.ToInt32(Request["IdUsuario"].ToString());
                ExameDicionario rDicionario = new ExameDicionario();

                rDicionario.Find(System.Convert.ToInt32(xIdExame));

                exame.IdExameDicionario = rDicionario;

                System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");
                exame.DataExame = System.Convert.ToDateTime(xData, ptBr);



                //pegar exames do ASO
                string sExamesASO = "";
                string sExamesASO_Aptidao = "";


                if (xIdExame == "3")   //mudança de função
                {
                    // procurar ghe_ant primeiro, na mesma classif.funcional
                    // se nao encontrar, procurar classif.funcional anterior e ghe


                    if (cliente.GHEAnterior_MudancaFuncao == true)
                    {
                        // procurar ghe_ant primeiro, na mesma classif.funcional
                        // se nao encontrar, procurar classif.funcional anterior e ghe

                        Ilitera.Data.PPRA_EPI xGHE = new Ilitera.Data.PPRA_EPI();

                        DataSet xdS = xGHE.Trazer_Laudos_GHEs_Colaborador(empregado.Id);

                        if (xdS.Tables[0].Rows.Count < 2)
                        {
                            return "-14  O empregado " + empregado.tNO_EMPG + " está associado a apenas um GHE/Classif.Funcional, logo, não é possível gerar o ASO de Mudança de Função. ";
                        }


                        int znAux = 0;
                        Int32 zGHE_Atual = 0;
                        Int32 zGHE_Ant = 0;


                        foreach (DataRow row in xdS.Tables[0].Rows)
                        {
                            znAux++;

                            if (znAux == 1) zGHE_Atual = System.Convert.ToInt32(row["nId_Func"].ToString());
                            else if (znAux == 2) zGHE_Ant = System.Convert.ToInt32(row["nId_Func"].ToString());
                            else break;

                        }


                        //ghe_ant
                        //ghe
                        Ghe zGhe1 = new Ghe();
                        zGhe1.Find(zGHE_Atual);
                        Ghe zGhe2 = new Ghe();
                        zGhe2.Find(zGHE_Ant);

                        if (zDesconsiderar == false)
                            sExamesASO = exame.GetPlanejamentoExamesAso_Formatado_Mudanca_Funcao(zGhe1, zGhe2, false, cliente.Exibir_Datas_Exames_ASO, xDataBranco, zDesconsiderar);
                        else
                            sExamesASO = exame.GetPlanejamentoExamesAso_Formatado_Mudanca_Funcao_Desconsiderar(zGhe1, zGhe2, false, cliente.Exibir_Datas_Exames_ASO, exame, cliente.Dias_Desconsiderar);
                    }
                    else
                    {
                        if (zDesconsiderar == false)
                            sExamesASO = exame.GetPlanejamentoExamesAso_Formatado(ghe, false, cliente.Exibir_Datas_Exames_ASO, xDataBranco, zDesconsiderar);
                        else
                            sExamesASO = exame.GetPlanejamentoExamesAso_Formatado_Desconsiderar(ghe, false, cliente.Exibir_Datas_Exames_ASO, exame, cliente.Dias_Desconsiderar);
                    }


                }
                else
                {

                    if (xIdExame == "2")  //demissional
                    {
                        if (zDesconsiderar == false)
                            sExamesASO = exame.GetPlanejamentoExamesAso_Formatado(ghe, false, false, xDataBranco, zDesconsiderar);
                        else
                            sExamesASO = exame.GetPlanejamentoExamesAso_Formatado_Desconsiderar(ghe, false, false, exame, cliente.Dias_Desconsiderar);
                    }
                    else
                    {
                        if (zDesconsiderar == false)
                            sExamesASO = exame.GetPlanejamentoExamesAso_Formatado(ghe, false, cliente.Exibir_Datas_Exames_ASO, xDataBranco, zDesconsiderar);
                        else
                            sExamesASO = exame.GetPlanejamentoExamesAso_Formatado_Desconsiderar(ghe, false, cliente.Exibir_Datas_Exames_ASO, exame, cliente.Dias_Desconsiderar);
                    }
                }





                //pegar exames para guia
                if (xIdExame == "3")   //mudança de função
                {

                    if (cliente.GHEAnterior_MudancaFuncao == true)
                    {
                        // procurar ghe_ant primeiro, na mesma classif.funcional
                        // se nao encontrar, procurar classif.funcional anterior e ghe

                        Ilitera.Data.PPRA_EPI xGHE = new Ilitera.Data.PPRA_EPI();

                        DataSet xdS = xGHE.Trazer_Laudos_GHEs_Colaborador(empregado.Id);

                        if (xdS.Tables[0].Rows.Count < 2)
                        {
                            return "-13  O empregado " + empregado.tNO_EMPG + " está associado a apenas um GHE/Classif.Funcional, logo, não é possível gerar o ASO de Mudança de Função. ";
                        }


                        int znAux = 0;
                        Int32 zGHE_Atual = 0;
                        Int32 zGHE_Ant = 0;


                        foreach (DataRow row in xdS.Tables[0].Rows)
                        {
                            znAux++;

                            if (znAux == 1) zGHE_Atual = System.Convert.ToInt32(row["nId_Func"].ToString());
                            else if (znAux == 2) zGHE_Ant = System.Convert.ToInt32(row["nId_Func"].ToString());
                            else break;

                        }


                        //ghe_ant
                        //ghe
                        Ghe zGhe1 = new Ghe();
                        zGhe1.Find(zGHE_Atual);
                        Ghe zGhe2 = new Ghe();
                        zGhe2.Find(zGHE_Ant);


                        sExamesOcupacionais = exame.GetPlanejamentoExamesAso_Formatado_Mudanca_Funcao_Guia(zGhe1, zGhe2, true);
                    }
                    else
                    {
                        sExamesOcupacionais = exame.GetPlanejamentoExamesAso_Guia(ghe, true, "M");
                    }


                }
                else if (xIdExame == "1")  // admissao
                {
                    sExamesOcupacionais = exame.GetPlanejamentoExamesAso_Guia(ghe, true, "A");
                }
                else if (xIdExame == "2")  // demissao
                {
                    sExamesOcupacionais = exame.GetPlanejamentoExamesAso_Guia(ghe, true, "D");
                }
                else if (xIdExame == "5")  // retorno
                {
                    sExamesOcupacionais = exame.GetPlanejamentoExamesAso_Guia(ghe, true, "R");
                }
                else if (xIdExame == "4")   //periódico
                {
                    sExamesOcupacionais = exame.GetPlanejamentoExamesAso_Guia(ghe, true, "P");
                }
                else
                {
                    sExamesOcupacionais = exame.GetPlanejamentoExamesAso_Guia(ghe, true);
                }



                //checar se têm exames de aptidão
                string sExamesOcupacionais_Aptidao = "";

                Empregado_Aptidao xAptidao = new Empregado_Aptidao();
                xAptidao.Find(" nId_Empregado = " + empregado.Id.ToString());


                GHE_Aptidao zAptidao = new GHE_Aptidao();
                zAptidao.Find("nId_Func = " + ghe.Id.ToString());

                if (xAptidao.Id != 0 || zAptidao.Id != 0)
                {
                    if ((xAptidao.apt_Alimento == true || xAptidao.apt_Aquaviario == true || xAptidao.apt_Eletricidade == true || xAptidao.apt_Espaco_Confinado == true ||
                              xAptidao.apt_Submerso == true || xAptidao.apt_Trabalho_Altura == true || xAptidao.apt_Transporte == true || xAptidao.apt_Brigadista == true || xAptidao.apt_Socorrista == true) ||
                              (zAptidao.apt_Alimento == true || zAptidao.apt_Aquaviario == true || zAptidao.apt_Eletricidade == true || zAptidao.apt_Espaco_Confinado == true ||
                             zAptidao.apt_Submerso == true || zAptidao.apt_Trabalho_Altura == true || zAptidao.apt_Transporte == true || zAptidao.apt_Brigadista == true || zAptidao.apt_Socorrista == true))
                    {

                        Empregado_Aptidao nAptidao = new Empregado_Aptidao();


                        nAptidao.nId_Empregado = empregado.Id;

                        //juntando aptidao do empregado com do PPRA-GHE
                        if (xAptidao.Id != 0 && zAptidao.Id != 0)
                        {
                            nAptidao.apt_Alimento = xAptidao.apt_Alimento || zAptidao.apt_Alimento;
                            nAptidao.apt_Aquaviario = xAptidao.apt_Aquaviario || zAptidao.apt_Aquaviario;
                            nAptidao.apt_Brigadista = xAptidao.apt_Brigadista || zAptidao.apt_Brigadista;
                            nAptidao.apt_Eletricidade = xAptidao.apt_Eletricidade || zAptidao.apt_Eletricidade;
                            nAptidao.apt_Espaco_Confinado = xAptidao.apt_Espaco_Confinado || zAptidao.apt_Espaco_Confinado;
                            nAptidao.apt_Socorrista = xAptidao.apt_Socorrista || zAptidao.apt_Socorrista;
                            nAptidao.apt_Submerso = xAptidao.apt_Submerso || zAptidao.apt_Submerso;
                            nAptidao.apt_Trabalho_Altura = xAptidao.apt_Trabalho_Altura || zAptidao.apt_Trabalho_Altura;
                            nAptidao.apt_Transporte = xAptidao.apt_Transporte || zAptidao.apt_Transporte;
                        }
                        else if (xAptidao.Id != 0)
                        {
                            nAptidao.apt_Alimento = xAptidao.apt_Alimento;
                            nAptidao.apt_Aquaviario = xAptidao.apt_Aquaviario;
                            nAptidao.apt_Brigadista = xAptidao.apt_Brigadista;
                            nAptidao.apt_Eletricidade = xAptidao.apt_Eletricidade;
                            nAptidao.apt_Espaco_Confinado = xAptidao.apt_Espaco_Confinado;
                            nAptidao.apt_Socorrista = xAptidao.apt_Socorrista;
                            nAptidao.apt_Submerso = xAptidao.apt_Submerso;
                            nAptidao.apt_Trabalho_Altura = xAptidao.apt_Trabalho_Altura;
                            nAptidao.apt_Transporte = xAptidao.apt_Transporte;
                        }
                        else if (zAptidao.Id != 0)
                        {
                            nAptidao.apt_Alimento = zAptidao.apt_Alimento;
                            nAptidao.apt_Aquaviario = zAptidao.apt_Aquaviario;
                            nAptidao.apt_Brigadista = zAptidao.apt_Brigadista;
                            nAptidao.apt_Eletricidade = zAptidao.apt_Eletricidade;
                            nAptidao.apt_Espaco_Confinado = zAptidao.apt_Espaco_Confinado;
                            nAptidao.apt_Socorrista = zAptidao.apt_Socorrista;
                            nAptidao.apt_Submerso = zAptidao.apt_Submerso;
                            nAptidao.apt_Trabalho_Altura = zAptidao.apt_Trabalho_Altura;
                            nAptidao.apt_Transporte = zAptidao.apt_Transporte;
                        }

                        Cliente xCliente = new Cliente();
                        xCliente.Find(pcmso.IdCliente.Id);

                        ExameDicionario rDic = new ExameDicionario();
                        rDic.Find(System.Convert.ToInt32(xIdExame));


                        exame.IdExameDicionario = rDic;
                        exame.IdEmpregado = empregado;


                        sExamesOcupacionais_Aptidao = exame.GetPlanejamentoExamesAso_Guia_Aptidao(nAptidao, xCliente.Exibir_Datas_Exames_ASO, "F", sExamesOcupacionais);

                        sExamesASO_Aptidao = exame.GetPlanejamentoExamesAso_Formatado_Aptidao(nAptidao, xCliente.Exibir_Datas_Exames_ASO, xDataBranco, sExamesOcupacionais, zDesconsiderar, exame);

                    }
                }

                if (sExamesOcupacionais.Trim() != "")
                    sExamesOcupacionais = sExamesOcupacionais + sExamesOcupacionais_Aptidao;
                else
                    sExamesOcupacionais = sExamesOcupacionais_Aptidao;

                string txt_Exames = sExamesOcupacionais;



                if (sExamesASO.Trim() != "")
                    sExamesASO = sExamesASO + sExamesASO_Aptidao;
                else
                    sExamesASO = sExamesASO_Aptidao;


                //bool zSelecao = true;





                //Prajna quer que apareça clinico se ele realmente estiver no planejamento
                if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().IndexOf("Prajna") < 0)
                {
                    lst_Exames = lst_Exames + "Exame Clínico" + "|";
                }


                //pegar apenas complementar da convocação - kit
                if (xIdExame != "4")
                {


                    int xPosit = 0;
                    DataRow[] rows = dsExames.Tables[0].Select("Id=" + xIdExame);

                    //valueListClinicaClienteExameDicionario.ValueListItems.Add(Convert.ToInt32(row[0]), Convert.ToString(rows[0]["Nome"]));
                    if (rows.Count() > 0)
                    {

                        // retirar admissional, periodico, retorno ao trab., complement. desta lista
                        if (rows[0]["Nome"].ToString().Trim().ToUpper() != "RETORNO AO TRABALHO" &&
                            rows[0]["Nome"].ToString().Trim().ToUpper() != "DEMISSIONAL" &&
                            rows[0]["Nome"].ToString().Trim().ToUpper() != "ADMISSIONAL" &&
                            rows[0]["Nome"].ToString().Trim().ToUpper() != "MUDANÇA DE FUNÇÃO" &&
                            rows[0]["Nome"].ToString().Trim().ToUpper() != "PERIÓDICO")
                        {

                            //  lst_Exames.Items.Add(Convert.ToString(rows[0]["Nome"]));

                            if (sExamesOcupacionais.ToUpper().IndexOf(Convert.ToString(rows[0]["Nome"]).ToUpper()) > 0)
                            {
                                //poderia procurar o exame, e ver se foi feito e está com resultado.  Se sim, não selecionar
                                //precisaria usar a data de planejamento deste exame

                                xPosit = sExamesASO.ToUpper().IndexOf(Convert.ToString(rows[0]["Nome"]).ToUpper());

                                if (xPosit > 0)
                                {
                                    if (sExamesASO.Substring(xPosit + 38, 12).Replace("/", " ").Trim() == "")
                                        xPosit = 0;

                                }

                                if (xPosit == 0)
                                {
                                    lst_Exames = lst_Exames + Convert.ToString(rows[0]["Nome"]) + "|";
                                }
                            }

                        }


                    }

                }
                else
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        int xPosit = 0;
                        DataRow[] rows = dsExames.Tables[0].Select("Id=" + Convert.ToInt32(row["IdExameDicionario"]));

                        //valueListClinicaClienteExameDicionario.ValueListItems.Add(Convert.ToInt32(row[0]), Convert.ToString(rows[0]["Nome"]));
                        if (rows.Count() > 0)
                        {

                            // retirar admissional, periodico, retorno ao trab., complement. desta lista
                            if (rows[0]["Nome"].ToString().Trim().ToUpper() != "RETORNO AO TRABALHO" &&
                                rows[0]["Nome"].ToString().Trim().ToUpper() != "DEMISSIONAL" &&
                                rows[0]["Nome"].ToString().Trim().ToUpper() != "ADMISSIONAL" &&
                                rows[0]["Nome"].ToString().Trim().ToUpper() != "MUDANÇA DE FUNÇÃO" &&
                                rows[0]["Nome"].ToString().Trim().ToUpper() != "PERIÓDICO")
                            {

                                //  lst_Exames.Items.Add(Convert.ToString(rows[0]["Nome"]));

                                if (sExamesOcupacionais.ToUpper().IndexOf(Convert.ToString(rows[0]["Nome"]).ToUpper()) > 0)
                                {
                                    //poderia procurar o exame, e ver se foi feito e está com resultado.  Se sim, não selecionar
                                    //precisaria usar a data de planejamento deste exame

                                    xPosit = sExamesASO.ToUpper().IndexOf(Convert.ToString(rows[0]["Nome"]).ToUpper());

                                    if (xPosit > 0)
                                    {
                                        if (sExamesASO.Substring(xPosit + 38, 12).Replace("/", " ").Trim() == "")
                                            xPosit = 0;

                                    }

                                    if (xPosit == 0)
                                    {
                                        lst_Exames = lst_Exames + Convert.ToString(rows[0]["Nome"]) + "|";

                                    }
                                }

                            }
                            else if (rows[0]["Nome"].ToString().Trim().ToUpper() == "PERIÓDICO")
                            {
                                zClinico = true;
                            }

                        }

                    }
                }






                if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().IndexOf("Prajna") >= 0)
                {
                    if (zClinico == true)
                    {

                        lst_Exames = lst_Exames + "Exame Clínico" + "|";
                    }
                }

            }
            else
            {
                lst_Exames = "-1 ( Empresa sem PCMSO criado )";
            }






            return lst_Exames;



        }



        protected Boolean Validar_Data(string zData)
        {
            int zDia = 0;
            int zMes = 0;
            int zAno = 0;

            string Validar;
            bool isNumerical;
            int myInt;


            if (zData.Length != 10)
            {
                return false;
            }

            Validar = zData.Substring(0, 2);
            isNumerical = int.TryParse(Validar, out myInt);
            if (isNumerical == false)
            {
                return false;
            }


            Validar = zData.Substring(3, 2);
            isNumerical = int.TryParse(Validar, out myInt);
            if (isNumerical == false)
            {
                return false;
            }


            Validar = zData.Substring(6, 4);
            isNumerical = int.TryParse(Validar, out myInt);
            if (isNumerical == false)
            {
                return false;
            }


            if (zData.Substring(2, 1) != "/" || zData.Substring(5, 1) != "/")
            {
                return false;
            }


            zDia = System.Convert.ToInt32(zData.Substring(0, 2));
            zMes = System.Convert.ToInt32(zData.Substring(3, 2));
            zAno = System.Convert.ToInt32(zData.Substring(6, 4));

            if (zAno < 1900 || zAno > 2025)
            {
                return false;
            }

            if (zMes < 1 || zMes > 12)
            {
                return false;
            }

            if (zMes == 1 || zMes == 3 || zMes == 5 || zMes == 7 || zMes == 8 || zMes == 10 || zMes == 12)
            {
                if (zDia < 1 || zDia > 31)
                {
                    return false;
                }
            }
            else if (zMes == 4 || zMes == 6 || zMes == 9 || zMes == 11)
            {
                if (zDia < 1 || zDia > 30)
                {
                    return false;
                }
            }
            else
            {
                if (zDia < 1 || zDia > 29)
                {
                    return false;
                }
            }

            return true;

        }





        [WebMethod]
        public XmlDocument Solicitar_eSocial_1060(string CNPJ, string DataInicial, string DataFinal, string CodUsuario)
        {

            Ilitera.Common.Pessoa rEmpresa = new Ilitera.Common.Pessoa();

            //int zId = 0;


            string rSelect = "";

            string xRetorno = "";

            string txt_Status = "";

            XmlDocument zRet = new XmlDocument();


            try
            {




                xRetorno = "";


                if (CNPJ.Length > 14)
                {
                    txt_Status = txt_Status + " Campo CNPJ inválido |";
                    xRetorno = xRetorno + " Campo CNPJ Inválido |";
                }

                if (DataInicial.Length != 10)
                {
                    txt_Status = txt_Status + " Campo Data Inicial inválido |";
                    xRetorno = xRetorno + " Campo Data Inicial Inválido |";
                }
                if (Validar_Data(DataInicial) == false)
                {
                    txt_Status = txt_Status + " Campo Data Inicial inválido |";
                    xRetorno = xRetorno + " Campo Data Inicial Inválido |";
                }

                if (DataFinal.Length != 10)
                {
                    txt_Status = txt_Status + " Campo Data Final inválido |";
                    xRetorno = xRetorno + " Campo Data Final Inválido |";
                }
                if (Validar_Data(DataFinal) == false)
                {
                    txt_Status = txt_Status + " Campo Data Final inválido |";
                    xRetorno = xRetorno + " Campo Data Final Inválido |";
                }


                if (xRetorno != "")
                    xRetorno = "50 ( Validação de campos ): " + xRetorno;






                if (txt_Status == "")
                {

                    rEmpresa = new Ilitera.Common.Pessoa();

                    rSelect = " dbo.udf_getnumeric( nomecodigo ) = '" + CNPJ + "' ";

                    //pegar Id Empresa                    
                    rEmpresa.Find(rSelect);

                    //se não achar empresa,  emitir retorno avisando
                    if (rEmpresa.Id == 0)
                    {
                        txt_Status = txt_Status + "Erro: Empresa não localizada ( " + CNPJ + "  )" + System.Environment.NewLine;
                        xRetorno = "03 (Empresa não localizada (" + CNPJ + ")";
                    }

                }
                
                


                if (txt_Status == "")
                {

                    //criar
                    DataSet zDs2 = new DataSet();
                    Ilitera.Data.eSocial xLista2 = new Ilitera.Data.eSocial();
                    zDs2 = xLista2.Mensageria_1060(rEmpresa.Id, 0, 0, DataInicial, DataFinal, System.Convert.ToInt32( CodUsuario), "4", "", "N");

                    for (int zCont = 0; zCont < zDs2.Tables[0].Rows.Count; zCont++)
                    {

                        string zCod = "";

                        string xLaudo = zDs2.Tables[0].Rows[zCont]["nId_Laud_Tec"].ToString().Trim();
                        string xCodAmb = zDs2.Tables[0].Rows[zCont]["CodAmb"].ToString().Trim();

                        string zCriado = zDs2.Tables[0].Rows[zCont]["Criado_Em"].ToString().Trim();
                        //verificar se já foi criado o XML


                        if (zCriado == "")
                        {
                            Ilitera.Net.e_Social.Controle_eSocial_1060 z1060 = new Ilitera.Net.e_Social.Controle_eSocial_1060();
                            z1060.Processar_S1060(System.Convert.ToInt32(xLaudo), DataInicial, DataFinal, zCod, xCodAmb, System.Convert.ToInt32(CodUsuario));
                        }


                    }

                }


                //                zRet = GetXmlDocument(
                //new XDocument(
                //     new XElement("Tipo",
                //        new XElement("CNPJ", CNPJ),
                //        new XElement("Laudo", Laudo),
                //        new XElement("Link_Laudo", ""),
                //        new XElement("Conteudo_Arquivo", strXml)
                //        )
                //      ));//.Save(xArq);





                //XmlDocument xml = new XmlDocument();
                //XmlElement root = xml.CreateElement("RETORNO");
                //xml.AppendChild(root);


                string xRetXML = "<RETORNO>";

                //juntar XMLs
                DataSet zDs = new DataSet();
                Ilitera.Data.eSocial xLista = new Ilitera.Data.eSocial();
                zDs = xLista.Mensageria_1060(rEmpresa.Id, 0, 0, DataInicial, DataFinal, System.Convert.ToInt32(CodUsuario), "4", "", "N");

                for (int zCont = 0; zCont < zDs.Tables[0].Rows.Count; zCont++)
                {
                                        
                    Ilitera.Data.eSocial xDados = new Ilitera.Data.eSocial();

                    DataSet rDs = new DataSet();
                    rDs = xDados.Trazer_XML2(System.Convert.ToInt32( zDs.Tables[0].Rows[zCont]["IdeSocial_Deposito"].ToString().Trim() ));

                    string strXML = rDs.Tables[0].Rows[0][0].ToString().Replace("\r\n", string.Empty);


                    //criar array para armazenas string de XML para depois criar o XML de retorno e ir adicionando ?   Ou crio antes do loop o XML e vou adicionando aqui, acho melhor 

                    //XmlElement child = xml.CreateElement("XMLS");
                    //child.SetAttribute("1060", strXML);
                    //root.AppendChild(child);

                    if ( strXML.Trim() != "")
                       xRetXML = xRetXML + "<XMLS>" + strXML.Substring(strXML.IndexOf("<eSocial")) + "</XMLS>";
                                        
                }

                //zRet = xml;

                xRetXML = xRetXML + "</RETORNO>";

                byte[] encodedString2 = Encoding.UTF8.GetBytes(xRetXML);

                MemoryStream ms2 = new MemoryStream(encodedString2);
                ms2.Flush();
                ms2.Position = 0;

                XmlDocument zLote = new XmlDocument();
                zLote.Load(ms2);

                zRet = zLote;

                xRetorno = "01 Processamento concluído sem erros";


            }
            catch (Exception ex)
            {
                txt_Status = txt_Status + "Erro: Carga do XML: " + ex.Message + System.Environment.NewLine;


            }
            finally
            {

                if (txt_Status != "")
                {
                    //montar XMLDocument com erro
                    string xRet = @"<?xml version=""1.0"" encoding=""UTF-8""?><Erro>" + txt_Status + "</Erro>";

                    zRet.LoadXml(xRet);


                }

            }


            return zRet;


        }







        [WebMethod]
        public XmlDocument Solicitar_eSocial_2210(string CNPJ, string DataInicial, string DataFinal, string CodUsuario)
        {

            Ilitera.Common.Pessoa rEmpresa = new Ilitera.Common.Pessoa();
            Empregado rEmpregado = new Empregado();

            //int zId = 0;


            string rSelect = "";

            string xRetorno = "";

            string txt_Status = "";

            XmlDocument zRet = new XmlDocument();


            try
            {




                xRetorno = "";


                if (CNPJ.Length < 10 || CNPJ.Length > 14)
                {
                    txt_Status = txt_Status + " Campo CNPJ inválido |";
                    xRetorno = xRetorno + " Campo CNPJ Inválido |";
                }

              

                if (DataInicial.Length != 10)
                {
                    txt_Status = txt_Status + " Campo Data Inicial inválido |";
                    xRetorno = xRetorno + " Campo Data Inicial Inválido |";
                }
                if (Validar_Data(DataInicial) == false)
                {
                    txt_Status = txt_Status + " Campo Data Inicial inválido |";
                    xRetorno = xRetorno + " Campo Data Inicial Inválido |";
                }

                if (DataFinal.Length != 10)
                {
                    txt_Status = txt_Status + " Campo Data Final inválido |";
                    xRetorno = xRetorno + " Campo Data Final Inválido |";
                }
                if (Validar_Data(DataFinal) == false)
                {
                    txt_Status = txt_Status + " Campo Data Final inválido |";
                    xRetorno = xRetorno + " Campo Data Final Inválido |";
                }


                if (xRetorno != "")
                    xRetorno = "50 ( Validação de campos ): " + xRetorno;






                if (txt_Status == "")
                {

                    rEmpresa = new Ilitera.Common.Pessoa();

                    rSelect = " dbo.udf_getnumeric( nomecodigo ) = '" + CNPJ + "' ";

                    //pegar Id Empresa                    
                    rEmpresa.Find(rSelect);

                    //se não achar empresa,  emitir retorno avisando
                    if (rEmpresa.Id == 0)
                    {
                        txt_Status = txt_Status + "Erro: Empresa não localizada ( " + CNPJ + "  )" + System.Environment.NewLine;
                        xRetorno = "03 (Empresa não localizada (" + CNPJ + ")";
                    }

                }


                Cliente zCliente = new Cliente(rEmpresa.Id);



                if (txt_Status == "")
                {

                    //criar
                    DataSet zDs2 = new DataSet();
                    Ilitera.Data.eSocial xLista2 = new Ilitera.Data.eSocial();
                    zDs2 = xLista2.Mensageria_2210(rEmpresa.Id, rEmpregado.Id, 0, DataInicial, DataFinal, System.Convert.ToInt32(CodUsuario), "4", "", "N", zCliente.ESocial_Ambiente);

                    for (int zCont = 0; zCont < zDs2.Tables[0].Rows.Count; zCont++)
                    {

                        string zCod = "";
                                                
                        string xIdAcidente = zDs2.Tables[0].Rows[zCont]["IdAcidente"].ToString().Trim();

                        string zCriado = zDs2.Tables[0].Rows[zCont]["Criado_Em"].ToString().Trim();
                        //verificar se já foi criado o XML


                        if (zCriado == "")
                        {
                            Ilitera.Net.e_Social.Controle_eSocial_2210 z2210 = new Ilitera.Net.e_Social.Controle_eSocial_2210();
                            z2210.Processar_S2210(0, DataInicial, DataFinal, zCod, xIdAcidente, System.Convert.ToInt32( CodUsuario),0 , "1");
                        }


                    }

                }


                //                zRet = GetXmlDocument(
                //new XDocument(
                //     new XElement("Tipo",
                //        new XElement("CNPJ", CNPJ),
                //        new XElement("Laudo", Laudo),
                //        new XElement("Link_Laudo", ""),
                //        new XElement("Conteudo_Arquivo", strXml)
                //        )
                //      ));//.Save(xArq);





                //XmlDocument xml = new XmlDocument();
                //XmlElement root = xml.CreateElement("RETORNO");
                //xml.AppendChild(root);


                string xRetXML = "<RETORNO>";

                //juntar XMLs
                DataSet zDs = new DataSet();
                Ilitera.Data.eSocial xLista = new Ilitera.Data.eSocial();
                zDs = xLista.Mensageria_2210(rEmpresa.Id, rEmpregado.Id, 0, DataInicial, DataFinal, System.Convert.ToInt32(CodUsuario), "4", "", "N", zCliente.ESocial_Ambiente);

                for (int zCont = 0; zCont < zDs.Tables[0].Rows.Count; zCont++)
                {

                    Ilitera.Data.eSocial xDados = new Ilitera.Data.eSocial();

                    DataSet rDs = new DataSet();
                    rDs = xDados.Trazer_XML2(System.Convert.ToInt32(zDs.Tables[0].Rows[zCont]["IdeSocial_Deposito"].ToString().Trim()));

                    string strXML = rDs.Tables[0].Rows[0][0].ToString().Replace("\r\n", string.Empty);


                    //criar array para armazenas string de XML para depois criar o XML de retorno e ir adicionando ?   Ou crio antes do loop o XML e vou adicionando aqui, acho melhor 

                    //XmlElement child = xml.CreateElement("XMLS");
                    //child.SetAttribute("1060", strXML);
                    //root.AppendChild(child);

                    if (strXML.Trim() != "")
                        xRetXML = xRetXML + "<XMLS>" + strXML.Substring(strXML.IndexOf("<eSocial")) + "</XMLS>";

                }

                //zRet = xml;

                xRetXML = xRetXML + "</RETORNO>";

                byte[] encodedString2 = Encoding.UTF8.GetBytes(xRetXML);

                MemoryStream ms2 = new MemoryStream(encodedString2);
                ms2.Flush();
                ms2.Position = 0;

                XmlDocument zLote = new XmlDocument();
                zLote.Load(ms2);

                zRet = zLote;

                xRetorno = "01 Processamento concluído sem erros";


            }
            catch (Exception ex)
            {
                txt_Status = txt_Status + "Erro: Carga do XML: " + ex.Message + System.Environment.NewLine;


            }
            finally
            {

                if (txt_Status != "")
                {
                    //montar XMLDocument com erro
                    string xRet = @"<?xml version=""1.0"" encoding=""UTF-8""?><Erro>" + txt_Status + "</Erro>";

                    zRet.LoadXml(xRet);


                }

            }


            return zRet;


        }





        [WebMethod]
        public XmlDocument Solicitar_eSocial_2220(string CNPJ, string DataInicial, string DataFinal, string CodUsuario)
        {

            Ilitera.Common.Pessoa rEmpresa = new Ilitera.Common.Pessoa();
            Empregado rEmpregado = new Empregado();

            //int zId = 0;


            string rSelect = "";

            string xRetorno = "";

            string txt_Status = "";

            XmlDocument zRet = new XmlDocument();


            try
            {




                xRetorno = "";


                if (CNPJ.Length < 10 || CNPJ.Length > 14)
                {
                    txt_Status = txt_Status + " Campo CNPJ inválido |";
                    xRetorno = xRetorno + " Campo CNPJ Inválido |";
                }



                if (DataInicial.Length != 10)
                {
                    txt_Status = txt_Status + " Campo Data Inicial inválido |";
                    xRetorno = xRetorno + " Campo Data Inicial Inválido |";
                }
                if (Validar_Data(DataInicial) == false)
                {
                    txt_Status = txt_Status + " Campo Data Inicial inválido |";
                    xRetorno = xRetorno + " Campo Data Inicial Inválido |";
                }

                if (DataFinal.Length != 10)
                {
                    txt_Status = txt_Status + " Campo Data Final inválido |";
                    xRetorno = xRetorno + " Campo Data Final Inválido |";
                }
                if (Validar_Data(DataFinal) == false)
                {
                    txt_Status = txt_Status + " Campo Data Final inválido |";
                    xRetorno = xRetorno + " Campo Data Final Inválido |";
                }


                if (xRetorno != "")
                    xRetorno = "50 ( Validação de campos ): " + xRetorno;






                if (txt_Status == "")
                {

                    DataSet rDs = new Ilitera.Common.Pessoa().Get(" dbo.udf_getnumeric( nomecodigo ) = '" + CNPJ + "' ");

                    if ( rDs.Tables[0].Rows.Count < 1)
                    {
                        txt_Status = txt_Status + "Erro: Empresa não localizada ( " + CNPJ + "  )" + System.Environment.NewLine;
                        xRetorno = "03 (Empresa não localizada (" + CNPJ + ")";
                    }

                    //for (int zCont = 0; zCont < rDs.Tables[0].Rows.Count; zCont++)
                



                    //rEmpresa = new Ilitera.Common.Pessoa();

                    //rSelect = " dbo.udf_getnumeric( nomecodigo ) = '" + CNPJ + "' ";

                    ////pegar Id Empresa                    
                    //rEmpresa.Find(rSelect);

                    ////se não achar empresa,  emitir retorno avisando
                    //if (rEmpresa.Id == 0)
                    //{
                    //    txt_Status = txt_Status + "Erro: Empresa não localizada ( " + CNPJ + "  )" + System.Environment.NewLine;
                    //    xRetorno = "03 (Empresa não localizada (" + CNPJ + ")";
                    //}

                }



                Cliente zCliente = new Cliente(rEmpresa.Id);


                if (txt_Status == "")
                {

                    DataSet rDs = new Ilitera.Common.Pessoa().Get(" dbo.udf_getnumeric( nomecodigo ) = '" + CNPJ + "' ");

                    for (int nCont = 0; nCont < rDs.Tables[0].Rows.Count; nCont++)
                    {


                        //criar
                        DataSet zDs2 = new DataSet();
                        Ilitera.Data.eSocial xLista2 = new Ilitera.Data.eSocial();
                        //zDs2 = xLista2.Mensageria_2220(rEmpresa.Id, 0, 0, DataInicial, DataFinal, System.Convert.ToInt32(CodUsuario), "4", "", "N");
                        zDs2 = xLista2.Mensageria_2220( System.Convert.ToInt32( rDs.Tables[0].Rows[nCont]["IdPessoa"].ToString()), 0, 0, DataInicial, DataFinal, System.Convert.ToInt32(CodUsuario), "4", "", "N", zCliente.ESocial_Ambiente);

                        for (int zCont = 0; zCont < zDs2.Tables[0].Rows.Count; zCont++)
                        {

                            string zCod = "";

                            string xIdExameBase = zDs2.Tables[0].Rows[zCont]["IdExameBase"].ToString().Trim();

                            string zCriado = zDs2.Tables[0].Rows[zCont]["Criado_Em"].ToString().Trim();
                            //verificar se já foi criado o XML


                            if (zCriado == "")
                            {
                                Ilitera.Net.e_Social.Controle_eSocial_2220 z2220 = new Ilitera.Net.e_Social.Controle_eSocial_2220();
                                z2220.Processar_S2220(System.Convert.ToInt32(xIdExameBase), DataInicial, DataFinal, zCod, System.Convert.ToInt32(CodUsuario),0,"1");
                            }


                        }

                    }
                }


                //                zRet = GetXmlDocument(
                //new XDocument(
                //     new XElement("Tipo",
                //        new XElement("CNPJ", CNPJ),
                //        new XElement("Laudo", Laudo),
                //        new XElement("Link_Laudo", ""),
                //        new XElement("Conteudo_Arquivo", strXml)
                //        )
                //      ));//.Save(xArq);





                //XmlDocument xml = new XmlDocument();
                //XmlElement root = xml.CreateElement("RETORNO");
                //xml.AppendChild(root);


                string xRetXML = "<RETORNO>";

                DataSet vDs = new Ilitera.Common.Pessoa().Get(" dbo.udf_getnumeric( nomecodigo ) = '" + CNPJ + "' ");

                for (int nCont = 0; nCont < vDs.Tables[0].Rows.Count; nCont++)
                {

                    //juntar XMLs
                    DataSet zDs = new DataSet();
                    Ilitera.Data.eSocial xLista = new Ilitera.Data.eSocial();
                    //zDs = xLista.Mensageria_2220(rEmpresa.Id, 0, 0, DataInicial, DataFinal, System.Convert.ToInt32(CodUsuario), "4", "", "N");
                    zDs = xLista.Mensageria_2220(System.Convert.ToInt32( vDs.Tables[0].Rows[nCont]["IdPessoa"].ToString()), 0, 0, DataInicial, DataFinal, System.Convert.ToInt32(CodUsuario), "4", "", "N", zCliente.ESocial_Ambiente);

                    for (int zCont = 0; zCont < zDs.Tables[0].Rows.Count; zCont++)
                    {

                        Ilitera.Data.eSocial xDados = new Ilitera.Data.eSocial();

                        DataSet rDs = new DataSet();
                        rDs = xDados.Trazer_XML2(System.Convert.ToInt32(zDs.Tables[0].Rows[zCont]["IdeSocial_Deposito"].ToString().Trim()));

                        string strXML = rDs.Tables[0].Rows[0][0].ToString().Replace("\r\n", string.Empty);


                        //criar array para armazenas string de XML para depois criar o XML de retorno e ir adicionando ?   Ou crio antes do loop o XML e vou adicionando aqui, acho melhor 

                        //XmlElement child = xml.CreateElement("XMLS");
                        //child.SetAttribute("1060", strXML);
                        //root.AppendChild(child);

                        if (strXML.Trim() != "")
                            xRetXML = xRetXML + "<XMLS>" + strXML.Substring(strXML.IndexOf("<eSocial")) + "</XMLS>";

                    }
                }
                //zRet = xml;

                xRetXML = xRetXML + "</RETORNO>";

                byte[] encodedString2 = Encoding.UTF8.GetBytes(xRetXML);

                MemoryStream ms2 = new MemoryStream(encodedString2);
                ms2.Flush();
                ms2.Position = 0;

                XmlDocument zLote = new XmlDocument();
                zLote.Load(ms2);

                zRet = zLote;

                xRetorno = "01 Processamento concluído sem erros";


            }
            catch (Exception ex)
            {
                txt_Status = txt_Status + "Erro: Carga do XML: " + ex.Message + System.Environment.NewLine;


            }
            finally
            {

                if (txt_Status != "")
                {
                    //montar XMLDocument com erro
                    string xRet = @"<?xml version=""1.0"" encoding=""UTF-8""?><Erro>" + txt_Status + "</Erro>";

                    zRet.LoadXml(xRet);


                }

            }


            return zRet;


        }







        [WebMethod]
        public XmlDocument Solicitar_eSocial_2221(string CNPJ, string DataInicial, string DataFinal, string CodUsuario)
        {

            Ilitera.Common.Pessoa rEmpresa = new Ilitera.Common.Pessoa();
            Empregado rEmpregado = new Empregado();

            //int zId = 0;


            string rSelect = "";

            string xRetorno = "";

            string txt_Status = "";

            XmlDocument zRet = new XmlDocument();


            try
            {




                xRetorno = "";


                if (CNPJ.Length < 10 || CNPJ.Length > 14)
                {
                    txt_Status = txt_Status + " Campo CNPJ inválido |";
                    xRetorno = xRetorno + " Campo CNPJ Inválido |";
                }



                if (DataInicial.Length != 10)
                {
                    txt_Status = txt_Status + " Campo Data Inicial inválido |";
                    xRetorno = xRetorno + " Campo Data Inicial Inválido |";
                }
                if (Validar_Data(DataInicial) == false)
                {
                    txt_Status = txt_Status + " Campo Data Inicial inválido |";
                    xRetorno = xRetorno + " Campo Data Inicial Inválido |";
                }

                if (DataFinal.Length != 10)
                {
                    txt_Status = txt_Status + " Campo Data Final inválido |";
                    xRetorno = xRetorno + " Campo Data Final Inválido |";
                }
                if (Validar_Data(DataFinal) == false)
                {
                    txt_Status = txt_Status + " Campo Data Final inválido |";
                    xRetorno = xRetorno + " Campo Data Final Inválido |";
                }


                if (xRetorno != "")
                    xRetorno = "50 ( Validação de campos ): " + xRetorno;






                if (txt_Status == "")
                {

                    rEmpresa = new Ilitera.Common.Pessoa();

                    rSelect = " dbo.udf_getnumeric( nomecodigo ) = '" + CNPJ + "' ";

                    //pegar Id Empresa                    
                    rEmpresa.Find(rSelect);

                    //se não achar empresa,  emitir retorno avisando
                    if (rEmpresa.Id == 0)
                    {
                        txt_Status = txt_Status + "Erro: Empresa não localizada ( " + CNPJ + "  )" + System.Environment.NewLine;
                        xRetorno = "03 (Empresa não localizada (" + CNPJ + ")";
                    }

                }






                if (txt_Status == "")
                {

                    //criar
                    DataSet zDs2 = new DataSet();
                    Ilitera.Data.eSocial xLista2 = new Ilitera.Data.eSocial();
                    zDs2 = xLista2.Mensageria_2221(rEmpresa.Id, 0, 0, DataInicial, DataFinal, System.Convert.ToInt32(CodUsuario), "4", "", "N");

                    for (int zCont = 0; zCont < zDs2.Tables[0].Rows.Count; zCont++)
                    {

                        string zCod = "";

                        string xIdExameBase = zDs2.Tables[0].Rows[zCont]["IdExameBase"].ToString().Trim();

                        string zCriado = zDs2.Tables[0].Rows[zCont]["Criado_Em"].ToString().Trim();
                        //verificar se já foi criado o XML


                        if (zCriado == "")
                        {
                            Ilitera.Net.e_Social.Controle_eSocial_2221 z2221 = new Ilitera.Net.e_Social.Controle_eSocial_2221();
                            //z2221.Processar_S2221(System.Convert.ToInt32(xIdExameBase), DataInicial, DataFinal, zCod, System.Convert.ToInt32(CodUsuario));
                        }


                    }

                }


                //                zRet = GetXmlDocument(
                //new XDocument(
                //     new XElement("Tipo",
                //        new XElement("CNPJ", CNPJ),
                //        new XElement("Laudo", Laudo),
                //        new XElement("Link_Laudo", ""),
                //        new XElement("Conteudo_Arquivo", strXml)
                //        )
                //      ));//.Save(xArq);





                //XmlDocument xml = new XmlDocument();
                //XmlElement root = xml.CreateElement("RETORNO");
                //xml.AppendChild(root);


                string xRetXML = "<RETORNO>";

                //juntar XMLs
                DataSet zDs = new DataSet();
                Ilitera.Data.eSocial xLista = new Ilitera.Data.eSocial();
                zDs = xLista.Mensageria_2221(rEmpresa.Id, 0, 0, DataInicial, DataFinal, System.Convert.ToInt32(CodUsuario), "4", "", "N");

                for (int zCont = 0; zCont < zDs.Tables[0].Rows.Count; zCont++)
                {

                    Ilitera.Data.eSocial xDados = new Ilitera.Data.eSocial();

                    DataSet rDs = new DataSet();
                    rDs = xDados.Trazer_XML2(System.Convert.ToInt32(zDs.Tables[0].Rows[zCont]["IdeSocial_Deposito"].ToString().Trim()));

                    string strXML = rDs.Tables[0].Rows[0][0].ToString().Replace("\r\n", string.Empty);


                    //criar array para armazenas string de XML para depois criar o XML de retorno e ir adicionando ?   Ou crio antes do loop o XML e vou adicionando aqui, acho melhor 

                    //XmlElement child = xml.CreateElement("XMLS");
                    //child.SetAttribute("1060", strXML);
                    //root.AppendChild(child);

                    if (strXML.Trim() != "")
                        xRetXML = xRetXML + "<XMLS>" + strXML.Substring(strXML.IndexOf("<eSocial")) + "</XMLS>";

                }

                //zRet = xml;

                xRetXML = xRetXML + "</RETORNO>";

                byte[] encodedString2 = Encoding.UTF8.GetBytes(xRetXML);

                MemoryStream ms2 = new MemoryStream(encodedString2);
                ms2.Flush();
                ms2.Position = 0;

                XmlDocument zLote = new XmlDocument();
                zLote.Load(ms2);

                zRet = zLote;

                xRetorno = "01 Processamento concluído sem erros";


            }
            catch (Exception ex)
            {
                txt_Status = txt_Status + "Erro: Carga do XML: " + ex.Message + System.Environment.NewLine;


            }
            finally
            {

                if (txt_Status != "")
                {
                    //montar XMLDocument com erro
                    string xRet = @"<?xml version=""1.0"" encoding=""UTF-8""?><Erro>" + txt_Status + "</Erro>";

                    zRet.LoadXml(xRet);


                }

            }


            return zRet;


        }





        [WebMethod]
        public XmlDocument Solicitar_eSocial_2230(string CNPJ, string DataInicial, string DataFinal, string CodUsuario)
        {

            Ilitera.Common.Pessoa rEmpresa = new Ilitera.Common.Pessoa();
            Empregado rEmpregado = new Empregado();

            //int zId = 0;


            string rSelect = "";

            string xRetorno = "";

            string txt_Status = "";

            XmlDocument zRet = new XmlDocument();


            try
            {




                xRetorno = "";


                if (CNPJ.Length < 10 || CNPJ.Length > 14)
                {
                    txt_Status = txt_Status + " Campo CNPJ inválido |";
                    xRetorno = xRetorno + " Campo CNPJ Inválido |";
                }



                if (DataInicial.Length != 10)
                {
                    txt_Status = txt_Status + " Campo Data Inicial inválido |";
                    xRetorno = xRetorno + " Campo Data Inicial Inválido |";
                }
                if (Validar_Data(DataInicial) == false)
                {
                    txt_Status = txt_Status + " Campo Data Inicial inválido |";
                    xRetorno = xRetorno + " Campo Data Inicial Inválido |";
                }

                if (DataFinal.Length != 10)
                {
                    txt_Status = txt_Status + " Campo Data Final inválido |";
                    xRetorno = xRetorno + " Campo Data Final Inválido |";
                }
                if (Validar_Data(DataFinal) == false)
                {
                    txt_Status = txt_Status + " Campo Data Final inválido |";
                    xRetorno = xRetorno + " Campo Data Final Inválido |";
                }


                if (xRetorno != "")
                    xRetorno = "50 ( Validação de campos ): " + xRetorno;






                if (txt_Status == "")
                {

                    rEmpresa = new Ilitera.Common.Pessoa();

                    rSelect = " dbo.udf_getnumeric( nomecodigo ) = '" + CNPJ + "' ";

                    //pegar Id Empresa                    
                    rEmpresa.Find(rSelect);

                    //se não achar empresa,  emitir retorno avisando
                    if (rEmpresa.Id == 0)
                    {
                        txt_Status = txt_Status + "Erro: Empresa não localizada ( " + CNPJ + "  )" + System.Environment.NewLine;
                        xRetorno = "03 (Empresa não localizada (" + CNPJ + ")";
                    }

                }


                Cliente zCliente = new Cliente(rEmpresa.Id);



                if (txt_Status == "")
                {

                    //criar
                    DataSet zDs2 = new DataSet();
                    Ilitera.Data.eSocial xLista2 = new Ilitera.Data.eSocial();
                    zDs2 = xLista2.Mensageria_2230(rEmpresa.Id, 0, 0, DataInicial, DataFinal, System.Convert.ToInt32(CodUsuario), "4", "", "N", zCliente.ESocial_Ambiente);

                    for (int zCont = 0; zCont < zDs2.Tables[0].Rows.Count; zCont++)
                    {

                        string zCod = "";

                        string xIdAfastamento = zDs2.Tables[0].Rows[zCont]["IdAfastamento"].ToString().Trim();

                        string zCriado = zDs2.Tables[0].Rows[zCont]["Criado_Em"].ToString().Trim();
                        //verificar se já foi criado o XML


                        if (zCriado == "")
                        {
                            Ilitera.Net.e_Social.Controle_eSocial_2230 z2230 = new Ilitera.Net.e_Social.Controle_eSocial_2230();
                            z2230.Processar_S2230(System.Convert.ToInt32(xIdAfastamento), DataInicial, DataFinal, zCod, System.Convert.ToInt32(CodUsuario),0);
                        }


                    }

                }


                //                zRet = GetXmlDocument(
                //new XDocument(
                //     new XElement("Tipo",
                //        new XElement("CNPJ", CNPJ),
                //        new XElement("Laudo", Laudo),
                //        new XElement("Link_Laudo", ""),
                //        new XElement("Conteudo_Arquivo", strXml)
                //        )
                //      ));//.Save(xArq);





                //XmlDocument xml = new XmlDocument();
                //XmlElement root = xml.CreateElement("RETORNO");
                //xml.AppendChild(root);


                string xRetXML = "<RETORNO>";

                //juntar XMLs
                DataSet zDs = new DataSet();
                Ilitera.Data.eSocial xLista = new Ilitera.Data.eSocial();
                zDs = xLista.Mensageria_2230(rEmpresa.Id, 0, 0, DataInicial, DataFinal, System.Convert.ToInt32(CodUsuario), "4", "", "N", zCliente.ESocial_Ambiente);

                for (int zCont = 0; zCont < zDs.Tables[0].Rows.Count; zCont++)
                {

                    Ilitera.Data.eSocial xDados = new Ilitera.Data.eSocial();

                    DataSet rDs = new DataSet();
                    rDs = xDados.Trazer_XML2(System.Convert.ToInt32(zDs.Tables[0].Rows[zCont]["IdeSocial_Deposito"].ToString().Trim()));

                    string strXML = rDs.Tables[0].Rows[0][0].ToString().Replace("\r\n", string.Empty);


                    //criar array para armazenas string de XML para depois criar o XML de retorno e ir adicionando ?   Ou crio antes do loop o XML e vou adicionando aqui, acho melhor 

                    //XmlElement child = xml.CreateElement("XMLS");
                    //child.SetAttribute("1060", strXML);
                    //root.AppendChild(child);

                    if (strXML.Trim() != "")
                        xRetXML = xRetXML + "<XMLS>" + strXML.Substring(strXML.IndexOf("<eSocial")) + "</XMLS>";

                }

                //zRet = xml;

                xRetXML = xRetXML + "</RETORNO>";

                byte[] encodedString2 = Encoding.UTF8.GetBytes(xRetXML);

                MemoryStream ms2 = new MemoryStream(encodedString2);
                ms2.Flush();
                ms2.Position = 0;

                XmlDocument zLote = new XmlDocument();
                zLote.Load(ms2);

                zRet = zLote;

                xRetorno = "01 Processamento concluído sem erros";


            }
            catch (Exception ex)
            {
                txt_Status = txt_Status + "Erro: Carga do XML: " + ex.Message + System.Environment.NewLine;


            }
            finally
            {

                if (txt_Status != "")
                {
                    //montar XMLDocument com erro
                    string xRet = @"<?xml version=""1.0"" encoding=""UTF-8""?><Erro>" + txt_Status + "</Erro>";

                    zRet.LoadXml(xRet);


                }

            }


            return zRet;


        }






        [WebMethod]
        public XmlDocument Solicitar_eSocial_2240(string CNPJ, string DataInicial, string DataFinal, string CodUsuario)
        {

            Ilitera.Common.Pessoa rEmpresa = new Ilitera.Common.Pessoa();
            Empregado rEmpregado = new Empregado();

            //int zId = 0;


            string rSelect = "";

            string xRetorno = "";

            string txt_Status = "";

            XmlDocument zRet = new XmlDocument();


            try
            {




                xRetorno = "";


                if (CNPJ.Length < 10 || CNPJ.Length > 14)
                {
                    txt_Status = txt_Status + " Campo CNPJ inválido |";
                    xRetorno = xRetorno + " Campo CNPJ Inválido |";
                }



                if (DataInicial.Length != 10)
                {
                    txt_Status = txt_Status + " Campo Data Inicial inválido |";
                    xRetorno = xRetorno + " Campo Data Inicial Inválido |";
                }
                if (Validar_Data(DataInicial) == false)
                {
                    txt_Status = txt_Status + " Campo Data Inicial inválido |";
                    xRetorno = xRetorno + " Campo Data Inicial Inválido |";
                }

                if (DataFinal.Length != 10)
                {
                    txt_Status = txt_Status + " Campo Data Final inválido |";
                    xRetorno = xRetorno + " Campo Data Final Inválido |";
                }
                if (Validar_Data(DataFinal) == false)
                {
                    txt_Status = txt_Status + " Campo Data Final inválido |";
                    xRetorno = xRetorno + " Campo Data Final Inválido |";
                }


                if (xRetorno != "")
                    xRetorno = "50 ( Validação de campos ): " + xRetorno;






                if (txt_Status == "")
                {

                    rEmpresa = new Ilitera.Common.Pessoa();

                    rSelect = " dbo.udf_getnumeric( nomecodigo ) = '" + CNPJ + "' ";

                    //pegar Id Empresa                    
                    rEmpresa.Find(rSelect);

                    //se não achar empresa,  emitir retorno avisando
                    if (rEmpresa.Id == 0)
                    {
                        txt_Status = txt_Status + "Erro: Empresa não localizada ( " + CNPJ + "  )" + System.Environment.NewLine;
                        xRetorno = "03 (Empresa não localizada (" + CNPJ + ")";
                    }

                }


                Cliente zCliente = new Cliente(rEmpresa.Id);



                if (txt_Status == "")
                {

                    //criar
                    DataSet zDs2 = new DataSet();
                    Ilitera.Data.eSocial xLista2 = new Ilitera.Data.eSocial();
                    zDs2 = xLista2.Mensageria_2240(rEmpresa.Id, 0, 0, DataInicial, DataFinal, System.Convert.ToInt32(CodUsuario), "4", "", "N", zCliente.ESocial_Ambiente);

                    for (int zCont = 0; zCont < zDs2.Tables[0].Rows.Count; zCont++)
                    {

                        string zCod = "";

                        string xIdColaborador = zDs2.Tables[0].Rows[zCont]["IdEmpregado"].ToString().Trim();

                        string zCriado = zDs2.Tables[0].Rows[zCont]["Criado_Em"].ToString().Trim();
                        //verificar se já foi criado o XML


                        if (zCriado == "")
                        {
                            Ilitera.Net.e_Social.Controle_eSocial_2240 z2240 = new Ilitera.Net.e_Social.Controle_eSocial_2240();
                            z2240.Processar_S2240(System.Convert.ToInt32(xIdColaborador), DataInicial, DataFinal, zCod, System.Convert.ToInt32(CodUsuario),0,0,0,"1");
                        }


                    }

                }


                //                zRet = GetXmlDocument(
                //new XDocument(
                //     new XElement("Tipo",
                //        new XElement("CNPJ", CNPJ),
                //        new XElement("Laudo", Laudo),
                //        new XElement("Link_Laudo", ""),
                //        new XElement("Conteudo_Arquivo", strXml)
                //        )
                //      ));//.Save(xArq);





                //XmlDocument xml = new XmlDocument();
                //XmlElement root = xml.CreateElement("RETORNO");
                //xml.AppendChild(root);


                string xRetXML = "<RETORNO>";

                //juntar XMLs
                DataSet zDs = new DataSet();
                Ilitera.Data.eSocial xLista = new Ilitera.Data.eSocial();
                zDs = xLista.Mensageria_2240(rEmpresa.Id, 0, 0, DataInicial, DataFinal, System.Convert.ToInt32(CodUsuario), "4", "", "N", zCliente.ESocial_Ambiente);

                for (int zCont = 0; zCont < zDs.Tables[0].Rows.Count; zCont++)
                {

                    Ilitera.Data.eSocial xDados = new Ilitera.Data.eSocial();

                    if (zDs.Tables[0].Rows[zCont]["IdeSocial_Deposito"].ToString().Trim() != "")
                    {

                        DataSet rDs = new DataSet();
                        rDs = xDados.Trazer_XML2(System.Convert.ToInt32(zDs.Tables[0].Rows[zCont]["IdeSocial_Deposito"].ToString().Trim()));

                        string strXML = rDs.Tables[0].Rows[0][0].ToString().Replace("\r\n", string.Empty);


                        //criar array para armazenas string de XML para depois criar o XML de retorno e ir adicionando ?   Ou crio antes do loop o XML e vou adicionando aqui, acho melhor 

                        //XmlElement child = xml.CreateElement("XMLS");
                        //child.SetAttribute("1060", strXML);
                        //root.AppendChild(child);

                        if (strXML.Trim() != "")
                            xRetXML = xRetXML + "<XMLS>" + strXML.Substring(strXML.IndexOf("<eSocial")) + "</XMLS>";

                    }

                }

                //zRet = xml;

                xRetXML = xRetXML + "</RETORNO>";

                byte[] encodedString2 = Encoding.UTF8.GetBytes(xRetXML);

                MemoryStream ms2 = new MemoryStream(encodedString2);
                ms2.Flush();
                ms2.Position = 0;

                XmlDocument zLote = new XmlDocument();
                zLote.Load(ms2);

                zRet = zLote;

                xRetorno = "01 Processamento concluído sem erros";


            }
            catch (Exception ex)
            {
                txt_Status = txt_Status + "Erro: Carga do XML: " + ex.Message + System.Environment.NewLine;


            }
            finally
            {

                if (txt_Status != "")
                {
                    //montar XMLDocument com erro
                    string xRet = @"<?xml version=""1.0"" encoding=""UTF-8""?><Erro>" + txt_Status + "</Erro>";

                    zRet.LoadXml(xRet);


                }

            }


            return zRet;


        }






        [WebMethod]
        public XmlDocument Solicitar_eSocial_2245(string CNPJ, string DataInicial, string DataFinal, string CodUsuario)
        {

            Ilitera.Common.Pessoa rEmpresa = new Ilitera.Common.Pessoa();
            Empregado rEmpregado = new Empregado();

            //int zId = 0;


            string rSelect = "";

            string xRetorno = "";

            string txt_Status = "";

            XmlDocument zRet = new XmlDocument();


            try
            {




                xRetorno = "";


                if (CNPJ.Length < 10 || CNPJ.Length > 14)
                {
                    txt_Status = txt_Status + " Campo CNPJ inválido |";
                    xRetorno = xRetorno + " Campo CNPJ Inválido |";
                }



                if (DataInicial.Length != 10)
                {
                    txt_Status = txt_Status + " Campo Data Inicial inválido |";
                    xRetorno = xRetorno + " Campo Data Inicial Inválido |";
                }
                if (Validar_Data(DataInicial) == false)
                {
                    txt_Status = txt_Status + " Campo Data Inicial inválido |";
                    xRetorno = xRetorno + " Campo Data Inicial Inválido |";
                }

                if (DataFinal.Length != 10)
                {
                    txt_Status = txt_Status + " Campo Data Final inválido |";
                    xRetorno = xRetorno + " Campo Data Final Inválido |";
                }
                if (Validar_Data(DataFinal) == false)
                {
                    txt_Status = txt_Status + " Campo Data Final inválido |";
                    xRetorno = xRetorno + " Campo Data Final Inválido |";
                }


                if (xRetorno != "")
                    xRetorno = "50 ( Validação de campos ): " + xRetorno;






                if (txt_Status == "")
                {

                    rEmpresa = new Ilitera.Common.Pessoa();

                    rSelect = " dbo.udf_getnumeric( nomecodigo ) = '" + CNPJ + "' ";

                    //pegar Id Empresa                    
                    rEmpresa.Find(rSelect);

                    //se não achar empresa,  emitir retorno avisando
                    if (rEmpresa.Id == 0)
                    {
                        txt_Status = txt_Status + "Erro: Empresa não localizada ( " + CNPJ + "  )" + System.Environment.NewLine;
                        xRetorno = "03 (Empresa não localizada (" + CNPJ + ")";
                    }

                }






                if (txt_Status == "")
                {

                    //criar
                    DataSet zDs2 = new DataSet();
                    Ilitera.Data.eSocial xLista2 = new Ilitera.Data.eSocial();
                    zDs2 = xLista2.Mensageria_2245(rEmpresa.Id, 0, 0, DataInicial, DataFinal, System.Convert.ToInt32(CodUsuario), "4", "", "N");

                    for (int zCont = 0; zCont < zDs2.Tables[0].Rows.Count; zCont++)
                    {

                        string zCod = "";

                        string xIdColaborador = zDs2.Tables[0].Rows[zCont]["IdParticipanteTreinamento"].ToString().Trim();

                        string zCriado = zDs2.Tables[0].Rows[zCont]["Criado_Em"].ToString().Trim();
                        //verificar se já foi criado o XML


                        if (zCriado == "")
                        {
                            Ilitera.Net.e_Social.Controle_eSocial_2245 z2245 = new Ilitera.Net.e_Social.Controle_eSocial_2245();
                            z2245.Processar_S2245(System.Convert.ToInt32(xIdColaborador), DataInicial, DataFinal, zCod, System.Convert.ToInt32(CodUsuario));
                        }


                    }

                }


                //                zRet = GetXmlDocument(
                //new XDocument(
                //     new XElement("Tipo",
                //        new XElement("CNPJ", CNPJ),
                //        new XElement("Laudo", Laudo),
                //        new XElement("Link_Laudo", ""),
                //        new XElement("Conteudo_Arquivo", strXml)
                //        )
                //      ));//.Save(xArq);





                //XmlDocument xml = new XmlDocument();
                //XmlElement root = xml.CreateElement("RETORNO");
                //xml.AppendChild(root);


                string xRetXML = "<RETORNO>";

                //juntar XMLs
                DataSet zDs = new DataSet();
                Ilitera.Data.eSocial xLista = new Ilitera.Data.eSocial();
                zDs = xLista.Mensageria_2245(rEmpresa.Id, 0, 0, DataInicial, DataFinal, System.Convert.ToInt32(CodUsuario), "4", "", "N");

                for (int zCont = 0; zCont < zDs.Tables[0].Rows.Count; zCont++)
                {

                    Ilitera.Data.eSocial xDados = new Ilitera.Data.eSocial();

                    if (zDs.Tables[0].Rows[zCont]["IdeSocial_Deposito"].ToString().Trim() != "")
                    {

                        DataSet rDs = new DataSet();
                        rDs = xDados.Trazer_XML2(System.Convert.ToInt32(zDs.Tables[0].Rows[zCont]["IdeSocial_Deposito"].ToString().Trim()));

                        string strXML = rDs.Tables[0].Rows[0][0].ToString().Replace("\r\n", string.Empty);


                        //criar array para armazenas string de XML para depois criar o XML de retorno e ir adicionando ?   Ou crio antes do loop o XML e vou adicionando aqui, acho melhor 

                        //XmlElement child = xml.CreateElement("XMLS");
                        //child.SetAttribute("1060", strXML);
                        //root.AppendChild(child);

                        if (strXML.Trim() != "")
                            xRetXML = xRetXML + "<XMLS>" + strXML.Substring(strXML.IndexOf("<eSocial")) + "</XMLS>";

                    }

                }

                //zRet = xml;

                xRetXML = xRetXML + "</RETORNO>";

                byte[] encodedString2 = Encoding.UTF8.GetBytes(xRetXML);

                MemoryStream ms2 = new MemoryStream(encodedString2);
                ms2.Flush();
                ms2.Position = 0;

                XmlDocument zLote = new XmlDocument();
                zLote.Load(ms2);

                zRet = zLote;

                xRetorno = "01 Processamento concluído sem erros";


            }
            catch (Exception ex)
            {
                txt_Status = txt_Status + "Erro: Carga do XML: " + ex.Message + System.Environment.NewLine;


            }
            finally
            {

                if (txt_Status != "")
                {
                    //montar XMLDocument com erro
                    string xRet = @"<?xml version=""1.0"" encoding=""UTF-8""?><Erro>" + txt_Status + "</Erro>";

                    zRet.LoadXml(xRet);


                }

            }


            return zRet;


        }






        [WebMethod]
        public XmlDocument Eventos_eSocial_1060(string CNPJ, string DataInicial, string DataFinal, string CodUsuario)
        {

            Ilitera.Common.Pessoa rEmpresa = new Ilitera.Common.Pessoa();

            //int zId = 0;


            string rSelect = "";

            string xRetorno = "";

            string txt_Status = "";

            XmlDocument zRet = new XmlDocument();


            try
            {




                xRetorno = "";


                if (CNPJ.Length > 14)
                {
                    txt_Status = txt_Status + " Campo CNPJ inválido |";
                    xRetorno = xRetorno + " Campo CNPJ Inválido |";
                }

                if (DataInicial.Length != 10)
                {
                    txt_Status = txt_Status + " Campo Data Inicial inválido |";
                    xRetorno = xRetorno + " Campo Data Inicial Inválido |";
                }
                if (Validar_Data(DataInicial) == false)
                {
                    txt_Status = txt_Status + " Campo Data Inicial inválido |";
                    xRetorno = xRetorno + " Campo Data Inicial Inválido |";
                }

                if (DataFinal.Length != 10)
                {
                    txt_Status = txt_Status + " Campo Data Final inválido |";
                    xRetorno = xRetorno + " Campo Data Final Inválido |";
                }
                if (Validar_Data(DataFinal) == false)
                {
                    txt_Status = txt_Status + " Campo Data Final inválido |";
                    xRetorno = xRetorno + " Campo Data Final Inválido |";
                }


                if (xRetorno != "")
                    xRetorno = "50 ( Validação de campos ): " + xRetorno;






                if (txt_Status == "")
                {

                    rEmpresa = new Ilitera.Common.Pessoa();

                    rSelect = " dbo.udf_getnumeric( nomecodigo ) = '" + CNPJ + "' ";

                    //pegar Id Empresa                    
                    rEmpresa.Find(rSelect);

                    //se não achar empresa,  emitir retorno avisando
                    if (rEmpresa.Id == 0)
                    {
                        txt_Status = txt_Status + "Erro: Empresa não localizada ( " + CNPJ + "  )" + System.Environment.NewLine;
                        xRetorno = "03 (Empresa não localizada (" + CNPJ + ")";
                    }

                }




                if (txt_Status == "")
                {

                    //criar
                    DataSet zDs2 = new DataSet();
                    Ilitera.Data.eSocial xLista2 = new Ilitera.Data.eSocial();
                    zDs2 = xLista2.Mensageria_1060(rEmpresa.Id, 0, 0, DataInicial, DataFinal, System.Convert.ToInt32(CodUsuario), "4", "", "N");

                    //ele retorna criações distintas do mesmo evento.  Acho que o certo é retornar apenas a última, ver se isso foi sujeira de testes ou se é assim mesmo

                    zDs2.Tables[0].Columns.Remove("Empresa");
                    zDs2.Tables[0].Columns.Remove("PIS");
                    zDs2.Tables[0].Columns.Remove("Colaborador");
                    zDs2.Tables[0].Columns.Remove("DtLaudo");
                    zDs2.Tables[0].Columns.Remove("IdeSocial");
                    zDs2.Tables[0].Columns.Remove("IdPessoa");
                    zDs2.Tables[0].Columns.Remove("NomeArquivo");
                    zDs2.Tables[0].Columns.Remove("NomeUsuario");
                    zDs2.Tables[0].Columns.Remove("Criado_Por");
                    zDs2.Tables[0].Columns.Remove("IdEmpregado");
                    zDs2.Tables[0].Columns.Remove("checkbox");
                    zDs2.Tables[0].Columns.Remove("Enviado_Em");
                    zDs2.Tables[0].Columns.Remove("nId_Laud_Tec");


                    DataColumn col = zDs2.Tables[0].Columns["IdeSocial_Deposito"];                    
                    DataColumn col2 = zDs2.Tables[0].Columns["Criado_Em"];

                    foreach (DataRow row in zDs2.Tables[0].Rows)
                    {
                        if (row.IsNull(col)) row[col] = 0;                        
                        if (row.IsNull(col2)) row[col2] = "";

                    }

                    
                    
                    

                    zDs2.DataSetName = "Retorno";
                    zDs2.Tables[0].TableName = "Evento_1060";

                    string xstrXML = zDs2.GetXml();

                    zRet.LoadXml(xstrXML);

                    xRetorno = "01 Processamento concluído sem erros";

                }


            }
            catch (Exception ex)
            {
                txt_Status = txt_Status + "Erro: Carga do XML: " + ex.Message + System.Environment.NewLine;


            }
            finally
            {

                if (txt_Status != "")
                {
                    //montar XMLDocument com erro
                    string xRet = @"<?xml version=""1.0"" encoding=""UTF-8""?><Erro>" + txt_Status + "</Erro>";

                    zRet.LoadXml(xRet);


                }

            }


            return zRet;


        }



    }
}
