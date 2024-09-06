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
    public partial class Controle_eSocial_2220 : System.Web.UI.Page
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
                    throw new Exception("Formul�rio n�o cadastrado - " + FormKey);

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
            ////}


            InicializaWebPageObjects();


            cliente = new Cliente(System.Convert.ToInt32(Session["Empresa"].ToString()));

            if (cliente.Bloquear_2220 == true)
            {
                Session["ExceptionType"] = "Restri��o de acesso";
                Session["Source"] = "Mensageria 2220";
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
                    MsgBox1.Show("Ilitera.Net", "XML Pendente de cria��o", null,
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
                    MsgBox1.Show("Ilitera.Net", "Sem detalhes para XML pendente de cria��o", null,
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
                        lst_Detalhes.Items.Add(zDs.Tables[0].Rows[zCont][3].ToString().Trim() + "   " + zDs.Tables[0].Rows[zCont][4].ToString().Trim() + "  XML Recriado em " + zDs.Tables[0].Rows[zCont][1].ToString().Trim() + "  por usu�rio " + zDs.Tables[0].Rows[zCont][2].ToString().Trim());
                    }
                    else
                    {
                        lst_Detalhes.Items.Add(zDs.Tables[0].Rows[zCont][3].ToString().Trim() + "   " + zDs.Tables[0].Rows[zCont][4].ToString().Trim() + "  Criado em " + zDs.Tables[0].Rows[zCont][1].ToString().Trim() + "  por usu�rio " + zDs.Tables[0].Rows[zCont][2].ToString().Trim());
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

                if (zCod.Trim() == "") //n�o foi processado ainda, n�o tem IdeSocial_Deposito

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
                MsgBox1.Show("Ilitera.Net", "Data Inv�lida.  Utilizar formato dd/MM/yyyy", null,
                new EO.Web.MsgBoxButton("OK"));
                return false;
            }

            Validar = zData.Substring(0, 2);
            isNumerical = int.TryParse(Validar, out myInt);
            if (isNumerical == false)
            {
                MsgBox1.Show("Ilitera.Net", "Dia Inv�lido.  Utilizar formato dd/MM/yyyy", null,
                new EO.Web.MsgBoxButton("OK"));
                return false;
            }


            Validar = zData.Substring(3, 2);
            isNumerical = int.TryParse(Validar, out myInt);
            if (isNumerical == false)
            {
                MsgBox1.Show("Ilitera.Net", "M�s Inv�lido.  Utilizar formato dd/MM/yyyy", null,
                new EO.Web.MsgBoxButton("OK"));
                return false;
            }


            Validar = zData.Substring(6, 4);
            isNumerical = int.TryParse(Validar, out myInt);
            if (isNumerical == false)
            {
                MsgBox1.Show("Ilitera.Net", "Ano Inv�lido.  Utilizar formato dd/MM/yyyy", null,
                new EO.Web.MsgBoxButton("OK"));
                return false;
            }


            if (zData.Substring(2, 1) != "/" || zData.Substring(5, 1) != "/")
            {
                MsgBox1.Show("Ilitera.Net", "Data Inv�lida.  Utilizar formato dd/MM/yyyy", null,
                new EO.Web.MsgBoxButton("OK"));
                return false;
            }


            zDia = System.Convert.ToInt32(zData.Substring(0, 2));
            zMes = System.Convert.ToInt32(zData.Substring(3, 2));
            zAno = System.Convert.ToInt32(zData.Substring(6, 4));

            if (zAno < 1900 || zAno > 2030)
            {
                MsgBox1.Show("Ilitera.Net", "Ano Inv�lido.  Utilizar formato dd/MM/yyyy", null,
                new EO.Web.MsgBoxButton("OK"));
                return false;
            }

            if (zMes < 1 || zMes > 12)
            {
                MsgBox1.Show("Ilitera.Net", "M�s Inv�lido.  Utilizar formato dd/MM/yyyy", null,
                new EO.Web.MsgBoxButton("OK"));
                return false;
            }

            if (zMes == 1 || zMes == 3 || zMes == 5 || zMes == 7 || zMes == 8 || zMes == 10 || zMes == 12)
            {
                if (zDia < 1 || zDia > 31)
                {
                    MsgBox1.Show("Ilitera.Net", "Dia Inv�lido.  Utilizar formato dd/MM/yyyy", null,
                    new EO.Web.MsgBoxButton("OK"));
                    return false;
                }
            }
            else if (zMes == 4 || zMes == 6 || zMes == 9 || zMes == 11)
            {
                if (zDia < 1 || zDia > 30)
                {
                    MsgBox1.Show("Ilitera.Net", "Dia Inv�lido.  Utilizar formato dd/MM/yyyy", null,
                    new EO.Web.MsgBoxButton("OK"));
                    return false;
                }
            }
            else
            {
                if (zDia < 1 || zDia > 29)
                {
                    MsgBox1.Show("Ilitera.Net", "Dia Inv�lido.  Utilizar formato dd/MM/yyyy", null,
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
                MsgBox1.Show("Ilitera.Net", "Data Inicial de Filtro inv�lida!", null,
                      new EO.Web.MsgBoxButton("OK"));
                return;
            }

            if (Validar_Data(txt_Data2.Text.Trim()) == false)
            {
                MsgBox1.Show("Ilitera.Net", "Data Final de Filtro inv�lida!", null,
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
            zDs = xLista.Mensageria_2220(Convert.ToInt32(Request.QueryString["IdEmpresa"]), 0, 0, txt_Data.Text, txt_Data2.Text, xUser.IdUsuario, zTipo_Carga, txt_Nome.Text, zGrupo, cliente.ESocial_Ambiente);
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


                if (zCod.Trim() != "") //foi processado, n�o pode recriar
                {
                    MsgBox1.Show("Ilitera.Net", "Existem registros enviados selecionados. No sistema web n�o � poss�vel recriar XML de registros enviados.", null,
                                                new EO.Web.MsgBoxButton("OK"));
                    return;
                }


                zCod = item.Cells[0].Value.ToString();



                if (zCod.Trim() != "") //foi processado, n�o pode recriar
                {
                    MsgBox1.Show("Ilitera.Net", "Existem registros processados selecionados. No sistema web n�o � poss�vel recriar XML.", null,
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

                string xIdExameBase = item.Cells[11].Value.ToString();
                string xD1 = item.Cells[6].Value.ToString();
                string xD2 = item.Cells[7].Value.ToString();

                if (xD1.Substring(0, 3) == "201" || xD1.Substring(0, 3) == "202")
                    xD1 = xD1.Substring(8, 2) + "/" + xD1.Substring(5, 2) + "/" + xD1.Substring(0, 4);

                if (xD2.Substring(0, 3) == "201" || xD2.Substring(0, 3) == "202")
                    xD2 = xD2.Substring(8, 2) + "/" + xD2.Substring(5, 2) + "/" + xD2.Substring(0, 4);

                //if (zCod.Trim() != "") //foi processado, apagar tblDeposito - inibir isso, acho melhor manter hist�rico completo
                //{
                //
                //    Ilitera.Data.eSocial zeSocial = new Ilitera.Data.eSocial();
                //    zeSocial.XML_Deposito_Excluir(System.Convert.ToInt32(zCod));
                //
                //}


                //criar e aguardar XML e registro tblDeposito
                Processar_S2220(System.Convert.ToInt32(xIdExameBase), xD1, xD2, zCod, user.IdUsuario, zContador, cliente.ESocial_Ambiente);

            }

            Carga_Grid();

        }



        public void Processar_S2220(Int32 zidExameBase, string zD1, string zD2, string zCod, Int32 zIdUsuario, int zContador, string zAmbiente)
        {

            //Int32 zEmpresa = 0;
            //Int32 zColaborador = 0;
            //Int32 zEmpresaGrupo = 0;

            //Int32 znId_Laud_Tec = 0;
            //Int32 znID_PPRA = 0;
            Int32 znId_Empr = 0;

     
            //int zEPI = 0;

            //int xRegs = 0;
            //string xCPF = "";
            //string xHDT_Laudo = "";


            string xArq = "";





            System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");


            //zEmpresa = 0;
            //zEmpresaGrupo = 0;
            //zColaborador = zIdEmpregado;


            //Entities.Usuario user = (Entities.Usuario)Session["usuarioLogado"];

            Ilitera.Data.eSocial zEsocial = new Ilitera.Data.eSocial();

            DataSet rDs = zEsocial.Trazer_2220(zidExameBase);

            if (rDs.Tables[0].Rows.Count < 1)
            {
                MsgBox1.Show("Ilitera.Net", "N�o h� registros para gerar o evento selecionado.", null, new EO.Web.MsgBoxButton("OK"));
                return;
            }


            //criar evento de controle para este lote
            tbleSocial xeSocial = new tbleSocial();
            xeSocial.DataHora_Criacao = System.DateTime.Now;
            xeSocial.Evento = "2220";

            if ( zAmbiente == "1" ) //(cliente.ESocial_Ambiente == "1")  //produ��o
                xeSocial.Ambiente = "1";
            else
                xeSocial.Ambiente = "2";   // 2 se produ��o restrita
            

            xeSocial.IdUsuario = zIdUsuario;//= user.IdUsuario;
            xeSocial.Save();



            for (int zCont = 0; zCont < rDs.Tables[0].Rows.Count; zCont++)
            {

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
                    //xMonit.Id = Gerar_ID(rDs.Tables[0].Rows[zCont]["CNPJ"].ToString().Trim().Substring(0, 14), zCont + zContador + 1);


                    znId_Empr = System.Convert.ToInt32(rDs.Tables[0].Rows[zCont]["nId_Empr"].ToString());


                    if (rDs.Tables[0].Rows[zCont]["CAEPF"].ToString().Trim() != "0" && rDs.Tables[0].Rows[zCont]["CAEPF"].ToString().Trim() != "")
                    {
                        xIdeEmpregador.tpInsc = 2;
                        string rCPF = GerarCpf(rDs.Tables[0].Rows[0]["CAEPF"].ToString().Trim().Substring(0, 9));
                        xIdeEmpregador.nrInsc = rCPF; //CPF
                        xMonit.Id = Gerar_ID(rCPF, zCont + zContador + 1);
                    }
                    else
                    {


                        //Empregador
                        xIdeEmpregador.tpInsc = 1;

                        //xIdeEmpregador.nrInsc = rDs.Tables[0].Rows[0]["CNPJ"].ToString().Substring(0, 8);  //CNPJ


                        Ilitera.Data.eSocial xTrazerCNPJ = new Ilitera.Data.eSocial();
                        string xCNPJ_Matriz = xTrazerCNPJ.Retornar_CNPJ_Matriz(znId_Empr);

                        if (xCNPJ_Matriz.Trim() == "")
                        {
                            xIdeEmpregador.nrInsc = rDs.Tables[0].Rows[0]["CNPJ"].ToString().Substring(0, 8);  //CNPJ
                            xMonit.Id = Gerar_ID(rDs.Tables[0].Rows[zCont]["CNPJ"].ToString().Trim().Substring(0, 8), zCont + zContador + 1);
                        }
                        else
                        {
                            xIdeEmpregador.nrInsc = xCNPJ_Matriz.Substring(0, 8);
                            xMonit.Id = Gerar_ID(xCNPJ_Matriz.Substring(0, 8), zCont + zContador + 1);
                        }
                    }



                    string xCNPJ = rDs.Tables[0].Rows[zCont]["CNPJ"].ToString().Trim();

                    //if (xCNPJ.Length == 14)
                    //    xIdeEmpregador.nrInsc = xCNPJ;
                    //else
                    //    xIdeEmpregador.nrInsc = new string('0', 14 - xCNPJ.Length) + xCNPJ;


                    xMonit.ideEmpregador = xIdeEmpregador;


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

                    if  ( zAmbiente == "1") //(cliente.ESocial_Ambiente == "1")  //produ��o
                        xIdeEvento.tpAmb = 1;
                    else
                        xIdeEvento.tpAmb = 2;   // 2 se produ��o restrita
                    

                    xIdeEvento.verProc = "1";

                    xMonit.ideEvento = xIdeEvento;




                    //IdeVinculo
                    xIdeVinculo.cpfTrab = rDs.Tables[0].Rows[zCont]["CPF"].ToString();

                    if (rDs.Tables[0].Rows[zCont]["Matricula"].ToString().Trim() != "")
                        xIdeVinculo.matricula = rDs.Tables[0].Rows[zCont]["Matricula"].ToString();
                    else
                        xIdeVinculo.codCateg = "101";
                    //  se n�o enviar matr�cula, enviar CodCateg


                    //vers�o 02/2020 - excluir NISTRAB
                    //xIdeVinculo.nisTrab = rDs.Tables[0].Rows[zCont]["PIS"].ToString();



                    xMonit.ideVinculo = xIdeVinculo;



                    eSocialEvtMonitExMedOcup xTipoExame = new eSocialEvtMonitExMedOcup();
                    xTipoExame.tpExameOcup = System.Convert.ToSByte(rDs.Tables[0].Rows[zCont]["tpASO"].ToString());    // 0 admissional    1 periodico    2 retorno ao trabalho    3 mudan�a de funcao    4 monitoracao pontual    9 demissional


                    eSocialEvtMonitExMedOcupAso xASO = new eSocialEvtMonitExMedOcupAso();
                    xASO.dtAso = System.Convert.ToDateTime(rDs.Tables[0].Rows[zCont]["Data_Exame"].ToString(), ptBr);

                    xASO.resAso = System.Convert.ToSByte(rDs.Tables[0].Rows[zCont]["ResASO"].ToString());   //( 2 inapto )

                    xASO.resAsoSpecified = true;






                    Clinico zClinico = new Clinico(System.Convert.ToInt32(rDs.Tables[0].Rows[zCont]["IdClinico"].ToString()));

                    Ghe zGHE = new Ghe();

                    zGHE.Find(System.Convert.ToInt32(rDs.Tables[0].Rows[zCont]["nId_Func"].ToString()));


                    string sExamesOcupacionais = zClinico.GetPlanejamentoExamesAso_Formatado(zGHE, true, true, "", false);

                    //1. Acuidade Visual                         /  /       \n2. Audiometria                           31/05/2016   \n3. Eletrocardiograma (ECG)                 /  /       \n4. Eletroencefalograma (EEG)               /  /       \n5. Glicemia Jejum                          /  /       \n
                    //1. �cido Mand�lico                         /  /          2. Acuidade Visual                         /  /       \n3. Ars�nico                                /  /          4. Audiometria                             /  /       \n5. Dosagem de �cido Hip�rico               /  /          6. Eletrocardiograma (ECG)                 /  /       \n7. Eletroencefalograma (EEG)               /  /          8. Fator Reumatoide ( Latex )              /  /       \n9. Flora Normal                            /  /          10. Glicemia Jejum                          /  /      \n11. Hepatite � HbsAG                        /  /         12. Metanol                                 /  /      \n13. Metemoglobina sangu�nea                 /  /         14. Sorologia para HIV                      /  /      \n\r\n\r\n




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
                        else if (rExames[zAux].Substring(rExames[zAux].IndexOf("/") - 2, 10) == "  /  /    ")
                        {
                            zExames = zExames - 1;
                        }
                        else
                        {
                            DateTime xD1 = System.Convert.ToDateTime(rExames[zAux].Substring(rExames[zAux].IndexOf("/") - 2, 10), ptBr);
                            DateTime xD2 = System.Convert.ToDateTime(rDs.Tables[0].Rows[zCont]["Data_Exame"].ToString(), ptBr);

                            DateTime xD3 = System.Convert.ToDateTime(rDs.Tables[0].Rows[zCont]["Data_Admissao"].ToString(), ptBr);

                            //data compl.n�o pode ser ap�s data ASO
                            if (xD1 > xD2)
                            {
                                zExames = zExames - 1;
                            }
                            else
                            {
                                //se n�o for admissional, data exame nao pode ser anterior � admiss�o
                                if (rDs.Tables[0].Rows[zCont]["TpASO"].ToString().Trim() != "0")
                                {
                                    if (xD1 < xD3) zExames = zExames - 1;
                                }
                            }
                        }
                    }


                    if (xTipoExame.tpExameOcup != 9)   //demissional n�o fazer essa busca de exames
                    {
                        //procurar demais exames feitos salvos
                        zClinico.IdEmpregado.Find();

                        //ArrayList list;

                        //if ( Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("PRAJNA") > 0 )
                        //   list = new ExameBase().Find(" IdEmpregado = " + zClinico.IdEmpregado.Id + " and IdExameDicionario not between 1 and 5 and DataExame between convert( smalldatetime,'" + zClinico.DataExame.AddDays(-365).ToString("dd/MM/yyyy", ptBr) + "', 103 ) and convert( smalldatetime,'" + zClinico.DataExame.ToString("dd/MM/yyyy", ptBr) + "', 103 ) ");
                        //else
                        //   list = new ExameBase().Find(" IdEmpregado = " + zClinico.IdEmpregado.Id + " and IdExameDicionario not between 1 and 5 and DataExame between convert( smalldatetime,'" + zClinico.DataExame.AddDays(-365).ToString("dd/MM/yyyy", ptBr) + "', 103 ) and convert( smalldatetime,'" + zClinico.DataExame.ToString("dd/MM/yyyy", ptBr) + "', 103 ) and IndResultado in (1,2) ");
                        DataSet mDs = new DataSet();

                        Ilitera.Data.eSocial ListExames = new Ilitera.Data.eSocial();

                        if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("PRAJNA") > 0)
                            mDs = ListExames.Retornar_Exames_2220(zClinico.IdEmpregado.Id, zClinico.DataExame.AddDays(-365).ToString("dd/MM/yyyy", ptBr), zClinico.DataExame.ToString("dd/MM/yyyy", ptBr), true);
                        else
                            mDs = ListExames.Retornar_Exames_2220(zClinico.IdEmpregado.Id, zClinico.DataExame.AddDays(-365).ToString("dd/MM/yyyy", ptBr), zClinico.DataExame.ToString("dd/MM/yyyy", ptBr), false);



                        //foreach (ExameBase xExame in list)
                        for (int jCont = 0; jCont < mDs.Tables[0].Rows.Count; jCont++)
                        {
                            bool rLoc = false;

                            ExameBase xExame = new ExameBase();
                            xExame.Find(System.Convert.ToInt32(mDs.Tables[0].Rows[jCont]["IdExameBase"].ToString()));

                            xExame.IdExameDicionario.Find();

                            //ver se tem exame fora da lista
                            for (int zAux = 0; zAux < rExames.Length; zAux++)
                            {

                                if (rExames[zAux].Trim().Length >= 40 && rExames[zAux].Substring(rExames[zAux].IndexOf("/") - 2, 10) != "  /  /    ")
                                {



                                    if (xExame.IdExameDicionario.Nome.Trim().Length >= 30)
                                    {
                                        if (rExames[zAux].ToUpper().IndexOf(xExame.IdExameDicionario.Nome.Trim().Substring(0, 30).ToUpper()) >= 0)
                                        {
                                            rLoc = true;
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        if (rExames[zAux].ToUpper().IndexOf(xExame.IdExameDicionario.Nome.Trim().ToUpper()) >= 0)
                                        {
                                            rLoc = true;
                                            break;
                                        }
                                    }
                                }
                            }

                            if (rLoc == false)
                            {
                                zExames = zExames + 1;
                                Array.Resize(ref rExames, rExames.Length + 1);
                                if (xExame.IdExameDicionario.Nome.Trim().Length >= 39)
                                {
                                    rExames[rExames.Length - 1] = "0." + xExame.IdExameDicionario.Nome.Substring(0, 39) + " " + xExame.DataExame.ToString("dd/MM/yyyy", ptBr) + " ";
                                }
                                else
                                {
                                    rExames[rExames.Length - 1] = "0." + xExame.IdExameDicionario.Nome.Trim() + new string(' ', 39 - xExame.IdExameDicionario.Nome.Trim().Length) + " " + xExame.DataExame.ToString("dd/MM/yyyy", ptBr) + " ";
                                }
                            }

                        }





                    }



                    eSocialEvtMonitExMedOcupAsoExame[] xMonitASOExame = new eSocialEvtMonitExMedOcupAsoExame[zExames + 1];
                    //eSocialEvtMonitExMedOcupAsoExame[] xMonitASOExame = new eSocialEvtMonitExMedOcupAsoExame[0];

                    bool xLoc0295 = false;

                    int zArray = 0;

                    for (int zAux = 0; zAux < rExames.Length; zAux++)
                    {

                        if (rExames[zAux].Trim().Length >= 40 && rExames[zAux].Substring(rExames[zAux].IndexOf("/") - 2, 10) != "  /  /    ")
                        {


                            DateTime xD1 = System.Convert.ToDateTime(rExames[zAux].Substring(rExames[zAux].IndexOf("/") - 2, 10), ptBr);
                            DateTime xD2 = System.Convert.ToDateTime(rDs.Tables[0].Rows[zCont]["Data_Exame"].ToString(), ptBr);

                            DateTime xD3 = System.Convert.ToDateTime(rDs.Tables[0].Rows[zCont]["Data_Admissao"].ToString(), ptBr);



                            //data compl.n�o pode ser ap�s data ASO
                            if (xD1 <= xD2)
                            {

                                //se n�o for admissional, data exame nao pode ser anterior � admiss�o
                                if (rDs.Tables[0].Rows[zCont]["TpASO"].ToString().Trim() == "0" || (rDs.Tables[0].Rows[zCont]["TpASO"].ToString().Trim() != "0" && xD1 >= xD3))
                                {



                                    xMonitASOExame[zArray] = new eSocialEvtMonitExMedOcupAsoExame();

                                    //xMonitASOExame[zAux].dtExm = System.Convert.ToDateTime(rDs.Tables[0].Rows[zCont]["Data_Exame"].ToString(), ptBr);
                                    //else


                                    xMonitASOExame[zArray].dtExm = System.Convert.ToDateTime(rExames[zAux].Substring(rExames[zAux].IndexOf("/") - 2, 10), ptBr);

                                    string rData = rExames[zAux].Substring(rExames[zAux].IndexOf("/") - 2, 10);


                                    //preciso fazer o de-para da tabela 27 com ExameDicionario
                                    // rExames[zAux] possui o exame de ExameDicionario
                                    //string tNomeExame = rExames[zAux].Substring(1, 36).Trim();
                                    string tNomeExame = rExames[zAux].Substring(rExames[zAux].IndexOf(".") + 1, 36).Trim();

                                    if (tNomeExame.Substring(tNomeExame.Length - 1, 1) == "1" || tNomeExame.Substring(tNomeExame.Length - 1, 1) == "2")
                                    {
                                        tNomeExame = tNomeExame.Substring(0, tNomeExame.Length - 1).Trim();
                                    }


                                    ExameDicionario tExame = new ExameDicionario();
                                    tExame.Find(" Nome = '" + tNomeExame + "'");


                                    bool rFind = false;

                                    if (tExame.Id != 0)
                                    {
                                        if (tExame.Codigo_eSocial != "")
                                        {
                                            string rCod = tExame.Codigo_eSocial.Trim();
                                            if (rCod.Length < 4)
                                                rCod = new string('0', 4 - rCod.Length) + rCod;
                                            else
                                                rCod = rCod.Substring(0, 4);

                                            if (rCod == "0295") xLoc0295 = true;


                                            //for ( int nFind=0; nFind< xMonitASOExame.Length; nFind++)
                                            //{
                                            //    if (xMonitASOExame[nFind]!=null && xMonitASOExame[nFind].procRealizado != null && xMonitASOExame[nFind].procRealizado == rCod )
                                            //    {
                                            //        rFind = true;
                                            //        break;
                                            //    }
                                            //}

                                            if (rFind == false)
                                            {
                                                xMonitASOExame[zArray].procRealizado = rCod;
                                            }

                                            //xMonitASOExame[zArray].procRealizado = rCod;
                                        }
                                        else
                                        {
                                            xMonitASOExame[zArray].procRealizado = "9999";
                                            rFind = false;
                                        }
                                    }
                                    else
                                    {
                                        xMonitASOExame[zArray].procRealizado = "9999";
                                        rFind = false;
                                    }


                                    if (rFind == false)
                                    {

                                        //Array.Resize(ref xMonitASOExame, xMonitASOExame.Length + 1);

                                        string xObsCompl = "";

                                        //procurar exame e resultado
                                        zClinico.IdEmpregado.Find();

                                        //ExameBase rExame = new ExameBase();
                                        //rExame.Find(" IdEmpregado = " + zClinico.IdEmpregado.Id.ToString() + " and IdExameDicionario = " + tExame.Id.ToString() + " and convert( char(10),DataExame,103 ) = '" + rData + "' ");

                                        //if (rExame.Id != 0) xObsCompl = rExame.Prontuario.Trim();

                                        //usar arraylist
                                        //ArrayList listDic = new ExameDicionario().Find(" IdeSocial_Deposito = " + zCod.ToString() + " and processamento_retorno like '%<nrrecibo>%'  order by data_envio desc ");



                                        ExameDicionario rExameDic = new ExameDicionario();
                                        rExameDic.Find(" IdExameDicionario = " + tExame.Id + " and Codigo_eSocial in (583, 998, 999, 1128, 1230, 1992, 1993, 1994, 1995, 1996, 1997, 1998, 1999, 9999)  ");

                                        if (rExameDic.Id != 0)
                                        {
                                            xObsCompl = rExameDic.Nome;
                                        }


                                        //if (cliente.Envio_2220_Sem_Resultado == true || Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("PRAJNA") > 0)                            
                                        //{
                                        xMonitASOExame[zArray].indResultSpecified = false;
                                        //}
                                        //else
                                        //{

                                        //    if (rExame.Id != 0)
                                        //    {
                                        //        if (rExame.IndResultado == 2)
                                        //            xMonitASOExame[zArray].indResult = 2;   //1 normal  2 alterado  3 estaval  4 agravamento
                                        //        else
                                        //            xMonitASOExame[zArray].indResult = 1;
                                        //    }
                                        //    else
                                        //    {
                                        //        xMonitASOExame[zArray].indResult = 1;
                                        //    }

                                        //    xMonitASOExame[zArray].indResultSpecified = true;
                                        //}



                                        //if (rExames[zAux].Substring(0, 40).Trim().IndexOf(".") > 0 && rExames[zAux].Substring(0, 40).Trim().IndexOf(".") < 4)
                                        //{
                                        //    xMonitASOExame[zAux].obsProc = rExames[zAux].Substring(rExames[zAux].Substring(0, 40).Trim().IndexOf(".") + 1, 40 - rExames[zAux].Substring(0, 40).Trim().IndexOf(".")).Trim();
                                        //}
                                        //else
                                        //{
                                        //    xMonitASOExame[zAux].obsProc = rExames[zAux].Substring(0, 39).Trim();
                                        //}


                                        //checar se em Opsa->PCMSOGHE.RiscosOcupacionais h� flag <esocial>,
                                        //explicando exames fora dos riscos
                                        PcmsoGhe xPCMSOGhe = new PcmsoGhe();
                                        xPCMSOGhe.Find(" IdPCMSO=" + rDs.Tables[0].Rows[zCont]["IdPCMSO"].ToString() + " and IdGHE=" + rDs.Tables[0].Rows[zCont]["nId_Func"].ToString());

                                        string xObsProc = "";

                                        if (xPCMSOGhe.Id != 0)
                                        {
                                            int mPosit = xPCMSOGhe.RiscosOcupacionais.ToUpper().IndexOf("<ESOCIAL>");
                                            if (mPosit >= 0)
                                            {
                                                int mPosit2 = xPCMSOGhe.RiscosOcupacionais.ToUpper().IndexOf("<ESOCIAL>", mPosit + 1);

                                                if (mPosit2 >= 0)
                                                {
                                                    xObsProc = xPCMSOGhe.RiscosOcupacionais.Substring(mPosit + 9, mPosit2 - mPosit - 9);
                                                }
                                                else
                                                {
                                                    xObsProc = xPCMSOGhe.RiscosOcupacionais.Substring(mPosit + 9);
                                                }

                                            }
                                        }


                                        if (xObsProc == "" && xObsCompl != "")
                                        {
                                            //xMonitASOExame[zAux].obsProc = xObsCompl;
                                            xMonitASOExame[zArray].obsProc = xObsCompl;
                                        }
                                        else if (xObsProc != "" || xObsCompl != "")
                                        {
                                            //xMonitASOExame[zAux].obsProc = xObsProc + "  -  " + xObsCompl;
                                            xMonitASOExame[zArray].obsProc = xObsProc + "  -  " + xObsCompl;
                                        }
                                        else if (xObsProc != "" && xObsCompl == "")
                                        {
                                            //xMonitASOExame[zAux].obsProc = xObsProc + "  -  " + xObsCompl;
                                            xMonitASOExame[zArray].obsProc = xObsProc;
                                        }
                                        else
                                        {
                                            if (xMonitASOExame[zArray].procRealizado == "9999")
                                            {
                                                xMonitASOExame[zArray].obsProc = tNomeExame;
                                            }
                                        }


                                        zClinico.IdExameDicionario.Find();
                                        if (zClinico.IdExameDicionario.Id == 1)
                                        {
                                            xMonitASOExame[zArray].ordExame = 1;  // 1 referencial  2 sequencial
                                            xMonitASOExame[zArray].ordExameSpecified = true;
                                        }
                                        else
                                        {
                                            xMonitASOExame[zArray].ordExame = 2;
                                            xMonitASOExame[zArray].ordExameSpecified = true;
                                        }
                                    }

                                    zArray = zArray + 1;

                                }
                            }

                        }

                    }


                    if (xLoc0295 == false)
                    {
                        //avalia��o cl�nica
                        xMonitASOExame[zExames] = new eSocialEvtMonitExMedOcupAsoExame();
                        xMonitASOExame[zExames].dtExm = System.Convert.ToDateTime(rDs.Tables[0].Rows[zCont]["Data_Exame"].ToString(), ptBr);
                        xMonitASOExame[zExames].procRealizado = "0295";


                        zClinico.IdExameDicionario.Find();
                        if (zClinico.IdExameDicionario.Id == 1)
                        {
                            xMonitASOExame[zExames].ordExame = 1;  // 1 referencial  2 sequencial
                            xMonitASOExame[zExames].ordExameSpecified = true;
                        }
                        else
                        {
                            xMonitASOExame[zExames].ordExame = 2;
                            xMonitASOExame[zExames].ordExameSpecified = true;
                        }


                        //if (cliente.Envio_2220_Sem_Resultado == true || Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("PRAJNA") > 0)                    
                        //{
                        xMonitASOExame[zExames].indResultSpecified = false;
                        //}
                        //else
                        //{
                        //    xMonitASOExame[zExames].indResult = 1;   //1 normal  2 alterado  3 estaval  4 agravamento
                        //    xMonitASOExame[zExames].indResultSpecified = true;
                        //}

                    }
                    else
                    {
                        //zExames = zExames - 1; 
                        Array.Resize(ref xMonitASOExame, zExames);
                    }



                    //remover algum exame com c�digo eSocial repetido
                    string rCods = "";

                    for (int nFind = 0; nFind < xMonitASOExame.Length; nFind++)
                    {
                        if (xMonitASOExame[nFind] != null && rCods.IndexOf(xMonitASOExame[nFind].procRealizado.ToString().Trim()) < 0)
                        {
                            rCods = rCods + "," + xMonitASOExame[nFind].procRealizado.ToString().Trim();
                        }
                        else
                        {
                            RemoveAt(ref xMonitASOExame, nFind);
                        }
                    }





                    xASO.exame = xMonitASOExame;

                    xTipoExame.aso = xASO;










                    xRespMonitor.nmResp = rDs.Tables[0].Rows[zCont]["RespNome"].ToString().ToUpper().Trim();
                    xRespMonitor.nrCRM = System.Text.RegularExpressions.Regex.Match(rDs.Tables[0].Rows[zCont]["NrConsClasse"].ToString().ToUpper().Trim(), @"\d+").Value;

                    //completar campos abaixo do m�dico coordenador do PCMSO

                    if (rDs.Tables[0].Rows[zCont]["RespCPF"].ToString().ToUpper().Trim() != "0" && rDs.Tables[0].Rows[zCont]["RespCPF"].ToString().ToUpper().Trim() != "")
                        xRespMonitor.cpfResp = rDs.Tables[0].Rows[zCont]["RespCPF"].ToString().ToUpper().Trim();

                    //criar campo CPF para respons�vel monitora��o biol�gica

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



                    //M�dico ASO

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

                    xMedicoASO.ufCRMSpecified = true;
                    //NIT do m�dico



                    //vers�o 02/2020 - excluir NISMED E CPFMED
                    //if (rDs.Tables[0].Rows[zCont]["NITMed"].ToString().ToUpper().Trim() != "")
                    //    xMedicoASO.nisMed = rDs.Tables[0].Rows[zCont]["NITMed"].ToString().ToUpper().Trim();

                    //trazer esse dado
                    //xMedicoASO.cpfMed


                    xASO.medico = xMedicoASO;

                    xTipoExame.aso = xASO;



                    xMonit.exMedOcup = xTipoExame;



                    x2220.evtMonit = xMonit;

                    //jogar em bytes o XML para salvar em tbleSocial_Deposito - devo alterar o esquema, n�o posso salvar como abaixo

                    //CRIAR XML
                    string xContent = "";

                    XmlSerializer serializer = new XmlSerializer(typeof(eSocial_2220));

                    var memoryStream = new MemoryStream();
                    var streamWriter = new StreamWriter(memoryStream, System.Text.Encoding.UTF8);

                    serializer.Serialize(streamWriter, x2220);

                    byte[] bytes = memoryStream.ToArray();
                    xContent = Encoding.UTF8.GetString(bytes);


                    if (xArq.Trim() == "")
                    {
                        xArq = "I:\\temp\\Evt2220_" + System.DateTime.Now.Second.ToString().Trim() + System.DateTime.Now.Minute.ToString().Trim() + System.DateTime.Now.Millisecond.ToString().Trim() + System.DateTime.Now.Day.ToString().Trim() + System.DateTime.Now.Month.ToString().Trim() + ".xml";
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
                    //    XmlSerializer serializer = new XmlSerializer(typeof(eSocialEvtExpRisco));
                    //    serializer.Serialize(stream, xRisco);
                    //    stream.Flush();

                    //    byte[] bytes = new byte[stream.Length];
                    //    stream.Position = 0;
                    //    stream.Read(bytes, 0, (int)stream.Length);
                    //    xContent = Encoding.ASCII.GetString(bytes);

                    //    stream.Close();
                    //}





                    //salvar XML no SQL server com xeSocial.Id e nome do arquivo criado e datahora 
                    Int64 zPis = 0;
                    if (rDs.Tables[0].Rows[zCont]["PIS"].ToString().Trim() != "")
                        zPis = System.Convert.ToInt64(System.Text.RegularExpressions.Regex.Match(rDs.Tables[0].Rows[zCont]["PIS"].ToString().Trim(), @"\d+").Value);


                    Ilitera.Data.eSocial zeSocial = new Ilitera.Data.eSocial();

                    if (zCod == "")
                    {
                        zeSocial.XML_Deposito(xArq, xeSocial.Id, zCont, xContent, rDs.Tables[0].Rows[zCont]["CPF"].ToString(), zPis, rDs.Tables[0].Rows[zCont]["CNPJ"].ToString(), zD1, zD2, System.Convert.ToInt32(rDs.Tables[0].Rows[0]["IdExameBase"].ToString()), xMonit.Id,0);
                    }
                    else
                    {
                        zeSocial.XML_Deposito(zCod, xArq, xeSocial.Id, zCont, xContent, rDs.Tables[0].Rows[zCont]["CPF"].ToString(), zPis, rDs.Tables[0].Rows[zCont]["CNPJ"].ToString(), zD1, zD2, System.Convert.ToInt32(rDs.Tables[0].Rows[0]["IdExameBase"].ToString()), xMonit.Id,0);

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




           // MsgBox1.Show("Ilitera.Net", "Evento criado.", null, new EO.Web.MsgBoxButton("OK"));
            return;

        }



        private string ConvertWesternEuropeanToASCII(string str)
        {
            return Encoding.ASCII.GetString(Encoding.GetEncoding(1251).GetBytes(str));
        }



        public static void RemoveAt<T>(ref T[] arr, int index)
        {
            for (int a = index; a < arr.Length - 1; a++)
            {
                // moving elements downwards, to fill the gap at [index]
                arr[a] = arr[a + 1];
            }
            // finally, let's decrement Array's size by one
            Array.Resize(ref arr, arr.Length - 1);
        }




        private string Gerar_ID(string xCNPJ, int xCont)
        {

            //IDTNNNNNNNNNNNNNNAAAAMMDDHHMMSSQQQQQ 
            //ID -Texto Fixo "ID"; T - Tipo de Inscri��o do Empregador(1 - CNPJ; 2 - CPF); NNNNNNNNNNNNNN - N�mero do CNPJ ou CPF do empregador - Completar com zeros � direita.No caso de pessoas jur�dicas, o CNPJ informado deve conter 8 ou 14 posi��es de acordo com o enquadramento do contribuinte para preenchimento do campo { ideEmpregador / nrInsc}
            //do evento S-1000, completando - se com zeros � direita, se necess�rio.
            //AAAAMMDD - Ano, m�s e dia da gera��o do evento; HHMMSS - Hora, minuto e segundo da gera��o do evento; 
            //QQQQQ - N�mero sequencial da chave.Incrementar somente quando ocorrer gera��o de eventos na mesma data/ hora, completando com zeros � esquerda.

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


            //    if (zCod.Trim() == "") //n�o foi processado, n�o pode enviar
            //    {
            //        MsgBox1.Show("Ilitera.Net", "Existem registros n�o processados selecionados.  Criar xml primeiro.", null,
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
            //        "Certificado(s) Digital(is) dispon�vel(is)", "Selecione o certificado digital para uso no aplicativo",
            //        X509SelectionFlag.MultiSelection);

            //    if (scollection.Count == 0)
            //    {
            //        var msgResultado =
            //            "Nenhum certificado digital foi selecionado ou o certificado selecionado est� com problemas.";
            //        MsgBox1.Show("Ilitera.Net", msgResultado, null,
            //        new EO.Web.MsgBoxButton("OK"));
            //        return;
            //    }
            //    else
            //    {
            //        oX509Cert = scollection[0];
            //        cert = oX509Cert;
            //        //txt_Status.Text = txt_Status.Text + oCertificado.IssuerName.Name + " - " + oCertificado.SerialNumber + " - " + oCertificado.NotBefore + " � " + oCertificado.NotAfter + System.Environment.NewLine;
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
            //        xAssinado = assinarXML(xDoc, cert, "evtMonit", "Id");


            //        //montar cabe�alho e rodap�
            //        string zId = "";
            //        string zNrInsc = "";

            //        zId = xAssinado.InnerXml.Substring(xAssinado.InnerXml.IndexOf(" Id=") + 5, 36);
            //        zNrInsc = xAssinado.InnerXml.Substring(xAssinado.InnerXml.IndexOf("<nrInsc>") + 8, 8);

            //        string xCabecalho = @"<eSocial xmlns = ""http://www.esocial.gov.br/schema/lote/eventos/envio/v1_1_1""><envioLoteEventos grupo = ""2""><ideEmpregador><tpInsc>1</tpInsc><nrInsc>03844493000134</nrInsc></ideEmpregador><ideTransmissor><tpInsc>1</tpInsc><nrInsc>03844493000134</nrInsc></ideTransmissor><eventos><evento Id = ""ID1038444930001342019032110270800011"" >";
            //        //string xCabecalho = @"<eSocial xmlns = ""http://www.esocial.gov.br/schema/lote/eventos/envio/v1_1_1""><envioLoteEventos grupo = ""1""><ideEmpregador><tpInsc>1</tpInsc><nrInsc>" + zNrInsc + "</nrInsc></ideEmpregador><ideTransmissor><tpInsc>1</tpInsc><nrInsc>03844493000134</nrInsc></ideTransmissor><eventos><evento Id = " + "" + zId  +  "" + " >";


            //        xCabecalho = xCabecalho.Replace("ID1038444930001342019032110270800011", zId);
            //        xCabecalho = xCabecalho.Replace("<ideEmpregador><tpInsc>1</tpInsc><nrInsc>03844493000134</nrInsc></ideEmpregador>", "<ideEmpregador><tpInsc>1</tpInsc><nrInsc>" + zNrInsc + "</nrInsc></ideEmpregador>");
            //        xCabecalho = xCabecalho.Replace("<ideTransmissor><tpInsc>1</tpInsc><nrInsc>03844493000134</nrInsc></ideTransmissor>", "<ideTransmissor><tpInsc>1</tpInsc><nrInsc>" + xCNPJ_Certificado + "</nrInsc></ideTransmissor>");


            //        //rodap�
            //        string xRodape = "</evento></eventos></envioLoteEventos></eSocial>";

            //        //adicionar evento dentro do cabe�alho e rodap�
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
            //        //transformar em formato XML e salvar no SQL, como hist�rico do envio

            //        //salvar em tbl_eSocial_Envio->Processamento_Lote
            //        //vai ter op��o de abrir como XML no sistema web
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
            //            //transformar em formato XML e salvar no SQL, como hist�rico do envio

            //            XmlDocument xDoc20 = new XmlDocument();

            //            byte[] encodedString4 = Encoding.UTF8.GetBytes(xRet4);

            //            MemoryStream ms4 = new MemoryStream(encodedString4);
            //            ms4.Flush();
            //            ms4.Position = 0;

            //            xDoc20.Load(ms4);


            //            //salvar em tbl_eSocial_Envio->Processamento_Retorno
            //            //vai ter op��o de abrir como XML no sistema web
            //            zEnvio.Processamento_Retorno = xDoc20.InnerXml;
            //            zEnvio.Save();


            //            //xDoc20.Load(xRet4);

            //            //XDocument doc = XDocument.Parse(xDoc20.InnerXml);

            //            //preciso testar loop para v�rios registros no mesmo XML

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
            //            //        //tbl_eSocial_Envio->Status_Retorno - talvez tenha que colocar esse campo em uma tabela � parte, para que um ideSocial_Deposito tenha v�rios retornos
            //            //    }



            //        }


            //        //para Lote-esse � corpo a ser adicionado, e o evento vai dentro.  
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
            // Vari�veis utilizadas na assinatura
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
                // Verifica se o certificado passado por par�metro possui chave privada.
                // Se n�o for poss�vel verificar se o certificado tem ou n�o a chave privada, significa que
                // a inst�ncia do objeto X509Certificate2 est� nula.
                try
                {
                    temChavePrivada = certificadoX509.HasPrivateKey;
                }
                catch (Exception ex)
                {
                    // Objeto X509Certificate2 passado por par�metro est� nulo                    
                    MsgBox1.Show("Ilitera.Net", "Objeto X509Certificate2 passado por par�metro n�o foi carregado." + ex.Message, null,
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
                                // Informando qual a tag ser� assinada
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

                                // Referenciando o identificador da tag que ser� assinada
                                reference = new Reference("#" + nodeParaAssinatura[0].Attributes[idAtributoTag].Value);
                                reference.Uri = "";
                                reference.AddTransform(new XmlDsigEnvelopedSignatureTransform(false));
                                reference.AddTransform(new XmlDsigC14NTransform(false));
                                reference.DigestMethod = System.IdentityModel.Tokens.SecurityAlgorithms.Sha256Digest;// "http://www.w3.org/2001/04/xmldsig-more#rsa-sha256";

                                // Adicionando a referencia de qual tag ser� assinada
                                signedXml.AddReference(reference);

                                // Adicionando informa��es do certificado na assinatura
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
                            // String que informa o id da tag XML a ser assinada est� vazia                            
                            MsgBox1.Show("Ilitera.Net", "String que informa o id da tag XML a ser assinada est� vazia", null,
                            new EO.Web.MsgBoxButton("OK"));
                        }
                    }
                    else
                    {
                        // String que informa a tag XML a ser assinada est� vazia                        
                        MsgBox1.Show("Ilitera.Net", "String que informa a tag XML a ser assinada est� vazia", null,
                        new EO.Web.MsgBoxButton("OK"));
                    }
                }
                else
                {
                    // Certificado Digital informado n�o possui chave privada                    
                    MsgBox1.Show("Ilitera.Net", "Certificado Digital informado n�o possui chave privada", null,
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
            //    MsgBox1.Show("Ilitera.Net", "N�o foi poss�vel obter o protocolo deste envio (1)",  null,
            //                                new EO.Web.MsgBoxButton("OK"));
            //    return;
            //}

            //if ( bEnvio.Protocolo.Trim()=="")
            //{
            //    MsgBox1.Show("Ilitera.Net", "N�o foi poss�vel obter o protocolo deste envio (2)", null,
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
            //        "Certificado(s) Digital(is) dispon�vel(is)", "Selecione o certificado digital para uso no aplicativo",
            //        X509SelectionFlag.MultiSelection);

            //    if (scollection.Count == 0)
            //    {
            //        var msgResultado =
            //            "Nenhum certificado digital foi selecionado ou o certificado selecionado est� com problemas.";
            //        MsgBox1.Show("Ilitera.Net", msgResultado, null,
            //        new EO.Web.MsgBoxButton("OK"));
            //        return;
            //    }
            //    else
            //    {
            //        oX509Cert = scollection[0];
            //        cert = oX509Cert;
            //        //txt_Status.Text = txt_Status.Text + oCertificado.IssuerName.Name + " - " + oCertificado.SerialNumber + " - " + oCertificado.NotBefore + " � " + oCertificado.NotAfter + System.Environment.NewLine;
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
            //    //transformar em formato XML e salvar no SQL, como hist�rico do envio

            //    XmlDocument xDoc20 = new XmlDocument();

            //    byte[] encodedString4 = Encoding.UTF8.GetBytes(xRet4);

            //    MemoryStream ms4 = new MemoryStream(encodedString4);
            //    ms4.Flush();
            //    ms4.Position = 0;

            //    xDoc20.Load(ms4);


            //    //salvar em tbl_eSocial_Envio->Processamento_Retorno
            //    //vai ter op��o de abrir como XML no sistema web
            //    bEnvio.Processamento_Retorno = xDoc20.InnerXml;
            //    bEnvio.Save();

            //}
            //catch (Exception ex)
            //{
            //    MsgBox1.Show("Ilitera.Net", "Erro na atualiza��o do Status do Processamento do lote: " + ex.Message, null,
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


                if (zCod.Trim() == "") //n�o foi processado, n�o pode enviar
                {
                    MsgBox1.Show("Ilitera.Net", "Existem registros n�o processados selecionados.  Criar xml primeiro.", null,
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
                string zCPF = item.Cells["CPF"].Value.ToString().Trim();

                XmlDocument xDoc = new XmlDocument();

                Ilitera.Data.eSocial xDados = new Ilitera.Data.eSocial();

                DataSet rDs = new DataSet();
                rDs = xDados.Trazer_XML2(System.Convert.ToInt32(zCod));

                string strXML = rDs.Tables[0].Rows[0][0].ToString().Replace("\r\n", string.Empty);

                if (cliente.ESocial_Download_Cabecalho == false)
                    strXML = strXML.Replace("xmlns=\"http://www.esocial.gov.br/schema/evt/evtMonit/v_S_01_02_00\"", string.Empty);

                

                byte[] encodedString = Encoding.UTF8.GetBytes(strXML);

                // Put the byte array into a stream and rewind it to the beginning
                MemoryStream ms = new MemoryStream(encodedString);
                ms.Flush();
                ms.Position = 0;


                xDoc.Load(ms);

                string xArq = "I:\\temp\\2220_" + zCPF + "_" + zCont.ToString().Trim() + "_" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + "_" + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString() + ".XML";

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
                string zipName = String.Format("eSocial2220_Zip_{0}.zip", DateTime.Now.ToString("yyyy-MMM-dd-HHmmss"));
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

        protected void chk_Grupo_CheckedChanged(object sender, EventArgs e)
        {
            Carga_Grid();
        }

        protected void txt_Nome_TextChanged(object sender, EventArgs e)
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


                if (zCod.Trim() == "") //n�o foi processado, n�o pode enviar
                {
                    MsgBox1.Show("Ilitera.Net", "Existem registros n�o processados selecionados.  Criar xml primeiro.", null,
                                                new EO.Web.MsgBoxButton("OK"));
                    return;
                }


                //se foi j� foi enviado n�o permitir reagendar, a n�o ser que seja retifica��o, mas ainda preciso pensar nessa parte
                zCod = item.Cells[14].Value.ToString();

                if (zCod.Trim().ToUpper().IndexOf("RECIBO GERADO") > 0)  //j� foi enviado, n�o pode agendar
                {
                    MsgBox1.Show("Ilitera.Net", "Existem registros j� enviados que n�o podem ser agendados. ", null,
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
                MsgBox1.Show("Ilitera.Net", "Data Inicial de Filtro inv�lida!", null,
                      new EO.Web.MsgBoxButton("OK"));
                return;
            }

            if (Validar_Data(txt_Data2.Text.Trim()) == false)
            {
                MsgBox1.Show("Ilitera.Net", "Data Final de Filtro inv�lida!", null,
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
            zDs = xLista.Mensageria_2220_CSV(Convert.ToInt32(Request.QueryString["IdEmpresa"]), 0, 0, txt_Data.Text, txt_Data2.Text, xUser.IdUsuario, zTipo_Carga, txt_Nome.Text, zGrupo, cliente.ESocial_Ambiente);

            string xFile = "eSocial2220_" + System.DateTime.Now.Second.ToString().Trim() + System.DateTime.Now.Millisecond.ToString().Trim() + System.DateTime.Now.Minute.ToString().Trim() + System.DateTime.Now.Day.ToString().Trim() + System.DateTime.Now.Month.ToString().Trim() + ".csv";
            string myStringWebResource = "I:\\temp\\" + xFile;

            string zLinha = "";



            TextWriter tw = new StreamWriter(myStringWebResource, false, Encoding.GetEncoding(1252));


            zLinha = "CPF;CNPJ;Empresa;Colaborador;Criado_por;Data_Criacao;Exame;Status_Envio;Matricula;Data_Exame";
            tw.WriteLine(zLinha);

            for (int zCont = 0; zCont < zDs.Tables[0].Rows.Count; zCont++)
            {
                zLinha = "";

                for (int zAux = 0; zAux < zDs.Tables[0].Columns.Count; zAux++)
                {
                    if (zAux == 0 || zAux == 1 || zAux == 2 || zAux == 3 || zAux == 4 || zAux == 5 || zAux == 7 || zAux == 8 || zAux == 9 || zAux == 12)
                    {
                        //  zLinha = zLinha + zDs.Tables[0].Rows[zCont][zAux].ToString().Trim() + ";";
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



    

