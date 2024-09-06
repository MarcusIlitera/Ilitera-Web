<%@ Page Language="C#"   AutoEventWireup="True" CodeBehind="PastaExAudiometrico.aspx.cs"  Inherits="Ilitera.Net.PCMSO.PastaExAudiometrico" Title="Ilitera.Net" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD runat="server">
		<title>Mestra.NET</title>
		<meta content="True" name="vs_showGrid">
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script language="javascript" src="../scripts/validador.js"></script>
		<script language="javascript">		
		function ExameAudiometricoMouseOver(gridName, id, objectType)
		{
		    DataGridMouseOverHandler(gridName, id, objectType, 2);
		}
		
		function ExameAudiometricoMouseOut(gridName, id, objectType)
		{
		    DataGridMouseOutHandler(gridName, id, objectType, 2);
		}
        
        function ExameAudiometrico_CellClickHandler(gridName, cellId, button)
        {
	        if (button == 0)
	        {
	            var cell = igtbl_getCellById(cellId);
	            
	            if (cell.Index == 2)
	            {
	                var DoScript = cell.getTargetURL();
	                DoScript = DoScript.replace("javascript:", "");
	                
	                eval(unescape(DoScript));
	            }
	        }
        }
		</script>
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0">
		<form id="WFormPasta" method="post" runat="server">
			<TABLE class="defaultFont" cellSpacing="0" cellPadding="0" width="600" align="center" border="0">
				<TR>
					<TD class="defaultFont" align="center" colSpan="1"><asp:label id="lblExaAud" runat="server" SkinID="TitleFont">Exames Audiométricos</asp:label><BR>
						<BR>
					</TD>
				<TR>
					<TD vAlign="top" align="right"><igtbl:ultrawebgrid id="UltraWebGridExameAudiometrico" runat="server" Height="128px" ImageDirectory="/ig_common/Images/"
							Width="592px" OnInitializeRow="UltraWebGridExameAudiometrico_InitializeRow" OnPageIndexChanged="UltraWebGridExameAudiometrico_PageIndexChanged"><Bands>
<igtbl:UltraGridBand GroupByRowDescriptionMask="[caption] : [value] ([count])   --   Relat&#243;rio: &lt;a href=&quot;javascript:RelatorioN('[value]')&quot;&gt;Por Nome&lt;/a&gt; ou &lt;a href=&quot;javascript:RelatorioD('[value]')&quot;&gt;Por Data&lt;/a&gt;"><Columns>
<igtbl:UltraGridColumn Key="Id" AllowGroupBy="No" BaseColumnName="Id" Hidden="True" Width="0px">
<Header Caption="Id"></Header>
</igtbl:UltraGridColumn>
<igtbl:UltraGridColumn Key="Data" AllowGroupBy="No" BaseColumnName="Data" FooterText="" Format="" Width="80px" EditorControlID="">
<CellButtonStyle HorizontalAlign="Left" Cursor="Hand" BackColor="#EFFFF6" Font-Bold="True" Font-Names="Verdana" Font-Size="XX-Small" ForeColor="#156047">
<Padding Left="3px"></Padding>

<BorderDetails StyleLeft="None" StyleRight="None" StyleTop="None" StyleBottom="None"></BorderDetails>
</CellButtonStyle>

<HeaderStyle>
<BorderDetails StyleLeft="None" StyleTop="None"></BorderDetails>
</HeaderStyle>

<CellStyle HorizontalAlign="Center">
<BorderDetails StyleLeft="None"></BorderDetails>
</CellStyle>

<SelectedCellStyle BorderStyle="None"></SelectedCellStyle>

<Header Caption="Data">
<RowLayoutColumnInfo OriginX="1"></RowLayoutColumnInfo>
</Header>

