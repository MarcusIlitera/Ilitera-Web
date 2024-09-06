<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EnviarAta.aspx.cs" Title="Ilitera.Net" Inherits="Ilitera.Net.EnviarAta" ValidateRequest="false" %>

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
        <link rel="preconnect" href="https://fonts.googleapis.com">
        <link rel="preconnect" href="https://fonts.gstatic.com" crossorigin>
        <link href="https://fonts.googleapis.com/css2?family=Ubuntu:wght@300;400&display=swap" rel="stylesheet">
	</HEAD>
    <script type="text/javascript">
    var g_itemIndex = -1;
    var g_cellIndex = -1;
    function OnCellSelected(grid) {
        var cell = grid.getSelectedCell();
        grid.raiseItemCommandEvent(cell.getItemIndex(), "Seleção");
    }
    function OnItemCommand(grid, itemIndex, colIndex, commandName) {
        //grid.raiseItemCommandEvent(itemIndex, commandName);
        grid.raiseItemCommandEvent(itemIndex, colIndex.toString());
    }
    </script>
    <style>
         @font-face {
            font-family: 'UniviaPro';
            src: url('css/css/UniviaPro-Regular.otf') format('opentype');
            font-weight: normal;
            font-style: normal;
        }

        .rbMembros{
            font-family:'Ubuntu'; 
        }

        .editar>a{
            color: #1C9489 !important;
            text-decoration: none !important;
        }
    </style>
	<body>
        <form id="frmPresenteReuniao" method="post" runat="server">
			<div class="container d-flex ms-5 ps-4 justify-content-center">
				<div class="row gx-3 gy-2 mt-5" style="width: 1100px">
					<div class="col-12 mb-2">
						<div class="row">
							<div class="col-12 subtituloBG text-center pt-2">
								<asp:Label ID="lblEnviarAta" runat="server" CssClass="subtitulo" style="font-family: 'UniviaPro', sans-serif !important;font-size: 16px !important;  font-weight: 500 !important;">Enviar Ata</asp:Label>
							</div>
							<%-- Tabela --%>
							<div class="col-6 gx-2 gy-2 mt-4">
								<div class="col-12 subtituloBG ms-2" style="padding-top: 0.4%; margin-left: 0px !important;">
                                  <h2 class="subtitulo" style="font-family: 'UniviaPro', sans-serif !important;font-size: 14px !important;  font-weight: 500 !important;">Membros</h2>
                                </div>
                                <div class="col-md-2 gx-3 gy-2 mt-4">
                                    <asp:RadioButtonList ID="rbMembros" runat="server" RepeatColumns="3" tabIndex="1" CssClass="texto form-control-sm ms-4 rbMembros" Width="500" AutoPostBack="True" OnSelectedIndexChanged="rbMembros_SelectedIndexChanged" >
                                    <asp:ListItem Value="1">Presidente e Vice-Presidente</asp:ListItem>
                                    <asp:ListItem Value="2">Todos Integrantes</asp:ListItem>
                                    <asp:ListItem Value="3" Selected="True">Todos Empregados</asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                                <div class="col-12 gx-2 gy-2 mt-3">
                                <eo:Grid ID="grdMembros" runat="server" FixedColumnCount="1" ColumnHeaderDividerOffset="6" 
                                    ColumnHeaderAscImage="00050403" ColumnHeaderDescImage="00050404"
                                    GridLines="Both" PageSize="500" KeyField="IdEmprcImaegado" CssClass="grid ms-1"
                                    ColumnHeaderHeight="30" ItemHeight="30" Width="480px" Height="300px">
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
                                    <ColumnHeaderStyle CssClass="tabelaC colunas" CssText="font-family: 'UniviaPro', sans-serif !important; font-weight: 400 !important;"/>
                                    <Columns>
                                    <eo:StaticColumn HeaderText="Empregado" AllowSort="True" 
                                        DataField="Empregado" Name="Empregado" ReadOnly="True" Width="330">
                                    <CellStyle CssText="font-family: 'UniviaPro'; font-size: 12px; font-weight: normal; color: #7D7B7B; text-align: left; height: 30px !important; " />
                                    </eo:StaticColumn>
                                    <eo:StaticColumn HeaderText="Cargo" AllowSort="True" 
                                        DataField="Cargo" Name="Cargo" ReadOnly="True" Width="150">
                                    <CellStyle CssText="font-family: 'UniviaPro'; font-size: 12px; font-weight: normal; color: #7D7B7B; text-align: left; height: 30px !important; " />
                                    </eo:StaticColumn>
                                 </Columns>
                                </eo:Grid>
                        </div>

                                </div>

                                   <div class="col-1 gx-4 gy-4 text-center" style="margin-left: 8px">
                                        <div class="row mt-4">
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
                                    </div>
                                </div>

                                      
                        <!--Tabela-->

                        <div class="col-4" style="padding-top: 1rem;">
                            <div class="subtituloBG " style="padding-top: 0.4%; margin-top: .4rem !important; width: 23rem;">
                                  <h2 class="subtitulo" style="font-family: 'UniviaPro', sans-serif !important;font-size: 14px !important;  font-weight: 500 !important;">Lista de E-mails</h2>
                            </div>
                           <div class="row">
                               <div class="col-6 gx-2 gy-2 mt-2" style="margin-right: 2px; margin-top: 1.8px">
                                    <eo:Grid ID="grdEmails" runat="server" FixedColumnCount="1" ColumnHeaderDividerOffset="6" 
                                        ColumnHeaderAscImage="00050403" ColumnHeaderDescImage="00050404"
                                        GridLines="Both" PageSize="500" KeyField="IdListaEmailsEnviarAta" CssClass="grid"
                                        ColumnHeaderHeight="30" ItemHeight="30" Width="380px" ClientSideOnItemCommand="OnItemCommand" ClientSideOnCellSelected="OnCellSelected" OnItemCommand="grdEmails_ItemCommand" Height="230px" >
                                    <ItemStyles>
                                        <eo:GridItemStyleSet>
                                            <ItemStyle CssText="background-color: #FAFAFA" />
                                            <ItemHoverStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500;  text-align: left; height: 30px !important; background-color: rgb(28, 148, 137, 0.32); color: #FFFFFF; padding: 0.3rem 0.3rem 0.3rem 0.5rem; " />
                                            <AlternatingItemStyle CssText="background-color: #F2F2F2;" />
                                            <AlternatingItemHoverStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500;  text-align: left; height: 30px !important; background-color: rgba(28, 148, 137, 0.32); color: #FFFFFF; padding: 0.3rem 0.3rem 0.3rem 0.5rem;" />
                                            <SelectedStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500;  text-align: left; height: 30px !important; background-color: rgba(176, 216, 213, 0.32); color: #FFFFFF; padding: 0.3rem 0.3rem 0.3rem 0.5rem;" />
                                            <CellStyle CssText="padding: 0.3rem 0.3rem 0.3rem 0.5rem; height: 30px !important;" />
                                        </eo:GridItemStyleSet>
                                    </ItemStyles>
                                    <ColumnHeaderTextStyle CssText="" />
                                    <ColumnHeaderStyle CssClass="tabelaC colunas" CssText="font-family: 'UniviaPro', sans-serif !important; font-weight: 400 !important;"/>
                                    <Columns>
                                        <eo:ButtonColumn ButtonText="Editar" CommandName="Editar" Name="Editar" DataField="Editar" Width="60" ButtonType="LinkButton">
                                            <CellStyle CssText="font-family: 'UniviaPro'; font-size: 12px; font-weight: normal; color: #7D7B7B; text-align: center; height: 30px !important;" CssClass="tituloLabel editar"/>
                                        </eo:ButtonColumn>
                                        <eo:StaticColumn HeaderText="Nome" AllowSort="True" 
                                            DataField="Nome" Name="Nome" ReadOnly="True" Width="180">
                                            <CellStyle CssText="font-family: 'UniviaPro'; font-size: 12px; font-weight: normal; color: #7D7B7B; text-align: left; height: 30px !important; " />
                                        </eo:StaticColumn>
                                        <eo:StaticColumn HeaderText="Email" AllowSort="True" 
                                            DataField="Email" Name="Email" ReadOnly="True" Width="120">
                                            <CellStyle CssText="font-family: 'UniviaPro'; font-size: 12px; font-weight: normal; color: #7D7B7B; text-align: left; height: 30px !important; " />
                                        </eo:StaticColumn>
                                </Columns>
                                </eo:Grid>
                                    
                               </div>
                                
                           </div>
                           <div class="row">
                               <asp:Label ID="lblEmail" runat="server" class="tituloLabel form-label col-form-label col-form-label-sm" Visible="False" style="padding-left: 0px !important; font-family: 'Ubuntu', sans-serif !important" ></asp:Label>
                               <asp:TextBox ID="txtEmail" runat="server" class="texto form-control form-control-sm" Visible="False" style="font-family: 'Ubuntu', sans-serif !important" ></asp:TextBox>
                               <div class="col-12" style="padding-left: 0px !important" >
                                    <asp:Button ID="btnEmailAplicar" runat="server" class="btn" Text="Aplicar" Visible="False" OnClick="btnEmailAplicar_Click"  style="margin-top: .3rem;font-family: 'Ubuntu', sans-serif !important; font-size: 14.5px !important; text-align:center; vertical-align:central;"/> 
                                    <asp:Button ID="btnEmailCancelar" runat="server" class="btn" Text="Cancelar" Visible="False" OnClick="btnEmailCancelar_Click" style="margin-top: .3rem;font-family: 'Ubuntu', sans-serif !important; font-size: 14.5px !important; text-align:center; vertical-align:central;"/>
                                </div>
                           </div>   
                        </div>

                            
                        </div>
                        <div class="row">
                                <div class="col-6 gx-2 gy-2">
								    <asp:label id="lblAssuntoEmail" runat="server" CssClass="tituloLabel" style="font-family: 'Ubuntu', sans-serif !important">Assunto do E-mail</asp:label>
								    <asp:textbox id="txtAssuntoEmail" runat="server" CssClass="texto form-control" style="font-family: 'Ubuntu', sans-serif !important; font-size: 0.875rem !important;"></asp:textbox>
                                    <asp:label id="lblSaudacao" runat="server" CssClass="tituloLabel form-label col-form-label col-form-label-sm" style="font-family: 'Ubuntu', sans-serif !important">Saudação do E-mail</asp:label>
                                    <asp:textbox id="txtSaudacao" runat="server" rows="12" CssClass="texto form-control form-control-sm" TextMode="SingleLine" OnTextChanged="txtSaudacao_TextChanged" style="font-family: 'Ubuntu', sans-serif !important; font-size: 0.875rem !important;"></asp:textbox>
                                    <asp:label id="lblCorpoEmail" runat="server" CssClass="tituloLabel form-label col-form-label col-form-label-sm" style="font-family: 'Ubuntu', sans-serif !important;">Corpo do E-mail</asp:label>
                                    <asp:textbox id="txtCorpoEmail" runat="server" rows="12" CssClass="texto form-control form-control-sm" Height="130px" TextMode="MultiLine" style="font-family: 'Ubuntu', sans-serif !important; font-size: 0.875rem !important;"></asp:textbox>
                                    <asp:Button ID="btnEnviarAta" runat="server" CssClass="btn mt-1" Text="Enviar Ata" OnClick="btnEnviarAta_Click" style="margin-top: .3rem;font-family: 'Ubuntu', sans-serif !important; font-size: 14.5px !important; text-align:center; vertical-align:central;"/>
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
