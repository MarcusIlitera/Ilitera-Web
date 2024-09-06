<%@ Page Language="C#"   AutoEventWireup="True"  CodeBehind="CadAspecto.aspx.cs"  Inherits="Ilitera.Net.OrdemDeServico.CadAspecto" Title="Ilitera.Net" %>


<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>



		<script language="javascript">
		function setNomeImpacto(sNome)
		{
			if (document.getElementById("lsbImpactos").options[0].selected)
			{	
				document.getElementById("btnExcluirImpacto").disabled = true;
				document.getElementById("txtNomeImpacto").value = "";
			}
			else
			{
				document.getElementById("btnExcluirImpacto").disabled = false;
				document.getElementById("txtNomeImpacto").value = sNome;
			}
			
			document.getElementById("txtNomeImpacto").focus();
		}
		</script>


	<body bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0">
		<form id="Form1" method="post" runat="server" defaultfocus="txtNomePerigo">

			<TABLE class="defaultFont" id="Table1" cellSpacing="0" cellPadding="0" width="400" align="center"
				border="0">
				<TR>
					<TD align="center"><BR>
						<asp:Label id="Label3" runat="server" SkinID="TitleFont">Cadastro e Edição dos Aspectos e Impactos associados</asp:Label><BR>
                        <br />
                        <asp:Panel ID="Panel1" runat="server" BorderStyle="None" BorderWidth="0px" DefaultButton="btnGravarAspecto"
                            Width="400px">
                            <table border="0" cellpadding="0" cellspacing="0" class="defaultFont" width="400">
                                <tr>
                                    <td align="center" width="250" valign="top">
                                        <asp:Label ID="Label1" runat="server" SkinID="BoldFont">Listagem dos Aspectos Ambientais</asp:Label><br />
                                        <asp:ListBox ID="lsbAspectos" runat="server" Width="230px" Rows="5" AutoPostBack="True" OnSelectedIndexChanged="lsbAspectos_SelectedIndexChanged"></asp:ListBox></td>
                                    <td width="150" align="center" valign="top">
                                        <asp:Label ID="Label4" runat="server" SkinID="BoldFont">Nome Aspecto</asp:Label><br />
                                        <asp:TextBox ID="txtNomeAspecto" runat="server" Width="130px" Rows="2" TextMode="MultiLine"></asp:TextBox><br />
                                        <br />
                                        <asp:Button ID="btnGravarAspecto" runat="server" Text="Gravar" Width="60px" OnClick="btnGravarAspecto_Click" />&nbsp;
                                        <asp:Button ID="btnExcluirAspecto" runat="server" Enabled="False" OnClick="btnExcluirAspecto_Click"
                                            Text="Excluir" Width="60px" /></td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <asp:Image ID="Image1" runat="server" /><br />
                            <table border="0" cellpadding="0" cellspacing="0" class="defaultFont" width="400">
                                <tr>
                                    <td align="center" width="250">
                                        <asp:Label ID="Label5" runat="server" SkinID="BoldFont">Impactos do Aspecto selecionado</asp:Label><br />
                                        <asp:ListBox ID="lsbImpactoAspecto" runat="server" Width="230px" SkinID="YellowList" SelectionMode="Multiple"></asp:ListBox><br />
                        <asp:Image ID="Image2" runat="server" /><br />
                                        <asp:ImageButton ID="imgRemoveImpacto" runat="server" SkinID="ImgDown" OnClick="imgRemoveImpacto_Click" ToolTip="Remover Impacto do Aspecto selecionado" />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:ImageButton ID="imbAddImpacto" runat="server" SkinID="ImgUp" OnClick="imbAddImpacto_Click" ToolTip="Adicionar Impacto no Aspecto selecionado" /></td>
                                    <td align="center" width="150" valign="middle">
                                        <asp:Label ID="Label7" runat="server" Text="Selecione o Aspecto para visualizar os Impactos associados. Adicione ou Remova os Impactos através das setas ao lado." Width="130px"></asp:Label></td>
                                </tr>
                            </table>
                        <asp:Image ID="Image3" runat="server" /><BR>
                        <asp:Panel ID="Panel2" runat="server" BorderStyle="None" BorderWidth="0px" DefaultButton="btnGravarImpacto"
                            Width="400px">
                            <table border="0" cellpadding="0" cellspacing="0" class="defaultFont" width="400">
                            <tr>
                                <td align="center" width="250" valign="top">
                                    <asp:Label ID="Label6" runat="server" SkinID="BoldFont">Listagem dos Impactos Ambientais</asp:Label><br />
						<asp:listbox id="lsbImpactos" runat="server" Width="230px" Rows="5"></asp:listbox></td>
                                <td align="center" width="150" valign="top">
						<asp:label id="Label2" runat="server" SkinID="BoldFont">Nome Impacto</asp:label><br />
						<asp:textbox id="txtNomeImpacto" runat="server" Width="130px" Rows="2" TextMode="MultiLine"></asp:textbox><br />
                                    <br />
						<asp:Button id="btnGravarImpacto" runat="server" Width="60px" Text="Gravar" onclick="btnGravarImpacto_Click"></asp:Button>&nbsp;
						<asp:Button id="btnExcluirImpacto" runat="server" Text="Excluir" Width="60px"
							Enabled="False" onclick="btnExcluirImpacto_Click"></asp:Button>
                            
                            
                                            <asp:Label ID="lbl_Id_Empresa" runat="server" Text="IdEmpresa" Visible="False"></asp:Label>
                                             <asp:Label ID="lbl_Id_Usuario" runat="server" Text="IdUsuario" Visible="False"></asp:Label>
                              
                            </td>
                            </tr>
                        </table>
                        </asp:Panel>
                    </TD>
				</TR>
			</TABLE>


         <eo:MsgBox ID="MsgBox1" runat="server" BackColor="#47729F" ControlSkinID="None" 
        HeaderHtml="Dialog Title" Height="100px" Width="168px">
        <HeaderStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #ffbf00 3px solid; padding-left: 4px; font-weight: bold; font-size: 11px; padding-bottom: 2px; color: white; padding-top: 2px; border-bottom: #22456a 1px solid; font-family: verdana" />
        <ContentStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #7d97b6 1px solid; padding-left: 4px; border-left-width: 1px; font-size: 11px; border-left-color: #728eb8; padding-bottom: 4px; color: white; padding-top: 4px; border-bottom: #22456a 1px solid; font-family: verdana" />
        <FooterStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #7d97b6 1px solid; padding-left: 4px; border-left-width: 1px; font-size: 11px; border-left-color: #728eb8; padding-bottom: 4px; color: white; padding-top: 4px; border-bottom: #22456a 1px solid; font-family: verdana" />
        </eo:MsgBox>

</form>
</body>
