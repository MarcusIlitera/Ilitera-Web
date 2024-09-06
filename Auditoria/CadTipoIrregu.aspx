<%@ Page language="c#" Inherits="Ilitera.Net.CadTipoIrregu" Codebehind="CadTipoIrregu.aspx.cs" %>

<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Ilitera.NET</title>
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="scripts/style.css" type="text/css" rel="stylesheet">
		<script language="javascript">
		function setNome(sNome)
		{
			if (document.getElementById("lbxTipoIrre").options[0].selected)
			{	
				document.getElementById("btnExcluir").disabled = true;
				document.getElementById("txtTipoIrregu").value = "";
			}
			else
			{
				document.getElementById("btnExcluir").disabled = false;
				document.getElementById("txtTipoIrregu").value = sNome;
			}
		}	
		</script>
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0">
		<form method="post" runat="server" defaultbutton="btnSalvar">
			<TABLE cellSpacing="0" cellPadding="0" width="350" align="center" border="0">
				<TR>
					<td class="normalFont" vAlign="top" align="center" colSpan="2"><BR>
						<asp:label id="lblTitulo" runat="server" CssClass="largeboldFont">Cadastro/Edição do Tipo da Irregularidade</asp:label><BR>
						<BR>
						<asp:ListBox id="lbxTipoIrre" runat="server" CssClass="inputBox" Rows="8" Width="320px"></asp:ListBox><BR>
						<BR>
						<asp:label id="lblTipo" runat="server" CssClass="boldFont">Tipo da Irregularidade</asp:label><BR>
						<asp:TextBox id="txtTipoIrregu" runat="server" CssClass="inputBox" Width="320px"></asp:TextBox><BR>
						<BR>
						<asp:Button id="btnSalvar" runat="server" CssClass="buttonBox" Width="60px" Text="Gravar" onclick="btnSalvar_Click"></asp:Button>&nbsp;&nbsp; &nbsp; &nbsp;&nbsp;
						<asp:Button id="btnExcluir" runat="server" CssClass="buttonBox" Width="60px" Text="Excluir" onclick="btnExcluir_Click" Enabled="False"></asp:Button></td>
				</TR>
			</TABLE>

        <eo:MsgBox ID="MsgBox1" runat="server" BackColor="#47729F" ControlSkinID="None" 
        HeaderHtml="Dialog Title" Height="100px" Width="250px" >
        <HeaderStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #ffbf00 3px solid; padding-left: 4px; font-weight: bold; font-size: 11px; padding-bottom: 2px; color: white; padding-top: 2px; border-bottom: #22456a 1px solid; font-family: verdana" />
        <ContentStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #7d97b6 1px solid; padding-left: 4px; border-left-width: 1px; font-size: 11px; border-left-color: #728eb8; padding-bottom: 4px; color: white; padding-top: 4px; border-bottom: #22456a 1px solid; font-family: verdana" />
        <FooterStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #7d97b6 1px solid; padding-left: 4px; border-left-width: 1px; font-size: 11px; border-left-color: #728eb8; padding-bottom: 4px; color: white; padding-top: 4px; border-bottom: #22456a 1px solid; font-family: verdana" />
        </eo:MsgBox>
		</form>
	</body>



</HTML>
