using System;
using System.Data;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using Ilitera.Common;
using Ilitera.Opsa.Data;
using System.Globalization;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Configuration;


namespace Ilitera.Net
{
    public partial class RepositorioEPI : System.Web.UI.Page
    {

        protected System.Web.UI.HtmlControls.HtmlTableCell TDGridListaEmpresa;
        protected Ilitera.Opsa.Data.EPIRepositorio xRepositorio;
        

        protected Prestador prestador = new Prestador();
        protected Usuario usuario = new Usuario();
        protected Cliente cliente = new Cliente();
        protected Empregado empregado = new Empregado();

        protected void Page_Load(object sender, System.EventArgs e)
        {
            InicializaWebPageObjects();
            string xUsuario = Session["usuarioLogado"].ToString();


            if (!IsPostBack)
            {

                string zAcao = Request["Acao"].ToString().Trim();

                if (zAcao == "E")
                {
                    btnOK.Visible = true;
                    btnExcluir.Visible = true;
                }
                else
                {
                    btnOK.Visible = false;
                    btnExcluir.Visible = false;
                }


                RegisterClientCode();



                Entities.Usuario usuario = (Entities.Usuario)Session["usuarioLogado"];
                prestador.Find(" IdPessoa = " + usuario.IdPessoa.ToString());

                prestador.IdJuridica.Find();
                

                if (!xRepositorio.Id.Equals(0))
                    PopulaTelaExame();




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

            if (Request["IdEPIRepositorio"].ToString().Trim() != "0")
            {
                xRepositorio = new Ilitera.Opsa.Data.EPIRepositorio();
                xRepositorio.Find(Convert.ToInt32(Request["IdEPIRepositorio"]));
            }
            else
            {
                xRepositorio = new Ilitera.Opsa.Data.EPIRepositorio();
                xRepositorio.Inicialize();
                xRepositorio.IdEmpregado = Convert.ToInt32(Request["IdEmpregado"]);
            }
        }


        private void RegisterClientCode()
        {
            btnExcluir.Attributes.Add("onClick", "javascript:return(confirm('Deseja realmente excluir este registro de ficha de EPI?'))");
        }

        private void PopulaExame()
        {


        }

        public void PopulaTelaExame()
        {
            System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");
            wdtDataExame.Text = xRepositorio.DataHora.ToString("dd/MM/yyyy", ptBr);

            txtDescricao.Text = xRepositorio.Descricao.ToString().Trim();

           

            if (xRepositorio.Anexo == null) txt_Arq.Text = "";
            else
            {
                txt_Arq.Text = xRepositorio.Anexo.ToString().Trim();
                Carregar_Imagem();
            }

            return;

        }

        protected void btnOK_Click(object sender, System.EventArgs e)
        {
            try
            {

                xRepositorio.Descricao = txtDescricao.Text;

                System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");
                xRepositorio.DataHora = System.Convert.ToDateTime(wdtDataExame.Text, ptBr);

               
                xRepositorio.Save();


                //salvar prontuario
                if (File1.FileName != string.Empty)
                {
                    string xExtension = File1.FileName.Substring(File1.FileName.Length - 3, 3).ToUpper().Trim();

                    if (xExtension == "PDF" || xExtension == "JPG")
                    {

                        Cliente xCliente = new Cliente();
                        xCliente.Find(System.Convert.ToInt32(Session["Empresa"].ToString()));


                        string xArq = "";

                        //if (Session["Servidor_Web"].ToString().Trim().ToUpper() == "ILITERA")
                        xArq = "I:\\fotosdocsdigitais\\" + xCliente.DiretorioPadrao.ToString().ToUpper().Trim() + "\\Prontuario\\" + File1.FileName;
                        // else                        
                        //   xArq = "C:\\DRIVE_I\\fotosdocsdigitais\\" + xCliente.DiretorioPadrao.ToString().ToUpper().Trim() + "\\Prontuario\\" + File1.FileName;


                        File1.SaveAs(xArq);

                        xArq = "I:\\fotosdocsdigitais\\" + xCliente.DiretorioPadrao.ToString().ToUpper().Trim() + "\\Prontuario\\" + File1.FileName;

                        xRepositorio.Anexo = xArq;
                        xRepositorio.Save();

                    }

                }

                //Request["IdEmpresa"].ToString()  Request["IdEmpregado"]


                btnExcluir.Enabled = true;

                StringBuilder st = new StringBuilder();

                st.Append("window.opener.document.getElementById('txtAuxiliar').value='atualiza';");
                st.Append("window.opener.document.forms[0].submit();");
                st.Append("window.alert('Dados de Ficha de EPI foram salvos com sucesso!');");

                this.ClientScript.RegisterStartupScript(this.GetType(), "AtualizaComplementar", st.ToString(), true);

                Response.Write("<script>window.opener.document.forms[0].submit(); window.close();</script>");


                Entities.Usuario user = (Entities.Usuario)Session["usuarioLogado"];


                Response.Redirect("EPIRepositorio.aspx?IdEmpregado=" + Request["IdEmpregado"].ToString() + "&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString() + "&Tipo=3");


            }
            catch (Exception ex)
            {
                MsgBox1.Show("Ilitera.Net", ex.Message, null,
                       new EO.Web.MsgBoxButton("OK"));
            }
        }

        protected void btnExcluir_Click(object sender, System.EventArgs e)
        {
            
            try
            {
                StringBuilder st = new StringBuilder();

                xRepositorio.Delete();


                st.Append("window.opener.document.getElementById('txtAuxiliar').value='atualiza';");
                st.Append("window.opener.document.forms[0].submit();");
                st.Append("window.alert('Registro de ficha de EPI foi deletado com sucesso!');");
                st.Append("self.close();");

                this.ClientScript.RegisterStartupScript(this.GetType(), "DeletaComplementar", st.ToString(), true);
                Response.Write("<script>window.opener.document.forms[0].submit(); window.close();</script>");

                Entities.Usuario user = (Entities.Usuario)Session["usuarioLogado"];

                Response.Redirect("EPIRepositorio.aspx?IdEmpregado=" + Request["IdEmpregado"].ToString() + "&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString() + "&Tipo=3");


            }
            catch (Exception ex)
            {
                MsgBox1.Show("Ilitera.Net", ex.Message, null,
                       new EO.Web.MsgBoxButton("OK"));
            }





        }


     


	

	

        protected void cmd_Voltar_Click(object sender, EventArgs e)
        {
            Entities.Usuario user = (Entities.Usuario)Session["usuarioLogado"];

            Response.Redirect("EPIRepositorio.aspx?IdEmpregado=" + Request["IdEmpregado"].ToString() + "&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString() + "&Tipo=3");
            return;

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

        protected void cmd_Abrir_PDF_Click(object sender, EventArgs e)
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

    }
}
