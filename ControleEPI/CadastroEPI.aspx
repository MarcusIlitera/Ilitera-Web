<%@ Page Language="C#"   AutoEventWireup="True"  MasterPageFile="~/Site.Master" CodeBehind="CadastroEPI.aspx.cs"  Inherits="Ilitera.Net.CadastroEPI" Title="Ilitera.Net" %>


<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>


<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="HeadContent">
    <link href="css/forms.css" rel="stylesheet" type="text/css" />
    <link href="css/tabelas.css" rel="stylesheet" type="text/css" />

    <style type="text/css">
    .defaultFont
    {
        width: 627px;
    }
    </style>

</asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent" >
	<div class="container-fluid d-flex ms-5 ps-4">
      <div class="row gx-3 gy-2 w-100">

      <script language="javascript" src="../scripts/validador.js"></script>

        <script type="text/javascript">
            function VerificaCampoInt() {
                if (isNaN(document.forms[0].txtPeriodicidade.value) || isNull(document.forms[0].txtPeriodicidade.value)) {
                    window.alert("O campo Periodicidade só aceita valores numéricos!");
                    return false;
                }
                else
                    return true;
            }
        </script>

       

<%--	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0">
		<form method="post" runat="server">--%>
		  
		  
	<%-- subtitulo --%>

        <div class="col-11 subtituloBG mb-3" style="padding-top: 10px;">
            <asp:Label ID="lblCadastroEPI" runat="server" SkinID="TitleFont" CssClass="subtitulo">Vinculação dos EPI a seus Respectivos Certificados de 
                        Aprovação - CA</asp:Label>
        </div>

		<div class="col-12">
			<div class="row">

			  <div class="col-11">
                   <asp:Label ID="lblCadastroEPI0" runat="server" CssClass="texto form-label">Esta janela tem por finalidade permitir a vinculação dos EPI mencionados no PGR com os respectivos Certificados de Aprovação - CA emitidos pela autoridade governamental, bem como a informação da periodicidade de troca dos EPI considerada apropriada pela direção da empresa.</asp:Label>
				</div>

			<div class="col-11 gy-4 gx-2">
               <asp:Button ID="cmd_Listagem" runat="server" CssClass="btn" Text="Listagem EPIs / CAs cadastrados" onclick="cmd_Atualizar_Click"/>
			</div>

			<div class="col-md-4 gy-4 gx-3">
				<asp:label id="lblSelecione" runat="server" CssClass="tituloLabel form-label" >Listagem de EPI utilizados pela Empresa</asp:label>
				<asp:dropdownlist id="ddlEPI" runat="server" CssClass="texto form-select form-select-sm" AutoPostBack="True" onselectedindexchanged="ddlEPI_SelectedIndexChanged"></asp:dropdownlist>
			</div>

			</div>
		</div>

		<div class="col-12 mb-4">
			<div class="row">
                 <div class="col-md-3 gx-3 gy-2">
                      <asp:Label runat="server" CssClass="tituloLabel form-label" SkinID="BoldFont" Text="Certificado de Aprovação - CA"></asp:Label>
                      <asp:ListBox ID="lsbCAs" runat="server" CssClass="texto form-control" Rows="9" CausesValidation="True"></asp:ListBox>
                 </div>
				  

			     <div class="col-2 gx-5 gy-2 text-center mt-3">
                      <div class="row">
                       <div class="col-12 gx-4 gy-1">
                             <asp:Button id="btnAdiciona" runat="server" Enabled="True" CssClass="btnMenor" Width="40px" Text=">>" ToolTip="Adiciona todos" ImageUrl="Images/double-right.svg" Style="padding: .4rem;" onclick="btnAdiciona_Click">
                              </asp:Button>
                        </div>
                                          
			          <div class="col-12 gy-1">
                           <asp:Button id="btnRemove" runat="server" Enabled="True"  CssClass="btnMenor" Width="40px" Text="<<" ToolTip="Remover todos" ImageUrl="Images/double-left.svg" onclick="btnRemove_Click"></asp:Button>
                      </div>
                       
                  </div>
                   </div>

			 <div class="col-md-3 gx-3 gy-2">
                      <asp:Label runat="server" CssClass="tituloLabel form-label" SkinID="BoldFont" Text="CA selecionado para o EPI"></asp:Label>
                      <asp:ListBox ID="lsbCASelect" runat="server" CssClass="texto form-control" Rows="9" CausesValidation="True" AutoPostBack="true" onselectedindexchanged="lsbCASelect_SelectedIndexChanged"></asp:ListBox>
                 </div>

            <div class="col-md-3 gx-3 gy-2">
                <asp:Label runat="server" CssClass="tituloLabel form-label" SkinID="BoldFont" Text=" Periodicidade de troca do EPI"></asp:Label>
                   <div class="row">
                      <div class="col-2 me-2">
                        <asp:textbox ID="txtPeriodicidade" runat="server" CssClass="texto form-control" Rows="9" CausesValidation="True"></asp:textbox>
                      </div>
                      <div class="col-4">
                          <asp:dropdownlist id="ddlTipoPeriodicidade" runat="server" CssClass="texto form-select form-select-sm">
					          <asp:ListItem Value="0">Dia(s)</asp:ListItem>
					          <asp:ListItem Value="1">M&#234;s(s)</asp:ListItem>
					          <asp:ListItem Value="2">Ano(s)</asp:ListItem>
				          </asp:dropdownlist>
                      </div>
                   </div>
                   <div class="row">
                      <div class="col-md-12 gy-2 mt-3">
                          <asp:button id="btnGravar" runat="server" CssClass="btn" Text="Gravar Periodicidade" onclick="btnGravar_Click"></asp:button>
                      </div>
                   </div>
            </div>


			</div>
			 </div>


          <%--BOTÕES FINAIS --%>

        <div class="col-11 mt-4 gx-2 gy-4">
          <div class="row">

           <div class="text-center mb-3">
               <asp:Button ID="btnAdcionarCA" runat="server" CssClass="btn" Text="Incluir Novo CA na Lista Acima" onclick="btnAdcionarCA_Click"/>
               <asp:Button ID="btnVerCA" runat="server" CssClass="btn" Text="Ver dados do CA Selecionado" onclick="btnVerCA_Click"/>
           </div>

        </div>
          </div>
               
                                
								

                        <asp:Label ID="lbl_Id_Empresa" runat="server" Text="IdEmpresa" Visible="False"></asp:Label>
                        <asp:Label ID="lbl_Id_Usuario" runat="server" Text="IdUsuario" Visible="False"></asp:Label>
                        <INPUT id="txtAuxUpdate" type="hidden" name="txtAuxUpdate" runat="server">
