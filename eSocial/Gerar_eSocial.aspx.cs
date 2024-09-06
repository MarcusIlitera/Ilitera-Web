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
using System.Net;
using System.IO;
using Entities;
using System.Xml.Serialization;
using Ilitera.Common;
using Ionic.Zip;


namespace Ilitera.Net.e_Social
{
    public partial class Gerar_eSocial : System.Web.UI.Page
    {


        protected Entities.Usuario usuario = new Entities.Usuario();
        protected Cliente cliente = new Cliente();


        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["Empresa"] != null && Session["Empresa"].ToString() != String.Empty)
                {
                    //InicializaWebPageObjects(true);

                    if (!IsPostBack)
                    {

                        //try
                        //{
                        //    string FormKey = this.Page.ToString().Substring(4);

                        //    Ilitera.Common.Funcionalidade funcionalidade = new Ilitera.Common.Funcionalidade();
                        //    funcionalidade.Find("ClassName='" + FormKey + "'");

                        //    if (funcionalidade.Id == 0)
                        //        throw new Exception("Formulário não cadastrado - " + FormKey);

                        //    Entities.Usuario xUser = (Entities.Usuario)Session["usuarioLogado"];
                        //    Ilitera.Common.Usuario.Permissao_Web(xUser.IdUsuario, funcionalidade.Id);
                        //}

                        //catch (Exception ex)
                        //{
                        //    Session["Message"] = ex.Message;
                        //    Server.Transfer("~/Tratar_Excecao.aspx");
                        //    return;
                        //}

                        cliente.Find(System.Convert.ToInt32( Session["Empresa"].ToString()));

                        Carregar_Empregados();


                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alerta", String.Format("alert('{0}');", ex.Message), true);
            }
        }

        //protected void cmd_Gerar_Click(object sender, EventArgs e)        
        //{
        //    //if ( rd_Arquivo.Checked == true)
        //    //{

        //    //    Ilitera.Data.eSocial zSocial = new Ilitera.Data.eSocial();

        //    //    zSocial.Gerar_XML_2360(cliente.Id, "i:\\temp\\2360.xml");

        //    //    System.IO.FileStream fs = null;
        //    //    fs = System.IO.File.Open("i:\\temp\\2360.xml", System.IO.FileMode.Open);
        //    //    byte[] btFile = new byte[fs.Length];
        //    //    fs.Read(btFile, 0, Convert.ToInt32(fs.Length));
        //    //    fs.Close();

        //    //    Response.AddHeader("Content-disposition", "attachment; filename=" +
        //    //                       "2360_2.xml");
        //    //    Response.ContentType = "application/octet-stream";
        //    //    Response.BinaryWrite(btFile);
        //    //    Response.End();


        //    //}
        //    //else
        //    //{

        //    //    Ilitera.Data.eSocial zSocial = new Ilitera.Data.eSocial();

        //    //    zSocial.Gerar_XML_2360(cliente.Id, "i:\\temp\\2360.xml");

        //    //    Enviar_Web("i:\\temp\\2360.xml");


        //    //}

        //    return;

        //}


        private void Enviar_Web(string xFile)
        {

            WebRequest req = null;
            WebResponse rsp = null;
            try
            {
                string fileName = xFile;
                string uri = "http://212.170.239.71/appservices/http/FrontendService";
                req = WebRequest.Create(uri);
                //req.Proxy = WebProxy.GetDefaultProxy(); // Enable if using proxy

                req.ContentType = "text/xml"; // content type
                req.Credentials = CredentialCache.DefaultNetworkCredentials;
                req.Method = "POST";        // Post method
                
                // Wrap the request stream with a text-based writer
                StreamWriter writer = new StreamWriter(req.GetRequestStream());
                // Write the XML text into the stream
                writer.WriteLine(this.GetTextFromXMLFile(fileName));
                writer.Close();
                // Send the data to the webserver
                rsp = req.GetResponse(); //I am getting error over here
                StreamReader sr = new StreamReader(rsp.GetResponseStream());
                string result = sr.ReadToEnd();
                sr.Close();

                //Response.Write(result);

            }
            catch (WebException webEx)
            {
                //Response.Write(webEx.Message.ToString());
                //Response.Write(webEx.StackTrace.ToString());                
                MsgBox1.Show("Ilitera.Net", "Erro no envio : " + webEx.Message.ToString(), null,
                                            new EO.Web.MsgBoxButton("OK"));               
            }
            catch (Exception ex)
            {                
                MsgBox1.Show("Ilitera.Net", "Erro no envio : " + ex.Message.ToString(), null,
                                            new EO.Web.MsgBoxButton("OK"));               

                //Response.Write(ex.Message.ToString());
                //Response.Write(ex.StackTrace.ToString());
            }
            finally
            {
                if (req != null) req.GetRequestStream().Close();
                if (rsp != null) rsp.GetResponseStream().Close();
            }

        }




        private void Carregar_Empregados()
        {

            cmb_Colaborador.Items.Clear();
            lst_Id.Items.Clear();

            Int32 zEmpresa = System.Convert.ToInt32(Session["Empresa"].ToString());

            DataSet zEmpr = new Ilitera.Opsa.Data.Empregado().Get("tblEmpregado.nID_EMPREGADO = tblEmpregado.nID_EMPREGADO  AND tblEmpregado.nID_EMPR = " + zEmpresa.ToString() + " AND tblEmpregado.nID_EMPREGADO IN(SELECT nID_EMPREGADO FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEMPREGADO_FUNCAO) ORDER BY tNO_EMPG");

            for (int zCont = 0; zCont < zEmpr.Tables[0].Rows.Count; zCont++)
            {

                cmb_Colaborador.Items.Add(zEmpr.Tables[0].Rows[zCont]["tNo_Empg"].ToString());
                lst_Id.Items.Add(zEmpr.Tables[0].Rows[zCont]["nId_Empregado"].ToString());

            }

            return;

        }

