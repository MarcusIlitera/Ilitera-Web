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

using System.Collections;

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
    public class IliNet__eSocial : System.Web.Services.WebService
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
        public String Solicitar_Token(string xUsuario, string xSenha )
        {
            string xRetorno = "";

            //validar usuário e senha
            Ilitera.Data.eSocial xUser = new Ilitera.Data.eSocial();
            Int32 xCodUsuario = xUser.Retornar_Codigo_Usuario(xUsuario, xSenha);

            if ( xCodUsuario == 0 )
            {
                return "Usuário Inválido.";
            }

            Ilitera.Common.Usuario xPerm = new Ilitera.Common.Usuario();
            bool xPermissao = xPerm.Permissao_API_eSocial(xCodUsuario);

            if (xPermissao != true)
            {
                return "Usuário sem acesso.";
            }


            //gerar token de 16 caracteres
            string token = Convert.ToBase64String(Guid.NewGuid().ToByteArray());

            if ( token.Length >= 16)
            {
                token = token.Substring(0, 16).Replace("/","-").Replace(@"\","-");
            }
            else
            {
                token = token + new string('0', 16 - token.Length).Replace("/", "-").Replace(@"\", "-"); 
            }


            //salvar em tbl_eSocial_Token
            tbleSocial_Token rToken = new tbleSocial_Token();
            rToken.Token = token;
            rToken.Criado = System.DateTime.Now;
            rToken.Utilizado = false;
            rToken.Usuario = xUsuario;
            rToken.Senha = xSenha;
            rToken.Save();

            xRetorno = token;

            return xRetorno;

        }

        





        [WebMethod]
        public XmlDocument Solicitar_eSocial_2210(string CNPJ, string DataInicial, string DataFinal, string Token)
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



                //validar token e pegar codUsuario
                string CodUsuario = "";

                tbleSocial_Token rToken = new tbleSocial_Token();
                rToken.Find(" Token = '" + Token + "' ");

                if ( rToken.Id == 0 )  // se não encontrou
                {
                    txt_Status = txt_Status + "60 ( Token Inválido ) |";
                    xRetorno = "60 ( Token Inválido )";
                }
                else
                {
                    if (rToken.Utilizado == true )
                    {
                        txt_Status = txt_Status + "61 ( Token já utilizado ) | ";
                        xRetorno = "61 ( Token já utilizado )";
                    }
                    else
                    {

                        Ilitera.Data.eSocial xUser = new Ilitera.Data.eSocial();
                        Int32 xCodUsuario = xUser.Retornar_Codigo_Usuario(rToken.Usuario, rToken.Senha);
                        
                                                if ( xCodUsuario == 0 )
                        {
                            txt_Status = txt_Status + "62 ( Token com usuário inválido ) | ";
                            xRetorno = "62 ( Token com usuário inválido )";
                        }

                        CodUsuario = xCodUsuario.ToString();

                        //colocar utilizado = true
                        rToken.Utilizado = true;
                        rToken.Save();


                        System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");

                        tbleSocial_API_Log xLog = new tbleSocial_API_Log();
                        xLog.Token = rToken.Token;
                        xLog.Modulo = "Solicitar_eSocial_2210";
                        xLog.DataHora = System.DateTime.Now;
                        xLog.CNPJ = CNPJ;
                        xLog.DataInicial = System.Convert.ToDateTime(DataInicial, ptBr);
                        xLog.DataFinal = System.Convert.ToDateTime(DataFinal, ptBr);
                        xLog.Save();


                    }
                }



                


                if (txt_Status == "")
                {

                    rEmpresa = new Ilitera.Common.Pessoa();

                    rSelect = " dbo.udf_getnumeric( nomecodigo ) = '" + CNPJ + "' and IsInativo = 0 and IdPessoa in ( Select IdPessoa from Juridica where idjuridicapapel in ( 1, 18 ) ) ";

                    //verificar se tem CNPJ em duplicidade
                    ArrayList nEmpresa = new Ilitera.Common.Pessoa().Find(rSelect);
                    
                    if ( nEmpresa.Count > 1)
                    {
                        txt_Status = txt_Status + "Erro: CNPJ em mais de uma empresa ( " + CNPJ + "  )" + System.Environment.NewLine;
                        xRetorno = "03 (CNPJ em mais de uma empresa (" + CNPJ + ")";
                    }


                    if (txt_Status == "")
                    {

                        //pegar Id Empresa                    
                        rEmpresa.Find(rSelect);

                        //se não achar empresa,  emitir retorno avisando
                        if (rEmpresa.Id == 0)
                        {
                            txt_Status = txt_Status + "Erro: Empresa não localizada ( " + CNPJ + "  )" + System.Environment.NewLine;
                            xRetorno = "03 (Empresa não localizada (" + CNPJ + ")";
                        }
                        else
                        {
                            //checar se usuário tem permissão para essa empresa
                            Ilitera.Data.eSocial rAcesso = new Ilitera.Data.eSocial();
                            if (rAcesso.Retornar_Acesso_Usuario_Empresa(System.Convert.ToInt32(CodUsuario), rEmpresa.Id) == false)
                            {
                                txt_Status = txt_Status + "Erro: Sem permissão para Empresa ( " + CNPJ + "  )" + System.Environment.NewLine;
                                xRetorno = "04 ( Sem permissão para Empresa (" + CNPJ + ")";
                            }
                        }
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
                            z2210.Processar_S2210(0, DataInicial, DataFinal, zCod, xIdAcidente, System.Convert.ToInt32(CodUsuario), 0, zCliente.ESocial_Ambiente);
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
        public XmlDocument Solicitar_eSocial_2220(string CNPJ, string DataInicial, string DataFinal, string Token)
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



                //validar token e pegar codUsuario
                string CodUsuario = "";

                tbleSocial_Token rToken = new tbleSocial_Token();
                rToken.Find(" Token = '" + Token + "' ");

                if (rToken.Id == 0)  // se não encontrou
                {
                    txt_Status = txt_Status + "60 ( Token Inválido ) |";
                    xRetorno = "60 ( Token Inválido )";
                }
                else
                {
                    if (rToken.Utilizado == true)
                    {
                        txt_Status = txt_Status + "61 ( Token já utilizado ) | ";
                        xRetorno = "61 ( Token já utilizado )";
                    }
                    else
                    {

                        Ilitera.Data.eSocial xUser = new Ilitera.Data.eSocial();
                        Int32 xCodUsuario = xUser.Retornar_Codigo_Usuario(rToken.Usuario, rToken.Senha);

                        if (xCodUsuario == 0)
                        {
                            txt_Status = txt_Status + "62 ( Token com usuário inválido ) | ";
                            xRetorno = "62 ( Token com usuário inválido )";
                        }

                        CodUsuario = xCodUsuario.ToString();

                        //colocar utilizado = true
                        rToken.Utilizado = true;
                        rToken.Save();

                        System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");

                        tbleSocial_API_Log xLog = new tbleSocial_API_Log();
                        xLog.Token = rToken.Token;
                        xLog.Modulo = "Solicitar_eSocial_2220";
                        xLog.DataHora = System.DateTime.Now;
                        xLog.CNPJ = CNPJ;
                        xLog.DataInicial = System.Convert.ToDateTime(DataInicial,ptBr);
                        xLog.DataFinal = System.Convert.ToDateTime(DataFinal,  ptBr);
                        xLog.Save();

                    }
                }





                Cliente zCliente = new Cliente();


                if (txt_Status == "")
                {
                    rSelect = " dbo.udf_getnumeric( nomecodigo ) = '" + CNPJ + "' and IsInativo = 0  and IdPessoa in ( Select IdPessoa from Juridica where idjuridicapapel in ( 1, 18 ) )  ";

                    //verificar se tem CNPJ em duplicidade
                    ArrayList nEmpresa = new Ilitera.Common.Pessoa().Find(rSelect);

                    if (nEmpresa.Count > 1)
                    {
                        txt_Status = txt_Status + "Erro: CNPJ em mais de uma empresa ( " + CNPJ + "  )" + System.Environment.NewLine;
                        xRetorno = "03 (CNPJ em mais de uma empresa (" + CNPJ + ")";
                    }


                    if (txt_Status == "")
                    {


                        DataSet rDs = new Ilitera.Common.Pessoa().Get(" dbo.udf_getnumeric( nomecodigo ) = '" + CNPJ + "' and IsInativo = 0  and IdPessoa in ( Select IdPessoa from Juridica where idjuridicapapel in ( 1, 18 ) ) ");

                        if (rDs.Tables[0].Rows.Count < 1)
                        {
                            txt_Status = txt_Status + "Erro: Empresa não localizada ( " + CNPJ + "  )" + System.Environment.NewLine;
                            xRetorno = "03 (Empresa não localizada (" + CNPJ + ")";
                        }
                        else
                        {
                            //checar se usuário tem permissão para essa empresa
                            Ilitera.Data.eSocial rAcesso = new Ilitera.Data.eSocial();
                            if (rAcesso.Retornar_Acesso_Usuario_Empresa(System.Convert.ToInt32(CodUsuario), System.Convert.ToInt32(rDs.Tables[0].Rows[0]["IdPessoa"].ToString())) == false)
                            {
                                txt_Status = txt_Status + "Erro: Sem permissão para Empresa ( " + CNPJ + "  )" + System.Environment.NewLine;
                                xRetorno = "04 ( Sem permissão para Empresa (" + CNPJ + ")";
                            }
                        }

                        zCliente.Find(System.Convert.ToInt32(rDs.Tables[0].Rows[0]["IdPessoa"].ToString()));

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
                }





                if (txt_Status == "")
                {
                    
                    //DataSet rDs = new Ilitera.Common.Pessoa().Get(" dbo.udf_getnumeric( nomecodigo ) = '" + CNPJ + "' ");
                    DataSet rDs = new Ilitera.Common.Pessoa().Get(" dbo.udf_getnumeric( nomecodigo ) = '" + CNPJ + "' and IsInativo = 0  and IdPessoa in ( Select IdPessoa from Juridica where idjuridicapapel in ( 1, 18 ) )  ");

                    for (int nCont = 0; nCont < rDs.Tables[0].Rows.Count; nCont++)
                    {


                        //criar
                        DataSet zDs2 = new DataSet();
                        Ilitera.Data.eSocial xLista2 = new Ilitera.Data.eSocial();
                        //zDs2 = xLista2.Mensageria_2220(rEmpresa.Id, 0, 0, DataInicial, DataFinal, System.Convert.ToInt32(CodUsuario), "4", "", "N");
                        zDs2 = xLista2.Mensageria_2220(System.Convert.ToInt32(rDs.Tables[0].Rows[nCont]["IdPessoa"].ToString()), 0, 0, DataInicial, DataFinal, System.Convert.ToInt32(CodUsuario), "4", "", "N", zCliente.ESocial_Ambiente);

                        for (int zCont = 0; zCont < zDs2.Tables[0].Rows.Count; zCont++)
                        {

                            string zCod = "";

                            string xIdExameBase = zDs2.Tables[0].Rows[zCont]["IdExameBase"].ToString().Trim();

                            string zCriado = zDs2.Tables[0].Rows[zCont]["Criado_Em"].ToString().Trim();
                            //verificar se já foi criado o XML


                            if (zCriado == "")
                            {
                                Ilitera.Net.e_Social.Controle_eSocial_2220 z2220 = new Ilitera.Net.e_Social.Controle_eSocial_2220();
                                z2220.Processar_S2220(System.Convert.ToInt32(xIdExameBase), DataInicial, DataFinal, zCod, System.Convert.ToInt32(CodUsuario), 0, zCliente.ESocial_Ambiente);
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

                    //DataSet vDs = new Ilitera.Common.Pessoa().Get(" dbo.udf_getnumeric( nomecodigo ) = '" + CNPJ + "' ");
                    DataSet vDs = new Ilitera.Common.Pessoa().Get(" dbo.udf_getnumeric( nomecodigo ) = '" + CNPJ + "' and IsInativo = 0  and IdPessoa in ( Select IdPessoa from Juridica where idjuridicapapel in ( 1, 18 ) )  ");

                    for (int nCont = 0; nCont < vDs.Tables[0].Rows.Count; nCont++)
                    {

                        //juntar XMLs
                        DataSet zDs = new DataSet();
                        Ilitera.Data.eSocial xLista = new Ilitera.Data.eSocial();
                        //zDs = xLista.Mensageria_2220(rEmpresa.Id, 0, 0, DataInicial, DataFinal, System.Convert.ToInt32(CodUsuario), "4", "", "N");
                        zDs = xLista.Mensageria_2220(System.Convert.ToInt32(vDs.Tables[0].Rows[nCont]["IdPessoa"].ToString()), 0, 0, DataInicial, DataFinal, System.Convert.ToInt32(CodUsuario), "4", "", "N", zCliente.ESocial_Ambiente);

                        for (int zCont = 0; zCont < zDs.Tables[0].Rows.Count; zCont++)
                        {

                            Ilitera.Data.eSocial xDados = new Ilitera.Data.eSocial();

                            DataSet nDs = new DataSet();
                            nDs = xDados.Trazer_XML2(System.Convert.ToInt32(zDs.Tables[0].Rows[zCont]["IdeSocial_Deposito"].ToString().Trim()));

                            string strXML = nDs.Tables[0].Rows[0][0].ToString().Replace("\r\n", string.Empty);


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









        //[WebMethod]
        //public XmlDocument Solicitar_eSocial_2230(string CNPJ, string DataInicial, string DataFinal, string Token)
        //{

        //    Ilitera.Common.Pessoa rEmpresa = new Ilitera.Common.Pessoa();
        //    Empregado rEmpregado = new Empregado();

        //    //int zId = 0;


        //    string rSelect = "";

        //    string xRetorno = "";

        //    string txt_Status = "";

        //    XmlDocument zRet = new XmlDocument();


        //    try
        //    {




        //        xRetorno = "";


        //        if (CNPJ.Length < 10 || CNPJ.Length > 14)
        //        {
        //            txt_Status = txt_Status + " Campo CNPJ inválido |";
        //            xRetorno = xRetorno + " Campo CNPJ Inválido |";
        //        }



        //        if (DataInicial.Length != 10)
        //        {
        //            txt_Status = txt_Status + " Campo Data Inicial inválido |";
        //            xRetorno = xRetorno + " Campo Data Inicial Inválido |";
        //        }
        //        if (Validar_Data(DataInicial) == false)
        //        {
        //            txt_Status = txt_Status + " Campo Data Inicial inválido |";
        //            xRetorno = xRetorno + " Campo Data Inicial Inválido |";
        //        }

        //        if (DataFinal.Length != 10)
        //        {
        //            txt_Status = txt_Status + " Campo Data Final inválido |";
        //            xRetorno = xRetorno + " Campo Data Final Inválido |";
        //        }
        //        if (Validar_Data(DataFinal) == false)
        //        {
        //            txt_Status = txt_Status + " Campo Data Final inválido |";
        //            xRetorno = xRetorno + " Campo Data Final Inválido |";
        //        }


        //        if (xRetorno != "")
        //            xRetorno = "50 ( Validação de campos ): " + xRetorno;



        //        //validar token e pegar codUsuario
        //        string CodUsuario = "";

        //        tbleSocial_Token rToken = new tbleSocial_Token();
        //        rToken.Find(" Token = '" + Token + "' ");

        //        if (rToken.Id == 0)  // se não encontrou
        //        {
        //            txt_Status = txt_Status + "60 ( Token Inválido ) |";
        //            xRetorno = "60 ( Token Inválido )";
        //        }
        //        else
        //        {
        //            if (rToken.Utilizado == true)
        //            {
        //                txt_Status = txt_Status + "61 ( Token já utilizado ) | ";
        //                xRetorno = "61 ( Token já utilizado )";
        //            }
        //            else
        //            {

        //                Ilitera.Data.eSocial xUser = new Ilitera.Data.eSocial();
        //                Int32 xCodUsuario = xUser.Retornar_Codigo_Usuario(rToken.Usuario, rToken.Senha);

        //                if (xCodUsuario == 0)
        //                {
        //                    txt_Status = txt_Status + "62 ( Token com usuário inválido ) | ";
        //                    xRetorno = "62 ( Token com usuário inválido )";
        //                }

        //                CodUsuario = xCodUsuario.ToString();

        //                //colocar utilizado = true
        //                rToken.Utilizado = true;
        //                rToken.Save();

        //            }
        //        }







        //        if (txt_Status == "")
        //        {

        //            rEmpresa = new Ilitera.Common.Pessoa();

        //            rSelect = " dbo.udf_getnumeric( nomecodigo ) = '" + CNPJ + "' ";

        //            //pegar Id Empresa                    
        //            rEmpresa.Find(rSelect);

        //            //se não achar empresa,  emitir retorno avisando
        //            if (rEmpresa.Id == 0)
        //            {
        //                txt_Status = txt_Status + "Erro: Empresa não localizada ( " + CNPJ + "  )" + System.Environment.NewLine;
        //                xRetorno = "03 (Empresa não localizada (" + CNPJ + ")";
        //            }
        //            else
        //            {
        //                //checar se usuário tem permissão para essa empresa
        //                Ilitera.Data.eSocial rAcesso = new Ilitera.Data.eSocial();
        //                if (rAcesso.Retornar_Acesso_Usuario_Empresa(System.Convert.ToInt32(CodUsuario), rEmpresa.Id) == false)
        //                {
        //                    txt_Status = txt_Status + "Erro: Sem permissão para Empresa ( " + CNPJ + "  )" + System.Environment.NewLine;
        //                    xRetorno = "04 ( Sem permissão para Empresa (" + CNPJ + ")";
        //                }
        //            }

        //        }




        //        Cliente zCliente = new Cliente(rEmpresa.Id);

        //        if (txt_Status == "")
        //        {

        //            //criar
        //            DataSet zDs2 = new DataSet();
        //            Ilitera.Data.eSocial xLista2 = new Ilitera.Data.eSocial();
        //            zDs2 = xLista2.Mensageria_2230(rEmpresa.Id, 0, 0, DataInicial, DataFinal, System.Convert.ToInt32(CodUsuario), "4", "", "N", zCliente.ESocial_Ambiente);

        //            for (int zCont = 0; zCont < zDs2.Tables[0].Rows.Count; zCont++)
        //            {

        //                string zCod = "";

        //                string xIdAfastamento = zDs2.Tables[0].Rows[zCont]["IdAfastamento"].ToString().Trim();

        //                string zCriado = zDs2.Tables[0].Rows[zCont]["Criado_Em"].ToString().Trim();
        //                //verificar se já foi criado o XML


        //                if (zCriado == "")
        //                {
        //                    Ilitera.Net.e_Social.Controle_eSocial_2230 z2230 = new Ilitera.Net.e_Social.Controle_eSocial_2230();
        //                    z2230.Processar_S2230(System.Convert.ToInt32(xIdAfastamento), DataInicial, DataFinal, zCod, System.Convert.ToInt32(CodUsuario), 0);
        //                }


        //            }




        //            //                zRet = GetXmlDocument(
        //            //new XDocument(
        //            //     new XElement("Tipo",
        //            //        new XElement("CNPJ", CNPJ),
        //            //        new XElement("Laudo", Laudo),
        //            //        new XElement("Link_Laudo", ""),
        //            //        new XElement("Conteudo_Arquivo", strXml)
        //            //        )
        //            //      ));//.Save(xArq);





        //            //XmlDocument xml = new XmlDocument();
        //            //XmlElement root = xml.CreateElement("RETORNO");
        //            //xml.AppendChild(root);


        //            string xRetXML = "<RETORNO>";

        //            //juntar XMLs
        //            DataSet zDs = new DataSet();
        //            Ilitera.Data.eSocial xLista = new Ilitera.Data.eSocial();
        //            zDs = xLista.Mensageria_2230(rEmpresa.Id, 0, 0, DataInicial, DataFinal, System.Convert.ToInt32(CodUsuario), "4", "", "N", zCliente.ESocial_Ambiente);

        //            for (int zCont = 0; zCont < zDs.Tables[0].Rows.Count; zCont++)
        //            {

        //                Ilitera.Data.eSocial xDados = new Ilitera.Data.eSocial();

        //                DataSet rDs = new DataSet();
        //                rDs = xDados.Trazer_XML2(System.Convert.ToInt32(zDs.Tables[0].Rows[zCont]["IdeSocial_Deposito"].ToString().Trim()));

        //                string strXML = rDs.Tables[0].Rows[0][0].ToString().Replace("\r\n", string.Empty);


        //                //criar array para armazenas string de XML para depois criar o XML de retorno e ir adicionando ?   Ou crio antes do loop o XML e vou adicionando aqui, acho melhor 

        //                //XmlElement child = xml.CreateElement("XMLS");
        //                //child.SetAttribute("1060", strXML);
        //                //root.AppendChild(child);

        //                if (strXML.Trim() != "")
        //                    xRetXML = xRetXML + "<XMLS>" + strXML.Substring(strXML.IndexOf("<eSocial")) + "</XMLS>";

        //            }

        //            //zRet = xml;

        //            xRetXML = xRetXML + "</RETORNO>";

        //            byte[] encodedString2 = Encoding.UTF8.GetBytes(xRetXML);

        //            MemoryStream ms2 = new MemoryStream(encodedString2);
        //            ms2.Flush();
        //            ms2.Position = 0;

        //            XmlDocument zLote = new XmlDocument();
        //            zLote.Load(ms2);

        //            zRet = zLote;

        //            xRetorno = "01 Processamento concluído sem erros";

        //        }


        //    }
        //    catch (Exception ex)
        //    {
        //        txt_Status = txt_Status + "Erro: Carga do XML: " + ex.Message + System.Environment.NewLine;


        //    }
        //    finally
        //    {

        //        if (txt_Status != "")
        //        {
        //            //montar XMLDocument com erro
        //            string xRet = @"<?xml version=""1.0"" encoding=""UTF-8""?><Erro>" + txt_Status + "</Erro>";

        //            zRet.LoadXml(xRet);


        //        }

        //    }


        //    return zRet;


        //}






        [WebMethod]
        public XmlDocument Solicitar_eSocial_2240(string CNPJ, string DataInicial, string DataFinal, string Token)
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









                //validar token e pegar codUsuario
                string CodUsuario = "";

                tbleSocial_Token rToken = new tbleSocial_Token();
                rToken.Find(" Token = '" + Token + "' ");

                if (rToken.Id == 0)  // se não encontrou
                {
                    txt_Status = txt_Status + "60 ( Token Inválido ) |";
                    xRetorno = "60 ( Token Inválido )";
                }
                else
                {
                    if (rToken.Utilizado == true)
                    {
                        txt_Status = txt_Status + "61 ( Token já utilizado ) | ";
                        xRetorno = "61 ( Token já utilizado )";
                    }
                    else
                    {

                        Ilitera.Data.eSocial xUser = new Ilitera.Data.eSocial();
                        Int32 xCodUsuario = xUser.Retornar_Codigo_Usuario(rToken.Usuario, rToken.Senha);

                        if (xCodUsuario == 0)
                        {
                            txt_Status = txt_Status + "62 ( Token com usuário inválido ) | ";
                            xRetorno = "62 ( Token com usuário inválido )";
                        }

                        CodUsuario = xCodUsuario.ToString();

                        //colocar utilizado = true
                        rToken.Utilizado = true;
                        rToken.Save();


                        System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");

                        tbleSocial_API_Log xLog = new tbleSocial_API_Log();
                        xLog.Token = rToken.Token;
                        xLog.Modulo = "Solicitar_eSocial_2240";
                        xLog.DataHora = System.DateTime.Now;
                        xLog.CNPJ = CNPJ;
                        xLog.DataInicial = System.Convert.ToDateTime(DataInicial, ptBr);
                        xLog.DataFinal = System.Convert.ToDateTime(DataFinal, ptBr);
                        xLog.Save();


                    }
                }



                if (txt_Status == "")
                {

                    rEmpresa = new Ilitera.Common.Pessoa();

                    rSelect = " dbo.udf_getnumeric( nomecodigo ) = '" + CNPJ + "' and IsInativo = 0  and IdPessoa in ( Select IdPessoa from Juridica where idjuridicapapel in ( 1, 18 ) )  ";

                    //verificar se tem CNPJ em duplicidade
                    ArrayList nEmpresa = new Ilitera.Common.Pessoa().Find(rSelect);

                    if (nEmpresa.Count > 1)
                    {
                        txt_Status = txt_Status + "Erro: CNPJ em mais de uma empresa ( " + CNPJ + "  )" + System.Environment.NewLine;
                        xRetorno = "03 (CNPJ em mais de uma empresa (" + CNPJ + ")";
                    }


                    if (txt_Status == "")
                    {


                        //pegar Id Empresa                    
                        rEmpresa.Find(rSelect);

                        //se não achar empresa,  emitir retorno avisando
                        if (rEmpresa.Id == 0)
                        {
                            txt_Status = txt_Status + "Erro: Empresa não localizada ( " + CNPJ + "  )" + System.Environment.NewLine;
                            xRetorno = "03 (Empresa não localizada (" + CNPJ + ")";
                        }
                        else
                        {
                            //checar se usuário tem permissão para essa empresa
                            Ilitera.Data.eSocial rAcesso = new Ilitera.Data.eSocial();
                            if (rAcesso.Retornar_Acesso_Usuario_Empresa(System.Convert.ToInt32(CodUsuario), rEmpresa.Id) == false)
                            {
                                txt_Status = txt_Status + "Erro: Sem permissão para Empresa ( " + CNPJ + "  )" + System.Environment.NewLine;
                                xRetorno = "04 ( Sem permissão para Empresa (" + CNPJ + ")";
                            }
                        }

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

                        string xnId_Laud_Tec = zDs2.Tables[0].Rows[zCont]["nId_Laud_Tec"].ToString().Trim();

                        string xnId_Func_Empregado = zDs2.Tables[0].Rows[zCont]["nId_Func_Empregado"].ToString().Trim();

                        string zCriado = zDs2.Tables[0].Rows[zCont]["Criado_Em"].ToString().Trim();
                        //verificar se já foi criado o XML


                        if (zCriado == "")
                        {
                            Ilitera.Net.e_Social.Controle_eSocial_2240 z2240 = new Ilitera.Net.e_Social.Controle_eSocial_2240();
                            z2240.Processar_S2240(System.Convert.ToInt32(xIdColaborador), DataInicial, DataFinal, zCod, System.Convert.ToInt32(CodUsuario), 0, System.Convert.ToInt32( xnId_Laud_Tec ), System.Convert.ToInt32(xnId_Func_Empregado), zCliente.ESocial_Ambiente);
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
