<%@ Page Title="Ilitera.Net" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="True"
    CodeBehind="ExameQuestionario.aspx.cs" Inherits="Ilitera.Net.ExameQuestionario" %>

<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>


<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    

    <style type="text/css">
        .defaultFont
    {
        width: 586px;
            height: 20px;
        }
        
        .auto-style1 {
            width: 795px;
        }
                
        .auto-style19 {
            font: xx-small Verdana;
            border: 1px solid #7CC5A1;
            color: #004000;
            background-color: #FCFEFD;
            text-align: left;
            margin-right: 15px;
        }
        .auto-style22 {
            width: 151px;
            height: 17px;
        }
        .auto-style23 {
            width: 317px;
            height: 17px;
        }
        .auto-style24 {
            width: 404px;
            height: 17px;
        }
                        
        .auto-style25 {
            width: 1041px;
            height: 20px;
        }
        .auto-style27 {
            width: 598px;
            height: 20px;
        }
                                
        .auto-style30 {
            width: 1024px;
            height: 20px;
        }
        .auto-style31 {
            width: 429px;
        }
        .auto-style32 {
            width: 598px;
        }
                        
        </style>
</asp:Content>




  

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent" >

    <%--  <eo:PageView ID="Pageview2" runat="server">


                                    
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    
       </eo:PageView>
