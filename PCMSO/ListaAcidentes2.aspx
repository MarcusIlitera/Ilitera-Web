<%@ Page Language="C#"   AutoEventWireup="True"  MasterPageFile="~/Site.Master" CodeBehind="ListaAcidentes2.aspx.cs"  Inherits="Ilitera.Net.ListaAcidentes2" Title="Ilitera.Net" %>

<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <link href="css/forms.css" rel="stylesheet" type="text/css" />
    <link href="css/tabelas.css" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div class="container-fluid d-flex ms-5 ps-4">
        <div class="row gx-3 gy-2 w-100">

<script language="javascript" src="scripts/validador.js"></script>
<script language="javascript">
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
	
    <asp:ScriptManager ID="ScriptManager1" runat="server">

    </asp:ScriptManager>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>

            <div class="col-11 subtituloBG mb-3" style="padding-top: 10px;">
                <asp:label id="lblExCli" runat="server" CssClass="tituloLabel"></asp:label>
            </div>

                
                <div class="col-11 gy-2 mb-3">
              <eo:Grid ID="UltraWebGridExameClinico" runat="server" ColumnHeaderAscImage="00050403" 
                            ColumnHeaderDescImage="00050404" FixedColumnCount="0" GridLines="Both" Height="500px" 
                         Width="1100px"
                            ColumnHeaderHeight="30" ItemHeight="30" KeyField="IdAcidente"  
                    OnItemCommand="UltraWebGridExameClinico_ItemCommand"  
                    ClientSideOnCellSelected="OnCellSelected"
                    ClientSideOnItemCommand="OnItemCommand" FullRowMode="False" >
                        <itemstyles>
                            <eo:GridItemStyleSet>
                              <ItemStyle CssText="background-color: #FAFAFA;" />
                                 <ItemHoverStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500;  text-align: left; height: 30px !important; background: rgba(28, 148, 137, 0.80); color: #FFFFFF; padding: 0.3rem 0.3rem 0.3rem 0.5rem; " />
                                 <AlternatingItemStyle CssText="background-color: #F2F2F2;" />
                                 <AlternatingItemHoverStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500;  text-align: left; height: 30px !important; background: rgba(28, 148, 137, 0.80); color: #FFFFFF; padding: 0.3rem 0.3rem 0.3rem 0.5rem;" />
                                 <SelectedStyle CssText="border-bottom-style:solid;border-left-style:solid;border-right-style:solid;border-top-style:solid;" />
                                 <CellStyle CssText="padding: 0.3rem 0.3rem 0.3rem 0.5rem; height: 30px !important;" />
                              </eo:GridItemStyleSet>
                        </itemstyles>
 

                    <ColumnHeaderStyle CssClass="tabelaC colunas" />
                        <Columns>
                            <eo:StaticColumn HeaderText="Data do Acidente" AllowSort="True" 
                                DataField="DataAcidente" Name="DataAcidente" ReadOnly="True" 
                                SortOrder="Ascending" Text="" Width="130">
                                <CellStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: normal; color: #7D7B7B !important; text-align: left; height: 30px !important; " />
                            </eo:StaticColumn>
                            <eo:StaticColumn HeaderText="Descricao" AllowSort="True"  
                                DataField="Descricao" Name="Descricao" ReadOnly="True" 
                                Width="220">
                                <CellStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: normal; color: #7D7B7B !important; text-align: left; height: 30px !important; " />                   
                            </eo:StaticColumn>

                            <eo:StaticColumn HeaderText="Agente Causador" AllowSort="True" 
                                DataField="AgenteCausador" Name="AgenteCausador" ReadOnly="True" 
                                Width="160">
                                <CellStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: normal; color: #7D7B7B !important; text-align: left; height: 30px !important; " />
                            </eo:StaticColumn>
                
                            <eo:CheckBoxColumn HeaderText="Reabertura" AllowSort="True" 
                                DataField="Reabertura" Name="Reabertura" ReadOnly="True" 
                                Width="100">
                                <CellStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: normal; color: #7D7B7B !important; text-align: left; height: 30px !important; " />
                            </eo:CheckBoxColumn>
  
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

                <asp:label id="lblTotRegistros" runat="server"></asp:label>

               <div class="col-11">
                    <div class="text-center mb-3">
                        <asp:HyperLink ID="hlkNovo" runat="server" SkinID="BoldLink" CssClass="btn" style="color: white !important">Novo Registro</asp:HyperLink>
                    </div>

               <div class="text-start ms-2 gx-3 mb-3">
                    <asp:Button ID="cmd_Reabertura" runat="server" Text="Reabertura de CAT" CssClass="btn" Visible="False" OnClick="cmd_Reabertura_Click"/>
                    <asp:Button ID="cmd_Reabrir" runat="server" Text="Reabrir"  CssClass="btn2" Visible="False" OnClick="cmd_Reabrir_Click" />
              </div>

              <div class="text-start ms-2 mb-3">
                   <asp:Button ID="cmd_Voltar" runat="server" CssClass="btn2" Text="Voltar" onclick="cmd_Voltar_Click" />
              </div>
          </div>

               
                <asp:Label ID="lbl_Reabertura" runat="server" Text="Número do Recibo:" Visible="False"></asp:Label>
                <asp:TextBox ID="txt_Reabertura" runat="server" MaxLength="23" Visible="False" Width="211px"></asp:TextBox>
          

                <%--<igtbl:ultrawebgrid id="UltraWebGridExameClinico" runat="server" ImageDirectory="/ig_common/Images/"
							Height="128px" Width="592px" OnInitializeRow="UltraWebGridExameClinico_InitializeRow" OnPageIndexChanged="UltraWebGridExameClinico_PageIndexChanged">
							<DisplayLayout AutoGenerateColumns="False" RowHeightDefault="18px"
								Version="3.00" ViewType="OutlookGroupBy" RowSelectorsDefault="No"
								Name="UltraWebGridExameClinico" TableLayout="Fixed">
								<AddNewBox>
                                    <BoxStyle BackColor="LightGray" BorderStyle="Solid" BorderWidth="1px">
                                    </BoxStyle>
								</AddNewBox>
								<Pager PrevText="Anterior" NextText="Pr&#243;ximo" QuickPages="10" PageSize="5" Pattern="P&#225;gina &lt;b&gt;[currentpageindex]&lt;/b&gt; de &lt;b&gt;[pagecount]&lt;/b&gt;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&lt;b&gt;[default]&lt;/b&gt;"
									StyleMode="QuickPages" Alignment="Center" AllowPaging="True">
                                    <PagerStyle BackColor="#DEEFE4" BorderStyle="None" BorderWidth="0px" Font-Names="Verdana"
                                        Font-Size="XX-Small" ForeColor="#44926D" Height="18px" HorizontalAlign="Center" />
								</Pager>
								<HeaderStyleDefault VerticalAlign="Middle" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True"
									HorizontalAlign="Center" ForeColor="#44926D" BackColor="#DEEFE4" Height="18px"></HeaderStyleDefault>
								<GroupByRowStyleDefault Height="18px"></GroupByRowStyleDefault>
								<FrameStyle Width="592px" BorderWidth="1px" BorderColor="#7CC5A1" BorderStyle="Solid" Height="128px" BackColor="WhiteSmoke"></FrameStyle>
								<FooterStyleDefault BorderWidth="1px" BorderStyle="Solid">
									<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
								</FooterStyleDefault>
								<ActivationObject AllowActivation="False" BorderColor="124, 197, 161" BorderWidth=""></ActivationObject>
								<GroupByBox Hidden="True"></GroupByBox>
								<EditCellStyleDefault BorderWidth="0px" BorderStyle="None"></EditCellStyleDefault>
								<RowStyleDefault VerticalAlign="Middle" BorderWidth="1px" Font-Size="XX-Small" Font-Names="Verdana"
									BorderColor="#7CC5A1" BorderStyle="Solid" HorizontalAlign="Center" ForeColor="#156047" BackColor="White"></RowStyleDefault>
                                <Images ImageDirectory="/ig_common/Images/">
                                </Images>
                               
							</DisplayLayout>
							<Bands>
								<igtbl:UltraGridBand GroupByRowDescriptionMask="[caption] : [value] ([count])   --   Relat&#243;rio: &lt;a href=&quot;javascript:RelatorioN('[value]')&quot;&gt;Por Nome&lt;/a&gt; ou &lt;a href=&quot;javascript:RelatorioD('[value]')&quot;&gt;Por Data&lt;/a&gt;">
									<Columns>
										<igtbl:UltraGridColumn Key="IdAcidente" Width="0px" Hidden="True" AllowGroupBy="No" BaseColumnName="IdAcidente">
											<Header Caption="IdAcidente"></Header>
										</igtbl:UltraGridColumn>
										<igtbl:UltraGridColumn Key="DataAcidente" EditorControlID="" Width="150px" AllowGroupBy="No" Format=""
											BaseColumnName="DataAcidente" FooterText="">
											<SelectedCellStyle BorderStyle="None"></SelectedCellStyle>
											<CellButtonStyle Cursor="Hand" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True" HorizontalAlign="Left"
												ForeColor="#156047" BackColor="#EFFFF6">
												<Padding Left="3px"></Padding>
												<BorderDetails StyleBottom="None" StyleTop="None" StyleRight="None" StyleLeft="None"></BorderDetails>
											</CellButtonStyle>
											<CellStyle HorizontalAlign="Center">
												<BorderDetails StyleLeft="None"></BorderDetails>
											</CellStyle>
											<Footer Key="DataAcidente" Caption="">
												<RowLayoutColumnInfo OriginX="1"></RowLayoutColumnInfo>
											</Footer>
											<HeaderStyle>
												<BorderDetails StyleTop="None" StyleLeft="None"></BorderDetails>
											</HeaderStyle>
											<Header Caption="Data">
												<RowLayoutColumnInfo OriginX="1"></RowLayoutColumnInfo>
											</Header>
										</igtbl:UltraGridColumn>
										<igtbl:UltraGridColumn Key="Descricao" EditorControlID="" Width="250px" AllowGroupBy="No" Format="" BaseColumnName="Descricao" FooterText="" ChangeLinksColor="True">
											<CellButtonStyle Cursor="Hand" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True" HorizontalAlign="Left"
												ForeColor="#156047" BackColor="#EFFFF6" BorderStyle="None" BorderWidth="0px" Font-Underline="True">
												<Padding Left="3px"></Padding>
											</CellButtonStyle>
											<CellStyle Font-Bold="True" HorizontalAlign="Left">
												<Padding Left="3px"></Padding>
												<BorderDetails StyleLeft="None"></BorderDetails>
											</CellStyle>
											<Footer Key="Descricao" Caption="">
												<RowLayoutColumnInfo OriginX="2"></RowLayoutColumnInfo>
											</Footer>
											<HeaderStyle>
												<BorderDetails StyleTop="None" StyleLeft="None"></BorderDetails>
											</HeaderStyle>
											<Header Caption="Descricao">
												<RowLayoutColumnInfo OriginX="2"></RowLayoutColumnInfo>
											</Header>
										</igtbl:UltraGridColumn>
										<igtbl:UltraGridColumn Key="AgenteCausador" Width="180px" AllowGroupBy="No" BaseColumnName="AgenteCausador">
											<CellStyle HorizontalAlign="Center"></CellStyle>
											<Footer Key="AgenteCausador">
												<RowLayoutColumnInfo OriginX="3"></RowLayoutColumnInfo>
											</Footer>
											<HeaderStyle>
												<BorderDetails StyleTop="None"></BorderDetails>
											</HeaderStyle>
											<Header Caption="Agente Causador">
												<RowLayoutColumnInfo OriginX="3"></RowLayoutColumnInfo>
											</Header>
										</igtbl:UltraGridColumn>
									</Columns>
									<RowTemplateStyle BorderColor="White" BorderStyle="Ridge" BackColor="White">
										<BorderDetails WidthLeft="3px" WidthTop="3px" WidthRight="3px" WidthBottom="3px"></BorderDetails>
									</RowTemplateStyle>
                                    <AddNewRow View="NotSet" Visible="NotSet">
                                    </AddNewRow>
								</igtbl:UltraGridBand>
							</Bands>
						</igtbl:ultrawebgrid>--%>


                  </ContentTemplate>
                  </asp:UpdatePanel>
                  

         <eo:MsgBox ID="MsgBox1" runat="server" BackColor="#47729F" ControlSkinID="None" 
        HeaderHtml="Dialog Title" Height="100px" Width="250px" OnButtonClick="MsgBox1_ButtonClick">
        <HeaderStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #ffbf00 3px solid; padding-left: 4px; font-weight: bold; font-size: 11px; padding-bottom: 2px; color: white; padding-top: 2px; border-bottom: #22456a 1px solid; font-family: verdana" />
        <ContentStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #7d97b6 1px solid; padding-left: 4px; border-left-width: 1px; font-size: 11px; border-left-color: #728eb8; padding-bottom: 4px; color: white; padding-top: 4px; border-bottom: #22456a 1px solid; font-family: verdana" />
        <FooterStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #7d97b6 1px solid; padding-left: 4px; border-left-width: 1px; font-size: 11px; border-left-color: #728eb8; padding-bottom: 4px; color: white; padding-top: 4px; border-bottom: #22456a 1px solid; font-family: verdana" />
        </eo:MsgBox>

            </div>
        </div>
    </asp:Content>