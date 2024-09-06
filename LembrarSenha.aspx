<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LembrarSenha.aspx.cs" Inherits="Ilitera.Net.LembrarSenha" %>
<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Ilitera.Net</title>
    <link href="css/StyleSheet1.css" rel="Stylesheet" type="text/css" />
    <style type="text/css">
        .style1
        {
            width: 182px;
        }
        .style2
        {
            font-family: Arial;
        }
        .style3
        {
            font-size: small;
        }
    </style>
</head>
<body>
    <div style="background-color: #edffeb;text-align:left;">
        <form id="form1" runat="server">
        <asp:ScriptManager ID="scriptManager1" runat="server">
        </asp:ScriptManager>
        <table>
            <tr>
                <td colspan="2">
                    <h5 class="style2">
                        Lembrar a senha.</h5>
                </td>
            </tr>
            <tr>
                <td colspan="2" class="style2">
                    <span class="style3">Por favor digite o Nome do Usuário cadastrado no sistema</span>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:textbox ID="txtNomeUsuario" runat="server" MaxLength="255">
                    </asp:textbox>
                </td>
                <td class="style1">
                    <asp:Button ID="btnEnviaSenha" runat="server" 
                        Text="Enviar minha senha por e-mail" OnClick="btnEnviaSenha_Click"
                        CssClass="btnLogarClass" Font-Size="X-Small" />
                </td>
            </tr>
        </table>

   <eo:MsgBox ID="MsgBox1" runat="server" BackColor="#47729F" ControlSkinID="None" 
        HeaderHtml="Dialog Title" Height="100px" Width="168px">
        <HeaderStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #ffbf00 3px solid; padding-left: 4px; font-weight: bold; font-size: 11px; padding-bottom: 2px; color: white; padding-top: 2px; border-bottom: #22456a 1px solid; font-family: verdana" />
        <ContentStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #7d97b6 1px solid; padding-left: 4px; border-left-width: 1px; font-size: 11px; border-left-color: #728eb8; padding-bottom: 4px; color: white; padding-top: 4px; border-bottom: #22456a 1px solid; font-family: verdana" />
        <FooterStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #7d97b6 1px solid; padding-left: 4px; border-left-width: 1px; font-size: 11px; border-left-color: #728eb8; padding-bottom: 4px; color: white; padding-top: 4px; border-bottom: #22456a 1px solid; font-family: verdana" />
    </eo:MsgBox>
        </form>
    </div>
</body>
</html>
