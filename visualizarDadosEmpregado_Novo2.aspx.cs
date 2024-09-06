using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using System.Text;
using System.Data.SqlClient;
using Entities;
using Facade;

using System.Net.Mail;
using System.Net;

using System.Text.RegularExpressions;
using BLL;

using Ilitera.Common;
using System.Reflection;

namespace Ilitera.Net
{
    public partial class visualizarDadosEmpregado_Novo2 : System.Web.UI.Page
    {
        //#region eventos


        private Int32 xIdEmpresa;
        private Int32 xIdUsuario;

        
        private byte[] zFoto;

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
        //private void InitializeComponent()
        //{

        //}
        //#endregion


        protected void InicializaWebPageObjects()
        {

        }

        protected void Page_Load(object sender, EventArgs e)
        {

            //wtClassificacaoFuncional.Visible = true;

            //wtClassificacaoFuncional.TabIndex = 0;


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


                //verificar se transferência deve ser ativada
                cmd_Transferir.Enabled = false;

                try
                {
                    string FormKey = "Transf_aspx";

                    Ilitera.Common.Funcionalidade funcionalidade = new Ilitera.Common.Funcionalidade();
                    funcionalidade.Find("ClassName='" + FormKey + "'");

                    Entities.Usuario xUser = (Entities.Usuario)Session["usuarioLogado"];
                    Ilitera.Common.Usuario.Permissao_Web(xUser.IdUsuario, funcionalidade.Id);

                    cmd_Transferir.Enabled = true;
                  
                }
                catch (Exception ex)
                {
                    cmd_Transferir.Enabled = false;
                }


                if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().IndexOf("Prajna") > 0)
                {
                    chk_Admissao_Origem.Checked = false;
                }
                else
                {
                    chk_Admissao_Origem.Checked = true;
                }


                // InicializaWebPageObjects();

                txt_D1.Text = "01/01/" + DateTime.Now.ToString("yyyy").Trim();
                txt_D2.Text = DateTime.Now.ToString("dd/MM/yyyy").Trim();


                txtMateriais.Text = DateTime.Now.ToString("dd/MM/yyyy").Trim();
                txtQtde.Text = "1";

                cmd_Atualizar.Attributes.Add("onClick", "javascript:return confirm('Você deseja realmente salvar alterações de dados ?');");
                cmd_Transferir.Attributes.Add("onClick", "javascript:return confirm('Você deseja realmente realizar a transferência deste colaborador ?');");

                Entities.Usuario user = (Entities.Usuario)Session["usuarioLogado"];

                xIdEmpresa = System.Convert.ToInt32(Session["Empresa"]);
                xIdUsuario = user.IdUsuario;

                Ilitera.Opsa.Data.Cliente xCliente = new Ilitera.Opsa.Data.Cliente();
                xCliente.Find( System.Convert.ToInt32(Session["Empresa"]));

                if ( xCliente.Bloquear_Novo_Setor == true )
                {
                    txtSetor.Enabled = false;
                }
                else
                {
                    txtSetor.Enabled = true;
                }

                if (xCliente.Bloquear_Novo_Cargo == true)
                {                    
                    txtCargo.Enabled = false;
                    txtFuncao.Enabled = false;
                    txtJornada.Enabled = false;
                    txtLocalTrabalho.Enabled = false;
                }
                else
                {                    
                    txtCargo.Enabled = true;
                    txtFuncao.Enabled = true;
                    txtJornada.Enabled = true;
                    txtLocalTrabalho.Enabled = true;
                }


                lbl_Id_Empresa.Text = xIdEmpresa.ToString().Trim();
                lbl_Id_Usuario.Text = xIdUsuario.ToString().Trim();

                selecionarDadosCadastraisEmpregado();
                PopulaGrid();

                PopulaUniforme();
                Carga_eSocial();

                PopulaTransferencia();

                cmb_Setor_SelectedIndexChanged(sender, e);

                if (xCliente.NomeAbreviado.ToUpper().IndexOf("CALOI")>=0)
                {
                    Entities.Usuario usuario = (Entities.Usuario)Session["usuarioLogado"];

                    if ( usuario.NomeUsuario.ToUpper().IndexOf("MAYAN") >=0 || 
                         usuario.NomeUsuario.ToUpper().IndexOf("SUZANA.COELHO") >= 0 ||
                         usuario.NomeUsuario.ToUpper().IndexOf("LETICIA") >= 0 )
                    {
                        txtDataDemissao.ReadOnly = false;
                    }
                    else
                    {
                        txtDataDemissao.ReadOnly = true;
                    }
                }


