<%@ Page Language="C#"   AutoEventWireup="True"  MasterPageFile="~/Site.Master" CodeBehind="MenuList.aspx.cs"  Inherits="Ilitera.Net.MenuList" Title="Ilitera.Net" %>

<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>


<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
  
</asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent" >


<%--<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Mestra.NET</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<script language="JavaScript" src="scripts/validador.js"></script>
		<script language="JavaScript" src="scripts/menu.js"></script>
		<LINK href="scripts/stylemenu.css" type="text/css" rel="stylesheet">
--%>			
     <script language="javascript">
		function SetLink(url, target, w, h, nomejanela, tipoclose)
		{
			if (url.indexOf("?") == -1)
				url = url + "?IdUsuario=" + top.window.document.frames.conteudo.window.document.getElementById('txtIdUsuario').value;
			else
				url = url + "&IdUsuario=" + top.window.document.frames.conteudo.window.document.getElementById('txtIdUsuario').value;
				
			url = url + "&IdEmpresa=" + top.window.document.frames.conteudo.window.document.getElementById('txtIdEmpresa').value;
			if (top.window.document.frames.conteudo.window.document.getElementById('txtIdEmpregado').value != "")
				url = url + "&IdEmpregado=" + top.window.document.frames.conteudo.window.document.getElementById('txtIdEmpregado').value;
				
			if (target == "popup")
				void(addItem(centerWin(url, w, h, nomejanela), tipoclose));
			else
				if (target == "contents")
					top.window.document.frames.conteudo.window.document.frames.principal.window.document.frames.principal.window.document.frames.SubDados.window.location.href=url;
				else
					if (target == "popupacrobat")
						void(window.open(url, nomejanela, "scrollbars=yes, resizable=yes, toolbar=no, addressbar=no, menubar=no, width=" + w + "px, height=" + h + "px").focus());
					else
						if (target == "error")
							window.alert("Em construção!");
						else
							if (target == "bigcontents")
								top.window.document.frames.conteudo.window.document.frames.principal.window.document.frames.principal.window.location.href=url;
		}
		
		function TrocaFunc()
		{			
			for (x=0; x<top.window.allwinfunc.length; x++)
			{
				try
				{
				    top.window.allwinfunc[x].close();
				}
				catch (e)
				{}
			}
			
			top.window.document.frames.conteudo.window.document.getElementById('txtIdEmpregado').value = "";
			top.window.document.frames.conteudo.window.document.frames.principal.window.document.frames.principal.window.location.href="ListaEmpregado.aspx?IdUsuario=" + top.window.document.frames.conteudo.window.document.getElementById('txtIdUsuario').value + "&IdEmpresa=" + top.window.document.frames.conteudo.window.document.getElementById('txtIdEmpresa').value;
		}
		
		function TrocaEmp()
		{
			for (i=0; i<top.window.allwin.length; i++)
			{
				try
			    {
			        top.window.allwin[i].close();
			    }
			    catch (e)
			    {}
			}
			
			window.location.href="MenuList.aspx?menutype=0";
			top.window.document.frames.conteudo.window.document.frames.principal.window.document.frames.principal.window.location.href="ListaEmpresa.aspx?IdUsuario=" + top.window.document.frames.conteudo.window.document.getElementById('txtIdUsuario').value;
		}
			</script>
<%--	</HEAD>
	<body bottomMargin="0" bgColor="#ffffff" leftMargin="0" topMargin="0" rightMargin="0">
		<form method="post" runat="server">--%>
			<TABLE id="Table1" height="100%" cellSpacing="0" cellPadding="0" width="150" bgColor="#006600"
				border="0">
				<TR>
					<TD vAlign="top" width="150" bgColor="#006600"><FONT face="Verdana" size="1">&nbsp;</FONT></TD>
				</TR>
				<TR>
					<TD vAlign="top" width="150" bgColor="#006600" height="100%">
						<iglbar:ultraweblistbar id="UltraWebListbarMenu" runat="server" HeaderClickAction="None" Height="0px" Width="150px"
							BorderStyle="None" BarWidth="100%" MergeStyles="True"
							ItemIconStyle="SmallWithInsetText" ViewType="ExplorerBar" GroupSpacing="10px">
							<DefaultGroupStyle Cursor="Default" BackColor="Transparent"></DefaultGroupStyle>
							<DefaultItemStyle Cursor="Default" Height="13px" Font-Size="XX-Small" Font-Names="Verdana" ForeColor="White">
								<Margin Left="2px"></Margin>
							</DefaultItemStyle>
							<DefaultItemHoverStyle Cursor="Hand" Font-Underline="True" ForeColor="White"></DefaultItemHoverStyle>
							<DefaultGroupHeaderAppearance>
								<ExpandedAppearance ExpansionIndicatorImage="uparrows_white.gif">
									<Style Width="100%" Cursor="Default" Height="17px" Font-Size="XX-Small" Font-Names="Verdana"
										Font-Bold="True" ForeColor="White" BackgroundImage="HeaderBackGreen.gif">

<Padding Left="1px">
</Padding>

</Style>
								</ExpandedAppearance>
							</DefaultGroupHeaderAppearance>
							<DefaultItemSelectedStyle Cursor="Default" Font-Underline="True"></DefaultItemSelectedStyle>
						</iglbar:ultraweblistbar></TD>
				</TR>
			</TABLE>
<%--		</form>
	</body>
</HTML>--%>


</asp:Content>