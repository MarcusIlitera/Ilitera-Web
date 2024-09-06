<%@ Page Title="Ilitera.Net" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="True"
    CodeBehind="visualizarDadosEmpregado_Novo2.aspx.cs" Inherits="Ilitera.Net.visualizarDadosEmpregado_Novo2" EnableEventValidation="false"  ValidateRequest="false"  %>

<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>


<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <title>Edição do Colaborador</title>
    <link href="css/forms.css" rel="stylesheet" type="text/css" />
    <link href="css/tabelas.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        #ctl00_MainContent_grd_Empregados_c0_sort, #ctl00_MainContent_grd_Empregados_c1_sort, #ctl00_MainContent_grd_Empregados_c2_sort, #ctl00_MainContent_grd_Empregados_c3_sort, #ctl00_MainContent_grd_Empregados_c5_sort{
            width: 12px;
            opacity: 35%;
            padding-top: 0.3rem;
        }

        .style6
        {
            width: 64px; height: 8px; text-align:left
        }
        
        .style5
        {
            width: 64px;
        }
        .buttonBox
{
	background: #3E684D center top;
	font: bold 9px Verdana, Arial, Helvetica, sans-serif, Tahoma;
	color:White;
	border-top: thin solid #597C4E;
	border-right: thin solid #23361F;
	border-bottom: thin solid #23361F;
	border-left: thin solid #597C4E;
    }
    
.inputBox
{
	font: xx-small Verdana;
	border: 1px solid #7CC5A1;
	color: #004000;
	background-color: #FCFEFD;
	text-align: left;
	
}

        .inputBox
{
	font: xx-small Verdana;
	border: 1px solid #7CC5A1;
	color: #004000;
	background-color: #FCFEFD;
	text-align: left;
	
}


.boldFont
{
	font: Bold xx-small Verdana;
	color:#44926D;
}

.boldFont
{
	font: Bold xx-small Verdana;
	color:#44926D;
}

    </style>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">

<div class="container-fluid d-flex ms-5 ps-4">
    

    <eo:CallbackPanel runat="server" id="CallbackPanel1" Triggers="">
        <div class="row gx-2 gy-2">

