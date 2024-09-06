using System;
using System.Data;
using System.Web;
using System.Web.SessionState;
using System.Web.UI.WebControls;
using Ilitera.Common;
using System.Text;
using Ilitera.Opsa.Data;
using Ilitera.Data;
using System.Web.UI;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using Facade;
using System.Configuration;
using System.Linq;
using System.Web.Security;

using System.Xml.Linq;

using Entities;
using BLL;

//  ter um grid com os CAs já indicados para este funcionário

//  idéia será exibir lista de PCMSOs
//  selecionou PCMSO,  exibir EPIs em combo
//  selecionou EPI,  colocar dados do CA, data entrega
//  salvar em nova tabela, com IdPCMSO, IdEPI, nIdEmpregado, dDtEntrega, CA

namespace Ilitera.Net
{
    public partial class DadosEmpregado_CA : System.Web.UI.Page
    {

        protected System.Web.UI.WebControls.TextBox txtPagina;
        protected System.Web.UI.WebControls.TextBox txtBuscaEmpresa;
        protected System.Web.UI.HtmlControls.HtmlTableCell TDGridListaEmpregado;
        protected System.Web.UI.HtmlControls.HtmlAnchor Principal;
        protected System.Web.UI.HtmlControls.HtmlTableCell NavBar;


        protected Prestador prestador = new Prestador();
        protected Ilitera.Common.Usuario usuario = new Ilitera.Common.Usuario();
        protected Cliente cliente = new Cliente();
        protected Ilitera.Opsa.Data.Empregado empregado = new Ilitera.Opsa.Data.Empregado();

