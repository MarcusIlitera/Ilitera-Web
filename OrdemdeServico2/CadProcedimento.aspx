<%@ Page Language="C#"   AutoEventWireup="True"  MasterPageFile="~/Site.Master" CodeBehind="CadProcedimento.aspx.cs"  Inherits="Ilitera.Net.OrdemDeServico.CadProcedimento" Title="Ilitera.Net" %>

<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <link href="css/forms.css" rel="stylesheet" type="text/css" />
    <link href="css/tabelas.css" rel="stylesheet" type="text/css" />
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
    <div class="container-fluid d-flex ms-5 ps-4">
        <div class="row gx-3 gy-2">
    <script language="javascript">
        function callme(oFile) {
            document.getElementById("txt_Arq").value = oFile.value;
        }
    </script>


    <style type="text/css">
        .btnMenor {
            min-width: 25px;
            height: 29px;
            background: #D9D9D9;
            border-radius: 5px;
            border: none;
            margin-right: 10px;
        }

        .btnMenor:hover {
            color: #ffffff !important;
            background: #B0ABAB;
            border-radius: 5px;
        }

        .inputBox
        {}
        .normalFont
        {
            margin-left: 0px;
            }
        .style1
        {
            width: 45px;
        }
        .style2
        {
            height: 16px;
            width: 137px;
        }
        .style3
        {
            width: 574px;
        }
        .style4
        {
            width: 318px;
        }
        .style5
        {
            width: 303px;
        }
        .radioMargin {
            margin-left: 10px;
        }
        .espaco {
            margin-right: 10px;
        }
        #ctl00_MainContent_txtNome {
            margin-right: 15px;
        }
        #ctl00_MainContent_rblTipoProcedimento {
            margin-left: 25px;
        }
        #ctl00_MainContent_lblTipoProcedimento {
            margin-bottom: 10px;
        }
        #ctl00_MainContent_rblIncidencia {
            margin-left: 20px;
        }
        #ctl00_MainContent_rblSituacao {
            margin-left: 20px;
        }

        #ctl00_MainContent_rblSeveridadeImpacto_1 {
            margin-left: 5px;
            margin-right: 5px;
        }
        #ctl00_MainContent_rblSeveridadeImpacto_2 {
            margin-left: 1px;
            margin-right: 5px;
        }
        </style>


        <script language="javascript" src="scripts/validador.js"></script>
		<script language="javascript" type="text/javascript">
            function showing(form) {
                if (form.UltraWebTabProcedimento__ctl0_searchFoto.value != "") {
                    form.UltraWebTabProcedimento__ctl0_txtSelectedFile.value = form.UltraWebTabProcedimento__ctl0_searchFoto.value;
                    adres = form.UltraWebTabProcedimento__ctl0_searchFoto.value;
                    adres = adres.toLowerCase();
                    index = adres.indexOf(".jpg");
                    index = index + adres.indexOf(".gif");
                    if (index < 0)
                        window.alert("O arquivo de imagem selecionado não é de formato válido!");
                    else {
                        var img_obj = new Image();
                        img_obj = form.UltraWebTabProcedimento__ctl0_searchFoto.value;
                        form.UltraWebTabProcedimento__ctl0_tabimgFoto.src = img_obj;
                        Set(form.UltraWebTabProcedimento__ctl0_tabimgFoto);
                    }
                }
            }
            function Set(image) {
                image.width = 175;
                image.height = 117;
            }
            function AbreCadPerigo(IdCliente, IdUsuario) {
                //if (document.forms[0].UltraWebTabProcedimento__ctl0_tabrblTipoProcedimento_0.checked)
                addItem(centerWin("CadPerigo.aspx?IdEmpresa=" + IdCliente + "&IdUsuario=" + IdUsuario, 700, 560, "CadPerigo"), "Empresa");
            }
            function AbreCadCelula(IdCliente, IdUsuario) {
                //if (document.forms[0].UltraWebTabProcedimento__ctl0_tabrblTipoProcedimento_0.checked)
                addItem(centerWin("CadCelula.aspx?IdEmpresa=" + IdCliente + "&IdUsuario=" + IdUsuario, 450, 455, "CadCelula"), "Empresa");
            }
            function AbreCadAspecto(IdCliente, IdUsuario) {
                if (document.forms[0].UltraWebTabProcedimento__ctl0_tabrblTipoProcedimento_0.checked)
                    addItem(centerWin("CadAspecto.aspx?IdEmpresa=" + IdCliente + "&IdUsuario=" + IdUsuario, 450, 455, "CadAspecto"), "Empresa");
            }
        </script>
	
		<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="1000" border="0">

                <br />
                <br />

			<TR>
					<TD >

                        <%-- BOTÕES --%>

                        <eo:TabStrip ID="TabStrip1" runat="server" ControlSkinID="None" 
                            MultiPageID="MultiPage1">
                            <topgroup>
                                <Items>
                                    <eo:TabItem Text-Html="Dados Básicos">
                                    </eo:TabItem>
                                    <eo:TabItem Text-Html="Dados Complementares">
                                    </eo:TabItem>
                                    <eo:TabItem Text-Html="Operações">
                                    </eo:TabItem>
                                    <eo:TabItem Text-Html="Medidas de Controle">
                                    </eo:TabItem>
                                    <eo:TabItem Text-Html="Atualizações">
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
                        <div>

                            <eo:MultiPage ID="MultiPage1" runat="server" Width="1000">

                                                               <%-- DADOS BÁSICOS--%>

                                <eo:PageView ID="Pageview1" runat="server">
										<TABLE  id="Table2" cellSpacing="0" cellPadding="0" width="1000" border="0"> <%-- container inteiro de 'dados básicos' --%>
											<TR>
												<TD style="height: 198px">
													<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="800" border="0"> <%-- imagem + número e tipo de procedimento --%>
														<TR>
															<TD vAlign="top" width="1000">
																<TABLE  id="Table6" cellSpacing="0" cellPadding="0" width="800" border="0"> <%-- número e tipo de procedimento --%>
																	<TR>
                                                                        <div class="col-12">
                                                                            <div class="row gx-3 gy-2">
                                                                                <div class="col-4">
                                                                                    <asp:Label id="Label12" runat="server" CssClass="tituloLabel form-label col-form-label col-form-label-sm">Número</asp:Label>
																			        <asp:TextBox id="txtnPOPs" runat="server" CssClass="texto form-control form-control-sm" ReadOnly="True"
																				        ToolTip="Número do Procedimento"></asp:TextBox>
                                                                                </div>

                                                                                <div class="row-8 gx-3 gy-2">
                                                                                    <div class="col">
                                                                                        <div class="row-12 mb-2">
                                                                                            <asp:Label id="lblTipoProcedimento" runat="server" CssClass="tituloLabel form-label col-form-label col-form-label-sm">Tipo de Procedimento</asp:Label>
                                                                                        </div>
                                                                                        <div class="row-12">
                                                                                            <asp:RadioButtonList id="rblTipoProcedimento" runat="server" ToolTip="Tipo de Procedimento"
																				               AutoPostBack="True" CssClass="texto form-control-sm">
																				                <asp:ListItem Value="0" Selected="True" CssClass="radioMargin">Resumo - Pai</asp:ListItem>
																				                <asp:ListItem Value="1">Específico - Filho</asp:ListItem>
																				                <asp:ListItem Value="2">Instrutivo</asp:ListItem>
																			                </asp:RadioButtonList>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
																			

                                                                            </div>
                                                                        </div>
																	</TR>
                                                                 
																</TABLE>
                                                                
                                                                <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                                                                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="conditional">
                                                                    <Triggers>
                                                                        <asp:PostBackTrigger ControlID="Button1" />
                                                                    </Triggers>
                                                                    <ContentTemplate>      
                                                                    
                                                                                <br>
                                                                                <asp:Label ID="Label10" runat="server" CssClass="tituloLabel form-label col-form-label col-form-label-sm">Foto do Procedimento</asp:Label>
                                                                                <br>
                                                                                <asp:FileUpload ID="searchFoto" runat="server" CssClass="texto form-control form-control-sm" onchange="callme(this)"
                                                                                    Width="498px" />
                                                                                    <asp:Button ID="Button1" runat="server" Text="Confirmar Foto" class="btn mt-2" OnClick="Button1_Click"/>
                                                                <asp:LinkButton ID="lkbClear" runat="server" CssClass="btn mt-2" 
                                                                    ToolTip="Remover arquivo selecionado para Foto" OnClick="lkbClear_Click">Limpar</asp:LinkButton>
                                                                                    </ContentTemplate>
                                                                    </asp:UpdatePanel>

                                        <table  border="0" cellpadding="0" cellspacing="0"     <%-- imagem / foto --%>
                                                                         width="375">
                                                        
													    <tr class="row">
                                                            <td class="col-12">
                                                                <asp:Label ID="Label5" runat="server" CssClass="boldFont" 
                                                                    ToolTip="Arquivo selecionado para Foto" Visible="False">Arquivo</asp:Label>
                                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                <asp:TextBox ID="txt_Arq" runat="server" CssClass="inputBox" Height="16px" 
                                                                    ReadOnly="True" Rows="4" TextMode="MultiLine" Width="478px" 
                                                                    Visible="False"></asp:TextBox>
                                                                <br />
                                                                <asp:TextBox ID="txtSelectedFile" runat="server" BackColor="WhiteSmoke" 
                                                                    BorderColor="DarkGray" CssClass="inputBox" ReadOnly="True" Visible="False" 
                                                                    Width="453px"></asp:TextBox>
                                                            </td>
                                                        </tr>
													</TABLE>
                                                    <br />
													<div>
                                                        <table border="0" cellpadding="0" cellspacing="0"      <%-- sobre procedimento --%>
                                                            width="395">
                                                            <tr>
                                                                <td  class="style2 espaco">
                                                                    <asp:Label ID="LblNome" runat="server" CssClass="tituloLabel form-label" 
                                                                        Font-Names="Tahoma" Font-Size="Small">Nome do Procedimento</asp:Label>
                                                                    <br />
                                                                    <asp:TextBox ID="txtNome" runat="server" CssClass="texto form-control form-control-sm" 
                                                                        Font-Size="Small" Height="75px" Rows="3" TextMode="MultiLine" 
                                                                        ToolTip="Nome do Procedimento" Width="280px" Font-Names="Tahoma"></asp:TextBox>
                                                                </td>
                                                                <td  style="height: 16px" width="198">
                                                                    <asp:Label ID="Label151" runat="server" CssClass="tituloLabel form-label"
                                                                        Font-Names="Tahoma" Font-Size="Small">Descrição Resumida do Procedimento</asp:Label>
                                                                    <br />
                                                                    <asp:TextBox ID="txtDescricao" runat="server" CssClass="texto form-control form-control-sm" 
                                                                        Font-Size="Small" Height="75px" Rows="3" TextMode="MultiLine" 
                                                                        ToolTip="Descrição do Procedimento" Width="280px" Font-Names="Tahoma"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </TD>
											    <td  width="195" valign="top">                                                       <%-- imagem / foto parte 2 --%>
                                                    <asp:Image ID="imgFoto" runat="server" Height="132px" 
                                                        ImageUrl="img/default_img.gif" Width="135px" />
                                                    <br>
                                                    <br />
                                                    <asp:LinkButton ID="lkbRemoverImagem" runat="server" CssClass="btn align-items-center pt-2" 
                                                        Enabled="False" ToolTip="Remover Imagem do Procedimento"><img border="0" 
                                                        src="Images/lixo.svg" class="w-auto"></img> Remover Imagem</asp:LinkButton>
                                               
                                                    <br />
                                               
                                                    <br>
                                              
                                                    <br />
                                                    <br />
                                                    <br />
                                              
                                                </td>
											</TR>
										</TABLE>
									    <br />
                                        <table style="width: 1000px"> <%-- procedimento resumo - pai --%>
                                        <tr>
                                        <td class="style3">
                                        <asp:Label ID="lblProcedimentoResumo" runat="server" CssClass="tituloLabel form-label" 
                                                Font-Names="Tahoma" Font-Size="Small">Procedimento 
                                        Resumo - Pai</asp:Label>
                                            
                                                <br>
                                                <asp:DropDownList ID="ddlProcedimentoResumo" runat="server" 
                                                    CssClass="texto form-select form-select-sm" Width="570px" Font-Names="Tahoma">
                                                </asp:DropDownList>
                                                <br />

                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        </table>
                                        <div >
                                            <table border="0" cellpadding="0" cellspacing="0"  width="750px"> <%-- última linha: colaboradores envolvidos e tipo de situação --%>
                                                <tr>
                                                    <td class="style5" width="280" >
                                                        <asp:Label ID="lblIncidencia" runat="server" CssClass="tituloLabel form-label">Colaboradores envolvidos</asp:Label>
                                                        <br />
                                                        <asp:RadioButtonList ID="rblIncidencia" runat="server"
                                                            CellPadding="0" CellSpacing="0" 
                                                            CssClass="texto form-check-input bg-transparent border-0" 
                                                            ToolTip="Tipo de Incidência do Procedimento" Width="275px" height="50px">
                                                            <asp:ListItem Value="1">Empregados</asp:ListItem>
                                                            <asp:ListItem Value="2">Terceiros</asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </td>
                                                    <td  class="style4" width="280">
                                                        <asp:Label ID="lblSituacao" runat="server" CssClass="tituloLabel form-label">Tipo de Situação</asp:Label>
                                                        <br />
                                                        <asp:RadioButtonList ID="rblSituacao" runat="server"
                                                         CellPadding="0" CellSpacing="0" 
                                                            CssClass="texto form-check-input bg-transparent border-0"  
                                                            ToolTip="Tipo de Situação do Procedimento" height="50px">
                                                            <asp:ListItem Value="1">Normal</asp:ListItem>
                                                            <asp:ListItem Value="2">Anormal</asp:ListItem>
                                                            <asp:ListItem Value="3">Emergêncial</asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </td>
                                                    <td width="190"></td>                                                   
                                                </tr>
                                            </table>
                                        </div>
                                        </TD>
                                        </TR>
                                        </TABLE>

                                        </eo:PageView>

                                <%-- DADOS COMPLEMENTARES --%>
                                
                                <eo:PageView ID="Pageview2" runat="server">
										<TABLE  id="Table7" cellSpacing="0" cellPadding="0" width="1000" border="0"> <%-- container inteiro de 'dados complementares' --%>
											<TR>
												<TD >
													<TABLE  id="Table25" cellSpacing="0" cellPadding="0" border="0"> <%-- subtítulo SETOR --%>
														<TR>
															<TD width="1000">
                                                                <div class="col-12 subtituloBG" style="padding-top: 10px">
																    <asp:Label id="Label1111" runat="server" CssClass="subtitulo">Setor</asp:Label>
                                                                </div>
															</TD>
														</TR>
													</TABLE>
                                                    <table  id="Table23" cellSpacing="0" cellPadding="0" width="630" border="0" align="center" style="margin-top: 30px;"> <%-- container SETOR --%>
                                                        <tr>
                                                            <TD vAlign="middle"  width="35"></TD>
                                                            <td valign="middle" width="265" style="margin: 0px">
                                                                <asp:ListBox id="lstSetor" runat="server" Width="265px" Height="160px" CssClass="texto form-control form-control-sm" SelectionMode="Multiple"
																	            Rows="2" AppendDataBoundItems="True"></asp:ListBox>
                                                            </td>
                                                            <td valign="middle" align="center" width="50">
                                                                <asp:ImageButton ID="imbAddSetor" runat="server" ImageUrl="Images/adicionar.svg" ToolTip="Adicionar Setor" CssClass="mb-3" /><BR>
                                                                <asp:ImageButton ID="imbRemoveSetor" runat="server" ImageUrl="Images/remover.svg" ToolTip="Remover Setor" />

                                                            </td>
                                                            <td valign="middle"  width="265">
                                                                <asp:ListBox id="lstSetorProc" runat="server" Width="265px" Height="160px" CssClass="texto form-control form-control-sm" SelectionMode="Multiple"
																	            Rows="2"></asp:ListBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <BR>
													<TABLE  id="Table17" cellSpacing="0" cellPadding="0" width="1000" border="0"> <%-- subtítulo CELULA --%>
														<TR>
															<TD align="left" width="1000">
                                                                <div class="col-12 subtituloBG" style="padding-top: 10px;">
                                                                    <asp:Label id="lblCelula" runat="server" CssClass="subtitulo">Celula</asp:Label>
                                                                </div>
															</TD>
														</TR>
													</TABLE>
													<TABLE  id="Table24" cellSpacing="0" cellPadding="0" width="630" border="0" align="center" style="margin-top: 30px;"> <%-- container CELULA --%>
														<TR>
															<%--<TD vAlign="middle"  width="30"><INPUT class=buttonBox id=btnNovaCelula style="WIDTH: 20px"  onclick="addItem(centerWin('CadCelula.aspx?IdEmpresa=<%=cliente.Id%>&IdUsuario=<%=usuario.Id%>', 400,305,'CadCelula'), 'Empresa')" tabIndex=0 type=button size=20 value=...></TD>--%>
                                                            <TD vAlign="top"  width="30"><INPUT class=btnMenor 
                                                                    id=btnNovaCelula style="WIDTH: 20px; height: 26px;"  
                                                                    tabIndex=0 type=button size=20 value=...>
                                                                </TD>
															<TD vAlign="middle"  width="265">
																<asp:ListBox id="lstCelula" runat="server" Width="265px" Height="160px" CssClass="texto form-control form-control-sm" SelectionMode="Multiple"
																	Rows="2" AppendDataBoundItems="True"></asp:ListBox></TD>
															<TD vAlign="middle" align="center"  width="50">
																<asp:imagebutton id="imbAddCelula" runat="server" ImageUrl="Images/adicionar.svg" ToolTip="Adicionar Celula" CssClass="mb-3"></asp:imagebutton><BR>
																<asp:imagebutton id="imbRemoveCelula" runat="server" ImageUrl="Images/remover.svg" ToolTip="Remover Celula"></asp:imagebutton></TD>
															<TD vAlign="middle"  width="265">
																<asp:ListBox id="lstCelulaProc" runat="server" Width="265px" Height="160px" CssClass="texto form-control form-control-sm" SelectionMode="Multiple"
																	Rows="2"></asp:ListBox></TD>
														</TR>
													</TABLE>
													<br />
													<TABLE  id="Table15" cellSpacing="0" cellPadding="0" width="1000" border="0"> <%-- subtítulo EQUIPAMENTO --%>
														<TR>
															<TD align="left" width="1000">
                                                                <div class="col-12 subtituloBG" style="padding-top: 10px;">
                                                                    <asp:Label id="lblEquipamento" runat="server" CssClass="subtitulo">Equipamento</asp:Label>
                                                                </div>
															</TD>
														</TR>
													</TABLE>
													<TABLE  id="Table11" cellSpacing="0" cellPadding="0" width="630" border="0" align="center" style="margin-top: 30px;"> <%-- container EQUIPAMENTO --%>
														<TR>
															<TD vAlign="top"  width="30"><INPUT class=btnMenor id=btnNovoEquipamento style="WIDTH: 20px" tabIndex=0 type=button size=20 value=...></TD>
															<TD vAlign="middle"  width="265">
																<asp:ListBox id="lstEquipamento" runat="server" Width="265px" Height="160px"
                                                                    CssClass="texto form-control form-control-sm" SelectionMode="Multiple"
																	Rows="2" AppendDataBoundItems="True"></asp:ListBox></TD>
															<TD vAlign="middle" align="center"  width="50">
																<asp:imagebutton id="imbAddEquipamento" runat="server" ImageUrl="Images/adicionar.svg" ToolTip="Adicionar Equipamento" CssClass="mb-3"></asp:imagebutton><BR>
																<asp:imagebutton id="imbRemoveEquipamento" runat="server" ImageUrl="Images/remover.svg" ToolTip="Remover Equipamento"></asp:imagebutton></TD>
															<TD vAlign="middle"  width="265">
																<asp:ListBox id="lstEquipamentoProc" runat="server" Width="265px" Height="160px" CssClass="texto form-control form-control-sm" SelectionMode="Multiple"
																	Rows="2"></asp:ListBox></TD>
														</TR>
													</TABLE>
													<BR>
													<TABLE  id="Table21" cellSpacing="0" cellPadding="0" width="1000" border="0"> <%-- subtítulo FERRAMENTA --%>
														<TR>
															<TD align="left" width="1000">
                                                                <div class="col-12 subtituloBG" style="padding-top: 10px;">
                                                                    <asp:Label id="lblFerramenta" runat="server" CssClass="subtitulo">Ferramenta</asp:Label>
                                                                </div>
															</TD>
														</TR>
													</TABLE>
													<TABLE  id="Table19" cellSpacing="0" cellPadding="0" width="630" border="0" align="center" style="margin-top: 30px;"> <%-- container FERRAMENTA --%>
														<TR>
															<TD vAlign="top"  width="30"><INPUT class=btnMenor id=btnNovaFerramenta style="WIDTH: 20px" tabIndex=0 type=button size=20 value=...></TD>
															<TD vAlign="middle"  width="265">
																<asp:ListBox id="lstFerramenta" runat="server" Width="265px" Height="160px" 
                                                                    CssClass="texto form-control form-control-sm" SelectionMode="Multiple"
																	Rows="2" AppendDataBoundItems="True"></asp:ListBox></TD>
															<TD vAlign="middle" align="center" width="50">
																<asp:imagebutton id="imbAddFerramenta" runat="server" ImageUrl="Images/adicionar.svg" ToolTip="Adicionar Ferramenta" CssClass="mb-3"></asp:imagebutton><BR>
																<asp:imagebutton id="imbRemoveFerramenta" runat="server" ImageUrl="Images/remover.svg" ToolTip="Remover Ferramenta"></asp:imagebutton></TD>
															<TD vAlign="middle"  width="265">
																<asp:ListBox id="lstFerramentaProc" runat="server" Width="265px" Height="160px" CssClass="texto form-control form-control-sm" SelectionMode="Multiple"
																	Rows="2"></asp:ListBox></TD>
														</TR>
													</TABLE>
													<BR>
													<TABLE  id="Table22" cellSpacing="0" cellPadding="0" width="1000" border="0"> <%-- subtítulo PRODUTO --%>
														<TR>
															<TD align="left" width="1000">
                                                                <div class="col-12 subtituloBG" style="padding-top: 10px">
                                                                    <asp:Label id="lblProduto" runat="server" CssClass="subtitulo">Produto</asp:Label>
                                                                </div>
															</TD>
														</TR>
													</TABLE>
													<TABLE  id="Table20" cellSpacing="0" cellPadding="0" width="630" border="0" align="center" style="margin-top: 30px;"> <%-- container PRODUTO --%>
														<TR>
															<TD vAlign="top"  width="30"><INPUT class=btnMenor id=btnNovoProduto style="WIDTH: 20px" tabIndex=0 type=button size=20 value=...></TD>
															<TD vAlign="middle"  width="265">
																<asp:ListBox id="lstProduto" runat="server" Width="265px" Height="160px"
                                                                    CssClass="texto form-control form-control-sm" SelectionMode="Multiple"
																	Rows="2" AppendDataBoundItems="True"></asp:ListBox></TD>
															<TD vAlign="middle" align="center" width="50">
																<asp:imagebutton id="imbAddProduto" runat="server" ImageUrl="Images/adicionar.svg" ToolTip="Adicionar Produto" CssClass="mb-3"></asp:imagebutton><BR>
																<asp:imagebutton id="imbRemoveProduto" runat="server" ImageUrl="Images/remover.svg" ToolTip="Remover Produto"></asp:imagebutton></TD>
															<TD vAlign="middle"  width="265">
																<asp:ListBox id="lstProdutoProc" runat="server" Width="265px" Height="160px" CssClass="texto form-control form-control-sm" SelectionMode="Multiple"
																	Rows="2"></asp:ListBox></TD>
														</TR>
													</TABLE>
												</TD>
											</TR>
										</TABLE>
                                </eo:PageView>

                                <%-- OPERAÇÕES --%>
                                
                                <eo:PageView ID="Pageview3" runat="server">
                                    <TABLE  id="Table5" cellSpacing="0" cellPadding="0"  border="0"> <%-- container inteiro de 'operações' --%>
                                        <TR>
                                            <TD>
                                                <table border="0" cellpadding="0" cellspacing="0"  width="1000"> <%-- primeiras linhas: operações, descrição, etc --%>
                                                    <tr>
                                                        <td  width="20">
                                                            <br />
                                                            <asp:imagebutton id="imbUpOperacao" runat="server" ToolTip="Mover Operação para a posição da Operação anterior" 
                                                                ImageUrl="Images/subir.svg"></asp:imagebutton><br />
                                                                    <br />
                                                            <asp:imagebutton id="imbDownOperacao" runat="server" ToolTip="Mover Operação para a posição da próxima Operação"
                                                                ImageUrl="Images/descer.svg"></asp:imagebutton>
                                                        </td>

                                                        <td  width="480">
                                                            <asp:Label id="lblOperacao" runat="server" CssClass="tituloLabel form-label col-form-label col-form-label-sm">Operações do Procedimento</asp:Label><br />
                                                            <asp:ListBox id="lstBoxOperacoes" runat="server" Width="430px" CssClass="texto form-control form-control-sm" Rows="5" AutoPostBack="True" Height="150px"></asp:ListBox>
                                                        </td>
                                                        
                                                        <td  width="500">
                                                            <div>
                                                                <table border="0" cellpadding="0" cellspacing="0" width="320">
                                                                    <tr>
                                                                        <td valign="top" width="145">
                                                                            <asp:Label id="lblOperacaoDescricao" runat="server" CssClass="tituloLabel form-label col-form-label col-form-label-sm">Descrição</asp:Label><br />
                                                                            <asp:TextBox id="txtOperacao" tabIndex="1" runat="server" Width="500px" CssClass="texto form-control form-control-sm"
                                                                                Rows="3" TextMode="MultiLine" Height="100px"></asp:TextBox>
                                                                        </td>

                                                                        <td  width="320">
                                                                            <%--<asp:Label id="lblObsQualidade" runat="server" CssClass="boldFont">Precauções</asp:Label><br />--%>
                                                                            <asp:TextBox id="txtObsQualidade" tabIndex="1" runat="server" Width="10px" CssClass="inputBox" Rows="3" 
                                                                                TextMode="MultiLine" Font-Size="X-Small" Height="59px" Visible="False"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                                </div>
                                                                
                                                                <br />
                                                                <asp:Button id="btnGravarOperacao" runat="server" Width="60px" Text="Gravar" CssClass="btn" onclick="btnGravarOperacao_Click1"></asp:Button>
                                                                <asp:Button id="btnExcluirOpe" runat="server" Width="60px" Text="Excluir" CssClass="btn" Enabled="False" onclick="btnExcluirOpe_Click"></asp:Button>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    <br />

                                                    <eo:TabStrip ID="TabStrip2" runat="server" ControlSkinID="None" MultiPageID="MultiPage2"> <%-- BOTÕES - SUBMENU --%>
                                                        <topgroup>
                                                            <Items>
                                                                <eo:TabItem Text-Html="Perigos - Fontes Geradoras">
                                                                </eo:TabItem>
                                                                <eo:TabItem Text-Html="Riscos">
                                                                </eo:TabItem>
                                                                <eo:TabItem Text-Html="Aspectos Ambientais">
                                                                </eo:TabItem>
                                                                <eo:TabItem Text-Html="Impactos Ambientais">
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
                                                    <div>

                                                        <eo:MultiPage ID="MultiPage2" runat="server" Height="300" Width="1000">


                                                            <eo:PageView ID="Pageview6" runat="server"> <%-- SUMENU: Perigos - Fontes Geradas --%>

																	<TABLE  id="Table8" cellSpacing="0" cellPadding="0" width="1000" 
																		border="0"> <%-- container inteiro do submenu 'perigos' --%>
                                                                        <TR>
                                                                            <TD  width="1000">
                                                                                <table border="0" cellpadding="0" cellspacing="0"  width="1000">
                                                                                    <tr class="row">
                                                                                        <td class="col-md-6">
                                                                                            <asp:Label ID="lblPerigos" runat="server" CssClass="tituloLabel form-label">Listagem dos Perigos</asp:Label><br />
                                                                                            <asp:ListBox id="lsbPerigos" runat="server" Width="450px" CssClass="texto form-control form-control-sm" 
                                                                                                Rows="7" SelectionMode="Multiple" Enabled="False"></asp:ListBox><div>
                                                                                                </div>
                                                                                            <br />
                                                                                            <INPUT class=btn id=btnCadPerigo style="WIDTH: 115px" tabIndex=0 type=button value="Adicionar / Editar" onclick="javascript:AbreCadPerigo(<%=cliente.Id%>, <%=usuario.Id%>);">
                                                                                        </td>
                                                                                        <td class="col-md-1 mt-5" style="padding-left: .7rem;">
                                                                                            <asp:ImageButton ID="imbAddPerigo" runat="server" ImageUrl="Images/adicionar.svg"
                                                                                                ToolTip="Adicionar Perigo a Operação" Enabled="False" OnClick="imbAddPerigo_Click" /><br />
                                                                                            <br />
                                                                                            <asp:ImageButton ID="imbRemovePerigo" runat="server" ImageUrl="Images/remover.svg"
                                                                                                ToolTip="Remover Perigo da Operação" Enabled="False" OnClick="imbRemovePerigo_Click" /><br />
                                                                                        </td>
                                                                                        <td class="col-md-5">
                                                                                            <asp:Label ID="lblPerigoSelected" runat="server" CssClass="tituloLabel form-label">Perigos Selecionados</asp:Label><br />
                                                                                            <asp:ListBox id="lsbPerigoSelected" runat="server"
                                                                                                CssClass="texto form-control form-control-sm" Rows="7" Enabled="False" AutoPostBack="True" onselectedindexchanged="lsbPerigoSelected_SelectedIndexChanged"></asp:ListBox>
                                                                                            <br />
                                                                                            <br />
                                                                                            <asp:Label ID="lblObsQualidade" runat="server" CssClass="tituloLabel form-label">Precauções 
                                                                                            associada ao Perigo Selecionado</asp:Label>
                                                                                            <br />
                                                                                            <asp:TextBox ID="txtPrecaucoes" runat="server" CssClass="texto form-control form-control-sm" Height="59px" Rows="3" 
                                                                                                tabIndex="1" TextMode="MultiLine"></asp:TextBox>
                                                                                            <br />
                                                                                            &nbsp;&nbsp;
                                                                                            <asp:Button ID="cmd_Salvar_Precaucoes" runat="server"
                                                                                                Text="Salvar Precauções" CssClass="btn" onclick="cmd_Salvar_Precaucoes_Click" />
                                                                                            <br />
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                        </tr>
																	</TABLE>
                                                                 </eo:PageView>

                                                                 <eo:PageView ID="Pageview7" runat="server"> <%-- SUBMENU: Riscos --%>
																	<TABLE  id="Table13" cellSpacing="0" cellPadding="0" width="1000" border="0"> <%-- container inteiro do submenu 'riscos' --%>
                                                                        <TR>
                                                                            <TD >
                                                                                <TABLE  id="Table26" cellSpacing="0" cellPadding="0" width="1000" 
																					border="0">
                                                                                    <TR>
                                                                                        <TD vAlign="top"  width="290">
                                                                                            <asp:Label ID="lblddlPerigo" runat="server" CssClass="tituloLabel form-label">Selecione o Perigo</asp:Label><br />
                                                                                            <asp:DropDownList ID="ddlPerigo" runat="server" CssClass="texto form-select form-select-sm" 
                                                                                                Width="431px" Enabled="False" AutoPostBack="True" OnSelectedIndexChanged="ddlPerigo_SelectedIndexChanged">
                                                                                            </asp:DropDownList><br /><table  border="0" cellpadding="0" cellspacing="0"  id="Table278" width="270">
                                                                                                <tr><TD align=center><asp:Image id="Image1211" runat="server" ImageUrl="img/5pixel.gif" Visible="false"></asp:Image></TD></tr>
                                                                                            </table>
                                                                                            <asp:Label ID="Label111" runat="server" CssClass="tituloLabel form-label">Riscos pontenciais do Perigo Selecionado</asp:Label><BR>
                                                                                            <TABLE  id="Table32" cellSpacing="0" cellPadding="0" width="270" border="0">
                                                                                                <TR>
                                                                                                    <TD >
                                                                                                        <asp:ListBox id="listBxRiscosOperacao" runat="server" Width="431px" 
                                                                                                            CssClass="texto form-control form-control-sm" AutoPostBack="True" Rows="5"></asp:ListBox></td>
                                                                                                </tr>
                                                                                            </table><table  border="0" cellpadding="0" cellspacing="0"  id="Table279" width="270" class="mb-2">
                                                                                                <tr><TD align=center><asp:Image id="Image125" runat="server" ImageUrl="img/5pixel.gif" Visible="false"></asp:Image></TD></tr>
                                                                                            </table>
                                                                                            <asp:Button ID="btnExcluirRisco" runat="server" CssClass="btn" Enabled="False"
                                                                                                Text="Excluir Risco" ToolTip="Excluir Risco" onclick="btnExcluirRisco_Click1" /></td>
                                                                                        <TD vAlign="top"  width="290">
                                                                                            <asp:Label ID="Label3" runat="server" CssClass="tituloLabel form-label">Grau de severidade</asp:Label><br />
                                                                                            <asp:RadioButtonList id="rblSeveridade" runat="server" Width="270px" 
                                                                                                CssClass="texto form-check-input bg-transparent border-0" ToolTip="Grau de Severidade" CellSpacing="0" CellPadding="0" 
                                                                                                RepeatDirection="Horizontal" RepeatColumns="2" Height="42px">
                                                                                                <asp:ListItem Value="1">Marginal</asp:ListItem>
                                                                                                <asp:ListItem Value="2">Crítica</asp:ListItem>
                                                                                                <asp:ListItem Value="4">Desprezível</asp:ListItem>
                                                                                            </asp:RadioButtonList><br />
                                                                                            <asp:Label ID="Label7" runat="server" CssClass="tituloLabel form-label">Nível de Risco</asp:Label><br />
                                                                                            <asp:RadioButtonList id="rblProbabilidade" runat="server" Width="270px" CssClass="texto form-check-input bg-transparent border-0" ToolTip="Probabilidade"
																				CellSpacing="0" CellPadding="0" RepeatDirection="Horizontal">
                                                                                                <asp:ListItem Value="1">Desprezível</asp:ListItem>
                                                                                                <asp:ListItem Value="2">Moderado</asp:ListItem>
                                                                                                <asp:ListItem Value="3">Crítico</asp:ListItem>
                                                                                            </asp:RadioButtonList><br />
                                                                                            <asp:Button ID="btnGravarRisco" runat="server" CssClass="btn" Enabled="False"
                                                                                                Text="Gravar Detalhes do Risco" ToolTip="Gravar Detalhes do Risco" onclick="btnGravarRisco_Click" /></td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                        </tr>
																	</TABLE>
                                                                 </eo:PageView>


                                                                 <eo:PageView ID="Pageview8" runat="server"> <%-- SUBMENU: Aspectos Ambientais --%>
                                                                    <TABLE  id="Table33" cellSpacing="0" cellPadding="0" width="1000" border="0"> <%-- container inteiro do submenu 'aspectos' --%>
                                                                        <TR>
                                                                            <TD  width="1000">
                                                                                <table border="0" cellpadding="0" cellspacing="0"  width="1000"> 
                                                                                    <tr class="row">
                                                                                        <td class="col-md-6">
                                                                                            <asp:Label ID="lblAspectos" runat="server" CssClass="tituloLabel form-label">Listagem dos Aspectos Ambientais</asp:Label><br />
                                                                                            <asp:ListBox id="lsbAspectos" runat="server" Width="430px" CssClass="texto form-control form-control-sm" Rows="7" SelectionMode="Multiple" Enabled="False"></asp:ListBox><div>
                                                                                                </div>
                                                                                            <br />
                                                                                            <INPUT class=btn id=btnCadAspectos style="WIDTH: 115px" tabIndex=0 type=button value="Adicionar / Editar" onclick="javascript:AbreCadAspecto(<%=cliente.Id%>, <%=usuario.Id%>);"></td>
                                                                                        <td class="col-md-1 mt-4">
                                                                                            <br />
                                                                                            <asp:ImageButton ID="imbAddAspecto" runat="server" ImageUrl="Images/adicionar.svg"
                                                                                                ToolTip="Adicionar Aspecto Ambiental a Operação" Enabled="False"/><br />
                                                                                            <br />
                                                                                            <asp:ImageButton ID="imbRemoveAspecto" runat="server" ImageUrl="Images/remover.svg"
                                                                                                ToolTip="Remover Aspecto Ambiental da Operação" Enabled="False"/><br />
                                                                                            <br />
                                                                                            <br />
                                                                                        </td>
                                                                                        <td class="col-md-4">
                                                                                            <asp:Label ID="lblAspectoSelected" runat="server" CssClass="tituloLabel form-label">Aspectos Ambientais Selecionados</asp:Label><br />
                                                                                            <asp:ListBox id="lsbAspectoSelected" runat="server" Width="430px" CssClass="texto form-control form-control-sm" SelectionMode="Multiple"
																												Rows="7" Enabled="False"></asp:ListBox></td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                 </eo:PageView>

                                                                 <eo:PageView ID="Pageview9" runat="server"> <%-- SUBMENU: Impactos Ambientais --%>
                                                                    <TABLE  id="Table41" cellSpacing="0" cellPadding="0" width="1000" border="0">
                                                                        <TR>
                                                                            <TD>
                                                                                <TABLE  id="Table40" cellSpacing="0" cellPadding="0" width="1000" 
																					border="0">
                                                                                    <TR>
                                                                                        <TD vAlign="top"  width="290">
                                                                                            <asp:Label ID="lblddlAspecto" runat="server" CssClass="tituloLabel form-label">Selecione o Aspecto Ambientais</asp:Label><br />
                                                                                            <asp:DropDownList ID="ddlAspecto" runat="server" CssClass="texto form-select form-select-sm" Width="431px" Enabled="False" AutoPostBack="True" OnSelectedIndexChanged="ddlAspecto_SelectedIndexChanged">
                                                                                            </asp:DropDownList><BR>
                                                                                            <TABLE  id="Table34" cellSpacing="0" cellPadding="0" width="270" 
																									border="0">
                                                                                                <TR>
                                                                                                    <TD >
                                                                                                        <asp:Image ID="Image121" runat="server" ImageUrl="img/5pixel.gif" Visible="false" /></td>
                                                                                                </tr>
                                                                                            </table>
                                                                                            <asp:Label ID="Label11111" runat="server" CssClass="tituloLabel form-label">Impactos Ambientais do Aspecto Selecionado</asp:Label><br />
                                                                                            <TABLE  id="Table39" cellSpacing="0" cellPadding="0" width="270" 
																									border="0">
                                                                                                <TR>
                                                                                                    <TD >
                                                                                                        <asp:ListBox id="lsbImpactosSelected" runat="server" Width="431px" CssClass="texto form-select form-select-sm" AutoPostBack="True"
																												Rows="5" OnSelectedIndexChanged="lsbImpactosSelected_SelectedIndexChanged"></asp:ListBox></td>
                                                                                                </tr>
                                                                                            </table>
                                                                                            <TABLE  id="Table38" cellSpacing="0" cellPadding="0" width="270" border="0" class="mb-2">
                                                                                                <TR>
                                                                                                    <TD >
                                                                                                        <asp:Image ID="Image31" runat="server" ImageUrl="img/5pixel.gif" Visible="false" /></td>
                                                                                                </tr>
                                                                                            </table><asp:Button ID="btnExcluirImpacto" runat="server" CssClass="btn" Enabled="False"
                                                                                                Text="Excluir Impacto" ToolTip="Excluir Impacto" Width="100px" OnClick="btnExcluirImpacto_Click"/></td>
                                                                                        <TD vAlign="top"  width="290">
                                                                                            <asp:Label ID="Label31" runat="server" CssClass="tituloLabel form-label">Grau de severidade</asp:Label><br />
                                                                                            <asp:RadioButtonList id="rblSeveridadeImpacto" runat="server" Width="270px" CssClass="texto form-check-input bg-transparent border-0 ms-3" ToolTip="Grau de Severidade"
																				CellSpacing="0" CellPadding="0" RepeatDirection="Horizontal">
                                                                                                <asp:ListItem Value="1">Ben&#233;fico</asp:ListItem>
                                                                                                <asp:ListItem Value="2">Adverso M&#233;dio</asp:ListItem>
                                                                                                <asp:ListItem Value="3">Adverso Alto</asp:ListItem>
                                                                                            </asp:RadioButtonList><br />
                                                                                            <asp:Label ID="Label71" runat="server" CssClass="tituloLabel form-label">Probabilidade</asp:Label><br />
                                                                                            <asp:RadioButtonList id="rblProbabilidadeImpacto" runat="server" Width="270px" CssClass="texto form-check-input bg-transparent border-0 ms-3" ToolTip="Probabilidade"
																				CellSpacing="0" CellPadding="0" RepeatDirection="Horizontal" RepeatColumns="2">
                                                                                                <asp:ListItem Value="1">Superior a 1 Ano</asp:ListItem>
                                                                                                <asp:ListItem Value="2">Superior a 1 M&#234;s</asp:ListItem>
                                                                                                <asp:ListItem Value="3">Inferior a 1 M&#234;s</asp:ListItem>
                                                                                            </asp:RadioButtonList><br />
                                                                                            <asp:Button ID="btnGravarDetalhesImpacto" runat="server" CssClass="btn" Enabled="False"
                                                                                                Text="Gravar Detalhes do Impacto" ToolTip="Gravar Detalhes do Impacto" OnClick="btnGravarDetalhesImpacto_Click"  /></td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                        </tr>
                                                                    </table>

                                                              </eo:PageView>
                                                          </eo:MultiPage>
                                                        </TD>
                                        </TR>
                                    </TABLE>
                                </eo:PageView>

                                <%-- MEDIDAS DE CONTROLE --%>
                                
                                <eo:PageView ID="Pageview4" runat="server">
                                    <TABLE  id="Table4" cellSpacing="0" cellPadding="0" width="1000"  border="0"> <%-- container inteiro de medidas de controle --%>
                                        <TR>
                                            <TD>
                                                <eo:TabStrip ID="TabStrip3" runat="server" ControlSkinID="None" MultiPageID="MultiPage3"> <%-- SUBMENU: BOTÕES --%>
                                                    <topgroup>
                                                        <Items>
                                                            <eo:TabItem Text-Html="Dados no Procedimento"></eo:TabItem>
                                                            <eo:TabItem Text-Html="Dados por GHE"></eo:TabItem>
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
                                                    <div>

                                                        <eo:MultiPage ID="MultiPage3" runat="server" Height="400" Width="1000"> 

                                                            <eo:PageView ID="Pageview10" runat="server"> <%--SUBMENU:  Dados do Procedimento --%>

																	<TABLE  id="Table7" cellSpacing="0" cellPadding="0" width="1000" border="0"> <%-- container inteiro de todo o subtópico 'dados'--%>
																		<TR>
																			<TD >
																				<TABLE  id="Table9" cellSpacing="0" cellPadding="0" width="1000" border="0" style="margin-top: 15px;">
																					<TR>
																						<TD vAlign="top"  width="334" align="center">
																							<asp:label id="lblEPI" runat="server" CssClass="tituloLabel form-label col-form-label col-form-label-sm">EPI - Equipamento de Proteção Individual</asp:label><BR>
																							<asp:ListBox id="lsbEPI" runat="server" Width="334px" Height="100px" CssClass="texto form-control form-control-sm" 
                                                                                                    Rows="3" SelectionMode="Multiple"></asp:ListBox>
																							<asp:Image id="Image11" runat="server" ImageUrl="img/5pixel.gif" Visible="false"></asp:Image><BR>
																							<asp:imagebutton id="imbDown" runat="server" ImageUrl="Images/adicionar.svg"
																								ToolTip="Adicionar EPIs Selecionados" ImageAlign="Middle" align="center" CssClass="me-3"></asp:imagebutton>
																							<asp:imagebutton id="imbUp" runat="server" ImageUrl="Images/remover.svg" ToolTip="Remover EPIs Selecionados" ImageAlign="Middle" align="center"></asp:imagebutton><BR>
																							<asp:Image id="Image21" runat="server" ImageUrl="img/5pixel.gif" Visible="false"></asp:Image><BR>
																							<asp:listbox id="lstBoxEPIs" runat="server" Width="332px" Height="100px" CssClass="texto form-control form-control-sm"
																								SelectionMode="Multiple"></asp:listbox></TD>
																						<TD vAlign="top"  width="290" align="center">
																							<asp:label id="lblMedidasAdm" runat="server" CssClass="tituloLabel form-label col-form-label col-form-label-sm">Medidas de Caráter Administrativo</asp:label><BR>
																							<asp:textbox id="txtMedidasAdm" runat="server" Width="343px" Height="250px" CssClass="texto form-control form-control-sm" Rows="10"
																								TextMode="MultiLine"></asp:textbox></TD>
																					</TR>
																					<TR>
																						<TD vAlign="top"  width="290" align="center"><BR>
																							<asp:label id="lblEPC" runat="server" CssClass="tituloLabel form-label col-form-label col-form-label-sm">EPC - Equipamento de Proteção Coletiva</asp:label><BR>
																							<asp:textbox id="txtEPC" runat="server" Width="334px" Height="100px" CssClass="texto form-control form-control-sm"  
                                                                                                    Rows="5" TextMode="MultiLine"></asp:textbox></TD>
																						<TD vAlign="top"  width="290" align="center"><BR>
																							<asp:label id="lblMedidasEdu" runat="server" CssClass="tituloLabel form-label col-form-label col-form-label-sm">Medidas de Caráter Educacional</asp:label><BR>
																							<asp:textbox id="txtMedidasEdu" runat="server" Width="346px" Height="100px" CssClass="texto form-control form-control-sm"  Rows="5"
																								TextMode="MultiLine"></asp:textbox></TD>
																					</TR>
																				</TABLE>
																			</TD>
																		</TR>
																	</TABLE>

                                                            </eo:PageView>

                                                            <eo:PageView ID="Pageview11" runat="server"> <%-- SUBMENU: Dados por GHE --%>

																	<TABLE  id="Table10" cellSpacing="0" cellPadding="0" width="1000" border="0"> <%-- container inteiro de 'dados ghe' --%>
																		<TR>
																			<TD >
																				<TABLE  id="Table12" cellSpacing="0" cellPadding="0" width="1000" border="0"> <%-- linha 1 --%>
																					<TR class="row">
																						<TD  class="col-md-6 gx-3">
																							<asp:Label id="Label112" runat="server" CssClass="tituloLabel form-label col-form-label col-form-label-sm">Selecione o Levantamento</asp:Label><BR>
																							<asp:DropDownList id="ddlLaudos" runat="server" CssClass="texto form-select form-select-sm" AutoPostBack="True"></asp:DropDownList></TD>
																						<TD  class="col-md-6 gx-3">
																							<asp:Label id="Label13" runat="server" CssClass="tituloLabel form-label col-form-label col-form-label-sm">Selecione o GHE</asp:Label><BR>
																							<asp:DropDownList id="ddlGHE" runat="server" CssClass="texto form-select form-select-sm" 
                                                                                                    AutoPostBack="True"></asp:DropDownList></TD>
																					</TR>
																				</TABLE>
																				<BR>
																				<TABLE  id="Table9" cellSpacing="0" cellPadding="0" width="1000" border="0"> <%-- linha 2 e 3 --%>
																					<TR class="row">
																						<TD class="col-md-6 gx-3">
																							<asp:label id="Label18" runat="server" CssClass="tituloLabel form-label col-form-label col-form-label-sm">EPI - Equipamento de Proteção Individual</asp:label><BR>
																							<asp:listbox id="lstBoxEPIGHE" runat="server"  CssClass="texto form-control form-control-sm" Height="100px"
                                                                                                    Rows="5"></asp:listbox></TD>
																						<TD  class="col-md-6 gx-3">
																							<asp:label id="Label17" runat="server" CssClass="tituloLabel form-label col-form-label col-form-label-sm">Medidas de Caráter Administrativo</asp:label><BR>
																							<asp:textbox id="txtMedidasAdmGHE" runat="server" 
                                                                                                    CssClass="texto form-control form-control-sm" Height="100px" Rows="5"
																								TextMode="MultiLine" ReadOnly="True"></asp:textbox></TD>
																					</TR>
																					<TR class="row">
																						<TD class="col-md-6 gx-3"><BR>
																							<asp:label id="Label16" runat="server" CssClass="tituloLabel form-label col-form-label col-form-label-sm">EPC - Equipamento de Proteção Coletiva</asp:label><BR>
																							<asp:textbox id="txtEPCGHE" runat="server" CssClass="texto form-control form-control-sm" Height="100px"
                                                                                                    Rows="4" TextMode="MultiLine"
																								ReadOnly="True"></asp:textbox></TD>
																						<TD class="col-md-6 gx-3"><BR>
																							<asp:label id="Label152" runat="server" CssClass="tituloLabel form-label col-form-label col-form-label-sm">Medidas de Caráter Educacional</asp:label><BR>
																							<asp:textbox id="txtMedidasEduGHE" runat="server"
                                                                                                    CssClass="texto form-control form-control-sm" Rows="4" Height="100px"
																								TextMode="MultiLine" ReadOnly="True"></asp:textbox></TD>
																					</TR>
																				</TABLE>
																				<BR>
                                                                                <div class="gx-3">
																				<asp:Button id="btnTransfMedidasControle" runat="server" Text="Transferir dados para o Procedimento"
																					CssClass="btn" onclick="btnTransfMedidasControle_Click1"></asp:Button>
                                                                                </div>
                                                                                    </TD>
																		</TR>
																	</TABLE>

                                                               </eo:PageView>

                                                          </eo:MultiPage>
                                                        </TD>
											</TR>
										</TABLE>
                                        </eo:PageView>
                                
                                <%-- ATUALIZAÇÕES --%>
                                
                                <eo:PageView ID="Pageview5" runat="server">
                                    <TABLE  id="Table10" cellSpacing="0" cellPadding="0" width="1000"  border="0"> <%-- container inteiro de 'atualizações' --%>
											<TR>
												<TD >
													<asp:Label id="lblAtualizacoes" runat="server" CssClass="tituloLabel form-label col-form-label col-form-label-sm">Listagem das Atualizações do Procedimento</asp:Label><BR> <%-- TÍTULO --%>
													<asp:ListBox id="lstBoxAtualizacoes" runat="server" Width="1000px" 
                                                            CssClass="texto form-control form-control-sm" Rows="8"
														AutoPostBack="True"></asp:ListBox><BR>
													<BR>
													<TABLE  id="Table9" cellSpacing="0" cellPadding="0" width="1000" border="0"> <%-- forms --%>
														<TR class="row">
															<TD class="col-12">
                                                                <div class="row">
                                                                    <div class="col-md-2">
                                                                        <asp:Label id="lblData" runat="server" CssClass="tituloLabel form-label col-form-label col-form-label-sm">Data da Atualização</asp:Label><BR>
																        <asp:textbox id="wdtDataAtualizacao" runat="server" ImageDirectory=" " Width="150px" CssClass="texto form-control form-control-sm" Horizontal
																	        Nullable="False" EditModeFormat="dd/MM/yyyy"></asp:textbox>
                                                                    </div>
                                                                    <div class="col-md-10">
                                                                        <asp:Label id="lblDescricao" runat="server" CssClass="tituloLabel form-label col-form-label col-form-label-sm">Descrição</asp:Label><BR>
																        <asp:TextBox id="txtDescricaoAtualizacao" runat="server" Height="100px" CssClass="texto form-control form-control-sm"
                                                                            Rows="3" TextMode="MultiLine"></asp:TextBox>&nbsp; </TD>
                                                                    </div>
                                                                </div>
																
																
															<TD vAlign="top"  width="295">
																<TABLE  id="Table12" cellSpacing="0" cellPadding="0" width="1000" 
																	border="0">
																	<TR>
																		<TD align="left" class="subtituloBG">
																			<asp:Label id="lblAprovacoes" runat="server" CssClass="subtitulo"> Aprovações</asp:Label></TD>
																	</TR>
																</TABLE>
																<asp:Panel id="Panel12" runat="server" Width="1000" style="margin-top: 15px;">
																	<asp:Image id="Image13" runat="server" ImageUrl="img/5pixel.gif" visible="false"></asp:Image>
																	<TABLE  id="Table14" cellSpacing="0" cellPadding="0" border="0" width="1000">
																		<TR class="row">
																			<TD class="col-12"><asp:Label id="lblResponsavel" runat="server" CssClass="tituloLabel form-label col-form-label col-form-label-sm">Responsável</asp:Label></TD><BR>
                                                                            <TD class="col-6"><asp:DropDownList id="ddlResponsavelAtualizacao" runat="server" Width="490px" CssClass="texto form-select form-select-sm"></asp:DropDownList></TD>
                                                                            <TD class="col-1"><INPUT class=btnMenor id=btnResponsavelAtualizacao style="WIDTH: 20px" tabIndex=3 type=button value=... onclick="addItem(centerWin('../CommonPages/CadResponsavel.aspx?IdEmpresa=<%=cliente.Id%>&IdUsuario=<%=usuario.Id%>', 350, 300, 'CadastroResponsavel'), 'Empresa');"></TD>
																		</TR>
																	</TABLE>
																	<TABLE  id="Table16" cellSpacing="0" cellPadding="0" border="0" width="1000">
																		<TR class="row">
																			<TD class="col-12"><asp:Label id="lblOperador" runat="server" CssClass="tituloLabel form-label col-form-label col-form-label-sm">Operador</asp:Label></TD><BR>
                                                                            <TD class="col-6"><asp:DropDownList id="ddlOperadorAtualizacao" runat="server" Width="490px" CssClass="texto form-select form-select-sm"></asp:DropDownList></TD>
                                                                            <TD class="col-1"><INPUT class=btnMenor id=btnOperadorAtualizacao style="WIDTH: 20px" tabIndex=3 type=button value=... onclick="addItem(centerWin('../CommonPages/CadResponsavel.aspx?IdEmpresa=<%=cliente.Id%>&IdUsuario=<%=usuario.Id%>', 350, 300, 'CadastroOperador'), 'Empresa');"></TD>
																		</TR>
																	</TABLE>
																	<TABLE  id="Table18" cellSpacing="0" cellPadding="0" width="255" 
																		border="0">
																		<TR>
																			<TD >
																				<asp:Image id="Image23" runat="server" ImageUrl="img/5pixel.gif" Visible="false"></asp:Image></TD>
																		</TR>
																	</TABLE>
																</asp:Panel></TD>
														</TR>
													</TABLE>
													<BR>
													<asp:Button id="btnGravarAtualizacao" runat="server" Width="130px" Text="Gravar Atualização"
														CssClass="btn" onclick="btnGravarAtualizacao_Click1" ></asp:Button>
													<asp:Button id="btnRemoverAtualizacao" runat="server" Width="130px" Text="Excluir Atualização"
														CssClass="btn" Enabled="False" onclick="btnRemoverAtualizacao_Click1"></asp:Button></TD>
											</TR>
										</TABLE>

                               </eo:PageView>

                            </eo:MultiPage>
                                        

                            <%-- BOTÕES FINAIS --%>

                     <div class="col-11 text-center mt-4"> 
						<asp:button id="btnGravar" runat="server" Width="85px" Text="Gravar" CssClass="btn" onclick="btnGravar_Click" ></asp:button>&nbsp;&nbsp;&nbsp;
						<asp:button id="btnExcluir" runat="server" Width="85px" Text="Excluir" CssClass="btn" onclick="btnExcluir_Click"></asp:button>&nbsp;&nbsp;&nbsp;
						<asp:button id="btnDuplicarProc" runat="server" Width="85px" Text="Duplicar" CssClass="btn" onclick="btnDuplicarProc_Click"></asp:button>&nbsp;&nbsp;&nbsp;
                        &nbsp; &nbsp;<asp:Button ID="btnAPP" runat="server" CssClass="btn" Text="APP" Width="85px" OnClick="btnAPP_Click" />&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnCancelar" runat="server" CssClass="btn" Text="Cancelar" Width="85px" OnClick="btnCancelar_Click" />
                        
                            <asp:Label ID="lbl_Id_Empresa" runat="server" Text="IdEmpresa" Visible="False"></asp:Label>
                            <asp:Label ID="lbl_Id_Usuario" runat="server" Text="IdUsuario" Visible="False"></asp:Label>
                    
                            <asp:Label ID="lbl_Id_Procedimento" runat="server" Text="0" Visible="False"></asp:Label>
                    
                            <br />
                         </div>
                    <div class="col-11 text-center mt-3">
                        <asp:LinkButton ID="lkbFichaPOPs" runat="server" CssClass="mt-2" OnClick="lkbFichaPOPs_Click"> Ficha POPs Horizontal</asp:LinkButton>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:LinkButton ID="lkbFichaPOPsV" runat="server" CssClass="mt-2" OnClick="lkbFichaPOPsV_Click"> Ficha POPs Vertical</asp:LinkButton>
                    </div>
                            <br />
                    </TD>
				</TR>
			</TABLE>
			<INPUT id="txtAuxiliar" type="hidden" name="txtAuxiliar" runat="server" />
        </div>
    </div>


         <eo:MsgBox ID="MsgBox1" runat="server" BackColor="#47729F" ControlSkinID="None" 
        HeaderHtml="Dialog Title" Height="100px" Width="168px">
        <HeaderStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #ffbf00 3px solid; padding-left: 4px; font-weight: bold; font-size: 11px; padding-bottom: 2px; color: white; padding-top: 2px; border-bottom: #22456a 1px solid; font-family: verdana" />
        <ContentStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #7d97b6 1px solid; padding-left: 4px; border-left-width: 1px; font-size: 11px; border-left-color: #728eb8; padding-bottom: 4px; color: white; padding-top: 4px; border-bottom: #22456a 1px solid; font-family: verdana" />
        <FooterStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #7d97b6 1px solid; padding-left: 4px; border-left-width: 1px; font-size: 11px; border-left-color: #728eb8; padding-bottom: 4px; color: white; padding-top: 4px; border-bottom: #22456a 1px solid; font-family: verdana" />
    </eo:MsgBox>

</asp:Content>