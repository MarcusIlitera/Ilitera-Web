using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Xml;

//using OneLogin.Saml;
using Ilitera.Data;

namespace Ilitera.Net.Documentos
{
    public partial class Consume : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            // replace with an instance of the users account.
            AccountSettings accountSettings = new AccountSettings();

            Ilitera.Data.Response samlResponse = new Response(accountSettings);

            string xResponse = Request.Form["SAMLResponse"];

            if (xResponse==null)
            {

                //Response.Redirect("https://www.ilitera.net.br/johnson/Login.aspx?nome=SGPelle@scj.com&senha=1111");
                //txt_Status.Text = "Login Failed";
                Session["Pagina"] = "1";
                Session["Empregado"] = String.Empty;
                Session["NomeEmpregado"] = String.Empty;
                Session["NomeEmpresa"] = String.Empty;
                Session["Preposto"] = String.Empty;
                Session["Preposto2"] = String.Empty;



                Session["Filtro_Nome"] = "";
                Session["Filtro_Empresa"] = "";
                Session["Filtro_Tipo"] = "";
                Session["Filtro_Setor"] = "";

                Page.ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.open('close.html', '_self', null);", true);

                return;
                
            }
            else
            {
                samlResponse.LoadXmlFromBase64(xResponse);
            }

            if (samlResponse.IsValid())
            {
                //Response.Write("OK!");
                //Response.Write(samlResponse.GetNameID());
                //txt_Status.Text = "Login OK - " + samlResponse.GetNameID();
                string xAcesso = samlResponse.GetNameID().Trim().ToUpper();

                if (xAcesso == "ECVIDAL@SCJ.COM")
                {
                    Response.Redirect("https://www.ilitera.net.br/johnson/Login.aspx?nome=" + samlResponse.GetNameID().Trim() + "&senha=4681");
                }
                else if (xAcesso == "KCSILVA@SCJ.COM")
                {
                    Response.Redirect("https://www.ilitera.net.br/johnson/Login.aspx?nome=" + samlResponse.GetNameID().Trim() + "&senha=9355");
                }
                else if (xAcesso == "FOSILVA@SCJ.COM")
                {
                    Response.Redirect("https://www.ilitera.net.br/johnson/Login.aspx?nome=" + samlResponse.GetNameID().Trim() + "&senha=6058");
                }
                else
                {
                    Response.Redirect("https://www.ilitera.net.br/johnson/Login.aspx?nome=" + samlResponse.GetNameID().Trim() + "&senha=1111");
                }
            }
            else
            {
                //txt_Status.Text = "Login Failed";
                Session["Pagina"] = "1";
                Session["Empregado"] = String.Empty;
                Session["NomeEmpregado"] = String.Empty;
                Session["NomeEmpresa"] = String.Empty;
                Session["Preposto"] = String.Empty;
                Session["Preposto2"] = String.Empty;



                Session["Filtro_Nome"] = "";
                Session["Filtro_Empresa"] = "";
                Session["Filtro_Tipo"] = "";
                Session["Filtro_Setor"] = "";

                Page.ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.open('close.html', '_self', null);", true);

                return;
            }
        }
    }
}