<%--			</TABLE>
			
	</body>
</HTML>
--%>

        
         <eo:MsgBox ID="MsgBox1" runat="server" BackColor="#47729F" ControlSkinID="None" 
        HeaderHtml="Dialog Title" Height="100px" Width="168px">
        <HeaderStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #ffbf00 3px solid; padding-left: 4px; font-weight: bold; font-size: 11px; padding-bottom: 2px; color: white; padding-top: 2px; border-bottom: #22456a 1px solid; font-family: verdana" />
        <ContentStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #7d97b6 1px solid; padding-left: 4px; border-left-width: 1px; font-size: 11px; border-left-color: #728eb8; padding-bottom: 4px; color: white; padding-top: 4px; border-bottom: #22456a 1px solid; font-family: verdana" />
        <FooterStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #7d97b6 1px solid; padding-left: 4px; border-left-width: 1px; font-size: 11px; border-left-color: #728eb8; padding-bottom: 4px; color: white; padding-top: 4px; border-bottom: #22456a 1px solid; font-family: verdana" />
        </eo:MsgBox>

    <asp:TextBox ID="txtChave" runat="server" Width="225px" Font-Size="X-Small" Visible="false"></asp:TextBox>
    <asp:CustomValidator ID="cvdCaracteres" runat="server" ControlToValidate="txtChave" Display="Dynamic" OnServerValidate="cvdCaracteres_ServerValidate"></asp:CustomValidator>
        </div>
    </div>
            
</asp:Content>