<%@ Page Title="Ilitera.Net" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="True"
    CodeBehind="ListaEmpregados.aspx.cs" Inherits="Ilitera.Net.ListaEmpregados" %>


<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <title>Listagem de Colaboradores</title>
    <link href="css/tabelas.css" rel="stylesheet" type="text/css" />
    <link href="css/forms.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
 
        /* fontes */
        @font-face{
            font-family: "Univia Pro";
            src: url("css/UniviaPro-Regular.otf");
        }
        @font-face{
            font-family: "Ubuntu";
            src: url("css/Ubuntu-Regular.ttf");
        }

        /* config. específicas */
        #ctl00_MainContent_grd_Empregados_c0_sort, ctl00_MainContent_grd_Empregados_c1_sort {
            width: 12px;
            opacity: 35%;
            padding-top: 0.3rem;
}

        .form-select {
            background-image: url("data:image/svg+xml,%3csvg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 16 16'%3e%3cpath fill='none' stroke='%23343a40' stroke-linecap='round' stroke-linejoin='round' stroke-width='2' d='m2 5 6 6 6-6'/%3e%3c/svg%3e") !important;
            background-repeat: no-repeat !important;
            background-position: right 0.75rem center !important;
            background-size: 16px 12px !important;
        }
        /* títulos e textos */
        .tituloConteudo {
            font-family: 'Univia Pro';
            font-size: 16px;
            font-weight: 500;
            color: #1C9489;
            text-align: left;
        }
        .tituloLabel {
            font-family: 'Univia Pro';
            font-size: 12px;
            font-weight: 400;
            color: #1C9489;
        }
        .label {
            font-family: 'Ubuntu';
            font-weight: 400;
            color: #1C9489;           
        }
        .subtitulo {
            font-family: 'Univia Pro';
            color: #1C9489;
            font-size: 16px;
            font-weight: 500;
            font-variant: normal;
            margin-bottom: 0 !important;
            margin-top: .8rem;
            margin-left: 50px;
        }
        .subtituloBG {
            background-color: #F1F1F1;
            border-radius: 5px;
            height: 45px;
        }
        /* input */
        .input {
            
            background: #FFFFFF;
            border: 1px solid #B0ABAB;
            border-radius: 4px;
            font-family: 'Ubuntu';
            font-style: normal;
            font-weight: 500;
            font-size: 12px;
            color: #B0ABAB;
            height: 28px;
        }
        /* botões */
        .btn {
            /*margin-left: 5px;
            font: 800 14px "Inter";
            color: white;
            padding: 8px;
            background-color: #5865f2;
            border-radius: 4px;
            border-color: #5865f2;*/
            width: 260px;
            height: 32px;
            font-family: 'Univia Pro';
            font-style: normal;
            font-weight: normal;
            font-size: 14px;
            /*text-align: center;*/
            color: #ffffff;
            background: linear-gradient(180deg, #48A79E 54.35%, #1C9489 54.36%);
            border-radius: 5px;
            width: 260px;
            height: 32px;
        }
            .btn:hover {
                color: #ffffff;
                background: linear-gradient(180deg, #F2B988 53.35%, #F09E60 53.36%);
                border-radius: 5px;
            }
        .btn2 {
            /*margin-left: 5px;
            font: 800 14px "Inter";
            color: white;
            padding: 8px;
            background-color: #5865f2;
            border-radius: 4px;
            border-color: #5865f2;*/
            width: 260px;
            height: 32px;
            font-family: 'Univia Pro';
            font-style: normal;
            font-weight: normal;
            font-size: 12px;
            /*text-align: center;*/
            color: #ffffff;
            background: linear-gradient(180deg, #48A79E 54.35%, #1C9489 54.36%);
            border-radius: 5px;
            border: none;
            /*margin: 10px;*/
            width: 80px;
            height: 25px;
        }
            .btn2:hover {
                color: #ffffff;
                background: linear-gradient(180deg, #F2B988 53.35%, #F09E60 53.36%);
                border-radius: 5px;
                border: none;
            }
            
        /* alinhamento */
        /* select */
        .wrapperSelect{
            display: flex;
            justify-content: center;
            align-items: center;
        }
        .select{
            max-width: 165px;
            height: 30px;
            font-family: 'Ubuntu';
            font-style: normal;
            font-weight: 500;
            color: #B0ABAB;
            border: 1px solid #B0ABAB;
            border-radius: 4px;
            outline: none;
        }
        /* botões do filtro */
        .labelDadosOpcao {
            color: #1C9489;
            font-size: 12px;
            font-family: 'Ubuntu';
            font-weight: 500;
        }  
        .dadoDadosOpcao {
            color: #B0ABAB;
            font-size: 12px;
            font-family: 'Ubuntu';
            font-weight: 500;
        }  
        .dados {
            background: #FFFFFF;
            border: 1px solid #B0ABAB;
            border-radius: 4px;
        }
        
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
        .style3
        {
            width: 546px;
            border: 2px solid #000000;
            background-color: #E8E8E8;
        }
        .style4
        {
            width: 158px;
        }
        .style5
        {
            width: 149px;
        }
        .style6
        {
            font-size: small;
            font-family: Arial;
        } 
        </style>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">


<script type="text/javascript">
    var g_itemIndex = -1;
    var g_cellIndex = -1;
    function ShowContextMenu(e, grid, item, cell) {
        //Save the target cell index
        g_itemIndex = item.getIndex();
        g_cellIndex = cell.getColIndex();
        //Show the context menu
        var menu = eo_GetObject("<%=Menu1.ClientID%>");
        eo_ShowContextMenu(e, "<%=Menu1.ClientID%>");
        //Return true to indicate that we have
        //displayed a context menu
        return true;
    }
    function OnContextMenuItemClicked(e, eventInfo) {
        var grid = eo_GetObject("<%=grd_Empregados.ClientID%>");
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

    <div runat="server" class="row align-items-center">

        <%-- FILTROS --%>    

        <div class="container ms-5 ps-4" style="width: 100vw;">
            <div class="row gy-2 gx-2">   
                    <div class="col-md-4 gx-2 gy-2">
                            <asp:Label ID="Label2" runat="server" 
                                Text="Nome Empregado" CssClass="tituloLabel form-label col-form-label col-form-label-sm"></asp:Label>                        
                            <asp:TextBox ID="txt_Nome" runat="server" AutoPostBack="True" 
                                CausesValidation="True" Font-Size="XX-Small" CssClass="input form-control form-control-sm"></asp:TextBox>
                    </div>
                    <div class="col-md-4">
                            <asp:Label ID="Label3" runat="server" 
                                Text="Setor" CssClass="tituloLabel form-label col-form-label col-form-label-sm"></asp:Label>
                            <asp:DropDownList ID="cmb_Setor" runat="server" AutoPostBack="True" CausesValidation="True" CssClass="input form-select form-select-sm">
                                </asp:DropDownList>
                    </div>
                    <div class="col-md-4 mt-4">
                        <div class="col-md-12 ml-2">                        
                            <fieldset>
                                <div class="form-check form-check-inline ms-3 mt-2">
                                    <asp:RadioButton ID="rd_Todos" runat="server" GroupName="Func" Text="Todos" CssClass="tituloLabel" oncheckedchanged="rd_Todos_CheckedChanged" 
                                        AutoPostBack="True"/>
                                </div>
                                <div class="form-check form-check-inline mt-2">
                                    <asp:RadioButton ID="rd_Ativos" runat="server" GroupName="Func" Text="Ativos" CssClass="tituloLabel" oncheckedchanged="rd_Ativos_CheckedChanged" 
                                        AutoPostBack="True" Checked="True"/>
                                </div>
                                <div class="form-check form-check-inline mt-2">
                                    <asp:RadioButton ID="rd_Inativos" runat="server" GroupName="Func" Text="Inativos" CssClass="tituloLabel" oncheckedchanged="rd_Inativos_CheckedChanged" 
                                        AutoPostBack="True"/>
                                </div>
                                                                
                                <%--<div class="form-check form-check-inline">
                                    <input class="form-check-input" type="radio" name="inlineRadioOptions" id="rd1_Todos" value="option1" />
                                    <label class="opcoes form-check-label" for="inlineRadio1">Todos</label>
                                </div>
                                <div class="form-check form-check-inline">
                                    <input class="form-check-input" type="radio" name="inlineRadioOptions" id="rd1_Ativos" value="option2" />
                                    <label class="opcoes form-check-label" for="inlineRadio2">Ativos</label>
                                </div>
                                <div class="form-check form-check-inline">
                                    <input class="form-check-input" type="radio" name="inlineRadioOptions" id="rd1_Inativos" value="option3" />
                                    <label class="opcoes form-check-label" for="inlineRadio3">Inativos</label>
                                </div>--%>
                            </fieldset>
                        </div>
                    </div>

                <div class="row mt-3 mb-3" style="padding-left: 5px !important;">
                    <asp:Button ID="cmd_Busca" runat="server" Text="Buscar"  onclick="cmd_Busca_Click" 
                        UseSubmitBehavior="False" CssClass="btn2"/>
                </div>
                </div>
            </div>
        <eo:CallbackPanel ID="CallbackPanel1" runat="server" Height="230px" Width="159px"
        Triggers="{ControlID:Menu1;Parameter:}" AutoDisableContents="True" SafeGuardUpdate="False">

        <asp:TextBox ID="txt_Matricula" runat="server" AutoPostBack="True" BackColor="#CCCCCC" CausesValidation="True" Font-Size="XX-Small" Height="18px" MaxLength="30" Visible="False" Width="100px" ontextchanged="txt_Nome_TextChanged" ></asp:TextBox>

        <%--<asp:Button ID="Button2" runat="server" Text="Cancelar" 
            UseSubmitBehavior="False" CssClass="btn2"/>--%>

         <%-- TABELA --%>

    <div class="container-fluid d-flex ms-5 ps-4" style="width:100vw">
        <div class="row w-100">
            <%-- TABELA 1 --%>
            <div class="col-12 col-md-5 gx-3 gy-2 ps-1">
                 <eo:Grid ID="grd_Empregados" runat="server"  ColumnHeaderDividerOffset="6" 
                    ColumnHeaderAscImage="00050403" ColumnHeaderDescImage="00050404"
                    GridLines="Both" PageSize="500" KeyField="IdEmpregado" CssClass="grid"
                    ColumnHeaderHeight="30" ItemHeight="30" Height="400px" Width="480px"
            ClientSideOnContextMenu="ShowContextMenu" FullRowMode="False"
        OnItemCommand="grd_Empregados_ItemCommand"  ClientSideOnCellSelected="OnCellSelected"
        ClientSideOnItemCommand="OnItemCommand"  >        

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
                <eo:StaticColumn HeaderText="Nome Empregado" AllowSort="True" 
                    DataField="NomeEmpregado" Name="NomeEmpregado" ReadOnly="True" 
                    SortOrder="Ascending" Text="" Width="330" >
                    <CellStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500; color: #7D7B7B; text-align: left; height: 30px !important; " />
                </eo:StaticColumn>
                <eo:StaticColumn HeaderText="Data de Admissão" AllowSort="True" 
                    DataField="DataAdmissao" Name="Data de Admissao" ReadOnly="True" 
                    Width="130" DataFormat="{0:dd/MM/yyyy}" DataType="DateTime">
                    <CellStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500; color: #7D7B7B; text-align: left; height: 30px !important; "/>
                </eo:StaticColumn>
                <%--<eo:ButtonColumn ButtonText="Selecionar Visible="false" 
                    Name="Selecionar" Width="80">
                    <CellStyle CssText="font-family:Tahoma;font-size:7pt;text-align:center" />
                </eo:ButtonColumn>
                <eo:ButtonColumn ButtonText="Dados Cadastrais" Name="Cadastro" Width="80" Visible="false">
                    <CellStyle CssText="font-family:Tahoma;font-size:7pt;text-align:center" />
                </eo:ButtonColumn>
                <eo:ButtonColumn ButtonText="PPP" Name="PPP" Width="40" Visible="false">
                    <CellStyle CssText="font-family:Tahoma;font-size:7pt;text-align:center" />
                </eo:ButtonColumn>
                <eo:ButtonColumn ButtonText="Guia Encaminhamento" Width="120" Visible="false">
                    <CellStyle CssText="font-family:Tahoma;font-size:7pt;text-align:center" />
                </eo:ButtonColumn>
                <eo:ButtonColumn ButtonText="Clínicos" Width="60" Visible="false">
                    <CellStyle CssText="font-family:Tahoma;font-size:7pt;text-align:center" />
                </eo:ButtonColumn>
                <eo:ButtonColumn ButtonText="Complementares" Width="80" Visible="false">
                    <CellStyle CssText="font-family:Tahoma;font-size:7pt;text-align:center" />
                </eo:ButtonColumn>
                <eo:ButtonColumn ButtonText="Audiométricos" Width="80" Visible="false">
                    <CellStyle CssText="font-family:Tahoma;font-size:7pt;text-align:center" />
                </eo:ButtonColumn>
                <eo:ButtonColumn ButtonText="Ambulatoriais" Width="80" Visible="false">
                    <CellStyle CssText="font-family:Tahoma;font-size:7pt;text-align:center" />
                </eo:ButtonColumn>
                <eo:ButtonColumn ButtonText="PCI" Width="40" Visible="false">
                    <CellStyle CssText="font-family:Tahoma;font-size:7pt;text-align:center" />
                </eo:ButtonColumn>--%>
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
                    <MaskedEdit ControlSkinID="None" TextBoxStyle-CssText="BORDER-RIGHT: #7f9db9 1px solid; PADDING-RIGHT: 2px; BORDER-TOP: #7f9db9 1px solid; PADDING-LEFT: 2px; PADDING-BOTTOM: 1px; MARGIN: 0px; BORDER-LEFT: #7f9db9 1px solid; PADDING-TOP: 2px; BORDER-BOTTOM: #7f9db9 1px solid; font-family:Courier New;font-size:8pt;">
                    </MaskedEdit>
                </eo:MaskedEditColumn>
            </ColumnTemplates>
            <FooterStyle CssText="padding-bottom:4px;padding-left:4px;padding-right:4px;padding-top:4px;" />
        </eo:Grid>
            </div>
            <%-- TABELA 2 --%>
            <div id="DadosColaborador" class="col-md-5 pt-3">
                <div class="dados row p-2">  
                <div class="col-5">
                    <div class="col flex-column d-flex align-items-start mb-2">
                       <asp:label id="lblNome" runat="server" CssClass="labelDadosOpcao" Font-Bold="True">Nome</asp:label>
                       <asp:label id="lblValorNome" runat="server"></asp:label>
                    </div>
                    <div class="col flex-column d-flex align-items-start mb-2">
                          <asp:label id="lblIdade" runat="server" CssClass="labelDadosOpcao" 
                                Font-Bold="True">Idade</asp:label>
                          <asp:label id="lblValorIdade" runat="server" ></asp:label>
                    </div>
                    <div class="col flex-column d-flex align-items-start mb-2">
                           <asp:label id="lblAdmissao" runat="server" CssClass="labelDadosOpcao" 
                                Font-Bold="True">Admissão</asp:label>
                           <asp:label id="lblValorAdmissao" runat="server" ></asp:label>
                    </div>
                    <div class="col flex-column d-flex align-items-start mb-2">
                            <asp:label id="lblGHE" runat="server" CssClass="labelDadosOpcao" 
                                Font-Bold="True">Tempo de Empresa</asp:label>
                            <asp:label id="lblValorTempoEmpresa" runat="server" ></asp:label>
                    </div>
                  
               </div>

                    <%-- COL 2 --%>

                <div class="col-4">
                    
                    <div class="col flex-column d-flex align-items-start mb-2">
                        <asp:label id="lblTipoBene" runat="server" CssClass="labelDadosOpcao"  
                                Font-Bold="True">Tipo de Beneficiário</asp:label>
                        <asp:label id="lblValorBene" runat="server" ></asp:label>
                    </div>
                    <div class="col flex-column d-flex align-items-start mb-2">
                            <asp:label id="lblSexo" runat="server" CssClass="labelDadosOpcao" 
                                Font-Bold="True">Sexo</asp:label>
                            <asp:label id="lblValorSexo" runat="server" ></asp:label>
                    </div>
                    <div class="col flex-column d-flex align-items-start mb-2">
                            <asp:label id="lblDataIni" runat="server" CssClass="labelDadosOpcao" 
                                Font-Bold="True">Início Função</asp:label>
                            <asp:label id="lblValorDataIni" runat="server" ></asp:label>
                    </div>
                    <div class="col flex-column d-flex align-items-start mb-2">
                          <asp:label id="lblRegRev" runat="server" CssClass="labelDadosOpcao" 
                                Font-Bold="True" >Regime Revezamento</asp:label>
                          <asp:label id="lblValorRegRev" runat="server" Width="93px" ></asp:label> 
                    </div>

                    
                    
                </div>

                    <%-- COL 3 --%>

                <div class="col-3">
                    <div class="col flex-column d-flex align-items-start mb-2">
                          <asp:label id="lblDataNascimento" runat="server" CssClass="labelDadosOpcao" 
                                Font-Bold="True">Nascimento</asp:label>
                          <asp:label id="lblValorNasc" runat="server" ></asp:label>
                    </div>
                    <div class="col flex-column d-flex align-items-start mb-2">
                           <asp:label id="lblRegistro" runat="server" CssClass="labelDadosOpcao"   
                                Font-Bold="True">RE</asp:label>
					      <asp:label id="lblValorRegistro" runat="server" ></asp:label>
                    </div>
                    <div class="col flex-column d-flex align-items-start mb-2">
                            <asp:label id="lblDemissao" runat="server" CssClass="labelDadosOpcao" 
                                Font-Bold="True">Demissão</asp:label>
                            <asp:label id="lblValorDemissao" runat="server" ></asp:label>
                    </div>
                </div>

                    <%-- ÚLTIMA LINHA --%>

                <div class="col-12">
                    <div class="col flex-column d-flex align-items-start mb-2">
                           <asp:label id="lblFuncao" runat="server" CssClass="labelDadosOpcao" 
                                Font-Bold="True">Função</asp:label>
                           <asp:label id="lblValorFuncao" runat="server" ></asp:label>
                    </div>
                    <div class="col flex-column d-flex align-items-start mb-2">
                            <asp:label id="lblSetor" runat="server" CssClass="labelDadosOpcao"  
                                Font-Bold="True">Setor</asp:label>
                            <asp:label id="lblValorSetor" runat="server" ></asp:label>
                    </div>
                    <div class="col flex-column d-flex align-items-start mb-2">
                           <asp:label id="Label4" runat="server" CssClass="labelDadosOpcao" 
                                Font-Bold="True">GHE</asp:label>
                            <asp:label id="lblGHE2" runat="server" ></asp:label>                           
                    </div>
                </div>
            </div>
            </div>
            </div>
        </div>
   
        

  </eo:CallbackPanel>

  <eo:ContextMenu ID="Menu1" Width="144px" runat="server" ControlSkinID="None" ClientSideOnItemClick="OnContextMenuItemClicked">
        <TopGroup Style-CssText="cursor:hand;font-family:'Univia Pro';font-size:12px;color:#7D7B7B;background-color:rgba(241, 241, 241, 0.93);border-radius:4px;border: 1px solid rgba(241, 241, 241, 0.93);">
            <Items>
<%--                <eo:MenuItem RaisesServerEvent="True" Text-Html="Selecionar">
                </eo:MenuItem>
--%>                
                <eo:MenuItem RaisesServerEvent="True" Text-Html="Dados Cadastrais" 
                SelectedStyle-CssText="">
                </eo:MenuItem>
                <eo:MenuItem RaisesServerEvent="True" Text-Html="Readmissão" 
                SelectedStyle-CssText="">
                </eo:MenuItem>
                <eo:MenuItem IsSeparator="True">
                </eo:MenuItem>
                <eo:MenuItem RaisesServerEvent="True" Text-Html="PPP">
                </eo:MenuItem>
                <eo:MenuItem RaisesServerEvent="True" Text-Html="LTCAT">
                </eo:MenuItem>
                <eo:MenuItem RaisesServerEvent="True" Text-Html="Laudo de Insalubridade/Periculosidade">
                </eo:MenuItem>
                <eo:MenuItem IsSeparator="True">
                </eo:MenuItem>
                  <eo:MenuItem RaisesServerEvent="True" Text-Html="Clínicos">
                </eo:MenuItem>
                <eo:MenuItem RaisesServerEvent="True" Text-Html="Complementares">
                </eo:MenuItem>
                <eo:MenuItem RaisesServerEvent="True" Text-Html="Audiométricos">
                </eo:MenuItem>
                <eo:MenuItem RaisesServerEvent="True" Text-Html="Ambulatoriais">
                </eo:MenuItem>
                <eo:MenuItem RaisesServerEvent="True" Text-Html="Ficha Clínica">
                </eo:MenuItem>               
                <eo:MenuItem RaisesServerEvent="True" Text-Html="Informações Médicas">
                </eo:MenuItem>               
                <eo:MenuItem RaisesServerEvent="True" Text-Html="Entrevista">
                </eo:MenuItem>               
                <eo:MenuItem IsSeparator="True">
                </eo:MenuItem>
                <eo:MenuItem RaisesServerEvent="True" Text-Html="Vacinação">
                </eo:MenuItem>
                <eo:MenuItem IsSeparator="True">
                </eo:MenuItem>
                <eo:MenuItem RaisesServerEvent="True" Text-Html="Guia de Encaminhamento">
                </eo:MenuItem>
                <eo:MenuItem IsSeparator="True">
                </eo:MenuItem>
                <eo:MenuItem RaisesServerEvent="True" Text-Html="Guias de Encaminhamento Separadas">
                </eo:MenuItem>
                <eo:MenuItem IsSeparator="True">
                </eo:MenuItem>
                <eo:MenuItem RaisesServerEvent="True" Text-Html="Cancelamento de Guia/ASO">
                </eo:MenuItem>
                <eo:MenuItem IsSeparator="True">
                </eo:MenuItem>
                <eo:MenuItem RaisesServerEvent="True" Text-Html="Riscos e EPI">
                </eo:MenuItem>
                <eo:MenuItem RaisesServerEvent="True" Text-Html="Prontuários Digitais">
                </eo:MenuItem>
                <eo:MenuItem IsSeparator="True">
                </eo:MenuItem>
                <eo:MenuItem RaisesServerEvent="True" Text-Html="Configuração Individual de CA">
                </eo:MenuItem>
                <eo:MenuItem IsSeparator="True">
                </eo:MenuItem>
                <eo:MenuItem RaisesServerEvent="True" Text-Html="CAT – Comunicação de Acidente de Trabalho">
                </eo:MenuItem>
                <eo:MenuItem RaisesServerEvent="True" Text-Html="Absenteísmo">
                </eo:MenuItem>
                <eo:MenuItem IsSeparator="True">
                </eo:MenuItem>
                <eo:MenuItem RaisesServerEvent="True" Text-Html="Ordem de Serviço">
                </eo:MenuItem>
            </Items>
        </TopGroup>
        <LookItems>
            <eo:MenuItem IsSeparator="True" ItemID="_Separator" NormalStyle-CssText="background-color:#E0E0E0;height:1px;width:1px;">
            </eo:MenuItem>
            <eo:MenuItem HoverStyle-CssText="color:#F09E60;font-weight:bold;padding-left:5px;padding-right:5px;"
                ItemID="_Default" 
                NormalStyle-CssText="padding-left:5px;padding-right:5px;" 
                SelectedExpandedStyle-CssText="" SelectedHoverStyle-CssText="" 
                SelectedStyle-CssText="">
                <SubMenu ExpandEffect-Type="GlideTopToBottom" Style-CssText="padding-right: 3px; padding-left: 3px; padding-bottom: 3px; padding-top: 3px; cursor:hand;font-family:'Univia Pro';font-size:12px;color:rgba(176, 171, 171, 0.69);background-color:rgba(241, 241, 241, 0.93);border-radius:4px;border: 1px solid rgba(241, 241, 241, 0.93);"
                    CollapseEffect-Type="GlideTopToBottom" OffsetX="3" ShadowDepth="0" OffsetY="-4"
                    ItemSpacing="5">
                </SubMenu>
            </eo:MenuItem>
            </LookItems>
            </eo:ContextMenu>
 </div>    

          

    <eo:MsgBox ID="MsgBox1" runat="server" BackColor="#47729F" ControlSkinID="None" 
        HeaderHtml="Dialog Title" Height="100px" Width="168px">
        <HeaderStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #ffbf00 3px solid; padding-left: 4px; font-weight: bold; font-size: 11px; padding-bottom: 2px; color: white; padding-top: 2px; border-bottom: #22456a 1px solid; font-family: verdana" />
        <ContentStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #7d97b6 1px solid; padding-left: 4px; border-left-width: 1px; font-size: 11px; border-left-color: #728eb8; padding-bottom: 4px; color: white; padding-top: 4px; border-bottom: #22456a 1px solid; font-family: verdana" />
        <FooterStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #7d97b6 1px solid; padding-left: 4px; border-left-width: 1px; font-size: 11px; border-left-color: #728eb8; padding-bottom: 4px; color: white; padding-top: 4px; border-bottom: #22456a 1px solid; font-family: verdana" />
    </eo:MsgBox>
            
 


&nbsp;<asp:ListBox ID="lst_Id_Setor" runat="server" Height="25px" Visible="False" 
                                                    Width="52px"></asp:ListBox>
                                                        
 
        </div>
   </div>

</asp:Content>