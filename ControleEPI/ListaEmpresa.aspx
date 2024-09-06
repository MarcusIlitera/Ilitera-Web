<%@ Page language="c#" Inherits="Ilitera.Net.ControleEPI.ListaEmpresa" StylesheetTheme="MestraNETTheme" Codebehind="ListaEmpresa.aspx.cs" %>
<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD runat="server">
		<title>Ilitera.NET</title>
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script language="javascript" src="../scripts/validador.js"></script>
		<script language="javascript">
		function Inicializa()
		{
			document.getElementById("txtEmpresa").focus();
		}
		
		function CadastroEPI(IdEmpresa, IdUsuario)
		{		    
		    top.window.document.frames.conteudo.window.document.getElementById('txtIdEmpresa').value = IdEmpresa;
			window.location.href = "CadastroEPI.aspx?IdEmpresa=" + IdEmpresa + "&IdUsuario=" + IdUsuario;
		}
		
		function ListaEmpresaMouseOver(gridName, id, objectType)
		{
		    DataGridMouseOverHandler(gridName, id, objectType, 1);
		}
		
		function ListaEmpresaMouseOut(gridName, id, objectType)
		{
		    DataGridMouseOutHandler(gridName, id, objectType, 1);
		}
        
        function UltraWebGridListaEmpresa_CellClickHandler(gridName, cellId, button)
        {
	        if (button == 0)
	        {
	            var cell = igtbl_getCellById(cellId);
	            var IdEmpresaCell = cell.getPrevCell(true);
	            
	            if (cell.Index == 1)
	                CadastroEPI(IdEmpresaCell.getValue(), top.window.document.frames.conteudo.window.document.getElementById('txtIdUsuario').value);
	        }
        }		
		</script>
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" onload="Inicializa()" rightMargin="0">
		<form method="post" runat="server">
			<TABLE class="defaultFont" cellSpacing="0" cellPadding="0" width="620" align="center" border="0">
				<TR>
					<td vAlign="top" align="center" colSpan="2">
						<table cellSpacing="0" cellPadding="0" width="100%" border="0" class="defaultFont">
							<tr>
								<td class="defaultFont" align="center" colSpan="2" height="41">
									<TABLE class="defaultFont" id="Table1" cellSpacing="0" cellPadding="0" width="600" border="0">
										<TR>
											<TD align="left" width="370">
												<TABLE class="defaultFont" id="Table2" cellSpacing="0" cellPadding="0" width="370" align="center"
													border="0">
													<TR>
														<TD align="left" width="330"><asp:label id="lblBuscaEmpresa" runat="server" Width="104px" SkinID="BoldFont">Empresa:</asp:label><asp:textbox id="txtEmpresa" runat="server" Width="215px"></asp:textbox><BR>
															<asp:label id="lblBuscaLocalTrab" runat="server" Width="104px" ForeColor="Gray" SkinID="BoldFont">Local de Trabalho:</asp:label><asp:textbox id="txtLocalTrabalho" runat="server" Width="215px" ReadOnly="True"
																BackColor="#EBEBEB" BorderColor="LightGray"></asp:textbox></TD>
														<TD vAlign="middle" align="center" width="40"><asp:imagebutton id="btnLocalizar" runat="server" BorderWidth="0px" AlternateText="Localizar" ImageUrl="img/busca.gif" OnClick="btnLocalizar_Click"></asp:imagebutton></TD>
													</TR>
												</TABLE>
											</TD>
											<TD vAlign="middle" align="center" width="230"><asp:checkbox id="ckbEnableLocais" runat="server" Width="185px" ToolTip="Visualizar Locais de Trabalho"
													Text="Visualizar Locais de Trabalho das Empresas" AutoPostBack="True" oncheckedchanged="ckbEnableLocais_CheckedChanged" SkinID="BoldFont"></asp:checkbox></TD>
										</TR>
									</TABLE>
									<TABLE class="defaultFont" id="Table3" cellSpacing="0" cellPadding="0" width="600" align="center"
										border="0">
										<TR>
											<TD align="center">
												<asp:image id="Image2" runat="server" ImageUrl="img/5pixel.gif"></asp:image><BR>
												<asp:radiobuttonlist id="rblFiltroLocaisTrabalho" runat="server" Width="370px"
													AutoPostBack="True" CellPadding="0" CellSpacing="0" RepeatDirection="Horizontal" Enabled="False" onselectedindexchanged="rblFiltroLocaisTrabalho_SelectedIndexChanged">
													<asp:ListItem Value="0" Selected="True">Locais Ativos</asp:ListItem>
													<asp:ListItem Value="1">Locais Inativos</asp:ListItem>
													<asp:ListItem Value="2">Locais Ativos e Inativos</asp:ListItem>
												</asp:radiobuttonlist>
												<asp:image id="Image1" runat="server" ImageUrl="img/5pixel.gif"></asp:image></TD>
										</TR>
									</TABLE>
								</td>
							</tr>
							<tr>
								<td align="center" colSpan="2"><igtbl:ultrawebgrid id="UltraWebGridListaEmpresa" runat="server" Width="602px"
										Height="218px" onpageindexchanged="UltraWebGridListaEmpresa_PageIndexChanged"><Bands>
