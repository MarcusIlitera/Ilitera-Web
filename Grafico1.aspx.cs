using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

// Use the `FusionCharts.Charts` namespace to be able to use classes and methods required to // create charts.
using FusionCharts.Charts;

using System.Text;
using System.Data.Odbc;
//using DataConnection;
using System.Data.OleDb;

namespace Ilitera.Net
{
    /// <summary>
	/// Summary description for ListagemEPI.
	/// </summary>
	public partial class Grafico1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string xUsuario = Session["usuarioLogado"].ToString();
            rGrafico2();
        }

        private void rGrafico1()
        { 
            // Create the `xmlData` StringBuilder object to store the data fetched
            //from the database as a string.
            StringBuilder xmlData = new StringBuilder();

            // Initialize the chart-level attributes and append them to the
            //`xmlData` StringBuilder object.

            xmlData.Append("<chart caption='Factory Output report' subCaption='By Quantity' showBorder='1' formatNumberScale='0' rotatelabels='1' showvalues='0'>");

            // Initialize the `<categories>` element.
            xmlData.AppendFormat("<categories>");

            // Every date between January 01, 2003 and January 20, 2003 is entered thrice
            // in the **datepro** field in the **FactoryDB **database.

            // The dates will be shown as category labels on the x-axis of the chart.

            // Because we need to show each date only once, use the `select` query
            // with the `distinct` keyword to fetch only one instance of each date from the database.

            // Store the output of the `select` query in the `factoryQuery` string variable.

            ////string factoryQuery = "select distinct format(datepro,'dd/mm/yyyy') as dd from factory_output";

            // Establish the database connection.


            ///// DbConn oRs = new DbConn(factoryQuery);

            // Iterate through the data in the `factoryQuery` variable and add the dates as

            // labels to the `<category>` element.

            // Append this data to the `xmlData` object.
            ///// while (oRs.ReadData.Read())
            ///// {
            ////     xmlData.AppendFormat("<category label='{0}'/>", oRs.ReadData["dd"].ToString());
            /////  }

            xmlData.AppendFormat("<category label='{0}'/>", "01/06/2015");
            xmlData.AppendFormat("<category label='{0}'/>", "01/07/2015");

            //Close the database connection.
            /////oRs.ReadData.Close();

            //Close the `<catgories>` element.
            xmlData.AppendFormat("</categories>");

            //Fetch all details for the three factories from the **Factory_Master** table
            // and store the result in the `factoryquery2` variable.

            ////string factoryquery2 = "select * from factory_master";

            //Establish the database connection..
            ////DbConn oRs1 = new DbConn(factoryquery2);

            // Iterate through the results in the `factoryquery2` variable to fetch the
            // factory name and factory id.

            ////while (oRs1.ReadData.Read())
            ////{
            // Append the factory name as the value for the `seriesName` attribute.
            ////xmlData.AppendFormat("<dataset seriesName='{0}'>", oRs1.ReadData["factoryname"].ToString());
            xmlData.AppendFormat("<dataset seriesName='{0}'>", "Fabrica 1");

            // Based on the factory id, fetch the quantity produced by each factory on each day
            // from the factory_output table.

            // Store the results in the `factoryquery3` string object.

            //string factoryquery3 = "select quantity from factory_output where factoryid=" + oRs1.ReadData["factoryid"].ToString();

            //Establish the database connection.
            //// DbConn oRs2 = new DbConn(factoryquery3);

            // Iterate through the results in the `factoryquery3` object and fetch the quantity details
            // for each factory.

            // Append the quantity details as the the value for the `<set>` element.

            //// while (oRs2.ReadData.Read())
            //// {
            ////  xmlData.AppendFormat("<set value='{0}'/>", oRs2.ReadData[0].ToString());
            //// }
            xmlData.AppendFormat("<set value='{0}'/>", "215");
            xmlData.AppendFormat("<set value='{0}'/>", "199");


            // Close the database connection.
            ////oRs2.ReadData.Close();

            // Close the `<dataset>` element.
            xmlData.AppendFormat("</dataset>");
            ////}


            xmlData.AppendFormat("<dataset seriesName='{0}'>", "Fabrica 2");
            xmlData.AppendFormat("<set value='{0}'/>", "315");
            xmlData.AppendFormat("<set value='{0}'/>", "115");
            xmlData.AppendFormat("</dataset>");

            // Close the database connection.
            ////oRs1.ReadData.Close();

            // Close the `<chart>` element.
            xmlData.AppendFormat("</chart>");

                                                                       // Initialize the chart.
            Chart factoryOutput = new Chart("msline", "myChart", "600", "350", "xml", xmlData.ToString());

            // Render the chart.
                Literal1.Text = factoryOutput.Render();
            
        }


        private void rGrafico2()
        {
            
            StringBuilder xmlData = new StringBuilder();


            // http://www.fusioncharts.com/dev/basic-chart-configurations/vertical-div-lines.html

            //xmlData.Append("<chart caption='Absenteeism 2016 (Days)' subCaption='TOTAL 11.185,9 days     AVG 932,2 days' showBorder='1' formatNumberScale='0' rotatelabels='1' showvalues='0'>");

            xmlData.Append("<chart caption='Delivery Center - Nº DE HORAS PERDIDAS' showBorder='1' formatNumberScale='0' exportEnabled='1' ");

            //fonte
            xmlData.Append("  baseFont='Verdana' baseFontSize='13' baseFontColor='#263238' ");

            //margens
            xmlData.Append(" chartLeftMargin='40' chartTopMargin='40'  chartRightMargin='40'  chartBottomMargin='40' ");

            //background color
            //xmlData.Append(" bgColor='#c0eef5'  canvasBgAlpha='0' bgAlpha='50'  bgratio='60, 40' ");

            //graph color
            xmlData.Append("  palettecolors='#008ee4'  useplotgradientcolor='0'  showplotborder='0'  showShadow='0'  palette='4' ");

            //divisão vertical
            xmlData.Append(" numVDivLines='10' vDivLineColor='#99ccff' vDivLineThickness='1' vDivLineAlpha='70' vDivLineDashed='1' vDivLineDashLen='5' vDivLineDashGap='3' yAxisMaxValue='2000' ");

            //logo
            xmlData.Append(" rotatelabels ='1' showvalues='1'  logoURL='https://www.br.capgemini.com/sites/all/themes/capgemini/logo.png' logoAlpha='40' logoScale='90' logoPosition='TR' > ");


            xmlData.AppendFormat("<categories>");


           
            xmlData.AppendFormat("<category label='{0}'/>", "jan/2016");
            xmlData.AppendFormat("<category label='{0}'/>", "feb/2016");
            xmlData.AppendFormat("<category label='{0}'/>", "mar/2016");
            xmlData.AppendFormat("<category label='{0}'/>", "apr/2016");
            xmlData.AppendFormat("<category label='{0}'/>", "may/2016");
            xmlData.AppendFormat("<category label='{0}'/>", "jun/2016");
            xmlData.AppendFormat("<category label='{0}'/>", "jul/2016");
            xmlData.AppendFormat("<category label='{0}'/>", "aug/2016");
            xmlData.AppendFormat("<category label='{0}'/>", "sep/2016");
            xmlData.AppendFormat("<category label='{0}'/>", "oct/2016");
            xmlData.AppendFormat("<category label='{0}'/>", "nov/2016");
            xmlData.AppendFormat("<category label='{0}'/>", "dec/2016");


            xmlData.AppendFormat("</categories>");

           
            xmlData.AppendFormat("<dataset seriesName='{0}'>", "Absense Days");

          
            xmlData.AppendFormat("<set value='{0}'/>", "864");
            xmlData.AppendFormat("<set value='{0}'/>", "1002");
            xmlData.AppendFormat("<set value='{0}'/>", "1551");
            xmlData.AppendFormat("<set value='{0}'/>", "953");
            xmlData.AppendFormat("<set value='{0}'/>", "722");
            xmlData.AppendFormat("<set value='{0}'/>", "904");
            xmlData.AppendFormat("<set value='{0}'/>", "964");
            xmlData.AppendFormat("<set value='{0}'/>", "1148");
            xmlData.AppendFormat("<set value='{0}'/>", "877");
            xmlData.AppendFormat("<set value='{0}'/>", "876");
            xmlData.AppendFormat("<set value='{0}'/>", "725");
            xmlData.AppendFormat("<set value='{0}'/>", "595");


            xmlData.AppendFormat("</dataset>");


            //linha de média
            //xmlData.AppendFormat(" <trendlines>< isTrendZone='1' startvalue='900' endValue='950' color='#1aaf5d' valueonright='1' tooltext='AVG 932 days'  /></trendlines> ");


            xmlData.AppendFormat("</chart>");





            // Initialize the chart.
            Chart factoryOutput = new Chart("msline", "myChart", "900", "700", "xml", xmlData.ToString());

            // Render the chart.
            Literal1.Text = factoryOutput.Render();

        }


    }
}
