using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using Entities;
using Facade;

namespace Ilitera.Net
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {

            }


            if ( Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString() == "" )
            {
                Ilitera.Data.SQLServer.EntitySQLServer.xDB1 = ConfigurationManager.AppSettings["DB1"].ToString();
                Ilitera.Data.SQLServer.EntitySQLServer.xDB2 = ConfigurationManager.AppSettings["DB2"].ToString();
                Ilitera.Data.SQLServer.EntitySQLServer._LocalServer = ConfigurationManager.AppSettings["LocalServer"].ToString();
                Session["Pagina"] = "1";
                Session["Empregado"] = String.Empty;
                Session["NomeEmpregado"] = String.Empty;
                Session["NomeEmpresa"] = String.Empty;
                Session["Preposto"] = String.Empty;
                Session["Preposto2"] = String.Empty;

            }

            Session["Filtro_Nome"] = "";
            Session["Filtro_Matricula"] = "";
            Session["Filtro_Empresa"] = "";
            Session["Filtro_Tipo"] = "";
            Session["Filtro_Setor"] = "";

            if (Session["NomeEmpresa"] == null)                                 Response.Redirect("ListaEmpresas2.aspx");

            if ( Session["NomeEmpresa"].ToString() == String.Empty )            Response.Redirect("ListaEmpresas2.aspx");



        }

      
        

    }
}
