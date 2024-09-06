using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;

using System.Net.Mail;
using System.Text;
using System.Net;

using Entities;
using Facade;
using Ilitera.Opsa.Data;

namespace Ilitera.Net
{
    public partial class InserirDadosEmpregado : System.Web.UI.Page
    {
        #region eventos

        private Int32 xIdEmpresa;
        private Int32 xIdUsuario;

        protected void Page_Load(object sender, EventArgs e)
        {


            if (!IsPostBack)
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
                
                Usuario user = (Usuario)Session["usuarioLogado"];

                xIdEmpresa = System.Convert.ToInt32(Session["Empresa"]);
                xIdUsuario = user.IdUsuario;

                Cliente xCliente = new Cliente(xIdEmpresa);

                if ( xCliente.Permitir_Colaboradores == true )
                {
                    cmd_Atualizar.Attributes.Add("onClick", "javascript:return confirm('Você deseja realmente salvar dados / classif.funcional ?');");
                    cmd_Atualizar.Enabled = true;
                }
                else
                {
                    cmd_Atualizar.Enabled = false;
                    MsgBox1.Show("Ilitera.Net", "Empresa não aceita inserção de colaboradores", null,
                    new EO.Web.MsgBoxButton("OK"));
                    return;
                }


                lbl_Id_Empresa.Text = xIdEmpresa.ToString().Trim();
                lbl_Id_Usuario.Text = xIdUsuario.ToString().Trim();


                if (xCliente.Bloquear_Novo_Setor == true)
                {
                    txtSetor.Enabled = false;
                    txtSetor2.Enabled = false;
                    txtSetor3.Enabled = false;
                    txtSetor4.Enabled = false;
                    txtSetor5.Enabled = false;
                }
                else
                {
                    txtSetor.Enabled = true;
                    txtSetor2.Enabled = true;
                    txtSetor3.Enabled = true;
                    txtSetor4.Enabled = true;
                    txtSetor5.Enabled = true;
                }

                if (xCliente.Bloquear_Novo_Cargo == true)
                {                    
                    txtCargo.Enabled = false;
                    txtFuncao.Enabled = false;                 
                    txtCargo2.Enabled = false;
                    txtFuncao2.Enabled = false;                    
                    txtCargo3.Enabled = false;
                    txtFuncao3.Enabled = false;                    
                    txtCargo4.Enabled = false;
                    txtFuncao4.Enabled = false;                    
                    txtCargo5.Enabled = false;
                    txtFuncao5.Enabled = false;
                }
                else
                {                    
                    txtCargo.Enabled = true;
                    txtFuncao.Enabled = true;                 
                    txtCargo2.Enabled = true;
                    txtFuncao2.Enabled = true;                    
                    txtCargo3.Enabled = true;
                    txtFuncao3.Enabled = true;                    
                    txtCargo4.Enabled = true;
                    txtFuncao4.Enabled = true;                    
                    txtCargo5.Enabled = true;
                    txtFuncao5.Enabled = true;
                }


                PopulaGrid();


                //readmissão - puxar dados do empregado
                if (Request["IdEmpregado"] != null && Request["IdEmpregado"].ToString().Trim() != "0")
                {
                    Entities.Empregado empregado = new Entities.Empregado();
                    empregado = EmpregadoFacade.RetornarEmpregadoDadosCadastrais(Convert.ToInt32((Request["IdEmpregado"].ToString().Trim())));

                    txtNomeEmpregado.Text = empregado.NomeEmpregado;
                    if (txtNomeEmpregado.Text.Trim() != "") txtNomeEmpregado.ReadOnly = true;

                    txtApelidoEmpregado.Text = empregado.ApelidoEmpregado;

                    txtCPF.Text = empregado.Cpf;
                    if (txtCPF.Text.Trim() != "") txtCPF.ReadOnly = true;

                    txtCTPS_Num.Text = empregado.numCTPS;
                    if (txtCTPS_Num.Text.Trim() != "") txtCTPS_Num.ReadOnly = true;

                    txtCTPS_Serie.Text = empregado.serieCTPS;
                    if (txtCTPS_Serie.Text.Trim() != "") txtCTPS_Serie.ReadOnly = true;


                    if (empregado.teMail != null)
                        txtEmail.Text = empregado.teMail.Trim();

                    txtDataAdmissao.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    txtInicioFuncao.Text = DateTime.Now.ToString("dd/MM/yyyy");

                    txtDataDemissao.Text = "";
                    //txtDataAdmissao.ReadOnly = true;

                    //readmissão não deve carregar matrícula,  geralmente muda, e se não ficarem atentos salvam com a matrícula antiga
                    //txtMatricula.Text = empregado.matricula;

                    txtPISPASEP.Text = empregado.PISPASEP;
                    if (txtPISPASEP.Text.Trim() != "") txtPISPASEP.ReadOnly = true;

                    if (empregado.DataNascimento.ToString("dd/MM/yyyy") == "01/01/0001")
                        txtDataNascimento.Text = "";
                    else
                        txtDataNascimento.Text = empregado.DataNascimento.ToString("dd/MM/yyyy");

                    if (txtDataNascimento.Text.Trim() != "") txtDataNascimento.ReadOnly = true;

                    txtRG.Text = empregado.Identidade;
                    if (txtRG.Text.Trim() != "") txtRG.ReadOnly = true;


                    txtCTPS_UF.Text = empregado.ufCTPS;
                    if (txtCTPS_UF.Text.Trim() != "") txtCTPS_UF.ReadOnly = true;

                    cbSexo.SelectedValue = empregado.Sexoempregado;
                    cbSexo.Enabled = true;
                                                        

                }


            }
        }


