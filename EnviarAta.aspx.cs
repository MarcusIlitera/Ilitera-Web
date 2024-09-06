using Ilitera.Common;
using Ilitera.Opsa.Data;
using Ilitera.Opsa.Report;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Ilitera.Net
{
    public partial class EnviarAta : System.Web.UI.Page
    {
        private Cliente cliente = new Cliente();
        private Cipa cipa = new Cipa();
        private Juridica juridica = new Juridica();
        private ReuniaoCipa reuniaoCipa;
        protected void Page_Load(object sender, EventArgs e)
        {
            cliente.Find(Convert.ToInt32(Session["Empresa"]));
            juridica = cliente;
            cipa = cliente.GetGestaoAtual();
            int idReuniao = 0;
            if (Request["r"] != null)
            {
                idReuniao = Convert.ToInt32(Request["r"]);
                reuniaoCipa = new ReuniaoCipa();
                reuniaoCipa.Find(idReuniao);
            }


            if (!IsPostBack)
            {
                PopulaGrdEmails();
                PopulaGrdMembros();

                int numReuniao = reuniaoCipa.GetNumeroReuniao();
                if (numReuniao != 13)
                    txtAssuntoEmail.Text = $"Ata - {reuniaoCipa.GetNumeroReuniao()}ª Reunião";
                else
                    txtAssuntoEmail.Text = $"Ata - Reunião Extraordinária ({reuniaoCipa.DataSolicitacao.ToString("dd/MM/yyyy")})";

                txtSaudacao.Text = "Prezados,";
                StringBuilder sb = new StringBuilder();
                sb.AppendLine($"Segue, anexa, Ata da {reuniaoCipa.GetNumeroReuniao()}ª reunião realizada em {reuniaoCipa.DataSolicitacao.ToString("dd/MM/yyyy")}.");
                txtCorpoEmail.Text = sb.ToString();
            }
        }

        private void PopulaGrdEmails()
        {
            grdEmails.DataSource = null;
            grdEmails.DataBind();
            DataSet Empregados = new ListaEmailsEnviarAta().Get("IdReuniaoCipa = " + reuniaoCipa.Id);

            DataTable zDt = new DataTable();
            zDt.Columns.Add("IdListaEmailsEnviarAta");
            zDt.Columns.Add("Editar");
            zDt.Columns.Add("Nome");
            zDt.Columns.Add("Email");

            if (Empregados.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in Empregados.Tables[0].Rows)
                {
                    zDt.Rows.Add(Convert.ToInt32(row.ItemArray[0]), "Editar", row.ItemArray[4], row.ItemArray[5]);
                }

                DataSet zDs = new DataSet();
                zDs.Tables.Add(zDt);
                grdEmails.DataSource = zDs;
                grdEmails.DataBind();
            }
            else
            {
                grdEmails.DataSource = null;
                grdEmails.DataBind();
                grdEmails.Items.Clear();
            }
        }

        private void PopulaGrdMembros()
        {
            grdMembros.DataSource = null;
            grdMembros.DataBind();
            EventoCipa eventoCipa = EventoCipa.GetEventoCipa(cipa, EventoBase.ComissaoEleitoral);
            Cipa cipaGestaoAtual = cliente.GetGestaoAtual();
            string strWhere;
            int selectedValue = Convert.ToInt32(rbMembros.SelectedValue);
            switch (selectedValue)
            {
                case 1:
                    grdMembros.Items.Clear();
                    grdMembros.DataSource = null;
                    grdMembros.DataBind();
                    strWhere = "IdCipa =" + cipaGestaoAtual.Id
                    + " AND Numero = 0 "
                    + " AND IndGrupoMembro IN (0, 1) "
                    + " AND IndStatus=" + ((int)MembroCipa.Status.Ativo).ToString()
                    + " AND IndTitularSuplente=" + (int)TitularSuplente.Titular
                    + " AND NomeMembro NOT IN("
                    + " SELECT Nome FROM ListaEmailsEnviarAta WHERE IdReuniaoCipa = " + reuniaoCipa.Id + ")"
                    + " AND IdEmpregado NOT IN("
                    + " SELECT nID_EMPREGADO FROM Sied_Novo.dbo.tblEMPREGADO Where tNO_EMPG IN("
                    + " SELECT Nome FROM ListaEmailsEnviarAta WHERE IdReuniaoCipa = " + reuniaoCipa.Id + "))"
                    + " ORDER BY Numero";

                    DataSet presidentes = new MembroCipa().Get(strWhere);
                    DataTable zDt = new DataTable();
                    zDt.Columns.Add("IdEmprcImaegado");
                    zDt.Columns.Add("Empregado");
                    zDt.Columns.Add("Cargo");

                    foreach (DataRow row in presidentes.Tables[0].Rows)
                    {
                        Empregado empregado = new Empregado();
                        empregado.Find(Convert.ToInt32(row["IdEmpregado"]));

                        MembroCipa membroCipa = new MembroCipa();
                        membroCipa.Find(Convert.ToInt32(row["IdMembroCipa"]));
                        string cargoCipa = membroCipa.GetNomeCargo();

                        zDt.Rows.Add(row["IdEmpregado"], empregado.tNO_EMPG, cargoCipa);
                    }

                    DataSet zDs = new DataSet();
                    zDs.Tables.Add(zDt);
                    grdMembros.DataSource = zDs;
                    grdMembros.DataBind();
                    break;
                case 2:
                    grdMembros.Items.Clear();
                    grdMembros.DataSource = null;
                    grdMembros.Columns[1].Visible = true;
                    grdMembros.Columns[1].Width = 150;
                    grdMembros.Columns[0].Width = 330;
                    grdMembros.Width = 480;
                    grdMembros.DataBind();
                    strWhere = "IdCipa =" + cipaGestaoAtual.Id
                        + " AND IndStatus=" + ((int)MembroCipa.Status.Ativo).ToString()
                        + " AND IdEmpregado Not IN ("
                        + " SELECT nID_EMPREGADO FROM Sied_Novo.dbo.tblEMPREGADO Where tNO_EMPG IN(SELECT Nome FROM ListaEmailsEnviarAta(NOLOCK) WHERE IdReuniaoCipa = " + reuniaoCipa.Id + "))"
                        + " UNION SELECT MembroCipa.IdMembroCipa, IdCipa, IdEmpregado, NomeMembro, Numero, Estabilidade, IndTitularSuplente, IndGrupoMembro, IndStatus FROM MembroCipa"
                        + " Where IdCipa = " + cipa.Id + " AND IdEmpregado IS NULL AND NomeMembro NOT IN (SELECT Nome FROM ListaEmailsEnviarAta  WHERE IdReuniaoCipa = " + reuniaoCipa.Id + ")"
                        + " ORDER BY Numero";

                    DataSet todosIntegrantes = new MembroCipa().Get(strWhere);
                    zDt = new DataTable();
                    zDt.Columns.Add("IdEmprcImaegado");
                    zDt.Columns.Add("Empregado");
                    zDt.Columns.Add("Cargo");

                    ArrayList membros = new MembroComissaoEleitoral().Find("IdCipa=" + eventoCipa.IdCipa.Id);

                    foreach (DataRow row in todosIntegrantes.Tables[0].Rows)
                    {
                        Empregado empregado = new Empregado();
                        MembroCipa membroCipa = new MembroCipa();
                        if (row["IdEmpregado"] != DBNull.Value)
                        {
                            empregado.Find(Convert.ToInt32(row["IdEmpregado"]));
                            membroCipa.Find(Convert.ToInt32(row["IdMembroCipa"]));
                            string cargoCipa = membroCipa.GetNomeCargo();
                            zDt.Rows.Add(row["IdEmpregado"], empregado.tNO_EMPG, cargoCipa);
                        }
                        else
                        {
                            membroCipa.Find(Convert.ToInt32(row["IdMembroCipa"]));
                            string cargoCipa = membroCipa.GetNomeCargo();
                            zDt.Rows.Add(0, membroCipa.NomeMembro, cargoCipa);
                        }
                    }

                    zDs = new DataSet();
                    zDs.Tables.Add(zDt);
                    grdMembros.DataSource = zDs;
                    grdMembros.DataBind();
                    break;
                case 3:
                    grdMembros.Items.Clear();
                    grdMembros.DataSource = null;
                    grdMembros.DataBind();
                    grdMembros.Columns[1].Visible = false;
                    grdMembros.Columns[1].Width = 0;
                    grdMembros.Columns[0].Width = 460;
                    grdMembros.Width = 480;
                    strWhere = "nID_EMPR=" + cliente.Id + " AND hDT_DEM IS NULL AND (tNO_EMPG NOT IN(SELECT Nome FROM Opsa.dbo.ListaEmailsEnviarAta (NOLOCK) WHERE IdReuniaoCipa = " + reuniaoCipa.Id + ") OR nID_EMPREGADO NOT IN (SELECT IdEmpregado FROM Opsa.dbo.ListaEmailsEnviarAta(NOLOCK) WHERE IdReuniaoCipa = " + reuniaoCipa.Id + "))ORDER BY tNO_EMPG";
                    DataSet empregados = new Empregado().Get(strWhere);
                    zDt = new DataTable();
                    zDt.Columns.Add("IdEmprcImaegado");
                    zDt.Columns.Add("Empregado");
                    zDt.Columns.Add("Cargo");

                    foreach (DataRow row in empregados.Tables[0].Rows)
                    {
                        zDt.Rows.Add(row["nID_EMPREGADO"], row["tNO_EMPG"], "");
                    }

                    zDs = new DataSet();
                    zDs.Tables.Add(zDt);
                    grdMembros.DataSource = zDs;
                    grdMembros.DataBind();
                    break;
            }
        }

        protected void rbMembros_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulaGrdMembros();
        }

        protected void btnRemover_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                ListaEmailsEnviarAta email = new ListaEmailsEnviarAta();
                email.Find(Convert.ToInt32(grdEmails.Items[grdEmails.SelectedItemIndex].Key));
                email.Delete();
            }
            catch (ArgumentOutOfRangeException)
            {
                MsgBox1.Show("Ilitera.Net", "Nenhum item foi selecionado", null, new EO.Web.MsgBoxButton("OK"));
            }
            finally
            {
                PopulaGrdEmails();
                PopulaGrdMembros();
            }
            string script = @"
                                    <script type='text/javascript'>
                                        setTimeout(function() {
                                            var contentElement = document.getElementById('MsgBox1_msg_t');
                                            if (contentElement) {
                                                contentElement.style.color = 'rgb(125, 123, 123)';
                                                contentElement.style.fontSize = '14px';
                                                contentElement.style.fontStyle = 'normal';
                                                contentElement.style.fontVariant = 'normal';
                                                contentElement.style.fontWeight = '500';
                                                contentElement.style.fontFamily = 'UniviaPro';
                                            }
                                        }, 100); 
                                        setTimeout(function() {
                                            var contentElement = document.getElementById('MsgBox1_headerHtml');
                                            if (contentElement) {
                                                contentElement.style.color = 'rgb(28, 148, 137)';
                                                contentElement.style.fontSize = '14px';
                                                contentElement.style.fontStyle = 'normal';
                                                contentElement.style.fontVariant = 'normal';
                                                contentElement.style.fontWeight = '500';
                                                contentElement.style.fontFamily = 'UniviaPro';
                                            }
                                        }, 100);
                                    </script>";
            ClientScript.RegisterStartupScript(this.GetType(), "ApplyMsgBoxStyles", script);
        }

        protected void btnAdicionar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                ListaEmailsEnviarAta listaEmails = new ListaEmailsEnviarAta();
                listaEmails.Inicialize();
                listaEmails.IdReuniaoCipa = reuniaoCipa.Id;
                Empregado empregado = new Empregado();
                int id = Convert.ToInt32(grdMembros.Items[grdMembros.SelectedItemIndex].Key);
                if (id != 0)
                {
                    empregado.Find(id);
                    listaEmails.Nome = empregado.tNO_EMPG;
                    listaEmails.Email = empregado.teMail;
                    listaEmails.IdEmpregado = empregado.Id;
                }
                {
                    listaEmails.Nome = grdMembros.Items[grdMembros.SelectedItemIndex].Cells["Empregado"].Value.ToString();
                }
                listaEmails.Save();
            }
            catch (ArgumentOutOfRangeException)
            {
                MsgBox1.Show("Ilitera.Net", "Nenhum item foi selecionado", null, new EO.Web.MsgBoxButton("OK"));
            }
            finally
            {
                PopulaGrdEmails();
                PopulaGrdMembros();
            }
            string script = @"
                                    <script type='text/javascript'>
                                        setTimeout(function() {
                                            var contentElement = document.getElementById('MsgBox1_msg_t');
                                            if (contentElement) {
                                                contentElement.style.color = 'rgb(125, 123, 123)';
                                                contentElement.style.fontSize = '14px';
                                                contentElement.style.fontStyle = 'normal';
                                                contentElement.style.fontVariant = 'normal';
                                                contentElement.style.fontWeight = '500';
                                                contentElement.style.fontFamily = 'UniviaPro';
                                            }
                                        }, 100); 
                                        setTimeout(function() {
                                            var contentElement = document.getElementById('MsgBox1_headerHtml');
                                            if (contentElement) {
                                                contentElement.style.color = 'rgb(28, 148, 137)';
                                                contentElement.style.fontSize = '14px';
                                                contentElement.style.fontStyle = 'normal';
                                                contentElement.style.fontVariant = 'normal';
                                                contentElement.style.fontWeight = '500';
                                                contentElement.style.fontFamily = 'UniviaPro';
                                            }
                                        }, 100);
                                    </script>";
            ClientScript.RegisterStartupScript(this.GetType(), "ApplyMsgBoxStyles", script);
        }

        protected void grdEmails_ItemCommand(object sender, EO.Web.GridCommandEventArgs e)
        {
            int id = Convert.ToInt32(e.Item.Key);
            ListaEmailsEnviarAta email = new ListaEmailsEnviarAta();
            email.Find(id);
            lblEmail.Visible = true;
            lblEmail.Text = "Insira um novo e-mail para " + email.Nome;
            txtEmail.Attributes["data-id"] = email.Id.ToString();
            txtEmail.Visible = true;
            txtEmail.Text = email.Email;
            btnEmailAplicar.Visible = true;
            btnEmailCancelar.Visible = true;
        }

        protected void btnEmailAplicar_Click(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(txtEmail.Attributes["data-id"]);
                ListaEmailsEnviarAta empregado = new ListaEmailsEnviarAta();
                empregado.Find(id);
                MailAddress email = new MailAddress(txtEmail.Text);
                empregado.Email = email.ToString();
                empregado.Save();
                lblEmail.Visible = false;
                lblEmail.Text = "";
                txtEmail.Attributes.Remove("data-id");
                txtEmail.Visible = false;
                txtEmail.Text = "";
                btnEmailAplicar.Visible = false;
                btnEmailCancelar.Visible = false;
            }
            catch (FormatException)
            {
                MsgBox1.Show("Formato Inválido", "Insira um e-mail válido", null, new EO.Web.MsgBoxButton("OK"));
            }
            catch (ArgumentNullException)
            {
                MsgBox1.Show("Formato Inválido", "Insira um e-mail válido", null, new EO.Web.MsgBoxButton("OK"));
            }

            PopulaGrdEmails();
            string script = @"
                                    <script type='text/javascript'>
                                        setTimeout(function() {
                                            var contentElement = document.getElementById('MsgBox1_msg_t');
                                            if (contentElement) {
                                                contentElement.style.color = 'rgb(125, 123, 123)';
                                                contentElement.style.fontSize = '14px';
                                                contentElement.style.fontStyle = 'normal';
                                                contentElement.style.fontVariant = 'normal';
                                                contentElement.style.fontWeight = '500';
                                                contentElement.style.fontFamily = 'UniviaPro';
                                            }
                                        }, 100); 
                                        setTimeout(function() {
                                            var contentElement = document.getElementById('MsgBox1_headerHtml');
                                            if (contentElement) {
                                                contentElement.style.color = 'rgb(28, 148, 137)';
                                                contentElement.style.fontSize = '14px';
                                                contentElement.style.fontStyle = 'normal';
                                                contentElement.style.fontVariant = 'normal';
                                                contentElement.style.fontWeight = '500';
                                                contentElement.style.fontFamily = 'UniviaPro';
                                            }
                                        }, 100);
                                    </script>";
            ClientScript.RegisterStartupScript(this.GetType(), "ApplyMsgBoxStyles", script);
        }

        protected void btnEmailCancelar_Click(object sender, EventArgs e)
        {
            lblEmail.Visible = false;
            lblEmail.Text = "";
            txtEmail.Attributes.Remove("data-id");
            txtEmail.Visible = false;
            txtEmail.Text = "";
            btnEmailAplicar.Visible = false;
            btnEmailCancelar.Visible = false;
        }

        protected void btnEnviarAta_Click(object sender, EventArgs e)
        {
            try
            {
                string listaEmails = "";
                string[] separador = new string[] { ";" };
                DataSet emails = new ListaEmailsEnviarAta().Get("IdReuniaoCipa = " + reuniaoCipa.Id);
                foreach (DataRow row in emails.Tables[0].Rows)
                {
                    listaEmails += row.ItemArray[5].ToString() + ";";
                }

                MailMessage email = new MailMessage();
                email.Priority = MailPriority.Normal;
                email.IsBodyHtml = true;
                email.SubjectEncoding = Encoding.GetEncoding("ISO-8859-1");
                email.BodyEncoding = Encoding.GetEncoding("ISO-8859-1");
                foreach (string destinatario in listaEmails.Trim().Split(separador, StringSplitOptions.RemoveEmptyEntries))
                {
                    try
                    {
                        MailAddress paraEmail = new MailAddress(destinatario);
                        email.To.Add(paraEmail);
                        email.CC.Add(paraEmail);
                    }
                    catch (Exception)
                    {
                    }
                }
                email.Subject = txtAssuntoEmail.Text;
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("<p style='color:#008080'>" + txtSaudacao.Text + "</style></p>");
                sb.AppendLine("<p>" + txtCorpoEmail.Text + "</p>");
                sb.AppendLine("<br>");
                sb.AppendLine("<img src=\"cid:minhaImagem\" />");
                sb.AppendLine("<hr>");
                sb.AppendLine("<p style='font-size: 0.8rem;font-style: italic;'>Este é um e-mail automático e, portanto, não deve ser respondido diretamente. Se você precisar de assistência ou tiver alguma dúvida, entre em contato conosco através dos canais de comunicação disponíveis em nosso site.</p>");
                sb.AppendLine("<p style='font-size: 0.8rem;font-style: italic;'>Este e - mail pode conter informações confidenciais e / ou privilegiadas.Se você não for o destinatário correto, por favor, exclua - o imediatamente e avise - nos.A divulgação, cópia, distribuição ou qualquer outra ação tomada com base neste e - mail é estritamente proibida.</p>");
                AlternateView htmlView = AlternateView.CreateAlternateViewFromString(sb.ToString(), null, System.Net.Mime.MediaTypeNames.Text.Html);
                LinkedResource linkedImage = new LinkedResource(@"C:\Users\VM02\Desktop\ilitera-email.png", System.Net.Mime.MediaTypeNames.Image.Jpeg)
                {
                    ContentId = "minhaImagem",
                    TransferEncoding = System.Net.Mime.TransferEncoding.Base64
                };
                htmlView.LinkedResources.Add(linkedImage);
                email.AlternateViews.Add(htmlView);


                RptAtaReuniaoCipa report = new DataSourceAtaReuniaoCIPA(reuniaoCipa).GetReport();
                System.IO.Stream reportStream = report.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                reportStream.Position = 0;
                string nomeArquivo = "";
                int numReuniao = reuniaoCipa.GetNumeroReuniao();
                if (numReuniao != 13)
                    nomeArquivo = $"Ata - {reuniaoCipa.GetNumeroReuniao()}ª Reunião";
                else
                    nomeArquivo = $"Ata - Reunião Extraordinária ({reuniaoCipa.DataSolicitacao.ToString("dd/MM/yyyy")})";

                Attachment attachment = new Attachment(reportStream, nomeArquivo, System.Net.Mime.MediaTypeNames.Application.Pdf);
                email.Attachments.Add(attachment);

                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.ilitera.com.br";
                smtp.Port = 587;

                if (System.DateTime.Now.Second % 3 == 0)
                {
                    email.From = new MailAddress("agendamento1.sp.sto@ilitera.com.br");
                    smtp.Credentials = new NetworkCredential("agendamento1.sp.sto@ilitera.com.br", "Ilitera_3624");
                }
                else
                {
                    email.From = new MailAddress("agendamento3.sp.sto@ilitera.com.br");
                    smtp.Credentials = new NetworkCredential("agendamento3.sp.sto@ilitera.com.br", "Ilitera_3625");
                }
                email.ReplyTo = new MailAddress("agendamento@ilitera.com.br");

                smtp.Send(email);
                reportStream.Close();
                MsgBox1.Show("E-mails enviados", "Os e-mails com as atas foram enviados com sucesso", null, new EO.Web.MsgBoxButton("OK"));
            }
            catch (Exception)
            {
                MsgBox1.Show("Erro", "Não foi possível enviar os e-mails com as atas", null, new EO.Web.MsgBoxButton("OK"));
            }
            string script = @"
                                    <script type='text/javascript'>
                                        setTimeout(function() {
                                            var contentElement = document.getElementById('MsgBox1_msg_t');
                                            if (contentElement) {
                                                contentElement.style.color = 'rgb(125, 123, 123)';
                                                contentElement.style.fontSize = '14px';
                                                contentElement.style.fontStyle = 'normal';
                                                contentElement.style.fontVariant = 'normal';
                                                contentElement.style.fontWeight = '500';
                                                contentElement.style.fontFamily = 'UniviaPro';
                                            }
                                        }, 100); 
                                        setTimeout(function() {
                                            var contentElement = document.getElementById('MsgBox1_headerHtml');
                                            if (contentElement) {
                                                contentElement.style.color = 'rgb(28, 148, 137)';
                                                contentElement.style.fontSize = '14px';
                                                contentElement.style.fontStyle = 'normal';
                                                contentElement.style.fontVariant = 'normal';
                                                contentElement.style.fontWeight = '500';
                                                contentElement.style.fontFamily = 'UniviaPro';
                                            }
                                        }, 100);
                                    </script>";
            ClientScript.RegisterStartupScript(this.GetType(), "ApplyMsgBoxStyles", script);
            return;
        }

        protected void btnSubir_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                for (int i = 0; i < grdMembros.Items.Count; i++)
                {
                    ListaEmailsEnviarAta listaEmails = new ListaEmailsEnviarAta();
                    listaEmails.Inicialize();
                    listaEmails.IdReuniaoCipa = reuniaoCipa.Id;
                    Empregado empregado = new Empregado();
                    int id = Convert.ToInt32(grdMembros.Items[i].Key);
                    if (id != 0)
                    {
                        empregado.Find(id);
                        listaEmails.Nome = empregado.tNO_EMPG;
                        listaEmails.Email = empregado.teMail;
                        listaEmails.IdEmpregado = empregado.Id;
                    }
                    {
                        listaEmails.Nome = grdMembros.Items[i].Cells["Empregado"].Value.ToString();
                    }
                    listaEmails.Save();
                }
            }
            catch (ArgumentOutOfRangeException)
            {
                MsgBox1.Show("Ilitera.Net", "Nenhum item foi selecionado", null, new EO.Web.MsgBoxButton("OK"));
            }
            finally
            {
                PopulaGrdEmails();
                PopulaGrdMembros();
            }
            string script = @"
                                    <script type='text/javascript'>
                                        setTimeout(function() {
                                            var contentElement = document.getElementById('MsgBox1_msg_t');
                                            if (contentElement) {
                                                contentElement.style.color = 'rgb(125, 123, 123)';
                                                contentElement.style.fontSize = '14px';
                                                contentElement.style.fontStyle = 'normal';
                                                contentElement.style.fontVariant = 'normal';
                                                contentElement.style.fontWeight = '500';
                                                contentElement.style.fontFamily = 'UniviaPro';
                                            }
                                        }, 100); 
                                        setTimeout(function() {
                                            var contentElement = document.getElementById('MsgBox1_headerHtml');
                                            if (contentElement) {
                                                contentElement.style.color = 'rgb(28, 148, 137)';
                                                contentElement.style.fontSize = '14px';
                                                contentElement.style.fontStyle = 'normal';
                                                contentElement.style.fontVariant = 'normal';
                                                contentElement.style.fontWeight = '500';
                                                contentElement.style.fontFamily = 'UniviaPro';
                                            }
                                        }, 100);
                                    </script>";
            ClientScript.RegisterStartupScript(this.GetType(), "ApplyMsgBoxStyles", script);
        }

        protected void btnDescer_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                for (int i = 0; i < grdEmails.Items.Count; i++)
                {
                    ListaEmailsEnviarAta email = new ListaEmailsEnviarAta();
                    email.Find(Convert.ToInt32(grdEmails.Items[i].Key));
                    email.Delete();
                }
            }
            catch (ArgumentOutOfRangeException)
            {
                MsgBox1.Show("Ilitera.Net", "Nenhum item foi selecionado", null, new EO.Web.MsgBoxButton("OK"));
            }
            finally
            {
                PopulaGrdEmails();
                PopulaGrdMembros();
            }
            string script = @"
                                    <script type='text/javascript'>
                                        setTimeout(function() {
                                            var contentElement = document.getElementById('MsgBox1_msg_t');
                                            if (contentElement) {
                                                contentElement.style.color = 'rgb(125, 123, 123)';
                                                contentElement.style.fontSize = '14px';
                                                contentElement.style.fontStyle = 'normal';
                                                contentElement.style.fontVariant = 'normal';
                                                contentElement.style.fontWeight = '500';
                                                contentElement.style.fontFamily = 'UniviaPro';
                                            }
                                        }, 100); 
                                        setTimeout(function() {
                                            var contentElement = document.getElementById('MsgBox1_headerHtml');
                                            if (contentElement) {
                                                contentElement.style.color = 'rgb(28, 148, 137)';
                                                contentElement.style.fontSize = '14px';
                                                contentElement.style.fontStyle = 'normal';
                                                contentElement.style.fontVariant = 'normal';
                                                contentElement.style.fontWeight = '500';
                                                contentElement.style.fontFamily = 'UniviaPro';
                                            }
                                        }, 100);
                                    </script>";
            ClientScript.RegisterStartupScript(this.GetType(), "ApplyMsgBoxStyles", script);
        }

        protected void txtSaudacao_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string saudacao = txtSaudacao.Text;
                if (string.IsNullOrEmpty(saudacao.Trim()))
                {
                    throw new ArgumentNullException();
                }
            }
            catch (Exception)
            {
                MsgBox1.Show("Ilitera.Net", "Insira um texto válido", null, new EO.Web.MsgBoxButton("OK"));
            }
            string script = @"
                                    <script type='text/javascript'>
                                        setTimeout(function() {
                                            var contentElement = document.getElementById('MsgBox1_msg_t');
                                            if (contentElement) {
                                                contentElement.style.color = 'rgb(125, 123, 123)';
                                                contentElement.style.fontSize = '14px';
                                                contentElement.style.fontStyle = 'normal';
                                                contentElement.style.fontVariant = 'normal';
                                                contentElement.style.fontWeight = '500';
                                                contentElement.style.fontFamily = 'UniviaPro';
                                            }
                                        }, 100); 
                                        setTimeout(function() {
                                            var contentElement = document.getElementById('MsgBox1_headerHtml');
                                            if (contentElement) {
                                                contentElement.style.color = 'rgb(28, 148, 137)';
                                                contentElement.style.fontSize = '14px';
                                                contentElement.style.fontStyle = 'normal';
                                                contentElement.style.fontVariant = 'normal';
                                                contentElement.style.fontWeight = '500';
                                                contentElement.style.fontFamily = 'UniviaPro';
                                            }
                                        }, 100);
                                    </script>";
            ClientScript.RegisterStartupScript(this.GetType(), "ApplyMsgBoxStyles", script);
        }
    }
}