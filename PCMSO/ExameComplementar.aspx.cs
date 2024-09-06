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


namespace Ilitera.Net
{
    public partial class ExameComplementar : System.Web.UI.Page
    {
        
        protected System.Web.UI.HtmlControls.HtmlTableCell TDGridListaEmpresa;
		protected Complementar exameComplementar;
		private string tipo;

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


                if (exameComplementar.Id == 0)
                {                
                    chk_PCMSO.Checked = true;
                    chk_PCMSO.Enabled = true;
                }
                else
                {
                    chk_PCMSO.Checked = false;
                    chk_PCMSO.Enabled = false;
                }

                RegisterClientCode();
 				PopulaDDLExame();
				ddlClinica.Items.Insert(0, new ListItem("Selecione...", "0"));
				ddlMedico.Items.Insert(0, new ListItem("Selecione...", "0"));

                PopulaDDLUnidadeMedida();

                //if (cliente.ContrataPCMSO.Equals((int)TipoPcmsoContratada.NaoContratada))
                //{
                //    lblClinica.ForeColor = foreColorDisabledLabel;
                //    ddlClinica.BackColor = backColorDisabledBox;
                //    ddlClinica.Enabled = false;
                //    btnAddClinica.Disabled = true;
                //    lblMedico.ForeColor = foreColorDisabledLabel;
                //    ddlMedico.BackColor = backColorDisabledBox;
                //    ddlMedico.Enabled = false;
                //    btnAddMedico.Disabled = true;
                //    lblAnotacoes.ForeColor = foreColorDisabledLabel;
                //    txtDescricao.BorderColor = borderColorDisabledBox;
                //    txtDescricao.BackColor = backColorDisabledBox;
                //    txtDescricao.Enabled = false;
                //    ddlTipoExame.AutoPostBack = false;
                //}
                //else if (!usuario.NomeUsuario.Equals("Admin"))
                //{

                Entities.Usuario usuario = (Entities.Usuario)Session["usuarioLogado"];
                    prestador.Find(" IdPessoa = " + usuario.IdPessoa.ToString());

					prestador.IdJuridica.Find();

					if (prestador.IdJuridica.IdJuridicaPapel.Id == (int)IndJuridicaPapel.Clinica)
					{
						Clinica clinica = new Clinica(prestador.IdJuridica.Id);

                        //if (!clinica.IsClinicaInterna)
                        //{
                        //    lblClinica.ForeColor = foreColorDisabledLabel;
                        //    ddlClinica.BackColor = backColorDisabledBox;
                        //    ddlClinica.Enabled = false;
                        //    btnAddClinica.Disabled = true;
                        //    lblMedico.ForeColor = foreColorDisabledLabel;
                        //    ddlMedico.BackColor = backColorDisabledBox;
                        //    ddlMedico.Enabled = false;
                        //    btnAddMedico.Disabled = true;
                        //    ddlTipoExame.AutoPostBack = false;
                        //}
					}
				//}

				if (!exameComplementar.Id.Equals(0))
                    PopulaTelaExame();

