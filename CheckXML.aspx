<%@ Page Title="Ilitera.Net" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="True"    CodeBehind="CheckXML.aspx.cs" Inherits="Ilitera.Net.CheckXML" %>

<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>


<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <style type="text/css">
        .defaultFont
    {
        width: 586px;
            height: 20px;
        }
        .inputBox
        {}
        .largeboldfont
        {}
    </style>
</asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent" >

        <LINK href="scripts/style.css" type="text/css" rel="stylesheet">
                    
                    <br />
                    
                    <asp:Label ID="lblPPRA" runat="server" Text="Carga XML" 
                        Font-Bold="True" Font-Italic="False"></asp:Label>
                    <br />
                    <br />
                                    <asp:FileUpload ID="File1" runat="server" ClientIDMode="Static" 
                                        Font-Size="XX-Small" Width="517px" accept=".xml" />
                    <br />

                    <table id="t1" >

                    <tr>
                    <td style="HEIGHT: 15px" align="left" width="295">

                        
                        <br />

                        <br />
                    <asp:Button ID="btnProjeto" runat="server" OnClick="btnProjeto_Click" 
                        Width="233px" Text="Executar Carga XML" CssClass="buttonBox"/>

                        <br />
                        <br />
                    <asp:Button ID="btnProjeto0" runat="server" OnClick="btnProjetoFecomercio_Click" 
                        Width="233px" Text="Executar Carga XML Fecomércio" CssClass="buttonBox"/>

                        <br />
                        <br />
                        <br />
                    <asp:Button ID="btnTesteAgendamento" runat="server" OnClick="btnTesteAgendamento_Click" 
                        Width="233px" Text="Teste Agendamento" CssClass="buttonBox"/>

                        <br />
                        <br />
                        <br />
                        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Button" />
                        <br />
                        <br />

                        </td>

                        <td style="HEIGHT: 15px" align="left" width="295">
                        </td>
                        </tr>
                      

                        </table>

                    <br />
                    <br />
                    <br />

       <eo:MsgBox ID="MsgBox1" runat="server" BackColor="#47729F" ControlSkinID="None" 
        HeaderHtml="Dialog Title" Height="100px" Width="250px" >
        <HeaderStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #ffbf00 3px solid; padding-left: 4px; font-weight: bold; font-size: 11px; padding-bottom: 2px; color: white; padding-top: 2px; border-bottom: #22456a 1px solid; font-family: verdana" />
        <ContentStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #7d97b6 1px solid; padding-left: 4px; border-left-width: 1px; font-size: 11px; border-left-color: #728eb8; padding-bottom: 4px; color: white; padding-top: 4px; border-bottom: #22456a 1px solid; font-family: verdana" />
        <FooterStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #7d97b6 1px solid; padding-left: 4px; border-left-width: 1px; font-size: 11px; border-left-color: #728eb8; padding-bottom: 4px; color: white; padding-top: 4px; border-bottom: #22456a 1px solid; font-family: verdana" />
        </eo:MsgBox>
                 
                    
</asp:Content>
