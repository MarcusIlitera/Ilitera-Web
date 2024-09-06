using System;
using System.Collections.Generic;
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
using Ilitera.Common;
using Entities;
using BLL;
using System.Collections;

namespace Ilitera.Net
{
    public partial class ListaAcidentes2 : System.Web.UI.Page
    {
        private Entities.Usuario xUser;

        protected Prestador prestador = new Prestador();
        protected Ilitera.Common.Usuario usuario = new Ilitera.Common.Usuario();
        protected Cliente cliente = new Cliente();
        protected Ilitera.Opsa.Data.Empregado empregado = new Ilitera.Opsa.Data.Empregado();

        protected void InicializaWebPageObjects()
        {
            // base.InicializaWebPageObjects();



            StringBuilder st = new StringBuilder();

            //st.Append("top.window.document.frames.conteudo.window.document.getElementById('txtIdEmpregado').value = '" + empregado.Id + "';");
            st.Append("top.window.document.getElementById('txtIdEmpregado').value = '" + Request["IdEmpregado"] + "';");
            st.Append("top.window.document.getElementById('txtIdUsuario').value = '" + Request["IdUsuario"] + "';");
            st.Append("top.window.document.getElementById('txtIdEmpresa').value = '" + Request["IdEmpresa"] + "';");


            this.ClientScript.RegisterStartupScript(this.GetType(), "IdEmpregado", st.ToString(), true);

        }


