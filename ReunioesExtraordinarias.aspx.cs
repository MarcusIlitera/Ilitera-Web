using Ilitera.Common;
using Ilitera.Opsa.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Ilitera.Net
{
    public partial class ReunioesExtraordinarias : System.Web.UI.Page
    {
        private Cliente cliente = new Cliente();
        private Cipa cipa = new Cipa();
        private Juridica juridica = new Juridica();

        protected void Page_Load(object sender, EventArgs e)
        {
            cliente.Find(Convert.ToInt32(Session["Empresa"]));
            juridica = cliente;
            cipa = cliente.GetGestaoAtual();
            PopulaGrdReunioesExtraordinarias();
        }

        private void PopulaGrdReunioesExtraordinarias()
        {
            DataSet ds = new ReuniaoCipa().Get("IdCipa=" + cipa.Id
                + " AND IdEventoBaseCipa=" + ((int)EventoBase.ReuniaoExtraordinaria).ToString()
                + " ORDER BY DataSolicitacao DESC");

            DataTable dt = new DataTable();
            dt.Columns.Add("IdReuniao");
            dt.Columns.Add("Editar");
            dt.Columns.Add("FraseAcidentes");
            dt.Columns.Add("DataReuniao");
            dt.Columns.Add("Observacao");

            foreach(DataRow row in ds.Tables[0].Rows)
            {
                dt.Rows.Add(row["IdReuniaoCipa"], "Editar", row["FraseAcidentes"], Convert.ToDateTime(row["DataSolicitacao"]).ToString("dd/MM/yyyy"), row["Observacao"]);
            }

            grdReunioesExtraordinarias.DataSource = dt;
        }

        protected void btnNovo_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("void(window.open('AddReuniao.aspx', 'AddReuniao', 'scrollbars=yes, resizable=yes, toolbar=no, addressbar=no, menubar=no, width=850px, height=1100px')); window.close();");

            System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "OpenWindow", sb.ToString(), true);
        }

        protected void grdReunioesExtraordinarias_ItemCommand(object sender, EO.Web.GridCommandEventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("void(window.open('AddReuniao.aspx?i={0}', 'AddReuniao', 'scrollbars=yes, resizable=yes, toolbar=no, addressbar=no, menubar=no, width=850px, height=1100px')); window.close();", e.Item.Key);
            System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "OpenWindow", sb.ToString(), true);
        }
    }
}