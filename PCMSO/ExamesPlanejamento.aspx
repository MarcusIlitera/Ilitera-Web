<%@ Page Language="C#"   AutoEventWireup="True" CodeBehind="ExamesPlanejamento.aspx.cs"  Inherits="Ilitera.Net.PCMSO.ExamesPlanejamento" Title="Ilitera.Net" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Mestra.NET</title>
    <script language="javascript" src="scripts/validador.js"></script>
    <script id="igClientScript" type="text/javascript">    
    function warpPlanejamentoExames_InitializePanel(oPanel)
    {
	    oPanel.getProgressIndicator().setImageUrl("img/loading.gif");
    }
    
    function UltraWebGridPlanejamentoExames_MouseOutHandler(gridName, id, objectType)
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

    function UltraWebGridPlanejamentoExames_MouseOverHandler(gridName, id, objectType)
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
    </script>
</head>
<body bottommargin="0" leftmargin="0" rightmargin="0" topmargin="0">
    <form id="form1" runat="server">
        <table align="center" border="0" cellpadding="0" cellspacing="0" class="defaultFont"
            width="620">
            <tr>
                <td align="center" width="620">
                    <br />
                    <asp:Label ID="lblTitulo" runat="server" SkinID="TitleFont" Width="600px"></asp:Label><br />
                        &nbsp;
                    <br />
                    <igmisc:WebAsyncRefreshPanel ID="warpPlanejamentoExames" runat="server" CssClass="defaultFont"
                        Height="" InitializePanel="warpPlanejamentoExames_InitializePanel" Width="602px">
                    <table id="Table2" align="center" border="0" cellpadding="0" cellspacing="0" class="defaultFont"
                        width="600">
                        <tr>
                            <td align="left">
                                <asp:Label ID="Label6" runat="server" SkinID="BoldFont">Mecanismo de Busca para o Planejamento de Exames</asp:Label><br />
                                    <asp:Panel
                                    ID="Panel1" runat="server" BackColor="WhiteSmoke" BorderColor="Silver" BorderStyle="Solid"
                                    BorderWidth="1px" Width="600px">
                                    <table id="Table4" align="center" border="0" cellpadding="0" cellspacing="0" class="defaultFont"
                                        width="600">
                                        <tr>
                                            <td align="left" width="5">
                                                <asp:Image ID="Image1" runat="server" ImageUrl="img/5pixel.gif" /></td>
                                            <td align="left" width="395">
                                                <asp:Image ID="Image6" runat="server" ImageUrl="img/5pixel.gif" /></td>
                                            <td align="left" width="200">
                                                <asp:Image ID="Image11" runat="server" ImageUrl="img/5pixel.gif" /></td>
                                        </tr>
                                        <tr>
                                            <td align="left" width="5" style="height: 20px">
                                            </td>
                                            <td align="left" width="395" style="height: 20px">
                                                <asp:Label ID="Label10" runat="server" SkinID="DisabledBoldFont" ToolTip="Nome do Empregado">Nome Empregado:</asp:Label>
                                                <asp:TextBox ID="txtNomeEmpregado" runat="server" onkeydown="ProcessaEnter(event, 'imgBusca')"
                                                    Width="220px" ToolTip="Nome do Empregado"></asp:TextBox>&nbsp;<asp:ImageButton ID="imgBusca" runat="server" ImageUrl="img/busca.gif" TabIndex="2" ToolTip="Procurar" OnClick="imgBusca_Click" /></td>
                                            <td align="left" width="200" style="height: 20px">
                                                <asp:CheckBox ID="ckbEmpresaGrupo" runat="server" SkinID="DisabledBoldFont" Text="Todas as empresas do grupo"
                                                    ToolTip="Todas as empresas do grupo" AutoPostBack="True" OnCheckedChanged="ckbEmpresaGrupo_CheckedChanged" /></td>
                                        </tr>
                                    </table>
                                    <table id="Table3" align="center" border="0" cellpadding="0" cellspacing="0" class="defaultFont"
                                        width="600">
                                        <tr>
                                            <td align="left" width="5">
                                                <asp:Image ID="Image2" runat="server" ImageUrl="img/5pixel.gif" /></td>
                                            <td align="left" width="172">
                                                <asp:Image ID="Image3" runat="server" ImageUrl="img/5pixel.gif" /></td>
                                            <td align="center" width="153">
                                            </td>
                                            <td align="center" width="145">
                                                <asp:Image ID="Image4" runat="server" ImageUrl="img/5pixel.gif" /></td>
                                            <td align="center" width="120">
                                            </td>
                                            <td align="center" width="5">
                                                <asp:Image ID="Image5" runat="server" ImageUrl="img/5pixel.gif" /></td>
                                        </tr>
                                        <tr>
                                            <td align="center" width="5">
                                            </td>
                                            <td align="left" width="172">
                                                <table border="0" cellpadding="0" cellspacing="0" class="defaultFont" width="172">
                                                    <tr>
                                                        <td width="32">
                                                <asp:Label ID="Label1" runat="server" SkinID="DisabledBoldFont" ToolTip="Tipo dos Exames">Tipo:</asp:Label></td>
                                                        <td width="140">
                                                <asp:DropDownList ID="ddlTipoExame" runat="server" Width="130px" ToolTip="Tipo dos Exames" AutoPostBack="True" OnSelectedIndexChanged="ddlTipoExame_SelectedIndexChanged">
                                                </asp:DropDownList></td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td align="left" width="153">
                                                <table align="center" border="0" cellpadding="0" cellspacing="0" class="defaultFont"
                                                    width="153">
                                                    <tr>
                                                        <td width="68">
                                                            <asp:Label ID="Label2" runat="server" SkinID="DisabledBoldFont" ToolTip="Renovação até">Renov. até:</asp:Label></td>
                                                        <td width="85">
                                                <igtxt:WebDateTimeEdit ID="wdtRenovacaoData" runat="server" EditModeFormat="dd-MM-yyyy" Width="75px" onkeydown="ProcessaEnter(event, 'imgBusca')" ToolTip="Data limite para Renovação">
                                                </igtxt:WebDateTimeEdit>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td align="left" width="145">
                                                <table align="center" border="0" cellpadding="0" cellspacing="0" class="defaultFont"
                                                    width="145">
                                                    <tr>
                                                        <td width="60">
                                                <asp:Label ID="Label4" runat="server" SkinID="DisabledBoldFont" ToolTip="Vencidos até">Venc. até:</asp:Label></td>
                                                        <td width="85">
                                                            <igtxt:WebDateTimeEdit ID="wdtVencimentoData" runat="server" EditModeFormat="dd-MM-yyyy" Width="75px" onkeydown="ProcessaEnter(event, 'imgBusca')" ToolTip="Data limite para Vencimento">
                                                </igtxt:WebDateTimeEdit>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td align="left" width="120">
                                                <asp:CheckBox ID="ckbDataUltimo" runat="server" SkinID="DisabledBoldFont" Text="Sem Data Último"
                                                    ToolTip="Sem Data do Último exame" AutoPostBack="True" OnCheckedChanged="ckbDataUltimo_CheckedChanged" /></td>
                                            <td align="center" width="5">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" width="5">
                                                <asp:Image ID="Image7" runat="server" ImageUrl="img/5pixel.gif" /></td>
                                            <td align="left" width="172">
                                                <asp:Image ID="Image8" runat="server" ImageUrl="img/5pixel.gif" /></td>
                                            <td align="center" width="153">
                                            </td>
                                            <td align="center" width="145">
                                                <asp:Image ID="Image9" runat="server" ImageUrl="img/5pixel.gif" /></td>
                                            <td align="center" width="120">
                                            </td>
                                            <td align="center" width="5">
                                                <asp:Image ID="Image10" runat="server" ImageUrl="img/5pixel.gif" /></td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                        <br />
                    <igtbl:ultrawebgrid id="UltraWebGridPlanejamentoExames" runat="server" height="237px" width="602px" OnPageIndexChanged="UltraWebGridPlanejamentoExames_PageIndexChanged" style="left: 0px" OnInitializeRow="UltraWebGridPlanejamentoExames_InitializeRow"><Bands>
                        <igtbl:UltraGridBand>
                            <Columns>
                                <igtbl:UltraGridColumn AllowGroupBy="No" BaseColumnName="NomeEmpregadoFull" Hidden="True"
                                    Key="NomeEmpregadoFull" Width="0px">
                                </igtbl:UltraGridColumn>
                                <igtbl:UltraGridColumn AllowGroupBy="No" BaseColumnName="TipoExameFull" Hidden="True"
                                    Key="TipoExameFull" Width="0px">
                                    <Header>
                                        <RowLayoutColumnInfo OriginX="1" />
                                    </Header>
                                    <Footer>
                                        <RowLayoutColumnInfo OriginX="1" />
                                    </Footer>
                                </igtbl:UltraGridColumn>
                                <igtbl:UltraGridColumn AllowGroupBy="No" AllowResize="Fixed" BaseColumnName="NomeEmpregado"
                                    Key="NomeEmpregado" Width="169px">
                                    <HeaderStyle HorizontalAlign="Center">
                                        <BorderDetails StyleLeft="None" StyleTop="None" />
                                    </HeaderStyle>
                                    <CellStyle HorizontalAlign="Left">
                                        <Padding Left="3px" />
                                        <BorderDetails StyleBottom="None" StyleLeft="None" StyleTop="None" />
                                    </CellStyle>
                                    <Header Caption="Empregado" Title="Nome do Empregado">
                                        <RowLayoutColumnInfo OriginX="2" />
                                    </Header>
                                    <Footer>
                                        <RowLayoutColumnInfo OriginX="2" />
                                    </Footer>
                                </igtbl:UltraGridColumn>
                                <igtbl:UltraGridColumn AllowGroupBy="No" BaseColumnName="TipoExame" Key="TipoExame"
                                    Width="120px">
                                    <HeaderStyle HorizontalAlign="Center">
                                        <BorderDetails StyleTop="None" />
                                    </HeaderStyle>
                                    <CellStyle HorizontalAlign="Left">
                                        <Padding Left="3px" />
                                        <BorderDetails StyleBottom="None" StyleTop="None" />
                                    </CellStyle>
                                    <Header Caption="Exame" Title="Tipo de Exame">
                                        <RowLayoutColumnInfo OriginX="3" />
                                    </Header>
                                    <Footer>
                                        <RowLayoutColumnInfo OriginX="3" />
                                    </Footer>
                                </igtbl:UltraGridColumn>
                                <igtbl:UltraGridColumn AllowGroupBy="No" BaseColumnName="DataUltimo" Key="DataUltimo"
                                    Width="90px">
                                    <HeaderStyle HorizontalAlign="Center">
                                        <BorderDetails StyleTop="None" />
                                    </HeaderStyle>
                                    <CellStyle HorizontalAlign="Center">
                                        <BorderDetails StyleBottom="None" StyleTop="None" />
                                    </CellStyle>
                                    <Header Caption="&#218;ltimo" Title="Data do &#218;ltimo Exame">
                                        <RowLayoutColumnInfo OriginX="4" />
                                    </Header>
                                    <Footer>
                                        <RowLayoutColumnInfo OriginX="4" />
                                    </Footer>
                                </igtbl:UltraGridColumn>
                                <igtbl:UltraGridColumn AllowGroupBy="No" BaseColumnName="DataVencimento" Key="DataVencimento"
                                    Width="90px">
                                    <HeaderStyle>
                                        <BorderDetails StyleTop="None" />
                                    </HeaderStyle>
                                    <CellStyle>
                                        <BorderDetails StyleBottom="None" StyleTop="None" />
                                    </CellStyle>
                                    <Header Caption="Vencimento" Title="Data de Vencimento do Exame">
                                        <RowLayoutColumnInfo OriginX="5" />
                                    </Header>
                                    <Footer>
                                        <RowLayoutColumnInfo OriginX="5" />
                                    </Footer>
                                </igtbl:UltraGridColumn>
                                <igtbl:UltraGridColumn AllowGroupBy="No" BaseColumnName="DataRenovacao" Key="DataRenovacao"
                                    Width="90px">
                                    <HeaderStyle HorizontalAlign="Center">
                                        <BorderDetails StyleTop="None" />
                                    </HeaderStyle>
                                    <CellStyle HorizontalAlign="Center">
                                        <Padding Left="3px" />
                                        <BorderDetails StyleBottom="None" StyleTop="None" />
                                    </CellStyle>
                                    <Header Caption="Renova&#231;&#227;o" Title="Data de Renova&#231;&#227;o do Exame">
                                        <RowLayoutColumnInfo OriginX="6" />
                                    </Header>
                                    <Footer>
                                        <RowLayoutColumnInfo OriginX="6" />
                                    </Footer>
                                </igtbl:UltraGridColumn>
                                <igtbl:UltraGridColumn AllowGroupBy="No" BaseColumnName="Preventivo" Key="Preventivo"
                                    Width="25px">
                                    <HeaderStyle>
                                        <BorderDetails StyleTop="None" />
                                    </HeaderStyle>
                                    <CellStyle HorizontalAlign="Center">
                                        <BorderDetails StyleBottom="None" StyleTop="None" />
                                    </CellStyle>
                                    <Header Caption="Pv" Title="Exame Preventivo">
                                        <RowLayoutColumnInfo OriginX="7" />
                                    </Header>
                                    <Footer>
                                        <RowLayoutColumnInfo OriginX="7" />
                                    </Footer>
                                </igtbl:UltraGridColumn>
                            </Columns>
                            <AddNewRow View="NotSet" Visible="NotSet">
                            </AddNewRow>
                        </igtbl:UltraGridBand>