        private string GetTextFromXMLFile(string file)
        {
            StreamReader reader = new StreamReader(file);
            string ret = reader.ReadToEnd();
            reader.Close();
            return ret;
        }

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]
        protected void cmd_Gerar_Click(object sender, EventArgs e)
        {

            lst_Arqs.Items.Clear();

            //  if (rd_Arq.Checked == true)
            //  {

            if (rd_Evento.SelectedValue.ToString()== "1060")
            {
                Processar_S1060();
            }
            else if (rd_Evento.SelectedValue.ToString() == "2221")
            {
                Processar_S2221();
            }
            else if (rd_Evento.SelectedValue.ToString() == "2230")
            {
                Processar_S2230();
            }
            else if (rd_Evento.SelectedValue.ToString() == "2210")
            {
                Processar_S2210();
            }
            else if (rd_Evento.SelectedValue.ToString() == "2220")
            {
                Processar_S2220();
            }
            else if (rd_Evento.SelectedValue.ToString() == "2240")
            {
                Processar_S2240();
            }
            else if (rd_Evento.SelectedValue.ToString() == "2245")
            {
                Processar_S2245();
            }
            else if (rd_Evento.SelectedValue.ToString() == "1005")
            {
                Processar_S1005();
            }

            //}

            return;
            
        }



        private string Gerar_ID(string xCNPJ, int xCont)
        {

            //IDTNNNNNNNNNNNNNNAAAAMMDDHHMMSSQQQQQ 
            //ID -Texto Fixo "ID"; T - Tipo de Inscrição do Empregador(1 - CNPJ; 2 - CPF); NNNNNNNNNNNNNN - Número do CNPJ ou CPF do empregador - Completar com zeros à direita.No caso de pessoas jurídicas, o CNPJ informado deve conter 8 ou 14 posições de acordo com o enquadramento do contribuinte para preenchimento do campo { ideEmpregador / nrInsc}
            //do evento S-1000, completando - se com zeros à direita, se necessário.
            //AAAAMMDD - Ano, mês e dia da geração do evento; HHMMSS - Hora, minuto e segundo da geração do evento; 
            //QQQQQ - Número sequencial da chave.Incrementar somente quando ocorrer geração de eventos na mesma data/ hora, completando com zeros à esquerda.

            System.Text.RegularExpressions.Regex regexObj = new System.Text.RegularExpressions.Regex(@"[^\d]");
            string resultString = regexObj.Replace(xCNPJ, "");

            if (resultString.Length < 14)
            {
                string Rep = new string('0', 14 - resultString.Length);
                resultString = resultString + Rep;
            }

            string xData;

            xData = DateTime.Now.Year.ToString();

            if (DateTime.Now.Month < 10)
                xData = xData + "0" + DateTime.Now.Month.ToString().Trim();
            else
                xData = xData + DateTime.Now.Month.ToString().Trim();

            if (DateTime.Now.Day < 10)
                xData = xData + "0" + DateTime.Now.Day.ToString().Trim();
            else
                xData = xData + DateTime.Now.Day.ToString().Trim();

            if (DateTime.Now.Hour < 10)
                xData = xData + "0" + DateTime.Now.Hour.ToString().Trim();
            else
                xData = xData + DateTime.Now.Hour.ToString().Trim();

            if (DateTime.Now.Minute < 10)
                xData = xData + "0" + DateTime.Now.Minute.ToString().Trim();
            else
                xData = xData + DateTime.Now.Minute.ToString().Trim();

            if (DateTime.Now.Second < 10)
                xData = xData + "0" + DateTime.Now.Second.ToString().Trim();
            else
                xData = xData + DateTime.Now.Second.ToString().Trim();

            string xContador = "";

            xContador = xCont.ToString().Trim();

            if (xContador.Length < 5)
            {
                string Rep = new string('0', 5 - xContador.Length);
                xContador = Rep + xContador;
            }

            return "ID" + "1" + resultString + xData + xContador;

        }

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]
        private void Processar_S2230()
        {

            Int32 zEmpresa = 0;
            Int32 zColaborador = 0;
            Int32 zEmpresaGrupo = 0;

            int zContador = 0;

            string xArq = "";

            System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");


            if (rd_Todos.Checked == true)
            {
                zColaborador = 0;
                zEmpresaGrupo = 0;
                zEmpresa = System.Convert.ToInt32( Session["Empresa"].ToString() );
            }
            else if (rd_Colaborador.Checked == true)
            {
                if (cmb_Colaborador.SelectedIndex < 0)
                {                    
                    MsgBox1.Show("Ilitera.Net", "Selecione Colaborador.", null,
                      new EO.Web.MsgBoxButton("OK"));
                    return;
                }

                zEmpresa = 0;
                zEmpresaGrupo = 0;
                zColaborador = System.Convert.ToInt32(lst_Id.Items[cmb_Colaborador.SelectedIndex].ToString());
            }
            else if (rd_Grupo.Checked == true)
            {
                zEmpresa = 0;
                zColaborador = 0;
                zEmpresaGrupo = System.Convert.ToInt32( Session["Empresa"].ToString() );
            }


            Ilitera.Data.eSocial zEsocial = new Ilitera.Data.eSocial();

            DataSet rDs = zEsocial.Trazer_2230(zEmpresa, zColaborador, zEmpresaGrupo, dtp_Inicial.Text, dtp_Final.Text);

            if (rDs.Tables[0].Rows.Count < 1)
            {                
                MsgBox1.Show("Ilitera.Net", "Não há registros para gerar o evento selecionado.", null,  new EO.Web.MsgBoxButton("OK"));

                return;
            }


            //criar evento de controle para este lote
            tbleSocial xeSocial = new tbleSocial();
            xeSocial.DataHora_Criacao = System.DateTime.Now;
            xeSocial.Evento = "2230";
            Entities.Usuario user = (Entities.Usuario)Session["usuarioLogado"];
            xeSocial.IdUsuario = user.IdUsuario;
            xeSocial.Save();



            for (int zCont = 0; zCont < rDs.Tables[0].Rows.Count; zCont++)
            {
                zContador = zContador + 1;

                eSocial_2230 x2230 = new eSocial_2230();

                eSocialEvtAfastTemp xAfast = new eSocialEvtAfastTemp();

                //TIdeEveTrab xIdeEvento = new TIdeEveTrab();
                T_ideEvento_trab xIdeEvento = new T_ideEvento_trab();

                //TEmpregador xIdeEmpregador = new TEmpregador();
                T_ideEmpregador xIdeEmpregador = new T_ideEmpregador();

                //TIdeVinculoEstag xIdeVinculo = new TIdeVinculoEstag();
                eSocialEvtAfastTempIdeVinculo xIdeVinculo = new eSocialEvtAfastTempIdeVinculo();

                eSocialEvtAfastTempInfoAfastamento xInfoAfastamento = new eSocialEvtAfastTempInfoAfastamento();

                // eSocialEvtAfastTempInfoAfastamentoAltAfastamento xAltAfastamento = new eSocialEvtAfastTempInfoAfastamentoAltAfastamento();
                eSocialEvtAfastTempInfoAfastamentoFimAfastamento xFimAfastamento = new eSocialEvtAfastTempInfoAfastamentoFimAfastamento();
                eSocialEvtAfastTempInfoAfastamentoIniAfastamento xIniAfastamento = new eSocialEvtAfastTempInfoAfastamentoIniAfastamento();


                //eSocialEvtAfastTempInfoAfastamentoIniAfastamentoInfoAtestado[] xAtestado = new eSocialEvtAfastTempInfoAfastamentoIniAfastamentoInfoAtestado[1];
                //xAtestado[0] = new eSocialEvtAfastTempInfoAfastamentoIniAfastamentoInfoAtestado();

                eSocialEvtAfastTempInfoAfastamentoIniAfastamentoInfoCessao xCessao = new eSocialEvtAfastTempInfoAfastamentoIniAfastamentoInfoCessao();
                eSocialEvtAfastTempInfoAfastamentoIniAfastamentoInfoMandSind xMandSind = new eSocialEvtAfastTempInfoAfastamentoIniAfastamentoInfoMandSind();
                //eSocialEvtAfastTempInfoAfastamentoIniAfastamentoInfoAtestadoEmitente xEmitente = new eSocialEvtAfastTempInfoAfastamentoIniAfastamentoInfoAtestadoEmitente();


                try
                {


                    //xAfast.Id = Gerar_ID(rDs.Tables[0].Rows[zCont]["CNPJ"].ToString().Trim().Substring(0, 8), zCont + zContador + 1);
                    xAfast.Id = Gerar_ID(rDs.Tables[0].Rows[zCont]["CNPJ"].ToString().Trim().Substring(0, 14), zCont + zContador + 1);



                    //Empregador
                    xIdeEmpregador.tpInsc = 1;
                    //xIdeEmpregador.nrInsc = rDs.Tables[0].Rows[0]["CNPJ"].ToString().Substring(0, 8);  //CNPJ
                    string xCNPJ = rDs.Tables[0].Rows[zCont]["CNPJ"].ToString().Trim();

                    if (xCNPJ.Length == 14)
                        xIdeEmpregador.nrInsc = xCNPJ;
                    else
                        xIdeEmpregador.nrInsc = new string('0', 14 - xCNPJ.Length) + xCNPJ;


                    xAfast.ideEmpregador = xIdeEmpregador;


                    ////////////////////////////////////////////////
                    xIdeEvento.indRetif = 1;
                    //IdeEvento
                    //IdeEvento
                    //xIdeEvento.indRetif = (sbyte)zRetif;

                    //if (zRetif == 2)
                    //{
                    //    xIdeEvento.nrRecibo = zProtocolo;
                    //}


                    xIdeEvento.procEmi = 1;
                    xIdeEvento.tpAmb = 2;
                    xIdeEvento.verProc = "1";

                    xAfast.ideEvento = xIdeEvento;

                    ////////////////////////////////////////////////


                    //levantar quantos CPFs estão nesta relação,  se todos está selecionado


                    //IdeVinculo
                    xIdeVinculo.cpfTrab = rDs.Tables[0].Rows[zCont]["CPF"].ToString();

                    if (rDs.Tables[0].Rows[zCont]["Matricula"].ToString().Trim() != "")
                        xIdeVinculo.matricula = rDs.Tables[0].Rows[zCont]["Matricula"].ToString();
                    else
                        xIdeVinculo.codCateg = "101";
                    //  se não enviar matrícula, enviar CodCateg



                    //versão 02/2020 - excluir NISTRAB
                    //xIdeVinculo.nisTrab = rDs.Tables[0].Rows[zCont]["PIS"].ToString();






                    xAfast.ideVinculo = xIdeVinculo;


                    ////////////////////////////////////////////////

                    //AltAfastamento
                    //xAltAfastamento.codMotAfast = rDs.Tables[0].Rows[zCont]["CodMotAfast"].ToString();
                    // xAltAfastamento.codMotAnt = "000";

                    ////////////////////////////////////////////////


                    //IniAfastamento                    

                    xIniAfastamento.dtIniAfast = System.Convert.ToDateTime(rDs.Tables[0].Rows[zCont]["DtInicial"].ToString(), ptBr);

                    if (rDs.Tables[0].Rows[zCont]["CodMotAfast"].ToString().Trim() != "")
                    {
                        if (rDs.Tables[0].Rows[zCont]["CodMotAfast"].ToString().Trim().Length == 1)
                            xIniAfastamento.codMotAfast = "0" + rDs.Tables[0].Rows[zCont]["CodMotAfast"].ToString().Trim();
                        else
                            xIniAfastamento.codMotAfast = rDs.Tables[0].Rows[zCont]["CodMotAfast"].ToString().Trim();
                    }
                    else
                    {
                        xIniAfastamento.codMotAfast = "01";
                    }



                    //versão 02/2020 - criar infoMesmoMotiv
                    if (xIniAfastamento.codMotAfast == "01" || xIniAfastamento.codMotAfast == "03")
                    {
                        string xCID = "( ";

                        if (rDs.Tables[0].Rows[zCont]["Cid1"].ToString().Trim() != "-9999")
                            xCID = xCID + rDs.Tables[0].Rows[zCont]["Cid1"].ToString().Trim() + ",";

                        if (rDs.Tables[0].Rows[zCont]["Cid2"].ToString().Trim() != "-9999")
                            xCID = xCID + rDs.Tables[0].Rows[zCont]["Cid2"].ToString().Trim() + ",";

                        if (rDs.Tables[0].Rows[zCont]["Cid3"].ToString().Trim() != "-9999")
                            xCID = xCID + rDs.Tables[0].Rows[zCont]["Cid3"].ToString().Trim() + ",";

                        if (rDs.Tables[0].Rows[zCont]["Cid4"].ToString().Trim() != "-9999")
                            xCID = xCID + rDs.Tables[0].Rows[zCont]["Cid4"].ToString().Trim() + ",";

                        if (xCID == "( ")
                            xCID = "( -9999 )";
                        else
                            xCID = xCID.Substring(0, xCID.Length - 1) + " ) ";


                        //checar se há afastamento anterior do mesmo colaborador com algum CID igual
                        Ilitera.Data.Clientes_Funcionarios xMesmo = new Ilitera.Data.Clientes_Funcionarios();
                        int zOcorrencia = xMesmo.Trazer_Afastamento_Anterior_CID( System.Convert.ToInt32(rDs.Tables[0].Rows[zCont]["IdAfastamento"].ToString()) , rDs.Tables[0].Rows[zCont]["DtInicial"].ToString().Trim(), xCID);

                        if (zOcorrencia > 0)
                            xIniAfastamento.infoMesmoMtv = TS_sim_nao.S;
                        else
                            xIniAfastamento.infoMesmoMtv = TS_sim_nao.N;
                    }
                    else
                    {
                        xIniAfastamento.infoMesmoMtv = TS_sim_nao.N;
                    }

                    xIniAfastamento.infoMesmoMtvSpecified = true;

                    xIniAfastamento.tpAcidTransitoSpecified = false;
                    //xIniAfastamento.tpAcidTransito = 0;



                    if (rDs.Tables[0].Rows[zCont]["Observacao"].ToString().Trim() != "")
                        xIniAfastamento.observacao = rDs.Tables[0].Rows[zCont]["Observacao"].ToString();





                    //versão 02/2020 - criar grupo PERAQUIS ( Férias, apenas dtInicio e dtFim ) e INFOMANDELET ( mandato eletivo - apenas cnpj e remun. )





                    // Cessao - desabilitar por enquanto
                    //xCessao.cnpjCess = "";
                    //xCessao.infOnus = 0;

                    //xIniAfastamento.infoCessao = xCessao;


                    //MandSind - desabilitar por enquanto
                    //xMandSind.cnpjSind = "04744011100014";
                    //xMandSind.infOnusRemun = 0;

                    //xIniAfastamento.infoMandSind = xMandSind;





                    if (rDs.Tables[0].Rows[zCont]["DtVolta"].ToString().Trim() != "")
                    {
                        xFimAfastamento.dtTermAfast = System.Convert.ToDateTime(rDs.Tables[0].Rows[zCont]["DtVolta"].ToString(), ptBr);
                        xInfoAfastamento.fimAfastamento = xFimAfastamento;
                    }

                    xInfoAfastamento.iniAfastamento = xIniAfastamento;

                    xAfast.infoAfastamento = xInfoAfastamento;

                    x2230.evtAfastTemp = xAfast;




                    ////criar xml
                    //if (rd_Todos.Checked == true)
                    //{
                    xArq = "I:\\temp\\" + txt_Arq.Text.Substring(0, txt_Arq.Text.IndexOf(".")) + "_2230_" + rDs.Tables[0].Rows[zCont]["CPF"].ToString().Trim() + "_" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + "_" + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString() + ".XML";
                    //}
                    //else
                    //{
                    //    xArq = txt_Arq.Text;
                    //}

                    


                    //CRIAR XML
                    string xContent = "";

                    XmlSerializer serializer = new XmlSerializer(typeof(eSocial_2230));

                    var memoryStream = new MemoryStream();
                    var streamWriter = new StreamWriter(memoryStream, System.Text.Encoding.UTF8);

                    serializer.Serialize(streamWriter, x2230);

                    byte[] bytes = memoryStream.ToArray();
                    xContent = Encoding.UTF8.GetString(bytes);

                    using (FileStream fs = File.Create(xArq))
                    {
                        fs.Write(bytes, 0, bytes.Length);
                        fs.Close();
                    }




                    string text = File.ReadAllText(xArq);
                    text = text.Replace("xmlns:xsi=", "");
                    text = text.Replace("\"http://www.w3.org/2001/XMLSchema-instance\"", "");
                    text = text.Replace("xmlns:xsd=", "");
                    text = text.Replace("\"http://www.w3.org/2001/XMLSchema\"", "");
                    //text = text.Replace("v_S_01_02_00", "v_S_01_02_00:eSocial_2230");
                    text = text.Replace("evtCAT", "evtAfastTemp");
                    text = text.Replace("v_S_01_02_00:eSocial_2230", "v_S_01_02_00");
                    text = text.Replace("eSocial_2230", "eSocial");

                    File.WriteAllText(xArq, text);

                    xContent = xContent.Replace("xmlns:xsi=", "");
                    xContent = xContent.Replace("\"http://www.w3.org/2001/XMLSchema-instance\"", "");
                    xContent = xContent.Replace("xmlns:xsd=", "");
                    xContent = xContent.Replace("\"http://www.w3.org/2001/XMLSchema\"", "");
                    //xContent = xContent.Replace("v_S_01_02_00", "v_S_01_02_00:eSocial_2230");
                    xContent = xContent.Replace("evtCAT", "evtAfastTemp");
                    xContent = xContent.Replace("v_S_01_02_00:eSocial_2230", "v_S_01_02_00");
                    xContent = xContent.Replace("eSocial_2230", "eSocial");

                    //using (Stream stream = File.Open(xArq, FileMode.Create))
                    //{
                    //    XmlSerializer serializer = new XmlSerializer(typeof(eSocialEvtAfastTemp));
                    //    serializer.Serialize(stream, xAfast);
                    //    stream.Flush();

                    //    byte[] bytes = new byte[stream.Length];
                    //    stream.Position = 0;
                    //    stream.Read(bytes, 0, (int)stream.Length);
                    //    xContent = Encoding.ASCII.GetString(bytes);

                    //    stream.Close();

                    //}

                    lst_Arqs.Items.Add(xArq);

                    //salvar XML no SQL server com xeSocial.Id e nome do arquivo criado e datahora 
                    Int64 zPis = 0;
                    if (rDs.Tables[0].Rows[zCont]["PIS"].ToString().Trim() != "")
                        zPis = System.Convert.ToInt64(System.Text.RegularExpressions.Regex.Match(rDs.Tables[0].Rows[zCont]["PIS"].ToString().Trim(), @"\d+").Value);


                    Ilitera.Data.eSocial zeSocial = new Ilitera.Data.eSocial();
                    zeSocial.XML_Deposito(xArq, xeSocial.Id, zCont, xContent, rDs.Tables[0].Rows[zCont]["CPF"].ToString(), zPis, rDs.Tables[0].Rows[zCont]["CNPJ"].ToString(), dtp_Inicial.Text, dtp_Final.Text, System.Convert.ToInt32(rDs.Tables[0].Rows[0]["IdAfastamento"].ToString()), xAfast.Id,0);



                }
                catch (Exception ex)
                {                    
                    MsgBox1.Show("Ilitera.Net", "Erro:" + ex.ToString(), null, new EO.Web.MsgBoxButton("OK"));
                    return;
                }



            }



            using (ZipFile zip = new ZipFile())
            {
                zip.AlternateEncodingUsage = ZipOption.AsNecessary;
                zip.AddDirectoryByName("Files");

                for (int rCont = 0; rCont < lst_Arqs.Items.Count; rCont++)
                {
                    zip.AddFile(lst_Arqs.Items[rCont].Text, "Files");
                }
                Response.Clear();
                Response.BufferOutput = false;
                string zipName = String.Format("eSocial2230_Zip_{0}.zip", DateTime.Now.ToString("yyyy-MMM-dd-HHmmss"));
                Response.ContentType = "application/zip";
                Response.AddHeader("content-disposition", "attachment; filename=" + zipName);
                zip.Save(Response.OutputStream);
                Response.End();
            }



            MsgBox1.Show("Ilitera.Net", "Arquivo(s) criados!", null, new EO.Web.MsgBoxButton("OK"));
            return;

        }



        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]
        private void Processar_S2210()
        {

            Int32 zEmpresa = 0;
            Int32 zColaborador = 0;
            Int32 zEmpresaGrupo = 0;

            int zContador = 0;

            string xArq = "";

            System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");



            if (rd_Todos.Checked == true)
            {
                zColaborador = 0;
                zEmpresa = System.Convert.ToInt32( Session["Empresa"].ToString() );
                zEmpresaGrupo = 0;
            }
            else if (rd_Colaborador.Checked == true)
            {
                if (cmb_Colaborador.SelectedIndex < 0)
                {                    
                    MsgBox1.Show("Ilitera.Net", "Selecione colaborador.", null, new EO.Web.MsgBoxButton("OK"));
                    return;
                }

                zEmpresa = 0;
                zEmpresaGrupo = 0;
                zColaborador = System.Convert.ToInt32(lst_Id.Items[cmb_Colaborador.SelectedIndex].ToString());
            }
            else if (rd_Grupo.Checked == true)
            {
                zEmpresa = 0;
                zColaborador = 0;
                zEmpresaGrupo = System.Convert.ToInt32( Session["Empresa"].ToString() );
            }

            //query com ajustes

            //  use opsa_prajna_hom

            //select c.tno_CPF as CPF, c.nNO_PIS_PASEP as PIS,  c.tCod_Empr as Matricula, '101' as codCateg, d.NomeCodigo as CNPJ,  convert(char(10), DataAcidente, 103) as DtAcidente,          
            //case when b.IdCodificacaoCat is null then '5.0.01'               when b.IdCodificacaoCAT is not null then b.IdCodificacaoCAT end as tpAcid, 
            //CONVERT(VARCHAR(5), DataAcidente, 108) as hrAcid, '0000' as hrsTrabAntesAcid,          
            //b.IndTipoCAT as tpCat,  case when b.hasMorte = 0 then 'N'  when b.hasMorte = 1 then 'S' end as IndCatObito,   	  
            // case when b.DataObito is null then ''  when b.DataObito is not null then convert(char(10), b.DataObito, 103) end as dtObito,   	   
            // case when hasregPolicial = 0 then 'N'  when hasregPolicial = 1 then 'S' end as IndComumPolicia,          a.codigo_agente_Causador as CodSitGeradora, 
            // IdIniciativaCat as IniciatCat, Observacoes as Observacao, IdTipoLocal as TpLocal, EspecLocal as DscLocal,          
            // case when IdTipoLocal = 2 then a.Logradouro else '' end as dscLograd, case when IdTipoLocal = 2 then a.Nr_Logradouro else '' end as NrLograd, 
            // '' as CodMunic, '' as pais, -- ??
            // case when IdTipoLocal = 2 then a.UF else '' end as UF,   	   
            // case when IdlocalAcidente is null then d.NomeCodigo when IdLocalAcidente is not null then e.NomeCodigo end as cnpjLocalAcid, '' as pais, '' as CodPostal,          
            // a.Codigo_Parte_Corpo_Atingida as CodParteAting, IdLateralidade as lateralidade, a.Codigo_Situacao_Geradora as CodAgntCausador,   	
            // --tpInsc e nrInsc  ??
            // case when CNES is null then '' when CNES is not null then CNES end as CNES,   	  
            //  case when DataInternacao is null then '' when DataInternacao is not null then convert(char(10), DataInternacao, 103) end as DtAtendimento,   	   
            //  case when DataInternacao is null then '0000' when DataInternacao is not null then convert(varchar(5), DataInternacao, 108) end as hrAtendimento,   	   
            //  case when HasInternacao = 0 then 'N' when HasInternacao = 1 then 'S' end as IndInternacao, DuracaoInternacao as DurTrat,   	   
            //  case when hasAfastamento = 0 then 'N' when hasAfastamento = 1 then 'S' end as IndAFast, a.codigo_descricao_lesao as dscLesao, a.Descricao as dscCompLesao,   
            //  --diagProvavel ??
            //  case when f.CodigoCid is null then '' when f.CodigoCid is not null then f.CodigoCid end as CodCID, 
            //  --Observacao ??,
            //  MedicoInternacao as nmEmit, 1 as IdeOC, CRMInternacao as nrOC,   
            //  --ideOC ??
            //  UfInternacao as UfOC, convert(char(10), DataEmissao, 103) as dtCatOrig, NumeroCAT as nrCatOrig
            //  from acidente as a
            //  left join cat as b   on(a.idcat = b.idcat)
            //  left join SIED_NOVO_PRAJNA_HOM.dbo.tblempregado as c   on(a.idempregado = c.nid_empregado)
            //  left join pessoa as d  on(c.nid_empr = d.idpessoa)
            //  left join Pessoa as e  on(IdLocalAcidente = e.IdPessoa)
            //  left join Cid as f  on(a.idcid = f.idcid)
            //   where a.IdEmpregado in  (select nId_Empregado from  SIED_NOVO_PRAJNA_HOM.dbo.tblEMPREGADO where nid_Empr in (select IdPessoa from Juridica where IdGrupoEmpresa in (select IdGrupoEmpresa from Juridica where Idpessoa = 1116992645 )  )  )  
            //   and a.IdCat is not null and tno_CPF is not null  and tno_CPF <> ''
            //   and a.DataAcidente between convert(smalldatetime, '06/04/2018', 103) and convert(smalldatetime, '06/09/2018', 103 )   
            //   order by c.tno_CPF, dataAcidente desc


            Ilitera.Data.eSocial zEsocial = new Ilitera.Data.eSocial();

            DataSet rDs = zEsocial.Trazer_2210(zEmpresa, zColaborador, zEmpresaGrupo, dtp_Inicial.Text, dtp_Final.Text);

            if (rDs.Tables[0].Rows.Count < 1)
            {                
                MsgBox1.Show("Ilitera.Net", "Não há registros para gerar o evento selecionado.", null, new EO.Web.MsgBoxButton("OK"));
                return;
            }



            //criar evento de controle para este lote
            tbleSocial xeSocial = new tbleSocial();
            xeSocial.DataHora_Criacao = System.DateTime.Now;
            xeSocial.Evento = "2210";
            Entities.Usuario user = (Entities.Usuario)Session["usuarioLogado"];
            xeSocial.IdUsuario = user.IdUsuario;
            xeSocial.Save();



            for (int zCont = 0; zCont < rDs.Tables[0].Rows.Count; zCont++)
            {
                zContador = zContador + 1;

                eSocial_2210 x2210 = new eSocial_2210();

                eSocialEvtCAT xCat = new eSocialEvtCAT();

                eSocialEvtCATCat xEvtCat = new eSocialEvtCATCat();

                T_ideEvento_trab xIdeEvento = new T_ideEvento_trab();
                //TIdeEveTrab xIdeEvento = new TIdeEveTrab();

                //eSocialEvtCATIdeEmpregador xIdeEmpregador = new eSocialEvtCATIdeEmpregador();

                T_ideVinculo_sst xIdeVinculo = new T_ideVinculo_sst();
                //eSocialEvtCATIdeVinculo xIdeVinculo = new eSocialEvtCATIdeVinculo();


                eSocialEvtCATCatAgenteCausador xAgente = new eSocialEvtCATCatAgenteCausador();

                eSocialEvtCATCatAtestado xAtestado = new eSocialEvtCATCatAtestado();
                eSocialEvtCATCatAtestadoEmitente xEmitente = new eSocialEvtCATCatAtestadoEmitente();
                eSocialEvtCATCatCatOrigem xCatOrigem = new eSocialEvtCATCatCatOrigem();
                eSocialEvtCATCatLocalAcidente xAcidente = new eSocialEvtCATCatLocalAcidente();
                eSocialEvtCATCatParteAtingida xParte = new eSocialEvtCATCatParteAtingida();

                T_ideEmpregador xIdeEmpregador = new T_ideEmpregador();
                //TEmpregador xIdeEmpregador = new TEmpregador();



                //xCat.ideEmpregador = xIdeEmpregador;


                xAgente = new eSocialEvtCATCatAgenteCausador();
                xParte = new eSocialEvtCATCatParteAtingida();



                try
                {

                    //xCat.Id = Gerar_ID(rDs.Tables[0].Rows[zCont]["CNPJ"].ToString().Trim().Substring(0, 8), zCont + zContador + 1);
                    xCat.Id = Gerar_ID(rDs.Tables[0].Rows[zCont]["CNPJ"].ToString().Trim().Substring(0, 14), zCont + zContador + 1);


                    //Empregador
                    xIdeEmpregador.tpInsc = 1;
                    //xIdeEmpregador.nrInsc = rDs.Tables[0].Rows[zCont]["CNPJ"].ToString().Substring(0, 8);  //CNPJ
                    string xCNPJ = rDs.Tables[0].Rows[zCont]["CNPJ"].ToString().Trim();

                    if (xCNPJ.Length == 14)
                        xIdeEmpregador.nrInsc = xCNPJ;
                    else
                        xIdeEmpregador.nrInsc = new string('0', 14 - xCNPJ.Length) + xCNPJ;


                    //xIdeEmpregador.nrInsc = rDs.Tables[0].Rows[zCont]["CNPJ"].ToString().Substring(0, 14);  //CNPJ

                    xCat.ideEmpregador = xIdeEmpregador;


                    ////////////////////////////////////////////////
                    xIdeEvento.indRetif = 1;

                    //IdeEvento
                    //IdeEvento
                    //xIdeEvento.indRetif = (sbyte)zRetif;

                    //if (zRetif == 2)
                    //{
                    //    xIdeEvento.nrRecibo = zProtocolo;
                    //}

                    xIdeEvento.procEmi = 1;
                    xIdeEvento.tpAmb = 2;   // 2 se produção restrita
                    xIdeEvento.verProc = "1";

                    xCat.ideEvento = xIdeEvento;



                    //IdeVinculo
                    xIdeVinculo.cpfTrab = rDs.Tables[0].Rows[zCont]["CPF"].ToString();


                    //versão 02/2020 - excluir NISTRAB
                    //xIdeVinculo.nisTrab = rDs.Tables[0].Rows[zCont]["PIS"].ToString();


                    if (rDs.Tables[0].Rows[zCont]["Matricula"].ToString().Trim() != "")
                        xIdeVinculo.matricula = rDs.Tables[0].Rows[zCont]["Matricula"].ToString();
                    else
                        xIdeVinculo.codCateg = "101";
                    //  se não enviar matrícula, enviar CodCateg


                    xCat.ideVinculo = xIdeVinculo;






                    //cat
                    xEvtCat.dtAcid = System.Convert.ToDateTime(rDs.Tables[0].Rows[zCont]["dtAcidente"].ToString(), ptBr);

                    //versão 02/2020 - ajuste TPACID
                    //if (rDs.Tables[0].Rows[zCont]["tpAcid"].ToString().Trim() != "")
                    //    xEvtCat.tpAcid = rDs.Tables[0].Rows[zCont]["tpAcid"].ToString();
                    //else
                    //    xEvtCat.tpAcid = "1.0.01";
                    xEvtCat.tpAcid = 1;


                    if (rDs.Tables[0].Rows[zCont]["hrAcid"].ToString().Trim() != "")
                    {
                        xEvtCat.hrAcid = rDs.Tables[0].Rows[zCont]["hrAcid"].ToString().Trim().Substring(0, 2) + rDs.Tables[0].Rows[zCont]["hrAcid"].ToString().Trim().Substring(3, 2);
                    }
                    else
                    {
                        xEvtCat.hrAcid = "00:00";
                    }

                    xEvtCat.hrsTrabAntesAcid = rDs.Tables[0].Rows[zCont]["hrsTrabAntesAcid"].ToString();

                    xEvtCat.tpCat = 1; // System.Convert.ToSByte(rDs.Tables[0].Rows[zCont]["tpCat"].ToString());


                    //if (rDs.Tables[0].Rows[zCont]["IndCatObito"].ToString().Trim() == "S")
                    //    xEvtCat.indCatObito = TS_sim_nao.S;
                    //else
                    //    xEvtCat.indCatObito = TS_sim_nao.N;



                    if (rDs.Tables[0].Rows[zCont]["dtObito"].ToString().Trim() != "")
                    {
                        xEvtCat.dtObito = System.Convert.ToDateTime(rDs.Tables[0].Rows[zCont]["dtObito"].ToString(), ptBr);
                        xEvtCat.indCatObito = TS_sim_nao.S;
                    }
                    else
                    {
                        xEvtCat.indCatObito = TS_sim_nao.N;
                    }



                    if (rDs.Tables[0].Rows[zCont]["indComumPolicia"].ToString().Trim() == "S")
                        xEvtCat.indComunPolicia = TS_sim_nao.S;
                    else
                        xEvtCat.indComunPolicia = TS_sim_nao.N;


                    xEvtCat.codSitGeradora = rDs.Tables[0].Rows[zCont]["CodSitGeradora"].ToString();


                    xEvtCat.iniciatCAT = System.Convert.ToSByte(rDs.Tables[0].Rows[zCont]["IniciatCat"].ToString());


                    if (rDs.Tables[0].Rows[zCont]["Observacao"].ToString().Trim() != "")
                        xEvtCat.obsCAT = rDs.Tables[0].Rows[zCont]["Observacao"].ToString();









                    //local acidente                    
                    xAcidente.tpLocal = System.Convert.ToSByte(rDs.Tables[0].Rows[zCont]["tpLocal"].ToString());

                    if (rDs.Tables[0].Rows[zCont]["DscLocal"].ToString().Trim() != "")
                        xAcidente.dscLocal = rDs.Tables[0].Rows[zCont]["DscLocal"].ToString().Trim();
                    else
                        xAcidente.dscLocal = "Empresa";



                    //validar municipio
                    string zMunic = "";

                    if (rDs.Tables[0].Rows[zCont]["CodMunic"].ToString().Trim() != null)
                        zMunic = rDs.Tables[0].Rows[zCont]["CodMunic"].ToString().Trim();

                    if (zMunic != "")
                    {
                        int n;
                        bool isNumeric = int.TryParse(zMunic, out n);


                        if (isNumeric == true)
                        {
                            Municipio rMunic = new Municipio();
                            rMunic.Find(System.Convert.ToInt32(zMunic));

                            if (rMunic.Id != 0)
                            {
                                zMunic = rMunic.Cod_Municipio_Completo.ToString().Trim();
                            }

                        }
                    }



                    if (rDs.Tables[0].Rows[zCont]["dscLograd"].ToString().Trim() != "")
                    {

                        xAcidente.tpLograd = "R";  // não temos esse campo na tela de acidente, logo, usar R de Rua

                        xAcidente.dscLograd = rDs.Tables[0].Rows[zCont]["dscLograd"].ToString();

                        if (rDs.Tables[0].Rows[zCont]["nrLograd"].ToString().Trim() != "")
                            xAcidente.nrLograd = rDs.Tables[0].Rows[zCont]["nrLograd"].ToString();
                        else
                            xAcidente.nrLograd = "S/N";

                        if (rDs.Tables[0].Rows[zCont]["Complemento"].ToString().Trim() != "")
                            xAcidente.complemento = rDs.Tables[0].Rows[zCont]["Complemento"].ToString().Trim();

                        if (rDs.Tables[0].Rows[zCont]["Bairro"].ToString().Trim() != "")
                            xAcidente.bairro = rDs.Tables[0].Rows[zCont]["Bairro"].ToString().Trim();

                        if (rDs.Tables[0].Rows[zCont]["CEP"].ToString().Trim() != "")
                            xAcidente.cep = rDs.Tables[0].Rows[zCont]["CEP"].ToString().Trim();

                        if (zMunic != "")
                            xAcidente.codMunic = zMunic;   //tabela de-para  municípios



                        if (rDs.Tables[0].Rows[zCont]["tpLocal"].ToString().Trim() == "2")
                            xAcidente.pais = "105";  //tabela de-para   países




                        if (rDs.Tables[0].Rows[zCont]["tpLocal"].ToString().Trim() == "2")
                            xAcidente.codPostal = rDs.Tables[0].Rows[zCont]["CodPostal"].ToString().Trim();      //apenas se tpLocal=2

                    }
                    else
                    {
                        //pegar endereço do cadastro da empresa
                        ArrayList list = new Cliente().Find(" dbo.udf_getnumeric(NomeCodigo) = '" + rDs.Tables[0].Rows[zCont]["CNPJ"].ToString().Trim() + "' ");

                        Cliente zCliente = new Cliente();

                        foreach (Cliente rCliente in list)
                        {
                            zCliente.Find(rCliente.Id);
                            break;
                        }


                        //zCliente.Find(" dbo.udf_getnumeric(NomeCodigo) = '" + rDs.Tables[0].Rows[zCont]["CNPJ"].ToString().Trim() + "' ");

                        if (zCliente.Id != 0)
                        {
                            Ilitera.Common.Endereco rEndereco = new Ilitera.Common.Endereco();
                            rEndereco.Find(" IdPessoa = " + zCliente.Id);


                            if (rEndereco.Numero.Trim() != "")
                                xAcidente.nrLograd = rEndereco.Numero;

                            if (rEndereco.Bairro.Trim() != "")
                                xAcidente.bairro = rEndereco.Bairro;

                            if (rEndereco.Cep.Trim() != "")
                                xAcidente.cep = rEndereco.Cep.Replace("-", string.Empty);


                            xAcidente.dscLocal = "Empresa";

                            if (rEndereco.Logradouro.Trim() != "")
                                xAcidente.dscLograd = rEndereco.Logradouro;

                            if (rEndereco.Complemento.Trim() != "")
                                xAcidente.complemento = rEndereco.Complemento;

                            if (rDs.Tables[0].Rows[zCont]["tpLocal"].ToString().Trim() == "2")
                                xAcidente.pais = "105";  //tabela de-para   países

                            rEndereco.IdTipoLogradouro.Find();
                            xAcidente.tpLograd = rEndereco.IdTipoLogradouro.NomeAbreviado.Trim().ToUpper();

                            if (rDs.Tables[0].Rows[zCont]["tpLocal"].ToString().Trim() == "2")
                                xAcidente.codPostal = rDs.Tables[0].Rows[zCont]["CodPostal"].ToString().Trim();      //apenas se tpLocal=2

                            rEndereco.IdMunicipio.Find();
                            xAcidente.codMunic = rEndereco.IdMunicipio.Cod_Municipio_Completo.ToString().Trim();


                        }


                    }










                    //versão 02/2020 - excluir CODAMB
                    //Ilitera.Data.Clientes_Funcionarios zAmb = new Ilitera.Data.Clientes_Funcionarios();

                    //string zCodAmb = zAmb.Trazer_Cod_GHE_Atual_Colaborador(System.Convert.ToInt32(rDs.Tables[0].Rows[zCont]["nId_Empregado"].ToString()));

                    //xAcidente.codAmb = zCodAmb;    //pegar cód GHE 








                    if (rDs.Tables[0].Rows[zCont]["UF"] != null && rDs.Tables[0].Rows[zCont]["UF"].ToString().ToUpper().Trim() != "")
                    {

                        //eSocialEvtCATCatLocalAcidenteUF xUF = new eSocialEvtCATCatLocalAcidenteUF();
                        xAcidente.uf = TS_uf.SP;
                        //xUF = eSocialEvtCATCatLocalAcidenteUF.SP;
                        switch (rDs.Tables[0].Rows[zCont]["UF"].ToString().ToUpper().Trim())
                        {

                            case "SP":
                                xAcidente.uf = TS_uf.SP;
                                break;
                            case "RJ":
                                xAcidente.uf = TS_uf.RJ;
                                break;
                            case "AC":
                                xAcidente.uf = TS_uf.AC;
                                break;
                            case "AL":
                                xAcidente.uf = TS_uf.AL;
                                break;
                            case "AM":
                                xAcidente.uf = TS_uf.AM;
                                break;
                            case "AP":
                                xAcidente.uf = TS_uf.AP;
                                break;
                            case "BA":
                                xAcidente.uf = TS_uf.BA;
                                break;
                            case "CE":
                                xAcidente.uf = TS_uf.CE;
                                break;
                            case "DF":
                                xAcidente.uf = TS_uf.DF;
                                break;
                            case "ES":
                                xAcidente.uf = TS_uf.ES;
                                break;
                            case "GO":
                                xAcidente.uf = TS_uf.GO;
                                break;
                            case "MA":
                                xAcidente.uf = TS_uf.MA;
                                break;
                            case "MG":
                                xAcidente.uf = TS_uf.MG;
                                break;
                            case "MS":
                                xAcidente.uf = TS_uf.MS;
                                break;
                            case "MT":
                                xAcidente.uf = TS_uf.MT;
                                break;
                            case "PA":
                                xAcidente.uf = TS_uf.PA;
                                break;
                            case "PB":
                                xAcidente.uf = TS_uf.PB;
                                break;
                            case "PE":
                                xAcidente.uf = TS_uf.PE;
                                break;
                            case "PI":
                                xAcidente.uf = TS_uf.PI;
                                break;
                            case "PR":
                                xAcidente.uf = TS_uf.PR;
                                break;
                            case "RN":
                                xAcidente.uf = TS_uf.RN;
                                break;
                            case "RO":
                                xAcidente.uf = TS_uf.RO;
                                break;
                            case "RR":
                                xAcidente.uf = TS_uf.RR;
                                break;
                            case "RS":
                                xAcidente.uf = TS_uf.RS;
                                break;
                            case "SC":
                                xAcidente.uf = TS_uf.SC;
                                break;
                            case "SE":
                                xAcidente.uf = TS_uf.SE;
                                break;
                            case "TO":
                                xAcidente.uf = TS_uf.TO;
                                break;
                            default:
                                xAcidente.uf = TS_uf.SP;
                                break;
                        }

                        //xAcidente.uf = xUF;
                        xAcidente.ufSpecified = true;

                    }
                    else
                    {
                        xAcidente.ufSpecified = false;
                    }




                    eSocialEvtCATCatLocalAcidenteIdeLocalAcid xLocalAcid = new eSocialEvtCATCatLocalAcidenteIdeLocalAcid();
                    xLocalAcid.tpInsc = 1;
                    //xLocalAcid.nrInsc = rDs.Tables[0].Rows[zCont]["CNPJ"].ToString().Substring(0, 8);  //CNPJ
                    xLocalAcid.nrInsc = xCNPJ;


                    xAcidente.ideLocalAcid = xLocalAcid;


                    xEvtCat.localAcidente = xAcidente;





                    //parte atingida
                    xParte.codParteAting = rDs.Tables[0].Rows[zCont]["CodParteAting"].ToString();
                    xParte.lateralidade = System.Convert.ToSByte(rDs.Tables[0].Rows[zCont]["Lateralidade"].ToString());


                    //ajustar 
                    xEvtCat.parteAtingida = xParte;



                    //agente causador
                    xAgente.codAgntCausador = rDs.Tables[0].Rows[zCont]["CodAgntCausador"].ToString();

                    xEvtCat.agenteCausador = xAgente;






                    //atestado
                    if (rDs.Tables[0].Rows[zCont]["dtAtendimento"].ToString().Trim() != "")
                    {
                        xAtestado.dtAtendimento = System.Convert.ToDateTime(rDs.Tables[0].Rows[zCont]["dtAtendimento"].ToString(), ptBr);

                        if (rDs.Tables[0].Rows[zCont]["hrAtendimento"].ToString().Trim() != "")
                            xAtestado.hrAtendimento = rDs.Tables[0].Rows[zCont]["hrAtendimento"].ToString().Replace(":", "");
                        else
                            xAtestado.hrAtendimento = "0000";



                        if (rDs.Tables[0].Rows[zCont]["IntInternacao"].ToString().Trim() == "S")
                            xAtestado.indInternacao = TS_sim_nao.S;
                        else
                            xAtestado.indInternacao = TS_sim_nao.N;

                        xAtestado.durTrat = rDs.Tables[0].Rows[zCont]["durTrat"].ToString();



                        if (rDs.Tables[0].Rows[zCont]["IndAfast"].ToString().Trim() == "S")
                            xAtestado.indAfast = TS_sim_nao.S;
                        else
                            xAtestado.indAfast = TS_sim_nao.N;

                        xAtestado.dscLesao = rDs.Tables[0].Rows[zCont]["dscLesao"].ToString();

                        if (rDs.Tables[0].Rows[zCont]["dscCompLesao"].ToString().Trim() != "")
                            xAtestado.dscCompLesao = rDs.Tables[0].Rows[zCont]["dscCompLesao"].ToString();

                        xAtestado.diagProvavel = rDs.Tables[0].Rows[zCont]["DiagnosticoProvavel"].ToString();
                        xAtestado.codCID = rDs.Tables[0].Rows[zCont]["CodCID"].ToString();

                        if (rDs.Tables[0].Rows[zCont]["Observacao"].ToString().Trim() != "")
                            xAtestado.observacao = rDs.Tables[0].Rows[zCont]["Observacao"].ToString();





                        //    emitente
                        xEmitente.ideOC = System.Convert.ToSByte(rDs.Tables[0].Rows[zCont]["ideOC"].ToString());
                        xEmitente.nmEmit = rDs.Tables[0].Rows[zCont]["nmEmit"].ToString();
                        xEmitente.nrOC = rDs.Tables[0].Rows[zCont]["nrOC"].ToString();



                        //eSocialEvtCATCatAtestadoEmitenteUfOC xUF2 = new eSocialEvtCATCatAtestadoEmitenteUfOC();


                        //ver se está certo esse evento
                        xEmitente.ufOC = TS_uf.SP;

                        switch (rDs.Tables[0].Rows[zCont]["ufOC"].ToString().Trim())
                        {
                            case "SP":
                                //xUF2.ufOC = TS_uf.SP;
                                xEmitente.ufOC = TS_uf.SP;
                                break;
                            case "RJ":
                                xEmitente.ufOC = TS_uf.RJ;
                                break;
                            case "AC":
                                xEmitente.ufOC = TS_uf.AC;
                                break;
                            case "AL":
                                xEmitente.ufOC = TS_uf.AL;
                                break;
                            case "AM":
                                xEmitente.ufOC = TS_uf.AM;
                                break;
                            case "AP":
                                xEmitente.ufOC = TS_uf.AP;
                                break;
                            case "BA":
                                xEmitente.ufOC = TS_uf.BA;
                                break;
                            case "CE":
                                xEmitente.ufOC = TS_uf.CE;
                                break;
                            case "DF":
                                xEmitente.ufOC = TS_uf.DF;
                                break;
                            case "ES":
                                xEmitente.ufOC = TS_uf.ES;
                                break;
                            case "GO":
                                xEmitente.ufOC = TS_uf.GO;
                                break;
                            case "MA":
                                xEmitente.ufOC = TS_uf.MA;
                                break;
                            case "MG":
                                xEmitente.ufOC = TS_uf.MG;
                                break;
                            case "MS":
                                xEmitente.ufOC = TS_uf.MS;
                                break;
                            case "MT":
                                xEmitente.ufOC = TS_uf.MT;
                                break;
                            case "PA":
                                xEmitente.ufOC = TS_uf.PA;
                                break;
                            case "PB":
                                xEmitente.ufOC = TS_uf.PB;
                                break;
                            case "PE":
                                xEmitente.ufOC = TS_uf.PE;
                                break;
                            case "PI":
                                xEmitente.ufOC = TS_uf.PI;
                                break;
                            case "PR":
                                xEmitente.ufOC = TS_uf.PR;
                                break;
                            case "RN":
                                xEmitente.ufOC = TS_uf.RN;
                                break;
                            case "RO":
                                xEmitente.ufOC = TS_uf.RO;
                                break;
                            case "RR":
                                xEmitente.ufOC = TS_uf.RR;
                                break;
                            case "RS":
                                xEmitente.ufOC = TS_uf.RS;
                                break;
                            case "SC":
                                xEmitente.ufOC = TS_uf.SC;
                                break;
                            case "SE":
                                xEmitente.ufOC = TS_uf.SE;
                                break;
                            case "TO":
                                xEmitente.ufOC = TS_uf.TO;
                                break;
                            default:
                                xEmitente.ufOC = TS_uf.SP;
                                break;
                        }
                        //xEmitente.ufOC = xUF2;
                        //xEmitente.ufOCSpecified = true;

                        xAtestado.emitente = xEmitente;


                        xEvtCat.atestado = xAtestado;

                    }


                    //catorigem
                    //if (rDs.Tables[0].Rows[zCont]["nrCatOrig"].ToString().Trim() != "" && rDs.Tables[0].Rows[zCont]["nrCatOrig"].ToString().Trim() != "0")
                    //{
                    //    xCatOrigem.nrRecCatOrig = rDs.Tables[0].Rows[zCont]["nrCatOrig"].ToString();

                    //    xEvtCat.catOrigem = xCatOrigem;
                    //}



                    xCat.cat = xEvtCat;

                    x2210.evtCAT = xCat;

                    ////criar xml
                    //if (rd_Todos.Checked == true)
                    //{
                    xArq = "I:\\temp\\" +  txt_Arq.Text.Substring(0, txt_Arq.Text.IndexOf(".")) + "_2210_" + rDs.Tables[0].Rows[zCont]["CPF"].ToString().Trim() + "_" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + "_" + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString() + ".XML";
                    //}
                    //else
                    //{
                    //    xArq = txt_Arq.Text;
                    //}




                    //CRIAR XML
                    string xContent;

                    XmlSerializer serializer = new XmlSerializer(typeof(eSocial_2210));

                    var memoryStream = new MemoryStream();
                    var streamWriter = new StreamWriter(memoryStream, System.Text.Encoding.UTF8);

                    serializer.Serialize(streamWriter, x2210);

                    byte[] bytes = memoryStream.ToArray();
                    xContent = Encoding.UTF8.GetString(bytes);

                    using (FileStream fs = File.Create(xArq))
                    {
                        fs.Write(bytes, 0, bytes.Length);
                        fs.Close();
                    }




                    string text = File.ReadAllText(xArq);
                    text = text.Replace("xmlns:xsi=", "");
                    text = text.Replace("\"http://www.w3.org/2001/XMLSchema-instance\"", "");
                    text = text.Replace("xmlns:xsd=", "");
                    text = text.Replace("\"http://www.w3.org/2001/XMLSchema\"", "");
                    //text = text.Replace("evtCAT", "evtAfastTemp");
                    //text = text.Replace("v_S_01_02_00", "v_S_01_02_00:eSocial_2210");
                    text = text.Replace("v_S_01_02_00:eSocial_2210", "v_S_01_02_00");
                    text = text.Replace("eSocial_2210", "eSocial");

                    File.WriteAllText(xArq, text);

                    xContent = xContent.Replace("xmlns:xsi=", "");
                    xContent = xContent.Replace("\"http://www.w3.org/2001/XMLSchema-instance\"", "");
                    xContent = xContent.Replace("xmlns:xsd=", "");
                    xContent = xContent.Replace("\"http://www.w3.org/2001/XMLSchema\"", "");
                    //xContent = xContent.Replace("evtCAT", "evtAfastTemp");
                    //xContent = xContent.Replace("v_S_01_02_00", "v_S_01_02_00:eSocial_2210");
                    xContent = xContent.Replace("v_S_01_02_00:eSocial_2210", "v_S_01_02_00");
                    xContent = xContent.Replace("eSocial_2210", "eSocial");

                    //using (Stream stream = File.Open(xArq, FileMode.Create))
                    //{
                    //    XmlSerializer serializer = new XmlSerializer(typeof(eSocialEvtCAT));
                    //    serializer.Serialize(stream, xCat);
                    //    stream.Flush();

                    //    byte[] bytes = new byte[stream.Length];
                    //    stream.Position = 0;
                    //    stream.Read(bytes, 0, (int)stream.Length);
                    //    xContent = Encoding.ASCII.GetString(bytes);

                    //    stream.Close();

                    //}


                    //salvar XML no SQL server com xeSocial.Id e nome do arquivo criado e datahora 

                    lst_Arqs.Items.Add(xArq);


                    Int64 zPis = 0;
                    if (rDs.Tables[0].Rows[zCont]["PIS"].ToString().Trim() != "")
                        zPis = System.Convert.ToInt64(System.Text.RegularExpressions.Regex.Match(rDs.Tables[0].Rows[zCont]["PIS"].ToString().Trim(), @"\d+").Value);

                    Ilitera.Data.eSocial zeSocial = new Ilitera.Data.eSocial();
                    zeSocial.XML_Deposito(xArq, xeSocial.Id, zCont, xContent, rDs.Tables[0].Rows[zCont]["CPF"].ToString(), zPis, rDs.Tables[0].Rows[zCont]["CNPJ"].ToString(), dtp_Inicial.Text, dtp_Final.Text, System.Convert.ToInt32(rDs.Tables[0].Rows[0]["IdAcidente"].ToString()), xCat.Id,0);




                }
                catch (Exception ex)
                {                    
                    MsgBox1.Show("Ilitera.Net", "Erro:" + ex.ToString(), null, new EO.Web.MsgBoxButton("OK"));
                    return;
                }



            }




            using (ZipFile zip = new ZipFile())
            {
                zip.AlternateEncodingUsage = ZipOption.AsNecessary;
                zip.AddDirectoryByName("Files");

                for (int rCont = 0; rCont < lst_Arqs.Items.Count; rCont++)
                {
                    zip.AddFile(lst_Arqs.Items[rCont].Text, "Files");
                }
                Response.Clear();
                Response.BufferOutput = false;
                string zipName = String.Format("eSocial2210_Zip_{0}.zip", DateTime.Now.ToString("yyyy-MMM-dd-HHmmss"));
                Response.ContentType = "application/zip";
                Response.AddHeader("content-disposition", "attachment; filename=" + zipName);
                zip.Save(Response.OutputStream);
                Response.End();
            }


            MsgBox1.Show("Ilitera.Net", "Arquivo(s) criados.", null, new EO.Web.MsgBoxButton("OK"));
            return;

        }


        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]
        private void Processar_S1005()
        {


            Int32 zEmpresaGrupo = 0;

            string xArq = "";

            System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");

            zEmpresaGrupo = System.Convert.ToInt32( Session["Empresa"].ToString() );



            Ilitera.Data.eSocial zEsocial = new Ilitera.Data.eSocial();

            DataSet rDs = zEsocial.Trazer_1005(zEmpresaGrupo);

            if (rDs.Tables[0].Rows.Count < 1)
            {                
                MsgBox1.Show("Ilitera.Net", "Não há registros para gerar o evento selecionado.", null, new EO.Web.MsgBoxButton("OK"));
                return;
            }


            //criar evento de controle para este lote
            tbleSocial xeSocial = new tbleSocial();
            xeSocial.DataHora_Criacao = System.DateTime.Now;
            xeSocial.Evento = "1005";
            Entities.Usuario user = (Entities.Usuario)Session["usuarioLogado"];
            xeSocial.IdUsuario = user.IdUsuario;
            xeSocial.Save();



            for (int zCont = 0; zCont < rDs.Tables[0].Rows.Count; zCont++)
            {

                //eSocial_1005 x1005 = new eSocial_1005();

                //eSocialEvtTabEstab xObra = new eSocialEvtTabEstab();
                //eSocialEvtTabEstabInfoEstabInclusao xEstab = new eSocialEvtTabEstabInfoEstabInclusao();

                //eSocialEvtTabEstabInfoEstab xEstabMain = new eSocialEvtTabEstabInfoEstab();

                //TIdeCadastro xIdeEvento = new TIdeCadastro();
                //TEmpregador xIdeEmpregador = new TEmpregador();

                //TIdeEstab xIdeEstab = new TIdeEstab();

                try
                {
                //    xObra.Id = Gerar_ID(rDs.Tables[0].Rows[zCont]["nrInsc"].ToString().Trim().Substring(0, 8), zCont);


                //    //Empregador
                //    xIdeEmpregador.tpInsc = 1;
                //    xIdeEmpregador.nrInsc = rDs.Tables[0].Rows[zCont]["nrInsc"].ToString().Substring(0, 8);  //CNPJ

                //    xObra.ideEmpregador = xIdeEmpregador;




                //    ////////////////////////////////////////////////

                //    //IdeEvento


                //    xIdeEvento.procEmi = 1;
                //    xIdeEvento.tpAmb = 1;   // 2 se produção restrita
                //    xIdeEvento.verProc = "1";

                //    xObra.ideEvento = xIdeEvento;


                //    TDadosEstab xDadosEstab = new TDadosEstab();
                //    //eSocialEvtTabEstabInfoEstab xDadosEstab = new eSocialEvtTabEstabInfoEstab();


                //    xDadosEstab.cnaePrep = rDs.Tables[0].Rows[0]["CNAE"].ToString();

                //    TDadosEstabAliqGilrat xAliq = new TDadosEstabAliqGilrat();


                //    if (rDs.Tables[0].Rows[0]["FAP"].ToString()!="" )
                //       xAliq.fap = System.Convert.ToDecimal(rDs.Tables[0].Rows[0]["FAP"].ToString());

                //    //preencher
                //    //xAliq.aliqRatAjust
                //    //xAliq.aliqRat



                //    //quando preencher processo adm ou judicial modificando aliquota RAT
                //    //TDadosEstabAliqGilratProcAdmJudRat xAdmJudRat = new TDadosEstabAliqGilratProcAdmJudRat();

                //    //xAdmJudRat.codSusp
                //    //xAdmJudRat.nrProc    
                //    //xAdmJudRat.tpProc

                //    //xAliq.procAdmJudRat = xAdmJudRat;


                //    //quando preencher processo adm ou judicial modificando aliquota FAP
                //    //TDadosEstabAliqGilratProcAdmJudFap xAdmJudFap = new TDadosEstabAliqGilratProcAdmJudFap();

                //    //xAdmJudFap.tpProc
                //    //xAdmJudFap.nrProc
                //    //xAdmJudFap.codSusp

                //    //xAliq.procAdmJudFap = xAdmJudFap;


                //    xDadosEstab.aliqGilrat = xAliq;


                //    //quando preencher relacionado atividade pessoa física
                //    //TDadosEstabInfoCaepf xCaepf = new TDadosEstabInfoCaepf();

                //    //xCaepf.tpCaepf

                //    //xDadosEstab.infoCaepf = xCaepf;



                //    //quando preencher construtoras contribuição patronal
                //    //TDadosEstabInfoObra xInfoObra = new TDadosEstabInfoObra();

                //    //xInfoObra.indSubstPatrObra

                //    //xDadosEstab.infoObra = xInfoObra;




                //    //quando indicar informações trabalhistas relativas ao estabelecimento
                //    //TDadosEstabInfoTrab xInfoTrab = new TDadosEstabInfoTrab();

                //    //quando indicar tipo de ponto 
                //    //xInfoTrab.regPt


                //    //quando indicar contratação de PCD
                //    //TDadosEstabInfoTrabInfoPCD xInfoPCD = new TDadosEstabInfoTrabInfoPCD();

                //    //xInfoPCD.contPCD
                //    //xInfoPCD.nrProcJud

                //    //xInfoTrab.infoPCD = xInfoPCD;


                //    //quando indicar contratação de aprendiz
                //    //TDadosEstabInfoTrabInfoApr xInfoApr = new TDadosEstabInfoTrabInfoApr();

                //    //xInfoApr.contApr
                //    //xInfoApr.contEntEd
                //    //xInfoApr.nrProcJud

                //    //TDadosEstabInfoTrabInfoAprInfoEntEduc[] xEntEduc = new TDadosEstabInfoTrabInfoAprInfoEntEduc[1];
                //    //xEntEduc[0].nrInsc

                //    //xInfoApr.infoEntEduc = xEntEduc;


                //    //xInfoTrab.infoApr = xInfoApr;


                //    //xDadosEstab.infoTrab = xInfoTrab;




                //    xEstab.dadosEstab = xDadosEstab;



                //    xIdeEstab.iniValid = "2010-01";
                //    xIdeEstab.tpInsc = 1;
                //    xIdeEstab.nrInsc = rDs.Tables[0].Rows[zCont]["nrInsc"].ToString().Substring(0, 8);  //CNPJ

                //    xEstab.ideEstab = xIdeEstab;


                //    xEstabMain.Item = xEstab;

                //    xObra.infoEstab = xEstabMain;

                //    x1005.evtTabEstab = xObra;


                ////criar xml
                //if (rd_Todos.Checked == true)
                //{
                xArq = "I:\\temp\\" + txt_Arq.Text.Substring(0, txt_Arq.Text.IndexOf(".")) + "_1005_" + rDs.Tables[0].Rows[zCont]["nrInsc"].ToString().Trim() + "_" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + "_" + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString() + ".XML";
                //}
                //else
                //{
                //    xArq = txt_Arq.Text;
                //}




                //CRIAR XML
                string xContent = "";


                //XmlSerializer serializer = new XmlSerializer(typeof(eSocial_1005));

                //var memoryStream = new MemoryStream();
                //var streamWriter = new StreamWriter(memoryStream, System.Text.Encoding.UTF8);

                //serializer.Serialize(streamWriter, x1005);

                //byte[] bytes = memoryStream.ToArray();
                //xContent = Encoding.UTF8.GetString(bytes);

                //using (FileStream fs = File.Create(xArq))
                //{
                //    fs.Write(bytes, 0, bytes.Length);
                //    fs.Close();
                //}



                //string text = File.ReadAllText(xArq);
                //text = text.Replace("xmlns:xsi=", "");
                //text = text.Replace("\"http://www.w3.org/2001/XMLSchema-instance\"", "");
                //text = text.Replace("xmlns:xsd=", "");
                //text = text.Replace("\"http://www.w3.org/2001/XMLSchema\"", "");
                //File.WriteAllText(xArq, text);

                //xContent = xContent.Replace("xmlns:xsi=", "");
                //xContent = xContent.Replace("\"http://www.w3.org/2001/XMLSchema-instance\"", "");
                //xContent = xContent.Replace("xmlns:xsd=", "");
                //xContent = xContent.Replace("\"http://www.w3.org/2001/XMLSchema\"", "");

                ////using (Stream stream = File.Open(xArq, FileMode.Create))
                ////{
                ////    XmlSerializer serializer = new XmlSerializer(typeof(eSocialEvtTabEstab));
                ////    serializer.Serialize(stream, xObra);
                ////    stream.Flush();

                ////    byte[] bytes = new byte[stream.Length];
                ////    stream.Position = 0;
                ////    stream.Read(bytes, 0, (int)stream.Length);
                ////    xContent = Encoding.ASCII.GetString(bytes);

                ////    stream.Close();

                ////}

                //lst_Arqs.Items.Add(xArq);

                ////salvar XML no SQL server com xeSocial.Id e nome do arquivo criado e datahora 
                //Int64 zPis = 0;

                //Ilitera.Data.eSocial zeSocial = new Ilitera.Data.eSocial();
                //zeSocial.XML_Deposito(xArq, xeSocial.Id, zCont, xContent, "", zPis, rDs.Tables[0].Rows[zCont]["nrInsc"].ToString(), dtp_Inicial.Text, dtp_Final.Text, System.Convert.ToInt32(rDs.Tables[0].Rows[0]["IdPessoa"].ToString()), xObra.Id);


            

                }
                catch (Exception ex)
                {                    
                    MsgBox1.Show("Ilitera.Net", "Erro:" + ex.ToString(), null, new EO.Web.MsgBoxButton("OK"));
                    return;
                }



            }


            using (ZipFile zip = new ZipFile())
            {
                zip.AlternateEncodingUsage = ZipOption.AsNecessary;
                zip.AddDirectoryByName("Files");

                for (int rCont = 0; rCont < lst_Arqs.Items.Count; rCont++)
                {
                    zip.AddFile(lst_Arqs.Items[rCont].Text, "Files");
                }
                Response.Clear();
                Response.BufferOutput = false;
                string zipName = String.Format("eSocial1005_Zip_{0}.zip", DateTime.Now.ToString("yyyy-MMM-dd-HHmmss"));
                Response.ContentType = "application/zip";
                Response.AddHeader("content-disposition", "attachment; filename=" + zipName);
                zip.Save(Response.OutputStream);
                Response.End();
            }



            MsgBox1.Show("Ilitera.Net", "Arquivo(s) Criados.", null, new EO.Web.MsgBoxButton("OK"));
            return;

        }



        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]
        private void Processar_S2245()
        {

            Int32 zEmpresa = 0;
            Int32 zColaborador = 0;
            Int32 zEmpresaGrupo = 0;

            string xArq = "";

            System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");



            if (rd_Todos.Checked == true)
            {
                zColaborador = 0;
                zEmpresa = System.Convert.ToInt32( Session["Empresa"].ToString() );
                zEmpresaGrupo = 0;
            }
            else if (rd_Colaborador.Checked == true)
            {
                if (cmb_Colaborador.SelectedIndex < 0)
                {                    
                    MsgBox1.Show("Ilitera.Net", "Selecione colaborador.", null, new EO.Web.MsgBoxButton("OK"));
                    return;
                }

                zEmpresa = 0;
                zEmpresaGrupo = 0;
                zColaborador = System.Convert.ToInt32(lst_Id.Items[cmb_Colaborador.SelectedIndex].ToString());
            }
            else if (rd_Grupo.Checked == true)
            {
                zEmpresa = 0;
                zColaborador = 0;
                zEmpresaGrupo = System.Convert.ToInt32( Session["Empresa"].ToString() );
            }




            Ilitera.Data.eSocial zEsocial = new Ilitera.Data.eSocial();

            DataSet rDs = zEsocial.Trazer_2245(zEmpresa, zColaborador, zEmpresaGrupo, dtp_Inicial.Text, dtp_Final.Text);

            if (rDs.Tables[0].Rows.Count < 1)
            {                
                MsgBox1.Show("Ilitera.Net", "Não há registros para gerar o evento selecionado.", null, new EO.Web.MsgBoxButton("OK"));
                return;
            }



            //criar evento de controle para este lote
            tbleSocial xeSocial = new tbleSocial();
            xeSocial.DataHora_Criacao = System.DateTime.Now;
            xeSocial.Evento = "2245";
            Entities.Usuario user = (Entities.Usuario)Session["usuarioLogado"];
            xeSocial.IdUsuario = user.IdUsuario;
            xeSocial.Save();


            //salvar resultado do dataset como controle de geração também

            eSocial_2245 x2245 = new eSocial_2245();

            //eSocialEvtTreiCap xTrei = new eSocialEvtTreiCap();


            //TIdeEveTrab xIdeEvento = new TIdeEveTrab();
            //TEmpregador xIdeEmpregador = new TEmpregador();
            //TIdeVinculoEstag xIdeVinculo = new TIdeVinculoEstag();
            ////eSocialEvtTreiCapIdeTrabalhador xIdeVinculo = new eSocialEvtTreiCapIdeTrabalhador();


            //for (int zCont = 0; zCont < rDs.Tables[0].Rows.Count; zCont++)
            //{

                try
                {


            //        xTrei.Id = Gerar_ID(rDs.Tables[0].Rows[zCont]["CNPJ"].ToString().Trim().Substring(0, 8), zCont);


            //        //Empregador
            //        xIdeEmpregador.tpInsc = 1;
            //        xIdeEmpregador.nrInsc = rDs.Tables[0].Rows[0]["CNPJ"].ToString().Substring(0, 8);  //CNPJ

            //        xTrei.ideEmpregador = xIdeEmpregador;


            //        ////////////////////////////////////////////////

            //        //IdeEvento
            //        xIdeEvento.indRetif = 1;
            //        xIdeEvento.nrRecibo = "";
            //        xIdeEvento.procEmi = 1;
            //        xIdeEvento.tpAmb = 1;
            //        xIdeEvento.verProc = "1";

            //        xTrei.ideEvento = xIdeEvento;

            //        ////////////////////////////////////////////////


            //        //levantar quantos CPFs estão nesta relação,  se todos está selecionado



            //        //IdeVinculo
            //        xIdeVinculo.cpfTrab = rDs.Tables[0].Rows[zCont]["CPF"].ToString();
            //        xIdeVinculo.matricula = rDs.Tables[0].Rows[zCont]["Matricula"].ToString();
            //        xIdeVinculo.nisTrab = rDs.Tables[0].Rows[zCont]["PIS"].ToString();
            //        xIdeVinculo.codCateg = "101";

            //        //xTrei.ideTrabalhador = xIdeVinculo;
            //        xTrei.ideVinculo = xIdeVinculo;



            //        eSocialEvtTreiCapTreiCap xTreiCap = new eSocialEvtTreiCapTreiCap();

            //        xTreiCap.codTreiCap = rDs.Tables[0].Rows[zCont]["CodTreiCap"].ToString();
            //        xTreiCap.obsTreiCap = "";


            //        eSocialEvtTreiCapTreiCapInfoComplem xTreiCompl = new eSocialEvtTreiCapTreiCapInfoComplem();
            //        xTreiCompl.dtTreiCap = System.Convert.ToDateTime(rDs.Tables[0].Rows[zCont]["DtTreiCap"].ToString(), ptBr);
            //        xTreiCompl.durTreiCap = System.Convert.ToDecimal(rDs.Tables[0].Rows[zCont]["DurTreiCap"].ToString());

            //        xTreiCompl.modTreiCap = 1;
            //        xTreiCompl.tpTreiCap = System.Convert.ToSByte(rDs.Tables[0].Rows[zCont]["tpTreiCap"].ToString());




            //        eSocialEvtTreiCapTreiCapInfoComplemIdeProfResp[] xTreiResp = new eSocialEvtTreiCapTreiCapInfoComplemIdeProfResp[1];

            //        xTreiResp[0] = new eSocialEvtTreiCapTreiCapInfoComplemIdeProfResp();

            //        xTreiResp[0].cpfProf = rDs.Tables[0].Rows[zCont]["CPFProf"].ToString();
            //        xTreiResp[0].nmProf = rDs.Tables[0].Rows[zCont]["nmProf"].ToString();

            //        if (rDs.Tables[0].Rows[zCont]["tpProf"].ToString().Trim() == "1")
            //            xTreiResp[0].tpProf = 1;
            //        else
            //            xTreiResp[0].tpProf = 2;

            //        xTreiResp[0].formProf = rDs.Tables[0].Rows[zCont]["FormProf"].ToString();
            //        xTreiResp[0].codCBO = rDs.Tables[0].Rows[zCont]["codCBO"].ToString();
            //        xTreiResp[0].nacProf = 1;

            //        xTreiCompl.ideProfResp = xTreiResp;


            //        xTreiCap.infoComplem = xTreiCompl;


            //        xTrei.treiCap = xTreiCap;

            //        x2245.evtTreiCap = xTrei;
                    // xCat.cat = xEvtCat;


                    ////criar xml
                    //if (rd_Todos.Checked == true)
                    //{
               //     xArq = "I:\\temp\\" + txt_Arq.Text.Substring(0, txt_Arq.Text.IndexOf(".")) + "_2245_" + rDs.Tables[0].Rows[zCont]["CPF"].ToString().Trim() + "_" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + "_" + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString() + ".XML";
                    //}
                    //else
                    //{
                    //    xArq = txt_Arq.Text;
                    //}





                    //CRIAR XML
                    string xContent = "";

                    XmlSerializer serializer = new XmlSerializer(typeof(eSocial_2245));

                    var memoryStream = new MemoryStream();
                    var streamWriter = new StreamWriter(memoryStream, System.Text.Encoding.UTF8);

                    serializer.Serialize(streamWriter, x2245);

                    byte[] bytes = memoryStream.ToArray();
                    xContent = Encoding.UTF8.GetString(bytes);

                    using (FileStream fs = File.Create(xArq))
                    {
                        fs.Write(bytes, 0, bytes.Length);
                        fs.Close();
                    }



                    string text = File.ReadAllText(xArq);
                    text = text.Replace("xmlns:xsi=", "");
                    text = text.Replace("\"http://www.w3.org/2001/XMLSchema-instance\"", "");
                    text = text.Replace("xmlns:xsd=", "");
                    text = text.Replace("\"http://www.w3.org/2001/XMLSchema\"", "");
                    File.WriteAllText(xArq, text);

                    xContent = xContent.Replace("xmlns:xsi=", "");
                    xContent = xContent.Replace("\"http://www.w3.org/2001/XMLSchema-instance\"", "");
                    xContent = xContent.Replace("xmlns:xsd=", "");
                    xContent = xContent.Replace("\"http://www.w3.org/2001/XMLSchema\"", "");

                    //using (Stream stream = File.Open(xArq, FileMode.Create))
                    //{
                    //    XmlSerializer serializer = new XmlSerializer(typeof(eSocialEvtTreiCap));
                    //    serializer.Serialize(stream, xTrei);
                    //    stream.Flush();

                    //    byte[] bytes = new byte[stream.Length];
                    //    stream.Position = 0;
                    //    stream.Read(bytes, 0, (int)stream.Length);
                    //    xContent = Encoding.ASCII.GetString(bytes);

                    //    stream.Close();

                    //}


                    lst_Arqs.Items.Add(xArq);


                    ////salvar XML no SQL server com xeSocial.Id e nome do arquivo criado e datahora 
                    //Int64 zPis = 0;
                    //if (rDs.Tables[0].Rows[zCont]["PIS"].ToString().Trim() != "")
                    //    zPis = System.Convert.ToInt64(System.Text.RegularExpressions.Regex.Match(rDs.Tables[0].Rows[zCont]["PIS"].ToString().Trim(), @"\d+").Value);


                    //Ilitera.Data.eSocial zeSocial = new Ilitera.Data.eSocial();
                    //zeSocial.XML_Deposito(xArq, xeSocial.Id, zCont, xContent, rDs.Tables[0].Rows[zCont]["CPF"].ToString(), zPis, rDs.Tables[0].Rows[zCont]["CNPJ"].ToString(), dtp_Inicial.Text, dtp_Final.Text, System.Convert.ToInt32(rDs.Tables[0].Rows[0]["IdTreinamento"].ToString()), xTrei.Id);




                }

                catch (Exception ex)
                {                    
                    MsgBox1.Show("Ilitera.Net", "Erro:" + ex.ToString(), null, new EO.Web.MsgBoxButton("OK"));
                    return;
                }


            



            using (ZipFile zip = new ZipFile())
            {
                zip.AlternateEncodingUsage = ZipOption.AsNecessary;
                zip.AddDirectoryByName("Files");

                for (int rCont = 0; rCont < lst_Arqs.Items.Count; rCont++)
                {
                    zip.AddFile(lst_Arqs.Items[rCont].Text, "Files");
                }
                Response.Clear();
                Response.BufferOutput = false;
                string zipName = String.Format("eSocial2245_Zip_{0}.zip", DateTime.Now.ToString("yyyy-MMM-dd-HHmmss"));
                Response.ContentType = "application/zip";
                Response.AddHeader("content-disposition", "attachment; filename=" + zipName);
                zip.Save(Response.OutputStream);
                Response.End();
            }



            MsgBox1.Show("Ilitera.Net", "Arquivo(s) Criados.", null, new EO.Web.MsgBoxButton("OK"));
            return;

        }


        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]
        private void Processar_S2221()
        {

            Int32 zEmpresa = 0;
            Int32 zColaborador = 0;
            Int32 zEmpresaGrupo = 0;

            string xArq = "";

            System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");


            if (rd_Todos.Checked == true)
            {
                zColaborador = 0;
                zEmpresaGrupo = 0;
                zEmpresa = System.Convert.ToInt32( Session["Empresa"].ToString() );
            }
            else if (rd_Colaborador.Checked == true)
            {
                if (cmb_Colaborador.SelectedIndex < 0)
                {                    
                    MsgBox1.Show("Ilitera.Net", "Selecione colaborador.", null, new EO.Web.MsgBoxButton("OK"));
                    return;
                }

                zEmpresa = 0;
                zEmpresaGrupo = 0;
                zColaborador = System.Convert.ToInt32(lst_Id.Items[cmb_Colaborador.SelectedIndex].ToString());
            }
            else if (rd_Grupo.Checked == true)
            {
                zEmpresa = 0;
                zColaborador = 0;
                zEmpresaGrupo = System.Convert.ToInt32( Session["Empresa"].ToString() );
            }


            Ilitera.Data.eSocial zEsocial = new Ilitera.Data.eSocial();


            //devo trazer um exame chamado toxicológico, ou pegar todos que examedicionario->istoxicologico = 1 ??
            DataSet rDs = zEsocial.Trazer_2221(zEmpresa, zColaborador, zEmpresaGrupo, dtp_Inicial.Text, dtp_Final.Text);

            if (rDs.Tables[0].Rows.Count < 1)
            {                
                MsgBox1.Show("Ilitera.Net", "Não há registros para gerar o evento selecionado.", null, new EO.Web.MsgBoxButton("OK"));
                return;
            }



            //criar evento de controle para este lote
            tbleSocial xeSocial = new tbleSocial();
            xeSocial.DataHora_Criacao = System.DateTime.Now;
            xeSocial.Evento = "2221";
            Entities.Usuario user = (Entities.Usuario)Session["usuarioLogado"];
            xeSocial.IdUsuario = user.IdUsuario;
            xeSocial.Save();



            for (int zCont = 0; zCont < rDs.Tables[0].Rows.Count; zCont++)
            {

                eSocial_2221 x2221 = new eSocial_2221();

                //eSocialEvtToxic xMonit = new eSocialEvtToxic();
                
                ////eSocialEvtMonitMonitExMedOcupAso xMonitASO = new eSocialEvtMonitMonitExMedOcupAso();

                ////eSocialEvtMonitMonitExMedOcupRespMonit xRespMonitor = new eSocialEvtMonitMonitExMedOcupRespMonit();

                ////eSocialEvtMonitMonitExMedOcupAsoMedico xMedico = new eSocialEvtMonitMonitExMedOcupAsoMedico();



                ////eSocialEvtMonitAsoExameRespMonitUfConsClasse xUFCons = new eSocialEvtMonitAsoExameRespMonitUfConsClasse();
                ////eSocialEvtMonitAsoIdeServSaude xServSaude = new eSocialEvtMonitAsoIdeServSaude();
                ////TMedico xMedico = new TMedico();
                ////TCrm xCRM = new TCrm();
                ////TCrmUfCRM xUF = new TCrmUfCRM();


                //TIdeEveTrab xIdeEvento = new TIdeEveTrab();
                //TEmpregador xIdeEmpregador = new TEmpregador();

                //TIdeVinculoEstag xIdeVinculo = new TIdeVinculoEstag();

                try
                {

                //    xMonit.Id = Gerar_ID(rDs.Tables[0].Rows[zCont]["CNPJ"].ToString().Trim().Substring(0, 8), zCont);



                //    //Empregador
                //    xIdeEmpregador.tpInsc = 1;
                //    xIdeEmpregador.nrInsc = rDs.Tables[0].Rows[0]["CNPJ"].ToString().Substring(0, 8);  //CNPJ

                //    xMonit.ideEmpregador = xIdeEmpregador;


                //    ////////////////////////////////////////////////

                //    //IdeEvento
                //    xIdeEvento.indRetif = 1;
                //    xIdeEvento.nrRecibo = "";
                //    xIdeEvento.procEmi = 1;
                //    xIdeEvento.tpAmb = 1;
                //    xIdeEvento.verProc = "1";

                //    xMonit.ideEvento = xIdeEvento;

                //    ////////////////////////////////////////////////


                //    //levantar quantos CPFs estão nesta relação,  se todos está selecionado





                //    //IdeVinculo
                //    xIdeVinculo.cpfTrab = rDs.Tables[0].Rows[zCont]["CPF"].ToString();
                //    xIdeVinculo.matricula = rDs.Tables[0].Rows[zCont]["Matricula"].ToString();
                //    xIdeVinculo.nisTrab = rDs.Tables[0].Rows[zCont]["PIS"].ToString();
                //    xIdeVinculo.codCateg = "101";

                //    xMonit.ideVinculo = xIdeVinculo;


                //    eSocialEvtToxicToxicologico xMedicoASO = new eSocialEvtToxicToxicologico();

                //    //eSocialEvtMonitMonitExMedOcupAsoMedico xMedicoASO = new eSocialEvtMonitMonitExMedOcupAsoMedico();

                //    //xMedicoASO.ufCRM = rDs.Tables[0].Rows[zCont]["UFMed"].ToString().ToUpper().Trim();


                //    eSocialEvtToxicToxicologicoUfCRM xUF = new eSocialEvtToxicToxicologicoUfCRM();
                //    xUF = eSocialEvtToxicToxicologicoUfCRM.SP;
                //    switch (rDs.Tables[0].Rows[zCont]["UFMed"].ToString().ToUpper().Trim())
                //    {

                //        case "SP":
                //            xUF = eSocialEvtToxicToxicologicoUfCRM.SP;
                //            break;
                //        case "RJ":
                //            xUF = eSocialEvtToxicToxicologicoUfCRM.RJ;
                //            break;
                //        case "AC":
                //            xUF = eSocialEvtToxicToxicologicoUfCRM.AC;
                //            break;
                //        case "AL":
                //            xUF = eSocialEvtToxicToxicologicoUfCRM.AL;
                //            break;
                //        case "AM":
                //            xUF = eSocialEvtToxicToxicologicoUfCRM.AM;
                //            break;
                //        case "AP":
                //            xUF = eSocialEvtToxicToxicologicoUfCRM.AP;
                //            break;
                //        case "BA":
                //            xUF = eSocialEvtToxicToxicologicoUfCRM.BA;
                //            break;
                //        case "CE":
                //            xUF = eSocialEvtToxicToxicologicoUfCRM.CE;
                //            break;
                //        case "DF":
                //            xUF = eSocialEvtToxicToxicologicoUfCRM.DF;
                //            break;
                //        case "ES":
                //            xUF = eSocialEvtToxicToxicologicoUfCRM.ES;
                //            break;
                //        case "GO":
                //            xUF = eSocialEvtToxicToxicologicoUfCRM.GO;
                //            break;
                //        case "MA":
                //            xUF = eSocialEvtToxicToxicologicoUfCRM.MA;
                //            break;
                //        case "MG":
                //            xUF = eSocialEvtToxicToxicologicoUfCRM.MG;
                //            break;
                //        case "MS":
                //            xUF = eSocialEvtToxicToxicologicoUfCRM.MS;
                //            break;
                //        case "MT":
                //            xUF = eSocialEvtToxicToxicologicoUfCRM.MT;
                //            break;
                //        case "PA":
                //            xUF = eSocialEvtToxicToxicologicoUfCRM.PA;
                //            break;
                //        case "PB":
                //            xUF = eSocialEvtToxicToxicologicoUfCRM.PB;
                //            break;
                //        case "PE":
                //            xUF = eSocialEvtToxicToxicologicoUfCRM.PE;
                //            break;
                //        case "PI":
                //            xUF = eSocialEvtToxicToxicologicoUfCRM.PI;
                //            break;
                //        case "PR":
                //            xUF = eSocialEvtToxicToxicologicoUfCRM.PR;
                //            break;
                //        case "RN":
                //            xUF = eSocialEvtToxicToxicologicoUfCRM.RN;
                //            break;
                //        case "RO":
                //            xUF = eSocialEvtToxicToxicologicoUfCRM.RO;
                //            break;
                //        case "RR":
                //            xUF = eSocialEvtToxicToxicologicoUfCRM.RR;
                //            break;
                //        case "RS":
                //            xUF = eSocialEvtToxicToxicologicoUfCRM.RS;
                //            break;
                //        case "SC":
                //            xUF = eSocialEvtToxicToxicologicoUfCRM.SC;
                //            break;
                //        case "SE":
                //            xUF = eSocialEvtToxicToxicologicoUfCRM.SE;
                //            break;
                //        case "TO":
                //            xUF = eSocialEvtToxicToxicologicoUfCRM.TO;
                //            break;
                //        default:
                //            xUF = eSocialEvtToxicToxicologicoUfCRM.SP;
                //            break;
                //    }

                //    xMedicoASO.ufCRM = xUF;



                //    xMedicoASO.nmMed = rDs.Tables[0].Rows[zCont]["NmMed"].ToString().Trim();
                //    xMedicoASO.nrCRM = rDs.Tables[0].Rows[zCont]["NrCRM"].ToString().Trim();

                //    xMedicoASO.codSeqExame = "AA0000001";
                //    //CNPJ da clínica ?
                //    xMedicoASO.cnpjLab = rDs.Tables[0].Rows[zCont]["CNPJLab"].ToString().Trim(); ;

                //    xMedicoASO.dtExame = System.Convert.ToDateTime(rDs.Tables[0].Rows[zCont]["Data_Exame"].ToString().Trim(), ptBr);

                //    xMedicoASO.indRecusa = "N";

                //    xMonit.toxicologico = xMedicoASO;


                //    x2221.evtToxic = xMonit;



                    ////criar xml
                    //if (rd_Todos.Checked == true)
                    //{
                    xArq = "I:\\temp\\" + txt_Arq.Text.Substring(0, txt_Arq.Text.IndexOf(".")) + "_2221_" + rDs.Tables[0].Rows[zCont]["CPF"].ToString().Trim() + "_" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + "_" + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString() + ".XML";
                    //}
                    //else
                    //{
                    //    xArq = txt_Arq.Text;
                    //}



                    //CRIAR XML
                    string xContent = "";

                    XmlSerializer serializer = new XmlSerializer(typeof(eSocial_2221));

                    var memoryStream = new MemoryStream();
                    var streamWriter = new StreamWriter(memoryStream, System.Text.Encoding.UTF8);

                    serializer.Serialize(streamWriter, x2221);

                    byte[] bytes = memoryStream.ToArray();
                    xContent = Encoding.UTF8.GetString(bytes);

                    using (FileStream fs = File.Create(xArq))
                    {
                        fs.Write(bytes, 0, bytes.Length);
                        fs.Close();
                    }



                    string text = File.ReadAllText(xArq);
                    text = text.Replace("xmlns:xsi=", "");
                    text = text.Replace("\"http://www.w3.org/2001/XMLSchema-instance\"", "");
                    text = text.Replace("xmlns:xsd=", "");
                    text = text.Replace("\"http://www.w3.org/2001/XMLSchema\"", "");
                    File.WriteAllText(xArq, text);

                    xContent = xContent.Replace("xmlns:xsi=", "");
                    xContent = xContent.Replace("\"http://www.w3.org/2001/XMLSchema-instance\"", "");
                    xContent = xContent.Replace("xmlns:xsd=", "");
                    xContent = xContent.Replace("\"http://www.w3.org/2001/XMLSchema\"", "");

                    //using (Stream stream = File.Open(xArq, FileMode.Create))
                    //{
                    //    XmlSerializer serializer = new XmlSerializer(typeof(eSocialEvtToxic));
                    //    serializer.Serialize(stream, xMonit);
                    //    stream.Flush();

                    //    byte[] bytes = new byte[stream.Length];
                    //    stream.Position = 0;
                    //    stream.Read(bytes, 0, (int)stream.Length);
                    //    xContent = Encoding.ASCII.GetString(bytes);

                    //    stream.Close();
                    //}

                    lst_Arqs.Items.Add(xArq);

                    //salvar XML no SQL server com xeSocial.Id e nome do arquivo criado e datahora 
                    Int64 zPis = 0;
                    if (rDs.Tables[0].Rows[zCont]["PIS"].ToString().Trim() != "")
                        zPis = System.Convert.ToInt64(System.Text.RegularExpressions.Regex.Match(rDs.Tables[0].Rows[zCont]["PIS"].ToString().Trim(), @"\d+").Value);


                    Ilitera.Data.eSocial zeSocial = new Ilitera.Data.eSocial();
                    //zeSocial.XML_Deposito(xArq, xeSocial.Id, zCont, xContent, rDs.Tables[0].Rows[zCont]["CPF"].ToString(), zPis, rDs.Tables[0].Rows[zCont]["CNPJ"].ToString(), dtp_Inicial.Text, dtp_Final.Text, System.Convert.ToInt32(rDs.Tables[0].Rows[0]["IdExameBase"].ToString()), xMonit.Id);






                }
                catch (Exception ex)
                {                    
                    MsgBox1.Show("Ilitera.Net", "Erro:" + ex.ToString(), null, new EO.Web.MsgBoxButton("OK"));
                    return;
                }



            }



            using (ZipFile zip = new ZipFile())
            {
                zip.AlternateEncodingUsage = ZipOption.AsNecessary;
                zip.AddDirectoryByName("Files");

                for (int rCont = 0; rCont < lst_Arqs.Items.Count; rCont++)
                {
                    zip.AddFile(lst_Arqs.Items[rCont].Text, "Files");
                }
                Response.Clear();
                Response.BufferOutput = false;
                string zipName = String.Format("eSocial2221_Zip_{0}.zip", DateTime.Now.ToString("yyyy-MMM-dd-HHmmss"));
                Response.ContentType = "application/zip";
                Response.AddHeader("content-disposition", "attachment; filename=" + zipName);
                zip.Save(Response.OutputStream);
                Response.End();
            }



            MsgBox1.Show("Ilitera.Net", "Arquivo(s) criados.", null, new EO.Web.MsgBoxButton("OK"));            
            return;


        }


        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]
        private void Processar_S1065()
        {

            //use sied_Novo

            //select distinct * from
            //(
            //	select nomecodigo as 'CNPJ', d.IdEpi as CodEP, '2019-01' as iniValid, '1' as tpEp, d.Descricao as dscEP, f.NumeroCA
            //	from opsa.dbo.pessoa as a
            //	left join (
            //	  select * from tbllaudo_tec 
            //	  where convert( char(12),nid_empr ) +  convert( char(10),hdt_laudo , 103 ) in
            //	  (
            //		select convert( char(12),nid_empr ) +  convert( char(10),max( hdt_laudo ) , 103 )
            //		from tbllaudo_tec as ar
            //		left join opsa.dbo.pedido as br on ( ar.nid_pedido = br.IdPedido )
            //		where br.indstatus = 2 and nid_empr= 1373057218
            //		group by nid_empr 
            //	  )
            //	) as tx70 on ( a.idpessoa = tx70.nid_empr )
            //	left join tblfunc as b on ( tx70.nID_LAUD_TEC = b.nID_LAUD_TEC )
            //	left join tblFunc_EPI_Exte as c on ( b.nID_FUNC = c.nID_FUNC )
            //	left join opsa.dbo.epi as d on ( c.nid_epi = d.idepi )
            //	left join opsa.dbo.epiclienteca as e on ( a.idpessoa = e.idcliente and d.idepi = e.idepi )
            //	left join opsa.dbo.ca as f on ( e.idca = f.idca ) 
            //	where idpessoa = 1373057218

            //	union all


            //	select nomecodigo as 'CNPJ', d.IdEpi as CodEP, '2019-01' as iniValid, '1' as tpEp, d.Descricao as dscEP, f.NumeroCA
            //	from opsa.dbo.pessoa as a
            //	left join (
            //	  select * from tbllaudo_tec 
            //	  where convert( char(12),nid_empr ) +  convert( char(10),hdt_laudo , 103 ) in
            //	  (
            //		select convert( char(12),nid_empr ) +  convert( char(10),max( hdt_laudo ) , 103 )
            //		from tbllaudo_tec as ar
            //		left join opsa.dbo.pedido as br on ( ar.nid_pedido = br.IdPedido )
            //		where br.indstatus = 2 and nid_empr= 1373057218
            //		group by nid_empr 
            //	  )
            //	) as tx70 on ( a.idpessoa = tx70.nid_empr )
            //	left join tblppra1 as b on ( tx70.nID_LAUD_TEC = b.nID_LAUD_TEC )
            //	left join tblEpI_Exte as c on ( b.nID_PPRA = c.nID_PPRA  )
            //	left join opsa.dbo.epi as d on ( c.nid_epi = d.idepi )
            //	left join opsa.dbo.epiclienteca as e on ( a.idpessoa = e.idcliente and d.idepi = e.idepi )
            //	left join opsa.dbo.ca as f on ( e.idca = f.idca ) 
            //	where idpessoa = 1373057218

            //) as tx90
            //where CodEP is not null and NumeroCA is not null




        }


        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]
        private void Processar_S2220()
        {

            Int32 zEmpresa = 0;
            Int32 zColaborador = 0;
            Int32 zEmpresaGrupo = 0;

            int zContador = 0;

            string xArq = "";

            System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");


            if (rd_Todos.Checked == true)
            {
                zColaborador = 0;
                zEmpresaGrupo = 0;
                zEmpresa = System.Convert.ToInt32( Session["Empresa"].ToString() );
            }
            else if (rd_Colaborador.Checked == true)
            {
                if (cmb_Colaborador.SelectedIndex < 0)
                {                    
                    MsgBox1.Show("Ilitera.Net", "Selecione colaborador.", null, new EO.Web.MsgBoxButton("OK"));
                    return;
                }

                zEmpresa = 0;
                zEmpresaGrupo = 0;
                zColaborador = System.Convert.ToInt32(lst_Id.Items[cmb_Colaborador.SelectedIndex].ToString());
            }
            else if (rd_Grupo.Checked == true)
            {
                zEmpresa = 0;
                zColaborador = 0;
                zEmpresaGrupo = System.Convert.ToInt32( Session["Empresa"].ToString() );
            }


            Ilitera.Data.eSocial zEsocial = new Ilitera.Data.eSocial();

            DataSet rDs = zEsocial.Trazer_2220(zEmpresa, zColaborador, zEmpresaGrupo, dtp_Inicial.Text, dtp_Final.Text);

            if (rDs.Tables[0].Rows.Count < 1)
            {                
                MsgBox1.Show("Ilitera.Net", "Não há registros para gerar o evento selecionado.", null, new EO.Web.MsgBoxButton("OK"));
                return;
            }


            //criar evento de controle para este lote
            tbleSocial xeSocial = new tbleSocial();
            xeSocial.DataHora_Criacao = System.DateTime.Now;
            xeSocial.Evento = "2220";
            Entities.Usuario user = (Entities.Usuario)Session["usuarioLogado"];
            xeSocial.IdUsuario = user.IdUsuario;
            xeSocial.Save();



            for (int zCont = 0; zCont < rDs.Tables[0].Rows.Count; zCont++)
            {
                zContador = zContador + 1;

                eSocial_2220 x2220 = new eSocial_2220();

                eSocialEvtMonit xMonit = new eSocialEvtMonit();


                eSocialEvtMonitExMedOcupAso xMonitASO = new eSocialEvtMonitExMedOcupAso();

                eSocialEvtMonitExMedOcupRespMonit xRespMonitor = new eSocialEvtMonitExMedOcupRespMonit();



                //eSocialEvtMonitMonitExMedOcupAsoMedico xMedico = new eSocialEvtMonitMonitExMedOcupAsoMedico();



                //eSocialEvtMonitAsoExameRespMonitUfConsClasse xUFCons = new eSocialEvtMonitAsoExameRespMonitUfConsClasse();
                //eSocialEvtMonitAsoIdeServSaude xServSaude = new eSocialEvtMonitAsoIdeServSaude();
                //TMedico xMedico = new TMedico();
                //TCrm xCRM = new TCrm();
                //TCrmUfCRM xUF = new TCrmUfCRM();


                //TIdeEveTrab xIdeEvento = new TIdeEveTrab();
                T_ideEvento_trab xIdeEvento = new T_ideEvento_trab();

                //TEmpregador xIdeEmpregador = new TEmpregador();
                T_ideEmpregador xIdeEmpregador = new T_ideEmpregador();


                //TIdeVinculoEstag xIdeVinculo = new TIdeVinculoEstag();
                T_ideVinculo_sst xIdeVinculo = new T_ideVinculo_sst();


                try
                {

                    //xMonit.Id = Gerar_ID(rDs.Tables[0].Rows[zCont]["CNPJ"].ToString().Trim().Substring(0, 8), zCont + zContador + 1);
                    xMonit.Id = Gerar_ID(rDs.Tables[0].Rows[zCont]["CNPJ"].ToString().Trim().Substring(0, 14), zCont + zContador + 1);



                    //Empregador
                    xIdeEmpregador.tpInsc = 1;
                    //xIdeEmpregador.nrInsc = rDs.Tables[0].Rows[0]["CNPJ"].ToString().Substring(0, 8);  //CNPJ
                    string xCNPJ = rDs.Tables[0].Rows[zCont]["CNPJ"].ToString().Trim();

                    if (xCNPJ.Length == 14)
                        xIdeEmpregador.nrInsc = xCNPJ;
                    else
                        xIdeEmpregador.nrInsc = new string('0', 14 - xCNPJ.Length) + xCNPJ;


                    xMonit.ideEmpregador = xIdeEmpregador;


                    ////////////////////////////////////////////////
                    xIdeEvento.indRetif = 1;
                    //IdeEvento
                    ////IdeEvento
                    //xIdeEvento.indRetif = (sbyte)zRetif;

                    //if (zRetif == 2)
                    //{
                    //    xIdeEvento.nrRecibo = zProtocolo;
                    //}


                    xIdeEvento.procEmi = 1;
                    xIdeEvento.tpAmb = 2;
                    xIdeEvento.verProc = "1";

                    xMonit.ideEvento = xIdeEvento;




                    //IdeVinculo
                    xIdeVinculo.cpfTrab = rDs.Tables[0].Rows[zCont]["CPF"].ToString();

                    if (rDs.Tables[0].Rows[zCont]["Matricula"].ToString().Trim() != "")
                        xIdeVinculo.matricula = rDs.Tables[0].Rows[zCont]["Matricula"].ToString();
                    else
                        xIdeVinculo.codCateg = "101";
                    //  se não enviar matrícula, enviar CodCateg


                    //versão 02/2020 - excluir NISTRAB
                    //xIdeVinculo.nisTrab = rDs.Tables[0].Rows[zCont]["PIS"].ToString();



                    xMonit.ideVinculo = xIdeVinculo;



                    eSocialEvtMonitExMedOcup xTipoExame = new eSocialEvtMonitExMedOcup();
                    xTipoExame.tpExameOcup = System.Convert.ToSByte(rDs.Tables[0].Rows[zCont]["tpASO"].ToString());    // 0 admissional    1 periodico    2 retorno ao trabalho    3 mudança de funcao    4 monitoracao pontual    9 demissional


                    eSocialEvtMonitExMedOcupAso xASO = new eSocialEvtMonitExMedOcupAso();
                    xASO.dtAso = System.Convert.ToDateTime(rDs.Tables[0].Rows[zCont]["Data_Exame"].ToString(), ptBr);
                    xASO.resAso = System.Convert.ToSByte(rDs.Tables[0].Rows[zCont]["ResASO"].ToString());   //( 2 inapto )







                    Clinico zClinico = new Clinico(System.Convert.ToInt32(rDs.Tables[0].Rows[zCont]["IdClinico"].ToString()));

                    Ghe zGHE = new Ghe();

                    zGHE.Find(System.Convert.ToInt32(rDs.Tables[0].Rows[zCont]["nId_Func"].ToString()));


                    string sExamesOcupacionais = zClinico.GetPlanejamentoExamesAso_Formatado(zGHE, true, true, "", false);

                    //1. Acuidade Visual                         /  /       \n2. Audiometria                           31/05/2016   \n3. Eletrocardiograma (ECG)                 /  /       \n4. Eletroencefalograma (EEG)               /  /       \n5. Glicemia Jejum                          /  /       \n
                    //1. Ácido Mandélico                         /  /          2. Acuidade Visual                         /  /       \n3. Arsênico                                /  /          4. Audiometria                             /  /       \n5. Dosagem de Ácido Hipúrico               /  /          6. Eletrocardiograma (ECG)                 /  /       \n7. Eletroencefalograma (EEG)               /  /          8. Fator Reumatoide ( Latex )              /  /       \n9. Flora Normal                            /  /          10. Glicemia Jejum                          /  /      \n11. Hepatite – HbsAG                        /  /         12. Metanol                                 /  /      \n13. Metemoglobina sanguínea                 /  /         14. Sorologia para HIV                      /  /      \n\r\n\r\n





                    char delimiterChars;

                    if (sExamesOcupacionais.Length > 499)
                    {
                        delimiterChars = '.';
                    }
                    else
                    {
                        delimiterChars = '\n';
                    }


                    string[] rExames = sExamesOcupacionais.Split(delimiterChars);


                    int zExames = rExames.Length;

                    for (int zAux = 0; zAux < rExames.Length; zAux++)
                    {
                        if (rExames[zAux].Trim().Length < 40)
                        {
                            zExames = zExames - 1;
                        }
                    }



                    eSocialEvtMonitExMedOcupAsoExame[] xMonitASOExame = new eSocialEvtMonitExMedOcupAsoExame[zExames + 1];



                    for (int zAux = 0; zAux < zExames; zAux++)
                    {


                        if (rExames[zAux].Trim().Length > 40)
                        {

                            xMonitASOExame[zAux] = new eSocialEvtMonitExMedOcupAsoExame();

                            if (rExames[zAux].Substring(rExames[zAux].IndexOf("/") - 2, 10) == "  /  /    ")
                                xMonitASOExame[zAux].dtExm = System.Convert.ToDateTime(rDs.Tables[0].Rows[zCont]["Data_Exame"].ToString(), ptBr);
                            else
                                xMonitASOExame[zAux].dtExm = System.Convert.ToDateTime(rExames[zAux].Substring(rExames[zAux].IndexOf("/") - 2, 10), ptBr);

                            //preciso fazer o de-para da tabela 27 com ExameDicionario
                            // rExames[zAux] possui o exame de ExameDicionario
                            string tNomeExame = rExames[zAux].Substring(3, 36).Trim();

                            ExameDicionario tExame = new ExameDicionario();
                            tExame.Find(" Nome = '" + tNomeExame + "'");

                            if (tExame.Id != 0)
                            {
                                if (tExame.Codigo_eSocial != "")
                                {
                                    xMonitASOExame[zAux].procRealizado = tExame.Codigo_eSocial.Trim();
                                }
                                else
                                {
                                    xMonitASOExame[zAux].procRealizado = "9999";
                                }
                            }
                            else
                            {
                                xMonitASOExame[zAux].procRealizado = "9999";
                            }





                            xMonitASOExame[zAux].indResult = 1;   //1 normal  2 alterado  3 estaval  4 agravamento
                            xMonitASOExame[zAux].indResultSpecified = true;



                            //if (rExames[zAux].Substring(0, 40).Trim().IndexOf(".") > 0 && rExames[zAux].Substring(0, 40).Trim().IndexOf(".") < 4)
                            //{
                            //    xMonitASOExame[zAux].obsProc = rExames[zAux].Substring(rExames[zAux].Substring(0, 40).Trim().IndexOf(".") + 1, 40 - rExames[zAux].Substring(0, 40).Trim().IndexOf(".")).Trim();
                            //}
                            //else
                            //{
                            //    xMonitASOExame[zAux].obsProc = rExames[zAux].Substring(0, 39).Trim();
                            //}

                            //xMonitASOExame[zAux].obsProc = "";

                            xMonitASOExame[zAux].ordExame = 1;  // 1 referencial  2 sequencial


                        }

                    }




                    //avaliação clínica
                    xMonitASOExame[zExames] = new eSocialEvtMonitExMedOcupAsoExame();
                    xMonitASOExame[zExames].dtExm = System.Convert.ToDateTime(rDs.Tables[0].Rows[zCont]["Data_Exame"].ToString(), ptBr);
                    xMonitASOExame[zExames].procRealizado = "0295";
                    xMonitASOExame[zExames].indResult = 1;
                    xMonitASOExame[zExames].ordExame = 1;
                    xMonitASOExame[zExames].indResultSpecified = true;


                    xASO.exame = xMonitASOExame;

                    xTipoExame.aso = xASO;









                    xRespMonitor.nmResp = rDs.Tables[0].Rows[zCont]["RespNome"].ToString().ToUpper().Trim();
                    xRespMonitor.nrCRM = System.Text.RegularExpressions.Regex.Match(rDs.Tables[0].Rows[zCont]["NrConsClasse"].ToString().ToUpper().Trim(), @"\d+").Value;

                    //completar campos abaixo do médico coordenador do PCMSO

                    if (rDs.Tables[0].Rows[zCont]["RespCPF"].ToString().ToUpper().Trim() != "0" && rDs.Tables[0].Rows[zCont]["RespCPF"].ToString().ToUpper().Trim() != "")
                        xRespMonitor.cpfResp = rDs.Tables[0].Rows[zCont]["RespCPF"].ToString().ToUpper().Trim();

                    //criar campo CPF para responsável monitoração biológica

                    //eSocialEvtMonitExMedOcupRespMonitUfCRM xUF = new eSocialEvtMonitExMedOcupRespMonitUfCRM();
                    //xUF = eSocialEvtMonitExMedOcupRespMonitUfCRM.SP;
                    xRespMonitor.ufCRM = TS_uf.SP;

                    switch (rDs.Tables[0].Rows[zCont]["RespUF"].ToString().ToUpper().Trim())
                    {

                        case "SP":
                            xRespMonitor.ufCRM = TS_uf.SP;
                            break;
                        case "RJ":
                            xRespMonitor.ufCRM = TS_uf.RJ;
                            break;
                        case "AC":
                            xRespMonitor.ufCRM = TS_uf.AC;
                            break;
                        case "AL":
                            xRespMonitor.ufCRM = TS_uf.AL;
                            break;
                        case "AM":
                            xRespMonitor.ufCRM = TS_uf.AM;
                            break;
                        case "AP":
                            xRespMonitor.ufCRM = TS_uf.AP;
                            break;
                        case "BA":
                            xRespMonitor.ufCRM = TS_uf.BA;
                            break;
                        case "CE":
                            xRespMonitor.ufCRM = TS_uf.CE;
                            break;
                        case "DF":
                            xRespMonitor.ufCRM = TS_uf.DF;
                            break;
                        case "ES":
                            xRespMonitor.ufCRM = TS_uf.ES;
                            break;
                        case "GO":
                            xRespMonitor.ufCRM = TS_uf.GO;
                            break;
                        case "MA":
                            xRespMonitor.ufCRM = TS_uf.MA;
                            break;
                        case "MG":
                            xRespMonitor.ufCRM = TS_uf.MG;
                            break;
                        case "MS":
                            xRespMonitor.ufCRM = TS_uf.MS;
                            break;
                        case "MT":
                            xRespMonitor.ufCRM = TS_uf.MT;
                            break;
                        case "PA":
                            xRespMonitor.ufCRM = TS_uf.PA;
                            break;
                        case "PB":
                            xRespMonitor.ufCRM = TS_uf.PB;
                            break;
                        case "PE":
                            xRespMonitor.ufCRM = TS_uf.PE;
                            break;
                        case "PI":
                            xRespMonitor.ufCRM = TS_uf.PI;
                            break;
                        case "PR":
                            xRespMonitor.ufCRM = TS_uf.PR;
                            break;
                        case "RN":
                            xRespMonitor.ufCRM = TS_uf.RN;
                            break;
                        case "RO":
                            xRespMonitor.ufCRM = TS_uf.RO;
                            break;
                        case "RR":
                            xRespMonitor.ufCRM = TS_uf.RR;
                            break;
                        case "RS":
                            xRespMonitor.ufCRM = TS_uf.RS;
                            break;
                        case "SC":
                            xRespMonitor.ufCRM = TS_uf.SC;
                            break;
                        case "SE":
                            xRespMonitor.ufCRM = TS_uf.SE;
                            break;
                        case "TO":
                            xRespMonitor.ufCRM = TS_uf.TO;
                            break;
                        default:
                            xRespMonitor.ufCRM = TS_uf.SP;
                            break;
                    }

                    //xRespMonitor.ufCRM = xUF;




                    xTipoExame.respMonit = xRespMonitor;



                    xMonit.exMedOcup = xTipoExame;



                    //Médico ASO

                    eSocialEvtMonitExMedOcupAsoMedico xMedicoASO = new eSocialEvtMonitExMedOcupAsoMedico();

                    //eSocialEvtMonitExMedOcupAsoMedicoUfCRM xUF2 = new eSocialEvtMonitExMedOcupAsoMedicoUfCRM();
                    //xUF2 = eSocialEvtMonitExMedOcupAsoMedicoUfCRM.SP;
                    xMedicoASO.ufCRM = TS_uf.SP;

                    switch (rDs.Tables[0].Rows[zCont]["UFMed"].ToString().ToUpper().Trim())
                    {

                        case "SP":
                            xMedicoASO.ufCRM = TS_uf.SP;
                            break;
                        case "RJ":
                            xMedicoASO.ufCRM = TS_uf.RJ;
                            break;
                        case "AC":
                            xMedicoASO.ufCRM = TS_uf.AC;
                            break;
                        case "AL":
                            xMedicoASO.ufCRM = TS_uf.AL;
                            break;
                        case "AM":
                            xMedicoASO.ufCRM = TS_uf.AM;
                            break;
                        case "AP":
                            xMedicoASO.ufCRM = TS_uf.AP;
                            break;
                        case "BA":
                            xMedicoASO.ufCRM = TS_uf.BA;
                            break;
                        case "CE":
                            xMedicoASO.ufCRM = TS_uf.CE;
                            break;
                        case "DF":
                            xMedicoASO.ufCRM = TS_uf.DF;
                            break;
                        case "ES":
                            xMedicoASO.ufCRM = TS_uf.ES;
                            break;
                        case "GO":
                            xMedicoASO.ufCRM = TS_uf.GO;
                            break;
                        case "MA":
                            xMedicoASO.ufCRM = TS_uf.MA;
                            break;
                        case "MG":
                            xMedicoASO.ufCRM = TS_uf.MG;
                            break;
                        case "MS":
                            xMedicoASO.ufCRM = TS_uf.MS;
                            break;
                        case "MT":
                            xMedicoASO.ufCRM = TS_uf.MT;
                            break;
                        case "PA":
                            xMedicoASO.ufCRM = TS_uf.PA;
                            break;
                        case "PB":
                            xMedicoASO.ufCRM = TS_uf.PB;
                            break;
                        case "PE":
                            xMedicoASO.ufCRM = TS_uf.PE;
                            break;
                        case "PI":
                            xMedicoASO.ufCRM = TS_uf.PI;
                            break;
                        case "PR":
                            xMedicoASO.ufCRM = TS_uf.PR;
                            break;
                        case "RN":
                            xMedicoASO.ufCRM = TS_uf.RN;
                            break;
                        case "RO":
                            xMedicoASO.ufCRM = TS_uf.RO;
                            break;
                        case "RR":
                            xMedicoASO.ufCRM = TS_uf.RR;
                            break;
                        case "RS":
                            xMedicoASO.ufCRM = TS_uf.RS;
                            break;
                        case "SC":
                            xMedicoASO.ufCRM = TS_uf.SC;
                            break;
                        case "SE":
                            xMedicoASO.ufCRM = TS_uf.SE;
                            break;
                        case "TO":
                            xMedicoASO.ufCRM = TS_uf.TO;
                            break;
                        default:
                            xMedicoASO.ufCRM = TS_uf.SP;
                            break;
                    }


                    //xMedicoASO.ufCRM = xUF2;




                    if (rDs.Tables[0].Rows[zCont]["NmMed"].ToString().Trim() != "")
                        xMedicoASO.nmMed = rDs.Tables[0].Rows[zCont]["NmMed"].ToString().Trim();
                    else
                        xMedicoASO.nmMed = rDs.Tables[0].Rows[zCont]["RespNome"].ToString().ToUpper().Trim();




                    if (rDs.Tables[0].Rows[zCont]["NrCRM"].ToString().Trim() != "")
                        xMedicoASO.nrCRM = System.Text.RegularExpressions.Regex.Match(rDs.Tables[0].Rows[zCont]["NrCRM"].ToString().ToUpper().Trim(), @"\d+").Value;
                    else
                        xMedicoASO.nrCRM = System.Text.RegularExpressions.Regex.Match(rDs.Tables[0].Rows[zCont]["NrConsClasse"].ToString().ToUpper().Trim(), @"\d+").Value;

                    //xMedicoASO.ufCRMSpecified = true;
                    //NIT do médico



                    //versão 02/2020 - excluir NISMED E CPFMED
                    //if (rDs.Tables[0].Rows[zCont]["NITMed"].ToString().ToUpper().Trim() != "")
                    //    xMedicoASO.nisMed = rDs.Tables[0].Rows[zCont]["NITMed"].ToString().ToUpper().Trim();

                    //trazer esse dado
                    //xMedicoASO.cpfMed


                    xASO.medico = xMedicoASO;

                    xTipoExame.aso = xASO;



                    xMonit.exMedOcup = xTipoExame;



                    x2220.evtMonit = xMonit;


                    ////criar xml
                    //if (rd_Todos.Checked == true)
                    //{
                    xArq = "I:\\temp\\" + txt_Arq.Text.Substring(0, txt_Arq.Text.IndexOf(".")) + "_2220_" + rDs.Tables[0].Rows[zCont]["CPF"].ToString().Trim() + "_" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + "_" + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString() + ".XML";
                    //}
                    //else
                    //{
                    //    xArq = txt_Arq.Text;
                    //}



                    //CRIAR XML
                    string xContent = "";

                    XmlSerializer serializer = new XmlSerializer(typeof(eSocial_2220));

                    var memoryStream = new MemoryStream();
                    var streamWriter = new StreamWriter(memoryStream, System.Text.Encoding.UTF8);

                    serializer.Serialize(streamWriter, x2220);

                    byte[] bytes = memoryStream.ToArray();
                    xContent = Encoding.UTF8.GetString(bytes);

                    using (FileStream fs = File.Create(xArq))
                    {
                        fs.Write(bytes, 0, bytes.Length);
                        fs.Close();
                    }



                    string text = File.ReadAllText(xArq);
                    text = text.Replace("xmlns:xsi=", "");
                    text = text.Replace("\"http://www.w3.org/2001/XMLSchema-instance\"", "");
                    text = text.Replace("xmlns:xsd=", "");
                    text = text.Replace("\"http://www.w3.org/2001/XMLSchema\"", "");
                    text = text.Replace("evtCAT", "evtMonit");
                    //text = text.Replace("v_S_01_02_00", "v_S_01_02_00:eSocial_2220");
                    text = text.Replace("v_S_01_02_00:eSocial_2220", "v_S_01_02_00");
                    text = text.Replace("eSocial_2220", "eSocial");
                    File.WriteAllText(xArq, text);

                    xContent = xContent.Replace("xmlns:xsi=", "");
                    xContent = xContent.Replace("\"http://www.w3.org/2001/XMLSchema-instance\"", "");
                    xContent = xContent.Replace("xmlns:xsd=", "");
                    xContent = xContent.Replace("\"http://www.w3.org/2001/XMLSchema\"", "");
                    xContent = xContent.Replace("evtCAT", "evtMonit");
                    //xContent = xContent.Replace("v_S_01_02_00", "v_S_01_02_00:eSocial_2220");
                    xContent = xContent.Replace("v_S_01_02_00:eSocial_2220", "v_S_01_02_00");
                    xContent = xContent.Replace("eSocial_2220", "eSocial");


                    //using (Stream stream = File.Open(xArq, FileMode.Create))
                    //{
                    //    XmlSerializer serializer = new XmlSerializer(typeof(eSocialEvtMonit));
                    //    serializer.Serialize(stream, xMonit);
                    //    stream.Flush();

                    //    byte[] bytes = new byte[stream.Length];
                    //    stream.Position = 0;
                    //    stream.Read(bytes, 0, (int)stream.Length);
                    //    xContent = Encoding.ASCII.GetString(bytes);

                    //    stream.Close();

                    //}


                    lst_Arqs.Items.Add(xArq);

                    //salvar XML no SQL server com xeSocial.Id e nome do arquivo criado e datahora 
                    Int64 zPis = 0;
                    if (rDs.Tables[0].Rows[zCont]["PIS"].ToString().Trim() != "")
                        zPis = System.Convert.ToInt64(System.Text.RegularExpressions.Regex.Match(rDs.Tables[0].Rows[zCont]["PIS"].ToString().Trim(), @"\d+").Value);


                    Ilitera.Data.eSocial zeSocial = new Ilitera.Data.eSocial();
                    zeSocial.XML_Deposito(xArq, xeSocial.Id, zCont, xContent, rDs.Tables[0].Rows[zCont]["CPF"].ToString(), zPis, rDs.Tables[0].Rows[zCont]["CNPJ"].ToString(), dtp_Inicial.Text, dtp_Final.Text, System.Convert.ToInt32(rDs.Tables[0].Rows[0]["IdExameBase"].ToString()), xMonit.Id,0);






                }
                catch (Exception ex)
                {                    
                    MsgBox1.Show("Ilitera.Net", "Erro:" + ex.ToString(), null, new EO.Web.MsgBoxButton("OK"));
                    return;
                }



            }



            using (ZipFile zip = new ZipFile())
            {
                zip.AlternateEncodingUsage = ZipOption.AsNecessary;
                zip.AddDirectoryByName("Files");

                for (int rCont = 0; rCont < lst_Arqs.Items.Count; rCont++)
                {
                    zip.AddFile(lst_Arqs.Items[rCont].Text, "Files");
                }
                Response.Clear();
                Response.BufferOutput = false;
                string zipName = String.Format("eSocial2220_Zip_{0}.zip", DateTime.Now.ToString("yyyy-MMM-dd-HHmmss"));
                Response.ContentType = "application/zip";
                Response.AddHeader("content-disposition", "attachment; filename=" + zipName);
                zip.Save(Response.OutputStream);
                Response.End();
            }


            MsgBox1.Show("Ilitera.Net", "Arquivo(s) criados.", null, new EO.Web.MsgBoxButton("OK"));
            return;

        }





        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]
        private void Processar_S2240()
        {

            //Int32 zEmpresa = 0;
            //Int32 zColaborador = 0;
            //Int32 zEmpresaGrupo = 0;

            //int zContador = 0;

            //Int32 znId_Laud_Tec = 0;
            //Int32 znID_PPRA = 0;
            //Int32 znId_Empr = 0;

            ////int zAux = 0;
            //int zEPI = 0;

            //int xRegs = 0;
            //string xCPF = "";
            //string xHDT_Laudo = "";


            //string xArq = "";





            //System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");


            //if (rd_Todos.Checked == true)
            //{
            //    zColaborador = 0;
            //    zEmpresaGrupo = 0;
            //    zEmpresa = System.Convert.ToInt32( Session["Empresa"].ToString() );
            //}
            //else if (rd_Colaborador.Checked == true)
            //{
            //    if (cmb_Colaborador.SelectedIndex < 0)
            //    {                    
            //        MsgBox1.Show("Ilitera.Net", "Selecione colaborador.", null, new EO.Web.MsgBoxButton("OK"));
            //        return;
            //    }

            //    zEmpresa = 0;
            //    zEmpresaGrupo = 0;
            //    zColaborador = System.Convert.ToInt32(lst_Id.Items[cmb_Colaborador.SelectedIndex].ToString());
            //}
            //else if (rd_Grupo.Checked == true)
            //{
            //    zEmpresa = 0;
            //    zColaborador = 0;
            //    zEmpresaGrupo = System.Convert.ToInt32( Session["Empresa"].ToString() );
            //}


            //Entities.Usuario user = (Entities.Usuario)Session["usuarioLogado"];

            //Ilitera.Data.eSocial zEsocial = new Ilitera.Data.eSocial();

            //DataSet rDs = zEsocial.Trazer_2240(zEmpresa, zColaborador, zEmpresaGrupo, dtp_Inicial.Text, dtp_Final.Text, user.IdUsuario,0);

            //if (rDs.Tables[0].Rows.Count < 1)
            //{                
            //    MsgBox1.Show("Ilitera.Net", "Não há registros para gerar o evento selecionado.", null, new EO.Web.MsgBoxButton("OK"));
            //    return;
            //}




            //if (rDs.Tables[0].Rows.Count > 0)
            //{
            //    xCPF = rDs.Tables[0].Rows[0]["CPF"].ToString().Trim();
            //    xHDT_Laudo = rDs.Tables[0].Rows[0]["DtIniCondicao"].ToString().Trim();
            //}


            

            ////criar evento de controle para este lote
            //tbleSocial xeSocial = new tbleSocial();
            //xeSocial.DataHora_Criacao = System.DateTime.Now;
            //xeSocial.Evento = "2240";            
            //xeSocial.IdUsuario = user.IdUsuario;
            //xeSocial.Save();



            //for (int zCont = 0; zCont < rDs.Tables[0].Rows.Count; zCont++)
            //{

            //    zContador = zContador + 1;

            //    eSocial_2240 x2240 = new eSocial_2240();
            //    eSocialEvtExpRisco xRisco = new eSocialEvtExpRisco();

            //    //eSocialEvtExpRiscoInfoExpRiscoAltExpRisco xAltExpRisco = new eSocialEvtExpRiscoInfoExpRiscoAltExpRisco();
            //    //eSocialEvtExpRiscoInfoExpRiscoFimExpRisco xFim = new eSocialEvtExpRiscoInfoExpRiscoFimExpRisco();
            //    //eSocialEvtExpRiscoInfoExpRiscoIniExpRisco xIni = new eSocialEvtExpRiscoInfoExpRiscoIniExpRisco();


            //    //TIdeEveTrab xIdeEvento = new TIdeEveTrab();
            //    T_ideEvento_trab xIdeEvento = new T_ideEvento_trab();

            //    //TEmpregador xIdeEmpregador = new TEmpregador();
            //    T_ideEmpregador xIdeEmpregador = new T_ideEmpregador();


            //    //TIdeVinculoEstag xIdeVinculo = new TIdeVinculoEstag();
            //    T_ideVinculo_sst xIdeVinculo = new T_ideVinculo_sst();



            //    try
            //    {

            //        //xRisco.Id = Gerar_ID(rDs.Tables[0].Rows[zCont]["CNPJ"].ToString().Trim().Substring(0, 8), zCont + zContador + 1);
            //        xRisco.Id = Gerar_ID(rDs.Tables[0].Rows[zCont]["CNPJ"].ToString().Trim().Substring(0, 14), zCont + zContador + 1);


            //        znId_Laud_Tec = System.Convert.ToInt32(rDs.Tables[0].Rows[zCont]["nId_Laud_Tec"].ToString());
            //        znID_PPRA = System.Convert.ToInt32(rDs.Tables[0].Rows[zCont]["nId_PPRA"].ToString());
            //        znId_Empr = System.Convert.ToInt32(rDs.Tables[0].Rows[zCont]["nId_Empr"].ToString());


            //        //Empregador
            //        xIdeEmpregador.tpInsc = 1;
            //        //xIdeEmpregador.nrInsc = rDs.Tables[0].Rows[0]["CNPJ"].ToString().Substring(0, 8);  //CNPJ
            //        string xCNPJ = rDs.Tables[0].Rows[zCont]["CNPJ"].ToString().Trim();

            //        if (xCNPJ.Length == 14)
            //            xIdeEmpregador.nrInsc = xCNPJ;
            //        else
            //            xIdeEmpregador.nrInsc = new string('0', 14 - xCNPJ.Length) + xCNPJ;


            //        xRisco.ideEmpregador = xIdeEmpregador;


            //        ////////////////////////////////////////////////


            //        //se zCod<>"",  devo tratar como retificação ?  Nesse caso preciso pegar o recibo do enviado anteriormente

            //        xIdeEvento.indRetif = 1;

            //        //IdeEvento
            //        //xIdeEvento.indRetif = (sbyte)zRetif;

            //        //if (zRetif == 2)
            //        //{
            //        //    xIdeEvento.nrRecibo = zProtocolo;
            //        //}


            //        xIdeEvento.procEmi = 1;
            //        xIdeEvento.tpAmb = 2;
            //        xIdeEvento.verProc = "1";

            //        xRisco.ideEvento = xIdeEvento;

            //        ////////////////////////////////////////////////


            //        //levantar quantos CPFs estão nesta relação,  se todos está selecionado



            //        //IdeVinculo
            //        xIdeVinculo.cpfTrab = rDs.Tables[0].Rows[zCont]["CPF"].ToString();

            //        //if (rDs.Tables[0].Rows[zCont]["Matricula"].ToString().Trim() != "")
            //        xIdeVinculo.matricula = rDs.Tables[0].Rows[zCont]["Matricula"].ToString();
            //        //else
            //        xIdeVinculo.codCateg = "101";
            //        //  se não enviar matrícula, enviar CodCateg

            //        //xIdeVinculo.nisTrab = rDs.Tables[0].Rows[zCont]["PIS"].ToString();


            //        xRisco.ideVinculo = xIdeVinculo;


            //        eSocialEvtExpRiscoInfoExpRisco xInfoRisco = new eSocialEvtExpRiscoInfoExpRisco();
            //        xInfoRisco.dtIniCondicao = System.Convert.ToDateTime(rDs.Tables[0].Rows[zCont]["DtIniCondicao"].ToString(), ptBr);



            //        eSocialEvtExpRiscoInfoExpRiscoInfoAmb xInfoAmb = new eSocialEvtExpRiscoInfoExpRiscoInfoAmb();
            //        xInfoAmb.localAmb = 1;

            //        //dscSetor ou nome do GHE ?
            //        if (rDs.Tables[0].Rows[zCont]["dscSetor"].ToString().Trim().Length > 100)
            //            xInfoAmb.dscSetor = rDs.Tables[0].Rows[zCont]["dscSetor"].ToString().Trim().Substring(0, 100);
            //        else
            //            xInfoAmb.dscSetor = rDs.Tables[0].Rows[zCont]["dscSetor"].ToString().Trim();


            //        xInfoAmb.tpInsc = 1;
            //        xInfoAmb.nrInsc = xCNPJ;  //rDs.Tables[0].Rows[0]["CNPJ"].ToString().Substring(0, 8);  //CNPJ


            //        xInfoRisco.infoAmb = xInfoAmb;


            //        //if (rDs.Tables[0].Rows[0]["dscAtivDes"].ToString().Trim() != "")
            //        //{
            //        eSocialEvtExpRiscoInfoExpRiscoInfoAtiv xInfoAtiv = new eSocialEvtExpRiscoInfoExpRiscoInfoAtiv();
            //        if (rDs.Tables[0].Rows[0]["dscAtivDes"].ToString().Trim() != "")
            //            xInfoAtiv.dscAtivDes = rDs.Tables[0].Rows[0]["dscAtivDes"].ToString().Substring(0, 998);
            //        else
            //            xInfoAtiv.dscAtivDes = "Descrição Atividade";

            //        xInfoRisco.infoAtiv = xInfoAtiv;
            //        // }





            //        //eSocialEvtExpRiscoInfoExpRiscoFatRisco[] xFatRisco = new eSocialEvtExpRiscoInfoExpRiscoFatRisco[100];
            //        eSocialEvtExpRiscoInfoExpRiscoAgNoc[] xFatRisco = new eSocialEvtExpRiscoInfoExpRiscoAgNoc[100];

            //        xRegs = 0;





            //        eSocialEvtExpRiscoInfoExpRiscoRespReg[] xRespReg = new eSocialEvtExpRiscoInfoExpRiscoRespReg[1];

            //        xRespReg[0] = new eSocialEvtExpRiscoInfoExpRiscoRespReg();


            //        //xRespReg[0].nisResp = rDs.Tables[0].Rows[zCont]["NISResp"].ToString();

            //        if (rDs.Tables[0].Rows[zCont]["CPFResp"].ToString().Trim() != "")
            //            xRespReg[0].cpfResp = rDs.Tables[0].Rows[zCont]["CPFResp"].ToString();
            //        else
            //            xRespReg[0].cpfResp = "00000000000";

            //        //xRespReg[0].nmResp = rDs.Tables[0].Rows[zCont]["NmResp"].ToString();

            //        xRespReg[0].ideOC = 4;
            //        if (rDs.Tables[0].Rows[zCont]["NrOC"].ToString().Trim() != "")
            //            xRespReg[0].nrOC = rDs.Tables[0].Rows[zCont]["NrOC"].ToString();
            //        else
            //            xRespReg[0].nrOC = "0000";


            //        switch (rDs.Tables[0].Rows[zCont]["UFOC"].ToString().ToUpper().Trim())
            //        {

            //            case "SP":
            //                xRespReg[0].ufOC = TS_uf.SP;
            //                break;
            //            case "RJ":
            //                xRespReg[0].ufOC = TS_uf.RJ;
            //                break;
            //            case "AC":
            //                xRespReg[0].ufOC = TS_uf.AC;
            //                break;
            //            case "AL":
            //                xRespReg[0].ufOC = TS_uf.AL;
            //                break;
            //            case "AM":
            //                xRespReg[0].ufOC = TS_uf.AM;
            //                break;
            //            case "AP":
            //                xRespReg[0].ufOC = TS_uf.AP;
            //                break;
            //            case "BA":
            //                xRespReg[0].ufOC = TS_uf.BA;
            //                break;
            //            case "CE":
            //                xRespReg[0].ufOC = TS_uf.CE;
            //                break;
            //            case "DF":
            //                xRespReg[0].ufOC = TS_uf.DF;
            //                break;
            //            case "ES":
            //                xRespReg[0].ufOC = TS_uf.ES;
            //                break;
            //            case "GO":
            //                xRespReg[0].ufOC = TS_uf.GO;
            //                break;
            //            case "MA":
            //                xRespReg[0].ufOC = TS_uf.MA;
            //                break;
            //            case "MG":
            //                xRespReg[0].ufOC = TS_uf.MG;
            //                break;
            //            case "MS":
            //                xRespReg[0].ufOC = TS_uf.MS;
            //                break;
            //            case "MT":
            //                xRespReg[0].ufOC = TS_uf.MT;
            //                break;
            //            case "PA":
            //                xRespReg[0].ufOC = TS_uf.PA;
            //                break;
            //            case "PB":
            //                xRespReg[0].ufOC = TS_uf.PB;
            //                break;
            //            case "PE":
            //                xRespReg[0].ufOC = TS_uf.PE;
            //                break;
            //            case "PI":
            //                xRespReg[0].ufOC = TS_uf.PI;
            //                break;
            //            case "PR":
            //                xRespReg[0].ufOC = TS_uf.PR;
            //                break;
            //            case "RN":
            //                xRespReg[0].ufOC = TS_uf.RN;
            //                break;
            //            case "RO":
            //                xRespReg[0].ufOC = TS_uf.RO;
            //                break;
            //            case "RR":
            //                xRespReg[0].ufOC = TS_uf.RR;
            //                break;
            //            case "RS":
            //                xRespReg[0].ufOC = TS_uf.RS;
            //                break;
            //            case "SC":
            //                xRespReg[0].ufOC = TS_uf.SC;
            //                break;
            //            case "SE":
            //                xRespReg[0].ufOC = TS_uf.SE;
            //                break;
            //            case "TO":
            //                xRespReg[0].ufOC = TS_uf.TO;
            //                break;
            //            default:
            //                xRespReg[0].ufOC = TS_uf.SP;
            //                break;
            //        }



            //        xInfoRisco.respReg = xRespReg;


            //        eSocialEvtExpRiscoInfoExpRiscoObs xObs = new eSocialEvtExpRiscoInfoExpRiscoObs();
            //        xObs.obsCompl = "Obs";

            //        xInfoRisco.obs = xObs;



            //        //  montar infomarções do GHE do PPRA - enquanto for mesmo colaborador e mesmo laudo
            //        while (zCont < rDs.Tables[0].Rows.Count && xCPF == rDs.Tables[0].Rows[zCont]["CPF"].ToString().Trim() && xHDT_Laudo == rDs.Tables[0].Rows[zCont]["DtIniCondicao"].ToString().Trim())
            //        {

            //            //eSocialEvtExpRiscoInfoExpRiscoInfoAtivAtivPericInsal[] xPericInsal = new eSocialEvtExpRiscoInfoExpRiscoInfoAtivAtivPericInsal[1];
            //            //tabela 28 atividades perigosas e insalubres
            //            //xPericInsal[0] = new eSocialEvtExpRiscoInfoExpRiscoInfoAtivAtivPericInsal();
            //            //xPericInsal[0].codAtiv = "99.999";

            //            //xInfoAtiv.ativPericInsal = xPericInsal;



            //            xFatRisco[xRegs] = new eSocialEvtExpRiscoInfoExpRiscoAgNoc();

            //            if (rDs.Tables[0].Rows[zCont]["CodFatRis"].ToString().Trim() == "")
            //                xFatRisco[xRegs].codAgNoc = "09.01.001";
            //            else
            //                xFatRisco[xRegs].codAgNoc = rDs.Tables[0].Rows[zCont]["CodFatRis"].ToString();  //pegar código do e-Social


            //            xFatRisco[xRegs].tpAval = System.Convert.ToSByte(rDs.Tables[0].Rows[zCont]["tpAval"].ToString());

            //            //testar conversão
            //            if (rDs.Tables[0].Rows[zCont]["tpAval"].ToString().Trim() == "1" && rDs.Tables[0].Rows[zCont]["intConc"].ToString().Trim() != "")
            //            {
            //                xFatRisco[xRegs].intConc = System.Convert.ToDecimal(rDs.Tables[0].Rows[zCont]["intConc"].ToString());
            //                xFatRisco[xRegs].intConcSpecified = true;

            //                xFatRisco[xRegs].limTol = System.Convert.ToDecimal(rDs.Tables[0].Rows[zCont]["LimTol"].ToString());
            //                xFatRisco[xRegs].limTolSpecified = true;

            //                if (System.Convert.ToSByte(rDs.Tables[0].Rows[zCont]["Unidade_eSocial"].ToString()) > 0)
            //                {
            //                    xFatRisco[xRegs].unMed = System.Convert.ToSByte(rDs.Tables[0].Rows[zCont]["Unidade_eSocial"].ToString());
            //                    xFatRisco[xRegs].unMedSpecified = true;
            //                }
            //                else
            //                {
            //                    xFatRisco[xRegs].unMedSpecified = false;
            //                }

            //            }
            //            else
            //            {
            //                xFatRisco[xRegs].intConcSpecified = false;
            //            }


            //            if (rDs.Tables[0].Rows[zCont]["tpAval"].ToString().Trim() == "1")
            //            {
            //                if (rDs.Tables[0].Rows[zCont]["tecMedicao"].ToString().Trim() != "")
            //                    xFatRisco[xRegs].tecMedicao = rDs.Tables[0].Rows[zCont]["tecMedicao"].ToString();
            //                else
            //                    xFatRisco[xRegs].tecMedicao = "Quantitativo";
            //            }



            //            //eSocialEvtExpRiscoInfoExpRiscoFatRiscoEpcEpi xEpcEpi = new eSocialEvtExpRiscoInfoExpRiscoFatRiscoEpcEpi();

            //            eSocialEvtExpRiscoInfoExpRiscoAgNocEpcEpi xEpcEpi = new eSocialEvtExpRiscoInfoExpRiscoAgNocEpcEpi();


            //            //TinfoAmbFatRiscoEpcEpiEpc[] xEpc = new TinfoAmbFatRiscoEpcEpiEpc[1];


            //            ////EPC
            //            xEpcEpi.utilizEPC = 0;

            //            if (rDs.Tables[0].Rows[zCont]["EPC"].ToString().Trim() != "")
            //            {
            //                xEpcEpi.utilizEPC = 2;
            //                xEpcEpi.eficEpc = TS_sim_nao.S;

            //                //    xEpc[0] = new TinfoAmbFatRiscoEpcEpiEpc();
            //                //    xEpc[0].dscEpc = rDs.Tables[0].Rows[zCont]["EPC"].ToString().Trim();
            //                //    xEpc[0].eficEpc = "S";
            //                //    xEpcEpi.epc = xEpc;

            //                //    xEpc = null;                            
            //            }



            //            //EPI
            //            xEpcEpi.utilizEPI = 0;

            //            ArrayList list = new EpiExistente().Find(" nId_PPRA = " + rDs.Tables[0].Rows[zCont]["nId_PPRA"].ToString());


            //            //eSocialEvtExpRiscoInfoExpRiscoFatRiscoEpcEpiEpi[] xEpi = new eSocialEvtExpRiscoInfoExpRiscoFatRiscoEpcEpiEpi[100];
            //            eSocialEvtExpRiscoInfoExpRiscoAgNocEpcEpiEpi[] xEpi = new eSocialEvtExpRiscoInfoExpRiscoAgNocEpcEpiEpi[50];

            //            zEPI = 0;

            //            foreach (EpiExistente rEpi in list)
            //            {

            //                if (zEPI >= 50) break;   //limite de 50 ocorrências no layout


            //                xEpcEpi.utilizEPI = 2;

            //                eSocialEvtExpRiscoInfoExpRiscoAgNocEpcEpiEpiCompl xEpiComp = new eSocialEvtExpRiscoInfoExpRiscoAgNocEpcEpiEpiCompl();
            //                xEpiComp.condFuncto = TS_sim_nao.S;
            //                xEpiComp.medProtecao = TS_sim_nao.S;
            //                xEpiComp.usoInint = TS_sim_nao.S;
            //                xEpiComp.higienizacao = TS_sim_nao.S;
            //                xEpiComp.periodicTroca = TS_sim_nao.S;
            //                xEpiComp.przValid = TS_sim_nao.S;

            //                xEpcEpi.epiCompl = xEpiComp;

            //                rEpi.nID_EPI.Find();
            //                DataSet hDs = new CA().GetCAAssociadoEPI(znId_Empr, rEpi.nID_EPI.Id);


            //                if (hDs.Tables[0].Rows.Count == 0)
            //                {
            //                    xEpi[zEPI] = new eSocialEvtExpRiscoInfoExpRiscoAgNocEpcEpiEpi();

            //                    //nome do epi
            //                    xEpi[zEPI].dscEPI = ConvertWesternEuropeanToASCII(rEpi.nID_EPI.Descricao);
            //                    xEpi[zEPI].eficEpi = TS_sim_nao.S;
            //                    //xEpi[zEPI].docAval = "";

            //                    zEPI = zEPI + 1;

            //                }
            //                else
            //                {

            //                    for (int zCA = 0; zCA < hDs.Tables[0].Rows.Count; zCA++)
            //                    {
            //                        xEpi[zEPI] = new eSocialEvtExpRiscoInfoExpRiscoAgNocEpcEpiEpi();

            //                        //nome do epi
            //                        xEpi[zEPI].dscEPI = ConvertWesternEuropeanToASCII(rEpi.nID_EPI.Descricao);

            //                        xEpi[zEPI].docAval = hDs.Tables[0].Rows[zCA]["NumeroCA"].ToString();
            //                        xEpi[zEPI].eficEpi = TS_sim_nao.S;

            //                        zEPI = zEPI + 1;


            //                    }
            //                }
            //            }


            //            System.Array.Resize(ref xEpi, zEPI);

            //            xEpcEpi.epi = xEpi;

            //            xEpi = null;


            //            //ANALISAR
            //            xFatRisco[xRegs].epcEpi = xEpcEpi;

            //            xEpcEpi = null;



            //            xRegs = xRegs + 1;
            //            zCont = zCont + 1;

            //        }




            //        if (zCont < rDs.Tables[0].Rows.Count)
            //        {
            //            xCPF = rDs.Tables[0].Rows[zCont]["CPF"].ToString().Trim();
            //            xHDT_Laudo = rDs.Tables[0].Rows[zCont]["DtIniCondicao"].ToString().Trim();
            //        }

            //        zCont = zCont - 1;


            //        //ver se há códigos ergonômicos
            //        ArrayList list2 = new Ghe_eSocial().Find(" nId_Func = " + rDs.Tables[0].Rows[zCont]["CodAmb"].ToString());


            //        foreach (Ghe_eSocial zGHE in list2)
            //        {

            //            xFatRisco[xRegs] = new eSocialEvtExpRiscoInfoExpRiscoAgNoc();
            //            xFatRisco[xRegs].codAgNoc = zGHE.Codigo;
            //            xFatRisco[xRegs].tpAval = 2;
            //            //xFatRisco[xRegs].insalubridade = "N";
            //            //xFatRisco[xRegs].periculosidade = "N";
            //            //xFatRisco[xRegs].aposentEsp = "N";

            //            xRegs++;
            //        }





            //        System.Array.Resize(ref xFatRisco, xRegs);

            //        xInfoRisco.agNoc = xFatRisco;











            //        //xInfoRisco.iniExpRisco = xIni;


            //        //somente se houver alteração do risco do colaborador
            //        ////////////////////////////////////////////////
            //        //xAltExpRisco.dtAltCondicao = DateTime.Now;
            //        //xAltExpRisco.infoAmb = xInfoAmb;
            //        //xInfoRisco.altExpRisco = xAltExpRisco;






            //        //data fim condição - validade do PPRA ?
            //        //xFim.dtFimCondicao = System.Convert.ToDateTime(rDs.Tables[0].Rows[zCont]["DtFimCondicao"].ToString(), ptBr);


            //        //eSocialEvtExpRiscoInfoExpRiscoFimExpRiscoInfoAmb[] xFimInfoAmb = new eSocialEvtExpRiscoInfoExpRiscoFimExpRiscoInfoAmb[1];

            //        //xFimInfoAmb[0] = new eSocialEvtExpRiscoInfoExpRiscoFimExpRiscoInfoAmb();
            //        //xFimInfoAmb[0].codAmb = rDs.Tables[0].Rows[zCont]["CodAmb"].ToString();

            //        //xFim.infoAmb = xFimInfoAmb;

            //        //xInfoRisco.fimExpRisco = xFim;






            //        xRisco.infoExpRisco = xInfoRisco;


            //        x2240.evtExpRisco = xRisco;







            //        ////criar xml
            //        //if (rd_Todos.Checked == true)
            //        //{
            //        xArq = "I:\\temp\\" + txt_Arq.Text.Substring(0, txt_Arq.Text.IndexOf(".")) + "_2240_" + rDs.Tables[0].Rows[zCont]["CPF"].ToString().Trim() + "_" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + "_" + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString() + ".XML";
            //        //}
            //        //else
            //        //{
            //        //    xArq = txt_Arq.Text;
            //        //}



            //        //CRIAR XML
            //        string xContent = "";

            //        XmlSerializer serializer = new XmlSerializer(typeof(eSocial_2240));

            //        var memoryStream = new MemoryStream();
            //        var streamWriter = new StreamWriter(memoryStream, System.Text.Encoding.UTF8);

            //        serializer.Serialize(streamWriter, x2240);

            //        byte[] bytes = memoryStream.ToArray();
            //        xContent = Encoding.UTF8.GetString(bytes);

            //        using (FileStream fs = File.Create(xArq))
            //        {
            //            fs.Write(bytes, 0, bytes.Length);
            //            fs.Close();
            //        }



            //        string text = File.ReadAllText(xArq);
            //        text = text.Replace("xmlns:xsi=", "");
            //        text = text.Replace("\"http://www.w3.org/2001/XMLSchema-instance\"", "");
            //        text = text.Replace("xmlns:xsd=", "");
            //        text = text.Replace("\"http://www.w3.org/2001/XMLSchema\"", "");
            //        text = text.Replace("evtCAT", "evtExpRisco");
            //        //text = text.Replace("v_S_01_02_00", "v_S_01_02_00:eSocial_2240");
            //        text = text.Replace("v_S_01_02_00:eSocial_2240", "v_S_01_02_00");
            //        text = text.Replace("eSocial_2240", "eSocial");

            //        File.WriteAllText(xArq, text);

            //        xContent = xContent.Replace("xmlns:xsi=", "");
            //        xContent = xContent.Replace("\"http://www.w3.org/2001/XMLSchema-instance\"", "");
            //        xContent = xContent.Replace("xmlns:xsd=", "");
            //        xContent = xContent.Replace("\"http://www.w3.org/2001/XMLSchema\"", "");
            //        xContent = xContent.Replace("evtCAT", "evtExpRisco");
            //        //xContent = xContent.Replace("v_S_01_02_00", "v_S_01_02_00:eSocial_2240");
            //        xContent = xContent.Replace("v_S_01_02_00:eSocial_2240", "v_S_01_02_00");
            //        xContent = xContent.Replace("eSocial_2240", "eSocial");
            //        //using (Stream stream = File.Open(xArq, FileMode.Create))
            //        //{
            //        //    XmlSerializer serializer = new XmlSerializer(typeof(eSocialEvtExpRisco));
            //        //    serializer.Serialize(stream, xRisco);
            //        //    stream.Flush();

            //        //    byte[] bytes = new byte[stream.Length];
            //        //    stream.Position = 0;
            //        //    stream.Read(bytes, 0, (int)stream.Length);
            //        //    xContent = Encoding.ASCII.GetString(bytes);

            //        //    stream.Close();
            //        //}

            //        lst_Arqs.Items.Add(xArq);

            //        //salvar XML no SQL server com xeSocial.Id e nome do arquivo criado e datahora 
            //        Int64 zPis = 0;
            //        if (rDs.Tables[0].Rows[zCont]["PIS"].ToString().Trim() != "")
            //            zPis = System.Convert.ToInt64(System.Text.RegularExpressions.Regex.Match(rDs.Tables[0].Rows[zCont]["PIS"].ToString().Trim(), @"\d+").Value);


            //        Ilitera.Data.eSocial zeSocial = new Ilitera.Data.eSocial();
            //        zeSocial.XML_Deposito(xArq, xeSocial.Id, zCont, xContent, rDs.Tables[0].Rows[zCont]["CPF"].ToString(), zPis, rDs.Tables[0].Rows[zCont]["CNPJ"].ToString(), dtp_Inicial.Text, dtp_Final.Text, System.Convert.ToInt32(rDs.Tables[0].Rows[0]["nId_Laud_Tec"].ToString()), xRisco.Id);





            //    }
            //    catch (Exception ex)
            //    {                    
            //        MsgBox1.Show("Ilitera.Net", "Erro:" + ex.ToString(), null, new EO.Web.MsgBoxButton("OK"));
            //        return;
            //    }



            //}


            //using (ZipFile zip = new ZipFile())
            //{
            //    zip.AlternateEncodingUsage = ZipOption.AsNecessary;
            //    zip.AddDirectoryByName("Files");

            //    for (int rCont = 0; rCont < lst_Arqs.Items.Count; rCont++)
            //    {                    
            //        zip.AddFile(lst_Arqs.Items[rCont].Text, "Files");
            //    }
            //    Response.Clear();
            //    Response.BufferOutput = false;
            //    string zipName = String.Format("eSocial2240_Zip_{0}.zip", DateTime.Now.ToString("yyyy-MMM-dd-HHmmss"));
            //    Response.ContentType = "application/zip";
            //    Response.AddHeader("content-disposition", "attachment; filename=" + zipName);
            //    zip.Save(Response.OutputStream);
            //    Response.End();
            //}


            //MsgBox1.Show("Ilitera.Net", "Arquivo(s) criados.", null, new EO.Web.MsgBoxButton("OK"));
            //return;

        }



        private string ConvertWesternEuropeanToASCII(string str)
        {
            return Encoding.ASCII.GetString(Encoding.GetEncoding(1251).GetBytes(str));
        }



        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]
        private void Processar_S1060()
        {

            Int32 zEmpresa = 0;

            string xArq = "";

            System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");


            zEmpresa = System.Convert.ToInt32( Session["Empresa"].ToString() );


            Ilitera.Data.eSocial zEsocial = new Ilitera.Data.eSocial();

            DataSet rDs = zEsocial.Trazer_1060(zEmpresa);

            if (rDs.Tables[0].Rows.Count < 1)
            {                
                MsgBox1.Show("Ilitera.Net", "Não há registros para gerar o evento selecionado.", null, new EO.Web.MsgBoxButton("OK"));
                return;
            }


            //criar evento de controle para este lote
            tbleSocial xeSocial = new tbleSocial();
            xeSocial.DataHora_Criacao = System.DateTime.Now;
            xeSocial.Evento = "1060";
            Entities.Usuario user = (Entities.Usuario)Session["usuarioLogado"];
            xeSocial.IdUsuario = user.IdUsuario;
            xeSocial.Save();



            for (int zCont = 0; zCont < rDs.Tables[0].Rows.Count; zCont++)
            {

                eSocial_1060 x1060 = new eSocial_1060();

                //eSocialEvtTabAmbiente xAmbiente = new eSocialEvtTabAmbiente();

                ////eSocialEvtTabAmbienteInfoAmbienteInclusao xAmbInc = new eSocialEvtTabAmbienteInfoAmbienteInclusao();

                //TIdeCadastro xIdeEvento = new TIdeCadastro();
                //TEmpregador xIdeEmpregador = new TEmpregador();



                try
                {

                //    xAmbiente.Id = Gerar_ID(rDs.Tables[0].Rows[zCont]["nrInsc"].ToString().Trim().Substring(0, 8), zCont);


                //    //Empregador
                //    xIdeEmpregador.tpInsc = 1;
                //    xIdeEmpregador.nrInsc = rDs.Tables[0].Rows[zCont]["nrInsc"].ToString().Substring(0, 8);  //CNPJ

                //    xAmbiente.ideEmpregador = xIdeEmpregador;


                //    ////////////////////////////////////////////////

                //    //IdeEvento

                //    xIdeEvento.procEmi = 1;
                //    xIdeEvento.tpAmb = 1;   // 2 se produção restrita
                //    xIdeEvento.verProc = "1";

                //    xAmbiente.ideEvento = xIdeEvento;


                //    eSocialEvtTabAmbienteInfoAmbiente xInfoAmbiente = new eSocialEvtTabAmbienteInfoAmbiente();
                //    eSocialEvtTabAmbienteInfoAmbienteInclusao xInfo2 = new eSocialEvtTabAmbienteInfoAmbienteInclusao();

                //    TDadosAmbiente xDadosAmbiente = new TDadosAmbiente();

                //    xDadosAmbiente.tpInsc = 1;
                //    xDadosAmbiente.localAmb = 1;
                //    xDadosAmbiente.dscAmb = rDs.Tables[0].Rows[zCont]["DscAmb"].ToString();

                //    xDadosAmbiente.nrInsc = rDs.Tables[0].Rows[zCont]["nrInsc"].ToString().Substring(0, 8);  //CNPJ

                //    xDadosAmbiente.nmAmb = rDs.Tables[0].Rows[zCont]["NmAmb"].ToString();

                //    //preencher
                //    //xDadosAmbiente.CodLotacao

                //    xInfo2.dadosAmbiente = xDadosAmbiente;




                //    TIdeAmbiente xTAmb = new TIdeAmbiente();

                //    xTAmb.codAmb = rDs.Tables[0].Rows[zCont]["CodAmb"].ToString();
                //    xTAmb.iniValid = "2019-07";

                //    xInfo2.ideAmbiente = xTAmb;

                    
                //    xInfoAmbiente.Item = xInfo2;

                    
                //    xAmbiente.infoAmbiente = xInfoAmbiente;

                //    x1060.evtTabAmbiente = xAmbiente;



                //    ////criar xml
                    //if (rd_Todos.Checked == true)
                    //{
                    xArq = "I:\\temp\\" + txt_Arq.Text.Substring(0, txt_Arq.Text.IndexOf(".")) + "_1060_" + rDs.Tables[0].Rows[zCont]["NrInsc"].ToString().Trim() + "_" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + "_" + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString() + ".XML";
                    //}
                    //else
                    //{
                    //    xArq = txt_Arq.Text;
                    //}




                    //CRIAR XML
                    string xContent = "";

                    XmlSerializer serializer = new XmlSerializer(typeof(eSocial_1060));

                    var memoryStream = new MemoryStream();
                    var streamWriter = new StreamWriter(memoryStream, System.Text.Encoding.UTF8);

                    serializer.Serialize(streamWriter, x1060);

                    byte[] bytes = memoryStream.ToArray();
                    xContent = Encoding.UTF8.GetString(bytes);

                    using (FileStream fs = File.Create(xArq))
                    {
                        fs.Write(bytes, 0, bytes.Length);
                        fs.Close();
                    }



                    string text = File.ReadAllText(xArq);
                    text = text.Replace("xmlns:xsi=", "");
                    text = text.Replace("\"http://www.w3.org/2001/XMLSchema-instance\"", "");
                    text = text.Replace("xmlns:xsd=", "");
                    text = text.Replace("\"http://www.w3.org/2001/XMLSchema\"", "");
                    File.WriteAllText(xArq, text);

                    xContent = xContent.Replace("xmlns:xsi=", "");
                    xContent = xContent.Replace("\"http://www.w3.org/2001/XMLSchema-instance\"", "");
                    xContent = xContent.Replace("xmlns:xsd=", "");
                    xContent = xContent.Replace("\"http://www.w3.org/2001/XMLSchema\"", "");

                    //using (Stream stream = File.Open(xArq, FileMode.Create))
                    //{
                    //    XmlSerializer serializer = new XmlSerializer(typeof(eSocialEvtTabAmbiente));
                    //    serializer.Serialize(stream, xAmbiente);
                    //    stream.Flush();

                    //    byte[] bytes = new byte[stream.Length];
                    //    stream.Position = 0;
                    //    stream.Read(bytes, 0, (int)stream.Length);
                    //    xContent = Encoding.ASCII.GetString(bytes);

                    //    stream.Close();

                    //    //System.Threading.Thread.Sleep(500);
                    //    //xContent = File.ReadAllText(xArq, Encoding.UTF8);

                    //}


                    lst_Arqs.Items.Add(xArq);

                    //salvar XML no SQL server com xeSocial.Id e nome do arquivo criado e datahora 
                    Int64 zPis = 0;

                    Ilitera.Data.eSocial zeSocial = new Ilitera.Data.eSocial();
                    //zeSocial.XML_Deposito(xArq, xeSocial.Id, zCont, xContent, "", zPis, rDs.Tables[0].Rows[zCont]["nrInsc"].ToString(), dtp_Inicial.Text, dtp_Final.Text, System.Convert.ToInt32(rDs.Tables[0].Rows[0]["CodAmb"].ToString()), xAmbiente.Id);




                }
                catch (Exception ex)
                {                    
                    MsgBox1.Show("Ilitera.Net", "Erro:" + ex.ToString(), null, new EO.Web.MsgBoxButton("OK"));
                    return;
                }


            }


            using (ZipFile zip = new ZipFile())
            {
                zip.AlternateEncodingUsage = ZipOption.AsNecessary;
                zip.AddDirectoryByName("Files");

                for (int rCont = 0; rCont < lst_Arqs.Items.Count; rCont++)
                {
                    zip.AddFile(lst_Arqs.Items[rCont].Text, "Files");
                }
                Response.Clear();
                Response.BufferOutput = false;
                string zipName = String.Format("eSocial1060_Zip_{0}.zip", DateTime.Now.ToString("yyyy-MMM-dd-HHmmss"));
                Response.ContentType = "application/zip";
                Response.AddHeader("content-disposition", "attachment; filename=" + zipName);
                zip.Save(Response.OutputStream);
                Response.End();
            }



            MsgBox1.Show("Ilitera.Net", "Arquivo(s) criados.", null, new EO.Web.MsgBoxButton("OK"));
            return;

        }




    }
}
