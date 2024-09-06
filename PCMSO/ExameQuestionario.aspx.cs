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

//using MestraNET;

namespace Ilitera.Net
{
    public partial class ExameQuestionario : System.Web.UI.Page
    {

        private Questionario exame;

        //private string tipo;

        protected Prestador prestador = new Prestador();
        protected Usuario usuario = new Usuario();
        protected Cliente cliente = new Cliente();
        protected Empregado empregado = new Empregado();

        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);


        }

        protected void Page_Load(object sender, System.EventArgs e)
        {

            string xUsuario = Session["usuarioLogado"].ToString();
            InicializaWebPageObjects();


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


                // textbox multiline não obedece o maxlenght,  colocando o código abaixo funciona
                wneEspecialista.Attributes["maxlength"] = "50";
                wneOrientacao.Attributes["maxlength"] = "200";
                txtObs.Attributes["maxlength"] = "500";



                rd_Agudo_Complicacao.Checked = true;
                rd_Agudo_sem_Complicacao.Checked = false;
                rd_Cronico.Checked = false;

                Ajuste_Controles();


                

                //if ( ddlCID.Items.Count<= 1)              Carregar_CID();


                Ilitera.Data.Clientes_Funcionarios xLista = new Ilitera.Data.Clientes_Funcionarios();

                DataSet zDs = new DataSet();

                zDs = xLista.Gerar_Lista_ExamesMedicos(Convert.ToInt32(Request.QueryString["IdEmpregado"]));

                gridExames.DataSource = zDs;
                gridExames.DataBind();



                Session["txtAuxiliar"] = string.Empty;
                RegisterClientCode();

                if (!exame.Id.Equals(0))
                {
                    Carregar_CID();
                    PopulaTelaExame();
                }



                if (exame.Id == 0)
                {

                }
                else
                {

                }



            }
            else
            {


                txtAuxiliar.Value = string.Empty;
                Session["txtAuxiliar"] = string.Empty;
            }
        }

        protected void InicializaWebPageObjects()
        {
            //base.InicializaWebPageObjects();

            if (Request["IdExame"] != null && Request["IdExame"] != "")
            {
                exame = new Questionario();
                exame.Find(Convert.ToInt32(Request["IdExame"]));
                //tipo = "atualizado";
            }
            else if (ViewState["IdExameNaoOcupacional"] != null)
            {
                exame = new Questionario();
                exame.Find(Convert.ToInt32(ViewState["IdExameNaoOcupacional"]));
                //tipo = "atualizado";
            }
            else
            {
                exame = new Questionario();

                exame.IdEmpregado = System.Convert.ToInt32(Request["IdEmpregado"].ToString());


                Entities.Usuario xusuario = (Entities.Usuario)Session["usuarioLogado"];
                prestador.Find(" IdPessoa = " + xusuario.IdPessoa.ToString());



                //tipo = "cadastrado";
                btnExcluir.Enabled = false;

            }

        }




        private void RegisterClientCode()
        {
            btnExcluir.Attributes.Add("onClick", "javascript:return(confirm('Deseja realmente excluir esta Entrevista?'))");
        }

        private void PopulaExame()
        {


            System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");
            exame.Data_Questionario = System.Convert.ToDateTime(wdtDataExame.Text.Trim(), ptBr);
            exame.Obs = txtObs.Text.Trim();


            //if (!cliente.ContrataPCMSO.Equals((int)TipoPcmsoContratada.NaoContratada))
            //{
            exame.Tipo_Orientacao = wneOrientacao.Text.Trim();
            exame.Especialista = wneEspecialista.Text.Trim();

            //exame.IdCID = System.Convert.ToInt32( ddlCID.SelectedValue );

            if (txtCID.Text.Trim() != "" && lbl_Id1.Text.Trim() != "")
            {
                exame.IdCID = System.Convert.ToInt32(lbl_Id1.Text);                 //Convert.ToInt32(ddlCID.SelectedValue);
                if (txtCID2.Text.Trim() != "" && lbl_Id2.Text.Trim() != "")
                    exame.IdCID2 = System.Convert.ToInt32(lbl_Id2.Text);
                if (txtCID3.Text.Trim() != "" && lbl_Id2.Text.Trim() != "")
                    exame.IdCID3 = System.Convert.ToInt32(lbl_Id3.Text);
                if (txtCID4.Text.Trim() != "" && lbl_Id4.Text.Trim() != "")
                    exame.IdCID4 = System.Convert.ToInt32(lbl_Id4.Text);
            }



            exame.Retorno_Dias = System.Convert.ToInt16(wndRetorno.Text.Trim());


            if (rd_Alto_Sim.Checked == true) exame.Tratamento_Alto_Custo = true;
            else exame.Tratamento_Alto_Custo = false;


            exame.Status = System.Convert.ToInt16(cmb_Status.SelectedValue);

            exame.IdProgramaSaude = System.Convert.ToInt32(cmb_Programa.SelectedValue);


            if (chk_Atualizar.Checked == true) exame.Atualizar_Nao_Critico = true;
            else exame.Atualizar_Nao_Critico = false;


            if (rd_Agudo_Complicacao.Checked == true)
            {
                exame.IdClassificacao = 1;
            }
            else if (rd_Agudo_sem_Complicacao.Checked == true)
            {
                exame.IdClassificacao = 2;
            }
            else
            {
                exame.IdClassificacao = 3;
            }
            //}            
        }



        public void PopulaTelaExame()
        {

            wdtDataExame.Text = exame.Data_Questionario.ToString("dd/MM/yyyy");
            txtObs.Text = exame.Obs.Trim();

            //if (!cliente.ContrataPCMSO.Equals((int)TipoPcmsoContratada.NaoContratada))
            //{
            wneOrientacao.Text = exame.Tipo_Orientacao.Trim();
            wneEspecialista.Text = exame.Especialista.Trim();
            //ddlCID.Text = exame.IdCID.ToString();
            wndRetorno.Text = exame.Retorno_Dias.ToString();

            if (exame.Atualizar_Nao_Critico == true) chk_Atualizar.Checked = true;
            else chk_Atualizar.Checked = false;


            if (exame.Tratamento_Alto_Custo == true)
            {
                rd_Alto_Nao.Checked = false;
                rd_Alto_Sim.Checked = true;
            }
            else
            {
                rd_Alto_Nao.Checked = true;
                rd_Alto_Sim.Checked = false;
            }


            Carregar_CID();

            CID xCid = new CID();
            xCid.Find(exame.IdCID);

            if (xCid.Id != 0)
            {
                txtCID.Text = xCid.Descricao.Trim();
                lbl_Id1.Text = xCid.Id.ToString().Trim();
            }

            xCid.Find(exame.IdCID2);

            if (xCid.Id != 0)
            {
                txtCID2.Text = xCid.Descricao.Trim();
                lbl_Id2.Text = xCid.Id.ToString().Trim();
            }


            xCid.Find(exame.IdCID3);

            if (xCid.Id != 0)
            {
                txtCID3.Text = xCid.Descricao.Trim();
                lbl_Id3.Text = xCid.Id.ToString().Trim();
            }


            xCid.Find(exame.IdCID4);

            if (xCid.Id != 0)
            {
                txtCID4.Text = xCid.Descricao.Trim();
                lbl_Id4.Text = xCid.Id.ToString().Trim();
            }





            object xSender = new object();
            EventArgs xE = new EventArgs();
            btnProcurar_Click(xSender, xE);


            


            ddlCID.SelectedIndex = 0;



            try
            {
                cmb_Programa.Items.FindByValue(exame.IdProgramaSaude.ToString()).Selected = true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                cmb_Programa.SelectedIndex = 0;
            }

            try
            {
                cmb_Status.Items.FindByValue(exame.Status.ToString()).Selected = true;
            }
            catch (Exception ex)
            {
                cmb_Status.SelectedIndex = 0;
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }





            if (exame.IdClassificacao == 1)
            {
                rd_Agudo_Complicacao.Checked = true;
                rd_Agudo_sem_Complicacao.Checked = false;
                rd_Cronico.Checked = false;
            }
            else if (exame.IdClassificacao == 2)
            {
                rd_Agudo_sem_Complicacao.Checked = true;
                rd_Agudo_Complicacao.Checked = false;
                rd_Cronico.Checked = false;
            }
            else
            {
                rd_Agudo_sem_Complicacao.Checked = false;
                rd_Agudo_Complicacao.Checked = false;
                rd_Cronico.Checked = true;
            }

            //ajustar ativação dos controles
            Ajuste_Controles();
            


            //}			
        }

        protected void btnOK_Click(object sender, System.EventArgs e)
        {
            try
            {

                System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");

                if (wdtDataExame.Text.Trim() == "")
                {
                    wdtDataExame.Text = DateTime.Now.ToString("dd/MM/yyyy", ptBr);
                }

                if (wndRetorno.Text.Trim() == "") wndRetorno.Text = "0";
                
                if (cmb_Status.SelectedIndex < 1)
                {
                    MsgBox1.Show("Ilitera.Net", "Selecione Status.", null,
                           new EO.Web.MsgBoxButton("OK"));
                    return;
                }


                if ( txtObs.Text.Trim() == "")
                {
                    MsgBox1.Show("Ilitera.Net", "Preencha a recomendação médica.", null,
                           new EO.Web.MsgBoxButton("OK"));
                    return;
                }


                if (rd_Agudo_Complicacao.Checked == true)
                {

                    if ( ddlCID.SelectedIndex < 0)
                    {
                        MsgBox1.Show("Ilitera.Net", "Selecione CID.", null,
                               new EO.Web.MsgBoxButton("OK"));
                        return;
                    }

                    if ( wneEspecialista.Text.Trim() == "")
                    {
                        if (cmb_Programa.SelectedIndex < 1)
                        {
                            MsgBox1.Show("Ilitera.Net", "Preencha com dados do especialista ou selecione programa de saúde.", null,
                               new EO.Web.MsgBoxButton("OK"));
                            return;
                        }
                    }

                    if (wneOrientacao.Text.Trim() == "")
                    {
                        MsgBox1.Show("Ilitera.Net", "Preencha com dados de orientação.", null,
                               new EO.Web.MsgBoxButton("OK"));
                        return;
                    }

                    if (cmb_Programa.SelectedIndex < 1)
                    {
                        if (wneEspecialista.Text.Trim() == "")
                        {
                            MsgBox1.Show("Ilitera.Net", "Selecione programa de saúde ou preencha dados de especialista.", null,
                               new EO.Web.MsgBoxButton("OK"));
                            return;
                        }
                    }                    

                }
                else if (rd_Agudo_sem_Complicacao.Checked == true)
                {

                    if (ddlCID.SelectedIndex < 0)
                    {
                        MsgBox1.Show("Ilitera.Net", "Selecione CID.", null,
                               new EO.Web.MsgBoxButton("OK"));
                        return;
                    }
                                      
                    if (wneOrientacao.Text.Trim() == "")
                    {
                        MsgBox1.Show("Ilitera.Net", "Preencha com dados de orientação.", null,
                               new EO.Web.MsgBoxButton("OK"));
                        return;
                    }
                 


                }
                else if (rd_Cronico.Checked == true)
                {

                    if (ddlCID.SelectedIndex < 0)
                    {
                        MsgBox1.Show("Ilitera.Net", "Selecione CID.", null,
                               new EO.Web.MsgBoxButton("OK"));
                        return;
                    }

                    if (wneEspecialista.Text.Trim() == "")
                    {
                        if (cmb_Programa.SelectedIndex < 1)
                        {
                            MsgBox1.Show("Ilitera.Net", "Preencha com dados do especialista ou selecione programa de saúde.", null,
                               new EO.Web.MsgBoxButton("OK"));
                            return;
                        }
                    }

                    if (wneOrientacao.Text.Trim() == "")
                    {
                        MsgBox1.Show("Ilitera.Net", "Preencha com dados de orientação.", null,
                               new EO.Web.MsgBoxButton("OK"));
                        return;
                    }

                    if (cmb_Programa.SelectedIndex < 1)
                    {
                        if (wneEspecialista.Text.Trim() == "")
                        {
                            MsgBox1.Show("Ilitera.Net", "Selecione programa de saúde ou preencha dados de especialista.", null,
                               new EO.Web.MsgBoxButton("OK"));
                            return;
                        }
                    }


                }



                if (wneOrientacao.Text.Length > 1500) wneOrientacao.Text = wneOrientacao.Text.Substring(0, 1500);

                if (wneEspecialista.Text.Length > 150) wneEspecialista.Text = wneEspecialista.Text.Substring(0,150);

                if (txtObs.Text.Length > 500) txtObs.Text = txtObs.Text.Substring(0, 500);



                Carregar_CID();
                PopulaExame();

                exame.IdEmpregado = System.Convert.ToInt32(Session["Empregado"].ToString());
                exame.Save();

                ViewState["IdExameNaoOcupacional"] = exame.Id;
                btnExcluir.Enabled = true;

                StringBuilder st = new StringBuilder();

                //st.Append("window.opener.document.getElementById('txtAuxiliar').value='atualiza';");
                //st.Append("window.opener.document.forms[0].submit();");
                //st.Append("window.alert('O Exame Ambulatorial foi " + tipo + " com sucesso!');");

                txtAuxAviso.Value = "O Exame Ambulatorial foi salvo com sucesso!";
                txtExecutePost.Value = "True";

                //if (cliente.ContrataPCMSO.Equals((int)TipoPcmsoContratada.NaoContratada))
                txtCloseWindow.Value = "True";

                Entities.Usuario user = (Entities.Usuario)Session["usuarioLogado"];

                if (Session["Retorno"].ToString().Trim() == "1" || Session["Retorno"].ToString().Trim() == "19")
                    Response.Redirect("DadosEmpregado_Questionario.aspx?IdEmpregado=" + Session["Empregado"].ToString() + "&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString() + "&Tipo=2");
                else if (Session["Retorno"].ToString().Trim() == "9")
                    Response.Redirect("DadosEmpregado_Dashboard.aspx?IdEmpregado=" + Session["Empregado"].ToString() + "&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString() + "&Tipo=6");
                else if (Session["Retorno"].ToString().Trim() == "91")
                    Response.Redirect("DadosEmpregado_Questionario.aspx?IdEmpregado=" + Session["Empregado"].ToString() + "&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString() + "&Tipo=6");
                else
                    Response.Redirect("DadosEmpregado_Lista.aspx?IdEmpregado=" + Session["Empregado"].ToString() + "&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString() + "&Tipo=6");



                //this.ClientScript.RegisterStartupScript(this.GetType(), "AtualizaNaoOcupacional", st.ToString(), true);
            }
            catch (Exception ex)
            {
                txtAuxAviso.Value = ex.Message;
                MsgBox1.Show("Ilitera.Net", ex.Message, null,
                       new EO.Web.MsgBoxButton("OK"));

            }
        }

        protected void btnExcluir_Click(object sender, System.EventArgs e)
        {
            try
            {
                exame.Delete();

                ViewState["IdExameNaoOcupacional"] = 0;  // null;

                btnExcluir.Enabled = true;
                StringBuilder st = new StringBuilder();

                txtAuxAviso.Value = "A entrevista foi deletada com sucesso";
                txtExecutePost.Value = "True";
                txtCloseWindow.Value = "True";

                Response.Write("<script>window.opener.document.forms[0].submit(); window.close();</script>");

                Entities.Usuario user = (Entities.Usuario)Session["usuarioLogado"];


                Response.Redirect("DadosEmpregado_Questionario.aspx?IdEmpregado=" + Session["Empregado"].ToString() + "&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString() + "&Tipo=2");


            }
            catch (Exception ex)
            {
                txtAuxAviso.Value = ex.Message;
                MsgBox1.Show("Ilitera.Net", ex.Message, null,
                       new EO.Web.MsgBoxButton("OK"));

            }
        }

        protected void cmd_Voltar_Click(object sender, EventArgs e)
        {

            Entities.Usuario user = (Entities.Usuario)Session["usuarioLogado"];

            Response.Redirect("DadosEmpregado_Questionario.aspx?IdEmpregado=" + Session["Empregado"].ToString() + "&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString() + "&Tipo=2");


        }

        protected void rd_Agudo_Complicacao_CheckedChanged(object sender, EventArgs e)
        {

            Ajuste_Controles();

        }

        protected void rd_Agudo_sem_Complicacao_CheckedChanged(object sender, EventArgs e)
        {

            Ajuste_Controles();

        }

        protected void rd_Cronico_CheckedChanged(object sender, EventArgs e)
        {

            Ajuste_Controles();

        }



        private void Ajuste_Controles()
        {

            if (rd_Agudo_Complicacao.Checked == true)
            {
                lblCID.Enabled = true;
                lblEspecialista.Enabled = true;
                lblOrientacao.Enabled = true;
                lblPrograma.Enabled = true;
                lblTratamento.Enabled = false;

                ddlCID.Enabled = true;
                wneEspecialista.Enabled = true;
                wneOrientacao.Enabled = true;
                cmb_Programa.Enabled = true;
                rd_Alto_Nao.Enabled = false;
                rd_Alto_Sim.Enabled = false;
                chk_Atualizar.Enabled = false;

            }
            else if (rd_Agudo_sem_Complicacao.Checked == true)
            {

                lblCID.Enabled = true;
                lblEspecialista.Enabled = false;
                lblOrientacao.Enabled = true;
                lblPrograma.Enabled = false;
                lblTratamento.Enabled = false;

                ddlCID.Enabled = true;
                wneEspecialista.Enabled = false;
                wneOrientacao.Enabled = true;
                cmb_Programa.Enabled = false;
                rd_Alto_Nao.Enabled = false;
                rd_Alto_Sim.Enabled = false;
                chk_Atualizar.Enabled = true;

            }
            else if (rd_Cronico.Checked == true)
            {

                lblCID.Enabled = true;
                lblEspecialista.Enabled = true;
                lblOrientacao.Enabled = true;
                lblPrograma.Enabled = true;
                lblTratamento.Enabled = true;

                ddlCID.Enabled = true;
                wneEspecialista.Enabled = true;
                wneOrientacao.Enabled = true;
                cmb_Programa.Enabled = true;
                rd_Alto_Nao.Enabled = true;
                rd_Alto_Sim.Enabled = true;
                chk_Atualizar.Enabled = false;

            }

        }



        private void Carregar_CID()
        {

            DataSet ds = new CID().Get("Descricao is not null ORDER BY Descricao DESC");

            ddlCID.Items.Insert(0, new ListItem("-", "0"));

            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in ds.Tables[0].Select())
                    ddlCID.Items.Insert(1, new ListItem(row["Descricao"].ToString(), row["IdCID"].ToString()));

            }
        }



        protected void grdExames_ItemCommand(object sender, EO.Web.GridCommandEventArgs e)
        {
            //Check whether it is from our client side
            //JavaScript call

            //guardar dados do empregado
            //string xComando = e.CommandName.Trim();
            string xId = "";

            int xLinha = ((EO.Web.Grid)sender).SelectedItemIndex;
            int xColuna = ((EO.Web.Grid)sender).SelectedCellIndex;


            xId = e.Item.Cells[xColuna + 6].Value.ToString();


            if (xColuna < 6)
            {
                if (xId.ToString().Trim() == "" || xId.ToString().Trim() == "0")
                    return;
            }

            Session["Questionario"] = exame.Id;  // Request["IdExame"].ToString();

            if (xColuna == 1)
            {
                Session["Retorno"] = "19";
                Response.Redirect("ExameClinico.aspx?IdEmpregado=" + Request["IdEmpregado"] + "&IdExame=" + xId.ToString() + "&IdUsuario=" + Request["IdUsuario"] + "&IdEmpresa=" + Request["IdEmpresa"] + "&Acao=V");
            }
            else if (xColuna == 2)
            {
                Session["Retorno"] = "19";
                Response.Redirect("ExameNaoOcupacional.aspx?IdEmpregado=" + Request["IdEmpregado"] + "&IdExame=" + xId.ToString() + "&IdUsuario=" + Request["IdUsuario"] + "&IdEmpresa=" + Request["IdEmpresa"] + "&Acao=V");
            }
            else if (xColuna == 3)
            {
                Session["Retorno"] = "19";
                string sScript = "javascript:void(addItem(centerWin('CadAbsentismo.aspx?IdEmpregado=" + Session["Empregado"].ToString() + "&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + Request["IdUsuario"] + "&IdAfastamento=" + xId.ToString() + "', 650, 650, 'CadAcidente'), 'Acidente'))";
                //System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", sScript, true);
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "MyScript", sScript, true);
                System.Web.UI.ScriptManager scriptManager = System.Web.UI.ScriptManager.GetCurrent(Page);
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(
               this,
               this.GetType(),
               "CadAcidente",
               sScript,
               true);


            }
            else if (xColuna == 4)
            {

                Session["Retorno"] = "19";
                string sScript = "javascript:void(addItem(centerWin('CadAbsentismo.aspx?IdEmpregado=" + Session["Empregado"].ToString() + "&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + Request["IdUsuario"] + "&IdAfastamento=" + xId.ToString() + "', 650, 650, 'CadAcidente'), 'Acidente'))";
                //System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", sScript, true);
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "MyScript", sScript, true);
                System.Web.UI.ScriptManager scriptManager = System.Web.UI.ScriptManager.GetCurrent(Page);
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(
               this,
               this.GetType(),
               "CadAcidente",
               sScript,
               true);

            }
            else if (xColuna == 6)
            {
                string zTipo = "";

                zTipo = e.Item.Cells[6].Value.ToString();

                if (zTipo.Trim() == "") return;

                zTipo = zTipo.Substring(28);

                Response.Redirect("Relatorio_AON.aspx?IdEmpregado=" + Request["IdEmpregado"] + "&Tipo=" + zTipo);

                // "&nbsp&nbsp002...............CONSULTAPS"
            }

        }


        protected void btnProcurar_Click(object sender, EventArgs e)
        {

            lbl_Procura.Text = "1";

            if (txtCID.Text.Trim() != string.Empty)
            {
                ddlCID.ClearSelection();

                while (ddlCID.Items.Count > 1)
                    ddlCID.Items.RemoveAt(0);

                DataSet ds = new CID().Get("CodigoCID='" + txtCID.Text.Trim() + "' OR Descricao LIKE '%" + txtCID.Text.Trim() + "%' ORDER BY Descricao DESC");

                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Select())
                        ddlCID.Items.Insert(0, new ListItem(row["Descricao"].ToString(), row["IdCID"].ToString()));
                    txtCID.Text = ddlCID.Items[ddlCID.SelectedIndex].ToString().Trim();
                    lbl_Id1.Text = ddlCID.SelectedValue.ToString().Trim();

                }
                else
                    MsgBox1.Show("Entrevista", "O código '" + txtCID.Text + "' não foi encontrado! Por favor, tente novamente!", null,
                                   new EO.Web.MsgBoxButton("OK"));


                //Aviso("O código '" + txtCID.Text + "' não foi encontrado! Por favor, tente novamente!");
            }
            else

                MsgBox1.Show("Entrevista", "É necessário colocar o Código CID para executar a busca!", null,
                               new EO.Web.MsgBoxButton("OK"));

        }



        protected void btnProcurar2_Click(object sender, System.EventArgs e)
        {

            lbl_Procura.Text = "2";

            if (txtCID2.Text.Trim() != string.Empty)
            {
                ddlCID.ClearSelection();

                while (ddlCID.Items.Count > 1)
                    ddlCID.Items.RemoveAt(0);

                DataSet ds = new CID().Get("CodigoCID='" + txtCID2.Text.Trim() + "' OR Descricao LIKE '%" + txtCID2.Text.Trim() + "%' ORDER BY Descricao DESC");

                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Select())
                        ddlCID.Items.Insert(0, new ListItem(row["Descricao"].ToString(), row["IdCID"].ToString()));
                    txtCID2.Text = ddlCID.Items[ddlCID.SelectedIndex].ToString().Trim();
                    lbl_Id2.Text = ddlCID.SelectedValue.ToString().Trim();
                }
                else
                    MsgBox1.Show("Entrevista", "O código '" + txtCID2.Text + "' não foi encontrado! Por favor, tente novamente!", null,
                                   new EO.Web.MsgBoxButton("OK"));


                //Aviso("O código '" + txtCID.Text + "' não foi encontrado! Por favor, tente novamente!");
            }
            else

                MsgBox1.Show("Entrevista", "É necessário colocar o Código CID para executar a busca!", null,
                               new EO.Web.MsgBoxButton("OK"));


            //Aviso("É necessário colocar o Código CID para executar a busca!");
        }

        protected void btnProcurar3_Click(object sender, System.EventArgs e)
        {

            lbl_Procura.Text = "3";

            if (txtCID3.Text.Trim() != string.Empty)
            {
                ddlCID.ClearSelection();

                while (ddlCID.Items.Count > 1)
                    ddlCID.Items.RemoveAt(0);

                DataSet ds = new CID().Get("CodigoCID='" + txtCID3.Text.Trim() + "' OR Descricao LIKE '%" + txtCID3.Text.Trim() + "%' ORDER BY Descricao DESC");

                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Select())
                        ddlCID.Items.Insert(0, new ListItem(row["Descricao"].ToString(), row["IdCID"].ToString()));
                    txtCID3.Text = ddlCID.Items[ddlCID.SelectedIndex].ToString().Trim();
                    lbl_Id3.Text = ddlCID.SelectedValue.ToString().Trim();
                }
                else
                    MsgBox1.Show("Entrevista", "O código '" + txtCID3.Text + "' não foi encontrado! Por favor, tente novamente!", null,
                                   new EO.Web.MsgBoxButton("OK"));


                //Aviso("O código '" + txtCID.Text + "' não foi encontrado! Por favor, tente novamente!");
            }
            else

                MsgBox1.Show("Entrevista", "É necessário colocar o Código CID para executar a busca!", null,
                               new EO.Web.MsgBoxButton("OK"));


            //Aviso("É necessário colocar o Código CID para executar a busca!");
        }

        protected void btnProcurar4_Click(object sender, System.EventArgs e)
        {

            lbl_Procura.Text = "4";

            if (txtCID4.Text.Trim() != string.Empty)
            {
                ddlCID.ClearSelection();

                while (ddlCID.Items.Count > 1)
                    ddlCID.Items.RemoveAt(0);

                DataSet ds = new CID().Get("CodigoCID='" + txtCID4.Text.Trim() + "' OR Descricao LIKE '%" + txtCID4.Text.Trim() + "%' ORDER BY Descricao DESC");

                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Select())
                        ddlCID.Items.Insert(0, new ListItem(row["Descricao"].ToString(), row["IdCID"].ToString()));
                    txtCID4.Text = ddlCID.Items[ddlCID.SelectedIndex].ToString().Trim();
                    lbl_Id4.Text = ddlCID.SelectedValue.ToString().Trim();
                }
                else
                    MsgBox1.Show("Entrevista", "O código '" + txtCID4.Text + "' não foi encontrado! Por favor, tente novamente!", null,
                                   new EO.Web.MsgBoxButton("OK"));


                //Aviso("O código '" + txtCID.Text + "' não foi encontrado! Por favor, tente novamente!");
            }
            else

                MsgBox1.Show("Entrevista", "É necessário colocar o Código CID para executar a busca!", null,
                               new EO.Web.MsgBoxButton("OK"));


            //Aviso("É necessário colocar o Código CID para executar a busca!");
        }





        protected void ddlCID_SelectedIndexChanged1(object sender, EventArgs e)
        {

            if (ddlCID.SelectedIndex < 0) return;

            if (lbl_Procura.Text == "1")
            {
                txtCID.Text = ddlCID.Items[ddlCID.SelectedIndex].ToString().Trim();
                lbl_Id1.Text = ddlCID.SelectedValue.ToString().Trim();
            }
            else if (lbl_Procura.Text == "2")
            {
                txtCID2.Text = ddlCID.Items[ddlCID.SelectedIndex].ToString().Trim();
                lbl_Id2.Text = ddlCID.SelectedValue.ToString().Trim();
            }
            else if (lbl_Procura.Text == "3")
            {
                txtCID3.Text = ddlCID.Items[ddlCID.SelectedIndex].ToString().Trim();
                lbl_Id3.Text = ddlCID.SelectedValue.ToString().Trim();
            }
            else if (lbl_Procura.Text == "4")
            {
                txtCID4.Text = ddlCID.Items[ddlCID.SelectedIndex].ToString().Trim();
                lbl_Id4.Text = ddlCID.SelectedValue.ToString().Trim();
            }
        }

    }



}
