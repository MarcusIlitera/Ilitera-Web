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
    public partial class EquipCalibracao_Det : System.Web.UI.Page
    {

        protected System.Web.UI.HtmlControls.HtmlTableCell TDGridListaEmpresa;
        protected Ilitera.Opsa.Data.EquipamentoCalibracao xRepositorio;


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

                Controles_Manutencao(false);

                if (!xRepositorio.Id.Equals(0))
                {
                    PopulaTelaExame();
                    Carregar_Lista_Arqs();
                    Carregar_Manutencoes();
                    cmd_Add_Manutencao.Enabled = true;                    
                }
                else
                {
                    TabStrip1.Items[1].Visible = false;
                    cmd_Add_Manutencao.Enabled = false;
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

            if (Request["IdEquipamentoCalibracao"].ToString().Trim() != "0")
            {
                xRepositorio = new Ilitera.Opsa.Data.EquipamentoCalibracao();
                xRepositorio.Find(Convert.ToInt32(Request["IdEquipamentoCalibracao"]));
            }
            else
            {
                xRepositorio = new Ilitera.Opsa.Data.EquipamentoCalibracao();
                xRepositorio.Inicialize();
                xRepositorio.nId_Empr = Convert.ToInt32(Session["Empresa"]);
            }
        }


        private void RegisterClientCode()
        {
            btnExcluir.Attributes.Add("onClick", "javascript:return(confirm('Deseja realmente excluir este registro de Equipamento de Calibração?'))");
        }

        private void PopulaExame()
        {


        }

        public void PopulaTelaExame()
        {
            System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");
            txtDataAquisicao.Text = xRepositorio.Data_Aquisicao.ToString("dd/MM/yyyy", ptBr);

            txtTipoEquipamento.Text = xRepositorio.Tipo_Equipamento.Trim();
            txtEquipamento.Text = xRepositorio.Equipamento.Trim();
            txtFabricante.Text = xRepositorio.Fabricante.Trim();
            txtModelo.Text = xRepositorio.Modelo.Trim();
            txtNumeroSerie.Text = xRepositorio.Numero_Serie.Trim();
            txtLocalizacao.Text = xRepositorio.Localizacao.Trim();

            txtCertificado.Text = xRepositorio.Certificado.Trim();
            txtAssistenciaTecnica.Text = xRepositorio.Assistencia_Tecnica.Trim();
            txtPlanoManutencaoPreventivo.Text = xRepositorio.Plano_Manutencao_Preventiva.Trim();
            txtRelatorioAfericao.Text = xRepositorio.Relatorio_Afericao.Trim();

            txtPeriodicidade.Text = xRepositorio.Periodicidade_Calibracao.ToString().Trim();

            if (xRepositorio.TAG == null) txtTAG.Text = "";
            else txtTAG.Text = xRepositorio.TAG.Trim();

            if (xRepositorio.Faixa_Utilizacao == null) txtFaixaUtilizacao.Text = ""; 
            else   txtFaixaUtilizacao.Text = xRepositorio.Faixa_Utilizacao.Trim();

            if (xRepositorio.Setor == null) txtSetor.Text = "";
            else  txtSetor.Text = xRepositorio.Setor.Trim();

            if (xRepositorio.Proximo_Monitoramento == null) txtProximoMonitoramento.Text = DateTime.Now.AddYears(1).ToString("dd/MM/yyyy", ptBr);
            else txtProximoMonitoramento.Text = xRepositorio.Proximo_Monitoramento.ToString("dd/MM/yyyy", ptBr);

            if (xRepositorio.Tipo_Monitoramento == null) txtTipoMonitoramento.Text = "";
            else  txtTipoMonitoramento.Text = xRepositorio.Tipo_Monitoramento.Trim();

            if (xRepositorio.Resultado == null) txtResultado.Text = "";
            else   txtResultado.Text = xRepositorio.Resultado.Trim();


            cmb_Periodicidade.SelectedIndex = xRepositorio.IdPeriodicidade - 1;

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


            if (Validar_Data(txtDataAquisicao.Text.Trim()) == false)
            {
                return;
            }

            try
            {

                xRepositorio.Equipamento = txtEquipamento.Text;

                System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");
                xRepositorio.Data_Aquisicao = System.Convert.ToDateTime(txtDataAquisicao.Text, ptBr);

                xRepositorio.nId_Empr = Convert.ToInt32(Session["Empresa"]);

                xRepositorio.Tipo_Equipamento = txtTipoEquipamento.Text.Trim();
                xRepositorio.Equipamento = txtEquipamento.Text.Trim();
                xRepositorio.Fabricante = txtFabricante.Text.Trim();
                xRepositorio.Modelo = txtModelo.Text.Trim();
                xRepositorio.Numero_Serie = txtNumeroSerie.Text.Trim();
                xRepositorio.Localizacao = txtLocalizacao.Text.Trim();
                xRepositorio.Certificado = txtCertificado.Text.Trim();
                xRepositorio.Assistencia_Tecnica = txtAssistenciaTecnica.Text.Trim();
                xRepositorio.Plano_Manutencao_Preventiva = txtPlanoManutencaoPreventivo.Text.Trim();
                xRepositorio.Relatorio_Afericao = txtRelatorioAfericao.Text.Trim();
                xRepositorio.Periodicidade_Calibracao = System.Convert.ToInt16(txtPeriodicidade.Text);
                xRepositorio.IdPeriodicidade = System.Convert.ToInt16(cmb_Periodicidade.SelectedValue);


                xRepositorio.TAG = txtTAG.Text.Trim();
                xRepositorio.Faixa_Utilizacao = txtFaixaUtilizacao.Text.Trim();
                xRepositorio.Setor = txtSetor.Text.Trim();
                xRepositorio.Proximo_Monitoramento = System.Convert.ToDateTime( txtProximoMonitoramento.Text, ptBr);
                xRepositorio.Tipo_Monitoramento = txtTipoMonitoramento.Text.Trim();
                xRepositorio.Resultado = txtResultado.Text.Trim();


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
                st.Append("window.alert('Equipamento / Calibração foi salvo com sucesso!');");

                this.ClientScript.RegisterStartupScript(this.GetType(), "AtualizaComplementar", st.ToString(), true);

                Response.Write("<script>window.opener.document.forms[0].submit(); window.close();</script>");


                Entities.Usuario user = (Entities.Usuario)Session["usuarioLogado"];


                Response.Redirect("EquipCalibracao.aspx?IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString() + "&Tipo=3");


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

                //MsgBox1.Show("Ilitera.Net", "Confirma exclusão deste Equipamento ?", null,
                //         new EO.Web.MsgBoxButton("Delete", null, "Delete."),
                //         new EO.Web.MsgBoxButton("Cancelar", null, "Cancelar"));


                StringBuilder st = new StringBuilder();

                xRepositorio.Delete();


                st.Append("window.opener.document.getElementById('txtAuxiliar').value='atualiza';");
                st.Append("window.opener.document.forms[0].submit();");
                st.Append("window.alert('Equipamento de calibração foi deletado com sucesso!');");
                st.Append("self.close();");

                this.ClientScript.RegisterStartupScript(this.GetType(), "DeletaComplementar", st.ToString(), true);
                Response.Write("<script>window.opener.document.forms[0].submit(); window.close();</script>");

                Entities.Usuario user = (Entities.Usuario)Session["usuarioLogado"];

                Response.Redirect("EquipCalibracao.aspx?IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString() + "&Tipo=3");


            }
            catch (Exception ex)
            {
                MsgBox1.Show("Ilitera.Net", ex.Message, null,
                       new EO.Web.MsgBoxButton("OK"));
            }





        }



        private void Carregar_Manutencoes()
        {

            DataSet zDs = new Ilitera.Opsa.Data.EquipamentoCalibracao_Manutencao().Get(" IdEquipamento_Calibracao=" + xRepositorio.Id.ToString() + " ORDER BY Data_Manutencao desc ");

            grd_Clinicos.DataSource = zDs;
            grd_Clinicos.DataBind();

            if (zDs.Tables[0].Rows.Count > 0)
                grd_Clinicos.Height = 122;
            else
                grd_Clinicos.Height = 50;



        }



        private void Carregar_Lista_Arqs()
        {

            lst_Arq.Items.Clear();

            DataSet zDs = new Ilitera.Opsa.Data.EquipamentoCalibracao_Anexos().Get(" IdEquipamento_Calibracao=" + xRepositorio.Id.ToString() + " ORDER BY Arquivo ");

            for (int zCont = 0; zCont < zDs.Tables[0].Rows.Count; zCont++)
            {
                lst_Arq.Items.Add(zDs.Tables[0].Rows[zCont][2].ToString());
            }


        }




        protected void cmd_Voltar_Click(object sender, EventArgs e)
        {
            Entities.Usuario user = (Entities.Usuario)Session["usuarioLogado"];

            Response.Redirect("EquipCalibracao.aspx?IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString() + "&Tipo=3");
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

                    EquipamentoCalibracao_Anexos xAnexos = new EquipamentoCalibracao_Anexos();
                    xAnexos.IdEquipamento_Calibracao = xRepositorio.Id;
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


            EquipamentoCalibracao_Anexos xAnexos = new EquipamentoCalibracao_Anexos();
            xAnexos.Find(" IdEquipamento_Calibracao =" + xRepositorio.Id + " and Arquivo = '" + lst_Arq.Items[lst_Arq.SelectedIndex].ToString().Trim() + "' ");
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


        protected void grd_Clinicos_ItemCommand(object sender, EO.Web.GridCommandEventArgs e)
        {

            lbl_Id.Text = "0";
            txtDataManutencao.Text = "";
            txtManutencao.Text = "";

            Controles_Manutencao(false);



            if (e.CommandName.ToString().Trim() == "3")  //editar
            {
                lbl_Id.Text = e.Item.Key.ToString();

                EquipamentoCalibracao_Manutencao xManut = new EquipamentoCalibracao_Manutencao();
                xManut.Find(System.Convert.ToInt32(lbl_Id.Text));


                System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");
                txtDataManutencao.Text = xManut.Data_Manutencao.ToString("dd/MM/yyyy", ptBr);
                
                txtManutencao.Text = xManut.Manutencao_Corretiva;
                                

                Controles_Manutencao(true);

                return;
            }
            else  //excluir
            {
                lbl_Id.Text = e.Item.Key.ToString();

                MsgBox1.Show("Ilitera.Net", "Confirma exclusão deste item de manutenção ?  ( " + e.Item.Cells[1].Value.ToString().Trim().Substring(0, 10) + " )", null,
                new EO.Web.MsgBoxButton("Delete", null, "Delete"),
                new EO.Web.MsgBoxButton("Cancelar", null, "Cancelar"));

            }



        }

        protected void cmd_Add_Manutencao_Click(object sender, EventArgs e)
        {

            lbl_Id.Text = "0";

            txtDataManutencao.Text = "";
            txtManutencao.Text = "";

            Controles_Manutencao(true);

        }


        protected void Controles_Manutencao(bool xValor)
        {

            lbl_Edicao.Visible = xValor;
            lblDataManutencao.Visible = xValor;
            lblManutencao.Visible = xValor;
            txtManutencao.Visible = xValor;
            txtDataManutencao.Visible = xValor;
            cmd_Gravar_Manutencao.Visible = xValor;
            cmd_Cancelar.Visible = xValor;

            return;

        }

        protected void cmd_Gravar_Manutencao_Click(object sender, EventArgs e)
        {


            if (Validar_Data(txtDataManutencao.Text.Trim()) == false)
            {
                return;
            }


            EquipamentoCalibracao_Manutencao xManut = new EquipamentoCalibracao_Manutencao();

            //salvar
            if ( lbl_Id.Text.Trim()=="0")
            {
                xManut.IdEquipamento_Calibracao = System.Convert.ToInt32(Request["IdEquipamentoCalibracao"].ToString());
                System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");
                xManut.Data_Manutencao = System.Convert.ToDateTime(txtDataManutencao.Text, ptBr);
                xManut.Manutencao_Corretiva = txtManutencao.Text;
            }
            else
            {
                xManut.Find(System.Convert.ToInt32(lbl_Id.Text));

                System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");
                xManut.Data_Manutencao = System.Convert.ToDateTime(txtDataManutencao.Text, ptBr);
                xManut.Manutencao_Corretiva = txtManutencao.Text;
            }

            xManut.Save();



            lbl_Id.Text = "0";

            txtDataManutencao.Text = "";
            txtManutencao.Text = "";

            Controles_Manutencao(false);

            Carregar_Manutencoes();

            return;

        }




        protected void cmd_Cancelar_Click(object sender, EventArgs e)
        {

            lbl_Id.Text = "0";

            txtDataManutencao.Text = "";
            txtManutencao.Text = "";

            Controles_Manutencao(false);

            Carregar_Manutencoes();

            return;
        }


        protected void MsgBox1_ButtonClick(object sender, System.Web.UI.WebControls.CommandEventArgs e)
        {
            //Use the command name to determine which
            //button was clicked
            if (e.CommandName == "Delete")
            {
                EquipamentoCalibracao_Manutencao xManut = new EquipamentoCalibracao_Manutencao();
                xManut.Find(System.Convert.ToInt32(lbl_Id.Text));
                xManut.Delete();

                Carregar_Manutencoes();

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

