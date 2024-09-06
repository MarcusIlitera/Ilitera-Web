<%@ Page Language="C#"   AutoEventWireup="True"  MasterPageFile="~/Site.Master" CodeBehind="ListaProcedimento.aspx.cs"  Inherits="Ilitera.Net.ListaProcedimento" Title="Ilitera.Net" %>
 

<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>


<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">

	<link href="css/forms.css" rel="stylesheet" type="text/css" />
    <link href="css/tabelas.css" rel="stylesheet" type="text/css" />
    <link href="css/sites.css" rel="stylesheet" type="text/css" />

    <style type="text/css">
    .defaultFont
    {
        width: 627px;
    }
        .table
        {}
    </style>
</asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent" >
	<div class="container-fluid d-flex ms-5 ps-4">
	<div class="row gx-3 gy-2">

    <script language="javascript">    
        function CadastroProcedimento(IdProcedimento, IdEmpresa, IdUsuario) {
            window.location.href = "CadProcedimento.aspx?IdProcedimento=" + IdProcedimento + "&IdEmpresa=" + IdEmpresa + "&IdUsuario=" + IdUsuario;
        }

        function ProcedimentosMouseOver(gridName, id, objectType) {
            DataGridMouseOverHandler(gridName, id, objectType, 1);
        }

        function ProcedimentosMouseOut(gridName, id, objectType) {
            DataGridMouseOutHandler(gridName, id, objectType, 1);
        }

        function Procedimentos_CellClickHandler(gridName, cellId, button) {
            if (button == 0) {
                var cell = igtbl_getCellById(cellId);
                var IdProcedimentoCell = cell.getPrevCell(true);

                if (cell.Index == 1)
                    CadastroProcedimento(IdProcedimentoCell.getValue(), document.getElementById('txtIdEmpresa').value,
                        document.getElementById('txtIdUsuario').value);
            }
        }
        var g_itemIndex = -1;
        var g_cellIndex = -1;
        function OnContextMenuItemClicked(e, eventInfo) {
            var grid = eo_GetObject("<%=gridEmpregados.ClientID%>");
            var item = eventInfo.getItem();
            switch (item.getText()) {
                case "Clínico":
                    item = "6";
                case "Detail":
                    //Show the item details
                    var gridItem = grid.getItem(g_itemIndex);
                    alert(
                        "Details about this grid item:\r\n" +
                        "Posted At: " + gridItem.getCell(1).getValue() + "\r\n" +
                        "Posted By: " + gridItem.getCell(2).getValue() + "\r\n" +
                        "Topic: " + gridItem.getCell(3).getValue());
                    break;
                case "Delete":
                    //Stop editing
                    grid.editCell(-1);
                    //Delete the item
                    grid.deleteItem(g_itemIndex);
                    break;
                case "Add New":
                    //This Grid's AllowNewItem is set to true. In this case
                    //the Grid displays a temporary new item as the last item
                    //The following code does not actually add a new item,
                    //but rather put the temporary new item into edit mode
                    var itemIndex = grid.getItemCount();
                    //Put the item into edit mode
                    grid.editCell(itemIndex, 1);
                    //Scroll the item into view
                    grid.getItem(itemIndex).ensureVisible();
                    break;
                case "Save":
                //Save menu item's RaisesServerEvent is set to true,
                //so the event is handled on the server side
            }
            grid.raiseItemCommandEvent(g_itemIndex, item.getText());
        }
        function OnCellSelected(grid) {
            var cell = grid.getSelectedCell();
            grid.raiseItemCommandEvent(cell.getItemIndex(), "Seleção");
        }
        function OnItemCommand(grid, itemIndex, colIndex, commandName) {
            //grid.raiseItemCommandEvent(itemIndex, commandName);
            grid.raiseItemCommandEvent(itemIndex, colIndex.toString());
        }

    </script>

        <div class="col-11 subtituloBG" style="padding-top: 10px">
            <asp:Label ID="Label6" runat="server" class="subtitulo">Mecanismo de Busca para os Procedimentos da Empresa</asp:Label>
        </div>

			<TABLE class="defaultFont ms-2" id="Table1" cellSpacing="0" cellPadding="0">
				<TR>
					<TD>
						<TABLE class="defaultFont" id="Table2" cellSpacing="0" cellPadding="0" width="1200" border="0">
							<TR>
								<TD align="left">
                                    <br />
                                    <asp:panel id="Panel1" runat="server">
						                <div class="col-12 py-2">
                                            <div class="row">

                                                <TABLE id="Table3" cellSpacing="0" cellPadding="0" width="1050" border="0">
                                                    <TR>
                                                        <TD align="left" width="0">
                                                            <asp:Image id="Image2" runat="server" ImageUrl="img/5pixel.gif" Visible="false"></asp:Image></TD>
                                                        <TD align="left" width="0">
                                                            <asp:Image id="Image3" runat="server" ImageUrl="img/5pixel.gif" Visible="false"></asp:Image></TD>
                                                        <TD align="center" width="0">
                                                            <asp:Image id="Image4" runat="server" ImageUrl="img/5pixel.gif" Visible="false"></asp:Image></TD>
                                                        <TD align="center" width="0">
                                                            <asp:Image id="Image5" runat="server" ImageUrl="img/5pixel.gif" Visible="false"></asp:Image></TD>
                                                    </TR>

                                                    <TR class="col-12 gx-3 gy-2">
                                                        <div class="col-md-4 gx-3 gy-2">
						                                    <fieldset>
                                                                <asp:Label ID="Label13" runat="server" Text="Equipamento" class="tituloLabel form-label col-form-label col-form-label-sm" Style="text-align: left;"></asp:Label>
                                                                <asp:Literal runat="server"><br /></asp:Literal>
                                                                <asp:DropDownList ID="ddlEquipamento" runat="server" class="texto form-select form-select-sm" Style="text-align: left;">
                                                                    <asp:ListItem Selected="True"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </fieldset>
                                                          </div>                     

                                                         <div class="col-md-4 gx-3 gy-2">
						                                    <fieldset>
                                                                <asp:Label ID="Label1" runat="server" Text="Tipo" class="tituloLabel form-label col-form-label col-form-label-sm" Style="text-align: left;"></asp:Label>
                                                                <asp:Literal runat="server"><br /></asp:Literal>
                                                                <asp:DropDownList ID="ddlTipoProcedimento" runat="server" class="texto form-select form-select-sm" Style="text-align: left;">
                                                                    <asp:ListItem Selected="True"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </fieldset>
                                                          </div>                     
                   
                                                         <div class="col-md-4 gx-3 gy-2">
						                                    <fieldset>
                                                                <asp:Label ID="Label4" runat="server" Text="Setor" class="tituloLabel form-label col-form-label col-form-label-sm" Style="text-align: left;"></asp:Label>
                                                                <asp:Literal runat="server"><br /></asp:Literal>
                                                                <asp:DropDownList ID="ddlSetor" runat="server" class="texto form-select form-select-sm" Style="text-align: left;">
                                                                    <asp:ListItem Selected="True"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </fieldset>
                                                            </div>  
                       
                                                           <div class="col-md-4 gx-3 gy-2">
						                                    <fieldset>
                                                                <asp:Label ID="Label8" runat="server" Text="Produto" class="tituloLabel form-label col-form-label col-form-label-sm" Style="text-align: left;"></asp:Label>
                                                                <asp:Literal runat="server"><br /></asp:Literal>
                                                                <asp:DropDownList ID="ddlProduto" runat="server" class="texto form-select form-select-sm" Style="text-align: left;">
                                                                    <asp:ListItem Selected="True"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </fieldset>
                                                            </div>                     

                                                         <div class="col-md-4 gx-3 gy-2">
						                                    <fieldset>
                                                                <asp:Label ID="Label11" runat="server" Text="Ferramentas" class="tituloLabel form-label col-form-label col-form-label-sm" Style="text-align: left;"></asp:Label>
                                                                <asp:Literal runat="server"><br /></asp:Literal>
                                                                <asp:DropDownList ID="ddlFerramenta" runat="server" class="texto form-select form-select-sm" Style="text-align: left;">
                                                                    <asp:ListItem Selected="True"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </fieldset>
                                                            </div> 
                                                        <div class="col-md-4 gx-3 gy-2">
                                                            <fieldset>
                                                                <asp:label id="Label12" runat="server" class="tituloLabel form-label col-form-label col-form-label-sm">Célula</asp:label>
                                                                <asp:Literal runat="server"><br /></asp:Literal>
													            <asp:dropdownlist id="ddlCelula" runat="server" class="texto form-select form-select-sm"></asp:dropdownlist>
                                                            </fieldset>
                                                        </div>
                                                        </TD>
                                                        </TR>
                                                     
                                                                <TR>
												                    <TD align="left" width="0">
													                    <asp:Image id="Image7" runat="server" ImageUrl="img/5pixel.gif" Visible="false"></asp:Image></TD>
												                    <TD align="left" width="0">
													                    <asp:Image id="Image8" runat="server" ImageUrl="img/5pixel.gif" Visible="false"></asp:Image></TD>
												                    <TD align="center" width="0">
													                    <asp:Image id="Image9" runat="server" ImageUrl="img/5pixel.gif" Visible="false"></asp:Image></TD>
												                    <TD align="center" width="0">
													                    <asp:Image id="Image10" runat="server" ImageUrl="img/5pixel.gif" Visible="false"></asp:Image></TD>
											                    </TR>
                                                            </TABLE>
										                <TABLE id="Table4" cellSpacing="0" cellPadding="0">
                                                            <div class="col-md-4 gx-3 gy-2">
						                                        <fieldset>
                                                                    <div class="row">
                                                                        <div class="col-12">
                                                                            <asp:Label ID="Label10" runat="server" Text="Palavra-Chave" class="tituloLabel form-label col-form-label col-form-label-sm" Style="text-align: left;"></asp:Label>
                                                                        </div>
                                                                        <div class="col-11">
                                                                            <asp:textbox ID="txtProcedimento" runat="server" class="texto form-control form-control-sm" Style="text-align: left;"></asp:textbox>
                                                                        </div>
                                                                        <div class="col-1 gx-2">
                                                                            <asp:imagebutton id="imgBusca" OnClick="imgBusca_Click"  tabIndex="2" runat="server" ImageUrl="Images/search.svg" ToolTip="Procurar" CssClass="btnMenor" Style="padding: .4rem!important;"></asp:imagebutton>
                                                                        </div>
                                                                    </div>
                                                                </fieldset>
                                                            </div>
										                </TABLE>
                                                           
                                                          </div>

						                </div>
									</asp:panel>
								</TD>
							</TR>
						</TABLE>

                        <br />

    <eo:Grid ID="gridEmpregados" runat="server" 
           ColumnHeaderDescImage="00050404" ColumnHeaderDividerOffset="6" ColumnHeaderHeight="30" 
		   ColumnHeaderAscImage="00050403"
		   FixedColumnCount="1" Cssclas="grid"
           GridLines="Both" Height="1000px" Width="880px"  ItemHeight="30" KeyField="IdProcedimento" PageSize="10"
                ClientSideOnContextMenu="ShowContextMenu" FullRowMode="False"    
                OnItemCommand="gridEmpregados_ItemCommand"  ClientSideOnCellSelected="OnCellSelected"
                ClientSideOnItemCommand="OnItemCommand"    >

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

                <eo:StaticColumn HeaderText="Número POPs" AllowSort="True" 
                    DataField="Numero" Name="Numero" ReadOnly="True" 
                    SortOrder="Ascending" Text="" Width="110" >
                   <CellStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500; color: #7D7B7B !important; text-align: left; height: 35px !important; padding: 0.3rem 0.3rem 0.3rem 0.5rem; height: 35px !important;" />
                </eo:StaticColumn>
               
                <eo:StaticColumn HeaderText="Nome" AllowSort="True" 
                    DataField="Nome" Name="Nome" ReadOnly="True" 
                    SortOrder="Ascending" Text="" Width="650" >
                    <CellStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500; color: #7D7B7B !important; text-align: left; height: 35px !important; padding: 0.3rem 0.3rem 0.3rem 0.5rem; height: 35px !important;" />
                </eo:StaticColumn>
                
            </Columns>
            <ColumnTemplates>
                <eo:TextBoxColumn>
                    <TextBoxStyle CssText="BORDER-RIGHT: #7f9db9 1px solid; PADDING-RIGHT: 2px; BORDER-TOP: #7f9db9 1px solid; PADDING-LEFT: 2px; FONT-SIZE: 8.75pt; PADDING-BOTTOM: 1px; MARGIN: 0px; BORDER-LEFT: #7f9db9 1px solid; PADDING-TOP: 2px; BORDER-BOTTOM: #7f9db9 1px solid; FONT-FAMILY: Tahoma" />
                </eo:TextBoxColumn>
                <eo:DateTimeColumn>
                    <DatePicker ControlSkinID="None" DayCellHeight="16" DayCellWidth="19" 
                        DayHeaderFormat="FirstLetter" DisabledDates="" OtherMonthDayVisible="True" 
                        SelectedDates="" TitleLeftArrowImageUrl="DefaultSubMenuIconRTL" 
                        TitleRightArrowImageUrl="DefaultSubMenuIcon">
                        <PickerStyle CssText="border-bottom-color:#7f9db9;border-bottom-style:solid;border-bottom-width:1px;border-left-color:#7f9db9;border-left-style:solid;border-left-width:1px;border-right-color:#7f9db9;border-right-style:solid;border-right-width:1px;border-top-color:#7f9db9;border-top-style:solid;border-top-width:1px;font-family:Courier New;font-size:8pt;margin-bottom:0px;margin-left:0px;margin-right:0px;margin-top:0px;padding-bottom:1px;padding-left:2px;padding-right:2px;padding-top:2px;" />
                        <CalendarStyle CssText="background-color: white; border-right: #7f9db9 1px solid; padding-right: 4px; border-top: #7f9db9 1px solid; padding-left: 4px; font-size: 9px; padding-bottom: 4px; border-left: #7f9db9 1px solid; padding-top: 4px; border-bottom: #7f9db9 1px solid; font-family: tahoma" />
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
                    </DatePicker>
                </eo:DateTimeColumn>
                <eo:MaskedEditColumn>
                    <MaskedEdit ControlSkinID="None" 
                        TextBoxStyle-CssText="BORDER-RIGHT: #7f9db9 1px solid; PADDING-RIGHT: 2px; BORDER-TOP: #7f9db9 1px solid; PADDING-LEFT: 2px; PADDING-BOTTOM: 1px; MARGIN: 0px; BORDER-LEFT: #7f9db9 1px solid; PADDING-TOP: 2px; BORDER-BOTTOM: #7f9db9 1px solid; font-family:Courier New;font-size:8pt;">
                    </MaskedEdit>
                </eo:MaskedEditColumn>
            </ColumnTemplates>
            <FooterStyle CssText="padding-bottom:4px;padding-left:4px;padding-right:4px;padding-top:4px;" />
        </eo:Grid>


                        <%--<ig:WebDataGrid ID="gridEmpregados" runat="server" AutoGenerateColumns="False" 
                            DataKeyFields="IdProcedimento, Nome" Height="423px" 
                            OnCellSelectionChanged="gridEmpregados_CellSelectionChanged" 
                            OnRowSelectionChanged="gridEmpregados_RowSelectionChanged" Width="780px">
                            <Columns>
                                <ig:BoundDataField DataFieldName="Numero" 
                                    Header-Text="Número POPs" Key="Numero" Width="110px" >
                                    <Header Text="Número POPs" />
                                </ig:BoundDataField>
                                <ig:BoundDataField DataFieldName="Nome" Header-Text="Procedimento" 
                                    Key="Nome" Width="650px">
                                    <Header Text="Procedimento" />
                                </ig:BoundDataField>
                            </Columns>
                            <editorproviders>
                                <ig:TextBoxProvider ID="TextBoxProvider">
                                </ig:TextBoxProvider>
                            </editorproviders>
                            <behaviors>
                                <ig:Paging Enabled="true" PagerAppearance="Both" 
                                    PagerMode="NextPreviousFirstLast" PageSize="50">
                                </ig:Paging>
                                <ig:Selection CellSelectType="Single" Enabled="true">
                                    <SelectionClientEvents CellSelectionChanged="gridEmpregados_CellSelectionChanged" />
                                </ig:Selection>
                                <ig:RowSelectors Enabled="true">
                                </ig:RowSelectors>
                            </behaviors>
                        </ig:WebDataGrid>--%>
