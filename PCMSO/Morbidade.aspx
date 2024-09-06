<%@ Page Language="C#"   AutoEventWireup="True" CodeBehind="Morbidade.aspx.cs"  Inherits="Ilitera.Net.PCMSO.Morbidade" Title="Ilitera.Net" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Mestra.NET</title>
    <script id="igClientScript" type="text/javascript">
    function UltraWebGridEmpregadoFatorRisco_MouseOverHandler(gridName, id, objectType)
    {
        if (objectType == 0)
	    {
	        var cell = igtbl_getCellById(id);	        	        
	        var row = cell.getRow();
	        var cells = row.getCellElements();
	        
	        for (var i = 0; i < cells.length; i++)
	            cells[i].style.backgroundColor="fefcab";
	    }
    }

    function UltraWebGridEmpregadoFatorRisco_MouseOutHandler(gridName, id, objectType)
    {
	    if (objectType == 0)
	    {
	        var cell = igtbl_getCellById(id);	        	        
	        var row = cell.getRow();
	        var cells = row.getCellElements();
	        
	        for (var i = 0; i < cells.length; i++)
	            cells[i].style.backgroundColor="#FCFEFD";
	    }
    }
    
    function warpMorbidade_InitializePanel(oPanel)
    {
        oPanel.getProgressIndicator().setImageUrl("img/loading.gif");
    }
    </script>
</head>
<body bottommargin="0" leftmargin="0" rightmargin="0" topmargin="0">
    <form id="form1" runat="server">
        <table align="center" border="0" cellpadding="0" cellspacing="0" width="580" class="defaultFont">
            <tr>
                <td align="center">
                    <br />
                    <asp:Label ID="lblTitulo" runat="server" SkinID="TitleFont"></asp:Label><br />
                    <br />
                    <igmisc:WebAsyncRefreshPanel ID="warpMorbidade" runat="server" CssClass="defaultFont"
                        Height="" HorizontalAlign="Center" InitializePanel="warpMorbidade_InitializePanel"
                        Width="580px">
                    <igtab:ultrawebtab id="UltraWebTabMorbidade" runat="server" bordercolor="#949878"
                        borderstyle="Solid" borderwidth="1px" cssclass="defaultFont" height="345px" threedeffect="False"
                        width="580px"><Tabs>