                if (exameComplementar.Id == 0)
                {
                    TabStrip1.Items[1].Visible = false;                    
                }
                else
                {                    

                    Guid strAux = Guid.NewGuid();

                    lkbAnamnese.Attributes.Add("onClick", "javascript:" + strOpenReport("PCMSO", "AnamneseDinamica.aspx?IliteraSystem=" + strAux.ToString() + strAux.ToString()
                        + "&IdExame=" + exameComplementar.Id, "Anamnese"));

                    exameComplementar.IdEmpregado.Find();
                    exameComplementar.IdEmpregado.nID_EMPR.Find();
                    exameComplementar.IdEmpregado.nID_EMPR.IdGrupoEmpresa.Find();

                    if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("PRAJNA") > 0  )//exameComplementar.IdEmpregado.nID_EMPR.IdGrupoEmpresa.Descricao.ToUpper().Trim() == "CAPGEMINI"  ) //&& Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("_HOM") > 0)  //.Id == -905238295)  // Capgemini
                    {
                        TabStrip1.Items[1].Visible = true;
                        Carregar_Dados_Anamnese_Exame();
                        Carregar_Grid_Anamnese_Capgemini();
                    }
                    else
                    {
                        TabStrip1.Items[1].Visible = false;
                    }
                }

            }
			else if (txtAuxiliar.Value.Equals("atualizaClinica"))
			{
				txtAuxiliar.Value = string.Empty;
				PopulaDDLClinicas(ddlTipoExame.SelectedValue);
                
			}
			else if (txtAuxiliar.Value.Equals("atualizaMedico"))
			{
				txtAuxiliar.Value = string.Empty;
				PopulaDDLMedico();
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

            empregado.Find( System.Convert.ToInt32( Request["IdEmpregado"].ToString() ));

			if (Request["IdExame"] != null && Request["IdExame"] != "")
			{
				exameComplementar = new Complementar(Convert.ToInt32(Request["IdExame"]));
				tipo = "atualizado";
			}
			else if (ViewState["IdExameComplementar"] != null)
			{
				exameComplementar = new Complementar(Convert.ToInt32(ViewState["IdExameComplementar"]));
				tipo = "atualizado";
			}
			else
			{
				exameComplementar = new Complementar();
				exameComplementar.Inicialize();
				exameComplementar.IdEmpregado = empregado;

                if (!cliente.ContrataPCMSO.Equals((int)TipoPcmsoContratada.NaoContratada))
				{
					if (!usuario.NomeUsuario.Equals("Admin"))
					{
                        
                        Entities.Usuario zusuario = (Entities.Usuario)Session["usuarioLogado"];
                        prestador.Find(" IdPessoa = " + zusuario.IdPessoa.ToString());

                        prestador.IdJuridica.Find();


						if (prestador.IdJuridica.IdJuridicaPapel.Id == (int)IndJuridicaPapel.Clinica)
						{
							Clinica clinica = new Clinica(prestador.IdJuridica.Id);

							if (!clinica.IsClinicaInterna)
							{
								exameComplementar.IdJuridica = prestador.IdJuridica;
								exameComplementar.IdMedico.Id = prestador.Id;
							}
						}
					}
				}
				else
				{
                    if (!usuario.NomeUsuario.Equals("Admin"))
                    {
                        Entities.Usuario zusuario = (Entities.Usuario)Session["usuarioLogado"];
                        prestador.Find(" IdPessoa = " + zusuario.IdPessoa.ToString());

                        prestador.IdJuridica.Find();

                        exameComplementar.IdJuridica = prestador.IdJuridica;
                    }
                    else
                        exameComplementar.IdJuridica.Id = System.Convert.ToInt32(Request["IdEmpresa"].ToString());

					//exameComplementar.IdMedico.Id = 1111; //PCMSO não contratada;
				}

				tipo = "cadastrado";
				btnExcluir.Enabled = false;
			}
		}

		private void RegisterClientCode()
		{
            btnAddClinica.Attributes.Add("onClick", "javascript:AbreClinicas(" + Request["IdEmpresa"].ToString() + ", " + Request["IdEmpregado"].ToString() + ");");
            btnAddMedico.Attributes.Add("onClick", "javascript:AbreMedicos(" + Request["IdUsuario"].ToString() + ");");
			btnExcluir.Attributes.Add("onClick" ,"javascript:return(confirm('Deseja realmente excluir este Exame Complementar?'))");
		}

		private void PopulaExame()
		{
            if (txtResultado.Text.Trim() == "") txtResultado.Text = "0";
            if (txtReferencia.Text.Trim() == "") txtReferencia.Text = "0";
            if (txtIBPM.Text.Trim() == "") txtIBPM.Text = "0";


            if (rblResultado.SelectedItem == null)
				throw new Exception("O resultado do Exame Complementar deve ser selecionado!");

            float i = 0;
            string s = txtResultado.Text;
            bool result = float.TryParse(s, out i);
            if ( result==false)
                throw new Exception("O valor do resultado do Exame Complementar deve ser um valor numérico!");

            float i2 = 0;
            string s2 = txtReferencia.Text;
            bool result2 = float.TryParse(s2, out i2);
            if (result2 == false)
                throw new Exception("O valor de Referência do Exame Complementar deve ser um valor numérico!");

            float i3 = 0;
            string s3 = txtIBPM.Text;
            bool result3 = float.TryParse(s3, out i3);
            if (result3 == false)
                throw new Exception("O valor IBPM do Exame Complementar deve ser um valor numérico!");

            

            exameComplementar.IdExameDicionario.Id = Convert.ToInt32(ddlTipoExame.SelectedValue);

            exameComplementar.IdEmpregado.Find( System.Convert.ToInt32( Request["IdEmpregado"].ToString() ));
			
			if (txtDescricao.Enabled)
                exameComplementar.Prontuario = txtDescricao.Text.Trim();

			exameComplementar.IndResultado = Convert.ToInt16(rblResultado.SelectedValue);

            float fResult = float.Parse(txtResultado.Text);
            exameComplementar.Resultado = fResult;

            float fReferencia = float.Parse(txtReferencia.Text);
            exameComplementar.Referencia = fReferencia;

            float fIBPM = float.Parse(txtIBPM.Text);
            exameComplementar.IBPM = fIBPM;

            exameComplementar.IdComplementarUnidadeMedida =  Convert.ToInt32(ddlUnidadeMedida.SelectedValue);

            //if (wdtDataExame.Date.Hour.Equals(0) && wdtDataExame.Date.Minute.Equals(0) && wdtDataExame.Date.Second.Equals(0))
            //    exameComplementar.DataExame = wdtDataExame.Date.AddHours(DateTime.Now.Hour).AddMinutes(DateTime.Now.Minute).AddSeconds(DateTime.Now.Second);
            //else
            System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");      
            exameComplementar.DataExame = System.Convert.ToDateTime( wdtDataExame.Text, ptBr);

			if (ddlClinica.Enabled)
			{
                exameComplementar.IdJuridica.Find(Convert.ToInt32(ddlClinica.SelectedValue));
				//exameComplementar.IdJuridica.Id = Convert.ToInt32(ddlClinica.SelectedValue);
				//exameComplementar.IdMedico.Id = Convert.ToInt32(ddlMedico.SelectedValue);
                exameComplementar.IdMedico.Find( Convert.ToInt32(ddlMedico.SelectedValue));
			}
		}

		public void PopulaTelaExame()
		{			
			wdtDataExame.Text = exameComplementar.DataExame.ToString("dd/MM/yyyy");
			ddlTipoExame.Items.FindByValue(exameComplementar.IdExameDicionario.Id.ToString()).Selected = true;
            

            txtResultado.Text = exameComplementar.Resultado.ToString("n5");

            txtReferencia.Text = exameComplementar.Referencia.ToString("n5");

            txtIBPM.Text = exameComplementar.IBPM.ToString("n5");

            ddlUnidadeMedida.Items.FindByValue(exameComplementar.IdComplementarUnidadeMedida.ToString()).Selected = true;

            ProntuarioDigital xProntuario = new ProntuarioDigital();
            xProntuario.Find( "IdExameBase = " + exameComplementar.Id.ToString() + " ");

            if (xProntuario.Arquivo.Trim() != "")
            {
                txt_Arq.Text = xProntuario.Arquivo.Trim() ;
                txt_Arq.ReadOnly = true;
            }
            else
            {
                txt_Arq.Text = "";
                txt_Arq.ReadOnly = true;
            }
              

			if (!exameComplementar.IndResultado.Equals(0)) 
				rblResultado.Items.FindByValue(exameComplementar.IndResultado.ToString()).Selected = true;
			
			if (txtDescricao.Enabled)
                txtDescricao.Text = exameComplementar.Prontuario;

			if (ddlClinica.Enabled)
			{
				if (!exameComplementar.Id.Equals(0))
				{
					PopulaDDLClinicas(exameComplementar.IdExameDicionario.Id.ToString());
					
					try
					{
						ddlClinica.Items.FindByValue(exameComplementar.IdJuridica.Id.ToString()).Selected = true;
					}
					catch
					{
						exameComplementar.IdJuridica.Find();
						ddlClinica.Items.Insert(1, new ListItem(exameComplementar.IdJuridica.NomeAbreviado, exameComplementar.IdJuridica.Id.ToString()));
						ddlClinica.Items[1].Selected = true;
					}
				}

				PopulaDDLMedico();
				
				try
				{
					ddlMedico.Items.FindByValue(exameComplementar.IdMedico.Id.ToString()).Selected = true;
				}
				catch
				{
					exameComplementar.IdMedico.Find();
					ddlMedico.Items.Insert(1, new ListItem(exameComplementar.IdMedico.NomeCompleto, exameComplementar.IdMedico.Id.ToString()));
					ddlMedico.Items[1].Selected = true;
				}
			}
		}
		
		protected void btnOK_Click(object sender, System.EventArgs e)
		{
			try
			{
				PopulaExame();
				exameComplementar.UsuarioId = usuario.Id;

                //ExameDicionario rExame = new ExameDicionario();

                //rExame.Find(" IdExameDicionario = " + ddlTipoExame.SelectedValue +  " and Codigo_eSocial in (583, 998, 999, 1128, 1230, 1992, 1993, 1994, 1995, 1996, 1997, 1998, 1999, 9999)  ");

                //if (rExame.Id != 0)
                //{
                //    if (txtDescricao.Text.Trim() == "")
                //    {
                //        throw new Exception("Para este tipo de exame deve ter uma descrição que será enviado no evento 2220 do eSocial!");                        
                //    }
                //}



                if (exameComplementar.Id == 0)
                {
                    exameComplementar.IdExameDicionario.Find();
                    if (exameComplementar.IdExameDicionario.Id == 100)
                    {
                        exameComplementar.CodBusca = Gerar_Sequencial_Toxicologico();
                    }
                }



                exameComplementar.Save();


                //salvar prontuario
                if (File1.FileName != string.Empty )
                {

                    string xExtension = File1.FileName.Substring(File1.FileName.Length - 3, 3).ToUpper().Trim();

                    if (xExtension == "PDF" || xExtension == "JPG")
                    {

                        Cliente xCliente = new Cliente();
                        xCliente.Find(System.Convert.ToInt32(Request["IdEmpresa"].ToString()));


                        string xArq = "";

                        //if (Session["Servidor_Web"].ToString().Trim().ToUpper() == "ILITERA")
                        xArq = "I:\\fotosdocsdigitais\\" + xCliente.DiretorioPadrao.ToString().ToUpper().Trim() + "\\Prontuario\\" + File1.FileName;
                        // else                        
                        //   xArq = "C:\\DRIVE_I\\fotosdocsdigitais\\" + xCliente.DiretorioPadrao.ToString().ToUpper().Trim() + "\\Prontuario\\" + File1.FileName;


                        File1.SaveAs(xArq);

                        xArq = "I:\\fotosdocsdigitais\\" + xCliente.DiretorioPadrao.ToString().ToUpper().Trim() + "\\Prontuario\\" + File1.FileName;




                        ExameBase xExameBase = new ExameBase();
                        xExameBase.Find(exameComplementar.Id);

                        Empregado xEmpregado = new Empregado();
                        xEmpregado.Find(System.Convert.ToInt32(Request["IdEmpregado"].ToString()));

                        Entities.Usuario zusuario = (Entities.Usuario)Session["usuarioLogado"];
                        prestador.Find(" IdPessoa = " + zusuario.IdPessoa.ToString());

                        ProntuarioDigital xProntuario = new ProntuarioDigital();
                        xProntuario.IdExameBase = xExameBase;
                        xProntuario.IdEmpregado = xEmpregado;

                        System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");
                        xProntuario.DataProntuario = System.Convert.ToDateTime(wdtDataExame.Text, ptBr);
                        xProntuario.Arquivo = xArq; //searchFile.Value.ToString();
                        xProntuario.DataDigitalizacao = System.DateTime.Now;
                        xProntuario.IdDigitalizador = prestador;
                        xProntuario.Save();

                    }
                }

                //Request["IdEmpresa"].ToString()  Request["IdEmpregado"]

				ViewState["IdExameComplementar"] = exameComplementar.Id;

				btnExcluir.Enabled = true;


                if ( exameComplementar.IndResultado==1 || exameComplementar.IndResultado == 2)
                {
                    Cliente rCliente = new Cliente();
                    rCliente.Find(System.Convert.ToInt32(Request["IdEmpresa"].ToString()));

                    if (rCliente.Ativar_DesconsiderarCompl != null && rCliente.Dias_Desconsiderar != null)
                    {
                        if (rCliente.Ativar_DesconsiderarCompl == true && rCliente.Dias_Desconsiderar > 0)
                        {
                            new Pcmso().GerarExamePlanejamento(exameComplementar.IdEmpregado.Id);
                        }
                    }
                    
                }


                StringBuilder st = new StringBuilder();

				st.Append("window.opener.document.getElementById('txtAuxiliar').value='atualiza';");
				st.Append("window.opener.document.forms[0].submit();");
				st.Append("window.alert('O Exame Complementar foi " + tipo + " com sucesso!');");

                this.ClientScript.RegisterStartupScript(this.GetType(), "AtualizaComplementar", st.ToString(), true);
                
                Response.Write("<script>window.opener.document.forms[0].submit(); window.close();</script>");


                Entities.Usuario user = (Entities.Usuario)Session["usuarioLogado"];


                if (Session["Retorno"].ToString().Trim() == "1")
                    Response.Redirect("DadosEmpregado.aspx?IdEmpregado=" + Session["Empregado"].ToString() + "&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString() + "&Tipo=3");
                else
                    Response.Redirect("DadosEmpregado_Lista.aspx?IdEmpregado=" + Session["Empregado"].ToString() + "&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString() + "&Tipo=6");


			}
			catch(Exception ex)
			{                
                MsgBox1.Show("Ilitera.Net", ex.Message, null,
                       new EO.Web.MsgBoxButton("OK"));
			}
		}


        private Int32 Gerar_Sequencial_Toxicologico()
        {
            Int32 zCodMax = 1;


            Ilitera.Data.Clientes_Funcionarios xBusca = new Data.Clientes_Funcionarios();
            zCodMax = xBusca.Retornar_CodBusca_Toxicologico(0, 10000000);


            zCodMax = zCodMax + 1;


            return zCodMax;

        }



        protected void btnExcluir_Click(object sender, System.EventArgs e)
		{
			try
			{

                ProntuarioDigital xProntuario = new ProntuarioDigital();
                xProntuario.Find("IdExameBase = " + exameComplementar.Id.ToString() + " ");
                if (xProntuario.Id != 0)
                {
                    xProntuario.Delete();
                }


				exameComplementar.UsuarioId = usuario.Id;

                //if (!cliente.ContrataPCMSO.Equals((int)TipoPcmsoContratada.NaoContratada))
                 //   exameComplementar.Delete();
				//else
					exameComplementar.Delete(true);

				ViewState["IdExameComplementar"] = null;

				StringBuilder st = new StringBuilder();

				st.Append("window.opener.document.getElementById('txtAuxiliar').value='atualiza';");
				st.Append("window.opener.document.forms[0].submit();");
				st.Append("window.alert('O Exame Complementar foi deletado com sucesso!');");
				st.Append("self.close();");

                this.ClientScript.RegisterStartupScript(this.GetType(), "DeletaComplementar", st.ToString(), true);
                Response.Write("<script>window.opener.document.forms[0].submit(); window.close();</script>");

                Entities.Usuario user = (Entities.Usuario)Session["usuarioLogado"];

                if (Session["Retorno"].ToString().Trim() == "1")
                    Response.Redirect("DadosEmpregado.aspx?IdEmpregado=" + Session["Empregado"].ToString() + "&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString() + "&Tipo=3");
                else
                    Response.Redirect("DadosEmpregado_Lista.aspx?IdEmpregado=" + Session["Empregado"].ToString() + "&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString() + "&Tipo=6");


                
			}
			catch(Exception ex)
			{                
                MsgBox1.Show("Ilitera.Net", ex.Message, null,
                       new EO.Web.MsgBoxButton("OK"));
			}
		}

		private void PopulaDDLExame()
		{
            string xFiltro = "";

            if ( chk_PCMSO.Checked == true )
            {
                xFiltro = " IndExame=" + (int)IndTipoExame.Complementar +
                          " and IdExameDicionario in " +
                          " ( " +
                          "   select distinct IdExameDicionario from PcmsoPlanejamento " +
                          "   where idghe in " +
                          "   (select top 1 nid_func from " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblfunc_Empregado as xA join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblLaudo_Tec as xB on (xA.nID_LAUD_TEC = xB.nID_LAUD_TEC) where nid_empregado_Funcao in " +
                          "      (select top 1 nid_empregado_Funcao from " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEmpregado_Funcao where nid_Empregado = " + Request["IdEmpregado"].ToString() + " order by hDT_INICIO desc) " +
                          "      order by hdt_Laudo desc " +
                          "    ) " +
                          " ) " +
                          " ORDER BY Nome ";
            }
            else
            {
                xFiltro = " IndExame=" + (int)IndTipoExame.Complementar + " ORDER BY Nome ";
            }


			ddlTipoExame.DataSource = new ExameDicionario().Find(xFiltro);
			ddlTipoExame.DataTextField = "Nome";
			ddlTipoExame.DataValueField = "Id";
			ddlTipoExame.DataBind();
			ddlTipoExame.Items.Insert(0, new ListItem("Selecione o Tipo do Exame...", "0"));
		}

		private void PopulaDDLClinicas(string IdExameDicionario)
		{
            //ddlClinica.DataSource = new Clinica().Get("((IdClinica IN (SELECT IdClinica FROM ClinicaCliente WHERE IdCliente=" + Request["IdEmpresa"].ToString() + " )"
            //    +" AND IdClinica IN (SELECT IdClinica FROM ClinicaExameDicionario WHERE IdExameDicionario=" + IdExameDicionario + ")"
            //    +" AND IdJuridicaPapel=" + (int)IndJuridicaPapel.Clinica + ")"
            //    + " OR (IdClinica IN (SELECT IdClinica FROM ClinicaCliente WHERE IdCliente=" + Request["IdEmpresa"].ToString() + ")"
            //    +" AND IdJuridicaPapel=" + (int)IndJuridicaPapel.ClinicaOutras + "))"
            //    +" AND FazComplementar=1"
            //    +" AND IsInativo=0 ORDER BY NomeAbreviado");

            ddlClinica.DataSource = new Clinica().Get("((IdClinica IN (SELECT IdClinica FROM ClinicaCliente WHERE IdCliente=" + Request["IdEmpresa"].ToString() + " )"
                 + " AND IdClinica IN (SELECT IdClinica FROM ClinicaExameDicionario WHERE IdExameDicionario=" + IdExameDicionario + ")"
                 + " AND IdJuridicaPapel=" + (int)IndJuridicaPapel.Clinica + ")"
                 + " OR (IdClinica IN (SELECT IdClinica FROM ClinicaCliente WHERE IdCliente=" + Request["IdEmpresa"].ToString() + ")"
                 + " AND IdJuridicaPapel=" + (int)IndJuridicaPapel.ClinicaOutras + "))"    
                 + " AND IsInativo=0 ORDER BY NomeAbreviado");
			ddlClinica.DataValueField = "IdClinica";
			ddlClinica.DataTextField = "NomeAbreviado";
			ddlClinica.DataBind();

			ddlClinica.Items.Insert(0, new ListItem("Selecione...", "0"));
            ddlClinica.Items.Insert(1, new ListItem("Ilitera", "310"));
		}


        private void PopulaDDLUnidadeMedida()
        {

            ddlUnidadeMedida.DataSource = new ComplementarUnidadeMedida().Get(" UnidadeMedida <> ''");
            ddlUnidadeMedida.DataValueField = "IdComplementarUnidadeMedida";
            ddlUnidadeMedida.DataTextField = "UnidadeMedida";
            ddlUnidadeMedida.DataBind();

            ddlUnidadeMedida.Items.Insert(0, new ListItem("Selecione...", "0"));
            
        }

        private void PopulaDDLMedico()
		{
			ddlMedico.DataSource = new qryMedico().Get(" ( IdJuridica=" +  ddlClinica.SelectedValue + " or IdJuridica = 310 ) "
                +" AND IsInativo=0"
                +" AND IdMedico IN (SELECT IdJuridicaPessoa FROM JuridicaPessoa WHERE"
                +" IdPessoa IN (SELECT IdPessoa FROM Pessoa WHERE IsInativo=0)) ORDER BY NomeCompleto");
			ddlMedico.DataTextField = "NomeCompleto";
			ddlMedico.DataValueField = "IdMedico";
			ddlMedico.DataBind();

			ddlMedico.Items.Insert(0, new ListItem("Selecione...", "0"));
		}

		protected void ddlClinica_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			PopulaDDLMedico();
		}

		protected void ddlTipoExame_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (!ddlTipoExame.SelectedValue.Equals("0"))
				PopulaDDLClinicas(ddlTipoExame.SelectedValue);
			else
			{
				while (ddlClinica.Items.Count > 1)
					ddlClinica.Items.RemoveAt(1);
			}

			while(ddlMedico.Items.Count > 1)
				ddlMedico.Items.RemoveAt(1);
		}

        protected void cmd_Voltar_Click(object sender, EventArgs e)
        {
            Entities.Usuario user = (Entities.Usuario)Session["usuarioLogado"];

            if (Session["Retorno"].ToString().Trim() == "1")
                Response.Redirect("DadosEmpregado.aspx?IdEmpregado=" + Session["Empregado"].ToString() + "&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString() + "&Tipo=3");
            else
                Response.Redirect("DadosEmpregado_Lista.aspx?IdEmpregado=" + Session["Empregado"].ToString() + "&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString() + "&Tipo=6");

        }






        protected void gridAnamnese_ItemCommand(object sender, EO.Web.GridCommandEventArgs e)
        {
            //Check whether it is from our client side
            //JavaScript call


            //Session["Id2"] = e.Item.Key.ToString();
            //Session["NomeEmpregado"] = e.Item.Cells[0].Value.ToString();



        }



        private void Carregar_Grid_Anamnese_Capgemini()
        {

            if (exameComplementar.Id == 0)
            {
                // criar registros padrão,  mas como criar se idclinico = 0
                return;
            }

            //DataSet ds = new Ilitera.Opsa.Data.Anamnese_Questao().Get(" Questao is not null order by Sistema, Questao ");

            Ilitera.Data.Clientes_Funcionarios zQuest = new Ilitera.Data.Clientes_Funcionarios();

            DataSet ds = zQuest.Trazer_Anamnese_Exame(exameComplementar.Id);


            gridAnamnese.DataSource = ds;
            gridAnamnese.DataBind();


            //string xSistema = "";
            //System.Drawing.Color zColor = new System.Drawing.Color();

            //int zMud = 0;

            //for (int xCont = 0; xCont < gridAnamnese.Items.Count; xCont++)
            //{
            //    if (xSistema != gridAnamnese.Items[xCont].Cells[4].Value.ToString())
            //    {
            //        zMud = zMud + 1;

            //        if (zMud % 2 == 1)
            //            zColor = Color.LightYellow;
            //        else
            //            zColor = Color.White;

            //        xSistema = gridAnamnese.Items[xCont].Cells[4].Value.ToString();

            //    }

            //    //gridAnamnese.Items[xCont].Grid.BackColor = zColor;

            //    //gridAnamnese.Items[xCont].Cells[6].Column.CellStyle.BackColor = zColor;
            //    gridAnamnese.Items[xCont].Grid.BackColor = zColor;
            //}

        }


        private void Carregar_Dados_Anamnese_Exame()
        {


            if (exameComplementar.Id == 0)
            {
                return;
            }



            List<Anamnese_Exame> AnExame = new Anamnese_Exame().Find<Anamnese_Exame>(" IdExameBase = " + exameComplementar.Id);



            if (AnExame.Count == 0)
            {
                //trazer padrão para cliente
                exameComplementar.IdEmpregado.nID_EMPR.Find();
                List<Anamnese_Dinamica> anExamePadrao = new Anamnese_Dinamica().Find<Anamnese_Dinamica>(" IdPessoa = " + exameComplementar.IdEmpregado.nID_EMPR.Id);


                if (anExamePadrao.Count == 0)
                {
                    return;
                }
                else
                {

                    Ilitera.Data.Clientes_Funcionarios xAnam = new Ilitera.Data.Clientes_Funcionarios();
                    xAnam.Carregar_Anamnese_Dinamica(exameComplementar.IdEmpregado.nID_EMPR.Id, exameComplementar.Id);

                    //foreach (Anamnese_Dinamica zPadrao in anExamePadrao)
                    //{
                    //    Anamnese_Exame rTestes = new Anamnese_Exame();

                    //    rTestes.IdAnamneseDinamica = zPadrao.Id;
                    //    rTestes.IdExameBase = exameComplementar.Id;
                    //    rTestes.Resultado = "N";
                    //    rTestes.Peso = zPadrao.Peso;
                    //    rTestes.Save();
                    //}

                }

            }


            return;


        }

        protected void cmd_Anamnese_Click(object sender, EventArgs e)
        {

            Int32 zKey = 0;
            string zResultado = "N";


            if (gridAnamnese.ChangedItems.Length == 0)
            {
                MsgBox1.Show("Ilitera.Net", "Não houve alterações na Anamnese", null,
                new EO.Web.MsgBoxButton("OK"));
                return;
            }

            else
            {
                foreach (EO.Web.GridItem item in gridAnamnese.ChangedItems)
                {
                    foreach (EO.Web.GridCell cell in item.Cells)
                    {
                        if (cell.Modified)
                        {
                            //string text = string.Format(
                            //    "Cell Changed: Key = {0}, Field = {1}, New Value = {2}",
                            //    item.Key,
                            //    cell.Column.DataField,
                            //    cell.Value);
                            zKey = System.Convert.ToInt32(item.Key);
                            zResultado = cell.Value.ToString().Trim();


                            Ilitera.Data.Clientes_Funcionarios zAtualiza = new Ilitera.Data.Clientes_Funcionarios();

                            zAtualiza.Atualizar_Anamnese_Dinamica(zKey, zResultado.Substring(0, 1));

                        }
                    }
                }

                MsgBox1.Show("Ilitera.Net", "Alterações na Anamnese salva.", null,
                new EO.Web.MsgBoxButton("OK"));
                return;

            }

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

        protected void chk_PCMSO_CheckedChanged(object sender, EventArgs e)
        {

            PopulaDDLExame();

        }
    }
}
