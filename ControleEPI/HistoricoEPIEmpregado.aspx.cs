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
using Ilitera.Opsa.Data;

namespace Ilitera.Net.ControleEPI
{
    /// <summary>
	/// Summary description for ListagemEPI.
	/// </summary>
	public partial class HistoricoEPIEmpregado : WebPageController
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
            InicializaWebPageObjects();
            string xUsuario = Session["usuarioLogado"].ToString();

            if (!IsPostBack)
				PopulaDropDownEmpregado();
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
			this.DGridEPIAconselhado.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DGridEPIAconselhado_PageIndexChanged);
			this.DGridEPIemUtilizacao.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DGridEPIemUtilizacao_PageIndexChanged);
			this.DGridEPIemUtilizacao.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.DGridEPIemUtilizacao_ItemDataBound);

		}
		#endregion

		private void PopulaDropDownEmpregado()
		{
			ddlEmpregado.DataSource = new Empregado().Get("nID_EMPR=" + Convert.ToInt32(Request["IdEmpresa"]) +" ORDER BY tNO_EMPG");
			ddlEmpregado.DataValueField= "nID_EMPREGADO";
			ddlEmpregado.DataTextField = "tNO_EMPG";
			ddlEmpregado.DataBind();
			ddlEmpregado.Items.Insert(0, new ListItem("Selecione o Empregado...","0"));
		}

		protected void ddlEPI_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (Convert.ToInt32(ddlEmpregado.SelectedItem.Value) != 0)
			{
				PopulaGrid(DGridEPIAconselhado, GeraArrayListEPIAconselhado(Convert.ToInt32(ddlEmpregado.SelectedItem.Value)), lblTotRegistrosA);
				PopulaGrid(DGridEPIemUtilizacao, GeraDataSetEPIUtilizacao(Convert.ToInt32(ddlEmpregado.SelectedItem.Value)), lblTotRegistrosU);
				DGridEPIAconselhado.Visible = true;
				DGridEPIemUtilizacao.Visible = true;
			}
			else
			{
				lblTotRegistrosA.Text = "";
				DGridEPIAconselhado.Visible = false;
				lblTotRegistrosU.Text = "";
				DGridEPIemUtilizacao.Visible = false;
			}
		}

		private void DGridEPIAconselhado_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DGridEPIAconselhado.CurrentPageIndex = e.NewPageIndex;
			PopulaGrid(DGridEPIAconselhado, GeraArrayListEPIAconselhado(Convert.ToInt32(ddlEmpregado.SelectedItem.Value)), lblTotRegistrosA);					
			lblTotRegistrosA.Visible = true;
		}

		private ArrayList GeraArrayListEPIAconselhado(int IdEmpregado)
		{
			Empregado empr = new Empregado(IdEmpregado);

			return EmpregadoFuncao.GetListaEpi(empr);
		}

		private DataSet GeraDataSetEPIUtilizacao(int IdEmpregado)
		{
			return new EPIClienteCA().GetEPIemUtilizacao(IdEmpregado);
		}

		private void DGridEPIemUtilizacao_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DGridEPIemUtilizacao.CurrentPageIndex = e.NewPageIndex;
			PopulaGrid(DGridEPIemUtilizacao, GeraDataSetEPIUtilizacao(Convert.ToInt32(ddlEmpregado.SelectedItem.Value)), lblTotRegistrosU);
			lblTotRegistrosU.Visible = true;
		}

		private void DGridEPIemUtilizacao_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			DataSet ds = new DataSet();
			int diastotal = 0;
			DateTime Validade = new DateTime();
			DateTime DataAtual = DateTime.Today;
			
			if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				ds = new EPIClienteCA().GetLastEPIToEmpregado(Convert.ToInt32(e.Item.Cells[0].Text));

				switch (Convert.ToInt32(ds.Tables[0].Rows[0]["Periodicidade"].ToString()))
				{
					case (int)TipoPeriodicidade.Dia:
						diastotal = Convert.ToInt32(ds.Tables[0].Rows[0]["NumPeriodicidade"].ToString());
						break;
					case (int)TipoPeriodicidade.Mês:
						diastotal = 30 * Convert.ToInt32(ds.Tables[0].Rows[0]["NumPeriodicidade"].ToString());
						break;
					case (int)TipoPeriodicidade.Ano:
						diastotal = 365 * Convert.ToInt32(ds.Tables[0].Rows[0]["NumPeriodicidade"].ToString());
						break;
				}
			
				Validade = Convert.ToDateTime(ds.Tables[0].Rows[0]["DataRecebimento"]).AddDays(diastotal);

				if (Validade < DataAtual)
					e.Item.ForeColor = System.Drawing.Color.Red;
				else if (Validade < DataAtual.AddDays(7))
					e.Item.ForeColor = System.Drawing.Color.Orange;
			}
		}
	}
}
