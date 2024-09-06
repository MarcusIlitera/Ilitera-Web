using System;
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
using System.Collections;
using System.Collections.Generic;

using Entities;
using BLL;



namespace Ilitera.Net
{
    public partial class DadosEmpregado_Guia : System.Web.UI.Page
    {

        protected Ilitera.Common.Prestador prestador = new Ilitera.Common.Prestador();
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


            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

			PopulaTelaEmpregado();

            if (cmb_Clinicas.Items.Count > 0)
            {
                if (cmb_Clinicas.SelectedIndex < 0)
                {
                    cmb_Clinicas.SelectedIndex = 0;
                    lbl_Id_Clinica.Text = cmb_Clinicas.SelectedValue.ToString().Trim();
                }
                else
                {
                    if (cmb_Clinicas.SelectedValue.ToString().Trim() != lbl_Id_Clinica.Text )  // || rd_Mudanca.Checked == true)
                    {
                        PopularValueListClinicaClienteExameDicionario(cmb_Clinicas.SelectedValue);

                        if ( lst_Exames.Items.Count < 1)
                        {
                            PopularValueListClinicaClienteExameDicionario(cmb_Clinicas.SelectedValue);
                        }
                    }



                }
            }



            if (!IsPostBack)
            {

                if (Ilitera.Data.SQLServer.EntitySQLServer._LocalServer.ToString().ToUpper().IndexOf("EY") > 0)
                {
                    btnEmpFicha.Visible = true;
                }
                else
                {
                    btnEmpFicha.Visible = false;
                }

                if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().IndexOf("Prajna") > 0)
                {
                    btnemp.Enabled = false;
                    btnemp.Visible = false;
                }

                if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().Trim().ToUpper() == "OPSA" || Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("PRO") > 0 || Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("VIA") > 0 || Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("DAITI") > 0 || Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("SHO") > 0 || ( Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("SAFETY") > 0 && Ilitera.Data.SQLServer.EntitySQLServer._LocalServer.ToString().ToUpper().IndexOf("FOCUS") > 0 ))
                { 
                    txt_Demissao.Visible = true;
                    lbl_Demissao.Visible = true;
                }
                else
                {
                    txt_Demissao.Visible = false;
                    lbl_Demissao.Visible = false;
                }

                PopularFindClinica();

                Entities.Usuario user = (Entities.Usuario)Session["usuarioLogado"];

                txt_Obs.Text = "";

