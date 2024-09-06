using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Ilitera.Opsa.Data;
using System.Text;
using Entities;
using System.IO;
using System.Data.OleDb;

using System.Xml;
using System.Xml.Serialization;

using System.Security.Cryptography.X509Certificates;
using System.Net;

using System.Security.Cryptography;
using System.Security.Cryptography.Xml;
using Ilitera.Net.Comunicacao_Dec;

//using System.ServiceModel;


namespace Ilitera.Net
{
    public partial class CheckXML : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    GenerateExcelData("Select");
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alerta", String.Format("alert('{0}');", ex.Message), true);
            }
        }

    
                  



        protected void btnProjeto_Click(object sender, EventArgs e)
        {

            //System.IO.FileInfo infoFile = new System.IO.FileInfo(File1.PostedFile.FileName.ToString());


            //string fileName = infoFile.FullName;  //File1.PostedFile.FileName.Trim();

            //if (fileName == "")
            //    return;

            //string fileName = "I:\\temp\\ExameAd.xml";

            try
            {

                //System.IO.StreamReader reader = new System.IO.StreamReader(fileName);
                //string ret = reader.ReadToEnd();
                //reader.Close();
                string ret = "";

                System.IO.StreamReader reader = new System.IO.StreamReader(File1.FileContent);
                do
                {
                    string textLine = reader.ReadLine();

                    ret = ret + textLine;


                }
                while (reader.Peek() != -1);
                                
                reader.Close();



                Session["XML"] = ret;
                
                Response.Redirect("~/Comunicacao.aspx");
            }
            catch ( Exception Ex)
            {

                MsgBox1.Show("Ilitera.Net", "Erro: " + Ex.Message, null,
                new EO.Web.MsgBoxButton("OK"));

            }

            return;

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            // i:\\temp\\Absenteismo_Sumarizado_2014.pdf

            byte[] arrBytes = File.ReadAllBytes(@"i:\temp\Absenteismo_Sumarizado_2014.pdf");
            string strXml = Convert.ToBase64String(arrBytes);
            File.WriteAllText(@"i:\temp\teste_xml.txt", strXml);

        }



        protected void btnTesteAgendamento_Click(object sender, EventArgs e)
        {

            Response.Redirect("Agendamento_Auto.aspx?xToken=1000");

        }

        protected void btnProjetoFecomercio_Click(object sender, EventArgs e)
        {

            try
            {

                //System.IO.StreamReader reader = new System.IO.StreamReader(fileName);
                //string ret = reader.ReadToEnd();
                //reader.Close();
                string ret = "";

                System.IO.StreamReader reader = new System.IO.StreamReader(File1.FileContent);
                do
                {
                    string textLine = reader.ReadLine();

                    ret = ret + textLine;


                }
                while (reader.Peek() != -1);

                reader.Close();



                Session["XML"] = ret;

                Response.Redirect("~/Comunicacao_Fec.aspx");
            }
            catch (Exception Ex)
            {

                MsgBox1.Show("Ilitera.Net", "Erro: " + Ex.Message, null,
                new EO.Web.MsgBoxButton("OK"));

            }

            return;


        }

        protected void btnESocial_Click(object sender, EventArgs e)
        {


            string xmlData = "";

            System.IO.StreamReader reader = new System.IO.StreamReader("I:\\temp\\evento.xml");
            do
            {
                string textLine = reader.ReadLine();

                xmlData = xmlData + textLine;


            }
            while (reader.Peek() != -1);

            reader.Close();





            //XDocument doc = XDocument.Parse(xmlData);

            ////var first = doc.Root.Elements("customer").Elements("first").Elements();

            //var points = doc.Descendants("retornoEventos");




            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xmlData);

            var nsmgr = new XmlNamespaceManager(doc.NameTable);
            nsmgr.AddNamespace("rate", "http://www.esocial.gov.br/schema/lote/eventos/envio/retornoProcessamento/v1_3_0");
            //var node = doc.SelectSingleNode("//rate:retornoEventos", nsmgr);
            var node = doc.SelectSingleNode("//rate:evento[@Id='ID1178556160000002019051613512400000']", nsmgr);

            //var nodes = doc.SelectSingleNode("/retornoProcessamentoLoteEventos/retornoEventos").ChildNodes;








            //string web_service_teste = "https://webservices.producaorestrita.esocial.gov.br/servicos/empregador/enviarloteeventos/WsEnviarLoteEventos.svc";

            //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;
            //string url = web_service_teste;
            //string response = "";

            ////X509Certificate2 cert = new X509Certificate2(@"D:\Projetos\certificados\xxxxx.pfx", "xxxxx");
            //X509Certificate2 cert;
            //var oX509Cert = new X509Certificate2();
            //var store = new X509Store("MY", StoreLocation.CurrentUser);
            //store.Open(OpenFlags.ReadOnly | OpenFlags.OpenExistingOnly);
            //var collection = store.Certificates;
            //var collection1 = collection.Find(X509FindType.FindByTimeValid, DateTime.Now, false);
            //var collection2 = collection.Find(X509FindType.FindByKeyUsage, X509KeyUsageFlags.DigitalSignature, true);
            //var scollection = X509Certificate2UI.SelectFromCollection(collection2,
            //    "Certificado(s) Digital(is) disponível(is)", "Selecione o certificado digital para uso no aplicativo",
            //    X509SelectionFlag.MultiSelection);

            //if (scollection.Count == 0)
            //{
            //    var msgResultado =
            //        "Nenhum certificado digital foi selecionado ou o certificado selecionado está com problemas.";
            //    MsgBox1.Show("Ilitera.Net", msgResultado, null,
            //    new EO.Web.MsgBoxButton("OK"));                
            //    return;
            //}
            //else
            //{
            //    oX509Cert = scollection[0];
            //    cert = oX509Cert;
            //    //txt_Status.Text = txt_Status.Text + oCertificado.IssuerName.Name + " - " + oCertificado.SerialNumber + " - " + oCertificado.NotBefore + " à " + oCertificado.NotAfter + System.Environment.NewLine;
            //}


            ////https://pt.stackoverflow.com/questions/256824/problema-ao-comunicar-com-webservice-do-esocial/277465#277465
            ////https://pt.stackoverflow.com/questions/239342/problemas-na-comunica%C3%A7%C3%A3o-com-o-webservice-disponibilizado-pelo-governo?rq=1


            ////TESTAR///////////////////////////////////////
            ////  https://pt.stackoverflow.com/questions/256824/problema-ao-comunicar-com-webservice-do-esocial
            ////  https://stackoverflow.com/questions/41496773/how-to-reference-a-wsdl-file-using-visual-studio-code

            ////var urlServicoEnvio = @"https://webservices.producaorestrita.esocial.gov.br/servicos/empregador/enviarloteeventos/WsEnviarLoteEventos.svc";
            ////var address = new System.ServiceModel.EndpointAddress(urlServicoEnvio);



            ////var binding = new BasicHttpsBinding();  //Disponível desde .NET Framework 4.5
            //// ou:

            ////var binding = new BasicHttpBinding( System.ServiceModel.BasicHttpsSecurityMode.Transport);

            ////var binding = new System.ServiceModel.BasicHttpBinding( BasicHttpSecurityMode.Transport);
            ////binding.Security.Transport.ClientCredentialType = System.ServiceModel.HttpClientCredentialType.Certificate;





            ////https://pt.stackoverflow.com/questions/281314/o-documento-enviado-n%C3%A3o-%C3%A9-um-xml-valido-do-esocial
            ////http://www.javac.com.br/jc/posts/list/45/2866-esocial.page




            //// System.ServiceModel.BasicHttpBinding xBinding = new BasicHttpBinding(BasicHttpSecurityMode.Transport);
            //// string xURL = @"https://webservices.producaorestrita.esocial.gov.br/servicos/empregador/enviarloteeventos/WsEnviarLoteEventos.svc";




            ////gerar XML dos endereços abaixo, e adicionar como webreference - Add Service Reference -> opção Advanced -> Add WebReference -> Após colocar path, clicar Add Reference
            //// https://webservices.producaorestrita.esocial.gov.br/servicos/empregador/enviarloteeventos/WsEnviarLoteEventos.svc?singleWsdl
            //// https://webservices.producaorestrita.esocial.gov.br/servicos/empregador/consultarloteeventos/WsConsultarLoteEventos.svc?singleWsdl




            //WsEnviar.ServicoEnviarLoteEventos xEnviar = new WsEnviar.ServicoEnviarLoteEventos();
            ////xEnviar.Url = web_service_teste;
            ////xEnviar.UseDefaultCredentials = true;
            //xEnviar.ClientCertificates.Add(cert);

            ////Wagner - 28/03/2019
            ////devo enviar para assinatura o evento sem as chaves de lote, e depois de assinado, acrescento essas chaves
            ////ID do evento deve ser igual ao ID do lote
            ////               < eSocial xmlns = "http://www.esocial.gov.br/schema/lote/eventos/envio/v1_1_1" >
            ////               < envioLoteEventos grupo = "1" >< ideEmpregador >< tpInsc > 1 </ tpInsc >< nrInsc > 03844493000134 </ nrInsc ></ ideEmpregador >
            ////               < ideTransmissor >< tpInsc > 1 </ tpInsc >< nrInsc > 03844493000134 </ nrInsc ></ ideTransmissor >< eventos >
            ////               < evento Id = "ID1038444930001342019032110270800011" >
            ////               ... evento assinado
            ////               </evento></eventos></envioLoteEventos></eSocial> 


            //XmlDocument xDoc = new XmlDocument();
            ////xDoc.LoadXml(xml_soap);
            //xDoc.Load("I:\\Desenv\\Ilitera\\Mestra\\Mestra.Viewer\\eSocial\\Envio_Teste_1060_Sem_Lote_Assinar.XML");

            //XmlDocument xAssinado = new XmlDocument();


            //xAssinado = assinarXML(xDoc, cert, "evtTabAmbiente", "Id");

            //xAssinado.Save("I:\\Desenv\\Ilitera\\Mestra\\Mestra.Viewer\\eSocial\\1060_Sem_Lote_Assinado2.XML");
            //xDoc.Load("I:\\Desenv\\Ilitera\\Mestra\\Mestra.Viewer\\eSocial\\1060_Sem_Lote_Assinado2.XML");



            //XmlElement xRet;

            //xRet = xEnviar.EnviarLoteEventos(xDoc.DocumentElement);
            //String xRet2 = xRet.InnerXml;

            ////Retorna na estrutura abaixo, quando vai certo volta o protocolo:
            ////<retornoEnvioLoteEventos xmlns=\"http://www.esocial.gov.br/schema/lote/eventos/envio/retornoEnvio/v1_1_0\"><ideEmpregador><tpInsc>1</tpInsc><nrInsc>03844493000134</nrInsc></ideEmpregador><ideTransmissor><tpInsc>1</tpInsc><nrInsc>03844493000134</nrInsc></ideTransmissor><status><cdResposta>201</cdResposta><descResposta>Lote Recebido com Sucesso.</descResposta></status><dadosRecepcaoLote><dhRecepcao>2019-03-25T14:32:49.457</dhRecepcao><versaoAplicativoRecepcao>0.1.0-A0350</versaoAplicativoRecepcao><protocoloEnvio>1.2.201903.0000000000084410135</protocoloEnvio></dadosRecepcaoLote></retornoEnvioLoteEventos>
            ////transformar em formato XML e salvar no SQL, como histórico do envio


            //XmlDocument xDoc2 = new XmlDocument();
            ////xDoc.LoadXml(xml_soap);
            //xDoc2.Load("I:\\Desenv\\Ilitera\\Mestra\\Mestra.Viewer\\eSocial\\Consulta_Lote_Teste_1060.XML");


            //WsConsultar.ServicoConsultarLoteEventos xConsulta = new WsConsultar.ServicoConsultarLoteEventos();
            //xConsulta.ClientCertificates.Add(cert);
            //XmlElement xRet3 = xConsulta.ConsultarLoteEventos(xDoc2.DocumentElement);

            //string xRet4 = xRet3.InnerXml;
            ////transformar em formato XML e salvar no SQL, como histórico do envio



            ////para Lote-esse é corpo a ser adicionado, e o evento vai dentro.  
            //// <? xml version = "1.0" encoding = "UTF-8" ?>
            //// < eSocial xmlns = "http://www.esocial.gov.br/schema/lote/eventos/envio/v1_1_1" >
            //// < envioLoteEventos grupo = "1" >
            ////    < ideEmpregador >
            ////     < tpInsc > 1 </ tpInsc >
            ////     < nrInsc > 03844493000134 </ nrInsc >
            ////    </ ideEmpregador >    
            ////    < ideTransmissor >    
            ////     < tpInsc > 1 </ tpInsc >    
            ////     < nrInsc > 03844493000134 </ nrInsc >    
            ////    </ ideTransmissor >    
            ////    < eventos >    
            ////      < evento Id = "ID1038444930001342019032110270800001" >     
            ////        < eSocial xmlns = "http://www.esocial.gov.br/schema/evt/evtTabAmbiente/v02_05_00" >


            ////Dados do Evento 1060 - nesse caso     


            ////        </ eSocial >
            ////      </ evento >
            ////   </ eventos >
            ////  </ envioLoteEventos >
            ////</ eSocial >



        }


        private XmlDocument assinarXML(XmlDocument documentoXML, X509Certificate2 certificadoX509, string tagAAssinar, string idAtributoTag)
        {
            


            // Variáveis utilizadas na assinatura
            XmlNodeList nodeParaAssinatura = null;
            SignedXml signedXml = null;
            Reference reference = null;
            KeyInfo keyInfo = null;
            XmlElement sig = null;
            XmlDocument xmlAssinado = null;
            bool temChavePrivada = false;
            bool eValido = true;

            if (eValido)
            {
                // Verifica se o certificado passado por parâmetro possui chave privada.
                // Se não for possível verificar se o certificado tem ou não a chave privada, significa que
                // a instância do objeto X509Certificate2 está nula.
                try
                {
                    temChavePrivada = certificadoX509.HasPrivateKey;
                }
                catch (Exception ex)
                {
                    // Objeto X509Certificate2 passado por parâmetro está nulo                    
                    MsgBox1.Show("Ilitera.Net", "Objeto X509Certificate2 passado por parâmetro não foi carregado." + ex.Message, null,
                    new EO.Web.MsgBoxButton("OK"));
                }
                if (temChavePrivada)
                {
                    if (!tagAAssinar.Equals(string.Empty))
                    {
                        if (!idAtributoTag.Equals(string.Empty))
                        {
                            try
                            {
                                // Informando qual a tag será assinada
                                nodeParaAssinatura = documentoXML.GetElementsByTagName(tagAAssinar);
                                signedXml = new SignedXml((XmlElement)nodeParaAssinatura[0]);
                                signedXml.SignedInfo.SignatureMethod = System.IdentityModel.Tokens.SecurityAlgorithms.RsaSha256Signature;  //"http://www.w3.org/2001/04/xmldsig-more#rsa-sha256"; // signatureMethod;

                                RSACryptoServiceProvider privateKey = (RSACryptoServiceProvider)certificadoX509.PrivateKey;

                                if (!privateKey.CspKeyContainerInfo.HardwareDevice)
                                {
                                    CspKeyContainerInfo enhCsp = new RSACryptoServiceProvider().CspKeyContainerInfo;
                                    CspParameters cspparams = new CspParameters(enhCsp.ProviderType, enhCsp.ProviderName, privateKey.CspKeyContainerInfo.KeyContainerName);
                                    if (privateKey.CspKeyContainerInfo.MachineKeyStore)
                                    {
                                        cspparams.Flags |= CspProviderFlags.UseMachineKeyStore;
                                    }
                                    privateKey = new RSACryptoServiceProvider(cspparams);
                                }

                                // Adicionando a chave privada para assinar o documento
                                signedXml.SigningKey = privateKey;

                                // Referenciando o identificador da tag que será assinada
                                reference = new Reference("#" + nodeParaAssinatura[0].Attributes[idAtributoTag].Value);
                                reference.Uri = "";
                                reference.AddTransform(new XmlDsigEnvelopedSignatureTransform(false));
                                reference.AddTransform(new XmlDsigC14NTransform(false));
                                reference.DigestMethod = System.IdentityModel.Tokens.SecurityAlgorithms.Sha256Digest;// "http://www.w3.org/2001/04/xmldsig-more#rsa-sha256";

                                // Adicionando a referencia de qual tag será assinada
                                signedXml.AddReference(reference);

                                // Adicionando informações do certificado na assinatura
                                keyInfo = new KeyInfo();
                                keyInfo.AddClause(new KeyInfoX509Data(certificadoX509));
                                signedXml.KeyInfo = keyInfo;

                                // Calculando a assinatura
                                // https://stackoverflow.com/questions/29005876/signedxml-compute-signature-with-sha256
                                // https://pt.stackoverflow.com/questions/221665/problema-com-assinatura-digital-sha-256
                                //https://social.msdn.microsoft.com/Forums/vstudio/en-US/6438011b-92fb-4123-a22f-ad071efddf85/xml-digital-signature-with-sha256-algorithm?forum=netfxbcl




                                signedXml.ComputeSignature();

                                // Adicionando a tag de assinatura ao documento xml
                                sig = signedXml.GetXml();
                                documentoXML.GetElementsByTagName(tagAAssinar)[0].ParentNode.AppendChild(sig);
                                xmlAssinado = new XmlDocument();
                                xmlAssinado.PreserveWhitespace = true;
                                xmlAssinado.LoadXml(documentoXML.OuterXml);
                            }
                            catch (Exception ex)
                            {
                                // Falha ao assinar documento XML                                
                                MsgBox1.Show("Ilitera.Net", "Falha ao assinar documento XML. " + ex.Message, null,
                                new EO.Web.MsgBoxButton("OK"));
                            }
                        }
                        else
                        {
                            // String que informa o id da tag XML a ser assinada está vazia                            
                            MsgBox1.Show("Ilitera.Net", "String que informa o id da tag XML a ser assinada está vazia", null,
                            new EO.Web.MsgBoxButton("OK"));
                        }
                    }
                    else
                    {
                        // String que informa a tag XML a ser assinada está vazia                        
                        MsgBox1.Show("Ilitera.Net", "String que informa a tag XML a ser assinada está vazia", null,
                        new EO.Web.MsgBoxButton("OK"));
                    }
                }
                else
                {
                    // Certificado Digital informado não possui chave privada                    
                    MsgBox1.Show("Ilitera.Net", "Certificado Digital informado não possui chave privada", null,
                        new EO.Web.MsgBoxButton("OK"));
                }
            }

            return xmlAssinado;
        }

        protected void cmd_webservice_Click(object sender, EventArgs e)
        {

            using (WebClient client = new WebClient())
            {
                //client.Headers.Add("SOAPAction", "\"http://tempuri.org/HelloWorld\"");
                //client.Headers.Add("Content-Type", "text/xml; charset=utf-8");
                ////var payload = @"<?xml version=""1.0"" encoding=""utf-8""?><soap:Envelope xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema""><soap:Body><HelloWorld xmlns=""http://tempuri.org/""><foo><Id>1</Id><Name>Bar</Name></foo></HelloWorld></soap:Body></soap:Envelope>";
                //var payload = @"<?xml version=""1.0"" encoding=""utf-8""?><soap:Envelope xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema""><soap:Body><HelloWorld xmlns=""http://tempuri.org/""><xTeste>ABC</xTeste ><xTeste2>123</xTeste2></HelloWorld></soap:Body></soap:Envelope>";

                // var data = Encoding.UTF8.GetBytes(payload);
                ////var result = client.UploadData("http://localhost:1475/Service1.asmx", data);   https://www.ilitera.net.br/essence_hom/comunicacao.asmx
                //var result = client.UploadData("https://www.ilitera.net.br/essence_hom/comunicacao.asmx", data);
                //Console.WriteLine(Encoding.Default.GetString(result));


                client.Headers.Add("SOAPAction", "\"http://tempuri.org/HelloWorld\"");
                client.Headers.Add("Content-Type", "text/xml; charset=utf-8");
                //var payload = @"<?xml version=""1.0"" encoding=""utf-8""?><soap:Envelope xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema""><soap:Body><HelloWorld xmlns=""http://tempuri.org/""><foo><Id>1</Id><Name>Bar</Name></foo></HelloWorld></soap:Body></soap:Envelope>";
//                var payload =
//@"<?xml version=""1.0"" encoding=""utf-8""?><soap:Envelope xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema""><soap:Body>
//<Executar_Afastamento xmlns=""http://tempuri.org/"">
//    <ID>PROC20181110095411871</ID>
//    <Empresa>5A - GT Hom.</Empresa>
//    <CNPJ>17855616000147</CNPJ>
//    <CodUsuario>245253956</CodUsuario>    
//    <Colaborador>TAMIRES LAPINHA DOS ANJOS BILO</Colaborador>
//    <DataInicio>13/11/2018</DataInicio>
//    <HoraInicio>08:00</HoraInicio>
//    <PrevisaoRetorno>18/11/2018</PrevisaoRetorno>
//    <DataRetorno>18/11/2018</DataRetorno>
//    <HoraRetorno>10:00</HoraRetorno> 
//    <Observacao></Observacao>
//    <TipoAfastamento>Ocupacional</TipoAfastamento>
//    <Emitente_Atestado>CRM</Emitente_Atestado>
//    <Responsavel_Atestado>Marcelo Candido</Responsavel_Atestado>
//    <NrConselho_Atestado>83733</NrConselho_Atestado>
//    <UFConselho_Atestado>SP</UFConselho_Atestado>
//    <CID1>A88</CID1>
//    <CID2></CID2>
//    <CID3></CID3>
//    <CID4></CID4>
//    <Arquivo></Arquivo>
//    <Conteudo_Arquivo></Conteudo_Arquivo>
//</Executar_Afastamento>
//</soap:Body></soap:Envelope>";

                // var data = Encoding.UTF8.GetBytes(payload);
                ////var result = client.UploadData("https://www.ilitera.net.br/essence_hom/comunicacao.asmx", data);
                //var result = client.UploadData("http://localhost:46870/Comunicacao.asmx", data);
                //Console.WriteLine(Encoding.Default.GetString(result));

                // https://www.c-sharpcorner.com/article/how-to-create-a-web-service-project-in-net-using-visual-studio/


                //ver ajuste em web.config
                //colocar opção assincrona na configuração



                //executando no servidor
                //var testex = new Ilitera.Net.Comunicacao_Dec2.Executar_AfastamentoRequest(
                //   "PROC20190410095411871",
                //   "5A - GT Hom.",
                //   "17855616000147",
                //   "245253956",
                //   "TAMIRES LAPINHA DOS ANJOS BILO",
                //   "13/11/2018",
                //   "08:00",
                //   "18/11/2018",
                //   "18/11/2018",
                //   "10:00",
                //   "",
                //   "Ocupacional",
                //   "CRM",
                //   "Marcelo Candido",
                //   "83733",
                //   "SP",
                //   "A88",
                //   "",
                //   "",
                //   "",
                //   "",
                //   "");


                //using (var clientx = new Ilitera.Net.Comunicacao_Dec2.Comunicacao1SoapClient("Comunicacao1Soap1"))
                //{                    
                //    var result = clientx.Executar_Afastamento(testex);
                //}

                //executando local
                var testex = new Ilitera.Net.Comunicacao_Dec.Executar_AfastamentoRequest(
                     "PROC20190410095411871",
                     "5A - GT Hom.",
                     "17855616000147",
                     "245253956",
                     "TAMIRES LAPINHA DOS ANJOS BILO",
                     "13/11/2018",
                     "08:00",
                     "18/11/2018",
                     "18/11/2018",
                     "10:00",
                     "",
                     "Ocupacional",
                     "CRM",
                     "Marcelo Candido",
                     "83733",
                     "SP",
                     "A88",
                     "",
                     "",
                     "",
                     "",
                     "");


                using (var clientx = new Ilitera.Net.Comunicacao_Dec.Comunicacao1SoapClient("Comunicacao1Soap"))
                {
                    var result = clientx.Executar_Afastamento(testex);
                }



            }

        }

        protected void cmd_webservice_Conv_Click(object sender, EventArgs e)
        {


            var testex = new Ilitera.Net.Comunicacao_Dec.Executar_ConvocacaoRequest(
          "17855616000147",
          "5A - GT Hom.",
          "245253956",
          "PERIODICO",
          "TAMIRES LAPINHA DOS ANJOS BILO",
          "13/05/2019",
          "PROC20190410095411871",
          "5555");


            using (var clientx = new Ilitera.Net.Comunicacao_Dec.Comunicacao1SoapClient("Comunicacao1Soap"))
            {
                

                clientx.InnerChannel.OperationTimeout = System.TimeSpan.FromMinutes(10.0);

                var result = clientx.Executar_Convocacao(testex);
            }


        }

        protected void cmd_webservice_Clinicas_Click(object sender, EventArgs e)
        {


            var testex = new Ilitera.Net.Comunicacao_Fecomercio.Retornar_Clinicas_ClienteRequest(
                "03518732009627",
          "03897-100", "10", "100.00"
               );


            using (var clientx = new Ilitera.Net.Comunicacao_Fecomercio.ComunicacaoFecSoapClient("ComunicacaoFecSoap"))
            {


                clientx.InnerChannel.OperationTimeout = System.TimeSpan.FromMinutes(10.0);

                var result = clientx.Retornar_Clinicas_Cliente(testex);
            }

        }

        protected void cmd_webservice_Laudos_Click(object sender, EventArgs e)
        {

            var testex = new Ilitera.Net.Comunicacao_Fecomercio.Retornar_LaudosRequest(
      "26755216000150"
           );


            using (var clientx = new Ilitera.Net.Comunicacao_Fecomercio.ComunicacaoFecSoapClient("ComunicacaoFecSoap"))
            { 


                clientx.InnerChannel.OperationTimeout = System.TimeSpan.FromMinutes(10.0);

                var result = clientx.Retornar_Laudos(testex);
            }

        }

        protected void cmd_webservice_Conv_Fec_Click(object sender, EventArgs e)
        {


            var testex = new Ilitera.Net.Comunicacao_Fecomercio.Executar_ConvocacaoRequest(
          "19770715000151",          
          "1280593603",
          "PERIODICO",
          "79970935089",
          "04/07/2019",
          "PROC2019061717030301",          
          "2146097877");


            using (var clientx = new Ilitera.Net.Comunicacao_Fecomercio.ComunicacaoFecSoapClient("ComunicacaoFecSoap"))
            {


                clientx.InnerChannel.OperationTimeout = System.TimeSpan.FromMinutes(10.0);

                var result = clientx.Executar_Convocacao(testex);
            }


        }

        protected void cmd_webservice_RetLaudo_Click(object sender, EventArgs e)
        {
            var testex = new Ilitera.Net.Comunicacao_Fecomercio.Solicitar_LaudoRequest(
          "17855616000147",
          "PPRA",
          "17/09/2016",
          "ARQUIVO");


            using (var clientx = new Ilitera.Net.Comunicacao_Fecomercio.ComunicacaoFecSoapClient("ComunicacaoFecSoap"))
            {

                
                clientx.InnerChannel.OperationTimeout = System.TimeSpan.FromMinutes(10.0);
                

                var result = clientx.Solicitar_Laudo(testex);
            }

        }



        protected void cmd_webservice_ASO_Click(object sender, EventArgs e)
        {

            var testex = new Ilitera.Net.Comunicacao_Fecomercio.Retornar_ASORequest(
"15363556000110",
"1280593603",
"PERIODICO",
"84759115013",
"29/06/2019",
"PROC2019061217030301",
"2146097877");


            using (var clientx = new Ilitera.Net.Comunicacao_Fecomercio.ComunicacaoFecSoapClient("ComunicacaoFecSoap"))
            {


                clientx.InnerChannel.OperationTimeout = System.TimeSpan.FromMinutes(10.0);

                var result = clientx.Retornar_ASO(testex);
            }


        }

        protected void cmd_webservice_Prontuario_Click(object sender, EventArgs e)
        {

            var testex = new Ilitera.Net.Comunicacao_Fecomercio.Retornar_DigitalizadoRequest(
"54820774001321",
"1280593603",
"ADMISSIONAL",
"27113368832",
"11/07/2017",
"PROC2019061417030301");


            using (var clientx = new Ilitera.Net.Comunicacao_Fecomercio.ComunicacaoFecSoapClient("ComunicacaoFecSoap"))
            {


                clientx.InnerChannel.OperationTimeout = System.TimeSpan.FromMinutes(10.0);

                var result = clientx.Retornar_Digitalizado(testex);
            }



        }

        protected void cmd_webservice_1060_Click(object sender, EventArgs e)
        {


            var testex = new Ilitera.Net.Comunicacao_Fecomercio.Solicitar_eSocial_1060Request(
"15279665000315",
"01/01/2015",
"01/01/2019",
"310");


            using (var clientx = new Ilitera.Net.Comunicacao_Fecomercio.ComunicacaoFecSoapClient("ComunicacaoFecSoap"))
            {


                clientx.InnerChannel.OperationTimeout = System.TimeSpan.FromMinutes(10.0);

                var result = clientx.Solicitar_eSocial_1060(testex);
            }

        }




        protected void cmd_webservice_2210_Click(object sender, EventArgs e)
        {

            var testex = new Ilitera.Net.Comunicacao_Fecomercio.Solicitar_eSocial_2210Request(
"17855616000147",
"01/01/2015",
"01/01/2019",
"310");


            using (var clientx = new Ilitera.Net.Comunicacao_Fecomercio.ComunicacaoFecSoapClient("ComunicacaoFecSoap"))
            {


                clientx.InnerChannel.OperationTimeout = System.TimeSpan.FromMinutes(10.0);

                var result = clientx.Solicitar_eSocial_2210(testex);
            }

        }

        protected void cmd_webservice_2220_Click(object sender, EventArgs e)
        {

            var testex = new Ilitera.Net.Comunicacao_Fecomercio.Solicitar_eSocial_2220Request(
"03983431000103",
"01/01/2018",
"01/01/2019",
"310");


            using (var clientx = new Ilitera.Net.Comunicacao_Fecomercio.ComunicacaoFecSoapClient("ComunicacaoFecSoap"))
            {


                clientx.InnerChannel.OperationTimeout = System.TimeSpan.FromMinutes(10.0);

                var result = clientx.Solicitar_eSocial_2220(testex);
            }
        }

        protected void cmd_webservice_2221_Click(object sender, EventArgs e)
        {

            var testex = new Ilitera.Net.Comunicacao_Fecomercio.Solicitar_eSocial_2221Request(
"17855616000147",
"01/01/2015",
"01/01/2019",
"310");


            using (var clientx = new Ilitera.Net.Comunicacao_Fecomercio.ComunicacaoFecSoapClient("ComunicacaoFecSoap"))
            {


                clientx.InnerChannel.OperationTimeout = System.TimeSpan.FromMinutes(10.0);

                var result = clientx.Solicitar_eSocial_2221(testex);
            }
        }

        protected void cmd_webservice_2230_Click(object sender, EventArgs e)
        {
            var testex = new Ilitera.Net.Comunicacao_Fecomercio.Solicitar_eSocial_2230Request(
"17855616000147",
"01/01/2015",
"01/01/2019",
"310");


            using (var clientx = new Ilitera.Net.Comunicacao_Fecomercio.ComunicacaoFecSoapClient("ComunicacaoFecSoap"))
            {


                clientx.InnerChannel.OperationTimeout = System.TimeSpan.FromMinutes(10.0);

                var result = clientx.Solicitar_eSocial_2230(testex);
            }
        }

        protected void cmd_webservice_2240_Click(object sender, EventArgs e)
        {

            var testex = new Ilitera.Net.Comunicacao_Fecomercio.Solicitar_eSocial_2240Request(
"17855616000147",
"01/01/2014",
"01/01/2016",
"310");


            using (var clientx = new Ilitera.Net.Comunicacao_Fecomercio.ComunicacaoFecSoapClient("ComunicacaoFecSoap"))
            {


                clientx.InnerChannel.OperationTimeout = System.TimeSpan.FromMinutes(10.0);

                var result = clientx.Solicitar_eSocial_2240(testex);
            }
        }

        protected void cmd_webservice_2245_Click(object sender, EventArgs e)
        {

            var testex = new Ilitera.Net.Comunicacao_Fecomercio.Solicitar_eSocial_2245Request(
"17855616000147",
"01/01/2016",
"01/01/2019",
"310");


            using (var clientx = new Ilitera.Net.Comunicacao_Fecomercio.ComunicacaoFecSoapClient("ComunicacaoFecSoap"))
            {


                clientx.InnerChannel.OperationTimeout = System.TimeSpan.FromMinutes(10.0);

                var result = clientx.Solicitar_eSocial_2245(testex);
            }
        }

        protected void cmd_Eventos_1060_Click(object sender, EventArgs e)
        {

            var testex = new Ilitera.Net.Comunicacao_Fecomercio.Eventos_eSocial_1060Request(
"17855616000147",
"01/01/2015",
"01/01/2019",
"310");


            using (var clientx = new Ilitera.Net.Comunicacao_Fecomercio.ComunicacaoFecSoapClient("ComunicacaoFecSoap"))
            {


                clientx.InnerChannel.OperationTimeout = System.TimeSpan.FromMinutes(10.0);

                var result = clientx.Eventos_eSocial_1060(testex);
            }

        }


        protected void cmd_Criar_Cargo_Click(object sender, EventArgs e)
        {
            var testex = new Ilitera.Net.Comunicacao_Dec.Criar_CargoRequest("ID1178556160000002019051613512400000", "310",
            "17855616000147",
            "Operador Panificadora",
            "Operar fornos",
            "14771");


            using (var clientx = new Ilitera.Net.Comunicacao_Dec.Comunicacao1SoapClient("Comunicacao1Soap"))
            {

                clientx.InnerChannel.OperationTimeout = System.TimeSpan.FromMinutes(10.0);

                var result = clientx.Criar_Cargo(testex);
            }
        }




        protected void cmd_Editar_Afastamento_Click(object sender, EventArgs e)
        {

            var testex = new Ilitera.Net.Comunicacao_Dec.Editar_AfastamentoRequest(
                  "PROC20190410095411871",
                  "17855616000147",
                  "245253956",
                  "TAMIRES LAPINHA DOS ANJOS BILO",
                  "16/11/2018",
                  "13/11/2018",
                  "08:00",
                  "18/11/2018",
                  "18/11/2018",
                  "10:00",
                  "",
                  "Ocupacional",
                  "CRM",
                  "Marcelo Candido",
                  "83733",
                  "SP",
                  "A88",
                  "",
                  "",
                  "",
                  "",
                  "");


            using (var clientx = new Ilitera.Net.Comunicacao_Dec.Comunicacao1SoapClient("Comunicacao1Soap"))
            {
                var result = clientx.Editar_Afastamento(testex);
            }
        }

        protected void cmd_Resultado_Exame_Click(object sender, EventArgs e)
        {

            var testex = new Ilitera.Net.Comunicacao_Dec.Resultado_ExameRequest(
           "PROC20190410095411871",
           "245253956",
           "17855616000147",
           "PERIODICO",
           "TAMIRES LAPINHA DOS ANJOS BILO",
           "13/05/2019",
           "1",
           "Teste Obs.",
           "",
           "");


            using (var clientx = new Ilitera.Net.Comunicacao_Dec.Comunicacao1SoapClient("Comunicacao1Soap"))
            {
                var result = clientx.Resultado_Exame(testex);
            }
        }




        protected void cmd_Criar_Empresa_Click(object sender, EventArgs e)
        {

            var testex = new Ilitera.Net.Comunicacao_Dec.Cadastrar_UnidadeRequest(
      "PROC20190410095411871",
      "AAPP Comércio",
      "AAPP Comércio Derivados Ltda",
      "04844493000155",
      "310",
      "Rua",
      "Abelardo Correa",
      "711",
      "5.Andar",
      "São Felix",
      "08080-114",
      "Guarulhos",
      "SP",
      "",
      "",
      "www.aapp.com.br",
      "sergio.bruguera@aapp.com.br",
      "Teste 222",
      "11",
      "95477-0068",
      "Sérgio",
      "RH",
      "",
      "8500144");


            using (var clientx = new Ilitera.Net.Comunicacao_Dec.Comunicacao1SoapClient("Comunicacao1Soap"))
            {
                var result = clientx.Cadastrar_Unidade(testex);
            }
        }




        protected void cmd_Editar_Acidente_Click(object sender, EventArgs e)
        {


            var testex = new Ilitera.Net.Comunicacao_Dec.Editar_AcidenteRequest(
                  "PROC20190410095411871",
                  "245253956",
                  "17855616000147",                  
                  "TAMIRES LAPINHA DOS ANJOS BILO",
                  "15/12/2018",
                  "15/12/2018",
                  "11:15",
                  "Tipico",
                  "200048600",
                  "757030000",
                  "Esquerda",
                  "305004350",
                  "702020000",
                  "Queda em escada",
                  "1",
                  "",
                  "",
                  "",
                  "",
                  "",
                  "",
                  "",
                  "N",
                  "",
                  "",
                  "",
                  "2233443",
                  "Marcelo Candido Alves",
                  "83777",
                  "AP",
                  "",
                  "A89",
                  "A80",
                  "",
                  "",
                  "N",
                  "N",
                  "10000",
                  "1",
                  "20/12/2018",
                  "10:41",
                  "1",
                  "393484873",
                  "1",
                  "",
                  "",
                  "",
                  "" );


            using (var clientx = new Ilitera.Net.Comunicacao_Dec.Comunicacao1SoapClient("Comunicacao1Soap"))
            {
                var result = clientx.Editar_Acidente(testex);
            }
        }



        // https://www.codeproject.com/Articles/1088970/Read-Write-Excel-file-with-OLEDB-in-Csharp-without

        protected void cmd_Ler_Excel_Click(object sender, EventArgs e)
        {

            string Extension = ".xlsx";
            string FilePath = @"I:\Pasta_Desenvolvimento\Planilha_Modelo_Teste.xlsx";
            
              
            string conStr = "";
            DataTable dt = new DataTable();

            /*Add below Commented in Webconfig*/
            /*   <add name ="Excel03ConString"
        connectionString="Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};
                        Extended Properties='Excel 8.0;HDR={1}'"/>
             <!--connectionString="Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};
                        Extended Properties='Excel 8.0;HDR={1}'"/>-->
   <add name ="Excel07ConString"
        connectionString="Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};
                        Extended Properties='Excel 8.0;HDR={1}'"/>
             * */
            switch (Extension)
            {
                case ".xls": //Excel 97-03
                    conStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + FilePath + "; Extended Properties = 'Excel 8.0;HDR=YES'";
                    break;
                case ".xlsx": //Excel 07
                    conStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + FilePath + "; Extended Properties = 'Excel 8.0;HDR=YEs'";
                    break;
            }
            conStr = String.Format(conStr, FilePath, "Yes");
            OleDbConnection connExcel = new OleDbConnection(conStr);


       


            OleDbCommand cmdExcel = new OleDbCommand();
            OleDbDataAdapter oda = new OleDbDataAdapter();

            cmdExcel.Connection = connExcel;
            try
            {
                int m = 1;
                //Get the name of First Sheet
                connExcel.Open();
                DataTable dtExcelSchema;

                dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                connExcel.Close();

                //Read Data from First Sheet
                connExcel.Open();
                cmdExcel.CommandText = "SELECT * From [" + SheetName + "]";
                oda.SelectCommand = cmdExcel;
                oda.Fill(dt);

                for (int zCont = 0; zCont < dt.Columns.Count; zCont++)
                {
                    EO.Web.StaticColumn xTipo = new EO.Web.StaticColumn();
                    //EO.Web.GridColumn xCol = new EO.Web.GridColumn();
                    xTipo.Name = dt.Columns[zCont].ColumnName;
                    xTipo.HeaderText = dt.Columns[zCont].ColumnName;
                    xTipo.Visible = true;
                    xTipo.Width = 200;
                    //xCol.Name = dt.Columns[zCont].ColumnName;
                    //xCol.HeaderText = dt.Columns[zCont].ColumnName;

                    grd_Teste.Columns.Add(xTipo);
                }
                
                

                grd_Teste.DataSource = dt;
                grd_Teste.DataBind();

                string xTexto = dt.Rows[0][0].ToString();
                xTexto = dt.Rows[1][0].ToString();
                xTexto = dt.Rows[2][0].ToString();




                DataTable myColumns = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Columns, new object[] { null, null, SheetName, null });

                foreach (DataRow dtRow in myColumns.Rows)
                {

                    string xTeste = dtRow.ToString();


                }


                connExcel.Close();




            // insert data
            //    using (OleDbConnection conn = new OleDbConnection(connectionString))
            //    {
            //        try
            //        {
            //            conn.Open();
            //            OleDbCommand cmd = new OleDbCommand();
            //            cmd.Connection = conn;
            //            cmd.CommandText = @"Insert into [Sheet1$] (month,mango,apple,orange) 
            //VALUES ('DEC','40','60','80');";
            //            cmd.ExecuteNonQuery();
            //        }
            //        catch (Exception ex)
            //        {
            //            //exception here
            //        }
            //        finally
            //        {
            //            conn.Close();
            //            conn.Dispose();
            //        }
            //    }

            //    //update data in EXCEL sheet (update data)
            //    using (OleDbConnection conn = new OleDbConnection(connectionString))
            //    {
            //        try
            //        {
            //            conn.Open();
            //            OleDbCommand cmd = new OleDbCommand();
            //            cmd.Connection = conn;
            //            cmd.CommandText = "UPDATE [Sheet1$] SET month = 'DEC' WHERE apple = 74;";
            //            cmd.ExecuteNonQuery();
            //        }
            //        catch (Exception ex)
            //        {
            //            //exception here
            //        }
            //        finally
            //        {
            //            conn.Close();
            //            conn.Dispose();
            //        }
            //    }

            }
            catch ( Exception ex)
            {

            }


            return;

        }

        protected void ddlSlno_SelectedIndexChanged(object sender, EventArgs e)
        {
            GenerateExcelData(ddlSlno.SelectedValue);
        }

        private void GenerateExcelData(string SlnoAbbreviation)
        {

            OleDbConnection oledbConn;

            oledbConn = new OleDbConnection();

            try
            {
                // need to pass relative path after deploying on server
                //string path = System.IO.Path.GetFullPath(Server.MapPath("~/InformationNew.xlsx"));
                string path = @"I:\Pasta_Desenvolvimento\Planilha_Modelo_Teste.xlsx";

                /* connection string  to work with excel file. HDR=Yes - indicates 
                   that the first row contains columnnames, not data. HDR=No - indicates 
                   the opposite. "IMEX=1;" tells the driver to always read "intermixed" 
                   (numbers, dates, strings etc) data columns as text. 
                Note that this option might affect excel sheet write access negative. */

                if (Path.GetExtension(path) == ".xls")
                {
                    oledbConn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source = " + path + ";Extended Properties =\"Excel 8.0;HDR=Yes;IMEX=2\"");
                }
                else if (Path.GetExtension(path) == ".xlsx")
                {
                    oledbConn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties = 'Excel 12.0;HDR=YES;IMEX=1;'; ");
                }
                oledbConn.Open();
                OleDbCommand cmd = new OleDbCommand(); ;
                OleDbDataAdapter oleda = new OleDbDataAdapter();
                DataSet ds = new DataSet();

                // passing list to drop-down list

                // selecting distinct list of Slno 
                cmd.Connection = oledbConn;
                cmd.CommandType = CommandType.Text;
                //cmd.CommandText = "SELECT distinct([Slno]) FROM [Sheet1$]";
                //oleda = new OleDbDataAdapter(cmd);
                //oleda.Fill(ds, "dsSlno");
                //ddlSlno.DataSource = ds.Tables["dsSlno"].DefaultView;
                //if (!IsPostBack)
                //{
                //    ddlSlno.DataTextField = "Slno";
                //    ddlSlno.DataValueField = "Slno";
                //    ddlSlno.DataBind();
                //}
                // by default we will show form data for all states 
                // but if any state is selected then show data accordingly
                //if (!String.IsNullOrEmpty(SlnoAbbreviation) && SlnoAbbreviation != "Select")
                //{
                //    cmd.CommandText = "SELECT [Slno], [FirstName], [LastName], [Location]" +
                //        "  FROM [Sheet1$] where [Slno]= @Slno_Abbreviation";
                //    cmd.Parameters.AddWithValue("@Slno_Abbreviation", SlnoAbbreviation);
                //}
                //else
                //{
                //    cmd.CommandText = "SELECT [Slno],[FirstName],[LastName],[Location] FROM[Sheet1$]";
                //}

                cmd.CommandText = "SELECT * FROM[Plan1$]";
                oleda = new OleDbDataAdapter(cmd);
                oleda.Fill(ds);

                // binding form data with grid view
                grvData.DataSource = ds.Tables[0].DefaultView;
                grvData.DataBind();


                for ( int zCont=0; zCont<grvData.Rows.Count;zCont++)
                {
                    string zVal = "";

                    GridViewRow zRow;

                    zRow = grvData.Rows[zCont];
                    zVal = HttpUtility.HtmlDecode(zRow.Cells[0].Text.ToString().Trim());
                    
                }
            }
            // need to catch possible exceptions
            catch (Exception ex)
            {
                lblError.Text = ex.ToString();
            }
            finally
            {
                oledbConn.Close();
            }
        }// close of method GemerateExceLData




        private DataTable GetDataTable(string sql, string connectionString)
        {
            DataTable dt = null;

            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                conn.Open();
                using (OleDbCommand cmd = new OleDbCommand(sql, conn))
                {
                    using (OleDbDataReader rdr = cmd.ExecuteReader())
                    {
                        dt.Load(rdr);
                        return dt;
                    }
                }
            }
        }





        protected void cmd_Dash_Exposicao_Click(object sender, EventArgs e)
        {

            var testex = new Ilitera.Net.Comunicacao_Dash.Exposicao_ColaboradoresRequest("17855616000147");

            using (var clientx = new Ilitera.Net.Comunicacao_Dash.ComunicacaoDashSoapClient("ComunicacaoDashSoap"))
            {


                clientx.InnerChannel.OperationTimeout = System.TimeSpan.FromMinutes(10.0);
                

                var result = clientx.Exposicao_Colaboradores(testex);
            }

        }
    }
}
