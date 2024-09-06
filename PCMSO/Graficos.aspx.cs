using System;
using System.Data;
using System.Text;
using Ilitera.Opsa.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using System.Collections;
using System.Collections.Generic;
using System.IO;

using Excel = Microsoft.Office.Interop.Excel;
using FusionCharts.Charts;

using Entities;
using BLL;


namespace Ilitera.Net.PCMSO
{
    public partial class Graficos : System.Web.UI.Page
    {
        //private string xTipo;
        private Int32 xIdPai;


        protected void Page_Load(object sender, System.EventArgs e)
        {
            InicializaWebPageObjects();
            //PreencheLabels("lblEmp", cliente.NomeAbreviado.ToString());

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


                if (rd_Considerar.SelectedIndex != 0)
                {
                    txt_Data.Enabled = false;
                    txt_Data2.Enabled = false;
                    cmb_Empresa.Enabled = false;
                }
                else
                {
                    txt_Data.Enabled = true;
                    txt_Data2.Enabled = true;
                    cmb_Empresa.Enabled = true;
                }

                cmb_Anamnese_Dados.SelectedIndex = 1;

                cmb_Anamneses.SelectedIndex = 2;

                rd_Considerar.Items[0].Enabled = false;

                Cliente zCliente = new Cliente();
                zCliente.Find(System.Convert.ToInt32(Session["Empresa"].ToString()));

                

                if ( zCliente.Id != 0)
                {
                    if ( zCliente.Liberar_Grafico != null)
                    {
                        if ( zCliente.Liberar_Grafico == true)
                        {
                            rd_Considerar.Items[0].Enabled = true;
                        }
                    }
                }


                //string xTipo;

                //if (Request["item"] == null)
                //{
                //    xTipo = "0";
                //    xIdPai = 0;
                //}
                //else
                //{
                //    xTipo = Request["item"];
                //    if (xTipo != "laudo")
                //        xIdPai = System.Convert.ToInt32(Request["idjuridicapai"].ToString());
                //    else
                //        xIdPai = 0;
                //}



                //if (xTipo == "laudo")
                //    rGrafico_Laudos();
                //else if (xTipo != "0")
                //    SubGrafico_Laudos(xTipo, xIdPai);

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

        }
        #endregion

        protected void InicializaWebPageObjects()
        {
            //base.InicializaWebPageObjects();
            //StringBuilder st = new StringBuilder();

            //st.Append("top.window.document.frames.conteudo.window.document.getElementById('txtIdEmpregado').value = '" + empregado.Id + "';");
            //st.Append("top.window.document.getElementById('txtIdEmpregado').value = '" + Request["IdEmpregado"] + "';");
            //st.Append("top.window.document.getElementById('txtIdUsuario').value = '" + Request["IdUsuario"] + "';");
            //st.Append("top.window.document.getElementById('txtIdEmpresa').value = '" + Request["IdEmpresa"] + "';");

            //this.ClientScript.RegisterStartupScript(this.GetType(), "IdEmpregado", st.ToString(), true);
            //btnFichaCompleta.Attributes.Add("onClick", "addItem(centerWin('../DadosEmpresa/FichaCompleta.aspx?IdEmpregado=" + Request["IdEmpregado"] + "&IdEmpresa=" + Request["IdEmpresa"] + "&IdUsuario=" + Request["IdUsuario"] + "',560,320,\'FichaCompleta\'),\'Todos\'); Reload();");

        }




        protected void btnemp_Click(object sender, EventArgs e)
        {

            if (rd_Considerar.SelectedValue != "1")
            {
                if (Validar_Data(txt_Data.Text.Trim()) == false)
                {
                    return;
                }

                if (Validar_Data(txt_Data2.Text.Trim()) == false)
                {
                    return;
                }
            }


            Guid strAux = Guid.NewGuid();

            //string xTipoRel = "A";
            //string xConsiderar = "0";
            //string xStatus = "1";


            //xTipoRel = "A";

            if (rd_Considerar.SelectedValue == "0")
                rGrafico_Absenteismo();
            //Create_Excel_Graph();
            else if (rd_Considerar.SelectedValue == "1")
                rGrafico_Laudos();
            else if (rd_Considerar.SelectedValue == "3")
            {
                if (cmb_Anamneses.SelectedValue == "0")
                {
                    rGrafico_Anamnese_Antecedentes();
                }
                else if (cmb_Anamneses.SelectedValue == "1")
                {
                    rGrafico_Anamnese_Alteracoes();
                }
                else if (cmb_Anamneses.SelectedValue == "2")
                {
                    rGrafico_Anamnese_Anamnese1();
                }
                else if (cmb_Anamneses.SelectedValue == "3")
                {
                    rGrafico_Anamnese_Anamnese2();
                }
                else if (cmb_Anamneses.SelectedValue == "4")
                {
                    rGrafico_Anamnese_Anamnese3();
                }

            }


            return;

        }






        protected void rd_Analitico_CheckedChanged(object sender, EventArgs e)
        {

            //if (rd_Analitico.Checked == true) cmb_Empresa.Enabled = false;
            //else cmb_Empresa.Enabled = true;

        }

        protected void rd_Sumarizado_CheckedChanged(object sender, EventArgs e)
        {

            //if (rd_Analitico.Checked == true) cmb_Empresa.Enabled = false;
            //else cmb_Empresa.Enabled = true;

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



        private void rGrafico_Absenteismo()
        {

            StringBuilder xmlData = new StringBuilder();
            short xEmp = 1;

            xEmp = System.Convert.ToInt16(cmb_Empresa.SelectedValue.ToString().Trim());


            Ilitera.Data.Clientes_Funcionarios xDados = new Ilitera.Data.Clientes_Funcionarios();
            DataSet rDs = xDados.Gerar_DS_Relatorio_Absenteismo_Grafico(System.Convert.ToInt32(Session["Empresa"].ToString()), txt_Data.Text, txt_Data2.Text, xEmp);


            //Int32 zMaxValue = 0; 

            //for (int zCont = 0; zCont < rDs.Tables[0].Rows.Count; zCont++)
            //{
            //    if (System.Convert.ToInt32(rDs.Tables[0].Rows[zCont][4].ToString()) > zMaxValue) zMaxValue = System.Convert.ToInt32(rDs.Tables[0].Rows[zCont][4].ToString());
            //}



            // http://www.fusioncharts.com/dev/basic-chart-configurations/vertical-div-lines.html

            //xmlData.Append("<chart caption='Absenteeism 2016 (Days)' subCaption='TOTAL 11.185,9 days     AVG 932,2 days' showBorder='1' formatNumberScale='0' rotatelabels='1' showvalues='0'>");

            xmlData.Append("<chart caption='COLABORADORES X CID' subCaption='Período: " + txt_Data.Text + " a " + txt_Data2.Text + "' showBorder='1' formatNumberScale='0' exportEnabled='1'  ");

            //fonte
            xmlData.Append("  baseFont='Arial' baseFontSize='11' baseFontColor='#000000'  outcnvbasefontsize='11' outcnvBaseFonte='Tamoha' ");

            //margens
            xmlData.Append(" chartLeftMargin='10' chartTopMargin='20'  chartRightMargin='10'  chartBottomMargin='20' ");

            //background color
            //xmlData.Append(" bgColor='#fdf5e6'  canvasBgAlpha='0' bgAlpha='50'  bgratio='60, 40' ");

            //graph color
            xmlData.Append("  palettecolors='#008ee4'  useplotgradientcolor='0'  showplotborder='0'  showShadow='0'  palette='4' ");

            //divisão vertical
            //xmlData.Append(" numVDivLines='10' vDivLineColor='#99ccff' vDivLineThickness='1' vDivLineAlpha='70' vDivLineDashed='1' vDivLineDashLen='5' vDivLineDashGap='3' "); // yAxisMaxValue='" + (zMaxValue+2).ToString() + "' ");

            //logo
            xmlData.Append(" rotatelabels ='1' showvalues='1' >"); // logoURL='https://www.br.capgemini.com/sites/all/themes/capgemini/logo.png' logoAlpha='40' logoScale='90' logoPosition='TR' > ");


            string zColor1 = "#31978D";
            string zColor2 = "#E98442";
            string zColor3 = "#b0c4de";


            for (int zCont = 0; zCont < rDs.Tables[0].Rows.Count; zCont++)
            {
                if (zCont % 3 == 0)
                    xmlData.AppendFormat("<set label='" + rDs.Tables[0].Rows[zCont][5].ToString() + "' Value='" + System.Convert.ToInt32(rDs.Tables[0].Rows[zCont][4].ToString()) + "' color ='" + zColor1 + "' />");
                else if (zCont % 3 == 1)
                    xmlData.AppendFormat("<set label='" + rDs.Tables[0].Rows[zCont][5].ToString() + "' Value='" + System.Convert.ToInt32(rDs.Tables[0].Rows[zCont][4].ToString()) + "' color ='" + zColor2 + "' />");
                else if (zCont % 3 == 2)
                    xmlData.AppendFormat("<set label='" + rDs.Tables[0].Rows[zCont][5].ToString() + "' Value='" + System.Convert.ToInt32(rDs.Tables[0].Rows[zCont][4].ToString()) + "' color ='" + zColor3 + "' />");
            }


            //xmlData.AppendFormat("<categories>");


            //for ( int zCont=0; zCont<rDs.Tables[0].Rows.Count;zCont++)
            //{
            //   xmlData.AppendFormat("<category label='{0}'/>", rDs.Tables[0].Rows[zCont][5].ToString());
            //}






            //xmlData.AppendFormat("</categories>");


            //xmlData.AppendFormat("<dataset seriesName='{0}'>", "Número de Colaboradores");

            ////valores

            //for (int zCont = 0; zCont < rDs.Tables[0].Rows.Count; zCont++)
            //{
            //    xmlData.AppendFormat("<set value='{0}'/>", System.Convert.ToInt32( rDs.Tables[0].Rows[zCont][4].ToString() ));
            //}





            //linha de média
            //xmlData.AppendFormat(" <trendlines>< isTrendZone='1' startvalue='900' endValue='950' color='#1aaf5d' valueonright='1' tooltext='AVG 932 days'  /></trendlines> ");


            xmlData.AppendFormat("</chart>");





            // Initialize the chart.
            Chart factoryOutput = new Chart("column2d", "Absenteismo", "1190", "600", "xml", xmlData.ToString());





            // Render the chart.
            Literal1.Text = factoryOutput.Render();


        }



        private void Create_Excel_Graph()
        {
            Excel.Application xlApp;
            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;
            object misValue = System.Reflection.Missing.Value;

            xlApp = new Excel.Application();
            xlWorkBook = xlApp.Workbooks.Add(misValue);
            xlWorkSheet = (Excel.Worksheet)xlWorkBook
               .Worksheets.get_Item(1);

            // Add data columns
            xlWorkSheet.Cells[1, 1] = "SL";
            xlWorkSheet.Cells[1, 2] = "Name";
            xlWorkSheet.Cells[1, 3] = "CTC";
            xlWorkSheet.Cells[1, 4] = "DA";
            xlWorkSheet.Cells[1, 5] = "HRA";
            xlWorkSheet.Cells[1, 6] = "Conveyance";
            xlWorkSheet.Cells[1, 7] = "Medical Expenses";
            xlWorkSheet.Cells[1, 8] = "Special";
            xlWorkSheet.Cells[1, 9] = "Bonus";
            xlWorkSheet.Cells[1, 10] = "TA";
            xlWorkSheet.Cells[1, 11] = "TOTAL";
            xlWorkSheet.Cells[1, 11] = "Contribution to PF";
            xlWorkSheet.Cells[1, 12] = "Profession Tax";
            xlWorkSheet.Cells[1, 13] = "TDS";
            xlWorkSheet.Cells[1, 14] = "Salary Advance";
            xlWorkSheet.Cells[1, 15] = "TOTAL";
            xlWorkSheet.Cells[1, 16] = "NET PAY";


            Excel.Application xlApp1 = new Excel.Application();
            Excel.Workbook xlWorkbook = xlApp1.Workbooks.Open
               (@"I:\Temp\Graph.xlsx");
            Excel._Worksheet xlWorksheet = (Excel._Worksheet)xlWorkbook.Sheets[1];
            Excel.Range xlRange = xlWorksheet.UsedRange;

            int rowCount = xlRange.Rows.Count;
            int colCount = xlRange.Columns.Count;

            for (int i = 1; i <= rowCount; i++)
            {
                for (int j = 1; j <= colCount; j++)
                {
                    //Console.WriteLine(xlRange.Cells[i, j].Value2.ToString());
                    xlWorkSheet.Cells[i, j] = xlRange.Cells[i, j].ToString();

                }
            }

            Console.ReadLine();

            Excel.Range chartRange;

            Excel.ChartObjects xlCharts = (Excel.ChartObjects)
               xlWorkSheet.ChartObjects(Type.Missing);
            Excel.ChartObject myChart = (Excel.ChartObject)
               xlCharts.Add(10, 80, 300, 250);
            Excel.Chart chartPage = myChart.Chart;

            chartRange = xlWorkSheet.get_Range("A1", "R22");
            chartPage.SetSourceData(chartRange, misValue);
            chartPage.ChartType = Excel.XlChartType.xlColumnClustered;

            // Export chart as picture file
            chartPage.Export(@"I:\Temp\EmployeeExportData.bmp",
               "BMP", misValue);

            xlWorkBook.SaveAs("EmployeeExportData.xls",
               Excel.XlFileFormat.xlWorkbookNormal, misValue,
               misValue, misValue, misValue,
               Excel.XlSaveAsAccessMode.xlExclusive, misValue,
               misValue, misValue, misValue, misValue);
            xlWorkBook.Close(true, misValue, misValue);
            xlApp.Quit();

            DeallocateObject(xlWorkSheet);
            DeallocateObject(xlWorkBook);
            DeallocateObject(xlApp);
            DeallocateObject(xlApp1);
        }



        private static void DeallocateObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal
                   .ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;

            }
            finally
            {
                GC.Collect();
            }
        }


