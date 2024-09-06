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
using System.IO;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Ilitera.Common;
using Ilitera.PCMSO.Report;
using Ilitera.Opsa.Report;
using Entities;
using BLL;
using System.Collections;
using EO.Web;

namespace Ilitera.Net.ControleCIPA
{
    public partial class CipaRepositorio : System.Web.UI.Page
    {
        protected Prestador prestador = new Prestador();
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


            hlkNovo.Visible = false;
            

            PopulaTelaEmpregado();
            
         

            if (!IsPostBack)
            {
                PopulaDDLCIPA();

               // DataSet dsaux = new Pcmso().Get("IdCliente=" + Session["Empresa"].ToString()
               // + " AND IsFromWeb=0"
               // + " ORDER BY DataPcmso DESC");   
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
            ////base.InicializaWebPageObjects();
            //StringBuilder st = new StringBuilder();

            ////st.Append("top.window.document.frames.conteudo.window.document.getElementById('txtIdEmpregado').value = '" + empregado.Id + "';");
            //st.Append("top.window.document.getElementById('txtIdEmpregado').value = '" + Request["IdEmpregado"] + "';");
            //st.Append("top.window.document.getElementById('txtIdUsuario').value = '" + Request["IdUsuario"] + "';");
            //st.Append("top.window.document.getElementById('txtIdEmpresa').value = '" + Request["IdEmpresa"] + "';");


            //this.ClientScript.RegisterStartupScript(this.GetType(), "IdEmpregado", st.ToString(), true);

        }



        private DataSet DSQuestionario()
        {   
            DataSet zDs = new Ilitera.Opsa.Data.CipaRepositorio().Get("IdCIPA=" + ddlCIPA.SelectedValue.ToString() + " ORDER BY DataHora DESC");


            for (int zCont = 0; zCont < zDs.Tables[0].Rows.Count; zCont++ )
            {
                if (zDs.Tables[0].Rows[zCont][3].ToString().ToUpper().Trim()=="O")
                {
                    zDs.Tables[0].Rows[zCont][3] = "Ordinária";
                }
                else
                {
                    zDs.Tables[0].Rows[zCont][3] = "Extraordinária";
                }
            }

            return zDs;

        }




        private void PopulaTelaEmpregado()
        {
            
            


        }

        protected void cmd_Voltar_Click(object sender, EventArgs e)
        {

            Entities.Usuario user = (Entities.Usuario)Session["usuarioLogado"];
            Response.Redirect("~\\ListaEmpregados.aspx?&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString());

        }

        protected void btnFichaCompleta_Click(object sender, EventArgs e)
        {

        }


        protected void grd_Clinicos_ItemCommand(object sender, EO.Web.GridCommandEventArgs e)
        {
            //Check whether it is from our client side
            //JavaScript call
            string zAcao = "V";


            if (e.CommandName.ToString().Trim() == "3")
                zAcao = "E";  //editar
            else
                zAcao = "V";  //visualizar


            //Session["Empregado"] = e.Item.Key.ToString();
            //Session["NomeEmpregado"] = e.Item.Cells[0].Value.ToString();
           // string zTipo = Request["Tipo"].ToString().Trim();

         
            Response.Redirect("Repositorio.aspx?IdCIPARepositorio=" + e.Item.Key.ToString() + "&IdCIPA=" + ddlCIPA.SelectedValue.ToString() +  "&Acao=" + zAcao);
          

        }


        private void PopulaDDLCIPA()
        {
           
           
            DataSet dsReuniao = new Cipa().GetIdNome("Edital", "IdCliente=" + Session["Empresa"].ToString() + " and Edital is not Null ", "Posse DESC");

            DataRow row;
            DataTable table = new DataTable();
            table.Columns.Add("IdCipa", typeof(string));
            table.Columns.Add("DataPosse", typeof(string));

            if (dsReuniao.Tables[0].Rows.Count > 0)
            {

                row = table.NewRow();
                row["IdCipa"] = 0;
                row["DataPosse"] = "Selecione CIPA...";
                table.Rows.Add(row);

                foreach (DataRow rowReuniao in dsReuniao.Tables[0].Select())
                {
                    row = table.NewRow();
                    row["IdCipa"] = rowReuniao["Id"];
                    row["DataPosse"] = Convert.ToDateTime(rowReuniao["Nome"]).ToString("dd/MM/yyyy");
                    table.Rows.Add(row);
                }

                ddlCIPA.DataSource = table;
                ddlCIPA.DataValueField = "IdCipa";
                ddlCIPA.DataTextField = "DataPosse";
                ddlCIPA.DataBind();

                ddlCIPA.SelectedIndex = 0;

                ddlCIPA.Enabled = true;

                lblStatus.Text = "";

                if (Request["IdCipa"] != null)
                {
                    ddlCIPA.SelectedValue = Request["IdCipa"].ToString().Trim();
                    object xSender = new object();
                    System.EventArgs xE = new System.EventArgs();
                    ddlCIPA_SelectedIndexChanged(xSender, xE);
                }


                //PopulaUltraWebGrid(UltraWebGridReuniaoCipa, GeraDataSet(), lblTotRegistros);
            }
            else
            {
                ddlCIPA.Enabled = false;

                lblStatus.Text = "A empresa " + cliente.NomeCompleto + " não possui CIPA criada no sistema!";
            }
        }

        protected void ddlCIPA_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            //PopulaUltraWebGrid(UltraWebGridReuniaoCipa, GeraDataSet(), lblTotRegistros);

            if (ddlCIPA.SelectedIndex <=0)
            {
                gridExames.DataSource = null;
                gridExames.Items.Clear();

                hlkNovo.Visible = false;

                grd_Clinicos.DataSource = null;
                grd_Clinicos.Items.Clear();


                grd_Representantes.DataSource = null;
                grd_Representantes.Items.Clear();

            }
            else
            {
                grd_Clinicos.DataSource = DSQuestionario();
                grd_Clinicos.DataBind();

                hlkNovo.Visible = true;
                hlkNovo.NavigateUrl = "javascript:void(window.location.href ='Repositorio.aspx?IdCIPARepositorio=0&IdCIPA=" + ddlCIPA.SelectedValue.ToString() + "&Acao=E')";
                

                Ilitera.Data.Clientes_Funcionarios xLista = new Ilitera.Data.Clientes_Funcionarios();

                DataSet zDs = new DataSet();
 
                //carregar cronograma
                zDs = xLista.Gerar_Lista_Cronograma(Convert.ToInt32(ddlCIPA.SelectedValue.ToString()));

                gridExames.DataSource = zDs;
                gridExames.DataBind();

                PopulaGridMembroCipa();

            }

        }


