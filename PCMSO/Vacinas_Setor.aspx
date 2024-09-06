<%@ Page Title="Ilitera.Net" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="True"
    CodeBehind="Vacinas_Setor.aspx.cs" Inherits="Ilitera.Net.Vacinas_Setor" %>

 

<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>


<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">

    <link href="css/forms.css" rel="stylesheet" type="text/css" />
    <link href="css/tabelas.css" rel="stylesheet" type="text/css" />

    <style type="text/css">
        .defaultFont
    {
        width: 689px;
            height: 20px;
        }
        
        .style3
        {
            width: 2453px;
        }
        .style4
        {
            width: 3032px;
        }
        .style5
        {
            width: 187px;
        }
        
        </style>
</asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent" >

     <div class="container-fluid d-flex ms-5 ps-4">
     <div class="row gx-3 gy-2 w-100">

    <eo:CallbackPanel runat="server" id="CallbackPanel1" Triggers="">
	
        <LINK href="scripts/style.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="scripts/validador.js"></script>
		<script language="javascript">
        var jaclicou = 0;

		function AbreCadastro(strPage, IdCliente, IdUsuario)
		{
			addItemPop(centerWin(strPage + '.aspx?IdEmpresa=' + IdCliente + '&IdUsuario=' + IdUsuario + '', 560, 370, 'CadQueixaClinica'));
        }

        				
		function VerificaProcesso()
		{
			if (jaclicou == 0)
				jaclicou=1;
			else
			{
				window.alert("Sua solicitação está sendo processada.\nAguarde!");
				return false;
			}
		}
		
		function ConsisteCkeckBoxDeAlteracao(checkBox){
			tipo = checkBox.id.substring(checkBox.id.length - 1, checkBox.id.length);
			if(tipo == 'S')
				checkBoxAux = eval('document.frmExameClinico.' + checkBox.id.substring(0, checkBox.id.length - 1) + 'N');
			if (tipo == 'N')	
				checkBoxAux = eval('document.frmExameClinico.' + checkBox.id.substring(0, checkBox.id.length - 1) + 'S');
			if (checkBoxAux.checked)  
				checkBoxAux.checked = !checkBox.checked
		}
		function btnAddQueixas_onclick() {

		}

        </script>

     
	<%--</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0">
		<form id="FormExameNaoOcupacional" method="post" runat="server">--%>
<%--        <igmisc:WebAsyncRefreshPanel ID="warpExameClinico" runat="server" CssClass="defaultFont"
                            Height="" Width="560px" InitializePanel="warpExameClinico_InitializePanel" RefreshComplete="warpExameClinico_RefreshComplete">
--%>
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
                        <asp:DropDownList ID="ddlVacina" runat="server"  CssClass="texto form-select form-select-sm">
                        </asp:DropDownList>
                      </fieldset>
                  </div>
                            </div>
                        </div>

                        <asp:Button ID="cmd_Marcar_Todos" runat="server" BackColor="#999999" CssClass="buttonFoto" Font-Bold="True" Font-Size="XX-Small" Height="15px" Text="Todos" Visible="False" Width="55px" />
                        <asp:CheckBoxList ID="chk_Setores" runat="server" AutoPostBack="True" BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" Font-Size="X-Small" Height="23px" RepeatColumns="3" Visible="False" Width="690px">
                        </asp:CheckBoxList>
                        <asp:Button ID="btnOK" runat="server" CssClass="buttonBox" Text="Gravar" Visible="False" Width="70px" />
                        <br />
                        <br />
                        <asp:Button ID="cmd_Voltar" runat="server" CssClass="btn" Text="Voltar" Width="127px" />
                   
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