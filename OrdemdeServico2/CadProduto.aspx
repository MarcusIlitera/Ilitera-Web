<%@ Page language="c#" Inherits="Ilitera.Net.OrdemDeServico.CadProduto" Codebehind="CadProduto.aspx.cs" %>
<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Ilitera.NET</title>
		<meta content="True" name="vs_showGrid">
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="scripts/style.css" type="text/css" rel="stylesheet">
		<LINK href="scripts/datagrid.css" type="text/css" rel="stylesheet">
		<script language="JavaScript" src="scripts/validador.js"></script>
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0">
		<form method="post" runat="server">
			<TABLE class="normalFont" id="Table1" cellSpacing="0" cellPadding="0" width="400" align="center"
				border="0">
				<TR>
					<TD align="center"><BR>
						<asp:label id="lblCadEqui" runat="server" CssClass="largeboldFont">Cadastro e Edição de Produtos</asp:label><BR>
						<BR>
						<asp:listbox id="lsbProdutos" runat="server" CssClass="inputBox" Width="380px" Rows="8" AutoPostBack="True" onselectedindexchanged="lsbProdutos_SelectedIndexChanged"></asp:listbox><BR>
						<BR>
						<asp:label id="Label1" runat="server" CssClass="boldFont">Nome</asp:label><BR>
						<asp:textbox id="txtNome" runat="server" CssClass="inputBox" Width="360px"></asp:textbox><BR>
						<BR>
						<asp:label id="Label2" runat="server" CssClass="boldFont">Descrição do Produto</asp:label><BR>
						<asp:textbox id="txtDescricao" runat="server" CssClass="inputBox" Width="360px" TextMode="MultiLine"
							Rows="4"></asp:textbox><BR>
						<BR>
						<asp:button id="btnGravar" runat="server" CssClass="buttonBox" Width="60px" Text="Gravar" onclick="btnGravar_Click"></asp:button>&nbsp;&nbsp;&nbsp;&nbsp;
						<asp:button id="btnExcluir" runat="server" CssClass="buttonBox" Width="60px" Text="Excluir"
							Enabled="False" onclick="btnExcluir_Click"></asp:button></TD>

                            
                            <asp:Label ID="lbl_Id_Empresa" runat="server" Text="IdEmpresa" Visible="False"></asp:Label>
                             <asp:Label ID="lbl_Id_Usuario" runat="server" Text="IdUsuario" Visible="False"></asp:Label>
				</TR>
			</TABLE>

         <eo:MsgBox ID="MsgBox1" runat="server" BackColor="#47729F" ControlSkinID="None" 
        HeaderHtml="Dialog Title" Height="100px" Width="168px">
        <HeaderStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #ffbf00 3px solid; padding-left: 4px; font-weight: bold; font-size: 11px; padding-bottom: 2px; color: white; padding-top: 2px; border-bottom: #22456a 1px solid; font-family: verdana" />
        <ContentStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #7d97b6 1px solid; padding-left: 4px; border-left-width: 1px; font-size: 11px; border-left-color: #728eb8; padding-bottom: 4px; color: white; padding-top: 4px; border-bottom: #22456a 1px solid; font-family: verdana" />
        <FooterStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #7d97b6 1px solid; padding-left: 4px; border-left-width: 1px; font-size: 11px; border-left-color: #728eb8; padding-bottom: 4px; color: white; padding-top: 4px; border-bottom: #22456a 1px solid; font-family: verdana" />
        </eo:MsgBox>

		</form>
	</body>
</HTML>
