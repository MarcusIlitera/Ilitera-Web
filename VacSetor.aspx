<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="VacSetor.aspx.cs"
    Inherits="Ilitera.Net.VacSetor" Title="Ilitera.Net" %>
 

<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>



<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">

    <link href="css/forms.css" rel="stylesheet" type="text/css" />
    <link href="css/tabelas.css" rel="stylesheet" type="text/css" />

       <style type="text/css">
        .linha
   {
	font: 8px Verdana, Arial, Helvetica, sans-serif, Tahoma;
    }
           .btnLogarClass
           {}
    
.boldFont
{
	font: Bold xx-small Verdana;
	color:#44926D;
}
           </style>

</asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent"  >

    <div class="container-fluid d-flex ms-5 ps-4">
    <div class="row gx-3 gy-2 w-100">


                    <div class="col-11 subtituloBG" style="padding-top: 10px" >
            <asp:Label runat="server" class="subtitulo">Vacinas - Setor</asp:Label>
        </div>

			<table class="normalFont" id="Table1" cellspacing="0" cellpadding="0" width="1050"
				border="0">
				<TR class="row">
					<TD><%--                    <igmisc:WebAsyncRefreshPanel ID="WebAsyncRefreshPanel1" runat="server" CssClass="defaultFont"
                            Height="" Width="560px" InitializePanel="warpExameClinico_InitializePanel" RefreshComplete="warpExameClinico_RefreshComplete">
--%>
                        
                        <br>
                        <div class="col-12">
                            <div class="row">
                 <div class="col-md-4 gy-2">
                      <fieldset>
                    <asp:Label ID="lblQueixas" runat="server" CssClass="tituloLabel form-label" Height="13">Vacina</asp:Label>
                        <asp:DropDownList ID="ddlVacina" onselectedindexchanged="ddlVacina_SelectedIndexChanged" runat="server"  CssClass="texto form-select form-select-sm" AutoPostBack="true">
                        </asp:DropDownList>
                        <asp:Label ID="lblUser" runat="server" Text="0" Visible="False"></asp:Label>
                        <asp:Label ID="lblEmpresa" runat="server" Text="0" Visible="False"></asp:Label>
                        <asp:Label ID="lblJuridicaPai" runat="server" Text="0" Visible="False"></asp:Label>
                        <asp:Label ID="lblEmpregado" runat="server" Text="0" Visible="False"></asp:Label>
                        <asp:Label ID="lblNomeEmpregado" runat="server" Text="0" Visible="False"></asp:Label>
                        <asp:Label ID="lblNomeEmpresa" runat="server" Text="0" Visible="False"></asp:Label>
                      </fieldset>
                  </div>
                            </div>
                        </div>

                <div class="col-md-12 mb-2 gx-3 gy-2">
                    <asp:CheckBoxList ID="chk_Setores" OnSelectedIndexChanged="chk_Setores_SelectedIndexChanged" runat="server" AutoPostBack="True" CssClass="texto form-check-input border-0 bg-transparent ms-3" Height="250px" RepeatColumns="3" Visible="False" Width="690px">
                    </asp:CheckBoxList>
                </div>

                <div class="col-12 mb-2 gx-3 gy-2">
                    <asp:Button ID="cmd_Marcar_Todos" onclick="cmd_Todos_Click" runat="server" CssClass="btn" Text="Todos" Visible="False" />
                </div>

                        
                        
                        <div class="row mt-3">
                            <asp:Button ID="cmd_Salvar" OnClick="cmd_Salvar_Click1"  runat="server" CssClass="btn" Text="Gravar" Width="70px" />
                            <asp:Button ID="Button1" onclick="cmd_Voltar_Click"  runat="server" CssClass="btn2" Text="Voltar" Width="127px" />
                        </div>
                        
                   
                </TD>
                </TR>


                
            <%--<input id="txtAuxiliar" runat="server" name="txtAuxiliar" type="hidden" />--%>
<%--		</form>
	</body>
</HTML>--%>

</TABLE>
    </eo:CallbackPanel>
       
</div>
 </div>
        
         <eo:MsgBox ID="MsgBox1" runat="server" BackColor="#47729F" ControlSkinID="None" 
        HeaderHtml="Dialog Title" Height="100px" Width="168px">
        <HeaderStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #ffbf00 3px solid; padding-left: 4px; font-weight: bold; font-size: 11px; padding-bottom: 2px; color: white; padding-top: 2px; border-bottom: #22456a 1px solid; font-family: verdana" />
        <ContentStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #7d97b6 1px solid; padding-left: 4px; border-left-width: 1px; font-size: 11px; border-left-color: #728eb8; padding-bottom: 4px; color: white; padding-top: 4px; border-bottom: #22456a 1px solid; font-family: verdana" />
        <FooterStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #7d97b6 1px solid; padding-left: 4px; border-left-width: 1px; font-size: 11px; border-left-color: #728eb8; padding-bottom: 4px; color: white; padding-top: 4px; border-bottom: #22456a 1px solid; font-family: verdana" />
        </eo:MsgBox>
    </asp:Content>