        private void rGrafico_Anamnese_Antecedentes()
        {
            StringBuilder xmlData = new StringBuilder();


            Int16 xEmp = 1;

            xEmp = System.Convert.ToInt16( cmb_Empresa.SelectedValue.ToString() );

            string xPorcentagem = "0";

            xPorcentagem = cmb_Anamnese_Dados.SelectedValue.ToString();



            xmlData.Append("<chart caption='Antecedentes Familiares' showBorder='1' formatNumberScale='0' exportEnabled='1'  showPercentValues='" + xPorcentagem + "' showpercentintooltip='1' theme='gammel' usedataplotcolorforlabels='1' ");

            //graph color
            xmlData.Append("  palettecolors='#008ee4'  useplotgradientcolor='0'  showplotborder='0'  showShadow='0'  palette='2' showlegend='1' ");

            //background color
            xmlData.Append(" bgColor='#F1EBE6'  canvasBgAlpha='0' bgAlpha='50'  bgratio='60, 40' ");

            //fonte
            xmlData.Append("  legendItemFont='Tahoma' legendItemFontSize='9'  legendItemFontBold='1' legendIconScale='2' ");
            xmlData.Append("  baseFont='Tahoma' baseFontSize='9' baseFontColor='#000000'  outcnvbasefontsize='9' outcnvBaseFonte='Tamoha' ");
            xmlData.Append("  labelFont='Tahoma' labelFontSize='9' labelFontColor='#000000' labelFontBold='1'  ");
           

            //margens
            xmlData.Append(" chartLeftMargin='3' chartTopMargin='3'  chartRightMargin='3'  chartBottomMargin='3' ");

            xmlData.Append(" rotatelabels ='1' showvalues='1'  ");

            StringBuilder xmlData2 = new StringBuilder( xmlData.ToString());
            StringBuilder xmlData3 = new StringBuilder(xmlData.ToString());
            StringBuilder xmlData4 = new StringBuilder(xmlData.ToString());
            StringBuilder xmlData5 = new StringBuilder(xmlData.ToString());
            StringBuilder xmlData6 = new StringBuilder(xmlData.ToString());
            StringBuilder xmlData7 = new StringBuilder(xmlData.ToString());
            StringBuilder xmlData8 = new StringBuilder(xmlData.ToString());



            // -520402605

            // https://www.fusioncharts.com/dev/chart-attributes/pie2d

            //colocar dados em cada 1 dos 8 gráficos de antecedentes

            Ilitera.Data.Empregado_Cadastral xGraf = new Ilitera.Data.Empregado_Cadastral();
            DataSet zDs = xGraf.Grafico_Anamnese("Has_AF_Hipertensao", Session["Empresa"].ToString(), txt_Data.Text, txt_Data2.Text, xEmp);

            xmlData.Append(" subCaption='Hipertensão Arterial' > ");

            if (zDs.Tables[0].Rows.Count > 0)
            {
                for (int zCont = 0; zCont < zDs.Tables[0].Rows.Count; zCont++)
                {
                    if (zDs.Tables[0].Rows[zCont]["Indicador"].ToString().Trim() == "1")
                    {
                        xmlData.Append(" <set label = 'Não' value = '" + zDs.Tables[0].Rows[zCont]["Valor"].ToString().Trim() + "'  color='#E98442' labelFont='Tahoma' labelFontSize='9' /> ");
                    }  
                    else if (zDs.Tables[0].Rows[zCont]["Indicador"].ToString().Trim() == "2")
                    {
                        xmlData.Append(" <set label = 'Sim' value = '" + zDs.Tables[0].Rows[zCont]["Valor"].ToString().Trim() + "' color='#31978D'  labelFont='Tahoma' labelFontSize='9' /> ");
                    }  
                }
            }
            else
            {
                xmlData.Append(" <set label = 'Não' value = '0'  color='#E98442' labelFont='Tahoma' labelFontSize='9' /> ");
            }

            
            xmlData.AppendFormat("</chart>");



            zDs = xGraf.Grafico_Anamnese("Has_AF_Diabetes", Session["Empresa"].ToString(), txt_Data.Text, txt_Data2.Text, xEmp);

            xmlData2.Append(" subCaption='Diabetes' > ");

            if (zDs.Tables[0].Rows.Count > 0)
            {
                for (int zCont = 0; zCont < zDs.Tables[0].Rows.Count; zCont++)
                {
                    if (zDs.Tables[0].Rows[zCont]["Indicador"].ToString().Trim() == "1")
                    {
                        xmlData2.Append(" <set label = 'Não' value = '" + zDs.Tables[0].Rows[zCont]["Valor"].ToString().Trim() + "'  color='#E98442' labelFont='Tahoma' labelFontSize='9' /> ");
                    }
                    else if (zDs.Tables[0].Rows[zCont]["Indicador"].ToString().Trim() == "2")
                    {
                        xmlData2.Append(" <set label = 'Sim' value = '" + zDs.Tables[0].Rows[zCont]["Valor"].ToString().Trim() + "' color='#31978D'  labelFont='Tahoma' labelFontSize='9' /> ");
                    }
                }
            }
            else
            {
                xmlData2.Append(" <set label = 'Não' value = '0'  color='#E98442' labelFont='Tahoma' labelFontSize='9' /> ");
            }


            
            xmlData2.AppendFormat("</chart>");



            zDs = xGraf.Grafico_Anamnese("Has_AF_Coracao", Session["Empresa"].ToString(), txt_Data.Text, txt_Data2.Text, xEmp);

            xmlData3.Append(" subCaption='Doenças do Coração' > ");

            if (zDs.Tables[0].Rows.Count > 0)
            {
                for (int zCont = 0; zCont < zDs.Tables[0].Rows.Count; zCont++)
                {
                    if (zDs.Tables[0].Rows[zCont]["Indicador"].ToString().Trim() == "1")
                    {
                        xmlData3.Append(" <set label = 'Não' value = '" + zDs.Tables[0].Rows[zCont]["Valor"].ToString().Trim() + "'  color='#E98442' labelFont='Tahoma' labelFontSize='9' /> ");
                    }
                    else if (zDs.Tables[0].Rows[zCont]["Indicador"].ToString().Trim() == "2")
                    {
                        xmlData3.Append(" <set label = 'Sim' value = '" + zDs.Tables[0].Rows[zCont]["Valor"].ToString().Trim() + "' color='#31978D'  labelFont='Tahoma' labelFontSize='9' /> ");
                    }
                }
            }
            else
            {
                xmlData3.Append(" <set label = 'Não' value = '0'  color='#E98442' labelFont='Tahoma' labelFontSize='9' /> ");
            }


            
            xmlData3.AppendFormat("</chart>");



            zDs = xGraf.Grafico_Anamnese("Has_AF_Derrames", Session["Empresa"].ToString(), txt_Data.Text, txt_Data2.Text, xEmp);

            xmlData4.Append(" subCaption='Derrames Cerebrais' > ");

            if (zDs.Tables[0].Rows.Count > 0)
            {
                for (int zCont = 0; zCont < zDs.Tables[0].Rows.Count; zCont++)
                {
                    if (zDs.Tables[0].Rows[zCont]["Indicador"].ToString().Trim() == "1")
                    {
                        xmlData4.Append(" <set label = 'Não' value = '" + zDs.Tables[0].Rows[zCont]["Valor"].ToString().Trim() + "'  color='#E98442' labelFont='Tahoma' labelFontSize='9' /> ");
                    }
                    else if (zDs.Tables[0].Rows[zCont]["Indicador"].ToString().Trim() == "2")
                    {
                        xmlData4.Append(" <set label = 'Sim' value = '" + zDs.Tables[0].Rows[zCont]["Valor"].ToString().Trim() + "' color='#31978D'  labelFont='Tahoma' labelFontSize='9' /> ");
                    }
                }
            }
            else
            {
                xmlData4.Append(" <set label = 'Não' value = '0'  color='#E98442' labelFont='Tahoma' labelFontSize='9' /> ");
            }


            
            xmlData4.AppendFormat("</chart>");




            zDs = xGraf.Grafico_Anamnese("Has_AF_Obesidade", Session["Empresa"].ToString(), txt_Data.Text, txt_Data2.Text, xEmp);

            xmlData5.Append(" subCaption='Obsesidade' > ");

            if (zDs.Tables[0].Rows.Count > 0)
            {
                for (int zCont = 0; zCont < zDs.Tables[0].Rows.Count; zCont++)
                {
                    if (zDs.Tables[0].Rows[zCont]["Indicador"].ToString().Trim() == "1")
                    {
                        xmlData5.Append(" <set label = 'Não' value = '" + zDs.Tables[0].Rows[zCont]["Valor"].ToString().Trim() + "'  color='#E98442' labelFont='Tahoma' labelFontSize='9' /> ");
                    }
                    else if (zDs.Tables[0].Rows[zCont]["Indicador"].ToString().Trim() == "2")
                    {
                        xmlData5.Append(" <set label = 'Sim' value = '" + zDs.Tables[0].Rows[zCont]["Valor"].ToString().Trim() + "' color='#31978D'  labelFont='Tahoma' labelFontSize='9' /> ");
                    }
                }
            }
            else
            {
                xmlData5.Append(" <set label = 'Não' value = '0'  color='#E98442' labelFont='Tahoma' labelFontSize='9' /> ");
            }


            
            xmlData5.AppendFormat("</chart>");




            zDs = xGraf.Grafico_Anamnese("Has_AF_Cancer", Session["Empresa"].ToString(), txt_Data.Text, txt_Data2.Text, xEmp);

            xmlData6.Append(" subCaption='Câncer' > ");

            if (zDs.Tables[0].Rows.Count > 0)
            {
                for (int zCont = 0; zCont < zDs.Tables[0].Rows.Count; zCont++)
                {
                    if (zDs.Tables[0].Rows[zCont]["Indicador"].ToString().Trim() == "1")
                    {
                        xmlData6.Append(" <set label = 'Não' value = '" + zDs.Tables[0].Rows[zCont]["Valor"].ToString().Trim() + "'  color='#E98442' labelFont='Tahoma' labelFontSize='9' /> ");
                    }
                    else if (zDs.Tables[0].Rows[zCont]["Indicador"].ToString().Trim() == "2")
                    {
                        xmlData6.Append(" <set label = 'Sim' value = '" + zDs.Tables[0].Rows[zCont]["Valor"].ToString().Trim() + "' color='#31978D'  labelFont='Tahoma' labelFontSize='9' /> ");
                    }
                }
            }
            else
            {
                xmlData6.Append(" <set label = 'Não' value = '0'  color='#E98442' labelFont='Tahoma' labelFontSize='9' /> ");
            }


            
            xmlData6.AppendFormat("</chart>");



            zDs = xGraf.Grafico_Anamnese("Has_AF_Colesterol", Session["Empresa"].ToString(), txt_Data.Text, txt_Data2.Text, xEmp);

            xmlData7.Append(" subCaption='Colesterol' > ");

            if (zDs.Tables[0].Rows.Count > 0)
            {
                for (int zCont = 0; zCont < zDs.Tables[0].Rows.Count; zCont++)
                {
                    if (zDs.Tables[0].Rows[zCont]["Indicador"].ToString().Trim() == "1")
                    {
                        xmlData7.Append(" <set label = 'Não' value = '" + zDs.Tables[0].Rows[zCont]["Valor"].ToString().Trim() + "'  color='#E98442' labelFont='Tahoma' labelFontSize='9' /> ");
                    }
                    else if (zDs.Tables[0].Rows[zCont]["Indicador"].ToString().Trim() == "2")
                    {
                        xmlData7.Append(" <set label = 'Sim' value = '" + zDs.Tables[0].Rows[zCont]["Valor"].ToString().Trim() + "' color='#31978D'  labelFont='Tahoma' labelFontSize='9' /> ");
                    }
                }
            }
            else
            {
                xmlData7.Append(" <set label = 'Não' value = '0'  color='#E98442' labelFont='Tahoma' labelFontSize='9' /> ");
            }


            
            xmlData7.AppendFormat("</chart>");




            zDs = xGraf.Grafico_Anamnese("Has_AF_Psiquiatricos", Session["Empresa"].ToString(), txt_Data.Text, txt_Data2.Text, xEmp);

            xmlData8.Append(" subCaption='Tratamento Psiquiatrico' > ");

            if (zDs.Tables[0].Rows.Count > 0)
            {
                for (int zCont = 0; zCont < zDs.Tables[0].Rows.Count; zCont++)
                {
                    if (zDs.Tables[0].Rows[zCont]["Indicador"].ToString().Trim() == "1")
                    {
                        xmlData8.Append(" <set label = 'Não' value = '" + zDs.Tables[0].Rows[zCont]["Valor"].ToString().Trim() + "'  color='#E98442' labelFont='Tahoma' labelFontSize='9' /> ");
                    }
                    else if (zDs.Tables[0].Rows[zCont]["Indicador"].ToString().Trim() == "2")
                    {
                        xmlData8.Append(" <set label = 'Sim' value = '" + zDs.Tables[0].Rows[zCont]["Valor"].ToString().Trim() + "' color='#31978D'  labelFont='Tahoma' labelFontSize='9' /> ");
                    }
                }
            }
            else
            {
                xmlData8.Append(" <set label = 'Não' value = '0'  color='#E98442' labelFont='Tahoma' labelFontSize='9' /> ");
            }


            
            xmlData8.AppendFormat("</chart>");



            Chart factoryOutput = new Chart("pie2d", "Anamnese1", "390", "390", "xml", xmlData.ToString());
            Chart factoryOutput2 = new Chart("pie2d", "Anamnese2", "390", "390", "xml", xmlData2.ToString());
            Chart factoryOutput3 = new Chart("pie2d", "Anamnese3", "390", "390", "xml", xmlData3.ToString());
            Chart factoryOutput4 = new Chart("pie2d", "Anamnese4", "390", "390", "xml", xmlData4.ToString());
            Chart factoryOutput5 = new Chart("pie2d", "Anamnese5", "390", "390", "xml", xmlData5.ToString());
            Chart factoryOutput6 = new Chart("pie2d", "Anamnese6", "390", "390", "xml", xmlData6.ToString());
            Chart factoryOutput7 = new Chart("pie2d", "Anamnese7", "390", "390", "xml", xmlData7.ToString());
            Chart factoryOutput8 = new Chart("pie2d", "Anamnese8", "390", "390", "xml", xmlData8.ToString());
            


            
            // Render the chart.
            Literal1.Text = factoryOutput.Render();

            Literal2.Text = factoryOutput2.Render();

            Literal3.Text = factoryOutput3.Render();

            Literal4.Text = factoryOutput4.Render();

            Literal5.Text = factoryOutput5.Render();

            Literal6.Text = factoryOutput6.Render();

            Literal7.Text = factoryOutput7.Render();

            Literal8.Text = factoryOutput8.Render();

            Literal9.Text = "";

        }




