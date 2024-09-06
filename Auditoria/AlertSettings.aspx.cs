using System;
using System.Data;
using System.Web.SessionState;
using System.Web.UI.WebControls;
using Ilitera.Common;
using Ilitera.Opsa.Data;
using System.Text;

using System.Collections;
using System.Drawing;

namespace Ilitera.Net
{
	/// <summary>
	/// Summary description for ListaEmpresa.
	/// </summary>
    public partial class AlertSettings : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label lblEmpresa;
		
		protected void Page_Load(object sender, System.EventArgs e)
		{
            //InicializaWebPageObjects((int)IndTipoPagina.BigContents, "ListagemIrregularidades.aspx");
            string xUsuario = Session["usuarioLogado"].ToString();

            if (!IsPostBack)
			{
				PopulaDDLAuditoria();
				if (ddlAuditoria.Enabled)
				{
					PopulaAlertService();
					
					if (ckbService.Checked)
                        PopulaLbxColaboradores();
					else
						DisableServiceWebControls();
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
			this.ID = "AlertSettings";

		}
		#endregion

		private void PopulaAlertService()
		{
			AvisoAuditoria avisoAuditoria = new AvisoAuditoria();
			avisoAuditoria.Find("IdAuditoria=" + ddlAuditoria.SelectedValue);

			if (avisoAuditoria.Id != 0)
			{
				ckbService.Checked = avisoAuditoria.IsAtivo;
				wneDias.Text = avisoAuditoria.DiasInicio.ToString();
				rblPeriodicidade.Items.FindByValue(avisoAuditoria.Periodicidade.ToString()).Selected = true;
				PopulaLbxEmailSelecionados(avisoAuditoria);
			}
		}
		
		private void PopulaLbxEmailSelecionados(AvisoAuditoria avisoAuditoria)
		{
			ArrayList alEmail = new ArrayList();

			char[] seps = {',', ';', '\n', '\r'};

			string[] emails = avisoAuditoria.Emails.Split(seps);

			foreach (string email in emails)
				alEmail.Add(email);

			alEmail.Sort();

			lbxEmails.DataSource = alEmail;
			lbxEmails.DataBind();
		}

		private void PopulaDDLAuditoria()
		{
            Cliente cliente;
            cliente = new Cliente(System.Convert.ToInt32(Session["Empresa"].ToString()));

            DataSet ds = new Ilitera.Opsa.Data.Auditoria().Get("IdCliente=" + cliente.Id
				+" AND IdPedido IN (SELECT IdPedido FROM Pedido WHERE IdPedidoGrupo IN (SELECT IdPedidoGrupo FROM PedidoGrupo WHERE DataConclusao IS NOT NULL))"
				+" ORDER BY DataLevantamento DESC");

			if (ds.Tables[0].Rows.Count > 0)
			{
				DataSet dsAuditorias = new DataSet();
				DataRow row;
				DataTable table = new DataTable();
				table.Columns.Add("IdAuditoria", Type.GetType("System.String"));
				table.Columns.Add("DataLevantamento", Type.GetType("System.String"));
				dsAuditorias.Tables.Add(table);

				foreach (DataRow rowAuditoria in ds.Tables[0].Select())
				{
					row = dsAuditorias.Tables[0].NewRow();
					row["IdAuditoria"] = rowAuditoria["IdAuditoria"];
					row["DataLevantamento"] = Convert.ToDateTime(rowAuditoria["DataLevantamento"]).ToString("dd-MM-yyyy");
					dsAuditorias.Tables[0].Rows.Add(row);
				}
				
				ddlAuditoria.DataSource = dsAuditorias;
				ddlAuditoria.DataValueField = "IdAuditoria";
				ddlAuditoria.DataTextField = "DataLevantamento";
				ddlAuditoria.DataBind();
			}
			else
			{
				lblSelecioneAuditoria.ForeColor = Color.Gray;
				ddlAuditoria.Enabled = false;
				ckbService.Enabled = false;
				btnGravarServico.Enabled = false;
				DisableServiceWebControls();
			}
		}

		private void PopulaLbxColaboradores()
		{
            Cliente cliente;
            cliente = new Cliente(System.Convert.ToInt32(Session["Empresa"].ToString()));

            ArrayList prestadores = new Prestador().GetListaPrestador(cliente, false, (int)TipoPrestador.TodosPrestadores);  //, (int)TipoPrestador.ContatoEmpresa);
//			prestadores.Sort();
			lbxColaboradores.DataSource = prestadores;
			lbxColaboradores.DataValueField = "Email";
			lbxColaboradores.DataTextField = "NomeCompleto";
			lbxColaboradores.DataBind();

			foreach (ListItem emailItem in lbxEmails.Items)
				while (lbxColaboradores.Items.Contains(lbxColaboradores.Items.FindByValue(emailItem.Text)))
					lbxColaboradores.Items.Remove(lbxColaboradores.Items.FindByValue(emailItem.Text));
		}

		private void DisableServiceWebControls()
		{
			Color backcolor = Color.FromName("#EBEBEB");
			
			lblInicioServico.ForeColor = Color.Gray;
			wneDias.BorderColor = Color.LightGray;
			wneDias.BackColor = backcolor;
			wneDias.Text = "0";
			wneDias.Enabled = false;
			lblDiasAntes.ForeColor = Color.Gray;
			lblPeriodicidade.ForeColor = Color.Gray;
			rblPeriodicidade.ClearSelection();
			rblPeriodicidade.Enabled = false;
			lblSelecioneEmail.ForeColor = Color.Gray;
			lblColaboradores.ForeColor = Color.Gray;
			lblEmails.ForeColor = Color.Gray;
			lbxColaboradores.BackColor = backcolor;
			lbxColaboradores.Items.Clear();
			lbxColaboradores.Enabled = false;
			lbxEmails.BackColor = backcolor;
			lbxEmails.Items.Clear();
			lbxEmails.Enabled = false;
			btnAdicionarSelecionado.Enabled = false;
			btnAdicionarTodos.Enabled = false;
			btnRemoverSelecionado.Enabled = false;
			btnRemoverTodos.Enabled = false;
			lblEmail.ForeColor = Color.Gray;
			txtEmail.BorderColor = Color.LightGray;
			txtEmail.BackColor = backcolor;
			txtEmail.Text = string.Empty;
			txtEmail.Enabled = false;
			btnAdicionarEmail.Enabled = false;
		}

		private void EnableServiceWebControls()
		{
			Color forebordercolor = Color.FromName("#44926D");
			Color backcolor = Color.FromName("#FCFEFD");
						
			lblInicioServico.ForeColor = forebordercolor;
			wneDias.BorderColor = forebordercolor;
			wneDias.BackColor = backcolor;
			wneDias.Enabled = true;
			lblDiasAntes.ForeColor = forebordercolor;
			lblPeriodicidade.ForeColor = forebordercolor;
			rblPeriodicidade.Enabled = true;
			lblSelecioneEmail.ForeColor = forebordercolor;
			lblColaboradores.ForeColor = forebordercolor;
			lblEmails.ForeColor = forebordercolor;
			lbxColaboradores.BackColor = backcolor;
			lbxColaboradores.Enabled = true;
			lbxEmails.BackColor = backcolor;
			lbxEmails.Enabled = true;
			btnAdicionarSelecionado.Enabled = true;
			btnAdicionarTodos.Enabled = true;
			btnRemoverSelecionado.Enabled = true;
			btnRemoverTodos.Enabled = true;
			lblEmail.ForeColor = forebordercolor;
			txtEmail.BorderColor = forebordercolor;
			txtEmail.BackColor = backcolor;
			txtEmail.Enabled = true;
			btnAdicionarEmail.Enabled = true;
		}

		protected void ckbService_CheckedChanged(object sender, System.EventArgs e)
		{
			if (ckbService.Checked)
			{
				EnableServiceWebControls();

				AvisoAuditoria avisoAuditoria = new AvisoAuditoria();
				avisoAuditoria.Find("IdAuditoria=" + ddlAuditoria.SelectedValue	+ " AND IsAtivo=1");

				if (avisoAuditoria.Id != 0)
				{
					wneDias.Text = avisoAuditoria.DiasInicio.ToString();
					rblPeriodicidade.Items.FindByValue(avisoAuditoria.Periodicidade.ToString()).Selected = true;
					PopulaLbxEmailSelecionados(avisoAuditoria);
				}
				
				PopulaLbxColaboradores();
			}
			else
				DisableServiceWebControls();
		}

		protected void ddlAuditoria_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			AvisoAuditoria avisoAuditoria = new AvisoAuditoria();
			avisoAuditoria.Find("IdAuditoria=" + ddlAuditoria.SelectedValue);

			if (avisoAuditoria.Id != 0 && avisoAuditoria.IsAtivo)
			{
				EnableServiceWebControls();

				ckbService.Checked = true;
				wneDias.Text = avisoAuditoria.DiasInicio.ToString();
				rblPeriodicidade.ClearSelection();
				rblPeriodicidade.Items.FindByValue(avisoAuditoria.Periodicidade.ToString()).Selected = true;
				PopulaLbxEmailSelecionados(avisoAuditoria);
				PopulaLbxColaboradores();
			}
			else
			{
				ckbService.Checked = false;
				DisableServiceWebControls();
			}
		}

		protected void btnGravarServico_Click(object sender, System.EventArgs e)
		{
			try
			{
				string tipo = string.Empty;

				AvisoAuditoria avisoAuditoria = new AvisoAuditoria();
				avisoAuditoria.Find("IdAuditoria=" + ddlAuditoria.SelectedValue);

				if (avisoAuditoria.Id != 0)
					tipo = "editado";
				else
				{
					avisoAuditoria = new AvisoAuditoria();
					avisoAuditoria.Inicialize();
					tipo = "cadastrado";
				}

				avisoAuditoria.IdAuditoria.Id = Convert.ToInt32(ddlAuditoria.SelectedValue);
				avisoAuditoria.IsAtivo = ckbService.Checked;
				if (ckbService.Checked)
				{
					avisoAuditoria.DiasInicio = System.Convert.ToInt32( wneDias.Text );
                    if (rblPeriodicidade.SelectedItem == null)
                    {
                        MsgBox1.Show("Ilitera.Net", "A Periodicidade do envio do e-mail de alerta é obrigatória!", null,
                        new EO.Web.MsgBoxButton("OK"));
                        return;                        
                    }
					avisoAuditoria.Periodicidade = Convert.ToInt32(rblPeriodicidade.SelectedValue);	
				}
				else
					avisoAuditoria.Emails = string.Empty;

                Entities.Usuario usuario = (Entities.Usuario)Session["usuarioLogado"];


				avisoAuditoria.UsuarioId = usuario.IdUsuario;
				avisoAuditoria.Save();

                MsgBox1.Show("Ilitera.Net", "O Serviço de Alerta da Auditoria foi " + tipo + " com sucesso!", null,
                new EO.Web.MsgBoxButton("OK"));
                return;                        
			}
			catch (Exception ex)
			{
                MsgBox1.Show("Ilitera.Net", ex.Message, null,
                new EO.Web.MsgBoxButton("OK"));
                return;                        
			}
		}

		protected void btnAdicionarSelecionado_Click(object sender, System.EventArgs e)
		{
			StringBuilder emails = new StringBuilder();

			AvisoAuditoria avisoAuditoria = new AvisoAuditoria();
			avisoAuditoria.Find("IdAuditoria=" + ddlAuditoria.SelectedValue);

			foreach (ListItem itemColaborador in lbxColaboradores.Items)
				if (itemColaborador.Selected)
					if (avisoAuditoria.Emails.IndexOf(itemColaborador.Value).Equals(-1) && emails.ToString().IndexOf(itemColaborador.Value).Equals(-1))
					{
						emails.Append(";");
						emails.Append(itemColaborador.Value);
					}

			AdicionaEmails(emails, avisoAuditoria);
		}

		private void AdicionaEmails(StringBuilder emails, AvisoAuditoria avisoAuditoria)
		{
            if (emails.ToString() != string.Empty)
                try
                {
                    if (avisoAuditoria.Id == 0)
                    {
                        avisoAuditoria = new AvisoAuditoria();
                        avisoAuditoria.Inicialize();
                        avisoAuditoria.IdAuditoria.Id = Convert.ToInt32(ddlAuditoria.SelectedValue);
                        avisoAuditoria.IsAtivo = ckbService.Checked;
                        avisoAuditoria.DiasInicio = System.Convert.ToInt32(wneDias.Text);
                        if (rblPeriodicidade.SelectedItem == null)
                        {
                            MsgBox1.Show("Ilitera.Net", "É necessário selecionar a periodicidade do envio do e-mail de alerta antes de adicionar um e-mail!", null,
                            new EO.Web.MsgBoxButton("OK"));
                            return;
                        }
                        avisoAuditoria.Periodicidade = Convert.ToInt32(rblPeriodicidade.SelectedValue);
                    }

                    if (avisoAuditoria.Emails == string.Empty)
                        emails.Remove(0, 1);
                    avisoAuditoria.Emails += emails.ToString();

                    Entities.Usuario usuario = (Entities.Usuario)Session["usuarioLogado"];

                    avisoAuditoria.UsuarioId = usuario.IdUsuario;
                    avisoAuditoria.UsuarioProcessoRealizado = "Cadastro de e-mails dos colaboradores para o serviço de alerta para Auditoria";
                    avisoAuditoria.Save();

                    PopulaLbxEmailSelecionados(avisoAuditoria);
                    PopulaLbxColaboradores();

                    MsgBox1.Show("Ilitera.Net", "Os e-mails dos colaboradores foram adicionados com sucesso!", null,
                    new EO.Web.MsgBoxButton("OK"));
                    return;

                }
                catch (Exception ex)
                {
                    MsgBox1.Show("Ilitera.Net", ex.Message, null,
                    new EO.Web.MsgBoxButton("OK"));
                }
            else
            {
                MsgBox1.Show("Ilitera.Net", "Não há nenhum colaborador ou não há nenhum e-mail cadastrado para os colaboradores!", null,
                new EO.Web.MsgBoxButton("OK"));
                return;
            }
				
		}

		protected void btnAdicionarTodos_Click(object sender, System.EventArgs e)
		{
			StringBuilder emails = new StringBuilder();

			AvisoAuditoria avisoAuditoria = new AvisoAuditoria();
			avisoAuditoria.Find("IdAuditoria=" + ddlAuditoria.SelectedValue);

			foreach (ListItem itemColaborador in lbxColaboradores.Items)
				if (avisoAuditoria.Emails.IndexOf(itemColaborador.Value).Equals(-1) && emails.ToString().IndexOf(itemColaborador.Value).Equals(-1))
				{
					emails.Append(";");
					emails.Append(itemColaborador.Value);
				}

			AdicionaEmails(emails, avisoAuditoria);
		}

		protected void btnRemoverSelecionado_Click(object sender, System.EventArgs e)
		{
			RemoveEmails(false);
		}

		private void RemoveEmails(bool isTodosItems)
		{
			try
			{				
				AvisoAuditoria avisoAuditoria = new AvisoAuditoria();
				avisoAuditoria.Find("IdAuditoria=" + ddlAuditoria.SelectedValue);

				string emails = avisoAuditoria.Emails;
                if (emails.Equals(string.Empty))
                {
                    MsgBox1.Show("Ilitera.Net", "Não há nenhum e-mail cadastrado para ser removido!", null,
                    new EO.Web.MsgBoxButton("OK"));
                    return;                        
                }

				if (isTodosItems)
				{
					foreach (ListItem itemEmail in lbxEmails.Items)
						if (emails.IndexOf(";" + itemEmail.Text).Equals(-1))
							if (emails.IndexOf(";").Equals(-1))
								emails = string.Empty;
							else
								emails = emails.Substring(itemEmail.Text.Length + 1);
						else
							emails = emails.Substring(0, emails.IndexOf(";" + itemEmail.Text)) + emails.Substring(emails.IndexOf(";" + itemEmail.Text) + itemEmail.Text.Length + 1);
				}
				else
				{
					foreach (ListItem itemEmail in lbxEmails.Items)
						if (itemEmail.Selected)
						{
							if (emails.IndexOf(";" + itemEmail.Text).Equals(-1))
								if (emails.IndexOf(";").Equals(-1))
									emails = string.Empty;
								else
									emails = emails.Substring(itemEmail.Text.Length + 1);
							else
								emails = emails.Substring(0, emails.IndexOf(";" + itemEmail.Text)) + emails.Substring(emails.IndexOf(";" + itemEmail.Text) + itemEmail.Text.Length + 1);
						}
				}

                if (avisoAuditoria.Emails.Equals(emails))
                {
                    MsgBox1.Show("Ilitera.Net", "Não há nenhum e-mail selecionado para ser removido!", null,
                    new EO.Web.MsgBoxButton("OK"));
                    return;                        
                }

                Entities.Usuario usuario = (Entities.Usuario)Session["usuarioLogado"];

				avisoAuditoria.Emails = emails;
                avisoAuditoria.UsuarioId = usuario.IdUsuario;
				avisoAuditoria.UsuarioProcessoRealizado = "Exclusão do cadastro de e-mails dos colaboradores para o serviço de alerta para Auditoria";
				avisoAuditoria.Save();

				PopulaLbxEmailSelecionados(avisoAuditoria);
				PopulaLbxColaboradores();

                MsgBox1.Show("Ilitera.Net", "Os e-mails foram removidos com sucesso!", null,
                new EO.Web.MsgBoxButton("OK"));

				
			}
			catch (Exception ex)
			{
                MsgBox1.Show("Ilitera.Net", ex.Message, null,
                new EO.Web.MsgBoxButton("OK"));
			}
		}

		protected void btnRemoverTodos_Click(object sender, System.EventArgs e)
		{
			RemoveEmails(true);
		}

		protected void btnAdicionarEmail_Click(object sender, System.EventArgs e)
		{
			if (!txtEmail.Text.Trim().Equals(string.Empty))
				try
				{
					StringBuilder emailsCorretos = new StringBuilder();
					char[] seps = {',', ';', '\n', '\r'};
					string[] emails = txtEmail.Text.Trim().Split(seps);

					//foreach (string email in emails)
     //                   if (!EmailBase.IsEmail(email.Trim()))
     //                   {
     //                       MsgBox1.Show("Ilitera.Net", "Um ou mais e-mails não são válidos!", null,
     //                       new EO.Web.MsgBoxButton("OK"));
     //                       return;                        
     //                   }

					foreach (string emailCorreto in emails)
					{
						emailsCorretos.Append(";"); 
						emailsCorretos.Append(emailCorreto.Trim());
					}

					AvisoAuditoria avisoAuditoria = new AvisoAuditoria();
					avisoAuditoria.Find("IdAuditoria=" + ddlAuditoria.SelectedValue);

					if (avisoAuditoria.Id == 0)
					{
						avisoAuditoria = new AvisoAuditoria();
						avisoAuditoria.Inicialize();
						avisoAuditoria.IdAuditoria.Id = Convert.ToInt32(ddlAuditoria.SelectedValue);
						avisoAuditoria.IsAtivo = ckbService.Checked;
						avisoAuditoria.DiasInicio = System.Convert.ToInt32( wneDias.Text );
                        if (rblPeriodicidade.SelectedItem == null)
                        {
                            MsgBox1.Show("Ilitera.Net", "É necessário selecionar a periodicidade do envio do e-mail de alerta antes de adicionar um e-mail!", null,
                            new EO.Web.MsgBoxButton("OK"));
                            return;                           
                        }
						avisoAuditoria.Periodicidade = Convert.ToInt32(rblPeriodicidade.SelectedValue);
					}

					if (avisoAuditoria.Emails.Equals(string.Empty))
						emailsCorretos.Remove(0, 1);

					avisoAuditoria.Emails += emailsCorretos.ToString();

                    Entities.Usuario usuario = (Entities.Usuario)Session["usuarioLogado"];
                    
					avisoAuditoria.UsuarioId = usuario.IdUsuario;
					avisoAuditoria.UsuarioProcessoRealizado = "Cadastro de e-mails para o serviço de alerta para Auditoria";
					avisoAuditoria.Save();
					
					txtEmail.Text = string.Empty;
					PopulaLbxEmailSelecionados(avisoAuditoria);
					PopulaLbxColaboradores();

                    MsgBox1.Show("Ilitera.Net", "Os e-mails foram cadastrados com sucesso!", null,
                    new EO.Web.MsgBoxButton("OK"));
				}
				catch (Exception ex)
				{
                    MsgBox1.Show("Ilitera.Net", ex.Message, null,
                    new EO.Web.MsgBoxButton("OK"));
				}
			else
                MsgBox1.Show("Ilitera.Net", "É necessário digitar ao menos 1 e-mail!", null,
                new EO.Web.MsgBoxButton("OK"));

				
		}
	}
}
