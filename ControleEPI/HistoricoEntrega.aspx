<%@ Page Language="C#"   AutoEventWireup="True"  MasterPageFile="~/Site.Master" CodeBehind="HistoricoEntrega.aspx.cs"  Inherits="Ilitera.Net.HistoricoEntrega" Title="Ilitera.Net" %>

<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <link href="css/forms.css" rel="stylesheet" type="text/css" />
    <link href="css/tabelas.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
    .defaultFont
    {
        width: 627px;
    }
        .largeboldFont
        {
            font-weight: 700;
        }
        .style1
        {
            height: 44px;
        }
        .style2
        {
            width: 457px;
        }
    </style>
</asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent" >

<script language="javascript" src="scripts/validador.js"></script>
<script type="text/javascript">
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
</script>


    <div class="container-fluid d-flex ms-5 ps-4">
        <div class="row gx-3 gy-2 w-100">


            <div class="col-11 subtituloBG" style="padding-top: 10px;">
                <asp:label id="lblHistoricoEntrega" runat="server" CssClass="subtitulo">Histórico de entrega de EPI ao empregado</asp:label>
            </div>

            <div class="col-11">
                <div class="row">
                    <div class="col-md-5 gx-3 gy-2 mb-3">
                        <asp:label id="lblSelecione" runat="server" CssClass="tituloLabel form-label">Selecione o Empregado</asp:label>
                        <div class="row">
                            <div class="col-11">
                                <asp:dropdownlist id="ddlEmpregado" onselectedindexchanged="ddlEPI_SelectedIndexChanged" runat="server" CssClass="texto form-select form-select-sm" AutoPostBack="True"></asp:dropdownlist>
                            </div>

                            <div class="col-1 gx-3">
                                <asp:ImageButton id="imbRelaTodos" runat="server" ImageUrl="Images/impressora.svg" CssClass="btnMenor" style="padding: .3rem;" ToolTip="Relatório Completo de Todos os Funcionários"></asp:ImageButton>
                                &nbsp;<asp:Button ID="cmd_Relat_Todos_CSV" runat="server" Font-Bold="True" Font-Size="6pt" OnClick="cmd_Relat_Todos_CSV_Click" Text="CSV" />
                            </div>
                        </div>
                    </div>

                    <div class="col-md-7 gx-3 mb-3" style="margin-top: 32px;">
                        <p class="texto">Selecione o Empregado e clique na Data de entrega para visualizar os EPI's fornecidos</p>
                    </div>

                    <div class="col-md-6 gx-3 gy-2 mb-3">
                        <eo:Grid ID="UltraWebGridDataEntrega" runat="server" ClientSideOnItemCommand="OnItemCommand" ColumnHeaderAscImage="00050403" OnItemCommand="UltraWebGridDataEntrega_ItemCommand"  
                                    ColumnHeaderDescImage="00050404" ColumnHeaderDividerOffset="6" ColumnHeaderHeight="30" FixedColumnCount="1"  
                                    GridLines="Both" Height="218px" ItemHeight="30" KeyField="IdEPICAEntrega" Width="318px" ClientSideOnCellSelected="OnCellSelected" CssClass="grid">
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
                                        <eo:StaticColumn AllowSort="True" DataField="IdEPICAEntrega" HeaderText="IdEPICAEntrega" 
                                            Name="IdEPICAEntrega" ReadOnly="True" SortOrder="Ascending" Text="" Width="0">
                                           <CellStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500; color: #7D7B7B !important; text-align: left; height: 35px !important; padding: 0.3rem 0.3rem 0.3rem 0.5rem; height: 35px !important;" />
                                        </eo:StaticColumn>
                                        <eo:StaticColumn AllowSort="True" DataField="DataRecebimento" HeaderText="Datas de Entrega" 
                                            Name="DataRecebimento" ReadOnly="True" Width="160">
                                           <CellStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500; color: #7D7B7B !important; text-align: left; height: 35px !important; padding: 0.3rem 0.3rem 0.3rem 0.5rem; height: 35px !important;" />
                                        </eo:StaticColumn>
                                                                     
                                        <eo:ButtonColumn Name="EPI" ButtonText="EPI" Width="30">
                                        </eo:ButtonColumn>
                                                                     
                                        <eo:ButtonColumn ButtonText="..." Name="Gerar" Width="30">
                                            <ButtonStyle CssText="background-image:url('img/print.gif');" />
                                            <CellStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500; color: #7D7B7B !important; text-align: left; height: 35px !important; padding: 0.3rem 0.3rem 0.3rem 0.5rem; height: 35px !important;" />
                                        </eo:ButtonColumn>
                                                                     
                                        <eo:ButtonColumn ButtonText="Excluir">
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
                                                <MonthStyle 
                                                    CssText="font-family: tahoma; font-size: 12px; margin-left: 14px; cursor: hand; margin-right: 14px" />
                                                <DayHeaderStyle CssText="font-family: tahoma; font-size: 12px; border-bottom: #aca899 1px solid" />
                                                <DayStyle CssText="font-family: tahoma; font-size: 12px; border-right: white 1px solid; border-top: white 1px solid; border-left: white 1px solid; border-bottom: white 1px solid" />
                                                <DayHoverStyle 
                                                    CssText="font-family: tahoma; font-size: 12px; border-right: #fbe694 1px solid; border-top: #fbe694 1px solid; border-left: #fbe694 1px solid; border-bottom: #fbe694 1px solid" />
                                                <TodayStyle 
                                                    CssText="font-family: tahoma; font-size: 12px; border-right: #bb5503 1px solid; border-top: #bb5503 1px solid; border-left: #bb5503 1px solid; border-bottom: #bb5503 1px solid" />
                                                <SelectedDayStyle CssText="font-family: tahoma; font-size: 12px; background-color: #fbe694; border-right: white 1px solid; border-top: white 1px solid; border-left: white 1px solid; border-bottom: white 1px solid" />
                                                <DisabledDayStyle 
                                                    CssText="font-family: tahoma; font-size: 12px; color: gray; border-right: white 1px solid; border-top: white 1px solid; border-left: white 1px solid; border-bottom: white 1px solid" />
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

                    <div class="col-md-6 gx-3 gy-2 mb-3">
                        <div class="row">
                            <div class="col-12 mb-3">
                                <asp:Label id="Label1" runat="server" CssClass="tituloLabel form-label">Data de Entrega</asp:Label>
                            </div>

                            <div class="col-12">
                                <eo:Grid ID="UltraWebGridItemsEntrega" runat="server" ClientSideOnItemCommand="OnItemCommand" ColumnHeaderAscImage="00050403" 
                                    ColumnHeaderDescImage="00050404" olumnHeaderDividerOffset="6" ColumnHeaderHeight="30" FixedColumnCount="1" GridLines="Both" 
                                    Height="139px" ItemHeight="30" KeyField="NOME_EPI" Width="412px" CssClass="grid">
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
                                        <eo:StaticColumn AllowSort="True" DataField="NOME_EPI" HeaderText="EPI" 
                                            Name="NOME_EPI" ReadOnly="True" SortOrder="Ascending" Text="" Width="170">
                                           <CellStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500; color: #7D7B7B !important; text-align: left; height: 35px !important; padding: 0.3rem 0.3rem 0.3rem 0.5rem; height: 35px !important;" />
                                        </eo:StaticColumn>
                                        <eo:StaticColumn AllowSort="True" DataField="CA" HeaderText="CA" 
                                            Name="CA" ReadOnly="True" Width="60">
                                            <CellStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500; color: #7D7B7B !important; text-align: left; height: 35px !important; padding: 0.3rem 0.3rem 0.3rem 0.5rem; height: 35px !important;" />
                                        </eo:StaticColumn>
                                        <eo:StaticColumn AllowSort="True" DataField="QTD_ENTREGUE" HeaderText="Qtde.Entregue" 
                                            Name="QTD_ENTREGUE" ReadOnly="True" Width="90">
                                            <CellStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500; color: #7D7B7B !important; text-align: left; height: 35px !important; padding: 0.3rem 0.3rem 0.3rem 0.5rem; height: 35px !important;" />
                                        </eo:StaticColumn>                                       
                                        <eo:StaticColumn AllowSort="True" DataField="PERIODICIDADE" HeaderText="Periodicidade" 
                                            Name="PERIODICIDADE" ReadOnly="True" Width="90">
                                            <CellStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500; color: #7D7B7B !important; text-align: left; height: 35px !important; padding: 0.3rem 0.3rem 0.3rem 0.5rem; height: 35px !important;" />
                                        </eo:StaticColumn>    
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
                                                <MonthStyle 
                                                    CssText="font-family: tahoma; font-size: 12px; margin-left: 14px; cursor: hand; margin-right: 14px" />
                                                <DayHeaderStyle CssText="font-family: tahoma; font-size: 12px; border-bottom: #aca899 1px solid" />
                                                <DayStyle CssText="font-family: tahoma; font-size: 12px; border-right: white 1px solid; border-top: white 1px solid; border-left: white 1px solid; border-bottom: white 1px solid" />
                                                <DayHoverStyle 
                                                    CssText="font-family: tahoma; font-size: 12px; border-right: #fbe694 1px solid; border-top: #fbe694 1px solid; border-left: #fbe694 1px solid; border-bottom: #fbe694 1px solid" />
                                                <TodayStyle 
                                                    CssText="font-family: tahoma; font-size: 12px; border-right: #bb5503 1px solid; border-top: #bb5503 1px solid; border-left: #bb5503 1px solid; border-bottom: #bb5503 1px solid" />
                                                <SelectedDayStyle CssText="font-family: tahoma; font-size: 12px; background-color: #fbe694; border-right: white 1px solid; border-top: white 1px solid; border-left: white 1px solid; border-bottom: white 1px solid" />
                                                <DisabledDayStyle 
                                                    CssText="font-family: tahoma; font-size: 12px; color: gray; border-right: white 1px solid; border-top: white 1px solid; border-left: white 1px solid; border-bottom: white 1px solid" />
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
                        </div>
                    </div>

                    <div class="col-12 gx-3 gy-2">
                        <div class="row">
                            <div class="col-md-3 ms-4">
                                <asp:CheckBox ID="chk_Todos" oncheckedchanged="chk_Todos_CheckedChanged" runat="server" Text="Período completo ( Todos )" AutoPostBack="True" CssClass="texto form-check-input bg-transparent border-0" />
                            </div>

                            <div class="col-md-3 gx-3">
                                <div class="row">
                                    <div class="col-7">
                                        <p class="tituloLabel">Relatório Completo</p>
                                    </div>

                                    <div class="col-1 gx-3">
                                        <asp:ImageButton ID="imbRelaComp" runat="server"  ImageUrl="Images/impressora.svg" CssClass="btnMenor" style="padding: .15rem; height: 20px !important;" ToolTip="Relatório Completo de Todos os Funcionários" />
                                    </div>
                                </div> 
                            </div>

                            <div class="col-md-3 gx-3">
                                <div class="row">
                                    <div class="col-8">
                                        <p class="tituloLabel">Recibo de Fornecimento</p>
                                    </div>

                                    <div class="col-1 gx-3">
                                        <asp:ImageButton ID="imbRelaRec" runat="server" ImageUrl="Images/impressora.svg" CssClass="btnMenor" style="padding: .15rem; height: 20px !important;" ToolTip="Relatório Completo de Todos os Funcionários" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="col-12 mt-3">
                        <div class="row">
                            <div class="col-md-3 gx-3">
                                <div class="row">
                                    <div class="col-12">
                                        <p class="tituloLabel">Data Inicial</p>
                                    </div>

                                    <div class="col-12">
                                        <asp:Calendar ID="dt_1"  runat="server" CellPadding="4" DayNameFormat="Shortest" 
                                            Height="155px" Width="174px" CssClass="mt-4 calendar" BorderColor="Transparent" BorderWidth="0">
                                            <DayHeaderStyle BackColor="#B0ABAB" Font-Bold="True" CssClass="calendarTitulo" />
                                            <NextPrevStyle VerticalAlign="Bottom" />
                                            <OtherMonthDayStyle ForeColor="#89898B" />
                                            <SelectedDayStyle BackColor="#666666" Font-Bold="True" ForeColor="White" />
                                            <SelectorStyle BackColor="#CCCCCC" />
                                            <TitleStyle BackColor="#D6D6D6" CssClass="calendarTitulo"  />
                                            <TodayDayStyle BackColor="#CCCCCC" ForeColor="Black" />
                                            <WeekendDayStyle />
                                        </asp:Calendar>
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-3 gx-3">
                                <div class="row">
                                    <div class="col-12">
                                        <p class="tituloLabel">Data Final</p>
                                    </div>

                                    <div class="col-12">
                                        <asp:Calendar ID="dt_2" runat="server" CellPadding="4" DayNameFormat="Shortest" 
                                            Height="155px" Width="174px" CssClass="mt-4 calendar" BorderColor="Transparent" BorderWidth="0">
                                            <DayHeaderStyle BackColor="#B0ABAB" Font-Bold="True" CssClass="calendarTitulo" />
                                            <NextPrevStyle VerticalAlign="Bottom" />
                                            <OtherMonthDayStyle ForeColor="#89898B" />
                                            <SelectedDayStyle BackColor="#666666" Font-Bold="True" ForeColor="White" />
                                            <SelectorStyle BackColor="#CCCCCC" />
                                            <TitleStyle BackColor="#D6D6D6" CssClass="calendarTitulo"  />
                                            <TodayDayStyle BackColor="#CCCCCC" ForeColor="Black" />
                                            <WeekendDayStyle />
                                        </asp:Calendar>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            