<igtab:Tab Tooltip="Panorama Geral" Key="PanoramaGeral" Text="Panorama Geral"><ContentTemplate>
<TABLE class="defaultFont" cellSpacing=0 cellPadding=0 width=570 align=center border=0><TBODY><TR><TD align=center><igtab:UltraWebTab id="UltraWebTabPanoramaGeral" runat="server" BorderColor="#949878" BorderStyle="Solid" ThreeDEffect="False" BorderWidth="1px" Width="560px" Height="305px" CssClass="defaultFont"><Tabs>
<igtab:Tab Tooltip="Morbidade na Empresa" Key="MorbidadeEmpresa" Text="Morbidade na Empresa">
    <ContentTemplate>
        <table align="center" border="0" cellpadding="0" cellspacing="0" class="defaultFont"
            width="550">
            <tr>
                <td>
                    <table border="0" cellpadding="0" cellspacing="0" class="defaultFont" width="550">
                        <tr>
                            <td width="280">
                    <igchart:UltraChart ID="UltraChartMorbidadeEmpresa" runat="server" ChartType="PieChart3D" Transform3D-XRotation="240" BackColor="#FEFCFD" Height="265px" Transform3D-Scale="75" Width="280px" EmptyChartText="">
                        <Axis>
                            <PE ElementType="None" Fill="Cornsilk" />
                            <X LineThickness="1" TickmarkInterval="0" TickmarkStyle="Smart" Visible="False">
                                <MajorGridLines AlphaLevel="255" Color="Gainsboro" DrawStyle="Dot" Thickness="1"
                                    Visible="True" />
                                <MinorGridLines AlphaLevel="255" Color="LightGray" DrawStyle="Dot" Thickness="1"
                                    Visible="False" />
                                <Labels Font="Verdana, 7pt" FontColor="DimGray" HorizontalAlign="Near" ItemFormatString="&lt;ITEM_LABEL&gt;"
                                    Orientation="Horizontal" VerticalAlign="Center">
                                    <SeriesLabels Font="Verdana, 7pt" FontColor="DimGray" HorizontalAlign="Near" Orientation="Horizontal"
                                        VerticalAlign="Center" FormatString="">
                                        <Layout Behavior="Auto">
                                        </Layout>
                                    </SeriesLabels>
                                    <Layout Behavior="Auto">
                                    </Layout>
                                </Labels>
                            </X>
                            <Y LineThickness="1" TickmarkInterval="20" TickmarkStyle="Smart" Visible="False">
                                <MajorGridLines AlphaLevel="255" Color="Gainsboro" DrawStyle="Dot" Thickness="1"
                                    Visible="True" />
                                <MinorGridLines AlphaLevel="255" Color="LightGray" DrawStyle="Dot" Thickness="1"
                                    Visible="False" />
                                <Labels Font="Verdana, 7pt" FontColor="DimGray" HorizontalAlign="Far" ItemFormatString="&lt;DATA_VALUE:00.##&gt;"
                                    Orientation="Horizontal" VerticalAlign="Center">
                                    <SeriesLabels Font="Verdana, 7pt" FontColor="DimGray" HorizontalAlign="Far" Orientation="Horizontal"
                                        VerticalAlign="Center" FormatString="">
                                        <Layout Behavior="Auto">
                                        </Layout>
                                    </SeriesLabels>
                                    <Layout Behavior="Auto">
                                    </Layout>
                                </Labels>
                            </Y>
                            <Y2 LineThickness="1" TickmarkInterval="20" TickmarkStyle="Smart" Visible="False">
                                <MajorGridLines AlphaLevel="255" Color="Gainsboro" DrawStyle="Dot" Thickness="1"
                                    Visible="True" />
                                <MinorGridLines AlphaLevel="255" Color="LightGray" DrawStyle="Dot" Thickness="1"
                                    Visible="False" />
                                <Labels Font="Verdana, 7pt" FontColor="Gray" HorizontalAlign="Near" ItemFormatString="&lt;DATA_VALUE:00.##&gt;"
                                    Orientation="Horizontal" VerticalAlign="Center">
                                    <SeriesLabels Font="Verdana, 7pt" FontColor="Gray" HorizontalAlign="Near" Orientation="Horizontal"
                                        VerticalAlign="Center" FormatString="">
                                        <Layout Behavior="Auto">
                                        </Layout>
                                    </SeriesLabels>
                                    <Layout Behavior="Auto">
                                    </Layout>
                                </Labels>
                            </Y2>
                            <X2 LineThickness="1" TickmarkInterval="0" TickmarkStyle="Smart" Visible="False">
                                <MajorGridLines AlphaLevel="255" Color="Gainsboro" DrawStyle="Dot" Thickness="1"
                                    Visible="True" />
                                <MinorGridLines AlphaLevel="255" Color="LightGray" DrawStyle="Dot" Thickness="1"
                                    Visible="False" />
                                <Labels Font="Verdana, 7pt" FontColor="Gray" HorizontalAlign="Far" ItemFormatString="&lt;ITEM_LABEL&gt;"
                                    Orientation="Horizontal" VerticalAlign="Center">
                                    <SeriesLabels Font="Verdana, 7pt" FontColor="Gray" HorizontalAlign="Far" Orientation="Horizontal"
                                        VerticalAlign="Center" FormatString="">
                                        <Layout Behavior="Auto">
                                        </Layout>
                                    </SeriesLabels>
                                    <Layout Behavior="Auto">
                                    </Layout>
                                </Labels>
                            </X2>
                            <Z LineThickness="1" TickmarkInterval="0" TickmarkStyle="Smart" Visible="False">
                                <MajorGridLines AlphaLevel="255" Color="Gainsboro" DrawStyle="Dot" Thickness="1"
                                    Visible="True" />
                                <MinorGridLines AlphaLevel="255" Color="LightGray" DrawStyle="Dot" Thickness="1"
                                    Visible="False" />
                                <Labels Font="Verdana, 7pt" FontColor="DimGray" HorizontalAlign="Far" ItemFormatString=""
                                    Orientation="Horizontal" VerticalAlign="Center">
                                    <SeriesLabels Font="Verdana, 7pt" FontColor="DimGray" HorizontalAlign="Far" Orientation="Horizontal"
                                        VerticalAlign="Center">
                                        <Layout Behavior="Auto">
                                        </Layout>
                                    </SeriesLabels>
                                    <Layout Behavior="Auto">
                                    </Layout>
                                </Labels>
                            </Z>
                            <Z2 LineThickness="1" TickmarkInterval="0" TickmarkStyle="Smart" Visible="False">
                                <MajorGridLines AlphaLevel="255" Color="Gainsboro" DrawStyle="Dot" Thickness="1"
                                    Visible="True" />
                                <MinorGridLines AlphaLevel="255" Color="LightGray" DrawStyle="Dot" Thickness="1"
                                    Visible="False" />
                                <Labels Font="Verdana, 7pt" FontColor="Gray" HorizontalAlign="Near" ItemFormatString=""
                                    Orientation="Horizontal" VerticalAlign="Center">
                                    <SeriesLabels Font="Verdana, 7pt" FontColor="Gray" HorizontalAlign="Near" Orientation="Horizontal"
                                        VerticalAlign="Center">
                                        <Layout Behavior="Auto">
                                        </Layout>
                                    </SeriesLabels>
                                    <Layout Behavior="Auto">
                                    </Layout>
                                </Labels>
                            </Z2>
                        </Axis><TitleTop Visible="False">
                        </TitleTop>
                        <Border Thickness="0" />
                        <Tooltips BackColor="LightYellow" />
                        <PieChart3D OthersCategoryPercent="0">
                            <Labels FillColor="LightYellow" />
                        </PieChart3D>
                        <Effects>
                            <Effects>
                                <igchartprop:GradientEffect>
                                </igchartprop:GradientEffect>
                            </Effects>
                        </Effects>
                        <Legend BorderColor="148, 152, 120" Font="Verdana, 7.75pt, style=Bold" Location="Bottom"
                            Visible="True" FontColor="68, 146, 109"></Legend>
                        <TitleBottom Visible="False" Extent="33" Location="Bottom">
                        </TitleBottom>
                        <ColorModel AlphaLevel="255" ModelStyle="LinearRange" ColorBegin="Green" ColorEnd="192, 255, 192">
                        </ColorModel>
                        <DeploymentScenario ImageURL="ChartImages/Chart_#SEQNUM(10000).png" />
                    </igchart:UltraChart>
                            </td>
                            <td width="15">
                            </td>
                            <td align="center" valign="top" width="245">
                                <br />
                                <br />
                                <br />
                                <asp:Label ID="lblTextoMorbidadeEmpresa" runat="server"></asp:Label></td>
                            <td align="center" valign="middle" width="10">
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </ContentTemplate>
</igtab:Tab>
<igtab:Tab Tooltip="Composi&#231;&#227;o da Morbidade" Key="ComposicaoMorbidade" Text="Composi&#231;&#227;o da Morbidade">
    <ContentTemplate>
        <table align="center" border="0" cellpadding="0" cellspacing="0" class="defaultFont"
            width="550">
            <tr>
                <td align="center">
                    <igchart:UltraChart ID="UltraChartComposicaoMorbidade" runat="server" ChartType="PieChart3D" Transform3D-XRotation="240" BackColor="#FEFCFD" Height="265px" Transform3D-Scale="75" Width="500px" EmptyChartText="">
                        <Axis>
                            <PE ElementType="None" Fill="Cornsilk" />
                            <X LineThickness="1" TickmarkInterval="0" TickmarkStyle="Smart" Visible="False">
                                <MajorGridLines AlphaLevel="255" Color="Gainsboro" DrawStyle="Dot" Thickness="1"
                                    Visible="True" />
                                <MinorGridLines AlphaLevel="255" Color="LightGray" DrawStyle="Dot" Thickness="1"
                                    Visible="False" />
                                <Labels Font="Verdana, 7pt" FontColor="DimGray" HorizontalAlign="Near" ItemFormatString="&lt;ITEM_LABEL&gt;"
                                    Orientation="Horizontal" VerticalAlign="Center">
                                    <SeriesLabels Font="Verdana, 7pt" FontColor="DimGray" HorizontalAlign="Near" Orientation="Horizontal"
                                        VerticalAlign="Center" FormatString="">
                                        <Layout Behavior="Auto">
                                        </Layout>
                                    </SeriesLabels>
                                    <Layout Behavior="Auto">
                                    </Layout>
                                </Labels>
                            </X>
                            <Y LineThickness="1" TickmarkInterval="20" TickmarkStyle="Smart" Visible="False">
                                <MajorGridLines AlphaLevel="255" Color="Gainsboro" DrawStyle="Dot" Thickness="1"
                                    Visible="True" />
                                <MinorGridLines AlphaLevel="255" Color="LightGray" DrawStyle="Dot" Thickness="1"
                                    Visible="False" />
                                <Labels Font="Verdana, 7pt" FontColor="DimGray" HorizontalAlign="Far" ItemFormatString="&lt;DATA_VALUE:00.##&gt;"
                                    Orientation="Horizontal" VerticalAlign="Center">
                                    <SeriesLabels Font="Verdana, 7pt" FontColor="DimGray" HorizontalAlign="Far" Orientation="Horizontal"
                                        VerticalAlign="Center" FormatString="">
                                        <Layout Behavior="Auto">
                                        </Layout>
                                    </SeriesLabels>
                                    <Layout Behavior="Auto">
                                    </Layout>
                                </Labels>
                            </Y>
                            <Y2 LineThickness="1" TickmarkInterval="20" TickmarkStyle="Smart" Visible="False">
                                <MajorGridLines AlphaLevel="255" Color="Gainsboro" DrawStyle="Dot" Thickness="1"
                                    Visible="True" />
                                <MinorGridLines AlphaLevel="255" Color="LightGray" DrawStyle="Dot" Thickness="1"
                                    Visible="False" />
                                <Labels Font="Verdana, 7pt" FontColor="Gray" HorizontalAlign="Near" ItemFormatString="&lt;DATA_VALUE:00.##&gt;"
                                    Orientation="Horizontal" VerticalAlign="Center">
                                    <SeriesLabels Font="Verdana, 7pt" FontColor="Gray" HorizontalAlign="Near" Orientation="Horizontal"
                                        VerticalAlign="Center" FormatString="">
                                        <Layout Behavior="Auto">
                                        </Layout>
                                    </SeriesLabels>
                                    <Layout Behavior="Auto">
                                    </Layout>
                                </Labels>
                            </Y2>
                            <X2 LineThickness="1" TickmarkInterval="0" TickmarkStyle="Smart" Visible="False">
                                <MajorGridLines AlphaLevel="255" Color="Gainsboro" DrawStyle="Dot" Thickness="1"
                                    Visible="True" />
                                <MinorGridLines AlphaLevel="255" Color="LightGray" DrawStyle="Dot" Thickness="1"
                                    Visible="False" />
                                <Labels Font="Verdana, 7pt" FontColor="Gray" HorizontalAlign="Far" ItemFormatString="&lt;ITEM_LABEL&gt;"
                                    Orientation="Horizontal" VerticalAlign="Center">
                                    <SeriesLabels Font="Verdana, 7pt" FontColor="Gray" HorizontalAlign="Far" Orientation="Horizontal"
                                        VerticalAlign="Center" FormatString="">
                                        <Layout Behavior="Auto">
                                        </Layout>
                                    </SeriesLabels>
                                    <Layout Behavior="Auto">
                                    </Layout>
                                </Labels>
                            </X2>
                            <Z LineThickness="1" TickmarkInterval="0" TickmarkStyle="Smart" Visible="False">
                                <MajorGridLines AlphaLevel="255" Color="Gainsboro" DrawStyle="Dot" Thickness="1"
                                    Visible="True" />
                                <MinorGridLines AlphaLevel="255" Color="LightGray" DrawStyle="Dot" Thickness="1"
                                    Visible="False" />
                                <Labels Font="Verdana, 7pt" FontColor="DimGray" HorizontalAlign="Far" ItemFormatString=""
                                    Orientation="Horizontal" VerticalAlign="Center">
                                    <SeriesLabels Font="Verdana, 7pt" FontColor="DimGray" HorizontalAlign="Far" Orientation="Horizontal"
                                        VerticalAlign="Center">
                                        <Layout Behavior="Auto">
                                        </Layout>
                                    </SeriesLabels>
                                    <Layout Behavior="Auto">
                                    </Layout>
                                </Labels>
                            </Z>
                            <Z2 LineThickness="1" TickmarkInterval="0" TickmarkStyle="Smart" Visible="False">
                                <MajorGridLines AlphaLevel="255" Color="Gainsboro" DrawStyle="Dot" Thickness="1"
                                    Visible="True" />
                                <MinorGridLines AlphaLevel="255" Color="LightGray" DrawStyle="Dot" Thickness="1"
                                    Visible="False" />
                                <Labels Font="Verdana, 7pt" FontColor="Gray" HorizontalAlign="Near" ItemFormatString=""
                                    Orientation="Horizontal" VerticalAlign="Center">
                                    <SeriesLabels Font="Verdana, 7pt" FontColor="Gray" HorizontalAlign="Near" Orientation="Horizontal"
                                        VerticalAlign="Center">
                                        <Layout Behavior="Auto">
                                        </Layout>
                                    </SeriesLabels>
                                    <Layout Behavior="Auto">
                                    </Layout>
                                </Labels>
                            </Z2>
                        </Axis>
                        <TitleTop Visible="False">
                        </TitleTop>
                        <Border Thickness="0" />
                        <Tooltips BackColor="LightYellow" />
                        <PieChart3D OthersCategoryPercent="0">
                            <Labels FillColor="LightYellow" />
                        </PieChart3D>
                        <Effects>
                            <Effects>
                                <igchartprop:GradientEffect>
                                </igchartprop:GradientEffect>
                            </Effects>
                        </Effects>
                        <Legend BorderColor="148, 152, 120" Font="Verdana, 7.75pt, style=Bold"
                            Visible="True" FontColor="68, 146, 109" SpanPercentage="45">
                            <Margins Left="5" Right="0" />
                        </Legend>
                        <TitleBottom Visible="False" Extent="33" Location="Bottom">
                        </TitleBottom>
                        <ColorModel AlphaLevel="255" ModelStyle="LinearRange" ColorBegin="Green" ColorEnd="192, 255, 192">
                        </ColorModel>
                        <DeploymentScenario ImageURL="ChartImages/ChartCompMorbidade_#SEQNUM(10000).png" />
                    </igchart:UltraChart>
                    <asp:Label ID="lblMsgError" runat="server" Font-Bold="True" SkinID="ErrorFont"></asp:Label></td>
            </tr>
        </table>
    </ContentTemplate>
