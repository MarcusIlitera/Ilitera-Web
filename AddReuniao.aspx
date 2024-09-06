<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddReuniao.aspx.cs" Inherits="Ilitera.Net.AddReuniao" %>

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
        <link rel="preconnect" href="https://fonts.googleapis.com">
        <link rel="preconnect" href="https://fonts.gstatic.com" crossorigin>
        <link href="https://fonts.googleapis.com/css2?family=Ubuntu&display=swap" rel="stylesheet">
	</HEAD>
	<style type="text/css">
        @font-face {
            font-family: 'UniviaPro';
            src: url('css/css/UniviaPro-Regular.otf') format('opentype');
            font-weight: normal;
            font-style: normal;
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
	</style>
    <script>
        function updateTxtEditarFrase() {
            var txtFraseAcidente2 = document.getElementById("<%= txtFraseAcidente2.ClientID %>");
            var txtEditarFrase = document.getElementById("<%= txtEditarFrase.ClientID %>");
            var hdnDataId = document.getElementById("<%= hdnDataId.ClientID %>");

            if (txtFraseAcidente2.selectedIndex !== -1) {
                var selectedIndex = txtFraseAcidente2.selectedIndex;
                var selectedItem = txtFraseAcidente2.options[selectedIndex];
                var selectedValue = selectedItem.value;
                txtEditarFrase.value = selectedItem.text;
                txtEditarFrase.setAttribute("data-id", selectedValue);
                hdnDataId.value = selectedValue;
            } else {
                txtEditarFrase.value = "";
                txtEditarFrase.removeAttribute("data-id");
                hdnDataId.value = "";
            }
        }
    </script>
	<body>
		<form id="frmAddReuniao" method="post" runat="server">
		<div class="container d-flex ms-5 ps-4 justify-content-center">
             <div class="row gx-3 gy-2 mt-5" style="width: 772px">
				 <div class="col-12 mb-2">
					 <div class="row">
						 <div class="col-12 subtituloBG mb-3" style="padding-top: 10px; margin-left: 0px !important;">
								<asp:Label ID="lblAddReuniao" runat="server" CssClass="subtitulo" style="margin-left: 1.2em !important;font-family: 'UniviaPro', sans-serif !important;font-size: 16px !important;  font-weight: 500 !important;">Adicionar Reunião</asp:Label>
						</div>

						 <!-- primeira parte -->
						 <div class="col-6 gx-2 gy-2">
								<asp:label id="lblSindicato" runat="server" CssClass="tituloLabel"  style="font-family: 'Ubuntu', sans-serif !important;">Sindicato</asp:label>
								<asp:textbox id="txtSindicato" runat="server" CssClass="texto form-control" Enabled="False" style="font-family: 'Ubuntu', sans-serif !important, font-size: .8rem;"></asp:textbox>
						</div>

						 <div class="col-2 gx-2 gy-2">
								<asp:label id="lblCNAE" runat="server" CssClass="tituloLabel" style="font-family: 'Ubuntu', sans-serif !important;">CNAE</asp:label>
								<asp:textbox id="txtCNAE" runat="server" CssClass="texto form-control" Enabled="False" style="font-family: 'Ubuntu', sans-serif !important, font-size: .8rem;"></asp:textbox>
						 </div>
						 <div class="col-2 gx-2 gy-2">
								<asp:label id="lblGrauRisco" runat="server" CssClass="tituloLabel" style="font-family: 'Ubuntu', sans-serif !important;">Grau de Risco</asp:label>
								<asp:textbox id="txtGrauRisco" runat="server" CssClass="texto form-control" Enabled="False" style="font-family: 'Ubuntu', sans-serif !important, font-size: .8rem;"></asp:textbox>
						</div>
						 <div class="col-2 gx-2 gy-2">
								<asp:label id="lblData" runat="server" CssClass="tituloLabel" style="font-family: 'Ubuntu', sans-serif !important;">Data</asp:label>
								<asp:Textbox id="txtData" runat="server" CssClass="texto form-control" AutoPostBack="True" OnTextChanged="txtData_TextChanged" style="font-family: 'Ubuntu', sans-serif !important, font-size: .8rem;"></asp:Textbox>
						 </div>
						 <div class="col-2 gx-2 gy-2">
								<asp:label id="lblHorario" runat="server" CssClass="tituloLabel" style="font-family: 'Ubuntu', sans-serif !important;">Horário</asp:label>
								<asp:Textbox id="txtHorario" runat="server" CssClass="texto form-control" AutoPostBack="True" OnTextChanged="txtHorario_TextChanged" style="font-family: 'Ubuntu', sans-serif !important, font-size: .8rem;"></asp:Textbox>
						 </div>
						 <div class="col-8 gx-2 gy-2">
								<asp:label id="lblLocalReuniao" runat="server" CssClass="tituloLabel" style="font-family: 'Ubuntu', sans-serif !important;">Local da Reunião</asp:label>
								<asp:textbox id="txtLocalReuniao" runat="server" CssClass="texto form-control" OnTextChanged="txtLocalReuniao_TextChanged" AutoPostBack="true" style="font-family: 'Ubuntu', sans-serif !important, font-size: .8rem;"></asp:textbox>
						 </div>
                         <hr class="mt-2 mb-1"/>
						 <div class="col-12">
										<asp:label id="lblCriarTexto" runat="server" CssClass="tituloLabel" style="font-family: 'Ubuntu', sans-serif !important; margin-top: 5% !important; font-size: .9rem">Criar Texto</asp:label>
						</div>
						 <!-- segunda parte -->
						 <div class="col-5 gx-2 gy-2">
							 <div class="row">
								 <div class="col-12 gx-2 gy-2 ms-2">
										<asp:label id="lblFraseAcidente" runat="server" CssClass="tituloLabel form-label col-form-label col-form-label-sm" style="font-family: 'Ubuntu', sans-serif !important;">Frase Acidente</asp:label>
								</div>
								<div class="col-12 gx-2 gy-2 ms-2">								
										<asp:textbox id="txtFraseAcidente" runat="server" rows="12" CssClass="texto form-control form-control-sm" Height="130px" TextMode="MultiLine" OnTextChanged="txtFraseAcidente_TextChanged" AutoPostBack="true" style="font-family: 'Ubuntu', sans-serif !important;"></asp:textbox>
								</div>
								<div class="col-12 gx-2 gy-2 ms-2">				
										<asp:textbox id="txtEditarFrase" runat="server" CssClass="texto form-control form-control-sm" AutoPostBack="True" OnTextChanged="txtEditarFrase_TextChanged" style="font-family: 'Ubuntu', sans-serif !important;"></asp:textbox>
								</div>
							 </div>
						 </div>
						 <!-- setinhas -->
						 <div class="col-1 text-center">
							 <div class="row" style="margin-top: 4.8rem;">
                                 <div class="col-12 gy-2">
                                      <asp:ImageButton ID="btnSalvar" ImageUrl="Images/direita.svg" runat="server" OnClick="btnSalvar_Click"/>
                                 </div>
                                 <div class="col-12 gy-2">
                                      <asp:ImageButton ID="btnDeletar" ImageUrl="Images/descer.svg" runat="server" OnClick="btnDeletar_Click" />
                                 </div>
							 </div>
						 </div>
						 <!-- terceira parte -->
							<div class="col-6 gx-2 gy-2">
                                <asp:HiddenField ID="hdnDataId" runat="server" />
								<asp:ListBox ID="txtFraseAcidente2" runat="server" class="texto form-control form-control-sm" Height="200px" Width="340px" onchange="updateTxtEditarFrase()" style="font-family: 'Ubuntu', sans-serif !important;"></asp:ListBox>
							</div>

							<!-- abas -->

							<div class="col-12 gx-2 gy-2">
								<ul class="nav nav-tabs" id="myTab" role="tablist">
								  <li class="nav-item nav-bg" role="presentation">
									<button class="nav-link nav-text active" id="assuntos-tab" data-bs-toggle="tab" data-bs-target="#assuntos-tab-pane" type="button" role="tab" aria-controls="assuntos-tab-pane" aria-selected="true"  style="font-family: 'Ubuntu', sans-serif !important;">Assuntos Discutidos</button>
								  </li>
								  <li class="nav-item nav-bg" role="presentation">
									<button class="nav-link nav-text" id="observacoes-tab" data-bs-toggle="tab" data-bs-target="#observacoes-tab-pane" type="button" role="tab" aria-controls="observacoes-tab-pane" aria-selected="false"  style="font-family: 'Ubuntu', sans-serif !important;">Observações</button>
								  </li>
								  <li class="nav-item nav-bg" role="presentation">
									<button class="nav-link nav-text" id="acidentes-tab" data-bs-toggle="tab" data-bs-target="#acidentes-tab-pane" type="button" role="tab" aria-controls="acidentes-tab-pane" aria-selected="false"  style="font-family: 'Ubuntu', sans-serif !important;">Acidentes Cadastrados</button>
								  </li>
								</ul>
								<div class="tab-content" id="myTabContent">
									<!-- aba 1: assuntos discutidos -->
									<div class="tab-pane fade show active" id="assuntos-tab-pane" role="tabpanel" aria-labelledby="assuntos-tab" tabindex="0">
										<div class="col-12 gx-2 gy-2 mt-2">
											<asp:TextBox ID="txtAssuntosDiscutidos" runat="server" class="texto form-control form-control-sm" rows="10" cols="1" AutoPostBack="True" OnTextChanged="txtAssuntosDiscutidos_TextChanged" TextMode="MultiLine" style="font-family: 'Ubuntu', sans-serif !important;"></asp:TextBox>
										</div>
									</div>
									<!-- aba 2: observações -->
									<div class="tab-pane fade" id="observacoes-tab-pane" role="tabpanel" aria-labelledby="observacoes-tab" tabindex="0">
										<div class="col-12 gx-2 gy-2 mt-2">
											<asp:TextBox ID="txtObservacoes" runat="server" class="texto form-control form-control-sm" rows="10" cols="1" AutoPostBack="True" OnTextChanged="txtObservacoes_TextChanged" TextMode="MultiLine" style="font-family: 'Ubuntu', sans-serif !important;"></asp:TextBox>
										</div>
									</div>
									<!-- aba 3: acidentes cadastrados -->
									<div class="tab-pane fade" id="acidentes-tab-pane" role="tabpanel" aria-labelledby="acidentes-tab" tabindex="0">
										 <div class="col-11 gx-2 gy-2 mt-2">
                                            <eo:Grid ID="grd_Acidentes_Cadastrados" runat="server" FixedColumnCount="1" ColumnHeaderDividerOffset="6" 
                                            ColumnHeaderAscImage="00050403" ColumnHeaderDescImage="00050404"
                                            GridLines="Both" PageSize="500" KeyField="IdEmprcImaegado" CssClass="grid"
                                            ColumnHeaderHeight="30" ItemHeight="30" Width="700px" Height="250px">
                                            <ItemStyles>
                                                <eo:GridItemStyleSet>
                                                <ItemStyle CssText="background-color: #FAFAFA" />
                                                <ItemHoverStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500;  text-align: left; height: 30px !important; background: rgba(28, 148, 137, 0.32); color: #FFFFFF; padding: 0.3rem 0.3rem 0.3rem 0.5rem; " />
                                                <AlternatingItemStyle CssText="background-color: #F2F2F2;" />
                                                <AlternatingItemHoverStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500;  text-align: left; height: 30px !important; background: rgba(28, 148, 137, 0.32); color: #FFFFFF; padding: 0.3rem 0.3rem 0.3rem 0.5rem;" />
                                                <SelectedStyle CssText="border-bottom-style:solid;border-left-style:solid;border-right-style:solid;border-top-style:solid;" />
                                                <CellStyle CssText="padding: 0.3rem 0.3rem 0.3rem 0.5rem; height: 30px !important;" />
                                                </eo:GridItemStyleSet>
                                            </ItemStyles>
                                            <ColumnHeaderTextStyle CssText="" />
                                            <ColumnHeaderStyle CssClass="tabelaC colunas" CssText="font-family: 'UniviaPro', sans-serif !important; font-weight: 400 !important;"/>
                                            <Columns>
                                            <eo:StaticColumn HeaderText="Empregado" AllowSort="True" 
                                                DataField="Empregado" Name="Empregado" ReadOnly="True" Width="180">
                                            <CellStyle CssText="font-family: 'UniviaPro'; font-size: 12px; font-weight: normal; color: #7D7B7B; text-align: left; height: 30px !important; " />
                                            </eo:StaticColumn>
                                            <eo:StaticColumn HeaderText="Data da CAT" AllowSort="True" 
                                                DataField="DataCAT" Name="Data da CAT" ReadOnly="True" Width="170">
                                            <CellStyle CssText="font-family: 'UniviaPro'; font-size: 12px; font-weight: normal; color: #7D7B7B; text-align: left; height: 30px !important; " />
                                            </eo:StaticColumn>
											<eo:StaticColumn HeaderText="Número da CAT" AllowSort="True" 
                                                DataField="NumeroCAT" Name="Número da CAT" ReadOnly="True" Width="170">
                                            <CellStyle CssText="font-family: 'UniviaPro'; font-size: 12px; font-weight: normal; color: #7D7B7B; text-align: left; height: 30px !important; " />
                                            </eo:StaticColumn>
											<eo:StaticColumn HeaderText="Arquivo da CAT" AllowSort="True" 
                                                DataField="ArquivoCAT" Name="Arquivo da CAT" ReadOnly="True" Width="180">
                                            <CellStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: normal; color: #7D7B7B; text-align: left; height: 30px !important; " />
                                            </eo:StaticColumn>
                                         </Columns>
                                        </eo:Grid>
                                        </div>
									</div>
						

							<div class="col-12 gx-2 gy-2 mt-2">
								<asp:Button ID="btnIndicacao" runat="server" CssClass="btn" Text="Indicação dos Presentes à Reunião" OnClick="btnIndicacao_Click" style="font-family: 'Ubuntu';font-size: 14.5px !important; text-align:center; vertical-align:central;"/>
								<asp:Button ID="btnEnviarAta" runat="server" CssClass="btn" Text="Enviar Ata" OnClick="btnEnviarAta_Click" style="font-family: 'Ubuntu';font-size: 14.5px !important; text-align:center; vertical-align:central;"/>
                                <asp:Button ID="btn_imprimir" runat="server" CssClass="btn" Text="Imprimir Ata" OnClick="btn_imprimir_Click" style="font-family: 'Ubuntu';font-size: 14.5px !important; text-align:center; vertical-align:central;"/>
                                <asp:Button ID="btnExcluir" runat="server" CssClass="btn2 pt-1" Text="Excluir" OnClick="btnExcluir_Click" style="font-family: 'Ubuntu';font-size: 14.5px !important; text-align:center; vertical-align:central;"/>
							</div>
						</div>
					</div>
				</div>
			</div>
            <eo:MsgBox ID="MsgBox1" runat="server" BackColor="#ffffff" ControlSkinID="None" HeaderHtml="Dialog Title" Height="192px" Width="345px" CssClass="card border border-1 p-2 text-center" IconUrl="Images/alerta_icon.svg">
                <HeaderStyleActive CssText="font-family: Ubuntu; font-size: 14px; font-weight: bold; color: #1C9489; text-align: center; padding: 5px;" />
                <ContentStyleActive CssText="font-family: Ubuntu; font-size: 14px; font-weight: bold; color: #7D7B7B; text-align: center; padding: 5px; width: 345px; height: 60px" />
                <FooterStyleActive CssText="width: 345px;" />
            </eo:MsgBox>
	</form>
	</body>

</HTML>

