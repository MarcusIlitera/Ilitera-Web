<%@ Page Language="C#"  AutoEventWireup="true" CodeBehind="ListaEmpregadoCurso.aspx.cs" Inherits="Ilitera.Net.Treinamentos.ListaEmpregadoCurso" Title="Ilitera.Net" %>

<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>


<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Ilitera.NET</title>
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<link href="https://cdn.jsdelivr.net/npm/bootstrap@5.2.3/dist/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
		<LINK href="scripts/style.css" type="text/css" rel="stylesheet">
		<link href="css/forms.css" rel="stylesheet" type="text/css" />
		<link href="css/tabelas.css" rel="stylesheet" type="text/css" />
		<script language="javascript" src="scripts/validador.js"></script>
		<style type="text/css">
            .img-printer{
				width: 20px;
				height: 20px;
				background-color: #7D7B7B;
				border-radius: 50%;
				padding: 5px;
            }

			, texto-link {
				color: #7D7B7B;
			}
				.texto-link:hover{
					color: #1C9489;
					text-decoration: none;
				}	
		</style>
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0">
		<form id="ListaCursoEmpregado" method="post" runat="server">
			<div class="container d-flex ms-5 ps-4 justify-content-center">
				<div class="row gx-3 gy-2 mt-3 text-center" style="width: 400px">
					
					<div class="col-12 mb-3">
						<div class="row">
							<div class="col-12 gx-1 gy-2">
								<asp:label id="lblCurso" runat="server" CssClass="largeboldFont"></asp:label>
								<asp:ListBox id="ltbEmpregadoCurso" runat="server" CssClass="texto form-control form-control-sm" Rows="13" SelectionMode="Multiple"></asp:ListBox>
							</div>

							<div class="col-12 gx-2 gy-3 text-center">
								<asp:linkbutton id="lkbListagemParticipantes" runat="server" CssClass="btn" onclick="lkbListagemParticipantes_Click"><img src="Images/printer.svg" border="0"></img>Listagem de Participantes</asp:linkbutton>
							</div>
						</div>
					</div>

					<div class="col-12 subtituloBG text-center">
						<asp:label id="Label1" runat="server" CssClass="subtitulo" style="margin-left: 0px !important">Certificados</asp:label>
					</div>

					<div class="col-12 text-center gy-2">
						<asp:panel id="pcertificado" runat="server" Width="370px" CssClass="texto form-control form-control-sm p-4">
							<asp:LinkButton id="lkbColetivo" runat="server" CssClass="btn" onclick="lkbColetivo_Click"><img src="Images/printer.svg" border="0"></img> Modelo Coletivo</asp:LinkButton>
							<asp:LinkButton id="lkbIndividual" runat="server" CssClass="btn" onclick="lkbIndividual_Click"><img src="Images/printer.svg" border="0"></img> Modelo Individual</asp:LinkButton>
						</asp:panel>
					</div>

					<div class="col-12 text-center">
						<asp:label id="Label2" runat="server" Width="380px" CssClass="texto">Obs: Para imprimir o modelo individual, selecione um ou mais participantes. Se caso nenhum for selecionado, o certificado individual será gerado para todos os participantes.</asp:label>
					</div>

				</div>
			</div>



			

 <eo:msgbox ID="MsgBox1" runat="server" BackColor="#47729F" ControlSkinID="None" 
        HeaderHtml="Dialog Title" Height="100px" Width="168px">
        <HeaderStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #ffbf00 3px solid; padding-left: 4px; font-weight: bold; font-size: 11px; padding-bottom: 2px; color: white; padding-top: 2px; border-bottom: #22456a 1px solid; font-family: verdana" />
        <ContentStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #7d97b6 1px solid; padding-left: 4px; border-left-width: 1px; font-size: 11px; border-left-color: #728eb8; padding-bottom: 4px; color: white; padding-top: 4px; border-bottom: #22456a 1px solid; font-family: verdana" />
        <FooterStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #7d97b6 1px solid; padding-left: 4px; border-left-width: 1px; font-size: 11px; border-left-color: #728eb8; padding-bottom: 4px; color: white; padding-top: 4px; border-bottom: #22456a 1px solid; font-family: verdana" />
        </eo:msgbox>

		</form>
	</body>
</HTML>


<%--              </Template>
            </ig:SplitterPane>
        </Panes>
    </ig:WebSplitter>

    </asp:Content>--%>