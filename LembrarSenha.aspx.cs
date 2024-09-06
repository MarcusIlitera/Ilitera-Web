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
using Entities;
using BLL;
using System.Net.Mail;
using System.Text;

namespace Ilitera.Net
{
    public partial class LembrarSenha : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnEnviaSenha_Click(object sender, EventArgs e)
        {
            try
            {
                UsuarioBLL usuarioBll = new UsuarioBLL();
                
                var usuario = usuarioBll.VerificarEmail(txtNomeUsuario.Text);

                if (usuario.Email == null )
                {
                    MsgBox1.Show("Lembrete de senha", "Usuário não tem e-mail cadastrado", null,
                    new EO.Web.MsgBoxButton("OK"));
                    return;
                }


                if (usuario.Email.Trim() == "")
                {
                    MsgBox1.Show("Lembrete de senha", "Usuário não tem e-mail cadastrado", null,
                    new EO.Web.MsgBoxButton("OK"));
                    return;
                }

                if (usuario.Senha.Trim() != String.Empty )
                {
                    System.Net.NetworkCredential smtpCred = new System.Net.NetworkCredential("wagner.sp.sto@ilitera.com.br", "bibi6096");

                    SmtpClient smtpClient = new SmtpClient("smtp.ilitera.com.br",587);
                    smtpClient.Credentials = smtpCred;

                    MailAddress de = new MailAddress("wagner.sp.sto@ilitera.com.br"); //ConfigurationSettings.AppSettings["emailRemetente"]);
                    MailAddress para = new MailAddress(usuario.Email);

                    MailMessage mensagem = new MailMessage(de, para);
                    
                    mensagem.IsBodyHtml = true;

                    mensagem.Subject = "Lembrete de Senha";

                    StringBuilder sbMensagem = new StringBuilder();

                    sbMensagem.Append("Conforme foi solicitado através do sistema Ilitera segue abaixo seus dados de ");
                    sbMensagem.Append("acesso ao sistema Ilitera. <br />");
                    sbMensagem.AppendFormat("Usuário: {0} <br />", usuario.NomeUsuario);
                    sbMensagem.AppendFormat("Senha: {0} <br /><br />", usuario.Senha);
                    sbMensagem.Append("Obrigado.");

                    mensagem.Body = sbMensagem.ToString();

                    

                    smtpClient.Send(mensagem);

                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "mensagem", "alert('Senha enviada com sucesso!');", true);
                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "fechar", "this.window.close();", true);

                    MsgBox1.Show("Lembrete de senha", "Senha enviada com sucesso", null,
                        new EO.Web.MsgBoxButton("OK"));

                }
            }
            catch (Exception ex)
            {
                MsgBox1.Show("Lembrete de senha", "Problema :" + ex.Message, null,
                new EO.Web.MsgBoxButton("OK"));
            }
        }
    }
}