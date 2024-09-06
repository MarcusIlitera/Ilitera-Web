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
using Ilitera.Common;
using Ilitera.Data;
using Ilitera.Opsa.Data;
using System.Text;



namespace Ilitera.Net
{
    /// <summary>
	/// Summary description for ListagemEPI.
	/// </summary>
	public partial class ControleEstoque : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Button lblCancel;
		protected System.Web.UI.WebControls.Label lblError;
		protected System.Web.UI.WebControls.Label lblQtdEstoque;


        protected Prestador prestador = new Prestador();
        protected Ilitera.Common.Usuario usuario = new Ilitera.Common.Usuario();
        protected Cliente cliente = new Cliente();
        protected Ilitera.Opsa.Data.Empregado empregado = new Ilitera.Opsa.Data.Empregado();
	

		protected void Page_Load(object sender, System.EventArgs e)
		{
            //InicializaWebPageObjects();

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

                Entities.Usuario user = (Entities.Usuario)Session["usuarioLogado"];

                int xIdEmpresa = System.Convert.ToInt32(Session["Empresa"]);
                int xIdUsuario = user.IdUsuario;

                lbl_Id_Empresa.Text = xIdEmpresa.ToString().Trim();
                lbl_Id_Usuario.Text = xIdUsuario.ToString().Trim();

				btnGravar.Attributes.Add("onClick", "return VerificaCampoInt();");
				PopulaDropDownEPI();
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
            //this.DGridCAAssociado.ClickCellButton += new Infragistics.WebUI.UltraWebGrid.ClickCellButtonEventHandler(this.DGridCAAssociado_ClickCellButton);
            //this.DGridCAAssociado.PageIndexChanged += new Infragistics.WebUI.UltraWebGrid.PageIndexChangedEventHandler(this.DGridCAAssociado_PageIndexChanged);
            //this.ID = "ControleEstoque";

		}
		#endregion

		private void PopulaDropDownEPI()
		{
            //LaudoTecnico laudo = LaudoTecnico.GetUltimoLaudo( System.Convert.ToInt32( lbl_Id_Empresa.Text));
			
            //DataSet ds = new Ghe().Get("nID_LAUD_TEC =" + laudo.Id + " ORDER BY tNO_FUNC");
            //ArrayList epitotal = new ArrayList();
            //bool Verifica;
            //bool Verifica2;

            //foreach (DataRow ghe in ds.Tables[0].Select())
            //{
            //    ArrayList listGheEpiExistente = new GheEpiExistente().Find("nID_FUNC=" + ghe["nID_FUNC"]);
            //    ArrayList listRiscoEpiExistente = new EpiExistente().FindIn(new PPRA().GetType(), "", true, "nID_FUNC=" + ghe["nID_FUNC"]);
			
            //    foreach (GheEpiExistente gheepi in listGheEpiExistente)
            //    {
            //        gheepi.nID_EPI.Find();
            //        Verifica = true;
				
            //        foreach (Epi epicompare in epitotal)
            //        {
            //            if (Convert.ToInt32(gheepi.nID_EPI.Id) == Convert.ToInt32(epicompare.Id))
            //            {
            //                Verifica = false;
            //                break;
            //            }
            //        }
				
            //        if (Verifica == true)
            //            epitotal.Add(gheepi.nID_EPI);
            //    }

            //    foreach (EpiExistente riscoepi in listRiscoEpiExistente)
            //    {
            //        riscoepi.nID_EPI.Find();
            //        Verifica2 = true;
				
            //        foreach (Epi epicompare in epitotal)
            //        {
            //            if (Convert.ToInt32(riscoepi.nID_EPI.Id) == Convert.ToInt32(epicompare.Id))
            //            {
            //                Verifica2 = false;
            //                break;
            //            }
            //        }
				
            //        if (Verifica2 == true)
            //            epitotal.Add(riscoepi.nID_EPI);
            //    }
            //}

            //epitotal.Sort();

            DataSet epitotal = new DataSet();
            Ilitera.Data.PPRA_EPI xEpis = new Ilitera.Data.PPRA_EPI();

            epitotal = xEpis.Retornar_EPIs_Utilizados_Laudos(System.Convert.ToInt32(lbl_Id_Empresa.Text));

			ddlEPI.DataSource = epitotal;
			ddlEPI.DataValueField= "Id";
			ddlEPI.DataTextField = "Descricao";
			ddlEPI.DataBind();
			ddlEPI.Items.Insert(0, new ListItem("Selecione o EPI...","0"));
		}

		private DataSet dsGridCA()
		{
			DataSet dsReturn = new DataSet();
			DataRow row;
            DataSet ds = new EPIClienteCA().GetEPIClienteCA(Convert.ToInt32(ddlEPI.SelectedItem.Value), System.Convert.ToInt32(lbl_Id_Empresa.Text));

			DataTable table = new DataTable("Default");
			table.Columns.Add("IdCA", Type.GetType("System.String"));
			table.Columns.Add("NumeroCA", Type.GetType("System.String"));
			dsReturn.Tables.Add(table);

			foreach (DataRow rowds in ds.Tables[0].Select())
			{
				row = dsReturn.Tables[0].NewRow();

				row["IdCA"] = rowds["IdCA"];
				row["NumeroCA"] = "CA " + rowds["NumeroCA"].ToString() + " - " + rowds["QtdEstoque"].ToString() + " unidades";

				dsReturn.Tables[0].Rows.Add(row);
			}

			return dsReturn;
		}

