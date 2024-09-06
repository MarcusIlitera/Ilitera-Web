
<%@ Page language="c#" validateRequest=false Inherits="Ilitera.Net.Agendamento_Auto" Codebehind="Agendamento_Auto.aspx.cs" %>
<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>


<HTML>
	<HEAD>
		<title>Ilitera</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio 7.0">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	    <style type="text/css">
            .auto-style1 {
                width: 121px;
            }
            .auto-style2 {
                width: 477px;
            }
        </style>
	</HEAD>
	<body>
        <form method="post" runat="server">
        		  <br />
                  <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="Larger" Text="Tela de Agendamento de Exame - Ili.Net"></asp:Label>
                  <br />
                  <br />
                  <br />
                  <asp:Label ID="Label2" runat="server" Font-Names="Arial" Text="Entre com senha de agendamento:"></asp:Label>
&nbsp;
                  <asp:TextBox ID="txtSenha" runat="server" MaxLength="10" TextMode="Password"></asp:TextBox>
&nbsp;<asp:Label ID="lblSenhaValidada" runat="server" Font-Bold="True" Font-Names="Arial" ForeColor="Red" Text="Senha Validada" Visible="False"></asp:Label>
                  &nbsp;&nbsp;
                  <asp:Button ID="cmdConfirmar" runat="server" Text="Confirmar" OnClick="cmdConfirmar_Click" />
                  <br />
                  <br />
                  <br />

            <table>

            <tr>
                <td class="auto-style1">
                  <asp:Label ID="lblTit1" runat="server" Font-Bold="True" Font-Names="Arial" Text="Colaborador:" Visible="False"></asp:Label>
                </td>
                <td class="auto-style2">
                  <asp:Label ID="lblColaborador" runat="server" Font-Names="Arial" Text="Nome" Visible="False"></asp:Label>
                </td>
            </tr>
                  <br />
                  <br />
               <tr>
                <td class="auto-style1">
                  <asp:Label ID="lblTit2" runat="server" Font-Bold="True" Font-Names="Arial" Text="Empresa:" Visible="False"></asp:Label>
                    </td>
                   <td class="auto-style2">
                  <asp:Label ID="lblEmpresa" runat="server" Font-Names="Arial" Text="Empresa" Visible="False"></asp:Label>
                       </td>
                   </tr>
                  <br />
                  <br />
                <tr>
                    <td class="auto-style1">
                  <asp:Label ID="lblTit3" runat="server" Font-Bold="True" Font-Names="Arial" Text="Clínica:" Visible="False"></asp:Label>
                        </td>
                    <td class="auto-style2">
                  <asp:Label ID="lblClinica" runat="server" Font-Names="Arial" Text="Clínica + Endereço" Visible="False"></asp:Label>
                        </td>
                    </tr>
                <tr>
                    <td class="auto-style1">
                  <asp:Label ID="lblTit4" runat="server" Font-Bold="True" Font-Names="Arial" Text="Data Planejada:" Visible="False"></asp:Label>
                        </td>
                    <td class="auto-style2">
                  <asp:Label ID="lblDataPlanejada" runat="server" Font-Names="Arial" Text="dd/MM/yyyy" Visible="False"></asp:Label>
                        </td>
                    </tr>
                <tr>
                    <td class="auto-style1">
                  <asp:Label ID="lblTit5" runat="server" Font-Bold="True" Font-Names="Arial" Text="Data Envio:" Visible="False"></asp:Label>
                        </td>
                    <td class="auto-style2">
                  <asp:Label ID="lblDataEnvio" runat="server" Font-Names="Arial" Text="dd/MM/yyyy" Visible="False"></asp:Label>
                        </td>
                    </tr>
                <tr>
                    <td class="auto-style1">
                  <asp:Label ID="lblTit6" runat="server" Font-Bold="True" Font-Names="Arial" Text="Exame:" Visible="False"></asp:Label>
                        </td>
                    <td class="auto-style2">
                  <asp:Label ID="lblExame" runat="server" Font-Names="Arial" Text="Exame" Visible="False"></asp:Label>
                  <asp:Label ID="lblIdExameDicionario" runat="server" Font-Names="Arial" Text="IdExame" Visible="False"></asp:Label>

                        </td>
                    </tr>
              </table>


                  <br />
                  <br />
                  <br />
                  <asp:Label ID="lbl_Data" runat="server" Font-Bold="True" Font-Names="Arial" Text="Datas disponíveis:" Visible="False"></asp:Label>


                  <br />
                  <br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                  <asp:RadioButton ID="rd_D1" runat="server" Font-Names="Arial" GroupName="Datas" Text="dd/MM/yyyy" Visible="False" />
&nbsp;&nbsp;
                  <asp:RadioButton ID="rd_D2" runat="server" Font-Names="Arial" GroupName="Datas" Text="dd/MM/yyyy" Visible="False" />
&nbsp;&nbsp;
                  <asp:RadioButton ID="rd_D3" runat="server" Font-Names="Arial" GroupName="Datas" Text="dd/MM/yyyy" Visible="False" />
&nbsp;&nbsp;
                  <asp:RadioButton ID="rd_D4" runat="server" Font-Names="Arial" GroupName="Datas" Text="dd/MM/yyyy" Visible="False" />
                  <br />
                  <br />
                  <br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                  <asp:Button ID="cmdAgendamento" runat="server" Text="Confirmar Agendamento para Data Selecionada" OnClick="cmdAgendamento_Click" Visible="False" />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                  <asp:Label ID="lbl_Token" runat="server" Text="0" Visible="False"></asp:Label>
                  <asp:Label ID="lbl_IdClinica" runat="server" Text="0" Visible="False"></asp:Label>
                  <asp:Label ID="lbl_IdEmpregado" runat="server" Text="0" Visible="False"></asp:Label>
                  <asp:Label ID="lbl_IdEmpresa" runat="server" Text="0" Visible="False"></asp:Label>
                  <br />
                  <br />
                  <%--  <asp:TextBox ID="txt_XML" runat="server" Height="384px" TextMode="MultiLine" Width="486px"></asp:TextBox>--%>
        <asp:TextBox ID="txt_Status" runat="server" Height="54px" Width="547px"></asp:TextBox>
		
            

<%--    <eo:MsgBox ID="MsgBox1" runat="server" BackColor="#47729F" ControlSkinID="None" 
        HeaderHtml="Dialog Title" Height="100px" Width="168px">
        <HeaderStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #ffbf00 3px solid; padding-left: 4px; font-weight: bold; font-size: 11px; padding-bottom: 2px; color: white; padding-top: 2px; border-bottom: #22456a 1px solid; font-family: verdana" />
        <ContentStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #7d97b6 1px solid; padding-left: 4px; border-left-width: 1px; font-size: 11px; border-left-color: #728eb8; padding-bottom: 4px; color: white; padding-top: 4px; border-bottom: #22456a 1px solid; font-family: verdana" />
        <FooterStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #7d97b6 1px solid; padding-left: 4px; border-left-width: 1px; font-size: 11px; border-left-color: #728eb8; padding-bottom: 4px; color: white; padding-top: 4px; border-bottom: #22456a 1px solid; font-family: verdana" />
        </eo:MsgBox>--%>

		          <p>
                      &nbsp;</p>

		</form>
	</body>
</HTML>
