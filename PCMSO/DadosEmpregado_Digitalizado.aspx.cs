using System;
using System.Data;
using System.Text;
using Ilitera.Opsa.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using System.Collections;
using System.Collections.Generic;
using Ilitera.Common;

using Entities;
using BLL;


namespace Ilitera.Net.PCMSO
{
    public partial class DadosEmpregado_Digitalizado : System.Web.UI.Page
    {
        protected Prestador prestador = new Prestador();
        protected Ilitera.Common.Usuario usuario = new Ilitera.Common.Usuario();
        protected Cliente cliente = new Cliente();
        protected Ilitera.Opsa.Data.Empregado empregado = new Ilitera.Opsa.Data.Empregado();


		protected void Page_Load(object sender, System.EventArgs e)
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

			InicializaWebPageObjects();
			//PreencheLabels("lblEmp", cliente.NomeAbreviado.ToString());
            
            
			PopulaTelaEmpregado();
			if (!IsPostBack)
			{

                ImgFunc.Visible = false;
                PopularFindDigitalizados();
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

		protected void InicializaWebPageObjects()
		{
			//base.InicializaWebPageObjects();
			StringBuilder st = new StringBuilder();

			//st.Append("top.window.document.frames.conteudo.window.document.getElementById('txtIdEmpregado').value = '" + empregado.Id + "';");
            st.Append("top.window.document.getElementById('txtIdEmpregado').value = '" + Request["IdEmpregado"] + "';");
            st.Append("top.window.document.getElementById('txtIdUsuario').value = '" + Request["IdUsuario"] + "';");
            st.Append("top.window.document.getElementById('txtIdEmpresa').value = '" + Request["IdEmpresa"] + "';");
                            
            this.ClientScript.RegisterStartupScript(this.GetType(), "IdEmpregado", st.ToString(), true);
            //btnFichaCompleta.Attributes.Add("onClick", "addItem(centerWin('../DadosEmpresa/FichaCompleta.aspx?IdEmpregado=" + Request["IdEmpregado"] + "&IdEmpresa=" + Request["IdEmpresa"] + "&IdUsuario=" + Request["IdUsuario"] + "',560,320,\'FichaCompleta\'),\'Todos\'); Reload();");
                        
		}




        private void PopularFindDigitalizados()
        {
            int xCont = 1;

            Entities.Usuario xUser = (Entities.Usuario)Session["usuarioLogado"];

            Ilitera.Common.Pessoa zPessoa = new Ilitera.Common.Pessoa();
            zPessoa.Find(xUser.IdPessoa);

            Prestador xPrestador = new Prestador();

            xPrestador = new Prestador();
            xPrestador.FindByPessoa(zPessoa);
            xPrestador.IdPessoa.Find();

            ArrayList list;


            if (xPrestador.IndTipoPrestador == (int)TipoPrestador.Medico)
            {
                list = new ProntuarioDigital().Find("IdEmpregado=" + Request["IdEmpregado"].ToString() + " ORDER BY DataProntuario DESC");
            }
            //liberar visualização de todos exames para Ilitera e Leila
            else if (zPessoa.NomeAbreviado.ToUpper().IndexOf("ILITERA") >= 0 || zPessoa.NomeAbreviado.ToUpper() == "LEILA")
            {
                list = new ProntuarioDigital().Find("IdEmpregado=" + Request["IdEmpregado"].ToString() + " ORDER BY DataProntuario DESC");
            }
            else
            {
                if (Ilitera.Data.SQLServer.EntitySQLServer._LocalServer.ToString().ToUpper().IndexOf("EY") > 0)
                {
                    //list = new ProntuarioDigital().Find("IdEmpregado=" + Request["IdEmpregado"].ToString() + " AND ( INDTIPODOCUMENTO NOT IN ( 0,2,7 )  OR DESCRICAO LIKE '%PCD%' ) "
                    // + " ORDER BY DataProntuario DESC");
                    list = new ProntuarioDigital().Find("IdEmpregado=" + Request["IdEmpregado"].ToString() + " AND ( INDTIPODOCUMENTO NOT IN ( 0,2,7 )  AND DESCRICAO NOT LIKE '%PCD%' ) "
                    + " ORDER BY DataProntuario DESC");

                }
                else
                {
                    list = new ProntuarioDigital().Find("IdEmpregado=" + Request["IdEmpregado"].ToString() + " AND INDTIPODOCUMENTO NOT IN ( 0,2,7 ) "
                     + " ORDER BY DataProntuario DESC");
                }
            }

            cmd_PDF.Visible = false;
            cmd_Imagem.Visible = false;

            cmb_Clinicas.Items.Clear();
            cmb_Clinicas.Items.Add("  -  ");
            

            cmb_Imagem.Items.Clear();
            cmb_Imagem.Items.Add("");


            foreach (ProntuarioDigital prontDigital in list)
            {
                cmb_Clinicas.Items.Add( xCont.ToString() + ". " + prontDigital.ToString());
                cmb_Imagem.Items.Add(prontDigital.Arquivo.ToString());
                xCont++;
            }

            if (cmb_Clinicas.Items.Count > 0)
            {
                cmb_Clinicas.SelectedIndex = 0;
                Carregar_Imagem(cmb_Imagem.Items[cmb_Clinicas.SelectedIndex].ToString());
            }

        }



        private void Carregar_Imagem(string xPath)
        {
            if (xPath.Trim() == "")
            {
                cmd_PDF.Visible = false;
                cmd_Imagem.Visible = false;
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

                    ImgFunc.Visible = false;
                    cmd_PDF.Visible = true;
                    cmd_Imagem.Visible = false;

                    //Cliente cliente;
                    //cliente = new Cliente(System.Convert.ToInt32(Session["Empresa"].ToString()));

                    //cliente.IdGrupoEmpresa.Find();

                     

                    //if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().IndexOf("Prajna") >= 0 && cliente.IdGrupoEmpresa.Id != -905238295 && cliente.IdGrupoEmpresa.Id != -682029520 && cliente.IdGrupoEmpresa.Id != -768119567 && cliente.IdGrupoEmpresa.Id != 1577634488 && cliente.IdGrupoEmpresa.Id != 2021173001 && cliente.IdGrupoEmpresa.Id != 1792930616 && cliente.IdGrupoEmpresa.Id != 1372398484 && cliente.IdGrupoEmpresa.Descricao.IndexOf("Security")<0)
                    //    cmd_PDF.Attributes.Add("onclick", "window.alert('Estamos em manutenção! Por favor contatar a central de atendimento para maiores informações')");
                    //else

                    //cmd_PDF.Attributes.Add("onclick", "window.open('file:////" + Ilitera.Common.Fotos.PathFoto_Uri(xPath).Replace("\\","/" ) + "', '_newtab')");

                    lbl_Path.Text = Ilitera.Common.Fotos.PathFoto_Uri(xPath);



                    //Response.Redirect(Mestra.Common.Fotos.PathFoto_Uri(xPath) );

                    //Response.Write("<script>");
                    //Response.Write("<script>window.open('" + Mestra.Common.Fotos.PathFoto_Uri(xPath) + "', '_newtab');</script>");
                    //Response.Write("</script>");

                    //Page.ClientScript.RegisterStartupScript(this.GetType(), "Open PDF", "<script type='text/javascript'>window.open('" + Mestra.Common.Fotos.PathFoto_Uri(xPath) + "', '_newtab');</script>", true);

                }
                else
                {
                    //Cliente cliente;
                    //cliente = new Cliente(System.Convert.ToInt32(Session["Empresa"].ToString()));

                    //cliente.IdGrupoEmpresa.Find();

                    // if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().IndexOf("Prajna") >= 0 && cliente.IdGrupoEmpresa.Id != -905238295 && cliente.IdGrupoEmpresa.Id != -682029520 && cliente.IdGrupoEmpresa.Id != -768119567 && cliente.IdGrupoEmpresa.Id != 1577634488)
                    //    return;

                    lbl_Path.Text = "";

                    cmd_Imagem.Visible = true;
                    cmd_PDF.Visible = false;

                    //ImgFunc.Visible = true;

                    lbl_Path.Text = Ilitera.Common.Fotos.PathFoto_Uri(xPath);


                    //ImgFunc.ImageUrl = Ilitera.Common.Fotos.PathFoto_Uri( xPath);  //, ConfigurationManager.AppSettings["Servidor_Web"].ToString());

                }
            }

        }




