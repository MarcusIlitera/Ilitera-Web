
<%@ Page language="c#" Inherits="Ilitera.Net.ControleEPI.AddEquipamento" Codebehind="AddEquipamento.aspx.cs" %>
<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Ilitera.NET</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="scripts/style.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0">
		<form id="CadastroEPI" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="600" align="center" border="0">
				<TR>
					<TD class="normalFont" align="center"><asp:label id="lblCadEquipamento" runat="server" CssClass="largeboldFont">Edição e Cadastro de Equipamento</asp:label></TD>
				</TR>
				<TR>
					<TD class="normalFont" align="center"><BR>
						<asp:label id="lblSelEquipamento" runat="server" CssClass="BoldFont">Selecione o Equipamento:</asp:label><BR>
						<asp:dropdownlist id="ddlEquipamento" runat="server" CssClass="inputBox" AutoPostBack="True" onselectedindexchanged="ddlEquipamento_SelectedIndexChanged"></asp:dropdownlist><BR>
						<BR>
						<asp:label id="lblEquipamento" runat="server" CssClass="BoldFont">Nome do Equipamento:</asp:label><BR>
						<asp:textbox id="txtEquipamento" runat="server" CssClass="inputBox" Width="580px"></asp:textbox></TD>
				</TR>
				<TR>
					<TD class="normalFont" align="center"><BR>
						<asp:button id="btnGravar" runat="server" CssClass="buttonBox" Text="Gravar" onclick="btnGravar_Click"></asp:button>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
						<asp:button id="btnCancela" runat="server" CssClass="buttonBox" Text="Cancela"></asp:button><BR>
						<BR>
						<asp:label id="lblError" runat="server" CssClass="errorFont"></asp:label></TD>
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