</igtab:Tab>
    <igtab:Tab Key="ComposicaoIdade" Text="Composi&#231;&#227;o por Idade" Tooltip="Composi&#231;&#227;o por Idade">
        <ContentTemplate>
        <table align="center" border="0" cellpadding="0" cellspacing="0" class="defaultFont"
            width="550">
            <tr>
                <td align="center">
                    <igchart:UltraChart ID="UltraChartComposicaoIdade" runat="server" ChartType="PieChart3D" Transform3D-XRotation="240" BackColor="#FEFCFD" Height="265px" Transform3D-Scale="75" Width="500px" EmptyChartText="">
                        <Axis>
                            <PE ElementType="None" Fill="Cornsilk" />
                            <X LineThickness="1" TickmarkInterval="0" TickmarkStyle="Smart" Visible="False">
                                <MajorGridLines AlphaLevel="255" Color="Gainsboro" DrawStyle="Dot" Thickness="1"
                                    Visible="True" />
                                <MinorGridLines AlphaLevel="255" Color="LightGray" DrawStyle="Dot" Thickness="1"
                                    Visible="False" />
                                <Labels Font="Verdana, 7pt" FontColor="DimGray" HorizontalAlign="Near" ItemFormatString="&lt;ITEM_LABEL&gt;"
                                    Orientation="Horizontal" VerticalAlign="Center">
                                    <SeriesLabels Font="Verdana, 7pt" FontColor="DimGray" HorizontalAlign="Near" Orientation="Horizontal"
                                        VerticalAlign="Center" FormatString="">
                                        <Layout Behavior="Auto">
                                        </Layout>
                                    </SeriesLabels>
                                    <Layout Behavior="Auto">
                                    </Layout>
                                </Labels>
                            </X>
                            <Y LineThickness="1" TickmarkInterval="20" TickmarkStyle="Smart" Visible="False">
                                <MajorGridLines AlphaLevel="255" Color="Gainsboro" DrawStyle="Dot" Thickness="1"
                                    Visible="True" />
                                <MinorGridLines AlphaLevel="255" Color="LightGray" DrawStyle="Dot" Thickness="1"
                                    Visible="False" />
                                <Labels Font="Verdana, 7pt" FontColor="DimGray" HorizontalAlign="Far" ItemFormatString="&lt;DATA_VALUE:00.##&gt;"
                                    Orientation="Horizontal" VerticalAlign="Center">
                                    <SeriesLabels Font="Verdana, 7pt" FontColor="DimGray" HorizontalAlign="Far" Orientation="Horizontal"
                                        VerticalAlign="Center" FormatString="">
                                        <Layout Behavior="Auto">
                                        </Layout>
                                    </SeriesLabels>
                                    <Layout Behavior="Auto">
                                    </Layout>
                                </Labels>
                            </Y>
                            <Y2 LineThickness="1" TickmarkInterval="20" TickmarkStyle="Smart" Visible="False">
                                <MajorGridLines AlphaLevel="255" Color="Gainsboro" DrawStyle="Dot" Thickness="1"
                                    Visible="True" />
                                <MinorGridLines AlphaLevel="255" Color="LightGray" DrawStyle="Dot" Thickness="1"
                                    Visible="False" />
                                <Labels Font="Verdana, 7pt" FontColor="Gray" HorizontalAlign="Near" ItemFormatString="&lt;DATA_VALUE:00.##&gt;"
                                    Orientation="Horizontal" VerticalAlign="Center">
                                    <SeriesLabels Font="Verdana, 7pt" FontColor="Gray" HorizontalAlign="Near" Orientation="Horizontal"
                                        VerticalAlign="Center" FormatString="">
                                        <Layout Behavior="Auto">
                                        </Layout>
                                    </SeriesLabels>
                                    <Layout Behavior="Auto">
                                    </Layout>
                                </Labels>
                            </Y2>
                            <X2 LineThickness="1" TickmarkInterval="0" TickmarkStyle="Smart" Visible="False">
                                <MajorGridLines AlphaLevel="255" Color="Gainsboro" DrawStyle="Dot" Thickness="1"
                                    Visible="True" />
                                <MinorGridLines AlphaLevel="255" Color="LightGray" DrawStyle="Dot" Thickness="1"
                                    Visible="False" />
                                <Labels Font="Verdana, 7pt" FontColor="Gray" HorizontalAlign="Far" ItemFormatString="&lt;ITEM_LABEL&gt;"
                                    Orientation="Horizontal" VerticalAlign="Center">
                                    <SeriesLabels Font="Verdana, 7pt" FontColor="Gray" HorizontalAlign="Far" Orientation="Horizontal"
                                        VerticalAlign="Center" FormatString="">
                                        <Layout Behavior="Auto">
                                        </Layout>
                                    </SeriesLabels>
                                    <Layout Behavior="Auto">
                                    </Layout>
                                </Labels>
                            </X2>
                            <Z LineThickness="1" TickmarkInterval="0" TickmarkStyle="Smart" Visible="False">
                                <MajorGridLines AlphaLevel="255" Color="Gainsboro" DrawStyle="Dot" Thickness="1"
                                    Visible="True" />
                                <MinorGridLines AlphaLevel="255" Color="LightGray" DrawStyle="Dot" Thickness="1"
                                    Visible="False" />
                                <Labels Font="Verdana, 7pt" FontColor="DimGray" HorizontalAlign="Far" ItemFormatString=""
                                    Orientation="Horizontal" VerticalAlign="Center">
                                    <SeriesLabels Font="Verdana, 7pt" FontColor="DimGray" HorizontalAlign="Far" Orientation="Horizontal"
                                        VerticalAlign="Center">
                                        <Layout Behavior="Auto">
                                        </Layout>
                                    </SeriesLabels>
                                    <Layout Behavior="Auto">
                                    </Layout>
                                </Labels>
                            </Z>
                            <Z2 LineThickness="1" TickmarkInterval="0" TickmarkStyle="Smart" Visible="False">
                                <MajorGridLines AlphaLevel="255" Color="Gainsboro" DrawStyle="Dot" Thickness="1"
                                    Visible="True" />
                                <MinorGridLines AlphaLevel="255" Color="LightGray" DrawStyle="Dot" Thickness="1"
                                    Visible="False" />
                                <Labels Font="Verdana, 7pt" FontColor="Gray" HorizontalAlign="Near" ItemFormatString=""
                                    Orientation="Horizontal" VerticalAlign="Center">
                                    <SeriesLabels Font="Verdana, 7pt" FontColor="Gray" HorizontalAlign="Near" Orientation="Horizontal"
                                        VerticalAlign="Center">
                                        <Layout Behavior="Auto">
                                        </Layout>
                                    </SeriesLabels>
                                    <Layout Behavior="Auto">
                                    </Layout>
                                </Labels>
                            </Z2>
                        </Axis>
                        <TitleTop Visible="False">
                        </TitleTop>
                        <Border Thickness="0" />
                        <Tooltips BackColor="LightYellow" />
                        <PieChart3D OthersCategoryPercent="0">
                            <Labels FillColor="LightYellow" />
                        </PieChart3D>
                        <Effects>
                            <Effects>
                                <igchartprop:GradientEffect>
                                </igchartprop:GradientEffect>
                            </Effects>
                        </Effects>
                        <Legend BorderColor="148, 152, 120" Font="Verdana, 7.75pt, style=Bold"
                            Visible="True" FontColor="68, 146, 109" SpanPercentage="45">
                            <Margins Left="5" Right="0" />
                        </Legend>
                        <TitleBottom Visible="False" Extent="33" Location="Bottom">
                        </TitleBottom>
                        <ColorModel AlphaLevel="255" ModelStyle="LinearRange" ColorBegin="Green" ColorEnd="192, 255, 192">
                        </ColorModel>
                        <DeploymentScenario ImageURL="ChartImages/ChartCompIdade_#SEQNUM(10000).png" />
                    </igchart:UltraChart>
                    <asp:Label ID="lblMsgIdade" runat="server" Font-Bold="True" SkinID="ErrorFont"></asp:Label></td>
            </tr>
        </table>
    </ContentTemplate>
    </igtab:Tab>
    <igtab:Tab Key="ComposicaoSexo" Text="Composi&#231;&#227;o por Sexo" Tooltip="Composi&#231;&#227;o por Sexo">
        <ContentTemplate>
        <table align="center" border="0" cellpadding="0" cellspacing="0" class="defaultFont"
            width="550">
            <tr>
                <td align="center">
                    <igchart:UltraChart ID="UltraChartComposicaoSexo" runat="server" ChartType="PieChart3D" Transform3D-XRotation="240" BackColor="#FEFCFD" Height="265px" Transform3D-Scale="75" Width="500px" EmptyChartText="">
                        <Axis>
                            <PE ElementType="None" Fill="Cornsilk" />
                            <X LineThickness="1" TickmarkInterval="0" TickmarkStyle="Smart" Visible="False">
                                <MajorGridLines AlphaLevel="255" Color="Gainsboro" DrawStyle="Dot" Thickness="1"
                                    Visible="True" />
                                <MinorGridLines AlphaLevel="255" Color="LightGray" DrawStyle="Dot" Thickness="1"
                                    Visible="False" />
                                <Labels Font="Verdana, 7pt" FontColor="DimGray" HorizontalAlign="Near" ItemFormatString="&lt;ITEM_LABEL&gt;"
                                    Orientation="Horizontal" VerticalAlign="Center">
                                    <SeriesLabels Font="Verdana, 7pt" FontColor="DimGray" HorizontalAlign="Near" Orientation="Horizontal"
                                        VerticalAlign="Center" FormatString="">
                                        <Layout Behavior="Auto">
                                        </Layout>
                                    </SeriesLabels>
                                    <Layout Behavior="Auto">
                                    </Layout>
                                </Labels>
                            </X>
                            <Y LineThickness="1" TickmarkInterval="20" TickmarkStyle="Smart" Visible="False">
                                <MajorGridLines AlphaLevel="255" Color="Gainsboro" DrawStyle="Dot" Thickness="1"
                                    Visible="True" />
                                <MinorGridLines AlphaLevel="255" Color="LightGray" DrawStyle="Dot" Thickness="1"
                                    Visible="False" />
                                <Labels Font="Verdana, 7pt" FontColor="DimGray" HorizontalAlign="Far" ItemFormatString="&lt;DATA_VALUE:00.##&gt;"
                                    Orientation="Horizontal" VerticalAlign="Center">
                                    <SeriesLabels Font="Verdana, 7pt" FontColor="DimGray" HorizontalAlign="Far" Orientation="Horizontal"
                                        VerticalAlign="Center" FormatString="">
                                        <Layout Behavior="Auto">
                                        </Layout>
                                    </SeriesLabels>
                                    <Layout Behavior="Auto">
                                    </Layout>
                                </Labels>
                            </Y>
                            <Y2 LineThickness="1" TickmarkInterval="20" TickmarkStyle="Smart" Visible="False">
                                <MajorGridLines AlphaLevel="255" Color="Gainsboro" DrawStyle="Dot" Thickness="1"
                                    Visible="True" />
                                <MinorGridLines AlphaLevel="255" Color="LightGray" DrawStyle="Dot" Thickness="1"
                                    Visible="False" />
                                <Labels Font="Verdana, 7pt" FontColor="Gray" HorizontalAlign="Near" ItemFormatString="&lt;DATA_VALUE:00.##&gt;"
                                    Orientation="Horizontal" VerticalAlign="Center">
                                    <SeriesLabels Font="Verdana, 7pt" FontColor="Gray" HorizontalAlign="Near" Orientation="Horizontal"
                                        VerticalAlign="Center" FormatString="">
                                        <Layout Behavior="Auto">
                                        </Layout>
                                    </SeriesLabels>
                                    <Layout Behavior="Auto">
                                    </Layout>
                                </Labels>
                            </Y2>
                            <X2 LineThickness="1" TickmarkInterval="0" TickmarkStyle="Smart" Visible="False">
                                <MajorGridLines AlphaLevel="255" Color="Gainsboro" DrawStyle="Dot" Thickness="1"
                                    Visible="True" />
                                <MinorGridLines AlphaLevel="255" Color="LightGray" DrawStyle="Dot" Thickness="1"
                                    Visible="False" />
                                <Labels Font="Verdana, 7pt" FontColor="Gray" HorizontalAlign="Far" ItemFormatString="&lt;ITEM_LABEL&gt;"
                                    Orientation="Horizontal" VerticalAlign="Center">
                                    <SeriesLabels Font="Verdana, 7pt" FontColor="Gray" HorizontalAlign="Far" Orientation="Horizontal"
                                        VerticalAlign="Center" FormatString="">
                                        <Layout Behavior="Auto">
                                        </Layout>
                                    </SeriesLabels>
                                    <Layout Behavior="Auto">
                                    </Layout>
                                </Labels>
                            </X2>
                            <Z LineThickness="1" TickmarkInterval="0" TickmarkStyle="Smart" Visible="False">
                                <MajorGridLines AlphaLevel="255" Color="Gainsboro" DrawStyle="Dot" Thickness="1"
                                    Visible="True" />
                                <MinorGridLines AlphaLevel="255" Color="LightGray" DrawStyle="Dot" Thickness="1"
                                    Visible="False" />
                                <Labels Font="Verdana, 7pt" FontColor="DimGray" HorizontalAlign="Far" ItemFormatString=""
                                    Orientation="Horizontal" VerticalAlign="Center">
                                    <SeriesLabels Font="Verdana, 7pt" FontColor="DimGray" HorizontalAlign="Far" Orientation="Horizontal"
                                        VerticalAlign="Center">
                                        <Layout Behavior="Auto">
                                        </Layout>
                                    </SeriesLabels>
                                    <Layout Behavior="Auto">
                                    </Layout>
                                </Labels>
                            </Z>
                            <Z2 LineThickness="1" TickmarkInterval="0" TickmarkStyle="Smart" Visible="False">
                                <MajorGridLines AlphaLevel="255" Color="Gainsboro" DrawStyle="Dot" Thickness="1"
                                    Visible="True" />
                                <MinorGridLines AlphaLevel="255" Color="LightGray" DrawStyle="Dot" Thickness="1"
                                    Visible="False" />
                                <Labels Font="Verdana, 7pt" FontColor="Gray" HorizontalAlign="Near" ItemFormatString=""
                                    Orientation="Horizontal" VerticalAlign="Center">
                                    <SeriesLabels Font="Verdana, 7pt" FontColor="Gray" HorizontalAlign="Near" Orientation="Horizontal"
                                        VerticalAlign="Center">
                                        <Layout Behavior="Auto">
                                        </Layout>
                                    </SeriesLabels>
                                    <Layout Behavior="Auto">
                                    </Layout>
                                </Labels>
                            </Z2>
                        </Axis>
                        <TitleTop Visible="False">
                        </TitleTop>
                        <Border Thickness="0" />
                        <Tooltips BackColor="LightYellow" />
                        <PieChart3D OthersCategoryPercent="0">
                            <Labels FillColor="LightYellow" />
                        </PieChart3D>
                        <Effects>
                            <Effects>
                                <igchartprop:GradientEffect>
                                </igchartprop:GradientEffect>
                            </Effects>
                        </Effects>
                        <Legend BorderColor="148, 152, 120" Font="Verdana, 7.75pt, style=Bold"
                            Visible="True" FontColor="68, 146, 109" SpanPercentage="45">
                            <Margins Left="5" Right="0" />
                        </Legend>
                        <TitleBottom Visible="False" Extent="33" Location="Bottom">
                        </TitleBottom>
                        <ColorModel AlphaLevel="255" ModelStyle="LinearRange" ColorBegin="Green" ColorEnd="192, 255, 192">
                        </ColorModel>
                        <DeploymentScenario ImageURL="ChartImages/ChartCompSexo_#SEQNUM(10000).png" />
                    </igchart:UltraChart>
                    <asp:Label ID="lblMsgSexo" runat="server" Font-Bold="True" SkinID="ErrorFont"></asp:Label></td>
            </tr>
        </table>
    </ContentTemplate>
    </igtab:Tab>
