<%@ Page Title="Ilitera.Net" Language="C#" AutoEventWireup="True"    CodeBehind="Grafico1.aspx.cs" Inherits="Ilitera.Net.Grafico1" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Multi series demo in xml data format</title>
     <script type="text/javascript" src="http://static.fusioncharts.com/code/latest/fusioncharts.js?cacheBust=823"></script>
	<script type="text/javascript" src="http://static.fusioncharts.com/code/latest/fusioncharts.charts.js?cacheBust=823"></script>
	<script type="text/javascript" src="http://static.fusioncharts.com/code/latest/themes/fusioncharts.theme.fint.js?cacheBust=823"></script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Literal ID="Literal1" runat="server"></asp:Literal>
    </div>
    </form>
</body>
</html>

<%--  <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
  <html>
      <head>
          <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
          <title>FusionCharts - Simple</title>
          <!-- FusionCharts script tag -->
          <script type="text/javascript" src="../fusioncharts/fusioncharts.js"></script>
          <script type="text/javascript" src="fusioncharts/js/themes/fusioncharts.theme.fint.js"></script>
          <!-- End -->
      </head>
      <body>
          <div style="text-align:center">
              <asp:Literal ID="Literal1" runat="server"></asp:Literal>
          </div>
      </body>
  </html>--%>



<%--<html>
<head>
<title>My first chart using FusionCharts Suite XT</title>
<script type="text/javascript" src="fusioncharts/js/fusioncharts.js"></script>
<script type="text/javascript" src="fusioncharts/js/themes/fusioncharts.theme.fint.js"></script>
<script type="text/javascript">
    FusionCharts['debugger'].outputTo(function (id, sender,
        eventName, eventArgs) {
        console.log(id + ': '+ eventName + ' from ' + sender,
        eventArgs);
    });
    FusionCharts['debugger'].outputFormat('verbose');
    FusionCharts['debugger'].enable(true);

  FusionCharts.ready(function(){
    var revenueChart = new FusionCharts({
        "type": "column2d",
        "renderAt": "chartContainer",
        "width": "500",
        "height": "300",
        "dataFormat": "json",
        "dataSource":  {
          "chart": {
            "caption": "Monthly revenue for last year",
            "subCaption": "Harry's SuperMart",
            "xAxisName": "Month",
            "yAxisName": "Revenues (In USD)",
            "theme": "fint"
         },
         "data": [
            {
               "label": "Jan",
               "value": "420000"
            },
            {
               "label": "Feb",
               "value": "810000"
            },
            {
               "label": "Mar",
               "value": "720000"
            },
            {
               "label": "Apr",
               "value": "550000"
            },
            {
               "label": "May",
               "value": "910000"
            },
            {
               "label": "Jun",
               "value": "510000"
            },
            {
               "label": "Jul",
               "value": "680000"
            },
            {
               "label": "Aug",
               "value": "620000"
            },
            {
               "label": "Sep",
               "value": "610000"
            },
            {
               "label": "Oct",
               "value": "490000"
            },
            {
               "label": "Nov",
               "value": "900000"
            },
            {
               "label": "Dec",
               "value": "730000"
            }
          ]
      }

  });
revenueChart.render();
})
</script>
</head>


<body>
  <div id="chartContainer">FusionCharts XT will load here!</div>
</body>
</html>
--%>
