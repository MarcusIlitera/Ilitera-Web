<%@ Page Language="C#"   AutoEventWireup="True"  MasterPageFile="~/Site.Master" CodeBehind="ListaExtintores.aspx.cs"  Inherits="Ilitera.Net.ControleExtintores.ListaExtintores" Title="Ilitera.Net" %>


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
           .buttonBox {}
    </style>

</asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent" >   
    <div class="container-fluid d-flex ms-5 ps-4">
        <div class="row gx-3 gy-2">

    <script language="javascript" src="../scripts/validador.js"></script>
    <script language="javascript">
    
//        function CadastroExtintor(IdExtintor, IdEmpresa, IdUsuario) {
//            addItem(centerWin("CadastroExtintores.aspx?IdExtintores=" + IdExtintor + "&IdEmpresa=" + IdEmpresa + "&IdUsuario=" + IdUsuario, 400, 310, "CadastroExtintores"), "Todos");
//        }

//        function ListaExtintoresMouseOver(gridName, id, objectType) {
//            DataGridMouseOverHandler(gridName, id, objectType, 1);
//        }

//        function ListaExtintoresMouseOut(gridName, id, objectType) {
//            DataGridMouseOutHandler(gridName, id, objectType, 1);
//        }

//        function ListaExtintores_CellClickHandler(gridName, cellId, button) {
//            if (button == 0) {
//                var cell = igtbl_getCellById(cellId);
//                var IdExtintorCell = cell.getPrevCell(true);

//                if (cell.Index == 1)
//                    CadastroExtintor(IdExtintorCell.getValue(), document.getElementById('txtIdEmpresa').value,
//	                    document.getElementById('txtIdUsuario').value);

