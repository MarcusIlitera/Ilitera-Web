using System;
using System.Collections.Generic;
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
using Ilitera.Common;
using Entities;
using BLL;
using System.Collections;

namespace Ilitera.Net
{
    public partial class DadosEmpregado : System.Web.UI.Page
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


            Session["Retorno"] = "1";
            
            InicializaWebPageObjects();
            //PreencheLabels("lblEmp", cliente.NomeAbreviado.ToString());

                

            string zTipo = Request["Tipo"].ToString().Trim();

            PopulaTelaEmpregado();

            if (zTipo == "2")
            {
                grd_Clinicos.DataSource = DSExamesClinicosNaoOcupacional();
            }
            else if (zTipo == "1")
            {
                grd_Clinicos.DataSource = DSExamesClinicos();
            }
            else if (zTipo == "3")
            {
                grd_Clinicos.DataSource = DSExamesComplementares();
            }
            else if (zTipo == "4")
            {
                grd_Clinicos.DataSource = DSExamesAudiometricos();
            }
            else if (zTipo == "5")
            {                
                grd_Clinicos.DataSource = DSExamesAudiometricos();
            }
            else if (zTipo == "6")
            {
                grd_Clinicos.DataSource = DSPCIs();
            }
            else if (zTipo == "7")   // ASO com guia de encaminhamento
            {
                grd_Clinicos.DataSource = DSExamesClinicos();
            }


            grd_Clinicos.DataBind();

