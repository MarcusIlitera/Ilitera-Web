<%@ Page Language="C#"   AutoEventWireup="True"  MasterPageFile="~/Site.Master" CodeBehind="Relatorio_Absenteismo.aspx.cs"  Inherits="Ilitera.Net.PCMSO.Relatorio_Absenteismo" Title="Ilitera.Net" %>

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
	<table id="Table1" cellspacing="0" cellpadding="0" width="1000">

        <div class="row">
            <div class="col-11 subtituloBG pt-2">
                <asp:Label ID="lblNome" runat="server" SkinID="TitleFont" CssClass="subtitulo">Relatório de Absenteísmo</asp:Label>
            </div>
        </div>

        <div class="row gx-3 gy-3" style="border: 1px solid silver; width: 91.5%; border-radius: 4px;">
            <div class="col-12 mt-2 gx-4 gy-4">
                <p class="tituloLabel">Data do Afastamento</p>

                <div class="row">
                    <div class="col-md-1 me-2 gx-2">
                        <fieldset>
                            <asp:Label runat="server" CssClass="tituloLabel form-label col-form-label">Início</asp:Label>
                            <asp:Textbox ID="txt_Data" runat="server" 
                                    CssClass="texto form-control form-control-sm" EditModeFormat="dd/MM/yyyy" HorizontalAlign="Center" 
                                    ImageDirectory=" " Nullable="False" ToolTip="Data de Início">
                            </asp:Textbox>
                        </fieldset>
                    </div>

                    <div class="col-md-1 gx-2">
                        <fieldset>
                            <asp:Label runat="server" CssClass="tituloLabel form-label col-form-label">Final</asp:Label>
                            <asp:Textbox ID="txt_Data2" runat="server" CssClass="texto form-control form-control-sm" 
                                    EditModeFormat="dd/MM/yyyy" HorizontalAlign="Center" ImageDirectory=" " 
                                    Nullable="False" ToolTip="Data de Início">
                            </asp:Textbox>
                        </fieldset>
                    </div>

                    <div class="col-4 gx-2 ms-4">
                        <div class="row">
                            <div class="col-12">
                                <asp:RadioButton ID="rd_Sumarizado_Empr" oncheckedchanged="rd_Sumarizado_Empr_CheckedChanged" runat="server" Text="Sumarizado por Empregado: " AutoPostBack="True" GroupName="g1" class="texto form-check-label bg-transparent border-0" />
                            </div>

                            <div class="col-11 gy-2">
                                <asp:DropDownList ID="cmb_Tipo" runat="server" CssClass="texto form-select form-select-sm" Height="32px">
                                    <asp:ListItem Value="1">Em afastamento</asp:ListItem>
                                    <asp:ListItem Value="2">Afastados com retorno</asp:ListItem>
                                    <asp:ListItem Value="3">Geral</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>

                    <div class="col-2 gx-2">
                        <asp:RadioButton ID="rd_Sumarizado" oncheckedchanged="rd_Sumarizado_CheckedChanged" runat="server" Text="Sumarizado" AutoPostBack="True" GroupName="g1" class="texto form-check-label bg-transparent border-0" />
                    </div>

                    <div class="col-2 mb-2">
                        <asp:RadioButton ID="rd_Analitico" oncheckedchanged="rd_Analitico_CheckedChanged" runat="server" Text="Analítico" AutoPostBack="True" Checked="True" GroupName="g1" class="texto form-check-label bg-transparent border-0" />
                    </div>
                </div>
            </div>

            <div class="col-12 mt-3 gx-4 gy-4">
                <div class="row">
                    

                    

                    

                    

                    <div class="col-8 mt-4 gx-2">
                        <asp:DropDownList ID="cmb_Empresa" runat="server" CssClass="texto form-select form-select-sm" Height="32px">
                            <asp:ListItem Value="1">Empresa selecionada</asp:ListItem>
                            <asp:ListItem Value="2">Empresa selecionada e projetos</asp:ListItem>
                            <asp:ListItem Value="3">Todas empresas do grupo</asp:ListItem>
                        </asp:DropDownList>
                    </div>

                    <div class="col-3 gx-2" style="margin-top: .2rem;">
                        <asp:Label runat="server" CssClass="tituloLabel form-label">Considerar Colab.</asp:Label>
                        <asp:DropDownList ID="cmb_Status" runat="server" CssClass="texto form-select" Height="32px">
                            <asp:ListItem Value="1">Ativos</asp:ListItem>
                            <asp:ListItem Value="2">Inativos</asp:ListItem>
                            <asp:ListItem Value="3">Todos</asp:ListItem>
                        </asp:DropDownList>
                    </div>

                    <div class="col-3 mt-3 gx-2">
                        <asp:Label runat="server" CssClass="tituloLabel form-label">Atestados</asp:Label>
                        <asp:DropDownList ID="cmb_Atestados" runat="server" CssClass="texto form-select" Height="32px">
                            <asp:ListItem Value="1">Todos</asp:ListItem>
                            <asp:ListItem Value="2">Com Atestados</asp:ListItem>
                            <asp:ListItem Value="3">Sem Atestados</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                
                
               
            </div>

            <div class="col-12 mt-3 gx-4 gy-4">
                <div class="row">
                    <div class="col-2 ms-4">
                        <asp:RadioButton ID="chk_PDF" oncheckedchanged="chk_PDF_CheckedChanged"  runat="server"  Text="Gerar PDF" Checked="true" AutoPostBack="true" GroupName="g2" CssClass="texto form-check-label bg-transparent border-0" />
                    </div>

                    <div class="col-2">
                        <asp:RadioButton ID="chk_CSV" oncheckedchanged="chk_CSV_CheckedChanged" runat="server" Text="Gerar CSV" AutoPostBack="true" GroupName="g2" CssClass="texto form-check-label bg-transparent border-0" />
                    </div>

                    <div class="col-8"></div>

                    <div class="col-12 mt-3 mb-3">
                        <asp:button ID="btnemp" onclick="btnemp_Click" runat="server" CssClass="btn" width="100px" Text="Gerar Relatório"></asp:button>
                    </div>
                </div>
            </div>
        </div>
                
                    
                    
                
                    <br />
                
                <tr>
                <td>
                <b>
                    <br />
                    
                    </b>
                </td>
                
                </tr>

                <tr>
                    <td>
                        <br />
                        
                         

                      
                        <br />
                        <br />
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


<%--		</form>
	</body>
</HTML>
--%>
            </div>
        </div>
    </asp:Content>