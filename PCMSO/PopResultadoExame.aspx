<%@ Page Language="C#"   AutoEventWireup="True" CodeBehind="PopResultadoExame.aspx.cs"  Inherits="Ilitera.Net.PCMSO.PopResultadoExame" Title="Ilitera.Net" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Mestra.NET</title>
		<meta content="True" name="vs_showGrid">
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="scripts/style.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="scripts/validador.js"></script>
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0">
		<form id="FrmResultadoExame" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="370" align="center" border="0"
				class="normalFont">
				<TR>
					<TD class="normalFont" vAlign="top" align="center"><BR>
						<asp:label id="lblExames" runat="server" CssClass="largeboldFont"> Exame Audiométrico</asp:label><BR>
						<BR>
						<TABLE class="normalFont" id="Table2" cellSpacing="0" cellPadding="0" width="370" align="center"
							border="0">
							<TR>
								<TD align="center" width="185"><asp:label id="Label1" runat="server" CssClass="boldFont">Data</asp:label><BR>
									<igtxt:WebDateTimeEdit id="wdtDataExame" runat="server" CssClass="inputBox" Width="100px" EditModeFormat="dd/MM/yyyy"
										ImageDirectory=" " Nullable="False"
										HorizontalAlign="Center"></igtxt:WebDateTimeEdit></TD>
								<TD align="center" width="185"><asp:label id="Label2" runat="server" CssClass="boldFont">Tipo</asp:label><BR>
									<asp:dropdownlist id="ddlTipoExame" runat="server" CssClass="inputBox" Width="165px">
										<asp:ListItem Value="0">Admissional</asp:ListItem>
										<asp:ListItem Value="5">Demissional</asp:ListItem>
										<asp:ListItem Value="3">Mudan&#231;a de Fun&#231;&#227;o</asp:ListItem>
										<asp:ListItem Value="2">Peri&#243;dico</asp:ListItem>
										<asp:ListItem Value="4">Retorno ao Trabalho</asp:ListItem>
										<asp:ListItem Value="1">Semestral</asp:ListItem>
									</asp:dropdownlist></TD>
							</TR>
						</TABLE>
						<BR>
						<TABLE class="normalFont" id="Table3" cellSpacing="0" cellPadding="0" width="370" align="center"
							border="0">
							<TR>
								<TD align="center" width="185">
									<asp:Panel id="Panel1" runat="server" CssClass="normalFont" Width="165px" BorderStyle="Solid"
										BorderWidth="1px" BorderColor="Silver">
										<TABLE class="normalFont" id="Table5" cellSpacing="0" cellPadding="0" width="100" align="center"
											border="0">
											<TR>
												<TD align="center">
													<asp:Image id="Image1" runat="server" ImageUrl="img/5pixel.gif"></asp:Image></TD>
											</TR>
										</TABLE>
										<asp:label id="lblOE" runat="server" CssClass="boldFont">Orelha Esquerda</asp:label>
										<BR>
										<BR>
										<asp:dropdownlist id="ddlResultadoEsquerda" runat="server" CssClass="inputBox" Width="145px" AutoPostBack="True" onselectedindexchanged="ddlResultadoEsquerda_SelectedIndexChanged">
											<asp:ListItem Value="0">Normal</asp:ListItem>
											<asp:ListItem Value="1">Anormal</asp:ListItem>
										</asp:dropdownlist>
										<BR>
										<BR>
										<TABLE class="normalFont" id="Table4" cellSpacing="0" cellPadding="0" width="145" align="center"
											border="0">
											<TR>
												<TD align="left">
													<asp:checkbox id="ckbOEReferencial" runat="server" CssClass="normalFont" Text="Referencial"></asp:checkbox><BR>
													<asp:checkbox id="ckbAgravamentoOE" runat="server" CssClass="normalFont" Text="Agravamento" Enabled="False"></asp:checkbox><BR>
													<asp:checkbox id="ckbOEOcupacional" runat="server" CssClass="normalFont" Text="Ocupacional" Enabled="False"></asp:checkbox></TD>
											</TR>
										</TABLE>
										<TABLE class="normalFont" id="Table7" cellSpacing="0" cellPadding="0" width="100" align="center"
											border="0">
											<TR>
												<TD align="center">
													<asp:Image id="Image2" runat="server" ImageUrl="img/5pixel.gif"></asp:Image></TD>
											</TR>
										</TABLE>
									</asp:Panel></TD>
								<TD align="center" width="185">
									<asp:Panel id="Panel2" runat="server" CssClass="normalFont" Width="165px" BorderStyle="Solid"
										BorderWidth="1px" BorderColor="Silver">
										<TABLE class="normalFont" id="Table8" cellSpacing="0" cellPadding="0" width="100" align="center"
											border="0">
											<TR>
												<TD align="center">
													<asp:Image id="Image4" runat="server" ImageUrl="img/5pixel.gif"></asp:Image></TD>
											</TR>
										</TABLE>
										<asp:label id="lblOD" runat="server" CssClass="boldFont">Orelha Direita</asp:label>
										<BR>
										<BR>
										<asp:dropdownlist id="ddlResultadoDireita" runat="server" CssClass="inputBox" Width="145px" AutoPostBack="True" onselectedindexchanged="ddlResultadoDireita_SelectedIndexChanged">
											<asp:ListItem Value="0">Normal</asp:ListItem>
											<asp:ListItem Value="1">Anormal</asp:ListItem>
										</asp:dropdownlist>
										<BR>
										<BR>
										<TABLE class="normalFont" id="Table6" cellSpacing="0" cellPadding="0" width="145" align="center"
											border="0">
											<TR>
												<TD align="left">
													<asp:checkbox id="ckbODReferencial" runat="server" CssClass="normalFont" Text="Referencial"></asp:checkbox><BR>
													<asp:checkbox id="ckbAgravamentoOD" runat="server" CssClass="normalFont" Text="Agravamento" Enabled="False"></asp:checkbox><BR>
													<asp:checkbox id="ckbODOcupacional" runat="server" CssClass="normalFont" Text="Ocupacional" Enabled="False"></asp:checkbox></TD>
											</TR>
										</TABLE>
										<TABLE class="normalFont" id="Table9" cellSpacing="0" cellPadding="0" width="100" align="center"
											border="0">
											<TR>
												<TD align="center">
													<asp:Image id="Image5" runat="server" ImageUrl="img/5pixel.gif"></asp:Image></TD>
											</TR>
										</TABLE>
									</asp:Panel></TD>
							</TR>
						</TABLE>
						<BR>
						<BR>
						<asp:button id="btnGravar" runat="server" CssClass="buttonBox" Text="Gravar" Width="70px" onclick="btnGravar_ServerClick"></asp:button>&nbsp;&nbsp;
						<asp:button id="btnExcluir" runat="server" CssClass="buttonBox" Text="Excluir" Width="70px" onclick="btnExcluir_Click"></asp:button></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
