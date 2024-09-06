using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ilitera.Opsa.Data;
using Ilitera.Common;
using System.Data;

namespace Ilitera.Net
{
    public partial class PresentesReuniao : System.Web.UI.Page
    {
        private Cliente cliente = new Cliente();
        private Cipa cipa = new Cipa();
        private Juridica juridica = new Juridica();
        ReuniaoCipa reuniaoCipa;
        EventoCipa eventoCipa = new EventoCipa();
       
        protected void Page_Load(object sender, EventArgs e)
        {
            cliente.Find(Convert.ToInt32(Session["Empresa"]));
            juridica = cliente;
            cipa = cliente.GetGestaoAtual();
            int idReuniao = 0;
            if (Request["r"] != null)
            {
                idReuniao = Convert.ToInt32(Request["r"]);
            }
            reuniaoCipa = new ReuniaoCipa();
            reuniaoCipa.Find(idReuniao);

            if (!IsPostBack)
            {
                PopulaGrdMembroCipa();
                PopularGridReuniaoPresenca();
            }
        }

        private void PopulaGrdMembroCipa()
        {
            grdMembrosCipa.DataSource = null;
            grdMembrosCipa.DataBind();
            StringBuilder str = new StringBuilder();

            str.Append("IdCipa=" + cipa.Id);
            str.Append(" AND IdMembroCipa NOT IN ");
            str.Append("(SELECT IdMembroCipa FROM ReuniaoPresencaCipa");
            str.Append(" WHERE IdReuniaoCipa =" + reuniaoCipa.Id + ")");
            str.Append(" AND IndStatus=" + ((int)MembroCipa.Status.Ativo).ToString());

            if (Convert.ToInt32(rbMembrosCipa.SelectedValue) == 1)
                str.Append(" AND IndTitularSuplente=" + ((int)TitularSuplente.Titular).ToString());
            else if (Convert.ToInt32(rbMembrosCipa.SelectedValue) == 2)
                str.Append(" AND IndTitularSuplente=" + ((int)TitularSuplente.Suplente).ToString());
            else if (Convert.ToInt32(rbMembrosCipa.SelectedValue) == 3)
                str.Append(" AND IndGrupoMembro=" + ((int)GrupoMembro.Outros).ToString());

            str.Append(" ORDER BY IndGrupoMembro DESC, Numero");

            DataSet membrosCipa = new MembroCipa().Get(str.ToString());
            DataTable zDt = new DataTable();
            zDt.Columns.Add("IdEmprcImaegado");
            zDt.Columns.Add("Editar");
            zDt.Columns.Add("Grupo");
            zDt.Columns.Add("Cargo");
            zDt.Columns.Add("Nome");

            if (membrosCipa.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in membrosCipa.Tables[0].Rows)
                {
                   
                    if (row["IdEmpregado"] != DBNull.Value)
                    {
                        Empregado empregado = new Empregado();
                        empregado.Find(Convert.ToInt32(row["IdEmpregado"]));
                        MembroCipa membroCipa = new MembroCipa();
                        membroCipa.Find(Convert.ToInt32(row["IdMembroCipa"]));
                        string cargoCipa = membroCipa.GetNomeCargo();
                        string grupoMembroParaReuniao = membroCipa.GetGrupoMembroParaReuniao();
                        string grupo = grupoMembroParaReuniao.Substring(grupoMembroParaReuniao.IndexOf('(') + 1, grupoMembroParaReuniao.Length - (2 + grupoMembroParaReuniao.IndexOf('(')));
                        zDt.Rows.Add(membroCipa.Id, "Editar", grupo, cargoCipa, empregado.tNO_EMPG);
                    }
                    else
                    {
                        MembroCipa membroCipa = new MembroCipa();
                        membroCipa.Find(Convert.ToInt32(row["IdMembroCipa"]));
                        string cargoCipa = membroCipa.GetNomeCargo();
                        zDt.Rows.Add(membroCipa.Id, "Editar", membroCipa.GetGrupoMembro(), cargoCipa, membroCipa.NomeMembro);
                    }
                }

                DataSet zDs = new DataSet();
                zDs.Tables.Add(zDt);
                grdMembrosCipa.DataSource = zDs;
                grdMembrosCipa.DataBind();
            }else
            {
                grdMembrosCipa.DataSource = null;
                grdMembrosCipa.DataBind();
                grdMembrosCipa.Items.Clear();
            }
        }

