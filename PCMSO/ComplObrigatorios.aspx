<%@ Page Language="C#"   AutoEventWireup="True" CodeBehind="ComplObrigatorios.aspx.cs"  Inherits="Ilitera.Net.PCMSO.ComplObrigatorios" Title="Ilitera.Net" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD runat="server">
		<title>Mestra.NET</title>
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK type="text/css" rel="stylesheet">
		<LINK href="scripts/datagrid.css" type="text/css" rel="stylesheet">
        <script id="igClientScript" type="text/javascript">
        function warpExamesComplementares_InitializePanel(oPanel)
        {
	        oPanel.getProgressIndicator().setImageUrl("img/loading.gif");
        }
        </script>
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0">
		<form method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="600" align="center" border="0" class="defaultFont">
				<TR>
					<TD class="defaultFont" align="center">
						<asp:Label id="lblTitulo" runat="server" SkinID="TitleFont">Exames Complementares Indicados</asp:Label><BR>
                        &nbsp;</TD>
				</TR>
                <tr>
                    <td align="right" class="defaultFont">
                        <igmisc:WebAsyncRefreshPanel ID="warpExamesComplementares" runat="server" Height=""
                            Width="595px" InitializePanel="warpExamesComplementares_InitializePanel">
                            <igtbl:UltraWebGrid ID="UltraWebGridExamesComplementares" runat="server" Height="128px" ImageDirectory="/ig_common/Images/"
                            Style="left: 3px; top: -91px" Width="592px" OnInitializeRow="UltraWebGridExamesComplementares_InitializeRow" OnPageIndexChanged="UltraWebGridExamesComplementares_PageIndexChanged">
                            <Bands>
                                <igtbl:UltraGridBand GroupByRowDescriptionMask="[caption] : [value] ([count])   --   Relat&#243;rio: &lt;a href=&quot;javascript:RelatorioN('[value]')&quot;&gt;Por Nome&lt;/a&gt; ou &lt;a href=&quot;javascript:RelatorioD('[value]')&quot;&gt;Por Data&lt;/a&gt;">
                                    <Columns>
                                        <igtbl:UltraGridColumn AllowGroupBy="No" BaseColumnName="PeriodicidadeFull" Hidden="True"
                                            Key="PeriodicidadeFull" Width="0px">
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn AllowGroupBy="No" BaseColumnName="NomeExameFull" Hidden="True"
                                            Key="NomeExameFull" Width="0px">
                                            <Header>
                                                <RowLayoutColumnInfo OriginX="1" />
                                            </Header>
                                            <Footer>
                                                <RowLayoutColumnInfo OriginX="1" />
                                            </Footer>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn AllowGroupBy="No" BaseColumnName="NomeExame" EditorControlID=""
                                            FooterText="" Format="" Key="NomeExame" Width="120px" AllowResize="Free">
                                            <CellButtonStyle BackColor="#EFFFF6" Cursor="Hand" Font-Bold="True" Font-Names="Verdana"
                                                Font-Size="XX-Small" ForeColor="#156047" HorizontalAlign="Left">
                                                <Padding Left="3px" />
                                                <BorderDetails StyleBottom="None" StyleLeft="None" StyleRight="None" StyleTop="None" />
                                            </CellButtonStyle>
                                            <HeaderStyle>
                                                <BorderDetails StyleLeft="None" StyleTop="None" />
                                            </HeaderStyle>
                                            <CellStyle HorizontalAlign="Left">
                                                <BorderDetails StyleLeft="None" />
                                                <Padding Left="3px" />
                                            </CellStyle>
                                            <SelectedCellStyle BorderStyle="None">
                                            </SelectedCellStyle>
                                            <Header Caption="Exame" Title="Exame">
                                                <RowLayoutColumnInfo OriginX="2" />
                                            </Header>
                                            <Footer Caption="" Key="NomeAbreviado">
                                                <RowLayoutColumnInfo OriginX="2" />
                                            </Footer>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn AllowGroupBy="No" BaseColumnName="DataUltimo" EditorControlID=""
                                            FooterText="" Format="" Key="DataUltimo" Width="90px">
                                            <CellButtonStyle BackColor="#EFFFF6" BorderStyle="None" BorderWidth="0px" Cursor="Hand"
                                                Font-Bold="True" Font-Names="Verdana" Font-Size="XX-Small" Font-Underline="True"
                                                ForeColor="#156047" HorizontalAlign="Left">
                                                <Padding Left="3px" />
                                            </CellButtonStyle>
                                            <HeaderStyle>
                                                <BorderDetails StyleLeft="None" StyleTop="None" />
                                            </HeaderStyle>
                                            <CellStyle Font-Bold="True" HorizontalAlign="Center">
                                                <BorderDetails StyleLeft="None" />
                                            </CellStyle>
                                            <Header Caption="&#218;ltimo" Title="Data do &#218;ltimo Exame">
                                                <RowLayoutColumnInfo OriginX="3" />
                                            </Header>
                                            <Footer Caption="" Key="NomeCompleto">
                                                <RowLayoutColumnInfo OriginX="3" />
                                            </Footer>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn AllowGroupBy="No" BaseColumnName="DataVencimento" Key="DataVencimento"
                                            Width="90px">
                                            <HeaderStyle>
                                                <BorderDetails StyleTop="None" />
                                            </HeaderStyle>
                                            <CellStyle HorizontalAlign="Center">
                                            </CellStyle>
                                            <Header Caption="Vencimento" Title="Data de Vencimento do Exame">
                                                <RowLayoutColumnInfo OriginX="4" />
                                            </Header>
                                            <Footer Key="EditTreinamento">
                                                <RowLayoutColumnInfo OriginX="4" />
                                            </Footer>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn AllowGroupBy="No" BaseColumnName="Periodicidade" Key="Periodicidade" Width="265px">
                                            <HeaderStyle>
                                                <BorderDetails StyleTop="None" />
                                            </HeaderStyle>
                                            <CellStyle HorizontalAlign="Left">
                                                <Padding Left="3px" />
                                            </CellStyle>
                                            <Header Caption="Periodicidade" Title="Periodicidade do Exame">
                                                <RowLayoutColumnInfo OriginX="5" />
                                            </Header>
                                            <Footer Key="Termino">
                                                <RowLayoutColumnInfo OriginX="5" />
                                            </Footer>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn AllowGroupBy="No" BaseColumnName="Preventivo" Key="Preventivo" Width="25px">
                                            <CellButtonStyle BackColor="#EFFFF6" Cursor="Hand" Font-Bold="True" Font-Names="Verdana"
                                                Font-Size="XX-Small" ForeColor="#156047" HorizontalAlign="Center">
                                                <BorderDetails StyleBottom="None" StyleLeft="None" StyleRight="None" StyleTop="None" />
                                            </CellButtonStyle>
                                            <HeaderStyle>
                                                <BorderDetails StyleTop="None" StyleRight="None" />
                                            </HeaderStyle>
                                            <CellStyle Font-Bold="True">
                                                <BorderDetails StyleRight="None" />
                                            </CellStyle>
                                            <Header Caption="Pv" Title="Exame Preventivo">
                                                <RowLayoutColumnInfo OriginX="6" />
                                            </Header>
                                            <Footer Key="PPRA">
                                                <RowLayoutColumnInfo OriginX="6" />
                                            </Footer>
                                        </igtbl:UltraGridColumn>
                                    </Columns>
                                    <RowTemplateStyle BackColor="White" BorderColor="White" BorderStyle="Ridge">
                                        <BorderDetails WidthBottom="3px" WidthLeft="3px" WidthRight="3px" WidthTop="3px" />
                                    </RowTemplateStyle>
                                    <AddNewRow View="NotSet" Visible="NotSet">
                                    </AddNewRow>
                                </igtbl:UltraGridBand>
                            </Bands>
                            <DisplayLayout AutoGenerateColumns="False" Name="UltraWebGridExamesComplementares" RowHeightDefault="18px"
                                RowSelectorsDefault="No" TableLayout="Fixed" Version="3.00" ViewType="OutlookGroupBy">
                                <FrameStyle BackColor="WhiteSmoke" BorderColor="#7CC5A1" BorderStyle="Solid" BorderWidth="1px"
                                    Height="128px" Width="592px">
                                </FrameStyle>
                                <Images ImageDirectory="/ig_common/Images/">
                                </Images>
                                <Pager Alignment="Center" AllowPaging="True" NextText="Pr&#243;ximo" PageSize="5"
                                    Pattern="P&#225;gina &lt;b&gt;[currentpageindex]&lt;/b&gt; de &lt;b&gt;[pagecount]&lt;/b&gt;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&lt;b&gt;[default]&lt;/b&gt;"
                                    PrevText="Anterior" QuickPages="10" StyleMode="QuickPages">
                                    <PagerStyle BackColor="#DEEFE4" BorderStyle="None" BorderWidth="0px" Font-Names="Verdana"
                                        Font-Size="XX-Small" ForeColor="#44926D" Height="18px" HorizontalAlign="Center" />
                                </Pager>
                                <EditCellStyleDefault BorderStyle="None" BorderWidth="0px">
                                </EditCellStyleDefault>
                                <FooterStyleDefault BorderStyle="Solid" BorderWidth="1px">
                                    <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" WidthTop="1px" />
                                </FooterStyleDefault>
                                <HeaderStyleDefault BackColor="#DEEFE4" Font-Bold="True" Font-Names="Verdana" Font-Size="XX-Small"
                                    ForeColor="#44926D" Height="18px" HorizontalAlign="Center" VerticalAlign="Middle">
                                </HeaderStyleDefault>
                                <RowStyleDefault BackColor="White" BorderColor="#7CC5A1" BorderStyle="Solid" BorderWidth="1px"
                                    Font-Names="Verdana" Font-Size="XX-Small" ForeColor="#156047" HorizontalAlign="Center"
                                    VerticalAlign="Middle">
                                </RowStyleDefault>
                                <GroupByRowStyleDefault Height="18px">
                                </GroupByRowStyleDefault>
                                <GroupByBox Hidden="True">
                                </GroupByBox>
                                <AddNewBox>
                                    <BoxStyle BackColor="LightGray" BorderStyle="Solid" BorderWidth="1px">
                                    </BoxStyle>
                                </AddNewBox>
                                <ActivationObject AllowActivation="False" BorderColor="124, 197, 161" BorderWidth="">
                                </ActivationObject>
                            </DisplayLayout>
                        </igtbl:UltraWebGrid>
                        </igmisc:WebAsyncRefreshPanel>
                        </td>
                </tr>
				<TR>
					<TD class="defaultFont" align="right"><asp:label id="lblTotRegistros" runat="server"></asp:label></TD>
				</TR>
                <tr>
                    <td align="center" class="defaultFont">
                        <asp:Label ID="lblAviso" runat="server" SkinID="ErrorFont" Width="500px"></asp:Label></td>
                </tr>
			</TABLE>
		</form>
	</body>
</HTML>
