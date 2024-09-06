using System;
using System.Data;
using System.Web.SessionState;
using System.Web.UI.WebControls;
using Ilitera.Common;
using Ilitera.Opsa.Data;
using System.Text;
using System.Web.UI;

using System.Collections;


namespace Ilitera.Net.PCMSO
{
    public partial class ListaVacinas : System.Web.UI.Page
    {

        private Extintores extintor;
		//string tipoCadastro;

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
				PopulaDDLVacina();

                lbl_Periodicidade.Visible = false;
                cmb_Periodicidade.Visible = false;

                txt_Periodo.Visible = false;
                lbl_Periodo.Visible = false;

                lbl_Tipo.Visible = false;
                txtVacina.Visible = false;
                lbl_Tipo.Text = "Adicionar Dose";
                txtVacina.Text = "";
                btnGravar.Visible = false;
                btnExcluir.Visible = false;

                //btnFechar.Attributes.Add("onClick", "javascript:self.close();");
                //btnExcluir.Attributes.Add("onClick", "javascript:return confirm('Você deseja realmente excluir este Equipamento de combate à Incêndio?');");
            }
            //else
            //{
            //	if (txtAuxiliar.Value == "atualizaFabricante")
            //	{
            //		PopulaDDLFabricante();
            //		txtAuxiliar.Value = string.Empty;
            //	}
            //	else if (txtAuxiliar.Value == "atualizaTipo")
            //	{
            //		PopulaDDLTipoExtintor();
            //		txtAuxiliar.Value = string.Empty;
            //	}
            //}
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

        protected  void InicializaWebPageObjects()
        {
            //base.InicializaWebPageObjects();

			//if (Request["IdExtintores"] == null || Request["IdExtintores"].ToString() == string.Empty)
			//{
   //             Cliente xCliente = new Cliente();
   //             xCliente.Find(System.Convert.ToInt32(Session["Empresa"].ToString()));

			//	extintor = new Extintores();
			//	extintor.Inicialize();
			//	extintor.IdCliente = xCliente;

				lblTitulo.Text = "Lista de Vacinas";
			//	btnExcluir.Visible = false;
			//	//tipoCadastro = "cadastrado";
			//}
			//else
			//{
			//	extintor = new Extintores(Convert.ToInt32(Request["IdExtintores"]));
			//	lblTitulo.Text = "Edição do Equipamento de Combate à Incêndio";
			//	//tipoCadastro = "editado";
			//}
		}

		private void PopulaTelaExtintor()
		{

            System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");

		}

        private void PopulaDDLVacina()
        {
            ArrayList nVacina = new Vacina_Tipo().Find(" VacinaTipo is not null order by VacinaTipo ");

            lst_Vacinas.Items.Clear();
            lst_Id_Vacinas.Items.Clear();

            foreach (Vacina_Tipo rVacina in nVacina)
            {                
                lst_Vacinas.Items.Add(rVacina.VacinaTipo.Trim());
                
                lst_Id_Vacinas.Items.Add(rVacina.Id.ToString().Trim());
            }
        }

        private void PopulaDDLSetor()
		{
		}
		
		private void PopulaDDLTipoExtintor()
		{
		}



