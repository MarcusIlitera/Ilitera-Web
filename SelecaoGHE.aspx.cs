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
using Ilitera.Data.SQLServer;
using System.Data.SqlClient;

using System.Net.Mail;
using System.Net;

using Entities;
using Facade;
using System.Text.RegularExpressions;
using BLL;
using Ilitera.Opsa.Data;


namespace Ilitera.Net
{
    public partial class SelecaoGHE : System.Web.UI.Page
    {
        //#region eventos

        protected Ilitera.Common.Prestador prestador = new Ilitera.Common.Prestador();
        protected Ilitera.Common.Usuario usuario = new Ilitera.Common.Usuario();
        protected Cliente cliente = new Cliente();
        protected Ilitera.Opsa.Data.Empregado empregado = new Ilitera.Opsa.Data.Empregado();

        private Int32 xIdEmpresa;
        private Int32 xIdUsuario;

        private string zEmpresa = "";


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

//            InicializaWebPageObjects();

           


            if (!IsPostBack)
            {
                //gridEmpregadossemGHE.Behaviors.CreateBehavior<Infragistics.Web.UI.GridControls.Sorting>();
                //this.gridEmpregadossemGHE.Behaviors.Sorting.SortedColumns.Add(this.gridEmpregadossemGHE.Columns["tNO_EMPG"], Infragistics.Web.UI.SortDirection.Ascending);

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

                Session["Id"]="";
                Session["Id2"]="";

                if (Session["Empresa"] != null)
                {

                    Usuario user = (Usuario)Session["usuarioLogado"];

                    xIdEmpresa = System.Convert.ToInt32(Session["Empresa"]);
                    xIdUsuario = user.IdUsuario;
                    zEmpresa = Session["NomeEmpresa"].ToString();

                    lbl_Empresa.Text = Session["NomeEmpresa"].ToString();

                    lbl_Id_Empresa.Text = xIdEmpresa.ToString().Trim();
                    lbl_Id_Usuario.Text = xIdUsuario.ToString().Trim();


                    chk_Finalizado.Checked = true;

                    if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("PRAJNA") > 0) 
                    {
                        chk_Finalizado.Checked = true;
                        chk_Finalizado.Enabled = false;
                    }

                    Carregar_Laudos();


                }
                else
                {
                    cmb_GHE.Enabled = false;
                    lst_PPRA.Enabled = false;
                    cmd_Add_1.Enabled = false;
                    cmd_Remove_1.Enabled = false;
                }


          
                
            }

        }



        protected void Carregar_Laudos()
        {
            int xAno1;
            int xAno2;
            string xFinalizados = "N";

            if (chk_Finalizado.Checked == true) xFinalizados = "S";


            lst_PPRA.Visible = true;
            lbl_Selecao.Visible = true;

            xAno1 = 1990;
            xAno2 = 2050;


            Ilitera.Data.PPRA_EPI xGHE = new Ilitera.Data.PPRA_EPI();

            DataSet xdS = xGHE.Trazer_Laudos_GHEs(xAno1, xAno2, System.Convert.ToInt32(Session["Empresa"].ToString()), xFinalizados);

            lst_1.Items.Clear();
            lst_1_ID.Items.Clear();

            lst_PPRA.Items.Clear();
            lst_Id_PPRA.Items.Clear();

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
                    lst_Id_PPRA.Items.Add( row["nId_Laud_Tec"].ToString().Trim());
                }


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



        protected void lst_PPRA_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (lst_PPRA.SelectedIndex >= 0)
            {

                lst_Id_GHE.Items.Clear();
                cmb_GHE.Items.Clear();

                for (int fcont = 0; fcont < lst_1.Items.Count; fcont++)
                {

                    if (lst_1.Items[fcont].ToString().Substring(0, 10) == lst_PPRA.Items[lst_PPRA.SelectedIndex].ToString())
                    {
                        cmb_GHE.Items.Add(lst_1.Items[fcont].ToString().Substring(11).Trim());
                        lst_Id_GHE.Items.Add(lst_1_ID.Items[fcont].ToString().Trim());
                    }

                }



                object xsender = new object();
                EventArgs xe = new EventArgs();                

                cmb_GHE_SelectedIndexChanged(xsender, xe);

                if (lst_Id_PPRA.Items[lst_PPRA.SelectedIndex].ToString().Trim() != "")
                {
                    PopularGridEmpregados(lst_Id_PPRA.Items[lst_PPRA.SelectedIndex].ToString().Trim());
                    if (cmb_GHE.SelectedIndex >= 0)
                        PopularGridEmpregadosExpostos(lst_Id_PPRA.Items[lst_PPRA.SelectedIndex].ToString().Trim(), lst_Id_GHE.Items[cmb_GHE.SelectedIndex].ToString().Trim());
                    else
                    {
                        gridEmpregados.DataSource = null;
                        gridEmpregados.DataBind();
                    }
                }
                else
                {
                    gridEmpregados.DataSource = null;
                    gridEmpregadossemGHE.DataSource = null;
                }
            }

        }






        
        private void PopularGridEmpregados( string xnId_Laudo_Tec)
        {
            //LayoutFiltroEmpregado();

            StringBuilder str = new StringBuilder();

            str.Append("SELECT nID_EMPREGADO_FUNCAO, nID_EMPREGADO, tNO_EMPG, tNO_FUNC_EMPR, tNO_STR_EMPR, CONVERT(char(10), hDT_INICIO, 103 ) as hDT_INICIO, convert( char(10),hDT_TERMINO, 103 ) as hDT_TERMINO, nID_EMPR, nID_FUNCAO, nID_SETOR, hDT_DEM, hDT_ADM, gTERCEIRO");
            str.Append(" FROM qryEmpregadoFuncao WHERE ");

            str.Append("nID_EMPR =" + lbl_Id_Empresa.Text);

            LaudoTecnico xlaudoTecnico = new LaudoTecnico(System.Convert.ToInt32(xnId_Laudo_Tec));

            int rAnos = 1;

            if (xlaudoTecnico.hDT_LAUDO.Year >= 2021)
            {
                rAnos = 2;
            }



            System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");


            ArrayList alLaudos = new LaudoTecnico().Find("nID_EMPR =" + lbl_Id_Empresa.Text + " and hDt_Laudo > convert( smalldatetime, '" + xlaudoTecnico.hDT_LAUDO.ToString("dd/MM/yyyy", ptBr) + "' ,103 )  order by hDt_Laudo ");
            LaudoTecnico xlaudoTecnicoPosterior = new LaudoTecnico();

            foreach (LaudoTecnico xlaudo in alLaudos)
            {
                xlaudoTecnicoPosterior.Find(xlaudo.Id);
                break;   
            }



                //"and datepart( yyyy, hdt_laudo) between " + xAnoInicial.ToString() + " and " + xAnoFinal.ToString() + " "
                //str.Append(" AND hDT_INICIO<'" + this.laudoTecnico.hDT_LAUDO.AddYears(1).ToString("yyyy-MM-dd") + "'"); 
                // ex. laudo de 2008,  pegar classif.funcionais com ( data final >= 2008 ou em branco ) e data inicial <= 2008
                //str.Append(" AND ( ( hDT_TERMINO IS NULL or hDT_TERMINO >='" + this.laudoTecnico.hDT_LAUDO.AddYears(1).ToString("yyyy-MM-dd") + "' ) AND hDT_INICIO <= '" + this.laudoTecnico.hDT_LAUDO.ToString("yyyy-MM-dd") + "' ) "); 

            str.Append(" AND ( ( ( hDT_INICIO <='" + xlaudoTecnico.hDT_LAUDO.ToString("yyyy-MM-dd") + "' ) AND ( hDT_TERMINO >= '" + xlaudoTecnico.hDT_LAUDO.ToString("yyyy-MM-dd") + "' ) ) ");

            if (xlaudoTecnicoPosterior.Id != 0)
            {
                str.Append("  OR   ( ( hDT_INICIO >='" + xlaudoTecnico.hDT_LAUDO.ToString("yyyy-MM-dd") + "' ) AND ( hDT_TERMINO <= '" + xlaudoTecnicoPosterior.hDT_LAUDO.ToString("yyyy-MM-dd") + "' ) ) ");
            }
            else
            {
                str.Append("  OR   ( ( hDT_INICIO >='" + xlaudoTecnico.hDT_LAUDO.ToString("yyyy-MM-dd") + "' ) AND ( hDT_TERMINO <= '" + xlaudoTecnico.hDT_LAUDO.AddYears(rAnos).ToString("yyyy-MM-dd") + "' ) ) ");
            }

            //se houver laudo com prazo menor que dois anos posterior, ajustar essa regra

            if (xlaudoTecnicoPosterior.Id != 0)
            {
                str.Append("  OR   ( ( hDT_INICIO >='" + xlaudoTecnico.hDT_LAUDO.ToString("yyyy-MM-dd") + "' and hDT_INICIO < '" + xlaudoTecnicoPosterior.hDT_LAUDO.ToString("yyyy-MM-dd") + "' ) AND ( hDT_TERMINO >= '" + xlaudoTecnicoPosterior.hDT_LAUDO.ToString("yyyy-MM-dd") + "' or hDT_TERMINO is null ) ) ");
            }
            else
            {
                str.Append("  OR   ( ( hDT_INICIO >='" + xlaudoTecnico.hDT_LAUDO.ToString("yyyy-MM-dd") + "' and hDT_INICIO <='" + xlaudoTecnico.hDT_LAUDO.AddYears(rAnos).ToString("yyyy-MM-dd") + "' ) AND ( hDT_TERMINO >= '" + xlaudoTecnico.hDT_LAUDO.AddYears(rAnos).ToString("yyyy-MM-dd") + "' or hDT_TERMINO is null ) ) ");
            }

            if (xlaudoTecnicoPosterior.Id != 0)
            {
                str.Append("  OR   ( ( hDT_INICIO <='" + xlaudoTecnico.hDT_LAUDO.ToString("yyyy-MM-dd") + "' ) AND ( hDT_TERMINO >= '" + xlaudoTecnicoPosterior.hDT_LAUDO.ToString("yyyy-MM-dd") + "' or hDT_TERMINO IS NULL ) ) ) ");
            }
            else
            {
                str.Append("  OR   ( ( hDT_INICIO <='" + xlaudoTecnico.hDT_LAUDO.ToString("yyyy-MM-dd") + "' ) AND ( hDT_TERMINO >= '" + xlaudoTecnico.hDT_LAUDO.AddYears(rAnos).ToString("yyyy-MM-dd") + "' or hDT_TERMINO IS NULL ) ) ) ");
            }

            if (chk_Demitidos.Checked == false)
            {
                str.Append(" AND (hDT_DEM IS NULL) "); 
            }

            //if (!ultrChckEdtrTodos.Checked)
            //{
            //    str.Append(" AND (hDT_DEM IS NULL) "); // AND (hDT_TERMINO IS NULL)");
            //    //str.Append(" AND hDT_INICIO<'" + this.laudoTecnico.hDT_LAUDO.AddYears(1).ToString("yyyy-MM-dd") + "'");

            //    if (!ultrChckEdtrTerceiros.Checked)
            //        str.Append(" AND gTERCEIRO = 0");
            //    else
            //        str.Append(" AND gTERCEIRO = 1");

            //    str.Append(" AND nID_EMPREGADO_FUNCAO NOT IN (SELECT nID_EMPREGADO_FUNCAO FROM " + Mestra.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblFUNC_EMPREGADO WHERE nID_FUNC IS NOT NULL AND nID_LAUD_TEC=" + laudoTecnico.Id + ")");

            //    if (ultrChckEdtrAfastados.Checked)
            //        str.Append(" AND nID_EMPREGADO IN (SELECT IdEmpregado FROM " + Mestra.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.Afastamento WHERE DataVolta IS NULL)");
            //    else
            //        str.Append(" AND nID_EMPREGADO NOT IN (SELECT IdEmpregado FROM " + Mestra.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.Afastamento WHERE DataVolta IS NULL)");
            //}
            //else
                str.Append(" AND nID_EMPREGADO_FUNCAO NOT IN (SELECT nID_EMPREGADO_FUNCAO FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblFUNC_EMPREGADO WHERE nID_FUNC IS NOT NULL AND nID_LAUD_TEC=" + xlaudoTecnico.Id + ") order by tNo_EMPG");


            gridEmpregadossemGHE.DataSource = null;
            gridEmpregadossemGHE.DataBind();

            gridEmpregadossemGHE.DataSource = new Cliente().ExecuteDataset(str.ToString());

            gridEmpregadossemGHE.DataBind();


            if (Request["Colaborador"].ToString().Trim() != "0")
            {

                for ( int zCont=0; zCont<gridEmpregadossemGHE.Items.Count; zCont++)
                {
                    if (gridEmpregadossemGHE.Items[zCont].Cells[1].Value.ToString().Trim() == Request["Colaborador"].ToString().Trim())
                    {
                        gridEmpregadossemGHE.SelectedItemIndex = zCont;
                    }
                    
                }

            }




        }


        private void PopularGridEmpregadosExpostos(string xnId_Laudo_Tec, string xId_GHE)
        {
            StringBuilder str = new StringBuilder();

            str.Append("SELECT nID_EMPREGADO, nIND_GFIP, tNO_EMPG, tNO_FUNC, tNO_FUNC_EMPR, tNO_STR_EMPR, CONVERT(char(10), hDT_INICIO, 103 ) as hDT_INICIO, convert( char(10),hDT_TERMINO, 103 ) as hDT_TERMINO, nID_EMPR, nID_FUNCAO, nID_SETOR, nID_FUNC, nID_LAUD_TEC, nID_FUNC_EMPREGADO, nID_EMPREGADO_FUNCAO, hDT_DEM");
            str.Append(" FROM  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.qryEmpregadoFuncaoGhe");
            str.Append(" WHERE " + GetCriteriaEmpregadoGhe(xnId_Laudo_Tec, xId_GHE) + " order by tNo_EMPG ");
            
            DataSet ds = new Cliente().ExecuteDataset(str.ToString());

            gridEmpregados.DataSource = null;
            gridEmpregados.DataBind();

            gridEmpregados.DataSource = ds;
            gridEmpregados.DataBind();
                        
        }



        private string GetCriteriaEmpregadoGhe(string xnId_Laudo_Tec, string xId_GHE)
        {
            StringBuilder str = new StringBuilder();

            //if (ultrChckEdtrTodosGhe.Checked)
            //str.Append("nID_LAUD_TEC =" + xnId_Laudo_Tec);
            //else
                str.Append("nID_LAUD_TEC =" + xnId_Laudo_Tec + " AND nID_FUNC =" + xId_GHE);

            //if (ultrChckEdtrVisualizarAfastados.Checked)
            //    str.Append(" AND nID_EMPREGADO IN (SELECT IdEmpregado FROM " + Mestra.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.Afastamento WHERE DataVolta IS NULL)");

            //if (ultrChckEdtrDemitidos.Checked)
            //    str.Append(" AND hDT_DEM IS NOT NULL");

            //if (ultrChckEdtrTerceiroAlocado.Checked)
            //    str.Append(" AND gTERCEIRO = 1");

            return str.ToString();
        }


        

                       
     

                


        protected void cmd_Add_1_Click(object sender, EventArgs e)
        {

            try
            {
                
               if ( gridEmpregadossemGHE.SelectedItemIndex >= 0 )
                {
                    AddSelecionados(gridEmpregadossemGHE.Items[gridEmpregadossemGHE.SelectedItemIndex].Key.ToString());
                }




                if (lst_Id_PPRA.Items[lst_PPRA.SelectedIndex].ToString().Trim() != "")
                {
                    PopularGridEmpregados(lst_Id_PPRA.Items[lst_PPRA.SelectedIndex].ToString().Trim());
                    if (cmb_GHE.SelectedIndex >= 0)
                        PopularGridEmpregadosExpostos(lst_Id_PPRA.Items[lst_PPRA.SelectedIndex].ToString().Trim(), lst_Id_GHE.Items[cmb_GHE.SelectedIndex].ToString().Trim());
                    else
                    {
                        gridEmpregados.DataSource = null;
                        gridEmpregados.DataBind();
                    }
                }
                else
                {
                    gridEmpregados.DataSource = null;
                    gridEmpregados.DataBind();
                    gridEmpregadossemGHE.DataSource = null;
                    gridEmpregadossemGHE.DataBind();
                }


            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            finally
            {
                
            }
        }



        private void AddSelecionados(string xId)
        {
            int colId = System.Convert.ToInt32(xId); 

            Int32 zLaudo = System.Convert.ToInt32( lst_Id_PPRA.Items[lst_PPRA.SelectedIndex].ToString() );
            Int32 zGHE = System.Convert.ToInt32(lst_Id_GHE.Items[cmb_GHE.SelectedIndex].ToString());

            if (colId != 0  && zLaudo != 0  && zGHE != 0 )
            {
                GheEmpregado gheEmpregado = new GheEmpregado();
                gheEmpregado.Find("nID_FUNC=" + zGHE + " AND nID_EMPREGADO_FUNCAO=" + colId);

                Cliente cliente2;
                cliente2 = new Cliente(System.Convert.ToInt32(Session["Empresa"].ToString()));

                bool xRealizar_Checagem = true;

                Ilitera.Opsa.Data.EmpregadoFuncao xEmpregadoFuncao = new Ilitera.Opsa.Data.EmpregadoFuncao(colId);

                xEmpregadoFuncao.nID_EMPREGADO.Find();

                if (xEmpregadoFuncao.nID_EMPREGADO.hDT_DEM != new DateTime())
                {
                    if (cliente2.Envio_2240_Demitidos == true)
                    {
                        xRealizar_Checagem = false;
                    }
                }

                LaudoTecnico vLaudo = new LaudoTecnico(zLaudo);

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
                    if (xChecagem.Checar_Alocacao_eSocial_Anterior(colId, "2240", cliente2.ESocial_Ambiente, zLaudo) > 0)
                    {
                        //throw new Exception("Essa alocação do colaborador não pode ser realizada pois há evento posterior criado associado ao e-Social. Primeiro exclua os eventos posteriores já enviados.");
                        StringBuilder st = new StringBuilder();
                        st.Append("window.alert('Essa alocação do colaborador não pode ser realizada pois há evento posterior criado associado ao e-Social.Primeiro exclua os eventos posteriores já enviados.')");
                        this.ClientScript.RegisterStartupScript(this.GetType(), "Alocação", st.ToString(), true);

                        return;
                    }
                }


                if (gheEmpregado.Id == 0)
                {
                    gheEmpregado.Inicialize();
                    gheEmpregado.nID_LAUD_TEC.Id = zLaudo;
                    gheEmpregado.nID_EMPREGADO_FUNCAO.Id = colId;
                }
                gheEmpregado.nID_FUNC.Id = zGHE;
                gheEmpregado.Save();

                Log_Web("Adicionar Colaborador em GHE    Laudo:" + lst_PPRA.Items[ lst_PPRA.SelectedIndex ].ToString() + "   Empresa:" + lbl_Empresa.Text + "  GHE:"  + cmb_GHE.Items[cmb_GHE.SelectedIndex].ToString() + "  Colaborador:" + gheEmpregado.nID_EMPREGADO_FUNCAO.nID_EMPREGADO.GetNomeEmpregadoComRE()
                        , System.Convert.ToInt32(lbl_Id_Usuario.Text), "GHE x Colaborador - Web");


                //enviar email alerta - alocação colaborador
                if (cliente2.Alerta_web_Alocar_Colaborador !=null && cliente2.Alerta_web_Alocar_Colaborador.Trim() != "")
                {

                    System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");
                    Entities.Usuario usuario = (Entities.Usuario)Session["usuarioLogado"];

                    string xCorpo = "<body><p><font size='2' face='Tahoma'><H1 align='center'>Alerta de Alocação de Colaborador - Ili.Net</H1></font></p> <br></br>" +
                                    "<p><font size='3' face='Tahoma'>Nome: " + gheEmpregado.nID_EMPREGADO_FUNCAO.nID_EMPREGADO.GetNomeEmpregadoComRE() + "<br>" +
                                    "Empresa:  " + lbl_Empresa.Text + "<br>" +
                                    "Laudo: " + lst_PPRA.Items[lst_PPRA.SelectedIndex].ToString() + "<br>" +
                                    "GHE: " + cmb_GHE.Items[cmb_GHE.SelectedIndex].ToString() + "<br><br>" +
                                    "Usuário responsável: " + usuario.NomeUsuario.ToUpper() + " em " + System.DateTime.Now.ToString("dd/MM/yyyy hh:mm", ptBr) + "<br></font></p></body>";


                    if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().IndexOf("Prajna") > 0)
                    {
                        Envio_Email_Prajna(cliente2.Alerta_web_Inserir_Colaborador.Trim(), "wagner@ilitera.com.br", "Alerta - Alocação de Colaborador em GHE", xCorpo, "", "Alocação de colaborador", gheEmpregado.nID_EMPREGADO_FUNCAO.nID_EMPREGADO.Id, System.Convert.ToInt32(lbl_Id_Empresa.Text));
                    }
                    else
                    {
                        if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("GLOBAL") > 0)
                            Envio_Email_Global(cliente2.Alerta_web_Inserir_Colaborador.Trim(), "wagner@ilitera.com.br", "Alerta - Alocação de Colaborador em GHE", xCorpo, "", "Alocação de colaborador", gheEmpregado.nID_EMPREGADO_FUNCAO.nID_EMPREGADO.Id, System.Convert.ToInt32(lbl_Id_Empresa.Text));
                        else
                            Envio_Email_Ilitera(cliente2.Alerta_web_Inserir_Colaborador.Trim(), "wagner@ilitera.com.br", "Alerta - Alocação de Colaborador em GHE", xCorpo, "", "Alocação de colaborador", gheEmpregado.nID_EMPREGADO_FUNCAO.nID_EMPREGADO.Id, System.Convert.ToInt32(lbl_Id_Empresa.Text));

                    }
                }




            }
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
            //checar se não é e-mail duplicado

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
            //checar se não é e-mail duplicado

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



        protected void cmd_Remove_1_Click(object sender, EventArgs e)
        {
            try
            {
                if (gridEmpregados.SelectedItemIndex >= 0)
                {
                    RemoveSelecionados(gridEmpregados.Items[gridEmpregados.SelectedItemIndex].Key.ToString());
                }

                //if (gridEmpregados.Behaviors.Selection.SelectedRows.Count > 0)
                //{
                //
                //    RemoveSelecionados(gridEmpregados.Behaviors.Selection.SelectedRows[0].Items[0].Value.ToString());
                //
                //}
               


                if (lst_Id_PPRA.Items[lst_PPRA.SelectedIndex].ToString().Trim() != "")
                {
                    PopularGridEmpregados(lst_Id_PPRA.Items[lst_PPRA.SelectedIndex].ToString().Trim());
                    if (cmb_GHE.SelectedIndex >= 0)
                        PopularGridEmpregadosExpostos(lst_Id_PPRA.Items[lst_PPRA.SelectedIndex].ToString().Trim(), lst_Id_GHE.Items[cmb_GHE.SelectedIndex].ToString().Trim());
                    else
                    {
                        gridEmpregados.DataSource = null;
                        gridEmpregados.DataBind();
                    }
                }
                else
                {
                    gridEmpregados.DataSource = null;
                    gridEmpregados.DataBind();
                                        
                    gridEmpregadossemGHE.DataSource = null;                    
                    gridEmpregadossemGHE.DataBind();
                    
                }

                Session["Id"]="";
                Session["Id2"]="";
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            finally
            {

            }

        }



        private void RemoveSelecionados(string zId)
        {
            int colId = Convert.ToInt32(zId);
            Int32 zLaudo = System.Convert.ToInt32(lst_Id_PPRA.Items[lst_PPRA.SelectedIndex].ToString());

            if (colId == 0 || zLaudo == 0 )
                return;

            GheEmpregado gheEmpregado = new GheEmpregado();
            gheEmpregado.Find( " nID_EMPREGADO_FUNCAO = " + colId + " and nId_Laud_Tec = " + zLaudo);

            EmpregadoFuncao xEF = new EmpregadoFuncao();
            xEF.Find(gheEmpregado.nID_EMPREGADO_FUNCAO.Id);


            Cliente cliente2;
            cliente2 = new Cliente(System.Convert.ToInt32(Session["Empresa"].ToString()));

            Ilitera.Data.eSocial xChecagem = new Ilitera.Data.eSocial();
            if (xChecagem.Checar_Alocacao_eSocial(gheEmpregado.Id, "2240", cliente2.ESocial_Ambiente) > 0)
            {                
                StringBuilder st = new StringBuilder();
                st.Append("window.alert('Essa alocação do colaborador tem evento criado associado ao e-Social, não é possível retirar a alocação.')");
                this.ClientScript.RegisterStartupScript(this.GetType(), "Alocação", st.ToString(), true);

                return;

            }


            //gheEmpregado.nID_EMPREGADO_FUNCAO.Id = colId;            
            gheEmpregado.Delete();

            Log_Web("Remover Colaborador em GHE    Laudo:" + lst_PPRA.Items[lst_PPRA.SelectedIndex].ToString() + "   Empresa:" + lbl_Empresa.Text + "  GHE:" + cmb_GHE.Items[cmb_GHE.SelectedIndex].ToString() + "  Colaborador:" + xEF.nID_EMPREGADO.GetNomeEmpregadoComRE()
                    ,  System.Convert.ToInt32( lbl_Id_Usuario.Text ), "GHE x Colaborador - Web");


        }




        protected void cmd_Atualizar_Click(object sender, EventArgs e)
        {

        //    int zId;
        //    int zCont = 0;
        //    int zAux = 0;
        //    bool zDif = false;
        //    bool zLoc = false;

        //    string xCargo;
        //    string xSetor;
        //    string xFuncao;


        //    zId = System.Convert.ToInt32(Session["Empregado"].ToString());

        //    if ( gridEmpregados.Rows.Count==0 )
        //    //if ((txtInicioFuncao.Text == "") || (txtFuncao.Text == "" && cmb_Funcao1.SelectedIndex < 0) || (txtSetor.Text == "" && cmb_Setor1.SelectedIndex < 0))
        //    {
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "alerta", String.Format("alert('{0}')", "Funcionário deve ter pelo menos uma classificação funcional preenchida de forma completa."), true);
        //    }
        //    else
        //    {

        //        //se data de demissão preenchida,  checar se ela bate com alguma data final de classif.funcional, senão, não há lógica
                


        //        //abrir tela para inserir novo empregado

        //        Mestra.Data.Empregado_Cadastral xEmpregado;

        //        xEmpregado = new Mestra.Data.Empregado_Cadastral();

        //        txtPISPASEP.Text = GetNumberFromStrFaster(txtPISPASEP.Text);
        //        txtCPF.Text = GetNumberFromStrFaster(txtCPF.Text);
                

        //        xEmpregado.Atualizar_Dados_Empregado(zId, txtCTPS_Num.Text, txtCTPS_Serie.Text, txtCTPS_UF.Text, txtMatricula.Text, txtRG.Text, txtDataAdmissao.Text, txtDataDemissao.Text, txtDataNascimento.Text, txtPISPASEP.Text, txtCPF.Text, txtApelidoEmpregado.Text, txtFoto.Text, cbSexo.SelectedValue.ToString(), System.Convert.ToInt32( lbl_Id_Usuario.Text ) );

                
        //        //salvar dados de uniformes
        //        Mestra.Data.PPRA_EPI xUnif = new Mestra.Data.PPRA_EPI();
        //        xUnif.Excluir_Dados_Empregado_Uniforme(zId);

        //        for (zCont = 0; zCont <= lst_Id_Uniforme_Medida_Sel.Items.Count - 1; zCont++)
        //        {

        //            xUnif.Salvar_Dados_Empregado_Uniforme(zId, System.Convert.ToInt32(lst_Id_Uniforme_Medida_Sel.Items[zCont].ToString()), System.Convert.ToInt32(lbl_Id_Usuario.Text));      

        //        }
                                                
        //        Response.Redirect("~/ListaEmpregados.aspx");
        //    }
            
        }

        ////}



        //protected void cmd_Refresh_Click(object sender, EventArgs e)
        //{

        //    Mestra.Opsa.Data.Empregado xempregado = new Mestra.Opsa.Data.Empregado();

        //    xempregado.Find(" tblEMPREGADO.nID_EMPREGADO = " + Session["Empregado"].ToString());

        //    string xArquivo;
        //    string xAux;
        //    ImgFunc.ImageUrl = "";



        //    if (txtFoto.Text.Trim() == "0")
        //    {
        //        xArquivo = "";
        //    }
        //    else
        //    {
        //        xAux = txtFoto.Text.Trim();

        //        for (int xCont = xAux.Length; xCont < xempregado.FotoTamanho; xCont++)
        //        {
        //            xAux = "0" + xAux;
        //        }

        //        xArquivo = xempregado.FotoDiretorio + "\\" + xempregado.FotoInicio + xAux + xempregado.FotoExtensao;
        //        if (xArquivo.ToUpper().IndexOf("FOTOSDOCSDIGITAIS") < 1)
        //        {
        //            xArquivo = xArquivo.Substring(0, xArquivo.IndexOf("\\") + 1) + "FOTOSDOCSDIGITAIS\\" + xArquivo.Substring(xArquivo.IndexOf("\\") + 1);
        //        }

        //        ImgFunc.ImageUrl = Mestra.Common.Fotos.PathFoto_Uri(xArquivo);

        //    }

            
        //}


        //protected void cmd_Absentismo_Click(object sender, EventArgs e)
        //{

        //    Response.Redirect("~/PCMSO/ListaAcidentes2.aspx?Tipo=2");

        //}

        //protected void btnAdd_Click(object sender, EventArgs e)
        //{

        //    if ( lst_Uniformes.SelectedIndex < 0)
        //    {
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "alerta", String.Format("alert('{0}')", "Selecione Uniforme/Tamanho a ser adicionado."), true);
        //        return;
        //    }

        //    Boolean zLoc = false;

        //    for (int zCont = 0; zCont < lst_Sel_Uni.Items.Count; zCont++)
        //    {
        //        if (lst_Id_Uniforme_Sel.Items[zCont].ToString().Trim() == lst_Id_Uniforme.Items[lst_Uniformes.SelectedIndex].ToString().Trim())
        //        {
        //            zLoc = true;
        //        }
        //    }

        //    for (int zCont = 0; zCont < lst_Sel_Uni.Items.Count; zCont++)
        //    {
        //        if (lst_Id_Uniforme_Medida_Sel.Items[zCont].ToString().Trim() == lst_Id_Uniforme_Medida.Items[lst_Uniformes.SelectedIndex].ToString().Trim())
        //        {
        //            zLoc = true;
        //        }
        //    }



        //    if (zLoc == false)
        //    {
        //        lst_Sel_Uni.Items.Add( lst_Uniformes.Items[lst_Uniformes.SelectedIndex].ToString().Trim());
        //        lst_Id_Uniforme_Sel.Items.Add(lst_Id_Uniforme.Items[lst_Uniformes.SelectedIndex].ToString().Trim());
        //        lst_Id_Uniforme_Medida_Sel.Items.Add(lst_Id_Uniforme_Medida.Items[lst_Uniformes.SelectedIndex].ToString().Trim());
        //    }            

        //}

        //protected void btnRemove_Click(object sender, EventArgs e)
        //{

        //    if (lst_Sel_Uni.SelectedIndex < 0)
        //    {
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "alerta", String.Format("alert('{0}')", "Selecione Medida a ser retirada."), true);
        //        return;
        //    }

        //    lst_Id_Uniforme_Sel.Items.RemoveAt(lst_Sel_Uni.SelectedIndex);
        //    lst_Id_Uniforme_Medida_Sel.Items.RemoveAt(lst_Sel_Uni.SelectedIndex);
        //    lst_Sel_Uni.Items.RemoveAt(lst_Sel_Uni.SelectedIndex);
            

        //}


        protected void cmb_GHE_SelectedIndexChanged(object sender, EventArgs e)
        {
            string xLinha = "";

            if (cmb_GHE.SelectedIndex < 0)
            {
                lst_Riscos.Items.Clear();
                return;
            }

            //Ilitera.Opsa.Data.PPRA PPRA;

            
            List<PPRA> listPPRA;


            listPPRA = new PPRA().Find<PPRA>("nID_FUNC=" + lst_Id_GHE.Items[cmb_GHE.SelectedIndex].ToString() + " ORDER BY nID_RSC");
            lst_Riscos.Items.Clear();

            foreach (PPRA ppra in listPPRA)
            {
                

                TrajetoriaRiscos xTraj = new TrajetoriaRiscos();
                xTraj.Find(ppra.nID_TRJ.Id);

                MeioPropagacao xMeio = new MeioPropagacao();
                xMeio.Find(ppra.nID_MEIO_PRPG.Id);

                xLinha = "";

                //newRow["nID_PPRA"] = ppra.Id;
                xLinha = ppra.GetNomeAgente();
                xLinha = xLinha + "  " + ppra.GetAvaliacaoQuantitativa();
                xLinha = xLinha + "  " + ppra.tVL_LIM_TOL;
                xLinha = xLinha + "  " + ppra.nID_AG_NCV.ToString();
                //xLinha = xLinha + "  " + ppra.GetModoExposicao();

                //newRow["TRAJETORIA"] = ppra.nID_TRJ.tDS_TRAJ_RSC.ToString();
                //newRow["MEIO_PROPAGACAO"] = ppra.nID_MEIO_PRPG.tDS_MEIO_PROP.ToString();

                //xLinha = xLinha + "  " + xTraj.ToString();
                //xLinha = xLinha + "  " + xMeio.ToString();


                //newRow["FONTE_GERADORA"] = ppra.tDS_FTE_GER;
                //newRow["MATERIA_PRIMA"] = ppra.tDS_MAT_PRM;
                //newRow["EPI"] = ppra.GetEpi();
                //newRow["EPC"] = ppra.mDS_EPC_EXTE;
                //newRow["EQUIP_MED"] = ppra.nID_EQP_MED.ToString();

                lst_Riscos.Items.Add(xLinha);
            }


            if (cmb_GHE.SelectedIndex >= 0)
                PopularGridEmpregadosExpostos(lst_Id_PPRA.Items[lst_PPRA.SelectedIndex].ToString().Trim(), lst_Id_GHE.Items[cmb_GHE.SelectedIndex].ToString().Trim());
            else
                gridEmpregados.DataSource = null;

        }


        protected void ListBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void lst_1_SelectedIndexChanged(object sender, EventArgs e)
        {

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


        protected void gridEmpregadossemGHE_ItemCommand(object sender, EO.Web.GridCommandEventArgs e)
        {
            //Check whether it is from our client side
            //JavaScript call


            //Session["Id"] = e.Item.Key.ToString();
            //Session["NomeEmpregado"] = e.Item.Cells[0].Value.ToString();



        }


        protected void gridEmpregados_ItemCommand(object sender, EO.Web.GridCommandEventArgs e)
        {
            //Check whether it is from our client side
            //JavaScript call


            //Session["Id2"] = e.Item.Key.ToString();
            //Session["NomeEmpregado"] = e.Item.Cells[0].Value.ToString();



        }

        protected void chk_Demitidos_CheckedChanged(object sender, EventArgs e)
        {
            if (lst_PPRA.SelectedIndex >= 0)
                PopularGridEmpregados(lst_Id_PPRA.Items[lst_PPRA.SelectedIndex].ToString().Trim());

        }

        protected void chk_Finalizado_CheckedChanged(object sender, EventArgs e)
        {
            Carregar_Laudos();
        }
    }
}
