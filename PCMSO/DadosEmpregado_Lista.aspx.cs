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
using System.IO;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Ilitera.Common;
using Ilitera.PCMSO.Report;
using Ilitera.Opsa.Report;
using Entities;
using BLL;
using System.Collections;
using EO.Web;

namespace Ilitera.Net
{
    public partial class DadosEmpregado_Lista : System.Web.UI.Page
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

            string zTipo = Request["Tipo"].ToString().Trim();

            PopulaTelaEmpregado();



            Entities.Usuario zUser = (Entities.Usuario)Session["usuarioLogado"];

            Ilitera.Common.Pessoa zPessoa = new Ilitera.Common.Pessoa();
            zPessoa.Find(zUser.IdPessoa);

            Prestador xPrestador = new Prestador();

            xPrestador = new Prestador();
            xPrestador.FindByPessoa(zPessoa);
            xPrestador.IdPessoa.Find();






            if (xPrestador.IndTipoPrestador == (int)TipoPrestador.Medico || zPessoa.NomeAbreviado.ToUpper().IndexOf("ILITERA") >= 0 || zPessoa.NomeAbreviado.ToUpper().IndexOf("PRAJNA") >= 0)
            {
                Ilitera.Data.Clientes_Funcionarios xLista = new Ilitera.Data.Clientes_Funcionarios();

                DataSet zDs = new DataSet();
                zDs = xLista.Gerar_Lista_Exames(Convert.ToInt32(Request.QueryString["IdEmpregado"]));

                grd_Clinicos.DataSource = zDs;

                grd_Clinicos.DataBind();

            }
            else
            {
                MsgBox1.Show("Ilitera.Net", "Usuário sem acesso à dados médicos.", null,
                             new EO.Web.MsgBoxButton("OK"));
                return;

            }


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


                lblExCli.Text = "Exames / Absenteísmo";

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


            //Session["Empregado"] = e.Item.Key.ToString();
            //Session["NomeEmpregado"] = e.Item.Cells[0].Value.ToString();

            string zAcao = "V";

            //if (e.CommandName.ToString().Trim() == "5")
            //    zAcao = "E";  //editar
            //else
                zAcao = "V";  //visualizar


            Session["Retorno"] = "2";
            string zTipo = e.Item.Cells[3].Value.ToString();
                        
