<%@ Page Language="C#"   AutoEventWireup="True"  MasterPageFile="~/Site.Master" CodeBehind="Relatorio_Exames.aspx.cs"  Inherits="Ilitera.Net.PCMSO.Relatorio_Exames" Title="Ilitera.Net" %>

<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <link href="css/tabelas.css" rel="stylesheet" type="text/css" />
    <link href="css/forms.css" rel="stylesheet" type="text/css" />
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

    </style>
</asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent" >
    <div class="container-fluid d-flex ms-5 ps-4">
    <div class="row gx-3 gy-2 w-100">

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
        
        <div class="col-11 subtituloBG pt-2">
            <asp:Label runat="server" CssClass="subtitulo">Relatório de Exames</asp:Label>
        </div>
	<table class="normalFont" id="Table1" cellspacing="0" cellpadding="0" width="1000" align="center"
				border="0">
        <div class="row mt-3" style="border: 1px solid silver; width: 91.5%; border-radius: 4px;">
            <div class="col-12">
                <div class="row">
                    <div class="col-2 gx-4 gy-2">
                        <asp:Label runat="server" CssClass="tituloLabel form-label form-label-sm">Data Inicial</asp:Label>
                        <asp:Literal runat="server"><br /></asp:Literal>
                        <asp:Textbox ID="txt_Data" runat="server"  CssClass="texto form-control form-control-sm" EditModeFormat="dd/MM/yyyy" HorizontalAlign="Center" 
                             ImageDirectory=" " Nullable="False" ToolTip="Data de Início" height="32px">
                         </asp:Textbox>
                    </div>
                    
                    <div class="col-2 gy-2">
                        <asp:Label runat="server" CssClass="tituloLabel form-label form-label-sm">Data Final</asp:Label>
                        <asp:Literal runat="server"><br /></asp:Literal>
                        <asp:Textbox ID="txt_Data2" runat="server" CssClass="texto form-control form-control-sm" EditModeFormat="dd/MM/yyyy" HorizontalAlign="Center" 
                             ImageDirectory=" " Nullable="False" ToolTip="Data de Início" height="32px">
                        </asp:Textbox>
                    </div>
                </div>
            </div>
            

            <div class="col-12">
                <div class="row">
                    <div class="col-2 ms-4 gx-4 gy-3">
                        <asp:RadioButton ID="rd_Analitico" runat="server" CssClass="texto form-check-label bg-transparent border-0" Text="Analítico" AutoPostBack="True" Checked="True" GroupName="g1" />
                    </div>
                    <div class="col-2 gy-3">
                        <asp:RadioButton ID="rd_Sumarizado" runat="server" CssClass="texto form-check-label bg-transparent border-0" Text="Sumarizado: " AutoPostBack="True" GroupName="g1" />
                    </div>
                    <div class="col-4 gy-2">
                        <asp:DropDownList ID="cmb_Empresa" runat="server" CssClass="texto form-select form-select-sm" Height="32px" AutoPostBack="True">
                            <asp:ListItem Value="1">Empresa selecionada</asp:ListItem>
                            <asp:ListItem Value="2">Empresa selecionada e projetos</asp:ListItem>
                            <asp:ListItem Value="3">Todas empresas do grupo</asp:ListItem>
                            <asp:ListItem Value="4">Todas empresas do sistema</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
            </div>

            <div class="col-12">
                <div class="row">
                    <div class="col-2 ms-4 gx-4 gy-2">
                        <asp:RadioButton ID="rd_Todos" runat="server" CssClass="texto form-check-label bg-transparent border-0" Text="Todos" Checked="true" AutoPostBack="true" GroupName="g2"/>
                    </div>
                    <div class="col-2 gy-2">
                        <asp:RadioButton ID="rd_Clinicos" runat="server" CssClass="texto form-check-label bg-transparent border-0" Text="Clínicos" AutoPostBack="true" GroupName="g2"/>
                    </div>
                    <div class="col-2 gy-2">
                        <asp:RadioButton ID="rd_Complementares" runat="server" CssClass="texto form-check-label bg-transparent border-0" Text="Complementares" AutoPostBack="true" GroupName="g2"/>
                    </div>
                </div>
            </div>

            <div class="col-12">
                <div class="row">
                    <div class="col-2 ms-4 gx-4 gy-2">
                        <asp:RadioButton ID="chk_PDF" runat="server" CssClass="texto form-check-label bg-transparent border-0" Text="Gerar PDF" Checked="true" AutoPostBack="true" GroupName="g3"/>
                    </div>
                    <div class="col-2 gy-2">
                        <asp:RadioButton ID="chk_CSV" runat="server" CssClass="texto form-check-label bg-transparent border-0" Text="Gerar CSV" AutoPostBack="true" GroupName="g3"/>
                    </div>
                </div>
            </div>

            <div class="col-12 mb-2">
                <div class="row">
                    <div class="col-2 ms-4  gx-4 gy-2">
                        <asp:CheckBox ID="chk_R_Normal" runat="server" CssClass="texto form-check-label bg-transparent border-0" AutoPostBack="True" Checked="True" Text="Normal" />
                    </div>
                    <div class="col-2 gy-2">
                        <asp:CheckBox ID="chk_R_Alterado" runat="server" CssClass="texto form-check-label bg-transparent border-0" AutoPostBack="True" Checked="True" Text="Alterado" />
                    </div>
                    <div class="col-2 gy-2">
                        <asp:CheckBox ID="chk_R_Espera" runat="server" CssClass="texto form-check-label bg-transparent border-0" AutoPostBack="True" Text="Em Espera" />
                    </div>
                    <div class="col-2 gy-2">
                        <asp:CheckBox ID="chk_R_SemResultado" runat="server" CssClass="texto form-check-label bg-transparent border-0" AutoPostBack="True" Text="Sem Resultado" />
                    </div>
                </div>
            </div>
        </div>       

        <div class="col-12">
            <asp:button ID="btnemp" runat="server"  onclick="btnemp_Click" Text="Gerar Relatório" CssClass="btn"></asp:button>
        </div>

                    
                    
                    

                                        
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


<%--		</form>
	</body>
</HTML>
--%>


        </div>
        </div>
    </asp:Content>