        protected void rbMembrosCipa_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(rbMembrosCipa.SelectedValue) == 3)
                btnCadastrarOutros.Enabled = true;
            PopulaGrdMembroCipa();
            if (grdMembrosCipa.DataSource == null)
            {
                MsgBox1.Show("Ilitera.Net", "Não há resultado", null, new EO.Web.MsgBoxButton("OK"));
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

        private void PopularGridReuniaoPresenca()
        {
            grdMembrosCipa2.DataSource = null;
            grdMembrosCipa2.DataBind();
            DataSet PresencaCipa = new ReuniaoPresencaCipa().Get("IdReuniaoCipa=" + reuniaoCipa.Id
                + " ORDER BY (SELECT IndGrupoMembro FROM MembroCipa WHERE MembroCipa.IdMembroCipa=ReuniaoPresencaCipa.IdMembroCipa) DESC,"
                + "(SELECT Numero FROM MembroCipa WHERE MembroCipa.IdMembroCipa=ReuniaoPresencaCipa.IdMembroCipa)");


            DataTable zDt = new DataTable();
            zDt.Columns.Add("IdReuniaoPresencaCipa");
            zDt.Columns.Add("Nome2");
            zDt.Columns.Add("Cargo2");
            zDt.Columns.Add("Presenca");

            if (PresencaCipa.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in PresencaCipa.Tables[0].Rows)
                {
                    MembroCipa membroCipa = new MembroCipa();
                    membroCipa.Find(Convert.ToInt32(row["IdMembroCipa"]));
                    if (membroCipa.NomeMembro == "")
                    {
                        Empregado empregado = new Empregado();
                        empregado.Find(membroCipa.IdEmpregado.Id);
                       
                        string cargoCipa = membroCipa.GetNomeCargo();
                        string grupoMembroParaReuniao = membroCipa.GetGrupoMembroParaReuniao();
                        string grupo = grupoMembroParaReuniao.Substring(grupoMembroParaReuniao.IndexOf('(') + 1, grupoMembroParaReuniao.Length - (2 + grupoMembroParaReuniao.IndexOf('(')));
                        zDt.Rows.Add(Convert.ToInt32(row.ItemArray[0]), empregado.tNO_EMPG, cargoCipa, ReuniaoPresencaCipa.GetPresenca(Convert.ToInt32(row.ItemArray[3])));
                    }
                    else
                    {
                        string cargoCipa = membroCipa.GetNomeCargo();
                        zDt.Rows.Add(Convert.ToInt32(row.ItemArray[0]), membroCipa.NomeMembro, cargoCipa, ReuniaoPresencaCipa.GetPresenca(Convert.ToInt32(row.ItemArray[3])));
                    }
                }

                DataSet zDs = new DataSet();
                zDs.Tables.Add(zDt);
                grdMembrosCipa2.DataSource = zDs;
                grdMembrosCipa2.DataBind();
            }
            else
            {
                grdMembrosCipa2.DataSource = null;
                grdMembrosCipa2.DataBind();
                grdMembrosCipa2.Items.Clear();
            }
        }

        protected void grdMembrosCipa_ItemCommand(object sender, EO.Web.GridCommandEventArgs e)
        {
            if (e.CommandName == "0")
            {
                string parametros = "?i=" + e.Item.Key.ToString();
                StringBuilder st = new StringBuilder();
                st.AppendFormat("void(window.open('EditMembroCIPA.aspx" + parametros + "', 'EditarMembroCipa', 'scrollbars=yes, resizable=yes, toolbar=no, addressbar=no, menubar=no, width=825px, height=350px')); window.close();");

                System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), " ", st.ToString(), true);
            }
        }

        protected void btnSubir_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                for (int i = 0; i < grdMembrosCipa.Items.Count; i++)
                {
                    ReuniaoPresencaCipa reuniaoPresencaCipa = new ReuniaoPresencaCipa();
                    reuniaoPresencaCipa.Inicialize();
                    reuniaoPresencaCipa.IdMembroCipa.Id = Convert.ToInt32(grdMembrosCipa.Items[i].Key);
                    reuniaoPresencaCipa.IdReuniaoCipa = reuniaoCipa;
                    reuniaoPresencaCipa.Save();

                    MembroCipa membro = new MembroCipa();
                    membro.Find(reuniaoPresencaCipa.IdMembroCipa.Id);

                    ListaEmailsEnviarAta listaEmails = new ListaEmailsEnviarAta();
                    listaEmails.Inicialize();
                    listaEmails.IdReuniaoCipa = reuniaoCipa.Id;
                    listaEmails.IdMembroCipa = membro.Id;
                    if (membro.IdEmpregado.Id != 0)
                    {
                        Empregado empregado = new Empregado();
                        empregado.Find(membro.IdEmpregado.Id);

                        listaEmails.Nome = empregado.tNO_EMPG;
                        listaEmails.Email = empregado.teMail;
                    }
                    else
                    {
                        listaEmails.Nome = membro.NomeMembro;
                    }
                    listaEmails.Save();
                }
            }
            catch (Exception ex)
            {
                MsgBox1.Show("Ilitera.Net", ex.Message, null, new EO.Web.MsgBoxButton("OK"));
            }finally
            {
                PopulaGrdMembroCipa();
                PopularGridReuniaoPresenca();
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
                ReuniaoPresencaCipa reuniaoPresencaCipa = new ReuniaoPresencaCipa();
                reuniaoPresencaCipa.Inicialize();
                reuniaoPresencaCipa.IdMembroCipa.Id = Convert.ToInt32(grdMembrosCipa.Items[grdMembrosCipa.SelectedItemIndex].Key);
                reuniaoPresencaCipa.IdReuniaoCipa = reuniaoCipa;
                reuniaoPresencaCipa.Save();

                MembroCipa membro = new MembroCipa();
                membro.Find(reuniaoPresencaCipa.IdMembroCipa.Id);

                ListaEmailsEnviarAta listaEmails = new ListaEmailsEnviarAta();
                listaEmails.Inicialize();
                listaEmails.IdReuniaoCipa = reuniaoCipa.Id;
                listaEmails.IdMembroCipa = membro.Id;
                if (membro.IdEmpregado.Id != 0)
                {
                    Empregado empregado = new Empregado();
                    empregado.Find(membro.IdEmpregado.Id);

                    listaEmails.Nome = empregado.tNO_EMPG;
                    listaEmails.Email = empregado.teMail;
                    listaEmails.IdEmpregado = empregado.Id;
                }else
                {
                    listaEmails.Nome = membro.NomeMembro;
                }
                listaEmails.Save();

            }
            catch (ArgumentOutOfRangeException)
            {
                MsgBox1.Show("Ilitera.Net", "Nenhum item foi selecionado", null, new EO.Web.MsgBoxButton("OK"));
            }
            catch (Exception ex)
            {
                MsgBox1.Show("Ilitera.Net", ex.Message, null, new EO.Web.MsgBoxButton("OK"));
            }
            finally
            {
                PopulaGrdMembroCipa();
                PopularGridReuniaoPresenca();
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

        protected void btnRemover_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                ReuniaoPresencaCipa reuniaoPresencaCipa = new ReuniaoPresencaCipa();
                reuniaoPresencaCipa.Find("IdReuniaoPresencaCipa=" + Convert.ToInt32(grdMembrosCipa2.Items[grdMembrosCipa2.SelectedItemIndex].Key));
                ListaEmailsEnviarAta listaEmails = new ListaEmailsEnviarAta();
                listaEmails.Find("IdMembroCipa =" + reuniaoPresencaCipa.IdMembroCipa.Id);

                reuniaoPresencaCipa.Delete();
                listaEmails.Delete();
            }
            catch (ArgumentOutOfRangeException)
            {
                MsgBox1.Show("Ilitera.Net", "Nenhum item foi selecionado", null, new EO.Web.MsgBoxButton("OK"));
            }
            catch (Exception ex)
            {
                MsgBox1.Show("Ilitera.Net", ex.Message, null, new EO.Web.MsgBoxButton("OK"));
            }finally
            {
                PopulaGrdMembroCipa();
                PopularGridReuniaoPresenca();
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
                for (int i = 0; i < grdMembrosCipa2.Items.Count; i++)
                {
                    ReuniaoPresencaCipa reuniaoPresencaCipa = new ReuniaoPresencaCipa();
                    reuniaoPresencaCipa.Find("IdReuniaoPresencaCipa=" + Convert.ToInt32(grdMembrosCipa2.Items[i].Key));
                    ListaEmailsEnviarAta listaEmails = new ListaEmailsEnviarAta();
                    listaEmails.Find("IdMembroCipa =" + reuniaoPresencaCipa.IdMembroCipa.Id);

                    reuniaoPresencaCipa.Delete();
                    listaEmails.Delete();
                }
            }
            catch (Exception ex)
            {
                MsgBox1.Show("Ilitera.Net", ex.Message, null, new EO.Web.MsgBoxButton("OK"));
            }
            finally
            {
                PopulaGrdMembroCipa();
                PopularGridReuniaoPresenca();
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

        protected void btnCadastrarOutros_Click(object sender, EventArgs e)
        {
            StringBuilder st = new StringBuilder();
            st.AppendFormat("void(window.open('OutrosParticipantes.aspx', 'OutrosParticipantes','scrollbars=yes, resizable=yes, toolbar=no, addressbar=no, menubar=no, width=905px, height=600px')); window.close();");

            System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), " ", st.ToString(), true);
        }
    }
}