        private void PopulaGridMembroCipa()
        {
            StringBuilder str = new StringBuilder();
            str.Append("IdCipa=" + Convert.ToInt32(ddlCIPA.SelectedValue.ToString()));

            str.Append(" AND IndTitularSuplente <> 0");
                        
            str.Append(" ORDER BY IndGrupoMembro, IndTitularSuplente, Numero");

            DataSet kDs = new MembroCipa().Get(str.ToString());


            string xNome;
            string xCargo;
            int IdMembroCipa;
            string xEstabilidade;

            
            DataSet zRet = new DataSet();

            zRet.Tables.Add(GetDataTable());


            for (int zCont = 0; zCont < kDs.Tables[0].Rows.Count; zCont++)
            {
                System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");

                if (kDs.Tables[0].Rows[zCont]["Estabilidade"].ToString() != null)
                    xEstabilidade = System.Convert.ToDateTime(kDs.Tables[0].Rows[zCont]["Estabilidade"], ptBr).ToString("dd/MM/yyyy", ptBr);
                else
                    xEstabilidade = "-";

                IdMembroCipa = Convert.ToInt32(kDs.Tables[0].Rows[zCont]["IdMembroCipa"].ToString());

                MembroCipa membroCipa = new MembroCipa();
                membroCipa.Find(IdMembroCipa);

                xNome = membroCipa.ToString();
                xCargo = membroCipa.GetNomeCargo();

                DataRow newRow = zRet.Tables[0].NewRow();
                newRow["IdMembroCIPA"] = IdMembroCipa;
                newRow["Nome"] = xNome;
                newRow["Cargo"] = xCargo;
                newRow["Estabilidade"] = xEstabilidade;
                zRet.Tables[0].Rows.Add(newRow);
                
            }
            
            grd_Representantes.DataSource = zRet;
            grd_Representantes.DataBind();

        }


        public static DataTable GetDataTable()
        {
            DataTable table = new DataTable("ResultAdendo");

            table.Columns.Add("IdMembroCIPA", Type.GetType("System.Int32"));
            table.Columns.Add("Nome", Type.GetType("System.String"));
            table.Columns.Add("Cargo", Type.GetType("System.String"));
            table.Columns.Add("Estabilidade", Type.GetType("System.String"));
            return table;
        }

    }
}
