<%@ Page Language="C#"   AutoEventWireup="True"  MasterPageFile="~/Site.Master" CodeBehind="Relatorio_Guias.aspx.cs"  Inherits="Ilitera.Net.PCMSO.Relatorio_Guias" Title="Ilitera.Net" %>


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
            <asp:Label ID="lblNome" runat="server" class="subtitulo">Relatórios de Guias Geradas</asp:Label>
        </div>
	<table class="normalFont" id="Table1" cellspacing="0" cellpadding="0" width="1000"
				border="0">
				
                <tr>
                    
                       <div class="col-12">
                            <div class="row" style="border: 1px solid silver; width: 91.5%; border-radius: 4px;">
                                <div class="col-2 me-2 gx-4 gy-2">
                                    <asp:Label runat="server" CssClass="tituloLabel form-label col-form-label col-form-label-sm mb-1">Data início</asp:Label>
                                    <asp:Textbox ID="txt_Data" runat="server" 
                                        CssClass="inputBox texto form-control form-control-sm" EditModeFormat="dd/MM/yyyy" HorizontalAlign="Center" 
                                        ImageDirectory=" " Nullable="False" ToolTip="Data de Início">
                                    </asp:Textbox>
                                </div>
                                <div class="col-2 me-4 gx-4 gy-2">
                                    <asp:Label runat="server" CssClass="tituloLabel form-label col-form-label col-form-label-sm">Data final</asp:Label>
                                    <asp:Textbox ID="txt_Data2" runat="server" CssClass="inputBox texto form-control form-control-sm" 
                                        EditModeFormat="dd/MM/yyyy" HorizontalAlign="Center" ImageDirectory=" " 
                                        Nullable="False" ToolTip="Data de Início">
                                    </asp:Textbox>
                                    </div>
                                <div class="col-4 me-4 gx-4 gy-2">
                                    <asp:Label runat="server" CssClass="tituloLabel form-label" Height="13">Empresa Selecionada </asp:Label>
                                    <asp:DropDownList ID="cmb_Empresa" runat="server" CssClass="texto form-select form-select-sm">
                                        <asp:ListItem Value="1">Empresa selecionada</asp:ListItem>
                                        <asp:ListItem Value="2">Empresa selecionada e projetos</asp:ListItem>
                                        <asp:ListItem Value="3">Todas empresas do grupo</asp:ListItem>
                                    </asp:DropDownList>
                                </div>

                                
                <div class="row mt-3">
                                    <div class="col-2 ms-4 gx-4 gy-2">
                                        <asp:RadioButton ID="chk_PDF" runat="server"  Text="Gerar PDF"  Cssclass="texto form-check-label border-0 bg-transparent" Style="text-align: left;" Checked="true" 
                                            AutoPostBack="true"
                                            GroupName="g3"/>
                                    </div>
                                    <div class="col-2 gy-2">
                                         <asp:RadioButton ID="chk_CSV" runat="server" Text="Gerar CSV"  Cssclass="texto form-check-label border-0 bg-transparent" Style="text-align: left;"  Checked="true" 
                                             AutoPostBack="true" 
                                             GroupName="g3"/>
                                    </div>
                                    <div class="col-2 gy-2">
                                        <asp:RadioButton ID="rd_Analitico" oncheckedchanged="rd_Analitico_CheckedChanged" runat="server" Text="Analítico" CssClass="texto form-check-label border-0 bg-transparent" Style="text-align: left" Checked="true"
                                            AutoPostBack="true"
                                            GroupName="g1" />
                                    </div>
                                   <div class="col-2 gy-2">
                                        <asp:RadioButton ID="rd_Sumarizado" oncheckedchanged="rd_Sumarizado_CheckedChanged" runat="server" Text="Sumarizado" CssClass="texto form-check-label border-0 bg-transparent" Style="text-align: left" Checked="true"
                                            AutoPostBack="true"
                                            GroupName="g1" />
                                    </div>
                                   <div class="col-10 ms-4  gx-4 gy-2 mb-2">
                                        <asp:RadioButton ID="chk_Associados" runat="server" Text="Somente guias associadas à exames." CssClass="texto form-check-label border-0 bg-transparent" Style="text-align: left" Checked="true"
                                            AutoPostBack="true"/>
                                    </div>
                                </div>
               
                    <br />

               
                    <br />

               <tr>
                <td>
                    <b>
                        <div>
                        <asp:button ID="btnemp" runat="server"  onclick="btnemp_Click" Text="Gerar Relatório"  CssClass="btn mt-4 gy-2 gx-2">
                        </asp:button>
                            </div>
                    </b>
                </td>
                </tr>
                <caption>
                    <input id="txtIdEmpresa" type="text" visible="False" style="visibility:hidden"/>
                </caption>
                </tr>                                   
                                          


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