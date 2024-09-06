<%@ Page Language="C#"   AutoEventWireup="True"  MasterPageFile="~/Site.Master" CodeBehind="CadastroCompra.aspx.cs"  Inherits="Ilitera.Net.CadastroCompra" Title="Ilitera.Net" %>

<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>


<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
	<link href="css/forms.css" rel="stylesheet" type="text/css" />
    <link href="css/tabelas.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
    .defaultFont
    {
        width: 627px;
    }
        .largeboldFont
        {
            font-weight: 700;
        }
        #Table1
        {
            width: 688px;
        }
        .style1
        {
            width: 223px;
        }
        #Table10
        {
            width: 662px;
        }
        .inputBox
        {}
    </style>
</asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent" >
	<div class="container-fluid d-flex ms-5 ps-4">
        <div class="row gx-3 gy-2 w-100">

    <script language="javascript">

        function CadastroFornecedor(IdEmpresa, IdUsuario) {
            addItem(centerWin("AddFornecedor.aspx?TipoJanela=CadastroCompra&IdFornecedorEPI=0&IdUsuario=" + IdUsuario + "&IdEmpresa=" + IdEmpresa,310,175,"CadFornecedor"), "Todos");                              
        }


        function VerificaData() {
            return validar_data(document.forms[0].txtddc.value, document.forms[0].txtmmc.value, document.forms[0].txtaac.value, 'Data de Compra');
        }

        function Inicialize() {
            document.forms[0].txtddc.onkeypress = ChecarTAB;
            document.forms[0].txtddc.onfocus = PararTAB;
            document.forms[0].txtddc.onkeyup = DiaC;
            document.forms[0].txtmmc.onkeypress = ChecarTAB;
            document.forms[0].txtmmc.onfocus = PararTAB;
            document.forms[0].txtmmc.onkeyup = MesC;
            document.forms[0].txtaac.onkeypress = ChecarTAB;
            document.forms[0].txtaac.onfocus = PararTAB;
            document.forms[0].txtaac.onkeyup = AnoC;
        }

        function DiaC() {
            if (document.forms[0].txtddc.value.length == 2 && VerifiqueTAB == true)
                document.forms[0].txtmmc.focus();
        }

        function MesC() {
            if (document.forms[0].txtmmc.value.length == 2 && VerifiqueTAB == true)
                document.forms[0].txtaac.focus();
        }

        function AnoC() {
            if (document.forms[0].txtaac.value.length == 4 && VerifiqueTAB == true)
                document.forms[0].txtValorTotal.focus();
        }


		</script>

			<div class="col-11 subtituloBG" style="padding-top: 10px;">
				<asp:label id="lblDadosCompra" runat="server" CssClass="subtitulo">Dados de Compra</asp:label>
			</div>

			<div class="col-11 mb-3">
				<div class="row">
					<div class="col-md-5 gx-3 gy-2">
						<asp:label id="lblSelecione" runat="server" CssClass="tituloLabel form-label">Selecione o Fornecedor</asp:label>
						<asp:dropdownlist id="ddlFornecedor" runat="server" CssClass="texto form-select form-select-sm" AutoPostBack="True"></asp:dropdownlist>
					</div>

					<div class="col-md-3 gx-3 mt-4">
						<asp:button id="btnNovaCelula" onclick="btnNovaCelula_Click" runat="server" CssClass="btn" Text="Novo Fornecedor"></asp:button>
					</div>

					<div class="col-4"></div>

					<div class="col-md-4 gx-3 gy-2">
						<asp:label id="lblDocumento" runat="server" CssClass="tituloLabel form-label">Documento</asp:label>
						<asp:textbox id="txtDocumento" runat="server" CssClass="texto form-control form-control-sm"></asp:textbox>
					</div>

					<div class="col-md-2 gx-3 gy-2">
						<asp:label id="lblDataCompra" runat="server" CssClass="tituloLabel form-label">Data de Compra</asp:label>
						<div class="row">
							<div class="col-2">
								<asp:textbox id="txtddc" runat="server" CssClass="texto form-control form-control-sm" MaxLength="2"></asp:textbox>
							</div>

							<div class="col-2 gx-3 gy-1 text-center">
								<asp:label id="Label1" runat="server" CssClass="tituloLabel form-label">/</asp:label>
							</div>

							<div class="col-2">
								<asp:textbox id="txtmmc" runat="server" CssClass="texto form-control form-control-sm" MaxLength="2"></asp:textbox>
							</div>

							<div class="col-2 gx-3 gy-1 text-center">
								<asp:label id="Label2" runat="server" CssClass="tituloLabel form-label">/</asp:label>
							</div>

							<div class="col-4">
								<asp:textbox id="txtaac" runat="server" CssClass="texto form-control form-control-sm" MaxLength="4"></asp:textbox>
							</div>
						</div>
					</div>
					
					<div class="col-md-4 gx-3 gy-2">
						<asp:label id="lblValorTotal" runat="server" CssClass="tituloLabel form-label">Valor Total</asp:label>
						<div class="row">
							<div class="col-1 gy-1">
								<asp:Label runat="server" CssClass="tituloLabel form-label">R$</asp:Label>
							</div>

							<div class="col-5">
								<asp:textbox id="txtValorTotal" runat="server" CssClass="texto form-control form-control-sm"></asp:textbox>
							</div>
						</div>
					</div>
				</div>
			</div>

			<div class="col-11 subtituloBG" style="padding-top: 10px;">
				<asp:label id="lblDetalhesCompra" runat="server" CssClass="subtitulo">Detalhes da Compra</asp:label>
			</div>

			<div class="col-11 mb-3">
				<div class="row">
					<div class="col-md-5 gx-3 gy-2">
						<asp:label id="lblCA" runat="server" CssClass="tituloLabel form-label"> Selecione o CA</asp:label>
						<asp:dropdownlist id="ddlCA" runat="server" CssClass="texto form-select"></asp:dropdownlist>
					</div>

					<div class="col-md-3 gx-3 mt-4" style="padding-top:3px;">
						<asp:button id="btnCadastro" onclick="btnCadastro_Click" runat="server" CssClass="btn" Text="Cadastro de CA's"></asp:button>
					</div>

					<div class="col-4"></div>

					<div class="col-md-3 gx-3 gy-2">
						<asp:label id="lblQuantidade" runat="server" CssClass="tituloLabel form-label">Quantidade</asp:label>
						<asp:textbox id="txtQuantidade" runat="server" CssClass="texto form-control form-control-sm"></asp:textbox>
					</div>

					<div class="col-md-3 gx-3 gy-2">
						<asp:label id="lblLoteFabricacao" runat="server" CssClass="tituloLabel form-label">Lote de Fabricação</asp:label>
						<asp:textbox id="txtLoteFabricacao" runat="server" CssClass="texto form-control form-control-sm"></asp:textbox>
					</div>

					<div class="col-md-4 gx-3 gy-2">
						<asp:label id="lblValorUnitario" runat="server" CssClass="tituloLabel form-label">Valor Unitário</asp:label>
						<div class="row">
							<div class="col-1 gy-1">
								<asp:Label runat="server" CssClass="tituloLabel form-label">R$</asp:Label>
							</div>

							<div class="col-5">
								<asp:textbox id="txtValorUnitario" runat="server" CssClass="texto form-control form-control-sm"></asp:textbox>
							</div>
						</div>
					</div>
				</div>
			</div>

			<div class="col-11 text-center mb-3">
				<asp:button id="btnAdicionar" onclick="btnAdicionar_Click" runat="server" CssClass="btn" Text="Adicionar Item"></asp:button>
			</div>

			<div class="col-11 text-center">
				<div class="row">
					<div class="col-12 text-center">
						<asp:datagrid id="DGridItemsCompra" runat="server" CssClass="table" Width="595px" HorizontalAlign="Center"
							CellPadding="3" AutoGenerateColumns="False" AllowPaging="True"
							PageSize="3" GridLines="Vertical">
							<AlternatingItemStyle CssClass="alternatingItem"></AlternatingItemStyle>
							<ItemStyle CssClass="tableItem"></ItemStyle>
							<HeaderStyle Font-Bold="True" CssClass="tableHeader"></HeaderStyle>
							<FooterStyle ForeColor="#000066" BackColor="White"></FooterStyle>
							<Columns>
								<asp:BoundColumn Visible="False" DataField="IdCA" HeaderText="IdCA">
									<HeaderStyle Width="0px"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="LoteFabricacao" HeaderText="LoteFabricacao">
									<HeaderStyle Width="0px"></HeaderStyle>
								</asp:BoundColumn>
								<asp:ButtonColumn DataTextField="Remover" HeaderText="-----" CommandName="RemoverCA">
									<HeaderStyle Width="80px"></HeaderStyle>
									<ItemStyle Font-Bold="True"></ItemStyle>
								</asp:ButtonColumn>
								<asp:TemplateColumn HeaderText="CA">
									<HeaderStyle Width="114px"></HeaderStyle>
									<ItemTemplate>
										<%#DataBinder.Eval(Container.DataItem, "NumeroCA")%>
									</ItemTemplate>
									<EditItemTemplate>
										<asp:dropdownlist id=ddlCAEditar runat="server" CssClass="inputBox" DataValueField="IdCA" DataTextField="NumeroCA">
										</asp:dropdownlist>
									</EditItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Quantidade">
									<HeaderStyle Width="113px"></HeaderStyle>
									<ItemTemplate>
										<%#DataBinder.Eval(Container.DataItem, "Quantidade")%>
									</ItemTemplate>
									<EditItemTemplate>
										<asp:textbox id=txtQuantidadeGrid runat="server" CssClass="inputBox" Text='<%#DataBinder.Eval(Container.DataItem, "Quantidade")%>' Width="100px">
										</asp:textbox>
										<asp:textbox id=txtIdCA runat="server" CssClass="inputBox" Text='<%#DataBinder.Eval(Container.DataItem, "IdCA")%>' Width="0px">
										</asp:textbox>
									</EditItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Valor Unit&#225;rio(R$)">
									<HeaderStyle Width="114px"></HeaderStyle>
									<ItemTemplate>
										<%#DataBinder.Eval(Container.DataItem, "ValorItem")%>
									</ItemTemplate>
									<EditItemTemplate>
										<asp:textbox id=txtValorItem runat="server" CssClass="inputBox" Text='<%#DataBinder.Eval(Container.DataItem, "ValorItem")%>' Width="100px">
										</asp:textbox>
									</EditItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="ValorTotal" ReadOnly="True" HeaderText="Valor Total" DataFormatString="R${0:G}">
									<HeaderStyle Width="114px"></HeaderStyle>
								</asp:BoundColumn>
								<asp:EditCommandColumn ButtonType="LinkButton" UpdateText="&lt;img src='img/update.gif' border=0 alt='Atualizar'&gt;"
									HeaderText="-----" CancelText="&lt;img src='img/cancel.gif' border=0 alt='Cancelar'&gt;" EditText="Editar">
									<HeaderStyle Width="60px"></HeaderStyle>
								</asp:EditCommandColumn>
							</Columns>
							<PagerStyle CssClass="tablePage" Mode="NumericPages"></PagerStyle>
						</asp:datagrid>
						<asp:label id="lblTotRegistros" runat="server" CssClass="normalFont"></asp:label>
					</div>
				</div>
			</div>

			<div class="col-11">
				<div class="row">
					<div class="col-md-2">
						<asp:button id="btnSalvar" onclick="btnSalvar_Click" runat="server" CssClass="btn" Text="Salvar Compra"></asp:button>
					</div>
					
					<div class="col-md-2">
						<asp:button id="lblCancelar" onclick="lblCancelar_Click" runat="server" CssClass="btn2" Text="Cancelar"></asp:button>
					</div>
				</div>
			</div>
						<%--<asp:button id="btnNovoFornecedor" runat="server" CssClass="buttonBox" Text="Novo Fornecedor" onclick="btnNovoFornecedor_Click"></asp:button>--%>
                        
                        <%--<INPUT class=buttonBox  id=btnNovaCelula style="WIDTH: 120px; height: 26px;"  onclick="javascript:void(CadastroFornecedor(<%=cliente.Id%>, <%=usuario.Id%>));"  tabIndex=0 type=button size=20 value="Novo Fornecedor">--%>
                        
						<asp:label id="lblError" runat="server" CssClass="errorFont"></asp:label>

                                  <asp:Label ID="lbl_Id_Empresa" runat="server" Text="IdEmpresa" Visible="False"></asp:Label>
                            <asp:Label ID="lbl_Id_Usuario" runat="server" Text="IdUsuario" Visible="False"></asp:Label>

<%--		</form>
	</body>
</HTML>
--%>

<eo:MsgBox ID="MsgBox1" runat="server" BackColor="#47729F" ControlSkinID="None" 
        HeaderHtml="Dialog Title" Height="100px" Width="168px">
        <HeaderStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #ffbf00 3px solid; padding-left: 4px; font-weight: bold; font-size: 11px; padding-bottom: 2px; color: white; padding-top: 2px; border-bottom: #22456a 1px solid; font-family: verdana" />
        <ContentStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #7d97b6 1px solid; padding-left: 4px; border-left-width: 1px; font-size: 11px; border-left-color: #728eb8; padding-bottom: 4px; color: white; padding-top: 4px; border-bottom: #22456a 1px solid; font-family: verdana" />
        <FooterStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #7d97b6 1px solid; padding-left: 4px; border-left-width: 1px; font-size: 11px; border-left-color: #728eb8; padding-bottom: 4px; color: white; padding-top: 4px; border-bottom: #22456a 1px solid; font-family: verdana" />
    </eo:MsgBox>


		</div>
	</div>
</asp:Content>