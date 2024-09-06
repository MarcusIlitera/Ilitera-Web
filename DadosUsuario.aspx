<%@ Page Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="DadosUsuario.aspx.cs"
    Inherits="Ilitera.Net.DadosUsuario" Title="Ilitera.Net" %>

<%@ Register TagPrefix="uc1" TagName="Menu" Src="~/ucMenuLateral.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script language="javascript" type="text/javascript">

function verifica(nomeCampo, mostrar){
	senha = document.getElementById(nomeCampo).value;
	forca = 0;
	mostra = document.getElementById(mostrar);
	if((senha.length >= 4) && (senha.length <= 7)){
		forca += 10;
	}else if(senha.length>7){
		forca += 25;
	}
	if(senha.match(/[a-z]+/)){
		forca += 10;
	}
	if(senha.match(/[A-Z]+/)){
		forca += 20;
	}
	if(senha.match(/\d+/)){
		forca += 20;
	}
	if(senha.match(/\W+/)){
		forca += 25;
	}	
	
	return mostra_res();
}

function mostra_res(){
	if(forca < 30){
		mostra.innerHTML = '<table><tr><td bgcolor="red" width="'+forca+'"></td><td>Fraca </td></tr></table>';
	}else if((forca >= 30) && (forca < 60)){
		mostra.innerHTML = '<table><tr><td bgcolor="yellow" width="'+forca+'"></td><td>Justa </td></tr></table>';
	}else if((forca >= 60) && (forca < 85)){
		mostra.innerHTML = '<table><tr><td bgcolor="blue" width="'+forca+'"></td><td>Forte </td></tr></table>';
	}else{
		mostra.innerHTML = '<table><tr><td bgcolor="green" width="'+forca+'"></td><td>Excelente </td></tr></table>';
	}
	
	if(senha.length == 0)
	{
	    mostra.innerHTML = '';
	}
}

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <ig:WebSplitter runat="server" ID="Painel" Height="700px">
        <Panes>
            <ig:SplitterPane Size="20%" Style="padding: 5px;" BackColor="#edffeb">
                <Template>
                    <uc1:Menu runat="server" ID="Menu" />
                </Template>
            </ig:SplitterPane>
            <ig:SplitterPane Size="80%" Style="padding: 5px;" BackColor="#edffeb">
                <Template>
                    <table>
                        <tr>
                            <td colspan="2" align="center">
                                <asp:Label ID="lblTitulo" runat="server"><h5>Informações do Usuário: </h5></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Label ID="lblNomeUsuario" runat="server"><b>Nome do Usuário:</b></asp:Label>
                            </td>
                            <td align="left">
                                <asp:Label ID="lblNomeUsuarioText" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Label ID="lblNomeAbreviado" runat="server"><b>Nome Completo:</b></asp:Label>
                            </td>
                            <td align="left">
                                <asp:Label ID="lblNomeAbreviadoText" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Label ID="lblEmail" runat="server"><b>Email Cadastrado:</b></asp:Label>
                            </td>
                            <td align="left">
                                <asp:Label ID="lblEmailText" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Label ID="lblNumLogin" runat="server"><b>Número de Acessos:</b></asp:Label>
                            </td>
                            <td align="left">
                                <asp:Label ID="lblNumLoginText" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Label ID="lblDataCadastro" runat="server"><b>Data de Cadastro:</b></asp:Label>
                            </td>
                            <td align="left">
                                <asp:Label ID="lblDataCadastroText" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Label ID="lblDataUltLogin" runat="server"><b>Data do Último Acesso:</b></asp:Label>
                            </td>
                            <td align="left">
                                <asp:Label ID="lblDataUltLoginText" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Button ID="btnAlterarSenha" runat="server" Text="Alterar a senha" CssClass="btnLogarClass"
                                    OnClick="btnAlterarSenha_Click" />
                            </td>
                        </tr>
                    </table>
                    <table runat="server" id="tblSenhas" visible="false">
                        <tr>
                            <td colspan="3" align="left">
                                <asp:ValidationSummary ID="vsmSenha" runat="server" ShowMessageBox="false" ShowSummary="true"
                                    ValidationGroup="grupoSenha" HeaderText="Por favor corrija os erros abaixo:" />
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Label ID="lblSenhaAtual" runat="server"><b>Senha Atual:</b></asp:Label>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtSenhaAtual" TextMode="Password" runat="server" MaxLength="8"></asp:TextBox>
                            </td>
                            <td width="100">
                                <asp:RequiredFieldValidator ID="rfvSenhaAtual" runat="server" ErrorMessage="Digite a Senha Atual"
                                    ControlToValidate="txtSenhaAtual" Display="None" ValidationGroup="grupoSenha"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <asp:CompareValidator ID="cmvSenha" runat="server" ControlToValidate="txtNovaSenha"
                                    ControlToCompare="txtConfirmarNovaSenha" Display="None" ValidationGroup="grupoSenha"
                                    ErrorMessage="A nova senha e a confirmação da senha não coincidem."></asp:CompareValidator>
                                    
                                <asp:RequiredFieldValidator ID="rfvNovaSenha" runat="server" ControlToValidate="txtNovaSenha"
                                    ErrorMessage="Digite a Nova Senha." Display="None" ValidationGroup="grupoSenha"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Label ID="lblNovaSenha" runat="server"><b>Nova Senha:</b></asp:Label>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtNovaSenha" TextMode="Password" runat="server" MaxLength="8"></asp:TextBox>
                            </td>
                            <td width="100">
                                <asp:Label ID="mostraNovaSenha" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Label ID="lblConfirmarNovaSenha" runat="server"><b>Confirme a Nova Senha:</b></asp:Label>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtConfirmarNovaSenha" TextMode="Password" runat="server" MaxLength="8"></asp:TextBox>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="rfvConfirmarNovaSenha" runat="server" ControlToValidate="txtConfirmarNovaSenha"
                                    ErrorMessage="Digite a Confirmação da Nova Senha" Display="None" ValidationGroup="grupoSenha"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <asp:Button ID="ConfirmarAlteracao" runat="server" Text="Confirmar Nova Senha" CssClass="btnLogarClass"
                                    OnClick="btnConfirmarAlteracao_Click" ValidationGroup="grupoSenha" />
                            </td>
                        </tr>
                    </table>
                </Template>
            </ig:SplitterPane>
        </Panes>
    </ig:WebSplitter>
</asp:Content>
