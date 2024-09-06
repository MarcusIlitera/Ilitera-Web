<%@ Page Language="C#"  AutoEventWireup="True"  MasterPageFile="~/Site.Master" Inherits="Ilitera.Net.OrdemDeServico.CadEmpregadoConjuntoProcedimento" Codebehind="CadEmpregadoConjuntoProcedimento.aspx.cs"  Title="Ilitera.Net" %>

<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <link href="css/forms.css" rel="stylesheet" type="text/css" />
    <link href="css/tabelas.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
		a{
			text-decoration: none !important;
		}

		.filter-cor{
			filter: invert(58%) sepia(4%) saturate(34%) hue-rotate(202deg) brightness(92%) contrast(90%);
		}

        .inputBox
        {}
        .style1
        {
            width: 192px;
        }
    </style>

</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div class="container-fluid d-flex ms-5 ps-4">
        <div class="row gx-3 gy-2">

		<script language="javascript" type="text/javascript">
</script>
			
<%--<HTML>
	<HEAD>
		<title>Ilitera.NET</TITLE>
		<meta content="False" name="vs_snapToGrid">
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="scripts/style.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="scripts/validador.js"></script>
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0">
		<form method="post" runat="server">
--%>			
	<%--		<TABLE class="normalFont" id="Table1" cellSpacing="0" cellPadding="0" width="620" align="center"
				border="0">
				<TR>
					<TD align="center">--%>

			<div class="col-11 subtituloBG" style="padding-top: 10px;">
                <asp:label id="Label1" runat="server" CssClass="subtitulo">Configuração da Ordem de Serviço para os Empregados</asp:label>
			</div>
			<div class="col-11 mb-3">
				<div class="row">
					<div class="col-12 gx-3 gy-2">
						<div class="row">

							<div class="col-md-3 gx-3 gy-2">
								<asp:Label ID="Label8" runat="server" CssClass="tituloLabel form-label">Filtrar por GHE</asp:Label>
								<asp:DropDownList ID="cmb_GHE" runat="server" CssClass="texto form-select" CausesValidation="True" AutoPostBack="true" onselectedindexchanged="cmb_GHE_SelectedIndexChanged"></asp:DropDownList>
							</div>

							<div class="col-md-3 gx-3 gy-2">
                                <asp:label id="Label9" runat="server" CssClass="tituloLabel form-label">Filtrar por Setor</asp:label>
                                <asp:DropDownList ID="cmb_Setor" runat="server" AutoPostBack="True" CausesValidation="True" CssClass="texto form-select" onselectedindexchanged="cmb_Setor_SelectedIndexChanged">
                                </asp:DropDownList>
							</div>

							<div class="col-md-5 gx-3 gy-2">
								<asp:label id="Label2" runat="server" CssClass="tituloLabel form-label">Busca de Conjuntos ou Procedimentos</asp:label>
								<asp:textbox id="txtProcedimento" runat="server" CssClass="texto form-control form-control-sm" onkeydown="ProcessaEnter(event, 'imgBusca')"></asp:textbox>
							</div>

							<div class="col-md-1 gx-3 gy-2">
								<asp:imagebutton id="imgBusca" runat="server" ToolTip="Busca de Conjuntos ou Procedimentos" ImageUrl="images/search.svg" CssClass="btnMenor  mt-3" style="padding: .5rem;"></asp:imagebutton>
							</div>
						</div>
					</div>
                    <div class="col-12 gx-3 gy-2">
						<div class="row">
							<div class="col-md-6 gx-3 gy-2">
									<asp:label id="Label4" runat="server" CssClass="tituloLabel form-label">Empregados</asp:label>
									<asp:listbox id="lsbEmpregados" runat="server" CssClass="texto form-control" Rows="14" SelectionMode="Multiple"
										AutoPostBack="True" style="height:431px" onselectedindexchanged="lsbEmpregados_SelectedIndexChanged">
									</asp:listbox>
							</div>
							<div class="col-md-6 gx-3 gy-2">
								<div class="row">
									<div class="col-8 mb-2 gy-1">
										<asp:dropdownlist id="ddlTipoProc" runat="server" CssClass="texto form-select mt-3" AutoPostBack="True" onselectedindexchanged="ddlTipoProc_SelectedIndexChanged">
											<asp:ListItem Value="0">Conjunto de Procedimentos</asp:ListItem>
											<asp:ListItem Value="1">Procedimentos</asp:ListItem>
											<asp:ListItem Value="2">Procedimentos e Conjuntos</asp:ListItem>
										</asp:dropdownlist>
									</div>

									<div class="col-2 gx-2 mb-2" style="margin-top: 11px;">
										<asp:linkbutton id="lkbListaTodos" runat="server"  CssClass="btn mt-2" onclick="lkbListaTodos_Click">Listar Todos</asp:linkbutton>
									</div>

									<div class="col-12 mb-2">
										<asp:listbox id="listBxConjuntoProc" runat="server" CssClass="texto form-select mt-2" Rows="8" SelectionMode="Multiple" style="height: 200px;">
										</asp:listbox>
									</div>

									<div class="col-12 mb-2 mt-2">
										<div class="row justify-content-center">
											<div class="col-1 gx-1">
												<asp:imagebutton id="imbDown" runat="server" ToolTip="Adicionar Selecionados" ImageUrl="Images/descer.svg" Visible="True"></asp:imagebutton>
											</div>

											<div class="col-1 gx-1">
												<asp:imagebutton id="imbAllDown" runat="server" ToolTip="Adicionar Todos" ImageUrl="Images/descer.svg" Visible="True"></asp:imagebutton>
											</div>

											<div class="col-1 gx-1">
												<asp:imagebutton id="imbUp" runat="server" ToolTip="Remover Selecionados" ImageUrl="Images/subir.svg" Visible="True"></asp:imagebutton>
											</div>

											<div class="col-1 gx-1">
												<asp:imagebutton id="imbAllUp" runat="server" ToolTip="Remover Todos" ImageUrl="Images/subir.svg" Visible="True"></asp:imagebutton>
											</div>
										</div>
									</div>

									<div class="col-12">
										<asp:label id="Label3" runat="server" CssClass="tituloLabel form-label"> Itens selecionados para o(s) Empregado(s)</asp:label>
										<asp:listbox id="listBxConjuntoEmpregado" runat="server" CssClass="texto form-select" Rows="8" AutoPostBack="True" onselectedindexchanged="listBxConjuntoEmpregado_SelectedIndexChanged"></asp:listbox>
									</div>
								</div>
								
								
							</div>
						</div>
					</div>   
			</div>
		</div>
		<div class="col-11 subtituloBG mt-4" style="padding-top: 10px;">
			<asp:label id="Label6" runat="server" CssClass="subtitulo">Ordem de Serviço</asp:label>
		</div>
		<div class="col-11 mb-3">
				<div class="row">
					<div class="col-12 gx-3 gy-2">
						<div class="row gx-3 gy-2">
							
							<div class="col-md-3 gx-3 gy-2 mb-2">
									<asp:Calendar ID="cld_OS" runat="server" CellPadding="4" DayNameFormat="Shortest" 
                                        Height="155px" Width="174px" CssClass="mt-4 calendar" BorderColor="Transparent" BorderWidth="0">
                                        <DayHeaderStyle BackColor="#B0ABAB" Font-Bold="True" CssClass="calendarTitulo" />
                                        <NextPrevStyle VerticalAlign="Bottom" />
                                        <OtherMonthDayStyle ForeColor="#89898B" />
                                        <SelectedDayStyle BackColor="#666666" Font-Bold="True" ForeColor="White" />
                                        <SelectorStyle BackColor="#CCCCCC" />
                                        <TitleStyle BackColor="#D6D6D6" CssClass="calendarTitulo"  />
                                        <TodayDayStyle BackColor="#CCCCCC" ForeColor="Black" />
                                        <WeekendDayStyle />
                                    </asp:Calendar>
								<div class="mt-3 ms-3 mb-2">
                                    <asp:CheckBox ID="chk_Analise_Risco" runat="server" AutoPostBack="True" margin-top="10px" CssClass="texto form-check-label bg-transparent border-0" Text="Cabeçalho como Análise de Risco" />
								</div>
							</div>

							<div class="col-md-2 ps-4 gx-3 gy-2">
									<asp:label id="lblDataInicio" runat="server" CssClass="tituloLabel form-label">Data de Início</asp:label>
									<asp:textbox id="wdtDataInicio" runat="server" CssClass="texto form-control form-control-sm" Width="120px" EditModeFormat="dd/MM/yyyy"
										ImageDirectory=" " HorizontalAlign="Center"
										ToolTip="Data de Início" ReadOnly="True" Enabled="False">
									</asp:textbox>
							</div>
							<div class="col-md-2 gx-3 gy-2">
									<asp:label id="lblDataTermino" runat="server" CssClass="tituloLabel form-label">Data de Término</asp:label>
									<asp:textbox id="wdtDataTermino" runat="server" CssClass="texto form-control form-control-sm" Width="120px" 
										EditModeFormat="dd/MM/yyyy"
										ImageDirectory=" " HorizontalAlign="Center"
										ToolTip="Data de Início" ReadOnly="True" Enabled="False">
									</asp:textbox>
							</div>
						</div>
					</div>
				</div>
				<div class="col-12 gx-4 gy-2">
					<asp:button id="btnGravar" runat="server" CssClass="btn"  Enabled="False" Text="Gravar" onclick="btnGravar_Click"></asp:button>
				</div>
				<div class="row mt-3 gx-3 gy-2">
					<asp:linkbutton id="lkbOrdemServico" runat="server" CssClass="boldFont" onclick="lkbOrdemServico_Click">
						<img src="Images/printer.svg" class="filter-cor" border="0"> Imprimir OS Individual Horizontal
					</asp:linkbutton>
					<asp:linkbutton id="lkbOrdemServicoV" runat="server" CssClass="boldFont" onclick="lkbOrdemServicoV_Click">
						<img src="Images/printer.svg" class="filter-cor" border="0"> Imprimir OS Individual Vertical
					</asp:linkbutton>
                    <asp:linkbutton id="lkbOrdemServico_Grupo" runat="server" CssClass="boldFont" onclick="lkbOrdemServico_Grupo_Click">
						<img src="Images/printer.svg" class="filter-cor" border="0"> Imprimir OS Selecionados Horizontal
                    </asp:linkbutton>
                    <asp:linkbutton id="lkbOrdemServico_GrupoV" runat="server" CssClass="boldFont" onclick="lkbOrdemServico_GrupoV_Click">
						<img src="Images/printer.svg" class="filter-cor" border="0"> Imprimir OS Selecionados Vertical
                    </asp:linkbutton>					
				</div>

			</div>
							
						

							<asp:label id="lblTipoRelatorio" runat="server" CssClass="boldFont" Visible="False">Selecione o Tipo a ser impresso</asp:label>
							<asp:dropdownlist id="ddlTipo" runat="server" CssClass="inputBox" Width="237px" 
                                    Height="23px" Font-Size="X-Small" Visible="False">
								<asp:ListItem Value="0">Completo</asp:ListItem>
								<asp:ListItem Value="1">Apenas Introdução</asp:ListItem>
								<asp:ListItem Value="4">Apenas Listagem</asp:ListItem>
								<asp:ListItem Value="2">Apenas Instrutivos</asp:ListItem>
								<asp:ListItem Value="3">Apenas Específicos</asp:ListItem>
							</asp:dropdownlist>

                            <asp:Label ID="Label10" runat="server" CssClass="boldFont" Visible="False">Data OS</asp:Label>
												
                            <asp:ListBox ID="lst_Id_GHE" runat="server" Height="32px" Visible="False" 
                                Width="51px"></asp:ListBox>
                            <asp:ListBox ID="lst_Id_Setor" runat="server" Height="25px" Visible="False" 
                                Width="52px"></asp:ListBox>

							<asp:image id="Image1" runat="server" ImageUrl="img/5pixel.gif" Visible="False"></asp:image><BR>
							<asp:image id="Image2" runat="server" ImageUrl="img/5pixel.gif" Visible="False"></asp:image><BR>
							
							<asp:image id="Image3" runat="server" ImageUrl="img/5pixel.gif" Visible="False"></asp:image><BR>
							<asp:image id="Image4" runat="server" ImageUrl="img/5pixel.gif" Visible="False"></asp:image><br>

                            <asp:Label ID="lbl_Id_Empresa" runat="server" Text="IdEmpresa" Visible="False"></asp:Label>
                            <asp:Label ID="lbl_Id_Usuario" runat="server" Text="IdUsuario" Visible="False"></asp:Label>

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
		</div>
	</div>

    </td>
    </tr>
    </table>

</asp:Content>