<Footer Caption="" Key="NomeAbreviado">
<RowLayoutColumnInfo OriginX="1"></RowLayoutColumnInfo>
</Footer>
</igtbl:UltraGridColumn>
<igtbl:UltraGridColumn Key="Tipo" AllowGroupBy="No" BaseColumnName="Tipo" FooterText="" Format="" Width="150px" EditorControlID="" ChangeLinksColor="True">
<CellButtonStyle HorizontalAlign="Left" Cursor="Hand" BackColor="#EFFFF6" BorderWidth="0px" BorderStyle="None" Font-Bold="True" Font-Names="Verdana" Font-Size="XX-Small" Font-Underline="True" ForeColor="#156047">
<Padding Left="3px"></Padding>
</CellButtonStyle>

<HeaderStyle>
<BorderDetails StyleLeft="None" StyleTop="None"></BorderDetails>
</HeaderStyle>

<CellStyle HorizontalAlign="Left" Font-Bold="True">
<Padding Left="3px"></Padding>

<BorderDetails StyleLeft="None"></BorderDetails>
</CellStyle>

<Header Caption="Tipo">
<RowLayoutColumnInfo OriginX="2"></RowLayoutColumnInfo>
</Header>

<Footer Caption="" Key="NomeCompleto">
<RowLayoutColumnInfo OriginX="2"></RowLayoutColumnInfo>
</Footer>
</igtbl:UltraGridColumn>
<igtbl:UltraGridColumn Key="Resultado" AllowGroupBy="No" BaseColumnName="Resultado" Width="100px">
<HeaderStyle>
<BorderDetails StyleTop="None"></BorderDetails>
</HeaderStyle>

<CellStyle HorizontalAlign="Center"></CellStyle>

<Header Caption="Resultado">
<RowLayoutColumnInfo OriginX="3"></RowLayoutColumnInfo>
</Header>

<Footer Key="EditTreinamento">
<RowLayoutColumnInfo OriginX="3"></RowLayoutColumnInfo>
</Footer>
</igtbl:UltraGridColumn>
<igtbl:UltraGridColumn Key="Medico" AllowGroupBy="No" BaseColumnName="Medico" Width="200px">
<HeaderStyle>
<BorderDetails StyleTop="None"></BorderDetails>
</HeaderStyle>

<CellStyle HorizontalAlign="Left">
<Padding Left="3px"></Padding>
</CellStyle>

<Header Caption="M&#233;dico">
<RowLayoutColumnInfo OriginX="4"></RowLayoutColumnInfo>
</Header>

<Footer Key="Termino">
<RowLayoutColumnInfo OriginX="4"></RowLayoutColumnInfo>
</Footer>
</igtbl:UltraGridColumn>
<igtbl:UltraGridColumn Key="Audiograma" AllowGroupBy="No" BaseColumnName="Audiograma" Width="30px">
<CellButtonStyle HorizontalAlign="Center" Cursor="Hand" BackColor="#EFFFF6" Font-Bold="True" Font-Names="Verdana" Font-Size="XX-Small" ForeColor="#156047">
<BorderDetails StyleLeft="None" StyleRight="None" StyleTop="None" StyleBottom="None"></BorderDetails>
</CellButtonStyle>

<HeaderStyle>
<BorderDetails StyleTop="None"></BorderDetails>
</HeaderStyle>

<CellStyle Font-Bold="True"></CellStyle>

<Header Caption="ADG">
<RowLayoutColumnInfo OriginX="5"></RowLayoutColumnInfo>
</Header>

<Footer Key="LTCAT">
<RowLayoutColumnInfo OriginX="5"></RowLayoutColumnInfo>
</Footer>
</igtbl:UltraGridColumn>
<igtbl:UltraGridColumn Key="Anamnese" AllowGroupBy="No" BaseColumnName="Anamnese" Width="30px">
<CellButtonStyle HorizontalAlign="Center" Cursor="Hand" BackColor="#EFFFF6" Font-Bold="True" Font-Names="Verdana" Font-Size="XX-Small" ForeColor="#156047">
<BorderDetails StyleLeft="None" StyleRight="None" StyleTop="None" StyleBottom="None"></BorderDetails>
</CellButtonStyle>

<HeaderStyle>
<BorderDetails StyleTop="None" StyleRight="None"></BorderDetails>
</HeaderStyle>

<CellStyle Font-Bold="True">
    <BorderDetails StyleRight="None" />
</CellStyle>

