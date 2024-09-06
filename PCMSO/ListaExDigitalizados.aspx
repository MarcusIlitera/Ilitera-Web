<%@ Page Language="C#"   AutoEventWireup="True" CodeBehind="ListaExDigitalizados.aspx.cs"  Inherits="Ilitera.Net.PCMSO.ListaExDigitalizados" Title="Ilitera.Net" %>
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
		function ExameDigitalizadoMouseOver(gridName, id, objectType)
		{
		    DataGridMouseOverHandler(gridName, id, objectType, 3);
		}
		
		function ExameDigitalizadoMouseOut(gridName, id, objectType)
		{
		    DataGridMouseOutHandler(gridName, id, objectType, 3);
		}
        
        function ExameDigitalizado_CellClickHandler(gridName, cellId, button)
        {
	        if (button == 0)
	        {
	            var cell = igtbl_getCellById(cellId);
	            
	            if (cell.Index == 3 && cell.getElement().innerText.Trim() != "")
	            {
	                var DoScript = cell.getTargetURL();
	                DoScript = DoScript.replace("javascript:", "");
	                
	                eval(unescape(DoScript));
	            }
	        }
        }
		</script>
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0" bgcolor="#edffeb">
		<form method="post" runat="server">
			<TABLE class="defaultFont" cellSpacing="0" cellPadding="0" width="600" align="center" border="0" bgcolor="#edffeb">
				<TR>
					<TD class="defaultFont" align="center" colSpan="1"><asp:label id="lblExCli" runat="server" SkinID="TitleFont">Exames Digitalizados</asp:label><BR>
						<BR>
					</TD>
				<TR>
					<TD vAlign="top" align="right" bgcolor="#EDFFEB"><igtbl:ultrawebgrid id="UltraWebGridExameDigitalizado" runat="server" ImageDirectory="/ig_common/Images/"
							Height="128px" Width="592px" OnInitializeRow="UltraWebGridExameDigitalizado_InitializeRow" OnPageIndexChanged="UltraWebGridExameDigitalizado_PageIndexChanged">
							<DisplayLayout AutoGenerateColumns="False" RowHeightDefault="18px"
								Version="3.00" ViewType="OutlookGroupBy" RowSelectorsDefault="No"
								Name="UltraWebGridExameDigitalizado" TableLayout="Fixed">
								<AddNewBox>
                                    <BoxStyle BackColor="LightGray" BorderStyle="Solid" BorderWidth="1px">
                                    </BoxStyle>
								</AddNewBox>
								<Pager PrevText="Anterior" NextText="Pr&#243;ximo" QuickPages="10" PageSize="5" Pattern="P&#225;gina &lt;b&gt;[currentpageindex]&lt;/b&gt; de &lt;b&gt;[pagecount]&lt;/b&gt;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&lt;b&gt;[default]&lt;/b&gt;"
									StyleMode="QuickPages" Alignment="Center" AllowPaging="True">
                                    <PagerStyle BackColor="#DEEFE4" BorderStyle="None" BorderWidth="0px" Font-Names="Verdana"
                                        Font-Size="XX-Small" ForeColor="#44926D" Height="18px" HorizontalAlign="Center" />
								</Pager>
								<HeaderStyleDefault VerticalAlign="Middle" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True"
									HorizontalAlign="Center" ForeColor="#44926D" BackColor="#DEEFE4" Height="18px"></HeaderStyleDefault>
								<GroupByRowStyleDefault Height="18px"></GroupByRowStyleDefault>
								<FrameStyle Width="592px" BorderWidth="1px" BorderColor="#7CC5A1" BorderStyle="Solid" Height="128px" BackColor="WhiteSmoke"></FrameStyle>
								<FooterStyleDefault BorderWidth="1px" BorderStyle="Solid">
									<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
								</FooterStyleDefault>
								<ActivationObject AllowActivation="False" BorderColor="124, 197, 161" BorderWidth=""></ActivationObject>
								<GroupByBox Hidden="True"></GroupByBox>
								<EditCellStyleDefault BorderWidth="0px" BorderStyle="None"></EditCellStyleDefault>
								<RowStyleDefault VerticalAlign="Middle" BorderWidth="1px" Font-Size="XX-Small" Font-Names="Verdana"
									BorderColor="#7CC5A1" BorderStyle="Solid" HorizontalAlign="Center" ForeColor="#156047" BackColor="White"></RowStyleDefault>
                                <Images ImageDirectory="/ig_common/Images/">
                                </Images>
                                <ClientSideEvents CellClickHandler="ExameDigitalizado_CellClickHandler" MouseOutHandler="ExameDigitalizadoMouseOut"
                                    MouseOverHandler="ExameDigitalizadoMouseOver" />
							</DisplayLayout>
							<Bands>
								<igtbl:UltraGridBand GroupByRowDescriptionMask="[caption] : [value] ([count])   --   Relat&#243;rio: &lt;a href=&quot;javascript:RelatorioN('[value]')&quot;&gt;Por Nome&lt;/a&gt; ou &lt;a href=&quot;javascript:RelatorioD('[value]')&quot;&gt;Por Data&lt;/a&gt;">
									<Columns>
										<igtbl:UltraGridColumn Key="Id" Width="0px" Hidden="True" AllowGroupBy="No" BaseColumnName="Id">
											<Header Caption="Id"></Header>
										</igtbl:UltraGridColumn>
										<igtbl:UltraGridColumn Key="DataDocumento" EditorControlID="" Width="120px"
											AllowGroupBy="No" Format="" BaseColumnName="DataDocumento" FooterText="">
											<SelectedCellStyle BorderStyle="None"></SelectedCellStyle>
											<CellButtonStyle Cursor="Hand" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True" HorizontalAlign="Left"
												ForeColor="#156047" BackColor="#EFFFF6">
												<Padding Left="3px"></Padding>
												<BorderDetails StyleBottom="None" StyleTop="None" StyleRight="None" StyleLeft="None"></BorderDetails>
											</CellButtonStyle>
											<CellStyle HorizontalAlign="Center">
												<BorderDetails StyleLeft="None"></BorderDetails>
											</CellStyle>
											<Footer Key="NomeAbreviado" Caption="">
												<RowLayoutColumnInfo OriginX="1"></RowLayoutColumnInfo>
											</Footer>
											<HeaderStyle>
												<BorderDetails StyleTop="None" StyleLeft="None"></BorderDetails>
											</HeaderStyle>
											<Header Caption="Data Documento">
												<RowLayoutColumnInfo OriginX="1"></RowLayoutColumnInfo>
											</Header>
										</igtbl:UltraGridColumn>
										<igtbl:UltraGridColumn Key="DataDigitalizacao" Width="120px"
											AllowGroupBy="No" BaseColumnName="DataDigitalizacao">
											<CellStyle HorizontalAlign="Center"></CellStyle>
											<Footer Key="EditTreinamento">
												<RowLayoutColumnInfo OriginX="2"></RowLayoutColumnInfo>
											</Footer>
											<HeaderStyle>
												<BorderDetails StyleTop="None"></BorderDetails>
											</HeaderStyle>
											<Header Caption="Data Digitaliza&#231;&#227;o">
												<RowLayoutColumnInfo OriginX="2"></RowLayoutColumnInfo>
											</Header>
										</igtbl:UltraGridColumn>
										<igtbl:UltraGridColumn Key="Tipo" EditorControlID="" Width="160px" AllowGroupBy="No" Format="" BaseColumnName="Tipo" FooterText="" ChangeLinksColor="True">
											<CellButtonStyle Cursor="Hand" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True" HorizontalAlign="Left"
												ForeColor="#156047" BackColor="#EFFFF6" BorderStyle="None" BorderWidth="0px" Font-Underline="True">
												<Padding Left="3px"></Padding>
											</CellButtonStyle>
											<CellStyle Font-Bold="True" HorizontalAlign="Left">
												<Padding Left="3px"></Padding>
											</CellStyle>
											<Footer Key="NomeCompleto" Caption="">
												<RowLayoutColumnInfo OriginX="3"></RowLayoutColumnInfo>
											</Footer>
											<HeaderStyle>
												<BorderDetails StyleTop="None" StyleLeft="None"></BorderDetails>
											</HeaderStyle>
											<Header Caption="Tipo">
												<RowLayoutColumnInfo OriginX="3"></RowLayoutColumnInfo>
											</Header>
										</igtbl:UltraGridColumn>
										<igtbl:UltraGridColumn Key="Descricao" Width="165px" AllowGroupBy="No"
											BaseColumnName="Descricao">
											<CellStyle HorizontalAlign="Left">
												<Padding Left="3px"></Padding>
											</CellStyle>
											<Footer Key="Termino">
												<RowLayoutColumnInfo OriginX="4"></RowLayoutColumnInfo>
											</Footer>
											<HeaderStyle>
												<BorderDetails StyleTop="None"></BorderDetails>
											</HeaderStyle>
											<Header Caption="Descri&#231;&#227;o">
												<RowLayoutColumnInfo OriginX="4"></RowLayoutColumnInfo>
											</Header>
										</igtbl:UltraGridColumn>
										<igtbl:UltraGridColumn Key="Pdf" Width="25px" AllowGroupBy="No" BaseColumnName="Pdf">
											<Footer>
												<RowLayoutColumnInfo OriginX="5"></RowLayoutColumnInfo>
											</Footer>
											<HeaderStyle>
												<BorderDetails StyleTop="None" StyleRight="None"></BorderDetails>
											</HeaderStyle>
											<Header>
												<RowLayoutColumnInfo OriginX="5"></RowLayoutColumnInfo>
											</Header>
                                            <CellStyle>
                                                <BorderDetails StyleRight="None" />
                                            </CellStyle>
										</igtbl:UltraGridColumn>
									</Columns>
									<RowTemplateStyle BorderColor="White" BorderStyle="Ridge" BackColor="White">
										<BorderDetails WidthLeft="3px" WidthTop="3px" WidthRight="3px" WidthBottom="3px"></BorderDetails>
									</RowTemplateStyle>
                                    <AddNewRow View="NotSet" Visible="NotSet">
                                    </AddNewRow>
								</igtbl:UltraGridBand>
							</Bands>
						</igtbl:ultrawebgrid></TD>
				</TR>
				<TR>
					<TD class="defaultFont" align="center"><TABLE class="defaultFont" id="Table1" cellSpacing="0" cellPadding="0" width="590" align="right"
							border="0">
							<TR>
								<TD align="right"><asp:label id="lblTotRegistros" runat="server"></asp:label></TD>
							</TR>
						</TABLE>
						<BR>
						<asp:label id="lblAviso" runat="server"></asp:label><BR>
						<asp:hyperlink id="hlkNovo" runat="server" SkinID="BoldLink">Novo Exame</asp:hyperlink></TD>
				</TR>
			</TABLE>
			<INPUT type="hidden" runat="server" id="txtAuxiliar" name="txtAuxiliar">
            <asp:LinkButton ID="lkbExameDigitalizado" runat="server" OnClick="lkbExameDigitalizado_Click"></asp:LinkButton>
		</form>
	</body>
</HTML>
