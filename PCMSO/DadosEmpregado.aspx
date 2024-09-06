<%@ Page Language="C#"   AutoEventWireup="True"  MasterPageFile="~/Site.Master" CodeBehind="DadosEmpregado.aspx.cs"  Inherits="Ilitera.Net.DadosEmpregado" Title="Ilitera.Net" %>

<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <title>Exames Clínicos</title>
    <link href="css/exames.css" rel="stylesheet" type="text/css" />
    <link href="css/tabelas.css" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">

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

    <div class="container-fluid d-flex ms-5 ps-4">
        <div class="row gx-3 gy-2 w-100 mb-3">

            <%-- PRIMEIRO CONTAINER --%>

            <div class="col-11 subtituloBG" >
                <h2 class="subtitulo">Dados do Empregado</h2>
            </div>

            <div class="col-11">
                <div class="row">
                     <%-- LINHA UM --%>
                        <div class="col-md-4 gx-2 gy-2">
                            <fieldset>
                                <asp:Label ID="lblNome" runat="server" Text="Nome" class="tituloLabel form-label col-form-label col-form-label-sm" Style="text-align: left;"></asp:Label>
                                <asp:Literal runat="server"><br /></asp:Literal>
                                <asp:Label ID="lblValorNome" runat="server" class="texto form-control form-control-sm" Style="text-align: left;"></asp:Label>
                            </fieldset>
                       </div>

                      <div class="col-md-3 gx-2 gy-2">
                        <fieldset>
                            <asp:Label ID="lblTipoBene" runat="server" Text="Tipo de Beneficiário" class="tituloLabel form-label col-form-label col-form-label-sm" Style="text-align: left;"></asp:Label>
                            <asp:Literal runat="server"><br /></asp:Literal>
                            <asp:Label ID="lblValorBene" runat="server" class="texto form-control form-control-sm" Style="text-align: left;"></asp:Label>
                        </fieldset>
                      </div>

                      <div class="col-md-2 gx-2 gy-2">
                        <fieldset>
                            <asp:Label ID="lblDataNascimento" runat="server" Text="Nascimento" CssClass="tituloLabel form-label col-form-label col-form-label-sm"></asp:Label>
                            <asp:Literal runat="server"><br /></asp:Literal>
                            <asp:Label ID="lblValorNasc" runat="server" CssClass="texto form-control form-control-sm" type="date"></asp:Label>
                        </fieldset>
                      </div>

                      <div class="col-md-1 gx-2 gy-2">
                        <fieldset>
                            <asp:Label ID="lblIdade" runat="server" Text="Idade" class="tituloLabel form-label col-form-label col-form-label-sm" Style="text-align: left;"></asp:Label>
                            <asp:Literal runat="server"><br /></asp:Literal>
                            <asp:Label ID="lblValorIdade" runat="server" class="texto form-control form-control-sm" Style="text-align: left;"></asp:Label>
                        </fieldset>
                      </div>

                      <div class="col-md-2 gx-2 gy-2">
                        <fieldset>
                            <asp:Label ID="lblSexo" runat="server" Text="Sexo" class="tituloLabel form-label col-form-label col-form-label-sm" Style="text-align: left;"></asp:Label>
                            <asp:Literal runat="server"><br /></asp:Literal>
                            <asp:Label ID="lblValorSexo" runat="server" class="texto form-control form-control-sm" Style="text-align: left;"></asp:Label>
                        </fieldset>
                      </div>

                        <%-- LINHA DOIS --%>

                      <div class="col-md-2 gx-2 gy-2">
                        <fieldset>
                            <asp:Label ID="lblAdmissao" runat="server" Text="Admissão" CssClass="tituloLabel form-label col-form-label col-form-label-sm"></asp:Label>
                            <asp:Literal runat="server"><br /></asp:Literal>
                            <asp:Label ID="lblValorAdmissao" runat="server" CssClass="texto form-control form-control-sm" type="date"></asp:Label>
                        </fieldset>
                      </div>

                      <div class="col-md-2 gx-2 gy-2">
                          <fieldset>
                            <asp:Label ID="lblDemissao" runat="server" Text="Demissão" CssClass="tituloLabel form-label col-form-label col-form-label-sm"></asp:Label>
                            <asp:Literal runat="server"><br /></asp:Literal>
                            <asp:Label ID="lblValorDemissao" runat="server" CssClass="texto form-control form-control-sm" type="date"></asp:Label>
                        </fieldset>
                      </div>

                      <div class="col-2 gx-2 gy-2">
                        <div class="form-check">
                          <fieldset>
                            <asp:Label ID="lblRegistro" runat="server" Text="RE" class="tituloLabel form-label col-form-label col-form-label-sm" Style="text-align: left;"></asp:Label>
                            <asp:Literal runat="server"><br /></asp:Literal>
                            <asp:Label ID="lblValorRegistro" runat="server" class="texto form-control form-control-sm" Style="text-align: left;"></asp:Label>
                        </fieldset>
                        </div>
                      </div>

                     <div class="col-2 gx-2 gy-2">
                        <div class="form-check">
                          <fieldset>
                            <asp:Label ID="lblJornada" runat="server" Text="Jornada" class="tituloLabel form-label col-form-label col-form-label-sm" Style="text-align: left;"></asp:Label>
                            <asp:Literal runat="server"><br /></asp:Literal>
                            <asp:Label ID="lblValorJornada" runat="server" class="texto form-control form-control-sm" Style="text-align: left;"></asp:Label>
                        </fieldset>
                        </div>
                      </div>


                      <div class="col-4 gx-2 gy-2">
                          <div class="form-check">
                              <fieldset>
                                  <asp:Label ID="lblRegRev" runat="server" Text="Regime de Revezamento" class="tituloLabel form-label col-form-label col-form-label-sm" Style="text-align: left;"></asp:Label>
                                  <asp:Literal runat="server"><br /></asp:Literal>
                                  <asp:Label ID="lblValorRegRev" runat="server" class="texto form-control form-control-sm" Style="text-align: left;"></asp:Label>
                                </fieldset>
                          </div>
                      </div>

                     <%-- LINHA TRÊS --%>

                      <div class="col-md-2 gx-2 gy-2">
                        <fieldset>
                            <asp:Label ID="lblGHE" runat="server" Text="Tempo de Empresa" CssClass="tituloLabel form-label col-form-label col-form-label-sm"></asp:Label>
                            <asp:Literal runat="server"><br /></asp:Literal>
                            <asp:Label ID="lblValorTempoEmpresa" runat="server" CssClass="texto form-control form-control-sm" ></asp:Label>
                        </fieldset>
                      </div>

                      <div class="col-md-2 gx-2 gy-2">
                          <fieldset>
                            <asp:Label ID="lblDataIni" runat="server" Text="Início da Função" CssClass="tituloLabel form-label col-form-label col-form-label-sm"></asp:Label>
                            <asp:Literal runat="server"><br /></asp:Literal>
                            <asp:Label ID="lblValorDataIni" runat="server" CssClass="texto form-control form-control-sm" text="27/03/2015" type="date"></asp:Label>
                        </fieldset>
                      </div>

                      <div class="col-4 gx-2 gy-2">
                        <div class="form-check">
                          <fieldset>
                            <asp:Label ID="lblSetor" runat="server" Text="Setor" class="tituloLabel form-label col-form-label col-form-label-sm" Style="text-align: left;"></asp:Label>
                            <asp:Literal runat="server"><br /></asp:Literal>
                            <asp:Label ID="lblValorSetor" runat="server" class="texto form-control form-control-sm" Style="text-align: left;"></asp:Label>
                        </fieldset>
                        </div>
                      </div>

                      <div class="col-4 gx-2 gy-2">
                          <div class="form-check">
                              <fieldset>
                                  <asp:Label ID="lblFuncao" runat="server" Text="Função" class="tituloLabel form-label col-form-label col-form-label-sm" Style="text-align: left;"></asp:Label>
                                  <asp:Literal runat="server"><br /></asp:Literal>
                                  <asp:Label ID="lblValorFuncao" runat="server" class="texto form-control form-control-sm" Style="text-align: left;"></asp:Label>
                                </fieldset>
                          </div>
                      </div>        
                    </div>
                    </div>

                    <div class="col-11 subtituloBG pt-2">
                        <asp:label id="lblExCli" runat="server" class="subtitulo">Exames Clínicos</asp:label>
                    </div>
                </div>
            </div>
           
        <%-- TABELA  --%>

            <div class="col-12 gx-2 gy-2 ms-5 ps-4">
