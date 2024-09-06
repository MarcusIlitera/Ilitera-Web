<%@ Page Language="C#"   AutoEventWireup="True"  MasterPageFile="~/Site.Master" CodeBehind="Relatorio_Empregados.aspx.cs"  Inherits="Ilitera.Net.PCMSO.Relatorio_Empregados" Title="Ilitera.Net" %>

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
        .auto-style1 {
            width: 762px;
        }

        #MainContent_chk_Ativos, #MainContent_chk_Historico, #MainContent_chk_Deficiencia, #MainContent_chk_eMail,
        #MainContent_chk_Inconsistencias, #MainContent_opt_SemGHE, #MainContent_opt_Encerre, #MainContent_opt_SemCPF,
        #MainContent_chk_PDF, #MainContent_chk_CSV{
            margin-left: 0px !important;
            margin-right: 5px !important;
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
        <asp:Label ID="Label5" runat="server" CssClass="subtitulo" Text="Relatório de Colaboradores"></asp:Label>
    </div>

	<table class="auto-style1" id="Table1" cellspacing="0" cellpadding="0" border="0">

        <div class="col-12">
            <div class="row" style="border: 1px solid silver; width: 91.5%; border-radius: 4px;">

                <div class="col-12">
                    <div class="row">
                        <div class="col-4 gx-4 gy-2">
                            <asp:Label ID="Label6" runat="server" CssClass="tituloLabel form-label form-label-sm" Text="Dados do relatório"></asp:Label>
                            <asp:Literal runat="server"><br /></asp:Literal>
                            <asp:DropDownList ID="cmb_Empresa" runat="server" CssClass="texto form-select form-select-sm" Height="32px">
                                <asp:ListItem Value="1">Empresa selecionada</asp:ListItem>
                                <asp:ListItem Value="2">Empresa selecionada e projetos</asp:ListItem>
                                <asp:ListItem Value="3">Todas empresas do grupo</asp:ListItem>
                            </asp:DropDownList>
                        </div>

                        <div class="col-4 gy-2">
                            <asp:Label runat="server" CssClass="tituloLabel form-label form-label-sm" Text="Considerar Colaboradores"></asp:Label>
                            <asp:Literal runat="server"><br /></asp:Literal>
                            <asp:DropDownList ID="cmb_Status" runat="server" CssClass="texto form-select form-select-sm" Height="32px">
                                <asp:ListItem Value="1">Ativos</asp:ListItem>
                                <asp:ListItem Value="2">Inativos</asp:ListItem>
                                <asp:ListItem Value="3">Todos</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>

                <div class="col-12">
                    <div class="row">
                        <div class="col-2 ms-4 gx-4 gy-3">
                            <asp:CheckBox ID="chk_Ativos" OnCheckedChanged="chk_Ativos_CheckedChanged"  runat="server" CssClass="texto form-check-label bg-transparent border-0" AutoPostBack="True" Text="Ativos em" />
                        </div>
                        <div class="col-2 gy-2">
                            <asp:Textbox ID="txt_Ativos" runat="server" CssClass="texto form-control form-control-sm" EditModeFormat="dd/MM/yyyy" HorizontalAlign="Center" 
                                ImageDirectory=" " Nullable="False" ToolTip="Data de Início" Height="32px" Enabled="False"></asp:Textbox>
                        </div>
                    </div>
                </div>

                <div class="col-12">
                    <div class="row">
                        <div class="col-3 ms-4 gx-4 gy-3">
                            <asp:CheckBox ID="chk_Historico" runat="server" CssClass="texto form-check-label bg-transparent border-0" Checked="false" Text="Exibir histórico de Funções" />
                        </div>

                        <div class="col-3 gy-3">
                            <asp:CheckBox ID="chk_Deficiencia" runat="server" CssClass="texto form-check-label bg-transparent border-0" Checked="false" Text="Exibir apenas portador deficiência" />
                        </div>

                        <div class="col-5 gy-3 gx-4">
                              <asp:CheckBox ID="chk_Classif" runat="server" CssClass="texto form-check-label bg-transparent border-0" Checked ="false" Text="Exibir apenas com Classif.Funcional Aberta na empresa/obra selecionada" />
                        </div>

                        <div class="col-3 ms-4 gx-4 gy-3">
                            <asp:CheckBox ID="chk_eMail" runat="server" CssClass="texto form-check-label bg-transparent border-0" Checked="false" Text="Exibir apenas sem e-mail" />
                        </div>
                    </div>
                </div>

                <div class="col-12 ms-4 gx-4 gy-3">
                    <asp:CheckBox ID="chk_Inconsistencias" runat="server" CssClass="texto form-check-label bg-transparent border-0" Checked="false" Text="Inconsistências:" />
                </div>

                <div class="col-12">
                    <div class="row">
                        <div class="col-4 ms-4 gx-4 gy-3">
                            <asp:RadioButton ID="opt_SemGHE" runat="server" Checked="false" CssClass="texto form-check-label bg-transparent border-0" Text="Sem GHE na classif.funcional mais recente" GroupName="Inconsistencia" />
                        </div>

                        <div class="col-3 gy-3">
                            <asp:RadioButton ID="opt_Classif" runat="server" Checked="false" CssClass="texto form-check-label bg-transparent border-0" Text="Sem Classificação Funcional" GroupName="Inconsistencia" />
                        </div>

                        <div class="col-4 gy-3">
                            <asp:RadioButton ID="opt_Encerre" runat="server" Checked="false" CssClass="texto form-check-label bg-transparent border-0" Text="Classif.Funcional encerrada sem data demissão" GroupName="Inconsistencia" />
                        </div>

                        <div class="col-1 ms-4 gx-4 gy-3">
                            <asp:RadioButton ID="opt_SemCPF" runat="server" Checked="false" CssClass="texto form-check-label bg-transparent border-0" Text="Sem CPF" GroupName="Inconsistencia" />
                        </div>
                    </div>
                </div>

                <div class="col-12 mb-2">
                    <div class="row">
                        <div class="col-2 ms-4 gx-4 gy-3">
                            <asp:RadioButton ID="chk_PDF" oncheckedchanged="chk_PDF_CheckedChanged"  runat="server" CssClass="texto form-check-label bg-transparent border-0" Text="Gerar PDF" Checked="true" AutoPostBack="true" GroupName="g3"/>
                        </div>

                        <div class="col-2 gy-3">
                            <asp:RadioButton ID="chk_CSV" oncheckedchanged="chk_CSV_CheckedChanged"  runat="server" CssClass="texto form-check-label bg-transparent border-0" Text="Gerar CSV" AutoPostBack="true" GroupName="g3"/>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        
        
                        <asp:Textbox ID="txt_Data" runat="server" 
                            CssClass="inputBox" EditModeFormat="dd/MM/yyyy" HorizontalAlign="Center" 
                            ImageDirectory=" " Nullable="False" ToolTip="Data de Início" Width="75px" 
                            Visible="False"></asp:Textbox>
                        <asp:Textbox ID="txt_Data2" runat="server" CssClass="inputBox" 
                            EditModeFormat="dd/MM/yyyy" HorizontalAlign="Center" ImageDirectory=" " 
                            Nullable="False" ToolTip="Data de Início" Width="75px" Visible="False"></asp:Textbox>
                   
      <div class="col-12">
          <asp:button ID="btnemp" onclick="btnemp_Click"  runat="server" CssClass="btn" Text="Gerar Relatório"></asp:button>
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