        private void rGrafico_Anamnese_Anamnese1()
        {
            StringBuilder xmlData = new StringBuilder();


            Int16 xEmp = 1;

            xEmp = System.Convert.ToInt16(cmb_Empresa.SelectedValue.ToString());

            string xPorcentagem = "0";

            xPorcentagem = cmb_Anamnese_Dados.SelectedValue.ToString();



            xmlData.Append("<chart caption='Anamnese - 1' showBorder='1' formatNumberScale='0' exportEnabled='1'  showPercentValues='" + xPorcentagem + "' showpercentintooltip='1' theme='gammel' usedataplotcolorforlabels='1' ");

            //graph color
            xmlData.Append("  palettecolors='#008ee4'  useplotgradientcolor='0'  showplotborder='0'  showShadow='0'  palette='2' showlegend='1' ");

            //background color
            xmlData.Append(" bgColor='#F1EBE6'  canvasBgAlpha='0' bgAlpha='50'  bgratio='60, 40' ");

            //fonte
            xmlData.Append("  legendItemFont='Tahoma' legendItemFontSize='9'  legendItemFontBold='1' legendIconScale='2' ");
            xmlData.Append("  baseFont='Tahoma' baseFontSize='9' baseFontColor='#000000'  outcnvbasefontsize='9' outcnvBaseFonte='Tamoha' ");
            xmlData.Append("  labelFont='Tahoma' labelFontSize='9' labelFontColor='#000000' labelFontBold='1'  ");


            //margens
            xmlData.Append(" chartLeftMargin='3' chartTopMargin='3'  chartRightMargin='3'  chartBottomMargin='3' ");

            xmlData.Append(" rotatelabels ='1' showvalues='1'  ");

            StringBuilder xmlData2 = new StringBuilder(xmlData.ToString());
            StringBuilder xmlData3 = new StringBuilder(xmlData.ToString());
            StringBuilder xmlData4 = new StringBuilder(xmlData.ToString());
            StringBuilder xmlData5 = new StringBuilder(xmlData.ToString());
            StringBuilder xmlData6 = new StringBuilder(xmlData.ToString());
            StringBuilder xmlData7 = new StringBuilder(xmlData.ToString());
            StringBuilder xmlData8 = new StringBuilder(xmlData.ToString());



            // -520402605

            // https://www.fusioncharts.com/dev/chart-attributes/pie2d

            //colocar dados em cada 1 dos 8 gráficos de antecedentes

            Ilitera.Data.Empregado_Cadastral xGraf = new Ilitera.Data.Empregado_Cadastral();
            DataSet zDs = xGraf.Grafico_Anamnese("HasAlcoolismo", Session["Empresa"].ToString(), txt_Data.Text, txt_Data2.Text, xEmp);

            xmlData.Append(" subCaption='Consumo de Bebidas Alcoólicas' > ");

            if (zDs.Tables[0].Rows.Count > 0)
            {
                for (int zCont = 0; zCont < zDs.Tables[0].Rows.Count; zCont++)
                {
                    if (zDs.Tables[0].Rows[zCont]["Indicador"].ToString().Trim() == "1")
                    {
                        xmlData.Append(" <set label = 'Não' value = '" + zDs.Tables[0].Rows[zCont]["Valor"].ToString().Trim() + "'  color='#E98442' labelFont='Tahoma' labelFontSize='9' /> ");
                    }
                    else if (zDs.Tables[0].Rows[zCont]["Indicador"].ToString().Trim() == "2")
                    {
                        xmlData.Append(" <set label = 'Sim' value = '" + zDs.Tables[0].Rows[zCont]["Valor"].ToString().Trim() + "' color='#31978D'  labelFont='Tahoma' labelFontSize='9' /> ");
                    }
                }
            }
            else
            {
                xmlData.Append(" <set label = 'Não' value = '0'  color='#E98442' labelFont='Tahoma' labelFontSize='9' /> ");
            }


            xmlData.AppendFormat("</chart>");



            zDs = xGraf.Grafico_Anamnese("HasDoencaCronica", Session["Empresa"].ToString(), txt_Data.Text, txt_Data2.Text, xEmp);

            xmlData2.Append(" subCaption='Alguma Doença não Mencionada' > ");

            if (zDs.Tables[0].Rows.Count > 0)
            {
                for (int zCont = 0; zCont < zDs.Tables[0].Rows.Count; zCont++)
                {
                    if (zDs.Tables[0].Rows[zCont]["Indicador"].ToString().Trim() == "1")
                    {
                        xmlData2.Append(" <set label = 'Não' value = '" + zDs.Tables[0].Rows[zCont]["Valor"].ToString().Trim() + "'  color='#E98442' labelFont='Tahoma' labelFontSize='9' /> ");
                    }
                    else if (zDs.Tables[0].Rows[zCont]["Indicador"].ToString().Trim() == "2")
                    {
                        xmlData2.Append(" <set label = 'Sim' value = '" + zDs.Tables[0].Rows[zCont]["Valor"].ToString().Trim() + "' color='#31978D'  labelFont='Tahoma' labelFontSize='9' /> ");
                    }
                }
            }
            else
            {
                xmlData2.Append(" <set label = 'Não' value = '0'  color='#E98442' labelFont='Tahoma' labelFontSize='9' /> ");
            }



            xmlData2.AppendFormat("</chart>");



            zDs = xGraf.Grafico_Anamnese("HasAlergia", Session["Empresa"].ToString(), txt_Data.Text, txt_Data2.Text, xEmp);

            xmlData3.Append(" subCaption='Possui Alergia' > ");

            if (zDs.Tables[0].Rows.Count > 0)
            {
                for (int zCont = 0; zCont < zDs.Tables[0].Rows.Count; zCont++)
                {
                    if (zDs.Tables[0].Rows[zCont]["Indicador"].ToString().Trim() == "1")
                    {
                        xmlData3.Append(" <set label = 'Não' value = '" + zDs.Tables[0].Rows[zCont]["Valor"].ToString().Trim() + "'  color='#E98442' labelFont='Tahoma' labelFontSize='9' /> ");
                    }
                    else if (zDs.Tables[0].Rows[zCont]["Indicador"].ToString().Trim() == "2")
                    {
                        xmlData3.Append(" <set label = 'Sim' value = '" + zDs.Tables[0].Rows[zCont]["Valor"].ToString().Trim() + "' color='#31978D'  labelFont='Tahoma' labelFontSize='9' /> ");
                    }
                }
            }
            else
            {
                xmlData3.Append(" <set label = 'Não' value = '0'  color='#E98442' labelFont='Tahoma' labelFontSize='9' /> ");
            }



            xmlData3.AppendFormat("</chart>");



            zDs = xGraf.Grafico_Anamnese("HasDiabetes", Session["Empresa"].ToString(), txt_Data.Text, txt_Data2.Text, xEmp);

            xmlData4.Append(" subCaption='Diabetes' > ");

            if (zDs.Tables[0].Rows.Count > 0)
            {
                for (int zCont = 0; zCont < zDs.Tables[0].Rows.Count; zCont++)
                {
                    if (zDs.Tables[0].Rows[zCont]["Indicador"].ToString().Trim() == "1")
                    {
                        xmlData4.Append(" <set label = 'Não' value = '" + zDs.Tables[0].Rows[zCont]["Valor"].ToString().Trim() + "'  color='#E98442' labelFont='Tahoma' labelFontSize='9' /> ");
                    }
                    else if (zDs.Tables[0].Rows[zCont]["Indicador"].ToString().Trim() == "2")
                    {
                        xmlData4.Append(" <set label = 'Sim' value = '" + zDs.Tables[0].Rows[zCont]["Valor"].ToString().Trim() + "' color='#31978D'  labelFont='Tahoma' labelFontSize='9' /> ");
                    }
                }
            }
            else
            {
                xmlData4.Append(" <set label = 'Não' value = '0'  color='#E98442' labelFont='Tahoma' labelFontSize='9' /> ");
            }



            xmlData4.AppendFormat("</chart>");




            zDs = xGraf.Grafico_Anamnese("HasCoracao", Session["Empresa"].ToString(), txt_Data.Text, txt_Data2.Text, xEmp);

            xmlData5.Append(" subCaption='Doenças do Coração' > ");

            if (zDs.Tables[0].Rows.Count > 0)
            {
                for (int zCont = 0; zCont < zDs.Tables[0].Rows.Count; zCont++)
                {
                    if (zDs.Tables[0].Rows[zCont]["Indicador"].ToString().Trim() == "1")
                    {
                        xmlData5.Append(" <set label = 'Não' value = '" + zDs.Tables[0].Rows[zCont]["Valor"].ToString().Trim() + "'  color='#E98442' labelFont='Tahoma' labelFontSize='9' /> ");
                    }
                    else if (zDs.Tables[0].Rows[zCont]["Indicador"].ToString().Trim() == "2")
                    {
                        xmlData5.Append(" <set label = 'Sim' value = '" + zDs.Tables[0].Rows[zCont]["Valor"].ToString().Trim() + "' color='#31978D'  labelFont='Tahoma' labelFontSize='9' /> ");
                    }
                }
            }
            else
            {
                xmlData5.Append(" <set label = 'Não' value = '0'  color='#E98442' labelFont='Tahoma' labelFontSize='9' /> ");
            }



            xmlData5.AppendFormat("</chart>");




            zDs = xGraf.Grafico_Anamnese("HasDigestiva", Session["Empresa"].ToString(), txt_Data.Text, txt_Data2.Text, xEmp);

            xmlData6.Append(" subCaption='Doenças Digestivas' > ");

            if (zDs.Tables[0].Rows.Count > 0)
            {
                for (int zCont = 0; zCont < zDs.Tables[0].Rows.Count; zCont++)
                {
                    if (zDs.Tables[0].Rows[zCont]["Indicador"].ToString().Trim() == "1")
                    {
                        xmlData6.Append(" <set label = 'Não' value = '" + zDs.Tables[0].Rows[zCont]["Valor"].ToString().Trim() + "'  color='#E98442' labelFont='Tahoma' labelFontSize='9' /> ");
                    }
                    else if (zDs.Tables[0].Rows[zCont]["Indicador"].ToString().Trim() == "2")
                    {
                        xmlData6.Append(" <set label = 'Sim' value = '" + zDs.Tables[0].Rows[zCont]["Valor"].ToString().Trim() + "' color='#31978D'  labelFont='Tahoma' labelFontSize='9' /> ");
                    }
                }
            }
            else
            {
                xmlData6.Append(" <set label = 'Não' value = '0'  color='#E98442' labelFont='Tahoma' labelFontSize='9' /> ");
            }



            xmlData6.AppendFormat("</chart>");



            zDs = xGraf.Grafico_Anamnese("HasEstomago", Session["Empresa"].ToString(), txt_Data.Text, txt_Data2.Text, xEmp);

            xmlData7.Append(" subCaption='Doenças do Estômago' > ");

            if (zDs.Tables[0].Rows.Count > 0)
            {
                for (int zCont = 0; zCont < zDs.Tables[0].Rows.Count; zCont++)
                {
                    if (zDs.Tables[0].Rows[zCont]["Indicador"].ToString().Trim() == "1")
                    {
                        xmlData7.Append(" <set label = 'Não' value = '" + zDs.Tables[0].Rows[zCont]["Valor"].ToString().Trim() + "'  color='#E98442' labelFont='Tahoma' labelFontSize='9' /> ");
                    }
                    else if (zDs.Tables[0].Rows[zCont]["Indicador"].ToString().Trim() == "2")
                    {
                        xmlData7.Append(" <set label = 'Sim' value = '" + zDs.Tables[0].Rows[zCont]["Valor"].ToString().Trim() + "' color='#31978D'  labelFont='Tahoma' labelFontSize='9' /> ");
                    }
                }
            }
            else
            {
                xmlData7.Append(" <set label = 'Não' value = '0'  color='#E98442' labelFont='Tahoma' labelFontSize='9' /> ");
            }



            xmlData7.AppendFormat("</chart>");




            zDs = xGraf.Grafico_Anamnese("HasUrinaria", Session["Empresa"].ToString(), txt_Data.Text, txt_Data2.Text, xEmp);

            xmlData8.Append(" subCaption='Doenças Urinárias' > ");

            if (zDs.Tables[0].Rows.Count > 0)
            {
                for (int zCont = 0; zCont < zDs.Tables[0].Rows.Count; zCont++)
                {
                    if (zDs.Tables[0].Rows[zCont]["Indicador"].ToString().Trim() == "1")
                    {
                        xmlData8.Append(" <set label = 'Não' value = '" + zDs.Tables[0].Rows[zCont]["Valor"].ToString().Trim() + "'  color='#E98442' labelFont='Tahoma' labelFontSize='9' /> ");
                    }
                    else if (zDs.Tables[0].Rows[zCont]["Indicador"].ToString().Trim() == "2")
                    {
                        xmlData8.Append(" <set label = 'Sim' value = '" + zDs.Tables[0].Rows[zCont]["Valor"].ToString().Trim() + "' color='#31978D'  labelFont='Tahoma' labelFontSize='9' /> ");
                    }
                }
            }
            else
            {
                xmlData8.Append(" <set label = 'Não' value = '0'  color='#E98442' labelFont='Tahoma' labelFontSize='9' /> ");
            }



            xmlData8.AppendFormat("</chart>");



            Chart factoryOutput = new Chart("pie2d", "Anamnese1", "390", "390", "xml", xmlData.ToString());
            Chart factoryOutput2 = new Chart("pie2d", "Anamnese2", "390", "390", "xml", xmlData2.ToString());
            Chart factoryOutput3 = new Chart("pie2d", "Anamnese3", "390", "390", "xml", xmlData3.ToString());
            Chart factoryOutput4 = new Chart("pie2d", "Anamnese4", "390", "390", "xml", xmlData4.ToString());
            Chart factoryOutput5 = new Chart("pie2d", "Anamnese5", "390", "390", "xml", xmlData5.ToString());
            Chart factoryOutput6 = new Chart("pie2d", "Anamnese6", "390", "390", "xml", xmlData6.ToString());
            Chart factoryOutput7 = new Chart("pie2d", "Anamnese7", "390", "390", "xml", xmlData7.ToString());
            Chart factoryOutput8 = new Chart("pie2d", "Anamnese8", "390", "390", "xml", xmlData8.ToString());




            // Render the chart.
            Literal1.Text = factoryOutput.Render();

            Literal2.Text = factoryOutput2.Render();

            Literal3.Text = factoryOutput3.Render();

            Literal4.Text = factoryOutput4.Render();

            Literal5.Text = factoryOutput5.Render();

            Literal6.Text = factoryOutput6.Render();

            Literal7.Text = factoryOutput7.Render();

            Literal8.Text = factoryOutput8.Render();

            Literal9.Text = "";

        }




