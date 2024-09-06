<%@ Page Language="C#"   AutoEventWireup="True" CodeBehind="CadProcedimentoClinico.aspx.cs"  Inherits="Ilitera.Net.PCMSO.CadProcedimentoClinico" Title="Ilitera.Net" %>

<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Ilitera.NET</title>
        <link href="scripts/style.css" type="text/css" rel="stylesheet">
        <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.2.3/dist/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
        <link href="css/forms.css" rel="stylesheet" type="text/css" />
        <link href="css/tabelas.css" rel="stylesheet" type="text/css" />
        <LINK href="scripts/style.css" type="text/css" rel="stylesheet">

    <script id="igClientScript" type="text/javascript">
<!--
    function warpLsbProcedimentoClinico_InitializePanel(oPanel)
    {
	    oPanel.getProgressIndicator().setImageUrl("img/loading.gif");
    }

    function warpDetails_RefreshComplete(oPanel)
    {
	    if (document.getElementById("txtAuxAviso").value != "")
	    {
	        window.alert(document.getElementById("txtAuxAviso").value);
	        window.opener.document.getElementById("txtAuxiliar").value = "atualizaProcedimentoClinico";
			window.opener.document.forms[0].submit();
	        document.getElementById("txtAuxAviso").value = "";
	    }
    }
// -->
    </script>
</head>
<body bottommargin="0" leftmargin="0" rightmargin="0" topmargin="0">
    <form id="form1" runat="server" defaultbutton="btnGravar">
        <div class="container d-flex ms-5 ps-4 justify-content-center">
            <div class="row gx-3 gy-2 text-center" style="width: 400px;">

                <div class="col-12 subtituloBG" style="padding-top: 10px; margin-top: 20px;">
                    <asp:Label ID="Label1" runat="server" SkinID="TitleFont" CssClass="subtitulo" Style="margin-left: 0px !important;" Text="Cadastro / Edição de Procedimentos Clínicos"></asp:Label>
                </div>

                <div class="col-12">
                    <div class="row">
                        <div class="col-12 gy-2">
                            <asp:ListBox id="lsbProcedimentoClinico" runat="server" AutoPostBack="True" Rows="7" CssClass="texto form-control form-control-sm" OnSelectedIndexChanged="lsbProcedimentoClinico_SelectedIndexChanged"></asp:ListBox>
                        </div>
                        
                        <div class="col-12 gy-2 text-start">
                            <asp:Label id="Label2" runat="server" Text="Nome" SkinID="BoldFont" CssClass="tituloLabel"></asp:Label>
                            <asp:TextBox id="txtNome" runat="server" CssClass="texto form-control form-control-sm"></asp:TextBox>
                        </div>

                        <div class="col-12 gy-2 text-start">
                            <asp:Label id="Label3" runat="server" Text="Descrição" SkinID="BoldFont" CssClass="tituloLabel"></asp:Label>
                            <asp:TextBox id="txtDescricao" runat="server" Rows="2" TextMode="MultiLine" CssClass="texto form-control form-control-sm"></asp:TextBox>
                        </div>

                        <div class="col-12 gy-2 text-center">
                            <asp:Button id="btnGravar" runat="server" Text="Gravar" ToolTip="Gravar Queixa Clínica" CssClass="btn" onclick="btnGravar_Click"></asp:Button>
                            <asp:Button id="btnExcluir" runat="server" Text="Excluir" ToolTip="Excluir Queixa Clínica" CssClass="btn2" onclick="btnExcluir_Click"></asp:Button>
                            <input id="txtAuxAviso" type="hidden" runat="server" />
                        </div>
                    </div>
                </div>

            </div>
        </div>
<%--                    <igmisc:webasyncrefreshpanel id="warpLsbProcedimentoClinico" runat="server" borderwidth="0px" cssclass="defaultFont"
                        height="" width="340px" InitializePanel="warpLsbProcedimentoClinico_InitializePanel" LinkedRefreshControlID="warpDetails">
--%>                        
    <%--                    </igmisc:webasyncrefreshpanel>--%>
                    <%--<igmisc:WebAsyncRefreshPanel ID="warpDetails" runat="server" BorderWidth="0px" CssClass="defaultFont" Width="340px" RefreshComplete="warpDetails_RefreshComplete">--%>
                      
                                    
                                    <%--</igmisc:WebAsyncRefreshPanel>--%>
        
         <eo:MsgBox ID="MsgBox1" runat="server" BackColor="#47729F" ControlSkinID="None" 
        HeaderHtml="Dialog Title" Height="100px" Width="168px">
        <HeaderStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #ffbf00 3px solid; padding-left: 4px; font-weight: bold; font-size: 11px; padding-bottom: 2px; color: white; padding-top: 2px; border-bottom: #22456a 1px solid; font-family: verdana" />
        <ContentStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #7d97b6 1px solid; padding-left: 4px; border-left-width: 1px; font-size: 11px; border-left-color: #728eb8; padding-bottom: 4px; color: white; padding-top: 4px; border-bottom: #22456a 1px solid; font-family: verdana" />
        <FooterStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #7d97b6 1px solid; padding-left: 4px; border-left-width: 1px; font-size: 11px; border-left-color: #728eb8; padding-bottom: 4px; color: white; padding-top: 4px; border-bottom: #22456a 1px solid; font-family: verdana" />
        </eo:MsgBox>
    </form>
</body>
</html>