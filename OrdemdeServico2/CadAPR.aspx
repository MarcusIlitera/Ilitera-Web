<%@ Page Language="C#"   AutoEventWireup="True"  MasterPageFile="~/Site.Master" CodeBehind="CadAPR.aspx.cs"  Inherits="Ilitera.Net.OrdemDeServico.CadAPR" Title="Ilitera.Net" %>


<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>


<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <style type="text/css">
    .defaultFont
    {
        width: 627px;
    }
        .table
        {}
    </style>
</asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent" >



    <script language="javascript" src="scripts/validador.js"></script>
	<script language="javascript" type="text/javascript">

    function AbreCadDano(IdCliente, IdUsuario)
	{
//	    for (var i = 0; i < document.getElementById("lsbRisco").options.length; i++)
//	        if (document.getElementById("lsbRisco").options[i].selected)
//	        {
	            //addItem(centerWin("CadDanos.aspx?IdEmpresa=" + IdCliente + "&IdUsuario=" + IdUsuario, 350, 225, "CadDano"), "Empresa");
	            centerWin("CadDanos.aspx?IdEmpresa=" + IdCliente + "&IdUsuario=" + IdUsuario, 350, 225, "CadDano");
	         //   break
//	        }	           
	}
    		</script>
	

       

<%--</head>
<body bottommargin="0" leftmargin="0" rightmargin="0" topmargin="0">
    <form id="form1" runat="server">
