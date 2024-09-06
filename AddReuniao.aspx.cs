using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Ilitera.Common;
using Ilitera.Opsa.Data;
using Ilitera.Opsa.Report;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace Ilitera.Net
{
    public partial class AddReuniao : System.Web.UI.Page
    {
        private Cliente cliente = new Cliente();
        private Cipa cipa = new Cipa();
        private Juridica juridica = new Juridica();
        private ReuniaoCipa reuniaoCipa;
        private List<Ata> listAtas = new List<Ata>();
        private EventoCipa eventoCipa = new EventoCipa();
        private FieldInfo reuniaoAtributo;

        protected void Page_Load(object sender, EventArgs e)
        {
            cliente.Find(Convert.ToInt32(Session["Empresa"]));
            juridica = cliente;
            cipa = cliente.GetGestaoAtual();
            int numeroReuniao = 0;
            if (Request["r"] != null)
            {
                numeroReuniao = Convert.ToInt32(Request["r"]);
                EventoBase eventoBase = (EventoBase)Enum.Parse(typeof(EventoBase), numeroReuniao.ToString());
                eventoCipa = EventoCipa.GetEventoCipa(cipa, eventoBase);
                if (eventoCipa.GetType() == typeof(ReuniaoCipa))
                {
                    reuniaoCipa = (ReuniaoCipa)eventoCipa;
                }
                else
                {
                    reuniaoCipa = new ReuniaoCipa();
                    reuniaoCipa.Find(eventoCipa.Id);
                }
                reuniaoAtributo = ObterReuniaoAtributo(cipa, numeroReuniao - 12);

            }
            else
            {
                if (Request["i"] != null)
                {
                    reuniaoCipa = new ReuniaoCipa();
                    reuniaoCipa.Find(Convert.ToInt32(Request["i"]));
                }
                else
                {
                    reuniaoCipa = new ReuniaoCipa();
                    reuniaoCipa.Inicialize();
                    reuniaoCipa.FraseAcidentes = "";
                    reuniaoCipa.HoraInicio = "";
                    reuniaoCipa.IdCipa = cipa;
                    reuniaoCipa.IdEventoBaseCipa.Id = (int)EventoBase.ReuniaoExtraordinaria;
                    reuniaoCipa.DataSolicitacao = DateTime.Today;
                    reuniaoCipa.Save();
                    Response.Redirect("AddReuniao.aspx?i=" + reuniaoCipa.Id);
                }
            }
            
            if (!IsPostBack)
            {
                lblAddReuniao.Text = PopularLegendaForm();
                cliente.IdCNAE.Find();
                cliente.IdSindicato.Find();
                cliente.IdCNAE.IdGrupoCipa.Find();
                txtSindicato.Text = cliente.IdSindicato.NomeAbreviado;
                txtCNAE.Text = cliente.IdCNAE.Codigo;
                txtGrauRisco.Text = cliente.IdCNAE.GrauRisco.ToString();

                if (reuniaoCipa.Id != 0)
                {
                    txtData.Text = reuniaoCipa.DataSolicitacao.ToString("dd/MM/yyyy");
                    txtHorario.Text = reuniaoCipa.HoraInicio;
                    txtLocalReuniao.Text = reuniaoCipa.Local;
                    txtFraseAcidente.Text = reuniaoCipa.FraseAcidentes;
                    txtFraseAcidente2.Items.Clear();
                    listAtas.Clear();
                    txtFraseAcidente2.Items.Clear();
                    txtAssuntosDiscutidos.Text = reuniaoCipa.AssuntosDiscutidos;
                    txtObservacoes.Text = reuniaoCipa.Observacao;

                    ArrayList arrayAtas = new Ata().Find("IdReuniaoCipa=" + reuniaoCipa.Id + "ORDER BY NumOrdem");
                    foreach (Ata ata in arrayAtas)
                    {
                        listAtas.Add(ata);
                    }
                    listAtas.Reverse();
                    foreach (Ata ata in listAtas)
                    {
                        string texto = ata.Texto;
                        string valor = ata.Id.ToString();
                        ListItem item = new ListItem(texto, valor);
                        txtFraseAcidente2.Items.Add(item);
                    }
                    PopulaGridAcidentes();
                }
                else
                {
                    reuniaoCipa.FraseAcidentes = "";
                    reuniaoCipa.HoraInicio = "";
                }
            }

            
        }

        private void PopulaGridAcidentes()
        {
            try
            {
                grd_Acidentes_Cadastrados.DataSource = new CAT().Get(
                    "IdEmpregado IN (SELECT nID_EMPREGADO FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEMPREGADO WHERE nID_EMPR=" + cliente.Id + ")"
                    + " AND MONTH(DataEmissao)=MONTH('" + reuniaoCipa.DataSolicitacao.ToString("yyyy-MM-dd") + "')"
                    + " AND YEAR(DataEmissao)=YEAR('" + reuniaoCipa.DataSolicitacao.ToString("yyyy-MM-dd") + "')");
            }
            catch (Exception ex)
            {

            }
        }

        private string PopularLegendaForm()
        {
            string ret = string.Empty;

            switch (reuniaoCipa.IdEventoBaseCipa.Id)
            {
                case (int)EventoBase.Reuniao1:
                    ret = "1º Reunião";
                    break;
                case (int)EventoBase.Reuniao2:
                    ret = "2º Reunião";
                    break;
                case (int)EventoBase.Reuniao3:
                    ret = "3º Reunião";
                    break;
                case (int)EventoBase.Reuniao4:
                    ret = "4º Reunião";
                    break;
                case (int)EventoBase.Reuniao5:
                    ret = "5º Reunião";
                    break;
                case (int)EventoBase.Reuniao6:
                    ret = "6º Reunião";
                    break;
                case (int)EventoBase.Reuniao7:
                    ret = "7º Reunião";
                    break;
                case (int)EventoBase.Reuniao8:
                    ret = "8º Reunião";
                    break;
                case (int)EventoBase.Reuniao9:
                    ret = "9º Reunião";
                    break;
                case (int)EventoBase.Reuniao10:
                    ret = "10º Reunião";
                    break;
                case (int)EventoBase.Reuniao11:
                    ret = "11º Reunião";
                    break;
                case (int)EventoBase.Reuniao12:
                    ret = "12º Reunião";
                    break;
                case (int)EventoBase.ReuniaoExtraordinaria:
                    ret = "Reunião Extraordinária";
                    break;
            }
            return ret;
        }

        private FieldInfo ObterReuniaoAtributo(Cipa cipa, int numeroReuniao)
        {
            string nomeAtributo = $"_Reuniao{numeroReuniao}";
            var tipo = cipa.GetType();
            return tipo.GetField(nomeAtributo, BindingFlags.NonPublic | BindingFlags.Instance);
        }

        protected void btnSalvar_Click(object sender, ImageClickEventArgs e)
        {
            listAtas.Clear();
            string text = txtEditarFrase.Text;
            if(text.Trim() == "")
            {
                MsgBox1.Show("Ilitera.Net", "Não é possível enviar um texto vazio", null, new EO.Web.MsgBoxButton("OK"));
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
            Ata editAta = new Ata();
            editAta.Inicialize();
            ArrayList arrayAtas = new Ata().Find("IdReuniaoCipa=" + reuniaoCipa.Id + "ORDER BY NumOrdem");
            foreach (Ata ata in arrayAtas)
            {
                listAtas.Add(ata);
            }
            listAtas.Reverse();
            int id = 0;
            if (int.TryParse(hdnDataId.Value, out id)) { 
                editAta.Id = id;
                editAta.IdFrase.Id = listAtas.Find(x => x.Id == id).IdFrase.Id;
            }
            editAta.IdReuniaoCipa = reuniaoCipa;
            editAta.Texto = text;
        
            if (txtFraseAcidente2.Items.Count > 0)
            {
                editAta.NumOrdem = listAtas[listAtas.Count - 1].NumOrdem + 1;
            }
            else
            {
                editAta.NumOrdem = 1;
            }

            editAta.Save();
            txtFraseAcidente2.Items.Clear();
            listAtas.Clear();
            txtEditarFrase.Text = "";
            txtEditarFrase.Attributes.Remove("data-id");
            hdnDataId.Value = "";
            arrayAtas = new Ata().Find("IdReuniaoCipa=" + reuniaoCipa.Id + "ORDER BY NumOrdem");
            foreach (Ata ata in arrayAtas)
            {
                listAtas.Add(ata);
            }
            listAtas.Reverse();
            foreach (Ata ata in listAtas)
            {
                string texto = ata.Texto;
                string valor = ata.Id.ToString();
                ListItem item = new ListItem(texto, valor);
                txtFraseAcidente2.Items.Add(item);
            }
            
        }

        protected void txtEditarFrase_TextChanged(object sender, EventArgs e)
        {
            if (txtEditarFrase.Text == "")
            {
                txtEditarFrase.Attributes.Remove("data-id");
            }
        }

        protected void btnDeletar_Click(object sender, ImageClickEventArgs e)
        {
            if (txtFraseAcidente2.SelectedItem != null)
            {
                try
                {
                    string id = txtFraseAcidente2.SelectedValue;
                    ArrayList arrayAtas = new Ata().Find("IdReuniaoCipa=" + reuniaoCipa.Id + "ORDER BY NumOrdem");
                    arrayAtas.Reverse();
                    foreach (Ata ata in arrayAtas)
                    {
                        if (ata.Id == Convert.ToInt32(id))
                        {
                            ata.Delete();
                            txtEditarFrase.Text = "";
                            txtEditarFrase.Attributes.Remove("data-id");
                            txtFraseAcidente2.Items.Clear();
                            listAtas.Clear();
                        }
                        else
                        {
                            string texto = ata.Texto;
                            string valor = ata.Id.ToString();
                            ListItem item = new ListItem(texto, valor);
                            txtFraseAcidente2.Items.Add(item);
                        }
                    }
                }
                catch (ArgumentOutOfRangeException)
                {
                    MsgBox1.Show("Ilitera.Net", "Nenhum item foi selecionado", null, new EO.Web.MsgBoxButton("OK"));
                }
                catch (Exception ex)
                {
                    MsgBox1.Show("Ilitera.Net", ex.Message, null, new EO.Web.MsgBoxButton("OK"));
                }
            }else
            {
                MsgBox1.Show("Ilitera.Net", "Nenhum item foi selecionado", null, new EO.Web.MsgBoxButton("OK"));
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

        protected void btnIndicacao_Click(object sender, EventArgs e)
        {
            try
            {
                Salvar();
            }
            catch (FormatException)
            {
                MsgBox1.Show("Data inválida", "Insira uma data válida para a reunião", null, new EO.Web.MsgBoxButton("OK"));
                return;
            }

            string st = "void(window.open('PresentesReuniao.aspx?r=" + reuniaoCipa.Id + "', 'PresentesReuniao', 'scrollbars=yes, resizable=yes, toolbar=no, addressbar=no, menubar=no, width=1250px, height=1100px'));";

            System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), " ", st.ToString(), true);
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

        protected void txtData_TextChanged(object sender, EventArgs e)
        {
            string script = "";
            try
            {
                DateTime dataCalendario = DateTime.ParseExact(txtData.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                if (Ilitera.Common.Feriado.IsFinalSemana(dataCalendario))
                {
                    MsgBox1.Show("Ilitera.Net", "Final de Semana!", null, new EO.Web.MsgBoxButton("OK"));
                    script = @"
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
                    txtData.Text = reuniaoCipa.DataSolicitacao.ToString("dd/MM/yyyy");
                    return;
                }
                reuniaoAtributo.SetValue(cipa, dataCalendario);
                reuniaoCipa.DataSolicitacao = dataCalendario;
                reuniaoCipa.Save();
                cipa.Save();
            }
            catch (Exception ex)
            {
                MsgBox1.Show("Ilitera.Net", "Data inválida", null, new EO.Web.MsgBoxButton("OK"));
                txtData.Text = reuniaoCipa.DataSolicitacao.ToString("dd/MM/yyyy");
            }
            script = @"
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

        protected void txtHorario_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DateTime horario = DateTime.ParseExact(txtHorario.Text, "HH:mm", System.Globalization.CultureInfo.InvariantCulture);
                eventoCipa.HoraInicio = horario.ToString("HH:mm");
                reuniaoCipa.HoraInicio = horario.ToString("HH:mm");
                reuniaoCipa.Save();
            }
            catch (Exception)
            {
                MsgBox1.Show("Ilitera.Net", "Horário inválido", null, new EO.Web.MsgBoxButton("OK"));
                txtHorario.Text = reuniaoCipa.HoraInicio;
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

        protected void txtLocalReuniao_TextChanged(object sender, EventArgs e)
        {
            reuniaoCipa.Local = txtLocalReuniao.Text;
            reuniaoCipa.Save();
        }

        protected void txtFraseAcidente_TextChanged(object sender, EventArgs e)
        {
            reuniaoCipa.FraseAcidentes = txtFraseAcidente.Text;
            reuniaoCipa.Save();
        }

        protected void txtAssuntosDiscutidos_TextChanged(object sender, EventArgs e)
        {
            reuniaoCipa.AssuntosDiscutidos = txtAssuntosDiscutidos.Text;
            reuniaoCipa.Save();
        }

        protected void txtObservacoes_TextChanged(object sender, EventArgs e)
        {
            reuniaoCipa.Observacao = txtObservacoes.Text;
            reuniaoCipa.Save();
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            StringBuilder st = new StringBuilder();
            st.AppendFormat("window.close();");

            System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), " ", st.ToString(), true);
        }

        protected void btnExcluir_Click(object sender, EventArgs e)
        {
            try
            {
                eventoCipa.Delete();
                reuniaoCipa.Delete();
                StringBuilder st = new StringBuilder();
                st.AppendFormat("window.close();");
                System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), " ", st.ToString(), true);
            }
            catch (Exception ex)
            {
                MsgBox1.Show("Ilitera.Net", ex.Message, null, new EO.Web.MsgBoxButton("OK"));
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

        protected void btnConfirmar_Click(object sender, EventArgs e)
        {
            Salvar();
        }

        private void Salvar()
        {
            try
            {
                if (Ilitera.Common.Feriado.IsFinalSemana(Convert.ToDateTime(txtData.Text)))
                {
                    MsgBox1.Show("Ilitera.Net", "Final de Semana!", null, new EO.Web.MsgBoxButton("OK"));
                    return;
                }
            

            if (!String.IsNullOrEmpty(txtData.Text))
                reuniaoCipa.DataSolicitacao = Convert.ToDateTime(txtData.Text);
            else
                reuniaoCipa.DataSolicitacao = new DateTime();
            }
            catch (Exception ex)
            {
                throw new FormatException(ex.Message, ex);
            }

            reuniaoCipa.Save();
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

        private ControlePedido GetControleEnviarEmailOuImpressaoDocumento()
        {
            ControlePedido cp = new ControlePedido();
            cp.Find("IdControle=" + (int)Controles.EnviandoEMAILouImpressaoDocumento
                    + " AND IdPedido=" + reuniaoCipa.Id);
            cp.Inicio = DateTime.Now;
            return cp;
        }

        protected static void CreatePDFDocument(ReportClass report, HttpResponse response, string DocumentName)
        {
            CreatePDFDocument(report, response, false, DocumentName);
        }

        protected static void CreatePDFDocument(ReportClass report, HttpResponse response, bool forceDownload, string DownloadfileName)
        {
            using (Stream pdfStream = report.ExportToStream(ExportFormatType.PortableDocFormat))
            {
                // Copy the content to a MemoryStream
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    byte[] buffer = new byte[4096];
                    int bytesRead;
                    while ((bytesRead = pdfStream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        memoryStream.Write(buffer, 0, bytesRead);
                    }

                    // Ensure the memory stream position is set to the beginning
                    memoryStream.Position = 0;

                    // Configure the HTTP response
                    response.Clear();
                    response.Buffer = true;
                    response.ContentType = "application/pdf";
                    response.AddHeader("Content-Disposition", "initial; filename=" + DownloadfileName + ".pdf");
                    response.BinaryWrite(memoryStream.ToArray());
                    response.End();
                }
            }
            //report.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, response, false, null);
        }

        protected void btnEnviarAta_Click(object sender, EventArgs e)
        {
            try
            {
                Salvar();
            }
            catch (FormatException)
            {
                MsgBox1.Show("Ilitera.Net", "Insira uma data válida para a reunião", null, new EO.Web.MsgBoxButton("OK"));
                return;
            }

            try
            {
                StringBuilder st = new StringBuilder();
                st.AppendFormat("void(window.open('EnviarAta.aspx?r=" + reuniaoCipa.Id + "', 'EnviarAta','scrollbars=yes, resizable=yes, toolbar=no, addressbar=no, menubar=no, width=1100px, height=1200px'));");

                System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), " ", st.ToString(), true);
            }
            catch (Exception ex)
            {
                MsgBox1.Show("Erro", ex.Message, null, new EO.Web.MsgBoxButton("OK"));
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

        protected void btn_imprimir_Click(object sender, EventArgs e)
        {
            try
            {
                Salvar();
            }
            catch(FormatException)
            {
                MsgBox1.Show("Ilitera.Net", "Insira uma data válida para a reunião", null, new EO.Web.MsgBoxButton("OK"));
                return;
            }

            try
            {
                ControlePedido contropePedido = GetControleEnviarEmailOuImpressaoDocumento();

                RptAtaReuniaoCipa report = new DataSourceAtaReuniaoCIPA(reuniaoCipa).GetReport();

                CreatePDFDocument(report, this.Response, "CIPA - Ata da " + PopularLegendaForm());
            }
            catch (Exception ex)
            {
                MsgBox1.Show("Ilitera.Net", ex.Message, null, new EO.Web.MsgBoxButton("OK"));
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