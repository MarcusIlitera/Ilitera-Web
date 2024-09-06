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
using System.Data.SqlClient;
using System.Reflection;

using System.Security.Cryptography;
using System.Text;
using System.Net;
using System.IO;

using Entities;
using Facade;
using System.Net.Mail;
using Ilitera.Data;


namespace Ilitera.Net
{
    public partial class Login : System.Web.UI.Page
    {

        private static byte[] key = new byte[8] { 1, 2, 3, 4, 5, 6, 7, 8 };
        private static byte[] iv = new byte[8] { 1, 2, 3, 4, 5, 6, 7, 8 };

        protected void Page_Load(object sender, EventArgs e)
        {            
            Ilitera.Data.SQLServer.EntitySQLServer.xDB1 = ConfigurationManager.AppSettings["DB1"].ToString();
            Ilitera.Data.SQLServer.EntitySQLServer.xDB2 = ConfigurationManager.AppSettings["DB2"].ToString();
            Ilitera.Data.SQLServer.EntitySQLServer._LocalServer = ConfigurationManager.AppSettings["LocalServer"].ToString();



            Session["Filtro_Nome"] = "";
            Session["Filtro_Setor"] = "";
            Session["Filtro_Tipo"] = "";
            Session["Pagina"] = "1";
            Session["Retorno"] = "1";
            Session["Ajuda"] = "~/Help/Manual_Novo_Ilitera_Net.pdf";
            Session["Ultima_Guia"] = "";



            if (!IsPostBack)
            {
                string xP_User = "";
                string xP_Senha = "";

                if (Request["Nome"] != null) xP_User = Request["Nome"].ToString().Trim();

                if (Request["Senha"] != null) xP_Senha = Request["Senha"].ToString().Trim();


                if (xP_User != "")
                {
                    txtUsuario.Text = xP_User;
                    txtSenha.Text = xP_Senha;
                    
                    //txtSenha.Text = Decrypt(txtSenha.Text);

                    object zSender = new object();                    
                    EventArgs zE = new EventArgs();

                    //btnLogar_Click(zSender, zE);
                    Logar();
                    

                    
                    //if (Logar() == false)
                    //{
                    //    if (Request["Nome"] != null)
                    //    {
                    //        Response.Redirect("http://www.essencenet.com.br");
                    //        return;
                    //    }
                    //    else
                    //    {
                    //        Response.Redirect("Login.aspx");
                    //        return;
                    //    }
                    //}
                    

                }

            }



            string zUrl = HttpContext.Current.Request.Url.AbsoluteUri.ToString().ToUpper();

            if ( zUrl.IndexOf("JOHNSON")> 0 )
            {
                btnLogar.Enabled = false;
                txtSenha.Enabled = false;
                txtUsuario.Enabled = false;
                lnkLembrarSenha.Enabled = false;
                //btnLogar.Visible = false;
                //txtSenha.Visible = false;
                //txtUsuario.Visible = false;
                //lnkLembrarSenha.Visible = false;


                btnSAML.Visible = true;
                btnSAML.Enabled = true;
            }
            else
            {
                btnLogar.Enabled = true;
                txtSenha.Enabled = true;
                txtUsuario.Enabled = true;
                lnkLembrarSenha.Enabled = true;
                //btnLogar.Visible = true;
                //txtSenha.Visible = true;
                //txtUsuario.Visible = true;
                //lnkLembrarSenha.Visible = true;


                btnSAML.Visible = false;
                btnSAML.Enabled = false;
            }


        }






