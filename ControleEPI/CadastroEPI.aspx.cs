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
//using MestraNET;
using Ilitera.Common;
using Ilitera.Data;
using Ilitera.Opsa.Data;
using System.Text;

namespace Ilitera.Net
{
    /// <summary>
	/// Summary description for ListagemEPI.
	/// </summary>
    /// 
    
    public partial class CadastroEPI : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Button lblCancel;
		protected System.Web.UI.WebControls.Label lblError;
		protected System.Web.UI.WebControls.Label lblQtdEstoque;
		protected System.Web.UI.WebControls.TextBox txtQtdEstoque;

        protected Prestador prestador = new Prestador();
        protected Ilitera.Common.Usuario usuario = new Ilitera.Common.Usuario();
        protected Cliente cliente = new Cliente();
        protected Ilitera.Opsa.Data.Empregado empregado = new Ilitera.Opsa.Data.Empregado();
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
            //InicializaWebPageObjects();

            Entities.Usuario user = (Entities.Usuario)Session["usuarioLogado"];

            int xIdEmpresa = System.Convert.ToInt32(Session["Empresa"]);
            int xIdUsuario = user.IdUsuario;

            lbl_Id_Empresa.Text = xIdEmpresa.ToString().Trim();
            lbl_Id_Usuario.Text = xIdUsuario.ToString().Trim();


