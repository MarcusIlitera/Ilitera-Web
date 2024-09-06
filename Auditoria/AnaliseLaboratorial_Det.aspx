<%@ Page Title="Ilitera.Net" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="True"
    CodeBehind="AnaliseLaboratorial_Det.aspx.cs" Inherits="Ilitera.Net.AnaliseLaboratorial_Det" %>

<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>


<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <link href="css/forms.css" rel="stylesheet" type="text/css" />
    <link href="css/tabelas.css" rel="stylesheet" type="text/css" />

<script type="text/javascript">
    function on_column_gettext(column, item, cellValue) {
        //if (cellValue == 1)
        //    return "Sim";
        //else
        //	return "Nao";
        return cellValue;
    }
    function on_begin_edit(cell) {
        //Get the current cell value
        var v = 0;
        var valor = cell.getValue();

        if (valor == "Sim")
            v = 1;
        else
            v = 0;
        //alert(v);
        //Use index 0 if there is no value
        //if (v == null)
        //  v = 0;

        //Load the value into our drop down box
        var dropDownBox = document.getElementById("grade_dropdown");
        dropDownBox.selectedIndex = v;
    }
    function on_end_edit(cell) {
        //Get the new value from the drop down box
        var dropDownBox = document.getElementById("grade_dropdown");
        var v = dropDownBox.selectedIndex;

        //Use null value if user has not selected a
        //value or selected "-Please Select-"
        //if (v == 0)    
        //  v = null;

        //Return the new value to the grid    
        //return v;
        if (v == 1)
            return "Sim";
        else
            return "Nao";
    }
