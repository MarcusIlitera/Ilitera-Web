<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="True"
    Title="Ilitera.Net"  EnableEventValidation="false" Inherits="Ilitera.Net.Treinamentos.ListaTreinamentos"    ValidateRequest="false"  %>

<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>


<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">

    <link href="css/forms.css" rel="stylesheet" type="text/css" />
    <link href="css/tabelas.css" rel="stylesheet" type="text/css" />

    <style type="text/css">
    .defaultFont
    {
        width: 627px;
    }
        #Table4
        {
            width: 594px;
        }
        #Table15
        {
            width: 214px;
        }
        #Table20
        {
            width: 184px;
        }
        #Table24
        {
            width: 333px;
        }
        #Table27
        {
            width: 583px;
        }
    </style>
</asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent" >
    <div class="container-fluid d-flex ms-5 ps-4">
    <div class="row gx-3 gy-2">

		<script language="javascript" src="../scripts/validador.js"></script>
		<script language="javascript">
		    function ListaEmpregadoCurso(IdTreinamento, IdEmpresa, IdUsuario) {
		        void (addItem(centerWin("ListaEmpregadoCurso.aspx?IdTreinamento=" + IdTreinamento + "&IdUsuario=" + IdUsuario + "&IdEmpresa=" + IdEmpresa, 400, 315, "ListaTreinamentoParticipantes"), "Todos"));
		    }

		    function ListaTreinamentosMouseOver(gridName, id, objectType) {
		        DataGridMouseOverHandler(gridName, id, objectType, 2);
		    }

		    function ListaTreinamentosMouseOut(gridName, id, objectType) {
		        DataGridMouseOutHandler(gridName, id, objectType, 2);
		    }

		    function ListaTreinamentos_CellClickHandler(gridName, cellId, button) {
		        if (button == 0) {
		            var cell = igtbl_getCellById(cellId);
		            var IdTreinamentoCell = cell.getPrevCell(true).getPrevCell(true);

		            if (cell.Index == 2 && cell.getElement().innerText.Trim() != "")
		                ListaEmpregadoCurso(IdTreinamentoCell.getValue(), top.window.document.frames.conteudo.window.document.getElementById('txtIdEmpresa').value,
	                    top.window.document.frames.conteudo.window.document.getElementById('txtIdUsuario').value);
		        }
		    }


		    var g_itemIndex = -1;
		    var g_cellIndex = -1;

		   
		    function OnContextMenuItemClicked(e, eventInfo) {
		        var grid = eo_GetObject("<%=UltraWebGridListaTreinamentos.ClientID%>");

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

		        grid.raiseItemCommandEvent(cell.getItemIndex(),  cell.getColIndex());
		    }


		    function OnItemCommand(grid, itemIndex, colIndex, commandName) {

		        //grid.raiseItemCommandEvent(itemIndex, commandName);
		        grid.raiseItemCommandEvent(itemIndex, colIndex.toString());
		    }

		</script>

   


<%--	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0">
		<form id="ListaEmpresa" method="post" runat="server">
			<TABLE class="defaultFont" cellSpacing="0" cellPadding="0" width="620" align="center" border="0">
--%>				
        <div class="col-11 subtituloBG mb-3" style="padding-top: 10px;">
            <asp:label id="Label1" runat="server" SkinID="TitleFont" CssClass="subtitulo">Listagem de treinamentos realizados</asp:label>
        </div>

        
                                   


         <eo:Grid ID="UltraWebGridListaTreinamentos" runat="server"
                ColumnHeaderDescImage="00050404" ColumnHeaderDividerOffset="6" ColumnHeaderHeight="30" 
                ColumnHeaderAscImage="00050403"
                FixedColumnCount="1" Cssclas="grid" FullRowMode="False"
                GridLines="Both" Height="218px" Width="1050px" ItemHeight="30" KeyField="Id" PageSize="10"
                OnItemCommand="UltraWebGridListaTreinamentos_ItemCommand" ClientSideOnCellSelected="OnCellSelected"
        ClientSideOnItemCommand="OnItemCommand" ClientSideOnContextMenu="ShowContextMenu"  >
           <ItemStyles>
                    <eo:GridItemStyleSet>
                    <ItemStyle CssText="background-color: #FAFAFA" />
                    <ItemHoverStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500;  text-align: left; height: 30px !important; background: rgba(28, 148, 137, 0.32); color: #FFFFFF; padding: 0.3rem 0.3rem 0.3rem 0.5rem; " />
                    <AlternatingItemStyle CssText="background-color: #F2F2F2;" />
                    <AlternatingItemHoverStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500;  text-align: left; height: 30px !important; background: rgba(28, 148, 137, 0.32); color: #FFFFFF; padding: 0.3rem 0.3rem 0.3rem 0.5rem;" />
                    <SelectedStyle CssText="border-bottom-style:solid;border-left-style:solid;border-right-style:solid;border-top-style:solid;" />
                    <CellStyle CssText="padding: 0.3rem 0.3rem 0.3rem 0.5rem; height: 30px !important;" />
                    </eo:GridItemStyleSet>
                    </ItemStyles>
             <ColumnHeaderTextStyle CssText="" />
             <ColumnHeaderStyle CssClass="tabelaC colunas"/>
             <Columns>
               
                <eo:StaticColumn HeaderText="Período" AllowSort="True" 
                    DataField="Periodo" Name="Periodo" ReadOnly="True" 
                    SortOrder="Ascending" Text="" Width="230" >
                    <CellStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500; color: #7D7B7B !important; text-align: left; height: 35px !important; padding: 0.3rem 0.3rem 0.3rem 0.5rem; height: 35px !important;" />
                </eo:StaticColumn>
                <eo:StaticColumn HeaderText="Treinamento" AllowSort="True" 
                    DataField="NomeTreinamento" Name="NomeTreinamento" ReadOnly="True" 
                    Width="380">
                    <CellStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500; color: #7D7B7B !important; text-align: left; height: 35px !important; padding: 0.3rem 0.3rem 0.3rem 0.5rem; height: 35px !important;" />
                </eo:StaticColumn>                
                <eo:ButtonColumn>
                    <ButtonStyle CssText="background-image:url('Images/editar.svg');" />
                    <CellStyle CssText="background-image:url('Images/editar.svg');" />
                </eo:ButtonColumn>
                <eo:ButtonColumn>
                    <ButtonStyle CssText="background-image:url('Images/impressora.svg');background-size:15px !important;" />
                    <CellStyle CssText="background-image:url('Images/impressora.svg');background-size:15px !important;" />
                </eo:ButtonColumn>
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
									