</Tabs>

<RoundedImage NormalImage="[ig_tab_winXP3.gif]" HoverImage="[ig_tab_winXP2.gif]" SelectedImage="[ig_tab_winXP1.gif]" LeftSideWidth="7" RightSideWidth="6" ShiftOfImages="2" FillStyle="LeftMergedWithCenter"></RoundedImage>

<SelectedTabStyle>
<Padding Bottom="2px"></Padding>
</SelectedTabStyle>

<DefaultTabStyle BackColor="#FEFCFD" Font-Names="Microsoft Sans Serif" Font-Size="8pt" ForeColor="Black" Height="22px">
<Padding Top="2px"></Padding>
</DefaultTabStyle>
</igtab:UltraWebTab></TD></TR></TBODY></TABLE>
</ContentTemplate>
</igtab:Tab>
<igtab:Tab Tooltip="Listagem de Empregados por Fator de Risco" Key="EmpregadoFatorRisco" Text="Empregados por Fator de Risco">
    <ContentTemplate>
        <table align="center" border="0" cellpadding="0" cellspacing="0" class="defaultFont"
            width="570">
            <tr>
                <td align="center" width="570">
                    <asp:Label ID="Label1" runat="server" SkinID="BoldFont" Text="Fator de Risco"
                        ToolTip="Fator de Risco"></asp:Label><br />
                    <asp:DropDownList ID="ddlFatorRisco" runat="server" Width="300px" AutoPostBack="True" OnSelectedIndexChanged="ddlFatorRisco_SelectedIndexChanged">
                    </asp:DropDownList><br />
                    <br />
                    <igtbl:ultrawebgrid id="UltraWebGridEmpregadoFatorRisco" runat="server" height="237px"
                        style="left: 0px" width="560px" OnInitializeRow="UltraWebGridEmpregadoFatorRisco_InitializeRow" OnPageIndexChanged="UltraWebGridEmpregadoFatorRisco_PageIndexChanged"><Bands>
                        <igtbl:UltraGridBand>
                            <Columns>
                                <igtbl:UltraGridColumn AllowGroupBy="No" BaseColumnName="NomeEmpregadoFull" Hidden="True"
                                    Key="NomeEmpregadoFull" Width="0px">
                                </igtbl:UltraGridColumn>
                                <igtbl:UltraGridColumn AllowGroupBy="No" AllowResize="Fixed" BaseColumnName="NomeEmpregado"
                                    Key="NomeEmpregado" Width="302px">
                                    <HeaderStyle HorizontalAlign="Center">
                                        <BorderDetails StyleLeft="None" StyleTop="None"  />
                                    </HeaderStyle>
                                    <CellStyle HorizontalAlign="Left">
                                        <Padding Left="3px"  />
                                        <BorderDetails StyleBottom="None" StyleLeft="None" StyleTop="None"  />
                                    </CellStyle>
                                    <Header Caption="Nome Empregado" Title="Nome do Empregado">
                                        <RowLayoutColumnInfo OriginX="1"  />
                                    </Header>
                                    <Footer>
                                        <RowLayoutColumnInfo OriginX="1"  />
                                    </Footer>
                                </igtbl:UltraGridColumn>
                                <igtbl:UltraGridColumn AllowGroupBy="No" BaseColumnName="DataExame" Key="DataExame"
                                    Width="125px">
                                    <HeaderStyle HorizontalAlign="Center">
                                        <BorderDetails StyleTop="None"  />
                                    </HeaderStyle>
                                    <CellStyle HorizontalAlign="Center">
                                        <BorderDetails StyleBottom="None" StyleTop="None"  />
                                    </CellStyle>
                                    <Header Caption="Data &#218;ltimo Exame" Title="Data do &#218;ltimo Exame">
                                        <RowLayoutColumnInfo OriginX="2"  />
                                    </Header>
                                    <Footer>
                                        <RowLayoutColumnInfo OriginX="2"  />
                                    </Footer>
                                </igtbl:UltraGridColumn>
                                <igtbl:UltraGridColumn AllowGroupBy="No" BaseColumnName="IdadeEmpregado" Key="IdadeEmpregado"
                                    Width="115px">
                                    <HeaderStyle HorizontalAlign="Center">
                                        <BorderDetails StyleTop="None"  />
                                    </HeaderStyle>
                                    <CellStyle HorizontalAlign="Center">
                                        <BorderDetails StyleBottom="None" StyleTop="None"  />
                                    </CellStyle>
                                    <Header Caption="Idade Empregado" Title="Idade Empregado">
                                        <RowLayoutColumnInfo OriginX="3"  />
                                    </Header>
                                    <Footer>
                                        <RowLayoutColumnInfo OriginX="3"  />
                                    </Footer>
                                </igtbl:UltraGridColumn>
                            </Columns>
                            <AddNewRow View="NotSet" Visible="NotSet">
                            </AddNewRow>
                        </igtbl:UltraGridBand>