--%>
	
        <LINK href="scripts/style.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="scripts/validador.js"></script>
		<script language="javascript">
		    var jaclicou = 0;

            
		    var g_itemIndex = -1;
		    var g_cellIndex = -1;

		function AbreCadastro(strPage, IdCliente, IdUsuario)
		{
			addItemPop(centerWin(strPage + '.aspx?IdEmpresa=' + IdCliente + '&IdUsuario=' + IdUsuario + '', 560, 370, 'CadQueixaClinica'));
        }

        				
		function VerificaProcesso()
		{
			if (jaclicou == 0)
				jaclicou=1;
			else
			{
				window.alert("Sua solicitação está sendo processada.\nAguarde!");
				return false;
			}
		}
		
		function ConsisteCkeckBoxDeAlteracao(checkBox){
			tipo = checkBox.id.substring(checkBox.id.length - 1, checkBox.id.length);
			if(tipo == 'S')
				checkBoxAux = eval('document.frmExameClinico.' + checkBox.id.substring(0, checkBox.id.length - 1) + 'N');
			if (tipo == 'N')	
				checkBoxAux = eval('document.frmExameClinico.' + checkBox.id.substring(0, checkBox.id.length - 1) + 'S');
			if (checkBoxAux.checked)  
				checkBoxAux.checked = !checkBox.checked
		}
		function btnAddQueixas_onclick() {

		}



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


    function ShowContextMenu(e, grid, item, cell) {
        //Save the target cell index
        g_itemIndex = item.getIndex();
        g_cellIndex = cell.getColIndex();

        //Show the context menu
    
        //Return true to indicate that we have
        //displayed a context menu
        return true;
    }

    function OnContextMenuItemClicked(e, eventInfo) {
        var grid = eo_GetObject("<%=gridExames.ClientID%>");

        var item = eventInfo.getItem();


        switch (item.getText()) {

            case "Clínico":
                item ="6";

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

<%--        function get_avail_on_text(column, item, cellValue) {
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
        }--%>

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

     
	<%--                                            <eo:StaticColumn HeaderText="Resultado" AllowSort="True" 
                                                DataField="Resultado" Name="Resultado" ReadOnly="False" 
                                                SortOrder="Ascending" Text="" Width="75">
                                                <CellStyle CssText="font-family:Tahoma;font-size:6pt;text-align:Center;" />           
                                            </eo:StaticColumn>--%><%--  <eo:PageView ID="Pageview2" runat="server">


                                    
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    
       </eo:PageView>
--%>

              <asp:ScriptManager ID="ScriptManager1" runat="server">

    </asp:ScriptManager>

    



							<eo:TabStrip ID="TabStrip1" runat="server" ControlSkinID="None" 
                            MultiPageID="MultiPage1">
                            <topgroup>
                                <Items>
                                    <eo:TabItem Text-Html="Dados">
                                    </eo:TabItem>
                                    <eo:TabItem Text-Html="Histórico de Atendimento">
                                    </eo:TabItem>
                                </Items>
                            </topgroup>
                            <lookitems>
                                <eo:TabItem Height="21" ItemID="_Default" LeftIcon-SelectedUrl="00010605" 
                                    LeftIcon-Url="00010604" 
                                    NormalStyle-CssText="background-image: url(00010602); background-repeat: repeat-x; font-weight: normal; color: black;" 
                                    RightIcon-SelectedUrl="00010607" RightIcon-Url="00010606" 
                                    SelectedStyle-CssText="background-image: url(00010603); background-repeat: repeat-x; font-weight: bold; color: #ff7e00;" 
                                    Text-Padding-Bottom="2" Text-Padding-Top="1">
                                    <subgroup itemspacing="1" 
                                        style-csstext="background-image:url(00010601);background-position-y:bottom;background-repeat:repeat-x;color:black;cursor:hand;font-family:Verdana;font-size:12px;">
                                    </subgroup>
                                </eo:TabItem>
                            </lookitems>
                        </eo:TabStrip>



             <eo:MultiPage ID="MultiPage1" runat="server" Height="400" Width="450">
            
                 


                  <eo:PageView ID="Pageview1" runat="server" Width="443px">







			<TABLE class="auto-style25" id="Table1" cellSpacing="0" cellPadding="0" align="center"
				border="0">
				<TR>
					<TD align="center"><%--                                            <eo:StaticColumn HeaderText="Resultado" AllowSort="True" 
                                                DataField="Resultado" Name="Resultado" ReadOnly="False" 
                                                SortOrder="Ascending" Text="" Width="75">
                                                <CellStyle CssText="font-family:Tahoma;font-size:6pt;text-align:Center;" />           
                                            </eo:StaticColumn>--%>
                        <asp:Label ID="Label4" runat="server" CssClass="largeboldFont">Entrevista</asp:Label>
                        <br />
                        <asp:Label ID="Label5" runat="server" CssClass="boldFont" Font-Bold="False" SkinID="BoldFont">Obrigatoriedade de Preenchimento: *Sempre **Agudo com Complicação ***Agudo sem Complicação ****Crônico</asp:Label>
                        <br>
                        <br />
                        <asp:Label ID="Label3" runat="server" CssClass="boldFont" SkinID="BoldFont">Tipo:</asp:Label>
                        &nbsp;&nbsp;&nbsp;
                        <asp:RadioButton ID="rd_Agudo_Complicacao" runat="server" AutoPostBack="True" Checked="True" CssClass="normalFont" GroupName="1" OnCheckedChanged="rd_Agudo_Complicacao_CheckedChanged" Text="Caso Agudo com Complicação" />
                        &nbsp;
                        <asp:RadioButton ID="rd_Agudo_sem_Complicacao" runat="server" AutoPostBack="True" CssClass="normalFont" GroupName="1" OnCheckedChanged="rd_Agudo_sem_Complicacao_CheckedChanged" Text="Caso Agudo sem Complicação" />
                        &nbsp;
                        <asp:RadioButton ID="rd_Cronico" runat="server" AutoPostBack="True" CssClass="normalFont" GroupName="1" OnCheckedChanged="rd_Cronico_CheckedChanged" Text="Caso Crônico" />
                        <br />
                        <table border="0" cellpadding="0" cellspacing="0" class="auto-style30">
                            <tr>
                                <td align="center" class="auto-style22">
                                    <asp:Label ID="lblData" runat="server" CssClass="boldFont" SkinID="BoldFont">Data *</asp:Label>
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <td align="center" width="153">
                                    <asp:TextBox ID="wdtDataExame" runat="server" CssClass="inputBox" DisplayModeFormat="dd/MM/yyyy" HorizontalAlign="Center" ImageDirectory=" " MaxLength="10" Nullable="False" Width="85px"></asp:TextBox>
                                </td>
                                <td>
                                    &nbsp;&nbsp;&nbsp;
                                    <asp:CheckBox ID="chk_Atualizar" runat="server" CssClass="boldFont" Text="Atualizar para não-crítico  ***" Width="198px" />
                                    <br />
                                </td>
                            </tr>
                            <tr>
                                <td align="center" class="auto-style23">
                                    <asp:Label ID="lblOrientacao" runat="server" CssClass="boldFont" SkinID="BoldFont">Orientação *</asp:Label>
                                </td>
                                <td align="center" class="auto-style24">
                                    <asp:Label ID="lblEspecialista" runat="server" CssClass="boldFont" SkinID="BoldFont">Tipo de Especialista ** e ****</asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <asp:TextBox ID="wneOrientacao" runat="server" CssClass="auto-style19" MaxLength="1500" Nullable="False" Width="465px" Height="60px" TextMode="MultiLine" Font-Size="X-Small"></asp:TextBox>
                                </td>
                                <td align="center" width="183"><asp:TextBox ID="wneEspecialista" runat="server" CssClass="inputBox" MaxLength="150" Nullable="False" Width="430px" Height="60px" TextMode="MultiLine" Font-Size="Small"></asp:TextBox>
                                </td>
                            </tr>

                                          

                        </table>
                        
									</TABLE>

                     
                       

									<hr />

                      <table>
                          <tr>
                          <td class="auto-style31">


									<asp:label id="lblCID" runat="server" CssClass="boldFont" Width="300px"> C.I.D. - Digite o código desejado ou uma palavra chave para Procurar a CID</asp:label><BR>
									<BR>

									&nbsp;<asp:label id="Label28" runat="server" CssClass="boldFont">Principal:</asp:label>
                                                &nbsp;<asp:textbox id="txtCID" runat="server" CssClass="inputBox" 
                                        Width="200px"></asp:textbox>&nbsp;<asp:button id="btnProcurar" runat="server" CssClass="buttonBox" Width="70px" Text="Procurar" onclick="btnProcurar_Click"></asp:button>
                                    <asp:Label ID="lbl_Id1" runat="server" Text="0" Visible="False"></asp:Label>
                                    <br />
                                    <br />
                                 <asp:label id="Label2" runat="server" CssClass="boldFont">2. CID</asp:label>
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                                    <asp:textbox id="txtCID2" 
                                        runat="server" CssClass="inputBox" Width="200px"></asp:textbox>&nbsp;<asp:button 
                                        id="btnProcurar2" runat="server" CssClass="buttonBox" Width="70px" 
                                        Text="Procurar" onclick="btnProcurar2_Click"></asp:button>
                                    <asp:Label ID="lbl_Id2" runat="server" Text="0" Visible="False"></asp:Label>
                                    <br />
                                    <br />

                                   <asp:label id="Label6" runat="server" CssClass="boldFont">3. CID</asp:label>
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                                    <asp:textbox id="txtCID3" 
                                        runat="server" CssClass="inputBox" Width="200px"></asp:textbox>&nbsp;<asp:button 
                                        id="btnProcurar3" runat="server" CssClass="buttonBox" Width="70px" 
                                        Text="Procurar" onclick="btnProcurar3_Click"></asp:button>
                                    <asp:Label ID="lbl_Id3" runat="server" Text="0" Visible="False"></asp:Label>
                                    <br />
                                    <br />

                                   <asp:label id="Label7" runat="server" CssClass="boldFont">4. CID</asp:label>
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                                    <asp:textbox id="txtCID4" 
                                        runat="server" CssClass="inputBox" Width="200px"></asp:textbox>&nbsp;<asp:button 
                                        id="btnProcurar4" runat="server" CssClass="buttonBox" Width="70px" 
                                        Text="Procurar" onclick="btnProcurar4_Click"></asp:button>
                                    <asp:Label ID="lbl_Id4" runat="server" Text="0" Visible="False"></asp:Label>
                                    <br />
                                        <asp:Label ID="lbl_Procura" runat="server" Text="0" Visible="False"></asp:Label>
                                        &nbsp;&nbsp;&nbsp;


                          </TD>
                              
                              <td class="auto-style32">

                                        <asp:Label ID="lblPrograma" runat="server" CssClass="boldFont" SkinID="BoldFont">Programa de Saúde: ** e ****</asp:Label>
                                        &nbsp;<br /> &nbsp;<asp:DropDownList ID="cmb_Programa" runat="server" Font-Size="X-Small" Height="18px" Width="414px">
                                            <asp:ListItem Value="0">-</asp:ListItem>
                                            <asp:ListItem Value="1">Saúde Alimentar</asp:ListItem>
                                            <asp:ListItem Value="2">Qualidade de Vida</asp:ListItem>
                                            <asp:ListItem Value="3">Combate ao Sedentarismo</asp:ListItem>
                                            <asp:ListItem Value="4">Ergonomia</asp:ListItem>
                                            <asp:ListItem Value="5">Grupo de Hipertensão</asp:ListItem>
                                            <asp:ListItem Value="6">Grupo de Diabetes</asp:ListItem>
                                            <asp:ListItem Value="7">Controle de Hábitos e Vícios</asp:ListItem>
                                            <asp:ListItem Value="8">Controle do Stress</asp:ListItem>
                                        </asp:DropDownList>
                                        <br />
                                        <asp:Label ID="lblPrograma0" runat="server" CssClass="boldFont" SkinID="BoldFont">Status: *</asp:Label>
                                        <br />
                                        &nbsp;
                                        <asp:DropDownList ID="cmb_Status" runat="server" Font-Size="X-Small" Height="16px" Width="414px">
                                            <asp:ListItem Value="0">-</asp:ListItem>
                                            <asp:ListItem Value="1">Crítico</asp:ListItem>
                                            <asp:ListItem Value="2">Programa de Saúde</asp:ListItem>
                                            <asp:ListItem Value="3">Sem Recuperação</asp:ListItem>
                                            <asp:ListItem Value="4">Crítico Crônico</asp:ListItem>
                                            <asp:ListItem Value="5">Crítico sem Atuação</asp:ListItem>
                                            <asp:ListItem Value="6">Averiguação</asp:ListItem>
                                            <asp:ListItem Value="7">Não Crítico</asp:ListItem>
                                        </asp:DropDownList>
                                   
                                                    <br />
                                        <br />
                                   
                                                    <asp:Label ID="lblRetorno" runat="server" CssClass="boldFont" SkinID="BoldFont">Retorno ( em dias ) **</asp:Label>
                                               
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                               
                                                    <asp:Label ID="lblTratamento" runat="server" CssClass="boldFont" Enabled="False" SkinID="BoldFont">Tratamento Alto-Custo ****</asp:Label>
                                           
                                                    <br />
                                           
                                                    <asp:TextBox ID="wndRetorno" runat="server" CssClass="inputBox" DataMode="Int" MaxLength="3" Nullable="False" Width="62px">0</asp:TextBox>
                                               
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                               
                                                    <asp:RadioButton ID="rd_Alto_Sim" runat="server" AutoPostBack="True" CssClass="normalFont" Enabled="False" GroupName="2" Text="Sim" />
                                                    &nbsp;&nbsp;
                                                    <asp:RadioButton ID="rd_Alto_Nao" runat="server" AutoPostBack="True" Checked="True" CssClass="normalFont" Enabled="False" GroupName="2" Text="Não" />
                                                
                                            </tr>
                      </TABLE>
                      <table border="0" cellpadding="0" cellspacing="0" class="auto-style27">
                                        <asp:DropDownList ID="ddlCID" runat="server" AutoPostBack="True" CssClass="inputBox" OnSelectedIndexChanged="ddlCID_SelectedIndexChanged1" Width="270px">
                                            <asp:ListItem Value="0">-</asp:ListItem>
                                        </asp:DropDownList>
                                        </table>
                                        <asp:Label ID="lblObs" runat="server" CssClass="boldFont" SkinID="BoldFont">Recomendação médica: * </asp:Label>
                                        <br>
                                        <asp:TextBox ID="txtObs" runat="server" CssClass="inputBox" Font-Size="X-Small" Height="56px" MaxLength="500" Rows="3" tabIndex="1" TextMode="MultiLine" Width="868px"></asp:TextBox>
                                        <br>
                                        <br>
                                        </br>
                                        </br>
                                        </br>



                      </eo:PageView>



      <%--  <eo:PageView ID="Pageview2" runat="server">


                                    
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    
       </eo:PageView>
--%>



                                <eo:PageView ID="Pageview2" runat="server">

                                            <TABLE class="defaultFont" cellSpacing="0" cellPadding="0" width="600" align="center" border="0" bgcolor="white">
				                            <TR>
					                            <TD class="defaultFont" align="center" colSpan="1"><asp:label id="lblExCli" 
                                                        runat="server" SkinID="TitleFont" style="font-weight: 700">Histórico de Atendimentos ( Informações Médicas )</asp:label>
                                                    <%--		</form>
	                            </body>
                            </HTML>
                            --%>
						                            <BR>
					                            </TD>
				                            <TR>
					                            <TD vAlign="top" align="right">
                    
                         
                            
                                                    <br />

                                    
                                                                <eo:Grid ID="gridExames" runat="server" BorderColor="Black" 
                                                                        BorderWidth="1px" ColumnHeaderAscImage="00050403" 
                                                                        ColumnHeaderDescImage="00050404" ColumnHeaderDividerImage="00050402" 
                                                                        FixedColumnCount="1" Font-Bold="False" Font-Italic="False" Font-Names="Verdana" 
                                                                        Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" 
                                                                        Font-Underline="False" GridLineColor="240, 240, 240" 
                                                                        GridLines="Both" Height="222px" Width="1024px" ColumnHeaderDividerOffset="6" ItemHeight="24" KeyField="IdHistorico" PageSize="40"  FullRowMode="False"
                                                                        OnItemCommand="grdExames_ItemCommand"  ClientSideOnCellSelected="OnCellSelected"
                                                                        ClientSideOnItemCommand="OnItemCommand"                                                                     >
                                                                    <ItemStyles>
                                                                        <eo:GridItemStyleSet>
                                                                            <ItemStyle CssText="background-color: white" />
                                                                            <AlternatingItemStyle CssText="background-color:#ffffcc;" />
                                                                            <SelectedStyle CssText="border-bottom-style:solid;border-left-style:solid;border-right-style:solid;border-top-style:solid;" />
                                                                            <CellStyle CssText="border-bottom-color:#99ccff;border-bottom-style:solid;border-left-color:#99ccff;border-left-style:solid;border-right-color:#99ccff;border-right-style:solid;border-top-color:#99ccff;border-top-style:solid;color:#black;padding-left:8px;padding-top:2px;white-space:nowrap;" />
                                                                        </eo:GridItemStyleSet>
                                                                    </ItemStyles>
                                                                    <ColumnHeaderTextStyle CssText="font-size:8pt;" />
                                                                    <ColumnHeaderStyle CssText="background-image:url('00050401');color:#666666;font-size:8pt;font-weight:bold;padding-left:8px;padding-top:2px;" />
                                                                    <ColumnHeaderHoverStyle CssText="font-size:8pt;" />
                                                                    <Columns>

                                            

                            <%--                                            <eo:StaticColumn HeaderText="Resultado" AllowSort="True" 
                                                                            DataField="Resultado" Name="Resultado" ReadOnly="False" 
                                                                            SortOrder="Ascending" Text="" Width="75">
                                                                            <CellStyle CssText="font-family:Tahoma;font-size:6pt;text-align:Center;" />           
                                                                        </eo:StaticColumn>--%>
  
                                                                        <eo:StaticColumn HeaderText="" AllowSort="True" 
                                                                            DataField="IdHistorico" Name="Id_Historico" ReadOnly="True" 
                                                                            SortOrder="Ascending" Text="" Width="0">
                                                                            <CellStyle CssText="background-color:#ffffcc;font-family:Tahoma;font-weight:bold;text-align:center" />
                                                                        </eo:StaticColumn>

                                                                        <eo:StaticColumn HeaderText="Exames Ocupacionais" 
                                                                            DataField="ExamesOcupacionais" Name="Exames Ocupacionais" ReadOnly="True" 
                                                                            SortOrder="Ascending" Text="" Width="185">
                                                                            <CellStyle CssText="border-bottom-color:gray;border-bottom-style:solid;border-bottom-width:1px;border-left-color:gray;border-left-style:solid;border-left-width:1px;border-right-color:gray;border-right-style:solid;border-right-width:1px;border-top-color:gray;border-top-style:solid;border-top-width:1px;font-family:Tahoma;font-size:8pt;text-align:left; vertical-align: middle;" />
                                                
                                                                        </eo:StaticColumn>

                                                                        <eo:StaticColumn HeaderText="Exames Ambulatoriais" 
                                                                            DataField="ExamesAmbulatoriais" Name="Exames Ambulatoriais" ReadOnly="True" 
                                                                            SortOrder="Ascending" Text="" Width="155">
                                                                            <CellStyle CssText="border-bottom-color:gray;border-bottom-style:solid;border-bottom-width:1px;border-left-color:gray;border-left-style:solid;border-left-width:1px;border-right-color:gray;border-right-style:solid;border-right-width:1px;border-top-color:gray;border-top-style:solid;border-top-width:1px;font-family:Tahoma;font-size:8pt;text-align:left; vertical-align: middle;" />
                                                                        </eo:StaticColumn>

                                                                        <eo:StaticColumn HeaderText="Afastamentos" 
                                                                            DataField="Atestados" Name="Atestados" Width="145">
                                                                            <CellStyle CssText="border-bottom-color:gray;border-bottom-style:solid;border-bottom-width:1px;border-left-color:gray;border-left-style:solid;border-left-width:1px;border-right-color:gray;border-right-style:solid;border-right-width:1px;border-top-color:gray;border-top-style:solid;border-top-width:1px;font-family:Tahoma;font-size:8pt;text-align:left; vertical-align: middle;" />
                                                                        </eo:StaticColumn>

                                                                        <eo:StaticColumn HeaderText="Afastamentos INSS" 
                                                                            DataField="AfastamentosINSS" Name="Afastamentos INSS" Width="145">
                                                                            <CellStyle CssText="border-bottom-color:gray;border-bottom-style:solid;border-bottom-width:1px;border-left-color:gray;border-left-style:solid;border-left-width:1px;border-right-color:gray;border-right-style:solid;border-right-width:1px;border-top-color:gray;border-top-style:solid;border-top-width:1px;font-family:Tahoma;font-size:8pt;text-align:left; vertical-align: middle;" />
                                                                        </eo:StaticColumn>

                                                                        <eo:StaticColumn HeaderText="Programa de Saúde" 
                                                                            DataField="ProgramaSaude" Name="Programa Saude" Width="155" >
                                                                            <CellStyle CssText="border-bottom-color:gray;border-bottom-style:solid;border-bottom-width:1px;border-left-color:gray;border-left-style:solid;border-left-width:1px;border-right-color:gray;border-right-style:solid;border-right-width:1px;border-top-color:gray;border-top-style:solid;border-top-width:1px;font-family:Tahoma;font-size:8pt;text-align:left; vertical-align: middle;" />                                                
                                                                        </eo:StaticColumn>

                                            
                                                                        <eo:StaticColumn DataField="AtendimentoAssistencial" HeaderText="Atendimento Assistencial" Name="Atendimento Assistencial" Width="220">
                                                                            <CellStyle CssText="border-bottom-color:gray;border-bottom-style:solid;border-bottom-width:1px;border-left-color:gray;border-left-style:solid;border-left-width:1px;border-right-color:gray;border-right-style:solid;border-right-width:1px;border-top-color:gray;border-top-style:solid;border-top-width:1px;font-family:Tahoma;font-size:8pt;text-align:left; vertical-align: middle;" />                                                
                                                                        </eo:StaticColumn>

                                            <eo:StaticColumn HeaderText="" AllowSort="False" 
                                                DataField="IdExameOcupacional" Name="IdExameOcupacional" ReadOnly="True" 
                                                SortOrder="Ascending" Text="" Width="0">
                                                <CellStyle CssText="background-color:#ffffcc;font-family:Tahoma;font-weight:bold;text-align:center" />
                                            </eo:StaticColumn>

                                            <eo:StaticColumn HeaderText="" AllowSort="False" 
                                                DataField="IdExameAmbulatorial" Name="IdExameAmbulatorial" ReadOnly="True" 
                                                SortOrder="Ascending" Text="" Width="0">
                                                <CellStyle CssText="background-color:#ffffcc;font-family:Tahoma;font-weight:bold;text-align:center" />
                                            </eo:StaticColumn>


                                            <eo:StaticColumn HeaderText="" AllowSort="False" 
                                                DataField="IdAtestado" Name="IdAtestado" ReadOnly="True" 
                                                SortOrder="Ascending" Text="" Width="0">
                                                <CellStyle CssText="background-color:#ffffcc;font-family:Tahoma;font-weight:bold;text-align:center" />
                                            </eo:StaticColumn>


                                            <eo:StaticColumn HeaderText="" AllowSort="False" 
                                                DataField="IdAfastamentoINSS" Name="IdAfastamentoINSS" ReadOnly="True" 
                                                SortOrder="Ascending" Text="" Width="0">
                                                <CellStyle CssText="background-color:#ffffcc;font-family:Tahoma;font-weight:bold;text-align:center" />
                                            </eo:StaticColumn>


                                            <eo:StaticColumn HeaderText="" AllowSort="False" 
                                                DataField="IdProgramaSaude" Name="IdProgramaSaude" ReadOnly="True" 
                                                SortOrder="Ascending" Text="" Width="0">
                                                <CellStyle CssText="background-color:#ffffcc;font-family:Tahoma;font-weight:bold;text-align:center" />
                                            </eo:StaticColumn>


                                            <eo:StaticColumn HeaderText="" AllowSort="False" 
                                                DataField="IdAtendimentoAssistencial" Name="IdAtendimentoAssistencial" ReadOnly="True" 
                                                SortOrder="Ascending" Text="" Width="0">
                                                <CellStyle CssText="background-color:#ffffcc;font-family:Tahoma;font-weight:bold;text-align:center" />
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

            
                                                </TD>
				                            </TR>
				                            <TR>
					                            <TD class="normalFont" style="HEIGHT: 15px" align="center"><P>
							                            <TABLE class="defaultFont" id="Table28" cellSpacing="0" cellPadding="0" width="590" align="right"
								                            border="0">
								                            <TR>
									                            <TD align="right"><asp:label id="lblTotRegistros" runat="server"></asp:label></TD>
                  
								                            </TR>
							                            </TABLE>
							                            <BR>
							                            </P>
					                            </TD>
				                            </TR>
			                            </TABLE>

                                    </eo:PageView>


                 </eo:MultiPage>


        <table>
            <tr>
                <td align="center" class="auto-style1">

                            <asp:Button ID="btnOK" runat="server" onclick="btnOK_Click" Text="Gravar"  CssClass="buttonBox"
                                Width="70px" Height="18px" />
                            &nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnExcluir" runat="server" onclick="btnExcluir_Click"  CssClass="buttonBox"
                                Text="Excluir" Width="70px" Height="18px" />
                    &nbsp;&nbsp;&nbsp;&nbsp; 
                    <br />
                    </td>
                </tr>
            
                            
            <tr>
                <td align="center" class="auto-style1">
        
                            <input id="txtAuxAviso" type="hidden" runat="server"/>
                            <input id="txtCloseWindow" type="hidden" 
    runat="server"/>
                            <input id="txtExecutePost" 
    type="hidden" runat="server"/>
                           
                    <%--</igmisc:WebAsyncRefreshPanel>--%>
                
                <%--</igmisc:WebAsyncRefreshPanel>--%>

                <asp:Button ID="cmd_Voltar" runat="server" BackColor="#999999" 
                                    CssClass="buttonFoto" Font-Size="XX-Small" onclick="cmd_Voltar_Click" 
                                    Text="Voltar" Width="127px" />
                </TD>
                </TR>
                </table>
            <input id="txtAuxiliar" runat="server" name="txtAuxiliar" type="hidden" />

    </eo:CallbackPanel>
       

        
         <eo:MsgBox ID="MsgBox1" runat="server" BackColor="#47729F" ControlSkinID="None" 
        HeaderHtml="Dialog Title" Height="100px" Width="168px">
        <HeaderStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #ffbf00 3px solid; padding-left: 4px; font-weight: bold; font-size: 11px; padding-bottom: 2px; color: white; padding-top: 2px; border-bottom: #22456a 1px solid; font-family: verdana" />
        <ContentStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #7d97b6 1px solid; padding-left: 4px; border-left-width: 1px; font-size: 11px; border-left-color: #728eb8; padding-bottom: 4px; color: white; padding-top: 4px; border-bottom: #22456a 1px solid; font-family: verdana" />
        <FooterStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #7d97b6 1px solid; padding-left: 4px; border-left-width: 1px; font-size: 11px; border-left-color: #728eb8; padding-bottom: 4px; color: white; padding-top: 4px; border-bottom: #22456a 1px solid; font-family: verdana" />
        </eo:MsgBox>

    

    </asp:Content>