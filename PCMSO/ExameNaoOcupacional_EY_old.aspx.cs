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
using Ilitera.PCMSO.Report;

using CrystalDecisions.CrystalReports.Engine;

//using MestraNET;

namespace Ilitera.Net
{
    public partial class ExameNaoOcupacional_EY_Old : System.Web.UI.Page
    {
        protected void cmd_SAE_Click(object sender, EventArgs e)
        {

        }

        //      private ExameFisicoAmbulatorial exame;

        ////private string tipo;

        //      protected Prestador prestador = new Prestador();
        //      protected Usuario usuario = new Usuario();
        //      protected Cliente cliente = new Cliente();
        //      protected Empregado empregado = new Empregado();

        //      protected override void OnLoadComplete(EventArgs e)
        //      {
        //          base.OnLoadComplete(e);


        //      }

        //      [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        //      protected void Page_Load(object sender, System.EventArgs e)
        //      {

        //          string xUsuario = Session["usuarioLogado"].ToString();
        //          InicializaWebPageObjects();

        //          if (wnePeso.Text.Trim() != "" && wneAltura.Text.Trim() != "")
        //          {
        //              Calcular_IMC();
        //          }
        //          else
        //          {
        //              txtIMC.Text = "";
        //          }

        //          if (!IsPostBack)
        //          {

        //              string zAcao = Request["Acao"].ToString().Trim();

        //              if (zAcao == "E")
        //              {
        //                  btnOK.Visible = true;
        //                  btnExcluir.Visible = true;
        //              }
        //              else
        //              {
        //                  btnOK.Visible = false;
        //                  btnExcluir.Visible = false;
        //              }




        //              Session["txtAuxiliar"] = string.Empty;
        //              RegisterClientCode();
        //              //PopulaDDLQueixas();
        //              //PopulaDDLProcedimentos();
        //              if (!exame.Id.Equals(0))
        //              {
        //                  PopulaTelaExame();
        //                  Calcular_IMC();
        //              }



        //              if (exame.Id == 0)
        //              {
        //                  TabStrip1.Items[1].Visible = false;

        //              }
        //              else
        //              {
        //                  Guid strAux = Guid.NewGuid();

        //                  lkbAnamnese.Attributes.Add("onClick", "javascript:" + strOpenReport("PCMSO", "AnamneseDinamica.aspx?IliteraSystem=" + strAux.ToString() + strAux.ToString()
        //                      + "&IdExame=" + exame.Id, "Anamnese"));

        //                      TabStrip1.Items[1].Visible = false;
        //                      btnemp.Visible = false;


        //              }



        //          }
        //          else
        //          {
        //              string IdDDL = string.Empty;



        //              txtAuxiliar.Value = string.Empty;
        //              Session["txtAuxiliar"] = string.Empty;
        //          }
        //      }



        //      protected void InicializaWebPageObjects()
        //      {
        //          //base.InicializaWebPageObjects();

        //          if (Request["IdExame"] != null && Request["IdExame"] != "")
        //          {
        //              exame = new ExameFisicoAmbulatorial(Convert.ToInt32(Request["IdExame"]));
        //              //tipo = "atualizado";
        //          }
        //          else if (ViewState["IdExameNaoOcupacional"] != null)
        //          {
        //              exame = new ExameFisicoAmbulatorial(Convert.ToInt32(ViewState["IdExameNaoOcupacional"]));
        //              //tipo = "atualizado";
        //          }
        //          else
        //          {
        //              exame = new ExameFisicoAmbulatorial();

        //              Empregado xEmpregado = new Empregado();
        //              xEmpregado.Find(System.Convert.ToInt32(Request["IdEmpregado"].ToString()));


        //              exame.IdEmpregado = xEmpregado;


        //              Entities.Usuario xusuario = (Entities.Usuario)Session["usuarioLogado"];
        //              prestador.Find(" IdPessoa = " + xusuario.IdPessoa.ToString());

        //              //if (usuario.NomeUsuario.Equals("Admin"))
        //              //    exame.IdJuridica = cliente;
        //              //else
        //              //{
        //              //    //exame.IdMedico.Id = prestador.Id;
        //              //    exame.IdJuridica.Id = prestador.IdJuridica.Id;
        //              //}