<%--                <eo:Grid runat="server" ID="grd_Clinicos" FixedColumnCount="1" GridLines="Both" ColumnHeaderDividerOffset="6"
                    KeyField="Id" PageSize="500" CssClass="grid" ColumnHeaderHeight="28" ItemHeight="28" Height="400" Width="800"
        OnItemCommand="grd_Clinicos_ItemCommand"  
        ClientSideOnItemCommand="OnItemCommand"  > --%>

                 <eo:Grid ID="grd_Clinicos" runat="server" ColumnHeaderAscImage="00050403" 
                ColumnHeaderDescImage="00050404"
                FixedColumnCount="1" GridLineColor="240, 240, 240" 
                GridLines="Both" Height="255px" Width="800px" ColumnHeaderDividerOffset="6" 
                ColumnHeaderHeight="30" ItemHeight="30" KeyField="Id"  
                OnItemCommand="grd_Clinicos_ItemCommand"  
                ClientSideOnItemCommand="OnItemCommand"  >
                    <ItemStyles>
                            <eo:GridItemStyleSet>
                                <ItemStyle CssText="background-color: #FAFAFA;" />
                                <ItemHoverStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: normal;  text-align: left; height: 30px !important; background: rgba(28, 148, 137, 0.32); color: #FFFFFF; padding: 0.3rem 0.3rem 0.3rem 0.5rem; " />
                                <AlternatingItemStyle CssText="background-color: #F2F2F2;" />
                                <AlternatingItemHoverStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: normal;  text-align: left; height: 30px !important; background: rgba(28, 148, 137, 0.32); color: #FFFFFF; padding: 0.3rem 0.3rem 0.3rem 0.5rem;" />
                                <SelectedStyle CssText="border-bottom-style:solid;border-left-style:solid;border-right-style:solid;border-top-style:solid;" />
                                <CellStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500; color: #7D7B7B !important; text-align: left; height: 35px !important; padding: 0.3rem 0.3rem 0.3rem 0.5rem; height: 35px !important;" />
                            </eo:GridItemStyleSet>
                        </ItemStyles>
                    <ColumnHeaderStyle CssClass="tabelaC colunas" />
                    <Columns>
                        <eo:StaticColumn AllowSort="True" DataField="Data" HeaderText="Data do Exame" Name="DataHora" ReadOnly="True" SortOrder="Ascending" Text="" Width="200" DataFormat="{0:dd/MM/yyyy}" DataType="DateTime">
                            <CellStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500; color: #7D7B7B !important; text-align: left; height: 35px !important; padding: 0.3rem 0.3rem 0.3rem 0.5rem; height: 35px !important;" />
                        </eo:StaticColumn>
                        <eo:StaticColumn AllowSort="True" DataField="Tipo" HeaderText="Tipo" Name="Descricao" ReadOnly="True" Width="450">
                            <CellStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500; color: #7D7B7B !important; text-align: left; height: 35px !important; padding: 0.3rem 0.3rem 0.3rem 0.5rem; height: 35px !important;" />
                        </eo:StaticColumn>
                        <eo:ButtonColumn ButtonText="Editar" Name="Selecionar" Width="70">
                            <CellStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500; color: #7D7B7B !important; text-align: left; height: 35px !important; padding: 0.3rem 0.3rem 0.3rem 0.5rem; height: 35px !important;" />
                        </eo:ButtonColumn>
                        <eo:ButtonColumn ButtonText="Visualizar" Name="Selecionar" Width="70">
                            <CellStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500; color: #7D7B7B !important; text-align: left; height: 35px !important; padding: 0.3rem 0.3rem 0.3rem 0.5rem; height: 35px !important;" />
                        </eo:ButtonColumn>
                    </Columns>
                    <columntemplates>
                        <eo:TextBoxColumn>
                            <TextBoxStyle CssText="BORDER-RIGHT: #7f9db9 1px solid; PADDING-RIGHT: 2px; BORDER-TOP: #7f9db9 1px solid; PADDING-LEFT: 2px; FONT-SIZE: 8.75pt; PADDING-BOTTOM: 1px; MARGIN: 0px; BORDER-LEFT: #7f9db9 1px solid; PADDING-TOP: 2px; BORDER-BOTTOM: #7f9db9 1px solid; FONT-FAMILY: Tahoma" />
                        </eo:TextBoxColumn>
                        <eo:DateTimeColumn>
                            <datepicker runat="server" controlskinid="None" daycellheight="16" daycellwidth="19" dayheaderformat="FirstLetter" disableddates="" othermonthdayvisible="True" selecteddates="" titleleftarrowimageurl="DefaultSubMenuIconRTL" titlerightarrowimageurl="DefaultSubMenuIcon">
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
                            </datepicker>
                        </eo:DateTimeColumn>
                        <eo:MaskedEditColumn>
                            <MaskedEdit runat="server" controlskinid="None" textboxstyle-csstext="BORDER-RIGHT: #7f9db9 1px solid; PADDING-RIGHT: 2px; BORDER-TOP: #7f9db9 1px solid; PADDING-LEFT: 2px; PADDING-BOTTOM: 1px; MARGIN: 0px; BORDER-LEFT: #7f9db9 1px solid; PADDING-TOP: 2px; BORDER-BOTTOM: #7f9db9 1px solid; font-family:Courier New;font-size:8pt;">
                            </MaskedEdit>
                        </eo:MaskedEditColumn>
                    </columntemplates>
                    <FooterStyle CssText="padding-bottom:4px;padding-left:4px;padding-right:4px;padding-top:4px;" />
                </eo:Grid>

                <div class="col-11 text-center">
                    <asp:hyperlink id="hlkNovo" runat="server" CssClass="btn fw-normal" style="color: #FFF !important;">Novo Exame</asp:hyperlink>
                </div>

                <div class="col-11 text-start pt-2">
                    <asp:Button ID="cmd_Voltar" onclick="cmd_Voltar_Click"  runat="server" CssClass="btn2" Text="Voltar"  />
                </div>

            <asp:label id="lblTotRegistros" runat="server"></asp:label>
            <asp:image id="ImagemEmpregado" runat="server" Height="102px" Width="81px" Visible="False"></asp:image>
            <asp:button id="btnFichaCompleta" onclick="btnFichaCompleta_Click"  runat="server" CssClass="buttonFoto" Text="Cadastro Completo" Width="132px" Visible="False"  ></asp:button></TD>
            <caption>
                <input id="txtIdUsuario" type="text" visible="False"  style="visibility:hidden"  />
                <input id="txtIdEmpregado" type="text" visible="False" style="visibility:hidden" />
                <input id="txtIdEmpresa" type="text" visible="False" style="visibility:hidden"/>
            </caption>
    </div>


        <eo:MsgBox ID="MsgBox1" runat="server" BackColor="#47729F" ControlSkinID="None" 
            HeaderHtml="Dialog Title" Height="100px" Width="168px">
            <HeaderStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #ffbf00 3px solid; padding-left: 4px; font-weight: bold; font-size: 11px; padding-bottom: 2px; color: white; padding-top: 2px; border-bottom: #22456a 1px solid; font-family: verdana" />
            <ContentStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #7d97b6 1px solid; padding-left: 4px; border-left-width: 1px; font-size: 11px; border-left-color: #728eb8; padding-bottom: 4px; color: white; padding-top: 4px; border-bottom: #22456a 1px solid; font-family: verdana" />
            <FooterStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #7d97b6 1px solid; padding-left: 4px; border-left-width: 1px; font-size: 11px; border-left-color: #728eb8; padding-bottom: 4px; color: white; padding-top: 4px; border-bottom: #22456a 1px solid; font-family: verdana" />
        </eo:MsgBox>
</asp:Content>