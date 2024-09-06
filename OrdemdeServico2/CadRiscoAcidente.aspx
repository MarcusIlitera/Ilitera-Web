<%@ Page language="c#" Inherits="Ilitera.Net.OrdemDeServico.CadRiscoAcidente" Codebehind="CadRiscoAcidente.aspx.cs" %>
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
			if (document.getElementById("lsbRiscoAcidente").options[0].selected)
			{	
				document.getElementById("btnExcluir").disabled = true;
				document.getElementById("txtNome").value = "";
			}
			else
			{
				document.getElementById("btnExcluir").disabled = false;
				document.getElementById("txtNome").value = sNome;
			}
		}
		</script>
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0">
		<form method="post" runat="server">
			<TABLE class="normalFont" id="Table1" cellSpacing="0" cellPadding="0" width="350" align="center"
				border="0">
				<TR>
					<TD align="center"><BR>
						<asp:Label id="Label3" runat="server" CssClass="largeboldFont">Cadastro e Edição de Riscos de Acidentes</asp:Label><BR>
						<BR>
						<asp:listbox id="lsbRiscoAcidente" runat="server" Width="330px" CssClass="inputBox" Rows="8"></asp:listbox><BR>
						<BR>
						<asp:label id="Label2" runat="server" CssClass="boldFont">Nome</asp:label><BR>
						<asp:textbox id="txtNome" runat="server" CssClass="inputBox" Width="320px"></asp:textbox><BR>
						<BR>
						<asp:Button id="btnGravar" runat="server" CssClass="buttonBox" Width="60px" Text="Gravar" onclick="btnGravar_Click"></asp:Button>&nbsp;&nbsp;&nbsp;&nbsp;
						<asp:Button id="btnExcluir" runat="server" Text="Excluir" CssClass="buttonBox" Width="60px"
							Enabled="False" onclick="btnExcluir_Click"></asp:Button></TD>
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
