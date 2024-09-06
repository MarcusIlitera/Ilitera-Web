<%@ Page Title="Ilitera.Net" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="True"
    CodeBehind="RiscosEPIs.aspx.cs" Inherits="Ilitera.Net.RiscosEPIs" %>


<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <link href="css/forms.css" rel="stylesheet" type="text/css" />
    <link href="css/tabelas.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .defaultFont
    {
        width: 586px;
            height: 20px;
        }
        
        .style1
        {
            width: 446px;
            height: 20px;
        }
        .style2
        {
            width: 446px;
        }
        
    </style>
</asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent" >
    <div class="container-fluid d-flex ms-5 ps-4">
     <div class="row gx-3 gy-2 w-100">

    <eo:CallbackPanel runat="server" id="CallbackPanel1" Triggers="" Width="1050px">


			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="1050" border="0">
				<TR class="row">
					<TD class="col-12 mb-3" >
                        <asp:Label ID="lbl_Colaborador" runat="server" SkinID="TitleFont" 
                            Text="Colaborador:" class="tituloLabel" ></asp:Label>
                        <br />
                        <br />
                        <div class="subtituloBG" style="padding-top: 10px;">
                            <asp:Label ID="Label1" runat="server" SkinID="TitleFont" CssClass="subtitulo"
                            Text="Riscos Ambientais"></asp:Label>
                        </div>
                        
					</TD>
					
				</TR>
				<TR class="row">
					<TD class="normalFont" vAlign="top"  width="5"></TD>
					<TD  class="col-12">
                                <eo:Grid ID="gridRiscos" runat="server" ClientSideOnItemCommand="OnItemCommand" ColumnHeaderAscImage="00050403" 
                                    ColumnHeaderDescImage="00050404"  ColumnHeaderDividerOffset="6" ColumnHeaderHeight="30" FixedColumnCount="1" GridLines="Both" 
                                    Height="200px" ItemHeight="30" KeyField="Id" Width="1050px" CssClass="grid">
                                    <ItemStyles>
                                        <eo:GridItemStyleSet>
                                            <ItemStyle CssText="background-color: #FAFAFA;" />
                                            <ItemHoverStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500;  text-align: left; height: 30px !important; background: rgba(28, 148, 137, 0.80); color: #FFFFFF; padding: 0.3rem 0.3rem 0.3rem 0.5rem; " />
                                            <AlternatingItemStyle CssText="background-color: #F2F2F2;" />
                                            <AlternatingItemHoverStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500;  text-align: left; height: 30px !important; background: rgba(28, 148, 137, 0.80); color: #FFFFFF; padding: 0.3rem 0.3rem 0.3rem 0.5rem;" />
                                            <SelectedStyle CssText="border-bottom-style:solid;border-left-style:solid;border-right-style:solid;border-top-style:solid;" />
                                            <CellStyle CssText="padding: 0.3rem 0.3rem 0.3rem 0.5rem; height: 30px !important;" />
                                        </eo:GridItemStyleSet>
                                    </ItemStyles>
                                    <ColumnHeaderStyle CssClass="tabelaC colunas" />
                                    <Columns>
                                        <eo:StaticColumn AllowSort="True" DataField="NomeAgente" HeaderText="Nome do Agente" 
                                            Name="NomeAgente" ReadOnly="True" Width="525">
                                            <CellStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500; color: #B0ABAB !important; text-align: left; height: 30px !important;" />
                                        </eo:StaticColumn>
                                        <eo:StaticColumn AllowSort="True" DataField="Medicao" HeaderText="Medição" 
                                            Name="Medicao" ReadOnly="True" SortOrder="Ascending" Text="" Width="525">
                                            <CellStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500; color: #B0ABAB !important; text-align: left; height: 30px !important;" />
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

                            <table  border="0" cellpadding="0" cellspacing="0" 
                            class="defaultFont" width="340">
                                <tr>
                                    <td  width="340">
                                        &nbsp;</td>
                                </tr>
                        </table>
                    </TD>
                    <TD class="col-12 mt-3 mb-3">
                        <div class="subtituloBG" style="padding-top: 10px;">
                            <asp:Label ID="Label2" runat="server" SkinID="TitleFont" Text="EPI's Indicados" CssClass="subtitulo"></asp:Label>
                        </div>
					</TD>
					<TD valign="top" class="col-12"  width="1050">


                                <eo:Grid ID="gridEPI" runat="server" ClientSideOnItemCommand="OnItemCommand" ColumnHeaderAscImage="00050403" 
                                    ColumnHeaderDescImage="00050404" ColumnHeaderDividerOffset="6" ColumnHeaderHeight="30" FixedColumnCount="1"  GridLines="Both" 
                                    Height="200px" ItemHeight="30" KeyField="Id" Width="1050px" CssClass="grid">
                                    <ItemStyles>
                                        <eo:GridItemStyleSet>
                                            <ItemStyle CssText="background-color: #FAFAFA;" />
                                            <ItemHoverStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500;  text-align: left; height: 30px !important; background: rgba(28, 148, 137, 0.80); color: #FFFFFF; padding: 0.3rem 0.3rem 0.3rem 0.5rem; " />
                                            <AlternatingItemStyle CssText="background-color: #F2F2F2;" />
                                            <AlternatingItemHoverStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500;  text-align: left; height: 30px !important; background: rgba(28, 148, 137, 0.80); color: #FFFFFF; padding: 0.3rem 0.3rem 0.3rem 0.5rem;" />
                                            <SelectedStyle CssText="border-bottom-style:solid;border-left-style:solid;border-right-style:solid;border-top-style:solid;" />
                                            <CellStyle CssText="padding: 0.3rem 0.3rem 0.3rem 0.5rem; height: 30px !important;" />
                                        </eo:GridItemStyleSet>
                                    </ItemStyles>
                                    <ColumnHeaderStyle CssClass="tabelaC colunas" />
                                    <Columns>
                                        <eo:StaticColumn AllowSort="True" DataField="NomeEPI" HeaderText="Nome do EPI" 
                                            Name="NomeEPI" ReadOnly="True" SortOrder="Ascending" Text="" Width="1050">
                                            <CellStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500; color: #B0ABAB !important; text-align: left; height: 30px !important;" />
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

                        <table  border="0" cellpadding="0" cellspacing="0" class="defaultFont"
                            width="235">
                            <tr>
                                <td align="right" width="235">
                                    &nbsp;</td>
                            </tr>
                        </table>
                    </TD>
				</TR>

                <tr class="row">
                    <td class="col-12">
                        <asp:Button ID="cmd_Voltar" onclick="cmd_Voltar_Click"  runat="server" CssClass="btn" Text="Voltar" Width="127px" />
                    </td>
                </tr>
                

                
                
			</TABLE>
<%--		</form>
	</body>
</HTML>
--%>


</eo:CallbackPanel>

        
         <eo:MsgBox ID="MsgBox1" runat="server" BackColor="#47729F" ControlSkinID="None" 
        HeaderHtml="Dialog Title" Height="100px" Width="168px">
        <HeaderStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #ffbf00 3px solid; padding-left: 4px; font-weight: bold; font-size: 11px; padding-bottom: 2px; color: white; padding-top: 2px; border-bottom: #22456a 1px solid; font-family: verdana" />
        <ContentStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #7d97b6 1px solid; padding-left: 4px; border-left-width: 1px; font-size: 11px; border-left-color: #728eb8; padding-bottom: 4px; color: white; padding-top: 4px; border-bottom: #22456a 1px solid; font-family: verdana" />
        <FooterStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #7d97b6 1px solid; padding-left: 4px; border-left-width: 1px; font-size: 11px; border-left-color: #728eb8; padding-bottom: 4px; color: white; padding-top: 4px; border-bottom: #22456a 1px solid; font-family: verdana" />
        </eo:MsgBox>

         </div>
        </div>
</asp:Content>
