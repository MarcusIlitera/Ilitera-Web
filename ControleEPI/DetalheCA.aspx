
<%@ Page language="c#" Inherits="Ilitera.Net.ControleEPI.DetalheCA" Codebehind="DetalheCA.aspx.cs" %>
<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Ilitera.NET</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="scripts/style.css" type="text/css" rel="stylesheet">
		<script language="javascript">
		<!-- #include file="scripts/validador.js" -->
		function VerificaData()
		{
			if (VerificaEmi() && VerificaVal())
				return true;
			else
				return false;
		}
		
		function VerificaEmi()
		{
			return validar_data(document.forms[0].txtdde.value, document.forms[0].txtmme.value, document.forms[0].txtaae.value, 'Data de Emissão do CA');
		}

		function VerificaVal()
		{
			return validar_data(document.forms[0].txtddv.value, document.forms[0].txtmmv.value, document.forms[0].txtaav.value, 'Data de Validade do CA');
		}
		
		function Inicialize()
		{
			document.forms[0].txtdde.onkeypress = ChecarTAB;
			document.forms[0].txtdde.onfocus = PararTAB;
			document.forms[0].txtdde.onkeyup = DiaE;
			document.forms[0].txtmme.onkeypress = ChecarTAB;
			document.forms[0].txtmme.onfocus = PararTAB;
			document.forms[0].txtmme.onkeyup = MesE;
			document.forms[0].txtaae.onkeypress = ChecarTAB;
			document.forms[0].txtaae.onfocus = PararTAB;
			document.forms[0].txtaae.onkeyup = AnoE;
			document.forms[0].txtddv.onkeypress = ChecarTAB;
			document.forms[0].txtddv.onfocus = PararTAB;
			document.forms[0].txtddv.onkeyup = DiaV;
			document.forms[0].txtmmv.onkeypress = ChecarTAB;
			document.forms[0].txtmmv.onfocus = PararTAB;
			document.forms[0].txtmmv.onkeyup = MesV;
			document.forms[0].txtaav.onkeypress = ChecarTAB;
			document.forms[0].txtaav.onfocus = PararTAB;
			document.forms[0].txtaav.onkeyup = AnoV;
		}
		
		function DiaE()
		{
			if (document.forms[0].txtdde.value.length == 2 && VerifiqueTAB == true)
				document.forms[0].txtmme.focus();
		}
		
		function MesE()
		{
			if (document.forms[0].txtmme.value.length == 2 && VerifiqueTAB == true)
				document.forms[0].txtaae.focus();
		}
		
		function AnoE()
		{
			if (document.forms[0].txtaae.value.length == 4 && VerifiqueTAB == true)
				document.forms[0].txtddv.focus();
		}
		
		function DiaV()
		{
			if (document.forms[0].txtddv.value.length == 2 && VerifiqueTAB == true)
				document.forms[0].txtmmv.focus();
		}
		
		function MesV()
		{
			if (document.forms[0].txtmmv.value.length == 2 && VerifiqueTAB == true)
				document.forms[0].txtaav.focus();
		}
		
		function AnoV()
		{
			if (document.forms[0].txtaav.value.length == 4 && VerifiqueTAB == true)
				document.forms[0].txtObjetivo.focus();
		}
		</script>
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0" onload="Inicialize()">
		<form method="post" runat="server">


                                    <eo:TabStrip ID="TabStrip1" runat="server" ControlSkinID="None" 
                            MultiPageID="MultiPage1" >
                            <topgroup>
                                <Items>
                                    <eo:TabItem Text-Html="Detalhes">
                                    </eo:TabItem>
                                    <eo:TabItem Text-Html="Certificado">
                                    </eo:TabItem>
                                </Items>
                            </topgroup>
                            <lookitems>
                                <eo:TabItem Height="21" ItemID="_Default" LeftIcon-SelectedUrl="00010605"  
                                    LeftIcon-Url="00010604" 
                                    NormalStyle-CssText="background-image: url(00010602); background-repeat: repeat-x; font-weight: normal; color: black;" 
                                    RightIcon-SelectedUrl="00010607" RightIcon-Url="00010606" 
                                    SelectedStyle-CssText="background-image: url(00010603); background-repeat: repeat-x; font-weight: bold; color: #ff7e00;" 
                                    Text-Padding-Bottom="2" Text-Padding-Top="1">
                                    <subgroup itemspacing="1" 
                                        style-csstext="background-image:url(00010601);background-position-y:bottom;background-repeat:repeat-x;color:black;cursor:hand;font-family:Verdana;font-size:12px;">
                                    </subgroup> 
                                </eo:TabItem>
                            </lookitems>
                        </eo:TabStrip>

            
                               

                        <div style="BORDER-RIGHT:#b0b0b0 1px solid; BORDER-LEFT:#b0b0b0 1px solid; BORDER-BOTTOM:#b0b0b0 1px solid; padding: 5px 5px 5px 5px; width:838px;">
                            <eo:MultiPage ID="MultiPage1" runat="server" Height="400" Width="838">


                                <eo:PageView ID="Pageview1" runat="server">

			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="620" align="center" border="0"><TR><TD class="normalFont" align="center"><BR><asp:label id="lblDetalheCA" runat="server" CssClass="largeboldFont">Detalhes do CA</asp:label></TD></TR><TR><TD class="normalFont" align="center"></TD></TR><TR><TD class="normalFont" align="center"><BR><asp:label id="lblCA" runat="server" CssClass="boldFont">Número do CA:</asp:label>&nbsp;<asp:label id="lblNumeroCAValor" runat="server"></asp:label></TD></TR><TR><TD class="normalFont" align="center"><BR></TD></TR><TR><TD class="normalFont" align="center"><asp:label id="lblFabricante" runat="server" CssClass="BoldFont">Fabricante:</asp:label><BR><asp:textbox id="txtFabricante" runat="server" CssClass="inputBox" Width="600px" ReadOnly="True"></asp:textbox><asp:dropdownlist id="ddlFabricante" runat="server" CssClass="inputBox" Visible="False"></asp:dropdownlist><asp:button id="btnNovoFabricante" runat="server" CssClass="buttonBox" Visible="False" Text="..." onclick="btnNovoFabricante_Click"></asp:button></TD></TR><TR><TD class="normalFont" align="center"><BR></TD></TR><TR><TD class="normalFont" align="center"><asp:label id="lblTipoEquipamento" runat="server" CssClass="boldFont">Tipo do Equipamento:</asp:label><BR><asp:textbox id="txtTipoEquipamento" runat="server" CssClass="inputBox" Width="600px" ReadOnly="True"></asp:textbox><asp:dropdownlist id="ddlEquipamento" runat="server" CssClass="inputBox" Visible="False"></asp:dropdownlist><asp:button id="btnNovoEquipamento" runat="server" CssClass="buttonBox" Visible="False" Text="..." onclick="btnNovoEquipamento_Click"></asp:button></TD></TR><TR><TD class="normalFont" align="center"><BR></TD></TR><TR><TD class="normalFont" align="center"><TABLE id="Table3" cellSpacing="0" cellPadding="0" width="620" border="0"><TR><TD class="normalFont" align="center" width="310"><asp:label id="lblEmissaoCA" runat="server" CssClass="BoldFont">Data de Emissão do CA:</asp:label><asp:textbox id="txtdde" runat="server" CssClass="inputBox" Width="20px" ReadOnly="True" MaxLength="2"></asp:textbox><asp:label id="lblbarra" runat="server" CssClass="BoldFont">/</asp:label><asp:textbox id="txtmme" runat="server" CssClass="inputBox" Width="20px" ReadOnly="True" MaxLength="2"></asp:textbox><asp:label id="Label1" runat="server" CssClass="BoldFont">/</asp:label><asp:textbox id="txtaae" runat="server" CssClass="inputBox" Width="38px" ReadOnly="True" MaxLength="4"></asp:textbox></TD><TD class="normalFont" vAlign="middle" align="center" width="310"><asp:label id="lblValidadeCA" runat="server" CssClass="BoldFont">Validade do CA:</asp:label><asp:textbox id="txtddv" runat="server" CssClass="inputBox" Width="20px" ReadOnly="True" MaxLength="2"></asp:textbox><asp:label id="Label3" runat="server" CssClass="BoldFont">/</asp:label><asp:textbox id="txtmmv" runat="server" CssClass="inputBox" Width="20px" ReadOnly="True" MaxLength="2"></asp:textbox><asp:label id="Label2" runat="server" CssClass="BoldFont">/</asp:label><asp:textbox id="txtaav" runat="server" CssClass="inputBox" Width="38px" ReadOnly="True" MaxLength="4"></asp:textbox></TD></TR></TABLE><BR></TD></TR><TR><TD class="normalFont" align="center"><asp:label id="lblFuncaoEqui" runat="server" CssClass="BoldFont">Objetivo do Equipamento:</asp:label><BR><asp:textbox id="txtObjetivo" runat="server" CssClass="inputBox" Width="600px" ReadOnly="True"
							TextMode="MultiLine"></asp:textbox></TD></TR><TR><TD class="normalFont" align="center"></TD></TR><TR><TD class="normalFont" align="center"><asp:label id="lblDescricao" runat="server" CssClass="BoldFont">Descrição do Equipamento:</asp:label><BR><asp:textbox id="txtDescricao" runat="server" CssClass="inputBox" Width="600px" ReadOnly="True"
							TextMode="MultiLine" Height="76px"></asp:textbox></TD></TR><TR><TD class="normalFont" align="center"><BR><asp:button id="btnEditar" runat="server" CssClass="buttonBox" Text="Editar" onclick="btnEditar_Click"></asp:button>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <asp:button id="lblCancel" runat="server" CssClass="buttonBox" Text="Cancela" onclick="lblCancel_Click"></asp:button>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnGravarDados" runat="server" CssClass="buttonBox" onclick="btnGravarDados_Click" Text="Gravar" />
                    <BR><BR><asp:label id="lblError" runat="server" CssClass="errorFont"></asp:label></TD></TR></TABLE>


                                      </eo:PageView>
                                
                          


                                <eo:PageView ID="Pageview2" runat="server">
                                    <br />
                                    &nbsp;&nbsp;
                                    <asp:Label ID="lblAnotacoes0" runat="server" CssClass="boldFont">Arquivo do Prontuário ASO :</asp:Label>
                                    <br />
                                    &nbsp;&nbsp;
                                    <asp:TextBox ID="txt_Arq" runat="server" CssClass="inputBox" Height="27px" 
                                        ReadOnly="True" Rows="4" TextMode="MultiLine" Width="443px" 
                                        Font-Size="X-Small"></asp:TextBox>
                                    <br />
                                    <br />
                                    &nbsp;&nbsp;
                                    <asp:Label ID="Label6" runat="server" CssClass="boldFont">Inserir / Modificar &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Arquivo de Prontuário ASO : </asp:Label>
                                    &nbsp;&nbsp;&nbsp;<br />
                                    <asp:FileUpload ID="File1" runat="server" ClientIDMode="Static" 
                                        Font-Size="XX-Small" Width="517px" />
                                    <br />
                                    <asp:Button ID="btnGravar" runat="server" CssClass="buttonBox" onclick="btnGravar_Click" Text="Gravar" />
                                    <br />
                                    <asp:Button ID="cmd_PDF" runat="server" BackColor="#FFCC99" Font-Bold="True" Font-Size="X-Small" Font-Strikeout="False" Text="Abrir PDF" Visible="False" OnClick="cmd_PDF_Click" />
                                    <asp:Button ID="cmd_Imagem" runat="server" BackColor="#FFCC99" Font-Bold="True" Font-Size="X-Small" Font-Strikeout="False" OnClick="cmd_Imagem_Click" Text="Visualizar Prontuário" Visible="False" />
                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                    <br />
                                    <asp:Image ID="ImgFunc" runat="server" BorderColor="#660033" BorderStyle="Inset" BorderWidth="2px" Height="545px" Visible="False" Width="428px" />
                                    <br />
                                    &nbsp;&nbsp; &nbsp;&nbsp;&nbsp;<br />
                                    <asp:Label ID="lbl_Path" runat="server" Visible="False"></asp:Label>
                                </eo:PageView>

                                   </eo:MultiPage>
        
         <eo:MsgBox ID="MsgBox1" runat="server" BackColor="#47729F" ControlSkinID="None" 
        HeaderHtml="Dialog Title" Height="100px" Width="168px">
        <HeaderStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #ffbf00 3px solid; padding-left: 4px; font-weight: bold; font-size: 11px; padding-bottom: 2px; color: white; padding-top: 2px; border-bottom: #22456a 1px solid; font-family: verdana" />
        <ContentStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #7d97b6 1px solid; padding-left: 4px; border-left-width: 1px; font-size: 11px; border-left-color: #728eb8; padding-bottom: 4px; color: white; padding-top: 4px; border-bottom: #22456a 1px solid; font-family: verdana" />
        <FooterStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #7d97b6 1px solid; padding-left: 4px; border-left-width: 1px; font-size: 11px; border-left-color: #728eb8; padding-bottom: 4px; color: white; padding-top: 4px; border-bottom: #22456a 1px solid; font-family: verdana" />
        </eo:MsgBox>
		</form>
	</body>
</HTML>
