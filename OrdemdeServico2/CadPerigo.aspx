<%@ Page language="c#" Inherits="Ilitera.Net.CadPerigo"  Codebehind="CadPerigo.aspx.cs" %>
<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD runat="server">
		<title>Ilitera.NET</title>
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
                <link href="../bootstrap/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
 		<link href="css/forms.css" rel="stylesheet" type="text/css" />
        <link href="css/tabelas.css" rel="stylesheet" type="text/css" />
		<script language="javascript">
		function setNomeRisco(sNome)
		{
			if (document.getElementById("lsbRisco").options[0].selected)
			{	
				document.getElementById("btnExcluirRisco").disabled = true;
				document.getElementById("txtNomeRisco").value = "";
			}
			else
			{
				document.getElementById("btnExcluirRisco").disabled = false;
				document.getElementById("txtNomeRisco").value = sNome;
			}
			
			document.getElementById("txtNomeRisco").focus();
		}
		</script>
        <style type="text/css">
            @font-face{
                font-family: "Univia Pro";
                src: url("css/UniviaPro-Regular.otf");
            }
            @font-face {
                font-family: "Ubuntu";
                src: url("css/Ubuntu-Regular.ttf");
            }
            .btn, .btn2{
                           min-width: 100px !important;
                       }
        </style>



	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0">
		<form method="post" runat="server" defaultfocus="txtNomePerigo">
            <div class="container d-flex ms-5 ps-4 justify-content-center mt-2">
                <div class="row gx-5 gy-2" style="width: 700px">

                    <div class="col-12 subtituloBG mb-1 text-center" style="padding-top: 10px; margin-top: 20px;">
						    <asp:Label id="Label3" runat="server" SkinID="TitleFont" CssClass="subtitulo">Cadastro e Edição dos Perigos e Riscos associados</asp:Label>                       
                         </div>
<%--                        <asp:Panel ID="Panel1" runat="server" BorderStyle="None" BorderWidth="0px" DefaultButton="btnGravarPerigo"
                            Width="400px">

--%>                            <div class="col-12">
                                    <div class="row">
                                        <div class="col-6 gy-2">
                                            <asp:Label ID="Label1" runat="server" SkinID="BoldFont" CssClass="tituloLabel">Listagem dos Perigos</asp:Label>
                                            <asp:ListBox ID="lsbPerigos" runat="server" CssClass="texto form-control form-control-sm" Rows="5" AutoPostBack="True" OnSelectedIndexChanged="lsbPerigos_SelectedIndexChanged" ></asp:ListBox>
                                        </div>

                                        <div class="col-6 gy-2">
                                            <asp:Label ID="Label4" runat="server" SkinID="BoldFont" CssClass="tituloLabel">Nome Perigo</asp:Label>
                                            <asp:TextBox ID="txtNomePerigo" runat="server" CssClass="texto form-control form-control-sm" Rows="2" TextMode="MultiLine"></asp:TextBox>
     
                                            <div class="row mt-2 gx-2 gy-2">
                                                <div class="col-5">
                                                    <asp:Button ID="btnGravarPerigo" runat="server" Text="Gravar" CssClass="btn" OnClick="btnGravarPerigo_Click" />
                                                </div>
                                                <div class="col-5">
                                                    <asp:Button ID="btnExcluirPerigo" runat="server" Enabled="False" Text="Excluir" CssClass="btn2" OnClick="btnExcluirPerigo_Click" />
                                                </div>
                                            </div>  
                                        </div>
                                    </div>
                                </div>  

                    <%--                        </asp:Panel>
                        <asp:Image ID="Image1" runat="server" /><br />
                        <asp:Panel ID="Panel3" runat="server" BorderStyle="None" BorderWidth="0px" >