        //              //if (!cliente.ContrataPCMSO.Equals((int)TipoPcmsoContratada.NaoContratada))
        //              //{
        //              //    if (!usuario.NomeUsuario.Equals("Admin"))



        //              exame.IdJuridica.Id = 310;

        //              //exame.IdMedico.Find();
        //              //exame.IdJuridica.Find();



        //              //}
        //              //else
        //              //    exame.IdMedico.Id = 1111; //PCMSO não contratada;

        //              //tipo = "cadastrado";
        //              btnExcluir.Enabled = false;
        //          }
        //      }


        //      private void PopulaDDLMedico()
        //      {
        //          //ajustar para pegar clinicas do cliente selecionado


        //      }


        //      private void RegisterClientCode()
        //      {
        //          btnExcluir.Attributes.Add("onClick", "javascript:return(confirm('Deseja realmente excluir este Exame Ambulatorial?'))");
        //      }

        //      private void PopulaDDLQueixas()
        //      {
        //      }

        //      private void PopulaDDLProcedimentos()
        //      {
        //      }

        //      private void PopulaExame()
        //      {
        //          System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");
        //          exame.DataExame = System.Convert.ToDateTime(wdtDataExame.Text.Trim(), ptBr);
        //          exame.Prontuario = txt_Avaliacao.Text.Trim();


        //          //if (!cliente.ContrataPCMSO.Equals((int)TipoPcmsoContratada.NaoContratada))
        //          //{
        //          exame.Altura = System.Convert.ToSingle(wneAltura.Text.Trim());
        //          exame.Peso = System.Convert.ToSingle(wnePeso.Text.Trim());
        //          exame.PressaoArterial = wtePA.Text.Trim();
        //          exame.Pulso = System.Convert.ToInt16(wnePulso.Text.Trim());
        //          exame.Temperatura = System.Convert.ToSingle(wneTemperatura.Text.Trim());

        //          exame.Glicose = txtGlicose.Text.Trim();

        //          exame.Tipo = "E";


        //          if (rd_MA_Acidente.Checked == true)
        //              exame.Motivo_Atendimento = "T";
        //          else if ( rd_MA_Amb.Checked == true)
        //              exame.Motivo_Atendimento = "A";
        //          else if ( rd_MA_Emerg.Checked == true)
        //              exame.Motivo_Atendimento = "E";
        //          else
        //              exame.Motivo_Atendimento = "A";

        //          exame.Responsavel_Atendimento = txt_Responsavel.Text;
        //          exame.Historico_Doencas = txt_Historico.Text;
        //          exame.Queixa_Principal = txt_Queixas.Text;
        //          exame.Outras_Queixas = txt_Outras_Queixas.Text;

        //          if (chk_Alergia.Checked == true)
        //              exame.Alergia_Medicacao = "S";
        //          else
        //              exame.Alergia_Medicacao = "N";


        //          exame.Alergia_Medicacao_Quais = txt_Alergia.Text;


        //          if (chk_Medicacao.Checked == true)
        //              exame.Medicacao_Continua = "S";
        //          else
        //              exame.Medicacao_Continua = "N";


        //          exame.Medicacao_Continua_Quais = txt_Medicacao.Text;


        //          string zDiagnostico = "";

        //          for ( int zCont=0; zCont<chk_Diagnostico.Items.Count; zCont++)
        //          {
        //              if ( chk_Diagnostico.Items[zCont].Selected == true)
        //              {
        //                  zDiagnostico = zDiagnostico + chk_Diagnostico.Items[zCont].Value.Trim() + "_";
        //              }
        //          }
        //          exame.Diagnostico_Enfermagem = zDiagnostico;


        //          exame.Diagnostico_Enfermagem_Outros = txt_Diagnostico_Enfermagem.Text;




        //          string zPlanejamento = "";

        //          for (int zCont = 0; zCont < chk_Planejamento.Items.Count; zCont++)
        //          {
        //              if (chk_Planejamento.Items[zCont].Selected == true)
        //              {
        //                  zPlanejamento = zPlanejamento + chk_Planejamento.Items[zCont].Value.Trim() + "_";
        //              }
        //          }
        //          exame.Planejamento_Enfermagem= zPlanejamento;


        //          exame.Planejamento_Enfermagem_Outros = txt_Planejamento.Text;

