using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using Ilitera.Opsa.Data;
using Ilitera.Common;
using System.Data;
using EO.Web;

namespace Ilitera.Net
{
    public partial class AdicionarIndicado : System.Web.UI.Page
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
                PopulaListBox();
                PopulaGrid();
            }
        }

        protected void txtLocalizar_TextChanged(object sender, EventArgs e)
        {
            lst_Empregados.Items.Clear();
            string localizar = txtLocalizar.Text;
            System.Text.StringBuilder strEmpregados = new System.Text.StringBuilder();

            strEmpregados.Append("nID_EMPR =" + cipa.IdCliente.Id + " AND tNO_EMPG Like '%" + localizar + "%'");
            strEmpregados.Append(" AND hDT_DEM IS NULL");
            strEmpregados.Append(" ORDER BY tNO_EMPG ");

            DataSet zDs = new Empregado().Get(strEmpregados.ToString());

            for (int zCont = 0; zCont < zDs.Tables[0].Rows.Count; zCont++)
            {
                string texto = zDs.Tables[0].Rows[zCont][1].ToString();
                string valor = zDs.Tables[0].Rows[zCont][0].ToString();

                ListItem item = new ListItem(texto, valor);
                lst_Empregados.Items.Add(item);
            }
        }

        protected void txtNome_TextChanged(object sender, EventArgs e)
        {

        }

        protected void btnAdicionar_Click(object sender, ImageClickEventArgs e)
        {
            if (lst_Empregados.SelectedItem != null)
            {
                foreach (ListItem item in lst_Empregados.Items)
                {
                    try
                    {
                        if (item.Selected)
                        {
                            Empregado empregado = new Empregado();
                            empregado.Find(Convert.ToInt32(item.Value));
                            MembroCipa membroCipa = new MembroCipa();
                            membroCipa.Inicialize();
                            membroCipa.IdCipa = cipa;
                            membroCipa.IdEmpregado = empregado;
                            membroCipa.IndGrupoMembro = (short)GrupoMembro.Empregador;
                            membroCipa.Save();
                        }
                    }
                    catch (Exception ex)
                    {
                        MsgBox1.Show("Ilitera.Net", ex.Message, null, new EO.Web.MsgBoxButton("OK"));
                    }
                }
                PopulaGrid();
                PopulaListBox();
            }else
            {
                MsgBox1.Show("Ilitera.Net", "Nenhum item foi selecionado", null, new EO.Web.MsgBoxButton("OK"));
            }
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

        protected void btnSubir_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                for (int i = 0; i < lst_Empregados.Items.Count; i++)
                {
                    Empregado empregado = new Empregado();
                    empregado.Find(Convert.ToInt32(lst_Empregados.Items[i].Value));
                    MembroCipa membroCipa = new MembroCipa();
                    membroCipa.Inicialize();
                    membroCipa.IdCipa = cipa;
                    membroCipa.IdEmpregado = empregado;
                    membroCipa.IndGrupoMembro = (short)GrupoMembro.Empregador;
                    membroCipa.Save();
                }
                PopulaGrid();
                PopulaListBox();
            }
            catch(Exception ex)
            {
                MsgBox1.Show("Ilitera.Net", ex.Message, null, new EO.Web.MsgBoxButton("OK"));
            }
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

        private void PopulaListBox()
        {
            lst_Empregados.Items.Clear();
            StringBuilder str = new StringBuilder();
            str.Append("nID_EMPR=" + cipa.IdCliente.Id);
            str.Append(" AND nID_EMPREGADO NOT IN ");
            str.Append(" (SELECT IdEmpregado FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.MembroCipa WHERE IdEmpregado IS NOT NULL AND IdCipa =" + cipa.Id + " AND IndGrupoMembro=" + (int)GrupoMembro.Empregador + ")");
            str.Append(" ORDER BY tNO_EMPG");

            DataSet zDs = new Empregado().Get(str.ToString());

            for (int zCont = 0; zCont < zDs.Tables[0].Rows.Count; zCont++)
            {
                string texto = zDs.Tables[0].Rows[zCont][1].ToString();
                string valor = zDs.Tables[0].Rows[zCont][0].ToString();

                ListItem item = new ListItem(texto, valor);
                lst_Empregados.Items.Add(item);
            }
        }

        private void PopulaGrid()
        { 
            grd_Membros2.Items.Clear();
            grd_Membros2.DataSource = null;
            grd_Membros2.DataBind();
            StringBuilder str = new StringBuilder();

            str.Append("IdCipa=" + cipa.Id);
            str.Append(" AND IndGrupoMembro=" + (int)GrupoMembro.Empregador);
            str.Append(" ORDER BY IndTitularSuplente, Numero");


            DataSet empregados = new MembroCipa().Get(str.ToString());
            DataTable zDt = new DataTable();
            zDt.Columns.Add("IdMembroCipa");
            zDt.Columns.Add("Empregado");
            zDt.Columns.Add("CargoCipa");


            foreach (DataRow row in empregados.Tables[0].Rows)
            {
                if (row["IdEmpregado"] != DBNull.Value)
                {
                    Empregado empregado = new Empregado();
                    empregado.Find(Convert.ToInt32(row["IdEmpregado"]));
                    MembroCipa membroCipa = new MembroCipa();
                    membroCipa.Find(Convert.ToInt32(row["IdMembroCipa"]));
                    string cargoCipa = membroCipa.GetNomeCargo();
                    zDt.Rows.Add(membroCipa.Id, empregado.tNO_EMPG, cargoCipa);
                }
                else
                {
                    MembroCipa membroCipa = new MembroCipa();
                    membroCipa.Find(Convert.ToInt32(row["IdMembroCipa"]));
                    string cargoCipa = membroCipa.GetNomeCargo();
                    zDt.Rows.Add(membroCipa.Id, membroCipa.NomeMembro, cargoCipa);
                }

               
            }


            DataSet zDs = new DataSet();
            zDs.Tables.Add(zDt);
            grd_Membros2.DataSource = zDs;
            grd_Membros2.DataBind();
        }

        protected void lst_Empregados_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnDescer_Click(object sender, ImageClickEventArgs e)
        {
            MembroCipa membroCipa = new MembroCipa();
            if (grd_Membros2 != null)
            {
                foreach (GridItem item in grd_Membros2.Items)
                {
                    membroCipa.Delete("IdMembroCipa=" + Convert.ToInt32(item.Key));
                }
            }
            PopulaGrid();
            PopulaListBox();
        }

        protected void btnRemover_Click(object sender, ImageClickEventArgs e)
        {
            MembroCipa membroCipa = new MembroCipa();
            if (grd_Membros2 != null)
            {
                try
                {
                    int idMembroCipa = Convert.ToInt32(grd_Membros2.Items[grd_Membros2.SelectedItemIndex].Key);
                    membroCipa.Delete("IdMembroCipa=" + idMembroCipa);
                }catch(ArgumentOutOfRangeException)
                {
                    MsgBox1.Show("Ilitera.Net", "Nenhum item selecionado", null, new EO.Web.MsgBoxButton("OK"));
                }
            }
            PopulaGrid();
            PopulaListBox();
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

        protected void btnNome_Click(object sender, ImageClickEventArgs e)
        {
            string nome = txtNome.Text;
            if(nome.Trim() == "")
            {
                MsgBox1.Show("Ilitera.Net", "Digite o nome do membro da Cipa!", null, new EO.Web.MsgBoxButton("OK"));
            }else
            {
                MembroCipa membroCipa = new MembroCipa();
                membroCipa.Inicialize();
                membroCipa.IdCipa = cipa;
                membroCipa.IdEmpregado.Id = 0;
                membroCipa.NomeMembro = nome;
                membroCipa.IndGrupoMembro = (short)GrupoMembro.Empregador;
                membroCipa.Save();
            }
            PopulaGrid();
            PopulaListBox();
            txtNome.Text = "";
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