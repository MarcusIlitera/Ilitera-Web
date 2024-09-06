<%@ Page Language="C#"   AutoEventWireup="True"  MasterPageFile="~/Site.Master" CodeBehind="DadosEmpregado_Vacina.aspx.cs"  Inherits="Ilitera.Net.DadosEmpregado_Vacina" Title="Ilitera.Net" %>

<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>


<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
	<link href="css/forms.css" rel="stylesheet" type="text/css" />
    <link href="css/tabelas.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
    .defaultFont
    {
        width: 627px;
    }
        #Table4
        {
            width: 594px;
        }
        #Table15
        {
            width: 214px;
        }
        #Table20
        {
            width: 184px;
        }
        #Table24
        {
            width: 333px;
        }
        #Table27
        {
            width: 583px;
        }
    </style>
</asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
	<div class="container-fluid d-flex ms-5 ps-4">
        <div class="row gx-3 gy-2 w-100">
    <script language="javascript">

	    function Reload() {
	        var f = document.getElementById('SubDados');
	        //f.src = f.src;
	        f.contentWindow.location.reload(true);
	    }


         function OnItemCommand(grid, itemIndex, colIndex, commandName) {
        
        //grid.raiseItemCommandEvent(itemIndex, commandName);
        grid.raiseItemCommandEvent(itemIndex, colIndex.toString());
       }



    </script>

  

<%--<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head id="Head1" runat="server">
		<title>Ilitera.NET</title>
		<script language="JavaScript" src="scripts/validador.js"></script>
		<link href="scripts/style.css" type="text/css" rel="stylesheet">
	</head>
	<body bottommargin="0" leftmargin="0" topmargin="0" rightmargin="0">
		<form name="DadosEmpregado" method="post" runat="server">
