<%@ Page Language="C#"   AutoEventWireup="True"  MasterPageFile="~/Site.Master" CodeBehind="AddFornecedor.aspx.cs"  Inherits="Ilitera.Net.AddFornecedor" Title="Ilitera.Net" %>


<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>


<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <style type="text/css">
    .defaultFont
    {
        width: 627px;
    }
    </style>
</asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent" >


			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="310" align="center" border="0">
				<TR>
					<TD class="normalFont" align="center">
						<P><BR>
							<asp:label id="lblCadFornecedor" runat="server" CssClass="largeboldFont">Edição e Cadastro de Fornecedor</asp:label></P>
					</TD>
				</TR>
				<TR>
					<TD class="normalFont" align="center"><BR>
						<asp:label id="lblSelFornecedor" runat="server" CssClass="BoldFont">Selecione o Fornecedor:</asp:label><BR>
						<asp:dropdownlist id="ddlFornecedor" runat="server" CssClass="inputBox" AutoPostBack="True" Width="290px" onselectedindexchanged="ddlFornecedor_SelectedIndexChanged"></asp:dropdownlist><BR>
						<BR>
						<asp:label id="lblFabricante" runat="server" CssClass="BoldFont">Nome do Fornecedor:</asp:label><BR>
						<asp:textbox id="txtFornecedor" runat="server" CssClass="inputBox" Width="290px"></asp:textbox></TD>
				</TR>
				<TR>
					<TD class="normalFont" align="center"><BR>
						<asp:button id="btnGravar" runat="server" CssClass="buttonBox" Text="Gravar" Width="60px" onclick="btnGravar_Click"></asp:button>&nbsp;&nbsp;
						<asp:button id="btnExcluir" runat="server" CssClass="buttonBox" Width="60px" Text="Excluir" onclick="btnExcluir_Click"></asp:button>&nbsp;&nbsp;
						<asp:button id="btnCancela" runat="server" CssClass="buttonBox" Text="Cancela" 
                            Width="60px" onclick="btnCancela_Click"></asp:button><BR>
						<BR>
						<asp:label id="lblError" runat="server" CssClass="errorFont"></asp:label>
                        
                            <asp:Label ID="lbl_Id_Empresa" runat="server" Text="IdEmpresa" Visible="False"></asp:Label>
                            &nbsp;&nbsp;
                            <asp:Label ID="lbl_Id_Usuario" runat="server" Text="IdUsuario" Visible="False"></asp:Label>

                        
                        </TD>
				</TR>
			</TABLE>

                    
         <eo:MsgBox ID="MsgBox1" runat="server" BackColor="#47729F" ControlSkinID="None" 
        HeaderHtml="Dialog Title" Height="100px" Width="168px">
        <HeaderStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #ffbf00 3px solid; padding-left: 4px; font-weight: bold; font-size: 11px; padding-bottom: 2px; color: white; padding-top: 2px; border-bottom: #22456a 1px solid; font-family: verdana" />
        <ContentStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #7d97b6 1px solid; padding-left: 4px; border-left-width: 1px; font-size: 11px; border-left-color: #728eb8; padding-bottom: 4px; color: white; padding-top: 4px; border-bottom: #22456a 1px solid; font-family: verdana" />
        <FooterStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #7d97b6 1px solid; padding-left: 4px; border-left-width: 1px; font-size: 11px; border-left-color: #728eb8; padding-bottom: 4px; color: white; padding-top: 4px; border-bottom: #22456a 1px solid; font-family: verdana" />
        </eo:MsgBox>

</asp:Content>
