<%@ Page Language="C#"   AutoEventWireup="True"  MasterPageFile="~/Site.Master" CodeBehind="Relatorio_Ambulatoriais.aspx.cs"  Inherits="Ilitera.Net.PCMSO.Relatorio_Ambulatoriais" Title="Ilitera.Net" %>

<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <style type="text/css">
        #txtIdUsuario
        {
            width: 0px;
        }
        #txtIdUsuario0
        {
            width: 0px;
        }
        #txtIdEmpregado
        {
            width: 0px;
        }
        #txtIdEmpregado0
        {
            width: 0px;
        }
        #txtIdEmpresa
        {
            width: 0px;
        }
        #SubDados
        {
            height: 317px;
        }
        #Table1
        {
            width: 713px;
        }
        #Table2
        {
            width: 725px;
        }
        .buttonFoto
        {}
        #Table13
        {
            height: 12px;
            width: 97px;
        }
        .tituloPagina1 {
            font-family: 'Univia Pro';
            font-style: normal;
            font-weight: 700;
            font-size: 16px;
            line-height: 120%;
            color: #E98035;
        }
        .borda {
            box-sizing: border-box;
            border: 1px solid #B0ABAB;
            border-radius: 4px;
            padding: 15px;
        }
    </style>
</asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent" >
     <link href="css/forms.css" rel="stylesheet" type="text/css" />

	<script language="javascript">
        function Reload() {
            var f = document.getElementById('SubDados');
            //f.src = f.src;
            f.contentWindow.location.reload(true);
        }
    </script>



<%--<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head id="Head1" runat="server">
		<title>Ilitera.NET</title>
		<script language="JavaScript" src="scripts/validador.js"></script>
		<link href="scripts/style.css" type="text/css" rel="stylesheet">
	</head>
	<body bottommargin="0" leftmargin="0" topmargin="0" rightmargin="0">
		<form name="DadosEmpregado" method="post" runat="server">
--%>		
    <div class="container-fluid d-flex ms-5 ps-4">
        <div class="row gx-3 gy-2 w-100">
                           <div class="col-11 subtituloBG" style="padding-top: 10px" >
                                <asp:Label ID="Label4" runat="server" class="subtitulo">Relatórios de Exames Ambulatoriais</asp:Label>
                            </div>
	<table class="normalFont" id="Table1" cellspacing="0" cellpadding="0" width="1100"
				border="0" style="width: 91.5%;">
				
                <tr>
                    <td>
                        <div class="col-12">
                            <div class="row gx-3 gy-2 borda mt-2">
                                <Span class="tituloLabel">Data do Exame</Span>
                                <div class="col-1 me-4">
                                    <asp:Label id="Label12" runat="server" CssClass="tituloLabel form-label col-form-label col-form-label-sm mb-1">Data início</asp:Label>
                                    <asp:Textbox ID="txt_Data" runat="server" 
                                        CssClass="inputBox texto form-control form-control-sm" EditModeFormat="dd/MM/yyyy" HorizontalAlign="Center" 
                                        ImageDirectory=" " Nullable="False" ToolTip="Data de Início">
                                    </asp:Textbox>
                                </div>
                                <div class="col-1 me-4">
                                    <asp:Label id="Label1" runat="server" CssClass="tituloLabel form-label col-form-label col-form-label-sm">Data final</asp:Label>
                                    <asp:Textbox ID="txt_Data2" runat="server" CssClass="inputBox texto form-control form-control-sm" 
                                        EditModeFormat="dd/MM/yyyy" HorizontalAlign="Center" ImageDirectory=" " 
                                        Nullable="False" ToolTip="Data de Início">
                                    </asp:Textbox>
                                </div>
                                <div class="col-4 me-4">
                                    <asp:Label id="Label3" runat="server" CssClass="tituloLabel form-label" Height="13">Empresa Selecionanda </asp:Label>
                                    <asp:DropDownList ID="cmb_Empresa" runat="server" CssClass="texto form-select form-select-sm">
                                        <asp:ListItem Value="1">Empresa selecionada</asp:ListItem>
                                        <asp:ListItem Value="2">Empresa selecionada e projetos</asp:ListItem>
                                        <asp:ListItem Value="3">Todas empresas do grupo</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-4">
                                    <asp:Label id="Label2" runat="server" CssClass="tituloLabel form-label" Height="13">Considerar Colaboradores </asp:Label>
                                    <asp:DropDownList ID="cmb_Tipo" runat="server" 
                                        CssClass="texto form-select form-select-sm">
                                        <asp:ListItem Value="1">Ativo</asp:ListItem>
                                        <asp:ListItem Value="2">Inativos</asp:ListItem>
                                        <asp:ListItem Value="3">Todos</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="row mt-3">
                                    <div class="col-2 ms-2 gx-4 gy-2">
                                        <asp:RadioButton ID="rd_Analitico" oncheckedchanged="rd_Analitico_CheckedChanged" runat="server" Text="Analítico" 
                                            AutoPostBack="True" Checked="True" GroupName="g1" CssClass="texto form-control-sm"/>
                                    </div>
                                    <div class="col-2 gx-4 gy-2">
                                        <asp:RadioButton ID="rd_Sumarizado" oncheckedchanged="rd_Sumarizado_CheckedChanged" runat="server" Text="Sumarizado" 
                                            AutoPostBack="True" GroupName="g1" CssClass="texto form-control-sm"/>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </td>
                </tr>

                <tr>
                <td>
                    <b>
                        <asp:button ID="btnemp" runat="server"  onclick="btnemp_Click" Text="Gerar Relatório"  CssClass="btn mt-4">
                        </asp:button>
                    </b>
                </td>
                </tr>
            </tr>

                                        
                                            <caption>
                                                <input id="txtIdEmpresa" type="text" visible="False" style="visibility:hidden"/>
                                </caption>


        <eo:MsgBox ID="MsgBox1" runat="server" BackColor="#47729F" ControlSkinID="None" 
        HeaderHtml="Dialog Title" Height="100px" Width="168px">
        <HeaderStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #ffbf00 3px solid; padding-left: 4px; font-weight: bold; font-size: 11px; padding-bottom: 2px; color: white; padding-top: 2px; border-bottom: #22456a 1px solid; font-family: verdana" />
        <ContentStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #7d97b6 1px solid; padding-left: 4px; border-left-width: 1px; font-size: 11px; border-left-color: #728eb8; padding-bottom: 4px; color: white; padding-top: 4px; border-bottom: #22456a 1px solid; font-family: verdana" />
        <FooterStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #7d97b6 1px solid; padding-left: 4px; border-left-width: 1px; font-size: 11px; border-left-color: #728eb8; padding-bottom: 4px; color: white; padding-top: 4px; border-bottom: #22456a 1px solid; font-family: verdana" />
        </eo:MsgBox>
            
            </table>  
            </div>
        </div>



<%--		</form>
	</body>
</HTML>
--%>



    </asp:Content>