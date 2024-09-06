<%@ Page Language="C#"   AutoEventWireup="True"  MasterPageFile="~/Site.Master" CodeBehind="Controle_eSocial_2210.aspx.cs"  Inherits="Ilitera.Net.e_Social.Controle_eSocial_2210" Title="Ilitera.Net" %>

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

        .eo_css_ctrl_ctl00_MainContent_grd_eSocial8{
        padding-left: 25px !important;
        }

        input[type="checkbox" i] {
        margin-left: 0.8em !important;
        }
    </style>
</asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent" >
    <div class="container-fluid d-flex ms-5 ps-4">
        <div class="row gx-3 gy-2 w-100">
    <script language="javascript">
        function Reload() {
            var f = document.getElementById('SubDados');
            //f.src = f.src;
            f.contentWindow.location.reload(true);
        }
        function OnItemCommand(grid, itemIndex, colIndex, commandName) {

            //grid.raiseItemCommandEvent(itemIndex, commandName);
            grid.raiseItemCommandEvent(itemIndex, colIndex.toString());
        }

    </script>
            
            <div class="col-11 subtituloBG" style="padding-top: 10px;">
                <asp:label id="lblExCli" runat="server" SkinID="TitleFont" CssClass="subtitulo">Evento 2210 eSocial</asp:label>
            </div>

            <div class="col-11">
                <div class="row">

                    <%-- FILTROS --%>

                    <div class="col-md-2 gx-3 gy-2">
                        <asp:Label id="Label1" runat="server" Text="Situação" CssClass="tituloLabel form-label"></asp:Label>
                        <asp:DropDownList ID="cmb_Carga" OnSelectedIndexChanged="cmb_Carga_SelectedIndexChanged"  runat="server" AutoPostBack="True" CssClass="texto form-select form-select-sm">
                            <asp:ListItem Selected="True" Value="2">Todos registros</asp:ListItem>
                            <asp:ListItem Value="1">XML Criados</asp:ListItem>
                            <asp:ListItem Value="0">Pendentes Criação XML</asp:ListItem>
                            <asp:ListItem Value="3">Pendentes Envio</asp:ListItem>
                            <asp:ListItem Value="4">Enviados</asp:ListItem>                        
                        </asp:DropDownList>
                    </div>

                    <div class="col-md-2 gx-3 gy-2">
                        <asp:Label runat="server" Text="Data Inicial" CssClass="tituloLabel form-label"></asp:Label>
                        <asp:TextBox ID="txt_Data" OnTextChanged="txt_Data_TextChanged" runat="server" MaxLength="10" AutoPostBack="True" CssClass="texto form-control form-control-sm"></asp:TextBox>
                    </div>

                    <div class="col-md-2 gx-3 gy-2">
                        <asp:Label runat="server" Text="Data Final" CssClass="tituloLabel form-label"></asp:Label>
                        <asp:TextBox ID="txt_Data2" OnTextChanged="txt_Data2_TextChanged" runat="server" MaxLength="10" AutoPostBack="True" CssClass="texto form-control form-control-sm"></asp:TextBox>
                    </div>

                    <div class="col-md-3 gx-3 gy-2">
                        <asp:Label runat="server" Text="Colaborador" CssClass="tituloLabel form-label"></asp:Label>
                        <asp:TextBox ID="txt_Nome" OnTextChanged="txt_Nome_TextChanged" runat="server" MaxLength="10" AutoPostBack="True" CssClass="texto form-control form-control-sm"></asp:TextBox>
                    </div>

                    <div class="col-md-3 gx-5 mt-4 pt-2">
                        <asp:CheckBox ID="chk_Grupo" OnCheckedChanged="chk_Grupo_CheckedChanged"  runat="server" AutoPostBack="true" Text="Todas Empresas do Grupo" CssClass="texto form-check-label" />
                    </div>

                    <asp:Button ID="cmd_Filtrar" OnClick="cmd_Filtrar_Click" runat="server" Font-Size="X-Small" Text="Filtrar" Width="57px" Visible="False" />
                    <asp:ListBox ID="lst_Arq" runat="server" Height="19px" Visible="False"></asp:ListBox>

                    <%-- FILTROS : BOTÕES --%>

                    <div class="col-12 gx-3 gy-3 mb-3">
                        <asp:Button ID="cmd_Marcar_Todos" OnClick="cmd_Marcar_Todos_Click"  runat="server" Text="Marcar todos" Width="73px" CssClass="btn" />
                        <asp:Button ID="cmd_Criar" OnClick="cmd_Criar_Click"  runat="server" Font-Size="X-Small" Text="Criar XML" Width="78px" CssClass="btn" />
                        <asp:Button ID="cmd_Enviar" OnClick="cmd_Enviar_Click"  runat="server" Font-Size="X-Small" Text="Enviar/Reenviar" Width="90px" Enabled="False" Visible="False" />
                        <asp:Button ID="cmd_Baixar" OnClick="cmd_Baixar_Click"  runat="server" Font-Size="X-Small" Text="Baixar XML" Width="75px" CssClass="btn" />
                        <asp:Button ID="cmd_Agendar_Envio" OnClick="cmd_Agendar_Envio_Click"  runat="server" Font-Size="X-Small" Text="Agendar Envio" Width="90px" CssClass="btn" Enabled="False" />
                        <asp:Button ID="cmd_CSV" OnClick="cmd_Gerar_CSV_Click"  runat="server" Font-Size="X-Small" Text="Gerar CSV" Width="100px" Visible="True" CssClass="btn" />
                    </div>

                    <%-- TABELA --%>

                    <div class="col-11" style="margin-left: 5rem; padding-left:30px;">
                        <eo:Grid ID="grd_eSocial" runat="server" ColumnHeaderAscImage="00050403" 
                            ColumnHeaderDescImage="00050404" FixedColumnCount="1" Font-Bold="False" Font-Italic="False" Font-Names="Verdana" 
                            Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" 
                            Font-Underline="False" GridLineColor="240, 240, 240" 
                            GridLines="Both" Height="485px" Width="1120px" ColumnHeaderDividerOffset="6" 
                            ColumnHeaderHeight="40" ItemHeight="40" KeyField="IdeSocial"   
                            OnItemCommand="grd_eSocial_ItemCommand"  
                            ClientSideOnItemCommand="OnItemCommand"  >
                        <ItemStyles>
                            <eo:GridItemStyleSet>
                                <ItemStyle CssText="background-color: white" />
                                <AlternatingItemStyle CssText="background-color:#eeeeee;" />
                                <SelectedStyle CssText="border-bottom-style:solid;border-left-style:solid;border-right-style:solid;border-top-style:solid;" />
                                <CellStyle CssText="font-family: 'Ubuntu'; font-size: 12px; color: #7D7B7B; text-align:left;" />
                            </eo:GridItemStyleSet>
                        </ItemStyles>
                        <ColumnHeaderTextStyle CssText="" />
                        <ColumnHeaderStyle CssText="background-color: #D9D9D9; color: #1C9489; padding: .3rem .3rem .3rem .5rem; text-align: left;" />
                        <Columns>


                             <eo:StaticColumn HeaderText="IdeSocial_Deposito" AllowSort="False" 
                                DataField="IdeSocial_Deposito" Name="IdeSocial_Deposito" ReadOnly="True"  
                                Width="0">
                               <CellStyle CssText="font-family: 'Ubuntu'; font-size: 12px; color: #7D7B7B; text-align:left;" />
                            </eo:StaticColumn>                


                            <eo:CheckBoxColumn HeaderText=""  Width="80" ></eo:CheckBoxColumn>

                            <eo:StaticColumn HeaderText="Empresa" AllowSort="False" 
                                DataField="Empresa" Name="Empresa" ReadOnly="True" 
                                SortOrder="Ascending" Text="" Width="130">
                                <CellStyle CssText="font-family: 'Ubuntu'; font-size: 12px; color: #7D7B7B; text-align:left;" />
                            </eo:StaticColumn>

                            <eo:StaticColumn HeaderText="Colaborador" AllowSort="False" 
                                DataField="Colaborador" Name="Colaborador" ReadOnly="True" 
                                SortOrder="Ascending" Text="" Width="200" Visible="true">
                               <CellStyle CssText="font-family: 'Ubuntu'; font-size: 12px; color: #7D7B7B; text-align:left;" />
                            </eo:StaticColumn>

                            <eo:CheckBoxColumn HeaderText="Reabertura" AllowSort="False" 
                                DataField="Reabertura" Name="Reabertura" ReadOnly="True" Width="60">
                               <CellStyle CssText="font-family: 'Ubuntu'; font-size: 12px; color: #7D7B7B; text-align:left;" />
                            </eo:CheckBoxColumn>

                            <eo:StaticColumn HeaderText="IdAcidente" AllowSort="False" 
                                DataField="IdAcidente" Name="IdAcidente" ReadOnly="True" Visible="false" 
                                Width="80">
                               <CellStyle CssText="font-family: 'Ubuntu'; font-size: 12px; color: #7D7B7B; text-align:left;" />
                            </eo:StaticColumn>

                            <eo:StaticColumn HeaderText="Data Acidente" AllowSort="False" 
                                DataField="DtAcidente" Name="DtAcidente" ReadOnly="True"  
                                Width="90">
                                <CellStyle CssText="font-family: 'Ubuntu'; font-size: 12px; color: #7D7B7B; text-align:left;" />
                            </eo:StaticColumn>

                            <eo:StaticColumn HeaderText="Data Acidente" AllowSort="False" Visible="false"
                                DataField="DtAcidente" Name="DtAcidente" ReadOnly="True"  
                                Width="75">
                                <CellStyle CssText="font-family: 'Ubuntu'; font-size: 12px; color: #7D7B7B; text-align:left;" />
                            </eo:StaticColumn>


                            <eo:StaticColumn HeaderText="Criador" AllowSort="False" 
                                DataField="Criado_Por" Name="Criado_Por" ReadOnly="True"  
                                Width="60">
                                <CellStyle CssText="font-family: 'Ubuntu'; font-size: 12px; color: #7D7B7B; text-align:left;" />
                            </eo:StaticColumn>


                            <eo:StaticColumn HeaderText="XML Criado" AllowSort="False" 
                                DataField="Criado_Em" Name="Criado_Em" ReadOnly="True"
                                Width="70">
                                <CellStyle CssText="font-family: 'Ubuntu'; font-size: 12px; color: #7D7B7B; text-align:left;" />
                            </eo:StaticColumn>

                            <eo:StaticColumn HeaderText="Nome_Arquivo" AllowSort="False" 
                                DataField="NomeArquivo" Name="NomeArquivo" ReadOnly="True"
                                Width="0">
                                <CellStyle CssText="font-family: 'Ubuntu'; font-size: 12px; color: #7D7B7B; text-align:left;" />
                            </eo:StaticColumn>


                            <eo:StaticColumn HeaderText="IdPessoa" AllowSort="False" 
                                DataField="IdPessoa" Name="IdPessoa" ReadOnly="True"  
                                Width="0">
                                <CellStyle CssText="font-family: 'Ubuntu'; font-size: 12px; color: #7D7B7B; text-align:left;" />
                            </eo:StaticColumn>

                            <eo:StaticColumn HeaderText="IdeSocial" AllowSort="False" 
                                DataField="IdeSocial" Name="IdeSocial" ReadOnly="True"  
                                Width="0">
                                <CellStyle CssText="font-family: 'Ubuntu'; font-size: 12px; color: #7D7B7B; text-align:left;" />
                            </eo:StaticColumn>                

                            <eo:StaticColumn HeaderText="IdEmpregado" AllowSort="False" 
                                DataField="IdEmpregado" Name="IdEmpregado" ReadOnly="True"  
                                Width="0">
                               <CellStyle CssText="font-family: 'Ubuntu'; font-size: 12px; color: #7D7B7B; text-align:left;" />
                            </eo:StaticColumn>                

                
                            <eo:StaticColumn HeaderText="Status do Envio" AllowSort="False" 
                                DataField="Enviado_Em" Name="Enviado_Em" ReadOnly="True"
                                Width="202">
                                <CellStyle CssText="font-family: 'Ubuntu'; font-size: 12px; color: #7D7B7B; text-align:left;" />
                            </eo:StaticColumn>

                            <eo:StaticColumn HeaderText="Agendado" AllowSort="False" 
                                DataField="Agendado" Name="Agendado" ReadOnly="True"
                                Width="62">
                                <CellStyle CssText="font-family: 'Ubuntu'; font-size: 12px; color: #7D7B7B; text-align:left;" />
                            </eo:StaticColumn>


                             <eo:ButtonColumn ButtonText="Detalhes" 
                                Name="Detalhes">
                               <CellStyle CssText="font-family: 'Ubuntu'; font-size: 12px; color: #7D7B7B; text-align:left;" />
                            </eo:ButtonColumn>

                              <eo:ButtonColumn ButtonText="Visualizar XML" 
                                Name="Visualizar XML" >
                               <CellStyle CssText="font-family: 'Ubuntu'; font-size: 12px; color: #7D7B7B; text-align:left;" />
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
                </div>
            </div>

        <div class="col-11 ms-5 ps-3 mb-2">
            <div class="row">
                <div class="col-1 gx-3 gy-2">
                    <asp:ListBox ID="lst_Detalhes" runat="server" OnSelectedIndexChanged="lst_Detalhes_SelectedIndexChanged"  Height="105px" Visible="False" Width="664px" Font-Size="Small" AutoPostBack="True" BackColor="#FFFFCC" Font-Names="Courier New"></asp:ListBox>
                    <asp:ListBox ID="lst_Id_Detalhes" runat="server" Visible="False"></asp:ListBox>
                    <TABLE class="defaultFont" id="Table28" cellSpacing="0" cellPadding="0" align="right" border="0">
								<TR>
									<TD align="right">
                                        <asp:Label ID="lbl_IdeSocial_Deposito" runat="server" Text="0" Visible="False"></asp:Label>
                                        <asp:Label ID="lbl_Qtde" runat="server" Text="0" Visible="False"></asp:Label>
                                        <asp:Label ID="lbl_IdEPIClienteCA" runat="server" Text="0" Visible="False"></asp:Label>
                                        <asp:Label ID="lbl_IdCA" runat="server" Text="0" Visible="False"></asp:Label>
                                        <asp:label id="lblTotRegistros" visible="false" runat="server"></asp:label></TD>
								</TR>
					</TABLE>
                </div>
            </div>
        </div>
                <div class="col-5 gx-3 gy-2 text-start mb-3 ms-5 ps-4">
                    <asp:Button ID="cmd_Fechar_Det" runat="server" CssClass="btn" Text="Fechar Detalhes" Visible="False" />
                    <asp:Button ID="cmd_Proc_Lote" runat="server" CssClass="btn" Text="XML Processamento Lote" OnClick="cmd_Proc_Lote_Click"  Enabled="False" Visible="False" />
                    <asp:Button ID="cmd_Proc_Evento" runat="server" CssClass="btn" Text="XML Processamento Evento" OnClick="cmd_Proc_Evento_Click" Enabled="False" Visible="False" />
                    <asp:Button ID="cmd_Proc_Atualizar" runat="server" CssClass="btn" Text="Atualizar Status Processamento" Enabled="False" Visible="False" />
                </div>
             <div class="col-11 ms-5 ps-4">
                  <asp:Button ID="cmd_Voltar" onclick="cmd_Voltar_Click"  runat="server" CssClass="btn" Text="Voltar" Width="132px" />
            </div>
         
			<%--<iframe id="SubDados" name="SubDados" align="middle" marginWidth="0" marginHeight="0" frameBorder="0"
				width="600" scrolling="no" src="../DadosEmpresa/SubDadosNull.aspx" >
			</iframe>--%>
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
         <eo:MsgBox ID="MsgBox2" runat="server" BackColor="#47729F" ControlSkinID="None" 
        HeaderHtml="Dialog Title" Height="100px" Width="550px" >
        <HeaderStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #ffbf00 3px solid; padding-left: 4px; font-weight: bold; font-size: 11px; padding-bottom: 2px; color: white; padding-top: 2px; border-bottom: #22456a 1px solid; font-family: verdana" />
        <ContentStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #7d97b6 1px solid; padding-left: 4px; border-left-width: 1px; font-size: 11px; border-left-color: #728eb8; padding-bottom: 4px; color: white; padding-top: 4px; border-bottom: #22456a 1px solid; font-family: verdana" />
        <FooterStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #7d97b6 1px solid; padding-left: 4px; border-left-width: 1px; font-size: 11px; border-left-color: #728eb8; padding-bottom: 4px; color: white; padding-top: 4px; border-bottom: #22456a 1px solid; font-family: verdana" />
        </eo:MsgBox>

        </div>
    </div>
</asp:Content>