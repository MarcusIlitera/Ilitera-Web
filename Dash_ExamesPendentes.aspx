<%@ Page Title="Ilitera.Net" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="True"    CodeBehind="Dash_ExamesPendentes.aspx.cs" Inherits="Ilitera.Net.Dash_ExamesPendentes" %>

<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>


<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
       <style type="text/css">
        .linha
   {
	font: 8px Verdana, Arial, Helvetica, sans-serif, Tahoma;
    }
           .btnLogarClass
           {}
           .style2
           {
               height: 26px;
           }
    </style>

</asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent" >

                 

    <table>

<iframe width="1024" height="1060" src="https://app.powerbi.com/view?r=eyJrIjoiMjNmYzhiZWMtMTVhMy00Y2EyLTkyNDktZjk2NzdiYjA5MTcyIiwidCI6IjdiNjZkNjhkLWVkZDktNDM1ZC05NzY4LTEwNzhkNTE3ODU0MyJ9" frameborder="0" allowFullScreen="true"></iframe>

    </table>


 <eo:MsgBox ID="MsgBox1" runat="server" BackColor="#47729F" ControlSkinID="None" 
        HeaderHtml="Dialog Title" Height="100px" Width="168px">
        <HeaderStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #ffbf00 3px solid; padding-left: 4px; font-weight: bold; font-size: 11px; padding-bottom: 2px; color: white; padding-top: 2px; border-bottom: #22456a 1px solid; font-family: verdana" />
        <ContentStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #7d97b6 1px solid; padding-left: 4px; border-left-width: 1px; font-size: 11px; border-left-color: #728eb8; padding-bottom: 4px; color: white; padding-top: 4px; border-bottom: #22456a 1px solid; font-family: verdana" />
        <FooterStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #7d97b6 1px solid; padding-left: 4px; border-left-width: 1px; font-size: 11px; border-left-color: #728eb8; padding-bottom: 4px; color: white; padding-top: 4px; border-bottom: #22456a 1px solid; font-family: verdana" />
    </eo:MsgBox>

</asp:Content>
