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

using System.Text;
using System.Collections;
using System.Xml.Linq;


using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

using Ilitera.Sied.Report;
using Ilitera.PCMSO.Report;


namespace Ilitera.Net
{
    /// <summary>
    /// Summary description for Comunicacao1
    /// </summary>
    //[WebService(Namespace = "http://tempuri.org/")]
    [WebService(Namespace = "https://www.ilitera.net.br/life")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [System.Web.Script.Services.ScriptService()]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class ComunicacaoDash : System.Web.Services.WebService
    {



        [WebMethod]
        public XmlDocument Exposicao_Colaboradores(string CNPJ)
        {

            Ilitera.Common.Pessoa rEmpresa = new Ilitera.Common.Pessoa();

            //int zId = 0;


            string rSelect = "";

            string xRetorno = "";

            string txt_Status = "";

            XmlDocument zRet = new XmlDocument();

            System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");


            try
            {




                xRetorno = "";





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
                    DataSet ds = new DataSet("DataSet");
                    ds.Tables.Add(GetTable());
                    DataRow newRow;

                    //preciso pegar a lista de laudos deste CNPJ

                    Cliente cliente = new Cliente();
                    cliente.Find(System.Convert.ToInt32(rEmpresa.Id.ToString()));


                    ArrayList laudos = new LaudoTecnico().Find("nID_EMPR=" + cliente.Id + " "
                                                             + " ORDER BY hDT_LAUDO DESC");

                    

                    foreach (LaudoTecnico laudo in laudos)
                    {


                        List<Ghe> listGhe = new Ghe().Find<Ghe>("nID_LAUD_TEC=" + laudo.Id + " ORDER BY tNO_FUNC");

                        foreach (Ghe ghe in listGhe)
                        {
                            List<Empregado> empregados = ghe.GetEmpregadosExpostos(false, false);

                            List<PPRA> listPPRA = new PPRA().Find<PPRA>("nID_FUNC=" + ghe.Id + " ORDER BY nID_RSC");

                            //Campo EPC
                            StringBuilder sEPC = new StringBuilder();
                            string strEpc = ghe.Epc();
                            string strEpiAcientente = ghe.EpiAcidentes();

                            if (strEpc.IndexOf("Inexistente") == -1)
                                sEPC.Append("EPC: \n" + strEpc + "\n");

                            if (strEpiAcientente.IndexOf("Inexistente") == -1)
                                sEPC.Append("EPI (Riscos de Acidentes):\n" + strEpiAcientente + "\n");

                            if (strEpc.IndexOf("Inexistente") != -1 && strEpiAcientente.IndexOf("Inexistente") != -1)
                                sEPC.Append("Inexistente");

                            string strConclusao = ghe.Conclusao(cliente);

                            foreach (Empregado empregado in empregados)
                            {
                                EmpregadoFuncao empregadoFuncao = EmpregadoFuncao.GetEmpregadoFuncao(ghe, empregado);

                                string strFuncao = empregadoFuncao.GetNomeFuncao();
                                string strSetor = empregadoFuncao.GetNomeSetor();

                                foreach (PPRA ppra in listPPRA)
                                {
                                    newRow = ds.Tables[0].NewRow();

                                    newRow["Laudo"] = laudo.hDT_LAUDO.ToString("dd/MM/yyyy", ptBr);
                                    newRow["Cliente"] = cliente.NomeAbreviado;
                                    newRow["Empregado"] = empregado.tNO_EMPG;
                                    newRow["Idade"] = empregado.IdadeEmpregado().ToString();
                                    newRow["Sexo"] = empregado.tSEXO;
                                    newRow["RazaoSocial"] = cliente.NomeCompleto;
                                    newRow["Endereco"] = cliente.GetEndereco().GetEndereco();
                                    newRow["Cidade"] = cliente.GetEndereco().GetCidade();
                                    newRow["Estado"] = cliente.GetEndereco().GetEstado();
                                    newRow["Cep"] = cliente.GetEndereco().Cep;
                                    newRow["Cnpj"] = cliente.GetCnpj();
                                    newRow["Funcao"] = strFuncao;
                                    newRow["TipoGhe"] = ghe.GetTipoAtividade();
                                    newRow["Setor"] = strSetor;
                                    newRow["Admissao"] = empregado.hDT_ADM;
                                    newRow["CodigoGfip"] = Ghe.GetGFIP(Convert.ToInt32((empregadoFuncao.nIND_GFIP)));
                                    newRow["Insalubridade"] = empregado.nIND_ADICIONAL == TipoAdicional.Insalubridade ? "S" : "N";
                                    newRow["Periculosidade"] = empregado.nIND_ADICIONAL == TipoAdicional.Periculosidade ? "S" : "N";
                                    newRow["Percentual"] = empregado.nAD_INSALUBRIDADE.ToString();
                                    newRow["Ghe"] = ghe.tNO_FUNC;
                                    newRow["GhePericulosidade"] = ghe.bPERICULOSIDADE ? "S" : "N";
                                    newRow["AgenteNocivo"] = ppra.GetNomeAgenteResumido();
                                    newRow["IntensidadeConcentracao"] = ppra.GetStrValorMedido();
                                    newRow["Unidade"] = ppra.GetStrUnidade();
                                    newRow["LimiteTol"] = ppra.GetLimiteToleranciaSemUnidade();
                                    newRow["LimiteUltrapassado"] = ppra.GetLimiteFoiUltrapassado();
                                    newRow["FonteGeradora"] = ppra.tDS_FTE_GER;
                                    newRow["TempoExposicao"] = ghe.nID_TEMPO_EXP.ToString();
                                    newRow["SituacaoRuido"] = string.Empty;
                                    newRow["SituacaoCalor"] = string.Empty;
                                    newRow["SituacaoAgentesQuimicos"] = string.Empty;
                                    newRow["ComprometimentosSaude"] = ppra.tDS_DANO_REL_SAU;
                                    newRow["EPI"] = ppra.GetEpi();
                                    newRow["EPC"] = sEPC.ToString();
                                    newRow["Conclusao"] = ghe.GetStrTipoExposicaoAmbiental();

                                    //newRow["Iluminância Lux"] = ghe.nLUX + " Lux";

                                    if (ghe.nIluminancia != 0)
                                        newRow["IluminanciaLux"] = ghe.nIluminancia + " E(lux)";
                                    else
                                        newRow["IluminanciaLux"] = ghe.nLUX + " Lux";


                                    newRow["IluminanciaRecomendada"] = ghe.GetIluminanciaRecomendada();
                                    newRow["VelocidadeAr"] = ghe.nVELOC + " m/s";
                                    newRow["UnidadeRelativa"] = ghe.nUMID + " %";
                                    newRow["Temperatura"] = ghe.nTEMP + " ºC";

                                    ds.Tables[0].Rows.Add(newRow);

                                    //break;
                                }
                                //break;
                            }
                        }

                    }


                    ds.DataSetName = "Retorno";
                    ds.Tables[0].TableName = "Exposicao";

                    string xstrXML = ds.GetXml();

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





        private static DataTable GetTable()
        {
            DataTable table = new DataTable("Result");

            table.Columns.Add("Laudo", Type.GetType("System.String"));
            table.Columns.Add("Cliente", Type.GetType("System.String"));
            table.Columns.Add("Empregado", Type.GetType("System.String"));
            table.Columns.Add("Idade", Type.GetType("System.String"));
            table.Columns.Add("Sexo", Type.GetType("System.String"));
            table.Columns.Add("RazaoSocial", Type.GetType("System.String"));
            table.Columns.Add("Endereco", Type.GetType("System.String"));
            table.Columns.Add("Cidade", Type.GetType("System.String"));
            table.Columns.Add("Estado", Type.GetType("System.String"));
            table.Columns.Add("Cep", Type.GetType("System.String"));
            table.Columns.Add("Cnpj", Type.GetType("System.String"));
            table.Columns.Add("Funcao", Type.GetType("System.String"));
            table.Columns.Add("TipoGhe", Type.GetType("System.String"));
            table.Columns.Add("Setor", Type.GetType("System.String"));
            table.Columns.Add("Admissao", Type.GetType("System.DateTime"));
            table.Columns.Add("CodigoGfip", Type.GetType("System.String"));
            table.Columns.Add("Insalubridade", Type.GetType("System.String"));
            table.Columns.Add("Periculosidade", Type.GetType("System.String"));
            table.Columns.Add("Percentual", Type.GetType("System.String"));
            table.Columns.Add("Ghe", Type.GetType("System.String"));
            table.Columns.Add("GhePericulosidade", Type.GetType("System.String"));
            table.Columns.Add("AgenteNocivo", Type.GetType("System.String"));
            table.Columns.Add("IntensidadeConcentracao", Type.GetType("System.String"));
            table.Columns.Add("Unidade", Type.GetType("System.String"));
            table.Columns.Add("TipoAvaliacao", Type.GetType("System.String"));
            table.Columns.Add("LimiteTol", Type.GetType("System.String"));
            table.Columns.Add("LimiteUltrapassado", Type.GetType("System.String"));
            table.Columns.Add("FonteGeradora", Type.GetType("System.String"));
            table.Columns.Add("TempoExposicao", Type.GetType("System.String"));
            table.Columns.Add("SituacaoRuido", Type.GetType("System.String"));
            table.Columns.Add("SituacaoCalor", Type.GetType("System.String"));
            table.Columns.Add("SituacaoAgentesQuimicos", Type.GetType("System.String"));
            table.Columns.Add("ComprometimentosSaude", Type.GetType("System.String"));
            table.Columns.Add("EPI", Type.GetType("System.String"));
            table.Columns.Add("EPC", Type.GetType("System.String"));
            table.Columns.Add("Conclusao", Type.GetType("System.String"));
            table.Columns.Add("IluminanciaLux", Type.GetType("System.String"));
            table.Columns.Add("IluminanciaRecomendada", Type.GetType("System.String"));
            table.Columns.Add("VelocidadeAr", Type.GetType("System.String"));
            table.Columns.Add("UnidadeRelativa", Type.GetType("System.String"));
            table.Columns.Add("Temperatura", Type.GetType("System.String"));

            return table;
        }


        private static bool SomenteNumeros(string str)
        {
            str = str.Trim();
            return (System.Text.RegularExpressions.Regex.IsMatch(str, @"^\+?\d+$"));
        }


        



        [WebMethod]
        private XmlDocument solicitar_CAT(string ID, string CNPJ, string CodUsuario, string Colaborador, string Data_Inicial, string Data_Final)
        {

            Clinica rClinica = new Clinica();
            Empregado rEmpregado = new Empregado();
            Ilitera.Common.Pessoa rEmpresa = new Ilitera.Common.Pessoa();
            int zId = 0;

            XmlDocument zRetAll = new XmlDocument();
            XmlDocument zRet = new XmlDocument();

            string rSelect = "";

            string txt_Status = "";

            string xId = ID;

            


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





                if (txt_Status == "")
                {
                    rEmpregado.Find(" tNo_Empg='" + Colaborador + "'  and nId_Empr = " + rEmpresa.Id.ToString());


                    if (txt_Status == "")
                    {
                        //se não achar empregado,  emitir retorno avisando
                        if (rEmpregado.Id == 0)
                        {
                            txt_Status = txt_Status + "Erro: Empregado não localizado ( " + Colaborador + "  )" + System.Environment.NewLine;
                        }
                    }

                }

                //ver se tem CAT e arquivo associados dentro do período
                int xArqs = 0;                

                ArrayList nCat = new CAT().Find(" IDEMPREGADO = " + rEmpregado.Id.ToString() + " and DataEmissao between convert( smalldatetime,'" + Data_Inicial + "', 103 ) and convert( smalldatetime,'" + Data_Final + "', 103 )");


                if ( nCat.Count == 0 )
                {
                    txt_Status = "Não há arquivo de CAT a ser baixado.";
                }


                if (txt_Status == "")
                {

                    foreach (CAT rCat in nCat)
                    {

                        if ( rCat.Arquivo_Cat != "")
                        {

                            string xPath = rCat.Arquivo_Cat.Trim();


                            string xExtensao = xPath.Substring(xPath.Length - 3, 3);


                            string xLink = xPath;
                            string strXml = "";

                            try
                            {

                                byte[] arrBytes = File.ReadAllBytes(xLink);
                                strXml = Convert.ToBase64String(arrBytes);

                            }
                            catch (Exception Ex)
                            {

                                txt_Status = "Erro ao carregar Arquivo de CAT: " + Ex.Message;

                            }


                            if (txt_Status == "")
                            {
                                
                                if (xArqs == 0)
                                {
                                    zRetAll = GetXmlDocument(
                                    new XDocument(
                                        new XElement("Arquivo",
                                            new XElement("Conteudo_Arquivo", strXml),
                                            new XElement("Extensao", xExtensao)
                                          )));
                                }
                                else
                                {
                                    zRet = GetXmlDocument(
                                    new XDocument(
                                        new XElement("Arquivo",
                                            new XElement("Conteudo_Arquivo", strXml),
                                            new XElement("Extensao", xExtensao)
                                          )));

                                    foreach ( XmlNode childNode in zRet.DocumentElement.ChildNodes)
                                        zRetAll.DocumentElement.AppendChild(childNode);
                                    
                                }

                                xArqs++;
                            }

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


            return zRetAll;

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



    }
}