		private void PopulaGridCA()
		{
			DataSet ds = dsGridCA();

            DGridCAAssociado.DataSource = ds;
            DGridCAAssociado.DataBind();

            if (ds.Tables[0].Rows.Count > 0)
                lblTotRegistros.Text = "Total de Registros: " + ds.Tables[0].Rows.Count.ToString();
            else
                lblTotRegistros.Text = string.Empty;
		}

		private string QtdTotalEstoque()
		{
			int qtdEstoque = 0;

            DataSet ds = new EPIClienteCA().Get("IdCliente=" + lbl_Id_Empresa.Text
				+" AND IdEPI=" + ddlEPI.SelectedItem.Value);

			foreach (DataRow row in ds.Tables[0].Select())
				qtdEstoque += Convert.ToInt32(row["QtdEstoque"]);

			return qtdEstoque.ToString();
		}

		protected void btnGravar_Click(object sender, System.EventArgs e)
		{
			try
			{
				if (ViewState["IdEPIClienteCA"] == null || ViewState["IdEPIClienteCA"].ToString() == string.Empty)
					throw new Exception("É necessário selecionar um CA para colocar os dados do estoque!");

				EPIClienteCA qtdEpiCA = new EPIClienteCA(Convert.ToInt32(ViewState["IdEPIClienteCA"]));

				qtdEpiCA.QtdEstoque = Convert.ToInt32(txtQtdEstoque.Text);
				qtdEpiCA.QtdEstoqueMax = Convert.ToInt32(txtQtdEstMax.Text);
				qtdEpiCA.QtdEstoqueMin = Convert.ToInt32(txtQtdEstMin.Text);

				qtdEpiCA.UsuarioId = usuario.Id;
				qtdEpiCA.Save();
				lblValorTotalEstoque.Text = QtdTotalEstoque();
				PopulaGridCA();

				
                MsgBox1.Show("Estoque","Os dados foram atualizados com sucesso!", null,
                                new EO.Web.MsgBoxButton("OK"));                
            

				lblValorCA.Text = string.Empty;
				txtQtdEstoque.Text = string.Empty;
				txtQtdEstMax.Text = string.Empty;
				txtQtdEstMin.Text = string.Empty;
				ViewState["IdEPIClienteCA"] = null;
			}
			catch(Exception ex)
			{
                MsgBox1.Show("Estoque", ex.Message, null,
                                new EO.Web.MsgBoxButton("OK"));                

			} 
		}

		protected void ddlEPI_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (ddlEPI.SelectedItem.Value != "0")
				lblValorTotalEstoque.Text = QtdTotalEstoque();
			else
				lblValorTotalEstoque.Text = string.Empty;

            //DGridCAAssociado.DisplayLayout.Pager.CurrentPageIndex = 1;
			PopulaGridCA();
			lblValorCA.Text = string.Empty;
			txtQtdEstoque.Text = string.Empty;
			txtQtdEstMax.Text = string.Empty;
			txtQtdEstMin.Text = string.Empty;
			ViewState["IdEPIClienteCA"] = null;
		}

		//private void DGridCAAssociado_ClickCellButton(object sender, Infragistics.WebUI.UltraWebGrid.CellEventArgs e)
        //protected void DGridCAAssociado_ClickCellButton(object sender, CellEventArgs e)
        //{
        //    EPIClienteCA QtdEpiCA = new EPIClienteCA();
        //    QtdEpiCA.Find("IdCliente=" + lbl_Id_Empresa.Text
        //        +" AND IdEPI=" + ddlEPI.SelectedItem.Value
        //        +" AND IdCA=" + e.Cell.Row.Cells[0].Text);
        //    QtdEpiCA.IdCA.Find();
        //    ViewState["IdEPIClienteCA"] = QtdEpiCA.Id;

        //    lblValorCA.Text = QtdEpiCA.IdCA.NumeroCA.ToString();
        //    txtQtdEstoque.Text = QtdEpiCA.QtdEstoque.ToString();
        //    txtQtdEstMax.Text = QtdEpiCA.QtdEstoqueMax.ToString();
        //    txtQtdEstMin.Text = QtdEpiCA.QtdEstoqueMin.ToString();
        //}

        //private void DGridCAAssociado_PageIndexChanged(object sender, Infragistics.WebUI.UltraWebGrid.PageEventArgs e)
        //{
        //    DGridCAAssociado.DataSource = dsGridCA();
        //    DGridCAAssociado.DisplayLayout.Pager.CurrentPageIndex = e.NewPageIndex;
        //    DGridCAAssociado.DataBind();
        //}


        protected void DGridCAAssociado_ItemCommand(object sender, EO.Web.GridCommandEventArgs e)
        {
            

            //Session["Empregado"] = e.Item.Key.ToString();
            //Session["NomeEmpregado"] = e.Item.Cells[0].Value.ToString();


            EPIClienteCA QtdEpiCA = new EPIClienteCA();
            QtdEpiCA.Find("IdCliente=" + lbl_Id_Empresa.Text
                + " AND IdEPI=" + ddlEPI.SelectedItem.Value
                + " AND IdCA=" + e.Item.Cells[0].Value.ToString());
            QtdEpiCA.IdCA.Find();
            ViewState["IdEPIClienteCA"] = QtdEpiCA.Id;

            lblValorCA.Text = QtdEpiCA.IdCA.NumeroCA.ToString();
            txtQtdEstoque.Text = QtdEpiCA.QtdEstoque.ToString();
            txtQtdEstMax.Text = QtdEpiCA.QtdEstoqueMax.ToString();
            txtQtdEstMin.Text = QtdEpiCA.QtdEstoqueMin.ToString();

        }

	}
}
