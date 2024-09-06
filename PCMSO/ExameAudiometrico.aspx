<%@ Reference Control="~/pcmso/wbusrcntrlaudiograma.ascx" %>

<%@ Page Title="Ilitera.Net" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="True"
    CodeBehind="ExameAudiometrico.aspx.cs" Inherits="Ilitera.Net.ExameAudiometrico" %>

<%@ Register TagPrefix="uc1" TagName="WbUsrCntrlAudiograma" Src="WbUsrCntrlAudiograma.ascx"%>

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
        .inputBox
        {}
        .largeboldfont
        {}
        #Table1
        {
            height: 338px;
        }
    </style>
</asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div class="container-fluid d-flex ms-5 ps-4">
        <div class="row gx-3 gy-2 w-100">

        <LINK href="scripts/style.css" type="text/css" rel="stylesheet">
		
		<script language="javascript" src="scripts/validador.js"></script>
		<script language="javascript">
            function AbreClinicas(IdCliente, IdUsuario) {
                addItemPop(centerWin('../CommonPages/CadClinica.aspx?IdEmpresa=' + IdCliente + '&IdUsuario=' + IdUsuario + '', 360, 270, 'CadatroClinica'));
            }

            function AbreAudiometro(IdCliente, IdUsuario) {
                var ddlClinicas = document.getElementById("UltraWebTabAudiometria__ctl0_ddlClinica");

                if (ddlClinicas.options[ddlClinicas.selectedIndex].value == "0")
                    window.alert("É necessário selecionar uma Clínica para continuar!");
                else
                    addItemPop(centerWin('CadAudiometro.aspx?IdEmpresa=' + IdCliente + '&IdUsuario=' + IdUsuario + '&IdClinica=' + ddlClinicas.options[ddlClinicas.selectedIndex].value + '', 300, 280, 'CadastroAudiometro'));
            }

            function AbreMedicos(IdUsuario) {
                var ddlClinicas = document.getElementById("UltraWebTabAudiometria__ctl0_ddlClinica");

                if (ddlClinicas.options[ddlClinicas.selectedIndex].value == "0")
                    window.alert("É necessário selecionar uma Clínica para continuar!");
                else
                    addItemPop(centerWin('../CommonPages/CadMedico.aspx?IdClinica=' + ddlClinicas.options[ddlClinicas.selectedIndex].value + '&IdUsuario=' + IdUsuario + '', 350, 310, 'CadastroMedico'));
            }

            function ConsisteCkeckBoxDeAlteracao(checkBox) {
                tipo = checkBox.id.substring(checkBox.id.length - 1, checkBox.id.length);

                if (tipo == 'S')
                    checkBoxAux = eval('document.frmExameAudiometrico.' + checkBox.id.substring(0, checkBox.id.length - 1) + 'N');
                if (tipo == 'N')
                    checkBoxAux = eval('document.frmExameAudiometrico.' + checkBox.id.substring(0, checkBox.id.length - 1) + 'S');

                if (checkBoxAux.checked)
                    checkBoxAux.checked = !checkBox.checked
            }
        </script>


    <eo:CallbackPanel runat="server" id="CallbackPanel1" Triggers="">

        <div class="col-11 subtituloBG mb-3" style="padding-top: 10px;">
            <asp:label id="Label94" runat="server" CssClass="subtitulo">Exame Audiométrico</asp:label>
        </div>

        <div class="col-11">
            <eo:TabStrip ID="TabStrip1" runat="server" ControlSkinID="None" 
                            MultiPageID="MultiPage1">
                            <topgroup>
                                <Items>
                                    <eo:TabItem Text-Html="Exame">
                                    </eo:TabItem>
                                    <eo:TabItem Text-Html="Orelha Direita">
                                    </eo:TabItem>
                                    <eo:TabItem Text-Html="Orelha Esquerda">
                                    </eo:TabItem>
                                    <eo:TabItem Text-Html="Observações">
                                    </eo:TabItem>
                                    <eo:TabItem Text-Html="Digitalização">
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



                            <eo:MultiPage ID="MultiPage1" runat="server" Height="400" Width="1050">
                                <eo:PageView ID="Pageview1" runat="server" Width="1050">

                                    <div class="col-12 mb-3">
                                        <div class="row">
                                            <div class="col-md-2 gx-3 gy-2">
                                                <asp:label id="Label66" runat="server" CssClass="tituloLabel">Data Exame</asp:label>
                                                <asp:textbox id="wdeDataExame" runat="server" CssClass="texto form-control" ImageDirectory=" " Nullable="False" MinValue="1800-01-01" 
                                                    DisplayModeFormat="dd/MM/yyyy" EditModeFormat="dd/MM/yyyy" HorizontalAlign="Center" MaxLength="10"></asp:textbox>
                                            </div>

                                            <div class="col-md-4 gx-3 gy-2">
                                                <asp:label id="Label65" runat="server" CssClass="tituloLabel">Tipo do Exame</asp:label>
                                                <asp:dropdownlist id="ddlTipoExame" runat="server" CssClass="texto form-select">
                                                    <asp:ListItem Value="-">Selecione...</asp:ListItem>
                                                    <asp:ListItem Value="0">Admissional</asp:ListItem>
                                                    <asp:ListItem Value="5">Demissional</asp:ListItem>
                                                    <asp:ListItem Value="3">Mudan&#231;a de Fun&#231;&#227;o</asp:ListItem>
                                                    <asp:ListItem Value="2">Peri&#243;dico</asp:ListItem>
                                                    <asp:ListItem Value="4">Retorno ao Trabalho</asp:ListItem>
                                                    <asp:ListItem Value="1">Semestral</asp:ListItem>
                                                </asp:dropdownlist>
                                            </div>

                                            <div class="col-md-4 gx-3 gy-2">
                                                <asp:label id="Label64" runat="server" CssClass="tituloLabel">Audiômetro</asp:label>
                                                <asp:dropdownlist id="ddlAudiometro" runat="server" CssClass="texto form-select"></asp:dropdownlist>
                                            </div>

                                            <div class="col-md-2 gx-3 gy-2">
                                                <asp:label id="Label67" runat="server" CssClass="tituloLabel" ToolTip="Tempo de Repouso Auditivo (horas)">Repouso Auditivo(horas)</asp:label>
                                                <asp:textbox id="wneRepouso" runat="server" CssClass="texto form-control" ImageDirectory=" " Nullable="False" HorizontalAlign="Center"></asp:textbox>
                                            </div>

                                            <div class="col-md-4 gx-3 gy-2">
                                                <asp:label id="lblClinica" runat="server" CssClass="tituloLabel">Clínica</asp:label>
                                                <asp:dropdownlist id="ddlClinica" OnSelectedIndexChanged="ddlClinica_SelectedIndexChanged" runat="server" CssClass="texto form-select" AutoPostBack="True"></asp:dropdownlist>
                                            </div>

                                            <div class="col-md-4 gx-3 gy-2">
                                                <asp:label id="lblMedico" runat="server" CssClass="tituloLabel">Médico</asp:label>
                                                <asp:dropdownlist id="ddlMedico" runat="server" CssClass="texto form-select"></asp:dropdownlist>
                                            </div>

                                            <div class="col-md-3 gx-3 gy-2">
                                                <asp:Label ID="Label27" runat="server" SkinID="BoldFont" CssClass="tituloLabel">Resultado do Exame</asp:Label>
                                                <asp:RadioButtonList ID="rblResultado" runat="server" RepeatColumns="3" tabIndex="1" CssClass="texto form-control-sm ms-3" Width="250">
                                                    <asp:ListItem Value="1">Normal</asp:ListItem>
                                                    <asp:ListItem Value="2">Alterado</asp:ListItem>
                                                    <asp:ListItem Value="3">Em Espera</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-12">
                                        <div class="row">
                                            <div class="col-md-3 gx-3 gy-2">
                                                <asp:Label ID="Label2" runat="server" CssClass="tituloLabel">Sente dificuldade auditiva?</asp:Label>
                                                <div class="row ms-3">
                                                    <div class="col-3">
                                                        <asp:CheckBox ID="ckbDificuldadeS" runat="server" CssClass="texto form-check-input bg-transparent border-0" Text="Sim" />
                                                    </div>

                                                    <div class="col-3">
                                                        <asp:CheckBox ID="ckbDificuldadeN" runat="server" CssClass="texto form-check-input bg-transparent border-0" Text="Não" />
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-3 gx-3 gy-2">
                                                <asp:Label ID="Label51" runat="server" CssClass="tituloLabel">Tem presença de vertigens?</asp:Label>
                                                <div class="row ms-3">
                                                    <div class="col-3"> 
                                                        <asp:CheckBox ID="ckbVertigensS" runat="server" CssClass="texto form-check-input bg-transparent border-0" Text="Sim" />
                                                    </div>

                                                    <div class="col-3">
                                                        <asp:CheckBox ID="ckbVertigensN" runat="server" CssClass="texto form-check-input bg-transparent border-0" Text="Não" />
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-3 gx-3 gy-2">
                                                <asp:Label ID="Label41" runat="server" CssClass="tituloLabel">Tem presença de acúfenos?</asp:Label>
                                                <div class="row ms-3">
                                                    <div class="col-3">
                                                        <asp:CheckBox ID="ckbAcufenosS" runat="server" CssClass="texto form-check-input bg-transparent border-0" Text="Sim" />
                                                    </div>

                                                    <div class="col-3">
                                                        <asp:CheckBox ID="ckbAcufenosN" runat="server" CssClass="texto form-check-input bg-transparent border-0" Text="Não" />
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-3 gx-3 gy-2">
                                                <asp:Label ID="Label8" runat="server" CssClass="tituloLabel">Antecedentes</asp:Label>
                                                <div class="row ms-3">
                                                    <div class="col-4">
                                                        <asp:CheckBox ID="ckbIsCaxumba" runat="server" CssClass="texto form-check-input bg-transparent border-0" Text="Caxumba" />
                                                    </div>

                                                    <div class="col-4">
                                                        <asp:CheckBox ID="ckbIsSarampo" runat="server" CssClass="texto form-check-input bg-transparent border-0"  Text="Sarampo" />
                                                    </div>

                                                    <div class="col-4">
                                                        <asp:CheckBox ID="ckbIsMeningite" runat="server" CssClass="texto form-check-input bg-transparent border-0" Text="Meningite" />
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-5 gx-3 gy-3 mb-3">
                                                <asp:Label ID="Label7" runat="server" CssClass="tituloLabel">Há familiares consangüíneos com problemas auditivos?</asp:Label>
                                                <div class="row ms-3">
                                                    <div class="col-2">
                                                        <asp:CheckBox ID="ckbFamiliaresS" runat="server" CssClass="texto form-check-input bg-transparent border-0" Text="Sim" />
                                                    </div>

                                                    <div class="col-3">
                                                        <asp:CheckBox ID="ckbFamiliaresN" runat="server" CssClass="texto form-check-input bg-transparent border-0" Text="Não" />
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-3 gx-3 gy-3 mb-3">
                                                <asp:Label ID="Label61" runat="server" CssClass="tituloLabel">Faz uso de alguma medicação?</asp:Label>
                                                <div class="row ms-3">
                                                    <div class="col-3">
                                                        <asp:CheckBox ID="ckbMedicacaoS" runat="server" CssClass="texto form-check-input bg-transparent border-0" Text="Sim" />
                                                    </div>

                                                    <div class="col-3">
                                                        <asp:CheckBox ID="ckbMedicacaoN" runat="server" CssClass="texto form-check-input bg-transparent border-0" Text="Não" />
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-4 gy-3 gy-3 mb-3">
                                                <asp:Label ID="Label91" runat="server" CssClass="tituloLabel">Qual?</asp:Label>
                                                <asp:TextBox ID="txtMedicacao" runat="server" CssClass="texto form-control"></asp:TextBox>
                                            </div>

                                            <div class="col-md-4 gx-3 gy-3">
                                                <asp:Label ID="Label21" runat="server" CssClass="tituloLabel">Tempo de exposição ao ruído ocupacional</asp:Label>
                                                <asp:TextBox ID="txtTempoExposicao" runat="server" CssClass="texto form-control"></asp:TextBox>
                                            </div>

                                            <div class="col-md-3 gx-3 gy-3">
                                                <asp:Label ID="Label5" runat="server" CssClass="tituloLabel">Tempo de uso do protetor auditivo</asp:Label>
                                                <asp:TextBox ID="txtTempoUso" runat="server" CssClass="texto form-control"></asp:TextBox>
                                            </div>

                                            <div class="col-md-3 gx-3 gy-3">
                                                <asp:Label ID="Label4" runat="server" CssClass="tituloLabel">Exposição a ruído extra laboral?</asp:Label>
                                                <div class="row ms-3">
                                                    <div class="col-3">
                                                        <asp:CheckBox ID="ckbRuidoExtraS" runat="server" CssClass="texto form-check-input bg-transparent border-0" Text="Sim" />
                                                    </div>

                                                    <div class="col-3">
                                                        <asp:CheckBox ID="ckbRuidoExtraN" runat="server" CssClass="texto form-check-input bg-transparent border-0" Text="Não" />
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-3 gx-3 gy-3">
                                                <asp:Label ID="Label6" runat="server" CssClass="tituloLabel">Exposição a produtos ototóxicos?</asp:Label>
                                                <div class="row ms-3">
                                                    <div class="col-3">
                                                        <asp:CheckBox ID="ckbototoxicosS" runat="server" CssClass="texto form-check-input bg-transparent border-0" Text="Sim" />
                                                    </div>

                                                    <div class="col-3">
                                                        <asp:CheckBox ID="ckbototoxicosN" runat="server" CssClass="texto form-check-input bg-transparent border-0" Text="Não" />
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-3 gx-3 gy-3">
                                                <asp:Label ID="Label9" runat="server" CssClass="tituloLabel">Qual?</asp:Label>
                                                <asp:TextBox ID="txtototoxicos" runat="server" CssClass="texto form-control"></asp:TextBox>
                                            </div>

                                            <div class="col-md-3 gx-3 gy-3">
                                                <asp:Label ID="Label11" runat="server" CssClass="tituloLabel">Alteração nos meatos acústicos?</asp:Label>
                                                <div class="row ms-3">
                                                    <div class="col-3">
                                                        <asp:CheckBox ID="ckbMeatosS" runat="server" CssClass="texto form-check-input bg-transparent border-0" Text="Sim" />
                                                    </div>

                                                    <div class="col-3">
                                                        <asp:CheckBox ID="ckbMeatosN" runat="server" CssClass="texto form-check-input bg-transparent border-0" Text="Não" />
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-3 gx-3 gy-3">
                                                <asp:Label ID="Label10" runat="server" CssClass="tituloLabel">Qual?</asp:Label>
                                                <asp:TextBox ID="txtMeatos" runat="server" CssClass="texto form-control"></asp:TextBox>
                                            </div>

                                            <div class="col-md-6 gx-3 gy-3">
                                                <asp:Label ID="Label93" runat="server" CssClass="tituloLabel" Visible="true">Observações</asp:Label>
                                                <asp:TextBox ID="txtObsAnamnese" runat="server" CssClass="texto form-control" Rows="4" TextMode="MultiLine" Visible="true"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                    <INPUT class="buttonBox" id="btnAddClinica" type="button" value="..." name="btnAddClinica" runat="server" visible="False">
                                    <INPUT class="buttonBox" id="btnAddAudiometro" type="button" value="..." name="btnAddMedico" runat="server" visible="False">
                                    <INPUT class="buttonBox" id="btnAddMedico" type="button" value="..." name="btnAddMedico" runat="server" visible="False">
                                

                                                       <%--     <defaulttabstyle backcolor="#FEFCFD" font-names="Microsoft Sans Serif" font-size="8pt" forecolor="Black" height="22px">
                                                                <padding top="2px">
                                                                </padding>
                                                            </defaulttabstyle>
                                                            <roundedimage fillstyle="LeftMergedWithCenter" hoverimage="ig_tab_winXP2.gif" leftsidewidth="7" normalimage="ig_tab_winXP3.gif" rightsidewidth="6" selectedimage="ig_tab_winXP1.gif" shiftofimages="2">
                                                            </roundedimage>
                                                            <selectedtabstyle>
                                                                <padding bottom="2px">
                                                                </padding>
                                                            </selectedtabstyle> --%>
                                                            
                                </eo:PageView>

                                <eo:PageView ID="Pageview2" runat="server" Width="457px">
											<TABLE class="normalFont" id="Table19" cellSpacing="0" cellPadding="0" width="450" align="center"
												border="0">
												<TR>
													<TD align="center">
                                                        <uc1:WbUsrCntrlAudiograma id="WbUsrCntrlAudiogramaDireito" runat="server"></uc1:WbUsrCntrlAudiograma>
														</TD>
												</TR>
											</TABLE>
                                </eo:PageView>

                                <eo:PageView ID="Pageview3" runat="server" Width="457px" Height="404px">
											<TABLE class="normalfont" id="Table1" cellSpacing="0" cellPadding="0" width="450" align="center"
												border="0">
												<TR>
													<TD align="center">
                                                        <uc1:WbUsrCntrlAudiograma id="WbUsrCntrlAudiogramaEsquerda" runat="server"></uc1:WbUsrCntrlAudiograma>
														</TD>
												</TR>
											</TABLE>
                               </eo:PageView>

                                <eo:PageView ID="Pageview4" runat="server" Width="1050px">

								    <div class="col-11 mb-3">
                                     <div class="row">
                                        <div class="col-md-6 gx-3 gy-2">
                                            <asp:TextBox id="txtObsExame" runat="server" CssClass="texto form-control form-control-sm" TextMode="MultiLine" Rows="20"></asp:TextBox>
                                        </div>
                                     </div>
                                    </div>
												
                                </eo:PageView>

                                <eo:PageView ID="Pageview5" runat="server" Width="1050px">
                                    
                                    <div class="col-12">
                                        <div class="row">
                                            <div class="row">
                                                <div class="col-md-6 gx-3 gy-2">
                                                    <asp:label id="lblAnotacoes0" runat="server" CssClass="tituloLabel form-label">Arquivo do Prontuário :</asp:label>
                                                    <asp:textbox id="txt_Arq" runat="server" CssClass="texto form-control form-control-sm" Rows="4" TextMode="MultiLine" Height="27px" ReadOnly="True"></asp:textbox>
                                                </div>
                                            </div>
                                            
                                            <div class="row">
                                                <div class="col-md-6 gx-3 gy-2">
                                                    <asp:label id="Label92" runat="server" CssClass="tituloLabel">Inserir / Modificar Arquivo de Prontuário : </asp:label>
                                                    <asp:FileUpload ID="File1" runat="server" lientIDMode="Static" CssClass="texto form-control"/>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                  </eo:PageView>

                                 <eo:PageView ID="Pageview6" runat="server">
                                    <div class="col-12 mt-3 mb-3">
                                    <eo:Grid ID="gridAnamnese" runat="server" ColumnHeaderAscImage="00050403" 
                                            ColumnHeaderDescImage="00050404" FixedColumnCount="1" 
                                            GridLineColor="240, 240, 240" 
                                            GridLines="Both" Height="366px" Width="800px" ColumnHeaderDividerOffset="6" 
                                            ColumnHeaderHeight="30" ItemHeight="18" KeyField="IdAnamneseExame"  
                                            FullRowMode="False" CssClass="grid" OnItemCommand="gridAnamnese_ItemCommand" ClientSideOnItemCommand="OnItemCommand"    >
                                        <ItemStyles>
                                            <eo:GridItemStyleSet>
                                                <ItemStyle CssText="background-color: #FAFAFA" />
                                                <AlternatingItemStyle CssText="background-color:#ffffcc;" />
                                                <SelectedStyle CssText="border-bottom-style:solid;border-left-style:solid;border-right-style:solid;border-top-style:solid;" />
                                                <CellStyle CssText="padding-left:8px;padding-top:2px; color:#black;white-space:nowrap;" />
                                            </eo:GridItemStyleSet>
                                        </ItemStyles>
                                        <ColumnHeaderTextStyle CssText="" />
                                        <ColumnHeaderStyle CssClass="tabelaC" />
                                        <Columns>

                                            <eo:StaticColumn HeaderText="" AllowSort="True" 
                                                DataField="IdAnamneseExame" Name="IdAnamneseExame" ReadOnly="True" 
                                                SortOrder="Ascending" Text="" Width="0">
                                                <CellStyle CssText="background-color:#ffffcc;font-family:Tahoma;font-weight:bold;text-align:center" />
                                            </eo:StaticColumn>

                                            <eo:StaticColumn HeaderText="" AllowSort="True" 
                                                DataField="IdExameBase" Name="IdExameBase" ReadOnly="True" 
                                                SortOrder="Ascending" Text="" Width="0">
                                                <CellStyle CssText="background-color:#ffffcc;font-family:Tahoma;font-weight:bold;text-align:center" />
                                            </eo:StaticColumn>

                                            <eo:StaticColumn HeaderText="" AllowSort="True" 
                                                DataField="IdAnamneseDinamica" Name="IdAnamneseDinamica" ReadOnly="True" 
                                                SortOrder="Ascending" Text="" Width="0">
                                                <CellStyle CssText="background-color:#ffffcc;font-family:Tahoma;font-weight:bold;text-align:center" />
                                            </eo:StaticColumn>

                                            <eo:StaticColumn HeaderText="" AllowSort="True" 
                                                DataField="Peso" Name="Peso" ReadOnly="True" 
                                                SortOrder="Ascending" Text="" Width="0">
                                                <CellStyle CssText="background-color:#ffffcc;font-family:Tahoma;font-weight:bold;text-align:center" />
                                            </eo:StaticColumn>

                                            <eo:StaticColumn HeaderText="Sistema" AllowSort="True" 
                                                DataField="Sistema" Name="Sistema" ReadOnly="True" 
                                                SortOrder="Ascending" Text="" Width="155">
                                                <CellStyle CssText="font-family:Tahoma;font-size:7pt;font-weight:bold;text-align:left;" />
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
				                                                </select></EditorTemplate>
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
                                        <asp:Button ID="cmd_Anamnese" onclick="cmd_Anamnese_Click"  runat="server" tabIndex="1" Text="Salvar Alterações na Anamnese" CssClass="btn" />
                                        <asp:LinkButton ID="lkbAnamnese2" runat="server" SkinID="BoldLinkButton" CssClass="btn"><img border="0" src="Images/printer.svg" style="padding-right: .3rem;"> Anamnese</img></asp:LinkButton>
                                    </div>
                                </div>

                                        

       </eo:PageView>


                            </eo:MultiPage>
        </div>

            <div class="text-center">
                <asp:linkbutton id="lkbAnamnese" runat="server" CssClass="btn" Visible="False" ><img src="Images/printer.svg" style="padding: .3rem;" border="0" alt="Visualizar Exame Digitalizado">Visualizar Anamnese</asp:linkbutton>
                <asp:LinkButton ID="lkbAudiograma" runat="server" SkinID="BoldLinkButton" CssClass="btn"><img border="0" src="Images/printer.svg" style="padding: .3rem;">Visualizar Audiograma</img></asp:LinkButton>
                <asp:button id="btbOk" onclick="btbOk_Click" runat="server" CssClass="btn" Text="Gravar"></asp:button>
                <asp:button id="btnExcluir" onclick="btnExcluir_Click" runat="server" CssClass="btn" Text="Excluir"></asp:button>
            </div>

            <div class="text-start">
                <asp:Button ID="cmd_Voltar" onclick="cmd_Voltar_Click"  runat="server" CssClass="btn2" Text="Voltar" />
            </div>



        <INPUT id="txtAuxiliar" type="hidden" name="txtAuxiliar" runat="server">
                             
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