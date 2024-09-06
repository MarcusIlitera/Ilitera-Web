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

namespace Ilitera.Net
{
    public partial class CheckMapas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
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
                    if (Session["Empresa"] != null && Session["Empresa"].ToString() != String.Empty)
                    {
                        PopulaDDLLaudos();
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alerta", String.Format("alert('{0}');", ex.Message), true);
            }
        }

        private void PopulaDDLLaudos()
        {

            Ilitera.Data.PPRA_EPI xEPI = new Ilitera.Data.PPRA_EPI();

            DataSet ds = xEPI.Trazer_Mapa_Riscos( System.Convert.ToInt32( Session["Empresa"].ToString() )  );


            //ddlLaudos2.Items.Add("Selecione um Mapa de Risco...");

            ddlLaudos2.DataSource = ds;            
            ddlLaudos2.DataValueField = "Path";
            ddlLaudos2.DataTextField = "Descricao";
            ddlLaudos2.DataBind();

            

            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlLaudos2.SelectedIndex = 0;
            }


        }

                  



        protected void btnProjeto_Click(object sender, EventArgs e)
        {
             
            if (ddlLaudos2.SelectedValue.ToString().Trim() == "0")
            {
                return;
            }

            try
            {

                //if (Session["Servidor_Web"].ToString().Trim().ToUpper() == "ILITERA")
                //{
                //    if (ddlLaudos2.SelectedValue.ToString().Trim().IndexOf("C:\\DRIVE_I") >= 0)
                //    {
                //        //AbreMapa("MapaRisco", ddlLaudos2.SelectedValue.ToString().Trim().Replace("C:\\DRIVE_I", "http://ilitera.dyndns.ws:8888/driveI").Replace("\\", "/"));
                //        Response.Write("<script>");
                //        Response.Write("window.open('" + ddlLaudos2.SelectedValue.ToString().Trim().Replace("C:\\DRIVE_I", "http://ilitera.dyndns.ws:8888/driveI").Replace("\\", "/") + "', '_newtab');");
                //        Response.Write("</script>");

                //    }
                //    else
                //    {
                //        //AbreMapa("MapaRisco", ddlLaudos2.SelectedValue.ToString().Trim().Replace("I:\\FOTOSDOCSDIGITAIS", "http://ilitera.dyndns.ws:8888/driveI/fotosdocsdigitais").Replace("\\", "/"));
                //        Response.Write("<script>");
                //        Response.Write("window.open('" + ddlLaudos2.SelectedValue.ToString().Trim().Replace("I:\\FOTOSDOCSDIGITAIS", "http://ilitera.dyndns.ws:8888/driveI/fotosdocsdigitais").Replace("\\", "/") + "', '_newtab');");
                //        Response.Write("</script>");

                //    }
                //}
                //else


                //if (ddlLaudos2.SelectedValue.ToString().Trim().IndexOf("C:\\DRIVE_I") >= 0)
                //{
                //    //AbreMapa("MapaRisco", ddlLaudos2.SelectedValue.ToString().Trim().Replace("C:\\DRIVE_I", "http://187.45.232.35/driveI").Replace("\\", "/"));
                //    Response.Write("<script>");
                //    Response.Write("window.open('" + ddlLaudos2.SelectedValue.ToString().Trim().ToUpper().Replace("C:\\DRIVE_I", "https://www.ilitera.net.br/driveI").Replace("\\", "/") + "', '_newtab');");
                //    Response.Write("</script>");
                //}
                //else
                //{
                //    //AbreMapa("MapaRisco", ddlLaudos2.SelectedValue.ToString().Trim().Replace("C:\\DRIVE_I", "http://187.45.232.35/driveI").Replace("\\", "/"));
                //    //AbreMapa("MapaRisco", ddlLaudos2.SelectedValue.ToString().Trim().Replace("I:\\FOTOSDOCSDIGITAIS", "http://www.ilitera.net.br/driveI/fotosdocsdigitais").Replace("\\", "/"));
                //    Response.Write("<script>");
                //    Response.Write("window.open('" + ddlLaudos2.SelectedValue.ToString().Trim().ToUpper().Replace("I:\\FOTOSDOCSDIGITAIS", "https://www.ilitera.net.br/driveI/fotosdocsdigitais").Replace("\\", "/") + "', '_newtab');");
                //    Response.Write("</script>");

                //    //ScriptManager.RegisterStartupScript(this, this.GetType(), "alerta", "<script> window.open('" + ddlLaudos2.SelectedValue.ToString().Trim().Replace("I:\\FOTOSDOCSDIGITAIS", "http://www.ilitera.net.br/driveI/fotosdocsdigitais").Replace("\\", "/") + "', '_newtab'); </script>", true);
                //}

                string xArq = ddlLaudos2.SelectedValue.ToString().Trim().Replace("C:", "I:");


                System.Net.WebClient client = new System.Net.WebClient();
                Byte[] buffer = client.DownloadData(xArq);
                Response.ContentType = "application/pdf";
                Response.AddHeader("Content-Disposition", "attachment; filename=" + xArq);
                Response.AddHeader("content-length", buffer.Length.ToString());
                Response.BinaryWrite(buffer);
                Response.End();


            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alerta", String.Format("alert('{0}');", ex.Message), true);
            }

           


        }


   




    }
}
