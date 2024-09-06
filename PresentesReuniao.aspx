<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PresentesReuniao.aspx.cs" Inherits="Ilitera.Net.PresentesReuniao" %>

<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Ilitera.NET</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<link href="https://cdn.jsdelivr.net/npm/bootstrap@5.2.3/dist/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
        <link href="css/forms.css" rel="stylesheet" type="text/css" />
        <link href="css/tabelas.css" rel="stylesheet" type="text/css" />
		<script src="https://ajax.googleapis.com/ajax/libs/jquery/3.7.1/jquery.min.js"></script>
		<link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.11.3/font/bootstrap-icons.min.css" />
		<link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" />
		<script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js"></script>
        <link rel="preconnect" href="https://fonts.googleapis.com">
        <link rel="preconnect" href="https://fonts.gstatic.com" crossorigin>
        <link href="https://fonts.googleapis.com/css2?family=Ubuntu&display=swap" rel="stylesheet">
	</HEAD>
    <script type="text/javascript">
    var g_itemIndex = -1;
    var g_cellIndex = -1;
    function OnCellSelected(grid) {
        var cell = grid.getSelectedCell();
        grid.raiseItemCommandEvent(cell.getItemIndex(), "Seleção");
    }
    function OnItemCommand(grid, itemIndex, colIndex, commandName) {
        //grid.raiseItemCommandEvent(itemIndex, commandName);
        grid.raiseItemCommandEvent(itemIndex, colIndex.toString());
    }
    function abrirReuniao(parametro) {
        window.open('AddReuniao.aspx?r=' + parametro, 'CursoEmpresa','scrollbars=yes, resizable=yes, toolbar=no, addressbar=no, menubar=no, width=850px, height=1100px');
    }
    </script>
    <style>
         @font-face {
            font-family: 'UniviaPro';
            src: url('css/css/UniviaPro-Regular.otf') format('opentype');
            font-weight: normal;
            font-style: normal;
        }

        .editar>a{
            color: #1C9489 !important;
            text-decoration: none !important;
        }

    </style>
	<body>
		<form id="frmPresenteReuniao" method="post" runat="server">
			<div class="container d-flex ms-5 ps-4 justify-content-center">
				<div class="row gx-3 gy-2 mt-5" style="width: 1100px">
					<div class="col-12 mb-2">
						<div class="row">
							<div class="col-12 subtituloBG text-center pt-2">
								<asp:Label ID="lblPresenteReuniao" runat="server" CssClass="subtitulo" style="font-family: 'UniviaPro', sans-serif !important;font-size: 16px !important;  font-weight: 500 !important;">Presentes na Reunião</asp:Label>
							</div>
							<%-- Tabela --%>
							<div class="col-6 gx-2 gy-2 mt-4">
								<asp:Label ID="lblMembros_Cipa" runat="server" class="tituloLabel form-check-label mt-2" Text="Membros da Cipa" style="font-family: 'Ubuntu', sans-serif !important;"></asp:Label>
							  <eo:Grid ID="grdMembrosCipa" runat="server" ColumnHeaderAscImage="00050403" 
                                                ColumnHeaderDescImage="00050404" FixedColumnCount="1" GridLines="Both" Height="200px" Width="600px" KeyField="IdEmprcImaegado"
                                                ColumnHeaderHeight="30" ItemHeight="30" ClientSideOnItemCommand="OnItemCommand" ClientSideOnCellSelected="OnCellSelected" CssClass="grid" OnItemCommand="grdMembrosCipa_ItemCommand">
                                            <itemstyles>
                                                <eo:GridItemStyleSet>
                                                    <ItemStyle CssText="background-color: #FAFAFA" />
                                                    <ItemHoverStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500;  text-align: left; height: 30px !important; background-color: rgb(28, 148, 137, 0.32); color: #FFFFFF; padding: 0.3rem 0.3rem 0.3rem 0.5rem; " />
                                                    <AlternatingItemStyle CssText="background-color: #F2F2F2;" />
                                                    <AlternatingItemHoverStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500;  text-align: left; height: 30px !important; background-color: rgba(28, 148, 137, 0.32); color: #FFFFFF; padding: 0.3rem 0.3rem 0.3rem 0.5rem;" />
                                                    <SelectedStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500;  text-align: left; height: 30px !important; background-color: rgba(176, 216, 213, 0.32); color: #FFFFFF; padding: 0.3rem 0.3rem 0.3rem 0.5rem;" />
                                                    <CellStyle CssText="padding: 0.3rem 0.3rem 0.3rem 0.5rem; height: 30px !important;" />
                                                </eo:GridItemStyleSet>
                                            </itemstyles>
                                            <ColumnHeaderStyle CssClass="tabelaC colunas" CssText="font-family: 'UniviaPro', sans-serif !important; font-weight: 400 !important;"/>
                                            <Columns>
                                                <eo:ButtonColumn ButtonText="Editar" CommandName="Editar" Name="Editar" DataField="Editar" Width="60" ButtonType="LinkButton">
                                                    <CellStyle CssText="font-family: 'UniviaPro'; font-size: 12px; font-weight: normal; color: #7D7B7B; text-align: center; height: 30px !important;" CssClass="tituloLabel editar"/>
                                                </eo:ButtonColumn>
                                                <eo:StaticColumn HeaderText="Grupo" AllowSort="True"  
                                                    DataField="Grupo" Name="Grupo" ReadOnly="True" 
                                                    Width="150">
                                                    <CellStyle CssText="font-family: 'UniviaPro'; font-size: 12px; font-weight: normal; color: #7D7B7B; text-align: left; height: 30px !important; " />                   
                                                </eo:StaticColumn>
                                                <eo:StaticColumn HeaderText="Cargo" AllowSort="True"  
                                                    DataField="Cargo" Name="Cargo" ReadOnly="True" 
                                                    Width="150">
                                                    <CellStyle CssText="font-family: 'UniviaPro'; font-size: 12px; font-weight: normal; color: #7D7B7B; text-align: left; height: 30px !important; " />                   
                                                </eo:StaticColumn>
                                                <eo:StaticColumn HeaderText="Nome" AllowSort="True"  
                                                    DataField="Nome" Name="Nome" ReadOnly="True" 
                                                    Width="180">
                                                    <CellStyle CssText="font-family: 'UniviaPro'; font-size: 12px; font-weight: normal; color: #7D7B7B; text-align: left; height: 30px !important; " />                   
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

                                </div>

                                   <div class="col-1 gx-4 gy-4 text-center" style="margin-left: 8px">
                                        <div class="row mt-4">
                                            <div class="col-12 gx-4 gy-2">
                                                <asp:ImageButton ID="btnSubir" ImageUrl="Images/subir.svg" runat="server" OnClick="btnSubir_Click" />
                                            </div>
                                            <div class="col-12 gx-4 gy-2">
                                                <asp:ImageButton ID="btnAdicionar" ImageUrl="Images/direita.svg" runat="server" OnClick="btnAdicionar_Click" />
                                            </div>
                                            <div class="col-12 gx-4 gy-2">
                                                <asp:ImageButton ID="btnRemover" ImageUrl="Images/subir.svg" runat="server" OnClick="btnRemover_Click" />
                                            </div>
                                            <div class="col-12 gx-4 gy-2">
                                                 <asp:ImageButton ID="btnDescer" ImageUrl="Images/descer.svg" runat="server" OnClick="btnDescer_Click" />
                                            </div>
                                    </div>
                                </div>

                                      
                        <!--Tabela-->
                        <div class="col-4 gx-2 gy-2 mt-4" >
                           <div class="row">
                               <div class="col-6" style="margin-right: 2px; margin-top: 1.5px">
                                   	<asp:Label ID="Label1" runat="server" class="tituloLabel form-check-label mt-2" Text="Presentes na reunião" style="font-family: 'Ubuntu', sans-serif !important;"></asp:Label>
                                    <eo:Grid ID="grdMembrosCipa2" runat="server" FixedColumnCount="1" ColumnHeaderDividerOffset="6" 
                                        ColumnHeaderAscImage="00050403" ColumnHeaderDescImage="00050404"
                                        GridLines="Both" PageSize="500" KeyField="IdReuniaoPresencaCipa" CssClass="grid"
                                        ColumnHeaderHeight="30" ItemHeight="30" Width="480px">
                                    <ItemStyles>
                                        <eo:GridItemStyleSet>
                                            <ItemStyle CssText="background-color: #FAFAFA" />
                                            <ItemHoverStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500;  text-align: left; height: 30px !important; background-color: rgb(28, 148, 137, 0.32); color: #FFFFFF; padding: 0.3rem 0.3rem 0.3rem 0.5rem; " />
                                            <AlternatingItemStyle CssText="background-color: #F2F2F2;" />
                                            <AlternatingItemHoverStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500;  text-align: left; height: 30px !important; background-color: rgba(28, 148, 137, 0.32); color: #FFFFFF; padding: 0.3rem 0.3rem 0.3rem 0.5rem;" />
                                            <SelectedStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500;  text-align: left; height: 30px !important; background-color: rgba(176, 216, 213, 0.32); color: #FFFFFF; padding: 0.3rem 0.3rem 0.3rem 0.5rem;" />
                                            <CellStyle CssText="padding: 0.3rem 0.3rem 0.3rem 0.5rem; height: 30px !important;" />
                                        </eo:GridItemStyleSet>
                                    </ItemStyles>
                                    <ColumnHeaderTextStyle CssText="" />
                                    <ColumnHeaderStyle CssClass="tabelaC colunas" CssText="font-family: 'UniviaPro', sans-serif !important; font-weight: 400 !important;"/>
                                    <Columns>
                                        <eo:StaticColumn HeaderText="Nome" AllowSort="True" 
                                            DataField="Nome2" Name="Nome2" ReadOnly="True" Width="180">
                                            <CellStyle CssText="font-family: 'UniviaPro'; font-size: 12px; font-weight: normal; color: #7D7B7B; text-align: left; height: 30px !important; padding-left: .5rem !important" />
                                        </eo:StaticColumn>
                                        <eo:StaticColumn HeaderText="Cargo" AllowSort="True" 
                                            DataField="Cargo2" Name="Cargo2" ReadOnly="True" Width="150">
                                            <CellStyle CssText="font-family: 'UniviaPro'; font-size: 12px; font-weight: normal; color: #7D7B7B; text-align: left; height: 30px !important; " />
                                        </eo:StaticColumn>
                                        <eo:StaticColumn HeaderText="Presença" AllowSort="True" 
                                            DataField="Presenca" Name="Presenca" ReadOnly="True" Width="100">
                                            <CellStyle CssText="font-family: 'UniviaPro'; font-size: 12px; font-weight: normal; color: #7D7B7B; text-align: left; height: 30px !important; " />
                                        </eo:StaticColumn>
                                </Columns>
                                </eo:Grid>
                               </div>
                           </div>
                        </div>

                             <div class="col-md-2 gx-2 gy-2">
                                    <asp:Label runat="server" Text="Membros da Cipa" CssClass="tituloLabel form-label col-form-label col-form-label-sm" style="font-family: 'Ubuntu', sans-serif !important;"></asp:Label>
                                    <asp:RadioButtonList ID="rbMembrosCipa" runat="server" RepeatColumns="3" tabIndex="1" CssClass="texto form-control-sm ms-4" Width="520" AutoPostBack="True" OnSelectedIndexChanged="rbMembrosCipa_SelectedIndexChanged">
                                        <asp:ListItem Value="1" Selected="True">Titulares</asp:ListItem>
                                        <asp:ListItem Value="2">Suplentes</asp:ListItem>
                                        <asp:ListItem Value="3">Outros</asp:ListItem>
                                    </asp:RadioButtonList>
                            </div>
                            <div class="col-12 gx-2 gy-2">
                                <asp:Button ID="btnCadastrarOutros" runat="server" CssClass="btn" Text="Cadastrar Outros" Enabled="False" OnClick="btnCadastrarOutros_Click" style="font-family: 'Ubuntu', sans-serif !important; font-size: 14.5px !important; text-align:center; vertical-align:central;"/>
                            </div>
                        </div>
					</div>
				</div>
                <eo:MsgBox ID="MsgBox1" runat="server" BackColor="#ffffff" ControlSkinID="None" HeaderHtml="Dialog Title" Height="192px" Width="345px" CssClass="card border border-1 p-2 text-center" IconUrl="Images/alerta_icon.svg">
                    <HeaderStyleActive CssText="font-family: Ubuntu; font-size: 14px; font-weight: bold; color: #1C9489; text-align: center; padding: 5px;" />
                    <ContentStyleActive CssText="font-family: Ubuntu; font-size: 14px; font-weight: bold; color: #7D7B7B; text-align: center; padding: 5px; width: 345px; height: 60px" />
                    <FooterStyleActive CssText="width: 345px;" />
                </eo:MsgBox>
			</div>
			</form>
	</body>
</HTML>