        private void rGrafico_Anamnese_Anamnese2()
        {
            StringBuilder xmlData = new StringBuilder();


            Int16 xEmp = 1;

            xEmp = System.Convert.ToInt16(cmb_Empresa.SelectedValue.ToString());

            string xPorcentagem = "0";

            xPorcentagem = cmb_Anamnese_Dados.SelectedValue.ToString();



            xmlData.Append("<chart caption='Anamnese - 2' showBorder='1' formatNumberScale='0' exportEnabled='1'  showPercentValues='" + xPorcentagem + "' showpercentintooltip='1' theme='gammel' usedataplotcolorforlabels='1' ");

            //graph color
            xmlData.Append("  palettecolors='#008ee4'  useplotgradientcolor='0'  showplotborder='0'  showShadow='0'  palette='2' showlegend='1' ");

            //background color
            xmlData.Append(" bgColor='#F1EBE6'  canvasBgAlpha='0' bgAlpha='50'  bgratio='60, 40' ");

            //fonte
            xmlData.Append("  legendItemFont='Tahoma' legendItemFontSize='9'  legendItemFontBold='1' legendIconScale='2' ");
            xmlData.Append("  baseFont='Tahoma' baseFontSize='9' baseFontColor='#000000'  outcnvbasefontsize='9' outcnvBaseFonte='Tamoha' ");
            xmlData.Append("  labelFont='Tahoma' labelFontSize='9' labelFontColor='#000000' labelFontBold='1'  ");


            //margens
            xmlData.Append(" chartLeftMargin='3' chartTopMargin='3'  chartRightMargin='3'  chartBottomMargin='3' ");

            xmlData.Append(" rotatelabels ='1' showvalues='1'  ");

            StringBuilder xmlData2 = new StringBuilder(xmlData.ToString());
            StringBuilder xmlData3 = new StringBuilder(xmlData.ToString());
            StringBuilder xmlData4 = new StringBuilder(xmlData.ToString());
            StringBuilder xmlData5 = new StringBuilder(xmlData.ToString());
            StringBuilder xmlData6 = new StringBuilder(xmlData.ToString());
            StringBuilder xmlData7 = new StringBuilder(xmlData.ToString());
            StringBuilder xmlData8 = new StringBuilder(xmlData.ToString());



            // -520402605

            // https://www.fusioncharts.com/dev/chart-attributes/pie2d

            //colocar dados em cada 1 dos 8 gráficos de antecedentes

            Ilitera.Data.Empregado_Cadastral xGraf = new Ilitera.Data.Empregado_Cadastral();
            DataSet zDs = xGraf.Grafico_Anamnese("HasDorCabeca", Session["Empresa"].ToString(), txt_Data.Text, txt_Data2.Text, xEmp);

            xmlData.Append(" subCaption='Dor de Cabeça' > ");

            if (zDs.Tables[0].Rows.Count > 0)
            {
                for (int zCont = 0; zCont < zDs.Tables[0].Rows.Count; zCont++)
                {
                    if (zDs.Tables[0].Rows[zCont]["Indicador"].ToString().Trim() == "1")
                    {
                        xmlData.Append(" <set label = 'Não' value = '" + zDs.Tables[0].Rows[zCont]["Valor"].ToString().Trim() + "'  color='#E98442' labelFont='Tahoma' labelFontSize='9' /> ");
                    }
                    else if (zDs.Tables[0].Rows[zCont]["Indicador"].ToString().Trim() == "2")
                    {
                        xmlData.Append(" <set label = 'Sim' value = '" + zDs.Tables[0].Rows[zCont]["Valor"].ToString().Trim() + "' color='#31978D'  labelFont='Tahoma' labelFontSize='9' /> ");
                    }
                }
            }
            else
            {
                xmlData.Append(" <set label = 'Não' value = '0'  color='#E98442' labelFont='Tahoma' labelFontSize='9' /> ");
            }


            xmlData.AppendFormat("</chart>");



            zDs = xGraf.Grafico_Anamnese("HasDoresCosta", Session["Empresa"].ToString(), txt_Data.Text, txt_Data2.Text, xEmp);

            xmlData2.Append(" subCaption='Dores nas Costas e Coluna' > ");

            if (zDs.Tables[0].Rows.Count > 0)
            {
                for (int zCont = 0; zCont < zDs.Tables[0].Rows.Count; zCont++)
                {
                    if (zDs.Tables[0].Rows[zCont]["Indicador"].ToString().Trim() == "1")
                    {
                        xmlData2.Append(" <set label = 'Não' value = '" + zDs.Tables[0].Rows[zCont]["Valor"].ToString().Trim() + "'  color='#E98442' labelFont='Tahoma' labelFontSize='9' /> ");
                    }
                    else if (zDs.Tables[0].Rows[zCont]["Indicador"].ToString().Trim() == "2")
                    {
                        xmlData2.Append(" <set label = 'Sim' value = '" + zDs.Tables[0].Rows[zCont]["Valor"].ToString().Trim() + "' color='#31978D'  labelFont='Tahoma' labelFontSize='9' /> ");
                    }
                }
            }
            else
            {
                xmlData2.Append(" <set label = 'Não' value = '0'  color='#E98442' labelFont='Tahoma' labelFontSize='9' /> ");
            }



            xmlData2.AppendFormat("</chart>");



            zDs = xGraf.Grafico_Anamnese("HasEnxerga", Session["Empresa"].ToString(), txt_Data.Text, txt_Data2.Text, xEmp);

            xmlData3.Append(" subCaption='Enxerga Bem' > ");

            if (zDs.Tables[0].Rows.Count > 0)
            {
                for (int zCont = 0; zCont < zDs.Tables[0].Rows.Count; zCont++)
                {
                    if (zDs.Tables[0].Rows[zCont]["Indicador"].ToString().Trim() == "1")
                    {
                        xmlData3.Append(" <set label = 'Não' value = '" + zDs.Tables[0].Rows[zCont]["Valor"].ToString().Trim() + "'  color='#E98442' labelFont='Tahoma' labelFontSize='9' /> ");
                    }
                    else if (zDs.Tables[0].Rows[zCont]["Indicador"].ToString().Trim() == "2")
                    {
                        xmlData3.Append(" <set label = 'Sim' value = '" + zDs.Tables[0].Rows[zCont]["Valor"].ToString().Trim() + "' color='#31978D'  labelFont='Tahoma' labelFontSize='9' /> ");
                    }
                }
            }
            else
            {
                xmlData3.Append(" <set label = 'Não' value = '0'  color='#E98442' labelFont='Tahoma' labelFontSize='9' /> ");
            }



            xmlData3.AppendFormat("</chart>");



            zDs = xGraf.Grafico_Anamnese("HasEscuta", Session["Empresa"].ToString(), txt_Data.Text, txt_Data2.Text, xEmp);

            xmlData4.Append(" subCaption='Escuta Bem' > ");

            if (zDs.Tables[0].Rows.Count > 0)
            {
                for (int zCont = 0; zCont < zDs.Tables[0].Rows.Count; zCont++)
                {
                    if (zDs.Tables[0].Rows[zCont]["Indicador"].ToString().Trim() == "1")
                    {
                        xmlData4.Append(" <set label = 'Não' value = '" + zDs.Tables[0].Rows[zCont]["Valor"].ToString().Trim() + "'  color='#E98442' labelFont='Tahoma' labelFontSize='9' /> ");
                    }
                    else if (zDs.Tables[0].Rows[zCont]["Indicador"].ToString().Trim() == "2")
                    {
                        xmlData4.Append(" <set label = 'Sim' value = '" + zDs.Tables[0].Rows[zCont]["Valor"].ToString().Trim() + "' color='#31978D'  labelFont='Tahoma' labelFontSize='9' /> ");
                    }
                }
            }
            else
            {
                xmlData4.Append(" <set label = 'Não' value = '0'  color='#E98442' labelFont='Tahoma' labelFontSize='9' /> ");
            }



            xmlData4.AppendFormat("</chart>");




            zDs = xGraf.Grafico_Anamnese("HasGripado", Session["Empresa"].ToString(), txt_Data.Text, txt_Data2.Text, xEmp);

            xmlData5.Append(" subCaption='Gripado com Frequência' > ");

            if (zDs.Tables[0].Rows.Count > 0)
            {
                for (int zCont = 0; zCont < zDs.Tables[0].Rows.Count; zCont++)
                {
                    if (zDs.Tables[0].Rows[zCont]["Indicador"].ToString().Trim() == "1")
                    {
                        xmlData5.Append(" <set label = 'Não' value = '" + zDs.Tables[0].Rows[zCont]["Valor"].ToString().Trim() + "'  color='#E98442' labelFont='Tahoma' labelFontSize='9' /> ");
                    }
                    else if (zDs.Tables[0].Rows[zCont]["Indicador"].ToString().Trim() == "2")
                    {
                        xmlData5.Append(" <set label = 'Sim' value = '" + zDs.Tables[0].Rows[zCont]["Valor"].ToString().Trim() + "' color='#31978D'  labelFont='Tahoma' labelFontSize='9' /> ");
                    }
                }
            }
            else
            {
                xmlData5.Append(" <set label = 'Não' value = '0'  color='#E98442' labelFont='Tahoma' labelFontSize='9' /> ");
            }



            xmlData5.AppendFormat("</chart>");




            zDs = xGraf.Grafico_Anamnese("HasDesmaio", Session["Empresa"].ToString(), txt_Data.Text, txt_Data2.Text, xEmp);

            xmlData6.Append(" subCaption='Já Desmaiou' > ");

            if (zDs.Tables[0].Rows.Count > 0)
            {
                for (int zCont = 0; zCont < zDs.Tables[0].Rows.Count; zCont++)
                {
                    if (zDs.Tables[0].Rows[zCont]["Indicador"].ToString().Trim() == "1")
                    {
                        xmlData6.Append(" <set label = 'Não' value = '" + zDs.Tables[0].Rows[zCont]["Valor"].ToString().Trim() + "'  color='#E98442' labelFont='Tahoma' labelFontSize='9' /> ");
                    }
                    else if (zDs.Tables[0].Rows[zCont]["Indicador"].ToString().Trim() == "2")
                    {
                        xmlData6.Append(" <set label = 'Sim' value = '" + zDs.Tables[0].Rows[zCont]["Valor"].ToString().Trim() + "' color='#31978D'  labelFont='Tahoma' labelFontSize='9' /> ");
                    }
                }
            }
            else
            {
                xmlData6.Append(" <set label = 'Não' value = '0'  color='#E98442' labelFont='Tahoma' labelFontSize='9' /> ");
            }



            xmlData6.AppendFormat("</chart>");



            zDs = xGraf.Grafico_Anamnese("HasBronquite", Session["Empresa"].ToString(), txt_Data.Text, txt_Data2.Text, xEmp);

            xmlData7.Append(" subCaption='Bronquite / Asma / Rinite' > ");

            if (zDs.Tables[0].Rows.Count > 0)
            {
                for (int zCont = 0; zCont < zDs.Tables[0].Rows.Count; zCont++)
                {
                    if (zDs.Tables[0].Rows[zCont]["Indicador"].ToString().Trim() == "1")
                    {
                        xmlData7.Append(" <set label = 'Não' value = '" + zDs.Tables[0].Rows[zCont]["Valor"].ToString().Trim() + "'  color='#E98442' labelFont='Tahoma' labelFontSize='9' /> ");
                    }
                    else if (zDs.Tables[0].Rows[zCont]["Indicador"].ToString().Trim() == "2")
                    {
                        xmlData7.Append(" <set label = 'Sim' value = '" + zDs.Tables[0].Rows[zCont]["Valor"].ToString().Trim() + "' color='#31978D'  labelFont='Tahoma' labelFontSize='9' /> ");
                    }
                }
            }
            else
            {
                xmlData7.Append(" <set label = 'Não' value = '0'  color='#E98442' labelFont='Tahoma' labelFontSize='9' /> ");
            }



            xmlData7.AppendFormat("</chart>");




            zDs = xGraf.Grafico_Anamnese("HasCirurgia", Session["Empresa"].ToString(), txt_Data.Text, txt_Data2.Text, xEmp);

            xmlData8.Append(" subCaption='Hospitalizado Alguma Vez' > ");

            if (zDs.Tables[0].Rows.Count > 0)
            {
                for (int zCont = 0; zCont < zDs.Tables[0].Rows.Count; zCont++)
                {
                    if (zDs.Tables[0].Rows[zCont]["Indicador"].ToString().Trim() == "1")
                    {
                        xmlData8.Append(" <set label = 'Não' value = '" + zDs.Tables[0].Rows[zCont]["Valor"].ToString().Trim() + "'  color='#E98442' labelFont='Tahoma' labelFontSize='9' /> ");
                    }
                    else if (zDs.Tables[0].Rows[zCont]["Indicador"].ToString().Trim() == "2")
                    {
                        xmlData8.Append(" <set label = 'Sim' value = '" + zDs.Tables[0].Rows[zCont]["Valor"].ToString().Trim() + "' color='#31978D'  labelFont='Tahoma' labelFontSize='9' /> ");
                    }
                }
            }
            else
            {
                xmlData8.Append(" <set label = 'Não' value = '0'  color='#E98442' labelFont='Tahoma' labelFontSize='9' /> ");
            }



            xmlData8.AppendFormat("</chart>");



            Chart factoryOutput = new Chart("pie2d", "Anamnese1", "390", "390", "xml", xmlData.ToString());
            Chart factoryOutput2 = new Chart("pie2d", "Anamnese2", "390", "390", "xml", xmlData2.ToString());
            Chart factoryOutput3 = new Chart("pie2d", "Anamnese3", "390", "390", "xml", xmlData3.ToString());
            Chart factoryOutput4 = new Chart("pie2d", "Anamnese4", "390", "390", "xml", xmlData4.ToString());
            Chart factoryOutput5 = new Chart("pie2d", "Anamnese5", "390", "390", "xml", xmlData5.ToString());
            Chart factoryOutput6 = new Chart("pie2d", "Anamnese6", "390", "390", "xml", xmlData6.ToString());
            Chart factoryOutput7 = new Chart("pie2d", "Anamnese7", "390", "390", "xml", xmlData7.ToString());
            Chart factoryOutput8 = new Chart("pie2d", "Anamnese8", "390", "390", "xml", xmlData8.ToString());




            // Render the chart.
            Literal1.Text = factoryOutput.Render();

            Literal2.Text = factoryOutput2.Render();

            Literal3.Text = factoryOutput3.Render();

            Literal4.Text = factoryOutput4.Render();

            Literal5.Text = factoryOutput5.Render();

            Literal6.Text = factoryOutput6.Render();

            Literal7.Text = factoryOutput7.Render();

            Literal8.Text = factoryOutput8.Render();

            Literal9.Text = "";

        }




