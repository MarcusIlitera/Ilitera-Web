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


namespace Ilitera.Net
{
	/// <summary>
	/// Summary description for CadRiscoAcidente.
	/// </summary>
    public partial class CadPerigo : System.Web.UI.Page
    {

        protected Ilitera.Common.Usuario usuario = new Ilitera.Common.Usuario();

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

                PopulaLsbPerigo();
                PopulaLsbRiscoAcidente();
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
            lsbRisco.Attributes.Add("onClick", "javascript:setNomeRisco(this.options[this.selectedIndex].text);");
            btnExcluirRisco.Attributes.Add("onClick", "javascript:return confirm('Você deseja realmente excluir este Risco?');");
            btnExcluirPerigo.Attributes.Add("onClick", "javascript:return confirm('Você deseja realmente excluir este Perigo?');");
        }

        private void PopulaLsbRiscoAcidente()
        {
            lsbRisco.DataSource = new RiscoAcidente().Get("IdCliente=" + lbl_Id_Empresa.Text
                + " AND IdRiscoAcidente NOT IN (SELECT IdRiscoAcidente FROM PerigoRiscoAcidente WHERE IdPerigo=" + lsbPerigos.SelectedValue
                + ") ORDER BY Nome");
            lsbRisco.DataValueField = "IdRiscoAcidente";
            lsbRisco.DataTextField = "Nome";
            lsbRisco.DataBind();

            lsbRisco.Items.Insert(0, new ListItem("Cadastrar um Novo Risco...", "0"));
            lsbRisco.Items[0].Selected = true;
        }

        private void PopulaLsbPerigo()
        {
            lsbPerigos.DataSource = new Perigo().GetIdNome("Nome", "IdCliente=" + lbl_Id_Empresa.Text);
            lsbPerigos.DataValueField = "Id";
            lsbPerigos.DataTextField = "Nome";
            lsbPerigos.DataBind();

            lsbPerigos.Items.Insert(0, new ListItem("Cadastrar um Novo Perigo...", "0"));
            lsbPerigos.Items[0].Selected = true;
        }

        protected void btnGravarPerigo_Click(object sender, EventArgs e)
        {
            try
            {
                Perigo perigo = new Perigo();
                string tipoProcess = string.Empty;

                if (lsbPerigos.SelectedValue.Equals("0"))
                {

                    Cliente xCliente;
                    xCliente = new Cliente();
                    xCliente.Inicialize();
                    xCliente.Find(System.Convert.ToInt32(lbl_Id_Empresa.Text));

                    tipoProcess = "cadastrado";
                    perigo.Inicialize();
                    perigo.IdCliente = xCliente;
                }
                else
                {
                    tipoProcess = "editado";
                    perigo.Find(Convert.ToInt32(lsbPerigos.SelectedValue));
                }

                perigo.Nome = txtNomePerigo.Text.Trim();

                perigo.UsuarioId = System.Convert.ToInt32( lbl_Id_Usuario.Text);
                perigo.Save();

                PopulaLsbPerigo();
                lsbPerigos.ClearSelection();
                lsbPerigos.Items.FindByValue(perigo.Id.ToString()).Selected = true;

                btnExcluirPerigo.Enabled = true;

                //StringBuilder st = new StringBuilder();

                //st.Append("window.opener.document.getElementById('txtAuxiliar').value='atualizaPerigo';");
                ////st.Append("window.opener.document.forms[0].submit();");
                ////st.Append("window.alert('O Perigo foi " + tipoProcess + " com sucesso!');");

                //this.ClientScript.RegisterStartupScript(this.GetType(), "GravarPerigo", st.ToString(), true);

                StringBuilder st = new StringBuilder();

                //st.Append("window.opener.document.getElementById('txtAuxiliar').value='atualizaCelula';");
                //st.Append("window.opener.document[0]['txtAuxiliar']='atualizaCelula';"); ;   // por que não funciona ??? - Wagner
                //st.Append("window.opener.document.forms[0].submit();");

                Session["txtAuxiliar"] = "atualizaPerigo";

                //st.Append("<script language='javascript'>");
                st.Append("window.opener.location.reload();");// refreshes main window (normally postback)
                st.Append("self.close();");//closes the pop up
                st.Append("</script>");
                //Page.RegisterStartUpScript("PopUpClose",st.ToString());


                this.ClientScript.RegisterStartupScript(this.GetType(), "GravarPerigo", st.ToString(),true);


            }
            catch (Exception ex)
            {
                
                MsgBox1.Show("Ilitera.Net", ex.Message, null,
                        new EO.Web.MsgBoxButton("OK"));                


            }
        }

