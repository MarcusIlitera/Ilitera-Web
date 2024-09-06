<%@ Page Language="C#"   AutoEventWireup="True"  MasterPageFile="~/Site.Master" CodeBehind="EntregaEPI_Auto.aspx.cs"  Inherits="Ilitera.Net.EntregaEPI_Auto" Title="Ilitera.Net" %>

<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>


<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <link href="css/tabelas.css" rel="stylesheet" type="text/css" />
    <link href="css/forms.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
    .defaultFont
    {
        width: 627px;
    }
        .inputBox {}
        .buttonBox {}
    </style>
</asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent" >
 <div class="container-fluid d-flex ms-5 ps-4">
   <div class="row gx-3 gy-2 w-100">

	   <LINK href="scripts/style.css" type="text/css" rel="stylesheet">


    <script language="javascript">
		//function VerificaData()
		//{
		//	return validar_data(document.forms[0].txtddr.value, document.forms[0].txtmmr.value, document.forms[0].txtaar.value, 'Data de Recebimento');
		//}

		//function Inicialize()
		//{
		//	document.forms[0].txtddr.onkeypress = ChecarTAB;
		//	document.forms[0].txtddr.onfocus = PararTAB;
		//	document.forms[0].txtddr.onkeyup = DiaR;
		//	document.forms[0].txtmmr.onkeypress = ChecarTAB;
		//	document.forms[0].txtmmr.onfocus = PararTAB;
		//	document.forms[0].txtmmr.onkeyup = MesR;
		//	document.forms[0].txtaar.onkeypress = ChecarTAB;
		//	document.forms[0].txtaar.onfocus = PararTAB;
		//	document.forms[0].txtaar.onkeyup = AnoR;
		//}

		//function DiaR()
		//{
		//	if (document.forms[0].txtddr.value.length == 2 && VerifiqueTAB == true)
		//		document.forms[0].txtmmr.focus();
		//}

		//function MesR()
		//{
		//	if (document.forms[0].txtmmr.value.length == 2 && VerifiqueTAB == true)
		//		document.forms[0].txtaar.focus();
		//}

		//function AnoR()
		//{
		//	if (document.forms[0].txtaar.value.length == 4 && VerifiqueTAB == true)
		//		document.forms[0].ckbRelatorioEntrega.focus();
		//}
    </script>

        
        

<%--	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0" onload="Inicialize()">
		<form method="post" runat="server" id="frmEntregaEPI">