<script language="javascript">

    function callme(oFile) {
        document.getElementById("txt_Arq").value = oFile.value;
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


         <%-- PRIMEIRO CONTAINER --%>

            <%-- LINHA UM --%>
            <div class="col-9">
                <div class="row">
                     <div class="col-md-4 gr-3 gy-2">
                        <fieldset>
                            <asp:Label ID="lblNome" runat="server" Text="Nome" class="tituloLabel form-label col-form-label col-form-label-sm" Style="text-align: left;"></asp:Label>
                            <asp:Literal runat="server"><br /></asp:Literal>
                            <asp:textbox ID="txtNomeEmpregado" runat="server" ontextchanged="txtNomeEmpregado_TextChanged"  class="texto form-control form-control-sm" Style="text-align: left;"></asp:textbox>
                            <asp:Label ID="lblNomeEmpregado" runat="server" Font-Size="XX-Small" Visible="False"></asp:Label>
                        </fieldset>
                   </div>
           
                    <div class="col-md-2 gx-3 gy-2">
                        <fieldset>
                            <asp:Label ID="lblNascimento" runat="server" Text="Nascimento" class="tituloLabel form-label col-form-label col-form-label-sm" Style="text-align: left;"></asp:Label>
                            <asp:Literal runat="server"><br /></asp:Literal>
                            <asp:textbox ID="txtDataNascimento" runat="server" class="texto form-control form-control-sm" type="date" Style="text-align: left;"></asp:textbox>
                        </fieldset>
                   </div>
          
                    <div class="col-md-2 gx-3 gy-2">
                        <fieldset>
                            <asp:Label ID="lblSexo" runat="server" Text="Sexo" class="tituloLabel form-label col-form-label col-form-label-sm" Style="text-align: left;"></asp:Label>
                            <asp:Literal runat="server"><br /></asp:Literal>
                            <asp:DropDownList ID="cbSexo" runat="server" class="texto form-select form-select-sm" Style="text-align: left;">
                                <asp:ListItem Selected="True">-</asp:ListItem>
                                <asp:ListItem Value="M">Masculino</asp:ListItem>
                                <asp:ListItem Value="F">Feminino</asp:ListItem>
                            </asp:DropDownList>
                        </fieldset>
                   </div>

                   <div class="col-md-2 gx-3 gy-2">
                        <fieldset>
                            <asp:Label ID="lblIdentidade" runat="server" Text="Identidade" class="tituloLabel form-label col-form-label col-form-label-sm" Style="text-align: left;"></asp:Label>
                            <asp:Literal runat="server"><br /></asp:Literal>
                            <asp:textbox ID="txtRG" runat="server" class="texto form-control form-control-sm" Style="text-align: left;"></asp:textbox>
                        </fieldset>
                   </div>

                   <div class="col-md-2 gx-3 gy-2" style="margin-bottom:5px;">
                        <fieldset>
                            <asp:Label ID="lblCPF" runat="server" Text="CPF" class="tituloLabel form-label col-form-label col-form-label-sm" Style="text-align: left;"></asp:Label>
                            <asp:Literal runat="server"><br /></asp:Literal>
                            <asp:textbox ID="txtCPF" runat="server" class="texto form-control form-control-sm" Style="text-align: left;"></asp:textbox>
                        </fieldset>
                   </div>

                    <%-- LINHA 2 --%>

                    <div class="col-md-3 gx-r gy-2">
                        <fieldset>
                            <asp:Label ID="lblMatricula" runat="server" Text="RE(Matrícula)" class="tituloLabel form-label col-form-label col-form-label-sm" Style="text-align: left;"></asp:Label>
                            <asp:Literal runat="server"><br /></asp:Literal>
                            <asp:textbox ID="txtMatricula" runat="server" class="texto form-control form-control-sm" Style="text-align: left;"></asp:textbox>
                        </fieldset>
                   </div>

                   <div class="col-md-3 gx-3 gy-2">
                        <fieldset>
                            <asp:Label ID="lblPISPASEP" runat="server" Text="PIS/PASEP" class="tituloLabel form-label col-form-label col-form-label-sm" Style="text-align: left;"></asp:Label>
                            <asp:Literal runat="server"><br /></asp:Literal>
                            <asp:textbox ID="txtPISPASEP" runat="server" class="texto form-control form-control-sm" Style="text-align: left;"></asp:textbox>
                        </fieldset>
                   </div>

                   <div class="col-md-2 gx-3 gy-2">
                        <fieldset>
                            <asp:Label ID="lblCTPS_Num" runat="server" Text="CTPS" class="tituloLabel form-label col-form-label col-form-label-sm" Style="text-align: left;"></asp:Label>
                            <asp:Literal runat="server"><br /></asp:Literal>
                            <asp:textbox ID="txtCTPS_Num" runat="server" class="texto form-control form-control-sm" Style="text-align: left;"></asp:textbox>
                        </fieldset>
                   </div>

                    <div class="col-md-2 gx-3 gy-2">
                        <fieldset>
                            <asp:Label ID="lblCTPS_SERIE" runat="server" Text="Série" class="tituloLabel form-label col-form-label col-form-label-sm" Style="text-align: left;"></asp:Label>
                            <asp:Literal runat="server"><br /></asp:Literal>
                            <asp:textbox ID="txtCTPS_Serie" runat="server" class="texto form-control form-control-sm" Style="text-align: left;"></asp:textbox>
                        </fieldset>
                   </div>

                    <div class="col-md-2 gx-3 gy-2" style="margin-bottom:5px;">
                        <fieldset>
                            <asp:Label ID="lblCTPS_UF" runat="server" Text="UF" class="tituloLabel form-label col-form-label col-form-label-sm" Style="text-align: left;"></asp:Label>
                            <asp:Literal runat="server"><br /></asp:Literal>
                            <asp:textbox ID="txtCTPS_UF" runat="server" class="texto form-control form-control-sm" Style="text-align: left;"></asp:textbox>
                        </fieldset>
                   </div>

                  <%-- TERCEIRA LINHA --%>
                    <div class="col-md-2 gx-r gy-2">
                        <fieldset>
                            <asp:Label ID="lblDataDemissao" runat="server" Text="Admissão" class="tituloLabel form-label col-form-label col-form-label-sm" Style="text-align: left;"></asp:Label>
                            <asp:Literal runat="server"><br /></asp:Literal>
                            <asp:textbox ID="txtDataAdmissao" runat="server" class="texto form-control form-control-sm" type="date" Style="text-align: left;"></asp:textbox>
                        </fieldset>
                   </div>

                   <div class="col-md-2 gx-3 gy-2">
                        <fieldset>
                            <asp:Label ID="lblDemissao" runat="server" Text="Demissão" class="tituloLabel form-label col-form-label col-form-label-sm" Style="text-align: left;"></asp:Label>
<asp:Label ID="lblDemissao0" runat="server" Text="e-Mail" Visible="false"></asp:Label>
                            <asp:Literal runat="server"><br /></asp:Literal>
                            <asp:textbox ID="txtDataDemissao" runat="server" class="texto form-control form-control-sm" type="date" Style="text-align: left;"></asp:textbox>
                        </fieldset>
                   </div>

                   <div class="col-md-4 gx-3 gy-2">
                        <fieldset>
                            <asp:Label ID="lblApelido" runat="server" Text="Apelido" class="tituloLabel form-label col-form-label col-form-label-sm" Style="text-align: left;"></asp:Label>
                            <asp:Literal runat="server"><br /></asp:Literal>
                            <asp:textbox ID="txtApelidoEmpregado" runat="server" class="texto form-control form-control-sm" Style="text-align: left;"></asp:textbox>
                        </fieldset>
                   </div>

                   <div class="col-md-4 gx-3 gy-2" style="margin-bottom:5px;">
                        <fieldset>
                            <asp:Label runat="server" Text="E-Mail" class="tituloLabel form-label col-form-label col-form-label-sm" Style="text-align: left;"></asp:Label>
                            <asp:Literal runat="server"><br /></asp:Literal>
                            <asp:textbox ID="txteMail" runat="server" class="texto form-control form-control-sm" Style="text-align: left;"></asp:textbox>
                        </fieldset>
                   </div>

                    <div class="col-md-5 gy-2">
                        <fieldset>
                            <asp:Label ID="lblApelido0" runat="server" Text="Observação" class="tituloLabel form-label col-form-label col-form-label-sm" Style="text-align: left;"></asp:Label>
                            <asp:Literal runat="server"><br /></asp:Literal>
                            <asp:textbox ID="txtObs" runat="server" class="texto form-control form-control-sm" TextMode="MultiLine" Style="text-align: left;"></asp:textbox>
                        </fieldset>
                   </div>
                   <div class="col-md-2 gx-5" style="margin-top: 42px;">
                       <fieldset>
                           <asp:CheckBox ID="chk_Terceirizado" runat="server" Text="Terceirizado / Estagiário" CssClass="titulo2"></asp:CheckBox>
                       </fieldset>
                   </div>
                   <div class="col-md-5 gx-3" style="margin-top: 36px;">
                       <asp:Button ID="cmd_Acidente" onclick="cmd_Acidente_Click"  runat="server" CssClass="btn" Text="Acidentes" />
                       <asp:Button ID="cmd_Absenteismo" onclick="cmd_Absentismo_Click" runat="server" CssClass="btn" Text="Absenteísmo" />
                   </div>

                </div>
               
            </div>

            <div class="col-3 gx-3 gy-2">
                <div class="card text-center border-0" style="width: 150px; background: #f6f6f6;">
                    <div class="card-header border-0">
                        <asp:Image runat="server" ID="ImgFunc" Height="125" Width="100" Style="border: 1px solid #B0ABAB; border-radius: 4px;" ></asp:Image>
                    </div>
                    <div class="card-header border-0 justify-content-center">
                        <asp:Label ID="Label23" runat="server" Text="Nº da Foto" class="tituloLabel form-label col-form-label col-form-label-sm text-center"></asp:Label>
                        <asp:Literal runat="server"><br /></asp:Literal>
                        <asp:textbox ID="txtFoto" runat="server" class="texto form-control form-control-sm text-center"></asp:textbox>
                    </div>
                    <div class="card-footer border-0">
                        <asp:Button ID="cmd_Refresh" runat="server" CssClass="btn" Text="Recarregar" onclick="cmd_Refresh_Click"  />
                    </div>
                </div>
            </div>

            <%-- BOTÕES PÁGINAS --%>
    <eo:TabStrip ID="TabStrip1" runat="server" ControlSkinID="None" Width="1000px" OnItemClick="TabStrip1_ItemClick"
        MultiPageID="MultiPage1">
        <LookItems>
            <eo:TabItem ItemID="_Default"
                NormalStyle-CssText="font-family:'Ubuntu'; font-size: 12px; color: #B0ABAB; padding: 10px 10px; background: #F1F1F1; border-radius: 8px; cursor: hand; width: fit-content; margin-right: 1rem;"
                SelectedStyle-CssText="font-family:'Ubuntu'; font-size: 12px; color: #1C9489; font-weight: bold; padding: 10px 15px; background: #D9D9D9; border-radius: 8px; cursor: hand; width: fit-content; margin-right: 1rem;">
                <SubGroup OverlapDepth="8" ItemSpacing="5"
                    Style-CssText="font-family:'Ubuntu'; font-size: 12px; color: #B0ABAB; padding: 10px 10px 10px 0px; border-radius: 8px; cursor: hand; width: fit-content;">
                </SubGroup>
            </eo:TabItem>
        </LookItems>
        <TopGroup>
            <Items>
                <eo:TabItem Text-Html="Classificação Funcional">
                </eo:TabItem>

                <eo:TabItem Text-Html="Descrição Função">

                </eo:TabItem>

                <eo:TabItem Text-Html="Uniformes">

                </eo:TabItem>

                <eo:TabItem Text-Html="Img.Colaborador">

                </eo:TabItem>

                <eo:TabItem Text-Html="Relatórios">

                </eo:TabItem>

                <eo:TabItem Text-Html="Beneficiário">

                </eo:TabItem>

                <eo:TabItem Text-Html="Endereço">

                </eo:TabItem>

                <eo:TabItem Text-Html="Ativ.Especiais">

                </eo:TabItem>

                <eo:TabItem Text-Html="eSocial">

                </eo:TabItem>

                <eo:TabItem Text-Html="Transferência">

                </eo:TabItem>

            </Items>
        </TopGroup>
    </eo:TabStrip>

    <eo:MultiPage ID="MultiPage1" runat="server" Height="200" Width="1050px">
              <eo:PageView ID="Pageview1" runat="server" Width="910px">


              <%-- TABELA / CLASSIFICAÇÃO FUNCIONAL  --%>
                    <eo:Grid ID="grd_Empregados" runat="server" FixedColumnCount="1" ColumnHeaderDividerOffset="6" 
                    ColumnHeaderAscImage="00050403" ColumnHeaderDescImage="00050404"
                    GridLines="Both" PageSize="500" KeyField="IdClassificacaoFuncional"   CssClass="grid"
                    ColumnHeaderHeight="30" ItemHeight="30" Height="250px" Width="910px"
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
                    <CellStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500; color: #7D7B7B !important; text-align: left; height: 35px !important; padding: 0.3rem 0.3rem 0.3rem 0.5rem; height: 35px !important;" />

                </eo:GridItemStyleSet>
            </ItemStyles>
            <ColumnHeaderTextStyle CssText="" />
            <ColumnHeaderStyle CssClass="tabelaC colunas"/>
            <Columns>               
                <eo:StaticColumn HeaderText="Função" AllowSort="True" 
                    DataField="Funcao" Name="Funcao" ReadOnly="True" 
                    SortOrder="Ascending" Text="" Width="250" >
                    <CellStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500; color: #7D7B7B !important; text-align: left; height: 35px !important; padding: 0.3rem 0.3rem 0.3rem 0.5rem; height: 35px !important;" />
                </eo:StaticColumn>
                <eo:StaticColumn HeaderText="Setor" AllowSort="True" 
                    DataField="Setor" Name="Setor" ReadOnly="True" 
                    Width="220">
                    <CellStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500; color: #7D7B7B !important; text-align: left; height: 35px !important; padding: 0.3rem 0.3rem 0.3rem 0.5rem; height: 35px !important;" />
                </eo:StaticColumn>
                <eo:StaticColumn HeaderText="Data Início" AllowSort="True" 
                    DataField="InicioFuncao" Name="InicioFuncao" ReadOnly="True" 
                    Width="100">
                    <CellStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500; color: #7D7B7B !important; text-align: left; height: 35px !important; padding: 0.3rem 0.3rem 0.3rem 0.5rem; height: 35px !important;" />
                </eo:StaticColumn>
                <eo:StaticColumn HeaderText="Data Término" AllowSort="True" 
                    DataField="TerminoFuncao" Name="TerminoFuncao" ReadOnly="True" 
                    Width="100">
                    <CellStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500; color: #7D7B7B !important; text-align: left; height: 35px !important; padding: 0.3rem 0.3rem 0.3rem 0.5rem; height: 35px !important;" />
                </eo:StaticColumn>
                <eo:StaticColumn HeaderText="Centro de Custo" AllowSort="True" 
                    DataField="tCentro_Custo" Name="tCentro_Custo" ReadOnly="True" 
                    Width="0">
                    <CellStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500; color: #7D7B7B !important; text-align: left; height: 35px !important; padding: 0.3rem 0.3rem 0.3rem 0.5rem; height: 35px !important;" />
                </eo:StaticColumn>             
                <eo:StaticColumn HeaderText="Local de Trabalho" AllowSort="True" 
                    DataField="NomeAlocado" Name="NomeAlocado" ReadOnly="True" 
                    Width="190">
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
                                    <table class="container d-flex">
                                    <tr class="row gx-2 gy-2" style="margin-bottom: 15px;">
                                    <td class="col-12">
                                        <asp:Button ID="cmd_Novo" onclick="cmd_Novo_Click"  runat="server" CssClass="btn" Text="Nova Classificação" />
                                        <asp:Button ID="cmd_Editar_Setor_Funcao" onclick="cmd_Editar_Setor_Funcao_Cargo_Click"  runat="server" CssClass="btn" Text="Editar Setor/Função/Cargo" />
                                      </td>
                                      </tr>
                                      </table>

                                      <table class="container d-flex">
                                                                                 
                                           
                                        <tr class="row gx-2 gy-2">
                                            <td class="col-md-6">
                                                <asp:Label ID="lblInicioFuncao" runat="server" class="tituloLabel form-label col-form-label col-form-label-sm" Text="Início da Função"></asp:Label>
                                                <asp:Literal runat="server"><br /></asp:Literal>
                                                <asp:TextBox ID="txtInicioFuncao" runat="server" class="texto form-control form-control-sm" Width="150px"></asp:TextBox>
                                            </td>

                                            <td class="col-md-6">
                                                <asp:Label ID="lblTerminoFuncao" runat="server" class="tituloLabel form-label col-form-label col-form-label-sm" Text="Término da Função"></asp:Label>
                                                <asp:Literal runat="server"><br /></asp:Literal>
                                                <asp:TextBox ID="txtTerminoFuncao" runat="server" class="texto form-control form-control-sm" Width="150px"></asp:TextBox>
                                                <asp:Label ID="lbl_Local_Trabalho" runat="server" Text="0" Visible="False"></asp:Label>
                                            </td>

                                            <td>
                                                <asp:Label ID="lbl_Id" runat="server" Text="0" Visible="False"></asp:Label>
                                            </td>

                                            <td class="col-md-6">
                                                <asp:Label ID="lblFuncao" runat="server" class="tituloLabel form-label col-form-label col-form-label-sm" Text="Função"></asp:Label>
                                                <asp:Literal runat="server"><br /></asp:Literal>
                                                <asp:TextBox ID="txtFuncao" runat="server" class="texto form-control form-control-sm mb-1" Width="350px"></asp:TextBox>
                                                <asp:DropDownList ID="cmb_Funcao1" runat="server" AutoPostBack="True" 
                                                    class="texto form-select form-select-sm" Height="28px" Width="350px" >
                                                </asp:DropDownList>
                                            </td>
                                            <td class="col-md-6">
                                                <asp:Label ID="lblFuncao0" runat="server" class="tituloLabel form-label col-form-label col-form-label-sm" Text="Centro Custo"></asp:Label>
                                                <asp:Literal runat="server"><br /></asp:Literal>
                                                <asp:TextBox ID="txtCC" runat="server" class="texto form-control form-control-sm mb-1" MaxLength="20" 
                                                    Width="350px"></asp:TextBox>                                              


                                                <asp:ListBox ID="lst_Sel_1_Id" runat="server" BackColor="#FFCC99" 
                                                    Font-Names="Courier New" Font-Size="X-Small" Height="5px" Visible="False" 
                                                    Width="364px"></asp:ListBox>
                                                <asp:ListBox ID="lst_Sel_1_Cop" runat="server" BackColor="#FFCC99" 
                                                    Font-Names="Courier New" Font-Size="X-Small" Height="5px" Visible="False" 
                                                    Width="364px"></asp:ListBox>
                                                <asp:ListBox ID="lst_1_ID" runat="server" BackColor="#FFCC99" 
                                                    Font-Names="Courier New" Font-Size="X-Small" Height="5px" Visible="False" 
                                                    Width="364px"></asp:ListBox>
                                                <asp:ListBox ID="lst_Sel_1" runat="server" BackColor="#FFCC99" 
                                                    Font-Names="Courier New" Font-Size="X-Small" Height="5px" Visible="False" 
                                                    Width="364px"></asp:ListBox>
                                               


                                                </td>
                                        </tr>
                                        <tr class="row gx-3 gy-2">
                                            <td class="col-md-6">
                                                <asp:Label ID="lblSetor" runat="server" class="tituloLabel form-label col-form-label col-form-label-sm" Style="text-align: left;" Text="Setor"></asp:Label>
                                                <asp:Literal runat="server"><br /></asp:Literal>
                                                <asp:TextBox ID="txtSetor" runat="server" class="texto form-control form-control-sm mb-1" width="350px">
                                                </asp:TextBox>
                                                <asp:DropDownList ID="cmb_Setor1" runat="server" AutoPostBack="True" class="texto form-select form-select-sm" Height="28px" width="350px">
                                                </asp:DropDownList>
                                            </td>
                                            <td class="col-md-6">
                                                <asp:Label ID="lblCargo" runat="server" class="tituloLabel form-label col-form-label col-form-label-sm" Style="text-align: left;" Text="Cargo"></asp:Label>
                                                <asp:Literal runat="server"><br /></asp:Literal>
                                                <asp:TextBox ID="txtCargo" runat="server" class="texto form-control form-control-sm mb-1" width="350px">
                                                </asp:TextBox>
                                                <asp:DropDownList ID="cmb_Cargo1" runat="server" AutoPostBack="True" class="texto form-select form-select-sm" Height="28px" width="350px">
                                                </asp:DropDownList>
                                            </td>
                                            <tr class="row gx-3 gy-2">
                                                <td class="col-md-6">
                                                    <asp:Label ID="lblSetor0" runat="server" class="tituloLabel form-label col-form-label col-form-label-sm" Style="text-align: left;" Text="Local de Trabalho"></asp:Label>
                                                    <asp:Literal runat="server"><br /></asp:Literal>
                                                    <asp:TextBox ID="txtLocalTrabalho" runat="server" class="texto form-control form-control-sm mb-1" width="350px">
                                                </asp:TextBox>
                                                    <asp:DropDownList ID="cmbLocalTrabalho" OnSelectedIndexChanged="cmbLocalTrabalho_SelectedIndexChanged"   runat="server" AutoPostBack="True" class="texto form-select form-select-sm" Height="28px" width="350px">
                                                    </asp:DropDownList>
                                                    <asp:ListBox ID="lstLocalTrabalho" runat="server" Font-Size="Smaller" Height="16px" Visible="False"></asp:ListBox>
                                                </td>
                                                <td class="col-md-6">
                                                    <asp:Label ID="Label2" runat="server" class="tituloLabel form-label col-form-label col-form-label-sm" Style="text-align: left;" Text="Jornada"></asp:Label>
                                                    <asp:Literal runat="server"><br /></asp:Literal>
                                                    <asp:TextBox ID="txtJornada" runat="server" class="texto form-control form-control-sm mb-1" width="350px">
                                                </asp:TextBox>
                                                    <asp:DropDownList ID="cmb_Jornada" runat="server" AutoPostBack="True" class="texto form-select form-select-sm" Height="28px" width="350px">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>

                                        </tr>

                                          </tr>

                                         <tr>
                                            <td class="style8">                                                
                                            <asp:Label ID="lbl_Id_Empresa" runat="server" Text="IdEmpresa" Visible="False"></asp:Label>
                                            <asp:Label ID="lbl_Id_Usuario" runat="server" Text="IdUsuario" Visible="False"></asp:Label>
                                            <asp:Button ID="cmd_Alterar1" onclick="cmd_Alterar1_Click"  runat="server" Text="Salvar Dados" CssClass="btn" Visible="False" />
                                           </td>
                                           <td class="style8">
                                                <asp:Button ID="cmd_Excluir1" runat="server" onclick="cmd_Excluir1_Click" Text="Excluir Classif.Func." CssClass="btn" Visible="False" />

                                           </td>

                                           <td class="style8">
                                           
                                               <asp:Button ID="cmdGHE_1" runat="server" onclick="cmdGHE_1_Click" Text="Mudar GHE" CssClass="btn" Visible="False" />
                                           
                                           </td>

                                           <td class="style8">
                                           
                                           
                                               
                                               <asp:ListBox ID="lst_1" runat="server" BackColor="#CCFFFF" onselectedindexchanged="lst_1_SelectedIndexChanged"
                                                   Font-Names="Courier New" Font-Size="X-Small" 
                                                   Width="364px" Visible="False" Height="5px">
                                               </asp:ListBox>
                                               <br />
                                               <asp:Label ID="lbl_Id_1" runat="server" Text="lbl_Id_1" Visible="False"></asp:Label>
                                               <asp:Button ID="cmd_Add_1" runat="server" BackColor="#CCCCCC" onclick="cmd_Add_1_Click" 
                                                   Font-Size="XX-Small" Height="18px" Text="Add" 
                                                   Width="86px" Visible="False" />
                                               &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                               <asp:Button ID="cmd_Remove_1" runat="server" BackColor="#CCCCCC" onclick="cmd_Remove_1_Click" 
                                                   Font-Size="XX-Small" Height="18px" Text="Remove" 
                                                   Width="86px" Visible="False" />
                                               <br />
                                           
                                           
                                           </td>

                                        </tr>


                                          <tr class="row gx-3 gy-2">
                                                                     
                                            <td class="col-md-6">
                                                <asp:Label ID="lbl_PPRA0" runat="server" class="tituloLabel form-label col-form-label col-form-label-sm"
                                                    Text="Selecione PPRA"></asp:Label>
                                                    <asp:Literal runat="server"><br /></asp:Literal>
                                                     <asp:ListBox ID="lst_PPRA" runat="server" onselectedindexchanged="lst_PPRA_SelectedIndexChanged" AutoPostBack="True" class="texto form-control form-control-sm"></asp:ListBox>
                                                      <asp:ListBox ID="lst_Id_PPRA" runat="server" onselectedindexchanged="lst_PPRA_SelectedIndexChanged"  AutoPostBack="True" Font-Bold="True" Font-Names="Cordia New" Font-Size="Small" Height="20px" Visible="False" Width="99px"></asp:ListBox>
</td>
                                            <td>    
                                            </td>
                                            <td class="style8">    
                                                <asp:Label ID="lbl_Selecao" runat="server" class="tituloLabel form-label col-form-label col-form-label-sm"
                                                    Text="Lista de GHE do Laudo Selecionado"></asp:Label>
                                                <asp:Literal runat="server"><br /></asp:Literal>

                                                <asp:ListBox ID="lst_11" runat="server" Cssclass="texto form-control form-control-sm mb-2"
                                                    Visible="False"></asp:ListBox>
                             
                                                <asp:Button ID="cmd_Add11" runat="server" onclick="cmd_Add11_Click" 
                                                    CssClass="btn mb-2" Text="Selecionar" 
                                                    Visible="False"/>
                                                
                                                <asp:Button ID="cmd_Remove11" runat="server" onclick="cmd_Remove11_Click"
                                                    CssClass="btn mb-2" Text="Remover" 
                                                    Visible="False" />
                                               
                                                <asp:Label ID="lbl_Selecionado" runat="server" class="tituloLabel form-label col-form-label col-form-label-sm"
                                                    Text="GHE selecionado para este laudo"></asp:Label>
                                               
                                                <asp:ListBox ID="lst_Sel_11" runat="server" Cssclass="texto form-control form-control-sm mb-2"
                                                    Visible="False"></asp:ListBox>

                                                <asp:ListBox ID="lst_Sel_11_Cop" runat="server" Cssclass="texto form-control form-control-sm mb-2"
                                                    Visible="False"></asp:ListBox>
                                              
                                                <asp:ListBox ID="lst_11_ID" runat="server" Cssclass="texto form-control form-control-sm mb-2"
                                                    Visible="False"></asp:ListBox>

                                                <asp:ListBox ID="lst_Sel_11_Id" runat="server" Cssclass="texto form-control form-control-sm mb-2"
                                                    Visible="False"></asp:ListBox>

                                                <asp:ListBox ID="ListBox9" runat="server" class="texto form-control form-control-sm mb-2">
                                                    
                                                    </asp:ListBox>
                                            </td>

                                          </tr>
                                    </table>
              </eo:PageView>

        <%-- DESCRIÇÃO DA FUNÇÃO --%>
              <eo:PageView ID="Pageview2" runat="server">
                                
                                <div class="container d-flex">
                                    <div class="row gx-2 gy-2">
                                        <div class="col-md-8">
                                            <asp:Label ID="Label24" runat="server" 
                                                Text="Função" class="tituloLabel form-label col-form-label col-form-label-sm"></asp:Label>
                                            <asp:Literal runat="server"><br /></asp:Literal>
                                            <asp:DropDownList ID="cmb_Setor" runat="server" AutoPostBack="True" onselectedindexchanged="cmb_Setor_SelectedIndexChanged" 
                                                class="texto form-select form-select-sm" Height="28px" Width="480px">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="cmb_SetorD" runat="server" Font-Size="X-Small" onselectedindexchanged="cmb_Setor_SelectedIndexChanged" 
                                                Height="16px"
                                                Visible="False" Width="378px">
                                            </asp:DropDownList>
                                            <br />
                                            <asp:DropDownList ID="cmb_CBOID" runat="server" Font-Size="X-Small" onselectedindexchanged="cmb_Setor_SelectedIndexChanged" 
                                                Height="16px" 
                                                Visible="False" Width="378px">
                                            </asp:DropDownList>
                                            <br />
                                            <asp:DropDownList ID="cmb_SetorID" runat="server" Font-Size="X-Small" onselectedindexchanged="cmb_Setor_SelectedIndexChanged" 
                                                Height="16px"
                                                Visible="False" Width="378px">
                                            </asp:DropDownList>
                                        </div>

                                        <div class="col-md-4">
                                            <asp:Label runat="server" Text="CBO da Função" CssClass="tituloLabel form-label col-form-label col-form-label-sm"></asp:Label>
                                            <asp:Literal runat="server"><br /></asp:Literal>
                                            <asp:TextBox ID="txt_CBO" ontextchanged="txt_CBO_TextChanged"  runat="server" AutoPostBack="True" Width="350px" class="texto form-control form-control-sm mb-1"></asp:TextBox>
                                            <asp:DropDownList ID="cmb_CBO" onselectedindexchanged="cmb_CBO_SelectedIndexChanged" runat="server" AutoPostBack="True" 
                                                class="texto form-select form-select-sm" Height="28px" Width="350px">
                                            </asp:DropDownList>
                                        </div>
                                            
                                        <div class="col-12 mb-2">
                                            <asp:TextBox ID="txt_SetorAlt" runat="server" class="texto form-control form-control-sm"
                                            TextMode="MultiLine"></asp:TextBox>
                                        </div>

                                        <div class="col-12 text-left">
                                            <asp:Button ID="cmd_Atualizar_DFuncao" onclick="cmd_Atualizar_DFuncao_Click"  runat="server" CssClass="btn" Text="Salvar alterações na descrição do Setor" />
                                        </div>
                                    </div>
                                </div>
                                    



              </eo:PageView>

        <%-- UNIFORMES --%>
              <eo:PageView ID="Pageview3" runat="server">
                  <div class="container d-flex">
                      <div class="row gy-2">
                          <div class="col-12">
                              <div class="row headerLista">
                                  <div class="col-md-4 listaTexto">
                                      Uniforme
                                  </div>
                                  <div class="col-md-4 listaTexto">
                                      Medida
                                  </div>
                                  <div class="col-md-4 listaTexto">
                                      Valor
                                  </div>
                              </div>
                          </div>

                          <div class="col-12 mb-1">
                              <asp:ListBox ID="lst_Uniformes" runat="server" class="texto" Height="150px" Width="1050px"></asp:ListBox>
                          </div>
                          <div class="col-12 mb-2">
                              <div class="row">
                                  <div class="col-md-2">
                                      <asp:Label ID="lblFornecimento" runat="server" class="tituloLabel form-label col-form-label col-form-label-sm" 
                                          Text="Data de Fornecimento"></asp:Label>
                                      <asp:Literal runat="server"><br /></asp:Literal>
                                      <asp:TextBox ID="txtMateriais" runat="server" class="texto form-control form-control-sm" MaxLength="10" ></asp:TextBox>                                        
                                  </div>

                                  <div class="col-md-2">
                                      <asp:Label ID="lblFornecimento0" runat="server" class="tituloLabel form-label col-form-label col-form-label-sm" 
                                          Text="Quantidade"></asp:Label>
                                      <asp:Literal runat="server"><br /></asp:Literal>
                                      <asp:TextBox ID="txtQtde" runat="server" class="texto form-control form-control-sm" MaxLength="3">1</asp:TextBox>
                                  </div>

                                  <div class="col-md-2 mt-3">
                                      <asp:Button ID="btnAdd" onclick="btnAdd_Click"  runat="server" CssClass="btn" Text="Adicionar" />
                                  </div>
                              </div>
                          </div>
                          <asp:ListBox ID="lst_Id_Uniforme_Medida" runat="server" Visible="False"></asp:ListBox>
                          <asp:ListBox ID="lst_Id_Uniforme" runat="server" Visible="False"></asp:ListBox>
                          <div class="col-12">
                              <div class="row headerLista">
                                  <div class="col-md-1 listaTexto">
                                      Data
                                  </div>
                                  <div class="col-md-1 listaTexto">
                                      Qtde.
                                  </div>
                                  <div class="col-md-4 listaTexto">
                                      Uniforme
                                  </div>
                                  <div class="col-md-3 listaTexto">
                                      Medida
                                  </div>
                                  <div class="col-md-3 listaTexto">
                                      Valor
                                  </div>
                              </div>
                          </div>

                          <div class="col-12">
                              <asp:ListBox ID="lst_Sel_Uni" runat="server" class="texto" Height="150px" Width="1050px"></asp:ListBox>
                          </div>

                          <div class="col-12 mb-1">
                              <asp:Button ID="btnRemove" onclick="btnRemove_Click"  runat="server" CssClass="btn2" Text="Remover" />
                              <asp:Button ID="cmdRecibo" onclick="cmdRecibo_Click"  runat="server" CssClass="btn" Text="Imprimir recibo" />
                          </div>
                          <asp:ListBox ID="lst_Id_Uniforme_Medida_Sel" runat="server" Visible="False">
                          </asp:ListBox>
                          <asp:ListBox ID="lst_Id_Uniforme_Sel" runat="server" Visible="False">
                          </asp:ListBox>
                      </div>
                  </div>                                                                                                           

              </eo:PageView>

        <%-- IMAGEM COLABORADOR --%>
              <eo:PageView ID="Pageview4" runat="server">

                               <%-- <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="conditional">
                                 <Triggers>
                                  <asp:PostBackTrigger ControlID="Button1" />
                                 </Triggers>
                                 <ContentTemplate>      --%>             
                  <div class="container d-flex">
                      <div class="row gx-2 gy-2">
                          <div class="col-md-5">
                              <asp:Label ID="lblAnotacoes0" runat="server" class="tituloLabel form-label col-form-label col-form-label-sm">Foto Colaborador</asp:Label>
                              <asp:Literal runat="server"><br /></asp:Literal>
                              <asp:TextBox ID="txt_Arq" runat="server" ReadOnly="True" Rows="4" class="texto form-control form-control-sm" TextMode="MultiLine" Width="350px"></asp:TextBox>
                          </div>

                          <div class="col-md-6" style="margin-left: 10px; margin-bottom: 30px;">
                              <asp:Label ID="Label6" runat="server" class="tituloLabel form-label col-form-label col-form-label-sm">Inserir / Modificar Arquivo de Foto do Colaborador</asp:Label>
                              <asp:Literal runat="server"><br /></asp:Literal>
                              <asp:FileUpload ID="File1" runat="server" onchange="callme(this)" class="texto form-control form-control-sm" Width="517px"   />
                              <asp:Literal runat="server"><br /></asp:Literal>
                              <asp:Button ID="Button1" OnClick="Button1_Click"   CssClass="btn" runat="server" Text="Confirmar Foto" />   
                          </div>

                          <div class="col-12">
                              <asp:Label ID="Label26" runat="server" CssClass="texto">Arquivo de foto do colaborador de uma empresa deve manter a mesma estrutura de nome, modificando apenas a numeração final.</asp:Label>
                              <asp:Literal runat="server"><br /></asp:Literal>
                              <asp:Label ID="Label27" runat="server" CssClass="texto">Ex.:&nbsp; GEDC_0098.jpg&nbsp;&nbsp; ( nesse caso, o padrão de nome de arquivo de foto de colaborador para essa empresa será&nbsp; GEDC_(numeração).jpg</asp:Label>
                          </div>
                      </div>
                  </div>
                                    <%--</ContentTemplate>
                                    </asp:UpdatePanel>--%>
              </eo:PageView>

        <%-- RELATÓRIOS --%>
              <eo:PageView ID="Pageview5" runat="server">

                  <div class="container d-flex">
                      <div class="row gx-2 gy-2">
                          <div class="col-md-3 mb-2 gx-2 gy-2">
                              <asp:Label ID="Label28" runat="server" Text="Data Inicial" class="tituloLabel form-label col-form-label col-form-label-sm" ></asp:Label>
                              <asp:Literal runat="server"><br /></asp:Literal>
                              <asp:TextBox ID="txt_D1" runat="server" class="texto form-control form-control-sm" MaxLength="10"></asp:TextBox>
                          </div>

                          <div class="col-md-3 mb-2 gx-2 gy-2">
                              <asp:Label ID="Label29" runat="server" class="tituloLabel form-label col-form-label col-form-label-sm" Text="Data Final"></asp:Label>
                              <asp:Literal runat="server"><br /></asp:Literal>
                              <asp:TextBox ID="txt_D2" runat="server" class="texto form-control form-control-sm" MaxLength="10"></asp:TextBox>
                          </div>

                          <div class="col-12">
                              <asp:Button ID="cmd_Relat_Abs" onclick="cmd_Relat_Abs_Click"  runat="server" class="btn" Text="Relatório de Absenteísmo" />
                          </div>
                      </div>
                  </div>

                                    

              </eo:PageView>

        <%-- BENEFICIÁRIO --%>
              <eo:PageView ID="Pageview6" runat="server">
                  <div class="container d-flex">
                            <div class="row gx-3 gy-2">
                                <fieldset>
                                    <asp:Label ID="Label21" runat="server" class="tituloLabel form-check-label"
                                        Text="Tipo de Beneficiário (Lei 8213 /91, Artigo 93)"></asp:Label>
                                    <asp:Literal runat="server"><br /></asp:Literal>
                                    <div class="form-check col-12">
                                        <asp:RadioButton ID="opt_Benef_Reabilitado" runat="server" CssClass="texto"
                                            GroupName="T1" Text="Beneficiário reabilitado" />
                                    </div>
                                    <div class="form-check col-12">
                                        <asp:RadioButton ID="opt_Benef_Deficiencia" runat="server" GroupName="T1" CssClass="texto" 
                                            Text="Portador de deficiência habilitada" />
                                    </div>
                                    <div class="form-check col-12">
                                        <asp:RadioButton ID="opt_Benef_NA" runat="server" Checked="True" CssClass="texto" 
                                            GroupName="T1" Text="Não aplicável" />
                                    </div>
                                </fieldset>
                                
                            </div>
                        </div>

              </eo:PageView>

            <%-- ENDEREÇO --%>

              <eo:PageView ID="Pageview7" runat="server">

                       <div class="container">
                           <div class="row gx-2 gy-2">
                               <div class="col-md-6 gx-2 gy-2">
                                    <asp:Label ID="Label4" runat="server" class="tituloLabel form-label col-form-label col-form-label-sm" Text="Endereço"></asp:Label>
                                    <asp:Literal runat="server"><br /></asp:Literal>
                                    <asp:TextBox ID="txtEndereco" runat="server"  class="texto form-control form-control-sm" MaxLength="250"></asp:TextBox>
                               </div>

                               <div class="col-md-2 gx-2 gy-2">
                                   <asp:Label ID="Label5" runat="server" class="tituloLabel form-label col-form-label col-form-label-sm" Text="Número" ></asp:Label>
                                   <asp:Literal runat="server"><br /></asp:Literal>
                                   <asp:TextBox ID="txtNumero" runat="server" class="texto form-control form-control-sm" MaxLength="50"></asp:TextBox>
                               </div>

                               <div class="col-md-4 gx-2 gy-2">
                                   <asp:Label ID="Label7" runat="server" class="tituloLabel form-label col-form-label col-form-label-sm" Text="Bairro"></asp:Label>
                                   <asp:Literal runat="server"><br /></asp:Literal>
                                   <asp:TextBox ID="txtBairro" runat="server" class="texto form-control form-control-sm" MaxLength="250"></asp:TextBox>
                               </div>

                               <div class="col-md-5 gx-2 gy-2">
                                   <asp:Label ID="Label3" runat="server" class="tituloLabel form-label col-form-label col-form-label-sm" Text="Complemento"></asp:Label>
                                   <asp:Literal runat="server"><br /></asp:Literal>
                                   <asp:TextBox ID="txtComplemento" runat="server" class="texto form-control form-control-sm" MaxLength="250"></asp:TextBox>
                               </div>

                               <div class="col-md-4 gx-2 gy-2">
                                   <asp:Label ID="Label8" runat="server" class="tituloLabel form-label col-form-label col-form-label-sm" Text="Munícipio" ></asp:Label>
                                   <asp:Literal runat="server"><br /></asp:Literal>
                                   <asp:TextBox ID="txtMunicipio" runat="server" class="texto form-control form-control-sm" MaxLength="50"></asp:TextBox>
                               </div>

                               <div class="col-md-1 gx-2 gy-2">
                                   <asp:Label ID="Label9" runat="server" class="tituloLabel form-label col-form-label col-form-label-sm" Text="UF"></asp:Label>
                                   <asp:Literal runat="server"><br /></asp:Literal>
                                   <asp:TextBox ID="txtUF" runat="server" class="texto form-control form-control-sm" MaxLength="2"></asp:TextBox>
                               </div>
                               <div class="col-md-2 gx-2 gy-2">
                                   <asp:Label ID="Label10" runat="server" class="tituloLabel form-label col-form-label col-form-label-sm" Text="CEP"></asp:Label>
                                   <asp:Literal runat="server"><br /></asp:Literal>
                                   <asp:TextBox ID="txtCEP" runat="server" class="texto form-control form-control-sm" MaxLength="15"></asp:TextBox>
                               </div>
                           </div>
                       </div>    

              </eo:PageView>

        <%-- ATIVIDADES ESPECIAIS --%>

              <eo:PageView ID="Pageview8" runat="server">

                  <div class="container d-flex">
                      <div class="row gx-2 gy-2">
                          <div class="col-12" style="margin-left: 30px;">
                              <asp:CheckBox ID="chk_apt_Altura" runat="server" class="texto form-check-sm-label" Text="trabalho em altura (item 35.4.1.2 e 35.4.1.2.1 da NR 35)" />
                          </div>
                          <div class="col-12" style="margin-left: 30px;">
                              <asp:CheckBox ID="chk_apt_Confinado" runat="server" class="texto form-check-sm-label" Text="trabalho para espaços confinados (item 33.3.4.1 da NR 33)" />
                          </div>
                          <div class="col-12" style="margin-left: 30px;">
                              <asp:CheckBox ID="chk_apt_Transportes" runat="server" AutoPostBack="True" class="texto form-check-sm-label" Text="operar equipamentos de transporte motorizados (item 11.1.6.1 da NR 11)" />
                          </div>
                          <div class="col-12" style="margin-left: 30px;">
                              <asp:CheckBox ID="chk_apt_Submerso" runat="server" AutoPostBack="True" class="texto form-check-sm-label" Text="atividades submersas (anexo VI da NR 15)" />
                          </div>
                          <div class="col-12" style="margin-left: 30px;">
                              <asp:CheckBox ID="chk_apt_Eletricidade" runat="server" AutoPostBack="True" class="texto form-check-sm-label" Text="serviços em eletricidade (item 10.8.7 da NR 10)" />
                          </div>
                          <div class="col-12" style="margin-left: 30px;">
                              <asp:CheckBox ID="chk_apt_Aquaviarios" runat="server" AutoPostBack="True" class="texto form-check-sm-label" Text="serviços aquaviários (item 30.5 da NR 30)" />
                          </div>
                          <div class="col-12" style="margin-left: 30px;">
                              <asp:CheckBox ID="chk_Apt_Alimento" runat="server" AutoPostBack="True" class="texto form-check-sm-label" Text="manipular alimentos (item 4.6.1 da Resolução RDC nº 216/2004)" />
                          </div>
                          <div class="col-12" style="margin-left: 30px;">
                              <asp:CheckBox ID="chk_Apt_Brigadista" runat="server" AutoPostBack="True" class="texto form-check-sm-label" Text="trabalho como Brigadista" />
                          </div>
                          <div class="col-12" style="margin-left: 30px;">
                              <asp:CheckBox ID="chk_Apt_Socorrista" runat="server" AutoPostBack="True" class="texto form-check-sm-label" Text="trabalho como Socorrista" />
                          </div>
                          <div class="col-12" style="margin-left: 30px;">
                              <asp:CheckBox ID="chk_Apt_Respirador" runat="server" AutoPostBack="True" class="texto form-check-sm-label" Text="uso de respiradores" />
                          </div>
                          <div class="col-12" style="margin-left: 30px;">
                              <asp:CheckBox ID="chk_Apt_Radiacao" runat="server" AutoPostBack="True" class="texto form-check-sm-label" Text="radiacao ionizante" />
                          </div>
                      </div>
                  </div>
                                                
                  </eo:PageView>

                <%-- ESOCIAL --%>

                  <eo:PageView ID="Pageview9" runat="server" Width="513px">
                      <div class="container d-flex">
                              <div class="row gx-2 gy-2">
                                  <div class="col-12">
                                      <div class="row headerLista">
                                          <div class="col-md-3 listaTexto">
                                              <asp:Label ID="Label30" runat="server" CssClass="tituloLabel" Text="Data Envio"></asp:Label>
                                          </div>
                                          <div class="col-md-3 listaTexto">
                                              <asp:Label ID="Label31" runat="server" CssClass="tituloLabel" Text="Evento"></asp:Label>
                                          </div>
                                          <div class="col-md-3 listaTexto">
                                              <asp:Label ID="Label32" runat="server" CssClass="tituloLabel" Text="Ambiente"></asp:Label>
                                          </div>
                                          <div class="col-md-3 listaTexto">
                                              <asp:Label ID="Label33" runat="server" CssClass="tituloLabel" Text="Status"></asp:Label>
                                          </div>
                                      </div>
                                      <div class="col-12 mb-1">
                                       <asp:ListBox ID="lst_eSocial" runat="server" Height="161px" Width="700px" CssClass="listaTexto"></asp:ListBox>
                                      </div>
                                  </div>
                              </div>
                          </div>
                  </eo:PageView>

                <%-- TRANFERÊNCIAS --%>

                  <eo:PageView ID="Pageview10" runat="server" Width="513px">
                      <div class="container d-flex">
                              <div class="row gx-2 gy-2">
                                  <div class="col-12">
                                          <div class="col-md-12 mb-4">
                                              <asp:Label ID="Label11" runat="server" class="tituloLabel form-label col-form-label col-form-label-sm" Text="Data Transferência"></asp:Label>
                                              <asp:TextBox ID="txtDataTransf" runat="server" class="texto form-control form-control-sm" Width="150px"></asp:TextBox>
                                          </div>
                                          <div class="col-md-12 my-4">
                                              <asp:Label ID="lblSetor1" runat="server" class="tituloLabel form-label col-form-label col-form-label-sm" Text="Unidade a ser Transferida"></asp:Label>                      
                                                 <asp:DropDownList ID="cmb_Transferencia" OnSelectedIndexChanged="cmb_Transferencia_SelectedIndexChanged"   runat="server" AutoPostBack="True" class="texto form-select form-select-sm" Width="206px">
                                                 </asp:DropDownList>
                                                 <asp:ListBox ID="lst_Transferencia" runat="server" Font-Size="Smaller" 
                                                    Height="16px" Visible="False"></asp:ListBox>
                                          </div>    

                                      <div class="col-md-12 my-4 ms-3">
                                            <asp:CheckBox ID="chk_Admissao_Origem" runat="server" CssClass="texto form-check-sm-label" Text="Manter Data Admissão da Origem" />
                                          </div>

                                          <div class="col-12 my-4">
                                              <asp:Button ID="cmd_Transferir" OnClick="cmd_Transferir_Click"  runat="server" CssClass="btn" Text="Realizar Transferência" />
                                          </div>
                                  </div>
                              </div>
                          </div>
                  </eo:PageView>
              

     </eo:MultiPage>
    
    <div class="row gx-2 gy-2">
        <div class="col-12 text-center" style="margin-top: 15px;">
            <asp:Button ID="cmd_Atualizar" runat="server" class="btn" Text="Salvar Dados / Classif.Funcional" onclick="cmd_Atualizar_Click"  />
        </div>

        <div class="col-12 gx-2 gy-2">
            <div class="text-start">
                <asp:Button ID="cmd_Voltar" onclick="cmd_Voltar_Click"  runat="server" CssClass="btn2" Text="Voltar" />
            </div>
        </div>
    </div>
        
</div>

</eo:CallbackPanel>

</div>


    <eo:MsgBox ID="MsgBox1" runat="server" BackColor="#47729F" ControlSkinID="None" 
        HeaderHtml="Dialog Title" Height="100px" Width="168px">
        <HeaderStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #ffbf00 3px solid; padding-left: 4px; font-weight: bold; font-size: 11px; padding-bottom: 2px; color: white; padding-top: 2px; border-bottom: #22456a 1px solid; font-family: verdana" />
        <ContentStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #7d97b6 1px solid; padding-left: 4px; border-left-width: 1px; font-size: 11px; border-left-color: #728eb8; padding-bottom: 4px; color: white; padding-top: 4px; border-bottom: #22456a 1px solid; font-family: verdana" />
        <FooterStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #7d97b6 1px solid; padding-left: 4px; border-left-width: 1px; font-size: 11px; border-left-color: #728eb8; padding-bottom: 4px; color: white; padding-top: 4px; border-bottom: #22456a 1px solid; font-family: verdana" />
        </eo:MsgBox>
</asp:Content>