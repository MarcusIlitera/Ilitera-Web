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

using System.Data.SqlClient;


namespace Ilitera.Net
{
    public partial class VacinaSetores : System.Web.UI.Page
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

            //string xUsuario = Session["usuarioLogado"].ToString();

            //InicializaWebPageObjects();

            if (!IsPostBack)
            {
                PopulaDDLVacinas();                
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

        //#region Web Form Designer generated code
        //override protected void OnInit(EventArgs e)
        //{
        //    ////
        //    //// CODEGEN: This call is required by the ASP.NET Web Form Designer.
        //    ////
        //    //InitializeComponent();
        //    //base.OnInit(e);
        //}

        ///// <summary>
        ///// Required method for Designer support - do not modify
        ///// the contents of this method with the code editor.
        ///// </summary>
        //private void InitializeComponent()
        //{

        //}
        //#endregion

        //protected void InicializaWebPageObjects()
        //{
            //base.InicializaWebPageObjects();
            //StringBuilder st = new StringBuilder();

            //st.Append("top.window.document.frames.conteudo.window.document.getElementById('txtIdEmpregado').value = '" + empregado.Id + "';");
            //st.Append("top.window.document.getElementById('txtIdEmpregado').value = '" + Request["IdEmpregado"] + "';");
            //st.Append("top.window.document.getElementById('txtIdUsuario').value = '" + Request["IdUsuario"] + "';");
            //st.Append("top.window.document.getElementById('txtIdEmpresa').value = '" + Request["IdEmpresa"] + "';");


            //this.ClientScript.RegisterStartupScript(this.GetType(), "IdEmpregado", st.ToString(), true);
            

        //}


        private void PopulaDDLVacinas()
        {

            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

            ddlVacina.DataSource = new Vacina_Tipo().GetIdNome("VacinaTipo", " VacinaTipo is not null ");
            ddlVacina.DataValueField = "Id";
            ddlVacina.DataTextField = "Nome";
            ddlVacina.DataBind();

            ddlVacina.Items.Insert(0, new ListItem("Selecione uma Vacina...", "0"));
        }



        protected void cmd_Voltar_Click(object sender, EventArgs e)
        {

            Entities.Usuario user = (Entities.Usuario)Session["usuarioLogado"];
            Response.Redirect("~\\ListaEmpregados.aspx?&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString());

        }

        private void PopulaSetores()
        {

            Ilitera.Data.PPRA_EPI xGHE = new Ilitera.Data.PPRA_EPI();

            //DataSet dS1 = xGHE.Carregar_Setores(System.Convert.ToInt32(Session["Empresa"].ToString()));
            DataSet dS1 = xGHE.Carregar_Setores(Convert.ToInt32(Request.QueryString["IdEmpresa"]));

            chk_Setores.DataSource = dS1;
            chk_Setores.DataValueField = "nID_SETOR";
            chk_Setores.DataTextField = "tNO_STR_EMPR";
            chk_Setores.DataBind();


            if (ddlVacina.SelectedValue == null) return;

            //carregar setores marcados para esta vacina            
            ArrayList alVacinaSetor = new Vacina_Setor().Find(" IdVacinaTipo = " + ddlVacina.SelectedValue + " ");



            foreach (Vacina_Setor zVS in alVacinaSetor)
            {

                for (int zAux = 0; zAux < chk_Setores.Items.Count; zAux++)
                {
                    if (System.Convert.ToInt32(chk_Setores.Items[zAux].Value) == zVS.IdSetor)
                    {
                        chk_Setores.Items[zAux].Selected = true;
                    }

                }

            }






        }



        protected void cmd_Salvar_Click(object sender, EventArgs e)
        {

            Salvar_Relacao();
            return;
        }



        private void Salvar_Relacao()
        {


            if (ddlVacina.SelectedValue == null) return;



            Int32 zCodVacina = System.Convert.ToInt32(ddlVacina.SelectedValue.ToString());

            Int32 zCodEmpresa = System.Convert.ToInt32(Convert.ToInt32(Request.QueryString["IdEmpresa"]));




            //Ilitera.Data.Clientes_Funcionarios xDelete = new Ilitera.Data.Clientes_Funcionarios();
            //xDelete.Excluir_Vacina_Setor(zCodVacina, zCodEmpresa);

            SqlConnection cnn = new SqlConnection(Ilitera.Data.SQLServer.EntitySQLServer.GetConnection());
            cnn.Open();
            SqlCommand com = new SqlCommand("Delete from dbo.Vacina_Setor where IdVacinaTipo = " + zCodVacina.ToString() + " and IdSetor in ( select nId_Setor from " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblSetor where nId_Empr = " + zCodEmpresa.ToString() + " ) ", cnn);

            com.ExecuteNonQuery();

            



            //ArrayList alVacinaSetor = new Vacina_Setor().Find(" IdVacinaTipo = " + zCodVacina.ToString() + " ");


            //foreach (Vacina_Setor zVS in alVacinaSetor)
            //{
            //    for (int zAux = 0; zAux < chk_Setores.Items.Count; zAux++)
            //    {
            //        try
            //        {
            //            if (chk_Setores.Items[zAux].Value != null)
            //            {
            //                if (System.Convert.ToInt32(chk_Setores.Items[zAux].Value) == zVS.IdSetor) zVS.Delete();
            //            }
            //        }
            //        catch (Exception Ex)
            //        {
            //        }
            //        finally
            //        {

            //        }
            //    }
            //}






            for (int zAux = 0; zAux < chk_Setores.Items.Count; zAux++)
            {
                if (chk_Setores.Items[zAux].Selected == true)
                {
                    try
                    {
                        if (chk_Setores.Items[zAux].Value != null && zCodVacina != 0 && chk_Setores.Items[zAux].Value.ToString().Trim() != "0")
                        {

                            Vacina_Setor xVS = new Vacina_Setor();
                            xVS.IdVacinaTipo = zCodVacina;
                            xVS.IdSetor = System.Convert.ToInt32(chk_Setores.Items[zAux].Value);
                            xVS.Save();
                        }
                    }
                    catch (Exception Ex)
                    {

                    }
                    finally
                    {

                    }
                }
            }


            PopulaDDLVacinas();
            chk_Setores.Visible = false;
            cmd_Salvar.Visible = false;
            cmd_Marcar_Todos.Visible = false;

            MsgBox2.Show("Ilitera.Net", "Relação Vacina x Setores salvo.", null,
                   new EO.Web.MsgBoxButton("OK"));






            return;
            //Entities.Usuario user = (Entities.Usuario)Session["usuarioLogado"];
            //Response.Redirect("~\\ListaEmpregados.aspx?&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString());



        }



        protected void MsgBox2_ButtonClick(object sender, System.Web.UI.WebControls.CommandEventArgs e)
        {
          
        }


        protected void ddlVacina_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (ddlVacina.SelectedIndex <= 0)
            {
                chk_Setores.Visible = false;
                cmd_Salvar.Visible = false;
                cmd_Marcar_Todos.Visible = false;
            }
            else
            {
                chk_Setores.Visible = true;
                cmd_Salvar.Visible = true;
                cmd_Marcar_Todos.Visible = true;
                PopulaSetores();
            }
        }



        protected void cmd_Todos_Click(object sender, EventArgs e)
        {

            for (int zAux = 0; zAux < chk_Setores.Items.Count; zAux++)
            {
                chk_Setores.Items[zAux].Selected = true;
            }

        }




    }
}
