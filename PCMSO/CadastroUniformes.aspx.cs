using System;
using System.Data;
using System.Web.SessionState;
using System.Web.UI.WebControls;
using Ilitera.Common;
using Ilitera.Opsa.Data;
using System.Text;


namespace Ilitera.Net.PCMSO
{
    public partial class CadastroUniformes : System.Web.UI.Page
    {

        //private Extintores extintor;
        private DataSet xdS;
        private DataSet xdS2;
		//string tipoCadastro;
		
		protected void Page_Load(object sender, System.EventArgs e)
		{
            InicializaWebPageObjects();
            string xUsuario = Session["usuarioLogado"].ToString();

            if (!IsPostBack)
			{
                lst_Tamanho.Items.Clear();
				btnFechar.Attributes.Add("onClick", "javascript:self.close();");
				btnExcluir.Attributes.Add("onClick", "javascript:return confirm('Você deseja realmente excluir este Uniforme ?');");
				//if (extintor.Id != 0)
                PopulaDropDownEPI();
                if (Request["IdUniforme"] != null && Request["IdUniforme"].ToString() != string.Empty)
					PopulaTelaUniforme();
			}
			else
			{
				if (txtAuxiliar.Value == "atualizaFabricante")
				{
                    //PopulaDDLFabricante();
					txtAuxiliar.Value = string.Empty;
				}
				else if (txtAuxiliar.Value == "atualizaTipo")
				{
                    //PopulaDDLTipoExtintor();
					txtAuxiliar.Value = string.Empty;
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
			this.ID = "CadastroExtintores";

		}
		#endregion

        protected void InicializaWebPageObjects()
        {
            //base.InicializaWebPageObjects();

			if (Request["IdUniforme"] == null || Request["IdUniforme"].ToString() == string.Empty)
			{
                //campos vazios

				lblTitulo.Text = "Cadastro de Uniformes";
				btnExcluir.Visible = false;
				//tipoCadastro = "cadastrado";
			}
			else
			{
				//extintor = new Extintores(Convert.ToInt32(Request["IdExtintores"]));
                //buscar dados do uniforme


                Ilitera.Data.PPRA_EPI xUnif = new Ilitera.Data.PPRA_EPI();

                xdS = xUnif.Retornar_Dados_Uniforme(System.Convert.ToInt32(Request["IdUniforme"].ToString()));
                xdS2 = xUnif.Retornar_EPIs_Uniforme(System.Convert.ToInt32(Request["IdUniforme"].ToString()));
                                

				lblTitulo.Text = "Edição de Uniformes";
				//tipoCadastro = "editado";
			}
		}

		private void PopulaTelaUniforme()
		{
            DataTable dt = xdS.Tables["Result"];

            foreach (DataRow dr in dt.Rows)
            {

                txt_Uniforme.Text = dr["Uniforme"].ToString();
                txt_Obs.Text = dr["Obs"].ToString();
                txt_Medida.Text = dr["Medida"].ToString();
                
                txtIntervalo.Text = dr["Intervalo"].ToString().Trim();

                if (txtIntervalo.Text.Trim() == "0") txtIntervalo.Text = "";

                string xPeriodicidade = dr["IndPeriodicidade"].ToString().Trim();

                if ( xPeriodicidade!="0")
                {
                    cmbPeriodicidade.SelectedIndex = System.Convert.ToInt16(xPeriodicidade);
                }


                string zTamanho = "";
                string Espacos = "";
                string zObs = "";

                zTamanho = dr["Valor"].ToString().Trim();
                zObs = dr["Obs_Medida"].ToString().Trim();

                if (zTamanho != "")
                {

                    for (int zcont = zTamanho.Length; zcont < 20; zcont++)
                    {
                        Espacos = Espacos + ".";
                    }

                    zTamanho = zTamanho + Espacos;

                    lst_Tamanho.Items.Add(zTamanho + "  " + zObs);

                }

            }



            DataTable dt2 = xdS2.Tables["Result"];

            lst_EPI_ID.Items.Clear();
            lst_EPI.Items.Clear();

            foreach (DataRow dr in dt2.Rows)
            {

                lst_EPI_ID.Items.Add( dr["IdEPI"].ToString() );
                lst_EPI.Items.Add(dr["EPI"].ToString()); 
            }


		}



        private void PopulaDropDownEPI()
        {
//            LaudoTecnico laudo = LaudoTecnico.GetUltimoLaudo(System.Convert.ToInt32(Session["Empresa"]));

            DataSet epitotal = new DataSet();
            Ilitera.Data.PPRA_EPI xEpis = new Ilitera.Data.PPRA_EPI();

            epitotal = xEpis.Retornar_EPIs_Utilizados_Laudos(System.Convert.ToInt32(Session["Empresa"]));


            //ddlEPI.DataSource = laudo.GetEPI();
            lst_EPI.DataSource = epitotal;

            lst_EPI.DataValueField = "Id";
            lst_EPI.DataTextField = "Descricao";
            lst_EPI.DataBind();
            lst_EPI.Items.Insert(0, new ListItem("Selecione o EPI...", "0"));
        }


		protected void btnGravar_Click(object sender, System.EventArgs e)
		{
			try
			{
                int xCod = 0;

                string xErro = "";

				StringBuilder st = new StringBuilder();

                Ilitera.Data.PPRA_EPI xUnif = new Ilitera.Data.PPRA_EPI();

                if (txtIntervalo.Text.Trim() == "")
                {
                    if (Request["IdUniforme"] != null && Request["IdUniforme"].ToString() != string.Empty)
                    {
                        xUnif.Salvar_Dados_Uniforme(System.Convert.ToInt32(Request["IdUniforme"].ToString()), txt_Uniforme.Text, txt_Obs.Text, txt_Medida.Text,0,0);
                        xCod = System.Convert.ToInt32(Request["IdUniforme"].ToString());
                    }
                    else
                    {
                        xCod = xUnif.Criar_Dados_Uniforme(System.Convert.ToInt32(Session["Empresa"]), txt_Uniforme.Text, txt_Obs.Text, txt_Medida.Text,0,0);
                    }
                }
                else
                {
                    string Validar;
                    bool isNumerical;
                    int myInt;
                    
                 
                    Validar = txtIntervalo.Text.Trim();
                    isNumerical = int.TryParse(Validar, out myInt);
                    if (isNumerical == false)
                    {
                        xErro = "Intervalo deve ser numérico.";
                    }
                    else
                    {
                        if ( System.Convert.ToInt16(txtIntervalo.Text) < 0 )
                        {
                            xErro = "Intervalo deve ser numérico maior que zero.";
                        }
                    }

                    if (xErro == "")
                    {
                        if (Request["IdUniforme"] != null && Request["IdUniforme"].ToString() != string.Empty)
                        {
                            xUnif.Salvar_Dados_Uniforme(System.Convert.ToInt32(Request["IdUniforme"].ToString()), txt_Uniforme.Text, txt_Obs.Text, txt_Medida.Text, System.Convert.ToInt16(txtIntervalo.Text), System.Convert.ToInt16(cmbPeriodicidade.SelectedValue.ToString()));
                            xCod = System.Convert.ToInt32(Request["IdUniforme"].ToString());
                        }
                        else
                        {
                            xCod = xUnif.Criar_Dados_Uniforme(System.Convert.ToInt32(Session["Empresa"]), txt_Uniforme.Text, txt_Obs.Text, txt_Medida.Text, System.Convert.ToInt16(txtIntervalo.Text), System.Convert.ToInt16(cmbPeriodicidade.SelectedValue.ToString()));
                        }
                    }
                }


                if (xErro == "")
                {
                    xUnif.Excluir_Tamanhos_Uniforme(xCod);

                    for (int zCont = 0; zCont <= lst_Tamanho.Items.Count - 1; zCont++)
                    {
                        xUnif.Salvar_Dados_Tamanho(xCod, lst_Tamanho.Items[zCont].ToString().Substring(0, lst_Tamanho.Items[zCont].ToString().IndexOf(".")), lst_Tamanho.Items[zCont].ToString().Substring(22).Trim());
                    }


                    xUnif.Excluir_EPIs_Uniforme(xCod);

                    for (int zCont = 0; zCont <= lst_EPI_ID.Items.Count - 1; zCont++)
                    {
                        xUnif.Salvar_Dados_EPIs(xCod, System.Convert.ToInt32(lst_EPI_ID.Items[zCont].ToString()));
                    }

                    Response.Write("<script>window.opener.document.forms[0].submit(); window.close();</script>");
                }
                else
                {
                    
                    st.Append("window.opener.document.forms[0].submit(); window.alert('" + xErro + " ');window.close();");

                    this.ClientScript.RegisterStartupScript(this.GetType(), "ExclusaoUniforme", st.ToString(), true);
                }                
                
			}
			catch (Exception ex)
			{
                System.Diagnostics.Debug.WriteLine(ex.Message);
                //Aviso(ex.Message);
            }
		}


		protected void btnExcluir_Click(object sender, System.EventArgs e)
		{
			try
			{

                if (Request["IdUniforme"] != null && Request["IdUniforme"].ToString() != string.Empty)
                {
                    Ilitera.Data.PPRA_EPI xUnif = new Ilitera.Data.PPRA_EPI();
                    xUnif.Excluir_Dados_Uniforme(System.Convert.ToInt32(Request["IdUniforme"].ToString()));
                }

				StringBuilder st = new StringBuilder();

                st.Append("window.opener.document.forms[0].submit(); window.alert('O Uniforme foi Excluído com sucesso!');window.close();");

                this.ClientScript.RegisterStartupScript(this.GetType(), "ExclusaoUniforme", st.ToString(), true);
			}
			catch (Exception ex)
			{
                System.Diagnostics.Debug.WriteLine(ex.Message);
                //Aviso(ex.Message);
            }
		}

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            string zTamanho = "";
            string Espacos = "";

            if (txt_Tamanho.Text.Trim() == "")
            {
                StringBuilder st = new StringBuilder();
                st.Append("window.alert('Campo Tamanho deve ser preenchido.');");
                this.ClientScript.RegisterStartupScript(this.GetType(), "Tamanhos", st.ToString(), true);
            }

            zTamanho = txt_Tamanho.Text.Trim();

            for (int zcont = zTamanho.Length; zcont < 20; zcont++)
            {
                Espacos = Espacos + ".";
            }

            zTamanho = zTamanho + Espacos;

            Boolean zLoc = false;

            for (int zCont = 0; zCont < lst_Tamanho.Items.Count; zCont++)
            {
                if (lst_Tamanho.Items[zCont].ToString().Trim() == zTamanho.ToString().Trim())
                {
                    zLoc = true;
                }
            }


            if (zLoc == false)
            {
                lst_Tamanho.Items.Add(zTamanho + "  " + txt_Obs_Tamanho.Text);

                txt_Obs_Tamanho.Text = "";
                txt_Tamanho.Text = "";
            }                

        }

        protected void btnRemove_Click(object sender, EventArgs e)
        {

            if (lst_Tamanho.SelectedIndex < 0)
            {
                StringBuilder st = new StringBuilder();
                st.Append("window.alert('Selecione tamanho a ser retirado.');");
                this.ClientScript.RegisterStartupScript(this.GetType(), "Tamanhos", st.ToString(), true);
            }

            lst_Tamanho.Items.RemoveAt(lst_Tamanho.SelectedIndex);
        }




        protected void cmd_Remove2_Click(object sender, EventArgs e)
        {
            if (lst_EPI.SelectedIndex < 0)
            {
                StringBuilder st = new StringBuilder();
                st.Append("window.alert('Selecione EPI a ser retirado.');");
                this.ClientScript.RegisterStartupScript(this.GetType(), "EPI", st.ToString(), true);
            }

            lst_EPI_ID.Items.RemoveAt(lst_EPI.SelectedIndex);
            lst_EPI.Items.RemoveAt(lst_EPI.SelectedIndex);

        }

        protected void cmd_Add2_Click(object sender, EventArgs e)
        {
            if (lst_EPI.SelectedIndex < 0)
            {
                StringBuilder st = new StringBuilder();
                st.Append("window.alert('Selecione EPI a ser adicionado.');");
                this.ClientScript.RegisterStartupScript(this.GetType(), "EPI", st.ToString(), true);
            }

            Boolean zLoc = false;

            for (int zCont = 0; zCont < lst_EPI_ID.Items.Count; zCont++)
            {
                if (lst_EPI_ID.Items[zCont].ToString().Trim() == lst_EPI.SelectedValue.ToString().Trim())
                {
                    zLoc = true;
                }
            }


            if (zLoc == false)
            {
                if (lst_EPI.SelectedValue.ToString().Trim() != "0")
                {
                    lst_EPI.Items.Add(lst_EPI.Items[lst_EPI.SelectedIndex].ToString().Trim());
                    lst_EPI_ID.Items.Add(lst_EPI.SelectedValue.ToString());
                }
            }


        }

      
	}
}