			if (!IsPostBack)
			{


                try
                {
                    string FormKey = this.Page.ToString().Substring(4);

                    Ilitera.Common.Funcionalidade funcionalidade = new Ilitera.Common.Funcionalidade();
                    funcionalidade.Find("ClassName='" + FormKey + "'");

                    if (funcionalidade.Id == 0)
                        throw new Exception("Formulário não cadastrado - " + FormKey);

                    Entities.Usuario xUser = (Entities.Usuario)Session["usuarioLogado"];
                    Ilitera.Common.Usuario.Permissao_Web(xUser.IdUsuario, funcionalidade.Id);
                }

                catch (Exception ex)
                {
                    Session["Message"] = ex.Message;
                    Server.Transfer("~/Tratar_Excecao.aspx");
                    return;
                }
				//GetMenu(((int)IndMenuType.Empresa).ToString(), lbl_Id_Usuario.Text.ToString(), lbl_Id_Empresa.Text.ToString());
				//PreencheLabels("lblEmp", cliente.NomeAbreviado.ToString());
				lsbCAs.Attributes.Add("ondblclick", "void(addItem(centerWin('DetalheCA.aspx?IdCA=' + this.value + '&IdUsuario=" + lbl_Id_Usuario.Text + "&IdEmpresa=" + lbl_Id_Empresa.Text + "',700,520,\'DetalheCA\'),\'Todos\'))");
				lsbCASelect.Attributes.Add("ondblclick", "void(addItem(centerWin('DetalheCA.aspx?IdCA=' + this.value + '&IdUsuario=" + lbl_Id_Usuario.Text + "&IdEmpresa=" + lbl_Id_Empresa.Text + "',700,520,\'DetalheCA\'),\'Todos\'))");
                //lsbCASelect.Attributes.Add("ondblclick", "javascript:addItemPop(centerWin('DetalheCA.aspx?IdCA=' + this.value + '&IdUsuario=" + lbl_Id_Usuario.Text + "&IdEmpresa=" + lbl_Id_Empresa.Text + "',680,420,\'DetalheCA\'));");
                //lsbCAs.Attributes.Add("ondblclick", "javascript:addItemPop(centerWin('DetalheCA.aspx?IdCA=' + this.value + '&IdUsuario=" + lbl_Id_Usuario.Text + "&IdEmpresa=" + lbl_Id_Empresa.Text + "',680,420,\'DetalheCA\'));");


                btnVerCA.Enabled = true;
                


                btnGravar.Attributes.Add("onClick", "return VerificaCampoInt();");
				PopulaDropDownEPI();
				PopulaListBoxCA();
			}
			else if (txtAuxUpdate.Value == "true")
			{
				PopulaListBoxCA();
				txtAuxUpdate.Value = string.Empty;
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

		private void PopulaDropDownEPI()
		{
            LaudoTecnico laudo = LaudoTecnico.GetUltimoLaudo(System.Convert.ToInt32(lbl_Id_Empresa.Text));

            ddlEPI.DataSource = laudo.GetEPI();
            ddlEPI.DataValueField = "Id";
            ddlEPI.DataTextField = "Descricao";
            ddlEPI.DataBind();
            ddlEPI.Items.Insert(0, new ListItem("Selecione o EPI...", "0"));
		}

		private void PopulaListBoxCASelected()
		{
			lsbCASelect.DataSource = new CA().GetCAAssociadoEPI(System.Convert.ToInt32( lbl_Id_Empresa.Text), Convert.ToInt32(ddlEPI.SelectedItem.Value));
			lsbCASelect.DataValueField = "IdCA";
			lsbCASelect.DataTextField = "NumeroCA";
			lsbCASelect.DataBind();
		}

        private void PopulaListBoxCA()
        {
            //LaudoTecnico laudo = LaudoTecnico.GetUltimoLaudo(System.Convert.ToInt32(lbl_Id_Empresa.Text));

            //DataSet ds = new Ghe().Get("nID_LAUD_TEC =" + laudo.Id + " ORDER BY tNO_FUNC");
            //ArrayList epitotal = new ArrayList();
            //bool Verifica;
            //bool Verifica2;

            //foreach (DataRow ghe in ds.Tables[0].Select())
            //{
            //    ArrayList listGheEpiExistente = new GheEpiExistente().Find("nID_FUNC=" + ghe["nID_FUNC"]);
            //    ArrayList listRiscoEpiExistente = new EpiExistente().FindIn(new PPRA().GetType(), "", true, "nID_FUNC=" + ghe["nID_FUNC"]);

            //    foreach (GheEpiExistente gheepi in listGheEpiExistente)
            //    {
            //        gheepi.nID_EPI.Find();
            //        Verifica = true;

            //        foreach (Epi epicompare in epitotal)
            //        {
            //            if (Convert.ToInt32(gheepi.nID_EPI.Id) == Convert.ToInt32(epicompare.Id))
            //            {
            //                Verifica = false;
            //                break;
            //            }
            //        }

            //        if (Verifica == true)
            //            epitotal.Add(gheepi.nID_EPI);
            //    }

            //    foreach (EpiExistente riscoepi in listRiscoEpiExistente)
            //    {
            //        riscoepi.nID_EPI.Find();
            //        Verifica2 = true;

            //        foreach (Epi epicompare in epitotal)
            //        {
            //            if (Convert.ToInt32(riscoepi.nID_EPI.Id) == Convert.ToInt32(epicompare.Id))
            //            {
            //                Verifica2 = false;
            //                break;
            //            }
            //        }

            //        if (Verifica2 == true)
            //            epitotal.Add(riscoepi.nID_EPI);
            //    }
            //}

            //epitotal.Sort();

            //ArrayList alEPIAtual = epitotal;
            //string IdsEPI = string.Empty;

            //foreach (Epi epiAtual in alEPIAtual)
            //    if (IdsEPI == string.Empty)
            //        IdsEPI = epiAtual.Id.ToString();
            //    else
            //        IdsEPI += ", " + epiAtual.Id.ToString();

            //if (IdsEPI != string.Empty)
            //{
            //lsbCAs.DataSource = new CA().Get("IdCA NOT IN (SELECT IdCA FROM EPIClienteCA WHERE IdCliente=" + lbl_Id_Empresa.Text
            //    + " AND IdEPI IN (" + IdsEPI + "))"
            //    + " ORDER BY NumeroCA");

            lsbCAs.DataSource = new CA().Get(" IdCA <> 0 ORDER BY NumeroCA");

            lsbCAs.DataValueField = "IdCA";
            lsbCAs.DataTextField = "NumeroCA";
            lsbCAs.DataBind();
            //}
        }


        protected void btnGravar_Click(object sender, System.EventArgs e)
        {
            try
            {
                int selectedCount = 0;

                Entities.Usuario xUser = (Entities.Usuario)Session["usuarioLogado"];

                foreach (ListItem item in lsbCASelect.Items)
                {
                    if (item.Selected)
                    {
                        EPIClienteCA perEPICA = new EPIClienteCA();
                        perEPICA.Find("IdEPI=" + ddlEPI.SelectedItem.Value
                            + " AND IdCliente=" + lbl_Id_Empresa.Text
                            + " AND IdCA=" + item.Value);

                        perEPICA.NumPeriodicidade = Convert.ToInt32(txtPeriodicidade.Text);
                        perEPICA.Periodicidade = Convert.ToInt32(ddlTipoPeriodicidade.SelectedItem.Value);
                        perEPICA.UsuarioId = xUser.IdUsuario; //usuario.Id;
                        perEPICA.Save();

                        selectedCount++;
                    }
                }

                if (selectedCount == 0)
                {
                    MsgBox1.Show("Ilitera.Net", "É necessário selecionar um CA associado, na tabela ao lado, para informar e gravar a sua periodicidade!", null,
                        new EO.Web.MsgBoxButton("OK"));
                    txtPeriodicidade.Text = "";
                }
                else
                {
                    MsgBox1.Show("Ilitera.Net", "A periodicidade foi salva com sucesso nos CA associados selecionados!", null,
                            new EO.Web.MsgBoxButton("OK"));

                    txtPeriodicidade.Text = string.Empty;
                    ddlTipoPeriodicidade.ClearSelection();
                    foreach (ListItem item in lsbCASelect.Items)
                        if (item.Selected)
                            item.Selected = false;
                }
            }
            catch (Exception ex)
            {
                MsgBox1.Show("Ilitera.Net", ex.Message, null,
                        new EO.Web.MsgBoxButton("OK"));

            }
        }

		private void ResetHTMLControls()
		{
			txtPeriodicidade.Text = string.Empty;
			ddlTipoPeriodicidade.ClearSelection();
			PopulaListBoxCA();
			while (lsbCASelect.Items.Count > 0)
				lsbCASelect.Items.RemoveAt(0);
		}

		protected void btnAdcionarCA_Click(object sender, System.EventArgs e)
		{
			StringBuilder st = new StringBuilder();

			//st.Append("void(addItem(centerWin('NovoCA.aspx?IdUsuario=" + lbl_Id_Usuario.Text + "&IdEmpresa=" + lbl_Id_Empresa.Text + "',620,365,\'NovoCA\'),\'Todos\'))");
            //this.ClientScript.RegisterStartupScript(this.GetType(), "CADetalhe", st.ToString(), true);

            Session["Pagina_Anterior"] = "CadastroEPI.aspx";
            Response.Redirect("NovoCA.aspx?IdUsuario=" + lbl_Id_Usuario.Text + "&IdEmpresa=" + lbl_Id_Empresa.Text);

            
		}


        protected void btnVerCA_Click(object sender, System.EventArgs e)
        {

            //st.Append("void(addItem(centerWin('NovoCA.aspx?IdUsuario=" + lbl_Id_Usuario.Text + "&IdEmpresa=" + lbl_Id_Empresa.Text + "',620,365,\'NovoCA\'),\'Todos\'))");
            //this.ClientScript.RegisterStartupScript(this.GetType(), "CADetalhe", st.ToString(), true);

            //btnVerCA.Attributes.Add("onClick", "void(addItem(centerWin('DetalheCA.aspx?IdCA='" + lsbCAs.SelectedItem.Value + "'&IdUsuario=" + lbl_Id_Usuario.Text + "&IdEmpresa=" + lbl_Id_Empresa.Text + "',680,420,\'DetalheCA\'),\'Todos\'))");

            //Session["Pagina_Anterior"] = "CadastroEPI.aspx";
            //Response.Redirect("DetalheCA.aspx?IdCA=" + lsbCAs.SelectedItem.Value + "&IdUsuario=" + lbl_Id_Usuario.Text + "&IdEmpresa=" + lbl_Id_Empresa.Text);



            //st.Append("javascript:centerWin('DetalheCA.aspx?IdCA='" + lsbCAs.SelectedItem.Value + "'&IdUsuario=" + lbl_Id_Usuario.Text + "&IdEmpresa=" + lbl_Id_Empresa.Text + "',680,420,\'DetalheCA\');");

            //StringBuilder st = new StringBuilder();

            //st.Append("javascript: window.open('DetalheCA.aspx?IdCA='" + lsbCAs.SelectedItem.Value + "'&IdUsuario=" + lbl_Id_Usuario.Text + "&IdEmpresa=" + lbl_Id_Empresa.Text + "', 'DetalheCA', toolbar=no,location=no,directories=no,status=no,menubar=no,scrollbars=no,resizable=no,copyhistory=no,width=680, height=420,top=500, left=200);");
            //this.ClientScript.RegisterStartupScript(this.GetType(), "CA", st.ToString(), true);

            StringBuilder st = new StringBuilder();

            st.AppendFormat("void(window.open('DetalheCA.aspx?IdCA=" + lsbCAs.SelectedItem.Value + "&IdUsuario=" + lbl_Id_Usuario.Text + "&IdEmpresa=" + lbl_Id_Empresa.Text + @"', 'CA','scrollbars=yes, resizable=yes, toolbar=no, addressbar=no, menubar=no, width=700px, height=520px'));");

            ScriptManager.RegisterStartupScript(this, this.GetType(), " ", st.ToString(), true);
        }


        protected void ddlEPI_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (ddlEPI.SelectedItem.Value != "0")
			{
				PopulaListBoxCA();
				PopulaListBoxCASelected();
				txtPeriodicidade.Text = string.Empty;
				ddlTipoPeriodicidade.ClearSelection();
			}
			else
				ResetHTMLControls();
		}

