<%@ Page Language="C#"   AutoEventWireup="True"  MasterPageFile="~/Site.Master" CodeBehind="LoginRestrito.aspx.cs"  Inherits="Ilitera.Net.LoginRestrito" Title="Ilitera.Net" %>

<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>


<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
  
</asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent" >

<%--<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Mestra.NET</title>
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="scripts/style.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="scripts/validador.js"></script>
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" onload="document.forms[0].txtSenha.focus()"
		rightMargin="0">
		<form method="post" runat="server" id="frmLoginRestrito">
--%>			<TABLE class="normalFont" id="Table1" cellSpacing="0" cellPadding="0" width="620" align="center"
				border="0">
				<TR>
					<TD align="center"><BR>
						<BR>
						<BR>
						<STRONG>ACESSO MEDIANTE SENHA<BR>
							<BR>
							<asp:textbox id="txtSenha" onkeydown="ProcessaEnter(event, 'btnEntrar')" runat="server" Width="150px" TextMode="Password"
								CssClass="inputBox"></asp:textbox><BR>
							<BR>
							<asp:button id="btnEntrar" runat="server" Width="60px" CssClass="buttonBox" Text="Entrar" onclick="btnLogin_Click"></asp:button><BR>
							<BR>
							<asp:requiredfieldvalidator class="sfont" id="ValidtxtSenha" runat="server" CssClass="errorFont" ControlToValidate="txtSenha"
								ErrorMessage="Digite sua Senha" Display="Dynamic">Digite sua Senha</asp:requiredfieldvalidator><asp:label id="lblError" runat="server" CssClass="errorFont"></asp:label></STRONG></TD>
				</TR>
			</TABLE>
<%--		</form>
	</body>
</HTML>
--%>

</asp:Content>