</Bands>

<DisplayLayout Version="4.00" ScrollBar="Always" AutoGenerateColumns="False" Name="UltraWebGridEmpregadoFatorRisco" RowSelectorsDefault="No" ScrollBarView="Vertical" RowHeightDefault="18px" TableLayout="Fixed" StationaryMargins="HeaderAndFooter">
<GroupByBox Hidden="True">
<BoxStyle BorderColor="Window" BackColor="ActiveBorder"></BoxStyle>
</GroupByBox>

<GroupByRowStyleDefault BorderColor="Window" BackColor="Control"></GroupByRowStyleDefault>

<ActivationObject BorderWidth="1px" BorderStyle="Solid" BorderColor="124, 197, 161" AllowActivation="False"></ActivationObject>

<FooterStyleDefault BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
</FooterStyleDefault>

<RowStyleDefault ForeColor="#156047" HorizontalAlign="Center" BorderWidth="1px" BorderColor="#7CC5A1" BorderStyle="Solid" Font-Size="XX-Small" Font-Names="Verdana" BackColor="White"></RowStyleDefault>

<FilterOptionsDefault>
<FilterOperandDropDownStyle BorderWidth="1px" BorderColor="Silver" BorderStyle="Solid" Font-Size="11px" Font-Names="Verdana,Arial,Helvetica,sans-serif" BackColor="White" CustomRules="overflow:auto;">
<Padding Left="2px"></Padding>
</FilterOperandDropDownStyle>