        protected void Page_Load(object sender, System.EventArgs e)
        {

            InicializaWebPageObjects();

            string xUsuario = Session["usuarioLogado"].ToString();
            //PreencheLabels("lblEmp", cliente.NomeAbreviado.ToString());

            //PopulaTelaEmpregado();


            if (Request["Tipo"].ToString().Trim() == "1")
            {
                string where = "IdEmpregado=" + Convert.ToInt32(Session["Empregado"].ToString()) + " ORDER BY DataAcidente";
                UltraWebGridExameClinico.DataSource = new Acidente().Get(where);

            }
            else if (Request["Tipo"].ToString().Trim() == "2")
            {

                UltraWebGridExameClinico.DataSource = DSAfastamento();

            }


            UltraWebGridExameClinico.DataBind();

            if (!IsPostBack)
            {


                xUser = (Entities.Usuario)Session["usuarioLogado"];

                if (Request["Tipo"].ToString().Trim() == "2")
                {
                    cmd_Reabertura.Visible = false;

                    lblExCli.Text = "Absenteísmo : " + Session["NomeEmpregado"].ToString();

                    hlkNovo.NavigateUrl = "javascript:void(addItem(centerWin('CadAbsentismo.aspx?IdEmpregado=" + Session["Empregado"].ToString() + "&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + xUser.IdUsuario.ToString() + "&IdAcidente=0', 650, 650, 'CadAcidente'), 'Acidente'))";

                }
                else if (Request["Tipo"].ToString().Trim() == "1")
                {

                    cmd_Reabertura.Visible = true;

                    if (UltraWebGridExameClinico.Items.Count > 0)
                    {
                        cmd_Reabertura.Enabled = true;
                    }
                    else
                    {
                        cmd_Reabertura.Enabled = false;
                    }

                    lblExCli.Text = "Acidentes : " + Session["NomeEmpregado"].ToString();

                    hlkNovo.NavigateUrl = "javascript:void(addItem(centerWin('CadAcidente.aspx?IdEmpregado=" + Session["Empregado"].ToString() + "&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + xUser.IdUsuario.ToString() + "&IdAcidente=0', 1150, 980, 'CadAcidente'), 'Acidente'))";

                }


                Ilitera.Opsa.Data.Empregado rEmpregado = new Ilitera.Opsa.Data.Empregado();
                rEmpregado.Find(Convert.ToInt32(Session["Empregado"].ToString()));

                if (rEmpregado.Id != 0)
                {
                    if (rEmpregado.tCOD_EMPR.Trim() == "")
                    {
                        hlkNovo.Visible = false;
                        MsgBox1.Show("Ilitera.Net", "Colaborador não tem matrícula preenchida, não será permitida criação de Acidente!", null,
                        new EO.Web.MsgBoxButton("OK"));
                        return;

                    }
                }


            }

            if (UltraWebGridExameClinico.Items.Count > 0)
            {
                cmd_Reabertura.Enabled = true;
            }
            else
            {
                cmd_Reabertura.Enabled = false;
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


        private DataSet DSAfastamento()
        {
            DataSet dsExames = new DataSet();
            DataTable table = new DataTable("Result");
            table.Columns.Add("IdAcidente", typeof(string));
            table.Columns.Add("DataAcidente", typeof(string));
            table.Columns.Add("Descricao", typeof(string));
            table.Columns.Add("AgenteCausador", typeof(string));
            table.Columns.Add("Reabertura", typeof(Boolean));
            dsExames.Tables.Add(table);
            DataRow newRow;
            
            if (prestador.Id == 0)
            {
                Entities.Usuario user = (Entities.Usuario)Session["usuarioLogado"];

                prestador.Find(" IdPessoa = " + user.IdPessoa.ToString());
            }


            string where = "IdEmpregado=" + Convert.ToInt32(Session["Empregado"].ToString()) + " ORDER BY DataInicial";
            ArrayList alExames = new Afastamento().Find(where);


            //ArrayList alExames = new Complementar().Find("IdEmpregado=" + Request["IdEmpregado"].ToString() + " ORDER BY DataExame DESC");

            //ArrayList alExames = new ClinicoNaoOcupacional().Find("IdEmpregado=" + Request["IdEmpregado"].ToString() + " ORDER BY DataExame DESC");
            DataSet dsResponsavel = new Responsavel().Get("IdPrestador=" + prestador.Id
                + " AND IndResponsavelPapel=" + (int)ResponsavelPapel.ASOPCIBranco);

            foreach (Afastamento exameNO in alExames)
            {

                newRow = dsExames.Tables[0].NewRow();
                newRow["IdAcidente"] = exameNO.Id.ToString();
                newRow["DataAcidente"] = exameNO.DataInicial.ToString("dd-MM-yyyy");

                if (exameNO.IndTipoAfastamento == 1) newRow["Descricao"] = "Ocupacional";
                else newRow["Descricao"] = "Assistencial";

                newRow["AgenteCausador"] = "";
                newRow["Reabertura"] = false;

                //if (!cliente.ContrataPCMSO.Equals((int)TipoPcmsoContratada.NaoContratada))
                //{
                //    if (exameNO.IndResultado == (int)ResultadoExame.Normal || exameNO.IndResultado == (int)ResultadoExame.Alterado)
                //    {
                //        newRow["Adendo"] = AdendoScript(exameNO);
                //        newRow["ASO"] = ASOScript(exameNO);
                //    }
                //    else if (dsResponsavel.Tables[0].Rows.Count > 0)
                //        newRow["ASO"] = ASOScript(exameNO);

                //    newRow["PCI"] = PCIScript(exameNO);
                //}

                dsExames.Tables[0].Rows.Add(newRow);
            }

            if (alExames.Count > 0)
                lblTotRegistros.Text = "Total de Registros: <b>" + alExames.Count + "</b>";
            else
                lblTotRegistros.Text = "Nenhum registro encontrado";

            return dsExames;
        }



        protected void cmd_Voltar_Click(object sender, EventArgs e)
        {



            if (Session["Retorno"].ToString() == "1")
            {
                if (Request["Tela"].ToString() == "1") Response.Redirect("~/VisualizarDadosEmpregado_Novo2.aspx");
                else Response.Redirect("~/ListaEmpregados.aspx");
            }
            else
            {
                Entities.Usuario user = (Entities.Usuario)Session["usuarioLogado"];
                Response.Redirect("DadosEmpregado_Lista.aspx?IdEmpregado=" + Session["Empregado"].ToString() + "&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString() + "&Tipo=6");
            }

        }

        protected void btnFichaCompleta_Click(object sender, EventArgs e)
        {

        }



        protected void UltraWebGridExameClinico_ItemCommand(object sender, EO.Web.GridCommandEventArgs e)
        {

            xUser = (Entities.Usuario)Session["usuarioLogado"];

            StringBuilder st = new StringBuilder();

            if (Request["Tipo"].ToString().Trim() == "2")
            {
                st.AppendFormat("javascript:void(addItem(centerWin('CadAbsentismo.aspx?IdEmpregado=" + Session["Empregado"].ToString() + "&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + xUser.IdUsuario.ToString() + "&IdAfastamento=" + e.Item.Key.ToString() + "', 650, 650, 'CadAcidente'), 'Acidente'))");
            }
            else
            {
                st.AppendFormat("javascript:void(addItem(centerWin('CadAcidente.aspx?IdEmpregado=" + Session["Empregado"].ToString() + "&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + xUser.IdUsuario.ToString() + "&IdAcidente=" + e.Item.Key.ToString() + "', 1150, 980, 'CadAcidente'), 'Acidente'))");
            }

            ScriptManager scriptManager = ScriptManager.GetCurrent(Page);
            ScriptManager.RegisterClientScriptBlock(
           this,
           this.GetType(),
           "CadAcidente",
           st.ToString(),
           true);

        }

        protected void cmd_Reabertura_Click(object sender, EventArgs e)
        {
            lbl_Reabertura.Visible = true;
            txt_Reabertura.Visible = true;
            txt_Reabertura.Text = "";
            cmd_Reabrir.Visible = true;
        }

        protected void cmd_Reabrir_Click(object sender, EventArgs e)
        {

            txt_Reabertura.Text = txt_Reabertura.Text.Trim();

            //checar se campo recibo tem 23 posições
            if (txt_Reabertura.Text.Length != 23)
            {
             //   MsgBox1.Show("Ilitera.Net", "Formato inválido do recibo! (Tamanho)", null,
             //   new EO.Web.MsgBoxButton("OK"));
                             
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alerta", String.Format("alert('{0}');", "Formato inválido do recibo! (Tamanho)"), true);

                return;
            }


            if (txt_Reabertura.Text.Substring(1, 1) != "." || txt_Reabertura.Text.Substring(3, 1) != ".")
            {
                //   MsgBox1.Show("Ilitera.Net", "Formato inválido do recibo! (Pontuação)", null,
                //  new EO.Web.MsgBoxButton("OK"));

                ScriptManager.RegisterStartupScript(this, this.GetType(), "alerta", String.Format("alert('{0}');", "Formato inválido do recibo! (Pontuação)"), true);

                return;
            }


            //procurar recibo em afastamentos da empresa selecionada
            Cliente cliente;
            cliente = new Cliente(System.Convert.ToInt32(Session["Empresa"].ToString()));


            Ilitera.Data.eSocial xBusca = new Ilitera.Data.eSocial();
            Int32 zAcidente = xBusca.Buscar_Acidente_Reabertura(cliente.Id, txt_Reabertura.Text);

            if ( zAcidente == 0)
            {

                ScriptManager.RegisterStartupScript(this, this.GetType(), "alerta", String.Format("alert('{0}');", "Recibo para acidente nesta empresa/obra não localizado."), true);

                return;

            }


            //criar registro novo de acidente com reabertura
            Acidente xAcidente = new Acidente(zAcidente);

            if ( xAcidente.Id == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alerta", String.Format("alert('{0}');", "Erro na criação da Reabertura de CAT."), true);

                return;

            }


            Acidente xReabertura = new Acidente();
            xReabertura = (Acidente)xAcidente.Clone();


            xReabertura.Reabertura = true;
            xReabertura.nrRecCatOrig = txt_Reabertura.Text;
            xReabertura.Id = 0;
            xReabertura.Save();

            xAcidente.IdCAT.Find();

            if (xAcidente.IdCAT.Id != 0)
            {
                CAT xCat = new CAT(xAcidente.IdCAT.Id);

                if ( xCat.Id != 0)
                {
                    CAT xCatReabertura = new CAT();
                    xCatReabertura = (CAT)xCat.Clone();
                    xCatReabertura.Id = 0;
                    xCatReabertura.Save();

                    xReabertura.IdCAT = xCatReabertura;
                    xReabertura.Save();
                }
            }


            ScriptManager.RegisterStartupScript(this, this.GetType(), "alerta", String.Format("alert('{0}');", "Reabertura de CAT salva."), true);


            object xSender = new object();
            System.EventArgs xe = new System.EventArgs();

            Page_Load(xSender, xe);


        }

        protected void MsgBox1_ButtonClick(object sender, System.Web.UI.WebControls.CommandEventArgs e)
        {

        }







    }

}
