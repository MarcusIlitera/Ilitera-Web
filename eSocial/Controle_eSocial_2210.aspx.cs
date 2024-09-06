using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Ilitera.Opsa.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Ilitera.Common;
using Entities;
using BLL;
using System.Collections;
using System.IO;
using System.Net;
using System.Xml.Serialization;
using System.Text.RegularExpressions;

using System.Xml;
using System.Xml.Linq;

using System.Security.Cryptography.X509Certificates;

using System.Security.Cryptography;
using System.Security.Cryptography.Xml;

namespace Ilitera.Net.e_Social
{
    public partial class Controle_eSocial_2210 : System.Web.UI.Page
    {

        protected Prestador prestador = new Prestador();
        protected Ilitera.Common.Usuario usuario = new Ilitera.Common.Usuario();
        protected Cliente cliente = new Cliente();
        protected Ilitera.Opsa.Data.Empregado empregado = new Ilitera.Opsa.Data.Empregado();


        protected void Page_Load(object sender, System.EventArgs e)
        {
            
            try
            {
                string FormKey = this.Page.ToString().Substring(4);

                Ilitera.Common.Funcionalidade funcionalidade = new Ilitera.Common.Funcionalidade();
                funcionalidade.Find("ClassName='" + FormKey + "'");

                if (funcionalidade.Id == 0)
                    throw new Exception("Formulário não cadastrado - " + FormKey);

                Entities.Usuario xUser = (Entities.Usuario)Session["usuarioLogado"];
                Ilitera.Common.Usuario.Permissao_Web(xUser.IdUsuario, funcionalidade.Id);
            }

            catch (Exception ex)
            {
                Session["Message"] = ex.Message;
                Server.Transfer("~/Tratar_Excecao.aspx");
                return;
            }

            //try
            //{
            //    Entities.Usuario xUser = (Entities.Usuario)Session["usuarioLogado"];
            //    Ilitera.Common.Usuario.Permissao_Web_Completo(xUser.IdUsuario);
            //}
            //catch (Exception ex)
            //{
            //    Session["Message"] = ex.Message;
            //    Server.Transfer("~/Tratar_Excecao.aspx");
            //    return;
            //}


            InicializaWebPageObjects();


            cliente = new Cliente(System.Convert.ToInt32(Session["Empresa"].ToString()));


            if ( cliente.Bloquear_2210==true)
            {
                Session["ExceptionType"] = "Restrição de acesso";
                Session["Source"] = "Mensageria 2210";
                Session["Message"] = "Mensageria bloqueada para este cliente.";
                Server.Transfer("~/Tratar_Excecao.aspx");
                return;
            }



            if (!IsPostBack)
            {
                System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");
                txt_Data2.Text = System.DateTime.Now.AddYears(1).ToString("dd/MM/yyyy", ptBr);
                txt_Data.Text = "13/10/2021"; //System.DateTime.Now.ToString("dd/MM/yyyy", ptBr);
                Carga_Grid();
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

        protected void InicializaWebPageObjects()
        {
            //base.InicializaWebPageObjects();
            StringBuilder st = new StringBuilder();

            //st.Append("top.window.document.frames.conteudo.window.document.getElementById('txtIdEmpregado').value = '" + empregado.Id + "';");
            st.Append("top.window.document.getElementById('txtIdEmpregado').value = '" + Request["IdEmpregado"] + "';");
            st.Append("top.window.document.getElementById('txtIdUsuario').value = '" + Request["IdUsuario"] + "';");
            st.Append("top.window.document.getElementById('txtIdEmpresa').value = '" + Request["IdEmpresa"] + "';");


            this.ClientScript.RegisterStartupScript(this.GetType(), "IdEmpregado", st.ToString(), true);


        }



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

        private string GetTextFromXMLFile(string file)
        {
            StreamReader reader = new StreamReader(file);
            string ret = reader.ReadToEnd();
            reader.Close();
            return ret;
        }



        protected void grd_eSocial_ItemCommand(object sender, EO.Web.GridCommandEventArgs e)
        {

            lbl_IdeSocial_Deposito.Text = e.Item.Cells[0].Value.ToString().Trim();

            if (e.CommandName.Trim() == "17")
            {
                if (lbl_IdeSocial_Deposito.Text.Trim() == "")
                {
                    MsgBox1.Show("Ilitera.Net", "XML Pendente de criação", null,
                                  new EO.Web.MsgBoxButton("OK"));
                    return;
                }


                //abrir janela com XML
                Ilitera.Data.eSocial xDados = new Ilitera.Data.eSocial();

                DataSet rDs = new DataSet();
                rDs = xDados.Trazer_XML2(System.Convert.ToInt32(lbl_IdeSocial_Deposito.Text));

                string strXML = rDs.Tables[0].Rows[0][0].ToString();

                //System.Xml.XmlDocument xDoc = new System.Xml.XmlDocument();
                //xDoc.LoadXml(strXML);

                //Session["XML"] = xDoc.InnerXml;
                Session["XML"] = strXML;


                //StringBuilder st = new StringBuilder();

                //st.AppendFormat("void(window.open('ViewXML.aspx',  '_newtab' ))" ) ;  
                //    //'CursoEmpresa','scrollbars=yes, resizable=yes, toolbar=no, addressbar=no, menubar=no, width=600px, height=500px'));");

                //ScriptManager.RegisterStartupScript(this, this.GetType(), " ", st.ToString(), true);


                //esse funcionou abrindo novo tab
                //string redirect = "<script>window.open('ViewXML.aspx');</script>";

                string redirect = "<script>window.open('ViewXML.aspx', '_blank', 'location=yes,height=570,width=900,scrollbars=yes,status=yes');</script>";
                Response.Write(redirect);


            }
            else if (e.CommandName.Trim() == "16")
            {

                if (lbl_IdeSocial_Deposito.Text.Trim() == "")
                {
                    MsgBox1.Show("Ilitera.Net", "Sem detalhes para XML pendente de criação", null,
                                  new EO.Web.MsgBoxButton("OK"));
                    return;
                }


                //carregar lista com detalhes
                lst_Detalhes.Items.Clear();
                lst_Id_Detalhes.Items.Clear();

                Ilitera.Data.eSocial zDet = new Ilitera.Data.eSocial();

                DataSet zDs = new DataSet();
                zDs = zDet.Trazer_Detalhes_Envio(System.Convert.ToInt32(lbl_IdeSocial_Deposito.Text));

                for (int zCont = 0; zCont < zDs.Tables[0].Rows.Count; zCont++)
                {
                    lst_Id_Detalhes.Items.Add(zDs.Tables[0].Rows[zCont][0].ToString().Trim());

                    if (zDs.Tables[0].Rows[zCont][5].ToString().Trim() == "R")  // recriar
                    {
                        lst_Detalhes.Items.Add(zDs.Tables[0].Rows[zCont][3].ToString().Trim() + "   " + zDs.Tables[0].Rows[zCont][4].ToString().Trim() + "  XML Recriado em " + zDs.Tables[0].Rows[zCont][1].ToString().Trim() + "  por usuário " + zDs.Tables[0].Rows[zCont][2].ToString().Trim());
                    }
                    else
                    {
                        lst_Detalhes.Items.Add(zDs.Tables[0].Rows[zCont][3].ToString().Trim() + "   " + zDs.Tables[0].Rows[zCont][4].ToString().Trim() + "  Criado em " + zDs.Tables[0].Rows[zCont][1].ToString().Trim() + "  por usuário " + zDs.Tables[0].Rows[zCont][2].ToString().Trim());
                    }
                }



                //ativar comandos
                grd_eSocial.Height = 300;
                lst_Detalhes.Visible = true;
                cmd_Fechar_Det.Visible = true;
                cmd_Proc_Evento.Visible = true;
                cmd_Proc_Lote.Visible = true;

                cmd_Proc_Atualizar.Visible = false;


            }


        }



        protected void cmd_Voltar_Click(object sender, EventArgs e)
        {

            Entities.Usuario user = (Entities.Usuario)Session["usuarioLogado"];
            Response.Redirect("~\\ListaEmpregados.aspx?&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString());

        }

        protected void btnFichaCompleta_Click(object sender, EventArgs e)
        {

        }





        protected static string strOpenReport(string directory, string fileAndQuery, string ReportName)
        {
            return strOpenReport(directory, fileAndQuery, ReportName, false);
        }

        protected static string strOpenReport(string directory, string fileAndQuery, string ReportName, bool useDirectoryForLocalProcess)
        {
            StringBuilder st = new StringBuilder();

            Guid strAux = Guid.NewGuid();

            string valueProcess = "Local";

            if (valueProcess.Equals("Remote"))
                st.Append("void(window.open('http://report.ilitera.net/index.aspx?Identity=" + strAux.ToString() + "opsa" + strAux.ToString()
                    + "&Key=" + strAux.ToString() + "MestraNet" + strAux.ToString()
                    + "&PathAndQuery=" + HttpUtility.UrlEncode(directory + "/" + fileAndQuery) + "', '" + ReportName + "', 'scrollbars=yes, resizable=yes, toolbar=no, addressbar=no, menubar=no, width=800px, height=600px').focus());");
            else if (valueProcess.Equals("Local"))
            {
                if (useDirectoryForLocalProcess)
                {
                    st.AppendFormat("void(window.open('../{0}/{1}', '{2}', 'scrollbars=yes, resizable=yes, toolbar=no, addressbar=no, menubar=no, width=800px, height=600px').focus());", directory, fileAndQuery, ReportName);
                }
                else
                {
                    st.AppendFormat("void(window.open('{0}','{1}', 'scrollbars=yes, resizable=yes, toolbar=no, addressbar=no, menubar=no, width=800px, height=600px').focus());", fileAndQuery, ReportName);
                }
            }

            return st.ToString();
        }


        protected void MsgBox2_ButtonClick(object sender, System.Web.UI.WebControls.CommandEventArgs e)
        {
            //Use the command name to determine which
            //button was clicked
            if (e.CommandName == "Confirmo entrega")
            {

            }
            else if (e.CommandName == "Confirmo entrega - qtde 100")
            {

            }
            else if (e.CommandName == "Confirmo entrega - qtde 10")
            {

            }
            else if (e.CommandName == "Confirmo entrega - qtde 1")
            {

            }

            foreach (EO.Web.GridItem item in grd_eSocial.CheckedItems)
            {
                //item.Deleted = true;
                string zCod = item.Cells[0].Value.ToString();

                if (zCod.Trim() == "") //não foi processado ainda, não tem IdeSocial_Deposito

                {

                }
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
                MsgBox1.Show("Ilitera.Net", "Data Inválida.  Utilizar formato dd/MM/yyyy", null,
                new EO.Web.MsgBoxButton("OK"));
                return false;
            }

            Validar = zData.Substring(0, 2);
            isNumerical = int.TryParse(Validar, out myInt);
            if (isNumerical == false)
            {
                MsgBox1.Show("Ilitera.Net", "Dia Inválido.  Utilizar formato dd/MM/yyyy", null,
                new EO.Web.MsgBoxButton("OK"));
                return false;
            }


            Validar = zData.Substring(3, 2);
            isNumerical = int.TryParse(Validar, out myInt);
            if (isNumerical == false)
            {
                MsgBox1.Show("Ilitera.Net", "Mês Inválido.  Utilizar formato dd/MM/yyyy", null,
                new EO.Web.MsgBoxButton("OK"));
                return false;
            }


            Validar = zData.Substring(6, 4);
            isNumerical = int.TryParse(Validar, out myInt);
            if (isNumerical == false)
            {
                MsgBox1.Show("Ilitera.Net", "Ano Inválido.  Utilizar formato dd/MM/yyyy", null,
                new EO.Web.MsgBoxButton("OK"));
                return false;
            }


            if (zData.Substring(2, 1) != "/" || zData.Substring(5, 1) != "/")
            {
                MsgBox1.Show("Ilitera.Net", "Data Inválida.  Utilizar formato dd/MM/yyyy", null,
                new EO.Web.MsgBoxButton("OK"));
                return false;
            }


            zDia = System.Convert.ToInt32(zData.Substring(0, 2));
            zMes = System.Convert.ToInt32(zData.Substring(3, 2));
            zAno = System.Convert.ToInt32(zData.Substring(6, 4));

            if (zAno < 1900 || zAno > 2030)
            {
                MsgBox1.Show("Ilitera.Net", "Ano Inválido.  Utilizar formato dd/MM/yyyy", null,
                new EO.Web.MsgBoxButton("OK"));
                return false;
            }

            if (zMes < 1 || zMes > 12)
            {
                MsgBox1.Show("Ilitera.Net", "Mês Inválido.  Utilizar formato dd/MM/yyyy", null,
                new EO.Web.MsgBoxButton("OK"));
                return false;
            }

            if (zMes == 1 || zMes == 3 || zMes == 5 || zMes == 7 || zMes == 8 || zMes == 10 || zMes == 12)
            {
                if (zDia < 1 || zDia > 31)
                {
                    MsgBox1.Show("Ilitera.Net", "Dia Inválido.  Utilizar formato dd/MM/yyyy", null,
                    new EO.Web.MsgBoxButton("OK"));
                    return false;
                }
            }
            else if (zMes == 4 || zMes == 6 || zMes == 9 || zMes == 11)
            {
                if (zDia < 1 || zDia > 30)
                {
                    MsgBox1.Show("Ilitera.Net", "Dia Inválido.  Utilizar formato dd/MM/yyyy", null,
                    new EO.Web.MsgBoxButton("OK"));
                    return false;
                }
            }
            else
            {
                if (zDia < 1 || zDia > 29)
                {
                    MsgBox1.Show("Ilitera.Net", "Dia Inválido.  Utilizar formato dd/MM/yyyy", null,
                    new EO.Web.MsgBoxButton("OK"));
                    return false;
                }
            }

            return true;

        }





        protected void cmb_Carga_SelectedIndexChanged(object sender, EventArgs e)
        {

            Carga_Grid();
        }

        protected void cmd_Filtrar_Click(object sender, EventArgs e)
        {

            Carga_Grid();

        }


        private void Carga_Grid()
        {

            if (Validar_Data(txt_Data.Text.Trim()) == false)
            {
                MsgBox1.Show("Ilitera.Net", "Data Inicial de Filtro inválida!", null,
                      new EO.Web.MsgBoxButton("OK"));
                return;
            }

            if (Validar_Data(txt_Data2.Text.Trim()) == false)
            {
                MsgBox1.Show("Ilitera.Net", "Data Final de Filtro inválida!", null,
                      new EO.Web.MsgBoxButton("OK"));
                return;
            }

            string zTipo_Carga = "4";

            if (cmb_Carga.SelectedIndex == 1) zTipo_Carga = "1";
            else if (cmb_Carga.SelectedIndex == 2) zTipo_Carga = "0";
            else if (cmb_Carga.SelectedIndex == 3) zTipo_Carga = "2";
            else if (cmb_Carga.SelectedIndex == 4) zTipo_Carga = "3";

            string zGrupo = "N";

            if (chk_Grupo.Checked == true)
            {
                zGrupo = "S";
            }
            else
            {
                zGrupo = "N";
            }

            Entities.Usuario xUser = (Entities.Usuario)Session["usuarioLogado"];

            DataSet zDs = new DataSet();
            Ilitera.Data.eSocial xLista = new Ilitera.Data.eSocial();
            zDs = xLista.Mensageria_2210(Convert.ToInt32(Request.QueryString["IdEmpresa"]), 0, 0, txt_Data.Text, txt_Data2.Text, xUser.IdUsuario, zTipo_Carga, txt_Nome.Text, zGrupo, cliente.ESocial_Ambiente);
            grd_eSocial.DataSource = zDs;

            grd_eSocial.DataBind();

        }




        protected void cmd_Criar_Click(object sender, EventArgs e)
        {
            int zCont = 0;


            foreach (EO.Web.GridItem item in grd_eSocial.CheckedItems)
            {
                //item.Deleted = true;
                string zCod = item.Cells[14].Value.ToString();


                if (zCod.Trim() != "") //foi processado, não pode recriar
                {
                    MsgBox1.Show("Ilitera.Net", "Existem registros enviados selecionados. No sistema web não é possível recriar XML de registros enviados.", null,
                                                new EO.Web.MsgBoxButton("OK"));
                    return;
                }


                zCod = item.Cells[0].Value.ToString();



                if (zCod.Trim() != "") //foi processado, não pode recriar
                {
                    MsgBox1.Show("Ilitera.Net", "Existem registros processados selecionados. No sistema web não é possível recriar XML.", null,
                                                new EO.Web.MsgBoxButton("OK"));
                    return;
                }

                zCont++;
            }


            if (zCont == 0)
            {
                MsgBox1.Show("Ilitera.Net", "Selecionar pelo menos 1 registro.", null,
                            new EO.Web.MsgBoxButton("OK"));
                return;
            }



            object zsender = new object();
            EventArgs ze = new EventArgs();
            cmd_Fechar_Det_Click(zsender, ze);

            Entities.Usuario user = (Entities.Usuario)Session["usuarioLogado"];

            int zContador = 0;

            foreach (EO.Web.GridItem item in grd_eSocial.CheckedItems)
            {
                zContador = zContador + 1;

                //item.Deleted = true;
                string zCod = item.Cells[0].Value.ToString();

                string xLaudo = item.Cells[11].Value.ToString();
                string xD1 = item.Cells[6].Value.ToString();
                string xD2 = item.Cells[7].Value.ToString();
                string xIdAcidente = item.Cells[5].Value.ToString().Trim();

                if (xD1.Substring(0, 3) == "201" || xD1.Substring(0, 3) == "202")
                    xD1 = xD1.Substring(8, 2) + "/" + xD1.Substring(5, 2) + "/" + xD1.Substring(0, 4);

                if (xD2.Substring(0, 3) == "201" || xD2.Substring(0, 3) == "202")
                    xD2 = xD2.Substring(8, 2) + "/" + xD2.Substring(5, 2) + "/" + xD2.Substring(0, 4);
                //if (zCod.Trim() != "") //foi processado, apagar tblDeposito - inibir isso, acho melhor manter histórico completo
                //{
                //
                //    Ilitera.Data.eSocial zeSocial = new Ilitera.Data.eSocial();
                //    zeSocial.XML_Deposito_Excluir(System.Convert.ToInt32(zCod));
                //
                //}


                //criar e aguardar XML e registro tblDeposito
                Processar_S2210(System.Convert.ToInt32(xLaudo), xD1, xD2, zCod, xIdAcidente, user.IdUsuario, zContador, cliente.ESocial_Ambiente);

            }

            Carga_Grid();

        }



        public void Processar_S2210(Int32 zLaudo, string zD1, string zD2, string zCod, string zIdAcidente, Int32 zIdUsuario, int zContador, string zAmbiente)
        {
            Int32 znId_Empr = 0;
            string xArq = "";

            System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");


            Ilitera.Data.eSocial zEsocial = new Ilitera.Data.eSocial();

            DataSet rDs = zEsocial.Trazer_2210(System.Convert.ToInt32(zIdAcidente));

            if (rDs.Tables[0].Rows.Count < 1)
            {
                MsgBox1.Show("Ilitera.Net", "Não há registros para gerar o evento selecionado.", null, new EO.Web.MsgBoxButton("OK"));
                return;
            }


            //criar evento de controle para este lote
            tbleSocial xeSocial = new tbleSocial();
            xeSocial.DataHora_Criacao = System.DateTime.Now;
            xeSocial.Evento = "2210";

            if ( zAmbiente=="1" ) //(cliente.ESocial_Ambiente == "1")  //produção
                xeSocial.Ambiente = "1";
            else
                xeSocial.Ambiente = "2";   // 2 se produção restrita

            xeSocial.IdUsuario = zIdUsuario;
            xeSocial.Save();



            for (int zCont = 0; zCont < rDs.Tables[0].Rows.Count; zCont++)
            {



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
                eSocialEvtCATCatCatOrigem xOrigem = new eSocialEvtCATCatCatOrigem();

                T_ideEmpregador xIdeEmpregador = new T_ideEmpregador();
                //TEmpregador xIdeEmpregador = new TEmpregador();



                //xCat.ideEmpregador = xIdeEmpregador;


                xAgente = new eSocialEvtCATCatAgenteCausador();
                xParte = new eSocialEvtCATCatParteAtingida();



                try
                {

                    znId_Empr = System.Convert.ToInt32(rDs.Tables[0].Rows[zCont]["nId_Empr"].ToString());


                    if (rDs.Tables[0].Rows[zCont]["CAEPF"].ToString().Trim() != "0" && rDs.Tables[0].Rows[zCont]["CAEPF"].ToString().Trim() != "")
                    {
                        xIdeEmpregador.tpInsc = 2;
                        string rCPF = GerarCpf(rDs.Tables[0].Rows[0]["CAEPF"].ToString().Trim().Substring(0, 9));
                        xIdeEmpregador.nrInsc = rCPF; //CPF
                        xCat.Id = Gerar_ID(rCPF, zCont + zContador + 1);
                    }
                    else
                    {


                        Ilitera.Data.eSocial xTrazerCNPJ = new Ilitera.Data.eSocial();
                        //string xCNPJ_Atual = xTrazerCNPJ.Retornar_CNPJ_Classif_Funcional_Atual(System.Convert.ToInt32(rDs.Tables[0].Rows[zCont]["nId_Empregado"].ToString().Trim()));
                        string xCNPJ_Matriz = xTrazerCNPJ.Retornar_CNPJ_Matriz(znId_Empr);


                        if (xCNPJ_Matriz.Trim() == "")
                        {
                            xIdeEmpregador.nrInsc = rDs.Tables[0].Rows[0]["CNPJ"].ToString().Substring(0, 8);  //CNPJ
                            xCat.Id = Gerar_ID(rDs.Tables[0].Rows[zCont]["CNPJ"].ToString().Trim().Substring(0, 8), zCont + zContador + 1);
                        }
                        else
                        {
                            xIdeEmpregador.nrInsc = xCNPJ_Matriz.Substring(0, 8);
                            xCat.Id = Gerar_ID(xCNPJ_Matriz.Substring(0, 8), zCont + zContador + 1);
                        }



                        //xCat.Id = Gerar_ID(rDs.Tables[0].Rows[zCont]["CNPJ"].ToString().Trim().Substring(0, 8), zCont + zContador + 1);

                        //Empregador
                        xIdeEmpregador.tpInsc = 1;
                        //xIdeEmpregador.nrInsc = rDs.Tables[0].Rows[zCont]["CNPJ"].ToString().Substring(0, 8);  //CNPJ

                    }


                    string xCNPJ = rDs.Tables[0].Rows[zCont]["CNPJ"].ToString().Trim();

                    //if (xCNPJ.Length == 14)
                    //    xIdeEmpregador.nrInsc = xCNPJ;
                    //else
                    //    xIdeEmpregador.nrInsc = new string('0', 14 - xCNPJ.Length) + xCNPJ;


                    //xIdeEmpregador.nrInsc = rDs.Tables[0].Rows[zCont]["CNPJ"].ToString().Substring(0, 14);  //CNPJ

                    xCat.ideEmpregador = xIdeEmpregador;


                    ////////////////////////////////////////////////

                    //IdeEvento
                    //IdeEvento
                    //xIdeEvento.indRetif = (sbyte)zRetif;

                    //if (zRetif == 2)
                    //{
                    //    xIdeEvento.nrRecibo = zProtocolo;
                    //}

                    xIdeEvento.indRetif = 1;
                    xIdeEvento.procEmi = 1;


                    if ( zAmbiente=="1" ) // (cliente.ESocial_Ambiente == "1")  //produção
                        xIdeEvento.tpAmb = 1;
                    else
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

                    int zTpAcid = System.Convert.ToInt32(rDs.Tables[0].Rows[zCont]["IndTipoAcidente"].ToString().Trim());

                    xEvtCat.tpAcid = (sbyte)zTpAcid;



                    //preencher horas somente se TpAcid = 1
                    if (zTpAcid == 1 || zTpAcid == 3)
                    {
                        if (rDs.Tables[0].Rows[zCont]["hrAcid"].ToString().Trim() != "")
                        {
                            xEvtCat.hrAcid = rDs.Tables[0].Rows[zCont]["hrAcid"].ToString().Trim().Substring(0, 2) + rDs.Tables[0].Rows[zCont]["hrAcid"].ToString().Trim().Substring(3, 2);
                        }
                        else
                        {
                            xEvtCat.hrAcid = "00:00";
                        }
                    }
                    //pelo manual esse campos deveria ir apenas com 
                    // TpAcid=1,  mas está dando erro se  não envio com tpAcid=3
                    // 28/07/2022
                    //if (zTpAcid == 1)
                    //{  

                    if (zTpAcid != 2)
                    {
                        xEvtCat.hrsTrabAntesAcid = rDs.Tables[0].Rows[zCont]["hrsTrabAntesAcid"].ToString();
                    }

                    //}

                    // }




                //xEvtCat.tpCat = 1; // System.Convert.ToSByte(rDs.Tables[0].Rows[zCont]["tpCat"].ToString());


                // tratar reabertura de CAT

                bool xReabertura = false;

                    if (rDs.Tables[0].Rows[zCont]["Reabertura"].ToString().ToUpper().Trim() == "SIM")
                    {
                        xReabertura = true;
                    }


                    if (xReabertura == false)
                        xEvtCat.tpCat = 1; // System.Convert.ToSByte(rDs.Tables[0].Rows[zCont]["tpCat"].ToString());
                    else
                    {
                        xEvtCat.tpCat = 2;
                        xOrigem.nrRecCatOrig = rDs.Tables[0].Rows[zCont]["nrRecCatOrig"].ToString().ToUpper().Trim();

                        xEvtCat.catOrigem = xOrigem;
                    }





                    //if (rDs.Tables[0].Rows[zCont]["IndCatObito"].ToString().Trim() == "S")
                    //    xEvtCat.indCatObito = TS_sim_nao.S;
                    //else
                    //    xEvtCat.indCatObito = TS_sim_nao.N;



                    if (rDs.Tables[0].Rows[zCont]["dtObito"].ToString().Trim() != "")
                    {
                        xEvtCat.dtObito = System.Convert.ToDateTime(rDs.Tables[0].Rows[zCont]["dtObito"].ToString(), ptBr);
                        xEvtCat.dtObitoSpecified = true;
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

                    if (xReabertura == true)
                    {
                        xEvtCat.ultDiaTrab = System.Convert.ToDateTime(rDs.Tables[0].Rows[zCont]["dtAcidente"].ToString(), ptBr).AddDays(1);
                    }
                    else
                    {
                        xEvtCat.ultDiaTrab = System.Convert.ToDateTime(rDs.Tables[0].Rows[zCont]["UltimoDiaTrab"].ToString(), ptBr);
                    }

                    xEvtCat.ultDiaTrabSpecified = true;







                    //local acidente                    
                    //local acidente                    
                    int ztpLocal = System.Convert.ToInt32(rDs.Tables[0].Rows[zCont]["tpLocal"].ToString());

                    xAcidente.tpLocal = (sbyte)ztpLocal; //System.Convert.ToSByte(rDs.Tables[0].Rows[zCont]["tpLocal"].ToString());

                    

                    if (rDs.Tables[0].Rows[zCont]["DscLocal"].ToString().Trim() != "")
                        xAcidente.dscLocal = rDs.Tables[0].Rows[zCont]["DscLocal"].ToString().Trim();
                    else
                        xAcidente.dscLocal = "Empresa";



                    //validar municipio
                    string zMunic = "";

                    if (ztpLocal != 2)
                    {
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
                    }


                    string rUF = "";


                    if (rDs.Tables[0].Rows[zCont]["dscLograd"].ToString().Trim() != "")
                    {

                        xAcidente.tpLograd = "R";  // não temos esse campo na tela de acidente, logo, usar R de Rua

                        xAcidente.dscLograd = rDs.Tables[0].Rows[zCont]["dscLograd"].ToString();

                        if (rDs.Tables[0].Rows[zCont]["nrLograd"].ToString().Trim() != "")
                            xAcidente.nrLograd = rDs.Tables[0].Rows[zCont]["nrLograd"].ToString();
                        else
                            xAcidente.nrLograd = "S/N";

                        if (rDs.Tables[0].Rows[zCont]["Complemento"].ToString().Trim() != "")
                        {
                            if (rDs.Tables[0].Rows[zCont]["Complemento"].ToString().Trim().Length > 30)
                                xAcidente.complemento = rDs.Tables[0].Rows[zCont]["Complemento"].ToString().Trim().Substring(0, 30);
                            else
                                xAcidente.complemento = rDs.Tables[0].Rows[zCont]["Complemento"].ToString().Trim();
                        }

                        if (rDs.Tables[0].Rows[zCont]["Bairro"].ToString().Trim() != "")
                            xAcidente.bairro = rDs.Tables[0].Rows[zCont]["Bairro"].ToString().Trim();

                        if (ztpLocal != 2)
                        {
                            if (rDs.Tables[0].Rows[zCont]["CEP"].ToString().Trim() != "")
                                xAcidente.cep = rDs.Tables[0].Rows[zCont]["CEP"].ToString().Trim();

                            if (zMunic != "")
                                xAcidente.codMunic = zMunic;   //tabela de-para  municípios
                        }


                        if (rDs.Tables[0].Rows[zCont]["tpLocal"].ToString().Trim() == "2")
                        {
                            xAcidente.pais = "105";  //tabela de-para   países
                            xAcidente.codPostal = rDs.Tables[0].Rows[zCont]["CodPostal"].ToString().Trim();      //apenas se tpLocal=2
                        }

                    }
                    else
                    {
                        //pegar endereço do cadastro da empresa
                        //ArrayList list = new Cliente().Find(" dbo.udf_getnumeric(NomeCodigo) = '" + rDs.Tables[0].Rows[zCont]["CNPJ"].ToString().Trim() + "' ");
                        ArrayList list = new Cliente().Find(" IdCliente = " + rDs.Tables[0].Rows[zCont]["IdPessoa"].ToString().Trim());

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

                            xAcidente.dscLocal = "Empresa";

                            if (rEndereco.Logradouro.Trim() != "")
                                xAcidente.dscLograd = rEndereco.Logradouro;

                            if (rEndereco.Complemento.Trim() != "")
                            {
                                if (rEndereco.Complemento.Trim().Length > 30)
                                    xAcidente.complemento = rEndereco.Complemento.Substring(0, 30);
                                else
                                    xAcidente.complemento = rEndereco.Complemento;
                            }

                            rEndereco.IdTipoLogradouro.Find();
                            xAcidente.tpLograd = rEndereco.IdTipoLogradouro.NomeAbreviado.Trim().ToUpper();

                            //rUF - guardará UF caso não tenha retornado na busca a UF
                            rUF = rEndereco.Uf.ToUpper();


                            if (ztpLocal != 2)
                            {
                                if (rEndereco.Cep.Trim() != "")
                                    xAcidente.cep = rEndereco.Cep.Replace("-", string.Empty);

                                rEndereco.IdMunicipio.Find();
                                xAcidente.codMunic = rEndereco.IdMunicipio.Cod_Municipio_Completo.ToString().Trim();

                            }


                            if (rDs.Tables[0].Rows[zCont]["tpLocal"].ToString().Trim() == "2")
                            {
                                xAcidente.pais = "105";  //tabela de-para   países
                                xAcidente.codPostal = rDs.Tables[0].Rows[zCont]["CodPostal"].ToString().Trim();      //apenas se tpLocal=2
                            }


                        }


                    }










                    //versão 02/2020 - excluir CODAMB
                    //Ilitera.Data.Clientes_Funcionarios zAmb = new Ilitera.Data.Clientes_Funcionarios();

                    //string zCodAmb = zAmb.Trazer_Cod_GHE_Atual_Colaborador(System.Convert.ToInt32(rDs.Tables[0].Rows[zCont]["nId_Empregado"].ToString()));

                    //xAcidente.codAmb = zCodAmb;    //pegar cód GHE 






                    if (ztpLocal != 2)
                    {

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
                            xAcidente.uf = TS_uf.SP;

                            if (rUF != "")
                            {

                                switch (rUF)
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

                                xAcidente.ufSpecified = true;
                            }
                        }
                    }

                    else
                    {
                        xAcidente.ufSpecified = false;
                    }





                    eSocialEvtCATCatLocalAcidenteIdeLocalAcid xLocalAcid = new eSocialEvtCATCatLocalAcidenteIdeLocalAcid();


                    if (xAcidente.tpLocal == 1)
                    {

                        string xCNPJ_Acid = rDs.Tables[0].Rows[zCont]["CNPJ_Acidente"].ToString().Trim();

                        if (rDs.Tables[0].Rows[zCont]["CAEPF"].ToString().Trim() != "0" && rDs.Tables[0].Rows[zCont]["CAEPF"].ToString().Trim() != "")  // && rDs.Tables[0].Rows[zCont]["CAEPF"].ToString().Trim() != xCNPJ_Acid)
                        {
                            xLocalAcid.tpInsc = 3;
                            xLocalAcid.nrInsc = xCNPJ_Acid;
                        }
                        else
                        {


                            if (xCNPJ_Acid.Trim() == "")
                            {
                                xLocalAcid.tpInsc = 1;
                                xLocalAcid.nrInsc = xCNPJ;
                            }
                            else
                            {
                                if (xCNPJ_Acid.Trim().Substring(0, 8) == (xCNPJ.Trim().Substring(0, 8)))
                                {
                                    xLocalAcid.tpInsc = 1;
                                    xLocalAcid.nrInsc = xCNPJ_Acid;
                                }
                                else
                                {
                                    xLocalAcid.tpInsc = 3;
                                    xLocalAcid.nrInsc = xCNPJ_Acid;
                                }
                            }

                            xAcidente.ideLocalAcid = xLocalAcid;
                        }
                    }
                    else if (xAcidente.tpLocal == 3)
                    {
                        xLocalAcid.tpInsc = 1;
                        xLocalAcid.nrInsc = rDs.Tables[0].Rows[zCont]["CNPJ_Terceiro"].ToString().Trim();

                        xAcidente.ideLocalAcid = xLocalAcid;
                    }




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
                    //if (rDs.Tables[0].Rows[zCont]["dtAtendimento"].ToString().Trim() != "" && rDs.Tables[0].Rows[zCont]["DiagnosticoProvavel"].ToString() != "" && rDs.Tables[0].Rows[zCont]["CodCID"].ToString() != "")
                    //{

                    if (rDs.Tables[0].Rows[zCont]["dtAtendimento"].ToString() != "")
                        xAtestado.dtAtendimento = System.Convert.ToDateTime(rDs.Tables[0].Rows[zCont]["dtAtendimento"].ToString(), ptBr);
                    else
                        xAtestado.dtAtendimento = System.Convert.ToDateTime(rDs.Tables[0].Rows[zCont]["dtAcidente"].ToString(), ptBr);



                    if (rDs.Tables[0].Rows[zCont]["hrAtendimento"].ToString().Trim() != "")
                            xAtestado.hrAtendimento = rDs.Tables[0].Rows[zCont]["hrAtendimento"].ToString().Replace(":", "");
                        else
                            xAtestado.hrAtendimento = "0000";



                        if (rDs.Tables[0].Rows[zCont]["IntInternacao"].ToString().Trim() == "S")
                            xAtestado.indInternacao = TS_sim_nao.S;
                        else
                            xAtestado.indInternacao = TS_sim_nao.N;

                        xAtestado.durTrat = rDs.Tables[0].Rows[zCont]["durTrat"].ToString();



                        if (rDs.Tables[0].Rows[zCont]["IndAfast"].ToString().Trim() != "S")
                            xAtestado.indAfast = TS_sim_nao.N;
                        else
                            xAtestado.indAfast = TS_sim_nao.S;



                        xAtestado.dscLesao = rDs.Tables[0].Rows[zCont]["dscLesao"].ToString();

                    if (rDs.Tables[0].Rows[zCont]["dscCompLesao"].ToString().Trim() != "")
                    {
                        if (rDs.Tables[0].Rows[zCont]["dscCompLesao"].ToString().Trim().Length > 200)
                        {
                            xAtestado.dscCompLesao = rDs.Tables[0].Rows[zCont]["dscCompLesao"].ToString().Substring(0, 200);
                        }
                        else
                        {
                            xAtestado.dscCompLesao = rDs.Tables[0].Rows[zCont]["dscCompLesao"].ToString();
                        }
                    }


                        xAtestado.diagProvavel = rDs.Tables[0].Rows[zCont]["DiagnosticoProvavel"].ToString();
                        xAtestado.codCID = rDs.Tables[0].Rows[zCont]["CodCID"].ToString();

                        if (rDs.Tables[0].Rows[zCont]["Observacao"].ToString().Trim() != "")
                            xAtestado.observacao = rDs.Tables[0].Rows[zCont]["Observacao"].ToString().Replace("'"," ");





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
                        xEmitente.ufOCSpecified = true;

                        xAtestado.emitente = xEmitente;


                        xEvtCat.atestado = xAtestado;

                    //}


                    //catorigem
                    //if (rDs.Tables[0].Rows[zCont]["nrCatOrig"].ToString().Trim() != "" && rDs.Tables[0].Rows[zCont]["nrCatOrig"].ToString().Trim() != "0")
                    //{
                    //    xCatOrigem.nrRecCatOrig = rDs.Tables[0].Rows[zCont]["nrCatOrig"].ToString();

                    //    xEvtCat.catOrigem = xCatOrigem;
                    //}



                    xCat.cat = xEvtCat;

                    x2210.evtCAT = xCat;



                    //jogar em bytes o XML para salvar em tbleSocial_Deposito - devo alterar o esquema, não posso salvar como abaixo

                    //CRIAR XML
                    string xContent = "";

                    XmlSerializer serializer = new XmlSerializer(typeof(eSocial_2210));

                    var memoryStream = new MemoryStream();
                    var streamWriter = new StreamWriter(memoryStream, System.Text.Encoding.UTF8);

                    serializer.Serialize(streamWriter, x2210);

                    byte[] bytes = memoryStream.ToArray();
                    xContent = Encoding.UTF8.GetString(bytes);


                    if (xArq.Trim() == "")
                    {
                        xArq = "I:\\temp\\Evt2210_" + System.DateTime.Now.Second.ToString().Trim() + System.DateTime.Now.Minute.ToString().Trim() + System.DateTime.Now.Millisecond.ToString().Trim() + System.DateTime.Now.Day.ToString().Trim() + System.DateTime.Now.Month.ToString().Trim() + ".xml";
                    }


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
                    text = text.Replace("evtMonit", "evtCAT");
                    //text = text.Replace("evtCAT", "evtAfastTemp");
                    //text = text.Replace("v_S_01_02_00", "v_S_01_02_00:eSocial_2210");
                    text = text.Replace("v_S_01_02_00:eSocial_2210", "v_S_01_02_00");
                    text = text.Replace("eSocial_2210", "eSocial");

                    File.WriteAllText(xArq, text);

                    xContent = xContent.Replace("xmlns:xsi=", "");
                    xContent = xContent.Replace("\"http://www.w3.org/2001/XMLSchema-instance\"", "");
                    xContent = xContent.Replace("xmlns:xsd=", "");
                    xContent = xContent.Replace("\"http://www.w3.org/2001/XMLSchema\"", "");
                    xContent = xContent.Replace("evtMonit", "evtCAT");
                    //xContent = xContent.Replace("evtCAT", "evtAfastTemp");
                    //xContent = xContent.Replace("v_S_01_02_00", "v_S_01_02_00:eSocial_2210");
                    xContent = xContent.Replace("v_S_01_02_00:eSocial_2210", "v_S_01_02_00");
                    xContent = xContent.Replace("eSocial_2210", "eSocial");

                    //salvar XML no SQL server com xeSocial.Id e nome do arquivo criado e datahora 
                    Int64 zPis = 0;



                    Ilitera.Data.eSocial zeSocial = new Ilitera.Data.eSocial();

                    if (zCod == "")
                    {
                        zeSocial.XML_Deposito(xArq, xeSocial.Id, zCont, xContent, rDs.Tables[0].Rows[zCont]["CPF"].ToString(), zPis, rDs.Tables[0].Rows[zCont]["CNPJ"].ToString(), zD1, zD2, System.Convert.ToInt32(rDs.Tables[0].Rows[0]["IdAcidente"].ToString()), xCat.Id,0);
                    }
                    else
                    {
                        zeSocial.XML_Deposito(zCod, xArq, xeSocial.Id, zCont, xContent, rDs.Tables[0].Rows[zCont]["CPF"].ToString(), zPis, rDs.Tables[0].Rows[zCont]["CNPJ"].ToString(), zD1, zD2, System.Convert.ToInt32(rDs.Tables[0].Rows[0]["IdAcidente"].ToString()), xCat.Id,0);

                        //Entities.Usuario xUser = (Entities.Usuario)Session["usuarioLogado"];
                        //tbleSocial_Envio zEnvio = new tbleSocial_Envio();
                        //zEnvio.IdeSocial_Deposito = System.Convert.ToInt32(zCod);
                        //zEnvio.IdUsuario = xUser.IdUsuario;
                        //zEnvio.Data_Envio = System.DateTime.Now;
                        //zEnvio.Tipo_Envio = "R";  //recriado
                        //zEnvio.Processamento_Lote = "";
                        //zEnvio.Save();
                    }

                }
                catch (Exception ex)
                {
                    MsgBox1.Show("Ilitera.Net", "Erro:" + ex.ToString(), null, new EO.Web.MsgBoxButton("OK"));
                    return;
                }



            }



            //MsgBox1.Show("Ilitera.Net", "Evento criado.", null, new EO.Web.MsgBoxButton("OK"));
            return;
        }



        private string ConvertWesternEuropeanToASCII(string str)
        {
            return Encoding.ASCII.GetString(Encoding.GetEncoding(1251).GetBytes(str));
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

            string rTpInscr = "1";

            if (resultString.Length == 11)
            {
                rTpInscr = "2";
            }



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

            if ((DateTime.Now.Day) < 10)
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

            //xContador = xCont.ToString().Trim();
            Ilitera.Data.eSocial zEsocial = new Ilitera.Data.eSocial();
            xContador = zEsocial.Trazer_Cont_ID("ID" + rTpInscr + resultString + xData).ToString();


            if (xContador.Length < 5)
            {
                string Rep = new string('0', 5 - xContador.Length);
                xContador = Rep + xContador;
            }

            return "ID" + rTpInscr + resultString + xData + xContador;

        }


        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]
        protected void cmd_Enviar_Click(object sender, EventArgs e)
        {
            //int zCont = 0;

            //object zsender = new object();
            //EventArgs ze = new EventArgs();
            //cmd_Fechar_Det_Click(zsender, ze);


            //foreach (EO.Web.GridItem item in grd_eSocial.CheckedItems)
            //{
            //    //item.Deleted = true;
            //    string zCod = item.Cells[0].Value.ToString();


            //    if (zCod.Trim() == "") //não foi processado, não pode enviar
            //    {
            //        MsgBox1.Show("Ilitera.Net", "Existem registros não processados selecionados.  Criar xml primeiro.", null,
            //                                    new EO.Web.MsgBoxButton("OK"));
            //        return;
            //    }

            //    zCont++;
            //}


            //if (zCont == 0)
            //{
            //    MsgBox1.Show("Ilitera.Net", "Selecionar pelo menos 1 registro.", null,
            //                new EO.Web.MsgBoxButton("OK"));
            //    return;
            //}


            //X509Certificate2 cert;

            //try
            //{
            //    string web_service_teste = "https://webservices.producaorestrita.esocial.gov.br/servicos/empregador/enviarloteeventos/WsEnviarLoteEventos.svc";

            //    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;
            //    string url = web_service_teste;
            //    //string response = "";

            //    //X509Certificate2 cert = new X509Certificate2(@"D:\Projetos\certificados\xxxxx.pfx", "xxxxx");

            //    var oX509Cert = new X509Certificate2();
            //    var store = new X509Store("MY", StoreLocation.CurrentUser);
            //    store.Open(OpenFlags.ReadOnly | OpenFlags.OpenExistingOnly);
            //    var collection = store.Certificates;
            //    var collection1 = collection.Find(X509FindType.FindByTimeValid, DateTime.Now, false);
            //    var collection2 = collection.Find(X509FindType.FindByKeyUsage, X509KeyUsageFlags.DigitalSignature, true);
            //    var scollection = X509Certificate2UI.SelectFromCollection(collection2,
            //        "Certificado(s) Digital(is) disponível(is)", "Selecione o certificado digital para uso no aplicativo",
            //        X509SelectionFlag.MultiSelection);

            //    if (scollection.Count == 0)
            //    {
            //        var msgResultado =
            //            "Nenhum certificado digital foi selecionado ou o certificado selecionado está com problemas.";
            //        MsgBox1.Show("Ilitera.Net", msgResultado, null,
            //        new EO.Web.MsgBoxButton("OK"));
            //        return;
            //    }
            //    else
            //    {
            //        oX509Cert = scollection[0];
            //        cert = oX509Cert;
            //        //txt_Status.Text = txt_Status.Text + oCertificado.IssuerName.Name + " - " + oCertificado.SerialNumber + " - " + oCertificado.NotBefore + " à " + oCertificado.NotAfter + System.Environment.NewLine;
            //    }


            //    string xCNPJ_Certificado = Regex.Replace(cert.SubjectName.Name.Substring(cert.SubjectName.Name.IndexOf(":")+1, 14), @"[^\d]", "");





            //    foreach (EO.Web.GridItem item in grd_eSocial.CheckedItems)
            //    {
            //        //item.Deleted = true;
            //        string zCod = item.Cells[0].Value.ToString();

               
            //        //criar lote e enviar


            //        WsEnviar.ServicoEnviarLoteEventos xEnviar = new WsEnviar.ServicoEnviarLoteEventos();
            //        xEnviar.ClientCertificates.Add(cert);




            //        //preciso obter o XML, e pegar o ID e o nrInsc

            //        XmlDocument xDoc = new XmlDocument();

            //        //abrir janela com XML
            //        Ilitera.Data.eSocial xDados = new Ilitera.Data.eSocial();

            //        DataSet rDs = new DataSet();
            //        rDs = xDados.Trazer_XML2(System.Convert.ToInt32(zCod));

            //        string strXML = rDs.Tables[0].Rows[0][0].ToString().Replace("\r\n", string.Empty);
            //        //string strXML = Validar_Caracteres_XML( rDs.Tables[0].Rows[0][0].ToString().Replace("\r\n", string.Empty).Replace("\"", string.Empty));
            //        //string strXML = rDs.Tables[0].Rows[0][0].ToString();


            //        //xDoc.LoadXml(Server.HtmlDecode( strXML) );
            //        //xDoc.Load(strXML);


            //        byte[] encodedString = Encoding.UTF8.GetBytes(strXML);

            //        // Put the byte array into a stream and rewind it to the beginning
            //        MemoryStream ms = new MemoryStream(encodedString);
            //        ms.Flush();
            //        ms.Position = 0;


            //        xDoc.Load(ms);


            //        XmlDocument xAssinado = new XmlDocument();
            //        xAssinado = assinarXML(xDoc, cert, "evtCAT", "Id");


            //        //montar cabeçalho e rodapé
            //        string zId = "";
            //        string zNrInsc = "";

            //        zId = xAssinado.InnerXml.Substring(xAssinado.InnerXml.IndexOf(" Id=") + 5, 36);
            //        zNrInsc = xAssinado.InnerXml.Substring(xAssinado.InnerXml.IndexOf("<nrInsc>") + 8, 8);

            //        string xCabecalho = @"<eSocial xmlns = ""http://www.esocial.gov.br/schema/lote/eventos/envio/v1_1_1""><envioLoteEventos grupo = ""2""><ideEmpregador><tpInsc>1</tpInsc><nrInsc>03844493000134</nrInsc></ideEmpregador><ideTransmissor><tpInsc>1</tpInsc><nrInsc>03844493000134</nrInsc></ideTransmissor><eventos><evento Id = ""ID1038444930001342019032110270800011"" >";
            //        //string xCabecalho = @"<eSocial xmlns = ""http://www.esocial.gov.br/schema/lote/eventos/envio/v1_1_1""><envioLoteEventos grupo = ""1""><ideEmpregador><tpInsc>1</tpInsc><nrInsc>" + zNrInsc + "</nrInsc></ideEmpregador><ideTransmissor><tpInsc>1</tpInsc><nrInsc>03844493000134</nrInsc></ideTransmissor><eventos><evento Id = " + "" + zId  +  "" + " >";


            //        xCabecalho = xCabecalho.Replace("ID1038444930001342019032110270800011", zId);
            //        xCabecalho = xCabecalho.Replace("<ideEmpregador><tpInsc>1</tpInsc><nrInsc>03844493000134</nrInsc></ideEmpregador>", "<ideEmpregador><tpInsc>1</tpInsc><nrInsc>" + zNrInsc + "</nrInsc></ideEmpregador>");
            //        xCabecalho = xCabecalho.Replace("<ideTransmissor><tpInsc>1</tpInsc><nrInsc>03844493000134</nrInsc></ideTransmissor>", "<ideTransmissor><tpInsc>1</tpInsc><nrInsc>" + xCNPJ_Certificado + "</nrInsc></ideTransmissor>");


            //        //rodapé
            //        string xRodape = "</evento></eventos></envioLoteEventos></eSocial>";

            //        //adicionar evento dentro do cabeçalho e rodapé
            //        XmlDocument xLoteFinal = new XmlDocument();

            //        //string xLoteF = @"<?xml version=""1.0"" encoding=""UTF-8""?>" + xCabecalho + xAssinado.InnerXml.Replace(@"<?xml version=\""1.0\"" encoding=\""utf-8\""?>", string.Empty) + xRodape;
            //        string xLoteF = @"<?xml version=""1.0"" encoding=""UTF-8""?>" + xCabecalho + xAssinado.InnerXml.Substring(38) + xRodape;


            //        byte[] encodedString2 = Encoding.UTF8.GetBytes(xLoteF);

            //        MemoryStream ms2 = new MemoryStream(encodedString2);
            //        ms2.Flush();
            //        ms2.Position = 0;

            //        XmlDocument zLote = new XmlDocument();
            //        zLote.Load(ms2);




            //        //TESTAR ENVIO

            //        XmlElement xRet;

            //        xRet = xEnviar.EnviarLoteEventos(zLote.DocumentElement);
            //        String xRet2 = xRet.InnerXml;

            //        //Retorna na estrutura abaixo, quando vai certo volta o protocolo:
            //        //<retornoEnvioLoteEventos xmlns=\"http://www.esocial.gov.br/schema/lote/eventos/envio/retornoEnvio/v1_1_0\"><ideEmpregador><tpInsc>1</tpInsc><nrInsc>03844493000134</nrInsc></ideEmpregador><ideTransmissor><tpInsc>1</tpInsc><nrInsc>03844493000134</nrInsc></ideTransmissor><status><cdResposta>201</cdResposta><descResposta>Lote Recebido com Sucesso.</descResposta></status><dadosRecepcaoLote><dhRecepcao>2019-03-25T14:32:49.457</dhRecepcao><versaoAplicativoRecepcao>0.1.0-A0350</versaoAplicativoRecepcao><protocoloEnvio>1.2.201903.0000000000084410135</protocoloEnvio></dadosRecepcaoLote></retornoEnvioLoteEventos>
            //        //transformar em formato XML e salvar no SQL, como histórico do envio

            //        //salvar em tbl_eSocial_Envio->Processamento_Lote
            //        //vai ter opção de abrir como XML no sistema web
            //        Entities.Usuario xUser = (Entities.Usuario)Session["usuarioLogado"];

            //        tbleSocial_Envio zEnvio = new tbleSocial_Envio();
            //        zEnvio.IdeSocial_Deposito = System.Convert.ToInt32(zCod);
            //        zEnvio.IdUsuario = xUser.IdUsuario;
            //        zEnvio.Data_Envio = System.DateTime.Now;
            //        zEnvio.Tipo_Envio = "W";  //web
            //        zEnvio.Processamento_Lote = xRet.InnerXml;
            //        zEnvio.Save();


            //        if (xRet2.ToUpper().IndexOf("LOTE RECEBIDO COM SUCESSO") > 0)
            //        {

            //            //pegar protocolo e substituir no texto abaixo
            //            string zProtocolo = "";

            //            zProtocolo = xRet.InnerXml.Substring(xRet.InnerXml.IndexOf("<protocoloEnvio>") + 16, 30);

            //            zEnvio.Protocolo = zProtocolo;
            //            zEnvio.Save();

            //            XmlDocument xDoc2 = new XmlDocument();
            //            //pegar retorno
            //            string zRet = @"<?xml version=""1.0"" encoding=""UTF-8""?><eSocial xmlns=""http://www.esocial.gov.br/schema/lote/eventos/envio/consulta/retornoProcessamento/v1_0_0""><consultaLoteEventos><protocoloEnvio>" + zProtocolo + "</protocoloEnvio></consultaLoteEventos></eSocial> ";
            //            //xDoc2.Load("I:\\Desenv\\Ilitera\\Mestra\\Mestra.Viewer\\eSocial\\Consulta_Lote_Teste_1060.XML");

            //            byte[] encodedString3 = Encoding.UTF8.GetBytes(zRet);

            //            MemoryStream ms3 = new MemoryStream(encodedString3);
            //            ms3.Flush();
            //            ms3.Position = 0;

            //            xDoc2.Load(ms3);



            //            WsConsultar.ServicoConsultarLoteEventos xConsulta = new WsConsultar.ServicoConsultarLoteEventos();
            //            xConsulta.ClientCertificates.Add(cert);
            //            XmlElement xRet3 = xConsulta.ConsultarLoteEventos(xDoc2.DocumentElement);

            //            string xRet4 = xRet3.InnerXml;
            //            //transformar em formato XML e salvar no SQL, como histórico do envio

            //            XmlDocument xDoc20 = new XmlDocument();

            //            byte[] encodedString4 = Encoding.UTF8.GetBytes(xRet4);

            //            MemoryStream ms4 = new MemoryStream(encodedString4);
            //            ms4.Flush();
            //            ms4.Position = 0;

            //            xDoc20.Load(ms4);


            //            //salvar em tbl_eSocial_Envio->Processamento_Retorno
            //            //vai ter opção de abrir como XML no sistema web
            //            zEnvio.Processamento_Retorno = xDoc20.InnerXml;
            //            zEnvio.Save();


            //            //xDoc20.Load(xRet4);

            //            //XDocument doc = XDocument.Parse(xDoc20.InnerXml);

            //            //preciso testar loop para vários registros no mesmo XML

            //            //var points = doc.Root.Descendants();

            //            //string xDescricao = "";
            //            //string xCodigo = "";

            //            //    foreach (XElement current in points)
            //            //    {
            //            //        try
            //            //        {
            //            //            if (current.Name.LocalName == "descricao") xDescricao = current.Value;
            //            //            if (current.Name.LocalName == "codigo") xCodigo = current.Value;
            //            //        }
            //            //        catch (Exception ex)
            //            //        {
            //            //            MsgBox1.Show("Ilitera.Net", "Erro(2): " + ex.Message, null,
            //            //            new EO.Web.MsgBoxButton("OK"));
            //            //            return;
            //            //        }
            //            //        finally
            //            //        {

            //            //        }

            //            //        //salvar no SQL Server
            //            //        //tbl_eSocial_Envio->Status_Retorno - talvez tenha que colocar esse campo em uma tabela à parte, para que um ideSocial_Deposito tenha vários retornos
            //            //    }



            //        }


            //        //para Lote-esse é corpo a ser adicionado, e o evento vai dentro.  
            //        // <? xml version = "1.0" encoding = "UTF-8" ?>
            //        // < eSocial xmlns = "http://www.esocial.gov.br/schema/lote/eventos/envio/v1_1_1" >
            //        // < envioLoteEventos grupo = "1" >
            //        //    < ideEmpregador >
            //        //     < tpInsc > 1 </ tpInsc >
            //        //     < nrInsc > 03844493000134 </ nrInsc >
            //        //    </ ideEmpregador >    
            //        //    < ideTransmissor >    
            //        //     < tpInsc > 1 </ tpInsc >    
            //        //     < nrInsc > 03844493000134 </ nrInsc >    
            //        //    </ ideTransmissor >    
            //        //    < eventos >    
            //        //      < evento Id = "ID1038444930001342019032110270800001" >     
            //        //        < eSocial xmlns = "http://www.esocial.gov.br/schema/evt/evtTabAmbiente/v02_05_00" >


            //        //Dados do Evento 1060 - nesse caso     


            //        //        </ eSocial >
            //        //      </ evento >
            //        //   </ eventos >
            //        //  </ envioLoteEventos >
            //        //</ eSocial >






            //    }

            //}
            //catch (Exception ex)
            //{
            //    MsgBox1.Show("Ilitera.Net", "Erro(1): " + ex.Message, null,
            //    new EO.Web.MsgBoxButton("OK"));
            //    return;

            //}
            //finally
            //{
            //    MsgBox1.Show("Ilitera.Net", "Processamento Finalizado.", null,
            //    new EO.Web.MsgBoxButton("OK"));

            //    Carga_Grid();
            //}

            //return;



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


        public string Validar_Caracteres_XML2(string message)
        {
            var pat = @"<(?![/a-zA-Z0-9]*[_/a-zA-Z0-9]*>)";
            return Regex.Replace(message, pat, string.Empty);
        }


        public string Validar_Caracteres_XML(string text)
        {
            // From xml spec valid chars: 
            // #x9 | #xA | #xD | [#x20-#xD7FF] | [#xE000-#xFFFD] | [#x10000-#x10FFFF]     
            // any Unicode character, excluding the surrogate blocks, FFFE, and FFFF. 
            string re = @"[^\x09\x0A\x0D\x20-\uD7FF\uE000-\uFFFD\u10000-\u10FFFF]";
            return Regex.Replace(text, re, "");
        }


        protected void cmd_Fechar_Det_Click(object sender, EventArgs e)
        {
            grd_eSocial.Height = 485;
            lst_Detalhes.Visible = false;
            cmd_Fechar_Det.Visible = false;
            cmd_Proc_Evento.Visible = false;
            cmd_Proc_Lote.Visible = false;
            cmd_Proc_Atualizar.Visible = false;
        }

        protected void cmd_Proc_Lote_Click(object sender, EventArgs e)
        {

            Ilitera.Data.eSocial xDados = new Ilitera.Data.eSocial();

            DataSet rDs = new DataSet();
            rDs = xDados.Trazer_Lote(System.Convert.ToInt32(lst_Id_Detalhes.Items[lst_Detalhes.SelectedIndex].ToString()));

            string strXML = rDs.Tables[0].Rows[0][0].ToString();

            Session["XML"] = strXML;

            string redirect = "<script>window.open('ViewXML.aspx', '_blank', 'location=yes,height=570,width=900,scrollbars=yes,status=yes');</script>";
            Response.Write(redirect);

        }


        protected void cmd_Proc_Evento_Click(object sender, EventArgs e)
        {

            Ilitera.Data.eSocial xDados = new Ilitera.Data.eSocial();

            DataSet rDs = new DataSet();
            rDs = xDados.Trazer_Evento(System.Convert.ToInt32(lst_Id_Detalhes.Items[lst_Detalhes.SelectedIndex].ToString()));

            string strXML = rDs.Tables[0].Rows[0][0].ToString();

            Session["XML"] = strXML;

            string redirect = "<script>window.open('ViewXML.aspx', '_blank', 'location=yes,height=570,width=900,scrollbars=yes,status=yes');</script>";
            Response.Write(redirect);

        }

        protected void lst_Detalhes_SelectedIndexChanged(object sender, EventArgs e)
        {

            cmd_Proc_Evento.Enabled = false;
            cmd_Proc_Lote.Enabled = false;
            cmd_Proc_Atualizar.Enabled = false;

            if (lst_Detalhes.SelectedIndex < 0) return;

            if (lst_Detalhes.Items[lst_Detalhes.SelectedIndex].ToString().IndexOf("Lote") > 0)
            {
                cmd_Proc_Lote.Enabled = true;                
            }

            if (lst_Detalhes.Items[lst_Detalhes.SelectedIndex].ToString().IndexOf("Retorno") > 0)
            {
                cmd_Proc_Evento.Enabled = true;

                //if (lst_Detalhes.SelectedIndex == 0 )
                //    cmd_Proc_Atualizar.Enabled = true;

            }

        }

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]
        protected void cmd_Proc_Proc_Atualizar_Click(object sender, EventArgs e)
        {
            //if (lst_Detalhes.SelectedIndex < 0)
            //    return;

            //Int32 zId = System.Convert.ToInt32(lst_Id_Detalhes.Items[lst_Detalhes.SelectedIndex].ToString());

            //tbleSocial_Envio bEnvio = new tbleSocial_Envio();
            //bEnvio.Find(zId);

            //if ( bEnvio.Id == 0)
            //{
            //    MsgBox1.Show("Ilitera.Net", "Não foi possível obter o protocolo deste envio (1)",  null,
            //                                new EO.Web.MsgBoxButton("OK"));
            //    return;
            //}

            //if ( bEnvio.Protocolo.Trim()=="")
            //{
            //    MsgBox1.Show("Ilitera.Net", "Não foi possível obter o protocolo deste envio (2)", null,
            //                                new EO.Web.MsgBoxButton("OK"));
            //    return;
            //}



            ////fazer envio para obter novo processamento do lote

            //try
            //{

            //    X509Certificate2 cert;


            //    string web_service_teste = "https://webservices.producaorestrita.esocial.gov.br/servicos/empregador/enviarloteeventos/WsEnviarLoteEventos.svc";

            //    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;
            //    string url = web_service_teste;
            //    //string response = "";

            //    //X509Certificate2 cert = new X509Certificate2(@"D:\Projetos\certificados\xxxxx.pfx", "xxxxx");

            //    var oX509Cert = new X509Certificate2();
            //    var store = new X509Store("MY", StoreLocation.CurrentUser);
            //    store.Open(OpenFlags.ReadOnly | OpenFlags.OpenExistingOnly);
            //    var collection = store.Certificates;
            //    var collection1 = collection.Find(X509FindType.FindByTimeValid, DateTime.Now, false);
            //    var collection2 = collection.Find(X509FindType.FindByKeyUsage, X509KeyUsageFlags.DigitalSignature, true);
            //    var scollection = X509Certificate2UI.SelectFromCollection(collection2,
            //        "Certificado(s) Digital(is) disponível(is)", "Selecione o certificado digital para uso no aplicativo",
            //        X509SelectionFlag.MultiSelection);

            //    if (scollection.Count == 0)
            //    {
            //        var msgResultado =
            //            "Nenhum certificado digital foi selecionado ou o certificado selecionado está com problemas.";
            //        MsgBox1.Show("Ilitera.Net", msgResultado, null,
            //        new EO.Web.MsgBoxButton("OK"));
            //        return;
            //    }
            //    else
            //    {
            //        oX509Cert = scollection[0];
            //        cert = oX509Cert;
            //        //txt_Status.Text = txt_Status.Text + oCertificado.IssuerName.Name + " - " + oCertificado.SerialNumber + " - " + oCertificado.NotBefore + " à " + oCertificado.NotAfter + System.Environment.NewLine;
            //    }



            //    XmlDocument xDoc2 = new XmlDocument();
            //    //pegar retorno
            //    string zRet = @"<?xml version=""1.0"" encoding=""UTF-8""?><eSocial xmlns=""http://www.esocial.gov.br/schema/lote/eventos/envio/consulta/retornoProcessamento/v1_0_0""><consultaLoteEventos><protocoloEnvio>" + bEnvio.Protocolo.Trim() + "</protocoloEnvio></consultaLoteEventos></eSocial> ";
            //    //xDoc2.Load("I:\\Desenv\\Ilitera\\Mestra\\Mestra.Viewer\\eSocial\\Consulta_Lote_Teste_1060.XML");

            //    byte[] encodedString3 = Encoding.UTF8.GetBytes(zRet);

            //    MemoryStream ms3 = new MemoryStream(encodedString3);
            //    ms3.Flush();
            //    ms3.Position = 0;

            //    xDoc2.Load(ms3);



            //    WsConsultar.ServicoConsultarLoteEventos xConsulta = new WsConsultar.ServicoConsultarLoteEventos();
            //    xConsulta.ClientCertificates.Add(cert);
            //    XmlElement xRet3 = xConsulta.ConsultarLoteEventos(xDoc2.DocumentElement);

            //    string xRet4 = xRet3.InnerXml;
            //    //transformar em formato XML e salvar no SQL, como histórico do envio

            //    XmlDocument xDoc20 = new XmlDocument();

            //    byte[] encodedString4 = Encoding.UTF8.GetBytes(xRet4);

            //    MemoryStream ms4 = new MemoryStream(encodedString4);
            //    ms4.Flush();
            //    ms4.Position = 0;

            //    xDoc20.Load(ms4);


            //    //salvar em tbl_eSocial_Envio->Processamento_Retorno
            //    //vai ter opção de abrir como XML no sistema web
            //    bEnvio.Processamento_Retorno = xDoc20.InnerXml;
            //    bEnvio.Save();

            //}
            //catch (Exception ex)
            //{
            //    MsgBox1.Show("Ilitera.Net", "Erro na atualização do Status do Processamento do lote: " + ex.Message, null,
            //                    new EO.Web.MsgBoxButton("OK"));

            //}
            //finally
            //{
            //    MsgBox1.Show("Ilitera.Net", "Status do Processamento do lote atualizado.", null,
            //                    new EO.Web.MsgBoxButton("OK"));
            //}

            //return;


        }

        protected void cmd_Baixar_Click(object sender, EventArgs e)
        {


            int zCont = 0;

            object zsender = new object();
            EventArgs ze = new EventArgs();
            cmd_Fechar_Det_Click(zsender, ze);

            lst_Arq.Items.Clear();


            foreach (EO.Web.GridItem item in grd_eSocial.CheckedItems)
            {
                //item.Deleted = true;
                string zCod = item.Cells[0].Value.ToString();


                if (zCod.Trim() == "") //não foi processado, não pode enviar
                {
                    MsgBox1.Show("Ilitera.Net", "Existem registros não processados selecionados.  Criar xml primeiro.", null,
                                                new EO.Web.MsgBoxButton("OK"));
                    return;
                }

                zCont++;
            }


            if (zCont == 0)
            {
                MsgBox1.Show("Ilitera.Net", "Selecionar pelo menos 1 registro.", null,
                            new EO.Web.MsgBoxButton("OK"));
                return;
            }



            zCont = 1;

            foreach (EO.Web.GridItem item in grd_eSocial.CheckedItems)
            {
                //item.Deleted = true;
                string zCod = item.Cells[0].Value.ToString();
                //string xCodAmb = item.Cells[5].Value.ToString().Trim();
                //string zCPF = item.Cells["CPF"].Value.ToString().Trim();

                XmlDocument xDoc = new XmlDocument();

                Ilitera.Data.eSocial xDados = new Ilitera.Data.eSocial();

                DataSet rDs = new DataSet();
                rDs = xDados.Trazer_XML2(System.Convert.ToInt32(zCod));

                string strXML = rDs.Tables[0].Rows[0][0].ToString().Replace("\r\n", string.Empty);

                if (cliente.ESocial_Download_Cabecalho == false)
                    strXML = strXML.Replace("xmlns=\"http://www.esocial.gov.br/schema/evt/evtCAT/v_S_01_02_00\"", string.Empty);


                byte[] encodedString = Encoding.UTF8.GetBytes(strXML);

                // Put the byte array into a stream and rewind it to the beginning
                MemoryStream ms = new MemoryStream(encodedString);
                ms.Flush();
                ms.Position = 0;


                xDoc.Load(ms);

                string xArq = "I:\\temp\\2210_" + zCont.ToString().Trim() + "_" +  DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + "_" + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString() + ".XML";

                xDoc.Save(xArq);

                lst_Arq.Items.Add(xArq);
                zCont++;


            }



            using (Ionic.Zip.ZipFile zip = new Ionic.Zip.ZipFile())
            {
                zip.AlternateEncodingUsage = Ionic.Zip.ZipOption.AsNecessary;
                zip.AddDirectoryByName("Files");

                for (int rCont = 0; rCont < lst_Arq.Items.Count; rCont++)
                {
                    zip.AddFile(lst_Arq.Items[rCont].Text, "Files");
                }
                Response.Clear();
                Response.BufferOutput = false;
                string zipName = String.Format("eSocial2210_Zip_{0}.zip", DateTime.Now.ToString("yyyy-MMM-dd-HHmmss"));
                Response.ContentType = "application/zip";
                Response.AddHeader("content-disposition", "attachment; filename=" + zipName);
                zip.Save(Response.OutputStream);
                Response.End();
            }



            MsgBox1.Show("Ilitera.Net", "Arquivo(s) criados!", null, new EO.Web.MsgBoxButton("OK"));
            return;
        }

        protected void txt_Data_TextChanged(object sender, EventArgs e)
        {
            Carga_Grid();
        }

        protected void txt_Data2_TextChanged(object sender, EventArgs e)
        {
            Carga_Grid();
        }

        protected void txt_Nome_TextChanged(object sender, EventArgs e)
        {
            Carga_Grid();
        }

        protected void chk_Grupo_CheckedChanged(object sender, EventArgs e)
        {
            Carga_Grid();
        }

        protected void cmd_Marcar_Todos_Click(object sender, EventArgs e)
        {
            foreach (EO.Web.GridItem item in grd_eSocial.Items)
            {
                item.Cells[1].Value = true;
            }
        }

        protected void cmd_Agendar_Envio_Click(object sender, EventArgs e)
        {

            int zCont = 0;

            object zsender = new object();
            EventArgs ze = new EventArgs();
            cmd_Fechar_Det_Click(zsender, ze);


            foreach (EO.Web.GridItem item in grd_eSocial.CheckedItems)
            {
                //item.Deleted = true;
                string zCod = item.Cells[0].Value.ToString();


                if (zCod.Trim() == "") //não foi processado, não pode enviar
                {
                    MsgBox1.Show("Ilitera.Net", "Existem registros não processados selecionados.  Criar xml primeiro.", null,
                                                new EO.Web.MsgBoxButton("OK"));
                    return;
                }


                //se foi já foi enviado não permitir reagendar, a não ser que seja retificação, mas ainda preciso pensar nessa parte
                zCod = item.Cells[14].Value.ToString();

                if (zCod.Trim().ToUpper().IndexOf("RECIBO GERADO") > 0)  //já foi enviado, não pode agendar
                {
                    MsgBox1.Show("Ilitera.Net", "Existem registros já enviados que não podem ser agendados. ", null,
                                                new EO.Web.MsgBoxButton("OK"));
                    return;
                }


                zCont++;
            }


            if (zCont == 0)
            {
                MsgBox1.Show("Ilitera.Net", "Selecionar pelo menos 1 registro.", null,
                            new EO.Web.MsgBoxButton("OK"));
                return;
            }



            Entities.Usuario user = (Entities.Usuario)Session["usuarioLogado"];

            foreach (EO.Web.GridItem item in grd_eSocial.CheckedItems)
            {

                string zCod = item.Cells[0].Value.ToString();


                string zAgendado = item.Cells[15].Value.ToString();

                if (zAgendado.Trim() != "X")
                {

                    Entities.Usuario zUser = (Entities.Usuario)Session["usuarioLogado"];


                    //criar registro em tbl_eSocial_Schedule
                    Ilitera.Data.eSocial xSched = new Ilitera.Data.eSocial();
                    xSched.Criar_Agendamento_Evento(System.Convert.ToInt32(zCod), zUser.IdUsuario);

                }


            }

            Carga_Grid();


        }

        protected void cmd_Gerar_CSV_Click(object sender, EventArgs e)
        {

            if (Validar_Data(txt_Data.Text.Trim()) == false)
            {
                MsgBox1.Show("Ilitera.Net", "Data Inicial de Filtro inválida!", null,
                      new EO.Web.MsgBoxButton("OK"));
                return;
            }

            if (Validar_Data(txt_Data2.Text.Trim()) == false)
            {
                MsgBox1.Show("Ilitera.Net", "Data Final de Filtro inválida!", null,
                      new EO.Web.MsgBoxButton("OK"));
                return;
            }

            string zTipo_Carga = "4";

            if (cmb_Carga.SelectedIndex == 1) zTipo_Carga = "1";
            else if (cmb_Carga.SelectedIndex == 2) zTipo_Carga = "0";
            else if (cmb_Carga.SelectedIndex == 3) zTipo_Carga = "2";
            else if (cmb_Carga.SelectedIndex == 4) zTipo_Carga = "3";

            string zGrupo = "N";

            if (chk_Grupo.Checked == true)
            {
                zGrupo = "S";
            }
            else
            {
                zGrupo = "N";
            }

            Entities.Usuario xUser = (Entities.Usuario)Session["usuarioLogado"];

            DataSet zDs = new DataSet();
            Ilitera.Data.eSocial xLista = new Ilitera.Data.eSocial();
            zDs = xLista.Mensageria_2210_CSV(Convert.ToInt32(Request.QueryString["IdEmpresa"]), 0, 0, txt_Data.Text, txt_Data2.Text, xUser.IdUsuario, zTipo_Carga, txt_Nome.Text, zGrupo, cliente.ESocial_Ambiente);

            string xFile = "eSocial2210_" + System.DateTime.Now.Second.ToString().Trim() + System.DateTime.Now.Millisecond.ToString().Trim() + System.DateTime.Now.Minute.ToString().Trim() + System.DateTime.Now.Day.ToString().Trim() + System.DateTime.Now.Month.ToString().Trim() + ".csv";
            string myStringWebResource = "I:\\temp\\" + xFile;

            string zLinha = "";



            TextWriter tw = new StreamWriter(myStringWebResource, false, Encoding.GetEncoding(1252));


            zLinha = "CPF;Reabertura;CNPJ;Empresa;Colaborador;Dt_Acidente;Matricula;Status_Envio";
            tw.WriteLine(zLinha);

            for (int zCont = 0; zCont < zDs.Tables[0].Rows.Count; zCont++)
            {
                zLinha = "";

                for (int zAux = 0; zAux < zDs.Tables[0].Columns.Count; zAux++)
                {
                    if (zAux == 0 || zAux == 1 || zAux == 2 || zAux == 3 || zAux == 4 || zAux == 5 || zAux == 6 || zAux == 8)
                    {
                        //   zLinha = zLinha + zDs.Tables[0].Rows[zCont][zAux].ToString().Trim() + ";";
                        string rTexto = zDs.Tables[0].Rows[zCont][zAux].ToString().Trim();
                        if (rTexto.Length > 12 && rTexto.All(char.IsDigit))
                            zLinha = zLinha + "'" + rTexto + ";";
                        else
                            zLinha = zLinha + rTexto + ";";

                    }
                }

                tw.WriteLine(zLinha);
            }

            tw.Close();


            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            String Header = "Attachment; Filename=" + xFile;
            HttpContext.Current.Response.AppendHeader("Content-Disposition", Header);
            System.IO.FileInfo Dfile = new System.IO.FileInfo(myStringWebResource); //HttpContext.Current.Server.MapPath(myStringWebResource));
            HttpContext.Current.Response.WriteFile(Dfile.FullName);
            HttpContext.Current.Response.End();

            MsgBox1.Show("Ilitera.Net", "Arquivo gerado.", null,
            new EO.Web.MsgBoxButton("OK"));


            return;


        }

        public static String GerarCpf(string zCAEPF)
        {
            int soma = 0, resto = 0;
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            string rCPF = "";

            for (int i = 0; i < 9; i++)
                soma += int.Parse(zCAEPF[i].ToString()) * multiplicador1[i];

            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            rCPF = zCAEPF + resto;
            soma = 0;

            for (int i = 0; i < 10; i++)
                soma += int.Parse(rCPF[i].ToString()) * multiplicador2[i];

            resto = soma % 11;

            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            rCPF = rCPF + resto;
            return rCPF;
        }



    }


}



    

