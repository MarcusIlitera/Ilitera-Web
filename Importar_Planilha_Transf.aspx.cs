using System;
using System.Collections;
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
using Facade;
using Ilitera.Opsa.Data;
using System.Collections.Generic;
using Ilitera.Data;
using System.IO;
using System.Data.OleDb;
using System.Data.SqlClient;

using System.Text;
using Ilitera.Common;


namespace Ilitera.Net
{
    public partial class Importar_Planilha_Transf : System.Web.UI.Page
    {


        protected void Page_Load(object sender, EventArgs e)
        {

            //try
            //{
            //    string FormKey = this.Page.ToString().Substring(4);

            //    Ilitera.Common.Funcionalidade funcionalidade = new Ilitera.Common.Funcionalidade();
            //    funcionalidade.Find("ClassName='" + FormKey + "'");

            //    if (funcionalidade.Id == 0)
            //        throw new Exception("Formulário não cadastrado - " + FormKey);

            //    Entities.Usuario xUser = (Entities.Usuario)Session["usuarioLogado"];
            //    Ilitera.Common.Usuario.Permissao_Web(xUser.IdUsuario, funcionalidade.Id);
            //}

            //catch (Exception ex)
            //{
            //    Session["Message"] = ex.Message;
            //    Server.Transfer("~/Tratar_Excecao.aspx");
            //    return;
            //}


            //try
            //{




            //    if (!IsPostBack)
            //    {




            //        if (Session["Empresa"] != null && Session["Empresa"].ToString().Trim() != String.Empty)
            //        {
            //            int idJuridica = Convert.ToInt32(Session["Empresa"]);
            //            int idJuridicaPai = 0;

            //            if (Session["JuridicaPai"] != null && Session["JuridicaPai"].ToString().Trim() != String.Empty)
            //            {
            //                idJuridicaPai = Convert.ToInt32(Session["JuridicaPai"]);
            //            }

            //            //PopularGrid();

            //            if (Request.QueryString["liberar"] != null && Request.QueryString["liberar"].ToString() != String.Empty)
            //            {
            //                Session["Empregado"] = String.Empty;
            //                Session["NomeEmpregado"] = String.Empty;

            //                Response.Redirect("~/ListaEmpregados.aspx");
            //            }
            //        }
            //        else
            //        {
            //            ScriptManager.RegisterStartupScript(this, this.GetType(), "alerta", "alert('Para selecionar um empregado é necessário selecionar uma Empresa antes, você será redirecionado para a página de seleção de empresas.');", true);
            //            Response.Redirect("~/ListaEmpresas2.aspx");
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "alerta", String.Format("alert('{0}');", ex.Message), true);
            //}
        }






        protected void txt_Nome_TextChanged(object sender, EventArgs e)
        {

        }

