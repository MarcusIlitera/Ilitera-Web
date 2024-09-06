
<%@ Page Language="C#"   AutoEventWireup="True" CodeBehind="ViewAmbulatorial.aspx.cs"  Inherits="Ilitera.Net.PCMSO.ViewAmbulatorial" Title="Ilitera.Net" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Mestra.NET</title>
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
			<TABLE cellSpacing="0" cellPadding="0" width="370" align="center" border="0">
				<TR>
					<TD vAlign="top" align="center" class="normalFont"><BR>
						<asp:label id="lblAdendo" runat="server" CssClass="largeboldFont"> Anotações do Exame Ambulatorial</asp:label><BR>
						<BR>
						<asp:TextBox id="txtAnotacoes" runat="server" CssClass="inputBox" Width="350px" Rows="12" TextMode="MultiLine"
							ReadOnly="True"></asp:TextBox>
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
