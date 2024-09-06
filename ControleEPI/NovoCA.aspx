<%@ Page Language="C#"   AutoEventWireup="True"  MasterPageFile="~/Site.Master" CodeBehind="NovoCA.aspx.cs"  Inherits="Ilitera.Net.ControleEPI.NovoCA" Title="Ilitera.Net" %>


<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>


<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
	<link href="css/forms.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
    .defaultFont
    {
        width: 627px;
    }
    </style>
</asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent" >

    <script language="javascript">


		<!-- #include file="scripts/validador.js" -->
		function VerificaData()
		{
			if (VerificaEmi() && VerificaVal())
				return true;
			else
				return false;
		}
		
		function VerificaEmi()
		{
			return validar_data(document.forms[0].txtdde.value, document.forms[0].txtmme.value, document.forms[0].txtaae.value, 'Data de Emissão do CA');
		}

		function VerificaVal()
		{
			return validar_data(document.forms[0].txtddv.value, document.forms[0].txtmmv.value, document.forms[0].txtaav.value, 'Data de Validade do CA');
		}
		
		function Inicialize()
		{
			document.forms[0].txtCA.focus();
			document.forms[0].txtdde.onkeypress = ChecarTAB;
			document.forms[0].txtdde.onfocus = PararTAB;
			document.forms[0].txtdde.onkeyup = DiaE;
			document.forms[0].txtmme.onkeypress = ChecarTAB;
			document.forms[0].txtmme.onfocus = PararTAB;
			document.forms[0].txtmme.onkeyup = MesE;
			document.forms[0].txtaae.onkeypress = ChecarTAB;
			document.forms[0].txtaae.onfocus = PararTAB;
			document.forms[0].txtaae.onkeyup = AnoE;
			document.forms[0].txtddv.onkeypress = ChecarTAB;
			document.forms[0].txtddv.onfocus = PararTAB;
			document.forms[0].txtddv.onkeyup = DiaV;
			document.forms[0].txtmmv.onkeypress = ChecarTAB;
			document.forms[0].txtmmv.onfocus = PararTAB;
			document.forms[0].txtmmv.onkeyup = MesV;
			document.forms[0].txtaav.onkeypress = ChecarTAB;
			document.forms[0].txtaav.onfocus = PararTAB;
			document.forms[0].txtaav.onkeyup = AnoV;
		}
		
		function DiaE()
		{
			if (document.forms[0].txtdde.value.length == 2 && VerifiqueTAB == true)
				document.forms[0].txtmme.focus();
		}
		
		function MesE()
		{
			if (document.forms[0].txtmme.value.length == 2 && VerifiqueTAB == true)
				document.forms[0].txtaae.focus();
		}
		
		function AnoE()
		{
			if (document.forms[0].txtaae.value.length == 4 && VerifiqueTAB == true)
				document.forms[0].txtddv.focus();
		}
		
		function DiaV()
		{
			if (document.forms[0].txtddv.value.length == 2 && VerifiqueTAB == true)
				document.forms[0].txtmmv.focus();
		}
		
		function MesV()
		{
			if (document.forms[0].txtmmv.value.length == 2 && VerifiqueTAB == true)
				document.forms[0].txtaav.focus();
		}
		
		function AnoV()
		{
			if (document.forms[0].txtaav.value.length == 4 && VerifiqueTAB == true)
				document.forms[0].txtObjetivo.focus();
		}

		</script>
        
       
<%--
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" onload="Inicialize()" rightMargin="0">
		<form method="post" runat="server">
