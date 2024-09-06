<%@ Page Language="C#"   AutoEventWireup="True"  MasterPageFile="~/Site.Master" CodeBehind="IndicarCIPA.aspx.cs"  Inherits="Ilitera.Net.ControleCIPA.IndicarCIPA" Title="Ilitera.Net" %>

<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>


<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
	<link href="css/tabelas.css" rel="stylesheet" type="text/css" />
    <link href="css/forms.css" rel="stylesheet" type="text/css" />
       <style type="text/css">
        .linha
   {
	font: 8px Verdana, Arial, Helvetica, sans-serif, Tahoma;
    }
           .btnLogarClass
           {}
           .largeboldFont
           {
               font-weight: 700;
           }
    </style>

</asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent" >
	<div class="container-fluid d-flex ms-5 ps-4">
        <div class="row gx-3 gy-2">
			<div class="col-11 subtituloBG"  style="padding-top: 10px">
				<asp:label id="lblEntregaEPI" runat="server" CssClass="subtitulo">Constituição CIPA</asp:label>
                <asp:panel id="Panel2" runat="server" Width="662px"></asp:panel>
            </div>

        <div class="col-11">
			<div class="row">
				<div class="col-md-4 gx-3 gy-2">			
					<asp:label id="lblSelecioneEPI" runat="server" CssClass="tituloLabel form-label">Selecione a CIPA:</asp:label>
					<asp:dropdownlist id="ddlCIPA" onselectedindexchanged="ddlCIPA_SelectedIndexChanged"  runat="server" CssClass="texto form-select form-select-sm" AutoPostBack="True"></asp:dropdownlist>
				</div>

				<div class="col-md-2 me-2 gx-3 gy-2">
                    <fieldset>
                    <asp:Label ID="lblCA0" runat="server" CssClass="tituloLabel form-label col-form-label">Início das Inscrições:</asp:Label>
                    <asp:TextBox ID="txt_Data" runat="server" ReadOnly="True" CssClass="texto form-control form-control-sm"></asp:TextBox>
                    <asp:Button ID="btnAlterarData" onclick="btnAlterarData_Click"  runat="server" 
                        CssClass="igdm_TrendyMenuItemVerticalSelected" Font-Size="X-Small" 
                        Text="Alterar Data" Width="25px" Visible="False" />
                    </fieldset>
                </div>

                <div class="col-md-2 gx-3 gy-2">
                    <fieldset>
                    <asp:Label ID="Label1" runat="server" CssClass="tituloLabel form-label col-form-label">Data da Eleição:</asp:Label>
                    <asp:TextBox ID="txt_Eleicao" runat="server" ReadOnly="True" CssClass="texto form-control form-control-sm"></asp:TextBox>
                    <asp:Button ID="btnAlterarEleicao" onclick="btnAlterarEleicao_Click"  runat="server" 
                        CssClass="igdm_TrendyMenuItemVerticalSelected" Font-Size="X-Small" 
                        Text="Alterar Data" Width="25px" Visible="False" />
                    </fieldset>
                </div> 
			</div>
		</div>
		<div class="col-11 subtituloBG mt-4"  style="padding-top: 10px">
            <asp:Label ID="lblEntregaEPI0" runat="server" CssClass="subtitulo">Candidatos</asp:Label>
            <asp:panel id="Panel1" runat="server" Width="662px"></asp:panel>
        </div>
		<div class="col-11">
			<div class="row">
				<div class="col gx-3 gx-2">
					<asp:datagrid id="DGridCandidatos" runat="server" CssClass="table" 
						Width="595px" GridLines="Vertical"
						PageSize="3" AllowPaging="True" AutoGenerateColumns="False"
						CellPadding="3" HorizontalAlign="Center" >
						<AlternatingItemStyle CssClass="alternatingItem" ForeColor="#D9D9D9"></AlternatingItemStyle>
						<ItemStyle CssClass="tableItem" BackColor="#FAFAFA" ForeColor="#B0ABAB"></ItemStyle>
						<HeaderStyle Font-Bold="True" CssClass="tableHeader" BackColor="#D9D9D9"></HeaderStyle>
						<EditItemStyle BackColor="#999999" />
						<FooterStyle ForeColor="#000066" BackColor="#5D7B9D"></FooterStyle>
						<Columns>
							<asp:BoundColumn Visible="False" DataField="IdParticipantesEleicaoCipa" HeaderText="IdParticipantesEleicaoCipa">
								<HeaderStyle Width="0px"></HeaderStyle>
							</asp:BoundColumn>
							<asp:BoundColumn Visible="true" DataField="tNo_Empg" HeaderText="Candidato">
								<HeaderStyle Width="580px"></HeaderStyle>
							</asp:BoundColumn>
			<%--
							<asp:TemplateColumn HeaderText="CA">
								<HeaderStyle Width="80px"></HeaderStyle>
								<ItemTemplate>
									<%#DataBinder.Eval(Container.DataItem, "IdParticipantesEleicaoCipa")%>
								</ItemTemplate>
								<EditItemTemplate>
									<asp:dropdownlist id="ddlCAEditar" runat="server" CssClass="inputBox" DataTextField="IdParticipantesEleicaoCipa" DataSource='<%#GetDropDownCandidatoExcluir((string)DataBinder.Eval(Container.DataItem, "IdParticipantesEleicaoCipa")))%>' DataValueField="IdParticipantesEleicaoCipa">
									</asp:dropdownlist>
								</EditItemTemplate>
							</asp:TemplateColumn>
                             
							<asp:EditCommandColumn ButtonType="LinkButton" UpdateText="&lt;img src='img/update.gif' border=0 alt='Atualizar'&gt;"
								HeaderText="-----" CancelText="&lt;img src='img/cancel.gif' border=0 alt='Cancelar'&gt;" EditText="Excluir">
								<HeaderStyle Width="60px"></HeaderStyle>
							</asp:EditCommandColumn>  --%> 
							</Columns>

						<PagerStyle CssClass="tablePage" Mode="NumericPages" BackColor="#284775"></PagerStyle>
						<SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
					</asp:datagrid>


				</div>
			</div>
			<div class="row">
				<div class="col-12 gx-3 gy-2">
					<asp:label id="lblTotRegistros" runat="server" CssClass="normalFont"></asp:label>
				</div>
				<div class="col-12 gx-3 gy-2">
					<asp:Label ID="lblError" runat="server" CssClass="errorFont"></asp:Label>
				</div>
			</div>
			<div class="row">
				<div class="col-2 gx-3 gy-2">
					<asp:Button ID="btnImprimirFicha" runat="server" CssClass="btn" Text="Imprimir Fichas de Inscrição" />
				</div>
			</div>
		</div>
		<div class="col-11 subtituloBG mt-4"  style="padding-top: 10px">
			<asp:Label ID="lblEntregaEPI1" runat="server" CssClass="subtitulo">Comissão Eleitoral</asp:Label>
			<asp:panel id="Panel3" runat="server" Width="662px"></asp:panel>
        </div>
		<div class="col-11">
			<div class="row">
				<div class="col gx-3 gx-2">
					<asp:datagrid id="DGridComissao" runat="server" CssClass="table" Width="595px" GridLines="None"
						PageSize="3" AllowPaging="True" AutoGenerateColumns="False"
						CellPadding="4" HorizontalAlign="Center" ForeColor="#D9D9D9">
						<AlternatingItemStyle CssClass="alternatingItem" ForeColor="#D9D9D9"></AlternatingItemStyle>
						<ItemStyle CssClass="tableItem" BackColor="#FAFAFA" ForeColor="#B0ABAB"></ItemStyle>
						<HeaderStyle Font-Bold="True" CssClass="tableHeader" BackColor="#D9D9D9"></HeaderStyle>
						<EditItemStyle BackColor="#999999" />
						<FooterStyle ForeColor="#000066" BackColor="#5D7B9D"></FooterStyle>
						<Columns>
							<asp:BoundColumn Visible="False" DataField="IdMembroComissaoEleitoral" HeaderText="IdMembroComissaoEleitoral">
								<HeaderStyle Width="0px"></HeaderStyle>
							</asp:BoundColumn>
							<asp:BoundColumn Visible="True" DataField="NomeMembro" HeaderText="Membro">
								<HeaderStyle Width="340px"></HeaderStyle>
							</asp:BoundColumn>
							<asp:BoundColumn Visible="True" DataField="NomeCargo" HeaderText="Cargo">
								<HeaderStyle Width="220px"></HeaderStyle>
							</asp:BoundColumn>
						</Columns>
						<PagerStyle CssClass="tablePage" Mode="NumericPages" BackColor="#284775"></PagerStyle>
						<SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
					</asp:datagrid>
				</div>
				<div class="row">
					<div class="col-12 gx-3 gy-2">
						<asp:label id="Label3" runat="server" CssClass="normalFont"></asp:label>
					</div>
				</div>
			</div>
		</div>
					
		<asp:label id="lblCA" runat="server" CssClass="boldFont" Visible="False">Selecione o membro :</asp:label>
        <br />
        <asp:dropdownlist id="ddlEmpregado2" runat="server" CssClass="inputBox" 
            AutoPostBack="true" Width="486px" Height="18px" Visible="False">
			<asp:ListItem Value="0">Selecione...</asp:ListItem>
		</asp:dropdownlist><br />
        <br />
        <asp:Button ID="btnAdicionarMembro" onclick="btnAdicionarMembro_Click"  runat="server" 
            CssClass="igdm_TrendyMenuItemVerticalSelected" Enabled="False" 
            Text="Adicionar Membro - Comissão Eleitoral" 
            Visible="False" />
        <br />
        <asp:Button ID="btnAdicionarMembroCandidato" runat="server"  onclick="btnAdicionarMembroCandidato_Click" 
            CssClass="igdm_TrendyMenuItemVerticalSelected" 
            Text="Adicionar Membro - Candidatos" Visible="False" />
        <asp:Label ID="lbl_Id_Empresa" runat="server" Text="IdEmpresa" Visible="False"></asp:Label>
		<br />
        <asp:Label ID="lbl_Id_Usuario" runat="server" Text="IdUsuario" Visible="False"></asp:Label>

<%--		</form>
	</body>
</HTML>
--%>

				</div>
			</div>


</asp:Content>
