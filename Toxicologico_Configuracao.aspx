<%@ Page Title="Ilitera.Net" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="True"    CodeBehind="Toxicologico_Configuracao.aspx.cs" Inherits="Ilitera.Net.Toxicologico_Configuracao" %>

<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>


<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
       <style type="text/css">
        .linha
   {
	font: 8px Verdana, Arial, Helvetica, sans-serif, Tahoma;
    }
           .btnLogarClass
           {
           }
           .auto-style2 {
               margin-left: 68px;
           }
           .auto-style3 {
               width: 44px;
           }
           .auto-style5 {
               width: 443px;
           }
           .auto-style6 {
               width: 543px;
           }
           .auto-style7 {
               width: 1135px;
           }
    </style>

</asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent" >

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
                            
              

                return "N/A";
            }
            else {
                if (!cellValue) {

                  
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






                    <table class="auto-style7">
                    <tr>
                    <td align="center" rowspan="1">

                        <asp:Label ID="lblTitulo" runat="server" style="font-weight: 700">Configuração - Toxicológico</asp:Label>
                        <br />
                        <br />
                        <asp:label id="lblPCMSO" runat="server" CssClass="boldFont">Selecione o PCMSO</asp:label>
                        &nbsp;
                        <asp:dropdownlist ID="ddlPCMSO2" runat="server" Width="190px" AutoPostBack="True" OnSelectedIndexChanged="ddlPCMSO2_SelectedIndexChanged"></asp:dropdownlist>
                        <br />
                        <br />
                        <asp:label id="lblPCMSO0" runat="server" CssClass="boldFont">Colaboradores por Sorteio</asp:label>
                        :&nbsp;&nbsp;
                        <asp:TextBox ID="txt_Colaboradores" runat="server" MaxLength="2" Width="46px"></asp:TextBox>
                        
                        &nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:label id="lblPCMSO1" runat="server" CssClass="boldFont">Período do Sorteio:</asp:label>
                        &nbsp;
                        <asp:TextBox ID="txt_Periodicidade" runat="server" MaxLength="2" Width="46px"></asp:TextBox>
                        
                        &nbsp;
                        <asp:dropdownlist ID="cmd_Periodicidade" runat="server" Width="104px" AutoPostBack="True" OnSelectedIndexChanged="ddlPCMSO2_SelectedIndexChanged" Height="16px">
                            <asp:ListItem Selected="True">Meses</asp:ListItem>
                            <asp:ListItem>Dias</asp:ListItem>
                        </asp:dropdownlist>
                        &nbsp;&nbsp;
                        <asp:label id="lblPCMSO2" runat="server" CssClass="boldFont">Data 1.Sorteio</asp:label>
                        <asp:TextBox ID="txt_Primeiro_Sorteio" runat="server" MaxLength="10" Width="118px"></asp:TextBox>
                        
                        &nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="cmd_Salvar" runat="server" Text="Salvar Configurações" Width="137px" />
                        <br />
                        <br />
                        
                        </td>
                        </table>

                        <table>

                        <td align="center" class="auto-style5">
                            

                                                              

                    &nbsp;<asp:Label ID="lblPPRA0" runat="server" Text="Colaboradores a serem sorteados" 
                        style="font-size: small; font-weight: 700" Font-Bold="True"></asp:Label>

                                
                                                    <eo:Grid ID="grd_Empregados" runat="server" BorderColor="Black" BorderWidth="1px" ClientSideOnItemCommand="OnItemCommand" ColumnHeaderAscImage="00050403" 
                                                        ColumnHeaderDescImage="00050404" ColumnHeaderDividerImage="00050402" ColumnHeaderDividerOffset="6" ColumnHeaderHeight="18" 
                                                        FixedColumnCount="1" Font-Bold="False" Font-Italic="False" Font-Names="Verdana" Font-Overline="False" Font-Size="9pt" 
                                                        Font-Strikeout="False" Font-Underline="False" GridLineColor="240, 240, 240" GridLines="Both" Height="300px" ItemHeight="22" 
                                                        KeyField="IdRepositorio" OnItemCommand="grd_Clinicos_ItemCommand" Width="420px">
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
                                                            <eo:StaticColumn AllowSort="True" DataField="tNo_Empg" HeaderText="Nome do Colaborador" Name="tNo_Empg" ReadOnly="True" SortOrder="Ascending" Text="" Width="220" >
                                                                <CellStyle CssText="background-color:#ffffcc;font-family:Tahoma;font-weight:bold;text-align:center" />
                                                            </eo:StaticColumn>                                                           
                                                            <eo:StaticColumn AllowSort="True" DataField="GHE" HeaderText="GHE" Name="GHE" ReadOnly="True" Width="190">
                                                                <CellStyle CssText="text-align:center" />
                                                                </eo:StaticColumn>
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

                                
                            </td>


                            <td class="auto-style3">




                                &nbsp;</td>


<td align="center" class="auto-style6" >

    <br />

    <asp:Label ID="Label1" runat="server" Text="Colaboradores já sorteados" 
                        style="font-size: small; font-weight: 700" Font-Bold="True"></asp:Label>

                                                        <eo:Grid ID="grd_Sorteados" runat="server" BorderColor="Black" BorderWidth="1px" ClientSideOnItemCommand="OnItemCommand" ColumnHeaderAscImage="00050403" 
                                                        ColumnHeaderDescImage="00050404" ColumnHeaderDividerImage="00050402" ColumnHeaderDividerOffset="6" ColumnHeaderHeight="18" 
                                                        FixedColumnCount="1" Font-Bold="False" Font-Italic="False" Font-Names="Verdana" Font-Overline="False" Font-Size="9pt" 
                                                        Font-Strikeout="False" Font-Underline="False" GridLineColor="240, 240, 240" GridLines="Both" Height="302px" ItemHeight="22" 
                                                        KeyField="IdRepositorio" OnItemCommand="grd_Clinicos_ItemCommand" Width="560px" CssClass="auto-style2">
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
                                                            <eo:StaticColumn AllowSort="True" DataField="tNo_Empg" HeaderText="Nome do Colaborador" Name="tNo_Empg" ReadOnly="True" SortOrder="Ascending" Text="" Width="220" >
                                                                <CellStyle CssText="background-color:#ffffcc;font-family:Tahoma;font-weight:bold;text-align:center" />
                                                            </eo:StaticColumn>                                                           
                                                            <eo:StaticColumn AllowSort="True" DataField="GHE" HeaderText="GHE" Name="GHE" ReadOnly="True" Width="190">
                                                                <CellStyle CssText="text-align:center" />
                                                                </eo:StaticColumn>
                                                            <eo:StaticColumn AllowSort="True" DataField="DataSorteior" HeaderText="Data Sorteio" Name="DataSorteio" ReadOnly="True" Width="120">
                                                                <CellStyle CssText="text-align:center" />
                                                                </eo:StaticColumn>

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
    <asp:Button ID="cmd_Disponibilizar" runat="server" Text="Disponibilizar colaborador para novo sorteio" Width="289px" />

    <br />

</td>
                                                    
                        </tr>




                        </table>

 <eo:MsgBox ID="MsgBox1" runat="server" BackColor="#47729F" ControlSkinID="None" 
        HeaderHtml="Dialog Title" Height="100px" Width="168px">
        <HeaderStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #ffbf00 3px solid; padding-left: 4px; font-weight: bold; font-size: 11px; padding-bottom: 2px; color: white; padding-top: 2px; border-bottom: #22456a 1px solid; font-family: verdana" />
        <ContentStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #7d97b6 1px solid; padding-left: 4px; border-left-width: 1px; font-size: 11px; border-left-color: #728eb8; padding-bottom: 4px; color: white; padding-top: 4px; border-bottom: #22456a 1px solid; font-family: verdana" />
        <FooterStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #7d97b6 1px solid; padding-left: 4px; border-left-width: 1px; font-size: 11px; border-left-color: #728eb8; padding-bottom: 4px; color: white; padding-top: 4px; border-bottom: #22456a 1px solid; font-family: verdana" />
    </eo:MsgBox>


</asp:Content>

