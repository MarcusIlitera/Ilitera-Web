
<%@ Page language="c#" Inherits="Ilitera.Net.ControleEPI.ListagemEPITodos" Codebehind="ListagemEPITodos.aspx.cs" %>
<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ListagemEPI</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="scripts/style.css" type="text/css" rel="stylesheet">
		<LINK href="scripts/datagrid.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="scripts/validador.js"></script>
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="620" align="center" border="0">
				<TR>
					<TD class="normalFont" align="center"><BR>
						<asp:label id="lblUtilizacaoEPI" runat="server" CssClass="largeboldFont">Listagem de Todos os EPI em utilização</asp:label></TD>
				</TR>
				<TR>
					<TD class="normalFont" align="center"><BR>
					</TD>
				</TR>
				<TR>
					<TD class="normalFont" align="center"><asp:datagrid id="DGridUtilizacaoEPITodos" runat="server" CssClass="table" Width="600px" GridLines="Vertical"
							BorderWidth="1px" BorderColor="#004000" AllowPaging="True" AutoGenerateColumns="False" CellPadding="3" HorizontalAlign="Center">
							<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#669999"></SelectedItemStyle>
							<EditItemStyle CssClass="tableItem"></EditItemStyle>
							<AlternatingItemStyle CssClass="alternatingItem"></AlternatingItemStyle>
							<ItemStyle CssClass="tableItem"></ItemStyle>
							<HeaderStyle Font-Bold="True" CssClass="tableHeader"></HeaderStyle>
							<FooterStyle ForeColor="#000066" BackColor="White"></FooterStyle>
							<Columns>
								<asp:BoundColumn Visible="False" DataField="IdEPI" HeaderText="IdEPI">
									<HeaderStyle Width="0px"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="Nome" HeaderText="EPI">
									<HeaderStyle Width="430px"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="QtdEntregue" HeaderText="Em Utiliza&#231;&#227;o">
									<HeaderStyle Width="100px"></HeaderStyle>
								</asp:BoundColumn>
								<asp:ButtonColumn Text="Detalhes" CommandName="DetalheEPI">
									<HeaderStyle Width="70px"></HeaderStyle>
									<ItemStyle Font-Bold="True"></ItemStyle>
								</asp:ButtonColumn>
							</Columns>
							<PagerStyle CssClass="tablePage" Mode="NumericPages"></PagerStyle>
						</asp:datagrid><asp:label id="lblTotRegistros" runat="server" CssClass="normalFont"></asp:label></TD>
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
