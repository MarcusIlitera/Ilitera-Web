<%@ Page Language="c#" Inherits="Ilitera.Net.ControleEPI.Index" Codebehind="index.aspx.cs" %>
<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>

<HTML>
	<HEAD>
		<title>Ilitera</title>
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
		<LINK href="scripts/style.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body bottomMargin="0" bgColor="#ffffff" leftMargin="0" topMargin="0" rightMargin="0">
		<form name="Index" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td align="left">
						<table height="100%" cellSpacing="0" cellPadding="0" width="779" border="0">
							<tr>
								<td vAlign="top" width="150"><iframe name="menu" marginWidth="0" hspace="0" vspace="0" marginHeight="0" frameBorder="0" width="150" scrolling="no" height="100%" src="MenuList.aspx?menutype=0"></iframe>
								</td>
								<TD vAlign="top" width="629"><iframe name="principal" marginWidth="0" hspace="0" vspace="0" marginHeight="0" src="ListaEmpresa.aspx?IdUsuario=<%=Request["IdUsuario"]%>"
										frameBorder="0" width="629" scrolling="auto" height="100%"></iframe>
								</TD>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
	</body>
	<script language="javascript">
	if(top.window.document.frames.conteudo.window.document.getElementById('txtIdEmpresa').value != "")
		top.window.document.frames.conteudo.window.document.frames.principal.window.document.frames.principal.window.location.href="CadastroEPI.aspx?IdUsuario=" + top.window.document.frames.conteudo.window.document.getElementById('txtIdUsuario').value + "&IdEmpresa=" + top.window.document.frames.conteudo.window.document.getElementById('txtIdEmpresa').value + "";
	</script>
</HTML>