<Header Caption="ANM">
<RowLayoutColumnInfo OriginX="6"></RowLayoutColumnInfo>
</Header>

<Footer Key="PPRA">
<RowLayoutColumnInfo OriginX="6"></RowLayoutColumnInfo>
</Footer>
</igtbl:UltraGridColumn>
</Columns>

<RowTemplateStyle BackColor="White" BorderColor="White" BorderStyle="Ridge">
<BorderDetails WidthLeft="3px" WidthTop="3px" WidthRight="3px" WidthBottom="3px"></BorderDetails>
</RowTemplateStyle>

<AddNewRow Visible="NotSet" View="NotSet"></AddNewRow>
</igtbl:UltraGridBand>
</Bands>

<DisplayLayout Version="3.00" JavaScriptFileName="" JavaScriptFileNameCommon="" Name="UltraWebGridExameAudiometrico" RowHeightDefault="18px" TableLayout="Fixed" ViewType="OutlookGroupBy" RowSelectorsDefault="No" AutoGenerateColumns="False">
<FrameStyle BackColor="WhiteSmoke" BorderColor="#7CC5A1" BorderWidth="1px" BorderStyle="Solid" Height="128px" Width="592px"></FrameStyle>



<Images  ImageDirectory="/ig_common/Images/"></Images>

<Pager PageSize="5" AllowPaging="True" StyleMode="QuickPages" Alignment="Center" PrevText="Anterior" NextText="Pr&#243;ximo" QuickPages="10" Pattern="P&#225;gina &lt;b&gt;[currentpageindex]&lt;/b&gt; de &lt;b&gt;[pagecount]&lt;/b&gt;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&lt;b&gt;[default]&lt;/b&gt;">
<PagerStyle HorizontalAlign="Center" BackColor="#DEEFE4" BorderWidth="0px" BorderStyle="None" Font-Names="Verdana" Font-Size="XX-Small" ForeColor="#44926D" Height="18px"></PagerStyle>
</Pager>

<EditCellStyleDefault BorderWidth="0px" BorderStyle="None"></EditCellStyleDefault>

<FooterStyleDefault BorderWidth="1px" BorderStyle="Solid">
<BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" WidthTop="1px"></BorderDetails>
</FooterStyleDefault>

<HeaderStyleDefault HorizontalAlign="Center" VerticalAlign="Middle" BackColor="#DEEFE4" Font-Bold="True" Font-Names="Verdana" Font-Size="XX-Small" ForeColor="#44926D" Height="18px"></HeaderStyleDefault>

<RowStyleDefault HorizontalAlign="Center" VerticalAlign="Middle" BackColor="White" BorderColor="#7CC5A1" BorderWidth="1px" BorderStyle="Solid" Font-Names="Verdana" Font-Size="XX-Small" ForeColor="#156047"></RowStyleDefault>

<GroupByRowStyleDefault Height="18px"></GroupByRowStyleDefault>

<GroupByBox Hidden="True"></GroupByBox>

<AddNewBox>
<BoxStyle BackColor="LightGray" BorderWidth="1px" BorderStyle="Solid"></BoxStyle>
</AddNewBox>

<ActivationObject AllowActivation="False" BorderColor="124, 197, 161" BorderWidth=""></ActivationObject>
    <ClientSideEvents CellClickHandler="ExameAudiometrico_CellClickHandler" MouseOutHandler="ExameAudiometricoMouseOut"
        MouseOverHandler="ExameAudiometricoMouseOver" />
</DisplayLayout>
</igtbl:ultrawebgrid>
						<TABLE class="defaultFont" id="Table1" cellSpacing="0" cellPadding="0" width="592" align="right"
							border="0">
							<TR>
								<TD align="right"><asp:label id="lblTotRegistros" runat="server"></asp:label></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD class="defaultFont" align="center"><BR>
						<asp:hyperlink id="hlkNovo" runat="server" SkinID="BoldLink">Novo Exame</asp:hyperlink></TD>
				</TR>
			</TABLE>
			<INPUT id="txtAuxiliar" type="hidden" name="txtAuxiliar" runat="server">
		</form>
	</body>
</HTML>
