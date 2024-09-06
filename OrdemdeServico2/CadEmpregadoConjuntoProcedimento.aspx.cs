using System;
using System.Collections;
using System.Data;
using System.Web.SessionState;
using System.Web.UI.WebControls;
using System.Web;
using Ilitera.Opsa.Data;
using System.Text;
using System.Drawing;
using System.Web.UI;

namespace Ilitera.Net.OrdemDeServico
{
    /// <summary>
    /// Summary description for CadConjuntoProcedimento.
    /// </summary>
    public partial class CadEmpregadoConjuntoProcedimento : System.Web.UI.Page
    {

        protected Color backColorDisabledBox = Color.FromName("#EBEBEB");
        protected Color backColorEnabledBox = Color.FromName("#FCFEFD");
        protected Color foreColorDisabledLabel = Color.Gray;
        protected Color foreColorEnabledLabel = Color.FromName("#44926D");
        protected Color foreColorEnabledTextBox = Color.FromName("#004000");
        protected Color foreColorDisabledTextBox = Color.Gray;
        protected Color borderColorDisabledBox = Color.LightGray;
        protected Color borderColorEnabledBox = Color.FromName("#7CC5A1");
        protected Color backColorEnabledBoxYellow = Color.LightYellow;


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

                cld_OS.SelectedDate = DateTime.Now;

                Carrega_Combo_GHE();
                Carrega_Combo_Setor();

                PopulaLsbEmpregados();

                imbDown.Enabled = false;
                imbAllDown.Enabled = false;
                imbUp.Enabled = false;
                imbAllUp.Enabled = false;
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
            this.imgBusca.Click += new System.Web.UI.ImageClickEventHandler(this.imgBusca_Click);
            this.imbDown.Click += new System.Web.UI.ImageClickEventHandler(this.imbDown_Click);
            this.imbAllDown.Click += new System.Web.UI.ImageClickEventHandler(this.imbAllDown_Click);
            this.imbUp.Click += new System.Web.UI.ImageClickEventHandler(this.imbUp_Click);
            this.imbAllUp.Click += new System.Web.UI.ImageClickEventHandler(this.imbAllUp_Click);

        }
        #endregion


        private void Carrega_Combo_Setor()
        {

            Ilitera.Data.PPRA_EPI xGHE = new Ilitera.Data.PPRA_EPI();

            DataSet dS1 = xGHE.Carregar_Setores(System.Convert.ToInt32(Session["Empresa"].ToString()));

            //cmb_Setor.DataSource = dS1;
            //cmb_Setor.DataValueField = "nID_SETOR";
            //cmb_Setor.DataTextField = "tNO_STR_EMPR";
            //cmb_Setor.DataBind();

            cmb_Setor.Items.Clear();
            cmb_Setor.Items.Add("Sem Filtro");

            lst_Id_Setor.Items.Clear();
            lst_Id_Setor.Items.Add("0");

            foreach (DataRow row in dS1.Tables[0].Rows)
            {
                cmb_Setor.Items.Add(row["tNO_Str_EMPR"].ToString());
                lst_Id_Setor.Items.Add(row["nId_Setor"].ToString());
            }

            cmb_Setor.SelectedIndex = 0;

            return;

        }


        private void Carrega_Combo_GHE()
        {
            string zLaudo = "";

            Ilitera.Data.PPRA_EPI xGHE = new Ilitera.Data.PPRA_EPI();
            DataSet xdS = xGHE.Trazer_Laudos_GHEs(2000, 2050, System.Convert.ToInt32(Session["Empresa"].ToString()), "N");

            cmb_GHE.Items.Clear();
            cmb_GHE.Items.Add("Sem Filtro");

            lst_Id_GHE.Items.Clear();
            lst_Id_GHE.Items.Add("0");

            foreach (DataRow row in xdS.Tables[0].Rows)
            {
                if (zLaudo == "") zLaudo = row["Laudo"].ToString();

                if (row["Laudo"].ToString() == zLaudo)
                {
                    cmb_GHE.Items.Add(row["GHE"].ToString());
                    lst_Id_GHE.Items.Add(row["nId_Func"].ToString());
                }

            }

            cmb_GHE.SelectedIndex = 0;

            return;
        }



        private void PopulaLsbEmpregados()
        {
            DataSet dsEmpregado = new DataSet();

            string xConsulta = "";

            //if (cliente.IsLocalDeTrabalho())
            //	dsEmpregado = new Empregado().ExecuteDataset("EXEC " + Mestra.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.sps_EmpregadoFuncao " + cliente.Id + ", NULL, 1, 0");
            //else



            if (cmb_GHE.SelectedIndex > 0)
            {
                xConsulta = " and nid_empregado in " +
                            " ( " +
                            "select nid_empregado " +
                            "  from " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblempregado_funcao " +
                            "  where nID_EMPREGADO_FUNCAO in " +
                            "  ( " +
                            "    select nid_empregado_funcao from " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblFUNC_EMPREGADO " +
                            "    where nid_func = " + lst_Id_GHE.Items[cmb_GHE.SelectedIndex].ToString() + " " +
                            "  ) " +
                            ") ";
            }


            if (cmb_Setor.SelectedIndex > 0)
            {
                xConsulta = xConsulta + " and nid_empregado in " +
                            " ( " +
                            "select nid_empregado " +
                            "  from " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblempregado_funcao " +
                            "  where nId_Setor = " + lst_Id_Setor.Items[cmb_Setor.SelectedIndex].ToString() + " " +
                            "  ) ";

            }


            dsEmpregado = new Empregado().Get(" ( nID_EMPR=" + lbl_Id_Empresa.Text + " or nid_Empregado in ( Select nId_Empregado from tblEmpregado_Funcao where nId_Empr=" + lbl_Id_Empresa.Text + " and hdt_Termino is null ) ) AND hDT_DEM IS NULL " + xConsulta + " ORDER BY tNO_EMPG");

            DataSet dsEmp = new DataSet();
            DataRow rowEmp;
            DataTable table = new DataTable("Default");
            table.Columns.Add("Id", Type.GetType("System.String"));
            table.Columns.Add("NomeEmpregado", Type.GetType("System.String"));
            dsEmp.Tables.Add(table);

            foreach (DataRow row in dsEmpregado.Tables[0].Select())
            {
                rowEmp = dsEmp.Tables[0].NewRow();
                rowEmp["Id"] = row["nID_EMPREGADO"].ToString();
                rowEmp["NomeEmpregado"] = row["tNO_EMPG"].ToString() + " - RG:" + row["tNO_IDENTIDADE"].ToString();
                dsEmp.Tables[0].Rows.Add(rowEmp);
            }

            lsbEmpregados.DataSource = dsEmp;
            lsbEmpregados.DataTextField = "NomeEmpregado";
            lsbEmpregados.DataValueField = "Id";
            lsbEmpregados.DataBind();
        }

