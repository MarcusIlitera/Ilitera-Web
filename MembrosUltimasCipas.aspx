<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MembrosUltimasCipas.aspx.cs"  Title="Ilitera.Net" Inherits="Ilitera.Net.MembrosUltimasCipas"  EnableEventValidation="false" ValidateRequest="false" %>

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
		<script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js"></script>
		<link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.11.3/font/bootstrap-icons.min.css" />
		<link href="css/forms.css" rel="stylesheet" type="text/css" />
		<link href="css/tabelas.css" rel="stylesheet" type="text/css" />
		<LINK href="scripts/style.css" type="text/css" rel="stylesheet">
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

        .tabela{
            font-family: 'UniviaPro', sans-serif !important; 
            font-weight: 400 !important;
        }
    </style>
	<body bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0">
		<form id="frmMembrosCIPA" method="post" runat="server">
			<div class="container d-flex ms-5 ps-4 justify-content-center">
				<div class="row gx-3 gy-2 mt-3" style="width: 772px">
					<div class="col-12 subtituloBG text-center pt-2">
						<asp:label id="lblMembrosCIPA" runat="server" CssClass="subtitulo" style="margin-left: 0px !important;font-family: 'UniviaPro', sans-serif !important;font-size: 16px !important;  font-weight: 500 !important;">Membros das Últimas CIPAS</asp:label>
					</div>
					<!-- primeiro grid -->

					<div class="col-12 gx-2">
						<asp:label id="lblUltimaCipa" runat="server" CssClass="tituloLabel" style="margin-left: 0px !important; color: #1C9489; font-size: .8rem;">Ùltima CIPA: </asp:label>
					</div>

					<div class="col-12 gx-2 gy-2 mt-3 mb-3">
						<eo:Grid ID="grdUltimaCipa" runat="server" FixedColumnCount="1" ColumnHeaderDividerOffset="6" 
							ColumnHeaderAscImage="00050403" ColumnHeaderDescImage="00050404"
							GridLines="Both" PageSize="500" CssClass="grid"
							ColumnHeaderHeight="30" ItemHeight="30" Width="770px" Height="210px">
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
							<ColumnHeaderStyle CssClass="tabelaC colunas tabela"/>
							<Columns>
								<eo:StaticColumn HeaderText="Início Estabilidade" AllowSort="True" DataField="InicioEstabilidade" Name="InicioEstabilidade" ReadOnly="True" Width="170">
									<CellStyle CssText="font-family: 'UniviaPro'; font-size: 12px; font-weight: normal; color: #7D7B7B; text-align: left; height: 30px !important; " />
								</eo:StaticColumn>
								<eo:StaticColumn HeaderText="Cargo" AllowSort="True" DataField="Cargo" Name="Cargo" ReadOnly="True" Width="150">
									<CellStyle CssText="font-family: 'UniviaPro'; font-size: 12px; font-weight: normal; color: #7D7B7B; text-align: left; height: 30px !important; " />
								</eo:StaticColumn>
								<eo:StaticColumn HeaderText="Nome" AllowSort="True" DataField="Nome" Name="Nome" ReadOnly="True" Width="240">
									<CellStyle CssText="font-family: 'UniviaPro'; font-size: 12px; font-weight: normal; color: #7D7B7B; text-align: left; height: 30px !important; " />
								</eo:StaticColumn>
								<eo:StaticColumn HeaderText="Término Estabilidade" AllowSort="True" DataField="TerminoEstabilidade" Name="TerminoEstabilidade" ReadOnly="True" Width="190">
									<CellStyle CssText="font-family: 'UniviaPro'; font-size: 12px; font-weight: normal; color: #7D7B7B; text-align: left; height: 30px !important; " />
								</eo:StaticColumn>
							</Columns>
						</eo:Grid>
					</div>

					<!-- segundo grid -->
					<div class="col-12 gx-2">
						<asp:label id="lblPenultimaCipa" runat="server" CssClass="tituloLabel" style="margin-left: 0px !important; color: #1C9489; font-size: .8rem;">Penultima CIPA: </asp:label>
					</div>
					
					<div class="col-12 gx-2 gy-2 mt-3 mb-3">
						<eo:Grid ID="grdPenultimaCipa" runat="server" FixedColumnCount="1" ColumnHeaderDividerOffset="6" 
							ColumnHeaderAscImage="00050403" ColumnHeaderDescImage="00050404"
							GridLines="Both" PageSize="500" CssClass="grid"
							ColumnHeaderHeight="30" ItemHeight="30" Width="770px" Height="210px">
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
							<ColumnHeaderStyle CssClass="tabelaC colunas tabela"/>
							<Columns>
								<eo:StaticColumn HeaderText="Início Estabilidade" AllowSort="True" DataField="InicioEstabilidade" Name="InicioEstabilidade" ReadOnly="True" Width="170">
									<CellStyle CssText="font-family: 'UniviaPro'; font-size: 12px; font-weight: normal; color: #7D7B7B; text-align: left; height: 30px !important; " />
								</eo:StaticColumn>
								<eo:StaticColumn HeaderText="Cargo" AllowSort="True" DataField="Cargo" Name="Cargo" ReadOnly="True" Width="150">
									<CellStyle CssText="font-family: 'UniviaPro'; font-size: 12px; font-weight: normal; color: #7D7B7B; text-align: left; height: 30px !important; " />
								</eo:StaticColumn>
								<eo:StaticColumn HeaderText="Nome" AllowSort="True" DataField="Nome" Name="Nome" ReadOnly="True" Width="240">
									<CellStyle CssText="font-family: 'UniviaPro'; font-size: 12px; font-weight: normal; color: #7D7B7B; text-align: left; height: 30px !important; " />
								</eo:StaticColumn>
								<eo:StaticColumn HeaderText="Término Estabilidade" AllowSort="True" DataField="TerminoEstabilidade" Name="TerminoEstabilidade" ReadOnly="True" Width="190">
									<CellStyle CssText="font-family: 'UniviaPro'; font-size: 12px; font-weight: normal; color: #7D7B7B; text-align: left; height: 30px !important; " />
								</eo:StaticColumn>
							</Columns>
						</eo:Grid>
					</div>

                    <!-- terceiro grid -->
                    <div class="col-12 gx-2">
						<asp:label id="lblAntepenultimaCipa" runat="server" CssClass="tituloLabel" style="margin-left: 0px !important; color: #1C9489; font-size: .8rem;">Antepenultima CIPA: </asp:label>
					</div>
					
					<div class="col-12 gx-2 gy-2 mt-3 mb-3">
						<eo:Grid ID="grdAntepenultimaCipa" runat="server" FixedColumnCount="1" ColumnHeaderDividerOffset="6" 
							ColumnHeaderAscImage="00050403" ColumnHeaderDescImage="00050404"
							GridLines="Both" PageSize="500" CssClass="grid"
							ColumnHeaderHeight="30" ItemHeight="30" Width="770px" Height="210px">
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
							<ColumnHeaderStyle CssClass="tabelaC colunas tabela"/>
							<Columns>
								<eo:StaticColumn HeaderText="Início Estabilidade" AllowSort="True" DataField="InicioEstabilidade" Name="InicioEstabilidade" ReadOnly="True" Width="170">
									<CellStyle CssText="font-family: 'UniviaPro'; font-size: 12px; font-weight: normal; color: #7D7B7B; text-align: left; height: 30px !important; " />
								</eo:StaticColumn>
								<eo:StaticColumn HeaderText="Cargo" AllowSort="True" DataField="Cargo" Name="Cargo" ReadOnly="True" Width="150">
									<CellStyle CssText="font-family: 'UniviaPro'; font-size: 12px; font-weight: normal; color: #7D7B7B; text-align: left; height: 30px !important; " />
								</eo:StaticColumn>
								<eo:StaticColumn HeaderText="Nome" AllowSort="True" DataField="Nome" Name="Nome" ReadOnly="True" Width="240">
									<CellStyle CssText="font-family: 'UniviaPro'; font-size: 12px; font-weight: normal; color: #7D7B7B; text-align: left; height: 30px !important; " />
								</eo:StaticColumn>
								<eo:StaticColumn HeaderText="Término Estabilidade" AllowSort="True" DataField="TerminoEstabilidade" Name="TerminoEstabilidade" ReadOnly="True" Width="190">
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