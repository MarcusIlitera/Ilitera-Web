<%@ Page Language="C#"   AutoEventWireup="True"  MasterPageFile="~/Site.Master" CodeBehind="Apresentacao.aspx.cs"  Inherits="Ilitera.Net.Apresentacao" Title="Ilitera.Net" %>

<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>


<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
  
</asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent" >


<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Mestra.NET</title>
    <LINK href="scripts/styleNew.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="scripts/validador.js"></script>
</head>
<script language="javascript">
    function AbreFlash()
	{
		window.open("http://www.adobe.com/shockwave/download/download.cgi?P1_Prod_Version=ShockwaveFlash&promoid=BIOW", "Acrobat");
	}
</script>

<body bottommargin="0" leftmargin="0" rightmargin="0" topmargin="0">
    <form id="frmApresentacao" runat="server">--%>
            <table border="0" cellpadding="0" cellspacing="0" class="normalFont" width="620" align="center">
                <tr>
                    <td align="center">
                        <br />
                        <asp:Label ID="Label1" runat="server" Text="Para visualizar a apresentação da Auditoria,<br>é necessário possuir préviamente instalado o <b>Adobe Flash Player.</b>"></asp:Label><br />
                        <br />
                            <table border="0" cellpadding="0" cellspacing="0" width="480" align="center">
                                <tr>
                                    <td align="center" width="360" style="height: 31px">
                        <asp:Label ID="Label2" runat="server" Text="Para fazer o download gratuitamente, clique no botão ao lado:"></asp:Label></td>
                                    <td align="center" width="120" style="height: 31px">
                                    <a href="#" onclick="AbreFlash()"><asp:Image
                            ID="Image1" runat="server" ImageUrl="../Img/getFlashPlayer.gif" ToolTip="Get Adobe Flash Player" /></a></td>
                                </tr>
                            </table>
                        <br />
                        <br />
                            <table align="center" border="0" cellpadding="0" cellspacing="0" width="360">
                                <tr>
                                    <td align="center" width="180">
                                        <asp:Label ID="Label3" runat="server" CssClass="boldFont" Text="Selecione a Auditoria"></asp:Label><br />
                                        <asp:DropDownList ID="ddlAuditoria" runat="server" CssClass="inputBox" ToolTip="Selecione a Auditoria"
                                            Width="110px">
                                        </asp:DropDownList></td>
                                    <td align="center" width="180">
                                        <asp:LinkButton ID="lkbAuditoriaFlash" runat="server" CssClass="boldFont" OnClick="lkbAuditoriaFlash_Click"><img src='img/flashDocument.jpg' alt='Visualizar apresentação' border='0'> Visualizar apresentação</asp:LinkButton></td>
                                </tr>
                            </table>
                        <br />
                        <br />
                        <br />
                        <asp:Label ID="Label4" runat="server" Text="Se preferir, clique no botão abaixo para fazer o download da Apresentação da<br>Auditoria selecionada e visualizá-la diretamente de seu computador."></asp:Label><br />
                        <br />
                        <asp:LinkButton ID="lkbDownload" runat="server" CssClass="boldFont" OnClick="lkbDownload_Click"><img src='img/download.gif' alt='Download da apresentação' border='0'>Download da apresentação</asp:LinkButton></td>
                </tr>
            </table>
<%--    </form>
</body>
</html>
--%>


</asp:Content>