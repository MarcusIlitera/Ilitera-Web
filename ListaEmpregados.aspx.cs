using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entities;
using Facade;
using System.Configuration;
using System.Data;
using System.Collections;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using EO.Web;
using Ilitera.Opsa.Data;


namespace Ilitera.Net
{
    public partial class ListaEmpregados : System.Web.UI.Page
    {

        static private string xBusca;
        static private string xTipo;

        protected void Page_Load(object sender, EventArgs e)
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
                Session["ExceptionType"] = " ";
                Session["Source"] = " ";
                //Server.Transfer("~/Tratar_Excecao.aspx");

                MsgBox1.Show( "Acesso ao Módulo", ex.Message, null,
                    new EO.Web.MsgBoxButton("OK"));                
                
                return;
            }

            if (Page.IsPostBack == false)
            {
                try
                {
                    //Session["Filtro_Tipo"] = "A";
                    if (Session["Filtro_Tipo"].ToString().Trim() != "")
                    {
                        rd_Inativos.Checked = false;
                        rd_Todos.Checked = false;
                        rd_Ativos.Checked = false;

                        if (Session["Filtro_Tipo"].ToString() == "T")
                        {
                            rd_Todos.Checked = true;
                        }
                        else if (Session["Filtro_Tipo"].ToString() == "I")
                        {
                            rd_Inativos.Checked = true;
                        }
                        if (Session["Filtro_Tipo"].ToString() == "A")
                        {
                            rd_Ativos.Checked = true;
                        }

                        xTipo = Session["Filtro_Tipo"].ToString();
                        //Session["Filtro_Tipo"] = "";
                    }
                    else
                        Session["Filtro_Tipo"] = "A";

                    Carrega_Combo_Setor();
                    PopularGrid();                    
                }



                catch (Exception ex)
                {
                    MsgBox1.Show("Listagem de Colaboradores", ex.Message, null,
                        new EO.Web.MsgBoxButton("OK"));
                }

            }
        }

        private void PopularGrid()
        {

            Entities.Usuario user = (Entities.Usuario)Session["usuarioLogado"];
            
            var retorno = new List<Entities.Empregado>();


            if (cmb_Setor.SelectedIndex <= 0)
            {
                if (Session["Filtro_Setor"].ToString().Trim() != "")
                {
                    for (int zCont = 0; zCont < lst_Id_Setor.Items.Count; zCont++)
                    {
                        if (Session["Filtro_Setor"].ToString().Trim() == lst_Id_Setor.Items[zCont].ToString().Trim())
                        {
                            cmb_Setor.SelectedIndex = zCont;
                            break;
                        }
                    }
                }
            }


            if (rd_Todos.Checked == true)
            {
                xTipo = "T";
            }
            else if (rd_Ativos.Checked == true)
            {
                xTipo = "A";
            }
            else if (rd_Inativos.Checked == true)
            {
                xTipo = "I";
            }


            if (cmb_Setor.SelectedIndex <= 0)
            {
                Session["Filtro_Setor"] = "";
                retorno = EmpregadoFacade.RetornarEmpregados(Convert.ToInt32(Session["Empresa"]), Convert.ToInt32(Session["JuridicaPai"]), user.IdPrestador, 0, xTipo);
            }
            else
            {
                Session["Filtro_Setor"] = lst_Id_Setor.Items[cmb_Setor.SelectedIndex].ToString();
                retorno = EmpregadoFacade.RetornarEmpregados(Convert.ToInt32(Session["Empresa"]), Convert.ToInt32(Session["JuridicaPai"]), user.IdPrestador, System.Convert.ToInt32(lst_Id_Setor.Items[cmb_Setor.SelectedIndex].ToString()), xTipo);
            }


            if (Session["Filtro_Nome"].ToString().ToUpper().Trim() != txt_Nome.Text.ToUpper().Trim())
            {
                Session["Filtro_Nome"] = txt_Nome.Text.Trim();
            }


            if (Session["Filtro_Nome"].ToString().Trim() != "")
            {
                txt_Nome.Text = Session["Filtro_Nome"].ToString().Trim();
                Session["Filtro_Nome"] = "";
            }

            if (Session["Filtro_Matricula"].ToString().Trim() != "")
            {
                txt_Matricula.Text = Session["Filtro_Matricula"].ToString().Trim();
                Session["Filtro_Matricula"] = "";
            }


            xBusca = txt_Nome.Text.Trim();

            if (xBusca == "")
            {
                xBusca = " ";
            }

            Session["Filtro_Nome"] = xBusca;
        
            var retorno2 = retorno.FindAll(new Predicate<Entities.Empregado>(Checar_Nome));
            retorno = null;


            xBusca = txt_Matricula.Text.Trim();

            if (xBusca == "")
            {
                xBusca = " ";
            }

            Session["Filtro_Matricula"] = xBusca;

            var retorno3 = retorno2.FindAll(new Predicate<Entities.Empregado>(Checar_Matricula));
            retorno2 = null;


            if (Session["Filtro_Tipo"].ToString().Trim() != "")
            {
                rd_Inativos.Checked = false;
                rd_Todos.Checked = false;
                rd_Ativos.Checked = false;

                if (Session["Filtro_Tipo"].ToString() == "T")
                {
                    rd_Todos.Checked = true;
                }
                else if (Session["Filtro_Tipo"].ToString() == "I")
                {
                    rd_Inativos.Checked = true;
                }
                if (Session["Filtro_Tipo"].ToString() == "A")
                {
                    rd_Ativos.Checked = true;
                }

                xTipo = Session["Filtro_Tipo"].ToString();
                Session["Filtro_Tipo"] = "";
            }



            Session["Filtro_Tipo"] = xTipo;
            
            var retorno4 = retorno3.FindAll(new Predicate<Entities.Empregado>(Checar_Tipo));
            retorno3 = null;
            
            grd_Empregados.DataSource = retorno4;
            grd_Empregados.DataBind();

            Boolean zAchou = false;

            if (Session["NomeEmpregado"].ToString().Trim() != "")
            {
                for (int zCont = 0; zCont < grd_Empregados.Items.Count; zCont++)
                {
                    if (grd_Empregados.Items[zCont].Cells[0].Value.ToString().Trim() == Session["NomeEmpregado"].ToString().Trim())
                    {
                        grd_Empregados.SelectedItemIndex = zCont;
                        Carregar_Dados_Colaborador();
                        zAchou = true;
                        break;
                    }
                }
            }

            if (zAchou == false)
            {
                Session["NomeEmpregado"] = "";
            }

            retorno = null;
            retorno2 = null;
            retorno3 = null;
            retorno4 = null;

            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();


        }




        private static bool Checar_Nome(Entities.Empregado xEmpr)
        {
            string result;
            string busca;


            //para ignorar acentuação
            Byte[] bytes = System.Text.Encoding.GetEncoding("Cyrillic").GetBytes(xEmpr.NomeEmpregado.ToString().ToUpper());
            result = System.Text.Encoding.ASCII.GetString(bytes);

            Byte[] bytes2 = System.Text.Encoding.GetEncoding("Cyrillic").GetBytes(xBusca.ToString().ToUpper().Trim());
            busca = System.Text.Encoding.ASCII.GetString(bytes2);


            if (  result.IndexOf(busca, StringComparison.InvariantCultureIgnoreCase) >= 0) { return true; } else { return false; }
            //if (xEmpr.NomeEmpregado.ToString().ToUpper().IndexOf(xBusca.ToString().ToUpper().Trim(), StringComparison.InvariantCultureIgnoreCase) >= 0) { return true; } else { return false; }
        }


        private static bool Checar_Matricula(Entities.Empregado xEmpr)
        {

            if (xEmpr.matricula == null) { return false; }
            else
               if (xEmpr.matricula.ToString().ToUpper().IndexOf(xBusca.ToString().ToUpper().Trim()) >= 0) { return true; } else { return false; }

        }

        private static bool Checar_Tipo(Entities.Empregado xEmpr)
        {
            if (xTipo == "T")
            {
                return true;
            }
            else if (xTipo == "A")
            {
                if (xEmpr.DataDemissao.Year == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else //if ( xTipo == "I" )
            {
                if (xEmpr.DataDemissao.Year == 1)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }

        }



        private void Carrega_Combo_Setor()
        {

            Ilitera.Data.PPRA_EPI xGHE = new Ilitera.Data.PPRA_EPI();

            DataSet dS1 = xGHE.Carregar_Setores(System.Convert.ToInt32(Session["Empresa"].ToString()));

            //cmb_Setor.DataSource = dS1;
            //cmb_Setor.DataValueField = "nID_SETOR";
            //cmb_Setor.DataTextField = "tNO_STR_EMPR";
            //cmb_Setor.DataBind();

            cmb_Setor.Items.Clear();
            cmb_Setor.Items.Add("Sem Filtro");

            lst_Id_Setor.Items.Clear();
            lst_Id_Setor.Items.Add("0");

            foreach (DataRow row in dS1.Tables[0].Rows)
            {
                cmb_Setor.Items.Add(row["tNO_Str_EMPR"].ToString());
                lst_Id_Setor.Items.Add(row["nId_Setor"].ToString());
            }

            cmb_Setor.SelectedIndex = 0;

            return;

        }


        protected void Menu1_ItemClick(object sender, EO.Web.NavigationItemEventArgs e)
        {

        //    //Session["Empregado"] = e.Item.Key.ToString();
        //    //Session["NomeEmpregado"] = e.Item.Cells[0].Value.ToString();


        //    //Code to save changes goes here
        //    if (e.MenuItem.Text.ToString() == "Clínicos zz")
        //    {

        //        //primeiro deve aparecer a relação de exames salvos


        //        //Response.Redirect("PCMSO\\DadosEmpregado.aspx");
        //        Entities.Usuario user = (Entities.Usuario)Session["usuarioLogado"];
        //        Response.Redirect("PCMSO\\DadosEmpregado.aspx?IdEmpregado=" + Session["Empregado"].ToString() + "&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString() + "&Tipo=1");

        //    }
                                   

            
        }



        protected void grd_Empregados_ItemCommand(object sender, EO.Web.GridCommandEventArgs e)
        {
            //Check whether it is from our client side
            //JavaScript call

            //guardar dados do empregado
            string xComando = e.CommandName.Trim();

            Session["Empregado"] = e.Item.Key.ToString();
            Session["NomeEmpregado"] = e.Item.Cells[0].Value.ToString();


            Label xColaborador = new Label();

            xColaborador = (Label)Page.Master.FindControl("lbl_Colaborador");

            xColaborador.Text = "Colaborador : " + Session["NomeEmpregado"].ToString().Trim();


            if (xComando == "Dados Cadastrais")  //dados cadastrais
            {
                Session["Ajuda"] = "~/Help/Manual_Novo_Ilitera_Net_C04_1.pdf";
                Response.Redirect("VisualizarDadosEmpregado_Novo2.aspx");
            }
            else if (xComando == "Readmissão")  //dados cadastrais
            {
                Session["Ajuda"] = "~/Help/Manual_Novo_Ilitera_Net_C04_2.pdf";
                Response.Redirect("~/InserirDadosEmpregado.aspx?IdEmpregado=" + e.Item.Key.ToString());
            }

            else if (xComando == "Seleção")
            {
                //ao clicar, seleciona o registro
                Session["Ajuda"] = "~/Help/Manual_Novo_Ilitera_Net_C04.pdf";
                Carregar_Dados_Colaborador();
            }
            else if (xComando == "PPP")  //PPP
            {
                Entities.Usuario user = (Entities.Usuario)Session["usuarioLogado"];
                Session["Ajuda"] = "~/Help/Manual_Novo_Ilitera_Net_C16.pdf";
                Response.Redirect("CheckPPPEmpregado.aspx?IdEmpregado=" + Session["Empregado"].ToString() + "&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString());
            }
            else if (xComando == "LTCAT")
            {
                Entities.Usuario user = (Entities.Usuario)Session["usuarioLogado"];
                Session["Ajuda"] = "~/Help/Manual_Novo_Ilitera_Net.pdf";
                Response.Redirect("CheckLTCAT.aspx?IdEmpregado=" + Session["Empregado"].ToString() + "&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString());
            }
            else if (xComando == "Laudo de Insalubridade/Periculosidade")
            {
                Entities.Usuario user = (Entities.Usuario)Session["usuarioLogado"];
                Session["Ajuda"] = "~/Help/Manual_Novo_Ilitera_Net.pdf";
                Response.Redirect("CheckInsalubridade.aspx?IdEmpregado=" + Session["Empregado"].ToString() + "&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString());
            }
            else if (xComando == "Guia de Encaminhamento")  //Guia Encaminhamento
            {
                Entities.Usuario user = (Entities.Usuario)Session["usuarioLogado"];
                Session["Ajuda"] = "~/Help/Manual_Novo_Ilitera_Net_C10.pdf";
                Response.Redirect("PCMSO\\DadosEmpregado_Guia.aspx?IdEmpregado=" + Session["Empregado"].ToString() + "&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString());
            }
            else if (xComando == "Cancelamento de Guia/ASO")  //Cancelamento de ASO e Guia Encaminhamento
            {
                Entities.Usuario user = (Entities.Usuario)Session["usuarioLogado"];
                Session["Ajuda"] = "~/Help/Manual_Novo_Ilitera_Net_C10.pdf";
                Response.Redirect("PCMSO\\Cancelar_Agendamento.aspx?IdEmpregado=" + Session["Empregado"].ToString() + "&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString());
            }
            else if (xComando == "Guias de Encaminhamento Separadas")  //Guia Encaminhamento separadas
            {
                Entities.Usuario user = (Entities.Usuario)Session["usuarioLogado"];
                Session["Ajuda"] = "~/Help/Manual_Novo_Ilitera_Net_C10.pdf";
                Response.Redirect("PCMSO\\DadosEmpregado_Guia_Dup.aspx?IdEmpregado=" + Session["Empregado"].ToString() + "&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString());
            }

            else if (xComando == "Clínicos")  //"6")  //Clinicos
            {
                Entities.Usuario user = (Entities.Usuario)Session["usuarioLogado"];
                Session["Ajuda"] = "~/Help/Manual_Novo_Ilitera_Net_C06.pdf";
                Response.Redirect("PCMSO\\DadosEmpregado.aspx?IdEmpregado=" + Session["Empregado"].ToString() + "&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString() + "&Tipo=1");

            }
            else if (xComando == "Complementares")  //Complementares
            {
                Entities.Usuario user = (Entities.Usuario)Session["usuarioLogado"];
                Session["Ajuda"] = "~/Help/Manual_Novo_Ilitera_Net_C07.pdf";
                Response.Redirect("PCMSO\\DadosEmpregado.aspx?IdEmpregado=" + Session["Empregado"].ToString() + "&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString() + "&Tipo=3");
            }
            else if (xComando == "Audiométricos")  //Audiometricos
            {
                Entities.Usuario user = (Entities.Usuario)Session["usuarioLogado"];
                Session["Ajuda"] = "~/Help/Manual_Novo_Ilitera_Net_C08.pdf";
                Response.Redirect("PCMSO\\DadosEmpregado.aspx?IdEmpregado=" + Session["Empregado"].ToString() + "&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString() + "&Tipo=4");

            }
            else if (xComando == "Ambulatoriais" || xComando == "Enfermagem")  //Ambulatoriais
            {
                Entities.Usuario user = (Entities.Usuario)Session["usuarioLogado"];
                Session["Ajuda"] = "~/Help/Manual_Novo_Ilitera_Net_C09.pdf";
                Response.Redirect("PCMSO\\DadosEmpregado.aspx?IdEmpregado=" + Session["Empregado"].ToString() + "&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString() + "&Tipo=2");
            }
            else if (xComando == "Vacinação")  //Vacinação
            {
                Entities.Usuario user = (Entities.Usuario)Session["usuarioLogado"];
                Session["Ajuda"] = "~/Help/Manual_Novo_Ilitera_Net_C09.pdf";
                Response.Redirect("PCMSO\\DadosEmpregado_Vacina.aspx?IdEmpregado=" + Session["Empregado"].ToString() + "&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString() + "&Tipo=8");

            }
            else if (xComando == "PCI")  //PCI
            {
                Entities.Usuario user = (Entities.Usuario)Session["usuarioLogado"];
                Session["Ajuda"] = "~/Help/Manual_Novo_Ilitera_Net.pdf";
                //Response.Redirect("PCMSO\\DadosEmpregado.aspx?IdEmpregado=" + Session["Empregado"].ToString() + "&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString() + "&Tipo=6");
                Response.Redirect("PCMSO\\DadosEmpregado_Lista.aspx?IdEmpregado=" + Session["Empregado"].ToString() + "&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString() + "&Tipo=6");
            }
            else if (xComando == "Informações Médicas")  //Informações Médicas
            {
                Entities.Usuario user = (Entities.Usuario)Session["usuarioLogado"];
                Session["Ajuda"] = "~/Help/Manual_Novo_Ilitera_Net.pdf";
                //Response.Redirect("PCMSO\\DadosEmpregado.aspx?IdEmpregado=" + Session["Empregado"].ToString() + "&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString() + "&Tipo=6");
                Response.Redirect("PCMSO\\DadosEmpregado_Dashboard.aspx?IdEmpregado=" + Session["Empregado"].ToString() + "&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString() + "&Tipo=6");
            }
            else if (xComando == "Entrevista")  //Informações Médicas
            {
                Entities.Usuario user = (Entities.Usuario)Session["usuarioLogado"];
                Session["Ajuda"] = "~/Help/Manual_Novo_Ilitera_Net.pdf";
                //Response.Redirect("PCMSO\\DadosEmpregado.aspx?IdEmpregado=" + Session["Empregado"].ToString() + "&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString() + "&Tipo=6");
                Response.Redirect("PCMSO\\DadosEmpregado_Questionario.aspx?IdEmpregado=" + Session["Empregado"].ToString() + "&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString() + "&Tipo=6");
            }

            else if (xComando == "Ficha Clínica")  //PCI
            {
                Entities.Usuario user = (Entities.Usuario)Session["usuarioLogado"];
                Session["Ajuda"] = "~/Help/Manual_Novo_Ilitera_Net.pdf";
                Response.Redirect("PCMSO\\DadosEmpregado.aspx?IdEmpregado=" + Session["Empregado"].ToString() + "&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString() + "&Tipo=6");
            }
            else if (xComando == "Riscos e EPI")  //Riscos e EPIs
            {
                Entities.Usuario user = (Entities.Usuario)Session["usuarioLogado"];
                Session["Ajuda"] = "~/Help/Manual_Novo_Ilitera_Net_C11.pdf";
                Response.Redirect("PCMSO\\RiscosEPIs.aspx?IdEmpregado=" + Session["Empregado"].ToString());
            }
            else if (xComando == "Configuração Individual de CA")
            {
                Entities.Usuario user = (Entities.Usuario)Session["usuarioLogado"];
                Session["Ajuda"] = "~/Help/Manual_Novo_Ilitera_Net_C13.pdf";
                Response.Redirect("PCMSO\\DadosEmpregado_CA.aspx?IdEmpregado=" + Session["Empregado"].ToString() + "&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString());
            }
            else if (xComando == "Prontuários Digitais")
            {
                Entities.Usuario user = (Entities.Usuario)Session["usuarioLogado"];
                Session["Ajuda"] = "~/Help/Manual_Novo_Ilitera_Net_C12.pdf";
                Response.Redirect("PCMSO\\DadosEmpregado_Digitalizado.aspx?IdEmpregado=" + Session["Empregado"].ToString() + "&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString());
            }
            else if (xComando == "Acidente" || xComando == "CAT – Comunicação de Acidente de Trabalho")
            {
                Session["Ajuda"] = "~/Help/Manual_Novo_Ilitera_Net_C14.pdf";
                Response.Redirect("~/PCMSO/ListaAcidentes2.aspx?Tipo=1&Tela=0");
            }
            else if (xComando == "Absenteísmo")
            {
                Session["Ajuda"] = "~/Help/Manual_Novo_Ilitera_Net_C15.pdf";
                Response.Redirect("~/PCMSO/ListaAcidentes2.aspx?Tipo=2&Tela=0");
            }
            else if (xComando == "Ordem de Serviço")
            {
                Session["Ajuda"] = "~/Help/Manual_Novo_Ilitera_Net_C15.pdf";
                //Response.Redirect("~/PCMSO/ListaAcidentes2.aspx?Tipo=2&Tela=0");
                System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");
                Guid strAux = Guid.NewGuid();

                Entities.Usuario user = (Entities.Usuario)Session["usuarioLogado"];

                //OpenReport("OrdemDeServico", "\\OrdemDeServico\\RptOrdemDeServico.aspx?IliteraSystem=" + strAux.ToString() + strAux.ToString()
                //           + "&IdEmpregado=" + e.Item.Key.ToString() + "&Impressao=V" + "&IdUsuario=" + user.IdUsuario.ToString() + "&IdEmpresa=" + Session["Empresa"].ToString().Trim() + "&DataOS=" + DateTime.Now.ToString("dd/MM/yyyy", ptBr).ToString(),
                //           "OrdemServico");

                string xPagina = "OrdemDeServico2/RptOrdemDeServico.aspx?IliteraSystem=" + strAux.ToString() + strAux.ToString()
                           + "&IdEmpregado=" + e.Item.Key.ToString() + "&Impressao=V" + "&IdUsuario=" + user.IdUsuario.ToString() + "&IdEmpresa=" + Session["Empresa"].ToString().Trim() + "&DataOS=" + DateTime.Now.ToString("dd/MM/yyyy", ptBr).ToString();

                System.Text.StringBuilder st = new System.Text.StringBuilder();


                st.Append("void(window.open('" + xPagina + "', 'OrdemDeServico2','scrollbars=yes, resizable=yes, toolbar=no, addressbar=no, menubar=no, width=800px, height=600px' ))");
                System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), String.Format("OpenReport{0}", "OS"), st.ToString(), true);
            }

            
        }




        protected void rd_Todos_CheckedChanged(object sender, EventArgs e)
        {
            Session["Filtro_Tipo"] = "T";
            PopularGrid();
        }

        protected void rd_Ativos_CheckedChanged(object sender, EventArgs e)
        {
            Session["Filtro_Tipo"] = "A";
            PopularGrid();
        }

        protected void rd_Inativos_CheckedChanged(object sender, EventArgs e)
        {
            Session["Filtro_Tipo"] = "I";
            PopularGrid();
        }

        protected void txt_Nome_TextChanged(object sender, EventArgs e)
        {
            Session["Filtro_Nome"] = txt_Nome.Text.Trim();
        }

        protected void cmd_Busca_Click(object sender, EventArgs e)
        {
            PopularGrid();
        }


        protected void Carregar_Dados_Colaborador()
        {

            
            Ilitera.Opsa.Data.Empregado empregado = new Ilitera.Opsa.Data.Empregado();

            empregado.Find(" tblEMPREGADO.nID_EMPREGADO = " + Session["Empregado"].ToString());
            EmpregadoFuncao empregadoFuncao = EmpregadoFuncao.GetEmpregadoFuncao(empregado);

            //ImagemEmpregado.ImageUrl = empregado.FotoEmpregadoUrl();
            lblValorNome.Text = empregado.tNO_EMPG;

            

            switch (empregado.nIND_BENEFICIARIO)
            {
                case (int)TipoBeneficiario.BeneficiarioReabilitado:
                    lblValorBene.Text = "Beneficiário Reabilitado";
                    break;
                case (int)TipoBeneficiario.PortadorDeficiencia:
                    lblValorBene.Text = "Portador de Deficiência habilitada";
                    break;
                case (int)TipoBeneficiario.NaoAplicavel:
                    lblValorBene.Text = "Não Aplicável";
                    break;
                default:
                    lblValorBene.Text = "Não Aplicável";
                    break;
            }




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

            NavigationItemText zT = "Readmissão";
            zT.Html = "Readmissão";

            if (empregado.hDT_DEM.ToString("dd-MM-yyyy").Equals("01-01-1753") || empregado.hDT_DEM == new DateTime())
            {
                lblValorDemissao.Text = "&nbsp;";
                for (int zCont = 0; zCont < Menu1.Items.Count; zCont++)
                {

                    if ( Menu1.Items[zCont].Text.Html == zT.Html )
                    {
                        Menu1.Items[zCont].Visible = false;
                        break;
                    }
                }  
            }
            else
            {
                lblValorDemissao.Text = empregado.hDT_DEM.ToString("dd-MM-yyyy");
                for (int zCont = 0; zCont < Menu1.Items.Count; zCont++)
                {

                    if (Menu1.Items[zCont].Text.Html == zT.Html)
                    {
                        Menu1.Items[zCont].Visible = true;
                        break;
                    }
                }

            }

            lblValorRegistro.Text = empregado.VerificaNullCampoString("tCOD_EMPR", "&nbsp;");

            if (empregado.nID_REGIME_REVEZAMENTO.Id == 0)
                lblValorRegRev.Text = "&nbsp;";
            else
                lblValorRegRev.Text = empregado.nID_REGIME_REVEZAMENTO.ToString();

            lblValorTempoEmpresa.Text = empregado.TempoEmpresaEmpregado();
            lblValorFuncao.Text = empregadoFuncao.GetNomeFuncao();
            lblValorSetor.Text = empregadoFuncao.GetNomeSetor();

            //if (EmpregadoFuncao.GetJornada(empregado) == "" || EmpregadoFuncao.GetJornada(empregado) == null)
            //    lblValorJornada.Text = "&nbsp;";
            //else
            //    lblValorJornada.Text = EmpregadoFuncao.GetJornada(empregado);

            if (empregadoFuncao.hDT_INICIO == new DateTime() || empregadoFuncao.hDT_INICIO == new DateTime(1753, 1, 1))
                lblValorDataIni.Text = "&nbsp;";
            else
                lblValorDataIni.Text = empregadoFuncao.hDT_INICIO.ToString("dd-MM-yyyy");


            Ilitera.Data.Clientes_Funcionarios xDadosGHE = new Ilitera.Data.Clientes_Funcionarios();
            lblGHE2.Text = xDadosGHE.Trazer_GHE_Atual_Colaborador(empregadoFuncao.Id);

        }


        protected void cmb_Setor_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopularGrid();
        }



    }
}
