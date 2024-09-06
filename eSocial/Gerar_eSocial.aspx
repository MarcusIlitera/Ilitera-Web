<%@ Page Title="Ilitera.Net" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="True"    CodeBehind="Gerar_eSocial.aspx.cs" Inherits="Ilitera.Net.e_Social.Gerar_eSocial" EnableEventValidation="false"  ValidateRequest="false"   %>

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

              	<table class="normalFont" id="Table1" cellspacing="0" cellpadding="0" width="600" align="center"
				border="0">

                    <tr>
                    <td align="center">

                        <asp:Label ID="lblTitulo" runat="server" style="font-weight: 700">Geração Eventos do e-Social</asp:Label>
                        <br />
                        <br />
                        <asp:label id="lblPCMSO" runat="server" CssClass="boldFont">Selecione o Evento :</asp:label>
                        &nbsp;
                        <br />
                        <asp:RadioButtonList ID="rd_Evento" runat="server" BorderStyle="Solid" 
                            CellPadding="2" CellSpacing="2" RepeatColumns="2" style="margin-right: 0px" 
                            Width="609px" BorderColor="Gray" BorderWidth="2px" AutoPostBack="True" EnableTheming="True">
                            <asp:ListItem Selected="True" Value="1005">1005 - Estabelecimentos/Obras</asp:ListItem>
                            <asp:ListItem Value="1060">1060 - Ambientes de Trabalho</asp:ListItem>
                            <asp:ListItem Value="2210">2210 - Acidente de Trabalho</asp:ListItem>
                            <asp:ListItem Value="2220">2220 - Monit. da Saúde do Trabalhador</asp:ListItem>
                            <asp:ListItem Value="2221">2221 - Exame Toxicológico</asp:ListItem>
                            <asp:ListItem Value="2230">2230 - Afastamento Temporário</asp:ListItem>
                            <asp:ListItem Value="2240">2240 - Condições amb. do Trabalho</asp:ListItem>
                            <asp:ListItem Value="2245">2245 - Treinamentos e Capacitações</asp:ListItem>
                        </asp:RadioButtonList>
                        <br />
                        <asp:Panel ID="Panel2" runat="server" BorderColor="Gray" 
                            BorderStyle="Solid" BorderWidth="2px" Height="143px" Width="604px">
                            <br />
                            <asp:Label ID="Label1" runat="server" Text="Data Inicial:"></asp:Label>
                            &nbsp;
                            <asp:TextBox ID="dtp_Inicial" runat="server" Width="81px"></asp:TextBox>
                            &nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Label ID="Label2" runat="server" Text="Data Final:"></asp:Label>
                            &nbsp;<asp:TextBox ID="dtp_Final" runat="server" Width="81px"></asp:TextBox>
                            <br />
                            &nbsp;&nbsp;&nbsp;
                            <br />
                            <asp:RadioButton ID="rd_Grupo" runat="server" GroupName="rd" Text="Todos colaboradores do Grupo" />
                            <br />
                            <asp:RadioButton ID="rd_Todos" runat="server" GroupName="rd" Text="Todos colaboradores da empresa" />
                            <br />
                            <asp:RadioButton ID="rd_Colaborador" runat="server" GroupName="rd" Text="Colaborador específico" />
                            <asp:ListBox ID="lst_Id" runat="server" Height="17px" Visible="False" Width="42px"></asp:ListBox>
                            <br />
                            <asp:DropDownList ID="cmb_Colaborador" runat="server" Font-Size="X-Small">
                            </asp:DropDownList>
                            <br />
                            <br />
                            &nbsp;&nbsp;&nbsp;&nbsp;
                            </asp:Panel>
                        <br />
                        <asp:Panel ID="Panel1" runat="server" BorderColor="Gray" 
                            BorderStyle="Solid" BorderWidth="2px" Height="63px" Width="591px">
                            <br />
                            <asp:CheckBox ID="chk_Gerar" runat="server" Font-Size="X-Small" Text="Gerar Arquivos XML durante geração dos eventos ( nome do arquivo será acrescido de CPF e Data/Hora )" Checked="True" />
                            &nbsp;
                            <br />
                            <asp:Label ID="Label4" runat="server" Font-Size="X-Small" Text="Nome do arquivo XML:"></asp:Label>
&nbsp;<asp:TextBox ID="txt_Arq" runat="server" Width="307px"></asp:TextBox>
                            <br />
                            <br />
                            &nbsp;&nbsp;&nbsp;&nbsp;
                            </asp:Panel>
                        <br />
                        <asp:Button ID="cmd_Gerar" runat="server" CssClass="btnLogarClass" 
                            OnClick="cmd_Gerar_Click" Width="224px" Text="Processar" />
                        <asp:ListBox ID="lst_Arqs" runat="server" Visible="False"></asp:ListBox>



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