                //if (user.NomeUsuario.ToUpper().Trim() == "MAYARA" || user.NomeUsuario.ToUpper().Trim() == "ROSANA" || user.NomeUsuario.ToUpper().Trim() == "EDNA" || user.NomeUsuario.ToUpper().Trim() == "LMM" || user.NomeUsuario.ToUpper().Trim() == "WAGNER")
                //{
                    txt_Obs.Enabled = true;
                //}
                //else
                //{
                //    txt_Obs.Enabled = false;
                //}

                    


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
            //btnFichaCompleta.Attributes.Add("onClick", "addItem(centerWin('../DadosEmpresa/FichaCompleta.aspx?IdEmpregado=" + Request["IdEmpregado"] + "&IdEmpresa=" + Request["IdEmpresa"] + "&IdUsuario=" + Request["IdUsuario"] + "',560,320,\'FichaCompleta\'),\'Todos\'); Reload();");
                        
		}



        private void PopularFindClinica()
        {
            //StringBuilder strFind = new StringBuilder();
                           
            //strFind.Append(" IdClinica IN (SELECT IdClinica FROM ClinicaCliente WHERE IdCliente=" + Request["IdEmpresa"] + ") and IsInativo = 0 ORDER BY NomeAbreviado");

            Ilitera.Data.Clientes_Clinicas xClinicas = new Ilitera.Data.Clientes_Clinicas();

            DataSet dsClinica = xClinicas.Retornar_Clinicas(" IdJuridicaPapel=8 AND IsInativo = 0 and IdJuridica  IN (SELECT IdClinica FROM ClinicaCliente WHERE IdCliente=" + Request["IdEmpresa"] + ")  ", Request["IdEmpresa"].ToString());

            //DataSet dsClinica = new Clinica().Get(strFind.ToString());

            cmb_Clinicas.DataSource = dsClinica;
            //cmb_Clinicas.DataValueField = "IdClinica";
            //cmb_Clinicas.DataTextField = "NomeAbreviado";
            
            cmb_Clinicas.DataValueField = "Id";
            cmb_Clinicas.DataTextField = "Nome";

            cmb_Clinicas.DataBind();

            if ( cmb_Clinicas.Items.Count > 0 )
            {
                cmb_Clinicas.SelectedIndex = 0;
                lbl_Id_Clinica.Text = cmb_Clinicas.SelectedValue.ToString().Trim();
                PopularValueListClinicaClienteExameDicionario(cmb_Clinicas.SelectedValue);

                if (lst_Exames.Items.Count < 1)
                {
                    PopularValueListClinicaClienteExameDicionario(cmb_Clinicas.SelectedValue);
                }

            }

        }




        private void PopularGridClinicaCredenciada()
        {

            StringBuilder where1 = new StringBuilder();
            where1.Append("IdCliente=" + Request["IdEmpresa"]
                + " AND IdClinica IN (SELECT IdJuridica FROM Juridica WHERE IdJuridicaPapel= 8 "
                + " AND IdPessoa IN (SELECT IdPessoa FROM Pessoa WHERE IsInativo=0))");


            where1.Append(" ORDER BY (SELECT NomeAbreviado FROM Pessoa WHERE IdPessoa=IdClinica)");

            //DataSet ds1 = new ClinicaCliente().Get(where1.ToString());
            DataSet ds1 = new Clinica().Get(where1.ToString());

            cmb_Clinicas.DataSource = ds1;
            cmb_Clinicas.DataValueField = "IdClinica";
            cmb_Clinicas.DataTextField = "NomeAbreviado";
            cmb_Clinicas.DataBind();
        }



        protected void cmb_Clinicas_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb_Clinicas.SelectedIndex >= 0)
            {
                PopularValueListClinicaClienteExameDicionario( cmb_Clinicas.SelectedValue );
                lbl_Id_Clinica.Text = cmb_Clinicas.SelectedValue.ToString().Trim();

                if (lst_Exames.Items.Count < 1)
                {
                    PopularValueListClinicaClienteExameDicionario(cmb_Clinicas.SelectedValue);
                }

            }
            else
            {
                lst_Exames.Items.Clear();
                lst_IdExames.Items.Clear();
                lbl_Id_Clinica.Text = "0";
            }
        }


        private void PopularValueListClinicaClienteExameDicionario(string xValue)
        {

            if (xValue.Trim() == "") return;

            DataSet dsExames = new ExameDicionario().GetIdNome("Nome", " IdExameDicionario IN (SELECT IdExameDicionario FROM ClinicaExameDicionario WHERE IdClinica IN (SELECT IdClinica FROM ClinicaCliente WHERE IdCliente=" + Request["IdEmpresa"] + " and IdClinica = " + xValue + " ))");

            DataSet ds = new DataSet();
            bool xExamesClinicasCompleto = true;
            string xExamesFaltantes = "";
            string rExamesASO = "";


            if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().IndexOf("Prajna") > 0)
            {
                Cliente zCliente;
                zCliente = new Cliente(System.Convert.ToInt32(Session["Empresa"].ToString()));

                zCliente.IdGrupoEmpresa.Find();

                if (zCliente.IdGrupoEmpresa.Descricao.ToUpper().IndexOf("BRASPRESS") >= 0)
                {
                    ds = new ClinicaExameDicionario().Get("IdClinica IN (SELECT IdClinica FROM ClinicaCliente WHERE IdCliente=" + Request["IdEmpresa"] + " " + " and IdClinica = " + xValue + " ) " + " AND IDCLINICAEXAMEDICIONARIO IN " +
                         "( " +
                          "   SELECT IdClinicaExameDicionario " +
                          "   FROM ClinicaClienteExameDicionario_Braspress  " +
                          "    WHERE IdClinicaCliente IN ( " +
                          "      SELECT IdClinicaCliente FROM ClinicaCliente " +
                          "      WHERE IdCliente=" + Request["IdEmpresa"] + " " + " and IdClinica = " + xValue + " and IsAutorizado = 1 ) ) ");

                }
                else
                {

                    ds = new ClinicaExameDicionario().Get("IdClinica IN (SELECT IdClinica FROM ClinicaCliente WHERE IdCliente=" + Request["IdEmpresa"] + " " + " and IdClinica = " + xValue + " ) " + " AND IDCLINICAEXAMEDICIONARIO IN " +
                         "( " +
                          "   SELECT IdClinicaExameDicionario " +
                          "   FROM ClinicaClienteExameDicionario  " +
                          "    WHERE IdClinicaCliente IN ( " +
                          "      SELECT IdClinicaCliente FROM ClinicaCliente " +
                          "      WHERE IdCliente=" + Request["IdEmpresa"] + " " + " and IdClinica = " + xValue + " and IsAutorizado = 1 ) ) ");
                }
            }
            else
            {

                ds = new ClinicaExameDicionario().Get("IdClinica IN (SELECT IdClinica FROM ClinicaCliente WHERE IdCliente=" + Request["IdEmpresa"] + " " + " and IdClinica = " + xValue + " ) " + " AND IDCLINICAEXAMEDICIONARIO IN " +
                 "( " +
                 "   SELECT IdClinicaExameDicionario " +
                 "   FROM ClinicaClienteExameDicionario  " +
                 "    WHERE IdClinicaCliente IN ( " +
                 "      SELECT IdClinicaCliente FROM ClinicaCliente " +
                 "      WHERE IdCliente=" + Request["IdEmpresa"] + " " + " and IdClinica = " + xValue + " and IsAutorizado = 1 ) ) ");
            }

            lst_Exames.Enabled = true;
            lst_Exames.Items.Clear();
            lst_IdExames.Items.Clear();

            ////exibir endereco e telefone da clinica na tela
            ////carregar dados da clinica
            Clinica xClinica = new Clinica(System.Convert.ToInt32( xValue));

            lbl_Fone_Clinica.Text = "";

            lbl_Fone_Clinica.Text =  xClinica.GetContatoTelefonico().DDD + " " + xClinica.GetContatoTelefonico().Numero;
            if ( lbl_Fone_Clinica.Text.Trim() != "" )
                lbl_Fone_Clinica.Text = lbl_Fone_Clinica.Text + "    ";

            lbl_Fone_Clinica.Text = lbl_Fone_Clinica.Text + xClinica.GetContatoTelefonico2().DDD + " " + xClinica.GetContatoTelefonico2().Numero;
            if (lbl_Fone_Clinica.Text.Trim() != "")
                lbl_Fone_Clinica.Text = lbl_Fone_Clinica.Text + "    ";

            lbl_Fone_Clinica.Text = lbl_Fone_Clinica.Text + xClinica.GetContatoTelefonico3().DDD + " " + xClinica.GetContatoTelefonico3().Numero;
            if (lbl_Fone_Clinica.Text.Trim() != "")
                lbl_Fone_Clinica.Text = lbl_Fone_Clinica.Text + "    ";

            lbl_Fone_Clinica.Text = lbl_Fone_Clinica.Text + xClinica.GetContatoTelefonico4().DDD + " " + xClinica.GetContatoTelefonico4().Numero;
            if (lbl_Fone_Clinica.Text.Trim() != "")
                lbl_Fone_Clinica.Text = lbl_Fone_Clinica.Text + "    ";

            lbl_Fone_Clinica.Text = lbl_Fone_Clinica.Text + xClinica.GetContatoTelefonico5().DDD + " " + xClinica.GetContatoTelefonico5().Numero;
            if (lbl_Fone_Clinica.Text.Trim() != "")
                lbl_Fone_Clinica.Text = lbl_Fone_Clinica.Text + "    ";

            lbl_Fone_Clinica.Text = "Tel.: " + lbl_Fone_Clinica.Text;


            lbl_email.Text = xClinica.Email.Trim();


            //ativar envio de e-mail apenas para prajna e Ilitera
            if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().IndexOf("Prajna") > 0 || Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().Trim() == "OPSA" || Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("UNO") > 0 ||   Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("SDTOURIN") > 0 || Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("VIA") > 0 || Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("GLOBAL") > 0 || Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("SHO") > 0 || Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("PRO") > 0)  
            {

                if (lbl_email.Text.Trim() == "")
                {
                    chk_eMail.Checked = false;
                    chk_eMail.Enabled = false;
                }
                else
                {
                    chk_eMail.Enabled = true;
                    if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().IndexOf("Prajna") > 0 )
                    {
                        chk_eMail.Enabled = true;
                        chk_eMail.Checked = false;
                    }
                    else if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("GLOBAL") > 0)
                    {
                        chk_eMail.Checked = true;
                        chk_eMail.Enabled = false;
                    }
                    else if ( Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().Trim() == "OPSA" || Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("UNO") > 0 || Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("SDTOURIN") > 0 || Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("VIA") > 0 || Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("GLOBAL") > 0 || Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("SHO") > 0 || Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("PRO") > 0)
                    {
                        chk_eMail.Checked = true;
                    }
                    else
                    {
                        chk_eMail.Checked = false;
                    }
                }
            }
            else if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().IndexOf("Daiti") > 0)
            {
                if (lbl_email.Text.Trim() == "")
                {
                    chk_eMail.Checked = false;
                    chk_eMail.Enabled = false;
                }
                else
                {
                    chk_eMail.Enabled = true;
                    chk_eMail.Checked = true;
                }
            }
            else if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("SAFETY") > 0 ) // || Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("GLOBAL") > 0)
            {
                if (lbl_email.Text.Trim() == "")
                {
                    chk_eMail.Checked = false;
                    chk_eMail.Enabled = false;
                }
                else
                {
                    chk_eMail.Checked = true;
                    chk_eMail.Enabled = true;
                }
            }
            else
            {
                chk_eMail.Checked = false;
                chk_eMail.Enabled = false;
            }





            lbl_Horario.Text = "";
            if (xClinica.HorarioAtendimento.ToString().Trim() != "")
            {
                lbl_Horario.Text = "Horário Atendimento :  " + xClinica.HorarioAtendimento.ToString(); 
            }

            lbl_End_Clinica.Text = "";

            lbl_End_Clinica.Text = xClinica.GetEndereco().IdTipoLogradouro + " " + xClinica.GetEndereco().Logradouro + " " + xClinica.GetEndereco().Numero.ToString() + " " + xClinica.GetEndereco().Complemento;
            lbl_End_Clinica.Text = lbl_End_Clinica.Text + "  -  " +  xClinica.GetEndereco().IdMunicipio + " / " +  xClinica.GetEndereco().Uf;

            lbl_End_Clinica.Text = "End.: " + lbl_End_Clinica.Text;


            cliente.Find(System.Convert.ToInt32(Request["IdEmpresa"].ToString()));



            //pegar exames de PCMSO do funcionário
            empregado.Find(" tblEMPREGADO.nID_EMPREGADO = " + Request["IdEmpregado"].ToString());

            Clinico exame = new Clinico();
            exame.IdEmpregado = empregado;

            //exame.IdEmpregadoFuncao = EmpregadoFuncao.GetEmpregadoFuncao(empregado);
            exame.IdEmpregadoFuncao = EmpregadoFuncao.GetEmpregadoFuncao(empregado, cliente.Id);

            exame.IdPcmso = exame.IdEmpregadoFuncao.nID_EMPR.GetUltimoPcmso();
            
            exame.UsuarioId = System.Convert.ToInt32(Request["IdUsuario"].ToString());

            Pcmso pcmso = new Pcmso();
            pcmso = exame.IdPcmso;

            bool zClinico = false;


            if (pcmso.IdLaudoTecnico != null)
            {
                List<Ghe> ghes = new Ghe().Find<Ghe>("nID_LAUD_TEC=" + pcmso.IdLaudoTecnico.Id);

                Ghe ghe;

                if (ghes == null || ghes.Count == 0)
                    ghe = exame.IdEmpregadoFuncao.GetGheEmpregado(pcmso.IdLaudoTecnico);
                else
                {
                    int IdGhe = exame.IdEmpregadoFuncao.GetIdGheEmpregado(pcmso.IdLaudoTecnico);

                    ghe = ghes.Find(delegate(Ghe g) { return g.Id == IdGhe; });
                }



                if (ghe == null)
                {
                    MsgBox1.Show("Ilitera.Net", "Colaborador não está alocado em GHE,  não é possível criar Guia de Encaminhamento/ASO.", null,
                                          new EO.Web.MsgBoxButton("OK"));
                    btnemp.Visible = false;
                    btnEmp2Via.Visible = false;
                    btnEmpASO.Visible = false;
                    return;
                }



                bool zDesconsiderar = false;
                string xDataBranco = "";
                Int16 zDias_Desconsiderar = 0;

                string sExamesOcupacionais = "";
                //cliente.Find(System.Convert.ToInt32(Request["IdEmpresa"].ToString()));



                PcmsoGhe pcmsoGhe = new PcmsoGhe();
                pcmsoGhe.Find("IdPcmso=" + pcmso.Id + " AND IdGHE=" + ghe.Id);

                //configuração de PCMSO para cada GHE, para desconsiderar dias do vencimento
                if (rd_Periodico.Checked == true || rd_Demissional.Checked == true)
                {
                    if (pcmsoGhe.Desconsiderar_Dias_ASO > 0)
                    {
                        zDias_Desconsiderar = pcmsoGhe.Desconsiderar_Dias_ASO;
                        zDesconsiderar = true;
                        xDataBranco = "  /  /    ";
                    }
                }


                if (zDias_Desconsiderar == 0)
                {

                    //Wagner 04/07/2018 - ver exames complementares que estão com data ( ver se opção exibir data complementares está ativa )
                    // esses exames com datas não precisam aparecer na guia, pois não precisam ser solicitados.

                    //se for para desconsiderar data de complementares a partir de certa data, usar o esquema que já existe com xDataBranco
                    if (xDataBranco == "")
                    {
                        if (cliente.Ativar_DesconsiderarCompl == true)
                        {
                            if (cliente.Dias_Desconsiderar > 0)
                            {
                                zDesconsiderar = true;
                                zDias_Desconsiderar = cliente.Dias_Desconsiderar;
                            }
                        }
                    }
                    else
                    {
                        zDesconsiderar = false;
                    }
                }




                if (cliente.Bloquear_Data_Demissionais == true && rd_Demissional.Checked == true)
                {
                    xDataBranco = "31/12/2050";
                    zDesconsiderar = false;
                }


                //Clinico clinico = new Clinico();

                //clinico.IdPcmso = pcmso;
                //clinico.IdEmpregadoFuncao = EmpregadoFuncao.GetEmpregadoFuncao(pcmso.IdLaudoTecnico, empregado);


                //clinico.UsuarioId = System.Convert.ToInt32(Request["IdUsuario"].ToString());
                ExameDicionario rDicionario = new ExameDicionario();

                if (rd_Admissao.Checked == true) rDicionario.Find(1);
                else if (rd_Demissional.Checked == true) rDicionario.Find(2);
                else if (rd_Mudanca.Checked == true) rDicionario.Find(3);
                else if (rd_Retorno.Checked == true) rDicionario.Find(5);
                else rDicionario.Find(4);


                exame.IdExameDicionario = rDicionario;

                if ( txt_Data.Text.Trim() != "")
                {
                    if (Validar_Data(txt_Data.Text) == true)
                    {
                        System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");
                        exame.DataExame = System.Convert.ToDateTime(txt_Data.Text, ptBr);
                    }
                }



                bool xExibirDatas = cliente.Exibir_Datas_Exames_ASO;


                exame.IdExameDicionario.Find();

                //Global quer que desconsiderar seja apenas para periódicos/mud.função e retorno trab. - 13/10/2023
                if (Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper().IndexOf("GLOBAL") > 0)
                {
                    if (exame.IdExameDicionario.Nome.ToString().Substring(0, 1) == "A" || exame.IdExameDicionario.Nome.ToString().Substring(0, 1) == "D")
                    {
                        zDesconsiderar = false;
                    }
                }




                //mudança de função e demissional da Global para alguns grupos não exibir data complementares
                //if (clinico.IdExameDicionario.Id == 3 || (clinico.IdExameDicionario.Id == 2))
                //eles também querem que Afonso França, Mudança de função e retorno ao trabalho com complementares sempre sem data
                //if (exame.IdExameDicionario.Nome.ToString().Substring(0, 1) == "D" || exame.IdExameDicionario.Nome.ToString().Substring(0, 1) == "M" || exame.IdExameDicionario.Nome.ToString().Substring(0, 1) == "R")
                //{
                //    if (Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper().IndexOf("GLOBAL") > 0)
                //    {
                //        exame.IdEmpregado.Find();
                //        exame.IdEmpregado.nID_EMPR.Find();
                //        exame.IdEmpregado.nID_EMPR.IdGrupoEmpresa.Find();
                //        Int32 zIdGrupo = exame.IdEmpregado.nID_EMPR.IdGrupoEmpresa.Id;

                //        if (zIdGrupo == -359564237 || zIdGrupo == 518994214 || zIdGrupo == 1635302153 || zIdGrupo == -987166118 || zIdGrupo == -1551657879)
                //        {

                //            xExibirDatas = false;

                //        }
                //    }
                //}




                //pegar exames do ASO
                string sExamesASO = "";
                string sExamesASO_Aptidao = "";


                if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().IndexOf("Daiti") > 0)
                {
                    if  ( rd_Demissional.Checked == true )
                    {
                        if (zDesconsiderar == false)
                            sExamesASO = exame.GetPlanejamentoExamesAso_Formatado_Asterisco(ghe, true, false, xDataBranco, zDesconsiderar);
                        else
                            sExamesASO = exame.GetPlanejamentoExamesAso_Formatado_Asterisco_Desconsiderar(ghe, true, false, exame, zDias_Desconsiderar);
                    }
                    else
                    {
                        if (zDesconsiderar == false)
                            sExamesASO = exame.GetPlanejamentoExamesAso_Formatado_Asterisco(ghe, true, cliente.Exibir_Datas_Exames_ASO, xDataBranco, zDesconsiderar);
                        else
                            sExamesASO = exame.GetPlanejamentoExamesAso_Formatado_Asterisco_Desconsiderar(ghe, true, cliente.Exibir_Datas_Exames_ASO, exame, zDias_Desconsiderar);
                    }
                }
                else if ( rd_Mudanca.Checked == true )
                {
                    // procurar ghe_ant primeiro, na mesma classif.funcional
                    // se nao encontrar, procurar classif.funcional anterior e ghe

                    
                    if (cliente.GHEAnterior_MudancaFuncao == true)
                    {
                        // procurar ghe_ant primeiro, na mesma classif.funcional
                        // se nao encontrar, procurar classif.funcional anterior e ghe

                        Ilitera.Data.PPRA_EPI xGHE = new Ilitera.Data.PPRA_EPI();

                        DataSet xdS = xGHE.Trazer_Laudos_GHEs_Colaborador(empregado.Id);

                        if (xdS.Tables[0].Rows.Count < 2)
                        {
                            rd_Outro.Checked = true;
                            rd_Mudanca.Checked = false;
                            MsgBox1.Show("Ilitera.Net", "O empregado " + empregado.tNO_EMPG
                                   + " está associado a apenas um GHE/Classif.Funcional, logo, não é possível gerar o ASO de Mudança de Risco. ", null,
                                          new EO.Web.MsgBoxButton("OK"));
                            return;
                        }


                        int znAux = 0;
                        Int32 zGHE_Atual = 0;
                        Int32 zGHE_Ant = 0;


                        foreach (DataRow row in xdS.Tables[0].Rows)
                        {
                            znAux++;

                            if (znAux == 1) zGHE_Atual = System.Convert.ToInt32(row["nId_Func"].ToString());
                            else if (znAux == 2) zGHE_Ant = System.Convert.ToInt32(row["nId_Func"].ToString());
                            else break;

                        }


                        //ghe_ant
                        //ghe
                        Ghe zGhe1 = new Ghe();
                        zGhe1.Find(zGHE_Atual);
                        Ghe zGhe2 = new Ghe();
                        zGhe2.Find(zGHE_Ant);

                        if (zDesconsiderar == false)
                            sExamesASO = exame.GetPlanejamentoExamesAso_Formatado_Mudanca_Funcao(zGhe1, zGhe2, false, xExibirDatas, xDataBranco, zDesconsiderar);
                        else
                            sExamesASO = exame.GetPlanejamentoExamesAso_Formatado_Mudanca_Funcao_Desconsiderar(zGhe1, zGhe2, false, xExibirDatas, exame, zDias_Desconsiderar);
                    }
                    else
                    {
                        if (zDesconsiderar == false)
                            sExamesASO = exame.GetPlanejamentoExamesAso_Formatado(ghe, false, xExibirDatas, xDataBranco, zDesconsiderar);
                        else
                            sExamesASO = exame.GetPlanejamentoExamesAso_Formatado_Desconsiderar(ghe, false, xExibirDatas, exame, zDias_Desconsiderar);
                    }


                }
                else
                {

                    if (rd_Demissional.Checked == true )
                    {
                        if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("GLOBAL") > 0)
                        {
                            if (zDesconsiderar == false)
                                sExamesASO = exame.GetPlanejamentoExamesAso_Formatado_Global(ghe, false, xExibirDatas, xDataBranco, zDesconsiderar);
                            else
                                sExamesASO = exame.GetPlanejamentoExamesAso_Formatado_Desconsiderar_Global(ghe, false, xExibirDatas, exame, zDias_Desconsiderar);
                        }
                        else
                        {
                            if (zDesconsiderar == false)
                                sExamesASO = exame.GetPlanejamentoExamesAso_Formatado(ghe, false, xExibirDatas, xDataBranco, zDesconsiderar);
                            else
                                sExamesASO = exame.GetPlanejamentoExamesAso_Formatado_Desconsiderar(ghe, false, xExibirDatas, exame, zDias_Desconsiderar);
                        }
                    }
                    else
                    {
                        if (zDesconsiderar == false)
                            sExamesASO = exame.GetPlanejamentoExamesAso_Formatado(ghe, false, xExibirDatas, xDataBranco, zDesconsiderar);
                        else
                            sExamesASO = exame.GetPlanejamentoExamesAso_Formatado_Desconsiderar(ghe, false, xExibirDatas, exame, zDias_Desconsiderar);
                    }
                }


            







            //pegar exames para guia

            if ( rd_Mudanca.Checked==true)
                {

                    if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().IndexOf("Daiti") > 0)
                    {

                        sExamesOcupacionais = exame.GetPlanejamentoExamesAso_Guia(ghe, true, "M");

                    }
                    else
                    {
                        cliente.Find(System.Convert.ToInt32(Request["IdEmpresa"].ToString()));

                        if (cliente.GHEAnterior_MudancaFuncao == true)
                        {
                            // procurar ghe_ant primeiro, na mesma classif.funcional
                            // se nao encontrar, procurar classif.funcional anterior e ghe

                            Ilitera.Data.PPRA_EPI xGHE = new Ilitera.Data.PPRA_EPI();

                            DataSet xdS = xGHE.Trazer_Laudos_GHEs_Colaborador(empregado.Id);

                            if (xdS.Tables[0].Rows.Count < 2)
                            {
                                rd_Outro.Checked = true;
                                rd_Mudanca.Checked = false;
                                MsgBox1.Show("Ilitera.Net", "O empregado " + empregado.tNO_EMPG
                                       + " está associado a apenas um GHE/Classif.Funcional, logo, não é possível gerar o ASO de Mudança de Risco. ", null,
                                              new EO.Web.MsgBoxButton("OK"));
                                return;
                            }


                            int znAux = 0;
                            Int32 zGHE_Atual = 0;
                            Int32 zGHE_Ant = 0;


                            foreach (DataRow row in xdS.Tables[0].Rows)
                            {
                                znAux++;

                                if (znAux == 1) zGHE_Atual = System.Convert.ToInt32(row["nId_Func"].ToString());
                                else if (znAux == 2) zGHE_Ant = System.Convert.ToInt32(row["nId_Func"].ToString());
                                else break;

                            }


                            //ghe_ant
                            //ghe
                            Ghe zGhe1 = new Ghe();
                            zGhe1.Find(zGHE_Atual);
                            Ghe zGhe2 = new Ghe();
                            zGhe2.Find(zGHE_Ant);


                            sExamesOcupacionais = exame.GetPlanejamentoExamesAso_Formatado_Mudanca_Funcao_Guia(zGhe1, zGhe2, true);
                        }
                        else
                        {
                            sExamesOcupacionais = exame.GetPlanejamentoExamesAso_Guia(ghe, true, "M");
                        }
                    }

                }                    
                else if (rd_Admissao.Checked == true)
                {
                    sExamesOcupacionais = exame.GetPlanejamentoExamesAso_Guia(ghe, true, "A");                    
                }
                else if ( rd_Demissional.Checked == true)
                {
                    sExamesOcupacionais = exame.GetPlanejamentoExamesAso_Guia(ghe, true, "D");                    
                }
                else if (rd_Retorno.Checked == true)
                {
                    sExamesOcupacionais = exame.GetPlanejamentoExamesAso_Guia(ghe, true, "R");                    
                }
                else if (rd_Periodico.Checked == true)
                {
                    sExamesOcupacionais = exame.GetPlanejamentoExamesAso_Guia(ghe, true, "P");                    
                }                    
                else
                {
                    sExamesOcupacionais = exame.GetPlanejamentoExamesAso_Guia(ghe, true);                    
                }



                //checar se têm exames de aptidão
                string sExamesOcupacionais_Aptidao = "";

                Empregado_Aptidao xAptidao = new Empregado_Aptidao();                
                xAptidao.Find(" nId_Empregado = " + empregado.Id.ToString());


                GHE_Aptidao zAptidao = new GHE_Aptidao();
                zAptidao.Find("nId_Func = " + ghe.Id.ToString());

                if (xAptidao.Id != 0 || zAptidao.Id != 0)
                {
                    if ((xAptidao.apt_Alimento == true || xAptidao.apt_Aquaviario == true || xAptidao.apt_Eletricidade == true || xAptidao.apt_Espaco_Confinado == true ||
                              xAptidao.apt_Submerso == true || xAptidao.apt_Trabalho_Altura == true || xAptidao.apt_Transporte == true || xAptidao.apt_Brigadista == true || xAptidao.apt_Socorrista == true || xAptidao.apt_PPR == true ) ||
                              (zAptidao.apt_Alimento == true || zAptidao.apt_Aquaviario == true || zAptidao.apt_Eletricidade == true || zAptidao.apt_Espaco_Confinado == true ||
                             zAptidao.apt_Submerso == true || zAptidao.apt_Trabalho_Altura == true || zAptidao.apt_Transporte == true || zAptidao.apt_Brigadista == true || zAptidao.apt_Socorrista == true || zAptidao.apt_PPR == true ))
                    {

                        Empregado_Aptidao nAptidao = new Empregado_Aptidao();


                        nAptidao.nId_Empregado = empregado.Id;

                        //juntando aptidao do empregado com do PPRA-GHE
                        if (xAptidao.Id != 0 && zAptidao.Id != 0)
                        {
                            nAptidao.apt_Alimento = xAptidao.apt_Alimento || zAptidao.apt_Alimento;
                            nAptidao.apt_Aquaviario = xAptidao.apt_Aquaviario || zAptidao.apt_Aquaviario;
                            nAptidao.apt_Brigadista = xAptidao.apt_Brigadista || zAptidao.apt_Brigadista;
                            nAptidao.apt_Eletricidade = xAptidao.apt_Eletricidade || zAptidao.apt_Eletricidade;
                            nAptidao.apt_Espaco_Confinado = xAptidao.apt_Espaco_Confinado || zAptidao.apt_Espaco_Confinado;
                            nAptidao.apt_Socorrista = xAptidao.apt_Socorrista || zAptidao.apt_Socorrista;
                            nAptidao.apt_Submerso = xAptidao.apt_Submerso || zAptidao.apt_Submerso;
                            nAptidao.apt_Trabalho_Altura = xAptidao.apt_Trabalho_Altura || zAptidao.apt_Trabalho_Altura;
                            nAptidao.apt_Transporte = xAptidao.apt_Transporte || zAptidao.apt_Transporte;
                            nAptidao.apt_PPR = xAptidao.apt_PPR || zAptidao.apt_PPR;
                        }
                        else if (xAptidao.Id != 0)
                        {
                            nAptidao.apt_Alimento = xAptidao.apt_Alimento;
                            nAptidao.apt_Aquaviario = xAptidao.apt_Aquaviario;
                            nAptidao.apt_Brigadista = xAptidao.apt_Brigadista;
                            nAptidao.apt_Eletricidade = xAptidao.apt_Eletricidade;
                            nAptidao.apt_Espaco_Confinado = xAptidao.apt_Espaco_Confinado;
                            nAptidao.apt_Socorrista = xAptidao.apt_Socorrista;
                            nAptidao.apt_Submerso = xAptidao.apt_Submerso;
                            nAptidao.apt_Trabalho_Altura = xAptidao.apt_Trabalho_Altura;
                            nAptidao.apt_Transporte = xAptidao.apt_Transporte;
                            nAptidao.apt_PPR = xAptidao.apt_PPR;
                        }
                        else if (zAptidao.Id != 0)
                        {
                            nAptidao.apt_Alimento = zAptidao.apt_Alimento;
                            nAptidao.apt_Aquaviario = zAptidao.apt_Aquaviario;
                            nAptidao.apt_Brigadista = zAptidao.apt_Brigadista;
                            nAptidao.apt_Eletricidade = zAptidao.apt_Eletricidade;
                            nAptidao.apt_Espaco_Confinado = zAptidao.apt_Espaco_Confinado;
                            nAptidao.apt_Socorrista = zAptidao.apt_Socorrista;
                            nAptidao.apt_Submerso = zAptidao.apt_Submerso;
                            nAptidao.apt_Trabalho_Altura = zAptidao.apt_Trabalho_Altura;
                            nAptidao.apt_Transporte = zAptidao.apt_Transporte;
                            nAptidao.apt_PPR = zAptidao.apt_PPR;
                        }

                        Cliente xCliente = new Cliente();
                        xCliente.Find(pcmso.IdCliente.Id);

                        ExameDicionario rDic  = new ExameDicionario();

                        if (rd_Admissao.Checked == true)
                        {
                            rDic.Find(1);
                        }
                        else if (rd_Demissional.Checked == true)
                        {
                            rDic.Find(2);
                        }
                        else if (rd_Retorno.Checked == true)
                        {
                            rDic.Find(5);
                        }
                        else if (rd_Periodico.Checked == true)
                        {
                            rDic.Find(4);
                        }
                        else
                        {
                            rDic.Find(3);
                        }

                        exame.IdExameDicionario = rDic;
                        exame.IdEmpregado = empregado;
                        

                        sExamesOcupacionais_Aptidao = exame.GetPlanejamentoExamesAso_Guia_Aptidao(nAptidao, xCliente.Exibir_Datas_Exames_ASO, "F", sExamesOcupacionais);


                        if (zDesconsiderar == false)
                        {
                            sExamesASO_Aptidao = exame.GetPlanejamentoExamesAso_Formatado_Aptidao(nAptidao, xCliente.Exibir_Datas_Exames_ASO, xDataBranco, sExamesOcupacionais, zDesconsiderar, exame);
                        }
                        else
                        {
                            sExamesASO_Aptidao = exame.GetPlanejamentoExamesAso_Formatado_Aptidao_Desconsiderar(nAptidao, xCliente.Exibir_Datas_Exames_ASO,  sExamesOcupacionais, exame, zDias_Desconsiderar);
                        }

                    }
                }

                if (sExamesOcupacionais.Trim() != "")
                    sExamesOcupacionais = sExamesOcupacionais + sExamesOcupacionais_Aptidao;
                else
                    sExamesOcupacionais = sExamesOcupacionais_Aptidao;
                
                txt_Exames.Text = sExamesOcupacionais;



                if (sExamesASO.Trim() != "")
                    sExamesASO = sExamesASO + sExamesASO_Aptidao;
                else
                    sExamesASO = sExamesASO_Aptidao;



                bool zSelecao = true;


                if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().IndexOf("Daiti") > 0 && rd_Mudanca.Checked==false) zSelecao = false;




                try
                {
                    //retirar data de audiometria
                    if (rd_Demissional.Checked == true)
                    {
                        Cliente xCliente = new Cliente();
                        xCliente.Find(pcmso.IdCliente.Id);

                        if (xCliente.Demissional_Audiometria == true)
                        {
                            
                            if (sExamesOcupacionais.IndexOf("Audiometria\n") > 0)
                            {
                                //buscar ultima audiometria com resultado
                                ArrayList nExame = new ExameBase().Find(" IDEMPREGADO = " + Request["IdEmpregado"].ToString() + " and IdExameDicionario in ( 6 ) and IndResultado  in ( 1, 2 ) order by DataExame desc ");

                                DateTime zDataAud = new DateTime(2000,1,1);

                                foreach (ExameBase rexame2 in nExame)
                                {
                                    zDataAud = rexame2.DataExame;
                                    break;
                                }



                                if (zDataAud != new DateTime(2000, 1, 1))
                                {
                                    //checar se último demissional foi a mais de 120 dias
                                    System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");                                    
                                    DateTime zDataExame = System.Convert.ToDateTime(txt_Data.Text, ptBr);

                                    int xDias = (zDataExame - zDataAud).Days;

                                    //testar
                                    if (xDias <= 120)
                                    {
                                        int xPosit2 = sExamesOcupacionais.IndexOf("Audiometria\n");
                                        sExamesOcupacionais = sExamesOcupacionais.Substring(0, xPosit2 - 2) + sExamesOcupacionais.Substring(xPosit2 + 12);
                                    }
                                    else
                                    {
                                        int zPosit = sExamesASO.IndexOf("Audiometria  ");
                                        int zPosit2 = sExamesASO.IndexOf("/", zPosit + 10);
                                        if (zPosit2 > 0)
                                        {
                                            sExamesASO = sExamesASO.Substring(0, zPosit2 - 2) + "          " + sExamesASO.Substring(zPosit2 + 8);
                                        }
                                    }
                                    
                                }


                            }
                        }
                    }


                }
                catch (Exception ex)
                {

                }
                finally
                {

                }



                ////Prajna quer que apareça clinico se ele realmente estiver no planejamento
                //if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().IndexOf("Prajna") < 0)
                //{
                //    lst_Exames.Items.Add("Exame Clínico");
                //    lst_Exames.Items[0].Selected = zSelecao;
                //}





                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    int xPosit = 0;
                    DataRow[] rows = dsExames.Tables[0].Select("Id=" + Convert.ToInt32(row["IdExameDicionario"]));

                    //valueListClinicaClienteExameDicionario.ValueListItems.Add(Convert.ToInt32(row[0]), Convert.ToString(rows[0]["Nome"]));
                    if (rows.Count() > 0)
                    {

                        // retirar admissional, periodico, retorno ao trab., complement. desta lista
                        if (rows[0]["Nome"].ToString().Trim().ToUpper() != "RETORNO AO TRABALHO" &&
                            rows[0]["Nome"].ToString().Trim().ToUpper() != "DEMISSIONAL" &&
                            rows[0]["Nome"].ToString().Trim().ToUpper() != "ADMISSIONAL" &&
                            rows[0]["Nome"].ToString().Trim().ToUpper() != "MUDANÇA DE FUNÇÃO" &&
                            rows[0]["Nome"].ToString().Trim().ToUpper() != "PERIÓDICO")
                        {

                            //  lst_Exames.Items.Add(Convert.ToString(rows[0]["Nome"]));
                            zClinico = true;

                            if (sExamesOcupacionais.ToUpper().IndexOf(Convert.ToString(rows[0]["Nome"]).ToUpper()) > 0)
                            {
                                //poderia procurar o exame, e ver se foi feito e está com resultado.  Se sim, não selecionar
                                //precisaria usar a data de planejamento deste exame
                                
                                if (Convert.ToString(rows[0]["Nome"]).ToUpper() == "AUDIOMETRIA" && Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().IndexOf("Prajna") > 0)
                                {
                                    xPosit = sExamesASO.ToUpper().IndexOf("AUDIOMETRIA  ");  //para evitar audiometria tonal e audiometria vocal
                                }
                                else
                                {
                                    xPosit = sExamesASO.ToUpper().IndexOf(Convert.ToString(rows[0]["Nome"]).ToUpper());
                                }

                                if (xPosit > 0)
                                {
                                    if (sExamesASO.Substring(xPosit + 38, 12).Replace("/", " ").Trim() == "")
                                        xPosit = 0;

                                }

                                if (xPosit == 0)
                                {
                                    lst_Exames.Items.Add(Convert.ToString(rows[0]["Nome"]));
                                    lst_Exames.Items[lst_Exames.Items.Count - 1].Selected = zSelecao;

                                    lst_IdExames.Items.Add(rows[0][0].ToString());

                                }
                            }


                        }
                        else if (rows[0]["Nome"].ToString().Trim().ToUpper() == "PERIÓDICO")
                        {
                            zClinico = true;
                        }

                    }

              

                }

                rExamesASO = sExamesOcupacionais.ToUpper().Trim();


                //Prajna quer que apareça clinico se ele realmente estiver no planejamento
                if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().IndexOf("Prajna") < 0)
                {
                    bool jClinico = false;

                    for ( int jCont=0; jCont<lst_Exames.Items.Count; jCont++)
                    {
                        if (lst_Exames.Items[jCont].ToString().ToUpper().IndexOf("EXAME CL") >= 0 || lst_Exames.Items[jCont].ToString().ToUpper().IndexOf("AVALIAÇÃO CL") >= 0 || lst_Exames.Items[jCont].ToString().ToUpper().IndexOf("AVALIACAO CL") >= 0)
                        {
                            jClinico = true;
                            break;
                        }
                    }

                    if (jClinico == false)
                    {
                        lst_Exames.Items.Add("Exame Clínico");
                        lst_Exames.Items[lst_Exames.Items.Count - 1].Selected = zSelecao;
                        //lst_Exames.Items[0].Selected = zSelecao;
                    }
                }


            }


            //checar se todos exames do ASO constam na clínica
            //olhar linha a linha do txt_Exames e procurar em ds
            //xExamesClinicasCompleto = false;

            //if (xExamesClinicasCompleto == false)
            //{
            //    xExamesFaltantes = xExamesFaltantes + Convert.ToString(rows[0]["Nome"]).ToUpper().Trim() + " / ";
            //}

            //for (int rLinha = 0; rLinha < lst_Exames.Items.Count; rLinha++)
            //{
            //    xExamesClinicasCompleto = false;

            //    foreach (DataRow row in ds.Tables[0].Rows)
            //    {
            //        int xPosit = 0;
            //        DataRow[] rows = dsExames.Tables[0].Select("Id=" + Convert.ToInt32(row["IdExameDicionario"]));

            //        //valueListClinicaClienteExameDicionario.ValueListItems.Add(Convert.ToInt32(row[0]), Convert.ToString(rows[0]["Nome"]));
            //        if (rows.Count() > 0)
            //        {
            //            if ( lst_Exames.Items[rLinha].ToString().ToUpper().Trim()==Convert.ToString(rows[0]["Nome"]).ToUpper().Trim())
            //            {
            //                xExamesClinicasCompleto = true;
            //            }

            //        }
            //    }

            //    if (xExamesClinicasCompleto == false)
            //    {
            //        xExamesFaltantes = xExamesFaltantes + lst_Exames.Items[rLinha].ToString().ToUpper().Trim()  + " / ";
            //    }

            //}





            foreach (DataRow row in ds.Tables[0].Rows)
            {                
                DataRow[] rows = dsExames.Tables[0].Select("Id=" + Convert.ToInt32(row["IdExameDicionario"]));
                                
                if (rows.Count() > 0)
                {
                    rExamesASO = rExamesASO.Replace(Convert.ToString(rows[0]["Nome"]).ToUpper().Trim(), "");
                }
            }




            if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().IndexOf("Prajna") >= 0)
            {
                if (zClinico == true)
                {
                    lst_Exames.Items.Add("Exame Clínico");
                    lst_Exames.Items[lst_Exames.Items.Count - 1].Selected = true;
                }
            }



            //if (lst_Exames.Items.Count < 1)
            //{
            //    btnemp.Enabled = false;
            //}
            //else
            //{

            if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().IndexOf("Prajna") > 0)
            {
                btnemp.Visible = false;
                btnemp.Enabled = false;
            }
            else
            {
                btnemp.Visible = false;
                btnemp.Enabled = false;
            }
            //}

            //lst_Exames.Enabled = false;


            Cliente rCliente;
            rCliente = new Cliente(System.Convert.ToInt32(Session["Empresa"].ToString()));


            //se faltar exames na clínica, bloquear criação da guia/aso
            if (rCliente.Exames_Faltantes_Bloquear_ASO == true)
            {
                rExamesASO = rExamesASO.Replace("\n", "");
                rExamesASO = rExamesASO.Replace(System.Environment.NewLine, "");
                
                for (int rC = 30; rC > 0; rC--)
                {
                    rExamesASO = rExamesASO.Replace(rC.ToString().Trim() + ".", "");
                }

                

                rExamesASO = rExamesASO.Replace("Avaliação Clínica", "");
                rExamesASO = rExamesASO.Replace("AVALIAÇÃO CLÍNICA", "");
                rExamesASO = rExamesASO.Replace("Exame Clínico", "");
                rExamesASO = rExamesASO.Replace("Periódico", "");
                rExamesASO = rExamesASO.Replace("Admissional", "");
                rExamesASO = rExamesASO.Replace("Mudança de Função", "");
                rExamesASO = rExamesASO.Replace("Retorno ao Trabalho", "");
                rExamesASO = rExamesASO.Replace("Demissional", "");

                rExamesASO = rExamesASO.Replace(";", "");

                if (rExamesASO.Trim() != "")
                {
                    string rExamesData = rExamesASO;

                    //tirar eventuais datas que sobraram
                    rExamesData = rExamesData.Replace("/", "");
                    rExamesData = rExamesData.Replace("-", "");

                    for (int rC = 10; rC >= 0; rC--)
                    {
                        rExamesData = rExamesData.Replace(rC.ToString().Trim(), "");
                    }

                    if (rExamesData.Trim() == "") rExamesASO = rExamesData;
                }


                if (rExamesASO.Trim() != "")
                {
                    btnemp.Visible = false;
                    btnEmpASO.Visible = false;
                    btnEmp2Via.Visible = false;
                    if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().IndexOf("Prajna") > 0)
                    {
                        MsgBox1.Show("Ilitera.Net", "Atenção, esta clínica não está autorizada a realizar os exames abaixo.  Por gentileza, escolher outra clínica ou solicitar a liberação à equipe da Essence." + System.Environment.NewLine + rExamesASO, null,
                        new EO.Web.MsgBoxButton("OK"));
                    }
                    else
                    {
                        MsgBox1.Show("Ilitera.Net", "Atenção, esta clínica não está autorizada a realizar os exames abaixo.  Por gentileza, escolher outra clínica ou solicitar a liberação à equipe da Ilitera." + System.Environment.NewLine + rExamesASO, null,
                        new EO.Web.MsgBoxButton("OK"));
                    }
                    return;

                }
                else
                {
                    
                    if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().IndexOf("Prajna") > 0)
                    {
                        btnemp.Enabled = false;
                        btnemp.Visible = false;
                    }
                    else
                    {
                        btnemp.Visible = true;
                    }


                    btnEmpASO.Visible = true;
                    btnEmp2Via.Visible = true;
                }
            }


        }



		private void PopulaTelaEmpregado()
		{
            //variável empregado está vazia.  Ver como carregá-lo. - Wagner  
            empregado.Find(" tblEMPREGADO.nID_EMPREGADO = " + Request["IdEmpregado"].ToString() );

            Cliente cliente;
            cliente = new Cliente(System.Convert.ToInt32(Session["Empresa"].ToString()));


            EmpregadoFuncao empregadoFuncao = EmpregadoFuncao.GetEmpregadoFuncao(cliente,empregado);
            
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


            //bloquear visão toxicológico para não motorista
            if ( lblValorFuncao.Text.ToUpper().IndexOf("MOTORISTA")>=0)
            {
                chk_Toxicologico.Visible = true;
            }
            else
            {
                chk_Toxicologico.Checked = false;
                chk_Toxicologico.Visible = false;
            }

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


        protected void OpenReport(string directory, string fileAndQuery, string ReportName)
        {
            this.OpenReport(directory, fileAndQuery, ReportName, false);
        }

        protected void OpenReport(string directory, string fileAndQuery, string ReportName, bool useDirectoryForLocalProcess)
        {
            StringBuilder st = new StringBuilder();

            Guid strAux = Guid.NewGuid();

            string valueProcess = "Local";

            if (valueProcess.Equals("Remote"))
                st.Append("void(window.open('http://report.ilitera.net/index.aspx?Identity=" + strAux.ToString() + "opsa" + strAux.ToString()
                    + "&Key=" + strAux.ToString() + "MestraNet" + strAux.ToString()
                    + "&PathAndQuery=" + HttpUtility.UrlEncode(directory + "/" + fileAndQuery) + "', '" + ReportName + "', 'scrollbars=yes, resizable=yes, toolbar=no, addressbar=no, menubar=no, width=800px, height=600px'));");
            else if (valueProcess.Equals("Local"))
            {
                if (useDirectoryForLocalProcess)
                {
                    //st.Append("void(window.open('../" + directory + "/" + fileAndQuery + "', '" + ReportName + "', 'scrollbars=yes, resizable=yes, toolbar=no, addressbar=no, menubar=no, width=800px, height=600px'));");
                    st.AppendFormat("void(window.open('../{0}/{1}', '{2}','scrollbars=yes, resizable=yes, toolbar=no, addressbar=no, menubar=no, width=800px, height=600px'));", directory, fileAndQuery, ReportName);
                }
                else
                {
                    //st.Append("void(window.open('" + fileAndQuery + "', '" + ReportName + "', 'scrollbars=yes, resizable=yes, toolbar=no, addressbar=no, menubar=no, width=800px, height=600px'));");
                    st.AppendFormat("void(window.open('{0}','{1}', 'scrollbars=yes, resizable=yes, toolbar=no, addressbar=no, menubar=no, width=800px, height=600px'));", fileAndQuery, ReportName);
                }
            }

            ScriptManager.RegisterStartupScript(this, this.GetType(), String.Format("OpenReport{0}", ReportName), st.ToString(), true);
        }





        protected void btnemp_Click(object sender, EventArgs e)
        {
            int xAux;
            string xExames = "";
            string xExames2 = "";
            string xExames3 = "";
            string xExames4 = "";
            string xTipo = "";
            string xBasico = "0";
            string xObs = "";
            int xCont = 0;


            Guid strAux = Guid.NewGuid();

            string xImpDt = "S";

            if (chk_Data.Checked == false) xImpDt = "N";

            xObs = txt_Obs.Text.Trim();

            if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().IndexOf("Prajna") > 0)
            {
                int xDivisao = 7;

                if (lst_Exames.Items.Count < 18)
                    xDivisao = 5;


                for (xAux = 0; xAux < lst_Exames.Items.Count; xAux++)
                {
                    if (lst_Exames.Items[xAux].Selected == true)
                    {
                        xCont++;

                        if (xCont < (xDivisao + 1))
                        {
                            xExames = xExames + " /n " + lst_Exames.Items[xAux].Text;
                        }
                        else if (xCont < ((xDivisao * 2) - 1))
                        {
                            xExames2 = xExames2 + " /n " + lst_Exames.Items[xAux].Text;
                        }
                        else if (xCont < ((xDivisao * 3) - 1))
                        {
                            xExames3 = xExames3 + " /n " + lst_Exames.Items[xAux].Text;
                        }
                        else
                        {
                            xExames4 = xExames4 + " /n " + lst_Exames.Items[xAux].Text;
                        }


                    }

                }
            }
            else
            {
                int xDivisao = 13;

               // if (lst_Exames.Items.Count < 18)
                   // xDivisao = 5;

                for (xAux = 0; xAux < lst_Exames.Items.Count; xAux++)
                {
                    if (lst_Exames.Items[xAux].Selected == true)
                    {
                        xCont++;

                        if (xCont < (xDivisao + 1))
                        {
                            xExames = xExames + " /n " + lst_Exames.Items[xAux].Text;
                        }
                        else if (xCont < ((xDivisao * 2) - 1))
                        {
                            xExames2 = xExames2 + " /n " + lst_Exames.Items[xAux].Text;
                        }
                        else if (xCont < ((xDivisao * 3) - 1))
                        {
                            xExames3 = xExames3 + " /n " + lst_Exames.Items[xAux].Text;
                        }
                        else
                        {
                            xExames4 = xExames4 + " /n " + lst_Exames.Items[xAux].Text;
                        }


                    }

                }


            }






            // int xTipoExame = 0;

            if (chk_Basico.Checked == true)
            {
                xBasico = "1";
            }

            if (rd_Admissao.Checked == true)
            {
                xTipo = "A";
                //  xTipoExame = 1;
            }
            else if (rd_Demissional.Checked == true)
            {
                xTipo = "D";
                // xTipoExame = 2;
            }
            else if (rd_Mudanca.Checked == true)
            {
                xTipo = "M";
                // xTipoExame = 3;
            }
            else if (rd_Outro.Checked == true)
            {
                xTipo = "O";
                // xTipoExame = 0;
            }
            else if (rd_Periodico.Checked == true)
            {
                xTipo = "P";
                //  xTipoExame = 4;
            }
            else if (rd_Retorno.Checked == true)
            {
                xTipo = "R";
                // xTipoExame = 5;
            }




            ////bloquear demissionais prajna
            //if (xTipo.ToUpper().Trim() == "D")
            //{
            //    if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().IndexOf("Prajna") > 0)
            //    {

            //        MsgBox1.Show("Ilitera.Net", "Prezado cliente, para agendamento de exame demissional favor entrar em contato com nossa central de atendimento: centraldeatendimento@5aessence.com.br", null,
            //        new EO.Web.MsgBoxButton("OK"));
            //        return;

            //    }
            //}





             cliente.Find(System.Convert.ToInt32(Request["IdEmpresa"].ToString()));
            
            
         
            //se demissional, deve ter 90 dias de diferença pelo menos, senao aviso
            //se periódico, deve ter 90 dias de diferença pelo menos, senao aviso
            if (xTipo.ToUpper().Trim() == "D" || xTipo.ToUpper().Trim() == "P")
            {

                Ilitera.Common.Juridica zJuridica = new Ilitera.Common.Juridica();
                zJuridica.Find(System.Convert.ToInt32(Request["IdEmpresa"].ToString()));
                int zDiasGrauRisco = 90;

                if (zJuridica.GetCnaeSesmt().GrauRisco < 3)
                    zDiasGrauRisco = 135;
                else
                    zDiasGrauRisco = 90;

                


                //ExameBase rexame2 = new ExameBase();
                //rexame2.Find(" IDEMPREGADO = " + Request["IdEmpregado"].ToString() + " and IdExameDicionario in ( 1,2,3,4,5 )  and  datediff(day, DataExame, convert( smalldatetime,'" + txt_Data.Text + "', 103 )) <= 90 and IndResultado <> 0 ");

                ArrayList nExame = new ExameBase().Find(" IDEMPREGADO = " + Request["IdEmpregado"].ToString() + " and IdExameDicionario in ( 1,2,3,4,5 )  and  datediff(day, DataExame, convert( smalldatetime,'" + txt_Data.Text + "', 103 )) <= " + zDiasGrauRisco.ToString() +  " and IndResultado  not in ( 0, 3 )  ");                

                foreach (ExameBase rexame2 in nExame)
                {
                    if (rexame2.Id != 0)
                    {

                        if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().IndexOf("Prajna") > 0)
                        {
                            if (xTipo.ToUpper().Trim() == "D")
                            {
                                //MsgBox1.Show("Ilitera.Net", "O último exame clínico realizado possibilita a demissão deste colaborador sem a necessidade de exame demissional. " +
                                //             "Caso deseje realizá-lo mesmo assim, favor manter contato com a central de atendimento no e-mail centraldeatendimento@5aessence.com.br", null,
                                //new EO.Web.MsgBoxButton("Ok", null, "Delete"),
                                //new EO.Web.MsgBoxButton("Saiba mais", null, "Saiba mais"));

                                MsgBox1.Show("Ilitera.Net", "O exame demissional somente é obrigatório desde que o último exame médico ocupacional, seja de que natureza for - admissional, retorno ao trabalho, mudança de risco ou mesmo o periódico - " +
                                             "tenha sido realizado há mais de 135 ou 90 dias, sendo a primeira opção de 135 dias para as empresas com grau de risco 1 ou 2 e 90 dias para as empresas com grau de risco equivalente a 3 ou 4. " + System.Environment.NewLine +
                                             "Os exame demissionais somente serão obrigatórios se as condições especificadas no parágrafo anterior forem preenchidas.  O exame clínico demissional não é obrigatório em quaisquer circunstâncias, " + 
                                             "bastando a demissão do colaborador para que se torne necessário.  Para sua relização deverá haver duas condições obrigatórias, ou sejam, a demissão do colaborador e a não realização de exame clínico " + 
                                             "anterior nos prazos estabelecidos nos dois itens da norma legal, 135 ou 90 dias conforme o caso. Dúvidas, entrar em contato com a central de atendimento no e-mail centraldeatendimento@essencenet.com.br", null,
                                new EO.Web.MsgBoxButton("Não quero realizar o Exame Demissional", null, "Fechar"),
                                new EO.Web.MsgBoxButton("Quero Agendar o Exame mesmo assim", null, "Quero Agendar o Exame mesmo assim"));


                                return;
                            }
                            else
                            {

                                //MsgBox1.Show("Ilitera.Net", "Último exame dentro da validade, caso deseje realizá-lo mesmo assim, favor manter contato com a central de atendimento no e-mail centraldeatendimento@5aessence.com.br", null,
                                //new EO.Web.MsgBoxButton("OK"));
                                //return;
                                if (cliente.Bloquear_ASO_Planejamento == false)
                                {

                                    MsgBox1.Show("Ilitera.Net", "Último exame dentro da validade. Dúvidas, entrar em contato com a central de atendimento no e-mail centraldeatendimento@essencenet.com.br", null,
                                                                   new EO.Web.MsgBoxButton("Não quero realizar o exame", null, "Fechar"),
                                                                   new EO.Web.MsgBoxButton("Quero Agendar o Exame mesmo assim", null, "Quero Agendar o Exame mesmo assim"));
                                    return;
                                }
                                else
                                {
                                    MsgBox1.Show("Ilitera.Net", "Último exame dentro da validade. Dúvidas, entrar em contato com a central de atendimento no e-mail centraldeatendimento@essencenet.com.br", null,
                                                                    new EO.Web.MsgBoxButton("Fechar", null, "Fechar"));
                                                                   //new EO.Web.MsgBoxButton("Não quero realizar o exame", null, "Delete"),
                                                                   //new EO.Web.MsgBoxButton("Quero Agendar o Exame mesmo assim", null, "Quero Agendar o Exame mesmo assim"));
                                    return;

                                }

                            }
                        }
                        else if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("COTIA") > 0 || Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("GLOBAL") > 0)
                        {
                            if (xTipo.ToUpper().Trim() == "D")
                            {
                                //MsgBox1.Show("Ilitera.Net", "O último exame clínico realizado possibilita a demissão deste colaborador sem a necessidade de exame demissional. " +
                                //             "Caso deseje realizá-lo mesmo assim, favor manter contato com a central de atendimento no e-mail centraldeatendimento@5aessence.com.br", null,
                                //new EO.Web.MsgBoxButton("Ok", null, "Delete"),
                                //new EO.Web.MsgBoxButton("Saiba mais", null, "Saiba mais"));

                                MsgBox1.Show("Ilitera.Net", "O exame demissional somente é obrigatório desde que o último exame médico ocupacional, seja de que natureza for - admissional, retorno ao trabalho, mudança de risco ou mesmo o periódico - " +
                                             "tenha sido realizado há mais de 135 ou 90 dias, sendo a primeira opção de 135 dias para as empresas com grau de risco 1 ou 2 e 90 dias para as empresas com grau de risco equivalente a 3 ou 4. " + System.Environment.NewLine +
                                             "Os exame demissionais somente serão obrigatórios se as condições especificadas no parágrafo anterior forem preenchidas.  O exame clínico demissional não é obrigatório em quaisquer circunstâncias, " +
                                             "bastando a demissão do colaborador para que se torne necessário.  Para sua relização deverá haver duas condições obrigatórias, ou sejam, a demissão do colaborador e a não realização de exame clínico " +
                                             "anterior nos prazos estabelecidos nos dois itens da norma legal, 135 ou 90 dias conforme o caso. Dúvidas, entrar em contato com a central de atendimento no e-mail medicina.sp.cot@ilitera.com.br", null,
                                new EO.Web.MsgBoxButton("Não quero realizar o Exame Demissional", null, "Fechar"),
                                new EO.Web.MsgBoxButton("Quero Agendar o Exame mesmo assim", null, "Quero Agendar o Exame mesmo assim"));


                                return;
                            }
                            else
                            {

                                //MsgBox1.Show("Ilitera.Net", "Último exame dentro da validade, caso deseje realizá-lo mesmo assim, favor manter contato com a central de atendimento no e-mail centraldeatendimento@5aessence.com.br", null,
                                //new EO.Web.MsgBoxButton("OK"));
                                //return;
                                if (cliente.Bloquear_ASO_Planejamento == false)
                                {

                                    MsgBox1.Show("Ilitera.Net", "Último exame dentro da validade. Dúvidas, entrar em contato com a central de atendimento no e-mail medicina.sp.cot@ilitera.com.br", null,
                                                                   new EO.Web.MsgBoxButton("Não quero realizar o exame", null, "Fechar"),
                                                                   new EO.Web.MsgBoxButton("Quero Agendar o Exame mesmo assim", null, "Quero Agendar o Exame mesmo assim"));
                                    return;
                                }
                                else
                                {
                                    MsgBox1.Show("Ilitera.Net", "Último exame dentro da validade. Dúvidas, entrar em contato com a central de atendimento no e-mail medicina.sp.cot@ilitera.com.br", null,
                                                                    new EO.Web.MsgBoxButton("Fechar", null, "Fechar"));
                                    //new EO.Web.MsgBoxButton("Não quero realizar o exame", null, "Delete"),
                                    //new EO.Web.MsgBoxButton("Quero Agendar o Exame mesmo assim", null, "Quero Agendar o Exame mesmo assim"));
                                    return;

                                }

                            }
                        }
                        else
                        {


                            if (xTipo.ToUpper().Trim() == "D")
                            {
                                if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().IndexOf("Daiti") > 0)
                                {
                                    //MsgBox1.Show("Ilitera.Net", "O último exame clínico realizado possibilita a demissão deste colaborador sem a necessidade de exame demissional. " +
                                    //             "Caso deseje realizá-lo mesmo assim, favor manter contato com a central de atendimento no e-mail recepcao.sp.dai@ilitera.com.br", null,
                                    //new EO.Web.MsgBoxButton("Ok", null, "Delete"),
                                    //new EO.Web.MsgBoxButton("Saiba mais", null, "Saiba mais"));
                                    MsgBox1.Show("Ilitera.Net", "O exame demissional somente é obrigatório desde que o último exame médico ocupacional, seja de que natureza for - admissional, retorno ao trabalho, mudança de risco ou mesmo o periódico - " +
                                                 "tenha sido realizado há mais de 135 ou 90 dias, sendo a primeira opção de 135 dias para as empresas com grau de risco 1 ou 2 e 90 dias para as empresas com grau de risco equivalente a 3 ou 4. " + System.Environment.NewLine +
                                                 "Os exame demissionais somente serão obrigatórios se as condições especificadas no parágrafo anterior forem preenchidas.  O exame clínico demissional não é obrigatório em quaisquer circunstâncias, " +
                                                 "bastando a demissão do colaborador para que se torne necessário.  Para sua relização deverá haver duas condições obrigatórias, ou sejam, a demissão do colaborador e a não realização de exame clínico " +
                                                 "anterior nos prazos estabelecidos nos dois itens da norma legal, 135 ou 90 dias conforme o caso. Dúvidas, entrar em contato com a central de atendimento no e-mail agendamento.sp.dai@ilitera.com.br", null,
                                                 new EO.Web.MsgBoxButton("Fechar", null, "Fechar"));
                                    //new EO.Web.MsgBoxButton("Não quero realizar o Exame Demissional", null, "Fechar"),
                                    //new EO.Web.MsgBoxButton("Quero Agendar o Exame mesmo assim", null, "Quero Agendar o Exame mesmo assim"));

                                }
                                else if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("GLOBAL") > 0)
                                {
                                    MsgBox1.Show("Ilitera.Net", "O exame demissional somente é obrigatório desde que o último exame médico ocupacional, seja de que natureza for - admissional, retorno ao trabalho, mudança de risco ou mesmo o periódico - " +
                                          "tenha sido realizado há mais de 135 ou 90 dias, sendo a primeira opção de 135 dias para as empresas com grau de risco 1 ou 2 e 90 dias para as empresas com grau de risco equivalente a 3 ou 4. " + System.Environment.NewLine +
                                          "Os exame demissionais somente serão obrigatórios se as condições especificadas no parágrafo anterior forem preenchidas.  O exame clínico demissional não é obrigatório em quaisquer circunstâncias, " +
                                          "bastando a demissão do colaborador para que se torne necessário.  Para sua relização deverá haver duas condições obrigatórias, ou sejam, a demissão do colaborador e a não realização de exame clínico " +
                                          "anterior nos prazos estabelecidos nos dois itens da norma legal, 135 ou 90 dias conforme o caso. Dúvidas, entrar em contato com a central de atendimento no e-mail medicina.sp.cot@ilitera.com.br", null,
                             new EO.Web.MsgBoxButton("Não quero realizar o Exame Demissional", null, "Fechar"),
                             new EO.Web.MsgBoxButton("Quero Agendar o Exame mesmo assim", null, "Quero Agendar o Exame mesmo assim"));


                                    return;
                                }
                                else
                                {
                                    //MsgBox1.Show("Ilitera.Net", "O último exame clínico realizado possibilita a demissão deste colaborador sem a necessidade de exame demissional. " +
                                    //             "Caso deseje realizá-lo mesmo assim, favor manter contato com a central de atendimento no e-mail atendimento@ilitera.com.br", null,
                                    //new EO.Web.MsgBoxButton("Ok", null, "Delete"),
                                    //new EO.Web.MsgBoxButton("Saiba mais", null, "Saiba mais"));
                                    MsgBox1.Show("Ilitera.Net", "O exame demissional somente é obrigatório desde que o último exame médico ocupacional, seja de que natureza for - admissional, retorno ao trabalho, mudança de risco ou mesmo o periódico - " +
                                                 "tenha sido realizado há mais de 135 ou 90 dias, sendo a primeira opção de 135 dias para as empresas com grau de risco 1 ou 2 e 90 dias para as empresas com grau de risco equivalente a 3 ou 4. " + System.Environment.NewLine +
                                                 "Os exame demissionais somente serão obrigatórios se as condições especificadas no parágrafo anterior forem preenchidas.  O exame clínico demissional não é obrigatório em quaisquer circunstâncias, " +
                                                 "bastando a demissão do colaborador para que se torne necessário.  Para sua relização deverá haver duas condições obrigatórias, ou sejam, a demissão do colaborador e a não realização de exame clínico " +
                                                 "anterior nos prazos estabelecidos nos dois itens da norma legal, 135 ou 90 dias conforme o caso. Dúvidas, entrar em contato com a central de atendimento no e-mail atendimento@ilitera.com.br", null,
                                                 new EO.Web.MsgBoxButton("Ok", null, "Delete"));
                                    //new EO.Web.MsgBoxButton("Não quero realizar o Exame Demissional", null, "Delete"),
                                    //new EO.Web.MsgBoxButton("Quero Agendar o Exame mesmo assim", null, "Quero Agendar o Exame mesmo assim"));

                                }
                                return;
                            }
                            else
                            {
                                if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().IndexOf("Daiti") > 0)
                                {
                                    //MsgBox1.Show("Ilitera.Net", "Último exame dentro da validade, caso deseje realizá-lo mesmo assim, favor manter contato com a central de atendimento no e-mail recepcao.sp.dai@ilitera.com.br", null,
                                    //new EO.Web.MsgBoxButton("OK"));

                                    if (cliente.Bloquear_ASO_Planejamento == false)
                                    {
                                        MsgBox1.Show("Ilitera.Net", "Último exame dentro da validade. Dúvidas, entrar em contato com a central de atendimento no e-mail agendamento.sp.dai@ilitera.com.br", null,                                            
                                                                       new EO.Web.MsgBoxButton("Não quero realizar o exame", null, "Fechar"),
                                                                       new EO.Web.MsgBoxButton("Quero Agendar o Exame mesmo assim", null, "Quero Agendar o Exame mesmo assim"));
                                        return;
                                    }
                                    else
                                    {
                                        MsgBox1.Show("Ilitera.Net", "Último exame dentro da validade. Dúvidas, entrar em contato com a central de atendimento no e-mail agendamento.sp.dai@ilitera.com.br", null,
                                                                     new EO.Web.MsgBoxButton("Fechar", null, "Fechar"));
                                        return;
                                    }

                                }
                                else
                                {
                                    //MsgBox1.Show("Ilitera.Net", "Último exame dentro da validade, caso deseje realizá-lo mesmo assim, favor manter contato com a central de atendimento no e-mail atendimento@ilitera.com.br", null,
                                    //new EO.Web.MsgBoxButton("OK"));
                                    if (cliente.Bloquear_ASO_Planejamento == false)
                                    {
                                        MsgBox1.Show("Ilitera.Net", "Último exame dentro da validade. Dúvidas, entrar em contato com a central de atendimento no e-mail atendimento@ilitera.com.br", null,                                               
                                                    new EO.Web.MsgBoxButton("Não quero realizar o exame", null, "Fechar"),
                                                    new EO.Web.MsgBoxButton("Quero Agendar o Exame mesmo assim", null, "Quero Agendar o Exame mesmo assim"));
                                        return;
                                    }
                                    else
                                    {
                                        MsgBox1.Show("Ilitera.Net", "Último exame dentro da validade. Dúvidas, entrar em contato com a central de atendimento no e-mail atendimento@ilitera.com.br", null,
                                                                       new EO.Web.MsgBoxButton("Fechar", null, "Fechar"));
                                        //new EO.Web.MsgBoxButton("Não quero realizar o exame", null, "Delete"),
                                        //new EO.Web.MsgBoxButton("Quero Agendar o Exame mesmo assim", null, "Quero Agendar o Exame mesmo assim"));
                                        return;
                                    }


                                }                                

                                
                            }


                        }
                    }

                }



                if (xTipo.ToUpper().Trim() == "P")
                {

                    int zDias_Desconsiderar = 0;
                    
                    //dias desconsiderar dos exames complementares vai valer para validar exame fora do planejamento
                    if (cliente.Ativar_DesconsiderarCompl == true)
                    {
                        if (cliente.Dias_Desconsiderar > 0)
                        {
                            zDias_Desconsiderar = cliente.Dias_Desconsiderar;
                        }
                    }


                    Ilitera.Data.Clientes_Funcionarios xPlan = new Ilitera.Data.Clientes_Funcionarios();

                    if (xPlan.Buscar_Planejamento_Exame_Colaborador(System.Convert.ToInt32(Request["IdEmpregado"].ToString()), 4, txt_Data.Text.Trim(), zDias_Desconsiderar) == false)
                    {
                        if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().IndexOf("Prajna") > 0)
                        {
                            //MsgBox1.Show("Ilitera.Net", "Exame fora do planejamento, caso deseje realizá-lo mesmo assim, favor manter contato com a central de atendimento no e-mail centraldeatendimento@5aessence.com.br", null,
                            //new EO.Web.MsgBoxButton("OK"));
                            //return;
                            if (cliente.Bloquear_ASO_Planejamento == false)
                            {

                                MsgBox1.Show("Ilitera.Net", "Último exame dentro da validade. Dúvidas, entrar em contato com a central de atendimento no e-mail centraldeatendimento@essencenet.com.br", null,
                                                               new EO.Web.MsgBoxButton("Não quero realizar o exame", null, "Fechar"),
                                                               new EO.Web.MsgBoxButton("Quero Agendar o Exame mesmo assim", null, "Quero Agendar o Exame mesmo assim"));
                                return;
                            }
                            else
                            {
                                MsgBox1.Show("Ilitera.Net", "Último exame dentro da validade. Dúvidas, entrar em contato com a central de atendimento no e-mail centraldeatendimento@essencenet.com.br", null,
                                                                new EO.Web.MsgBoxButton("Fechar", null, "Fechar"));
                                //new EO.Web.MsgBoxButton("Não quero realizar o exame", null, "Delete"),
                                //new EO.Web.MsgBoxButton("Quero Agendar o Exame mesmo assim", null, "Quero Agendar o Exame mesmo assim"));
                                return;

                            }

                        }
                        else
                        {
                            if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().IndexOf("Daiti") > 0)
                            {
                                //MsgBox1.Show("Ilitera.Net", "Exame fora do planejamento, caso deseje realizá-lo mesmo assim, favor manter contato com a central de atendimento no e-mail recepcao.sp.dai@ilitera.com.br", null,
                                //new EO.Web.MsgBoxButton("Ok"));
                                if (cliente.Bloquear_ASO_Planejamento == false)
                                {
                                    MsgBox1.Show("Ilitera.Net", "Último exame dentro da validade. Dúvidas, entrar em contato com a central de atendimento no e-mail agendamento.sp.dai@ilitera.com.br", null,                                        
                                                                   new EO.Web.MsgBoxButton("Não quero realizar o exame", null, "Fechar"),
                                                                   new EO.Web.MsgBoxButton("Quero Agendar o Exame mesmo assim", null, "Quero Agendar o Exame mesmo assim"));
                                    return;
                                }
                                else
                                {
                                    MsgBox1.Show("Ilitera.Net", "Último exame dentro da validade. Dúvidas, entrar em contato com a central de atendimento no e-mail agendamento.sp.dai@ilitera.com.br", null,
                                                                 new EO.Web.MsgBoxButton("Fechar", null, "Fechar"));
                                    return;
                                }


                            }
                            else if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().IndexOf("Cotia") > 0)
                            {
                                //MsgBox1.Show("Ilitera.Net", "Exame fora do planejamento, caso deseje realizá-lo mesmo assim, favor manter contato com a central de atendimento no e-mail recepcao.sp.dai@ilitera.com.br", null,
                                //new EO.Web.MsgBoxButton("Ok"));
                                if (cliente.Bloquear_ASO_Planejamento == false)
                                {
                                    MsgBox1.Show("Ilitera.Net", "Último exame dentro da validade. Dúvidas, entrar em contato com a central de atendimento no e-mail medicina.sp.cot@ilitera.com.br", null,
                                        new EO.Web.MsgBoxButton("Fechar", null, "Fechar"));
                                    //new EO.Web.MsgBoxButton("Não quero realizar o exame", null, "Fechar"),
                                    //                              new EO.Web.MsgBoxButton("Quero Agendar o Exame mesmo assim", null, "Quero Agendar o Exame mesmo assim"));
                                    return;
                                }
                                else
                                {
                                    MsgBox1.Show("Ilitera.Net", "Último exame dentro da validade. Dúvidas, entrar em contato com a central de atendimento no e-mail medicina.sp.cot@ilitera.com.br", null,
                                                                 new EO.Web.MsgBoxButton("Fechar", null, "Fechar"));
                                    return;
                                }


                            }
                            else
                            {
                                //MsgBox1.Show("Ilitera.Net", "Exame fora do planejamento, caso deseje realizá-lo mesmo assim, favor manter contato com a central de atendimento no e-mail atendimento@ilitera.com.br", null,
                                //new EO.Web.MsgBoxButton("Ok"));
                                if (cliente.Bloquear_ASO_Planejamento == false)
                                {
                                    MsgBox1.Show("Ilitera.Net", "Último exame dentro da validade. Dúvidas, entrar em contato com a central de atendimento no e-mail atendimento@ilitera.com.br", null,
                                                new EO.Web.MsgBoxButton("Não quero realizar o exame", null, "Fechar"),
                                                new EO.Web.MsgBoxButton("Quero Agendar o Exame mesmo assim", null, "Quero Agendar o Exame mesmo assim"));
                                    return;
                                }
                                else
                                {
                                    MsgBox1.Show("Ilitera.Net", "Último exame dentro da validade. Dúvidas, entrar em contato com a central de atendimento no e-mail atendimento@ilitera.com.br", null,
                                                                   new EO.Web.MsgBoxButton("Fechar", null, "Fechar"));
                                    //new EO.Web.MsgBoxButton("Não quero realizar o exame", null, "Delete"),
                                    //new EO.Web.MsgBoxButton("Quero Agendar o Exame mesmo assim", null, "Quero Agendar o Exame mesmo assim"));
                                    return;
                                }
                                                               


                            }
                            
                        }
                    }
                }

            }



            //Session["Exames"] = xExames;

            //salvar dados em tabela da guia gerada
            //empresa
            //colaborador
            //tipo de exame
            //clinica
            //data
            //hora
            //exames
            Entities.Usuario user = (Entities.Usuario)Session["usuarioLogado"];


            //pegar data de planejamento + "|" + Data Ultimo exame
            string rData = "";
            Ilitera.Data.Clientes_Funcionarios xPlan2 = new Ilitera.Data.Clientes_Funcionarios();
            rData = xPlan2.Buscar_Data_Planejamento_Exame_Colaborador(System.Convert.ToInt32(Request["IdEmpregado"].ToString()), 4, txt_Data.Text.Trim());


            Ilitera.Data.PPRA_EPI xGuia = new Ilitera.Data.PPRA_EPI();
            xGuia.Salvar_Dados_Guia_Encaminhamento(System.Convert.ToInt32(Request["IdEmpresa"].ToString()), System.Convert.ToInt32(Request["IdEmpregado"].ToString()), xTipo, xExames + "|" + xExames2 + "|" + xExames3 + "|" + xExames4, txt_Data.Text, txt_Hora.Text, cmb_Clinicas.Items[cmb_Clinicas.SelectedIndex].ToString(), user.IdUsuario, "N", rData.Substring(0,10), rData.Substring(11), txt_Obs.Text.Trim());


            //OpenReport("DadosEmpresa", "RelatorioGuia.aspx?IliteraSystem=" + strAux.ToString() + strAux.ToString()
            //    + "&IdEmpresa=" + Request["IdEmpresa"] + "&IdEmpregado=" + Request["IdEmpregado"] + "&tiporelatorio=tudo" + "&Exames=" + xExames + "&Exames2=" + xExames2 + "&Exames3=" + xExames3 + "&Exames4=" + xExames4 +  "&IdClinica=" + cmb_Clinicas.SelectedValue.ToString() + "&Hora_Exame=" + txt_Hora.Text + "&Data_Exame=" + txt_Data.Text + "&Tipo=" + xTipo + "&Basico=" + xBasico + "&Obs=" + xObs , "RelatorioGuia", true);

            Session["Obs_Guia"] = xObs;


            cliente.Find(System.Convert.ToInt32(Request["IdEmpresa"].ToString()));
            //pegar exames de PCMSO do funcionário
            empregado.Find(" tblEMPREGADO.nID_EMPREGADO = " + Request["IdEmpregado"].ToString());

            EmpregadoFuncao rEmprFuncao = new EmpregadoFuncao(); 
            rEmprFuncao = EmpregadoFuncao.GetEmpregadoFuncao(empregado, cliente.Id);


            OpenReport("DadosEmpresa", "RelatorioGuia.aspx?IliteraSystem=" + strAux.ToString()
    + "&IdEmpresa=" + Request["IdEmpresa"] + "&IdEmpregado=" + Request["IdEmpregado"] + "&E1=" + xExames + "&E2=" + xExames2 + "&E3=" + xExames3 + "&E4=" + xExames4 + "&IdClinica=" + cmb_Clinicas.SelectedValue.ToString() + "&H_E=" + txt_Hora.Text + "&D_E=" + txt_Data.Text + "&Tipo=" + xTipo + "&Basico=" + xBasico + "&ImpDt=" + xImpDt + "&IdEmprFunc=" + rEmprFuncao.Id.ToString().Trim(), "RelatorioGuia", true);


        }


        protected void Gerar_Guia()
        {
            int xAux;
            string xExames = "";
            string xExames2 = "";
            string xExames3 = "";
            string xExames4 = "";
            string xTipo = "";
            string xBasico = "0";
            string xObs = "";
            int xCont = 0;


            Guid strAux = Guid.NewGuid();

            string xImpDt = "S";

            if (chk_Data.Checked == false) xImpDt = "N";

            xObs = txt_Obs.Text.Trim();



            if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().IndexOf("Prajna") > 0)
            {
                int xDivisao = 7;

                if (lst_Exames.Items.Count < 18)
                    xDivisao = 5;


                for (xAux = 0; xAux < lst_Exames.Items.Count; xAux++)
                {
                    if (lst_Exames.Items[xAux].Selected == true)
                    {
                        xCont++;

                        if (xCont < (xDivisao + 1))
                        {
                            xExames = xExames + " /n " + lst_Exames.Items[xAux].Text;
                        }
                        else if (xCont < ((xDivisao * 2) - 1))
                        {
                            xExames2 = xExames2 + " /n " + lst_Exames.Items[xAux].Text;
                        }
                        else if (xCont < ((xDivisao * 3) - 1))
                        {
                            xExames3 = xExames3 + " /n " + lst_Exames.Items[xAux].Text;
                        }
                        else
                        {
                            xExames4 = xExames4 + " /n " + lst_Exames.Items[xAux].Text;
                        }


                    }

                }
            }
            else
            {
                int xDivisao = 13;

                // if (lst_Exames.Items.Count < 18)
                // xDivisao = 5;

                for (xAux = 0; xAux < lst_Exames.Items.Count; xAux++)
                {
                    if (lst_Exames.Items[xAux].Selected == true)
                    {
                        xCont++;

                        if (xCont < (xDivisao + 1))
                        {
                            xExames = xExames + " /n " + lst_Exames.Items[xAux].Text;
                        }
                        else if (xCont < ((xDivisao * 2) - 1))
                        {
                            xExames2 = xExames2 + " /n " + lst_Exames.Items[xAux].Text;
                        }
                        else if (xCont < ((xDivisao * 3) - 1))
                        {
                            xExames3 = xExames3 + " /n " + lst_Exames.Items[xAux].Text;
                        }
                        else
                        {
                            xExames4 = xExames4 + " /n " + lst_Exames.Items[xAux].Text;
                        }


                    }

                }


            }



          

            // int xTipoExame = 0;

            if (chk_Basico.Checked == true)
            {
                xBasico = "1";
            }

            if (rd_Admissao.Checked == true)
            {
                xTipo = "A";
              //  xTipoExame = 1;
            }
            else if (rd_Demissional.Checked == true)
            {
                xTipo = "D";
              //  xTipoExame = 2;
            }
            else if (rd_Mudanca.Checked == true)
            {
                xTipo = "M";
              //  xTipoExame = 3;
            }
            else if (rd_Outro.Checked == true)
            {
                xTipo = "O";
              //  xTipoExame = 0;
            }
            else if (rd_Periodico.Checked == true)
            {
                xTipo = "P";
              //  xTipoExame = 4;
            }
            else if (rd_Retorno.Checked == true)
            {
                xTipo = "R";
               // xTipoExame = 5;
            }


            //bloquear demissionais prajna
            //if (xTipo.ToUpper().Trim() == "D")
            //{
            //    if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().IndexOf("Prajna") > 0)
            //    {

            //        MsgBox1.Show("Ilitera.Net", "Prezado cliente, para agendamento de exame demissional favor entrar em contato com nossa central de atendimento: centraldeatendimento@5aessence.com.br", null,
            //        new EO.Web.MsgBoxButton("OK"));
            //        return;

            //    }
            //}




            Entities.Usuario user = (Entities.Usuario)Session["usuarioLogado"];

            //pegar data de planejamento + "|" + Data Ultimo exame
            string rData = "          |          ";

            Ilitera.Data.PPRA_EPI xGuia = new Ilitera.Data.PPRA_EPI();
            xGuia.Salvar_Dados_Guia_Encaminhamento(System.Convert.ToInt32(Request["IdEmpresa"].ToString()), System.Convert.ToInt32(Request["IdEmpregado"].ToString()), xTipo, xExames + "|" + xExames2 + "|" + xExames3 + "|" + xExames4, txt_Data.Text, txt_Hora.Text, cmb_Clinicas.Items[cmb_Clinicas.SelectedIndex].ToString(), user.IdUsuario, "S", rData.Substring(0, 10), rData.Substring(11), txt_Obs.Text.Trim());


            //OpenReport("DadosEmpresa", "RelatorioGuia.aspx?IliteraSystem=" + strAux.ToString() + strAux.ToString()
            //    + "&IdEmpresa=" + Request["IdEmpresa"] + "&IdEmpregado=" + Request["IdEmpregado"] + "&tiporelatorio=tudo" + "&Exames=" + xExames + "&Exames2=" + xExames2 + "&Exames3=" + xExames3 + "&Exames4=" + xExames4 +  "&IdClinica=" + cmb_Clinicas.SelectedValue.ToString() + "&Hora_Exame=" + txt_Hora.Text + "&Data_Exame=" + txt_Data.Text + "&Tipo=" + xTipo + "&Basico=" + xBasico + "&Obs=" + xObs , "RelatorioGuia", true);

            Session["Obs_Guia"] = xObs;


            cliente.Find(System.Convert.ToInt32(Request["IdEmpresa"].ToString()));
            //pegar exames de PCMSO do funcionário
            empregado.Find(" tblEMPREGADO.nID_EMPREGADO = " + Request["IdEmpregado"].ToString());

            EmpregadoFuncao rEmprFuncao = new EmpregadoFuncao();
            rEmprFuncao = EmpregadoFuncao.GetEmpregadoFuncao(empregado, cliente.Id);


            OpenReport("DadosEmpresa", "RelatorioGuia.aspx?IliteraSystem=" + strAux.ToString()
    + "&IdEmpresa=" + Request["IdEmpresa"] + "&IdEmpregado=" + Request["IdEmpregado"] + "&E1=" + xExames + "&E2=" + xExames2 + "&E3=" + xExames3 + "&E4=" + xExames4 + "&IdClinica=" + cmb_Clinicas.SelectedValue.ToString() + "&H_E=" + txt_Hora.Text + "&D_E=" + txt_Data.Text + "&Tipo=" + xTipo + "&Basico=" + xBasico + "&ImpDt=" + xImpDt + "&IdEmprFunc=" + rEmprFuncao.Id.ToString().Trim(), "RelatorioGuia", true);


        }





        protected void MsgBox1_ButtonClick(object sender, System.Web.UI.WebControls.CommandEventArgs e)
        {
            //Use the command name to determine which
            //button was clicked
            if (e.CommandName == "Saiba mais")
            {
                //Proceed to delete....
                MsgBox2.Show("Saiba mais", "Observemos que o exame demissional é obrigatório desde que o último exame médico " +
                                            "ocupacional, seja de que natureza for – admissional, retorno ao trabalho, mudança de risco ou mesmo o periódico - tenha sido realizado há mais de 135 ou 90 dias, sendo a primeira opção de 135 dias para as empresas com grau de risco 1 ou 2 e 90 dias para as empresas com grau de risco equivalente a 3 ou 4. " + System.Environment.NewLine +
                                            " O texto legal é claro: os exames demissionais somente serão obrigatórios se as condições especificadas no parágrafo anterior foram preenchidas. O exame clínico demissional não é obrigatório em quaisquer circunstâncias, bastando a demissão do colaborador para que ele se torne necessário. Para a sua realização deverá haver duas condições obrigatórias, ou sejam a demissão do colaborador e a não realização de exame clínico anterior nos prazos estabelecidos nos dois item da norma legal, 135 ou 90 dias conforme o caso. " + System.Environment.NewLine +
                                            "Pensar em exame demissional com realização automática a partir da demissão é impactar o custo operacional dos programas médicos com exames desnecessários e procedimentos complementares, a cargo das empresas, com valores maiores ainda. O legislador foi sábio ao determinar o procedimento em questão, uma vez que, na grande maioria dos casos, a avaliação clínica dentro dos períodos especificados, 135 e 90 dias, são extremamente satisfatórias do ponto de vista da boa conduta médica e das diretrizes estabelecidas e aceitas pelo Conselho Federal de Medicina, órgão regulamentador máximo da profissão médica. " + System.Environment.NewLine +
                                            "Ressalte-se que a não exigência dos exames demissionais, nos casos específicos  " +
                                            "apresentados, não se trata de “brecha” na legislação vigente, mas, isto sim, foi alternativa pensada, discutida, debatida pelo grupo legislador e criador da Norma Regulamentadora nº 7, no ano de 1994, quando da emissão da Portaria nº 24 pelo Ministério do Trabalho e Emprego.  " + System.Environment.NewLine +
                                            "Vinte anos serão completados da sua publicação em dezembro próximo e jamais houve qualquer alteração neste item legal. ", null,
                new EO.Web.MsgBoxButton("OK"));
                return;
            }
            else if (e.CommandName == "Quero Agendar o Exame mesmo assim")
            {

                if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().IndexOf("Prajna") > 0)
                {
                    MsgBox2.Show("Ilitera.Net", "O exame clínico que será agendado a partir da sua escolha, como é dispensado legalmente, poderá acarretar cobrança adicional na sua próxima fatura mensal emitida. " +
                                                "A emissão ou não da mencionada cobrança dependerá dos acordos contratuais realizados. Dúvdas, entre em contato com a central de atendimento no e-mail centraldeatendimento@essencenet.com.br", null,
                    new EO.Web.MsgBoxButton("Não realizar o exame", null, "Delete"),
                    new EO.Web.MsgBoxButton("Prosseguir no agendamento do exame", null, "Prosseguir no agendamento do exame"));

                }
                else if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().IndexOf("Daiti") > 0)
                {
                    MsgBox2.Show("Ilitera.Net", "O exame clínico que será agendado a partir da sua escolha, como é dispensado legalmente, poderá acarretar cobrança adicional na sua próxima fatura mensal emitida. " +
                                                "A emissão ou não da mencionada cobrança dependerá dos acordos contratuais realizados. Dúvdas, entre em contato com a central de atendimento no e-mail agendamento.sp.dai@ilitera.com.br", null,
                    new EO.Web.MsgBoxButton("Não realizar o exame", null, "Delete"),
                    new EO.Web.MsgBoxButton("Prosseguir no agendamento do exame", null, "Prosseguir no agendamento do exame"));

                }
                else
                {

                    MsgBox2.Show("Ilitera.Net", "O exame clínico que será agendado a partir da sua escolha, como é dispensado legalmente, poderá acarretar cobrança adicional na sua próxima fatura mensal emitida. " +
                                                "A emissão ou não da mencionada cobrança dependerá dos acordos contratuais realizados. Dúvdas, entre em contato com a central de atendimento no e-mail atendimento@ilitera.com.br", null,
                    new EO.Web.MsgBoxButton("Não realizar o exame", null, "Delete"),
                    new EO.Web.MsgBoxButton("Prosseguir no agendamento do exame", null, "Prosseguir no agendamento do exame"));

                }

            }
            else if (e.CommandName == "Quero Agendar o Exame mesmo assim.")
            {
                if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().IndexOf("Prajna") > 0)
                {
                    MsgBox2.Show("Ilitera.Net", "O exame clínico que será agendado a partir da sua escolha, como é dispensado legalmente, poderá acarretar cobrança adicional na sua próxima fatura mensal emitida. " +
                                                "A emissão ou não da mencionada cobrança dependerá dos acordos contratuais realizados. Dúvdas, entre em contato com a central de atendimento no e-mail centraldeatendimento@essencenet.com.br", null,
                    new EO.Web.MsgBoxButton("Não realizar o exame", null, "Delete"),
                    new EO.Web.MsgBoxButton("Prosseguir no agendamento do exame.", null, "Prosseguir no agendamento do exame."));

                }
                else if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().IndexOf("Daiti") > 0)
                {
                    MsgBox2.Show("Ilitera.Net", "O exame clínico que será agendado a partir da sua escolha, como é dispensado legalmente, poderá acarretar cobrança adicional na sua próxima fatura mensal emitida. " +
                                                "A emissão ou não da mencionada cobrança dependerá dos acordos contratuais realizados. Dúvdas, entre em contato com a central de atendimento no e-mail agendamento.sp.dai@ilitera.com.br", null,
                    new EO.Web.MsgBoxButton("Não realizar o exame", null, "Delete"),
                    new EO.Web.MsgBoxButton("Prosseguir no agendamento do exame.", null, "Prosseguir no agendamento do exame."));

                }
                else
                {

                    MsgBox2.Show("Ilitera.Net", "O exame clínico que será agendado a partir da sua escolha, como é dispensado legalmente, poderá acarretar cobrança adicional na sua próxima fatura mensal emitida. " +
                                                "A emissão ou não da mencionada cobrança dependerá dos acordos contratuais realizados. Dúvdas, entre em contato com a central de atendimento no e-mail atendimento@ilitera.com.br", null,
                    new EO.Web.MsgBoxButton("Não realizar o exame", null, "Delete"),
                    new EO.Web.MsgBoxButton("Prosseguir no agendamento do exame.", null, "Prosseguir no agendamento do exame."));

                }

            }
            else if (e.CommandName == "Fechar")
            {
                Entities.Usuario user = (Entities.Usuario)Session["usuarioLogado"];
                Response.Redirect("DadosEmpregado.aspx?IdEmpregado=" + Session["Empregado"].ToString() + "&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString() + "&Tipo=1");
            }

        }


        protected void MsgBox2_ButtonClick(object sender, System.Web.UI.WebControls.CommandEventArgs e)
        {
            //Use the command name to determine which
            //button was clicked
            if (e.CommandName == "Prosseguir no agendamento do exame")
            {

                //criar log
                //enviar e-mail
                Gerar_Guia();

            }
            else if (e.CommandName == "Prosseguir no agendamento do exame.")
            {

                //criar log
                //enviar e-mail
                Gerar_Guia_ASO();

            }
            else if (e.CommandName == "Prosseguir no agendamento do exame-2")
            {
                //EY   - apagar exames clinicos sem resultado e em espera anteriores
                ArrayList nExame = new ExameBase().Find(" IDEMPREGADO = " + Request["IdEmpregado"].ToString() + " and IdExameDicionario in ( 1,2,3,4,5 )  and  DataExame < convert( smalldatetime, '" + txt_Data.Text + "', 103 ) and IndResultado  not in ( 1, 2 )  ");

                foreach (ExameBase rexame2 in nExame)
                {
                    if (rexame2.Id != 0)
                    {
                        rexame2.Delete();
                    }
                }

                Gerar_Guia_ASO();

            }
            else if ( e.CommandName == "Delete-2" )
            {
                Entities.Usuario user = (Entities.Usuario)Session["usuarioLogado"];
                Response.Redirect("DadosEmpregado.aspx?IdEmpregado=" + Session["Empregado"].ToString() + "&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString() + "&Tipo=1");
            }


        }


        protected void btnempASO_Click(object sender, EventArgs e)
        {
            if (cmb_Clinicas.SelectedValue.Trim() == "") return;

            lblTipoGuia.Text = "1";
            Criacao_Guia_ASO_Ficha(1);

        }

        protected void Criacao_Guia_ASO_Ficha( int xTipoGuia )
        { 
            int xAux;
            string xExames="";
            string xExames2="";
            string xExames3 = "";
            string xExames4 = "";
            string xTipo = "";
            string xBasico = "0";
            string xObs = "";
            int xCont = 0;
            string xEnvio_Email = "N";


            if (Validar_Data(txt_Data.Text) == false)
            {
                return;
            }


            if (rd_Outro.Checked == true)
            {
                MsgBox1.Show("Ilitera.Net", "Opção Outros não é válida para esse tipo de geração de documentos.", null,
                new EO.Web.MsgBoxButton("OK"));
                return;
            }

            if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().Trim().ToUpper() == "OPSA" || Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("PRO") > 0 || Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("VIA") > 0 || Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("DAITI") > 0 || Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("SHO") > 0 || (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("SAFETY") > 0 && Ilitera.Data.SQLServer.EntitySQLServer._LocalServer.ToString().ToUpper().IndexOf("FOCUS") > 0))
            {
                if (rd_Demissional.Checked == true)
                {
                    if (Validar_Data(txt_Demissao.Text) == false)
                    {
                        txt_Demissao.BackColor = System.Drawing.Color.Yellow;
                        return;
                    }
                }
            }


            string xEmail = lbl_email.Text.Trim();

            if (chk_eMail.Checked == true)
            {
                xEnvio_Email = "S";

                if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().IndexOf("Prajna") > 0 || Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().Trim() == "OPSA")
                {
                    {
                        if (xEmail == "")
                        {
                            MsgBox1.Show("Ilitera.Net", "Clínica não possui e-mail cadastrado.", null,
                            new EO.Web.MsgBoxButton("OK"));
                            return;
                        }

                        if (xEmail.IndexOf("@") < 0)
                        {
                            MsgBox1.Show("Ilitera.Net", "Clínica não possui e-mail válido cadastrado.", null,
                            new EO.Web.MsgBoxButton("OK"));
                            return;
                        }
                    }
                }
            }
            else
            {
                xEnvio_Email = "N";
            }

            string xImpDt = "S";

            if (chk_Data.Checked == false) xImpDt = "N";


            Guid strAux = Guid.NewGuid();

            

            xObs = txt_Obs.Text.Trim();

            if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().IndexOf("Prajna") > 0)
            {
                int xDivisao = 7;

                if (lst_Exames.Items.Count < 18)
                    xDivisao = 5;


                for (xAux = 0; xAux < lst_Exames.Items.Count; xAux++)
                {
                    if (lst_Exames.Items[xAux].Selected == true)
                    {
                        xCont++;

                        if (xCont < (xDivisao + 1))
                        {
                            xExames = xExames + " /n " + lst_Exames.Items[xAux].Text;
                        }
                        else if (xCont < ((xDivisao * 2) - 1))
                        {
                            xExames2 = xExames2 + " /n " + lst_Exames.Items[xAux].Text;
                        }
                        else if (xCont < ((xDivisao * 3) - 1))
                        {
                            xExames3 = xExames3 + " /n " + lst_Exames.Items[xAux].Text;
                        }
                        else
                        {
                            xExames4 = xExames4 + " /n " + lst_Exames.Items[xAux].Text;
                        }


                    }

                }
            }
            else
            {
                int xDivisao = 13;

                // if (lst_Exames.Items.Count < 18)
                // xDivisao = 5;

                for (xAux = 0; xAux < lst_Exames.Items.Count; xAux++)
                {
                    if (lst_Exames.Items[xAux].Selected == true)
                    {
                        xCont++;

                        if (xCont < (xDivisao + 1))
                        {
                            xExames = xExames + " /n " + lst_Exames.Items[xAux].Text;
                        }
                        else if (xCont < ((xDivisao * 2) - 1))
                        {
                            xExames2 = xExames2 + " /n " + lst_Exames.Items[xAux].Text;
                        }
                        else if (xCont < ((xDivisao * 3) - 1))
                        {
                            xExames3 = xExames3 + " /n " + lst_Exames.Items[xAux].Text;
                        }
                        else
                        {
                            xExames4 = xExames4 + " /n " + lst_Exames.Items[xAux].Text;
                        }


                    }

                }


            }

            if (chk_Basico.Checked == true)
            {
                xBasico = "1";
            }

            if (rd_Admissao.Checked == true)
            {
                xTipo = "A";
            }
            else if (rd_Demissional.Checked == true)
            {
                xTipo = "D";
            }
            else if ( rd_Mudanca.Checked == true)
            {
                xTipo = "M";
            }
            else if ( rd_Outro.Checked == true)
            {
                xTipo = "O";
            }
            else if ( rd_Periodico.Checked == true)
            {
                xTipo = "P";
            }
            else if ( rd_Retorno.Checked == true)
            {
                xTipo = "R";
            }


            int xTipoExame = 0;

            if (xTipo.ToUpper().Trim() == "A")
            {
                xTipoExame = 1;
            }
            else if (xTipo.ToUpper().Trim() == "D")
            {
                xTipoExame = 2;
            }
            else if (xTipo.ToUpper().Trim() == "M")
            {
                xTipoExame = 3;
            }
            else if (xTipo.ToUpper().Trim() == "P")
            {
                xTipoExame = 4;
            }
            else if (xTipo.ToUpper().Trim() == "R")
            {
                xTipoExame = 5;
            }




            //bloquear demissionais prajna
            //if (xTipo.ToUpper().Trim() == "D")
            //{
            //    if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().IndexOf("Prajna") > 0)
            //    {

            //        MsgBox1.Show("Ilitera.Net", "Prezado cliente, para agendamento de exame demissional favor entrar em contato com nossa central de atendimento: centraldeatendimento@5aessence.com.br", null,
            //        new EO.Web.MsgBoxButton("OK"));
            //        return;

            //    }
            //}




            ExameBase rexame = new ExameBase();
            

            rexame.Find(" IDEMPREGADO = " + Request["IdEmpregado"].ToString() + " and IdExameDicionario = " + xTipoExame.ToString() + " and convert( char(10),DataExame,103 ) = '" + txt_Data.Text + "' ");

            

            if (rexame.Id != 0)
            {
                MsgBox1.Show("Ilitera.Net", "ASO já foi criado para este tipo de exame e data.", null,
                new EO.Web.MsgBoxButton("OK"));
                return;
            }




            //checar EY - ver se existe algum exame clínico "em espera" e "sem resultado" aberto, criar rotina que devolva data e tipo de exame existente
            // alerta exibe a data e tipo de exame e pergunta se pode substituir por um novo exame.  Apago o exame anterior e continuo
            if (Ilitera.Data.SQLServer.EntitySQLServer._LocalServer.ToString().ToUpper().IndexOf("EY") > 0)
            {
                Entities.Usuario user2 = (Entities.Usuario)Session["usuarioLogado"];

                string xUsuario = user2.NomeUsuario.Trim().ToUpper();

                if (xUsuario != "EY_LUCAS" && xUsuario != "EY_ALBERTO" && xUsuario != "EY_RENAN" && xUsuario != "EY_VANESSA")
                {

                    System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");
                    string xResposta = "";
                    int zExames = 0;

                    ArrayList nExame = new ExameBase().Find(" IDEMPREGADO = " + Request["IdEmpregado"].ToString() + " and IdExameDicionario in ( 1,2,3,4,5 )  and  DataExame < convert( smalldatetime, '" + txt_Data.Text + "', 103 ) and IndResultado  not in ( 1, 2 )  ");


                    foreach (ExameBase rexame2 in nExame)
                    {
                        if (rexame2.Id != 0)
                        {
                            rexame2.IdExameDicionario.Find();
                            xResposta = xResposta + "Exame " + rexame2.IdExameDicionario.Nome + " - " + rexame2.DataExame.ToString("dd/MM/yyyy", ptBr) + " | ";
                            zExames = zExames + 1;
                        }

                    }


                    if (xResposta != "")
                    {
                        if (zExames == 1)
                        {
                            MsgBox2.Show("Ilitera.Net", "Existe exame criado e que ainda não foi realizado. Gostaria de substituí-lo pelo exame com a data indicada ? " +
                                 System.Environment.NewLine + "Exames localizados: " + xResposta, null,
                                 new EO.Web.MsgBoxButton("Não realizar o exame", null, "Delete-2"),
                                 new EO.Web.MsgBoxButton("Prosseguir no agendamento do exame", null, "Prosseguir no agendamento do exame-2"));
                            return;

                        }

                        else
                        {
                            MsgBox2.Show("Ilitera.Net", "Existem exames criados e que ainda não foram realizados. Gostaria de substituí-los pelo exame com a data indicada ? " +
                                         System.Environment.NewLine + xResposta, null,
                                         new EO.Web.MsgBoxButton("Não realizar o exame", null, "Delete-2"),
                                         new EO.Web.MsgBoxButton("Prosseguir no agendamento do exame", null, "Prosseguir no agendamento do exame-2"));
                            return;
                        }

                    }
                }
            }



            cliente.Find(System.Convert.ToInt32(Request["IdEmpresa"].ToString()));

            //se demissional, deve ter 90 dias de diferença pelo menos, senao aviso
            //se periódico, deve ter 90 dias de diferença pelo menos, senao aviso
            if (xTipo.ToUpper().Trim() == "D" || xTipo.ToUpper().Trim() == "P")
            {

                Ilitera.Common.Juridica zJuridica = new Ilitera.Common.Juridica();
                zJuridica.Find(System.Convert.ToInt32(Request["IdEmpresa"].ToString()));
                int zDiasGrauRisco = 90;

                if (zJuridica.GetCnaeSesmt().GrauRisco < 3)
                    zDiasGrauRisco = 135;
                else
                    zDiasGrauRisco = 90;


                //ExameBase rexame2 = new ExameBase();
                //rexame2.Find(" IDEMPREGADO = " + Request["IdEmpregado"].ToString() + " and IdExameDicionario = " + xTipoExame.ToString() + " and  datediff(day, DataExame, convert( smalldatetime,'" + txt_Data.Text + "', 103 )) <= 90 and IndResultado <> 0 ");
                ArrayList nExame = new ExameBase().Find(" IDEMPREGADO = " + Request["IdEmpregado"].ToString() + " and IdExameDicionario in ( 1,2,3,4,5 )  and  datediff(day, DataExame, convert( smalldatetime,'" + txt_Data.Text + "', 103 )) <= " + zDiasGrauRisco + " and IndResultado  not in ( 0, 3 )  ");

                foreach (ExameBase rexame2 in nExame)
                {
                    if (rexame2.Id != 0)
                    {

                        if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().IndexOf("Prajna") > 0)
                        {
                            if (xTipo.ToUpper().Trim() == "D")
                            {
                                //MsgBox1.Show("Ilitera.Net", "O último exame clínico realizado possibilita a demissão deste colaborador sem a necessidade de exame demissional. " +
                                //             "Caso deseje realizá-lo mesmo assim, favor manter contato com a central de atendimento no e-mail centraldeatendimento@5aessence.com.br", null,
                                //new EO.Web.MsgBoxButton("Ok", null, "Delete"),
                                //new EO.Web.MsgBoxButton("Saiba mais", null, "Saiba mais"));
                                //return;
                                MsgBox1.Show("Ilitera.Net", "O exame demissional somente é obrigatório desde que o último exame médico ocupacional, seja de que natureza for - admissional, retorno ao trabalho, mudança de risco ou mesmo o periódico - " +
                                              "tenha sido realizado há mais de 135 ou 90 dias, sendo a primeira opção de 135 dias para as empresas com grau de risco 1 ou 2 e 90 dias para as empresas com grau de risco equivalente a 3 ou 4. " + System.Environment.NewLine +
                                              "Os exame demissionais somente serão obrigatórios se as condições especificadas no parágrafo anterior forem preenchidas.  O exame clínico demissional não é obrigatório em quaisquer circunstâncias, " +
                                              "bastando a demissão do colaborador para que se torne necessário.  Para sua relização deverá haver duas condições obrigatórias, ou sejam, a demissão do colaborador e a não realização de exame clínico " +
                                              "anterior nos prazos estabelecidos nos dois itens da norma legal, 135 ou 90 dias conforme o caso. Dúvidas, entrar em contato com a central de atendimento no e-mail centraldeatendimento@essencenet.com.br", null,
                                 new EO.Web.MsgBoxButton("Não quero realizar o Exame Demissional", null, "Fechar"),
                                 new EO.Web.MsgBoxButton("Quero Agendar o Exame mesmo assim.", null, "Quero Agendar o Exame mesmo assim."));
                                return;

                            }
                            else
                            {

                                //MsgBox1.Show("Ilitera.Net", "Último exame dentro da validade, caso deseje realizá-lo mesmo assim, favor manter contato com a central de atendimento no e-mail centraldeatendimento@5aessence.com.br", null,
                                //new EO.Web.MsgBoxButton("OK"));
                                //return;
                                MsgBox1.Show("Ilitera.Net", "Último exame dentro da validade. Dúvidas, entrar em contato com a central de atendimento no e-mail centraldeatendimento@essencenet.com.br", null,
                                                               new EO.Web.MsgBoxButton("Não quero realizar o exame", null, "Fechar"),
                                                               new EO.Web.MsgBoxButton("Quero Agendar o Exame mesmo assim.", null, "Quero Agendar o Exame mesmo assim."));
                                return;

                            }
                        }
                        else if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("COTIA") > 0 || Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("GLOBAL") > 0)
                        {
                            if (xTipo.ToUpper().Trim() == "D")
                            {
                                if (cliente.Bloquear_ASO_Planejamento == false)
                                {
                                    MsgBox1.Show("Ilitera.Net", "O exame demissional somente é obrigatório desde que o último exame médico ocupacional, seja de que natureza for - admissional, retorno ao trabalho, mudança de risco ou mesmo o periódico - " +
                                              "tenha sido realizado há mais de 135 ou 90 dias, sendo a primeira opção de 135 dias para as empresas com grau de risco 1 ou 2 e 90 dias para as empresas com grau de risco equivalente a 3 ou 4. " + System.Environment.NewLine +
                                              "Os exame demissionais somente serão obrigatórios se as condições especificadas no parágrafo anterior forem preenchidas.  O exame clínico demissional não é obrigatório em quaisquer circunstâncias, " +
                                              "bastando a demissão do colaborador para que se torne necessário.  Para sua relização deverá haver duas condições obrigatórias, ou sejam, a demissão do colaborador e a não realização de exame clínico " +
                                              "anterior nos prazos estabelecidos nos dois itens da norma legal, 135 ou 90 dias conforme o caso. Dúvidas, entrar em contato com a central de atendimento no e-mail medicina.sp.cot@ilitera.com.br", null,
                                 new EO.Web.MsgBoxButton("Não quero realizar o Exame Demissional", null, "Fechar"),
                                 new EO.Web.MsgBoxButton("Quero Agendar o Exame mesmo assim.", null, "Quero Agendar o Exame mesmo assim."));
                                    return;
                                }
                                else
                                {
                                    MsgBox1.Show("Ilitera.Net", "O exame demissional somente é obrigatório desde que o último exame médico ocupacional, seja de que natureza for - admissional, retorno ao trabalho, mudança de risco ou mesmo o periódico - " +
                                            "tenha sido realizado há mais de 135 ou 90 dias, sendo a primeira opção de 135 dias para as empresas com grau de risco 1 ou 2 e 90 dias para as empresas com grau de risco equivalente a 3 ou 4. " + System.Environment.NewLine +
                                            "Os exame demissionais somente serão obrigatórios se as condições especificadas no parágrafo anterior forem preenchidas.  O exame clínico demissional não é obrigatório em quaisquer circunstâncias, " +
                                            "bastando a demissão do colaborador para que se torne necessário.  Para sua relização deverá haver duas condições obrigatórias, ou sejam, a demissão do colaborador e a não realização de exame clínico " +
                                            "anterior nos prazos estabelecidos nos dois itens da norma legal, 135 ou 90 dias conforme o caso. Dúvidas, entrar em contato com a central de atendimento no e-mail atendimento@ilitera.com.br", null,
                                            new EO.Web.MsgBoxButton("Ok", null, "Delete"));
                                    return;
                                }

                            }
                            else
                            {

                                if (cliente.Bloquear_ASO_Planejamento == false)
                                {
                                    MsgBox1.Show("Ilitera.Net", "Último exame dentro da validade. Dúvidas, entrar em contato com a central de atendimento no e-mail atendimento@ilitera.com.br", null,
                                                new EO.Web.MsgBoxButton("Não quero realizar o exame", null, "Fechar"),
                                                new EO.Web.MsgBoxButton("Quero Agendar o Exame mesmo assim.", null, "Quero Agendar o Exame mesmo assim."));
                                    return;
                                }
                                else
                                {
                                    MsgBox1.Show("Ilitera.Net", "Último exame dentro da validade. Dúvidas, entrar em contato com a central de atendimento no e-mail atendimento@ilitera.com.br", null,
                                                                   new EO.Web.MsgBoxButton("Fechar", null, "Fechar"));
                                    //new EO.Web.MsgBoxButton("Não quero realizar o exame", null, "Delete"),
                                    //new EO.Web.MsgBoxButton("Quero Agendar o Exame mesmo assim", null, "Quero Agendar o Exame mesmo assim"));
                                    return;
                                }
                                //return;

                            }
                        }
                        else
                        {
                          

                            if (xTipo.ToUpper().Trim() == "D")
                            {
                                if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().IndexOf("Daiti") > 0)
                                {
                                    //MsgBox1.Show("Ilitera.Net", "O último exame clínico realizado possibilita a demissão deste colaborador sem a necessidade de exame demissional. " +
                                    //             "Caso deseje realizá-lo mesmo assim, favor manter contato com a central de atendimento no e-mail recepcao.sp.dai@ilitera.com.br", null,
                                    //new EO.Web.MsgBoxButton("Ok", null, "Delete"),
                                    //new EO.Web.MsgBoxButton("Saiba mais", null, "Saiba mais"));
                                    MsgBox1.Show("Ilitera.Net", "O exame demissional somente é obrigatório desde que o último exame médico ocupacional, seja de que natureza for - admissional, retorno ao trabalho, mudança de risco ou mesmo o periódico - " +
                                                                                "tenha sido realizado há mais de 135 ou 90 dias, sendo a primeira opção de 135 dias para as empresas com grau de risco 1 ou 2 e 90 dias para as empresas com grau de risco equivalente a 3 ou 4. " + System.Environment.NewLine +
                                                                                "Os exame demissionais somente serão obrigatórios se as condições especificadas no parágrafo anterior forem preenchidas.  O exame clínico demissional não é obrigatório em quaisquer circunstâncias, " +
                                                                                "bastando a demissão do colaborador para que se torne necessário.  Para sua relização deverá haver duas condições obrigatórias, ou sejam, a demissão do colaborador e a não realização de exame clínico " +
                                                                                "anterior nos prazos estabelecidos nos dois itens da norma legal, 135 ou 90 dias conforme o caso. Dúvidas, entrar em contato com a central de atendimento no e-mail agendamento.sp.dai@ilitera.com.br", null,
                                                                                new EO.Web.MsgBoxButton("Ok", null, "Delete"));
                                    //new EO.Web.MsgBoxButton("Não quero realizar o Exame Demissional", null, "Fechar"),
                                    //                               new EO.Web.MsgBoxButton("Quero Agendar o Exame mesmo assim.", null, "Quero Agendar o Exame mesmo assim."));
                                    return;
                                }
                                else
                                {
                                    //MsgBox1.Show("Ilitera.Net", "O último exame clínico realizado possibilita a demissão deste colaborador sem a necessidade de exame demissional. " +
                                    //             "Caso deseje realizá-lo mesmo assim, favor manter contato com a central de atendimento no e-mail atendimento@ilitera.com.br", null,
                                    //new EO.Web.MsgBoxButton("Ok", null, "Delete"),
                                    //new EO.Web.MsgBoxButton("Saiba mais", null, "Saiba mais"));
                                    MsgBox1.Show("Ilitera.Net", "O exame demissional somente é obrigatório desde que o último exame médico ocupacional, seja de que natureza for - admissional, retorno ao trabalho, mudança de risco ou mesmo o periódico - " +
                                                                                "tenha sido realizado há mais de 135 ou 90 dias, sendo a primeira opção de 135 dias para as empresas com grau de risco 1 ou 2 e 90 dias para as empresas com grau de risco equivalente a 3 ou 4. " + System.Environment.NewLine +
                                                                                "Os exame demissionais somente serão obrigatórios se as condições especificadas no parágrafo anterior forem preenchidas.  O exame clínico demissional não é obrigatório em quaisquer circunstâncias, " +
                                                                                "bastando a demissão do colaborador para que se torne necessário.  Para sua relização deverá haver duas condições obrigatórias, ou sejam, a demissão do colaborador e a não realização de exame clínico " +
                                                                                "anterior nos prazos estabelecidos nos dois itens da norma legal, 135 ou 90 dias conforme o caso. Dúvidas, entrar em contato com a central de atendimento no e-mail atendimento@ilitera.com.br", null,
                                                                                new EO.Web.MsgBoxButton("Ok", null, "Delete"));
                                                                   //new EO.Web.MsgBoxButton("Não quero realizar o Exame Demissional", null, "Delete"),
                                                                   //new EO.Web.MsgBoxButton("Quero Agendar o Exame mesmo assim.", null, "Quero Agendar o Exame mesmo assim."));
                                    return;
                                }
                                
                            }
                            else
                            {
                                if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().IndexOf("Daiti") > 0)
                                {
                                    //MsgBox1.Show("Ilitera.Net", "Último exame dentro da validade, caso deseje realizá-lo mesmo assim, favor manter contato com a central de atendimento no e-mail recepcao.sp.dai@ilitera.com.br", null,
                                    //new EO.Web.MsgBoxButton("Ok", null, "Delete"),
                                    //new EO.Web.MsgBoxButton("Saiba mais", null, "Saiba mais"));
                                    if (cliente.Bloquear_ASO_Planejamento == false)
                                    {
                                        MsgBox1.Show("Ilitera.Net", "Último exame dentro da validade. Dúvidas, entrar em contato com a central de atendimento no e-mail agendamento.sp.dai@ilitera.com.br", null,                                            
                                                                       new EO.Web.MsgBoxButton("Não quero realizar o exame", null, "Fechar"),
                                                                       new EO.Web.MsgBoxButton("Quero Agendar o Exame mesmo assim.", null, "Quero Agendar o Exame mesmo assim."));
                                        return;
                                    }
                                    else
                                    {
                                        MsgBox1.Show("Ilitera.Net", "Último exame dentro da validade. Dúvidas, entrar em contato com a central de atendimento no e-mail agendamento.sp.dai@ilitera.com.br", null,
                                                                     new EO.Web.MsgBoxButton("Fechar", null, "Fechar"));
                                        return;
                                    }

                                }
                                else
                                {
                                    //MsgBox1.Show("Ilitera.Net", "Último exame dentro da validade, caso deseje realizá-lo mesmo assim, favor manter contato com a central de atendimento no e-mail atendimento@ilitera.com.br", null,
                                    //new EO.Web.MsgBoxButton("OK"));
                                    if (cliente.Bloquear_ASO_Planejamento == false)
                                    {
                                        MsgBox1.Show("Ilitera.Net", "Último exame dentro da validade. Dúvidas, entrar em contato com a central de atendimento no e-mail atendimento@ilitera.com.br", null,
                                                    new EO.Web.MsgBoxButton("Não quero realizar o exame", null, "Fechar"),
                                                    new EO.Web.MsgBoxButton("Quero Agendar o Exame mesmo assim.", null, "Quero Agendar o Exame mesmo assim."));
                                        return;
                                    }
                                    else
                                    {
                                        MsgBox1.Show("Ilitera.Net", "Último exame dentro da validade. Dúvidas, entrar em contato com a central de atendimento no e-mail atendimento@ilitera.com.br", null,
                                                                       new EO.Web.MsgBoxButton("Fechar", null, "Fechar"));
                                        //new EO.Web.MsgBoxButton("Não quero realizar o exame", null, "Delete"),
                                        //new EO.Web.MsgBoxButton("Quero Agendar o Exame mesmo assim", null, "Quero Agendar o Exame mesmo assim"));
                                        return;
                                    }
                                    //return;

                                }
                                
                            }

                        }
                    }

                }



                if (xTipo.ToUpper().Trim() == "P")
                {

                    int zDias_Desconsiderar = 0;

                    //dias desconsiderar dos exames complementares vai valer para validar exame fora do planejamento
                    if (cliente.Ativar_DesconsiderarCompl == true)
                    {
                        if (cliente.Dias_Desconsiderar > 0)
                        {
                            zDias_Desconsiderar = cliente.Dias_Desconsiderar;
                        }
                    }


                    Ilitera.Data.Clientes_Funcionarios xPlan = new Ilitera.Data.Clientes_Funcionarios();

                    if (xPlan.Buscar_Planejamento_Exame_Colaborador(System.Convert.ToInt32(Request["IdEmpregado"].ToString()), 4, txt_Data.Text.Trim(), zDias_Desconsiderar) == false)
                    {
                        if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().IndexOf("Prajna") > 0)
                        {
                            //MsgBox1.Show("Ilitera.Net", "Exame fora do planejamento, caso deseje realizá-lo mesmo assim, favor manter contato com a central de atendimento no e-mail centraldeatendimento@5aessence.com.br", null,
                            //new EO.Web.MsgBoxButton("OK"));
                            //return;
                            if (cliente.Bloquear_ASO_Planejamento == false)
                            {

                                MsgBox1.Show("Ilitera.Net", "Exame fora do planejamento. Dúvidas, entrar em contato com a central de atendimento no e-mail centraldeatendimento@essencenet.com.br", null,
                                                               new EO.Web.MsgBoxButton("Não quero realizar o exame", null, "Fechar"),
                                                               new EO.Web.MsgBoxButton("Quero Agendar o Exame mesmo assim.", null, "Quero Agendar o Exame mesmo assim."));
                                return;
                            }
                            else
                            {
                                MsgBox1.Show("Ilitera.Net", "Exame fora do planejamento. Dúvidas, entrar em contato com a central de atendimento no e-mail centraldeatendimento@essencenet.com.br", null,
                                                                new EO.Web.MsgBoxButton("Fechar", null, "Fechar"));
                                //new EO.Web.MsgBoxButton("Não quero realizar o exame", null, "Delete"),
                                //new EO.Web.MsgBoxButton("Quero Agendar o Exame mesmo assim", null, "Quero Agendar o Exame mesmo assim"));
                                return;

                            }
                        }
                        else
                        {
                            if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().IndexOf("Daiti") > 0)
                            {
                                //MsgBox1.Show("Ilitera.Net", "Exame fora do planejamento, caso deseje realizá-lo mesmo assim, favor manter contato com a central de atendimento no e-mail recepcao.sp.dai@ilitera.com.br", null,
                                //new EO.Web.MsgBoxButton("Ok", null, "Delete"),
                                //new EO.Web.MsgBoxButton("Saiba mais", null, "Saiba mais"));
                                if (cliente.Bloquear_ASO_Planejamento == false)
                                {
                                    //MsgBox1.Show("Ilitera.Net", "Exame fora do planejamento. Dúvidas, entrar em contato com a central de atendimento no e-mail agendamento.sp.dai@ilitera.com.br", null,
                                    //    new EO.Web.MsgBoxButton("Fechar", null, "Fechar"));
                                    ////new EO.Web.MsgBoxButton("Não quero realizar o exame", null, "Fechar"),
                                    //                               //new EO.Web.MsgBoxButton("Quero Agendar o Exame mesmo assim.", null, "Quero Agendar o Exame mesmo assim."));
                                    MsgBox1.Show("Ilitera.Net", "Último exame dentro da validade. Dúvidas, entrar em contato com a central de atendimento no e-mail agendamento.sp.dai@ilitera.com.br", null,
                                                new EO.Web.MsgBoxButton("Não quero realizar o exame", null, "Fechar"),
                                                new EO.Web.MsgBoxButton("Quero Agendar o Exame mesmo assim.", null, "Quero Agendar o Exame mesmo assim."));
                                    return;
                                }
                                else
                                {
                                    MsgBox1.Show("Ilitera.Net", "Exame fora do planejamento. Dúvidas, entrar em contato com a central de atendimento no e-mail agendamento.sp.dai@ilitera.com.br", null,
                                                                 new EO.Web.MsgBoxButton("Fechar", null, "Fechar"));
                                    return;
                                }

                            }
                            //else if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().IndexOf("Cotia") > 0)
                            //{
                            //    //MsgBox1.Show("Ilitera.Net", "Exame fora do planejamento, caso deseje realizá-lo mesmo assim, favor manter contato com a central de atendimento no e-mail recepcao.sp.dai@ilitera.com.br", null,
                            //    //new EO.Web.MsgBoxButton("Ok", null, "Delete"),
                            //    //new EO.Web.MsgBoxButton("Saiba mais", null, "Saiba mais"));
                            //    if (cliente.Bloquear_ASO_Planejamento == false)
                            //    {
                            //        MsgBox1.Show("Ilitera.Net", "Exame fora do planejamento. Dúvidas, entrar em contato com a central de atendimento no e-mail medicina.sp.cot@ilitera.com.br", null,
                            //                                   new EO.Web.MsgBoxButton("Não quero realizar o exame", null, "Fechar"),
                            //                                   new EO.Web.MsgBoxButton("Quero Agendar o Exame mesmo assim.", null, "Quero Agendar o Exame mesmo assim."));

                            //        //new EO.Web.MsgBoxButton("Fechar", null, "Fechar"));
                            //        //new EO.Web.MsgBoxButton("Não quero realizar o exame", null, "Fechar"),
                            //        //new EO.Web.MsgBoxButton("Quero Agendar o Exame mesmo assim.", null, "Quero Agendar o Exame mesmo assim."));
                            //        return;
                            //    }
                            //    else
                            //    {
                            //        MsgBox1.Show("Ilitera.Net", "Exame fora do planejamento. Dúvidas, entrar em contato com a central de atendimento no e-mail medicina.sp.cot@ilitera.com.br", null,
                            //                                     new EO.Web.MsgBoxButton("Fechar", null, "Fechar"));
                            //        return;
                            //    }

                            //}                            
                            else
                            {
                                //MsgBox1.Show("Ilitera.Net", "Exame fora do planejamento, caso deseje realizá-lo mesmo assim, favor manter contato com a central de atendimento no e-mail atendimento@ilitera.com.br", null,
                                //new EO.Web.MsgBoxButton("OK"));
                                if (cliente.Bloquear_ASO_Planejamento == false)
                                {
                                    MsgBox1.Show("Ilitera.Net", "Último exame dentro da validade. Dúvidas, entrar em contato com a central de atendimento no e-mail atendimento@ilitera.com.br", null,
                                                new EO.Web.MsgBoxButton("Não quero realizar o exame", null, "Fechar"),
                                                new EO.Web.MsgBoxButton("Quero Agendar o Exame mesmo assim.", null, "Quero Agendar o Exame mesmo assim."));
                                    return;
                                }
                                else
                                {
                                    MsgBox1.Show("Ilitera.Net", "Último exame dentro da validade. Dúvidas, entrar em contato com a central de atendimento no e-mail atendimento@ilitera.com.br", null,
                                                                   new EO.Web.MsgBoxButton("Fechar", null, "Fechar"));
                                    //new EO.Web.MsgBoxButton("Não quero realizar o exame", null, "Delete"),
                                    //new EO.Web.MsgBoxButton("Quero Agendar o Exame mesmo assim", null, "Quero Agendar o Exame mesmo assim"));
                                    return;
                                }
                            

                            }                            
                        }
                    }
                }


            
            }



            string xAptidao = "";

         


            if (chk_apt_Altura.Checked == true)       xAptidao = xAptidao + "A";
            if (chk_apt_Confinado.Checked == true)    xAptidao = xAptidao + "C";
            if (chk_apt_Eletricidade.Checked == true) xAptidao = xAptidao + "E";
            if (chk_apt_Submersas.Checked == true)    xAptidao = xAptidao + "S";
            if (chk_apt_Transportes.Checked == true)  xAptidao = xAptidao + "T";
            if (chk_apt_Aquaviarios.Checked == true)  xAptidao = xAptidao + "Q";
            if (chk_apt_Alimento.Checked == true)     xAptidao = xAptidao + "M";
            if (chk_Apt_Brigadista.Checked == true)   xAptidao = xAptidao + "B";
            if (chk_Apt_Socorrista.Checked == true)   xAptidao = xAptidao + "R";
            if (chk_Apt_Respiradores.Checked == true) xAptidao = xAptidao + "P";

            //Session["Exames"] = xExames;

            //salvar dados em tabela da guia gerada
            //empresa
            //colaborador
            //tipo de exame
            //clinica
            //data
            //hora
            //exames
            Entities.Usuario user = (Entities.Usuario)Session["usuarioLogado"];



            //pegar data de planejamento + "|" + Data Ultimo exame
            string rData = "";
            Ilitera.Data.Clientes_Funcionarios xPlan2 = new Ilitera.Data.Clientes_Funcionarios();
            rData = xPlan2.Buscar_Data_Planejamento_Exame_Colaborador(System.Convert.ToInt32(Request["IdEmpregado"].ToString()), 4, txt_Data.Text.Trim());



            if (chk_Toxicologico.Checked == true)
            {
                if (xExames.Trim() == "") xExames = "   Toxicológico";
                else if (xExames2.Trim() == "") xExames2 = "   Toxicológico";
                else if (xExames3.Trim() == "") xExames3 = "   Toxicológico";
                else if (xExames4.Trim() == "") xExames4 = "   Toxicológico";
                else xExames4 = xExames4 + "   , Toxicológico";
            }


            Ilitera.Data.PPRA_EPI xGuia = new Ilitera.Data.PPRA_EPI();
            xGuia.Salvar_Dados_Guia_Encaminhamento(System.Convert.ToInt32(Request["IdEmpresa"].ToString()), System.Convert.ToInt32(Request["IdEmpregado"].ToString()), xTipo, xExames + "|" + xExames2 + "|" + xExames3 + "|" + xExames4, txt_Data.Text, txt_Hora.Text, cmb_Clinicas.Items[cmb_Clinicas.SelectedIndex].ToString(), user.IdUsuario, "N", rData.Substring(0, 10), rData.Substring(11), txt_Obs.Text.Trim());



            //se demissional da Prajna,  colocar data do ASO na data de demissão de colaborador
            if ( Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().Trim().ToUpper() == "OPSA" ||
                 Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().Trim().ToUpper() == "OPSA_VIA" ||
                 Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().Trim().ToUpper() == "OPSA_UNO" ||
                 Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().Trim().ToUpper() == "OPSA_SHO" ||
                 Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().Trim().ToUpper() == "OPSA_PRO" ||
                 (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("SAFETY") > 0 && Ilitera.Data.SQLServer.EntitySQLServer._LocalServer.ToString().ToUpper().IndexOf("FOCUS") > 0))  
            {
                if (xTipo.ToUpper().Trim() == "D")
                {
                    //System.Convert.ToInt32(Request["IdEmpregado"].ToString())
                    //txt_Data.Text
                    Ilitera.Data.PPRA_EPI xDem = new Ilitera.Data.PPRA_EPI();

                    //xDem.Guia_Demissao_Colaborador(System.Convert.ToInt32(Request["IdEmpregado"].ToString()), txt_Data.Text);

                    if ( txt_Demissao.Text.Trim()!="")
                        xDem.Guia_Demissao_Colaborador_Obras(System.Convert.ToInt32(Request["IdEmpregado"].ToString()), txt_Demissao.Text);
                    else
                       xDem.Guia_Demissao_Colaborador_Obras(System.Convert.ToInt32(Request["IdEmpregado"].ToString()), txt_Data.Text);

                }
            }
            else if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().IndexOf("Prajna") > 0 )
            {
                if (xTipo.ToUpper().Trim() == "D")
                {
                    //System.Convert.ToInt32(Request["IdEmpregado"].ToString())
                    //txt_Data.Text
                    Ilitera.Data.PPRA_EPI xDem = new Ilitera.Data.PPRA_EPI();

                    //xDem.Guia_Demissao_Colaborador_Obras(System.Convert.ToInt32(Request["IdEmpregado"].ToString()), txt_Data.Text);
                    if (txt_Demissao.Text.Trim() != "")
                        xDem.Guia_Demissao_Colaborador_Obras(System.Convert.ToInt32(Request["IdEmpregado"].ToString()), txt_Demissao.Text);
                    else
                        xDem.Guia_Demissao_Colaborador_Obras(System.Convert.ToInt32(Request["IdEmpregado"].ToString()), txt_Data.Text);


                }
                //else if ( xTipo.ToUpper().Trim() == "R")
                //{
                //    //checar se há afastamento INSS aberto
                //    //  em caso positivo,  perguntar se quer colocar data final igual a data do exame 
                //    ArrayList nAfast = new Afastamento().Find(" IDEMPREGADO = " + Request["IdEmpregado"].ToString() + " and DataVolta is null and ( INSS is not null and INSS = 1 ) ");

                //    foreach (Afastamento zAfast in nAfast)
                //    {


                //    }



                //}
            }


            Cliente xCliente = new Cliente();
            xCliente.Find( System.Convert.ToInt32( Request["IdEmpresa"].ToString() ));



            // criar exames complementares do ASO, se estes não existirem ainda.  Colocar como em espera
            if (xCliente.Gerar_Complementares_Guia == true)
            {


                Ilitera.Opsa.Data.Empregado nEmpregado = new Ilitera.Opsa.Data.Empregado(System.Convert.ToInt32(Request["IdEmpregado"].ToString()));

                Ilitera.Common.Juridica xClin = new Ilitera.Common.Juridica();
                xClin.Find(" IdJuridica = " + System.Convert.ToInt32(cmb_Clinicas.SelectedValue.ToString()));

                System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");

                for (int nCont = 0; nCont < lst_IdExames.Items.Count; nCont++)
                {

                    //checar se exame já existe
                    Int32 xIdExameDicionario = System.Convert.ToInt32(lst_IdExames.Items[nCont].ToString());

                    
                    Complementar xCompl = new Complementar();

                    ExameBase xCompl2 = new ExameBase();

                    DataSet alCompl = new DataSet();
                    alCompl = xCompl2.Get(" IdEmpregado = " + Request["IdEmpregado"].ToString() + " and IdExameDicionario = " + xIdExameDicionario.ToString() + " and convert( char(10), DataExame, 103 ) = '" + txt_Data.Text + "'");


                    //xCompl.Find(" IdEmpregado = " + Request["IdEmpregado"].ToString() + " and IdExameDicionario = " + xIdExameDicionario.ToString() + " and convert( char(10), DataExame, 103 ) = '" + txt_Data.Text + "'");

                    //if (xCompl.Id == 0)
                    //{

                    if (alCompl.Tables[0].Rows.Count == 0)
                    {

                        //sem essa busca vazia, gera erro na hora de salvar
                        xCompl.Find(" IdEmpregado = " + Request["IdEmpregado"].ToString() + " and IdExameDicionario = " + xIdExameDicionario.ToString() + " and convert( char(10), DataExame, 103 ) = '" + txt_Data.Text + "'");

                        ExameDicionario xED = new ExameDicionario();
                        xED.Find(" IdExameDicionario = " + xIdExameDicionario.ToString());

                        if (xED.Nome.ToUpper().Trim() == "AUDIOMETRIA")
                        {
                            Audiometria xAud = new Audiometria();
                            xAud.IdExameDicionario = xED;
                            xAud.IdEmpregado = nEmpregado;
                            xAud.DataExame = System.Convert.ToDateTime(txt_Data.Text, ptBr);
                            xAud.IndResultado = (int)Ilitera.Opsa.Data.ResultadoExame.EmEspera;
                            xAud.IdJuridica = xClin;

                            PagamentoClinica xPag = new PagamentoClinica();
                            xAud.IdPagamentoClinica = xPag;

                            xAud.IndAudiometriaTipo = 0;

                            Medico xMedico = new Medico();
                            xAud.IdMedico = xMedico;

                            ConvocacaoExame xConv = new ConvocacaoExame();
                            xAud.IdConvocacaoExame = xConv;

                            Audiometro xAudiometro = new Audiometro();
                            xAud.IdAudiometro = xAudiometro;

                            Ilitera.Common.Compromisso xcompr = new Ilitera.Common.Compromisso();
                            xAud.IdCompromisso = xcompr;

                            xAud.Save();
                        }
                        else if (xED.Nome.ToUpper().IndexOf("AVALIAÇÃO CL") < 0 && xED.Nome.ToUpper().IndexOf("AVALIACAO CL") < 0)
                        {
                            xCompl.IdExameDicionario = xED;
                            xCompl.IdEmpregado = nEmpregado;
                            xCompl.DataExame = System.Convert.ToDateTime(txt_Data.Text, ptBr);
                            xCompl.IndResultado = (int)Ilitera.Opsa.Data.ResultadoExame.EmEspera;
                            xCompl.IdJuridica = xClin;
                            xCompl.Save();
                        }

                    }



                }


                if (chk_Toxicologico.Checked == true)
                {
                    Complementar xCompl_Tox = new Complementar();
                    ExameBase xCompl2_Tox = new ExameBase();
                    DataSet alCompl_Tox = xCompl2_Tox.Get(" IdEmpregado = " + Request["IdEmpregado"].ToString() + " and IdExameDicionario = 100 and convert( char(10), DataExame, 103 ) = '" + txt_Data.Text + "'");

                    ExameDicionario xED_Tox = new ExameDicionario();
                    xED_Tox.Find(100);

                    if (alCompl_Tox.Tables[0].Rows.Count == 0)
                    {

                        //sem essa busca vazia, gera erro na hora de salvar
                        xCompl_Tox.Find(" IdEmpregado = " + Request["IdEmpregado"].ToString() + " and IdExameDicionario = 100 and convert( char(10), DataExame, 103 ) = '" + txt_Data.Text + "'");

                        xCompl_Tox.IdExameDicionario = xED_Tox;
                        xCompl_Tox.IdEmpregado = nEmpregado;
                        xCompl_Tox.DataExame = System.Convert.ToDateTime(txt_Data.Text, ptBr);
                        xCompl_Tox.IndResultado = (int)Ilitera.Opsa.Data.ResultadoExame.EmEspera;
                        xCompl_Tox.IdJuridica = xClin;
                        xCompl_Tox.Save();
                    }
                }

            }



            //OpenReport("DadosEmpresa", "RelatorioGuia.aspx?IliteraSystem=" + strAux.ToString() + strAux.ToString()
            //    + "&IdEmpresa=" + Request["IdEmpresa"] + "&IdEmpregado=" + Request["IdEmpregado"] + "&tiporelatorio=tudo" + "&Exames=" + xExames + "&Exames2=" + xExames2 + "&Exames3=" + xExames3 + "&Exames4=" + xExames4 +  "&IdClinica=" + cmb_Clinicas.SelectedValue.ToString() + "&Hora_Exame=" + txt_Hora.Text + "&Data_Exame=" + txt_Data.Text + "&Tipo=" + xTipo + "&Basico=" + xBasico + "&Obs=" + xObs , "RelatorioGuia", true);

            Session["Obs_Guia"] = xObs;

            if (xTipoGuia == 2)
            {
                OpenReport("DadosEmpresa", "RelatorioGuiaASO.aspx?IliteraSystem=" + strAux.ToString()
                + "&IdEmpresa=" + Request["IdEmpresa"] + "&IdEmpregado=" + Request["IdEmpregado"] + "&E1=" + xExames + "&E2=" + xExames2 + "&E3=" + xExames3 + "&E4=" + xExames4 + "&IdClinica=" + cmb_Clinicas.SelectedValue.ToString() + "&H_E=" + txt_Hora.Text + "&D_E=" + txt_Data.Text + "&Tipo=" + xTipo + "&Basico=" + xBasico + "&Mail=" + xEnvio_Email + "&TipoGuia=2" + "&ImpDt=" + xImpDt + "&Apt=" + xAptidao + "&Dem=" + txt_Demissao.Text, "RelatorioGuia", true);
            }
            else
            {
                OpenReport("DadosEmpresa", "RelatorioGuiaASO.aspx?IliteraSystem=" + strAux.ToString()
                + "&IdEmpresa=" + Request["IdEmpresa"] + "&IdEmpregado=" + Request["IdEmpregado"] + "&E1=" + xExames + "&E2=" + xExames2 + "&E3=" + xExames3 + "&E4=" + xExames4 + "&IdClinica=" + cmb_Clinicas.SelectedValue.ToString() + "&H_E=" + txt_Hora.Text + "&D_E=" + txt_Data.Text + "&Tipo=" + xTipo + "&Basico=" + xBasico + "&Mail=" + xEnvio_Email + "&TipoGuia=1" + "&ImpDt=" + xImpDt + "&Apt=" + xAptidao + "&Dem=" + txt_Demissao.Text, "RelatorioGuia", true);
            }


            //if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().IndexOf("Prajna") > 0)
            //{
            //    MsgBox1.Show("Ilitera.Net", "ATENÇÃO: " + System.Environment.NewLine +
            //                 "Importante: Recomendamos ligar para a clinica selecionada para confirmar o horário de atendimento no periodo de festas de final de ano 23/12 a 10/01", null,
            //    new EO.Web.MsgBoxButton("OK"));
            //    return;
            //}



        }





        protected void Gerar_Guia_ASO()
        {
            int xAux;
            string xExames = "";
            string xExames2 = "";
            string xExames3 = "";
            string xExames4 = "";
            string xTipo = "";
            string xBasico = "0";
            string xObs = "";
            int xCont = 0;
            string xEnvio_Email = "N";


            if (Validar_Data(txt_Data.Text) == false)
            {
                return;
            }


            if (rd_Outro.Checked == true)
            {
                MsgBox1.Show("Ilitera.Net", "Opção Outros não é válida para esse tipo de geração de documentos.", null,
                new EO.Web.MsgBoxButton("OK"));
                return;
            }


            string xEmail = lbl_email.Text.Trim();

            if (chk_eMail.Checked == true)
            {
                xEnvio_Email = "S";

                if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().IndexOf("Prajna") > 0 || Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().Trim() == "OPSA")
                {

                    if (xEmail == "")
                    {
                        MsgBox1.Show("Ilitera.Net", "Clínica não possui e-mail cadastrado.", null,
                        new EO.Web.MsgBoxButton("OK"));
                        return;
                    }

                    if (xEmail.IndexOf("@") < 0)
                    {
                        MsgBox1.Show("Ilitera.Net", "Clínica não possui e-mail válido cadastrado.", null,
                        new EO.Web.MsgBoxButton("OK"));
                        return;
                    }

                }
            }
            else
            {
                xEnvio_Email = "N";
            }

            string xImpDt = "S";

            if (chk_Data.Checked == false) xImpDt = "N";


            Guid strAux = Guid.NewGuid();



            xObs = txt_Obs.Text.Trim();


            if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().IndexOf("Prajna") > 0)
            {
                int xDivisao = 7;

                if (lst_Exames.Items.Count < 18)
                    xDivisao = 5;


                for (xAux = 0; xAux < lst_Exames.Items.Count; xAux++)
                {
                    if (lst_Exames.Items[xAux].Selected == true)
                    {
                        xCont++;

                        if (xCont < (xDivisao + 1))
                        {
                            xExames = xExames + " /n " + lst_Exames.Items[xAux].Text;
                        }
                        else if (xCont < ((xDivisao * 2) - 1))
                        {
                            xExames2 = xExames2 + " /n " + lst_Exames.Items[xAux].Text;
                        }
                        else if (xCont < ((xDivisao * 3) - 1))
                        {
                            xExames3 = xExames3 + " /n " + lst_Exames.Items[xAux].Text;
                        }
                        else
                        {
                            xExames4 = xExames4 + " /n " + lst_Exames.Items[xAux].Text;
                        }


                    }

                }
            }
            else
            {
                int xDivisao = 13;

                // if (lst_Exames.Items.Count < 18)
                // xDivisao = 5;

                for (xAux = 0; xAux < lst_Exames.Items.Count; xAux++)
                {
                    if (lst_Exames.Items[xAux].Selected == true)
                    {
                        xCont++;

                        if (xCont < (xDivisao + 1))
                        {
                            xExames = xExames + " /n " + lst_Exames.Items[xAux].Text;
                        }
                        else if (xCont < ((xDivisao * 2) - 1))
                        {
                            xExames2 = xExames2 + " /n " + lst_Exames.Items[xAux].Text;
                        }
                        else if (xCont < ((xDivisao * 3) - 1))
                        {
                            xExames3 = xExames3 + " /n " + lst_Exames.Items[xAux].Text;
                        }
                        else
                        {
                            xExames4 = xExames4 + " /n " + lst_Exames.Items[xAux].Text;
                        }


                    }

                }


            }

            if (chk_Basico.Checked == true)
            {
                xBasico = "1";
            }

            if (rd_Admissao.Checked == true)
            {
                xTipo = "A";
            }
            else if (rd_Demissional.Checked == true)
            {
                xTipo = "D";
            }
            else if (rd_Mudanca.Checked == true)
            {
                xTipo = "M";
            }
            else if (rd_Outro.Checked == true)
            {
                xTipo = "O";
            }
            else if (rd_Periodico.Checked == true)
            {
                xTipo = "P";
            }
            else if (rd_Retorno.Checked == true)
            {
                xTipo = "R";
            }


            int xTipoExame = 0;

            if (xTipo.ToUpper().Trim() == "A")
            {
                xTipoExame = 1;
            }
            else if (xTipo.ToUpper().Trim() == "D")
            {
                xTipoExame = 2;
            }
            else if (xTipo.ToUpper().Trim() == "M")
            {
                xTipoExame = 3;
            }
            else if (xTipo.ToUpper().Trim() == "P")
            {
                xTipoExame = 4;
            }
            else if (xTipo.ToUpper().Trim() == "R")
            {
                xTipoExame = 5;
            }


            //bloquear demissionais prajna
            //if (xTipo.ToUpper().Trim() == "D")
            //{
            //    if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().IndexOf("Prajna") > 0)
            //    {

            //        MsgBox1.Show("Ilitera.Net", "Prezado cliente, para agendamento de exame demissional favor entrar em contato com nossa central de atendimento: centraldeatendimento@5aessence.com.br", null,
            //        new EO.Web.MsgBoxButton("OK"));
            //        return;

            //    }
            //}


            ExameBase rexame = new ExameBase();


            rexame.Find(" IDEMPREGADO = " + Request["IdEmpregado"].ToString() + " and IdExameDicionario = " + xTipoExame.ToString() + " and convert( char(10),DataExame,103 ) = '" + txt_Data.Text + "' ");



            if (rexame.Id != 0)
            {
                MsgBox1.Show("Ilitera.Net", "ASO já foi criado para este tipo de exame e data.", null,
                new EO.Web.MsgBoxButton("OK"));
                return;
            }


            string xAptidao = "";



            if (chk_apt_Altura.Checked == true) xAptidao = xAptidao + "A";
            if (chk_apt_Confinado.Checked == true) xAptidao = xAptidao + "C";
            if (chk_apt_Eletricidade.Checked == true) xAptidao = xAptidao + "E";
            if (chk_apt_Submersas.Checked == true) xAptidao = xAptidao + "S";
            if (chk_apt_Transportes.Checked == true) xAptidao = xAptidao + "T";
            if (chk_apt_Aquaviarios.Checked == true) xAptidao = xAptidao + "Q";
            if (chk_apt_Alimento.Checked == true) xAptidao = xAptidao + "M";
            if (chk_Apt_Brigadista.Checked == true) xAptidao = xAptidao + "B";
            if (chk_Apt_Socorrista.Checked == true) xAptidao = xAptidao + "R";
            if (chk_Apt_Respiradores.Checked == true) xAptidao = xAptidao + "P";

            //Session["Exames"] = xExames;

            //salvar dados em tabela da guia gerada
            //empresa
            //colaborador
            //tipo de exame
            //clinica
            //data
            //hora
            //exames
            Entities.Usuario user = (Entities.Usuario)Session["usuarioLogado"];

            //pegar data de planejamento + "|" + Data Ultimo exame
            string rData = "";
            Ilitera.Data.Clientes_Funcionarios xPlan2 = new Ilitera.Data.Clientes_Funcionarios();
            rData = xPlan2.Buscar_Data_Planejamento_Exame_Colaborador(System.Convert.ToInt32(Request["IdEmpregado"].ToString()), 4, txt_Data.Text.Trim());


            Ilitera.Data.PPRA_EPI xGuia = new Ilitera.Data.PPRA_EPI();
            xGuia.Salvar_Dados_Guia_Encaminhamento(System.Convert.ToInt32(Request["IdEmpresa"].ToString()), System.Convert.ToInt32(Request["IdEmpregado"].ToString()), xTipo, xExames + "|" + xExames2 + "|" + xExames3 + "|" + xExames4, txt_Data.Text, txt_Hora.Text, cmb_Clinicas.Items[cmb_Clinicas.SelectedIndex].ToString(), user.IdUsuario, "S", rData.Substring(0, 10), rData.Substring(11), txt_Obs.Text.Trim());

            
            
            //se demissional da Prajna,  colocar data do ASO na data de demissão de colaborador
            if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().Trim().ToUpper() == "OPSA")
            {
                if (xTipo.ToUpper().Trim() == "D")
                {
                    //System.Convert.ToInt32(Request["IdEmpregado"].ToString())
                    //txt_Data.Text
                    Ilitera.Data.PPRA_EPI xDem = new Ilitera.Data.PPRA_EPI();

                    //xDem.Guia_Demissao_Colaborador(System.Convert.ToInt32(Request["IdEmpregado"].ToString()), txt_Data.Text);
                    xDem.Guia_Demissao_Colaborador_Obras(System.Convert.ToInt32(Request["IdEmpregado"].ToString()), txt_Data.Text);

                }
            }
            else if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().IndexOf("Prajna") > 0)
            {
                if (xTipo.ToUpper().Trim() == "D")
                {
                    //System.Convert.ToInt32(Request["IdEmpregado"].ToString())
                    //txt_Data.Text
                    Ilitera.Data.PPRA_EPI xDem = new Ilitera.Data.PPRA_EPI();

                    xDem.Guia_Demissao_Colaborador_Obras(System.Convert.ToInt32(Request["IdEmpregado"].ToString()), txt_Data.Text);

                }
            }



            
            Cliente xCliente = new Cliente();
            xCliente.Find(System.Convert.ToInt32(Request["IdEmpresa"].ToString()));

            // criar exames complementares do ASO, se estes não existirem ainda.  Colocar como em espera




            if (xCliente.Gerar_Complementares_Guia == true)
            {


                Ilitera.Opsa.Data.Empregado nEmpregado = new Ilitera.Opsa.Data.Empregado(System.Convert.ToInt32(Request["IdEmpregado"].ToString()));

                Ilitera.Common.Juridica xClin = new Ilitera.Common.Juridica();
                xClin.Find(" IdJuridica = " + System.Convert.ToInt32(cmb_Clinicas.SelectedValue.ToString()));

                for (int nCont = 0; nCont < lst_IdExames.Items.Count; nCont++)
                {

                    //checar se exame já existe
                    Int32 xIdExameDicionario = System.Convert.ToInt32(lst_IdExames.Items[nCont].ToString());

                    System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");

                    Complementar xCompl = new Complementar();

                    ExameBase xCompl2 = new ExameBase();
                    xCompl2.Find(" IdEmpregado = " + Request["IdEmpregado"].ToString() + " and IdExameDicionario = " + xIdExameDicionario.ToString() + " and convert( char(10), DataExame, 103 ) = '" + txt_Data.Text + "'");



                    if (xCompl.Id == 0)
                    {

                        ExameDicionario xED = new ExameDicionario();
                        xED.Find(" IdExameDicionario = " + xIdExameDicionario.ToString());

                        if (xED.Nome.ToUpper().Trim() == "AUDIOMETRIA")
                        {
                            Audiometria xAud = new Audiometria();
                            xAud.IdExameDicionario = xED;
                            xAud.IdEmpregado = nEmpregado;
                            xAud.DataExame = System.Convert.ToDateTime(txt_Data.Text, ptBr);
                            xAud.IndResultado = (int)Ilitera.Opsa.Data.ResultadoExame.EmEspera;
                            xAud.IdJuridica = xClin;

                            PagamentoClinica xPag = new PagamentoClinica();
                            xAud.IdPagamentoClinica = xPag;

                            xAud.IndAudiometriaTipo = 0;

                            Medico xMedico = new Medico();
                            xAud.IdMedico = xMedico;

                            ConvocacaoExame xConv = new ConvocacaoExame();
                            xAud.IdConvocacaoExame = xConv;

                            Audiometro xAudiometro = new Audiometro();
                            xAud.IdAudiometro = xAudiometro;

                            Ilitera.Common.Compromisso xcompr = new Ilitera.Common.Compromisso();
                            xAud.IdCompromisso = xcompr;

                            try
                            {
                                xAud.Save();
                            }
                            catch (Exception ex)
                            {

                            }
                            finally
                            {

                            }
                        }
                        else if (xED.Nome.ToUpper().IndexOf("AVALIAÇÃO CL") < 0 && xED.Nome.ToUpper().IndexOf("AVALIACAO CL") < 0)
                        {
                            xCompl.IdExameDicionario = xED;
                            xCompl.IdEmpregado = nEmpregado;
                            xCompl.DataExame = System.Convert.ToDateTime(txt_Data.Text, ptBr);
                            xCompl.IndResultado = (int)Ilitera.Opsa.Data.ResultadoExame.EmEspera;
                            xCompl.IdJuridica = xClin;
                            try
                            {
                                xCompl.Save();
                            }
                            catch( Exception ex)
                            {

                            }
                            finally
                            {

                            }
                        }

                    }







                }


            }

            //OpenReport("DadosEmpresa", "RelatorioGuia.aspx?IliteraSystem=" + strAux.ToString() + strAux.ToString()
            //    + "&IdEmpresa=" + Request["IdEmpresa"] + "&IdEmpregado=" + Request["IdEmpregado"] + "&tiporelatorio=tudo" + "&Exames=" + xExames + "&Exames2=" + xExames2 + "&Exames3=" + xExames3 + "&Exames4=" + xExames4 +  "&IdClinica=" + cmb_Clinicas.SelectedValue.ToString() + "&Hora_Exame=" + txt_Hora.Text + "&Data_Exame=" + txt_Data.Text + "&Tipo=" + xTipo + "&Basico=" + xBasico + "&Obs=" + xObs , "RelatorioGuia", true);

            Session["Obs_Guia"] = xObs;

            OpenReport("DadosEmpresa", "RelatorioGuiaASO.aspx?IliteraSystem=" + strAux.ToString()
    + "&IdEmpresa=" + Request["IdEmpresa"] + "&IdEmpregado=" + Request["IdEmpregado"] + "&E1=" + xExames + "&E2=" + xExames2 + "&E3=" + xExames3 + "&E4=" + xExames4 + "&IdClinica=" + cmb_Clinicas.SelectedValue.ToString() + "&H_E=" + txt_Hora.Text + "&D_E=" + txt_Data.Text + "&Tipo=" + xTipo + "&Basico=" + xBasico + "&Mail=" + xEnvio_Email + "&TipoGuia=" + lblTipoGuia.Text.Trim()  + "&ImpDt=" + xImpDt + "&Apt=" + xAptidao, "RelatorioGuia", true);





        }



        protected void btnEmp2Via_Click(object sender, EventArgs e)
        {

            int xAux;
            string xExames = "";
            string xExames2 = "";
            string xExames3 = "";
            string xExames4 = "";
            string xTipo = "";
            string xBasico = "0";
            string xObs = "";
            int xCont = 0;
            string xEnvio_Email = "2";


            if (Validar_Data(txt_Data.Text) == false)
            {
                return;
            }


            if (rd_Outro.Checked == true)
            {
                MsgBox1.Show("Ilitera.Net", "Opção Outros não é válida para esse tipo de geração de documentos.", null,
                new EO.Web.MsgBoxButton("OK"));
                return;
            }


            
            

            string xImpDt = "S";

            if (chk_Data.Checked == false) xImpDt = "N";


            Guid strAux = Guid.NewGuid();



            xObs = txt_Obs.Text.Trim();

            if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().IndexOf("Prajna") > 0)
            {
                int xDivisao = 7;

                if (lst_Exames.Items.Count < 18)
                    xDivisao = 5;


                for (xAux = 0; xAux < lst_Exames.Items.Count; xAux++)
                {
                    if (lst_Exames.Items[xAux].Selected == true)
                    {
                        xCont++;

                        if (xCont < (xDivisao + 1))
                        {
                            xExames = xExames + " /n " + lst_Exames.Items[xAux].Text;
                        }
                        else if (xCont < ((xDivisao * 2) - 1))
                        {
                            xExames2 = xExames2 + " /n " + lst_Exames.Items[xAux].Text;
                        }
                        else if (xCont < ((xDivisao * 3) - 1))
                        {
                            xExames3 = xExames3 + " /n " + lst_Exames.Items[xAux].Text;
                        }
                        else
                        {
                            xExames4 = xExames4 + " /n " + lst_Exames.Items[xAux].Text;
                        }


                    }

                }
            }
            else
            {
                int xDivisao = 11;

                // if (lst_Exames.Items.Count < 18)
                // xDivisao = 5;

                for (xAux = 0; xAux < lst_Exames.Items.Count; xAux++)
                {
                    if (lst_Exames.Items[xAux].Selected == true)
                    {
                        xCont++;

                        if (xCont < (xDivisao + 1))
                        {
                            xExames = xExames + " /n " + lst_Exames.Items[xAux].Text;
                        }
                        else if (xCont < ((xDivisao * 2) - 1))
                        {
                            xExames2 = xExames2 + " /n " + lst_Exames.Items[xAux].Text;
                        }
                        else if (xCont < ((xDivisao * 3) - 1))
                        {
                            xExames3 = xExames3 + " /n " + lst_Exames.Items[xAux].Text;
                        }
                        else
                        {
                            xExames4 = xExames4 + " /n " + lst_Exames.Items[xAux].Text;
                        }


                    }

                }


            }


            if (chk_Basico.Checked == true)
            {
                xBasico = "1";
            }

            if (rd_Admissao.Checked == true)
            {
                xTipo = "A";
            }
            else if (rd_Demissional.Checked == true)
            {
                xTipo = "D";
            }
            else if (rd_Mudanca.Checked == true)
            {
                xTipo = "M";
            }
            else if (rd_Outro.Checked == true)
            {
                xTipo = "O";
            }
            else if (rd_Periodico.Checked == true)
            {
                xTipo = "P";
            }
            else if (rd_Retorno.Checked == true)
            {
                xTipo = "R";
            }


            int xTipoExame = 0;

            if (xTipo.ToUpper().Trim() == "A")
            {
                xTipoExame = 1;
            }
            else if (xTipo.ToUpper().Trim() == "D")
            {
                xTipoExame = 2;
            }
            else if (xTipo.ToUpper().Trim() == "M")
            {
                xTipoExame = 3;
            }
            else if (xTipo.ToUpper().Trim() == "P")
            {
                xTipoExame = 4;
            }
            else if (xTipo.ToUpper().Trim() == "R")
            {
                xTipoExame = 5;
            }


            //bloquear demissionais prajna
            //if (xTipo.ToUpper().Trim() == "D")
            //{
            //    if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().IndexOf("Prajna") > 0)
            //    {

            //        MsgBox1.Show("Ilitera.Net", "Prezado cliente, para agendamento de exame demissional favor entrar em contato com nossa central de atendimento: centraldeatendimento@5aessence.com.br", null,
            //        new EO.Web.MsgBoxButton("OK"));
            //        return;

            //    }
            //}



            ExameBase rexame = new ExameBase();


            rexame.Find(" IDEMPREGADO = " + Request["IdEmpregado"].ToString() + " and IdExameDicionario = " + xTipoExame.ToString() + " and convert( char(10),DataExame,103 ) = '" + txt_Data.Text + "' ");


            if (rexame.Id == 0)
            {
                MsgBox1.Show("Ilitera.Net", "ASO não encontrado para este tipo de exame e data.", null,
                new EO.Web.MsgBoxButton("OK"));
                return;
            }

            //Session["Exames"] = xExames;

            //salvar dados em tabela da guia gerada
            //empresa
            //colaborador
            //tipo de exame
            //clinica
            //data
            //hora
            //exames
            Entities.Usuario user = (Entities.Usuario)Session["usuarioLogado"];


            //pegar data de planejamento + "|" + Data Ultimo exame
            string rData = "";
            Ilitera.Data.Clientes_Funcionarios xPlan2 = new Ilitera.Data.Clientes_Funcionarios();
            rData = xPlan2.Buscar_Data_Planejamento_Exame_Colaborador(System.Convert.ToInt32(Request["IdEmpregado"].ToString()), 4, txt_Data.Text.Trim());


            Ilitera.Data.PPRA_EPI xGuia = new Ilitera.Data.PPRA_EPI();
            xGuia.Salvar_Dados_Guia_Encaminhamento(System.Convert.ToInt32(Request["IdEmpresa"].ToString()), System.Convert.ToInt32(Request["IdEmpregado"].ToString()), xTipo, xExames + "|" + xExames2 + "|" + xExames3 + "|" + xExames4, txt_Data.Text, txt_Hora.Text, cmb_Clinicas.Items[cmb_Clinicas.SelectedIndex].ToString(), user.IdUsuario, "N", rData.Substring(0, 10), rData.Substring(11), txt_Obs.Text.Trim());


            //OpenReport("DadosEmpresa", "RelatorioGuia.aspx?IliteraSystem=" + strAux.ToString() + strAux.ToString()
            //    + "&IdEmpresa=" + Request["IdEmpresa"] + "&IdEmpregado=" + Request["IdEmpregado"] + "&tiporelatorio=tudo" + "&Exames=" + xExames + "&Exames2=" + xExames2 + "&Exames3=" + xExames3 + "&Exames4=" + xExames4 +  "&IdClinica=" + cmb_Clinicas.SelectedValue.ToString() + "&Hora_Exame=" + txt_Hora.Text + "&Data_Exame=" + txt_Data.Text + "&Tipo=" + xTipo + "&Basico=" + xBasico + "&Obs=" + xObs , "RelatorioGuia", true);

            Session["Obs_Guia"] = xObs;

            OpenReport("DadosEmpresa", "RelatorioGuiaASO.aspx?IliteraSystem=" + strAux.ToString()
    + "&IdEmpresa=" + Request["IdEmpresa"] + "&IdEmpregado=" + Request["IdEmpregado"] + "&E1=" + xExames + "&E2=" + xExames2 + "&E3=" + xExames3 + "&E4=" + xExames4 + "&IdClinica=" + cmb_Clinicas.SelectedValue.ToString() + "&H_E=" + txt_Hora.Text + "&D_E=" + txt_Data.Text + "&Tipo=" + xTipo + "&Basico=" + xBasico + "&Mail=" + xEnvio_Email + "&TipoGuia=" + lblTipoGuia.Text.Trim() + "&ImpDt=" + xImpDt + "&Apt=0", "RelatorioGuia", true);


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

        protected void rd_Mudanca_CheckedChanged(object sender, EventArgs e)
        {
            PopularValueListClinicaClienteExameDicionario(cmb_Clinicas.SelectedValue);

            if (lst_Exames.Items.Count < 1)
            {
                PopularValueListClinicaClienteExameDicionario(cmb_Clinicas.SelectedValue);
            }


            if (rd_Demissional.Checked == true) txt_Demissao.Enabled = true;
            else txt_Demissao.Enabled = false;

            if (rd_Admissao.Checked == true || rd_Demissional.Checked == true)
            {
                chk_Toxicologico.Enabled = true;
            }
            else
            {
                chk_Toxicologico.Checked = false;
                chk_Toxicologico.Enabled = false;
            }
        }

        protected void rd_Admissao_CheckedChanged(object sender, EventArgs e)
        {
            if (cmb_Clinicas.SelectedValue.Trim() == "") return;

            PopularValueListClinicaClienteExameDicionario(cmb_Clinicas.SelectedValue);

            if (lst_Exames.Items.Count < 1)
            {
                PopularValueListClinicaClienteExameDicionario(cmb_Clinicas.SelectedValue);
            }


            if (rd_Demissional.Checked == true) txt_Demissao.Enabled = true;
            else txt_Demissao.Enabled = false;

            if (rd_Admissao.Checked == true || rd_Demissional.Checked == true)
            {                
                chk_Toxicologico.Enabled = true;
            }
            else
            {
                chk_Toxicologico.Checked = false;
                chk_Toxicologico.Enabled = false;
            }
        }

        protected void rd_Demissional_CheckedChanged(object sender, EventArgs e)
        {
            PopularValueListClinicaClienteExameDicionario(cmb_Clinicas.SelectedValue);

            if (lst_Exames.Items.Count < 1)
            {
                PopularValueListClinicaClienteExameDicionario(cmb_Clinicas.SelectedValue);
            }


            if (rd_Demissional.Checked == true) txt_Demissao.Enabled = true;
            else txt_Demissao.Enabled = false;

            if (rd_Admissao.Checked == true || rd_Demissional.Checked == true)
            {
                chk_Toxicologico.Enabled = true;
            }
            else
            {
                chk_Toxicologico.Checked = false;
                chk_Toxicologico.Enabled = false;
            }

        }

        protected void rd_Retorno_CheckedChanged(object sender, EventArgs e)
        {
            PopularValueListClinicaClienteExameDicionario(cmb_Clinicas.SelectedValue);

            if (lst_Exames.Items.Count < 1)
            {
                PopularValueListClinicaClienteExameDicionario(cmb_Clinicas.SelectedValue);
            }


            if (rd_Demissional.Checked == true) txt_Demissao.Enabled = true;
            else txt_Demissao.Enabled = false;

            if (rd_Admissao.Checked == true || rd_Demissional.Checked == true)
            {
                chk_Toxicologico.Enabled = true;
            }
            else
            {
                chk_Toxicologico.Checked = false;
                chk_Toxicologico.Enabled = false;
            }
        }

        protected void rd_Periodico_CheckedChanged(object sender, EventArgs e)
        {
            PopularValueListClinicaClienteExameDicionario(cmb_Clinicas.SelectedValue);

            if (lst_Exames.Items.Count < 1)
            {
                PopularValueListClinicaClienteExameDicionario(cmb_Clinicas.SelectedValue);
            }


            if (rd_Demissional.Checked == true) txt_Demissao.Enabled = true;
            else txt_Demissao.Enabled = false;

            if (rd_Admissao.Checked == true || rd_Demissional.Checked == true)
            {
                chk_Toxicologico.Enabled = true;
            }
            else
            {
                chk_Toxicologico.Checked = false;
                chk_Toxicologico.Enabled = false;
            }
        }

        protected void rd_Outro_CheckedChanged(object sender, EventArgs e)
        {
            PopularValueListClinicaClienteExameDicionario(cmb_Clinicas.SelectedValue);

            if (lst_Exames.Items.Count < 1)
            {
                PopularValueListClinicaClienteExameDicionario(cmb_Clinicas.SelectedValue);
            }


            if (rd_Demissional.Checked == true) txt_Demissao.Enabled = true;
            else txt_Demissao.Enabled = false;

            if (rd_Admissao.Checked == true || rd_Demissional.Checked == true)
            {
                chk_Toxicologico.Enabled = true;
            }
            else
            {
                chk_Toxicologico.Checked = false;
                chk_Toxicologico.Enabled = false;
            }
        }

        protected void txt_Data_TextChanged(object sender, EventArgs e)
        {
            if (cmb_Clinicas.SelectedIndex >= 0)
            {
                PopularValueListClinicaClienteExameDicionario(cmb_Clinicas.SelectedValue);
                lbl_Id_Clinica.Text = cmb_Clinicas.SelectedValue.ToString().Trim();

                if (lst_Exames.Items.Count < 1)
                {
                    PopularValueListClinicaClienteExameDicionario(cmb_Clinicas.SelectedValue);
                }

            }
            else
            {
                lst_Exames.Items.Clear();
                lst_IdExames.Items.Clear();
                lbl_Id_Clinica.Text = "0";
            }

        }

        protected void btnempFicha_Click(object sender, EventArgs e)
        {
            lblTipoGuia.Text = "2";
            Criacao_Guia_ASO_Ficha(2);

        }


    }

}