        private void rGrafico_Anamnese_Anamnese3()
        {
            StringBuilder xmlData = new StringBuilder();


            Int16 xEmp = 1;

            xEmp = System.Convert.ToInt16(cmb_Empresa.SelectedValue.ToString());

            string xPorcentagem = "0";

            xPorcentagem = cmb_Anamnese_Dados.SelectedValue.ToString();



            xmlData.Append("<chart caption='Anamnese - 3' showBorder='1' formatNumberScale='0' exportEnabled='1'  showPercentValues='" + xPorcentagem + "' showpercentintooltip='1' theme='gammel' usedataplotcolorforlabels='1' ");

            //graph color
            xmlData.Append("  palettecolors='#008ee4'  useplotgradientcolor='0'  showplotborder='0'  showShadow='0'  palette='2' showlegend='1' ");

            //background color
            xmlData.Append(" bgColor='#F1EBE6'  canvasBgAlpha='0' bgAlpha='50'  bgratio='60, 40' ");
            
            //fonte
            xmlData.Append("  legendItemFont='Tahoma' legendItemFontSize='9'  legendItemFontBold='1' legendIconScale='2' ");
            xmlData.Append("  baseFont='Tahoma' baseFontSize='9' baseFontColor='#000000'  outcnvbasefontsize='9' outcnvBaseFonte='Tamoha' ");
            xmlData.Append("  labelFont='Tahoma' labelFontSize='9' labelFontColor='#000000' labelFontBold='1'  ");


            //margens
            xmlData.Append(" chartLeftMargin='3' chartTopMargin='3'  chartRightMargin='3'  chartBottomMargin='3' ");

            xmlData.Append(" rotatelabels ='1' showvalues='1'  ");

            StringBuilder xmlData2 = new StringBuilder(xmlData.ToString());
            StringBuilder xmlData3 = new StringBuilder(xmlData.ToString());
            StringBuilder xmlData4 = new StringBuilder(xmlData.ToString());
            StringBuilder xmlData5 = new StringBuilder(xmlData.ToString());



            // -520402605

            // https://www.fusioncharts.com/dev/chart-attributes/pie2d

            //colocar dados em cada 1 dos 5 gráficos 

            Ilitera.Data.Empregado_Cadastral xGraf = new Ilitera.Data.Empregado_Cadastral();
            DataSet zDs = xGraf.Grafico_Anamnese("HasAfastamento", Session["Empresa"].ToString(), txt_Data.Text, txt_Data2.Text, xEmp);

            xmlData.Append(" subCaption='Já ficou Afastado' > ");

            if (zDs.Tables[0].Rows.Count > 0)
            {
                for (int zCont = 0; zCont < zDs.Tables[0].Rows.Count; zCont++)
                {
                    if (zDs.Tables[0].Rows[zCont]["Indicador"].ToString().Trim() == "1")
                    {
                        xmlData.Append(" <set label = 'Não' value = '" + zDs.Tables[0].Rows[zCont]["Valor"].ToString().Trim() + "'  color='#E98442' labelFont='Tahoma' labelFontSize='9' /> ");
                    }
                    else if (zDs.Tables[0].Rows[zCont]["Indicador"].ToString().Trim() == "2")
                    {
                        xmlData.Append(" <set label = 'Sim' value = '" + zDs.Tables[0].Rows[zCont]["Valor"].ToString().Trim() + "' color='#31978D'  labelFont='Tahoma' labelFontSize='9' /> ");
                    }
                }
            }
            else
            {
                xmlData.Append(" <set label = 'Não' value = '0'  color='#E98442' labelFont='Tahoma' labelFontSize='9' /> ");
            }


            xmlData.AppendFormat("</chart>");



            zDs = xGraf.Grafico_Anamnese("HasReumatismo", Session["Empresa"].ToString(), txt_Data.Text, txt_Data2.Text, xEmp);

            xmlData2.Append(" subCaption='Reumatismo' > ");

            if (zDs.Tables[0].Rows.Count > 0)
            {
                for (int zCont = 0; zCont < zDs.Tables[0].Rows.Count; zCont++)
                {
                    if (zDs.Tables[0].Rows[zCont]["Indicador"].ToString().Trim() == "1")
                    {
                        xmlData2.Append(" <set label = 'Não' value = '" + zDs.Tables[0].Rows[zCont]["Valor"].ToString().Trim() + "'  color='#E98442' labelFont='Tahoma' labelFontSize='9' /> ");
                    }
                    else if (zDs.Tables[0].Rows[zCont]["Indicador"].ToString().Trim() == "2")
                    {
                        xmlData2.Append(" <set label = 'Sim' value = '" + zDs.Tables[0].Rows[zCont]["Valor"].ToString().Trim() + "' color='#31978D'  labelFont='Tahoma' labelFontSize='9' /> ");
                    }
                }
            }
            else
            {
                xmlData2.Append(" <set label = 'Não' value = '0'  color='#E98442' labelFont='Tahoma' labelFontSize='9' /> ");
            }



            xmlData2.AppendFormat("</chart>");



            zDs = xGraf.Grafico_Anamnese("HasTraumatismos", Session["Empresa"].ToString(), txt_Data.Text, txt_Data2.Text, xEmp);

            xmlData3.Append(" subCaption='Teve alguma fratura' > ");

            if (zDs.Tables[0].Rows.Count > 0)
            {
                for (int zCont = 0; zCont < zDs.Tables[0].Rows.Count; zCont++)
                {
                    if (zDs.Tables[0].Rows[zCont]["Indicador"].ToString().Trim() == "1")
                    {
                        xmlData3.Append(" <set label = 'Não' value = '" + zDs.Tables[0].Rows[zCont]["Valor"].ToString().Trim() + "'  color='#E98442' labelFont='Tahoma' labelFontSize='9' /> ");
                    }
                    else if (zDs.Tables[0].Rows[zCont]["Indicador"].ToString().Trim() == "2")
                    {
                        xmlData3.Append(" <set label = 'Sim' value = '" + zDs.Tables[0].Rows[zCont]["Valor"].ToString().Trim() + "' color='#31978D'  labelFont='Tahoma' labelFontSize='9' /> ");
                    }
                }
            }
            else
            {
                xmlData3.Append(" <set label = 'Não' value = '0'  color='#E98442' labelFont='Tahoma' labelFontSize='9' /> ");
            }



            xmlData3.AppendFormat("</chart>");



            zDs = xGraf.Grafico_Anamnese("HasMedicacoes", Session["Empresa"].ToString(), txt_Data.Text, txt_Data2.Text, xEmp);

            xmlData4.Append(" subCaption='Uso de Medicamento Contínuo' > ");

            if (zDs.Tables[0].Rows.Count > 0)
            {
                for (int zCont = 0; zCont < zDs.Tables[0].Rows.Count; zCont++)
                {
                    if (zDs.Tables[0].Rows[zCont]["Indicador"].ToString().Trim() == "1")
                    {
                        xmlData4.Append(" <set label = 'Não' value = '" + zDs.Tables[0].Rows[zCont]["Valor"].ToString().Trim() + "'  color='#E98442' labelFont='Tahoma' labelFontSize='9' /> ");
                    }
                    else if (zDs.Tables[0].Rows[zCont]["Indicador"].ToString().Trim() == "2")
                    {
                        xmlData4.Append(" <set label = 'Sim' value = '" + zDs.Tables[0].Rows[zCont]["Valor"].ToString().Trim() + "' color='#31978D'  labelFont='Tahoma' labelFontSize='9' /> ");
                    }
                }
            }
            else
            {
                xmlData4.Append(" <set label = 'Não' value = '0'  color='#E98442' labelFont='Tahoma' labelFontSize='9' /> ");
            }



            xmlData4.AppendFormat("</chart>");




            zDs = xGraf.Grafico_Anamnese("HasQueixasAtuais", Session["Empresa"].ToString(), txt_Data.Text, txt_Data2.Text, xEmp);

            xmlData5.Append(" subCaption='Está bem de saúde' > ");

            if (zDs.Tables[0].Rows.Count > 0)
            {
                for (int zCont = 0; zCont < zDs.Tables[0].Rows.Count; zCont++)
                {
                    if (zDs.Tables[0].Rows[zCont]["Indicador"].ToString().Trim() == "1")
                    {
                        xmlData5.Append(" <set label = 'Não' value = '" + zDs.Tables[0].Rows[zCont]["Valor"].ToString().Trim() + "'  color='#E98442' labelFont='Tahoma' labelFontSize='9' /> ");
                    }
                    else if (zDs.Tables[0].Rows[zCont]["Indicador"].ToString().Trim() == "2")
                    {
                        xmlData5.Append(" <set label = 'Sim' value = '" + zDs.Tables[0].Rows[zCont]["Valor"].ToString().Trim() + "' color='#31978D'  labelFont='Tahoma' labelFontSize='9' /> ");
                    }
                }
            }
            else
            {
                xmlData5.Append(" <set label = 'Não' value = '0'  color='#E98442' labelFont='Tahoma' labelFontSize='9' /> ");
            }



            xmlData5.AppendFormat("</chart>");






            Chart factoryOutput = new Chart("pie2d", "Anamnese1", "390", "390", "xml", xmlData.ToString());
            Chart factoryOutput2 = new Chart("pie2d", "Anamnese2", "390", "390", "xml", xmlData2.ToString());
            Chart factoryOutput3 = new Chart("pie2d", "Anamnese3", "390", "390", "xml", xmlData3.ToString());
            Chart factoryOutput4 = new Chart("pie2d", "Anamnese4", "390", "390", "xml", xmlData4.ToString());
            Chart factoryOutput5 = new Chart("pie2d", "Anamnese5", "390", "390", "xml", xmlData5.ToString());




            // Render the chart.
            Literal1.Text = factoryOutput.Render();

            Literal2.Text = factoryOutput2.Render();

            Literal3.Text = factoryOutput3.Render();

            Literal4.Text = factoryOutput4.Render();

            Literal5.Text = factoryOutput5.Render();

            Literal6.Text = "";
            Literal7.Text = "";
            Literal8.Text = "";
            Literal9.Text = "";


        }








