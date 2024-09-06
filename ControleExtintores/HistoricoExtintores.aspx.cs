using System;
using System.Data;
using System.Web.SessionState;
using System.Web.UI.WebControls;
using Ilitera.Common;
using Ilitera.Opsa.Data;
using System.Text;
//using MestraNET;
using System.Collections;
using System.Drawing;

namespace Ilitera.Net.ControleExtintores
{
    public partial class HistoricoExtintores : System.Web.UI.Page
    {

        protected System.Web.UI.WebControls.Label lblEmpresa;
		private Extintores extintores;
		private HistoricoExtintor historico;

        protected Prestador prestador = new Prestador();
        protected Usuario usuario = new Usuario();
        protected Cliente cliente = new Cliente();
        protected Empregado empregado = new Empregado();

        protected Color backColorDisabledBox = Color.FromName("#EBEBEB");
        protected Color backColorEnabledBox = Color.FromName("#FCFEFD");
        protected Color foreColorDisabledLabel = Color.Gray;
        protected Color foreColorEnabledLabel = Color.FromName("#44926D");
        protected Color foreColorEnabledTextBox = Color.FromName("#004000");
        protected Color foreColorDisabledTextBox = Color.Gray;
        protected Color borderColorDisabledBox = Color.LightGray;
        protected Color borderColorEnabledBox = Color.FromName("#7CC5A1");
        protected Color backColorEnabledBoxYellow = Color.LightYellow;


		
		protected void Page_Load(object sender, System.EventArgs e)
		{
            InicializaWebPageObjects();
            string xUsuario = Session["usuarioLogado"].ToString();

            if (!IsPostBack)
			{
				lblTitulo.Text = "Histórico do Extintor de Ativo Fixo '" + extintores.AtivoFixo + "'";
				PopulalsbHistorico();
				PopulaDDLResponsavel();
				btnFechar.Attributes.Add("onClick", "javascript:self.close();");
				btnExcluir.Attributes.Add("onClick", "javascript:return confirm('Você deseja realmente Excluir este Histórico do Extintor de Incêndio?');");
				Color backcolor = Color.FromName("#EBEBEB");
				lblReparacao.ForeColor = foreColorDisabledLabel;
				ddlReparacao.Items.Insert(0, new ListItem(string.Empty, "0"));
				ddlReparacao.BackColor = backcolor;
				ddlReparacao.Enabled = false;

				lblVencimento.ForeColor = foreColorDisabledLabel;
				wdtVencimento.BackColor = backColorDisabledBox;
				wdtVencimento.BorderColor = borderColorDisabledBox;
				wdtVencimento.Enabled = false;
			}
			else
			{
				if (txtAuxiliar.Value == "atualizaColaborador")
				{
					PopulaDDLResponsavel();
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
			this.ID = "HistoricoExtintores";

		}
		#endregion


        protected  void InicializaWebPageObjects()
        {
            //base.InicializaWebPageObjects();
			
			extintores = new Extintores(Convert.ToInt32(Request["IdExtintores"]));
		}

		private void PopulalsbHistorico()
		{
			DataSet ds = new DataSet();
			DataRow row;
			DataTable table = new DataTable("Default");
			table.Columns.Add("IdHistorico", Type.GetType("System.String"));
			table.Columns.Add("TextoHistorico", Type.GetType("System.String"));
			ds.Tables.Add(table);

			ArrayList alHistorico = new HistoricoExtintor().Find("IdExtintores=" + Request["IdExtintores"] + " ORDER BY DataEvento DESC");

			foreach (HistoricoExtintor historico in alHistorico)
			{
				row = ds.Tables[0].NewRow();
				row["IdHistorico"] = historico.Id.ToString();
				row["TextoHistorico"] = historico.ToString();
				ds.Tables[0].Rows.Add(row);
			}

			lsbHistoricoExtintor.DataSource = ds;
			lsbHistoricoExtintor.DataValueField = "IdHIstorico";
			lsbHistoricoExtintor.DataTextField = "TextoHistorico";
			lsbHistoricoExtintor.DataBind();

			lsbHistoricoExtintor.Items.Insert(0, new ListItem("Novo Cadastro de Histórico do Extintor " + extintores.AtivoFixo, "Novo"));
			lsbHistoricoExtintor.Items.FindByValue("Novo").Selected = true;

			wdeData.Text = string.Empty;
			ddlTipoHistorico.ClearSelection();
			ddlReparacao.ClearSelection();
			Color backcolor = Color.FromName("#EBEBEB");
			lblReparacao.ForeColor = foreColorDisabledLabel;
			if (ddlReparacao.Items[0].Value != "0")
				ddlReparacao.Items.Insert(0, new ListItem(string.Empty, "0"));
			ddlReparacao.BackColor = backcolor;
			ddlReparacao.Enabled = false;
			wnePesoAtual.Text = string.Empty;
			ddlResponsavel.ClearSelection();
			lblVencimento.ForeColor = foreColorDisabledLabel;
			wdtVencimento.Text = "";
			wdtVencimento.BackColor = backColorDisabledBox;
			wdtVencimento.BorderColor = borderColorDisabledBox;
			wdtVencimento.Enabled = false;
			txtDescricao.Text = string.Empty;
			btnExcluir.Visible = false;

			SetFocus();
		}

		private void SetFocus()
		{
			StringBuilder st = new StringBuilder();

			st.Append("document.forms[0].igtxtwdeData.focus();");

            this.ClientScript.RegisterStartupScript(this.GetType(), "Focus", st.ToString(), true);
		}

		private void PopulaDDLResponsavel()
		{
			ArrayList prestadores = new Prestador().GetListaPrestador(cliente, false, (int)TipoPrestador.ContatoEmpresa);
			prestadores.Sort();

			ddlResponsavel.DataSource = prestadores;
			ddlResponsavel.DataValueField = "Id";
			ddlResponsavel.DataTextField = "NomeCompleto";
			ddlResponsavel.DataBind();

			ddlResponsavel.Items.Insert(0, new ListItem("Selecione o Responsável...", "0"));
			ddlResponsavel.Items.Add(new ListItem("Cadastro Novo Responsável...", "Novo"));
		}

		protected void ddlResponsavel_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (ddlResponsavel.SelectedValue == "Novo")
			{
				ddlResponsavel.ClearSelection();
				StringBuilder st = new StringBuilder();

				st.Append("addItemPop(centerWin('../CommonPages/CadResponsavel.aspx?IdEmpresa=" + Request["IdEmpresa"] + "&IdUsuario=" + Request["IdUsuario"] + "',350,300,\'CadastroResponsavel\'));");

                this.ClientScript.RegisterStartupScript(this.GetType(), "CadastroResponsavel", st.ToString(), true);
			}
		}

		protected void btnGravar_Click(object sender, System.EventArgs e)
		{
			try
			{
				string tipoCadastro = string.Empty;

				if (lsbHistoricoExtintor.SelectedValue == "Novo")
				{
					historico = new HistoricoExtintor();
					historico.Inicialize();
					historico.IdExtintores = extintores;
					tipoCadastro = "cadastrado";
				}
				else
				{
					historico = new HistoricoExtintor(Convert.ToInt32(lsbHistoricoExtintor.SelectedValue));
					tipoCadastro = "editado";
				}

				PopulaHistoricoExtintor();

				historico.UsuarioId = usuario.Id;
				historico.Save();


                MsgBox1.Show("Ilitera.Net", "O Histórico do Extintor foi " + tipoCadastro + " com sucesso!", null,
                                new EO.Web.MsgBoxButton("OK"));                

                
				PopulalsbHistorico();
			}
			catch (Exception ex)
			{
                MsgBox1.Show("Ilitera.Net", ex.Message, null,
                                new EO.Web.MsgBoxButton("OK"));                
                

			}
		}

		private void PopulaHistoricoExtintor()
		{
			historico.IndTipoHistorico = Convert.ToInt32(ddlTipoHistorico.SelectedValue);
			historico.IndReparacao = Convert.ToInt32(ddlReparacao.SelectedValue);
			historico.DataEvento = System.Convert.ToDateTime( wdeData.Text );
			historico.PesoAtual =  System.Convert.ToSingle( wnePesoAtual.Text );
			historico.Descricao = txtDescricao.Text.Trim();
			historico.IdUsuarioResp.Id = Convert.ToInt32(ddlResponsavel.SelectedValue);
			historico.Vencimento = System.Convert.ToDateTime( wdtVencimento.Text );
		}

		protected void lsbHistoricoExtintor_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (lsbHistoricoExtintor.SelectedValue == "Novo")
			{
				wdeData.Text = string.Empty;
				ddlTipoHistorico.ClearSelection();
				ddlReparacao.ClearSelection();
				Color backcolor = Color.FromName("#EBEBEB");
				lblReparacao.ForeColor = foreColorDisabledLabel;
				if (ddlReparacao.Items[0].Value != "0")
					ddlReparacao.Items.Insert(0, new ListItem(string.Empty, "0"));
				ddlReparacao.BackColor = backcolor;
				ddlReparacao.Enabled = false;
				wnePesoAtual.Text = string.Empty;
				ddlResponsavel.ClearSelection();
				txtDescricao.Text = string.Empty;
				lblVencimento.ForeColor = foreColorDisabledLabel;
				wdtVencimento.Text = "";
				wdtVencimento.BackColor = backColorDisabledBox;
				wdtVencimento.BorderColor = borderColorDisabledBox;
				wdtVencimento.Enabled = false;
				
				btnExcluir.Visible = false;
			}
			else
			{
				HistoricoExtintor historico = new HistoricoExtintor(Convert.ToInt32(lsbHistoricoExtintor.SelectedValue));

				wdeData.Text = historico.DataEvento.ToString("dd/MM/yyyy");
				ddlTipoHistorico.ClearSelection();
				ddlTipoHistorico.Items.FindByValue(historico.IndTipoHistorico.ToString()).Selected = true;
				ddlReparacao.ClearSelection();

				if (historico.IndTipoHistorico == (int)IndTipoHistoricoExtintor.Inspecao ||
					historico.IndTipoHistorico == (int)IndTipoHistoricoExtintor.Reparacao)
				{
					Color forecolor = Color.FromName("#44926D");
					Color backcolor = Color.FromName("#FCFEFD");
					lblReparacao.ForeColor = forecolor;
					ddlReparacao.Enabled = true;
					ddlReparacao.BackColor = backcolor;
					if (ddlReparacao.Items[0].Value == "0")
						ddlReparacao.Items.RemoveAt(0);
					ddlReparacao.Items.FindByValue(historico.IndReparacao.ToString()).Selected = true;
					lblVencimento.ForeColor = foreColorEnabledLabel;
					wdtVencimento.Enabled = true;
					wdtVencimento.BorderColor = borderColorEnabledBox;
					wdtVencimento.BackColor = backColorEnabledBox;
					wdtVencimento.Text = historico.Vencimento.ToString("dd/MM/yyyy");
				}
				else
				{
					Color backcolor = Color.FromName("#EBEBEB");
					lblReparacao.ForeColor = foreColorDisabledLabel;
					if (ddlReparacao.Items[0].Value != "0")
						ddlReparacao.Items.Insert(0, new ListItem(string.Empty, "0"));
					ddlReparacao.BackColor = backcolor;
					ddlReparacao.Enabled = false;

					lblVencimento.ForeColor = foreColorDisabledLabel;
					wdtVencimento.Text = "";
					wdtVencimento.BackColor = backColorDisabledBox;
					wdtVencimento.BorderColor = borderColorDisabledBox;
					wdtVencimento.Enabled = false;
				}

				wnePesoAtual.Text= historico.PesoAtual.ToString();
				ddlResponsavel.ClearSelection();
				ddlResponsavel.Items.FindByValue(historico.IdUsuarioResp.Id.ToString()).Selected = true;
				txtDescricao.Text = historico.Descricao;

				btnExcluir.Visible = true;
			}
			
			SetFocus();
		}