<%--                                    <igtbl:ultrawebgrid id="UltraWebGridListaTreinamentos" runat="server" Width="630px" 
                                            Height="218px" ImageDirectory="" 
                                            OnPageIndexChanged="UltraWebGridListaTreinamentos_PageIndexChanged" 
                                            oninitializelayout="UltraWebGridListaTreinamentos_InitializeLayout">
										<DisplayLayout AutoGenerateColumns="False" RowHeightDefault="18px"
											Version="3.00" ViewType="OutlookGroupBy" RowSelectorsDefault="No"
											Name="UltraWebGridListaTreinamentos" TableLayout="Fixed">
											<AddNewBox>
                                                <BoxStyle BackColor="LightGray" BorderStyle="Solid" BorderWidth="1px">
                                                </BoxStyle>
											</AddNewBox>
											<Pager PrevText="Anterior" NextText="Pr&#243;ximo" QuickPages="10" PageSize="10" Pattern="P&#225;gina &lt;b&gt;[currentpageindex]&lt;/b&gt; de &lt;b&gt;[pagecount]&lt;/b&gt;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&lt;b&gt;[default]&lt;/b&gt;" StyleMode="QuickPages"
												Alignment="Center" AllowPaging="True">
                                                <PagerStyle BackColor="#DEEFE4" BorderStyle="None" BorderWidth="0px"
                                                    Font-Names="Verdana" Font-Size="XX-Small" ForeColor="#44926D" Height="18px" HorizontalAlign="Center" />
											</Pager>
											<HeaderStyleDefault VerticalAlign="Middle" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True"
												HorizontalAlign="Center" ForeColor="#44926D" BackColor="#DEEFE4" Height="18px"></HeaderStyleDefault>
											<GroupByRowStyleDefault Height="18px"></GroupByRowStyleDefault>
											<FrameStyle Width="630px" BorderWidth="1px" BorderColor="#7CC5A1" 
                                                BorderStyle="Solid" Height="218px" BackColor="WhiteSmoke"></FrameStyle>
											<FooterStyleDefault BorderWidth="1px" BorderStyle="Solid">
												<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
											</FooterStyleDefault>
											<ActivationObject AllowActivation="False" BorderColor="124, 197, 161" BorderWidth=""></ActivationObject>
											<GroupByBox Hidden="True"></GroupByBox>
											<EditCellStyleDefault BorderWidth="0px" BorderStyle="None"></EditCellStyleDefault>
											<RowStyleDefault VerticalAlign="Middle" BorderWidth="1px" Font-Size="XX-Small" Font-Names="Verdana"
												BorderColor="#7CC5A1" BorderStyle="Solid" HorizontalAlign="Center" ForeColor="#156047" BackColor="White"></RowStyleDefault>
                                            <Images ImageDirectory="">
                                            </Images>
                                            <ClientSideEvents CellClickHandler="ListaTreinamentos_CellClickHandler" MouseOutHandler="ListaTreinamentosMouseOut"
                                                MouseOverHandler="ListaTreinamentosMouseOver" />
										</DisplayLayout>
										<Bands>
											<igtbl:UltraGridBand GroupByRowDescriptionMask="[caption] : [value] ([count])   --   Relat&#243;rio: &lt;a href=&quot;javascript:RelatorioN('[value]')&quot;&gt;Por Nome&lt;/a&gt; ou &lt;a href=&quot;javascript:RelatorioD('[value]')&quot;&gt;Por Data&lt;/a&gt;">
												<Columns>
													<igtbl:UltraGridColumn Key="Id" Width="0px" Hidden="True" AllowGroupBy="No" BaseColumnName="Id">
														<Header Caption="Id"></Header>
													</igtbl:UltraGridColumn>
													<igtbl:UltraGridColumn Key="Periodo" EditorControlID="" Width="150px"
														AllowGroupBy="No" Format="" BaseColumnName="Periodo" FooterText="">
														<SelectedCellStyle BorderStyle="None"></SelectedCellStyle>
														<CellButtonStyle Cursor="Hand" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True" HorizontalAlign="Left"
															ForeColor="#156047" BackColor="#EFFFF6">
															<Padding Left="3px"></Padding>
															<BorderDetails StyleBottom="None" StyleTop="None" StyleRight="None" StyleLeft="None"></BorderDetails>
														</CellButtonStyle>
														<CellStyle HorizontalAlign="Center">
															<BorderDetails StyleLeft="None" WidthLeft="0px"></BorderDetails>
														</CellStyle>
														<Footer Key="NomeAbreviado" Caption="">
                                                            <RowLayoutColumnInfo OriginX="1" />
                                                        </Footer>
														<HeaderStyle>
															<BorderDetails StyleTop="None" StyleLeft="None"></BorderDetails>
														</HeaderStyle>
														<Header Caption="Data de In&#237;cio">
                                                            <RowLayoutColumnInfo OriginX="1" />
														</Header>
													</igtbl:UltraGridColumn>
													<igtbl:UltraGridColumn Key="NomeTreinamento" EditorControlID="" Width="425px"
														AllowGroupBy="No" Format="" BaseColumnName="NomeTreinamento" FooterText="">
														<CellButtonStyle Cursor="Hand" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True" HorizontalAlign="Left"
															ForeColor="#156047" BackColor="#EFFFF6" Font-Underline="True">
															<Padding Left="3px"></Padding>
															<BorderDetails StyleBottom="None" StyleTop="None" StyleRight="None" StyleLeft="None"></BorderDetails>
														</CellButtonStyle>
														<CellStyle Font-Bold="True" HorizontalAlign="Left">
															<Padding Left="3px"></Padding>
														</CellStyle>
														<Footer Key="NomeCompleto" Caption="">
                                                            <RowLayoutColumnInfo OriginX="2" />
                                                        </Footer>
														<HeaderStyle>
															<BorderDetails StyleTop="None" StyleRight="None"></BorderDetails>
														</HeaderStyle>
														<Header Caption="Treinamento">
                                                            <RowLayoutColumnInfo OriginX="2" />
														</Header>
													</igtbl:UltraGridColumn>
													<igtbl:UltraGridColumn Key="EditTreinamento" Width="25px" AllowGroupBy="No" BaseColumnName="EditTreinamento">
														<Footer>
                                                            <RowLayoutColumnInfo OriginX="3" />
                                                        </Footer>
														<HeaderStyle>
															<BorderDetails StyleTop="None" StyleRight="None"></BorderDetails>
														</HeaderStyle>
														<Header>
                                                            <RowLayoutColumnInfo OriginX="3" />
														</Header>
                                                        <CellStyle>
                                                            <BorderDetails StyleRight="None" />
                                                        </CellStyle>
													</igtbl:UltraGridColumn>
													<igtbl:UltraGridColumn Key="Certificado" Width="25px" AllowGroupBy="No" BaseColumnName="Certificado">
														<Footer>
                                                            <RowLayoutColumnInfo OriginX="4" />
                                                        </Footer>
														<HeaderStyle>
															<BorderDetails StyleTop="None" StyleRight="None"></BorderDetails>
														</HeaderStyle>
														<Header>
                                                            <RowLayoutColumnInfo OriginX="4" />
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
									</igtbl:ultrawebgrid>
--%>									

                                   <asp:label id="lblTotRegistros" runat="server"></asp:label>
										
									<asp:button id="btnNovoTreinamento" runat="server" Width="130px" 
                                            Text="Novo Treinamento" 
                                            Visible="False" onclick="btnNovoTreinamento_Click"></asp:button>

        <div class="col-11">
            <asp:linkbutton id="lkbListagemCurso" runat="server" SkinID="BoldLinkButton" CssClass="btn" onclick="lkbListagemCurso_Click"><img src="Images/printer.svg" style="padding: .25rem;"  border="0"></img> Treinamentos Realizados</asp:linkbutton>
        </div>
						<asp:label id="lblNomeEmpresa" runat="server" ForeColor="Red" SkinID="BoldFont"></asp:label></td>
			<INPUT id="txtAuxiliar" type="hidden" name="txtAuxiliar" runat="server">
<%--		</form>
	</body>
</HTML>
--%>
</div>
</div>
</asp:Content>