                if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("DAITI") > 0)
                {
                    chk_apt_Altura.Enabled = false;
                    chk_apt_Confinado.Enabled = false;
                    chk_apt_Transportes.Enabled = false;
                    chk_apt_Submerso.Enabled = false;
                    chk_apt_Eletricidade.Enabled = false;
                    chk_apt_Aquaviarios.Enabled = false;
                    chk_Apt_Alimento.Enabled = false;
                    chk_Apt_Brigadista.Enabled = false;
                    chk_Apt_Socorrista.Enabled = false;
                    chk_Apt_Respirador.Enabled = false;
                    chk_Apt_Radiacao.Enabled = false;
                }


            }
        }


       

        protected void cmd_Absentismo_Click(object sender, EventArgs e)
        {
            Session["Retorno"] = "1";
            Response.Redirect("~/PCMSO/ListaAcidentes2.aspx?Tipo=2&Tela=1");

        }




        protected void cmdGHE_1_Click(object sender, EventArgs e)
        {
            int xAno1;
            int xAno2;

            //lst_1.Visible = true;
            //lst_Sel_1.Visible = true;
            //cmd_Add_1.Visible = true;
            //cmd_Remove_1.Visible = true;

            lst_PPRA.Visible = true;
            lbl_PPRA0.Visible = true;
            lst_11.Visible = true;
            lst_Sel_11.Visible = true;
            cmd_Add11.Visible = true;
            cmd_Remove11.Visible = true;
            lbl_Selecao.Visible = true;
            lbl_Selecionado.Visible = true;

            xAno1 = 1990;
            xAno2 = 2050;

            //pegar ano inicial e final da classif.funcional, para exibir os laudos apenas deste invervalo de anos
            if (txtInicioFuncao.Text.Trim() != "")
            {
                xAno1 = System.Convert.ToInt32(txtInicioFuncao.Text.Substring(txtInicioFuncao.Text.Length - 4)) - 1;
            }

            if (txtTerminoFuncao.Text.Trim() != "")
            {
                xAno2 = System.Convert.ToInt32(txtTerminoFuncao.Text.Trim().Substring(txtTerminoFuncao.Text.Trim().Length - 4));
            }


            Ilitera.Data.PPRA_EPI xGHE = new Ilitera.Data.PPRA_EPI();

            //DataSet xdS = xGHE.Trazer_Laudos_GHEs(xAno1, xAno2, System.Convert.ToInt32(Session["Empresa"].ToString()));

            DataSet xdS = new DataSet();

            if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("PRAJNA") > 0)
            {
                xdS = xGHE.Trazer_Laudos_GHEs(xAno1, xAno2, System.Convert.ToInt32(lbl_Local_Trabalho.Text), "S");
            }
            else
            {
                xdS = xGHE.Trazer_Laudos_GHEs(xAno1, xAno2, System.Convert.ToInt32(lbl_Local_Trabalho.Text), "N");
            }

            lst_1.Items.Clear();
            lst_1_ID.Items.Clear();

            lst_Id_PPRA.Items.Clear();
            lst_PPRA.Items.Clear();
            Boolean fLoc = false;

            foreach (DataRow row in xdS.Tables[0].Rows)
            {
                lst_1.Items.Add(row["Laudo"].ToString() + "  " + row["GHE"].ToString());
                lst_1_ID.Items.Add(row["nId_Func"].ToString());

                fLoc = false;

                for (int fCont = 0; fCont < lst_PPRA.Items.Count; fCont++)
                {

                    if (lst_PPRA.Items[fCont].ToString().Trim() == row["Laudo"].ToString().Trim())
                    {
                        fLoc = true;
                        break;
                    }
                }

                if (fLoc == false)
                {
                    lst_PPRA.Items.Add(row["Laudo"].ToString().Trim());
                    lst_Id_PPRA.Items.Add(row["nId_Laud_Tec"].ToString().Trim());
                }


            }



            //carregar GHEs para a classif.funcional
            Ilitera.Data.PPRA_EPI xGHE2 = new Ilitera.Data.PPRA_EPI();

            DataSet xdS2 = xGHE.Trazer_Laudos_GHEs_Salvos(System.Convert.ToInt32(lbl_Id.Text));

            lst_Sel_1.Items.Clear();
            lst_Sel_1_Cop.Items.Clear();
            lst_Sel_1_Id.Items.Clear();

            foreach (DataRow row in xdS2.Tables[0].Rows)
            {
                lst_Sel_1.Items.Add(row["Laudo"].ToString() + "  " + row["GHE"].ToString());
                lst_Sel_1_Cop.Items.Add(row["Laudo"].ToString() + "  " + row["GHE"].ToString());

                lst_Sel_1_Id.Items.Add(row["nId_Func"].ToString());
            }

            //posicionar no primeiro elemento
            if (lst_PPRA.Items.Count > 0)
            {
                lst_PPRA.SelectedIndex = 0;

                object xsender = new object();
                EventArgs xe = new EventArgs();
                lst_PPRA_SelectedIndexChanged(xsender, xe);
            }


        }




       

    



        protected void lst_1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }



        protected void cmb_CBO_SelectedIndexChanged(object sender, EventArgs e)
        {

        }





        protected void txtNomeEmpregado_TextChanged(object sender, EventArgs e)
        {

        }




        private void PopulaGrid()
        {

            DataRow newRow;
            string xCBO;


            cmb_Setor.Items.Clear();
            cmb_SetorD.Items.Clear();
            cmb_SetorID.Items.Clear();
            cmb_CBOID.Items.Clear();


            //carregar combos de setor, funcao e cargo
            cmb_Setor1.Items.Clear();

            cmb_Cargo1.Items.Clear();

            cmb_Funcao1.Items.Clear();

            cmb_Jornada.Items.Clear();



            Ilitera.Data.PPRA_EPI xGHE = new Ilitera.Data.PPRA_EPI();

            DataSet dS1 = xGHE.Carregar_Setores(System.Convert.ToInt32(Session["Empresa"].ToString()));

            cmb_Setor1.DataSource = dS1;
            cmb_Setor1.DataValueField = "nID_SETOR";
            cmb_Setor1.DataTextField = "tNO_STR_EMPR";
            cmb_Setor1.DataBind();


            Ilitera.Opsa.Data.Cliente rCliente = new Ilitera.Opsa.Data.Cliente();
            rCliente.Find(System.Convert.ToInt32(Session["Empresa"].ToString()));

            if (rCliente.NomeAbreviado.ToUpper().IndexOf("VITERRA") >= 0)
            {
                DataSet dS2 = xGHE.Carregar_Funcoes_Viterra(System.Convert.ToInt32(Session["Empresa"].ToString()));
                cmb_Funcao1.DataSource = dS2;
                cmb_Funcao1.DataValueField = "IdFuncao";
                cmb_Funcao1.DataTextField = "NomeFuncao";
                cmb_Funcao1.DataBind();
            }
            else
            {
                DataSet dS2 = xGHE.Carregar_Funcoes(System.Convert.ToInt32(Session["Empresa"].ToString()));
                cmb_Funcao1.DataSource = dS2;
                cmb_Funcao1.DataValueField = "IdFuncao";
                cmb_Funcao1.DataTextField = "NomeFuncao";
                cmb_Funcao1.DataBind();
            }

            DataSet dS3 = xGHE.Carregar_Cargos(System.Convert.ToInt32(Session["Empresa"].ToString()));
            cmb_Cargo1.DataSource = dS3;
            cmb_Cargo1.DataValueField = "nID_CARGO";
            cmb_Cargo1.DataTextField = "tNO_CARGO";
            cmb_Cargo1.DataBind();


            DataTable table = new DataTable("Result");
            table.Columns.Add("IdClassificacaoFuncional", Type.GetType("System.Int32"));
            table.Columns.Add("Funcao", Type.GetType("System.String"));
            table.Columns.Add("Setor", Type.GetType("System.String"));
            table.Columns.Add("InicioFuncao", Type.GetType("System.String"));
            table.Columns.Add("TerminoFuncao", Type.GetType("System.String"));
            table.Columns.Add("tCentro_Custo", Type.GetType("System.String"));
            table.Columns.Add("NomeAlocado", Type.GetType("System.String"));
            

            DataSet m_ds = new DataSet();


            m_ds.Tables.Add(table);

            var retorno = EmpregadoFacade.RetornarClassificacaoFuncionalEmpregado(Convert.ToInt32(Session["Empregado"]));

            //string where = "nID_EMPREGADO=" + Session["Empregado"].ToString() + " ORDER BY hDT_INICIO";

            //gridEmpregados.DataSource = new Mestra.Opsa.Data.EmpregadoFuncao().Get(where);

            for (int zCont = 0; zCont < retorno.Count; zCont++)
            {

                newRow = m_ds.Tables["Result"].NewRow();

                newRow["Funcao"] = retorno[zCont].Funcao.ToString();
                newRow["Setor"] = retorno[zCont].Setor.ToString();
                newRow["tCentro_Custo"] = retorno[zCont].tCentro_Custo.ToString();
                
                newRow["InicioFuncao"] = System.Convert.ToDateTime(retorno[zCont].InicioFuncao).ToString("dd/MM/yyyy").Substring(0, 10);

                if (System.Convert.ToDateTime(retorno[zCont].TerminoFuncao).ToString("dd/MM/yyyy").Substring(0, 10) == "01/01/0001")
                {
                    newRow["TerminoFuncao"] = " ";
                }
                else
                {
                    newRow["TerminoFuncao"] = System.Convert.ToDateTime(retorno[zCont].TerminoFuncao).ToString("dd/MM/yyyy").Substring(0, 10);
                }

                string where = "nID_EMPREGADO=" + retorno[zCont].IdEmpregado.ToString() + " and convert( char(10),hDT_Inicio,103 ) = '" + System.Convert.ToDateTime(retorno[zCont].InicioFuncao).ToString("dd/MM/yyyy").Substring(0, 10) + "' ORDER BY hDT_INICIO";

                Ilitera.Opsa.Data.EmpregadoFuncao xEmpregado = new Ilitera.Opsa.Data.EmpregadoFuncao();

                DataSet dS = xEmpregado.Get(where);

                DataRow xRow = dS.Tables[0].Rows[0];
                int id = System.Convert.ToInt32(xRow[0].ToString());
                             


                newRow["IdClassificacaoFuncional"] = id.ToString();

                Ilitera.Opsa.Data.Cliente xCliente = new Ilitera.Opsa.Data.Cliente();
                xCliente.Find(System.Convert.ToInt32(xRow[12].ToString()));

                newRow["NomeAlocado"] = xCliente.NomeAbreviado;

                m_ds.Tables["Result"].Rows.Add(newRow);


                Ilitera.Opsa.Data.EmpregadoFuncao emprFuncao;
                emprFuncao = new Ilitera.Opsa.Data.EmpregadoFuncao();
                emprFuncao.Find(id);


                //emprFuncao.nID_FUNCAO.NumeroCBO


                lbl_Id_1.Text = id.ToString();
                //cmdGHE_1.Visible = true;


                if (rCliente.NomeAbreviado.ToUpper().IndexOf("VITERRA") >= 0 )
                {
                    //procurar função + codigo
                    emprFuncao.nID_FUNCAO.Find();

                    if (emprFuncao.nID_FUNCAO.CodigoFuncao.Trim() != "")
                    {
                        newRow["Funcao"] = emprFuncao.nID_FUNCAO.NomeFuncao + " | " + emprFuncao.nID_FUNCAO.CodigoFuncao;
                    }


                }

                cmb_SetorID.Items.Add(emprFuncao.nID_FUNCAO.Id.ToString());
                cmb_Setor.Items.Add(retorno[zCont].Funcao.ToString());
                cmb_SetorD.Items.Add(retorno[zCont].DescricaoFuncao.ToString());



                xCBO = emprFuncao.GetNumeroCBO(); //.nID_FUNCAO.NumeroCBO.ToString().Trim();

                if (xCBO == "")
                {
                    cmb_CBOID.Items.Add("0");
                }
                else
                {
                    cmb_CBOID.Items.Add(xCBO);
                }



                Ilitera.Data.PPRA_EPI xCBOz = new Ilitera.Data.PPRA_EPI();

                cmb_CBO.DataSource = xCBOz.Lista_CBO("");
                cmb_CBO.DataValueField = "CBO";
                cmb_CBO.DataTextField = "CBONome";
                cmb_CBO.DataBind();

                cmb_CBO.SelectedIndex = -1;



            }


            PopulaComboLocalTrabalho();

            List<Ilitera.Opsa.Data.TempoExposicao> list = new Ilitera.Opsa.Data.TempoExposicao().FindAll<Ilitera.Opsa.Data.TempoExposicao>();
            cmb_Jornada.Items.Clear();
            cmb_Jornada.Items.Add("-");

            foreach (Ilitera.Opsa.Data.TempoExposicao jornada in list)
                cmb_Jornada.Items.Add(jornada.ToString());



            grd_Empregados.DataSource = m_ds;

            grd_Empregados.DataBind();



            lbl_Id.Text = "0";
                       
            txtSetor.Text = "";
            txtCargo.Text = "";
            txtFuncao.Text = "";
            txtJornada.Text = "";
            txtCC.Text = "";
            txtLocalTrabalho.Visible = true;
            txtLocalTrabalho.Text = "";

            txtInicioFuncao.Text = "";
            txtTerminoFuncao.Text = "";

            txtInicioFuncao.ReadOnly = true;
            txtTerminoFuncao.ReadOnly = true;
            txtCargo.ReadOnly = true;
            txtFuncao.ReadOnly = true;
            txtSetor.ReadOnly = true;
            txtJornada.ReadOnly = true;
            txtCC.ReadOnly = true;
            txtLocalTrabalho.ReadOnly = true;


            txtInicioFuncao.BackColor = System.Drawing.ColorTranslator.FromHtml("#DFDFDF");
            txtTerminoFuncao.BackColor = System.Drawing.ColorTranslator.FromHtml("#DFDFDF");
            txtCargo.BackColor = System.Drawing.ColorTranslator.FromHtml("#DFDFDF");
            txtFuncao.BackColor = System.Drawing.ColorTranslator.FromHtml("#DFDFDF");
            txtSetor.BackColor = System.Drawing.ColorTranslator.FromHtml("#DFDFDF");
            txtJornada.BackColor = System.Drawing.ColorTranslator.FromHtml("#DFDFDF");
            txtCC.BackColor = System.Drawing.ColorTranslator.FromHtml("#DFDFDF");
            txtLocalTrabalho.BackColor = System.Drawing.ColorTranslator.FromHtml("#DFDFDF");

            cmb_Setor1.Visible = false;
            cmb_Cargo1.Visible = false;
            cmb_Funcao1.Visible = false;
            cmb_Jornada.Visible = false;
            cmbLocalTrabalho.Visible = false;

            cmb_Setor1.SelectedIndex = 0;
            cmb_Cargo1.SelectedIndex = 0;
            cmb_Funcao1.SelectedIndex = 0;
            cmb_Jornada.SelectedIndex = 0;
            cmbLocalTrabalho.SelectedIndex = 0;

            cmdGHE_1.Visible = false;

            cmd_Editar_Setor_Funcao.Visible = false;

            cmd_Excluir1.Enabled = false;
            cmd_Alterar1.Enabled = true;

            lst_1.Visible = false;
            lst_Sel_1.Visible = false;
            cmd_Add_1.Visible = false;
            cmd_Remove_1.Visible = false;

            lst_PPRA.Visible = false;
            lbl_PPRA0.Visible = false;
            lst_11.Visible = false;
            lst_Sel_11.Visible = false;
            cmd_Add11.Visible = false;
            cmd_Remove11.Visible = false;
            lbl_Selecao.Visible = false;
            lbl_Selecionado.Visible = false;

            return;

        }



        protected void grd_Empregados_ItemCommand(object sender, EO.Web.GridCommandEventArgs e)
        {
            


            string xId = "0";

            //try
            //{
            if (e.Item.Key.ToString().Trim() != "")
            {
                xId = e.Item.Key.ToString();
                lbl_Id.Text = xId;


                //carregar campos para edição

                //se salvar ou cancelar,  recarregar grid ( o grid tem um problema, só responde ao primeiro click no evento )

                cmd_Alterar1.Visible = true;
                cmd_Excluir1.Visible = true;
                cmdGHE_1.Visible = true;
                cmd_Editar_Setor_Funcao.Visible = true;

                lst_1.Visible = false;
                lst_Sel_1.Visible = false;
                cmd_Add_1.Visible = false;
                cmd_Remove_1.Visible = false;

                lst_PPRA.Visible = false;
                lbl_PPRA0.Visible = false;
                lst_11.Visible = false;
                lst_Sel_11.Visible = false;
                cmd_Add11.Visible = false;
                cmd_Remove11.Visible = false;
                lbl_Selecao.Visible = false;
                lbl_Selecionado.Visible = false;

                Ilitera.Opsa.Data.EmpregadoFuncao emprFuncao;
                emprFuncao = new Ilitera.Opsa.Data.EmpregadoFuncao();
                emprFuncao.Find(System.Convert.ToInt32(xId));

                
                txtInicioFuncao.Text = e.Item.Cells[2].Value.ToString().Trim();
                if (e.Item.Cells[3].Value == null)
                {
                    txtTerminoFuncao.Text = "";
                }
                else
                {
                    txtTerminoFuncao.Text = e.Item.Cells[3].Value.ToString().Trim();
                }
                txtCargo.Text = emprFuncao.nID_CARGO.ToString(); //retorno[0].DescricaoCargo.ToString();
                txtFuncao.Text = e.Item.Cells[0].Value.ToString().Trim();
                txtSetor.Text = e.Item.Cells[1].Value.ToString().Trim();


                Ilitera.Opsa.Data.Cliente xCliente = new Ilitera.Opsa.Data.Cliente();
                xCliente.Find(System.Convert.ToInt32(Session["Empresa"]));

                txtLocalTrabalho.Text = emprFuncao.GetLocalDeTrabalho(xCliente);

                for (int zAux = 0; zAux < cmbLocalTrabalho.Items.Count; zAux++)
                {
                    if (cmbLocalTrabalho.Items[zAux].ToString().Trim().ToUpper() == txtLocalTrabalho.Text.Trim().ToUpper())
                    {
                        cmbLocalTrabalho.SelectedIndex = zAux;
                        break;
                    }
                }



                //se selecionou classif.funcional de outro local de trabalho, eu poderia selecionar essa obra como ativa no sistema
                emprFuncao.nID_EMPR.Find();
                lbl_Local_Trabalho.Text = emprFuncao.nID_EMPR.Id.ToString().Trim();




                //carregar jornada
                txtJornada.Text = emprFuncao.nID_TEMPO_EXP.ToString();




                txtCC.Text = e.Item.Cells[4].Value.ToString().Trim();

                txtInicioFuncao.ReadOnly = false;
                txtTerminoFuncao.ReadOnly = false;
                txtCargo.ReadOnly = true;
                txtFuncao.ReadOnly = true;
                txtSetor.ReadOnly = true;
                txtJornada.ReadOnly = true;
                txtCC.ReadOnly = false;
                txtLocalTrabalho.ReadOnly = true;
                txtLocalTrabalho.Visible = true;

                txtInicioFuncao.BackColor = System.Drawing.ColorTranslator.FromHtml("#DFDFDF");
                txtTerminoFuncao.BackColor = System.Drawing.ColorTranslator.FromHtml("#DFDFDF");
                txtCargo.BackColor = System.Drawing.ColorTranslator.FromHtml("#DFDFDF");
                txtFuncao.BackColor = System.Drawing.ColorTranslator.FromHtml("#DFDFDF");
                txtSetor.BackColor = System.Drawing.ColorTranslator.FromHtml("#DFDFDF");
                txtJornada.BackColor = System.Drawing.ColorTranslator.FromHtml("#DFDFDF");
                txtCC.BackColor = System.Drawing.ColorTranslator.FromHtml("#DFDFDF");
                txtLocalTrabalho.BackColor = System.Drawing.ColorTranslator.FromHtml("#DFDFDF");

                cmb_Setor1.Visible = false;
                cmb_Cargo1.Visible = false;
                cmb_Funcao1.Visible = false;
                cmb_Jornada.Visible = false;
                cmbLocalTrabalho.Visible = false;

               

                cmd_Excluir1.Visible = true;
                cmd_Alterar1.Visible = true;

                cmd_Excluir1.Enabled = true;
                cmd_Alterar1.Enabled = true;

            }



        }




        protected void PopulaUniforme()
        {
            DataSet xdS;

            Ilitera.Data.PPRA_EPI xUnif = new Ilitera.Data.PPRA_EPI();

            xdS = xUnif.Retornar_Tamanhos_Uniforme(System.Convert.ToInt32(Session["Empresa"].ToString()));


            DataTable dt = xdS.Tables["Result"];

            string xId_Uniforme;
            string xId_Uniforme_Medidas;
            string xUniforme;
            string xMedida;
            string xValor;
            string xDtFornecimento;

            lst_Uniformes.Items.Clear();
            lst_Id_Uniforme.Items.Clear();
            lst_Id_Uniforme_Medida.Items.Clear();
            lst_Sel_Uni.Items.Clear();


            foreach (DataRow dr in dt.Rows)
            {

                xId_Uniforme = dr["Id_Uniforme"].ToString().Trim();
                xId_Uniforme_Medidas = dr["Id_Uniforme_Medidas"].ToString().Trim();
                xUniforme = dr["Uniforme"].ToString().Trim();
                xMedida = dr["Medida"].ToString().Trim();
                xValor = dr["Valor"].ToString().Trim();
                

                lst_Id_Uniforme_Medida.Items.Add(xId_Uniforme_Medidas);
                lst_Id_Uniforme.Items.Add(xId_Uniforme);


                string Espacos = "";

                if (xUniforme.Length < 40)
                {
                    for (int zcont = xUniforme.Length; zcont < 40; zcont++)
                    {
                        Espacos = Espacos + ".";
                    }

                    xUniforme = xUniforme + Espacos;
                }
                else
                {
                    xUniforme = xUniforme.Substring(0, 40);
                }



                Espacos = "";

                if (xMedida.Length < 25)
                {
                    for (int zcont = xMedida.Length; zcont < 25; zcont++)
                    {
                        Espacos = Espacos + ".";
                    }

                    xMedida = xMedida + Espacos;
                }
                else
                {
                    xMedida = xMedida.Substring(0, 25);
                }


                lst_Uniformes.Items.Add(xUniforme + "  " + xMedida + "  " + xValor);


            }


            //carregar dados uniformes
            DataSet xdS2 = xUnif.Retornar_Uniformes_Empregado(Convert.ToInt32(Session["Empregado"]));


            DataTable dt2 = xdS2.Tables["Result"];

            lst_Id_Uniforme_Medida_Sel.Items.Clear();
            lst_Id_Uniforme_Sel.Items.Clear();
            lst_Sel_Uni.Items.Clear();

            string rQtde = "";

            foreach (DataRow dr in dt2.Rows)
            {

                xId_Uniforme = dr["Id_Uniforme"].ToString().Trim();
                xId_Uniforme_Medidas = dr["Id_Uniforme_Medidas"].ToString().Trim();
                xUniforme = dr["Uniforme"].ToString().Trim();
                xMedida = dr["Medida"].ToString().Trim();
                xValor = dr["Valor"].ToString().Trim();
                xDtFornecimento = dr["DataFornecimento"].ToString().Trim();
                rQtde = dr["Qtde"].ToString().Trim();

                if (rQtde.Length == 1) rQtde = "00" + rQtde;
                else if (rQtde.Length == 2) rQtde = "0" + rQtde;


                lst_Id_Uniforme_Medida_Sel.Items.Add(xId_Uniforme_Medidas);
                lst_Id_Uniforme_Sel.Items.Add(xId_Uniforme);


                string Espacos = "";

                if (xUniforme.Length < 40)
                {
                    for (int zcont = xUniforme.Length; zcont < 40; zcont++)
                    {
                        Espacos = Espacos + ".";
                    }

                    xUniforme = xUniforme + Espacos;
                }
                else
                {
                    xUniforme = xUniforme.Substring(0, 40);
                }



                Espacos = "";

                if (xMedida.Length < 25)
                {
                    for (int zcont = xMedida.Length; zcont < 25; zcont++)
                    {
                        Espacos = Espacos + ".";
                    }

                    xMedida = xMedida + Espacos;
                }
                else
                {
                    xMedida = xMedida.Substring(0, 25);
                }


                lst_Sel_Uni.Items.Add( xDtFornecimento + "  " + rQtde + "  " + xUniforme + "  " + xMedida + "  " + xValor);
            }


        }






        protected void cmb_Setor_SelectedIndexChanged(object sender, EventArgs e)
        {

            txt_CBO.Text = "";

            if (cmb_Setor.SelectedIndex < 0)
            {
                txt_SetorAlt.Text = "";
                return;
            }

            txt_SetorAlt.Text = cmb_SetorD.Items[cmb_Setor.SelectedIndex].ToString();



            //DataSet xDs;
            //int xCont;

            Ilitera.Data.PPRA_EPI xCBO = new Ilitera.Data.PPRA_EPI();


            //cmb_CBO.DataSource = null;
            //cmb_CBO.DataBind();   //por que não esta limpando a lista ??? Será que o select está voltando os valores certos ?  Será necessário reinicializar alguma variável ?
            //cmb_CBO.Items.Clear();

            //xDs = xCBO.Lista_CBO("");
            //cmb_CBO.DataSource = xDs.Tables["Result"];
            //cmb_CBO.DataValueField = "CBO";
            //cmb_CBO.DataTextField = "CBONome";
            //cmb_CBO.DataBind();

            cmb_CBO.SelectedIndex = -1;

            string xCBOID = cmb_CBOID.Items[cmb_Setor.SelectedIndex].ToString().Trim();


            if (xCBOID != "0")
            {                
                string xCBOID_Numbers =  Regex.Replace(xCBOID, "[^0-9 _]", "");

                for (int zCont = 0; zCont < cmb_CBO.Items.Count; zCont++)
                {

                    if (xCBOID == cmb_CBO.Items[zCont].Value || xCBOID_Numbers == cmb_CBO.Items[zCont].Value)
                    {
                        cmb_CBO.SelectedIndex = zCont;
                        break;
                    }

                }

            }




        }


        //protected void cmd_Editar_Click(object sender, EventArgs e)
        //{

        //}

        //protected void cmd_Inserir_Click(object sender, EventArgs e)
        //{

        //}

        protected void cmd_Alterar1_Click(object sender, EventArgs e)
        {

            int zId;
            //int zCont = 0;
            //int zAux = 0;
            //bool zDif = false;
            //bool zLoc = false;

            string xCargo;
            string xSetor;
            string xFuncao;
            string xJornada;
            Int32 zIdLocalTrabalho=0;

            DateTime resultado = DateTime.MinValue;

            //if (System.DateTime.TryParse( txtInicioFuncao.Text, out resultado) == false || txtInicioFuncao.Text.Trim().Length != 10)
            //{
            //    MsgBox1.Show("Ilitera.Net", "Data de Início de Função inválida.", null,
            //      new EO.Web.MsgBoxButton("OK"));
            //    return;
            //}

            //if (txtTerminoFuncao.Text.Trim() != "")
            //{
            //    if (System.DateTime.TryParse(txtTerminoFuncao.Text, out resultado) == false || txtTerminoFuncao.Text.Trim().Length != 10)
            //    {
            //        MsgBox1.Show("Ilitera.Net", "Data de Término de Função inválida.", null,
            //          new EO.Web.MsgBoxButton("OK"));
            //        return;
            //    }
            //}


            
            if (txtInicioFuncao.Text.Trim() != "")
            {
                if (Validar_Data(txtInicioFuncao.Text.Trim()) == false)
                {
                    return;
                }
            }

            if (txtTerminoFuncao.Text.Trim() != "")
            {
                if (Validar_Data(txtTerminoFuncao.Text.Trim()) == false)
                {
                    return;
                }
            }


            //checar se término função é anterior à data de admissão e a data início da classif.funcional
            if (txtTerminoFuncao.Text.Trim() != "")
            {
                System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");
                DateTime zFim = System.Convert.ToDateTime(txtTerminoFuncao.Text, ptBr);
                DateTime zInicio = System.Convert.ToDateTime(txtInicioFuncao.Text, ptBr);

                if ( zFim < zInicio)
                {
                    MsgBox1.Show("Ilitera.Net", "Data Final da Classificação Funcional anterior à Data Inicial.", null,
                    new EO.Web.MsgBoxButton("OK"));
                    return;
                }

                DateTime zAdm = System.Convert.ToDateTime(txtDataAdmissao.Text, ptBr);

                if (zFim < zAdm)
                {
                    MsgBox1.Show("Ilitera.Net", "Data Final da Classificação Funcional anterior à Data de Admissão.", null,
                    new EO.Web.MsgBoxButton("OK"));
                    return;
                }


            }





            zId = System.Convert.ToInt32(Session["Empregado"].ToString());


            if ((txtInicioFuncao.Text == "") || (txtFuncao.Text == "" && cmb_Funcao1.SelectedIndex <= 0) || (txtSetor.Text == "" && cmb_Setor1.SelectedIndex <= 0) || (txtCargo.Text == "" && cmb_Cargo1.SelectedIndex <= 0))
            {
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "alerta", String.Format("alert('{0}')", "Funcionário deve ter pelo menos uma classificação funcional preenchida de forma completa."), true);
                MsgBox1.Show("Ilitera.Net", "Funcionário deve ter pelo menos uma classificação funcional preenchida de forma completa.", null,
                  new EO.Web.MsgBoxButton("OK"));                   
            }
            else
            {

                //se data de demissão preenchida,  checar se ela bate com alguma data final de classif.funcional, senão, não há lógica



                //abrir tela para inserir novo empregado

                Ilitera.Data.Empregado_Cadastral xEmpregado;

                xEmpregado = new Ilitera.Data.Empregado_Cadastral();


                //se selecionar combo de cargo, funcao ou setor,  ele prevalecerá sobre textbox.  Ver se SelectedValue <> 0
                if (cmb_Cargo1.SelectedValue.ToString().Trim() != "0")
                {
                    xCargo = cmb_Cargo1.Items[cmb_Cargo1.SelectedIndex].ToString();
                }
                else
                {
                    xCargo = txtCargo.Text.ToUpper();
                }

                if (cmb_Setor1.SelectedValue.ToString().Trim() != "0" && cmb_Setor1.Visible==true)
                {
                    xSetor = cmb_Setor1.Items[cmb_Setor1.SelectedIndex].ToString();
                }
                else
                {
                    xSetor = txtSetor.Text.ToUpper();
                }

                if (cmb_Funcao1.SelectedValue.ToString().Trim() != "0")
                {
                    xFuncao = cmb_Funcao1.Items[cmb_Funcao1.SelectedIndex].ToString();
                }
                else
                {
                    xFuncao = txtFuncao.Text.ToUpper();
                }


                string xCodigo = "";

                if (xFuncao.IndexOf("|") > 0)
                {
                    xCodigo = xFuncao.Substring(xFuncao.IndexOf("|") + 1).Trim();
                    xFuncao = xFuncao.Substring(0, xFuncao.IndexOf("|") - 1);
                }



                if (cmb_Jornada.SelectedValue.ToString().Trim() != "0")
                {
                    xJornada = cmb_Jornada.Items[cmb_Jornada.SelectedIndex].ToString();
                    if (xJornada.Trim() == "()" || xJornada.Trim() == "-") xJornada = "";
                }
                else
                {
                    xJornada = txtJornada.Text.ToUpper();
                    if (xJornada.Trim() == "()" || xJornada.Trim() == "-") xJornada = "";
                }

                if (cmbLocalTrabalho.SelectedIndex <= 0)   // PRIMEIRA OPÇÃO DA LISTA É A PRÓPRIA EMPRESA
                {
                    zIdLocalTrabalho = System.Convert.ToInt32(lbl_Id_Empresa.Text);
                }
                else
                {
                    zIdLocalTrabalho = System.Convert.ToInt32(lstLocalTrabalho.Items[cmbLocalTrabalho.SelectedIndex].Value);
                }


                if (txtInicioFuncao.BackColor == System.Drawing.ColorTranslator.FromHtml("#FF8090"))  //deletar
                {
                    Ilitera.Opsa.Data.Cliente xCliente2 = new Ilitera.Opsa.Data.Cliente();
                    xCliente2.Find(System.Convert.ToInt32(Session["Empresa"]));

                    Ilitera.Data.eSocial xChecagem = new Ilitera.Data.eSocial();
                    if (xChecagem.Checar_Classif_Funcional_eSocial(System.Convert.ToInt32(lbl_Id.Text), "2240", xCliente2.ESocial_Ambiente) > 0)
                    {
                        MsgBox1.Show("Ilitera.Net", "Essa classif.funcional do colaborador tem evento criado associado ao e-Social, não é possível excluir.", null,
                        new EO.Web.MsgBoxButton("OK"));
                        return;
                    }
                    else
                    {
                        //xEmpregado.Apagar_Classificacao_Funcional(zId, System.Convert.ToInt32(lbl_Id_Empresa.Text), xCargo, xSetor, xFuncao, System.Convert.ToInt32(lbl_Id_Usuario.Text));
                        xEmpregado.Apagar_Classificacao_Funcional(zId, zIdLocalTrabalho, xCargo, xSetor, xFuncao, System.Convert.ToInt32(lbl_Id_Usuario.Text));
                    }
                }
                else if (lbl_Id.Text != "0")   //update
                {
                    //xEmpregado.Atualizar_Classificacao_Funcional(zId, System.Convert.ToInt32(lbl_Id_Empresa.Text), txtInicioFuncao.Text, txtTerminoFuncao.Text.Trim(), xCargo, xSetor, xFuncao, System.Convert.ToInt32(lbl_Id_Usuario.Text), txtCC.Text.Trim());
                    //xEmpregado.Atualizar_Classificacao_Funcional(zId, zIdLocalTrabalho, txtInicioFuncao.Text, txtTerminoFuncao.Text.Trim(), xCargo, xSetor, xFuncao, System.Convert.ToInt32(lbl_Id_Usuario.Text), txtCC.Text.Trim());

                    if (xCodigo == "")
                    {
                        xEmpregado.Atualizar_Classificacao_Funcional2(System.Convert.ToInt32(lbl_Id.Text), zIdLocalTrabalho, txtInicioFuncao.Text, txtTerminoFuncao.Text.Trim(), xCargo, xSetor, xFuncao, System.Convert.ToInt32(lbl_Id_Usuario.Text), txtCC.Text.Trim(), xJornada);
                    }
                    else
                    {
                        xEmpregado.Atualizar_Classificacao_Funcional4(System.Convert.ToInt32(lbl_Id.Text), zIdLocalTrabalho, txtInicioFuncao.Text, txtTerminoFuncao.Text.Trim(), xCargo, xSetor, xFuncao, System.Convert.ToInt32(lbl_Id_Usuario.Text), txtCC.Text.Trim(), xJornada, xCodigo);
                    }
                }
                else
                {
                    //como saber se dado já existia ???   vou checar se registro já existe pela Stored Procedure   
                    if (xJornada.Trim() == "")
                    {
                        //xEmpregado.Inserir_Classificacao_Funcional(zId, System.Convert.ToInt32(lbl_Id_Empresa.Text), txtInicioFuncao.Text, txtTerminoFuncao.Text.Trim(), xCargo, xSetor, xFuncao, System.Convert.ToInt32(lbl_Id_Usuario.Text), txtCC.Text.Trim());

                        if (xCodigo == "")
                        {
                            xEmpregado.Inserir_Classificacao_Funcional(zId, zIdLocalTrabalho, txtInicioFuncao.Text, txtTerminoFuncao.Text.Trim(), xCargo, xSetor, xFuncao, System.Convert.ToInt32(lbl_Id_Usuario.Text), txtCC.Text.Trim());
                        }
                        else
                        {
                            xEmpregado.Inserir_Classificacao_Funcional2(zId, zIdLocalTrabalho, txtInicioFuncao.Text, txtTerminoFuncao.Text.Trim(), xCargo, xSetor, xFuncao, System.Convert.ToInt32(lbl_Id_Usuario.Text), txtCC.Text.Trim(), xCodigo);
                        }

                        bool xAlocacao = false;

                        Ilitera.Data.Empregado_Cadastral xFuncao_GHE = new Ilitera.Data.Empregado_Cadastral();
                        xAlocacao = xFuncao_GHE.Criar_Alocacao_Funcao_GHE(zIdLocalTrabalho, xFuncao, zId);

                        MsgBox1.Show("Ilitera.Net", "Agendar exame de mudança de risco ocupacional, caso colaborador ficar exposto a riscos, na nova função, diferentes da função atual.", null,
                              new EO.Web.MsgBoxButton("OK"));
                        PopulaGrid();
                        return;

                    }
                    else
                    {
                        //xEmpregado.Inserir_Classificacao_Funcional_Jornada(zId, System.Convert.ToInt32(lbl_Id_Empresa.Text), txtInicioFuncao.Text, txtTerminoFuncao.Text.Trim(), xCargo, xSetor, xFuncao, System.Convert.ToInt32(lbl_Id_Usuario.Text), txtCC.Text.Trim(), xJornada);

                        if (xCodigo == "")
                        {
                            xEmpregado.Inserir_Classificacao_Funcional_Jornada(zId, zIdLocalTrabalho, txtInicioFuncao.Text, txtTerminoFuncao.Text.Trim(), xCargo, xSetor, xFuncao, System.Convert.ToInt32(lbl_Id_Usuario.Text), txtCC.Text.Trim(), xJornada);
                        }
                        else
                        {
                            xEmpregado.Inserir_Classificacao_Funcional_Jornada2(zId, zIdLocalTrabalho, txtInicioFuncao.Text, txtTerminoFuncao.Text.Trim(), xCargo, xSetor, xFuncao, System.Convert.ToInt32(lbl_Id_Usuario.Text), txtCC.Text.Trim(), xJornada, xCodigo);
                        }

                        bool xAlocacao = false;

                        Ilitera.Data.Empregado_Cadastral xFuncao_GHE = new Ilitera.Data.Empregado_Cadastral();
                        xAlocacao = xFuncao_GHE.Criar_Alocacao_Funcao_GHE(zIdLocalTrabalho, xFuncao, zId);


                        MsgBox1.Show("Ilitera.Net", "Agendar exame de mudança de risco ocupacional, caso colaborador ficar exposto a riscos, na nova função, diferentes da função atual.", null,
                              new EO.Web.MsgBoxButton("OK"));
                        PopulaGrid();
                        return;

                    }




                }



            }



            ////checar se houve atualização dos GHES nas classif.funcionais
            //zDif = false;

            //if (lst_PPRA.Visible == true)  
            //{

            //    if (lst_Sel_1.Items.Count == lst_Sel_1_Cop.Items.Count)
            //    {
            //        for (zCont = 0; zCont < lst_Sel_1_Cop.Items.Count; zCont++)
            //        {
            //            zLoc = false;

            //            for (zAux = 0; zAux < lst_Sel_1.Items.Count; zAux++)
            //            {
            //                if (lst_Sel_1.Items[zAux].ToString() == lst_Sel_1_Cop.Items[zCont].ToString())
            //                {
            //                    zLoc = true;
            //                }
            //            }

            //            if (zLoc == false)
            //            {
            //                zDif = true;
            //            }
            //        }
            //    }
            //    else
            //    {
            //        zDif = true;
            //    }
            //}


            //if (zDif == true)
            //{

            //    Mestra.Data.PPRA_EPI xGHE1 = new Mestra.Data.PPRA_EPI();

            //    //primeiro apagar
            //    xGHE1.Excluir_GHE_Classif_Funcional(System.Convert.ToInt32(lbl_Id.Text), System.Convert.ToInt32(Session["Empregado"].ToString()), System.Convert.ToInt32(Session["Empresa"].ToString()));

            //    Mestra.Data.PPRA_EPI xGHE1_1 = new Mestra.Data.PPRA_EPI();

            //    //inserir
            //    for (zCont = 0; zCont < lst_Sel_1.Items.Count; zCont++)
            //    {
            //        xGHE1_1.Salvar_GHE_Classif_Funcional(System.Convert.ToInt32(lbl_Id.Text), System.Convert.ToInt32(lst_Sel_1_Id.Items[zCont].ToString()), System.Convert.ToInt32(Session["Empregado"].ToString()), System.Convert.ToInt32(Session["Empresa"].ToString()), lst_Sel_1.Items[zCont].ToString().Substring(0, 10));
            //    }

            //}



            PopulaGrid();

            //Response.Redirect("~/ListaEmpregados.aspx");


        }

        protected void cmd_Excluir1_Click(object sender, EventArgs e)
        {

            txtInicioFuncao.BackColor = System.Drawing.ColorTranslator.FromHtml("#FF8090");
            txtTerminoFuncao.BackColor = System.Drawing.ColorTranslator.FromHtml("#FF8090");
            txtCargo.BackColor = System.Drawing.ColorTranslator.FromHtml("#FF8090");
            txtFuncao.BackColor = System.Drawing.ColorTranslator.FromHtml("#FF8090");
            txtSetor.BackColor = System.Drawing.ColorTranslator.FromHtml("#FF8090");
            txtJornada.BackColor = System.Drawing.ColorTranslator.FromHtml("#FF8090");
            txtCC.BackColor = System.Drawing.ColorTranslator.FromHtml("#FF8090");
            txtLocalTrabalho.BackColor = System.Drawing.ColorTranslator.FromHtml("#FF8090");


            cmd_Excluir1.Enabled = false;
            cmd_Alterar1.Enabled = true;

        }



        protected void cmd_Add_1_Click(object sender, EventArgs e)
        {

            if (lst_1.SelectedIndex < 0) return;

            int zCont = 0;

            for (zCont = 0; zCont < lst_Sel_1.Items.Count; zCont++)
            {
                if (lst_1.Items[lst_1.SelectedIndex].ToString() == lst_Sel_1.Items[zCont].ToString())
                    return;

                if (lst_1.Items[lst_1.SelectedIndex].ToString().Substring(1, 10) == lst_Sel_1.Items[zCont].ToString().Substring(1, 10))
                {
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "Alerta", "Permitido apenas 1 GHE por Laudo.", true);
                    MsgBox1.Show("Ilitera.Net", "Permitido apenas 1 GHE por Laudo.", null,
                            new EO.Web.MsgBoxButton("OK"));                   
                    return;
                }

            }

            lst_Sel_1.Items.Add(lst_1.Items[lst_1.SelectedIndex].ToString());
            lst_Sel_1_Id.Items.Add(lst_1_ID.Items[lst_1.SelectedIndex].ToString());
        }

        protected void cmd_Remove_1_Click(object sender, EventArgs e)
        {

            if (lst_Sel_1.SelectedIndex < 0) return;

            lst_Sel_1_Id.Items.RemoveAt(lst_Sel_1.SelectedIndex);
            lst_Sel_1.Items.RemoveAt(lst_Sel_1.SelectedIndex);

        }



        protected void cmdGHE_Click(object sender, EventArgs e)
        {

        }

        protected void cmd_Acidente_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/PCMSO/ListaAcidentes2.aspx?Tipo=1&Tela=1");
        }

        protected void cmd_Atualizar_DFuncao_Click(object sender, EventArgs e)
        {


            if (cmb_Setor.SelectedIndex < 0)
            {
                return;
            }


            //salvar dados da função
            //tabela  " + Mestra.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.Funcao WHERE Funcao.IdFuncao=2146160551
            Ilitera.Data.Empregado_Cadastral xEmpregado;

            xEmpregado = new Ilitera.Data.Empregado_Cadastral();


            xEmpregado.Atualizar_Descricao_Funcao(System.Convert.ToInt32(cmb_SetorID.Items[cmb_Setor.SelectedIndex].ToString()), txt_SetorAlt.Text.Trim(), cmb_CBO.SelectedValue.ToString(), System.Convert.ToInt32(lbl_Id_Usuario.Text));



            Response.Redirect("~/ListaEmpregados.aspx");
        }


        //protected void cmb_CBO_SelectedIndexChanged(object sender, EventArgs e)
        //{

        //}


        //protected void lst_1_SelectedIndexChanged(object sender, EventArgs e)
        //{

        //}

        protected void txt_CBO_TextChanged(object sender, EventArgs e)
        {
            string xFiltro;

            xFiltro = txt_CBO.Text.Trim();

            DataSet xDs;

            Ilitera.Data.PPRA_EPI xCBO = new Ilitera.Data.PPRA_EPI();

            cmb_CBO.DataSource = null;
            cmb_CBO.DataBind();   //por que não esta limpando a lista ??? Será que o select está voltando os valores certos ?  Será necessário reinicializar alguma variável ?
            cmb_CBO.Items.Clear();

            xDs = xCBO.Lista_CBO(xFiltro);
            cmb_CBO.DataSource = xDs.Tables["Result"];
            cmb_CBO.DataValueField = "CBO";
            cmb_CBO.DataTextField = "CBONome";
            cmb_CBO.DataBind();

            if (cmb_CBO.Items.Count > 0)
            {
                cmb_CBO.SelectedIndex = 0;
            }


        }



        public void selecionarDadosCadastraisEmpregado()
        {
            try
            {
                if (Session["Empregado"] != null && Session["Empregado"].ToString().Trim() != String.Empty)
                {
                    Empregado empregado = new Empregado();
                    empregado = EmpregadoFacade.RetornarEmpregadoDadosCadastrais(Convert.ToInt32(Session["Empregado"]));

                    //txtNomeEmpregado.Text = empregado.NomeEmpregado;
                    txtApelidoEmpregado.Text = empregado.ApelidoEmpregado;
                    txtObs.Text = empregado.tObs; //empregado.ApelidoEmpregado;
                    txtCPF.Text = empregado.Cpf;
                    txtCTPS_Num.Text = empregado.numCTPS;
                    txtCTPS_Serie.Text = empregado.serieCTPS;

                    if (empregado.gTerceiro == true) chk_Terceirizado.Checked = true;
                    else chk_Terceirizado.Checked = false;

                    if ( empregado.teMail != null )
                       txteMail.Text = empregado.teMail.Trim();

                    if (empregado.DataAdmissao.ToString("dd/MM/yyyy") == "01/01/0001")
                       txtDataAdmissao.Text = "";
                    else
                       txtDataAdmissao.Text = empregado.DataAdmissao.ToString("dd/MM/yyyy");

                    if ( empregado.DataDemissao.ToString("dd/MM/yyyy") == "01/01/0001" )
                        txtDataDemissao.Text = "";
                    else
                       txtDataDemissao.Text = empregado.DataDemissao.ToString("dd/MM/yyyy");

                    txtMatricula.Text = empregado.matricula;
                    txtPISPASEP.Text = empregado.PISPASEP;

                    if (empregado.DataNascimento.ToString("dd/MM/yyyy") == "01/01/0001")
                        txtDataNascimento.Text = "";
                    else
                        txtDataNascimento.Text = empregado.DataNascimento.ToString("dd/MM/yyyy");

                    txtRG.Text = empregado.Identidade;
                    txtCTPS_UF.Text = empregado.ufCTPS;

                    cbSexo.SelectedValue = empregado.Sexoempregado;
                    cbSexo.Enabled = true;


                    //endereço
                    txtEndereco.Text = empregado.endLogradouro.Trim();
                    txtNumero.Text = empregado.endNumero.Trim();
                    txtComplemento.Text = empregado.endComplemento.Trim();
                    txtBairro.Text = empregado.endBairro.Trim();
                    txtMunicipio.Text = empregado.endMunicipio.Trim();
                    txtUF.Text = empregado.endUF.Trim();
                    txtCEP.Text = empregado.endCEP.Trim();





                    //lblNomeEmpregado.Text = Session["NomeEmpregado"].ToString();
                    lblNomeEmpregado.Text = empregado.NomeEmpregado.Trim();
                    txtNomeEmpregado.Text = empregado.NomeEmpregado.Trim();

                    Ilitera.Opsa.Data.Cliente xCliente = new Ilitera.Opsa.Data.Cliente();
                    xCliente.Find(System.Convert.ToInt32(Session["Empresa"]));

                    if (xCliente.Permitir_Edicao_Nome == true) txtNomeEmpregado.Enabled = true;
                    else txtNomeEmpregado.Enabled = false;
                    


                    txtFoto.Text = empregado.nNOFOTO;

                    if (empregado.nInd_Beneficiario == "0")
                    {
                        opt_Benef_NA.Checked = true;
                        opt_Benef_Reabilitado.Checked = false;
                        opt_Benef_Deficiencia.Checked = false;
                    }
                    else if (empregado.nInd_Beneficiario == "1")
                    {
                        opt_Benef_NA.Checked = false;
                        opt_Benef_Reabilitado.Checked = true;
                        opt_Benef_Deficiencia.Checked = false;
                    }
                    else if (empregado.nInd_Beneficiario == "2")
                    {
                        opt_Benef_NA.Checked = false;
                        opt_Benef_Reabilitado.Checked = false;
                        opt_Benef_Deficiencia.Checked = true;
                    }


                    Ilitera.Opsa.Data.Empregado xempregado = new Ilitera.Opsa.Data.Empregado();

                    xempregado.Find(" tblEMPREGADO.nID_EMPREGADO = " + Session["Empregado"].ToString());

                    string xArquivo;
                    string xAux;

                    if (txtFoto.Text.Trim() == "0")
                    {
                        xArquivo = "";
                    }
                    else
                    {
                        xAux = txtFoto.Text.Trim();

                        for (int xCont = xAux.Length; xCont < xempregado.FotoTamanho; xCont++)
                        {
                            xAux = "0" + xAux;
                        }

                        xArquivo = xempregado.FotoDiretorio + "\\" + xempregado.FotoInicio + xAux + xempregado.FotoExtensao;
                        txt_Arq.Text = xArquivo;
                        if (xArquivo.ToUpper().IndexOf("FOTOSDOCSDIGITAIS") < 1)
                        {
                            xArquivo = xArquivo.Substring(0, xArquivo.IndexOf("\\") + 1) + "FOTOSDOCSDIGITAIS\\" + xArquivo.Substring(xArquivo.IndexOf("\\") + 1);
                        }


                        //carregar imagem em um array

                        if (System.IO.File.Exists(Ilitera.Common.Fotos.PathFoto_Uri(xArquivo)))
                        {
                            ImgFunc.ImageUrl = "data:image;base64," + Convert.ToBase64String(System.IO.File.ReadAllBytes(Ilitera.Common.Fotos.PathFoto_Uri(xArquivo)));
                        }

                        //ImgFunc.ImageUrl = Ilitera.Common.Fotos.PathFoto_Uri(xArquivo);
                    }



                    Ilitera.Opsa.Data.Empregado_Aptidao xAptidao = new Ilitera.Opsa.Data.Empregado_Aptidao();
                    xAptidao.Find(" nId_Empregado = " + Session["Empregado"].ToString());

                    if (xAptidao.Id != 0)
                    {

                        if (xAptidao.apt_Trabalho_Altura == true) chk_apt_Altura.Checked = true;
                        else chk_apt_Altura.Checked = false;

                        if (xAptidao.apt_Alimento == true) chk_Apt_Alimento.Checked = true;
                        else chk_Apt_Alimento.Checked = false;
                        
                        if (xAptidao.apt_Aquaviario == true) chk_apt_Aquaviarios.Checked = true;
                        else chk_apt_Aquaviarios.Checked = false;

                        if (xAptidao.apt_Eletricidade == true) chk_apt_Eletricidade.Checked = true;
                        else chk_apt_Eletricidade.Checked = false;

                        if (xAptidao.apt_Espaco_Confinado == true) chk_apt_Confinado.Checked = true;
                        else chk_apt_Confinado.Checked = false;

                        if (xAptidao.apt_Submerso == true) chk_apt_Submerso.Checked = true;
                        else chk_apt_Submerso.Checked = false;
                        
                        if (xAptidao.apt_Transporte == true) chk_apt_Transportes.Checked = true;
                        else chk_apt_Transportes.Checked = false;

                        if (xAptidao.apt_Brigadista == true) chk_Apt_Brigadista.Checked = true;
                        else chk_Apt_Brigadista.Checked = false;

                        if (xAptidao.apt_Socorrista == true) chk_Apt_Socorrista.Checked = true;
                        else chk_Apt_Socorrista.Checked = false;

                        if (xAptidao.apt_Respirador== true) chk_Apt_Respirador.Checked = true;
                        else chk_Apt_Respirador.Checked = false;

                        if (xAptidao.apt_Radiacao == true) chk_Apt_Radiacao.Checked = true;
                        else chk_Apt_Radiacao.Checked = false;

                    }

                    ////buscar GHE Atual
                    //Mestra.Data.PPRA_EPI xGHE = new Mestra.Data.PPRA_EPI();

                    //txtGHE_Atual.Text = xGHE.Trazer_GHE_Atual(System.Convert.ToInt32(Session["Empregado"].ToString()), System.Convert.ToInt32(Session["Empresa"].ToString()));
                    //txtGHE_Atual.ReadOnly = true;


                    //cmb_GHE.DataSource = xGHE.Trazer_Lista_GHEs(System.Convert.ToInt32(Session["Empresa"].ToString()));
                    //cmb_GHE.DataValueField = "nID_Func";
                    //cmb_GHE.DataTextField = "tNO_Func";                                        
                    //cmb_GHE.DataBind();

                    //cmb_GHE.SelectedIndex = -1;

                    //cmb_GHE.Enabled = false;
                    //lblGHE.Enabled = false;

                }
            }
            catch (Exception ex)
            {
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "alerta", String.Format("alert('{0}')", ex.Message), true);
                MsgBox1.Show("Ilitera.Net", ex.Message, null,
                         new EO.Web.MsgBoxButton("OK"));                   
 
            }
        }

        protected void cmd_Novo_Click(object sender, EventArgs e)
        {

            lbl_Id.Text = "0";

            txtInicioFuncao.ReadOnly = false;
            txtTerminoFuncao.ReadOnly = false;
            txtCargo.ReadOnly = false;
            txtFuncao.ReadOnly = false;
            txtSetor.ReadOnly = false;
            txtJornada.ReadOnly = false;
            txtCC.ReadOnly = false;
            txtLocalTrabalho.ReadOnly = false;

            txtInicioFuncao.BackColor = System.Drawing.ColorTranslator.FromHtml("#DFDFDF");
            txtTerminoFuncao.BackColor = System.Drawing.ColorTranslator.FromHtml("#DFDFDF");
            txtCargo.BackColor = System.Drawing.ColorTranslator.FromHtml("#DFDFDF");
            txtFuncao.BackColor = System.Drawing.ColorTranslator.FromHtml("#DFDFDF");
            txtSetor.BackColor = System.Drawing.ColorTranslator.FromHtml("#DFDFDF");
            txtJornada.BackColor = System.Drawing.ColorTranslator.FromHtml("#DFDFDF");
            txtCC.BackColor = System.Drawing.ColorTranslator.FromHtml("#DFDFDF");
            txtLocalTrabalho.BackColor = System.Drawing.ColorTranslator.FromHtml("#DFDFDF");

            txtSetor.Text = "";
            txtCargo.Text = "";
            txtFuncao.Text = "";
            txtJornada.Text = "";
            txtCC.Text = "";
            txtLocalTrabalho.Text = "";

            txtInicioFuncao.Text = "";
            txtTerminoFuncao.Text = "";


            cmb_Setor1.Visible = true;
            cmb_Cargo1.Visible = true;
            cmb_Funcao1.Visible = true;
            cmb_Jornada.Visible = true;
            cmbLocalTrabalho.Visible = true;
            cmb_Setor1.SelectedIndex = 0;
            cmb_Cargo1.SelectedIndex = 0;
            cmb_Funcao1.SelectedIndex = 0;
            cmb_Jornada.SelectedIndex = 0;
            cmbLocalTrabalho.SelectedIndex = 0;

            cmdGHE_1.Visible = false;
            cmd_Editar_Setor_Funcao.Visible = false;

            cmd_Excluir1.Enabled = false;
            cmd_Alterar1.Enabled = true;
            cmd_Alterar1.Visible = true;

            lst_1.Visible = false;
            lst_Sel_1.Visible = false;
            cmd_Add_1.Visible = false;
            cmd_Remove_1.Visible = false;

            lst_PPRA.Visible = false;
            lbl_PPRA0.Visible = false;
            lst_11.Visible = false;
            lst_Sel_11.Visible = false;
            cmd_Add11.Visible = false;
            cmd_Remove11.Visible = false;
            lbl_Selecao.Visible = false;
            lbl_Selecionado.Visible = false;
            txtLocalTrabalho.Visible = false;

            return;

        }



        static string GetNumberFromStrFaster(string str)
        {
            str = str.Trim();
            //Match m = new Regex(@"^[\+\-]?\d*\.?[Ee]?[\+\-]?\d*$", RegexOptions.Compiled).Match(str);
            //return (m.Value);

            //for (int xCont = 0; xCont<str.Length - 1; xCont++)
            //{
            //if ( str.Substring(xCont,1) 

            //}

            return string.Join(null, System.Text.RegularExpressions.Regex.Split(str, "[^\\d]"));

        }



        protected Boolean Validar_Data(string zData)
        {
            int zDia = 0;
            int zMes = 0;
            int zAno = 0;

            string Validar;
            bool isNumerical;
            int myInt;


            if (zData.Length != 10)
            {
                MsgBox1.Show("Ilitera.Net", "Data Inválida.  Utilizar formato dd/MM/yyyy", null,
                new EO.Web.MsgBoxButton("OK"));
                return false;
            }

            Validar = zData.Substring(0, 2);
            isNumerical = int.TryParse(Validar, out myInt);
            if (isNumerical == false)
            {
                MsgBox1.Show("Ilitera.Net", "Dia Inválido.  Utilizar formato dd/MM/yyyy", null,
                new EO.Web.MsgBoxButton("OK"));
                return false;
            }


            Validar = zData.Substring(3, 2);
            isNumerical = int.TryParse(Validar, out myInt);
            if (isNumerical == false)
            {
                MsgBox1.Show("Ilitera.Net", "Mês Inválido.  Utilizar formato dd/MM/yyyy", null,
                new EO.Web.MsgBoxButton("OK"));
                return false;
            }


            Validar = zData.Substring(6, 4);
            isNumerical = int.TryParse(Validar, out myInt);
            if (isNumerical == false)
            {
                MsgBox1.Show("Ilitera.Net", "Ano Inválido.  Utilizar formato dd/MM/yyyy", null,
                new EO.Web.MsgBoxButton("OK"));
                return false;
            }


            if (zData.Substring(2, 1) != "/" || zData.Substring(5, 1) != "/")
            {
                MsgBox1.Show("Ilitera.Net", "Data Inválida.  Utilizar formato dd/MM/yyyy", null,
                new EO.Web.MsgBoxButton("OK"));
                return false;
            }


            zDia = System.Convert.ToInt32(zData.Substring(0, 2));
            zMes = System.Convert.ToInt32(zData.Substring(3, 2));
            zAno = System.Convert.ToInt32(zData.Substring(6, 4));

            if (zAno < 1900 || zAno > 2025)
            {
                MsgBox1.Show("Ilitera.Net", "Ano Inválido.  Utilizar formato dd/MM/yyyy", null,
                new EO.Web.MsgBoxButton("OK"));
                return false;
            }

            if (zMes < 1 || zMes > 12)
            {
                MsgBox1.Show("Ilitera.Net", "Mês Inválido.  Utilizar formato dd/MM/yyyy", null,
                new EO.Web.MsgBoxButton("OK"));
                return false;
            }

            if (zMes == 1 || zMes == 3 || zMes == 5 || zMes == 7 || zMes == 8 || zMes == 10 || zMes == 12)
            {
                if (zDia < 1 || zDia > 31)
                {
                    MsgBox1.Show("Ilitera.Net", "Dia Inválido.  Utilizar formato dd/MM/yyyy", null,
                    new EO.Web.MsgBoxButton("OK"));
                    return false;
                }
            }
            else if (zMes == 4 || zMes == 6 || zMes == 9 || zMes == 11)
            {
                if (zDia < 1 || zDia > 30)
                {
                    MsgBox1.Show("Ilitera.Net", "Dia Inválido.  Utilizar formato dd/MM/yyyy", null,
                    new EO.Web.MsgBoxButton("OK"));
                    return false;
                }
            }
            else
            {
                if (zDia < 1 || zDia > 29)
                {
                    MsgBox1.Show("Ilitera.Net", "Dia Inválido.  Utilizar formato dd/MM/yyyy", null,
                    new EO.Web.MsgBoxButton("OK"));
                    return false;
                }
            }

            return true;

        }


        protected void cmd_Atualizar_Click(object sender, EventArgs e)
        {

            int zId;
            //int zCont = 0;
            string xBeneficiario = "0";


            DateTime resultado = DateTime.MinValue;


            txtDataAdmissao.Text = txtDataAdmissao.Text.Trim();
            txtDataDemissao.Text = txtDataDemissao.Text.Trim();
            txtDataNascimento.Text = txtDataNascimento.Text.Trim();

            //checar se data admissao, nascimento ok
            if (Validar_Data(txtDataAdmissao.Text.Trim()) == false)
            {
                return;
            }

            if (txtDataNascimento.Text != "")
            {
                if (Validar_Data(txtDataNascimento.Text.Trim()) == false)
                {
                    return;
                }
            }


            if (txtDataDemissao.Text != "")
            {
                if (Validar_Data(txtDataDemissao.Text.Trim()) == false)
                {
                    return;
                }
            }


            if (txtCPF.Text.Trim() != "")
            {
                if ( Valida_CPF( txtCPF.Text.Trim() ) == false )
                {
                   MsgBox1.Show("Ilitera.Net", "CPF Inválido.", null,
                   new EO.Web.MsgBoxButton("OK"));
                   return;
                }
            }
            else
            {
                MsgBox1.Show("Ilitera.Net", "CPF Inválido.", null,
                new EO.Web.MsgBoxButton("OK"));
                return;
            }

            if (txtPISPASEP.Text.Trim() != "" && txtPISPASEP.Text.Trim() != "0")
            {
                if (Valida_PIS(txtPISPASEP.Text.Trim()) == false)
                {
                    MsgBox1.Show("Ilitera.Net", "PIS/PASEP Inválido.", null,
                    new EO.Web.MsgBoxButton("OK"));
                    return;
                }
            }


            if (txtDataDemissao.Text.Trim() != "")
            {

                Ilitera.Opsa.Data.Cliente rCliente = new Ilitera.Opsa.Data.Cliente();
                rCliente.Find(System.Convert.ToInt32(Session["Empresa"].ToString()));

                if (rCliente.Bloquear_Data_Demissao == true)
                {

                    Empregado empregado = new Empregado();
                    empregado = EmpregadoFacade.RetornarEmpregadoDadosCadastrais(Convert.ToInt32(Session["Empregado"]));

                    if (empregado.DataDemissao.Year < 1950)   // se ainda não estava preenchida
                    {

                        if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().IndexOf("Prajna") > 0)
                        {
                            MsgBox1.Show("Ilitera.Net", "Colaborador está configurado para não ter a data de demissão preenchida. Entrar em contato com a central de atendimento no e-mail centraldeatendimento@essencenet.com.br ", null,
                            new EO.Web.MsgBoxButton("OK"));
                        }
                        else if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().IndexOf("Daiti") > 0)
                        {
                            MsgBox1.Show("Ilitera.Net", "Colaborador está configurado para não ter a data de demissão preenchida. Entrar em contato com a central de atendimento no e-mail agendamento.sp.dai@ilitera.com.br ", null,
                            new EO.Web.MsgBoxButton("OK"));
                        }
                        else
                        {
                            MsgBox1.Show("Ilitera.Net", "Colaborador está configurado para não ter a data de demissão preenchida. Entrar em contato com a central de atendimento no e-mail atendimento@ilitera.com.br ", null,
                            new EO.Web.MsgBoxButton("OK"));
                        }

                        return;
                    }
                }

            }


            //ver se campo nome foi colado do Excel com \n\r e espaços - retirar
            string gNome = "";
            
            for ( int gAux=0; gAux < txtNomeEmpregado.Text.Length; gAux++)
            {
                if (!(txtNomeEmpregado.Text.Substring(gAux, 1) == "\n" || txtNomeEmpregado.Text.Substring(gAux, 1) == "\r" || txtNomeEmpregado.Text.Substring(gAux, 1) == "\""))
                { 
                    gNome = gNome + txtNomeEmpregado.Text.Substring(gAux, 1);
                }
            }

            txtNomeEmpregado.Text = gNome.Trim();
      



            if (opt_Benef_NA.Checked == true) xBeneficiario = "3";
            else if (opt_Benef_Reabilitado.Checked == true) xBeneficiario = "1";
            else if (opt_Benef_Deficiencia.Checked == true) xBeneficiario = "2";


            zId = System.Convert.ToInt32(Session["Empregado"].ToString());



            //checar se nome, RG, CPF ou PIS já existem no sistema com colaborador ativo
            Ilitera.Opsa.Data.Empregado zEmpregado = new Ilitera.Opsa.Data.Empregado();



            if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().IndexOf("Prajna") > 0)
            {
                string zBusca = "";

                if (txtCPF.Text.Trim() != "")
                {
                    zBusca = " tNo_CPF collate Latin1_General_CI_AI = '" + txtCPF.Text.Trim() + "'  or  tNo_CPF collate Latin1_General_CI_AI = '" + GetNumberFromStrFaster(txtCPF.Text) + "'  or ";
                }

                if (txtPISPASEP.Text.Trim() != "")
                {
                    zBusca = zBusca + " nNo_PIS_PASEP  = " + GetNumberFromStrFaster(txtPISPASEP.Text) + "  or ";
                }

                if (txtRG.Text.Trim() != "")
                {
                    zBusca = zBusca + " tNo_Identidade collate Latin1_General_CI_AI = '" + txtRG.Text.Trim() + "'  or ";
                }

                if (zBusca == "")
                {
                    zEmpregado.Find(" tNo_Empg collate Latin1_General_CI_AI = '" + txtNomeEmpregado.Text.Trim() + "' and nId_Empr = " + lbl_Id_Empresa.Text + " and hDt_Dem is null and nId_Empregado <> " + zId.ToString() + " ");
                }
                else
                {
                    zEmpregado.Find(" tNo_Empg collate Latin1_General_CI_AI = '" + txtNomeEmpregado.Text.Trim() + "' and nId_Empr = " + lbl_Id_Empresa.Text + " and hDt_Dem is null and nId_Empregado <> " + zId.ToString() + "  and ( " + zBusca.Substring(0, zBusca.Length - 3) + " ) ");
                }

            }
            else
            {
                zEmpregado.Find(" tNo_Empg collate Latin1_General_CI_AI = '" + txtNomeEmpregado.Text.Trim() + "' and nId_Empr = " + lbl_Id_Empresa.Text + " and hDt_Dem is null and nId_Empregado <> " + zId.ToString() + " ");
            }



            //zEmpregado.Find(" tNo_Empg collate Latin1_General_CI_AI = '" + txtNomeEmpregado.Text.Trim() + "' and nId_Empr = " + lbl_Id_Empresa.Text + " and nId_Empregado <> " + zId.ToString());

            if (zEmpregado.tNO_EMPG.Trim() != "")
            {
                MsgBox1.Show("Ilitera.Net", "Colaborador já está cadastrado no sistema.", null,
                new EO.Web.MsgBoxButton("OK"));
                return;
            }




            if (grd_Empregados.Items.Count == 0)
            //if ((txtInicioFuncao.Text == "") || (txtFuncao.Text == "" && cmb_Funcao1.SelectedIndex < 0) || (txtSetor.Text == "" && cmb_Setor1.SelectedIndex < 0))
            {
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "alerta", String.Format("alert('{0}')", "Funcionário deve ter pelo menos uma classificação funcional preenchida de forma completa."), true);
                MsgBox1.Show("Ilitera.Net", "Funcionário deve ter pelo menos uma classificação funcional preenchida de forma completa.", null,
                  new EO.Web.MsgBoxButton("OK"));
                return;
            }
            else
            {

                //se foto de colaborador foi inserida

                string xNomeArq = txt_Arq.Text; //File1.FileName.Trim();

                Entities.Usuario usuario = (Entities.Usuario)Session["usuarioLogado"];

                //salvar prontuario
                if (xNomeArq != string.Empty && xNomeArq.ToUpper().IndexOf("ORGANOGRAMAS") < 0)
                {

                    string xExtension = xNomeArq.Substring(xNomeArq.Length - 3, 3).ToUpper().Trim();

                    if (xExtension == "PDF" || xExtension == "JPG")
                    {

                        Ilitera.Opsa.Data.Cliente xCliente = new Ilitera.Opsa.Data.Cliente();
                        xCliente.Find(System.Convert.ToInt32(Session["Empresa"].ToString()));

                        string xArq = "";

                        // if (Session["Servidor_Web"].ToString().Trim().ToUpper() == "ILITERA")
                        xArq = "I:\\fotosdocsdigitais\\" + xCliente.DiretorioPadrao.ToString().ToUpper().Trim() + "\\Organogramas\\" + xNomeArq;
                        //  else
                        //    xArq = "C:\\DRIVE_I\\fotosdocsdigitais\\" + xCliente.DiretorioPadrao.ToString().ToUpper().Trim() + "\\Organogramas\\" + xNomeArq;


                        //com o nome do arquivo,  como fazer para substituir o File1.SaveAs,  que está vazio ?
                        //byte[] zFoto = null;

                        //zFoto = Fotos.GetByteFoto(xNomeArq);


                        //File1.SaveAs(xArq);


                        zFoto = (byte[])Session["zFoto"];

                        //se for o caso, diminuir tamanho da foto
                        System.IO.MemoryStream xStream = new System.IO.MemoryStream(zFoto);


                        System.Drawing.Bitmap workingBitmap;
                        workingBitmap = new System.Drawing.Bitmap(xStream);

                        if (workingBitmap.Size.Width > 500)
                        {
                            float zIndice = 2;
                            zIndice = workingBitmap.Size.Width / 500;

                            workingBitmap = BitmapManipulator.ResizeBitmap(workingBitmap,
                                                                    (int)(workingBitmap.Size.Width / zIndice),
                                                                    (int)(workingBitmap.Size.Height / zIndice));

                        }


                        workingBitmap.Save(xArq);



                        //using (System.Drawing.Image image = System.Drawing.Image.FromStream(new System.IO.MemoryStream(zFoto)))
                        //{
                        //    image.Save(xArq);  // Or Png
                        //}

                        xArq = "I:\\fotosdocsdigitais\\" + xCliente.DiretorioPadrao.ToString().ToUpper().Trim() + "\\Organogramas\\" + xNomeArq;
                        txt_Arq.Text = xArq;



                        xCliente.ArqFotoEmpregInicio = Fotos.Inicio(xArq);
                        xCliente.ArqFotoEmpregTermino = Fotos.Termino(xNomeArq);
                        xCliente.ArqFotoEmpregExtensao = Fotos.Extensao(xNomeArq);
                        xCliente.ArqFotoEmrpegQteDigitos = Fotos.Tamanho(xArq);
                        xCliente.Save();


                        txtFoto.Text = Fotos.Numero(xArq).ToString().Trim();   //pegar nunmeração da foto

                    }

                }

                //se data de demissão preenchida,  checar se ela bate com alguma data final de classif.funcional, senão, não há lógica



                //abrir tela para inserir novo empregado

                Ilitera.Data.Empregado_Cadastral xEmpregado;

                xEmpregado = new Ilitera.Data.Empregado_Cadastral();

                txtPISPASEP.Text = GetNumberFromStrFaster(txtPISPASEP.Text);
                txtCPF.Text = GetNumberFromStrFaster(txtCPF.Text);

                string xTerceiro = "0";

                if (chk_Terceirizado.Checked == true) xTerceiro = "1";

                xEmpregado.Atualizar_Dados_Empregado(zId, txtCTPS_Num.Text, txtCTPS_Serie.Text, txtCTPS_UF.Text, txtMatricula.Text, txtRG.Text, txtDataAdmissao.Text, txtDataDemissao.Text, txtDataNascimento.Text, txtPISPASEP.Text, txtCPF.Text, txtApelidoEmpregado.Text, txtFoto.Text, cbSexo.SelectedValue.ToString(), System.Convert.ToInt32(lbl_Id_Usuario.Text), xBeneficiario, txtEndereco.Text.Trim(), txtNumero.Text.Trim(), txtComplemento.Text.Trim(), txtBairro.Text.Trim(), txtMunicipio.Text.Trim(), txtUF.Text.Trim(), txtCEP.Text.Trim(),txteMail.Text.Trim(), xTerceiro, txtObs.Text);

                if (lblNomeEmpregado.Text.Trim().ToUpper() != txtNomeEmpregado.Text.Trim().ToUpper())
                {
                    //modificar nome
                    Ilitera.Data.Empregado_Cadastral xEmpregado2;
                    xEmpregado2 = new Ilitera.Data.Empregado_Cadastral();

                    xEmpregado2.Atualizar_Nome_Empregado(zId, txtNomeEmpregado.Text.Trim(),System.Convert.ToInt32(lbl_Id_Usuario.Text));

                }


                ////salvar dados de uniformes
                //Ilitera.Data.PPRA_EPI xUnif = new Ilitera.Data.PPRA_EPI();
                //xUnif.Excluir_Dados_Empregado_Uniforme(zId);

                //for (zCont = 0; zCont <= lst_Id_Uniforme_Medida_Sel.Items.Count - 1; zCont++)
                //{

                //    xUnif.Salvar_Dados_Empregado_Uniforme(zId, System.Convert.ToInt32(lst_Id_Uniforme_Medida_Sel.Items[zCont].ToString()), System.Convert.ToInt32(lbl_Id_Usuario.Text), lst_Sel_Uni.Items[zCont].ToString().Substring(0, 10));

                //}


                //salvar aptidoes
                Ilitera.Opsa.Data.Empregado_Aptidao xAptidao = new Ilitera.Opsa.Data.Empregado_Aptidao();
                xAptidao.Find(" nId_Empregado = " + zId.ToString());

                if (xAptidao.Id != 0)
                {
                    string zAptidao = "";

                    if (chk_apt_Altura.Checked == true)
                    {
                        xAptidao.apt_Trabalho_Altura = true;
                        zAptidao = zAptidao + "|Altura";
                    }
                    else xAptidao.apt_Trabalho_Altura = false;

                    if (chk_Apt_Alimento.Checked == true)
                    {
                        xAptidao.apt_Alimento = true;
                        zAptidao = zAptidao + "|Alimento";
                    }
                    else xAptidao.apt_Alimento = false;

                    if (chk_apt_Aquaviarios.Checked == true)
                    {
                        xAptidao.apt_Aquaviario = true;
                        zAptidao = zAptidao + "|Aquaviario";
                    }
                    else xAptidao.apt_Aquaviario = false;

                    if (chk_apt_Confinado.Checked == true)
                    {
                        xAptidao.apt_Espaco_Confinado = true;
                        zAptidao = zAptidao + "|Confinado";
                    }
                    else xAptidao.apt_Espaco_Confinado = false;

                    if (chk_apt_Eletricidade.Checked == true)
                    {
                        xAptidao.apt_Eletricidade = true;
                        zAptidao = zAptidao + "|Eletricidade";
                    }
                    else xAptidao.apt_Eletricidade = false;

                    if (chk_apt_Submerso.Checked == true)
                    {
                        xAptidao.apt_Submerso = true;
                        zAptidao = zAptidao + "|Submerso";
                    }
                    else xAptidao.apt_Submerso = false;

                    if (chk_Apt_Brigadista.Checked == true)
                    {
                        xAptidao.apt_Brigadista = true;
                        zAptidao = zAptidao + "|Brigadista";
                    }
                    else xAptidao.apt_Brigadista = false;

                    if (chk_Apt_Socorrista.Checked == true)
                    {
                        xAptidao.apt_Socorrista = true;
                        zAptidao = zAptidao + "|Socorrista";
                    }
                    else xAptidao.apt_Socorrista = false;

                    if (chk_Apt_Respirador.Checked == true)
                    {
                        xAptidao.apt_Respirador = true;
                        zAptidao = zAptidao + "|Respirador";
                    }
                    else xAptidao.apt_Respirador = false;


                    if (chk_Apt_Radiacao.Checked == true)
                    {
                        xAptidao.apt_Radiacao = true;
                        zAptidao = zAptidao + "|Radiacao";
                    }
                    else xAptidao.apt_Radiacao = false;

                    if (chk_apt_Transportes.Checked == true)
                    {
                        xAptidao.apt_Transporte = true;
                        zAptidao = zAptidao + "|Transportes";
                    }
                    else xAptidao.apt_Transporte = false;

                    xAptidao.Save();

                    if (zAptidao == "") zAptidao = "Sem Aptidão";

                    Log_Web("Aptidão atualizada ( " + zAptidao + " )  Colaborador:" + txtNomeEmpregado.Text
                            , System.Convert.ToInt32(lbl_Id_Usuario.Text), "Edição Colaborador - Web - Aptidão");

                }
                else
                {
                    if (chk_Apt_Alimento.Checked == true || chk_apt_Altura.Checked == true || chk_apt_Aquaviarios.Checked == true || chk_apt_Confinado.Checked == true ||
                         chk_apt_Eletricidade.Checked == true || chk_apt_Submerso.Checked == true || chk_apt_Transportes.Checked == true || chk_Apt_Brigadista.Checked == true || chk_Apt_Socorrista.Checked == true || chk_Apt_Respirador.Checked == true || chk_Apt_Radiacao.Checked == true)
                    {
                        string zAptidao = "";

                        Ilitera.Opsa.Data.Empregado_Aptidao xAptidao2 = new Ilitera.Opsa.Data.Empregado_Aptidao();
                        xAptidao2.nId_Empregado = zId;

                        if (chk_apt_Altura.Checked == true)
                        {
                            xAptidao2.apt_Trabalho_Altura = true;
                            zAptidao = zAptidao + "|Altura";
                        }
                        else xAptidao2.apt_Trabalho_Altura = false;

                        if (chk_Apt_Alimento.Checked == true)
                        {
                            xAptidao2.apt_Alimento = true;
                            zAptidao = zAptidao + "|Alimento";
                        }
                        else xAptidao2.apt_Alimento = false;

                        if (chk_apt_Aquaviarios.Checked == true)
                        {
                            xAptidao2.apt_Aquaviario = true;
                            zAptidao = zAptidao + "|Aquaviario";
                        }
                        else xAptidao2.apt_Aquaviario = false;

                        if (chk_apt_Confinado.Checked == true)
                        {
                            xAptidao2.apt_Espaco_Confinado = true;
                            zAptidao = zAptidao + "|Confinado";
                        }
                        else xAptidao2.apt_Espaco_Confinado = false;

                        if (chk_apt_Eletricidade.Checked == true)
                        {
                            xAptidao2.apt_Eletricidade = true;
                            zAptidao = zAptidao + "|Eletricidade";
                        }
                        else xAptidao2.apt_Eletricidade = false;

                        if (chk_apt_Submerso.Checked == true)
                        {
                            xAptidao2.apt_Submerso = true;
                            zAptidao = zAptidao + "|Submerso";
                        }
                        else xAptidao2.apt_Submerso = false;

                        if (chk_apt_Transportes.Checked == true)
                        {
                            xAptidao2.apt_Transporte = true;
                            zAptidao = zAptidao + "|Transporte";
                        }
                        else xAptidao2.apt_Transporte = false;

                        if (chk_Apt_Brigadista.Checked == true)
                        {
                            xAptidao2.apt_Brigadista = true;
                            zAptidao = zAptidao + "|Brigadista";
                        }
                        else xAptidao2.apt_Brigadista = false;

                        if (chk_Apt_Socorrista.Checked == true)
                        {
                            xAptidao2.apt_Socorrista = true;
                            zAptidao = zAptidao + "|Socorrista";
                        }
                        else xAptidao2.apt_Socorrista = false;

                        if (chk_Apt_Respirador.Checked == true)
                        {
                            xAptidao2.apt_Respirador = true;
                            zAptidao = zAptidao + "|Respirador";
                        }
                        else xAptidao2.apt_Respirador = false;

                        if (chk_Apt_Radiacao.Checked == true)
                        {
                            xAptidao2.apt_Radiacao = true;
                            zAptidao = zAptidao + "|Radiacao";
                        }
                        else xAptidao2.apt_Radiacao = false;


                        if (zAptidao == "") zAptidao = "Sem Aptidão";
                        
                        xAptidao2.Save();

                        Log_Web("Aptidão atualizada ( " + zAptidao + " )  Colaborador:" + txtNomeEmpregado.Text
                                , System.Convert.ToInt32(lbl_Id_Usuario.Text), "Edição Colaborador - Web - Aptidão");


                    }
                }



                Response.Redirect("~/ListaEmpregados.aspx");
            }

        }

        //}



        protected void cmd_Refresh_Click(object sender, EventArgs e)
        {

            Ilitera.Opsa.Data.Empregado xempregado = new Ilitera.Opsa.Data.Empregado();

            xempregado.Find(" tblEMPREGADO.nID_EMPREGADO = " + Session["Empregado"].ToString());

            string xArquivo;
            string xAux;
            ImgFunc.ImageUrl = "";



            if (txtFoto.Text.Trim() == "0")
            {
                xArquivo = "";
            }
            else
            {
                xAux = txtFoto.Text.Trim();

                for (int xCont = xAux.Length; xCont < xempregado.FotoTamanho; xCont++)
                {
                    xAux = "0" + xAux;
                }

                xArquivo = xempregado.FotoDiretorio + "\\" + xempregado.FotoInicio + xAux + xempregado.FotoExtensao;
                if (xArquivo.ToUpper().IndexOf("FOTOSDOCSDIGITAIS") < 1)
                {
                    xArquivo = xArquivo.Substring(0, xArquivo.IndexOf("\\") + 1) + "FOTOSDOCSDIGITAIS\\" + xArquivo.Substring(xArquivo.IndexOf("\\") + 1);
                }


                if (System.IO.File.Exists(Ilitera.Common.Fotos.PathFoto_Uri(xArquivo)))
                {
                    ImgFunc.ImageUrl = "data:image;base64," + Convert.ToBase64String(System.IO.File.ReadAllBytes(Ilitera.Common.Fotos.PathFoto_Uri(xArquivo)));
                }

                //ImgFunc.ImageUrl = Ilitera.Common.Fotos.PathFoto_Uri(xArquivo);

            }

            
        }


        //protected void cmd_Absentismo_Click(object sender, EventArgs e)
        //{

        //    Response.Redirect("~/PCMSO/ListaAcidentes2.aspx?Tipo=2");

        //}

        protected void btnAdd_Click(object sender, EventArgs e)
        {

            if ( Validar_Data( txtMateriais.Text)==false)
            {
                return;
            }

            int xQtde = 0;

            try
            {
               xQtde = System.Convert.ToInt16(txtQtde.Text);
            }
            catch( Exception ex)
            {
                MsgBox1.Show("Ilitera.Net", "Quantidade inválida.", null,
                  new EO.Web.MsgBoxButton("OK"));
                System.Diagnostics.Debug.WriteLine(ex.Message); 
                return;
            }

            if ( xQtde<1)
            {
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "alerta", String.Format("alert('{0}')", "Selecione Uniforme/Tamanho a ser adicionado."), true);
                MsgBox1.Show("Ilitera.Net", "Quantidade inválida.", null,
                  new EO.Web.MsgBoxButton("OK"));

                return;
            }


            if (lst_Uniformes.SelectedIndex < 0)
            {
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "alerta", String.Format("alert('{0}')", "Selecione Uniforme/Tamanho a ser adicionado."), true);
                MsgBox1.Show("Ilitera.Net", "Selecione Uniforme/Tamanho a ser adicionado", null,
                  new EO.Web.MsgBoxButton("OK"));                   

                return;
            }

            Boolean zLoc = false;

            //for (int zCont = 0; zCont < lst_Sel_Uni.Items.Count; zCont++)
            //{
            //    if (lst_Id_Uniforme_Sel.Items[zCont].ToString().Trim() == lst_Id_Uniforme.Items[lst_Uniformes.SelectedIndex].ToString().Trim())
            //    {
            //        zLoc = true;
            //    }
            //}

            //for (int zCont = 0; zCont < lst_Sel_Uni.Items.Count; zCont++)
            //{
            //    if (lst_Id_Uniforme_Medida_Sel.Items[zCont].ToString().Trim() == lst_Id_Uniforme_Medida.Items[lst_Uniformes.SelectedIndex].ToString().Trim())
            //    {
            //        zLoc = true;
            //    }
            //}


            string rQtde = xQtde.ToString().Trim();

            if (rQtde.Length == 1) rQtde = "00" + rQtde;
            else if (rQtde.Length == 2) rQtde = "0" + rQtde;


            if (zLoc == false)
            {
                lst_Sel_Uni.Items.Add(txtMateriais.Text + "  " + rQtde + "  " + lst_Uniformes.Items[lst_Uniformes.SelectedIndex].ToString().Trim());
                lst_Id_Uniforme_Sel.Items.Add(lst_Id_Uniforme.Items[lst_Uniformes.SelectedIndex].ToString().Trim());
                lst_Id_Uniforme_Medida_Sel.Items.Add(lst_Id_Uniforme_Medida.Items[lst_Uniformes.SelectedIndex].ToString().Trim());
            }


            //salvar dados de uniformes

            Int32 zId = System.Convert.ToInt32(Session["Empregado"].ToString());

            Ilitera.Data.PPRA_EPI xUnif = new Ilitera.Data.PPRA_EPI();
            xUnif.Excluir_Dados_Empregado_Uniforme(zId);

            for (int zCont = 0; zCont <= lst_Id_Uniforme_Medida_Sel.Items.Count - 1; zCont++)
            {

                xUnif.Salvar_Dados_Empregado_Uniforme(zId, System.Convert.ToInt32(lst_Id_Uniforme_Medida_Sel.Items[zCont].ToString()), System.Convert.ToInt32(lbl_Id_Usuario.Text), lst_Sel_Uni.Items[zCont].ToString().Substring(0, 10), System.Convert.ToInt16(lst_Sel_Uni.Items[zCont].ToString().Substring(12, 4)));

            }


        }

        protected void btnRemove_Click(object sender, EventArgs e)
        {

            if (lst_Sel_Uni.SelectedIndex < 0)
            {
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "alerta", String.Format("alert('{0}')", "Selecione Medida a ser retirada."), true);
                MsgBox1.Show("Ilitera.Net", "Selecione Medida a ser retirada.", null,
                  new EO.Web.MsgBoxButton("OK"));                   

                return;
            }

            lst_Id_Uniforme_Sel.Items.RemoveAt(lst_Sel_Uni.SelectedIndex);
            lst_Id_Uniforme_Medida_Sel.Items.RemoveAt(lst_Sel_Uni.SelectedIndex);
            lst_Sel_Uni.Items.RemoveAt(lst_Sel_Uni.SelectedIndex);


            //salvar dados de uniformes

            Int32 zId = System.Convert.ToInt32(Session["Empregado"].ToString());

            Ilitera.Data.PPRA_EPI xUnif = new Ilitera.Data.PPRA_EPI();
            xUnif.Excluir_Dados_Empregado_Uniforme(zId);

            for (int zCont = 0; zCont <= lst_Id_Uniforme_Medida_Sel.Items.Count - 1; zCont++)
            {

                xUnif.Salvar_Dados_Empregado_Uniforme(zId, System.Convert.ToInt32(lst_Id_Uniforme_Medida_Sel.Items[zCont].ToString()), System.Convert.ToInt32(lbl_Id_Usuario.Text), lst_Sel_Uni.Items[zCont].ToString().Substring(0, 10), System.Convert.ToInt16( lst_Sel_Uni.Items[zCont].ToString().Substring(12,4)));

            }

        }

        protected void lst_PPRA_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (lst_PPRA.SelectedIndex >= 0)
            {

                lst_11.Items.Clear();
                lst_11_ID.Items.Clear();

                for (int fcont = 0; fcont < lst_1.Items.Count; fcont++)
                {

                    if (lst_1.Items[fcont].ToString().Substring(0, 10) == lst_PPRA.Items[lst_PPRA.SelectedIndex].ToString())
                    {
                        lst_11.Items.Add(lst_1.Items[fcont].ToString().Substring(11).Trim());
                        lst_11_ID.Items.Add(lst_1_ID.Items[fcont].ToString().Trim());
                    }

                }

                lst_Sel_11.Items.Clear();
                lst_Sel_11_Id.Items.Clear();

                for (int fcont = 0; fcont < lst_Sel_1.Items.Count; fcont++)
                {

                    if (lst_Sel_1.Items[fcont].ToString().Substring(0, 10) == lst_PPRA.Items[lst_PPRA.SelectedIndex].ToString())
                    {
                        lst_Sel_11.Items.Add(lst_Sel_1.Items[fcont].ToString().Substring(11).Trim());
                        lst_Sel_11_Id.Items.Add(lst_Sel_1_Id.Items[fcont].ToString().Trim());
                    }

                }

            }

        }

        protected void cmd_Add11_Click(object sender, EventArgs e)
        {
            if (lst_11.SelectedIndex < 0) return;

            if (lst_Sel_11.Items.Count > 0)
            {
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Alerta", "Permitido apenas 1 GHE por Laudo.", true);
                MsgBox1.Show("Ilitera.Net", "Permitido apenas 1 GHE por Laudo.", null,
                  new EO.Web.MsgBoxButton("OK"));                   

                return;
            }


            for (int zCont = 0; zCont < lst_Sel_11.Items.Count; zCont++)
            {
                if (lst_11.Items[lst_11.SelectedIndex].ToString() == lst_Sel_11.Items[zCont].ToString())
                    return;
            }


            Ilitera.Opsa.Data.Cliente cliente2;
            cliente2 = new Ilitera.Opsa.Data.Cliente(System.Convert.ToInt32(Session["Empresa"].ToString()));


            bool xRealizar_Checagem = true;

            Ilitera.Opsa.Data.EmpregadoFuncao xEmpregadoFuncao = new Ilitera.Opsa.Data.EmpregadoFuncao(System.Convert.ToInt32(lbl_Id.Text));

            xEmpregadoFuncao.nID_EMPREGADO.Find();

            if (xEmpregadoFuncao.nID_EMPREGADO.hDT_DEM != new DateTime())
            {
                if (cliente2.Envio_2240_Demitidos == true)
                {
                    xRealizar_Checagem = false;
                }
            }



            Ilitera.Opsa.Data.LaudoTecnico vLaudo = new Ilitera.Opsa.Data.LaudoTecnico(System.Convert.ToInt32(lst_Id_PPRA.Items[lst_PPRA.SelectedIndex].ToString()));

            if (vLaudo.Id != 0)
            {
                if (xEmpregadoFuncao.hDT_TERMINO.Year > 2000 && xEmpregadoFuncao.hDT_TERMINO < vLaudo.hDT_LAUDO)
                {
                    StringBuilder st = new StringBuilder();
                    st.Append("window.alert('Essa alocação do colaborador não pode ser realizada em laudo posterior ao encerramento da classif.funcional.')");
                    this.ClientScript.RegisterStartupScript(this.GetType(), "Alocação", st.ToString(), true);

                    return;

                }

                if (xEmpregadoFuncao.hDT_INICIO.Year > 2000 && xEmpregadoFuncao.hDT_INICIO > vLaudo.hDT_LAUDO.AddYears(2))
                {
                    StringBuilder st = new StringBuilder();
                    st.Append("window.alert('Essa alocação do colaborador não pode ser realizada em laudo já vencido.')");
                    this.ClientScript.RegisterStartupScript(this.GetType(), "Alocação", st.ToString(), true);

                    return;
                }

            }




            if (xRealizar_Checagem == true)
            {
                Ilitera.Data.eSocial xChecagem = new Ilitera.Data.eSocial();
                if (xChecagem.Checar_Alocacao_eSocial_Anterior(System.Convert.ToInt32(lbl_Id.Text), "2240", cliente2.ESocial_Ambiente, System.Convert.ToInt32(lst_Id_PPRA.Items[lst_PPRA.SelectedIndex].ToString())) > 0)
                {
                    StringBuilder st = new StringBuilder();
                    st.Append("window.alert('Essa alocação do colaborador não pode ser realizada pois há evento posterior criado associado ao e-Social.Primeiro exclua os eventos posteriores já enviados.')");
                    this.ClientScript.RegisterStartupScript(this.GetType(), "Alocação", st.ToString(), true);
                    return;
                }
            }



            lst_Sel_11_Id.Items.Clear();
            lst_Sel_11.Items.Clear();

            lst_Sel_11.Items.Add(lst_11.Items[lst_11.SelectedIndex].ToString());
            lst_Sel_11_Id.Items.Add(lst_11_ID.Items[lst_11.SelectedIndex].ToString());


            Ilitera.Data.PPRA_EPI xGHE1_1 = new Ilitera.Data.PPRA_EPI();

            //inserir           
            //xGHE1_1.Salvar_GHE_Classif_Funcional(System.Convert.ToInt32(lbl_Id.Text), System.Convert.ToInt32(lst_Sel_11_Id.Items[0].ToString()), System.Convert.ToInt32(Session["Empregado"].ToString()), System.Convert.ToInt32(Session["Empresa"].ToString()), lst_PPRA.Items[lst_PPRA.SelectedIndex].ToString().Trim());
            xGHE1_1.Salvar_GHE_Classif_Funcional(System.Convert.ToInt32(lbl_Id.Text), System.Convert.ToInt32(lst_Sel_11_Id.Items[0].ToString()), System.Convert.ToInt32(Session["Empregado"].ToString()), System.Convert.ToInt32(lbl_Local_Trabalho.Text), lst_PPRA.Items[lst_PPRA.SelectedIndex].ToString().Trim());


            Log_Web("Adicionar Colaborador em GHE    Laudo:" + lst_PPRA.Items[lst_PPRA.SelectedIndex].ToString() + "    GHE:" + lst_11.Items[lst_11.SelectedIndex].ToString() + "  Colaborador:" + txtNomeEmpregado.Text
                    , System.Convert.ToInt32(lbl_Id_Usuario.Text), "Edição Colaborador - Web");


            //enviar email alerta - alocação colaborador
            if (cliente2.Alerta_web_Alocar_Colaborador!=null && cliente2.Alerta_web_Alocar_Colaborador.Trim() != "")
            {

                System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");
                Entities.Usuario usuario = (Entities.Usuario)Session["usuarioLogado"];

                string xCorpo = "<body><p><font size='2' face='Tahoma'><H1 align='center'>Alerta de Alocação de Colaborador - Ili.Net</H1></font></p> <br></br>" +
                                "<p><font size='3' face='Tahoma'>Nome: " + txtNomeEmpregado.Text + "<br>" +
                                "Empresa:  " + cliente2.NomeAbreviado + "<br>" +
                                "Laudo: " + lst_PPRA.Items[lst_PPRA.SelectedIndex].ToString() + "<br>" +
                                "GHE: " + lst_11.Items[lst_11.SelectedIndex].ToString() + "<br><br>" +
                                "Usuário responsável: " + usuario.NomeUsuario.ToUpper() + " em " + System.DateTime.Now.ToString("dd/MM/yyyy hh:mm", ptBr) + "<br></font></p></body>";


                if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().IndexOf("Prajna") > 0)
                {
                    Envio_Email_Prajna(cliente2.Alerta_web_Inserir_Colaborador.Trim(), "wagner@ilitera.com.br", "Alerta - Alocação de Colaborador em GHE", xCorpo, "", "Alocação de colaborador", xEmpregadoFuncao.nID_EMPREGADO.Id, System.Convert.ToInt32(lbl_Id_Empresa.Text));
                }
                else
                {
                    if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("GLOBAL") > 0)
                        Envio_Email_Global(cliente2.Alerta_web_Inserir_Colaborador.Trim(), "wagner@ilitera.com.br", "Alerta - Alocação de Colaborador em GHE", xCorpo, "", "Alocação de colaborador", xEmpregadoFuncao.nID_EMPREGADO.Id, System.Convert.ToInt32(lbl_Id_Empresa.Text));
                    else
                        Envio_Email_Ilitera(cliente2.Alerta_web_Inserir_Colaborador.Trim(), "wagner@ilitera.com.br", "Alerta - Alocação de Colaborador em GHE", xCorpo, "", "Alocação de colaborador", xEmpregadoFuncao.nID_EMPREGADO.Id, System.Convert.ToInt32(lbl_Id_Empresa.Text));

                    
                }
            }





            //carregar GHEs para a classif.funcional
            Ilitera.Data.PPRA_EPI xGHE2 = new Ilitera.Data.PPRA_EPI();

            DataSet xdS2 = xGHE2.Trazer_Laudos_GHEs_Salvos(System.Convert.ToInt32(lbl_Id.Text));

            lst_Sel_1.Items.Clear();
            lst_Sel_1_Cop.Items.Clear();
            lst_Sel_1_Id.Items.Clear();

            foreach (DataRow row in xdS2.Tables[0].Rows)
            {
                lst_Sel_1.Items.Add(row["Laudo"].ToString() + "  " + row["GHE"].ToString());
                lst_Sel_1_Cop.Items.Add(row["Laudo"].ToString() + "  " + row["GHE"].ToString());

                lst_Sel_1_Id.Items.Add(row["nId_Func"].ToString());
            }


            object xsender = new object();
            EventArgs xe = new EventArgs();
            lst_PPRA_SelectedIndexChanged(xsender, xe);

        }


        protected void Envio_Email_Ilitera(string xPara, string xCopia, string xSubject, string xBody, string xAttach, string xModulo, Int32 xIdEmpregado, Int32 xIdEmpresa)
        {
            string xDestinatario = "";
            //enviar e-mail para clinica e prajna

            MailMessage objEmail = new MailMessage();

            //rementente do email
            //objEmail.From = new MailAddress("agendamento.sp.sto@ilitera.com.br");


            string[] stringSeparators4 = new string[] { ";" };
            string[] result4;
            result4 = xPara.Trim().Split(stringSeparators4, StringSplitOptions.RemoveEmptyEntries);

            foreach (string s in result4)
            {
                if (s.Trim() != "")
                {
                    objEmail.To.Add(s);
                }
            }



            string[] stringSeparators5 = new string[] { ";" };
            string[] result5;
            result5 = xCopia.Trim().Split(stringSeparators5, StringSplitOptions.RemoveEmptyEntries);

            foreach (string s in result5)
            {
                if (s.Trim() != "")
                {
                    objEmail.CC.Add(s);
                }
            }





            objEmail.Priority = MailPriority.Normal;
            objEmail.IsBodyHtml = true;

            objEmail.Subject = xSubject;
            objEmail.Body = xBody;
            objEmail.SubjectEncoding = Encoding.GetEncoding("ISO-8859-1");
            objEmail.BodyEncoding = Encoding.GetEncoding("ISO-8859-1");

            if (xAttach != "")
            {
                Attachment xItem = new Attachment(xAttach);
                objEmail.Attachments.Add(xItem);
            }

            SmtpClient objSmtp = new SmtpClient();
            //objSmtp.Host = "smtp.ilitera.com.br";
            objSmtp.Host = "smtp.office365.com";
            objSmtp.EnableSsl = true;
            objSmtp.Port = 587;

            if (System.DateTime.Now.Second % 3 == 0)
            {
                objEmail.From = new MailAddress("agendamento1@ilitera.com.br");
                objSmtp.Credentials = new NetworkCredential("agendamento1@ilitera.com.br", "Ilitera_3624");
            }
            else
            {
                objEmail.From = new MailAddress("agendamento3@ilitera.com.br");
                objSmtp.Credentials = new NetworkCredential("agendamento3@ilitera.com.br", "Ilitera_3625");
            }

            objEmail.ReplyTo = new MailAddress("agendamento@ilitera.com.br");


            Ilitera.Data.Clientes_Clinicas xEnvio = new Ilitera.Data.Clientes_Clinicas();


            objSmtp.Send(objEmail);
            xEnvio.Envio_eMail(xIdEmpregado, xIdEmpresa, xDestinatario, "Alerta Alocação Colaborador web");



            return;

        }



        protected void Envio_Email_Global(string xPara, string xCopia, string xSubject, string xBody, string xAttach, string xModulo, Int32 xIdEmpregado, Int32 xIdEmpresa)
        {
            string xDestinatario = "";
            //enviar e-mail para clinica e prajna

            MailMessage objEmail = new MailMessage();

            //rementente do email
            //objEmail.From = new MailAddress("agendamento.sp.sto@ilitera.com.br");


            string[] stringSeparators4 = new string[] { ";" };
            string[] result4;
            result4 = xPara.Trim().Split(stringSeparators4, StringSplitOptions.RemoveEmptyEntries);

            foreach (string s in result4)
            {
                if (s.Trim() != "")
                {
                    objEmail.To.Add(s);
                }
            }



            string[] stringSeparators5 = new string[] { ";" };
            string[] result5;
            result5 = xCopia.Trim().Split(stringSeparators5, StringSplitOptions.RemoveEmptyEntries);

            foreach (string s in result5)
            {
                if (s.Trim() != "")
                {
                    objEmail.CC.Add(s);
                }
            }





            objEmail.Priority = MailPriority.Normal;
            objEmail.IsBodyHtml = true;

            objEmail.Subject = xSubject;
            objEmail.Body = xBody;
            objEmail.SubjectEncoding = Encoding.GetEncoding("ISO-8859-1");
            objEmail.BodyEncoding = Encoding.GetEncoding("ISO-8859-1");

            if (xAttach != "")
            {
                Attachment xItem = new Attachment(xAttach);
                objEmail.Attachments.Add(xItem);
            }

            SmtpClient objSmtp = new SmtpClient();
            objSmtp.Host = "smtp.globalsegmed.com.br";
            objSmtp.Port = 587;


            Ilitera.Common.Juridica xJur = new Ilitera.Common.Juridica();
            xJur.Find(xIdEmpresa);

            if (xJur.Id != 0)
            {
                if (xJur.Auxiliar == "RAMO" || xJur.Auxiliar == "GLOBAL2")
                {
                    //objEmail.From = new MailAddress("guias@globalsegmed.com.br");
                    //objSmtp.Credentials = new NetworkCredential("guias@globalsegmed.com.br", "Sergio2024@");

                    //objEmail.ReplyTo = new MailAddress("guias@globalsegmed.com.br");

                    //if (xPara.IndexOf("asos@globalsegmed.com.br") < 0)
                    //{
                    //    objEmail.CC.Add("asos@globalsegmed.com.br");
                    //}
                    objSmtp.Host = "smtp.ramoassessoria.com.br";
                    objEmail.From = new MailAddress("guias@ramoassessoria.com.br");
                    objSmtp.Credentials = new NetworkCredential("guias@ramoassessoria.com.br", "Ramo@2024");

                    objEmail.ReplyTo = new MailAddress("guias@ramoassessoria.com.br");
                }
                //else if (xJur.Auxiliar == "GLOBAL2")
                //{
                //    objEmail.From = new MailAddress("guias2@globalsegmed.com.br");
                //    objSmtp.Credentials = new NetworkCredential("guias2@globalsegmed.com.br", "Guiasglobal2#");

                //    objEmail.ReplyTo = new MailAddress("guias2@globalsegmed.com.br");
                //}
                else
                {
                    objEmail.From = new MailAddress("guias@globalsegmed.com.br");
                    objSmtp.Credentials = new NetworkCredential("guias@globalsegmed.com.br", "Sergio2024@");

                    objEmail.ReplyTo = new MailAddress("guias@globalsegmed.com.br");
                }
            }
            else
            {
                objEmail.From = new MailAddress("guias@globalsegmed.com.br");
                objSmtp.Credentials = new NetworkCredential("guias@globalsegmed.com.br", "Sergio2024@");

                objEmail.ReplyTo = new MailAddress("guias@globalsegmed.com.br");
            }



            //objEmail.From = new MailAddress("guias2@globalsegmed.com.br");            
            //objSmtp.Credentials = new NetworkCredential("guias2@globalsegmed.com.br", "Guiasglobal2#");

            //objEmail.ReplyTo = new MailAddress("guias2@globalsegmed.com.br");


            Ilitera.Data.Clientes_Clinicas xEnvio = new Ilitera.Data.Clientes_Clinicas();


            objSmtp.Send(objEmail);
            xEnvio.Envio_eMail(xIdEmpregado, xIdEmpresa, xDestinatario, "Alerta Alocação Colaborador web");



            return;

        }



        protected void Envio_Email_Prajna(string xPara, string xCopia, string xSubject, string xBody, string xAttach, string xModulo, Int32 xIdEmpregado, Int32 xIdEmpresa)
        {

            string xDestinatario = "";


            MailMessage objEmail = new MailMessage();

            //rementente do email           
            //objEmail.From = new MailAddress("agendamento@5aessence.com.br");
            objEmail.From = new MailAddress("agendamento@essencenet.com.br");



            string[] stringSeparators4 = new string[] { ";" };
            string[] result4;
            result4 = xPara.Trim().Split(stringSeparators4, StringSplitOptions.RemoveEmptyEntries);

            foreach (string s in result4)
            {
                if (s.Trim() != "")
                {
                    objEmail.To.Add(s);
                }
            }



            string[] stringSeparators5 = new string[] { ";" };
            string[] result5;
            result5 = xCopia.Trim().Split(stringSeparators5, StringSplitOptions.RemoveEmptyEntries);

            foreach (string s in result5)
            {
                if (s.Trim() != "")
                {
                    objEmail.CC.Add(s);
                }
            }



            objEmail.Priority = MailPriority.Normal;
            objEmail.IsBodyHtml = true;

            objEmail.Subject = xSubject;
            objEmail.Body = xBody;
            objEmail.SubjectEncoding = Encoding.GetEncoding("ISO-8859-1");
            objEmail.BodyEncoding = Encoding.GetEncoding("ISO-8859-1");

            if (xAttach != "")
            {
                Attachment xItem = new Attachment(xAttach);
                objEmail.Attachments.Add(xItem);
            }

            SmtpClient objSmtp = new SmtpClient();
            objSmtp.Host = "mail.exchange.locaweb.com.br";
            objSmtp.Port = 587;
            objSmtp.Credentials = new NetworkCredential("agendamento@essencenet.com.br", "Vug31145");

            objSmtp.EnableSsl = false;

            //objSmtp.Host = "outlook.office.com";


            //objSmtp.Host = "smtp.office365.com";
            //objSmtp.Port = 587;                       
            //objSmtp.Credentials = new NetworkCredential("agendamento@5aessence.com.br", "Agend_5060");

            //objSmtp.Host = "smtp.5aessencenet.com.br";
            //objSmtp.Port = 587;
            //objSmtp.Credentials = new NetworkCredential("agendamento@5aessencenet.com.br", "Prana@2022!@");

            //objSmtp.Send(objEmail);

            //Ilitera.Data.Clientes_Clinicas xEnvio = new Ilitera.Data.Clientes_Clinicas();
            //xEnvio.Envio_eMail(xIdEmpregado, xIdEmpresa, xDestinatario, "Alerta Alocação Colaborador web");

            return;

        }





        private void Log_Web(string Command,
                            int IdUsuario,
                            string ProcessoRealizado)
        {

            using (SqlConnection cnn = new SqlConnection(Ilitera.Data.SQLServer.EntitySQLServer.GetConnection()))
            {

                string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection();



                string strLog = "USE logdb exec dbo.sps_AddLog_OPSA "
                                + IdUsuario + ","
                                + "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',"
                                + "'0',"
                                + "'0',"
                                + "0,"
                                + "1,"
                                + "'" + Command.Replace("'", "''") + "',"
                                + "'" + ProcessoRealizado + "'";

                try
                {
                    //Debug.WriteLine(strLog);
                    cnn.ConnectionString = connString;
                    cnn.Open();

                    SqlCommand cmd = new SqlCommand();

                    cmd.CommandText = strLog;

                    cmd.Connection = cnn;

                    cmd.ExecuteNonQuery(); //DESENVOLVIMENTO ILITERA - linha ao lado comentada (acessava tab de logs) 29/07/10
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                }

            }

        }



        protected void cmd_Remove11_Click(object sender, EventArgs e)
        {
            if (lst_Sel_11.SelectedIndex < 0) return;

            lst_Sel_11_Id.Items.Clear();
            lst_Sel_11.Items.Clear();

            Ilitera.Data.PPRA_EPI xGHE1 = new Ilitera.Data.PPRA_EPI();


            Ilitera.Opsa.Data.Cliente xCliente2 = new Ilitera.Opsa.Data.Cliente();
            xCliente2.Find(System.Convert.ToInt32(Session["Empresa"]));

            Ilitera.Data.eSocial xChecagem = new Ilitera.Data.eSocial();
            if (xChecagem.Checar_Classif_Funcional_eSocial(System.Convert.ToInt32(lbl_Id.Text), "2240", xCliente2.ESocial_Ambiente) > 0)
            {                
                StringBuilder st = new StringBuilder();
                st.Append("window.alert('Essa classif.funcional do colaborador tem evento criado associado ao e-Social, não é possível excluir.')");
                this.ClientScript.RegisterStartupScript(this.GetType(), "Alocação", st.ToString(), true);

                return;
            }
            else
            {

                //primeiro apagar
                //xGHE1.Excluir_GHE_Classif_Funcional_Laudo(System.Convert.ToInt32(lbl_Id.Text), System.Convert.ToInt32(Session["Empregado"].ToString()), System.Convert.ToInt32(Session["Empresa"].ToString()), lst_PPRA.Items[lst_PPRA.SelectedIndex].ToString().Trim());
                xGHE1.Excluir_GHE_Classif_Funcional_Laudo(System.Convert.ToInt32(lbl_Id.Text), System.Convert.ToInt32(Session["Empregado"].ToString()), System.Convert.ToInt32(lbl_Local_Trabalho.Text), lst_PPRA.Items[lst_PPRA.SelectedIndex].ToString().Trim());

                //carregar GHEs para a classif.funcional
                Ilitera.Data.PPRA_EPI xGHE2 = new Ilitera.Data.PPRA_EPI();

                DataSet xdS2 = xGHE2.Trazer_Laudos_GHEs_Salvos(System.Convert.ToInt32(lbl_Id.Text));

                lst_Sel_1.Items.Clear();
                lst_Sel_1_Cop.Items.Clear();
                lst_Sel_1_Id.Items.Clear();

                foreach (DataRow row in xdS2.Tables[0].Rows)
                {
                    lst_Sel_1.Items.Add(row["Laudo"].ToString() + "  " + row["GHE"].ToString());
                    lst_Sel_1_Cop.Items.Add(row["Laudo"].ToString() + "  " + row["GHE"].ToString());

                    lst_Sel_1_Id.Items.Add(row["nId_Func"].ToString());
                }
            }


            object xsender = new object();
            EventArgs xe = new EventArgs();
            lst_PPRA_SelectedIndexChanged(xsender, xe);

        }

        protected void Button1_Click(object sender, EventArgs e)
        {

            string xExtension = File1.FileName.Substring(File1.FileName.Length - 3, 3).ToUpper().Trim();

            if (xExtension == "PDF" || xExtension == "JPG")
            {
                Session["zFoto"] = File1.FileBytes;
                txt_Arq.Text = File1.FileName.ToString();
            }

        }

        protected void cmd_Voltar_Click(object sender, EventArgs e)
        {
            Response.Redirect("ListaEmpregados.aspx");
        }

        protected void cmd_Relat_Abs_Click(object sender, EventArgs e)
        {
            AbreRelat("RelatAbsenteismo");

        }


        private void AbreRelat(string page)
        {
            Guid strAux = Guid.NewGuid();

            OpenReport("Documentos", page + ".aspx?IliteraSystem=" + strAux.ToString() + strAux.ToString()
                    + "&IdEmpregado=" + Session["Empregado"].ToString() + "&IdUsuario=" + Request["IdUsuario"] + "&Data1=" + txt_D1.Text + "&Data2=" + txt_D2.Text, page);
        }


        private void AbreRelatMat(string page, string zMaterial)
        {
            Guid strAux = Guid.NewGuid();

            OpenReport("Documentos", page + ".aspx?IliteraSystem=" + strAux.ToString() + strAux.ToString()
                    + "&IdEmpregado=" + Session["Empregado"].ToString() + "&IdUsuario=" + Request["IdUsuario"] + "&Material="+zMaterial, page);
        }

        protected void OpenReport(string directory, string fileAndQuery, string ReportName)
        {
            this.OpenReport(directory, fileAndQuery, ReportName, false);
        }

        protected void OpenReport(string directory, string fileAndQuery, string ReportName, bool useDirectoryForLocalProcess)
        {
            StringBuilder st = new StringBuilder();

            Guid strAux = Guid.NewGuid();

            string valueProcess = "Local";

            if (valueProcess.Equals("Remote"))
                st.Append("void(window.open('http://report.ilitera.net/index.aspx?Identity=" + strAux.ToString() + "opsa" + strAux.ToString()
                    + "&Key=" + strAux.ToString() + "MestraNet" + strAux.ToString()
                    + "&PathAndQuery=" + HttpUtility.UrlEncode(directory + "/" + fileAndQuery) + "', '" + ReportName + "', 'scrollbars=yes, resizable=yes, toolbar=no, addressbar=no, menubar=no, width=800px, height=600px'));");
            else if (valueProcess.Equals("Local"))
            {
                if (useDirectoryForLocalProcess)
                {
                    //st.Append("void(window.open('../" + directory + "/" + fileAndQuery + "', '" + ReportName + "', 'scrollbars=yes, resizable=yes, toolbar=no, addressbar=no, menubar=no, width=800px, height=600px'));");
                    st.AppendFormat("void(window.open('../{0}/{1}', '{2}','scrollbars=yes, resizable=yes, toolbar=no, addressbar=no, menubar=no, width=800px, height=600px'));", directory, fileAndQuery, ReportName);
                }
                else
                {
                    //st.Append("void(window.open('" + fileAndQuery + "', '" + ReportName + "', 'scrollbars=yes, resizable=yes, toolbar=no, addressbar=no, menubar=no, width=800px, height=600px'));");
                    st.AppendFormat("void(window.open('{0}','{1}', 'scrollbars=yes, resizable=yes, toolbar=no, addressbar=no, menubar=no, width=800px, height=600px'));", fileAndQuery, ReportName);
                }
            }

            ScriptManager.RegisterStartupScript(this, this.GetType(), String.Format("OpenReport{0}", ReportName), st.ToString(), true);
        }



        public bool Valida_CPF(string cpf)
        {

            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            string tempCpf;

            string digito;

            int soma;

            int resto;

            cpf = cpf.Trim();

            cpf = cpf.Replace(".", "").Replace("-", "");

            if (cpf.Length != 11)

                return false;

            tempCpf = cpf.Substring(0, 9);

            soma = 0;

            for (int i = 0; i < 9; i++)

                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];

            resto = soma % 11;

            if (resto < 2)

                resto = 0;

            else

                resto = 11 - resto;

            digito = resto.ToString();

            tempCpf = tempCpf + digito;

            soma = 0;

            for (int i = 0; i < 10; i++)

                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];

            resto = soma % 11;

            if (resto < 2)

                resto = 0;

            else

                resto = 11 - resto;

            digito = digito + resto.ToString();

            return cpf.EndsWith(digito);

        }


        public static bool Valida_PIS(string pis)
        {
            int[] multiplicador = new int[10] { 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int soma;
            int resto;
            if (pis.Trim().Length != 11)
                return false;
            pis = pis.Trim();
            pis = pis.Replace("-", "").Replace(".", "").PadLeft(11, '0');

            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(pis[i].ToString()) * multiplicador[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            return pis.EndsWith(resto.ToString());
        }


        private void PopulaTransferencia()
        {


            ArrayList list;

            Ilitera.Opsa.Data.Cliente xCliente = new Ilitera.Opsa.Data.Cliente();
            xCliente.Find(System.Convert.ToInt32(Session["Empresa"]));

            cmb_Transferencia.Items.Clear();
            lst_Transferencia.Items.Clear();


            if (xCliente.IdGrupoEmpresa.Id != 0)
                list = new Juridica().Find("IdGrupoEmpresa=" + xCliente.IdGrupoEmpresa.Id + " and IdJuridica<>" + xCliente.Id.ToString() + " ORDER BY NomeAbreviado");
            else
                return;


            foreach (Juridica juridica in list)
            {
                if (juridica.IsInativo == false)
                {
                    lst_Transferencia.Items.Add(juridica.Id.ToString());
                    cmb_Transferencia.Items.Add(juridica.NomeAbreviado);
                }
            }

            return;


        }


        private void PopulaComboLocalTrabalho()
        {
            //this.ultrCmbEdtrEmpresa.SelectionChangeCommitted -= new System.EventHandler(this.ultrCmbEdtrEmpresa_SelectionChangeCommitted);
                       

            Ilitera.Opsa.Data.Cliente xCliente = new Ilitera.Opsa.Data.Cliente();
            xCliente.Find(System.Convert.ToInt32(Session["Empresa"]));


            cmbLocalTrabalho.Items.Clear();
            lstLocalTrabalho.Items.Clear();

            cmbLocalTrabalho.Items.Add("Próprio Local de Trabalho");
            lstLocalTrabalho.Items.Add(xCliente.Id.ToString());


            Ilitera.Opsa.Data.EmpregadoFuncao emprFuncao;
            emprFuncao = new Ilitera.Opsa.Data.EmpregadoFuncao();
            emprFuncao.Find( System.Convert.ToInt32( lbl_Id_1.Text) );
            List < Ilitera.Opsa.Data.Cliente > list = emprFuncao.FindListaLocaisDeTrabalho("");

            //emprFuncao.nID_FUNCAO.NumeroCBO            

            
            foreach (Juridica juridica in list)
            {
                //if (juridica.Id != clienteEmpregado.Id)
                cmbLocalTrabalho.Items.Add(juridica.NomeAbreviado);
                lstLocalTrabalho.Items.Add(juridica.Id.ToString());
            }

            //this.ultrCmbEdtrEmpresa.AutoComplete = true;
            //this.ultrCmbEdtrEmpresa.AutoSize = true;
            //this.ultrCmbEdtrEmpresa.SortStyle = Infragistics.Win.ValueListSortStyle.Ascending;

            //this.ultrCmbEdtrEmpresa.SelectionChangeCommitted += new System.EventHandler(this.ultrCmbEdtrEmpresa_SelectionChangeCommitted);

            //this.ultrCmbEdtrEmpresa.ReadOnly = IsBloquearTrocaLocalDeTrabalho();
            //this.ultrTxtEdtrPesquisa.ReadOnly = this.ultrCmbEdtrEmpresa.ReadOnly;
        }



        protected void cmd_Editar_Setor_Funcao_Cargo_Click(object sender, EventArgs e)
        {


            //lbl_Id.Text  - classif.funcional

            txtInicioFuncao.ReadOnly = true;
            txtTerminoFuncao.ReadOnly = false;
            txtCargo.ReadOnly = true;
            txtFuncao.ReadOnly = true;
            txtSetor.ReadOnly = true;
            txtJornada.ReadOnly = true;
            txtCC.ReadOnly = true;
            txtLocalTrabalho.ReadOnly = true;

            txtInicioFuncao.BackColor = System.Drawing.ColorTranslator.FromHtml("#DFDFDF");
            txtTerminoFuncao.BackColor = System.Drawing.ColorTranslator.FromHtml("#DFDFDF");
            txtCargo.BackColor = System.Drawing.ColorTranslator.FromHtml("#DFDFDF");
            txtFuncao.BackColor = System.Drawing.ColorTranslator.FromHtml("#DFDFDF");
            txtSetor.BackColor = System.Drawing.ColorTranslator.FromHtml("#DFDFDF");
            txtJornada.BackColor = System.Drawing.ColorTranslator.FromHtml("#DFDFDF");
            txtCC.BackColor = System.Drawing.ColorTranslator.FromHtml("#DFDFDF");
            txtLocalTrabalho.BackColor = System.Drawing.ColorTranslator.FromHtml("#DFDFDF");

           

            cmb_Setor1.Visible = true;
            cmb_Cargo1.Visible = true;
            cmb_Funcao1.Visible = true;
            cmb_Jornada.Visible = true;
            cmbLocalTrabalho.Visible = false;
            cmb_Setor1.SelectedIndex = 0;
            cmb_Cargo1.SelectedIndex = 0;
            cmb_Funcao1.SelectedIndex = 0;
            //cmb_Jornada.SelectedIndex = 0;
            //cmbLocalTrabalho.SelectedIndex = 0;

            cmdGHE_1.Visible = false;
            

            cmd_Excluir1.Enabled = false;
            cmd_Alterar1.Enabled = true;
            cmd_Alterar1.Visible = true;

            lst_1.Visible = false;
            lst_Sel_1.Visible = false;
            cmd_Add_1.Visible = false;
            cmd_Remove_1.Visible = false;

            lst_PPRA.Visible = false;
            lbl_PPRA0.Visible = false;
            lst_11.Visible = false;
            lst_Sel_11.Visible = false;
            cmd_Add11.Visible = false;
            cmd_Remove11.Visible = false;
            lbl_Selecao.Visible = false;
            lbl_Selecionado.Visible = false;
            //txtLocalTrabalho.Visible = false;
            txtLocalTrabalho.Visible = true;



            cmb_Setor1.Items.Clear();

            cmb_Cargo1.Items.Clear();

            cmb_Funcao1.Items.Clear();



            Ilitera.Data.PPRA_EPI xGHE = new Ilitera.Data.PPRA_EPI();

            DataSet dS1 = xGHE.Carregar_Setores(System.Convert.ToInt32(System.Convert.ToInt32(lstLocalTrabalho.Items[cmbLocalTrabalho.SelectedIndex].Value)));

            cmb_Setor1.DataSource = dS1;
            cmb_Setor1.DataValueField = "nID_SETOR";
            cmb_Setor1.DataTextField = "tNO_STR_EMPR";
            cmb_Setor1.DataBind();


            Ilitera.Opsa.Data.Cliente rCliente = new Ilitera.Opsa.Data.Cliente();
            rCliente.Find(System.Convert.ToInt32(Session["Empresa"].ToString()));

            if (rCliente.NomeAbreviado.ToUpper().IndexOf("VITERRA") >= 0)
            {
                DataSet dS2 = xGHE.Carregar_Funcoes_Viterra(System.Convert.ToInt32(System.Convert.ToInt32(lstLocalTrabalho.Items[cmbLocalTrabalho.SelectedIndex].Value)));
                cmb_Funcao1.DataSource = dS2;
                cmb_Funcao1.DataValueField = "IdFuncao";
                cmb_Funcao1.DataTextField = "NomeFuncao";
                cmb_Funcao1.DataBind();
            }
            else
            {
                DataSet dS2 = xGHE.Carregar_Funcoes(System.Convert.ToInt32(System.Convert.ToInt32(lstLocalTrabalho.Items[cmbLocalTrabalho.SelectedIndex].Value)));
                cmb_Funcao1.DataSource = dS2;
                cmb_Funcao1.DataValueField = "IdFuncao";
                cmb_Funcao1.DataTextField = "NomeFuncao";
                cmb_Funcao1.DataBind();
            }

            DataSet dS3 = xGHE.Carregar_Cargos(System.Convert.ToInt32(System.Convert.ToInt32(lstLocalTrabalho.Items[cmbLocalTrabalho.SelectedIndex].Value)));
            cmb_Cargo1.DataSource = dS3;
            cmb_Cargo1.DataValueField = "nID_CARGO";
            cmb_Cargo1.DataTextField = "tNO_CARGO";
            cmb_Cargo1.DataBind();


            for ( int zCont=0; zCont<cmb_Setor1.Items.Count; zCont++)
            {
                if ( txtSetor.Text.ToUpper().Trim() == cmb_Setor1.Items[zCont].ToString().ToUpper().Trim())
                {
                    cmb_Setor1.SelectedIndex = zCont;
                    break;
                }
            }



            for (int zCont = 0; zCont < cmb_Funcao1.Items.Count; zCont++)
            {
                string xFuncao = cmb_Funcao1.Items[zCont].ToString().ToUpper().Trim();

                if (xFuncao.IndexOf("|") > 0)
                {
                    xFuncao = xFuncao.Substring(0, xFuncao.IndexOf("|") - 1);
                }

                if (txtFuncao.Text.ToUpper().Trim() == xFuncao)
                {
                    cmb_Funcao1.SelectedIndex = zCont;
                    break;
                }
            }



            for (int zCont = 0; zCont < cmb_Cargo1.Items.Count; zCont++)
            {
                if (txtCargo.Text.ToUpper().Trim() == cmb_Cargo1.Items[zCont].ToString().ToUpper().Trim())
                {
                    cmb_Cargo1.SelectedIndex = zCont;
                    break;
                }
            }


            for (int zCont = 0; zCont <  cmb_Jornada.Items.Count; zCont++)
            {
                if (txtJornada.Text.ToUpper().Trim() == cmb_Jornada.Items[zCont].ToString().ToUpper().Trim())
                {
                    cmb_Jornada.SelectedIndex = zCont;
                    break;
                }
            }

            


            for (int zCont = 0; zCont < cmbLocalTrabalho.Items.Count; zCont++)
            {
                if ( txtLocalTrabalho.Text.ToUpper().Trim() == cmbLocalTrabalho.Items[zCont].ToString().ToUpper().Trim())
                {
                    cmbLocalTrabalho.SelectedIndex = zCont;
                    break;
                }
            }



            return;

        }



        protected void cmbLocalTrabalho_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (cmbLocalTrabalho.SelectedIndex < 0) return;


            cmb_Setor1.Items.Clear();

            cmb_Cargo1.Items.Clear();

            cmb_Funcao1.Items.Clear();

            

            Ilitera.Data.PPRA_EPI xGHE = new Ilitera.Data.PPRA_EPI();

            DataSet dS1 = xGHE.Carregar_Setores(System.Convert.ToInt32(System.Convert.ToInt32(lstLocalTrabalho.Items[cmbLocalTrabalho.SelectedIndex].Value)));

            cmb_Setor1.DataSource = dS1;
            cmb_Setor1.DataValueField = "nID_SETOR";
            cmb_Setor1.DataTextField = "tNO_STR_EMPR";
            cmb_Setor1.DataBind();

            Ilitera.Opsa.Data.Cliente rCliente = new Ilitera.Opsa.Data.Cliente();
            rCliente.Find(System.Convert.ToInt32(Session["Empresa"].ToString()));

            if (rCliente.NomeAbreviado.ToUpper().IndexOf("VITERRA") >= 0 )                
            {
                DataSet dS2 = xGHE.Carregar_Funcoes_Viterra(System.Convert.ToInt32(System.Convert.ToInt32(lstLocalTrabalho.Items[cmbLocalTrabalho.SelectedIndex].Value)));
                cmb_Funcao1.DataSource = dS2;
                cmb_Funcao1.DataValueField = "IdFuncao";
                cmb_Funcao1.DataTextField = "NomeFuncao";
                cmb_Funcao1.DataBind();
            }
            else
            {
                DataSet dS2 = xGHE.Carregar_Funcoes(System.Convert.ToInt32(System.Convert.ToInt32(lstLocalTrabalho.Items[cmbLocalTrabalho.SelectedIndex].Value)));
                cmb_Funcao1.DataSource = dS2;
                cmb_Funcao1.DataValueField = "IdFuncao";
                cmb_Funcao1.DataTextField = "NomeFuncao";
                cmb_Funcao1.DataBind();
            }

            DataSet dS3 = xGHE.Carregar_Cargos(System.Convert.ToInt32(System.Convert.ToInt32(lstLocalTrabalho.Items[cmbLocalTrabalho.SelectedIndex].Value)));
            cmb_Cargo1.DataSource = dS3;
            cmb_Cargo1.DataValueField = "nID_CARGO";
            cmb_Cargo1.DataTextField = "tNO_CARGO";
            cmb_Cargo1.DataBind();

            return;

        }

        protected void cmdRecibo_Click(object sender, EventArgs e)
        {

            //if (lst_Sel_Uni.SelectedIndex < 0) return;
            string zItems = "";

            for ( int zCont=0; zCont<lst_Sel_Uni.Items.Count; zCont++)
            {
                if ( lst_Sel_Uni.Items[zCont].Selected==true)
                {
                    zItems = zItems + lst_Sel_Uni.Items[zCont].ToString().Substring(0,16) + lst_Id_Uniforme_Sel.Items[zCont].ToString().Trim() + ";";
                    
                }

            }

            if (zItems == "") return;

            //AbreRelatMat("RelatMateriais", lst_Sel_Uni.Items[lst_Sel_Uni.SelectedIndex].ToString().Trim());

            AbreRelatMat("RelatMateriais", zItems);


            return;
            
        }

        protected void TabStrip1_ItemClick(object sender, EO.Web.NavigationItemEventArgs e)
        {
        }


        private void Carga_eSocial()
        {


            Empregado empregado = new Empregado();
            empregado = EmpregadoFacade.RetornarEmpregadoDadosCadastrais(Convert.ToInt32(Session["Empregado"]));

            DataSet rDs = new DataSet();

            Ilitera.Data.eSocial zDados = new Ilitera.Data.eSocial();
            rDs = zDados.Dados_Colaborador_eSocial(empregado.Cpf.Trim());

            lst_eSocial.Items.Clear();

            for (int rCont = 0; rCont < rDs.Tables[0].Rows.Count; rCont++)
            {
                lst_eSocial.Items.Add(rDs.Tables[0].Rows[rCont]["DataEnvio"].ToString().Trim() + "......" +
                                      rDs.Tables[0].Rows[rCont]["Evento"].ToString().Trim() + "....." +
                                      rDs.Tables[0].Rows[rCont]["Ambiente"].ToString().Trim() + "....." +
                                      rDs.Tables[0].Rows[rCont]["Status"].ToString().Trim());

            }

            if (lst_eSocial.Items.Count == 0)
            {
                lst_eSocial.Items.Add("Sem ocorrências.");
            }

        }



        protected void cmd_Transferir_Click(object sender, EventArgs e)
        {

            string xErro = "";

            if (cmb_Transferencia.SelectedIndex < 0) return;



            if (txtDataTransf.Text.Trim() != "")
            {
                if (Validar_Data(txtDataTransf.Text.Trim()) == false)
                {
                    xErro = "Data de Transferência Inválida.";
                }
            }
            else
            {
                xErro = "Data de Transferência Inválida.";
            }


            if (xErro == "")
            {
                System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");
                DateTime zTransf = System.Convert.ToDateTime(txtDataTransf.Text, ptBr);
                DateTime zInicio = System.Convert.ToDateTime(txtDataAdmissao.Text, ptBr);

                if (zTransf < zInicio)
                {
                    xErro = "Data de transferência anterior à data de admissão.";
                }
            }

            if (xErro == "")
            {
                Realizar_Transferencia();
            }
            else
            {
                MsgBox1.Show("Ilitera.Net", xErro, null,
                new EO.Web.MsgBoxButton("OK"));
                return;
            }


        }



        private void Realizar_Transferencia()
        {

            string IdClienteDestino = "";
            Ilitera.Opsa.Data.Empregado empregado = new Ilitera.Opsa.Data.Empregado();
            Ilitera.Opsa.Data.Empregado newEmpregado = new Ilitera.Opsa.Data.Empregado();

            System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");
            DateTime Admissao = System.Convert.ToDateTime(txtDataTransf.Text, ptBr);
            DateTime Demissao = Admissao.AddDays(-1);


            try
            {

                empregado.Find(Convert.ToInt32(Session["Empregado"]));

                IdClienteDestino = lst_Transferencia.Items[cmb_Transferencia.SelectedIndex].ToString().Trim();


                

                string where = "nID_EMPR=" + IdClienteDestino
                            + " AND tNO_EMPG='" + empregado.tNO_EMPG.Replace("'", "''").Trim() + "'";

                newEmpregado.Find(where);

                int IdEmpregado = newEmpregado.Id;

                newEmpregado = (Ilitera.Opsa.Data.Empregado)empregado.Clone();

                Ilitera.Opsa.Data.Cliente zCliente = new Ilitera.Opsa.Data.Cliente();
                zCliente.Find(System.Convert.ToInt32(IdClienteDestino));


                newEmpregado.Id = IdEmpregado;
                //newEmpregado.nID_EMPR.Id = IdClienteDestino;
                newEmpregado.nID_EMPR = zCliente;


                if ( chk_Admissao_Origem.Checked == false )
                   newEmpregado.hDT_ADM = Admissao;
                else
                   newEmpregado.hDT_ADM = empregado.hDT_ADM;

                newEmpregado.isBrigadista = false;

                if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().IndexOf("Prajna") < 0)
                {                    
                    empregado.isBrigadista = true;
                }



                //Demite o empregado
                empregado.hDT_DEM = Demissao;
                empregado.Save();


                newEmpregado.Save();

                System.Threading.Thread.Sleep(400);

            }
            catch (Exception ex)
            {
                MsgBox1.Show("Ilitera.Net", "Erro na Transferência(1):" + ex.Message, null,
                            new EO.Web.MsgBoxButton("OK"));
                return;
            }



            try
            {
                CopiaProntuarioDigital(empregado, newEmpregado);
            }
            catch (Exception ex)
            {
                MsgBox1.Show("Ilitera.Net", "Erro na Transferência (2):" + ex.Message, null,
                            new EO.Web.MsgBoxButton("OK"));
                return;
            }


            Ilitera.Opsa.Data.EmpregadoFuncao newEmpregadoFuncao = new Ilitera.Opsa.Data.EmpregadoFuncao();

            try
            {
                bool ComClassificacaoFuncional = true;               


                if (ComClassificacaoFuncional)
                {
                    bool xFirst = true;
                    ArrayList list;

                    //if (ComDemissaoReadmissao)
                    //    list = new EmpregadoFuncao().Find("nID_EMPREGADO=" + empregado.Id + " AND hDT_TERMINO IS NULL");
                    //else
                    list = new Ilitera.Opsa.Data.EmpregadoFuncao().Find("nID_EMPREGADO=" + empregado.Id + " order by hdt_Inicio desc");

                    foreach (Ilitera.Opsa.Data.EmpregadoFuncao empregadoFuncao in list)
                    {


                        int IdLocalDeTrabalho = 0;
                        Int32 IdClienteOrigem = System.Convert.ToInt32(Session["Empresa"]);


                        if (empregadoFuncao.nID_EMPR.Id == IdClienteOrigem)
                            IdLocalDeTrabalho = System.Convert.ToInt32(IdClienteDestino);
                        else
                            IdLocalDeTrabalho = empregadoFuncao.nID_EMPR.Id;

                        Ilitera.Opsa.Data.Cliente xCliente = new Ilitera.Opsa.Data.Cliente(System.Convert.ToInt32(IdClienteDestino));

                        empregadoFuncao.nID_FUNCAO.Find();

                        Ilitera.Opsa.Data.Funcao funcao = new Ilitera.Opsa.Data.Funcao();
                        Ilitera.Opsa.Data.Funcao xFuncao = new Ilitera.Opsa.Data.Funcao();
                        funcao.Find("IdCliente=" + IdClienteDestino
                            + " AND NomeFuncao='" + empregadoFuncao.nID_FUNCAO.NomeFuncao.Replace("'", "''").Trim() + "'");

                        if (funcao.Id == 0)
                        {

                            xFuncao = (Ilitera.Opsa.Data.Funcao)empregadoFuncao.nID_FUNCAO.Clone();

                            while (xFuncao.IdCliente != xCliente)
                            {
                                xFuncao.IdCliente.Find(xCliente.Id);
                                xFuncao.IdCliente.Id = xCliente.Id;
                                xFuncao.IdCliente = xCliente;
                            }


                            xFuncao.Id = 0;
                            xFuncao.Save();
                            while (xFuncao.IdCliente != xCliente)
                            {
                                xFuncao.IdCliente.Find(xCliente.Id);
                                xFuncao.IdCliente.Id = xCliente.Id;
                                xFuncao.IdCliente = xCliente;
                            }
                            xFuncao.Save();

                        }
                        else
                        {
                            xFuncao.Id = funcao.Id;
                        }

                        empregadoFuncao.nID_SETOR.Find();

                        Ilitera.Opsa.Data.Setor setor = new Ilitera.Opsa.Data.Setor();
                        Ilitera.Opsa.Data.Setor xSetor = new Ilitera.Opsa.Data.Setor();

                        setor.Find("nID_EMPR=" + IdClienteDestino
                            + " AND tNO_STR_EMPR='" + empregadoFuncao.nID_SETOR.tNO_STR_EMPR.Replace("'", "''").Trim() + "'");

                        if (setor.Id == 0)
                        {

                            xSetor = (Ilitera.Opsa.Data.Setor)empregadoFuncao.nID_SETOR.Clone();

                            while (xSetor.nID_EMPR != xCliente)
                            {
                                xSetor.nID_EMPR.Find(xCliente.Id);
                                xSetor.nID_EMPR.Id = xCliente.Id;
                                xSetor.nID_EMPR = xCliente;

                            }

                            xSetor.Id = 0;
                            xSetor.Save();
                            while (xSetor.nID_EMPR != xCliente)
                            {
                                xSetor.nID_EMPR.Find(xCliente.Id);
                                xSetor.nID_EMPR.Id = xCliente.Id;
                                xSetor.nID_EMPR = xCliente;

                            }
                            xSetor.Save();
                        }
                        else
                        {
                            xSetor.Id = setor.Id;
                        }


                        empregadoFuncao.nID_CARGO.Find();

                        Ilitera.Opsa.Data.Cargo xCargo = new Ilitera.Opsa.Data.Cargo();

                        if (empregadoFuncao.nID_CARGO.tNO_CARGO.Trim() != "")
                        {
                            Ilitera.Opsa.Data.Cargo cargo = new Ilitera.Opsa.Data.Cargo();
                            cargo.Find("nID_EMPR=" + IdClienteDestino
                                + " AND tNO_Cargo='" + empregadoFuncao.nID_CARGO.tNO_CARGO.Replace("'", "''").Trim() + "'");

                            if (cargo.Id == 0)
                            {

                                xCargo = (Ilitera.Opsa.Data.Cargo)empregadoFuncao.nID_CARGO.Clone();

                                while (xCargo.nID_EMPR != xCliente)
                                {
                                    xCargo.nID_EMPR.Find(xCliente.Id);
                                    xCargo.nID_EMPR.Id = xCliente.Id;
                                    xCargo.nID_EMPR = xCliente;
                                }

                                xCargo.Id = 0;
                                xCargo.Save();
                                while (xCargo.nID_EMPR != xCliente)
                                {
                                    xCargo.nID_EMPR.Find(xCliente.Id);
                                    xCargo.nID_EMPR.Id = xCliente.Id;
                                    xCargo.nID_EMPR = xCliente;
                                }
                                xCargo.Save();
                            }
                            else
                            {
                                xCargo.Id = cargo.Id;
                            }
                        }

                        System.Threading.Thread.Sleep(400);

                        newEmpregadoFuncao.Find("nID_EMPREGADO=" + newEmpregado.Id
                                                + " AND nID_EMPR=" + IdClienteDestino
                                                + " AND nID_FUNCAO=" + xFuncao.Id
                                                + " AND nID_SETOR=" + xSetor.Id);

                        if (newEmpregadoFuncao.Id == 0)
                        {

                            newEmpregadoFuncao.Find("nID_EMPREGADO=" + newEmpregado.Id
                                               + " AND hDT_Inicio = convert( smalldatetime,'" + empregadoFuncao.hDT_INICIO.ToString("dd/MM/yyyy",ptBr).Substring(0, 10) + "', 103 )");

                            if (newEmpregadoFuncao.Id == 0)
                            {

                                newEmpregadoFuncao = (Ilitera.Opsa.Data.EmpregadoFuncao)empregadoFuncao.Clone();
                                newEmpregadoFuncao.Id = 0;
                                newEmpregadoFuncao.nID_EMPR.Id = IdLocalDeTrabalho;
                                newEmpregadoFuncao.nID_EMPREGADO.Id = newEmpregado.Id;
                                newEmpregadoFuncao.nID_FUNCAO.Id = xFuncao.Id;
                                newEmpregadoFuncao.nID_SETOR.Id = xSetor.Id;
                                newEmpregadoFuncao.nID_CARGO.Id = xCargo.Id;


                                newEmpregadoFuncao.hDT_INICIO = Admissao;
                                newEmpregadoFuncao.hDT_TERMINO = new DateTime();


                                empregadoFuncao.hDT_TERMINO = Demissao;
                                empregadoFuncao.Save();


                                newEmpregadoFuncao.Save();



                                //fazer alocação automática se for o primeiro
                                if (xFirst == true)
                                {
                                    xFirst = false;

                                    newEmpregadoFuncao.nID_EMPREGADO.Find();
                                    Int32 nIdEmpregadoId = newEmpregadoFuncao.nID_EMPREGADO.Id;

                                    newEmpregadoFuncao.nID_FUNCAO.Find();
                                    string xNomeFuncao = newEmpregadoFuncao.nID_FUNCAO.NomeFuncao;

                                    newEmpregadoFuncao.nID_EMPR.Find();
                                    Int32 xIdEmpr = newEmpregadoFuncao.nID_EMPR.Id;

                                    newEmpregadoFuncao.Save();

                                    Ilitera.Data.Empregado_Cadastral xFuncao_GHE = new Ilitera.Data.Empregado_Cadastral();
                                    bool xAlocacao = xFuncao_GHE.Criar_Alocacao_Funcao_GHE(xIdEmpr, xNomeFuncao, nIdEmpregadoId);

                                }


                                break;  //usar apenas última classif.funcional

                            }

                        }





                    }
                }
            }
            catch (Exception ex)
            {
                MsgBox1.Show("Ilitera.Net", "Erro na Transferência (3):" + ex.Message, null,
                            new EO.Web.MsgBoxButton("OK"));
                return;
            }


            try
            {
                bool ComProntuarioDigital = true;

                if (ComProntuarioDigital)
                {
                    //Copia Exames Realizados
                    ArrayList listExames = new Ilitera.Opsa.Data.ExameBase().Find("IdEmpregado=" + empregado.Id.ToString());
                    foreach (Ilitera.Opsa.Data.ExameBase exameBase in listExames)
                    {
                        Ilitera.Opsa.Data.ExameBase xExame = new Ilitera.Opsa.Data.ExameBase();
                        Ilitera.Opsa.Data.Audiometria xAud = new Ilitera.Opsa.Data.Audiometria();
                        Ilitera.Opsa.Data.Clinico xClin = new Ilitera.Opsa.Data.Clinico();
                        Ilitera.Opsa.Data.Complementar xCompl = new Ilitera.Opsa.Data.Complementar();
                        Ilitera.Opsa.Data.ClinicoNaoOcupacional xNaoOC = new Ilitera.Opsa.Data.ClinicoNaoOcupacional();

                        xExame = (Ilitera.Opsa.Data.ExameBase)exameBase.Clone();
                        //xExame.LiberarPagamento = false;

                        //xExame.Tirar_eSocial = true;

                        //xExame.IdPagamentoClinica.Id = 0;
                        //xExame.IdPagamentoClinica.Find();

                        if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().IndexOf("Prajna") < 0)
                        {
                            //xExame.LiberarPagamento = true;                            
                            xExame.IdPagamentoClinica.Find();
                            if (xExame.IdPagamentoClinica.Id == 0)
                            {
                                //xExame.LiberarPagamento = true;
                                xExame.IdPagamentoClinica.Id = 1000;
                                xExame.IdPagamentoClinica.Find();
                            }
                                                       

                        }


                        xExame.Save();



                        if (xExame.IdExameDicionario.ToString() == "Audiometria")
                        {
                            xAud.Find(" ExameBase.IdExameBase = " + xExame.Id);
                        }
                        else if (xExame.IdExameDicionario.Id >= 1 && xExame.IdExameDicionario.Id <= 5)
                        {
                            xClin.Find(" ExameBase.IdExameBase = " + xExame.Id);
                        }
                        else if (xExame.IdExameDicionario.Id == 7)
                        {
                            xNaoOC.Find(" ExameBase.IdExameBase = " + xExame.Id);
                        }
                        else
                        {
                            xCompl.Find(" ExameBase.IdExameBase = " + xExame.Id);
                        }

                        //xExame.Id = 0;
                        //xExame.IdEmpregado.Id = newEmpregado.Id;
                        //xExame.Save();


                        if (xExame.IdExameDicionario.ToString() == "Audiometria")
                        {
                            Ilitera.Opsa.Data.Audiometria xAud2 = new Ilitera.Opsa.Data.Audiometria();
                            xAud2 = (Ilitera.Opsa.Data.Audiometria)xAud.Clone();
                            xAud2.IdEmpregado = newEmpregado;
                            xAud2.Id = 0;
                            xAud2.Save();

                            //ver se há prontuáriodigital para novo colaborador com IdExameBase antigo e ajustar
                            Ilitera.Opsa.Data.ProntuarioDigital rPront = new Ilitera.Opsa.Data.ProntuarioDigital();
                            rPront.Find(" IdEmpregado = " + newEmpregado.Id + " and IdExameBase = " + xAud.Id);
                            if (rPront.Id != 0)
                            {
                                Ilitera.Opsa.Data.ExameBase rEB = new Ilitera.Opsa.Data.ExameBase(xAud2.Id);
                                if (rEB.Id != 0)
                                {
                                    rPront.IdExameBase = rEB;
                                    rPront.Save();
                                }
                            }

                        }
                        else if (xExame.IdExameDicionario.Id >= 1 && xExame.IdExameDicionario.Id <= 5)
                        {
                            Ilitera.Opsa.Data.Clinico xClin2 = new Ilitera.Opsa.Data.Clinico();
                            xClin2 = (Ilitera.Opsa.Data.Clinico)xClin.Clone();
                            xClin2.IdEmpregado = newEmpregado;
                            xClin2.IdEmpregadoFuncao = newEmpregadoFuncao;
                            xClin2.Id = 0;
                            xClin2.Save();

                            //ajustar data resultado, para pegar da origem, e não colocar getdate() - isso atrapalha rotina faturamento - Wagner 13/06/2018
                            Ilitera.Data.Clientes_Clinicas xAtualizacao = new Ilitera.Data.Clientes_Clinicas();
                            xAtualizacao.Atualizar_Data_Resultado(xClin2.Id, exameBase.Id);

                            //ver se há prontuáriodigital para novo colaborador com IdExameBase antigo e ajustar
                            Ilitera.Opsa.Data.ProntuarioDigital rPront = new Ilitera.Opsa.Data.ProntuarioDigital();
                            rPront.Find(" IdEmpregado = " + newEmpregado.Id + " and IdExameBase = " + xClin.Id);
                            if (rPront.Id != 0)
                            {
                                Ilitera.Opsa.Data.ExameBase rEB = new Ilitera.Opsa.Data.ExameBase(xClin2.Id);
                                if (rEB.Id != 0)
                                {
                                    rPront.IdExameBase = rEB;
                                    rPront.Save();
                                }
                            }

                        }
                        else if (xExame.IdExameDicionario.Id == 7)
                        {
                            Ilitera.Opsa.Data.ClinicoNaoOcupacional xNO = new Ilitera.Opsa.Data.ClinicoNaoOcupacional();
                            xNO = (Ilitera.Opsa.Data.ClinicoNaoOcupacional)xNaoOC.Clone();
                            xNO.IdEmpregado = newEmpregado;
                            xNO.Id = 0;
                            xNO.Save();
                        }
                        else
                        {
                            Ilitera.Opsa.Data.Complementar xCompl2 = new Ilitera.Opsa.Data.Complementar();
                            xCompl2 = (Ilitera.Opsa.Data.Complementar)xCompl.Clone();
                            xCompl2.IdEmpregado = newEmpregado;
                            xCompl2.Id = 0;
                            xCompl2.Save();

                            //ver se há prontuáriodigital para novo colaborador com IdExameBase antigo e ajustar
                            Ilitera.Opsa.Data.ProntuarioDigital rPront = new Ilitera.Opsa.Data.ProntuarioDigital();
                            rPront.Find(" IdEmpregado = " + newEmpregado.Id + " and IdExameBase = " + xCompl.Id);
                            if (rPront.Id != 0)
                            {
                                Ilitera.Opsa.Data.ExameBase rEB = new Ilitera.Opsa.Data.ExameBase(xCompl2.Id);
                                if (rEB.Id != 0)
                                {
                                    rPront.IdExameBase = rEB;
                                    rPront.Save();
                                }
                            }


                        }


                    }
                }

            }
            catch (Exception ex)
            {
                MsgBox1.Show("Ilitera.Net", "Erro na Transferência (4):" + ex.Message, null,
                            new EO.Web.MsgBoxButton("OK"));
                return;
            }




            try
            {
                ArrayList listAbs = new Ilitera.Opsa.Data.Afastamento().Find("IdEmpregado=" + empregado.Id.ToString() + " order by DataInicial ");
                {


                    foreach (Ilitera.Opsa.Data.Afastamento Afast in listAbs)
                    {
                        Ilitera.Opsa.Data.Afastamento xAf = new Ilitera.Opsa.Data.Afastamento();

                        xAf = (Ilitera.Opsa.Data.Afastamento)Afast.Clone();

                        xAf.IdEmpregado.Id = newEmpregado.Id;
                        xAf.Id = 0;

                        xAf.Save();
                    }


                }

            }
            catch ( Exception ex)
            {
                MsgBox1.Show("Ilitera.Net", "Erro na Transferência (5):" + ex.Message, null,
                            new EO.Web.MsgBoxButton("OK"));
                return;
            }



            string xDetalhes = "";

            if ( chk_Admissao_Origem.Checked == true)
            {
                xDetalhes = xDetalhes + " Manter Data Admissão Origem-Sim |";
            }
            else
            {
                xDetalhes = xDetalhes + " Manter Data Admissão Origem-Não |";
            }

            if ( txtDataTransf.Text.Trim()!="")
            {
                xDetalhes = xDetalhes + " Data Transf - " + txtDataTransf.Text  + " |";
            }

            Log_Web("Origem - " + empregado.tNO_EMPG + "(" + empregado.Id.ToString() + " para " + newEmpregado.Id.ToString() + ") " + xDetalhes , System.Convert.ToInt32(lbl_Id_Usuario.Text), "Transferência Colaborador - Web");



            MsgBox1.Show("Ilitera.Net", "Transferência Realizada.  Não esquecer de alocar o colaborador no GHE/GTSRO da unidade para qual foi transferido.", null,
                        new EO.Web.MsgBoxButton("OK"));


            //Session["Empregado"] = newEmpregado.Id;
            //Session["NomeEmpregado"] = newEmpregado.tNO_EMPG;
            //Response.Redirect("~/ListaEmpregados.aspx");

            //precisaria selecionar a empresa destino
            //Response.Redirect("~/SelecaoGHE.aspx?Colaborador=" + txtNomeEmpregado.Text.Trim());



            return;

        }



        public static void CopiaProntuarioDigital(Ilitera.Opsa.Data.Empregado empregado, Ilitera.Opsa.Data.Empregado empregadoDestino)
        {
            List<Ilitera.Opsa.Data.ProntuarioDigital> prontuarios = new Ilitera.Opsa.Data.ProntuarioDigital().Find<Ilitera.Opsa.Data.ProntuarioDigital>("IdEmpregado=" + empregado.Id);

            foreach (Ilitera.Opsa.Data.ProntuarioDigital prontuarioDigital in prontuarios)
            {
                string whereProntuario = "IdEmpregado=" + empregadoDestino.Id
                                        + " AND Arquivo='" + prontuarioDigital.Arquivo + "'";

                int count = new Ilitera.Opsa.Data.ProntuarioDigital().ExecuteCount(whereProntuario);

                if (count > 0)
                    continue;



                try
                {
                    string xOrigem = prontuarioDigital.GetArquivo(empregado.nID_EMPR);

                    //System.IO.FileInfo arquivo = new System.IO.FileInfo(prontuarioDigital.GetArquivo(empregado.nID_EMPR));

                    string pathDestino = Ilitera.Opsa.Data.ProntuarioDigital.GetArquivo(empregadoDestino.nID_EMPR, prontuarioDigital.Arquivo);

                    if ( pathDestino.ToUpper().IndexOf("FOTOSDOCSDIGITAIS") < 0)
                    {
                       pathDestino = pathDestino.Replace("I:\\", "I:\\FOTOSDOCSDIGITAIS\\");
                    }

                    //System.IO.FileInfo arquivo2 = new System.IO.FileInfo(pathDestino); //ProntuarioDigital.GetArquivo(empregadoDestino.nID_EMPR, prontuarioDigital.Arquivo););

                    //if (arquivo2.Exists == false) //arquivo.CopyTo(pathDestino);

                    if (System.IO.File.Exists(xOrigem) && !System.IO.File.Exists(pathDestino))
                    {
                        System.IO.File.Copy(xOrigem, pathDestino);
                    }

                    Ilitera.Opsa.Data.ProntuarioDigital novoProntuarioDigital = (Ilitera.Opsa.Data.ProntuarioDigital)prontuarioDigital.Clone();
                    novoProntuarioDigital.Id = 0;
                    novoProntuarioDigital.IdEmpregado.Id = empregadoDestino.Id;
                    novoProntuarioDigital.Save();

                    System.Threading.Thread.Sleep(400);
                }
                catch (Exception ex)
                {

                }
                finally
                {
                }
                

            }

        }



        protected void cmb_Transferencia_SelectedIndexChanged(object sender, EventArgs e)
        {


        }


    }
}

