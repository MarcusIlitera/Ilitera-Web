<%@ Page language="c#" Inherits="Ilitera.Net.PCMSO.CadastroUniformes" Codebehind="CadastroUniformes.aspx.cs" Title="Ilitera.Net"%>

<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<link href="https://cdn.jsdelivr.net/npm/bootstrap@5.2.3/dist/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
        <link href="css/forms.css" rel="stylesheet" type="text/css" />
        <link href="css/tabelas.css" rel="stylesheet" type="text/css" />
		<title>Ilitera.NET</title>
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<LINK href="scripts/style.css" type="text/css" rel="stylesheet">
		<script language="JavaScript" src="scripts/validador.js"></script>

      

	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0">
		<form method="post" runat="server" id="frmCadastroExtintores">
			<div class="container d-flex ms-5 ps-4 justify-content-center">
				<div class="row gx-3 gy-2 text-center" style="width: 450px">

					<div class="col-12 subtituloBG" style="padding-top: 10px; margin-top: 40px;">
						<asp:Label ID="lblTitulo" runat="server" SkinID="TitleFont" CssClass="subtitulo"></asp:Label>
					</div>

					<div class="col-12">
						<div class="row">
							<div class="col-5 gx-2 gy-4 mt-2 text-start">
								<asp:Label ID="Label1" runat="server" CssClass="tituloLabel">Uniforme</asp:Label>
								<asp:textbox id="txt_Uniforme" runat="server"  CssClass="texto form-control form-control-sm"></asp:textbox>
							</div>
							<div class="col-5 gx-2 gy-3 mt-2 text-start">
								<asp:label id="Label11" runat="server" CssClass="tituloLabel">Obs.</asp:label>
								<asp:textbox id="txt_Obs" runat="server" CssClass="texto form-control form-control-sm"></asp:textbox>
							</div>
						    <div class="col-2 gx-2 gy-3 mt-2 text-start">
								<asp:label id="Label10" runat="server" CssClass="tituloLabel">Medida</asp:label>
								<asp:textbox id="txt_Medida" runat="server" CssClass="texto form-control form-control-sm"></asp:textbox>
							</div>
						</div>
					</div>

					<div class="col-12 text-start">
						<div class="row">
							<asp:label id="Label12" runat="server" CssClass="tituloLabel ps-1">Periodicidade Troca</asp:label>
							<div class="row">
								<div class="col-2 gx-2">
									<asp:textbox id="txtIntervalo" runat="server" CssClass="texto form-control form-control-sm" MaxLength="3"></asp:textbox>
								</div>
								
								<div class="col-4">
									<asp:dropdownlist id="cmbPeriodicidade" runat="server" CssClass="texto form-select form-select-sm">
										<asp:ListItem Value="0">Dia(s)</asp:ListItem>
										<asp:ListItem Value="1">M&#234;s(s)</asp:ListItem>
										<asp:ListItem Value="2">Ano(s)</asp:ListItem>
									</asp:dropdownlist>
								</div>
							</div>
						</div>
					</div>


					<div class="col-12">
						<div class="row">
							<div class="col-4 text-start gx-2 gy-1">
								<asp:label id="Label2" runat="server" CssClass="tituloLabel">Tamanho</asp:label>
								<asp:textbox id="txt_Tamanho" runat="server" CssClass="texto form-control form-control-sm" 
									MaxLength="20"></asp:textbox>     
							</div>
							<div class="col-4 text-start gx-2 gy-1">
								<asp:label id="Label3" runat="server" CssClass="tituloLabel">Obs</asp:label>
								<asp:textbox id="txt_Obs_Tamanho" runat="server" CssClass="texto form-control form-control-sm" 
									MaxLength="50"></asp:textbox>   
							</div>
							<div class="col-4 mt-2 pt-3 gx-2 gy-1">
								<asp:button id="btnAdd" runat="server" CssClass="btn2" Text="Add" onclick="btnAdd_Click"></asp:button>  
							</div>
						</div>
						<div class="row">
							<div class="col-8 gx-2 gy-1">
								<asp:ListBox ID="lst_Tamanho" runat="server" 
								CssClass="texto form-control form-control-sm" ></asp:ListBox>   
							</div>
							<div class="col-4 gx-2 gy-1">
								<asp:button id="btnRemove" runat="server" CssClass="btn2" Text="Remove" onclick="btnRemove_Click"></asp:button>  
							</div>
						</div>
					</div>
					
					<div class="col-12">
						<div class="row">

							<div class="col-8 text-start gx-2 gy-1">
								<asp:label id="Label4" runat="server" CssClass="tituloLabel">EPIs Relacionados</asp:label>
								<asp:dropdownlist id="ddlEPI" runat="server" CssClass="texto form-select form-select-sm" AutoPostBack="True"></asp:dropdownlist>  
							</div>
							<div class="col-4 mt-2 pt-3 gx-2 gy-1">
								<asp:button id="cmd_add2" runat="server" CssClass="btn2" Text="Add" onclick="cmd_Add2_Click"></asp:button> 
							</div>
						</div>
						<div class="row">
							<div class="col-8 gx-2 gy-1">
								<asp:ListBox ID="lst_EPI" runat="server" CssClass="texto form-control form-control-sm"></asp:ListBox>  
							</div>
							<div class="col-4 gx-2 gy-1">
								<asp:button id="cmd_Remove2" runat="server" CssClass="btn2" Text="Remove" onclick="cmd_Remove2_Click"></asp:button>
								<asp:ListBox ID="lst_EPI_ID" runat="server" Height="16px" Visible="False"></asp:ListBox> 
							</div>
						</div>
					</div>

						<div class="text-center mt-1 mb-3">
							<asp:button id="btnGravar" runat="server" CssClass="btn2" Text="Gravar" onclick="btnGravar_Click"></asp:button>
							<asp:button id="btnExcluir" runat="server" CssClass="btn2" Text="Excluir" onclick="btnExcluir_Click"></asp:button>
							<asp:button id="btnFechar" runat="server" CssClass="btn2" Text="Fechar"></asp:button>
                        </div>

					<input id="txtCloseWindow" type="hidden"   runat="server"/>
                           
				<INPUT id="txtAuxiliar" type="hidden" name="txtAuxiliar" runat="server">

				</div>
			</div>
		</form>
	</body>
</HTML>