        //          exame.Implementacao = txt_Implementacao.Text;
        //          exame.Avaliacao_Enfermagem = txt_Avaliacao.Text;

        //          return;           

        //      }


        //      [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        //      public void PopulaTelaExame()
        //      {
        //          wdtDataExame.Text = exame.DataExame.ToString("dd/MM/yyyy");
        //          txt_Avaliacao.Text = exame.Prontuario;

        //          //if (!cliente.ContrataPCMSO.Equals((int)TipoPcmsoContratada.NaoContratada))
        //          //{
        //          wneAltura.Text = exame.Altura.ToString();
        //          wnePeso.Text = exame.Peso.ToString();
        //          wtePA.Text = exame.PressaoArterial;
        //          wnePulso.Text = exame.Pulso.ToString();
        //          wneTemperatura.Text = exame.Temperatura.ToString();




        //          txtGlicose.Text = exame.Glicose.Trim();

        //          rd_MA_Amb.Checked = true;

        //          if ( exame.Motivo_Atendimento != null)
        //          {
        //              if (exame.Motivo_Atendimento == "T")
        //              {
        //                  rd_MA_Acidente.Checked = true;
        //                  rd_MA_Amb.Checked = false;
        //                  rd_MA_Emerg.Checked = false;
        //              }
        //              else if (exame.Motivo_Atendimento == "A")
        //              {
        //                  rd_MA_Acidente.Checked = false;
        //                  rd_MA_Amb.Checked = true;
        //                  rd_MA_Emerg.Checked = false;
        //              }
        //              else if (exame.Motivo_Atendimento == "E")
        //              {
        //                  rd_MA_Acidente.Checked = false;
        //                  rd_MA_Amb.Checked = false;
        //                  rd_MA_Emerg.Checked = true;
        //              }
        //              else
        //              {
        //                  rd_MA_Acidente.Checked = false;
        //                  rd_MA_Amb.Checked = true;
        //                  rd_MA_Emerg.Checked = false;
        //              }

        //          }


        //          if ( exame.Responsavel_Atendimento != null)
        //          {
        //              txt_Responsavel.Text = exame.Responsavel_Atendimento;
        //          }

        //          if ( exame.Historico_Doencas != null)
        //          {
        //              txt_Historico.Text = exame.Historico_Doencas;
        //          }

        //          if (exame.Queixa_Principal != null)
        //          {
        //              txt_Queixas.Text = exame.Queixa_Principal;
        //          }

        //          if ( exame.Outras_Queixas != null)
        //          {
        //              txt_Outras_Queixas.Text = exame.Outras_Queixas;
        //          }

        //          if ( exame.Alergia_Medicacao != null)
        //          {
        //              if ( exame.Alergia_Medicacao == "S")
        //              {
        //                  chk_Alergia.Checked = true;
        //              }
        //              else
        //              {
        //                  chk_Alergia.Checked = false;
        //              }
        //          }

        //          if ( exame.Alergia_Medicacao_Quais != null)
        //          {
        //              txt_Alergia.Text = exame.Alergia_Medicacao_Quais;
        //          }

        //          if ( exame.Medicacao_Continua != null)
        //          {
        //              if ( exame.Medicacao_Continua == "S")
        //              {
        //                  chk_Medicacao.Checked = true;
        //              }
        //              else
        //              {
        //                  chk_Medicacao.Checked = false;
        //              }
        //          }

        //          if ( exame.Medicacao_Continua_Quais != null)
        //          {
        //              txt_Medicacao.Text = exame.Medicacao_Continua_Quais;
        //          }


        //          if ( exame.Diagnostico_Enfermagem != null)
        //          {
        //              string[] stringSeparators2 = new string[] { "_" };
        //              string[] result2;

        //              result2 = exame.Diagnostico_Enfermagem.Split(stringSeparators2, StringSplitOptions.RemoveEmptyEntries);

        //              foreach (string s in result2)
        //              {
        //                  chk_Diagnostico.Items.FindByValue(s.Substring(0, 2)).Selected = true;
        //              }
        //          }

        //          if ( exame.Diagnostico_Enfermagem_Outros != null)
        //          {
        //              txt_Diagnostico_Enfermagem.Text = exame.Diagnostico_Enfermagem_Outros;
        //          }



        //          if ( exame.Planejamento_Enfermagem != null)
        //          {
        //              string[] stringSeparators2 = new string[] { "_" };
        //              string[] result2;

