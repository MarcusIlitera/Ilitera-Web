<%@ Page Language="C#"   AutoEventWireup="True"  MasterPageFile="~/Site.Master" Codebehind="ListagemEPI.aspx.cs"  Inherits="Ilitera.Net.ControleEPI.ListagemEPI" Title="Ilitera.Net" %>

<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
	<link href="css/tabelas.css" rel="stylesheet" type="text/css" />
    <link href="css/forms.css" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent" >


    <script language="javascript">       
</script>
		<div class="container-fluid d-flex ms-5 ps-4">
			<div class="row gx-3 gy-2 w-100">
				<div class="row">
					<div class="col-11 subtituloBG pt-2">
					   <asp:label id="lblUtilizacaoEPI" runat="server" CssClass="subtitulo">EPI em utilização listados por EPI</asp:label>
				   </div>
				 </div>
				<div class="col-md-4 gx-4 gy-4">
					<asp:label id="lblSelecione" runat="server" CssClass="tituloLabel">Selecione o EPI:</asp:label>
					<asp:dropdownlist id="ddlEPI" onselectedindexchanged="ddlEPI_SelectedIndexChanged" runat="server" CssClass="texto form-select form-select-sm" AutoPostBack="True"></asp:dropdownlist>
				</div>
				<div class="col-11">
					<div class="row">
						<div class="col gx-3 gx-2">
							<asp:datagrid id="DGridUtilizacaoEPI" runat="server" CssClass="table" HorizontalAlign="Center"
								CellPadding="4" AutoGenerateColumns="False" AllowPaging="True"
								GridLines="None" Width="680px" ForeColor="#333333">
								<SelectedItemStyle Font-Bold="True" ForeColor="#333333" BackColor="#C5BBAF"></SelectedItemStyle>
								<EditItemStyle BackColor="#999999"></EditItemStyle>
								<AlternatingItemStyle CssClass="alternatingItem" ForeColor="#D9D9D9"></AlternatingItemStyle>
								<ItemStyle CssClass="tableItem" BackColor="#FAFAFA" ForeColor="#B0ABAB"></ItemStyle>
								<HeaderStyle Font-Bold="True" CssClass="tableHeader" BackColor="#D9D9D9"></HeaderStyle>
								<FooterStyle ForeColor="#000066" BackColor="#5D7B9D"></FooterStyle>
								<Columns>
									<asp:BoundColumn DataField="NomeE" HeaderText="Equipamento">
										<HeaderStyle Width="180px"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="NomeF" HeaderText="Fabricante">
										<HeaderStyle Width="290px"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="NumeroCA" HeaderText="CA"  FooterStyle-HorizontalAlign="Center"  >
										<FooterStyle HorizontalAlign="Center" />
										<HeaderStyle Width="60px"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="QtdEntregue" HeaderText="Em Utiliza&#231;&#227;o" ItemStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Center">
										<FooterStyle HorizontalAlign="Center" />
										<HeaderStyle Width="70px"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" />
									</asp:BoundColumn>
									<asp:BoundColumn DataField="Periodicidade" HeaderText="Periodicidade" ItemStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Center">
										<FooterStyle HorizontalAlign="Center" />
										<HeaderStyle Width="80px"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" />
									</asp:BoundColumn>
								</Columns>
								<PagerStyle CssClass="tablePage" BackColor="#284775"  
									HorizontalAlign="Center"></PagerStyle>
							</asp:datagrid>
						</div>
					</div>
					<div class="row">
						<asp:label id="lblTotRegistros" runat="server" CssClass="tituloLabel"></asp:label>
					</div>
				</div>

                            <asp:Label ID="lbl_Id_Empresa" runat="server" Text="IdEmpresa" Visible="False"></asp:Label>
                    <caption>
						<asp:Label ID="lbl_Id_Usuario" runat="server" Text="IdUsuario" Visible="False"></asp:Label>
                    </caption>

					<div class="col-11 gx-4 gy-4">
                        <asp:Button ID="lblCancel" onclick="lblCancel_Click"  runat="server" CssClass="btn" Text="Voltar" Width="132px" />
					</div>
			</div>	        
		</div>			        
         <eo:msgbox ID="MsgBox1" runat="server" BackColor="#47729F" ControlSkinID="None" 
        HeaderHtml="Dialog Title" Height="100px" Width="168px">
        <HeaderStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #ffbf00 3px solid; padding-left: 4px; font-weight: bold; font-size: 11px; padding-bottom: 2px; color: white; padding-top: 2px; border-bottom: #22456a 1px solid; font-family: verdana" />
        <ContentStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #7d97b6 1px solid; padding-left: 4px; border-left-width: 1px; font-size: 11px; border-left-color: #728eb8; padding-bottom: 4px; color: white; padding-top: 4px; border-bottom: #22456a 1px solid; font-family: verdana" />
        <FooterStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #7d97b6 1px solid; padding-left: 4px; border-left-width: 1px; font-size: 11px; border-left-color: #728eb8; padding-bottom: 4px; color: white; padding-top: 4px; border-bottom: #22456a 1px solid; font-family: verdana" />
        </eo:msgbox>       

         
</asp:Content>