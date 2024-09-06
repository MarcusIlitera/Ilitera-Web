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
using System.Configuration;

namespace Ilitera.Net.ControleEPI
{
    /// <summary>
	/// Summary description for ListagemEPI.
	/// </summary>
	public partial class DetalheCA : System.Web.UI.Page
	{
		private CA dadosCA;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
            string xUsuario = Session["usuarioLogado"].ToString();

            if (Request["IdCA"] != string.Empty && Request["IdCA"] != null)
			{
                InicializaWebPageObjects();


				if(!IsPostBack)
				{
					PopulaCA();
					PopulaDDLFabricante();
					PopulaDDLEquipamento();
					btnGravar.Attributes.Add("onClick", "javascript: return VerificaData();");

                    Carregar_Imagem();

                }
                else
				{
					RetornaValorDDL(ddlFabricante, "Fabricante");
					RetornaValorDDL(ddlEquipamento, "Equipamento");
				}
			}
			else
                MsgBox1.Show("Ilitera.Net", "É necessário selecionar e dar um duplo clique no CA que deseja visualizar os detalhes!", null,
                    new EO.Web.MsgBoxButton("OK"));                

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

        protected  void InicializaWebPageObjects()
        {
            //base.InicializaWebPageObjects();

			dadosCA = new CA(Convert.ToInt32(Request["IdCA"]));
			dadosCA.IdFabricante.Find();
			dadosCA.IdTipoEPI.Find();			
		}

		private void RetornaValorDDL(DropDownList ddl, string tipo)
		{
			string ddlvalue = ddl.SelectedItem.Value;

			if (tipo == "Fabricante")
			{
				ddl.DataSource = new Fabricante().GetAll("Nome");
				ddl.DataValueField = "IdFabricante";
			}
			else if (tipo == "Equipamento")
			{
				ddl.DataSource = new TipoEPI().GetAll("Nome");
				ddl.DataValueField = "IdTipoEPI";
			}
			
			ddl.DataTextField = "Nome";
			ddl.DataBind();

			for (int i=0; i<ddl.Items.Count; i++)
			{
				if (ddl.Items[i].Value == ddlvalue)
					ddl.Items.FindByValue(ddlvalue).Selected = true;
			}
		}
		
		private void PopulaDDLFabricante()
		{
			ddlFabricante.DataSource = new Fabricante().GetAll("Nome");
			ddlFabricante.DataValueField = "IdFabricante";
			ddlFabricante.DataTextField = "Nome";
			ddlFabricante.DataBind();
			
			ddlFabricante.Items.FindByValue(dadosCA.IdFabricante.Id.ToString()).Selected = true;
		}

		private void PopulaDDLEquipamento()
		{
			ddlEquipamento.DataSource = new TipoEPI().GetAll("Nome");
			ddlEquipamento.DataValueField = "IdTipoEPI";
			ddlEquipamento.DataTextField = "Nome";
			ddlEquipamento.DataBind();
			
			ddlEquipamento.Items.FindByValue(dadosCA.IdTipoEPI.Id.ToString()).Selected = true;
		}

		private void PopulaCA()
		{
			lblNumeroCAValor.Text = dadosCA.NumeroCA.ToString();
			txtFabricante.Text = dadosCA.IdFabricante.Nome.ToString();
			txtTipoEquipamento.Text = dadosCA.IdTipoEPI.Nome.ToString();
			txtdde.Text = dadosCA.DataEmissao.ToString("dd");
			txtmme.Text = dadosCA.DataEmissao.ToString("MM");
			txtaae.Text = dadosCA.DataEmissao.ToString("yyyy");
			txtddv.Text = dadosCA.Validade.ToString("dd");
			txtmmv.Text = dadosCA.Validade.ToString("MM");
			txtaav.Text = dadosCA.Validade.ToString("yyyy");
			txtObjetivo.Text = dadosCA.AprovadoEPI.ToString();
			txtDescricao.Text = dadosCA.DescricaoEPI.ToString();

            if (dadosCA.Arquivo.Trim() != "")
            {
                txt_Arq.Text = dadosCA.Arquivo.Trim();
                txt_Arq.ReadOnly = true;
            }
            else
            {
                txt_Arq.Text = "";
                txt_Arq.ReadOnly = true;
            }


        }

