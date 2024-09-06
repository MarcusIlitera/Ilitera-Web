<%@ Page Language="C#"  MasterPageFile="~/Site1.Master" AutoEventWireup="True" CodeBehind="ListaEmpresa.aspx.cs"  Inherits="Ilitera.Net.PCMSO.ListaEmpresa" Title="Ilitera.Net" %>
<%@ Register TagPrefix="uc1" TagName="Menu" Src="~/ucMenuLateral.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <ig:WebSplitter runat="server" ID="TestePainel" Height="700px">
        <Panes>
            <ig:SplitterPane Size="20%" Style="padding: 5px;" BackColor="#edffeb">
                <Template>
                    <uc1:Menu runat="server" ID="Menu" />
                </Template>
            </ig:SplitterPane>
             <ig:SplitterPane Size="80%" Style="padding: 5px;" BackColor="#edffeb">
              <Template>
<%--<HTML>
	<HEAD>
--%>		
	<%--	<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="scripts/style.css" type="text/css" rel="stylesheet">--%>
<%--		<script language="javascript" src="../scripts/validador.js"></script>
		<script language="javascript">
		function Inicializa()
		{
			document.getElementById("txtEmpresa").focus();
		}
		
		function ListaEmpregado(IdEmpresa, IdUsuario)
		{
		    //top.window.document.frames.conteudo.window.document.getElementById('txtIdEmpresa').value = IdEmpresa;
		    document.getElementById('txtIdEmpresa').value = IdEmpresa;
			window.location.href = "ListaEmpregado.aspx?IdEmpresa=" + IdEmpresa + "&IdUsuario=" + IdUsuario;
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
	                //ListaEmpregado(IdEmpresaCell.getValue(), top.window.document.frames.conteudo.window.document.getElementById('txtIdUsuario').value);
	                ListaEmpregado(IdEmpresaCell.getValue(), document.getElementById('txtIdUsuario').value);
	        }
        }		
		</script>
--%><%--	</HEAD>--%>

<%--	<body bottomMargin="0" leftMargin="0" topMargin="0" onload="Inicializa()" rightMargin="0">
		<form method="post" runat="server">--%>
			<TABLE class="normalFont" id="Table3" cellSpacing="0" cellPadding="0" width="620" align="center"
				border="0">
				<TR>
					<TD vAlign="top" align="center" colSpan="2">
						<TABLE id="Table4" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<TR>
								<TD class="normalFont" align="center" colSpan="2" height="41">
									<TABLE class="normalFont" id="Table1" cellSpacing="0" cellPadding="0" width="600" border="0">
										<TR>
											<TD align="left" width="370">
												<TABLE class="normalFont" id="Table2" cellSpacing="0" cellPadding="0" width="370" align="center"
													border="0">
													<TR>
														<TD align="left" width="330">
															<asp:label id="lblBuscaEmpresa" runat="server" CssClass="boldFont" Width="104px">Empresa:</asp:label>
															<asp:textbox id="txtEmpresa" runat="server" CssClass="inputBox" Width="215px"></asp:textbox><BR>
															<asp:label id="lblBuscaLocalTrab" runat="server" CssClass="boldFont" Width="104px" ForeColor="Gray">Local de Trabalho:</asp:label>
															<asp:textbox id="txtLocalTrabalho" runat="server" CssClass="inputBox" Width="215px" BackColor="#EBEBEB"
																BorderColor="LightGray" ReadOnly="True"></asp:textbox></TD>
														<TD vAlign="middle" align="center" width="40">
															<asp:imagebutton id="btnLocalizar" runat="server" BorderWidth="0px" AlternateText="Localizar" ImageUrl="img/busca.gif" OnClick="btnLocalizar_Click"></asp:imagebutton></TD>
													</TR>
												</TABLE>
											</TD>
											<TD vAlign="middle" align="center" width="230">
												<asp:checkbox id="ckbEnableLocais" runat="server" CssClass="boldFont" Width="185px" ToolTip="Visualizar Locais de Trabalho"
													Text="Visualizar Locais de Trabalho das Empresas" AutoPostBack="True" ></asp:checkbox></TD>
