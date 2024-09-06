using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Ilitera.Opsa.Data;
using System.Text;


namespace Ilitera.Net.OrdemDeServico
{
	/// <summary>
	/// Summary description for CadRiscoAcidente.
	/// </summary>
    public partial class CadAspecto : System.Web.UI.Page
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
            //InicializaWebPageObjects();

            if (!IsPostBack)
            {

                Entities.Usuario user = (Entities.Usuario)Session["usuarioLogado"];

                int xIdEmpresa = System.Convert.ToInt32(Session["Empresa"]);
                int xIdUsuario = user.IdUsuario;

                lbl_Id_Empresa.Text = xIdEmpresa.ToString().Trim();
                lbl_Id_Usuario.Text = xIdUsuario.ToString().Trim();

                PopulaLsbAspecto();
                PopulaLsbImpacto();
                RegisterClienteCode();
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

        }
        #endregion

        private void RegisterClienteCode()
        {
            lsbImpactos.Attributes.Add("onClick", "javascript:setNomeImpacto(this.options[this.selectedIndex].text);");
            btnExcluirImpacto.Attributes.Add("onClick", "javascript:return confirm('Você deseja realmente excluir este Impacto Ambiental?');");
            btnExcluirAspecto.Attributes.Add("onClick", "javascript:return confirm('Você deseja realmente excluir este Aspecto Ambiental?');");
        }

        private void PopulaLsbImpacto()
        {
            lsbImpactos.DataSource = new ImpactoAmbiental().GetIdNome("Nome", "IdCliente=" + lbl_Id_Empresa.Text
                + " AND IdImpactoAmbiental NOT IN (SELECT IdImpactoAmbiental FROM AspectoImpacto WHERE IdAspectoAmbiental=" + lsbAspectos.SelectedValue + ")");
            lsbImpactos.DataValueField = "Id";
            lsbImpactos.DataTextField = "Nome";
            lsbImpactos.DataBind();

            lsbImpactos.Items.Insert(0, new ListItem("Cadastrar um Novo Impacto...", "0"));
            lsbImpactos.Items[0].Selected = true;
        }

        private void PopulaLsbAspecto()
        {
            lsbAspectos.DataSource = new AspectoAmbiental().GetIdNome("Nome", "IdCliente=" + lbl_Id_Empresa.Text);
            lsbAspectos.DataValueField = "Id";
            lsbAspectos.DataTextField = "Nome";
            lsbAspectos.DataBind();

            lsbAspectos.Items.Insert(0, new ListItem("Cadastrar um Novo Aspecto...", "0"));
            lsbAspectos.Items[0].Selected = true;
        }

        private void PopulaLsbAspectoImpacto()
        {
            lsbImpactoAspecto.DataSource = new ImpactoAmbiental().GetIdNome("Nome", "IdImpactoAmbiental IN (SELECT IdImpactoAmbiental FROM AspectoImpacto WHERE IdAspectoAmbiental=" + lsbAspectos.SelectedValue + ")");
            lsbImpactoAspecto.DataValueField = "Id";
            lsbImpactoAspecto.DataTextField = "Nome";
            lsbImpactoAspecto.DataBind();
        }

