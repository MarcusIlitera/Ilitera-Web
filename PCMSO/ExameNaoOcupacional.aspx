<%@ Page Title="Ilitera.Net" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="True"
    CodeBehind="ExameNaoOcupacional.aspx.cs" Inherits="Ilitera.Net.ExameNaoOcupacional" %>

<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>


<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <link href="css/forms.css" rel="stylesheet" type="text/css" />
    <link href="css/tabelas.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .defaultFont
    {
        width: 586px;
            height: 20px;
        }
        
        .auto-style1 {
            width: 795px;
        }
                
    </style>
</asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent" >
    <div class="container-fluid d-flex ms-5 ps-4">
        <div class="row gx-3 gy-2">

    <eo:CallbackPanel runat="server" id="CallbackPanel1" Triggers="" Width="519px">
	
        <LINK href="scripts/style.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="scripts/validador.js"></script>
		<script language="javascript">
            var jaclicou = 0;
            function AbreCadastro(strPage, IdCliente, IdUsuario) {
                addItemPop(centerWin(strPage + '.aspx?IdEmpresa=' + IdCliente + '&IdUsuario=' + IdUsuario + '', 560, 370, 'CadQueixaClinica'));
            }

            function VerificaProcesso() {
                if (jaclicou == 0)
                    jaclicou = 1;
                else {
                    window.alert("Sua solicitação está sendo processada.\nAguarde!");
                    return false;
                }
            }

            function ConsisteCkeckBoxDeAlteracao(checkBox) {
                tipo = checkBox.id.substring(checkBox.id.length - 1, checkBox.id.length);
                if (tipo == 'S')
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
        </script>

     
	<%--</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0">
		<form id="FormExameNaoOcupacional" method="post" runat="server">--%>
<%--        <igmisc:WebAsyncRefreshPanel ID="warpExameClinico" runat="server" CssClass="defaultFont"
                            Height="" Width="560px" InitializePanel="warpExameClinico_InitializePanel" RefreshComplete="warpExameClinico_RefreshComplete">
--%>

							<eo:TabStrip ID="TabStrip1" runat="server" ControlSkinID="None" 
                            MultiPageID="MultiPage1">
                            <topgroup>
                                <Items>
                                    <eo:TabItem Text-Html="Dados">
                                    </eo:TabItem>
                                    <eo:TabItem Text-Html="Anamnese">
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



             <eo:MultiPage ID="MultiPage1" runat="server" Height="400" Width="1050px">
            
                  <eo:PageView ID="Pageview1" runat="server" Width="1050px">

                      <div class="col-12 subtituloBG mt-3 mb-3" style="padding-top: 10px">
                          <asp:Label ID="Label4" runat="server" CssClass="subtitulo">Exame Ambulatorial</asp:Label>
                          <asp:Label ID="lblEncaminhar" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Red" Text="ENCAMINHAR PARA ATENDIMENTO MÉDICO" Visible="False"></asp:Label>
                      </div>

                      <div class="col-12">
                          <div class="row">
                              <div class="col-md-2 gx-3 gy-2">
                                  <asp:Label ID="Label2" runat="server" CssClass="tituloLabel" SkinID="BoldFont">Data Exame</asp:Label>
                                  <asp:TextBox ID="wdtDataExame" runat="server" CssClass="texto form-control form-control-sm" DisplayModeFormat="dd/MM/yyyy" ImageDirectory=" " Nullable="False"></asp:TextBox>
                              </div>

                              <div class="col-md-2 gx-3 gy-2">
                                  <asp:Label ID="lblAltura" runat="server" CssClass="tituloLabel" SkinID="BoldFont">Altura</asp:Label>
                                  <asp:TextBox ID="wneAltura" runat="server" AutoPostBack="True" CssClass="texto form-control form-control-sm" DataMode="Float" Nullable="False">0</asp:TextBox>
                              </div>

                              <div class="col-md-2 gx-3 gy-2">
                                  <asp:Label ID="lblPeso" runat="server" CssClass="tituloLabel" SkinID="BoldFont">Peso (Kg)</asp:Label>
                                  <asp:TextBox ID="wnePeso" runat="server" AutoPostBack="True" CssClass="texto form-control form-control-sm" DataMode="Float" Nullable="False">0</asp:TextBox>
                              </div>

                              <div class="col-md-2 gx-3 gy-2">
                                  <asp:Label ID="Label1" runat="server" CssClass="tituloLabel" SkinID="BoldFont">IMC</asp:Label>
                                  <asp:TextBox ID="txtIMC" runat="server" AutoPostBack="True" CssClass="texto form-control form-control-sm" DataMode="Float" Nullable="False" ReadOnly="True">0</asp:TextBox>
                              </div>

                              <div class="col-md-2 gx-3 gy-2">
                                  <asp:Label ID="lblPA" runat="server" CssClass="tituloLabel" SkinID="BoldFont">PA (mmHg)</asp:Label>
                                  <asp:TextBox ID="wtePA" runat="server" AutoPostBack="True" CssClass="texto form-control form-control-sm" DataMode="Float" Nullable="False">0</asp:TextBox>
                              </div>

                              <div class="col-md-2 gx-3 gy-2">
                                  <asp:Label ID="lblPulso" runat="server" CssClass="tituloLabel" SkinID="BoldFont">Pulso</asp:Label>
                                  <asp:TextBox ID="wnePulso" runat="server" AutoPostBack="True" CssClass="texto form-control form-control-sm" DataMode="Int" Nullable="False">0</asp:TextBox>
                              </div>

                              <div class="col-md-2 gx-3 gy-2">
                                  <asp:Label ID="lblTemperatura" runat="server" CssClass="tituloLabel" SkinID="BoldFont">Temperatura</asp:Label>
                                  <asp:TextBox ID="wneTemperatura" runat="server" AutoPostBack="True" CssClass="texto form-control form-control-sm" DataMode="Float" Nullable="False">0</asp:TextBox>
                              </div>

                              <div class="col-md-2 gx-3 gy-2">
                                  <asp:Label ID="Label5" runat="server" CssClass="tituloLabel" SkinID="BoldFont">Indíce de Glicose</asp:Label>
                                  <asp:TextBox ID="txtGlicose" MaxLength="20" runat="server" AutoPostBack="True" CssClass="texto form-control form-control-sm" DataMode="Float" Nullable="False">0</asp:TextBox>
                              </div>

                              <div class="col-md-6 gx-3 gy-2">
                                  <div class="row">
                                      <div class="col-12">
                                          <asp:Label ID="Label3" runat="server" CssClass="tituloLabel" SkinID="BoldFont">Tipo de Atendimento</asp:Label>
                                      </div>
                                      
                                      <div class="col-2 gx-1 ms-3 gy-1">
                                          <asp:RadioButton ID="rd_Enfermagem" OnCheckedChanged="rd_Enfermagem_CheckedChanged" runat="server" AutoPostBack="True" CssClass="texto form-check-input bg-transparent border-0" GroupName="1" Text="Enfermagem" />
                                      </div>
                                      
                                      <div class="col-2 gx-4 gy-1">
                                          <asp:RadioButton ID="rd_Medico" OnCheckedChanged="rd_Medico_CheckedChanged" runat="server" CssClass="texto form-check-input bg-transparent border-0" GroupName="1" Text="Médico" AutoPostBack="True" />
                                      </div>
                                      
                                      <div class="col-2 gx-1 gy-1">
                                          <asp:RadioButton ID="rd_Outros" OnCheckedChanged="rd_Outros_CheckedChanged" runat="server" CssClass="texto form-check-input bg-transparent border-0" GroupName="1" Text="Outros" AutoPostBack="True" />
                                      </div>
                                      <asp:CheckBox ID="chk_PacienteCritico" OnCheckedChanged="Chk_PacienteCritico_CheckedChanged" runat="server" AutoPostBack="True" CssClass="boldFont" Text="Paciente Crítico" Visible="False" />
                                  </div>
                              </div>
                          </div>
                      </div>

                      <div class="col-12">
                          <div class="row">
                              <div class="col-md-6 gx-3 gy-2">
                                  <asp:Label ID="lblQueixas" runat="server" CssClass="tituloLabel" SkinID="BoldFont">Sintomas ou Queixas do Colaborador</asp:Label>
                                  <div class="row">
                                      <div class="col-11 gx-1">
                                          <asp:DropDownList ID="ddlQueixas" runat="server" CssClass="texto form-select form-select-sm"></asp:DropDownList>
                                      </div>

                                      <div class="col-1 gx-1">
                                          <input id="btnAddQueixas" runat="server" class="btnMenor" name="btnAddQueixas" type="button" value="..." />
                                      </div>
                                  </div>
                              </div>

                              <div class="col-md-6 gx-3 gy-2">
                                  <asp:Label ID="lblProcedimento" runat="server" CssClass="tituloLabel" SkinID="BoldFont">Procedimento Adotado</asp:Label>
                                  <div class="row">
                                      <div class="col-11 gx-1">
                                          <asp:DropDownList ID="ddlProcedimento" runat="server" CssClass="texto form-select form-select-sm"></asp:DropDownList>
                                      </div>

                                      <div class="col-1 gx-1">
                                          <input id="btnAddProcedimento" runat="server" class="btnMenor" name="btnAddProcedimento" type="button" value="..." />
                                      </div>
                                  </div>
                              </div>

                              <div class="col-md-6 gx-3 gy-2">
                                  <asp:Label ID="lblAnotacoes" runat="server" CssClass="tituloLabel" SkinID="BoldFont">Outras Observações sobre o Atendimento</asp:Label>
                                  <asp:TextBox ID="txtDescricao" runat="server" CssClass="texto form-control" Rows="3" tabIndex="1" TextMode="MultiLine"></asp:TextBox>
                              </div>
                          </div>
                      </div>


                      </eo:PageView>



        <eo:PageView ID="Pageview2" runat="server">

            <div class="col-12 mt-3 mb-3">
                <eo:Grid ID="gridAnamnese" runat="server" ColumnHeaderAscImage="00050403" ColumnHeaderDescImage="00050404" FixedColumnCount="1" 
                                        GridLines="Both" Height="445px" Width="800px" ColumnHeaderDividerOffset="6" ColumnHeaderHeight="30" ItemHeight="30" KeyField="IdAnamneseExame"   
                                    OnItemCommand="gridAnamnese_ItemCommand"   ClientSideOnItemCommand="OnItemCommand" FullRowMode="False" CssClass="grid" >
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
                                        <ColumnHeaderStyle CssClass="tabelaC" />
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
                                                <CellStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500; color: #7D7B7B !important; text-align: left; height: 35px !important; padding: 0.3rem 0.3rem 0.3rem 0.5rem; height: 35px !important;" />
                                            </eo:StaticColumn>

                                            

                                            <eo:CustomColumn HeaderText="Resultado" 
                                                DataField="Resultado" Name="Resultado" ReadOnly="False" 
                                                Width="75" DataType="String"  ClientSideEndEdit="on_end_edit" ClientSideBeginEdit="on_begin_edit"  ClientSideGetText="on_column_gettext" >
                                                <CellStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500; color: #7D7B7B !important; text-align: left; height: 35px !important; padding: 0.3rem 0.3rem 0.3rem 0.5rem; height: 35px !important;" />
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

            </div>

            <div class="col-12 mb-3">
                <div>
                    <asp:Button ID="cmd_Anamnese" onclick="cmd_Anamnese_Click"   runat="server" tabIndex="1" Text="Salvar Alterações na Anamnese" CssClass="btn" />
                    <asp:LinkButton ID="lkbAnamnese" runat="server" SkinID="BoldLinkButton" CssClass="btn"><img border="0" src="Images/printer.svg" style="padding: .2rem;"> Anamnese</img></asp:LinkButton>
                </div>
            </div>
        </eo:PageView>
             </eo:MultiPage>

        <div class="col-12 mb-3">

            <div class="text-center">
                <asp:Button ID="btnOK"  onclick="btnOK_Click"  runat="server" CssClass="btn" Text="OK" />
                <asp:Button ID="btnExcluir" onclick="btnExcluir_Click"   runat="server" CssClass="btn" Text="Excluir" />
            </div>

            <div class="text-start">
                <asp:Button ID="cmd_Voltar" onclick="cmd_Voltar_Click"  runat="server" CssClass="btn2" Text="Voltar" />
            </div>
        </div>

        <asp:Button ID="btnemp"  onclick="btnemp_Click" runat="server" CssClass="buttonBox" Height="18px" Text="Anamnese em Branco" Visible="False" Width="140px" />
        <input id="txtAuxAviso" type="hidden" runat="server"/>
        <input id="txtCloseWindow" type="hidden"  runat="server"/>
        <input id="txtExecutePost" type="hidden" runat="server"/>
        <input id="txtAuxiliar" runat="server" name="txtAuxiliar" type="hidden" />
        <asp:Label ID="lblMedico" runat="server" CssClass="boldFont" SkinID="BoldFont" Visible="False">Médico responsável pelo atendimento:</asp:Label>
        <asp:DropDownList ID="ddlMedico" runat="server" Font-Size="X-Small" Height="16px" Visible="False" Width="268px"></asp:DropDownList>
                           
                    <%--    </igmisc:WebAsyncRefreshPanel>--%>
                
                <%--</igmisc:WebAsyncRefreshPanel>--%>
            

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