        //              result2 = exame.Planejamento_Enfermagem.Split(stringSeparators2, StringSplitOptions.RemoveEmptyEntries);

        //              foreach (string s in result2)
        //              {
        //                  chk_Planejamento.Items.FindByValue(s.Substring(0, 2)).Selected = true;
        //              }
        //          }

        //          if ( exame.Planejamento_Enfermagem_Outros != null)
        //          {
        //              txt_Planejamento.Text = exame.Planejamento_Enfermagem_Outros;
        //          }


        //          if ( exame.Implementacao != null)
        //          {
        //              txt_Implementacao.Text = exame.Implementacao;
        //          }

        //          if ( exame.Avaliacao_Enfermagem != null)
        //          {
        //              txt_Avaliacao.Text = exame.Avaliacao_Enfermagem;
        //          }



        //          //}			
        //      }

        //      protected void btnOK_Click(object sender, System.EventArgs e)
        //      {
        //          try
        //          {

        //              System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");

        //              if (wdtDataExame.Text.Trim() == "")
        //              {
        //                  wdtDataExame.Text = DateTime.Now.ToString("dd/MM/yyyy", ptBr);
        //              }



        //              PopulaExame();

        //              //exame.IdEmpregado = System.Convert.ToInt32(Request["IdEmpregado"].ToString());





        //              exame.IdJuridica.Id = 310;


        //              exame.Save(System.Convert.ToInt32(Request["IdUsuario"].ToString()));

        //              ViewState["IdExameNaoOcupacional"] = exame.Id;
        //              btnExcluir.Enabled = true;

        //              StringBuilder st = new StringBuilder();

        //              //st.Append("window.opener.document.getElementById('txtAuxiliar').value='atualiza';");
        //              //st.Append("window.opener.document.forms[0].submit();");
        //              //st.Append("window.alert('O Exame Ambulatorial foi " + tipo + " com sucesso!');");

        //              txtAuxAviso.Value = "O Exame Ambulatorial foi salvo com sucesso!";
        //              txtExecutePost.Value = "True";

        //              //if (cliente.ContrataPCMSO.Equals((int)TipoPcmsoContratada.NaoContratada))
        //              txtCloseWindow.Value = "True";

        //              Entities.Usuario user = (Entities.Usuario)Session["usuarioLogado"];

        //              if (Session["Retorno"].ToString().Trim() == "1")
        //                  Response.Redirect("DadosEmpregado.aspx?IdEmpregado=" + Session["Empregado"].ToString() + "&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString() + "&Tipo=2");
        //              else if (Session["Retorno"].ToString().Trim() == "9")
        //                  Response.Redirect("DadosEmpregado_Dashboard.aspx?IdEmpregado=" + Session["Empregado"].ToString() + "&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString() + "&Tipo=6");
        //              else if (Session["Retorno"].ToString().Trim() == "91")
        //                  Response.Redirect("DadosEmpregado_Questionario.aspx?IdEmpregado=" + Session["Empregado"].ToString() + "&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString() + "&Tipo=6");
        //              else if (Session["Retorno"].ToString().Trim() == "19")
        //                  Response.Redirect("ExameQuestionario.aspx?IdEmpregado=" + Session["Empregado"].ToString() + "&IdExame=" + Session["Questionario"].ToString().Trim() + "&IdUsuario=" + Request["IdUsuario"] + "&IdEmpresa=" + Request["IdEmpresa"] + "&Acao=E");
        //              else
        //                  Response.Redirect("DadosEmpregado_Lista.aspx?IdEmpregado=" + Session["Empregado"].ToString() + "&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString() + "&Tipo=6");



        //              //this.ClientScript.RegisterStartupScript(this.GetType(), "AtualizaNaoOcupacional", st.ToString(), true);
        //          }
        //          catch (Exception ex)
        //          {
        //              txtAuxAviso.Value = ex.Message;
        //              MsgBox1.Show("Ilitera.Net", ex.Message, null,
        //                     new EO.Web.MsgBoxButton("OK"));

        //          }
        //      }

        //      protected void btnExcluir_Click(object sender, System.EventArgs e)
        //      {
        //          try
        //          {
        //              exame.Delete(System.Convert.ToInt32(Request["IdUsuario"].ToString()));