--%>		
	
            <%-- PRIMEIRO CONTAINER --%>

            <div class="col-11 subtituloBG">
                <h2 class="subtitulo">Dados do Empregado</h2>
            </div>

            <div class="col-11 mb-3">
                <div class="row">
                    <%-- LINHA 1--%>
                    <div class="col-md-4 gx-2 gy-2">
                        <fieldset>
                            <asp:label runat="server" ID="lblNome" CssClass="tituloLabel form-label" Text="Nome"></asp:label>
                            <asp:Literal runat="server"><br /></asp:Literal>
                            <asp:label runat="server" ID="lblValorNome" CssClass="texto form-control form-control-sm"></asp:label>
                        </fieldset>
                    </div>

                    <div class="col-md-3 gx-2 gy-2">
                        <fieldset>
                             <asp:label runat="server" ID="lblTipoBene" CssClass="tituloLabel form-label" Text="Tipo de Beneficiário"></asp:label>
                            <asp:Literal runat="server"><br /></asp:Literal>
                            <asp:label runat="server" ID="lblValorBene" CssClass="texto form-control form-control-sm"></asp:label>
                        </fieldset>
                    </div>

                    <div class="col-md-2 gx-2 gy-2">
                        <fieldset>
                             <asp:label runat="server" ID="lblDataNascimento" CssClass="tituloLabel form-label" Text="Nascimento"></asp:label>
                            <asp:Literal runat="server"><br /></asp:Literal>
                            <asp:label runat="server" ID="lblValorNasc" CssClass="texto form-control form-control-sm"></asp:label>
                        </fieldset>
                    </div>

                    <div class="col-md-1 gx-2 gy-2">
                        <fieldset>
                             <asp:label runat="server" ID="lblIdade" CssClass="tituloLabel form-label" Text="Idade"></asp:label>
                            <asp:Literal runat="server"><br /></asp:Literal>
                            <asp:label runat="server" ID="lblValorIdade" CssClass="texto form-control form-control-sm"></asp:label>
                        </fieldset>
                    </div>

                    <div class="col-md-2 gx-2 gy-2">
                        <fieldset>
                             <asp:label runat="server" ID="lblSexo" CssClass="tituloLabel form-label" Text="Sexo"></asp:label>
                            <asp:Literal runat="server"><br /></asp:Literal>
                            <asp:label runat="server" ID="lblValorSexo" CssClass="texto form-control form-control-sm"></asp:label>
                        </fieldset>
                    </div>

                    <%-- LINHA 2 --%>

                    <div class="col-md-2 gx-2 gy-2">
                        <fieldset>
                             <asp:label runat="server" ID="lblRegistro" CssClass="tituloLabel form-label" Text="RE"></asp:label>
                            <asp:Literal runat="server"><br /></asp:Literal>
                            <asp:label runat="server" ID="lblValorRegistro" CssClass="texto form-control form-control-sm"></asp:label>
                        </fieldset>
                    </div>

                    <div class="col-md-3 gx-2 gy-2">
                        <fieldset>
                             <asp:label runat="server" ID="lblRegRev" CssClass="tituloLabel form-label" Text="Regime Revezamento"></asp:label>
                            <asp:Literal runat="server"><br /></asp:Literal>
                            <asp:label runat="server" ID="lblValorRegRev" CssClass="texto form-control form-control-sm"></asp:label>
                        </fieldset>
                    </div>

                    <div class="col-md-3 gx-2 gy-2">
                        <fieldset>
                             <asp:label runat="server" ID="lblJornada" CssClass="tituloLabel form-label" Text="Jornada"></asp:label>
                            <asp:Literal runat="server"><br /></asp:Literal>
                            <asp:label runat="server" ID="lblValorJornada" CssClass="texto form-control form-control-sm"></asp:label>
                        </fieldset>
                    </div>

                    <div class="col-md-2 gx-2 gy-2">
                        <fieldset>
                             <asp:label runat="server" ID="lblAdmissao" CssClass="tituloLabel form-label" Text="Admissão"></asp:label>
                            <asp:Literal runat="server"><br /></asp:Literal>
                            <asp:label runat="server" ID="lblValorAdmissao" CssClass="texto form-control form-control-sm"></asp:label>
                        </fieldset>
                    </div>

                    <div class="col-md-2 gx-2 gy-2">
                        <fieldset>
                             <asp:label runat="server" ID="lblDemissao" CssClass="tituloLabel form-label" Text="Demissão"></asp:label>
                            <asp:Literal runat="server"><br /></asp:Literal>
                            <asp:label runat="server" ID="lblValorDemissao" CssClass="texto form-control form-control-sm"></asp:label>
                        </fieldset>
                    </div>

                    <%-- LINHA 3 --%>

                    <div class="col-md-2 gx-2 gy-2">
                        <fieldset>
                             <asp:label runat="server" ID="lblDataIni" CssClass="tituloLabel form-label" Text="Início Função"></asp:label>
                            <asp:Literal runat="server"><br /></asp:Literal>
                            <asp:label runat="server" ID="lblValorDataIni" CssClass="texto form-control form-control-sm"></asp:label>
                        </fieldset>
                    </div>

                    <div class="col-md-2 gx-2 gy-2">
                        <fieldset>
                             <asp:label runat="server" ID="lblGHE" CssClass="tituloLabel form-label" Text="Tempo de Empresa"></asp:label>
                            <asp:Literal runat="server"><br /></asp:Literal>
                            <asp:label runat="server" ID="lblValorTempoEmpresa" CssClass="texto form-control form-control-sm"></asp:label>
                        </fieldset>
                    </div>

                    <div class="col-md-4 gx-2 gy-2">
                        <fieldset>
                             <asp:label runat="server" ID="lblSetor" CssClass="tituloLabel form-label" Text="Setor"></asp:label>
                            <asp:Literal runat="server"><br /></asp:Literal>
                            <asp:label runat="server" ID="lblValorSetor" CssClass="texto form-control form-control-sm"></asp:label>
                        </fieldset>
                    </div>

                    <div class="col-md-4 gx-2 gy-2">
                        <fieldset>
                             <asp:label runat="server" ID="lblFuncao" CssClass="tituloLabel form-label" Text="Função"></asp:label>
                            <asp:Literal runat="server"><br /></asp:Literal>
                            <asp:label runat="server" ID="lblValorFuncao" CssClass="texto form-control form-control-sm"></asp:label>
                        </fieldset>
                    </div>

                    <div class="col-md-5 gx-2 gy-3">
                        <fieldset>
                            <asp:Label ID="Label1" runat="server" CssClass="tituloLabel" Text="e-mail(s) para alerta de Vacinação deste cliente"></asp:Label>
                            <asp:TextBox ID="txt_Email" runat="server" CssClass="texto form-control form-control-sm" MaxLength="200"></asp:TextBox>
                            <asp:Label ID="Label2" runat="server" CssClass="texto" Text="( separe mais de um e-mail por &quot;;&quot; ponto e vírgula )"></asp:Label>
                        </fieldset>
                    </div>

                    <div class="col-md-3 gx-2 gy-3">
                        <fieldset>
                            <asp:Label ID="Label3" runat="server" CssClass="tituloLabel" Text="Dias de antecedência para alerta"></asp:Label>
                            <div class="row">
                                <div class="col-md-4">
                                    <asp:TextBox ID="txt_Alerta_Dias" runat="server" CssClass="texto form-control form-control-sm" MaxLength="2"></asp:TextBox>
                                </div>
                            </div>
                        </fieldset>
                    </div>
                    <div class="col-md-4"></div>

                    <div class="col-md-2 gx-2 gy-2">
                        <asp:Button ID="cmd_Salvar_Alerta" runat="server" CssClass="btn mt-2" Text="Salvar" />
                    </div>

                </div>
            </div>




