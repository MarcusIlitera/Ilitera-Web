<%@ Page Language="C#"   AutoEventWireup="True"  MasterPageFile="~/Site.Master" CodeBehind="CadConjuntoProcedimento.aspx.cs"  Inherits="Ilitera.Net.CadConjuntoProcedimento" Title="Ilitera.Net" %>
 
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
        #Table2
        {
            width: 630px;
        }
        #Table3
        {
            width: 635px;
        }
        .boldFont
        {
            font-weight: 700;
        }
    </style>
</asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent" >
	<div class="container-fluid d-flex ms-5 ps-4">
    <div class="row gx-3 gy-2">
		<script language="javascript" src="scripts\validador.js"></script>

		<div class="col-11 subtituloBG"  style="padding-top: 10px">
                <asp:label id="Label4" runat="server" EnableViewState="False" CssClass="subtitulo">Mecanismo de Busca para os Procedimentos da Empresa</asp:label>
                <asp:panel id="Panel2" runat="server" Width="662px"></asp:panel>
            </div>

	
        <div class="col-11">
        <div class="row">

		<%--Filtros--%>

		<div class="col-12 gx-3 gy-2">
		 <div class="row">
		<div class="col-12 gx-2 gy-2">
		    <asp:Label ID="Label19" runat="server" Text="Palavra-Chave" class="tituloLabel form-label"></asp:Label>
		</div>
		<div class="col-md-4 gy-2">
		   <asp:textbox id="txtProcedimento" onkeydown="ProcessaEnter(event, 'imgBusca')" runat="server" class="texto form-control form-control-sm"></asp:textbox>
		   </div>
		<div  class="col-md-5 gx-3 gy-2">
			<asp:imagebutton id="imgBusca" tabIndex="2" runat="server" AlternateText="Buscar Extintores" BorderWidth="0px" ImageUrl="Images/search.svg" 
			CssClass="btnMenor" style="padding: .5rem;" onclick="imgBusca_Click1" /> 
	   </div>
				
		</div>
		 </div>
																				

	   <div class="col-md-4 gx-3 gy-2">
            <asp:Label ID="Label1" runat="server"  EnableViewState="False" CssClass="tituloLabel form-label">Tipo</asp:Label>
            <asp:DropDownList ID="ddlTipoProcedimento" runat="server" AutoPostBack="True" CssClass="texto form-select form-select-sm"></asp:DropDownList>
       </div>

      <div class="col-md-4 gx-3 gy-2">
            <asp:Label ID="Label18" runat="server"  EnableViewState="False" CssClass="tituloLabel form-label">Equipamento</asp:Label>
            <asp:DropDownList ID="ddlEquipamento" runat="server" AutoPostBack="True" CssClass="texto form-select form-select-sm"></asp:DropDownList>
       </div>

	  <div class="col-md-4 gx-3 gy-2">
            <asp:Label ID="Label17" runat="server"  EnableViewState="False" CssClass="tituloLabel form-label">Setor</asp:Label>
            <asp:DropDownList ID="ddlSetor" runat="server" AutoPostBack="True" CssClass="texto form-select form-select-sm"></asp:DropDownList>
       </div>

	  <div class="col-md-4 gx-3 gy-2">
            <asp:Label ID="Label16" runat="server"  EnableViewState="False" CssClass="tituloLabel form-label">Produto</asp:Label>
            <asp:DropDownList ID="ddlProduto" runat="server" AutoPostBack="True" CssClass="texto form-select form-select-sm"></asp:DropDownList>
       </div>

	  <div class="col-md-4 gx-3 gy-2">
            <asp:Label ID="Label5" runat="server"  EnableViewState="False" CssClass="tituloLabel form-label">Célula</asp:Label>
            <asp:DropDownList ID="ddlCelula" runat="server" AutoPostBack="True" CssClass="texto form-select form-select-sm"></asp:DropDownList>
       </div>

	   <div class="col-md-4 gx-3 gy-2">
            <asp:Label ID="Label14" runat="server"  EnableViewState="False" CssClass="tituloLabel form-label">Ferramenta</asp:Label>
            <asp:DropDownList ID="ddlFerramenta" runat="server" AutoPostBack="True" CssClass="texto form-select form-select-sm"></asp:DropDownList>
       </div>

	   	<div class="col-12 gx-3 gy-2">
		 <div class="row">
	    <div class="col-md-4 gy-1">
            <asp:Label ID="Label13" runat="server"  EnableViewState="False" CssClass="tituloLabel form-label">Conjunto de Procedimentos</asp:Label>
            <asp:DropDownList ID="ddlConjunto" runat="server" onselectedindexchanged="ddlConjunto_SelectedIndexChanged" AutoPostBack="True" CssClass="texto form-select form-select-sm"></asp:DropDownList>
		</div>
         <div class="col-md-5 gx-3 gy-2" style="margin-top:23px;">
           <%--<INPUT class=buttonBox id=btnConjunto onclick="addItem(centerWin('CadConjunto.aspx?IdEmpresa=<%=cliente.Id%>&amp;IdUsuario=<%=usuario.Id%>',350,225,'ConjuntoProcedimento'), 'Empresa');" tabindex=0 type=button size=20 value=...>&nbsp;--%>
           <INPUT class=buttonBox id=btnConjunto onclick="centerWin('CadConjunto.aspx?IdEmpresa=<%=cliente.Id%>&amp;IdUsuario=<%=usuario.Id%>',350,225,'ConjuntoProcedimento');" type=button size=20 value=...>
	   </div>

	</div>
	 </div>
	<div class="col-11">
	 <div class="row">
		<div class="col-md-4 gx-3 gy-2">
			<asp:Label ID="Label12" runat="server" Text="Procedimentos da Empresa" CssClass="tituloLabel form-label"></asp:Label>
			<asp:ListBox ID="listBxProcedimento" runat="server" Height="106px" AutoPostBack="True" CssClass="texto form-control"></asp:ListBox>
	   
		 <div class="row">
			 <div class="col-12 text-end">
             <asp:label id="lblTotProcedimentos" runat="server" CssClass="boldFont" Width="120px"></asp:label>
			 </div>
        </div>
			</div>
		   <div class="col-2 gx-5 gy-2 text-center mt-3">
			<div class="row">
			<div class="col-12 gy-1">
			<asp:imagebutton id="imbAddProcedimento" runat="server" CssClass="btnMenor" Width="40px" Text=">" ToolTip="Adiciona Procedimentos selecionados" ImageUrl="Images/right.svg" Style="padding: .4rem;" onclick="imbAddProcedimento_Click1"></asp:imagebutton>
	   </div>
			</div>

	   <div class="col-12 gy-1">
			<asp:imagebutton id="imbAddAllProcedimento" runat="server" CssClass="btnMenor" Width="40px" Text=">>" ToolTip="Adiciona todos os Procedimentos" ImageUrl="Images/double-right.svg" Style="padding: .4rem;" onclick="imbAddAllProcedimento_Click1"></asp:imagebutton>
	   </div>

	   <div class="col-12 gy-1">
			<asp:imagebutton id="imbRemoveProcedimento" runat="server" CssClass="btnMenor" Width="40px" Text="<" ToolTip="Remove Procedimentos selecionados" ImageUrl="Images/left.svg" Style="padding: .4rem;" onclick="imbRemoveProcedimento_Click1"></asp:imagebutton>
	   </div>

	   <div class="col-12 gy-1">
			<asp:imagebutton id="imbRemoveAllProcedimento" runat="server" CssClass="btnMenor" Width="40px" Text="<<" ToolTip="Remove todos os Procedimentos" ImageUrl="Images/double-left.svg" Style="padding: .4rem;" onclick="imbRemoveAllProcedimento_Click1"></asp:imagebutton>
	   </div>
       

   </div>

		 <div class="col-md-4 pt-2 gx-3 gy-2">
		<asp:listbox id="listBxConjuntoProcedimento" runat="server" Height="106px" CssClass="texto form-control" style="margin-top: 15px"
		SelectionMode="Multiple" Rows="11"></asp:listbox>
	   </div>

		 <asp:label id="lblTotConjProcedimento" runat="server" CssClass="boldFont" Width="120px"></asp:label>

			<div class="col-12 gx-3 gy-2">
			<div class="row">
				<div class="col-2">
             <asp:linkbutton id="lkbListaTodos" runat="server" CssClass="btn" onclick="lkbListaTodos_Click">Listar Todos</asp:linkbutton>
        </div>

           <div class="col-1 mt-1">
             <asp:imagebutton id="imbConjuntoProcedimentos" runat="server" ImageUrl="Images/printer.svg" CssClass="btnMenor" Style="padding: .4rem;" ToolTip="Imprimir listagem dos Conjuntos de Procedimentos"></asp:imagebutton>
        </div>

      </div>
         </div>
	</div>
	 </div>

    </div>
      </div>				
									
				
                                                    &nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
								
									
							
									
									<TABLE class="normalFont" id="Table10" cellSpacing="0" cellPadding="0" align="center"
										border="0">
										<TR>

                                             <asp:Label ID="lbl_Id_Empresa" runat="server" Text="IdEmpresa" Visible="False"></asp:Label>
                                             <asp:Label ID="lbl_Id_Usuario" runat="server" Text="IdUsuario" Visible="False"></asp:Label>

										</TR>
									</TABLE>
								</TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
			<INPUT id="txtAuxiliar" type="hidden" name="txtAuxiliar" runat="server">

		</div>
		 </div>

	      <asp:Image id="Image1" runat="server" ImageUrl="img/5pixel.gif" Visible="false"></asp:Image>
          <asp:Image id="Image6" runat="server" ImageUrl="img/5pixel.gif" Visible="false"></asp:Image>
		<asp:image id="Image14" runat="server" ImageUrl="img/5pixel.gif" Visible="false"></asp:image>
      
	<TABLE class="normalFont" id="Table3" cellSpacing="0" cellPadding="0" align="center"
											border="0">
	
		<asp:Image id="Image2" runat="server" ImageUrl="img/5pixel.gif" Visible="false"></asp:Image>
		<asp:Image id="Image3" runat="server" ImageUrl="img/5pixel.gif" Visible="false"></asp:Image>
		<asp:Image id="Image4" runat="server" ImageUrl="img/5pixel.gif" Visible="false"></asp:Image>
		<asp:Image id="Image5" runat="server" ImageUrl="img/5pixel.gif" Visible="false"></asp:Image>

		<asp:Image id="Image7" runat="server" ImageUrl="img/5pixel.gif" Visible="false"></asp:Image>
		<asp:Image id="Image8" runat="server" ImageUrl="img/5pixel.gif" Visible="false"></asp:Image>
		<asp:Image id="Image9" runat="server" ImageUrl="img/5pixel.gif" Visible="false"></asp:Image>
		<asp:Image id="Image10" runat="server" ImageUrl="img/5pixel.gif" Visible="false"></asp:Image>

         <eo:MsgBox ID="MsgBox1" runat="server" BackColor="#47729F" ControlSkinID="None" 
        HeaderHtml="Dialog Title" Height="100px" Width="168px">
        <HeaderStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #ffbf00 3px solid; padding-left: 4px; font-weight: bold; font-size: 11px; padding-bottom: 2px; color: white; padding-top: 2px; border-bottom: #22456a 1px solid; font-family: verdana" />
        <ContentStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #7d97b6 1px solid; padding-left: 4px; border-left-width: 1px; font-size: 11px; border-left-color: #728eb8; padding-bottom: 4px; color: white; padding-top: 4px; border-bottom: #22456a 1px solid; font-family: verdana" />
        <FooterStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #7d97b6 1px solid; padding-left: 4px; border-left-width: 1px; font-size: 11px; border-left-color: #728eb8; padding-bottom: 4px; color: white; padding-top: 4px; border-bottom: #22456a 1px solid; font-family: verdana" />
        </eo:MsgBox>


</asp:Content>