<%@ Page Language="C#"   AutoEventWireup="True"  MasterPageFile="~/Site.Master" CodeBehind="Controle_eSocial_1060.aspx.cs"  Inherits="Ilitera.Net.e_Social.Controle_eSocial_1060" Title="Ilitera.Net" %>

<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>


<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
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
    </style>
</asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent" >
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

  

<%--		</form>
	</body>
</HTML>
--%>		
	

<TABLE class="defaultFont" cellSpacing="0" cellPadding="0" width="900" align="center" border="0" bgcolor="white">
				<TR>
					<TD class="defaultFont" align="center" colSpan="1"><asp:label id="lblExCli" 
                            runat="server" SkinID="TitleFont" style="font-weight: 700">Evento 1060 eSocial</asp:label><BR>
						<BR>
					</TD>
                    </TR>
    <TR>
					<TD>
                        <asp:label id="Label1" 
                            runat="server" style="font-weight: 700" Font-Bold="False" Font-Size="X-Small">Situação</asp:label>&nbsp;<asp:DropDownList ID="cmb_Carga" runat="server" Height="20px" Width="196px" Font-Size="XX-Small" AutoPostBack="True" OnSelectedIndexChanged="cmb_Carga_SelectedIndexChanged" >
                            <asp:ListItem Selected="True" Value="2">Todos registro</asp:ListItem>
                            <asp:ListItem Value="1">XML Criados</asp:ListItem>
                            <asp:ListItem Value="0">Pendentes Criação XML</asp:ListItem>
                            <asp:ListItem Value="3">Pendentes Envio</asp:ListItem>
                            <asp:ListItem Value="4">Enviados</asp:ListItem>
                        </asp:DropDownList>
                         &nbsp;&nbsp;&nbsp;&nbsp; Filtrar:&nbsp;&nbsp; Data Inicial
                         <asp:TextBox ID="txt_Data" runat="server" Font-Size="X-Small" MaxLength="10" Width="62px" AutoPostBack="True" OnTextChanged="txt_Data_TextChanged"></asp:TextBox>
                        
                        &nbsp;&nbsp; Data Final
                         <asp:TextBox ID="txt_Data2" runat="server" Font-Size="X-Small" MaxLength="10" Width="68px" AutoPostBack="True" OnTextChanged="txt_Data2_TextChanged"></asp:TextBox>
                        
					    &nbsp;<asp:Button ID="cmd_Filtrar" runat="server" Font-Size="X-Small" Text="Filtrar" Width="57px" OnClick="cmd_Filtrar_Click" Visible="False" />
                        
					    <asp:ListBox ID="lst_Arq" runat="server" Height="20px" Visible="False"></asp:ListBox>
                        
					&nbsp;&nbsp; Nome GHE
                         <asp:TextBox ID="txt_Nome" runat="server" Font-Size="X-Small" MaxLength="10" Width="68px" AutoPostBack="True" OnTextChanged="txt_Nome_TextChanged"></asp:TextBox>
                        
					    <asp:CheckBox ID="chk_Grupo" runat="server" AutoPostBack="true" OnCheckedChanged="chk_Grupo_CheckedChanged" Text="Todas Empresas do Grupo" />
                        
					</TD>
                    </TR>

    <tr>
        <td>

            <asp:Button ID="cmd_Marcar_Todos" runat="server" Font-Size="X-Small" Text="Marcar todos" Width="73px" OnClick="cmd_Marcar_Todos_Click" />
            &nbsp;&nbsp;&nbsp;

            <asp:Button ID="cmd_Criar" runat="server" Font-Size="X-Small" Text="Criar XML" Width="78px" OnClick="cmd_Criar_Click" />
            &nbsp;
            <asp:Button ID="cmd_Enviar" runat="server" Font-Size="X-Small" Text="Enviar/Reenviar" Width="90px" OnClick="cmd_Enviar_Click" Enabled="False" />
            &nbsp;
            <asp:Button ID="cmd_Baixar" runat="server" Font-Size="X-Small" Text="Baixar XML" Width="75px" OnClick="cmd_Baixar_Click" />
        </td>


    </tr>

				<TR>
					<TD vAlign="top" align="right">
                    
                         
                            
                 <eo:Grid ID="grd_eSocial" runat="server" BorderColor="Black" 
                BorderWidth="1px" ColumnHeaderAscImage="00050403" 
                ColumnHeaderDescImage="00050404" ColumnHeaderDividerImage="00050402" 
                FixedColumnCount="1" Font-Bold="False" Font-Italic="False" Font-Names="Verdana" 
                Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" 
                Font-Underline="False" GridLineColor="240, 240, 240" 
                GridLines="Both" Height="485px" Width="1068px" ColumnHeaderDividerOffset="6" 
                ColumnHeaderHeight="18" ItemHeight="22" KeyField="IdeSocial" 
        OnItemCommand="grd_eSocial_ItemCommand"  
        ClientSideOnItemCommand="OnItemCommand"  >
            <ItemStyles>
                <eo:GridItemStyleSet>
                    <ItemStyle CssText="background-color: white" />
                    <AlternatingItemStyle CssText="background-color:#eeeeee;" />
                    <SelectedStyle CssText="border-bottom-style:solid;border-left-style:solid;border-right-style:solid;border-top-style:solid;" />
                    <CellStyle CssText="padding-left:8px;padding-top:2px; color:#black;white-space:nowrap;" />
                </eo:GridItemStyleSet>
            </ItemStyles>
            <ColumnHeaderTextStyle CssText="text-align:center; font-size: x-small;" />
            <ColumnHeaderStyle CssText="background-image:url('00050401');padding-left:8px;padding-top:2px;" />
            <Columns>


                 <eo:StaticColumn HeaderText="IdeSocial_Deposito" AllowSort="False" 
                    DataField="IdeSocial_Deposito" Name="IdeSocial_Deposito" ReadOnly="True"  
                    Width="0">
                    <CellStyle CssText="text-align:center; font-size: xx-small;" />
                </eo:StaticColumn>                


                 <eo:CheckBoxColumn HeaderText=""  Width="30" >
                </eo:CheckBoxColumn>

                <eo:StaticColumn HeaderText="Empresa" AllowSort="False" 
                    DataField="Empresa" Name="Empresa" ReadOnly="True" 
                    SortOrder="Ascending" Text="" Width="140">
                    <CellStyle CssText="background-color:#ffffcc;font-family:Tahoma;text-align:left; font-size: xx-small;" />
                </eo:StaticColumn>

                <eo:StaticColumn HeaderText="Colaborador" AllowSort="False" 
                    DataField="Colaborador" Name="Colaborador" ReadOnly="True" 
                    SortOrder="Ascending" Text="" Width="1" Visible="false">
                    <CellStyle CssText="background-color:#ffffcc;font-family:Tahoma;text-align:left; font-size: xx-small;" />
                </eo:StaticColumn>

                <eo:StaticColumn HeaderText="GHE" AllowSort="False" 
                    DataField="NmAmb" Name="NmAmb" ReadOnly="True" Width="200">
                    <CellStyle CssText="text-align:center; font-size: xx-small;" />
                </eo:StaticColumn>

                <eo:StaticColumn HeaderText="CodAmb" AllowSort="False" 
                    DataField="CodAmb" Name="CodAmb" ReadOnly="True" Visible="false" 
                    Width="80">
                    <CellStyle CssText="text-align:center; font-size: xx-small;" />
                </eo:StaticColumn>

                <eo:StaticColumn HeaderText="Início Cond." AllowSort="False" 
                    DataField="DtIniCondicao" Name="DtIniCondicao" ReadOnly="True"  
                    Width="70">
                    <CellStyle CssText="text-align:center; font-size: xx-small;" />
                </eo:StaticColumn>

                <eo:StaticColumn HeaderText="Fim Condicao" AllowSort="False" 
                    DataField="DtFimCondicao" Name="DtFimondicao" ReadOnly="True"  
                    Width="70">
                    <CellStyle CssText="text-align:center; font-size: xx-small;" />
                </eo:StaticColumn>

                <eo:StaticColumn HeaderText="Criador" AllowSort="False" 
                    DataField="Criado_Por" Name="Criado_Por" ReadOnly="True"  
                    Width="60">
                    <CellStyle CssText="text-align:center; font-size: xx-small;" />
                </eo:StaticColumn>


                <eo:StaticColumn HeaderText="Criado em" AllowSort="False" 
                    DataField="Criado_Em" Name="Criado_Em" ReadOnly="True"
                    Width="82">
                    <CellStyle CssText="text-align:center; font-size: xx-small;" />
                </eo:StaticColumn>

                <eo:StaticColumn HeaderText="Nome_Arquivo" AllowSort="False" 
                    DataField="NomeArquivo" Name="NomeArquivo" ReadOnly="True"
                    Width="0">
                    <CellStyle CssText="text-align:center; font-size: xx-small;" />
                </eo:StaticColumn>


                <eo:StaticColumn HeaderText="nId_Laud_Tec" AllowSort="False" 
                    DataField="nId_Laud_Tec" Name="nId_Laud_Tec" ReadOnly="True"  
                    Width="0">
                    <CellStyle CssText="text-align:center; font-size: xx-small;" />
                </eo:StaticColumn>

                <eo:StaticColumn HeaderText="IdeSocial" AllowSort="False" 
                    DataField="IdeSocial" Name="IdeSocial" ReadOnly="True"  
                    Width="0">
                    <CellStyle CssText="text-align:center; font-size: xx-small;" />
                </eo:StaticColumn>                

                <eo:StaticColumn HeaderText="IdEmpregado" AllowSort="False" 
                    DataField="IdEmpregado" Name="IdEmpregado" ReadOnly="True"  
                    Width="0">
                    <CellStyle CssText="text-align:center; font-size: xx-small;" />
                </eo:StaticColumn>                


                <eo:StaticColumn HeaderText="Enviado em" AllowSort="False" 
                    DataField="Enviado_Em" Name="Enviado_Em" ReadOnly="True"
                    Width="82">
                    <CellStyle CssText="text-align:center; font-size: xx-small;" />
                </eo:StaticColumn>


                 <eo:ButtonColumn ButtonText="Detalhes" 
                    Name="Detalhes">
                    <CellStyle CssText="font-family:Arial;font-size:7pt;text-align:center;" />
                </eo:ButtonColumn>

                  <eo:ButtonColumn ButtonText="Visualizar XML" 
                    Name="Visualizar XML" >
                    <CellStyle CssText="font-family:Arial;font-size:7pt;text-align:center;" />
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

                    
                    </TD>
				</TR>
				<TR>
					<TD class="normalFont" style="HEIGHT: 15px" align="center"><P>
							<TABLE class="defaultFont" id="Table28" cellSpacing="0" cellPadding="0" width="590" align="right"
								border="0">
								<TR>
									<TD align="right">
                                        <asp:Label ID="lbl_IdeSocial_Deposito" runat="server" Text="0" Visible="False"></asp:Label>
                                        <asp:Label ID="lbl_Qtde" runat="server" Text="0" Visible="False"></asp:Label>
                                        <asp:Label ID="lbl_IdEPIClienteCA" runat="server" Text="0" Visible="False"></asp:Label>
                                        <asp:Label ID="lbl_IdCA" runat="server" Text="0" Visible="False"></asp:Label>
                                        <asp:label id="lblTotRegistros" visible="false" runat="server"></asp:label></TD>
								</TR>
							</TABLE>
							<asp:ListBox ID="lst_Detalhes" runat="server" Height="105px" Visible="False" Width="664px" Font-Size="Small" AutoPostBack="True" BackColor="#FFFFCC" Font-Names="Courier New" OnSelectedIndexChanged="lst_Detalhes_SelectedIndexChanged"></asp:ListBox>
                            <asp:ListBox ID="lst_Id_Detalhes" runat="server" Visible="False"></asp:ListBox>
                        <br />
                        <asp:Button ID="cmd_Fechar_Det" runat="server" Font-Size="X-Small" Text="Fechar Detalhes" Width="87px" OnClick="cmd_Fechar_Det_Click" Visible="False" />
                        &nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="cmd_Proc_Lote" runat="server" Font-Size="X-Small" Text="XML Processamento Lote" Width="137px" OnClick="cmd_Proc_Lote_Click" Enabled="False" Visible="False" />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="cmd_Proc_Evento" runat="server" Font-Size="X-Small" Text="XML Processamento Evento" Width="137px" OnClick="cmd_Proc_Evento_Click" Enabled="False" Visible="False" />
                        										
					&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="cmd_Proc_Atualizar" runat="server" Font-Size="X-Small" Text="Atualizar Status Processamento" Width="162px" OnClick="cmd_Proc_Proc_Atualizar_Click" Enabled="False" Visible="False" />
                        
										
					</TD>
				</TR>
			</TABLE>
			<%--<iframe id="SubDados" name="SubDados" align="middle" marginWidth="0" marginHeight="0" frameBorder="0"
				width="600" scrolling="no" src="../DadosEmpresa/SubDadosNull.aspx" >
			</iframe>--%>
