
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

using System.Web.Services;
using System.Data.SqlClient;
   
using System.Collections.Generic;


namespace Ilitera.Net
{
    public partial class Grafico_Google2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {

                    
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

            DataSet ds = xEPI.Trazer_PAE(System.Convert.ToInt32(Session["Empresa"].ToString()));


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
                //    //Response.Write("window.open('" + ddlLaudos2.SelectedValue.ToString().Trim().Replace("C:\\DRIVE_I", "http://54.94.157.244/driveI").Replace("\\", "/") + "', '_newtab');");
                //    Response.Write("window.open('" + ddlLaudos2.SelectedValue.ToString().Trim().ToUpper().Replace("C:\\DRIVE_I", "https://www.ilitera.net.br/driveI").Replace("\\", "/") + "', '_newtab');");
                //    Response.Write("</script>");
                //}
                //else
                //{
                //    //AbreMapa("MapaRisco", ddlLaudos2.SelectedValue.ToString().Trim().Replace("C:\\DRIVE_I", "http://187.45.232.35/driveI").Replace("\\", "/"));
                //    Response.Write("<script>");
                //    //Response.Write("window.open('" + ddlLaudos2.SelectedValue.ToString().Trim().Replace("I:\\FOTOSDOCSDIGITAIS", "http://54.94.157.244/driveI/fotosdocsdigitais").Replace("\\", "/") + "', '_newtab');");
                //    Response.Write("window.open('" + ddlLaudos2.SelectedValue.ToString().Trim().ToUpper().Replace("I:\\FOTOSDOCSDIGITAIS", "https://www.ilitera.net.br/driveI/fotosdocsdigitais").Replace("\\", "/") + "', '_newtab');");
                //    Response.Write("</script>");
                //    //}
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


        private void AbreMapa(string page, string xPath)
        {
            Guid strAux = Guid.NewGuid();

            OpenReport("Documentos", page + ".aspx?IliteraSystem=" + strAux.ToString() + strAux.ToString()
                    + "&Arquivo=" + xPath + "&IdUsuario=" + Request["IdUsuario"], page);
        }


        protected void OpenReport(string directory, string fileAndQuery, string ReportName)
        {
            this.OpenReport(directory, fileAndQuery, ReportName, false);
        }

        protected void OpenReport(string directory, string fileAndQuery, string ReportName, bool useDirectoryForLocalProcess)
        {
            StringBuilder st = new StringBuilder();

            Guid strAux = Guid.NewGuid();

            string valueProcess = "Local";

            if (valueProcess.Equals("Remote"))
                st.Append("void(window.open('http://report.ilitera.net/index.aspx?Identity=" + strAux.ToString() + "opsa" + strAux.ToString()
                    + "&Key=" + strAux.ToString() + "IliteraNet" + strAux.ToString()
                    + "&PathAndQuery=" + HttpUtility.UrlEncode(directory + "/" + fileAndQuery) + "', '" + ReportName + "', 'scrollbars=yes, resizable=yes, toolbar=no, addressbar=no, menubar=no, width=800px, height=600px'));");
            else if (valueProcess.Equals("Local"))
            {
                if (useDirectoryForLocalProcess)
                {
                    //st.Append("void(window.open('../" + directory + "/" + fileAndQuery + "', '" + ReportName + "', 'scrollbars=yes, resizable=yes, toolbar=no, addressbar=no, menubar=no, width=800px, height=600px'));");
                    st.AppendFormat("void(window.open('../{0}/{1}', '{2}','scrollbars=yes, resizable=yes, toolbar=no, addressbar=no, menubar=no, width=800px, height=600px'));", directory, fileAndQuery, ReportName);
                }
                else
                {
                    //st.Append("void(window.open('" + fileAndQuery + "', '" + ReportName + "', 'scrollbars=yes, resizable=yes, toolbar=no, addressbar=no, menubar=no, width=800px, height=600px'));");
                    st.AppendFormat("void(window.open('{0}','{1}', 'scrollbars=yes, resizable=yes, toolbar=no, addressbar=no, menubar=no, width=800px, height=600px'));", fileAndQuery, ReportName);
                }
            }

            ScriptManager.RegisterStartupScript(this, this.GetType(), String.Format("OpenReport{0}", ReportName), st.ToString(), true);
        }




        [WebMethod]
        public static List<object> GetChartData()
        {
            string query = "SELECT ShipCity, COUNT(orderid) TotalOrders";
            query += " FROM Orders WHERE ShipCountry = 'USA' GROUP BY ShipCity";
            string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            List<object> chartData = new List<object>();
            chartData.Add(new object[]
            {
                "ShipCity", "TotalOrders"
            });
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            chartData.Add(new object[]
                            {
                                sdr["ShipCity"], sdr["TotalOrders"]
                            });
                        }
                    }
                    con.Close();
                    return chartData;
                }
            }
        }






    }
}


//using System;
//using System.Linq;
//using System.Web;
//using System.Web.UI;
//using System.Web.UI.WebControls;

//using System.Data;
//using System.Web.Services;
//using System.Configuration;
//using System.Data.SqlClient;

//using System.Collections.Generic;

//namespace Ilitera.Net
//{
//    public partial class Grafico_Google2 : System.Web.UI.Page
//    {
//        protected void Page_Load(object sender, EventArgs e)
//        {

//        }


//        [WebMethod]
//        public static List<object> GetChartData()
//        {
//            string query = "SELECT ShipCity, COUNT(orderid) TotalOrders";
//            query += " FROM Orders WHERE ShipCountry = 'USA' GROUP BY ShipCity";
//            string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
//            List<object> chartData = new List<object>();
//            chartData.Add(new object[]
//            {
//        "ShipCity", "TotalOrders"
//            });
//            using (SqlConnection con = new SqlConnection(constr))
//            {
//                using (SqlCommand cmd = new SqlCommand(query))
//                {
//                    cmd.CommandType = CommandType.Text;
//                    cmd.Connection = con;
//                    con.Open();
//                    using (SqlDataReader sdr = cmd.ExecuteReader())
//                    {
//                        while (sdr.Read())
//                        {
//                            chartData.Add(new object[]
//                            {
//                        sdr["ShipCity"], sdr["TotalOrders"]
//                            });
//                        }
//                    }
//                    con.Close();
//                    return chartData;
//                }
//            }
//        }





//    }
//}


