<%@ Page Language="C#"   AutoEventWireup="True"  MasterPageFile="~/Site.Master" CodeBehind="Fornecimento_Lista.aspx.cs"  Inherits="Ilitera.Net.ControleEPI.Fornecimento_Lista" Title="Ilitera.Net" %>

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
    </style>
</asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent" >
    <div class="container-fluid d-flex ms-5 ps-4">
        <div class="row gx-3 gy-2">
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
	

            <div class="col-11 subtituloBG"  style="padding-top: 10px">
                <asp:label id="lblExCli" runat="server" SkinID="TitleFont" CssClass="subtitulo">Fornecimento EPI/CA Automático</asp:label>
                <asp:panel id="Panel2" runat="server" Width="662px"></asp:panel>
            </div>

	
        <div class="col-11">
            <div class="row">
                 <div class="col-md-4 gx-3 gy-2">
                        <asp:label id="Label1" runat="server" CssClass="tituloLabel form-label">Data de Fornecimento:</asp:label>
                        <asp:TextBox ID="txt_Data" runat="server" CssClass="texto form-select form-select-sm"></asp:TextBox>
		        </div>
                <div class="col-md-1 ms-3 mt-4 gx-3 gy-2">
                    <asp:RadioButton ID="rd_Normal" runat="server" AutoPostBack="True" Checked="True" GroupName="Carga" OnCheckedChanged="rd_Normal_CheckedChanged" Text="Grid Normal" Cssclass="texto form-check-label border-0 bg-transparent" />
		        </div>
                <div class="col-md-2 ms-3 mt-4 gx-3 gy-2">
                    <asp:RadioButton ID="rd_CA" runat="server" AutoPostBack="True" GroupName="Carga" OnCheckedChanged="rd_CA_CheckedChanged" Text="Grid com todos CAs" Cssclass="texto form-check-label border-0 bg-transparent"  />
		        </div>

                <div class="col-11 mt-4">
			        <div class="row">
				        <div class="col gx-3 gx-2">
                             <eo:Grid ID="grd_Clinicos" runat="server"  
                                ColumnHeaderAscImage="00050403" 
                                ColumnHeaderDescImage="00050404"  
                                FixedColumnCount="1" GridLines="Both" Cssclass="grid"
                                Height="435px" Width="1120px" ColumnHeaderDividerOffset="6" 
                                ColumnHeaderHeight="30" ItemHeight="30" KeyField="nId_Empregado" 
                                OnItemCommand="grd_Clinicos_ItemCommand" 
                                ClientSideOnItemCommand="OnItemCommand"  >
                            <ItemStyles>
                                <eo:GridItemStyleSet>
                                    <ItemStyle CssText="background-color: #FAFAFA" />
                                    <AlternatingItemStyle CssText="background-color: #F2F2F2;" />
                                    <SelectedStyle CssText="border-bottom-style:solid;border-left-style:solid;border-right-style:solid;border-top-style:solid;" />
                                    <CellStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500; color: #7D7B7B !important; text-align: left; height: 35px !important; padding: 0.3rem 0.3rem 0.3rem 0.5rem; height: 35px !important;" />
                                </eo:GridItemStyleSet>
                            </ItemStyles>
                            <ColumnHeaderTextStyle CssText="" />
                            <ColumnHeaderStyle CssClass="tabelaC colunas" />
                            <Columns>
                                <eo:StaticColumn HeaderText="Colaborador" AllowSort="False" 
                                    DataField="Colaborador" Name="Colaborador" ReadOnly="True" 
                                    SortOrder="Ascending" Text="" Width="200">
                                    <CellStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500; color: #7D7B7B !important; text-align: left; height: 35px !important; padding: 0.3rem 0.3rem 0.3rem 0.5rem; height: 35px !important;" />
                                </eo:StaticColumn>

                                <eo:StaticColumn HeaderText="Qtde" AllowSort="False" 
                                    DataField="Qtde_Entregue" Name="Qtde_Entregue" ReadOnly="True" Width="50">
                                    <CellStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500; color: #7D7B7B !important; text-align: left; height: 35px !important; padding: 0.3rem 0.3rem 0.3rem 0.5rem; height: 35px !important;" />
                                </eo:StaticColumn>

                                <eo:StaticColumn HeaderText="EPI" AllowSort="False" 
                                    DataField="EPI" Name="EPI" ReadOnly="True" 
                                    Width="180">
                                    <CellStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500; color: #7D7B7B !important; text-align: left; height: 35px !important; padding: 0.3rem 0.3rem 0.3rem 0.5rem; height: 35px !important;" />
                                </eo:StaticColumn>

                                <eo:StaticColumn HeaderText="CA" AllowSort="False" 
                                    DataField="CA" Name="CA" ReadOnly="True"  
                                    Width="50">
                                    <CellStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500; color: #7D7B7B !important; text-align: left; height: 35px !important; padding: 0.3rem 0.3rem 0.3rem 0.5rem; height: 35px !important;" />
                                </eo:StaticColumn>

                                <eo:StaticColumn HeaderText="Último Rec." AllowSort="False" 
                                    DataField="Ultimo_Receb" Name="Ultimo_Receb" ReadOnly="True"  
                                    Width="120">
                                    <CellStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500; color: #7D7B7B !important; text-align: left; height: 35px !important; padding: 0.3rem 0.3rem 0.3rem 0.5rem; height: 35px !important;" />
                                </eo:StaticColumn>


                                <eo:StaticColumn HeaderText="Intervalo" AllowSort="False" 
                                    DataField="Intervalo" Name="Intervalo" ReadOnly="True"
                                    Width="55">
                                    <CellStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500; color: #7D7B7B !important; text-align: left; height: 35px !important; padding: 0.3rem 0.3rem 0.3rem 0.5rem; height: 35px !important;" />
                                </eo:StaticColumn>

                                <eo:StaticColumn HeaderText="Período" AllowSort="False" 
                                    DataField="Periodo" Name="Periodo" ReadOnly="True"
                                    Width="70">
                                   <CellStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500; color: #7D7B7B !important; text-align: left; height: 35px !important; padding: 0.3rem 0.3rem 0.3rem 0.5rem; height: 35px !important;" />
                                </eo:StaticColumn>

                                <eo:StaticColumn HeaderText="Data Rec." AllowSort="False" 
                                    DataField="Data_Recebimento" Name="Data_Recebimento" ReadOnly="True"  
                                    Width="75" >
                                    <CellStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500; color: #7D7B7B !important; text-align: left; height: 35px !important; padding: 0.3rem 0.3rem 0.3rem 0.5rem; height: 35px !important;" />
                                </eo:StaticColumn>


                                <eo:StaticColumn HeaderText="Próximo Rec." AllowSort="False" 
                                    DataField="Proximo_Recebimento" Name="Proximo_Recebimento" ReadOnly="True"  
                                    Width="120">
                                    <CellStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500; color: #7D7B7B !important; text-align: left; height: 35px !important; padding: 0.3rem 0.3rem 0.3rem 0.5rem; height: 35px !important;" />
                                </eo:StaticColumn>

                                <eo:StaticColumn HeaderText="IdEPIClienteCA" AllowSort="False" 
                                    DataField="IdEPIClienteCA" Name="Proximo_Recebimento" ReadOnly="True"  
                                    Width="0">
                                    <CellStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500; color: #7D7B7B !important; text-align: left; height: 35px !important; padding: 0.3rem 0.3rem 0.3rem 0.5rem; height: 35px !important;" />
                                </eo:StaticColumn>

                                <eo:StaticColumn HeaderText="IdCA" AllowSort="False" 
                                    DataField="IdCA" Name="Proximo_Recebimento" ReadOnly="True"  
                                    Width="0">
                                    <CellStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500; color: #7D7B7B !important; text-align: left; height: 35px !important; padding: 0.3rem 0.3rem 0.3rem 0.5rem; height: 35px !important;" />
                                </eo:StaticColumn>                

                                  <eo:ButtonColumn ButtonText="Entregar EPI" 
                                    Name="Entregar EPI">
                                    <CellStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500; color: #7D7B7B !important; text-align: left; height: 35px !important; padding: 0.3rem 0.3rem 0.3rem 0.5rem; height: 35px !important;" />
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
                        <div>
                            <asp:label id="lblTotRegistros" runat="server"></asp:label></TD>

                        </div>
                    </div>
                </div>

                <asp:Label ID="lbl_Id_Empregado" runat="server" Text="0" Visible="False"></asp:Label>
                <asp:Label ID="lbl_Qtde" runat="server" Text="0" Visible="False"></asp:Label>
                <asp:Label ID="lbl_IdEPIClienteCA" runat="server" Text="0" Visible="False"></asp:Label>
                <asp:Label ID="lbl_IdCA" runat="server" Text="0" Visible="False"></asp:Label>
								
			<%--<iframe id="SubDados" name="SubDados" align="middle" marginWidth="0" marginHeight="0" frameBorder="0"
				width="600" scrolling="no" src="../DadosEmpresa/SubDadosNull.aspx" >
			</iframe>--%>
<%--		</form>
	</body>
</HTML>
--%>
                <div class="col-md-3">
                    <div class="row">
                        <div class="col-8">
                            <asp:Button ID="cmd_Voltar" runat="server" CssClass="btn" Text="Voltar" onclick="cmd_Voltar_Click" />
                        </div>
                    </div>
                </div>

                  <br />
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

            </div>
        </div>
    </div>
</div>

</asp:Content>