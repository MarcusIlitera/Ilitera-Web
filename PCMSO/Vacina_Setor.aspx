<%@ Page Language="C#"   AutoEventWireup="True"  MasterPageFile="~/Site.Master" CodeBehind="Vacina_Setor.aspx.cs"  Inherits="Ilitera.Net.VacinaSetores" Title="Ilitera.Net" %>

<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>


<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <style type="text/css">
    .defaultFont
    {
        width: 627px;
    }
        #Table4
        {
            width: 594px;
        }
        #Table15
        {
            width: 214px;
        }
        #Table20
        {
            width: 184px;
        }
        #Table24
        {
            width: 333px;
        }
        #Table27
        {
            width: 583px;
        }
        .buttonFoto
        {}
    </style>
</asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent" >
    <script language="javascript">

	    function Reload() {
	        var f = document.getElementById('SubDados');
	        //f.src = f.src;
	        f.contentWindow.location.reload(true);
	    }


         function OnItemCommand(grid, itemIndex, colIndex, commandName) {
        
        //grid.raiseItemCommandEvent(itemIndex, commandName);
        grid.raiseItemCommandEvent(itemIndex, colIndex.toString());
       }



    </script>

  

<%--<iframe id="SubDados" name="SubDados" align="middle" marginWidth="0" marginHeight="0" frameBorder="0"
				width="600" scrolling="no" src="../DadosEmpresa/SubDadosNull.aspx" >
			</iframe>--%>		
	

<TABLE class="defaultFont" cellSpacing="0" cellPadding="0" width="600" align="center" border="0" bgcolor="white">

    
				<TR>
					<TD vAlign="top" align="center">                                            


                    
                        <br />


                    
                        <asp:Label ID="lblQueixas" runat="server" CssClass="boldFont" SkinID="BoldFont" Font-Bold="True">Vacina - Setor</asp:Label>
                        <br />
                        <br>
                        <asp:DropDownList ID="ddlVacina" runat="server" AutoPostBack="True" CssClass="inputBox" Height="20px" onselectedindexchanged="ddlVacina_SelectedIndexChanged" Width="275px">
                        </asp:DropDownList>
                        &nbsp;<br />
                        <br>
                        <asp:Button ID="cmd_Marcar_Todos" runat="server" BackColor="#999999" CssClass="buttonFoto" Font-Bold="True" Font-Size="XX-Small" Height="15px" onclick="cmd_Todos_Click" Text="Todos" Visible="False" Width="55px" />
                        <asp:CheckBoxList ID="chk_Setores" runat="server" AutoPostBack="True" BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" Font-Size="X-Small" Height="23px" RepeatColumns="3" Visible="False" Width="690px">
                        </asp:CheckBoxList>
                  <asp:Button ID="cmd_Salvar" runat="server" BackColor="#FF5050" 
                      CssClass="buttonFoto" Font-Size="XX-Small" onclick="cmd_Salvar_Click" 
                      Text="Gravar" Width="113px" />
                        <br />
                        <br />
                        <asp:Button ID="Button1" runat="server" BackColor="#999999" CssClass="buttonFoto" Font-Size="XX-Small" onclick="cmd_Voltar_Click" Text="Voltar" Width="127px" />


                    </TD>
				</TR>

				<%--		</form>
	</body>
</HTML>
--%>
			</TABLE>
			<%--<iframe id="SubDados" name="SubDados" align="middle" marginWidth="0" marginHeight="0" frameBorder="0"
				width="600" scrolling="no" src="../DadosEmpresa/SubDadosNull.aspx" >
			</iframe>--%>
<%--		</form>
	</body>
</HTML>
--%>

                  <br />
                  <asp:Button ID="cmd_Voltar" runat="server" BackColor="#999999" 
                      CssClass="buttonFoto" Font-Size="XX-Small" onclick="cmd_Voltar_Click" 
                      Text="Voltar" Width="132px" />
                  <br />

        
         <eo:MsgBox ID="MsgBox2" runat="server" BackColor="#47729F" ControlSkinID="None" 
        HeaderHtml="Dialog Title" Height="100px" Width="350px" OnButtonClick="MsgBox2_ButtonClick" >
        <HeaderStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #ffbf00 3px solid; padding-left: 4px; font-weight: bold; font-size: 11px; padding-bottom: 2px; color: white; padding-top: 2px; border-bottom: #22456a 1px solid; font-family: verdana" />
        <ContentStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #7d97b6 1px solid; padding-left: 4px; border-left-width: 1px; font-size: 11px; border-left-color: #728eb8; padding-bottom: 4px; color: white; padding-top: 4px; border-bottom: #22456a 1px solid; font-family: verdana" />
        <FooterStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #7d97b6 1px solid; padding-left: 4px; border-left-width: 1px; font-size: 11px; border-left-color: #728eb8; padding-bottom: 4px; color: white; padding-top: 4px; border-bottom: #22456a 1px solid; font-family: verdana" />
        </eo:MsgBox>


</asp:Content>