--%>

                    <div class="col-12">
                        <div class="row">
                            <div class="col-6 gx-3 gy-2">
                                <asp:Label ID="Label5" runat="server" CssClass="tituloLabel">Riscos do Perigo selecionado</asp:Label>
                                <asp:ListBox ID="lsbRiscoPerigo" runat="server" SelectionMode="Multiple" Height="77px" CssClass="texto form-control form-control-sm"></asp:ListBox><br />
                                <div class="row">
                                    <div class="col-12 text-center gy-2">
                                        <asp:ImageButton ID="imgRemoveRisco" runat="server" SkinID="ImgDown" ImageUrl="Images/descer.svg" ToolTip="Remover Risco do Perigo selecionado" OnClick="imgRemoveRisco_Click" />
                                        <asp:ImageButton ID="imbAddRisco" runat="server" SkinID="ImgUp" ImageUrl="Images/subir.svg" ToolTip="Adicionar Risco no Perigo selecionado" OnClick="imbAddRisco_Click" />
                                    </div>
                                </div>
                            </div>

                            <div class="col-6 gx-3 gy-2 mt-5">
                                <asp:Label ID="Label7" runat="server" CssClass="texto" Text="Selecione o Perigo para visualizar os Riscos associados. Adicione ou Remova os Riscos através das setas ao lado."></asp:Label>
                            </div>
                            
                            <div class="col-12">
                                    <div class="row">
                                        <div class="col-6 gy-2">
                                            <asp:Label ID="Label6" runat="server" CssClass="tituloLabel">Listagem dos Riscos</asp:Label>
						                    <asp:listbox id="lsbRisco" runat="server" Rows="5" CssClass="texto form-control form-control-sm"></asp:listbox>
                                        </div>
                                        <div class="col-6 gy-2">
						                    <asp:label id="Label2" runat="server" CssClass="tituloLabel">Nome Risco</asp:label>
						                    <asp:textbox id="txtNomeRisco" runat="server" Rows="2" TextMode="MultiLine" CssClass="texto form-control form-control-sm" ></asp:textbox>
                                            <div class="row mt-2 gy-2">
                                                <div class="col-5">
						                            <asp:Button id="btnGravarRisco" runat="server" Text="Gravar" CssClass="btn" OnClick="btnGravarRisco_Click" ></asp:Button>
                                                </div>
                                                <div class="col-5">
						                            <asp:Button id="btnExcluirRisco" runat="server" Text="Excluir" Enabled="False" CssClass="btn2" OnClick="btnExcluirRisco_Click" ></asp:Button>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                            <table border="0" cellpadding="0" cellspacing="0" class="defaultFont" width="700">
                            <tr>

                                <td align="center" width="330" valign="top">
                                    <br />
                            
                                             <asp:Label ID="lbl_Id_Empresa" runat="server" Text="IdEmpresa" Visible="False"></asp:Label>
                                             <asp:Label ID="lbl_Id_Usuario" runat="server" Text="IdUsuario" Visible="False"></asp:Label>
                            </td>
                            </tr>
                        </table>

                        </div>
                    </div>

                </div>
            </div>



         <eo:MsgBox ID="MsgBox1" runat="server" BackColor="#47729F" ControlSkinID="None" 
        HeaderHtml="Dialog Title" Height="100px" Width="168px">
        <HeaderStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #ffbf00 3px solid; padding-left: 4px; font-weight: bold; font-size: 11px; padding-bottom: 2px; color: white; padding-top: 2px; border-bottom: #22456a 1px solid; font-family: verdana" />
        <ContentStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #7d97b6 1px solid; padding-left: 4px; border-left-width: 1px; font-size: 11px; border-left-color: #728eb8; padding-bottom: 4px; color: white; padding-top: 4px; border-bottom: #22456a 1px solid; font-family: verdana" />
        <FooterStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #7d97b6 1px solid; padding-left: 4px; border-left-width: 1px; font-size: 11px; border-left-color: #728eb8; padding-bottom: 4px; color: white; padding-top: 4px; border-bottom: #22456a 1px solid; font-family: verdana" />
        </eo:MsgBox>

		</form>
	</body>
</HTML>