		protected void ddlTipoHistorico_SelectedIndexChanged(object sender, System.EventArgs e)
		{			
			if (ddlTipoHistorico.SelectedValue == ((int)IndTipoHistoricoExtintor.Inspecao).ToString() ||
				ddlTipoHistorico.SelectedValue == ((int)IndTipoHistoricoExtintor.Reparacao).ToString())
			{
				Color forecolor = Color.FromName("#44926D");
				Color backcolor = Color.FromName("#FCFEFD");
				lblReparacao.ForeColor = forecolor;
				ddlReparacao.Enabled = true;
				ddlReparacao.BackColor = backcolor;
                if (ddlReparacao.Items[0].Value == "0")
					ddlReparacao.Items.RemoveAt(0);

				lblVencimento.ForeColor = foreColorEnabledLabel;
				wdtVencimento.Enabled = true;
				wdtVencimento.BackColor = backColorEnabledBox;
				wdtVencimento.BorderColor = borderColorEnabledBox;
			}
			else
			{
				Color backcolor = Color.FromName("#EBEBEB");
				lblReparacao.ForeColor = foreColorDisabledLabel;
				if (ddlReparacao.Items[0].Value != "0")
                    ddlReparacao.Items.Insert(0, new ListItem(string.Empty, "0"));
				ddlReparacao.ClearSelection();
				ddlReparacao.BackColor = backcolor;
				ddlReparacao.Enabled = false;

				lblVencimento.ForeColor = foreColorDisabledLabel;
				wdtVencimento.Text = "";
				wdtVencimento.BackColor = backColorDisabledBox;
				wdtVencimento.BorderColor = borderColorDisabledBox;
				wdtVencimento.Enabled = false;
			}
		}

		protected void btnExcluir_Click(object sender, System.EventArgs e)
		{
			try
			{
				historico = new HistoricoExtintor(Convert.ToInt32(lsbHistoricoExtintor.SelectedValue));

				historico.UsuarioId = usuario.Id;
				historico.Delete();


                MsgBox1.Show("Ilitera.Net", "O Histórico do Extintor de Incêndio foi Excluído com sucesso!", null,
                                new EO.Web.MsgBoxButton("OK"));                



				PopulalsbHistorico();
			}
			catch (Exception ex)
			{
                MsgBox1.Show("Ilitera.Net", ex.Message, null,
                                new EO.Web.MsgBoxButton("OK"));                

			}
		}
	}
}
