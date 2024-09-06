<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MembrosCIPA.aspx.cs"  Title="Ilitera.Net" Inherits="Ilitera.Net.MembrosCIPA" EnableEventValidation="false" ValidateRequest="false" %>

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
        <link rel="stylesheet" href="css/css/UniviaPro-Regular.otf">
		<script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js"></script>		
	</HEAD>
    <style>
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
	<body bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0">
		<form id="frmMembrosCIPA" method="post" runat="server">
			<div class="container d-flex ms-5 ps-4 justify-content-center">
				<div class="row gx-3 gy-2 mt-3" style="width: 772px">
					<div class="col-12 subtituloBG text-center pt-2">
						<asp:label id="lblMembrosCIPA" runat="server" CssClass="subtitulo" style="margin-left: 0px !important;font-family: 'UniviaPro', sans-serif !important;font-size: 16px !important;  font-weight: 500 !important;">Membros da CIPA Gestão Atual</asp:label>
					</div>

					<div class="col-3 gx-2 gy-2">
						<div class="row">
							<div class="form-check ms-3">
								<asp:CheckBox runat="server" type="checkbox" value="" id="chkFiltro" CssClass="" AutoPostBack="True" Checked="False" OnCheckedChanged="chkFiltro_CheckedChanged" />
								<label class="texto form-check-sm-label mt-1" style="font-family: 'Ubuntu', sans-serif !important;color: #1C9489; font-size: .8rem;font-weight: 300 !important;" for="flexCheckDefault">Mostrar Empregados Inativos</label>
							</div>
						</div>
					</div>

					<!-- primeiro grid -->

					<div class="col-12 gx-2">
						<asp:label id="lblRepresentantesEmpregador" runat="server" CssClass="tituloLabel" style="margin-left: 0px !important; color: #1C9489; font-size: .8rem;">Grupo: Representantes do Empregador (Designados)</asp:label>
					</div>

					<div class="col-12 gx-2 gy-2 mt-3 mb-3">
						<eo:Grid ID="grd_Representantes_Empregador" runat="server" FixedColumnCount="1" ColumnHeaderDividerOffset="6" 
							ColumnHeaderAscImage="00050403" ColumnHeaderDescImage="00050404"
							GridLines="Both" PageSize="500" KeyField="IdEmprcImaegado"
							ColumnHeaderHeight="30" ItemHeight="30" Width="770px" Height="210px" ClientSideOnItemCommand="OnItemCommand" OnItemCommand="grd_Representantes_Empregador_ItemCommand" ClientSideOnCellSelected="OnCellSelected">
							<ItemStyles>
								<eo:GridItemStyleSet>
									<ItemStyle CssText="background-color: #FAFAFA; font-family: 'UniviaPro', sans-serif !important;" />
                                    <AlternatingItemStyle CssText="background-color: #F2F2F2; font-family: 'UniviaPro', sans-serif !important;" />
									<SelectedStyle CssText="border-bottom-style:solid;border-left-style:solid;border-right-style:solid;border-top-style:solid; font-family: 'UniviaPro', sans-serif !important;" />
									<CellStyle CssText="padding: 0.3rem 0.3rem 0.3rem 0.5rem; height: 30px !important; font-family: 'UniviaPro', sans-serif !important;" />
								</eo:GridItemStyleSet>
							</ItemStyles>
							<ColumnHeaderTextStyle CssText="font-family: 'UniviaPro', sans-serif !important;" />
							<ColumnHeaderStyle CssClass="tabelaC colunas" CssText="font-family: 'UniviaPro', sans-serif !important; font-weight: 400 !important;"/>
							<Columns>
                                <eo:ButtonColumn Name="Editar" DataField="Editar" Width="100" ButtonType="LinkButton">
                                    <CellStyle CssText="font-family: 'UniviaPro'; font-size: 12px; font-weight: normal; text-align: left; height: 30px !important; text-align: center;" CssClass="tituloLabel editar" />
                                </eo:ButtonColumn>
								<eo:StaticColumn HeaderText="Cargo" AllowSort="True" DataField="Cargo" Name="Cargo" ReadOnly="True" Width="120">
									<CellStyle CssText="font-family: 'UniviaPro'; font-size: 12px; font-weight: normal; color: #7D7B7B; text-align: left; height: 30px !important; " />
								</eo:StaticColumn>
								<eo:StaticColumn HeaderText="Nome" AllowSort="True" DataField="Nome" Name="Nome" ReadOnly="True" Width="280">
									<CellStyle CssText="font-family: 'UniviaPro'; font-size: 12px; font-weight: normal; color: #7D7B7B; text-align: left; height: 30px !important; " />
								</eo:StaticColumn>
								<eo:StaticColumn HeaderText="Status" AllowSort="True" DataField="Status" Name="Status" ReadOnly="True" Width="150">
									<CellStyle CssText="font-family: 'UniviaPro'; font-size: 12px; font-weight: normal; color: #7D7B7B; text-align: left; height: 30px !important; " />
								</eo:StaticColumn>
								<eo:StaticColumn HeaderText="Estabilidade" AllowSort="True" DataField="Estabilidade" Name="Estabilidade" ReadOnly="True" Width="120">
									<CellStyle CssText="font-family: 'UniviaPro'; font-size: 12px; font-weight: normal; color: #7D7B7B; text-align: left; height: 30px !important; " />
								</eo:StaticColumn>
							</Columns>
						</eo:Grid>
					</div>

					<!-- segundo grid -->
					<div class="col-12 gx-2">
						<asp:label id="lblRepresentantesEmpregados" runat="server" CssClass="tituloLabel" style="margin-left: 0px !important; color: #1C9489; font-size: .825rem;">Grupo: Representantes dos Empregados (Eleitos)</asp:label>
					</div>
					
					<div class="col-12 gx-2 gy-2 mt-3 mb-3">
						<eo:Grid ID="grd_Representantes_Empregados" runat="server" FixedColumnCount="1" ColumnHeaderDividerOffset="6" 
							ColumnHeaderAscImage="00050403" ColumnHeaderDescImage="00050404"
							GridLines="Both" PageSize="500" KeyField="IdEmprcImaegado" CssClass="grid"
							ColumnHeaderHeight="30" ItemHeight="30" Width="770px" Height="210px" ClientSideOnItemCommand="OnItemCommand" ClientSideOnCellSelected="OnCellSelected" OnItemCommand="grd_Representantes_Empregados_ItemCommand">
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
							<ColumnHeaderTextStyle CssText="" />
							<ColumnHeaderStyle CssClass="tabelaC colunas" CssText="font-family: 'UniviaPro', sans-serif !important; font-weight: 400 !important;"/>
							<Columns>
                                <eo:ButtonColumn Name="Editar" DataField="Editar" Width="100" >
                                    <CellStyle CssText="font-family: 'UniviaPro'; font-size: 12px; font-weight: normal; color: #7D7B7B; text-align: left; height: 30px !important; text-align: center" CssClass="tituloLabel editar" />
                                </eo:ButtonColumn>
								<eo:StaticColumn HeaderText="Cargo" AllowSort="True" DataField="Cargo" Name="Cargo" ReadOnly="True" Width="120">
									<CellStyle CssText="font-family: 'UniviaPro'; font-size: 12px; font-weight: normal; color: #7D7B7B; text-align: left; height: 30px !important; " />
								</eo:StaticColumn>
								<eo:StaticColumn HeaderText="Nome" AllowSort="True" DataField="Nome" Name="Nome" ReadOnly="True" Width="280">
									<CellStyle CssText="font-family: 'UniviaPro'; font-size: 12px; font-weight: normal; color: #7D7B7B; text-align: left; height: 30px !important; " />
								</eo:StaticColumn>
								<eo:StaticColumn HeaderText="Status" AllowSort="True" DataField="Status" Name="Status" ReadOnly="True" Width="150">
									<CellStyle CssText="font-family: 'UniviaPro'; font-size: 12px; font-weight: normal; color: #7D7B7B; text-align: left; height: 30px !important; " />
								</eo:StaticColumn>
								<eo:StaticColumn HeaderText="Estabilidade" AllowSort="True" DataField="Estabilidade" Name="Estabilidade" ReadOnly="True" Width="120">
									<CellStyle CssText="font-family: 'UniviaPro'; font-size: 12px; font-weight: normal; color: #7D7B7B; text-align: left; height: 30px !important; " />
								</eo:StaticColumn>
							</Columns>
						</eo:Grid>
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
