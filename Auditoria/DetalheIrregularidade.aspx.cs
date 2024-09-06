using System;
using System.IO;
using System.Data;
using System.Web.SessionState;
using System.Web.UI.WebControls;
using Ilitera.Common;
using Ilitera.Opsa.Data;
using System.Text;

using System.Web;
using System.Web.UI;

using System.Collections;


namespace Ilitera.Net
{
	/// <summary>
	/// Summary description for ListaEmpresa.
	/// </summary>
    public partial class DetalheIrregularidade : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label lblEmpresa;
		private Irregularidade irregula;
		protected System.Web.UI.WebControls.Label lblIniRegu;
		protected System.Web.UI.WebControls.Label lblPrevRegu;
		protected System.Web.UI.WebControls.Label lblDataRegu;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Label lblTipo;
		protected System.Web.UI.WebControls.DropDownList ddlTipo;
		protected System.Web.UI.WebControls.Label lblRespAdm;
		protected System.Web.UI.WebControls.Label lblRespOpe;
		protected System.Web.UI.WebControls.Label lblObservacao;
		protected System.Web.UI.WebControls.TextBox txtObservacao;
		protected System.Web.UI.WebControls.DropDownList ddlRespAdm;
		protected System.Web.UI.WebControls.DropDownList ddlRespOper;
		protected System.Web.UI.WebControls.Label lblObjetivo;
		protected System.Web.UI.WebControls.TextBox txtObjetivo;
		//protected Infragistics.WebUI.UltraWebTab.UltraWebTab uwtRespons;
		protected System.Web.UI.WebControls.Button btnSalvar;
		protected System.Web.UI.WebControls.TextBox txtCivil;
		protected System.Web.UI.WebControls.TextBox txtPenal;
		protected System.Web.UI.WebControls.TextBox txtTrabalhista;
		protected System.Web.UI.WebControls.TextBox txtPrevid;
		protected System.Web.UI.WebControls.TextBox txtAmbiental;
		protected System.Web.UI.WebControls.TextBox txtNormativa;
		protected System.Web.UI.WebControls.Label lblOrcamento;
		protected System.Web.UI.WebControls.Label lblCustoFinal;
		//protected Infragistics.WebUI.WebDataInput.WebCurrencyEdit txtOrcamento;
		//protected Infragistics.WebUI.WebDataInput.WebCurrencyEdit txtCustoFinal;
		private System.Web.UI.WebControls.Label lblUFIRMinimo;
		private System.Web.UI.WebControls.Label lblUFIRMaximo;
		private System.Web.UI.WebControls.Label lblReaisMinimo;
		private System.Web.UI.WebControls.Label lblReaisMaximo;
		
		protected void Page_Load(object sender, System.EventArgs e)
		{
            string xUsuario = Session["usuarioLogado"].ToString();
            //InicializaWebPageObjects();
            Carregar_Irregula();
			

			if (!IsPostBack)
			{
				PopulaDDLTipo();
				PopulaDDLResp();
				PopulaTela();
				//imgIrregularidade.Attributes.Add("onClick", "javascript:void(addItemPop(centerWin('ImgIrregularidade.aspx?PathImg=" + imgIrregularidade.ImageUrl + "', 400, 300,\'ImageIrregula\')));");
			}
			else if (txtAuxiliar.Value.Equals("atualizaColaborador"))
			{
				txtAuxiliar.Value = string.Empty;
                PopulaDDLResp();
			}
            else if (txtAuxiliar.Value.Equals("atualizaTipoIrregularidade"))
            {
                txtAuxiliar.Value = string.Empty;
                PopulaDDLTipo();
            }
		}

		#region Inicialização de componentes do WebTab