        protected void lsbAspectos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lsbAspectos.SelectedValue.Equals("0"))
            {
                txtNomeAspecto.Text = string.Empty;
                lsbImpactoAspecto.Items.Clear();
                btnExcluirAspecto.Enabled = false;
                PopulaLsbImpacto();
            }
            else
            {
                AspectoAmbiental aspecto = new AspectoAmbiental(Convert.ToInt32(lsbAspectos.SelectedValue));
                txtNomeAspecto.Text = aspecto.Nome;

                btnExcluirAspecto.Enabled = true;

                PopulaLsbAspectoImpacto();
                PopulaLsbImpacto();
            }

            txtNomeAspecto.Focus();
        }

        protected void btnGravarAspecto_Click(object sender, EventArgs e)
        {
            try
            {
                AspectoAmbiental aspecto = new AspectoAmbiental();
                string tipoProcess = string.Empty;

                if (lsbAspectos.SelectedValue.Equals("0"))
                {
                    Cliente xCliente;
                    xCliente = new Cliente();
                    xCliente.Inicialize();
                    xCliente.Find(System.Convert.ToInt32(lbl_Id_Empresa.Text));


                    tipoProcess = "cadastrado";
                    aspecto.Inicialize();
                    aspecto.IdCliente = xCliente;
                }
                else
                {
                    tipoProcess = "editado";
                    aspecto.Find(Convert.ToInt32(lsbAspectos.SelectedValue));
                }

                aspecto.Nome = txtNomeAspecto.Text.Trim();

                aspecto.UsuarioId = System.Convert.ToInt32( lbl_Id_Usuario.Text );
                aspecto.Save();

                PopulaLsbAspecto();
                lsbAspectos.ClearSelection();
                lsbAspectos.Items.FindByValue(aspecto.Id.ToString()).Selected = true;

                btnExcluirAspecto.Enabled = true;

                //StringBuilder st = new StringBuilder();

                //st.Append("window.opener.document.getElementById('txtAuxiliar').value='atualizaAspecto';");
                //st.Append("window.opener.document.forms[0].submit();");
                //st.Append("window.alert('O Aspecto Ambiental foi " + tipoProcess + " com sucesso!');");

                //this.ClientScript.RegisterStartupScript(this.GetType(), "GravarAspecto", st.ToString(), true);
            }
            catch (Exception ex)
            {
                MsgBox1.Show("Ilitera.Net", ex.Message, null,
                       new EO.Web.MsgBoxButton("OK"));

            }
        }

        protected void btnExcluirAspecto_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet dsOperacaoAspecto = new OperacaoAspectoAmbiental().Get("IdAspectoAmbiental=" + lsbAspectos.SelectedValue);
                if (dsOperacaoAspecto.Tables[0].Rows.Count > 0)
                    throw new Exception("Não é possível excluir este Aspecto Ambiental! Este Aspecto já está associado a uma ou mais Operações!");

                AspectoAmbiental aspecto = new AspectoAmbiental(Convert.ToInt32(lsbAspectos.SelectedValue));

                aspecto.UsuarioId = System.Convert.ToInt32( lbl_Id_Usuario.Text);
                aspecto.Delete();

                PopulaLsbAspecto();
                lsbImpactoAspecto.Items.Clear();
                PopulaLsbImpacto();

                txtNomeAspecto.Text = string.Empty;
                btnExcluirAspecto.Enabled = false;

                //StringBuilder st = new StringBuilder();

                //st.Append("window.opener.document.getElementById('txtAuxiliar').value='atualizaAspecto';");
                //st.Append("window.opener.document.forms[0].submit();");
                //st.Append("window.alert('O Aspecto Ambiental selecionado foi excluído com sucesso!');");

                //this.ClientScript.RegisterStartupScript(this.GetType(), "ExcluirAspecto", st.ToString(), true);
            }
            catch (Exception ex)
            {

                MsgBox1.Show("Ilitera.Net", ex.Message, null,
                       new EO.Web.MsgBoxButton("OK"));

                btnExcluirAspecto.Enabled = true;
            }
        }

        protected void imgRemoveImpacto_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (lsbImpactoAspecto.SelectedItem == null)
                    throw new Exception("Não há nenhum Impacto Ambiental selecionado para ser removido do Aspecto Ambiental!");

                bool checkProcess = false;

                foreach (ListItem itemImpacto in lsbImpactoAspecto.Items)
                {
                    if (itemImpacto.Selected)
                    {
                        AspectoImpacto aspectoImpacto = new AspectoImpacto();
                        aspectoImpacto.Find("IdAspectoAmbiental=" + lsbAspectos.SelectedValue
                            + " AND IdImpactoAmbiental=" + itemImpacto.Value);

                        if (!checkProcess)
                        {
                            checkProcess = true;
                            aspectoImpacto.UsuarioProcessoRealizado = "Desassociação do Impacto Ambiental ao Aspecto Ambiental";
                        }

                        aspectoImpacto.UsuarioId = System.Convert.ToInt32( lbl_Id_Usuario.Text);
                        aspectoImpacto.Delete();
                    }
                }

                PopulaLsbAspectoImpacto();
                PopulaLsbImpacto();


                MsgBox1.Show("Ilitera.Net", "Os Impactos Ambientais foram desassociados com sucesso do Aspecto selecionado!", null,
                       new EO.Web.MsgBoxButton("OK"));

            }
            catch (Exception ex)
            {
                
                MsgBox1.Show("Ilitera.Net", ex.Message, null,
                       new EO.Web.MsgBoxButton("OK"));

            }
        }

        protected void imbAddImpacto_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (lsbAspectos.SelectedValue.Equals("0"))
                    throw new Exception("É necessário selecionar um Aspecto Ambiental antes de associar um Impacto Ambiental!");
                if (lsbImpactos.SelectedValue.Equals("0"))
                    throw new Exception("É necessário selecionar um Impacto Ambiental para ser associado ao Aspecto Ambiental!");

                foreach (ListItem itemImpacto in lsbImpactos.Items)
                {
                    if (itemImpacto.Selected)
                    {
                        AspectoImpacto aspectoImpacto = new AspectoImpacto();
                        aspectoImpacto.Inicialize();

                        aspectoImpacto.IdAspectoAmbiental.Id = Convert.ToInt32(lsbAspectos.SelectedValue);
                        aspectoImpacto.IdImpactoAmbiental.Id = Convert.ToInt32(lsbImpactos.SelectedValue);

                        aspectoImpacto.UsuarioProcessoRealizado = "Associação do Impacto Ambiental ao Aspecto Ambiental";
                        aspectoImpacto.UsuarioId = System.Convert.ToInt32( lbl_Id_Usuario.Text);
                        aspectoImpacto.Save();
                    }
                }

                PopulaLsbAspectoImpacto();
                PopulaLsbImpacto();

                txtNomeImpacto.Text = string.Empty;


                MsgBox1.Show("Ilitera.Net", "O Impacto Ambiental foi associado com sucesso ao Aspecto Ambiental selecionado!", null,
                       new EO.Web.MsgBoxButton("OK"));

            }
            catch (Exception ex)
            {
                
                MsgBox1.Show("Ilitera.Net", ex.Message, null,
                       new EO.Web.MsgBoxButton("OK"));

            }
        }

        protected void btnGravarImpacto_Click(object sender, EventArgs e)
        {
            try
            {
                ImpactoAmbiental impacto = new ImpactoAmbiental();
                string tipoProcess = string.Empty;

                if (lsbImpactos.SelectedValue.Equals("0"))
                {

                    Cliente xCliente;
                    xCliente = new Cliente();
                    xCliente.Inicialize();
                    xCliente.Find(System.Convert.ToInt32(lbl_Id_Empresa.Text));

                    tipoProcess = "cadastrado";
                    impacto.Inicialize();
                    impacto.IdCliente = xCliente;
                }
                else
                {
                    tipoProcess = "editado";
                    impacto.Find(Convert.ToInt32(lsbImpactos.SelectedValue));
                }

                impacto.Nome = txtNomeImpacto.Text.Trim();

                impacto.UsuarioId = System.Convert.ToInt32( lbl_Id_Usuario.Text);
                impacto.Save();

                PopulaLsbImpacto();
                lsbImpactos.ClearSelection();
                lsbImpactos.Items.FindByValue(impacto.Id.ToString()).Selected = true;

                btnExcluirImpacto.Enabled = true;


                MsgBox1.Show("Ilitera.Net", "O Impacto Ambiental foi " + tipoProcess + " com sucesso!", null,
                       new EO.Web.MsgBoxButton("OK"));

            }
            catch (Exception ex)
            {
                MsgBox1.Show("Ilitera.Net", ex.Message, null,
                        new EO.Web.MsgBoxButton("OK"));

            }
        }

        protected void btnExcluirImpacto_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet dsAspectoImpacto = new AspectoImpacto().Get("IdImpactoAmbiental=" + lsbImpactos.SelectedValue);
                if (dsAspectoImpacto.Tables[0].Rows.Count > 0)
                    throw new Exception("Não é possível excluir este Impacto Ambiental! O Impacto já está associado a um ou mais Aspectos Ambientais!");

                ImpactoAmbiental impacto = new ImpactoAmbiental(Convert.ToInt32(lsbImpactos.SelectedValue));

                impacto.UsuarioId = System.Convert.ToInt32(lbl_Id_Usuario.Text);
                impacto.Delete();

                PopulaLsbImpacto();

                txtNomeImpacto.Text = string.Empty;
                btnExcluirImpacto.Enabled = false;


                MsgBox1.Show("Ilitera.Net", "O Impacto Ambiental selecionado foi excluído com sucesso!", null,
                       new EO.Web.MsgBoxButton("OK"));

            }
            catch (Exception ex)
            {
                MsgBox1.Show("Ilitera.Net", ex.Message, null,
                       new EO.Web.MsgBoxButton("OK"));

                btnExcluirImpacto.Enabled = true;
            }
        }
    }
}