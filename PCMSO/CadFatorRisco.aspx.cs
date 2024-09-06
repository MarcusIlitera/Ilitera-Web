using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using Ilitera.Data;
using Ilitera.Common;
using Ilitera.Opsa.Data;

namespace Ilitera.Net.PCMSO
{
    public partial class CadFatorRisco : System.Web.UI.Page
    {

        protected override void OnPreInit(EventArgs e)
        {
            //InicializaWebPageObjects();
            base.OnPreInit(e);
        }

        protected override void OnLoadComplete(EventArgs e)
        {
            lsbFatorRisco.Items[0].Attributes.Add("style", "background-color: LightYellow");
            base.OnLoadComplete(e);
        }
        
        protected void Page_Load(object sender, EventArgs e)
        {
            string xUsuario = Session["usuarioLogado"].ToString();
            if (!IsPostBack)
            {
                btnExcluir.Attributes.Add("onClick", "javascript:return confirm('Você tem certeza que gostaria de excluir o Fator de Risco selecionado?');");
                PopulaLsbFatorRisco();
            }
        }

        private void PopulaLsbFatorRisco()
        {
            lsbFatorRisco.DataSource = new FatorRisco().GetIdNome("Nome", "IdCliente=" + Session["Empresa"].ToString());
            lsbFatorRisco.DataTextField = "Nome";
            lsbFatorRisco.DataValueField = "Id";
            lsbFatorRisco.DataBind();

            lsbFatorRisco.Items.Insert(0, new ListItem("Novo Fator de Risco...", "0"));
            lsbFatorRisco.Items[0].Selected = true;
        }

        protected void btnGravar_Click(object sender, EventArgs e)
        {
            try
            {
                FatorRisco fatorRisco = new FatorRisco();
                string tipoCadastro = string.Empty;

                if (lsbFatorRisco.SelectedValue.Equals("0"))
                {
                    Cliente xCliente;
                    xCliente = new Cliente();
                    xCliente.Inicialize();
                    xCliente.Find(System.Convert.ToInt32(Request["IdEmpresa"]));

                    fatorRisco.Inicialize();
                    fatorRisco.IdCliente = xCliente;
                    tipoCadastro = "cadastrado";
                }
                else
                {
                    fatorRisco.Find(Convert.ToInt32(lsbFatorRisco.SelectedValue));
                    tipoCadastro = "atualizado";
                }

                fatorRisco.Nome = txtNome.Text.Trim();
                fatorRisco.Descricao = txtDescricao.Text.Trim();
                fatorRisco.UsuarioId =  System.Convert.ToInt32( Request["IdUsuario"]);
                fatorRisco.Save();

                PopulaLsbFatorRisco();
                lsbFatorRisco.ClearSelection();
                lsbFatorRisco.Items.FindByValue(fatorRisco.Id.ToString()).Selected = true;

                btnExcluir.Enabled = true;
                //txtAuxAviso.Value = "O Fator de Risco foi " + tipoCadastro + " com sucesso!";

                StringBuilder strBuilder = new StringBuilder();


                //strBuilder.Append("window.opener.document.getElementById('txtAuxiliar').value='atualizaFatorRisco';");
                strBuilder.Append("window.opener.document.forms[0].submit();");
                strBuilder.Append("window.alert('O Fator de risco foi salvo com sucesso!');");

                this.ClientScript.RegisterStartupScript(this.GetType(), "FatorRisco", strBuilder.ToString(), true);

            }
            catch (Exception ex)
            {
                txtAuxAviso.Value = ex.Message;
            }
        }

        protected void lsbProcedimentoClinico_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnExcluir.Enabled = !lsbFatorRisco.SelectedValue.Equals("0");

            if (lsbFatorRisco.SelectedValue.Equals("0"))
            {
                txtNome.Text = string.Empty;
                txtDescricao.Text = string.Empty;
            }
            else
            {
                FatorRisco fatorRisco = new FatorRisco(Convert.ToInt32(lsbFatorRisco.SelectedValue));

                txtNome.Text = fatorRisco.Nome;
                txtDescricao.Text = fatorRisco.Descricao;
            }
        }

        protected void btnExcluir_Click(object sender, EventArgs e)
        {
            try
            {
                int numFatorRisco = new FatorRiscoClinico().ExecuteCount("IdFatorRisco=" + lsbFatorRisco.SelectedValue);
                if (numFatorRisco > 0)
                    throw new Exception("Não é possível excluir o Fator de Risco selecionado! Ele já está associado a um ou mais exames clínicos!");
                
                FatorRisco fatorRisco = new FatorRisco(Convert.ToInt32(lsbFatorRisco.SelectedValue));
                fatorRisco.UsuarioId = System.Convert.ToInt32(  Request["IdUsuario"].ToString()); //usuario.Id;

                fatorRisco.Delete();

                PopulaLsbFatorRisco();
                txtNome.Text = string.Empty;
                txtDescricao.Text = string.Empty;
                btnExcluir.Enabled = false;

                //txtAuxAviso.Value = "O Fator de Risco selecionado foi excluído com sucesso!";

                StringBuilder strBuilder = new StringBuilder();

                //strBuilder.Append("window.opener.document.getElementById('txtAuxiliar').value='atualizaFatorRisco';");
                strBuilder.Append("window.opener.document.forms[0].submit();");
                strBuilder.Append("window.alert('O Fator de risco foi excluído com sucesso!');");

                this.ClientScript.RegisterStartupScript(this.GetType(), "FatorRisco", strBuilder.ToString(), true);

            }
            catch (Exception ex)
            {
                txtAuxAviso.Value = ex.Message;
            }
        }
    }
}
