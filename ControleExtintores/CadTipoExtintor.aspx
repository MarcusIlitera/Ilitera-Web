<%@ Page language="c#" Inherits="Ilitera.Net.ControleExtintores.CadTipoExtintor" Codebehind="CadTipoExtintor.aspx.cs" %>

<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>


<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Ilitera.Net</title>
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<LINK href="scripts/style.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0">
		<form method="post" runat="server">
			<TABLE class="normalFont" id="Table4" cellSpacing="0" cellPadding="0" width="300" align="center"
				border="0">
				<TR>
					<TD align="center"><BR>
						<asp:label id="lblTitulo" runat="server" CssClass="largeboldFont">Cadastro\Edi��o de Tipos de Extintores</asp:label><BR>
						<BR>
						<asp:listbox id="lsbTipoExtintor" runat="server" CssClass="inputBox" Width="280px" Rows="10"
							AutoPostBack="True" onselectedindexchanged="lsbTipoExtintor_SelectedIndexChanged"></asp:listbox><BR>
						<BR>
						<asp:label id="Label1" runat="server" CssClass="boldFont">Modelo do Extintor</asp:label><BR>
						<asp:textbox id="txtModeloExtintor" runat="server" CssClass="inputBox" Width="280px"></asp:textbox><BR>
						<BR>
						<asp:label id="Label6" runat="server" CssClass="boldFont"> Agente Extintor</asp:label><BR>
						<asp:textbox id="txtAgenteExtintor" runat="server" CssClass="inputBox" Width="280px"></asp:textbox><BR>
						<BR>
						<BR>
						<asp:button id="btnGravar" runat="server" CssClass="buttonBox" Width="60px" Text="Gravar" onclick="btnGravar_Click"></asp:button>&nbsp;
						<asp:button id="btnExcluir" runat="server" CssClass="buttonBox" Width="60px" Text="Excluir" onclick="btnExcluir_Click"></asp:button>&nbsp;
						<asp:button id="btnFechar" runat="server" CssClass="buttonBox" Width="60px" Text="Fechar"></asp:button></TD>
				</TR>
			</TABLE>

<eo:MsgBox ID="MsgBox1" runat="server" BackColor="#47729F" ControlSkinID="None" 
        HeaderHtml="Dialog Title" Height="100px" Width="168px">
        <HeaderStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #ffbf00 3px solid; padding-left: 4px; font-weight: bold; font-size: 11px; padding-bottom: 2px; color: white; padding-top: 2px; border-bottom: #22456a 1px solid; font-family: verdana" />
        <ContentStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #7d97b6 1px solid; padding-left: 4px; border-left-width: 1px; font-size: 11px; border-left-color: #728eb8; padding-bottom: 4px; color: white; padding-top: 4px; border-bottom: #22456a 1px solid; font-family: verdana" />
        <FooterStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #7d97b6 1px solid; padding-left: 4px; border-left-width: 1px; font-size: 11px; border-left-color: #728eb8; padding-bottom: 4px; color: white; padding-top: 4px; border-bottom: #22456a 1px solid; font-family: verdana" />
    </eo:MsgBox>



		</form>
	</body>
</HTML>
