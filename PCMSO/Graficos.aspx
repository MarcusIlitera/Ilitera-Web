<%@ Page Language="C#"   AutoEventWireup="True"  MasterPageFile="~/Site.Master" CodeBehind="Graficos.aspx.cs"  Inherits="Ilitera.Net.PCMSO.Graficos" Title="Ilitera.Net" %>

<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>


<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <meta http-equiv="Content-Security-Policy" content="upgrade-insecure-requests">
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
            width: 813px;
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
            width: 817px;
        }

        [type="button"], [type="reset"], [type="submit"], button {
            min-width: 120px;
            height: 32px;
            font-family: 'Univia Pro' !important;
            font-style: normal;
            font-weight: normal !important;
            font-size: 12px;
            /*text-align: center;*/
            color: #ffffff !important;
            background: linear-gradient(180deg, #48A79E 54.35%, #1C9489 54.36%);
            border-radius: 5px;
            border: none;
            margin-right: 10px;
            margin-bottom: 5px;
            margin-top: 20px;
        }

            [type="button"]:hover, [type="reset"]:hover, [type="submit"]:hover, button:hover {
                color: #ffffff !important;
                background: linear-gradient(180deg, #F2B988 53.35%, #F09E60 53.36%);
                border-radius: 5px;
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
            <div class="col-11 subtituloBG"  style="padding-top: 10px">
                <asp:Label id="Label1" runat="server" CssClass="subtitulo">Indicadores</asp:Label>
			    <asp:panel id="Panel2" runat="server" Width="662px"></asp:panel>
		    </div>
            <div class="col-11 mb-2">
				<div class="row gx-2 gy-2">
                    <div class="col-md-2 me-3">
                        <asp:Label ID="Label2" runat="server" SkinID="BoldFont" CssClass="tituloLabel mb-2">Tipo de Indicador</asp:Label>
						<asp:RadioButtonList ID="rd_Considerar" runat="server" Width="492px" Height="100px"
                            CssClass="texto form-check-input bg-transparent border-0 ms-3" RepeatColumns="1" AutoPostBack="True" OnSelectedIndexChanged="rd_Considerar_SelectedIndexChanged">
                                <asp:ListItem Value="3">Anamnese</asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                </div>
		    </div>
            <div class="col-11">
				<div class="row gx-2 gy-2">
                    <div class="col-md-1 me-3">
                        <asp:Label ID="Label3" runat="server" SkinID="BoldFont" CssClass="tituloLabel mb-2">Data inicial</asp:Label>
                        <asp:Textbox ID="txt_Data" runat="server" 
                            CssClass="texto form-control" EditModeFormat="dd/MM/yyyy" HorizontalAlign="Center" 
                            ImageDirectory=" " Nullable="False" ToolTip="Data de Início" >
                        </asp:Textbox>
                    </div>
                    <div class="col-md-1 me-3">
                        <asp:Label ID="Label4" runat="server" SkinID="BoldFont" CssClass="tituloLabel mb-2">Data final</asp:Label>
                        <asp:Textbox ID="txt_Data2" runat="server" CssClass="texto form-control" 
                            EditModeFormat="dd/MM/yyyy" HorizontalAlign="Center" ImageDirectory=" " 
                            Nullable="False" ToolTip="Data de Início">
                        </asp:Textbox>
                    </div>
                    <div class="col-md-2 me-3">
                        <asp:Label ID="Label5" runat="server" SkinID="BoldFont" CssClass="tituloLabel mb-2">Considerar</asp:Label>
                                <asp:DropDownList ID="cmb_Empresa" runat="server" CssClass="texto form-select form-select-sm" AutoPostBack="True">
                                    <asp:ListItem Value="1">Empresa selecionada</asp:ListItem>
                                    <asp:ListItem Value="2">Empresa selecionada e projetos</asp:ListItem>
                                    <asp:ListItem Value="3">Todas empresas do grupo</asp:ListItem>
                                </asp:DropDownList>
                    </div>

                    <div class="col-md-2 me-3">
                        <asp:Label ID="lblAnamneses" runat="server" CssClass="tituloLabel mb-2">Tipos de Anamneses</asp:Label>
                        <asp:DropDownList ID="cmb_Anamneses" runat="server" CssClass="texto form-select form-select-sm mb-2" AutoPostBack="True">
                            <asp:ListItem Value="0">Antecedentes Familiares</asp:ListItem>
                            <asp:ListItem Value="1">Exame Físico</asp:ListItem>
                            <asp:ListItem Value="2">Anamnese - 1</asp:ListItem>
                            <asp:ListItem Value="3">Anamnese - 2</asp:ListItem>
                            <asp:ListItem Value="4">Anamnese - 3</asp:ListItem>
                        </asp:DropDownList>

               
                        <asp:DropDownList ID="cmb_Anamnese_Dados" runat="server" CssClass="texto form-select form-select-sm" AutoPostBack="True">
                            <asp:ListItem Value="0">Valor</asp:ListItem>
                            <asp:ListItem Value="1">Porcentagem</asp:ListItem>
                        </asp:DropDownList>
                    </div>

                    <div class="col-1 me-3">
                        <asp:button ID="btnemp" runat="server" Text="Carregar Gráfico" CssClass="btn mt-3" onclick="btnemp_Click"></asp:button>
                    </div>
                </div>

                <table>
                    <tr>

                        <td> 
                             <div  id="div1"> 
                              <asp:Literal ID="Literal1" runat="server"></asp:Literal>
                            </div>           
                        </td>

                        <td>                        
                            <div  id="div2">
                            <asp:Literal ID="Literal2" runat="server"></asp:Literal>
                                </div>
                        </td>

                        <td>                        
                            <div  id="div3">
                            <asp:Literal ID="Literal3" runat="server"></asp:Literal>
                                </div>
                        </td>

                    </tr>

                                <tr>

                        <td> 
                             <div  id="div4"> 
                              <asp:Literal ID="Literal4" runat="server"></asp:Literal>
                            </div>           
                        </td>

                        <td>                        
                            <div  id="div5">
                            <asp:Literal ID="Literal5" runat="server"></asp:Literal>
                                </div>
                        </td>

                        <td>                        
                            <div  id="div6">
                            <asp:Literal ID="Literal6" runat="server"></asp:Literal>
                                </div>
                        </td>

                    </tr>

                                <tr>

                        <td> 
                             <div  id="div7"> 
                              <asp:Literal ID="Literal7" runat="server"></asp:Literal>
                            </div>           
                        </td>

                        <td>                        
                            <div  id="div8">
                            <asp:Literal ID="Literal8" runat="server"></asp:Literal>
                                </div>
                        </td>

                        <td>                        
                            <div  id="div9">
                            <asp:Literal ID="Literal9" runat="server"></asp:Literal>
                                </div>
                        </td>

                    </tr>

                       <caption>
                           <asp:Label ID="lblNome" runat="server" CssClass="tituloLabel" SkinID="TitleFont">Indicadores</asp:Label>
                       </caption>

            </table>
		    </div>

	

        <eo:MsgBox ID="MsgBox1" runat="server" BackColor="#ffffff" ControlSkinID="None" HeaderHtml="Dialog Title" Height="192px" Width="345px" CssClass="card border border-1 p-2 text-center" IconUrl="Images/alerta_icon.svg">
            <HeaderStyleActive CssText="font-family: Ubuntu; font-size: 14px; font-weight: bold; color: #1C9489; text-align: center; padding: 5px;" />
            <ContentStyleActive CssText="font-family: Ubuntu; font-size: 14px; font-weight: bold; color: #7D7B7B; text-align: center; padding: 5px; width: 345px; height: 60px" />
            <FooterStyleActive CssText="width: 345px;" />
        </eo:MsgBox>


<%--		</form>
	</body>
</HTML>
--%>

            </div>
        </div>

    </asp:Content>