        protected void btnGravar_Click(object sender, System.EventArgs e)
        {
            if (txtVacina.Text.Trim() == "")
            {
                MsgBox1.Show("Ilitera.Net", "Preencha o campo!", null,
                new EO.Web.MsgBoxButton("OK"));

                return;
            }


            if (lbl_Acao.Text == "2")
            {
                if (cmb_Periodicidade.SelectedIndex < 1)
                {
                    MsgBox1.Show("Ilitera.Net", "Preencha a periodicidade!", null,
                    new EO.Web.MsgBoxButton("OK"));

                    return;
                }

                if (txt_Periodo.Text.Trim() == "")
                {
                    MsgBox1.Show("Ilitera.Net", "Preencha o Período!", null,
                    new EO.Web.MsgBoxButton("OK"));

                    return;
                }

                short number;
                bool result = Int16.TryParse(txt_Periodo.Text.Trim(), out number);
                if (!result)
                {
                    MsgBox1.Show("Ilitera.Net", "Preencha o Período com valor numérico!", null,
                    new EO.Web.MsgBoxButton("OK"));

                    return;
                }
            }




            try
            {


                if (lbl_Tipo.Text == "Adicionar Dose" || lbl_Tipo.Text == "Editar Dose")
                {
                    if (lst_Vacinas.SelectedIndex < 0)
                    {
                        MsgBox1.Show("Ilitera.Net", "Necessário selecionar a vacina!", null,
                        new EO.Web.MsgBoxButton("OK"));

                        return;
                    }

                    if (lbl_Acao.Text == "1")
                    {

                        ArrayList nVacina_Dose = new Vacina_Dose().Find(" IdVacinaTipo = " + lst_Id_Vacinas.Items[lst_Vacinas.SelectedIndex].ToString() + " and Dose = '" + txtVacina.Text.Trim() + "' order by Dose ");

                        if (nVacina_Dose.Count > 0)
                        {
                            MsgBox1.Show("Ilitera.Net", "Dose já existe para essa vacina!", null,
                            new EO.Web.MsgBoxButton("OK"));

                            return;
                        }

                        Vacina_Dose rDose = new Vacina_Dose();
                        rDose.IdVacinaTipo = System.Convert.ToInt32(lst_Id_Vacinas.Items[lst_Vacinas.SelectedIndex].ToString());
                        rDose.Dose = txtVacina.Text.Trim();

                        rDose.Periodicidade = System.Convert.ToInt16(txt_Periodo.Text.Trim());

                        if (cmb_Periodicidade.SelectedIndex == 1)
                            rDose.IndPeriodicidade = 1;
                        else if (cmb_Periodicidade.SelectedIndex == 2)
                            rDose.IndPeriodicidade = 2;
                        else if (cmb_Periodicidade.SelectedIndex == 3)
                            rDose.IndPeriodicidade = 4;


                        rDose.Save();
                    }
                    else
                    {

                        Vacina_Dose rDose = new Vacina_Dose();
                        rDose.Find(" IdVacinaDose = " + lst_Id_Doses.Items[lst_Doses.SelectedIndex].ToString().Trim() + " ");

                        if (rDose.Id == 0) return;

                        rDose.Dose = txtVacina.Text.Trim();
                        rDose.Periodicidade = System.Convert.ToInt16(txt_Periodo.Text.Trim());

                        if (cmb_Periodicidade.SelectedIndex == 1)
                            rDose.IndPeriodicidade = 1;
                        else if (cmb_Periodicidade.SelectedIndex == 2)
                            rDose.IndPeriodicidade = 2;
                        else if (cmb_Periodicidade.SelectedIndex == 3)
                            rDose.IndPeriodicidade = 4;

                        rDose.Save();
                    }

                }
                else
                {

                    ArrayList nVacina = new Vacina_Tipo().Find(" VacinaTipo = '" + txtVacina.Text.Trim() + "' ");

                    if (nVacina.Count > 0)
                    {
                        MsgBox1.Show("Ilitera.Net", "Vacina já existe cadastrada!", null,
                        new EO.Web.MsgBoxButton("OK"));

                        return;
                    }

                    Vacina_Tipo rVacina = new Vacina_Tipo();
                    rVacina.VacinaTipo = txtVacina.Text.Trim();
                    rVacina.Save();
                }




                Session["txtAuxiliar"] = "atualizaVacina";

                StringBuilder st = new StringBuilder();
                st.Append("window.opener.location.reload();");// refreshes main window (normally postback)
                st.Append("self.close();");//closes the pop up
                st.Append("</script>");

                this.ClientScript.RegisterStartupScript(this.GetType(), "GravarPerigo", st.ToString(), true);

                //Response.Write("<script>window.opener.document.forms[0].submit(); window.close();</script>");
            }
            catch (Exception ex)
            {
                MsgBox1.Show("Ilitera.Net", ex.Message, null,
                                new EO.Web.MsgBoxButton("OK"));
            }
        }

		private void PopulaExtintor()
		{
		
		}

		protected void btnExcluir_Click(object sender, System.EventArgs e)
		{

            lbl_Tipo.Visible = false;
            txtVacina.Visible = false;
            lbl_Tipo.Text = "Adicionar Dose";
            txtVacina.Text = "";
            btnGravar.Visible = false;
            btnExcluir.Visible = false;

            lbl_Periodicidade.Visible = false;
            cmb_Periodicidade.Visible = false;

            txt_Periodo.Visible = false;
            lbl_Periodo.Visible = false;

        }