            if (!IsPostBack)
            {

                if (Request.QueryString["IdUsuario"] != null
                    && Request.QueryString["IdUsuario"] != "")
                {
                    this.usuario = new Ilitera.Common.Usuario(Convert.ToInt32(Request.QueryString["IdUsuario"]));
                    this.usuario.IdPessoa.Find();

                    if (!this.usuario.NomeUsuario.Equals("lmm"))
                    {
                        this.prestador = new Prestador();
                        this.prestador.FindByPessoa(this.usuario.IdPessoa);
                        this.prestador.IdPessoa.Find();
                    }
                }

                if (Request.QueryString["IdEmpresa"] != null
                    && Request.QueryString["IdEmpresa"] != "")
                    this.cliente = new Cliente(Convert.ToInt32(Request.QueryString["IdEmpresa"]));

                if (Request.QueryString["IdEmpregado"] != null
                    && Request.QueryString["IdEmpregado"] != "")
                    this.empregado = new Ilitera.Opsa.Data.Empregado(Convert.ToInt32(Request.QueryString["IdEmpregado"]));


                int width = 680, height = 550;

                if (zTipo == "2")
                {

                    lblExCli.Text = "Exames Ambulatoriais/Enfermagem";

                    if (Ilitera.Data.SQLServer.EntitySQLServer._LocalServer.ToString().ToUpper().IndexOf("EY") > 0)
                        hlkNovo.NavigateUrl = "javascript:void(window.location.href ='ExameNaoOcupacional_EY.aspx?IdEmpregado=" + Request["IdEmpregado"] + "&IdUsuario=" + Request["IdUsuario"] + "&IdEmpresa=" + Request["IdEmpresa"] + "&Acao=E')";
                    else
                        hlkNovo.NavigateUrl = "javascript:void(window.location.href ='ExameNaoOcupacional.aspx?IdEmpregado=" + Request["IdEmpregado"] + "&IdUsuario=" + Request["IdUsuario"] + "&IdEmpresa=" + Request["IdEmpresa"] + "&Acao=E')";




                    //if (usuario.NomeUsuario.Equals("lmm"))
                    //    hlkNovo.NavigateUrl = "javascript:void(addItem(centerWin('ExameNaoOcupacional.aspx?IdEmpregado=" + Request["IdEmpregado"] + "&IdUsuario=" + Request["IdUsuario"] + "&IdEmpresa=" + Request["IdEmpresa"] + "'," + width + "," + height + ",\'EC\'),\'Todos\'))";
                    //else
                    //    switch (prestador.IndTipoPrestador)
                    //    {
                    //        case (int)TipoPrestador.Medico:
                    //            if (!cliente.ContrataPCMSO.Equals((int)TipoPcmsoContratada.NaoContratada))
                    //            {
                    //                prestador.IdJuridica.Find();

                    //                if (prestador.IdJuridica.Id == (int)Empresas.MestraPaulista) // Medico da Mestra
                    //                    hlkNovo.NavigateUrl = "javascript:void(addItem(centerWin('ExameNaoOcupacional.aspx?IdEmpregado=" + Request["IdEmpregado"] + "&IdUsuario=" + Request["IdUsuario"] + "&IdEmpresa=" + Request["IdEmpresa"] + "'," + width + "," + height + ",\'EC\'),\'Todos\'))";
                    //                else if (prestador.IdJuridica.IdJuridicaPapel.Id == (int)IndJuridicaPapel.Clinica)
                    //                {
                    //                    Clinica clinica = new Clinica(prestador.IdJuridica.Id);

                    //                    if (!clinica.IsClinicaInterna)
                    //                        hlkNovo.NavigateUrl = "javascript:window.alert('O usuário " + usuario.NomeUsuario + " não possui autorização para incluir um novo exame!')";
                    //                    else
                    //                        hlkNovo.NavigateUrl = "javascript:void(addItem(centerWin('ExameNaoOcupacional.aspx?IdEmpregado=" + Request["IdEmpregado"] + "&IdUsuario=" + Request["IdUsuario"] + "&IdEmpresa=" + Request["IdEmpresa"] + "'," + width + "," + height + ",\'EC\'),\'Todos\'))";
                    //                }
                    //                else if (prestador.IdJuridica.IdJuridicaPapel.Id == (int)IndJuridicaPapel.Cliente)
                    //                    hlkNovo.NavigateUrl = "javascript:void(addItem(centerWin('ExameNaoOcupacional.aspx?IdEmpregado=" + Request["IdEmpregado"] + "&IdUsuario=" + Request["IdUsuario"] + "&IdEmpresa=" + Request["IdEmpresa"] + "'," + width + "," + height + ",\'EC\'),\'Todos\'))";
                    //            }
                    //            else
                    //                hlkNovo.NavigateUrl = "javascript:void(addItem(centerWin('ExameNaoOcupacional.aspx?IdEmpregado=" + Request["IdEmpregado"] + "&IdUsuario=" + Request["IdUsuario"] + "&IdEmpresa=" + Request["IdEmpresa"] + "'," + width + "," + height + ",\'EC\'),\'Todos\'))";
                    //            break;
                    //        case (int)TipoPrestador.ContatoEmpresa:
                    //            if (!cliente.ContrataPCMSO.Equals((int)TipoPcmsoContratada.NaoContratada))
                    //                hlkNovo.NavigateUrl = "javascript:window.alert('O usuário " + usuario.NomeUsuario + " não é do tipo médico e não possui autorização para incluir um novo exame!')";
                    //            else
                    //                hlkNovo.NavigateUrl = "javascript:void(addItem(centerWin('ExameNaoOcupacional.aspx?IdEmpregado=" + Request["IdEmpregado"] + "&IdUsuario=" + Request["IdUsuario"] + "&IdEmpresa=" + Request["IdEmpresa"] + "'," + width + "," + height + ",\'EC\'),\'Todos\'))";
                    //            break;
                    //        default:
                    //            hlkNovo.NavigateUrl = "javascript:window.alert('O usuário " + usuario.NomeUsuario + " não é do tipo médico e não possui autorização para incluir um novo exame!')";
                    //            break;
                    //    }
                }
                else if (zTipo == "1")
                {

                    lblExCli.Text = "Exames Clínicos";


                    if (Ilitera.Data.SQLServer.EntitySQLServer._LocalServer.ToString().ToUpper().IndexOf("EY") > 0)
                        hlkNovo.NavigateUrl = "javascript:void(window.location.href ='ExameClinico_EY.aspx?IdEmpregado=" + Request["IdEmpregado"] + "&IdUsuario=" + Request["IdUsuario"] + "&IdEmpresa=" + Request["IdEmpresa"] + "&Acao=E')";
                    else
                        hlkNovo.NavigateUrl = "javascript:void(window.location.href ='ExameClinico.aspx?IdEmpregado=" + Request["IdEmpregado"] + "&IdUsuario=" + Request["IdUsuario"] + "&IdEmpresa=" + Request["IdEmpresa"]  + "&Acao=E')";


                }
                else if (zTipo == "7")
                {

                    lblExCli.Text = "ASO com Guia de Encaminhamento";


                    hlkNovo.NavigateUrl = "javascript:void(window.location.href ='ExameClinicoGuia.aspx?IdEmpregado=" + Request["IdEmpregado"] + "&IdUsuario=" + Request["IdUsuario"] + "&IdEmpresa=" + Request["IdEmpresa"] + "&Acao=E')";
                }

                else if (zTipo == "3")
                {

                    lblExCli.Text = "Exames Complementares";

                    hlkNovo.NavigateUrl = "javascript:void(window.location.href ='ExameComplementar.aspx?IdEmpregado=" + Request["IdEmpregado"] + "&IdUsuario=" + Request["IdUsuario"] + "&IdEmpresa=" + Request["IdEmpresa"] + "&Acao=E')";

                    //if (usuario.NomeUsuario.Equals("lmm"))
                    //    hlkNovo.NavigateUrl = "javascript:void(addItem(centerWin('ExameComplementar.aspx?IdEmpregado=" + Request["IdEmpregado"] + "&IdUsuario=" + Request["IdUsuario"] + "&IdEmpresa=" + Request["IdEmpresa"] + "'," + width + "," + height + ",\'EC\'),\'Todos\'))";
                    //else
                    //    switch (prestador.IndTipoPrestador)
                    //    {
                    //        case (int)TipoPrestador.Medico:
                    //            if (!cliente.ContrataPCMSO.Equals((int)TipoPcmsoContratada.NaoContratada))
                    //            {
                    //                prestador.IdJuridica.Find();

                    //                if (prestador.IdJuridica.Id == (int)Empresas.MestraPaulista) // Medico da Mestra
                    //                    hlkNovo.NavigateUrl = "javascript:void(addItem(centerWin('ExameComplementar.aspx?IdEmpregado=" + Request["IdEmpregado"] + "&IdUsuario=" + Request["IdUsuario"] + "&IdEmpresa=" + Request["IdEmpresa"] + "'," + width + "," + height + ",\'EC\'),\'Todos\'))";
                    //                else if (prestador.IdJuridica.IdJuridicaPapel.Id == (int)IndJuridicaPapel.Clinica)
                    //                {
                    //                    Clinica clinica = new Clinica(prestador.IdJuridica.Id);

                    //                    if (!clinica.IsClinicaInterna)
                    //                        hlkNovo.NavigateUrl = "javascript:window.alert('O usuário " + usuario.NomeUsuario + " não possui autorização para incluir um novo exame!')";
                    //                    else
                    //                        hlkNovo.NavigateUrl = "javascript:void(addItem(centerWin('ExameComplementar.aspx?IdEmpregado=" + Request["IdEmpregado"] + "&IdUsuario=" + Request["IdUsuario"] + "&IdEmpresa=" + Request["IdEmpresa"] + "'," + width + "," + height + ",\'EC\'),\'Todos\'))";
                    //                }
                    //                else if (prestador.IdJuridica.IdJuridicaPapel.Id == (int)IndJuridicaPapel.Cliente)
                    //                    hlkNovo.NavigateUrl = "javascript:void(addItem(centerWin('ExameComplementar.aspx?IdEmpregado=" + Request["IdEmpregado"] + "&IdUsuario=" + Request["IdUsuario"] + "&IdEmpresa=" + Request["IdEmpresa"] + "'," + width + "," + height + ",\'EC\'),\'Todos\'))";
                    //            }
                    //            else
                    //                hlkNovo.NavigateUrl = "javascript:void(addItem(centerWin('ExameComplementar.aspx?IdEmpregado=" + Request["IdEmpregado"] + "&IdUsuario=" + Request["IdUsuario"] + "&IdEmpresa=" + Request["IdEmpresa"] + "'," + width + "," + height + ",\'EC\'),\'Todos\'))";
                    //            break;
                    //        case (int)TipoPrestador.ContatoEmpresa:
                    //            if (!cliente.ContrataPCMSO.Equals((int)TipoPcmsoContratada.NaoContratada))
                    //                hlkNovo.NavigateUrl = "javascript:window.alert('O usuário " + usuario.NomeUsuario + " não é do tipo médico e não possui autorização para incluir um novo exame!')";
                    //            else
                    //                hlkNovo.NavigateUrl = "javascript:void(addItem(centerWin('ExameComplementar.aspx?IdEmpregado=" + Request["IdEmpregado"] + "&IdUsuario=" + Request["IdUsuario"] + "&IdEmpresa=" + Request["IdEmpresa"] + "'," + width + "," + height + ",\'EC\'),\'Todos\'))";
                    //            break;
                    //        default:
                    //            hlkNovo.NavigateUrl = "javascript:window.alert('O usuário " + usuario.NomeUsuario + " não é do tipo médico e não possui autorização para incluir um novo exame!')";
                    //            break;
                    //    }
                }

                else if (zTipo == "4")
                {

                    lblExCli.Text = "Exames Audiométricos";

                    hlkNovo.NavigateUrl = "javascript:void(window.location.href ='ExameAudiometrico.aspx?IdEmpregado=" + Request["IdEmpregado"] + "&IdUsuario=" + Request["IdUsuario"] + "&IdEmpresa=" + Request["IdEmpresa"] + "&Acao=E')";


                    //if (usuario.NomeUsuario.Equals("lmm"))
                    //    hlkNovo.NavigateUrl = "javascript:void(addItem(centerWin('ExameAudiometrico.aspx?IdEmpregado=" + Request["IdEmpregado"] + "&IdUsuario=" + Request["IdUsuario"] + "&IdEmpresa=" + Request["IdEmpresa"] + "'," + width + "," + height + ",\'EC\'),\'Todos\'))";
                    //else
                    //    switch (prestador.IndTipoPrestador)
                    //    {
                    //        case (int)TipoPrestador.Medico:
                    //            if (!cliente.ContrataPCMSO.Equals((int)TipoPcmsoContratada.NaoContratada))
                    //            {
                    //                prestador.IdJuridica.Find();

                    //                if (prestador.IdJuridica.Id == (int)Empresas.MestraPaulista) // Medico da Mestra
                    //                    hlkNovo.NavigateUrl = "javascript:void(addItem(centerWin('ExameAudiometrico.aspx?IdEmpregado=" + Request["IdEmpregado"] + "&IdUsuario=" + Request["IdUsuario"] + "&IdEmpresa=" + Request["IdEmpresa"] + "'," + width + "," + height + ",\'EC\'),\'Todos\'))";
                    //                else if (prestador.IdJuridica.IdJuridicaPapel.Id == (int)IndJuridicaPapel.Clinica)
                    //                {
                    //                    Clinica clinica = new Clinica(prestador.IdJuridica.Id);

                    //                    if (!clinica.IsClinicaInterna)
                    //                        hlkNovo.NavigateUrl = "javascript:window.alert('O usuário " + usuario.NomeUsuario + " não possui autorização para incluir um novo exame!')";
                    //                    else
                    //                        hlkNovo.NavigateUrl = "javascript:void(addItem(centerWin('ExameAudiometrico.aspx?IdEmpregado=" + Request["IdEmpregado"] + "&IdUsuario=" + Request["IdUsuario"] + "&IdEmpresa=" + Request["IdEmpresa"] + "'," + width + "," + height + ",\'EC\'),\'Todos\'))";
                    //                }
                    //                else if (prestador.IdJuridica.IdJuridicaPapel.Id == (int)IndJuridicaPapel.Cliente)
                    //                    hlkNovo.NavigateUrl = "javascript:void(addItem(centerWin('ExameAudiometrico.aspx?IdEmpregado=" + Request["IdEmpregado"] + "&IdUsuario=" + Request["IdUsuario"] + "&IdEmpresa=" + Request["IdEmpresa"] + "'," + width + "," + height + ",\'EC\'),\'Todos\'))";
                    //            }
                    //            else
                    //                hlkNovo.NavigateUrl = "javascript:void(addItem(centerWin('ExameAudiometrico.aspx?IdEmpregado=" + Request["IdEmpregado"] + "&IdUsuario=" + Request["IdUsuario"] + "&IdEmpresa=" + Request["IdEmpresa"] + "'," + width + "," + height + ",\'EC\'),\'Todos\'))";
                    //            break;
                    //        case (int)TipoPrestador.ContatoEmpresa:
                    //            if (!cliente.ContrataPCMSO.Equals((int)TipoPcmsoContratada.NaoContratada))
                    //                hlkNovo.NavigateUrl = "javascript:window.alert('O usuário " + usuario.NomeUsuario + " não é do tipo médico e não possui autorização para incluir um novo exame!')";
                    //            else
                    //                hlkNovo.NavigateUrl = "javascript:void(addItem(centerWin('ExameAudiometrico.aspx?IdEmpregado=" + Request["IdEmpregado"] + "&IdUsuario=" + Request["IdUsuario"] + "&IdEmpresa=" + Request["IdEmpresa"] + "'," + width + "," + height + ",\'EC\'),\'Todos\'))";
                    //            break;
                    //        default:
                    //            hlkNovo.NavigateUrl = "javascript:window.alert('O usuário " + usuario.NomeUsuario + " não é do tipo médico e não possui autorização para incluir um novo exame!')";
                    //            break;
                    //    }


                }

                else if (zTipo == "5")
                {

                    lblExCli.Text = "Exames Digitalizados";

                    if (usuario.NomeUsuario.Equals("lmm"))
                        hlkNovo.NavigateUrl = "javascript:void(addItem(centerWin('ExameDigitalizado.aspx?IdEmpregado=" + Request["IdEmpregado"] + "&IdUsuario=" + Request["IdUsuario"] + "&IdEmpresa=" + Request["IdEmpresa"] + "&Acao=E'," + width + "," + height + ",\'EC\'),\'Todos\'))";
                    else
                        switch (prestador.IndTipoPrestador)
                        {
                            case (int)TipoPrestador.Medico:
                                if (!cliente.ContrataPCMSO.Equals((int)TipoPcmsoContratada.NaoContratada))
                                {
                                    prestador.IdJuridica.Find();

                                    if (prestador.IdJuridica.Id == (int)Empresas.MestraPaulista) // Medico da Mestra
                                        hlkNovo.NavigateUrl = "javascript:void(addItem(centerWin('ExameDigitalizado.aspx?IdEmpregado=" + Request["IdEmpregado"] + "&IdUsuario=" + Request["IdUsuario"] + "&IdEmpresa=" + Request["IdEmpresa"] + "&Acao=E'," + width + "," + height + ",\'EC\'),\'Todos\'))";
                                    else if (prestador.IdJuridica.IdJuridicaPapel.Id == (int)IndJuridicaPapel.Clinica)
                                    {
                                        Clinica clinica = new Clinica(prestador.IdJuridica.Id);

                                        if (!clinica.IsClinicaInterna)
                                            hlkNovo.NavigateUrl = "javascript:window.alert('O usuário " + usuario.NomeUsuario + " não possui autorização para incluir um novo exame!')";
                                        else
                                            hlkNovo.NavigateUrl = "javascript:void(addItem(centerWin('ExameDigitalizado.aspx?IdEmpregado=" + Request["IdEmpregado"] + "&IdUsuario=" + Request["IdUsuario"] + "&IdEmpresa=" + Request["IdEmpresa"] + "&Acao=E'," + width + "," + height + ",\'EC\'),\'Todos\'))";
                                    }
                                    else if (prestador.IdJuridica.IdJuridicaPapel.Id == (int)IndJuridicaPapel.Cliente)
                                        hlkNovo.NavigateUrl = "javascript:void(addItem(centerWin('ExameDigitalizado.aspx?IdEmpregado=" + Request["IdEmpregado"] + "&IdUsuario=" + Request["IdUsuario"] + "&IdEmpresa=" + Request["IdEmpresa"] + "&Acao=E'," + width + "," + height + ",\'EC\'),\'Todos\'))";
                                }
                                else
                                    hlkNovo.NavigateUrl = "javascript:void(addItem(centerWin('ExameDigitalizado.aspx?IdEmpregado=" + Request["IdEmpregado"] + "&IdUsuario=" + Request["IdUsuario"] + "&IdEmpresa=" + Request["IdEmpresa"] + "&Acao=E'," + width + "," + height + ",\'EC\'),\'Todos\'))";
                                break;
                            case (int)TipoPrestador.ContatoEmpresa:
                                if (!cliente.ContrataPCMSO.Equals((int)TipoPcmsoContratada.NaoContratada))
                                    hlkNovo.NavigateUrl = "javascript:window.alert('O usuário " + usuario.NomeUsuario + " não é do tipo médico e não possui autorização para incluir um novo exame!')";
                                else
                                    hlkNovo.NavigateUrl = "javascript:void(addItem(centerWin('ExameDigitalizado.aspx?IdEmpregado=" + Request["IdEmpregado"] + "&IdUsuario=" + Request["IdUsuario"] + "&IdEmpresa=" + Request["IdEmpresa"] + "'," + width + "," + height + ",\'EC\'),\'Todos\'))";
                                break;
                            default:
                                hlkNovo.NavigateUrl = "javascript:window.alert('O usuário " + usuario.NomeUsuario + " não é do tipo médico e não possui autorização para incluir um novo exame!')";
                                break;
                        }



                }


                else if (zTipo == "6")
                {

                    lblExCli.Text = "Ficha Clínica - Prontuário Digital";

                    hlkNovo.NavigateUrl = "javascript:void(window.location.href ='ExameDigitalizado.aspx?IdEmpregado=" + Request["IdEmpregado"] + "&IdUsuario=" + Request["IdUsuario"] + "&IdEmpresa=" + Request["IdEmpresa"] + "&Acao=E')";

                    //if (usuario.NomeUsuario.Equals("lmm"))
                    //    hlkNovo.NavigateUrl = "javascript:void(addItem(centerWin('ExameDigitalizado.aspx?IdEmpregado=" + Request["IdEmpregado"] + "&IdUsuario=" + Request["IdUsuario"] + "&IdEmpresa=" + Request["IdEmpresa"] + "'," + width + "," + height + ",\'EC\'),\'Todos\'))";
                    //else
                    //    switch (prestador.IndTipoPrestador)
                    //    {
                    //        case (int)TipoPrestador.Medico:
                    //            if (!cliente.ContrataPCMSO.Equals((int)TipoPcmsoContratada.NaoContratada))
                    //            {
                    //                prestador.IdJuridica.Find();

                    //                if (prestador.IdJuridica.Id == (int)Empresas.MestraPaulista) // Medico da Mestra
                    //                    hlkNovo.NavigateUrl = "javascript:void(addItem(centerWin('ExameDigitalizado.aspx?IdEmpregado=" + Request["IdEmpregado"] + "&IdUsuario=" + Request["IdUsuario"] + "&IdEmpresa=" + Request["IdEmpresa"] + "'," + width + "," + height + ",\'EC\'),\'Todos\'))";
                    //                else if (prestador.IdJuridica.IdJuridicaPapel.Id == (int)IndJuridicaPapel.Clinica)
                    //                {
                    //                    Clinica clinica = new Clinica(prestador.IdJuridica.Id);

                    //                    if (!clinica.IsClinicaInterna)
                    //                        hlkNovo.NavigateUrl = "javascript:window.alert('O usuário " + usuario.NomeUsuario + " não possui autorização para incluir um novo exame!')";
                    //                    else
                    //                        hlkNovo.NavigateUrl = "javascript:void(addItem(centerWin('ExameDigitalizado.aspx?IdEmpregado=" + Request["IdEmpregado"] + "&IdUsuario=" + Request["IdUsuario"] + "&IdEmpresa=" + Request["IdEmpresa"] + "'," + width + "," + height + ",\'EC\'),\'Todos\'))";
                    //                }
                    //                else if (prestador.IdJuridica.IdJuridicaPapel.Id == (int)IndJuridicaPapel.Cliente)
                    //                    hlkNovo.NavigateUrl = "javascript:void(addItem(centerWin('ExameDigitalizado.aspx?IdEmpregado=" + Request["IdEmpregado"] + "&IdUsuario=" + Request["IdUsuario"] + "&IdEmpresa=" + Request["IdEmpresa"] + "'," + width + "," + height + ",\'EC\'),\'Todos\'))";
                    //            }
                    //            else
                    //                hlkNovo.NavigateUrl = "javascript:void(addItem(centerWin('ExameDigitalizado.aspx?IdEmpregado=" + Request["IdEmpregado"] + "&IdUsuario=" + Request["IdUsuario"] + "&IdEmpresa=" + Request["IdEmpresa"] + "'," + width + "," + height + ",\'EC\'),\'Todos\'))";
                    //            break;
                    //        case (int)TipoPrestador.ContatoEmpresa:
                    //            if (!cliente.ContrataPCMSO.Equals((int)TipoPcmsoContratada.NaoContratada))
                    //                hlkNovo.NavigateUrl = "javascript:window.alert('O usuário " + usuario.NomeUsuario + " não é do tipo médico e não possui autorização para incluir um novo exame!')";
                    //            else
                    //                hlkNovo.NavigateUrl = "javascript:void(addItem(centerWin('ExameDigitalizado.aspx?IdEmpregado=" + Request["IdEmpregado"] + "&IdUsuario=" + Request["IdUsuario"] + "&IdEmpresa=" + Request["IdEmpresa"] + "'," + width + "," + height + ",\'EC\'),\'Todos\'))";
                    //            break;
                    //        default:
                    //            hlkNovo.NavigateUrl = "javascript:window.alert('O usuário " + usuario.NomeUsuario + " não é do tipo médico e não possui autorização para incluir um novo exame!')";
                    //            break;
                    //    }


                    ////GetMenu(((int)IndMenuType.Empregado).ToString(), Request["IdUsuario"].ToString(), Request["IdEmpresa"].ToString(), Request["IdEmpregado"].ToString());

                    //StringBuilder st = new StringBuilder();


                    //st.Append("var URL = \"PastaExClinico.aspx?IdUsuario=\" + top.window.document.frames.conteudo.window.document.getElementById('txtIdUsuario').value + \"&"
                    //	+"IdEmpresa=\" + top.window.document.frames.conteudo.window.document.getElementById('txtIdEmpresa').value + \"&"
                    //	+"IdEmpregado=\" + top.window.document.frames.conteudo.window.document.getElementById('txtIdEmpregado').value;"); 

                    //problemas com chrome - abaixo
                    //st.Append("var URL = \"PastaExClinico.aspx?IdUsuario=\" + top.window.document.getElementById('txtIdUsuario').value + \"&"
                    //    + "IdEmpresa=\" + top.window.document.getElementById('txtIdEmpresa').value + \"&"
                    //    + "IdEmpregado=\" + top.window.document.getElementById('txtIdEmpregado').value;");

                    //st.Append("var IFrameObj = document.frames['SubDados'];");
                    //st.Append("IFrameObj.document.location.replace(URL);");

                    //this.ClientScript.RegisterStartupScript(this.GetType(), "ChangeFrameSrc", st.ToString(), true);

                    //Response.Redirect("PastaExClinico.aspx?IdUsuario=" + lbl_Id_Usuario.Text + "&IdEmpresa=" + lbl_Id_Empresa.Text);
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
            //base.InicializaWebPageObjects();
            StringBuilder st = new StringBuilder();

            //st.Append("top.window.document.frames.conteudo.window.document.getElementById('txtIdEmpregado').value = '" + empregado.Id + "';");
            st.Append("top.window.document.getElementById('txtIdEmpregado').value = '" + Request["IdEmpregado"] + "';");
            st.Append("top.window.document.getElementById('txtIdUsuario').value = '" + Request["IdUsuario"] + "';");
            st.Append("top.window.document.getElementById('txtIdEmpresa').value = '" + Request["IdEmpresa"] + "';");


            this.ClientScript.RegisterStartupScript(this.GetType(), "IdEmpregado", st.ToString(), true);
            btnFichaCompleta.Attributes.Add("onClick", "addItem(centerWin('../DadosEmpresa/FichaCompleta.aspx?IdEmpregado=" + Request["IdEmpregado"] + "&IdEmpresa=" + Request["IdEmpresa"] + "&IdUsuario=" + Request["IdUsuario"] + "',560,320,\'FichaCompleta\'),\'Todos\'); Reload();");

        }


        protected void grd_Clinicos_ItemCommand(object sender, EO.Web.GridCommandEventArgs e)
        {
            //Check whether it is from our client side
            //JavaScript call
            string zAcao = "V";


            if (e.CommandName.ToString().Trim() == "2") 
                zAcao = "E";  //editar
            else
                zAcao = "V";  //visualizar


            //Session["Empregado"] = e.Item.Key.ToString();
            //Session["NomeEmpregado"] = e.Item.Cells[0].Value.ToString();
            string zTipo = Request["Tipo"].ToString().Trim();
                        
            if (zTipo == "2")
            {

                Entities.Usuario xUser = (Entities.Usuario)Session["usuarioLogado"];

                Ilitera.Common.Pessoa zPessoa = new Ilitera.Common.Pessoa();
                zPessoa.Find(xUser.IdPessoa);

                Prestador xPrestador = new Prestador();

                xPrestador = new Prestador();
                xPrestador.FindByPessoa(zPessoa);
                xPrestador.IdPessoa.Find();

                if (xPrestador.IndTipoPrestador == (int)TipoPrestador.Medico)
                {
                    //if (Ilitera.Data.SQLServer.EntitySQLServer._LocalServer.ToString().ToUpper().IndexOf("EY") > 0)
                    //    Response.Redirect("ExameNaoOcupacional_EY.aspx?IdEmpregado=" + Request["IdEmpregado"] + "&IdExame=" + e.Item.Key.ToString() + "&IdUsuario=" + Request["IdUsuario"] + "&IdEmpresa=" + Request["IdEmpresa"] + "&Acao=" + zAcao);
                    //else
                    Response.Redirect("ExameNaoOcupacional.aspx?IdEmpregado=" + Request["IdEmpregado"] + "&IdExame=" + e.Item.Key.ToString() + "&IdUsuario=" + Request["IdUsuario"] + "&IdEmpresa=" + Request["IdEmpresa"] + "&Acao=" + zAcao);
                }
                else
                {
                    MsgBox1.Show("Ilitera.Net", "Visualização deste exame apenas por prestadores com perfil médico!", null,
                                 new EO.Web.MsgBoxButton("OK"));
                    return;
                }


            }
            else if (zTipo == "1")
            {
                Entities.Usuario xUser = (Entities.Usuario)Session["usuarioLogado"];

                Ilitera.Common.Pessoa zPessoa = new Ilitera.Common.Pessoa();
                zPessoa.Find(xUser.IdPessoa);

                Prestador xPrestador = new Prestador();

                xPrestador = new Prestador();
                xPrestador.FindByPessoa(zPessoa);
                xPrestador.IdPessoa.Find();

                if (xPrestador.IndTipoPrestador == (int)TipoPrestador.Medico)
                {
                    //if (Ilitera.Data.SQLServer.EntitySQLServer._LocalServer.ToString().ToUpper().IndexOf("EY") > 0)
                    //    Response.Redirect("ExameClinico_EY.aspx?IdEmpregado=" + Request["IdEmpregado"] + "&IdExame=" + e.Item.Key.ToString() + "&IdUsuario=" + Request["IdUsuario"] + "&IdEmpresa=" + Request["IdEmpresa"] + "&Acao=" + zAcao);
                    //else
                    Response.Redirect("ExameClinico.aspx?IdEmpregado=" + Request["IdEmpregado"] + "&IdExame=" + e.Item.Key.ToString() + "&IdUsuario=" + Request["IdUsuario"] + "&IdEmpresa=" + Request["IdEmpresa"] + "&Acao=" + zAcao);
                }
                else
                {
                    MsgBox1.Show("Ilitera.Net", "Visualização deste exame apenas por prestadores com perfil médico!", null,
                                 new EO.Web.MsgBoxButton("OK"));
                    return;
                }



            }
            else if (zTipo == "3")
            {
                Entities.Usuario xUser = (Entities.Usuario)Session["usuarioLogado"];

                Ilitera.Common.Pessoa zPessoa = new Ilitera.Common.Pessoa();
                zPessoa.Find(xUser.IdPessoa);

                Prestador xPrestador = new Prestador();

                xPrestador = new Prestador();
                xPrestador.FindByPessoa(zPessoa);
                xPrestador.IdPessoa.Find();

                if (xPrestador.IndTipoPrestador == (int)TipoPrestador.Medico)
                {
                    Response.Redirect("ExameComplementar.aspx?IdEmpregado=" + Request["IdEmpregado"] + "&IdExame=" + e.Item.Key.ToString() + "&IdUsuario=" + Request["IdUsuario"] + "&IdEmpresa=" + Request["IdEmpresa"] + "&Acao=" + zAcao);
                }
                else
                {
                    MsgBox1.Show("Ilitera.Net", "Visualização deste exame apenas por prestadores com perfil médico!", null,
                                 new EO.Web.MsgBoxButton("OK"));
                    return;
                }

            }
            else if (zTipo == "4")
            {

                Entities.Usuario xUser = (Entities.Usuario)Session["usuarioLogado"];

                Ilitera.Common.Pessoa zPessoa = new Ilitera.Common.Pessoa();
                zPessoa.Find(xUser.IdPessoa);

                Prestador xPrestador = new Prestador();

                xPrestador = new Prestador();
                xPrestador.FindByPessoa(zPessoa);
                xPrestador.IdPessoa.Find();
                
                if (xPrestador.IndTipoPrestador == (int)TipoPrestador.Medico)
                {
                    Response.Redirect("ExameAudiometrico.aspx?IdEmpregado=" + Request["IdEmpregado"] + "&IdExame=" + e.Item.Key.ToString() + "&IdUsuario=" + Request["IdUsuario"] + "&IdEmpresa=" + Request["IdEmpresa"] + "&Acao=" + zAcao);
                }
                else
                {
                    MsgBox1.Show("Ilitera.Net", "Visualização deste exame apenas por prestadores com perfil médico!", null,
                                 new EO.Web.MsgBoxButton("OK"));
                    return;
                }

            }
            else if (zTipo == "5")
            {
                //digitalizados
                //grd_Clinicos.DataSource = DSExamesAudiometricos();
            }
            else if (zTipo == "6")
            {
                Entities.Usuario xUser = (Entities.Usuario)Session["usuarioLogado"];

                Ilitera.Common.Pessoa zPessoa = new Ilitera.Common.Pessoa();
                zPessoa.Find(xUser.IdPessoa);

                Prestador xPrestador = new Prestador();

                xPrestador = new Prestador();
                xPrestador.FindByPessoa(zPessoa);
                xPrestador.IdPessoa.Find();

                if (xPrestador.IndTipoPrestador == (int)TipoPrestador.Medico)
                {
                    Response.Redirect("ExameDigitalizado.aspx?IdEmpregado=" + Request["IdEmpregado"] + "&IdExame=" + e.Item.Key.ToString() + "&IdUsuario=" + Request["IdUsuario"] + "&IdEmpresa=" + Request["IdEmpresa"] + "&Acao=" + zAcao);
                }
                else
                {
                    MsgBox1.Show("Ilitera.Net", "Visualização deste exame apenas por prestadores com perfil médico!", null,
                                 new EO.Web.MsgBoxButton("OK"));
                    return;
                }

                
            }
            else if (zTipo == "7")
            {
                Entities.Usuario xUser = (Entities.Usuario)Session["usuarioLogado"];

                Ilitera.Common.Pessoa zPessoa = new Ilitera.Common.Pessoa();
                zPessoa.Find(xUser.IdPessoa);

                Prestador xPrestador = new Prestador();

                xPrestador = new Prestador();
                xPrestador.FindByPessoa(zPessoa);
                xPrestador.IdPessoa.Find();

                if (xPrestador.IndTipoPrestador == (int)TipoPrestador.Medico)
                {
                    Response.Redirect("ExameClinicoGuia.aspx?IdEmpregado=" + Request["IdEmpregado"] + "&IdExame=" + e.Item.Key.ToString() + "&IdUsuario=" + Request["IdUsuario"] + "&IdEmpresa=" + Request["IdEmpresa"] + "&Acao=" + zAcao);
                }
                else
                {
                    MsgBox1.Show("Ilitera.Net", "Visualização deste exame apenas por prestadores com perfil médico!", null,
                                 new EO.Web.MsgBoxButton("OK"));
                    return;
                }
                                
            }

            
        }




        private void PopulaTelaEmpregado()
        {
            //variável empregado está vazia.  Ver como carregá-lo. - Wagner  
            empregado.Find(" tblEMPREGADO.nID_EMPREGADO = " + Request["IdEmpregado"].ToString());
            EmpregadoFuncao empregadoFuncao = EmpregadoFuncao.GetEmpregadoFuncao(empregado);

            ImagemEmpregado.ImageUrl = empregado.FotoEmpregadoUrl();
            lblValorNome.Text = empregado.tNO_EMPG;

            switch (empregado.nIND_BENEFICIARIO)
            {
                case (int)TipoBeneficiario.BeneficiarioReabilitado:
                    lblValorBene.Text = "BR";
                    break;
                case (int)TipoBeneficiario.PortadorDeficiencia:
                    lblValorBene.Text = "PDH";
                    break;
                case (int)TipoBeneficiario.NaoAplicavel:
                    lblValorBene.Text = "NA";
                    break;
                default:
                    lblValorBene.Text = "NA";
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

            if (empregado.hDT_DEM.ToString("dd-MM-yyyy").Equals("01-01-1753") || empregado.hDT_DEM == new DateTime())
                lblValorDemissao.Text = "&nbsp;";
            else
                lblValorDemissao.Text = empregado.hDT_DEM.ToString("dd-MM-yyyy");

            lblValorRegistro.Text = empregado.VerificaNullCampoString("tCOD_EMPR", "&nbsp;");

            if (empregado.nID_REGIME_REVEZAMENTO.Id == 0)
                lblValorRegRev.Text = "&nbsp;";
            else
                lblValorRegRev.Text = empregado.nID_REGIME_REVEZAMENTO.ToString();

            lblValorTempoEmpresa.Text = empregado.TempoEmpresaEmpregado();
            lblValorFuncao.Text = empregadoFuncao.GetNomeFuncao();
            lblValorSetor.Text = empregadoFuncao.GetNomeSetor();

            if (EmpregadoFuncao.GetJornada(empregado) == "" || EmpregadoFuncao.GetJornada(empregado) == null)
                lblValorJornada.Text = "&nbsp;";
            else
                lblValorJornada.Text = EmpregadoFuncao.GetJornada(empregado);

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

        protected void btnFichaCompleta_Click(object sender, EventArgs e)
        {

        }


        private DataSet DSExamesClinicos()
        {
            DataSet dsExames = new DataSet();
            DataTable table = new DataTable("Result");
            table.Columns.Add("Id", typeof(string));
            table.Columns.Add("Data", typeof(string));
            table.Columns.Add("Tipo", typeof(string));
            table.Columns.Add("Resultado", typeof(string));
            table.Columns.Add("Medico", typeof(string));
            table.Columns.Add("Adendo", typeof(string));
            table.Columns.Add("ASO", typeof(string));
            table.Columns.Add("PCI", typeof(string));
            dsExames.Tables.Add(table);
            DataRow newRow;

            if (prestador.Id == 0)
            {
                Entities.Usuario user = (Entities.Usuario)Session["usuarioLogado"];

                prestador.Find(" IdPessoa = " + user.IdPessoa.ToString());
            }


            ArrayList alExamesClinicos = new Clinico().Find("IdEmpregado=" + Request["IdEmpregado"].ToString() + " and IdExameDicionario between 1 and 5 ORDER BY DataExame DESC");
            DataSet dsResponsavel = new Responsavel().Get("IdPrestador=" + prestador.Id
                + " AND IndResponsavelPapel=" + (int)ResponsavelPapel.ASOPCIBranco);

            foreach (Clinico exameClinico in alExamesClinicos)
            {
                exameClinico.IdExameDicionario.Find();
                exameClinico.IdMedico.Find();
                newRow = dsExames.Tables[0].NewRow();
                newRow["Id"] = exameClinico.Id.ToString();
                newRow["Data"] = exameClinico.DataExame.ToString("dd-MM-yyyy");
                newRow["Tipo"] = exameClinico.IdExameDicionario.Nome.Replace("Mudança de Função", "Mudança de Risco" );
                newRow["Resultado"] = exameClinico.GetResultadoExame();
                newRow["Medico"] = exameClinico.IdMedico.NomeCompleto;

                if (!cliente.ContrataPCMSO.Equals((int)TipoPcmsoContratada.NaoContratada))
                {
                    if (exameClinico.IndResultado == (int)ResultadoExame.Normal || exameClinico.IndResultado == (int)ResultadoExame.Alterado)
                    {
                        newRow["Adendo"] = AdendoScript(exameClinico);
                        newRow["ASO"] = ASOScript(exameClinico);
                    }
                    else if (dsResponsavel.Tables[0].Rows.Count > 0)
                        newRow["ASO"] = ASOScript(exameClinico);

                    newRow["PCI"] = PCIScript(exameClinico);
                }

                dsExames.Tables[0].Rows.Add(newRow);
            }

            if (alExamesClinicos.Count > 0)
                lblTotRegistros.Text = "Total de Registros: <b>" + alExamesClinicos.Count + "</b>";
            else
                lblTotRegistros.Text = "Nenhum registro encontrado";

            return dsExames;
        }



        private DataSet DSExamesClinicosNaoOcupacional()
        {
            DataSet dsExames = new DataSet();
            DataTable table = new DataTable("Result");
            table.Columns.Add("Id", typeof(string));
            table.Columns.Add("Data", typeof(string));
            table.Columns.Add("Tipo", typeof(string));
            table.Columns.Add("Resultado", typeof(string));
            table.Columns.Add("Medico", typeof(string));
            table.Columns.Add("Adendo", typeof(string));
            table.Columns.Add("ASO", typeof(string));
            table.Columns.Add("PCI", typeof(string));
            dsExames.Tables.Add(table);
            DataRow newRow;

            if (prestador.Id == 0)
            {
                Entities.Usuario user = (Entities.Usuario)Session["usuarioLogado"];

                prestador.Find(" IdPessoa = " + user.IdPessoa.ToString());
            }


            ArrayList alExames = new ClinicoNaoOcupacional().Find("IdEmpregado=" + Request["IdEmpregado"].ToString() + " and ( Tipo is not null and Tipo <> '' )  ORDER BY DataExame DESC");
            DataSet dsResponsavel = new Responsavel().Get("IdPrestador=" + prestador.Id
                + " AND IndResponsavelPapel=" + (int)ResponsavelPapel.ASOPCIBranco);

            foreach (ClinicoNaoOcupacional exameNO in alExames)
            {
                exameNO.IdExameDicionario.Find();
                exameNO.IdMedico.Find();
                newRow = dsExames.Tables[0].NewRow();
                newRow["Id"] = exameNO.Id.ToString();
                newRow["Data"] = exameNO.DataExame.ToString("dd-MM-yyyy");

                if (exameNO.Tipo == "E")
                {
                    newRow["Tipo"] = "Enfermagem";
                }
                else if (exameNO.Tipo == "M")
                {
                    newRow["Tipo"] = "Médico";
                }
                else
                {
                    newRow["Tipo"] = "Outros";
                }

                newRow["Resultado"] = "Realizado"; //exameNO.GetResultadoExame();
                newRow["Medico"] = exameNO.IdMedico.NomeCompleto;

                //if (!cliente.ContrataPCMSO.Equals((int)TipoPcmsoContratada.NaoContratada))
                //{
                //    if (exameNO.IndResultado == (int)ResultadoExame.Normal || exameNO.IndResultado == (int)ResultadoExame.Alterado)
                //    {
                //        newRow["Adendo"] = AdendoScript(exameNO);
                //        newRow["ASO"] = ASOScript(exameNO);
                //    }
                //    else if (dsResponsavel.Tables[0].Rows.Count > 0)
                //        newRow["ASO"] = ASOScript(exameNO);

                //    newRow["PCI"] = PCIScript(exameNO);
                //}

                dsExames.Tables[0].Rows.Add(newRow);
            }

            if (alExames.Count > 0)
                lblTotRegistros.Text = "Total de Registros: <b>" + alExames.Count + "</b>";
            else
                lblTotRegistros.Text = "Nenhum registro encontrado";

            return dsExames;
        }




        private DataSet DSExamesComplementares()
        {
            DataSet dsExames = new DataSet();
            DataTable table = new DataTable("Result");
            table.Columns.Add("Id", typeof(string));
            table.Columns.Add("Data", typeof(string));
            table.Columns.Add("Tipo", typeof(string));
            table.Columns.Add("Resultado", typeof(string));
            table.Columns.Add("Medico", typeof(string));
            table.Columns.Add("Adendo", typeof(string));
            table.Columns.Add("ASO", typeof(string));
            table.Columns.Add("PCI", typeof(string));
            dsExames.Tables.Add(table);
            DataRow newRow;

            if (prestador.Id == 0)
            {
                Entities.Usuario user = (Entities.Usuario)Session["usuarioLogado"];

                prestador.Find(" IdPessoa = " + user.IdPessoa.ToString());
            }


            ArrayList alExames = new Complementar().Find("IdEmpregado=" + Request["IdEmpregado"].ToString() + " ORDER BY DataExame DESC");

            //ArrayList alExames = new ClinicoNaoOcupacional().Find("IdEmpregado=" + Request["IdEmpregado"].ToString() + " ORDER BY DataExame DESC");
            DataSet dsResponsavel = new Responsavel().Get("IdPrestador=" + prestador.Id
                + " AND IndResponsavelPapel=" + (int)ResponsavelPapel.ASOPCIBranco);

            foreach (Complementar exameNO in alExames)
            {

                exameNO.IdExameDicionario.Find();
                exameNO.IdMedico.Find();
                newRow = dsExames.Tables[0].NewRow();
                newRow["Id"] = exameNO.Id.ToString();
                newRow["Data"] = exameNO.DataExame.ToString("dd-MM-yyyy");

                newRow["Tipo"] = exameNO.IdExameDicionario.Nome.ToString();

                newRow["Resultado"] = exameNO.GetResultadoExame();
                newRow["Medico"] = exameNO.IdMedico.NomeCompleto;

                //if (!cliente.ContrataPCMSO.Equals((int)TipoPcmsoContratada.NaoContratada))
                //{
                //    if (exameNO.IndResultado == (int)ResultadoExame.Normal || exameNO.IndResultado == (int)ResultadoExame.Alterado)
                //    {
                //        newRow["Adendo"] = AdendoScript(exameNO);
                //        newRow["ASO"] = ASOScript(exameNO);
                //    }
                //    else if (dsResponsavel.Tables[0].Rows.Count > 0)
                //        newRow["ASO"] = ASOScript(exameNO);

                //    newRow["PCI"] = PCIScript(exameNO);
                //}

                dsExames.Tables[0].Rows.Add(newRow);
            }

            if (alExames.Count > 0)
                lblTotRegistros.Text = "Total de Registros: <b>" + alExames.Count + "</b>";
            else
                lblTotRegistros.Text = "Nenhum registro encontrado";

            return dsExames;
        }





        private DataSet DSPCIs()
        {
            DataSet dsExames = new DataSet();
            DataTable table = new DataTable("Result");
            table.Columns.Add("Id", typeof(string));
            table.Columns.Add("Data", typeof(string));
            table.Columns.Add("Tipo", typeof(string));
            table.Columns.Add("Resultado", typeof(string));
            table.Columns.Add("Medico", typeof(string));
            table.Columns.Add("Adendo", typeof(string));
            table.Columns.Add("ASO", typeof(string));
            table.Columns.Add("PCI", typeof(string));
            dsExames.Tables.Add(table);
            DataRow newRow;

            if (prestador.Id == 0)
            {
                Entities.Usuario user = (Entities.Usuario)Session["usuarioLogado"];

                prestador.Find(" IdPessoa = " + user.IdPessoa.ToString());
            }


            ArrayList alExames = new ProntuarioDigital().Find("IdEmpregado=" + Request["IdEmpregado"].ToString() + " and IndTipoDocumento = 0 ORDER BY DataProntuario DESC");

            DataSet dsResponsavel = new Responsavel().Get("IdPrestador=" + prestador.Id
                + " AND IndResponsavelPapel=" + (int)ResponsavelPapel.ASOPCIBranco);


            foreach (ProntuarioDigital exameNO in alExames)
            {

                //exameNO.IdExameDicionario.Find();
                //exameNO.IdMedico.Find();
                newRow = dsExames.Tables[0].NewRow();
                newRow["Id"] = exameNO.Id.ToString();
                newRow["Data"] = exameNO.DataProntuario.ToString("dd-MM-yyyy");

                newRow["Tipo"] = "PCI";//exameNO.IdExameDicionario.Nome.ToString();

                newRow["Resultado"] = " ";  //exameNO.GetResultadoExame();
                newRow["Medico"] = " "; //exameNO.IdMedico.NomeCompleto;

                //if (!cliente.ContrataPCMSO.Equals((int)TipoPcmsoContratada.NaoContratada))
                //{
                //    if (exameNO.IndResultado == (int)ResultadoExame.Normal || exameNO.IndResultado == (int)ResultadoExame.Alterado)
                //    {
                //        newRow["Adendo"] = AdendoScript(exameNO);
                //        newRow["ASO"] = ASOScript(exameNO);
                //    }
                //    else if (dsResponsavel.Tables[0].Rows.Count > 0)
                //        newRow["ASO"] = ASOScript(exameNO);

                //    newRow["PCI"] = PCIScript(exameNO);
                //}

                dsExames.Tables[0].Rows.Add(newRow);
            }

            if (alExames.Count > 0)
                lblTotRegistros.Text = "Total de Registros: <b>" + alExames.Count + "</b>";
            else
                lblTotRegistros.Text = "Nenhum registro encontrado";

            return dsExames;
        }





        private DataSet DSExamesAudiometricos()
        {
            DataSet dsExames = new DataSet();
            DataTable table = new DataTable("Result");
            table.Columns.Add("Id", typeof(string));
            table.Columns.Add("Data", typeof(string));
            table.Columns.Add("Tipo", typeof(string));
            table.Columns.Add("Resultado", typeof(string));
            table.Columns.Add("Medico", typeof(string));
            table.Columns.Add("Adendo", typeof(string));
            table.Columns.Add("ASO", typeof(string));
            table.Columns.Add("PCI", typeof(string));
            dsExames.Tables.Add(table);
            DataRow newRow;

            if (prestador.Id == 0)
            {
                Entities.Usuario user = (Entities.Usuario)Session["usuarioLogado"];

                prestador.Find(" IdPessoa = " + user.IdPessoa.ToString());
            }

            ArrayList alExames = new Audiometria().Find("IdEmpregado=" + Request["IdEmpregado"].ToString() + " ORDER BY DataExame DESC");
            //ArrayList alExames = new Complementar().Find("IdEmpregado=" + Request["IdEmpregado"].ToString() + " ORDER BY DataExame DESC");

            //ArrayList alExames = new ClinicoNaoOcupacional().Find("IdEmpregado=" + Request["IdEmpregado"].ToString() + " ORDER BY DataExame DESC");
            DataSet dsResponsavel = new Responsavel().Get("IdPrestador=" + prestador.Id
                + " AND IndResponsavelPapel=" + (int)ResponsavelPapel.ASOPCIBranco);

            foreach (Audiometria exameNO in alExames)
            {

                exameNO.IdExameDicionario.Find();
                exameNO.IdMedico.Find();
                newRow = dsExames.Tables[0].NewRow();
                newRow["Id"] = exameNO.Id.ToString();
                newRow["Data"] = exameNO.DataExame.ToString("dd-MM-yyyy");

                newRow["Tipo"] = exameNO.IdExameDicionario.Nome.ToString();

                newRow["Resultado"] = exameNO.GetResultadoExame();
                newRow["Medico"] = exameNO.IdMedico.NomeCompleto;

                //if (!cliente.ContrataPCMSO.Equals((int)TipoPcmsoContratada.NaoContratada))
                //{
                //    if (exameNO.IndResultado == (int)ResultadoExame.Normal || exameNO.IndResultado == (int)ResultadoExame.Alterado)
                //    {
                //        newRow["Adendo"] = AdendoScript(exameNO);
                //        newRow["ASO"] = ASOScript(exameNO);
                //    }
                //    else if (dsResponsavel.Tables[0].Rows.Count > 0)
                //        newRow["ASO"] = ASOScript(exameNO);

                //    newRow["PCI"] = PCIScript(exameNO);
                //}

                dsExames.Tables[0].Rows.Add(newRow);
            }

            if (alExames.Count > 0)
                lblTotRegistros.Text = "Total de Registros: <b>" + alExames.Count + "</b>";
            else
                lblTotRegistros.Text = "Nenhum registro encontrado";

            return dsExames;
        }



        private string PCIScript(Clinico exameClinico)
        {
            StringBuilder st = new StringBuilder();
            Guid strAux = Guid.NewGuid();

            st.Append(@"<a href=""#""><img src=""img/print.gif"" border=0 alt=""Imprimir PCI"" onClick=""javascript:");

            if (usuario.NomeUsuario.Equals("lmm"))
                st.Append(strOpenReport("PCMSO", "AnotacoesPCI.aspx?IliteraSystem=" + strAux.ToString() + strAux.ToString()
                    + "&IdEmpresa=" + Request["IdEmpresa"] + "&IdExame=" + exameClinico.Id + "&IdEmpregado=" + Request["IdEmpregado"] + "&IdUsuario=" + Request["IdUsuario"], "AnotacoesPCI") + @"""");
            else
                switch (prestador.IndTipoPrestador)
                {
                    case (int)TipoPrestador.Medico:
                        st.Append(strOpenReport("PCMSO", "AnotacoesPCI.aspx?IliteraSystem=" + strAux.ToString() + strAux.ToString()
                            + "&IdEmpresa=" + Request["IdEmpresa"] + "&IdExame=" + exameClinico.Id + "&IdEmpregado=" + Request["IdEmpregado"] + "&IdUsuario=" + Request["IdUsuario"], "AnotacoesPCI") + @"""");
                        break;
                    case (int)TipoPrestador.ContatoEmpresa:
                        DataSet ds = new Responsavel().Get("IdPrestador=" + prestador.Id
                            + " AND IndResponsavelPapel=" + (int)ResponsavelPapel.ASOPCIBranco);

                        if (ds.Tables[0].Rows.Count > 0 && exameClinico.IndResultado == (int)ResultadoExame.NaoRealizado)
                            st.Append(strOpenReport("PCMSO", "AnotacoesPCI.aspx?IliteraSystem=" + strAux.ToString() + strAux.ToString()
                                + "&IdEmpresa=" + Request["IdEmpresa"] + "&IdExame=" + exameClinico.Id + "&IdEmpregado=" + Request["IdEmpregado"] + "&IdUsuario=" + Request["IdUsuario"], "AnotacoesPCI") + @"""");
                        else
                            st.Append("window.alert('O usuário " + usuario.NomeUsuario + @" não é do tipo médico e não possui autorização para visualizar o PCI deste exame!');""");
                        break;
                    default:
                        st.Append("window.alert('O usuário " + usuario.NomeUsuario + @" não é do tipo médico e não possui autorização para visualizar o PCI deste exame!');""");
                        break;
                }

            st.Append("></a>");

            return st.ToString();
        }

        private string ASOScript(Clinico exameClinico)
        {
            StringBuilder st = new StringBuilder();
            Guid strAux = Guid.NewGuid();

            st.Append(@"<a href=""#""><img src=""img/print.gif"" border=0 alt=""Imprimir ASO"" onClick=""javascript:");

            if (usuario.NomeUsuario.Equals("lmm"))
                st.Append(strOpenReport("PCMSO", "ASOEmpregado.aspx?IliteraSystem=" + strAux.ToString() + strAux.ToString()
                    + "&IdEmpresa=" + Request["IdEmpresa"] + "&IdExame=" + exameClinico.Id + "&IdEmpregado=" + Request["IdEmpregado"] + "&IdUsuario=" + Request["IdUsuario"], "ASOEmpregado") + @"""");
            else
                switch (prestador.IndTipoPrestador)
                {
                    case (int)TipoPrestador.Medico:
                        st.Append(strOpenReport("PCMSO", "ASOEmpregado.aspx?IliteraSystem=" + strAux.ToString() + strAux.ToString()
                            + "&IdEmpresa=" + Request["IdEmpresa"] + "&IdExame=" + exameClinico.Id + "&IdEmpregado=" + Request["IdEmpregado"] + "&IdUsuario=" + Request["IdUsuario"], "ASOEmpregado") + @"""");
                        break;
                    case (int)TipoPrestador.ContatoEmpresa:
                        DataSet ds = new Responsavel().Get("IdPrestador=" + prestador.Id
                            + " AND IndResponsavelPapel=" + (int)ResponsavelPapel.ASOPCIBranco);

                        if (ds.Tables[0].Rows.Count > 0 && exameClinico.IndResultado == (int)ResultadoExame.NaoRealizado)
                            st.Append(strOpenReport("PCMSO", "ASOEmpregado.aspx?IliteraSystem=" + strAux.ToString() + strAux.ToString()
                                + "&IdEmpresa=" + Request["IdEmpresa"] + "&IdExame=" + exameClinico.Id + "&IdEmpregado=" + Request["IdEmpregado"] + "&IdUsuario=" + Request["IdUsuario"], "ASOEmpregado") + @"""");
                        else
                            st.Append("window.alert('O usuário " + usuario.NomeUsuario + @" não é do tipo médico e não possui autorização para visualizar o ASO deste exame!');""");
                        break;
                    default:
                        st.Append("window.alert('O usuário " + usuario.NomeUsuario + @" não é do tipo médico e não possui autorização para visualizar o ASO deste exame!');""");
                        break;
                }

            st.Append("></a>");

            return st.ToString();
        }

        private string AdendoScript(Clinico exameClinico)
        {
            StringBuilder st = new StringBuilder();
            st.Append(@"<a href=""#""><img src=""img/adendo.gif"" border=0 alt=""Adendo ao Exame Clínico"" onClick=""javascript:");

            if (usuario.NomeUsuario.Equals("lmm"))
                st.Append("void(addItem(centerWin('AdendoExame.aspx?IdEmpregado=" + Request["IdEmpregado"] + "&IdUsuario=" + Request["IdUsuario"]
                    + "&IdEmpresa=" + Request["IdEmpresa"] + "&IdExame=" + exameClinico.Id + @"','600','500','AE'),'Todos'));""");
            else
                switch (prestador.IndTipoPrestador)
                {
                    case (int)TipoPrestador.Medico:
                        st.Append("void(addItem(centerWin('AdendoExame.aspx?IdEmpregado=" + empregado.Id + "&IdUsuario=" + usuario.Id
                            + "&IdEmpresa=" + cliente.Id + "&IdExame=" + exameClinico.Id + @"','600','500','AE'),'Todos'));""");
                        break;
                    case (int)TipoPrestador.ContatoEmpresa:
                        st.Append("window.alert('O usuário " + usuario.NomeUsuario + @" não é do tipo médico e não possui autorização para visualizar e inserir Adendos a este exame!');""");
                        break;
                    default:
                        st.Append("window.alert('O usuário " + usuario.NomeUsuario + @" não é do tipo médico e não possui autorização para visualizar e inserir Adendos a este exame!');""");
                        break;
                }

            st.Append("></a>");

            return st.ToString();
        }

        //protected void grd_Clinicos_PageIndexChanged(object sender, Infragistics.WebUI.UltraWebGrid.PageEventArgs e)
        //{
        //    if (Request["Tipo"].ToString().Trim() == "2")
        //    {
        //        grd_Clinicos.DataSource = DSExamesClinicosNaoOcupacional();
        //    }
        //    else
        //    {
        //        grd_Clinicos.DataSource = DSExamesClinicos();
        //    }
        //    grd_Clinicos.DisplayLayout.Pager.CurrentPageIndex = e.NewPageIndex;
        //    grd_Clinicos.DataBind();
        //}

        //protected void grd_Clinicos_InitializeRow(object sender, Infragistics.WebUI.UltraWebGrid.RowEventArgs e)
        //{
        //    int width = 680, height = 510;

        //    try
        //    {
        //        PermissaoEdicaoExame(new Clinico(Convert.ToInt32(e.Row.Cells[0].Text)));

        //        if (Request["Tipo"].ToString().Trim() == "2")
        //        {
        //            e.Row.Cells.FromKey("Tipo").TargetURL = "javascript:void(addItem(centerWin('ExameNaoOcupacional.aspx?IdEmpregado=" + Request["IdEmpregado"] + "&IdExame=" + e.Row.Cells[0].Text
        //                + "&IdUsuario=" + Request["IdUsuario"] + "&IdEmpresa=" + Request["IdEmpresa"] + "'," + width + "," + height + ",\'EC\'),\'Todos\'));";
        //        }
        //        else if (Request["Tipo"].ToString().Trim() == "1")
        //        {
        //            e.Row.Cells.FromKey("Tipo").TargetURL = "javascript:void(addItem(centerWin('ExameClinico.aspx?IdEmpregado=" + Request["IdEmpregado"] + "&IdExame=" + e.Row.Cells[0].Text
        //                + "&IdUsuario=" + Request["IdUsuario"] + "&IdEmpresa=" + Request["IdEmpresa"] + "'," + width + "," + height + ",\'EC\'),\'Todos\'));";
        //        }
        //        else if (Request["Tipo"].ToString().Trim() == "3")
        //        {
        //            e.Row.Cells.FromKey("Tipo").TargetURL = "javascript:void(addItem(centerWin('ExameComplementar.aspx?IdEmpregado=" + Request["IdEmpregado"] + "&IdExame=" + e.Row.Cells[0].Text
        //                + "&IdUsuario=" + Request["IdUsuario"] + "&IdEmpresa=" + Request["IdEmpresa"] + "'," + width + "," + height + ",\'EC\'),\'Todos\'));";
        //        }
        //        else if (Request["Tipo"].ToString().Trim() == "4")
        //        {
        //            e.Row.Cells.FromKey("Tipo").TargetURL = "javascript:void(addItem(centerWin('ExameAudiometrico.aspx?IdEmpregado=" + Request["IdEmpregado"] + "&IdExame=" + e.Row.Cells[0].Text
        //                + "&IdUsuario=" + Request["IdUsuario"] + "&IdEmpresa=" + Request["IdEmpresa"] + "'," + width + "," + height + ",\'EC\'),\'Todos\'));";
        //        }
        //        else if (Request["Tipo"].ToString().Trim() == "6")
        //        {
        //            e.Row.Cells.FromKey("Tipo").TargetURL = "javascript:void(addItem(centerWin('ExameDigitalizado.aspx?IdEmpregado=" + Request["IdEmpregado"] + "&IdExame=" + e.Row.Cells[0].Text
        //                + "&IdUsuario=" + Request["IdUsuario"] + "&IdEmpresa=" + Request["IdEmpresa"] + "'," + width + "," + height + ",\'EC\'),\'Todos\'));";
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        e.Row.Cells.FromKey("Tipo").TargetURL = "javascript:window.alert(\"" + ex.Message + "\");";
        //    }

        //}


        protected static string strOpenReport(string directory, string fileAndQuery, string ReportName)
        {
            return strOpenReport(directory, fileAndQuery, ReportName, false);
        }

        protected static string strOpenReport(string directory, string fileAndQuery, string ReportName, bool useDirectoryForLocalProcess)
        {
            StringBuilder st = new StringBuilder();

            Guid strAux = Guid.NewGuid();

            string valueProcess = "Local";

            if (valueProcess.Equals("Remote"))
                st.Append("void(window.open('http://report.ilitera.net/index.aspx?Identity=" + strAux.ToString() + "opsa" + strAux.ToString()
                    + "&Key=" + strAux.ToString() + "MestraNet" + strAux.ToString()
                    + "&PathAndQuery=" + HttpUtility.UrlEncode(directory + "/" + fileAndQuery) + "', '" + ReportName + "', 'scrollbars=yes, resizable=yes, toolbar=no, addressbar=no, menubar=no, width=800px, height=600px').focus());");
            else if (valueProcess.Equals("Local"))
            {
                if (useDirectoryForLocalProcess)
                {
                    st.AppendFormat("void(window.open('../{0}/{1}', '{2}', 'scrollbars=yes, resizable=yes, toolbar=no, addressbar=no, menubar=no, width=800px, height=600px').focus());", directory, fileAndQuery, ReportName);
                }
                else
                {
                    st.AppendFormat("void(window.open('{0}','{1}', 'scrollbars=yes, resizable=yes, toolbar=no, addressbar=no, menubar=no, width=800px, height=600px').focus());", fileAndQuery, ReportName);
                }
            }

            return st.ToString();
        }


        protected void PermissaoEdicaoExame(ExameBase exame)
        {
            string AvisoPrazo = string.Empty, AvisoPermissao = string.Empty, AvisoAutorizacao = string.Empty;
            bool VerificaMedico = false;

            if (exame.IdExameDicionario.mirrorOld == null)
                exame.IdExameDicionario.Find();

            switch (exame.IdExameDicionario.IndExame)
            {
                case (int)IndTipoExame.Clinico:
                    AvisoPrazo = "O prazo de 2 horas para a edição do exame clínico se encerrou! Não é mais possível editar o exame!";
                    AvisoAutorizacao = String.Format("O usuário {0} não possui autorização para visualizar detalhes deste exame clínico!", usuario.NomeUsuario);
                    AvisoPermissao = String.Format("O usuário {0} não é do tipo médico e não possui permissão para visualizar detalhes deste exame clínico!", usuario.NomeUsuario);
                    VerificaMedico = true;
                    break;

                case (int)IndTipoExame.Audiometrico:
                    AvisoPrazo = "O prazo de 2 horas para a edição do exame audiométrico se encerrou! Não é mais possível editar o exame!";
                    AvisoPermissao = String.Format("O usuário {0} não é do tipo médico e não possui permissão para visualizar detalhes deste exame audiométrico!", usuario.NomeUsuario);
                    VerificaMedico = false;
                    break;

                case (int)IndTipoExame.Complementar:
                    AvisoPrazo = "O prazo de 2 horas para a edição do exame complementar se encerrou! Não é mais possível editar o exame!";
                    AvisoPermissao = String.Format("O usuário {0} não é do tipo médico e não possui permissão para visualizar detalhes deste exame complementar!", usuario.NomeUsuario);
                    VerificaMedico = false;
                    break;

                case (int)IndTipoExame.NaoOcupacional:
                    AvisoPrazo = "O prazo de 2 horas para a edição do exame ambulatorial se encerrou! Não é mais possível editar o exame!";
                    AvisoAutorizacao = String.Format("O usuário {0} não possui autorização para visualizar detalhes deste exame ambulatorial!", usuario.NomeUsuario);
                    AvisoPermissao = String.Format("O usuário {0} não é do tipo médico e não possui permissão para visualizar detalhes deste exame ambulatorial!", usuario.NomeUsuario);
                    VerificaMedico = true;
                    break;
            }

            if (!usuario.NomeUsuario.Equals("lmm"))
            {
                switch (prestador.IndTipoPrestador)
                {
                    case (int)TipoPrestador.Medico:
                        if (!cliente.ContrataPCMSO.Equals((int)TipoPcmsoContratada.NaoContratada))
                        {
                            if (!exame.IdMedico.Id.Equals(0) &&
                                (exame.IdExameDicionario.IndExame.Equals((int)IndTipoExame.NaoOcupacional) ||
                                (!exame.IdExameDicionario.IndExame.Equals((int)IndTipoExame.NaoOcupacional) && !exame.IndResultado.Equals((int)ResultadoExame.NaoRealizado) &&
                                !exame.IndResultado.Equals((int)ResultadoExame.EmEspera))))
                                if (VerificaMedico)
                                {
                                    if (exame.IdMedico.Id.Equals(prestador.Id))
                                    {
                                        if (DateTime.Now > exame.DataCriacao.AddHours(2))
                                            throw new Exception(AvisoPrazo);
                                    }
                                    else
                                        throw new Exception(AvisoAutorizacao);
                                }
                                else
                                    if (DateTime.Now > exame.DataCriacao.AddHours(2))
                                        throw new Exception(AvisoPrazo);
                        }
                        break;
                    case (int)TipoPrestador.ContatoEmpresa:
                        if (!cliente.ContrataPCMSO.Equals((int)TipoPcmsoContratada.NaoContratada))
                            throw new Exception(AvisoPermissao);
                        break;
                    default:
                        throw new Exception(AvisoPermissao);
                }
            }
        }



    }
}