		protected void btnAdiciona_Click(object sender, System.EventArgs e)
		{
            if (ddlEPI.SelectedItem.Value != "0")
            {
                foreach (ListItem item in lsbCAs.Items)
                    if (item.Selected)
                    {
                        Ilitera.Opsa.Data.Cliente xCliente = new Ilitera.Opsa.Data.Cliente(System.Convert.ToInt32(lbl_Id_Empresa.Text));


                        EPIClienteCA CadEPICA = new EPIClienteCA();
                        CadEPICA.Inicialize();

                        CadEPICA.IdEPI.Id = Convert.ToInt32(ddlEPI.SelectedItem.Value);
                        CadEPICA.IdCliente = xCliente; //cliente;
                        CadEPICA.IdCA.Id = Convert.ToInt32(item.Value);

                        CadEPICA.UsuarioId = Convert.ToInt32(lbl_Id_Usuario.Text);
                        CadEPICA.Save();
                    }

                PopulaListBoxCA();
                PopulaListBoxCASelected();
                txtPeriodicidade.Text = string.Empty;
                ddlTipoPeriodicidade.ClearSelection();
            }
            else
                MsgBox1.Show("Ilitera.Net", "É necessário selecionar um EPI para associar um CA!", null,
                        new EO.Web.MsgBoxButton("OK"));                

		}

