<%@ Page Language="C#"   AutoEventWireup="True"  MasterPageFile="~/Site.Master" CodeBehind="Relatorio_PCD.aspx.cs"  Inherits="Ilitera.Net.PCMSO.Relatorio_PCD" Title="Ilitera.Net" %>

 

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
            <asp:Label ID="Label1" runat="server" class="subtitulo">Relatórios PCD</asp:Label>
        </div>

	<table class="normalFont" id="Table1" cellspacing="0" cellpadding="0" width="1000"
				border="0">
				
               
                  
                      <div class="col-12 mt-3">
                           <div class="row gx-4 gy-2" style="border: 1px solid silver; width: 91.5%; border-radius: 4px;">
                               <Span class="tituloLabel gx-4">Período</Span>
                               <div class="col-2 me-2 gx-4 gy-2">
                           <asp:Label id="Label12" runat="server" CssClass="tituloLabel form-label col-form-label col-form-label-sm mb-1">Mês</asp:Label>
                             <asp:DropDownList ID="cmb_Mes" runat="server" CssClass="texto form-select form-select-sm">
                             <asp:ListItem Value="1">Janeiro</asp:ListItem>
                                <asp:ListItem Value="2">Fevereiro</asp:ListItem>
                                <asp:ListItem Value="3">Março</asp:ListItem>
                                <asp:ListItem Value="4">Abril</asp:ListItem>
                                <asp:ListItem Value="5">Maio</asp:ListItem>
                                <asp:ListItem Value="6">Junho</asp:ListItem>
                                <asp:ListItem Value="7">Julho</asp:ListItem>
                                <asp:ListItem Value="8">Agosto</asp:ListItem>
                                <asp:ListItem Value="9">Setembro</asp:ListItem>
                                <asp:ListItem Value="10">Outubro</asp:ListItem>
                                <asp:ListItem Value="11">Novembro</asp:ListItem>
                                <asp:ListItem Value="12">Dezembro</asp:ListItem>
                                 </asp:DropDownList>
                                   </div>
                                <div class="col-1 me-4">
                                   <asp:Label runat="server" CssClass="tituloLabel form-label col-form-label col-form-label-sm mb-1">Ano</asp:Label>
                                    <asp:DropDownList id="cmb_Ano" runat="server" 
                                        CssClass="inputBox texto form-select" EditModeFormat="dd/MM/yyyy" HorizontalAlign="Center" 
                                        ImageDirectory=" " Nullable="False" ToolTip="Data de Início">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-4 me-4">
                                    <asp:Label id="Label3" runat="server" CssClass="tituloLabel form-label" Height="13">Empresa Selecionanda </asp:Label>
                                    <asp:DropDownList ID="cmb_Empresa" runat="server" CssClass="texto form-select form-select-sm">
                                        <asp:ListItem Value="1">Empresa selecionada</asp:ListItem>
                                        <asp:ListItem Value="2">Empresa selecionada e projetos</asp:ListItem>
                                        <asp:ListItem Value="3">Todas empresas do grupo</asp:ListItem>
                                    </asp:DropDownList>
                                </div>

                                    <div class="col-1 mt-3 ms-4 pt-3">
                                      <asp:Textbox ID="txt_Data" runat="server" 
                                        CssClass="inputBox" EditModeFormat="dd/MM/yyyy" HorizontalAlign="Center" 
                                        ImageDirectory=" " Nullable="False" ToolTip="Data de Início" Width="75px" 
                                        Visible="False"></asp:Textbox>
                                    <asp:Textbox ID="txt_Data2" runat="server" CssClass="inputBox" 
                                        EditModeFormat="dd/MM/yyyy" HorizontalAlign="Center" ImageDirectory=" " 
                                        Nullable="False" ToolTip="Data de Início" Width="75px" Visible="False"></asp:Textbox>
                                        <asp:CheckBox ID="chk_Ativos" runat="server" Cssclass="texto form-check-label border-0 bg-transparent" 
                         AutoPostBack="True" Checked="True" 
                         Text="Ativos" />
                                         </div>
                                    <div class="col-2 mt-3 gx-1 pt-3">
                                         <asp:CheckBox ID="chk_Inativos" runat="server" Cssclass="texto form-check-label border-0 bg-transparent" 
                         AutoPostBack="True" Checked="True" 
                         Text="Inativos" />
                                   </div>

                                <div class="row mt-3">
                                    <div class="col-2 ms-4 gx-4 gy-2">
                                       <asp:RadioButton ID="rd_Analitico" oncheckedchanged="rd_Analitico_CheckedChanged"  runat="server" Text="Analítico"  Cssclass="texto form-check-label border-0 bg-transparent" Style="text-align: left;"
                        AutoPostBack="True" Checked="True" GroupName="g1" />
                                        </div>
                 
                                    <div class="col-2 gy-2">
                                       <asp:RadioButton ID="rd_Sumarizado" oncheckedchanged="rd_Sumarizado_CheckedChanged" runat="server" Text="Sumarizado" Cssclass="texto form-check-label border-0 bg-transparent" Style="text-align: left;"
                        AutoPostBack="True" GroupName="g1" />
                                    </div>   
                                   </div>
                                  

                                   <div class="col-2 ms-4 mb-2 gx-4 gy-3">
                                      <asp:RadioButton ID="chk_PDF" oncheckedchanged="chk_PDF_CheckedChanged"  runat="server"  Cssclass="texto form-check-label" Text="Gerar PDF" Checked="true" AutoPostBack="true" GroupName="g3"/>
                                   </div>
                                  
                                  <div class="col-2 col-md-1 mb-2 gy-3">
                                    <asp:RadioButton ID="chk_CSV" oncheckedchanged="chk_CSV_CheckedChanged"  runat="server" Cssclass="texto form-check-label" Text="Gerar CSV" AutoPostBack="true" GroupName="g3"/>
                                  </div>
                        </div>
                          </div>
                                   
           
                   
&nbsp;&nbsp;&nbsp;
                  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <b>
            
        
                    <br />
                    <asp:button ID="btnemp" runat="server" onclick="btnemp_Click" 
                        Text="Gerar Relatório" CssClass="btn">
                    </asp:button>
                    </b>
               
               
   
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