<%@ Page Title="Ilitera.Net" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="True"
    CodeBehind="ExameComplementar.aspx.cs" Inherits="Ilitera.Net.ExameComplementar" %>

<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>


<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <link href="css/forms.css" rel="stylesheet" type="text/css" />
    <link href="css/tabelas.css" rel="stylesheet" type="text/css" />

<script type="text/javascript">

function on_column_gettext(column, item, cellValue)
{
    //if (cellValue == 1)
    //    return "Sim";
    //else
    //	return "Nao";
    return cellValue;
}

function on_begin_edit(cell)
{
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

function on_end_edit(cell)
{
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
        
        .auto-style1 {
            width: 444px;
        }
        .auto-style2 {
            width: 444px;
            height: 22px;
        }
        
        .auto-style3 {
            width: 318px;
        }
        .auto-style4 {
            width: 539px;
        }
        
        .auto-style5 {
            width: 309px;
        }
        
    </style>
</asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent" >
   <div class="container-fluid d-flex ms-5 ps-4">
    <div class="row gx-3 gy-2 w-100">

    <eo:CallbackPanel runat="server" id="CallbackPanel1" Triggers="" Width="519px">
	

		<LINK href="scripts/style.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="scripts/validador.js"></script>
		<script language="javascript">
		function AbreClinicas(IdCliente, IdUsuario)
		{
			addItemPop(centerWin('../CommonPages/CadClinica.aspx?IdEmpresa=' + IdCliente + '&IdUsuario=' + IdUsuario + '', 360, 270, 'CadatroClinica'));
		}
		
		function AbreMedicos(IdUsuario)
		{
			var ddlClinicas = document.getElementById("ddlClinica");
			
			if (ddlClinicas.options[ddlClinicas.selectedIndex].value == "0")
				window.alert("É necessário selecionar uma Clínica para continuar!");
			else
				addItemPop(centerWin('../CommonPages/CadMedico.aspx?IdClinica=' + ddlClinicas.options[ddlClinicas.selectedIndex].value + '&IdUsuario=' + IdUsuario + '', 350, 310, 'CadastroMedico'));
		}
		</script>
<%--	</HEAD>
	<body>
		<form id="FormExameComplementar" method="post" runat="server">--%>

         
         <%-- multipage --%>

        <div class="col-11">
            <eo:TabStrip ID="TabStrip1" runat="server" ControlSkinID="None" 
                            MultiPageID="MultiPage1" >
                            <topgroup>
                                <Items>
                                    <eo:TabItem Text-Html="Dados">
                                    </eo:TabItem>
                                    <eo:TabItem Text-Html="Anamnese">
                                    </eo:TabItem>
                                    <eo:TabItem Text-Html="Resultado">
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




             <eo:MultiPage ID="MultiPage1" runat="server" Height="400" Width="1050" CssClass="mb-4" >
            
                  <eo:PageView ID="Pageview1" runat="server" Width="1050px">



			 <%-- subtitulo --%>

        <div class="col-11 subtituloBG mb-3" style="padding-top: 10px;">
            <asp:Label ID="Label1" runat="server" SkinID="TitleFont" CssClass="subtitulo">Exame Complementar</asp:Label>
        </div> 

        
        <div class="col-11 mb-3">
            <div class="row">

                <div class="col-md-3 gx-3 gy-2">
                    <asp:Label ID="Label27" runat="server" SkinID="BoldFont" CssClass="tituloLabel form-label">Resultado do Exame</asp:Label>
                    <div class="row">
                        <div class="col-12 gx-8 gy-1 ms-3">
                            <asp:RadioButtonList ID="rblResultado" runat="server" RepeatColumns="3" tabIndex="1" CssClass="texto form-control-sm" Width="250"> 
                                <asp:ListItem Value="1">Normal</asp:ListItem>
                                <asp:ListItem Value="2">Alterado</asp:ListItem>
                                <asp:ListItem Value="3">Espera</asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </div>
                </div>

         <div class="col-md-2 gx-6 gy-2 ms-3">
                    <asp:Label ID="Label3" runat="server" SkinID="BoldFont" CssClass="tituloLabel form-label">Data do Exame</asp:Label>
                    <asp:TextBox ID="wdtDataExame" runat="server" MaxLength="10" CssClass="texto form-control form-control-sm"></asp:TextBox>
                </div>

        </div>
       </div>

        <%-- FILTROS --%>

                      <div class="col-11 mb-3">
                          <div class="row">

                    <div class="col-8 gx-3 gy-2">
                         <asp:label id="Label4" runat="server" CssClass="tituloLabel form-label">Tipo do Exame</asp:label><BR>
						<asp:dropdownlist id="ddlTipoExame" onselectedindexchanged="ddlTipoExame_SelectedIndexChanged" runat="server" CssClass="texto form-select form-select-sm" AutoPostBack="True"></asp:dropdownlist>
                    </div>
                    <div class="col-4 ps-4 pt-1 gy-4">
                        <asp:CheckBox ID="chk_PCMSO" runat="server" AutoPostBack="True" Checked="True" OnCheckedChanged="chk_PCMSO_CheckedChanged" Text="Exames do PCMSO" />
                    </div>

                    <div class="col-md-2 gx-3 gy-2">
                        <asp:label id="lblClinica"  runat="server" CssClass="tituloLabel form-label">Clínica</asp:label><BR>
						<asp:dropdownlist id="ddlClinica" onselectedindexchanged="ddlClinica_SelectedIndexChanged" runat="server" CssClass="texto form-select form-select-sm" AutoPostBack="True"></asp:dropdownlist><input
                         class="buttonBox" id="btnAddClinica" type="button" value="..." name="btnAddClinica"
							runat="server" visible="False">
                    </div>

                    <div class="col-md-2 gx-3 gy-2">
                        <asp:label id="lblMedico" runat="server" CssClass="tituloLabel form-label">Médico</asp:label><BR>
						<asp:dropdownlist id="ddlMedico" runat="server" CssClass="texto form-select form-select-sm"></asp:dropdownlist><input
                        class="buttonBox" id="btnAddMedico" type="button" value="..." name="btnAddMedico"
							runat="server" visible="False">
                    </div>

                    <div class="col-md-4 gx-3 gy-2">
                        <asp:label id="lblAnotacoes" runat="server" CssClass="tituloLabel form-label">Anotações para o Prontuário</asp:label><BR>
						<asp:textbox id="txtDescricao" runat="server" CssClass="texto form-control form-control-sm" Width="380px" Rows="4" TextMode="MultiLine"></asp:textbox>
                    </div>

                </div>
                 </div>


                      <div class="col-12">

                          <div class="row">
                      <div class="col-md-6 gx-3 gy-2">
                        <asp:label id="lblAnotacoes0" runat="server" CssClass="tituloLabel form-label">Arquivo do Prontuário :</asp:label>
						<asp:textbox id="txt_Arq" runat="server" CssClass="texto form-control form-control-sm" Rows="4" TextMode="MultiLine" Height="27px" ReadOnly="True"></asp:textbox>
                    </div>
                      </div>

                    <div class="row">
                   <div class="col-md-6 gx-3 gy-2">
                       <asp:label id="Label6" runat="server" CssClass="tituloLabel">Inserir / Modificar Arquivo de Prontuário : </asp:label>
                        <asp:FileUpload ID="File1" runat="server" lientIDMode="Static" CssClass="texto form-control"/>
                   </div>
                      </div>

                </div>
                 


         </eo:PageView>

        <eo:PageView ID="Pageview2" runat="server">

             <%-- TABELA--%>
                                    
                                    <eo:Grid ID="gridAnamnese" runat="server"
                                            ColumnHeaderAscImage="00050403" 
                                            ColumnHeaderDescImage="00050404"
                                            FixedColumnCount="1"
                                            GridLines="Both" Height="445px" Width="800px" ColumnHeaderDividerOffset="6" 
                                            ColumnHeaderHeight="40" ItemHeight="40" KeyField="IdAnamneseExame"  
                                        ClientSideOnItemCommand="OnItemCommand" OnItemCommand="gridAnamnese_ItemCommand" FullRowMode="False"  >
                                        <ItemStyles>
                                            <eo:GridItemStyleSet>
                                                <ItemStyle CssText="background-color: white" />
                                                <AlternatingItemStyle CssText="background-color:#ffffcc;" />
                                                <SelectedStyle CssText="border-bottom-style:solid;border-left-style:solid;border-right-style:solid;border-top-style:solid;" />
                                                <CellStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500; color: #7D7B7B !important; text-align: left; height: 35px !important; padding: 0.3rem 0.3rem 0.3rem 0.5rem; height: 35px !important;" />
                                            </eo:GridItemStyleSet>
                                        </ItemStyles>
                                        <ColumnHeaderTextStyle CssText="text-align:center; font-size: x-small;" />
                                        <ColumnHeaderStyle CssText="background-color: #D9D9D9; color: #1C9489; padding: .3rem .3rem .3rem .5rem; text-align: left;" />
                                        <Columns>

                                            <eo:StaticColumn HeaderText="" AllowSort="True" 
                                                DataField="IdAnamneseExame" Name="IdAnamneseExame" ReadOnly="True" 
                                                SortOrder="Ascending" Text="" Width="0">
                                                <CellStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500; color: #7D7B7B !important; text-align: left; height: 35px !important; padding: 0.3rem 0.3rem 0.3rem 0.5rem; height: 35px !important;" />
                                            </eo:StaticColumn>

                                            <eo:StaticColumn HeaderText="" AllowSort="True" 
                                                DataField="IdExameBase" Name="IdExameBase" ReadOnly="True" 
                                                SortOrder="Ascending" Text="" Width="0">
                                                <CellStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500; color: #7D7B7B !important; text-align: left; height: 35px !important; padding: 0.3rem 0.3rem 0.3rem 0.5rem; height: 35px !important;" />
                                            </eo:StaticColumn>

                                            <eo:StaticColumn HeaderText="" AllowSort="True" 
                                                DataField="IdAnamneseDinamica" Name="IdAnamneseDinamica" ReadOnly="True" 
                                                SortOrder="Ascending" Text="" Width="0">
                                                <CellStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500; color: #7D7B7B !important; text-align: left; height: 35px !important; padding: 0.3rem 0.3rem 0.3rem 0.5rem; height: 35px !important;" />
                                            </eo:StaticColumn>

                                            <eo:StaticColumn HeaderText="" AllowSort="True" 
                                                DataField="Peso" Name="Peso" ReadOnly="True" 
                                                SortOrder="Ascending" Text="" Width="0">
                                                <CellStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500; color: #7D7B7B !important; text-align: left; height: 35px !important; padding: 0.3rem 0.3rem 0.3rem 0.5rem; height: 35px !important;" />
                                            </eo:StaticColumn>

                                            <eo:StaticColumn HeaderText="Sistema" AllowSort="True" 
                                                DataField="Sistema" Name="Sistema" ReadOnly="True" 
                                                SortOrder="Ascending" Text="" Width="155">
                                                <CellStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500; color: #7D7B7B !important; text-align: left; height: 35px !important; padding: 0.3rem 0.3rem 0.3rem 0.5rem; height: 35px !important;" />
                                            </eo:StaticColumn>

                                            <eo:StaticColumn HeaderText="Questao" AllowSort="True" 
                                                DataField="Questao" Name="Questao" ReadOnly="True" 
                                                SortOrder="Ascending" Text="" Width="535">
                                                <CellStyle CssText="font-family:Tahoma;font-size:7pt;text-align:left;" />
                                            </eo:StaticColumn>

                                            

                                            <eo:CustomColumn HeaderText="Resultado" 
                                                DataField="Resultado" Name="Resultado" ReadOnly="False" 
                                                Width="75" DataType="String"  ClientSideEndEdit="on_end_edit" ClientSideBeginEdit="on_begin_edit"  ClientSideGetText="on_column_gettext" >
                                                <CellStyle CssText="font-family:Tahoma;font-size:7pt;text-align:Center;" />           
                                               <EditorTemplate>
				                                                <select id="grade_dropdown"  >
                                                                    <option>Não</option>
					                                                <option>Sim</option>					                                                
				                                                </select>
			                                    </EditorTemplate>
                                            </eo:CustomColumn>


<%--                                            <eo:StaticColumn HeaderText="Resultado" AllowSort="True" 
                                                DataField="Resultado" Name="Resultado" ReadOnly="False" 
                                                SortOrder="Ascending" Text="" Width="75">
                                                <CellStyle CssText="font-family:Tahoma;font-size:6pt;text-align:Center;" />           
                                            </eo:StaticColumn>--%>
  
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


                               <div class="col-12 text-start"> 
                                        <asp:Button ID="cmd_Anamnese" onclick="cmd_Anamnese_Click"  runat="server" tabIndex="1" Text="Salvar Alterações na Anamnese" Width="209px" CssClass="btn"/>
                                        <asp:LinkButton ID="lkbAnamnese" runat="server" SkinID="BoldLinkButton" CssClass="btn"><img border="0" src="Images/printer.svg" style="padding: .3rem;">Anamnese</img></asp:LinkButton>
                                </div>
                                        
       </eo:PageView>

                 <eo:PageView ID="Pageview3" runat="server">

               <div class="col-11 mb-3">
                <div class="row">

                <div class="col-11 gx-3 gy-2">
						<asp:label id="Label5" runat="server" CssClass="tituloLabel form-label">Resultados</asp:label>
                  </div>

                <div class="col-md-3 gx-3 gy-2">
                     <asp:label id="Label7" runat="server" CssClass="tituloLabel form-label">Resultado</asp:label>
                     <asp:textbox id="txtResultado" runat="server" CssClass="texto form-control form-control-sm" ImageDirectory=" " Nullable="False" HorizontalAlign="Center" MaxLength="10">
                     </asp:textbox>
                  </div>

                <div class="col-md-3 gx-3 gy-2">
                    <asp:label id="Label8" runat="server" CssClass="tituloLabel form-label">Referência</asp:label>
                    <asp:textbox id="txtReferencia" runat="server" CssClass="texto form-control form-control-sm" 
                                        ImageDirectory=" " Nullable="False" HorizontalAlign="Center" MaxLength="10">
                    </asp:textbox>
                </div>

                <div class="col-md-3 gx-3 gy-2">
                     <asp:label id="Label9" runat="server" CssClass="tituloLabel form-label">IBPM NR 7</asp:label>
                     <asp:textbox id="txtIBPM" runat="server" CssClass="texto form-control form-control-sm"
                                        ImageDirectory=" " Nullable="False" HorizontalAlign="Center" MaxLength="10">
                     </asp:textbox>
                </div>

             </div>
                </div>
                        
                <div class="col-11 mb-3">
                 <div class="row">
                     <div class="col-11 gx-3 gy-2">
                         <asp:label id="Label10" runat="server" CssClass="tituloLabel form-label">Unidade de Medida</asp:label>
                         <asp:DropDownList ID="ddlUnidadeMedida" runat="server" CssClass="texto form-select form-select-sm" Width="200px"></asp:DropDownList>
                        </div>
                </div>
                    </div>
                       

                 
                 </eo:PageView>

       </eo:MultiPage>


        <table>
            <tr>

                    <%-- botões finais --%>

                 <div class="col-12 mb-3">
            <div class="row text-center">
                <div>
                    <asp:Button ID="btnOK" onclick="btnOK_Click" runat="server" CssClass="btn" Text="Gravar" />
                    <asp:Button ID="btnExcluir" onclick="btnExcluir_Click"  runat="server" CssClass="btn" Text="Excluir"/>
                </div>
            </div>
        </div>

        <div class="col-12 text-start"> 
            <asp:Button ID="cmd_Voltar" onclick="cmd_Voltar_Click"  runat="server" CssClass="btn2" Text="Voltar" />
        </div>

                  </div>


     </table>

            <input id="txtAuxiliar" runat="server" name="txtAuxiliar" type="hidden">
                <%--		</form>
	</body>
</HTML>
--%>
            </input>
                <br>
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