        protected void ddlFabricante_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			
		}

		protected void ddlTipoExtintor_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			
		}

        protected void lst_Vacinas_SelectedIndexChanged(object sender, EventArgs e)
        {

            txtVacina.Text = "";
            lst_Doses.Items.Clear();
            lst_Id_Doses.Items.Clear();

            if ( lst_Vacinas.SelectedIndex >= 0 )
            {

                ArrayList nVacina_Dose = new Vacina_Dose().Find(" IdVacinaTipo = " + lst_Id_Vacinas.Items[lst_Vacinas.SelectedIndex].ToString() +   "  order by Dose ");

                foreach (Vacina_Dose rVacina in nVacina_Dose)
                {
                    lst_Doses.Items.Add(rVacina.Dose.Trim());
                    lst_Id_Doses.Items.Add(rVacina.Id.ToString().Trim());
                }
                
            }

            lbl_Periodicidade.Visible = false;
            cmb_Periodicidade.Visible = false;

            txt_Periodo.Visible = false;
            lbl_Periodo.Visible = false;

            return;

        }

        protected void cmd_Add_Vacina_Click(object sender, EventArgs e)
        {
            lbl_Tipo.Visible = true;
            txtVacina.Visible = true;
            lbl_Tipo.Text = "Adicionar Vacina";
            txtVacina.Text = "";
            btnGravar.Visible = true;
            btnExcluir.Visible = true;
        }

        protected void cmd_Add_Dose_Click(object sender, EventArgs e)
        {
            lbl_Tipo.Visible = true;
            txtVacina.Visible = true;
            lbl_Tipo.Text = "Adicionar Dose";
            txtVacina.Text = "";
            btnGravar.Visible = true;
            btnExcluir.Visible = true;

            lbl_Periodicidade.Visible = true;
            cmb_Periodicidade.Visible = true;

            txt_Periodo.Visible = true;
            lbl_Periodo.Visible = true;

            lbl_Acao.Text = "1";

        }

        protected void cmd_Editar_Dose_Click(object sender, EventArgs e)
        {

            if ( lst_Doses.SelectedIndex < 0)
            {
                return;
            }


            //carregar dados da dose
            Vacina_Dose rDose = new Vacina_Dose();
            rDose.Find(" IdVacinaDose = " + lst_Id_Doses.Items[ lst_Doses.SelectedIndex].ToString().Trim() + " ");

            if (rDose.Id == 0) return;


            lbl_Tipo.Visible = true;
            txtVacina.Visible = true;
            lbl_Tipo.Text = "Editar Dose";
            txtVacina.Text = "";
            btnGravar.Visible = true;
            btnExcluir.Visible = true;

            lbl_Periodicidade.Visible = true;
            cmb_Periodicidade.Visible = true;

            txt_Periodo.Visible = true;
            lbl_Periodo.Visible = true;

            lbl_Acao.Text = "2";
            cmb_Periodicidade.SelectedIndex = 0;


            txtVacina.Text = rDose.Dose.Trim();

            if (rDose.Periodicidade != null) txt_Periodo.Text = rDose.Periodicidade.ToString().Trim() ;
            else txt_Periodo.Text = "0";

            if (rDose.IndPeriodicidade != null )
            {
                if (rDose.IndPeriodicidade == 1) cmb_Periodicidade.SelectedIndex = 1;
                else if (rDose.IndPeriodicidade == 2) cmb_Periodicidade.SelectedIndex = 2;
                else if (rDose.IndPeriodicidade == 4) cmb_Periodicidade.SelectedIndex = 3;
            }

            return;

        }



        protected void lst_Doses_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (lst_Doses.SelectedIndex < 0)
            {
                return;
            }


            //carregar dados da dose
            Vacina_Dose rDose = new Vacina_Dose();
            rDose.Find(" IdVacinaDose = " + lst_Id_Doses.Items[lst_Doses.SelectedIndex].ToString().Trim() + " ");

            if (rDose.Id == 0) return;


            lbl_Tipo.Visible = true;
            txtVacina.Visible = true;
            lbl_Tipo.Text = "Visualizar Dose";
            txtVacina.Text = "";
            btnGravar.Visible = false;
            btnExcluir.Visible = false;

            lbl_Periodicidade.Visible = true;
            cmb_Periodicidade.Visible = true;

            txt_Periodo.Visible = true;
            lbl_Periodo.Visible = true;

            lbl_Acao.Text = "2";
            cmb_Periodicidade.SelectedIndex = 0;


            txtVacina.Text = rDose.Dose.Trim();

            if (rDose.Periodicidade != null) txt_Periodo.Text = rDose.Periodicidade.ToString().Trim();
            else txt_Periodo.Text = "0";

            if (rDose.IndPeriodicidade != null)
            {
                if (rDose.IndPeriodicidade == 1) cmb_Periodicidade.SelectedIndex = 1;
                else if (rDose.IndPeriodicidade == 2) cmb_Periodicidade.SelectedIndex = 2;
                else if (rDose.IndPeriodicidade == 4) cmb_Periodicidade.SelectedIndex = 3;
            }

            return;


        }
    }
}