		protected void btnRemove_Click(object sender, System.EventArgs e)
		{
			foreach (ListItem item in lsbCASelect.Items)
				if (item.Selected)
				{
					EPIClienteCA remEPICA = new EPIClienteCA();
					remEPICA.Find("IdEPI=" + ddlEPI.SelectedItem.Value
						+" AND IdCliente=" + lbl_Id_Empresa.Text
						+" AND IdCA=" + item.Value);
					remEPICA.UsuarioId = Convert.ToInt32( lbl_Id_Usuario.Text );

					DataSet ds = new EPICAEntregaDetalhe().Get("IdEPIClienteCA=" + remEPICA.Id);

					if (ds.Tables[0].Rows.Count > 0)
					{
						remEPICA.IdCA.Find();
                        MsgBox1.Show("Ilitera.Net", "O CA " + remEPICA.IdCA.NumeroCA + " não pode ser removido! Ele está sendo ou já foi utilizado por um ou mais funcionários!", null,
                                new EO.Web.MsgBoxButton("OK"));                

					}
					else
						remEPICA.Delete();
				}

			PopulaListBoxCA();
			PopulaListBoxCASelected();
			txtPeriodicidade.Text = string.Empty;
			ddlTipoPeriodicidade.ClearSelection();
		}

		protected void lsbCASelect_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			int selectedCount = 0;
			
			foreach (ListItem item in lsbCASelect.Items)
				if (item.Selected)
					selectedCount++;
            
			if (selectedCount == 1)
			{
				EPIClienteCA perEPICA = new EPIClienteCA();
				perEPICA.Find("IdEPI=" + ddlEPI.SelectedItem.Value
					+" AND IdCliente=" + lbl_Id_Empresa.Text
					+" AND IdCA=" + lsbCASelect.SelectedItem.Value);

				if (perEPICA.NumPeriodicidade != 0)
					txtPeriodicidade.Text = perEPICA.NumPeriodicidade.ToString();
				else
					txtPeriodicidade.Text = string.Empty;
				ddlTipoPeriodicidade.ClearSelection();
				ddlTipoPeriodicidade.Items.FindByValue(perEPICA.Periodicidade.ToString()).Selected = true;
			}
			else if (selectedCount > 1)
				txtPeriodicidade.Text = "##";
		}

        protected void cmd_Atualizar_Click(object sender, EventArgs e)
        {
            Response.Redirect("ListagemEPI.aspx");
        }


        protected void cvdCaracteres_ServerValidate(object source, System.Web.UI.WebControls.ServerValidateEventArgs args)
        {
            //VerificaCaracter(args, txtChave, cvdCaracteres);
        }
    }
}
