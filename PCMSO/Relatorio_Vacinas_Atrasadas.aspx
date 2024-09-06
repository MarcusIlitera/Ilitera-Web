<%@ Page Language="C#"   AutoEventWireup="True"  MasterPageFile="~/Site.Master" CodeBehind="Relatorio_Vacinas_Atrasadas.aspx.cs"  Inherits="Ilitera.Net.PCMSO.Relatorio_Vacinas_Atrasadas" Title="Ilitera.Net" %>

<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">

        <link href="css/forms.css" rel="stylesheet" type="text/css" />
        <link href="css/tabelas.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        #txtIdUsuario
        {
            width: 0px;
        }
        #txtIdUsuario0
        {
            width: 0px;
        }
        #txtIdEmpregado
        {
            width: 0px;
        }
        #txtIdEmpregado0
        {
            width: 0px;
        }
        #txtIdEmpresa
        {
            width: 0px;
        }
        #SubDados
        {
            height: 317px;
        }
        #Table1
        {
            width: 713px;
        }
        #Table2
        {
            width: 725px;
        }
        .buttonFoto
        {}
        #Table13
        {
            height: 12px;
            width: 97px;
        }
    </style>
</asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent" >
    <div class="container-fluid d-flex ms-5 ps-4">
     <div class="row gx-3 gy-2 w-100">

	<script language="javascript">
        function Reload() {
            var f = document.getElementById('SubDados');
            //f.src = f.src;
            f.contentWindow.location.reload(true);
        }
    </script>



<%--<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head id="Head1" runat="server">
		<title>Ilitera.NET</title>
		<script language="JavaScript" src="scripts/validador.js"></script>
		<link href="scripts/style.css" type="text/css" rel="stylesheet">
	</head>
	<body bottommargin="0" leftmargin="0" topmargin="0" rightmargin="0">
		<form name="DadosEmpregado" method="post" runat="server">
--%>		

    <div class="col-11 subtituloBG" style="padding-top: 10px" >
            <asp:Label ID="lblNome" runat="server" class="subtitulo">Relatório de Convocação para Vacinas </asp:Label>
        </div>
                        <div class="col-12 mt-3 p-2" style="border: 1px solid silver; width: 91.5%; border-radius: 4px;">
                           <div class="row-12 ms-1 gx-4 gy-2" >
                                <div class="col-3 me-2 gx-4 gy-2 mb-4">
                                    <asp:Label id="Label12" runat="server" CssClass="tituloLabel form-label col-form-label col-form-label-sm mb-1">Empresa Selecionada</asp:Label>
                                    <asp:DropDownList ID="cmb_Empresa" runat="server" Visible="false" 
                                        CssClass="texto form-select form-select-sm">
                                        <asp:ListItem Value="1">Empresa selecionada</asp:ListItem>
                                        <asp:ListItem Value="2">Empresa selecionada e projetos</asp:ListItem>
                                        <asp:ListItem Value="3">Todas empresas do grupo</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-5 me-2 gx-4 gy-2 mb-2">
                                    <div class="row-6 ms-4 gx-2 gy-2 mt-2">
                                        <asp:CheckBox ID="chk_Setor" runat="server" AutoPostBack="True" OnCheckedChanged="chk_Setor_CheckedChanged"
                                            Cssclass="texto form-check-label border-0 bg-transparent gx-4 gy-2 mt-2" Text="Selecionar Setor:" />
                                    </div>
                                    <div class="row mt-2">
                                        <asp:DropDownList ID="cmb_Setor" runat="server" Enabled="False" CssClass="texto form-select form-select-sm">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>

                    <div class="row">
                        <div class="col-2 ms-2 gx-4 gy-2">
                            <asp:RadioButton ID="chk_PDF" runat="server"  Cssclass="texto form-check-label border-0 bg-transparent" AutoPostBack="true" oncheckedchanged="chk_PDF_CheckedChanged" GroupName="g2"
                            Text="Gerar PDF" Checked="true" />
                        </div>

                         <div class="col-2 gx-4 gy-2">
                             <asp:RadioButton ID="chk_CSV" runat="server" Cssclass="texto form-check-label border-0 bg-transparent" AutoPostBack="true" oncheckedchanged="chk_CSV_CheckedChanged" GroupName="g2"
                             Text="Gerar CSV" />
                         </div>
                    </div>

            </div>
                        <div>
                            <asp:button ID="btnemp" runat="server" onclick="btnemp_Click" Text="Gerar Relatório" CssClass="btn">
                            </asp:button>
                        </div>
                                        
                            <caption>
                                <input id="txtIdEmpresa" type="text" visible="False" style="visibility:hidden"/>
                            </caption>



            <eo:MsgBox ID="MsgBox1" runat="server" BackColor="#47729F" ControlSkinID="None" 
            HeaderHtml="Dialog Title" Height="100px" Width="168px">
            <HeaderStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #ffbf00 3px solid; padding-left: 4px; font-weight: bold; font-size: 11px; padding-bottom: 2px; color: white; padding-top: 2px; border-bottom: #22456a 1px solid; font-family: verdana" />
            <ContentStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #7d97b6 1px solid; padding-left: 4px; border-left-width: 1px; font-size: 11px; border-left-color: #728eb8; padding-bottom: 4px; color: white; padding-top: 4px; border-bottom: #22456a 1px solid; font-family: verdana" />
            <FooterStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #7d97b6 1px solid; padding-left: 4px; border-left-width: 1px; font-size: 11px; border-left-color: #728eb8; padding-bottom: 4px; color: white; padding-top: 4px; border-bottom: #22456a 1px solid; font-family: verdana" />
            </eo:MsgBox>
            
        </table>
    </div>
</div>


<%--		</form>
	</body>
</HTML>
--%>



    </asp:Content>