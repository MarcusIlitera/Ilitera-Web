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

using System.Web.Script.Serialization;

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
	public partial class Grafico2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            string xUsuario = Session["usuarioLogado"].ToString();
            string xTipo;

            if (Request["item"] == null)
            {
                xTipo = "0";
            }
            else
            {
                xTipo = Request["item"];
            }



            if (xTipo == "0")
                Grafico_Principal();
            else
                SubGrafico(xTipo);

        }

        private void Grafico_Principal()
        { 
            
            object[,] arrData = new object[6, 3];

            //Store product labels in the first column.
            arrData[0, 0] = "Product A";
            arrData[1, 0] = "Product B";
            arrData[2, 0] = "Product C";
            arrData[3, 0] = "Product D";
            arrData[4, 0] = "Product E";
            arrData[5, 0] = "Product F";

            //Store sales data for the current year in the second column.
            arrData[0, 1] = 567500;
            arrData[1, 1] = 815300;
            arrData[2, 1] = 556800;
            arrData[3, 1] = 734500;
            arrData[4, 1] = 676800;
            arrData[5, 1] = 648500;

            //Store sales data for previous year in the third column.
            arrData[0, 2] = 367300;
            arrData[1, 2] = 584500;
            arrData[2, 2] = 754000;
            arrData[3, 2] = 456300;
            arrData[4, 2] = 754500;
            arrData[5, 2] = 437600;



            //create arquivo json via dll
            //var personlist = arrData; //db.People.ToList();

            //string jsondata = new JavaScriptSerializer().Serialize(personlist);
            //string path = Server.MapPath("~/");

            //System.IO.File.WriteAllText(path + "output.json", jsondata);



            // To render a chart from the above data, you will have to convert this data into

            // JSON data for the chart.

            // You can do this using string concatenation.

            //Create objects of the `StringBuilder` class to store the converted JSON strings.

            // Define the `jsonData` object to store the entire chart data as a JSON string.

            StringBuilder jsonData = new StringBuilder();

            // Define the `categories` object to store the product labels converted to
            // JSON strings.
            StringBuilder categories = new StringBuilder();

            //Define the `currentYear` and `previousYear` objects to store
            // the converted current and previous years sales data, respectively.
            StringBuilder currentYear = new StringBuilder();
            StringBuilder previousYear = new StringBuilder();
            

            //Initialize the chart object. Define  the chart-level attributes and
            // append them as strings to the chart data in the `jsonData` object
            // using the `Append` method.

            jsonData.Append("{" +

                //Initialize the chart object with the chart-level attributes..
                "'chart': {" +

                    "'caption': 'Sales by Product'," +
                    "'numberPrefix': '$'," +
                    "'formatNumberScale': '1'," +
                    "'placeValuesInside': '1'," +
                    "'decimals': '0'," +
                    "'exportEnabled':'1' " +
                "},");

            //Initialize the `categories` and `category` object arrays.

            //Using the `Append ` method, append the initial part of array definition as

            // string to the `categories` StringBuilder object.
            categories.Append("'categories': [" +
                "{" +
                    "'category': [");

            //Using the `Append` method, append the dataset level attributes and the initial part of
            // the `data` object array definition to the
            // `currentYear` StringBuilder object.

            currentYear.Append("{" +
                        // dataset level attributes
                        "'seriesname': 'Current Year'," +
                        "'data': [");

            //Using the `Append` method, append the dataset level attributes and the initial part of

            // the `data` object array definition to the

            // `previousYear` StringBuilder object.

            previousYear.Append("{" +
                        // dataset level attributes
                        "'seriesname': 'Previous Year'," +
                        "'data': [");

            //Iterate through the data contained  in the `arrData` array.

            for (int i = 0; i < arrData.GetLength(0); i++)
            {
                if (i > 0)
                {
                    categories.Append(",");
                    currentYear.Append(",");
                    previousYear.Append(",");
                    //sLink.Append(",");
                }

                //Append individual category-level data to the `categories` object.

                categories.AppendFormat("{{" +
                        // category level attributes
                        "'label': '{0}'" +
                    "}}", arrData[i, 0]);

                //Append current year’s sales data for each product to the `currentYear` object.

                //drill down funcionará de forma recursiva, chamando essa própria página, mas com um parâmetro

                currentYear.AppendFormat("{{" +
                        // data level attributes
                        "'value': '{0}' , 'link': '{1}'" +
                    "}}", arrData[i, 1], "Grafico2.aspx?item=cy" + i.ToString().Trim());

                //Append previous year’s sales data for each product to the `currentYear` object.

                previousYear.AppendFormat("{{" +
                        // data level attributes
                        "'value': '{0}' , 'link': '{1}'" +
                      "}}", arrData[i, 2], "Grafico2.aspx?item=py" + i.ToString().Trim());

              
            }

            //Append as strings the closing part of the array definition of the

            // `categories` object array.

            categories.Append("]" +
                    "}" +
                "],");

            //Append as strings the closing part of the array definition of the `data` object array to the `currentYear` and `previousYear` objects.

            currentYear.Append("]" +
                    "},");
            previousYear.Append("]" +
                    "}");

            //Append the complete chart data converted to a string to the `jsonData` object.

            jsonData.Append(categories.ToString());
            jsonData.Append("'dataset': [");
            jsonData.Append(currentYear.ToString());
            jsonData.Append(previousYear.ToString());

           // jsonData.Append(sLink.ToString());

            jsonData.Append("]" +
                    "}");

            // Initialize the chart.

            //Chart sales = new Chart("msline", "myChart", "600", "350", "json", jsonData.ToString());
            Chart sales = new Chart("column2d", "myChart", "600", "350", "json", jsonData.ToString());
            // Render the chart.
            Literal1.Text = sales.Render();
        }




        private void SubGrafico( string xTipo )
        {

            object[,] arrData = new object[6, 3];

            //Store product labels in the first column.
            arrData[0, 0] = "Month 1";
            arrData[1, 0] = "Month 1";
            arrData[2, 0] = "Month 1";
            arrData[3, 0] = "Month 1";

            //Store sales data for the current year in the second column.
            arrData[0, 1] = 120000;
            arrData[1, 1] = 115300;
            arrData[2, 1] = 156800;
            arrData[3, 1] = 134500;

          
                   

            StringBuilder jsonData = new StringBuilder();

            // Define the `categories` object to store the product labels converted to
            // JSON strings.
            StringBuilder categories = new StringBuilder();

            //Define the `currentYear` and `previousYear` objects to store
            // the converted current and previous years sales data, respectively.
            StringBuilder currentYear = new StringBuilder();
            


            //Initialize the chart object. Define  the chart-level attributes and
            // append them as strings to the chart data in the `jsonData` object
            // using the `Append` method.

            jsonData.Append("{" +

                //Initialize the chart object with the chart-level attributes..
                "'chart': {" +

                    "'caption': 'Sales by Product'," +
                    "'numberPrefix': '$'," +
                    "'formatNumberScale': '1'," +
                    "'placeValuesInside': '1'," +
                    "'decimals': '0'," +
                    "'exportEnabled':'1' " +
                "},");

            //Initialize the `categories` and `category` object arrays.

            //Using the `Append ` method, append the initial part of array definition as

            // string to the `categories` StringBuilder object.
            categories.Append("'categories': [" +
                "{" +
                    "'category': [");

            //Using the `Append` method, append the dataset level attributes and the initial part of
            // the `data` object array definition to the
            // `currentYear` StringBuilder object.

            currentYear.Append("{" +
                        // dataset level attributes
                        "'seriesname': 'Current Year'," +
                        "'data': [");

            //Using the `Append` method, append the dataset level attributes and the initial part of

            // the `data` object array definition to the

    

            //Iterate through the data contained  in the `arrData` array.

            for (int i = 0; i < arrData.GetLength(0); i++)
            {
                if (i > 0)
                {
                    categories.Append(",");
                    currentYear.Append(",");
                    
                }

                //Append individual category-level data to the `categories` object.

                categories.AppendFormat("{{" +
                        // category level attributes
                        "'label': '{0}'" +
                    "}}", arrData[i, 0]);

                //Append current year’s sales data for each product to the `currentYear` object.

                //drill down funcionará de forma recursiva, chamando essa própria página, mas com um parâmetro

                currentYear.AppendFormat("{{" +
                        // data level attributes
                        "'value': '{0}' , 'link': '{1}'" +
                    "}}", arrData[i, 1], "Grafico2.aspx");





            }

            //Append as strings the closing part of the array definition of the

            // `categories` object array.

            categories.Append("]" +
                    "}" +
                "],");

            //Append as strings the closing part of the array definition of the `data` object array to the `currentYear` and `previousYear` objects.

            currentYear.Append("]" +
                    "},");
          
            //Append the complete chart data converted to a string to the `jsonData` object.

            jsonData.Append(categories.ToString());
            jsonData.Append("'dataset': [");
            jsonData.Append(currentYear.ToString());
          

            // jsonData.Append(sLink.ToString());

            jsonData.Append("]" +
                    "}");

            // Initialize the chart.

            //Chart sales = new Chart("msline", "myChart", "600", "350", "json", jsonData.ToString());
            Chart sales = new Chart("column2d", "myChart", "600", "350", "json", jsonData.ToString());
            // Render the chart.
            Literal1.Text = sales.Render();
        }

    }
}