        //              ViewState["IdExameNaoOcupacional"] = 0;  // null;

        //              btnExcluir.Enabled = true;
        //              StringBuilder st = new StringBuilder();

        //              txtAuxAviso.Value = "O Exame Ambulatorial foi deletado com sucesso";
        //              txtExecutePost.Value = "True";
        //              txtCloseWindow.Value = "True";

        //              Response.Write("<script>window.opener.document.forms[0].submit(); window.close();</script>");

        //              Entities.Usuario user = (Entities.Usuario)Session["usuarioLogado"];


        //              if (Session["Retorno"].ToString().Trim() == "1")
        //                  Response.Redirect("DadosEmpregado.aspx?IdEmpregado=" + Session["Empregado"].ToString() + "&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString() + "&Tipo=2");
        //              else if (Session["Retorno"].ToString().Trim() == "9")
        //                  Response.Redirect("DadosEmpregado_Dashboard.aspx?IdEmpregado=" + Session["Empregado"].ToString() + "&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString() + "&Tipo=6");
        //              else if (Session["Retorno"].ToString().Trim() == "91")
        //                  Response.Redirect("DadosEmpregado_Questionario.aspx?IdEmpregado=" + Session["Empregado"].ToString() + "&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString() + "&Tipo=6");
        //              else if (Session["Retorno"].ToString().Trim() == "19")
        //                  Response.Redirect("ExameQuestionario.aspx?IdEmpregado=" + Session["Empregado"].ToString() + "&IdExame=" + Session["Questionario"].ToString().Trim() + "&IdUsuario=" + Request["IdUsuario"] + "&IdEmpresa=" + Request["IdEmpresa"] + "&Acao=E");
        //              else
        //                  Response.Redirect("DadosEmpregado_Lista.aspx?IdEmpregado=" + Session["Empregado"].ToString() + "&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString() + "&Tipo=6");


        //          }
        //          catch (Exception ex)
        //          {
        //              txtAuxAviso.Value = ex.Message;
        //              MsgBox1.Show("Ilitera.Net", ex.Message, null,
        //                     new EO.Web.MsgBoxButton("OK"));

        //          }
        //      }

        //      protected void cmd_Voltar_Click(object sender, EventArgs e)
        //      {

        //          Entities.Usuario user = (Entities.Usuario)Session["usuarioLogado"];
        //          if (Session["Retorno"].ToString().Trim() == "1")
        //              Response.Redirect("DadosEmpregado.aspx?IdEmpregado=" + Session["Empregado"].ToString() + "&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString() + "&Tipo=2");
        //          else if (Session["Retorno"].ToString().Trim() == "9")
        //              Response.Redirect("DadosEmpregado_Dashboard.aspx?IdEmpregado=" + Session["Empregado"].ToString() + "&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString() + "&Tipo=6");
        //          else if (Session["Retorno"].ToString().Trim() == "91")
        //              Response.Redirect("DadosEmpregado_Questionario.aspx?IdEmpregado=" + Session["Empregado"].ToString() + "&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString() + "&Tipo=6");
        //          else if (Session["Retorno"].ToString().Trim() == "19")
        //              Response.Redirect("ExameQuestionario.aspx?IdEmpregado=" + Session["Empregado"].ToString() + "&IdExame=" + Session["Questionario"].ToString().Trim() + "&IdUsuario=" + Request["IdUsuario"] + "&IdEmpresa=" + Request["IdEmpresa"] + "&Acao=E");
        //          else
        //              Response.Redirect("DadosEmpregado_Lista.aspx?IdEmpregado=" + Session["Empregado"].ToString() + "&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString() + "&Tipo=6");


        //      }


        //      private void Calcular_IMC()
        //      {
        //          float zIMC = 0;

        //          float zPeso = 0;
        //          float zAltura = 0;

        //          bool zReturn = false;

        //          string decimalSeparator = CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator;
        //          string xTroca = " ";

        //          if (decimalSeparator == ".") xTroca = ",";
        //          else xTroca = ".";


        //          wnePeso.Text = wnePeso.Text.Replace(xTroca, decimalSeparator);
        //          wneAltura.Text = wneAltura.Text.Replace(xTroca, decimalSeparator);