<table class="defaultFont" cellSpacing="0" cellPadding="0" width="1050"  border="0">
				<tr class="row">
					<td class="col-12 subtituloBG mb-3" style="padding-top: 10px;">
						<asp:label id="lblExCli" runat="server" SkinID="TitleFont" class="subtitulo">Exames Clínicos</asp:label>
					</td>
				</tr>

				<tr>
					<td vAlign="top" align="right">
                    
                         
                            
           <eo:Grid ID="grd_Clinicos" runat="server" ColumnHeaderAscImage="00050403" 
                ColumnHeaderDescImage="00050404" FixedColumnCount="1" GridLines="Both" 
			   Height="200px" Width="1050px" ColumnHeaderDividerOffset="6" 
                ColumnHeaderHeight="30" ItemHeight="30" KeyField="IdVacina" CssClass="grid" OnItemCommand="grd_Clinicos_ItemCommand" >
            <ItemStyles>
                <eo:GridItemStyleSet>
                    <ItemStyle CssText="background-color: #FAFAFA" />
                    <ItemHoverStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500;  text-align: left; height: 30px !important; background: rgba(28, 148, 137, 0.32); color: #FFFFFF; padding: 0.3rem 0.3rem 0.3rem 0.5rem; " />
                            <AlternatingItemStyle CssText="background-color: #F2F2F2;" />
                            <AlternatingItemHoverStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500;  text-align: left; height: 30px !important; background: rgba(28, 148, 137, 0.32); color: #FFFFFF; padding: 0.3rem 0.3rem 0.3rem 0.5rem;" />
                            <SelectedStyle CssText="border-bottom-style:solid;border-left-style:solid;border-right-style:solid;border-top-style:solid;" />
                            <CellStyle CssText="padding: 0.3rem 0.3rem 0.3rem 0.5rem; height: 30px !important;" />
                </eo:GridItemStyleSet>
            </ItemStyles>
            <ColumnHeaderTextStyle CssText="" />
            <ColumnHeaderStyle CssClass="tabelaC colunas"/>
            <Columns>
                <eo:StaticColumn HeaderText="Data da Vacina" AllowSort="True" 
                    DataField="Data_Vacina" Name="Data_Vacina" ReadOnly="True" 
                    SortOrder="Ascending" Text="" Width="130">
                    <CellStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500; color: #B0ABAB !important; text-align: left; height: 30px !important;" />
                </eo:StaticColumn>
                <eo:StaticColumn HeaderText="Dose" AllowSort="True" 
                    DataField="Dose" Name="Dose" ReadOnly="True" 
                    Width="160">
                    <CellStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500; color: #B0ABAB !important; text-align: left; height: 30px !important;" />
                </eo:StaticColumn>
                <eo:StaticColumn HeaderText="Vacina" AllowSort="True" 
                    DataField="VacinaTipo" Name="VacinaTipo" ReadOnly="True" 
                    Width="540">
                    <CellStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500; color: #B0ABAB !important; text-align: left; height: 30px !important;" />
                </eo:StaticColumn>

                <eo:ButtonColumn ButtonText="Editar" 
                    Name="Selecionar" Width="105">
                    <CellStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500; color: #B0ABAB !important; text-align: left; height: 30px !important;" />
                </eo:ButtonColumn>

                <eo:ButtonColumn ButtonText="Visualizar" 
                    Name="Selecionar" Width="105">
                    <CellStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500; color: #B0ABAB !important; text-align: left; height: 30px !important;" />
                </eo:ButtonColumn>
  
            </Columns>
            <ColumnTemplates>
                <eo:TextBoxColumn>
                    <TextBoxStyle CssText="BORDER-RIGHT: #7f9db9 1px solid; PADDING-RIGHT: 2px; BORDER-TOP: #7f9db9 1px solid; PADDING-LEFT: 2px; FONT-SIZE: 8.75pt; PADDING-BOTTOM: 1px; MARGIN: 0px; BORDER-LEFT: #7f9db9 1px solid; PADDING-TOP: 2px; BORDER-BOTTOM: #7f9db9 1px solid; FONT-FAMILY: Tahoma" />
                </eo:TextBoxColumn>
                <eo:DateTimeColumn>
                    <DatePicker ControlSkinID="None" DayCellHeight="16" DayCellWidth="19" 
                        DayHeaderFormat="FirstLetter" DisabledDates="" OtherMonthDayVisible="True" 
                        SelectedDates="" TitleLeftArrowImageUrl="DefaultSubMenuIconRTL" 
                        TitleRightArrowImageUrl="DefaultSubMenuIcon">
                        <PickerStyle CssText="border-bottom-color:#7f9db9;border-bottom-style:solid;border-bottom-width:1px;border-left-color:#7f9db9;border-left-style:solid;border-left-width:1px;border-right-color:#7f9db9;border-right-style:solid;border-right-width:1px;border-top-color:#7f9db9;border-top-style:solid;border-top-width:1px;font-family:Courier New;font-size:8pt;margin-bottom:0px;margin-left:0px;margin-right:0px;margin-top:0px;padding-bottom:1px;padding-left:2px;padding-right:2px;padding-top:2px;" />
                        <CalendarStyle CssText="background-color: white; border-right: #7f9db9 1px solid; padding-right: 4px; border-top: #7f9db9 1px solid; padding-left: 4px; font-size: 9px; padding-bottom: 4px; border-left: #7f9db9 1px solid; padding-top: 4px; border-bottom: #7f9db9 1px solid; font-family: tahoma" />
                        <TitleStyle CssText="background-color:#9ebef5;font-family:Tahoma;font-size:12px;padding-bottom:2px;padding-left:6px;padding-right:6px;padding-top:2px;" />
                        <TitleArrowStyle CssText="cursor:hand" />
                        <MonthStyle CssText="font-family: tahoma; font-size: 12px; margin-left: 14px; cursor: hand; margin-right: 14px" />
                        <DayHeaderStyle CssText="font-family: tahoma; font-size: 12px; border-bottom: #aca899 1px solid" />
                        <DayStyle CssText="font-family: tahoma; font-size: 12px; border-right: white 1px solid; border-top: white 1px solid; border-left: white 1px solid; border-bottom: white 1px solid" />
                        <DayHoverStyle CssText="font-family: tahoma; font-size: 12px; border-right: #fbe694 1px solid; border-top: #fbe694 1px solid; border-left: #fbe694 1px solid; border-bottom: #fbe694 1px solid" />
                        <TodayStyle CssText="font-family: tahoma; font-size: 12px; border-right: #bb5503 1px solid; border-top: #bb5503 1px solid; border-left: #bb5503 1px solid; border-bottom: #bb5503 1px solid" />
                        <SelectedDayStyle CssText="font-family: tahoma; font-size: 12px; background-color: #fbe694; border-right: white 1px solid; border-top: white 1px solid; border-left: white 1px solid; border-bottom: white 1px solid" />
                        <DisabledDayStyle CssText="font-family: tahoma; font-size: 12px; color: gray; border-right: white 1px solid; border-top: white 1px solid; border-left: white 1px solid; border-bottom: white 1px solid" />
                        <OtherMonthDayStyle CssText="font-family: tahoma; font-size: 12px; color: gray; border-right: white 1px solid; border-top: white 1px solid; border-left: white 1px solid; border-bottom: white 1px solid" />
                    </DatePicker>
                </eo:DateTimeColumn>
                <eo:MaskedEditColumn>
                    <MaskedEdit ControlSkinID="None" 
                        TextBoxStyle-CssText="BORDER-RIGHT: #7f9db9 1px solid; PADDING-RIGHT: 2px; BORDER-TOP: #7f9db9 1px solid; PADDING-LEFT: 2px; PADDING-BOTTOM: 1px; MARGIN: 0px; BORDER-LEFT: #7f9db9 1px solid; PADDING-TOP: 2px; BORDER-BOTTOM: #7f9db9 1px solid; font-family:Courier New;font-size:8pt;">
                    </MaskedEdit>
                </eo:MaskedEditColumn>
            </ColumnTemplates>
            <FooterStyle CssText="padding-bottom:4px;padding-left:4px;padding-right:4px;padding-top:4px;" />
        </eo:Grid>

                    
                    </td>
				</tr>
				<tr>
					<td class="normalFont" style="HEIGHT: 15px" ><P>
							<table class="defaultFont" id="Table28" cellSpacing="0" cellPadding="0" width="590" align="right"
								border="0">
								<tr>
									<td align="right"><asp:label id="lblTotRegistros" runat="server"></asp:label></td>
								</tr>
							</table>
							<BR>
							<asp:hyperlink id="hlkNovo" runat="server" SkinID="BoldLink" CssClass="texto">Nova Vacinação</asp:hyperlink></P>
					</td>
				</tr>
			</table>
			<%--<iframe id="SubDados" name="SubDados" align="middle" marginWidth="0" marginHeight="0" frameBorder="0"
				width="600" scrolling="no" src="../DadosEmpresa/SubDadosNull.aspx" >
			</iframe>--%>
