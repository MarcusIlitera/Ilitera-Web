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

using System.Text;
using Ilitera.Common;


namespace Ilitera.Net
{
    public partial class Importar_Planilha : System.Web.UI.Page
    {


        protected void Page_Load(object sender, EventArgs e)
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

            //arquivo modelo
            //I:\Pasta_Desenvolvimento\Planilha_Modelo_ImpAut.xlsx


            //procurar colunas
            int C_NOME = 0;
            int C_MATRICULA =0;
            int C_NIT = 0;
            int C_CTPS = 0;
            int C_SERIE = 0;
            int C_UF = 0;
            int C_RG = 0;
            int C_CPF = 0;
            int C_SEXO = 0;
            int C_NASCIMENTO = 0;
            int C_ADMISSAO = 0;
            int C_FUNCAO = 0;
            int C_CARGO = 0;
            int C_SETOR = 0;
            int C_CBO = 0;

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
                                if (zVal.ToUpper() == "C_NOME")
                                {
                                    C_NOME = zCel;

                                    if (zLinha_Cab == 0) zLinha_Cab = zCont;

                                    if (zLinha_Cab != zCont)
                                    {
                                        lst_Inconsistencias.Items.Add("Linha do cabeçalho apresenta inconsistência C_NOME - Impossibilitada Importação");
                                        zImportacao_Impossibilitada = true;
                                    }
                                }
                                else if (zVal.ToUpper() == "C_MATRICULA")
                                {
                                    C_MATRICULA = zCel;

                                    if (zLinha_Cab == 0) zLinha_Cab = zCont;

                                    if (zLinha_Cab != zCont)
                                    {
                                        lst_Inconsistencias.Items.Add("Linha do cabeçalho apresenta inconsistência C_MATRICULA - Impossibilitada Importação");
                                        zImportacao_Impossibilitada = true;
                                    }
                                }
                                else if (zVal.ToUpper() == "C_NIT")
                                {
                                    C_NIT = zCel;

                                    if (zLinha_Cab == 0) zLinha_Cab = zCont;

                                    if (zLinha_Cab != zCont)
                                    {
                                        lst_Inconsistencias.Items.Add("Linha do cabeçalho apresenta inconsistência C_NIT - Impossibilitada Importação");
                                        zImportacao_Impossibilitada = true;
                                    }
                                }
                                else if (zVal.ToUpper() == "C_CTPS")
                                {
                                    C_CTPS = zCel;

                                    if (zLinha_Cab == 0) zLinha_Cab = zCont;

                                    if (zLinha_Cab != zCont)
                                    {
                                        lst_Inconsistencias.Items.Add("Linha do cabeçalho apresenta inconsistência C_CTPS - Impossibilitada Importação");
                                        zImportacao_Impossibilitada = true;
                                    }
                                }
                                else if (zVal.ToUpper() == "C_SERIE")
                                {
                                    C_SERIE = zCel;

                                    if (zLinha_Cab == 0) zLinha_Cab = zCont;

                                    if (zLinha_Cab != zCont)
                                    {
                                        lst_Inconsistencias.Items.Add("Linha do cabeçalho apresenta inconsistência C_SERIE - Impossibilitada Importação");
                                        zImportacao_Impossibilitada = true;
                                    }
                                }
                                else if (zVal.ToUpper() == "C_UF")
                                {
                                    C_UF = zCel;

                                    if (zLinha_Cab == 0) zLinha_Cab = zCont;

                                    if (zLinha_Cab != zCont)
                                    {
                                        lst_Inconsistencias.Items.Add("Linha do cabeçalho apresenta inconsistência C_UF - Impossibilitada Importação");
                                        zImportacao_Impossibilitada = true;
                                    }
                                }
                                else if (zVal.ToUpper() == "C_RG")
                                {
                                    C_RG = zCel;

                                    if (zLinha_Cab == 0) zLinha_Cab = zCont;

                                    if (zLinha_Cab != zCont)
                                    {
                                        lst_Inconsistencias.Items.Add("Linha do cabeçalho apresenta inconsistência C_RG - Impossibilitada Importação");
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
                                else if (zVal.ToUpper() == "C_SEXO")
                                {
                                    C_SEXO = zCel;

                                    if (zLinha_Cab == 0) zLinha_Cab = zCont;

                                    if (zLinha_Cab != zCont)
                                    {
                                        lst_Inconsistencias.Items.Add("Linha do cabeçalho apresenta inconsistência C_SEXO - Impossibilitada Importação");
                                        zImportacao_Impossibilitada = true;
                                    }
                                }
                                else if (zVal.ToUpper() == "C_NASCIMENTO")
                                {
                                    C_NASCIMENTO = zCel;

                                    if (zLinha_Cab == 0) zLinha_Cab = zCont;

                                    if (zLinha_Cab != zCont)
                                    {
                                        lst_Inconsistencias.Items.Add("Linha do cabeçalho apresenta inconsistência C_NASCIMENTO - Impossibilitada Importação");
                                        zImportacao_Impossibilitada = true;
                                    }
                                }
                                else if (zVal.ToUpper() == "C_ADMISSAO")
                                {
                                    C_ADMISSAO = zCel;

                                    if (zLinha_Cab == 0) zLinha_Cab = zCont;

                                    if (zLinha_Cab != zCont)
                                    {
                                        lst_Inconsistencias.Items.Add("Linha do cabeçalho apresenta inconsistência C_ADMISSAO - Impossibilitada Importação");
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
                                else if (zVal.ToUpper() == "C_CARGO")
                                {
                                    C_CARGO = zCel;

                                    if (zLinha_Cab == 0) zLinha_Cab = zCont;

                                    if (zLinha_Cab != zCont)
                                    {
                                        lst_Inconsistencias.Items.Add("Linha do cabeçalho apresenta inconsistência C_CARGO - Impossibilitada Importação");
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
                                else if (zVal.ToUpper() == "C_CBO")
                                {
                                    C_CBO = zCel;

                                    if (zLinha_Cab == 0) zLinha_Cab = zCont;

                                    if (zLinha_Cab != zCont)
                                    {
                                        lst_Inconsistencias.Items.Add("Linha do cabeçalho apresenta inconsistência C_CBO - Impossibilitada Importação");
                                        zImportacao_Impossibilitada = true;
                                    }
                                }
                                else if (zVal.ToUpper() == "C_CNPJ")
                                {
                                    //considerando CNPJ na coluna a frente
                                    C_CNPJ = SomenteNumeros(HttpUtility.HtmlDecode(zRow.Cells[zCel + 1].Text.ToString().Trim()));

                                }


                            }
                            zRow.Cells[0].Text = zCont.ToString();

                        }


                        grvData.SelectedIndex = 0;





                        //CHECAR INCONSISTENCIAS

                        if (C_CNPJ == "")
                        {
                            lst_Inconsistencias.Items.Add("CNPJ não localizado - Impossibilita Importação");
                            zImportacao_Impossibilitada = true;
                        }
                        else
                        {
                            //checar se CNPJ bate com valor fornecido
                            Cliente cliente;
                            cliente = new Cliente(System.Convert.ToInt32(Session["Empresa"].ToString()));

                            Ilitera.Common.Pessoa rEmpresa = new Ilitera.Common.Pessoa();

                            string rSelect = " NomeAbreviado = '" + cliente.NomeAbreviado.Trim() + "' and dbo.udf_getnumeric( nomecodigo ) = '" + C_CNPJ + "' and IdPessoa in ( select IdCliente from Cliente ) ";

                            //pegar Id Empresa                    
                            rEmpresa.Find(rSelect);

                            //se não achar empresa,  emitir retorno avisando.  
                            if (rEmpresa.Id == 0)
                            {
                                lst_Inconsistencias.Items.Add("CNPJ da planilha não bate com dados cadastrais da Empresa selecionada - Impossibilita Importação");
                                zImportacao_Impossibilitada = true;
                            }

                            lblC_CNPJ.Text = C_CNPJ;

                        }





                        if (C_NOME == 0)
                        {
                            lst_Inconsistencias.Items.Add("C_NOME não localizado - Impossibilita Importação");
                            zImportacao_Impossibilitada = true;
                        }

                        if (C_MATRICULA == 0)
                            lst_Inconsistencias.Items.Add("C_MATRICULA não localizado  ");

                        if (C_NIT == 0)
                            lst_Inconsistencias.Items.Add("C_NIT não localizado  ");

                        if (C_CTPS == 0)
                            lst_Inconsistencias.Items.Add("C_CTPS não localizado  ");

                        if (C_SERIE == 0)
                            lst_Inconsistencias.Items.Add("C_SERIE não localizado  ");

                        if (C_UF == 0)
                            lst_Inconsistencias.Items.Add("C_UF não localizado  ");

                        if (C_RG == 0)
                            lst_Inconsistencias.Items.Add("C_RG não localizado  ");

                        if (C_CPF == 0)
                        {
                            lst_Inconsistencias.Items.Add("C_CPF não localizado - Impossibilitada Importação  ");
                            zImportacao_Impossibilitada = true;
                        }

                        if (C_SEXO == 0)
                            lst_Inconsistencias.Items.Add("C_SEXO não localizado  ");

                        if (C_NASCIMENTO == 0)
                        {
                            lst_Inconsistencias.Items.Add("C_NASCIMENTO não localizado - Impossibilitada Importação ");
                            zImportacao_Impossibilitada = true;
                        }

                        if (C_ADMISSAO == 0)
                        {
                            lst_Inconsistencias.Items.Add("C_ADMISSAO não localizado - Impossibilitada Importação  ");
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

                        if (C_CBO == 0)
                            lst_Inconsistencias.Items.Add("C_CBO não localizado  ");



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

                                    zVal = zRow.Cells[C_NOME].Text.ToString().Trim().Replace("&nbsp;", "");

                                    //checar se campo NOME preenchido para avaliar linha
                                    if (zVal != "")
                                    {


                                        for (int zCel = 1; zCel < zRow.Cells.Count; zCel++)
                                        {
                                            // zVal = zRow.Cells[C_NOME].Text.ToString().Trim();

                                            zVal = zRow.Cells[zCel].Text.ToString().Trim();

                                            if (zCel == C_ADMISSAO || zCel == C_NASCIMENTO)
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



                                

                                if (f_DDMMYYYY >= f_MMDDYYYY )
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

                                    zVal = zRow.Cells[C_NOME].Text.ToString().Trim().Replace("&nbsp;", "");

                                    //checar se campo NOME preenchido para avaliar linha
                                    if (zVal != "")
                                    {


                                        for (int zCel = 1; zCel < zRow.Cells.Count; zCel++)
                                        {
                                            // zVal = zRow.Cells[C_NOME].Text.ToString().Trim();

                                            zVal = zRow.Cells[zCel].Text.ToString().Trim();

                                            if (zCel == C_ADMISSAO || zCel == C_NASCIMENTO)
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
                                                else if ( f_MMDDYYYY > 0 )
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
                                            else if (zCel == C_NIT || zCel == C_CPF || zCel == C_SERIE || zCel == C_CTPS || zCel == C_CBO)
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
                                            else if (zCel == C_RG)
                                            {
                                                bool isNumerical = true;
                                                Int64 myInt;

                                                //checar numerico
                                                String Validar = zVal.ToUpper().Replace(".", "").Replace("-", "").Replace("X", "");
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

                                            else if (zCel == C_SETOR || zCel == C_FUNCAO || zCel == C_NOME || zCel == C_SEXO)
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
                                            else if (zCel == C_UF)
                                            {
                                                //checar string ( tamanho mínimo )
                                                if (zVal.Length < 2)
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
                                            else if (zCel == C_MATRICULA || zCel == C_CARGO)
                                            {
                                                grvData.Rows[zCont].Cells[zCel].BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFF99");
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
                                            cmd_Importar.Visible = true;
                                            cmd_Importar.Enabled = true;

                                            cmd_Cancelar.Visible = true;
                                            cmd_Cancelar.Enabled = true;

                                            lbl_Selecionar.Visible = false;
                                            File1.Visible = false;
                                            cmd_Analisar.Visible = false;

                                            lblC_ADMISSAO.Text = C_ADMISSAO.ToString().Trim();
                                            lblC_CARGO.Text = C_CARGO.ToString().Trim();
                                            lblC_CBO.Text = C_CBO.ToString().Trim();
                                            lblC_CPF.Text = C_CPF.ToString().Trim();
                                            lblC_CTPS.Text = C_CTPS.ToString().Trim();
                                            lblC_FUNCAO.Text = C_FUNCAO.ToString().Trim();
                                            lblC_MATRICULA.Text = C_MATRICULA.ToString().Trim();
                                            lblC_NASCIMENTO.Text = C_NASCIMENTO.ToString().Trim();
                                            lblC_NIT.Text = C_NIT.ToString().Trim();
                                            lblC_NOME.Text = C_NOME.ToString().Trim();
                                            lblC_RG.Text = C_RG.ToString().Trim();
                                            lblC_SERIE.Text = C_SERIE.ToString().Trim();
                                            lblC_SETOR.Text = C_SETOR.ToString().Trim();
                                            lblC_SEXO.Text = C_SEXO.ToString().Trim();
                                            lblC_UF.Text = C_UF.ToString().Trim();

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



        private static string  SomenteNumeros(string str)
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




        //protected Boolean Validar_Data(string zData)
        //{

        //int zDia = 0;
        //int zMes = 0;
        //int zAno = 0;

        //string Validar;
        //bool isNumerical;
        //int myInt;


        //if (zData.Length != 10)
        //{
        //    //MsgBox1.Show("Ilitera.Net", "Data Inválida.  Utilizar formato dd/MM/yyyy", null,
        //    //new EO.Web.MsgBoxButton("OK"));
        //    return false;
        //}

        //Validar = zData.Substring(0, 2);
        //isNumerical = int.TryParse(Validar, out myInt);
        //if (isNumerical == false)
        //{
        //    //MsgBox1.Show("Ilitera.Net", "Dia Inválido.  Utilizar formato dd/MM/yyyy", null,
        //    //new EO.Web.MsgBoxButton("OK"));
        //    return false;
        //}


        //Validar = zData.Substring(3, 2);
        //isNumerical = int.TryParse(Validar, out myInt);
        //if (isNumerical == false)
        //{
        //    //MsgBox1.Show("Ilitera.Net", "Mês Inválido.  Utilizar formato dd/MM/yyyy", null,
        //    //new EO.Web.MsgBoxButton("OK"));
        //    return false;
        //}


        //Validar = zData.Substring(6, 4);
        //isNumerical = int.TryParse(Validar, out myInt);
        //if (isNumerical == false)
        //{
        //    //MsgBox1.Show("Ilitera.Net", "Ano Inválido.  Utilizar formato dd/MM/yyyy", null,
        //    //new EO.Web.MsgBoxButton("OK"));
        //    return false;
        //}


        //if ( ( zData.Substring(2, 1) != "/" && zData.Substring(2, 1) != "-" ) || ( zData.Substring(5, 1) != "/"  &&  zData.Substring(5, 1) != "-" ))
        //{
        //    //MsgBox1.Show("Ilitera.Net", "Data Inválida.  Utilizar formato dd/MM/yyyy", null,
        //    //new EO.Web.MsgBoxButton("OK"));
        //    return false;
        //}


        //zDia = System.Convert.ToInt32(zData.Substring(0, 2));
        //zMes = System.Convert.ToInt32(zData.Substring(3, 2));
        //zAno = System.Convert.ToInt32(zData.Substring(6, 4));

        //if (zAno < 1900 || zAno > 2025)
        //{
        //    //MsgBox1.Show("Ilitera.Net", "Ano Inválido.  Utilizar formato dd/MM/yyyy", null,
        //    //new EO.Web.MsgBoxButton("OK"));
        //    return false;
        //}

        //if (zMes < 1 || zMes > 12)
        //{
        //    //MsgBox1.Show("Ilitera.Net", "Mês Inválido.  Utilizar formato dd/MM/yyyy", null,
        //    //new EO.Web.MsgBoxButton("OK"));
        //    return false;
        //}

        //if (zMes == 1 || zMes == 3 || zMes == 5 || zMes == 7 || zMes == 8 || zMes == 10 || zMes == 12)
        //{
        //    if (zDia < 1 || zDia > 31)
        //    {
        //        //MsgBox1.Show("Ilitera.Net", "Dia Inválido.  Utilizar formato dd/MM/yyyy", null,
        //        //new EO.Web.MsgBoxButton("OK"));
        //        return false;
        //    }
        //}
        //else if (zMes == 4 || zMes == 6 || zMes == 9 || zMes == 11)
        //{
        //    if (zDia < 1 || zDia > 30)
        //    {
        //        //MsgBox1.Show("Ilitera.Net", "Dia Inválido.  Utilizar formato dd/MM/yyyy", null,
        //        //new EO.Web.MsgBoxButton("OK"));
        //        return false;
        //    }
        //}
        //else
        //{
        //    if (zDia < 1 || zDia > 29)
        //    {
        //        //MsgBox1.Show("Ilitera.Net", "Dia Inválido.  Utilizar formato dd/MM/yyyy", null,
        //        //new EO.Web.MsgBoxButton("OK"));
        //        return false;
        //    }
        //}

        //return true;
        //}

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

           
            string xNome = "";
            string xMatricula = "";
            string xNIT = "";
            string xCTPS_Numero = "";
            string xCTPS_Serie = "";
            string xCTPS_UF = "";
            string xRG = "";
            string xCPF = "";
            string xSexo = "";
            string xDataNascimento = "";
            string xDataAdmissao = "";
            string xFuncao = "";
            string xCargo = "";
            string xSetor = "";
            string xCBO = "";
            string xeMail = "";
            string xeMail_Responsavel = "";
            string xDescricao_Funcao = "";

            string rSelect = "";

            string xRetorno = "";


            try
            {

                string f_Formato_Importacao = "";

                f_Formato_Importacao = lbl_Formato.Text.Trim();


                rEmpresa = new Ilitera.Common.Pessoa();

                rSelect = " dbo.udf_getnumeric( nomecodigo ) = '" + lblC_CNPJ.Text  + "' ";

                //pegar Id Empresa                    
                rEmpresa.Find(rSelect);

                //se não achar empresa,  emitir retorno avisando
                if (rEmpresa.Id == 0)
                {
                    MsgBox1.Show("Ilitera.Net", "Erro na localização do cadastro da empresa", null,
                    new EO.Web.MsgBoxButton("OK"));
                    return;                    

                }

                Entities.Usuario xUser = (Entities.Usuario)Session["usuarioLogado"];
                


                Importacao_Planilha xImportacao = new Importacao_Planilha();
                xImportacao.IdUsuario = xUser.IdUsuario;
                xImportacao.nId_Empr = rEmpresa.Id;
                xImportacao.DataImportacao = System.DateTime.Now;
                xImportacao.Save();



                txt_Status.Text = "";
                lst_Inconsistencias.Items.Clear();


                for (int zCont = 1; zCont < grvData.Rows.Count; zCont++)
                {
                    string zVal = "";

                    GridViewRow zRow;
                    zRow = grvData.Rows[zCont];

                    zVal = HttpUtility.HtmlDecode( zRow.Cells[System.Convert.ToInt16(lblC_NOME.Text)].Text.ToString().Trim().Replace("&nbsp;", "") ) ;



                    //checar se campo NOME preenchido para avaliar linha
                    if (zVal != "")
                    {
                                             
                        for (int zCel = 1; zCel < zRow.Cells.Count; zCel++)
                        {
                            // zVal = zRow.Cells[C_NOME].Text.ToString().Trim();

                            zVal = HttpUtility.HtmlDecode(zRow.Cells[zCel].Text.ToString().Trim().Replace("&nbsp;", ""));

                            if (zCel == System.Convert.ToInt16(lblC_NOME.Text))
                            {
                                xNome = zVal;
                            }
                            else if (zCel == System.Convert.ToInt16( lblC_MATRICULA.Text))
                            {
                                xMatricula = zVal;
                            }
                            else if (zCel == System.Convert.ToInt16( lblC_NIT.Text))
                            {
                                xNIT = zVal;
                            }
                            else if (zCel == System.Convert.ToInt16(lblC_CTPS.Text))
                            {
                                 xCTPS_Numero = zVal;
                            }
                            else if (zCel == System.Convert.ToInt16( lblC_SERIE.Text))
                            {
                                 xCTPS_Serie = zVal;
                            }
                            else if (zCel == System.Convert.ToInt16( lblC_UF.Text))
                            {
                                xCTPS_UF = zVal;
                            }
                            else if (zCel == System.Convert.ToInt16( lblC_RG.Text))
                            {
                                xRG = zVal;
                            }
                            else if (zCel == System.Convert.ToInt16( lblC_CPF.Text))
                            {
                                xCPF = zVal;
                            }
                            else if (zCel == System.Convert.ToInt16( lblC_SEXO.Text))
                            {
                                xSexo = zVal;
                            }
                            else if (zCel == System.Convert.ToInt16( lblC_NASCIMENTO.Text))
                            {
                                xDataNascimento = zVal;
                            }
                            else if (zCel == System.Convert.ToInt16(lblC_ADMISSAO.Text))
                            {
                                xDataAdmissao = zVal;
                            }
                            else if (zCel == System.Convert.ToInt16( lblC_FUNCAO.Text))
                            {
                                xFuncao = zVal;
                            }
                            else if (zCel == System.Convert.ToInt16(lblC_CARGO.Text))
                            {
                                xCargo = zVal;
                            }
                            else if (zCel == System.Convert.ToInt16( lblC_SETOR.Text))
                            {
                                xSetor = zVal;
                            }
                            else if (zCel == System.Convert.ToInt16(lblC_CBO.Text))
                            {
                                xCBO = zVal;
                            }

                        }


                        System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");

                        string xCPF_Completo = "";

                        if (xCPF.Length < 11)
                            xCPF_Completo = new string('0', 11 - xCPF.Length) + xCPF;
                        else
                            xCPF_Completo = xCPF;



                        Importacao_Planilha_Detalhes xDetalhes = new Importacao_Planilha_Detalhes();
                        xDetalhes.nId_Importacao = xImportacao.Id;
                        lbl_Id_Importacao.Text = xImportacao.Id.ToString();

                        DateTime result;

                        bool zLoc = false;

                        Empregado rColaborador2 = new Empregado();
                        rColaborador2.Find(" ( tNo_CPF = '" + xCPF + "' or  tNo_CPF = '" + xCPF_Completo + "' )  and nId_Empr = " + rEmpresa.Id.ToString());



                        if (rColaborador2.Id != 0)
                        {
                            //se colaborador já existe, atualizar ?
                            zLoc = true;

                            rColaborador2.tNUM_CTPS = xCTPS_Numero;
                            rColaborador2.tSER_CTPS = xCTPS_Serie;
                            rColaborador2.tUF_CTPS = xCTPS_UF;
                            rColaborador2.tNO_IDENTIDADE = xRG;
                            rColaborador2.tSEXO = xSexo;

                            rColaborador2.hDT_NASC = Retornar_Data(xDataNascimento, f_Formato_Importacao);  //System.Convert.ToDateTime(xDataNascimento, ptBr);

                            rColaborador2.teMail = xeMail;
                            rColaborador2.teMail_Resp = xeMail_Responsavel;

                            rColaborador2.hDT_ADM = Retornar_Data(xDataAdmissao, f_Formato_Importacao);

                            rColaborador2.tCOD_EMPR = xMatricula;

                            rColaborador2.Save();

                            xDetalhes.nId_Empregado = rColaborador2.Id;
                            xDetalhes.Status = "Registro atualizado";

                            

                            xDetalhes.tNUM_CTPS = xCTPS_Numero;
                            xDetalhes.tSER_CTPS = xCTPS_Serie;
                            xDetalhes.tUF_CPTS = xCTPS_UF;
                            xDetalhes.tNO_IDENTIDADE = xRG;
                            xDetalhes.tSEXO = xSexo;

                            xDetalhes.hDT_NASC = Retornar_Data(xDataNascimento, f_Formato_Importacao);  //System.Convert.ToDateTime(xDataNascimento, ptBr);

                            xDetalhes.teMail = xeMail;
                            xDetalhes.teMail_Resp = xeMail_Responsavel;

                            xDetalhes.hDT_ADM = Retornar_Data(xDataAdmissao, f_Formato_Importacao);

                            xDetalhes.tCOD_EMPR = xMatricula;

                            xDetalhes.Save();

                            lst_Processamento.Items.Add("Colaborador " + xNome + " - Registro Atualizado");

                        }

                        if ( zLoc == false)
                        {

                            Empregado rColaborador3 = new Empregado();
                            rColaborador3.Find(" ( tNo_empg = '" + xNome + "' )  and nId_Empr = " + rEmpresa.Id.ToString());

                            if (rColaborador3.Id != 0)
                            {
                                //se colaborador já existe, atualizar ?
                                zLoc = true;

                                rColaborador3.tNUM_CTPS = xCTPS_Numero;
                                rColaborador3.tSER_CTPS = xCTPS_Serie;
                                rColaborador3.tUF_CTPS = xCTPS_UF;
                                rColaborador3.tNO_IDENTIDADE = xRG;
                                rColaborador3.tSEXO = xSexo;

                                rColaborador3.hDT_NASC = Retornar_Data(xDataNascimento, f_Formato_Importacao);  //System.Convert.ToDateTime(xDataNascimento, ptBr);

                                rColaborador3.teMail = xeMail;
                                rColaborador3.teMail_Resp = xeMail_Responsavel;

                                rColaborador3.hDT_ADM = Retornar_Data(xDataAdmissao, f_Formato_Importacao);

                                rColaborador3.tCOD_EMPR = xMatricula;

                                rColaborador3.Save();

                                xDetalhes.nId_Empregado = rColaborador3.Id;
                                xDetalhes.Status = "Registro atualizado";

                                xDetalhes.tNUM_CTPS = xCTPS_Numero;
                                xDetalhes.tSER_CTPS = xCTPS_Serie;
                                xDetalhes.tUF_CPTS = xCTPS_UF;
                                xDetalhes.tNO_IDENTIDADE = xRG;
                                xDetalhes.tSEXO = xSexo;

                                xDetalhes.hDT_NASC = Retornar_Data(xDataNascimento, f_Formato_Importacao);  //System.Convert.ToDateTime(xDataNascimento, ptBr);

                                xDetalhes.teMail = xeMail;
                                xDetalhes.teMail_Resp = xeMail_Responsavel;

                                xDetalhes.hDT_ADM = Retornar_Data(xDataAdmissao, f_Formato_Importacao);

                                xDetalhes.tCOD_EMPR = xMatricula;

                                xDetalhes.Save();

                                lst_Processamento.Items.Add("Colaborador " + xNome + " - Registro Atualizado");

                            }

                        }


                        if (zLoc == false)
                        {

                            Cliente rCliente = new Cliente();

                            if (txt_Status.Text == "")
                            {

                                rCliente.Find(" IdCliente = " + rEmpresa.Id.ToString());

                                if (rCliente.Id == 0)
                                {
                                    txt_Status.Text = txt_Status.Text + "Erro: Problema na localização de Cliente " + System.Environment.NewLine;
                                    xRetorno = "03 (Problema na localização de cliente";

                                    lst_Processamento.Items.Add("Colaborador " + xNome + " - Problema na localização de Cliente");

                                }


                            }



                            if (txt_Status.Text == "")
                            {

                                //criar registro de colaborador
                                Empregado rColaborador = new Empregado();


                                rColaborador.tNO_EMPG = xNome;
                                rColaborador.tNO_CPF = xCPF_Completo;

                                if (xNIT != "")
                                    rColaborador.nNO_PIS_PASEP = System.Convert.ToInt64(xNIT);

                                rColaborador.tNUM_CTPS = xCTPS_Numero;
                                rColaborador.tSER_CTPS = xCTPS_Serie;
                                rColaborador.tUF_CTPS = xCTPS_UF;
                                rColaborador.tNO_IDENTIDADE = xRG;
                                rColaborador.tSEXO = xSexo;

                                rColaborador.hDT_NASC = Retornar_Data(xDataNascimento, f_Formato_Importacao);  //System.Convert.ToDateTime(xDataNascimento, ptBr);

                                rColaborador.teMail = xeMail;
                                rColaborador.teMail_Resp = xeMail_Responsavel;

                                rColaborador.hDT_ADM = Retornar_Data(xDataAdmissao, f_Formato_Importacao);

                                rColaborador.tCOD_EMPR = xMatricula;

                                rColaborador.nID_EMPR = rCliente;

                                RegimeRevezamento xRegimeRevezamento = new RegimeRevezamento();
                                rColaborador.nID_REGIME_REVEZAMENTO = xRegimeRevezamento;


                                rColaborador.Save();  //erro por aqui

                                if (rColaborador.Id == 0)
                                {
                                    txt_Status.Text = txt_Status.Text + "Erro: Problema na salva do colaborador ( " + xNome + "  )" + System.Environment.NewLine;
                                    xRetorno = "03 (Problema na salva do colaborador (" + xNome + ")";

                                    lst_Processamento.Items.Add("Colaborador " + xNome + " - Problema na salva do colaborador");

                                    xDetalhes.nId_Empregado = rColaborador.Id;
                                    xDetalhes.Status = xRetorno;
                                    xDetalhes.Save();

                                }
                                else
                                {
                                    //criar classif.funcional

                                    //ver se função existe
                                    Funcao rFuncao = new Funcao();
                                    rFuncao.Find(" IdCliente = " + rEmpresa.Id.ToString() + " and NomeFuncao = '" + xFuncao + "' ");

                                    if (rFuncao.Id == 0)
                                    {
                                        rFuncao = new Funcao();
                                        rFuncao.NumeroCBO = xCBO;
                                        rFuncao.NomeFuncao = xFuncao;
                                        rFuncao.IdCliente = rCliente;
                                        rFuncao.DescricaoFuncao = xDescricao_Funcao;
                                        rFuncao.Save();
                                    }

                                    if (rFuncao.Id == 0)
                                    {
                                        txt_Status.Text = txt_Status.Text + "Erro: Problema na salva da Funcao ( " + xFuncao + "  )" + System.Environment.NewLine;
                                        xRetorno = "03 (Problema na salva da Funcao (" + xFuncao + ")";

                                        lst_Processamento.Items.Add("Colaborador " + xNome + " - Problema na salva da Funcao (" + xFuncao + ")");

                                        rColaborador.Delete();

                                        xDetalhes.nId_Empregado = rColaborador.Id;
                                        xDetalhes.Status = xRetorno;
                                        xDetalhes.Save();
                                    }
                                    else
                                    {
                                        xDetalhes.nId_Empregado = rColaborador.Id;
                                        xDetalhes.Status = "Registro Inserido";
                                        xDetalhes.Save();
                                    }



                                    Cargo rCargo = new Cargo();

                                    if (txt_Status.Text == "")
                                    {

                                        //ver se cargo existe
                                        if (xCargo != "")
                                        {

                                            rCargo.Find(" nId_Empr = " + rEmpresa.Id.ToString() + " and tNo_Cargo = '" + xCargo + "' ");

                                            if (rCargo.Id == 0)
                                            {
                                                rCargo = new Cargo();
                                                rCargo.tNO_CARGO = xCargo;
                                                rCargo.nID_EMPR = rCliente;
                                                rCargo.Save();
                                            }


                                            if (rCargo.Id == 0)
                                            {
                                                txt_Status.Text = txt_Status.Text + "Erro: Problema na salva do Cargo ( " + xCargo + "  )" + System.Environment.NewLine;
                                                xRetorno = "03 (Problema na salva do Cargo (" + xCargo + ")";
                                                rColaborador.Delete();

                                                lst_Processamento.Items.Add("Colaborador " + xNome + " - Problema na salva do Cargo (" + xCargo + ")");

                                                xDetalhes.nId_Empregado = rColaborador.Id;
                                                xDetalhes.Status = xRetorno;
                                                xDetalhes.Save();
                                            }
                                            else
                                            {
                                                xDetalhes.nId_Empregado = rColaborador.Id;
                                                xDetalhes.Status = "Registro Inserido";
                                                xDetalhes.Save();
                                            }


                                        }

                                    }



                                    Setor rSetor = new Setor();

                                    if (txt_Status.Text == "")
                                    {
                                        //ver se setor existe
                                        rSetor.Find(" nId_Empr = " + rEmpresa.Id.ToString() + " and tNO_STR_EMPR = '" + xSetor + "' ");

                                        if (rSetor.Id == 0)
                                        {
                                            rSetor = new Setor();
                                            rSetor.tNO_STR_EMPR = xSetor;
                                            rSetor.nID_EMPR = rCliente;
                                            rSetor.Save();
                                        }

                                        if (rSetor.Id == 0)
                                        {
                                            txt_Status.Text = txt_Status.Text + "Erro: Problema na salva do Setor ( " + xSetor + "  )" + System.Environment.NewLine;
                                            xRetorno = "03 (Problema na salva do Setor (" + xSetor + ")";
                                            rColaborador.Delete();

                                            lst_Processamento.Items.Add("Colaborador " + xNome + " - Problema na salva do Setor (" + xSetor + ")");

                                            xDetalhes.nId_Empregado = rColaborador.Id;
                                            xDetalhes.Status = xRetorno;
                                            xDetalhes.Save();
                                        }
                                        else
                                        {
                                            xDetalhes.nId_Empregado = rColaborador.Id;
                                            xDetalhes.Status = "Registro Inserido";


                                            xDetalhes.tNO_EMPG = xNome;
                                            xDetalhes.tNO_CPF = xCPF_Completo;

                                            if (xNIT != "")
                                                xDetalhes.nNO_PIS_PASEP = System.Convert.ToInt64(xNIT);

                                            xDetalhes.tNUM_CTPS = xCTPS_Numero;
                                            xDetalhes.tSER_CTPS = xCTPS_Serie;
                                            xDetalhes.tUF_CPTS = xCTPS_UF;
                                            xDetalhes.tNO_IDENTIDADE = xRG;
                                            xDetalhes.tSEXO = xSexo;

                                            xDetalhes.hDT_NASC = Retornar_Data(xDataNascimento, f_Formato_Importacao);  //System.Convert.ToDateTime(xDataNascimento, ptBr);

                                            xDetalhes.teMail = xeMail;
                                            xDetalhes.teMail_Resp = xeMail_Responsavel;

                                            xDetalhes.hDT_ADM = Retornar_Data(xDataAdmissao, f_Formato_Importacao);

                                            xDetalhes.tCOD_EMPR = xMatricula;

                                            xDetalhes.CARGO = xCargo;
                                            xDetalhes.FUNCAO = xFuncao;
                                            xDetalhes.SETOR = xSetor;

                                            xDetalhes.Save();
                                        }



                                    }


                                    if (txt_Status.Text == "")
                                    {
                                        EmpregadoFuncao xEmprFunc = new EmpregadoFuncao();


                                        TempoExposicao xTempo = new TempoExposicao();
                                        xTempo.Find(" tHora_Extenso_Semanal = '44 horas semanais' and thora_extenso = '08h48min' ");

                                        Ghe xGhe = new Ghe();
                                        xEmprFunc.nID_GHE_AE = xGhe;

                                        ImportacaoAutomatica xImp = new ImportacaoAutomatica();
                                        xEmprFunc.nID_IMPORTACAO_AUTOMATICA = xImp;

                                        xEmprFunc.nID_TEMPO_EXP = xTempo;
                                        xEmprFunc.nID_SETOR = rSetor;
                                        xEmprFunc.nID_CARGO = rCargo;
                                        xEmprFunc.nID_FUNCAO = rFuncao;
                                        xEmprFunc.nID_EMPREGADO = rColaborador;                                        
                                        xEmprFunc.hDT_INICIO = Retornar_Data(xDataAdmissao, f_Formato_Importacao);
                                        xEmprFunc.nID_EMPR = rCliente;
                                        xEmprFunc.Save();

                                        if (xEmprFunc.Id == 0)
                                        {
                                            txt_Status.Text = txt_Status.Text + "Erro: Problema na salva da Classif.Funcional " + System.Environment.NewLine;
                                            xRetorno = "03 (Problema na salva da Classif.Funcional ";
                                            rColaborador.Delete();

                                            lst_Processamento.Items.Add("Colaborador " + xNome + " - Problema na salva da Classificação Funcional");

                                            xDetalhes.nId_Empregado = rColaborador.Id;
                                            xDetalhes.Status = xRetorno;
                                            xDetalhes.Save();
                                        }
                                        else
                                        {
                                            xDetalhes.nId_Empregado = rColaborador.Id;
                                            xDetalhes.Status = "Registro Inserido";
                                            xDetalhes.Save();
                                        }


                                    }

                                }



                            }

                        }


                        if (txt_Status.Text != "")
                        {

                            txt_Status.Text = txt_Status.Text + " (" + xNome + " )" + System.Environment.NewLine;
                            xRetorno = xRetorno + " (" + xNome + " )" + System.Environment.NewLine;

                        }
                        else
                        {

                            //para cada registro enviar o retorno
                            xRetorno = "01 Processamento concluído sem erros";
                        }


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

            lbl_Arq.Text = "";

        }

        protected void cmd_Exibir_Log_Click(object sender, EventArgs e)
        {


            //gerar csv com status de cada registro
            string xFile = "Relat_Importacao_" + System.DateTime.Now.Second.ToString().Trim() + System.DateTime.Now.Millisecond.ToString().Trim() + System.DateTime.Now.Minute.ToString().Trim() + System.DateTime.Now.Day.ToString().Trim() + System.DateTime.Now.Month.ToString().Trim() + ".csv";
            string myStringWebResource = "I:\\temp\\" + xFile;
            string zLinha = "";

            Ilitera.Data.Clientes_Funcionarios xRep = new Clientes_Funcionarios();

            DataSet zDs = xRep.Trazer_Dados_Importacao(System.Convert.ToInt32(lbl_Id_Importacao.Text));

            TextWriter tw = new StreamWriter(myStringWebResource, false, Encoding.GetEncoding(1252));

            zLinha = "Colaborador;NomeUsuario;DataImportacao;Cliente;Status;Imp_Nome;Imp_CPF;Imp_Num_Ctps;Imp_Ser_ctps;Imp_UF_ctps;Imp_identidade;Imp_sexo;Imp_dt_Nasc;Imp_dt_adm;Imp_email;Imp_eMail_Resp;Imp_Matricula;Imp_PIS_PASEP;Imp_setor;Imp_cargo;Imp_funcao";
            tw.WriteLine(zLinha);



            for (int bCont = 0; bCont < zDs.Tables[0].Rows.Count; bCont++)
            {
                zLinha = "";

                for (int bAux = 0; bAux < zDs.Tables[0].Columns.Count; bAux++)
                {
                    zLinha = zLinha + zDs.Tables[0].Rows[bCont][bAux].ToString().Trim() + ";";
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
            string dateString = zVal.Replace("-","/");
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

            if ( zFormato == "MM/dd/yyyy")
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
                            return new DateTime(2000,1,1);
                        }

                    }

                }

            }

        }




    }
}
