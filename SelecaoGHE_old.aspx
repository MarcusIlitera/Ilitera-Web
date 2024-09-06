<%@ Page Title="Ilitera.Net" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="True"    CodeBehind="SelecaoGHE.aspx.cs" Inherits="Ilitera.Net.SelecaoGHE"   EnableEventValidation="false"  ValidateRequest="false"  %>

<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>


<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <link href="css/forms.css" rel="stylesheet" type="text/css" />
    <link href="css/tabelas.css" rel="stylesheet" type="text/css" />
       <style type="text/css">
        .linha
   {
	font: 8px Verdana, Arial, Helvetica, sans-serif, Tahoma;
    }
        .style1
        {
            width: 192px;
        }
           .btnLogarClass
           {}
           .auto-style1 {
               width: 192px;
               height: 21px;
           }
    </style>

</asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent" >
    <script language="javascript">

    function Reload() {
        var f = document.getElementById('SubDados');
        //f.src = f.src;
        f.contentWindow.location.reload(true);
    }


    function OnItemCommand(grid, itemIndex, colIndex, commandName) {

        //grid.raiseItemCommandEvent(itemIndex, commandName);
        grid.raiseItemCommandEvent(itemIndex, colIndex.toString());
    }

    function OnCellSelected(grid) {
        var cell = grid.getSelectedCell();
        grid.raiseItemCommandEvent(cell.getItemIndex(), "Seleção");
    }

    


    </script>

    <div class="container-fluid d-flex ms-5 ps-4">
     <div class="row gx-3 gy-2 w-100">
    <script language="javascript">

    function Reload() {
        var f = document.getElementById('SubDados');
        //f.src = f.src;
        f.contentWindow.location.reload(true);
    }


    function OnItemCommand(grid, itemIndex, colIndex, commandName) {

        //grid.raiseItemCommandEvent(itemIndex, commandName);
        grid.raiseItemCommandEvent(itemIndex, colIndex.toString());
    }



    </script>

         <div class="col-11 subtituloBG" style="padding-top: 10px" >
            <asp:Label ID="lblPPRA" runat="server" class="subtitulo">Seleção de GHE</asp:Label>
        </div>

         <div class="col-11 mb-3">
             <div class="row">
                 <div class="col-md-2 gx-3 gy-2">
                     <asp:Label ID="Label1" runat="server" Text="Selecione PPRA" CssClass="tituloLabel form-label"></asp:Label>
                     <asp:ListBox ID="lst_PPRA" runat="server" Height="106px" AutoPostBack="True" onselectedindexchanged="lst_PPRA_SelectedIndexChanged" CssClass="texto form-control"></asp:ListBox>
                     <div class="ms-4 mt-2">
                         <asp:CheckBox ID="chk_Finalizado" runat="server" AutoPostBack="True" OnCheckedChanged="chk_Finalizado_CheckedChanged"  Text="Somente Finalizados" CssClass="texto form-check-input" />
                     </div>
                 </div>

                 <div class="col-10">
                     <div class="row">
                         <div class="col-12 gx-3 gy-2">
                             <asp:Label ID="lbl_Selecao" runat="server" Text="Lista de GHE do PPRA Selecionado" CssClass="tituloLabel form-label"></asp:Label>
                             <asp:DropDownList ID="cmb_GHE" runat="server" AutoPostBack="True" onselectedindexchanged="cmb_GHE_SelectedIndexChanged"  CssClass="texto form-select"></asp:DropDownList>
                         </div>

                         <div class="col-12 gx-3 gy-2">
                             <asp:Label ID="lbl_Selecao0" runat="server" Text="Riscos / Agentes Nocivos do GHE" CssClass="tituloLabel form-label"></asp:Label>
                             <asp:ListBox ID="lst_Riscos" runat="server" Height="67px" CssClass="texto form-control"></asp:ListBox>
                         </div>
                     </div>
                 </div>
             </div>
         </div>

         <div class="col-11 subtituloBG" style="padding-top: 10px" >
            <asp:Label runat="server" class="subtitulo">Ajustar GHE dos Colaboradores</asp:Label>
        </div>

         <div class="col-11 mb-3">
             <div class="row">
                 <div class="col-12">
                     <div class="row">
                         <div class="col-4 gx-3 gy-2 mb-2">
                             <asp:Label ID="lbl_Selecao1" runat="server" Text="Colaboradores sem GHE para esse PPRA" CssClass="tituloLabel form-label"></asp:Label>
                         </div>
                         <div class="col-4 gx-3 gy-2 mb-2">
                             <asp:CheckBox ID="chk_Demitidos" runat="server" AutoPostBack="True" oncheckedchanged="chk_Demitidos_CheckedChanged"  Text="Visualizar demitidos" CssClass="texto form-check-input" />
                         </div>

                         <%-- TABELA --%>

                         <div class="col-12 mb-3">
                                        <eo:Grid ID="gridEmpregadossemGHE" runat="server" BorderColor="Black" 
                                            BorderWidth="1px" ColumnHeaderAscImage="00050403" 
                                            ColumnHeaderDescImage="00050404" ColumnHeaderDividerImage="00050402" 
                                            FixedColumnCount="1" Font-Bold="False" Font-Italic="False" Font-Names="Verdana" 
                                            Font-Overline="False" Font-Size="7pt" Font-Strikeout="False" 
                                            Font-Underline="False" GridLineColor="240, 240, 240" 
                                            GridLines="Both" Height="145px" Width="800px" ColumnHeaderDividerOffset="6" 
                                            ColumnHeaderHeight="18" ItemHeight="18" KeyField="nId_Empregado_Funcao"  
                                    OnItemCommand="gridEmpregadossemGHE_ItemCommand"  
                                    ClientSideOnItemCommand="OnItemCommand"  >
                                        <ItemStyles>
                                            <eo:GridItemStyleSet>
                                                <ItemStyle CssText="background-color: white" />
                                                <AlternatingItemStyle CssText="background-color:#eeeeee;" />
                                                <SelectedStyle CssText="border-bottom-style:solid;border-left-style:solid;border-right-style:solid;border-top-style:solid;" />
                                                <CellStyle CssText="padding-left:8px;padding-top:2px; color:#black;white-space:nowrap;" />
                                            </eo:GridItemStyleSet>
                                        </ItemStyles>
                                        <ColumnHeaderTextStyle CssText="" />
                                        <ColumnHeaderStyle CssText="background-image:url('00050401');padding-left:8px;padding-top:2px;" />
                                        <Columns>

                                            <eo:StaticColumn HeaderText="" AllowSort="True" 
                                                DataField="nId_Empregado_Funcao" Name="nId_Empregado_Funcao" ReadOnly="True" 
                                                SortOrder="Ascending" Text="" Width="0">
                                                <CellStyle CssText="background-color:#ffffcc;font-family:Tahoma;font-weight:bold;text-align:right" />
                                            </eo:StaticColumn>

                                            <eo:StaticColumn HeaderText="Colaborador" AllowSort="True" 
                                                DataField="tno_Empg" Name="tno_Empg" ReadOnly="True" 
                                                SortOrder="Ascending" Text="" Width="245">
                                                <CellStyle CssText="background-color:#ffffcc;font-family:Tahoma;font-size:6pt;font-weight:bold;text-align:right;" />
                                            </eo:StaticColumn>

                                            <eo:StaticColumn HeaderText="Função" AllowSort="True" 
                                                DataField="tno_Func_Empr" Name="tno_Func_Empr" ReadOnly="True" 
                                                SortOrder="Ascending" Text="" Width="185">
                                                <CellStyle CssText="background-color:#ffffcc;font-family:Tahoma;font-size:6pt;text-align:right;" />
                                            </eo:StaticColumn>

                                            <eo:StaticColumn HeaderText="Setor" AllowSort="True" 
                                                DataField="tno_Str_Empr" Name="tno_Str_Empr" ReadOnly="True" 
                                                SortOrder="Ascending" Text="" Width="185">
                                                <CellStyle CssText="background-color:#ffffcc;font-family:Tahoma;font-size:6pt;text-align:right;" />
                                            </eo:StaticColumn>

                                            <eo:StaticColumn HeaderText="Dt.Início" AllowSort="True" 
                                                DataField="hDT_Inicio" Name="hDT_Inicio" ReadOnly="True" 
                                                SortOrder="Ascending" Text="" Width="80">
                                                <CellStyle CssText="background-color:#ffffcc;font-family:Tahoma;font-size:6pt;font-weight:bold;text-align:center;" />
                                            </eo:StaticColumn>

                                            <eo:StaticColumn HeaderText="Dt.Término" AllowSort="True" 
                                                DataField="hDT_Termino" Name="hDT_Termino" ReadOnly="True" 
                                                SortOrder="Ascending" Text="" Width="80">
                                                <CellStyle CssText="background-color:#ffffcc;font-family:Tahoma;font-size:7pt;font-weight:bold;text-align:center;" />
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

                         <%-- BOTÕES --%>

                         <div class="col-12 mb-3">
                             <div class="row justify-content-center">
                                 <div class="col-2">
                                     <asp:Button ID="cmd_Add_1" runat="server" CssClass="btn" onclick="cmd_Add_1_Click"  Text="Adicionar no GHE" width="130px" />
                                 </div>

                                 <div class="col-2">
                                    <asp:Button ID="cmd_Remove_1" runat="server" CssClass="btn" onclick="cmd_Remove_1_Click"  Text="Remover do GHE" Width="130px" />
                                 </div>
                             </div>
                         </div>

                         <div class="col-md-4 gx-3 gy-2 mb-2">
                             <asp:Label ID="lbl_Selecao2" runat="server" Text="Colaboradores alocados no GHE" CssClass="tituloLabel form-label"></asp:Label>
                         </div>

                         <%-- TABELA DOIS --%>

                         <div class="col-12 mb-3">
                                        <eo:Grid ID="gridEmpregados" runat="server" BorderColor="Black" 
                                            BorderWidth="1px" ColumnHeaderAscImage="00050403" 
                                            ColumnHeaderDescImage="00050404" ColumnHeaderDividerImage="00050402" 
                                            FixedColumnCount="1" Font-Bold="False" Font-Italic="False" Font-Names="Verdana" 
                                            Font-Overline="False" Font-Size="7pt" Font-Strikeout="False" 
                                            Font-Underline="False" GridLineColor="240, 240, 240" 
                                            GridLines="Both" Height="145px" Width="800px" ColumnHeaderDividerOffset="6" 
                                            ColumnHeaderHeight="18" ItemHeight="18" KeyField="nId_Empregado_Funcao"  
                                    OnItemCommand="gridEmpregados_ItemCommand"  
                                    ClientSideOnItemCommand="OnItemCommand"  >
                                        <ItemStyles>
                                            <eo:GridItemStyleSet>
                                                <ItemStyle CssText="background-color: white" />
                                                <AlternatingItemStyle CssText="background-color:#eeeeee;" />
                                                <SelectedStyle CssText="border-bottom-style:solid;border-left-style:solid;border-right-style:solid;border-top-style:solid;" />
                                                <CellStyle CssText="padding-left:8px;padding-top:2px; color:#black;white-space:nowrap;" />
                                            </eo:GridItemStyleSet>
                                        </ItemStyles>
                                        <ColumnHeaderTextStyle CssText="" />
                                        <ColumnHeaderStyle CssText="background-image:url('00050401');padding-left:8px;padding-top:2px;" />
                                        <Columns>

                                            <eo:StaticColumn HeaderText="" AllowSort="True" 
                                                DataField="nId_Empregado_Funcao" Name="nId_Empregado_Funcao" ReadOnly="True" 
                                                SortOrder="Ascending" Text="" Width="0">
                                                <CellStyle CssText="background-color:#ffffcc;font-family:Tahoma;font-weight:bold;text-align:center" />
                                            </eo:StaticColumn>

                                            <eo:StaticColumn HeaderText="Colaborador" AllowSort="True" 
                                                DataField="tno_Empg" Name="tno_Empg" ReadOnly="True" 
                                                SortOrder="Ascending" Text="" Width="245">
                                                <CellStyle CssText="background-color:#ffffcc;font-family:Tahoma;font-size:6pt;font-weight:bold;text-align:right;" />
                                            </eo:StaticColumn>

                                            <eo:StaticColumn HeaderText="Função" AllowSort="True" 
                                                DataField="tno_Func_Empr" Name="tno_Func_Empr" ReadOnly="True" 
                                                SortOrder="Ascending" Text="" Width="185">
                                                <CellStyle CssText="background-color:#ffffcc;font-family:Tahoma;font-size:6pt;text-align:right;" />
                                            </eo:StaticColumn>

                                            <eo:StaticColumn HeaderText="Setor" AllowSort="True" 
                                                DataField="tno_Str_Empr" Name="tno_Str_Empr" ReadOnly="True" 
                                                SortOrder="Ascending" Text="" Width="185">
                                                <CellStyle CssText="background-color:#ffffcc;font-family:Tahoma;font-size:6pt;text-align:right;" />
                                            </eo:StaticColumn>

                                            <eo:StaticColumn HeaderText="Dt.Início" AllowSort="True" 
                                                DataField="hDT_Inicio" Name="hDT_Inicio" ReadOnly="True" 
                                                SortOrder="Ascending" Text="" Width="80">
                                                <CellStyle CssText="background-color:#ffffcc;font-family:Tahoma;font-size:6pt;font-weight:bold;text-align:center;" />
                                            </eo:StaticColumn>

                                            <eo:StaticColumn HeaderText="Dt.Término" AllowSort="True" 
                                                DataField="hDT_Termino" Name="hDT_Termino" ReadOnly="True" 
                                                SortOrder="Ascending" Text="" Width="80">
                                                <CellStyle CssText="background-color:#ffffcc;font-family:Tahoma;font-size:6pt;text-align:center;" />
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
                     </div>
                 </div>
             </div>
         </div>
                                      
                                                     


                                                    <tr>
                                                        <td>
                                                            <asp:ListBox ID="lst_Id_GHE" runat="server" Height="38px" Visible="False" 
                                                                Width="49px"></asp:ListBox>
                                                            <asp:ListBox ID="lst_1_ID" runat="server" Height="35px"  Visible="False" 
                                                                Width="38px"></asp:ListBox>
                                                            <asp:ListBox ID="lst_1" runat="server" Height="43px" Visible="False" 
                                                                Width="25px"></asp:ListBox>
                                                            <asp:ListBox ID="lst_2" runat="server" Height="44px"  Visible="False" 
                                                                Width="29px"></asp:ListBox>
                                                            <asp:ListBox ID="lst_Id_PPRA" runat="server" AutoPostBack="True" 
                                                                Font-Bold="True" Font-Names="Cordia New" Font-Size="XX-Small" Height="16px" Visible="False" 
                                                                Width="74px"></asp:ListBox>
                                                        </td>
                                                    </tr>
                                                    </td>
                                                </tr>
                                            
                                    </table>

         <asp:Label ID="lbl_Id_Usuario" runat="server" Text="IdUsuario" Visible="False"></asp:Label>
         <asp:Label ID="lbl_Id_1" runat="server" Text="lbl_Id_1" Visible="False"></asp:Label>
         <asp:Label ID="lbl_Id_Empresa" runat="server" Text="IdEmpresa" Visible="False"></asp:Label>
         <asp:Label ID="lbl_Empresa" runat="server" Text="lbl_Id_1" Visible="False"></asp:Label>
         <asp:Label ID="lbl_Id" runat="server" Text="0" Visible="False"></asp:Label>

                                    
                                    
    <eo:MsgBox ID="MsgBox1" runat="server" BackColor="#47729F" ControlSkinID="None" 
        HeaderHtml="Dialog Title" Height="100px" Width="168px">
        <HeaderStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #ffbf00 3px solid; padding-left: 4px; font-weight: bold; font-size: 11px; padding-bottom: 2px; color: white; padding-top: 2px; border-bottom: #22456a 1px solid; font-family: verdana" />
        <ContentStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #7d97b6 1px solid; padding-left: 4px; border-left-width: 1px; font-size: 11px; border-left-color: #728eb8; padding-bottom: 4px; color: white; padding-top: 4px; border-bottom: #22456a 1px solid; font-family: verdana" />
        <FooterStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #7d97b6 1px solid; padding-left: 4px; border-left-width: 1px; font-size: 11px; border-left-color: #728eb8; padding-bottom: 4px; color: white; padding-top: 4px; border-bottom: #22456a 1px solid; font-family: verdana" />
        </eo:MsgBox>
         
         </div>
    </div>         
</asp:Content>
