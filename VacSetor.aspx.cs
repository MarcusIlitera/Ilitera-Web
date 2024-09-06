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
using Facade;
using Ilitera.Opsa.Data;
using System.Collections.Generic;
using Ilitera.Data;
using System.IO;
using System.Data.OleDb;
using System.Data.SqlClient;

using System.Text;
using Ilitera.Common;


namespace Ilitera.Net
{
    public partial class VacSetor : System.Web.UI.Page
    {

        

        protected void Page_Load(object sender, EventArgs e)
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


            //try
            //{




            //    if (!IsPostBack)
            //    {




            //        if (Session["Empresa"] != null && Session["Empresa"].ToString().Trim() != String.Empty)
            //        {
            //            int idJuridica = Convert.ToInt32(Session["Empresa"]);
            //            int idJuridicaPai = 0;

            //            if (Session["JuridicaPai"] != null && Session["JuridicaPai"].ToString().Trim() != String.Empty)
            //            {
            //                idJuridicaPai = Convert.ToInt32(Session["JuridicaPai"]);
            //            }

            //            //PopularGrid();

            //            if (Request.QueryString["liberar"] != null && Request.QueryString["liberar"].ToString() != String.Empty)
            //            {
            //                Session["Empregado"] = String.Empty;
            //                Session["NomeEmpregado"] = String.Empty;

            //                Response.Redirect("~/ListaEmpregados.aspx");
            //            }
            //        }
            //        else
            //        {
            //            ScriptManager.RegisterStartupScript(this, this.GetType(), "alerta", "alert('Para selecionar um empregado é necessário selecionar uma Empresa antes, você será redirecionado para a página de seleção de empresas.');", true);
            //            Response.Redirect("~/ListaEmpresas2.aspx");
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "alerta", String.Format("alert('{0}');", ex.Message), true);
            //}

