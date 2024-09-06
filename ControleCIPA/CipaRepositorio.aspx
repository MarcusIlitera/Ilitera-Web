<%@ Page Language="C#"   AutoEventWireup="True"  MasterPageFile="~/Site.Master" CodeBehind="CipaRepositorio.aspx.cs"  Inherits="Ilitera.Net.ControleCIPA.CipaRepositorio" Title="Ilitera.Net" %>
 

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
        .auto-style1 {
            width: 892px;
        }
    </style>
</asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent" >
    <div class="container-fluid d-flex ms-5 ps-4">
        <div class="row gx-3 gy-2">
    <script language="javascript">
        var g_DatePicker = null;
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
        function OnItemCommand(grid, itemIndex, colIndex, commandName) {
            //grid.raiseItemCommandEvent(itemIndex, commandName);
            grid.raiseItemCommandEvent(itemIndex, colIndex.toString());
        }
    </script>

	<table class="auto-style1" id="Table1" cellspacing="0" cellpadding="0"
				border="0">
				<tr>
					<td nowrap>
						<table class="fotoborder" id="Table2" cellspacing="0" 
                            cellpadding="0">
							<tr> <!-- celula esquerda-->
								<td class="normalFont" width="880" >
									<table id="Table3" cellSpacing="0" cellPadding="0" width="880" border="0">
										<tr>
											<td vAlign="middle" align="left" width="7" >
                                                <asp:label id="lblNome" runat="server" CssClass="tituloLabel">Selecione CIPA</asp:label></TD>
											<td class="tableEdit" valign="middle" align="right" width="7" >
                                                &nbsp;</TD>
											<td class="textDadosOpcao" valign="middle" align="left" width="31" >
                                                &nbsp;</TD>
											<td valign="middle" align="left" width="445" >
												<table id="Table4" cellSpacing="0" cellPadding="0" border="0">
													<tr>
														<td class="textDadosNome">
                                                            <asp:dropdownlist id="ddlCIPA" runat="server" AutoPostBack="True" onselectedindexchanged="ddlCIPA_SelectedIndexChanged" CssClass="texto form-select form-select-sm"></asp:dropdownlist>
                                                            <asp:label id="lblStatus" runat="server" 
                                                                Font-Bold="True" ></asp:label></TD>
													</tr>
												</TABLE>
											</TD>
										</TR>
									</TABLE>
									<TABLE id="Table5" cellSpacing="0" cellPadding="0" width="880" border="0">
										<TR>
											<TD class="tableSpace">
                                                <asp:Literal runat="server"><br /></asp:Literal>
                                            </TD>
										</TR>
									</TABLE>

									<TABLE class="tableEdit" id="Table6" cellSpacing="0" cellPadding="0" width="880" border="0">
										<TR class="tableEdit" vAlign="middle">
											<TD class="tableEdit" vAlign="middle" align="left" width="7" >
                                                <asp:label id="lblGHE" runat="server" CssClass="tituloLabel">Representantes</asp:label>

                                    
                                                                <eo:Grid ID="grd_Representantes" runat="server" ColumnHeaderAscImage="00050403" ColumnHeaderHeight="30"
                                                                        ColumnHeaderDescImage="00050404" FixedColumnCount="1" GridLines="Both" Height="185px" Width="851px" ItemHeight="30" 
                                                                        KeyField="IdMembroCIPA" PageSize="10" CssClass="grid mt-2"  >
                                                                    <ItemStyles>
                                                                        <eo:GridItemStyleSet>
                                                                            <ItemStyle CssText="background-color: #FAFAFA" />
                                                                            <AlternatingItemStyle CssText="background-color: #F2F2F2;" />
                                                                            <SelectedStyle CssText="border-bottom-style:solid;border-left-style:solid;border-right-style:solid;border-top-style:solid;" />
                                                                            <CellStyle CssText="padding: 0.3rem 0.3rem 0.3rem 0.5rem; height: 30px !important;" />
                                                                        </eo:GridItemStyleSet>
                                                                    </ItemStyles>
                                                                    <ColumnHeaderTextStyle CssText="" />
                                                                    <ColumnHeaderStyle CssClass="tabelaC colunas"/>
                                                                    <Columns>

                                            

                            <%--                                            <eo:StaticColumn HeaderText="Resultado" AllowSort="True" 
                                                                            DataField="Resultado" Name="Resultado" ReadOnly="False" 
                                                                            SortOrder="Ascending" Text="" Width="75">
                                                                            <CellStyle CssText="font-family:Tahoma;font-size:6pt;text-align:Center;" />           
                                                                        </eo:StaticColumn>--%>
  
                                                                        <eo:StaticColumn HeaderText="" AllowSort="True" 
                                                                            DataField="IdMembroCIPA" Name="IdMembroCIPA" ReadOnly="True" 
                                                                            SortOrder="Ascending" Text="" Width="0">
                                                                            <CellStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500; color: #B0ABAB !important; text-align: left; height: 30px !important;" />
                                                                        </eo:StaticColumn>

                                                                        <eo:StaticColumn HeaderText="Nome" 
                                                                            DataField="Nome" Name="Nome" ReadOnly="True" 
                                                                            SortOrder="Ascending" Text="" Width="320">
                                                                            <CellStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500; color: #B0ABAB !important; text-align: left; height: 30px !important;" />                                             
                                                                        </eo:StaticColumn>

                                                                        <eo:StaticColumn HeaderText="Cargo" 
                                                                            DataField="Cargo" Name="Cargo" ReadOnly="True" 
                                                                            SortOrder="Ascending" Text="" Width="210">
                                                                            <CellStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500; color: #B0ABAB !important; text-align: left; height: 30px !important;" />
                                                                        </eo:StaticColumn>

                                                                        <eo:StaticColumn HeaderText="Estabilidade" 
                                                                            DataField="Estabilidade" Name="Estabilidade" ReadOnly="True" 
                                                                            SortOrder="Ascending" Text="" Width="250" DataFormat="">
                                                                            <CellStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500; color: #B0ABAB !important; text-align: left; height: 30px !important;" />
                                                                        </eo:StaticColumn>

  
                                                                    </Columns>
                                                                    <ColumnTemplates>
                                                                        <eo:TextBoxColumn>
                                                                            <TextBoxStyle CssText="BORDER-RIGHT: #7f9db9 1px solid; PADDING-RIGHT: 2px; BORDER-TOP: #7f9db9 1px solid; PADDING-LEFT: 2px; FONT-SIZE: 7pt; PADDING-BOTTOM: 1px; MARGIN: 0px; BORDER-LEFT: #7f9db9 1px solid; PADDING-TOP: 2px; BORDER-BOTTOM: #7f9db9 1px solid; FONT-FAMILY: Tahoma" />
                                                                        </eo:TextBoxColumn>
                                                                        <eo:DateTimeColumn>
                                                                            <DatePicker ControlSkinID="None" DayCellHeight="16" DayCellWidth="19" 
                                                                                DayHeaderFormat="FirstLetter" DisabledDates="" OtherMonthDayVisible="True" 
                                                                                SelectedDates="" TitleLeftArrowImageUrl="DefaultSubMenuIconRTL" 
                                                                                TitleRightArrowImageUrl="DefaultSubMenuIcon">
                                                                                <PickerStyle CssText="border-bottom-color:#7f9db9;border-bottom-style:solid;border-bottom-width:1px;border-left-color:#7f9db9;border-left-style:solid;border-left-width:1px;border-right-color:#7f9db9;border-right-style:solid;border-right-width:1px;border-top-color:#7f9db9;border-top-style:solid;border-top-width:1px;font-family:Courier New;font-size:7pt;margin-bottom:0px;margin-left:0px;margin-right:0px;margin-top:0px;padding-bottom:1px;padding-left:2px;padding-right:2px;padding-top:2px;" />
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
									</TABLE>
			

									

								

									<TABLE id="Table21" cellSpacing="0" cellPadding="0" width="880" border="0">
										<TR>
											<TD class="tableSpace">
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
                                    <eo:TabItem Text-Html="Reuniões CIPA">
                                    </eo:TabItem>
                                    <eo:TabItem Text-Html="Cronograma CIPA">
                                    </eo:TabItem>
                                </Items>
                            </topgroup>
                            <LookItems >
                                <eo:TabItem ItemID="_Default" 
                                    NormalStyle-CssText="font-family:'Ubuntu'; font-size: 12px; color: #B0ABAB; padding: 10px 10px; background: #F1F1F1; border-radius: 8px; cursor: hand; width: fit-content; margin-right: 1rem;"
                                    SelectedStyle-CssText="font-family:'Ubuntu'; font-size: 12px; color: #1C9489; font-weight: bold; padding: 10px 15px; background: #D9D9D9; border-radius: 8px; cursor: hand; width: fit-content; margin-right: 1rem;">
                                    <SubGroup OverlapDepth="8"
                                        Style-CssText="font-family:'Ubuntu'; font-size: 12px; color: #B0ABAB; padding: 10px 10px; border-radius: 8px; cursor: hand; width: fit-content;">
                                    </SubGroup>
                                </eo:TabItem>
                            </LookItems>
                        </eo:TabStrip>



                            <eo:MultiPage ID="MultiPage1" runat="server" Height="200" Width="838">

                                <eo:PageView ID="Pageview2" runat="server">


                                            <TABLE class="defaultFont" cellSpacing="0" cellPadding="0" width="860" align="center" border="0">
				                            <TR>
					                            <TD class="defaultFont" align="center" colSpan="1">




                                                    <eo:Grid ID="grd_Clinicos" runat="server" ColumnHeaderAscImage="00050403" 
                                                        ColumnHeaderDescImage="00050404" ColumnHeaderDividerOffset="6" ColumnHeaderHeight="30" 
                                                        FixedColumnCount="1" GridLines="Both" Height="149px" ItemHeight="30" 
                                                        KeyField="IdCipaRepositorio" Width="851px">
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
                                                            <eo:StaticColumn AllowSort="True" DataField="DataHora" HeaderText="Data da Reunião" Name="DataHora" ReadOnly="True" SortOrder="Ascending" Text="" Width="150">
                                                                <CellStyle CssText="background-color:#ffffcc;font-family:Tahoma;font-weight:bold;text-align:center" />
                                                            </eo:StaticColumn>
                                                            <eo:StaticColumn AllowSort="True" DataField="TipoReuniao" HeaderText="Tipo" Name="Tipo" ReadOnly="True" Width="155">
                                                                <CellStyle CssText="text-align:center" />
                                                            </eo:StaticColumn>
                                                            <eo:StaticColumn AllowSort="True" DataField="Evento" HeaderText="Evento" Name="Evento" ReadOnly="True" Width="320">
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

                                                    <div class="col-12 text-center">
                                                        <asp:HyperLink ID="hlkNovo" runat="server" SkinID="BoldLink" CssClass="btn">Novo Evento</asp:HyperLink>
                                                    </div>




                                                    </TD>


                                                </TR>

                                             </TABLE> 

                                    </eo:PageView>
                                <eo:PageView ID="Pageview1" runat="server">

                                            <TABLE class="defaultFont" cellSpacing="0" cellPadding="0" width="600" align="center" border="0">
				                            <TR>
					                            <TD class="defaultFont" colSpan="1"><asp:label id="lblExCli" 
                                                        runat="server" SkinID="TitleFont" CssClass="tituloLabel">Cronograma CIPA</asp:label>
                                                    <%--		</form>
	                            </body>
                            </HTML>
                            --%>
					                            </TD>
				                            <TR>
					                            <TD vAlign="top">
                    
                         
                            
                                                    <br />

                                    
                                                                <eo:Grid ID="gridExames" runat="server" ColumnHeaderAscImage="00050403" ColumnHeaderDescImage="00050404" FixedColumnCount="1" 
                                                                        GridLines="Both" Height="200px" Width="554px" ColumnHeaderDividerOffset="6" ItemHeight="30" ColumnHeaderHeight="30" KeyField="IdEvento" PageSize="10" CssClass="grid"  >
                                                                    <ItemStyles>
                                                                        <eo:GridItemStyleSet>
                                                                            <ItemStyle CssText="background-color: #FAFAFA" />
                                                                            <AlternatingItemStyle CssText="background-color: #F2F2F2;" />
                                                                            <SelectedStyle CssText="border-bottom-style:solid;border-left-style:solid;border-right-style:solid;border-top-style:solid;" />
                                                                            <CellStyle CssText="padding: 0.3rem 0.3rem 0.3rem 0.5rem; height: 30px !important;" />
                                                                        </eo:GridItemStyleSet>
                                                                    </ItemStyles>
                                                                    <ColumnHeaderTextStyle CssText="" />
                                                                    <ColumnHeaderStyle CssClass="tabelaC colunas"/>
                                                                    <Columns>

                                            

                            <%--                                            <eo:StaticColumn HeaderText="Resultado" AllowSort="True" 
                                                                            DataField="Resultado" Name="Resultado" ReadOnly="False" 
                                                                            SortOrder="Ascending" Text="" Width="75">
                                                                            <CellStyle CssText="font-family:Tahoma;font-size:6pt;text-align:Center;" />           
                                                                        </eo:StaticColumn>--%>
  
                                                                        <eo:StaticColumn HeaderText="" AllowSort="True" 
                                                                            DataField="IdEvento" Name="IdEvento" ReadOnly="True" 
                                                                            SortOrder="Ascending" Text="" Width="0">
                                                                            <CellStyle CssText="background-color:#ffffcc;font-family:Tahoma;font-weight:bold;text-align:center" />
                                                                        </eo:StaticColumn>

                                                                        <eo:StaticColumn HeaderText="Evento" 
                                                                            DataField="Tipo" Name="Tipo" ReadOnly="True" 
                                                                            SortOrder="Ascending" Text="" Width="255">
                                                                            <CellStyle CssText="border-bottom-color:gray;border-bottom-style:solid;border-bottom-width:1px;border-left-color:gray;border-left-style:solid;border-left-width:1px;border-right-color:gray;border-right-style:solid;border-right-width:1px;border-top-color:gray;border-top-style:solid;border-top-width:1px;font-family:Tahoma;font-size:9pt;text-align:left; vertical-align: middle;" />
                                                
                                                                        </eo:StaticColumn>

                                                                        <eo:StaticColumn HeaderText="Data do Evento" 
                                                                            DataField="DataEvento" Name="DataEvento" ReadOnly="True" 
                                                                            SortOrder="Ascending" Text="" Width="165">
                                                                            <CellStyle CssText="border-bottom-color:gray;border-bottom-style:solid;border-bottom-width:1px;border-left-color:gray;border-left-style:solid;border-left-width:1px;border-right-color:gray;border-right-style:solid;border-right-width:1px;border-top-color:gray;border-top-style:solid;border-top-width:1px;font-family:Tahoma;font-size:9pt;text-align:middle; vertical-align: middle;" />
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

                    <div class="col-12">
                        <asp:Button ID="cmd_Voltar" runat="server" CssClass="btn2" Text="Voltar"  onclick="cmd_Voltar_Click"  />
                    </div>
                  
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

        </div>
    </div>

</asp:Content>