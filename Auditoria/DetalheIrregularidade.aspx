<%@ Page Language="C#"   AutoEventWireup="True"   CodeBehind="DetalheIrregularidade.aspx.cs"  Inherits="Ilitera.Net.DetalheIrregularidade" Title="Ilitera.Net Plano de Ação da Auditoria de Segurança" %>

<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>


<%--<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
  
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent" >--%>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Ilitera.NET</title>
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
        <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.2.3/dist/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
		<link href="css/forms.css" rel="stylesheet" type="text/css" />
        <link href="css/tabelas.css" rel="stylesheet" type="text/css" />
		<LINK href="scripts/style.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="scripts/validador.js"></script>
      <script language="javascript" src="../scripts/validador.js"></script>
        <script language="javascript">
            function centerWin(url, width, height, name) { // Pop no centro da Tela
                var centerX = (screen.width - width) / 2; //Need to subtract the window width/height so that it appears centered. If you didn't, the top left corner of the window would be centered.
                var centerY = (screen.height - height) / 2;
                janela = window.open(url, name, "toolbar=no,location=no,directories=no,status=no,menubar=no,scrollbars=no,resizable=no,copyhistory=no,width=" + width + ", height=" + height + ",top=" + centerY + ", left=" + centerX);
                //addItemPop(janela);
                //AddItem(janela,'Todos');
                return janela;
            }

        </script>

	    <style type="text/css">
            .style2
            {
                font: xx-small Verdana;
                color: #44926D;
                width: 258px;
            }
            .style3
            {
                font: xx-small Verdana;
                color: #44926D;
                width: 315px;
            }

            @font-face{
                font-family: "Univia Pro";
                src: url("css/UniviaPro-Regular.otf");
            }
            @font-face{
                font-family: "Ubuntu";
                src: url("css/Ubuntu-Regular.ttf");
            }

          [type="button"], [type="reset"], [type="submit"], button {
            min-width: 120px;
            height: 32px;
            font-family: 'Univia Pro' !important;
            font-style: normal;
            font-weight: normal !important;
            font-size: 12px;
            /*text-align: center;*/
            color: #ffffff !important;
            background: linear-gradient(180deg, #48A79E 54.35%, #1C9489 54.36%);
            border-radius: 5px;
            border: none;
            margin-right: 10px;
            margin-bottom: 5px;
            margin-top: 20px;
        }

            [type="button"]:hover, [type="reset"]:hover, [type="submit"]:hover, button:hover {
                color: #ffffff !important;
                background: linear-gradient(180deg, #F2B988 53.35%, #F09E60 53.36%);
                border-radius: 5px;
            }
        </style>

	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0">
		<form method="post" runat="server" id="frmDetalheIrregularidade">
            <div class="container d-flex ms-5 ps-4 justify-content-center">
                     <div class="row gx-3 gy-2 text-center" style="width: 700px">

                         <%-- abas --%>

                         <div class="col-12 subtituloBG mb-1" style="padding-top: 10px; margin-top: 20px;">
                             <asp:label id="lblTitulo" runat="server" CssClass="subtitulo"></asp:label>                         
                         </div>

                         <div class="col-12 text-center">
                             <div class="row">
                                  <eo:TabStrip ID="TabStrip1" runat="server" ControlSkinID="None" 
                                    MultiPageID="MultiPage1" Width="700" height="550">
                                    <topgroup>
                                        <Items>
                                            <eo:TabItem Text-Html="Irregularidade">
                                            </eo:TabItem>
                                            <eo:TabItem Text-Html="Plano de Ação">
                                            </eo:TabItem>                                    
                                            <eo:TabItem Text-Html="Responsabilidades">
                                            </eo:TabItem>
                                        </Items>
                                    </topgroup>
                                    <LookItems>
                                        <eo:TabItem ItemID="_Default"
                                            NormalStyle-CssText="font-family:'Ubuntu'; font-size: 12px; color: #B0ABAB; padding: 10px 10px; background: #F1F1F1; border-radius: 8px; cursor: hand; width: fit-content; margin-right: 1rem;"
                                            SelectedStyle-CssText="font-family:'Ubuntu'; font-size: 12px; color: #1C9489; font-weight: bold; padding: 10px 15px; background: #D9D9D9; border-radius: 8px; cursor: hand; width: fit-content; margin-right: 1rem;">
                                            <SubGroup OverlapDepth="8" ItemSpacing="5"
                                                Style-CssText="font-family:'Ubuntu'; font-size: 12px; color: #B0ABAB; padding: 10px 10px 10px 0px; border-radius: 8px; cursor: hand; width: fit-content;">
                                            </SubGroup>
                                        </eo:TabItem>
                                    </LookItems>
                                </eo:TabStrip>
                             </div>
                         </div>

                         <%-- multipages --%>

                         <div class="col-12">
                             
                             <eo:MultiPage ID="MultiPage1" runat="server" Height="350" Width="700" BorderStyle="None">

                                 <eo:PageView ID="Pageview1" runat="server" Width="700">

                                     <div class="col-12">
                                         <div class="row">

                                             <div class="col-md-4">
                                                 <div class="row">

                                                     <div class="col-12 gy-2 mb-2">
                                                         <asp:Label ID="lblProvaFotografica" runat="server" CssClass="tituloLabel">Prova Fotográfica</asp:Label>
                                                         <asp:ImageButton ID="imbMovePrev" runat="server" ImageUrl="/Images/left.svg" Width="10" CssClass="align-item-middle text-center" ToolTip="Foto Anterior" Visible="False" />
                                                         <asp:ImageButton ID="imbMoveNext" runat="server" ImageUrl="Images/right.svg" Width="10" CssClass="align-item-middle text-center" ToolTip="Próxima Foto" Visible="False" />
                                                     </div>

                                                     <div class="col-12 gy-2">
                                                         <asp:Image ID="Image1" runat="server" ImageUrl="img/5pixel.gif" Visible="false" />
                                                         <asp:Button ID="cmd_Imagem" runat="server" CssClass="btn" OnClick="cmd_Imagem_Click" Text="Visualizar Imagem" />
                                                     </div>

                                                 </div>
                                             </div>

                                             <div class="col-md-8">
                                                 <div class="row">

                                                     <div class="col-12 gy-2">
                                                         <asp:label id="lblNaoConformidade" runat="server" CssClass="tituloLabel">Infração - Não Conformidade Legal</asp:label>
                                                         <asp:TextBox ID="txtNaoConformidadeLegal" runat="server" CssClass="texto form-control form-control-sm" ReadOnly="True" Rows="2" TextMode="MultiLine"></asp:TextBox>
                                                     </div>

                                                     <div class="col-12 gy-2">
                                                         <asp:Label ID="lblEnquadramentoLegal" runat="server" CssClass="tituloLabel">Enquadramento Legal</asp:Label>
                                                         <asp:TextBox ID="txtEnquadramentoLegal" runat="server" CssClass="texto form-control form-contorl-sm" ReadOnly="True" Rows="2" TextMode="MultiLine"></asp:TextBox>
                                                     </div>

                                                     <div class="col-12 gy-2">
                                                         <asp:Label ID="lblLocal" runat="server" CssClass="tituloLabel">Locais/Setores</asp:Label>
                                                         <asp:textbox id="txtLocal" runat="server" CssClass="texto form-control form-control-sm" ReadOnly="True" Rows="2" TextMode="MultiLine"></asp:textbox>
                                                     </div>

                                                 </div>
                                             </div>

                                             <div class="col-12">
                                                 <div class="row">

                                                     <div class="col-6 gy-2">
                                                         <asp:Label ID="lblAcoesExecutar" runat="server" CssClass="tituloLabel">Ações a Executar</asp:Label>
                                                         <asp:TextBox ID="txtDescricao" runat="server" CssClass="texto form-control form-control-sm" ReadOnly="True" Rows="4" TextMode="MultiLine"></asp:TextBox>
                                                     </div>

                                                     <div class="col-6 gy-2">
                                                         <asp:Label ID="lblParecerJuridico" runat="server" CssClass="tituloLabel">Parecer Jurídico</asp:Label>
                                                         <asp:TextBox ID="txtParecerJuridico" runat="server" CssClass="texto form-control form-control-sm" ReadOnly="True" Rows="4" TextMode="MultiLine"></asp:TextBox>
                                                     </div>

                                                 </div>
                                             </div>

                                         </div>
                                     </div>



                                 <%--           </ContentTemplate>                            
                                        </igtab:Tab>
								        <igtab:Tab Text="Responsabilidades">
									        <ContentTemplate>--%>

                                        </eo:PageView>                               


                                               <eo:PageView ID="Pageview3" runat="server" Width="457px">
                                                   
                                                   <div class="col-12 mb-3">
                                                        <div class="row">

                                                    <div class="col-4">
                                                         <div class="row">
                                                     <div class="col-12 gx-2 gy-2">
                                                           <asp:Label ID="lblIniRegut" runat="server" CssClass="tituloLabel form-label">Data de Início </asp:Label>
                                                      </div>
                                                     <div class="col-6 gx-2">
                                                              <asp:TextBox ID="wdtInicioReg" runat="server" CssClass="texto form-control form-control-sm" EditModeFormat="dd/MM/yyyy" 
                                                                 HorizontalAlign="Center" ImageDirectory="" MaxLength="10" UseLastGoodDate="True" Width="100px">
                                                              </asp:TextBox>
                                                     </div>

                                                      </div>
                                                 </div>

                                                  <div class="col-4">
                                                      <div class="row">
                                                  <div class="col-12 gx-2 gy-2">
                                                            <asp:Label ID="lblPrevRegut" runat="server"  CssClass="tituloLabel form-label">Meta para Conclusão</asp:Label>
                                                   </div>
                                                  <div class="col-3 gx-2">
                                                               <asp:TextBox ID="wdtPrevisaoReg" runat="server"  CssClass="texto form-control form-control-sm" EditModeFormat="dd/MM/yyyy" 
                                                                   HorizontalAlign="Center" ImageDirectory="" MaxLength="10" UseLastGoodDate="True" Width="100px">
                                                               </asp:TextBox>
                                                   </div>
                                                       </div>
                                                     </div>

                                                     <div class="col-4">
                                                         <div class="row">
                                                     <div class="col-12 gx-2 gy-2">
                                                           <asp:Label ID="lblDataRegut" runat="server" CssClass="tituloLabel form-label">Data da Conclusão</asp:Label>
                                                     </div>
                                                     <div class="col-3 gx-2">
                                                              <asp:TextBox ID="wdtRegularizacao" runat="server" CssClass="texto form-control form-control-sm" EditModeFormat="dd/MM/yyyy" 
                                                                   HorizontalAlign="Center" ImageDirectory="" MaxLength="10" UseLastGoodDate="True" Width="100px">
                                                              </asp:TextBox>
                                                       </div>
                                                     </div>

                                                     </div>
                                                      </div>
                                                       </div>
                       


                                                  <div class="col-12 gx-5">
                                                    <div class="row">

                                                 <div class="col-6 gx-2 gy-2">
                                                   <fieldset>
                                                     <asp:label id="lblTipot" runat="server" class="tituloLabel form-label col-form-label col-form-label-sm">Tipo da Irregularidade</asp:label>
                                                     <asp:dropdownlist id="ddlTipot" runat="server" CssClass="texto form-select form-select-sm" Width="230px" AutoPostBack="True"></asp:dropdownlist>
                                                   </fieldset>
                                                </div>

                                               <div class="col-3 gy-2">
                                                  <fieldset>
                                                    <asp:label id="lblOrcamentot" runat="server" class="tituloLabel form-label col-form-label col-form-label-sm">Orçamento</asp:label>
                                                    <asp:textbox id="txtOrcamentot" runat="server"  CssClass="texto form-control form-control-sm" Width="110px" JavaScriptFileNameCommon="ig_shared.js"
                                                       JavaScriptFileName="ig_edit.js" ImageDirectory=" " DataMode="Float" HorizontalAlign="Center" MinValue="0" Nullable="False">
                                                    </asp:textbox>
                                                </fieldset>
                                               </div>

                                             <div class="col-3 gx-4 gy-2">
                                                 <fieldset>
                                                   <asp:label id="lblCustoFinalt" runat="server" class="tituloLabel form-label col-form-label col-form-label-sm">Custo Final</asp:label>
                                                   <asp:textbox id="txtCustoFinalt" runat="server" CssClass="texto form-control form-control-sm" Width="110px" JavaScriptFileNameCommon="ig_shared.js"
                                                      JavaScriptFileName="ig_edit.js" ImageDirectory=" " DataMode="Float" HorizontalAlign="Center" MinValue="0" Nullable="False">
                                                   </asp:textbox>
                                                 </fieldset>
                                             </div>

                                                  </div>
                                                   </div>


                                          <div class="col-12 mb-3">
                                            <div class="row">

                                            <div class="col-6 gx-2 gy-2">
                                                 <fieldset>
                                                   <asp:label id="lblRespAdmt" runat="server" class="tituloLabel form-label col-form-label col-form-label-sm">Responsável Administrativo</asp:label>
                                                     <asp:dropdownlist id="ddlRespAdmt" runat="server" CssClass="texto form-select form-select-sm" AutoPostBack="True"></asp:dropdownlist>
                                                 </fieldset>
                                            </div>

                                           <div class="col-6 gx-2 gy-2">
                                                 <fieldset>
                                                   <asp:label id="lblRespOpet" runat="server" class="tituloLabel form-label col-form-label col-form-label-sm">Responsável Operacional</asp:label>
                                                      <asp:dropdownlist id="ddlRespOpert" runat="server" CssClass="texto form-select form-select-sm" AutoPostBack="True"></asp:dropdownlist>
                                                 </fieldset>
                                           </div>

                                            </div>
                                           </div>

                                         <div class="col-12 mb-3">
                                            <div class="row">

                                         <div class="col-6 gx-2 gy-2">
                                                   <asp:label id="lblObjetivot" runat="server" class="tituloLabel form-label col-form-label col-form-label-sm">Justificativa para a regularização</asp:label>
                                                   <asp:textbox id="txtObjetivot" runat="server" CssClass="texto form-control form-control-sm" Width="227px" TextMode="MultiLine" Rows="4"></asp:textbox>
                                         </div>
           
                                        <div class="col-6 gx-2 gy-2">
                                            <asp:label id="lblObservacaot" runat="server" class="tituloLabel form-label col-form-label col-form-label-sm">Outras observações</asp:label>
                                             <asp:textbox id="txtObservacaot" runat="server" CssClass="texto form-control form-control-sm" Width="227px" TextMode="MultiLine" Rows="4"></asp:textbox>
                                        </div>

                                         </div>
                                          </div>

                                                         <div class="text-center mt-4 mb-3">
                                                             <asp:button id="btnSalvart" runat="server" CssClass="btn" Text="Gravar Item do Plano do Ação"></asp:button>
                                                         </div>
        
        


                                        </eo:PageView>


                                        <eo:PageView ID="Pageview2" runat="server" Width="457px">



										        <TABLE class="normalFont" id="Table6" cellSpacing="0" cellPadding="0" width="480" align="center"
											        border="0">
                                                    <TR>
                                                        <TD align="center">
                                                            <br />
                                                            <eo:TabStrip ID="TabStrip2" runat="server" ControlSkinID="None" 
                                                                MultiPageID="MultiPage1" Width="600">
                                                                <topgroup>
                                                                    <Items>
                                                                        <eo:TabItem Text-Html="Civil">

                                                                        </eo:TabItem>
                                                                        <eo:TabItem Text-Html="Penal">

                                                                        </eo:TabItem>
                                                                        <eo:TabItem Text-Html="Trabalhista (Multas)">

                                                                        </eo:TabItem>
                                                                        <eo:TabItem Text-Html="Previdenciaria">

                                                                        </eo:TabItem>
                                                                        <eo:TabItem Text-Html="Ambiental">

                                                                        </eo:TabItem>
                                                                        <eo:TabItem Text-Html="Normativa">

                                                                        </eo:TabItem>

                                                                    </Items>

                                                                </topgroup>
                                                                <lookitems>
                                                                    <eo:TabItem Height="21" ItemID="_Default" 
                                                                        NormalStyle-CssText="font-family:'Ubuntu'; font-size: 12px; color: #B0ABAB; padding: 10px 10px; background: #F1F1F1; border-radius: 8px; cursor: hand; width: fit-content; margin-right: 1rem;"
                                                                        SelectedStyle-CssText="font-family:'Ubuntu'; font-size: 12px; color: #1C9489; font-weight: bold; padding: 10px 15px; background: #D9D9D9; border-radius: 8px; cursor: hand; width: fit-content; margin-right: 1rem;">
                                                                        <subgroup itemspacing="1" 
                                                                            style-csstext="font-family:'Ubuntu'; font-size: 12px; color: #B0ABAB; padding: 10px 10px; border-radius: 8px; cursor: hand; width: fit-content;">
                                                                        </subgroup> 
                                                                    </eo:TabItem>
                                                                </lookitems>

                                                            </eo:TabStrip>
                                                            <eo:MultiPage ID="MultiPage2" runat="server" Height="400" Width="450">
                                                                <eo:PageView ID="Pageview4" runat="server">
                                                                    <TABLE id="Table10" cellSpacing="0" cellPadding="0" width="470" align="center" border="0">
                                                                        <TR>
                                                                            <TD class="normalFont" vAlign="top" align="center" width="470">
                                                                                <br />
                                                                                <asp:textbox id="txtCivilt" runat="server" CssClass="texto form-control form-control-sm" Width="450px" TextMode="MultiLine"
																					        Rows="15" ReadOnly="True"></asp:textbox>
                                                                                <br>
                                                                                <br>

                                                                            </TD>

                                                                        </TR>

                                                                    </TABLE>

                                                                </eo:PageView>
                                                                <eo:PageView ID="Pageview5" runat="server" Width="457px">
                                                                    <TABLE id="Table11" cellSpacing="0" cellPadding="0" width="470" align="center" border="0">
                                                                        <TR>
                                                                            <TD class="normalFont" vAlign="top" align="center" width="470">
                                                                                <br />
                                                                                <asp:textbox id="txtPenalt" runat="server" Width="450px" CssClass="texto form-control form-control-sm" ReadOnly="True"
																					        Rows="15" TextMode="MultiLine"></asp:textbox>
                                                                                <BR>
                                                                                <BR>

                                                                            </TD>

                                                                        </TR>

                                                                    </TABLE>

                                                                </eo:PageView>
                                                                <eo:PageView ID="Pageview6" runat="server" Width="457px">
                                                                    <TABLE id="Table12" cellSpacing="0" cellPadding="0" width="470" align="center" border="0">
                                                                        <TR>
                                                                            <TD class="normalFont" vAlign="top" align="center" width="470">
                                                                                <asp:textbox id="txtTrabalhistat" runat="server" Width="450px" CssClass="texto form-control form-control-sm" ReadOnly="True"
																					        Rows="8" TextMode="MultiLine"></asp:textbox>
                                                                                <BR>
                                                                                <BR>
                                                                                <BR>
                                                                                <TABLE class="normalFont" id="Table20" cellSpacing="0" cellPadding="0" width="460" align="center"
																					        border="0">
                                                                                    <TR>
                                                                                        <TD align="center" width="230">
                                                                                            <asp:label id="Label5" runat="server" CssClass="boldFont">Valor da Multa em UFIR</asp:label>
                                                                                            <BR>
                                                                                            <BR>
                                                                                            <TABLE class="normalFont" id="Table9" cellSpacing="0" cellPadding="0" width="220" align="center"
																								        border="0">
                                                                                                <TR>
                                                                                                    <TD align="center" width="108" bgColor="lemonchiffon">
                                                                                                        <asp:label id="Label2" runat="server" CssClass="boldFont">Mínimo</asp:label>
                                                                                                    </TD>
                                                                                                    <TD align="center" width="4">

                                                                                                    </TD>
                                                                                                    <TD align="center" width="108" bgColor="#fffacd">
                                                                                                        <asp:label id="Label3" runat="server" CssClass="boldFont">Máximo</asp:label>

                                                                                                    </TD>

                                                                                                </TR>
                                                                                                <TR>
                                                                                                    <TD align="center" width="108">
                                                                                                        <asp:label id="tablblUFIRMinimo" runat="server" CssClass="normalFont"></asp:label>

                                                                                                    </TD>
                                                                                                    <TD align="center" width="4">

                                                                                                    </TD>
                                                                                                    <TD align="center" width="108">
                                                                                                        <asp:label id="tablblUFIRMaximo" runat="server" CssClass="normalFont"></asp:label>

                                                                                                    </TD>

                                                                                                </TR>

                                                                                            </TABLE>

                                                                                        </TD>
                                                                                        <TD align="center" width="230">
                                                                                            <asp:label id="Label6" runat="server" CssClass="boldFont">Valor da Multa em Reais</asp:label>
                                                                                            <BR>
                                                                                            <BR>
                                                                                            <TABLE class="normalFont" id="Table8" cellSpacing="0" cellPadding="0" width="220" align="center"
																								        border="0">
                                                                                                <TR>
                                                                                                    <TD align="center" width="108" bgColor="#fffacd">
                                                                                                        <asp:label id="Label8" runat="server" CssClass="boldFont">Mínimo</asp:label>

                                                                                                    </TD>
                                                                                                    <TD align="center" width="4">

                                                                                                    </TD>
                                                                                                    <TD align="center" width="108" bgColor="#fffacd">
                                                                                                        <asp:label id="Label7" runat="server" CssClass="boldFont">Máximo</asp:label>

                                                                                                    </TD>

                                                                                                </TR>
                                                                                                <TR>
                                                                                                    <TD align="center" width="108">
                                                                                                        <asp:label id="tablblReaisMinimo" runat="server" CssClass="normalFont"></asp:label>

                                                                                                    </TD>
                                                                                                    <TD align="center" width="4">

                                                                                                    </TD>
                                                                                                    <TD align="center" width="108">
                                                                                                        <asp:label id="tablblReaisMaximo" runat="server" CssClass="normalFont"></asp:label>

                                                                                                    </TD>

                                                                                                </TR>

                                                                                            </TABLE>

                                                                                        </TD>

                                                                                    </TR>

                                                                                </TABLE>

                                                                            </TD>

                                                                        </TR>

                                                                    </TABLE>

                                                                </eo:PageView>
                                                                <eo:PageView ID="Pageview7" runat="server" Width="457px">
                                                                    <TABLE id="Table13" cellSpacing="0" cellPadding="0" width="470" align="center" border="0">
                                                                        <TR>
                                                                            <TD class="normalFont" vAlign="top" align="center" width="470">
                                                                                <br />
                                                                                <asp:textbox id="txtPrevidt" runat="server" CssClass="inputBox" Width="450px" TextMode="MultiLine"
																					        Rows="15" ReadOnly="True"></asp:textbox>
                                                                                <br>
                                                                                <br>

                                                                            </TD>

                                                                        </TR>

                                                                    </TABLE>

                                                                </eo:PageView>
                                                                <eo:PageView ID="Pageview8" runat="server" Width="457px">
                                                                    <TABLE id="Table14" cellSpacing="0" cellPadding="0" width="470" align="center" border="0">
                                                                        <TR>
                                                                            <TD class="normalFont" vAlign="top" align="center" width="470">
                                                                                <br />
                                                                                <asp:textbox id="txtAmbientalt" runat="server" CssClass="inputBox" Width="450px" TextMode="MultiLine"
																					        Rows="15" ReadOnly="True"></asp:textbox>
                                                                                <br>
                                                                                <br>

                                                                            </TD>

                                                                        </TR>

                                                                    </TABLE>
                                                                    </ContentTemplate>
                                                                    </igtab:Tab>
                                                                    <igtab:Tab Text="Normativa">
																        <ContentTemplate>


																	        <TABLE id="Table15" cellSpacing="0" cellPadding="0" width="470" align="center" border="0">
																		        <TR>
																			        <TD class="normalFont" vAlign="top" align="center" width="470">
																				        <br />
																				        <asp:textbox id="txtNormativat" runat="server" CssClass="inputBox" Width="450px" TextMode="MultiLine"
																					        Rows="15" ReadOnly="True"></asp:textbox>
                                                                                        <br>
																				        <br>
																			        </br></br></TD>
																		        </TR>
																	        </TABLE>

        <%--
																        </ContentTemplate>
															        </igtab:Tab>
														        </Tabs>
													        </igtab:UltraWebTab></TD>--%>


                                                           </eo:PageView></eo:MultiPage>

											        </TR></TABLE>



	        <%--								</ContentTemplate>
								        </igtab:Tab>
								        <igtab:Tab Text="Opera&#231;&#227;o">
									        <ContentTemplate>--%>


                                        </eo:PageView>



                                        </eo:MultiPage>

                         </div>


<%--
						<igtab:ultrawebtab id="uwtAuditoria" runat="server" Width="480px" ImageDirectory="../InfragisticsImg/" ThreeDEffect="False" BorderColor="#949878"
							BorderWidth="1px" BorderStyle="Solid" Height="267px">
							<DefaultTabStyle Height="22px" Font-Size="8pt" Font-Names="Microsoft Sans Serif" ForeColor="Black"
								BackColor="#FEFCFD">
								<Padding Top="2px"></Padding>
							</DefaultTabStyle>
							<RoundedImage LeftSideWidth="7" RightSideWidth="6" ShiftOfImages="2" SelectedImage="ig_tab_winXP1.gif"
								NormalImage="ig_tab_winXP3.gif" HoverImage="ig_tab_winXP2.gif" FillStyle="LeftMergedWithCenter"></RoundedImage>
							<SelectedTabStyle>
								<Padding Bottom="2px"></Padding>
							</SelectedTabStyle>
							<Tabs>
                                <igtab:Tab Text="Infra&#231;&#227;o" Tooltip="Infra&#231;&#227;o - N&#227;o Conformidade Legal">
                                    <ContentTemplate>
--%>

                           



                        




<%--
									</ContentTemplate>
								</igtab:Tab>
							</Tabs>
						</igtab:ultrawebtab>--%>




                        <br />

                         <div class="col-12 text-center">
                             <asp:button id="btnNR" runat="server" CssClass="btn" OnClick="btnNR_Click" Text="Visualizar NR"></asp:button>
                        <asp:Button ID="btnIrregularidade" runat="server" CssClass="btn" OnClick="btnIrregularidade_Click" Text="Imprimir Irregularidade" />
                         </div>

						</td>
				</TR>
			</TABLE>
			<INPUT id="txtAuxiliar" type="hidden" name="txtAuxiliar" runat="server">

 <eo:MsgBox ID="MsgBox1" runat="server" BackColor="#ffffff" ControlSkinID="None" HeaderHtml="Dialog Title" Height="192px" Width="345px" CssClass="card border border-1 p-2 text-center" IconUrl="Images/alerta_icon.svg">
            <HeaderStyleActive CssText="font-family: Ubuntu; font-size: 14px; font-weight: bold; color: #1C9489; text-align: center; padding: 5px;" />
            <ContentStyleActive CssText="font-family: Ubuntu; font-size: 14px; font-weight: bold; color: #7D7B7B; text-align: center; padding: 5px; width: 345px; height: 60px" />
            <FooterStyleActive CssText="width: 345px;" />
        </eo:MsgBox>

                </div>
            </div>
		</form>
	</body>


</HTML>


<%--</asp:Content>--%>