        private void rGrafico_Anamnese_Alteracoes()
        {
            StringBuilder xmlData = new StringBuilder();


            Int16 xEmp = 1;

            xEmp = System.Convert.ToInt16(cmb_Empresa.SelectedValue.ToString());


            string xPorcentagem = "0";

            xPorcentagem = cmb_Anamnese_Dados.SelectedValue.ToString();



            xmlData.Append("<chart caption='Exame Físico' showBorder='1' formatNumberScale='0' exportEnabled='1' showPercentValues='" + xPorcentagem + "' showpercentintooltip='1' theme='gammel' usedataplotcolorforlabels='1' ");

            //graph color
            xmlData.Append("  palettecolors='#008ee4'  useplotgradientcolor='0'  showplotborder='0'  showShadow='0'  palette='2' showlegend='1' ");


            //background color
            xmlData.Append(" bgColor='#F1EBE6'  canvasBgAlpha='0' bgAlpha='50'  bgratio='60, 40' ");

            //fonte
            xmlData.Append("  legendItemFont='Tahoma' legendItemFontSize='9'  legendItemFontBold='1' legendIconScale='2' ");
            xmlData.Append("  baseFont='Tahoma' baseFontSize='9' baseFontColor='#000000'  outcnvbasefontsize='9' outcnvBaseFonte='Tamoha' ");
            xmlData.Append("  labelFont='Tahoma' labelFontSize='9' labelFontColor='#000000' labelFontBold='1'  ");


            //margens
            xmlData.Append(" chartLeftMargin='3' chartTopMargin='3'  chartRightMargin='3'  chartBottomMargin='3' ");

            xmlData.Append(" rotatelabels ='1' showvalues='1'  ");

            StringBuilder xmlData2 = new StringBuilder(xmlData.ToString());
            StringBuilder xmlData3 = new StringBuilder(xmlData.ToString());
            StringBuilder xmlData4 = new StringBuilder(xmlData.ToString());
            StringBuilder xmlData5 = new StringBuilder(xmlData.ToString());
            StringBuilder xmlData6 = new StringBuilder(xmlData.ToString());
            StringBuilder xmlData7 = new StringBuilder(xmlData.ToString());
            StringBuilder xmlData8 = new StringBuilder(xmlData.ToString());



            // -520402605

            // https://www.fusioncharts.com/dev/chart-attributes/pie2d

            //colocar dados em cada 1 dos 8 gráficos de antecedentes

            Ilitera.Data.Empregado_Cadastral xGraf = new Ilitera.Data.Empregado_Cadastral();
            DataSet zDs = xGraf.Grafico_Exame_Fisico("hasPeleAnexosAlterado", Session["Empresa"].ToString(), txt_Data.Text, txt_Data2.Text, xEmp);

            xmlData.Append(" subCaption='Peles e Anexos' > ");

            if (zDs.Tables[0].Rows.Count > 0)
            {
                for (int zCont = 0; zCont < zDs.Tables[0].Rows.Count; zCont++)
                {
                    if (zDs.Tables[0].Rows[zCont]["Indicador"].ToString().Trim() == "1")
                    {
                        xmlData.Append(" <set label = 'Não' value = '" + zDs.Tables[0].Rows[zCont]["Valor"].ToString().Trim() + "'  color='#E98442' labelFont='Tahoma' labelFontSize='9' /> ");
                    }
                    else if (zDs.Tables[0].Rows[zCont]["Indicador"].ToString().Trim() == "2" )
                    {
                        xmlData.Append(" <set label = 'Sim' value = '" + zDs.Tables[0].Rows[zCont]["Valor"].ToString().Trim() + "' color='#31978D' labelFont='Tahoma' labelFontSize='9' /> ");
                    }
                }
            }
            else
            {
                xmlData.Append(" <set label = 'Não' value = '0'  color='#E98442' labelFont='Tahoma' labelFontSize='9' /> ");
            }


            xmlData.AppendFormat("</chart>");



            zDs = xGraf.Grafico_Exame_Fisico("hasOsteoAlterado", Session["Empresa"].ToString(), txt_Data.Text, txt_Data2.Text, xEmp);

            xmlData2.Append(" subCaption='Osteo Muscular' > ");

            if (zDs.Tables[0].Rows.Count > 0)
            {
                for (int zCont = 0; zCont < zDs.Tables[0].Rows.Count; zCont++)
                {
                    if (zDs.Tables[0].Rows[zCont]["Indicador"].ToString().Trim() == "1")
                    {
                        xmlData2.Append(" <set label = 'Não' value = '" + zDs.Tables[0].Rows[zCont]["Valor"].ToString().Trim() + "'  color='#E98442' labelFont='Tahoma' labelFontSize='9' /> ");
                    }
                    else if (zDs.Tables[0].Rows[zCont]["Indicador"].ToString().Trim() == "2")
                    {
                        xmlData2.Append(" <set label = 'Sim' value = '" + zDs.Tables[0].Rows[zCont]["Valor"].ToString().Trim() + "' color='#31978D'  labelFont='Tahoma' labelFontSize='9' /> ");
                    }
                }
            }
            else
            {
                xmlData2.Append(" <set label = 'Não' value = '0'  color='#E98442' labelFont='Tahoma' labelFontSize='9' /> ");
            }



            xmlData2.AppendFormat("</chart>");



            zDs = xGraf.Grafico_Exame_Fisico("hasCabecaAlterado", Session["Empresa"].ToString(), txt_Data.Text, txt_Data2.Text, xEmp);

            xmlData3.Append(" subCaption='Cabeça e Pescoço' > ");

            if (zDs.Tables[0].Rows.Count > 0)
            {
                for (int zCont = 0; zCont < zDs.Tables[0].Rows.Count; zCont++)
                {
                    if (zDs.Tables[0].Rows[zCont]["Indicador"].ToString().Trim() == "1")
                    {
                        xmlData3.Append(" <set label = 'Não' value = '" + zDs.Tables[0].Rows[zCont]["Valor"].ToString().Trim() + "'  color='#E98442' labelFont='Tahoma' labelFontSize='9' /> ");
                    }
                    else if (zDs.Tables[0].Rows[zCont]["Indicador"].ToString().Trim() == "2")
                    {
                        xmlData3.Append(" <set label = 'Sim' value = '" + zDs.Tables[0].Rows[zCont]["Valor"].ToString().Trim() + "' color='#31978D'  labelFont='Tahoma' labelFontSize='9' /> ");
                    }
                }
            }
            else
            {
                xmlData3.Append(" <set label = 'Não' value = '0'  color='#E98442' labelFont='Tahoma' labelFontSize='9' /> ");
            }



            xmlData3.AppendFormat("</chart>");



            zDs = xGraf.Grafico_Exame_Fisico("hasCoracaoAlterado", Session["Empresa"].ToString(), txt_Data.Text, txt_Data2.Text, xEmp);

            xmlData4.Append(" subCaption='Coração' > ");

            if (zDs.Tables[0].Rows.Count > 0)
            {
                for (int zCont = 0; zCont < zDs.Tables[0].Rows.Count; zCont++)
                {
                    if (zDs.Tables[0].Rows[zCont]["Indicador"].ToString().Trim() == "1")
                    {
                        xmlData4.Append(" <set label = 'Não' value = '" + zDs.Tables[0].Rows[zCont]["Valor"].ToString().Trim() + "'  color='#E98442' labelFont='Tahoma' labelFontSize='9' /> ");
                    }
                    else if (zDs.Tables[0].Rows[zCont]["Indicador"].ToString().Trim() == "2")
                    {
                        xmlData4.Append(" <set label = 'Sim' value = '" + zDs.Tables[0].Rows[zCont]["Valor"].ToString().Trim() + "' color='#31978D'  labelFont='Tahoma' labelFontSize='9' /> ");
                    }
                }
            }
            else
            {
                xmlData4.Append(" <set label = 'Não' value = '0'  color='#E98442' labelFont='Tahoma' labelFontSize='9' /> ");
            }



            xmlData4.AppendFormat("</chart>");




            zDs = xGraf.Grafico_Exame_Fisico("hasPulmaoAlterado", Session["Empresa"].ToString(), txt_Data.Text, txt_Data2.Text, xEmp);

            xmlData5.Append(" subCaption='Pulmão' > ");

            if (zDs.Tables[0].Rows.Count > 0)
            {
                for (int zCont = 0; zCont < zDs.Tables[0].Rows.Count; zCont++)
                {
                    if (zDs.Tables[0].Rows[zCont]["Indicador"].ToString().Trim() == "1")
                    {
                        xmlData5.Append(" <set label = 'Não' value = '" + zDs.Tables[0].Rows[zCont]["Valor"].ToString().Trim() + "'  color='#E98442' labelFont='Tahoma' labelFontSize='9' /> ");
                    }
                    else if (zDs.Tables[0].Rows[zCont]["Indicador"].ToString().Trim() == "2")
                    {
                        xmlData5.Append(" <set label = 'Sim' value = '" + zDs.Tables[0].Rows[zCont]["Valor"].ToString().Trim() + "' color='#31978D'  labelFont='Tahoma' labelFontSize='9' /> ");
                    }
                }
            }
            else
            {
                xmlData5.Append(" <set label = 'Não' value = '0'  color='#E98442' labelFont='Tahoma' labelFontSize='9' /> ");
            }



            xmlData5.AppendFormat("</chart>");




            zDs = xGraf.Grafico_Exame_Fisico("hasAbdomemAlterado", Session["Empresa"].ToString(), txt_Data.Text, txt_Data2.Text, xEmp);

            xmlData6.Append(" subCaption='Abdômen' > ");

            if (zDs.Tables[0].Rows.Count > 0)
            {
                for (int zCont = 0; zCont < zDs.Tables[0].Rows.Count; zCont++)
                {
                    if (zDs.Tables[0].Rows[zCont]["Indicador"].ToString().Trim() == "1")
                    {
                        xmlData6.Append(" <set label = 'Não' value = '" + zDs.Tables[0].Rows[zCont]["Valor"].ToString().Trim() + "'  color='#E98442' labelFont='Tahoma' labelFontSize='9' /> ");
                    }
                    else if (zDs.Tables[0].Rows[zCont]["Indicador"].ToString().Trim() == "2")
                    {
                        xmlData6.Append(" <set label = 'Sim' value = '" + zDs.Tables[0].Rows[zCont]["Valor"].ToString().Trim() + "' color='#31978D'  labelFont='Tahoma' labelFontSize='9' /> ");
                    }
                }
            }
            else
            {
                xmlData6.Append(" <set label = 'Não' value = '0'  color='#E98442' labelFont='Tahoma' labelFontSize='9' /> ");
            }



            xmlData6.AppendFormat("</chart>");



            zDs = xGraf.Grafico_Exame_Fisico("hasMSAlterado", Session["Empresa"].ToString(), txt_Data.Text, txt_Data2.Text, xEmp);

            xmlData7.Append(" subCaption='Membros Superiores' > ");

            if (zDs.Tables[0].Rows.Count > 0)
            {
                for (int zCont = 0; zCont < zDs.Tables[0].Rows.Count; zCont++)
                {
                    if (zDs.Tables[0].Rows[zCont]["Indicador"].ToString().Trim() == "1")
                    {
                        xmlData7.Append(" <set label = 'Não' value = '" + zDs.Tables[0].Rows[zCont]["Valor"].ToString().Trim() + "'  color='#E98442' labelFont='Tahoma' labelFontSize='9' /> ");
                    }
                    else if (zDs.Tables[0].Rows[zCont]["Indicador"].ToString().Trim() == "2")
                    {
                        xmlData7.Append(" <set label = 'Sim' value = '" + zDs.Tables[0].Rows[zCont]["Valor"].ToString().Trim() + "' color='#31978D'  labelFont='Tahoma' labelFontSize='9' /> ");
                    }
                }
            }
            else
            {
                xmlData7.Append(" <set label = 'Não' value = '0'  color='#E98442' labelFont='Tahoma' labelFontSize='9' /> ");
            }



            xmlData7.AppendFormat("</chart>");




            zDs = xGraf.Grafico_Exame_Fisico("hasMIAlterado", Session["Empresa"].ToString(), txt_Data.Text, txt_Data2.Text, xEmp);

            xmlData8.Append(" subCaption='Membros Inferiores' > ");

            if (zDs.Tables[0].Rows.Count > 0)
            {
                for (int zCont = 0; zCont < zDs.Tables[0].Rows.Count; zCont++)
                {
                    if (zDs.Tables[0].Rows[zCont]["Indicador"].ToString().Trim() == "1")
                    {
                        xmlData8.Append(" <set label = 'Não' value = '" + zDs.Tables[0].Rows[zCont]["Valor"].ToString().Trim() + "'  color='#E98442' labelFont='Tahoma' labelFontSize='9' /> ");
                    }
                    else if (zDs.Tables[0].Rows[zCont]["Indicador"].ToString().Trim() == "2")
                    {
                        xmlData8.Append(" <set label = 'Sim' value = '" + zDs.Tables[0].Rows[zCont]["Valor"].ToString().Trim() + "' color='#31978D'  labelFont='Tahoma' labelFontSize='9' /> ");
                    }
                }
            }
            else
            {
                xmlData8.Append(" <set label = 'Não' value = '0'  color='#E98442' labelFont='Tahoma' labelFontSize='9' /> ");
            }



            xmlData8.AppendFormat("</chart>");



            Chart factoryOutput = new Chart("pie2d", "Anamnese1", "390", "390", "xml", xmlData.ToString());
            Chart factoryOutput2 = new Chart("pie2d", "Anamnese2", "390", "390", "xml", xmlData2.ToString());
            Chart factoryOutput3 = new Chart("pie2d", "Anamnese3", "390", "390", "xml", xmlData3.ToString());
            Chart factoryOutput4 = new Chart("pie2d", "Anamnese4", "390", "390", "xml", xmlData4.ToString());
            Chart factoryOutput5 = new Chart("pie2d", "Anamnese5", "390", "390", "xml", xmlData5.ToString());
            Chart factoryOutput6 = new Chart("pie2d", "Anamnese6", "390", "390", "xml", xmlData6.ToString());
            Chart factoryOutput7 = new Chart("pie2d", "Anamnese7", "390", "390", "xml", xmlData7.ToString());
            Chart factoryOutput8 = new Chart("pie2d", "Anamnese8", "390", "390", "xml", xmlData8.ToString());



            // https://forum.fusioncharts.com/topic/445-multiple-charts-on-same-page/
            // Render the chart.
            Literal1.Text = factoryOutput.Render();

            Literal2.Text = factoryOutput2.Render();

            Literal3.Text = factoryOutput3.Render();

            Literal4.Text = factoryOutput4.Render();

            Literal5.Text = factoryOutput5.Render();

            Literal6.Text = factoryOutput6.Render();

            Literal7.Text = factoryOutput7.Render();

            Literal8.Text = factoryOutput8.Render();

            Literal9.Text = "";


        

        }





