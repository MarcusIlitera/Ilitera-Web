<%@ Page Language="c#" Inherits="Ilitera.Net.PageSet" Codebehind="PageSet.aspx.cs" %>
<HTML>
	<HEAD>
		<title>Ilitera.NET</title>
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
		<LINK href="scripts/style.css" type="text/css" rel="stylesheet">
		<script language="javascript">
		if (top.window.document.frames.conteudo.window.document.getElementById('txtIdUsuario').value == "")
		    top.window.document.frames.conteudo.window.document.getElementById('txtIdUsuario').value = window.location.href.substring(window.location.href.indexOf('IdUsuario=') + 10);
		
		if(top.window.document.frames.conteudo.window.document.getElementById('txtIdEmpresa').value != "")
			top.window.document.frames.conteudo.window.document.frames.principal.window.document.frames.principal.window.location.href="ListagemIrregularidades.aspx?IdUsuario=" + top.window.document.frames.conteudo.window.document.getElementById('txtIdUsuario').value + "&IdEmpresa=" + top.window.document.frames.conteudo.window.document.getElementById('txtIdEmpresa').value + "";
		else
			top.window.document.frames.conteudo.window.document.frames.principal.window.document.frames.principal.window.location.href="ListaEmpresa.aspx?IdUsuario=" + top.window.document.frames.conteudo.window.document.getElementById('txtIdUsuario').value + "";
		</script>
	</HEAD>
	<body bottomMargin="0" bgColor="#ffffff" leftMargin="0" topMargin="0" rightMargin="0">
		<form name="Index" method="post" runat="server">
		</form>
	</body>
</HTML>