--%>			
	   
	  
    <div class="col-11 subtituloBG pt-2">
       <asp:Label ID="lblEntregaEPI" runat="server" SkinID="TitleFont" CssClass="subtitulo">Entrega de EPI automático a todos os colaboradores</asp:Label>
   </div>


	   <br />
	   <br />
	   
  <div class="col-11 mb-3">
	<div class="row">
	 <div class="col-md-5 gx-3 gy-2">
		<asp:label id="lblSelecioneEPI" runat="server" CssClass="tituloLabel form-label">Selecione o EPI:</asp:label>&nbsp; 
	    <asp:dropdownlist id="ddlEPI" onselectedindexchanged="ddlEPI_SelectedIndexChanged"  runat="server" CssClass="texto form-select" AutoPostBack="True"></asp:dropdownlist>
	</div>
	
	  <div class="col-md-3 gx-3 mt-2">
         <asp:Label ID="lblEntregue" runat="server" CssClass="tituloLabel form-label col-form-label">Quantidade Entregue</asp:Label>
         <asp:TextBox ID="txtQtdEntregue" runat="server" CssClass="texto form-control form-control-sm" MaxLength="4" Width="56px"></asp:TextBox>
     </div>

		<div class="col-4"></div>

	 <div class="col-md-5 gx-3 gy-2">
		<asp:label id="lblCA" runat="server" CssClass="tituloLabel form-label">Selecione o CA:</asp:label>&nbsp; 
	    <asp:dropdownlist id="ddlCA" runat="server" CssClass="texto form-select" AutoPostBack="True"></asp:dropdownlist>
	 </div>
	

	   <div class="col-md-3 mt-4">
		   <asp:button id="btnAdicionarEPI" onclick="btnAdicionarEPI_Click" onselectedindexchanged="ddlEPI_SelectedIndexChanged" runat="server" CssClass="btn" Text="Adicionar EPI"></asp:button>
	   </div>
		</div>
	  </div>
			
					<TD class="normalFont" align="center"><BR>
						<asp:datagrid id="DGridItemsEPI" runat="server" CssClass="tabelaC" Width="595px" GridLines="Vertical"
							PageSize="3" AllowPaging="True" AutoGenerateColumns="False"
							CellPadding="3" HorizontalAlign="Center" BorderStyle="Groove">
							<AlternatingItemStyle CssClass="alternatingItem"></AlternatingItemStyle>
							<ItemStyle CssClass="tableItem"></ItemStyle>
							<HeaderStyle Font-Bold="True" CssClass="tableHeader"></HeaderStyle>
							<FooterStyle ForeColor="#000066" BackColor="White"></FooterStyle>
							<Columns>
								<asp:BoundColumn Visible="False" DataField="IdEPIClienteCA" HeaderText="IdEPIClienteCA">
									<HeaderStyle Width="0px"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="IdEPI" HeaderText="IdEPI">
									<HeaderStyle Width="0px"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="IdCA" HeaderText="IdCA">
									<HeaderStyle Width="0px"></HeaderStyle>
								</asp:BoundColumn>
								<asp:ButtonColumn DataTextField="Remover" HeaderText="-----" CommandName="RemoverEPI" Visible="false">
									<HeaderStyle Width="80px"></HeaderStyle>
									<ItemStyle Font-Bold="True"></ItemStyle>
								</asp:ButtonColumn>
								<asp:BoundColumn DataField="NomeEPI" ReadOnly="True" HeaderText="EPI">
									<HeaderStyle Width="255px"></HeaderStyle>
								</asp:BoundColumn>
								<asp:TemplateColumn HeaderText="CA">
									<HeaderStyle Width="100px"></HeaderStyle>
									<ItemTemplate>
										<%#DataBinder.Eval(Container.DataItem, "NumeroCA")%>
									</ItemTemplate>
								<%--	<EditItemTemplate>
										<asp:dropdownlist id=ddlCAEditar runat="server" CssClass="inputBox" DataTextField="NumeroCA" DataSource='<%#GetDropDownCAEditar((string)DataBinder.Eval(Container.DataItem, "IdCA"), (string)DataBinder.Eval(Container.DataItem, "IdEPI"))%>' DataValueField="IdCA">
										</asp:dropdownlist>
									</EditItemTemplate>--%>
								</asp:TemplateColumn>
   								<asp:BoundColumn DataField="QtdEntregue" ReadOnly="True" HeaderText="Qtde.">
									<HeaderStyle Width="125px"></HeaderStyle>
								</asp:BoundColumn>

							<%--	<asp:TemplateColumn HeaderText="Qtd. Entregue">
									<HeaderStyle Width="100px"></HeaderStyle>
									<ItemTemplate>
										<%#DataBinder.Eval(Container.DataItem, "QtdEntregue")%>
									</ItemTemplate>
									<EditItemTemplate>
										<asp:textbox id="txtQtdEntregueEditar" runat="server" CssClass="inputBox" Width="50px" Text='<%#DataBinder.Eval(Container.DataItem, "QtdEntregue")%>'>
										</asp:textbox>
										<asp:textbox id="txtIdCA" runat="server" CssClass="inputBox" Text='<%#DataBinder.Eval(Container.DataItem, "IdCA")%>' Width="0px">
										</asp:textbox>
										<asp:textbox id="txtIdEPI" runat="server" CssClass="inputBox" Text='<%#DataBinder.Eval(Container.DataItem, "IdEPI")%>' Width="0px">
										</asp:textbox>
									</EditItemTemplate>
								</asp:TemplateColumn>--%>
								<asp:EditCommandColumn ButtonType="LinkButton" Visible="false" UpdateText="&lt;img src='img/update.gif' border=0 alt='Atualizar'&gt;"
									HeaderText="-----" CancelText="&lt;img src='img/cancel.gif' border=0 alt='Cancelar'&gt;" EditText="Editar">
									<HeaderStyle Width="60px"></HeaderStyle>
								</asp:EditCommandColumn>
							</Columns>
							<PagerStyle CssClass="tablePage" Mode="NumericPages"></PagerStyle>
						</asp:datagrid><asp:label id="lblTotRegistros" runat="server" CssClass="normalFont"></asp:label></TD>
				</TR>
				<TR>
					<TD class="normalFont" align="center"><BR>
						<asp:label id="lblEmpregado" runat="server" CssClass="boldFont" Visible="False">Selecione o Empregado:</asp:label><asp:dropdownlist id="ddlEmpregado" runat="server" CssClass="inputBox" Visible="False"></asp:dropdownlist><BR>
						<BR>

    <div class="col-8 mt-4 gx-4">
	  <asp:label id="lblDataCompra" runat="server" CssClass="tituloLabel form-label col-form-label">Data de Recebimento:</asp:label>
	  <asp:TextBox ID="txt_Data" runat="server" CssClass="texto form-control form-control-sm" Width="82px" MaxLength="10"></asp:TextBox>
	</div>
  
                     

                        </b>&nbsp;&nbsp;&nbsp; 
						&nbsp;<asp:checkbox id="ckbRelatorioEntrega" runat="server" CssClass="boldFont" Text="Gerar Relatório de Entrega" Visible="False"></asp:checkbox><BR>
						<BR>


