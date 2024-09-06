<%@ Page Language="C#"   AutoEventWireup="True"  MasterPageFile="~/Site.Master" CodeBehind="DadosEmpregado_Dashboard.aspx.cs"  Inherits="Ilitera.Net.DadosEmpregado_Dashboard" Title="Ilitera.Net" %>

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

<script language="javascript" src="scripts/validador.js"></script>
<script type="text/javascript">

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





      <asp:ScriptManager ID="ScriptManager1" runat="server">

    </asp:ScriptManager>



    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate> 
                <div class="container-fluid d-flex ms-5 ps-4">
                    <div class="row gx-3 gy-2 w-100">
                        <div class="col-11 mb-4">
                            <div class="row">
                                    <%-- LINHA UM --%>
                                <div class="col-md-4 gx-3 gy-2">
                                        <asp:Label ID="lblNome" runat="server" Text="Nome" class="tituloLabel form-label col-form-label col-form-label-sm" Style="text-align: left;"></asp:Label>
                                    <asp:Label ID="lblValorNome" runat="server" class="texto form-control form-control-sm" Style="text-align: left;"></asp:Label>
                                </div>

                                <div class="col-md-2 gx-3 gy-2">
                                    <asp:Label ID="lblGHE" runat="server" Text="Tempo de Empresa" class="tituloLabel form-label col-form-label col-form-label-sm" Style="text-align: left;"></asp:Label>
                                    <asp:Label ID="lblValorTempoEmpresa" runat="server" class="texto form-control form-control-sm" Style="text-align: left;"></asp:Label>
                                    </div>

                                <div class="col-md-1 gx-3 gy-2">
                                    <asp:Label ID="lblIdade" runat="server" Text="Idade" class="tituloLabel form-label col-form-label col-form-label-sm" Style="text-align: left;"></asp:Label>
                                    <asp:Label ID="lblValorIdade" runat="server" class="texto form-control form-control-sm" Style="text-align: left;"></asp:Label>
                                    </div>

                                <div class="col-md-1 gx-3 gy-2">
                                    <asp:Label ID="lblSexo" runat="server" Text="Sexo" class="tituloLabel form-label col-form-label col-form-label-sm" Style="text-align: left;"></asp:Label>
                                    <asp:Label ID="lblValorSexo" runat="server" class="texto form-control form-control-sm" Style="text-align: left;"></asp:Label>
                                </div>

                                <%--LINHA DOIS --%>
                                <div class="col-md-2 gx-3 gy-2">
                                    <asp:label id="Label1" runat="server" CssClass="tituloLabel form-label" >Admissão&nbsp;</asp:label>
                                    <asp:label id="Label2" runat="server" CssClass="texto form-control form-control-sm"></asp:label>
                                </div>
                        
                                <div class="col-md-2 gx-3 gy-2">
                                    <asp:label id="Label3" runat="server" CssClass="tituloLabel form-label">Demissão&nbsp;</asp:label>
                                    <asp:label id="Label4" runat="server" CssClass="texto form-control form-control-sm"></asp:label>
                                </div>
                        
                                <div class="col-md-2 gx-3 gy-2">
                                    <asp:label id="Label5" runat="server" CssClass="tituloLabel form-label">Início&nbsp;Função&nbsp;</asp:label>
                                    <asp:label id="Label6" runat="server" CssClass="texto form-control form-control-sm"></asp:label>
                                </div>
                        
                                <div class="col-md-5 gx-3 gy-2">
                                    <asp:label id="Label7" runat="server" CssClass="tituloLabel form-label">Setor&nbsp;</asp:label>
                                    <asp:label id="Label8" runat="server" CssClass="texto form-control form-control-sm"></asp:label>
                                </div>
                        
                                <div class="col-md-5 gx-3 gy-2">
                                    <asp:label id="Label9" runat="server" CssClass="tituloLabel form-label">Função&nbsp;</asp:label>
                                    <asp:label id="Label10" runat="server" CssClass="texto form-control form-control-sm"></asp:label>
                                </div>

            
                                <div class="col-md-4 gx-3 gy-2">
                                    <asp:Label ID="Label11" runat="server" Text="Nome" class="tituloLabel form-label col-form-label col-form-label-sm" Style="text-align: left;"></asp:Label>
                                    <asp:Label ID="Label12" runat="server" class="texto form-control form-control-sm" Style="text-align: left;"></asp:Label>
                                </div>
                        
                                <div class="col-md-2 gx-3 gy-2">
                                    <asp:Label ID="Label13" runat="server" Text="Tempo de Empresa" class="tituloLabel form-label col-form-label col-form-label-sm" Style="text-align: left;"></asp:Label>
                                    <asp:Label ID="Label14" runat="server" class="texto form-control form-control-sm" Style="text-align: left;"></asp:Label>
                                </div>
                        
                                <div class="col-md-1 gx-3 gy-2">
                                    <asp:Label ID="Label15" runat="server" Text="Idade" class="tituloLabel form-label col-form-label col-form-label-sm" Style="text-align: left;"></asp:Label>
                                    <asp:Label ID="Label16" runat="server" class="texto form-control form-control-sm" Style="text-align: left;"></asp:Label>
                                </div>
                        
                                <div class="col-md-1 gx-3 gy-2">
                                    <asp:Label ID="Label17" runat="server" Text="Sexo" class="tituloLabel form-label col-form-label col-form-label-sm" Style="text-align: left;"></asp:Label>
                                    <asp:Label ID="Label18" runat="server" class="texto form-control form-control-sm" Style="text-align: left;"></asp:Label>
                                </div>

                                <div class="col-md-2 gx-3 gy-2">
                                    <asp:label id="lblAdmissao" runat="server" CssClass="tituloLabel form-label col-form-label-sm" >Admissão&nbsp;</asp:label>
                                    <asp:label id="lblValorAdmissao" runat="server" CssClass="texto form-control form-control-sm"></asp:label>
                                </div>
                        
                                <div class="col-md-2 gx-3 gy-2">
                                    <asp:label id="lblDemissao" runat="server" CssClass="tituloLabel form-label col-form-label-sm">Demissão&nbsp;</asp:label>
                                    <asp:label id="lblValorDemissao" runat="server" CssClass="texto form-control form-control-sm"></asp:label>
                                </div>
                        
                                <div class="col-md-2 gx-3 gy-2">
                                    <asp:label id="lblDataIni" runat="server" CssClass="tituloLabel form-label col-form-label-sm">Início&nbsp;Função&nbsp;</asp:label>
                                    <asp:label id="lblValorDataIni" runat="server" CssClass="texto form-control form-control-sm"></asp:label>
                                </div>
                        
                                <div class="col-md-5 gx-3 gy-2">
                                    <asp:label id="lblSetor" runat="server" CssClass="tituloLabel form-label col-form-label-sm">Setor&nbsp;</asp:label>
                                    <asp:label id="lblValorSetor" runat="server" CssClass="texto form-control form-control-sm"></asp:label>
                                </div>
                        
                                <div class="col-md-5 gx-3 gy-2">
                                    <asp:label id="lblFuncao" runat="server" CssClass="tituloLabel form-label col-form-label-sm">Função&nbsp;</asp:label>
                                    <asp:label id="lblValorFuncao" runat="server" CssClass="texto form-control form-control-sm"></asp:label>
                                </div>
                            </div>
                        </div>



