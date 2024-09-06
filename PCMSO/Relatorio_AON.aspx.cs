using System;
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
using System.Collections;
using System.Collections.Generic;
using System.IO;

using Entities;
using BLL;


namespace Ilitera.Net.PCMSO
{
    public partial class Relatorio_AON : System.Web.UI.Page
    {

        private string zTipo;
        private string zIdEmpregado;


		protected void Page_Load(object sender, System.EventArgs e)
		{
			InicializaWebPageObjects();
            //PreencheLabels("lblEmp", cliente.NomeAbreviado.ToString());

            string xUsuario = Session["usuarioLogado"].ToString();

            if (!IsPostBack)
			{

                zTipo = Request["Tipo"].ToString().Trim();
                zIdEmpregado = Request["IdEmpregado"].ToString().Trim();


                PopularGrid();

                //try
                //{
                //    string FormKey = this.Page.ToString().Substring(4);

                //    Ilitera.Common.Funcionalidade funcionalidade = new Ilitera.Common.Funcionalidade();
                //    funcionalidade.Find("ClassName='" + FormKey + "'");

                //    if (funcionalidade.Id == 0)
                //        throw new Exception("Formulário não cadastrado - " + FormKey);

                //    Entities.Usuario xUser = (Entities.Usuario)Session["usuarioLogado"];
                //    Ilitera.Common.Usuario.Permissao_Web(xUser.IdUsuario, funcionalidade.Id);
                //}

                //catch (Exception ex)
                //{
                //    Session["Message"] = ex.Message;
                //    Server.Transfer("~/Tratar_Excecao.aspx");
                //    return;
                //}
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
			//StringBuilder st = new StringBuilder();

			//st.Append("top.window.document.frames.conteudo.window.document.getElementById('txtIdEmpregado').value = '" + empregado.Id + "';");
            //st.Append("top.window.document.getElementById('txtIdEmpregado').value = '" + Request["IdEmpregado"] + "';");
            //st.Append("top.window.document.getElementById('txtIdUsuario').value = '" + Request["IdUsuario"] + "';");
            //st.Append("top.window.document.getElementById('txtIdEmpresa').value = '" + Request["IdEmpresa"] + "';");
                            
            //this.ClientScript.RegisterStartupScript(this.GetType(), "IdEmpregado", st.ToString(), true);
            //btnFichaCompleta.Attributes.Add("onClick", "addItem(centerWin('../DadosEmpresa/FichaCompleta.aspx?IdEmpregado=" + Request["IdEmpregado"] + "&IdEmpresa=" + Request["IdEmpresa"] + "&IdUsuario=" + Request["IdUsuario"] + "',560,320,\'FichaCompleta\'),\'Todos\'); Reload();");
                        
		}






        protected void PopularGrid()
        {

            DataSet zDs = new DataSet();
        
           

            Ilitera.Data.Clientes_Funcionarios xClientes_Funcionarios = new Ilitera.Data.Clientes_Funcionarios();

           

            zDs = xClientes_Funcionarios.Gerar_DS_Relatorio_AON( System.Convert.ToInt32( zIdEmpregado), zTipo );

            gridAON.DataSource = zDs;
            gridAON.DataBind();

            return;

        }


        protected void cmd_Voltar_Click(object sender, EventArgs e)
        {

            Entities.Usuario user = (Entities.Usuario)Session["usuarioLogado"];


            if (Session["Retorno"].ToString().Trim() == "91")
                Response.Redirect("DadosEmpregado_Questionario.aspx?IdEmpregado=" + Session["Empregado"].ToString() + "&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString() + "&Tipo=6");
            else
                Response.Redirect("DadosEmpregado_Dashboard.aspx?IdEmpregado=" + Session["Empregado"].ToString() + "&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString() + "&Tipo=6");

            return;

        }


    }

}