<%--
	<body bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0">
		<form method="post" runat="server" id="frmHistoricoEntrega">
--%>			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="820"  align="center" border="0">
				<TR>
					<TD class="normalFont" align="center"><BR>
						</TD>
				</TR>
				<TR>
					<TD class="normalFont" align="center"><BR>
						&nbsp;&nbsp;</TD>
				</TR>
				<TR>
					<TD class="normalFont" align="center"><BR>
						<TABLE class="normalFont" id="Table2" cellSpacing="0" cellPadding="0" align="center"
							border="0">
							<TR>
								<TD width="174" align="center" vAlign="top">

                                
                                <%--JavaScriptFileName="ig_WebGrid.js" AutoGenerateColumns="False" JavaScriptFileNameCommon="ig_shared.js"  GridLinesDefault="None"--%>
								



                                

                                
                                

									<asp:label id="lblTotRegistrosDatas" runat="server" CssClass="normalFont"></asp:label><BR>
									    <b>
                                        <br />
                                        <asp:Button ID="cmd_Excluir" runat="server" BackColor="#FF8080" onclick="cmd_Excluir_Click" 
                                            CssClass="buttonFoto" Font-Size="XX-Small" Text="Excluir" Width="120px" Visible="False" />
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lbl_ID" runat="server" Text="0" Visible="False"></asp:Label>
					                &nbsp;&nbsp;</b></TD>
								<TD align="center" vAlign="top" class="style2"><U><BR>
										<BR>
									</U>
									<TABLE class="normalFont" id="Table3" cellSpacing="0" cellPadding="0" width="300" align="center"
										border="0">
										<TR>
											<TD align="right" width="147">
												</TD>
											<TD width="5"></TD>
											<TD width="148">
												<asp:Label id="lblValorData" runat="server" CssClass="boldFont"></asp:Label></TD>
										</TR>
									</TABLE>
									<BR>

                                    


