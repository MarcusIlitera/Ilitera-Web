using Ilitera.Common;
using Ilitera.Opsa.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Ilitera.Net
{
    public partial class MembrosUltimasCipas : System.Web.UI.Page
    {
        private Cliente cliente = new Cliente();
        private Cipa cipa = new Cipa();
        private Juridica juridica = new Juridica();
        protected void Page_Load(object sender, EventArgs e)
        {
            cliente.Find(Convert.ToInt32(Session["Empresa"]));

            PopulaGris();
        }

        private void PopulaGris()
        {
            ArrayList listCipa = cliente.GetUltimas3Cipa();

            if (listCipa.Count > 0)
                PopulaListEmpregadosUltimaCipa((Cipa)listCipa[0]);

            if (listCipa.Count > 1)
                PopulaListEmpregadosPenultimaCipa((Cipa)listCipa[1]);

            if (listCipa.Count > 2)
                PopulaListEmpregadosAntepenultimaCipa((Cipa)listCipa[2]);
        }

        private void PopulaListEmpregadosUltimaCipa(Cipa cipa)
        {
            DataSet ds = cipa.ListaGridTitularesSuplentesCipa((int)GrupoMembro.Empregado);
            lblUltimaCipa.Text += cipa.Posse.ToString("dd-MM-yyyy");
            grdUltimaCipa.DataSource = ds;
        }

        private void PopulaListEmpregadosPenultimaCipa(Cipa cipa)
        {
            DataSet ds = cipa.ListaGridTitularesSuplentesCipa((int)GrupoMembro.Empregado);
            lblPenultimaCipa.Text += cipa.Posse.ToString("dd-MM-yyyy");
            grdPenultimaCipa.DataSource = ds;
        }

        private void PopulaListEmpregadosAntepenultimaCipa(Cipa cipa)
        {
            DataSet ds = cipa.ListaGridTitularesSuplentesCipa((int)GrupoMembro.Empregado);
            lblAntepenultimaCipa.Text = cipa.Posse.ToString("dd-MM-yyyy");
            grdAntepenultimaCipa.DataSource = ds;
        }
    }
}