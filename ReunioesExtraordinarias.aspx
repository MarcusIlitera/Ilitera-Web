<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReunioesExtraordinarias.aspx.cs" Title="Ilitera.Net" Inherits="Ilitera.Net.ReunioesExtraordinarias" EnableEventValidation="false"
    ValidateRequest="false"%>

<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>

<!DOCTYPE html>

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
    </script>
	<style type="text/css">
     @font-face {
            font-family: 'UniviaPro';
            src: url('css/css/UniviaPro-Regular.otf') format('opentype');
            font-weight: normal;
            font-style: normal;
        }

        /* Agora, você pode usar a fonte em qualquer lugar no CSS */
        body {
            font-family: 'UniviaPro', sans-serif;
        }
        .editar>a{
            color: #1C9489 !important;
            text-decoration: none !important;
        }
	</style>

	<body>
		<form id="frmReunioesExtraordinarias" method="post" runat="server">
			<div class="container d-flex ms-5 ps-5 justify-content-center">
			<div class="row gx-5 gy-2 mt-4" style="width: 700px">
					<div class="col-12 subtituloBG text-center pt-2">
						<asp:label id="lblReunioesExtraordinarias" runat="server" CssClass="subtitulo" style="margin-left: 0px !important;font-family: 'UniviaPro', sans-serif !important;font-size: 16px !important;  font-weight: 500 !important;">Reuniões Extraordinárias</asp:label>
					</div>
					<div class="col-5 gx-2 gy-2">
						<div class="row">
							<div class="col-6 gx-2 gy-2 pt-3">
                                   <eo:grid id="grdReunioesExtraordinarias" runat="server" fixedcolumncount="1" columnheaderdivideroffset="6"
                                       columnheaderascimage="00050403" columnheaderdescimage="00050404"
                                       gridlines="Both" pagesize="500" keyfield="IdReuniao" cssclass="grid"
                                       columnheaderheight="30" itemheight="30" width="700px" ClientSideOnItemCommand="OnItemCommand" ClientSideOnCellSelected="OnCellSelected" OnItemCommand="grdReunioesExtraordinarias_ItemCommand">
                                    <ItemStyles>
                                        <eo:GridItemStyleSet>
                                            <ItemStyle CssText="background-color: #FAFAFA;" />
                                            <ItemHoverStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500;  text-align: left; height: 30px !important; background: rgba(28, 148, 137, 0.80); color: #FFFFFF; padding: 0.3rem 0.3rem 0.3rem 0.5rem; " />
                                            <AlternatingItemStyle CssText="background-color: #F2F2F2;" />
                                            <AlternatingItemHoverStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500;  text-align: left; height: 30px !important; background: rgba(28, 148, 137, 0.80); color: #FFFFFF; padding: 0.3rem 0.3rem 0.3rem 0.5rem;" />
                                            <SelectedStyle CssText="border-bottom-style:solid;border-left-style:solid;border-right-style:solid;border-top-style:solid;" />
                                            <CellStyle CssText="padding: 0.3rem 0.3rem 0.3rem 0.5rem; height: 30px !important;" />
                                        </eo:GridItemStyleSet>
                                    </ItemStyles>
                                    <ColumnHeaderTextStyle CssText="" />
                                    <ColumnHeaderStyle CssClass="tabelaC colunas" CssText="font-family: 'UniviaPro', sans-serif !important; font-weight: 400 !important;"/>
                                    <Columns>
                                        <eo:ButtonColumn ButtonText="Editar" CommandName="Editar" Name="Editar" DataField="Editar" Width="60" ButtonType="LinkButton">
                                            <CellStyle CssText="font-family: 'UniviaPro'; font-size: 12px; font-weight: normal; color: #7D7B7B; text-align: center; height: 30px !important; text-decoration: none;" CssClass="tituloLabel editar" />
                                        </eo:ButtonColumn>
                                        <eo:StaticColumn HeaderText="Frase Acidentes" AllowSort="True" 
                                            DataField="FraseAcidentes" Name="FraseAcidentes" ReadOnly="True" Width="240">
                                            <CellStyle CssText="font-family: 'UniviaPro'; font-size: 12px; font-weight: normal; color: #7D7B7B; text-align: left; height: 30px !important; padding-left: .5rem " />
                                        </eo:StaticColumn>
                                        <eo:StaticColumn HeaderText="Data Reunião" AllowSort="True" 
                                            DataField="DataReuniao" Name="DataReuniao" ReadOnly="True" Width="120">
                                            <CellStyle CssText="font-family: 'UniviaPro'; font-size: 12px; font-weight: normal; color: #7D7B7B; text-align: left; height: 30px !important; " />
                                        </eo:StaticColumn>
                                        <eo:StaticColumn HeaderText="Observação" AllowSort="True" 
                                            DataField="Observacao" Name="Observacao" ReadOnly="True" Width="280">
                                            <CellStyle CssText="font-family: 'UniviaPro'; font-size: 12px; font-weight: normal; color: #7D7B7B; text-align: left; height: 30px !important; " />
                                        </eo:StaticColumn>
                                </Columns>
                                </eo:grid>
                            </div>          
						</div>
                        <div class="col-6 gx-3 gy-2">
                                    <asp:Button ID="btnNovo" runat="server" class="btn" Text="Nova Reunião Extraordinária" OnClick="btnNovo_Click" style="font-size: 14.5px !important; text-align:center; vertical-align:central;"/>
                            </div>
					</div>
			</div>
            </div>
            <eo:MsgBox ID="MsgBox1" runat="server" BackColor="#ffffff" ControlSkinID="None" HeaderHtml="Dialog Title" Height="192px" Width="345px" CssClass="card border border-1 p-2 text-center" IconUrl="Images/alerta_icon.svg">
            <HeaderStyleActive CssText="font-family: Ubuntu; font-size: 14px; font-weight: bold; color: #1C9489; text-align: center; padding: 5px;" />
            <ContentStyleActive CssText="font-family: Ubuntu; font-size: 14px; font-weight: bold; color: #7D7B7B; text-align: center; padding: 5px; width: 345px; height: 60px" />
        <FooterStyleActive CssText="width: 345px;" />
            </eo:MsgBox>
		</form>
	</body>
</HTML>
		


