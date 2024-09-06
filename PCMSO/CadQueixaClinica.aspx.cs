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
    public partial class CadQueixaClinica : System.Web.UI.Page
    {

        private QueixaClinica queixaClinica;
        protected Usuario usuario = new Usuario();


        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);

            if (lsbQueixas.Items.Count > 0)
                lsbQueixas.Items[0].Attributes.Add("style", "background-color: LightYellow");
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            //InicializaWebPageObjects();
            string xUsuario = Session["usuarioLogado"].ToString();
            if (!IsPostBack)
            {
                btnExcluir.Attributes.Add("onClick", "javascript:return confirm('Você deseja realmente excluir a Queixa Clínica selecionada?');");
                PopulaLsbQueixas();
            }
        }

        private void PopulaLsbQueixas()
        {
            lsbQueixas.DataSource = new QueixaClinica().Find<QueixaClinica>("IdCliente=" + Request["IdEmpresa"].ToString() + " ORDER BY NomeQueixa");
            lsbQueixas.DataValueField = "Id";
            lsbQueixas.DataTextField = "NomeQueixa";
            lsbQueixas.DataBind();

            lsbQueixas.Items.Insert(0, new ListItem("Cadastrar Nova Queixa Clínica...", "0"));
            lsbQueixas.Items[0].Selected = true;

            txtNome.Focus();
        }

        protected void lsbQueixas_SelectedIndexChanged(object sender, EventArgs e)
        {
            //btnExcluir.Enabled = !lsbQueixas.SelectedValue.Equals("0");

            if (lsbQueixas.SelectedValue.Equals("0"))
            {
                txtNome.Text = string.Empty;
                txtDescricao.Text = string.Empty;
            }
            else
            {
                queixaClinica = new QueixaClinica(Convert.ToInt32(lsbQueixas.SelectedValue));

                txtNome.Text = queixaClinica.NomeQueixa;
                txtDescricao.Text = queixaClinica.DescricaoQueixa;
            }
        }

        protected void btnGravar_Click(object sender, EventArgs e)
        {
            try
            {
                if (lsbQueixas.SelectedValue.Equals("0"))
                {
                    queixaClinica = new QueixaClinica();
                    queixaClinica.Inicialize();

                    Cliente xCliente = new Cliente();
                    xCliente.Find(System.Convert.ToInt32(Request["IdEmpresa"].ToString()));

                    queixaClinica.IdCliente = xCliente;
                    txtAuxAviso.Value = "A Queixa Clínica foi cadastrada com sucesso!";
                }
                else
                {
                    queixaClinica = new QueixaClinica(Convert.ToInt32(lsbQueixas.SelectedValue));
                    txtAuxAviso.Value = "A Queixa Clínica foi atualizada com sucesso!";
                }

                queixaClinica.NomeQueixa = txtNome.Text.Trim();
                queixaClinica.DescricaoQueixa = txtDescricao.Text.Trim();

                queixaClinica.UsuarioId = System.Convert.ToInt32( Request["IdUsuario"].ToString() );
                queixaClinica.Save();

                PopulaLsbQueixas();

                lsbQueixas.ClearSelection();
                lsbQueixas.Items.FindByValue(queixaClinica.Id.ToString()).Selected = true;

                Session["txtAuxiliar"] = "atualizaQueixaClinica";
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
                DataSet dsClinicoNaoOcupacional = new ClinicoNaoOcupacional().Get("IdQueixaClinica=" + lsbQueixas.SelectedValue);
                if (dsClinicoNaoOcupacional.Tables[0].Rows.Count > 0)
                    //throw new Exception("Não é possível Excluir a Queixa Clínica selecionada! Ela está associada a um ou mais Exames Ambulatoriais!");
                    MsgBox1.Show("Ilitera.Net", "Não é possível Excluir a Queixa Clínica selecionada! Ela está associada a um ou mais Exames Ambulatoriais!", null,
                    new EO.Web.MsgBoxButton("OK"));

                
                queixaClinica = new QueixaClinica(Convert.ToInt32(lsbQueixas.SelectedValue));

                queixaClinica.UsuarioId = usuario.Id;
                queixaClinica.Delete();

                txtNome.Text = string.Empty;
                txtDescricao.Text = string.Empty;
                btnExcluir.Enabled = false;

                PopulaLsbQueixas();

                txtAuxAviso.Value = "A Queixa Clínica selecionada foi Excluída com sucesso!";

                Session["txtAuxiliar"] = "atualizaQueixaClinica";
                Response.Write("<script>window.opener.document.forms[0].submit(); window.close();</script>");

            }
            catch (Exception ex)
            {
                txtAuxAviso.Value = ex.Message;
            }
        }
    }
}
