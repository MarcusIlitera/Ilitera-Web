<%@ Page Language="C#"   AutoEventWireup="True"  MasterPageFile="~/Site.Master" CodeBehind="ControleEstoque.aspx.cs"  Inherits="Ilitera.Net.ControleEstoque" Title="Ilitera.Net" %>

<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>

    <asp:Content ID="Content1" runat="server" ContentPlaceHolderID="HeadContent">
    <link href="css/forms.css" rel="stylesheet" type="text/css" />
    <link href="css/tabelas.css" rel="stylesheet" type="text/css" />

    <style type="text/css">
    .defaultFont
    {
        width: 627px;
    }
        .table
        {}
    </style>
</asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent" >
    <div class="container-fluid d-flex ms-5 ps-4">
      <div class="row gx-3 gy-2 w-100">




    <script language="javascript">
        function VerificaCampoInt() {
            if (isNaN(document.ControleEstoque.txtQtdEstoque.value) || isNull(document.ControleEstoque.txtQtdEstoque.value)
                || isNaN(document.ControleEstoque.txtQtdEstMax.value) || isNull(document.ControleEstoque.txtQtdEstMax.value)
                || isNaN(document.ControleEstoque.txtQtdEstMin.value) || isNull(document.ControleEstoque.txtQtdEstMin.value)) {
                window.alert("Todos os campos só aceitam valores numéricos!");
                return false;
            }
            else
                return true;
        }




        var g_itemIndex = -1;
        var g_cellIndex = -1;



        function OnContextMenuItemClicked(e, eventInfo) {
            var grid = eo_GetObject("<%=DGridCAAssociado.ClientID%>");

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

        <%-- subtitulo --%>

        <div class="col-11 subtituloBG mb-3" style="padding-top: 10px;">
            <asp:Label ID="lblCadastroEPI" runat="server" SkinID="TitleFont" CssClass="subtitulo">Controle de Estoque de EPI</asp:Label>
        </div>
					
        <div class="col-12">
            <div class="row">
                <div class="col-md-4 gy-4 gx-3">
                    <asp:label id="lblSelecione" runat="server" CssClass="tituloLabel form-label">Selecione o EPI:</asp:label>
                    <asp:dropdownlist id="ddlEPI" AutoPostBack="True" onselectedindexchanged="ddlEPI_SelectedIndexChanged" runat="server" CssClass="texto form-select form-select-sm" ></asp:dropdownlist>
                </div>
            </div>
        </div>

        <div class="col-12">
            <div class="row">
                  <div class="col-md-3 gx-3 gy-2">
                      <asp:label id="Label2" runat="server" CssClass="tituloLabel form-label">Quantidade total em estoque:</asp:label>
                      <asp:label id="lblValorTotalEstoque" runat="server" CssClass="boldFont"></asp:label>
                  </div>
                  
                <div class="col-md-11 gx-3 gy-2">
                     <asp:label id="lblTotRegistros" runat="server" CssClass="texto">Selecione o EPI e clique no número do CA desejado para modificar dados do seu estoque.
                     </asp:label>
                </div>

            </div>
        </div>

          <%--TABELA--%>
		
          <div class="col-12">
              <div class="row">
                  <div class="col-md-3 gx-4 gy-4 mb-3">
                      <eo:Grid ID="DGridCAAssociado" runat="server"
                          ColumnHeaderAscImage="00050403"
                          ColumnHeaderDescImage="00050404" FixedColumnCount="1"
                          GridLines="Both" Height="182px" Width="218px" ColumnHeaderDividerOffset="6"
                          ColumnHeaderHeight="30" ItemHeight="30" KeyField="IdCA"
                          ClientSideOnContextMenu="ShowContextMenu" FullRowMode="False"
                          ClientSideOnCellSelected="OnCellSelected"
                          ClientSideOnItemCommand="OnItemCommand" OnItemCommand="DGridCAAssociado_ItemCommand">
                          <ItemStyles>
                              <eo:GridItemStyleSet>
                                  <ItemStyle CssText="background-color: #FAFAFA;" />
                                  <ItemHoverStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500;  text-align: left; height: 35px !important; background: rgba(28, 148, 137, 0.80); color: #FFFFFF; padding: 0.3rem 0.3rem 0.3rem 0.5rem; " />
                                  <AlternatingItemStyle CssText="background-color: #F2F2F2;" />
                                  <AlternatingItemHoverStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500;  text-align: left; height: 35px !important; background: rgba(28, 148, 137, 0.80); color: #FFFFFF; padding: 0.3rem 0.3rem 0.3rem 0.5rem;" />
                                  <SelectedStyle CssText="border-bottom-style:solid;border-left-style:solid;border-right-style:solid;border-top-style:solid;" />
                                  <CellStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500; color: #7D7B7B !important; text-align: left; height: 35px !important; padding: 0.3rem 0.3rem 0.3rem 0.5rem; height: 35px !important;" />
                              </eo:GridItemStyleSet>
                          </ItemStyles>
                          <ColumnHeaderStyle CssClass="tabelaC colunas" />
                          <Columns>

                              <eo:StaticColumn HeaderText="IdCA" AllowSort="True"
                                  DataField="IdCA" Name="IdCA" ReadOnly="True"
                                  SortOrder="Ascending" Text="" Width="0">
                                  <CellStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500; color: #7D7B7B !important; text-align: left; height: 35px !important; padding: 0.3rem 0.3rem 0.3rem 0.5rem; height: 35px !important;" />
                              </eo:StaticColumn>
                              <eo:StaticColumn HeaderText="CA's associados ao EPI" AllowSort="True"
                                  DataField="NumeroCA" Name="NumeroCA" ReadOnly="True"
                                  Width="180">
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

              <div class="col-md-6 gx-2 gy-4">

                <div class="row">
                  <div class="col-md-4 gx-3 gy-2">
                      <asp:Label ID="Label1" runat="server" CssClass="tituloLabel form-label">CA número</asp:Label>
                      <asp:Label ID="lblValorCA" runat="server" CssClass="texto form-control form-control-sm"></asp:Label>
                  </div>
                </div>

              <div class="row">
                  <div class="col-md-4 gx-3 gy-2">
                      <asp:Label ID="Label3" runat="server" CssClass="tituloLabel form-label">Quantidade em estoque</asp:Label>
                      <asp:TextBox ID="txtQtdEstoque" runat="server" CssClass="texto form-control form-control-sm"></asp:TextBox>
                  </div>
             </div>

             <div class="row">
                  <div class="col-md-4 gx-3 gy-2">
                      <asp:Label ID="Label4" runat="server" CssClass="tituloLabel form-label">Quantidade de estoque máxima</asp:Label>
                      <asp:TextBox ID="txtQtdEstMax" runat="server" CssClass="texto form-control form-control-sm"></asp:TextBox>
                  </div>
            </div>

             <div class="row">
                  <div class="col-md-4 gx-3 gy-2">
                      <asp:Label ID="Label5" runat="server" CssClass="tituloLabel form-label">Quantidade de estoque mínima</asp:Label>
                      <asp:TextBox ID="txtQtdEstMin" runat="server" CssClass="texto form-control form-control-sm"></asp:TextBox>
                  </div>
            </div>


          </div>
            </div>


        <%-- BOTÃO FINAL--%>

        <div class="col-12 gx-2 gy-4">
          <div class="row">
             <div class="text-center gy-4 mb-3 ms-2">
                 <asp:button id="btnGravar" onclick="btnGravar_Click" runat="server" CssClass="btn" Text="Gravar"></asp:button>
             </div>
         </div>
          </div>
                            <asp:Label ID="lbl_Id_Empresa" runat="server" Text="IdEmpresa" Visible="False"></asp:Label>
                            <caption>
                      
                                <asp:Label ID="lbl_Id_Usuario" runat="server" Text="IdUsuario" Visible="False"></asp:Label>
                    </caption>

<%--		</form>
	</body>
</HTML>
--%>

<eo:MsgBox ID="MsgBox1" runat="server" BackColor="#47729F" ControlSkinID="None" 
        HeaderHtml="Dialog Title" Height="100px" Width="168px">
        <HeaderStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #ffbf00 3px solid; padding-left: 4px; font-weight: bold; font-size: 11px; padding-bottom: 2px; color: white; padding-top: 2px; border-bottom: #22456a 1px solid; font-family: verdana" />
        <ContentStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #7d97b6 1px solid; padding-left: 4px; border-left-width: 1px; font-size: 11px; border-left-color: #728eb8; padding-bottom: 4px; color: white; padding-top: 4px; border-bottom: #22456a 1px solid; font-family: verdana" />
        <FooterStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #7d97b6 1px solid; padding-left: 4px; border-left-width: 1px; font-size: 11px; border-left-color: #728eb8; padding-bottom: 4px; color: white; padding-top: 4px; border-bottom: #22456a 1px solid; font-family: verdana" />
    </eo:MsgBox>

 </div>
    </div>
  </asp:Content>