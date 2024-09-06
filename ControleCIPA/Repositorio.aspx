<%@ Page Language="C#"   AutoEventWireup="True"  MasterPageFile="~/Site.Master" CodeBehind="Repositorio.aspx.cs"  Inherits="Ilitera.Net.Repositorio" Title="Ilitera.Net" %>


<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>


<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <link href="css/forms.css" rel="stylesheet" type="text/css" />
    <link href="css/tabelas.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
    .defaultFont
    {
        width: 627px;
    }
        #Table4
        {
            width: 594px;
        }
        #Table15
        {
            width: 214px;
        }
        #Table20
        {
            width: 184px;
        }
        #Table24
        {
            width: 333px;
        }
        #Table27
        {
            width: 583px;
        }
        .auto-style1 {
            width: 892px;
        }
        .auto-style2 {
            width: 57px;
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

    <div class="container-fluid d-flex ms-5 ps-4">
        <div class="row gx-3 gy-2">


                    <div class="col-11 subtituloBG" style="padding-top: 10px">
                        <asp:Label ID="lblNome" runat="server" CssClass="subtitulo">Repositório de Documentos/Laudos</asp:Label>
                    </div>


                <div class="col-11">
                <div class="row">

                    <div class="col-3 gx-3 gy-2">
                        <div class="row">
                            <div class="col-8 gx-3 gy-2">
                                <asp:Label ID="Label_Arq" runat="server" CssClass="tituloLabel" Text="Tipo"></asp:Label>                     
                                <asp:DropDownList ID="DropDownList1" runat="server" CssClass="texto form-select form-select-sm">
                                    <asp:ListItem Value="b">ASO</asp:ListItem>
                                    <asp:ListItem Value="y">Banco de Dados</asp:ListItem>
                                    <asp:ListItem Value="v">Vaso de Pressão</asp:ListItem>
                                    <asp:ListItem Value="c">Caldeira</asp:ListItem>
                                    <asp:ListItem Value="j">Covid</asp:ListItem>
                                    <asp:ListItem Value="g">Guia Encaminhamento</asp:ListItem>
                                    <asp:ListItem Value="i">Insalubridade</asp:ListItem>
                                    <asp:ListItem Value="e">Laudo Elétrico</asp:ListItem>
                                    <asp:ListItem Value="1">Laudo Ergonômico</asp:ListItem>
                                    <asp:ListItem Value="h">Lista Campanhas</asp:ListItem>
                                    <asp:ListItem Value="l">LTCAT</asp:ListItem>
                                    <asp:ListItem Value="z">PCA</asp:ListItem>
                                    <asp:ListItem Value="m">PCMSO</asp:ListItem>
                                    <asp:ListItem Value="p">Periculosidade</asp:ListItem>
                                    <asp:ListItem Value="w">PGR</asp:ListItem>
                                    <asp:ListItem Value="5">PGRTR</asp:ListItem>
                                    <asp:ListItem Value="d">PPP</asp:ListItem>
                                    <asp:ListItem Value="x">PPR</asp:ListItem>
                                    <asp:ListItem Value="a">PPRA</asp:ListItem>
                                    <asp:ListItem Value="n">Relat.Anual</asp:ListItem>
                                    <asp:ListItem Value="r">Relat.Gerencial</asp:ListItem>
                                    <asp:ListItem Value="s">SPDA</asp:ListItem>
                                    <asp:ListItem Value="t">Treinamento</asp:ListItem>
                                    <asp:ListItem Value="o">Outros</asp:ListItem>
                                </asp:DropDownList>
                            </div>                   
                        <div class="col-2 mt-4">
                            <asp:Button ID="Busca_Arq" runat="server" Text="Buscar" class="btnMenor2" OnClick="Busca_Arq_Click"/>
                        </div>               
                  </div>
                </div>
                        <div class="col-4 gx-3 gy-2">
                            <div class="row">
                                <div class="col-2 gx-4 gy-2 mt-4">
                                    <asp:Label ID="Label_Data" runat="server" CssClass="tituloLabel" Text="Data"></asp:Label>
                                </div>
                                <div class="col-3 gx-3 gy-2 pe-2">
                                    <asp:Label ID="Label_DataDe" runat="server" CssClass="tituloLabel" Text="DE"></asp:Label>
                                    <asp:TextBox ID="TextBox_DataInicio" runat="server" CssClass="texto form-control form-control-sm" DisplayModeFormat="dd/MM/yyyy" HorizontalAlign="Center" MaxLength="10"></asp:TextBox>
                                </div>
                                <div class="col-3 gx-3 gy-2">
                                    <asp:Label ID="Label_Ate" runat="server" CssClass="tituloLabel" Text="ATÉ"></asp:Label>
                                    <asp:TextBox ID="TextBox_DataFim" runat="server" CssClass="texto form-control form-control-sm" DisplayModeFormat="dd/MM/yyyy" HorizontalAlign="Center" MaxLength="10"></asp:TextBox>   
                                </div>
                                <div class="col-2 mt-4">
                                    <asp:Button ID="Busca_Data" runat="server" CssClass="btnMenor2" Text="Buscar" OnClick="Busca_Data_Click"/>
                                </div>
                            </div>
                        </div>
                    <div class="col-4  gx-3 gy-2">
                        <div class="row">
                            <div class="col-6 gx-3 gy-2">
                                <asp:Label ID="Label_Descricao" runat="server" CssClass="tituloLabel" Text="Descrição"></asp:Label>
                                <asp:TextBox ID="TextBox_Descricao" runat="server" CssClass="texto form-control form-control-sm" ></asp:TextBox>
                            </div>
                            <div class="col-2 mt-4 gx-3 gy-2">
                                <asp:Button ID="Button_Descricao" runat="server" CssClass="btnMenor2" Text="Buscar" OnClick="Button_Descricao_Click"  />
                            </div>
                            <div class="col-2 mt-4 gx-3 gy-2">
                                <asp:Button ID="Button_VerTodos" runat="server" CssClass="btn2" Text="Limpar Pesquisa" OnClick="Button_VerTodos_Click" />
                            </div>

                            </div>
                         </div>

                     </div>
                 </div>
          
            

            <eo:TabStrip ID="TabStrip1" runat="server" ControlSkinID="None" 
                            MultiPageID="MultiPage1" >
                            <topgroup>
                                <Items>
                                    <eo:TabItem Text-Html="Documentos/Laudos">
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



                            <eo:MultiPage ID="MultiPage1" runat="server" Height="250px" Width="838px">

                                <eo:PageView ID="Pageview2" runat="server">


                                            <TABLE class="defaultFont" cellSpacing="0" cellPadding="0" width="860" align="center">
				                            <TR>
					                            <TD class="defaultFont" align="center" colSpan="1">


                                                    <eo:Grid ID="grd_Clinicos" runat="server" ColumnHeaderAscImage="00050403" ColumnHeaderDescImage="00050404" ColumnHeaderDividerOffset="6" 
                                                        ColumnHeaderHeight="30" FixedColumnCount="1" GridLines="Both" Height="250px" ItemHeight="30" CssClass="grid"
                                                        KeyField="IdRepositorio" Width="910px" OnItemCommand="grd_Clinicos_ItemCommand" ClientSideOnItemCommand="OnItemCommand">
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
                                                            <eo:StaticColumn AllowSort="True" DataField="Tipo_Documento" HeaderText="Tipo" Name="DataHora" ReadOnly="True" SortOrder="Ascending" Text="" Width="175">
                                                                <CellStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500; color: #7D7B7B !important; text-align: left; height: 35px !important; padding: 0.3rem 0.3rem 0.3rem 0.5rem; height: 35px !important;" />
                                                            </eo:StaticColumn>

                                                            <eo:StaticColumn AllowSort="True" DataField="DataHora" HeaderText="Data do Documento" Name="DataHora" ReadOnly="True" SortOrder="Ascending" Text="" Width="180" DataFormat="{0:dd/MM/yyyy}" DataType="DateTime">
                                                                <CellStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500; color: #7D7B7B !important; text-align: left; height: 35px !important; padding: 0.3rem 0.3rem 0.3rem 0.5rem; height: 35px !important;" />
                                                            </eo:StaticColumn>
                                                            <eo:StaticColumn AllowSort="True" DataField="Descricao" HeaderText="Descrição" Name="Descricao" ReadOnly="True" Width="360">
                                                                <CellStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500; color: #7D7B7B !important; text-align: left; height: 35px !important; padding: 0.3rem 0.3rem 0.3rem 0.5rem; height: 35px !important;" />
                                                            </eo:StaticColumn>
                                                            <eo:ButtonColumn ButtonText="Editar" Name="Selecionar" Width="90">
                                                                <CellStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500; color: #7D7B7B !important; text-align: left; height: 35px !important; padding: 0.3rem 0.3rem 0.3rem 0.5rem; height: 35px !important;" />
                                                            </eo:ButtonColumn>
                                                            <eo:ButtonColumn ButtonText="Visualizar" Name="Selecionar" Width="90">
                                                                <CellStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500; color: #7D7B7B !important; text-align: left; height: 35px !important; padding: 0.3rem 0.3rem 0.3rem 0.5rem; height: 35px !important;" />
                                                            </eo:ButtonColumn>
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




                                                    <br />
                                                    <asp:HyperLink ID="hlkNovo" runat="server" SkinID="BoldLink" CssClass="btn">Novo Documento/Laudo</asp:HyperLink>
                                                    

                                                    </TD>


                                                </TR>

                                             </TABLE> 

                                    </eo:PageView>



                                </eo:MultiPage>

                <div class="col-12">
                    <asp:Button ID="cmd_Voltar" runat="server" CssClass="btn2" Text="Voltar" onclick="cmd_Voltar_Click" />
                </div>        

                  
                  <asp:TextBox ID="lblreg" runat="server" Width="0" Visible="False" ></asp:TextBox>
                  <br />
                  <asp:TextBox ID="lblnreg" runat="server" Width="0" Visible="False" ></asp:TextBox>
                  <br />
                  <input id="txtIdUsuario" type="text" visible="False"  style="visibility:hidden"  />
                  <input id="txtIdEmpregado" type="text" visible="False" style="visibility:hidden" />
                  <input id="txtIdEmpresa" type="text" visible="False" style="visibility:hidden"/>
          
         <eo:MsgBox ID="MsgBox1" runat="server" BackColor="#ffffff" ControlSkinID="None" HeaderHtml="Dialog Title" Height="192px" Width="345px" CssClass="card border border-1 p-2 text-center" IconUrl="Images/alerta_icon.svg">
            <HeaderStyleActive CssText="font-family: Ubuntu; font-size: 14px; font-weight: bold; color: #1C9489; text-align: center; padding: 5px;" />
            <ContentStyleActive CssText="font-family: Ubuntu; font-size: 14px; font-weight: bold; color: #7D7B7B; text-align: center; padding: 5px; width: 345px; height: 60px" />
            <FooterStyleActive CssText="width: 345px;" />
        </eo:MsgBox>


      </div>
    </div>

</asp:Content>
