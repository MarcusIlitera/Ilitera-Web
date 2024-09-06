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
using System.Xml.Linq;
using System.IO;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Ilitera.Common;
using Ilitera.PCMSO.Report;
using Ilitera.Opsa.Report;
using Entities;
using BLL;
using System.Collections;
using EO.Web;

namespace Ilitera.Net
{
    public partial class EPIRepositorio : System.Web.UI.Page
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


            InicializaWebPageObjects();


            hlkNovo.Visible = false;
            

        
            
         

            if (!IsPostBack)
            {
                PopulaDDLColaboradores();

               // DataSet dsaux = new Pcmso().Get("IdCliente=" + Session["Empresa"].ToString()
               // + " AND IsFromWeb=0"
               // + " ORDER BY DataPcmso DESC");   
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
            ////base.InicializaWebPageObjects();
            //StringBuilder st = new StringBuilder();

            ////st.Append("top.window.document.frames.conteudo.window.document.getElementById('txtIdEmpregado').value = '" + empregado.Id + "';");
            //st.Append("top.window.document.getElementById('txtIdEmpregado').value = '" + Request["IdEmpregado"] + "';");
            //st.Append("top.window.document.getElementById('txtIdUsuario').value = '" + Request["IdUsuario"] + "';");
            //st.Append("top.window.document.getElementById('txtIdEmpresa').value = '" + Request["IdEmpresa"] + "';");


            //this.ClientScript.RegisterStartupScript(this.GetType(), "IdEmpregado", st.ToString(), true);

        }



        private DataSet DSQuestionario()
        {   
            DataSet zDs = new Ilitera.Opsa.Data.EPIRepositorio().Get("IdEmpregado=" + ddlEmpregado.SelectedValue.ToString() + " ORDER BY DataHora DESC");
                       
            return zDs;

        }





        protected void cmd_Voltar_Click(object sender, EventArgs e)
        {

            Entities.Usuario user = (Entities.Usuario)Session["usuarioLogado"];
            Response.Redirect("~\\ListaEmpregados.aspx?&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString());

        }

        protected void btnFichaCompleta_Click(object sender, EventArgs e)
        {

        }


        protected void grd_Clinicos_ItemCommand(object sender, EO.Web.GridCommandEventArgs e)
        {
            //Check whether it is from our client side
            //JavaScript call
            string zAcao = "V";


            if (e.CommandName.ToString().Trim() == "2")
                zAcao = "E";  //editar
            else
                zAcao = "V";  //visualizar


            //Session["Empregado"] = e.Item.Key.ToString();
            //Session["NomeEmpregado"] = e.Item.Cells[0].Value.ToString();
           // string zTipo = Request["Tipo"].ToString().Trim();

         
            Response.Redirect("RepositorioEPI.aspx?IdEPIRepositorio=" + e.Item.Key.ToString() + "&IdEmpregado=" + ddlEmpregado.SelectedValue.ToString() +  "&Acao=" + zAcao);
          

        }


        private void PopulaDDLColaboradores()
        {

            ddlEmpregado.DataSource = new Ilitera.Opsa.Data.Empregado().Get("nID_EMPR=" + Convert.ToInt32(Session["Empresa"].ToString()) + " ORDER BY tNO_EMPG");
            ddlEmpregado.DataValueField = "nID_EMPREGADO";
            ddlEmpregado.DataTextField = "tNO_EMPG";
            ddlEmpregado.DataBind();
            ddlEmpregado.Items.Insert(0, new ListItem("Selecione o Empregado...", "0"));


            if ( Request["IdEmpregado"].ToString().Trim() != "0" )
            {
                ddlEmpregado.SelectedValue = Request["IdEmpregado"].ToString().Trim();
            }


            object xSender = new object();
            System.EventArgs xE = new System.EventArgs();

            ddlEmpregado_SelectedIndexChanged(xSender, xE);
            return;

        }



        protected void ddlEmpregado_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            //PopulaUltraWebGrid(UltraWebGridReuniaoCipa, GeraDataSet(), lblTotRegistros);

            if (ddlEmpregado.SelectedIndex <=0)
            {

                hlkNovo.Visible = false;

                grd_Clinicos.DataSource = null;
                grd_Clinicos.Items.Clear();


            }
            else
            {
                grd_Clinicos.DataSource = DSQuestionario();
                grd_Clinicos.DataBind();

                hlkNovo.Visible = true;
                hlkNovo.NavigateUrl = "javascript:void(window.location.href ='RepositorioEPI.aspx?IdEPIRepositorio=0&IdEmpregado=" + ddlEmpregado.SelectedValue.ToString() + "&Acao=E')";
                

            }

        }




    }
}