        protected void btnExcluirPerigo_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet dsOperacaoPerigo = new OperacaoPerigo().Get("IdPerigo=" + lsbPerigos.SelectedValue);
                if (dsOperacaoPerigo.Tables[0].Rows.Count > 0)
                    throw new Exception("Não é possível excluir este Perigo! Este Perigo já está associado a uma ou mais Operações!");

                Perigo perigo = new Perigo(Convert.ToInt32(lsbPerigos.SelectedValue));

                perigo.UsuarioId = System.Convert.ToInt32( lbl_Id_Usuario.Text);
                perigo.Delete();

                PopulaLsbPerigo();
                lsbRiscoPerigo.Items.Clear();
                PopulaLsbRiscoAcidente();

                txtNomePerigo.Text = string.Empty;
                btnExcluirPerigo.Enabled = false;

                //StringBuilder st = new StringBuilder();

                //st.Append("window.opener.document.getElementById('txtAuxiliar').value='atualizaPerigo';");
                ////st.Append("window.opener.document.forms[0].submit();");
                ////st.Append("window.alert('O Perigo selecionado foi excluído com sucesso!');");

                //this.ClientScript.RegisterStartupScript(this.GetType(), "ExcluirPerigo", st.ToString(), true);

                StringBuilder st = new StringBuilder();

                //st.Append("window.opener.document.getElementById('txtAuxiliar').value='atualizaCelula';");
                //st.Append("window.opener.document[0]['txtAuxiliar']='atualizaCelula';"); ;   // por que não funciona ??? - Wagner
                //st.Append("window.opener.document.forms[0].submit();");

                Session["txtAuxiliar"] = "atualizaPerigo";

                //st.Append("<script language='javascript'>");
                st.Append("window.opener.location.reload();");// refreshes main window (normally postback)
                st.Append("self.close();");//closes the pop up
                st.Append("</script>");
                //Page.RegisterStartUpScript("PopUpClose",st.ToString());