<FilterHighlightRowStyle ForeColor="White" BackColor="#151C55"></FilterHighlightRowStyle>

<FilterDropDownStyle BorderWidth="1px" BorderColor="Silver" BorderStyle="Solid" Font-Size="11px" Font-Names="Verdana,Arial,Helvetica,sans-serif" BackColor="White" Width="200px" Height="300px" CustomRules="overflow:auto;">
<Padding Left="2px"></Padding>
</FilterDropDownStyle>
</FilterOptionsDefault>

<HeaderStyleDefault ForeColor="#44926D" HorizontalAlign="Center" VerticalAlign="Middle" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True" BackColor="#DEEFE4">
<Margin Top="0px" Left="0px" Bottom="0px" Right="0px"></Margin>

<Padding Top="0px" Left="0px" Bottom="0px" Right="0px"></Padding>
</HeaderStyleDefault>

<EditCellStyleDefault BorderWidth="0px" BorderStyle="None"></EditCellStyleDefault>

<FrameStyle BorderWidth="1px" BorderColor="#7CC5A1" BorderStyle="Solid" Font-Size="XX-Small" Font-Names="Verdana" BackColor="WhiteSmoke" Width="560px" Height="237px"></FrameStyle>

<Pager Alignment="Center" ChangeLinksColor="True" NextText="Pr&#243;ximo" PageSize="800" PrevText="Anterior" Pattern="P&#225;gina &lt;b&gt;[currentpageindex]&lt;/b&gt; de &lt;b&gt;[pagecount]&lt;/b&gt;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&lt;b&gt;[default]&lt;/b&gt;" QuickPages="5" StyleMode="QuickPages" AllowPaging="True">
<PagerStyle ForeColor="#44926D" BorderWidth="1px" BorderStyle="Solid" Font-Size="XX-Small" Font-Names="Verdana" BackColor="#DEEFE4" Height="18px" BorderColor="#7CC5A1">
    <BorderDetails StyleBottom="None" StyleLeft="None" StyleRight="None" StyleTop="Solid" WidthTop="1px"  />