        //          if (float.TryParse(wnePeso.Text, out zPeso))
        //          {
        //              // success! Use f here
        //          }
        //          else
        //          {
        //              wnePeso.Text = "0";
        //              txtIMC.Text = "";
        //              zReturn = true;
        //          }


        //          if (float.TryParse(wneAltura.Text, out zAltura))
        //          {
        //              // success! Use f here
        //          }
        //          else
        //          {
        //              wneAltura.Text = "0";
        //              txtIMC.Text = "";
        //              zReturn = true;
        //          }


        //          if (zAltura <= 0)
        //          {
        //              wneAltura.Text = "0";
        //              txtIMC.Text = "";
        //              zReturn = true;
        //          }

        //          if (zPeso <= 0)
        //          {
        //              wnePeso.Text = "0";
        //              txtIMC.Text = "";
        //              zReturn = true;
        //          }


        //          if (zReturn == true) return;

        //          if (zAltura > 100) zAltura = zAltura / 100;

        //          if (zAltura == 0)
        //          {
        //              txtIMC.Text = "";
        //              return;
        //          }

        //          zIMC = (zPeso / (zAltura * zAltura));

        //          txtIMC.Text = zIMC.ToString("#.##");
        //          return;

        //      }








        //      protected void gridAnamnese_ItemCommand(object sender, EO.Web.GridCommandEventArgs e)
        //      {
        //          //Check whether it is from our client side
        //          //JavaScript call


        //          //Session["Id2"] = e.Item.Key.ToString();
        //          //Session["NomeEmpregado"] = e.Item.Cells[0].Value.ToString();



        //      }



        //      private void Carregar_Grid_Anamnese_Capgemini()
        //      {

        //          if (exame.Id == 0)
        //          {
        //              // criar registros padrão,  mas como criar se idclinico = 0
        //              return;
        //          }

        //          //DataSet ds = new Ilitera.Opsa.Data.Anamnese_Questao().Get(" Questao is not null order by Sistema, Questao ");

        //          Ilitera.Data.Clientes_Funcionarios zQuest = new Ilitera.Data.Clientes_Funcionarios();

        //          DataSet ds = zQuest.Trazer_Anamnese_Exame(exame.Id);


        //          gridAnamnese.DataSource = ds;
        //          gridAnamnese.DataBind();


        //          //string xSistema = "";
        //          //System.Drawing.Color zColor = new System.Drawing.Color();

        //          //int zMud = 0;

        //          //for (int xCont = 0; xCont < gridAnamnese.Items.Count; xCont++)
        //          //{
        //          //    if (xSistema != gridAnamnese.Items[xCont].Cells[4].Value.ToString())
        //          //    {
        //          //        zMud = zMud + 1;

        //          //        if (zMud % 2 == 1)
        //          //            zColor = Color.LightYellow;
        //          //        else
        //          //            zColor = Color.White;

        //          //        xSistema = gridAnamnese.Items[xCont].Cells[4].Value.ToString();

        //          //    }

        //          //    //gridAnamnese.Items[xCont].Grid.BackColor = zColor;

        //          //    //gridAnamnese.Items[xCont].Cells[6].Column.CellStyle.BackColor = zColor;
        //          //    gridAnamnese.Items[xCont].Grid.BackColor = zColor;
        //          //}

        //      }


        //      private void Carregar_Dados_Anamnese_Exame()
        //      {


        //          if (exame.Id == 0)
        //          {
        //              return;
        //          }



        //          List<Anamnese_Exame> AnExame = new Anamnese_Exame().Find<Anamnese_Exame>(" IdExameBase = " + exame.Id);



        //          if (AnExame.Count == 0)
        //          {
        //              //trazer padrão para cliente
        //              exame.IdEmpregado.nID_EMPR.Find();
        //              List<Anamnese_Dinamica> anExamePadrao = new Anamnese_Dinamica().Find<Anamnese_Dinamica>(" IdPessoa = " + exame.IdEmpregado.nID_EMPR.Id);


        //              if (anExamePadrao.Count == 0)
        //              {
        //                  return;
        //              }
        //              else
        //              {

        //                  Ilitera.Data.Clientes_Funcionarios xAnam = new Ilitera.Data.Clientes_Funcionarios();
        //                  xAnam.Carregar_Anamnese_Dinamica(exame.IdEmpregado.nID_EMPR.Id, exame.Id);