        protected void cmd_Busca_Click(object sender, EventArgs e)
        {





            //procurar colunas
            int C_CPF = 0;
            int C_FUNCAO = 0;
            int C_SETOR = 0;
            int C_FILIAL_ORIGEM = 0;
            int C_CNPJ_ORIGEM = 0;
            int C_FILIAL_DESTINO = 0;
            int C_CNPJ_DESTINO = 0;
            int C_DATA_INICIAL = 0;
            int C_DIA_DATA_INICIAL = 0;
            int C_INATIVAR_ORIGEM = 0;
            int C_DATA_DEMISSAO = 0;
            int C_DIA_DATA_DEMISSAO = 0;
            int C_GHE = 0;

            int C_DATA_PLANILHA = 0;
            int C_LOGIN_RESPONSAVEL = 0;

            string C_CNPJ = "";



            lbl_Cabecalho.Visible = false;
            lbl_Dados.Visible = false;
            lbl_Incons.Visible = false;
            btn_Cabecalho.Visible = false;
            btn_Dados.Visible = false;
            btn_Inconsistencias.Visible = false;

            lbl_Inconsistencias.Visible = false;
            lst_Inconsistencias.Visible = false;

            cmd_Importar.Visible = false;


            if (File1.HasFile)
            {
                string FileName = Path.GetFileName(File1.PostedFile.FileName);
                string Extension = Path.GetExtension(File1.PostedFile.FileName);
                //string FolderPath = ConfigurationManager.AppSettings["FolderPath"];

                if (Extension.ToUpper() == ".XLS" || Extension.ToUpper() == ".XLSX")
                {

                    lbl_Arq.Text = "Arquivo Selecionado: " + FileName;
                    lbl_Arq.Visible = true;

                    string FilePath = "I:\\temp\\" + FileName;
                    File1.SaveAs(FilePath);

                    OleDbConnection oledbConn;

                    oledbConn = new OleDbConnection();

                    try
                    {
                        // 
                        string path = FilePath;


                        if (Path.GetExtension(path) == ".xls")
                        {
                            oledbConn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source = " + path + ";Extended Properties =\"Excel 8.0;HDR=Yes;IMEX=2\"");
                        }
                        else if (Path.GetExtension(path) == ".xlsx")
                        {
                            oledbConn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties = 'Excel 12.0;HDR=YES;IMEX=1;'; ");
                        }
                        oledbConn.Open();
                        OleDbCommand cmd = new OleDbCommand(); ;
                        OleDbDataAdapter oleda = new OleDbDataAdapter();
                        DataSet ds = new DataSet();

                        //pegar nome da planilha
                        DataTable dtExcelSchema;
                        dtExcelSchema = oledbConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                        string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();

                        // passing list to drop-down list
                        cmb_Planilha.Items.Clear();

                        for (int rCont = 0; rCont < dtExcelSchema.Rows.Count; rCont++)
                        {
                            cmb_Planilha.Items.Add(dtExcelSchema.Rows[rCont]["TABLE_NAME"].ToString());
                        }

                        if (cmb_Planilha.Items.Count > 0)
                        {
                            cmb_Planilha.SelectedIndex = 0;
                            // cmb_Planilha.Visible = true;
                            //  lbl_Planilhas.Visible = true;
                        }
                        else
                            return;


                        // selecting distinct list of Slno 
                        cmd.Connection = oledbConn;
                        cmd.CommandType = CommandType.Text;


                        //cmd.CommandText = "SELECT * FROM[Plan1$]";
                        cmd.CommandText = "SELECT top 10000 * FROM[" + SheetName + "]";
                        oleda = new OleDbDataAdapter(cmd);
                        oleda.Fill(ds);

                        ds.Tables[0].Columns.Add("Linha", typeof(string));
                        ds.Tables[0].Columns["Linha"].SetOrdinal(0);

                        // binding form data with grid view
                        grvData.DataSource = ds.Tables[0].DefaultView;
                        grvData.DataBind();

                        lst_Inconsistencias.Items.Clear();

                        bool zImportacao_Impossibilitada = false;
                        int zLinha_Cab = 0;


                        string xCNPJ_Origem = "";
                        string xCNPJ_Destino = "";

                        for (int zCont = 0; zCont < grvData.Rows.Count; zCont++)
                        {
                            string zVal = "";

                            GridViewRow zRow;
                            zRow = grvData.Rows[zCont];

                            for (int zCel = 1; zCel < zRow.Cells.Count; zCel++)
                            {
                                zVal = HttpUtility.HtmlDecode(zRow.Cells[zCel].Text.ToString().Trim());
                                zRow.Cells[zCel].Text = zVal;

                                //indicar colunas
                                if (zVal.ToUpper() == "C_DATA_PLANILHA")
                                {
                                    //pegar coluna à direita e ver se é data válida
                                    string zData = HttpUtility.HtmlDecode(zRow.Cells[zCel + 1].Text.ToString().Trim());

                                    if (Validar_Data(zData) == false)
                                    {
                                        if (Validar_Data2(zData) == false)
                                        {
                                            lst_Inconsistencias.Items.Add("Data do campo C_DATA_PLANILHA inválida - Impossibilitada Importação");
                                            zImportacao_Impossibilitada = true;
                                        }
                                        else
                                        {
                                            C_DATA_PLANILHA = zCel;
                                            lblC_Data.Text = zData;
                                        }
                                    }
                                    else
                                    {
                                        C_DATA_PLANILHA = zCel;
                                        lblC_Data.Text = zData;
                                    }

                                }
                                else if (zVal.ToUpper() == "C_LOGIN_RESPONSAVEL")
                                {
                                    //pegar coluna à direita e ver se é data válida
                                    string zUser = HttpUtility.HtmlDecode(zRow.Cells[zCel + 1].Text.ToString().Trim());

                                    Usuario rUser = new Usuario();
                                    rUser.Find("NomeUsuario = '" + zUser + "' ");

                                    if (rUser.Id == 0)
                                    {
                                        lst_Inconsistencias.Items.Add("Usuário indicado no campo C_LOGIN_RESPONSAVEL inválido - Impossibilitada Importação");
                                        zImportacao_Impossibilitada = true;
                                    }
                                    else
                                    {
                                        C_LOGIN_RESPONSAVEL = zCel;
                                        lblC_Login.Text = zUser;
                                    }


                                }
                                else if (zVal.ToUpper() == "C_FILIAL_ORIGEM")
                                {
                                    C_FILIAL_ORIGEM = zCel;

                                    if (zLinha_Cab == 0) zLinha_Cab = zCont;

                                    if (zLinha_Cab != zCont)
                                    {
                                        lst_Inconsistencias.Items.Add("Linha do cabeçalho apresenta inconsistência C_FILIAL_ORIGEM - Impossibilitada Importação");
                                        zImportacao_Impossibilitada = true;
                                    }
                                }
                                else if (zVal.ToUpper() == "C_CNPJ_ORIGEM")
                                {
                                    C_CNPJ_ORIGEM = zCel;

                                    if (zLinha_Cab == 0) zLinha_Cab = zCont;

                                    if (zLinha_Cab != zCont)
                                    {
                                        lst_Inconsistencias.Items.Add("Linha do cabeçalho apresenta inconsistência C_CNPJ_ORIGEM - Impossibilitada Importação");
                                        zImportacao_Impossibilitada = true;
                                    }
                                    else
                                    {
                                        xCNPJ_Origem = SomenteNumeros(HttpUtility.HtmlDecode(zRow.Cells[zCel + 1].Text.ToString().Trim()));
                                    }
                                }
                                else if (zVal.ToUpper() == "C_FILIAL_DESTINO")
                                {
                                    C_FILIAL_DESTINO = zCel;

                                    if (zLinha_Cab == 0) zLinha_Cab = zCont;

                                    if (zLinha_Cab != zCont)
                                    {
                                        lst_Inconsistencias.Items.Add("Linha do cabeçalho apresenta inconsistência C_FILIAL_DESTINO - Impossibilitada Importação");
                                        zImportacao_Impossibilitada = true;
                                    }
                                }
                                else if (zVal.ToUpper() == "C_CNPJ_DESTINO")
                                {
                                    C_CNPJ_DESTINO = zCel;

                                    if (zLinha_Cab == 0) zLinha_Cab = zCont;

                                    if (zLinha_Cab != zCont)
                                    {
                                        lst_Inconsistencias.Items.Add("Linha do cabeçalho apresenta inconsistência C_CNPJ_DESTINO - Impossibilitada Importação");
                                        zImportacao_Impossibilitada = true;
                                    }
                                    else
                                    {
                                        xCNPJ_Destino = SomenteNumeros(HttpUtility.HtmlDecode(zRow.Cells[zCel + 1].Text.ToString().Trim()));
                                    }
                                }
                                else if (zVal.ToUpper() == "C_DATA_INICIAL")
                                {
                                    C_DATA_INICIAL = zCel;

                                    if (zLinha_Cab == 0) zLinha_Cab = zCont;

                                    if (zLinha_Cab != zCont)
                                    {
                                        lst_Inconsistencias.Items.Add("Linha do cabeçalho apresenta inconsistência C_DATA_INICIAL - Impossibilitada Importação");
                                        zImportacao_Impossibilitada = true;
                                    }
                                }
                                else if (zVal.ToUpper() == "C_DIA_DATA_INICIAL")
                                {
                                    C_DIA_DATA_INICIAL = zCel;

                                    if (zLinha_Cab == 0) zLinha_Cab = zCont;

                                    if (zLinha_Cab != zCont)
                                    {
                                        lst_Inconsistencias.Items.Add("Linha do cabeçalho apresenta inconsistência C_DIA_DATA_INICIAL - Impossibilitada Importação");
                                        zImportacao_Impossibilitada = true;
                                    }
                                }
                                else if (zVal.ToUpper() == "C_DIA_DATA_DEMISSAO")
                                {
                                    C_DIA_DATA_DEMISSAO = zCel;

                                    if (zLinha_Cab == 0) zLinha_Cab = zCont;

                                    if (zLinha_Cab != zCont)
                                    {
                                        lst_Inconsistencias.Items.Add("Linha do cabeçalho apresenta inconsistência C_DIA_DATA_DEMISSAO - Impossibilitada Importação");
                                        zImportacao_Impossibilitada = true;
                                    }
                                }
                                else if (zVal.ToUpper() == "C_INATIVAR_ORIGEM")
                                {
                                    C_INATIVAR_ORIGEM = zCel;

                                    if (zLinha_Cab == 0) zLinha_Cab = zCont;

                                    if (zLinha_Cab != zCont)
                                    {
                                        lst_Inconsistencias.Items.Add("Linha do cabeçalho apresenta inconsistência C_INATIVAR_ORIGEM - Impossibilitada Importação");
                                        zImportacao_Impossibilitada = true;
                                    }
                                }
                                else if (zVal.ToUpper() == "C_DATA_DEMISSAO")
                                {
                                    C_DATA_DEMISSAO = zCel;

                                    if (zLinha_Cab == 0) zLinha_Cab = zCont;

                                    if (zLinha_Cab != zCont)
                                    {
                                        lst_Inconsistencias.Items.Add("Linha do cabeçalho apresenta inconsistência C_DATA_DEMISSAO - Impossibilitada Importação");
                                        zImportacao_Impossibilitada = true;
                                    }
                                }
                                else if (zVal.ToUpper() == "C_CPF")
                                {
                                    C_CPF = zCel;

                                    if (zLinha_Cab == 0) zLinha_Cab = zCont;

                                    if (zLinha_Cab != zCont)
                                    {
                                        lst_Inconsistencias.Items.Add("Linha do cabeçalho apresenta inconsistência C_CPF - Impossibilitada Importação");
                                        zImportacao_Impossibilitada = true;
                                    }
                                }
                                else if (zVal.ToUpper() == "C_FUNCAO")
                                {
                                    C_FUNCAO = zCel;

                                    if (zLinha_Cab == 0) zLinha_Cab = zCont;

                                    if (zLinha_Cab != zCont)
                                    {
                                        lst_Inconsistencias.Items.Add("Linha do cabeçalho apresenta inconsistência C_FUNCAO - Impossibilitada Importação");
                                        zImportacao_Impossibilitada = true;
                                    }
                                }
                                else if (zVal.ToUpper() == "C_SETOR")
                                {
                                    C_SETOR = zCel;

                                    if (zLinha_Cab == 0) zLinha_Cab = zCont;

                                    if (zLinha_Cab != zCont)
                                    {
                                        lst_Inconsistencias.Items.Add("Linha do cabeçalho apresenta inconsistência C_SETOR - Impossibilitada Importação");
                                        zImportacao_Impossibilitada = true;
                                    }
                                }
                                else if (zVal.ToUpper() == "C_GHE")
                                {
                                    C_GHE = zCel;

                                    if (zLinha_Cab == 0) zLinha_Cab = zCont;

                                    if (zLinha_Cab != zCont)
                                    {
                                        lst_Inconsistencias.Items.Add("Linha do cabeçalho apresenta inconsistência C_GHE - Impossibilitada Importação");
                                        zImportacao_Impossibilitada = true;
                                    }
                                }

                                //else if (zVal.ToUpper() == "C_CNPJ")
                                //{
                                //    //considerando CNPJ na coluna a frente
                                //    C_CNPJ = SomenteNumeros(HttpUtility.HtmlDecode(zRow.Cells[zCel + 1].Text.ToString().Trim()));

                                //}


                            }
                            zRow.Cells[0].Text = zCont.ToString();

                        }


                        grvData.SelectedIndex = 0;





                        //CHECAR INCONSISTENCIAS


                        ////checar se CNPJ bate com valor fornecido
                        //Cliente cliente;
                        //cliente = new Cliente(System.Convert.ToInt32(Session["Empresa"].ToString()));

                        //Ilitera.Common.Pessoa rEmpresa = new Ilitera.Common.Pessoa();

                        //string rSelect = " NomeAbreviado = '" + cliente.NomeAbreviado.Trim() + "' and dbo.udf_getnumeric( nomecodigo ) = '" + xCNPJ_Origem + "' and IdPessoa in ( select IdCliente from Cliente ) ";

                        ////pegar Id Empresa                    
                        //rEmpresa.Find(rSelect);

                        ////se não achar empresa,  emitir retorno avisando.  
                        //if (rEmpresa.Id == 0)
                        //{
                        //    lst_Inconsistencias.Items.Add("CNPJ da planilha não bate com dados cadastrais da Empresa selecionada - Impossibilita Importação");
                        //    zImportacao_Impossibilitada = true;
                        //}









                        if (C_FILIAL_ORIGEM == 0)
                        {
                            lst_Inconsistencias.Items.Add("C_FILIAL_ORIGEM não localizado - Impossibilita Importação");
                            zImportacao_Impossibilitada = true;
                        }
                        if (C_CNPJ_ORIGEM == 0)
                        {
                            lst_Inconsistencias.Items.Add("C_MATRICULA não localizado - Impossibilita Importação");
                            zImportacao_Impossibilitada = true;
                        }
                        if (C_FILIAL_DESTINO == 0)
                        {
                            lst_Inconsistencias.Items.Add("C_FILIAL_DESTINO não localizado - Impossibilita Importação");
                            zImportacao_Impossibilitada = true;
                        }
                        if (C_CNPJ_DESTINO == 0)
                        {
                            lst_Inconsistencias.Items.Add("C_CNPJ_DESTINO não localizado - Impossibilita Importação");
                            zImportacao_Impossibilitada = true;
                        }
                        if (C_DATA_INICIAL == 0)
                            lst_Inconsistencias.Items.Add("C_DATA_INICIAL não localizado  ");

                        if (C_DIA_DATA_INICIAL == 0)
                            lst_Inconsistencias.Items.Add("C_DIA_DATA_INICIAL não localizado  ");

                        if (C_CPF == 0)
                        {
                            lst_Inconsistencias.Items.Add("C_CPF não localizado - Impossibilitada Importação  ");
                            zImportacao_Impossibilitada = true;
                        }


                        if (C_INATIVAR_ORIGEM == 0)
                        {
                            lst_Inconsistencias.Items.Add("C_INATIVAR_ORIGEM não localizado - Impossibilitada Importação  ");
                            zImportacao_Impossibilitada = true;
                        }

                        if (C_FUNCAO == 0)
                        {
                            lst_Inconsistencias.Items.Add("C_FUNCAO não localizado - Impossibilitada Importação  ");
                            zImportacao_Impossibilitada = true;
                        }

                        if (C_SETOR == 0)
                        {
                            lst_Inconsistencias.Items.Add("C_SETOR não localizado - Impossibilitada Importação  ");
                            zImportacao_Impossibilitada = true;
                        }

                        if (C_DATA_DEMISSAO == 0)
                            lst_Inconsistencias.Items.Add("C_DATA_DEMISSAO não localizado  ");

                        if (C_DIA_DATA_DEMISSAO == 0)
                            lst_Inconsistencias.Items.Add("C_DIA_DATA_DEMISSAO não localizado  ");


                        if (C_GHE == 0)
                            lst_Inconsistencias.Items.Add("C_GHE não localizado  ");


                        if (lst_Inconsistencias.Items.Count > 0)
                        {
                            lst_Inconsistencias.Visible = true;
                            lbl_Inconsistencias.Visible = true;
                        }


                        if (zImportacao_Impossibilitada == true)
                        {
                            cmd_Importar.Enabled = false;
                            cmd_Importar.Visible = false;

                            cmd_Cancelar.Visible = false;
                            cmd_Cancelar.Enabled = false;
                        }
                        else
                        {


                            if (zLinha_Cab > 0)
                            {


                                //excluir linhas anteriores ao cabeçalho ?
                                for (int zCont = 0; zCont < zLinha_Cab; zCont++)
                                    ds.Tables[0].Rows[zCont].Delete();

                                ds.Tables[0].AcceptChanges();

                                grvData.DataSource = null;
                                grvData.DataBind();

                                grvData.DataSource = ds.Tables[0].DefaultView;
                                grvData.DataBind();




                                //grvData.Rows[zLinha_Cab].BackColor = System.Drawing.ColorTranslator.FromHtml("#99CCFF");   // System.Drawing.Color.SteelBlue;
                                //grvData.Rows[zLinha_Cab].ForeColor = System.Drawing.Color.White;                                

                                grvData.Rows[0].BackColor = System.Drawing.ColorTranslator.FromHtml("#99CCFF");   // System.Drawing.Color.SteelBlue;
                                grvData.Rows[0].ForeColor = System.Drawing.Color.White;


                                int f_DDMMYYYY = 0;
                                int f_MMDDYYYY = 0;




                                //descobrir formato data usado
                                for (int zCont = 1; zCont < grvData.Rows.Count; zCont++)
                                {
                                    string zVal = "";

                                    GridViewRow zRow;
                                    zRow = grvData.Rows[zCont];

                                    zVal = zRow.Cells[C_CPF].Text.ToString().Trim().Replace("&nbsp;", "");

                                    //checar se campo CPF preenchido para avaliar linha
                                    if (zVal != "")
                                    {


                                        for (int zCel = 1; zCel < zRow.Cells.Count; zCel++)
                                        {
                                            // zVal = zRow.Cells[C_NOME].Text.ToString().Trim();

                                            zVal = zRow.Cells[zCel].Text.ToString().Trim();

                                            if (zCel == C_DATA_INICIAL || zCel == C_DATA_DEMISSAO)
                                            {
                                                //checar se todas estão em D/M/YYYY ou se todas estão em M/D/YYYY
                                                if (Validar_Data2(zVal) == true)
                                                {
                                                    f_MMDDYYYY = f_MMDDYYYY + 1;
                                                }
                                                if (Validar_Data(zVal) == true)
                                                {
                                                    f_DDMMYYYY = f_DDMMYYYY + 1;
                                                }

                                            }
                                        }

                                    }
                                }





                                if (f_DDMMYYYY >= f_MMDDYYYY)
                                {
                                    f_MMDDYYYY = 0;
                                    lbl_Formato.Text = "dd/MM/yyyy";
                                }

                                if (f_MMDDYYYY > f_DDMMYYYY)
                                {
                                    f_DDMMYYYY = 0;
                                    lbl_Formato.Text = "MM/dd/yyyy";
                                }






                                //agora validar dados
                                //for (int zCont = zLinha_Cab+1; zCont < grvData.Rows.Count; zCont++)
                                for (int zCont = 1; zCont < grvData.Rows.Count; zCont++)
                                {
                                    string zVal = "";

                                    GridViewRow zRow;
                                    zRow = grvData.Rows[zCont];

                                    zVal = zRow.Cells[C_CPF].Text.ToString().Trim().Replace("&nbsp;", "");

                                    //checar se campo CPF preenchido para avaliar linha
                                    if (zVal != "")
                                    {
                                        bool Checar_Filial_Origem = false;
                                        bool Checar_Filial_Destino = false;
                                        bool Checar_CPF = false;

                                        string rCNPJ_Origem = "";
                                        string rCNPJ_Destino = "";
                                        string rNome_Origem = "";
                                        string rNome_Destino = "";
                                        string rCPF = "";
                                        int rCont_CNPJ_Origem = 0;
                                        int rCont_CNPJ_Destino = 0;
                                        int rCont_Nome_Origem = 0;
                                        int rCont_Nome_Destino = 0;
                                        Int32 rId_Origem = 0;
                                        string rGHE = "";
                                        Int32 rId_Cliente_GHE = 0;


                                        for (int zCel = 1; zCel < zRow.Cells.Count; zCel++)
                                        {
                                            // zVal = zRow.Cells[C_NOME].Text.ToString().Trim();

                                            //zVal = zRow.Cells[zCel].Text.ToString().Trim();
                                            zVal = HttpUtility.HtmlDecode(zRow.Cells[zCel].Text.ToString().Trim());


                                            if (zCel == C_DATA_INICIAL || zCel == C_DATA_DEMISSAO)
                                            {
                                                if (zVal != "-")
                                                {
                                                    //checar data        
                                                    if (f_DDMMYYYY > 0)
                                                    {
                                                        if (Validar_Data(zVal) == false)
                                                        {
                                                            zImportacao_Impossibilitada = true;
                                                            grvData.Rows[zCont].Cells[zCel].BackColor = System.Drawing.ColorTranslator.FromHtml("#FF9999");
                                                            lst_Inconsistencias.Items.Add("Valor Inválido - Linha " + zCont.ToString() + "   Coluna " + zCel.ToString());
                                                        }
                                                        else
                                                        {
                                                            grvData.Rows[zCont].Cells[zCel].BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFF99");
                                                        }
                                                    }
                                                    else if (f_MMDDYYYY > 0)
                                                    {
                                                        if (zVal != "-")
                                                        {
                                                            if (Validar_Data2(zVal) == false)
                                                            {
                                                                zImportacao_Impossibilitada = true;
                                                                grvData.Rows[zCont].Cells[zCel].BackColor = System.Drawing.ColorTranslator.FromHtml("#FF9999");
                                                                lst_Inconsistencias.Items.Add("Valor Inválido - Linha " + zCont.ToString() + "   Coluna " + zCel.ToString());
                                                            }
                                                            else
                                                            {
                                                                grvData.Rows[zCont].Cells[zCel].BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFF99");
                                                            }
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    grvData.Rows[zCont].Cells[zCel].BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFF99");
                                                }


                                            }
                                            else if (zCel == C_DIA_DATA_INICIAL || zCel == C_DIA_DATA_DEMISSAO)
                                            {
                                                grvData.Rows[zCont].Cells[zCel].BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFF99");
                                            }
                                            else if (zCel == C_CPF)
                                            {
                                                bool isNumerical = true;
                                                Int64 myInt;

                                                //checar numerico
                                                String Validar = zVal.Replace(".", "").Replace("-", "");
                                                isNumerical = Int64.TryParse(Validar, out myInt);
                                                if (isNumerical == false)
                                                {
                                                    zImportacao_Impossibilitada = true;
                                                    grvData.Rows[zCont].Cells[zCel].BackColor = System.Drawing.ColorTranslator.FromHtml("#FF9999");
                                                    lst_Inconsistencias.Items.Add("Valor Inválido - Linha " + zCont.ToString() + "   Coluna " + zCel.ToString());
                                                }
                                                else
                                                {
                                                    grvData.Rows[zCont].Cells[zCel].BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFF99");
                                                }

                                            }

                                            else if (zCel == C_GHE)
                                            {
                                                //checar string ( tamanho mínimo )
                                                if (zVal.Length < 1)
                                                {
                                                    //pode vir vazio
                                                    //zImportacao_Impossibilitada = true;
                                                    //grvData.Rows[zCont].Cells[zCel].BackColor = System.Drawing.ColorTranslator.FromHtml("#FF9999");
                                                    //lst_Inconsistencias.Items.Add("Valor Inválido - Linha " + zCont.ToString() + "   Coluna " + zCel.ToString());
                                                }
                                                else
                                                {
                                                    grvData.Rows[zCont].Cells[zCel].BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFF99");
                                                    rGHE = zVal.Trim();


                                                    if (rGHE != "" && rId_Cliente_GHE != 0)
                                                    {
                                                        //procurar por laudo técnico mais recente e ver se ghe existe nele
                                                        ArrayList alGHE = new Ghe().Find("  tno_Func = '" + rGHE + "' and nid_laud_tec in ( Select top 1 nid_Laud_Tec from tbllaudo_tec where nid_empr=" + rId_Cliente_GHE.ToString() + " order by hdt_laudo desc ) ");

                                                        if (alGHE.Count < 1)
                                                        {
                                                            zImportacao_Impossibilitada = true;
                                                            grvData.Rows[zCont].Cells[C_GHE].BackColor = System.Drawing.ColorTranslator.FromHtml("#FF9999");
                                                            lst_Inconsistencias.Items.Add("GHE não localizado no destino - Linha " + zCont.ToString() + "   Coluna " + zCel.ToString());
                                                        }
                                                    }

                                                }

                                            }



                                            else if (zCel == C_SETOR || zCel == C_FUNCAO || zCel == C_INATIVAR_ORIGEM || zCel == C_FILIAL_ORIGEM || zCel == C_FILIAL_DESTINO || zCel == C_CNPJ_ORIGEM || zCel == C_CNPJ_DESTINO)
                                            {
                                                //checar string ( tamanho mínimo )
                                                if (zVal.Length < 1)
                                                {
                                                    zImportacao_Impossibilitada = true;
                                                    grvData.Rows[zCont].Cells[zCel].BackColor = System.Drawing.ColorTranslator.FromHtml("#FF9999");
                                                    lst_Inconsistencias.Items.Add("Valor Inválido - Linha " + zCont.ToString() + "   Coluna " + zCel.ToString());
                                                }
                                                else
                                                {
                                                    grvData.Rows[zCont].Cells[zCel].BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFF99");
                                                }

                                            }


                                            //checagem valor CPF
                                            if (zCel == C_CPF)
                                            {

                                                string xCPF_Completo = "";
                                                string xCPF = zVal.Trim();

                                                if (xCPF.Length < 11)
                                                    xCPF_Completo = new string('0', 11 - xCPF.Length) + xCPF;
                                                else
                                                    xCPF_Completo = xCPF;


                                                StringBuilder cpf = new StringBuilder();
                                                cpf.Append(xCPF_Completo)
                                                    .Replace(".", string.Empty)
                                                    .Replace("-", string.Empty)
                                                    .Replace(" ", string.Empty);

                                                if (!Pessoa.VeriricaCPF(cpf.ToString()))
                                                {
                                                    zImportacao_Impossibilitada = true;
                                                    grvData.Rows[zCont].Cells[zCel].BackColor = System.Drawing.ColorTranslator.FromHtml("#FF9999");
                                                    lst_Inconsistencias.Items.Add("CPF Valor Inválido - Linha " + zCont.ToString() + "   Coluna " + zCel.ToString());
                                                }
                                                else
                                                {
                                                    grvData.Rows[zCont].Cells[zCel].BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFF99");
                                                    Checar_CPF = true;
                                                    rCPF = xCPF_Completo;
                                                }

                                            }


                                            if (zCel == C_FILIAL_ORIGEM)
                                            {
                                                rNome_Origem = zVal.Trim();
                                                rCont_Nome_Origem = zCont;
                                            }

                                            if (zCel == C_FILIAL_DESTINO)
                                            {
                                                rNome_Destino = zVal.Trim();
                                                rCont_Nome_Destino = zCont;
                                            }


                                            //checar CNPJ Origem
                                            if (zCel == C_CNPJ_ORIGEM)
                                            {
                                                string xCNPJ = SomenteNumeros(zVal);

                                                //procurar CNPJ na base de dados
                                                ArrayList alCliente = new Cliente().Find(" dbo.udf_getnumeric(NomeCodigo) like '%" + xCNPJ + "%' ");

                                                if (alCliente.Count == 0)
                                                {
                                                    zImportacao_Impossibilitada = true;
                                                    grvData.Rows[zCont].Cells[zCel].BackColor = System.Drawing.ColorTranslator.FromHtml("#FF9999");
                                                    lst_Inconsistencias.Items.Add("CNPJ Valor Inválido - Linha " + zCont.ToString() + "   Coluna " + zCel.ToString());
                                                }
                                                else if (alCliente.Count > 1)
                                                {
                                                    //procurar com descrição para ver se encontra registro único
                                                    Checar_Filial_Origem = true;
                                                    rCNPJ_Origem = xCNPJ;
                                                    rCont_CNPJ_Origem = zCont;
                                                }
                                                else
                                                {
                                                    grvData.Rows[zCont].Cells[zCel].BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFF99");

                                                    foreach (Cliente rCli in alCliente)
                                                    {
                                                        rId_Origem = rCli.Id;
                                                    }

                                                }

                                            }

                                            //checar CNPJ Origem
                                            if (zCel == C_CNPJ_DESTINO)
                                            {
                                                string xCNPJ = SomenteNumeros(zVal);

                                                //procurar CNPJ na base de dados                                               
                                                ArrayList alCliente = new Cliente().Find(" dbo.udf_getnumeric(NomeCodigo) like '%" + xCNPJ + "%' ");

                                                if (alCliente.Count == 0)
                                                {
                                                    zImportacao_Impossibilitada = true;
                                                    grvData.Rows[zCont].Cells[zCel].BackColor = System.Drawing.ColorTranslator.FromHtml("#FF9999");
                                                    lst_Inconsistencias.Items.Add("CNPJ Valor Inválido - Linha " + zCont.ToString() + "   Coluna " + zCel.ToString());
                                                }
                                                else if (alCliente.Count > 1)
                                                {
                                                    //procurar com descrição para ver se encontra registro único
                                                    Checar_Filial_Destino = true;
                                                    rCNPJ_Destino = xCNPJ;
                                                    rCont_CNPJ_Destino = zCont;
                                                }
                                                else
                                                {
                                                    grvData.Rows[zCont].Cells[zCel].BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFF99");


                                                    //checar GHE
                                                    Int32 zId = 0;

                                                    foreach (Cliente rCli in alCliente)
                                                    {
                                                        zId = rCli.Id;
                                                    }

                                                    rId_Cliente_GHE = zId;


                                                    if (rGHE != "")
                                                    {
                                                        //procurar por laudo técnico mais recente e ver se ghe existe nele
                                                        ArrayList alGHE = new Ghe().Find("  tno_Func = '" + rGHE + "' and nid_laud_tec in ( Select top 1 nid_Laud_Tec from tbllaudo_tec where nid_empr=" + rId_Cliente_GHE.ToString() + " order by hdt_laudo desc ) ");

                                                        if (alGHE.Count < 1)
                                                        {
                                                            zImportacao_Impossibilitada = true;
                                                            grvData.Rows[zCont].Cells[C_GHE].BackColor = System.Drawing.ColorTranslator.FromHtml("#FF9999");
                                                            lst_Inconsistencias.Items.Add("GHE não localizado no destino - Linha " + zCont.ToString() + "   Coluna " + zCel.ToString());
                                                        }
                                                    }
                                                }

                                            }


                                            //checar nome_origem e nome_destino quando flag indicar
                                            if (Checar_Filial_Origem == true)
                                            {
                                                if (rNome_Origem != "" && rCNPJ_Origem != "" && rCont_CNPJ_Origem == rCont_Nome_Origem)
                                                {
                                                    Cliente zCliente = new Cliente();

                                                    ArrayList alCliente = new Cliente().Find(" dbo.udf_getnumeric(NomeCodigo) like '%" + rCNPJ_Origem + "%' and NomeAbreviado = '" + rNome_Origem + "' ");

                                                    if (alCliente.Count != 1)
                                                    {
                                                        zImportacao_Impossibilitada = true;
                                                        grvData.Rows[zCont].Cells[C_CNPJ_ORIGEM].BackColor = System.Drawing.ColorTranslator.FromHtml("#FF9999");
                                                        lst_Inconsistencias.Items.Add("CNPJ e Nome não são registro único na base - Linha " + zCont.ToString() + "   Coluna " + zCel.ToString());
                                                    }
                                                    else
                                                    {
                                                        foreach (Cliente rCli in alCliente)
                                                        {
                                                            rId_Origem = rCli.Id;
                                                        }

                                                    }


                                                    //Checar_Filial_Origem = false;
                                                    rNome_Origem = "";
                                                    rCNPJ_Origem = "";
                                                }
                                            }


                                            if (Checar_Filial_Destino == true)
                                            {
                                                if (rNome_Destino != "" && rCNPJ_Destino != "" && rCont_CNPJ_Destino == rCont_Nome_Destino)
                                                {
                                                    Cliente zCliente = new Cliente();

                                                    ArrayList alCliente = new Cliente().Find(" dbo.udf_getnumeric(NomeCodigo) like '%" + rCNPJ_Destino + "%' and NomeAbreviado = '" + rNome_Destino + "' ");

                                                    if (alCliente.Count != 1)
                                                    {
                                                        zImportacao_Impossibilitada = true;
                                                        grvData.Rows[zCont].Cells[C_CNPJ_DESTINO].BackColor = System.Drawing.ColorTranslator.FromHtml("#FF9999");
                                                        lst_Inconsistencias.Items.Add("CNPJ e Nome não são registro único na base - Linha " + zCont.ToString() + "   Coluna " + zCel.ToString());
                                                    }

                                                    Checar_Filial_Destino = false;
                                                    rNome_Destino = "";
                                                    rCNPJ_Destino = "";
                                                }
                                            }


                                            if (Checar_CPF == true && rId_Origem != 0)   //verificar se colaborador existe na origem
                                            {
                                                //string xCPF_Completo = "";
                                                string xCPF = SomenteNumeros(rCPF);
                                                string xCPF_Completo = "";

                                                if (xCPF.Length < 11)
                                                    xCPF_Completo = new string('0', 11 - xCPF.Length) + xCPF;
                                                else
                                                    xCPF_Completo = xCPF;



                                                ArrayList list = new Empregado().Find(" tNo_CPF = '" + xCPF + "' and nId_Empr = " + rId_Origem.ToString() + " ");

                                                if (list.Count == 0)  // procurar por CPF Completo
                                                {
                                                    ArrayList list2 = new Empregado().Find(" tNo_CPF = '" + xCPF_Completo + "' and nId_Empr = " + rId_Origem.ToString() + " ");

                                                    if (list2.Count == 0)
                                                    {
                                                        zImportacao_Impossibilitada = true;
                                                        grvData.Rows[zCont].Cells[C_CPF].BackColor = System.Drawing.ColorTranslator.FromHtml("#FF9999");
                                                        lst_Inconsistencias.Items.Add("CPF na origem não encontrado - Linha " + zCont.ToString() + "   Coluna " + zCel.ToString());
                                                    }
                                                    else if (list2.Count > 1)
                                                    {
                                                        zImportacao_Impossibilitada = true;
                                                        grvData.Rows[zCont].Cells[C_CPF].BackColor = System.Drawing.ColorTranslator.FromHtml("#FF9999");
                                                        lst_Inconsistencias.Items.Add("CPF na origem não é registro único na base - Linha " + zCont.ToString() + "   Coluna " + zCel.ToString());
                                                    }

                                                }
                                                else if (list.Count > 1)
                                                {
                                                    zImportacao_Impossibilitada = true;
                                                    grvData.Rows[zCont].Cells[C_CPF].BackColor = System.Drawing.ColorTranslator.FromHtml("#FF9999");
                                                    lst_Inconsistencias.Items.Add("CPF na origem não é registro único na base - Linha " + zCont.ToString() + "   Coluna " + zCel.ToString());
                                                }


                                                Checar_CPF = false;
                                                Checar_Filial_Origem = false;
                                                rId_Origem = 0;
                                                rCPF = "";
                                            }


                                        }


                                    }
                                }


                                lbl_Cabecalho.Visible = true;
                                lbl_Dados.Visible = true;
                                lbl_Incons.Visible = true;
                                btn_Cabecalho.Visible = true;
                                btn_Dados.Visible = true;
                                btn_Inconsistencias.Visible = true;


                                if (lst_Inconsistencias.Items.Count > 0)
                                {
                                    lst_Inconsistencias.Visible = true;
                                    lbl_Inconsistencias.Visible = true;
                                }



                                if (zImportacao_Impossibilitada == false)
                                {
                                    lblC_Data1.Visible = true;
                                    lblC_Data.Visible = true;
                                    lblC_Login.Visible = true;
                                    lblC_Login1.Visible = true;

                                    cmd_Importar.Visible = true;
                                    cmd_Importar.Enabled = true;

                                    cmd_Cancelar.Visible = true;
                                    cmd_Cancelar.Enabled = true;

                                    lbl_Selecionar.Visible = false;
                                    File1.Visible = false;
                                    cmd_Analisar.Visible = false;

                                    lblC_Filial_Destino.Text = C_FILIAL_DESTINO.ToString().Trim();
                                    lblC_CNPJ_Destino.Text = C_CNPJ_DESTINO.ToString().Trim();
                                    lblC_Filial_Origem.Text = C_FILIAL_ORIGEM.ToString().Trim();
                                    lblC_CNPJ_Origem.Text = C_CNPJ_ORIGEM.ToString().Trim();
                                    lblC_CPF.Text = C_CPF.ToString().Trim();

                                    lblC_FUNCAO.Text = C_FUNCAO.ToString().Trim();
                                    lblC_SETOR.Text = C_SETOR.ToString().Trim();

                                    lblC_Data_Inicial.Text = C_DATA_INICIAL.ToString().Trim();
                                    lblC_Dia_Data_Inicial.Text = C_DIA_DATA_INICIAL.ToString().Trim();

                                    lblC_Inativar_Origem.Text = C_INATIVAR_ORIGEM.ToString().Trim();

                                    lblC_Data_Demissao.Text = C_DATA_DEMISSAO.ToString().Trim();
                                    lblC_Dia_Data_Demissao.Text = C_DIA_DATA_DEMISSAO.ToString().Trim();

                                    lblC_GHE.Text = C_GHE.ToString().Trim();

                                }
                                else
                                {
                                    cmd_Importar.Visible = false;
                                    cmd_Importar.Enabled = false;

                                    cmd_Cancelar.Visible = false;
                                    cmd_Cancelar.Enabled = false;
                                }






                            }

                        }

                    }

                    // need to catch possible exceptions
                    catch (Exception ex)
                    {
                        MsgBox1.Show("Ilitera.Net", ex.ToString(), null,
                        new EO.Web.MsgBoxButton("OK"));
                        return;

                    }
                    finally
                    {
                        oledbConn.Close();
                    }



                }
                else
                {
                    MsgBox1.Show("Ilitera.Net", "Somente arquivos XLS ou XLXS são válidos!", null,
                    new EO.Web.MsgBoxButton("OK"));

                    grvData.DataSource = null;
                    grvData.DataBind();
                    return;
                }


            }



        }



        private static string SomenteNumeros(string str)
        {

            string Ret = new String(str.Where(Char.IsDigit).ToArray());

            return Ret;
        }


        protected void UltraWebGridPendencias_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        protected void UltraWebGridPendencias_PageIndexChanging(Object sender, GridViewPageEventArgs e)
        {


            grvData.PageIndex = e.NewPageIndex;
            grvData.DataBind();


        }




        protected void cmd_Localizar_Click(object sender, EventArgs e)
        {

            if (txt_Nome.Text.Trim() == "") return;

            string zBusca = txt_Nome.Text.ToUpper().Trim();
            bool zSair = false;

            for (int zCont = 0; zCont < grvData.Rows.Count; zCont++)
            {
                //string zVal = "";

                GridViewRow zRow;

                zRow = grvData.Rows[zCont];

                for (int zCel = 0; zCel < zRow.Cells.Count; zCel++)
                {
                    if (zRow.Cells[zCel].Text.ToString().Trim().ToUpper().IndexOf(zBusca) >= 0)
                    {
                        this.Page.MaintainScrollPositionOnPostBack = true;
                        grvData.SelectedIndex = zCont;
                        //grvData.SelectedRow.Cells[0].Focus();
                        Page.SetFocus(zBusca);
                        zSair = true;
                        break;
                    }
                }

                if (zSair == true) break;
            }

        }




        protected void cmd_Linha_Click(object sender, EventArgs e)
        {
            if (txt_Linha.Text.Trim() == "") return;

            int myInt;
            string Validar = txt_Linha.Text.Trim();
            bool isNumerical = int.TryParse(Validar, out myInt);
            if (isNumerical == false)
            {
                MsgBox1.Show("Ilitera.Net", "Número de linha inválido", null,
                new EO.Web.MsgBoxButton("OK"));
                return;
            }

            int zLinha = 1;

            if (System.Convert.ToInt32(txt_Linha.Text) > grvData.Rows.Count)
                zLinha = grvData.Rows.Count;
            else
                zLinha = System.Convert.ToInt32(txt_Linha.Text);

            //this.Page.MaintainScrollPositionOnPostBack = true;
            grvData.SelectedIndex = zLinha;
            //grvData.DataBind();

            //grvData.SelectedRow.Cells[0].Focus();
            //grvData.SelectedRow.Focus();
            Page.SetFocus(grvData.SelectedRow.Cells[2].ToString());




        }

        protected void lst_Inconsistencias_SelectedIndexChanged(object sender, EventArgs e)
        {


            //ao clicar na inconsistencia, irá para a linha do grid
            //será carregada com a linha ocupando as 5 primeiras posições,  depois o erro encontrado



        }




        protected void cmd_CSV_Click(object sender, EventArgs e)
        {


            //pegar código da importação Fecomércio de colaboradores, está quase tudo lá

            Ilitera.Common.Pessoa rEmpresa = new Ilitera.Common.Pessoa();

            //int zId = 0;


            string xFilial_Origem = "";
            string xCNPJ_Origem = "";
            string xFilial_Destino = "";
            string xCNPJ_Destino = "";
            string xData_Inicial = "";
            string xDia_Data_Inicial = "";
            string xInativar_Origem = "";
            string xCPF = "";
            string xData_Demissao = "";
            string xDia_Data_Demissao = "";
            string xFuncao = "";
            string xSetor = "";
            string xGHE = "";

            string rSelect = "";

            string xRetorno = "";


            try
            {

                string f_Formato_Importacao = "";

                f_Formato_Importacao = lbl_Formato.Text.Trim();





                Entities.Usuario xUser = (Entities.Usuario)Session["usuarioLogado"];



                //talvez tenha que pensar outra maneira de trabalhar com transferência, pois empresa pode ser diferente registro a registro

                Importacao_Planilha_Transf xImportacao = new Importacao_Planilha_Transf();
                xImportacao.IdUsuario = xUser.IdUsuario;                
                xImportacao.DataImportacao = System.DateTime.Now;
                xImportacao.Save();



                txt_Status.Text = "";
                lst_Inconsistencias.Items.Clear();


                for (int zCont = 1; zCont < grvData.Rows.Count; zCont++)
                {
                    string zVal = "";

                    GridViewRow zRow;
                    zRow = grvData.Rows[zCont];

                    zVal = HttpUtility.HtmlDecode(zRow.Cells[System.Convert.ToInt16(lblC_CNPJ_Origem.Text)].Text.ToString().Trim().Replace("&nbsp;", ""));



                    //checar se campo CNPJ_Origem preenchido para avaliar linha
                    if (zVal != "")
                    {

                        for (int zCel = 1; zCel < zRow.Cells.Count; zCel++)
                        {
                            // zVal = zRow.Cells[C_NOME].Text.ToString().Trim();

                            zVal = HttpUtility.HtmlDecode(zRow.Cells[zCel].Text.ToString().Trim().Replace("&nbsp;", ""));

                            if (zCel == System.Convert.ToInt16(lblC_Filial_Origem.Text))
                            {
                                xFilial_Origem = zVal;
                            }
                            else if (zCel == System.Convert.ToInt16(lblC_CNPJ_Origem.Text))
                            {
                                xCNPJ_Origem = zVal;
                            }
                            else if (zCel == System.Convert.ToInt16(lblC_Filial_Destino.Text))
                            {
                                xFilial_Destino = zVal;
                            }
                            else if (zCel == System.Convert.ToInt16(lblC_CNPJ_Destino.Text))
                            {
                                xCNPJ_Destino = zVal;
                            }
                            else if (zCel == System.Convert.ToInt16(lblC_Dia_Data_Inicial.Text))
                            {
                                xDia_Data_Inicial = zVal;
                            }
                            else if (zCel == System.Convert.ToInt16(lblC_Dia_Data_Demissao.Text))
                            {
                                xDia_Data_Demissao = zVal;
                            }
                            else if (zCel == System.Convert.ToInt16(lblC_Data_Inicial.Text))
                            {
                                xData_Inicial = zVal;
                            }
                            else if (zCel == System.Convert.ToInt16(lblC_Inativar_Origem.Text))
                            {
                                xInativar_Origem = zVal;
                            }
                            else if (zCel == System.Convert.ToInt16(lblC_Data_Demissao.Text))
                            {
                                xData_Demissao = zVal;
                            }
                            if (zCel == System.Convert.ToInt16(lblC_CPF.Text))
                            {
                                xCPF = zVal;
                            }
                            else if (zCel == System.Convert.ToInt16(lblC_FUNCAO.Text))
                            {
                                xFuncao = zVal;
                            }
                            else if (zCel == System.Convert.ToInt16(lblC_SETOR.Text))
                            {
                                xSetor = zVal;
                            }
                            else if (zCel == System.Convert.ToInt16(lblC_GHE.Text))
                            {
                                xGHE = zVal;
                            }

                        }


                        System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");

                        string xCPF_Completo = "";

                        if (xCPF.Length < 11)
                            xCPF_Completo = new string('0', 11 - xCPF.Length) + xCPF;
                        else
                            xCPF_Completo = xCPF;




                        //talvez tenha que criar Importacao_Planilha_Transf para transferências

                        Int32 xIdOrigem = 0;
                        Int32 xIdDestino = 0;

                        Int32 xIdEmpregadoOrigem = 0;
                        Int32 xIdEmpregadoDestino = 0;


                        if (xCNPJ_Origem == xCNPJ_Destino && xFilial_Origem == xFilial_Destino)
                        {
                            //atualização classif.funcional mesmo CNPJ
                            ArrayList alCliente = new Cliente().Find(" dbo.udf_getnumeric(NomeCodigo) like '%" + SomenteNumeros(xCNPJ_Origem) + "%' ");

                            if (alCliente.Count > 1)
                            {
                                //nesse caso usar descrição para achar registro único                                
                                ArrayList alCliente2 = new Cliente().Find(" dbo.udf_getnumeric(NomeCodigo) like '%" + SomenteNumeros(xCNPJ_Origem) + "%' and NomeAbreviado = '" + xFilial_Origem + "' ");

                                foreach (Cliente rCli in alCliente2)
                                {
                                    xIdOrigem = rCli.Id;
                                }
                            }
                            else
                            {
                                foreach (Cliente rCli in alCliente)
                                {
                                    xIdOrigem = rCli.Id;
                                }

                            }



                            ArrayList list = new ArrayList();
                            Empregado empregado = new Empregado();

                            empregado.Find(" tNo_CPF = '" + xCPF + "' and nId_Empr = " + xIdOrigem.ToString() + " ");

                            if ( empregado.Id == 0 && xCPF.Length < 11)
                            {
                                empregado.Find(" tNo_CPF = '" + xCPF_Completo + "' and nId_Empr = " + xIdOrigem.ToString() + " ");
                            }

                            xIdEmpregadoOrigem = empregado.Id;
                            xIdEmpregadoDestino = empregado.Id;



                            //criar classif.funcional nova com C_DATA_INICIAL, C_SETOR e C_FUNCAO
                            xRetorno = Nova_Classificacao_Funcional(xIdOrigem, xIdEmpregadoOrigem, xGHE, xFuncao, xSetor, xData_Inicial);

                            

                        }
                        else
                        {
                            //transferência de colaborador

                            //procurar CNPJ na base de dados - Origem
                            ArrayList alCliente = new Cliente().Find(" dbo.udf_getnumeric(NomeCodigo) like '%" + SomenteNumeros(xCNPJ_Origem) + "%' ");

                            if (alCliente.Count > 1)
                            {
                                //nesse caso usar descrição para achar registro único                                
                                ArrayList alCliente2 = new Cliente().Find(" dbo.udf_getnumeric(NomeCodigo) like '%" + SomenteNumeros(xCNPJ_Origem) + "%' and NomeAbreviado = '" + xFilial_Origem + "' ");

                                foreach (Cliente rCli in alCliente2)
                                {
                                    xIdOrigem = rCli.Id;
                                }
                            }
                            else
                            {
                                foreach (Cliente rCli in alCliente)
                                {
                                    xIdOrigem = rCli.Id;
                                }

                            }


                            //procurar CNPJ na base de dados - Destino
                            ArrayList alCliente3 = new Cliente().Find(" dbo.udf_getnumeric(NomeCodigo) like '%" + SomenteNumeros(xCNPJ_Destino) + "%' ");

                            if (alCliente3.Count > 1)
                            {
                                //nesse caso usar descrição para achar registro único                                
                                ArrayList alCliente4 = new Cliente().Find(" dbo.udf_getnumeric(NomeCodigo) like '%" + SomenteNumeros(xCNPJ_Destino) + "%' and NomeAbreviado = '" + xFilial_Destino + "' ");

                                foreach (Cliente rCli in alCliente4)
                                {
                                    xIdDestino = rCli.Id;
                                }
                            }
                            else
                            {
                                foreach (Cliente rCli in alCliente3)
                                {
                                    xIdDestino = rCli.Id;
                                }
                            }



                            ArrayList list = new ArrayList();
                            Empregado empregado = new Empregado();

                            empregado.Find(" tNo_CPF = '" + xCPF + "' and nId_Empr = " + xIdOrigem.ToString() + " ");

                            if ( empregado.Id == 0 )
                            {
                                empregado.Find(" tNo_CPF = '" + xCPF_Completo + "' and nId_Empr = " + xIdOrigem.ToString() + " ");
                            }

                            xIdEmpregadoOrigem = empregado.Id;

                            list.Add(empregado);


                            if (xInativar_Origem == "M")  // criar classif.funcional apenas com CNPJ destino
                            {

                                empregado.Find(" tNo_CPF = '" + xCPF + "' and nId_Empr = " + xIdOrigem.ToString() + " ");

                                xIdEmpregadoOrigem = empregado.Id;
                                xIdEmpregadoDestino = empregado.Id;

                                if (xData_Inicial != "-" && xData_Inicial != "")
                                {
                                    //criar classif.funcional nova com C_DATA_INICIAL, C_SETOR e C_FUNCAO
                                    xRetorno = Nova_Classificacao_Funcional(xIdDestino, xIdEmpregadoOrigem, xGHE, xFuncao, xSetor, xData_Inicial);
                                }
                                else
                                {
                                    //criar classif.funcional nova com C_DATA_INICIAL, C_SETOR e C_FUNCAO
                                    xRetorno = Nova_Classificacao_Funcional(xIdDestino, xIdEmpregadoOrigem, xGHE, xFuncao, xSetor, System.DateTime.Now.ToString("dd/MM/yyyy", ptBr ));
                                }




                            }
                            else
                            {
                                if (xData_Demissao != "-" && xData_Demissao != "")
                                {
                                    DateTime gDemissaoData = new DateTime();
                                    DateTime gAdmissaoData = new DateTime();

                                    if (xDia_Data_Demissao != "")
                                    {
                                        if ( xData_Demissao.IndexOf(xDia_Data_Demissao) > 0)
                                        {
                                            System.Globalization.CultureInfo usDt = new System.Globalization.CultureInfo("en-US");
                                            gDemissaoData = System.Convert.ToDateTime(xData_Demissao, usDt);
                                        }
                                        else
                                        {
                                            gDemissaoData = System.Convert.ToDateTime(xData_Demissao, ptBr);
                                        }
                                    }
                                    else
                                    {
                                        if (Validar_Data(xData_Demissao) == false)
                                        {
                                            System.Globalization.CultureInfo usDt = new System.Globalization.CultureInfo("en-US");
                                            gDemissaoData = System.Convert.ToDateTime(xData_Demissao, usDt);
                                        }
                                        else
                                        {
                                            gDemissaoData = System.Convert.ToDateTime(xData_Demissao, ptBr);
                                        }

                                    }


                                    if (xData_Inicial != "" && xData_Inicial != "-")
                                    {
                                        if (xDia_Data_Inicial != "")
                                        {
                                            if (xData_Inicial.IndexOf(xDia_Data_Inicial) > 0)
                                            {
                                                System.Globalization.CultureInfo usDt = new System.Globalization.CultureInfo("en-US");
                                                gAdmissaoData = System.Convert.ToDateTime(xData_Inicial, usDt);
                                            }
                                            else
                                            {
                                                gAdmissaoData = System.Convert.ToDateTime(xData_Inicial, ptBr);
                                            }
                                        }
                                        else
                                        {
                                            if (Validar_Data(xData_Inicial) == false)
                                            {
                                                System.Globalization.CultureInfo usDt = new System.Globalization.CultureInfo("en-US");
                                                gAdmissaoData = System.Convert.ToDateTime(xData_Inicial, usDt);
                                            }
                                            else
                                            {
                                                gAdmissaoData = System.Convert.ToDateTime(xData_Inicial, ptBr);
                                            }
                                        }
                                    }

                                    xRetorno = TransfereEmpregado(xIdOrigem, xIdDestino, true, true, list, true, gDemissaoData, gAdmissaoData, false, true, false, xGHE);
                                }
                                else
                                {
                                    if (xData_Inicial != "-" && xData_Inicial != "")
                                    {
                                        DateTime gAdmissaoData = new DateTime();

                                        if (xDia_Data_Inicial != "")
                                        {
                                            if (xData_Inicial.IndexOf(xDia_Data_Inicial) > 0)
                                            {
                                                System.Globalization.CultureInfo usDt = new System.Globalization.CultureInfo("en-US");
                                                gAdmissaoData = System.Convert.ToDateTime(xData_Inicial, usDt);
                                            }
                                            else
                                            {
                                                gAdmissaoData = System.Convert.ToDateTime(xData_Inicial, ptBr);
                                            }

                                        }
                                        else
                                        {
                                            if (Validar_Data(xData_Inicial) == false)
                                            {
                                                System.Globalization.CultureInfo usDt = new System.Globalization.CultureInfo("en-US");
                                                gAdmissaoData = System.Convert.ToDateTime(xData_Inicial, usDt);
                                            }
                                            else
                                            {
                                                gAdmissaoData = System.Convert.ToDateTime(xData_Inicial, ptBr);
                                            }
                                        }
                                        xRetorno = TransfereEmpregado(xIdOrigem, xIdDestino, true, true, list, false, System.DateTime.Now, gAdmissaoData, false, true, false, xGHE);
                                    }
                                    else
                                        xRetorno = TransfereEmpregado(xIdOrigem, xIdDestino, true, true, list, false, System.DateTime.Now, System.DateTime.Now, false, true, false, xGHE);
                                }
                            }


                            //como pegar o colaborador destino ?
                            //xIdEmpregadoDestino = 

                        

                        }





                        Importacao_Planilha_Transf_Detalhes xDetalhes = new Importacao_Planilha_Transf_Detalhes();
                        xDetalhes.nId_ImportacaoTransf = xImportacao.Id;
                        lbl_Id_Importacao.Text = xImportacao.Id.ToString();

                        xDetalhes.Status = xRetorno;
                        xDetalhes.CPF = xCPF;
                        xDetalhes.CNPJ_Origem = SomenteNumeros(xCNPJ_Origem);
                        xDetalhes.Filial_Origem = xFilial_Origem;
                        xDetalhes.CNPJ_Destino = SomenteNumeros(xCNPJ_Destino);
                        xDetalhes.Filial_Destino = xFilial_Destino;

                        //System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");

                        if ( xData_Demissao!="-" && xData_Demissao != "")
                        {
                                DateTime gDemissaoData = new DateTime();

                                if (xDia_Data_Demissao != "")
                                {
                                    if (xData_Demissao.IndexOf(xDia_Data_Demissao) > 0)
                                    {
                                        System.Globalization.CultureInfo usDt = new System.Globalization.CultureInfo("en-US");
                                        gDemissaoData = System.Convert.ToDateTime(xData_Demissao, usDt);
                                    }
                                    else
                                    {
                                        gDemissaoData = System.Convert.ToDateTime(xData_Demissao, ptBr);
                                    }
                                }
                                else
                                {
                                    if (Validar_Data(xData_Demissao) == false)
                                    {
                                        System.Globalization.CultureInfo usDt = new System.Globalization.CultureInfo("en-US");
                                        gDemissaoData = System.Convert.ToDateTime(xData_Demissao, usDt);
                                    }
                                    else
                                    {
                                        gDemissaoData = System.Convert.ToDateTime(xData_Demissao, ptBr);
                                    }

                                }
                                
                            xDetalhes.Data_Demissao = gDemissaoData; //System.Convert.ToDateTime(xData_Demissao, ptBr);
                        }

                        if (xData_Inicial != "-" && xData_Inicial != "")
                        {
                            DateTime gAdmissaoData = new DateTime();

                            if (xDia_Data_Inicial != "")
                            {
                                if (xData_Inicial.IndexOf(xDia_Data_Inicial) > 0)
                                {
                                    System.Globalization.CultureInfo usDt = new System.Globalization.CultureInfo("en-US");
                                    gAdmissaoData = System.Convert.ToDateTime(xData_Inicial, usDt);
                                }
                                else
                                {
                                    gAdmissaoData = System.Convert.ToDateTime(xData_Inicial, ptBr);
                                }
                            }
                            else
                            {
                                if (Validar_Data(xData_Inicial) == false)
                                {
                                    System.Globalization.CultureInfo usDt = new System.Globalization.CultureInfo("en-US");
                                    gAdmissaoData = System.Convert.ToDateTime(xData_Inicial, usDt);
                                }
                                else
                                {
                                    gAdmissaoData = System.Convert.ToDateTime(xData_Inicial, ptBr);
                                }
                            }


                            xDetalhes.Data_Inicial = gAdmissaoData; //System.Convert.ToDateTime(xData_Inicial, ptBr);
                        }

                        xDetalhes.Setor = xSetor;
                        xDetalhes.Funcao = xFuncao;
                        xDetalhes.GHE = xGHE;

                        //colocar Id_Empr Origem e Destino
                        xDetalhes.IdEmpr_Origem = xIdOrigem;
                        xDetalhes.IdEmpr_Destino = xIdDestino;

                        //coloco IdEmpregadoOrigem e IdEmpregadoDestino  
                        xDetalhes.nIdEmpregado_Origem = xIdEmpregadoOrigem;
                        xDetalhes.nIdEmpregado_Destino = xIdEmpregadoDestino;

                        xDetalhes.Save();

                        lst_Processamento.Items.Add(" (" + xCPF + " ) - " + xRetorno);

                        txt_Status.Text = txt_Status.Text + " (" + xCPF + " ) - " + xRetorno +  System.Environment.NewLine;                            

                        if (txt_Status.Text != "")
                        {
                            lst_Inconsistencias.Items.Add(txt_Status.Text);
                        }

                        txt_Status.Text = "";


                    }


                }



            }
            catch (Exception ex)
            {
                MsgBox1.Show("Ilitera.Net", ex.ToString(), null,
                new EO.Web.MsgBoxButton("OK"));
                return;

            }
            finally
            {

            }



            lbl_Processamento.Visible = true;
            lst_Processamento.Visible = true;
            cmd_Analisar.Visible = false;
            cmd_Importar.Visible = false;

            cmd_Cancelar.Visible = false;
            cmd_Exibir_Log.Visible = true;


            MsgBox1.Show("Ilitera.Net", "Importação finalizada.", null,
            new EO.Web.MsgBoxButton("OK"));

            return;


        }

        


        protected void grvData_SelectedIndexChanged(object sender, EventArgs e)
        {

            grvData.SelectedRow.Cells[0].Focus();


        }


        private void ScrollGrid()
        {
            int intScrollTo = this.grvData.SelectedIndex * (int)this.grvData.RowStyle.Height.Value;
            string strScript = string.Empty;
            strScript += "var gridView = document.getElementById('" + this.grvData.ClientID + "');\n";
            strScript += "if (gridView != null && gridView.parentElement != null && gridView.parentElement.parentElement != null)\n";
            strScript += "  gridView.parentElement.parentElement.scrollTop = " + intScrollTo + ";\n";
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "ScrollGrid", strScript, true);
        }




