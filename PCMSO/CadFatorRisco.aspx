<%@ Page Language="C#"   AutoEventWireup="True" CodeBehind="CadFatorRisco.aspx.cs"  Inherits="Ilitera.Net.PCMSO.CadFatorRisco" Title="Ilitera.Net" %>

<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Ilitera.NET</title>
        <link href="scripts/style.css" type="text/css" rel="stylesheet">
        <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.2.3/dist/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
        <link href="css/forms.css" rel="stylesheet" type="text/css" />
        <link href="css/tabelas.css" rel="stylesheet" type="text/css" /> 
</head>
<body bottommargin="0" leftmargin="0" rightmargin="0" topmargin="0">
    <form id="form1" runat="server">
        <div class="container d-flex ms-5 mt-2 ps-4 justify-content-center">
            <div class="row gx-3 gy-2 text-start" style="width: 360px;">

                <div class="col-12 subtituloBG text-center" style="padding-top: 10px; margin-top: 20px;">
                    <asp:Label ID="Label1" runat="server" SkinID="TitleFont" CssClass="subtitulo" Style="margin-left: 0px !important;" Text="Cadastro/Edição de Fatores de Risco"
                        ToolTip="Cadastro/Edição de Fatores de Risco"></asp:Label>
                </div>

                <div class="col-12 gy-2">
                    <asp:ListBox ID="lsbFatorRisco" runat="server" AutoPostBack="True" Rows="7" CssClass="texto form-control form-control-sm" OnSelectedIndexChanged="lsbProcedimentoClinico_SelectedIndexChanged"></asp:ListBox>
                </div>

                <div class="col-12 gy-2">
                    <asp:Label ID="Label2" runat="server" SkinID="BoldFont" Text="Nome" CssClass="tituloLabel"></asp:Label>
                    <asp:TextBox ID="txtNome" runat="server" CssClass="texto form-control form-control-sm"></asp:TextBox>
                </div>

                <div class="col-12">
                    <asp:Label ID="Label3" runat="server" SkinID="BoldFont" Text="Descrição" CssClass="tituloLabel form-label"></asp:Label>
                    <asp:TextBox ID="txtDescricao" runat="server" Rows="2" TextMode="MultiLine" CssClass="texto form-control form-control-sm"></asp:TextBox>
                </div>

                 <div class="col-12">
                    <div class="row">
                        <div class="text-center gy-4 mb-3 ms-2">
                            <asp:Button ID="btnGravar" runat="server" Text="Gravar" ToolTip="Gravar Fator de Risco" CssClass="btn" OnClick="btnGravar_Click" />
                             <asp:Button ID="btnExcluir" runat="server" Enabled="False" Text="Excluir" ToolTip="Excluir Fator de Risco" Width="60px" CssClass="btn" OnClick="btnExcluir_Click" />
                         </div>
                      </div>
                 </div>

            </div>
        </div>
    </form>
</body>
</html>