        protected void btnEditar_Click(object sender, System.EventArgs e)
		{
			ddlFabricante.Visible = true;
			btnNovoFabricante.Visible = true;
			txtFabricante.Visible = false;
			ddlEquipamento.Visible = true;
			btnNovoEquipamento.Visible = true;
			txtTipoEquipamento.Visible = false;
			btnGravar.Visible = true;
			lblCancel.Visible = true;
			btnEditar.Visible = false;
			txtdde.ReadOnly = false;
			txtmme.ReadOnly = false;
			txtaae.ReadOnly = false;
			txtddv.ReadOnly = false;
			txtmmv.ReadOnly = false;
			txtaav.ReadOnly = false;
			txtObjetivo.ReadOnly = false;
			txtDescricao.ReadOnly = false;
		}


        protected void btnGravarDados_Click(object sender, System.EventArgs e)
        {
            try
            {
             
                PopulaCABanco();	
                dadosCA.Save();
                dadosCA.IdFabricante.Find();
                dadosCA.IdTipoEPI.Find();


                StringBuilder st = new StringBuilder("");

                if (Request["TipoJanela"] == "Edicao")
                    st.Append("window.opener.EdicaoEPI.submit(); window.alert(\"O CA \'" + dadosCA.NumeroCA.ToString() + "\' foi editado com sucesso!\");");
                else
                    st.Append("window.alert(\"O CA \'" + dadosCA.NumeroCA.ToString() + "\' foi editado com sucesso!\");");

                ResetHTMLControls();

                InicializaWebPageObjects();


                PopulaCA();
                PopulaDDLFabricante();
                PopulaDDLEquipamento();
                btnGravar.Attributes.Add("onClick", "javascript: return VerificaData();");

               //Carregar_Imagem();

                //this.ClientScript.RegisterStartupScript(this.GetType(), "FecharJanela", st.ToString(), true);
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
            }
        }



        protected void btnGravar_Click(object sender, System.EventArgs e)
		{
            try
            {
                string xNomeArq = File1.FileName.Trim();

                //salvar prontuario
                if (xNomeArq != string.Empty)
                {

                    string xExtension = xNomeArq.Substring(File1.FileName.Length - 3, 3).ToUpper().Trim();

                    if (xExtension == "PDF" || xExtension == "JPG")
                    {
                        Cliente xCliente = new Cliente();
                        xCliente.Find(System.Convert.ToInt32(Request["IdEmpresa"].ToString()));

                        string xArq = "";

                        //if (Session["Servidor_Web"].ToString().Trim().ToUpper() == "ILITERA")
                        xArq = "I:\\fotosdocsdigitais\\" + xCliente.DiretorioPadrao.ToString().ToUpper().Trim() + "\\Prontuario\\" + xNomeArq;
                        // else
                        //    xArq = "C:\\DRIVE_I\\fotosdocsdigitais\\" + xCliente.DiretorioPadrao.ToString().ToUpper().Trim() + "\\Prontuario\\" + xNomeArq;

                        File1.SaveAs(xArq);

                        xArq = "I:\\fotosdocsdigitais\\" + xCliente.DiretorioPadrao.ToString().ToUpper().Trim() + "\\Prontuario\\" + xNomeArq;


                        dadosCA.Arquivo = xArq;

                    }

                }

                //PopulaCABanco();	
                dadosCA.Save();
                dadosCA.IdFabricante.Find();
                dadosCA.IdTipoEPI.Find();


                StringBuilder st = new StringBuilder("");

                if (Request["TipoJanela"] == "Edicao")
                    st.Append("window.opener.EdicaoEPI.submit(); window.alert(\"O CA \'" + dadosCA.NumeroCA.ToString() + "\' foi editado com sucesso!\");");
                else
                    st.Append("window.alert(\"O CA \'" + dadosCA.NumeroCA.ToString() + "\' foi editado com sucesso!\");");

                ResetHTMLControls();

                InicializaWebPageObjects();


                PopulaCA();
                PopulaDDLFabricante();
                PopulaDDLEquipamento();
                btnGravar.Attributes.Add("onClick", "javascript: return VerificaData();");

                Carregar_Imagem();

                //this.ClientScript.RegisterStartupScript(this.GetType(), "FecharJanela", st.ToString(), true);
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
            } 
		}