                this.ClientScript.RegisterStartupScript(this.GetType(), "ExcluirPerigo", st.ToString(),true);


            }
            catch (Exception ex)
            {
                
                MsgBox1.Show("Ilitera.Net", ex.Message, null,
                        new EO.Web.MsgBoxButton("OK"));                

                btnExcluirPerigo.Enabled = true;
            }
        }

        protected void btnGravarRisco_Click(object sender, EventArgs e)
        {
            try
            {
                RiscoAcidente riscoAcidente = new RiscoAcidente();
                string tipoProcess = string.Empty;

                if (lsbRisco.SelectedValue.Equals("0"))
                {
                    Cliente xCliente;
                    xCliente = new Cliente();
                    xCliente.Inicialize();
                    xCliente.Find(System.Convert.ToInt32(lbl_Id_Empresa.Text));

                    tipoProcess = "cadastrado";
                    riscoAcidente.Inicialize();
                    riscoAcidente.IdCliente = xCliente;
                }
                else
                {
                    tipoProcess = "editado";
                    riscoAcidente.Find(Convert.ToInt32(lsbRisco.SelectedValue));
                }

                riscoAcidente.Nome = txtNomeRisco.Text.Trim();

                riscoAcidente.UsuarioId = System.Convert.ToInt32( lbl_Id_Usuario.Text);
                riscoAcidente.Save();

                PopulaLsbRiscoAcidente();
                lsbRisco.ClearSelection();
                lsbRisco.Items.FindByValue(riscoAcidente.Id.ToString()).Selected = true;

                btnExcluirRisco.Enabled = true;


                MsgBox1.Show("Ilitera.Net", "O Risco foi " + tipoProcess + " com sucesso!", null,
                        new EO.Web.MsgBoxButton("OK"));                

            }
            catch (Exception ex)
            {

                MsgBox1.Show("Ilitera.Net", ex.Message, null,
                        new EO.Web.MsgBoxButton("OK"));                

            }
        }

        protected void btnExcluirRisco_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet dsOperacaoRiscoAcidente = new OperacaoRiscoAcidente().Get("IdRiscoAcidente=" + lsbRisco.SelectedValue);
                if (dsOperacaoRiscoAcidente.Tables[0].Rows.Count > 0)
                    throw new Exception("Não é possível excluir este Risco! O Risco já está associado a uma ou mais Operações!");
                DataSet dsPerigoRiscoAcidente = new PerigoRiscoAcidente().Get("IdRiscoAcidente=" + lsbRisco.SelectedValue);
                if (dsPerigoRiscoAcidente.Tables[0].Rows.Count > 0)
                    throw new Exception("Não é possível excluir este Risco! O Risco já está associado a um ou mais Perigos!");

                RiscoAcidente riscoAcidente = new RiscoAcidente(Convert.ToInt32(lsbRisco.SelectedValue));

                riscoAcidente.UsuarioId = usuario.Id;
                riscoAcidente.Delete();

                PopulaLsbRiscoAcidente();

                txtNomeRisco.Text = string.Empty;
                btnExcluirRisco.Enabled = false;


                MsgBox1.Show("Ilitera.Net", "O Risco selecionado foi excluído com sucesso!", null,
                        new EO.Web.MsgBoxButton("OK"));                

            }
            catch (Exception ex)
            {
                
                MsgBox1.Show("Ilitera.Net", ex.Message, null,
                        new EO.Web.MsgBoxButton("OK"));                
                btnExcluirRisco.Enabled = true;
            }
        }

        protected void imgRemoveRisco_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (lsbRiscoPerigo.SelectedItem == null)
                    throw new Exception("Não há nenhum Risco selecionado para ser removido do Perigo!");

                bool checkProcess = false;

                foreach (ListItem itemRisco in lsbRiscoPerigo.Items)
                {
                    if (itemRisco.Selected)
                    {
                        PerigoRiscoAcidente perigoRisco = new PerigoRiscoAcidente();
                        perigoRisco.Find("IdPerigo=" + lsbPerigos.SelectedValue
                            + " AND IdRiscoAcidente=" + itemRisco.Value);

                        if (!checkProcess)
                        {
                            checkProcess = true;
                            perigoRisco.UsuarioProcessoRealizado = "Desassociação do Risco ao Perigo";
                        }

                        perigoRisco.UsuarioId = System.Convert.ToInt32( lbl_Id_Usuario.Text);
                        perigoRisco.Delete();
                    }
                }

                PopulaLsbPerigoRisco();
                PopulaLsbRiscoAcidente();


                MsgBox1.Show("Ilitera.Net", "Os Riscos foram desassociados com sucesso do Perigo selecionado!", null,
                        new EO.Web.MsgBoxButton("OK"));                

            }
            catch (Exception ex)
            {
                
                MsgBox1.Show("Ilitera.Net", ex.Message, null,
                        new EO.Web.MsgBoxButton("OK"));                

            }
        }

        protected void imbAddRisco_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (lsbPerigos.SelectedValue.Equals("0"))
                    throw new Exception("É necessário selecionar um Perigo antes de associar um Risco!");
                if (lsbRisco.SelectedValue.Equals("0"))
                    throw new Exception("É necessário selecionar um Risco para ser associado ao Perigo!");

                foreach (ListItem itemRisco in lsbRisco.Items)
                {
                    if (itemRisco.Selected)
                    {
                        PerigoRiscoAcidente perigoRisco = new PerigoRiscoAcidente();
                        perigoRisco.Inicialize();

                        perigoRisco.IdPerigo.Id = Convert.ToInt32(lsbPerigos.SelectedValue);
                        perigoRisco.IdRiscoAcidente.Id = Convert.ToInt32(lsbRisco.SelectedValue);

                        perigoRisco.UsuarioProcessoRealizado = "Associação do Risco ao Perigo";
                        perigoRisco.UsuarioId = System.Convert.ToInt32( lbl_Id_Usuario.Text);
                        perigoRisco.Save();
                    }
                }

                PopulaLsbPerigoRisco();
                PopulaLsbRiscoAcidente();

                txtNomeRisco.Text = string.Empty;


                MsgBox1.Show("Ilitera.Net", "O Risco foi associado com sucesso ao Perigo selecionado!", null,
                        new EO.Web.MsgBoxButton("OK"));                

            }
            catch (Exception ex)
            {
                
                MsgBox1.Show("Ilitera.Net", ex.Message, null,
                        new EO.Web.MsgBoxButton("OK"));                

            }
        }

        protected void lsbPerigos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lsbPerigos.SelectedValue.Equals("0"))
            {
                txtNomePerigo.Text = string.Empty;
                lsbRiscoPerigo.Items.Clear();
                btnExcluirPerigo.Enabled = false;                
                PopulaLsbRiscoAcidente();
            }
            else
            {
                Perigo perigo = new Perigo(Convert.ToInt32(lsbPerigos.SelectedValue));
                txtNomePerigo.Text = perigo.Nome;
                
                btnExcluirPerigo.Enabled = true;

                PopulaLsbPerigoRisco();
                PopulaLsbRiscoAcidente();
            }

            txtNomePerigo.Focus();
        }

        private void PopulaLsbPerigoRisco()
        {
            lsbRiscoPerigo.DataSource = new RiscoAcidente().GetIdNome("Nome", "IdRiscoAcidente IN (SELECT IdRiscoAcidente FROM PerigoRiscoAcidente WHERE IdPerigo=" + lsbPerigos.SelectedValue + ")");
            lsbRiscoPerigo.DataValueField = "Id";
            lsbRiscoPerigo.DataTextField = "Nome";
            lsbRiscoPerigo.DataBind();
        }
    }
}