--%>        <table align="center" border="0" cellpadding="0" cellspacing="0" width="620" class="defaultFont">
            <tr>
                <td align="center" style="width: 620px">
                    <asp:Label ID="lblTitle" runat="server" SkinID="TitleFont"></asp:Label><br />
                    <asp:Label ID="lblSubTitle" runat="server" Text="Selecione a Operacao, o Perigo e o Risco ou o Aspecto e o Impacto para iniciar"></asp:Label><br />
                    <br />
                    <table align="center" border="0" cellpadding="0" cellspacing="0" width="600" class="defaultFont">
                        <tr>
                            <td width="190" align="center" valign="top">
                                <asp:Label ID="Label1" runat="server" SkinID="BoldFont" Text="Operações"></asp:Label><br />
                                <asp:ListBox ID="lsbOperacao" runat="server" Rows="8" Width="190px" AutoPostBack="True" OnSelectedIndexChanged="lsbOperacao_SelectedIndexChanged"></asp:ListBox></td>
                            <td width="220" align="center">
                                <asp:Label ID="Label2" runat="server" SkinID="BoldFont" Text="Perigos"></asp:Label><br />
                                <asp:ListBox ID="lsbPerigo" runat="server" Rows="3" Width="190px" AutoPostBack="True" OnSelectedIndexChanged="lsbPerigo_SelectedIndexChanged"></asp:ListBox><br />
                                <br />
                                <asp:Label ID="Label4" runat="server" SkinID="BoldFont" Text="Aspectos"></asp:Label><br />
                                <asp:ListBox ID="lsbAspecto" runat="server" Rows="3" Width="190px" AutoPostBack="True" OnSelectedIndexChanged="lsbAspecto_SelectedIndexChanged"></asp:ListBox></td>
                            <td align="center" width="190">
                                <asp:Label ID="Label3" runat="server" SkinID="BoldFont" Text="Riscos"></asp:Label><br />
                                <asp:ListBox ID="lsbRisco" runat="server" Rows="3" Width="190px" SkinID="YellowList" AutoPostBack="True" OnSelectedIndexChanged="lsbRisco_SelectedIndexChanged"></asp:ListBox><br />
                                <br />
                                <asp:Label ID="Label5" runat="server" SkinID="BoldFont" Text="Impactos"></asp:Label><br />
                                <asp:ListBox ID="lsbImpacto" runat="server" Rows="3" Width="190px" SkinID="YellowList" AutoPostBack="True" OnSelectedIndexChanged="lsbImpacto_SelectedIndexChanged"></asp:ListBox></td>
                        </tr>
                    </table>
                    <br />



  <eo:TabStrip ID="TabStrip1" runat="server" ControlSkinID="None" 
                            MultiPageID="MultiPage1">
                            <topgroup>
                                <Items>
                                    <eo:TabItem Text-Html="Danos e Perdas">
                                    </eo:TabItem>
                                    <eo:TabItem Text-Html="Controle do Risco">
                                    </eo:TabItem>
                                    <eo:TabItem Text-Html="Controle do Impacto">
                                    </eo:TabItem>
                                    <eo:TabItem Text-Html="Responsáveis APP">
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
                                
                                        <table align="center" border="0" cellpadding="0" cellspacing="0" class="defaultFont"     width="590">
            <tr>
                <td align="center" width="590">
                    <table align="center" border="0" cellpadding="0" cellspacing="0" class="defaultFont"
                        width="590">
                        <tr>
                            <td align="center" width="260" valign="top">
                                <asp:Label ID="lblDanos" runat="server" SkinID="BoldFont" Text="Listagem dos Danos"></asp:Label><br />
                                <asp:ListBox ID="lsbDanos" runat="server" Width="240px" SelectionMode="Multiple"></asp:ListBox><br />
                                <br />
                                <INPUT class=button id=btnCadDano style="WIDTH: 150px" onclick="javascript:AbreCadDano(<%=cliente.Id%>, <%=usuario.Id%>);" tabIndex=0 type=button value="Adicionar / Editar Danos">
                                </td>
                            <td align="center" width="70" valign="top">
                                <br />
                                <asp:ImageButton ID="imbAddDano" runat="server" SkinID="ImgNext"
                                    ToolTip="Adicionar Danos ao Risco selecionado" OnClick="imbAddDano_Click" ImageUrl="InfragisticsImg/XpOlive/next_down.gif" /><br />
                                <br />
                                <asp:ImageButton ID="imbRemoveDano" runat="server" SkinID="ImgPrev"
                                    ToolTip="Remover Danos do Risco selecionado" OnClick="imbRemoveDano_Click" ImageUrl="InfragisticsImg/XpOlive/prev_down.gif"/></td>
                            <td align="center" width="260" valign="top">
                                <asp:Label ID="lblDanosSelected" runat="server" SkinID="BoldFont" Text="Danos Selecionados"></asp:Label><br />
                                <asp:ListBox ID="lsbDanosSelected" runat="server"
                                    Width="240px" SelectionMode="Multiple" SkinID="YellowList"></asp:ListBox></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>

  </eo:PageView> 

 <eo:PageView ID="Pageview2" runat="server">

        <table align="center" border="0" cellpadding="0" cellspacing="0" class="defaultFont"
            width="590">
            <tr>
                <td align="center" width="295" valign="top">
                    <asp:Label ID="Label6" runat="server" SkinID="BoldFont" Text="EPI"></asp:Label><br />
                    <asp:ListBox ID="lsbEPI" runat="server" Rows="2" Width="275px" SelectionMode="Multiple"></asp:ListBox><br />
                    <br />
                    &nbsp; &nbsp; &nbsp;&nbsp;
                    <br />
                    <br />
                    <asp:ListBox ID="lsbEPISelected" runat="server" Rows="2" SkinID="YellowList" Width="275px" SelectionMode="Multiple">
                    </asp:ListBox></td>
                <td width="295" align="center" valign="top">
                    <asp:Label ID="Label7" runat="server" SkinID="BoldFont" Text="EPC"></asp:Label><br />
                    <asp:TextBox ID="txtEPC" runat="server" Width="275px"></asp:TextBox><br />
                    <br />
                    <asp:Label ID="Label8" runat="server" SkinID="BoldFont" Text="Medidas Administrativas"></asp:Label><br />
                    <asp:TextBox ID="txtMedAdm" runat="server" Width="275px"></asp:TextBox><br />
                    <br />
                    <asp:Label ID="Label9" runat="server" SkinID="BoldFont" Text="Medidas Educacionais"></asp:Label><br />
                    <asp:TextBox ID="txtMedEdu" runat="server" Width="275px"></asp:TextBox></td>
            </tr>
        </table>

   </eo:PageView> 
   <eo:PageView ID="Pageview3" runat="server">

        <table align="center" border="0" cellpadding="0" cellspacing="0" class="defaultFont"
            width="590">
            <tr>
                <td align="center" width="295">
                    <asp:Label ID="Label10" runat="server" SkinID="BoldFont" Text="Medidas Operacionais"></asp:Label><br />
                    <asp:TextBox ID="txtMedOpeAmb" runat="server" Rows="7" TextMode="MultiLine" Width="275px"></asp:TextBox></td>
                <td align="center" width="295">
                    <asp:Label ID="Label11" runat="server" SkinID="BoldFont" Text="Medidas Educacionais"></asp:Label><br />
                    <asp:TextBox ID="txtMedEduAmb" runat="server" Rows="7" TextMode="MultiLine" Width="275px"></asp:TextBox></td>
            </tr>
        </table>

        </eo:PageView>
        <eo:PageView ID="Pageview4" runat="server">

            <div style="text-align: center">
                <table align="center" border="0" cellpadding="0" cellspacing="0" class="defaultFont"
                    width="590">
                    <tr>
                        <td align="center" width="295">
                            <asp:Label ID="Label12" runat="server" SkinID="BoldFont" Text="Responsável pela Elaboração"></asp:Label><br />
                            <asp:DropDownList ID="ddlRespElaboracao" runat="server" Width="250px"></asp:DropDownList>&nbsp;<INPUT class=button id=btnRespElaboracao style="WIDTH: 20px" onclick="centerWin('../CommonPages/CadResponsavel.aspx?IdEmpresa=<%=cliente.Id%>&IdUsuario=<%=usuario.Id%>', 350, 300, 'CadastroElaborador');" type=button value=...><br />
                            <br />
                            <asp:Label ID="Label14" runat="server" SkinID="BoldFont" Text="Data da Elaboração"></asp:Label><br />
                            <asp:textbox id="wdtDataEleboracao" runat="server" Width="140px" ImageDirectory=""></asp:textbox>
                        </td>
                        <td align="center" width="295">
                            <asp:Label ID="Label13" runat="server" SkinID="BoldFont" Text="Responsável pela Revisão"></asp:Label><br />
                            <asp:DropDownList ID="ddlRespRevisao" runat="server" Width="250px"></asp:DropDownList>&nbsp;<INPUT class=button id=btnRespRevisao style="WIDTH: 20px" onclick="centerWin('../CommonPages/CadResponsavel.aspx?IdEmpresa=<%=cliente.Id%>&IdUsuario=<%=usuario.Id%>', 350, 300, 'CadastroRevisor');" type=button value=...><br />
                            <br />
                            <asp:Label ID="Label15" runat="server" SkinID="BoldFont" Text="Data da Revisão"></asp:Label><br />
                            <asp:textbox ID="wdtDataRevisao" runat="server" ImageDirectory="" Width="140px">
                            </asp:textbox>
                        </td>
                    </tr>
                </table>
            </div>
   </eo:PageView>