<%--												<asp:checkbox id="Checkbox1" runat="server" CssClass="boldFont" Width="185px" ToolTip="Visualizar Locais de Trabalho"
													Text="Visualizar Locais de Trabalho das Empresas" AutoPostBack="True" oncheckedchanged="ckbEnableLocais_CheckedChanged"></asp:checkbox></TD>--%>
										</TR>
									</TABLE>
									<TABLE class="normalFont" id="Table6" cellSpacing="0" cellPadding="0" width="600" align="center"
										border="0">
										<TR>
											<TD align="center">
												<asp:image id="Image2" runat="server" ImageUrl="img/5pixel.gif"></asp:image><BR>
												<asp:radiobuttonlist id="rblFiltroLocaisTrabalho" runat="server" Width="370px" CssClass="normalFont"
													AutoPostBack="True" Enabled="False" RepeatDirection="Horizontal" CellSpacing="0" CellPadding="0" onselectedindexchanged="rblFiltroLocaisTrabalho_SelectedIndexChanged">
													<asp:ListItem Value="0" Selected="True">Locais Ativos</asp:ListItem>
													<asp:ListItem Value="1">Locais Inativos</asp:ListItem>
													<asp:ListItem Value="2">Locais Ativos e Inativos</asp:ListItem>
												</asp:radiobuttonlist>
												<asp:TextBox ID="txtIdUsuario" runat="server" Visible="False" Width="40px"></asp:TextBox>												
                                                <asp:TextBox ID="txtIdEmpregado" runat="server" Visible="False" Width="40px"></asp:TextBox>
                                                <asp:image id="Image1" runat="server" ImageUrl="img/5pixel.gif"></asp:image>
                                                    <asp:Label ID="lblEmp" runat="server" Text="Label" Visible="False"></asp:Label>
                                            </TD>
										</TR>
									</TABLE>
								</TD>
							</TR>
							<TR>
								<TD align="center" colSpan="2">
									<igtbl:ultrawebgrid id="UltraWebGridListaEmpresa" runat="server" Width="602px" Height="218px" onpageindexchanged="UltraWebGridListaEmpresa_PageIndexChanged"><Bands>
<igtbl:UltraGridBand>
<RowExpAreaStyle BackColor="#F5FAF7"></RowExpAreaStyle>
<Columns>
<igtbl:UltraGridColumn Key="Id" AllowGroupBy="No" BaseColumnName="Id" FooterText="" Hidden="True" Format="" Width="0px" EditorControlID="" FormulaErrorValue="">
<Header Title="" Caption="Id"></Header>

<Footer Title="" Caption=""></Footer>
</igtbl:UltraGridColumn>
<igtbl:UltraGridColumn Key="NomeAbreviado" AllowGroupBy="No" BaseColumnName="NomeAbreviado" FooterText="" Format="" Width="200px" EditorControlID="" ChangeLinksColor="True" FormulaErrorValue="">
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

<%--<ClientSideEvents CellClickHandler="UltraWebGridListaEmpresa_CellClickHandler" MouseOverHandler="ListaEmpresaMouseOver" MouseOutHandler="ListaEmpresaMouseOut"></ClientSideEvents>--%>


<Pager PageSize="10" AllowPaging="True" StyleMode="QuickPages" Alignment="Center" PrevText="Anterior" NextText="Pr&#243;ximo" ChangeLinksColor="True" QuickPages="10" Pattern="P&#225;gina &lt;b&gt;[currentpageindex]&lt;/b&gt; de &lt;b&gt;[pagecount]&lt;/b&gt;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&lt;b&gt;[default]&lt;/b&gt;">
<PagerStyle BackColor="#DEEFE4" BorderWidth="0px" BorderStyle="Solid" Font-Names="Verdana" Font-Size="XX-Small" ForeColor="#44926D" Height="18px"></PagerStyle>
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
</igtbl:ultrawebgrid></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD class="normalFont" align="center">
						<TABLE id="Table5" cellSpacing="0" cellPadding="0" width="600" border="0">
							<TR>
								<TD width="300">
									<asp:hyperlink id="lisTodas" runat="server" CssClass="boldFont">Lista 
                Todas</asp:hyperlink></TD>
								<TD align="right" width="300">
									<asp:label id="lblTotRegistros" runat="server" CssClass="normalFont"></asp:label></TD>
							</TR>
						</TABLE>
						<asp:label id="lblError" runat="server" CssClass="errorFont"></asp:label>
						<asp:customvalidator id="cvdCaracteres" runat="server" CssClass="errorFont" ControlToValidate="txtEmpresa"
							Display="Dynamic" OnServerValidate="cvdCaracteres_ServerValidate"></asp:customvalidator>
						<asp:customvalidator id="cvdLocalTrabalho" runat="server" CssClass="errorFont" ControlToValidate="txtLocalTrabalho"
							Display="Dynamic" OnServerValidate="cvdLocalTrabalho_ServerValidate"></asp:customvalidator></TD>
				</TR>
			</TABLE>
<%--		</form>
	</body>--%>
<%--</HTML>
--%>
</Template>
            </ig:SplitterPane>
        </Panes>
    </ig:WebSplitter>

    </asp:Content>