            if (!IsPostBack)
            {


                //if (!string.IsNullOrEmpty(Session["Empresa"] as string)) lblEmpresa.Text = Session["Empresa"].ToString().Trim();
                //if (!string.IsNullOrEmpty(Session["Empregado"] as string)) lblEmpregado.Text = Session["Empregado"].ToString().Trim();
                //if (!string.IsNullOrEmpty(Session["NomeEmpregado"] as string)) lblNomeEmpregado.Text = Session["NomeEmpregado"].ToString().Trim();
                //if (!string.IsNullOrEmpty(Session["NomeEmpresa"] as string)) lblNomeEmpresa.Text = Session["NomeEmpresa"].ToString().Trim();
                

                lblUser.Text = Request["IdUsuario"].ToString().Trim();

                PopulaDDLVacinas();
            }

        }




        private void PopulaDDLVacinas()
        {



            ddlVacina.DataSource = new Vacina_Tipo().GetIdNome("VacinaTipo", " VacinaTipo is not null ");
            ddlVacina.DataValueField = "Id";
            ddlVacina.DataTextField = "Nome";
            ddlVacina.DataBind();

            ddlVacina.Items.Insert(0, new ListItem("Selecione uma Vacina...", "0"));
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



        protected void cmd_Salvar_Click1(object sender, EventArgs e)
        {

            Salvar_Relacao();
            return;
        }



        private void Salvar_Relacao()
        {
            

            if (ddlVacina.SelectedValue == null) return;


            //lblEmpresa.Text = Session["Empresa"].ToString().Trim();
            //lblJuridicaPai.Text= Session["JuridicaPai"].ToString().Trim();
            //lblEmpregado.Text= Session["Empregado"].ToString().Trim();
            //lblNomeEmpregado.Text = Session["NomeEmpregado"].ToString().Trim();
            //lblNomeEmpresa.Text = Session["NomeEmpresa"].ToString().Trim();


            Int32 zCodVacina = System.Convert.ToInt32(ddlVacina.SelectedValue.ToString());
            Int32 zCodEmpresa = Convert.ToInt32(Request.QueryString["IdEmpresa"]);


            //Ilitera.Data.Clientes_Funcionarios xDelete = new Ilitera.Data.Clientes_Funcionarios();
            //xDelete.Excluir_Vacina_Setor(zCodVacina, zCodEmpresa);

            SqlConnection cnn = new SqlConnection(Ilitera.Data.SQLServer.EntitySQLServer.GetConnection());
            cnn.Open();


            try
            {
                SqlCommand com = new SqlCommand();
                com.CommandText = "Delete from dbo.Vacina_Setor where IdVacinaTipo = " + zCodVacina.ToString() + " and IdSetor in ( select nId_Setor from " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblSetor where nId_Empr = " + zCodEmpresa.ToString() + " ) ";
                com.Connection = cnn;
                com.ExecuteNonQuery();
                //com.Transaction = null;
                //com.Dispose();
                //com = null;
            }
            catch( Exception Ex)
            {
                //if (string.IsNullOrEmpty(Session["Empresa"] as string)) Session["Empresa"] = lblEmpresa.Text;
                //if (string.IsNullOrEmpty(Session["JuridicaPai"] as string)) Session["JuridicaPai"] = lblJuridicaPai.Text;
                //if (string.IsNullOrEmpty(Session["Empregado"] as string)) Session["Empregado"] = lblEmpregado.Text;
                //if (string.IsNullOrEmpty(Session["NomeEmpregado"] as string)) Session["NomeEmpregado"] = lblNomeEmpregado.Text;
                //if (string.IsNullOrEmpty(Session["NomeEmpresa"] as string)) Session["NomeEmpresa"] = lblNomeEmpresa.Text;
            }
            finally
            {
                //if (string.IsNullOrEmpty(Session["Empresa"] as string)) Session["Empresa"] = lblEmpresa.Text;
                //if (string.IsNullOrEmpty(Session["JuridicaPai"] as string)) Session["JuridicaPai"] = lblJuridicaPai.Text;
                //if (string.IsNullOrEmpty(Session["Empregado"] as string)) Session["Empregado"] = lblEmpregado.Text;
                //if (string.IsNullOrEmpty(Session["NomeEmpregado"] as string)) Session["NomeEmpregado"] = lblNomeEmpregado.Text;
                //if (string.IsNullOrEmpty(Session["NomeEmpresa"] as string)) Session["NomeEmpresa"] = lblNomeEmpresa.Text;
            }



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
                        //if (!string.IsNullOrEmpty(chk_Setores.Items[zAux].Value as string)  )
                       // {

                            //Vacina_Setor xVS = new Vacina_Setor();
                            //xVS.IdVacinaTipo = zCodVacina;
                            //xVS.IdSetor = System.Convert.ToInt32(chk_Setores.Items[zAux].Value);
                            //xVS.Save();
                            //xVS.Dispose();
                            //xVS = null;

                            //int n;
                            //bool isnum = Int32.TryParse(chk_Setores.Items[zAux].Value.ToString(),out n);

                            //if (isnum)
                            //{

                                Random rnd = new Random();
                                int rId = rnd.Next(-21000000, 21000000);

                                SqlCommand com = new SqlCommand();
                                com.CommandText = "Insert into dbo.Vacina_Setor values ( " + rId.ToString() + " ," + zCodVacina.ToString() + " , " + chk_Setores.Items[zAux].Value.ToString() + " )";
                                com.Connection = cnn;
                                com.ExecuteNonQuery();
                                //com.Transaction = null;
                                com.Dispose();
                        //com = null;
                                System.Threading.Thread.Sleep(100);
                      
                            //}
                        //}
                    }
                    catch (Exception Ex)
                    {
                    }
                    finally
                    {
                    }
                }
            }



            //if (string.IsNullOrEmpty(Session["Empresa"] as string))  Session["Empresa"] = lblEmpresa.Text;
            //if (string.IsNullOrEmpty(Session["JuridicaPai"] as string)) Session["JuridicaPai"] = lblJuridicaPai.Text;
            //if (string.IsNullOrEmpty(Session["Empregado"] as string)) Session["Empregado"] = lblEmpregado.Text;
            //if (string.IsNullOrEmpty(Session["NomeEmpregado"] as string)) Session["NomeEmpregado"] = lblNomeEmpregado.Text;
            //if (string.IsNullOrEmpty(Session["NomeEmpresa"] as string)) Session["NomeEmpresa"] = lblNomeEmpresa.Text;

            //PopulaDDLVacinas();
            //chk_Setores.Visible = false;
            //cmd_Salvar.Visible = false;
            //cmd_Marcar_Todos.Visible = false;

            MsgBox1.Show("Ilitera.Net", "Relação Vacina x Setores salvo.", null,
                   new EO.Web.MsgBoxButton("OK"));

                       
            //Wagner  02/09/2021
            //Está perdendo as sessões ( Empresa, Empregado,etc )
            //não sei se é algo dessa página ou de algum componente
            //pois nas demais páginas do sistema não ocorre isso
            //assim, até salva a primeira alteração, mas se eu chamo PopulaDDLVacinas dá erro de Select
            //se tento recarregar página dá outro tipo de erro



            //Response.Redirect("~/VacSetor.aspx?IdEmpresa=" + Convert.ToInt32(Request.QueryString["IdEmpresa"]) + "&IdUsuario=0&Tipo=8", false);
            //Response.Redirect("~\\ListaEmpregados.aspx?&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + lblUser.Text.Trim());

            return;

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

        protected void chk_Setores_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}



 