        private void rGrafico_Laudos()
        {

            StringBuilder xmlData = new StringBuilder();
            short xEmp = 3;

            //xEmp = System.Convert.ToInt16(cmb_Empresa.SelectedValue.ToString().Trim());

            Ilitera.Data.Clientes_Funcionarios xDados = new Ilitera.Data.Clientes_Funcionarios();
            DataSet rDs = xDados.Gerar_DS_Relatorio_Laudos_Grafico(System.Convert.ToInt32(Session["Empresa"].ToString()), "01/01/2017", "01/01/2018", xEmp, "");


            //Int32 zMaxValue = 0; 

            //for (int zCont = 0; zCont < rDs.Tables[0].Rows.Count; zCont++)
            //{
            //    if (System.Convert.ToInt32(rDs.Tables[0].Rows[zCont][4].ToString()) > zMaxValue) zMaxValue = System.Convert.ToInt32(rDs.Tables[0].Rows[zCont][4].ToString());
            //}



            // http://www.fusioncharts.com/dev/basic-chart-configurations/vertical-div-lines.html

            //xmlData.Append("<chart caption='Absenteeism 2016 (Days)' subCaption='TOTAL 11.185,9 days     AVG 932,2 days' showBorder='1' formatNumberScale='0' rotatelabels='1' showvalues='0'>");

            xmlData.Append("<chart caption='Vencimento de Laudos' subCaption='PPRA/PCMSO' showBorder='1' formatNumberScale='0' exportEnabled='1'  ");

            //fonte
            xmlData.Append("  baseFont='Arial' baseFontSize='11' baseFontColor='#000000'  outcnvbasefontsize='11' outcnvBaseFonte='Tamoha' ");

            //margens
            xmlData.Append(" chartLeftMargin='10' chartTopMargin='20'  chartRightMargin='10'  chartBottomMargin='20' ");

            //background color
            //xmlData.Append(" bgColor='#fdf5e6'  canvasBgAlpha='0' bgAlpha='50'  bgratio='60, 40' ");

            //graph color
            xmlData.Append("  palettecolors='#008ee4'  useplotgradientcolor='0'  showplotborder='0'  showShadow='0'  palette='4' ");

            //divisão vertical
            //xmlData.Append(" numVDivLines='10' vDivLineColor='#99ccff' vDivLineThickness='1' vDivLineAlpha='70' vDivLineDashed='1' vDivLineDashLen='5' vDivLineDashGap='3' "); // yAxisMaxValue='" + (zMaxValue+2).ToString() + "' ");

            //logo
            //xmlData.Append(" rotatelabels ='1' showvalues='1'  logoURL='https://www.br.capgemini.com/sites/all/themes/capgemini/logo.png' logoAlpha='40' logoScale='90' logoPosition='TR' > ");
            xmlData.Append(" rotatelabels ='1' showvalues='1' > ");


            //string zColor1 = "#31978D";
            //string zColor2 = "#E98442";
            //string zColor3 = "#b0c4de";


            //for (int zCont = 0; zCont < rDs.Tables[0].Rows.Count; zCont++)
            //{
            //    if (zCont % 3 == 0)
            //        xmlData.AppendFormat("<set label='" + rDs.Tables[0].Rows[zCont][5].ToString() + "' Value='" + System.Convert.ToInt32(rDs.Tables[0].Rows[zCont][6].ToString()) + "' color ='" + zColor1 + "' />");
            //    else if (zCont % 3 == 1)
            //        xmlData.AppendFormat("<set label='" + rDs.Tables[0].Rows[zCont][5].ToString() + "' Value='" + System.Convert.ToInt32(rDs.Tables[0].Rows[zCont][6].ToString()) + "' color ='" + zColor2 + "' />");
            //    else if (zCont % 3 == 2)
            //        xmlData.AppendFormat("<set label='" + rDs.Tables[0].Rows[zCont][5].ToString() + "' Value='" + System.Convert.ToInt32(rDs.Tables[0].Rows[zCont][6].ToString()) + "' color ='" + zColor3 + "' />");
            //}


            xmlData.AppendFormat("<categories>");


            //[3] filial
            //[4] matriz

            for (int zCont = 0; zCont < rDs.Tables[0].Rows.Count; zCont = zCont + 4)
            {
                xmlData.AppendFormat("<category label='{0}'/>", rDs.Tables[0].Rows[zCont][2].ToString());
            }


            xmlData.AppendFormat("</categories>");



            xmlData.AppendFormat("<dataset seriesName='{0}' color='#cca300'>", "PCMSO a Vencer");

            ////valores

            for (int zCont = 0; zCont < rDs.Tables[0].Rows.Count; zCont++)
            {
                if (rDs.Tables[0].Rows[zCont][0].ToString().Trim() == "A Vencer" && rDs.Tables[0].Rows[zCont][1].ToString().Trim() == "PCMSO")
                    xmlData.AppendFormat("<set value='{0}' link='Graficos.aspx?item=pcmsovencer&IdJuridicaPai=" + rDs.Tables[0].Rows[zCont][7].ToString() + "'/>", System.Convert.ToInt32(rDs.Tables[0].Rows[zCont][6].ToString()));

                //               { { 'seriesname': 'Current Year','data': [{'value': '567500' , 'link': 'Grafico2.aspx?item=cy0'}

            }

            xmlData.AppendFormat("</dataset>");



            xmlData.AppendFormat("<dataset seriesName='{0}' color='#ff4d4d'>", "PCMSO Vencidos");

            ////valores

            for (int zCont = 0; zCont < rDs.Tables[0].Rows.Count; zCont++)
            {
                if (rDs.Tables[0].Rows[zCont][0].ToString().Trim() == "Vencidos" && rDs.Tables[0].Rows[zCont][1].ToString().Trim() == "PCMSO")
                    xmlData.AppendFormat("<set value='{0}' link='Graficos.aspx?item=pcmsovencidos&IdJuridicaPai=" + rDs.Tables[0].Rows[zCont][7].ToString() + "'/>", System.Convert.ToInt32(rDs.Tables[0].Rows[zCont][6].ToString()));
            }

            xmlData.AppendFormat("</dataset>");

            xmlData.AppendFormat("<dataset seriesName='{0}' color='#1aaf5d'>", "PPRA a Vencer");

            ////valores

            for (int zCont = 0; zCont < rDs.Tables[0].Rows.Count; zCont++)
            {
                if (rDs.Tables[0].Rows[zCont][0].ToString().Trim() == "A Vencer" && rDs.Tables[0].Rows[zCont][1].ToString().Trim() == "PPRA")
                    xmlData.AppendFormat("<set value='{0}' link='Graficos.aspx?item=ppravencer&IdJuridicaPai=" + rDs.Tables[0].Rows[zCont][7].ToString() + "'/>", System.Convert.ToInt32(rDs.Tables[0].Rows[zCont][6].ToString()));
                //xmlData.AppendFormat("<set value='{0}'/>", System.Convert.ToInt32(rDs.Tables[0].Rows[zCont][6].ToString()));
            }

            xmlData.AppendFormat("</dataset>");

            xmlData.AppendFormat("<dataset seriesName='{0}' color='#0075c2'>", "PPRA Vencidos");

            ////valores

            for (int zCont = 0; zCont < rDs.Tables[0].Rows.Count; zCont++)
            {
                if (rDs.Tables[0].Rows[zCont][0].ToString().Trim() == "Vencidos" && rDs.Tables[0].Rows[zCont][1].ToString().Trim() == "PPRA")
                    xmlData.AppendFormat("<set value='{0}' link='Graficos.aspx?item=ppravencidos&IdJuridicaPai=" + rDs.Tables[0].Rows[zCont][7].ToString() + "'/>", System.Convert.ToInt32(rDs.Tables[0].Rows[zCont][6].ToString()));
                //xmlData.AppendFormat("<set value='{0}'/>", System.Convert.ToInt32(rDs.Tables[0].Rows[zCont][6].ToString()));
            }

            xmlData.AppendFormat("</dataset>");

            //linha de média
            //xmlData.AppendFormat(" <trendlines>< isTrendZone='1' startvalue='900' endValue='950' color='#1aaf5d' valueonright='1' tooltext='AVG 932 days'  /></trendlines> ");

            xmlData.AppendFormat("</chart>");


            // Initialize the chart.
            //Chart factoryOutput = new Chart("mscolumn2d", "Laudos", "1190", "600", "xml", xmlData.ToString());
            Chart factoryOutput = new Chart("msbar2d", "Laudos", "1190", "600", "xml", xmlData.ToString());



            // Render the chart.
            Literal1.Text = factoryOutput.Render();
            Literal2.Text = "";
            Literal3.Text = "";
            Literal4.Text = "";
            Literal5.Text = "";
            Literal6.Text = "";
            Literal7.Text = "";
            Literal8.Text = "";
            Literal9.Text = "";



        }






