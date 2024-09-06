<%@ Page language="c#" Inherits="Ilitera.Net.ControleExtintores.CadastroExtintores" Codebehind="CadastroExtintores.aspx.cs" %>

<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Ilitera.NET</title>
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<LINK href="scripts/style.css" type="text/css" rel="stylesheet">
		<script language="JavaScript" src="scripts/validador.js"></script>

      

	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0">
		<form method="post" runat="server" id="frmCadastroExtintores">



			<TABLE class="normalFont" id="Table4" cellSpacing="0" cellPadding="0" width="400" align="center"
				border="0">


				<TR>
					<TD align="center"><BR>
						<asp:label id="lblTitulo" runat="server" CssClass="largeboldFont"></asp:label><BR>
						<BR>
						<TABLE class="normalFont" id="Table5" cellSpacing="0" cellPadding="0" width="400" align="center"
							border="0">
							<TR>
								<TD align="center" width="130"><asp:label id="Label1" runat="server" CssClass="boldFont">Ativo Fixo</asp:label><BR>
									<asp:textbox id="txtAtivoFixo" runat="server" CssClass="inputBox" Width="110px"></asp:textbox></TD>
								<TD align="center" width="140"><asp:label id="Label6" runat="server" CssClass="boldFont">Data de Fabricação</asp:label><BR>
									<asp:textbox id="wdeDataFabricacao" runat="server" CssClass="inputBox" Width="120px" EditModeFormat="dd/MM/yyyy"
										DisplayModeFormat="dd/MM/yyyy" HorizontalAlign="Center"
										ImageDirectory=" " MinValue="1800-01-01"></asp:textbox></TD>
								<TD align="center" width="130"><asp:label id="Label7" runat="server" CssClass="boldFont">Garantia (Anos)</asp:label><BR>
									<asp:textbox id="wneGarantia" runat="server" CssClass="inputBox" Width="110px" HorizontalAlign="Left" ImageDirectory=" " DataMode="Int"></asp:textbox></TD>
							</TR>
						</TABLE>
						<BR>
						<TABLE class="normalFont" id="Table7" cellSpacing="0" cellPadding="0" width="400" align="center"
							border="0">
							<TR>
								<TD align="center" width="120"><asp:label id="Label10" runat="server" CssClass="boldFont">Peso Vazio (Kg)</asp:label><BR>
									<asp:textbox id="wnePesoVazio" runat="server" CssClass="inputBox" Width="100px" HorizontalAlign="Left" ImageDirectory=" " DataMode="Float"
										Nullable="False"></asp:textbox></TD>
								<TD align="center" width="120"><asp:label id="Label11" runat="server" CssClass="boldFont">Peso Cheio (Kg)</asp:label><BR>
									<asp:textbox id="wnePesoCheio" runat="server" CssClass="inputBox" Width="100px" HorizontalAlign="Left" ImageDirectory=" " DataMode="Float"
										Nullable="False"></asp:textbox></TD>
								<TD align="center" width="160"><asp:label id="Label12" runat="server" CssClass="boldFont">Fabricante</asp:label><BR>
									<asp:dropdownlist id="ddlFabricante" runat="server" CssClass="inputBox" Width="140px" AutoPostBack="True" onselectedindexchanged="ddlFabricante_SelectedIndexChanged"></asp:dropdownlist></TD>
							</TR>
						</TABLE>
						<BR>
						<TABLE class="normalFont" id="Table6" cellSpacing="0" cellPadding="0" width="400" align="center"
							border="0">
							<TR>
								<TD align="center" width="200"><asp:label id="Label8" runat="server" CssClass="boldFont">Tipo do Equipamento</asp:label><BR>
									<asp:dropdownlist id="ddlTipoExtintor" runat="server" CssClass="inputBox" Width="180px" AutoPostBack="True" onselectedindexchanged="ddlTipoExtintor_SelectedIndexChanged"></asp:dropdownlist></TD>
								<TD align="center" width="200"><asp:label id="Label9" runat="server" CssClass="boldFont">Setor</asp:label><BR>
									<asp:dropdownlist id="ddlSetor" runat="server" CssClass="inputBox" Width="180px"></asp:dropdownlist></TD>
							</TR>
						</TABLE>
						<BR>
						<TABLE class="normalFont" id="Table8" cellSpacing="0" cellPadding="0" width="400" align="center"
							border="0">
							<TR>
								<TD align="center" width="200"><asp:label id="Label14" runat="server" CssClass="boldFont">Localização</asp:label><BR>
									<asp:textbox id="txtLocalizacao" runat="server" CssClass="inputBox" Width="180px" TextMode="MultiLine"
										Rows="2"></asp:textbox></TD>
								<TD align="center" width="200"><asp:label id="Label13" runat="server" CssClass="boldFont">Observações</asp:label><BR>
									<asp:textbox id="txtObservacao" runat="server" CssClass="inputBox" Width="180px" TextMode="MultiLine"
										Rows="2"></asp:textbox></TD>
							</TR>
						</TABLE>
						<BR>
						<TABLE class="normalFont" id="Table5" cellSpacing="0" cellPadding="0" width="400" align="center"
							border="0">
							<TR>
								<TD align="center" width="340">
                                    <asp:label id="Label3" runat="server" CssClass="boldFont">Data de Vencimento da Recarga</asp:label><BR>
									<asp:textbox id="txtVctoRecarga" runat="server" CssClass="inputBox" Width="120px" EditModeFormat="dd/MM/yyyy"
										DisplayModeFormat="dd/MM/yyyy" HorizontalAlign="Center"
										ImageDirectory=" " MinValue="1800-01-01"></asp:textbox></TD>
							</TR>
						</TABLE>
						<BR>

						<asp:label id="Label15" runat="server" CssClass="boldFont">Condição do Extintor</asp:label><BR>
						<asp:radiobuttonlist id="rblCondicao" runat="server" CssClass="normalFont" Width="250px" RepeatDirection="Horizontal"
							CellSpacing="0" CellPadding="0">
							<asp:ListItem Value="1" Selected="True">Alocado</asp:ListItem>
							<asp:ListItem Value="2">Fora de Uso</asp:ListItem>
							<asp:ListItem Value="3">Reserva</asp:ListItem>
						</asp:radiobuttonlist><BR>
						<BR>
						<asp:button id="btnGravar" runat="server" CssClass="buttonBox" Width="60px" Text="Gravar" onclick="btnGravar_Click"></asp:button>&nbsp;
						<asp:button id="btnExcluir" runat="server" CssClass="buttonBox" Width="60px" Text="Excluir" onclick="btnExcluir_Click"></asp:button>&nbsp;
						<asp:button id="btnFechar" runat="server" CssClass="buttonBox" Width="60px" Text="Fechar"></asp:button></TD>
				</TR>

			</TABLE>

               <input id="txtCloseWindow" type="hidden"   runat="server"/>

<eo:MsgBox ID="MsgBox1" runat="server" BackColor="#47729F" ControlSkinID="None" 
        HeaderHtml="Dialog Title" Height="100px" Width="168px">
        <HeaderStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #ffbf00 3px solid; padding-left: 4px; font-weight: bold; font-size: 11px; padding-bottom: 2px; color: white; padding-top: 2px; border-bottom: #22456a 1px solid; font-family: verdana" />
        <ContentStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #7d97b6 1px solid; padding-left: 4px; border-left-width: 1px; font-size: 11px; border-left-color: #728eb8; padding-bottom: 4px; color: white; padding-top: 4px; border-bottom: #22456a 1px solid; font-family: verdana" />
        <FooterStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #7d97b6 1px solid; padding-left: 4px; border-left-width: 1px; font-size: 11px; border-left-color: #728eb8; padding-bottom: 4px; color: white; padding-top: 4px; border-bottom: #22456a 1px solid; font-family: verdana" />
    </eo:MsgBox>


                           
			<INPUT id="txtAuxiliar" type="hidden" name="txtAuxiliar" runat="server">
		</form>
	</body>
</HTML>