<%--									<igtbl:ultrawebgrid id="UltraWebGridItemsEntrega" runat="server" Height="155px" 
                                            Width="412px">
										<DisplayLayout JavaScriptFileName="ig_WebGrid.js" AutoGenerateColumns="False" JavaScriptFileNameCommon="ig_shared.js"
											RowHeightDefault="18px" Version="3.00" GridLinesDefault="None" ViewType="OutlookGroupBy" RowSelectorsDefault="No"
											Name="UltraWebGridItemsEntrega" TableLayout="Fixed">
											<AddNewBox>
												<BoxStyle BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
                                                    <BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White">
                                                    </BorderDetails>
												</BoxStyle>
											</AddNewBox>
											<Pager PrevText="Anterior" NextText="Pr&#243;ximo" QuickPages="10" PageSize="6" StyleMode="QuickPages"
												Alignment="Center" AllowPaging="True">
												<PagerStyle BorderWidth="0px" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True" BorderStyle="Solid"
													ForeColor="#004000" BackColor="#A3E0AA" Height="18px">
												</PagerStyle>
											</Pager>
											<HeaderStyleDefault Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True" HorizontalAlign="Center"
												ForeColor="White" BackColor="#004000" Height="18px"></HeaderStyleDefault>
											<GroupByRowStyleDefault Height="18px"></GroupByRowStyleDefault>
											<FrameStyle Width="412px" BorderWidth="1px" BorderColor="#004000" 
                                                BorderStyle="Solid" Height="155px"></FrameStyle>
											<FooterStyleDefault BorderWidth="0px" BorderStyle="Solid" BackColor="LightGray"></FooterStyleDefault>
											<ActivationObject AllowActivation="False" BorderColor="124, 197, 161"></ActivationObject>
											<GroupByBox Hidden="True"></GroupByBox>
											<EditCellStyleDefault BorderWidth="0px" BorderStyle="None"></EditCellStyleDefault>
											<images imagedirectory="">
                                            </images>
											<RowAlternateStyleDefault BackColor="#DFFFDF"></RowAlternateStyleDefault>
											<RowStyleDefault BorderWidth="1px" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True" BorderColor="#004000"
												BorderStyle="Solid" HorizontalAlign="Center" ForeColor="#004000"></RowStyleDefault>
											<ImageUrls ImageDirectory=""></ImageUrls>
										</DisplayLayout>
										<Bands>
											<igtbl:UltraGridBand>
												<Columns>
													<igtbl:UltraGridColumn HeaderText="EPI" Key="NOME_EPI" Width="170px" AllowGroupBy="No" BaseColumnName="NOME_EPI">
														<Footer Key="NOME_EPI"></Footer>
														<Header Key="NOME_EPI" Caption="EPI"></Header>
													</igtbl:UltraGridColumn>
													<igtbl:UltraGridColumn HeaderText="CA" Key="CA" EditorControlID="" Width="60px" AllowGroupBy="No" Format=""
														BaseColumnName="CA" FooterText="">
														<CellButtonStyle Cursor="Hand" BorderWidth="0px" Font-Size="XX-Small" Font-Underline="True" Font-Names="Verdana"
															Font-Bold="True" ForeColor="#004000" BackColor="#F7FFF7"></CellButtonStyle>
														<Footer Key="CA" Caption="">
                                                            <rowlayoutcolumninfo originx="1" />
                                                        </Footer>
														<Header Key="CA" Caption="CA">
                                                            <rowlayoutcolumninfo originx="1" />
                                                        </Header>
													</igtbl:UltraGridColumn>
													<igtbl:UltraGridColumn HeaderText="Qtd. Entregue" Key="QTD_ENTREGUE" Width="90px" AllowGroupBy="No" BaseColumnName="QTD_ENTREGUE">
														<Footer Key="QTD_ENTREGUE">
                                                            <rowlayoutcolumninfo originx="2" />
                                                        </Footer>
														<Header Key="QTD_ENTREGUE" Caption="Qtd. Entregue">
                                                            <rowlayoutcolumninfo originx="2" />
                                                        </Header>
													</igtbl:UltraGridColumn>
													<igtbl:UltraGridColumn HeaderText="Periodicidade" Key="PERIODICIDADE" Width="90px" AllowGroupBy="No" BaseColumnName="PERIODICIDADE">
														<Footer Key="PERIODICIDADE">
                                                            <rowlayoutcolumninfo originx="3" />
                                                        </Footer>
														<Header Key="PERIODICIDADE" Caption="Periodicidade">
                                                            <rowlayoutcolumninfo originx="3" />
                                                        </Header>
													</igtbl:UltraGridColumn>
												</Columns>
											    <addnewrow view="NotSet" visible="NotSet">
                                                </addnewrow>
											</igtbl:UltraGridBand>
										</Bands>
									</igtbl:ultrawebgrid>
