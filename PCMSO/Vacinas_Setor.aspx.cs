using System;
using System.Data;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using Ilitera.Common;
using Ilitera.Opsa.Data;
using System.Globalization;
using System.Collections;
//using MestraNET;

namespace Ilitera.Net
{
    public partial class Vacinas_Setor : System.Web.UI.Page
    {

        //private Ilitera.Opsa.Data.Vacina zVacina = new Ilitera.Opsa.Data.Vacina();
        //private string tipo;

        protected Prestador prestador = new Prestador();
        protected Usuario usuario = new Usuario();
        protected Cliente cliente = new Cliente();

        //protected override void OnLoadComplete(EventArgs e)
        //{
        //    base.OnLoadComplete(e);


        //}

        protected void Page_Load(object sender, System.EventArgs e)
        {
            InicializaWebPageObjects();
            //string xUsuario = Session["usuarioLogado"].ToString();

            if (!IsPostBack)
            {                

                //Session["txtAuxiliar"] = string.Empty;
                //RegisterClientCode();
                                

                PopulaDDLVacinas();

                PopulaTelaExame();

            }
           // else
            //{
                //string IdDDL = string.Empty;

                ////if (txtAuxiliar.Value.Equals("atualizaQueixaClinica"))
                //if (Session["txtAuxiliar"].ToString().Trim()=="atualizaQueixaClinica")
                //{
                //    IdDDL = ddlQueixas.SelectedValue;
                //    PopulaDDLQueixas();

                //    if (ddlQueixas.Items.FindByValue(IdDDL) != null)
                //        ddlQueixas.Items.FindByValue(IdDDL).Selected = true;
                //}
                //else if (Session["txtAuxiliar"].ToString().Trim()=="atualizaProcedimentoClinico")
                //{
                //    IdDDL = ddlProcedimento.SelectedValue;
                //    PopulaDDLProcedimentos();

                //    if (ddlProcedimento.Items.FindByValue(IdDDL) != null)
                //        ddlProcedimento.Items.FindByValue(IdDDL).Selected = true;
                //}

                //txtAuxiliar.Value = string.Empty;
                //Session["txtAuxiliar"] = string.Empty;
           // }
        }

        protected void InicializaWebPageObjects()
        {
             //base.InicializaWebPageObjects();
            //StringBuilder st = new StringBuilder();

            ////st.Append("top.window.document.frames.conteudo.window.document.getElementById('txtIdEmpregado').value = '" + empregado.Id + "';");
            //st.Append("top.window.document.getElementById('txtIdEmpregado').value = '" + Request["IdEmpregado"] + "';");
            //st.Append("top.window.document.getElementById('txtIdUsuario').value = '" + Request["IdUsuario"] + "';");
            //st.Append("top.window.document.getElementById('txtIdEmpresa').value = '" + Request["IdEmpresa"] + "';");

            //this.ClientScript.RegisterStartupScript(this.GetType(), "IdEmpregado", st.ToString(), true);
            ////btnFichaCompleta.Attributes.Add("onClick", "addItem(centerWin('../DadosEmpresa/FichaCompleta.aspx?IdEmpregado=" + Request["IdEmpregado"] + "&IdEmpresa=" + Request["IdEmpresa"] + "&IdUsuario=" + Request["IdUsuario"] + "',560,320,\'FichaCompleta\'),\'Todos\'); Reload();");

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


        private void RegisterClientCode()
        {
            //btnAddQueixas.Disabled = false;
            //btnAddProcedimento.Disabled = false;
            //btnAddQueixas.Attributes.Add("onClick", "javascript:AbreCadastro('CadQueixaClinica', " + Request["IdEmpresa"].ToString() + ", " + Request["IdUsuario"].ToString() + ");");
            //btnAddProcedimento.Attributes.Add("onClick", "javascript:AbreCadastro('CadProcedimentoClinico', " + Request["IdEmpresa"].ToString() + ", " + Request["IdUsuario"].ToString()  + ");");
            //btnExcluir.Attributes.Add("onClick", "javascript:return(confirm('Deseja realmente excluir esta Vacina?'))");
        }

        private void PopulaDDLVacinas()
        {
            ddlVacina.DataSource = new Vacina_Tipo().GetIdNome("VacinaTipo", " VacinaTipo is not null ");
            ddlVacina.DataValueField = "Id";
            ddlVacina.DataTextField = "Nome";
            ddlVacina.DataBind();

            ddlVacina.Items.Insert(0, new ListItem("Selecione uma Vacina...", "0"));
        }




        public void PopulaTelaExame()
        {




        }

        protected void btnOK_Click(object sender, System.EventArgs e)
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


            ArrayList alVacinaSetor = new Vacina_Setor().Find(" IdVacinaTipo = " + zCodVacina.ToString() + " ");


            foreach (Vacina_Setor zVS in alVacinaSetor)
            {
                for (int zAux = 0; zAux < chk_Setores.Items.Count; zAux++)
                {
                    try
                    {
                        if (chk_Setores.Items[zAux].Value != null)
                        {
                            if (System.Convert.ToInt32(chk_Setores.Items[zAux].Value) == zVS.IdSetor) zVS.Delete();
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
            btnOK.Visible = false;
            cmd_Marcar_Todos.Visible = false;

            MsgBox1.Show("Ilitera.Net", "Relação Vacina x Setores salvo.", null,
                   new EO.Web.MsgBoxButton("OK"));

            return;
            //Entities.Usuario user = (Entities.Usuario)Session["usuarioLogado"];
            //Response.Redirect("~\\ListaEmpregados.aspx?&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString());



        }


        protected void cmd_Voltar_Click(object sender, EventArgs e)
        {

            Entities.Usuario user = (Entities.Usuario)Session["usuarioLogado"];
            Response.Redirect("~\\ListaEmpregados.aspx?&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString());


        }

        protected void ddlVacina_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (ddlVacina.SelectedIndex <= 0)
            {
                chk_Setores.Visible = false;
                btnOK.Visible = false;
                cmd_Marcar_Todos.Visible =false;
            }
            else
            {
                chk_Setores.Visible = true;
                btnOK.Visible = true;
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