        private void PopuladsConjuntos(DataSet ds, string idsEmpregados)
        {
            DataRow row;
            DataSet dsConjuntos = new Conjunto().Get("IdCliente=" + lbl_Id_Empresa.Text
                + " AND UPPER(Nome) COLLATE SQL_Latin1_General_Cp1251_CS_AS LIKE '%" + txtProcedimento.Text.Trim().ToUpper() + "%'"
                + " AND IdConjunto NOT IN (SELECT IdConjunto FROM ConjuntoEmpregado WHERE IdConjunto IS NOT NULL AND IdEmpregado IN (" + idsEmpregados + "))"
                + " ORDER BY Nome");

            foreach (DataRow rowConj in dsConjuntos.Tables[0].Select())
            {
                row = ds.Tables[0].NewRow();
                row["Id"] = rowConj["IdConjunto"];
                row["Nome"] = "Conj - " + rowConj["Nome"];
                ds.Tables[0].Rows.Add(row);
            }
        }

        private void PopuladsProcedimentos(DataSet ds, string idsEmpregados)
        {
            DataRow row;
            StringBuilder st = new StringBuilder();

            st.Append("IdCliente=" + lbl_Id_Empresa.Text);

            if (!txtProcedimento.Text.Trim().Equals(string.Empty))
                try
                {
                    Convert.ToInt32(txtProcedimento.Text.Trim());

                    st.Append(" AND (UPPER(Nome) COLLATE SQL_Latin1_General_Cp1251_CS_AS LIKE '%" + txtProcedimento.Text.Trim().ToUpper() + "%'");
                    st.Append(" OR Numero=" + txtProcedimento.Text.Trim() + ")");
                }
                catch (Exception)
                {
                    st.Append(" AND UPPER(Nome) COLLATE SQL_Latin1_General_Cp1251_CS_AS LIKE '%" + txtProcedimento.Text.Trim().ToUpper() + "%'");
                }

            st.Append(" AND IdProcedimento NOT IN (SELECT IdProcedimento FROM ProcedimentoEmpregado WHERE IdProcedimento IS NOT NULL AND IdEmpregado IN (" + idsEmpregados + "))");
            st.Append(" ORDER BY Numero");

            DataSet dsProcedimentos = new Procedimento().Get(st.ToString());

            foreach (DataRow rowProc in dsProcedimentos.Tables[0].Select())
            {
                row = ds.Tables[0].NewRow();
                row["Id"] = rowProc["IdProcedimento"];
                row["Nome"] = "Proc - " + Convert.ToInt32(rowProc["Numero"]).ToString("0000") + " " + rowProc["Nome"];
                ds.Tables[0].Rows.Add(row);
            }
        }

        private void PopulaListConjuntos(string idsEmpregados)
        {
            DataSet ds = new DataSet();
            DataTable table = new DataTable("Default");
            table.Columns.Add("Id", Type.GetType("System.String"));
            table.Columns.Add("Nome", Type.GetType("System.String"));
            ds.Tables.Add(table);

            switch (ddlTipoProc.SelectedValue)
            {
                case "0":
                    PopuladsConjuntos(ds, idsEmpregados);
                    break;
                case "1":
                    PopuladsProcedimentos(ds, idsEmpregados);
                    break;
                case "2":
                    PopuladsConjuntos(ds, idsEmpregados);
                    PopuladsProcedimentos(ds, idsEmpregados);
                    break;
            }

            SetAddControls(ds.Tables[0].Rows.Count);

            listBxConjuntoProc.DataSource = ds;
            listBxConjuntoProc.DataTextField = "Nome";
            listBxConjuntoProc.DataValueField = "Id";
            listBxConjuntoProc.DataBind();
        }

        private void SetAddControls(int numRegistros)
        {
            if (numRegistros > 0)
            {
                imbDown.Enabled = true;
            //    imbDown.ImageUrl = "img/down.jpg";
                imbAllDown.Enabled = true;
            //    imbAllDown.ImageUrl = "img/downdown.jpg";
            }
            else
            {
            //    imbDown.ImageUrl = "img/downDisabled.jpg";
                imbDown.Enabled = false;
            //    imbAllDown.ImageUrl = "img/downdownDisabled.jpg";
                imbAllDown.Enabled = false;
            }
        }

        private void SetRemoveControls(int numRegistros)
        {
            if (numRegistros > 0)
            {
                imbUp.Enabled = true;
            //    imbUp.ImageUrl = "img/up.jpg";
                imbAllUp.Enabled = true;
            //    imbAllUp.ImageUrl = "img/upup.jpg";
            }
            else
            {
            //    imbUp.ImageUrl = "img/upDisabled.jpg";
                imbUp.Enabled = false;
            //    imbAllUp.ImageUrl = "img/upupDisabled.jpg";
                imbAllUp.Enabled = false;
            }
        }

        private void PopulaListConjuntoEmpregados()
        {
            listBxConjuntoEmpregado.Items.Clear();
            StringBuilder stConj = new StringBuilder(), stProc = new StringBuilder();

            foreach (ListItem IEmpregado in lsbEmpregados.Items)
                if (IEmpregado.Selected)
                {
                    if (stConj.ToString().Equals(string.Empty))
                        stConj.Append("IdConjunto IN (SELECT IdConjunto FROM ConjuntoEmpregado WHERE IdEmpregado=" + IEmpregado.Value + ")");
                    else
                        stConj.Append(" AND IdConjunto IN (SELECT IdConjunto FROM ConjuntoEmpregado WHERE IdEmpregado=" + IEmpregado.Value + ")");

                    if (stProc.ToString().Equals(string.Empty))
                        stProc.Append("IdProcedimento IN (SELECT IdProcedimento FROM ProcedimentoEmpregado WHERE IdEmpregado=" + IEmpregado.Value + ")");
                    else
                        stProc.Append(" AND IdProcedimento IN (SELECT IdProcedimento FROM ProcedimentoEmpregado WHERE IdEmpregado=" + IEmpregado.Value + ")");
                }

            stConj.Append(" ORDER BY Nome");
            stProc.Append(" ORDER BY Numero");

            ArrayList listConjuntoEmpregados = new Conjunto().Find(stConj.ToString());
            ArrayList listProcedimentoEmpregados = new Procedimento().Find(stProc.ToString());

            SetRemoveControls(listConjuntoEmpregados.Count + listProcedimentoEmpregados.Count);

            foreach (Conjunto conjuntoEmpregado in listConjuntoEmpregados)
                listBxConjuntoEmpregado.Items.Add(new ListItem("Conj - " + conjuntoEmpregado.Nome, conjuntoEmpregado.Id.ToString()));
            foreach (Procedimento procedimentoEmpregado in listProcedimentoEmpregados)
                listBxConjuntoEmpregado.Items.Add(new ListItem("Proc - " + procedimentoEmpregado.Numero.ToString("0000") + " " + procedimentoEmpregado.Nome, procedimentoEmpregado.Id.ToString()));
        }

