using System;
using System.Collections;
using System.Globalization;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text;
using Ilitera.Opsa.Data;
using Ilitera.Common;

using System.Linq;
using System.Net.Mail;

using System.Drawing;
using System.Data;
using System.Data.SqlClient;
using Ilitera.Data;


namespace Ilitera.Net
{
	

    public partial class CadAcidente : System.Web.UI.Page
	{
		private Acidente acidente;
        private Cliente cliente;
        private Empregado empregado;

		protected System.Web.UI.WebControls.Label lblNumero2;
		protected string tipo, ProcessoRealizado;
		private System.Web.UI.WebControls.TextBox txtdda;
		private System.Web.UI.WebControls.TextBox txtmma;
		private System.Web.UI.WebControls.TextBox txtaaa;
		private System.Web.UI.WebControls.DropDownList ddlHora;
		private System.Web.UI.WebControls.DropDownList ddlMinuto;
		private System.Web.UI.WebControls.RadioButtonList rblTipoAcidente;
		private System.Web.UI.WebControls.TextBox txtMembroAtingido;
		private System.Web.UI.WebControls.TextBox txtAgenteCausador;
		private System.Web.UI.WebControls.TextBox txtNaturezaLesao;
		private System.Web.UI.WebControls.TextBox txtDescricao;
		private System.Web.UI.WebControls.DropDownList ddlLocalAcidente;
		private System.Web.UI.WebControls.Button btnAddLocal;
		private System.Web.UI.WebControls.TextBox txtEspecLocal;
		private System.Web.UI.WebControls.RadioButtonList rblAfastamento;
		private System.Web.UI.WebControls.Label lblSetor;
		private System.Web.UI.WebControls.RadioButtonList rblSetor;
		private System.Web.UI.WebControls.DropDownList ddlSetor;
		private System.Web.UI.WebControls.TextBox txtCID;
		private System.Web.UI.WebControls.Button btnProcurar;
		private System.Web.UI.WebControls.DropDownList ddlCID;
		private System.Web.UI.WebControls.RadioButtonList rblTransfSetor;
		private System.Web.UI.WebControls.RadioButtonList rblAposInval;
		//private Infragistics.WebUI.UltraWebTab.UltraWebTab uwtCAT;
		private System.Web.UI.WebControls.RadioButtonList rblCAT;
		private System.Web.UI.WebControls.RadioButtonList rblEmitente;
		private System.Web.UI.WebControls.RadioButtonList rblTipoCAT;
		private System.Web.UI.WebControls.RadioButtonList rblRegPol;
		private System.Web.UI.WebControls.Label lblBO;
		private System.Web.UI.WebControls.TextBox txtBO;
		private System.Web.UI.WebControls.RadioButtonList rblMorte;
		private System.Web.UI.WebControls.Label lblObito;
		private System.Web.UI.WebControls.TextBox txtddo;
		private System.Web.UI.WebControls.TextBox txtmmo;
		private System.Web.UI.WebControls.TextBox txtaao;
		private System.Web.UI.WebControls.Label lblBarra1;
		private System.Web.UI.WebControls.Label lblBarra2;
		private System.Web.UI.WebControls.ListBox lsbEmpregados;
		//private System.Web.UI.WebControls.ListBox lsbTestemunhas;
		//private System.Web.UI.WebControls.Button btnAdiciona;
		//private System.Web.UI.WebControls.Button btnRemove;
		//private System.Web.UI.WebControls.Button btnNovaTest;
		private System.Web.UI.WebControls.DropDownList ddlAbsentismo;
		private System.Web.UI.WebControls.TextBox txtddi;
		private System.Web.UI.WebControls.TextBox txtmmi;
		private System.Web.UI.WebControls.TextBox txtaai;
		private System.Web.UI.WebControls.DropDownList ddlHoraIni;
		private System.Web.UI.WebControls.DropDownList ddlMinutoIni;
		private System.Web.UI.WebControls.TextBox txtddp;
		private System.Web.UI.WebControls.TextBox txtmmp;
		private System.Web.UI.WebControls.TextBox txtaap;
		private System.Web.UI.WebControls.TextBox txtddr;
		private System.Web.UI.WebControls.TextBox txtmmr;
		private System.Web.UI.WebControls.TextBox txtaar;
		private System.Web.UI.WebControls.DropDownList ddlHoraRet;
		private System.Web.UI.WebControls.DropDownList ddlMinutoRet;
		private System.Web.UI.WebControls.TextBox txtDiaCAT;
		private System.Web.UI.WebControls.TextBox txtMesCAT;
		private System.Web.UI.WebControls.TextBox txtAnoCAT;
		private System.Web.UI.WebControls.DropDownList ddlHoraCAT;
		private System.Web.UI.WebControls.DropDownList ddlMinutoCAT;
		private System.Web.UI.WebControls.TextBox txtNumeroCAT;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        protected void Page_Load(object sender, System.EventArgs e)
		{
            InicializaWebPageObjects();
            string xUsuario = Session["usuarioLogado"].ToString();

            if (!Page.IsPostBack)
			{



				for (int i=0; i<24; i++)
				{
					ddlHora.Items.Insert(i, new ListItem(i.ToString("00"), i.ToString("00")));
                    ddlHoraAntes.Items.Insert(i, new ListItem(i.ToString("00"), i.ToString("00")));
                    ddlHoraCAT.Items.Insert(i, new ListItem(i.ToString("00"), i.ToString("00")));
					ddlHoraIni.Items.Insert(i, new ListItem(i.ToString("00"), i.ToString("00")));
					ddlHoraRet.Items.Insert(i, new ListItem(i.ToString("00"), i.ToString("00")));
                    ddlHoraAt.Items.Insert(i, new ListItem(i.ToString("00"), i.ToString("00")));
                }
				for (int i=0; i<60; i++)
				{
					ddlMinuto.Items.Insert(i, new ListItem(i.ToString("00"), i.ToString("00")));
                    ddlMinutoAntes.Items.Insert(i, new ListItem(i.ToString("00"), i.ToString("00")));
                    ddlMinutoCAT.Items.Insert(i, new ListItem(i.ToString("00"), i.ToString("00")));
					ddlMinutoIni.Items.Insert(i, new ListItem(i.ToString("00"), i.ToString("00")));
					ddlMinutoRet.Items.Insert(i, new ListItem(i.ToString("00"), i.ToString("00")));
                    ddlMinutoAt.Items.Insert(i, new ListItem(i.ToString("00"), i.ToString("00")));
                }

				PopulaDDLLocalAcidente();
				//PopulaListBoxEmpregados();
				//PopulaListBoxTestemunhas();
				PopulaDDLAbsentismos();

                PopulaDDLSituacaoGeradora();
                PopulaDDLParteCorpo();
                PopulaDDLAgenteCausador();
                PopulaDDLDescricaoLesao();
                PopulaDDLTipoAcidente();

                
                PopulaDDLUF();

                if (acidente.Id != 0)
					PopulaTela();



                Cliente cliente;
                cliente = new Cliente(System.Convert.ToInt32(Session["Empresa"].ToString()));

                cliente.IdGrupoEmpresa.Find();

                if (cliente.IdGrupoEmpresa.Descricao.ToUpper().Trim() == "CAPGEMINI" && Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("PRAJNA") > 0)  //.Id == -905238295)  // Capgemini
                {
                    chk_INSS.Visible = true;
                }


            }
			else
			{
				string DDLlocalValue = ddlLocalAcidente.SelectedItem.Value;
				PopulaDDLLocalAcidente();
				foreach (ListItem item in ddlLocalAcidente.Items)
					if (item.Value == DDLlocalValue)
						item.Selected = true;
				//PopulaMantemListBoxTestemunhas();
			}
			RegisterClientCode();
		}
		
		#region Inicialização de componentes do WebTab
		
		protected void InicializaComponentes()
		{
            txtdda = (TextBox)FindControl("tabtxtdda");
            txtmma = (TextBox)FindControl("tabtxtmma");
            txtaaa = (TextBox)FindControl("tabtxtaaa");
            ddlHora = (DropDownList)FindControl("tabddlHora");
            ddlMinuto = (DropDownList)FindControl("tabddlMinuto");
            rblTipoAcidente = (RadioButtonList)FindControl("tabrblTipoAcidente");
            txtMembroAtingido = (TextBox)FindControl("tabtxtMembroAtingido");
            txtAgenteCausador = (TextBox)FindControl("tabtxtAgenteCausador");
            txtNaturezaLesao = (TextBox)FindControl("tabtxtNaturezaLesao");
            txtDescricao = (TextBox)FindControl("tabtxtDescricao");
            ddlLocalAcidente = (DropDownList)FindControl("tabddlLocalAcidente");
            btnAddLocal = (Button)FindControl("tabbtnAddLocal");
            txtEspecLocal = (TextBox)FindControl("tabtxtEspecLocal");
            rblAfastamento = (RadioButtonList)FindControl("tabrblAfastamento");
            lblSetor = (Label)FindControl("tablblSetor");
            rblSetor = (RadioButtonList)FindControl("tabrblSetor");
            ddlSetor = (DropDownList)FindControl("tabddlSetor");
            txtCID = (TextBox)FindControl("tabtxtCID");
            btnProcurar = (Button)FindControl("tabbtnProcurar");
            ddlCID = (DropDownList)FindControl("tabddlCID");
            rblTransfSetor = (RadioButtonList)FindControl("tabrblTransfSetor");
            rblAposInval = (RadioButtonList)FindControl("tabrblAposInval");
            //uwtCAT = (Infragistics.WebUI.UltraWebTab.UltraWebTab)FindControl("tabuwtCAT");
            rblCAT = (RadioButtonList)FindControl("tabrblCAT");
            rblEmitente = (RadioButtonList)FindControl("tabrblEmitente");
            rblTipoCAT = (RadioButtonList)FindControl("tabrblTipoCAT");
            rblRegPol = (RadioButtonList)FindControl("tabrblRegPol");
            lblBO = (Label)FindControl("tablblBO");
            txtBO = (TextBox)FindControl("tabtxtBO");
            rblMorte = (RadioButtonList)FindControl("tabrblMorte");
            lblObito = (Label)FindControl("tablblObito");
            txtddo = (TextBox)FindControl("tabtxtddo");
            txtmmo = (TextBox)FindControl("tabtxtmmo");
            txtaao = (TextBox)FindControl("tabtxtaao");
            lblBarra1 = (Label)FindControl("tablblBarra1");
            lblBarra2 = (Label)FindControl("tablblBarra2");
            lsbEmpregados = (ListBox)FindControl("tablsbEmpregados");
            //lsbTestemunhas = (ListBox)FindControl("tablsbTestemunhas");
            //btnAdiciona = (Button)FindControl("tabbtnAdiciona");
            //btnRemove = (Button)FindControl("tabbtnRemove");
            //btnNovaTest = (Button)FindControl("tabbtnNovaTest");
            ddlAbsentismo = (DropDownList)FindControl("tabddlAbsentismo");
            txtddi = (TextBox)FindControl("tabtxtddi");
            txtmmi = (TextBox)FindControl("tabtxtmmi");
            txtaai = (TextBox)FindControl("tabtxtaai");
            ddlHoraIni = (DropDownList)FindControl("tabddlHoraIni");
            ddlMinutoIni = (DropDownList)FindControl("tabddlMinutoIni");
            txtddp = (TextBox)FindControl("tabtxtddp");
            txtmmp = (TextBox)FindControl("tabtxtmmp");
            txtaap = (TextBox)FindControl("tabtxtaap");
            txtddr = (TextBox)FindControl("tabtxtddr");
            txtmmr = (TextBox)FindControl("tabtxtmmr");
            txtaar = (TextBox)FindControl("tabtxtaar");
            ddlHoraRet = (DropDownList)FindControl("tabddlHoraRet");
            ddlMinutoRet = (DropDownList)FindControl("tabddlMinutoRet");
            txtDiaCAT = (TextBox)FindControl("tabtxtDiaCAT");
            txtMesCAT = (TextBox)FindControl("tabtxtMesCAT");
            txtAnoCAT = (TextBox)FindControl("tabtxtAnoCAT");
            ddlHoraCAT = (DropDownList)FindControl("tabddlHoraCAT");
            ddlMinutoCAT = (DropDownList)FindControl("tabddlMinutoCAT");
            txtNumeroCAT = (TextBox)FindControl("tabtxtNumeroCAT");
		}

		protected void InicializaEventosDosComponentes()
		{
			btnAddLocal.Click += new System.EventHandler(btnAddLocal_Click);
			rblSetor.SelectedIndexChanged += new System.EventHandler(rblSetor_SelectedIndexChanged);
			rblTipoAcidente.SelectedIndexChanged += new System.EventHandler(rblTipoAcidente_SelectedIndexChanged);
			btnProcurar.Click += new System.EventHandler(btnProcurar_Click);
			rblAfastamento.SelectedIndexChanged += new System.EventHandler(rblAfastamento_SelectedIndexChanged);
			rblCAT.SelectedIndexChanged += new System.EventHandler(rblCAT_SelectedIndexChanged);
			rblRegPol.SelectedIndexChanged += new System.EventHandler(rblRegPol_SelectedIndexChanged);
			rblMorte.SelectedIndexChanged += new System.EventHandler(rblMorte_SelectedIndexChanged);
			ddlAbsentismo.SelectedIndexChanged += new System.EventHandler(ddlAbsentismo_SelectedIndexChanged);
			//btnAdiciona.Click += new System.EventHandler(btnAdiciona_Click);
			//btnRemove.Click += new System.EventHandler(btnRemove_Click);
			//btnNovaTest.Click += new System.EventHandler(btnNovaTest_Click);
		}
		#endregion

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			InicializaComponentes();
			InicializaEventosDosComponentes();
			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.ID = "CadAcidente";

		}
		#endregion

        //private void PopulaMantemListBoxTestemunhas()
        //{
        //    ArrayList selectedValues = new ArrayList();

        //    foreach (ListItem item in lsbTestemunhas.Items)
        //        if (item.Selected)
        //            selectedValues.Add(item.Value);

        //    PopulaListBoxTestemunhas();

        //    foreach (string lsbValue in selectedValues)
        //        lsbTestemunhas.Items.FindByValue(lsbValue).Selected = true;
        //}
		
		private void PopulaDDLAbsentismos()
		{
            DataSet dsAbsentismo = new Afastamento().Get("IdEmpregado=" + Session["Empregado"].ToString()
				+" AND (IdAcidente IS NULL OR IdAcidente=" + Request["IdAcidente"].ToString() + ")"
				+" AND IndTipoAfastamento=" + (int)TipoAfastamento.Ocupacional
				+" ORDER BY DataInicial DESC");

			DataSet ds = new DataSet();
			DataRow row;
			DataTable table = new DataTable("Default");
			table.Columns.Add("IdAfastamento", Type.GetType("System.String"));
			table.Columns.Add("Afastamento", Type.GetType("System.String"));
			ds.Tables.Add(table);

			foreach (DataRow rowAbsentismo in dsAbsentismo.Tables[0].Select())
			{
				row = ds.Tables[0].NewRow();
				row["IdAfastamento"] = rowAbsentismo["IdAfastamento"];
				row["Afastamento"] = Convert.ToDateTime(rowAbsentismo["DataInicial"]).ToString("dd-MM-yyyy") + " " + Convert.ToDateTime(rowAbsentismo["DataInicial"]).ToString("t");
				ds.Tables[0].Rows.Add(row);
			}

			ddlAbsentismo.DataSource = ds;
			ddlAbsentismo.DataValueField = "IdAfastamento";
			ddlAbsentismo.DataTextField = "Afastamento";
			ddlAbsentismo.DataBind();

            ddlAbsentismo.Items.Insert(0, new ListItem("Novo Absentismo...", "0"));
		}

