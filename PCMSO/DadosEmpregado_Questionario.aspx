<%@ Page Language="C#"   AutoEventWireup="True"  MasterPageFile="~/Site.Master" CodeBehind="DadosEmpregado_Questionario.aspx.cs"  Inherits="Ilitera.Net.DadosEmpregado_Questionario" Title="Ilitera.Net" %>

<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>


<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
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
        .auto-style1 {
            width: 892px;
        }
    </style>
</asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent" >

    <script language="javascript" src="scripts/validador.js"></script>
    <script language="javascript">

        var g_DatePicker = null;




    var g_itemIndex = -1;
    var g_cellIndex = -1;

    function ShowContextMenu(e, grid, item, cell) {
        //Save the target cell index
        g_itemIndex = item.getIndex();
        g_cellIndex = cell.getColIndex();

        //Show the context menu
    
        //Return true to indicate that we have
        //displayed a context menu
        return true;
    }

    function OnContextMenuItemClicked(e, eventInfo) {
        var grid = eo_GetObject("<%=gridExames.ClientID%>");

        var item = eventInfo.getItem();


        switch (item.getText()) {

            case "Clínico":
                item ="6";

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

        var g_DatePicker = null;

        function datepicker_loaded(obj) {
            g_DatePicker = obj;
        }



 



        function datepicker_loaded(obj) {
            g_DatePicker = obj;
        }

        //Check whether "Back Ordered" column is checked
        function is_back_ordered(item) {
            
            return item.getCell(1).getValue() == "1";
        }

        //This function is called when user checks/unchecks
        //"back ordered" column
        function end_edit_back_ordered(cell, newValue) {
            //Get the GridItem object
            var item = cell.getItem();
            

            //Force update the "Available On" cell. Note
            //the code must be delayed with a timer so that
            //the value of the "Back Ordered" cell is first
            //updated
            setTimeout(function () {
                var availOnCell = item.getCell(2);
                availOnCell.refresh();
            }, 10);

            //Accept the new value
            return newValue;
        }

        //This function is called for "Available On" column
        //to translate a cell value to the final HTML to be
        //displayed in the cell. You can put any HTML inside
        //the grid cell even though this function only returns
        //simple text. For example, instead of returning
        //"N/A", you can return something like this:
        // <input type="text" disabled="disabled" style="width:100px" />
        //This would render a disabled textbox inside the Grid cell
        //when no value should be entered for "available On"
        //column. Note that the textbox rendered by this 
        //function, regardless disabled or not, are never
        //used by the user because the actual editing UI is
        //specified by the column's EditorTemplate, which is
        //a DatePicker control for this sample
        function get_avail_on_text(column, item, cellValue) {
            if (!is_back_ordered(item)) {
                            
                if (document.getElementById("<%=lblnreg.ClientID%>").value != "") {
                    document.getElementById("<%=lblnreg.ClientID%>").value = document.getElementById("<%=lblnreg.ClientID%>").value + "|" + item.getCell(5).getValue() + " ";
                }

                return "N/A";
            }
            else {
                if (!cellValue) {

                    document.getElementById("<%=lblreg.ClientID%>").value = document.getElementById("<%=lblreg.ClientID%>").value + "|" + item.getCell(5).getValue() + " ";

                    if (document.getElementById("<%=lblnreg.ClientID%>").value == "") document.getElementById("<%=lblnreg.ClientID%>").value = "|";

                    return "Click to edit";
                }
                else
                    return g_DatePicker.formatDate(cellValue, "MM/dd/yyyy");
            }
        }

        //This function is called when user clicks any cell in 
        //the "Available On" column. 
        function begin_edit_avail_on_date(cell) {
            //Get the item object
            var item = cell.getItem();

            //Do not enter edit mode unless "back ordered" is checked
            if (!is_back_ordered(item))
                return false;

            //Load cell value into the DatePicker control
            var v = cell.getValue();
            g_DatePicker.setSelectedDate(v);
        }

        //This function is called when user leaves edit mode
        //from an "Available On" cell. It returns the DatePicker
        //value to the Grid
        function end_edit_avail_on_date(cell) {
            return g_DatePicker.getSelectedDate();
        }


        //function OnItemCommand(grid, itemIndex, colIndex, commandName) {

        //    //grid.raiseItemCommandEvent(itemIndex, commandName);
        //    grid.raiseItemCommandEvent(itemIndex, colIndex.toString());
        //}


    </script>




      <asp:ScriptManager ID="ScriptManager1" runat="server">

    </asp:ScriptManager>

  
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>  



<%--<iframe id="SubDados" name="SubDados" align="middle" marginWidth="0" marginHeight="0" frameBorder="0"
				width="600" scrolling="no" src="../DadosEmpresa/SubDadosNull.aspx" >
			</iframe>--%>		
	<table class="auto-style1" id="Table1" cellspacing="0" cellpadding="0" align="center"
				border="0">
				<tr>
					<td nowrap align="center">
						<table class="fotoborder" bgcolor="white" id="Table2" cellspacing="0" 
                            cellpadding="0" align="center"
							border="0">
							<tr> <!-- celula esquerda-->
								<td class="normalFont" width="880" bgcolor="#EDFFEB">
									<table id="Table3" cellSpacing="0" cellPadding="0" width="880" border="0">
										<tr>
											<td vAlign="middle" align="left" width="7" bgcolor="#edffeb">
                                                <asp:label id="lblNome" runat="server" BackColor="#EDFFEB" Font-Bold="True">Nome&nbsp;</asp:label></TD>
											<td class="tableEdit" valign="middle" align="right" width="7" bgcolor="#edffeb">
                                                &nbsp;</TD>
											<td class="textDadosOpcao" valign="middle" align="left" width="31" bgcolor="#edffeb">
                                                &nbsp;</TD>
											<td valign="middle" align="left" width="445" bgcolor="#edffeb">
												<table id="Table4" cellSpacing="0" cellPadding="0" border="0">
													<tr>
														<td class="textDadosNome">
                                                            <asp:label id="lblValorNome" runat="server" 
                                                                Font-Bold="True" BackColor="#edffeb"></asp:label></TD>
													</tr>
												</TABLE>
											</TD>
										</TR>
									</TABLE>
									<TABLE id="Table5" cellSpacing="0" cellPadding="0" width="880" border="0">
										<TR>
											<TD class="tableSpace">
                                                <hr />
                                            </TD>
										</TR>
									</TABLE>
									<TABLE class="tableEdit" id="Table6" cellSpacing="0" cellPadding="0" width="880" border="0">
										<TR class="tableEdit" vAlign="middle">
											<TD class="tableEdit" vAlign="middle" align="left" width="7" bgColor="#edffeb">
                                                <asp:label id="lblGHE" runat="server" CssClass="textDadosOpcao" 
                                                    BackColor="#EDFFEB" Font-Bold="True">Tempo&nbsp;de&nbsp;Empresa</asp:label></TD>
											<TD class="tableEdit" vAlign="middle" align="right" width="7" bgColor="#edffeb">
                                                            <asp:label id="lblValorTempoEmpresa" runat="server" BackColor="#EDFFEB"></asp:label>
                                                </TD>
											<TD class="textDadosOpcao" align="left" width="105" bgColor="#edffeb">
                                                            &nbsp;</TD>
											<TD align="left" width="56" bgColor="#edffeb">
												<TABLE id="Table7" cellSpacing="0" cellPadding="0" width="30" border="0">
													<TR>
														<TD class="textDadosCampo" bgColor="#edffeb">&nbsp;</TD>
													</TR>
												</TABLE>
											    </TD>
											<TD class="textDadosOpcao" align="left" width="64" bgColor="#edffeb">
                                                &nbsp; 
                                                &nbsp; </TD>
											<TD align="left" width="98" bgColor="#edffeb">
												<TABLE id="Table8" cellSpacing="0" cellPadding="0" width="76" border="0">
													<TR>
														<TD class="textDadosCampo" width="97" bgColor="#edffeb">
                                                            &nbsp;</TD>
													</TR>
												</TABLE>
											</TD>
											<TD class="textDadosOpcao" align="left" width="32" bgColor="#edffeb">
                                                <asp:label id="lblIdade" runat="server" CssClass="textDadosOpcao" 
                                                    BackColor="#EDFFEB" Font-Bold="True">Idade&nbsp;</asp:label></TD>
											<TD align="left" width="34" bgColor="#edffeb">
												<TABLE id="Table9" cellSpacing="0" cellPadding="0" width="22" border="0">
													<TR>
														<TD class="textDadosCampo" width="27" bgColor="#edffeb">
                                                            <asp:label id="lblValorIdade" runat="server" BackColor="#EDFFEB"></asp:label>
                                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </TD>
													</TR>
												</TABLE>
											</TD>
											<TD class="textDadosOpcao" align="left" width="27" bgColor="#edffeb">
                                                <asp:label id="lblSexo" runat="server" CssClass="textDadosOpcao" 
                                                    BackColor="#EDFFEB" Font-Bold="True">Sexo&nbsp;</asp:label></TD>
											<TD align="left" width="60" bgColor="#edffeb">
												<TABLE id="Table10" cellSpacing="0" cellPadding="0" width="59" border="0">
													<TR>
														<TD class="textDadosCampo" width="58" bgColor="#edffeb">
                                                            <asp:label id="lblValorSexo" runat="server" BackColor="#EDFFEB"></asp:label></TD>
													</TR>
												</TABLE>
											</TD>
										</TR>
									</TABLE>
									<TABLE id="Table11" cellSpacing="0" cellPadding="0" width="880" border="0">
										<TR>
											<TD class="tableSpace">
                                                <hr />
                                            </TD>
										</TR>
									</TABLE>

									
									<TABLE id="Table17" cellSpacing="0" cellPadding="0" width="880" border="0">
										<TR vAlign="middle">
											<TD vAlign="middle" align="left" width="7" bgColor="#edffeb">
                                                <asp:label id="lblAdmissao" runat="server" CssClass="textDadosOpcao" 
                                                    BackColor="#EDFFEB" Font-Bold="True">Admissão&nbsp;</asp:label></TD>
											<TD class="textDadosOpcao" vAlign="middle" align="right" width="7" bgColor="#edffeb">
                                                &nbsp;</TD>
											<TD class="textDadosOpcao" align="left" width="51" bgColor="#edffeb">
                                                            <asp:label id="lblValorAdmissao" runat="server" BackColor="#EDFFEB"></asp:label></TD>
											<TD align="left" width="109" bgColor="#edffeb">
												<TABLE id="Table18" cellSpacing="0" cellPadding="0" width="75" border="0">
													<TR>
														<TD class="textDadosCampo" width="75" bgColor="#edffeb">
                                                            &nbsp;</TD>
													</TR>
												</TABLE>
											</TD>
											<TD class="textDadosOpcao" vAlign="bottom" align="left" width="50" bgColor="#edffeb">
                                                &nbsp;&nbsp;</TD>
											<TD align="left" width="121" bgColor="#edffeb">
												<TABLE id="Table19" cellSpacing="0" cellPadding="0" width="75" border="0">
													<TR>
														<TD class="textDadosCampo" width="75" bgColor="#edffeb">
                                                            &nbsp;</TD>
													</TR>
												</TABLE>
											</TD>
											<TD class="textDadosOpcao" align="left" width="69" bgColor="#edffeb">
                                                &nbsp;
                                                <asp:label id="lblDemissao" runat="server" CssClass="textDadosOpcao" 
                                                    BackColor="#EDFFEB" Font-Bold="True">Demissão&nbsp;</asp:label>
                                                </TD>
											<TD align="left" width="76" bgColor="#edffeb">
												<TABLE id="Table20" cellSpacing="0" cellPadding="0" border="0">
													<TR>
														<TD class="textDadosCampo" bgColor="#edffeb">
                                                            <asp:label id="lblValorDemissao" runat="server" BackColor="#EDFFEB"></asp:label></TD>
													</TR>
												</TABLE>
											</TD>
										</TR>
									</TABLE>
									<TABLE id="Table21" cellSpacing="0" cellPadding="0" width="880" border="0">
										<TR>
											<TD class="tableSpace">
                                                <hr />
                                            </TD>
										</TR>
									</TABLE>
									<TABLE id="Table22" cellSpacing="0" cellPadding="0" width="880" border="0">
										<TR vAlign="middle">
											<TD vAlign="middle" align="left" width="7" bgColor="#edffeb">
                                                <asp:label id="lblDataIni" runat="server" CssClass="textDadosOpcao" 
                                                    BackColor="#EDFFEB" Font-Bold="True">Início&nbsp;Função&nbsp;</asp:label></TD>
											<TD class="textDadosOpcao" vAlign="middle" align="right" width="7" bgColor="#edffeb">
                                                            <asp:label id="lblValorDataIni" 
                                                                runat="server" BackColor="#EDFFEB"></asp:label></TD>
											<TD class="textDadosOpcao" align="left" width="95" bgColor="#edffeb">
                                                            &nbsp;</TD>
											<TD align="left" width="100" bgColor="#edffeb">
												<TABLE id="Table23" cellSpacing="0" cellPadding="0" width="85" border="0">
													<TR>
														<TD class="textDadosCampo" width="85" bgColor="#edffeb">
                                                            &nbsp;</TD>
													</TR>
												</TABLE>
											</TD>
											<TD class="textDadosOpcao" align="left" width="33" bgColor="#edffeb">
                                                <asp:label id="lblSetor" runat="server" CssClass="textDadosOpcao" 
                                                    BackColor="#EDFFEB" Font-Bold="True">Setor&nbsp;</asp:label>&nbsp;
                                                </TD>
											<TD align="left" width="248" bgColor="#edffeb">
												<TABLE id="Table24" cellSpacing="0" cellPadding="0" border="0">
													<TR>
														<TD class="textDadosCampo" bgColor="#edffeb" width="247">
                                                            <asp:label id="lblValorSetor" runat="server" BackColor="#EDFFEB"></asp:label>&nbsp;
                                                <asp:label id="lblFuncao" runat="server" CssClass="textDadosOpcao" 
                                                    BackColor="#EDFFEB" Font-Bold="True">Função&nbsp;</asp:label>
                                                            <asp:label id="lblValorFuncao" runat="server" BackColor="#EDFFEB"></asp:label></TD>
													</TR>
												</TABLE>
											</TD>
										</TR>
									</TABLE>
									<TABLE id="Table21" cellSpacing="0" cellPadding="0" width="880" border="0">
										<TR>
											<TD class="tableSpace">
                                                <hr />
                                            </TD>
										</TR>
									</TABLE>

									
								</TD> <!-- celula direita-->
								

                                        
                                            <caption>
                                                <input id="txtIdUsuario" type="text" visible="False"  style="visibility:hidden"  />
                                                <input id="txtIdEmpregado" type="text" visible="False" style="visibility:hidden" />
                                                <input id="txtIdEmpresa" type="text" visible="False" style="visibility:hidden"/>
                                </caption>
                                
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD noWrap align="center">
					    <br />
					</TD>
				</TR>
			</TABLE>



     <eo:TabStrip ID="TabStrip1" runat="server" ControlSkinID="None" 
                            MultiPageID="MultiPage1" >
                            <topgroup>
                                <Items>
                                    <eo:TabItem Text-Html="Entrevista">
                                    </eo:TabItem>
                                    <eo:TabItem Text-Html="Informações Médicas">
                                    </eo:TabItem>
                                </Items>
                            </topgroup>
                            <lookitems>
                                <eo:TabItem Height="21" ItemID="_Default" LeftIcon-SelectedUrl="00010605"  
                                    LeftIcon-Url="00010604" 
                                    NormalStyle-CssText="background-image: url(00010602); background-repeat: repeat-x; font-weight: normal; color: black;" 
                                    RightIcon-SelectedUrl="00010607" RightIcon-Url="00010606" 
                                    SelectedStyle-CssText="background-image: url(00010603); background-repeat: repeat-x; font-weight: bold; color: #ff7e00;" 
                                    Text-Padding-Bottom="2" Text-Padding-Top="1">
                                    <subgroup itemspacing="1" 
                                        style-csstext="background-image:url(00010601);background-position-y:bottom;background-repeat:repeat-x;color:black;cursor:hand;font-family:Verdana;font-size:12px;">
                                    </subgroup> 
                                </eo:TabItem>
                            </lookitems>
                        </eo:TabStrip>



                            <eo:MultiPage ID="MultiPage1" runat="server" Height="400" Width="838">

                                <eo:PageView ID="Pageview2" runat="server">


                                            <TABLE class="defaultFont" cellSpacing="0" cellPadding="0" width="860" align="center" border="0" bgcolor="white">
				                            <TR>
					                            <TD class="defaultFont" align="center" colSpan="1">




                                                    <eo:Grid ID="grd_Clinicos" runat="server" BorderColor="Black" BorderWidth="1px" ClientSideOnItemCommand="OnItemCommand" ColumnHeaderAscImage="00050403" 
                                                        ColumnHeaderDescImage="00050404" ColumnHeaderDividerImage="00050402" ColumnHeaderDividerOffset="6" ColumnHeaderHeight="18" 
                                                        FixedColumnCount="1" Font-Bold="False" Font-Italic="False" Font-Names="Verdana" Font-Overline="False" Font-Size="9pt" 
                                                        Font-Strikeout="False" Font-Underline="False" GridLineColor="240, 240, 240" GridLines="Both" Height="149px" ItemHeight="22" 
                                                        KeyField="IdQuestionario" OnItemCommand="grd_Clinicos_ItemCommand" Width="851px">
                                                        <itemstyles>
                                                            <eo:GridItemStyleSet>
                                                                <ItemStyle CssText="background-color: white" />
                                                                <AlternatingItemStyle CssText="background-color:#eeeeee;" />
                                                                <SelectedStyle CssText="border-bottom-style:solid;border-left-style:solid;border-right-style:solid;border-top-style:solid;" />
                                                                <CellStyle CssText="padding-left:8px;padding-top:2px; color:#black;white-space:nowrap;" />
                                                            </eo:GridItemStyleSet>
                                                        </itemstyles>
                                                        <ColumnHeaderTextStyle CssText="" />
                                                        <ColumnHeaderStyle CssText="background-image:url('00050401');padding-left:8px;padding-top:2px;" />
                                                        <Columns>
                                                            <eo:StaticColumn AllowSort="True" DataField="DataQuestionario" HeaderText="Data da Entrevista" Name="DataQuestionario" ReadOnly="True" SortOrder="Ascending" Text="" Width="200">
                                                                <CellStyle CssText="background-color:#ffffcc;font-family:Tahoma;font-weight:bold;text-align:center" />
                                                            </eo:StaticColumn>
                                                            <eo:StaticColumn AllowSort="True" DataField="Tipo" HeaderText="Tipo" Name="Tipo" ReadOnly="True" Width="430">
                                                                <CellStyle CssText="text-align:center" />
                                                            </eo:StaticColumn>
                                                            <eo:ButtonColumn ButtonText="Editar" Name="Selecionar" Width="80">
                                                                <CellStyle CssText="font-family:Tahoma;font-size:7pt;text-align:center" />
                                                            </eo:ButtonColumn>
                                                            <eo:ButtonColumn ButtonText="Visualizar" Name="Selecionar" Width="80">
                                                                <CellStyle CssText="font-family:Tahoma;font-size:7pt;text-align:center" />
                                                            </eo:ButtonColumn>
                                                        </Columns>
                                                        <columntemplates>
                                                            <eo:TextBoxColumn>
                                                                <TextBoxStyle CssText="BORDER-RIGHT: #7f9db9 1px solid; PADDING-RIGHT: 2px; BORDER-TOP: #7f9db9 1px solid; PADDING-LEFT: 2px; FONT-SIZE: 8.75pt; PADDING-BOTTOM: 1px; MARGIN: 0px; BORDER-LEFT: #7f9db9 1px solid; PADDING-TOP: 2px; BORDER-BOTTOM: #7f9db9 1px solid; FONT-FAMILY: Tahoma" />
                                                            </eo:TextBoxColumn>
                                                            <eo:DateTimeColumn>
                                                                <datepicker controlskinid="None" daycellheight="16" daycellwidth="19" dayheaderformat="FirstLetter" disableddates="" othermonthdayvisible="True" selecteddates="" titleleftarrowimageurl="DefaultSubMenuIconRTL" titlerightarrowimageurl="DefaultSubMenuIcon">
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
                                                                </datepicker>
                                                            </eo:DateTimeColumn>
                                                            <eo:MaskedEditColumn>
                                                                <maskededit controlskinid="None" textboxstyle-csstext="BORDER-RIGHT: #7f9db9 1px solid; PADDING-RIGHT: 2px; BORDER-TOP: #7f9db9 1px solid; PADDING-LEFT: 2px; PADDING-BOTTOM: 1px; MARGIN: 0px; BORDER-LEFT: #7f9db9 1px solid; PADDING-TOP: 2px; BORDER-BOTTOM: #7f9db9 1px solid; font-family:Courier New;font-size:8pt;">
                                                                </maskededit>
                                                            </eo:MaskedEditColumn>
                                                        </columntemplates>
                                                        <FooterStyle CssText="padding-bottom:4px;padding-left:4px;padding-right:4px;padding-top:4px;" />
                                                    </eo:Grid>




                                                    <br />
                                                    <asp:HyperLink ID="hlkNovo" runat="server" SkinID="BoldLink">Nova Entrevista</asp:HyperLink>




                                                    </TD>


                                                </TR>

                                             </TABLE> 

                                    </eo:PageView>
                                <eo:PageView ID="Pageview1" runat="server">

                                            <TABLE class="defaultFont" cellSpacing="0" cellPadding="0" width="600" align="center" border="0" bgcolor="white">
				                            <TR>
					                            <TD class="defaultFont" align="center" colSpan="1"><asp:label id="lblExCli" 
                                                        runat="server" SkinID="TitleFont" style="font-weight: 700">Histórico de Atendimentos ( Informações Médicas )</asp:label>
                                                    <%--		</form>
	                            </body>
                            </HTML>
                            --%>
						                            <BR>
					                            </TD>
				                            <TR>
					                            <TD vAlign="top" align="right">
                    
                         
                            
                                                    <br />

                                    
                                                                <eo:Grid ID="gridExames" runat="server" BorderColor="Black" 
                                                                        BorderWidth="1px" ColumnHeaderAscImage="00050403" 
                                                                        ColumnHeaderDescImage="00050404" ColumnHeaderDividerImage="00050402" 
                                                                        FixedColumnCount="1" Font-Bold="False" Font-Italic="False" Font-Names="Verdana" 
                                                                        Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" 
                                                                        Font-Underline="False" GridLineColor="240, 240, 240" 
                                                                        GridLines="Both" Height="263px" Width="1024px" ColumnHeaderDividerOffset="6" ItemHeight="24" KeyField="IdHistorico" PageSize="40" FullRowMode="False" 
                                                                        OnItemCommand="grdExames_ItemCommand"  ClientSideOnCellSelected="OnCellSelected"
                                                                        ClientSideOnItemCommand="OnItemCommand"                                          >
                                                                    <ItemStyles>
                                                                        <eo:GridItemStyleSet>
                                                                            <ItemStyle CssText="background-color: white" />
                                                                            <AlternatingItemStyle CssText="background-color:#ffffcc;" />
                                                                            <SelectedStyle CssText="border-bottom-style:solid;border-left-style:solid;border-right-style:solid;border-top-style:solid;" />
                                                                            <CellStyle CssText="border-bottom-color:#99ccff;border-bottom-style:solid;border-left-color:#99ccff;border-left-style:solid;border-right-color:#99ccff;border-right-style:solid;border-top-color:#99ccff;border-top-style:solid;color:#black;padding-left:8px;padding-top:2px;white-space:nowrap;" />
                                                                        </eo:GridItemStyleSet>
                                                                    </ItemStyles>
                                                                    <ColumnHeaderTextStyle CssText="font-size:8pt;" />
                                                                    <ColumnHeaderStyle CssText="background-image:url('00050401');color:#666666;font-size:8pt;font-weight:bold;padding-left:8px;padding-top:2px;" />
                                                                    <ColumnHeaderHoverStyle CssText="font-size:8pt;" />
                                                                    <Columns>

                                            

                            <%--                                            <eo:StaticColumn HeaderText="Resultado" AllowSort="True" 
                                                                            DataField="Resultado" Name="Resultado" ReadOnly="False" 
                                                                            SortOrder="Ascending" Text="" Width="75">
                                                                            <CellStyle CssText="font-family:Tahoma;font-size:6pt;text-align:Center;" />           
                                                                        </eo:StaticColumn>--%>
  
                                                                        <eo:StaticColumn HeaderText="" AllowSort="True" 
                                                                            DataField="IdHistorico" Name="Id_Historico" ReadOnly="True" 
                                                                            SortOrder="Ascending" Text="" Width="0">
                                                                            <CellStyle CssText="background-color:#ffffcc;font-family:Tahoma;font-weight:bold;text-align:center" />
                                                                        </eo:StaticColumn>

                                                                        <eo:StaticColumn HeaderText="Exames Ocupacionais" 
                                                                            DataField="ExamesOcupacionais" Name="Exames Ocupacionais" ReadOnly="True" 
                                                                            SortOrder="Ascending" Text="" Width="185">
                                                                            <CellStyle CssText="border-bottom-color:gray;border-bottom-style:solid;border-bottom-width:1px;border-left-color:gray;border-left-style:solid;border-left-width:1px;border-right-color:gray;border-right-style:solid;border-right-width:1px;border-top-color:gray;border-top-style:solid;border-top-width:1px;font-family:Tahoma;font-size:8pt;text-align:left; vertical-align: middle;" />
                                                
                                                                        </eo:StaticColumn>

                                                                        <eo:StaticColumn HeaderText="Exames Ambulatoriais" 
                                                                            DataField="ExamesAmbulatoriais" Name="Exames Ambulatoriais" ReadOnly="True" 
                                                                            SortOrder="Ascending" Text="" Width="155">
                                                                            <CellStyle CssText="border-bottom-color:gray;border-bottom-style:solid;border-bottom-width:1px;border-left-color:gray;border-left-style:solid;border-left-width:1px;border-right-color:gray;border-right-style:solid;border-right-width:1px;border-top-color:gray;border-top-style:solid;border-top-width:1px;font-family:Tahoma;font-size:8pt;text-align:left; vertical-align: middle;" />
                                                                        </eo:StaticColumn>

                                                                        <eo:StaticColumn HeaderText="Afastamentos" 
                                                                            DataField="Atestados" Name="Atestados" Width="145">
                                                                            <CellStyle CssText="border-bottom-color:gray;border-bottom-style:solid;border-bottom-width:1px;border-left-color:gray;border-left-style:solid;border-left-width:1px;border-right-color:gray;border-right-style:solid;border-right-width:1px;border-top-color:gray;border-top-style:solid;border-top-width:1px;font-family:Tahoma;font-size:8pt;text-align:left; vertical-align: middle;" />
                                                                        </eo:StaticColumn>

                                                                        <eo:StaticColumn HeaderText="Afastamentos INSS" 
                                                                            DataField="AfastamentosINSS" Name="Afastamentos INSS" Width="145">
                                                                            <CellStyle CssText="border-bottom-color:gray;border-bottom-style:solid;border-bottom-width:1px;border-left-color:gray;border-left-style:solid;border-left-width:1px;border-right-color:gray;border-right-style:solid;border-right-width:1px;border-top-color:gray;border-top-style:solid;border-top-width:1px;font-family:Tahoma;font-size:8pt;text-align:left; vertical-align: middle;" />
                                                                        </eo:StaticColumn>

                                                                        <eo:StaticColumn HeaderText="Programa de Saúde" 
                                                                            DataField="ProgramaSaude" Name="Programa Saude" Width="155" >
                                                                            <CellStyle CssText="border-bottom-color:gray;border-bottom-style:solid;border-bottom-width:1px;border-left-color:gray;border-left-style:solid;border-left-width:1px;border-right-color:gray;border-right-style:solid;border-right-width:1px;border-top-color:gray;border-top-style:solid;border-top-width:1px;font-family:Tahoma;font-size:8pt;text-align:left; vertical-align: middle;" />                                                
                                                                        </eo:StaticColumn>

                                            
                                                                        <eo:StaticColumn DataField="AtendimentoAssistencial" HeaderText="Atendimento Assistencial" Name="Atendimento Assistencial" Width="220">
                                                                            <CellStyle CssText="border-bottom-color:gray;border-bottom-style:solid;border-bottom-width:1px;border-left-color:gray;border-left-style:solid;border-left-width:1px;border-right-color:gray;border-right-style:solid;border-right-width:1px;border-top-color:gray;border-top-style:solid;border-top-width:1px;font-family:Tahoma;font-size:8pt;text-align:left; vertical-align: middle;" />                                                
                                                                        </eo:StaticColumn>


                                            <eo:StaticColumn HeaderText="" AllowSort="False" 
                                                DataField="IdExameOcupacional" Name="IdExameOcupacional" ReadOnly="True" 
                                                SortOrder="Ascending" Text="" Width="0">
                                                <CellStyle CssText="background-color:#ffffcc;font-family:Tahoma;font-weight:bold;text-align:center" />
                                            </eo:StaticColumn>

                                            <eo:StaticColumn HeaderText="" AllowSort="False" 
                                                DataField="IdExameAmbulatorial" Name="IdExameAmbulatorial" ReadOnly="True" 
                                                SortOrder="Ascending" Text="" Width="0">
                                                <CellStyle CssText="background-color:#ffffcc;font-family:Tahoma;font-weight:bold;text-align:center" />
                                            </eo:StaticColumn>


                                            <eo:StaticColumn HeaderText="" AllowSort="False" 
                                                DataField="IdAtestado" Name="IdAtestado" ReadOnly="True" 
                                                SortOrder="Ascending" Text="" Width="0">
                                                <CellStyle CssText="background-color:#ffffcc;font-family:Tahoma;font-weight:bold;text-align:center" />
                                            </eo:StaticColumn>


                                            <eo:StaticColumn HeaderText="" AllowSort="False" 
                                                DataField="IdAfastamentoINSS" Name="IdAfastamentoINSS" ReadOnly="True" 
                                                SortOrder="Ascending" Text="" Width="0">
                                                <CellStyle CssText="background-color:#ffffcc;font-family:Tahoma;font-weight:bold;text-align:center" />
                                            </eo:StaticColumn>


                                            <eo:StaticColumn HeaderText="" AllowSort="False" 
                                                DataField="IdProgramaSaude" Name="IdProgramaSaude" ReadOnly="True" 
                                                SortOrder="Ascending" Text="" Width="0">
                                                <CellStyle CssText="background-color:#ffffcc;font-family:Tahoma;font-weight:bold;text-align:center" />
                                            </eo:StaticColumn>


                                            <eo:StaticColumn HeaderText="" AllowSort="False" 
                                                DataField="IdAtendimentoAssistencial" Name="IdAtendimentoAssistencial" ReadOnly="True" 
                                                SortOrder="Ascending" Text="" Width="0">
                                                <CellStyle CssText="background-color:#ffffcc;font-family:Tahoma;font-weight:bold;text-align:center" />
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

            
                                                </TD>
				                            </TR>
				                            <TR>
					                            <TD class="normalFont" style="HEIGHT: 15px" align="center"><P>
							                            <TABLE class="defaultFont" id="Table28" cellSpacing="0" cellPadding="0" width="590" align="right"
								                            border="0">
								                            <TR>
									                            <TD align="right"><asp:label id="lblTotRegistros" runat="server"></asp:label></TD>
								                            </TR>
							                            </TABLE>
							                            <BR>
							                            </P>
					                            </TD>
				                            </TR>
			                            </TABLE>

                                    </eo:PageView>



                                </eo:MultiPage>

			<%--<iframe id="SubDados" name="SubDados" align="middle" marginWidth="0" marginHeight="0" frameBorder="0"
				width="600" scrolling="no" src="../DadosEmpresa/SubDadosNull.aspx" >
			</iframe>--%>
<%--		</form>
	</body>
</HTML>
--%>

                  <br />
                  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                  <asp:Button ID="cmd_Voltar" runat="server" BackColor="#999999" 
                      CssClass="buttonFoto" Font-Size="XX-Small" onclick="cmd_Voltar_Click" 
                      Text="Voltar" Width="132px" />
                  <asp:TextBox ID="lblreg" runat="server" Width="0" Visible="False" ></asp:TextBox>
                  <br />
                  <asp:TextBox ID="lblnreg" runat="server" Width="0" Visible="False" ></asp:TextBox>
                  <br />

        
           
         <eo:MsgBox ID="MsgBox1" runat="server" BackColor="#47729F" ControlSkinID="None" 
        HeaderHtml="Dialog Title" Height="100px" Width="168px">
        <HeaderStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #ffbf00 3px solid; padding-left: 4px; font-weight: bold; font-size: 11px; padding-bottom: 2px; color: white; padding-top: 2px; border-bottom: #22456a 1px solid; font-family: verdana" />
        <ContentStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #7d97b6 1px solid; padding-left: 4px; border-left-width: 1px; font-size: 11px; border-left-color: #728eb8; padding-bottom: 4px; color: white; padding-top: 4px; border-bottom: #22456a 1px solid; font-family: verdana" />
        <FooterStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #7d97b6 1px solid; padding-left: 4px; border-left-width: 1px; font-size: 11px; border-left-color: #728eb8; padding-bottom: 4px; color: white; padding-top: 4px; border-bottom: #22456a 1px solid; font-family: verdana" />
        </eo:MsgBox>



                </ContentTemplate>

        </asp:UpdatePanel>
  



</asp:Content>