		private void PopulaCABanco()
		{
			dadosCA.IdFabricante.Id = Convert.ToInt32(ddlFabricante.SelectedItem.Value);
			dadosCA.IdTipoEPI.Id = Convert.ToInt32(ddlEquipamento.SelectedItem.Value);
			dadosCA.DataEmissao = new DateTime(Convert.ToInt32(txtaae.Text), Convert.ToInt32(txtmme.Text), Convert.ToInt32(txtdde.Text));
			dadosCA.Validade = new DateTime(Convert.ToInt32(txtaav.Text), Convert.ToInt32(txtmmv.Text), Convert.ToInt32(txtddv.Text));
			dadosCA.DescricaoEPI = txtDescricao.Text;
			dadosCA.AprovadoEPI = txtObjetivo.Text;
		}

		private void ResetHTMLControls()
		{
			ddlFabricante.SelectedItem.Selected = false;
			ddlFabricante.Items.FindByValue(dadosCA.IdFabricante.Id.ToString()).Selected = true;
			ddlFabricante.Visible = false;
			btnNovoFabricante.Visible = false;
			txtFabricante.Visible = true;
			ddlEquipamento.SelectedItem.Selected = false;
			ddlEquipamento.Items.FindByValue(dadosCA.IdTipoEPI.Id.ToString()).Selected = true;
			ddlEquipamento.Visible = false;
			btnNovoEquipamento.Visible = false;
			txtTipoEquipamento.Visible = true;
			btnGravar.Visible = false;
			lblCancel.Visible = false;
			btnEditar.Visible = true;
			txtdde.ReadOnly = true;
			txtmme.ReadOnly = true;
			txtaae.ReadOnly = true;
			txtddv.ReadOnly = true;
			txtmmv.ReadOnly = true;
			txtaav.ReadOnly = true;
			txtObjetivo.ReadOnly = true;
			txtDescricao.ReadOnly = true;
			txtFabricante.Text = dadosCA.IdFabricante.Nome.ToString();
			txtTipoEquipamento.Text = dadosCA.IdTipoEPI.Nome.ToString();
			txtdde.Text = dadosCA.DataEmissao.ToString("dd");
			txtmme.Text = dadosCA.DataEmissao.ToString("MM");
			txtaae.Text = dadosCA.DataEmissao.ToString("yyyy");
			txtddv.Text = dadosCA.Validade.ToString("dd");
			txtmmv.Text = dadosCA.Validade.ToString("MM");
			txtaav.Text = dadosCA.Validade.ToString("yyyy");
			txtObjetivo.Text = dadosCA.AprovadoEPI.ToString();
			txtDescricao.Text = dadosCA.DescricaoEPI.ToString();
		}

		protected void lblCancel_Click(object sender, System.EventArgs e)
		{
			ResetHTMLControls();
		}

		protected void btnNovoFabricante_Click(object sender, System.EventArgs e)
		{
			StringBuilder st = new StringBuilder();

			st.Append("void(addItemPop(centerWin('AddFabricante.aspx?TipoJanela=DetalheCA&IdFabricante=" + ddlFabricante.SelectedItem.Value + "&IdEmpresa=" + Request["IdEmpresa"] + "&IdUsuario=" + Request["IdUsuario"] + "',400,160,\'CadFabricante\')))");

            this.ClientScript.RegisterStartupScript(this.GetType(), "Fabricante", st.ToString(), true);
		}

		protected void btnNovoEquipamento_Click(object sender, System.EventArgs e)
		{
			StringBuilder st = new StringBuilder();

			st.Append("void(addItemPop(centerWin('AddEquipamento.aspx?TipoJanela=DetalheCA&IdEquipamento=" + ddlEquipamento.SelectedItem.Value + "&IdEmpresa=" + Request["IdEmpresa"] + "&IdUsuario=" + Request["IdUsuario"] + "',600,160,\'CadEquipamento\')))");

            this.ClientScript.RegisterStartupScript(this.GetType(), "Equipamento", st.ToString(), true);
		}