<%--		</form>
	</body>
</HTML>
--%>

                  <br />
                  <asp:Button ID="cmd_Voltar" runat="server" BackColor="#999999" 
                      CssClass="buttonFoto" Font-Size="XX-Small" onclick="cmd_Voltar_Click" 
                      Text="Voltar" Width="132px" />
                  <br />

        
         <eo:MsgBox ID="MsgBox1" runat="server" BackColor="#47729F" ControlSkinID="None" 
        HeaderHtml="Dialog Title" Height="100px" Width="168px">
        <HeaderStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #ffbf00 3px solid; padding-left: 4px; font-weight: bold; font-size: 11px; padding-bottom: 2px; color: white; padding-top: 2px; border-bottom: #22456a 1px solid; font-family: verdana" />
        <ContentStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #7d97b6 1px solid; padding-left: 4px; border-left-width: 1px; font-size: 11px; border-left-color: #728eb8; padding-bottom: 4px; color: white; padding-top: 4px; border-bottom: #22456a 1px solid; font-family: verdana" />
        <FooterStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #7d97b6 1px solid; padding-left: 4px; border-left-width: 1px; font-size: 11px; border-left-color: #728eb8; padding-bottom: 4px; color: white; padding-top: 4px; border-bottom: #22456a 1px solid; font-family: verdana" />
        </eo:MsgBox>
         <eo:MsgBox ID="MsgBox2" runat="server" BackColor="#47729F" ControlSkinID="None" 
        HeaderHtml="Dialog Title" Height="100px" Width="550px" OnButtonClick="MsgBox2_ButtonClick" >
        <HeaderStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #ffbf00 3px solid; padding-left: 4px; font-weight: bold; font-size: 11px; padding-bottom: 2px; color: white; padding-top: 2px; border-bottom: #22456a 1px solid; font-family: verdana" />
        <ContentStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #7d97b6 1px solid; padding-left: 4px; border-left-width: 1px; font-size: 11px; border-left-color: #728eb8; padding-bottom: 4px; color: white; padding-top: 4px; border-bottom: #22456a 1px solid; font-family: verdana" />
        <FooterStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #7d97b6 1px solid; padding-left: 4px; border-left-width: 1px; font-size: 11px; border-left-color: #728eb8; padding-bottom: 4px; color: white; padding-top: 4px; border-bottom: #22456a 1px solid; font-family: verdana" />
        </eo:MsgBox>


</asp:Content>
