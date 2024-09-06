<%@ Page Language="C#"   AutoEventWireup="True"  MasterPageFile="~/Site.Master" CodeBehind="Alerta_EPI.aspx.cs"  Inherits="Ilitera.Net.ControleEPI.Alerta_EPI" Title="Ilitera.Net" %>

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
        .buttonFoto
        {}
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

  

<%--<iframe id="SubDados" name="SubDados" align="middle" marginWidth="0" marginHeight="0" frameBorder="0"
				width="600" scrolling="no" src="../DadosEmpresa/SubDadosNull.aspx" >
			</iframe>--%>		
	

<div class="col-11 subtituloBG pt-2">
       <asp:Label ID="lblExCli" runat="server" SkinID="TitleFont" CssClass="subtitulo">Alerta de vencimento de EPI</asp:Label>
   </div>

  <div class="col-11">
      <div class="row">
          <div class="col-md-5 gx-3 gy-2 mb-3">
            <asp:Label ID="Label1" runat="server" CssClass="tituloLabel form-label" Text="e-mail primário para alerta"></asp:Label>
            <asp:TextBox ID="txt_Email1" runat="server" CssClass="texto form-control form-control-sm" MaxLength="100" Width="210px"></asp:TextBox>
          </div>

          <div class="col-md-5 gx-3 gy-2 mb-3">
            <asp:Label ID="Label2" runat="server" CssClass="tituloLabel form-label" Text="e-mail secundário para alerta"></asp:Label>
            <asp:TextBox ID="txt_Email2" runat="server" CssClass="texto form-control form-control-sm" MaxLength="100" Width="210px"></asp:TextBox>
          </div>

     <div class="col-md-5 gx-3 gy-2">
	  <asp:label id="Label3" runat="server" CssClass="tituloLabel form-label col-form-label">Dias de antecedência para vencimento</asp:label>
	  <asp:TextBox ID="txt_Dias" runat="server" CssClass="texto form-control form-control-sm" Width="82px" MaxLength="10"></asp:TextBox>
	</div>

     <div class="col-md-5 mt-4 gx-4 mb-2">
	  <asp:Checkbox ID="chk_EPI_Apenas" runat="server" CssClass="texto form-check-input bg-transparent border-0 ms-3" AutoPostBack="True" Text="Alerta por EPI, não por CA individualmente" Width="1200px">
	  </asp:Checkbox>
	</div>

     <div class="col-md-5 mt-4 gx-4">
         <asp:Button ID="cmd_Salvar" runat="server" BackColor="#FF5050" CssClass="btn" onclick="cmd_Salvar_Click" Text="Salvar e-mails" Width="113px" />
     </div>

     <div class="col-12 mt-4 gx-4">
         <asp:label id="lblExCli0" runat="server" CssClass="tituloLabel form-label col-form-label">Selecione EPI(s) para alerta</asp:label>
         <asp:CheckBoxList ID="chk_EPI" runat="server" CssClass="texto form-check-input bg-transparent border-0 ms-3" AutoPostBack="True" onselectedindexchanged="chk_EPI_SelectedIndexChanged" RepeatColumns="5" Width="1200px"></asp:CheckBoxList>
     </div>

  </div> 
	   </div>

                         
                            
                 <eo:Grid ID="grd_Clinicos" runat="server" CssClass="grid ms-2"
                ColumnHeaderAscImage="00050403" 
                ColumnHeaderDescImage="00050404"
                FixedColumnCount="1"
                GridLines="Both" Height="435px" Width="1018px" ColumnHeaderDividerOffset="6" 
                ColumnHeaderHeight="30" ItemHeight="30" KeyField="nId_Empregado"   >
            <ItemStyles><eo:GridItemStyleSet><ItemStyle CssText="background-color: #FAFAFA" />
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
                <eo:StaticColumn HeaderText="Colaborador" AllowSort="False" 
                    DataField="Colaborador" Name="Colaborador" ReadOnly="True" 
                    SortOrder="Ascending" Text="" Width="280">
                    <CellStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500; color: #7D7B7B !important; text-align: left; height: 35px !important; padding: 0.3rem 0.3rem 0.3rem 0.5rem; height: 35px !important;" />
                </eo:StaticColumn>


                <eo:StaticColumn HeaderText="Qtde" AllowSort="False" 
                    DataField="Qtde_Entregue" Name="Qtde_Entregue" ReadOnly="True" 
                    Width="60">
                    <CellStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500; color: #7D7B7B !important; text-align: left; height: 35px !important; padding: 0.3rem 0.3rem 0.3rem 0.5rem; height: 35px !important;" />
                </eo:StaticColumn>

                <eo:StaticColumn HeaderText="EPI" AllowSort="False" 
                    DataField="EPI" Name="EPI" ReadOnly="True"  
                    Width="220">
                    <CellStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500; color: #7D7B7B !important; text-align: left; height: 35px !important; padding: 0.3rem 0.3rem 0.3rem 0.5rem; height: 35px !important;" />
                </eo:StaticColumn>

                <eo:StaticColumn HeaderText="CA" AllowSort="False" 
                    DataField="CA" Name="CA" ReadOnly="True"  
                    Width="70">
                    <CellStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500; color: #7D7B7B !important; text-align: left; height: 35px !important; padding: 0.3rem 0.3rem 0.3rem 0.5rem; height: 35px !important;" />
                </eo:StaticColumn>


                <eo:StaticColumn HeaderText="Intervalo" AllowSort="False" 
                    DataField="Intervalo" Name="Intervalo" ReadOnly="True"  
                    Width="65">
                    <CellStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500; color: #7D7B7B !important; text-align: left; height: 35px !important; padding: 0.3rem 0.3rem 0.3rem 0.5rem; height: 35px !important;" />
                </eo:StaticColumn>

                <eo:StaticColumn HeaderText="Período" AllowSort="False" 
                    DataField="Periodo" Name="Periodo" ReadOnly="True"  
                    Width="80">
                    <CellStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500; color: #7D7B7B !important; text-align: left; height: 35px !important; padding: 0.3rem 0.3rem 0.3rem 0.5rem; height: 35px !important;" />
                </eo:StaticColumn>

                <eo:StaticColumn HeaderText="Próximo Rec." AllowSort="False" 
                    DataField="Proximo_Recebimento" Name="Proximo_Recebimento" ReadOnly="True" 
                    Width="85" >
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

                    
                    </TD>
				</TR>

				<%--		</form>
	</body>