        protected void InicializaComponentes()
        {
            lblIniRegu = (Label)FindControl("lblIniRegut");
            lblPrevRegu = (Label)FindControl("lblPrevRegut");
            lblDataRegu = (Label)FindControl("lblDataRegut");
            lblTipo = (Label)FindControl("lblTipot");
            lblOrcamento = (Label)FindControl("lblOrcamentot");
            lblCustoFinal = (Label)FindControl("lblCustoFinalt");
            ddlTipo = (DropDownList)FindControl("ddlTipot");
            //txtOrcamento = (TextBox)FindControl("txtOrcamentot");
            //txtCustoFinal = (TextBox)FindControl("txtCustoFinalt");
            lblRespAdm = (Label)FindControl("lblRespAdmt");
            lblRespOpe = (Label)FindControl("lblRespOpet");
            ddlRespAdm = (DropDownList)FindControl("ddlRespAdmt");
            ddlRespOper = (DropDownList)FindControl("ddlRespOpert");
            lblObjetivo = (Label)FindControl("lblObjetivot");
            lblObservacao = (Label)FindControl("lblObservacaot");
            txtObjetivo = (TextBox)FindControl("txtObjetivot");
            txtObservacao = (TextBox)FindControl("txtObservacaot");
            btnSalvar = (Button)FindControl("btnSalvart");
            //uwtRespons = (TextBox)FindControl("uwtResponst");
            txtCivil = (TextBox)FindControl("txtCivilt");
            txtPenal = (TextBox)FindControl("txtPenalt");
            txtTrabalhista = (TextBox)FindControl("txtTrabalhistat");
            txtPrevid = (TextBox)FindControl("txtPrevidt");
            txtAmbiental = (TextBox)FindControl("txtAmbientalt");
            txtNormativa = (TextBox)FindControl("txtNormativat");
            lblUFIRMinimo = (Label)FindControl("tablblUFIRMinimo");
            lblUFIRMaximo = (Label)FindControl("tablblUFIRMaximo");
            lblReaisMinimo = (Label)FindControl("tablblReaisMinimo");
            lblReaisMaximo = (Label)FindControl("tablblReaisMaximo");
        }

