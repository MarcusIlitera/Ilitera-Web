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
using Ilitera.Common;
using Entities;
using BLL;
using System.Collections;

namespace Ilitera.Net.ControleEPI
{
    public partial class Alerta_EPI : System.Web.UI.Page
    {

        protected Prestador prestador = new Prestador();
        protected Ilitera.Common.Usuario usuario = new Ilitera.Common.Usuario();
        protected Cliente cliente = new Cliente();
        protected Ilitera.Opsa.Data.Empregado empregado = new Ilitera.Opsa.Data.Empregado();


        protected void Page_Load(object sender, System.EventArgs e)
        {


            //try
            //{
            //    string FormKey = this.Page.ToString().Substring(4);

            //    Ilitera.Common.Funcionalidade funcionalidade = new Ilitera.Common.Funcionalidade();
            //    funcionalidade.Find("ClassName='" + FormKey + "'");

            //    if (funcionalidade.Id == 0)
            //        throw new Exception("Formulário não cadastrado - " + FormKey);

            //    Entities.Usuario xUser = (Entities.Usuario)Session["usuarioLogado"];
            //    Ilitera.Common.Usuario.Permissao_Web(xUser.IdUsuario, funcionalidade.Id);
            //}

            //catch (Exception ex)
            //{
            //    Session["Message"] = ex.Message;
            //    Server.Transfer("~/Tratar_Excecao.aspx");
            //    return;
            //}

            string xUsuario = Session["usuarioLogado"].ToString();

            InicializaWebPageObjects();

            if (!IsPostBack)
            {
                Carregar_Emails();
                Carregar_Grids();
            }


            //if (!IsPostBack)
            //{

            //    if (Request.QueryString["IdUsuario"] != null
            //        && Request.QueryString["IdUsuario"] != "")
            //    {
            //        this.usuario = new Ilitera.Common.Usuario(Convert.ToInt32(Request.QueryString["IdUsuario"]));
            //        this.usuario.IdPessoa.Find();

            //        if (!this.usuario.NomeUsuario.Equals("lmm"))
            //        {
            //            this.prestador = new Prestador();
            //            this.prestador.FindByPessoa(this.usuario.IdPessoa);
            //            this.prestador.IdPessoa.Find();
            //        }
            //    }

            //    if (Request.QueryString["IdEmpresa"] != null
            //        && Request.QueryString["IdEmpresa"] != "")
            //        this.cliente = new Cliente(Convert.ToInt32(Request.QueryString["IdEmpresa"]));

            //    if (Request.QueryString["IdEmpregado"] != null
            //        && Request.QueryString["IdEmpregado"] != "")
            //        this.empregado = new Ilitera.Opsa.Data.Empregado(Convert.ToInt32(Request.QueryString["IdEmpregado"]));


            //    lblExCli.Text = "Exames / Absenteísmo";

            //}

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
            //StringBuilder st = new StringBuilder();

            //st.Append("top.window.document.frames.conteudo.window.document.getElementById('txtIdEmpregado').value = '" + empregado.Id + "';");
            //st.Append("top.window.document.getElementById('txtIdEmpregado').value = '" + Request["IdEmpregado"] + "';");
            //st.Append("top.window.document.getElementById('txtIdUsuario').value = '" + Request["IdUsuario"] + "';");
            //st.Append("top.window.document.getElementById('txtIdEmpresa').value = '" + Request["IdEmpresa"] + "';");


            //this.ClientScript.RegisterStartupScript(this.GetType(), "IdEmpregado", st.ToString(), true);
            

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
          
        }


        protected void Entrega_EPI()
        {

          

        }

        protected void chk_EPI_SelectedIndexChanged(object sender, EventArgs e)
        {

            //apagar seleção epis do cliente
            Ilitera.Data.PPRA_EPI rLista = new Ilitera.Data.PPRA_EPI();
            rLista.Excluir_EPI_Alerta(Convert.ToInt32(Request.QueryString["IdEmpresa"]));


            //salvar dados
            for (int rAux = 0; rAux < chk_EPI.Items.Count; rAux++)
            {
                if (chk_EPI.Items[rAux].Selected == true)
                {
                    rLista.Inserir_EPI_Alerta_Lista(Convert.ToInt32(Request.QueryString["IdEmpresa"]), System.Convert.ToInt32(chk_EPI.Items[rAux].Value));
                }
            }



            //recarregar grid
            Carregar_Grids();


        }


