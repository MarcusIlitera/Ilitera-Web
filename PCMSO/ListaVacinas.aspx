<%@ Page language="c#" Inherits="Ilitera.Net.PCMSO.ListaVacinas" Codebehind="ListaVacinas.aspx.cs" %>

<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Ilitera.NET</title>
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
        <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.2.3/dist/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
        <link href="css/forms.css" rel="stylesheet" type="text/css" />
        <link href="css/tabelas.css" rel="stylesheet" type="text/css" />
		<LINK href="scripts/style.css" type="text/css" rel="stylesheet">
		<script language="JavaScript" src="scripts/validador.js"></script>

      

	    <style type="text/css">
            .auto-style1 {
                font: xx-small Verdana;
                border: 1px solid #7CC5A1;
                color: #004000;
                background-color: #FCFEFD;
                text-align: left;
                margin-bottom: 0px;
            }
            .auto-style2 {
                font: xx-small Verdana;
                color: #44926D;
                height: 323px;
            }
            .auto-style3 {
                font: xx-small Verdana;
                color: #44926D;
                height: 75px;
            }
            .auto-style4 {
                font: xx-small Verdana;
                color: #44926D;
                height: 196px;
            }
        </style>

      

	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0">
		<form method="post" runat="server" id="frmCadastroExtintores">
            <div class="container d-flex ms-5 ps-4 justify-content-center">
	            <div class="row gx-3 gy-2 mt-3" style="width: 400px">
               <div class="col-12 subtituloBG text-center" style="padding-top: 10px">
				        <asp:label id="lblTitulo" runat="server" CssClass="subtitulo"></asp:label>
                    </div>

                    <div class="col-12">
                        <div class="row">
                            <div class="col-6 gx-3 gy-2">
                                <asp:label id="Label1" runat="server" CssClass="tituloLabel">Vacinas</asp:label>
                                <asp:listbox id="lst_Vacinas" runat="server" CssClass="texto form-control form-control-sm" TextMode="MultiLine" Rows="2" Height="126px" AutoPostBack="True" OnSelectedIndexChanged="lst_Vacinas_SelectedIndexChanged"></asp:listbox>
                                <asp:Button ID="cmd_Add_Vacina" runat="server" CssClass="btn mt-1" Text="Adicionar Vacina" OnClick="cmd_Add_Vacina_Click" />
                                <asp:ListBox ID="lst_Id_Vacinas" runat="server" CssClass="texto form-control form-control-sm"  Height="16px" Visible="False"></asp:ListBox>
                            </div>

                            <div class="col-6 gx-3 gy-2">
                                <asp:label id="Label2" runat="server" CssClass="tituloLabel">Doses</asp:label>
                                <asp:listbox id="lst_Doses" runat="server" CssClass="texto form-control form-control-sm" TextMode="MultiLine" Rows="2" Height="126px" AutoPostBack="True" OnSelectedIndexChanged="lst_Doses_SelectedIndexChanged"></asp:listbox>
                                <asp:ListBox ID="lst_Id_Doses" runat="server" CssClass="texto form-control form-control-sm" Height="16px" Visible="False"></asp:ListBox>
                                <asp:Button ID="cmd_Add_Dose" runat="server" CssClass="btn mt-1" Text="Adicionar Dose" OnClick="cmd_Add_Dose_Click" />
                                <asp:Button ID="cmd_Editar_Dose" runat="server" CssClass="btn mt-1" Text="Editar Dose" OnClick="cmd_Editar_Dose_Click" />
                                <asp:Label ID="lbl_Acao" runat="server" Text="1" CssClass="texto" Visible="False"></asp:Label>
                            </div>
                        </div>
                    </div>

                    <div class="col-md-12 gx-2 gy-2">
                        <asp:Label ID="lbl_Tipo" runat="server" Text="Adicionar Vacina" class="tituloLabel form-label col-form-label col-form-label-sm" Style="text-align: left;"></asp:Label>
                        <asp:textbox id="txtVacina" runat="server" CssClass="texto form-control form-control-sm" MaxLength="40"></asp:textbox>
                    </div>
                    <div class="col-md-4 gx-3 gy-2">
                         <asp:Label ID="lbl_Periodo" runat="server" Text="Período" class="tituloLabel form-label col-form-label col-form-label-sm" Style="text-align: left;"></asp:Label>
                         <asp:textbox id="txt_Periodo" runat="server" CssClass="texto form-control form-control-sm" MaxLength="3"></asp:textbox>
                    </div>
                    <div class="col-md-4 gx-3 gy-2">
                         <asp:Label ID="lbl_Periodicidade" runat="server" Text="Periodicidade" class="tituloLabel form-label col-form-label col-form-label-sm" Style="text-align: left;"></asp:Label>
                         <asp:DropDownList ID="cmb_Periodicidade" runat="server" AutoPostBack="True" CssClass="texto form-select">
                             <asp:ListItem Value="0">-</asp:ListItem>
                                  <asp:ListItem Value="1">Dia(s)</asp:ListItem>
                                  <asp:ListItem Value="2">Mês(es)</asp:ListItem>
                                  <asp:ListItem Value="4">Ano(s)</asp:ListItem>
                         </asp:DropDownList>
                    </div>

                    <div class="col-12 text-center pt-2 mt-2">
                        <asp:Button ID="btnGravar" runat="server" CssClass="btn" Text="Gravar" onclick="btnGravar_Click"  />
                        <asp:Button ID="btnExcluir" runat="server" CssClass="btn2" Text="Cancelar" onclick="btnExcluir_Click"  />
                    </div>
                </div>
            </div>

            <input id="txtCloseWindow" type="hidden"   runat="server"/>


<eo:MsgBox ID="MsgBox1" runat="server" BackColor="#47729F" ControlSkinID="None" 
        HeaderHtml="Dialog Title" Height="100px" Width="168px">
        <HeaderStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #ffbf00 3px solid; padding-left: 4px; font-weight: bold; font-size: 11px; padding-bottom: 2px; color: white; padding-top: 2px; border-bottom: #22456a 1px solid; font-family: verdana" />
        <ContentStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #7d97b6 1px solid; padding-left: 4px; border-left-width: 1px; font-size: 11px; border-left-color: #728eb8; padding-bottom: 4px; color: white; padding-top: 4px; border-bottom: #22456a 1px solid; font-family: verdana" />
        <FooterStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #7d97b6 1px solid; padding-left: 4px; border-left-width: 1px; font-size: 11px; border-left-color: #728eb8; padding-bottom: 4px; color: white; padding-top: 4px; border-bottom: #22456a 1px solid; font-family: verdana" />
    </eo:MsgBox>


                           
			<INPUT id="txtAuxiliar" type="hidden" name="txtAuxiliar" runat="server">
		</form>
	</body>
</HTML>