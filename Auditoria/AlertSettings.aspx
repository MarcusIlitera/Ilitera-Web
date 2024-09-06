<%@ Page Language="C#"   AutoEventWireup="True"  MasterPageFile="~/Site.Master" CodeBehind="AlertSettings.aspx.cs"  Inherits="Ilitera.Net.AlertSettings" Title="Ilitera.Net" %>


<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>


<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
  <link href="css/forms.css" rel="stylesheet" type="text/css" />
  <link href="css/tabelas.css" rel="stylesheet" type="text/css" />
  <style type="text/css">
 .defaultFont
    {
        width: 627px;
    }
        .table
        {}

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
        <div class="row gx-3 gy-2">
    <script language="javascript">
        function callme(oFile) {
            document.getElementById("txt_Arq").value = oFile.value;
        }
    </script>

<%--
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Mestra</title>
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="scripts/style.css" type="text/css" rel="stylesheet">
	</HEAD>--%>
<%--	<body bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0">
		<form method="post" runat="server">--%>

			<div class="col-11 subtituloBG mb-4" style="padding-top: 10px" >
                <asp:Label id="lblExCli" 
					runat="server" class="subtitulo">Gerar Alertas de Auditoria</asp:Label>
            </div>
			
			<div class="col-11">
				<div class="row">

						<div class="col-md-4 gx-3 gy-2">
                             <asp:Label ID="lblSelecioneAuditoria" runat="server" Text="Selecione a Auditoria" CssClass="tituloLabel form-label"></asp:Label>
                             <asp:DropDownList ID="ddlAuditoria" onselectedindexchanged="ddlAuditoria_SelectedIndexChanged" runat="server" AutoPostBack="True" CssClass="texto form-select"></asp:DropDownList>
                        </div>

					    <div class="col-md-3 ms-4 me-2 gx-3 gy-2">
							<asp:checkbox id="ckbService" oncheckedchanged="ckbService_CheckedChanged" runat="server" CssClass="texto form-check-label border-0 bg-transparent" AutoPostBack="True"
								Text="Habilitar serviço de alerta para com os autos de infração não regularizados até a Data de Previsão de Regularização"></asp:checkbox>
						</div>

						<div class="col-md-4 gx-3 gy-2">
							<div class="row">
								<div class="col-12">
									<asp:label id="lblInicioServico" runat="server" CssClass="tituloLabel form-label">Início do envio do e-mail de alerta</asp:label>
								</div>
								<div class="col-2">
									<asp:textbox id="wneDias" runat="server" CssClass="texto form-control form-control-sm" ImageDirectory=" " Nullable="False" HorizontalAlign="Center"></asp:textbox>
								</div>
								<div class="col-10 gx-3 mt-1">
									<asp:label id="lblDiasAntes" runat="server" CssClass="texto gx-3">dia(s) antes da Data de Previsão</asp:label>
								</div>
							</div>
						</div>

					  <div class="col-md-4 gx-2 gy-2">
						  <asp:label id="lblPeriodicidade" runat="server" CssClass="tituloLabel form-label">Periodicidade do envio do e-mail</asp:label>
						  <asp:radiobuttonlist id="rblPeriodicidade" runat="server" CssClass="texto form-check-label ms-4 gx-4 border-0 bg-transparent" Width="150px" CellPadding="0"
						  CellSpacing="0" RepeatDirection="Horizontal">
						  <asp:ListItem Value="1">Diário</asp:ListItem>
						  <asp:ListItem Value="2">Semanal</asp:ListItem>
						  </asp:radiobuttonlist>
					  </div>

					  <div class="gx-3 gy-2">
                        <asp:button ID="btnGravarServico"  onclick="btnGravarServico_Click" runat="server" Text="Gravar" CssClass="btn mt-4 gy-2 gx-2">
                        </asp:button>
                      </div>
						
					     
						<div class="gx-3 gy-2">
                           <asp:label id="lblSelecioneEmail" runat="server" CssClass="texto form-check-label border-0 bg-transparent" Width="500px">Selecione ou cadastre os e-mails que irão também receber o alerta enviado ao responsável da Regularização</asp:label><BR>
						</div>

						<div class="col-md-5 gx-3 gy-2">
							<asp:Label ID="lblColaboradores" runat="server" Text="Colaboradores" CssClass="tituloLabel form-label"></asp:Label>
							<asp:ListBox ID="lbxColaboradores" runat="server" Height="106px" AutoPostBack="True" CssClass="texto form-control"></asp:ListBox>
					   </div>
							
					<div class="col-2 gx-5 gy-2 text-center">
						<div class="row">
							<div class="col-12 gy-1">
								<asp:button id="btnAdicionarSelecionado" onclick="btnAdicionarSelecionado_Click" runat="server" CssClass="btnMenor" Width="40px" Text=">" ToolTip="Adiciona o Colaborador selecionado" ></asp:button>
							</div>

							<div class="col-12 gy-1">
								<asp:button id="btnAdicionarTodos" onclick="btnAdicionarTodos_Click" runat="server" CssClass="btnMenor" Width="40px" Text=">>" ToolTip="Adiciona todos os Colaboradores" ></asp:button>
							</div>

							<div class="col-12 gy-1">
								<asp:button id="btnRemoverSelecionado" onclick="btnRemoverSelecionado_Click" runat="server" CssClass="btnMenor" Width="40px" Text="<" ToolTip="Remove e-mail selecionado" ></asp:button>
							</div>

							<div class="col-12 gy-1">
								<asp:button id="btnRemoverTodos" onclick="btnRemoverTodos_Click" runat="server" CssClass="btnMenor" Width="40px" Text="<<" ToolTip="Remove todos os e-mails"></asp:button>
							</div>
						</div>
					</div>
			
							<div class="col-md-5 gx-3 gy-2">
								<asp:label id="lblEmails" runat="server" CssClass="tituloLabel form-label">E-mails selecionados</asp:label>
							    <asp:listbox id="lbxEmails" runat="server" Height="106px" AutoPostBlack="True" CssClass="texto form-control"></asp:listbox>
							</div>

					<div class="col-md-5 gx-3 gy-2 mt-3">
						<asp:label id="lblEmail" runat="server" CssClass="tituloLabel form-label">E-mail</asp:label>
						<asp:textbox id="txtEmail" runat="server" CssClass="texto form-control form-control-sm"></asp:textbox>
					</div>

					<div class="col-md-2 gx-3" style="margin-top: 38px;">
						<asp:button id="btnAdicionarEmail" runat="server" CssClass="btnMenor" Width="120px" Text="Adicionar e-mail"></asp:button>
					</div>
			
			</div>
		</div>
<%--		</form>
	</body>--%>
<%--</HTML>--%>

			</div>
		    </div>


    <eo:MsgBox ID="MsgBox1" runat="server" BackColor="#ffffff" ControlSkinID="None" HeaderHtml="Dialog Title" Height="192px" Width="345px" CssClass="card border border-1 p-2 text-center" IconUrl="Images/alerta_icon.svg">
            <HeaderStyleActive CssText="font-family: Ubuntu; font-size: 14px; font-weight: bold; color: #1C9489; text-align: center; padding: 5px;" />
            <ContentStyleActive CssText="font-family: Ubuntu; font-size: 14px; font-weight: bold; color: #7D7B7B; text-align: center; padding: 5px; width: 345px; height: 60px" />
            <FooterStyleActive CssText="width: 345px;" />
        </eo:MsgBox>
        
</asp:Content>