<%@ Page Title="Ilitera.Net" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="True"
    CodeBehind="EquipCalibracao_Det.aspx.cs" Inherits="Ilitera.Net.EquipCalibracao_Det" %>

<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>


<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">

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

    <link href="css/forms.css" rel="stylesheet" type="text/css" />
    <link href="css/tabelas.css" rel="stylesheet" type="text/css" />
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
        width: 408px;
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

        

	
        <div class="col-11">
            <div class="row">

                
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
                            <lookitems>
                                <eo:TabItem Height="21" ItemID="_Default"
                                    NormalStyle-CssText="font-family:'Ubuntu'; font-size: 12px; color: #B0ABAB; padding: 10px 10px; background: #F1F1F1; border-radius: 8px; cursor: hand; width: fit-content; margin-right: 1rem;" 
                                    SelectedStyle-CssText="font-family:'Ubuntu'; font-size: 12px; color: #1C9489; font-weight: bold; padding: 10px 15px; background: #D9D9D9; border-radius: 8px; cursor: hand; width: fit-content; margin-right: 1rem;" >
                                    <subgroup itemspacing="1" 
                                        Style-CssText="font-family:'Ubuntu'; font-size: 12px; color: #B0ABAB; padding: 10px 10px; border-radius: 8px; cursor: hand; width: fit-content;">
                                    </subgroup>
                                </eo:TabItem>
                            </lookitems>
                        </eo:TabStrip>
            </div>
        </div>
        
        <eo:MultiPage ID="MultiPage1" runat="server" Height="400px" Width="1050px">
            
            <eo:PageView ID="Pageview1" runat="server" Width="1050px">
        <div class="col-11 subtituloBG"  style="padding-top: 10px">
            <asp:Label ID="Label1" runat="server" CssClass="subtitulo">Equipamento - Calibração</asp:Label>
            <asp:panel id="Panel2" runat="server" Width="662px"></asp:panel>
        </div>
        <div class="col-11">
            <div class="row">
                <div class="col-md-4 gx-3 gy-2">
                    <asp:Label ID="Label3" runat="server" CssClass="tituloLabel form-label mt-2" >Data de Aquisição</asp:Label>
                    <asp:TextBox ID="txtDataAquisicao" runat="server" CssClass="texto form-control form-control-sm mt-1" DisplayModeFormat="dd/MM/yyyy" HorizontalAlign="Center" ImageDirectory=" " Nullable="False"></asp:TextBox>
		        </div>
                <div class="col-md-4 gx-3 gy-2">
                    <asp:Label ID="Label7" runat="server" CssClass="tituloLabel form-label mt-2">Número de Série</asp:Label>
                    <asp:TextBox ID="txtNumeroSerie" runat="server" CssClass="texto form-control form-control-sm mt-1" DisplayModeFormat="dd/MM/yyyy" Nullable="False" ></asp:TextBox>
		        </div>
                <div class="col-md-4 gx-3 gy-2">
                    <asp:Label ID="Label8" runat="server" CssClass="tituloLabel form-label mt-2">Modelo</asp:Label>
                    <asp:TextBox ID="txtModelo" runat="server" CssClass="texto form-control form-control-sm mt-1" DisplayModeFormat="dd/MM/yyyy" HorizontalAlign="Center" ImageDirectory=" " Nullable="False"></asp:TextBox>
		        </div>
            </div>
            <div class="row">
                <div class="col-md-4 gx-3 gy-2 mt-2">
                    <asp:Label ID="Label9" runat="server" CssClass="tituloLabel form-label">Localização</asp:Label>
                    <asp:TextBox ID="txtLocalizacao" runat="server" CssClass="texto form-control form-control-sm mt-1" DisplayModeFormat="dd/MM/yyyy" HorizontalAlign="Center" ImageDirectory=" " MaxLength="100" Nullable="False"></asp:TextBox>
		        </div>
                <div class="col-md-4 gx-3 gy-2">
                    <asp:Label ID="Label13" runat="server" CssClass="tituloLabel form-label">Fabricante</asp:Label>
                    <asp:TextBox ID="txtFabricante" runat="server" CssClass="texto form-control form-control-sm mt-1" DisplayModeFormat="dd/MM/yyyy" HorizontalAlign="Center" ImageDirectory=" " MaxLength="50" Nullable="False" ></asp:TextBox>
		        </div>
                <div class="col-md-4 gx-3 gy-2">
                    <asp:Label ID="Label4" runat="server" CssClass="tituloLabel form-label">Relatório de Aferição</asp:Label>
                    <asp:TextBox ID="txtRelatorioAfericao" runat="server" CssClass="texto form-control form-control-sm mt-1" TextMode="MultiLine" MaxLength="1000"></asp:TextBox>
                    
		        </div>
            </div>
            <div class="row">
                <div class="col-md-4 gx-3 gy-2">
                    <asp:Label ID="Label12" runat="server" CssClass="tituloLabel form-label">Periodicidade de Calibração</asp:Label>
                    <div class="row">
                        <div class="col-2">
                            <asp:TextBox ID="txtPeriodicidade" runat="server" CssClass="texto form-control form-control-sm mt-1" DisplayModeFormat="dd/MM/yyyy" HorizontalAlign="Center" ImageDirectory=" " MaxLength="3" Nullable="False"></asp:TextBox>
                        </div>
                        <div class="col-10 ps-2">
                            <asp:DropDownList ID="cmb_Periodicidade" runat="server" CssClass="texto form-select form-select-sm mt-1">
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
                    <asp:Label ID="Label10" runat="server" CssClass="tituloLabel form-label">Assistência Técnica</asp:Label>
                    <asp:TextBox ID="txtAssistenciaTecnica" runat="server" CssClass="texto form-control form-control-sm mt-1" DisplayModeFormat="dd/MM/yyyy" HorizontalAlign="Center" ImageDirectory=" " MaxLength="50" Nullable="False"></asp:TextBox>
		        </div>
                <div class="col-md-4 gx-3 gy-2">
                    <asp:Label ID="lblAnotacoes" runat="server" CssClass="tituloLabel form-label">Equipamento</asp:Label>
                     <asp:TextBox ID="txtEquipamento" runat="server" CssClass="texto form-control form-control-sm mt-1" TextMode="MultiLine" MaxLength="100"></asp:TextBox>
		        </div>
            </div>
            <div class="row">
                <div class="col-md-4 gx-3 gy-2">
                    <asp:Label ID="Label2" runat="server" CssClass="tituloLabel form-label">Plano de Manutenção Preventiva</asp:Label>
                    <asp:TextBox ID="txtPlanoManutencaoPreventivo" runat="server" CssClass="texto form-control form-control-sm mt-1" TextMode="MultiLine" MaxLength="500"></asp:TextBox>
		        </div>
                <div class="col-md-4 gx-3 gy-2">
                    <asp:Label ID="Label5" runat="server" CssClass="tituloLabel form-label">Tipo de Equipamento</asp:Label>
                    <asp:TextBox ID="txtTipoEquipamento" runat="server" CssClass="texto form-control form-control-sm mt-1" TextMode="MultiLine" MaxLength="50"></asp:TextBox>
		        </div>
                <div class="col-md-4 gx-3 gy-2">
                    <asp:Label ID="Label11" runat="server" CssClass="tituloLabel form-label">Certificado</asp:Label>
                    <asp:TextBox ID="txtCertificado" runat="server" CssClass="texto form-control form-control-sm mt-1" TextMode="MultiLine" MaxLength="100"></asp:TextBox>
		        </div>
            </div>
            <div class="row">
                <div class="col-md-4 gx-3 gy-2">
                    <asp:Label ID="Label15" runat="server" CssClass="tituloLabel form-label">TAG</asp:Label>
                    <asp:TextBox ID="txtTAG" runat="server" CssClass="texto form-control form-control-sm mt-1" TextMode="MultiLine" MaxLength="50"></asp:TextBox>
		        </div>
                <div class="col-md-4 gx-3 gy-2">
                    <asp:Label ID="Label16" runat="server" CssClass="tituloLabel form-label">Faixa de Utilização</asp:Label>
                    <asp:TextBox ID="txtFaixaUtilizacao" runat="server" CssClass="texto form-control form-control-sm mt-1" TextMode="MultiLine" MaxLength="50"></asp:TextBox>
		        </div>
                <div class="col-md-4 gx-3 gy-2">
                    <asp:Label ID="Label17" runat="server" CssClass="tituloLabel form-label">Setor</asp:Label>
                    <asp:TextBox ID="txtSetor" runat="server" CssClass="texto form-control form-control-sm mt-1" MaxLength="50"></asp:TextBox>
		        </div>
            </div>
            <div class="row">
                <div class="col-md-4 gx-3 gy-2">
                    <span class="tituloLabel form-label">Próximo Monitoramento</span><br>
                    <asp:TextBox ID="txtProximoMonitoramento" runat="server" CssClass="texto form-control form-control-sm mt-1" MaxLength="10">01/01/0001</asp:TextBox>
		        </div>
                <div class="col-md-4 gx-3 gy-2">
                    <asp:Label ID="Label19" runat="server" CssClass="tituloLabel form-label">Tipo de Monitoramento</asp:Label>
                    <asp:TextBox ID="txtTipoMonitoramento" runat="server" CssClass="texto form-control form-control-sm mt-1" TextMode="MultiLine" MaxLength="50"></asp:TextBox>
		        </div>
                <div class="col-md-4 gx-3 gy-2">
                    <asp:Label ID="Label20" runat="server" CssClass="tituloLabel form-label">Resultado</asp:Label>
                    <asp:TextBox ID="txtResultado" runat="server" CssClass="texto form-control form-control-sm mt-1" TextMode="MultiLine" MaxLength="50"></asp:TextBox>
		        </div>
            </div>
            <div class="row">
                <eo:Grid ID="grd_Clinicos" runat="server" ClientSideOnItemCommand="OnItemCommand"
                    ColumnHeaderAscImage="00050403" ColumnHeaderDescImage="00050404" 
                    ColumnHeaderDividerOffset="6" ColumnHeaderHeight="30" FixedColumnCount="1" Cssclass="grid mt-4"
                    GridLines="Both" Height="122px" ItemHeight="30"
                    KeyField="IdEquipamento_Calibracao_Manutencao" Width="887px" AllowPaging="True" OnItemCommand="grd_Clinicos_ItemCommand" >
                        <itemstyles>
                            <eo:GridItemStyleSet>
                                <ItemStyle CssText="background-color: #FAFAFA" />
                                <AlternatingItemStyle CssText="background-color: #F2F2F2;" />
                                <SelectedStyle CssText="border-bottom-style:solid;border-left-style:solid;border-right-style:solid;border-top-style:solid;" />
                                <CellStyle CssText="padding: 0.3rem 0.3rem 0.3rem 0.5rem; height: 30px !important;" />
                            </eo:GridItemStyleSet>
                        </itemstyles>
                        <ColumnHeaderTextStyle CssText="" />
                        <ColumnHeaderStyle CssClass="tabelaC colunas"/>
                        <Columns>
                            <eo:StaticColumn AllowSort="True" DataField="IdEquipamento_Calibracao" HeaderText="" Name="IdEquipamento_Calibracao" ReadOnly="True" SortOrder="Ascending" Visible="false" Text="" Width="0">
                                <CellStyle CssText="background-color:#ffffcc;font-family:Arial;font-size:9pt;font-weight:bold;text-align:center;" />
                            </eo:StaticColumn>
                            <eo:StaticColumn AllowSort="True" DataField="Data_Manutencao" HeaderText="Data Manutenção" Name="Data_Manutencao" ReadOnly="True" Width="110" >
                                <CellStyle CssText="font-size:7pt;text-align:center;" />
                            </eo:StaticColumn>
                            <eo:StaticColumn AllowSort="True" DataField="Manutencao_Corretiva" HeaderText="Descrição Manutenção Corretiva" Name="Manutencao_Corretiva" ReadOnly="True" Width="630">
                                <CellStyle CssText="font-size:7pt;text-align:left;" />
                            </eo:StaticColumn>
                            <eo:ButtonColumn ButtonText="Editar" Name="Selecionar" Width="70">
                                <CellStyle CssText="font-family:Tahoma;font-size:7pt;text-align:center" />
                            </eo:ButtonColumn>
                            <eo:ButtonColumn ButtonText="Excluir" Name="Excluir" Width="70">
                                <CellStyle CssText="font-family:Tahoma;font-size:7pt;text-align:center" />
                            </eo:ButtonColumn>
                        </Columns>
                        <columntemplates>
                            <eo:TextBoxColumn>
                                <TextBoxStyle CssText="BORDER-RIGHT: #7f9db9 1px solid; PADDING-RIGHT: 2px; BORDER-TOP: #7f9db9 1px solid; PADDING-LEFT: 2px; FONT-SIZE: 7pt; PADDING-BOTTOM: 1px; MARGIN: 0px; BORDER-LEFT: #7f9db9 1px solid; PADDING-TOP: 2px; BORDER-BOTTOM: #7f9db9 1px solid; FONT-FAMILY: Tahoma" />
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

             <div class="col-md-5 mt-4">
                <div class="row">
                    <div class="col-md-4">
                        <asp:Button ID="cmd_Add_Manutencao" OnClick="cmd_Add_Manutencao_Click" runat="server" CssClass="btn" Text="Inserir Manutenção" />
                    </div>
                    <div class="col-md-4 gx-2">
                        <asp:Button ID="cmd_Cancelar" OnClick="cmd_Cancelar_Click"  runat="server" CssClass="btn" Text="Cancelar" />
                    </div>
                </div>
            </div>
        </div>
                    <asp:Label ID="lbl_Edicao" runat="server" CssClass="boldFont" Font-Underline="True" Visible="False">Inserção / Edição Manutenção Corretiva</asp:Label>
                    <asp:Label ID="lbl_Id" runat="server" CssClass="boldFont" Font-Underline="True" Visible="False">0</asp:Label>
                    <asp:Label ID="lblDataManutencao" runat="server" CssClass="boldFont" Visible="False">Data da Manutenção:</asp:Label>

                    <asp:TextBox ID="txtDataManutencao" runat="server" CssClass="inputBox" DisplayModeFormat="dd/MM/yyyy" HorizontalAlign="Center" ImageDirectory=" " MaxLength="10" Nullable="False" Width="100px" Visible="False"></asp:TextBox>

                    <asp:Label ID="lblManutencao" runat="server" CssClass="boldFont" Visible="False">Descrição da Manutenção Corretiva:</asp:Label>
                    <asp:TextBox ID="txtManutencao" runat="server" Font-Size="X-Small" Height="50px" MaxLength="1000" TextMode="MultiLine" Visible="False" Width="867px" AutoPostBack="True"></asp:TextBox>
                    <asp:Button ID="cmd_Gravar_Manutencao" OnClick="cmd_Gravar_Manutencao_Click"  runat="server" BackColor="#FFCC99" Font-Bold="True" Font-Size="X-Small" Font-Strikeout="False" Text="Gravar Manutenção" Visible="False" />

                   

         </eo:PageView>

        <eo:PageView ID="Pageview2" runat="server" Width="1050px">

            <div class="col-12">
                <div class="row">
                    <div class="col-md-4 gx-3 gy-2">
                        <asp:Label ID="Label6" runat="server" CssClass="tituloLabel">Selecione Arquivo:</asp:Label>
                        <asp:FileUpload ID="File1" runat="server" ClientIDMode="Static" CssClass="texto form-control" />
                    </div>
                    <div class="col-md-3 gx-3 mt-3 pt-1">
                        <asp:Button ID="cmd_Add" OnClick="cmd_Add_Click"  runat="server" CssClass="btn" Style="margin-top: 3px" Text="Adicionar Arquivo" />
                    </div>
                    
                    <div class="col-5"></div>

                    <div class="col-md-6 gx-3 gy-2">
                        <asp:Label ID="Label14" runat="server" CssClass="tituloLabel">Arquivos Adicionados:</asp:Label>
                        <asp:ListBox ID="lst_Arq" runat="server" CssClass="texto form-control form-control-sm" AutoPostBack="True"></asp:ListBox>
                    </div>

                    <div class="col-6"></div>

                    <div class="col-md-5 gx-3 gy-2">
                        <div class="row">
                            <div class="col-md-4 gx-2">
                                <asp:Button ID="cmd_PDF" OnClick="cmd_PDF_Click"  runat="server" CssClass="btn" Text="Abrir PDF" Enabled="False"/>
                            </div>

                            <div class="col-md-4 gx-2">
                                <asp:Button ID="cmd_Imagem" OnClick="cmd_Imagem_Click"  runat="server" CssClass="btn" Text="Visualizar Arquivo" Enabled="False"/>
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

        <div class="col-12">
            <div class="row">
                <div class="col-12 gx-3 gy-2 text-center" style="padding-left: 0px !important;">
                    <div runat="server">
                        <asp:Button ID="btnOK" onclick="btnOK_Click"  runat="server" CssClass="btn" Text="Gravar"/>
                        <asp:Button ID="btnExcluir" onclick="btnExcluir_Click"  runat="server" CssClass="btn" Text="Excluir"/>
                    </div>
                </div>
            </div>
        </div>

        <div class="col-12 mt-4">
            <div class="text-start">
                <asp:Button ID="cmd_Voltar" onclick="cmd_Voltar_Click"  runat="server" CssClass="btn2" Text="Voltar"/>
            </div>
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

            </div>
        </div>



        
         <eo:MsgBox ID="MsgBox1" runat="server" BackColor="#ffffff" ControlSkinID="None" HeaderHtml="Dialog Title" Height="192px" Width="345px" CssClass="card border border-1 p-2 text-center" IconUrl="Images/alerta_icon.svg">
            <HeaderStyleActive CssText="font-family: Ubuntu; font-size: 14px; font-weight: bold; color: #1C9489; text-align: center; padding: 5px;" />
            <ContentStyleActive CssText="font-family: Ubuntu; font-size: 14px; font-weight: bold; color: #7D7B7B; text-align: center; padding: 5px; width: 345px; height: 60px" />
            <FooterStyleActive CssText="width: 345px;" />
        </eo:MsgBox>      
    </asp:Content>