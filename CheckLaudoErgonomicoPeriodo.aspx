<%@ Page Title="Ilitera.Net" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="True"    CodeBehind="CheckLaudoErgonomicoPeriodo.aspx.cs" Inherits="Ilitera.Net.CheckLaudoErgonomicoPeriodo" %>


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

            <div class="col-11">
                <div class="row">

                    <div class="col-12">
                        <div class="row">
                            <div class="col-md-4 gy-2">
                                <asp:Label ID="lblTitulo" runat="server" CssClass="tituloLabel form-label">Laudo Ergonômico</asp:Label>
                                <asp:dropdownlist ID="ddlLaudos2" runat="server" CssClass="texto form-select"></asp:dropdownlist>
                            </div>
                        </div>
                    </div>
                    
                    <div class="col-md-2 mt-3 mb-3">
                        <asp:Button ID="btnCronograma" OnClick="lkbCronograma_Click" runat="server" CssClass="btn" ToopTip="Laudo Ergonômico - Cronograma" Text="Gerar AEP - Cronograma" />
                    </div>

                    <div class="col-md-2 mt-3 mb-3">
                        <asp:Button ID="btnLaudoErgonomico"  OnClick="lkbLaudoErgonomico_Click" runat="server" CssClass="btn" ToolTip="Laudo Ergonômico - Análise Ergonômica" Text="Gerar AEP Completa" />
                    </div>

                    <div class="col-12 subtituloBG mb-3" style="padding-top: 10px;">
                        <asp:Label ID="lblPPRA0" runat="server" CssClass="subtitulo" Text="Certificado com Assinatura Digital"></asp:Label>
                    </div>

                    <div class="col-12">
                        <eo:Grid ID="grd_Clinicos" runat="server" ClientSideOnItemCommand="OnItemCommand" ColumnHeaderAscImage="00050403" 
                            ColumnHeaderDescImage="00050404" ColumnHeaderDividerOffset="6" ColumnHeaderHeight="30" OnItemCommand="grd_Clinicos_ItemCommand"  FixedColumnCount="1" GridLines="Both" Height="227px" ItemHeight="30" 
                            KeyField="IdRepositorio" Width="640px" CssClass="grid">
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
                                <eo:StaticColumn AllowSort="True" DataField="DataHora" HeaderText="Data do Documento" Name="DataHora" ReadOnly="True" SortOrder="Ascending" Text="" Width="140" DataFormat="{0:dd/MM/yyyy}" DataType="DateTime">
                                    <CellStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500; color: #7D7B7B !important; text-align: left; height: 35px !important; padding: 0.3rem 0.3rem 0.3rem 0.5rem; height: 35px !important;" />
                                </eo:StaticColumn>
                                <eo:StaticColumn AllowSort="True" DataField="Descricao" HeaderText="Descrição" Name="Descricao" ReadOnly="True" Width="390">
                                    <CellStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500; color: #7D7B7B !important; text-align: left; height: 35px !important; padding: 0.3rem 0.3rem 0.3rem 0.5rem; height: 35px !important;" />
                                </eo:StaticColumn>
                                <eo:StaticColumn AllowSort="True" DataField="Anexo" HeaderText="Anexo" Name="Anexo" ReadOnly="True" Width="1">
                                    <CellStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500; color: #7D7B7B !important; text-align: left; height: 35px !important; padding: 0.3rem 0.3rem 0.3rem 0.5rem; height: 35px !important;" />
                                </eo:StaticColumn>
                                <eo:ButtonColumn ButtonText="Visualizar" Name="Selecionar" Width="90">
                                    <CellStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500; color: #7D7B7B !important; text-align: left; height: 35px !important; padding: 0.3rem 0.3rem 0.3rem 0.5rem; height: 35px !important;" />
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
                    </div>

                </div>
            </div>

                   
                    

                                
 

                                
                            </td>
                            
                        </tr>
</table>


 <eo:MsgBox ID="MsgBox1" runat="server" BackColor="#47729F" ControlSkinID="None" 
        HeaderHtml="Dialog Title" Height="100px" Width="168px">
        <HeaderStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #ffbf00 3px solid; padding-left: 4px; font-weight: bold; font-size: 11px; padding-bottom: 2px; color: white; padding-top: 2px; border-bottom: #22456a 1px solid; font-family: verdana" />
        <ContentStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #7d97b6 1px solid; padding-left: 4px; border-left-width: 1px; font-size: 11px; border-left-color: #728eb8; padding-bottom: 4px; color: white; padding-top: 4px; border-bottom: #22456a 1px solid; font-family: verdana" />
        <FooterStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #7d97b6 1px solid; padding-left: 4px; border-left-width: 1px; font-size: 11px; border-left-color: #728eb8; padding-bottom: 4px; color: white; padding-top: 4px; border-bottom: #22456a 1px solid; font-family: verdana" />
    </eo:MsgBox>

        </div>
    </div>
</asp:Content>
