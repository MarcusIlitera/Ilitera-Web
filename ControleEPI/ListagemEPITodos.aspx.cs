using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
//using MestraNET;
using Ilitera.Common;
using Ilitera.Data;
using Ilitera.Data.SQLServer;
using Ilitera.Opsa.Data;
using System.Data.SqlClient;
using System.Text;

namespace Ilitera.Net.ControleEPI
{
    /// <summary>
	/// Summary description for ListagemEPI.
	/// </summary>
	public partial class ListagemEPITodos : WebPageController
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
            InicializaWebPageObjects();
            string xUsuario = Session["usuarioLogado"].ToString();

            if (!IsPostBack)
				PopulaGrid(DGridUtilizacaoEPITodos, GeraDataSetEPI("EPITodos", ""), lblTotRegistros);
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
			this.DGridUtilizacaoEPITodos.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGridUtilizacaoEPITodos_ItemCommand);
			this.DGridUtilizacaoEPITodos.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DGridUtilizacaoEPI_PageIndexChanged);

		}
		#endregion

		private void DGridUtilizacaoEPI_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DGridUtilizacaoEPITodos.CurrentPageIndex = e.NewPageIndex;
			PopulaGrid(DGridUtilizacaoEPITodos, GeraDataSetEPI("EPITodos", ""), lblTotRegistros);
		}

		private void DGridUtilizacaoEPITodos_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			if (e.CommandName == "DetalheEPI")
			{
				StringBuilder st = new StringBuilder();

				st.Append("void(addItem(centerWin('DetalheEPIUtilizacao.aspx?IdEPI=" + e.Item.Cells[0].Text + "&IdUsuario=" + Request["IdUsuario"] + "&IdEmpresa=" + Request["IdEmpresa"] + "',510,320,\'DetalheEPIUtilizacao\'),\'Todos\'))");

                this.ClientScript.RegisterStartupScript(this.GetType(), "DetalheEPIUtil", st.ToString(), true);
			}
		}
	}
}
