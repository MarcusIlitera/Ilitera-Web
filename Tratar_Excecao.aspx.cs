using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using Entities;
using BLL;
using System.IO;
using System.Text;

namespace Ilitera.Net
{
    public partial class Tratar_Excecao : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //String ErrorDescription;

            // Exception ObjError = Server.GetLastError().GetBaseException();

            //Exception err = Server.GetLastError();
            //ErrorDescription = ObjError.Message.ToString();

            //ErrorDescription = Session["Message"].ToString();

            if (Session["Message"] != null)
            {
                if (Session["Message"].ToString().Trim() == "O usuário não possui permissão para acessar esta área do sistema!")
                {
                    txt_Excecao.Text = "O usuário não possui permissão para acessar esta área do sistema!";
                    return;
                }
            }

            string xFile = "Log_Erro_" + System.DateTime.Now.Year.ToString().Trim() + "_" + System.DateTime.Now.Month.ToString().Trim() + "_" + System.DateTime.Now.Day.ToString().Trim() + "_" + System.DateTime.Now.Hour.ToString().Trim() + System.DateTime.Now.Minute.ToString().Trim() + System.DateTime.Now.Second.ToString().Trim() + "_" + System.DateTime.Now.Millisecond.ToString().Trim() + ".log";
            string myStringWebResource = "I:\\temp\\" + xFile;


            txt_Erro.Text = xFile; 

                //Session["Message"].ToString();
            
             if ( Session["ExceptionType"] != null )     txt_Excecao.Text = Session["ExceptionType"].ToString();

             if (Session["Source"] != null)              txt_Source.Text = Session["Source"].ToString();


            string zLinha = "";

            


            TextWriter tw = new StreamWriter(myStringWebResource, false, Encoding.GetEncoding(1252));

            zLinha = Session["Message"].ToString();
            tw.WriteLine(zLinha);

            zLinha = Session["ExceptionType"].ToString();
            tw.WriteLine(zLinha);
            
            zLinha = Session["Source"].ToString();
            tw.WriteLine(zLinha);


            tw.Close();



        }

        protected void btnConfirmarAlteracao_Click(object sender, EventArgs e)
        {

        }

          
      
    }
}