<igtbl:UltraGridBand>
<RowExpAreaStyle BackColor="#F5FAF7"></RowExpAreaStyle>
<Columns>
<igtbl:UltraGridColumn Key="Id" AllowGroupBy="No" BaseColumnName="Id" FooterText="" Hidden="True" Format="" Width="0px" EditorControlID="" FormulaErrorValue="">
<Header Title="" Caption="Id"></Header>

<Footer Title="" Caption=""></Footer>
</igtbl:UltraGridColumn>
<igtbl:UltraGridColumn Key="NomeAbreviado" AllowGroupBy="No" BaseColumnName="NomeAbreviado" FooterText="" Format="" Width="200px" EditorControlID="" FormulaErrorValue="">
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

<SelectedCellStyle BorderStyle="None"></SelectedCellStyle>

<Header Title="" Caption="Nome Fantasia">
<RowLayoutColumnInfo OriginX="1"></RowLayoutColumnInfo>
</Header>

<Footer Title="" Caption="">
<RowLayoutColumnInfo OriginX="1"></RowLayoutColumnInfo>
</Footer>
</igtbl:UltraGridColumn>
<igtbl:UltraGridColumn Key="NomeCompleto" AllowGroupBy="No" BaseColumnName="NomeCompleto" FooterText="" Format="" Width="400px" EditorControlID="" FormulaErrorValue="">
<CellButtonStyle HorizontalAlign="Left" Cursor="Hand" BackColor="#EFFFF6" Font-Bold="True" Font-Names="Verdana" Font-Size="XX-Small" ForeColor="#156047">
<Padding Left="3px"></Padding>

<BorderDetails StyleLeft="None" StyleRight="None" StyleTop="None" StyleBottom="None"></BorderDetails>
</CellButtonStyle>

<HeaderStyle>
<BorderDetails StyleRight="None" StyleTop="None"></BorderDetails>
</HeaderStyle>

<CellStyle HorizontalAlign="Left">
<Padding Left="3px"></Padding>

<BorderDetails StyleRight="None"></BorderDetails>
</CellStyle>

<Header Title="" Caption="Raz&#227;o Social">
<RowLayoutColumnInfo OriginX="2"></RowLayoutColumnInfo>
</Header>

<Footer Title="" Caption="">
<RowLayoutColumnInfo OriginX="2"></RowLayoutColumnInfo>
</Footer>
</igtbl:UltraGridColumn>
</Columns>

<HeaderStyle>
<BorderDetails StyleLeft="None" StyleTop="None"></BorderDetails>
</HeaderStyle>

<RowTemplateStyle BackColor="White" BorderColor="White" BorderStyle="Ridge">
<BorderDetails WidthLeft="3px" WidthTop="3px" WidthRight="3px" WidthBottom="3px"></BorderDetails>
</RowTemplateStyle>

<AddNewRow Visible="NotSet" View="NotSet"></AddNewRow>
</igtbl:UltraGridBand>
</Bands>

<DisplayLayout Version="3.00" Name="UltraWebGridListaEmpresa" RowHeightDefault="18px" TableLayout="Fixed" ViewType="Hierarchical" RowSelectorsDefault="No" AutoGenerateColumns="False">
<FrameStyle BackColor="WhiteSmoke" BorderColor="#7CC5A1" BorderWidth="1px" BorderStyle="Solid" Font-Names="Verdana" Font-Size="XX-Small" Height="218px" Width="602px"></FrameStyle>



<Images ><CollapseImage Url="ig_treeXPMinus.GIF"></CollapseImage><ExpandImage Url="ig_treeXPPlus.GIF"></ExpandImage></Images>

<ClientSideEvents CellClickHandler="UltraWebGridListaEmpresa_CellClickHandler" MouseOverHandler="ListaEmpresaMouseOver" MouseOutHandler="ListaEmpresaMouseOut"></ClientSideEvents>

<Pager PageSize="10" AllowPaging="True" StyleMode="QuickPages" Alignment="Center" PrevText="Anterior" NextText="Pr&#243;ximo" ChangeLinksColor="True" QuickPages="10" Pattern="P&#225;gina &lt;b&gt;[currentpageindex]&lt;/b&gt; de &lt;b&gt;[pagecount]&lt;/b&gt;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&lt;b&gt;[default]&lt;/b&gt;">
<PagerStyle BackColor="#DEEFE4" BorderWidth="0px" BorderStyle="None" Font-Names="Verdana" Font-Size="XX-Small" ForeColor="#44926D" Height="18px"></PagerStyle>
</Pager>

