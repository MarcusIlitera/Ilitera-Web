<%@ Page language="c#" Inherits="Ilitera.Net.OrdemDeServico.CadConjunto" Codebehind="CadConjunto.aspx.cs" %>
<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Ilitera.NET</title>
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<link href="scripts/style.css" type="text/css" rel="stylesheet">
		<link href="https://cdn.jsdelivr.net/npm/bootstrap@5.2.3/dist/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
		<link href="css/forms.css" rel="stylesheet" type="text/css" />
		<link href="css/tabelas.css" rel="stylesheet" type="text/css" /> 
		<script language="javascript">
		function setNome(sNome)
		{
			if (document.getElementById("listBxConjuntos").options[0].selected)
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
			<div class="container d-flex ms-5 mt-2 ps-4 justify-content-center">
				<div class="row gx-3 gy-2 text-start" style="width: 400px;">

					<div class="col-12 subtituloBG text-center" style="padding-top: 10px; margin-top: 20px;">
						<asp:label id="Label3" runat="server" CssClass="subtitulo" style="margin-left: 0 !important;">Cadastro e Edição de Conjuntos de Procedimentos</asp:label>
					</div>

					<div class="col-12 gx-3 gy-2">
						<asp:listbox id="listBxConjuntos" runat="server" CssClass="texto form-control form-control-sm" Rows="8"></asp:listbox>
					</div>

					<div class="col-12 gx-3 gy-2">
						<asp:label id="Label2" runat="server" CssClass="tituloLabel">Nome</asp:label>
						<asp:textbox id="txtNome" runat="server" CssClass="texto form-control form-control-sm"></asp:textbox>
					</div>

					<div class="text-center gx-3 gy-2">
						<asp:button id="btnGravar" runat="server" CssClass="btn" Text="Gravar" onclick="btnGravar_Click"></asp:button>
						<asp:button id="btnExcluir" runat="server" CssClass="btn2" Text="Excluir" Enabled="False" onclick="btnExcluir_Click"></asp:button>
					</div>

					<div class="col-12 gx-3 gy-2">
						<asp:Label ID="lbl_Id_Empresa" runat="server" Text="IdEmpresa" Visible="False" CssClass="tituloLabel"></asp:Label>
						<asp:Label ID="lbl_Id_Usuario" runat="server" Text="IdUsuario" Visible="False" CssClass="tituloLabel"></asp:Label>
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