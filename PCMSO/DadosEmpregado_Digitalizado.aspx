<%@ Page Language="C#"   AutoEventWireup="True"  MasterPageFile="~/Site.Master" CodeBehind="DadosEmpregado_Digitalizado.aspx.cs"  Inherits="Ilitera.Net.PCMSO.DadosEmpregado_Digitalizado" Title="Ilitera.Net" %>



<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>


<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">

	 <link href="css/forms.css" rel="stylesheet" type="text/css" />
    <link href="css/tabelas.css" rel="stylesheet" type="text/css" />

    <style type="text/css">
    .defaultFont
    {
        width: 627px;
    }
        #Table4
        {
            width: 594px;
        }
        #Table15
        {
            width: 214px;
        }
        #Table20
        {
            width: 184px;
        }
        #Table24
        {
            width: 333px;
        }
        #Table27
        {
            width: 583px;
        }
    </style>
</asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div class="container-fluid d-flex ms-5 ps-4">
        <div class="row gx-3 gy-2 w-100">

    <script language="javascript">
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
                <asp:Label runat="server" class="subtitulo">Dados do Empregado</asp:Label>
            </div>

	        <div class="col-12 mb-3">
                <div class="row">
         <%--LINHA UM--%>
                  <div class="col-md-4 gx-3 gy-2">
                        <fieldset>
                            <asp:Label ID="lblNome" runat="server" Text="Nome" class="tituloLabel form-label col-form-label col-form-label-sm" Style="text-align: left;"></asp:Label>
                            <asp:Literal runat="server"><br /></asp:Literal>
                            <asp:Label ID="lblValorNome" runat="server" class="texto form-control form-control-sm" Style="text-align: left;"></asp:Label>
                        </fieldset>
                   </div>

                  <div class="col-md-2 gx-3 gy-2">
                    <fieldset>
                        <asp:Label ID="lblDataNascimento" runat="server" Text="Nascimento" CssClass="tituloLabel form-label col-form-label col-form-label-sm"></asp:Label>
                        <asp:Literal runat="server"><br /></asp:Literal>
                        <asp:Label ID="lblValorNasc" runat="server" CssClass="texto form-control form-control-sm" type="date"></asp:Label>
                    </fieldset>
                   </div>

                  <div class="col-md-1 gx-3 gy-2">
                    <fieldset>
                        <asp:Label ID="lblIdade" runat="server" Text="Idade" class="tituloLabel form-label col-form-label col-form-label-sm" Style="text-align: left;"></asp:Label>
                        <asp:Literal runat="server"><br /></asp:Literal>
                        <asp:Label ID="lblValorIdade" runat="server" class="texto form-control form-control-sm" Style="text-align: left;"></asp:Label>
                                                                    <asp:Label ID="lblTipoGuia" runat="server" Text="1" Visible="False"></asp:Label>
                    </fieldset>
                   </div>

                    <div class="col-md-2 gx-3 gy-2">
                    <fieldset>
                        <asp:Label ID="lblSexo" runat="server" Text="Sexo" class="tituloLabel form-label col-form-label col-form-label-sm" Style="text-align: left;"></asp:Label>
                        <asp:Literal runat="server"><br /></asp:Literal>
                        <asp:Label ID="lblValorSexo" runat="server" class="texto form-control form-control-sm" Style="text-align: left;"></asp:Label>
                    </fieldset>
                  </div>

                   <div class="col-md-2 gx-3 gy-2">
                      <fieldset>
                        <asp:Label ID="lblDataIni" runat="server" Text="Início da Função" CssClass="tituloLabel form-label col-form-label col-form-label-sm"></asp:Label>
                        <asp:Literal runat="server"><br /></asp:Literal>
                        <asp:Label ID="lblValorDataIni" runat="server" CssClass="texto form-control form-control-sm" text="27/03/2015" type="date"></asp:Label>
                    </fieldset>
                  </div>

                    <%--LINHA DOIS--%>
                  <div class="col-md-2 gx-3 gy-2">
                    <fieldset>
                        <asp:Label ID="lblAdmissao" runat="server" Text="Admissão" CssClass="tituloLabel form-label col-form-label col-form-label-sm"></asp:Label>
                        <asp:Literal runat="server"><br /></asp:Literal>
                        <asp:Label ID="lblValorAdmissao" runat="server" CssClass="texto form-control form-control-sm" type="date"></asp:Label>
                    </fieldset>
                  </div>

                  <div class="col-md-2 gx-3 gy-2">
                      <fieldset>
                        <asp:Label ID="lblDemissao" runat="server" Text="Demissão" CssClass="tituloLabel form-label col-form-label col-form-label-sm"></asp:Label>
                        <asp:Literal runat="server"><br /></asp:Literal>
                        <asp:Label ID="lblValorDemissao" runat="server" CssClass="texto form-control form-control-sm" type="date"></asp:Label>
                    </fieldset>
                  </div>

                    <%--LINHA TRÊS--%>
                   <div class="col-12 gx-3 gy-2">
                       <div class="row"> 
                     <div class="col-md-4 ps-0"> 
                      <fieldset>
                    <asp:Label runat="server" CssClass="tituloLabel form-label" Height="13">Prontuários Digitais</asp:Label>
                        <asp:DropDownList ID="cmb_Clinicas" AutoPostBack="true" onselectedindexchanged="cmb_Clinicas_SelectedIndexChanged" runat="server"  CssClass="texto form-select form-select-sm">
                        </asp:DropDownList>
                      </fieldset>
                     </div>
                       </div>
                      
                       </div>
				   </div>
	        </div>

            <div class="col-12">
                <asp:Button ID="cmd_Voltar" onclick="cmd_Voltar_Click" runat="server" CssClass="btn" Text="Voltar" Width="132px" />
                <asp:Label ID="lbl_Path" runat="server" Visible="False"></asp:Label>
            </div>
                   
                    
                    <caption>
                        <input id="txtIdUsuario" type="text" visible="False"  style="visibility:hidden"  />
                        <input id="txtIdEmpregado" type="text" visible="False" style="visibility:hidden" />
                        <input id="txtIdEmpresa" type="text" visible="False" style="visibility:hidden"/>
                    </caption>
                    
            
                    
                        

            <asp:button ID="btnemp" onclick="btnemp_Click"  runat="server" Text="Imprimir" Visible="False" Width="100px"></asp:button>
            <asp:DropDownList ID="cmb_Imagem" onselectedindexchanged="cmb_Clinicas_SelectedIndexChanged"  runat="server" AutoPostBack="True" Font-Size="XX-Small" Visible="False" Width="400px"></asp:DropDownList>
            <asp:Button ID="cmd_PDF" OnClick="cmd_PDF_Click"  runat="server" Text="Abrir PDF" BackColor="#FFCC99" Font-Bold="True" Font-Size="X-Small" Font-Strikeout="False" Visible="False" />
            <asp:Button ID="cmd_Imagem" OnClick="cmd_Imagem_Click"  runat="server" Text="Abrir Imagem" BackColor="#FFCC99" Font-Bold="True" Font-Size="X-Small" Font-Strikeout="False" Visible="False" />
            <asp:Image ID="ImgFunc" runat="server" BorderColor="#660033" BorderStyle="Inset" BorderWidth="2px" Height="600px" Width="430px" Visible="False" />
		 </div>
		 </div>

<%--		</form>
	</body>
</HTML>
--%>


    </asp:Content>