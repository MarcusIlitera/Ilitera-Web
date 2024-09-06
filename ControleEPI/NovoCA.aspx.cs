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
using System.Net;
using System.IO;

namespace Ilitera.Net.ControleEPI
{
    /// <summary>
	/// Summary description for ListagemEPI.
	/// </summary>
	public partial class NovoCA : System.Web.UI.Page
	{
		private CA dadosCA;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
            InicializaWebPageObjects();


            Entities.Usuario user = (Entities.Usuario)Session["usuarioLogado"];

            int xIdEmpresa = System.Convert.ToInt32(Session["Empresa"]);
            int xIdUsuario = user.IdUsuario;

            lbl_Id_Empresa.Text = xIdEmpresa.ToString().Trim();
            lbl_Id_Usuario.Text = xIdUsuario.ToString().Trim();

            

			if(!IsPostBack)
			{

                PopulaDDLFabricante();

                //PopulaDDLFabricante();
                PopulaDDLEquipamento();
				//btnGravar.Attributes.Add("onClick", "javascript: if (isNull(document.forms[0].txtCA.value)) {window.alert('O Nº do CA deve ser fornecido!'); return false;} else return VerificaData();");
                //btnProcurarCA.Attributes.Add("onClick", "javascript: if (isNull(document.forms[0].txtCA.value)) {window.alert('O Nº do CA deve ser fornecido!'); return false;} else return true;");

                //st.Append("void(addItemPop(centerWin('AddFabricante.aspx?TipoJanela=NovoCA&IdFabricante=" + ddlFabricante.SelectedItem.Value + "&IdEmpresa=" + lbl_Id_Empresa.Text + "&IdUsuario=" + lbl_Id_Usuario.Text + "',400,160,\'CadFabricante\')))");
                hlkNovo.NavigateUrl = "javascript:void(addItem(centerWin('AddFabricante.aspx?TipoJanela=NovoCA&IdFabricante=" + ddlFabricante.SelectedItem.Value + "&IdEmpresa=" + lbl_Id_Empresa.Text + "&IdUsuario=" + lbl_Id_Usuario.Text + "',400,160,\'CadFabricante\'),\'Todos\'))";
                hlkNovo.Attributes.Add("onclick", "void(addItem(centerWin('AddFabricante.aspx?TipoJanela=NovoCA&IdFabricante=" + ddlFabricante.SelectedItem.Value + "&IdEmpresa=" + lbl_Id_Empresa.Text + "&IdUsuario=" + lbl_Id_Usuario.Text + "',400,160,\'CadFabricante\'),\'Todos\'))");
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
		
		private void InitializeComponent()
		{    

		}
		#endregion

        protected  void InicializaWebPageObjects()
        {
            //base.InicializaWebPageObjects();

			dadosCA = new CA();
			dadosCA.Inicialize();

			//if (usuario.Id == -224701788 || usuario.Id == 1814000994)
			//{
				//txtCATudo.Visible = true;
				//btnTudo.Visible = true;
            //}
            //else
            //{
                txtCATudo.Visible = false;
                btnTudo.Visible = false;
                btnProcurarCA.Visible = false;
            //}
		}
		
		private void PopulaDDLFabricante()
		{		
			//ddlFabricante.Items.Insert(0, new ListItem("Selecione um Fabricante...", "0"));
            string fabricante = "";

            DataSet ds = new Fabricante().GetAllFabricantes();
            if (ds.Tables[0].Rows.Count == 0)
            {
                Fabricante newfabricante = new Fabricante();
                newfabricante.Nome = fabricante;
                ddlFabricante.Items.Insert(0, new ListItem(fabricante, newfabricante.Save().ToString()));
                ddlFabricante.SelectedItem.Selected = false;
                ddlFabricante.Items.FindByValue(newfabricante.Id.ToString()).Selected = true;
            }
            else
            {
                for (int zCont = 0; zCont < ds.Tables[0].Rows.Count; zCont++)
                {
                    ddlFabricante.Items.Insert(0, new ListItem(ds.Tables[0].Rows[zCont]["Nome"].ToString(), ds.Tables[0].Rows[zCont]["IdFabricante"].ToString()));
                    ddlFabricante.SelectedItem.Selected = false;
                    //ddlFabricante.Items.FindByValue(ds.Tables[0].Rows[zCont]["IdFabricante"].ToString()).Selected = true;
                }
            }

		}

		private void PopulaDDLFabricante(string fabricante)
		{
			if(fabricante==null)
				return;

			DataSet ds = new Fabricante().GetFabricanteByNome(fabricante);
			if (ds.Tables[0].Rows.Count == 0)
			{
				Fabricante newfabricante = new Fabricante();
				newfabricante.Nome = fabricante;
				ddlFabricante.Items.Insert(0, new ListItem(fabricante, newfabricante.Save().ToString()));
				ddlFabricante.SelectedItem.Selected = false;
				ddlFabricante.Items.FindByValue(newfabricante.Id.ToString()).Selected = true;
			}
			else
			{
				ddlFabricante.Items.Insert(0, new ListItem(fabricante, ds.Tables[0].Rows[0]["IdFabricante"].ToString()));
				ddlFabricante.SelectedItem.Selected = false;
				ddlFabricante.Items.FindByValue(ds.Tables[0].Rows[0]["IdFabricante"].ToString()).Selected = true;
			}
		}

		private void PopulaDDLEquipamento()
		{
			//ddlEquipamento.Items.Insert(0, new ListItem("Selecione um Equipamento...", "0"));
            string equipamento = "";

            DataSet ds = new TipoEPI().GetAllEquipamentos();

            if (ds.Tables[0].Rows.Count == 0)
            {
                TipoEPI tipoEquipamento = new TipoEPI();
                tipoEquipamento.Nome = equipamento;
                ddlEquipamento.Items.Insert(0, new ListItem(equipamento, tipoEquipamento.Save().ToString()));
                ddlEquipamento.SelectedItem.Selected = false;
                ddlEquipamento.Items.FindByValue(tipoEquipamento.Id.ToString()).Selected = true;
            }
            else
            {
                for (int zCont = 0; zCont < ds.Tables[0].Rows.Count; zCont++)
                {
                    ddlEquipamento.Items.Insert(0, new ListItem(ds.Tables[0].Rows[zCont]["Nome"].ToString(), ds.Tables[0].Rows[zCont]["IdTipoEPI"].ToString()));
                    ddlEquipamento.SelectedItem.Selected = false;
                    //ddlEquipamento.Items.FindByValue(ds.Tables[0].Rows[0]["IdTipoEPI"].ToString()).Selected = true;
                }
            }


		}

		private void PopulaDDLEquipamento(string equipamento)
		{
			if(equipamento==null)
				return;

			DataSet ds = new TipoEPI().GetEquipamentoByNome(equipamento);

			if (ds.Tables[0].Rows.Count == 0)
			{
				TipoEPI tipoEquipamento = new TipoEPI();
				tipoEquipamento.Nome = equipamento;
				ddlEquipamento.Items.Insert(0, new ListItem(equipamento, tipoEquipamento.Save().ToString()));
				ddlEquipamento.SelectedItem.Selected = false;
				ddlEquipamento.Items.FindByValue(tipoEquipamento.Id.ToString()).Selected = true;
			}
			else
			{
				ddlEquipamento.Items.Insert(0, new ListItem(equipamento, ds.Tables[0].Rows[0]["IdTipoEPI"].ToString()));			
				ddlEquipamento.SelectedItem.Selected = false;
				ddlEquipamento.Items.FindByValue(ds.Tables[0].Rows[0]["IdTipoEPI"].ToString()).Selected = true;
			}

		}

		protected void btnGravar_Click(object sender, System.EventArgs e)
		{
			try
			{
				DataSet DSverificaCAExiste = new CA().Get("NumeroCA=" + txtCA.Text);
                bool isAtualizacao = false;

                if (DSverificaCAExiste.Tables[0].Rows.Count >= 1)
                {
                    dadosCA = new CA(Convert.ToInt32(DSverificaCAExiste.Tables[0].Rows[0]["IdCA"]));
                    isAtualizacao = true;
                }
                
                PopulaCABanco();
                dadosCA.Save();

                StringBuilder st = new StringBuilder("");

                st.Append("window.opener.document.forms[0].txtAuxUpdate.value = 'true';");
                st.Append("window.opener.document.forms[0].submit();");
                
                if (isAtualizacao)
                    st.Append("window.alert(\"O CA \'" + txtCA.Text + "\' foi atualizado com sucesso!\");");
                else
                    st.Append("window.alert(\"O CA \'" + txtCA.Text + "\' foi adicionado com sucesso!\");");

                //ResetHTMLControls();

                this.ClientScript.RegisterStartupScript(this.GetType(), "FecharJanela", st.ToString(), true);

                Response.Redirect("~/ControleEPI/CadastroEPI.aspx");
			}
			catch(Exception ex)
			{
				lblError.Text = ex.Message;
			} 
		}

		private void PopulaCABanco()
		{
			dadosCA.NumeroCA = Convert.ToInt32(txtCA.Text);

			if (ddlFabricante.SelectedItem.Value == "0")
				throw new ArgumentException("O Fabricante precisa ser selecionado!");
			else
				dadosCA.IdFabricante.Id = Convert.ToInt32(ddlFabricante.SelectedItem.Value);

            dadosCA.IdFabricante.Find();

			if (ddlEquipamento.SelectedItem.Value == "0")
				throw new ArgumentException("O Equipamento precisa ser selecionado!");
			else
                dadosCA.IdTipoEPI.Id = Convert.ToInt32(ddlEquipamento.SelectedItem.Value);

			dadosCA.DataEmissao = new DateTime(Convert.ToInt32(txtaae.Text), Convert.ToInt32(txtmme.Text), Convert.ToInt32(txtdde.Text));
			dadosCA.Validade = new DateTime(Convert.ToInt32(txtaav.Text), Convert.ToInt32(txtmmv.Text), Convert.ToInt32(txtddv.Text));
			dadosCA.DescricaoEPI = txtDescricao.Text;
			dadosCA.AprovadoEPI = txtObjetivo.Text;
		}

		private void ResetHTMLControls()
		{
			txtCA.Text = "";
			ddlFabricante.SelectedItem.Selected = false;
			//ddlFabricante.Items.FindByValue("0").Selected = true;
			ddlEquipamento.SelectedItem.Selected = false;
			ddlEquipamento.Items.FindByValue("0").Selected = true;
			txtdde.Text = "";
			txtmme.Text = "";
			txtaae.Text = "";
			txtddv.Text = "";
			txtmmv.Text = "";
			txtaav.Text = "";
			txtObjetivo.Text = "";
			txtDescricao.Text = "";
		}

		protected void lblCancel_Click(object sender, System.EventArgs e)
		{
			//StringBuilder st = new StringBuilder();

			//st.Append("self.close();");

            //this.ClientScript.RegisterStartupScript(this.GetType(), "CloseNovoCA", st.ToString(), true);
            Response.Redirect("~/ControleEPI/" + Session["Pagina_Anterior"].ToString()); //CadastroEPI.aspx");
		}

		protected void btnNovoFabricante_Click(object sender, System.EventArgs e)
		{
			StringBuilder st = new StringBuilder();

			st.Append("void(addItemPop(centerWin('AddFabricante.aspx?TipoJanela=NovoCA&IdFabricante=" + ddlFabricante.SelectedItem.Value + "&IdEmpresa=" + lbl_Id_Empresa.Text + "&IdUsuario=" + lbl_Id_Usuario.Text + "',400,160,\'CadFabricante\')))");

            this.ClientScript.RegisterStartupScript(this.GetType(), "Fabricante", st.ToString(), true);
		}

		protected void btnNovoEquipamento_Click(object sender, System.EventArgs e)
		{
			StringBuilder st = new StringBuilder();

			st.Append("void(addItemPop(centerWin('AddEquipamento.aspx?TipoJanela=NovoCA&IdEquipamento=" + ddlEquipamento.SelectedItem.Value + "&IdEmpresa=" + lbl_Id_Empresa.Text + "&IdUsuario=" + lbl_Id_Usuario.Text + "',600,160,\'CadEquipamento\')))");

            this.ClientScript.RegisterStartupScript(this.GetType(), "Equipamento", st.ToString(), true);
		}

	
		private void PopulaData(TextBox dd, TextBox mm, TextBox aa, DateTime DataAdmissao)
		{
			dd.Text = DataAdmissao.ToString("dd");
			mm.Text = DataAdmissao.ToString("MM");
			aa.Text = DataAdmissao.ToString("yyyy");
		}
		
		protected void btnProcurarCA_Click(object sender, System.EventArgs e)
		{			
			if (ddlFabricante.Items.Count != 1)
                ddlFabricante.Items.RemoveAt(0);
			if (ddlEquipamento.Items.Count != 1)
				ddlEquipamento.Items.RemoveAt(0);

			try
			{
				CA novoCa = CA.ProcuraCA_MTE(Convert.ToInt32(txtCA.Text));
				txtDescricao.Text	= novoCa.DescricaoEPI;
				txtObjetivo.Text	= novoCa.AprovadoEPI;
				PopulaData(txtdde, txtmme, txtaae, novoCa.DataEmissao);
				PopulaData(txtddv, txtmmv, txtaav, novoCa.Validade);
				PopulaDDLEquipamento(novoCa.IdTipoEPI.Nome);
				PopulaDDLFabricante(novoCa.IdFabricante.Nome);
				lblError.Text = "";
			}
			catch(CA.NumeroNaoEncontrado ex)
			{
				ResetHTMLControls();
				lblError.Text = ex.Message; 
			}
		}

		protected void btnTudo_Click(object sender, System.EventArgs e)
		{
			for (int i = Convert.ToInt32(txtCATudo.Text); i < Convert.ToInt32(txtCATudo.Text)+100; i++)
			{
				DataSet DSverificaCAExiste = new CA().Get("NumeroCA=" + i);

				if (DSverificaCAExiste.Tables[0].Rows.Count > 0)
					continue;
				else
				{
					try
					{
						CA novoCa = CA.ProcuraCA_MTE(i);
						PopulaDDLEquipamento(novoCa.IdTipoEPI.Nome);
						PopulaDDLFabricante(novoCa.IdFabricante.Nome);
						novoCa.Save();
					}
					catch(CA.NumeroNaoEncontrado ex)
					{
						System.Diagnostics.Debug.WriteLine(ex.Message);
						continue;
					}
				}
			}
		}
	}
}
