<%@ Page Language="C#"   AutoEventWireup="True"  MasterPageFile="~/Site.Master" CodeBehind="ListagemIrregularidades.aspx.cs"  Inherits="Ilitera.Net.ListagemIrregularidades" Title="Ilitera.Net" %>

<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>



<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <link href="css/forms.css" rel="stylesheet" type="text/css" />
    <link href="css/tabelas.css" rel="stylesheet" type="text/css" />
  
    <style type="text/css">
        .style1
        {
            width: 114px;
        }
        .style2
        {
            width: 237px;
        }
        .style3
        {
            width: 373px;
        }

        [type="button"], [type="reset"], [type="submit"], button {
            min-width: 120px;
            height: 32px;
            font-family: 'Univia Pro' !important;
            font-style: normal;
            font-weight: normal !important;
            font-size: 12px;
            /*text-align: center;*/
            color: #ffffff !important;
            background: linear-gradient(180deg, #48A79E 54.35%, #1C9489 54.36%);
            border-radius: 5px;
            border: none;
            margin-right: 10px;
            margin-bottom: 5px;
            margin-top: 20px;
        }

            [type="button"]:hover, [type="reset"]:hover, [type="submit"]:hover, button:hover {
                color: #ffffff !important;
                background: linear-gradient(180deg, #F2B988 53.35%, #F09E60 53.36%);
                border-radius: 5px;
            }
    </style>
  
</asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent" >
    <div class="container-fluid d-flex ms-5 ps-4">
        <div class="row gx-3 gy-2">

<%--<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD runat="server">
		<title>Mestra.NET</title>
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
--%>		


        <script language="javascript" src="../scripts/validador.js"></script>
        <script language="javascript">
            var g_itemIndex = -1;
            var g_cellIndex = -1;
            function OnCellSelected(grid) {
                var cell = grid.getSelectedCell();
                grid.raiseItemCommandEvent(cell.getItemIndex(), "Seleção");
            }
            function OnItemCommand(grid, itemIndex, colIndex, commandName) {
                //grid.raiseItemCommandEvent(itemIndex, commandName);
                grid.raiseItemCommandEvent(itemIndex, colIndex.toString());
            }
            function centerWin(url, width, height, name) { // Pop no centro da Tela
                var centerX = (screen.width - width) / 2; //Need to subtract the window width/height so that it appears centered. If you didn't, the top left corner of the window would be centered.
                var centerY = (screen.height - height) / 2;
                janela = window.open(url, name, "toolbar=no,location=no,directories=no,status=no,menubar=no,scrollbars=no,resizable=no,copyhistory=no,width=" + width + ", height=" + height + ",top=" + centerY + ", left=" + centerX);
                //addItemPop(janela);
                //AddItem(janela,'Todos');
                return janela;
            }
            function DetalheIrregularidade(IdIrregularidade, IdEmpresa, IdUsuario) {
                void (addItem(centerWin("DetalheIrregularidade.aspx?IdIrregularidade=" + IdIrregularidade + "&IdUsuario=" + IdUsuario + "&IdEmpresa=" + IdEmpresa + "", 500, 360,
                    "DetalheIrregularidade"), "Todos"));
            }
            function IrregularidadesMouseOver(gridName, id, objectType) {
                DataGridMouseOverHandler(gridName, id, objectType, 1);
            }
            function IrregularidadesMouseOut(gridName, id, objectType) {
                DataGridMouseOutHandler(gridName, id, objectType, 1);
            }
            function Irregularidades_CellClickHandler(gridName, cellId, button) {
                if (button == 0) {
                    var cell = igtbl_getCellById(cellId);
                    var IdIrregularidadeCell = cell.getPrevCell(true);
                    if (cell.Index == 1 && cell.getElement().innerText.Trim() != "")
                        DetalheIrregularidade(IdIrregularidadeCell.getValue(), top.window.document.frames.conteudo.window.document.getElementById('txtIdEmpresa').value,
                            top.window.document.frames.conteudo.window.document.getElementById('txtIdUsuario').value);
                }
            }
        </script>
<%--	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0">
		<form method="post" runat="server">--%>

            <div class="col-11 subtituloBG mb-3" style="padding-top: 10px;">
                <asp:label id="lblExCli" runat="server" SkinID="TitleFont" CssClass="subtitulo">Auditoria / Listagem de Irregularidades</asp:label>
            </div>

            <div class="col-11 mb-3">
                <div class="row">
                    <div class="col-md-4 gx-3 gy-2">
                        <asp:label id="lblAuditoria" runat="server" SkinID="BoldFont" CssClass="tituloLabel">Auditoria</asp:label>
                        <div class="row">
                            <div class="col-11">
                                <asp:dropdownlist id="ddlAuditoria" runat="server" AutoPostBack="True" CssClass="texto form-select" onselectedindexchanged="ddlAuditoria_SelectedIndexChanged"></asp:dropdownlist>
                            </div>

                            <div class="col-1 gx-2">
                                <asp:imagebutton id="imbAuditoria" runat="server" ToolTip="Imprimir Auditoria Completa" ImageUrl="Images/printer.svg" CssClass="btnMenor" style="padding: .3rem;" OnClick="imbAuditoria_Click"></asp:imagebutton>
                            </div>
                        </div>
                    </div>

                    <div class="col-md-4 gx-3 gy-2" style="margin-top: 30px; padding-left: 5rem;">
                        <asp:radiobuttonlist id="rblFiltro" runat="server" AutoPostBack="True" CellPadding="0" RepeatDirection="Horizontal" CellSpacing="0" 
                            CssClass="texto form-check-input bg-transparent border-0" Width="200" onselectedindexchanged="rblFiltro_SelectedIndexChanged">
                            <asp:ListItem Value="T" Selected="True">Todas</asp:ListItem>
                            <asp:ListItem Value="R">Regul.</asp:ListItem>
                            <asp:ListItem Value="P">Pend.</asp:ListItem>
                        </asp:radiobuttonlist>
                    </div>

                    <div class="col-md-4 gx-3 gy-2">
                        <asp:label id="lblBuscaIrregularidade" runat="server" SkinID="BoldFont" CssClass="tituloLabel">Irregularidade</asp:label>
                        <div class="row">
                            <div class="col-11">
                                <asp:textbox id="txtIrregula" runat="server" CssClass="texto form-control" onkeydown="ProcessaEnter(event, 'btnLocalizar')"></asp:textbox>
                            </div>

                            <div class="col-1 gx-2">
                                <asp:imagebutton id="btnLocalizar" runat="server" ImageUrl="Images/search.svg" BorderWidth="0px" AlternateText="Buscar Irregularidade" CssClass="btnMenor" style="padding: .3rem;" OnClick="btnLocalizar_Click"></asp:imagebutton>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="col-11 gx-3 gy-2">
                <eo:Grid ID="UltraWebGridIrregularidades" runat="server" ClientSideOnItemCommand="OnItemCommand" ColumnHeaderAscImage="00050403" 
                    ColumnHeaderDescImage="00050404" ColumnHeaderDividerOffset="6" ColumnHeaderHeight="30" FixedColumnCount="1" 
                    GridLines="Both" Height="368px" ItemHeight="30" KeyField="IdIrregularidade" Width="1060px" OnItemCommand="UltraWebGridIrregularidades_ItemCommand"
                    ClientSideOnCellSelected="OnCellSelected">
                    <ItemStyles>
                        <eo:GridItemStyleSet>
                            <ItemStyle CssText="background-color: #FAFAFA;" />
                            <ItemHoverStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500;  text-align: left; height: 35px !important; background: rgba(28, 148, 137, 0.80); color: #FFFFFF; padding: 0.3rem 0.3rem 0.3rem 0.5rem; " />
                            <AlternatingItemStyle CssText="background-color: #F2F2F2;" />
                            <AlternatingItemHoverStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500;  text-align: left; height: 35px !important; background: rgba(28, 148, 137, 0.80); color: #FFFFFF; padding: 0.3rem 0.3rem 0.3rem 0.5rem;" />
                            <SelectedStyle CssText="border-bottom-style:solid;border-left-style:solid;border-right-style:solid;border-top-style:solid;" />
                            <CellStyle CssText="padding: 0.3rem 0.3rem 0.3rem 0.5rem; height: 35px !important;" />
                        </eo:GridItemStyleSet>
                    </ItemStyles>
                    <ColumnHeaderStyle CssClass="tabelaC colunas" />
                    <Columns>
                        <eo:StaticColumn AllowSort="True" DataField="IdIrregularidade" HeaderText="IdIrregularidade" 
                            Name="IdIrregularidade" ReadOnly="True" SortOrder="Ascending" Text="" Width="0">
                            <CellStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500; color: #B0ABAB !important; text-align: left; height: 35px !important; padding: 0.3rem 0.3rem 0.3rem 0.5rem; height: 35px !important;" />
                        </eo:StaticColumn>
                        
                        <eo:StaticColumn AllowSort="True" DataField="Norma" HeaderText="Norma" 
                            Name="Norma" ReadOnly="True" Width="100">
                            <CellStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500; color: #B0ABAB !important; text-align: left; height: 35px !important; padding: 0.3rem 0.3rem 0.3rem 0.5rem; height: 35px !important;" />
                        </eo:StaticColumn>
                        
                        <eo:StaticColumn AllowSort="True" DataField="Local" HeaderText="Local" 
                            Name="Local" ReadOnly="True" Width="180">
                            <CellStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500; color: #B0ABAB !important; text-align: left; height: 35px !important; padding: 0.3rem 0.3rem 0.3rem 0.5rem; height: 35px !important;" />
                        </eo:StaticColumn>
                        
                        <eo:StaticColumn AllowSort="True" DataField="DescricaoIrregularidade" HeaderText="Descricao da Irregularidade" 
                            Name="DescricaoIrregularidade" ReadOnly="True" Width="640">
                            <CellStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500; color: #B0ABAB !important; text-align: left; height: 35px !important; padding: 0.3rem 0.3rem 0.3rem 0.5rem; height: 35px !important;" />
                        </eo:StaticColumn>
                        
                        <eo:ButtonColumn ButtonText="..." Name="Gerar" Width="40">
                            <ButtonStyle CssText="background-image:url('img/print.gif');" />
                            <CellStyle CssText="" />
                        </eo:ButtonColumn>
                        
                        <eo:ButtonColumn Name="Detalhe" ButtonText="Detalhe" Width="70">
                        </eo:ButtonColumn>
                    </Columns>
                    <columntemplates>
                        <eo:TextBoxColumn>
                            <TextBoxStyle CssText="BORDER-RIGHT: #7f9db9 1px solid; PADDING-RIGHT: 2px; BORDER-TOP: #7f9db9 1px solid; PADDING-LEFT: 2px; FONT-SIZE: 8.75pt; PADDING-BOTTOM: 1px; MARGIN: 0px; BORDER-LEFT: #7f9db9 1px solid; PADDING-TOP: 2px; BORDER-BOTTOM: #7f9db9 1px solid; FONT-FAMILY: Tahoma" />
                        </eo:TextBoxColumn>
                        <eo:DateTimeColumn>
                            <datepicker controlskinid="None" daycellheight="16" daycellwidth="19" 
                                dayheaderformat="FirstLetter" disableddates="" othermonthdayvisible="True" 
                                selecteddates="" titleleftarrowimageurl="DefaultSubMenuIconRTL" 
                                titlerightarrowimageurl="DefaultSubMenuIcon">
                                <PickerStyle 
                                    CssText="border-bottom-color:#7f9db9;border-bottom-style:solid;border-bottom-width:1px;border-left-color:#7f9db9;border-left-style:solid;border-left-width:1px;border-right-color:#7f9db9;border-right-style:solid;border-right-width:1px;border-top-color:#7f9db9;border-top-style:solid;border-top-width:1px;font-family:Courier New;font-size:8pt;margin-bottom:0px;margin-left:0px;margin-right:0px;margin-top:0px;padding-bottom:1px;padding-left:2px;padding-right:2px;padding-top:2px;" />
                                <CalendarStyle 
                                    CssText="background-color: white; border-right: #7f9db9 1px solid; padding-right: 4px; border-top: #7f9db9 1px solid; padding-left: 4px; font-size: 9px; padding-bottom: 4px; border-left: #7f9db9 1px solid; padding-top: 4px; border-bottom: #7f9db9 1px solid; font-family: tahoma" />
                                <TitleStyle CssText="background-color:#9ebef5;font-family:Tahoma;font-size:12px;padding-bottom:2px;padding-left:6px;padding-right:6px;padding-top:2px;" />
                                <TitleArrowStyle CssText="cursor:hand" />
                                <MonthStyle CssText="font-family: tahoma; font-size: 12px; margin-left: 14px; cursor: hand; margin-right: 14px" />
                                <DayHeaderStyle CssText="font-family: tahoma; font-size: 12px; border-bottom: #aca899 1px solid" />
                                <DayStyle CssText="font-family: tahoma; font-size: 12px; border-right: white 1px solid; border-top: white 1px solid; border-left: white 1px solid; border-bottom: white 1px solid" />
                                <DayHoverStyle CssText="font-family: tahoma; font-size: 12px; border-right: #fbe694 1px solid; border-top: #fbe694 1px solid; border-left: #fbe694 1px solid; border-bottom: #fbe694 1px solid" />
                                <TodayStyle CssText="font-family: tahoma; font-size: 12px; border-right: #bb5503 1px solid; border-top: #bb5503 1px solid; border-left: #bb5503 1px solid; border-bottom: #bb5503 1px solid" />
                                <SelectedDayStyle CssText="font-family: tahoma; font-size: 12px; background-color: #fbe694; border-right: white 1px solid; border-top: white 1px solid; border-left: white 1px solid; border-bottom: white 1px solid" />
                                <DisabledDayStyle CssText="font-family: tahoma; font-size: 12px; color: gray; border-right: white 1px solid; border-top: white 1px solid; border-left: white 1px solid; border-bottom: white 1px solid" />
                                <OtherMonthDayStyle CssText="font-family: tahoma; font-size: 12px; color: gray; border-right: white 1px solid; border-top: white 1px solid; border-left: white 1px solid; border-bottom: white 1px solid" />
                            </datepicker>
                        </eo:DateTimeColumn>
                        <eo:MaskedEditColumn>
                            <maskededit controlskinid="None" 
                                textboxstyle-csstext="BORDER-RIGHT: #7f9db9 1px solid; PADDING-RIGHT: 2px; BORDER-TOP: #7f9db9 1px solid; PADDING-LEFT: 2px; PADDING-BOTTOM: 1px; MARGIN: 0px; BORDER-LEFT: #7f9db9 1px solid; PADDING-TOP: 2px; BORDER-BOTTOM: #7f9db9 1px solid; font-family:Courier New;font-size:8pt;">
                            </maskededit>
                        </eo:MaskedEditColumn>
                    </columntemplates>
                    <FooterStyle CssText="padding-bottom:4px;padding-left:4px;padding-right:4px;padding-top:4px;" />
                </eo:Grid>
            </div>

            <div class="col-11">
                <div class="text-center">
                    <asp:linkbutton id="lbtListaIrre" runat="server" CssClass="btn" style="font-weight: normal !important;" onclick="lbtListaIrre_Click">Lista Todas</asp:linkbutton>
                    <asp:linkbutton id="lkbIntroducaoAuditoria" tabIndex="14" runat="server" SkinID="BoldLinkButton" CssClass="btn" style="font-weight: normal !important;" onclick="lkbIntroducaoAuditoria_Click"><img src="Images/printer.svg" border="0" alt=" " style="padding: .3rem;" /> Introdução</asp:linkbutton>
                    <asp:linkbutton id="lkbListagemIrregSimples" tabIndex="14" runat="server" SkinID="BoldLinkButton" CssClass="btn" style="font-weight: normal !important;" onclick="lkbListagemIrregSimples_Click"><img src="Images/printer.svg" border="0" alt=" " style="padding: .3rem;" />Listagem das Irregularidades</asp:linkbutton>
                    <asp:linkbutton id="lkbListagemIrregCompleta" tabIndex="14" runat="server" SkinID="BoldLinkButton" CssClass="btn" style="font-weight: normal !important;" onclick="lkbListagemIrregCompleta_Click"><img src="Images/printer.svg" border="0" alt=" " style="padding: .3rem;" />Listagem do plano de ação</asp:linkbutton>
                </div>
            </div>

            <asp:label id="lblError" runat="server" SkinID="ErrorFont"></asp:label>
            <asp:customvalidator id="cvdCaracteres" runat="server" ControlToValidate="txtIrregula" Display="Dynamic" OnServerValidate="cvdCaracteres_ServerValidate"></asp:customvalidator>
            <asp:label id="lblTotRegistros" runat="server"></asp:label>

			
<%--                                <igtbl:ultrawebgrid id="UltraWebGridIrregularidades" runat="server" Width="602px" Height="218px" OnPageIndexChanged="UltraWebGridIrregularidades_PageIndexChanged" ImageDirectory=""><Bands>
<igtbl:UltraGridBand GroupByRowDescriptionMask="[caption] : [value] ([count])   --   Relat&#243;rio: &lt;a href=&quot;javascript:RelatorioN('[value]')&quot;&gt;Por Nome&lt;/a&gt; ou &lt;a href=&quot;javascript:RelatorioD('[value]')&quot;&gt;Por Data&lt;/a&gt;"><Columns>
<igtbl:UltraGridColumn Key="Id" AllowGroupBy="No" BaseColumnName="IdIrregularidade" FooterText="" Hidden="True" Format="" Width="0px" EditorControlID="" FormulaErrorValue="">
<HeaderStyle>
<BorderDetails StyleLeft="None" StyleTop="None"></BorderDetails>
</HeaderStyle>
<CellStyle>
<BorderDetails StyleLeft="None"></BorderDetails>
</CellStyle>
<Header Caption="Id"></Header>
<Footer Caption="" Key="TipoDocumento"></Footer>
</igtbl:UltraGridColumn>
<igtbl:UltraGridColumn Key="Norma" AllowGroupBy="No" BaseColumnName="Norma" FooterText="" Format="" Width="68px" EditorControlID="" FormulaErrorValue="">
<CellButtonStyle HorizontalAlign="Left" Cursor="Hand" BackColor="#EFFFF6" BorderWidth="0px" BorderStyle="None" Font-Bold="True" Font-Names="Verdana" Font-Size="XX-Small" Font-Underline="True" ForeColor="#156047">
<Padding Left="3px"></Padding>
</CellButtonStyle>
<HeaderStyle VerticalAlign="Middle">
<BorderDetails StyleLeft="None" StyleTop="None"></BorderDetails>
</HeaderStyle>
<CellStyle HorizontalAlign="Left" VerticalAlign="Middle" Font-Bold="True">
<Padding Left="3px"></Padding>
<BorderDetails StyleLeft="None"></BorderDetails>
</CellStyle>
<Header Caption="Norma">
<RowLayoutColumnInfo OriginX="1"></RowLayoutColumnInfo>
</Header>
<Footer Caption="" Key="ImpressoesEmpresa">
<RowLayoutColumnInfo OriginX="1"></RowLayoutColumnInfo>
</Footer>
</igtbl:UltraGridColumn>
<igtbl:UltraGridColumn Key="Local" AllowGroupBy="No" BaseColumnName="Local" FooterText="" Format="" Width="177px" EditorControlID="" FormulaErrorValue="">
<HeaderStyle VerticalAlign="Middle">
<BorderDetails StyleTop="None"></BorderDetails>
</HeaderStyle>
<CellStyle HorizontalAlign="Left" VerticalAlign="Middle">
<Padding Left="3px"></Padding>
</CellStyle>
<SelectedCellStyle BorderStyle="None"></SelectedCellStyle>
<Header Caption="Local/Setor">
<RowLayoutColumnInfo OriginX="2"></RowLayoutColumnInfo>
</Header>
<Footer Caption="" Key="ImpressoesUsuario">
<RowLayoutColumnInfo OriginX="2"></RowLayoutColumnInfo>
</Footer>
</igtbl:UltraGridColumn>
<igtbl:UltraGridColumn Key="DescricaoIrregularidade" AllowGroupBy="No" BaseColumnName="DescricaoIrregularidade" FooterText="" Format="" Width="330px" EditorControlID="" FormulaErrorValue="">
<HeaderStyle VerticalAlign="Middle">
<BorderDetails StyleRight="None" StyleTop="None"></BorderDetails>
</HeaderStyle>
<CellStyle HorizontalAlign="Left" VerticalAlign="Middle">
<Padding Left="3px"></Padding>
<BorderDetails StyleRight="None"></BorderDetails>
</CellStyle>
<SelectedCellStyle BorderStyle="None"></SelectedCellStyle>
<Header Caption="A&#231;&#245;es a Executar">
<RowLayoutColumnInfo OriginX="3"></RowLayoutColumnInfo>
</Header>
<Footer Caption="" Key="DataLastImpressao">
<RowLayoutColumnInfo OriginX="3"></RowLayoutColumnInfo>
</Footer>
</igtbl:UltraGridColumn>
<igtbl:UltraGridColumn Key="Pdf" AllowGroupBy="No" BaseColumnName="Pdf" FooterText="" Format="" Width="25px" EditorControlID="" FormulaErrorValue="">
<HeaderStyle>
<BorderDetails StyleRight="None" StyleTop="None"></BorderDetails>
</HeaderStyle>
<CellStyle HorizontalAlign="Center" VerticalAlign="Middle">
<BorderDetails StyleRight="None"></BorderDetails>
</CellStyle>
<Header Caption="">
<RowLayoutColumnInfo OriginX="4"></RowLayoutColumnInfo>
</Header>
<Footer Caption="">
<RowLayoutColumnInfo OriginX="4"></RowLayoutColumnInfo>
</Footer>
</igtbl:UltraGridColumn>
</Columns>
<RowTemplateStyle BackColor="White" BorderColor="White" BorderStyle="Ridge">
<BorderDetails WidthLeft="3px" WidthTop="3px" WidthRight="3px" WidthBottom="3px"></BorderDetails>
</RowTemplateStyle>
<AddNewRow Visible="NotSet" View="NotSet"></AddNewRow>
</igtbl:UltraGridBand>
</Bands>
<DisplayLayout Version="3.00" Name="UltraWebGridIrregularidades" RowHeightDefault="18px" TableLayout="Fixed" RowSelectorsDefault="No" AutoGenerateColumns="False">
<FrameStyle BackColor="WhiteSmoke" BorderColor="#7CC5A1" BorderWidth="1px" BorderStyle="Solid" Height="218px" Width="602px"></FrameStyle>
<Images  ImageDirectory=""></Images>
<ClientSideEvents CellClickHandler="Irregularidades_CellClickHandler" MouseOverHandler="IrregularidadesMouseOver" MouseOutHandler="IrregularidadesMouseOut"></ClientSideEvents>
<Pager PageSize="10" AllowPaging="True" StyleMode="QuickPages" Alignment="Center" PrevText="Anterior" NextText="Pr&#243;ximo" ChangeLinksColor="True" QuickPages="10" Pattern="P&#225;gina &lt;b&gt;[currentpageindex]&lt;/b&gt; de &lt;b&gt;[pagecount]&lt;/b&gt;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&lt;b&gt;[default]&lt;/b&gt;">
<PagerStyle HorizontalAlign="Center" VerticalAlign="Middle" BackColor="#DEEFE4" BorderWidth="0px" BorderStyle="None" Font-Bold="False" Font-Names="Verdana" Font-Size="XX-Small" ForeColor="#44926D" Height="18px"></PagerStyle>
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
</DisplayLayout>
</igtbl:ultrawebgrid>
--%>

                                    
<%--		</form>
	</body>
</HTML>
--%>

    <eo:MsgBox ID="MsgBox1" runat="server" BackColor="#ffffff" ControlSkinID="None" HeaderHtml="Dialog Title" Height="192px" Width="345px" CssClass="card border border-1 p-2 text-center" IconUrl="Images/alerta_icon.svg">
            <HeaderStyleActive CssText="font-family: Ubuntu; font-size: 14px; font-weight: bold; color: #1C9489; text-align: center; padding: 5px;" />
            <ContentStyleActive CssText="font-family: Ubuntu; font-size: 14px; font-weight: bold; color: #7D7B7B; text-align: center; padding: 5px; width: 345px; height: 60px" />
            <FooterStyleActive CssText="width: 345px;" />
        </eo:MsgBox>

        </div>
    </div>
</asp:Content>