</Bands>

<DisplayLayout Version="4.00" ScrollBar="Always" AutoGenerateColumns="False" Name="UltraWebGridPlanejamentoExames" RowSelectorsDefault="No" ScrollBarView="Vertical" RowHeightDefault="18px" TableLayout="Fixed" StationaryMargins="HeaderAndFooter">
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

<FrameStyle BorderWidth="1px" BorderColor="#7CC5A1" BorderStyle="Solid" Font-Size="XX-Small" Font-Names="Verdana" BackColor="WhiteSmoke" Width="602px" Height="237px"></FrameStyle>

<Pager Alignment="Center" ChangeLinksColor="True" NextText="Pr&#243;ximo" PageSize="800" PrevText="Anterior" Pattern="P&#225;gina &lt;b&gt;[currentpageindex]&lt;/b&gt; de &lt;b&gt;[pagecount]&lt;/b&gt;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&lt;b&gt;[default]&lt;/b&gt;" QuickPages="5" StyleMode="QuickPages" AllowPaging="True">
<PagerStyle ForeColor="#44926D" BorderWidth="1px" BorderStyle="Solid" Font-Size="XX-Small" Font-Names="Verdana" BackColor="#DEEFE4" Height="18px" BorderColor="#7CC5A1">
    <BorderDetails StyleBottom="None" StyleLeft="None" StyleRight="None" StyleTop="Solid" WidthTop="1px" />
