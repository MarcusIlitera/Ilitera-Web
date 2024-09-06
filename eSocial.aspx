<%@ Page Title="Ilitera.Net" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="True"    CodeBehind="eSocial.aspx.cs" Inherits="Ilitera.Net.eSocial" %>

<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>


<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
       <style type="text/css">
        .linha
   {
	font: 8px Verdana, Arial, Helvetica, sans-serif, Tahoma;
    }
           .btnLogarClass
           {}
    </style>

</asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent" >

                    <table style="width: 675px">
                    <tr>
                    <td align="center">

                        <br />

                        <asp:Label ID="lblTitulo" runat="server" style="font-weight: 700">Arquivos do e-Social</asp:Label>
                        <br />
                        <br />
                        <asp:label id="lblPCMSO" runat="server" CssClass="boldFont">Selecione o Evento :</asp:label>
                        &nbsp;
                        <br />
                        <asp:RadioButtonList ID="rd_Evento" runat="server" BorderStyle="Solid" 
                            CellPadding="0" CellSpacing="4" RepeatColumns="1" style="margin-right: 0px" 
                            Width="600px" BorderColor="Gray" BorderWidth="2px">
                            <asp:ListItem Selected="True">2260 - CAT</asp:ListItem>
                            <asp:ListItem>2280 - ASO</asp:ListItem>
                            <asp:ListItem>2320 - Afastamento temporário</asp:ListItem>
                            <asp:ListItem>2330 - Retorno de afast.temporário</asp:ListItem>
                            <asp:ListItem>2340 - Estabilidade Início</asp:ListItem>
                            <asp:ListItem>2345 - Estabilidade Término</asp:ListItem>
                            <asp:ListItem>2360 - Condição diferenciada de trabalho - Início</asp:ListItem>
                            <asp:ListItem>2365 - Condição diferenciada de trabalho - Término</asp:ListItem>
                        </asp:RadioButtonList>
                        <br />
                        Destino :<asp:Panel ID="Panel1" runat="server" BorderColor="Gray" 
                            BorderStyle="Solid" BorderWidth="2px" Height="140px" Width="600px">
                            <br />
                            <asp:RadioButton ID="rd_Arquivo" runat="server" Checked="True" GroupName="r1" 
                                Text="Arquivo :" />
                            &nbsp;
                            <asp:TextBox ID="TextBox1" runat="server" Width="439px"></asp:TextBox>
                            <br />
                            <br />
                            <asp:RadioButton ID="rd_URL" runat="server" GroupName="r1" Text="URL :" />
                            &nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:TextBox ID="TextBox2" runat="server" Width="452px"></asp:TextBox>
                        </asp:Panel>
                        <br />
                        <br />
                        <asp:Button ID="cmd_Gerar" runat="server" CssClass="btnLogarClass" 
                            OnClick="cmd_Gerar_Click" Width="224px" Text="Processar" />
                        <br />
                        <br />
                        <br />
                        <br />

                        </td>
                        </tr>
                        </table>

 <eo:MsgBox ID="MsgBox1" runat="server" BackColor="#47729F" ControlSkinID="None" 
        HeaderHtml="Dialog Title" Height="100px" Width="168px">
        <HeaderStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #ffbf00 3px solid; padding-left: 4px; font-weight: bold; font-size: 11px; padding-bottom: 2px; color: white; padding-top: 2px; border-bottom: #22456a 1px solid; font-family: verdana" />
        <ContentStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #7d97b6 1px solid; padding-left: 4px; border-left-width: 1px; font-size: 11px; border-left-color: #728eb8; padding-bottom: 4px; color: white; padding-top: 4px; border-bottom: #22456a 1px solid; font-family: verdana" />
        <FooterStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #7d97b6 1px solid; padding-left: 4px; border-left-width: 1px; font-size: 11px; border-left-color: #728eb8; padding-bottom: 4px; color: white; padding-top: 4px; border-bottom: #22456a 1px solid; font-family: verdana" />
    </eo:MsgBox>


</asp:Content>

