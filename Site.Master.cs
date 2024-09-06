using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using Entities;
using Facade;
using System.Net;
using System.Diagnostics;

namespace Ilitera.Net
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            // NavigationMenu.Items[1].NavigateUrl = Session["Ajuda"].ToString();

            if (!IsPostBack)
            {

            }



            

            if (Session["NomeEmpresa"] == null)
            {
                lbl_Empresa.Text = "";

                if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("PRAJNA") > 0)
                {
                    Items_Menu_Essence(false);
                }

            }
            else
            {
                if (Session["NomeEmpresa"].ToString().Trim() == String.Empty)
                {
                    lbl_Empresa.Text = "";

                    if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("PRAJNA") > 0)
                    {
                        Items_Menu_Essence(false);
                    }
                }
                else
                {
                    lbl_Empresa.Text = "Empresa Selecionada : " + Session["NomeEmpresa"].ToString().Trim();

                    if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("PRAJNA") > 0)
                    {
                        if ( lbl_Empresa.Text.ToUpper().IndexOf("CAPGEM") > 0)
                            Items_Menu_Essence(true);
                        else
                            Items_Menu_Essence(false);
                    }

                    }
                }


            if (Session["NomeEmpregado"] == null)
            {
                lbl_Colaborador.Text = "";
            }
            else
            {
                if (Session["NomeEmpregado"].ToString().Trim() == String.Empty)
                    lbl_Colaborador.Text = "";
                else
                    lbl_Colaborador.Text = "Colaborador : " + Session["NomeEmpregado"].ToString().Trim();
            }


        }



        private void Abrir_Help()
        {

            String strPathAndQuery = HttpContext.Current.Request.Url.PathAndQuery;

            string xPath = "";
            xPath = HttpContext.Current.Request.Url.AbsoluteUri;

            try
            {

                xPath = xPath.Replace(HttpContext.Current.Request.Url.Segments[2].ToString(), "");
                xPath = xPath.Replace(HttpContext.Current.Request.Url.Segments[3].ToString(), "");
                xPath = xPath.Replace(HttpContext.Current.Request.Url.Segments[4].ToString(), "");
                xPath = xPath.Replace(HttpContext.Current.Request.Url.Segments[5].ToString(), "");

            }
            catch
            {

            }

            xPath = xPath + "help.aspx";


            Response.Write("<script>");
            Response.Write("window.open('" + xPath + "','_blank')");
            Response.Write("</script>");
        }

        protected void Menu1_ItemClick(object sender, EO.Web.NavigationItemEventArgs e)
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

            string xModulo = "";

            //try
            //{
            //    xModulo = ((EO.Web.MenuItem)e.NavigationItem).Text.ToString().Trim();
            //}
            //catch ( Exception Ex)
            //{
            //    xModulo = e.NavigationItem.ItemID.ToString().Trim();
            //}

            try
            {
                if (((EO.Web.MenuItem)e.NavigationItem) == null)
                {
                    xModulo = "";
                }
                else
                {
                    xModulo = ((EO.Web.MenuItem)e.NavigationItem).Text.ToString().Trim();
                }
            }
            catch (Exception Ex)
            {
                //xModulo = e.NavigationItem.ItemID.ToString().Trim();
                xModulo = "";
            }

            if (xModulo == "Sair do<br>\r\nSistema" || xModulo == "Sair")
            {
                Session["Pagina"] = "1";
                Session["Empregado"] = String.Empty;
                Session["NomeEmpregado"] = String.Empty;
                Session["NomeEmpresa"] = String.Empty;
                Session["Preposto"] = String.Empty;
                Session["Preposto2"] = String.Empty;



                Session["Filtro_Nome"] = "";
                Session["Filtro_Empresa"] = "";
                Session["Filtro_Tipo"] = "";
                Session["Filtro_Setor"] = "";

                if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("PRAJNA") > 0)
                    Page.ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.open('close_essence.html', '_self', null);", true);
                else
                    Page.ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.open('close.html', '_self', null);", true);

                return;
            }

            ////security baseline
            //if ( xModulo == "Web Application Security Policy")
            //{
            //    Session["Ajuda"] = "~/baselines/web_application_security_policy.pdf";
            //    Abrir_Help();               
            //}
            //else if (xModulo == "Database Credentials Policy")
            //{
            //    Session["Ajuda"] = "~/baselines/database_credentials_policy.pdf";
            //    Abrir_Help();
            //}
            //else if (xModulo == "Information Logging Standard")
            //{
            //    Session["Ajuda"] = "~/baselines/information_logging_standard.pdf";
            //    Abrir_Help();
            //}
            //else if (xModulo == "Lab Security Policy")
            //{
            //    Session["Ajuda"] = "~/baselines/lab_security_policy.pdf";
            //    Abrir_Help();
            //}
            //else if (xModulo == "Server Security")
            //{
            //    Session["Ajuda"] = "~/baselines/server_security_policy.pdf";
            //    Abrir_Help();
            //}
            //else if (xModulo == "Software Installation Policy")
            //{
            //    Session["Ajuda"] = "~/baselines/software_installation_policy.pdf";
            //    Abrir_Help();
            //}
            //else if (xModulo == "Technology Equipament Disposal Policy")
            //{
            //    Session["Ajuda"] = "~/baselines/technology_equipment_disposal_policy.pdf";
            //    Abrir_Help();
            //}
            //else if (xModulo == "Workstation Security")
            //{
            //    Session["Ajuda"] = "~/baselines/workstation_security_for_hipaa_policy.pdf";
            //    Abrir_Help();
            //}
            //else if (xModulo == "Acquisition Assessment Policy")
            //{
            //    Session["Ajuda"] = "~/baselines/acquisition_assessment_policy.pdf";
            //    Abrir_Help();
            //}
            //else if (xModulo == "Bluetooth Baseline Requirements Policy")
            //{
            //    Session["Ajuda"] = "~/baselines/bluetooth_baseline_requirements_policy.pdf";
            //    Abrir_Help();
            //}
            //else if (xModulo == "Remote Access Policy")
            //{
            //    Session["Ajuda"] = "~/baselines/remote_access_policy.pdf";
            //    Abrir_Help();
            //}
            //else if (xModulo == "Remote Access Tools Security")
            //{
            //    Session["Ajuda"] = "~/baselines/remote_access_tools_policy.pdf";
            //    Abrir_Help();
            //}
            //else if (xModulo == "Router and Switch Security Policy")
            //{
            //    Session["Ajuda"] = "~/baselines/router_and_switch_security_policy.pdf";
            //    Abrir_Help();
            //}
            //else if (xModulo == "Wireless Communication Policy")
            //{
            //    Session["Ajuda"] = "~/baselines/wireless_communication_policy.pdf";
            //    Abrir_Help();
            //}
            //else if (xModulo == "Wireless Communication Standard")
            //{
            //    Session["Ajuda"] = "~/baselines/wireless_communication_standard.pdf";
            //    Abrir_Help();
            //}
            //else if (xModulo == "Acceptable Encryption Policy")
            //{
            //    Session["Ajuda"] = "~/baselines/acceptable_encryption_policy.pdf";
            //    Abrir_Help();
            //}
            //else if (xModulo == "Acceptable Use Policy")
            //{
            //    Session["Ajuda"] = "~/baselines/acceptable_use_policy.pdf";
            //    Abrir_Help();
            //}
            //else if (xModulo == "Clean Desk Policy")
            //{
            //    Session["Ajuda"] = "~/baselines/clean_desk_policy.pdf";
            //    Abrir_Help();
            //}
            //else if (xModulo == "Digital Signature Acceptance Policy")
            //{
            //    Session["Ajuda"] = "~/baselines/digital_signature_acceptance_policy.pdf";
            //    Abrir_Help();
            //}
            //else if (xModulo == "Disaster Recovery Plan Policy")
            //{
            //    Session["Ajuda"] = "~/baselines/disaster_recovery_plan_policy.pdf";
            //    Abrir_Help();
            //}
            //else if (xModulo == "Email Policy")
            //{
            //    Session["Ajuda"] = "~/baselines/email_policy.pdf";
            //    Abrir_Help();
            //}
            //else if (xModulo == "End User Encryption Key Protection Policy")
            //{
            //    Session["Ajuda"] = "~/baselines/end_user_encryption_key_protection_policy.pdf";
            //    Abrir_Help();
            //}
            //else if (xModulo == "Ethics Policy")
            //{
            //    Session["Ajuda"] = "~/baselines/ethics_policy.pdf";
            //    Abrir_Help();
            //}
            //else if (xModulo == "Pandemic Response Planning Policy")
            //{
            //    Session["Ajuda"] = "~/baselines/pandemic_response_planning_policy.pdf";
            //    Abrir_Help();
            //}
            //else if (xModulo == "Password Construction Guidelines")
            //{
            //    Session["Ajuda"] = "~/baselines/password_construction_guidelines.pdf";
            //    Abrir_Help();
            //}
            //else if (xModulo == "Password Protection Policy")
            //{
            //    Session["Ajuda"] = "~/baselines/password_protection_policy.pdf";
            //    Abrir_Help();
            //}
            //else if (xModulo == "Security Response Plan Policy")
            //{
            //    Session["Ajuda"] = "~/baselines/security_response_plan_policy.pdf";
            //    Abrir_Help();
            //}





            if (xModulo == "Ajuda do<br>\r\nSistema" || xModulo == "Ajuda")
            {
                Abrir_Help();
           
                //String strPathAndQuery = HttpContext.Current.Request.Url.PathAndQuery;
            
                //string xPath = "";
                //xPath = HttpContext.Current.Request.Url.AbsoluteUri;

                //try
                //{

                //    xPath = xPath.Replace(HttpContext.Current.Request.Url.Segments[2].ToString(), "");
                //    xPath = xPath.Replace(HttpContext.Current.Request.Url.Segments[3].ToString(), "");
                //    xPath = xPath.Replace(HttpContext.Current.Request.Url.Segments[4].ToString(), "");
                //    xPath = xPath.Replace(HttpContext.Current.Request.Url.Segments[5].ToString(), "");

                //}
                //catch
                //{

                //}

                //xPath = xPath + "help.aspx";


                //Response.Write("<script>");
                //Response.Write("window.open('" + xPath + "','_blank')");
                //Response.Write("</script>");


            }

            if (xModulo == "Política de Privacidade")
            {
                
                Response.Write("<script>");
                Response.Write("window.open('https://www.ilitera.com.br/politica-de-privacidade','_blank')");
                Response.Write("</script>");
                return;
            }

                if (Session["NomeEmpresa"] == null)
            {
                MsgBox1.Show("Ilitera.Net", "Necessário selecionar empresa!", null,
                              new EO.Web.MsgBoxButton("OK"));
                return;
            }

            if (Session["NomeEmpresa"].ToString() == String.Empty)
            {
                MsgBox1.Show("Ilitera.Net", "Necessário selecionar empresa!", null,
                              new EO.Web.MsgBoxButton("OK"));
                return;

            }

            Entities.Usuario user = (Entities.Usuario)Session["usuarioLogado"];


            if (xModulo == "Listar Empresas")
            {
                Session["Ajuda"] = "~/Help/Manual_Novo_Ilitera_Net_C03.pdf";
                Response.Redirect("~/ListaEmpresas2.aspx");
            }
            if (xModulo == "Dados Cadastrais")
            {
                Session["Ajuda"] = "~/Help/Manual_Novo_Ilitera_Net_C03.pdf";
                Response.Redirect("~/VisualizarDadosEmpresa.aspx");
            }
            else if (xModulo == "Listar Colaboradores")
            {
                Session["Ajuda"] = "~/Help/Manual_Novo_Ilitera_Net_C04.pdf";
                Response.Redirect("~/ListaEmpregados.aspx");
            }
            else if (xModulo == "Inserir Colaborador")
            {
                Session["Ajuda"] = "~/Help/Manual_Novo_Ilitera_Net_C04_2.pdf";
                Response.Redirect("~/InserirDadosEmpregado.aspx?IdEmpregado=0");
            }
            else if (xModulo == "Edição do Colaborador")
            {
                if (Session["NomeEmpregado"] == null)
                {
                    MsgBox1.Show("Ilitera.Net", "Necessário selecionar colaborador!", null, new EO.Web.MsgBoxButton("OK"));
                    return;
                }
                if (Session["NomeEmpregado"].ToString() == String.Empty)
                {
                    MsgBox1.Show("Ilitera.Net", "Necessário selecionar colaborador!", null, new EO.Web.MsgBoxButton("OK"));
                    return;
                }
                Session["Ajuda"] = "~/Help/Manual_Novo_Ilitera_Net_C04_1.pdf";
                Response.Redirect("~/VisualizarDadosEmpregado_Novo2.aspx");
            }
            else if (xModulo == "Fornecimento de EPI Manual")
            {
                Session["Ajuda"] = "~/Help/Manual_Novo_Ilitera_Net_C23.pdf";
                Response.Redirect("~/ControleEPI/EntregaEPI.aspx");
            }
            else if (xModulo == "Fornecimento de EPI Automático")
            {
                Session["Ajuda"] = "~/Help/Manual_Novo_Ilitera_Net_C23.pdf";
                Response.Redirect("~/ControleEPI/EntregaEPI_Auto.aspx?IdEmpresa=" + Session["Empresa"].ToString() + " ");
            }            
            else if (xModulo == "Fornecimento de EPI - Lista")
            {
                Session["Ajuda"] = "~/Help/Manual_Novo_Ilitera_Net_C23.pdf";
                Response.Redirect("~/ControleEPI/Fornecimento_Lista.aspx?IdEmpresa=" + Session["Empresa"].ToString() + " ");
            }

            else if (xModulo == "Alerta de EPI")
            {
                Session["Ajuda"] = "~/Help/Manual_Novo_Ilitera_Net_C23.pdf";
                Response.Redirect("~/ControleEPI/Alerta_EPI.aspx?IdEmpresa=" + Session["Empresa"].ToString() + " ");
            }
            else if (xModulo == "Repositório EPI")
            {
                Session["Ajuda"] = "~/Help/Manual_Novo_Ilitera_Net_C23.pdf";
                Response.Redirect("~/ControleEPI/EPIRepositorio.aspx?IdEmpresa=" + Session["Empresa"].ToString() + "&IdEmpregado=0 ");
            }
            else if (xModulo == "Repositório Documentos/Laudos")
            {
                Session["Ajuda"] = "~/Help/Manual_Novo_Ilitera_Net_C23.pdf";
                Response.Redirect("~/Repositorio.aspx?IdEmpresa=" + Session["Empresa"].ToString() + "&IdEmpregado=0 ");
            }
            else if (xModulo == "CA e Periodicidade")
            {
                Session["Ajuda"] = "~/Help/Manual_Novo_Ilitera_Net_C24.pdf";
                Response.Redirect("~/ControleEPI/CadastroEPI.aspx");
            }
            else if (xModulo == "Compra de EPI")
            {
                Session["Ajuda"] = "~/Help/Manual_Novo_Ilitera_Net_C26.pdf";
                Response.Redirect("~/ControleEPI/CadastroCompra.aspx");
            }
            else if (xModulo == "Atualização Manual do Estoque")
            {
                Session["Ajuda"] = "~/Help/Manual_Novo_Ilitera_Net_C26.pdf";
                Response.Redirect("~/ControleEPI/ControleEstoque.aspx");
            }
            else if (xModulo == "Histórico de Entrega")
            {
                Session["Ajuda"] = "~/Help/Manual_Novo_Ilitera_Net_C25.pdf";
                Response.Redirect("~/ControleEPI/HistoricoEntrega.aspx");
            }
            else if (xModulo == "Mapa de Riscos")
            {
                Session["Ajuda"] = "~/Help/Manual_Novo_Ilitera_Net_C20.pdf";
                Response.Redirect("~/CheckMapas.aspx");
            }
            else if (xModulo == "PAE")
            {
                Session["Ajuda"] = "~/Help/Manual_Novo_Ilitera_Net_C20.pdf";
                Response.Redirect("~/CheckPAE.aspx");
            }
            else if (xModulo == "Vasos de Pressão")
            {
                Session["Ajuda"] = "~/Help/Manual_Novo_Ilitera_Net_C20.pdf";
                Response.Redirect("~/CheckVasos.aspx");
            }
            else if (xModulo == "Elétrica")
            {
                Session["Ajuda"] = "~/Help/Manual_Novo_Ilitera_Net_C20.pdf";
                Response.Redirect("~/CheckLaudoEletricoPeriodos.aspx");
            }
            else if (xModulo == "SPDA")
            {
                Session["Ajuda"] = "~/Help/Manual_Novo_Ilitera_Net_C20.pdf";
                Response.Redirect("~/CheckLaudoSPDAPeriodos.aspx");
            }
            else if (xModulo == "Colaborador x GHE")
            {
                Session["Ajuda"] = "~/Help/Manual_Novo_Ilitera_Net_C05.pdf";
                Response.Redirect("~/SelecaoGHE.aspx?Colaborador=0");
            }
            else if (xModulo == "PPRA")
            {
                Session["Ajuda"] = "~/Help/Manual_Novo_Ilitera_Net_C17.pdf";
                Response.Redirect("~/CheckPPRAPeriodos.aspx");
            }
            else if (xModulo == "PGR")
            {
                Session["Ajuda"] = "~/Help/Manual_Novo_Ilitera_Net_C17.pdf";
                Response.Redirect("~/CheckPGRPeriodos.aspx");
            }
            else if (xModulo == "PCA / PPR")
            {
                Session["Ajuda"] = "~/Help/Manual_Novo_Ilitera_Net_C17.pdf";
                Response.Redirect("~/CheckPCAPeriodos.aspx");
            }
            else if (xModulo == "PCMSO")
            {
                Session["Ajuda"] = "~/Help/Manual_Novo_Ilitera_Net_C19.pdf";
                Response.Redirect("~/CheckPCMSOPeriodos.aspx");
            }
            else if (xModulo == "Ergonomia")
            {
                Session["Ajuda"] = "~/Help/Manual_Novo_Ilitera_Net_C18.pdf";
                Response.Redirect("~/CheckLaudoErgonomicoPeriodo.aspx");
            }
            else if (xModulo == "Gerar Eventos Manual")
            {
                Session["Ajuda"] = "~/Help/Manual_Novo_Ilitera_Net.pdf";
                Response.Redirect("~/esocial/Gerar_eSocial.aspx?IdEmpresa=" + Session["Empresa"].ToString());
            }
            else if (xModulo.IndexOf("2210") >= 0)
            {
                Session["Ajuda"] = "~/Help/Manual_Novo_Ilitera_Net.pdf";
                Response.Redirect("~/esocial/Controle_eSocial_2210.aspx?IdEmpresa=" + Session["Empresa"].ToString());
            }
            else if (xModulo.IndexOf("2220") >= 0)
            {
                Session["Ajuda"] = "~/Help/Manual_Novo_Ilitera_Net.pdf";
                Response.Redirect("~/esocial/Controle_eSocial_2220.aspx?IdEmpresa=" + Session["Empresa"].ToString());
            }
            else if (xModulo.IndexOf("2221") >= 0)
            {
                Session["Ajuda"] = "~/Help/Manual_Novo_Ilitera_Net.pdf";
                Response.Redirect("~/esocial/Controle_eSocial_2221.aspx?IdEmpresa=" + Session["Empresa"].ToString());
            }
            else if (xModulo.IndexOf("2240")>=0)
            {
                Session["Ajuda"] = "~/Help/Manual_Novo_Ilitera_Net.pdf";
                Response.Redirect("~/esocial/Controle_eSocial_2240.aspx?IdEmpresa=" + Session["Empresa"].ToString());
            }        
            else if (xModulo == "Carga XML")
            {
                Session["Ajuda"] = "~/Help/Manual_Novo_Ilitera_Net.pdf";
                if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("PRAJNA_HOM") > 0)
                    Response.Redirect("~/CheckXML.aspx?IdEmpresa=" + Session["Empresa"].ToString());
            }
            else if (xModulo == "Importação Planilha")
            {
                Session["Ajuda"] = "~/Help/Manual_Novo_Ilitera_Net.pdf";
                //if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("PRAJNA_HOM") > 0)
                    Response.Redirect("~/Importar_Planilha.aspx?IdEmpresa=" + Session["Empresa"].ToString());
            }
            else if (xModulo == "Importação Planilha Transf.")
            {
                Session["Ajuda"] = "~/Help/Manual_Novo_Ilitera_Net.pdf";
                //if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("PRAJNA_HOM") > 0)
                Response.Redirect("~/Importar_Planilha_Transf.aspx?IdEmpresa=" + Session["Empresa"].ToString());
            }
            else if (xModulo == "Sorteio Toxicológico")
            {

                Session["Ajuda"] = "~/Help/Manual_Novo_Ilitera_Net.pdf";
                Response.Redirect("~\\Toxicologico_Sorteio.aspx?IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString());
            }
            else if (xModulo == "Procedimentos")
            {
                Session["Ajuda"] = "~/Help/Manual_Novo_Ilitera_Net_C27.pdf";
                Response.Redirect("~/OrdemdeServico2/ListaProcedimento.aspx");
            }
            else if (xModulo == "Conjunto de Procedimentos")
            {
                Session["Ajuda"] = "~/Help/Manual_Novo_Ilitera_Net_C29.pdf";
                Response.Redirect("~/OrdemdeServico2/CadConjuntoProcedimento.aspx");
            }
            else if (xModulo == "Ordens de Serviço")
            {
                Session["Ajuda"] = "~/Help/Manual_Novo_Ilitera_Net_C27.pdf";
                Response.Redirect("~/OrdemdeServico2/ListaProcedimentoAPR.aspx");
            }
            else if (xModulo == "Proc.Empregados")
            {
                Session["Ajuda"] = "~/Help/Manual_Novo_Ilitera_Net_C30.pdf";
                Response.Redirect("~/OrdemdeServico2/CadEmpregadoConjuntoProcedimento.aspx");
            }
            else if (xModulo == "Realizados")
            {
                Session["Ajuda"] = "~/Help/Manual_Novo_Ilitera_Net_C31.pdf";
                Response.Redirect("~/Treinamentos/ListaTreinamentos.aspx");
            }
            else if (xModulo == "Certificados")
            {
                Session["Ajuda"] = "~/Help/Manual_Novo_Ilitera_Net_C31.pdf";
                Response.Redirect("~/Treinamentos/EmpregadoTreinamento.aspx");
            }
            else if (xModulo == "Exames e Pendências")
            {
                Session["Ajuda"] = "~/Help/Manual_Novo_Ilitera_Net.pdf";
                Response.Redirect("~/ListaPendencias.aspx");
            }
            else if (xModulo == "Constituição")
            {
                Session["Ajuda"] = "~/Help/Manual_Novo_Ilitera_Net_C21.pdf";
                Response.Redirect("~/ControleCIPA/IndicarCIPA.aspx");
            }
            else if (xModulo == "Atas de Reunião")
            {
                Session["Ajuda"] = "~/Help/Manual_Novo_Ilitera_Net_C22.pdf";
                Response.Redirect("~/ControleCIPA/ListaReunioesCIPA.aspx");
            }
            else if (xModulo == "Repositório CIPA")
            {
                Session["Ajuda"] = "~/Help/Manual_Novo_Ilitera_Net_C22.pdf";
                Response.Redirect("~/ControleCIPA/CipaRepositorio.aspx");
            }
            else if (xModulo == "Cipa")
            {
                Session["Ajuda"] = "~/Help/Manual_Novo_Ilitera_Net_C21.pdf";
                Response.Redirect("~/Cipa_New.aspx");
            }
            else if (xModulo == "Cadastro de Extintores")
            {
                Session["Ajuda"] = "~/Help/Manual_Novo_Ilitera_Net_C32.pdf";
                Response.Redirect("~/ControleExtintores/ListaExtintores.aspx");
            }
            else if (xModulo == "Equipamentos/Calibração")
            {
                Session["Ajuda"] = "~/Help/Manual_Novo_Ilitera_Net_C32.pdf";
                Response.Redirect("~/Auditoria/EquipCalibracao.aspx");
            }
            else if (xModulo == "Análise Laboratorial")
            {
                Session["Ajuda"] = "~/Help/Manual_Novo_Ilitera_Net_C32.pdf";
                Response.Redirect("~/Auditoria/AnaliseLaboratorial.aspx");
            }            
            else if (xModulo == "Cadastro de Uniformes")
            {
                Session["Ajuda"] = "~/Help/Manual_Novo_Ilitera_Net_C33.pdf";
                Response.Redirect("~/PCMSO/ListaUniformes.aspx");
            }
            else if (xModulo == "Relatório de ASO com bloqueio")  //Relatório de Asos gerados em bloqueio
            {
                Response.Redirect("~\\PCMSO\\Relatorio_Guias.aspx?IdEmpregado=" + Session["Empregado"].ToString() + "&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString() + "&xTipo=B");
            }
            else if (xModulo == "Relatório de Absenteísmo")  //Relatório de Absenteismo
            {
                Response.Redirect("~\\PCMSO\\Relatorio_Absenteismo.aspx?IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString() + "&xTipo=B");
            }
            else if (xModulo == "Gráficos")  //Relatório de Absenteismo
            {
                Response.Redirect("~\\PCMSO\\Graficos.aspx?IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString() + "&xTipo=B");
            }
            else if (xModulo == "Relatório de Exames Ambulatoriais")  //Relatório de Absenteismo
            {
                Response.Redirect("~\\PCMSO\\Relatorio_Ambulatoriais.aspx?IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString() + "&xTipo=B");
            }
            else if (xModulo == "Relatório de Guias Geradas")  //Relatório de Guias geradas
            {
                Response.Redirect("~\\PCMSO\\Relatorio_Guias.aspx?IdEmpregado=" + Session["Empregado"].ToString() + "&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString() + "&xTipo=G");
            }
            else if (xModulo == "Relatório de Exames Gerados")  //Relatório de exames
            {
                Response.Redirect("~\\PCMSO\\Relatorio_Exames.aspx?IdEmpregado=" + Session["Empregado"].ToString() + "&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString());
            }
            else if (xModulo == "Relatório de Mailing")  //Relatório de exames
            {
                Response.Redirect("~\\PCMSO\\Relatorio_Mailing.aspx?IdEmpregado=" + Session["Empregado"].ToString() + "&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString());
            }
            else if (xModulo == "Guia de Encaminhamento")  //Guia Encaminhamento
            {
                if (Session["NomeEmpregado"] == null)
                {
                    MsgBox1.Show("Ilitera.Net", "Necessário selecionar colaborador!", null, new EO.Web.MsgBoxButton("OK"));
                    return;
                }
                if (Session["NomeEmpregado"].ToString() == String.Empty)
                {
                    MsgBox1.Show("Ilitera.Net", "Necessário selecionar colaborador!", null, new EO.Web.MsgBoxButton("OK"));
                    return;
                }
                Session["Ajuda"] = "~/Help/Manual_Novo_Ilitera_Net_C10.pdf";
                Response.Redirect("~\\PCMSO\\DadosEmpregado_Guia.aspx?IdEmpregado=" + Session["Empregado"].ToString() + "&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString());
            }
            else if (xModulo == "Guias de Encaminhamentos Separadas")  //Guia Encaminhamento
            {
                if (Session["NomeEmpregado"] == null)
                {
                    MsgBox1.Show("Ilitera.Net", "Necessário selecionar colaborador!", null, new EO.Web.MsgBoxButton("OK"));
                    return;
                }
                if (Session["NomeEmpregado"].ToString() == String.Empty)
                {
                    MsgBox1.Show("Ilitera.Net", "Necessário selecionar colaborador!", null, new EO.Web.MsgBoxButton("OK"));
                    return;
                }
                Session["Ajuda"] = "~/Help/Manual_Novo_Ilitera_Net_C10.pdf";
                Response.Redirect("~\\PCMSO\\DadosEmpregado_Guia_Dup.aspx?IdEmpregado=" + Session["Empregado"].ToString() + "&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString());
            }


            else if (xModulo == "Clínicos")  //"6")  //Clinicos
            {
                if (Session["NomeEmpregado"] == null)
                {
                    MsgBox1.Show("Ilitera.Net", "Necessário selecionar colaborador!", null, new EO.Web.MsgBoxButton("OK"));
                    return;
                }
                if (Session["NomeEmpregado"].ToString() == String.Empty)
                {
                    MsgBox1.Show("Ilitera.Net", "Necessário selecionar colaborador!", null, new EO.Web.MsgBoxButton("OK"));
                    return;
                }
                Session["Ajuda"] = "~/Help/Manual_Novo_Ilitera_Net_C06.pdf";
                Response.Redirect("~\\PCMSO\\DadosEmpregado.aspx?IdEmpregado=" + Session["Empregado"].ToString() + "&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString() + "&Tipo=1");

            }
            else if (xModulo == "Complementares")  //Complementares
            {
                if (Session["NomeEmpregado"] == null)
                {
                    MsgBox1.Show("Ilitera.Net", "Necessário selecionar colaborador!", null, new EO.Web.MsgBoxButton("OK"));
                    return;
                }
                if (Session["NomeEmpregado"].ToString() == String.Empty)
                {
                    MsgBox1.Show("Ilitera.Net", "Necessário selecionar colaborador!", null, new EO.Web.MsgBoxButton("OK"));
                    return;
                }
                Session["Ajuda"] = "~/Help/Manual_Novo_Ilitera_Net_C07.pdf";
                Response.Redirect("~\\PCMSO\\DadosEmpregado.aspx?IdEmpregado=" + Session["Empregado"].ToString() + "&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString() + "&Tipo=3");

            }
            else if (xModulo == "Audiométricos")  //Audiometricos
            {
                if (Session["NomeEmpregado"] == null)
                {
                    MsgBox1.Show("Ilitera.Net", "Necessário selecionar colaborador!", null, new EO.Web.MsgBoxButton("OK"));
                    return;
                }
                if (Session["NomeEmpregado"].ToString() == String.Empty)
                {
                    MsgBox1.Show("Ilitera.Net", "Necessário selecionar colaborador!", null, new EO.Web.MsgBoxButton("OK"));
                    return;
                }
                Session["Ajuda"] = "~/Help/Manual_Novo_Ilitera_Net_C08.pdf";
                Response.Redirect("~\\PCMSO\\DadosEmpregado.aspx?IdEmpregado=" + Session["Empregado"].ToString() + "&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString() + "&Tipo=4");

            }
            else if (xModulo == "Não-Ocupacionais" || xModulo == "Enfermagem")  //Ambulatoriais
            {
                if (Session["NomeEmpregado"] == null)
                {
                    MsgBox1.Show("Ilitera.Net", "Necessário selecionar colaborador!", null, new EO.Web.MsgBoxButton("OK"));
                    return;
                }
                if (Session["NomeEmpregado"].ToString() == String.Empty)
                {
                    MsgBox1.Show("Ilitera.Net", "Necessário selecionar colaborador!", null, new EO.Web.MsgBoxButton("OK"));
                    return;
                }

                Session["Ajuda"] = "~/Help/Manual_Novo_Ilitera_Net_C09.pdf";
                Response.Redirect("~\\PCMSO\\DadosEmpregado.aspx?IdEmpregado=" + Session["Empregado"].ToString() + "&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString() + "&Tipo=2");
            }
            else if (xModulo == "Vacinação")  //Ambulatoriais
            {
                if (Session["NomeEmpregado"] == null)
                {
                    MsgBox1.Show("Ilitera.Net", "Necessário selecionar colaborador!", null, new EO.Web.MsgBoxButton("OK"));
                    return;
                }
                if (Session["NomeEmpregado"].ToString() == String.Empty)
                {
                    MsgBox1.Show("Ilitera.Net", "Necessário selecionar colaborador!", null, new EO.Web.MsgBoxButton("OK"));
                    return;
                }

                Session["Ajuda"] = "~/Help/Manual_Novo_Ilitera_Net_C09.pdf";
                Response.Redirect("~\\PCMSO\\DadosEmpregado_Vacina.aspx?IdEmpregado=" + Session["Empregado"].ToString() + "&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString() + "&Tipo=8");
            }
            else if (xModulo == "Vacinas - Setor")  //Ambulatoriais
            {
                Session["Ajuda"] = "~/Help/Manual_Novo_Ilitera_Net_C09.pdf";
                Response.Redirect("~/VacSetor.aspx?IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString() + "&Tipo=8",false);
            }
            else if (xModulo == "PCI")  //PCI
            {
                if (Session["NomeEmpregado"] == null)
                {
                    MsgBox1.Show("Ilitera.Net", "Necessário selecionar colaborador!", null, new EO.Web.MsgBoxButton("OK"));
                    return;
                }
                if (Session["NomeEmpregado"].ToString() == String.Empty)
                {
                    MsgBox1.Show("Ilitera.Net", "Necessário selecionar colaborador!", null, new EO.Web.MsgBoxButton("OK"));
                    return;
                }
                Session["Ajuda"] = "~/Help/Manual_Novo_Ilitera_Net.pdf";
                //Response.Redirect("~\\PCMSO\\DadosEmpregado.aspx?IdEmpregado=" + Session["Empregado"].ToString() + "&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString() + "&Tipo=6");
                Response.Redirect("~\\PCMSO\\DadosEmpregado_Lista.aspx?IdEmpregado=" + Session["Empregado"].ToString() + "&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString() + "&Tipo=6");
            }
            else if (xModulo == "Ficha Clínica")  //PCI
            {
                if (Session["NomeEmpregado"] == null)
                {
                    MsgBox1.Show("Ilitera.Net", "Necessário selecionar colaborador!", null, new EO.Web.MsgBoxButton("OK"));
                    return;
                }
                if (Session["NomeEmpregado"].ToString() == String.Empty)
                {
                    MsgBox1.Show("Ilitera.Net", "Necessário selecionar colaborador!", null, new EO.Web.MsgBoxButton("OK"));
                    return;
                }
                Session["Ajuda"] = "~/Help/Manual_Novo_Ilitera_Net.pdf";
                Response.Redirect("~\\PCMSO\\DadosEmpregado.aspx?IdEmpregado=" + Session["Empregado"].ToString() + "&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString() + "&Tipo=6");

            }


            else if (xModulo == "Riscos e EPI")  //Riscos e EPIs
            {
                if (Session["NomeEmpregado"] == null)
                {
                    MsgBox1.Show("Ilitera.Net", "Necessário selecionar colaborador!", null, new EO.Web.MsgBoxButton("OK"));
                    return;
                }
                if (Session["NomeEmpregado"].ToString() == String.Empty)
                {
                    MsgBox1.Show("Ilitera.Net", "Necessário selecionar colaborador!", null, new EO.Web.MsgBoxButton("OK"));
                    return;
                }
                Session["Ajuda"] = "~/Help/Manual_Novo_Ilitera_Net_C11.pdf";
                Response.Redirect("~\\PCMSO\\RiscosEPIs.aspx?IdEmpregado=" + Session["Empregado"].ToString());
            }
            else if (xModulo == "Prontuários Digitais")
            {
                if (Session["NomeEmpregado"] == null)
                {
                    MsgBox1.Show("Ilitera.Net", "Necessário selecionar colaborador!", null, new EO.Web.MsgBoxButton("OK"));
                    return;
                }
                if (Session["NomeEmpregado"].ToString() == String.Empty)
                {
                    MsgBox1.Show("Ilitera.Net", "Necessário selecionar colaborador!", null, new EO.Web.MsgBoxButton("OK"));
                    return;
                }
                Session["Ajuda"] = "~/Help/Manual_Novo_Ilitera_Net_C12.pdf";
                Response.Redirect("~\\PCMSO\\DadosEmpregado_Digitalizado.aspx?IdEmpregado=" + Session["Empregado"].ToString() + "&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString());
            }
            else if (xModulo == "PPP")
            {
                if (Session["NomeEmpregado"] == null)
                {
                    MsgBox1.Show("Ilitera.Net", "Necessário selecionar colaborador!", null, new EO.Web.MsgBoxButton("OK"));
                    return;
                }
                if (Session["NomeEmpregado"].ToString() == String.Empty)
                {
                    MsgBox1.Show("Ilitera.Net", "Necessário selecionar colaborador!", null, new EO.Web.MsgBoxButton("OK"));
                    return;
                }
                Session["Ajuda"] = "~/Help/Manual_Novo_Ilitera_Net_C16.pdf";
                Response.Redirect("~\\CheckPPPEmpregado.aspx?IdEmpregado=" + Session["Empregado"].ToString() + "&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString());
            }
            else if (xModulo == "LTCAT")
            {
                if (Session["NomeEmpregado"] == null)
                {
                    MsgBox1.Show("Ilitera.Net", "Necessário selecionar colaborador!", null, new EO.Web.MsgBoxButton("OK"));
                    return;
                }
                if (Session["NomeEmpregado"].ToString() == String.Empty)
                {
                    MsgBox1.Show("Ilitera.Net", "Necessário selecionar colaborador!", null, new EO.Web.MsgBoxButton("OK"));
                    return;
                }
                Session["Ajuda"] = "~/Help/Manual_Novo_Ilitera_Net.pdf";
                Response.Redirect("~\\CheckLTCAT.aspx?IdEmpregado=" + Session["Empregado"].ToString() + "&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString());

            }
            else if (xModulo == "Laudo de Insalubridade")
            {
                if (Session["NomeEmpregado"].ToString() == String.Empty)
                {
                    MsgBox1.Show("Ilitera.Net", "Necessário selecionar colaborador!", null, new EO.Web.MsgBoxButton("OK"));
                    return;
                }
                Session["Ajuda"] = "~/Help/Manual_Novo_Ilitera_Net.pdf";
                Response.Redirect("~\\CheckInsalubridade.aspx?IdEmpregado=" + Session["Empregado"].ToString() + "&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString());
            }
            else if (xModulo == "Laudo de Periculosidade")
            {
                Session["Ajuda"] = "~/Help/Manual_Novo_Ilitera_Net.pdf";
                Response.Redirect("~\\CheckPericulosidade.aspx?IdEmpregado=" + Session["Empregado"].ToString() + "&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString());
            }
            else if (xModulo == "Relatório de Colaboradores")
            {
                Session["Ajuda"] = "~/Help/Manual_Novo_Ilitera_Net_C04_1.pdf";
                Response.Redirect("~\\PCMSO\\Relatorio_Empregados.aspx");
            }
            else if (xModulo == "Relatório de Colaboradores e Aptidões" || xModulo == "Relatórios de Colaboradores e Aptidões")
            {
                Session["Ajuda"] = "~/Help/Manual_Novo_Ilitera_Net_C04_1.pdf";
                Response.Redirect("~\\PCMSO\\Relatorio_Empregados_Aptidoes.aspx");
            }
            else if (xModulo == "Relatório PCD")
            {
                Session["Ajuda"] = "~/Help/Manual_Novo_Ilitera_Net_C04_1.pdf";
                Response.Redirect("~\\PCMSO\\Relatorio_PCD.aspx");
            }
            else if (xModulo == "Relatório de Vacinação")
            {
                Session["Ajuda"] = "~/Help/Manual_Novo_Ilitera_Net_C04_1.pdf";
                Response.Redirect("~\\PCMSO\\Relatorio_Vacinas.aspx");
            }
            else if (xModulo == "Convocação de Vacinação")
            {
                Session["Ajuda"] = "~/Help/Manual_Novo_Ilitera_Net_C04_1.pdf";
                Response.Redirect("~\\PCMSO\\Relatorio_Vacinas_Atrasadas.aspx");
            }
            else if (xModulo == "Relatório de GHE - Função - Exames")
            {
                Session["Ajuda"] = "~/Help/Manual_Novo_Ilitera_Net_C04_1.pdf";
                Response.Redirect("~\\PCMSO\\Relatorio_GHE_Funcao_Exames.aspx");
            }
            else if (xModulo == "Gerar Alertas")
            {
                Session["Ajuda"] = "~/Help/Manual_Novo_Ilitera_Net_C04_1.pdf";
                Response.Redirect("~\\Auditoria\\AlertSettings.aspx");
            }
            else if (xModulo == "Auditoria/Irregularidades")
            {
                Session["Ajuda"] = "~/Help/Manual_Novo_Ilitera_Net_C04_1.pdf";
                Response.Redirect("~\\Auditoria\\ListagemIrregularidades.aspx");
            }
            else if (xModulo == "Quadros NR-4")
            {
                Session["Ajuda"] = "~/Help/Manual_Novo_Ilitera_Net_C04_1.pdf";
                Response.Redirect("~\\CheckNR4.aspx");
            }

            
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();


        }


        public void ItemMenu(object sender, EventArgs e)
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

            //var myLI = (HtmlControl)sender;

            //string xModulo = ((EO.Web.MenuItem)e.NavigationItem).Text.ToString().Trim();

            string xModulo = ((System.Web.UI.HtmlControls.HtmlContainerControl)sender).InnerText;

            if (xModulo == "Sair do<br>\r\nSistema")
            {
                Session["Pagina"] = "1";
                Session["Empregado"] = String.Empty;
                Session["NomeEmpregado"] = String.Empty;
                Session["NomeEmpresa"] = String.Empty;
                Session["Preposto"] = String.Empty;
                Session["Preposto2"] = String.Empty;



                Session["Filtro_Nome"] = "";
                Session["Filtro_Empresa"] = "";
                Session["Filtro_Tipo"] = "";
                Session["Filtro_Setor"] = "";

                //Page.ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.open('close.html', '_self', null);", true);

                if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("PRAJNA") > 0)
                    Page.ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.open('close_essence.html', '_self', null);", true);
                else
                    Page.ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.open('close.html', '_self', null);", true);


                return;
            }


            if (xModulo == "Ajuda do<br>\r\nSistema")
            {
                ////string path = Server.MapPath(Session["Ajuda"].ToString());
                //string path = Session["Ajuda"].ToString();

                String strPathAndQuery = HttpContext.Current.Request.Url.PathAndQuery;
                ////String strUrl = HttpContext.Current.Request.Url.AbsoluteUri.Replace(strPathAndQuery, "/");

                //WebClient client = new WebClient();
                //Byte[] buffer = client.DownloadData(HttpContext.Current.Request.Url.AbsoluteUri.Replace(strPathAndQuery, "/") + path.Substring(2));
                //if (buffer != null)
                //{
                //    Response.ContentType = "application/pdf";
                //    Response.AddHeader("content-length", buffer.Length.ToString());
                //    Response.BinaryWrite(buffer);
                //}

                ////Process process = new Process();
                ////process.StartInfo.UseShellExecute = true;
                ////process.StartInfo.FileName = HttpContext.Current.Request.Url.AbsoluteUri.Replace(strPathAndQuery, "/") + path.Substring(2);
                ////process.Start();

                //string xPath = HttpContext.Current.Request.Url.AbsoluteUri.Replace(strPathAndQuery, "/") + "help.aspx";

                string xPath = "";
                xPath = HttpContext.Current.Request.Url.AbsoluteUri;

                try
                {

                    xPath = xPath.Replace(HttpContext.Current.Request.Url.Segments[2].ToString(), "");
                    xPath = xPath.Replace(HttpContext.Current.Request.Url.Segments[3].ToString(), "");
                    xPath = xPath.Replace(HttpContext.Current.Request.Url.Segments[4].ToString(), "");
                    xPath = xPath.Replace(HttpContext.Current.Request.Url.Segments[5].ToString(), "");

                }
                catch
                {

                }

                xPath = xPath + "help.aspx";


                //? HttpContext.Current.Request.Url.Segments
                //{string[3]}
                //    [0]: "/"
                //    [1]: "ControleEPI/"
                //    [2]: "EntregaEPI.aspx"
                //? HttpContext.Current.Request.Url.Segments
                //{string[2]}
                //    [0]: "/"
                //    [1]: "ListaEmpresas2.aspx"

                Response.Write("<script>");
                Response.Write("window.open('" + xPath + "','_blank')");
                Response.Write("</script>");



            }


            if (Session["NomeEmpresa"] == null)
            {
                //MsgBox1.Show("Ilitera.Net", "Necessário selecionar empresa!", null,
                //              new EO.Web.MsgBoxButton("OK"));
                return;
            }

            if (Session["NomeEmpresa"].ToString() == String.Empty)
            {
                //MsgBox1.Show("Ilitera.Net", "Necessário selecionar empresa!", null,
                //              new EO.Web.MsgBoxButton("OK"));
                return;

            }

            Entities.Usuario user = (Entities.Usuario)Session["usuarioLogado"];


            if (xModulo == "Listar Empresas")
            {
                Session["Ajuda"] = "~/Help/Manual_Novo_Ilitera_Net_C03.pdf";
                Response.Redirect("~/ListaEmpresas2.aspx");
            }
            if (xModulo == "Dados Cadastrais")
            {
                Session["Ajuda"] = "~/Help/Manual_Novo_Ilitera_Net_C03.pdf";
                Response.Redirect("~/VisualizarDadosEmpresa.aspx");
            }
            else if (xModulo == "Listar Colaboradores")
            {
                Session["Ajuda"] = "~/Help/Manual_Novo_Ilitera_Net_C04.pdf";
                Response.Redirect("~/ListaEmpregados.aspx");
            }
            else if (xModulo == "Inserir Colaborador")
            {
                Session["Ajuda"] = "~/Help/Manual_Novo_Ilitera_Net_C04_2.pdf";
                Response.Redirect("~/InserirDadosEmpregado.aspx");
            }
            else if (xModulo == "Edição do Colaborador")
            {
                if (Session["NomeEmpregado"] == null)
                {
                    MsgBox1.Show("Ilitera.Net", "Necessário selecionar colaborador!", null, new EO.Web.MsgBoxButton("OK"));
                    return;
                }
                if (Session["NomeEmpregado"].ToString() == String.Empty)
                {
                    MsgBox1.Show("Ilitera.Net", "Necessário selecionar colaborador!", null, new EO.Web.MsgBoxButton("OK"));
                    return;
                }
                Session["Ajuda"] = "~/Help/Manual_Novo_Ilitera_Net_C04_1.pdf";
                Response.Redirect("~/VisualizarDadosEmpregado_Novo2.aspx");
            }
            else if (xModulo == "Fornecimento de EPI Manual")
            {
                Session["Ajuda"] = "~/Help/Manual_Novo_Ilitera_Net_C23.pdf";
                Response.Redirect("~/ControleEPI/EntregaEPI.aspx");
            }
            else if (xModulo == "Fornecimento de EPI Automático")
            {
                Session["Ajuda"] = "~/Help/Manual_Novo_Ilitera_Net_C23.pdf";
                Response.Redirect("~/ControleEPI/EntregaEPI_Auto.aspx?IdEmpresa=" + Session["Empresa"].ToString() + " ");
            }
            else if (xModulo == "Alerta de EPI")
            {
                Session["Ajuda"] = "~/Help/Manual_Novo_Ilitera_Net_C23.pdf";
                Response.Redirect("~/ControleEPI/Alerta_EPI.aspx?IdEmpresa=" + Session["Empresa"].ToString() + " ");
            }
            else if (xModulo == "CA e Periodicidade")
            {
                Session["Ajuda"] = "~/Help/Manual_Novo_Ilitera_Net_C24.pdf";
                Response.Redirect("~/ControleEPI/CadastroEPI.aspx");
            }
            else if (xModulo == "Compra de EPI")
            {
                Session["Ajuda"] = "~/Help/Manual_Novo_Ilitera_Net_C26.pdf";
                Response.Redirect("~/ControleEPI/CadastroCompra.aspx");
            }
            else if (xModulo == "Atualização Manual do Estoque")
            {
                Session["Ajuda"] = "~/Help/Manual_Novo_Ilitera_Net_C26.pdf";
                Response.Redirect("~/ControleEPI/ControleEstoque.aspx");
            }
            else if (xModulo == "Histórico de Entrega")
            {
                Session["Ajuda"] = "~/Help/Manual_Novo_Ilitera_Net_C25.pdf";
                Response.Redirect("~/ControleEPI/HistoricoEntrega.aspx");
            }
            else if (xModulo == "Mapa de Riscos")
            {
                Session["Ajuda"] = "~/Help/Manual_Novo_Ilitera_Net_C20.pdf";
                Response.Redirect("~/CheckMapas.aspx");
            }
            else if (xModulo == "Elétrica")
            {
                Session["Ajuda"] = "~/Help/Manual_Novo_Ilitera_Net_C20.pdf";
                Response.Redirect("~/CheckLaudoEletricoPeriodos.aspx");
            }
            else if (xModulo == "SPDA")
            {
                Session["Ajuda"] = "~/Help/Manual_Novo_Ilitera_Net_C20.pdf";
                Response.Redirect("~/CheckLaudoSPDAPeriodos.aspx");
            }
            else if (xModulo == "Colaborador x GHE")
            {
                Session["Ajuda"] = "~/Help/Manual_Novo_Ilitera_Net_C05.pdf";
                Response.Redirect("~/SelecaoGHE.aspx?Colaborador=0");
            }
            else if (xModulo == "PPRA")
            {
                Session["Ajuda"] = "~/Help/Manual_Novo_Ilitera_Net_C17.pdf";
                Response.Redirect("~/CheckPPRAPeriodos.aspx");
            }
            else if (xModulo == "PCA / PPR" || xModulo == "PCA" || xModulo == "PPR")
            {
                Session["Ajuda"] = "~/Help/Manual_Novo_Ilitera_Net_C17.pdf";
                Response.Redirect("~/CheckPCAPeriodos.aspx");
            }
            else if (xModulo == "PCMSO")
            {
                Session["Ajuda"] = "~/Help/Manual_Novo_Ilitera_Net_C19.pdf";
                Response.Redirect("~/CheckPCMSOPeriodos.aspx");
            }
            else if (xModulo == "Ergonomia")
            {
                Session["Ajuda"] = "~/Help/Manual_Novo_Ilitera_Net_C18.pdf";
                Response.Redirect("~/CheckLaudoErgonomicoPeriodo.aspx");
            }
            else if (xModulo == "e-Social")
            {
                Session["Ajuda"] = "~/Help/Manual_Novo_Ilitera_Net.pdf";
                Response.Redirect("~/eSocial.aspx?IdEmpresa=" + Session["Empresa"].ToString());
            }
            else if (xModulo == "Procedimentos")
            {
                Session["Ajuda"] = "~/Help/Manual_Novo_Ilitera_Net_C27.pdf";
                Response.Redirect("~/OrdemdeServico2/ListaProcedimento.aspx");
            }
            else if (xModulo == "Conjunto de Procedimentos")
            {
                Session["Ajuda"] = "~/Help/Manual_Novo_Ilitera_Net_C29.pdf";
                Response.Redirect("~/OrdemdeServico2/CadConjuntoProcedimento.aspx");
            }
            else if (xModulo == "Ordens de Serviço")
            {
                Session["Ajuda"] = "~/Help/Manual_Novo_Ilitera_Net_C27.pdf";
                Response.Redirect("~/OrdemdeServico2/ListaProcedimentoAPR.aspx");
            }
            else if (xModulo == "Proc. Empregados")
            {
                Session["Ajuda"] = "~/Help/Manual_Novo_Ilitera_Net_C30.pdf";
                Response.Redirect("~/OrdemdeServico2/CadEmpregadoConjuntoProcedimento.aspx");
            }
            else if (xModulo == "Realizados")
            {
                Session["Ajuda"] = "~/Help/Manual_Novo_Ilitera_Net_C31.pdf";
                Response.Redirect("~/Treinamentos/ListaTreinamentos.aspx");
            }
            else if (xModulo == "Certificados")
            {
                Session["Ajuda"] = "~/Help/Manual_Novo_Ilitera_Net_C31.pdf";
                Response.Redirect("~/Treinamentos/EmpregadoTreinamento.aspx");
            }
            else if (xModulo == "Exames e Pendências")
            {
                Session["Ajuda"] = "~/Help/Manual_Novo_Ilitera_Net.pdf";
                Response.Redirect("~/ListaPendencias.aspx");
            }
            else if (xModulo == "Constituição")
            {
                Session["Ajuda"] = "~/Help/Manual_Novo_Ilitera_Net_C21.pdf";
                Response.Redirect("~/ControleCIPA/IndicarCIPA.aspx");
            }
            else if (xModulo == "Atas de Reunião")
            {
                Session["Ajuda"] = "~/Help/Manual_Novo_Ilitera_Net_C22.pdf";
                Response.Redirect("~/ControleCIPA/ListaReunioesCIPA.aspx");
            }
            else if (xModulo == "Cadastro de Extintores")
            {
                Session["Ajuda"] = "~/Help/Manual_Novo_Ilitera_Net_C32.pdf";
                Response.Redirect("~/ControleExtintores/ListaExtintores.aspx");
            }
            else if (xModulo == "Cadastro de Uniformes")
            {
                Session["Ajuda"] = "~/Help/Manual_Novo_Ilitera_Net_C33.pdf";
                Response.Redirect("~/PCMSO/ListaUniformes.aspx");
            }
            else if (xModulo == "Repositório EPI")
            {
                Session["Ajuda"] = "~/Help/Manual_Novo_Ilitera_Net_C23.pdf";
                Response.Redirect("~/ControleEPI/EPIRepositorio.aspx?IdEmpresa=" + Session["Empresa"].ToString() + "&IdEmpregado=0 ");
            }
            else if (xModulo == "Repositório Documentos/Laudos")
            {
                Session["Ajuda"] = "~/Help/Manual_Novo_Ilitera_Net_C23.pdf";
                Response.Redirect("~/Repositorio.aspx?IdEmpresa=" + Session["Empresa"].ToString() + "&IdEmpregado=0 ");
            }
            else if (xModulo == "Relatório de ASO com bloqueio")  //Relatório de Asos gerados em bloqueio
            {
                Response.Redirect("~\\PCMSO\\Relatorio_Guias.aspx?IdEmpregado=" + Session["Empregado"].ToString() + "&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString() + "&xTipo=B");
            }
            else if (xModulo == "Relatório de Absenteísmo")  //Relatório de Absenteismo
            {
                Response.Redirect("~\\PCMSO\\Relatorio_Absenteismo.aspx?IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString() + "&xTipo=B");
            }
            else if (xModulo == "Relatório de Exames Ambulatoriais")  //Relatório de Absenteismo
            {
                Response.Redirect("~\\PCMSO\\Relatorio_Ambulatoriais.aspx?IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString() + "&xTipo=B");
            }
            else if (xModulo == "Relatório de Guias Geradas")  //Relatório de Guias geradas
            {
                Response.Redirect("~\\PCMSO\\Relatorio_Guias.aspx?IdEmpregado=" + Session["Empregado"].ToString() + "&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString() + "&xTipo=G");
            }
            else if (xModulo == "Relatório de Exames Gerados")  //Relatório de exames
            {
                Response.Redirect("~\\PCMSO\\Relatorio_Exames.aspx?IdEmpregado=" + Session["Empregado"].ToString() + "&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString());
            }
            else if (xModulo == "Relatório de Mailing")  //Relatório de exames
            {
                Response.Redirect("~\\PCMSO\\Relatorio_Mailing.aspx?IdEmpregado=" + Session["Empregado"].ToString() + "&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString());
            }
            else if (xModulo == "Gráficos")  //Relatório de Absenteismo
            {
                Response.Redirect("~\\PCMSO\\Graficos.aspx?IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString() + "&xTipo=B");
            }
            else if (xModulo == "Guia de Encaminhamento")  //Guia Encaminhamento
            {
                if (Session["NomeEmpregado"] == null)
                {
                    MsgBox1.Show("Ilitera.Net", "Necessário selecionar colaborador!", null, new EO.Web.MsgBoxButton("OK"));
                    return;
                }
                if (Session["NomeEmpregado"].ToString() == String.Empty)
                {
                    MsgBox1.Show("Ilitera.Net", "Necessário selecionar colaborador!", null, new EO.Web.MsgBoxButton("OK"));
                    return;
                }
                Session["Ajuda"] = "~/Help/Manual_Novo_Ilitera_Net_C10.pdf";
                Response.Redirect("~\\PCMSO\\DadosEmpregado_Guia.aspx?IdEmpregado=" + Session["Empregado"].ToString() + "&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString());
            }
            else if (xModulo == "Guias de Encaminhamentos Separadas" || xModulo == "Guias de Encaminhamento Separadas")  //Guia Encaminhamento
            {
                if (Session["NomeEmpregado"] == null)
                {
                    MsgBox1.Show("Ilitera.Net", "Necessário selecionar colaborador!", null, new EO.Web.MsgBoxButton("OK"));
                    return;
                }
                if (Session["NomeEmpregado"].ToString() == String.Empty)
                {
                    MsgBox1.Show("Ilitera.Net", "Necessário selecionar colaborador!", null, new EO.Web.MsgBoxButton("OK"));
                    return;
                }
                Session["Ajuda"] = "~/Help/Manual_Novo_Ilitera_Net_C10.pdf";
                Response.Redirect("~\\PCMSO\\DadosEmpregado_Guia_Dup.aspx?IdEmpregado=" + Session["Empregado"].ToString() + "&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString());
            }
            else if (xModulo == "Vasos de Pressão" || xModulo == "Caldeira")
            {
                Session["Ajuda"] = "~/Help/Manual_Novo_Ilitera_Net_C20.pdf";
                Response.Redirect("~/CheckVasos.aspx");
            }

            else if (xModulo == "Clínicos" || xModulo == "Exames Clínicos")  //"6")  //Clinicos
            {
                if (Session["NomeEmpregado"] == null)
                {
                    MsgBox1.Show("Ilitera.Net", "Necessário selecionar colaborador!", null, new EO.Web.MsgBoxButton("OK"));
                    return;
                }
                if (Session["NomeEmpregado"].ToString() == String.Empty)
                {
                    MsgBox1.Show("Ilitera.Net", "Necessário selecionar colaborador!", null, new EO.Web.MsgBoxButton("OK"));
                    return;
                }
                Session["Ajuda"] = "~/Help/Manual_Novo_Ilitera_Net_C06.pdf";
                Response.Redirect("~\\PCMSO\\DadosEmpregado.aspx?IdEmpregado=" + Session["Empregado"].ToString() + "&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString() + "&Tipo=1");

            }
            else if (xModulo == "Complementares" || xModulo == "Exames Complementares")  //Complementares
            {
                if (Session["NomeEmpregado"] == null)
                {
                    MsgBox1.Show("Ilitera.Net", "Necessário selecionar colaborador!", null, new EO.Web.MsgBoxButton("OK"));
                    return;
                }
                if (Session["NomeEmpregado"].ToString() == String.Empty)
                {
                    MsgBox1.Show("Ilitera.Net", "Necessário selecionar colaborador!", null, new EO.Web.MsgBoxButton("OK"));
                    return;
                }
                Session["Ajuda"] = "~/Help/Manual_Novo_Ilitera_Net_C07.pdf";
                Response.Redirect("~\\PCMSO\\DadosEmpregado.aspx?IdEmpregado=" + Session["Empregado"].ToString() + "&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString() + "&Tipo=3");

            }
            else if (xModulo == "Audiométricos" || xModulo == "Exames Audiométricos")  //Audiometricos
            {
                if (Session["NomeEmpregado"] == null)
                {
                    MsgBox1.Show("Ilitera.Net", "Necessário selecionar colaborador!", null, new EO.Web.MsgBoxButton("OK"));
                    return;
                }
                if (Session["NomeEmpregado"].ToString() == String.Empty)
                {
                    MsgBox1.Show("Ilitera.Net", "Necessário selecionar colaborador!", null, new EO.Web.MsgBoxButton("OK"));
                    return;
                }
                Session["Ajuda"] = "~/Help/Manual_Novo_Ilitera_Net_C08.pdf";
                Response.Redirect("~\\PCMSO\\DadosEmpregado.aspx?IdEmpregado=" + Session["Empregado"].ToString() + "&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString() + "&Tipo=4");

            }
            else if (xModulo == "Não-Ocupacionais" || xModulo == "Exames Não Ocupacionais")  //Ambulatoriais
            {
                if (Session["NomeEmpregado"] == null)
                {
                    MsgBox1.Show("Ilitera.Net", "Necessário selecionar colaborador!", null, new EO.Web.MsgBoxButton("OK"));
                    return;
                }
                if (Session["NomeEmpregado"].ToString() == String.Empty)
                {
                    MsgBox1.Show("Ilitera.Net", "Necessário selecionar colaborador!", null, new EO.Web.MsgBoxButton("OK"));
                    return;
                }

                Session["Ajuda"] = "~/Help/Manual_Novo_Ilitera_Net_C09.pdf";
                Response.Redirect("~\\PCMSO\\DadosEmpregado.aspx?IdEmpregado=" + Session["Empregado"].ToString() + "&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString() + "&Tipo=2");
            }
            else if (xModulo == "Vacinação" || xModulo == "Plano de Vacinação")  //Ambulatoriais
            {
                if (Session["NomeEmpregado"] == null)
                {
                    MsgBox1.Show("Ilitera.Net", "Necessário selecionar colaborador!", null, new EO.Web.MsgBoxButton("OK"));
                    return;
                }
                if (Session["NomeEmpregado"].ToString() == String.Empty)
                {
                    MsgBox1.Show("Ilitera.Net", "Necessário selecionar colaborador!", null, new EO.Web.MsgBoxButton("OK"));
                    return;
                }

                Session["Ajuda"] = "~/Help/Manual_Novo_Ilitera_Net_C09.pdf";
                Response.Redirect("~\\PCMSO\\DadosEmpregado_Vacina.aspx?IdEmpregado=" + Session["Empregado"].ToString() + "&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString() + "&Tipo=8");
            }
            else if (xModulo == "Vacinas - Setor" || xModulo == "Vacinação por Setor")  //Ambulatoriais
            {
                Session["Ajuda"] = "~/Help/Manual_Novo_Ilitera_Net_C09.pdf";
                Response.Redirect("~/VacSetor.aspx?IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString() + "&Tipo=8",false);
            }
            else if (xModulo == "PCI" || xModulo == "PCI Prontuário Clínico Individual")  //PCI
            {
                if (Session["NomeEmpregado"] == null)
                {
                    MsgBox1.Show("Ilitera.Net", "Necessário selecionar colaborador!", null, new EO.Web.MsgBoxButton("OK"));
                    return;
                }
                if (Session["NomeEmpregado"].ToString() == String.Empty)
                {
                    MsgBox1.Show("Ilitera.Net", "Necessário selecionar colaborador!", null, new EO.Web.MsgBoxButton("OK"));
                    return;
                }
                Session["Ajuda"] = "~/Help/Manual_Novo_Ilitera_Net.pdf";
                //Response.Redirect("~\\PCMSO\\DadosEmpregado.aspx?IdEmpregado=" + Session["Empregado"].ToString() + "&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString() + "&Tipo=6");
                Response.Redirect("~\\PCMSO\\DadosEmpregado_Lista.aspx?IdEmpregado=" + Session["Empregado"].ToString() + "&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString() + "&Tipo=6");
            }
            else if (xModulo == "Ficha Clínica")  //PCI
            {
                if (Session["NomeEmpregado"] == null)
                {
                    MsgBox1.Show("Ilitera.Net", "Necessário selecionar colaborador!", null, new EO.Web.MsgBoxButton("OK"));
                    return;
                }
                if (Session["NomeEmpregado"].ToString() == String.Empty)
                {
                    MsgBox1.Show("Ilitera.Net", "Necessário selecionar colaborador!", null, new EO.Web.MsgBoxButton("OK"));
                    return;
                }
                Session["Ajuda"] = "~/Help/Manual_Novo_Ilitera_Net.pdf";
                Response.Redirect("~\\PCMSO\\DadosEmpregado.aspx?IdEmpregado=" + Session["Empregado"].ToString() + "&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString() + "&Tipo=6");

            }
            else if (xModulo == "Proc.Empregados")
            {
                Session["Ajuda"] = "~/Help/Manual_Novo_Ilitera_Net_C30.pdf";
                Response.Redirect("~/OrdemdeServico2/CadEmpregadoConjuntoProcedimento.aspx");
            }

            else if (xModulo == "Riscos e EPI")  //Riscos e EPIs
            {
                if (Session["NomeEmpregado"] == null)
                {
                    MsgBox1.Show("Ilitera.Net", "Necessário selecionar colaborador!", null, new EO.Web.MsgBoxButton("OK"));
                    return;
                }
                if (Session["NomeEmpregado"].ToString() == String.Empty)
                {
                    MsgBox1.Show("Ilitera.Net", "Necessário selecionar colaborador!", null, new EO.Web.MsgBoxButton("OK"));
                    return;
                }
                Session["Ajuda"] = "~/Help/Manual_Novo_Ilitera_Net_C11.pdf";
                Response.Redirect("~\\PCMSO\\RiscosEPIs.aspx?IdEmpregado=" + Session["Empregado"].ToString());
            }
            else if (xModulo == "Prontuários Digitais")
            {
                if (Session["NomeEmpregado"] == null)
                {
                    MsgBox1.Show("Ilitera.Net", "Necessário selecionar colaborador!", null, new EO.Web.MsgBoxButton("OK"));
                    return;
                }
                if (Session["NomeEmpregado"].ToString() == String.Empty)
                {
                    MsgBox1.Show("Ilitera.Net", "Necessário selecionar colaborador!", null, new EO.Web.MsgBoxButton("OK"));
                    return;
                }
                Session["Ajuda"] = "~/Help/Manual_Novo_Ilitera_Net_C12.pdf";
                Response.Redirect("~\\PCMSO\\DadosEmpregado_Digitalizado.aspx?IdEmpregado=" + Session["Empregado"].ToString() + "&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString());
            }
            else if (xModulo == "PPP")
            {
                if (Session["NomeEmpregado"] == null)
                {
                    MsgBox1.Show("Ilitera.Net", "Necessário selecionar colaborador!", null, new EO.Web.MsgBoxButton("OK"));
                    return;
                }
                if (Session["NomeEmpregado"].ToString() == String.Empty)
                {
                    MsgBox1.Show("Ilitera.Net", "Necessário selecionar colaborador!", null, new EO.Web.MsgBoxButton("OK"));
                    return;
                }
                Session["Ajuda"] = "~/Help/Manual_Novo_Ilitera_Net_C16.pdf";
                Response.Redirect("~\\CheckPPPEmpregado.aspx?IdEmpregado=" + Session["Empregado"].ToString() + "&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString());
            }
            else if (xModulo == "LTCAT")
            {
                if (Session["NomeEmpregado"] == null)
                {
                    MsgBox1.Show("Ilitera.Net", "Necessário selecionar colaborador!", null, new EO.Web.MsgBoxButton("OK"));
                    return;
                }
                if (Session["NomeEmpregado"].ToString() == String.Empty)
                {
                    MsgBox1.Show("Ilitera.Net", "Necessário selecionar colaborador!", null, new EO.Web.MsgBoxButton("OK"));
                    return;
                }
                Session["Ajuda"] = "~/Help/Manual_Novo_Ilitera_Net.pdf";
                Response.Redirect("~\\CheckLTCAT.aspx?IdEmpregado=" + Session["Empregado"].ToString() + "&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString());

            }
            else if (xModulo == "Laudo de Insalubridade")
            {
                if (Session["NomeEmpregado"].ToString() == String.Empty)
                {
                    MsgBox1.Show("Ilitera.Net", "Necessário selecionar colaborador!", null, new EO.Web.MsgBoxButton("OK"));
                    return;
                }
                Session["Ajuda"] = "~/Help/Manual_Novo_Ilitera_Net.pdf";
                Response.Redirect("~\\CheckInsalubridade.aspx?IdEmpregado=" + Session["Empregado"].ToString() + "&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString());
            }
            else if (xModulo == "Laudo de Periculosidade")
            {
                Session["Ajuda"] = "~/Help/Manual_Novo_Ilitera_Net.pdf";
                Response.Redirect("~\\CheckPericulosidade.aspx?IdEmpregado=" + Session["Empregado"].ToString() + "&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString());
            }
            else if (xModulo == "Relatório de Colaboradores" || xModulo == "Relatórios de Colaboradores")
            {
                Session["Ajuda"] = "~/Help/Manual_Novo_Ilitera_Net_C04_1.pdf";
                Response.Redirect("~\\PCMSO\\Relatorio_Empregados.aspx");
            }
            else if (xModulo == "Relatório de Colaboradores e Aptidões" || xModulo == "Relatórios de Colaboradores e Aptidões")
            {
                Session["Ajuda"] = "~/Help/Manual_Novo_Ilitera_Net_C04_1.pdf";
                Response.Redirect("~\\PCMSO\\Relatorio_Empregados_Aptidoes.aspx");
            }
            else if (xModulo == "Relatório PCD")
            {
                Session["Ajuda"] = "~/Help/Manual_Novo_Ilitera_Net_C04_1.pdf";
                Response.Redirect("~\\PCMSO\\Relatorio_PCD.aspx");
            }
            else if (xModulo == "Relatório de Vacinação")
            {
                Session["Ajuda"] = "~/Help/Manual_Novo_Ilitera_Net_C04_1.pdf";
                Response.Redirect("~\\PCMSO\\Relatorio_Vacinas.aspx");
            }
            else if (xModulo == "Convocação de Vacinação")
            {
                Session["Ajuda"] = "~/Help/Manual_Novo_Ilitera_Net_C04_1.pdf";
                Response.Redirect("~\\PCMSO\\Relatorio_Vacinas_Atrasadas.aspx");
            }
            else if (xModulo == "Relatório de GHE - Função - Exames")
            {
                Session["Ajuda"] = "~/Help/Manual_Novo_Ilitera_Net_C04_1.pdf";
                Response.Redirect("~\\PCMSO\\Relatorio_GHE_Funcao_Exames.aspx");
            }
            else if (xModulo == "Gerar Alertas")
            {
                Session["Ajuda"] = "~/Help/Manual_Novo_Ilitera_Net_C04_1.pdf";
                Response.Redirect("~\\Auditoria\\AlertSettings.aspx");
            }
            else if (xModulo == "Auditoria/Irregularidades")
            {
                Session["Ajuda"] = "~/Help/Manual_Novo_Ilitera_Net_C04_1.pdf";
                Response.Redirect("~\\Auditoria\\ListagemIrregularidades.aspx");
            }

            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();


        }


        private void Items_Menu_Essence(bool xEnable)
        {
            // habilitar / desabilitar via código
            EO.Web.NavigationItem[] xItem = Menu1.SearchItems("PGR", EO.Web.NavigationItemSearchOptions.SearchText);

            EO.Web.ElementStyle xEstilo = new EO.Web.ElementStyle();

            if (xEnable == false)
            {
                xEstilo.ForeColor = System.Drawing.Color.Gray;
                xItem[0].DisabledStyle = xEstilo;
                xItem[0].Disabled = true;
            }
            else
            {
                xEstilo.ForeColor = System.Drawing.Color.Black;
                xItem[0].DisabledStyle = xEstilo;
                xItem[0].Disabled = false;
            }

            //// DisabledStyle - CssText = "color:silver;"


            EO.Web.NavigationItem[] xItem2 = Menu1.SearchItems("PCMSO", EO.Web.NavigationItemSearchOptions.SearchText);

            EO.Web.ElementStyle xEstilo2 = new EO.Web.ElementStyle();

            if (xEnable == false)
            {
                xEstilo2.ForeColor = System.Drawing.Color.Gray;
                xItem2[0].DisabledStyle = xEstilo;
                xItem2[0].Disabled = true;
            }
            else
            {
                xEstilo2.ForeColor = System.Drawing.Color.Black;
                xItem2[0].DisabledStyle = xEstilo;
                xItem2[0].Disabled = false;
            }



        }


    }
}