<%--<iframe id="SubDados" name="SubDados" align="middle" marginWidth="0" marginHeight="0" frameBorder="0"
				width="600" scrolling="no" src="../DadosEmpresa/SubDadosNull.aspx" >
			</iframe>--%>		
	
            <div class="col-11 subtituloBG mb-3" style="padding-top: 10px;">
                <asp:label id="lblExCli" runat="server" CssClass="subtitulo" >Histórico de Atendimentos ( Informações Médicas )</asp:label>
            </div>
                                <div class="col-11 d-flex justify-content-center mb-3">
                                    <div class="row">
                                        <div class="col-12">
                                            <eo:Grid ID="gridExames" runat="server" ColumnHeaderAscImage="00050403" 
                                                    ColumnHeaderDescImage="00050404" FixedColumnCount="1" GridLines="Both" Height="263px" Width="1024px" ColumnHeaderDividerOffset="6" 
                                                    ColumnHeaderHeight="30" ItemHeight="30" KeyField="IdHistorico" PageSize="40" FullRowMode="False" CssClass="grid" 
 OnItemCommand="grdExames_ItemCommand"  ClientSideOnCellSelected="OnCellSelected"
                                            ClientSideOnItemCommand="OnItemCommand"                                          >                                                <%-- <ItemStyles>
                                                    <eo:GridItemStyleSet>
                                                        <ItemStyle CssText="background-color: white" />
                                                        <AlternatingItemStyle CssText="background-color:#ffffcc;" />
                                                        <SelectedStyle CssText="border-bottom-style:solid;border-left-style:solid;border-right-style:solid;border-top-style:solid;" />
                                                        <CellStyle CssText="border-bottom-color:#99ccff;border-bottom-style:solid;border-left-color:#99ccff;border-left-style:solid;border-right-color:#99ccff;border-right-style:solid;border-top-color:#99ccff;border-top-style:solid;color:#black;padding-left:8px;padding-top:2px;white-space:nowrap;" />
                                                    </eo:GridItemStyleSet>
                                                </ItemStyles>
                                                <ColumnHeaderTextStyle CssText="font-size:8pt;" />
                                                <ColumnHeaderStyle CssText="background-image:url('00050401');color:#666666;font-size:8pt;font-weight:bold;padding-left:8px;padding-top:2px;" />
                                                <ColumnHeaderHoverStyle CssText="font-size:8pt;" />--%>
                                                    <ItemStyles>
                                                        <eo:GridItemStyleSet>
                                                            <ItemStyle CssText="background-color: #FAFAFA;" />
                                                            <AlternatingItemStyle CssText="background-color: #F2F2F2;" />
                                                            <SelectedStyle CssText="border-bottom-style:solid;border-left-style:solid;border-right-style:solid;border-top-style:solid;" />
                                                            <CellStyle CssText="padding: 0.3rem 0.3rem 0.3rem 0.5rem; height: 35px !important;" />
                                                        </eo:GridItemStyleSet>
                                                    </ItemStyles>
                                                    <ColumnHeaderTextStyle CssText="" />
                                                    <ColumnHeaderStyle CssClass="tabelaC colunas" />
                                                <Columns>

        <%--                                            <eo:StaticColumn HeaderText="Resultado" AllowSort="True" 
                                                        DataField="Resultado" Name="Resultado" ReadOnly="False" 
                                                        SortOrder="Ascending" Text="" Width="75">
                                                        <CellStyle CssText="font-family:Tahoma;font-size:6pt;text-align:Center;" />           
                                                    </eo:StaticColumn>--%>
  
                                                    <eo:StaticColumn HeaderText="" AllowSort="True" 
                                                        DataField="IdHistorico" Name="Id_Historico" ReadOnly="True" 
                                                        SortOrder="Ascending" Text="" Width="0">
                                                        <CellStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500; color: #7D7B7B !important; text-align: left; height: 35px !important; padding: 0.3rem 0.3rem 0.3rem 0.5rem; height: 35px !important;" />
                                                    </eo:StaticColumn>

                                                    <eo:StaticColumn HeaderText="Exames Ocupacionais" 
                                                        DataField="ExamesOcupacionais" Name="Exames Ocupacionais" ReadOnly="True" 
                                                        SortOrder="Ascending" Text="" Width="185">
                                                        <CellStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500; color: #7D7B7B !important; text-align: left; height: 35px !important; padding: 0.3rem 0.3rem 0.3rem 0.5rem; height: 35px !important;" />
                                                
                                                    </eo:StaticColumn>

                                                    <eo:StaticColumn HeaderText="Exames Ambulatoriais" 
                                                        DataField="ExamesAmbulatoriais" Name="Exames Ambulatoriais" ReadOnly="True" 
                                                        SortOrder="Ascending" Text="" Width="155">
                                                        <CellStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500; color: #7D7B7B !important; text-align: left; height: 35px !important; padding: 0.3rem 0.3rem 0.3rem 0.5rem; height: 35px !important;" />
                                                    </eo:StaticColumn>

                                                    <eo:StaticColumn HeaderText="Afastamentos" 
                                                        DataField="Atestados" Name="Atestados" Width="145">
                                                        <CellStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500; color: #7D7B7B !important; text-align: left; height: 35px !important; padding: 0.3rem 0.3rem 0.3rem 0.5rem; height: 35px !important;" />
                                                    </eo:StaticColumn>

                                                    <eo:StaticColumn HeaderText="Afastamentos INSS" 
                                                        DataField="AfastamentosINSS" Name="Afastamentos INSS" Width="145">
                                                        <CellStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500; color: #7D7B7B !important; text-align: left; height: 35px !important; padding: 0.3rem 0.3rem 0.3rem 0.5rem; height: 35px !important;" />
                                                    </eo:StaticColumn>

                                                    <eo:StaticColumn HeaderText="Programa de Saúde" 
                                                        DataField="ProgramaSaude" Name="Programa Saude" Width="155" >
                                                        <CellStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500; color: #7D7B7B !important; text-align: left; height: 35px !important; padding: 0.3rem 0.3rem 0.3rem 0.5rem; height: 35px !important;" />                                                
                                                    </eo:StaticColumn>                                            

                                                    <eo:StaticColumn DataField="AtendimentoAssistencial" HeaderText="Atendimento Assistencial" Name="Atendimento Assistencial" Width="220">
                                                        <CellStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500; color: #7D7B7B !important; text-align: left; height: 35px !important; padding: 0.3rem 0.3rem 0.3rem 0.5rem; height: 35px !important;" />                                                
                                                    </eo:StaticColumn>

                                                    <eo:StaticColumn HeaderText="" AllowSort="False" 
                                                        DataField="IdExameOcupacional" Name="IdExameOcupacional" ReadOnly="True" 
                                                        SortOrder="Ascending" Text="" Width="0">
                                                        <CellStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500; color: #7D7B7B !important; text-align: left; height: 35px !important; padding: 0.3rem 0.3rem 0.3rem 0.5rem; height: 35px !important;" />
                                                    </eo:StaticColumn>

                                                    <eo:StaticColumn HeaderText="" AllowSort="False" 
                                                        DataField="IdExameAmbulatorial" Name="IdExameAmbulatorial" ReadOnly="True" 
                                                        SortOrder="Ascending" Text="" Width="0">
                                                        <CellStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500; color: #7D7B7B !important; text-align: left; height: 35px !important; padding: 0.3rem 0.3rem 0.3rem 0.5rem; height: 35px !important;" />
                                                    </eo:StaticColumn>


                                                    <eo:StaticColumn HeaderText="" AllowSort="False" 
                                                        DataField="IdAtestado" Name="IdAtestado" ReadOnly="True" 
                                                        SortOrder="Ascending" Text="" Width="0">
                                                        <CellStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500; color: #7D7B7B !important; text-align: left; height: 35px !important; padding: 0.3rem 0.3rem 0.3rem 0.5rem; height: 35px !important;" />
                                                    </eo:StaticColumn>


                                                    <eo:StaticColumn HeaderText="" AllowSort="False" 
                                                        DataField="IdAfastamentoINSS" Name="IdAfastamentoINSS" ReadOnly="True" 
                                                        SortOrder="Ascending" Text="" Width="0">
                                                        <CellStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500; color: #7D7B7B !important; text-align: left; height: 35px !important; padding: 0.3rem 0.3rem 0.3rem 0.5rem; height: 35px !important;" />
                                                    </eo:StaticColumn>


                                                    <eo:StaticColumn HeaderText="" AllowSort="False" 
                                                        DataField="IdProgramaSaude" Name="IdProgramaSaude" ReadOnly="True" 
                                                        SortOrder="Ascending" Text="" Width="0">
                                                        <CellStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500; color: #7D7B7B !important; text-align: left; height: 35px !important; padding: 0.3rem 0.3rem 0.3rem 0.5rem; height: 35px !important;" />
                                                    </eo:StaticColumn>


                                                    <eo:StaticColumn HeaderText="" AllowSort="False" 
                                                        DataField="IdAtendimentoAssistencial" Name="IdAtendimentoAssistencial" ReadOnly="True" 
                                                        SortOrder="Ascending" Text="" Width="0">
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
                                        </div>
                                        <div class="col">
                                            <asp:label id="lblTotRegistros" CssClass="tituloLabel"  runat="server"></asp:label></TD>
                                        </div>
                                    </div>
                                </div>

                                    <div class="col-12 mt-4 gx-4">
                                        <asp:Button ID="cmd_Voltar" onclick="cmd_Voltar_Click"  runat="server" CssClass="btn2" Text="Voltar" Width="132px" />
                                    </div>

			<%--<iframe id="SubDados" name="SubDados" align="middle" marginWidth="0" marginHeight="0" frameBorder="0"
				width="600" scrolling="no" src="../DadosEmpresa/SubDadosNull.aspx" >
			</iframe>--%>
