<%@ Page Language="C#"   AutoEventWireup="True" CodeBehind="AdendoExame.aspx.cs"  Inherits="Ilitera.Net.PCMSO.AdendoExame" Title="Ilitera.Net" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Ilitera.NET</title>
		<meta content="True" name="vs_showGrid">
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script language="javascript" src="scripts/validador.js"></script>
		<LINK href="scripts/datagrid.css" type="text/css" rel="stylesheet">
		<LINK href="scripts/style.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0">
		<form method="post" runat="server">
			<TABLE cellSpacing="0" cellPadding="0" width="400" align="center" border="0">
				<TR>
					<TD vAlign="top" align="center" class="normalFont"><BR>
						<asp:label id="lblAdendo" runat="server" CssClass="largeboldFont"></asp:label><BR>
						<BR>
						<asp:ListBox id="lsbAdendos" runat="server" CssClass="inputBox" Width="380px" Rows="7" AutoPostBack="True" onselectedindexchanged="lsbAdendos_SelectedIndexChanged"></asp:ListBox>
						<BR>
						<TABLE class="normalFont" id="Table1" cellSpacing="0" cellPadding="0" width="382" align="center"
							border="0">
							<TR>
								<TD align="right">
									<asp:label id="lblTotRegistros" runat="server" CssClass="normalFont"></asp:label></TD>
							</TR>
						</TABLE>
						<BR>
						<TABLE class="normalFont" id="Table2" cellSpacing="0" cellPadding="0" width="400" align="center"
							border="0">
							<TR>
								<TD align="center" width="130">
									<asp:Label id="Label1" runat="server" CssClass="boldFont">Data</asp:Label><BR>
									<igtxt:WebTextEdit id="wteDataAdendo" runat="server" CssClass="inputBox" Width="110px" ReadOnly="True"
										ImageDirectory=" " JavaScriptFileName="ig_edit.js" JavaScriptFileNameCommon="ig_shared.js" HorizontalAlign="Center"
										BorderStyle="None" BorderWidth="0px" BackColor="LightYellow"></igtxt:WebTextEdit></TD>
								<TD align="center" width="270">
									<asp:Label id="Label2" runat="server" CssClass="boldFont">Médico</asp:Label><BR>
									<igtxt:WebTextEdit id="wteMedicoAdendo" runat="server" CssClass="inputBox" Width="250px" ReadOnly="True"
										ImageDirectory=" " JavaScriptFileName="ig_edit.js" JavaScriptFileNameCommon="ig_shared.js" HorizontalAlign="Center"
										BorderStyle="None" BorderWidth="0px" BackColor="LightYellow"></igtxt:WebTextEdit></TD>
							</TR>
						</TABLE>
						<BR>
						<asp:Label id="lblDescAden" runat="server" CssClass="boldFont">Descrição do Adendo</asp:Label><BR>
						<asp:TextBox id="txtDescricao" runat="server" CssClass="inputBox" Width="380px" TextMode="MultiLine"
							Rows="4"></asp:TextBox><BR>
						<BR>
						<asp:Button id="btnGravar" runat="server" CssClass="buttonBox" Width="140px" Text="Inserir Novo Adendo" onclick="btnGravar_Click"></asp:Button>
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