		protected void Page_Load(object sender, System.EventArgs e)
		{
			InicializaWebPageObjects();
            //PreencheLabels("lblEmp", cliente.NomeAbreviado.ToString());

            string xUsuario = Session["usuarioLogado"].ToString();
            PopulaTelaEmpregado();

            if (opt_Eventual.Checked == true)
            {
                cmb_Laudo.Enabled = false;
                cmb_EPI.Visible = false;
                cmb_EPI2.Visible = true;
            }
            else
            {
                cmb_Laudo.Enabled = true;
                cmb_EPI.Visible = true;
                cmb_EPI2.Visible = false;
            }



			if (!IsPostBack)
			{

                cmd_Excluir.Attributes.Add("onClick", "javascript:return confirm('Você deseja realmente excluir este CA para o EPI indicado ?');");
                PopulaGrid();


                cmb_EPI2.Items.Clear();
                cmb_EPI2.DataSource = Carregar_Todos_EPIs();
                cmb_EPI2.DataTextField = "Nome";
                cmb_EPI2.DataValueField = "IdEPI";
                cmb_EPI2.DataBind();


                //PopularFindClinica();
                PopulaDDLPCMSO();
                //PopularGridClinicaCredenciada();


				//GetMenu(((int)IndMenuType.Empregado).ToString(), Request["IdUsuario"].ToString(), Request["IdEmpresa"].ToString(), Request["IdEmpregado"].ToString());

				//StringBuilder st = new StringBuilder();


				//st.Append("var URL = \"PastaExClinico.aspx?IdUsuario=\" + top.window.document.frames.conteudo.window.document.getElementById('txtIdUsuario').value + \"&"
				//	+"IdEmpresa=\" + top.window.document.frames.conteudo.window.document.getElementById('txtIdEmpresa').value + \"&"
				//	+"IdEmpregado=\" + top.window.document.frames.conteudo.window.document.getElementById('txtIdEmpregado').value;"); 

                //st.Append("var URL = \"PastaExClinico.aspx?IdUsuario=\" + top.window.document.getElementById('txtIdUsuario').value + \"&"
                //    + "IdEmpresa=\" + top.window.document.getElementById('txtIdEmpresa').value + \"&"
                //    + "IdEmpregado=\" + top.window.document.getElementById('txtIdEmpregado').value;"); 

                //st.Append("var IFrameObj = document.frames['SubDados'];");
                //st.Append("IFrameObj.document.location.replace(URL);");

                //this.ClientScript.RegisterStartupScript(this.GetType(), "ChangeFrameSrc", st.ToString(), true);
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




        private void PopulaDDLPCMSO()
        {


            DataSet ds = new LaudoTecnico().GetLaudosTecnicos(Session["Empresa"].ToString());


            cmb_Laudo.DataSource = ds;
            cmb_Laudo.DataValueField = "IdLaudoTecnico";
            cmb_Laudo.DataTextField = "DataLaudo";
            cmb_Laudo.DataBind();

            


            //DataSet ds = new DataSet();
            //DataRow rowds;

            //DataTable table = new DataTable("Default");

            //table.Columns.Add("IdPcmso", Type.GetType("System.String"));
            //table.Columns.Add("DataPcmso", Type.GetType("System.String"));

            //ds.Tables.Add(table);

            //DataSet dsaux = new Pcmso().Get("IdCliente=" + Session["Empresa"].ToString()
            //    + " AND IsFromWeb=0"
            //    + " ORDER BY DataPcmso DESC");

            //foreach (DataRow row in dsaux.Tables[0].Select())
            //{
            //    rowds = ds.Tables[0].NewRow();

            //    Pcmso pcmso = new Pcmso();
            //    pcmso.Find(Convert.ToInt32(row["IdPcmso"]));

            //    rowds["IdPcmso"] = row["IdPcmso"];
            //    rowds["DataPcmso"] = pcmso.GetPeriodo();

            //    ds.Tables[0].Rows.Add(rowds);
            //}

            //cmb_Laudo.DataSource = ds;
            //cmb_Laudo.DataValueField = "IdPCMSO";
            //cmb_Laudo.DataTextField = "DataPcmso";
            //cmb_Laudo.DataBind();

            if (cmb_Laudo.Items.Count > 0)
            {
                cmb_Laudo.SelectedIndex = 0;
                PopularValueListEPI(cmb_Laudo.SelectedValue);
            }    
            
        }
        
        

        protected void cmb_Laudo_SelectedIndexChanged(object sender, EventArgs e)
        {

            PopularValueListEPI(cmb_Laudo.SelectedValue);
       
        }


        protected void cmb_EPI_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        //private void PopularValueListEPI(string xValue)
        //{
        //    //DataSet dsExames = new ExameDicionario().GetIdNome("Nome", " IdExameDicionario IN (SELECT IdExameDicionario FROM ClinicaExameDicionario WHERE IdClinica IN (SELECT IdClinica FROM ClinicaCliente WHERE IdCliente=" + Request["IdEmpresa"] + " and IdClinica = " + xValue + " ))");
        //    //DataSet ds = new ClinicaExameDicionario().Get("IdClinica IN (SELECT IdClinica FROM ClinicaCliente WHERE IdCliente=" + Request["IdEmpresa"] + " "  + " and IdClinica = " + xValue +  " ) " + " AND IDCLINICAEXAMEDICIONARIO IN " +
        //    // "( " +
        //    // "   SELECT IdClinicaExameDicionario " + 
        //    // "   FROM ClinicaClienteExameDicionario  " + 
        //    // "    WHERE IdClinicaCliente IN ( " + 
        //    // "      SELECT IdClinicaCliente FROM ClinicaCliente " + 
        //    // "      WHERE IdCliente=" + Request["IdEmpresa"] + " "  + " and IdClinica = " + xValue + " and IsAutorizado = 1 ) ) ");


        //    ////pegar exames de PCMSO do funcionário
        //    empregado.Find(" tblEMPREGADO.nID_EMPREGADO = " + Request["IdEmpregado"].ToString());


        //    //está pegando apenas última classif.funcional
        //    Clinico exame = new Clinico();
        //    exame.IdEmpregadoFuncao = EmpregadoFuncao.GetEmpregadoFuncao(empregado);
        //    exame.IdPcmso = exame.IdEmpregadoFuncao.nID_EMPR.GetUltimoPcmso();

            

        //    exame.UsuarioId = System.Convert.ToInt32(Request["IdUsuario"].ToString());


        //    //wagner
        //    //carregar combo com lista de PPRAs ( ver relatório PPRA para ver método para carga )
        //    //xValue vai ter o valor do laudo PPRA.  Não precisarei mais criar objeto PCMSO para chegar ao laudo.
        //    // alterar SP_Buscar_CAs e SP_Inserir_Dados_CA_EPI :  colocar nIdLaudTec no lugar de IdPCMSO ??

        //    LaudoTecnico xLaudo = new LaudoTecnico();
        //    xLaudo.Find( System.Convert.ToInt32( xValue ) );


        //    //Pcmso pcmso = new Pcmso();
        //    //pcmso.Find(System.Convert.ToInt32(xValue));

        //    //if (pcmso.IdLaudoTecnico != null)
        //    //{
        //    if( xLaudo != null )
        //    {
        //        //List<Ghe> ghes = new Ghe().Find<Ghe>("nID_LAUD_TEC=" + pcmso.IdLaudoTecnico.Id);
        //        List<Ghe> ghes = new Ghe().Find<Ghe>("nID_LAUD_TEC=" + xLaudo.Id);

        //        //preciso fazer código caso tenha mais de 1 GHE



        //        Ghe ghe;

        //        if (ghes == null || ghes.Count == 0)
        //            ghe = exame.IdEmpregadoFuncao.GetGheEmpregado(xLaudo);
        //        else
        //        {
        //            int IdGhe = exame.IdEmpregadoFuncao.GetIdGheEmpregado(xLaudo);

        //            ghe = ghes.Find(delegate(Ghe g) { return g.Id == IdGhe; });
        //        }


        //        if (ghe != null)
        //        {
        //            //pegar listagem EPIs
        //            List<PPRA> listPPRA = new PPRA().Find<PPRA>("nID_FUNC=" + ghe.Id
        //                                   + " AND nID_FUNC IS NOT NULL ORDER BY nID_RSC");

        //            if (ghe.mirrorOld == null)
        //                ghe.Find();

        //            cmb_EPI.Items.Clear();

        //            foreach (PPRA ppra in listPPRA)
        //            {

        //                //Pcmso xPcmso = new Pcmso();
        //                //xPcmso.Find("IdLaudoTecnico = " + ppra.Id);
                                               
        //                //exame.IdPcmso = xPcmso;

        //                if (ppra.mirrorOld == null)
        //                    ppra.Find();

        //                string xEPI = ppra.GetEpi();


        //                int xPosit = 0;

        //                xPosit = xEPI.IndexOf("\n");

        //                if (xPosit > 0)
        //                {

        //                    //ver quebras de linha 
        //                    while (xPosit > 0)
        //                    {

        //                        string xItem = xEPI.Substring(0, xPosit);
        //                        xEPI = xEPI.Substring(xPosit + 1);

        //                        if (xItem.Trim().ToUpper() != "INEXISTENTE")
        //                        {
        //                            cmb_EPI.Items.Add(xItem);
        //                        }

        //                        xPosit = xEPI.IndexOf("\n");

        //                        if (xPosit < 1)
        //                        {
        //                            if (xEPI.Trim() != "")
        //                            {
        //                                if (xEPI.Trim().ToUpper() != "INEXISTENTE")
        //                                {
        //                                    cmb_EPI.Items.Add(xEPI);
        //                                }
        //                            }
        //                        }
        //                    }
        //                }
        //                else
        //                {
        //                    if (xEPI.Trim().ToUpper() != "INEXISTENTE")
        //                    {
        //                        cmb_EPI.Items.Add(xEPI);
        //                    }
        //                }
        //            }

        //        }

        //    }
        //    else
        //    {
        //        cmb_EPI.Items.Clear();
        //    }

        //        //if (lst_Exames.Items.Count < 1)
        //    //{
        //        //    btnemp.Enabled = false;
        //        //}
        //        //else
        //        //{
        //        btnSalvar.Enabled = true;
        //        //}

        //        //lst_Exames.Enabled = false;
            
        //}


        private void PopularValueListEPI(string xValue)
        {
           
            ////pegar exames de PCMSO do funcionário
            empregado.Find(" tblEMPREGADO.nID_EMPREGADO = " + Request["IdEmpregado"].ToString());


            //está pegando apenas última classif.funcional
            Clinico exame = new Clinico();


            cmb_EPI.Items.Clear();


     
            LaudoTecnico xLaudo = new LaudoTecnico();
            xLaudo.Find(System.Convert.ToInt32(xValue));

            //PPRA xPPRA = new PPRA();                                    
            //xPPRA.Find(System.Convert.ToInt32(xValue));


            Pcmso xPcmso = new Pcmso();
            xPcmso.Find("IdLaudoTecnico = " + System.Convert.ToInt32(xValue));

            exame.IdPcmso = xPcmso;
            exame.IdEmpregadoFuncao = EmpregadoFuncao.GetEmpregadoFuncao(xLaudo, empregado);


            exame.UsuarioId = System.Convert.ToInt32(Request["IdUsuario"].ToString());




            //Pcmso pcmso = new Pcmso();
            //pcmso.Find(System.Convert.ToInt32(xValue));

            //if (pcmso.IdLaudoTecnico != null)
            //{
            if (xLaudo != null)
            {
                //List<Ghe> ghes = new Ghe().Find<Ghe>("nID_LAUD_TEC=" + pcmso.IdLaudoTecnico.Id);
                List<Ghe> ghes = new Ghe().Find<Ghe>("nID_LAUD_TEC=" + xLaudo.Id);

                //preciso fazer código caso tenha mais de 1 GHE



                Ghe ghe;

                if (ghes == null || ghes.Count == 0)
                    ghe = exame.IdEmpregadoFuncao.GetGheEmpregado(xLaudo);
                else
                {
                    int IdGhe = exame.IdEmpregadoFuncao.GetIdGheEmpregado(xLaudo);

                    ghe = ghes.Find(delegate(Ghe g) { return g.Id == IdGhe; });
                }


                if (ghe != null)
                {
                    //pegar listagem EPIs
                    List<PPRA> listPPRA = new PPRA().Find<PPRA>("nID_FUNC=" + ghe.Id
                                           + " AND nID_FUNC IS NOT NULL ORDER BY nID_RSC");

                    if (ghe.mirrorOld == null)
                        ghe.Find();

                    cmb_EPI.Items.Clear();

                    foreach (PPRA ppra in listPPRA)
                    {

                        //Pcmso xPcmso = new Pcmso();
                        //xPcmso.Find("IdLaudoTecnico = " + ppra.Id);

                        //exame.IdPcmso = xPcmso;

                        if (ppra.mirrorOld == null)
                            ppra.Find();

                        string xEPI = ppra.GetEpi();


                        int xPosit = 0;
                                                

                        xPosit = xEPI.IndexOf("\n");

                        if (xPosit > 0)
                        {

                            //ver quebras de linha 
                            while (xPosit > 0)
                            {

                                string xItem = xEPI.Substring(0, xPosit);
                                xEPI = xEPI.Substring(xPosit + 1);

                                if (xItem.Trim().ToUpper() != "INEXISTENTE")
                                {
                                                                                //checar se EPI já foi inserido
                                    int xLoc = 0;                                            

                                    for (int zCont = 0; zCont < cmb_EPI.Items.Count; zCont++)
                                    {
                                        if (cmb_EPI.Items[zCont].ToString().Trim() == xItem.Trim())
                                            xLoc = 1;
                                    }

                                    if (xLoc == 0)
                                    {
                                        cmb_EPI.Items.Add(xItem);
                                    }
                                }

                                xPosit = xEPI.IndexOf("\n");

                                if (xPosit < 1)
                                {
                                    if (xEPI.Trim() != "")
                                    {
                                        if (xEPI.Trim().ToUpper() != "INEXISTENTE")
                                        {

                                            //checar se EPI já foi inserido
                                            int xLoc = 0;                                            

                                            for (int zCont = 0; zCont < cmb_EPI.Items.Count; zCont++)
                                            {
                                                if (cmb_EPI.Items[zCont].ToString().Trim() == xEPI.Trim())
                                                    xLoc = 1;
                                            }

                                            if (xLoc == 0)
                                            {
                                                cmb_EPI.Items.Add(xEPI);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (xEPI.Trim().ToUpper() != "INEXISTENTE")
                            {
                                                                                                                //checar se EPI já foi inserido
                                    int xLoc = 0;                                            

                                    for (int zCont = 0; zCont < cmb_EPI.Items.Count; zCont++)
                                    {
                                        if (cmb_EPI.Items[zCont].ToString().Trim() == xEPI.Trim())
                                            xLoc = 1;
                                    }

                                    if (xLoc == 0)
                                    {
                                        cmb_EPI.Items.Add(xEPI);
                                    }


                            }
                        }
                    }

                }

            }
            else
            {
                cmb_EPI.Items.Clear();
            }

            //if (lst_Exames.Items.Count < 1)
            //{
            //    btnemp.Enabled = false;
            //}
            //else
            //{
            btnSalvar.Enabled = true;
            //}

            //lst_Exames.Enabled = false;

        }



		private void PopulaTelaEmpregado()
		{
            //variável empregado está vazia.  Ver como carregá-lo. - Wagner  
            empregado.Find(" tblEMPREGADO.nID_EMPREGADO = " + Request["IdEmpregado"].ToString() );
			EmpregadoFuncao empregadoFuncao = EmpregadoFuncao.GetEmpregadoFuncao(empregado);
            
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
        

        private void PopulaGrid()
        {
            gridEmpregados.DataSource = GeraDataSet();
            gridEmpregados.DataBind();
        }


        private DataSet GeraDataSet()
        {

            DataSet ds;

            Ilitera.Data.PPRA_EPI xEPI = new Ilitera.Data.PPRA_EPI();

            ds = xEPI.Trazer_CAs(System.Convert.ToInt32( Request["IdEmpregado"].ToString()));

            return ds;
        }



        private DataSet Carregar_Todos_EPIs()
        {

            DataSet ds;

            Ilitera.Data.PPRA_EPI xEPI = new Ilitera.Data.PPRA_EPI();

            ds = xEPI.Trazer_EPIs();

            return ds;
        }



        //protected void gridEmpregados_CellSelectionChanged(object sender, SelectedCellEventArgs e)
        //{

        //    string xId = "0";

        //    //try
        //    //{
        //        if (e.CurrentSelectedCells.Count >= 0)
        //        {
        //            xId = e.CurrentSelectedCells[0].Row.DataKey.GetValue(0).ToString();
        //            lbl_ID.Text = xId;
        //        }
        //    //}
        //    //catch (Exception ex)
        //    //{
        //    //    this.ClientScript.RegisterStartupScript(this.GetType(), "alerta", String.Format("alert('{0}');", ex.Message), true);
        //    //}
        //}

        protected void cmd_Excluir_Click(object sender, EventArgs e)
        {

           if (  lbl_ID.Text.Trim() != "0" )
            {


                Ilitera.Data.PPRA_EPI xEPI = new Ilitera.Data.PPRA_EPI();

                xEPI.Excluir_CA_EPI(System.Convert.ToInt32(lbl_ID.Text)); 


                PopulaGrid();

            }
                

        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {


            string zMotivo = "";
            string zHabitual_Eventual = "";

            if (opt_Eventual.Checked == true)
            {
                zHabitual_Eventual = "E";
            }
            else
            {
                zHabitual_Eventual = "H";
            }

            if (rd_Admissao.Checked == true)
            {
                zMotivo = "A";
            }
            else if (rd_Devolucao.Checked == true)
            {
                zMotivo = "D";
            }
            else if (rd_Mudanca.Checked == true)
            {
                zMotivo = "M";
            }
            else if (rd_Perda.Checked == true)
            {
                zMotivo = "P";
            }
            else if (rd_Quebrado.Checked == true)
            {
                zMotivo = "Q";
            }
            else if (rd_Substituicao.Checked == true)
            {
                zMotivo = "S";
            }

            Ilitera.Data.PPRA_EPI xEPI = new Ilitera.Data.PPRA_EPI();

            if (opt_Eventual.Checked == true)
            {
                xEPI.Inserir_Dados_CA_EPI(System.Convert.ToInt32(cmb_Laudo.SelectedValue.ToString()), cmb_EPI2.SelectedItem.ToString(), System.Convert.ToInt32(Request["IdEmpregado"].ToString()), txt_CA.Text, txt_Data.Text, txt_Descricao.Text, txt_Conjunto.Text, txt_Tamanho.Text, txt_Tipo.Text, zMotivo, zHabitual_Eventual);
            }
            else
            {
                xEPI.Inserir_Dados_CA_EPI(System.Convert.ToInt32(cmb_Laudo.SelectedValue.ToString()), cmb_EPI.SelectedItem.ToString(), System.Convert.ToInt32(Request["IdEmpregado"].ToString()), txt_CA.Text, txt_Data.Text, txt_Descricao.Text, txt_Conjunto.Text, txt_Tamanho.Text, txt_Tipo.Text, zMotivo, zHabitual_Eventual);
            }

            PopulaGrid();


            txt_CA.Text = "";
            txt_Conjunto.Text = "";
            txt_Data.Text = "";
            txt_Descricao.Text = "";
            txt_Tamanho.Text = "";
            txt_Tipo.Text = "";

            rd_Substituicao.Checked = true;


        }


        protected void gridEmpregados_ItemCommand(object sender, EO.Web.GridCommandEventArgs e)
        {

            //Session["Empregado"] = e.Item.Key.ToString();
            //Session["NomeEmpregado"] = e.Item.Cells[0].Value.ToString();

            string xId = "";

            xId = e.Item.Key.ToString();
            lbl_ID.Text = xId;

        }

	}

}