        protected void lkbListaTodos_Click(object sender, System.EventArgs e)
        {
            if (lsbEmpregados.SelectedItem != null)
            {
                txtProcedimento.Text = string.Empty;
                PopulaListConjuntos(GetIdsEmpregados());
            }
        }

        private void imgBusca_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            if (lsbEmpregados.SelectedItem != null)
                PopulaListConjuntos(GetIdsEmpregados());
        }

        private void PopulaConjuntoEmpregado(int IdConjunto, ref bool checkProcess)
        {
            foreach (ListItem IEmpregado in lsbEmpregados.Items)
                if (IEmpregado.Selected)
                {
                    ConjuntoEmpregado conjuntoEmpregado = new ConjuntoEmpregado();
                    conjuntoEmpregado.Inicialize();
                    conjuntoEmpregado.IdEmpregado.Id = Convert.ToInt32(IEmpregado.Value);
                    conjuntoEmpregado.IdConjunto.Id = IdConjunto;

                    if (!checkProcess)
                    {
                        conjuntoEmpregado.UsuarioProcessoRealizado = "Associação de Conjuntos ou Procedimentos a um ou mais empregados";
                        checkProcess = true;
                    }

                    conjuntoEmpregado.UsuarioId = System.Convert.ToInt32(lbl_Id_Usuario.Text);
                    conjuntoEmpregado.Save();
                }
        }

        private void PopulaProcedimentoEmpregado(int IdProcedimento, ref bool checkProcess)
        {
            foreach (ListItem IEmpregado in lsbEmpregados.Items)
                if (IEmpregado.Selected)
                {
                    ProcedimentoEmpregado procedimentoEmpregado = new ProcedimentoEmpregado();
                    procedimentoEmpregado.Inicialize();
                    procedimentoEmpregado.IdEmpregado.Id = Convert.ToInt32(IEmpregado.Value);
                    procedimentoEmpregado.IdProcedimento.Id = IdProcedimento;

                    if (!checkProcess)
                    {
                        procedimentoEmpregado.UsuarioProcessoRealizado = "Associação de Conjuntos ou Procedimentos a um ou mais empregados";
                        checkProcess = true;
                    }

                    procedimentoEmpregado.UsuarioId = System.Convert.ToInt32(lbl_Id_Usuario.Text);
                    procedimentoEmpregado.Save();
                }
        }

        private void imbDown_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            try
            {
                if (listBxConjuntoProc.SelectedItem == null)
                    throw new Exception("É necessário selecionar os Conjuntos de Procedimento ou Procedimentos que deseja adicionar!");

                bool checkProcess = false;

                foreach (ListItem conjuntoProc in listBxConjuntoProc.Items)
                    if (conjuntoProc.Selected)
                        if (conjuntoProc.Text.IndexOf("Conj - ") != -1)
                            PopulaConjuntoEmpregado(Convert.ToInt32(conjuntoProc.Value), ref checkProcess);
                        else if (conjuntoProc.Text.IndexOf("Proc - ") != -1)
                            PopulaProcedimentoEmpregado(Convert.ToInt32(conjuntoProc.Value), ref checkProcess);

                PopulaListConjuntos(GetIdsEmpregados());
                PopulaListConjuntoEmpregados();
                DisableDateControls();

                MsgBox1.Show("Ordem de Serviço", "Os Conjuntos e Procedimentos selecionados foram adicionados com sucesso aos Empregados selecionados!", null,
                            new EO.Web.MsgBoxButton("OK"));

            }
            catch (Exception ex)
            {
                MsgBox1.Show("Ordem de Serviço", ex.Message, null,
                            new EO.Web.MsgBoxButton("OK"));
            }
        }

        private void imbAllDown_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            try
            {
                if (listBxConjuntoProc.Items.Count.Equals(0))
                    throw new Exception("Não há Conjuntos de Procedimento ou Procedimentos para serem adicionados!");

                bool checkProcess = false;

                foreach (ListItem conjuntoProc in listBxConjuntoProc.Items)
                    if (conjuntoProc.Text.IndexOf("Conj - ") != -1)
                        PopulaConjuntoEmpregado(Convert.ToInt32(conjuntoProc.Value), ref checkProcess);
                    else if (conjuntoProc.Text.IndexOf("Proc - ") != -1)
                        PopulaProcedimentoEmpregado(Convert.ToInt32(conjuntoProc.Value), ref checkProcess);

                PopulaListConjuntos(GetIdsEmpregados());
                PopulaListConjuntoEmpregados();
                DisableDateControls();


                MsgBox1.Show("Ordem de Serviço", "Todos os Conjuntos e Procedimentos listados acima foram adicionados com sucesso aos Empregados selecionados!", null,
                        new EO.Web.MsgBoxButton("OK"));

            }
            catch (Exception ex)
            {
                MsgBox1.Show("Ordem de Serviço", ex.Message, null,
                        new EO.Web.MsgBoxButton("OK"));

            }
        }

        private void RemoveConjuntoEmpregado(string IdConjunto, ref bool checkProcess)
        {
            foreach (ListItem IEmpregado in lsbEmpregados.Items)
                if (IEmpregado.Selected)
                {
                    ConjuntoEmpregado conjuntoEmpregado = new ConjuntoEmpregado();
                    conjuntoEmpregado.Find("IdConjunto=" + IdConjunto + " AND IdEmpregado=" + IEmpregado.Value);

                    if (!checkProcess)
                    {
                        conjuntoEmpregado.UsuarioProcessoRealizado = "Desassociação de Conjuntos ou Procedimentos de um ou mais empregados";
                        checkProcess = true;
                    }

                    conjuntoEmpregado.UsuarioId = System.Convert.ToInt32(lbl_Id_Usuario.Text);
                    conjuntoEmpregado.Delete();
                }
        }

        private void RemoveProcedimentoEmpregado(string IdProcedimento, ref bool checkProcess)
        {
            foreach (ListItem IEmpregado in lsbEmpregados.Items)
                if (IEmpregado.Selected)
                {
                    ProcedimentoEmpregado procedimentoEmpregado = new ProcedimentoEmpregado();
                    procedimentoEmpregado.Find("IdProcedimento=" + IdProcedimento + " AND IdEmpregado=" + IEmpregado.Value);

                    if (!checkProcess)
                    {
                        procedimentoEmpregado.UsuarioProcessoRealizado = "Desassociação de Conjuntos ou Procedimentos de um ou mais empregados";
                        checkProcess = true;
                    }

                    procedimentoEmpregado.UsuarioId = System.Convert.ToInt32(lbl_Id_Usuario.Text);
                    procedimentoEmpregado.Delete();
                }
        }

        private void imbUp_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            try
            {
                if (listBxConjuntoEmpregado.SelectedItem == null)
                    throw new Exception("É necessário selecionar o Conjunto de Procedimentos ou Procedimento que deseja remover!");

                bool checkProcess = false;

                if (listBxConjuntoEmpregado.SelectedItem.Text.IndexOf("Conj - ") != -1)
                    RemoveConjuntoEmpregado(listBxConjuntoEmpregado.SelectedValue, ref checkProcess);
                else if (listBxConjuntoEmpregado.SelectedItem.Text.IndexOf("Proc - ") != -1)
                    RemoveProcedimentoEmpregado(listBxConjuntoEmpregado.SelectedValue, ref checkProcess);

                PopulaListConjuntos(GetIdsEmpregados());
                PopulaListConjuntoEmpregados();
                DisableDateControls();


                MsgBox1.Show("Ordem de Serviço", "O Conjunto ou Procedimento selecionado foi removido com sucesso dos Empregados selecionados!", null,
                        new EO.Web.MsgBoxButton("OK"));

            }
            catch (Exception ex)
            {
                MsgBox1.Show("Ordem de Serviço", ex.Message, null,
                        new EO.Web.MsgBoxButton("OK"));

            }
        }

        private void imbAllUp_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            try
            {
                if (listBxConjuntoEmpregado.Items.Count.Equals(0))
                    throw new Exception("Não há Conjuntos de Procedimento ou Procedimentos para serem removidos!");

                bool checkProcess = false;

                foreach (ListItem conjuntoEmpreg in listBxConjuntoEmpregado.Items)
                    if (conjuntoEmpreg.Text.IndexOf("Conj - ") != -1)
                        RemoveConjuntoEmpregado(conjuntoEmpreg.Value, ref checkProcess);
                    else if (conjuntoEmpreg.Text.IndexOf("Proc - ") != -1)
                        RemoveProcedimentoEmpregado(conjuntoEmpreg.Value, ref checkProcess);

                PopulaListConjuntos(GetIdsEmpregados());
                PopulaListConjuntoEmpregados();
                DisableDateControls();


                MsgBox1.Show("Ordem de Serviço", "Todos os Conjuntos e Procedimentos foram removidos com sucesso dos Empregados selecionados!", null,
                        new EO.Web.MsgBoxButton("OK"));

            }
            catch (Exception ex)
            {
                MsgBox1.Show("Ordem de Serviço", ex.Message, null,
                        new EO.Web.MsgBoxButton("OK"));

            }
        }

        private void DisableDateControls()
        {
            lblDataInicio.ForeColor = foreColorDisabledLabel;
            wdtDataInicio.ReadOnly = true;
            wdtDataInicio.BackColor = backColorDisabledBox;
            wdtDataInicio.BorderColor = borderColorDisabledBox;
            lblDataTermino.ForeColor = foreColorDisabledLabel;
            wdtDataTermino.Text = "";
            wdtDataTermino.ReadOnly = true;
            wdtDataTermino.BackColor = backColorDisabledBox;
            wdtDataTermino.BorderColor = borderColorDisabledBox;
            btnGravar.Enabled = false;
        }

        protected void lsbEmpregados_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            txtProcedimento.Text = string.Empty;
            DisableDateControls();

            if (lsbEmpregados.SelectedItem != null)
            {
                PopulaListConjuntos(GetIdsEmpregados());
                PopulaListConjuntoEmpregados();
            }
            else
            {
                listBxConjuntoProc.Items.Clear();
                listBxConjuntoEmpregado.Items.Clear();
            }
        }

        private string GetIdsEmpregados()
        {
            string idEmpreg = string.Empty;

            foreach (ListItem empregSelected in lsbEmpregados.Items)
                if (empregSelected.Selected)
                    if (idEmpreg == string.Empty)
                        idEmpreg = empregSelected.Value;
                    else
                        idEmpreg += ", " + empregSelected.Value;

            return idEmpreg;
        }

        protected void ddlTipoProc_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (lsbEmpregados.SelectedItem != null)
                PopulaListConjuntos(GetIdsEmpregados());
            else
                MsgBox1.Show("Ordem de Serviço", "É necessário selecionar o(s) empregado(s) antes de adicionar os Procedimentos e Conjuntos na Ordem de Serviço!", null,
                    new EO.Web.MsgBoxButton("OK"));

        }

        protected void listBxConjuntoEmpregado_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            lblDataInicio.ForeColor = foreColorEnabledLabel;
            wdtDataInicio.ReadOnly = false;
            wdtDataInicio.BackColor = backColorEnabledBox;
            wdtDataInicio.BorderColor = borderColorEnabledBox;
            lblDataTermino.ForeColor = foreColorEnabledLabel;
            wdtDataTermino.ReadOnly = false;
            wdtDataTermino.BackColor = backColorEnabledBox;
            wdtDataTermino.BorderColor = borderColorEnabledBox;
            btnGravar.Enabled = true;

            DateTime dataInicio = new DateTime();
            DateTime dataTermino = new DateTime();
            bool checkProcess = false;
            bool showDataInicio = true;
            bool showDataTermino = true;

            if (listBxConjuntoEmpregado.SelectedItem.Text.IndexOf("Conj - ") != -1)
                foreach (ListItem itemEmpregadoConjunto in lsbEmpregados.Items)
                    if (itemEmpregadoConjunto.Selected)
                    {
                        ConjuntoEmpregado conjuntoEmpregado = new ConjuntoEmpregado();
                        conjuntoEmpregado.Find("IdConjunto=" + listBxConjuntoEmpregado.SelectedValue
                            + " AND IdEmpregado=" + itemEmpregadoConjunto.Value);

                        if (!checkProcess)
                        {
                            dataInicio = conjuntoEmpregado.DataInicio;
                            dataTermino = conjuntoEmpregado.DataTermino;
                            checkProcess = true;
                        }
                        else
                        {
                            if (dataInicio != conjuntoEmpregado.DataInicio)
                                showDataInicio = false;
                            if (dataTermino != conjuntoEmpregado.DataTermino)
                                showDataTermino = false;
                        }
                    }
                    else if (listBxConjuntoEmpregado.SelectedItem.Text.IndexOf("Proc - ") != -1)
                        foreach (ListItem itemEmpregadoProcedimento in lsbEmpregados.Items)
                            if (itemEmpregadoProcedimento.Selected)
                            {
                                ProcedimentoEmpregado procedimentoEmpregado = new ProcedimentoEmpregado();
                                procedimentoEmpregado.Find("IdProcedimento=" + listBxConjuntoEmpregado.SelectedValue
                                    + " AND IdEmpregado=" + itemEmpregadoProcedimento.Value);

                                if (!checkProcess)
                                {
                                    dataInicio = procedimentoEmpregado.DataInicio;
                                    dataTermino = procedimentoEmpregado.DataTermino;
                                    checkProcess = true;
                                }
                                else
                                {
                                    if (dataInicio != procedimentoEmpregado.DataInicio)
                                        showDataInicio = false;
                                    if (dataTermino != procedimentoEmpregado.DataTermino)
                                        showDataTermino = false;
                                }
                            }

            if (showDataInicio)
                wdtDataInicio.Text = dataInicio.ToString("dd/MM/yyyy");
            if (showDataTermino)
                wdtDataTermino.Text = dataTermino.ToString("dd/MM/yyyy");
        }

        protected void btnGravar_Click(object sender, System.EventArgs e)
        {
            bool checkProcess = false;

            try
            {
                if (listBxConjuntoEmpregado.SelectedItem.Text.IndexOf("Conj - ") != -1)
                {
                    foreach (ListItem itemEmpregadoConjunto in lsbEmpregados.Items)
                        if (itemEmpregadoConjunto.Selected)
                        {
                            ConjuntoEmpregado conjuntoEmpregado = new ConjuntoEmpregado();
                            conjuntoEmpregado.Find("IdConjunto=" + listBxConjuntoEmpregado.SelectedValue
                                + " AND IdEmpregado=" + itemEmpregadoConjunto.Value);

                            conjuntoEmpregado.DataInicio = Convert.ToDateTime(wdtDataInicio.Text);
                            conjuntoEmpregado.DataTermino = Convert.ToDateTime(wdtDataTermino.Text);

                            if (!checkProcess)
                            {
                                conjuntoEmpregado.UsuarioProcessoRealizado = "Edição do Período para o Conjunto associado a um ou mais Empregados";
                                checkProcess = true;
                            }

                            conjuntoEmpregado.UsuarioId = System.Convert.ToInt32(lbl_Id_Usuario.Text);
                            conjuntoEmpregado.Save();
                        }

                    MsgBox1.Show("Ordem de Serviço", "O Período para o Conjunto selecionado foi salvo com sucesso para o(s) Empregado(s) selecionado(s)!", null,
                            new EO.Web.MsgBoxButton("OK"));

                }
                else if (listBxConjuntoEmpregado.SelectedItem.Text.IndexOf("Proc - ") != -1)
                {
                    foreach (ListItem itemEmpregadoProcedimento in lsbEmpregados.Items)
                        if (itemEmpregadoProcedimento.Selected)
                        {
                            ProcedimentoEmpregado procedimentoEmpregado = new ProcedimentoEmpregado();
                            procedimentoEmpregado.Find("IdProcedimento=" + listBxConjuntoEmpregado.SelectedValue
                                + " AND IdEmpregado=" + itemEmpregadoProcedimento.Value);

                            procedimentoEmpregado.DataInicio = Convert.ToDateTime(wdtDataInicio.Text);
                            procedimentoEmpregado.DataTermino = Convert.ToDateTime(wdtDataTermino.Text);

                            if (!checkProcess)
                            {
                                procedimentoEmpregado.UsuarioProcessoRealizado = "Edição do Período para o Procedimento associado a um ou mais Empregados";
                                checkProcess = true;
                            }

                            procedimentoEmpregado.UsuarioId = System.Convert.ToInt32(lbl_Id_Usuario.Text);
                            procedimentoEmpregado.Save();
                        }

                    MsgBox1.Show("Ordem de Serviço", "O Período para o Procedimento selecionado foi salvo com sucesso para o(s) Empregado(s) selecionado(s)!", null,
                            new EO.Web.MsgBoxButton("OK"));

                }
            }
            catch (Exception ex)
            {
                MsgBox1.Show("Ordem de Serviço", ex.Message, null,
                        new EO.Web.MsgBoxButton("OK"));

            }
        }



        protected void lkbOrdemServico_Grupo_Click(object sender, System.EventArgs e)
        {
            //bool ListaProc = false, POP = false;
            //bool POPGenerico = false;
            int numSelEmpreg = 0;
            Guid strAux = Guid.NewGuid();

            string xCabecalho = "ORDEM DE SERVIÇO";

            if ( chk_Analise_Risco.Checked == true)
                xCabecalho = "ANÁLISE DE RISCO";



            foreach (ListItem Iempregado in lsbEmpregados.Items)
                if (Iempregado.Selected)
                    numSelEmpreg += 1;

            try
            {
                if (numSelEmpreg < 1)
                    throw new Exception("É necessário selecionar pelo menos um empregado antes de imprimir a OS!");

                                

                Session["OSEmpregados"] = "";

                Entities.Usuario user = (Entities.Usuario)Session["usuarioLogado"];
                Guid strAux2 = Guid.NewGuid();

                int xItems = 0;
                string xEmpregados = "";

                //lsbEmpregados.Items[0].Selected
                for (int zCont = 0; zCont < lsbEmpregados.Items.Count; zCont++)
                {
                    if (lsbEmpregados.Items[zCont].Selected == true)
                    {
                        xEmpregados = xEmpregados + "|" + lsbEmpregados.Items[zCont].Value.ToString();
                        xItems++;
                    }
                }

                xEmpregados = xEmpregados + "|";

                System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");

                Session["OSEmpregados"] = xEmpregados;

                OpenReport("Documentos", "OSCompleto.aspx?IliteraSystem=" + strAux2.ToString() + strAux2.ToString()
                        + "&IdEmpresa=" + Session["Empresa"].ToString() + "&Impressao=H" + "&OSData=" + cld_OS.SelectedDate.ToString("dd/MM/yyyy", ptBr) + "&Items=" + xItems.ToString() + "&Cabec=" + xCabecalho, "OSCompleto");





            }
            catch (Exception ex)
            {
                MsgBox1.Show("Ordem de Serviço", ex.Message, null,
                        new EO.Web.MsgBoxButton("OK"));
            }


        }



        protected void lkbOrdemServico_GrupoV_Click(object sender, System.EventArgs e)
        {
            //bool ListaProc = false, POP = false;
            //bool POPGenerico = false;
            int numSelEmpreg = 0;
            Guid strAux = Guid.NewGuid();

            string xCabecalho = "ORDEM DE SERVIÇO";

            if (chk_Analise_Risco.Checked == true)
                xCabecalho = "ANÁLISE DE RISCO";


            foreach (ListItem Iempregado in lsbEmpregados.Items)
                if (Iempregado.Selected)
                    numSelEmpreg += 1;

            try
            {
                if (numSelEmpreg < 1)
                    throw new Exception("É necessário selecionar pelo menos um empregado antes de imprimir a OS!");



                Session["OSEmpregados"] = "";

                Entities.Usuario user = (Entities.Usuario)Session["usuarioLogado"];
                Guid strAux2 = Guid.NewGuid();

                int xItems = 0;
                string xEmpregados = "";

                //lsbEmpregados.Items[0].Selected
                for (int zCont = 0; zCont < lsbEmpregados.Items.Count; zCont++)
                {
                    if (lsbEmpregados.Items[zCont].Selected == true)
                    {
                        xEmpregados = xEmpregados + "|" + lsbEmpregados.Items[zCont].Value.ToString();
                        xItems++;
                    }
                }

                xEmpregados = xEmpregados + "|";

                System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");

                Session["OSEmpregados"] = xEmpregados;

                OpenReport("Documentos", "OSCompleto.aspx?IliteraSystem=" + strAux2.ToString() + strAux2.ToString()
                        + "&IdEmpresa=" + Session["Empresa"].ToString() + "&Impressao=V" + "&OSData=" + cld_OS.SelectedDate.ToString("dd/MM/yyyy", ptBr) + "&Items=" + xItems.ToString() + "&Cabec=" + xCabecalho, "OSCompleto");





            }
            catch (Exception ex)
            {
                MsgBox1.Show("Ordem de Serviço", ex.Message, null,
                        new EO.Web.MsgBoxButton("OK"));
            }


        }

        protected void lkbOrdemServico_Click(object sender, System.EventArgs e)
        {
            bool ListaProc = false, POP = false, POPGenerico = false;
            int numSelEmpreg = 0;
            Guid strAux = Guid.NewGuid();

            string xCabecalho = "ORDEM DE SERVIÇO";

            if (chk_Analise_Risco.Checked == true)
                xCabecalho = "ANÁLISE DE RISCO";


            foreach (ListItem Iempregado in lsbEmpregados.Items)
                if (Iempregado.Selected)
                    numSelEmpreg += 1;

            System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");

            try
            {
                if (!numSelEmpreg.Equals(1))
                    throw new Exception("É necessário selecionar um único empregado antes de imprimir a OS!");

                if (listBxConjuntoEmpregado.Items.Count.Equals(0))
                    throw new Exception("É necessário adicionar pelo menos 1 Conjunto ou 1 Procedimento para o empregado antes de imprimir a OS!");

                if (ddlTipo.SelectedValue.Equals("1"))
                {
                    
                    OpenReport("OrdemDeServico", "RptIntroducaoOS.aspx?IliteraSystem=" + strAux.ToString() + strAux.ToString()
                        + "&IdEmpregado=" + lsbEmpregados.SelectedValue + "&IdUsuario=" + lbl_Id_Usuario.Text + "&IdEmpresa=" + lbl_Id_Empresa.Text + "&DataOS=" + cld_OS.SelectedDate.ToString("dd/MM/yyyy", ptBr),
                        "RptIntroducaoOS");
                }
                else
                {
                    foreach (ListItem IConjunto in listBxConjuntoEmpregado.Items)
                    {
                        if (IConjunto.Text.IndexOf("Conj - ") != -1)
                        {
                            ArrayList alConjProc = new ConjuntoProcedimento().Find("IdConjunto=" + IConjunto.Value
                                + " AND IdConjunto IN (SELECT IdConjunto FROM ConjuntoEmpregado WHERE IdEmpregado=" + lsbEmpregados.SelectedValue
                                + " AND (DataTermino IS NULL OR (DataInicio<=convert( smalldatetime, '" + cld_OS.SelectedDate.ToString("dd/MM/yyyy", ptBr) + "',103 ) AND DataTermino>=convert( smalldatetime,'" + cld_OS.SelectedDate.ToString("dd/MM/yyyy") + "',103) )))");

                            foreach (ConjuntoProcedimento ConjProc in alConjProc)
                            {
                                ListaProc = true;

                                ConjProc.IdProcedimento.Find();

                                if (ConjProc.IdProcedimento.IndTipoProcedimento == TipoProcedimento.Instrutivo)
                                    POPGenerico = true;
                                else
                                    POP = true;
                            }
                        }
                        else if (IConjunto.Text.IndexOf("Proc - ") != -1)
                        {

                            
                            ProcedimentoEmpregado procedimentoEmpregado = new ProcedimentoEmpregado();
                            procedimentoEmpregado.Find("IdEmpregado=" + lsbEmpregados.SelectedValue
                                + " AND IdProcedimento=" + IConjunto.Value
                                + " AND (DataTermino IS NULL OR (DataInicio<=convert( smalldatetime,'" + cld_OS.SelectedDate.ToString("dd/MM/yyyy", ptBr) + "',103) AND DataTermino>=convert( smalldatetime,'" + cld_OS.SelectedDate.ToString("dd/MM/yyyy") + "',103)))");

                            if (procedimentoEmpregado.Id != 0)
                            {
                                ListaProc = true;

                                procedimentoEmpregado.IdProcedimento.Find();

                                if (procedimentoEmpregado.IdProcedimento.IndTipoProcedimento == TipoProcedimento.Instrutivo)
                                    POPGenerico = true;
                                else
                                    POP = true;
                            }
                        }
                    }

                    

                    switch (ddlTipo.SelectedValue)
                    {
                        case "0":
                            if (!ListaProc)
                                throw new Exception("Não é possível imprimir a OS! Não há procedimentos cadastrados com o período válido para a elaboração da Ordem de Serviço!");

                            OpenReport("OrdemDeServico", "RptOrdemDeServico.aspx?IliteraSystem=" + strAux.ToString() + strAux.ToString()
                                + "&IdEmpregado=" + lsbEmpregados.SelectedValue + "&IdUsuario=" + lbl_Id_Usuario.Text + "&IdEmpresa=" + lbl_Id_Empresa.Text + "&DataOS=" + cld_OS.SelectedDate.ToString("dd/MM/yyyy", ptBr).ToString() + "&Cabec=" + xCabecalho,
                                "OrdemServico");
                            break;
                        case "3":
                            if (!POP)
                                throw new Exception("Não existem Procedimentos Específicos com o período válido para serem impressos!");

                            OpenReport("OrdemDeServico", "RptPOP.aspx?IliteraSystem=" + strAux.ToString() + strAux.ToString()
                                + "&IdEmpregado=" + lsbEmpregados.SelectedValue + "&IdUsuario=" + lbl_Id_Usuario.Text + "&IdEmpresa=" + lbl_Id_Empresa.Text + "&DataOS=" + cld_OS.SelectedDate.ToString("dd/MM/yyyy", ptBr).ToString(),
                                "RptPOP");
                            break;
                        case "2":
                            if (!POPGenerico)
                                throw new Exception("Não existem Procedimentos Instrutivos com o período válido para serem impressos!");

                            OpenReport("OrdemDeServico", "RptPOPGenerico.aspx?IliteraSystem=" + strAux.ToString() + strAux.ToString()
                                + "&IdEmpregado=" + lsbEmpregados.SelectedValue + "&IdUsuario=" + lbl_Id_Usuario.Text + "&IdEmpresa=" + lbl_Id_Empresa.Text + "&DataOS=" + cld_OS.SelectedDate.ToString("dd/MM/yyyy", ptBr),
                                "RptPOPGenerico");
                            break;
                        case "4":
                            if (!ListaProc)
                                throw new Exception("Não há Procedimentos cadastrados com o período válido para a impressão da listagem de procedimentos!");

                            OpenReport("OrdemDeServico", "RptListaProcOS.aspx?IliteraSystem=" + strAux.ToString() + strAux.ToString()
                                + "&IdEmpregado=" + lsbEmpregados.SelectedValue + "&IdUsuario=" + lbl_Id_Usuario.Text + "&IdEmpresa=" + lbl_Id_Empresa.Text + "&DataOS=" + cld_OS.SelectedDate.ToString("dd/MM/yyyy", ptBr).ToString(),
                                "RptListaProcOS");
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                MsgBox1.Show("Ordem de Serviço", ex.Message, null,
                        new EO.Web.MsgBoxButton("OK"));

            }
        }



        protected void lkbOrdemServicoV_Click(object sender, System.EventArgs e)
        {
            bool ListaProc = false, POP = false, POPGenerico = false;
            int numSelEmpreg = 0;
            Guid strAux = Guid.NewGuid();

            string xCabecalho = "ORDEM DE SERVIÇO";

            if (chk_Analise_Risco.Checked == true)
                xCabecalho = "ANÁLISE DE RISCO";


            foreach (ListItem Iempregado in lsbEmpregados.Items)
                if (Iempregado.Selected)
                    numSelEmpreg += 1;

            System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");

            try
            {
                if (!numSelEmpreg.Equals(1))
                    throw new Exception("É necessário selecionar um único empregado antes de imprimir a OS!");

                if (listBxConjuntoEmpregado.Items.Count.Equals(0))
                    throw new Exception("É necessário adicionar pelo menos 1 Conjunto ou 1 Procedimento para o empregado antes de imprimir a OS!");

                if (ddlTipo.SelectedValue.Equals("1"))
                {

                    OpenReport("OrdemDeServico", "RptIntroducaoOS.aspx?IliteraSystem=" + strAux.ToString() + strAux.ToString()
                        + "&IdEmpregado=" + lsbEmpregados.SelectedValue + "&IdUsuario=" + lbl_Id_Usuario.Text + "&IdEmpresa=" + lbl_Id_Empresa.Text + "&DataOS=" + cld_OS.SelectedDate.ToString("dd/MM/yyyy", ptBr),
                        "RptIntroducaoOS");
                }
                else
                {
                    foreach (ListItem IConjunto in listBxConjuntoEmpregado.Items)
                    {
                        if (IConjunto.Text.IndexOf("Conj - ") != -1)
                        {
                            ArrayList alConjProc = new ConjuntoProcedimento().Find("IdConjunto=" + IConjunto.Value
                                + " AND IdConjunto IN (SELECT IdConjunto FROM ConjuntoEmpregado WHERE IdEmpregado=" + lsbEmpregados.SelectedValue
                                + " AND (DataTermino IS NULL OR (DataInicio<=convert( smalldatetime, '" + cld_OS.SelectedDate.ToString("dd/MM/yyyy", ptBr) + "',103 ) AND DataTermino>=convert( smalldatetime,'" + cld_OS.SelectedDate.ToString("dd/MM/yyyy") + "',103) )))");

                            foreach (ConjuntoProcedimento ConjProc in alConjProc)
                            {
                                ListaProc = true;

                                ConjProc.IdProcedimento.Find();

                                if (ConjProc.IdProcedimento.IndTipoProcedimento == TipoProcedimento.Instrutivo)
                                    POPGenerico = true;
                                else
                                    POP = true;
                            }
                        }
                        else if (IConjunto.Text.IndexOf("Proc - ") != -1)
                        {


                            ProcedimentoEmpregado procedimentoEmpregado = new ProcedimentoEmpregado();
                            procedimentoEmpregado.Find("IdEmpregado=" + lsbEmpregados.SelectedValue
                                + " AND IdProcedimento=" + IConjunto.Value
                                + " AND (DataTermino IS NULL OR (DataInicio<=convert( smalldatetime,'" + cld_OS.SelectedDate.ToString("dd/MM/yyyy", ptBr) + "',103) AND DataTermino>=convert( smalldatetime,'" + cld_OS.SelectedDate.ToString("dd/MM/yyyy") + "',103)))");

                            if (procedimentoEmpregado.Id != 0)
                            {
                                ListaProc = true;

                                procedimentoEmpregado.IdProcedimento.Find();

                                if (procedimentoEmpregado.IdProcedimento.IndTipoProcedimento == TipoProcedimento.Instrutivo)
                                    POPGenerico = true;
                                else
                                    POP = true;
                            }
                        }
                    }



                    switch (ddlTipo.SelectedValue)
                    {
                        case "0":
                            if (!ListaProc)
                                throw new Exception("Não é possível imprimir a OS! Não há procedimentos cadastrados com o período válido para a elaboração da Ordem de Serviço!");

                            OpenReport("OrdemDeServico", "RptOrdemDeServico.aspx?IliteraSystem=" + strAux.ToString() + strAux.ToString()
                                + "&IdEmpregado=" + lsbEmpregados.SelectedValue + "&Impressao=V" + "&IdUsuario=" + lbl_Id_Usuario.Text + "&IdEmpresa=" + lbl_Id_Empresa.Text + "&DataOS=" + cld_OS.SelectedDate.ToString("dd/MM/yyyy", ptBr).ToString() + "&Cabec=" + xCabecalho,
                                "OrdemServico");
                            break;
                        case "3":
                            if (!POP)
                                throw new Exception("Não existem Procedimentos Específicos com o período válido para serem impressos!");

                            OpenReport("OrdemDeServico", "RptPOP.aspx?IliteraSystem=" + strAux.ToString() + strAux.ToString()
                                + "&IdEmpregado=" + lsbEmpregados.SelectedValue + "&IdUsuario=" + lbl_Id_Usuario.Text + "&IdEmpresa=" + lbl_Id_Empresa.Text + "&DataOS=" + cld_OS.SelectedDate.ToString("dd/MM/yyyy", ptBr).ToString(),
                                "RptPOP");
                            break;
                        case "2":
                            if (!POPGenerico)
                                throw new Exception("Não existem Procedimentos Instrutivos com o período válido para serem impressos!");

                            OpenReport("OrdemDeServico", "RptPOPGenerico.aspx?IliteraSystem=" + strAux.ToString() + strAux.ToString()
                                + "&IdEmpregado=" + lsbEmpregados.SelectedValue + "&IdUsuario=" + lbl_Id_Usuario.Text + "&IdEmpresa=" + lbl_Id_Empresa.Text + "&DataOS=" + cld_OS.SelectedDate.ToString("dd/MM/yyyy", ptBr),
                                "RptPOPGenerico");
                            break;
                        case "4":
                            if (!ListaProc)
                                throw new Exception("Não há Procedimentos cadastrados com o período válido para a impressão da listagem de procedimentos!");

                            OpenReport("OrdemDeServico", "RptListaProcOS.aspx?IliteraSystem=" + strAux.ToString() + strAux.ToString()
                                + "&IdEmpregado=" + lsbEmpregados.SelectedValue + "&IdUsuario=" + lbl_Id_Usuario.Text + "&IdEmpresa=" + lbl_Id_Empresa.Text + "&DataOS=" + cld_OS.SelectedDate.ToString("dd/MM/yyyy", ptBr).ToString(),
                                "RptListaProcOS");
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                MsgBox1.Show("Ordem de Serviço", ex.Message, null,
                        new EO.Web.MsgBoxButton("OK"));

            }
        }


        protected void OpenReport(string directory, string fileAndQuery, string ReportName)
        {
            this.OpenReport(directory, fileAndQuery, ReportName, true);
        }

        protected void OpenReport(string directory, string fileAndQuery, string ReportName, bool useDirectoryForLocalProcess)
        {
            StringBuilder st = new StringBuilder();

            Guid strAux = Guid.NewGuid();

            string valueProcess = "Local";

            if (valueProcess.Equals("Remote"))
                //st.Append("void(window.open('http://report.ilitera.net/index.aspx?Identity=" + strAux.ToString() + "opsa" + strAux.ToString()
                //    + "&Key=" + strAux.ToString() + "MestraNet" + strAux.ToString()
                //    + "&PathAndQuery=" + HttpUtility.UrlEncode(directory + "/" + fileAndQuery) + "', '" + ReportName + "', 'scrollbars=yes, resizable=yes, toolbar=no, addressbar=no, menubar=no, width=800px, height=600px'));");
                System.Diagnostics.Debug.WriteLine("");
            else if (valueProcess.Equals("Local"))
            {
                if (useDirectoryForLocalProcess)
                {
                    //st.Append("void(window.open('../" + directory + "/" + fileAndQuery + "', '" + ReportName + "', 'scrollbars=yes, resizable=yes, toolbar=no, addressbar=no, menubar=no, width=800px, height=600px'));");
                    //st.AppendFormat("void(window.open('../{0}/{1}', '{2}','scrollbars=yes, resizable=yes, toolbar=no, addressbar=no, menubar=no, width=800px, height=600px'));", directory, fileAndQuery, ReportName);
                    st.AppendFormat("void(window.open('{1}', '{2}','scrollbars=yes, resizable=yes, toolbar=no, addressbar=no, menubar=no, width=800px, height=600px'));", directory, fileAndQuery, ReportName);
                }
                else
                {
                    //st.Append("void(window.open('" + fileAndQuery + "', '" + ReportName + "', 'scrollbars=yes, resizable=yes, toolbar=no, addressbar=no, menubar=no, width=800px, height=600px'));");
                    st.AppendFormat("void(window.open('{0}','{1}', 'scrollbars=yes, resizable=yes, toolbar=no, addressbar=no, menubar=no, width=800px, height=600px'));", fileAndQuery, ReportName);
                }
            }

            ScriptManager.RegisterStartupScript(this, this.GetType(), String.Format("OpenReport{0}", ReportName), st.ToString(), true);
        }


        protected void cmb_GHE_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulaLsbEmpregados();
        }

        protected void cmb_Setor_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulaLsbEmpregados();
        }



    }
}