        //private void PopulaListBoxTestemunhas()
        //{
        //    ArrayList alTestemunha = new Testemunha().Find("IdCAT=" + acidente.IdCAT.Id);

        //    DataSet ds = new DataSet();
        //    DataSet dsOrdered = new DataSet();
        //    DataRow row, rowOrdered;

        //    DataTable table = new DataTable("Default");
        //    table.Columns.Add("IdTestemunha", Type.GetType("System.String"));
        //    table.Columns.Add("NomeTestemunha", Type.GetType("System.String"));
        //    ds.Tables.Add(table);

        //    DataTable tableOrdered = new DataTable("Default");
        //    tableOrdered.Columns.Add("IdTestemunha", Type.GetType("System.String"));
        //    tableOrdered.Columns.Add("NomeTestemunha", Type.GetType("System.String"));
        //    dsOrdered.Tables.Add(tableOrdered);

        //    foreach (Testemunha testemunha in alTestemunha)
        //    {
        //        row = ds.Tables[0].NewRow();
        //        row["IdTestemunha"] = testemunha.Id.ToString();
        //        if (testemunha.IdEmpregado.Id != 0)
        //        {
        //            testemunha.IdEmpregado.Find();
        //            row["NomeTestemunha"] = testemunha.IdEmpregado.tNO_EMPG;
        //        }
        //        else
        //            row["NomeTestemunha"] = testemunha.NomeCompleto;
        //        ds.Tables[0].Rows.Add(row);
        //    }

        //    DataRow []rows = ds.Tables[0].Select("", "NomeTestemunha");

        //    foreach (DataRow rowOrder in rows)
        //    {
        //        rowOrdered = dsOrdered.Tables[0].NewRow();
        //        rowOrdered["IdTestemunha"] = rowOrder["IdTestemunha"];
        //        rowOrdered["NomeTestemunha"] = rowOrder["NomeTestemunha"];
        //        dsOrdered.Tables[0].Rows.Add(rowOrdered);
        //    }

        //    lsbTestemunhas.DataSource = dsOrdered;
        //    lsbTestemunhas.DataValueField = "IdTestemunha";
        //    lsbTestemunhas.DataTextField = "NomeTestemunha";
        //    lsbTestemunhas.DataBind();
        //}

        //private void PopulaListBoxEmpregados()
        //{
        //    DataSet ds = new Empregado().Get("nID_EMPR=" + cliente.Id + " AND hDT_DEM IS NULL"
        //        + " AND nID_EMPREGADO<>" + empregado.Id
        //        + " AND nID_EMPREGADO NOT IN (SELECT opsa.dbo.Testemunha.IdEmpregado FROM opsa.dbo.Testemunha WHERE opsa.dbo.Testemunha.IdCAT=" + acidente.IdCAT.Id
        //        + " AND opsa.dbo.Testemunha.IdEmpregado IS NOT NULL)"
        //        + " ORDER BY tNO_EMPG");

        //    lsbEmpregados.DataSource = ds;
        //    lsbEmpregados.DataValueField = "nID_EMPREGADO";
        //    lsbEmpregados.DataTextField = "tNO_EMPG";
        //    lsbEmpregados.DataBind();
        //}

        private void PopulaDDLUF()
        {
            DataSet dsESocial = new UnidadeFederativa().Get("NomeAbreviado is not null ORDER BY NomeAbreviado");
            //dsESocial.Tables[0].Columns.Add("CodDesc", typeof(string), "UF + ' - ' + NomeAbreviado");


            ddlUF.DataSource = dsESocial;
            ddlUF.DataTextField = "NomeAbreviado";
            ddlUF.DataValueField = "IdUnidadeFederativa";
            ddlUF.DataBind();

            ddlUF.Items.Insert(0, new ListItem("Selecione...", "0"));

        }


        private void PopulaDDLMunicipio( string xUF )
        {
            DataSet dsESocial = new Municipio().Get("NomeCompleto is not null and IdUnidadeFederativa = '" + xUF + "' ORDER BY NomeCompleto");
            //dsESocial.Tables[0].Columns.Add("CodDesc", typeof(string), "UF + ' - ' + NomeAbreviado");

            ddlMunicipio.Items.Clear();

            ddlMunicipio.DataSource = dsESocial;
            ddlMunicipio.DataTextField = "NomeCompleto";
            ddlMunicipio.DataValueField = "IdMunicipio";
            ddlMunicipio.DataBind();

            ddlMunicipio.Items.Insert(0, new ListItem("Selecione...", "0"));

        }