<%--		</form>
	</body>
</HTML>
--%>

                  <div class="col-12">
                  <asp:Button ID="cmd_Voltar" runat="server" CssClass="btn" Text="Voltar" onclick="cmd_Voltar_Click" />
                  </div>

             <TD class="normalFont" align="center" width="0" height="0">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                                    <asp:image id="ImagemEmpregado" runat="server" Height="102px" Width="81px" 
                                        Visible="False"></asp:image><BR>
									<asp:button id="btnFichaCompleta" runat="server" CssClass="btn" Text="Cadastro Completo" Visible="False" onclick="btnFichaCompleta_Click"></asp:button></TD>

            <caption>
                <input id="txtIdUsuario" type="text" visible="False"  style="visibility:hidden"  />
                <input id="txtIdEmpregado" type="text" visible="False" style="visibility:hidden" />
                <input id="txtIdEmpresa" type="text" visible="False" style="visibility:hidden"/>
            </caption>

        
         <eo:MsgBox ID="MsgBox1" runat="server" BackColor="#47729F" ControlSkinID="None" 
        HeaderHtml="Dialog Title" Height="100px" Width="168px">
        <HeaderStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #ffbf00 3px solid; padding-left: 4px; font-weight: bold; font-size: 11px; padding-bottom: 2px; color: white; padding-top: 2px; border-bottom: #22456a 1px solid; font-family: verdana" />
        <ContentStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #7d97b6 1px solid; padding-left: 4px; border-left-width: 1px; font-size: 11px; border-left-color: #728eb8; padding-bottom: 4px; color: white; padding-top: 4px; border-bottom: #22456a 1px solid; font-family: verdana" />
        <FooterStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #7d97b6 1px solid; padding-left: 4px; border-left-width: 1px; font-size: 11px; border-left-color: #728eb8; padding-bottom: 4px; color: white; padding-top: 4px; border-bottom: #22456a 1px solid; font-family: verdana" />
        </eo:MsgBox>
			</div>
		</div>
</asp:Content>