</PagerStyle>
</Pager>

<AddNewBox Hidden="False"></AddNewBox>
    <ClientSideEvents MouseOverHandler="UltraWebGridPlanejamentoExames_MouseOverHandler" MouseOutHandler="UltraWebGridPlanejamentoExames_MouseOutHandler" />
</DisplayLayout>
</igtbl:ultrawebgrid>
                                    <table align="center" border="0" cellpadding="0" cellspacing="0" class="defaultFont"
                        width="600" id="TABLE1">
                        <tr>
                            <td align="left" width="200">
                                <asp:LinkButton ID="lkbListarTodos" runat="server" OnClick="lkbListarTodos_Click"
                                    SkinID="BoldLinkButton" ToolTip="Listar todos os Exames do Planejamento">Listar Todos</asp:LinkButton></td>
                            <td align="center" width="200">
                                <asp:Label ID="lblError" runat="server" SkinID="ErrorFont"></asp:Label></td>
                            <td align="right" width="200">
                                <asp:Label ID="lblTotRegistros" runat="server" ToolTip="Total de Exames"></asp:Label></td>
                        </tr>
                    </table>
                        <asp:Image ID="Image16" runat="server" ImageUrl="img/5pixel.gif" /><br/>
                        <asp:LinkButton ID="lkbListagemPlanejamentoExames" runat="server" SkinID="BoldLinkButton" ToolTip="Listagem do Planejamento de Exames">Listagem do Planejamento de Exames <img src="img/print.gif" alt="Listagem do Planejamento de Exames" border="0"></asp:LinkButton></igmisc:WebAsyncRefreshPanel>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
