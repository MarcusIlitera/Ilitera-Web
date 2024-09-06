<%@ Page Title="Ilitera.Net" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="True"    CodeBehind="Toxicologico_Sorteio.aspx.cs" Inherits="Ilitera.Net.Toxicologico_Sorteio" %>

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
           {
           }
           .auto-style2 {
               margin-left: 68px;
           }
           .auto-style3 {
               width: 4px;
           }
           .auto-style5 {
               width: 443px;
           }
           .auto-style6 {
               width: 573px;
           }
           .auto-style7 {
               width: 1188px;
           }
           .auto-style9 {
               width: 60px;
           }
           .auto-style10 {
               width: 1210px;
           }

            #ctl00_MainContent_Grid1_c0_sort, #ctl00_MainContent_Grid1_c1_sort, #ctl00_MainContent_Grid1_c2_sort, #ctl00_MainContent_Grid1_c3_sort, #ctl00_MainContent_grd_Empregados_c0_sort,
            #ctl00_MainContent_grd_Empregados_c1_sort, #ctl00_MainContent_grd_Sorteados_c0_sort{
            width: 12px;
            opacity: 35%;
            padding-top: 0.3rem;
            }
           </style>

</asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent" >


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
            <div class="col-12">
                <div class="row">
                     <div class="col-11 subtituloBG mb-1" style="padding-top: 10px;">
                         <asp:Label ID="lblTitulo" runat="server" SkinID="TtleFont" CssClass="subtitulo">Sorteio - Toxicológico</asp:Label>
                     </div>
                    <div class="col-11 gy-2">
                        <div class="row">
                            <div class="col-3 gy-2 gx-2">
                                <asp:label id="lblPCMSO" runat="server" CssClass="tituloLabel">Sorteios Criados</asp:label>
                                <asp:dropdownlist id="ddlSorteios" runat="server" CssClass="texto form-select form-select-sm" OnSelectedIndexChanged="ddlSorteios_SelectedIndexChanged"></asp:dropdownlist>
                            </div>
                            <div class="col-2 gy-4 gx-3">
                                <asp:button ID="cmd_Novo_Sorteio" runat="server" Text="Criar Novo Sorteio" CssClass="btn" OnClick="cmd_Novo_Sorteio_Click" />
                            </div>
                             <div class="col-2 gy-4 gx-3">
                                <asp:button ID="cmd_Carregar" runat="server" Text="Carregar Sorteio" CssClass="btn" OnClick="cmd_Carregar_Sorteio_Click" />
                            </div>
                            <div class="col-3 col-xxl-2 gy-2 gx-3">
                                <asp:label id="lblPCMSO3" runat="server" CssClass="tituloLabel">Clínica</asp:label>
                                <asp:dropdownlist id="ddlClinica" runat="server" CssClass="texto form-select form-select-sm"></asp:dropdownlist>
                            </div>
                            <div class="col-3 gy-2 gx-3">
                                <asp:label id="lblPCMSO0" runat="server" CssClass="tituloLabel">Código do Sorteio</asp:label>
                                <asp:textbox id="txt_Codigo" runat="server" CssClass="texto form-control form-control-sm"></asp:textbox>
                            </div>                          
                            <div class="col-3 col-xxl-2 gy-2 gx-3">
                                <asp:button ID="cmd_Salvar" runat="server" Text="Salvar" CssClass="btn" OnClick="cmd_Salvar_Click"/>
                            </div>
                        </div>
                    </div>
                     <%--Tabela 1 --%>
                    <div class="col-7 gx-3 gy-4">
                         <asp:label id="Label2" runat="server" CssClass="tituloLabel">Colaboradores Ativos</asp:label>
                             <eo:Grid ID="grd_Colaboradores_Ativos" runat="server" ColumnHeaderAscImage="00050403" 
                            ColumnHeaderDescImage="00050404"
                            FixedColumnCount="1" GridLineColor="240, 240, 240" 
                            GridLines="Both" Height="255px" Width="800px" ColumnHeaderDividerOffset="6" 
                            ColumnHeaderHeight="30" ItemHeight="30" KeyField="nId_Empregado"  OnItemCommand="grd_Colaboradores_Ativos_ItemCommand"
                            ClientSideOnItemCommand="OnItemCommand">
                            <itemstyles>
                                <eo:GridItemStyleSet>
                                    <ItemStyle CssText="background-color: #FAFAFA;" />
                                       <AlternatingItemStyle CssText="background-color: #F2F2F2;" />
                                       <SelectedStyle CssText="border-bottom-style:solid;border-left-style:solid;border-right-style:solid;border-top-style:solid;" />
                                       <CellStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: normal; color: #B0ABAB; text-align: left; height: 30px !important; " />
                                </eo:GridItemStyleSet>
                          </itemstyles>
                          <ColumnHeaderTextStyle CssText="" />
                          <ColumnHeaderStyle CssClass="tabelaC colunas" />
                          <Columns>
                            <eo:StaticColumn AllowSort="True" DataField="tNo_Empg" HeaderText="Nome do Colaborador" Name="tNo_Empg" ReadOnly="True" SortOrder="Ascending" Text="" Width="220" >
                                <CellStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: normal; color: #B0ABAB; text-align: left; height: 30px !important; " />
                            </eo:StaticColumn>                                                           
                            <eo:StaticColumn AllowSort="True" DataField="Funcao" HeaderText="Função" Name="Funcao" ReadOnly="True" Width="190">
                                <CellStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: normal; color: #B0ABAB; text-align: left; height: 30px !important; " />
                            </eo:StaticColumn>
                            <eo:StaticColumn AllowSort="True" DataField="Data_Sorteio" HeaderText="Data Sorteio" Name="DataSorteio" ReadOnly="True" Width="120">
                                <CellStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: normal; color: #B0ABAB; text-align: left; height: 30px !important; " />
                            </eo:StaticColumn>
                            <eo:StaticColumn AllowSort="True" DataField="Sorteado" HeaderText="Sorteado" Name="Sorteado" ReadOnly="True" Width="90">
                                <CellStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: normal; color: #B0ABAB; text-align: left; height: 30px !important; " />
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
                    </div>
                        <div class="col-1">
                            <div class="row">
                                <div class="col-12 gy-4 gx-2">
                                    <asp:button ID="cmd_Add" runat="server" Text="&gt;&gt;" Width="58px" CssClass="btnMenor" OnClick="cmd_Add_Click" />
                                </div>
                                 <div class="col-12 gy-4 gx-2">
                                    <asp:button ID="cmd_Remove" runat="server" Text="<<" Width="58px" CssClass="btnMenor" OnClick="cmd_Remove_Click"/>
                                </div>
                            </div>
                        </div>
                    <%--Tabela 2 --%>
                    <div class="col-4 gx-3 gy-4">
                        <asp:label id="lblPPRA0" runat="server" CssClass="tituloLabel">Colaboradores participantes do Sorteio</asp:label>
                        <eo:Grid ID="grd_Lista_Sorteio" runat="server" ColumnHeaderAscImage="00050403" 
                            ColumnHeaderDescImage="00050404"
                            FixedColumnCount="1" GridLineColor="240, 240, 240" 
                            GridLines="Both" Height="255px" Width="350px" ColumnHeaderDividerOffset="6" 
                            ColumnHeaderHeight="30" ItemHeight="30" KeyField="Id_Toxicologico_Sorteio_Colaborador" OnItemCommand="grd_Lista_Sorteio_ItemCommand"  
                            ClientSideOnItemCommand="OnItemCommand">
                            <itemstyles>
                               <eo:GridItemStyleSet>
                            <ItemStyle CssText="background-color: #FAFAFA;" />
                                <AlternatingItemStyle CssText="background-color: #F2F2F2;" />
                                <SelectedStyle CssText="border-bottom-style:solid;border-left-style:solid;border-right-style:solid;border-top-style:solid;" />
                                <CellStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: normal; color: #B0ABAB; text-align: left; height: 30px !important; " />
                              </eo:GridItemStyleSet>
                           </itemstyles>
                           <ColumnHeaderTextStyle CssText="" />
                           <ColumnHeaderStyle CssClass="tabelaC colunas" />
                           <Columns>
                              <eo:StaticColumn AllowSort="True" DataField="tNo_Empg" HeaderText="Nome do Colaborador" Name="tNo_Empg" ReadOnly="True" SortOrder="Ascending" Text="" Width="220" >
                                   <CellStyle  CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: normal; color: #B0ABAB; text-align: left; height: 30px !important; " />
                              </eo:StaticColumn>
                               <eo:StaticColumn AllowSort="True" DataField="Funcao" HeaderText="Função" Name="Funcao" ReadOnly="True" Width="190">
                                   <CellStyle  CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: normal; color: #B0ABAB; text-align: left; height: 30px !important; " />
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
                    </div>

                    <div class="col-3 col-xxl-2 gy-2 gx-3">
                         <asp:label id="lblPCMSO2"  runat="server" CssClass="tituloLabel">Data Exame</asp:label>
                        <asp:textbox id="txt_Data" runat="server" Width="100" MaxLength="10" CssClass="texto form-control form-control-sm" Enabled="False" ></asp:textbox>

                    </div>

                    <div class="col-12 gy-2 gx-2">
                          <asp:button ID="cmd_Ultimo_Sorteio" runat="server" Text="Mesmos colaboradores do último sorteio" CssClass="btn" Visible="false" />
                    </div>
                    <div class="col-11 gy-2">
                        <div class="row">
                              <div class="col-3 gy-2 gx-2">
                                  <asp:label id="lblPCMSO4" runat="server" CssClass="tituloLabel">Colaboradores a serem sorteados</asp:label>
                                  <asp:textbox id="txt_Colaboradores" runat="server" Width="70" MaxLength="2" CssClass="texto form-control form-control-sm"></asp:textbox>
                              </div>
                              <div class="col-2 gy-4 gx-2">
                                  <asp:button ID="cmd_Realizar_Sorteio" runat="server" Text="Realizar Sorteio" CssClass="btn" Enabled="False" OnClick="cmd_Realizar_Sorteio_Click1"/>
                              </div>
                            <div class="col-2 gy-4 gx-2">
                                <asp:button ID="cmd_Certificado_Participacao" runat="server" Text="Imprimir Certificado Participação no Sorteio" CssClass="btn" Enabled="False" Visible="False" />
                            </div>
                        </div>
                    </div>
                    <%--Tabela 3 --%>
                    <div class="col-4 gx-3 gy-4">
                         <asp:label id="Label1" runat="server" CssClass="tituloLabel">Sorteados</asp:label>
                             <eo:Grid ID="grd_Sorteados" runat="server" ColumnHeaderAscImage="00050403" 
                            ColumnHeaderDescImage="00050404"
                            FixedColumnCount="1" GridLineColor="240, 240, 240" 
                            GridLines="Both" Height="255px" Width="350px" ColumnHeaderDividerOffset="6" 
                            ColumnHeaderHeight="30" ItemHeight="30" KeyField="Id_Toxicologico_Sorteio_Colaborador"  OnItemCommand="grd_Sorteados_ItemCommand"
                            ClientSideOnItemCommand="OnItemCommand">
                            <itemstyles>
                                <eo:GridItemStyleSet>
                                <ItemStyle CssText="background-color: #FAFAFA;" />
                                <AlternatingItemStyle CssText="background-color: #F2F2F2;" />
                                <SelectedStyle CssText="border-bottom-style:solid;border-left-style:solid;border-right-style:solid;border-top-style:solid;" />
                                    <CellStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: normal; color: #B0ABAB; text-align: left; height: 30px !important; " />
                               </eo:GridItemStyleSet>
                           </itemstyles>
                           <ColumnHeaderTextStyle CssText="" />
                           <ColumnHeaderStyle CssClass="tabelaC colunas" />
                           <Columns>
                                <eo:StaticColumn AllowSort="True" DataField="tNo_Empg" HeaderText="Nome do Colaborador" Name="tNo_Empg" ReadOnly="True" SortOrder="Ascending" Text="" Width="320" >
                                    <CellStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: normal; color: #B0ABAB; text-align: left; height: 30px !important; " />
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
                    </div>   
                    <div clas="col-12 gy-2 gx-2">
                        <div class="row">
                                    <div class="col-3 gy-2 gx-2">
                             <asp:Button ID="cmd_Imprimir_Sorteado" runat="server" Text="Imprimir Certificado Sorteio do Colaborador" CssClass="btn" Enabled="False"  OnClick="cmd_Imprimir_Sorteado_Click" Visible="False" />
                         </div>
                        <div class="col-3 gy-2 gx-2">
                            <asp:Button ID="cmd_Guias" runat="server" Text="Enviar Guias do Exame para colaboradores" CssClass="btn" Enabled="False" />
                        </div>
                        <div class="col-3 gy-2 gx-2">
                            <asp:Button ID="cmd_Imprimir_Guias" runat="server" Text="Imprimir Guia do Colaborador" CssClass="btn" Enabled="False"  OnClick="cmd_Imprimir_Guias_Click" />
                        </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

 <eo:MsgBox ID="MsgBox1" runat="server" BackColor="#47729F" ControlSkinID="None" 
        HeaderHtml="Dialog Title" Height="100px" Width="168px" OnButtonClick="MsgBox1_ButtonClick">
        <HeaderStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #ffbf00 3px solid; padding-left: 4px; font-weight: bold; font-size: 11px; padding-bottom: 2px; color: white; padding-top: 2px; border-bottom: #22456a 1px solid; font-family: verdana" />
        <ContentStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #7d97b6 1px solid; padding-left: 4px; border-left-width: 1px; font-size: 11px; border-left-color: #728eb8; padding-bottom: 4px; color: white; padding-top: 4px; border-bottom: #22456a 1px solid; font-family: verdana" />
        <FooterStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #7d97b6 1px solid; padding-left: 4px; border-left-width: 1px; font-size: 11px; border-left-color: #728eb8; padding-bottom: 4px; color: white; padding-top: 4px; border-bottom: #22456a 1px solid; font-family: verdana" />
    </eo:MsgBox>


</asp:Content>

