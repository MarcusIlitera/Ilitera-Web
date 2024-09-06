<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="Tratar_Excecao.aspx.cs"
    Inherits="Ilitera.Net.Tratar_Excecao" Title="Ilitera.Net" %>



<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent" >

   
                    <table style="width: 601px">
                        <tr>
                            <td colspan="2" align="center">
                                <asp:Label ID="lblTitulo" runat="server" Font-Size="Medium"><h5>Informações de Exceção do Sistema: </h5></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                <br />
                                <strong>Log :</strong></td>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:TextBox ID="txt_Erro" runat="server" BackColor="#CCCCCC" Font-Bold="True" 
                                    Height="64px" Width="600px" Font-Size="Small" TextMode="MultiLine"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                <br />
                                <strong>Erro :</strong></td>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:TextBox ID="txt_Excecao" runat="server" BackColor="#CCCCCC" Font-Bold="True" 
                                    Height="43px" Width="600px"></asp:TextBox>
                            </td>
                        </tr>

                        <tr>
                            <td align="left">
                                <br />
                                <strong>Origem :</strong></td>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:TextBox ID="txt_Source" runat="server" BackColor="#CCCCCC" Font-Bold="True" 
                                    Height="42px" Width="600px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                &nbsp;</td>
                        </tr>
                    </table>
                    <table runat="server" id="tblSenhas" visible="false">
                        <tr>
                            <td colspan="3" align="left">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td align="right">
                                &nbsp;</td>
                            <td align="left">
                                &nbsp;</td>
                            <td width="100">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td align="right">
                                &nbsp;</td>
                            <td align="left">
                                &nbsp;</td>
                            <td width="100">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td align="right">
                                &nbsp;</td>
                            <td align="left">
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <asp:Button ID="ConfirmarAlteracao" runat="server" Text="Voltar ao Sistema" CssClass="btnLogarClass"
                                    OnClick="btnConfirmarAlteracao_Click" ValidationGroup="grupoSenha" />
                            </td>
                        </tr>
                    </table>
</asp:Content>