</HTML>
--%>
			</TABLE>
			<%--<iframe id="SubDados" name="SubDados" align="middle" marginWidth="0" marginHeight="0" frameBorder="0"
				width="600" scrolling="no" src="../DadosEmpresa/SubDadosNull.aspx" >
			</iframe>--%>
<%--		</form>
	</body>
</HTML>
--%>

  <div class="col-12 mt-4 gx-4">
      <asp:Button ID="cmd_Voltar" runat="server" CssClass="btn2" onclick="cmd_Voltar_Click" Text="Voltar" />
  </div>
            
         <eo:MsgBox ID="MsgBox2" runat="server" BackColor="#47729F" ControlSkinID="None" 
        HeaderHtml="Dialog Title" Height="100px" Width="350px" OnButtonClick="MsgBox2_ButtonClick">
        <HeaderStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #ffbf00 3px solid; padding-left: 4px; font-weight: bold; font-size: 11px; padding-bottom: 2px; color: white; padding-top: 2px; border-bottom: #22456a 1px solid; font-family: verdana" />
        <ContentStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #7d97b6 1px solid; padding-left: 4px; border-left-width: 1px; font-size: 11px; border-left-color: #728eb8; padding-bottom: 4px; color: white; padding-top: 4px; border-bottom: #22456a 1px solid; font-family: verdana" />
        <FooterStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #7d97b6 1px solid; padding-left: 4px; border-left-width: 1px; font-size: 11px; border-left-color: #728eb8; padding-bottom: 4px; color: white; padding-top: 4px; border-bottom: #22456a 1px solid; font-family: verdana" />
        </eo:MsgBox>
</div>
  </div>

</asp:Content>