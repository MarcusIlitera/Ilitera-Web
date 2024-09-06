<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="True"
    Title="Ilitera.Net"  EnableEventValidation="false"  ValidateRequest="false"  %>

 

<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <link href="css/forms.css" rel="stylesheet" type="text/css" />
    <link href="css/tabelas.css" rel="stylesheet" type="text/css" />
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

    <div class="col-11 subtituloBG" style="padding-top: 10px" >
            <asp:Label ID="Label1" runat="server" class="subtitulo">Relatórios de Vacina</asp:Label>
        </div>

	<table class="normalFont" id="Table1" cellspacing="0" cellpadding="0" width="1000"
				border="0">
				
                <tr>
                    <div class="col-12 mt-3">
                        <div class="row gx-4 gy-2" style="border: 1px solid silver; width: 91.5%; border-radius: 4px;">
                               <Span class="tituloLabel gx-4">Data de Vacinação</Span>
                            <div class="col-2 me-2 gx-4 gy-2">
                                    <asp:Label id="Label12" runat="server" CssClass="tituloLabel form-label col-form-label col-form-label-sm mb-1">Data início</asp:Label>
                                    <asp:Textbox ID="txt_Data" runat="server" 
                                        CssClass="inputBox texto form-control form-control-sm" EditModeFormat="dd/MM/yyyy" HorizontalAlign="Center" 
                                        ImageDirectory=" " Nullable="False" ToolTip="Data de Início">
                                    </asp:Textbox>
                                </div>
                            <div class="col-2 me-4 gx-4 gy-2">
                                    <asp:Label id="Label2" runat="server" CssClass="tituloLabel form-label col-form-label col-form-label-sm">Data final</asp:Label>
                                    <asp:Textbox ID="txt_Data2" runat="server" CssClass="inputBox texto form-control form-control-sm" 
                                        EditModeFormat="dd/MM/yyyy" HorizontalAlign="Center" ImageDirectory=" " 
                                        Nullable="False" ToolTip="Data de Início">
                                    </asp:Textbox>
                                    </div>
                            <div class="col-4 me-4 gx-4 gy-2">
                                    <asp:Label runat="server" CssClass="tituloLabel form-label" Height="13">Empresa Selecionada </asp:Label>
                                    <asp:DropDownList id="cmb_Empresa" runat="server" CssClass="texto form-select form-select-sm">
                                        <asp:ListItem Value="1">Empresa selecionada</asp:ListItem>
                                        <asp:ListItem Value="2">Empresa selecionada e projetos</asp:ListItem>
                                        <asp:ListItem Value="3">Todas empresas do grupo</asp:ListItem>
                                    </asp:DropDownList>
                                </div>  
                            <div class="row mt-3">
                                <div class="col-2 ms-4 gx-4 gy-2">
                                    <asp:RadioButton ID="rd_Analitico" runat="server" Text="Analítico" CssClass="texto form-check-label border-0 bg-transparent" Style="text-align: left" Checked="true"
                                        AutoPostBack="true"
                                        GroupName="g1" />
                                </div>
                                <div class="col-2 gx-4 gy-2">
                                    <asp:RadioButton ID="rd_Sumarizado" runat="server" Text="Sumarizado" CssClass="texto form-check-label border-0 bg-transparent" Style="text-align: left" Checked="true"
                                        AutoPostBack="true"
                                        GroupName="g1" />
                                </div>

                                <div class="row">
                                    <div class="col-2 ms-4 gx-4 gy-2">
                                        <asp:RadioButton ID="chk_PDF" runat="server"  Cssclass="texto form-check-label border-0 bg-transparent" AutoPostBack="true" GroupName="g3"
                                        Text="Gerar PDF" Checked="true" />
                                    </div>

                                <div class="col-2 gx-4 gy-2">
                                    <asp:RadioButton ID="chk_CSV" runat="server" Cssclass="texto form-check-label border-0 bg-transparent" AutoPostBack="true" GroupName="g3"
                                        Text="Gerar CSV" />
                                </div>
                               
                                <div class="ms-4 gx-4 gy-2 mb-2">
                                  <asp:CheckBox ID="chk_AntiHBS" runat="server" Cssclass="texto form-check-label border-0 bg-transparent"  AutoPostBack="True" 
                                    Checked="True" Text="Exibir Anti-HBS" />
                                </div>
                           
                            </div>
                        </div>
                    </div>
                </tr>
        
        <tr>
            <td>
                <b>
                    <asp:button ID="btnemp" runat="server" 
                        Text="Gerar Relatório" CssClass="btn">
                    </asp:button>
                </b>
            </td>
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
</asp:Content>