</script>

    <style type="text/css">
        .defaultFont
    {
        width: 586px;
            height: 20px;
        }
        
        .auto-style2 {
            width: 927px;
            height: 22px;
        }
        
    .auto-style3 {
        width: 450px;
    }
        
        .auto-style4 {
            font: xx-small Verdana;
            color: #44926D;
            width: 923px;
        }
        
        .auto-style5 {
            width: 927px;
        }

         [type="button"], [type="reset"], [type="submit"], button {
            min-width: 120px;
            height: 32px;
            font-family: 'Univia Pro' !important;
            font-style: normal;
            font-weight: normal !important;
            font-size: 12px;
            /*text-align: center;*/
            color: #ffffff !important;
            background: linear-gradient(180deg, #48A79E 54.35%, #1C9489 54.36%);
            border-radius: 5px;
            border: none;
            margin-right: 10px;
            margin-bottom: 5px;
            margin-top: 20px;
        }

            [type="button"]:hover, [type="reset"]:hover, [type="submit"]:hover, button:hover {
                color: #ffffff !important;
                background: linear-gradient(180deg, #F2B988 53.35%, #F09E60 53.36%);
                border-radius: 5px;
            }
        
    </style>
</asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent" >
    <div class="container-fluid d-flex ms-5 ps-4">
        <div class="row gx-3 gy-2">

    <eo:CallbackPanel runat="server" id="CallbackPanel1" Triggers="" Width="851px">
	

		<LINK href="scripts/style.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="scripts/validador.js"></script>
		
  <script language="javascript">
      var g_DatePicker = null;
      function datepicker_loaded(obj) {
          g_DatePicker = obj;
      }
      //Check whether "Back Ordered" column is checked
      function is_back_ordered(item) {

          return item.getCell(1).getValue() == "1";
      }
      //This function is called when user checks/unchecks
      //"back ordered" column
      function end_edit_back_ordered(cell, newValue) {
          //Get the GridItem object
          var item = cell.getItem();

          //Force update the "Available On" cell. Note
          //the code must be delayed with a timer so that
          //the value of the "Back Ordered" cell is first
          //updated
          setTimeout(function () {
              var availOnCell = item.getCell(2);
              availOnCell.refresh();
          }, 10);
          //Accept the new value
          return newValue;
      }
      //This function is called for "Available On" column
      //to translate a cell value to the final HTML to be
      //displayed in the cell. You can put any HTML inside
      //the grid cell even though this function only returns
      //simple text. For example, instead of returning
      //"N/A", you can return something like this:
      // <input type="text" disabled="disabled" style="width:100px" />
      //This would render a disabled textbox inside the Grid cell
      //when no value should be entered for "available On"
      //column. Note that the textbox rendered by this 
      //function, regardless disabled or not, are never
      //used by the user because the actual editing UI is
      //specified by the column's EditorTemplate, which is
      //a DatePicker control for this sample
      function get_avail_on_text(column, item, cellValue) {
          if (!is_back_ordered(item)) {

              if (document.getElementById("<%=lblnreg.ClientID%>").value != "") {
                    document.getElementById("<%=lblnreg.ClientID%>").value = document.getElementById("<%=lblnreg.ClientID%>").value + "|" + item.getCell(5).getValue() + " ";
                }
                return "N/A";
            }
            else {
                if (!cellValue) {
                    document.getElementById("<%=lblreg.ClientID%>").value = document.getElementById("<%=lblreg.ClientID%>").value + "|" + item.getCell(5).getValue() + " ";
                    if (document.getElementById("<%=lblnreg.ClientID%>").value == "") document.getElementById("<%=lblnreg.ClientID%>").value = "|";
                  return "Click to edit";
              }
              else
                  return g_DatePicker.formatDate(cellValue, "MM/dd/yyyy");
          }
      }
      //This function is called when user clicks any cell in 
      //the "Available On" column. 
      function begin_edit_avail_on_date(cell) {
          //Get the item object
          var item = cell.getItem();
          //Do not enter edit mode unless "back ordered" is checked
          if (!is_back_ordered(item))
              return false;
          //Load cell value into the DatePicker control
          var v = cell.getValue();
          g_DatePicker.setSelectedDate(v);
      }
      //This function is called when user leaves edit mode
      //from an "Available On" cell. It returns the DatePicker
      //value to the Grid
      function end_edit_avail_on_date(cell) {
          return g_DatePicker.getSelectedDate();
      }
      function OnItemCommand(grid, itemIndex, colIndex, commandName) {
          //grid.raiseItemCommandEvent(itemIndex, commandName);
          grid.raiseItemCommandEvent(itemIndex, colIndex.toString());
      }
  </script>
<%--	</HEAD>
	<body>
		<form id="FormExameComplementar" method="post" runat="server">--%>



							<eo:TabStrip ID="TabStrip1" runat="server" ControlSkinID="None" 
                            MultiPageID="MultiPage1">
                            <topgroup>
                                <Items>
                                    <eo:TabItem Text-Html="Reunião">
                                    </eo:TabItem>
                                    <eo:TabItem Text-Html="Anexos">
                                    </eo:TabItem>
                                </Items>
                            </topgroup>
                            <LookItems>
                                <eo:TabItem ItemID="_Default"
                                    NormalStyle-CssText="font-family:'Ubuntu'; font-size: 12px; color: #B0ABAB; padding: 10px 10px; background: #F1F1F1; border-radius: 8px; cursor: hand; width: fit-content; margin-right: 1rem;"
                                    SelectedStyle-CssText="font-family:'Ubuntu'; font-size: 12px; color: #1C9489; font-weight: bold; padding: 10px 15px; background: #D9D9D9; border-radius: 8px; cursor: hand; width: fit-content; margin-right: 1rem;">
                                    <SubGroup OverlapDepth="8" ItemSpacing="5"
                                        Style-CssText="font-family:'Ubuntu'; font-size: 12px; color: #B0ABAB; padding: 10px 10px 10px 0px; border-radius: 8px; cursor: hand; width: fit-content;">
                                    </SubGroup>
                                </eo:TabItem>
                            </LookItems>
                        </eo:TabStrip>



             <eo:MultiPage ID="MultiPage1" runat="server" Height="400px" Width="1050px"> <%-- PÁGINA 1: REUNIÃO --%>
            
                  <eo:PageView ID="Pageview1" runat="server" Width="1050px">

                      <div class="col-12 subtituloBG mb-3" style="padding-top: 10px;">
                          <asp:Label ID="Label1" runat="server" CssClass="subtitulo">Análise Laboratorial</asp:Label>
                      </div>

                      <div class="col-12 mb-3">
                          <div class="row">

                              <div class="col-md-4 gx-3 gy-2">
                                  <asp:Label ID="Label3" runat="server" CssClass="tituloLabel">Tipo</asp:Label>
                                  <asp:DropDownList ID="cmb_Tipo" runat="server" CssClass="texto form-select form-select-sm"></asp:DropDownList>
                              </div>

                              <div class="col-md-4 gx-3 gy-2">
                                  <asp:Label ID="Label8" runat="server" CssClass="tituloLabel">Novo Tipo</asp:Label>
                                  <asp:TextBox ID="txtNovoTipo" runat="server" CssClass="texto form-control form-control-sm" DisplayModeFormat="dd/MM/yyyy" HorizontalAlign="Center" ImageDirectory=" " MaxLength="50" Nullable="False"></asp:TextBox>
                              </div>

                              <div class="col-md-4 gx-3 gy-2">
                                  <asp:Label ID="Label12" runat="server" CssClass="tituloLabel">Periodicidade da Análise</asp:Label>
                                  <div class="row">
                                      <div class="col-2">
                                          <asp:TextBox ID="txtPeriodicidade" runat="server" CssClass="texto form-control form-control-sm" DisplayModeFormat="dd/MM/yyyy" HorizontalAlign="Center" ImageDirectory=" " MaxLength="3" Nullable="False"></asp:TextBox>
                                      </div>

                                      <div class="col-10 gx-3">
                                          <asp:DropDownList ID="cmb_Periodicidade" runat="server" CssClass="texto form-select form-select-sm">
                                            <asp:ListItem Value="1">Dia</asp:ListItem>
                                            <asp:ListItem Value="2">Semana</asp:ListItem>
                                            <asp:ListItem Value="3">Mês</asp:ListItem>
                                            <asp:ListItem Value="4">Ano</asp:ListItem>
                                            <asp:ListItem Value="5">Semestral</asp:ListItem>
                                        </asp:DropDownList>
                                      </div>
                                  </div>
                              </div>

                              <div class="col-md-4 gx-3 gy-2">
                                  <asp:Label ID="Label22" runat="server" CssClass="tituloLabel">Manipulador</asp:Label>
                                  <asp:DropDownList ID="cmb_Manipulador" runat="server" CssClass="texto form-select form-select-sm"></asp:DropDownList>
                              </div>

                              <div class="col-md-4 gx-3 gy-2">
                                  <asp:Label ID="Label23" runat="server" CssClass="tituloLabel">Novo Manipulador</asp:Label>
                                  <asp:TextBox ID="txtNovoManipulador" runat="server" CssClass="texto form-control form-control-sm" DisplayModeFormat="dd/MM/yyyy" HorizontalAlign="Center" ImageDirectory=" " MaxLength="50" Nullable="False"></asp:TextBox>
                              </div>

                              <div class="col-md-4 gx-3 gy-2">
                                  <div class="row">
                                      <div class="col-6 gx-2">
                                          <asp:Label ID="Label17" runat="server" CssClass="tituloLabel">Última Análise</asp:Label>
                                          <asp:TextBox ID="txtUltimaAnalise" runat="server" CssClass="texto form-control form-control-sm" MaxLength="10"></asp:TextBox>
                                      </div>

                                      <div class="col-6 gx-2">
                                          <asp:Label ID="Label21" runat="server" CssClass="tituloLabel">Próxima Análise</asp:Label>
                                          <asp:TextBox ID="txtProximaAnalise" runat="server" CssClass="texto form-control form-control-sm" MaxLength="10">01/01/0001</asp:TextBox>
                                      </div>
                                  </div>
                              </div>

                              <div class="col-md-6 gx-3 gy-2">
                                  <asp:Label ID="Label19" runat="server" CssClass="tituloLabel">Obs</asp:Label>
                                  <asp:TextBox ID="txtObs" runat="server" Height="50px" TextMode="MultiLine" CssClass="texto form-control" MaxLength="500"></asp:TextBox>
                              </div>

                              <div class="col-md-6 gx-3 gy-2">
                                  <asp:Label ID="Label20" runat="server" CssClass="tituloLabel">Resultado</asp:Label>
                                   <asp:TextBox ID="txtResultado" runat="server" Height="50px" TextMode="MultiLine" CssClass="texto form-control" MaxLength="500"></asp:TextBox>
                              </div>
                          </div>
                      </div>

                      <asp:DropDownList ID="cmb_IdTipo" runat="server" Font-Size="X-Small" Height="16px" Visible="False" Width="47px"></asp:DropDownList>
                      <asp:DropDownList ID="cmb_IdManipulador" runat="server" Font-Size="X-Small" Height="16px" Visible="False" Width="30px"></asp:DropDownList>



         </eo:PageView>

        <eo:PageView ID="Pageview2" runat="server" Width="1080px">

            <div class="col-12">
                <div class="row">

                    <div class="col-md-4 gx-3 gy-2">
                        <asp:Label ID="Label6" runat="server" CssClass="tituloLabel">Selecione Arquivo</asp:Label>
                        <asp:FileUpload ID="File1" runat="server" CssClass="texto form-control" ClientIDMode="Static"  />
                    </div>

                    <div class="col-md-3 gx-3 mt-4 pt-1">
                        <asp:Button ID="cmd_Add" OnClick="cmd_Add_Click" runat="server" Text="Adicionar Arquivo" CssClass="btn" />
                    </div>

                    <div class="col-5"></div>

                    <div class="col-md-6 gx-3 gy-2">
                        <asp:Label ID="Label14" runat="server" CssClass="tituloLabel">Arquivos Adicionados</asp:Label>
                        <asp:ListBox ID="lst_Arq" OnSelectedIndexChanged="lst_Arq_SelectedIndexChanged" runat="server" Height="228px" AutoPostBack="True" CssClass="texto form-control form-control-sm"></asp:ListBox>
                    </div>

                    <div class="col-6"></div>

                    <div class="col-md-5 gx-3 gy-2">
                        <div class="row">
                            <div class="col-md-4 gx-2">
                                <asp:Button ID="cmd_PDF" OnClick="cmd_PDF_Click" runat="server" CssClass="btn" Text="Abrir PDF" Enabled="False" />
                            </div>

                            <div class="col-md-4 gx-2">
                                <asp:Button ID="cmd_Imagem" OnClick="cmd_Imagem_Click" runat="server" CssClass="btn" Text="Visualizar Arquivo" />
                            </div>

                            <div class="col-md-4 gx-2">
                                <asp:Button ID="cmd_Remove" OnClick="cmd_Remove_Click" runat="server" CssClass="btn" Text="Remover Arquivo" />
                            </div>

                        </div>
                    </div>
                </div>
            </div>
                        

            <asp:TextBox ID="txt_Arq" runat="server" Visible="False"></asp:TextBox>
            <asp:Label ID="lbl_Path" runat="server" Visible="False"></asp:Label>
            <asp:Image ID="ImgFunc" runat="server" BorderColor="#660033" BorderStyle="Inset" BorderWidth="2px" Height="545px" Visible="False" Width="428px" />
       </eo:PageView>

       </eo:MultiPage>

        <div class="col-12 mb-3">
            <div class="row text-center">
                <div>
                    <asp:Button ID="btnOK" onclick="btnOK_Click"  runat="server" CssClass="btn" Text="Gravar" />
                    <asp:Button ID="btnExcluir" onclick="btnExcluir_Click"  runat="server" CssClass="btn" Text="Excluir"/>
                </div>
            </div>
        </div>

        <div class="col-12 text-start"> 
            <asp:Button ID="cmd_Voltar" onclick="cmd_Voltar_Click"  runat="server" CssClass="btn2" Text="Voltar" />
        </div>

                              
                  <asp:TextBox ID="lblnreg" runat="server" Width="0" Visible="False" ></asp:TextBox>
                  <asp:TextBox ID="lblreg" runat="server" Width="0" Visible="False" ></asp:TextBox>

            <input id="txtAuxiliar" runat="server" name="txtAuxiliar" type="hidden">
                <%--		</form>
	</body>
</HTML>
--%>
            </input>
                <br>
                </eo:CallbackPanel>
        
         <eo:MsgBox ID="MsgBox1" runat="server" BackColor="#ffffff" ControlSkinID="None" HeaderHtml="Dialog Title" Height="192px" Width="345px" CssClass="card border border-1 p-2 text-center" IconUrl="Images/alerta_icon.svg">
            <HeaderStyleActive CssText="font-family: Ubuntu; font-size: 14px; font-weight: bold; color: #1C9489; text-align: center; padding: 5px;" />
            <ContentStyleActive CssText="font-family: Ubuntu; font-size: 14px; font-weight: bold; color: #7D7B7B; text-align: center; padding: 5px; width: 345px; height: 60px" />
            <FooterStyleActive CssText="width: 345px;" />
        </eo:MsgBox>
            
            </div>
        </div>
    </asp:Content>