        protected void btnLogar_Click(object sender, EventArgs e)
        {



            var client = new System.Net.WebClient();

            string EncodedResponse = Request.Form["g-Recaptcha-Response"];

            string PrivateKey = "6Ld_cqoUAAAAAJnHWBkL0OHsPM0l9n3PA2Arw8gS";

            var GoogleReply = client.DownloadString(string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", PrivateKey, EncodedResponse));

            string xCaptcha = "";

            xCaptcha = GoogleReply.ToString();

            if (Request["Nome"] == null || Request["Senha"] == null)
            {
                if (xCaptcha.IndexOf("success") < 0 || xCaptcha.IndexOf(": true") < 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alerta", "alert('Não validado!');", true);
                    return;
                }
            }

            try
            {                
                //usuário e senha do connectionstring                
                var settings = ConfigurationManager.ConnectionStrings["ConexaoIliteraOpsa"];

                var fi = typeof(ConfigurationElement).GetField("_bReadOnly", BindingFlags.Instance | BindingFlags.NonPublic);

                fi.SetValue(settings, false);

                settings.ConnectionString = ConfigurationManager.ConnectionStrings["ConexaoIliteraOpsa"].ConnectionString + ";User ID = admin; Password = Ilitera572160x4";
                //ConfigurationManager.ConnectionStrings["ConexaoIliteraOpsa"].ConnectionString + ";User ID = sa; Password = Ilitera572160";


                settings = ConfigurationManager.ConnectionStrings["ConexaoIliteraSiedNovo"];

                fi = typeof(ConfigurationElement).GetField("_bReadOnly", BindingFlags.Instance | BindingFlags.NonPublic);

                fi.SetValue(settings, false);

                settings.ConnectionString = ConfigurationManager.ConnectionStrings["ConexaoIliteraSiedNovo"].ConnectionString + ";User ID = admin; Password = Ilitera572160x4";
                

                //if ( txtUsuario.Text == "COMUNIC99")
                //{
                    
                    
                //    string fileName = "I:\\temp\\ExameAd.xml";

                //    System.IO.StreamReader reader = new System.IO.StreamReader(fileName);
                //    string ret = reader.ReadToEnd();
                //    reader.Close();

                //    //string postData = ret;
                //    //System.Text.Encoding encoding = System.Text.Encoding.UTF8;
                //    //byte[] bytes = encoding.GetBytes(postData);
                //    //string url = "https://www.ilitera.net.br/essence_hom/Comunicacao.aspx";

                //    Session["XML"] = ret;

                //    Response.Redirect("~/Comunicacao.aspx");
                    
                //}
                //else
                //{
                    Session["XML"] = "";
                //}


                //logar

                Usuario user = UsuarioFacade.AutenticarUsuaro(txtUsuario.Text, txtSenha.Text);

                if (user.NomeUsuario != null && user.NomeUsuario != String.Empty)
                {
                    
                    Session["usuarioLogado"] = user;
                    InicializaSessoesMenu();


                   Log_Web("Login - Sistema" , user.IdUsuario, "Acesso ao sistema - Web");

                   FormsAuthentication.RedirectFromLoginPage(txtUsuario.Text, false);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alerta", "alert('Usuário/senha inválidos.');", true);
                    //MsgBox1.Show("Ilitera.Net", "Usuário/senha inválidos.", null, new EO.Web.MsgBoxButton("OK"));
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alerta", String.Format("alert('{0}');", ex.Message), true);
            }
        }


        protected void Logar()
        {


            //var client = new System.Net.WebClient();

            //string EncodedResponse = Request.Form["g-Recaptcha-Response"];

            //string PrivateKey = "6Ld_cqoUAAAAAJnHWBkL0OHsPM0l9n3PA2Arw8gS";

            //var GoogleReply = client.DownloadString(string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", PrivateKey, EncodedResponse));

            //string xCaptcha = "";

            //xCaptcha = GoogleReply.ToString();

            //if (Request["Nome"] == null || Request["Senha"] == null)
            //{
            //    if (xCaptcha.IndexOf("success") < 0 || xCaptcha.IndexOf(": true") < 0)
            //    {
            //        ScriptManager.RegisterStartupScript(this, this.GetType(), "alerta", "alert('Não validado!');", true);
            //        return;
            //    }
            //}

            try
            {

                //usuário e senha do connectionstring

                var settings = ConfigurationManager.ConnectionStrings["ConexaoIliteraOpsa"];

                var fi = typeof(ConfigurationElement).GetField("_bReadOnly", BindingFlags.Instance | BindingFlags.NonPublic);

                fi.SetValue(settings, false);

                settings.ConnectionString = ConfigurationManager.ConnectionStrings["ConexaoIliteraOpsa"].ConnectionString + ";User ID = admin; Password = Ilitera572160x4";
                //ConfigurationManager.ConnectionStrings["ConexaoIliteraOpsa"].ConnectionString + ";User ID = sa; Password = Ilitera572160";


                settings = ConfigurationManager.ConnectionStrings["ConexaoIliteraSiedNovo"];

                fi = typeof(ConfigurationElement).GetField("_bReadOnly", BindingFlags.Instance | BindingFlags.NonPublic);

                fi.SetValue(settings, false);

                settings.ConnectionString = ConfigurationManager.ConnectionStrings["ConexaoIliteraSiedNovo"].ConnectionString + ";User ID = admin; Password = Ilitera572160x4";


                //if ( txtUsuario.Text == "COMUNIC99")
                //{


                //    string fileName = "I:\\temp\\ExameAd.xml";

                //    System.IO.StreamReader reader = new System.IO.StreamReader(fileName);
                //    string ret = reader.ReadToEnd();
                //    reader.Close();

                //    //string postData = ret;
                //    //System.Text.Encoding encoding = System.Text.Encoding.UTF8;
                //    //byte[] bytes = encoding.GetBytes(postData);
                //    //string url = "https://www.ilitera.net.br/essence_hom/Comunicacao.aspx";

                //    Session["XML"] = ret;

                //    Response.Redirect("~/Comunicacao.aspx");

                //}
                //else
                //{
                Session["XML"] = "";
                //}


                //logar

                Usuario user = UsuarioFacade.AutenticarUsuaro(txtUsuario.Text, txtSenha.Text);

                if (user.NomeUsuario != null && user.NomeUsuario != String.Empty)
                {

                    Session["usuarioLogado"] = user;
                    InicializaSessoesMenu();
                    

                    Log_Web("Login - Sistema", user.IdUsuario, "Acesso ao sistema - Web");

                    FormsAuthentication.RedirectFromLoginPage(txtUsuario.Text, false);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alerta", "alert('Usuário/senha inválidos.');", true);
                    //MsgBox1.Show("Ilitera.Net", "Usuário/senha inválidos.", null, new EO.Web.MsgBoxButton("OK"));
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alerta", String.Format("alert('{0}');", ex.Message), true);
            }
        }




        //protected void Logar()
        //{
        //    try
        //    {


        //        Usuario user = UsuarioFacade.AutenticarUsuaro(txtUsuario.Text, txtSenha2.Text);

        //        if (user.NomeUsuario != null && user.NomeUsuario != String.Empty)
        //        {

        //            Session["usuarioLogado"] = user;
        //            InicializaSessoesMenu();


        //            Log_Web("Login - Sistema", user.IdUsuario, "Acesso ao sistema - Web");

        //            FormsAuthentication.RedirectFromLoginPage(txtUsuario.Text, false);                    
        //        }
        //        else
        //        {
        //            ScriptManager.RegisterStartupScript(this, this.GetType(), "alerta", "alert('Usuário/senha inválidos.');", true);
        //            return;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "alerta", String.Format("alert('{0}');", ex.Message), true);
        //        return;
        //    }
        //}



        public string Crypt(string text)
        {
            SymmetricAlgorithm algorithm = DES.Create();
            ICryptoTransform transform = algorithm.CreateEncryptor(key, iv);
            byte[] inputbuffer = Encoding.Unicode.GetBytes(text);
            byte[] outputBuffer = transform.TransformFinalBlock(inputbuffer, 0, inputbuffer.Length);
            return Convert.ToBase64String(outputBuffer);
        }

        public string Decrypt(string text)
        {
            SymmetricAlgorithm algorithm = DES.Create();
            ICryptoTransform transform = algorithm.CreateDecryptor(key, iv);
            byte[] inputbuffer = Convert.FromBase64String(text);
            byte[] outputBuffer = transform.TransformFinalBlock(inputbuffer, 0, inputbuffer.Length);
            return Encoding.Unicode.GetString(outputBuffer);
        }



        //static public byte[] RSAEncrypt(byte[] DataToEncrypt, RSAParameters RSAKeyInfo, bool DoOAEPPadding)
        //{
        //    try
        //    {
        //        byte[] encryptedData;
        //        //Create a new instance of RSACryptoServiceProvider.
        //        using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
        //        {

        //            //Import the RSA Key information. This only needs
        //            //toinclude the public key information.
        //            RSA.ImportParameters(RSAKeyInfo);

        //            //Encrypt the passed byte array and specify OAEP padding.  
        //            //OAEP padding is only available on Microsoft Windows XP or
        //            //later.  
        //            encryptedData = RSA.Encrypt(DataToEncrypt, DoOAEPPadding);
        //        }
        //        return encryptedData;
        //    }
        //    //Catch and display a CryptographicException  
        //    //to the console.
        //    catch (CryptographicException e)
        //    {
        //        Console.WriteLine(e.Message);

        //        return null;
        //    }

        //}




        //static public byte[] RSADecrypt(byte[] DataToDecrypt, RSAParameters RSAKeyInfo, bool DoOAEPPadding)
        //{
        //    try
        //    {
        //        byte[] decryptedData;
        //        //Create a new instance of RSACryptoServiceProvider.
        //        using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
        //        {
        //            //Import the RSA Key information. This needs
        //            //to include the private key information.
        //            RSA.ImportParameters(RSAKeyInfo);

        //            //Decrypt the passed byte array and specify OAEP padding.  
        //            //OAEP padding is only available on Microsoft Windows XP or
        //            //later.  
        //            decryptedData = RSA.Decrypt(DataToDecrypt, DoOAEPPadding);
        //        }
        //        return decryptedData;
        //    }
        //    //Catch and display a CryptographicException  
        //    //to the console.
        //    catch (CryptographicException e)
        //    {
        //        Console.WriteLine(e.ToString());

        //        return null;
        //    }

        //}





        private void Log_Web(string Command,
                            int IdUsuario,
                            string ProcessoRealizado)
        {

            using (SqlConnection cnn = new SqlConnection(Ilitera.Data.SQLServer.EntitySQLServer.GetConnection()))
            {

                string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection();



                string strLog = "USE logdb exec dbo.sps_AddLog_OPSA "
                                + IdUsuario + ","
                                + "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',"
                                + "'0',"
                                + "'0',"
                                + "0,"
                                + "1,"
                                + "'" + Command.Replace("'", "''") + "',"
                                + "'" + ProcessoRealizado + "'";

                try
                {
                    //Debug.WriteLine(strLog);
                    cnn.ConnectionString = connString;
                    cnn.Open();

                    SqlCommand cmd = new SqlCommand();

                    cmd.CommandText = strLog;

                    cmd.Connection = cnn;

                    cmd.ExecuteNonQuery(); //DESENVOLVIMENTO ILITERA - linha ao lado comentada (acessava tab de logs) 29/07/10
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                }

            }

        }

        private void InicializaSessoesMenu()
        {
            Session["EmpresaExpandido"] = "n";
            Session["EmpregadoExpandido"] = "n";
            Session["DocumentosExpandido"] = "n";
            Session["CIPAExpandido"] = "n";
            Session["SaudeOcupacionalExpandido"] = "n";
            Session["AuditoriaSegurancaExpandido"] = "n";
            Session["OrdensServicoExpandido"] = "n";
            Session["TreinamentosExpandido"] = "n";
            Session["OutrasFerramentasExpandido"] = "n";
            Session["SistemaExpandido"] = "n";
        }

        protected void lnkLembrarSenha_Click(object sender, EventArgs e)
        {
            #region Comentado

            //SmtpClient client = new SmtpClient();
            //MailAddress de = new MailAddress(ConfigurationSettings.AppSettings["emailRemetente"]);
            //MailAddress para = new MailAddress("bene@ilitera.com.br");

            //MailMessage mensagem = new MailMessage(de, para);
            //mensagem.IsBodyHtml = false;

            //mensagem.Subject = "Lembrete de Senha";

            //mensagem.Body = "Texto do email";

            //client.Send(mensagem);
            #endregion

            ScriptManager.RegisterStartupScript(this, this.GetType(), "janela", "window.open('LembrarSenha.aspx', '', 'height=140, width=600, top=260, left=300');", true);
        }

        protected void btnJohnson_Click(object sender, EventArgs e)
        {

            AccountSettings accountSettings = new AccountSettings();

            Ilitera.Data.AuthRequest req = new AuthRequest("Ilitera.Net", "Consume.aspx", accountSettings);

            Response.Redirect(accountSettings.idp_sso_target_url + "?SAMLRequest=" + Server.UrlEncode(req.GetRequest(AuthRequest.AuthRequestFormat.Base64)));

        }
    }



}

