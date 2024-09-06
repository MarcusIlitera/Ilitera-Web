<%@ Page Language="C#"   AutoEventWireup="True"  MasterPageFile="~/Site.Master" CodeBehind="Relatorio_GHE_Funcao_Exames.aspx.cs"  Inherits="Ilitera.Net.PCMSO.Relatorio_GHE_Funcao_Exames" Title="Ilitera.Net" %>
 

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
        #MainContent_chk_PDF
        {
            margin-left: 20px !important;
            margin-right: 6px !important;
        }
        #MainContent_chk_CSV
        {
            margin-left: 20px !important;
            margin-right: 6px !important;
        }
    </style>
</asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent" >
    <div class="container-fluid d-flex ms-5 ps-4">
	<div class="row gx-3 gy-2 vw-100">


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
            <asp:Label runat="server" class="subtitulo">Relatórios GHE - Função - Exames</asp:Label>
        </div>
      
	<table class="normalFont" id="Table1" cellspacing="0" cellpadding="0" width="1000"
				style="border: 1px solid silver; border-radius: 4px !important; border-collapse: initial !important; width: 91.5%;">
               
            <tr>
                    <td>
                        <div class="col-2 ms-4">
                        <br />
                        <b> <asp:Label ID="Label6" runat="server" Text="Dados do relatório" Cssclass="tituloLabel form-label col-form-label col-form-label-sm" Style="text-align: left;" ></asp:Label>
                             <asp:Literal runat="server"><br /></asp:Literal>
                            <asp:DropDownList ID="cmb_Empresa" runat="server" Cssclass="texto form-select form-select-sm" Style="text-align: left;"
                            Height="32px" Width="182px">
                        <asp:ListItem Value="1">Empresa selecionada</asp:ListItem>
                        <asp:ListItem Value="2">Empresa selecionada e projetos</asp:ListItem>
                        <asp:ListItem Value="3">Todas empresas do grupo</asp:ListItem>
                    </asp:DropDownList>
                            <asp:Textbox ID="txt_Data" runat="server" CssClass="inputBox" EditModeFormat="dd/MM/yyyy" HorizontalAlign="Center" 
                                ImageDirectory=" " Nullable="False" ToolTip="Data de Início" Width="75px" Visible="False"></asp:Textbox>
                            <asp:Textbox ID="txt_Data2" runat="server" CssClass="inputBox" EditModeFormat="dd/MM/yyyy" HorizontalAlign="Center" ImageDirectory=" " 
                                Nullable="False" ToolTip="Data de Início" Width="75px" Visible="False"></asp:Textbox>
                                                         

</td>
                                                         

                <tr>
                <td>
                
                    <div class="row ms-4">
                     <div class="col-md-2 ms-3 mb-3">
                    <asp:RadioButton ID="chk_PDF" oncheckedchanged="chk_PDF_CheckedChanged"  runat="server"  Text="Gerar PDF"  Cssclass="texto form-check-label border-0 bg-transparent" Style="text-align: left;" Checked="true" 
                        AutoPostBack="true"
                        GroupName="g3"/>
                         </div>
                         
           
                     <div class="col-md-2 ms-2 mb-3">
                    <asp:RadioButton ID="chk_CSV" oncheckedchanged="chk_CSV_CheckedChanged" runat="server" Text="Gerar CSV"  Cssclass="texto form-check-label border-0 bg-transparent" Style="text-align: left;"  Checked="true" 
                        AutoPostBack="true" 
                        GroupName="g3"/>
                         </div>
                </div>
                    </td>
               

                <tr>
                <td>
                

           
                </td>
                </tr>

                     </tr>

        <eo:MsgBox ID="MsgBox1" runat="server" BackColor="#47729F" ControlSkinID="None" 
        HeaderHtml="Dialog Title" Height="100px" Width="168px">
        <HeaderStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #ffbf00 3px solid; padding-left: 4px; font-weight: bold; font-size: 11px; padding-bottom: 2px; color: white; padding-top: 2px; border-bottom: #22456a 1px solid; font-family: verdana" />
        <ContentStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #7d97b6 1px solid; padding-left: 4px; border-left-width: 1px; font-size: 11px; border-left-color: #728eb8; padding-bottom: 4px; color: white; padding-top: 4px; border-bottom: #22456a 1px solid; font-family: verdana" />
        <FooterStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #7d97b6 1px solid; padding-left: 4px; border-left-width: 1px; font-size: 11px; border-left-color: #728eb8; padding-bottom: 4px; color: white; padding-top: 4px; border-bottom: #22456a 1px solid; font-family: verdana" />
        </eo:MsgBox>
            </table>
        <div class="col-md-1 ms-1 gx-1 mb-1">
                    <br />
                    <asp:button ID="btnemp" runat="server" onclick="btnemp_Click" 
                        Text="Gerar Relatório" CssClass="btn">
                    </asp:button>
                    </div>
</div>                         
 </div>
       


<%--		</form>
	</body>
</HTML>
--%>



    </asp:Content>