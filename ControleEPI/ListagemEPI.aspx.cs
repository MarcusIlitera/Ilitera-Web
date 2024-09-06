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

namespace Ilitera.Net.ControleEPI
{
    /// <summary>
	/// Summary description for ListagemEPI.
	/// </summary>
	public partial class ListagemEPI : WebPageController
	{
        private DataSet ds = new DataSet();
	
        protected void Page_Load(object sender, System.EventArgs e)
        {
            InicializaWebPageObjects();

            Entities.Usuario user = (Entities.Usuario)Session["usuarioLogado"];

            int xIdEmpresa = System.Convert.ToInt32(Session["Empresa"]);
            int xIdUsuario = user.IdUsuario;

            lbl_Id_Empresa.Text = xIdEmpresa.ToString().Trim();
            lbl_Id_Usuario.Text = xIdUsuario.ToString().Trim();


            if(!IsPostBack)
                PopulaDropDownEPI();
        }

        //#region Web Form Designer generated code
        //override protected void OnInit(EventArgs e)
        //{
        //    //
        //    // CODEGEN: This call is required by the ASP.NET Web Form Designer.
        //    //
        //    InitializeComponent();
        //    base.OnInit(e);
        //}

        ///// <summary>
        ///// Required method for Designer support - do not modify
        ///// the contents of this method with the code editor.
        ///// </summary>
        private void InitializeComponent()
        {
            this.DGridUtilizacaoEPI.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DGridUtilizacaoEPI_PageIndexChanged);

        }
        //#endregion

        private void PopulaDropDownEPI()
        {
            //LaudoTecnico laudo = LaudoTecnico.GetUltimoLaudo(System.Convert.ToInt32( lbl_Id_Empresa.Text));

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
            ddlEPI.DataValueField = "Id";
            ddlEPI.DataTextField = "Descricao";
            ddlEPI.DataBind();
            ddlEPI.Items.Insert(0, new ListItem("Selecione o EPI...", "0"));
        }

        private DataSet GeraDataSet()
        {
            int dias = 0;
            DataSet dsReturn = new DataSet();
            DataRow newrow;
            string xPeriodo = "";


            ds = new EPIClienteCA().GetDetalheEPIemUtilizacao(Convert.ToInt32(ddlEPI.SelectedItem.Value), System.Convert.ToInt32(lbl_Id_Empresa.Text), 0, "Cliente"); //System.Convert.ToInt32(Session["Empregado"]), "Cliente");

            DataTable table = new DataTable("Default");
            table.Columns.Add("NomeE", Type.GetType("System.String"));
            table.Columns.Add("NomeF", Type.GetType("System.String"));
            table.Columns.Add("QtdEntregue", Type.GetType("System.String"));
            table.Columns.Add("NumeroCA", Type.GetType("System.String"));
            table.Columns.Add("Periodicidade", Type.GetType("System.String"));
            dsReturn.Tables.Add(table);

            foreach (DataRow dsrow in ds.Tables[0].Select())
            {
                //bool hasNumeroCA = false;
                

                switch (Convert.ToInt32(dsrow["Periodicidade"]))
                {
                    case 0:
                        dias = Convert.ToInt32(dsrow["NumPeriodicidade"]);
                        xPeriodo = Convert.ToInt32(dsrow["NumPeriodicidade"]).ToString() + " Dia(s)";
                        break;
                    case 1:
                        dias = Convert.ToInt32(dsrow["NumPeriodicidade"]) * 31;
                        xPeriodo = Convert.ToInt32(dsrow["NumPeriodicidade"]).ToString() + " Mes(es)";
                        break;
                    case 2:
                        dias = Convert.ToInt32(dsrow["NumPeriodicidade"]) * 365;
                        xPeriodo = Convert.ToInt32(dsrow["NumPeriodicidade"]).ToString() + " Ano(s)";
                        break;
                }

                foreach (DataRow row in dsReturn.Tables[0].Select())
                {
                    if (dsrow["NumeroCA"].ToString() == row["NumeroCA"].ToString())
                    {
                       // hasNumeroCA = true;
                        if (DateTime.Now < Convert.ToDateTime(dsrow["DataRecebimento"]).AddDays(dias * Convert.ToInt32(dsrow["QtdEntregue"])))
                            row["QtdEntregue"] = Convert.ToInt32(row["QtdEntregue"]) + Convert.ToInt32(dsrow["QtdEntregue"]);
                        break;
                    }
                }

                //if ((!hasNumeroCA) && (DateTime.Now < Convert.ToDateTime(dsrow["DataRecebimento"]).AddDays(dias * Convert.ToInt32(dsrow["QtdEntregue"]))))
                //{
                    newrow = dsReturn.Tables[0].NewRow();

                    if (dsrow["NomeE"].ToString().Length > 30)
                        newrow["NomeE"] = dsrow["NomeE"].ToString().Substring(0, 27) + "...";
                    else
                        newrow["NomeE"] = dsrow["NomeE"];
                    if (dsrow["NomeF"].ToString().Length > 30)
                        newrow["NomeF"] = dsrow["NomeF"].ToString().Substring(0, 27) + "...";
                    else
                        newrow["NomeF"] = dsrow["NomeF"];
                    newrow["QtdEntregue"] = dsrow["QtdEntregue"];
                    newrow["NumeroCA"] = dsrow["NumeroCA"];
                    newrow["Periodicidade"] = xPeriodo;

                    dsReturn.Tables[0].Rows.Add(newrow);
                //}
            }

            return dsReturn;
        }

        protected void ddlEPI_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (Convert.ToInt32(ddlEPI.SelectedItem.Value) != 0)
                PopulaGrid(DGridUtilizacaoEPI, GeraDataSet(), lblTotRegistros);
            else
            {
                lblTotRegistros.Text = "";
                DGridUtilizacaoEPI.Visible = false;
            }
        }

        private void DGridUtilizacaoEPI_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
        {
            DGridUtilizacaoEPI.CurrentPageIndex = e.NewPageIndex;
            PopulaGrid(DGridUtilizacaoEPI, GeraDataSet(), lblTotRegistros);
        }

        protected void lblCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/ControleEPI/CadastroEPI.aspx");
        }
	}
}