        protected void PopulaGrid()
        {

            //carregar combos de setor, funcao e cargo
            cmb_Setor1.Items.Clear();
            cmb_Setor2.Items.Clear();
            cmb_Setor3.Items.Clear();
            cmb_Setor4.Items.Clear();
            cmb_Setor5.Items.Clear();

            cmb_Cargo1.Items.Clear();
            cmb_Cargo2.Items.Clear();
            cmb_Cargo3.Items.Clear();
            cmb_Cargo4.Items.Clear();
            cmb_Cargo5.Items.Clear();

            cmb_Funcao1.Items.Clear();
            cmb_Funcao2.Items.Clear();
            cmb_Funcao3.Items.Clear();
            cmb_Funcao4.Items.Clear();
            cmb_Funcao5.Items.Clear();



            Ilitera.Data.PPRA_EPI xGHE = new Ilitera.Data.PPRA_EPI();

            DataSet dS1 = xGHE.Carregar_Setores(System.Convert.ToInt32(Session["Empresa"].ToString()));

            cmb_Setor1.DataSource = dS1;
            cmb_Setor1.DataValueField = "nID_SETOR";
            cmb_Setor1.DataTextField = "tNO_STR_EMPR";
            cmb_Setor1.DataBind();

            cmb_Setor2.DataSource = dS1;
            cmb_Setor2.DataValueField = "nID_SETOR";
            cmb_Setor2.DataTextField = "tNO_STR_EMPR";
            cmb_Setor2.DataBind();

            cmb_Setor3.DataSource = dS1;
            cmb_Setor3.DataValueField = "nID_SETOR";
            cmb_Setor3.DataTextField = "tNO_STR_EMPR";
            cmb_Setor3.DataBind();

            cmb_Setor4.DataSource = dS1;
            cmb_Setor4.DataValueField = "nID_SETOR";
            cmb_Setor4.DataTextField = "tNO_STR_EMPR";
            cmb_Setor4.DataBind();

            cmb_Setor5.DataSource = dS1;
            cmb_Setor5.DataValueField = "nID_SETOR";
            cmb_Setor5.DataTextField = "tNO_STR_EMPR";
            cmb_Setor5.DataBind();


            Ilitera.Opsa.Data.Cliente rCliente = new Ilitera.Opsa.Data.Cliente();
            rCliente.Find(System.Convert.ToInt32(Session["Empresa"].ToString()));

            if (rCliente.NomeAbreviado.ToUpper().IndexOf("VITERRA") >= 0 )
            {
                DataSet dS2 = xGHE.Carregar_Funcoes_Viterra(System.Convert.ToInt32(Session["Empresa"].ToString()));
                cmb_Funcao1.DataSource = dS2;
                cmb_Funcao1.DataValueField = "IdFuncao";
                cmb_Funcao1.DataTextField = "NomeFuncao";
                cmb_Funcao1.DataBind();

                cmb_Funcao2.DataSource = dS2;
                cmb_Funcao2.DataValueField = "IdFuncao";
                cmb_Funcao2.DataTextField = "NomeFuncao";
                cmb_Funcao2.DataBind();

                cmb_Funcao3.DataSource = dS2;
                cmb_Funcao3.DataValueField = "IdFuncao";
                cmb_Funcao3.DataTextField = "NomeFuncao";
                cmb_Funcao3.DataBind();

                cmb_Funcao4.DataSource = dS2;
                cmb_Funcao4.DataValueField = "IdFuncao";
                cmb_Funcao4.DataTextField = "NomeFuncao";
                cmb_Funcao4.DataBind();

                cmb_Funcao5.DataSource = dS2;
                cmb_Funcao5.DataValueField = "IdFuncao";
                cmb_Funcao5.DataTextField = "NomeFuncao";
                cmb_Funcao5.DataBind();
            }
            else
            {
                DataSet dS2 = xGHE.Carregar_Funcoes(System.Convert.ToInt32(Session["Empresa"].ToString()));
                cmb_Funcao1.DataSource = dS2;
                cmb_Funcao1.DataValueField = "IdFuncao";
                cmb_Funcao1.DataTextField = "NomeFuncao";
                cmb_Funcao1.DataBind();

                cmb_Funcao2.DataSource = dS2;
                cmb_Funcao2.DataValueField = "IdFuncao";
                cmb_Funcao2.DataTextField = "NomeFuncao";
                cmb_Funcao2.DataBind();

                cmb_Funcao3.DataSource = dS2;
                cmb_Funcao3.DataValueField = "IdFuncao";
                cmb_Funcao3.DataTextField = "NomeFuncao";
                cmb_Funcao3.DataBind();

                cmb_Funcao4.DataSource = dS2;
                cmb_Funcao4.DataValueField = "IdFuncao";
                cmb_Funcao4.DataTextField = "NomeFuncao";
                cmb_Funcao4.DataBind();

                cmb_Funcao5.DataSource = dS2;
                cmb_Funcao5.DataValueField = "IdFuncao";
                cmb_Funcao5.DataTextField = "NomeFuncao";
                cmb_Funcao5.DataBind();
            }

            DataSet dS3 = xGHE.Carregar_Cargos(System.Convert.ToInt32(Session["Empresa"].ToString()));
            cmb_Cargo1.DataSource = dS3;
            cmb_Cargo1.DataValueField = "nID_CARGO";
            cmb_Cargo1.DataTextField = "tNO_CARGO";
            cmb_Cargo1.DataBind();

            cmb_Cargo2.DataSource = dS3;
            cmb_Cargo2.DataValueField = "nID_CARGO";
            cmb_Cargo2.DataTextField = "tNO_CARGO";
            cmb_Cargo2.DataBind();

            cmb_Cargo3.DataSource = dS3;
            cmb_Cargo3.DataValueField = "nID_CARGO";
            cmb_Cargo3.DataTextField = "tNO_CARGO";
            cmb_Cargo3.DataBind();

            cmb_Cargo4.DataSource = dS3;
            cmb_Cargo4.DataValueField = "nID_CARGO";
            cmb_Cargo4.DataTextField = "tNO_CARGO";
            cmb_Cargo4.DataBind();

            cmb_Cargo5.DataSource = dS3;
            cmb_Cargo5.DataValueField = "nID_CARGO";
            cmb_Cargo5.DataTextField = "tNO_CARGO";
            cmb_Cargo5.DataBind();

            


        }


        //protected void gridEmpregados_CellSelectionChanged(object sender, SelectedCellEventArgs e)
        //{
        //    try
        //    {
        //        if (e.CurrentSelectedCells.Count > 0)
        //        {
        //            wtClassificacaoFuncional.Visible = true;
        //            string idClassificacaoFuncional = e.CurrentSelectedCells[0].Row.DataKey.GetValue(1).ToString();

