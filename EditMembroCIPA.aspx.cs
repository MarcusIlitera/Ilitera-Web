using Ilitera.Common;
using Ilitera.Opsa.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Ilitera.Net
{
    public partial class EditMembroCIPA : System.Web.UI.Page
    {
        private Cliente cliente = new Cliente();
        private Cipa cipa = new Cipa();
        private Juridica juridica = new Juridica();
        MembroCipa membroCipa = new MembroCipa();
        int id = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            cliente.Find(Convert.ToInt32(Session["Empresa"]));
            juridica = cliente;
            cipa = cliente.GetGestaoAtual();
            if (!IsPostBack)
            {
                PopulaCampos();
            }
        }

        private void PopulaCampos()
        {
            lstEmpregados.Items.Clear();
            if (!string.IsNullOrEmpty(Request.QueryString["i"]))
            {
                id = Convert.ToInt32(Request.QueryString["i"]);
            }
            else
            {
                MsgBox1.Show("Ilitera.Net", "Parâmetro não encontrado", null, new EO.Web.MsgBoxButton("OK"));
            }

            StringBuilder str = new StringBuilder();
            str.Append("IdCipa=" + cipa.Id);
            str.Append(" AND IdMembroCipa=" + id);
            DataSet membrosCipa = new MembroCipa().Get(str.ToString());
            membroCipa.Find(Convert.ToInt32(membrosCipa.Tables[0].Rows[0].ItemArray[0]));
            if (membrosCipa.Tables[0].Rows[0].ItemArray[2] != DBNull.Value)
            {
                Empregado empregado = new Empregado();
                empregado.Find(Convert.ToInt32(membrosCipa.Tables[0].Rows[0].ItemArray[2]));

                txtNome.Text = empregado.tNO_EMPG;
            }else
            {
                txtNome.Text = membroCipa.NomeMembro;
            }
            txtCargo.Text = membroCipa.GetNomeCargo();
            txtEstabilidade.Text = membroCipa.Estabilidade == new DateTime() ? "" : membroCipa.Estabilidade.ToString("dd/MM/yyyy");
            if (membroCipa.IndStatus == 1)
            {
                rbStatus.SelectedIndex = 0;
            }else if(membroCipa.IndStatus == 2)
            {
                rbStatus.SelectedIndex = 1;
            }else
            {
                rbStatus.SelectedIndex = 2;
            }

            ArrayList list = new ArrayList();

            if (membroCipa.IndStatus == (int)MembroCipa.Status.Ativo)
            {
                lblEmpregados.Text = "Sustituir pelo Suplente:";

                if (membroCipa.IndGrupoMembro == (int)GrupoMembro.Empregado)
                    list = MembroCipa.GetListaSuplentes(membroCipa.IdCipa, (int)GrupoMembro.Empregado);
                else if (membroCipa.IndGrupoMembro == (int)GrupoMembro.Empregador)
                    list = MembroCipa.GetListaSuplentes(membroCipa.IdCipa, (int)GrupoMembro.Empregador);
                else
                    list = MembroCipa.GetListaSuplentes(membroCipa.IdCipa, (int)GrupoMembro.Secretario);
            }
            else
            {
                lblEmpregados.Text = "Sustituir pelo Titular:";

                if (membroCipa.IndGrupoMembro == (int)GrupoMembro.Empregado)
                    list = MembroCipa.GetListaIntegrantes(membroCipa.IdCipa, (int)GrupoMembro.Empregado);
                else if (membroCipa.IndGrupoMembro == (int)GrupoMembro.Empregador)
                    list = MembroCipa.GetListaIntegrantes(membroCipa.IdCipa, (int)GrupoMembro.Empregador);
                else
                    list = MembroCipa.GetListaIntegrantes(membroCipa.IdCipa, (int)GrupoMembro.Secretario);
            }


            foreach (MembroCipa membro in list)
            {
                if (membro.Id != id)
                {
                    string texto = "";
                    if (membro.NomeMembro != "")
                    {
                        texto = membro.NomeMembro;
                    }
                    else
                    {

                        texto = membro.IdEmpregado.tNO_EMPG;


                        while (texto == "")
                        {
                            int idEmpregado = membro.IdEmpregado.Id;
                            Empregado emp = new Empregado();
                            emp.Find(idEmpregado);
                            texto = emp.tNO_EMPG;
                        }
                    }
                    string valor = membro.Id.ToString();
                    ListItem item = new ListItem(texto, valor);


                    lstEmpregados.Items.Add(item);
                }
            }
        }

        protected void btnConfirmar_Click(object sender, EventArgs e)
        {
            try
            {
                int id = 0;
                if (!string.IsNullOrEmpty(Request.QueryString["i"]))
                {
                    id = Convert.ToInt32(Request.QueryString["i"]);
                }
                else
                {
                    MsgBox1.Show("Ilitera.Net", "Parâmetro não encontrado", null, new EO.Web.MsgBoxButton("OK"));
                }

                StringBuilder str = new StringBuilder();
                str.Append("IdCipa=" + cipa.Id);
                str.Append(" AND IdMembroCipa=" + id);
                DataSet empregados = new MembroCipa().Get(str.ToString());
                membroCipa.Find(Convert.ToInt32(empregados.Tables[0].Rows[0].ItemArray[0]));
                bool IsAtivo = membroCipa.IndStatus == (int)MembroCipa.Status.Ativo;

                if (IsAtivo && Convert.ToInt32(rbStatus.SelectedValue) == (int)MembroCipa.Status.Ativo)
                {
                    MsgBox1.Show("Ilitera.Net", "Selecione o Status Afastado ou Renunciou!", null, new EO.Web.MsgBoxButton("OK"));
                    return;
                }

                if (!IsAtivo && Convert.ToInt32(rbStatus.SelectedValue) != (int)MembroCipa.Status.Ativo)
                {
                    MsgBox1.Show("Ilitera.Net", "Selecione o Status Ativo!", null, new EO.Web.MsgBoxButton("OK"));
                    return;
                }

                if (IsAtivo && lstEmpregados.SelectedItem == null)
                {
                    MsgBox1.Show("Ilitera.Net", "Selecione um suplente!", null, new EO.Web.MsgBoxButton("OK"));
                    return;
                }

                if (!IsAtivo && lstEmpregados.SelectedItem == null)
                {
                    MsgBox1.Show("Ilitera.Net", "Selecione um titular!", null, new EO.Web.MsgBoxButton("OK"));
                    return;
                }

                MembroCipa suplenteCipa = new MembroCipa();
                suplenteCipa.Find("IdMembroCipa =" + lstEmpregados.SelectedValue);

                if (Convert.ToInt32(rbStatus.SelectedValue) == (int)MembroCipa.Status.Afastado)
                    MembroCipa.SubstituirMembro(membroCipa, suplenteCipa, MembroCipa.Status.Afastado, (GrupoMembro)membroCipa.IndGrupoMembro);
                else if (Convert.ToInt32(rbStatus.SelectedValue) == (int)MembroCipa.Status.Renunciou)
                    MembroCipa.SubstituirMembro(membroCipa, suplenteCipa, MembroCipa.Status.Renunciou, (GrupoMembro)membroCipa.IndGrupoMembro);
                else if (Convert.ToInt32(rbStatus.SelectedValue) == (int)MembroCipa.Status.Ativo)
                    MembroCipa.SubstituirMembro(membroCipa, suplenteCipa, MembroCipa.Status.Ativo, (GrupoMembro)membroCipa.IndGrupoMembro);

                StringBuilder st = new StringBuilder();
                st.AppendFormat("void(window.open('MembrosCIPA.aspx?a=true', 'MembrosCIPA','scrollbars=yes, resizable=yes, toolbar=no, addressbar=no, menubar=no, width=825px, height=700px')); window.close();");

                System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), " ", st.ToString(), true);

            }
            catch (Exception ex)
            {
                MsgBox1.Show("Ilitera.Net", ex.Message, null, new EO.Web.MsgBoxButton("OK"));
            }
        }
    }
}