<div class="col-11">
   <div class="row">
	   <div class="col-md-5 gx-2 mt-4 me-2">
      <asp:button id="btnGravar" onclick="btnGravar_Click" runat="server" CssClass="btn" Text="Gravar Recebimento aos colaboradores relacionados ao EPI" Width="417px"></asp:button>
   </div>
 
   <div class="col-md-5 gx-3 mt-4">
      <asp:button id="lblCancel" onclick="lblCancel_Click"  runat="server" CssClass="btn2" Text="Cancelar" Width="88px"></asp:button>
    </div>
	</div>
	</div>
                    
						
						<asp:label id="lblError" runat="server" CssClass="errorFont"></asp:label>

                                  <asp:Label ID="lbl_Id_Empresa" runat="server" Text="IdEmpresa" Visible="False"></asp:Label>
                            &nbsp;&nbsp;
                            <asp:Label ID="lbl_Id_Usuario" runat="server" Text="IdUsuario" Visible="False"></asp:Label>
                        </TD>
				</TR>
			</TABLE>
<%--		</form>
	</body>
</HTML>
--%>

        
         <eo:MsgBox ID="MsgBox1" runat="server" BackColor="#47729F" ControlSkinID="None" 
        HeaderHtml="Dialog Title" Height="100px" Width="348px">
        <HeaderStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #ffbf00 3px solid; padding-left: 4px; font-weight: bold; font-size: 11px; padding-bottom: 2px; color: white; padding-top: 2px; border-bottom: #22456a 1px solid; font-family: verdana" />
        <ContentStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #7d97b6 1px solid; padding-left: 4px; border-left-width: 1px; font-size: 11px; border-left-color: #728eb8; padding-bottom: 4px; color: white; padding-top: 4px; border-bottom: #22456a 1px solid; font-family: verdana" />
        <FooterStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #7d97b6 1px solid; padding-left: 4px; border-left-width: 1px; font-size: 11px; border-left-color: #728eb8; padding-bottom: 4px; color: white; padding-top: 4px; border-bottom: #22456a 1px solid; font-family: verdana" />
        </eo:MsgBox>

  </div>
   </div>
</asp:Content>