        protected void cmd_Cancelar_Processo_Click(object sender, EventArgs e)
        {

            cmd_Importar.Visible = false;
            lst_Inconsistencias.Items.Clear();
            lst_Inconsistencias.Visible = false;

            lst_Processamento.Items.Clear();
            lst_Processamento.Visible = false;

            txt_Status.Text = "";
            lbl_Inconsistencias.Visible = false;
            lbl_Processamento.Visible = false;

            grvData.DataSource = null;
            grvData.DataBind();

            cmd_Analisar.Visible = true;
            File1.Visible = true;

            lblC_Data1.Visible = false;
            lblC_Data.Visible = false;
            lblC_Login.Visible = false;
            lblC_Login1.Visible = false;

            lblC_Login.Text = "";
            lblC_Data.Text = "";

            lbl_Arq.Text = "";

        }

        protected void cmd_Exibir_Log_Click(object sender, EventArgs e)
        {


            //gerar csv com status de cada registro
            string xFile = "Relat_Importacao_" + System.DateTime.Now.Second.ToString().Trim() + System.DateTime.Now.Millisecond.ToString().Trim() + System.DateTime.Now.Minute.ToString().Trim() + System.DateTime.Now.Day.ToString().Trim() + System.DateTime.Now.Month.ToString().Trim() + ".csv";
            string myStringWebResource = "I:\\temp\\" + xFile;
            string zLinha = "";

            Ilitera.Data.Clientes_Funcionarios xRep = new Clientes_Funcionarios();

            DataSet zDs = xRep.Trazer_Dados_Importacao_Transf(System.Convert.ToInt32(lbl_Id_Importacao.Text));

            TextWriter tw = new StreamWriter(myStringWebResource, false, Encoding.GetEncoding(1252));

            zLinha = "Colaborador;NomeUsuario;DataImportacao;Cliente;Status;CPF;Filial_Origem;CNPJ_Origem;Filial_Destino;CNPJ_Destino;DataInicial;Setor;Funcao;Inativar_Origem;DataDemissao;GHE";
            tw.WriteLine(zLinha);
            
            for (int bCont = 0; bCont < zDs.Tables[0].Rows.Count; bCont++)
            {
                zLinha = "";

                for (int bAux = 0; bAux < zDs.Tables[0].Columns.Count; bAux++)
                {
                    zLinha = zLinha + zDs.Tables[0].Rows[bCont][bAux].ToString().Trim().Replace("\r\n","").Replace("\n","") + ";";
                }

                tw.WriteLine(zLinha);
            }

            tw.Close();

            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            String Header = "Attachment; Filename=" + xFile;
            HttpContext.Current.Response.AppendHeader("Content-Disposition", Header);
            System.IO.FileInfo Dfile = new System.IO.FileInfo(myStringWebResource); //HttpContext.Current.Server.MapPath(myStringWebResource));
            HttpContext.Current.Response.WriteFile(Dfile.FullName);
            HttpContext.Current.Response.End();



        }