        protected void Carregar_Emails()
        {
            DataSet rDs = new DataSet();

            Ilitera.Data.PPRA_EPI rLista = new Ilitera.Data.PPRA_EPI();
            rDs = rLista.Carregar_EPI_Alerta(Convert.ToInt32(Request.QueryString["IdEmpresa"]));


            if (rDs.Tables[0].Rows.Count > 0)
            {
                txt_Dias.Text = rDs.Tables[0].Rows[0]["Dias"].ToString().Trim();
                txt_Email1.Text = rDs.Tables[0].Rows[0]["email1"].ToString().Trim();
                txt_Email2.Text = rDs.Tables[0].Rows[0]["email2"].ToString().Trim();

                if ( System.Convert.ToBoolean( rDs.Tables[0].Rows[0]["Somente_EPI"] ) == true )
                {
                    chk_EPI_Apenas.Checked = true;
                }
                else
                {
                    chk_EPI_Apenas.Checked = false;
                }
            }

            return;

        }


        protected void Carregar_Grids()
        {

            DataSet rDs = new DataSet();

            Ilitera.Data.PPRA_EPI rLista = new Ilitera.Data.PPRA_EPI();
            rDs = rLista.Gerar_Lista_Apenas_EPI_Baseado_Entrega(Convert.ToInt32(Request.QueryString["IdEmpresa"]));
            grd_Clinicos.DataSource = rDs;

            chk_EPI.Items.Clear();
            chk_EPI.DataSource = rDs;
            chk_EPI.DataValueField = "IdEPI";
            chk_EPI.DataTextField = "EPI";
            chk_EPI.DataBind();

            for (int zCont = 0; zCont < rDs.Tables[0].Rows.Count; zCont++)
            {
                if (rDs.Tables[0].Rows[zCont]["Selecao"].ToString().Trim().ToUpper() == "X")
                {

                    for (int rAux = 0; rAux < chk_EPI.Items.Count; rAux++)
                    {
                        if (rDs.Tables[0].Rows[zCont]["EPI"].ToString().Trim() == chk_EPI.Items[rAux].Text.Trim())
                        {
                            chk_EPI.Items[rAux].Selected = true;
                            break;
                        }
                    }
                }

            }


            DataSet zDs = new DataSet();

            Ilitera.Data.PPRA_EPI xLista = new Ilitera.Data.PPRA_EPI();
            zDs = xLista.Gerar_Lista_EPI_Baseado_Entrega(Convert.ToInt32(Request.QueryString["IdEmpresa"]));
            grd_Clinicos.DataSource = zDs;

            grd_Clinicos.DataBind();



        }

        protected void cmd_Salvar_Click(object sender, EventArgs e)
        {

            if (txt_Dias.Text.Trim() == "") txt_Dias.Text = "0";


            if (txt_Email1.Text.Trim() != "")
            {
                if (txt_Email1.Text.IndexOf("@") < 2)
                {
                    MsgBox2.Show("Ilitera.Net", "Formato inválido do e-mail (1) ", null,
                                  new EO.Web.MsgBoxButton("OK"));
                    return;

                }

                if (txt_Email1.Text.IndexOf(".com") < 2)
                {
                    MsgBox2.Show("Ilitera.Net", "Formato inválido do e-mail (1) ", null,
                                  new EO.Web.MsgBoxButton("OK"));
                    return;
                }
                
            }

            if (txt_Email2.Text.Trim() != "")
            {
                if (txt_Email2.Text.IndexOf("@") < 2)
                {
                    MsgBox2.Show("Ilitera.Net", "Formato inválido do e-mail (1) ", null,
                                  new EO.Web.MsgBoxButton("OK"));
                    return;

                }

                if (txt_Email2.Text.IndexOf(".com") < 2)
                {
                    MsgBox2.Show("Ilitera.Net", "Formato inválido do e-mail (1) ", null,
                                  new EO.Web.MsgBoxButton("OK"));
                    return;
                }

            }


            int n;
            bool isNumeric = int.TryParse(txt_Dias.Text.Trim(), out n);

            if (!isNumeric)
            {
                MsgBox2.Show("Ilitera.Net", "Número de dias em formato inválido.", null,
                              new EO.Web.MsgBoxButton("OK"));
                return;
            }

            int zEPI_Apenas = 0;

            if (chk_EPI_Apenas.Checked == true) zEPI_Apenas = 1;
            
            Ilitera.Data.PPRA_EPI rLista = new Ilitera.Data.PPRA_EPI();
            rLista.Inserir_EPI_Alerta(Convert.ToInt32(Request.QueryString["IdEmpresa"]), txt_Email1.Text.Trim(), txt_Email2.Text.Trim(), System.Convert.ToInt32( txt_Dias.Text.Trim() ), zEPI_Apenas);

            MsgBox2.Show("Ilitera.Net", "Dados de e-mail de alerta salvos", null,
                          new EO.Web.MsgBoxButton("OK"));
            return;


        }

    }
}