--%>


                                




									<asp:label id="lblTotRegistrosItems" runat="server" CssClass="normalFont"></asp:label>

                                    <asp:Label ID="lbl_Id_Empresa" runat="server" Text="IdEmpresa" Visible="False"></asp:Label>
                                    &nbsp;&nbsp;
                                    <asp:Label ID="lbl_Id_Usuario" runat="server" Text="IdUsuario" Visible="False"></asp:Label>

								        <b>
                                        <br />
                                        </b>

								</TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
<%--		</form>
	</body>
</HTML>
--%>
      

 
  <eo:MsgBox ID="MsgBox1" runat="server" BackColor="#47729F" ControlSkinID="None" 
        HeaderHtml="Dialog Title" Height="100px" Width="168px" OnButtonClick="MsgBox1_ButtonClick">
        <HeaderStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #ffbf00 3px solid; padding-left: 4px; font-weight: bold; font-size: 11px; padding-bottom: 2px; color: white; padding-top: 2px; border-bottom: #22456a 1px solid; font-family: verdana" />
        <ContentStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #7d97b6 1px solid; padding-left: 4px; border-left-width: 1px; font-size: 11px; border-left-color: #728eb8; padding-bottom: 4px; color: white; padding-top: 4px; border-bottom: #22456a 1px solid; font-family: verdana" />
        <FooterStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #7d97b6 1px solid; padding-left: 4px; border-left-width: 1px; font-size: 11px; border-left-color: #728eb8; padding-bottom: 4px; color: white; padding-top: 4px; border-bottom: #22456a 1px solid; font-family: verdana" />
    </eo:MsgBox>

        </div>
    </div>
</asp:Content>