        protected void cmd_PDF_Click(object sender, EventArgs e)
        {
            if (lbl_Path.Text != "")
            {
                System.Net.WebClient client = new System.Net.WebClient();
                Byte[] buffer = client.DownloadData(lbl_Path.Text);
                Response.ContentType = "application/pdf";
                Response.AddHeader("Content-Disposition", "attachment; filename=" + lbl_Path.Text);
                Response.AddHeader("content-length", buffer.Length.ToString());
                Response.BinaryWrite(buffer);
                Response.End();
            }
        }

        private void Carregar_Imagem()
        {
            string xPath = txt_Arq.Text.Trim();

            if (xPath.Trim() == "")
            {
                cmd_PDF.Visible = false;
                ImgFunc.Visible = false;
            }
            else
            {

                if (xPath.ToUpper().IndexOf(".PDF") > 0)
                {
                    //Response.ContentType = "application/pdf";
                    //Response.AddHeader("content-disposition", "attachment; filename='" + Mestra.Common.Fotos.PathFoto_Uri(xPath)+"'");
                    //Response.WriteFile(Mestra.Common.Fotos.PathFoto_Uri(xPath));
                    //HttpContext.Current.ApplicationInstance.CompleteRequest();

                    //System.Diagnostics.Process.Start(Mestra.Common.Fotos.PathFoto_Uri(xPath));

                    //Response.Redirect( "ViewPDF.aspx?Arquivo=" + xPath);
                    //Response.Write("<script>window.open('ViewPDF.aspx?" + xPath + "', '_newtab');</script>");
                    //Server.Transfer("ViewPDF.aspx?Arquivo=" + xPath);

                    cmd_Imagem.Visible = false;
                    ImgFunc.Visible = false;
                    cmd_PDF.Visible = true;


                    //Cliente cliente;
                    //cliente = new Cliente(System.Convert.ToInt32(Session["Empresa"].ToString()));

                    //cliente.IdGrupoEmpresa.Find();


                    //cmd_PDF.Attributes.Add("onclick", "window.open('" + Ilitera.Common.Fotos.PathFoto_Uri(xPath) + "', '_newtab')");

                    lbl_Path.Text = Ilitera.Common.Fotos.PathFoto_Uri(xPath);

                    //Response.Redirect(Mestra.Common.Fotos.PathFoto_Uri(xPath) );

                    //Response.Write("<script>");
                    //Response.Write("<script>window.open('" + Mestra.Common.Fotos.PathFoto_Uri(xPath) + "', '_newtab');</script>");
                    //Response.Write("</script>");

                    //Page.ClientScript.RegisterStartupScript(this.GetType(), "Open PDF", "<script type='text/javascript'>window.open('" + Mestra.Common.Fotos.PathFoto_Uri(xPath) + "', '_newtab');</script>", true);

                }
                else
                {
                    cmd_PDF.Visible = false;
                    cmd_Imagem.Visible = true;
                }
            }

        }

        protected void cmd_Imagem_Click(object sender, EventArgs e)
        {

            //Cliente cliente;
            //cliente = new Cliente(System.Convert.ToInt32(Session["Empresa"].ToString()));

            //cliente.IdGrupoEmpresa.Find();


            string xPath = txt_Arq.Text.Trim();

            ImgFunc.Visible = false;
            //ImgFunc.ImageUrl = Ilitera.Common.Fotos.PathFoto_Uri(xPath, ConfigurationManager.AppSettings["Servidor_Web"].ToString());

            lbl_Path.Text = Ilitera.Common.Fotos.PathFoto_Uri(xPath);

            System.Net.WebClient client = new System.Net.WebClient();
            Byte[] buffer = client.DownloadData(lbl_Path.Text);
            Response.ContentType = "image/jpeg";
            Response.AddHeader("Content-Disposition", "attachment; filename=" + lbl_Path.Text);
            Response.AddHeader("content-length", buffer.Length.ToString());
            Response.BinaryWrite(buffer);
            Response.End();
        }



    }
}