        private void SubGrafico_Laudos(string zTipo, Int32 zIdPai)
        {

            StringBuilder xmlData = new StringBuilder();
            short xEmp = 1;

            xEmp = System.Convert.ToInt16(cmb_Empresa.SelectedValue.ToString().Trim());


            Ilitera.Data.Clientes_Funcionarios xDados = new Ilitera.Data.Clientes_Funcionarios();
            DataSet rDs = xDados.Gerar_DS_Relatorio_Laudos_SubGrafico(System.Convert.ToInt32(Session["Empresa"].ToString()), "01/01/2017", "01/01/2018", xEmp, zTipo, zIdPai);





            xmlData.Append("<chart caption='Vencimento de Laudos' subCaption='PPRA/PCMSO' showBorder='1' formatNumberScale='0' exportEnabled='1'  ");

            //fonte
            xmlData.Append("  baseFont='Arial' baseFontSize='11' baseFontColor='#000000'  outcnvbasefontsize='11' outcnvBaseFonte='Tamoha' ");

            //margens
            xmlData.Append(" chartLeftMargin='10' chartTopMargin='20'  chartRightMargin='10'  chartBottomMargin='20' ");

            //background color
            //xmlData.Append(" bgColor='#fdf5e6'  canvasBgAlpha='0' bgAlpha='50'  bgratio='60, 40' ");

            //graph color
            xmlData.Append("  palettecolors='#008ee4'  useplotgradientcolor='0'  showplotborder='0'  showShadow='0'  palette='4' ");

            //divisão vertical
            //xmlData.Append(" numVDivLines='10' vDivLineColor='#99ccff' vDivLineThickness='1' vDivLineAlpha='70' vDivLineDashed='1' vDivLineDashLen='5' vDivLineDashGap='3' "); // yAxisMaxValue='" + (zMaxValue+2).ToString() + "' ");

            //logo
            //xmlData.Append(" rotatelabels ='1' showvalues='1'  logoURL='https://www.br.capgemini.com/sites/all/themes/capgemini/logo.png' logoAlpha='40' logoScale='90' logoPosition='TR' > ");
            xmlData.Append(" rotatelabels ='1' showvalues='1' > ");




            xmlData.AppendFormat("<categories>");




            for (int zCont = 0; zCont < rDs.Tables[0].Rows.Count; zCont++)
            {
                xmlData.AppendFormat("<category label='{0}'/>", rDs.Tables[0].Rows[zCont][2].ToString());
            }


            xmlData.AppendFormat("</categories>");


            if (zTipo == "pcmsovencer")
                xmlData.AppendFormat("<dataset seriesName='{0}' color='#cca300'>", "PCMSO a Vencer");
            else if (zTipo == "pcmsovencidos")
                xmlData.AppendFormat("<dataset seriesName='{0}' color='#ff4d4d'>", "PCMSO Vencidos");
            else if (zTipo == "ppravencer")
                xmlData.AppendFormat("<dataset seriesName='{0}' color='#1aaf5d'>", "PPRA a Vencer");
            else if (zTipo == "ppravencidos")
                xmlData.AppendFormat("<dataset seriesName='{0}' color='#0075c2'>", "PPRA Vencidos");


            ////valores

            for (int zCont = 0; zCont < rDs.Tables[0].Rows.Count; zCont++)
            {
                xmlData.AppendFormat("<set value='{0}' link='Graficos.aspx?item=laudo'/>", System.Convert.ToInt32(rDs.Tables[0].Rows[zCont][6].ToString()));
                //               { { 'seriesname': 'Current Year','data': [{'value': '567500' , 'link': 'Grafico2.aspx?item=cy0'}

            }

            xmlData.AppendFormat("</dataset>");


            xmlData.AppendFormat("</chart>");


            // Initialize the chart.
            //Chart factoryOutput = new Chart("mscolumn2d", "Laudos", "1190", "600", "xml", xmlData.ToString());
            Chart factoryOutput = new Chart("msbar2d", "Laudos", "1190", "600", "xml", xmlData.ToString());

            // Render the chart.
            Literal1.Text = factoryOutput.Render("msbar2d", "Laudos", "1190", "600", "xml", xmlData.ToString());
            Literal2.Text = "";
            Literal3.Text = "";
            Literal4.Text = "";
            Literal5.Text = "";
            Literal6.Text = "";
            Literal7.Text = "";
            Literal8.Text = "";
            Literal9.Text = "";


        }


        protected void rd_Considerar_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (rd_Considerar.SelectedIndex != 0)
            {
                txt_Data.Enabled = false;
                txt_Data2.Enabled = false;
                cmb_Empresa.Enabled = false;
                cmb_Anamneses.Enabled = false;
                cmb_Anamnese_Dados.Enabled = false;
            }
            else 
            {
                txt_Data.Enabled = true;
                txt_Data2.Enabled = true;
                cmb_Anamneses.Enabled = true;
                cmb_Empresa.Enabled = true;
                cmb_Anamnese_Dados.Enabled = true;
            }
            //else
            //{
            //    txt_Data.Enabled = true;
            //    txt_Data2.Enabled = true;
            //    cmb_Empresa.Enabled = true;
            //    cmb_Anamneses.Enabled = false;
            //    cmb_Anamnese_Dados.Enabled = false;
            //}

        }
    }

}
