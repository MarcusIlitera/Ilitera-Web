<%@ Page language="c#" Inherits="Ilitera.Net.OrdemDeServico.CadDanos" Codebehind="CadDanos.aspx.cs" %>
<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD runat="server">
		<title>Ilitera.NET</title>
        <script language="JavaScript" src="scripts/validador.js"></script>
        <LINK href="scripts/style.css" type="text/css" rel="stylesheet">
		<LINK href="scripts/datagrid.css" type="text/css" rel="stylesheet">

		<script language="javascript">
		function setNome(sNome)
		{
			if (document.getElementById("lsbDanos").options[0].selected)
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
			<TABLE class="defaultFont" id="Table1" cellSpacing="0" cellPadding="0" width="350" align="center"
				border="0">
				<TR>
					<TD align="center"><BR>
						<asp:label id="Label3" runat="server" SkinID="TitleFont">Cadastro e Edição de Danos</asp:label><BR>
						<BR>
						<asp:listbox id="lsbDanos" runat="server" Rows="8" Width="330px"></asp:listbox><BR>
						<BR>
						<asp:label id="Label2" runat="server" SkinID="BoldFont">Nome</asp:label><BR>
						<asp:textbox id="txtNome" runat="server" Width="320px"></asp:textbox><BR>
						<BR>
						<asp:button id="btnGravar" runat="server" Width="60px" Text="Gravar" onclick="btnGravar_Click"></asp:button>&nbsp;&nbsp;&nbsp;&nbsp;
						<asp:button id="btnExcluir" runat="server" Width="60px" Text="Excluir"
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
