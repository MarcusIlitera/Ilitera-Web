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
using Entities;
using Facade;
using System.Collections.Generic;

namespace Ilitera.Net
{
    public partial class visualizarDadosEmpresa : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
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

            try
            {
                if (Session["Empresa"] != null && Session["Empresa"].ToString() != String.Empty)
                {
                    EmpresaDTO empresaDto = EmpresaFacade.RetornarDadosEmpresa(Convert.ToInt32(Session["Empresa"]));
                    List<ContatoTelefonico> lstContatoTelefonico = ContatoTelefonicoFacade.RetornarTelefones(Convert.ToInt32(Session["Empresa"]));
                    GrupoEmpresa grupoEmpresa = GrupoEmpresaFacade.RetornarGrupoEmpresa(empresaDto.IdGrupoEmpresa);
                    Endereco endereco = EnderecoFacade.RetornarEndereco(empresaDto.IdPessoa);

                    

                    txtNomeEmpresa.Text = empresaDto.NomeAbreviado;
                    txtRazaoSocial.Text = empresaDto.NomeCompleto;
                    txtCodigo.Text = empresaDto.NomeCodigo;
                    txtDataCadastro.Text = empresaDto.DataCadastro.ToShortDateString();

                    if (grupoEmpresa != null && grupoEmpresa.Descricao != String.Empty)
                    {
                        txtGrupo.Text = grupoEmpresa.Descricao;
                    }

                    //txtTipoLogradouro
                    txtLogradouro.Text = endereco.Logradouro;
                    txtNumeroEndereco.Text = endereco.Numero;
                    txtComplemento.Text = endereco.Complemento;
                    txtBairro.Text = endereco.Bairro;
                    txtCep.Text = endereco.CEP;
                    txtCidade.Text = endereco.Municipio;
                    txtEstado.Text = endereco.UF;
                    txtCnpj.Text = empresaDto.CNPJ;
                    txtIe.Text = empresaDto.IE;
                    txtCCM.Text = empresaDto.CCM;
                    txtNumeroEmpregados.Text = empresaDto.QtdEmpregados.ToString();
                    txtAtividade.Text = empresaDto.Atividade;

                    //txtCnae

                    txtSite.Text = empresaDto.Site;
                    txtEmail.Text = empresaDto.Email;

                    //txtMotorista

                    txtDiretor.Text = empresaDto.Diretor;

                    gridContatoTelefonico.DataSource = lstContatoTelefonico;
                    gridContatoTelefonico.DataBind();


                    if (!IsPostBack)
                    {
                        int xPreposto;
                        int xPreposto2;

                        
                        Ilitera.Data.PPRA_EPI xPrep = new Ilitera.Data.PPRA_EPI();


                        //ver se tem preposto selecionado
                        xPreposto = xPrep.Retorna_Preposto(Convert.ToInt32(Session["Empresa"]));
                        xPreposto2 = xPrep.Retorna_Preposto2(Convert.ToInt32(Session["Empresa"]));
                        
                        //carregar combo preposto
                        cmb_Preposto.DataSource = xPrep.Lista_Prestadores();
                        cmb_Preposto.DataValueField = "IdPessoa";
                        cmb_Preposto.DataTextField = "Nome_Codigo";
                        cmb_Preposto.DataBind();

                        cmb_Preposto.SelectedIndex = -1;

                        cmb_Preposto2.DataSource = xPrep.Lista_Prestadores();
                        cmb_Preposto2.DataValueField = "IdPessoa";
                        cmb_Preposto2.DataTextField = "Nome_Codigo";
                        cmb_Preposto2.DataBind();

                        cmb_Preposto2.SelectedIndex = -1;


                        //caso exista preposto selecionado, posicionar o listbox,  caso contrário, colocar no primeiro item - Não Definido
                        for (int xCont = 0; xCont < cmb_Preposto.Items.Count; xCont++)
                        {

                            if ( System.Convert.ToInt32( cmb_Preposto.Items[xCont].Value.ToString()) == xPreposto )
                            {
                                cmb_Preposto.SelectedIndex = xCont;
                            }

                        }

                        for (int xCont = 0; xCont < cmb_Preposto2.Items.Count; xCont++)
                        {

                            if (System.Convert.ToInt32(cmb_Preposto2.Items[xCont].Value.ToString()) == xPreposto2)
                            {
                                cmb_Preposto2.SelectedIndex = xCont;
                            }

                        }


                    }

                    
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alerta", "alert('Para selecionar um empregado é necessário selecionar uma Empresa antes, você será redirecionado para a página de seleção de Emprsas.');", true);
                    Response.Redirect("~/ListaEmpresas2.aspx");
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alerta", String.Format("alert('{0}');", ex.Message), true);
            }            
        }



        protected void cmd_Confirmar_Click(object sender, EventArgs e)
        {

            //se estiver em não definido, salvar null ( enviar 0 )

            //senao, salvar idpessoa em Cliente->IdRespPPP

            Ilitera.Data.PPRA_EPI xPrep = new Ilitera.Data.PPRA_EPI();

            if ( cmb_Preposto.SelectedIndex < 0 )
                xPrep.Atualizar_Preposto(0, Convert.ToInt32(Session["Empresa"]));
            else
               xPrep.Atualizar_Preposto(Convert.ToInt32(cmb_Preposto.Items[cmb_Preposto.SelectedIndex].Value.ToString()), Convert.ToInt32(Session["Empresa"]));

            if (cmb_Preposto2.SelectedIndex < 0)
                xPrep.Atualizar_Preposto2(0, Convert.ToInt32(Session["Empresa"]));
            else
                xPrep.Atualizar_Preposto2(Convert.ToInt32(cmb_Preposto2.Items[cmb_Preposto2.SelectedIndex].Value.ToString()), Convert.ToInt32(Session["Empresa"]));

            string xPreposto = EmpresaFacade.RetornarDadosPreposto(Convert.ToInt32(Session["Empresa"]));
            Session["Preposto"] = xPreposto;

            string xPreposto2 = EmpresaFacade.RetornarDadosPreposto2(Convert.ToInt32(Session["Empresa"]));
            Session["Preposto2"] = xPreposto2;


            //voltar a tela principal
            Response.Redirect("~/ListaEmpregados.aspx");

        }

        protected void cmd_Salvar_Click(object sender, EventArgs e)
        {

            //verificar se nome e NIT estão preenchidos
            if (txt_Nome.Text.Trim() == "" || txt_NIT.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alerta", String.Format("alert('{0}')", "Preposto deve ter o nome e o CPF preenchidos."), true);
            }
            else
            {
                //verificar se já existe nome e NIT iguais
                Ilitera.Data.PPRA_EPI xPrep = new Ilitera.Data.PPRA_EPI();

                string xRet = xPrep.Checar_Duplicidade_Preposto(txt_Nome.Text.Trim(), txt_NIT.Text.Trim());

                if (xRet != "0")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alerta", String.Format("alert('{0}')", "Preposto já existe no cadastro."), true);
                }
                else
                {
                    //se OK,  salvar, e já colocar como preposto, recarregar página
                    xPrep.Salvar_Preposto(Convert.ToInt32(Session["Empresa"]), txt_Nome.Text.Trim(), txt_NIT.Text.Trim());
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alerta", String.Format("alert('{0}')", "Preposto salvo e atualizado."), true);

                    //voltar a tela principal
                    Response.Redirect("~/ListaEmpregados.aspx");

                }


            }
            
            
            


        }
    }
}
