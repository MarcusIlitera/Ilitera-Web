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
using System.IO;
using Ilitera.Common;

namespace Ilitera.Net
{
    public partial class Toxicologico_Configuracao : System.Web.UI.Page
    {


        protected  Entities.Usuario usuario = new Entities.Usuario();
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

                        cliente.Find(System.Convert.ToInt32(Session["Empresa"].ToString()));

                        if (!cliente.ContrataPCMSO.Equals((int)TipoPcmsoContratada.NaoContratada))
                        {
                           
                            PopulaDDLPCMSO();
                            
                        }
                        else
                            MsgBox1.Show("PCMSO", "A empresa " + cliente.NomeCompleto + " não possui o PCMSO contratado com a Ilitera!", null,
                                            new EO.Web.MsgBoxButton("OK"));


                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alerta", String.Format("alert('{0}');", ex.Message), true);
            }
        }

        private void PopulaDDLPCMSO()
        {
            DataSet ds = new DataSet();
            DataRow rowds;

            DataTable table = new DataTable("Default");

            table.Columns.Add("IdPcmso", Type.GetType("System.String"));
            table.Columns.Add("DataPcmso", Type.GetType("System.String"));

            ds.Tables.Add(table);

            DataSet dsaux = new Pcmso().Get("IdCliente=" + Session["Empresa"].ToString()
                + " AND IsFromWeb=0"
                + " ORDER BY DataPcmso DESC");

            foreach (DataRow row in dsaux.Tables[0].Select())
            {
                rowds = ds.Tables[0].NewRow();

                Pcmso pcmso = new Pcmso();
                pcmso.Find(Convert.ToInt32(row["IdPcmso"]));

                rowds["IdPcmso"] = row["IdPcmso"];
                rowds["DataPcmso"] = pcmso.GetPeriodo();

                ds.Tables[0].Rows.Add(rowds);
            }

            ddlPCMSO2.DataSource = ds;
            ddlPCMSO2.DataValueField = "IdPcmso";
            ddlPCMSO2.DataTextField = "DataPcmso";
            ddlPCMSO2.DataBind();

            //ddlPCMSO2.Items.Insert(0, new Infragistics.Web.UI.ListControls.DropDownItem("Selecione...", "0"));

            if (dsaux.Tables[0].Rows.Count > 0)
            {
                ddlPCMSO2.SelectedIndex = 0;
               

               
            }
        }

        private void AbrePCMSO(string page)
        {

            Entities.Usuario user = (Entities.Usuario)Session["usuarioLogado"];

        }
        
      


      

   

      
      
    }
}
