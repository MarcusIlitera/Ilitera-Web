<%@ Page Title="Ilitera.Net" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="True"
    CodeBehind="Vacinas.aspx.cs" Inherits="Ilitera.Net.Vacinas" %>

<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <link href="css/forms.css" rel="stylesheet" type="text/css" />
    <link href="css/tabelas.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .defaultFont
    {
        width: 586px;
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
            function AbreCadastro(strPage, IdCliente, IdUsuario) {
                addItemPop(centerWin(strPage + '.aspx?IdEmpresa=' + IdCliente + '&IdUsuario=' + IdUsuario + '', 560, 370, 'CadQueixaClinica'));
            }

            function VerificaProcesso() {
                if (jaclicou == 0)
                    jaclicou = 1;
                else {
                    window.alert("Sua solicitação está sendo processada.\nAguarde!");
                    return false;
                }
            }

            function ConsisteCkeckBoxDeAlteracao(checkBox) {
                tipo = checkBox.id.substring(checkBox.id.length - 1, checkBox.id.length);
                if (tipo == 'S')
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

        <div class="col-11 subtituloBG mb-3" style="padding-top: 10px;">
            <asp:Label id="Label4" runat="server" CssClass="subtitulo">Vacinação - Colaborador</asp:Label>
        </div>

        <div class="col-11 mb-5">
            <div class="row">
                <div class="col-md-2 gx-3 gy-2">
                    <asp:Label ID="Label2" runat="server" SkinID="BoldFont" CssClass="tituloLabel">Data Vacina</asp:Label>
                    <asp:TextBox id="wdtDataVacina" runat="server" ImageDirectory=" " Nullable="False" DisplayModeFormat="dd/MM/yyyy" HorizontalAlign="Center" CssClass="texto form-control"></asp:TextBox>
                </div>

                <div class="col-md-5 gx-3 gy-2">
                    <div class="row">
                        <div class="col-12">
                            <asp:Label ID="lblQueixas" runat="server" SkinID="BoldFont" CssClass="tituloLabel">Vacina</asp:Label>
                        </div>

                        <div class="col-11 gx-1">
                            <asp:DropDownList ID="ddlVacina" onselectedindexchanged="ddlVacina_SelectedIndexChanged" runat="server" CssClass="texto form-select" AutoPostBack="True"></asp:DropDownList>
                        </div>

                        <div class="col-1 gx-1">
                            <asp:Button ID="cmd_Nova_Vacina" onclick="cmd_Nova_Vacina_Click"  runat="server" Text="..." CssClass="btnMenor" />
                        </div>
                    </div>
                </div>

                <div class="col-md-5 gx-3 gy-2">
                    <asp:Label ID="lblProcedimento" runat="server" SkinID="BoldFont" CssClass="tituloLabel">Dose</asp:Label>
                    <asp:DropDownList ID="ddlDose" runat="server" CssClass="texto form-select"></asp:DropDownList>
                </div>

                <div class="col-md-6 gx-3 gy-2">
                    <asp:Label ID="lblAnotacoes" runat="server" SkinID="BoldFont" CssClass="tituloLabel">Outras Observações </asp:Label>
                    <asp:TextBox ID="txtDescricao" runat="server" Rows="3" tabIndex="1" TextMode="MultiLine" CssClass="texto form-control"></asp:TextBox>
                </div>
            </div>
        </div>

        <div class="col-11 mb-3">
            <div class="row">

                <div class="col-12">
                    <div class="text-center">
                        <asp:Button ID="btnOK" onclick="btnOK_Click" runat="server" CssClass="btn" Text="OK" />
                        <asp:Button ID="btnExcluir" onclick="btnExcluir_Click" runat="server" CssClass="btn" Text="Excluir" />
                    </div>

                    <div class="text-start">
                        <asp:Button ID="cmd_Voltar" onclick="cmd_Voltar_Click" runat="server" CssClass="btn2" Text="Voltar" />
                    </div>
                </div>

            </div>
        </div>

        <input id="btnAddQueixas" runat="server" class="btnMenor" name="btnAddQueixas" type="button" value="..." visible="False" />
        <input id="btnAddProcedimento" runat="server" class="button" name="btnAddProcedimento" type="button" value="..." visible="False" />
        <input id="txtAuxiliar" runat="server" name="txtAuxiliar" type="hidden" />
        <input id="txtAuxAviso" type="hidden" runat="server"/>
        <input id="txtCloseWindow" type="hidden" runat="server"/>
        <input id="txtExecutePost" type="hidden" runat="server"/>
    </eo:CallbackPanel>
       

        
         <eo:MsgBox ID="MsgBox1" runat="server" BackColor="#47729F" ControlSkinID="None" 
        HeaderHtml="Dialog Title" Height="100px" Width="168px">
        <HeaderStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #ffbf00 3px solid; padding-left: 4px; font-weight: bold; font-size: 11px; padding-bottom: 2px; color: white; padding-top: 2px; border-bottom: #22456a 1px solid; font-family: verdana" />
        <ContentStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #7d97b6 1px solid; padding-left: 4px; border-left-width: 1px; font-size: 11px; border-left-color: #728eb8; padding-bottom: 4px; color: white; padding-top: 4px; border-bottom: #22456a 1px solid; font-family: verdana" />
        <FooterStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #7d97b6 1px solid; padding-left: 4px; border-left-width: 1px; font-size: 11px; border-left-color: #728eb8; padding-bottom: 4px; color: white; padding-top: 4px; border-bottom: #22456a 1px solid; font-family: verdana" />
        </eo:MsgBox>

            </div>
        </div>
    </asp:Content>
