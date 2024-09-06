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
    public partial class DadosEmpregado_Questionario : System.Web.UI.Page
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
            //PreencheLabels("lblEmp", cliente.NomeAbreviado.ToString());

            string zTipo = Request["Tipo"].ToString().Trim();

            PopulaTelaEmpregado();



            Entities.Usuario zUser = (Entities.Usuario)Session["usuarioLogado"];

            Ilitera.Common.Pessoa zPessoa = new Ilitera.Common.Pessoa();
            zPessoa.Find(zUser.IdPessoa);

            Prestador xPrestador = new Prestador();

            xPrestador = new Prestador();
            xPrestador.FindByPessoa(zPessoa);
            xPrestador.IdPessoa.Find();


            hlkNovo.NavigateUrl = "javascript:void(window.location.href ='ExameQuestionario.aspx?IdEmpregado=" + Request["IdEmpregado"] + "&IdUsuario=" + Request["IdUsuario"] + "&IdEmpresa=" + Request["IdEmpresa"] + "&Acao=E')";

            grd_Clinicos.DataSource = DSQuestionario();



            if (xPrestador.IndTipoPrestador == (int)TipoPrestador.Medico || zPessoa.NomeAbreviado.ToUpper().IndexOf("ILITERA") >= 0 || zPessoa.NomeAbreviado.ToUpper().IndexOf("PRAJNA") >= 0)
            {
                Ilitera.Data.Clientes_Funcionarios xLista = new Ilitera.Data.Clientes_Funcionarios();

                DataSet zDs = new DataSet();
                //zDs = xLista.Gerar_Lista_Exames(Convert.ToInt32(Request.QueryString["IdEmpregado"]));

                //grd_Clinicos.DataSource = zDs;

                //grd_Clinicos.DataBind();


                //carregar exames
                zDs = xLista.Gerar_Lista_ExamesMedicos(Convert.ToInt32(Request.QueryString["IdEmpregado"]));

                gridExames.DataSource = zDs;
                gridExames.DataBind();


            }
            else
            {
                MsgBox1.Show("Ilitera.Net", "Usuário sem acesso à dados médicos.", null,
                             new EO.Web.MsgBoxButton("OK"));
                return;

            }


            if (!IsPostBack)
            {

                if (Request.QueryString["IdUsuario"] != null
                    && Request.QueryString["IdUsuario"] != "")
                {
                    this.usuario = new Ilitera.Common.Usuario(Convert.ToInt32(Request.QueryString["IdUsuario"]));
                    this.usuario.IdPessoa.Find();

                    if (!this.usuario.NomeUsuario.Equals("lmm"))
                    {
                        this.prestador = new Prestador();
                        this.prestador.FindByPessoa(this.usuario.IdPessoa);
                        this.prestador.IdPessoa.Find();
                    }
                }

                if (Request.QueryString["IdEmpresa"] != null
                    && Request.QueryString["IdEmpresa"] != "")
                    this.cliente = new Cliente(Convert.ToInt32(Request.QueryString["IdEmpresa"]));

                if (Request.QueryString["IdEmpregado"] != null
                    && Request.QueryString["IdEmpregado"] != "")
                    this.empregado = new Ilitera.Opsa.Data.Empregado(Convert.ToInt32(Request.QueryString["IdEmpregado"]));


                //    lblExCli.Text = "Exames / Absenteísmo";
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
            //base.InicializaWebPageObjects();
            StringBuilder st = new StringBuilder();

            //st.Append("top.window.document.frames.conteudo.window.document.getElementById('txtIdEmpregado').value = '" + empregado.Id + "';");
            st.Append("top.window.document.getElementById('txtIdEmpregado').value = '" + Request["IdEmpregado"] + "';");
            st.Append("top.window.document.getElementById('txtIdUsuario').value = '" + Request["IdUsuario"] + "';");
            st.Append("top.window.document.getElementById('txtIdEmpresa').value = '" + Request["IdEmpresa"] + "';");


            this.ClientScript.RegisterStartupScript(this.GetType(), "IdEmpregado", st.ToString(), true);

        }



        private DataSet DSQuestionario()
        {
            Ilitera.Data.Clientes_Funcionarios xListaq = new Ilitera.Data.Clientes_Funcionarios();

            DataSet zDs = new DataSet();
            //zDs = xLista.Gerar_Lista_Exames(Convert.ToInt32(Request.QueryString["IdEmpregado"]));

            //grd_Clinicos.DataSource = zDs;

            //grd_Clinicos.DataBind();


            //carregar exames
            zDs = xListaq.Busca_Questionario(Convert.ToInt32(Request.QueryString["IdEmpregado"]));

            return zDs;

        }




        private void PopulaTelaEmpregado()
        {
            //variável empregado está vazia.  Ver como carregá-lo. - Wagner  
            empregado.Find(" tblEMPREGADO.nID_EMPREGADO = " + Request["IdEmpregado"].ToString());
            EmpregadoFuncao empregadoFuncao = EmpregadoFuncao.GetEmpregadoFuncao(empregado);


            lblValorNome.Text = empregado.tNO_EMPG;



            if (empregado.hDT_ADM.ToString("dd-MM-yyyy").Equals("01-01-1753") || empregado.hDT_ADM == new DateTime())
                lblValorAdmissao.Text = "&nbsp;";
            else
                lblValorAdmissao.Text = empregado.hDT_ADM.ToString("dd-MM-yyyy");

            if (empregado.IdadeEmpregado() != 0)
                lblValorIdade.Text = empregado.IdadeEmpregado().ToString();
            else
                lblValorIdade.Text = "&nbsp;";

            if (empregado.tSEXO.Trim() != "" && empregado.tSEXO != "S")
                if (empregado.tSEXO == "M")
                    lblValorSexo.Text = "Masculino";
                else if (empregado.tSEXO == "F")
                    lblValorSexo.Text = "Feminino";
                else
                    lblValorSexo.Text = "&nbsp;";



            if (empregado.hDT_DEM.ToString("dd-MM-yyyy").Equals("01-01-1753") || empregado.hDT_DEM == new DateTime())
                lblValorDemissao.Text = "&nbsp;";
            else
                lblValorDemissao.Text = empregado.hDT_DEM.ToString("dd-MM-yyyy");



            lblValorTempoEmpresa.Text = empregado.TempoEmpresaEmpregado();
            lblValorFuncao.Text = empregadoFuncao.GetNomeFuncao();
            lblValorSetor.Text = empregadoFuncao.GetNomeSetor();



            if (empregadoFuncao.hDT_INICIO == new DateTime() || empregadoFuncao.hDT_INICIO == new DateTime(1753, 1, 1))
                lblValorDataIni.Text = "&nbsp;";
            else
                lblValorDataIni.Text = empregadoFuncao.hDT_INICIO.ToString("dd-MM-yyyy");
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

         
            Response.Redirect("ExameQuestionario.aspx?IdEmpregado=" + Request["IdEmpregado"] + "&IdExame=" + e.Item.Key.ToString() + "&IdUsuario=" + Request["IdUsuario"] + "&IdEmpresa=" + Request["IdEmpresa"] + "&Acao=" + zAcao);
          

        }


        protected void grdExames_ItemCommand(object sender, EO.Web.GridCommandEventArgs e)
        {
            //Check whether it is from our client side
            //JavaScript call

            //guardar dados do empregado
            //string xComando = e.CommandName.Trim();
            string xId = "";

            int xLinha = ((EO.Web.Grid)sender).SelectedItemIndex;
            int xColuna = ((EO.Web.Grid)sender).SelectedCellIndex;


            xId = e.Item.Cells[xColuna + 6].Value.ToString();


            if (xColuna < 6)
            {
                if (xId.ToString().Trim() == "" || xId.ToString().Trim() == "0")
                    return;
            }


            if (xColuna == 1)
            {

                Entities.Usuario xUser = (Entities.Usuario)Session["usuarioLogado"];

                Ilitera.Common.Pessoa zPessoa = new Ilitera.Common.Pessoa();
                zPessoa.Find(xUser.IdPessoa);

                Prestador xPrestador = new Prestador();

                xPrestador = new Prestador();
                xPrestador.FindByPessoa(zPessoa);
                xPrestador.IdPessoa.Find();

                if (xPrestador.IndTipoPrestador == (int)TipoPrestador.Medico)
                {
                    Session["Retorno"] = "91";
                    Response.Redirect("ExameClinico.aspx?IdEmpregado=" + Request["IdEmpregado"] + "&IdExame=" + xId.ToString() + "&IdUsuario=" + Request["IdUsuario"] + "&IdEmpresa=" + Request["IdEmpresa"] + "&Acao=V");
                }
                else
                {
                    MsgBox1.Show("Ilitera.Net", "Visualização deste exame apenas por prestadores com perfil médico!", null,
                                 new EO.Web.MsgBoxButton("OK"));
                    return;
                }


            }
            else if (xColuna == 2)
            {

                Entities.Usuario xUser = (Entities.Usuario)Session["usuarioLogado"];

                Ilitera.Common.Pessoa zPessoa = new Ilitera.Common.Pessoa();
                zPessoa.Find(xUser.IdPessoa);

                Prestador xPrestador = new Prestador();

                xPrestador = new Prestador();
                xPrestador.FindByPessoa(zPessoa);
                xPrestador.IdPessoa.Find();

                if (xPrestador.IndTipoPrestador == (int)TipoPrestador.Medico)
                {
                    Session["Retorno"] = "91";
                    Response.Redirect("ExameNaoOcupacional.aspx?IdEmpregado=" + Request["IdEmpregado"] + "&IdExame=" + xId.ToString() + "&IdUsuario=" + Request["IdUsuario"] + "&IdEmpresa=" + Request["IdEmpresa"] + "&Acao=V");
                }
                else
                {
                    MsgBox1.Show("Ilitera.Net", "Visualização deste exame apenas por prestadores com perfil médico!", null,
                                 new EO.Web.MsgBoxButton("OK"));
                    return;
                }

            }
            else if (xColuna == 3)
            {
                Session["Retorno"] = "91";
                string sScript = "javascript:void(addItem(centerWin('CadAbsentismo.aspx?IdEmpregado=" + Session["Empregado"].ToString() + "&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + Request["IdUsuario"] + "&IdAfastamento=" + xId.ToString() + "', 650, 650, 'CadAcidente'), 'Acidente'))";
                //System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", sScript, true);
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "MyScript", sScript, true);
                System.Web.UI.ScriptManager scriptManager = System.Web.UI.ScriptManager.GetCurrent(Page);
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(
               this,
               this.GetType(),
               "CadAcidente",
               sScript,
               true);


            }
            else if (xColuna == 4)
            {

                Session["Retorno"] = "91";
                string sScript = "javascript:void(addItem(centerWin('CadAbsentismo.aspx?IdEmpregado=" + Session["Empregado"].ToString() + "&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + Request["IdUsuario"] + "&IdAfastamento=" + xId.ToString() + "', 650, 650, 'CadAcidente'), 'Acidente'))";
                //System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", sScript, true);
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "MyScript", sScript, true);
                System.Web.UI.ScriptManager scriptManager = System.Web.UI.ScriptManager.GetCurrent(Page);
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(
               this,
               this.GetType(),
               "CadAcidente",
               sScript,
               true);

            }
            else if (xColuna == 6)
            {
                string zTipo = "";

                zTipo = e.Item.Cells[6].Value.ToString();

                if (zTipo.Trim() == "") return;

                zTipo = zTipo.Substring(28);

                Response.Redirect("Relatorio_AON.aspx?IdEmpregado=" + Request["IdEmpregado"] + "&Tipo=" + zTipo);

                // "&nbsp&nbsp002...............CONSULTAPS"
            }

        }


    }
}
