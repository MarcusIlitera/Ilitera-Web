<%@ Page language="c#" ValidateRequest="False" Inherits="Ilitera.Net.CadastroCurso" Codebehind="CadastroCurso.aspx.cs" %>

<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>


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
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0">
		<form method="post" runat="server">
            <div class="container d-flex ms-5 ps-4 justify-content-center mt-2">
                <div class="row gx-5 gy-2" style="width: 570px">
                    <div class="col-12 subtituloBG mb-1 text-center" style="padding-top: 10px; margin-top: 20px;">
                        <asp:label id="lblCadastroCurso" runat="server" SkinID="TitleFont" CssClass="subtitulo" style="margin-left: 0px !important">Dados de Treinamentos</asp:label>
                    </div>

                   <div class="col-12 mb-3">
                        <div class="row">
                            <div class="col-md-12 gx-2 gy-2">
                                <fieldset>
                                    <asp:Label id="Label1" runat="server" Text="Selecione o Treinamento" CssClass="tituloLabel form-label"></asp:Label>
                                    <asp:DropDownList ID="ddlCursos" runat="server" AutoPostBack="True" CssClass="texto form-select form-select-sm" onselectedindexchanged="ddlCursos_SelectedIndexChanged"></asp:DropDownList>
                                </fieldset>
                            </div>
                        <div class="col-md-3 gx-2 gy-2">
                            <fieldset>
                                <asp:Label id="Label9" runat="server" CssClass="tituloLabel form-label">Data de Início</asp:Label><BR>
                                <asp:textbox id="wdeDataInicio" runat="server" CssClass="texto form-control form-control-sm" ImageDirectory=" " EditModeFormat="dd/MM/yyyy" DisplayModeFormat="dd/MM/yyyy" HorizontalAlign="Center" MinValue="1800-01-01"></asp:textbox>
                            </fieldset>
                        </div>
                        <div class="col-md-3 gx-2 gy-2">
                             <fieldset>
                                 <asp:Label id="Label2" runat="server" CssClass="tituloLabel form-label">Período</asp:Label><BR>
                                 <asp:TextBox id="txtPeriodo" runat="server" CssClass="texto form-control form-control-sm"></asp:TextBox>
                             </fieldset>
                        </div>

                        <div class="col-md-6 gx-2 gy-2">
                            <fieldset>
                                <asp:Label id="Label3" runat="server" CssClass="tituloLabel form-label">Instrutor</asp:Label><BR>
                                <asp:DropDownList id="ddlTreinador" runat="server" CssClass="texto form-select form-select-sm" AutoPostBack="True"></asp:DropDownList>
                            </fieldset>
                        </div>
                    </div>
                </div>

                    <div class="col-12 subtituloBG mb-1 text-center" style="padding-top: 10px; margin-top: 20px;">
                        <asp:Label id="Label4" runat="server" CssClass="subtitulo" style="margin-left: 0px !important">Participantes do Treinamento</asp:Label>
                    </div>

                    <div class="col-5 gx-1 gy-2">
                        <asp:Label id="Label5" runat="server" CssClass="tituloLabel">Empregados</asp:Label><BR>
                        <asp:ListBox id="lsbEmpregados" runat="server" CssClass="texto form-control form-control-sm" Rows="13" SelectionMode="Multiple"></asp:ListBox>
                    </div>

                    <div class="col-2">
                        <asp:ImageButton id="ImbAdiciona" runat="server" ToolTip="Adiciona Selecionado" 
                            ImageUrl="InfragisticsImg/XpOlive/next_down.gif" Visible="False"></asp:ImageButton><BR>
                        <BR>
                        <asp:ImageButton id="ImbAdicionaTodos" runat="server" ToolTip="Adiciona Todos" 
                            ImageUrl="InfragisticsImg/XpOlive/ff_down.gif" Visible="False"></asp:ImageButton><BR>
                        <BR>
                        <asp:ImageButton id="ImbRemove" runat="server" ToolTip="Remove Selecionado" 
                            ImageUrl="InfragisticsImg/XpOlive/prev_down.gif" Visible="False"></asp:ImageButton><BR>
                        <BR>
                        <asp:ImageButton id="ImbRemoveTodos" runat="server" ToolTip="Remove Todos" 
                            ImageUrl="InfragisticsImg/XpOlive/rew_down.gif" Visible="False"></asp:ImageButton>
                    </div>

                    <div class="col-5 gx-1 gy-2">
                        <asp:Label id="Label6" runat="server" CssClass="tituloLabel">Participantes</asp:Label><BR>
                        <asp:ListBox id="lsbParticipantes" runat="server" CssClass="texto form-control form-control-sm" Rows="13" SelectionMode="Multiple"></asp:ListBox>
                    </div>

                    <div class="col-12 gx-2 gy-3 text-center">
                        <asp:Button id="btnNovoParticipante" runat="server" CssClass="btn" Text="Novo Participante" Visible="False"></asp:Button>
                        <asp:button id="btnGravar" runat="server" CssClass="btn" Text="Gravar" Enabled="False" Visible="False" onclick="btnGravar_Click"></asp:button>
                        <asp:button id="btnExcluir" runat="server" CssClass="btn" Text="Excluir" Enabled="False" Visible="False" onclick="btnExcluir_Click"></asp:button>
                        <input id="Button1" class="btn2" size="" type="button" value="Cancelar" />
                        <INPUT id="txtAuxiliar" type="hidden" name="txtAuxiliar" runat="server">
                    </div>
                </div>
            </div>

			

