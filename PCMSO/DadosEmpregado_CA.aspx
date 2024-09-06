<%@ Page Title="Ilitera.Net" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="True"
    CodeBehind="DadosEmpregado_CA.aspx.cs" Inherits="Ilitera.Net.DadosEmpregado_CA" %>

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
					<td nowrap align="center">
						<table class="fotoborder" bgcolor="#edffeb" id="Table2" cellspacing="0" 
                            cellpadding="0" align="center"
							border="0">
							<tr> <!-- celula esquerda-->
								<td class="normalFont" width="490">
									<table id="Table3" cellSpacing="0" cellPadding="0" width="490" border="0">
										<tr>
											<td vAlign="middle" align="left" width="7" bgcolor="#edffeb">&nbsp;</TD>
											<td class="tableEdit" valign="middle" align="right" width="7" bgcolor="#edffeb">&nbsp;</TD>
											<td class="textDadosOpcao" valign="middle" align="left" width="31" bgcolor="#edffeb">
                                                <asp:label id="lblNome" runat="server" BackColor="#EDFFEB" Font-Bold="True">Nome&nbsp;</asp:label></TD>
											<td valign="middle" align="left" width="445" bgcolor="#edffeb">
												<table id="Table4" cellSpacing="0" cellPadding="0" width="444" border="0">
													<tr>
														<td class="textDadosNome">
                                                            <asp:label id="lblValorNome" runat="server" 
                                                                Font-Bold="True" BackColor="#edffeb"></asp:label></TD>
													</tr>
												</TABLE>
											</TD>
										</TR>
									</TABLE>
									<TABLE id="Table5" cellSpacing="0" cellPadding="0" width="490" border="0">
										<TR>
											<TD class="tableSpace"></TD>
										</TR>
									</TABLE>
									<TABLE class="tableEdit" id="Table6" cellSpacing="0" cellPadding="0" width="490" border="0">
										<TR class="tableEdit" vAlign="middle">
											<TD class="tableEdit" vAlign="middle" align="left" width="7" bgColor="#edffeb">&nbsp;</TD>
											<TD class="tableEdit" vAlign="middle" align="right" width="7" bgColor="#edffeb">&nbsp;</TD>
											<TD class="textDadosOpcao" align="left" width="105" bgColor="#edffeb">
                                                <asp:label id="lblTipoBene" runat="server" CssClass="textDadosOpcao" 
                                                    BackColor="#EDFFEB" Font-Bold="True">Tipo&nbsp;de&nbsp;Beneficiário&nbsp;</asp:label></TD>
											<TD align="left" width="56" bgColor="#edffeb">
												<TABLE id="Table7" cellSpacing="0" cellPadding="0" width="30" border="0">
													<TR>
														<TD class="textDadosCampo" bgColor="#edffeb"><asp:label id="lblValorBene" 
                                                                runat="server" BackColor="#EDFFEB"></asp:label></TD>
													</TR>
												</TABLE>
											    </TD>
											<TD class="textDadosOpcao" align="left" width="64" bgColor="#edffeb">
                                                &nbsp; 
                                                <asp:label id="lblDataNascimento" runat="server" CssClass="textDadosOpcao" 
                                                    BackColor="#EDFFEB" Font-Bold="True">Nascimento&nbsp;</asp:label>&nbsp; </TD>
											<TD align="left" width="98" bgColor="#edffeb">
												<TABLE id="Table8" cellSpacing="0" cellPadding="0" width="76" border="0">
													<TR>
														<TD class="textDadosCampo" width="97" bgColor="#edffeb">
                                                            <asp:label id="lblValorNasc" runat="server" BackColor="#EDFFEB"></asp:label></TD>
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
									<TABLE id="Table11" cellSpacing="0" cellPadding="0" width="490" border="0">
										<TR>
											<TD class="tableSpace"></TD>
										</TR>
									</TABLE>
									<TABLE id="Table12" cellSpacing="0" cellPadding="0" width="490" border="0">
										<TR vAlign="middle">
											<TD vAlign="middle" align="left" width="7" bgColor="#edffeb">&nbsp;</TD>
											<TD class="textDadosOpcao" vAlign="middle" align="right" width="7" bgColor="#edffeb">&nbsp;</TD>
											<TD class="textDadosOpcao" align="left" width="19" bgColor="#edffeb">
                                                <asp:label id="lblRegistro" runat="server" CssClass="textDadosOpcao" 
                                                    BackColor="#EDFFEB" Font-Bold="True">RE&nbsp;</asp:label></TD>
											<TD align="left" width="100" bgColor="#edffeb">
												<TABLE id="Table13" cellSpacing="0" cellPadding="0" border="0">
													<TR>
														<TD class="textDadosCampo" width="85" bgColor="#edffeb">
                                                            <asp:label id="lblValorRegistro" runat="server" BackColor="#EDFFEB"></asp:label></TD>
													</TR>
												</TABLE>
											    </TD>
											<TD class="textDadosOpcao" align="left" width="111" bgColor="#edffeb">&nbsp;<ASP:LABEL 
                                                    id="lblRegRev" runat="server" CssClass="textDadosOpcao" Font-Bold="True" 
                                                    BackColor="#edffeb">Regime&nbsp;Revezamento&nbsp;</ASP:LABEL></TD>
											<TD align="left" width="123" bgColor="#edffeb">
												<TABLE id="Table14" cellSpacing="0" cellPadding="0" width="75" border="0">
													<TR>
														<TD class="textDadosCampo" width="61" bgColor="#edffeb">
                                                            <asp:label id="lblValorRegRev" runat="server" Width="93px" BackColor="#EDFFEB"></asp:label></TD>
													</TR>
												</TABLE>
											</TD>
											<TD class="textDadosOpcao" align="left" width="45" bgColor="#edffeb">
                                                &nbsp;&nbsp;
                                                <asp:label id="lblJornada" runat="server" CssClass="textDadosOpcao" 
                                                    BackColor="#EDFFEB" Font-Bold="True">Jornada&nbsp;</asp:label></TD>
											<TD align="left" width="78" bgColor="#edffeb">
												<TABLE id="Table15" cellSpacing="0" cellPadding="0" width="77" border="0">
													<TR>
														<TD class="textDadosCampo" width="75" bgColor="#edffeb">
                                                            <asp:label id="lblValorJornada" runat="server" BackColor="#EDFFEB"></asp:label></TD>
													</TR>
												</TABLE>
											</TD>
										</TR>
									</TABLE>
									<TABLE id="Table16" cellSpacing="0" cellPadding="0" width="490" border="0">
										<TR>
											<TD class="tableSpace"></TD>
										</TR>
									</TABLE>
									<TABLE id="Table17" cellSpacing="0" cellPadding="0" width="490" border="0">
										<TR vAlign="middle">
											<TD vAlign="middle" align="left" width="7" bgColor="#edffeb">&nbsp;</TD>
											<TD class="textDadosOpcao" vAlign="middle" align="right" width="7" bgColor="#edffeb">&nbsp;</TD>
											<TD class="textDadosOpcao" align="left" width="51" bgColor="#edffeb">
                                                <asp:label id="lblAdmissao" runat="server" CssClass="textDadosOpcao" 
                                                    BackColor="#EDFFEB" Font-Bold="True">Admissão&nbsp;</asp:label></TD>
											<TD align="left" width="109" bgColor="#edffeb">
												<TABLE id="Table18" cellSpacing="0" cellPadding="0" width="75" border="0">
													<TR>
														<TD class="textDadosCampo" width="75" bgColor="#edffeb">
                                                            <asp:label id="lblValorAdmissao" runat="server" BackColor="#EDFFEB"></asp:label></TD>
													</TR>
												</TABLE>
											</TD>
											<TD class="textDadosOpcao" vAlign="bottom" align="left" width="50" bgColor="#edffeb">
                                                &nbsp;&nbsp;<asp:label id="lblDemissao" runat="server" CssClass="textDadosOpcao" 
                                                    BackColor="#EDFFEB" Font-Bold="True">Demissão&nbsp;</asp:label></TD>
											<TD align="left" width="121" bgColor="#edffeb">
												<TABLE id="Table19" cellSpacing="0" cellPadding="0" width="75" border="0">
													<TR>
														<TD class="textDadosCampo" width="75" bgColor="#edffeb">
                                                            <asp:label id="lblValorDemissao" runat="server" BackColor="#EDFFEB"></asp:label></TD>
													</TR>
												</TABLE>
											</TD>
											<TD class="textDadosOpcao" align="left" width="69" bgColor="#edffeb">
                                                &nbsp;
                                                <asp:label id="lblDataIni" runat="server" CssClass="textDadosOpcao" 
                                                    BackColor="#EDFFEB" Font-Bold="True">Início&nbsp;Função&nbsp;</asp:label></TD>
											<TD align="left" width="76" bgColor="#edffeb">
												<TABLE id="Table20" cellSpacing="0" cellPadding="0" width="75" border="0">
													<TR>
														<TD class="textDadosCampo" bgColor="#edffeb"><asp:label id="lblValorDataIni" 
                                                                runat="server" BackColor="#EDFFEB"></asp:label></TD>
													</TR>
												</TABLE>
											</TD>
										</TR>
									</TABLE>
									<TABLE id="Table21" cellSpacing="0" cellPadding="0" width="490" border="0">
										<TR>
											<TD class="tableSpace"></TD>
										</TR>
									</TABLE>
									<TABLE id="Table22" cellSpacing="0" cellPadding="0" width="490" border="0">
										<TR vAlign="middle">
											<TD vAlign="middle" align="left" width="7" bgColor="#edffeb">&nbsp;</TD>
											<TD class="textDadosOpcao" vAlign="middle" align="right" width="7" bgColor="#edffeb">&nbsp;</TD>
											<TD class="textDadosOpcao" align="left" width="95" bgColor="#edffeb">
                                                <asp:label id="lblGHE" runat="server" CssClass="textDadosOpcao" 
                                                    BackColor="#EDFFEB" Font-Bold="True">Tempo&nbsp;de&nbsp;Empresa</asp:label></TD>
											<TD align="left" width="100" bgColor="#edffeb">
												<TABLE id="Table23" cellSpacing="0" cellPadding="0" width="85" border="0">
													<TR>
														<TD class="textDadosCampo" width="85" bgColor="#edffeb">
                                                            <asp:label id="lblValorTempoEmpresa" runat="server" BackColor="#EDFFEB"></asp:label></TD>
													</TR>
												</TABLE>
											</TD>
											<TD class="textDadosOpcao" align="left" width="33" bgColor="#edffeb">
                                                <asp:label id="lblSetor" runat="server" CssClass="textDadosOpcao" 
                                                    BackColor="#EDFFEB" Font-Bold="True">Setor&nbsp;</asp:label></TD>
											<TD align="left" width="248" bgColor="#edffeb">
												<TABLE id="Table24" cellSpacing="0" cellPadding="0" width="247" border="0">
													<TR>
														<TD class="textDadosCampo" bgColor="#edffeb" width="247">
                                                            <asp:label id="lblValorSetor" runat="server" BackColor="#EDFFEB"></asp:label></TD>
													</TR>
												</TABLE>
											</TD>
										</TR>
									</TABLE>
									<TABLE id="Table25" cellSpacing="0" cellPadding="0" width="490" border="0">
										<TR>
											<TD class="tableSpace"></TD>
										</TR>
									</TABLE>
									<TABLE id="Table26" cellSpacing="0" cellPadding="0" width="490" border="0" >
										<TR>
											<TD vAlign="middle" align="left" width="7" bgColor="#edffeb">&nbsp;</TD>
											<TD class="textDadosOpcao" vAlign="middle" align="right" width="7" bgColor="#edffeb">&nbsp;</TD>
											<TD class="textDadosOpcao" align="left" width="38" bgColor="#edffeb">
                                                <asp:label id="lblFuncao" runat="server" CssClass="textDadosOpcao" 
                                                    BackColor="#EDFFEB" Font-Bold="True">Função&nbsp;</asp:label></TD>
											<TD align="left" width="438" bgColor="#edffeb">
												<TABLE id="Table27" cellSpacing="0" cellPadding="0" width="437" border="0">
													<TR>
														<TD class="textDadosOpcao" width="437" bgColor="#edffeb">
                                                            <asp:label id="lblValorFuncao" runat="server" BackColor="#EDFFEB"></asp:label></TD>
													</TR>
												</TABLE>
											</TD>
										</TR>
									</TABLE>
								</TD> <!-- celula direita-->
								<TD class="normalFont" align="center" width="110">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                                    </TD>

                                        
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
					<TD noWrap align="center"><BR>
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
                GridLines="Both" Height="104px" Width="800px" ColumnHeaderDividerOffset="6" 
                ColumnHeaderHeight="18" ItemHeight="22" KeyField="IdEmpregado"  
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
               
                <eo:StaticColumn HeaderText="Frequência" AllowSort="True" 
                    DataField="Frequencia" Name="Frequencia" ReadOnly="True" 
                    SortOrder="Ascending" Text="" Width="90" >
                    <CellStyle CssText="background-color:#ffffcc;font-family:Tahoma;font-weight:bold;text-align:left" />
                </eo:StaticColumn>
                <eo:StaticColumn HeaderText="CAs" AllowSort="True" 
                    DataField="CA" Name="CA" ReadOnly="True" 
                    Width="85">
                    <CellStyle CssText="text-align:center" />
                </eo:StaticColumn>           
                <eo:StaticColumn HeaderText="EPI" AllowSort="True" 
                    DataField="EPI" Name="EPI" ReadOnly="True" 
                    Width="120">
                    <CellStyle CssText="text-align:center" />
                </eo:StaticColumn>           
                <eo:StaticColumn HeaderText="Data Entrega" AllowSort="True" 
                    DataField="DataEntrega" Name="DataEntrega" ReadOnly="True" 
                    Width="90">
                    <CellStyle CssText="text-align:center" />
                </eo:StaticColumn>           
                <eo:StaticColumn HeaderText="Descrição" AllowSort="True" 
                    DataField="Descricao" Name="Descricao" ReadOnly="True" 
                    Width="120">
                    <CellStyle CssText="text-align:center" />
                </eo:StaticColumn>
                <eo:StaticColumn HeaderText="Tipo" AllowSort="True" 
                    DataField="Tipo" Name="Tipo" ReadOnly="True" 
                    Width="80">
                    <CellStyle CssText="text-align:center" />
                </eo:StaticColumn>    
                <eo:StaticColumn HeaderText="Tamanho" AllowSort="True" 
                    DataField="Tamanho" Name="Tamanho" ReadOnly="True" 
                    Width="75">
                    <CellStyle CssText="text-align:center" />
                </eo:StaticColumn>     
                <eo:StaticColumn HeaderText="Conjunto" AllowSort="True" 
                    DataField="Conjunto" Name="Conjunto" ReadOnly="True" 
                    Width="75">
                    <CellStyle CssText="text-align:center" />
                </eo:StaticColumn>           
                <eo:StaticColumn HeaderText="Motivo" AllowSort="True" 
                    DataField="Motivo" Name="Motivo" ReadOnly="True" 
                    Width="50">
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
            
            <table class="normalFont" id="Table1" cellspacing="0" cellpadding="0" 
        align="center" border="0"  >

            <tr>
            
                <td>
                    <hr />
                </td>
                <tr>
                    <td>
                        <br />

                        <asp:RadioButton ID="opt_Habitual" runat="server" AutoPostBack="True" 
                            Checked="True" GroupName="1" Text="Habitual" />

                        <b><span class="style2">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:RadioButton ID="opt_Eventual" 
                            runat="server" AutoPostBack="True" Font-Bold="False" GroupName="1" 
                            Text="Eventual" />
                        <br />
                        <br />
                        &nbsp;&nbsp;Data do Laudo&nbsp; :</span><span class="style1">&nbsp;
                        </span></b>
                        <asp:DropDownList ID="cmb_Laudo" runat="server" Font-Size="Small" 
                            Width="200px" AutoPostBack="True" 
                            onselectedindexchanged="cmb_Laudo_SelectedIndexChanged" 
                            CssClass="style1">
                        </asp:DropDownList>
                        <br class="style1" />
                        <br />
                        <b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; EPI&#39;s :&nbsp;
                        <asp:DropDownList ID="cmb_EPI" runat="server" AutoPostBack="True" 
                            CssClass="style1" Font-Size="X-Small" 
                            onselectedindexchanged="cmb_EPI_SelectedIndexChanged" Width="400px">
                        </asp:DropDownList>
                        <asp:DropDownList ID="cmb_EPI2" runat="server" AutoPostBack="True" 
                            CssClass="style1" Font-Size="X-Small" 
                            onselectedindexchanged="cmb_EPI_SelectedIndexChanged" Width="400px">
                        </asp:DropDownList>
                        </b>
                        <br />
                        <br />
                    </td>
                </tr>
            

            

                <tr>
                    <td>
                        <br />
                        <b>CA:&nbsp; 
                        <asp:TextBox ID="txt_CA" runat="server" Width="162px" MaxLength="20"></asp:TextBox>

                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Data de Entrega:&nbsp;
                        <asp:TextBox ID="txt_Data" runat="server" Width="87px" MaxLength="10"></asp:TextBox>
                        <br />
                                                         

                        </td>
                </tr>


                <tr>
                    <td>
                        <br />
                        <b>Descrição:&nbsp; 
                        <asp:TextBox ID="txt_Descricao" runat="server" Width="319px" MaxLength="150"></asp:TextBox>

                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Tipo :

                        <asp:TextBox ID="txt_Tipo" runat="server" Width="130px" MaxLength="50"></asp:TextBox>

                        
                                                         
                        </td>
                </tr>



                <tr>
                    <td>
                        <br />
                        <b>Conjunto:&nbsp; 
                
                        <asp:TextBox ID="txt_Conjunto" runat="server" Width="220px" MaxLength="50"></asp:TextBox>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Tamanho:&nbsp;
                        <asp:TextBox ID="txt_Tamanho" runat="server" Width="264px" MaxLength="30"></asp:TextBox>
                        <br />
                                                         
                        </td>
                </tr>


                <tr>
                    <td>
                        <br />
                        <b>Motivo:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                        <asp:RadioButton ID="rd_Admissao" Text="Admissão" runat="server" GroupName="Tipo"/>                        
                        &nbsp;&nbsp;&nbsp;                        
                        <asp:RadioButton ID="rd_Substituicao" Text="Substituição" Checked="true" runat="server" GroupName="Tipo"/>
                        &nbsp;&nbsp;
                        <asp:RadioButton ID="rd_Mudanca" Text="Mudança de Operação" runat="server" GroupName="Tipo"/>
                        &nbsp;&nbsp;
                        <asp:RadioButton ID="rd_Perda" Text="Perda" runat="server" GroupName="Tipo"/>
                        &nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:RadioButton ID="rd_Quebrado" Text="Quebrado" runat="server" GroupName="Tipo"/>
                        &nbsp;&nbsp;&nbsp;
                        <asp:RadioButton ID="rd_Devolucao" Text="Devolução"  runat="server" GroupName="Tipo" />
                        <br />
                    </td>
                </tr>


                <tr>
                <td>
                <b>
                    <br />
                    &nbsp;
                    <asp:Button ID="btnSalvar" runat="server" onclick="btnSalvar_Click" 
                        Text="Inserir Dados" Width="132px">
                    </asp:Button>
                    <br />
                    </b>
                </td>
                
                </tr>


            </tr>
            
            </table>

                    
         <eo:MsgBox ID="MsgBox1" runat="server" BackColor="#47729F" ControlSkinID="None" 
        HeaderHtml="Dialog Title" Height="100px" Width="168px">
        <HeaderStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #ffbf00 3px solid; padding-left: 4px; font-weight: bold; font-size: 11px; padding-bottom: 2px; color: white; padding-top: 2px; border-bottom: #22456a 1px solid; font-family: verdana" />
        <ContentStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #7d97b6 1px solid; padding-left: 4px; border-left-width: 1px; font-size: 11px; border-left-color: #728eb8; padding-bottom: 4px; color: white; padding-top: 4px; border-bottom: #22456a 1px solid; font-family: verdana" />
        <FooterStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #7d97b6 1px solid; padding-left: 4px; border-left-width: 1px; font-size: 11px; border-left-color: #728eb8; padding-bottom: 4px; color: white; padding-top: 4px; border-bottom: #22456a 1px solid; font-family: verdana" />
        </eo:MsgBox>


    </asp:Content>