		protected void InicializaEventosDosComponentes()
		{
            this.ddlTipo.SelectedIndexChanged += new System.EventHandler(this.ddlTipo_SelectedIndexChanged);
            this.ddlRespAdm.SelectedIndexChanged += new System.EventHandler(this.ddlRespAdm_SelectedIndexChanged);
            this.ddlRespOper.SelectedIndexChanged += new System.EventHandler(this.ddlRespOper_SelectedIndexChanged);
            this.btnSalvar.Click += new System.EventHandler(this.btnSalvar_Click);
		}
		#endregion

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InicializaComponentes();
			InicializaEventosDosComponentes();
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.ID = "DetalheIrregularidade";

		}
		#endregion

        //protected override void  InicializaWebPageObjects()
        private void Carregar_Irregula()
        {
 	       //base.InicializaWebPageObjects();

			irregula = new Irregularidade(Convert.ToInt32(Request.QueryString["IdIrregularidade"]));
			irregula.IdAuditoria.Find();
			irregula.IdNorma.Find();
		}

		private void PopulaDDLResp()
		{
            Cliente cliente;
            cliente = new Cliente(System.Convert.ToInt32(Session["Empresa"].ToString()));

			ArrayList prestadores = new Prestador().GetListaPrestador(cliente, false, (int)TipoPrestador.ContatoEmpresa);
			prestadores.Sort();
			ddlRespAdm.DataSource = prestadores;
			ddlRespAdm.DataValueField = "Id";
			ddlRespAdm.DataTextField = "NomeCompleto";
			ddlRespAdm.DataBind();

			ddlRespOper.DataSource = prestadores;
			ddlRespOper.DataValueField = "Id";
			ddlRespOper.DataTextField = "NomeCompleto";
			ddlRespOper.DataBind();

			ddlRespAdm.Items.Insert(0, new ListItem("Selecione o Responsável Administrativo...", "0"));
			ddlRespAdm.Items.Insert(ddlRespAdm.Items.Count, new ListItem("Cadastre um novo Responsável...", "Novo"));
			ddlRespOper.Items.Insert(0, new ListItem("Selecione o Responsável Operacional...", "0"));
			ddlRespOper.Items.Insert(ddlRespOper.Items.Count, new ListItem("Cadastre um novo Responsável...", "Novo"));
		}
		
		private void PopulaDDLTipo()
		{

            Cliente cliente;
            cliente = new Cliente(System.Convert.ToInt32(Session["Empresa"].ToString()));

			DataSet ds = new TipoIrregularidade().Get("IdCliente=" + cliente.Id + " ORDER BY Nome");

			ddlTipo.DataSource = ds;
			ddlTipo.DataValueField = "IdTipoIrregularidade";
			ddlTipo.DataTextField = "Nome";
			ddlTipo.DataBind();

			ddlTipo.Items.Insert(0, new ListItem("Selecione um Tipo...", "0"));
			ddlTipo.Items.Insert(ddlTipo.Items.Count, new ListItem("Cadastre um novo Tipo...", "Novo"));
		}
		
		private void PopulaTela()
		{            
            int indexAl = 0;
            ViewState["IndexFoto"] = 0;

            ArrayList alIrregularidadeFotoLocal = new IrregularidadeFotoLocal().Find("IdIrregularidade=" + irregula.Id
                +" ORDER BY (SELECT NomeLocal FROM LocalIrregularidade WHERE LocalIrregularidade.IdLocalIrregularidade=IrregularidadeFotoLocal.IdLocalIrregularidade)");

            foreach (IrregularidadeFotoLocal irregularidadeFotoLocal in alIrregularidadeFotoLocal)
            {
                if (irregularidadeFotoLocal.IsFotoPadrao)
                {
                    ViewState["IndexFoto"] = indexAl;
                    break;
                }

                indexAl++;
            }

            if (irregula.IdNorma.CodigoItem.IndexOf(".") >= 0)
               lblTitulo.Text = "Irregularidade referente ao Item " + irregula.IdNorma.CodigoItem + " da NR " + irregula.IdNorma.CodigoItem.Substring(0, irregula.IdNorma.CodigoItem.IndexOf(".")) + ", constatada na Auditoria de " + irregula.IdAuditoria.DataLevantamento.ToString("dd-MM-yyyy");
            else
                lblTitulo.Text = "Irregularidade referente ao Item " + irregula.IdNorma.CodigoItem + " da NR " + irregula.IdNorma.CodigoItem + ", constatada na Auditoria de " + irregula.IdAuditoria.DataLevantamento.ToString("dd-MM-yyyy");

            //try
            //{
            //             //FileInfo fileFoto = new FileInfo(Fotos.PathFoto(irregula.IdAuditoria, Convert.ToInt32(((IrregularidadeFotoLocal)alIrregularidadeFotoLocal[Convert.ToInt32(ViewState["IndexFoto"])]).NumeroFoto)));

            //             //if (!fileFoto.Exists)
            //             //    throw new Exception("O arquivo '" + fileFoto.Name + "'não existe ou não é possível acessá-lo!");

            //             string xArquivo = Fotos.PathFoto(irregula.IdAuditoria, Convert.ToInt32(((IrregularidadeFotoLocal)alIrregularidadeFotoLocal[Convert.ToInt32(ViewState["IndexFoto"])]).NumeroFoto));

            //             if (xArquivo.ToUpper().IndexOf("FOTOSDOCSDIGITAIS") < 1)
            //             {
            //                 xArquivo = xArquivo.Substring(0, xArquivo.IndexOf("\\") + 1) + "FOTOSDOCSDIGITAIS\\" + xArquivo.Substring(xArquivo.IndexOf("\\") + 1);
            //             }

            //             xArquivo = Ilitera.Common.Fotos.PathFoto_Uri(xArquivo);

            //             string imageUrl = xArquivo;
            //             //string imageUrl = fileFoto.FullName;
            //             //imageUrl = imageUrl.Replace(@"\\ClusterMestra\DocsDigitais$", "../DocsDigitais");
            //	//imageUrl = imageUrl.Replace(@"\", "/");                
            //	//imgIrregularidade.ImageUrl = imageUrl;
            //}
            //catch (Exception ex)
            //{
            //	System.Diagnostics.Debug.WriteLine(ex.Message);
            //	System.Diagnostics.Trace.WriteLine(ex.Message);
            //	//imgIrregularidade.ImageUrl = "img/fotoaud.gif";
            //}

            if (alIrregularidadeFotoLocal.Count > 1)
            {
                imbMovePrev.Visible = ((int)ViewState["IndexFoto"] >= 1);
                imbMoveNext.Visible = (((int)ViewState["IndexFoto"] + 1) < alIrregularidadeFotoLocal.Count);
            }

            txtNaoConformidadeLegal.Text = irregula.IdNorma.NaoConformidadeLegal; 
            txtEnquadramentoLegal.Text = irregula.IdNorma.GetEnquadramentoLegal();
            txtLocal.Text = irregula.strLocalIrregularidade();
			txtDescricao.Text = irregula.strAcoesExecutar();
            txtParecerJuridico.Text = irregula.IdNorma.ParecerJuridico;

            System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");
            
            wdtInicioReg.Text = irregula.DataInicioRegul.ToString("dd/MM/yyyy", ptBr );
            wdtPrevisaoReg.Text = irregula.DataPrevisaoRegul.ToString("dd/MM/yyyy", ptBr);
            wdtRegularizacao.Text = irregula.DataFinalRegul.ToString("dd/MM/yyyy", ptBr);

			ddlTipo.Items.FindByValue(irregula.IdTipoIrregularidade.Id.ToString()).Selected = true;
			ViewState["ddlValorBanco"] = irregula.IdTipoIrregularidade.Id.ToString();
            ddlRespAdm.Items.FindByValue(irregula.IdRespAdm.Id.ToString()).Selected = true;
            ViewState["ddlRespAdmValorBanco"] = irregula.IdRespAdm.Id.ToString();
            ddlRespOper.Items.FindByValue(irregula.IdRespOpe.Id.ToString()).Selected = true;
            ViewState["ddlRespOperValorBanco"] = irregula.IdRespOpe.Id.ToString();

            //txtOrcamento.Text = irregula.Orcamento.ToString() ;
            //txtCustoFinal.Text = irregula.CustoFinal.ToString();
			
            txtObservacao.Text = irregula.ObservacaoRegul;
			txtObjetivo.Text = irregula.ObjetivoRegul;

            //txtCivil.Text = irregula.strResponsabilidadeCivil();
			txtPenal.Text = irregula.IdNorma.RespPenal;
			txtTrabalhista.Text = irregula.IdNorma.RespTrabalhista;
			txtPrevid.Text = irregula.IdNorma.RespPrevidenciaria;
			txtAmbiental.Text = irregula.IdNorma.RespAmbiental;
			txtNormativa.Text = irregula.IdNorma.RespNormativa;

            Cliente cliente;
            cliente = new Cliente(System.Convert.ToInt32(Session["Empresa"].ToString()));

			Infracao infracao = new Infracao(irregula.GetIdInfracaoValorMulta(cliente.GetEmpregadosAtivos(), irregula.IdNorma.CodigoInfracao));
			ValoresUfir valorUfir = new ValoresUfir();
			valorUfir.Find("Ano=" + irregula.IdAuditoria.DataLevantamento.Year);

			lblUFIRMinimo.Text = infracao.ValorMin.ToString("n");
			lblUFIRMaximo.Text = infracao.ValorMax.ToString("n");
			lblReaisMinimo.Text = ((float)(Convert.ToSingle(infracao.ValorMin)*valorUfir.Valor)).ToString("n");
			lblReaisMaximo.Text = ((float)(Convert.ToSingle(infracao.ValorMax)*valorUfir.Valor)).ToString("n");
		}

		protected void btnIrregularidade_Click(object sender, System.EventArgs e)
		{
			Guid strAux = Guid.NewGuid();

            Entities.Usuario xUsuario = (Entities.Usuario)Session["usuarioLogado"];

            OpenReport("Auditoria", "Auditoria.aspx?IliteraSystem=" + strAux.ToString() + strAux.ToString()
                    + "&IdUsuario=" + xUsuario.IdUsuario + "&IdEmpresa=" + Session["Empresa"].ToString() + "&IdIrregularidade=" + Request["IdIrregularidade"] + "&IdAuditoria=" + irregula.IdAuditoria.Id, "AuditoriaReport");


			//OpenReport("Auditoria", "Auditoria.aspx?IliteraSystem=" + strAux.ToString() + strAux.ToString()
				//+ "&IdUsuario=" + usuario.Id + "&IdEmpresa=" + cliente.Id + "&IdIrregularidade=" + Request["IdIrregularidade"] + "&IdAuditoria=" + irregula.IdAuditoria.Id, "AuditoriaReport");
		}

		protected void btnNR_Click(object sender, System.EventArgs e)
		{
			
            StringBuilder st = new StringBuilder();

            st.Append("javascript:void(centerWin('Norma.aspx?CodigoNorma=" + irregula.IdNorma.Id + "',450,300,\'Norma\'))");

            ScriptManager scriptManager = ScriptManager.GetCurrent(Page);
            ScriptManager.RegisterClientScriptBlock(
           this,
           this.GetType(),
           "Norma",
           st.ToString(),
           true);

            this.ClientScript.RegisterStartupScript(this.GetType(), "Norma", st.ToString(), true);

		}

		private void PopulaIrregularidade()
		{

            System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");
            
            irregula.DataInicioRegul = System.Convert.ToDateTime( wdtInicioReg.Text, ptBr );
            irregula.DataPrevisaoRegul = System.Convert.ToDateTime(wdtPrevisaoReg.Text, ptBr);
            irregula.DataFinalRegul = System.Convert.ToDateTime(wdtRegularizacao.Text, ptBr);
			
			irregula.IdTipoIrregularidade.Id = Convert.ToInt32(ddlTipo.SelectedItem.Value);

            //irregula.Orcamento = System.Convert.ToDouble( txtOrcamento.Text );
            //irregula.CustoFinal = System.Convert.ToDouble( txtCustoFinal.Text );
			
			irregula.IdRespAdm.Id = Convert.ToInt32(ddlRespAdm.SelectedItem.Value);
			irregula.IdRespOpe.Id = Convert.ToInt32(ddlRespOper.SelectedItem.Value);

			irregula.ObjetivoRegul = txtObjetivo.Text.Trim();
			irregula.ObservacaoRegul = txtObservacao.Text.Trim();
		}

		private void btnSalvar_Click(object sender, System.EventArgs e)
		{
			try
			{
				PopulaIrregularidade();
				
				ViewState["ddlValorBanco"] = ddlTipo.SelectedItem.Value;
				ViewState["ddlRespAdmValorBanco"] = ddlRespAdm.SelectedItem.Value;
				ViewState["ddlRespOperValorBanco"] = ddlRespOper.SelectedItem.Value;
				
				irregula.Save();

				
                MsgBox1.Show("Ilitera.Net", "Os Detalhes da Operação foram gravados com sucesso!", null,
                new EO.Web.MsgBoxButton("OK"));

			}
			catch (Exception ex)
			{
                MsgBox1.Show("Ilitera.Net", ex.Message, null,
                new EO.Web.MsgBoxButton("OK"));

			}
		}

		private void ddlTipo_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (ddlTipo.SelectedItem.Value == "Novo")
			{
				ddlTipo.ClearSelection();
				
				if (ViewState["ddlValorBanco"] != null && ViewState["ddlValorBanco"].ToString() != "0")
					ddlTipo.Items.FindByValue(ViewState["ddlValorBanco"].ToString()).Selected = true;    

				StringBuilder st = new StringBuilder();


                Entities.Usuario xUsuario = (Entities.Usuario)Session["usuarioLogado"];

                st.Append("void(addItemPop(centerWin('CadTipoIrregu.aspx?IdUsuario=" + xUsuario.IdUsuario + "&IdEmpresa=" + Session["Empresa"].ToString() + "', 350, 220,\'TipoIrregula\')));");

				this.ClientScript.RegisterStartupScript(this.GetType(), "TipoIrregularidade", st.ToString(), true);
			}
		}

		private void ddlRespAdm_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (ddlRespAdm.SelectedItem.Value == "Novo")
			{
				ddlRespAdm.ClearSelection();
				
				if (ViewState["ddlRespAdmValorBanco"] != null && ViewState["ddlRespAdmValorBanco"].ToString() != "0")
					ddlRespAdm.Items.FindByValue(ViewState["ddlRespAdmValorBanco"].ToString()).Selected = true;    

				StringBuilder st = new StringBuilder();

                Entities.Usuario xUsuario = (Entities.Usuario)Session["usuarioLogado"];

                st.Append("addItemPop(centerWin('CadResponsavel.aspx?IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + xUsuario.IdUsuario + "',350,300,\'CadastroResponsavelAdm\'));");

				this.ClientScript.RegisterStartupScript(this.GetType(), "CadastroResponsavelAdm", st.ToString(), true);
			}
		}

		private void ddlRespOper_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (ddlRespOper.SelectedItem.Value == "Novo")
			{
				ddlRespOper.ClearSelection();
				
				if (ViewState["ddlRespOperValorBanco"] != null && ViewState["ddlRespOperValorBanco"].ToString() != "0")
					ddlRespOper.Items.FindByValue(ViewState["ddlRespOperValorBanco"].ToString()).Selected = true;    

				StringBuilder st = new StringBuilder();

                Entities.Usuario xUsuario = (Entities.Usuario)Session["usuarioLogado"];

                st.Append("addItemPop(centerWin('CadResponsavel.aspx?IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + xUsuario.IdUsuario + "',350,300,\'CadastroResponsavelOpe\'));");

				this.ClientScript.RegisterStartupScript(this.GetType(), "CadastroResponsavelOpe", st.ToString(), true);
			}
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

            ScriptManager.RegisterStartupScript(this, this.GetType(), String.Format("OpenReport{0}", ReportName), st.ToString(), true);
        }

        protected void cmd_Imagem_Click(object sender, EventArgs e)
        {

            int indexAl = 0;
            ViewState["IndexFoto"] = 0;

            ArrayList alIrregularidadeFotoLocal = new IrregularidadeFotoLocal().Find("IdIrregularidade=" + irregula.Id
                + " ORDER BY (SELECT NomeLocal FROM LocalIrregularidade WHERE LocalIrregularidade.IdLocalIrregularidade=IrregularidadeFotoLocal.IdLocalIrregularidade)");

            foreach (IrregularidadeFotoLocal irregularidadeFotoLocal in alIrregularidadeFotoLocal)
            {
                if (irregularidadeFotoLocal.IsFotoPadrao)
                {
                    ViewState["IndexFoto"] = indexAl;
                    break;
                }

                indexAl++;
            }

         

            string xArquivo = Fotos.PathFoto(irregula.IdAuditoria, Convert.ToInt32(((IrregularidadeFotoLocal)alIrregularidadeFotoLocal[Convert.ToInt32(ViewState["IndexFoto"])]).NumeroFoto));

            if (xArquivo.ToUpper().IndexOf("FOTOSDOCSDIGITAIS") < 1)
            {
                xArquivo = xArquivo.Substring(0, xArquivo.IndexOf("\\") + 1) + "FOTOSDOCSDIGITAIS\\" + xArquivo.Substring(xArquivo.IndexOf("\\") + 1);
            }

            xArquivo = Ilitera.Common.Fotos.PathFoto_Uri(xArquivo);

            xArquivo = Ilitera.Common.Fotos.PathFoto_Uri(xArquivo);

            System.Net.WebClient client = new System.Net.WebClient();
            Byte[] buffer = client.DownloadData(xArquivo);
            Response.ContentType = "image/jpeg";
            Response.AddHeader("Content-Disposition", "attachment; filename=" + xArquivo);
            Response.AddHeader("content-length", buffer.Length.ToString());
            Response.BinaryWrite(buffer);
            Response.End();

        }
    }
}