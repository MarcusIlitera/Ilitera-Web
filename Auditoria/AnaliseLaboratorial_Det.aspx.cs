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
    public partial class AnaliseLaboratorial_Det : System.Web.UI.Page
    {

        protected System.Web.UI.HtmlControls.HtmlTableCell TDGridListaEmpresa;
        protected Ilitera.Opsa.Data.Analise_Laboratorial xRepositorio;


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

                Carregar_Combos();

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
                {
                    PopulaTelaExame();
                    Carregar_Lista_Arqs();                         
                }
                else
                {
                    TabStrip1.Items[1].Visible = false;                    
                }




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

            if (Request["IdAnalise"].ToString().Trim() != "0")
            {
                xRepositorio = new Ilitera.Opsa.Data.Analise_Laboratorial();
                xRepositorio.Find(Convert.ToInt32(Request["IdAnalise"]));
            }
            else
            {
                xRepositorio = new Ilitera.Opsa.Data.Analise_Laboratorial();
                xRepositorio.Inicialize();
                xRepositorio.nId_Empr = Convert.ToInt32(Session["Empresa"]);
            }
        }


        private void RegisterClientCode()
        {
            btnExcluir.Attributes.Add("onClick", "javascript:return(confirm('Deseja realmente excluir este registro de Análise Laboratorial?'))");
        }

        private void PopulaExame()
        {


        }

        public void PopulaTelaExame()
        {
            System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");
            
            txtUltimaAnalise.Text = xRepositorio.UltimaAnalise.ToString("dd/MM/yyyy", ptBr);

            if (xRepositorio.ProximaAnalise == null) txtProximaAnalise.Text = "01/01/0001";
            else   txtProximaAnalise.Text = xRepositorio.ProximaAnalise.ToString("dd/MM/yyyy", ptBr);
                      

            if (xRepositorio.Obs == null) txtObs.Text = "";
            else   txtObs.Text = xRepositorio.Obs.Trim();

            if (xRepositorio.Resultado == null) txtResultado.Text = "";
            else   txtResultado.Text = xRepositorio.Resultado.Trim();

            txtPeriodicidade.Text = xRepositorio.PeriodicidadeAnalise.ToString().Trim();
            cmb_Periodicidade.SelectedIndex = xRepositorio.Periodicidade - 1;


            for ( int zCont =0; zCont<cmb_IdTipo.Items.Count; zCont++)
            {
                if ( cmb_IdTipo.Items[zCont].ToString().Trim() == xRepositorio.IdTipoAnalise.ToString().Trim() )
                {
                    cmb_Tipo.SelectedIndex = zCont;
                    break;
                }
            }

            for (int zCont = 0; zCont < cmb_IdManipulador.Items.Count; zCont++)
            {
                if (cmb_IdManipulador.Items[zCont].ToString().Trim() == xRepositorio.IdManipulador.ToString().Trim())
                {
                    cmb_Manipulador.SelectedIndex = zCont;
                    break;
                }
            }


            //if (xRepositorio.Anexo == null) txt_Arq.Text = "";
            //else
            //{
            //    txt_Arq.Text = xRepositorio.Anexo.ToString().Trim();
            //    Carregar_Imagem();
            //}

            return;

        }

        protected void btnOK_Click(object sender, System.EventArgs e)
        {


          
            try
            {

               

                System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");

                if (Validar_Data(txtUltimaAnalise.Text) == true)
                    xRepositorio.UltimaAnalise = System.Convert.ToDateTime(txtUltimaAnalise.Text, ptBr);
                else
                    return;

                if (Validar_Data(txtProximaAnalise.Text) == true)
                    xRepositorio.ProximaAnalise = System.Convert.ToDateTime(txtProximaAnalise.Text, ptBr);
                else
                    return;


                xRepositorio.nId_Empr = Convert.ToInt32(Session["Empresa"]);

                xRepositorio.PeriodicidadeAnalise = System.Convert.ToInt16(txtPeriodicidade.Text);
                xRepositorio.Periodicidade = System.Convert.ToInt16(cmb_Periodicidade.SelectedValue);

                xRepositorio.Obs = txtObs.Text.Trim(); 
                xRepositorio.Resultado = txtResultado.Text.Trim();

                if (System.Convert.ToInt32(cmb_IdTipo.Items[cmb_Tipo.SelectedIndex].ToString()) == 0)
                {
                    if ( txtNovoTipo.Text.Trim() == "" )
                    {
                        MsgBox1.Show("Ilitera.Net", "Preencha novo tipo.", null,new EO.Web.MsgBoxButton("OK"));
                        return;
                    }
                    else
                    {
                        for ( int zCont=0; zCont<cmb_Tipo.Items.Count;zCont++)
                        {
                            if (cmb_Tipo.Items[zCont].ToString().ToUpper().Trim() == txtNovoTipo.Text.Trim().ToUpper() )
                            {
                                xRepositorio.IdTipoAnalise = System.Convert.ToInt32(cmb_IdTipo.Items[zCont].ToString());
                                break;
                            }
                        }

                        Analise_Laboratorial_Tipo rTipo = new Analise_Laboratorial_Tipo();
                        rTipo.Descricao = txtNovoTipo.Text.Trim();
                        rTipo.Save();

                        xRepositorio.IdTipoAnalise = rTipo.Id;
                    }
                }
                else
                {
                    xRepositorio.IdTipoAnalise = System.Convert.ToInt32(cmb_IdTipo.Items[cmb_Tipo.SelectedIndex].ToString());
                }



                if (System.Convert.ToInt32(cmb_IdManipulador.Items[cmb_Manipulador.SelectedIndex].ToString()) == 0)
                {
                    if (txtNovoManipulador.Text.Trim() == "")
                    {
                        MsgBox1.Show("Ilitera.Net", "Preencha novo manipulador.", null, new EO.Web.MsgBoxButton("OK"));
                        return;
                    }
                    else
                    {
                        for (int zCont = 0; zCont < cmb_Manipulador.Items.Count; zCont++)
                        {
                            if (cmb_Manipulador.Items[zCont].ToString().ToUpper().Trim() == txtNovoManipulador.Text.Trim().ToUpper())
                            {
                                xRepositorio.IdManipulador = System.Convert.ToInt32(cmb_IdManipulador.Items[zCont].ToString());
                                break;
                            }
                        }

                        Analise_Laboratorial_Manipulador rTipo = new Analise_Laboratorial_Manipulador();
                        rTipo.Descricao = txtNovoManipulador.Text.Trim();
                        rTipo.nId_Empr = Convert.ToInt32(Session["Empresa"]);
                        rTipo.Save();

                        xRepositorio.IdManipulador = rTipo.Id;
                    }
                }
                else
                {
                    xRepositorio.IdManipulador = System.Convert.ToInt32(cmb_IdManipulador.Items[cmb_Manipulador.SelectedIndex].ToString());
                }

                

                xRepositorio.Save();


                ////salvar prontuario
                //if (File1.FileName != string.Empty)
                //{
                //    Cliente xCliente = new Cliente();
                //    xCliente.Find(System.Convert.ToInt32(Session["Empresa"].ToString()));


                //    string xArq = "";

                //    //if (Session["Servidor_Web"].ToString().Trim().ToUpper() == "ILITERA")
                //    xArq = "I:\\fotosdocsdigitais\\" + xCliente.DiretorioPadrao.ToString().ToUpper().Trim() + "\\Prontuario\\" + File1.FileName;
                //    // else                        
                //    //   xArq = "C:\\DRIVE_I\\fotosdocsdigitais\\" + xCliente.DiretorioPadrao.ToString().ToUpper().Trim() + "\\Prontuario\\" + File1.FileName;


                //    File1.SaveAs(xArq);

                //    xArq = "I:\\fotosdocsdigitais\\" + xCliente.DiretorioPadrao.ToString().ToUpper().Trim() + "\\Prontuario\\" + File1.FileName;

                //    xRepositorio.Anexo = xArq;
                //    xRepositorio.Save();

                //}

                ////Request["IdEmpresa"].ToString()  Request["IdEmpregado"]


                btnExcluir.Enabled = true;

                StringBuilder st = new StringBuilder();

                st.Append("window.opener.document.getElementById('txtAuxiliar').value='atualiza';");
                st.Append("window.opener.document.forms[0].submit();");
                st.Append("window.alert('Análise Laboratorial foi salvo com sucesso!');");

                this.ClientScript.RegisterStartupScript(this.GetType(), "AtualizaComplementar", st.ToString(), true);

                Response.Write("<script>window.opener.document.forms[0].submit(); window.close();</script>");


                Entities.Usuario user = (Entities.Usuario)Session["usuarioLogado"];


                Response.Redirect("AnaliseLaboratorial.aspx?IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString() + "&Tipo=3");


            }
            catch (Exception ex)
            {
                MsgBox1.Show("Ilitera.Net", ex.Message, null,
                       new EO.Web.MsgBoxButton("OK"));
            }
        }


        protected void MsgBox1_ButtonClick(object sender, System.Web.UI.WebControls.CommandEventArgs e)
        {
            //Use the command name to determine which
            //button was clicked
            if (e.CommandName == "Delete")
            {
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

                Response.Redirect("AnaliseLaboratorial.aspx?IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString() + "&Tipo=3");


            }
            catch (Exception ex)
            {
                MsgBox1.Show("Ilitera.Net", ex.Message, null,
                       new EO.Web.MsgBoxButton("OK"));
            }





        }




        private void Carregar_Combos()
        {

            cmb_Manipulador.Items.Clear();
            cmb_IdManipulador.Items.Clear();

            cmb_Tipo.Items.Clear();
            cmb_IdTipo.Items.Clear();

            DataSet zDs = new Ilitera.Opsa.Data.Analise_Laboratorial_Manipulador().Get(" nId_Empr = " + Session["Empresa"].ToString() + " and Descricao is not null  ORDER BY Descricao ");

            cmb_Manipulador.Items.Add("- Inserir novo Manipulador -");
            cmb_IdManipulador.Items.Add("0");

            for (int zCont = 0; zCont < zDs.Tables[0].Rows.Count; zCont++)
            {
                cmb_Manipulador.Items.Add(zDs.Tables[0].Rows[zCont][2].ToString());
                cmb_IdManipulador.Items.Add(zDs.Tables[0].Rows[zCont][0].ToString());
            }


            DataSet zDs2 = new Ilitera.Opsa.Data.Analise_Laboratorial_Tipo().Get(" Descricao is not null  ORDER BY Descricao ");

            cmb_Tipo.Items.Add("- Inserir novo Tipo -");
            cmb_IdTipo.Items.Add("0");

            for (int zCont = 0; zCont < zDs2.Tables[0].Rows.Count; zCont++)
            {
                cmb_Tipo.Items.Add(zDs2.Tables[0].Rows[zCont][1].ToString());
                cmb_IdTipo.Items.Add(zDs2.Tables[0].Rows[zCont][0].ToString());
            }

            return;

        }


        private void Carregar_Lista_Arqs()
        {

            lst_Arq.Items.Clear();

            DataSet zDs = new Ilitera.Opsa.Data.Analise_Laboratorial_Anexos().Get(" IdAnalise=" + xRepositorio.Id.ToString() + " ORDER BY Arquivo ");

            for (int zCont = 0; zCont < zDs.Tables[0].Rows.Count; zCont++)
            {
                lst_Arq.Items.Add(zDs.Tables[0].Rows[zCont][2].ToString());
            }


        }




        protected void cmd_Voltar_Click(object sender, EventArgs e)
        {
            Entities.Usuario user = (Entities.Usuario)Session["usuarioLogado"];

            Response.Redirect("AnaliseLaboratorial.aspx?IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString() + "&Tipo=3");
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
                    xPath = lst_Arq.Items[lst_Arq.SelectedIndex].ToString().Trim();


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


            string xPath = lst_Arq.Items[lst_Arq.SelectedIndex].ToString().Trim();

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




        protected void cmd_Add_Click(object sender, EventArgs e)
        {

            if (File1.FileName != string.Empty)
            {

                string xExtension = File1.FileName.Substring(File1.FileName.Length - 3, 3).ToUpper().Trim();

                if (xExtension == "PDF" || xExtension == "JPG")
                {


                    Cliente xCliente = new Cliente();
                    xCliente.Find(System.Convert.ToInt32(Session["Empresa"].ToString()));


                    string xArq = "";

                    xArq = "I:\\fotosdocsdigitais\\" + xCliente.DiretorioPadrao.ToString().ToUpper().Trim() + "\\VasoPressao\\" + File1.FileName;

                    File1.SaveAs(xArq);

                    xArq = "I:\\fotosdocsdigitais\\" + xCliente.DiretorioPadrao.ToString().ToUpper().Trim() + "\\VasoPressao\\" + File1.FileName;

                    Analise_Laboratorial_Anexos xAnexos = new Analise_Laboratorial_Anexos();
                    xAnexos.IdAnalise = xRepositorio.Id;
                    xAnexos.Arquivo = xArq;
                    xAnexos.Save();

                    Carregar_Lista_Arqs();

                }
            }


        }

        protected void cmd_Remove_Click(object sender, EventArgs e)
        {

            if (lst_Arq.SelectedIndex < 0)
            {
                MsgBox1.Show("Ilitera.Net", "Selecione arquivo a ser excluído", null,
                       new EO.Web.MsgBoxButton("OK"));
                return;
            }


            Analise_Laboratorial_Anexos xAnexos = new Analise_Laboratorial_Anexos();
            xAnexos.Find(" IdAnalise =" + xRepositorio.Id + " and Arquivo = '" + lst_Arq.Items[lst_Arq.SelectedIndex].ToString().Trim() + "' ");
            xAnexos.Delete();


            Carregar_Lista_Arqs();

            return;


        }

        protected void lst_Arq_SelectedIndexChanged(object sender, EventArgs e)
        {

            ImgFunc.Visible = false;

            if (lst_Arq.SelectedIndex < 0)
            {
                cmd_PDF.Enabled = false;
                cmd_Imagem.Enabled = false;
                return;
            }

            string xPath = lst_Arq.Items[lst_Arq.SelectedIndex].ToString().Trim();


            if (xPath.ToUpper().IndexOf(".PDF") > 0)
            {
                cmd_PDF.Enabled = true;
                cmd_Imagem.Enabled = false;
                cmd_PDF.Attributes.Add("onclick", "window.open('" + Ilitera.Common.Fotos.PathFoto_Uri(xPath) + "', '_newtab')");

            }
            else
            {
                cmd_PDF.Enabled = false;
                cmd_Imagem.Enabled = true;
            }



        }


      
       
     



    
     


        protected Boolean Validar_Data(string zData)
        {
            int zDia = 0;
            int zMes = 0;
            int zAno = 0;

            string Validar;
            bool isNumerical;
            int myInt;


            if (zData.Length != 10)
            {
                MsgBox1.Show("Ilitera.Net", "Data Inválida.  Utilizar formato dd/MM/yyyy", null,
                new EO.Web.MsgBoxButton("OK"));
                return false;
            }

            Validar = zData.Substring(0, 2);
            isNumerical = int.TryParse(Validar, out myInt);
            if (isNumerical == false)
            {
                MsgBox1.Show("Ilitera.Net", "Dia Inválido.  Utilizar formato dd/MM/yyyy", null,
                new EO.Web.MsgBoxButton("OK"));
                return false;
            }


            Validar = zData.Substring(3, 2);
            isNumerical = int.TryParse(Validar, out myInt);
            if (isNumerical == false)
            {
                MsgBox1.Show("Ilitera.Net", "Mês Inválido.  Utilizar formato dd/MM/yyyy", null,
                new EO.Web.MsgBoxButton("OK"));
                return false;
            }


            Validar = zData.Substring(6, 4);
            isNumerical = int.TryParse(Validar, out myInt);
            if (isNumerical == false)
            {
                MsgBox1.Show("Ilitera.Net", "Ano Inválido.  Utilizar formato dd/MM/yyyy", null,
                new EO.Web.MsgBoxButton("OK"));
                return false;
            }


            if (zData.Substring(2, 1) != "/" || zData.Substring(5, 1) != "/")
            {
                MsgBox1.Show("Ilitera.Net", "Data Inválida.  Utilizar formato dd/MM/yyyy", null,
                new EO.Web.MsgBoxButton("OK"));
                return false;
            }


            zDia = System.Convert.ToInt32(zData.Substring(0, 2));
            zMes = System.Convert.ToInt32(zData.Substring(3, 2));
            zAno = System.Convert.ToInt32(zData.Substring(6, 4));

            if (zAno < 1900 || zAno > 2025)
            {
                MsgBox1.Show("Ilitera.Net", "Ano Inválido.  Utilizar formato dd/MM/yyyy", null,
                new EO.Web.MsgBoxButton("OK"));
                return false;
            }

            if (zMes < 1 || zMes > 12)
            {
                MsgBox1.Show("Ilitera.Net", "Mês Inválido.  Utilizar formato dd/MM/yyyy", null,
                new EO.Web.MsgBoxButton("OK"));
                return false;
            }

            if (zMes == 1 || zMes == 3 || zMes == 5 || zMes == 7 || zMes == 8 || zMes == 10 || zMes == 12)
            {
                if (zDia < 1 || zDia > 31)
                {
                    MsgBox1.Show("Ilitera.Net", "Dia Inválido.  Utilizar formato dd/MM/yyyy", null,
                    new EO.Web.MsgBoxButton("OK"));
                    return false;
                }
            }
            else if (zMes == 4 || zMes == 6 || zMes == 9 || zMes == 11)
            {
                if (zDia < 1 || zDia > 30)
                {
                    MsgBox1.Show("Ilitera.Net", "Dia Inválido.  Utilizar formato dd/MM/yyyy", null,
                    new EO.Web.MsgBoxButton("OK"));
                    return false;
                }
            }
            else
            {
                if (zDia < 1 || zDia > 29)
                {
                    MsgBox1.Show("Ilitera.Net", "Dia Inválido.  Utilizar formato dd/MM/yyyy", null,
                    new EO.Web.MsgBoxButton("OK"));
                    return false;
                }
            }

            return true;

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
    }
}