            if (zTipo == "Absenteísmo")
            {               
               //Response.Redirect("CadAbsentismo.aspx?IdEmpregado=" + Request["IdEmpregado"] + "&IdExame=" + e.Item.Key.ToString() + "&IdUsuario=" + Request["IdUsuario"] + "&IdEmpresa=" + Request["IdEmpresa"]);
                Response.Redirect("ListaAcidentes2.aspx?Tipo=2&Tela=1");
            }
            else if (zTipo == "Periódico" || zTipo == "Mudança de Função" || zTipo == "Admissional" || zTipo == "Demissional")
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
                    Response.Redirect("ExameClinico.aspx?IdEmpregado=" + Request["IdEmpregado"] + "&IdExame=" + e.Item.Key.ToString() + "&IdUsuario=" + Request["IdUsuario"] + "&IdEmpresa=" + Request["IdEmpresa"] + "&Acao=" + zAcao);
                }
                else
                {
                    MsgBox1.Show("Ilitera.Net", "Visualização deste exame apenas por prestadores com perfil médico!", null,
                                 new EO.Web.MsgBoxButton("OK"));
                    return;
                }

            }
            else if (zTipo == "Audiometria")
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
            else if (zTipo == "Ambulatorial")
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
                    Response.Redirect("ExameNaoOcupacional.aspx?IdEmpregado=" + Request["IdEmpregado"] + "&IdExame=" + e.Item.Key.ToString() + "&IdUsuario=" + Request["IdUsuario"] + "&IdEmpresa=" + Request["IdEmpresa"] + "&Acao=" + zAcao);
                }
                else
                {
                    MsgBox1.Show("Ilitera.Net", "Visualização deste exame apenas por prestadores com perfil médico!", null,
                                 new EO.Web.MsgBoxButton("OK"));
                    return;
                }                
            }
            else 
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

        protected void cmd_Gerar_PDF_Click(object sender, EventArgs e)
        {

            Guid strAux = Guid.NewGuid();

            string xLinhas = "";
            string xnLinhas = "";

            //xLinhas = lbl_Items.Text.Trim();

            xLinhas = lblreg.Text;   //registros marcados
            xnLinhas = lblnreg.Text;  //registros que foram desmarcados 


            string zAux = "";

            for (int zCont = 0; zCont < xnLinhas.Length; zCont++)
            {

                string xChar=xnLinhas.Substring(zCont, 1);
                

                if (xChar == "|")
                {
                    if (zAux != "")
                    {

                        if (xLinhas.IndexOf(zAux) >= 0)
                        {

                            xLinhas = xLinhas.Remove(xLinhas.IndexOf(zAux), zAux.Length + 1);

                        }

                    }
                    zAux = "";
                }
                else if (char.IsNumber(xChar, 0) || xChar=="-" )
                {
                    zAux = zAux + xChar;
                }

            }


            if (xLinhas.IndexOf(zAux) >= 0)
            {

                xLinhas = xLinhas.Remove(xLinhas.IndexOf(zAux), zAux.Length + 1);

            }


            if (xLinhas.Trim() != "")
            {

                OpenReport("", "PCICompleto.aspx?IliteraSystem=" + strAux.ToString() + strAux.ToString()
                    + "&IdEmpresa=" + Request["IdEmpresa"] + "&IdUsuario=" + Request["IdUsuario"] + "&IdEmpregado=" + Request["IdEmpregado"].ToString() + "&xId=" + xLinhas.ToString(), "PCICompleto");
               
            }

            lblreg.Text = "";
            lblnreg.Text = "";

        }


        protected static void CreatePDFMerged(CrystalDecisions.CrystalReports.Engine.ReportClass[] reports, HttpResponse response, string watermark, bool RenumerarPaginas)
        {
            Stream[] streams = new Stream[reports.Length];

            int i = 0;

            foreach (CrystalDecisions.CrystalReports.Engine.ReportClass report in reports)
            {
                if (RenumerarPaginas)
                    report.ReportDefinition.ReportObjects["PaginaNdeN1"].ObjectFormat.EnableSuppress = true;

                streams[i] = report.ExportToStream(ExportFormatType.PortableDocFormat);

                report.Close();

                i++;
            }

            CreatePDFMerged(streams, response, watermark, RenumerarPaginas);
        }

        protected static void CreatePDFMerged(Stream[] streams, HttpResponse response, string watermark, bool RenumerarPaginas)
        {
            MemoryStream reportStream = PdfSharpUtility.MergeReports(streams, string.Empty, watermark, RenumerarPaginas);

            ShowPdfDocument(response, reportStream);
        }

        protected static void ShowPdfDocument(HttpResponse response, MemoryStream reportStream)
        {
            response.Clear();
            response.ContentType = "application/pdf";
            response.AddHeader("content-length", reportStream.Length.ToString());
            response.BinaryWrite(reportStream.ToArray()); ;
            response.Flush();
            reportStream.Close();
            response.End();
        }


        protected void OpenReport(string directory, string fileAndQuery, string ReportName)
        {
            this.OpenReport(directory, fileAndQuery, ReportName, true);
        }

        protected void OpenReport(string directory, string fileAndQuery, string ReportName, bool useDirectoryForLocalProcess)
        {
            StringBuilder st = new StringBuilder();

            Guid strAux = Guid.NewGuid();

            string valueProcess = "Local";

            if (valueProcess.Equals("Remote"))
                //st.Append("void(window.open('http://report.ilitera.net/index.aspx?Identity=" + strAux.ToString() + "opsa" + strAux.ToString()
                //    + "&Key=" + strAux.ToString() + "MestraNet" + strAux.ToString()
                //    + "&PathAndQuery=" + HttpUtility.UrlEncode(directory + "/" + fileAndQuery) + "', '" + ReportName + "', 'scrollbars=yes, resizable=yes, toolbar=no, addressbar=no, menubar=no, width=800px, height=600px'));");
                System.Diagnostics.Debug.WriteLine("");
            else if (valueProcess.Equals("Local"))
            {
                if (useDirectoryForLocalProcess)
                {
                    //st.Append("void(window.open('../" + directory + "/" + fileAndQuery + "', '" + ReportName + "', 'scrollbars=yes, resizable=yes, toolbar=no, addressbar=no, menubar=no, width=800px, height=600px'));");
                    //st.AppendFormat("void(window.open('../{0}/{1}', '{2}','scrollbars=yes, resizable=yes, toolbar=no, addressbar=no, menubar=no, width=800px, height=600px'));", directory, fileAndQuery, ReportName);
                    st.AppendFormat("void(window.open('{1}', '{2}','scrollbars=yes, resizable=yes, toolbar=no, addressbar=no, menubar=no, width=800px, height=600px'));", directory, fileAndQuery, ReportName);
                }
                else
                {
                    //st.Append("void(window.open('" + fileAndQuery + "', '" + ReportName + "', 'scrollbars=yes, resizable=yes, toolbar=no, addressbar=no, menubar=no, width=800px, height=600px'));");
                    st.AppendFormat("void(window.open('{0}','{1}', 'scrollbars=yes, resizable=yes, toolbar=no, addressbar=no, menubar=no, width=800px, height=600px'));", fileAndQuery, ReportName);
                }
            }

            System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), String.Format("OpenReport{0}", ReportName), st.ToString(), true);
        }


        protected void grd_Clinicos_ItemChanged(object sender, EO.Web.GridItemEventArgs e)
        {
            //Check whether it is from our client side
            //JavaScript call
            //int zCont = 0;

            //if ( e.Item.Cells[0].Value.ToString().Trim()=="true") zCont = 1;
            
        }



    }
}