--%>			



	<div class="container-fluid d-flex ms-5 ps-4">
        <div class="row gx-3 gy-2 w-100">

			
			<div class="col-11 subtituloBG mb-3" style="padding-top: 10px;">
				<asp:label id="lblNovoCA" runat="server" CssClass="subtitulo"> Novo CA</asp:label>
			</div>

			<div class="col-11 mb-3">
				<div class="row">
					<div class="col-md-2 gx-3 gy-2">
						<asp:label id="lblCA" runat="server" CssClass="tituloLabel">Número do CA</asp:label>
						<asp:textbox id="txtCA" runat="server" CssClass="texto form-control"></asp:textbox>
					</div>

					<div class="col-md-2 gx-3" style="margin-top: 28px;">
						<asp:button id="btnTudo" onclick="btnTudo_Click" runat="server" CssClass="btn" Text="Consultar CA's"></asp:button>
					</div>

					<div class="col-md-6 gy-2">
						<asp:label id="lblFabricante" runat="server" CssClass="tituloLabel">Fabricante</asp:label>
						<asp:dropdownlist id="ddlFabricante" runat="server" CssClass="texto form-select" AutoPostBack="True" CausesValidation="True"></asp:dropdownlist>
					</div>

					<div class="col-md-2 gx-3" style="margin-top: 28px;">
						<asp:HyperLink ID="hlkNovo" runat="server" SkinID="BoldLink" CssClass="btn">Novo Fabricante</asp:HyperLink>
					</div>

					<div class="col-md-6 gx-3 gy-2">
						<asp:label id="lblTipoEquipamento" runat="server" CssClass="tituloLabel">Tipo do Equipamento</asp:label>
						<asp:dropdownlist id="ddlEquipamento" runat="server" CssClass="texto form-select"></asp:dropdownlist>
					</div>

					<div class="col-md-3 gx-3 gy-2">
						<asp:label id="lblEmissaoCA" runat="server" CssClass="tituloLabel">Data de Emissão do CA</asp:label>
						<div class="row">
							<div class="col-md-2">
								<asp:textbox id="txtdde" runat="server" CssClass="texto form-control" MaxLength="2"></asp:textbox>
							</div>

							<div class="col-md-1 text-center">
								<asp:label id="lblbarra" runat="server" CssClass="texto">/</asp:label>
							</div>

							<div class="col-md-2">
								<asp:textbox id="txtmme" runat="server" CssClass="texto form-control" MaxLength="2"></asp:textbox>
							</div>

							<div class="col-md-1 text-center">
								<asp:label id="Label1" runat="server" CssClass="texto">/</asp:label>
							</div>

							<div class="col-md-4">
								<asp:textbox id="txtaae" runat="server" CssClass="texto form-control" MaxLength="4"></asp:textbox>
							</div>
						</div>
					</div>

					<div class="col-md-3 gx-3 gy-2">
						<asp:label id="lblValidadeCA" runat="server" CssClass="tituloLabel">Validade do CA</asp:label>
						<div class="row">
							<div class="col-md-2">
								<asp:textbox id="txtddv" runat="server" CssClass="texto form-control" MaxLength="2"></asp:textbox>
							</div>

							<div class="col-md-1 text-center">
								<asp:label id="Label3" runat="server" CssClass="texto">/</asp:label>
							</div>

							<div class="col-md-2">
								<asp:textbox id="txtmmv" runat="server" CssClass="texto form-control" MaxLength="2"></asp:textbox>
							</div>

							<div class="col-md-1 text-center">
								<asp:label id="Label2" runat="server" CssClass="texto">/</asp:label>
							</div>

							<div class="col-md-4">
								<asp:textbox id="txtaav" runat="server" CssClass="texto form-control" MaxLength="4"></asp:textbox>
							</div>
						</div>
					</div>

					<div class="col-12 gx-3 gy-2">
						<asp:label id="lblFuncaoEqui" runat="server" CssClass="tituloLabel">Objetivo do Equipamento</asp:label>
						<asp:textbox id="txtObjetivo" runat="server" CssClass="texto form-control" TextMode="MultiLine"></asp:textbox>
					</div>

					<div class="col-12 gx-3 gy-2">
						<asp:label id="lblDescricao" runat="server" CssClass="tituloLabel">Descrição do Equipamento</asp:label>
						<asp:textbox id="txtDescricao" runat="server" CssClass="texto form-control" TextMode="MultiLine" Rows="4"></asp:textbox>
					</div>
				</div>
			</div>

			<div class="col-11">
				<div class="text-center">
					<asp:button id="btnGravar" onclick="btnGravar_Click"  runat="server" CssClass="btn" Text="Gravar"></asp:button>
					<asp:button id="lblCancel" onclick="lblCancel_Click" runat="server" CssClass="btn2" Text="Cancela"></asp:button>
				</div>
			</div>


			<asp:button id="btnProcurarCA" onclick="btnProcurarCA_Click"  runat="server" CssClass="buttonBox" Text="Pesquisar" Visible="False"></asp:button>
			<asp:textbox id="txtCATudo" runat="server" CssClass="inputBox" Width="50px"  Visible="False"></asp:textbox>
			<asp:button id="btnNovoFabricante" onclick="btnNovoFabricante_Click" runat="server" CssClass="buttonBox" Text="..." Visible="False"></asp:button>
			<asp:button id="btnNovoEquipamento" onclick="btnNovoEquipamento_Click" runat="server" CssClass="buttonBox" Text="..." Visible="False"></asp:button>
			<asp:label id="lblError" runat="server" CssClass="errorFont"></asp:label>
			<asp:Label ID="lbl_Id_Empresa" runat="server" Text="IdEmpresa" Visible="False"></asp:Label>
			<asp:Label ID="lbl_Id_Usuario" runat="server" Text="IdUsuario" Visible="False"></asp:Label>				

         <eo:MsgBox ID="MsgBox1" runat="server" BackColor="#47729F" ControlSkinID="None" 
        HeaderHtml="Dialog Title" Height="100px" Width="168px">
        <HeaderStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #ffbf00 3px solid; padding-left: 4px; font-weight: bold; font-size: 11px; padding-bottom: 2px; color: white; padding-top: 2px; border-bottom: #22456a 1px solid; font-family: verdana" />
        <ContentStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #7d97b6 1px solid; padding-left: 4px; border-left-width: 1px; font-size: 11px; border-left-color: #728eb8; padding-bottom: 4px; color: white; padding-top: 4px; border-bottom: #22456a 1px solid; font-family: verdana" />
        <FooterStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #7d97b6 1px solid; padding-left: 4px; border-left-width: 1px; font-size: 11px; border-left-color: #728eb8; padding-bottom: 4px; color: white; padding-top: 4px; border-bottom: #22456a 1px solid; font-family: verdana" />
        </eo:MsgBox>

		</div>
	</div>


</asp:Content>