        //                  //foreach (Anamnese_Dinamica zPadrao in anExamePadrao)
        //                  //{
        //                  //    Anamnese_Exame rTestes = new Anamnese_Exame();

        //                  //    rTestes.IdAnamneseDinamica = zPadrao.Id;
        //                  //    rTestes.IdExameBase = exame.Id;
        //                  //    rTestes.Resultado = "N";
        //                  //    rTestes.Peso = zPadrao.Peso;
        //                  //    rTestes.Save();
        //                  //}

        //              }

        //          }


        //          return;


        //      }

        //      protected void cmd_Anamnese_Click(object sender, EventArgs e)
        //      {

        //          Int32 zKey = 0;
        //          string zResultado = "N";


        //          if (gridAnamnese.ChangedItems.Length == 0)
        //          {
        //              MsgBox1.Show("Ilitera.Net", "Não houve alterações na Anamnese", null,
        //              new EO.Web.MsgBoxButton("OK"));
        //              return;
        //          }

        //          else
        //          {
        //              foreach (EO.Web.GridItem item in gridAnamnese.ChangedItems)
        //              {
        //                  foreach (EO.Web.GridCell cell in item.Cells)
        //                  {
        //                      if (cell.Modified)
        //                      {
        //                          //string text = string.Format(
        //                          //    "Cell Changed: Key = {0}, Field = {1}, New Value = {2}",
        //                          //    item.Key,
        //                          //    cell.Column.DataField,
        //                          //    cell.Value);
        //                          zKey = System.Convert.ToInt32(item.Key);
        //                          zResultado = cell.Value.ToString().Trim();


        //                          Ilitera.Data.Clientes_Funcionarios zAtualiza = new Ilitera.Data.Clientes_Funcionarios();

        //                          zAtualiza.Atualizar_Anamnese_Dinamica(zKey, zResultado.Substring(0, 1));

        //                      }
        //                  }
        //              }

        //              MsgBox1.Show("Ilitera.Net", "Alterações na Anamnese salva.", null,
        //              new EO.Web.MsgBoxButton("OK"));
        //              return;

        //          }

        //      }

        //      protected static string strOpenReport(string directory, string fileAndQuery, string ReportName)
        //      {
        //          return strOpenReport(directory, fileAndQuery, ReportName, false);
        //      }

        //      protected static string strOpenReport(string directory, string fileAndQuery, string ReportName, bool useDirectoryForLocalProcess)
        //      {
        //          StringBuilder st = new StringBuilder();

        //          Guid strAux = Guid.NewGuid();

        //          string valueProcess = "Local";

        //          if (valueProcess.Equals("Remote"))
        //              st.Append("void(window.open('http://report.ilitera.net/index.aspx?Identity=" + strAux.ToString() + "opsa" + strAux.ToString()
        //                  + "&Key=" + strAux.ToString() + "MestraNet" + strAux.ToString()
        //                  + "&PathAndQuery=" + HttpUtility.UrlEncode(directory + "/" + fileAndQuery) + "', '" + ReportName + "', 'scrollbars=yes, resizable=yes, toolbar=no, addressbar=no, menubar=no, width=800px, height=600px').focus());");
        //          else if (valueProcess.Equals("Local"))
        //          {
        //              if (useDirectoryForLocalProcess)
        //              {
        //                  st.AppendFormat("void(window.open('../{0}/{1}', '{2}', 'scrollbars=yes, resizable=yes, toolbar=no, addressbar=no, menubar=no, width=800px, height=600px').focus());", directory, fileAndQuery, ReportName);
        //              }
        //              else
        //              {
        //                  st.AppendFormat("void(window.open('{0}','{1}', 'scrollbars=yes, resizable=yes, toolbar=no, addressbar=no, menubar=no, width=800px, height=600px').focus());", fileAndQuery, ReportName);
        //              }
        //          }

        //          return st.ToString();
        //      }

        //      [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        //      protected void rd_Medico_CheckedChanged(object sender, EventArgs e)
        //      {

        //          Cliente cliente;
        //          cliente = new Cliente(System.Convert.ToInt32(Session["Empresa"].ToString()));

        //          cliente.IdGrupoEmpresa.Find();



        //      }

