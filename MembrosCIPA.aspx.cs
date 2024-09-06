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
    public partial class MembrosCIPA : System.Web.UI.Page
    {
        private Cliente cliente = new Cliente();
        private Cipa cipa = new Cipa();
        private Juridica juridica = new Juridica();
        protected void Page_Load(object sender, EventArgs e)
        {
            cliente.Find(Convert.ToInt32(Session["Empresa"]));
            juridica = cliente;
            cipa = cliente.GetGestaoAtual();

            if (!IsPostBack)
            {
                PopulaGridRepresentantesEmpregador(true);
                PopulaGridRepresentatesEmpregados(true);
                bool foiAlterado = false;
                if (!string.IsNullOrEmpty(Request.QueryString["a"]))
                {
                    foiAlterado = Convert.ToBoolean(Request.QueryString["a"]);
                }

                if (foiAlterado) { 
                    MsgBox1.Show("Ilitera.Net", "Alteração realizada", null, new EO.Web.MsgBoxButton("OK"));

                    string script = @"
                                    <script type='text/javascript'>
                                        setTimeout(function() {
                                            var contentElement = document.getElementById('MsgBox1_msg_t');
                                            if (contentElement) {
                                                contentElement.style.color = 'rgb(125, 123, 123)';
                                                contentElement.style.fontSize = '14px';
                                                contentElement.style.fontStyle = 'normal';
                                                contentElement.style.fontVariant = 'normal';
                                                contentElement.style.fontWeight = '500';
                                                contentElement.style.fontFamily = 'UniviaPro';
                                            }
                                        }, 100); 
                                        setTimeout(function() {
                                            var contentElement = document.getElementById('MsgBox1_headerHtml');
                                            if (contentElement) {
                                                contentElement.style.color = 'rgb(28, 148, 137)';
                                                contentElement.style.fontSize = '14px';
                                                contentElement.style.fontStyle = 'normal';
                                                contentElement.style.fontVariant = 'normal';
                                                contentElement.style.fontWeight = '500';
                                                contentElement.style.fontFamily = 'UniviaPro';
                                            }
                                        }, 100);
                                    </script>";
                    ClientScript.RegisterStartupScript(this.GetType(), "ApplyMsgBoxStyles", script);
                }
            }     
        }


        private void PopulaGridRepresentantesEmpregador(bool Ativos)
        {
            grd_Representantes_Empregador.Items.Clear();
            grd_Representantes_Empregador.DataSource = null;
            grd_Representantes_Empregador.DataBind();
            StringBuilder str = new StringBuilder();
            str.Append("IdCipa=" + cipa.Id);
            str.Append(" AND IndGrupoMembro=" + (int)GrupoMembro.Empregador);
            if (Ativos)
                str.Append(" AND IndStatus=" + ((int)MembroCipa.Status.Ativo).ToString());
            else
                str.Append(" AND IndStatus<>" + ((int)MembroCipa.Status.Ativo).ToString());
            str.Append(" ORDER BY IndTitularSuplente, Numero");

            DataSet empregados = new MembroCipa().Get(str.ToString());
            DataTable zDt = new DataTable();
            zDt.Columns.Add("IdEmprcImaegado");
            zDt.Columns.Add("Editar");
            zDt.Columns.Add("Cargo");
            zDt.Columns.Add("Nome");
            zDt.Columns.Add("Status");
            zDt.Columns.Add("Estabilidade");

            if (empregados.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in empregados.Tables[0].Rows)
                {
                    if (row["IdEmpregado"] != DBNull.Value)
                    {
                        Empregado empregado = new Empregado();
                        empregado.Find(Convert.ToInt32(row["IdEmpregado"]));
                        MembroCipa membroCipa = new MembroCipa();
                        membroCipa.Find(Convert.ToInt32(row["IdMembroCipa"]));
                        string titularSuplente = Convert.ToInt32(row["IndTitularSuplente"]) == 1 ? "Titular" : "Suplente";
                        string DataEstabilidade = "";
                        if (row["Estabilidade"] != DBNull.Value)
                        {
                            DataEstabilidade = Convert.ToDateTime(row["Estabilidade"]).ToString("dd/MM/yyyy");
                        }
                        string cargoCipa = membroCipa.GetNomeCargo();
                        if (cargoCipa.Contains("0º"))
                            membroCipa.ReordenaMembro(membroCipa.IndGrupoMembro);
                        zDt.Rows.Add(membroCipa.Id, "Editar", cargoCipa, empregado.tNO_EMPG, titularSuplente, DataEstabilidade);
                    }
                    else
                    {
                        MembroCipa membroCipa = new MembroCipa();
                        membroCipa.Find(Convert.ToInt32(row["IdMembroCipa"]));
                        string titularSuplente = Convert.ToInt32(row["IndTitularSuplente"]) == 1 ? "Titular" : "Suplente";
                        string DataEstabilidade = "";
                        if (row["Estabilidade"] != DBNull.Value)
                        {
                            DataEstabilidade = Convert.ToDateTime(row["Estabilidade"]).ToString("dd/MM/yyyy");
                        }
                        string cargoCipa = membroCipa.GetNomeCargo();
                        zDt.Rows.Add(membroCipa.Id, "Editar", cargoCipa, membroCipa.NomeMembro, titularSuplente, DataEstabilidade);
                    }
                }

                DataSet zDs = new DataSet();
                zDs.Tables.Add(zDt);
                grd_Representantes_Empregador.DataSource = zDs;
                grd_Representantes_Empregador.DataBind();
            }
        }

        private void PopulaGridRepresentatesEmpregados(bool Ativos)
        {
            grd_Representantes_Empregados.Items.Clear();
            grd_Representantes_Empregados.DataSource = null;
            grd_Representantes_Empregados.DataBind();
            StringBuilder str = new StringBuilder();
            str.Append("IdCipa=" + cipa.Id);
            str.Append(" AND IndGrupoMembro=" + (int)GrupoMembro.Empregado);
            if (Ativos)
                str.Append(" AND IndStatus=" + ((int)MembroCipa.Status.Ativo).ToString());
            else
                str.Append(" AND IndStatus<>" + ((int)MembroCipa.Status.Ativo).ToString());
            str.Append(" ORDER BY IndTitularSuplente, Numero");

            DataSet empregados = new MembroCipa().Get(str.ToString());
            DataTable zDt = new DataTable();
            zDt.Columns.Add("IdEmprcImaegado");
            zDt.Columns.Add("Editar");
            zDt.Columns.Add("Cargo");
            zDt.Columns.Add("Nome");
            zDt.Columns.Add("Status");
            zDt.Columns.Add("Estabilidade");

            if (empregados.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in empregados.Tables[0].Rows)
                {
                    if (row["IdEmpregado"] != DBNull.Value)
                    {
                        Empregado empregado = new Empregado();
                        empregado.Find(Convert.ToInt32(row["IdEmpregado"]));
                        MembroCipa membroCipa = new MembroCipa();
                        membroCipa.Find(Convert.ToInt32(row["IdMembroCipa"]));
                        string titularSuplente = Convert.ToInt32(row["IndTitularSuplente"]) == 1 ? "Titular" : "Suplente";
                        string DataEstabilidade = "";
                        if (row["Estabilidade"] != DBNull.Value)
                        {
                            DataEstabilidade = Convert.ToDateTime(row["Estabilidade"]).ToString("dd/MM/yyyy");
                        }
                        string cargoCipa = membroCipa.GetNomeCargo();
                        if (cargoCipa.Contains("0º"))
                            membroCipa.ReordenaMembro(membroCipa.IndGrupoMembro);
                        zDt.Rows.Add(membroCipa.Id, "Editar", cargoCipa, empregado.tNO_EMPG, titularSuplente, DataEstabilidade);
                    }
                    else
                    {
                        MembroCipa membroCipa = new MembroCipa();
                        membroCipa.Find(Convert.ToInt32(row["IdMembroCipa"]));
                        string titularSuplente = Convert.ToInt32(row["IndTitularSuplente"]) == 1 ? "Titular" : "Suplente";
                        string DataEstabilidade = "";
                        if (row["Estabilidade"] != DBNull.Value)
                        {
                            DataEstabilidade = Convert.ToDateTime(row["Estabilidade"]).ToString("dd/MM/yyyy");
                        }
                        string cargoCipa = membroCipa.GetNomeCargo();
                        zDt.Rows.Add(membroCipa.Id, "Editar", cargoCipa, membroCipa.NomeMembro, titularSuplente, DataEstabilidade);
                    }
                }

                DataSet zDs = new DataSet();
                zDs.Tables.Add(zDt);
                grd_Representantes_Empregados.DataSource = zDs;
                grd_Representantes_Empregados.DataBind();
            }
        }

        protected void chkFiltro_CheckedChanged(object sender, EventArgs e)
        {
            if (chkFiltro.Checked)
            {
                PopulaGridRepresentantesEmpregador(false);
                PopulaGridRepresentatesEmpregados(false);
            }
            else
            {
                PopulaGridRepresentantesEmpregador(true);
                PopulaGridRepresentatesEmpregados(true);
            }
        }

        protected void grd_Representantes_Empregador_ItemCommand(object sender, EO.Web.GridCommandEventArgs e)
        {
            if (e.CommandName == "0")
            {
                string parametros = "?i=" + e.Item.Key.ToString();
                StringBuilder st = new StringBuilder();
                st.AppendFormat("void(window.open('EditMembroCIPA.aspx" + parametros + "', 'EditarMembroCipa', 'scrollbars=yes, resizable=yes, toolbar=no, addressbar=no, menubar=no, width=825px, height=350px')); window.close();");

                System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), " ", st.ToString(), true);
            }
        }

        protected void grd_Representantes_Empregados_ItemCommand(object sender, EO.Web.GridCommandEventArgs e)
        {
            if (e.CommandName == "0")
            {
                string parametros = "?i=" + e.Item.Key.ToString();
                StringBuilder st = new StringBuilder();
                st.AppendFormat("void(window.open('EditMembroCIPA.aspx" + parametros + "', 'EditarMembroCipa', 'scrollbars=yes, resizable=yes, toolbar=no, addressbar=no, menubar=no, width=825px, height=350px')); window.close();");

                System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), " ", st.ToString(), true);
            }
        }
    }
}