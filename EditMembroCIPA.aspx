<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditMembroCIPA.aspx.cs" Inherits="Ilitera.Net.EditMembroCIPA" %>

<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Ilitera.NET</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<link href="https://cdn.jsdelivr.net/npm/bootstrap@5.2.3/dist/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
        <link href="css/forms.css" rel="stylesheet" type="text/css" />
        <link href="css/tabelas.css" rel="stylesheet" type="text/css" />
		<script src="https://ajax.googleapis.com/ajax/libs/jquery/3.7.1/jquery.min.js"></script>
		<link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.11.3/font/bootstrap-icons.min.css" />
		<link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" />
		<script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js"></script>
	</HEAD>
	<style type="text/css">
        @font-face {
            font-family: 'UniviaPro';
            src: url('css/css/UniviaPro-Regular.otf') format('opentype');
            font-weight: normal;
            font-style: normal;
        }

        /* Agora, você pode usar a fonte em qualquer lugar no CSS */
        body {
            font-family: 'UniviaPro', sans-serif;
        }
		.nav-bg{
			background-color: #F1F1F1;
		}
			.nav-bg:hover, .nav-bg:active{
				background-color: transparent;
			}

		.nav-text{
			color: #7D7B7B !important;
			font-size: 12px;
			font-family: 'Ubuntu' !important;
		}

		.nav-tabs .nav-item.show .nav-link, .nav-tabs .nav-link.active{
			border-color: #1C9489 !important;
			border-width: 1.5px;
			border-bottom: #f1f1f1 !important;
		}

		.nav-tabs{
			--bs-nav-tabs-border-color: #1C9489 !important;
			--bs-nav-tabs-border-width: 1.5px !important;
			  background-color: #f1f1f1;
		}

		.nav-pills .nav-link.active, .nav-pills .show > .nav-link{
			color: #1C9489 !important;
			font-size: 12px;
			font-family: 'Ubuntu' !important;
			background-color: #D9D9D9 !important;
		}

		.nav-pills .nav-link{
			color: #7D7B7B !important;
			font-size: 12px;
			font-family: 'Ubuntu' !important;
			background-color: #f1f1f1 !important;
		}

        .radiobtn{
            font-family: 'UniviaPro', sans-serif !important;
            font-weight: 300 !important;
            font-size: 12px !important;
        }
	</style>
	<body>
		<form id="frmAddReuniao" method="post" runat="server">
		    <div class="container d-flex ms-5 ps-4 justify-content-center">
                <div class="row gx-3 gy-2 mt-3" style="width: 772px">
                    <div class="col-12 subtituloBG text-center pt-2">
						<asp:label id="lblMembrosCIPA" runat="server" CssClass="subtitulo" style="margin-left: 0px !important;font-family: 'UniviaPro', sans-serif !important;font-size: 16px !important;  font-weight: 500 !important;">Editar Membro CIPA</asp:label>
					</div>
                    <div class="col-12 gx-2 gy-2">
                        <div class="row">
                            <div class="col-5">
                                <div class="row">
                                    <div class="col-12 gx-2 gy-1">
                                        <asp:Label ID="lblNome" runat="server" class="tituloLabel form-label col-form-label col-form-label-sm" style="font-size: .8rem;font-weight: 300 !important;">Nome</asp:Label>
                                        <asp:TextBox ID="txtNome" runat="server" class="texto form-control form-control-sm" Enabled="False" style="font-family: 'UniviaPro', sans-serif !important;font-weight: 300 !important;font-size: 12px !important;"></asp:TextBox>                                        
                                    </div>

                                    <div class="col-12 gx-2 gy-1">
                                        <asp:Label ID="lblCargo" runat="server" class="tituloLabel form-label col-form-label col-form-label-sm" style="font-size: .8rem;font-weight: 300 !important;">Cargo</asp:Label>
                                        <asp:TextBox ID="txtCargo" runat="server" class="texto form-control form-control-sm" Enabled="False" style="font-family: 'UniviaPro', sans-serif !important;font-weight: 300 !important;font-size: 12px !important;"></asp:TextBox> 
                                    </div>

                                    <div class="col-12 gx-2 gy-1">
                                        <asp:Label ID="lblEstabilidade" runat="server" class="tituloLabel form-label col-form-label col-form-label-sm" style="font-size: .8rem;font-weight: 300 !important;">Estabilidade</asp:Label>
                                        <asp:TextBox ID="txtEstabilidade" runat="server" class="texto form-control form-control-sm" Enabled="False" style="font-family: 'UniviaPro', sans-serif !important;font-weight: 300 !important;font-size: 12px !important;"></asp:TextBox>
                                    </div>

                                    <div class="col-12 gx-2 gy-1">
                                        <asp:Label runat="server" Text="Status" CssClass="tituloLabel form-label col-form-label col-form-label-sm" style="font-size: .8rem;font-weight: 300 !important;"></asp:Label>
                                        <asp:RadioButtonList ID="rbStatus" runat="server" RepeatColumns="3" tabIndex="1" CssClass="texto form-control-sm ms-3 radiobtn" Width="300">
                                            <asp:ListItem Value="1" >Ativo</asp:ListItem>
                                            <asp:ListItem Value="2">Afastado</asp:ListItem>
                                            <asp:ListItem Value="3">Renunciou</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                </div>
                            </div>

                            <div class="col-1 text-center">
                            </div>

                            <div class="col-6 mt-1">
                                <asp:Label ID="lblEmpregados" runat="server" class="tituloLabel form-label col-form-label col-form-label-sm" style="font-family: 'UniviaPro', sans-serif !important;font-weight: 300 !important;font-size: 13px !important;"></asp:Label>
                                <asp:ListBox ID="lstEmpregados" runat="server" class="texto form-control form-control-sm" style="height: 70%; font-family: 'UniviaPro', sans-serif !important;font-weight: 300 !important;font-size: 12px !important;" ></asp:ListBox>
                                <div class="d-flex justify-content-center mt-2">
                                    <asp:Button ID="btnConfirmar" runat="server" CssClass="btn" Text="Confirmar" OnClick="btnConfirmar_Click" style="font-family: 'UniviaPro', sans-serif !important;font-weight: 300 !important;font-size: 13px !important;"/>
                                </div>
                            </div>
                            
                        </div>
                    </div>
                <eo:MsgBox ID="MsgBox1" runat="server" BackColor="#ffffff" ControlSkinID="None" HeaderHtml="Dialog Title" Height="192px" Width="345px" CssClass="card border border-1 p-2 text-center" IconUrl="Images/alerta_icon.svg">
                    <HeaderStyleActive CssText="font-family: Ubuntu; font-size: 14px; font-weight: bold; color: #1C9489; text-align: center; padding: 5px;" />
                    <ContentStyleActive CssText="font-family: Ubuntu; font-size: 14px; font-weight: bold; color: #7D7B7B; text-align: center; padding: 5px; width: 345px; height: 60px" />
                    <FooterStyleActive CssText="width: 345px;" />
                </eo:MsgBox>
                </div>
            </div>
	    </form>
	</body>
</HTML>