</PagerStyle>
</Pager>

<AddNewBox Hidden="False"></AddNewBox>
    <ClientSideEvents MouseOverHandler="UltraWebGridEmpregadoFatorRisco_MouseOverHandler" MouseOutHandler="UltraWebGridEmpregadoFatorRisco_MouseOutHandler"  />
</DisplayLayout>
</igtbl:ultrawebgrid>
                    <table align="center" border="0" cellpadding="0" cellspacing="0" class="defaultFont"
                        width="560">
                        <tr>
                            <td align="right">
                                <asp:Label ID="lblTotRegistros" runat="server"></asp:Label></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </ContentTemplate>
</igtab:Tab>
</Tabs>

<RoundedImage NormalImage="[ig_tab_winXP3.gif]" HoverImage="[ig_tab_winXP2.gif]" SelectedImage="[ig_tab_winXP1.gif]" LeftSideWidth="7" RightSideWidth="6" ShiftOfImages="2" FillStyle="LeftMergedWithCenter"></RoundedImage>

<SelectedTabStyle>
<Padding Bottom="2px"></Padding>
</SelectedTabStyle>

<DefaultTabStyle BackColor="#FEFCFD" Font-Names="Microsoft Sans Serif" Font-Size="8pt" ForeColor="Black" Height="22px">
<Padding Top="2px"></Padding>
</DefaultTabStyle>
</igtab:ultrawebtab>
                    </igmisc:WebAsyncRefreshPanel>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
