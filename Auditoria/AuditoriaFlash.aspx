<%@ Page Language="C#" AutoEventWireup="true" Inherits="Ilitera.Net.AuditoriaFlash" Codebehind="AuditoriaFlash.aspx.cs" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Ilitera.NET</title>
    <LINK href="scripts/styleNew.css" type="text/css" rel="stylesheet">
</head>

<body bottommargin="0" leftmargin="0" rightmargin="0" topmargin="0">
    <form id="frmAuditoriaFlash" runat="server">
            <table border="0" cellpadding="0" cellspacing="0" class="normalFont" width="779" align="center">
                <tr>
                    <td align="center">
                        <object classid="clsid:D27CDB6E-AE6D-11cf-96B8-444553540000" 
                        codebase="http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=7,0,19,0" width="779" height="523">
                        <param name="movie" value="Flash/auditoria.swf" />
                        <PARAM NAME="FlashVars" VALUE="IdCliente=<%=Request.QueryString["IdEmpresa"]%>">
                        <param name="quality" value="high" />
                        <param name="menu" value="false" />
                        <embed src="Flash/auditoria.swf" quality="high" pluginspage="http://www.macromedia.com/go/getflashplayer" type="application/x-shockwave-flash" width="779" height="523"></embed>
                        </object>
                    </td>
                </tr>
            </table>
    </form>
</body>
</html>