        private bool Validar_Data2(string zVal)
        {
            string dateString = zVal.Replace("-", "/");
            string format = "MM/dd/yyyy";

            DateTime result;
            if (DateTime.TryParseExact(dateString, format, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out result))
            {
                return true;
            }
            else
            {
                format = "M/dd/yyyy";

                if (DateTime.TryParseExact(dateString, format, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out result))
                {
                    return true;
                }
                else
                {
                    format = "MM/d/yyyy";

                    if (DateTime.TryParseExact(dateString, format, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out result))
                    {
                        return true;
                    }
                    else
                    {
                        format = "M/d/yyyy";

                        if (DateTime.TryParseExact(dateString, format, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out result))
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }

                    }

                }

            }

        }



        private bool Validar_Data(string zVal)
        {
            string dateString = zVal.Replace("-", "/");
            string format = "dd/MM/yyyy";

            DateTime result;
            if (DateTime.TryParseExact(dateString, format, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out result))
            {
                return true;
            }
            else
            {
                format = "dd/M/yyyy";

                if (DateTime.TryParseExact(dateString, format, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out result))
                {
                    return true;
                }
                else
                {
                    format = "d/MM/yyyy";

                    if (DateTime.TryParseExact(dateString, format, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out result))
                    {
                        return true;
                    }
                    else
                    {
                        format = "d/M/yyyy";

                        if (DateTime.TryParseExact(dateString, format, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out result))
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }

                    }

                }

            }

        }



        private DateTime Retornar_Data(string zVal, string zFormato)
        {
            string dateString = zVal.Replace("-", "/");

            string format = "dd/MM/yyyy";
            string format2 = "d/MM/yyyy";
            string format3 = "dd/M/yyyy";
            string format4 = "d/M/yyyy";

            if (zFormato == "MM/dd/yyyy")
            {
                format = "MM/dd/yyyy";
                format2 = "MM/d/yyyy";
                format3 = "M/dd/yyyy";
                format4 = "M/d/yyyy";
            }

            DateTime result;

            if (DateTime.TryParseExact(dateString, format, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out result))
            {
                return result;
            }
            else
            {

                if (DateTime.TryParseExact(dateString, format2, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out result))
                {
                    return result;
                }
                else
                {

                    if (DateTime.TryParseExact(dateString, format3, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out result))
                    {
                        return result;
                    }
                    else
                    {

                        if (DateTime.TryParseExact(dateString, format4, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out result))
                        {
                            return result;
                        }
                        else
                        {
                            return new DateTime(2000, 1, 1);
                        }

                    }

                }

            }

        }




        private static string TransfereEmpregado(int IdClienteOrigem,
                                int IdClienteDestino,
                                bool ComClassificacaoFuncional,
                                bool ComProntuarioDigital,
                                ArrayList listEmpregados,
                                bool ComDemissaoReadmissao,
                                DateTime Demissao,
                                DateTime Admissao,
                                bool ExcluirOrigem,
                                bool ComAfastamentos,
                                bool ExcluirAfastamentos,
                                string zGHE)
        {

            try
            {


                foreach (Empregado empregado in listEmpregados)
                {
                    if (ComDemissaoReadmissao && empregado.hDT_DEM != new DateTime())
                        continue;

                    Empregado newEmpregado = new Empregado();

                    string where = "nID_EMPR=" + IdClienteDestino
                                + " AND tNO_EMPG='" + empregado.tNO_EMPG.Replace("'", "''").Trim() + "'";

                    newEmpregado.Find(where);

                    int IdEmpregado = newEmpregado.Id;

                    newEmpregado = (Empregado)empregado.Clone();

                    Cliente zCliente = new Cliente();
                    zCliente.Find(IdClienteDestino);


                    newEmpregado.Id = IdEmpregado;
                    //newEmpregado.nID_EMPR.Id = IdClienteDestino;
                    newEmpregado.nID_EMPR = zCliente;
                    newEmpregado.nID_EMPR.Find();

                    if (ComDemissaoReadmissao)
                    {
                        newEmpregado.hDT_ADM = Admissao;
                        //Demite o empregado
                        empregado.hDT_DEM = Demissao;
                        empregado.Save();
                    }

                    newEmpregado.Save();






                    ////Tranansfere Treinamento
                    //ArrayList listTreinamento = new ParticipanteTreinamento().Find("IdEmpregado=" + empregado.Id.ToString());
                    //foreach (ParticipanteTreinamento treinamento in listTreinamento)
                    //{
                    //    ParticipanteTreinamento xPart = new ParticipanteTreinamento();

                    //    xPart = (ParticipanteTreinamento)treinamento.Clone();                                       
                    //    xPart.Id = 0;
                    //    xPart.IdEmpregado.Id = newEmpregado.Id;
                    //    xPart.Save();
                    //}


                    if (ComProntuarioDigital)
                        ProntuarioDigital.CopiaProntuarioDigital(empregado, newEmpregado);

                    ArrayList list;

                    //ProntuarioDigital.TransfereProntuarioDigital(empregado, clienteOrigem, clienteDestino);

                    if (ComClassificacaoFuncional)
                    {


                        //if (ComDemissaoReadmissao)
                        //    list = new EmpregadoFuncao().Find("nID_EMPREGADO=" + empregado.Id + " AND hDT_TERMINO IS NULL");
                        //else
                        list = new EmpregadoFuncao().Find("nID_EMPREGADO=" + empregado.Id + " order by hdt_inicio ");

                        int zEF = 0;


                        foreach (EmpregadoFuncao empregadoFuncao in list)
                        {
                            EmpregadoFuncao newEmpregadoFuncao = new EmpregadoFuncao();

                            int IdLocalDeTrabalho = 0;

                            if (empregadoFuncao.nID_EMPR.Id == IdClienteOrigem)
                                IdLocalDeTrabalho = IdClienteDestino;
                            else
                                IdLocalDeTrabalho = empregadoFuncao.nID_EMPR.Id;

                            Cliente xCliente = new Cliente(IdClienteDestino);

                            empregadoFuncao.nID_FUNCAO.Find();

                            Funcao funcao = new Funcao();
                            Funcao xFuncao = new Funcao();
                            funcao.Find("IdCliente=" + IdClienteDestino
                                + " AND NomeFuncao='" + empregadoFuncao.nID_FUNCAO.NomeFuncao.Replace("'", "''").Trim() + "'");

                            if (funcao.Id == 0)
                            {

                                xFuncao = (Funcao)empregadoFuncao.nID_FUNCAO.Clone();

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

                            Setor setor = new Setor();
                            Setor xSetor = new Setor();

                            setor.Find("nID_EMPR=" + IdClienteDestino
                                + " AND tNO_STR_EMPR='" + empregadoFuncao.nID_SETOR.tNO_STR_EMPR.Replace("'", "''").Trim() + "'");

                            if (setor.Id == 0)
                            {

                                xSetor = (Setor)empregadoFuncao.nID_SETOR.Clone();

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

                            Cargo xCargo = new Cargo();

                            if (empregadoFuncao.nID_CARGO.tNO_CARGO.Trim() != "")
                            {
                                Cargo cargo = new Cargo();
                                cargo.Find("nID_EMPR=" + IdClienteDestino
                                    + " AND tNO_Cargo='" + empregadoFuncao.nID_CARGO.tNO_CARGO.Replace("'", "''").Trim() + "'");

                                if (cargo.Id == 0)
                                {

                                    xCargo = (Cargo)empregadoFuncao.nID_CARGO.Clone();

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



                            newEmpregadoFuncao.Find("nID_EMPREGADO=" + newEmpregado.Id
                                                    + " AND nID_EMPR=" + IdClienteDestino
                                                    + " AND nID_FUNCAO=" + xFuncao.Id
                                                    + " AND nID_SETOR=" + xSetor.Id);

                            if (newEmpregadoFuncao.Id == 0)
                            {
                                System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");

                                newEmpregadoFuncao.Find("nID_EMPREGADO=" + newEmpregado.Id
                                                   + " AND hDT_Inicio = convert( smalldatetime,'" + empregadoFuncao.hDT_INICIO.ToString("dd/MM/yyyy",ptBr) + "', 103 )");

                                if (newEmpregadoFuncao.Id == 0)
                                {

                                    newEmpregadoFuncao = (EmpregadoFuncao)empregadoFuncao.Clone();
                                    newEmpregadoFuncao.Id = 0;
                                    newEmpregadoFuncao.nID_EMPR.Id = IdLocalDeTrabalho;
                                    newEmpregadoFuncao.nID_EMPREGADO.Id = newEmpregado.Id;
                                    newEmpregadoFuncao.nID_FUNCAO.Id = xFuncao.Id;
                                    newEmpregadoFuncao.nID_SETOR.Id = xSetor.Id;
                                    newEmpregadoFuncao.nID_CARGO.Id = xCargo.Id;

                                    if (ComDemissaoReadmissao)
                                    {
                                        newEmpregadoFuncao.hDT_INICIO = Admissao;
                                        newEmpregadoFuncao.hDT_TERMINO = new DateTime();


                                        empregadoFuncao.hDT_TERMINO = Demissao;
                                        empregadoFuncao.Save();
                                    }

                                    newEmpregadoFuncao.Save();



                                }

                            }


                            //ghe deveria vir aqui
                            if (zGHE != "")
                            {
                                //apenas ultima classif.funcional
                                if (zEF == list.Count - 1)
                                {

                                    newEmpregadoFuncao.nID_EMPR.Find();

                                    ArrayList alGHE = new Ghe().Find("  tno_Func = '" + zGHE + "' and nid_laud_tec in ( Select top 1 nid_Laud_Tec from tbllaudo_tec where nid_empr=" + newEmpregadoFuncao.nID_EMPR.Id.ToString() + " order by hdt_laudo desc ) ");

                                    if (alGHE.Count == 1)
                                    {

                                        foreach (Ghe nGHE in alGHE)
                                        {

                                            GheEmpregado xaLoc = new GheEmpregado();
                                            xaLoc.nID_LAUD_TEC = nGHE.nID_LAUD_TEC;
                                            xaLoc.nID_FUNC = nGHE;
                                            xaLoc.nID_EMPREGADO_FUNCAO = newEmpregadoFuncao;
                                            xaLoc.Save();

                                        }

                                    }
                                }
                            }

                            zEF = zEF + 1;

                        }

                    }




                    if (ComProntuarioDigital)
                    {
                        //Copia Exames Realizados
                        ArrayList listExames = new ExameBase().Find("IdEmpregado=" + empregado.Id.ToString());
                        foreach (ExameBase exameBase in listExames)
                        {
                            ExameBase xExame = new ExameBase();
                            Audiometria xAud = new Audiometria();
                            Clinico xClin = new Clinico();
                            Complementar xCompl = new Complementar();
                            ClinicoNaoOcupacional xNaoOC = new ClinicoNaoOcupacional();

                            xExame = (ExameBase)exameBase.Clone();
                            //xExame.LiberarPagamento = false;
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
                                Audiometria xAud2 = new Audiometria();
                                xAud2 = (Audiometria)xAud.Clone();
                                xAud2.IdEmpregado = newEmpregado;
                                xAud2.Id = 0;
                                xAud2.Save();
                            }
                            else if (xExame.IdExameDicionario.Id >= 1 && xExame.IdExameDicionario.Id <= 5)
                            {

                                list = new EmpregadoFuncao().Find("nID_EMPREGADO=" + empregado.Id + " order by hdt_inicio ");

                                EmpregadoFuncao rEmpregadoFuncao = new EmpregadoFuncao();

                                foreach (EmpregadoFuncao empregadoFuncao in list)
                                {
                                    rEmpregadoFuncao = empregadoFuncao;
                                }

                                Clinico xClin2 = new Clinico();
                                xClin2 = (Clinico)xClin.Clone();
                                xClin2.IdEmpregado = newEmpregado;
                                xClin2.IdEmpregadoFuncao = rEmpregadoFuncao;
                                xClin2.Id = 0;
                                xClin2.Save();

                                //ajustar data resultado, para pegar da origem, e não colocar getdate() - isso atrapalha rotina faturamento - Wagner 13/06/2018
                                Ilitera.Data.Clientes_Clinicas xAtualizacao = new Ilitera.Data.Clientes_Clinicas();
                                xAtualizacao.Atualizar_Data_Resultado(xClin2.Id, exameBase.Id);
                            }
                            else if (xExame.IdExameDicionario.Id == 7)
                            {
                                ClinicoNaoOcupacional xNO = new ClinicoNaoOcupacional();
                                xNO = (ClinicoNaoOcupacional)xNaoOC.Clone();
                                xNO.IdEmpregado = newEmpregado;
                                xNO.Id = 0;
                                xNO.Save();
                            }
                            else
                            {
                                Complementar xCompl2 = new Complementar();
                                xCompl2 = (Complementar)xCompl.Clone();
                                xCompl2.IdEmpregado = newEmpregado;
                                xCompl2.Id = 0;
                                xCompl2.Save();
                            }


                        }
                    }


                    //se marcado para transferir afastamentos/acidentes
                    if (ComAfastamentos)
                    {

                        ArrayList listAbs = new Afastamento().Find("IdEmpregado=" + empregado.Id.ToString() + " order by DataInicial ");


                        if (ExcluirAfastamentos)   //na verdade o afastamento será movido para o novo registro do empregado
                        {

                            foreach (Afastamento Afast in listAbs)
                            {
                                Afast.IdEmpregado.Id = newEmpregado.Id;
                                Afast.Save();

                            }

                        }
                        else
                        {

                            foreach (Afastamento Afast in listAbs)
                            {
                                Afastamento xAf = new Afastamento();

                                xAf = (Afastamento)Afast.Clone();

                                xAf.IdEmpregado.Id = newEmpregado.Id;
                                xAf.Id = 0;

                                xAf.Save();
                            }


                        }


                    }

                    ////Copia CAT
                    //ArrayList listCAT = new CAT().Find("IdEmpregado=" + empregado.Id.ToString());
                    //foreach (CAT cat in listCAT)
                    //{
                    //    CAT xCat = new CAT();

                    //    xCat = (CAT)cat.Clone();

                    //    xCat.IdEmpregado.Id = newEmpregado.Id;                    
                    //    xCat.Id = 0;

                    //    xCat.Save();
                    //}


                    ////Transfere Acidente
                    //ArrayList listAcidente = new Acidente().Find("IdEmpregado=" + empregado.Id.ToString());
                    //foreach (Acidente acidente in listAcidente)
                    //{
                    //    Acidente xAcid = new Acidente();

                    //    xAcid = (Acidente)acidente.Clone();                    
                    //    xAcid.Id = 0;
                    //    xAcid.IdEmpregado.Id = newEmpregado.Id;
                    //    xAcid.Save();
                    //}




                    if (ExcluirOrigem == true)
                    {

                        List<ProntuarioDigital> prontuarios = new ProntuarioDigital().Find<ProntuarioDigital>("IdEmpregado=" + empregado.Id);

                        foreach (ProntuarioDigital prontuarioDigital in prontuarios)
                        {
                            prontuarioDigital.Delete();
                        }



                        ArrayList listExames2 = new ExameBase().Find("IdEmpregado=" + empregado.Id.ToString());
                        foreach (ExameBase exameBase in listExames2)
                        {

                            List<ProntuarioDigital> prontuarios2 = new ProntuarioDigital().Find<ProntuarioDigital>("IdExameBase=" + exameBase.Id);
                            foreach (ProntuarioDigital prontuarioDigital in prontuarios2)
                            {
                                prontuarioDigital.Delete();
                            }


                            exameBase.Delete();
                        }


                        ArrayList list2 = new EmpregadoFuncao().Find("nID_EMPREGADO=" + empregado.Id);

                        foreach (EmpregadoFuncao empregadoFuncao2 in list2)
                        {

                            empregadoFuncao2.Delete();
                        }


                        empregado.Delete();


                    }

                }

                return "Processamento Concluído";

            }
            catch( Exception Ex)
            {
                return Ex.Message.Substring(0, 200);
            }

        }






        private static string Nova_Classificacao_Funcional(int IdCliente, Int32 IdEmpregado,
                        string zGHE, string zFuncao, string zSetor, string zDataInicial)
        {

            try
            {

                Empregado newEmpregado = new Empregado();

                newEmpregado.Find(IdEmpregado);


                Cliente xCliente = new Cliente();
                xCliente.Find(IdCliente);





                EmpregadoFuncao newEmpregadoFuncao = new EmpregadoFuncao();

                int IdLocalDeTrabalho = 0;


                Funcao funcao = new Funcao();
                Funcao xFuncao = new Funcao();

                funcao.Find("IdCliente=" + IdCliente
                    + " AND NomeFuncao='" + zFuncao.Replace("'", "''").Trim() + "'");

                if (funcao.Id == 0)
                {

                    xFuncao.IdCliente = xCliente;
                    xFuncao.NomeFuncao = zFuncao;

                    xFuncao.Save();
                }
                else
                {
                    xFuncao.Find(funcao.Id);
                }



                Setor setor = new Setor();
                Setor xSetor = new Setor();

                setor.Find("nID_EMPR=" + IdCliente
                    + " AND tNO_STR_EMPR='" + zSetor.Replace("'", "''").Trim() + "'");

                if (setor.Id == 0)
                {

                    xSetor.nID_EMPR = xCliente;
                    xSetor.tNO_STR_EMPR = zSetor;

                    xSetor.Save();


                }
                else
                {
                    xSetor.Find(setor.Id);
                }



                Cargo cargo = new Cargo();
                Cargo xCargo = new Cargo();

                cargo.Find("nID_EMPR=" + IdCliente
                         + " AND tNO_Cargo='" + zFuncao.Replace("'", "''").Trim() + "'");

                if (cargo.Id == 0)
                {

                    xCargo.tNO_CARGO = zFuncao;
                    xCargo.nID_EMPR = xCliente;

                    xCargo.Save();



                }
                else
                {
                    xCargo.Find(cargo.Id);
                }


                GheEmpregado xaLoc = new GheEmpregado();


                newEmpregadoFuncao.Find("nID_EMPREGADO=" + newEmpregado.Id
                                        + " AND nID_EMPR=" + IdCliente
                                        + " AND nID_FUNCAO=" + xFuncao.Id
                                        + " AND nID_SETOR=" + xSetor.Id
                                        + " AND hdt_Inicio = convert( smalldatetime, '" + zDataInicial + "', 103 ) ");



                if (newEmpregadoFuncao.Id == 0)
                {

                    System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");
                    EmpregadoFuncao REF = new EmpregadoFuncao();

                    try
                    {
                        REF.nID_EMPR = xCliente;
                        REF.nID_EMPREGADO = newEmpregado;
                        REF.nID_SETOR = xSetor;
                        REF.nID_FUNCAO = xFuncao;
                        REF.nID_CARGO = xCargo;
                        REF.bHOUVE_MUDANCA = false;
                        REF.bTEM_LAUDO = false;

                        REF.hDT_INICIO = System.Convert.ToDateTime(zDataInicial, ptBr);
                        REF.hDT_TERMINO = new DateTime();

                        REF.Save();

                        xaLoc.nID_EMPREGADO_FUNCAO = REF;
                    }
                    catch (Exception ex)
                    {
                        if (ex.Message.ToUpper() != "MÉTODO NÃO-ESTÁTICO REQUER UM DESTINO." && ex.Message.ToUpper() != "NON-STATIC METHOD REQUIRES A TARGET.")
                            throw new Exception(ex.Message.ToString());
                        else
                            xaLoc.nID_EMPREGADO_FUNCAO = REF;
                    }

                }
                else
                {
                    xaLoc.nID_EMPREGADO_FUNCAO = newEmpregadoFuncao;
                }


                //ghe deveria vir aqui
                if (zGHE != "")
                {
                    ArrayList alGHE = new Ghe().Find("  tno_Func = '" + zGHE + "' and nid_laud_tec in ( Select top 1 nid_Laud_Tec from tbllaudo_tec where nid_empr=" + IdCliente.ToString() + " order by hdt_laudo desc ) ");

                    if (alGHE.Count == 1)
                    {

                        foreach (Ghe nGHE in alGHE)
                        {
                            xaLoc.nID_LAUD_TEC = nGHE.nID_LAUD_TEC;
                            xaLoc.nID_FUNC = nGHE;

                            xaLoc.Save();

                        }

                    }
                }

                return "Processamento concluído";

            }
            catch ( Exception ex)
            {
                if (ex.Message.Length > 200)
                    return ex.Message.Substring(0, 200);
                else
                    return ex.Message;
            }


        }


    }
}



 