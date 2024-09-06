<%@ Page language="c#" Inherits="Ilitera.Net.ControleEPI.AddFabricante" Codebehind="AddFabricante.aspx.cs" %>
<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Ilitera.NET</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<link href="https://cdn.jsdelivr.net/npm/bootstrap@5.2.3/dist/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
		<link href="css/forms.css" rel="stylesheet" type="text/css" />
		<link href="css/tabelas.css" rel="stylesheet" type="text/css" />
		<LINK href="scripts/style.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0">
		<form id="frmAddFabricante" method="post" runat="server">
			<div class="container d-flex ms-5 ps-4 justify-content-center">
				<div class="row gx-3 gy-2 mt-3" style="width: 400px">
					<div class="col-12 subtituloBG text-center">
						<asp:label id="lblCadFabricante" runat="server" CssClass="subtitulo" style="margin-left: 0px !important">Edição e Cadastro de Fabricante</asp:label>
					</div>

					<div class="col-12">
						<div class="row">
							<div class="col-12 gy-3">
                                <asp:label id="lblSelFabricante" runat="server" CssClass="tituloLabel">Selecione o Fabricante</asp:label>
                                <asp:dropdownlist id="ddlFabricante" runat="server" CssClass="texto form-select" AutoPostBack="True" onselectedindexchanged="ddlFabricante_SelectedIndexChanged"></asp:dropdownlist>
                            </div>
							<div class="col-12 gy-3">
								<asp:label id="lblFabricante" runat="server" CssClass="tituloLabel">Nome do Fabricante</asp:label><BR>
								<asp:textbox id="txtFabricante" runat="server" CssClass="texto form-control form-control-sm"></asp:textbox>
							</div>
							<div class="col-12 gy-4 text-center">
								<asp:button id="btnGravar" runat="server" CssClass="btn" Text="Gravar" onclick="btnGravar_Click"></asp:button>
                                <asp:button id="btnCancela" runat="server" CssClass="btn2" Text="Cancela"></asp:button>
								<asp:label id="lblError" runat="server" CssClass="errorFont"></asp:label>
							</div>
						</div>
					</div>
				</div>
			</div>
        
         <eo:MsgBox ID="MsgBox1" runat="server" BackColor="#47729F" ControlSkinID="None" 
        HeaderHtml="Dialog Title" Height="100px" Width="168px">
        <HeaderStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #ffbf00 3px solid; padding-left: 4px; font-weight: bold; font-size: 11px; padding-bottom: 2px; color: white; padding-top: 2px; border-bottom: #22456a 1px solid; font-family: verdana" />
        <ContentStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #7d97b6 1px solid; padding-left: 4px; border-left-width: 1px; font-size: 11px; border-left-color: #728eb8; padding-bottom: 4px; color: white; padding-top: 4px; border-bottom: #22456a 1px solid; font-family: verdana" />
        <FooterStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #7d97b6 1px solid; padding-left: 4px; border-left-width: 1px; font-size: 11px; border-left-color: #728eb8; padding-bottom: 4px; color: white; padding-top: 4px; border-bottom: #22456a 1px solid; font-family: verdana" />
        </eo:MsgBox>
		</form>
	</body>
</HTML>
