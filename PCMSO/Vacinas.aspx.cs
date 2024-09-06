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
    public partial class Vacinas : System.Web.UI.Page
    {
        
        private Ilitera.Opsa.Data.Vacina zVacina = new Ilitera.Opsa.Data.Vacina();
		//private string tipo;

        protected Prestador prestador = new Prestador();
        protected Usuario usuario = new Usuario();
        protected Cliente cliente = new Cliente();
        protected Empregado empregado = new Empregado();

        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);

           
        }

        protected void Page_Load(object sender, System.EventArgs e)
		{
			InicializaWebPageObjects();
            string xUsuario = Session["usuarioLogado"].ToString();

            if (!IsPostBack)
            {

                string zAcao = Request["Acao"].ToString().Trim();

                if (zAcao == "E")
                {
                    btnOK.Visible = true;
                    btnExcluir.Visible = true;
                }
                else
                {
                    btnOK.Visible = false;
                    btnExcluir.Visible = false;
                }
                                
                Session["txtAuxiliar"] = string.Empty; 
                RegisterClientCode();
                PopulaDDLVacinas();
                PopulaDDLDose(0);

                if (!zVacina.Id.Equals(0))
                    PopulaTelaExame();

            }
            else
            {
                string IdDDL = string.Empty;

                ////if (txtAuxiliar.Value.Equals("atualizaQueixaClinica"))
                if (Session["txtAuxiliar"].ToString().Trim()=="atualizaVacina")
                {
                    PopulaDDLVacinas();
                    PopulaDDLDose(0);
                }
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

                txtAuxiliar.Value = string.Empty;
                Session["txtAuxiliar"] = string.Empty; 
            }
		}

		protected void InicializaWebPageObjects()
		{
			//base.InicializaWebPageObjects();

            if (Request["IdVacina"] != null && Request["IdVacina"] != "")
            {

                zVacina.Find(Convert.ToInt32(Request["IdVacina"]));
                //tipo = "atualizado";
            }
            else
            {
                zVacina.Id = 0;
				//tipo = "cadastrado";
				btnExcluir.Enabled = false;
			}
		}

		private void RegisterClientCode()
		{
            //btnAddQueixas.Disabled = false;
            //btnAddProcedimento.Disabled = false;
            //btnAddQueixas.Attributes.Add("onClick", "javascript:AbreCadastro('CadQueixaClinica', " + Request["IdEmpresa"].ToString() + ", " + Request["IdUsuario"].ToString() + ");");
            //btnAddProcedimento.Attributes.Add("onClick", "javascript:AbreCadastro('CadProcedimentoClinico', " + Request["IdEmpresa"].ToString() + ", " + Request["IdUsuario"].ToString()  + ");");
            btnExcluir.Attributes.Add("onClick", "javascript:return(confirm('Deseja realmente excluir esta Vacina?'))");
		}

        private void PopulaDDLVacinas()
        {
            ddlVacina.DataSource = new Vacina_Tipo().GetIdNome("VacinaTipo", "VacinaTipo is not null" );
            ddlVacina.DataValueField = "Id";
            ddlVacina.DataTextField = "Nome";
            ddlVacina.DataBind();

            ddlVacina.Items.Insert(0, new ListItem("Selecione uma Vacina...", "0"));
        }

        private void PopulaDDLDose( int zVacinaTipo )
        {
            ddlDose.DataSource = new Vacina_Dose().GetIdNome("Dose", "Dose is not null and IdVacinaTipo = " + zVacinaTipo + " ");
            ddlDose.DataValueField = "Id";
            ddlDose.DataTextField = "Nome";
            ddlDose.DataBind();

            ddlDose.Items.Insert(0, new ListItem("Selecione uma Dose...", "0"));
        }

		private void PopulaExame()
		{
            System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");           
            zVacina.DataVacina = System.Convert.ToDateTime( wdtDataVacina.Text.Trim(), ptBr );
            zVacina.Observacao = txtDescricao.Text.Trim();

            zVacina.IdEmpregado = System.Convert.ToInt32(Session["Empregado"].ToString());

            if (ddlVacina.SelectedIndex <= 0)
            {
                MsgBox1.Show("Ilitera.Net", "Selecione Vacina.", null,
                    new EO.Web.MsgBoxButton("OK"));
                return;
            }


            if (ddlDose.SelectedIndex <= 0)
            {
                MsgBox1.Show("Ilitera.Net", "Selecione Dose da Vacina.", null,
                    new EO.Web.MsgBoxButton("OK"));
                return;
            }

            //if (!cliente.ContrataPCMSO.Equals((int)TipoPcmsoContratada.NaoContratada))
            //{


            zVacina.IdVacinaDose = Convert.ToInt32(ddlDose.Items[ddlDose.SelectedIndex].Value.ToString());
                //exame.IdProcedimentoClinico.Id = Convert.ToInt32(ddlDose.SelectedValue);

            //}            
		}

		public void PopulaTelaExame()
		{

          
            
            wdtDataVacina.Text = zVacina.DataVacina.ToString("dd/MM/yyyy");
            txtDescricao.Text = zVacina.Observacao;



            Vacina_Dose zDose = new Vacina_Dose();
            zDose.Find(" IdVacinaDose = " +  zVacina.IdVacinaDose.ToString() + " ");
            

            ddlVacina.Items.FindByValue(zDose.IdVacinaTipo.ToString()).Selected = true;

            PopulaDDLDose(System.Convert.ToInt32(ddlVacina.Items[ddlVacina.SelectedIndex].Value.ToString()));
            ddlDose.Items.FindByValue(zVacina.IdVacinaDose.ToString()).Selected = true;

            //}			
		}
		
		protected void btnOK_Click(object sender, System.EventArgs e)
		{
			try
			{                
                PopulaExame();

                //exame.IdEmpregado = System.Convert.ToInt32(Request["IdEmpregado"].ToString());
                
                zVacina.Save();
				
				ViewState["IdExameNaoOcupacional"] = zVacina.Id;
				btnExcluir.Enabled = true;
                				
				StringBuilder st = new StringBuilder();

				//st.Append("window.opener.document.getElementById('txtAuxiliar').value='atualiza';");
				//st.Append("window.opener.document.forms[0].submit();");
				//st.Append("window.alert('O Exame Ambulatorial foi " + tipo + " com sucesso!');");

                txtAuxAviso.Value = "A Vacina foi salvo com sucesso!";
                txtExecutePost.Value = "True";

                //if (cliente.ContrataPCMSO.Equals((int)TipoPcmsoContratada.NaoContratada))
                txtCloseWindow.Value = "True";

                Entities.Usuario user = (Entities.Usuario)Session["usuarioLogado"];

                Response.Redirect("DadosEmpregado_Vacina.aspx?IdEmpregado=" + Session["Empregado"].ToString() + "&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString() + "&Tipo=8");



                //this.ClientScript.RegisterStartupScript(this.GetType(), "AtualizaNaoOcupacional", st.ToString(), true);
			}
			catch(Exception ex)
			{
                txtAuxAviso.Value = ex.Message;
                MsgBox1.Show("Ilitera.Net", ex.Message, null,
                       new EO.Web.MsgBoxButton("OK"));

			}
		}

		protected void btnExcluir_Click(object sender, System.EventArgs e)
		{
			try
			{
                zVacina.Delete();

                ViewState["IdExameNaoOcupacional"] = 0;  // null;

                btnExcluir.Enabled = true;
                StringBuilder st = new StringBuilder();

                txtAuxAviso.Value = "A Vacina foi deletada com sucesso";
                txtExecutePost.Value = "True";
                txtCloseWindow.Value = "True";

                Response.Write("<script>window.opener.document.forms[0].submit(); window.close();</script>");

                Entities.Usuario user = (Entities.Usuario)Session["usuarioLogado"];


                Response.Redirect("DadosEmpregado_Vacina.aspx?IdEmpregado=" + Session["Empregado"].ToString() + "&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString() + "&Tipo=8");


            }
			catch(Exception ex)
			{
                txtAuxAviso.Value = ex.Message;
                MsgBox1.Show("Ilitera.Net", ex.Message, null,
                       new EO.Web.MsgBoxButton("OK"));

			}
		}

        protected void cmd_Voltar_Click(object sender, EventArgs e)
        {

            Entities.Usuario user = (Entities.Usuario)Session["usuarioLogado"];
            Response.Redirect("DadosEmpregado_Vacina.aspx?IdEmpregado=" + Session["Empregado"].ToString() + "&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString() + "&Tipo=8");


        }

        protected void ddlVacina_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlVacina.SelectedIndex <= 0) PopulaDDLDose(0);
            else PopulaDDLDose( System.Convert.ToInt32( ddlVacina.Items[ ddlVacina.SelectedIndex].Value.ToString() ));
        }

        protected void cmd_Nova_Vacina_Click(object sender, EventArgs e)
        {
            StringBuilder st = new StringBuilder();

            Entities.Usuario usuario = (Entities.Usuario)Session["usuarioLogado"];

            st.AppendFormat("void(window.open('ListaVacinas.aspx?IdUsuario=" + usuario.IdUsuario.ToString() + @"', 'ListaVacinas','scrollbars=yes, resizable=yes, toolbar=no, addressbar=no, menubar=no, width=450px, height=420px'));");

            ScriptManager.RegisterStartupScript(this, this.GetType(), " ", st.ToString(), true);

        }
    }
}
