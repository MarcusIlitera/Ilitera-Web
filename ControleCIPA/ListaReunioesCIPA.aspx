<%@ Page Language="C#"   AutoEventWireup="True"  MasterPageFile="~/Site.Master" Codebehind="ListaReunioesCIPA.aspx.cs"  Inherits="Ilitera.Net.ControleCIPA.ListaReunioesCIPA" Title="Ilitera.Net" %>

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
           .largeboldFont
           {
               font-weight: 700;
           }
    </style>

</asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent" >
    <div class="container-fluid d-flex ms-5 ps-4">
        <div class="row gx-3 gy-2">

    <script language="javascript">
</script>

            <div class="col-11">
                <div class="row">
                    <div class="col-md-4 gy-2 mb-3">
                        <asp:label id="lblCipa" runat="server" SkinID="BoldFont" CssClass="tituloLabel form-label">Selecione a CIPA desejada (Data de Posse)</asp:label>
                        <div class="row mt-2">
                            <div class="col-8">
                                <asp:dropdownlist id="ddlCIPA" onselectedindexchanged="ddlCIPA_SelectedIndexChanged" runat="server" AutoPostBack="True" CssClass="texto form-select form-select-sm"></asp:dropdownlist>
                            </div>
                        </div>
                    </div>

                    <div class="col-12">
                        <eo:Grid ID="UltraWebGridReuniaoCipa" runat="server" ColumnHeaderAscImage="00050403" 
                                    ColumnHeaderDescImage="00050404" FixedColumnCount="1" GridLines="Both" Height="254px" Width="1050px" ColumnHeaderDividerOffset="6" 
                                    ColumnHeaderHeight="30" ItemHeight="30" KeyField="IdReuniaoCIPA" ClientSideOnItemCommand="OnItemCommand" CssClass="grid">
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
                                    <eo:StaticColumn HeaderText="Tipo Evento" AllowSort="True" 
                                        DataField="TipoEvento" Name="TipoEvento" ReadOnly="True" 
                                        SortOrder="Ascending" Text="" Width="800">
                                        <CellStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500; color: #7D7B7B !important; text-align: left; height: 35px !important; padding: 0.3rem 0.3rem 0.3rem 0.5rem; height: 35px !important;" />
                                    </eo:StaticColumn>
                                    <eo:StaticColumn HeaderText="Data" AllowSort="True" 
                                        DataField="Data" Name="Data" ReadOnly="True" Width="250">
                                        <CellStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500; color: #7D7B7B !important; text-align: left; height: 35px !important; padding: 0.3rem 0.3rem 0.3rem 0.5rem; height: 35px !important;" />
                                    </eo:StaticColumn>
                                    <eo:StaticColumn AllowSort="True" DataField="Executa" 
                                        HeaderText="Executa" Name="Executa" ReadOnly="True"  Visible="false"
                                        Width="250">
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

                    <div class="col-md-3">
                        <div class="row">
                            <div class="col-8">
                                <asp:LinkButton ID="lkbPrintCalendario" OnClick="lkbPrintCalendario_Click" runat="server" SkinID="BoldLinkButton"> Calendário Anual <img src="Images/printer.svg" border="0" class="btnMenor ms-3" style="padding: .3rem;"></asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            
								


         


                                    <%--<igtbl:ultrawebgrid ID="UltraWebGridReuniaoCipa" runat="server" Height="254px"
                                        ImageDirectory="/ig_common/Images/" Width="600px">
                                        <Bands>
                                            <igtbl:UltraGridBand GroupByRowDescriptionMask="[caption] : [value] ([count])   --   Relat&#243;rio: &lt;a href=&quot;javascript:RelatorioN('[value]')&quot;&gt;Por Nome&lt;/a&gt; ou &lt;a href=&quot;javascript:RelatorioD('[value]')&quot;&gt;Por Data&lt;/a&gt;">
                                                <AddNewRow View="NotSet" Visible="NotSet">
                                                </AddNewRow>
                                                <FilterOptions AllString="" EmptyString="" NonEmptyString="">
                                                    <FilterDropDownStyle BackColor="White" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px"
                                                        CustomRules="overflow:auto;" Font-Names="Verdana,Arial,Helvetica,sans-serif"
                                                        Font-Size="11px" Width="200px">
                                                        <Padding Left="2px" />
                                                    </FilterDropDownStyle>
                                                    <FilterHighlightRowStyle BackColor="#151C55" ForeColor="White">
                                                    </FilterHighlightRowStyle>
                                                </FilterOptions>
                                                <Columns>
                                                    <igtbl:UltraGridColumn AllowGroupBy="No" BaseColumnName="IdReuniaoCipa"
                                                        Hidden="True" Key="IdReuniaoCipa" Width="0px">
                                                        <Header Caption="IdReuniaoCipa">
                                                        </Header>
                                                        <Footer Key="Id">
                                                        </Footer>
                                                    </igtbl:UltraGridColumn>
                                                    <igtbl:UltraGridColumn AllowGroupBy="No" BaseColumnName="TipoEvento" EditorControlID=""
                                                        FooterText="" Format="" Key="TipoEvento"
                                                        Width="170px">
                                                        <HeaderStyle>
                                                            <BorderDetails StyleLeft="None" StyleTop="None" />
                                                        </HeaderStyle>
                                                        <Header Caption="Reuni&#245;es">
                                                            <RowLayoutColumnInfo OriginX="1" />
                                                        </Header>
                                                        <SelectedCellStyle BorderStyle="None">
                                                        </SelectedCellStyle>
                                                        <CellStyle Font-Bold="False" HorizontalAlign="Center">
                                                            <BorderDetails StyleLeft="None" />
                                                            <Padding Left="3px" />
                                                        </CellStyle>
                                                        <CellButtonStyle BackColor="#EFFFF6" Cursor="Hand" Font-Bold="True" Font-Names="Verdana"
                                                            Font-Size="XX-Small" ForeColor="#156047" HorizontalAlign="Left">
                                                            <BorderDetails StyleBottom="None" StyleLeft="None" StyleRight="None" StyleTop="None" />
                                                            <Padding Left="3px" />
                                                        </CellButtonStyle>
                                                        <Footer Caption="" Key="NomeAbreviado">
                                                            <RowLayoutColumnInfo OriginX="1" />
                                                        </Footer>
                                                    </igtbl:UltraGridColumn>
                                                    <igtbl:UltraGridColumn AllowGroupBy="No" BaseColumnName="Data" EditorControlID=""
                                                        FooterText="" Format="" Key="Data" Width="170px">
                                                        <HeaderStyle>
                                                            <BorderDetails StyleRight="None" StyleTop="None" />
                                                        </HeaderStyle>
                                                        <Header Caption="Data">
                                                            <RowLayoutColumnInfo OriginX="2" />
                                                        </Header>
                                                        <CellStyle HorizontalAlign="Center">
                                                            <BorderDetails StyleRight="None" />
                                                            <Padding Left="3px" />
                                                        </CellStyle>
                                                        <CellButtonStyle BackColor="#EFFFF6" Cursor="Hand" Font-Bold="True" Font-Names="Verdana"
                                                            Font-Size="XX-Small" ForeColor="#156047" HorizontalAlign="Left">
                                                            <BorderDetails StyleBottom="None" StyleLeft="None" StyleRight="None" StyleTop="None" />
                                                            <Padding Left="3px" />
                                                        </CellButtonStyle>
                                                        <Footer Caption="" Key="NomeCompleto">
                                                            <RowLayoutColumnInfo OriginX="2" />
                                                        </Footer>
                                                    </igtbl:UltraGridColumn>
                                                    <igtbl:UltraGridColumn AllowGroupBy="No" BaseColumnName="Executa" Key="Executa" Width="258px">
                                                        <HeaderStyle>
                                                            <BorderDetails StyleTop="None" />
                                                        </HeaderStyle>
                                                        <Header>
                                                            <RowLayoutColumnInfo OriginX="3" />
                                                        </Header>
                                                        <CellStyle HorizontalAlign="Center" VerticalAlign="Middle" ForeColor="#156047">
                                                        </CellStyle>
                                                        <Footer>
                                                            <RowLayoutColumnInfo OriginX="3" />
                                                        </Footer>
                                                    </igtbl:UltraGridColumn>
                                                </Columns>
                                                <RowTemplateStyle BackColor="White" BorderColor="White" BorderStyle="Ridge">
                                                    <BorderDetails WidthBottom="3px" WidthLeft="3px" WidthRight="3px" WidthTop="3px" />
                                                </RowTemplateStyle>
                                            </igtbl:UltraGridBand>
                                        </Bands>
                                        <DisplayLayout AutoGenerateColumns="False"
                                            Name="UltraWebGridReuniaoCipa" RowHeightDefault="18px" RowSelectorsDefault="No"
                                            TableLayout="Fixed" Version="3.00" ViewType="OutlookGroupBy">
                                            <GroupByBox Hidden="True">
                                            </GroupByBox>
                                            <GroupByRowStyleDefault Height="18px">
                                            </GroupByRowStyleDefault>
                                            <ActivationObject AllowActivation="False" BorderColor="124, 197, 161" BorderWidth="">
                                            </ActivationObject>
                                            <FooterStyleDefault BorderStyle="Solid" BorderWidth="1px">
                                                <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" WidthTop="1px" />
                                            </FooterStyleDefault>
                                            <RowStyleDefault BorderColor="#7CC5A1" BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana"
                                                Font-Size="XX-Small" ForeColor="#156047" HorizontalAlign="Center" VerticalAlign="Middle" BackColor="White">
                                            </RowStyleDefault>
                                            <FilterOptionsDefault>
                                                <FilterDropDownStyle BackColor="White" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px"
                                                    CustomRules="overflow:auto;" Font-Names="Verdana,Arial,Helvetica,sans-serif"
                                                    Font-Size="11px" Width="200px">
                                                    <Padding Left="2px" />
                                                </FilterDropDownStyle>
                                                <FilterHighlightRowStyle BackColor="#151C55" ForeColor="White">
                                                </FilterHighlightRowStyle>
                                            </FilterOptionsDefault>
                                            <HeaderStyleDefault BackColor="#DEEFE4" Font-Bold="True" Font-Names="Verdana" Font-Size="XX-Small"
                                                ForeColor="#44926D" Height="18px" HorizontalAlign="Center" VerticalAlign="Middle">
                                            </HeaderStyleDefault>
                                            <EditCellStyleDefault BorderStyle="None" BorderWidth="0px">
                                            </EditCellStyleDefault>
                                            <FrameStyle BorderColor="#7CC5A1" BorderStyle="Solid" BorderWidth="1px" Height="254px"
                                                Width="600px" BackColor="WhiteSmoke">
                                            </FrameStyle>
                                            <Pager Alignment="Center" NextText="Pr&#243;ximo" PageSize="12"
                                                Pattern=""
                                                PrevText="Anterior" QuickPages="7" StyleMode="QuickPages" AllowPaging="True">
                                                <PagerStyle BackColor="#DEEFE4" BorderStyle="None" BorderWidth="0px" Font-Names="Verdana"
                                                    Font-Size="XX-Small" ForeColor="#44926D" Height="18px" HorizontalAlign="Center" />
                                            </Pager>
                                            <AddNewBox>
                                                <BoxStyle BackColor="LightGray" BorderStyle="Solid" BorderWidth="1px">
                                                </BoxStyle>
                                            </AddNewBox>
                                            <Images ImageDirectory="/ig_common/Images/">
                                            </Images>
                                        </DisplayLayout>
                                    </igtbl:ultrawebgrid>--%></td>


							</tr>
						</table>
					</td>
				</TR>
				<tr>
					<td align="center">
						<table class="defaultFont" cellSpacing="0" cellPadding="0" width="600" border="0">
							<tr>
								<td width="200"></td>
								<TD class="normalFont" align="center" width="200">
                                    </TD>
								<td align="right" width="200" valign="top"><asp:label id="lblTotRegistros" runat="server"></asp:label></td>
							</tr>
						</table>
						<asp:label id="lblError" runat="server" CssClass="errorFont" Width="550px" SkinID="ErrorFont"></asp:label></td>
                        <input id="txtAuxiliar" type="hidden" runat="server" />
				</tr>
			</TABLE>
            

        </div>
    </div>
</asp:Content>
