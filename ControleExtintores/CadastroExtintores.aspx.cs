using System;
using System.Data;
using System.Web.SessionState;
using System.Web.UI.WebControls;
using Ilitera.Common;
using Ilitera.Opsa.Data;
using System.Text;
using System.Web.UI;


namespace Ilitera.Net.ControleExtintores
{
    public partial class CadastroExtintores : System.Web.UI.Page
    {

        private Extintores extintor;
		//string tipoCadastro;

        protected Prestador prestador = new Prestador();
        protected Usuario usuario = new Usuario();
        protected Cliente cliente = new Cliente();
        protected Empregado empregado = new Empregado();

		
		protected void Page_Load(object sender, System.EventArgs e)
		{
            InicializaWebPageObjects();
            string xUsuario = Session["usuarioLogado"].ToString();

            if (!IsPostBack)
			{
				PopulaDDLFabricante();
				PopulaDDLTipoExtintor();
				PopulaDDLSetor();
				btnFechar.Attributes.Add("onClick", "javascript:self.close();");
				btnExcluir.Attributes.Add("onClick", "javascript:return confirm('Você deseja realmente excluir este Equipamento de combate à Incêndio?');");
				if (extintor.Id != 0)
					PopulaTelaExtintor();
			}
			else
			{
				if (txtAuxiliar.Value == "atualizaFabricante")
				{
					PopulaDDLFabricante();
					txtAuxiliar.Value = string.Empty;
				}
				else if (txtAuxiliar.Value == "atualizaTipo")
				{
					PopulaDDLTipoExtintor();
					txtAuxiliar.Value = string.Empty;
				}
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
			this.ID = "CadastroExtintores";

		}
		#endregion

        protected  void InicializaWebPageObjects()
        {
            //base.InicializaWebPageObjects();

			if (Request["IdExtintores"] == null || Request["IdExtintores"].ToString() == string.Empty)
			{
                Cliente xCliente = new Cliente();
                xCliente.Find(System.Convert.ToInt32(Session["Empresa"].ToString()));

				extintor = new Extintores();
				extintor.Inicialize();
				extintor.IdCliente = xCliente;

				lblTitulo.Text = "Cadastro de Equipamento de combate à Incêndio";
				btnExcluir.Visible = false;
				//tipoCadastro = "cadastrado";
			}
			else
			{
				extintor = new Extintores(Convert.ToInt32(Request["IdExtintores"]));
				lblTitulo.Text = "Edição do Equipamento de Combate à Incêndio";
				//tipoCadastro = "editado";
			}
		}

		private void PopulaTelaExtintor()
		{

            System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");

			txtAtivoFixo.Text = extintor.AtivoFixo;
            wdeDataFabricacao.Text = extintor.DataFabricacao.ToString("dd/MM/yyyy", ptBr);
            txtVctoRecarga.Text = extintor.VencimentoRecarga.ToString("dd/MM/yyyy", ptBr);
            wneGarantia.Text = extintor.Garantia.ToString();
            wnePesoVazio.Text = extintor.PesoVazio.ToString();
			wnePesoCheio.Text= extintor.PesoCheio.ToString();
			ddlFabricante.Items.FindByValue(extintor.IdFabricanteExtintor.Id.ToString()).Selected = true;
			ddlTipoExtintor.Items.FindByValue(extintor.IdTipoExtintor.Id.ToString()).Selected = true;
			ddlSetor.Items.FindByValue(extintor.IdSetor.Id.ToString()).Selected = true;
			txtLocalizacao.Text = extintor.Localizacao;
			txtObservacao.Text = extintor.Observacao;

            if (extintor.IndCondicao == 0)
            {
                rblCondicao.SelectedIndex = 0;
            }
            else
            {
                rblCondicao.ClearSelection();
                rblCondicao.Items.FindByValue(extintor.IndCondicao.ToString()).Selected = true;
            }
		}

		private void PopulaDDLFabricante()
		{
			ddlFabricante.DataSource = new FabricanteExtintor().Get("IdCliente=" + Request["IdEmpresa"]
				+" ORDER BY NomeCompleto");
			ddlFabricante.DataValueField = "IdFabricanteExtintor";
			ddlFabricante.DataTextField = "NomeAbreviado";
			ddlFabricante.DataBind();

			ddlFabricante.Items.Insert(0, new ListItem("Selecione...", "0"));
			ddlFabricante.Items.Add(new ListItem("Novo Fabricante...", "Novo"));
		}

		private void PopulaDDLSetor()
		{
			ddlSetor.DataSource = new Setor().Get("nID_EMPR=" + Request["IdEmpresa"] + " ORDER BY tNO_STR_EMPR");
			ddlSetor.DataValueField = "nID_SETOR";
			ddlSetor.DataTextField = "tNO_STR_EMPR";
			ddlSetor.DataBind();

			ddlSetor.Items.Insert(0, new ListItem("Selecione...", "0"));
		}
		
		private void PopulaDDLTipoExtintor()
		{
			ddlTipoExtintor.DataSource = new TipoExtintor().Get("IdCliente=" + Request["IdEmpresa"] + " ORDER BY ModeloExtintor");
			ddlTipoExtintor.DataValueField = "IdTipoExtintor";
			ddlTipoExtintor.DataTextField = "ModeloExtintor";
			ddlTipoExtintor.DataBind();

			ddlTipoExtintor.Items.Insert(0, new ListItem("Selecione...", "0"));
			ddlTipoExtintor.Items.Add(new ListItem("Cadastro de um Novo Tipo...", "Novo"));
		}

		protected void btnGravar_Click(object sender, System.EventArgs e)
		{
			try
			{
				PopulaExtintor();

                
                Entities.Usuario xUser = (Entities.Usuario)Session["usuarioLogado"];

				extintor.UsuarioId = xUser.IdUsuario;
				extintor.Save();

				StringBuilder st = new StringBuilder();

                //st.Append("window.opener.document.getElementById('txtAuxiliar').value='atualiza';");
                
                //st.Append("window.opener.document.forms[0].submit();");
                //st.Append("window.opener.document.forms[0].submit(); window.alert(O Equipamento de combate à Incêndio foi " + tipoCadastro + " com sucesso!');window.close();");
                //st.Append("window.opener.document.forms[0].submit();");
                              
				//st.Append("window.alert('O Equipamento de combate à Incêndio foi " + tipoCadastro + " com sucesso!');");
                
				//st.Append("self.close();");
                //this.ClientScript.RegisterStartupScript(this.GetType(), "EdicaoCadastroExtintor", st.ToString(), true);
                
                Response.Write("<script>window.opener.document.forms[0].submit(); window.close();</script>"); 
			}
			catch (Exception ex)
			{
				
                MsgBox1.Show("Ilitera.Net", ex.Message, null,
                                new EO.Web.MsgBoxButton("OK"));                

			}
		}

		private void PopulaExtintor()
		{
			extintor.AtivoFixo = txtAtivoFixo.Text.Trim();
			extintor.IdTipoExtintor.Id = Convert.ToInt32(ddlTipoExtintor.SelectedValue);
			extintor.IdSetor.Id = Convert.ToInt32(ddlSetor.SelectedValue);
			extintor.Localizacao = txtLocalizacao.Text.Trim();

            System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");

            if (wdeDataFabricacao.Text.Trim() == "" || wdeDataFabricacao.Text.Trim() == "01/01/0001")
            {
                throw new Exception("A Data de Fabricação é Obrigatória!");
                //MsgBox1.Show("Ilitera.Net", "A Data de Fabricação é Obrigatória!", null,
                //new EO.Web.MsgBoxButton("OK"));
                //return;
            }
			extintor.DataFabricacao = System.Convert.ToDateTime( wdeDataFabricacao.Text, ptBr  );

            if (txtVctoRecarga.Text.Trim() == "" || txtVctoRecarga.Text.Trim() == "01/01/0001" )
            {
                throw new Exception("A Data de vencimento da recarga é Obrigatório!");
                //MsgBox1.Show("Ilitera.Net", "A Data de Vencimento da Recarga é Obrigatória!", null,
                //new EO.Web.MsgBoxButton("OK"));
                //return;
            }
            extintor.VencimentoRecarga = System.Convert.ToDateTime(txtVctoRecarga.Text, ptBr);

            extintor.IdFabricanteExtintor.Id = Convert.ToInt32(ddlFabricante.SelectedValue);
			extintor.Garantia =  System.Convert.ToInt32(wneGarantia.Text);
			extintor.IndCondicao = Convert.ToInt32(rblCondicao.SelectedValue);
			extintor.PesoVazio = System.Convert.ToSingle( wnePesoVazio.Text );
			extintor.PesoCheio = System.Convert.ToSingle(wnePesoCheio.Text);
			extintor.Observacao = txtObservacao.Text.Trim();
		}

		protected void btnExcluir_Click(object sender, System.EventArgs e)
		{
			try
			{
				extintor.UsuarioId = usuario.Id;
				extintor.Delete();

				StringBuilder st = new StringBuilder();

				//st.Append("window.opener.document.getElementById('txtAuxiliar').value='atualiza';");
				//st.Append("window.opener.document.forms[0].submit();");
                st.Append("window.opener.document.forms[0].submit(); window.alert('O Extintor de Incêndio foi Excluído com sucesso!');window.close();");
				//st.Append("self.close();");

                this.ClientScript.RegisterStartupScript(this.GetType(), "ExclusaoExtintor", st.ToString(), true);
			}
			catch (Exception ex)
			{
				
                MsgBox1.Show("Ilitera.Net", ex.Message, null,
                                new EO.Web.MsgBoxButton("OK"));                

			}
		}

		protected void ddlFabricante_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (ddlFabricante.SelectedValue == "Novo")
			{
				ddlFabricante.ClearSelection();
				
                StringBuilder st = new StringBuilder();

                st.AppendFormat("void(window.open('CadFabricanteExtintor.aspx?IdEmpresa=" + Request["IdEmpresa"] + "&IdUsuario=" + Request["IdUsuario"] + @"', 'CadastroExtintores','scrollbars=yes, resizable=yes, toolbar=no, addressbar=no, menubar=no, width=600px, height=500px'));");
                
                ScriptManager.RegisterStartupScript(this, this.GetType(), " ", st.ToString(), true);

				//st.Append("addItemPop(centerWin('CadFabricanteExtintor.aspx?IdEmpresa=" + Request["IdEmpresa"] + "&IdUsuario=" + Request["IdUsuario"] + "',300,280,\'CadastroFabricanteExtintores\'),\'Todos\');");

                //this.ClientScript.RegisterStartupScript(this.GetType(), "CadastroFabricanteExtintores", st.ToString(), true);
			}
		}

		protected void ddlTipoExtintor_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (ddlTipoExtintor.SelectedValue == "Novo")
			{
				ddlTipoExtintor.ClearSelection();
				StringBuilder st = new StringBuilder();

                st.AppendFormat("void(window.open('CadTipoExtintor.aspx?IdEmpresa=" + Request["IdEmpresa"] + "&IdUsuario=" + Request["IdUsuario"] + @"', 'CadastroExtintores','scrollbars=yes, resizable=yes, toolbar=no, addressbar=no, menubar=no, width=600px, height=500px'));");

                ScriptManager.RegisterStartupScript(this, this.GetType(), " ", st.ToString(), true);


				//st.Append("addItemPop(centerWin('CadTipoExtintor.aspx?IdEmpresa=" + Request["IdEmpresa"] + "&IdUsuario=" + Request["IdUsuario"] + "',300,300,\'CadastroTipoExtintores\'),\'Todos\');");

                //this.ClientScript.RegisterStartupScript(this.GetType(), "CadastroTipoExtintores", st.ToString(), true);
			}
		}
	}
}
