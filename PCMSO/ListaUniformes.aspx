<%@ Page Language="C#"   AutoEventWireup="True"  MasterPageFile="~/Site.Master" CodeBehind="ListaUniformes.aspx.cs"  Inherits="Ilitera.Net.PCMSO.ListaUniformes" Title="Ilitera.Net" %>

<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <link href="css/forms.css" rel="stylesheet" type="text/css" />
    <link href="css/tabelas.css" rel="stylesheet" type="text/css" />
       <style type="text/css">
        .linha
   {
	font: 8px Verdana, Arial, Helvetica, sans-serif, Tahoma;
    }
           .btnLogarClass
           {}
    </style>

</asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent" >
    <div class="container-fluid d-flex ms-5 ps-4">
        <div class="w-100 row gx-3 gy-2">

		<br />


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
		        var grid = eo_GetObject("<%=UltraWebGridListaUniformes.ClientID%>");

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

		        grid.raiseItemCommandEvent(cell.getItemIndex(), cell.getColIndex());
		    }


		    function OnItemCommand(grid, itemIndex, colIndex, commandName) {

		        //grid.raiseItemCommandEvent(itemIndex, commandName);
		        grid.raiseItemCommandEvent(itemIndex, colIndex.toString());
		    }

		</script>

            <div class="col-11 subtituloBG mb-1" style="padding-top: 10px;">
                <asp:label id="Label1" runat="server" SkinID="BoldFont" CssClass="subtitulo">Cadastro de Uniformes</asp:label>
            </div>

            <div class="col-11">
                <div class="row">
                    <div class="col-3 gx-3 gy-2">
                        <asp:Label ID="Label2" runat="server" Text="E-mail primário para alerta" CssClass="tituloLabel"></asp:Label>
                        <asp:TextBox ID="txt_Email1" runat="server" MaxLength="100" CssClass="texto form-control form-control-sm"></asp:TextBox>
                    </div>

                    <div class="col-3 gx-3 gy-2">
                        <asp:Label ID="Label3" runat="server" Text="E-mail secundário para alerta" CssClass="tituloLabel"></asp:Label>
                        <asp:TextBox ID="txt_Email2" runat="server" MaxLength="100" CssClass="texto form-control form-control-sm"></asp:TextBox>
                    </div>

                    <div class="col-2 gx-3 gy-2">
                        <asp:Label ID="Label4" runat="server" Text="Dias para o alerta" CssClass="tituloLabel"></asp:Label>
                        <div class="row">
                            <div class="col-6">
                                <asp:TextBox ID="txt_Dias" runat="server" MaxLength="3" CssClass="texto form-control form-control-sm"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            
            <div class="col-11 mb-3">
                <div class="row">
                    <div class="col-2 gx-3 gy-2">
                        <asp:Button ID="cmd_Salvar_Dias" runat="server" Text="Salvar Alerta" CssClass="btn" OnClick="cmd_Salvar_Dias_Click" />
                    </div>
                </div>
            </div>

            <%-- TABELA --%>

         <eo:Grid ID="UltraWebGridListaUniformes" runat="server" ColumnHeaderAscImage="00050403" ColumnHeaderDescImage="00050404" GridLines="Both" Height="218px" Width="800px" ColumnHeaderDividerOffset="6" 
                ColumnHeaderHeight="30" ItemHeight="30" KeyField="Id_Uniforme"  ClientSideOnContextMenu="ShowContextMenu" FullRowMode="False"  
             ClientSideOnCellSelected="OnCellSelected" ClientSideOnItemCommand="OnItemCommand" OnItemCommand="UltraWebGridListaUniformes_ItemCommand">
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
               
                <eo:StaticColumn HeaderText="Uniforme" AllowSort="True" 
                    DataField="Uniforme" Name="Uniforme" ReadOnly="True" 
                    SortOrder="Ascending" Text="" Width="230" >
                    <CellStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500; color: #7D7B7B !important; text-align: left; height: 35px !important; padding: 0.3rem 0.3rem 0.3rem 0.5rem; height: 35px !important;" />
                </eo:StaticColumn>
                <eo:StaticColumn HeaderText="Medida" AllowSort="True" 
                    DataField="Medida" Name="Medida" ReadOnly="True" 
                    Width="140">
                    <CellStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500; color: #7D7B7B !important; text-align: left; height: 35px !important; padding: 0.3rem 0.3rem 0.3rem 0.5rem; height: 35px !important;" />
                </eo:StaticColumn>                
                <eo:StaticColumn HeaderText="Obs" AllowSort="True" 
                    DataField="Obs" Name="Obs" ReadOnly="True" 
                    Width="240">
                    <CellStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500; color: #7D7B7B !important; text-align: left; height: 35px !important; padding: 0.3rem 0.3rem 0.3rem 0.5rem; height: 35px !important;" />
                </eo:StaticColumn>                

                <eo:ButtonColumn>
                    <ButtonStyle CssText="background-image:url('Images/editar.svg'); padding: 0.3rem 0.3rem 0.3rem 0.5rem; height: 35px !important;" />
                    <CellStyle CssText="background-image:url('Images/editar.svg'); padding: 0.3rem 0.3rem 0.3rem 0.5rem; height: 35px !important;" />
                </eo:ButtonColumn>
               <%-- <eo:ButtonColumn>
                    <ButtonStyle CssText="background-image:url('img/print.gif');" />
                    <CellStyle CssText="background-image:url('img/print.gif');" />
                </eo:ButtonColumn>--%>
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


                                                        <%--<igtbl:ultrawebgrid ID="UltraWebGridListaExtintores" runat="server" 
                                                            Height="210px" 
        OnPageIndexChanged="UltraWebGridListaEmpresa_PageIndexChanged" OnClickCellButton="UltraWebGridListaExtintores_ClickCellButton" 
                                                            Width="800px" 
        OnInitializeRow="UltraWebGridListaEmpresa_InitializeRow" 
        style="text-align: justify">
                                                            <displaylayout autogeneratecolumns="False" name="UltraWebGridListaExtintores" 
                                                                rowheightdefault="18px" rowselectorsdefault="No" tablelayout="Fixed" 
                                                                version="3.00">
                                                                <addnewbox hidden="False">
                                                                </addnewbox>
                                                                <pager alignment="Center" allowpaging="True" changelinkscolor="True" 
                                                                    nexttext="Próximo" 
                                                                    pattern="Página &lt;b&gt;[currentpageindex]&lt;/b&gt; de &lt;b&gt;[pagecount]&lt;/b&gt;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&lt;b&gt;[default]&lt;/b&gt;" 
                                                                    prevtext="Anterior" quickpages="10" stylemode="QuickPages" PageSize="10">
                                                                    <PagerStyle BackColor="#DEEFE4" BorderStyle="None" BorderWidth="0px" 
                                                                        Font-Names="Verdana" Font-Size="XX-Small" ForeColor="#44926D" Height="18px" />
                                                                </pager>
                                                                <headerstyledefault backcolor="#DEEFE4" font-bold="True" font-names="Verdana" 
                                                                    font-size="XX-Small" forecolor="#44926D" horizontalalign="Center" 
                                                                    verticalalign="Middle">
                                                                    <Margin Bottom="0px" Left="0px" Right="0px" Top="0px" />
                                                                    <Padding Bottom="0px" Left="0px" Right="0px" Top="0px" />
                                                                </headerstyledefault>
                                                                <groupbyrowstyledefault backcolor="Control" bordercolor="Window">
                                                                </groupbyrowstyledefault>
                                                                <framestyle backcolor="WhiteSmoke" bordercolor="#7CC5A1" borderstyle="Solid" 
                                                                    borderwidth="1px" font-names="Verdana" font-size="XX-Small" height="210px" 
                                                                    width="800px">
                                                                </framestyle>
                                                                <footerstyledefault backcolor="LightGray" borderstyle="Solid" borderwidth="1px">
                                                                    <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" 
                                                                    WidthTop="1px" />
                                                                </footerstyledefault>
                                                                <activationobject allowactivation="False" bordercolor="124, 197, 161" 
                                                                    borderstyle="Solid" borderwidth="1px">
                                                                </activationobject>
                                                                <groupbybox hidden="True">
                                                                    <boxstyle backcolor="ActiveBorder" bordercolor="Window">
                                                                    </boxstyle>
                                                                </groupbybox>                                                                
                                                                <editcellstyledefault borderstyle="None" borderwidth="0px">
                                                                </editcellstyledefault>
                                                                <rowstyledefault backcolor="White" bordercolor="#7CC5A1" borderstyle="Solid" 
                                                                    borderwidth="1px" font-names="Verdana" font-size="XX-Small" forecolor="#156047" 
                                                                    horizontalalign="Center">
                                                                </rowstyledefault>
                                                                <filteroptionsdefault>
                                                                    <filteroperanddropdownstyle backcolor="White" bordercolor="Silver" 
                                                                        borderstyle="Solid" borderwidth="1px" customrules="overflow:auto;" 
                                                                        font-names="Verdana,Arial,Helvetica,sans-serif" font-size="11px">
                                                                        <Padding Left="2px" />
                                                                    </filteroperanddropdownstyle>
                                                                    <filterhighlightrowstyle backcolor="#151C55" forecolor="White">
                                                                    </filterhighlightrowstyle>
                                                                    <filterdropdownstyle backcolor="White" bordercolor="Silver" borderstyle="Solid" 
                                                                        borderwidth="1px" customrules="overflow:auto;" 
                                                                        font-names="Verdana,Arial,Helvetica,sans-serif" font-size="11px" height="300px" 
                                                                        width="200px">
                                                                        <Padding Left="2px" />
                                                                    </filterdropdownstyle>
                                                                </filteroptionsdefault>
                                                                <ClientSideEvents CellClickHandler="ListaExtintores_CellClickHandler" 
                                                                MouseOutHandler="ListaExtintoresMouseOut" 
                                                                MouseOverHandler="ListaExtintoresMouseOver" />
                                                            </displaylayout>
                                                            <bands>
                                                                <igtbl:UltraGridBand GroupByRowDescriptionMask="[caption] : [value] ([count])   --   Relatório: &lt;a href=&quot;javascript:RelatorioN('[value]')&quot;&gt;Por Nome&lt;/a&gt; ou &lt;a href=&quot;javascript:RelatorioD('[value]')&quot;&gt;Por Data&lt;/a&gt;">
                                                                    <Columns>
                                                                        <igtbl:UltraGridColumn AllowGroupBy="No" BaseColumnName="Id_Uniforme" Hidden="True" 
                                                                            Key="Id_Uniforme" Width="0px">
                                                                            <header caption="Id">
                                                                            </header>
                                                                        </igtbl:UltraGridColumn>
                                                                        <igtbl:UltraGridColumn AllowGroupBy="No" BaseColumnName="Uniforme" 
                                                                            EditorControlID="" FooterText="" Format="" Key="Uniforme" Width="340px">                                                                     
                                                                          
                                                                            <cellstyle font-bold="True" horizontalalign="Left">
                                                                                <Padding Left="3px" />
                                                                                <BorderDetails StyleLeft="None" />
                                                                            </cellstyle>
                                                                            <footer caption="">
                                                                                <RowLayoutColumnInfo OriginX="1" />
                                                                            </footer>
                                                                            <HeaderStyle>
                                                                            <BorderDetails StyleLeft="None" StyleTop="None" />
                                                                            </HeaderStyle>
                                                                            <header caption="Uniforme">
                                                                                <RowLayoutColumnInfo OriginX="1" />
                                                                            </header>
                                                                        </igtbl:UltraGridColumn>
                                                                        <igtbl:UltraGridColumn AllowGroupBy="No" BaseColumnName="Medida" 
                                                                            EditorControlID="" FooterText="" Format="" Key="Medida" Width="170px">
                                                                            <cellstyle horizontalalign="Center">
                                                                                <Padding Left="3px" />
                                                                                <BorderDetails StyleRight="None" />
                                                                            </cellstyle>
                                                                            <footer caption="">
                                                                                <RowLayoutColumnInfo OriginX="2" />
                                                                            </footer>
                                                                            <HeaderStyle>
                                                                            <BorderDetails StyleRight="None" StyleTop="None" />
                                                                            </HeaderStyle>
                                                                            <header caption="Medida">
                                                                                <RowLayoutColumnInfo OriginX="2" />
                                                                            </header>
                                                                        </igtbl:UltraGridColumn>
                                                                        <igtbl:UltraGridColumn AllowGroupBy="No" BaseColumnName="Obs" 
                                                                            Key="Obs" Width="270px">
                                                                            <cellstyle horizontalalign="Center">
                                                                            </cellstyle>
                                                                            <footer>
                                                                                <RowLayoutColumnInfo OriginX="3" />
                                                                            </footer>
                                                                            <HeaderStyle>
                                                                            <BorderDetails StyleTop="None" />
                                                                            </HeaderStyle>
                                                                            <header caption="Obs">
                                                                                <RowLayoutColumnInfo OriginX="3" />
                                                                            </header>
                                                                        </igtbl:UltraGridColumn>
                                                                    </Columns>
                                                                    <rowtemplatestyle backcolor="White" bordercolor="White" borderstyle="Ridge">
                                                                        <BorderDetails WidthBottom="3px" WidthLeft="3px" WidthRight="3px" 
                                                                        WidthTop="3px" />
                                                                    </rowtemplatestyle>
                                                                    <addnewrow view="NotSet" visible="NotSet">
                                                                    </addnewrow>
                                                                </igtbl:UltraGridBand>
                                                            </bands>
                                                        </igtbl:ultrawebgrid>--%>
                                                        <br/>

            <div class="col-12">
                <asp:hyperlink id="hlkNovo" runat="server" SkinID="BoldLink" CssClass="btn">Novo Uniforme</asp:hyperlink>
            </div>
                       
                                                        <br />
                    <br />
                                                        <asp:LinkButton ID="lkbListagemExtintores" runat="server" 
                        SkinID="BoldLinkButton" Visible="False" onclick="lkbListagemExtintores_Click"><img 
                                                            border="0" src="img/print.gif"></img> Listagem Uniformes</asp:LinkButton>
                                                        <br />
                                                        <BR>
												
                                                
																<asp:label id="lblError" runat="server" SkinID="ErrorFont"></asp:label>
                                                            <input id="txtAuxiliar" runat="server" name="txtAuxiliar" type="hidden">
                                                                <%--	</form>--%><%--</body>--%><%--</asp:Content>--%>
                                                            </input>
        </div>
    </div>

    <eo:MsgBox ID="MsgBox2" runat="server" BackColor="#ffffff" ControlSkinID="None" HeaderHtml="Dialog Title" Height="192px" Width="345px" CssClass="card border border-1 p-2 text-center" IconUrl="Images/alerta_icon.svg" OnButtonClick="MsgBox2_ButtonClick">
        <HeaderStyleActive CssText="font-family: Ubuntu; font-size: 14px; font-weight: bold; color: #1C9489; text-align: center; padding: 5px;" />
        <ContentStyleActive CssText="font-family: Ubuntu; font-size: 14px; font-weight: bold; color: #7D7B7B; text-align: center; padding: 5px; width: 345px; height: 60px" />
        <FooterStyleActive CssText="width: 345px;" />
    </eo:MsgBox>
</asp:Content>
