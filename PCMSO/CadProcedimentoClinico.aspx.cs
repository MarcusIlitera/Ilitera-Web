using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Ilitera.Common;
using Ilitera.Data;
using Ilitera.Opsa.Data;
//using MestraNET;

namespace Ilitera.Net.PCMSO
{
    public partial class CadProcedimentoClinico : System.Web.UI.Page
    {


        protected Usuario usuario = new Usuario();
        private ProcedimentoClinico procedimentoClinico;

        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);

            if (lsbProcedimentoClinico.Items.Count > 0)
                lsbProcedimentoClinico.Items[0].Attributes.Add("style", "background-color: LightYellow");
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            //InicializaWebPageObjects();
            string xUsuario = Session["usuarioLogado"].ToString();

            if (!IsPostBack)
            {
                btnExcluir.Attributes.Add("onClick", "javascript:return confirm('Você deseja realmente excluir o Procedimento Clínico selecionado?');");
                PopulaLsbProcedimentos();
            }
        }

        private void PopulaLsbProcedimentos()
        {
            lsbProcedimentoClinico.DataSource = new ProcedimentoClinico().GetIdNome("NomeProcedimento", "IdCliente=" + Request["IdEmpresa"].ToString());
            lsbProcedimentoClinico.DataValueField = "Id";
            lsbProcedimentoClinico.DataTextField = "Nome";
            lsbProcedimentoClinico.DataBind();

            lsbProcedimentoClinico.Items.Insert(0, new ListItem("Cadastrar Novo Procedimento Clínico...", "0"));
            lsbProcedimentoClinico.Items[0].Selected = true;

            txtNome.Focus();
        }

        protected void btnGravar_Click(object sender, EventArgs e)
        {
            try
            {
                if (lsbProcedimentoClinico.SelectedValue.Equals("0"))
                {
                    procedimentoClinico = new ProcedimentoClinico();
                    procedimentoClinico.Inicialize();
                    Cliente xCliente = new Cliente();
                    xCliente.Find( System.Convert.ToInt32( Request["IdEmpresa"].ToString() ));
                    
                    procedimentoClinico.IdCliente = xCliente;
                    txtAuxAviso.Value = "O Procedimento Clínico foi cadastrado com sucesso!";
                }
                else
                {
                    procedimentoClinico = new ProcedimentoClinico(Convert.ToInt32(lsbProcedimentoClinico.SelectedValue));
                    txtAuxAviso.Value = "O Procedimento Clínico foi atualizado com sucesso!";
                }

                procedimentoClinico.NomeProcedimento = txtNome.Text.Trim();
                procedimentoClinico.DescricaoProcedimento = txtDescricao.Text.Trim();

                procedimentoClinico.UsuarioId = System.Convert.ToInt32( Request["IdUsuario"].ToString());
                procedimentoClinico.Save();

                PopulaLsbProcedimentos();

                lsbProcedimentoClinico.ClearSelection();
                lsbProcedimentoClinico.Items.FindByValue(procedimentoClinico.Id.ToString()).Selected = true;

                Session["txtAuxiliar"] = "atualizaProcedimentoClinico";
                Response.Write("<script>window.opener.document.forms[0].submit(); window.close();</script>");


                //btnExcluir.Enabled = true;
            }
            catch (Exception ex)
            {
                txtAuxAviso.Value = ex.Message;
            }
        }

        protected void btnExcluir_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet dsClinicoNaoOcupacional = new ClinicoNaoOcupacional().Get("IdProcedimentoClinico=" + lsbProcedimentoClinico.SelectedValue);
                if (dsClinicoNaoOcupacional.Tables[0].Rows.Count > 0)
                    //throw new Exception("Não é possível Excluir o Procedimento Clínico selecionado! Ele está associado a um ou mais Exames Ambulatoriais!");
                    MsgBox1.Show("Ilitera.Net", "Não é possível Excluir o Procedimento Clínico selecionado! Ele está associada a um ou mais Exames Ambulatoriais!", null,
                    new EO.Web.MsgBoxButton("OK"));
                
                procedimentoClinico = new ProcedimentoClinico(Convert.ToInt32(lsbProcedimentoClinico.SelectedValue));

                procedimentoClinico.UsuarioId = usuario.Id;
                procedimentoClinico.Delete();

                txtNome.Text = string.Empty;
                txtDescricao.Text = string.Empty;
                btnExcluir.Enabled = false;

                PopulaLsbProcedimentos();

                txtAuxAviso.Value = "O Procedimento Clínico selecionado foi Excluído com sucesso!";

                Session["txtAuxiliar"] = "atualizaProcedimentoClinico";
                Response.Write("<script>window.opener.document.forms[0].submit(); window.close();</script>");



            }
            catch (Exception ex)
            {
                txtAuxAviso.Value = ex.Message;
            }
        }

        protected void lsbProcedimentoClinico_SelectedIndexChanged(object sender, EventArgs e)
        {
            //btnExcluir.Enabled = !lsbProcedimentoClinico.SelectedValue.Equals("0");

            if (lsbProcedimentoClinico.SelectedValue.Equals("0"))
            {
                txtNome.Text = string.Empty;
                txtDescricao.Text = string.Empty;
            }
            else
            {
                procedimentoClinico = new ProcedimentoClinico(Convert.ToInt32(lsbProcedimentoClinico.SelectedValue));

                txtNome.Text = procedimentoClinico.NomeProcedimento;
                txtDescricao.Text = procedimentoClinico.DescricaoProcedimento;
            }
        }
}
}