<%--		</form>
	</body>
</HTML>
--%>

                  <asp:TextBox ID="lblreg" runat="server" Width="0" Visible="False" ></asp:TextBox>
                  <asp:TextBox ID="lblnreg" runat="server" Width="0" Visible="False" ></asp:TextBox>
       
           
         <eo:MsgBox ID="MsgBox1" runat="server" BackColor="#47729F" ControlSkinID="None" 
        HeaderHtml="Dialog Title" Height="100px" Width="168px">
        <HeaderStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #ffbf00 3px solid; padding-left: 4px; font-weight: bold; font-size: 11px; padding-bottom: 2px; color: white; padding-top: 2px; border-bottom: #22456a 1px solid; font-family: verdana" />
        <ContentStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #7d97b6 1px solid; padding-left: 4px; border-left-width: 1px; font-size: 11px; border-left-color: #728eb8; padding-bottom: 4px; color: white; padding-top: 4px; border-bottom: #22456a 1px solid; font-family: verdana" />
        <FooterStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #7d97b6 1px solid; padding-left: 4px; border-left-width: 1px; font-size: 11px; border-left-color: #728eb8; padding-bottom: 4px; color: white; padding-top: 4px; border-bottom: #22456a 1px solid; font-family: verdana" />
        </eo:MsgBox>
                        </div>
                    </div>
            </ContentTemplate>

    </asp:UpdatePanel>
  



</asp:Content>
