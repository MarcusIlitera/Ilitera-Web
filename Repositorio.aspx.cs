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

namespace Ilitera.Net
{
    public partial class Repositorio : System.Web.UI.Page
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



            hlkNovo.NavigateUrl = "javascript:void(window.location.href ='Repositorio_Cadastro.aspx?IdRepositorio=0&IdCliente=" + Session["Empresa"].ToString() + "&Acao=E')";



            if (!IsPostBack)
            {
                grd_Clinicos.DataSource = DSGrid("","",null);
                grd_Clinicos.DataBind();



                //verificar se é apenas consulta - grupo repositório consulta para este usuário
                Entities.Usuario xUser = (Entities.Usuario)Session["usuarioLogado"];

                if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().IndexOf("Prajna") > 0)
                {
                    Ilitera.Data.Clientes_Funcionarios xRep = new Ilitera.Data.Clientes_Funcionarios();

                    if (xRep.Retornar_Acesso_Repositorio_Consulta(xUser.IdUsuario) == false)
                    {
                        grd_Clinicos.Columns[3].Visible = false;
                    }
                    else
                    {
                        grd_Clinicos.Columns[3].Visible = true;
                    }
                }
                else
                {
                    grd_Clinicos.Columns[3].Visible = true;
                }

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



        private DataSet DSGrid(string tipo, string descricao, DateTime[] data)
        {

            Ilitera.Data.PPRA_EPI xRep = new Ilitera.Data.PPRA_EPI();
            DataSet zDs = xRep.Retornar_Repositorio( System.Convert.ToInt32( Session["Empresa"].ToString()), tipo, data, descricao );

            return zDs;

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

         
            Response.Redirect("Repositorio_Cadastro.aspx?IdRepositorio=" + e.Item.Key.ToString() + "&IdCliente=" + Session["Empresa"].ToString() +  "&Acao=" + zAcao);
          

        }

        protected void Busca_Arq_Click(object sender, EventArgs e)
        {
            try
            {
                
                DataSet ds = DSGrid(DropDownList1.SelectedValue.ToUpper(), "", null);
                if (ds.Tables[0].Rows.Count != 0)
                {
                    grd_Clinicos.DataSource = ds;
                    grd_Clinicos.DataBind();
                }
                else
                {
                    throw new Exception("Não há arquivos criados desse tipo");
                }
            }
            catch(Exception ex)
            {
                MsgBox1.Show("Ilitera.Net", ex.Message, null,
                       new EO.Web.MsgBoxButton("OK"));
                grd_Clinicos.DataSource = DSGrid("", "", null);
                grd_Clinicos.DataBind();
            }
            this.LimparPesquisa("arq");
        }

        protected void Busca_Data_Click(object sender, EventArgs e)
        {

            System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");

            try
            {
                DateTime[] data = new DateTime[2];
                if (TextBox_DataFim.Text == "")
                {
                    data[0] = System.Convert.ToDateTime(TextBox_DataInicio.Text, ptBr);
                    data[1] = data[0].AddDays(1);
                    TextBox_DataFim.Text = data[0].ToShortDateString();
                }else if (TextBox_DataInicio.Text == "")
                {
                    data[1] = System.Convert.ToDateTime(TextBox_DataFim.Text, ptBr);
                    TextBox_DataInicio.Text = data[1].ToShortDateString();
                    data[0] = data[1];
                    data[1] = data[1].AddDays(1);
                }
                else
                {
                    data[0] = System.Convert.ToDateTime(TextBox_DataInicio.Text, ptBr);
                    data[1] = System.Convert.ToDateTime(TextBox_DataFim.Text, ptBr).AddDays(1);
                }
                if (data[0] > DateTime.Today)
                {
                    throw new Exception("Data inválida");
                }

                
                DataSet ds = DSGrid("", "", data);
                if (ds.Tables[0].Rows.Count != 0 )
                {
                    grd_Clinicos.DataSource = ds;
                    grd_Clinicos.DataBind();
                }else
                {
                    throw new Exception("Não há dados para o período pesquisado");
                }
            }
            catch (Exception ex)
            {
                MsgBox1.Show("Ilitera.Net", ex.Message, null,
                       new EO.Web.MsgBoxButton("OK"));
                grd_Clinicos.DataSource = DSGrid("", "", null);
                grd_Clinicos.DataBind();
            }
            this.LimparPesquisa("data");
        }

        protected void Button_Descricao_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = DSGrid("", TextBox_Descricao.Text, null);
                if (ds.Tables[0].Rows.Count != 0) {
                    grd_Clinicos.DataSource = ds;
                    grd_Clinicos.DataBind();
                }else
                {
                    throw new Exception("Nenhuma das descrições consultadas contém o texto pesquisado");
                }
            }
            catch(Exception ex)
            {
                MsgBox1.Show("Ilitera.Net", ex.Message, null, new EO.Web.MsgBoxButton("OK"));
                grd_Clinicos.DataSource = DSGrid("", "", null);
                grd_Clinicos.DataBind();
            }
            this.LimparPesquisa("desc");
        }

        protected void Button_VerTodos_Click(object sender, EventArgs e)
        {
            this.LimparPesquisa("toodos");
            grd_Clinicos.DataSource = DSGrid("", "", null);
            grd_Clinicos.DataBind();
        }


        private void LimparPesquisa(string tipo)
        {
            switch (tipo)
            {
                case "arq":
                    TextBox_DataInicio.Text = "";
                    TextBox_DataFim.Text = "";
                    TextBox_Descricao.Text = "";
                    break;
                case "data":
                    DropDownList1.ClearSelection();
                    TextBox_Descricao.Text = "";
                    break;
                case "desc":
                    DropDownList1.ClearSelection();
                    TextBox_DataInicio.Text = "";
                    TextBox_DataFim.Text = "";
                    break;
                default:
                     DropDownList1.ClearSelection();
                    TextBox_DataInicio.Text = "";
                    TextBox_DataFim.Text = "";
                    TextBox_Descricao.Text = "";
                    break;
            }
          

        }
    }
}