        protected void cmb_Clinicas_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb_Clinicas.SelectedIndex >= 0)
            {
                Carregar_Imagem(cmb_Imagem.Items[cmb_Clinicas.SelectedIndex].ToString());
            }
            else
            {
                Carregar_Imagem("");
            }
        }


		private void PopulaTelaEmpregado()
		{
            //variável empregado está vazia.  Ver como carregá-lo. - Wagner  
            empregado.Find(" tblEMPREGADO.nID_EMPREGADO = " + Request["IdEmpregado"].ToString());
			EmpregadoFuncao empregadoFuncao = EmpregadoFuncao.GetEmpregadoFuncao(empregado);
            
			lblValorNome.Text = empregado.tNO_EMPG;

			
			if (empregado.hDT_ADM.ToString("dd-MM-yyyy").Equals("01-01-1753") || empregado.hDT_ADM == new DateTime())
				lblValorAdmissao.Text = "&nbsp;";
			else
				lblValorAdmissao.Text = empregado.hDT_ADM.ToString("dd-MM-yyyy");

			if (empregado.IdadeEmpregado() != 0)
				lblValorIdade.Text = empregado.IdadeEmpregado().ToString();
			else
				lblValorIdade.Text = "&nbsp;";
			
			if (empregado.tSEXO.Trim() != "" && empregado.tSEXO != "S")
				if (empregado.tSEXO == "M")
                    lblValorSexo.Text = "Masculino";
				else if (empregado.tSEXO == "F")
					lblValorSexo.Text = "Feminino";
			else
				lblValorSexo.Text = "&nbsp;";

			if (empregado.hDT_NASC.ToString("dd-MM-yyyy").Equals("01-01-1753") || empregado.hDT_NASC == new DateTime())
				lblValorNasc.Text = "&nbsp;";
			else
				lblValorNasc.Text = empregado.hDT_NASC.ToString("dd-MM-yyyy");
			
			if (empregado.hDT_DEM.ToString("dd-MM-yyyy").Equals("01-01-1753") || empregado.hDT_DEM == new DateTime())
				lblValorDemissao.Text = "&nbsp;";
			else
				lblValorDemissao.Text = empregado.hDT_DEM.ToString("dd-MM-yyyy");

			

			if (empregadoFuncao.hDT_INICIO == new DateTime() || empregadoFuncao.hDT_INICIO == new DateTime(1753, 1, 1))
				lblValorDataIni.Text = "&nbsp;";
			else
				lblValorDataIni.Text = empregadoFuncao.hDT_INICIO.ToString("dd-MM-yyyy");
		}

      
        protected void cmd_Voltar_Click(object sender, EventArgs e)
        {
            Entities.Usuario user = (Entities.Usuario)Session["usuarioLogado"];
            Response.Redirect("~\\ListaEmpregados.aspx?&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString());
        }

        protected void btnemp_Click(object sender, EventArgs e)
        {

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

        protected void cmd_Imagem_Click(object sender, EventArgs e)
        {
            if (lbl_Path.Text != "")
            {
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

}
