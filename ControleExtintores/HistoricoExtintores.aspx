<%@ Page language="c#" Inherits="Ilitera.Net.ControleExtintores.HistoricoExtintores" Codebehind="HistoricoExtintores.aspx.cs" %>

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
		<form method="post" runat="server" id="frmHistoricoExtintores">
			<TABLE class="normalFont" id="Table4" cellSpacing="0" cellPadding="0" width="450" align="center"
				border="0">
				<TR>
					<TD align="center"><BR>
						<asp:label id="lblTitulo" runat="server" CssClass="largeboldFont"></asp:label><BR>
						<BR>
						<asp:listbox id="lsbHistoricoExtintor" runat="server" CssClass="inputBox" Rows="10" Width="430px"
							AutoPostBack="True" onselectedindexchanged="lsbHistoricoExtintor_SelectedIndexChanged"></asp:listbox><BR>
						<BR>
						<TABLE class="normalFont" id="Table5" cellSpacing="0" cellPadding="0" width="450" align="center"
							border="0">
							<TR>
								<TD align="center" width="100"><asp:label id="Label7" runat="server" CssClass="boldFont">Data</asp:label><BR>
									<asp:textbox id="wdeData" runat="server" CssClass="inputBox" Width="80px" MinValue="1800-01-01"
										DisplayModeFormat="dd/MM/yyyy" EditModeFormat="dd/MM/yyyy" ImageDirectory=" " HorizontalAlign="Center" Nullable="False"></asp:textbox></TD>
								<TD align="center" width="145"><asp:label id="Label8" runat="server" CssClass="boldFont">Tipo do Histórico</asp:label><BR>
									<asp:dropdownlist id="ddlTipoHistorico" runat="server" CssClass="inputBox" Width="125px" AutoPostBack="True" onselectedindexchanged="ddlTipoHistorico_SelectedIndexChanged">
										<asp:ListItem Value="1">Recebimento</asp:ListItem>
										<asp:ListItem Value="2">Inspe&#231;&#227;o</asp:ListItem>
										<asp:ListItem Value="3">Repara&#231;&#227;o</asp:ListItem>
										<asp:ListItem Value="4">Uso em Instru&#231;&#227;o</asp:ListItem>
										<asp:ListItem Value="5">Uso em Inc&#234;ndio</asp:ListItem>
									</asp:dropdownlist></TD>
								<TD align="center" width="205"><asp:label id="lblReparacao" runat="server" CssClass="boldFont">Reparação</asp:label><BR>
									<asp:dropdownlist id="ddlReparacao" runat="server" CssClass="inputBox" Width="185px">
										<asp:ListItem Value="1">Substitui&#231;&#227;o do Gatilho</asp:ListItem>
										<asp:ListItem Value="2">Substitui&#231;&#227;o do Difusor</asp:ListItem>
										<asp:ListItem Value="3">Mangote</asp:ListItem>
										<asp:ListItem Value="4">V&#225;lvula de Seguran&#231;a</asp:ListItem>
										<asp:ListItem Value="5">V&#225;lvula Completa</asp:ListItem>
										<asp:ListItem Value="6">V&#225;lvula de Cilindro Adicional</asp:ListItem>
										<asp:ListItem Value="7">Pintura</asp:ListItem>
										<asp:ListItem Value="8">Man&#244;metro</asp:ListItem>
										<asp:ListItem Value="9">Teste Hidrost&#225;tico</asp:ListItem>
										<asp:ListItem Value="10">Recarregado</asp:ListItem>
										<asp:ListItem Value="11">Diversos</asp:ListItem>
									</asp:dropdownlist></TD>
							</TR>
						</TABLE>
						<BR>
						<TABLE class="normalFont" id="Table6" cellSpacing="0" cellPadding="0" width="450" align="center"
							border="0">
							<TR>
								<TD align="center" width="80"><asp:label id="Label10" runat="server" CssClass="boldFont">Peso Atual</asp:label><BR>
									<asp:textbox id="wnePesoAtual" runat="server" CssClass="inputBox" Width="60px" ImageDirectory=" " HorizontalAlign="Center" Nullable="False" DataMode="Float"></asp:textbox></TD>
								<TD align="center" width="270"><asp:label id="Label11" runat="server" CssClass="boldFont"> Responsável</asp:label><BR>
									<asp:dropdownlist id="ddlResponsavel" runat="server" CssClass="inputBox" Width="250px" AutoPostBack="True" onselectedindexchanged="ddlResponsavel_SelectedIndexChanged"></asp:dropdownlist></TD>
								<TD align="center" width="100">
									<asp:label id="lblVencimento" runat="server" CssClass="boldFont">Vencimento</asp:label><BR>
									<asp:textbox id="wdtVencimento" runat="server" CssClass="inputBox" Width="80px" HorizontalAlign="Center"
										ImageDirectory=" " EditModeFormat="dd/MM/yyyy"
										DisplayModeFormat="dd/MM/yyyy" MinValue="1800-01-01"></asp:textbox></TD>
							</TR>
						</TABLE>
						<BR>
						<asp:label id="Label12" runat="server" CssClass="boldFont">Descrição</asp:label><BR>
						<asp:textbox id="txtDescricao" runat="server" CssClass="inputBox" Rows="2" Width="430px" TextMode="MultiLine"></asp:textbox><BR>
						<BR>
						<BR>
						<asp:button id="btnGravar" runat="server" CssClass="buttonBox" Width="60px" Text="Gravar" onclick="btnGravar_Click"></asp:button>&nbsp;
						<asp:button id="btnExcluir" runat="server" CssClass="buttonBox" Width="60px" Text="Excluir" onclick="btnExcluir_Click"></asp:button>&nbsp;
						<asp:button id="btnFechar" runat="server" CssClass="buttonBox" Width="60px" Text="Fechar"></asp:button></TD>
				</TR>
			</TABLE>
			<INPUT id="txtAuxiliar" type="hidden" name="txtAuxiliar" runat="server">

<eo:MsgBox ID="MsgBox1" runat="server" BackColor="#47729F" ControlSkinID="None" 
        HeaderHtml="Dialog Title" Height="100px" Width="168px">
        <HeaderStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #ffbf00 3px solid; padding-left: 4px; font-weight: bold; font-size: 11px; padding-bottom: 2px; color: white; padding-top: 2px; border-bottom: #22456a 1px solid; font-family: verdana" />
        <ContentStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #7d97b6 1px solid; padding-left: 4px; border-left-width: 1px; font-size: 11px; border-left-color: #728eb8; padding-bottom: 4px; color: white; padding-top: 4px; border-bottom: #22456a 1px solid; font-family: verdana" />
        <FooterStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #7d97b6 1px solid; padding-left: 4px; border-left-width: 1px; font-size: 11px; border-left-color: #728eb8; padding-bottom: 4px; color: white; padding-top: 4px; border-bottom: #22456a 1px solid; font-family: verdana" />
    </eo:MsgBox>

		</form>
	</body>
</HTML>
