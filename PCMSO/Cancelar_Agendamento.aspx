<%@ Page Title="Ilitera.Net" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="True"
    CodeBehind="Cancelar_Agendamento.aspx.cs" Inherits="Ilitera.Net.Cancelar_Agendamento" %>

<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>




<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <style type="text/css">
        .style1
        {
            width: 483px;
        }
        .style2
        {
            width: 237px;
        }
        #n1
        {
            width: 451px;
        }
        #Table1
        {
            width: 740px;
        }
        </style>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <script language="javascript">

	    function Reload() {
	        var f = document.getElementById('SubDados');
	        //f.src = f.src;
	        f.contentWindow.location.reload(true);
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

   
		
	<table class="normalFont" id="Table1" cellspacing="0" cellpadding="0" width="600" align="center"
				border="0">
				
                                    <tr>
								
                                    </TD>

                                    
                                       
                                                <input id="txtIdUsuario" type="text" visible="False"  style="visibility:hidden"  />
                                                <input id="txtIdEmpregado" type="text" visible="False" style="visibility:hidden" />
                                                <input id="txtIdEmpresa" type="text" visible="False" style="visibility:hidden"/>
                            
                                </td>
							</TR>
					</TD>
				</TR>
				<TR>
					<TD noWrap align="center"><BR>
					    <asp:Label ID="Label5" runat="server" Font-Bold="True" 
                            Text="Cancelamento de ASO/Guia"></asp:Label>
                        <br />
                        <br />
					    <asp:Button ID="cmd_Voltar" runat="server" BackColor="#999999" 
                            CssClass="buttonFoto" Font-Size="XX-Small" onclick="cmd_Voltar_Click" 
                            Text="Voltar" Width="132px" />
                        <br>
                        <br>




          <eo:Grid ID="gridEmpregados" runat="server" BorderColor="Black" 
                BorderWidth="1px" ColumnHeaderAscImage="00050403" 
                ColumnHeaderDescImage="00050404" ColumnHeaderDividerImage="00050402" 
                Font-Bold="False" Font-Italic="False" Font-Names="Verdana" 
                Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" 
                Font-Underline="False" GridLineColor="240, 240, 240" 
                GridLines="Both" Height="204px" Width="400px" ColumnHeaderDividerOffset="6" 
                ColumnHeaderHeight="18" ItemHeight="22" KeyField="IdExameBase"  
                ClientSideOnContextMenu="ShowContextMenu" FullRowMode="False"
                OnItemCommand="gridEmpregados_ItemCommand"  ClientSideOnItemCommand="OnItemCommand" 
                ClientSideOnCellSelected="OnCellSelected" >
            <ItemStyles>
                <eo:GridItemStyleSet>
                    <ItemStyle CssText="background-color: white" />
                    <AlternatingItemStyle CssText="background-color:#eeeeee;" />
                    <SelectedStyle CssText="background-color:#99ccff;border-bottom-style:solid;border-left-style:solid;border-right-style:solid;border-top-style:solid;" />
                    <CellStyle CssText="padding-left:8px;padding-top:2px; color:#black;white-space:nowrap;" />
                </eo:GridItemStyleSet>
            </ItemStyles>
            <ColumnHeaderTextStyle CssText="" />
            <ColumnHeaderStyle CssText="background-image:url('00050401');padding-left:8px;padding-top:2px;" />
            <Columns>
               
                <eo:StaticColumn HeaderText="Exame" AllowSort="True" 
                    DataField="Exame" Name="Exame" ReadOnly="True" 
                    SortOrder="Ascending" Text="" Width="180" >
                    <CellStyle CssText="background-color:#ffffcc;font-family:Tahoma;font-weight:bold;text-align:left" />
                </eo:StaticColumn>          
                <eo:StaticColumn HeaderText="Data Exame" AllowSort="True" 
                    DataField="Data_Exame" Name="Data_Exame" ReadOnly="True" 
                    Width="90">
                    <CellStyle CssText="text-align:center" />
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

                        &nbsp;&nbsp;
                        <br>
                        <asp:Button ID="cmd_Excluir" runat="server" BackColor="#FF8080" 
                            CssClass="buttonFoto" Font-Size="XX-Small" onclick="cmd_Excluir_Click" 
                            Text="Excluir" Width="132px" />
			
					    &nbsp;
                        <asp:Label ID="lbl_ID" runat="server" Text="0" Visible="False"></asp:Label>
			
					</TD>
				</TR>
                </TABLE>
            

                    
         <eo:MsgBox ID="MsgBox1" runat="server" BackColor="#47729F" ControlSkinID="None" 
        HeaderHtml="Dialog Title" Height="100px" Width="168px">
        <HeaderStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #ffbf00 3px solid; padding-left: 4px; font-weight: bold; font-size: 11px; padding-bottom: 2px; color: white; padding-top: 2px; border-bottom: #22456a 1px solid; font-family: verdana" />
        <ContentStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #7d97b6 1px solid; padding-left: 4px; border-left-width: 1px; font-size: 11px; border-left-color: #728eb8; padding-bottom: 4px; color: white; padding-top: 4px; border-bottom: #22456a 1px solid; font-family: verdana" />
        <FooterStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #7d97b6 1px solid; padding-left: 4px; border-left-width: 1px; font-size: 11px; border-left-color: #728eb8; padding-bottom: 4px; color: white; padding-top: 4px; border-bottom: #22456a 1px solid; font-family: verdana" />
        </eo:MsgBox>


    </asp:Content>