</eo:MultiPage> 


                    <br />
                    &nbsp;<asp:Button ID="btnGravar" runat="server" Text="Gravar Dados" Width="110px" OnClick="btnGravar_Click" />
                    &nbsp; &nbsp;
                    <asp:Button ID="btnProcedimento" runat="server" Text="Procedimento" Width="110px" OnClick="btnProcedimento_Click" />
                    &nbsp;&nbsp; &nbsp;<asp:Button ID="btnCancelar" runat="server" Text="Cancelar" Width="110px" OnClick="btnCancelar_Click" />
                    &nbsp; &nbsp;
                    <asp:LinkButton ID="lkbAPR" runat="server" OnClick="lkbAPR_Click" SkinID="BoldLinkButton"
                        ToolTip="Análise Preliminar de Riscos"><img src="img/print.gif" border="0"></img> APR</asp:LinkButton>
                        
                            <asp:Label ID="lbl_Id_Empresa" runat="server" Text="IdEmpresa" Visible="False"></asp:Label>
                            <asp:Label ID="lbl_Id_Usuario" runat="server" Text="IdUsuario" Visible="False"></asp:Label>                        
                        </td>

            </tr>
        </table>
        <input id="txtAuxiliar" runat="server" name="txtAuxiliar" type="hidden" />


         <eo:MsgBox ID="MsgBox1" runat="server" BackColor="#47729F" ControlSkinID="None" 
        HeaderHtml="Dialog Title" Height="100px" Width="168px">
        <HeaderStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #ffbf00 3px solid; padding-left: 4px; font-weight: bold; font-size: 11px; padding-bottom: 2px; color: white; padding-top: 2px; border-bottom: #22456a 1px solid; font-family: verdana" />
        <ContentStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #7d97b6 1px solid; padding-left: 4px; border-left-width: 1px; font-size: 11px; border-left-color: #728eb8; padding-bottom: 4px; color: white; padding-top: 4px; border-bottom: #22456a 1px solid; font-family: verdana" />
        <FooterStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #7d97b6 1px solid; padding-left: 4px; border-left-width: 1px; font-size: 11px; border-left-color: #728eb8; padding-bottom: 4px; color: white; padding-top: 4px; border-bottom: #22456a 1px solid; font-family: verdana" />
    </eo:MsgBox>




</asp:Content>