        private void PopulaDDLTipoAcidente()
        {
            DataSet dsESocial = new eSocial_24_Tipo_Acidente().Get("Situacao is not null ORDER BY Situacao");
            dsESocial.Tables[0].Columns.Add("CodDesc", typeof(string), "Codigo + ' - ' + Situacao");

            ddlTipoAcidente.DataSource = dsESocial;
            ddlTipoAcidente.DataTextField = "CodDesc";
            ddlTipoAcidente.DataValueField = "Codigo";
            ddlTipoAcidente.DataBind();

            ddlTipoAcidente.Items.Insert(0, new ListItem("Selecione...", "0"));


            if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("PRAJNA") > 0)  
            {
                Label211.Visible = false;
                ddlTipoAcidente.Visible = false;
            }


        }

        private void PopulaDDLDescricaoLesao()
        {
            DataSet dsESocial = new eSocial_17_Natureza_Lesao().Get("Descricao is not null ORDER BY Descricao");
            dsESocial.Tables[0].Columns.Add("CodDesc", typeof(string), "Codigo + ' - ' + Descricao");

            ddlDescricaoLesao.DataSource = dsESocial;
            ddlDescricaoLesao.DataTextField = "CodDesc";
            ddlDescricaoLesao.DataValueField = "Codigo";
            ddlDescricaoLesao.DataBind();

            ddlDescricaoLesao.Items.Insert(0, new ListItem("Selecione...", "0"));

        }


        private void PopulaDDLAgenteCausador()
        {
            if (acidente.Id != 0 && acidente.DataAcidente < new DateTime(2024, 1, 22))
            {

                DataSet dsESocial = new eSocial_14_Agente_Acidente_Trabalho().Get("Descricao is not null ORDER BY Descricao");

                DataSet dsESocial2 = new eSocial_15_Agente_Situacao_Doenca_Profissional().Get("Descricao is not null ORDER BY Descricao");

                dsESocial.Merge(dsESocial2);
                dsESocial.Tables[0].DefaultView.Sort = "Descricao";

                dsESocial.Tables[0].Columns.Add("CodDesc", typeof(string), "Codigo + ' - ' + Descricao");

                DataSet dsView = new DataSet();

                dsView.Tables.Add(dsESocial.Tables[0].DefaultView.ToTable());

                ddlAgenteCausador.DataSource = dsView;
                ddlAgenteCausador.DataTextField = "CodDesc";
                ddlAgenteCausador.DataValueField = "Codigo";
                ddlAgenteCausador.DataBind();

                ddlAgenteCausador.Items.Insert(0, new ListItem("Selecione...", "0"));
            }
            else
            {

                DataSet dsESocial = new eSocial_14_Agente_Acidente_Trabalho().Get("Descricao is not null ORDER BY Descricao");

                dsESocial.Tables[0].DefaultView.Sort = "Descricao";

                dsESocial.Tables[0].Columns.Add("CodDesc", typeof(string), "Codigo + ' - ' + Descricao");

                DataSet dsView = new DataSet();

                dsView.Tables.Add(dsESocial.Tables[0].DefaultView.ToTable());

                ddlAgenteCausador.DataSource = dsView;
                ddlAgenteCausador.DataTextField = "CodDesc";
                ddlAgenteCausador.DataValueField = "Codigo";
                ddlAgenteCausador.DataBind();

                ddlAgenteCausador.Items.Insert(0, new ListItem("Selecione...", "0"));
            }

        }


        private void PopulaDDLSituacaoGeradora()
        {
            DataSet dsESocial = new eSocial_16_Situacao_Geradora_Acidente_Trabalho().Get("Descricao is not null ORDER BY Descricao");

            DataSet dsESocial2 = new eSocial_15_Agente_Situacao_Doenca_Profissional().Get("Descricao is not null ORDER BY Descricao");

            dsESocial.Merge(dsESocial2);
            dsESocial.Tables[0].DefaultView.Sort = "Descricao";


            dsESocial.Tables[0].Columns.Add("CodDesc", typeof(string), "Codigo + ' - ' + Descricao");

            ddlSituacaoGeradora.DataSource = dsESocial;
            ddlSituacaoGeradora.DataTextField = "CodDesc";
            ddlSituacaoGeradora.DataValueField = "Codigo";
            ddlSituacaoGeradora.DataBind();

            ddlSituacaoGeradora.Items.Insert(0, new ListItem("Selecione...", "0"));

        }


        private void PopulaDDLParteCorpo()
        {
            DataSet dsESocial = new eSocial_13_Parte_Corpo_Atingida().Get("Descricao is not null ORDER BY Descricao");

            dsESocial.Tables[0].Columns.Add("CodDesc", typeof(string), "Codigo + ' - ' + Descricao");

            ddlParteCorpo.DataSource = dsESocial;
            ddlParteCorpo.DataTextField = "CodDesc";
            ddlParteCorpo.DataValueField = "Codigo";
            ddlParteCorpo.DataBind();

            ddlParteCorpo.Items.Insert(0, new ListItem("Selecione...", "0"));

        }


        private void PopulaDDLSetor()
		{
			DataSet dsSetor = new Setor().Get("nID_EMPR=" + cliente.Id + " ORDER BY tNO_STR_EMPR");

			ddlSetor.DataSource = dsSetor;
			ddlSetor.DataTextField = "tNO_STR_EMPR";
			ddlSetor.DataValueField = "nID_SETOR";
			ddlSetor.DataBind();
		}

		private void PopulaDDLLocalAcidente()
		{
			DataSet dsLocalAcidente = new DataSet();
			DataSet dsLocalOrdered = new DataSet();
			DataRow rowds;

			DataTable table = new DataTable("Default");
			table.Columns.Add("Id", Type.GetType("System.String"));
			table.Columns.Add("CNPJ", Type.GetType("System.String"));
			dsLocalAcidente.Tables.Add(table);

			DataTable tableOrdered = new DataTable("DefaultOrdered");
			tableOrdered.Columns.Add("Id", Type.GetType("System.String"));
			tableOrdered.Columns.Add("CNPJ", Type.GetType("System.String"));
			dsLocalOrdered.Tables.Add(tableOrdered);

			DataSet dsJuridicaGrupo = new Juridica().Get("IdGrupoEmpresa=" + cliente.IdGrupoEmpresa.Id
				+" AND NomeCodigo IS NOT NULL AND NomeCodigo<>''"
				+" AND IdJuridica<>" + cliente.Id + " and juridica.IdPessoa in ( select IdPessoa from Pessoa where isinativo = 0 ) ");

			DataSet dsLA = new LocalAcidente().Get("IdCliente=" + cliente.Id);

			foreach (DataRow row in dsJuridicaGrupo.Tables[0].Select())
			{
				rowds = dsLocalAcidente.Tables[0].NewRow();
			
				rowds["Id"] = row["IdJuridica"];
				rowds["CNPJ"] = row["NomeCodigo"] + " " + row["NomeAbreviado"];

				dsLocalAcidente.Tables[0].Rows.Add(rowds);
			}

			foreach (DataRow row in dsLA.Tables[0].Select())
			{
				rowds = dsLocalAcidente.Tables[0].NewRow();

				rowds["Id"] = row["IdLocalAcidente"];
				rowds["CNPJ"] = row["CNPJ"] + " " + row["Nome"];

				dsLocalAcidente.Tables[0].Rows.Add(rowds);
			}

			DataRow []rows = dsLocalAcidente.Tables[0].Select("", "CNPJ");

			foreach (DataRow rowOrdered in rows)
			{
				rowds = dsLocalOrdered.Tables[0].NewRow();

				rowds["Id"] = rowOrdered["Id"];
				rowds["CNPJ"] = rowOrdered["CNPJ"];

				dsLocalOrdered.Tables[0].Rows.Add(rowds);
			}

			ddlLocalAcidente.DataSource = dsLocalOrdered;
			ddlLocalAcidente.DataValueField = "Id";
			ddlLocalAcidente.DataTextField = "CNPJ";
			ddlLocalAcidente.DataBind();

            ddlLocalAcidente.Items.Insert(0, new ListItem("No Próprio Local de Trabalho", Session["Empresa"].ToString()));


            //deixar posicionado na empresa ou obra selecionada no sistema
            //if (ddlLocalAcidente.Items.Count > 0)
            //{
            //    if (ddlLocalAcidente.Items.FindByValue(cliente.Id.ToString().Trim()).Value == cliente.Id.ToString().Trim())
            //    {
            //        ddlLocalAcidente.SelectedValue = cliente.Id.ToString().Trim();
            //    }
            //}

        }

		private void RegisterClientCode()
		{
			//lsbTestemunhas.Attributes.Add("ondblclick", "void(addItemPop(centerWin('CadNovaTestemunha.aspx?IdTestemunha=' + this.value + '&IdUsuario=" + Request["IdUsuario"] + "&IdEmpresa=" + Request["IdEmpresa"] + "&IdEmpregado=" + Request["IdEmpregado"] + "',400,205,\'CadNovaTestemunha\')))");
			btnCancelar.Attributes.Add("onClick" ,"javascript:window.close();");
			
			if (rblAfastamento.SelectedValue == "0" && rblCAT.SelectedValue == "0")
			{
				btnOK.Attributes.Add("onClick", "javascript: return VerificaDataAcidente();");
				btnExcluir.Attributes.Add("onClick" ,"javascript:if(confirm('Deseja realmente excluir este Acidente?')){ return true;} else return false;");
			}
			else if (rblAfastamento.SelectedValue == "0" && rblCAT.SelectedValue == "1")
			{
				btnOK.Attributes.Add("onClick", "javascript: return VerificaDataAcidente() && VerificaDataCAT();");
				btnExcluir.Attributes.Add("onClick" ,"javascript:if(confirm('Deseja realmente excluir este Acidente com a CAT relacionada?')){ return true;} else return false;");
			}
			else if (rblAfastamento.SelectedValue == "1" && rblCAT.SelectedValue == "0")
			{
				btnOK.Attributes.Add("onClick", "javascript: return VerificaDataAcidente() && VerificaDataAbsentismo();");
				btnExcluir.Attributes.Add("onClick" ,"javascript:if(confirm('Deseja realmente excluir este Acidente com o Afastamento relacionado?')){ return true;} else return false;");
			}
			else if (rblAfastamento.SelectedValue == "1" && rblCAT.SelectedValue == "1")
			{
				btnOK.Attributes.Add("onClick", "javascript: return VerificaDataAcidente() && VerificaDataAbsentismo() && VerificaDataCAT();");
				btnExcluir.Attributes.Add("onClick" ,"javascript:if(confirm('Deseja realmente excluir este Acidente com o Afastamento e a CAT relacionados?')){ return true;} else return false;");
			}
		}

        protected void InicializaWebPageObjects()
        {
            //base.InicializaWebPageObjects();

            cliente = new Cliente(System.Convert.ToInt32(Session["Empresa"].ToString()));
            empregado = new Empregado(System.Convert.ToInt32(Session["Empregado"].ToString()));
            acidente = new Acidente(System.Convert.ToInt32(Request["IdAcidente"].ToString()));


			if (ViewState["IdAcidente"] != null)
			{
				acidente = new Acidente(Convert.ToInt32(ViewState["IdAcidente"]));
				tipo = "atualizado";
				ProcessoRealizado = "Edição do cadastro de Acidente para o Empregado " + empregado.tNO_EMPG;
			}
			else if(Request["IdAcidente"] != null && Request["IdAcidente"] != "")
			{
				acidente = new Acidente(Convert.ToInt32(Request["IdAcidente"]));
				tipo = "atualizado";
				ProcessoRealizado = "Edição do cadastro de Acidente para o Empregado " + empregado.tNO_EMPG;
			}
			else
			{
				acidente = new Acidente();
				acidente.Inicialize();
				acidente.IdEmpregado = empregado;
				btnExcluir.Visible = false;
				tipo = "cadastrado";
				ProcessoRealizado = "Cadastro de Acidente para o Empregado " + empregado.tNO_EMPG;
			}
		}

		private int PopulaAcidente() //SqlTransaction transaction)
		{
			int IdCAT = 0;

            acidente.DataAcidente = new DateTime(Convert.ToInt32(txtaaa.Text), Convert.ToInt32(txtmma.Text), Convert.ToInt32(txtdda.Text),
                    Convert.ToInt32(ddlHora.SelectedItem.Value), Convert.ToInt32(ddlMinuto.SelectedItem.Value), 0, 0);

            if (chk_UltDia.Checked == true)
            {
                acidente.UltDiaTrab = new DateTime(Convert.ToInt32(txtUltAno.Text), Convert.ToInt32(txtUltMes.Text), Convert.ToInt32(txtUltDia.Text));
            }
            else
            {
                acidente.UltDiaTrab = new DateTime(2000,1,1);
            }

            acidente.hrsTrabAntesAcid = ddlHoraAntes.SelectedItem.Value + ddlMinutoAntes.SelectedItem.Value;


            acidente.IndTipoAcidente = Convert.ToInt32(rblTipoAcidente.SelectedItem.Value);
			acidente.MembroAtingido = txtMembroAtingido.Text;
			acidente.AgenteCausador = txtAgenteCausador.Text;
			acidente.NaturezaLesao = txtNaturezaLesao.Text;
			acidente.Descricao = txtDescricao.Text;

			DataSet dsJuridica = new Juridica().Get("IdJuridica=" + ddlLocalAcidente.SelectedItem.Value);
			DataSet dsLocalAcidente = new LocalAcidente().Get("IdLocalAcidente=" + ddlLocalAcidente.SelectedItem.Value);

			if (dsJuridica.Tables[0].Rows.Count > 0)
				acidente.IdJuridica.Id = Convert.ToInt32(ddlLocalAcidente.SelectedItem.Value);
			else if (dsLocalAcidente.Tables[0].Rows.Count > 0)
				acidente.IdLocalAcidente.Id = Convert.ToInt32(ddlLocalAcidente.SelectedItem.Value);

			acidente.EspecLocal = txtEspecLocal.Text;
			acidente.indTipoSetor = Convert.ToInt32(rblSetor.SelectedValue);

			if (acidente.indTipoSetor == (int)TipoSetor.SetorNormal)
			{
				ArrayList emprFuncao = new EmpregadoFuncao().Find("nID_EMPREGADO=" + empregado.Id
					+" AND hDT_INICIO<='" + acidente.DataAcidente.ToString("yyyy-MM-dd")
					+"' AND (hDT_TERMINO IS NULL OR hDT_TERMINO>='" + acidente.DataAcidente.ToString("yyyy-MM-dd") + "')");

                //if (emprFuncao.Count != 1)
                //{
                //    //throw new Exception("Não foi possível adicionar este Acidente! Verifique se a Classificação Funcional do empregado está preenchida corretamente!");
                //    MsgBox1.Show("Acidente", "Não foi possível adicionar este Acidente! Verifique se a Classificação Funcional do empregado está preenchida corretamente!", null,
                //                    new EO.Web.MsgBoxButton("OK"));
                //    return 0;

                //}

//                if (((EmpregadoFuncao)emprFuncao[0]).nID_SETOR.Id == 0)
//                {
////                    throw new Exception("Não foi possível adicionar este Acidente! Não há Setor cadastrado para este empregado no período da Data de Acidente informada!");
//                    MsgBox1.Show("Acidente", "Não foi possível adicionar este Acidente! Não há Setor cadastrado para este empregado no período da Data de Acidente informada!", null,
//                                    new EO.Web.MsgBoxButton("OK"));
//                    return 0;

//                }

				acidente.IdSetor = ((EmpregadoFuncao)emprFuncao[0]).nID_SETOR;
			}
			else if (acidente.indTipoSetor == (int)TipoSetor.OutroSetor)
				acidente.IdSetor.Id = Convert.ToInt32(ddlSetor.SelectedValue);

			if (acidente.IndTipoAcidente == (int)TipoAcidente.Doenca)
                //if (ddlCID.SelectedValue == "0")
                if (txtCID.Text.Trim() == "" || lbl_Id1.Text.Trim() == "")             
                {
                    //throw new Exception("É necessário informar o CID para este Tipo de Acidente!");
                    MsgBox1.Show("Acidente", "É necessário informar o CID para este Tipo de Acidente!", null,
                                    new EO.Web.MsgBoxButton("OK"));

                }
			
            //acidente.IdCID.Id = Convert.ToInt32(ddlCID.SelectedValue);

            if (txtCID.Text.Trim() != "" && lbl_Id1.Text.Trim() != "")
            {
                acidente.IdCID.Id = System.Convert.ToInt32(lbl_Id1.Text);                 //Convert.ToInt32(ddlCID.SelectedValue);
                if (txtCID2.Text.Trim() != "" && lbl_Id2.Text.Trim() != "")
                    acidente.IdCID2 = System.Convert.ToInt32(lbl_Id2.Text);
                if (txtCID3.Text.Trim() != "" && lbl_Id2.Text.Trim() != "")
                    acidente.IdCID3 = System.Convert.ToInt32(lbl_Id3.Text);
                if (txtCID4.Text.Trim() != "" && lbl_Id4.Text.Trim() != "")
                    acidente.IdCID4 = System.Convert.ToInt32(lbl_Id4.Text);
            }



            acidente.IdLateralidade = System.Convert.ToInt16(rd_Lateralidade.SelectedValue);

            if (rd_Internacao.SelectedValue.ToString().Trim() == "0")
                acidente.HasInternacao = false;
            else
                acidente.HasInternacao = true;

            acidente.CNES = txtCNES.Text.Trim();

            if (txtaaAt.Text.Trim() != "")
            {
                acidente.DataInternacao = new DateTime(Convert.ToInt32(txtaaAt.Text), Convert.ToInt32(txtmmAt.Text), Convert.ToInt32(txtddAt.Text),
                    Convert.ToInt32(ddlHoraAt.SelectedItem.Value), Convert.ToInt32(ddlMinutoAt.SelectedItem.Value), 0, 0);
            }

            if (txtDuracao.Text.Trim() == "")
                acidente.DuracaoInternacao = 0;
            else
                acidente.DuracaoInternacao = System.Convert.ToInt16(txtDuracao.Text);

            acidente.MedicoInternacao = txtMedico.Text.Trim();
            acidente.CRMInternacao = txtCRM.Text.Trim();
            acidente.UFInternacao = txtCRMUF.Text.Trim();
            acidente.DiagnosticoProvavel = txtDiagnostico.Text.Trim();




            acidente.IdIniciativaCat = System.Convert.ToInt16(rblIniciativa.SelectedValue.ToString());

            acidente.Codigo_Parte_Corpo_Atingida = System.Convert.ToInt32( ddlParteCorpo.SelectedValue.ToString() );

            acidente.Codigo_Descricao_Lesao = System.Convert.ToInt32(ddlDescricaoLesao.SelectedValue);

            acidente.Codigo_Agente_Causador = System.Convert.ToInt32(ddlAgenteCausador.SelectedValue);

            acidente.Codigo_Situacao_Geradora = System.Convert.ToInt32(ddlSituacaoGeradora.SelectedValue);

            acidente.Codigo_Acidente_Trabalho = ddlTipoAcidente.SelectedValue;

            acidente.IdTipoLocal = System.Convert.ToInt16( rd_TipoLocal.SelectedValue);

            acidente.Logradouro = txtEndereco.Text.Trim();
            acidente.Nr_Logradouro = txtNumero.Text.Trim();
            //acidente.Municipio = txtMunicipio.Text.Trim();
            //acidente.UF = txtUF.Text.Trim().ToUpper();

            acidente.Municipio = "";
            acidente.UF = "";

            if (ddlUF.SelectedIndex > 0)
            {
                acidente.UF = ddlUF.SelectedItem.ToString();
                if ( ddlMunicipio.SelectedIndex>=0)
                   acidente.Municipio = ddlMunicipio.SelectedValue;
            }

            acidente.Bairro = txtBairro.Text.Trim();
            acidente.Complemento = txtComplemento.Text.Trim();
            acidente.CEP = txtCEP.Text.Trim();


            acidente.dscLocal = txtDescLocal.Text;
            acidente.codPostal = txtCodPostal.Text;

            acidente.CNPJ_Terceiro = txtCNPJTerceiros.Text;
            

            acidente.isTransfSetor = Convert.ToBoolean(Convert.ToInt32(rblTransfSetor.SelectedValue));
			acidente.isAposInval = Convert.ToBoolean(Convert.ToInt32(rblAposInval.SelectedValue));

            if (wcePerdaMaterial.Text.Trim() == "") wcePerdaMaterial.Text = "0";
            acidente.PerdaMaterial = Convert.ToSingle( wcePerdaMaterial.Text );
            acidente.Observacoes = txtObservacoes.Text.Trim();

            if (rblCAT.SelectedValue == "0")
            {
                IdCAT = acidente.IdCAT.Id;
                acidente.IdCAT.Id = 0;
            }
            else if (rblCAT.SelectedValue == "1")
                PopulaCAT(); //transaction);

			if (rblAfastamento.SelectedValue == "0")
				acidente.hasAfastamento = false;
			else if (rblAfastamento.SelectedValue == "1")
				acidente.hasAfastamento = true;




            acidente.IdEmpregado = empregado;
            acidente.UsuarioId = Convert.ToInt32(Session["Empregado"].ToString());
			acidente.UsuarioProcessoRealizado = ProcessoRealizado;
            //acidente.Transaction = transaction;

            if (acidente.Id == 0) acidente.Reabertura = false;

            acidente.Save();

			if (IdCAT != 0)
			{
				CAT cat = new CAT(IdCAT);
                cat.UsuarioId = Convert.ToInt32(Session["Empregado"].ToString());
				//cat.Transaction = transaction;
				cat.Delete();
			}

            if (acidente.hasAfastamento)
            {
                if (PopulaAbsentismo() == 1)
                {
                    //MsgBox1.Show("Absenteísmo", "Absenteísmo salvo !  Existem absenteísmos anteriores com pelo menos 15 dias corridos ou com total de dias afastados de pelo 15 dias nos últimos 2 meses. ", null,
                    //new EO.Web.MsgBoxButton("OK"));
                    return 1;
                }
            }
            else
            {
                ArrayList alAbsentismo = new Afastamento().Find("IdAcidente=" + acidente.Id);

                foreach (Afastamento absentismo in alAbsentismo)
                {
                    absentismo.UsuarioId = Convert.ToInt32(Session["Empregado"].ToString());
                    //absentismo.Transaction = transaction;
                    absentismo.Delete();
                }
            }

            return 0;
		}

		private int PopulaAbsentismo() //SqlTransaction transaction)
		{
            bool xEnvio_Alerta = false;

            Afastamento absentismo = new Afastamento();
			
			if (ddlAbsentismo.SelectedValue == "0")
			{
				ArrayList alAbsentAtual = new Afastamento().Find("IdAcidente=" + acidente.Id);

				if (alAbsentAtual.Count == 1)
				{
					Afastamento absentAtual = (Afastamento)alAbsentAtual[0];
					absentAtual.IdAcidente.Id = 0;
                    absentAtual.UsuarioId = Convert.ToInt32(Session["Empregado"].ToString());
					//absentAtual.Transaction = transaction;
					absentAtual.Save();                    
				}
				
				absentismo.Inicialize();
                absentismo.IdEmpregado = empregado;




            }
            else
				absentismo.Find(Convert.ToInt32(ddlAbsentismo.SelectedValue));


            
            absentismo.DataInicial = new DateTime(Convert.ToInt32(txtaai.Text), Convert.ToInt32(txtmmi.Text), Convert.ToInt32(txtddi.Text), 
				Convert.ToInt32(ddlHoraIni.SelectedValue), Convert.ToInt32(ddlMinutoIni.SelectedValue), 0, 0);
			if (txtmmp.Text != string.Empty)
				absentismo.DataPrevista = new DateTime(Convert.ToInt32(txtaap.Text), Convert.ToInt32(txtmmp.Text), Convert.ToInt32(txtddp.Text));
			else
				absentismo.DataPrevista = new DateTime();
			if (txtmmr.Text != string.Empty)
				absentismo.DataVolta = new DateTime(Convert.ToInt32(txtaar.Text), Convert.ToInt32(txtmmr.Text), Convert.ToInt32(txtddr.Text), 
					Convert.ToInt32(ddlHoraRet.SelectedValue), Convert.ToInt32(ddlMinutoRet.SelectedValue), 0, 0);
			else
				absentismo.DataVolta = new DateTime();

			absentismo.IndTipoAfastamento = (int)TipoAfastamento.Ocupacional;
			absentismo.IdAcidente = acidente;
			//absentismo.IdCID.Id = Convert.ToInt32(ddlCID.SelectedValue);
            if (txtCID.Text.Trim() != "" && lbl_Id1.Text.Trim() != "")
            {
                absentismo.IdCID.Id = System.Convert.ToInt32(lbl_Id1.Text);                 //Convert.ToInt32(ddlCID.SelectedValue);
                if (txtCID2.Text.Trim() != "" && lbl_Id2.Text.Trim() != "")
                    absentismo.IdCID2 = System.Convert.ToInt32(lbl_Id2.Text);
                if (txtCID3.Text.Trim() != "" && lbl_Id2.Text.Trim() != "")
                    absentismo.IdCID3 = System.Convert.ToInt32(lbl_Id3.Text);
                if (txtCID4.Text.Trim() != "" && lbl_Id4.Text.Trim() != "")
                    absentismo.IdCID4 = System.Convert.ToInt32(lbl_Id4.Text);
            }



            if (chk_INSS.Visible == true)
            {
                if (chk_INSS.Checked == true) absentismo.INSS = true;
                else absentismo.INSS = false;
            }
            else
            {
                absentismo.INSS = false;
            }

            absentismo.UsuarioId = Convert.ToInt32(Session["Empregado"].ToString());
			//absentismo.Transaction = transaction;
			absentismo.Save();





            if (absentismo.DataVolta == null || absentismo.DataVolta.Year == 1)
            {
                if (absentismo.DataInicial.AddDays(15) <= absentismo.DataPrevista)
                {
                    xEnvio_Alerta = true;
                }
            }
            else
            {
                if (absentismo.DataInicial.AddDays(15) <= absentismo.DataVolta)
                {
                    xEnvio_Alerta = true;
                }
            }


            if (xEnvio_Alerta == false)
            //checar se há atetados com mais de 15 dias afastados nos ultimos 60 dias
            {
                System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");
                
                //checar se há atetados com mais de 15 dias afastados nos ultimos 60 dias ou afastamento com mais de 15 dias
                Ilitera.Data.Clientes_Funcionarios xAbs = new Ilitera.Data.Clientes_Funcionarios();
                DataSet rDs = xAbs.Checar_Absenteismo_Colaborador(Convert.ToInt32(Session["Empregado"].ToString()), absentismo.DataInicial.ToString("dd/MM/yyyy", ptBr));


                if (rDs.Tables[0].Rows.Count > 0)
                {
                    if (rDs.Tables[0].Rows[0][0].ToString().Trim() != "")
                    {
                        if (System.Convert.ToInt16(rDs.Tables[0].Rows[0][0].ToString()) >= 15)
                        {
                            xEnvio_Alerta = true;
                        }
                    }
                }

            }



            if (xEnvio_Alerta == true)
            {

                Cliente xCliente = new Cliente();
                xCliente.Find(System.Convert.ToInt32(Request["IdEmpresa"].ToString()));

                if (xCliente.Mail_Alerta_Absenteismo != null && xCliente.Mail_Alerta_Absenteismo.ToString().Trim() != "")
                {
                    string xBody = "";

                    if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().IndexOf("Prajna") > 0)
                    {
                        xBody = "<body><p><font size='2' face='Tahoma'><H1 align='center'>Alerta de Absenteísmo</H1></font></p> <br></br>" +
                                  "<p><font size='3' face='Tahoma'>O colaborador " + Session["NomeEmpregado"] + " possui afastamentos anteriores com pelo menos 15 dias corridos ou com total de dias afastados de pelo menos 15 dias nos últimos 60 dias. Favor verificar se os atestados possuem a mesma patologia para possível encaminhamento ao INSS. <br><br> Dúvidas contatar a central de atendimento Essence: centraldeatendimento@essencenet.com.br<br><br><br>Central de Atendimento<br>5A Essence<br>www.5aessence.com.br<br>Tel.: (11) 2344 - 4585</font></p></body>";

                        Envio_Email_Prajna(xCliente.Mail_Alerta_Absenteismo.Trim(), "", "Alerta de Absenteísmo", xBody, "", "Absenteísmo", Convert.ToInt32(Session["Empregado"].ToString()), System.Convert.ToInt32(Request["IdEmpresa"].ToString()));
                    }
                    else
                    {
                        xBody = "<body><p><font size='2' face='Tahoma'><H1 align='center'>Alerta de Absenteísmo</H1></font></p> <br></br>" +
                                         "<p><font size='3' face='Tahoma'>O colaborador " + Session["NomeEmpregado"] + " possui afastamentos anteriores com pelo menos 15 dias corridos ou com total de dias afastados de pelo menos 15 dias nos últimos 60 dias. Favor verificar se os atestados possuem a mesma patologia para possível encaminhamento ao INSS.<br><br> Dúvidas contatar a central de atendimento : atendimento@ilitera.com.br</font></p></body>";

                        Envio_Email_Ilitera(xCliente.Mail_Alerta_Absenteismo.Trim(), "", "Alerta de Absenteísmo", xBody, "", "Absenteísmo", Convert.ToInt32(Session["Empregado"].ToString()), System.Convert.ToInt32(Request["IdEmpresa"].ToString()));
                    }
                }

                //MsgBox1.Show("Absenteísmo", "Absenteísmo salvo !  Existem absenteísmos anteriores com pelo menos 15 dias corridos ou com total de dias afastados de pelo 15 dias nos últimos 2 meses. ", null,
                // new EO.Web.MsgBoxButton("OK"));

                //StringBuilder st2 = new StringBuilder();

                //st2.Append("window.opener.document.forms[0].submit(); window.alert('Absenteísmo salvo !  Existem absenteísmos anteriores com pelo menos 15 dias corridos ou com total de dias afastados de pelo 15 dias nos últimos 2 meses. '); ");

                //this.ClientScript.RegisterStartupScript(this.GetType(), "AbsentismoEmpregado", st2.ToString(), true);
                return 1;
            }
            else
            {
                return 0;
            }


        }


        protected void Envio_Email_Prajna(string xPara, string xCopia, string xSubject, string xBody, string xAttach, string xModulo, Int32 xIdEmpregado, Int32 xIdEmpresa)
        {

            string xDestinatario = "";

            //enviar e-mail para clinica e prajna

            MailMessage objEmail = new MailMessage();

            //rementente do email           
            objEmail.From = new MailAddress("agendamento@5aessencenet.com.br");


            //para
            string xEmail = xPara;

            if (xEmail.IndexOf(";") > 0) xEmail = xEmail.Substring(0, xEmail.IndexOf(";"));

            objEmail.To.Add(xEmail);
            xDestinatario = xEmail + "; agendamento@5aessence.com.br ;";

            objEmail.CC.Add("agendamento@5aessence.com.br");


            objEmail.Priority = MailPriority.Normal;
            objEmail.IsBodyHtml = true;

            objEmail.Subject = xSubject;
            objEmail.Body = xBody;
            objEmail.SubjectEncoding = Encoding.GetEncoding("ISO-8859-1");
            objEmail.BodyEncoding = Encoding.GetEncoding("ISO-8859-1");

            //Attachment xItem = new Attachment(xAttach);
            //objEmail.Attachments.Add(xItem);

            SmtpClient objSmtp = new SmtpClient();
            //objSmtp.Host = "mail.exchange.locaweb.com.br";
            //objSmtp.Port = 587;
            //objSmtp.Credentials = new NetworkCredential("agendamento@essencenet.com.br", "kr.prj1705");

            //objSmtp.EnableSsl = true;

            //objSmtp.Host = "outlook.office.com";
            //objSmtp.Host = "smtp.office365.com";
            //objSmtp.Port = 587;
            //objSmtp.Credentials = new System.Net.NetworkCredential("agendamento@5aessence.com.br", "Agend_5060");

            objSmtp.EnableSsl = false;

            //objSmtp.Host = "outlook.office.com";


            //objSmtp.Host = "smtp.office365.com";
            //objSmtp.Port = 587;                       
            //objSmtp.Credentials = new NetworkCredential("agendamento@5aessence.com.br", "Agend_5060");

            objSmtp.Host = "smtp.5aessencenet.com.br";
            objSmtp.Port = 587;
            objSmtp.Credentials = new System.Net.NetworkCredential("agendamento@5aessencenet.com.br", "Prana@2022!@");


            objSmtp.Send(objEmail);

            Ilitera.Data.Clientes_Clinicas xEnvio = new Ilitera.Data.Clientes_Clinicas();
            xEnvio.Envio_eMail(xIdEmpregado, xIdEmpresa, xDestinatario, "Absenteísmo");

            return;

        }




        protected void Envio_Email_Ilitera(string xPara, string xCopia, string xSubject, string xBody, string xAttach, string xModulo, Int32 xIdEmpregado, Int32 xIdEmpresa)
        {
            string xDestinatario = "";
            //enviar e-mail para clinica e prajna

            MailMessage objEmail = new MailMessage();

            //rementente do email
            objEmail.From = new MailAddress("agendamento1@ilitera.com.br");



            string xEmail = xPara;


            if (xEmail.IndexOf(";") > 0) xEmail = xEmail.Substring(0, xEmail.IndexOf(";"));

            objEmail.To.Add(xEmail);
            xDestinatario = xEmail + "; atendimento@ilitera.com.br ;";

            //objEmail.Bcc.Add("oculto@provedor.com.br");

            objEmail.CC.Add("atendimento@ilitera.com.br");


            //if (Ilitera.Data.SQLServer.EntitySQLServer._LocalServer.ToUpper().IndexOf("EY") > 0)
            //    objEmail.CC.Add("alberto.pereira@br.ey.com");
            //else
            //    objEmail.CC.Add("lucas.sp.sto@ilitera.com.br");


            objEmail.Priority = MailPriority.Normal;
            objEmail.IsBodyHtml = true;

            objEmail.Subject = xSubject;
            objEmail.Body = xBody;
            objEmail.SubjectEncoding = Encoding.GetEncoding("ISO-8859-1");
            objEmail.BodyEncoding = Encoding.GetEncoding("ISO-8859-1");

            //Attachment xItem = new Attachment(xAttach);
            //objEmail.Attachments.Add(xItem);

            SmtpClient objSmtp = new SmtpClient();
            //objSmtp.Host = "smtp.ilitera.com.br";
            objSmtp.Host = "smtp.office365.com";
            objSmtp.EnableSsl = true;
            objSmtp.Port = 587;
            objSmtp.Credentials = new System.Net.NetworkCredential("agendamento1@ilitera.com.br", "Ilitera_3624");


           Ilitera.Data.Clientes_Clinicas xEnvio = new Ilitera.Data.Clientes_Clinicas();
           objSmtp.Send(objEmail);

           xEnvio.Envio_eMail(xIdEmpregado, xIdEmpresa, xDestinatario, "Absenteísmo");



            return;

        }


        private void PopulaCAT()//SqlTransaction transaction)
		{
			CAT cat = new CAT();
			
			if (acidente.IdCAT.Id == 0)
			{
				cat.Inicialize();
				cat.IdEmpregado = empregado;
			}
			else
			{
				acidente.IdCAT.Find();
				cat = acidente.IdCAT;
			}

			cat.NumeroCAT = txtNumeroCAT.Text;
			cat.DataEmissao = new DateTime(Convert.ToInt32(txtAnoCAT.Text), Convert.ToInt32(txtMesCAT.Text), Convert.ToInt32(txtDiaCAT.Text), 
				Convert.ToInt32( ddlHoraCAT.SelectedItem.Value), Convert.ToInt32(ddlMinutoCAT.SelectedItem.Value), 0, 0);
			cat.IndEmitente = Convert.ToInt32(rblEmitente.SelectedItem.Value);
			cat.IndTipoCAT = Convert.ToInt32(rblTipoCAT.SelectedItem.Value);
			cat.hasRegPolicial = Convert.ToBoolean(Convert.ToInt32(rblRegPol.SelectedItem.Value));
			if (cat.hasRegPolicial)
				cat.BO = txtBO.Text;
			else
				cat.BO = string.Empty;
			cat.hasMorte = Convert.ToBoolean(Convert.ToInt32(rblMorte.SelectedItem.Value));
			if (cat.hasMorte)
				cat.DataObito = new DateTime(Convert.ToInt32(txtaao.Text), Convert.ToInt32(txtmmo.Text), Convert.ToInt32(txtddo.Text));
			else
				cat.DataObito = new DateTime();



            string xNomeArq = File1.FileName.Trim();


            if (xNomeArq != string.Empty)
            {

                string xExtension = xNomeArq.Substring(xNomeArq.Length - 3, 3).ToUpper().Trim();

                if (xExtension == "PDF" || xExtension == "JPG")
                {

                    Cliente xCliente = new Cliente();
                    xCliente.Find(System.Convert.ToInt32(Request["IdEmpresa"].ToString()));

                    string xArq = "";

                    //if (Session["Servidor_Web"].ToString().Trim().ToUpper() == "ILITERA")
                    xArq = "I:\\fotosdocsdigitais\\" + xCliente.DiretorioPadrao.ToString().ToUpper().Trim() + "\\Prontuario\\" + xNomeArq;
                    // else
                    //    xArq = "C:\\DRIVE_I\\fotosdocsdigitais\\" + xCliente.DiretorioPadrao.ToString().ToUpper().Trim() + "\\Prontuario\\" + xNomeArq;

                    File1.SaveAs(xArq);

                    xArq = "I:\\fotosdocsdigitais\\" + xCliente.DiretorioPadrao.ToString().ToUpper().Trim() + "\\Prontuario\\" + xNomeArq;

                    cat.Arquivo_Cat = xArq;

                }

            }


          

            cat.UsuarioId = Convert.ToInt32(Session["Empregado"].ToString());
			//cat.Transaction = transaction;
			acidente.IdCAT.Id = cat.Save();
		}

        private void PopulaTela()
        {
            txtdda.Text = acidente.DataAcidente.ToString("dd");
            txtmma.Text = acidente.DataAcidente.ToString("MM");
            txtaaa.Text = acidente.DataAcidente.ToString("yyyy");

            ddlHora.Items.FindByValue(acidente.DataAcidente.ToString("HH")).Selected = true;
            ddlMinuto.Items.FindByValue(acidente.DataAcidente.ToString("mm")).Selected = true;

            if ( acidente.hrsTrabAntesAcid != null )
            {
                ddlHoraAntes.Items.FindByValue(acidente.hrsTrabAntesAcid.Substring(0,2)).Selected = true;
                ddlMinutoAntes.Items.FindByValue(acidente.hrsTrabAntesAcid.Substring(2, 2)).Selected = true;
            }

            if (acidente.UltDiaTrab != null  && acidente.UltDiaTrab != new DateTime(2000,1,1) )
            {
                txtUltDia.Enabled = true;
                txtUltMes.Enabled = true;
                txtUltAno.Enabled = true;
                txtUltDia.Text = acidente.UltDiaTrab.ToString("dd");
                txtUltMes.Text = acidente.UltDiaTrab.ToString("MM");
                txtUltAno.Text = acidente.UltDiaTrab.ToString("yyyy");
                chk_UltDia.Checked = true;
            }
            else
            {
                txtUltDia.Enabled = false;
                txtUltMes.Enabled = false;
                txtUltAno.Enabled = false;
                chk_UltDia.Checked = false;
            }


            rblTipoAcidente.ClearSelection();
            rblTipoAcidente.Items.FindByValue(acidente.IndTipoAcidente.ToString()).Selected = true;

            txtMembroAtingido.Text = acidente.MembroAtingido;
            txtAgenteCausador.Text = acidente.AgenteCausador;
            txtNaturezaLesao.Text = acidente.NaturezaLesao;

            

            if (txtMembroAtingido.Text.Trim() == "" && txtAgenteCausador.Text.Trim() == "" && txtNaturezaLesao.Text.Trim() == "")
            {
                txtMembroAtingido.Visible = false;
                lblMembroAtingido.Visible = false;
                txtAgenteCausador.Visible = false;
                lblAgenteCausador.Visible = false;
                txtNaturezaLesao.Visible = false;
                lblNaturezaLesao.Visible = false;
            }
            else
            {
                txtMembroAtingido.Visible = true;
                lblMembroAtingido.Visible = true;
                txtAgenteCausador.Visible = true;
                lblAgenteCausador.Visible = true;
                txtNaturezaLesao.Visible = true;
                lblNaturezaLesao.Visible = true;
            }

            txtMembroAtingido.Enabled = false;
            txtNaturezaLesao.Enabled = false;
            txtAgenteCausador.Enabled = false;



            txtDescricao.Text = acidente.Descricao;

            if (acidente.IdJuridica.Id != 0)
                ddlLocalAcidente.Items.FindByValue(acidente.IdJuridica.Id.ToString()).Selected = true;
            else if (acidente.IdLocalAcidente.Id != 0)
                ddlLocalAcidente.Items.FindByValue(acidente.IdLocalAcidente.Id.ToString()).Selected = true;

            txtEspecLocal.Text = acidente.EspecLocal;

            rblSetor.ClearSelection();
            if (acidente.indTipoSetor != 0)
            {
                rblSetor.Items.FindByValue(acidente.indTipoSetor.ToString()).Selected = true;
            }
            if (acidente.indTipoSetor == (int)TipoSetor.OutroSetor)
            {
                Color forecolor = Color.FromName("#44926D");
                lblSetor.ForeColor = forecolor;
                ddlSetor.Enabled = true;
                PopulaDDLSetor();
                ddlSetor.Items.FindByValue(acidente.IdSetor.Id.ToString()).Selected = true;
            }


            rd_TipoLocal.SelectedValue = acidente.IdTipoLocal.ToString();


            txtEndereco.Text = acidente.Logradouro.Trim();
            txtNumero.Text = acidente.Nr_Logradouro.Trim();
            //txtMunicipio.Text = acidente.Municipio.Trim();
            //txtUF.Text = acidente.UF.Trim().ToUpper();

            ddlUF.ClearSelection();

            if (acidente.UF.Trim() != "")
            {                 
                ddlUF.Items.FindByText(acidente.UF.Trim()).Selected = true;
                
                PopulaDDLMunicipio(ddlUF.SelectedValue.ToString().Trim());
            }

            if (acidente.Municipio.Trim() != "")
            {
                //ddlMunicipio.ClearSelection();
                //ddlMunicipio.Items.FindByValue(acidente.Municipio.Trim()).Selected = true;
                for ( int fCont=0;fCont<ddlMunicipio.Items.Count; fCont++)
                {
                    //if ( ddlMunicipio.Items[fCont].Text.ToUpper().Trim() == acidente.Municipio.ToUpper().Trim())
                    if (ddlMunicipio.Items[fCont].Value.ToUpper().Trim() == acidente.Municipio.ToUpper().Trim())
                    {
                        ddlMunicipio.SelectedIndex = fCont;
                        break;
                    }
                }
            }


            if (acidente.codPostal != null )
               txtCodPostal.Text = acidente.codPostal.Trim();

            if (acidente.dscLocal != null )
               txtDescLocal.Text = acidente.dscLocal.Trim();

            if (acidente.CNPJ_Terceiro != null)
                txtCNPJTerceiros.Text = acidente.CNPJ_Terceiro.Trim();

            if (acidente.Bairro != null)
                txtBairro.Text = acidente.Bairro.Trim();

            if (acidente.Complemento != null)
                txtComplemento.Text = acidente.Complemento.Trim();

            if (acidente.CEP != null)
                txtCEP.Text = acidente.CEP.Trim();


            if (acidente.IdTipoLocal == 1)
            {
                ddlLocalAcidente.Enabled = true;
                btnAddLocal.Enabled = true;
                rblSetor.Enabled = true;
                lblSetor.Enabled = true;
                lblLocalAcidente.Enabled = true;

                lblSetor.ForeColor = Color.FromName("#44926D");
                lblLocalAcidente.ForeColor = Color.FromName("#44926D");

                lblEndereco.Enabled = false;
                lblNumero2.Enabled = false;
                lblMunicipio.Enabled = false;
                lblUF.Enabled = false;
                lblBairro.Enabled = false;
                lblComplemento.Enabled = false;
                lblCEP.Enabled = false;

                lblEndereco.ForeColor = System.Drawing.Color.LightGray;
                lblNumero2.ForeColor = System.Drawing.Color.LightGray;
                lblMunicipio.ForeColor = System.Drawing.Color.LightGray;
                lblUF.ForeColor = System.Drawing.Color.LightGray;

                lblBairro.ForeColor = System.Drawing.Color.LightGray;
                lblComplemento.ForeColor = System.Drawing.Color.LightGray;
                lblCEP.ForeColor = System.Drawing.Color.LightGray;

                txtEndereco.Enabled = false;
                txtNumero.Enabled = false;
                //txtMunicipio.Enabled = false;
                //txtUF.Enabled = false;
                ddlMunicipio.Enabled = false;
                ddlUF.Enabled = false;

                txtBairro.Enabled = false;
                txtComplemento.Enabled = false;
                txtCEP.Enabled = false;

                txtCodPostal.Enabled =false;
                lblCodPostal.Enabled = false;
            }
            else if (acidente.IdTipoLocal == 2)
            {
                txtCodPostal.Enabled = true;
                lblCodPostal.Enabled = true;
            }
            else
            {
                ddlLocalAcidente.Enabled = false;
                btnAddLocal.Enabled = false;
                rblSetor.Enabled = false;
                lblSetor.Enabled = false;
                lblLocalAcidente.Enabled = false;

                lblSetor.ForeColor = System.Drawing.Color.LightGray;
                lblLocalAcidente.ForeColor = System.Drawing.Color.LightGray;

                lblEndereco.Enabled = true;
                lblNumero2.Enabled = true;
                lblMunicipio.Enabled = true;
                lblUF.Enabled = true;

                lblBairro.Enabled = true;
                lblComplemento.Enabled = true;
                lblCEP.Enabled = true;

                txtEndereco.Enabled = true;
                txtNumero.Enabled = true;
                //txtMunicipio.Enabled = true;
                //txtUF.Enabled = true;
                ddlMunicipio.Enabled = true;
                ddlUF.Enabled = true;

                txtBairro.Enabled = true;
                txtComplemento.Enabled = true;
                txtCEP.Enabled = true;

                lblEndereco.ForeColor = Color.FromName("#44926D");
                lblNumero2.ForeColor = Color.FromName("#44926D");
                lblMunicipio.ForeColor = Color.FromName("#44926D");
                lblUF.ForeColor = Color.FromName("#44926D");

                lblBairro.ForeColor = Color.FromName("#44926D");
                lblComplemento.ForeColor = Color.FromName("#44926D");
                lblCEP.ForeColor = Color.FromName("#44926D");

                txtCodPostal.Enabled = false;
                lblCodPostal.Enabled = false;

            }


            rd_Lateralidade.SelectedValue = acidente.IdLateralidade.ToString();

            if (acidente.HasInternacao == false)
                rd_Internacao.SelectedValue = "0";
            else
                rd_Internacao.SelectedValue = "1";

            txtCNES.Text = acidente.CNES;


            if (acidente.DataInternacao != new DateTime())
            {
                txtddAt.Text = acidente.DataInternacao.ToString("dd");
                txtmmAt.Text = acidente.DataInternacao.ToString("MM");
                txtaaAt.Text = acidente.DataInternacao.ToString("yyyy");

                ddlHoraAt.Items.FindByValue(acidente.DataInternacao.ToString("HH")).Selected = true;
                ddlMinutoAt.Items.FindByValue(acidente.DataInternacao.ToString("mm")).Selected = true;
            }

            txtDuracao.Text = acidente.DuracaoInternacao.ToString().Trim();

            txtMedico.Text = acidente.MedicoInternacao;
            txtCRM.Text = acidente.CRMInternacao;
            txtCRMUF.Text = acidente.UFInternacao;

            txtDiagnostico.Text = acidente.DiagnosticoProvavel;
            


            rblIniciativa.SelectedValue = acidente.IdIniciativaCat.ToString();

            if (acidente.Codigo_Agente_Causador != 0)
                ddlAgenteCausador.Items.FindByValue(acidente.Codigo_Agente_Causador.ToString().Trim()).Selected = true;

            if (acidente.Codigo_Descricao_Lesao != 0)
                ddlDescricaoLesao.Items.FindByValue(acidente.Codigo_Descricao_Lesao.ToString().Trim()).Selected = true;

            if (acidente.Codigo_Parte_Corpo_Atingida!= 0)
                ddlParteCorpo.Items.FindByValue(acidente.Codigo_Parte_Corpo_Atingida.ToString().Trim()).Selected = true;

            if (acidente.Codigo_Situacao_Geradora != 0)
                ddlSituacaoGeradora.Items.FindByValue(acidente.Codigo_Situacao_Geradora.ToString().Trim()).Selected = true;

            if (acidente.Codigo_Acidente_Trabalho != null && acidente.Codigo_Acidente_Trabalho.Trim() != "")
                ddlTipoAcidente.Items.FindByValue(acidente.Codigo_Acidente_Trabalho.Trim()).Selected = true;

            if (acidente.IdCID.Id != 0)
            {
                acidente.IdCID.Find();
                ddlCID.Items.Insert(0, new ListItem(acidente.IdCID.Descricao, acidente.IdCID.Id.ToString()));

                txtCID.Text = ddlCID.Items[ddlCID.SelectedIndex].ToString().Trim();
                lbl_Id1.Text = ddlCID.SelectedValue.ToString().Trim();
            }

            if (acidente.IdCID2 != 0)
            {
                CID xCid = new CID();
                xCid.Find(acidente.IdCID2);

                ddlCID.Items.Insert(0, new ListItem(xCid.Descricao, xCid.Id.ToString()));
                //ddlCID.Items.FindByValue(xCid.Id.ToString());
                //ddlCID.Items.FindByText(xCid.Descricao);

                txtCID2.Text = xCid.Descricao;
                lbl_Id2.Text = xCid.Id.ToString();
            }

            if (acidente.IdCID3 != 0)
            {
                CID xCid = new CID();
                xCid.Find(acidente.IdCID3);

                ddlCID.Items.Insert(0, new ListItem(xCid.Descricao, xCid.Id.ToString()));
                // ddlCID.Items.FindByValue(xCid.Id.ToString());

                txtCID3.Text = xCid.Descricao;
                lbl_Id3.Text = xCid.Id.ToString();
            }

            if (acidente.IdCID4 != 0)
            {
                CID xCid = new CID();
                xCid.Find(acidente.IdCID4);

                ddlCID.Items.Insert(0, new ListItem(xCid.Descricao, xCid.Id.ToString()));
                //ddlCID.Items.FindByValue(xCid.Id.ToString());

                txtCID4.Text = xCid.Descricao;
                lbl_Id4.Text = xCid.Id.ToString();
            }

			rblTransfSetor.ClearSelection();
			rblTransfSetor.Items.FindByValue(Convert.ToInt32(acidente.isTransfSetor).ToString()).Selected = true;
			rblAposInval.ClearSelection();
            rblAposInval.Items.FindByValue(Convert.ToInt32(acidente.isAposInval).ToString()).Selected = true;
            wcePerdaMaterial.Text = acidente.PerdaMaterial.ToString();
            txtObservacoes.Text = acidente.Observacoes;

			rblAfastamento.ClearSelection();
			rblAfastamento.Items.FindByValue(Convert.ToInt32(acidente.hasAfastamento).ToString()).Selected = true;
			   
			if (acidente.hasAfastamento)
			{
                //uwtAcidentes.Tabs.GetTab(5).Enabled = true;
				
				ArrayList alAbsentismo = new Afastamento().Find("IdAcidente=" + acidente.Id);

				if (alAbsentismo.Count == 1)
				{
                    try
                    {
                        Afastamento absentismo = (Afastamento)alAbsentismo[0];

                        ddlAbsentismo.ClearSelection();
                        ddlAbsentismo.Items.FindByValue(absentismo.Id.ToString()).Selected = true;

                        PopulaTelaAbsentismo(absentismo);
                    }
                    catch ( Exception ex)
                    {

                    }
				}
			}

            

            if (acidente.IdCAT.Id != 0)
			{
				acidente.IdCAT.Find();
				rblCAT.ClearSelection();
				rblCAT.Items.FindByValue("1").Selected = true;


                if (acidente.IdCAT.Arquivo_Cat != null)
                {
                    if (acidente.IdCAT.Arquivo_Cat.Trim() != "")
                    {
                        txt_Arq.Text = acidente.IdCAT.Arquivo_Cat.Trim();
                        txt_Arq.ReadOnly = true;
                    }
                    else
                    {
                        txt_Arq.Text = "";
                        txt_Arq.ReadOnly = true;
                    }
                }
                else
                {
                    txt_Arq.Text = "";
                    txt_Arq.ReadOnly = true;
                }


                //uwtAcidentes.Tabs.GetTab(4).Enabled = true;

                txtNumeroCAT.Text  = acidente.IdCAT.NumeroCAT;

            
				txtDiaCAT.Text = acidente.IdCAT.DataEmissao.ToString("dd");
				txtMesCAT.Text = acidente.IdCAT.DataEmissao.ToString("MM");
				txtAnoCAT.Text = acidente.IdCAT.DataEmissao.ToString("yyyy");

				ddlHoraCAT.Items.FindByValue(acidente.IdCAT.DataEmissao.ToString("HH")).Selected = true;
				ddlMinutoCAT.Items.FindByValue(acidente.IdCAT.DataEmissao.ToString("mm")).Selected = true;

				if (acidente.IdCAT.IndEmitente != 0)
					rblEmitente.Items.FindByValue(acidente.IdCAT.IndEmitente.ToString()).Selected = true;
				if (acidente.IdCAT.IndTipoCAT != 0)
					rblTipoCAT.Items.FindByValue(acidente.IdCAT.IndTipoCAT.ToString()).Selected = true;
				if (acidente.IdCAT.hasRegPolicial)
				{
					rblRegPol.ClearSelection();
					rblRegPol.Items.FindByValue("1").Selected = true;
					Color forecolor = Color.FromName("#44926D");
					Color bordercolor = Color.FromName("#7CC5A1");
					Color backcolor = Color.FromName("#FCFEFD");
					lblBO.ForeColor = forecolor;
					txtBO.Enabled = true;
					txtBO.BackColor = backcolor;
					txtBO.BorderColor = bordercolor;
					txtBO.Text = acidente.IdCAT.BO;
				}
				if (acidente.IdCAT.hasMorte)
				{
					rblMorte.ClearSelection();
					rblMorte.Items.FindByValue("1").Selected = true;
					Color forecolor = Color.FromName("#44926D");
					Color bordercolor = Color.FromName("#7CC5A1");
					Color backcolor = Color.FromName("#FCFEFD");
					lblObito.ForeColor = forecolor;
					lblBarra1.ForeColor = forecolor;
					lblBarra2.ForeColor = forecolor;
					txtddo.BorderColor = bordercolor;
					txtddo.BackColor = backcolor;
					txtmmo.BorderColor = bordercolor;
					txtmmo.BackColor = backcolor;
					txtaao.BorderColor = bordercolor;
					txtaao.BackColor = backcolor;
					txtddo.Enabled = true;
					txtmmo.Enabled = true;
					txtaao.Enabled = true;
					txtddo.Text = acidente.IdCAT.DataObito.ToString("dd");
					txtmmo.Text = acidente.IdCAT.DataObito.ToString("MM");
					txtaao.Text = acidente.IdCAT.DataObito.ToString("yyyy");
				}
			}
		}

		protected void btnOK_Click(object sender, System.EventArgs e)
		{


            using (SqlConnection connection = new SqlConnection(Ilitera.Data.SQLServer.EntitySQLServer.GetConnection()))
            {
                connection.Open();
                //SqlTransaction transaction = connection.BeginTransaction();

                try
                {

                    //checar parte do corpo atingida e ver se tem lateralidade
                    string xParteCorpo = "0";

                    if (System.Convert.ToInt16(rd_TipoLocal.SelectedValue)==3)
                    {
                        string zCNPJ = new String(txtCNPJTerceiros.Text.Trim().Where(Char.IsDigit).ToArray());

                        txtCNPJTerceiros.Text = zCNPJ.Trim();

                        if ( txtCNPJTerceiros.Text.Length < 14)
                        {
                            MsgBox1.Show("Acidente", "Necessário indicar CNPJ válido para este tipo de local de acidente.", null,
                            new EO.Web.MsgBoxButton("OK"));
                            return;
                        }
                    }

                    if ( rblSetor.SelectedIndex < 0)
                    {
                        MsgBox1.Show("Acidente", "Selecione indicação de setor do acidente.", null,
                        new EO.Web.MsgBoxButton("OK"));
                        return;
                    }


                    if (ddlParteCorpo.SelectedIndex >= 0) xParteCorpo = ddlParteCorpo.SelectedValue.ToString().Trim();

                    if ( xParteCorpo=="753050000" || xParteCorpo=="753510000" || xParteCorpo=="753510200" || 
                         xParteCorpo=="755010400" || xParteCorpo=="755010600" || xParteCorpo=="755030000" ||
                         xParteCorpo=="755050000" || xParteCorpo=="755070000" || xParteCorpo=="755080000" ||
                         xParteCorpo=="755090000" || xParteCorpo=="756020000" || xParteCorpo=="757010000" ||
                         xParteCorpo=="755010200" || xParteCorpo=="757010400" || xParteCorpo=="757010600" ||
                         xParteCorpo=="757030000" || xParteCorpo=="757050000" || xParteCorpo=="757080000" ||
                         xParteCorpo=="757090000" || xParteCorpo=="758000000" )
                    {
                        if (rd_Lateralidade.SelectedValue.ToString().Trim()=="0")
                        {
                            MsgBox1.Show("Acidente", "Necessário indicar lateralidade da parte do corpo atingida! ", null,
                            new EO.Web.MsgBoxButton("OK"));
                            return;
                        }
                    }

                    if ( txtCEP.Text.Trim() !="" )
                    {
                        if ( txtCEP.Text.Trim().Length < 8)
                        {
                            MsgBox1.Show("Acidente", "CEP deve ter 8 números! ", null,
                            new EO.Web.MsgBoxButton("OK"));
                            return;
                        }
                        if (txtCEP.Text.IndexOf("-")>=0)
                        {
                            MsgBox1.Show("Acidente", "CEP não deve ter traço! ", null,
                            new EO.Web.MsgBoxButton("OK"));
                            return;
                        }
                        if (System.Text.RegularExpressions.Regex.IsMatch(txtCEP.Text.Trim(), @"^\d+$")==false )
                        {
                            MsgBox1.Show("Acidente", "CEP deve ter 8 números! ", null,
                            new EO.Web.MsgBoxButton("OK"));
                            return;
                        }
                    }

                    if (txtMedico.Text.Trim() == "")
                    {
                        MsgBox1.Show("Acidente", "Necessário preencher médico! ", null,
                        new EO.Web.MsgBoxButton("OK"));
                        return;
                    }

                    if (txtCRM.Text.Trim() == "")
                    {
                        MsgBox1.Show("Acidente", "Necessário preencher CRM de médico! ", null,
                        new EO.Web.MsgBoxButton("OK"));
                        return;
                    }

                    if (txtCRMUF.Text.Trim() == "")
                    {
                        MsgBox1.Show("Acidente", "Necessário preencher UF de CRM do médico! ", null,
                        new EO.Web.MsgBoxButton("OK"));
                        return;
                    }

                    if (txtaaAt.Text.Trim() == "")
                    {
                        MsgBox1.Show("Acidente", "Necessário preencher data de atendimento! ", null,
                        new EO.Web.MsgBoxButton("OK"));
                        return;
                    }

                    if ( txtDiagnostico.Text.Trim()=="")
                    {
                        MsgBox1.Show("Acidente", "Necessário preencher diagnóstico provável! ", null,
                        new EO.Web.MsgBoxButton("OK"));
                        return;
                    }
                    
                    if ( tabtxtCID.Text.Trim() == "")
                    {
                        MsgBox1.Show("Acidente", "Necessário preencher CID! ", null,
                        new EO.Web.MsgBoxButton("OK"));
                        return;
                    }





                    DateTime xDataAcidente = new DateTime();
                    try
                    {
                        xDataAcidente = new DateTime(Convert.ToInt32(txtaaa.Text), Convert.ToInt32(txtmma.Text), Convert.ToInt32(txtdda.Text),
                        Convert.ToInt32(ddlHora.SelectedItem.Value), Convert.ToInt32(ddlMinuto.SelectedItem.Value), 0, 0);
                    }
                    catch ( Exception Ex)
                    {
                        MsgBox1.Show("Acidente", "Data do Acidente inválida! ", null,
                        new EO.Web.MsgBoxButton("OK"));
                        return;
                    }

                    if ( chk_UltDia.Checked == true)
                    {
                        DateTime xDataUltDia = new DateTime();
                        try
                        {
                            xDataUltDia = new DateTime(Convert.ToInt32(txtUltAno.Text), Convert.ToInt32(txtUltMes.Text), Convert.ToInt32(txtUltDia.Text));
                        }
                        catch (Exception Ex)
                        {
                            MsgBox1.Show("Acidente", "Data do Último dia de trabalho inválida! ", null,
                            new EO.Web.MsgBoxButton("OK"));
                            return;
                        }

                    }

                    DateTime xDataInternacao = new DateTime();
                    try
                    {
                        xDataInternacao = new DateTime(Convert.ToInt32(txtaaAt.Text), Convert.ToInt32(txtmmAt.Text), Convert.ToInt32(txtddAt.Text),
                        Convert.ToInt32(ddlHoraAt.SelectedItem.Value), Convert.ToInt32(ddlMinutoAt.SelectedItem.Value), 0, 0);
                    }
                    catch ( Exception Ex)
                    {
                        MsgBox1.Show("Acidente", "Data da Internação inválida! ", null,
                        new EO.Web.MsgBoxButton("OK"));
                        return;
                    }


                    if (rblAfastamento.SelectedValue == "1")
                    {
                        DateTime xDataInicial = new DateTime();
                        try
                        {
                            xDataInicial = new DateTime(Convert.ToInt32(txtaai.Text), Convert.ToInt32(txtmmi.Text), Convert.ToInt32(txtddi.Text),
                            Convert.ToInt32(ddlHoraIni.SelectedValue), Convert.ToInt32(ddlMinutoIni.SelectedValue), 0, 0);
                        }
                        catch (Exception Ex)
                        {
                            MsgBox1.Show("Acidente", "Data de Absenteísmo Inicial Inválida! ", null,
                            new EO.Web.MsgBoxButton("OK"));
                            return;
                        }

                        if (txtmmp.Text != string.Empty)
                        {
                            DateTime xDataPrevista = new DateTime();
                            try
                            {
                                xDataPrevista = new DateTime(Convert.ToInt32(txtaap.Text), Convert.ToInt32(txtmmp.Text), Convert.ToInt32(txtddp.Text));
                            }
                            catch (Exception Ex)
                            {
                                MsgBox1.Show("Acidente", "Data de Absenteísmo Prevista Inválida! ", null,
                                new EO.Web.MsgBoxButton("OK"));
                                return;
                            }
                        }

                        if (txtmmr.Text != string.Empty)
                        {
                            DateTime xDataVolta = new DateTime();
                            try
                            {
                                xDataVolta = new DateTime(Convert.ToInt32(txtaar.Text), Convert.ToInt32(txtmmr.Text), Convert.ToInt32(txtddr.Text),
                                    Convert.ToInt32(ddlHoraRet.SelectedValue), Convert.ToInt32(ddlMinutoRet.SelectedValue), 0, 0);
                            }
                            catch (Exception Ex)
                            {
                                MsgBox1.Show("Acidente", "Data de Volta de Absenteísmo Inválida! ", null,
                                new EO.Web.MsgBoxButton("OK"));
                                return;
                            }

                        }

                    }


                    if (rblCAT.SelectedValue == "1")
                    {
                        DateTime xDataEmissao = new DateTime();

                        try
                        {
                            xDataEmissao = new DateTime(Convert.ToInt32(txtAnoCAT.Text), Convert.ToInt32(txtMesCAT.Text), Convert.ToInt32(txtDiaCAT.Text),
                            Convert.ToInt32(ddlHoraCAT.SelectedItem.Value), Convert.ToInt32(ddlMinutoCAT.SelectedItem.Value), 0, 0);
                        }
                        catch (Exception Ex)
                        {
                            MsgBox1.Show("Acidente", "Data de Emissão de Cat Inválida! ", null,
                            new EO.Web.MsgBoxButton("OK"));
                            return;
                        }
                    }




                    if (tabtxtEspecLocal.Text.Trim() == "")
                    {
                        MsgBox1.Show("Acidente", "Preencha especificação do local do acidente! ", null,
                        new EO.Web.MsgBoxButton("OK"));
                        return;
                    }

                    if (ddlHoraAntes.SelectedItem.Value == "00" && ddlMinutoAntes.SelectedItem.Value == "00")
                    {
                        //if (rd_TipoLocal.SelectedValue.ToString().Trim() != "4")
                        //{
                        //    MsgBox1.Show("Acidente", "Hora trabalhadas antes do acidente inválida! ", null,
                        //    new EO.Web.MsgBoxButton("OK"));
                        //    return;
                        //}
                    }


                    if (ddlHora.SelectedItem.Value == "00" && ddlMinuto.SelectedItem.Value == "00")
                    {
                        MsgBox1.Show("Acidente", "Hora do acidente inválida! ", null,
                        new EO.Web.MsgBoxButton("OK"));
                        return;
                    }
                    else
                    {
                        int xAlerta = 0;

                        xAlerta = PopulaAcidente(); // transaction);

                        //transaction.Commit();
                        ViewState["IdAcidente"] = acidente.Id;

                        StringBuilder st = new StringBuilder("");

                        //if (xAlerta != 1)
                        //{
                        //    //st.Append("window.opener.document.forms[0].submit(); window.alert('O Acidente foi " + tipo + " com sucesso!');");
                        //    MsgBox1.Show("Acidente", "Acidente salvo ! ", null,
                        //    new EO.Web.MsgBoxButton("OK"));

                        //}
                        //else
                        //{
                        //    // st.Append("window.opener.document.forms[0].submit(); window.alert('Acidente salvo !  Existem absenteísmos anteriores com pelo menos 15 dias corridos ou com total de dias afastados de pelo 15 dias nos últimos 2 meses. ');");
                        //    MsgBox1.Show("Acidente", "Acidente salvo !  Existem absenteísmos anteriores com pelo menos 15 dias corridos ou com total de dias afastados de pelo 15 dias nos últimos 2 meses. ", null,
                        //    new EO.Web.MsgBoxButton("OK"));
                        //}

                        this.ClientScript.RegisterStartupScript(this.GetType(), "AcidenteEmpregado", st.ToString(), true);

                        if (xAlerta != 1)
                            Response.Write("<script>window.opener.document.forms[0].submit(); window.alert('O Acidente foi " + tipo + " com sucesso!'); window.close();</script>");
                        else
                            Response.Write("<script>window.opener.document.forms[0].submit(); window.alert('Acidente salvo !  Existem absenteísmos anteriores com pelo menos 15 dias corridos ou com total de dias afastados de pelo 15 dias nos últimos 2 meses. '); window.close();</script>");
                    }
                }
                catch (Exception ex)
                {
                    //transaction.Rollback();
                    MsgBox1.Show("Acidente", ex.Message, null,
                                    new EO.Web.MsgBoxButton("OK"));

                    //Aviso(ex.Message);
                }

            }
			
		}

		protected void btnExcluir_Click(object sender, System.EventArgs e)
		{

            if (acidente.Id != 0)
            {
                Cliente cliente;
                cliente = new Cliente(System.Convert.ToInt32(Session["Empresa"].ToString()));


                Ilitera.Data.eSocial xChecagem = new Ilitera.Data.eSocial();
                if (xChecagem.Checar_Chave_eSocial(acidente.Id, "2210", cliente.ESocial_Ambiente) > 0)
                {
                    MsgBox1.Show("Ilitera.Net", "Acidente com evento criado no eSocial não pode ser excluído.", null,
                    new EO.Web.MsgBoxButton("OK"));
                    return;
                }
            }



            using (SqlConnection connection = new SqlConnection(Ilitera.Data.SQLServer.EntitySQLServer.GetConnection()))
			{
				connection.Open();
				SqlTransaction transaction = connection.BeginTransaction();

				try
				{
					CAT cat = new CAT();
				
					if (acidente.hasAfastamento)
					{
						ArrayList alAbsent = new Afastamento().Find("IdAcidente=" + acidente.Id);
				
						foreach (Afastamento afastamento in alAbsent)
						{
                            afastamento.UsuarioId = Convert.ToInt32(Session["Empregado"].ToString());
							afastamento.Transaction = transaction;
							afastamento.Delete();
						}
					}

					if (acidente.IdCAT.Id != 0)
						cat = new CAT(acidente.IdCAT.Id);
			
					acidente.UsuarioId = Convert.ToInt32(Session["Empregado"].ToString());
					acidente.UsuarioProcessoRealizado = "Exclusão de Acidente do Empregado " + empregado.tNO_EMPG;
					acidente.Transaction = transaction;
					acidente.Delete();

					if (cat.Id != 0)
					{
                        cat.UsuarioId = Convert.ToInt32(Session["Empregado"].ToString());
						cat.Transaction = transaction;
						cat.Delete();
					}

					transaction.Commit();

					StringBuilder st = new StringBuilder(""); 

					st.Append("window.opener.document.forms[0].submit(); window.alert('O Acidente foi deletado com sucesso!'); window.close();");

                    this.ClientScript.RegisterStartupScript(this.GetType(), "DeletaAcidente", st.ToString(), true);

                    Response.Write("<script>window.opener.document.forms[0].submit(); window.close();</script>"); 
				}

				catch(Exception ex)
				{
					transaction.Rollback();
					//Aviso(ex.Message);
                    MsgBox1.Show("Acidente", ex.Message, null,
                                    new EO.Web.MsgBoxButton("OK"));

				}
			}
		}

		private void btnAddLocal_Click(object sender, System.EventArgs e)
		{
			StringBuilder st = new StringBuilder();

			st.Append("addItemPop(centerWin('CadNovoLocalAcidente.aspx?IdEmpregado=" + Request["IdEmpregado"] + "&IdEmpresa=" + Request["IdEmpresa"] +  "&IdUsuario=" + Request["IdUsuario"] + "',400,255,\'CadNovoLocalAcidente\'))");

            this.ClientScript.RegisterStartupScript(this.GetType(), "NovoLocalAcidente", st.ToString(), true);
		}

		private void rblSetor_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (rblSetor.SelectedValue == ((int)TipoSetor.OutroSetor).ToString())
			{
				if (rblTipoAcidente.SelectedValue == ((int)TipoAcidente.Doenca).ToString())
				{
					rblSetor.ClearSelection();
					rblSetor.Items.FindByValue(((int)TipoSetor.SetorNormal).ToString()).Selected = true;
					lblSetor.ForeColor = System.Drawing.Color.LightGray;
					ddlSetor.Items.Clear();
					ddlSetor.Enabled = false;
					//Aviso("Não é possível selecionar um outro Setor! O Tipo de Acidente é Doença!");
				}
				else if (rblTipoAcidente.SelectedValue == ((int)TipoAcidente.Trajeto).ToString())
				{
					rblSetor.ClearSelection();
					rblSetor.Items.FindByValue(((int)TipoSetor.NaoAplicavel).ToString()).Selected = true;
					lblSetor.ForeColor = System.Drawing.Color.LightGray;
					ddlSetor.Items.Clear();
					ddlSetor.Enabled = false;
					//Aviso("Não é possível selecionar um outro Setor! O Tipo de Acidente é Trajeto!");
				}
				else
				{
					Color forecolor = Color.FromName("#44926D");
					lblSetor.ForeColor = forecolor;
					ddlSetor.Enabled = true;
					PopulaDDLSetor();
				}
			}
			else if (rblSetor.SelectedValue == ((int)TipoSetor.SetorNormal).ToString())
			{
				if (rblTipoAcidente.SelectedValue == ((int)TipoAcidente.Trajeto).ToString())
				{
					rblSetor.ClearSelection();
					rblSetor.Items.FindByValue(((int)TipoSetor.NaoAplicavel).ToString()).Selected = true;
					lblSetor.ForeColor = System.Drawing.Color.LightGray;
					ddlSetor.Items.Clear();
					ddlSetor.Enabled = false;
					//Aviso("Não é possível selecionar o Setor de Trabalho normal! O Tipo de Acidente é Trajeto!");
				}
				else
				{
					lblSetor.ForeColor = System.Drawing.Color.LightGray;
					ddlSetor.Items.Clear();
					ddlSetor.Enabled = false;
				}
			}
			else if (rblSetor.SelectedValue == ((int)TipoSetor.NaoAplicavel).ToString())
			{
				if (rblTipoAcidente.SelectedValue == ((int)TipoAcidente.Doenca).ToString() || rblTipoAcidente.SelectedValue == ((int)TipoAcidente.Tipico).ToString())
				{
					rblSetor.ClearSelection();
					rblSetor.Items.FindByValue(((int)TipoSetor.SetorNormal).ToString()).Selected = true;
					lblSetor.ForeColor = System.Drawing.Color.LightGray;
					ddlSetor.Items.Clear();
					ddlSetor.Enabled = false;
					//Aviso("Não é possível selecionar Não Aplicável! O Tipo de Acidente é Típico ou Doença!");
				}
				else
				{
					lblSetor.ForeColor = System.Drawing.Color.LightGray;
					ddlSetor.Items.Clear();
					ddlSetor.Enabled = false;
				}
			}
		}

		private void rblTipoAcidente_SelectedIndexChanged(object sender, EventArgs e)
		{
			switch (Convert.ToInt32(rblTipoAcidente.SelectedValue))
			{					
				case (int)TipoAcidente.Trajeto:
					rblSetor.ClearSelection();
					rblSetor.Items.FindByValue(((int)TipoSetor.NaoAplicavel).ToString()).Selected = true;
					lblSetor.ForeColor = System.Drawing.Color.LightGray;
					ddlSetor.Items.Clear();
					ddlSetor.Enabled = false;
					break;
				case (int)TipoAcidente.Doenca:
					rblSetor.ClearSelection();
					rblSetor.Items.FindByValue(((int)TipoSetor.SetorNormal).ToString()).Selected = true;
					lblSetor.ForeColor = System.Drawing.Color.LightGray;
					ddlSetor.Items.Clear();
					ddlSetor.Enabled = false;
					break;
				case (int)TipoAcidente.Tipico:
					if (rblSetor.SelectedValue == ((int)TipoSetor.NaoAplicavel).ToString())
					{
						rblSetor.ClearSelection();
						rblSetor.Items.FindByValue(((int)TipoSetor.SetorNormal).ToString()).Selected = true;
						lblSetor.ForeColor = System.Drawing.Color.LightGray;
						ddlSetor.Items.Clear();
						ddlSetor.Enabled = false;
					}
					break;
			}
		}

        protected void btnProcurar_Click(object sender, System.EventArgs e)
        {

            lbl_Procura.Text = "1";

            if (txtCID.Text.Trim() != string.Empty)
            {
                ddlCID.ClearSelection();

                while (ddlCID.Items.Count > 1)
                    ddlCID.Items.RemoveAt(0);

                DataSet ds = new CID().Get("CodigoCID='" + txtCID.Text.Trim() + "' OR Descricao LIKE '%" + txtCID.Text.Trim() + "%' ORDER BY Descricao DESC");

                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Select())
                        ddlCID.Items.Insert(0, new ListItem(row["Descricao"].ToString(), row["IdCID"].ToString()));
                    txtCID.Text = ddlCID.Items[ddlCID.SelectedIndex].ToString().Trim();
                    lbl_Id1.Text = ddlCID.SelectedValue.ToString().Trim();
                }
                else
                    MsgBox1.Show("Acidente", "O código '" + txtCID.Text + "' não foi encontrado! Por favor, tente novamente!", null,
                                   new EO.Web.MsgBoxButton("OK"));


                //Aviso("O código '" + txtCID.Text + "' não foi encontrado! Por favor, tente novamente!");
            }
            else

                MsgBox1.Show("Acidente", "É necessário colocar o Código CID para executar a busca!", null,
                               new EO.Web.MsgBoxButton("OK"));

            //Aviso("É necessário colocar o Código CID para executar a busca!");
        }


        protected void btnProcurar2_Click(object sender, System.EventArgs e)
        {

            lbl_Procura.Text = "2";

            if (txtCID2.Text.Trim() != string.Empty)
            {
                ddlCID.ClearSelection();

                while (ddlCID.Items.Count > 1)
                    ddlCID.Items.RemoveAt(0);

                DataSet ds = new CID().Get("CodigoCID='" + txtCID2.Text.Trim() + "' OR Descricao LIKE '%" + txtCID2.Text.Trim() + "%' ORDER BY Descricao DESC");

                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Select())
                        ddlCID.Items.Insert(0, new ListItem(row["Descricao"].ToString(), row["IdCID"].ToString()));
                    txtCID2.Text = ddlCID.Items[ddlCID.SelectedIndex].ToString().Trim();
                    lbl_Id2.Text = ddlCID.SelectedValue.ToString().Trim();
                }
                else
                    MsgBox1.Show("Acidente", "O código '" + txtCID2.Text + "' não foi encontrado! Por favor, tente novamente!", null,
                                   new EO.Web.MsgBoxButton("OK"));


                //Aviso("O código '" + txtCID.Text + "' não foi encontrado! Por favor, tente novamente!");
            }
            else

                MsgBox1.Show("Acidente", "É necessário colocar o Código CID para executar a busca!", null,
                               new EO.Web.MsgBoxButton("OK"));

            //Aviso("É necessário colocar o Código CID para executar a busca!");
        }

        protected void btnProcurar3_Click(object sender, System.EventArgs e)
        {

            lbl_Procura.Text = "3";

            if (txtCID3.Text.Trim() != string.Empty)
            {
                ddlCID.ClearSelection();

                while (ddlCID.Items.Count > 1)
                    ddlCID.Items.RemoveAt(0);

                DataSet ds = new CID().Get("CodigoCID='" + txtCID3.Text.Trim() + "' OR Descricao LIKE '%" + txtCID3.Text.Trim() + "%' ORDER BY Descricao DESC");

                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Select())
                        ddlCID.Items.Insert(0, new ListItem(row["Descricao"].ToString(), row["IdCID"].ToString()));
                    txtCID3.Text = ddlCID.Items[ddlCID.SelectedIndex].ToString().Trim();
                    lbl_Id3.Text = ddlCID.SelectedValue.ToString().Trim();
                }
                else
                    MsgBox1.Show("Acidente", "O código '" + txtCID3.Text + "' não foi encontrado! Por favor, tente novamente!", null,
                                   new EO.Web.MsgBoxButton("OK"));


                //Aviso("O código '" + txtCID.Text + "' não foi encontrado! Por favor, tente novamente!");
            }
            else

                MsgBox1.Show("Acidente", "É necessário colocar o Código CID para executar a busca!", null,
                               new EO.Web.MsgBoxButton("OK"));

            //Aviso("É necessário colocar o Código CID para executar a busca!");
        }

        protected void btnProcurar4_Click(object sender, System.EventArgs e)
        {

            lbl_Procura.Text = "4";

            if (txtCID4.Text.Trim() != string.Empty)
            {
                ddlCID.ClearSelection();

                while (ddlCID.Items.Count > 1)
                    ddlCID.Items.RemoveAt(0);

                DataSet ds = new CID().Get("CodigoCID='" + txtCID4.Text.Trim() + "' OR Descricao LIKE '%" + txtCID4.Text.Trim() + "%' ORDER BY Descricao DESC");

                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Select())
                        ddlCID.Items.Insert(0, new ListItem(row["Descricao"].ToString(), row["IdCID"].ToString()));
                    txtCID4.Text = ddlCID.Items[ddlCID.SelectedIndex].ToString().Trim();
                    lbl_Id4.Text = ddlCID.SelectedValue.ToString().Trim();
                }
                else
                    MsgBox1.Show("Acidente", "O código '" + txtCID4.Text + "' não foi encontrado! Por favor, tente novamente!", null,
                                   new EO.Web.MsgBoxButton("OK"));


                //Aviso("O código '" + txtCID.Text + "' não foi encontrado! Por favor, tente novamente!");
            }
            else

                MsgBox1.Show("Acidente", "É necessário colocar o Código CID para executar a busca!", null,
                               new EO.Web.MsgBoxButton("OK"));

            //Aviso("É necessário colocar o Código CID para executar a busca!");
        }



		private void rblAfastamento_SelectedIndexChanged(object sender, EventArgs e)
		{
            //if (rblAfastamento.SelectedValue == "1")
            //    uwtAcidentes.Tabs.GetTab(5).Enabled = true;
            //else if (rblAfastamento.SelectedValue == "0")
            //    uwtAcidentes.Tabs.GetTab(5).Enabled = false;
		}

		private void rblCAT_SelectedIndexChanged(object sender, EventArgs e)
		{
            //if (rblCAT.SelectedValue == "1")
            //    uwtAcidentes.Tabs.GetTab(4).Enabled = true;
            //else if (rblCAT.SelectedValue == "0")
            //    uwtAcidentes.Tabs.GetTab(4).Enabled = false;
		}

		private void rblRegPol_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (rblRegPol.SelectedItem.Value == "1")
			{
				Color forecolor = Color.FromName("#44926D");
				Color bordercolor = Color.FromName("#7CC5A1");
				Color backcolor = Color.FromName("#FCFEFD");
				lblBO.ForeColor = forecolor;
				txtBO.Enabled = true;
				txtBO.BackColor = backcolor;
				txtBO.BorderColor = bordercolor;
				StringBuilder st = new StringBuilder();

				st.Append("document.frmCadAcidente.uwtAcidentes__ctl4_tabuwtCAT__ctl0_tabtxtBO.focus();");

                this.ClientScript.RegisterStartupScript(this.GetType(), "Focus", st.ToString(), true);
			}
			else
			{
				Color backcolor = Color.FromName("#EBEBEB");
				lblBO.ForeColor = System.Drawing.Color.LightGray;
				txtBO.BackColor = backcolor;
				txtBO.BorderColor = System.Drawing.Color.LightGray;
				txtBO.Text = string.Empty;
				txtBO.Enabled = false;
			}
		}

		private void rblMorte_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (rblMorte.SelectedItem.Value == "1")
			{
				Color forecolor = Color.FromName("#44926D");
				Color bordercolor = Color.FromName("#7CC5A1");
				Color backcolor = Color.FromName("#FCFEFD");
				lblObito.ForeColor = forecolor;
				lblBarra1.ForeColor = forecolor;
				lblBarra2.ForeColor = forecolor;
				txtddo.BorderColor = bordercolor;
				txtddo.BackColor = backcolor;
				txtmmo.BorderColor = bordercolor;
				txtmmo.BackColor = backcolor;
				txtaao.BorderColor = bordercolor;
				txtaao.BackColor = backcolor;
				txtddo.Enabled = true;
				txtmmo.Enabled = true;
				txtaao.Enabled = true;
				StringBuilder st = new StringBuilder();

				st.Append("document.frmCadAcidente.uwtAcidentes__ctl4_tabuwtCAT__ctl0_tabtxtddo.focus();");

                this.ClientScript.RegisterStartupScript(this.GetType(), "Focus", st.ToString(), true);
			}
			else
			{
				Color backcolor = Color.FromName("#EBEBEB");
				lblObito.ForeColor = System.Drawing.Color.LightGray;
				lblBarra1.ForeColor = System.Drawing.Color.LightGray;
				lblBarra2.ForeColor = System.Drawing.Color.LightGray;
				txtddo.BackColor = backcolor;
				txtddo.BorderColor = System.Drawing.Color.LightGray;
				txtmmo.BackColor = backcolor;
				txtmmo.BorderColor = System.Drawing.Color.LightGray;
				txtaao.BackColor = backcolor;
				txtaao.BorderColor = System.Drawing.Color.LightGray;
				txtddo.Text = string.Empty;
				txtmmo.Text = string.Empty;
				txtaao.Text = string.Empty;
				txtddo.Enabled = false;
				txtmmo.Enabled = false;
				txtaao.Enabled = false;
			}
		}

		private void ddlAbsentismo_SelectedIndexChanged(object sender, EventArgs e)
		{
			Afastamento absentismo = new Afastamento(Convert.ToInt32(ddlAbsentismo.SelectedValue));

			PopulaTelaAbsentismo(absentismo);
		}

		private void PopulaTelaAbsentismo(Afastamento absentismo)
		{

            if (absentismo.Id != 0)
            {
                if (absentismo.INSS == true) chk_INSS.Checked = true;
                else chk_INSS.Checked = false;
            }


            if (absentismo.DataInicial != new DateTime() && absentismo.DataInicial != new DateTime(1753, 1, 1))
			{
				txtddi.Text = absentismo.DataInicial.ToString("dd");
				txtmmi.Text = absentismo.DataInicial.ToString("MM");
				txtaai.Text = absentismo.DataInicial.ToString("yyyy");

				ddlHoraIni.ClearSelection();
				ddlMinutoIni.ClearSelection();
				ddlHoraIni.Items.FindByValue(absentismo.DataInicial.ToString("HH")).Selected = true;
				ddlMinutoIni.Items.FindByValue(absentismo.DataInicial.ToString("mm")).Selected = true;
			}
			else
			{
				txtddi.Text = string.Empty;
				txtmmi.Text = string.Empty;
				txtaai.Text = string.Empty;

				ddlHoraIni.ClearSelection();
				ddlMinutoIni.ClearSelection();
			}

			if (absentismo.DataPrevista != new DateTime() && absentismo.DataPrevista != new DateTime(1753, 1, 1))
			{
				txtddp.Text = absentismo.DataPrevista.ToString("dd");
				txtmmp.Text = absentismo.DataPrevista.ToString("MM");
				txtaap.Text = absentismo.DataPrevista.ToString("yyyy");
			}
			else
			{
				txtddp.Text = string.Empty;
				txtmmp.Text = string.Empty;
				txtaap.Text = string.Empty;
			}

			if (absentismo.DataVolta != new DateTime() && absentismo.DataVolta != new DateTime(1753, 1, 1))
			{
				txtddr.Text = absentismo.DataVolta.ToString("dd");
				txtmmr.Text = absentismo.DataVolta.ToString("MM");
				txtaar.Text = absentismo.DataVolta.ToString("yyyy");

				ddlHoraRet.ClearSelection();
				ddlMinutoRet.ClearSelection();
				ddlHoraRet.Items.FindByValue(absentismo.DataVolta.ToString("HH")).Selected = true;
				ddlMinutoRet.Items.FindByValue(absentismo.DataVolta.ToString("mm")).Selected = true;
			}
			else
			{
				txtddr.Text = string.Empty;
				txtmmr.Text = string.Empty;
				txtaar.Text = string.Empty;

				ddlHoraRet.ClearSelection();
				ddlMinutoRet.ClearSelection();
			}

            if (!absentismo.IdCID.Id.Equals(0))
            {
                absentismo.IdCID.Find();

                ddlCID.ClearSelection();

                while (ddlCID.Items.Count > 1)
                    ddlCID.Items.RemoveAt(0);

                ddlCID.Items.Insert(0, new ListItem(absentismo.IdCID.Descricao, absentismo.IdCID.Id.ToString()));
            }
		}

		private void btnAdiciona_Click(object sender, EventArgs e)
		{
			if (acidente.IdCAT.Id != 0)
			{
				foreach (ListItem item in lsbEmpregados.Items)
					if (item.Selected)
					{
						Testemunha testemunha = new Testemunha();
						testemunha.Inicialize();
						testemunha.IdCAT = acidente.IdCAT;
						testemunha.IdEmpregado.Id = Convert.ToInt32(item.Value);
                        testemunha.UsuarioId = Convert.ToInt32(Session["Empregado"].ToString());
						testemunha.UsuarioProcessoRealizado = "Cadastro da Testemunha " + item.Text + " para CAT";
						testemunha.Save();
					}

				//PopulaListBoxTestemunhas();
				//PopulaListBoxEmpregados();
			}
			//else
			//	Aviso("É necessário salvar o Acidente antes de adicionar uma Testemunha no cadastro de CAT!");
		}

        //private void btnRemove_Click(object sender, EventArgs e)
        //{
        //    foreach (ListItem item in lsbTestemunhas.Items)
        //        if (item.Selected)
        //        {
        //            Testemunha testemunha = new Testemunha(Convert.ToInt32(item.Value));
        //            testemunha.UsuarioId = Convert.ToInt32(Session["Empregado"].ToString());
        //            testemunha.UsuarioProcessoRealizado = "Exclusão da Testemunha " + item.Text + " de CAT";
        //            testemunha.Delete();
        //        }

        //    PopulaListBoxTestemunhas();
        //    PopulaListBoxEmpregados();
        //}

		private void btnNovaTest_Click(object sender, EventArgs e)
		{
			if (acidente.IdCAT.Id != 0)
			{
				StringBuilder st = new StringBuilder();

				st.Append("addItemPop(centerWin('CadNovaTestemunha.aspx?IdEmpregado=" + Request["IdEmpregado"] + "&IdEmpresa=" + Request["IdEmpresa"] +  "&IdUsuario=" + Request["IdUsuario"] + "&IdCAT=" + acidente.IdCAT.Id + "',400,205,\'CadNovaTestemunha\'))");

                this.ClientScript.RegisterStartupScript(this.GetType(), "NovaTestemunha", st.ToString(), true);
			}
			//else
			//	Aviso("É necessário salvar o Acidente antes de adicionar uma Nova Testemunha no cadastro de CAT!");
		}

        protected void tabbtnProcurar_Click(object sender, EventArgs e)
        {

        }

        protected void rd_TipoLocal_SelectedIndexChanged(object sender, EventArgs e)
        {
            

            if (rd_TipoLocal.SelectedValue.ToString().Trim() == "1")
            {
                ddlLocalAcidente.Enabled = true;
                btnAddLocal.Enabled = true;
                rblSetor.Enabled = true;
                lblSetor.Enabled = true;
                lblLocalAcidente.Enabled = true;

                lblSetor.ForeColor = Color.FromName("#44926D");
                lblLocalAcidente.ForeColor = Color.FromName("#44926D");

                lblEndereco.Enabled = false;
                lblNumero2.Enabled = false;
                lblMunicipio.Enabled = false;
                lblUF.Enabled = false;

                lblBairro.Enabled = false;
                lblComplemento.Enabled = false;
                lblCEP.Enabled = false;

                lblEndereco.ForeColor = System.Drawing.Color.LightGray;
                lblNumero2.ForeColor = System.Drawing.Color.LightGray;
                lblMunicipio.ForeColor = System.Drawing.Color.LightGray;
                lblUF.ForeColor = System.Drawing.Color.LightGray;

                lblBairro.ForeColor = System.Drawing.Color.LightGray;
                lblComplemento.ForeColor = System.Drawing.Color.LightGray;
                lblCEP.ForeColor = System.Drawing.Color.LightGray;


                txtEndereco.Enabled = false;
                txtNumero.Enabled = false;
                //txtMunicipio.Enabled = false;
                //txtUF.Enabled = false;
                ddlMunicipio.Enabled = false;
                ddlUF.Enabled = false;

                txtBairro.Enabled = false;
                txtComplemento.Enabled = false;
                txtCEP.Enabled = false;

                txtCodPostal.Enabled = false;
                lblCodPostal.Enabled = false;
                lblCodPostal.ForeColor = System.Drawing.Color.LightGray;
            }
            else if (rd_TipoLocal.SelectedValue.ToString().Trim() == "2")
            {
                txtCodPostal.Enabled = true;
                lblCodPostal.Enabled = true;
                lblCodPostal.ForeColor = Color.FromName("#44926D");
            }
            else
            {

                if (rd_TipoLocal.SelectedValue.ToString().Trim() == "4")  //via pública
                {
                    tabrblSetor.SelectedValue = "3";
                }

                ddlLocalAcidente.Enabled = false;
                btnAddLocal.Enabled = false;
                rblSetor.Enabled = false;
                lblSetor.Enabled = false;
                lblLocalAcidente.Enabled = false;

                lblSetor.ForeColor = System.Drawing.Color.LightGray;
                lblLocalAcidente.ForeColor = System.Drawing.Color.LightGray;

                lblEndereco.Enabled = true;
                lblNumero2.Enabled = true;
                lblMunicipio.Enabled = true;
                lblUF.Enabled = true;

                lblBairro.Enabled = true;
                lblComplemento.Enabled = true;
                lblCEP.Enabled = true;

                txtEndereco.Enabled = true;
                txtNumero.Enabled = true;
                //txtMunicipio.Enabled = true;
                //txtUF.Enabled = true;
                ddlMunicipio.Enabled = true;
                ddlUF.Enabled = true;

                txtBairro.Enabled = true;
                txtComplemento.Enabled = true;
                txtCEP.Enabled = true;

                lblEndereco.ForeColor = Color.FromName("#44926D");
                lblNumero2.ForeColor = Color.FromName("#44926D");
                lblMunicipio.ForeColor = Color.FromName("#44926D");
                lblUF.ForeColor = Color.FromName("#44926D");

                lblBairro.ForeColor = Color.FromName("#44926D");
                lblComplemento.ForeColor = Color.FromName("#44926D");
                lblCEP.ForeColor = Color.FromName("#44926D");

                txtCodPostal.Enabled = false;
                lblCodPostal.Enabled = false;
                lblCodPostal.ForeColor = System.Drawing.Color.LightGray;

            }

        }

        protected void btnProjeto_Click(object sender, EventArgs e)
        {

            if (txt_Arq.Text.Trim() == "")
            {
                return;
            }

            try
            {

                //if (Session["Servidor_Web"].ToString().Trim().ToUpper() == "ILITERA")
                //{
                //    if (txt_Arq.Text.Trim().IndexOf("C:\\DRIVE_I") >= 0)
                //    {
                //        Response.Write("<script>");
                //        Response.Write("window.open('" + txt_Arq.Text.Trim().ToUpper().Replace("C:\\DRIVE_I", "http://ilitera.dyndns.ws:8888/driveI").Replace("\\", "/") + "','mywindow','width=450,height=700,left=100,top=100,screenX=0,screenY=100');");
                //        Response.Write("</script>");

                //    }
                //    else
                //    {
                //        Response.Write("<script>");
                //        Response.Write("window.open('" + txt_Arq.Text.Trim().ToUpper().Replace("I:\\FOTOSDOCSDIGITAIS", "http://ilitera.dyndns.ws:8888/driveI/fotosdocsdigitais").Replace("\\", "/") + "','mywindow','width=450,height=700,left=100,top=100,screenX=0,screenY=100');");
                //        Response.Write("</script>");

                //    }
                //}
                //else
                //    if (txt_Arq.Text.ToString().Trim().IndexOf("C:\\DRIVE_I") >= 0)
                //{
                //    Response.Write("<script>");
                //    //    Response.Write("window.open('" + txt_Arq.Text.Trim().ToUpper().Replace("C:\\DRIVE_I", "http://54.94.157.244/driveI").Replace("\\", "/") + "','mywindow','width=450,height=700,left=100,top=100,screenX=0,screenY=100');");
                //    Response.Write("window.open('" + txt_Arq.Text.Trim().ToUpper().Replace("C:\\DRIVE_I", "https://www.ilitera.net.br/driveI").Replace("\\", "/") + "','mywindow','width=450,height=700,left=100,top=100,screenX=0,screenY=100');");

                //    Response.Write("</script>");
                //}
                //else
                //{
                //    Response.Write("<script>");
                //    //Response.Write("window.open('" + txt_Arq.Text.Trim().ToUpper().Replace("I:\\FOTOSDOCSDIGITAIS", "http://54.94.157.244/driveI/fotosdocsdigitais").Replace("\\", "/") + "','mywindow','width=450,height=700,left=100,top=100,screenX=0,screenY=100');");
                //    Response.Write("window.open('" + txt_Arq.Text.Trim().ToUpper().Replace("I:\\FOTOSDOCSDIGITAIS", "https://www.ilitera.net.br/driveI/fotosdocsdigitais").Replace("\\", "/") + "','mywindow','width=450,height=700,left=100,top=100,screenX=0,screenY=100');");
                //    Response.Write("</script>");
                //}


                string xArq = txt_Arq.Text.ToString().Trim().Replace("C:", "I:");


                System.Net.WebClient client = new System.Net.WebClient();
                Byte[] buffer = client.DownloadData(xArq);
                Response.ContentType = "application/pdf";
                Response.AddHeader("Content-Disposition", "attachment; filename=" + xArq);
                Response.AddHeader("content-length", buffer.Length.ToString());
                Response.BinaryWrite(buffer);
                Response.End();


            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alerta", String.Format("alert('{0}');", ex.Message), true);
            }

        }



        protected void ddlUF_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (ddlUF.SelectedIndex >= 0)
                PopulaDDLMunicipio(ddlUF.SelectedValue.ToString().Trim());
            else
                ddlMunicipio.Items.Clear();

        }

        protected void tabddlLocalAcidente_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void chk_UltDia_CheckedChanged(object sender, EventArgs e)
        {
            txtUltDia.Enabled = chk_UltDia.Checked;
            txtUltMes.Enabled = chk_UltDia.Checked;
            txtUltAno.Enabled = chk_UltDia.Checked;
        }

        protected void tabddlCID_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (ddlCID.SelectedIndex < 0) return;

            if (lbl_Procura.Text == "1")
            {
                txtCID.Text = ddlCID.Items[ddlCID.SelectedIndex].ToString().Trim();
                lbl_Id1.Text = ddlCID.SelectedValue.ToString().Trim();
            }
            else if (lbl_Procura.Text == "2")
            {
                txtCID2.Text = ddlCID.Items[ddlCID.SelectedIndex].ToString().Trim();
                lbl_Id2.Text = ddlCID.SelectedValue.ToString().Trim();
            }
            else if (lbl_Procura.Text == "3")
            {
                txtCID3.Text = ddlCID.Items[ddlCID.SelectedIndex].ToString().Trim();
                lbl_Id3.Text = ddlCID.SelectedValue.ToString().Trim();
            }
            else if (lbl_Procura.Text == "4")
            {
                txtCID4.Text = ddlCID.Items[ddlCID.SelectedIndex].ToString().Trim();
                lbl_Id4.Text = ddlCID.SelectedValue.ToString().Trim();
            }

        }
	}
}