//                }
//            }


		    var g_itemIndex = -1;
		    var g_cellIndex = -1;


           function OnCellSelected(grid) {
		        var cell = grid.getSelectedCell();

		        grid.raiseItemCommandEvent(cell.getItemIndex(), cell.getColIndex());
		    }


		    function OnContextMenuItemClicked(e, eventInfo) {
		        var grid = eo_GetObject("<%=UltraWebGridListaExtintores.ClientID%>");

		        var item = eventInfo.getItem();
                
		        grid.raiseItemCommandEvent(g_itemIndex, item.getText());
		    }


		  

		    function OnItemCommand(grid, itemIndex, colIndex, commandName) {

		        //grid.raiseItemCommandEvent(itemIndex, commandName);
		        grid.raiseItemCommandEvent(itemIndex, colIndex.toString());
		    }



        		
		</script>

            <div class="col-11 subtituloBG" style="padding-top: 10px">
                <asp:Label id="Label7" runat="server" CssClass="subtitulo">Controle de Extintores</asp:Label>
            </div>

            <div class="col-11">
                <div class="row">

                    <%-- FILTROS --%>

                    <div class="col-md-4 gx-3 gy-2">
                        <asp:Label ID="lblBusca" runat="server" SkinID="BoldFontGray" CssClass="tituloLabel form-label">Palavra-Chave</asp:Label>
                        <asp:TextBox ID="txtChave" runat="server" CssClass="texto form-control form-control-sm"></asp:TextBox> 
                    </div>

                    <div class="col-md-4 gx-3 gy-2">
                        <asp:Label ID="lblBusca1" runat="server" SkinID="BoldFontGray" CssClass="tituloLabel form-label">Tipo de Extintor</asp:Label>
                        <asp:DropDownList ID="ddlTipoExtintor" runat="server" AutoPostBack="True" CssClass="texto form-select form-select-sm"></asp:DropDownList>
                    </div>

                    <div class="col-md-4 gx-3 gy-2">
                        <asp:Label ID="lblBusca2" runat="server" SkinID="BoldFontGray" CssClass="tituloLabel form-label">Condição do Extintor</asp:Label>
                        <asp:RadioButtonList ID="rblCondicao" runat="server" CellPadding="0" 
                            CellSpacing="0" Width="300" RepeatDirection="Horizontal" CssClass="texto form-check-label ms-3 mt-1">
                            <asp:ListItem Value="1">Alocado</asp:ListItem>
                            <asp:ListItem Value="2">Fora de Uso</asp:ListItem>
                            <asp:ListItem Value="3">Reserva</asp:ListItem>
                            <asp:ListItem Selected="True" Value="0">Todas</asp:ListItem>
                        </asp:RadioButtonList>
                    </div>

                    <div class="col-md-2 gx-3 gy-2">
                        <asp:Label ID="Label4" runat="server" SkinID="BoldFontGray" CssClass="tituloLabel form-label">Garantia (Anos)</asp:Label>
                        <asp:textbox ID="wneGarantia" runat="server" DataMode="Int" HorizontalAlign="Left" ImageDirectory=" " CssClass="texto form-control"></asp:textbox>
                    </div>

                    <div class="col-md-4 gx-3 gy-2">
                        <asp:Label ID="Label6" runat="server" SkinID="BoldFontGray" CssClass="tituloLabel form-label">Fabricante</asp:Label>
                        <asp:DropDownList ID="ddlFabricante" runat="server" AutoPostBack="True" CssClass="texto form-select form-select-sm"></asp:DropDownList>
                    </div>

                    <div class="col-md-4 gx-3 gy-2">
                        <asp:Label ID="Label3" runat="server" SkinID="BoldFontGray" CssClass="tituloLabel form-label">Setor</asp:Label>
                        <asp:DropDownList ID="ddlSetor" runat="server" CssClass="texto form-select form-select-sm"></asp:DropDownList>
                    </div>

                    <div class="col-1 gx-3 gy-2 mb-3" style="margin-top: 28px;">
                        <asp:ImageButton ID="btnLocalizar" runat="server" AlternateText="Buscar Extintores" BorderWidth="0px" ImageUrl="Images/search.svg" CssClass="btnMenor" style="padding: .5rem;" />
                    </div>

                </div>
            </div>         
            
            <%-- TABELA --%>
   
            <div class="col-11 gx-3 gy-2">
                <eo:Grid ID="UltraWebGridListaExtintores" runat="server" ColumnHeaderAscImage="00050403" 
                    ColumnHeaderDescImage="00050404" GridLines="Both" Height="218px" Width="1050px" ColumnHeaderDividerOffset="6" 
                    ColumnHeaderHeight="30" ItemHeight="30" KeyField="Id" ClientSideOnContextMenu="ShowContextMenu" 
                    FullRowMode="false" ClientSideOnCellSelected="OnCellSelected" ClientSideOnItemCommand="OnItemCommand" CssClass="grid">
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

                 <eo:StaticColumn HeaderText="Vcto Recarga" AllowSort="True" 
                    DataField="VencimentoRecarga" Name="VencimentoRecarga" ReadOnly="True" 
                    SortOrder="Ascending" Text="" Width="105" >
                    <CellStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500; color: #7D7B7B !important; text-align: left; height: 35px !important; padding: 0.3rem 0.3rem 0.3rem 0.5rem; height: 35px !important;" />
                </eo:StaticColumn>               
                 <eo:StaticColumn HeaderText="Ativo Fixo" AllowSort="True" 
                    DataField="AtivoFixo" Name="AtivoFixo" ReadOnly="True" 
                    Width="125" >
                    <CellStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500; color: #7D7B7B !important; text-align: left; height: 35px !important; padding: 0.3rem 0.3rem 0.3rem 0.5rem; height: 35px !important;" />
                </eo:StaticColumn>
                <eo:StaticColumn HeaderText="Fabricação" AllowSort="True" 
                    DataField="DataFabricacao" Name="DataFabricacao" ReadOnly="True" 
                    Width="105">
                    <CellStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500; color: #7D7B7B !important; text-align: left; height: 35px !important; padding: 0.3rem 0.3rem 0.3rem 0.5rem; height: 35px !important;" />
                </eo:StaticColumn>                
                <eo:StaticColumn HeaderText="Tipo Extintor" AllowSort="True" 
                    DataField="TipoExtintor" Name="TipoExtintor" ReadOnly="True" 
                    Width="180">
                    <CellStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500; color: #7D7B7B !important; text-align: left; height: 35px !important; padding: 0.3rem 0.3rem 0.3rem 0.5rem; height: 35px !important;" />
                </eo:StaticColumn>                        
                <eo:StaticColumn HeaderText="Setor" AllowSort="True" 
                    DataField="Setor" Name="Setor" ReadOnly="True" 
                    Width="185">
                    <CellStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500; color: #7D7B7B !important; text-align: left; height: 35px !important; padding: 0.3rem 0.3rem 0.3rem 0.5rem; height: 35px !important;" />
                </eo:StaticColumn>       

                <eo:ButtonColumn>
                    <ButtonStyle CssText="background-image:url('img/adendo.gif'); padding: 0.3rem 0.3rem 0.3rem 0.5rem; height: 35px !important;" />
                    <CellStyle CssText="background-image:url('img/adendo.gif'); padding: 0.3rem 0.3rem 0.3rem 0.5rem; height: 35px !important;" />
                </eo:ButtonColumn>

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

            <div class="col-12 gx-3 gy-2 mb-3">
                <asp:Button ID="cmd_Novo_Equipamento" runat="server" Text="Novo Equipamento" CssClass="btn" />
                <asp:Button ID="cmd_Listar_Todas" runat="server" Text="Listar Todas" CssClass="btn" />
                <asp:LinkButton ID="lkbListagemExtintores" runat="server" SkinID="BoldLinkButton"><img border="0" src="Images/printer.svg" class="btnMenor" style="padding: .3rem;"></img></asp:LinkButton>
            </div>

            <div class="col-12 gx-3 gy-2">
                <div class="row">
                    <div class="col-12 gx-3 ms-2">
                        <asp:CheckBox ID="chk_Alerta" runat="server" Text="Ativar alerta de vencimento de recarga" AutoPostBack="True" CssClass="texto form-check-label" />
                    </div>

                    <div class="col-md-4 gy-2">
                        <asp:Label ID="lblBusca0" runat="server" SkinID="BoldFontGray" CssClass="tituloLabel form-label">E-Mail para receber Alerta</asp:Label>
                        <asp:TextBox ID="txtEmail" runat="server" Enabled="False" MaxLength="50" CssClass="texto form-control"></asp:TextBox>
                    </div>

                    <div class="col-md-2 gx-3 gy-2">
                        <asp:Label ID="Label8" runat="server" SkinID="BoldFontGray" CssClass="tituloLabel form-label">Dias antecedência</asp:Label>
                        <asp:textbox ID="txt_Dias" runat="server" DataMode="Int" HorizontalAlign="Left" ImageDirectory=" " Enabled="False" MaxLength="3" CssClass="texto form-control"></asp:textbox>
                    </div> 

                    <div class="col-md-2 gx-3" style="margin-top: 27px;">
                        <asp:button id="btnGravarServico" runat="server" CssClass="btn" Text="Gravar Alerta"></asp:button>
                    </div>
                </div>
            </div>               

    <eo:MsgBox ID="MsgBox1" runat="server" BackColor="#47729F" ControlSkinID="None" 
        HeaderHtml="Dialog Title" Height="100px" Width="168px">
        <HeaderStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #ffbf00 3px solid; padding-left: 4px; font-weight: bold; font-size: 11px; padding-bottom: 2px; color: white; padding-top: 2px; border-bottom: #22456a 1px solid; font-family: verdana" />
        <ContentStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #7d97b6 1px solid; padding-left: 4px; border-left-width: 1px; font-size: 11px; border-left-color: #728eb8; padding-bottom: 4px; color: white; padding-top: 4px; border-bottom: #22456a 1px solid; font-family: verdana" />
        <FooterStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #7d97b6 1px solid; padding-left: 4px; border-left-width: 1px; font-size: 11px; border-left-color: #728eb8; padding-bottom: 4px; color: white; padding-top: 4px; border-bottom: #22456a 1px solid; font-family: verdana" />
    </eo:MsgBox>
																<asp:label id="lblError" runat="server" SkinID="ErrorFont"></asp:label>
																<asp:CustomValidator ID="cvdCaracteres" runat="server" 
                                                                ControlToValidate="txtChave" Display="Dynamic"></asp:CustomValidator>
                                                            <input id="txtAuxiliar" runat="server" name="txtAuxiliar" type="hidden">                                                                
                                                            </input>

        </div>
    </div>
</asp:Content>


