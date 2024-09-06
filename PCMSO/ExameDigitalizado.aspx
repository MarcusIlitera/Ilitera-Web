<%@ Page Title="Ilitera.Net" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="True"
    CodeBehind="ExameDigitalizado.aspx.cs" Inherits="Ilitera.Net.ExameDigitalizado" %>

<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>


<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
	<link href="css/forms.css" rel="stylesheet" type="text/css" />
    <link href="css/tabelas.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .defaultFont
    {
        width: 586px;
            height: 20px;
        }
        
    </style>
</asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent" >
	<div class="container-fluid d-flex ms-5 ps-4">
        <div class="row gx-3 gy-2 w-100">

    <eo:CallbackPanel runat="server" id="CallbackPanel1" Triggers="">

		<LINK href="scripts/style.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="scripts/validador.js"></script>
		<script language="javascript">
            function checking(form) {
                if (form.searchFile.value != "") {
                    adres = form.searchFile.value;
                    adres = adres.toLowerCase();
                    index = adres.indexOf(".pdf");

                    if (index < 0)
                        window.alert("O documento selecionado não é de formato válido! É necessário que o arquivo esteja no formato '.pdf'!");
                }
            }
        </script>
	<%--</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0">
		<form method="post" runat="server">--%>

			<div class="col-11 subtituloBG"  style="padding-top: 10px">
				<asp:label id="Label1" runat="server" CssClass="subtitulo">Exame Digitalizado</asp:label>
				<asp:panel id="Panel2" runat="server" Width="662px"></asp:panel>
			</div>
			<div class="col-11">
				<div class="row">
					<div class="row gx-2 gy-2">
                          <div class="col-md-2 me-3">
							<asp:label id="Label2" runat="server" CssClass="tituloLabel mb-2">Data de Digitalização</asp:label>
							<asp:textbox id="wdtDataDigitalizacao" runat="server" CssClass="texto form-control form-control-sm" ReadOnly="True" HorizontalAlign="Center"
								DisplayModeFormat="dd/MM/yyyy" Nullable="False" ImageDirectory=" "></asp:textbox>
                          </div>

                          <div class="col-md-2 ms-3">
							<asp:label id="Label3" runat="server" CssClass="tituloLabel mb-2">Data do Documento</asp:label>
							<asp:textbox id="wdtDataDocumento" runat="server" CssClass="texto form-control form-control-sm" HorizontalAlign="Center"
								DisplayModeFormat="dd/MM/yyyy" Nullable="False" ImageDirectory=" "></asp:textbox>
                          </div>
                      </div>
				</div>
			</div>

				<div class="col-12 mb-2">
					<div class="row mt-2">
						<div class="col-md-6 gx-3 gy-2">
							<asp:label id="Label5" runat="server" CssClass="tituloLabel">Descrição</asp:label>
							<asp:textbox id="txtDescricao" tabIndex="1" runat="server" CssClass="texto form-control form-control-sm" 
								TextMode="MultiLine" Rows="4"></asp:textbox>
						</div>
					</div>
					<div class="row mt-2">
						<div class="col-md-6 gx-3 gy-2">
							<asp:Label ID="lblAnotacoes0" runat="server" CssClass="tituloLabel">Arquivo do Prontuário PCI :</asp:Label>
							<asp:TextBox ID="txt_Arq" runat="server" CssClass="texto form-control form-control-sm" Height="27px" 
								ReadOnly="True" Rows="4" TextMode="MultiLine"></asp:TextBox>
						</div>
					</div>
					<din class="row mt-2">
						<div class="col-md-6 gx-3 gy-2">
							<asp:label id="Label6" runat="server" CssClass="tituloLabel">Arquivo</asp:label>
							<asp:FileUpload ID="File1" runat="server" ClientIDMode="Static" CssClass="texto form-control"/>
						</div>
					</din>
				</div>
				<div class="col-11 mt-4">
					<div class="row">
						<div class="text-center mb-3">
							<asp:button id="btnGravar" onclick="btnGravar_Click" runat="server" CssClass="btn" Text="Gravar" ></asp:button>
							<asp:button id="btnExcluir" onclick="btnExcluir_Click" runat="server" CssClass="btn" Text="Excluir" ></asp:button>
						</div>

						<div class="text-start">
							<asp:Button ID="cmd_Voltar" onclick="cmd_Voltar_Click" runat="server" CssClass="btn2" Text="Voltar" />
						</div>

					</div>
				</div>


						<%--<asp:label id="Label4" runat="server" CssClass="boldFont">Tipo do Exame</asp:label><BR>--%>
						<asp:radiobuttonlist id="rblTipoExame" tabIndex="1" runat="server" CssClass="normalFont" Width="380px"
							BorderWidth="1px" RepeatColumns="3" CellPadding="0" CellSpacing="0" BorderStyle="None" Visible="false">
							<ASP:LISTITEM Value="1">Periódico</ASP:LISTITEM>
							<ASP:LISTITEM Value="3">Admissional</ASP:LISTITEM>
							<ASP:LISTITEM Value="4">Demissional</ASP:LISTITEM>
							<ASP:LISTITEM Value="5">Mudança de Função</ASP:LISTITEM>
							<ASP:LISTITEM Value="6">Retorno ao Trabalho</ASP:LISTITEM>
							<ASP:LISTITEM Value="2">Audiometria</ASP:LISTITEM>
							<ASP:LISTITEM Value="7">Complementar</ASP:LISTITEM>
							<ASP:LISTITEM Value="0" Selected="True">Outros (PCI)</ASP:LISTITEM>
						</asp:radiobuttonlist>
						
						<asp:linkbutton id="lkbVisualizar" onclick="lkbVisualizar_Click"  runat="server" CssClass="boldFont" 
                            Visible="False"><img src="img/print.gif" border="0" alt="Visualizar Exame Digitalizado">  Visualizar</asp:linkbutton>
			
<%--		</form>
	</body>
</HTML>--%>
    </eo:CallbackPanel>

        
         <eo:MsgBox ID="MsgBox1" runat="server" BackColor="#47729F" ControlSkinID="None" 
        HeaderHtml="Dialog Title" Height="100px" Width="168px">
        <HeaderStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #ffbf00 3px solid; padding-left: 4px; font-weight: bold; font-size: 11px; padding-bottom: 2px; color: white; padding-top: 2px; border-bottom: #22456a 1px solid; font-family: verdana" />
        <ContentStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #7d97b6 1px solid; padding-left: 4px; border-left-width: 1px; font-size: 11px; border-left-color: #728eb8; padding-bottom: 4px; color: white; padding-top: 4px; border-bottom: #22456a 1px solid; font-family: verdana" />
        <FooterStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #7d97b6 1px solid; padding-left: 4px; border-left-width: 1px; font-size: 11px; border-left-color: #728eb8; padding-bottom: 4px; color: white; padding-top: 4px; border-bottom: #22456a 1px solid; font-family: verdana" />
        </eo:MsgBox>
		</div>
	</div>
</asp:Content>