        //            var gridSituacaoFuncional = (WebDataGrid)sender;
                               

                    
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "alerta", String.Format("alert('{0}')", ex.Message), true);
        //    }
        //}

        #endregion 

        #region metodos

        public void selecionarDadosCadastraisEmpregado()
        {
            try
            {
                if (Session["Empregado"] != null && Session["Empregado"].ToString().Trim() != String.Empty)
                {
                    Entities.Empregado empregado = new Entities.Empregado();
                    empregado = EmpregadoFacade.RetornarEmpregadoDadosCadastrais(Convert.ToInt32(Session["Empregado"]));

                    txtNomeEmpregado.Text = empregado.NomeEmpregado;
                    txtApelidoEmpregado.Text = empregado.ApelidoEmpregado;
                    txtCPF.Text = empregado.Cpf;
                    txtCTPS_Num.Text = empregado.numCTPS;
                    txtCTPS_Serie.Text = empregado.serieCTPS;
                    txtDataAdmissao.Text = empregado.DataAdmissao.ToString("dd/MM/yyyy");
                    txtDataDemissao.Text = empregado.DataDemissao.ToString("dd/MM/yyyy");
                    //txtMatricula.Text = empregado.matricula;
                    txtPISPASEP.Text = empregado.PISPASEP;
                    txtDataNascimento.Text = empregado.DataNascimento.ToString("dd/MM/yyyy");
                    txtRG.Text = empregado.Identidade;
                    txtCTPS_UF.Text = empregado.ufCTPS;
                    cbSexo.SelectedValue = empregado.Sexoempregado;
                    lblNomeEmpregado.Text = Session["NomeEmpregado"].ToString();
                }
            }
            catch (Exception ex)
            {
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "alerta", String.Format("alert('{0}')", ex.Message), true);
                MsgBox1.Show("Ilitera.Net", ex.Message, null,
                new EO.Web.MsgBoxButton("OK"));

            }
        }


        public void selecionarDetalheClassificacaoFuncional()
        {

        }

        #endregion



        static string GetNumberFromStrFaster(string str)
        {
            str = str.Trim();
            //Match m = new Regex(@"^[\+\-]?\d*\.?[Ee]?[\+\-]?\d*$", RegexOptions.Compiled).Match(str);
            //return (m.Value);

            //for (int xCont = 0; xCont<str.Length - 1; xCont++)
            //{
            //if ( str.Substring(xCont,1) 

            //}

            return string.Join(null, System.Text.RegularExpressions.Regex.Split(str, "[^\\d]"));

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


            Validar = zData.Substring(6,4);
            isNumerical = int.TryParse(Validar, out myInt);
            if (isNumerical == false)
            {
                MsgBox1.Show("Ilitera.Net", "Ano Inválido.  Utilizar formato dd/MM/yyyy", null,
                new EO.Web.MsgBoxButton("OK"));
                return false;
            }


            if ( zData.Substring(2,1) != "/"  ||  zData.Substring(5,1) != "/"  )
            {
                MsgBox1.Show("Ilitera.Net", "Data Inválida.  Utilizar formato dd/MM/yyyy", null,
                new EO.Web.MsgBoxButton("OK"));
                return false;
            }

            
            zDia = System.Convert.ToInt32( zData.Substring(0,2));
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

            return true ;

        }



        protected void cmd_Atualizar_Click(object sender, EventArgs e)
        {

            int zId;
            string xCargo;
            string xSetor;
            string xFuncao;
            string xBeneficiario = "0";

            System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");

            try
            {

                txtDataAdmissao.Text = txtDataAdmissao.Text.Trim();
                txtDataDemissao.Text = txtDataDemissao.Text.Trim();
                txtDataNascimento.Text = txtDataNascimento.Text.Trim();

                //checar se data admissao, nascimento ok
                if (Validar_Data(txtDataAdmissao.Text.Trim()) == false)
                {
                    MsgBox1.Show("Ilitera.Net", "Data de Admissão Inválida.", null,
                    new EO.Web.MsgBoxButton("OK"));
                    return;
                }

                if (txtDataNascimento.Text != "")
                {
                    if (Validar_Data(txtDataNascimento.Text.Trim()) == false)
                    {
                        MsgBox1.Show("Ilitera.Net", "Data de Nascimento Inválida.", null,
                        new EO.Web.MsgBoxButton("OK"));
                        return;
                    }
                    else
                    {
                        DateTime rNasc = System.Convert.ToDateTime(txtDataNascimento.Text, ptBr);

                        if (rNasc > System.DateTime.Now.AddYears(-14) )
                        {
                            MsgBox1.Show("Ilitera.Net", "Data de Nascimento Inválida.", null,
                            new EO.Web.MsgBoxButton("OK"));
                            return;
                        }
                    }
                }
                else
                {
                    MsgBox1.Show("Ilitera.Net", "Data de Nascimento Inválida.", null,
                    new EO.Web.MsgBoxButton("OK"));
                    return;
                }


                if (txtDataDemissao.Text != "")
                {
                    if (Validar_Data(txtDataDemissao.Text.Trim()) == false)
                    {
                        return;
                    }
                }


                if (txtCPF.Text.Trim() != "")
                {
                    if (Valida_CPF(txtCPF.Text.Trim()) == false)
                    {
                        MsgBox1.Show("Ilitera.Net", "CPF Inválido.", null,
                        new EO.Web.MsgBoxButton("OK"));
                        return;
                    }
                }
                else
                {
                    MsgBox1.Show("Ilitera.Net", "CPF Inválido.", null,
                    new EO.Web.MsgBoxButton("OK"));
                    return;
                }

                if (txtPISPASEP.Text.Trim() != "" && txtPISPASEP.Text.Trim() != "0")
                {
                    if (Valida_PIS(txtPISPASEP.Text.Trim()) == false)
                    {
                        MsgBox1.Show("Ilitera.Net", "PIS/PASEP Inválido.", null,
                        new EO.Web.MsgBoxButton("OK"));
                        return;
                    }
                }

                if ( cbSexo.SelectedIndex < 1)
                {
                    MsgBox1.Show("Ilitera.Net", "Sexo Inválido.", null,
                    new EO.Web.MsgBoxButton("OK"));
                    return;
                }

                //checar se pelo menos 1 classificacao funcional foi preenchida
                if ((txtInicioFuncao.Text == "") || (txtFuncao.Text == "" && cmb_Funcao1.SelectedIndex <= 0) || (txtSetor.Text == "" && cmb_Setor1.SelectedIndex <= 0) || (txtCargo.Text == "" && cmb_Cargo1.SelectedIndex <= 0))
                {
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "alerta", String.Format("alert('{0}')", "Funcionário deve ter pelo menos uma classificação funcional preenchida de forma completa."), true);
                    MsgBox1.Show("Ilitera.Net", "Funcionário deve ter pelo menos uma classificação funcional preenchida de forma completa.", null,
                    new EO.Web.MsgBoxButton("OK"));
                    return;

                }
                else
                {


                    //checar se data admissao, nascimento ok
                    if (txtInicioFuncao.Text.Trim() != "")
                    {
                        if (Validar_Data(txtInicioFuncao.Text.Trim()) == false)
                        {
                            return;
                        }
                    }

                    if (txtTerminoFuncao.Text.Trim() != "")
                    {
                        if (Validar_Data(txtTerminoFuncao.Text.Trim()) == false)
                        {
                            return;
                        }
                    }

                    if (txtInicioFuncao2.Text.Trim() != "")
                    {
                        if (Validar_Data(txtInicioFuncao2.Text.Trim()) == false)
                        {
                            return;
                        }
                    }

                    if (txtTerminoFuncao2.Text.Trim() != "")
                    {
                        if (Validar_Data(txtTerminoFuncao2.Text.Trim()) == false)
                        {
                            return;
                        }
                    }


                    if (txtInicioFuncao3.Text.Trim() != "")
                    {
                        if (Validar_Data(txtInicioFuncao3.Text.Trim()) == false)
                        {
                            return;
                        }
                    }

                    if (txtTerminoFuncao3.Text.Trim() != "")
                    {
                        if (Validar_Data(txtTerminoFuncao3.Text.Trim()) == false)
                        {
                            return;
                        }
                    }

                    if (txtInicioFuncao4.Text.Trim() != "")
                    {
                        if (Validar_Data(txtInicioFuncao4.Text.Trim()) == false)
                        {
                            return;
                        }
                    }

                    if (txtTerminoFuncao4.Text.Trim() != "")
                    {
                        if (Validar_Data(txtTerminoFuncao4.Text.Trim()) == false)
                        {
                            return;
                        }
                    }

                    if (txtInicioFuncao5.Text.Trim() != "")
                    {
                        if (Validar_Data(txtInicioFuncao5.Text.Trim()) == false)
                        {
                            return;
                        }
                    }

                    if (txtTerminoFuncao5.Text.Trim() != "")
                    {
                        if (Validar_Data(txtTerminoFuncao5.Text.Trim()) == false)
                        {
                            return;
                        }
                    }



                    
                    DateTime zAdm = System.Convert.ToDateTime(txtDataAdmissao.Text, ptBr);

                    //checar se término função é anterior à data de admissão e a data início da classif.funcional
                    if (txtTerminoFuncao.Text.Trim() != "")
                    {                        
                        DateTime zFim = System.Convert.ToDateTime(txtTerminoFuncao.Text, ptBr);
                        DateTime zInicio = System.Convert.ToDateTime(txtInicioFuncao.Text, ptBr);

                        if (zFim < zInicio)
                        {
                            MsgBox1.Show("Ilitera.Net", "Data Final da Classificação Funcional anterior à Data Inicial.", null,
                            new EO.Web.MsgBoxButton("OK"));
                            return;
                        }                                                

                        if (zFim < zAdm)
                        {
                            MsgBox1.Show("Ilitera.Net", "Data Final da Classificação Funcional anterior à Data de Admissão.", null,
                            new EO.Web.MsgBoxButton("OK"));
                            return;
                        }

                    }

                    if ( txtInicioFuncao.Text != "")
                    {
                        DateTime zInicio = System.Convert.ToDateTime(txtInicioFuncao.Text, ptBr);

                        if (zInicio < zAdm)
                        {
                            MsgBox1.Show("Ilitera.Net", "Data  da Classificação Funcional anterior à Data de Admissão.", null,
                            new EO.Web.MsgBoxButton("OK"));
                            return;
                        }
                    }


                    //checar se término função é anterior à data de admissão e a data início da classif.funcional
                    if (txtTerminoFuncao2.Text.Trim() != "")
                    {                        
                        DateTime zFim = System.Convert.ToDateTime(txtTerminoFuncao2.Text, ptBr);
                        DateTime zInicio = System.Convert.ToDateTime(txtInicioFuncao2.Text, ptBr);

                        if (zFim < zInicio)
                        {
                            MsgBox1.Show("Ilitera.Net", "Data Final da Classificação Funcional anterior à Data Inicial.", null,
                            new EO.Web.MsgBoxButton("OK"));
                            return;
                        }

                        
                        if (zFim < zAdm)
                        {
                            MsgBox1.Show("Ilitera.Net", "Data Final da Classificação Funcional anterior à Data de Admissão.", null,
                            new EO.Web.MsgBoxButton("OK"));
                            return;
                        }
                        
                    }

                    if (txtInicioFuncao2.Text != "")
                    {
                        DateTime zInicio = System.Convert.ToDateTime(txtInicioFuncao2.Text, ptBr);

                        if (zInicio < zAdm)
                        {
                            MsgBox1.Show("Ilitera.Net", "Data  da Classificação Funcional anterior à Data de Admissão.", null,
                            new EO.Web.MsgBoxButton("OK"));
                            return;
                        }
                    }



                    //checar se término função é anterior à data de admissão e a data início da classif.funcional
                    if (txtTerminoFuncao3.Text.Trim() != "")
                    {                        
                        DateTime zFim = System.Convert.ToDateTime(txtTerminoFuncao3.Text, ptBr);
                        DateTime zInicio = System.Convert.ToDateTime(txtInicioFuncao3.Text, ptBr);

                        if (zFim < zInicio)
                        {
                            MsgBox1.Show("Ilitera.Net", "Data Final da Classificação Funcional anterior à Data Inicial.", null,
                            new EO.Web.MsgBoxButton("OK"));
                            return;
                        }
                                                

                        if (zFim < zAdm)
                        {
                            MsgBox1.Show("Ilitera.Net", "Data Final da Classificação Funcional anterior à Data de Admissão.", null,
                            new EO.Web.MsgBoxButton("OK"));
                            return;
                        }


                    }

                    if (txtInicioFuncao3.Text != "")
                    {
                        DateTime zInicio = System.Convert.ToDateTime(txtInicioFuncao3.Text, ptBr);

                        if (zInicio < zAdm)
                        {
                            MsgBox1.Show("Ilitera.Net", "Data  da Classificação Funcional anterior à Data de Admissão.", null,
                            new EO.Web.MsgBoxButton("OK"));
                            return;
                        }
                    }



                    //checar se término função é anterior à data de admissão e a data início da classif.funcional
                    if (txtTerminoFuncao4.Text.Trim() != "")
                    {                        
                        DateTime zFim = System.Convert.ToDateTime(txtTerminoFuncao4.Text, ptBr);
                        DateTime zInicio = System.Convert.ToDateTime(txtInicioFuncao4.Text, ptBr);

                        if (zFim < zInicio)
                        {
                            MsgBox1.Show("Ilitera.Net", "Data Final da Classificação Funcional anterior à Data Inicial.", null,
                            new EO.Web.MsgBoxButton("OK"));
                            return;
                        }
                                                

                        if (zFim < zAdm)
                        {
                            MsgBox1.Show("Ilitera.Net", "Data Final da Classificação Funcional anterior à Data de Admissão.", null,
                            new EO.Web.MsgBoxButton("OK"));
                            return;
                        }


                    }


                    if (txtInicioFuncao4.Text != "")
                    {
                        DateTime zInicio = System.Convert.ToDateTime(txtInicioFuncao4.Text, ptBr);

                        if (zInicio < zAdm)
                        {
                            MsgBox1.Show("Ilitera.Net", "Data  da Classificação Funcional anterior à Data de Admissão.", null,
                            new EO.Web.MsgBoxButton("OK"));
                            return;
                        }
                    }



                    //checar se término função é anterior à data de admissão e a data início da classif.funcional
                    if (txtTerminoFuncao5.Text.Trim() != "")
                    {                        
                        DateTime zFim = System.Convert.ToDateTime(txtTerminoFuncao5.Text, ptBr);
                        DateTime zInicio = System.Convert.ToDateTime(txtInicioFuncao5.Text, ptBr);

                        if (zFim < zInicio)
                        {
                            MsgBox1.Show("Ilitera.Net", "Data Final da Classificação Funcional anterior à Data Inicial.", null,
                            new EO.Web.MsgBoxButton("OK"));
                            return;
                        }
                                                

                        if (zFim < zAdm)
                        {
                            MsgBox1.Show("Ilitera.Net", "Data Final da Classificação Funcional anterior à Data de Admissão.", null,
                            new EO.Web.MsgBoxButton("OK"));
                            return;
                        }
                        
                    }

                    if (txtInicioFuncao5.Text != "")
                    {
                        DateTime zInicio = System.Convert.ToDateTime(txtInicioFuncao5.Text, ptBr);

                        if (zInicio < zAdm)
                        {
                            MsgBox1.Show("Ilitera.Net", "Data  da Classificação Funcional anterior à Data de Admissão.", null,
                            new EO.Web.MsgBoxButton("OK"));
                            return;
                        }
                    }



                    Ilitera.Opsa.Data.Cliente rCliente = new Ilitera.Opsa.Data.Cliente();
                    rCliente.Find(System.Convert.ToInt32(Session["Empresa"].ToString()));



                    //ver se campo nome foi colado do Excel com \n\r e espaços - retirar
                    string gNome = "";

                    for (int gAux = 0; gAux < txtNomeEmpregado.Text.Length; gAux++)
                    {
                        if (!(txtNomeEmpregado.Text.Substring(gAux, 1) == "\n" || txtNomeEmpregado.Text.Substring(gAux, 1) == "\r" || txtNomeEmpregado.Text.Substring(gAux, 1) == "\""))
                        {
                            gNome = gNome + txtNomeEmpregado.Text.Substring(gAux, 1);
                        }
                    }

                    txtNomeEmpregado.Text = gNome.Trim();




                    //não for readmissão 
                    if (Request["IdEmpregado"] != null && Request["IdEmpregado"].ToString().Trim() == "0")
                    {



                        //checar se nome, RG, CPF ou PIS já existem no sistema com colaborador ativo
                        Ilitera.Opsa.Data.Empregado zEmpregado = new Ilitera.Opsa.Data.Empregado();


                        if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().IndexOf("Prajna") > 0 || Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("GLOBAL") > 0)
                        {
                            string zBusca = "";

                            if (txtCPF.Text.Trim() != "")
                            {
                                zBusca = " tNo_CPF collate Latin1_General_CI_AI = '" + txtCPF.Text.Trim() + "'  or  tNo_CPF collate Latin1_General_CI_AI = '" + GetNumberFromStrFaster(txtCPF.Text) + "' or ";
                            }

                            if (txtPISPASEP.Text.Trim() != "")
                            {
                                zBusca = zBusca + " nNo_PIS_PASEP  = " + GetNumberFromStrFaster(txtPISPASEP.Text) + " or ";
                            }

                            if (txtRG.Text.Trim() != "")
                            {
                                zBusca = zBusca + " tNo_Identidade collate Latin1_General_CI_AI = '" + txtRG.Text.Trim() + "' or ";
                            }

                            if (zBusca == "")
                            {
                                zEmpregado.Find(" tNo_Empg collate Latin1_General_CI_AI = '" + txtNomeEmpregado.Text.Trim() + "' and nId_Empr = " + lbl_Id_Empresa.Text + "  ");
                            }
                            else
                            {
                                if ( Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("GLOBAL") > 0)
                                {
                                    zEmpregado.Find(" tNo_Empg <> '' and hDt_Dem is null and ( " + zBusca.Substring(0, zBusca.Length - 3) + " ) ");
                                }
                                else
                                {
                                    zEmpregado.Find(" tNo_Empg collate Latin1_General_CI_AI = '" + txtNomeEmpregado.Text.Trim() + "' and nId_Empr = " + lbl_Id_Empresa.Text + " and ( " + zBusca.Substring(0, zBusca.Length - 3) + " ) ");
                                }
                            }

                        }
                        else
                        {
                            zEmpregado.Find(" tNo_Empg collate Latin1_General_CI_AI = '" + txtNomeEmpregado.Text.Trim() + "' and nId_Empr = " + lbl_Id_Empresa.Text + " ");
                        }


                        if (zEmpregado.tNO_EMPG.Trim() != "")
                        {
                            MsgBox1.Show("Ilitera.Net", "Colaborador já está cadastrado no sistema.", null,
                            new EO.Web.MsgBoxButton("OK"));
                            return;
                        }

                        //CPF com formatação
                        if (txtCPF.Text.Trim() != "")
                        {
                            zEmpregado = new Ilitera.Opsa.Data.Empregado();
                            zEmpregado.Find(" tNo_CPF collate Latin1_General_CI_AI = '" + txtCPF.Text.Trim() + "' and nId_Empr = " + lbl_Id_Empresa.Text + " ");

                            if (zEmpregado.tNO_EMPG.Trim() != "")
                            {
                                MsgBox1.Show("Ilitera.Net", "Colaborador (CPF) já está cadastrado no sistema.", null,
                                new EO.Web.MsgBoxButton("OK"));
                                return;
                            }
                        }

                        txtCPF.Text = GetNumberFromStrFaster(txtCPF.Text);

                        //CPF sem formatação
                        if (txtCPF.Text.Trim() != "")
                        {
                            zEmpregado = new Ilitera.Opsa.Data.Empregado();
                            zEmpregado.Find(" tNo_CPF collate Latin1_General_CI_AI = '" + txtCPF.Text.Trim() + "' and nId_Empr = " + lbl_Id_Empresa.Text + " ");

                            if (zEmpregado.tNO_EMPG.Trim() != "")
                            {
                                MsgBox1.Show("Ilitera.Net", "Colaborador (CPF) já está cadastrado no sistema.", null,
                                new EO.Web.MsgBoxButton("OK"));
                                return;
                            }
                        }

                        txtPISPASEP.Text = GetNumberFromStrFaster(txtPISPASEP.Text);

                        if (txtPISPASEP.Text.Trim() != "")
                        {
                            zEmpregado = new Ilitera.Opsa.Data.Empregado();
                            zEmpregado.Find(" nNo_PIS_PASEP = " + txtPISPASEP.Text.Trim() + " and nId_Empr = " + lbl_Id_Empresa.Text + " ");

                            if (zEmpregado.tNO_EMPG.Trim() != "")
                            {
                                MsgBox1.Show("Ilitera.Net", "Colaborador (PIS) já está cadastrado no sistema.", null,
                                new EO.Web.MsgBoxButton("OK"));
                                return;
                            }

                        }


                        if (txtRG.Text.Trim() != "")
                        {
                            zEmpregado = new Ilitera.Opsa.Data.Empregado();
                            zEmpregado.Find(" tNo_Identidade collate Latin1_General_CI_AI = '" + txtRG.Text.Trim() + "' and nId_Empr = " + lbl_Id_Empresa.Text + " ");

                            if (zEmpregado.tNO_EMPG.Trim() != "")
                            {
                                MsgBox1.Show("Ilitera.Net", "Colaborador (RG) já está cadastrado no sistema.", null,
                                new EO.Web.MsgBoxButton("OK"));
                                return;
                            }
                        }



                        if (txtDataDemissao.Text.Trim() != "")
                        {


                            if (rCliente.Bloquear_Data_Demissao == true)
                            {


                                if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().IndexOf("Prajna") > 0)
                                {
                                    MsgBox1.Show("Ilitera.Net", "Colaborador está configurado para não ter a data de demissão preenchida. Entrar em contato com a central de atendimento no e-mail centraldeatendimento@essencenet.com.br ", null,
                                    new EO.Web.MsgBoxButton("OK"));
                                }
                                else if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().IndexOf("Daiti") > 0)
                                {
                                    MsgBox1.Show("Ilitera.Net", "Colaborador está configurado para não ter a data de demissão preenchida. Entrar em contato com a central de atendimento no e-mail agendamento.sp.dai@ilitera.com.br ", null,
                                    new EO.Web.MsgBoxButton("OK"));
                                }
                                else
                                {
                                    MsgBox1.Show("Ilitera.Net", "Colaborador está configurado para não ter a data de demissão preenchida. Entrar em contato com a central de atendimento no e-mail atendimento@ilitera.com.br ", null,
                                    new EO.Web.MsgBoxButton("OK"));
                                }

                                return;

                            }

                        }


                    }



                    if (opt_Benef_NA.Checked == true) xBeneficiario = "3";
                    else if (opt_Benef_Reabilitado.Checked == true) xBeneficiario = "1";
                    else if (opt_Benef_Deficiencia.Checked == true) xBeneficiario = "2";


                    //salvar dados
                    Ilitera.Data.Empregado_Cadastral xEmpregado;

                    xEmpregado = new Ilitera.Data.Empregado_Cadastral();

                    //txtPISPASEP.Text = GetNumberFromStrFaster(txtPISPASEP.Text);
                    //txtCPF.Text = GetNumberFromStrFaster(txtCPF.Text);


                    zId = xEmpregado.Inserir_Dados_Empregado(lbl_Id_Empresa.Text, txtNomeEmpregado.Text, cbSexo.SelectedValue.ToString(), txtCTPS_Num.Text, txtCTPS_Serie.Text, txtCTPS_UF.Text, txtMatricula.Text, txtRG.Text, txtDataAdmissao.Text, txtDataDemissao.Text, txtDataNascimento.Text, txtPISPASEP.Text, txtCPF.Text, txtApelidoEmpregado.Text, System.Convert.ToInt32( lbl_Id_Usuario.Text ), xBeneficiario, txtEmail.Text);

                    Session["Empregado"] = zId.ToString();
                    Session["NomeEmpregado"] = txtNomeEmpregado.Text;
                    lblNomeEmpregado.Text = txtNomeEmpregado.Text;




                    //se selecionar combo de cargo, funcao ou setor,  ele prevalecerá sobre textbox.  Ver se SelectedValue <> 0
                    if (cmb_Cargo1.SelectedValue.ToString().Trim() != "0")    xCargo = cmb_Cargo1.Items[cmb_Cargo1.SelectedIndex].ToString();
                    else                                                      xCargo = txtCargo.Text.ToUpper();
                    
                    if (cmb_Setor1.SelectedValue.ToString().Trim() != "0")    xSetor = cmb_Setor1.Items[cmb_Setor1.SelectedIndex].ToString();
                    else                                                      xSetor = txtSetor.Text.ToUpper();
                    
                    if (cmb_Funcao1.SelectedValue.ToString().Trim() != "0")   xFuncao = cmb_Funcao1.Items[cmb_Funcao1.SelectedIndex].ToString();
                    else                                                      xFuncao = txtFuncao.Text.ToUpper();

                    if (xFuncao.IndexOf("|") > 0)
                    {
                        string xCodigo = xFuncao.Substring(xFuncao.IndexOf("|") + 1).Trim();
                        xFuncao = xFuncao.Substring(0, xFuncao.IndexOf("|") - 1);                        
                        xEmpregado.Inserir_Classificacao_Funcional2(zId, System.Convert.ToInt32(lbl_Id_Empresa.Text), txtInicioFuncao.Text, txtTerminoFuncao.Text, xCargo, xSetor, xFuncao, System.Convert.ToInt32(lbl_Id_Usuario.Text), "", xCodigo);
                    }
                    else
                    {
                        //salvar classificacoes funcionais
                        //xEmpregado.Inserir_Classificacao_Funcional(zId, System.Convert.ToInt32(lbl_Id_Empresa.Text), txtInicioFuncao.Text, txtTerminoFuncao.Text, txtCargo.Text.ToUpper(), txtSetor.Text.ToUpper(), txtFuncao.Text.ToUpper());

                        xEmpregado.Inserir_Classificacao_Funcional(zId, System.Convert.ToInt32(lbl_Id_Empresa.Text), txtInicioFuncao.Text, txtTerminoFuncao.Text, xCargo, xSetor, xFuncao, System.Convert.ToInt32(lbl_Id_Usuario.Text), "");
                    }



                    if (txtInicioFuncao2.Text != "" &&  ( txtFuncao2.Text != "" || cmb_Funcao2.SelectedIndex != 0 ) && ( txtSetor2.Text != "" || cmb_Setor2.SelectedIndex != 0) )
                    {
                        if (cmb_Cargo2.SelectedValue.ToString().Trim() != "0") xCargo = cmb_Cargo2.Items[cmb_Cargo2.SelectedIndex].ToString();
                        else xCargo = txtCargo2.Text.ToUpper();

                        if (cmb_Setor2.SelectedValue.ToString().Trim() != "0") xSetor = cmb_Setor2.Items[cmb_Setor2.SelectedIndex].ToString();
                        else xSetor = txtSetor2.Text.ToUpper();

                        if (cmb_Funcao2.SelectedValue.ToString().Trim() != "0") xFuncao = cmb_Funcao2.Items[cmb_Funcao2.SelectedIndex].ToString();
                        else xFuncao = txtFuncao2.Text.ToUpper();

                        if (xFuncao.IndexOf("|") > 0)
                        {
                            string xCodigo = xFuncao.Substring(xFuncao.IndexOf("|") + 1).Trim();
                            xFuncao = xFuncao.Substring(0, xFuncao.IndexOf("|") - 1);                            
                            xEmpregado.Inserir_Classificacao_Funcional2(zId, System.Convert.ToInt32(lbl_Id_Empresa.Text), txtInicioFuncao2.Text, txtTerminoFuncao2.Text, xCargo, xSetor, xFuncao, System.Convert.ToInt32(lbl_Id_Usuario.Text), "", xCodigo);
                        }
                        else
                        {
                            xEmpregado.Inserir_Classificacao_Funcional(zId, System.Convert.ToInt32(lbl_Id_Empresa.Text), txtInicioFuncao2.Text, txtTerminoFuncao2.Text, xCargo, xSetor, xFuncao, System.Convert.ToInt32(lbl_Id_Usuario.Text), "");
                        }


                    }


                    if (txtInicioFuncao3.Text != "" && (txtFuncao3.Text != "" || cmb_Funcao3.SelectedIndex != 0) && (txtSetor3.Text != "" || cmb_Setor3.SelectedIndex != 0))
                    {
                        if (cmb_Cargo3.SelectedValue.ToString().Trim() != "0") xCargo = cmb_Cargo3.Items[cmb_Cargo3.SelectedIndex].ToString();
                        else xCargo = txtCargo3.Text.ToUpper();

                        if (cmb_Setor3.SelectedValue.ToString().Trim() != "0") xSetor = cmb_Setor3.Items[cmb_Setor3.SelectedIndex].ToString();
                        else xSetor = txtSetor3.Text.ToUpper();

                        if (cmb_Funcao3.SelectedValue.ToString().Trim() != "0") xFuncao = cmb_Funcao3.Items[cmb_Funcao3.SelectedIndex].ToString();
                        else xFuncao = txtFuncao3.Text.ToUpper();

                        if (xFuncao.IndexOf("|") > 0)
                        {
                            string xCodigo = xFuncao.Substring(xFuncao.IndexOf("|") + 1).Trim();
                            xFuncao = xFuncao.Substring(0, xFuncao.IndexOf("|") - 1);                            
                            xEmpregado.Inserir_Classificacao_Funcional2(zId, System.Convert.ToInt32(lbl_Id_Empresa.Text), txtInicioFuncao3.Text, txtTerminoFuncao3.Text, xCargo, xSetor, xFuncao, System.Convert.ToInt32(lbl_Id_Usuario.Text), "", xCodigo);
                        }
                        else
                        {
                            xEmpregado.Inserir_Classificacao_Funcional(zId, System.Convert.ToInt32(lbl_Id_Empresa.Text), txtInicioFuncao3.Text, txtTerminoFuncao3.Text, xCargo, xSetor, xFuncao, System.Convert.ToInt32(lbl_Id_Usuario.Text), "");
                        }
                    }


                    if (txtInicioFuncao4.Text != "" && (txtFuncao4.Text != "" || cmb_Funcao4.SelectedIndex != 0) && (txtSetor4.Text != "" || cmb_Setor4.SelectedIndex != 0))
                    {
                        if (cmb_Cargo4.SelectedValue.ToString().Trim() != "0") xCargo = cmb_Cargo4.Items[cmb_Cargo4.SelectedIndex].ToString();
                        else xCargo = txtCargo4.Text.ToUpper();

                        if (cmb_Setor4.SelectedValue.ToString().Trim() != "0") xSetor = cmb_Setor4.Items[cmb_Setor4.SelectedIndex].ToString();
                        else xSetor = txtSetor4.Text.ToUpper();

                        if (cmb_Funcao4.SelectedValue.ToString().Trim() != "0") xFuncao = cmb_Funcao4.Items[cmb_Funcao4.SelectedIndex].ToString();
                        else xFuncao = txtFuncao4.Text.ToUpper();

                        if (xFuncao.IndexOf("|") > 0)
                        {
                            string xCodigo = xFuncao.Substring(xFuncao.IndexOf("|") + 1).Trim();
                            xFuncao = xFuncao.Substring(0, xFuncao.IndexOf("|") - 1);                            
                            xEmpregado.Inserir_Classificacao_Funcional2(zId, System.Convert.ToInt32(lbl_Id_Empresa.Text), txtInicioFuncao4.Text, txtTerminoFuncao4.Text, xCargo, xSetor, xFuncao, System.Convert.ToInt32(lbl_Id_Usuario.Text), "", xCodigo);
                        }
                        else
                        {
                            xEmpregado.Inserir_Classificacao_Funcional(zId, System.Convert.ToInt32(lbl_Id_Empresa.Text), txtInicioFuncao4.Text, txtTerminoFuncao4.Text, xCargo, xSetor, xFuncao, System.Convert.ToInt32(lbl_Id_Usuario.Text), "");
                        }

                    }


                    if (txtInicioFuncao5.Text != "" && (txtFuncao5.Text != "" || cmb_Funcao5.SelectedIndex != 0) && (txtSetor5.Text != "" || cmb_Setor5.SelectedIndex != 0))
                    {
                        if (cmb_Cargo5.SelectedValue.ToString().Trim() != "0") xCargo = cmb_Cargo5.Items[cmb_Cargo5.SelectedIndex].ToString();
                        else xCargo = txtCargo5.Text.ToUpper();

                        if (cmb_Setor5.SelectedValue.ToString().Trim() != "0") xSetor = cmb_Setor5.Items[cmb_Setor5.SelectedIndex].ToString();
                        else xSetor = txtSetor5.Text.ToUpper();

                        if (cmb_Funcao5.SelectedValue.ToString().Trim() != "0") xFuncao = cmb_Funcao5.Items[cmb_Funcao5.SelectedIndex].ToString();
                        else xFuncao = txtFuncao5.Text.ToUpper();

                        if (xFuncao.IndexOf("|") > 0)
                        {
                            string xCodigo = xFuncao.Substring(xFuncao.IndexOf("|") + 1).Trim();
                            xFuncao = xFuncao.Substring(0, xFuncao.IndexOf("|") - 1);                            
                            xEmpregado.Inserir_Classificacao_Funcional2(zId, System.Convert.ToInt32(lbl_Id_Empresa.Text), txtInicioFuncao5.Text, txtTerminoFuncao5.Text, xCargo, xSetor, xFuncao, System.Convert.ToInt32(lbl_Id_Usuario.Text), "", xCodigo);
                        }
                        else
                        {
                            xEmpregado.Inserir_Classificacao_Funcional(zId, System.Convert.ToInt32(lbl_Id_Empresa.Text), txtInicioFuncao5.Text, txtTerminoFuncao5.Text, xCargo, xSetor, xFuncao, System.Convert.ToInt32(lbl_Id_Usuario.Text), "");
                        }
                    }


                    //enviar email alerta - inserção colaborador
                    if (rCliente.Alerta_web_Inserir_Colaborador!=null && rCliente.Alerta_web_Inserir_Colaborador.Trim() != "")
                    {

                        Entities.Usuario usuario = (Entities.Usuario)Session["usuarioLogado"];

                        string xCorpo = "<body><p><font size='2' face='Tahoma'><H1 align='center'>Alerta de Inserção de Colaborador - Ili.Net</H1></font></p> <br></br>" +
                                        "<p><font size='3' face='Tahoma'>Nome: " + txtNomeEmpregado.Text.Trim() + "<br>" +
                                        "Data de Admissão: " + txtDataAdmissao.Text.Trim() + "<br><br>" +
                                        "Data de Nascimento: " + txtDataNascimento.Text.Trim() + "<br><br>" +
                                        "Empresa:  " + rCliente.NomeAbreviado + "<br>" +
                                        "Função: " + xFuncao + "<br>" +
                                        "Setor: " + xSetor + "<br><br>" +                        
                                        "Usuário responsável: " + usuario.NomeUsuario.ToUpper() + " em " + System.DateTime.Now.ToString("dd/MM/yyyy hh:mm", ptBr) + "<br><br>Próxima etapa: agendamento de exame, caso aplicável." + "<br></font></p></body>";

                        if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().IndexOf("Prajna") > 0)
                        {
                            Envio_Email_Prajna(rCliente.Alerta_web_Inserir_Colaborador.Trim(), "wagner@ilitera.com.br", "Alerta - Inserção de Colaborador", xCorpo, "", "Inserção de colaborador", zId, System.Convert.ToInt32(lbl_Id_Empresa.Text));
                        }
                        else
                        {
                            if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("GLOBAL") > 0)
                                Envio_Email_Global(rCliente.Alerta_web_Inserir_Colaborador.Trim(), "wagner@ilitera.com.br", "Alerta - Inserção de Colaborador", xCorpo, "", "Inserção de colaborador", zId, System.Convert.ToInt32(lbl_Id_Empresa.Text));
                            else
                                Envio_Email_Ilitera(rCliente.Alerta_web_Inserir_Colaborador.Trim(), "wagner@ilitera.com.br", "Alerta - Inserção de Colaborador", xCorpo, "", "Inserção de colaborador", zId, System.Convert.ToInt32(lbl_Id_Empresa.Text));

                        }
                    }




                    //checar se função tem GHE indicado para essa empresa
                    bool xAlocacao = false;

                    Ilitera.Data.Empregado_Cadastral xFuncao_GHE = new Ilitera.Data.Empregado_Cadastral();
                    xAlocacao = xFuncao_GHE.Criar_Alocacao_Funcao_GHE(rCliente.Id, xFuncao, zId);
                    



                    //se der certo alocação, ir para listagem colaboradores, senão, abrir página GHE abaixo
                    if ( xAlocacao == true )
                    {
                        Response.Redirect("~/ListaEmpregados.aspx");
                    }
                    else
                    {
                        Response.Redirect("~/SelecaoGHE.aspx?Colaborador=" + txtNomeEmpregado.Text.Trim());
                    }

                    //wtClassificacaoFuncional.Visible = true;

                    //Response.Redirect("~/ListaEmpregados.aspx");
                    //Response.Redirect("~/SelecaoGHE.aspx?Colaborador=" + txtNomeEmpregado.Text.Trim());
                }

            }

            catch (Exception ex)
            {
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "alerta", String.Format("alert('{0}')", ex.Message), true);
                MsgBox1.Show("Ilitera.Net", ex.Message, null,
                new EO.Web.MsgBoxButton("OK"));

            }


        }


        protected void Envio_Email_Ilitera(string xPara, string xCopia, string xSubject, string xBody, string xAttach, string xModulo, Int32 xIdEmpregado, Int32 xIdEmpresa)
        {
            string xDestinatario = "";
            //enviar e-mail para clinica e prajna

            MailMessage objEmail = new MailMessage();

            //rementente do email
            //objEmail.From = new MailAddress("agendamento.sp.sto@ilitera.com.br");


            string[] stringSeparators4 = new string[] { ";" };
            string[] result4;
            result4 = xPara.Trim().Split(stringSeparators4, StringSplitOptions.RemoveEmptyEntries);

            foreach (string s in result4)
            {
                if (s.Trim() != "")
                {
                    objEmail.To.Add(s);
                }
            }



            string[] stringSeparators5 = new string[] { ";" };
            string[] result5;
            result5 = xCopia.Trim().Split(stringSeparators5, StringSplitOptions.RemoveEmptyEntries);

            foreach (string s in result5)
            {
                if (s.Trim() != "")
                {
                    objEmail.CC.Add(s);
                }
            }





            objEmail.Priority = MailPriority.Normal;
            objEmail.IsBodyHtml = true;

            objEmail.Subject = xSubject;
            objEmail.Body = xBody;
            objEmail.SubjectEncoding = Encoding.GetEncoding("ISO-8859-1");
            objEmail.BodyEncoding = Encoding.GetEncoding("ISO-8859-1");

            if (xAttach != "")
            {
                Attachment xItem = new Attachment(xAttach);
                objEmail.Attachments.Add(xItem);
            }

            SmtpClient objSmtp = new SmtpClient();
            //objSmtp.Host = "smtp.ilitera.com.br";
            objSmtp.Host = "smtp.office365.com";
            objSmtp.EnableSsl = true;
            objSmtp.Port = 587;


            if (System.DateTime.Now.Second % 3 == 0)
            {
                objEmail.From = new MailAddress("agendamento1@ilitera.com.br");
                objSmtp.Credentials = new NetworkCredential("agendamento1@ilitera.com.br", "Ilitera_3624");
            }
            else
            {
                objEmail.From = new MailAddress("agendamento3@ilitera.com.br");
                objSmtp.Credentials = new NetworkCredential("agendamento3@ilitera.com.br", "Ilitera_3625");
            }

            objEmail.ReplyTo = new MailAddress("agendamento@ilitera.com.br");


            Ilitera.Data.Clientes_Clinicas xEnvio = new Ilitera.Data.Clientes_Clinicas();
            

            objSmtp.Send(objEmail);
            xEnvio.Envio_eMail(xIdEmpregado, xIdEmpresa, xDestinatario, "Alerta Inserção Colaborador web");



            return;

        }


        protected void Envio_Email_Global(string xPara, string xCopia, string xSubject, string xBody, string xAttach, string xModulo, Int32 xIdEmpregado, Int32 xIdEmpresa)
        {
            string xDestinatario = "";
            //enviar e-mail para clinica e prajna

            MailMessage objEmail = new MailMessage();

            //rementente do email
            //objEmail.From = new MailAddress("agendamento.sp.sto@ilitera.com.br");


            string[] stringSeparators4 = new string[] { ";" };
            string[] result4;
            result4 = xPara.Trim().Split(stringSeparators4, StringSplitOptions.RemoveEmptyEntries);

            foreach (string s in result4)
            {
                if (s.Trim() != "")
                {
                    objEmail.To.Add(s);
                }
            }



            string[] stringSeparators5 = new string[] { ";" };
            string[] result5;
            result5 = xCopia.Trim().Split(stringSeparators5, StringSplitOptions.RemoveEmptyEntries);

            foreach (string s in result5)
            {
                if (s.Trim() != "")
                {
                    objEmail.CC.Add(s);
                }
            }





            objEmail.Priority = MailPriority.Normal;
            objEmail.IsBodyHtml = true;

            objEmail.Subject = xSubject;
            objEmail.Body = xBody;
            objEmail.SubjectEncoding = Encoding.GetEncoding("ISO-8859-1");
            objEmail.BodyEncoding = Encoding.GetEncoding("ISO-8859-1");

            if (xAttach != "")
            {
                Attachment xItem = new Attachment(xAttach);
                objEmail.Attachments.Add(xItem);
            }

            SmtpClient objSmtp = new SmtpClient();
            objSmtp.Host = "smtp.globalsegmed.com.br";
            objSmtp.Port = 587;


            Ilitera.Common.Juridica xJur = new Ilitera.Common.Juridica();
            xJur.Find(xIdEmpresa);

            if (xJur.Id != 0)
            {
                if (xJur.Auxiliar == "RAMO" || xJur.Auxiliar == "GLOBAL2")
                {
                    objEmail.From = new MailAddress("guias@globalsegmed.com.br");
                    objSmtp.Credentials = new NetworkCredential("guias@globalsegmed.com.br", "Sergio2024@");

                    objEmail.ReplyTo = new MailAddress("guias@globalsegmed.com.br");

                    if (xPara.IndexOf("asos@globalsegmed.com.br") < 0)
                    {
                        objEmail.CC.Add("asos@globalsegmed.com.br");
                    }
                    //objSmtp.Host = "smtp.ramoassessoria.com.br";
                    //objEmail.From = new MailAddress("guias@ramoassessoria.com.br");
                    //objSmtp.Credentials = new NetworkCredential("guias@ramoassessoria.com.br", "Ramo2024@");

                    //objEmail.ReplyTo = new MailAddress("guias@ramoassessoria.com.br");
                }
                //else if (xJur.Auxiliar == "GLOBAL2")
                //{
                //    objEmail.From = new MailAddress("guias2@globalsegmed.com.br");
                //    objSmtp.Credentials = new NetworkCredential("guias2@globalsegmed.com.br", "Guiasglobal2#");

                //    objEmail.ReplyTo = new MailAddress("guias2@globalsegmed.com.br");
                //}
                else
                {
                    objEmail.From = new MailAddress("guias@globalsegmed.com.br");
                    objSmtp.Credentials = new NetworkCredential("guias@globalsegmed.com.br", "Sergio2024@");

                    objEmail.ReplyTo = new MailAddress("guias@globalsegmed.com.br");
                }
            }
            else
            {
                objEmail.From = new MailAddress("guias@globalsegmed.com.br");
                objSmtp.Credentials = new NetworkCredential("guias@globalsegmed.com.br", "Sergio2024@");

                objEmail.ReplyTo = new MailAddress("guias@globalsegmed.com.br");
            }


            //objEmail.From = new MailAddress("guias2@globalsegmed.com.br");
            //objSmtp.Credentials = new NetworkCredential("guias2@globalsegmed.com.br", "Guiasglobal2#");

            //objEmail.ReplyTo = new MailAddress("guias2@globalsegmed.com.br");


            Ilitera.Data.Clientes_Clinicas xEnvio = new Ilitera.Data.Clientes_Clinicas();


            objSmtp.Send(objEmail);
            xEnvio.Envio_eMail(xIdEmpregado, xIdEmpresa, xDestinatario, "Alerta Inserção Colaborador web");



            return;

        }



        protected void Envio_Email_Prajna(string xPara, string xCopia, string xSubject, string xBody, string xAttach, string xModulo, Int32 xIdEmpregado, Int32 xIdEmpresa)
        {

            string xDestinatario = "";


            MailMessage objEmail = new MailMessage();

            //rementente do email           
            //objEmail.From = new MailAddress("agendamento@5aessence.com.br");
            objEmail.From = new MailAddress("agendamento@essencenet.com.br");



            string[] stringSeparators4 = new string[] { ";" };
            string[] result4;
            result4 = xPara.Trim().Split(stringSeparators4, StringSplitOptions.RemoveEmptyEntries);

            foreach (string s in result4)
            {
                if (s.Trim() != "")
                {
                    objEmail.To.Add(s);
                }
            }



            string[] stringSeparators5 = new string[] { ";" };
            string[] result5;
            result5 = xCopia.Trim().Split(stringSeparators5, StringSplitOptions.RemoveEmptyEntries);

            foreach (string s in result5)
            {
                if (s.Trim() != "")
                {
                    objEmail.CC.Add(s);
                }
            }



            objEmail.Priority = MailPriority.Normal;
            objEmail.IsBodyHtml = true;

            objEmail.Subject = xSubject;
            objEmail.Body = xBody;
            objEmail.SubjectEncoding = Encoding.GetEncoding("ISO-8859-1");
            objEmail.BodyEncoding = Encoding.GetEncoding("ISO-8859-1");

            if (xAttach != "")
            {
                Attachment xItem = new Attachment(xAttach);
                objEmail.Attachments.Add(xItem);
            }

            SmtpClient objSmtp = new SmtpClient();
            objSmtp.Host = "mail.exchange.locaweb.com.br";
            objSmtp.Port = 587;
            objSmtp.Credentials = new NetworkCredential("agendamento@essencenet.com.br", "Vug31145");

            objSmtp.EnableSsl = false;

            //objSmtp.Host = "outlook.office.com";


            //objSmtp.Host = "smtp.office365.com";
            //objSmtp.Port = 587;                       
            //objSmtp.Credentials = new NetworkCredential("agendamento@5aessence.com.br", "Agend_5060");

            //objSmtp.Host = "smtp.5aessencenet.com.br";
            //objSmtp.Port = 587;
            //objSmtp.Credentials = new NetworkCredential("agendamento@5aessencenet.com.br", "Prana@2022!@");

            //objSmtp.Send(objEmail);

            //Ilitera.Data.Clientes_Clinicas xEnvio = new Ilitera.Data.Clientes_Clinicas();
            //xEnvio.Envio_eMail(xIdEmpregado, xIdEmpresa, xDestinatario, "Alerta Inserção Colaborador web");

            return;

        }




        protected void cmd_Voltar_Click(object sender, EventArgs e)
        {
            Response.Redirect("ListaEmpregados.aspx");
        }



        public bool Valida_CPF(string cpf)
        {

            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            string tempCpf;

            string digito;

            int soma;

            int resto;

            cpf = cpf.Trim();

            cpf = cpf.Replace(".", "").Replace("-", "");

            if (cpf.Length != 11)

                return false;

            tempCpf = cpf.Substring(0, 9);

            soma = 0;

            for (int i = 0; i < 9; i++)

                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];

            resto = soma % 11;

            if (resto < 2)

                resto = 0;

            else

                resto = 11 - resto;

            digito = resto.ToString();

            tempCpf = tempCpf + digito;

            soma = 0;

            for (int i = 0; i < 10; i++)

                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];

            resto = soma % 11;

            if (resto < 2)

                resto = 0;

            else

                resto = 11 - resto;

            digito = digito + resto.ToString();

            return cpf.EndsWith(digito);

        }


        public static bool Valida_PIS(string pis)
        {
            int[] multiplicador = new int[10] { 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int soma;
            int resto;
            if (pis.Trim().Length != 11)
                return false;
            pis = pis.Trim();
            pis = pis.Replace("-", "").Replace(".", "").PadLeft(11, '0');

            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(pis[i].ToString()) * multiplicador[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            return pis.EndsWith(resto.ToString());
        }


    }
}