        //      [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        //      protected void rd_Enfermagem_CheckedChanged(object sender, EventArgs e)
        //      {
        //          Cliente cliente;
        //          cliente = new Cliente(System.Convert.ToInt32(Session["Empresa"].ToString()));

        //          cliente.IdGrupoEmpresa.Find();


        //      }

        //      [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        //      protected void rd_Outros_CheckedChanged(object sender, EventArgs e)
        //      {
        //          Cliente cliente;
        //          cliente = new Cliente(System.Convert.ToInt32(Session["Empresa"].ToString()));

        //          cliente.IdGrupoEmpresa.Find();


        //      }

        //      [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        //      protected void Chk_PacienteCritico_CheckedChanged(object sender, EventArgs e)
        //      {
        //          Cliente cliente;
        //          cliente = new Cliente(System.Convert.ToInt32(Session["Empresa"].ToString()));

        //          cliente.IdGrupoEmpresa.Find();


        //      }

        //      protected void btnemp_Click(object sender, EventArgs e)
        //      {

        //          Guid strAux = Guid.NewGuid();
        //          System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");

        //          if (exame.Id == 0)
        //          {
        //              if (wdtDataExame.Text.Trim() == "")
        //              {
        //                  wdtDataExame.Text = DateTime.Now.ToString("dd/MM/yyyy", ptBr);
        //              }


        //              exame.Save(System.Convert.ToInt32(Request["IdUsuario"].ToString()));
        //              ViewState["IdExameNaoOcupacional"] = exame.Id;

        //              Carregar_Dados_Anamnese_Exame();
        //          }

        //          OpenReport("PCMSO", "AnamneseDinamicaBranco.aspx?IliteraSystem=" + strAux.ToString() + "&IdExame=" + exame.Id, "Anamnese", true);

        //      }



        //      protected void OpenReport(string directory, string fileAndQuery, string ReportName)
        //      {
        //          this.OpenReport(directory, fileAndQuery, ReportName, false);
        //      }

        //      protected void OpenReport(string directory, string fileAndQuery, string ReportName, bool useDirectoryForLocalProcess)
        //      {
        //          StringBuilder st = new StringBuilder();

        //          Guid strAux = Guid.NewGuid();

        //          string valueProcess = "Local";

        //          if (valueProcess.Equals("Remote"))
        //              st.Append("void(window.open('http://report.ilitera.net/index.aspx?Identity=" + strAux.ToString() + "opsa" + strAux.ToString()
        //                  + "&Key=" + strAux.ToString() + "MestraNet" + strAux.ToString()
        //                  + "&PathAndQuery=" + HttpUtility.UrlEncode(directory + "/" + fileAndQuery) + "', '" + ReportName + "', 'scrollbars=yes, resizable=yes, toolbar=no, addressbar=no, menubar=no, width=800px, height=600px'));");
        //          else if (valueProcess.Equals("Local"))
        //          {
        //              if (useDirectoryForLocalProcess)
        //              {
        //                  //st.Append("void(window.open('../" + directory + "/" + fileAndQuery + "', '" + ReportName + "', 'scrollbars=yes, resizable=yes, toolbar=no, addressbar=no, menubar=no, width=800px, height=600px'));");
        //                  st.AppendFormat("void(window.open('../{0}/{1}', '{2}','scrollbars=yes, resizable=yes, toolbar=no, addressbar=no, menubar=no, width=800px, height=600px'));", directory, fileAndQuery, ReportName);
        //              }
        //              else
        //              {
        //                  //st.Append("void(window.open('" + fileAndQuery + "', '" + ReportName + "', 'scrollbars=yes, resizable=yes, toolbar=no, addressbar=no, menubar=no, width=800px, height=600px'));");
        //                  st.AppendFormat("void(window.open('{0}','{1}', 'scrollbars=yes, resizable=yes, toolbar=no, addressbar=no, menubar=no, width=800px, height=600px'));", fileAndQuery, ReportName);
        //              }
        //          }

        //          ScriptManager.RegisterStartupScript(this, this.GetType(), String.Format("OpenReport{0}", ReportName), st.ToString(), true);
        //      }

        //      protected void cmd_SAE_Click(object sender, EventArgs e)
        //      {


        //          OpenReport("DadosEmpresa", "RelatorioSAE.aspx?IdExame=" + exame.Id.ToString().ToString(), "RelatorioExame", true);

        //      }


    }
}
