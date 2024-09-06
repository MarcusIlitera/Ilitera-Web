<%@ Page Language="C#"   AutoEventWireup="True" CodeBehind="CadAudiometro.aspx.cs"  Inherits="Ilitera.Net.PCMSO.CadAudiometro" Title="Ilitera.Net" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Ilitera.NET</title>
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="scripts/style.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="scripts/validador.js"></script>
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0">
		<form method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="300" align="center" border="0">
				<TR>
					<TD align="center" colSpan="2" class="normalFont"><BR>
						<asp:Label id="lblCadEdi" runat="server" CssClass="largeboldFont">Cadastro e Edição de Audiometros</asp:Label><BR>
						<BR>
						<asp:ListBox id="lsbAudiometro" runat="server" CssClass="inputBox" Width="280px" Rows="8" AutoPostBack="True" onselectedindexchanged="lsbAudiometro_SelectedIndexChanged"></asp:ListBox><BR>
						<BR>
						<asp:Label id="lblNome" runat="server" CssClass="boldFont">Nome</asp:Label><BR>
						<asp:textbox id="txtNome" runat="server" Width="280px" CssClass="inputBox"></asp:textbox><BR>
						<BR>
						<TABLE class="normalFont" id="Table2" cellSpacing="0" cellPadding="0" width="300" align="center"
							border="0">
							<TR>
								<TD align="center" width="150"><asp:Label id="lblFabricante" runat="server" CssClass="boldFont">Fabricante</asp:Label><BR>
									<asp:textbox id="txtFabricante" runat="server" Width="130px" CssClass="inputBox"></asp:textbox></TD>
								<TD align="center" width="150"><asp:Label id="lblData" runat="server" CssClass="boldFont">Data da última aferição</asp:Label><BR>
									<igtxt:WebDateTimeEdit id="wdtDataAfericao" runat="server" CssClass="inputBox" Width="130px" EditModeFormat="dd/MM/yyyy"
										ImageDirectory=" " JavaScriptFileName="ig_edit.js" JavaScriptFileNameCommon="ig_shared.js" Nullable="False"
										HorizontalAlign="Center"></igtxt:WebDateTimeEdit></TD>
							</TR>
						</TABLE>
						<BR>
						<BR>
						<asp:button id="btnOk" runat="server" Text="Gravar" CssClass="buttonBox" Width="70px" onclick="btnOk_Click"></asp:button>&nbsp;&nbsp;&nbsp;&nbsp;<asp:button id="btnExcluir" runat="server" Text="Excluir" CssClass="buttonBox" Width="70px"
							Enabled="False" onclick="btnExcluir_Click"></asp:button>
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
