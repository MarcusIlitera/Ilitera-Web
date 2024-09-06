using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Ilitera.Opsa.Data;
using System.Configuration;
using System.Linq;
using System.Web;

using Ilitera.Common;
using System.Net.Mail;


namespace Ilitera.Net
{
    public partial class DadosEmpregado_Vacina : System.Web.UI.Page
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

            DataSet zDs = new DataSet();

            Ilitera.Data.Clientes_Clinicas zVacinas = new Ilitera.Data.Clientes_Clinicas();
            zDs = zVacinas.Retornar_Vacinas( Request["IdEmpregado"].ToString());

            grd_Clinicos.DataSource = zDs;
            

            if ( zDs.Tables[0].Rows.Count > 0)
                lblTotRegistros.Text = "Total de Registros: <b>" + zDs.Tables[0].Rows.Count + "</b>";
            else
                lblTotRegistros.Text = "Nenhum registro encontrado";



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


                //int width = 680, height = 550;


                Cliente cliente;
                cliente = new Cliente(System.Convert.ToInt32(Session["Empresa"].ToString()));

                if (cliente.Alerta_Vacina_Dias != null)
                    txt_Alerta_Dias.Text = cliente.Alerta_Vacina_Dias.ToString().Trim();
                else
                    txt_Alerta_Dias.Text = "";

                if (cliente.Alerta_Vacina_Email != null)
                    txt_Email.Text = cliente.Alerta_Vacina_Email.Trim();
                else
                    txt_Email.Text = "";





                lblExCli.Text = "Vacinação Colaborador";

                hlkNovo.NavigateUrl = "javascript:void(window.location.href ='Vacinas.aspx?IdEmpregado=" + Request["IdEmpregado"] + "&IdUsuario=" + Request["IdUsuario"] + "&IdEmpresa=" + Request["IdEmpresa"] + "&Acao=E')";
              
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


           // this.ClientScript.RegisterStartupScript(this.GetType(), "IdEmpregado", st.ToString(), true);
            //btnFichaCompleta.Attributes.Add("onClick", "addItem(centerWin('../DadosEmpresa/FichaCompleta.aspx?IdEmpregado=" + Request["IdEmpregado"] + "&IdEmpresa=" + Request["IdEmpresa"] + "&IdUsuario=" + Request["IdUsuario"] + "',560,320,\'FichaCompleta\'),\'Todos\'); Reload();");

        }


        protected void grd_Clinicos_ItemCommand(object sender, EO.Web.GridCommandEventArgs e)
        {
            //Check whether it is from our client side
            //JavaScript call
            string zAcao = "V";


            if (e.CommandName.ToString().Trim() == "3") 
                zAcao = "E";  //editar
            else
                zAcao = "V";  //visualizar


            if (e.Item.Key.ToString().Trim() == "0") return;


            //Session["Empregado"] = e.Item.Key.ToString();
            //Session["NomeEmpregado"] = e.Item.Cells[0].Value.ToString();
            string zTipo = Request["Tipo"].ToString().Trim();
                        

            Response.Redirect("Vacinas.aspx?IdEmpregado=" + Request["IdEmpregado"] + "&IdVacina=" + e.Item.Key.ToString() + "&IdUsuario=" + Request["IdUsuario"] + "&IdEmpresa=" + Request["IdEmpresa"] + "&Acao=" + zAcao);


            
        }




        private void PopulaTelaEmpregado()
        {
            //variável empregado está vazia.  Ver como carregá-lo. - Wagner  
            empregado.Find(" tblEMPREGADO.nID_EMPREGADO = " + Request["IdEmpregado"].ToString());

            Cliente cliente;
            cliente = new Cliente(System.Convert.ToInt32(Session["Empresa"].ToString()));

            EmpregadoFuncao empregadoFuncao = EmpregadoFuncao.GetEmpregadoFuncao(cliente,empregado);

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

        protected void cmd_Salvar_Alerta_Click(object sender, EventArgs e)
        {

            string xErro = "";


            if ( txt_Email.Text.Trim()=="")
            {
                xErro = "Necessário fornecer e-mail para alerta.";
            }
            else
            {
                string[] stringSeparators4 = new string[] { ";" };
                string[] result4;

                result4 = txt_Email.Text.Trim().Split(stringSeparators4, StringSplitOptions.RemoveEmptyEntries);

                foreach (string s in result4)
                {
                    if (s.Trim() != "")
                    {
                        if (IsValidMail(s) == false)
                        {
                            xErro = "e-mail de alerta de eSocial inválido! " +  s;                            
                        }
                    }
                }

            }
            

            if ( txt_Alerta_Dias.Text.Trim()=="")
            {
                xErro = "Necessário fornecer dias de antecedência para alerta.";
            }
            else
            {
                Int16 zVal1;

                if (!Int16.TryParse(txt_Alerta_Dias.Text, out zVal1))
                {
                    xErro = "Dia fornecido inválido.";                    
                }
            }


            if ( xErro=="")
            {
                try
                {
                    Cliente cliente;
                    cliente = new Cliente(System.Convert.ToInt32(Session["Empresa"].ToString()));

                    cliente.Alerta_Vacina_Dias = System.Convert.ToInt16(txt_Alerta_Dias.Text);
                    cliente.Alerta_Vacina_Email = txt_Email.Text;

                    cliente.Save();
                 
                    MsgBox1.Show("Ilitera.Net", "Configurações Salvas!", null,
                    new EO.Web.MsgBoxButton("OK"));

                }
                catch ( Exception ex)
                {
                    MsgBox1.Show("Ilitera.Net", "Erro ao salvar configuração:" + ex.Message, null,
                    new EO.Web.MsgBoxButton("OK"));
                }
            }
            else
            {
                MsgBox1.Show("Ilitera.Net", xErro, null,
                new EO.Web.MsgBoxButton("OK"));
            }

            return;



        }




        private static bool IsValidMail(string email)
        {
            var valid = true;

            try
            {
                var emailAddress = new MailAddress(email);
            }
            catch
            {
                valid = false;
            }

            return valid;
        }


    }
}