<%--						<igtbl:ultrawebgrid id="UltraWebGridProcedimentos" runat="server" Height="344px" Width="602px" ImageDirectory="/ig_common/Images/" onpageindexchanged="UltraWebGridProcedimentos_PageIndexChanged"><Bands>
<igtbl:UltraGridBand GroupByRowDescriptionMask="[caption] : [value] ([count])   --   Relat&#243;rio: &lt;a href=&quot;javascript:RelatorioN('[value]')&quot;&gt;Por Nome&lt;/a&gt; ou &lt;a href=&quot;javascript:RelatorioD('[value]')&quot;&gt;Por Data&lt;/a&gt;"><Columns>
<igtbl:UltraGridColumn Key="IdProcedimento" AllowGroupBy="No" BaseColumnName="IdProcedimento" Hidden="True" Width="0px">
<Header Caption="IdProcedimento"></Header>
<Footer Key="Id"></Footer>
</igtbl:UltraGridColumn>
<igtbl:UltraGridColumn Key="Numero" AllowGroupBy="No" BaseColumnName="Numero" FooterText="" Format="" Width="100px" EditorControlID="">
<CellButtonStyle HorizontalAlign="Center" Cursor="Hand" BackColor="#EFFFF6" BorderWidth="0px" BorderStyle="None" Font-Bold="True" Font-Names="Verdana" Font-Size="XX-Small" Font-Underline="True" ForeColor="#156047"></CellButtonStyle>
<HeaderStyle>
<BorderDetails StyleLeft="None" StyleTop="None"></BorderDetails>
</HeaderStyle>
<CellStyle HorizontalAlign="Center" Font-Bold="True">
<BorderDetails StyleLeft="None"></BorderDetails>
</CellStyle>
<SelectedCellStyle BorderStyle="None"></SelectedCellStyle>
<Header Caption="N&#250;mero POPs">
<RowLayoutColumnInfo OriginX="1"></RowLayoutColumnInfo>
</Header>
<Footer Caption="" Key="NomeAbreviado">
<RowLayoutColumnInfo OriginX="1"></RowLayoutColumnInfo>
</Footer>
</igtbl:UltraGridColumn>
<igtbl:UltraGridColumn Key="Nome" AllowGroupBy="No" BaseColumnName="Nome" FooterText="" Format="" Width="500px" EditorControlID="">
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
<Header Caption="Nome do Procedimento">
<RowLayoutColumnInfo OriginX="2"></RowLayoutColumnInfo>
</Header>
<Footer Caption="" Key="NomeCompleto">
<RowLayoutColumnInfo OriginX="2"></RowLayoutColumnInfo>
</Footer>
</igtbl:UltraGridColumn>
</Columns>
<RowTemplateStyle BackColor="White" BorderColor="White" BorderStyle="Ridge">
<BorderDetails WidthLeft="3px" WidthTop="3px" WidthRight="3px" WidthBottom="3px"></BorderDetails>
</RowTemplateStyle>
<AddNewRow Visible="NotSet" View="NotSet"></AddNewRow>
</igtbl:UltraGridBand>
</Bands>
<DisplayLayout Version="3.00" Name="UltraWebGridProcedimentos" RowHeightDefault="18px" TableLayout="Fixed" ViewType="OutlookGroupBy" RowSelectorsDefault="No" AutoGenerateColumns="False">
<FrameStyle BackColor="WhiteSmoke" BorderColor="#7CC5A1" BorderWidth="1px" BorderStyle="Solid" Height="344px" Width="602px"></FrameStyle>
<Images  ImageDirectory="/ig_common/Images/"></Images>
<ClientSideEvents CellClickHandler="Procedimentos_CellClickHandler" MouseOverHandler="ProcedimentosMouseOver" MouseOutHandler="ProcedimentosMouseOut"></ClientSideEvents>
<Pager PageSize="50" AllowPaging="True" StyleMode="QuickPages" Alignment="Center" PrevText="Anterior" NextText="Pr&#243;ximo" QuickPages="10" Pattern="P&#225;gina &lt;b&gt;[currentpageindex]&lt;/b&gt; de &lt;b&gt;[pagecount]&lt;/b&gt;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&lt;b&gt;[default]&lt;/b&gt;">
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
</DisplayLayout>
</igtbl:ultrawebgrid>
--%>						<TABLE class="defaultFont" id="Table5" cellSpacing="0" cellPadding="0" width="600" align="center"
							border="0">
							<TR class="col-12">
								<TD vAlign="top" align="center"><asp:hyperlink id="lisTodas" runat="server" SkinID="BoldLink">Listar Todas</asp:hyperlink></TD>
								<TD vAlign="top" align="center"><asp:linkbutton id="lkbListagemPOPs" runat="server" >
                                        Listagem por nPOPS</asp:linkbutton></TD>
								<TD vAlign="top" align="center"><asp:linkbutton id="lkbListagemNome" runat="server">
                                        </> Listagem por Nome</asp:linkbutton></TD>
								<TD vAlign="top" align="center"><asp:label id="lblTotalRegistros" style="TEXT-ALIGN: right" runat="server"></asp:label></TD>
						<TD> <asp:hyperlink id="hlk_Imp" runat="server" SkinID="BoldLink" 
                                >Importar Procedimentos Genéricos</asp:hyperlink> </TD>
						</TABLE>
						<BR>

                        <asp:button id="Button1" runat="server" Text="Importar Procedimentos Genéricos" Visible="false" ></asp:button>
						

                            <input id="txtIdEmpresa" type="text"  style="visibility:hidden"  />
                            <input id="txtIdUsuario" type="text"  style="visibility:hidden"  />

                            <asp:Label ID="lbl_Id_Empresa" runat="server" Text="IdEmpresa" Visible="False"></asp:Label>
                            <asp:Label ID="lbl_Id_Usuario" runat="server" Text="IdUsuario" Visible="False"></asp:Label>
                    </TD>
				</TR>
                <tr>
                   <td align="center">
                            <asp:button id="btn_Ajustar_Numeracao" onclick="btn_Ajustar_Numeracao_Click" runat="server" CssClass="btn"
								Width ="290px" Text="Ajustar Numeração dos Procedimentos" ></asp:button>

                           <asp:button id="btnIncluirNovo" onclick="btnIncluirNovo_Click" runat="server" CssClass="btn"
						    Width="210px" Text="Incluir Novo Procedimento" ></asp:button>
<asp:button id="btnImportar" runat="server" Width="210px" Text="Importar Procedimentos Genéricos" onclick="btnImportar_Click" Visible="false"  ></asp:button>

                    </TD>
				</TR>
			</TABLE>


         <eo:MsgBox ID="MsgBox1" runat="server" BackColor="#47729F" ControlSkinID="None" 
        HeaderHtml="Dialog Title" Height="100px" Width="168px">
        <HeaderStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #ffbf00 3px solid; padding-left: 4px; font-weight: bold; font-size: 11px; padding-bottom: 2px; color: white; padding-top: 2px; border-bottom: #22456a 1px solid; font-family: verdana" />
        <ContentStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #7d97b6 1px solid; padding-left: 4px; border-left-width: 1px; font-size: 11px; border-left-color: #728eb8; padding-bottom: 4px; color: white; padding-top: 4px; border-bottom: #22456a 1px solid; font-family: verdana" />
        <FooterStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #7d97b6 1px solid; padding-left: 4px; border-left-width: 1px; font-size: 11px; border-left-color: #728eb8; padding-bottom: 4px; color: white; padding-top: 4px; border-bottom: #22456a 1px solid; font-family: verdana" />
        </eo:MsgBox>

</div>
</div>
</asp:Content>