<EditCellStyleDefault BorderWidth="0px" BorderStyle="None"></EditCellStyleDefault>

<FooterStyleDefault BackColor="LightGray" BorderWidth="1px" BorderStyle="Solid">
<BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" WidthTop="1px"></BorderDetails>
</FooterStyleDefault>

<HeaderStyleDefault HorizontalAlign="Center" VerticalAlign="Middle" BackColor="#DEEFE4" Font-Bold="True" Font-Names="Verdana" Font-Size="XX-Small" ForeColor="#44926D">
<Margin Top="0px" Left="0px" Right="0px" Bottom="0px"></Margin>

<Padding Top="0px" Left="0px" Right="0px" Bottom="0px"></Padding>
</HeaderStyleDefault>

<RowStyleDefault HorizontalAlign="Center" BackColor="White" BorderColor="#7CC5A1" BorderWidth="1px" BorderStyle="Solid" Font-Names="Verdana" Font-Size="XX-Small" ForeColor="#156047"></RowStyleDefault>

<GroupByRowStyleDefault BackColor="Control" BorderColor="Window"></GroupByRowStyleDefault>

<GroupByBox Hidden="True">
<BoxStyle BackColor="ActiveBorder" BorderColor="Window"></BoxStyle>
</GroupByBox>

<AddNewBox Hidden="False"></AddNewBox>

<ActivationObject AllowActivation="False" BorderStyle="Solid" BorderColor="124, 197, 161" BorderWidth="1px"></ActivationObject>

<FilterOptionsDefault>
<FilterDropDownStyle CustomRules="overflow:auto;" BackColor="White" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" Font-Names="Verdana,Arial,Helvetica,sans-serif" Font-Size="11px" Height="300px" Width="200px">
<Padding Left="2px"></Padding>
</FilterDropDownStyle>

<FilterHighlightRowStyle BackColor="#151C55" ForeColor="White"></FilterHighlightRowStyle>

<FilterOperandDropDownStyle CustomRules="overflow:auto;" BackColor="White" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" Font-Names="Verdana,Arial,Helvetica,sans-serif" Font-Size="11px">
<Padding Left="2px"></Padding>
</FilterOperandDropDownStyle>
</FilterOptionsDefault>
</DisplayLayout>
</igtbl:ultrawebgrid></td>
							</tr>
						</table>
					</td>
				</TR>
				<tr>
					<td class="defaultFont" align="center">
						<table cellSpacing="0" cellPadding="0" width="600" border="0" class="defaultFont">
							<tr>
								<td width="300"><asp:hyperlink id="lisTodas" runat="server" SkinID="BoldLink">Lista Todas</asp:hyperlink></td>
								<td align="right" width="300"><asp:label id="lblTotRegistros" runat="server"></asp:label></td>
							</tr>
						</table>
						<asp:label id="lblError" runat="server" SkinID="ErrorFont"></asp:label><asp:customvalidator id="cvdCaracteres" runat="server" Display="Dynamic" ControlToValidate="txtEmpresa" OnServerValidate="cvdCaracteres_ServerValidate"></asp:customvalidator><asp:customvalidator id="cvdLocalTrabalho" runat="server" Display="Dynamic" ControlToValidate="txtLocalTrabalho" OnServerValidate="cvdLocalTrabalho_ServerValidate"></asp:customvalidator></td>
				</tr>
			</TABLE>
        
         <eo:MsgBox ID="MsgBox1" runat="server" BackColor="#47729F" ControlSkinID="None" 
        HeaderHtml="Dialog Title" Height="100px" Width="168px">
        <HeaderStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #ffbf00 3px solid; padding-left: 4px; font-weight: bold; font-size: 11px; padding-bottom: 2px; color: white; padding-top: 2px; border-bottom: #22456a 1px solid; font-family: verdana" />
        <ContentStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #7d97b6 1px solid; padding-left: 4px; border-left-width: 1px; font-size: 11px; border-left-color: #728eb8; padding-bottom: 4px; color: white; padding-top: 4px; border-bottom: #22456a 1px solid; font-family: verdana" />
        <FooterStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #7d97b6 1px solid; padding-left: 4px; border-left-width: 1px; font-size: 11px; border-left-color: #728eb8; padding-bottom: 4px; color: white; padding-top: 4px; border-bottom: #22456a 1px solid; font-family: verdana" />
        </eo:MsgBox>
		</form>
	</body>
</HTML>
