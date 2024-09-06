<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdicionarIndicado.aspx.cs" Title="Ilitera.Net"  EnableEventValidation="false" 
    Inherits="Ilitera.Net.AdicionarIndicado" ValidateRequest="false"%>
    

<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>

<!DOCTYPE html>

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

        localiza{
            font-family: 'UniviaPro', sans-serif !important;
        }
	</style>

	<body>
		<form id="frmPresenteReuniao" method="post" runat="server">
			<div class="container d-flex ms-5 ps-5 justify-content-center">
			<div class="row gx-5 gy-2 mt-4" style="width: 700px">
					<div class="col-12 subtituloBG text-center pt-2">
						<asp:label id="lblAdicionarIndicados" runat="server" CssClass="subtitulo" style="margin-left: 0px !important;font-family: 'UniviaPro', sans-serif !important;font-size: 16px !important;  font-weight: 500 !important;">Adicionar Indicados</asp:label>
					</div>
					<div class="col-5 gx-2 gy-2">
						<div class="row">
							<div class="col-12 gx-2 gy-2">
								<asp:label id="LabelEmpregados" runat="server" CssClass="subtitulo" style="margin-left: 0px !important;font-family: 'UniviaPro', sans-serif !important;font-size: 13.5px !important;  font-weight: 500 !important;" >Empregados exceto os leitos</asp:label>
							</div>
							<div class="col-12 gx-2">
								<asp:label id="LabelLocalizar" runat="server" CssClass="subtitulo" style="margin-left: 0px !important;font-family: 'UniviaPro', sans-serif !important;font-size: 12px !important;  font-weight: 500 !important;" >Localizar</asp:label>
								<asp:Textbox ID="txtLocalizar" runat="server" class="form-control form-control-sm localiza" AutoPostBack="True" OnTextChanged="txtLocalizar_TextChanged" style="font-family: 'UniviaPro', sans-serif !important;font-size: 12px !important;  font-weight: 300 !important;"></asp:Textbox>
							</div>
							<div class="col-12 gx-2">
								<asp:ListBox ID="lst_Empregados" runat="server" CssClass="form-control form-control-sm mt-1" Height="180px" EnableViewState="true" style="font-family: 'UniviaPro', sans-serif !important; font-size: 11.5px; font-weight: 200 !important; color: #7D7B7B;"></asp:ListBox>
							</div>
                            <div class="col-12 gx-2 gy-2">
                                <asp:label id="lblAddNaoEmpregado" runat="server" CssClass="subtitulo" style="margin-left: 0px !important;font-family: 'UniviaPro', sans-serif !important;font-size: 13.5px !important;  font-weight: 500 !important;">Adicionar não Empregado</asp:label>
                                <br/>
                                <asp:Label ID="lblNome" runat="server" class="tituloLabel form-check-label gx-2 gy-2" style="margin-left: 0px !important;font-family: 'UniviaPro', sans-serif !important;font-size: 12px !important;  font-weight: 500 !important;" Text="Nome"></asp:Label>
                                <asp:Textbox ID="txtNome" runat="server" class="texto form-control form-control-sm" AutoPostBack="True" OnTextChanged="txtNome_TextChanged"></asp:Textbox>
                            </div>                 
						</div>
					</div>
					
				 <div class="col-1 gx-2 gy-2 text-center">
                      <div class="row mt-4" style="padding-top: 8rem">
                           <div class="col-12 gx-4 gy-2">
                                <asp:ImageButton ID="btnSubir" ImageUrl="Images/subir.svg" runat="server" OnClick="btnSubir_Click" />
                           </div>
							<div class="col-12 gx-4 gy-2">
                                  <asp:ImageButton ID="btnAdicionar" ImageUrl="Images/direita.svg" runat="server" OnClick="btnAdicionar_Click" />
                            </div>
                            <div class="col-12 gx-4 gy-2">
								<asp:ImageButton ID="btnRemover" ImageUrl="Images/subir.svg" runat="server" OnClick="btnRemover_Click" />
                             </div>
                            <div class="col-12 gx-4 gy-2">
                                 <asp:ImageButton ID="btnDescer" ImageUrl="Images/descer.svg" runat="server" OnClick="btnDescer_Click" />
                           </div>
                           <div class="col-12 gx-4 gy-5">
                                  <asp:ImageButton ID="btnNome" ImageUrl="Images/direita.svg" runat="server" class="mt-2" OnClick="btnNome_Click"/>
                           </div> 
                       </div>
                     </div>

				<div class="col-4 gx-2 gy-2">
                        <div class="row">
                            <div class="col-6 mt-2">
                                <asp:Label ID="lblSelecionados" runat="server" class="tituloLabel form-check-label gx-2 gy-2 subtitulo" style="margin-left: 0px !important;font-family: 'UniviaPro', sans-serif !important;font-size: 13.5px !important;  font-weight: 500 !important;" Text="Selecionados"></asp:Label>
                            </div>
                        </div>               
                           <div class="row">
                               <div class="col-6 gx-2 gy-2 pt-3">
                                   <eo:grid id="grd_Membros2" runat="server" fixedcolumncount="1" columnheaderdivideroffset="6"
                                       columnheaderascimage="00050403" columnheaderdescimage="00050404"
                                       gridlines="Both" pagesize="500" keyfield="IdMembroCipa" cssclass="grid"
                                       columnheaderheight="30" itemheight="30" width="350px">
                                    <ItemStyles>
                                        <eo:GridItemStyleSet>
                                        <ItemStyle CssText="background-color: #FAFAFA" />
                                        <ItemHoverStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500;  text-align: left; height: 30px !important; background-color: rgb(28, 148, 137, 0.32); color: #FFFFFF; padding: 0.3rem 0.3rem 0.3rem 0.5rem; " />
                                        <AlternatingItemStyle CssText="background-color: #F2F2F2;" />
                                        <AlternatingItemHoverStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500;  text-align: left; height: 30px !important; background-color: rgba(28, 148, 137, 0.32); color: #FFFFFF; padding: 0.3rem 0.3rem 0.3rem 0.5rem;" />
                                        <SelectedStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500;  text-align: left; height: 30px !important; background-color: rgba(28, 148, 137, 0.32); color: #FFFFFF; padding: 0.3rem 0.3rem 0.3rem 0.5rem;" />
                                        <CellStyle CssText="padding: 0.3rem 0.3rem 0.3rem 0.5rem; height: 30px !important;" />
                                        </eo:GridItemStyleSet>
                                    </ItemStyles>
                                    <ColumnHeaderTextStyle CssText="" />
                                    <ColumnHeaderStyle CssClass="tabelaC colunas"  CssText="font-family: 'UniviaPro', sans-serif !important; font-weight: 400 !important;"/>
                                    <Columns>
                                        <eo:StaticColumn HeaderText="Empregado" AllowSort="True" 
                                            DataField="Empregado" Name="Nome2" ReadOnly="True" Width="200">
                                            <CellStyle CssText="font-family: 'UniviaPro'; font-size: 11.5px; font-weight: normal; color: #7D7B7B; text-align: left; height: 30px !important; padding-left: .5rem " />
                                        </eo:StaticColumn>
                                        <eo:StaticColumn HeaderText="Cargo Cipa" AllowSort="True" 
                                            DataField="CargoCipa" Name="CargoCipa" ReadOnly="True" Width="150">
                                            <CellStyle CssText="font-family: 'UniviaPro'; font-size: 11.5px; font-weight: normal; color: #7D7B7B; text-align: left; height: 30px !important; " />
                                        </eo:StaticColumn>
     
                                </Columns>
                                </eo:grid>
                               </div>
                           </div>
                        </div>
                <div class="col-12 gx-2">
                    <div class="row">
                                   
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
		