<%--									<igtab:ultrawebtab id="UltraWebTabCertificado" runat="server" Width="560px" 
                                        ImageDirectory="../InfragisticsImg/" BorderWidth="1px" ThreeDEffect="False"
										BorderStyle="Solid" BorderColor="#949878" Height="310px" CssClass="normalFont" SelectedTab="2">
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
											<igtab:Tab Key="TextoCertificadoColetivo" Text="Texto do Certificado Coletivo" 
                                                Tooltip="Texto do Certificado Coletivo" Visible="False">
												<ContentTemplate>
													<TABLE class="normalFont" id="Table1" cellSpacing="0" cellPadding="0" width="550" align="center"
														border="0">
														<TR>
															<TD align="center">
                                                                <ighedit:WebHtmlEditor ID="WHETextoColetivo" runat="server" BackgroundImageName=""
                                                                    Focus="False" FontFormattingList="Heading 1=<h1>&Heading 2=<h2>&Heading 3=<h3>&Heading 4=<h4>&Heading 5=<h5>&Normal=<p>"
                                                                    FontNameList="Arial,Verdana,Tahoma,Courier New,Georgia" FontSizeList="1,2,3,4,5,6,7"
                                                                    FontStyleList="Blue Underline=color:blue;text-decoration:underline;&Red Bold=color:red;font-weight:bold;&ALL CAPS=text-transform:uppercase;&all lowercase=text-transform:lowercase;&Reset="
                                                                    Height="245px" SpecialCharacterList="O,S,?,F,G,?,?,T,?,?,?,�,?,f,?,e,?,d,?,?,�,p,s,�,�,�,�,?,?,?,?,?,?,?,?,?,?,?,?,�,�,�,�,�,�,�,�,�,�,�,�,�,�,�,�,�,?,�,�,�,�,@,�,�,&#14;,?,?,?,?,?,?,?,?,?,?,&#18;,�,�,�,�,�,�,�,�,�,�,�,�,�,�,�,�,�,�,�,�,�,�,�"
                                                                    Width="540px">
                                                                    <Toolbar Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                                        Font-Underline="False">
                                                                        <ighedit:ToolbarImage runat="server" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                                                            Font-Strikeout="False" Font-Underline="False" Type="DoubleSeparator" />
                                                                        <ighedit:ToolbarButton runat="server" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                                                            Font-Strikeout="False" Font-Underline="False" Type="Bold" />
                                                                        <ighedit:ToolbarButton runat="server" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                                                            Font-Strikeout="False" Font-Underline="False" Type="Italic" />
                                                                        <ighedit:ToolbarButton runat="server" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                                                            Font-Strikeout="False" Font-Underline="False" Type="Underline" />
                                                                        <ighedit:ToolbarButton runat="server" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                                                            Font-Strikeout="False" Font-Underline="False" Type="Strikethrough" />
                                                                        <ighedit:ToolbarImage runat="server" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                                                            Font-Strikeout="False" Font-Underline="False" Type="Separator" />
                                                                        <ighedit:ToolbarButton runat="server" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                                                            Font-Strikeout="False" Font-Underline="False" Type="Undo" />
                                                                        <ighedit:ToolbarButton runat="server" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                                                            Font-Strikeout="False" Font-Underline="False" Type="Redo" />
                                                                        <ighedit:ToolbarImage runat="server" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                                                            Font-Strikeout="False" Font-Underline="False" Type="Separator" />
                                                                        <ighedit:ToolbarButton runat="server" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                                                            Font-Strikeout="False" Font-Underline="False" Type="JustifyLeft" />
                                                                        <ighedit:ToolbarButton runat="server" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                                                            Font-Strikeout="False" Font-Underline="False" Type="JustifyCenter" />
                                                                        <ighedit:ToolbarButton runat="server" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                                                            Font-Strikeout="False" Font-Underline="False" Type="JustifyRight" />
                                                                        <ighedit:ToolbarButton runat="server" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                                                            Font-Strikeout="False" Font-Underline="False" Type="JustifyFull" />
                                                                        <ighedit:ToolbarImage runat="server" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                                                            Font-Strikeout="False" Font-Underline="False" Type="Separator" />
                                                                        <ighedit:ToolbarDialogButton runat="server" Font-Bold="False" Font-Italic="False"
                                                                            Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Type="FontColor">
                                                                            <Dialog Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                                                Font-Underline="False" />
                                                                        </ighedit:ToolbarDialogButton>
                                                                        <ighedit:ToolbarDialogButton runat="server" Font-Bold="False" Font-Italic="False"
                                                                            Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Type="FindReplace">
                                                                            <Dialog Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                                                Font-Underline="False" InternalDialogType="FindReplace" />
                                                                        </ighedit:ToolbarDialogButton>
                                                                        <ighedit:ToolbarButton runat="server" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                                                            Font-Strikeout="False" Font-Underline="False" Type="WordCount" />
                                                                        <ighedit:ToolbarImage runat="server" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                                                            Font-Strikeout="False" Font-Underline="False" Type="RowSeparator" />
                                                                        <ighedit:ToolbarImage runat="server" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                                                            Font-Strikeout="False" Font-Underline="False" Type="DoubleSeparator" />
                                                                        <ighedit:ToolbarDropDown runat="server" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                                                            Font-Strikeout="False" Font-Underline="False" Type="FontName">
                                                                        </ighedit:ToolbarDropDown>
                                                                        <ighedit:ToolbarDropDown runat="server" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                                                            Font-Strikeout="False" Font-Underline="False" Type="FontSize">
                                                                        </ighedit:ToolbarDropDown>
                                                                    </Toolbar>
                                                                    <RightClickMenu Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                                        Font-Underline="False">
                                                                        <ighedit:HtmlBoxMenuItem runat="server" Act="Cut" Font-Bold="False" Font-Italic="False"
                                                                            Font-Overline="False" Font-Strikeout="False" Font-Underline="False">
                                                                            <Dialog Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                                                Font-Underline="False" />
                                                                        </ighedit:HtmlBoxMenuItem>
                                                                        <ighedit:HtmlBoxMenuItem runat="server" Act="Copy" Font-Bold="False" Font-Italic="False"
                                                                            Font-Overline="False" Font-Strikeout="False" Font-Underline="False">
                                                                            <Dialog Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                                                Font-Underline="False" />
                                                                        </ighedit:HtmlBoxMenuItem>
                                                                        <ighedit:HtmlBoxMenuItem runat="server" Act="Paste" Font-Bold="False" Font-Italic="False"
                                                                            Font-Overline="False" Font-Strikeout="False" Font-Underline="False">
                                                                            <Dialog Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                                                Font-Underline="False" />
                                                                        </ighedit:HtmlBoxMenuItem>
                                                                        <ighedit:HtmlBoxMenuItem runat="server" Act="PasteHtml" Font-Bold="False" Font-Italic="False"
                                                                            Font-Overline="False" Font-Strikeout="False" Font-Underline="False">
                                                                            <Dialog Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                                                Font-Underline="False" />
                                                                        </ighedit:HtmlBoxMenuItem>
                                                                        <ighedit:HtmlBoxMenuItem runat="server" Act="CellProperties" Font-Bold="False" Font-Italic="False"
                                                                            Font-Overline="False" Font-Strikeout="False" Font-Underline="False">
                                                                            <Dialog Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                                                Font-Underline="False" InternalDialogType="CellProperties" />
                                                                        </ighedit:HtmlBoxMenuItem>
                                                                        <ighedit:HtmlBoxMenuItem runat="server" Act="TableProperties" Font-Bold="False" Font-Italic="False"
                                                                            Font-Overline="False" Font-Strikeout="False" Font-Underline="False">
                                                                            <Dialog Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                                                Font-Underline="False" InternalDialogType="ModifyTable" />
                                                                        </ighedit:HtmlBoxMenuItem>
                                                                        <ighedit:HtmlBoxMenuItem runat="server" Act="InsertImage" Font-Bold="False" Font-Italic="False"
                                                                            Font-Overline="False" Font-Strikeout="False" Font-Underline="False">
                                                                            <Dialog Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                                                Font-Underline="False" />
                                                                        </ighedit:HtmlBoxMenuItem>
                                                                    </RightClickMenu>
                                                                    <DropDownStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                                        Font-Underline="False" />
                                                                    <ProgressBar Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                                        Font-Underline="False" />
                                                                    <DownlevelTextArea Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                                        Font-Underline="False" />
                                                                    <TextWindow Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                                        Font-Underline="False" />
                                                                    <DownlevelLabel Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                                        Font-Underline="False" />
                                                                    <TabStrip Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                                        Font-Underline="False" />
                                                                </ighedit:WebHtmlEditor>
																<BR>
																<asp:Label id="Label71" runat="server" CssClass="boldFont">Vari�veis:</asp:Label>
																<asp:Label id="Label81" runat="server">NOME_EMPRESA, PERIODO_CURSO, ENDERECO_EMPRESA</asp:Label></TD>
														</TR>
													</TABLE>
												</ContentTemplate>
											</igtab:Tab>
											<igtab:Tab Key="TextoCertificadoIndividual" 
                                                Text="Texto do Certificado Individual" 
                                                Tooltip="Texto do Certificado Individual" Visible="False">
												<ContentTemplate>
													<TABLE class="normalFont" id="Table24" cellSpacing="0" cellPadding="0" width="550" align="center"
														border="0">
														<TR>
															<TD align="center">
																<ighedit:WebHtmlEditor ID="WHETextoIndividual" runat="server" BackgroundImageName=""
                                                                    Focus="False" FontFormattingList="Heading 1=<h1>&Heading 2=<h2>&Heading 3=<h3>&Heading 4=<h4>&Heading 5=<h5>&Normal=<p>"
                                                                    FontNameList="Arial,Verdana,Tahoma,Courier New,Georgia" FontSizeList="1,2,3,4,5,6,7"
                                                                    FontStyleList="Blue Underline=color:blue;text-decoration:underline;&Red Bold=color:red;font-weight:bold;&ALL CAPS=text-transform:uppercase;&all lowercase=text-transform:lowercase;&Reset="
                                                                    Height="245px" SpecialCharacterList="O,S,?,F,G,?,?,T,?,?,?,�,?,f,?,e,?,d,?,?,�,p,s,�,�,�,�,?,?,?,?,?,?,?,?,?,?,?,?,�,�,�,�,�,�,�,�,�,�,�,�,�,�,�,�,�,?,�,�,�,�,@,�,�,&#14;,?,?,?,?,?,?,?,?,?,?,&#18;,�,�,�,�,�,�,�,�,�,�,�,�,�,�,�,�,�,�,�,�,�,�,�"
                                                                    Width="540px">
                                                                    <Toolbar Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                                        Font-Underline="False">
                                                                        <ighedit:ToolbarImage runat="server" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                                                            Font-Strikeout="False" Font-Underline="False" Type="DoubleSeparator" />
                                                                        <ighedit:ToolbarButton runat="server" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                                                            Font-Strikeout="False" Font-Underline="False" Type="Bold" />
                                                                        <ighedit:ToolbarButton runat="server" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                                                            Font-Strikeout="False" Font-Underline="False" Type="Italic" />
                                                                        <ighedit:ToolbarButton runat="server" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                                                            Font-Strikeout="False" Font-Underline="False" Type="Underline" />
                                                                        <ighedit:ToolbarButton runat="server" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                                                            Font-Strikeout="False" Font-Underline="False" Type="Strikethrough" />
                                                                        <ighedit:ToolbarImage runat="server" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                                                            Font-Strikeout="False" Font-Underline="False" Type="Separator" />
                                                                        <ighedit:ToolbarButton runat="server" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                                                            Font-Strikeout="False" Font-Underline="False" Type="Undo" />
                                                                        <ighedit:ToolbarButton runat="server" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                                                            Font-Strikeout="False" Font-Underline="False" Type="Redo" />
                                                                        <ighedit:ToolbarImage runat="server" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                                                            Font-Strikeout="False" Font-Underline="False" Type="Separator" />
                                                                        <ighedit:ToolbarButton runat="server" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                                                            Font-Strikeout="False" Font-Underline="False" Type="JustifyLeft" />
                                                                        <ighedit:ToolbarButton runat="server" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                                                            Font-Strikeout="False" Font-Underline="False" Type="JustifyCenter" />
                                                                        <ighedit:ToolbarButton runat="server" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                                                            Font-Strikeout="False" Font-Underline="False" Type="JustifyRight" />
                                                                        <ighedit:ToolbarButton runat="server" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                                                            Font-Strikeout="False" Font-Underline="False" Type="JustifyFull" />
                                                                        <ighedit:ToolbarImage runat="server" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                                                            Font-Strikeout="False" Font-Underline="False" Type="Separator" />
                                                                        <ighedit:ToolbarDialogButton runat="server" Font-Bold="False" Font-Italic="False"
                                                                            Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Type="FontColor">
                                                                            <Dialog Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                                                Font-Underline="False" />
                                                                        </ighedit:ToolbarDialogButton>
                                                                        <ighedit:ToolbarDialogButton runat="server" Font-Bold="False" Font-Italic="False"
                                                                            Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Type="FindReplace">
                                                                            <Dialog Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                                                Font-Underline="False" InternalDialogType="FindReplace" />
                                                                        </ighedit:ToolbarDialogButton>
                                                                        <ighedit:ToolbarButton runat="server" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                                                            Font-Strikeout="False" Font-Underline="False" Type="WordCount" />
                                                                        <ighedit:ToolbarImage runat="server" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                                                            Font-Strikeout="False" Font-Underline="False" Type="RowSeparator" />
                                                                        <ighedit:ToolbarImage runat="server" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                                                            Font-Strikeout="False" Font-Underline="False" Type="DoubleSeparator" />
                                                                        <ighedit:ToolbarDropDown runat="server" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                                                            Font-Strikeout="False" Font-Underline="False" Type="FontName">
                                                                        </ighedit:ToolbarDropDown>
                                                                        <ighedit:ToolbarDropDown runat="server" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                                                            Font-Strikeout="False" Font-Underline="False" Type="FontSize">
                                                                        </ighedit:ToolbarDropDown>
                                                                    </Toolbar>
                                                                    <RightClickMenu Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                                        Font-Underline="False">
                                                                        <ighedit:HtmlBoxMenuItem runat="server" Act="Cut" Font-Bold="False" Font-Italic="False"
                                                                            Font-Overline="False" Font-Strikeout="False" Font-Underline="False">
                                                                            <Dialog Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                                                Font-Underline="False" />
                                                                        </ighedit:HtmlBoxMenuItem>
                                                                        <ighedit:HtmlBoxMenuItem runat="server" Act="Copy" Font-Bold="False" Font-Italic="False"
                                                                            Font-Overline="False" Font-Strikeout="False" Font-Underline="False">
                                                                            <Dialog Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                                                Font-Underline="False" />
                                                                        </ighedit:HtmlBoxMenuItem>
                                                                        <ighedit:HtmlBoxMenuItem runat="server" Act="Paste" Font-Bold="False" Font-Italic="False"
                                                                            Font-Overline="False" Font-Strikeout="False" Font-Underline="False">
                                                                            <Dialog Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                                                Font-Underline="False" />
                                                                        </ighedit:HtmlBoxMenuItem>
                                                                        <ighedit:HtmlBoxMenuItem runat="server" Act="PasteHtml" Font-Bold="False" Font-Italic="False"
                                                                            Font-Overline="False" Font-Strikeout="False" Font-Underline="False">
                                                                            <Dialog Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                                                Font-Underline="False" />
                                                                        </ighedit:HtmlBoxMenuItem>
                                                                        <ighedit:HtmlBoxMenuItem runat="server" Act="CellProperties" Font-Bold="False" Font-Italic="False"
                                                                            Font-Overline="False" Font-Strikeout="False" Font-Underline="False">
                                                                            <Dialog Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                                                Font-Underline="False" InternalDialogType="CellProperties" />
                                                                        </ighedit:HtmlBoxMenuItem>
                                                                        <ighedit:HtmlBoxMenuItem runat="server" Act="TableProperties" Font-Bold="False" Font-Italic="False"
                                                                            Font-Overline="False" Font-Strikeout="False" Font-Underline="False">
                                                                            <Dialog Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                                                Font-Underline="False" InternalDialogType="ModifyTable" />
                                                                        </ighedit:HtmlBoxMenuItem>
                                                                        <ighedit:HtmlBoxMenuItem runat="server" Act="InsertImage" Font-Bold="False" Font-Italic="False"
                                                                            Font-Overline="False" Font-Strikeout="False" Font-Underline="False">
                                                                            <Dialog Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                                                Font-Underline="False" />
                                                                        </ighedit:HtmlBoxMenuItem>
                                                                    </RightClickMenu>
                                                                    <DropDownStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                                        Font-Underline="False" />
                                                                    <ProgressBar Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                                        Font-Underline="False" />
                                                                    <DownlevelTextArea Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                                        Font-Underline="False" />
                                                                    <TextWindow Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                                        Font-Underline="False" />
                                                                    <DownlevelLabel Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                                        Font-Underline="False" />
                                                                    <TabStrip Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                                        Font-Underline="False" />
                                                                </ighedit:WebHtmlEditor>
																<BR>
																<asp:Label id="Label7" runat="server" CssClass="boldFont">Vari�veis:</asp:Label>
																<asp:Label id="Label8" runat="server">NOME_EMPRESA, PERIODO_CURSO, ENDERECO_EMPRESA, NOME_PARTICIPANTE</asp:Label></TD>
														</TR>
													</TABLE>
												</ContentTemplate>
											</igtab:Tab>
											<igtab:Tab Key="DadosComplementares" Text="Dados Complementares" Tooltip="Dados Complementares">
												<ContentTemplate>
--%>												
                                    <%--												</ContentTemplate>
											</igtab:Tab>
										</Tabs>
									</igtab:ultrawebtab><br>
--%>									
			

 
         <eo:MsgBox ID="MsgBox1" runat="server" BackColor="#47729F" ControlSkinID="None" 
        HeaderHtml="Dialog Title" Height="100px" Width="168px">
        <HeaderStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #ffbf00 3px solid; padding-left: 4px; font-weight: bold; font-size: 11px; padding-bottom: 2px; color: white; padding-top: 2px; border-bottom: #22456a 1px solid; font-family: verdana" />
        <ContentStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #7d97b6 1px solid; padding-left: 4px; border-left-width: 1px; font-size: 11px; border-left-color: #728eb8; padding-bottom: 4px; color: white; padding-top: 4px; border-bottom: #22456a 1px solid; font-family: verdana" />
        <FooterStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #7d97b6 1px solid; padding-left: 4px; border-left-width: 1px; font-size: 11px; border-left-color: #728eb8; padding-bottom: 4px; color: white; padding-top: 4px; border-bottom: #22456a 1px solid; font-family: verdana" />
        </eo:MsgBox>

		</form>
	</body>
</HTML>