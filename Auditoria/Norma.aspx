<%@ Page Language="C#"   AutoEventWireup="True"   CodeBehind="Norma.aspx.cs"  Inherits="Ilitera.Net.Norma2" Title="Ilitera.Net" %>

<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>



<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Ilitera.NET</title>
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<link href="https://cdn.jsdelivr.net/npm/bootstrap@5.2.3/dist/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
        <link href="css/forms.css" rel="stylesheet" type="text/css" />
        <link href="css/tabelas.css" rel="stylesheet" type="text/css" />
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0">
		<form method="post" runat="server">
			<div class="container d-flex ms-5 ps-4 justify-content-center">
				<div class="row gx-3 gy-2 mt-4" style="width: 400px">
					<div class="col-12">
						<asp:label id="lblTitulo" runat="server" CssClass="tituloLabel"></asp:label><BR>
						<asp:textbox id="txtNR" runat="server" CssClass="texto form-control form-control-sm" TextMode="MultiLine" Rows="10" ReadOnly="True"></asp:textbox>
					</div>
				</div>
			</div>
		</form>
	</body>
</HTML>