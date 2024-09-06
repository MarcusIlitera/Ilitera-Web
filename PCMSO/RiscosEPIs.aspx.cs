using System;
using System.Web;
using System.Web.SessionState;
using System.Web.UI.WebControls;
using Ilitera.Opsa.Data;
using System.Text;
using System.Data;
using System.Collections;
//using MestraNET;

namespace Ilitera.Net
{
    public partial class RiscosEPIs : System.Web.UI.Page
    {
        protected Empregado empregado = new Empregado();

        protected override void OnPreInit(EventArgs e)
        {
            //InicializaWebPageObjects();
            base.OnPreInit(e);
        }

		protected void Page_Load(object sender, System.EventArgs e)
		{
            if (!IsPostBack)
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

                PopulaGrids();

            }
		}

		private void PopulaGrids()
		{
            gridRiscos.DataSource = GeraDataSetRA();
            gridRiscos.DataBind();
            

            gridEPI.DataSource = GeraDataSetEPI();
            gridEPI.DataBind();


		}

		private DataSet GeraDataSetRA()
		{
			DataSet ds = new DataSet();
			DataTable table = new DataTable();
            table.Columns.Add("Id", typeof(Int32));			
			table.Columns.Add("NomeAgenteFull", typeof(string));			
			table.Columns.Add("NomeAgente", typeof(string));
			table.Columns.Add("Medicao", typeof(string));
			ds.Tables.Add(table);
			DataRow newRow;



            Empregado xEmpregado = new Empregado(System.Convert.ToInt32(Session["Empregado"]));

            lbl_Colaborador.Text = "Colaborador : " + xEmpregado.tNO_EMPG;

            EmpregadoFuncao empregadoFuncao = EmpregadoFuncao.GetEmpregadoFuncao(xEmpregado);
			Ghe ghe = empregadoFuncao.GetGheEmpregado(true);            

			ArrayList alAgente = new PPRA().Find("nID_FUNC=" + ghe.Id + " ORDER BY nID_RSC");

			foreach (PPRA agente in alAgente)
			{
                string nomeAgente = agente.GetNomeAgente();
                
                newRow = ds.Tables[0].NewRow();

                newRow["Id"] = agente.Id;
                newRow["NomeAgenteFull"] = nomeAgente;
                if (nomeAgente.Length > 38)
                    newRow["NomeAgente"] = nomeAgente.Substring(0, 36) + "...";
                else
                    newRow["NomeAgente"] = nomeAgente;
				newRow["Medicao"] = agente.GetAvaliacaoQuantitativa();
				
                ds.Tables[0].Rows.Add(newRow);
			}

			return ds;
		}

        private DataSet GeraDataSetEPI()
		{
            DataSet ds = new DataSet();
            DataTable table = new DataTable();
            table.Columns.Add("Id", typeof(Int32));			
            table.Columns.Add("NomeEPIFull", typeof(string));
            table.Columns.Add("NomeEPI", typeof(string));
            ds.Tables.Add(table);
            DataRow newRow;


            Empregado xEmpregado = new Empregado(System.Convert.ToInt32(Session["Empregado"]));
            ArrayList alEPI = EmpregadoFuncao.GetListaEpi(xEmpregado);

            foreach (Epi epi in alEPI)
            {
                string nomeEPI = epi.Descricao;

                newRow = ds.Tables[0].NewRow();

                newRow["Id"] = epi.Id;
                newRow["NomeEPIFull"] = nomeEPI;
                if (nomeEPI.Length > 39)
                    newRow["NomeEPI"] = nomeEPI.Substring(0, 37) + "...";
                else
                    newRow["NomeEPI"] = nomeEPI;

                ds.Tables[0].Rows.Add(newRow);
            }

            return ds;
		}

        protected void cmd_Voltar_Click(object sender, EventArgs e)
        {
            Entities.Usuario user = (Entities.Usuario)Session["usuarioLogado"];
            Response.Redirect("~\\ListaEmpregados.aspx?&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString());
        }
      
   }
}
