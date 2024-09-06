using System;
using System.Collections;
using System.Data;
using System.Web.SessionState;
using System.Web.UI.WebControls;
using Ilitera.Common;
using Ilitera.Opsa.Data;
using System.Text;


namespace Ilitera.Net
{
	/// <summary>
	/// Summary description for ListaEmpresa.
	/// </summary>
	public partial class CadResponsavel : System.Web.UI.Page
	{
		
		protected void Page_Load(object sender, System.EventArgs e)
		{
            //InicializaWebPageObjects();
            string xUsuario = Session["usuarioLogado"].ToString();

            if (!IsPostBack)
			{
				PopulalsbColaboradores();
				btnFechar.Attributes.Add("onClick", "javascript:self.close();");
				btnExcluir.Attributes.Add("onClick", "javascript:return confirm('Você deseja realmente excluir este Colaborador?');");
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
			this.ID = "CadResponsavel";

		}
		#endregion

		private void PopulalsbColaboradores()
		{
            Cliente cliente;
            cliente = new Cliente(System.Convert.ToInt32(Session["Empresa"].ToString()));

			ArrayList prestadores = new Prestador().GetListaPrestador(cliente, false, (int)TipoPrestador.ContatoEmpresa);
			prestadores.Sort();

			lsbColaboradores.DataSource = prestadores;
			lsbColaboradores.DataValueField = "Id";
			lsbColaboradores.DataTextField = "NomeCompleto";
			lsbColaboradores.DataBind();

			lsbColaboradores.Items.Insert(0, new ListItem("Novo Colaborador...", "Novo"));
			lsbColaboradores.Items.FindByValue("Novo").Selected = true;

			SetFocus();
		}

		private void SetFocus()
		{
			StringBuilder st = new StringBuilder();

			st.Append("document.forms[0].txtNomeCompleto.focus();");

            this.ClientScript.RegisterStartupScript(this.GetType(), "Focus", st.ToString(), true);
		}

		protected void btnGravar_Click(object sender, System.EventArgs e)
		{
			try
			{
				//if (!txtEmail.Text.Trim().Equals(string.Empty))
				//	if (!EmailBase.IsEmail(txtEmail.Text.Trim()))
				//		throw new Exception("O E-mail fornecido não é um e-mail válido! Por favor, digite novamente!");
				
				Prestador colaborador = new Prestador();
				string tipoCadastro = string.Empty;

                Cliente cliente;
                cliente = new Cliente(System.Convert.ToInt32(Session["Empresa"].ToString()));


				if (lsbColaboradores.SelectedValue == "Novo")
				{

					colaborador.Inicialize();
					colaborador.IdJuridica.Id = cliente.Id;
					colaborador.IndTipoPrestador = (int)TipoPrestador.ContatoEmpresa;
					colaborador.IdPessoa.IndTipoPessoa = (short)Pessoa.TipoPessoa.Fisica;
					tipoCadastro = "cadastrado";
				}
				else
				{
					colaborador = new Prestador(Convert.ToInt32(lsbColaboradores.SelectedValue));
					tipoCadastro = "editado";
				}

                colaborador.Titulo = txtTitulo.Text.Trim();
				colaborador.Numero = txtNumeroRegistro.Text.Trim();
				colaborador.IdPessoa.NomeCodigo = txtNIT.Text.Trim();
				colaborador.IdPessoa.NomeCompleto = txtNomeCompleto.Text.Trim();
				colaborador.IdPessoa.Email = txtEmail.Text;
				
				if (txtNomeCompleto.Text.Trim().IndexOf(" ") == -1)
					colaborador.IdPessoa.NomeAbreviado = txtNomeCompleto.Text.Trim();
				else
					colaborador.IdPessoa.NomeAbreviado = txtNomeCompleto.Text.Trim().Substring(0, txtNomeCompleto.Text.Trim().IndexOf(" "));


                Usuario usuario = new Usuario();
                usuario.Find(System.Convert.ToInt32(Request["IdUsuario"].ToString()));


				colaborador.UsuarioId = usuario.Id;
				colaborador.UsuarioProcessoRealizado = "Cadastro de Colaborador para a Empresa " + cliente.NomeAbreviado;
				colaborador.Save();

				StringBuilder st = new StringBuilder();

				st.Append("window.opener.document.getElementById('txtAuxiliar').value='atualizaColaborador';");
				st.Append("window.opener.document.forms[0].submit();");
				st.Append("window.alert('O Colaborador foi " + tipoCadastro + " com sucesso!');");

                this.ClientScript.RegisterStartupScript(this.GetType(), "EdicaoCadastroColaborador", st.ToString(), true);

				PopulalsbColaboradores();
				lsbColaboradores.ClearSelection();
				lsbColaboradores.Items.FindByValue(colaborador.Id.ToString()).Selected = true;

				btnExcluir.Enabled = true;
			}
			catch (Exception ex)
			{
                MsgBox1.Show("Ilitera.Net", ex.Message, null,
                new EO.Web.MsgBoxButton("OK"));

			}
		}

		protected void btnExcluir_Click(object sender, System.EventArgs e)
		{
			try
			{
				Prestador colaborador = new Prestador(Convert.ToInt32(lsbColaboradores.SelectedValue));

				DataSet dsVerificaLogin = new Usuario().Get("IdPessoa=" + colaborador.IdPessoa.Id);
				DataSet dsVerificaUsoAuditoria = new Irregularidade().Get("IdRespAdm=" + colaborador.Id + " OR IdRespOpe=" + colaborador.Id);
				DataSet dsVerificaUsoExtintor = new HistoricoExtintor().Get("IdUsuarioResp=" + colaborador.Id);
				DataSet dsVerificaUsoTreinamento = new Treinamento().Get("IdResponsavel=" + colaborador.Id);
				DataSet dsVerificaUsoOS = new AtualizacaoProcedimento().Get("IdResponsavel=" + colaborador.Id + " OR IdOperador=" + colaborador.Id);
                DataSet dsVerificaUsoAPP = new Procedimento().Get("IdElaboradorAPP=" + colaborador.Id + " OR IdRevisorAPP=" + colaborador.Id);

				if (dsVerificaLogin.Tables[0].Rows.Count > 0)
					throw new Exception("Este Colaborador possui um Login no Sistema Mestra.NET cadastrado! Não é possível excluí-lo!");
				if (dsVerificaUsoAuditoria.Tables[0].Rows.Count > 0)
					throw new Exception("Este Colaborador está como Responsável de alguma Irregularidade de Auditoria! Não é possível excluí-lo!");
				if (dsVerificaUsoExtintor.Tables[0].Rows.Count > 0)
					throw new Exception("Este Colaborador está como Responsável de algum Histórico de Extintor! Não é possível excluí-lo!");
				if (dsVerificaUsoTreinamento.Tables[0].Rows.Count > 0)
					throw new Exception("Este Colaborador está como Instrutor de algum Treinamento! Não é possível excluí-lo!");
				if (dsVerificaUsoOS.Tables[0].Rows.Count > 0)
					throw new Exception("Este Colaborador está como Responsável ou Operador de alguma Atualização de Procedimento na Ordem de Serviço! Não é possível excluí-lo!");
                if (dsVerificaUsoAPP.Tables[0].Rows.Count > 0)
                    throw new Exception("Este Colaborador está como Elaborador ou Revisor de alguma APP na Ordem de Serviço! Não é possível excluí-lo!");

                Usuario usuario = new Usuario();
                usuario.Find(System.Convert.ToInt32(Request["IdUsuario"].ToString()));


				colaborador.UsuarioId = usuario.Id;
				colaborador.UsuarioProcessoRealizado = "Exclusão do Colaborador " + colaborador.NomeCompleto;
				colaborador.Delete();

				StringBuilder st = new StringBuilder();

				st.Append("window.opener.document.getElementById('txtAuxiliar').value='atualizaColaborador';");
				st.Append("window.opener.document.forms[0].submit();");
				st.Append("window.alert('O Colaborador foi Excluído com sucesso!');");

                this.ClientScript.RegisterStartupScript(this.GetType(), "ExclusaoColaborador", st.ToString(), true);

				PopulalsbColaboradores();

				txtNomeCompleto.Text = string.Empty;
				txtTitulo.Text = string.Empty;
				txtNumeroRegistro.Text = string.Empty;
				txtNIT.Text = string.Empty;
				txtEmail.Text = string.Empty;

				btnExcluir.Enabled = false;
			}
			catch (Exception ex)
			{
                MsgBox1.Show("Ilitera.Net", ex.Message, null,
                new EO.Web.MsgBoxButton("OK"));

			}
		}

		protected void lsbColaborador_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (lsbColaboradores.SelectedValue.Equals("Novo"))
			{
				txtNomeCompleto.Text = string.Empty;
				txtTitulo.Text = string.Empty;
				txtNumeroRegistro.Text = string.Empty;
				txtNIT.Text = string.Empty;
				txtEmail.Text = string.Empty;
				btnExcluir.Enabled = false;
			}
			else
			{
				Prestador colaborador = new Prestador(Convert.ToInt32(lsbColaboradores.SelectedValue));
				colaborador.IdPessoa.Find();

				txtNomeCompleto.Text = colaborador.NomeCompleto;
				txtTitulo.Text = colaborador.Titulo;
				txtNumeroRegistro.Text = colaborador.Numero;
				txtNIT.Text = colaborador.IdPessoa.NomeCodigo;
				txtEmail.Text = colaborador.IdPessoa.Email;
				btnExcluir.Enabled = true;
			}

			SetFocus();
		}
	}
}
