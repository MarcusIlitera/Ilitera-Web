<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="True"
    Title="Ilitera.Net"  EnableEventValidation="false"  CodeBehind="Cipa_New.aspx.cs" Inherits="Ilitera.Net.Cipa_New" ValidateRequest="false"  %>

<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <title>Guia de Encaminhamento</title>
    <link href="css/forms.css" rel="stylesheet" type="text/css" />
    <link href="css/tabelas.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.11.3/font/bootstrap-icons.min.css" />
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" />
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js"></script>
    <style type="text/css">
         @font-face {
            font-family: 'UniviaPro';
            src: url('css/css/UniviaPro-Regular.otf') format('opentype');
            font-weight: normal;
            font-style: normal;
        }

        /* Agora, você pode usar a fonte em qualquer lugar no CSS */
        body {
            font-family: 'UniviaPro', sans-serif;
        }
        img#ctl00_MainContent_Grid1_c0_sort, img#ctl00_MainContent_Grid1_c1_sort, img#ctl00_MainContent_Grid1_c2_sort, img#ctl00_MainContent_grd_Presidente_Vice_c0_sort,
        img#ctl00_MainContent_grd_Presidente_Vice_c1_sort, img#ctl00_MainContent_grd_Membros_c0_sort, img#ctl00_MainContent_grd_Membros_c1_sort, img#ctl00_MainContent_Grid4_c0_sort,
        img#ctl00_MainContent_Grid4_c1_sort, img#ctl00_MainContent_Grid4_c2_sort, img#ctl00_MainContent_Grid4_c3_sort, img#ctl00_MainContent_Grid4_c2_sort, 
        img#ctl00_MainContent_GridCandidatos_c0_sort, img#ctl00_MainContent_GridCandidatos_c1_sort, img#ctl00_MainContent_GridCandidatos_c2_sort, img#ctl00_MainContent_Grid2_c0_sort,
        img#ctl00_MainContent_Grid2_c1_sort, img#ctl00_MainContent_Grid2_c2_sort, img#ctl00_MainContent_Grid3_c0_sort, img#ctl00_MainContent_Grid3_c1_sort, img#ctl00_MainContent_Grid3_c2_sort{
            width: 10px !important;
            opacity: 35%;
            margin-top: .5rem; 
        }
        .auto-style1 {
            width: 120%;
        }

        .tabSecundaria{
            font-family: 'UniviaPro', sans-serif !important;
            font-size: 12px; color: #7D7B7B; 
            padding: 10px 10px; 
            background: #F1F1F1; 
            border-radius: 8px; 
            cursor: hand; 
            width: fit-content; 
            margin-left: 5rem !important;
        }

        .tabSecundaria:hover, .tabSecundariaAtivado{
            font-family: 'UniviaPro', sans-serif !important;
            font-size: 12px; 
            color: #1C9489; 
            font-weight: 500; 
            padding: 10px 15px; 
            background: #D9D9D9; 
            border-radius: 8px; 
            cursor: hand; 
            width: fit-content; 
            margin-left: 5rem !important;
        }

        .rbMembros{
            font-family:'Ubuntu'; 
        }
        
        .listboxEmpregados option{
            font-family: 'UniviaPro', sans-serif !important;
            font-size: 11.5px;
            font-weight: 200 !important;
            color: #7D7B7B;
        }

        .tabelaIndicados {
            font-family: 'UniviaPro', sans-serif !important; 
            font-weight: 400 !important;
        }

        .celularIndicados {
            font-family: 'UniviaPro', sans-serif !important; 
            font-weight: 200 !important;
            font-size: 11.5px;
            padding-left: .5em;
        
	        color: #7D7B7B;
        }

        .editar>a{
            font-size: 11.5px;
            color: #1C9489 !important;
            text-decoration: none !important;
        }

        .editarReunioes{
            padding-left: 15% !important;
            margin-top: -5% !important;
        }

        .editarReunioes>a{
            font-size: 12.45px;
            color: #1C9489 !important;
            text-decoration: none !important;
            font-weight: 500 !important;
            text-align: center !important;
        }

        .chkRegistsro > input{
            height: 1.15rem !important;
            width: 1.15rem !important;
            margin-top: 0rem !important;
            margin-right: 0.25rem !important;
        }

        .chkInativos1 > input{
            height: 1.15rem !important;
            width: 1.15rem !important;
            margin-top: 0rem !important;
            margin-right: 0.25rem !important;
        }

        .chkInativos2 > input{
            height: 1.15rem !important;
            width: 1.15rem !important;
            margin-top: 0rem !important;
            margin-right: 0.25rem !important;
        }

        .linhaVazia{
            background-color: white;
            color: white;
        }
        .linhaVazia > a{
            pointer-events: none;
            cursor: default;
            color: white;
        }
    </style>
    <script type="text/javascript">
    var g_itemIndex = -1;
    var g_cellIndex = -1;
    function OnCellSelected(grid) {
        var cell = grid.getSelectedCell();
        grid.raiseItemCommandEvent(cell.getItemIndex(), "Seleção");
    }
    function OnItemCommand(grid, itemIndex, colIndex, commandName) {
        //grid.raiseItemCommandEvent(itemIndex, commandName);
        grid.raiseItemCommandEvent(itemIndex, colIndex.toString());
    }
    function abrirReuniao(parametro) {
        window.open('AddReuniao.aspx?r=' + parametro, 'CursoEmpresa','scrollbars=yes, resizable=yes, toolbar=no, addressbar=no, menubar=no, width=850px, height=1100px');
    }
    </script>
</asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    
    <div class="container-fluid d-flex ms-5 ps-4">
        <div class="row gx-3 gy-2 w-100">

            <!-- ABAS -->
            <eo:TabStrip ID="TabStrip1" runat="server" ControlSkinID="None" Width="1000px" MultiPageID="MultiPage1">
                <LookItems>
                    <eo:TabItem ItemID="_Default"
                        NormalStyle-CssText="font-family:'Ubuntu'; font-size: 12px; color: #7D7B7B; padding: 5px 8px; background: #F1F1F1; cursor: hand; width: fit-content; margin-right: 1rem; border-bottom: 2px solid #1C9489"
                        SelectedStyle-CssText="font-family:'Ubuntu'; font-size: 12px; color: #1C9489; font-weight: bold; padding: 5px 8px; background: transparent; border-radius: 8px; border-left: 2px solid #1C9489; border-right: 2px solid #1C9489; border-top: 2px solid #1C9489;  cursor: hand; width: fit-content; margin-right: 1rem;">
                        <SubGroup OverlapDepth="8" ItemSpacing="5"
                            Style-CssText="font-family:'Ubuntu'; font-size: 12px; color: #B0ABAB; padding: 10px 10px 10px 0px; border-radius: 8px; cursor: hand; width: fit-content;">
                        </SubGroup>
                    </eo:TabItem>
                </LookItems>
                <TopGroup>
                    <Items>
                        <eo:TabItem Text-Html="Início"></eo:TabItem>

                        <eo:TabItem Text-Html="Criação da Comissão Eleitoral"></eo:TabItem>

                        <eo:TabItem Text-Html="Edital de Convocação da Eleição"></eo:TabItem>

                        <eo:TabItem Text-Html="Comunicação ao Sindicato"></eo:TabItem>

                        <eo:TabItem Text-Html="Inscrições"></eo:TabItem>

                        <eo:TabItem Text-Html="Indicação dos Candidatos"></eo:TabItem>

                        <eo:TabItem Text-Html="Cédula de Votação"></eo:TabItem>

                        <eo:TabItem Text-Html="Ata de Eleição da Posse"></eo:TabItem>

                        <eo:TabItem Text-Html="Ata da Posse da Nova Gestão"></eo:TabItem>

                        <eo:TabItem Text-Html="Calendário das Reuniões Ordinárias"></eo:TabItem>

                    </Items>
                </TopGroup>
            </eo:TabStrip>

            <eo:MultiPage ID="MultiPage1" runat="server" Height="200" Width="1050px">

                <!-- Início -->
                <eo:PageView ID="Pageview18" runat="server" Width="1050px">
                  <div class="col-12 subtituloBG">
                        <h2 class="subtitulo" style="padding-top: .8rem">Configurações</h2>
                  </div>
                  <div class="container">
                      <div class="row">
                          <div class="col-12 pt-3">
                              <asp:Label ID="lblSindicato" runat="server" class="tituloLabel form-label col-form-label col-form-label-sm" style="font-family: 'Ubuntu', sans-serif !important;color: #1C9489;">Sindicato <i class="bi bi-pencil-square"></i></asp:Label>
                              <asp:TextBox ID="txtSindicato" runat="server" class="texto form-control form-control-sm" AutoPostBack="true" ONTEXTCHANGED="txtSindicato_TextChanged" ReadOnly="True" Enabled="False"  style="font-family: 'Ubuntu', sans-serif !important;"></asp:TextBox>
                        </div>
                        <div>
                            <asp:Label runat="server" Text="E-mails para Alerta da CIPA (separar com ;)" CssClass="tituloLabel form-label col-form-label col-form-label-sm" style="font-family: 'Ubuntu', sans-serif !important;color: #1C9489; font-size: .8rem;font-weight: 300 !important;"></asp:Label>
                            <asp:TextBox ID="txtEmailsAlerta" runat="server" class="texto form-control form-control-sm" Rows="5" TextMode="MultiLine" AutoPostBack="true" ONTEXTCHANGED="txtEmailsAlerta_TextChanged" style="font-family: 'Ubuntu', sans-serif !important;"></asp:TextBox>
                        </div>
                          <div class="col-12 subtituloBG gy-2 gx-2">
                              <h2 class="subtitulo">Composição da CIPA</h2>
                          </div>
                          <div class="col-3 gy-2 gx-2">
                              <asp:Label runat="server" Text="Titulares" CssClass="tituloLabel form-label col-form-label col-form-label-sm" style="font-family: 'Ubuntu', sans-serif !important;color: #1C9489;"></asp:Label>
                              <br />
                              <asp:TextBox ID="txtTitulares" runat="server" class="texto form-control form-control-sm" Enabled="False" style="font-family: 'Ubuntu', sans-serif !important;"></asp:TextBox>
                          </div>
                          <div class="col-3 gy-2 gx-2">
                              <asp:Label runat="server" Text="Suplentes" CssClass="tituloLabel form-label col-form-label col-form-label-sm" style="font-family: 'Ubuntu', sans-serif !important;color: #1C9489;"></asp:Label>
                              <br />
                              <asp:TextBox ID="txtSuplentes" runat="server" class="texto form-control form-control-sm" Enabled="False"></asp:TextBox>
                          </div>
                          <div class="col-2 gy-2 gx-2">
                              <asp:Label runat="server" Text="Término do Mandato" CssClass="tituloLabel form-label col-form-label col-form-label-sm" style="font-family: 'Ubuntu', sans-serif !important;color: #1C9489; font-size: .8rem;font-weight: 300 !important;"></asp:Label>
                              <asp:TextBox ID="txtTerminoMandato" runat="server" class="texto form-control form-control-sm" ONTEXTCHANGED="txtTerminoMandato_TextChanged" AutoPostBack="true" Enabled="False" ></asp:TextBox>
                          </div>
                      </div>               
                  </div>
                     
                  <div class="col-12">
                      <div class="row">
                            <div class="col-2 gy-2 gx-2 m-2 pt-3">
                              <asp:Button ID="cmd_GestaoAtual" runat="server" class="btn text-center" Text="Integrantes Gestão Atual" OnClick="cmd_GestaoAtual_Click" style="font-size: 14.5px !important; text-align:center; vertical-align:central;"/>
                            </div>
                          <div class="col-2 gy-2 gx-2 m-2 pt-3 ms-3">
                              <asp:Button ID="cmd_Membros" runat="server" class="btn text-center" Text="Membros 3 Últimas CIPA" OnClick="cmd_Membros_Click" style="font-size: 14.5px !important; text-align:center; vertical-align:central;"/>
                            </div>
                      </div>            
                  </div>

                  </eo:PageView>

                <!-- Criação da Comissão Eleitoral -->
                <eo:PageView ID="Pageview1" runat="server" Width="1050px">
                         <div class="row mt-3">
                            <div class="col-5 subtituloBG">
                                <div class="row">
                                    <h2 class="subtitulo">Calendário CIPA</h2>
                                <div class="col-12 gx-2 gy-2 mt-4">
                                    <asp:Label runat="server" Text="Data" CssClass="tituloLabel form-label col-form-label col-form-label-sm" style="font-family: 'Ubuntu', sans-serif !important;color: #1C9489;"></asp:Label>
                                    <asp:TextBox ID="dataCalendario_Comissao" runat="server" AutoPostBack="True" class="texto form-control form-control-sm" ONTEXTCHANGED="dataCalendario_Comissao_TextChanged" style="font-family: 'Ubuntu', sans-serif !important;"></asp:TextBox>
                                </div>                     
                                <div class="col-12 gx-2 gy-2">
                                        <asp:Label ID="lblLocalizaCandidatos" runat="server" class="tituloLabel form-label col-form-label col-form-label-sm" style="font-family: 'Ubuntu', sans-serif !important;color: #1C9489;">Localizar Candidatos</asp:Label>
                                        <asp:TextBox ID="txtLocalizaCandidatos" runat="server" class="texto form-control form-control-sm" AutoPostBack="true" OnTextChanged="txtLocalizarCandidatos_TextChanged" style="font-family: 'Ubuntu', sans-serif !important;"></asp:TextBox> 
                                </div>
                                <!--Tabela-->
                                <div class="col-11 gx-2 gy-2 mt-1">
                                    <asp:Label ID="lblPresidente_Vice" runat="server" class="tituloLabel form-check-label mt-2" Text="Presidente e Vice-Presidente" style="font-family: 'Ubuntu', sans-serif !important;color: #1C9489;"></asp:Label>
                                    <eo:Grid ID="grd_Presidente_Vice" runat="server" FixedColumnCount="1" ColumnHeaderDividerOffset="6" 
                                    ColumnHeaderAscImage="00050403" ColumnHeaderDescImage="00050404"
                                    GridLines="Both" PageSize="500" KeyField="IdEmprcImaegado" CssClass="grid"
                                    ColumnHeaderHeight="30" ItemHeight="30" Width="440px">
                                    <ItemStyles>
                                        <eo:GridItemStyleSet>
                                            <ItemStyle CssText="background-color: #FAFAFA" />
                                            <ItemHoverStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500;  text-align: left; height: 30px !important; background-color: rgb(28, 148, 137, 0.32); color: #FFFFFF; padding: 0.3rem 0.3rem 0.3rem 0.5rem; " />
                                            <AlternatingItemStyle CssText="background-color: #F2F2F2;" />
                                            <AlternatingItemHoverStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500;  text-align: left; height: 30px !important; background-color: rgba(28, 148, 137, 0.32); color: #FFFFFF; padding: 0.3rem 0.3rem 0.3rem 0.5rem;" />
                                            <SelectedStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500;  text-align: left; height: 30px !important; background-color: black; color: #FFFFFF; padding: 0.3rem 0.3rem 0.3rem 0.5rem;" />
                                            <CellStyle CssText="padding: 0.3rem 0.3rem 0.3rem 0.5rem; height: 30px !important;" />
                                        </eo:GridItemStyleSet>
                                    </ItemStyles>
                                    <ColumnHeaderTextStyle CssText="" />
                                    <ColumnHeaderStyle CssClass="tabelaC colunas" CssText="font-family: 'UniviaPro', sans-serif !important; font-weight: 400 !important;"/>
                                    <Columns>
                                    <eo:StaticColumn HeaderText="Empregado" AllowSort="True" 
                                        DataField="Empregado" Name="Empregado" ReadOnly="True" Width="220">
                                    <CellStyle CssText="font-family: 'UniviaPro'; font-size: 11.5px; font-weight: normal; color: #7D7B7B; text-align: left; height: 30px !important; " />
                                    </eo:StaticColumn>
                                    <eo:StaticColumn HeaderText="Cargo" AllowSort="True" 
                                        DataField="Cargo" Name="Cargo" ReadOnly="True" Width="220">
                                    <CellStyle CssText="font-family: 'UniviaPro'; font-size: 11.5px; font-weight: normal; color: #7D7B7B; text-align: left; height: 30px !important; " />
                                    </eo:StaticColumn>
                                 </Columns>
                                </eo:Grid>
                                </div>

                                      
                        </div>
                     </div>
                    <div class="col me-2" style="margin-top:15rem">
                        <div class="col-1 text-center ms-5 mt-5">
                                    <div class="row mt-4">
                                      <div class="col-12 gy-2">
                                            <asp:ImageButton ID="ImageButton1" ImageUrl="Images/subir.svg" runat="server" OnClick="subir_Click"/>
                                     </div>
                                     <div class="col-12 gy-2">
                                          <asp:ImageButton ID="ImageButton2" ImageUrl="Images/direita.svg" runat="server" OnClick="direita_Click"/>
                                     </div>
                                     <div class="col-12 gy-2">
                                          <asp:ImageButton ID="ImageButton3" ImageUrl="Images/esquerda.svg" runat="server" OnClick="esquerda_Click"/>
                                     </div>
                                     <div class="col-12 gy-2">
                                         <asp:ImageButton ID="ImageButton4" ImageUrl="Images/descer.svg" runat="server" OnClick="descer_Click"/>
                                     </div>
                                 </div>
                            </div>
 
                    </div>
               
                            <div class="col-6 subtituloBG">
                              <div class="row">
                                  <h2 class="subtitulo">Membros</h2>
                                <div class="col-md-2 gx-3 gy-2 mt-4">
                                    <asp:Label runat="server" Text="Membros" CssClass="tituloLabel form-label col-form-label col-form-label-sm" style="font-family: 'Ubuntu', sans-serif !important;color: #1C9489;"></asp:Label>
                                    <asp:RadioButtonList ID="rbMembros" runat="server" RepeatColumns="3" tabIndex="1" CssClass="texto form-control-sm ms-3 rbMembros" Width="520" OnSelectedIndexChanged="rbMembros_SelectedIndexChanged" AutoPostBack="true">
                                    <asp:ListItem Value="1">Presidente e Vice-Presidente</asp:ListItem>
                                    <asp:ListItem Value="2">Todos Integrantes</asp:ListItem>
                                    <asp:ListItem Value="3" Selected="True">Todos Empregados</asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                                 <div class="col-12 gx-2 gy-2 mt-3">
                                        
                                </div>
                                  <!--Tabela-->
                                <div class="col-12 gx-2 gy-2 mt-5">
                                    <asp:Label ID="Label1" runat="server" class="tituloLabel form-check-label mt-2" Text="Comissão Eleitoral" style="font-family: 'Ubuntu', sans-serif !important;color: #1C9489;"></asp:Label>
                                    <eo:Grid ID="grd_Membros" runat="server" FixedColumnCount="1" ColumnHeaderDividerOffset="6" 
                                    ColumnHeaderAscImage="00050403" ColumnHeaderDescImage="00050404"
                                    GridLines="Both" PageSize="500" KeyField="IdEmprcImaegado" CssClass="grid"
                                    ColumnHeaderHeight="30" ItemHeight="30" Width="540">
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
                                    <ColumnHeaderStyle CssClass="tabelaC colunas" CssText="font-family: 'UniviaPro', sans-serif !important; font-weight: 400 !important;"/>
                                    <Columns>
                                    <eo:StaticColumn HeaderText="Empregado" AllowSort="True" 
                                        DataField="NomeMembro" Name="Empregado" ReadOnly="True" Width="340">
                                    <CellStyle CssText="font-family: 'UniviaPro'; font-size: 11.5px; font-weight: normal; color: #7D7B7B; text-align: left; height: 30px !important" />
                                    </eo:StaticColumn>
                                    <eo:StaticColumn HeaderText="Cargo" AllowSort="True" 
                                        DataField="NomeCargo" Name="Cargo" ReadOnly="True" Width="167">
                                    <CellStyle CssText="font-family: 'UniviaPro'; font-size: 11.5px; font-weight: normal; color: #7D7B7B; text-align: left; height: 30px !important;" />
                                    </eo:StaticColumn>
                                 </Columns>
                                </eo:Grid>
                                </div>
                              </div>
                            </div>
                        </div>       
                </eo:PageView>

                <!-- Edital de Convocação da Eleição -->
                <eo:PageView ID="Pageview2" runat="server" Width="1050px">
                    <div class="row mt-3">
                        <div class="col-4">
                            <div class="row">
                                <div class="col-12 subtituloBG">
                                    <h2 class="subtitulo">Calendário CIPA</h2>
                                </div>
                                <div class="col-12 gx-2 gy-2">
                                    <asp:Label runat="server" Text="Data" CssClass="tituloLabel form-label col-form-label col-form-label-sm" style="font-family: 'Ubuntu', sans-serif !important;color: #1C9489;"></asp:Label>
                                    <asp:TextBox ID="dataCalendario_Edital" runat="server" AutoPostBack="True" class="texto form-control form-control-sm" OnTextChanged="dataCalendario_Edital_TextChanged" style="font-family: 'Ubuntu', sans-serif !important;"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <div class="col-6 ms-4">
                            <div class="row">
                                <div class="col-12 subtituloBG">
                                    <h2 class="subtitulo">Local</h2>
                                </div>
                                <div class="col-3 gx-2 gy-2">
                                    <asp:Label runat="server" Text="Horário" CssClass="tituloLabel form-label col-form-label col-form-label-sm" style="font-family: 'Ubuntu', sans-serif !important;color: #1C9489;"></asp:Label>
                                    <asp:TextBox ID="txt_horario_local" runat="server" class="texto form-control form-control-sm" AutoPostBack="True" OnTextChanged="txt_horario_local_TextChanged" style="font-family: 'Ubuntu', sans-serif !important;"></asp:TextBox>
                                </div>
                                <div class="col-9 gx-2 gy-2">
                                    <asp:Label runat="server" Text="Local" CssClass="tituloLabel form-label col-form-label col-form-label-sm" style="font-family: 'Ubuntu', sans-serif !important;color: #1C9489;"></asp:Label>
                                    <asp:TextBox ID="txt_local" runat="server" class="texto form-control form-control-sm" AutoPostBack="True" OnTextChanged="txt_local_TextChanged" style="font-family: 'Ubuntu', sans-serif !important;"></asp:TextBox>
                                </div>
                            </div>
                        </div>
    
                        <div class="col-12 gx-2 gy-2">
                            <asp:Button ID="btn_folha_inscricao" runat="server" class="btn" Text="Folha de Inscrição" OnClick="btn_folha_inscricao_Click" style="font-size: 14.5px !important; text-align:center; vertical-align:central;"/>
                            <asp:Button ID="btn_folha_comprovante" runat="server" class="btn" Text="Comprovante de Inscrição" OnClick="btn_folha_comprovante_Click" style="font-size: 14.5px !important; text-align:center; vertical-align:central;"/>
                            <asp:Button ID="btn_edital" runat="server" class="btn" Text="Edital" OnClick="btn_edital_Click" style="font-size: 14.5px !important; text-align:center; vertical-align:central;"/>
                       </div>
                    </div>
                </eo:PageView>
                        

                <!-- Comunicação ao Sindicato -->
                <eo:PageView ID="Pageview3" runat="server" Width="1050px">
                        <div class="col-12 subtituloBG">
                            <h2 class="subtitulo" style="padding-top: .8rem;">Comunicação ao Sindicato</h2>
                        </div>
                        <div class="row">
                             <div class="col-9">
                                 <div class="row">
                                     <div class="col-3 gx-2 gy-2">
                                          <asp:Label runat="server" Text="Data" CssClass="tituloLabel form-label col-form-label col-form-label-sm" style="font-family: 'Ubuntu', sans-serif !important;color: #1C9489;"></asp:Label>
                                          <asp:TextBox ID="data_comunicacao" runat="server" AutoPostBack="True" class="texto form-control form-control-sm" OnTextChanged="data_comunicacao_TextChanged" style="font-family: 'Ubuntu', sans-serif !important; width:95%"></asp:TextBox>
                                    </div> 
                                 </div>
                             </div>
                            
                        </div>
                        <div class="col-6 gx-2 gy-4">
                                <asp:Button ID="btn_imprimir_comunicacao" class="btn" style="margin-top: .5rem; font-size: 14.5px !important; text-align:center; vertical-align:central;" runat="server" Text="Comunição ao Sindicato" OnClick="btn_imprimir_comunicacao_Click"/>
                        </div>
                </eo:PageView>

                <!-- Início das Inscrições -->
                <eo:PageView ID="Pageview5" runat="server" Width="1050px">
                        <div class="col-12 subtituloBG">
                            <h2 class="subtitulo" style="padding-top: .8rem;">Data das Inscrições</h2>
                        </div>
                        <div class="row">
                             <div class="col-12">
                                <div class="row">
                                     <div class="col-md-2 gx-2 gy-2"">
                                        <asp:Label runat="server" CssClass="tituloLabel form-label col-form-label col-form-label-sm" style="font-family: 'Ubuntu', sans-serif !important;color: #1C9489;">Data de Início</asp:Label>
                                        <asp:TextBox ID="data_inscricoes" runat="server" AutoPostBack="True" class="texto form-control form-control-sm" OnTextChanged="data_inscricoes_TextChanged" style="font-family: 'Ubuntu', sans-serif !important;"></asp:TextBox>
                                    </div>
                                    <div class="col-md-2 gx-2 gy-2"">
                                        <asp:Label ID="lblDataTermInscricoes" runat="server" class="tituloLabel form-label col-form-label col-form-label-sm" style="font-family: 'Ubuntu', sans-serif !important;color: #1C9489;">Data de Término</asp:Label>
                                        <asp:TextBox ID="txtDataTermInscricoes" runat="server" class="texto form-control form-control-sm" MaxLength="10" AutoPostBack="true" OnTextChanged="txtDataTerminoInscricoes_TextChanged" style="font-family: 'Ubuntu', sans-serif !important;"></asp:TextBox>                                   
                                </div>
                             </div>
                        </div>
                    </div>
                </eo:PageView>

              
                
                <!-- Candidatos -->
                <eo:PageView ID="Pageview7" runat="server" Width="1050px">
                    <div class="col-12 subtituloBG">
                        <h2 class="subtitulo" style="padding-top: .8rem;">Indicação dos Candidatos (Empregado)</h2>
                    </div>

                    <div class="col-12 gx-2 gy-2">
                        <div class="row">
                            <div class="col-md-5 gx-2 gy-2">
                                <asp:Label ID="lblDataCandidatos" runat="server" class="tituloLabel form-label col-form-label col-form-label-sm" style="font-family: 'Ubuntu', sans-serif !important;color: #1C9489;">Data</asp:Label>
                                <asp:TextBox ID="txtDataCandidatos" runat="server" class="texto form-control form-control-sm" MaxLength="10" OnTextChanged="txtDataCandidatos_TextChanged" AutoPostBack="True" style="font-family: 'Ubuntu', sans-serif !important;"></asp:TextBox>                                        
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-5">
                                <div class="row">
                                    <div class="col-12 gx-2 gy-2">
                                        <asp:Label ID="lblLocalizar" runat="server" class="tituloLabel form-label col-form-label col-form-label-sm" style="font-family: 'Ubuntu', sans-serif !important;color: #1C9489;">Localizar Candidatos</asp:Label>
                                        <asp:TextBox ID="txtLocalizar" runat="server" class="texto form-control form-control-sm" AutoPostBack="true" OnTextChanged="txtLocalizar_TextChanged" style="font-family: 'Ubuntu', sans-serif !important;"></asp:TextBox> 
                                    </div>

                                    <div class="col-12 gx-2 gy-2">
                                        <asp:Label ID="lblEmpregados" runat="server" class="tituloLabel form-label col-form-label col-form-label-sm" style="font-family: 'Ubuntu', sans-serif !important;color: #1C9489;">Empregados</asp:Label>
                                        <asp:ListBox ID="lstEmpregados" runat="server" class="form-control form-control-sm listboxEmpregados"></asp:ListBox>
                                    </div>
                                </div>
                            </div>

                            <div class="col-1 text-center">
                                <div class="row mt-4">
                                    <div class="col-12 gy-2">
                                        <asp:ImageButton ID="btnSubir" ImageUrl="Images/subir.svg" runat="server" OnClick="btnSubir_Click"/>
                                    </div>
                                    <div class="col-12 gy-2">
                                        <asp:ImageButton ID="btnAdicionar" ImageUrl="Images/direita.svg" runat="server" OnClick="btnDireita_Click"/>
                                    </div>
                                    <div class="col-12 gy-2">
                                        <asp:ImageButton ID="btnRemover" ImageUrl="Images/esquerda.svg" runat="server" OnClick="btnEsquerda_Click"/>
                                    </div>
                                    <div class="col-12 gy-2">
                                        <asp:ImageButton ID="btnDescer" ImageUrl="Images/descer.svg" runat="server" OnClick="btnDescer_Click"/>
                                    </div>
                                </div>
                            </div>
                            <div class="col-5" style="margin-top: -3rem">
                                <div class="row">
                                    <div class="col-md-5">
                                        <asp:Label ID="lblIndicados" runat="server" class="tituloLabel form-label col-form-label col-form-label-sm" style="font-family: 'Ubuntu', sans-serif !important;color: #1C9489;">Indicados</asp:Label>
                                    </div>
                                </div>
                                    <eo:Grid ID="Grid4" runat="server" ColumnHeaderAscImage="00050403" 
                                                ColumnHeaderDescImage="00050404" FixedColumnCount="0" GridLines="Both" Height="200px" Width="525px"  CssClass="grid"
                                                ColumnHeaderHeight="30" ItemHeight="30" KeyField="IdEmprcImaegado" ClientSideOnItemCommand="OnItemCommand" OnItemCommand="Grid4_ItemCommand" ClientSideOnCellSelected="OnCellSelected">                          
                                            <itemstyles>
                                                <eo:GridItemStyleSet>
                                                    <ItemStyle CssText="background-color: #FAFAFA" />
                                                    <ItemHoverStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500;  text-align: left; height: 30px !important; background-color: rgb(28, 148, 137, 0.32); color: #FFFFFF; padding: 0.3rem 0.3rem 0.3rem 0.5rem; " />
                                                    <AlternatingItemStyle CssText="background-color: #F2F2F2;" />
                                                    <AlternatingItemHoverStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500;  text-align: left; height: 30px !important; background-color: rgba(28, 148, 137, 0.32); color: #FFFFFF; padding: 0.3rem 0.3rem 0.3rem 0.5rem;" />
                                                    <SelectedStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500;  text-align: left; height: 30px !important; background-color: rgba(28, 148, 137, 0.32); color: #FFFFFF; padding: 0.3rem 0.3rem 0.3rem 0.5rem;" />
                                                    <CellStyle CssText="padding: 0.3rem 0.3rem 0.3rem 0.5rem; height: 30px !important;" />
                                                </eo:GridItemStyleSet>
                                            </itemstyles>
                                            <ColumnHeaderStyle CssClass="tabelaC colunas tabelaIndicados" />
                                            <Columns>
                                                <eo:ButtonColumn ButtonText="Editar" CommandName="Editar" Name="Editar" DataField="Editar" Width="60" ButtonType="LinkButton">
                                                    <CellStyle CssText="font-family: 'UniviaPro', sans-serif !important; font-size: 11.5px; font-weight: 300 !important; color: #7D7B7B; text-align: center; height: 30px !important;" CssClass="tituloLabel editar" />
                                                </eo:ButtonColumn>
                                                <eo:StaticColumn HeaderText="Nome" AllowSort="True"  
                                                    DataField="Nome" Name="Nome" ReadOnly="True" 
                                                    Width="160">
                                                    <CellStyle CssText="font-family: 'UniviaPro', sans-serif !important; font-size: 11.5px; font-weight: 300 !important; color: #7D7B7B; text-align: left; height: 30px !important;" CssClass="celularIndicados" />                   
                                                </eo:StaticColumn>
                                                <eo:StaticColumn HeaderText="Setor" AllowSort="True"  
                                                    DataField="Setor" Name="Setor" ReadOnly="True" 
                                                    Width="170">
                                                    <CellStyle CssText="font-family: 'UniviaPro', sans-serif !important; font-size: 11.5 px; font-weight: 300 !important; color: #7D7B7B; text-align: left; height: 30px !important;" CssClass="celularIndicados" />                   
                                                </eo:StaticColumn>
                                                <eo:StaticColumn HeaderText="Apelido" AllowSort="True"  
                                                    DataField="Apelido" Name="Apelido" ReadOnly="True" 
                                                    Width="135">
                                                    <CellStyle CssText="font-family: 'UniviaPro', sans-serif !important; font-size: 11.5 px; font-weight: 300 !important; color: #7D7B7B; text-align: left; height: 30px !important;" CssClass="celularIndicados" />                   
                                                </eo:StaticColumn>
                                            </Columns>
                                        </eo:Grid>
                                <div class="auto-style1">
                                    <asp:Label ID="lblApelido" runat="server" class="tituloLabel form-label col-form-label col-form-label-sm" Visible="False"></asp:Label>
                                    <asp:TextBox ID="txtApelido" runat="server" class="texto form-control form-control-sm" AutoPostBack="true" Visible="False"></asp:TextBox>
                                    <asp:Button ID="btnApelidoAplicar" runat="server" class="btn" style="margin-top: .3rem; font-size: 14.5px !important; text-align:center; vertical-align:central;" Text="Aplicar" Visible="False" OnClick="btnApelidoAplicar_Click"/> 
                                    <asp:Button ID="btnApelidoCancelar" runat="server" class="btn" style="margin-top: .3rem; font-size: 14.5px !important; text-align:center; vertical-align:central;" Text="Cancelar" Visible="False" OnClick="btnApelidoCancelar_Click"/> 
                                </div>
                            </div>
                            
                        </div>
                    </div>
                </eo:PageView>

                <!-- Cédula de Votação -->
                <eo:PageView ID="Pageview8" runat="server" Width="1050px">
                    <div class="col-12 subtituloBG">
                        <h2 class="subtitulo" style="padding-top: .8rem;">Cédula de Votação</h2>
                    </div>

                    <div class="col-12">
                        <div class="row">
                            <div class="col-md-2 gx-2 gy-2">
                                <asp:Label ID="lblDataCedula" runat="server" class="tituloLabel form-label col-form-label col-form-label-sm">Data</asp:Label>
                                <asp:TextBox ID="txtDataCedula" runat="server" class="texto form-control form-control-sm" MaxLength="10" AutoPostBack="true" OnTextChanged="txtDataCedula_TextChanged"></asp:TextBox>                                        
                            </div>

                            <div class="col-md-3 gx-2 gy-2">
                                <asp:Label ID="lblQtdEmpregado" runat="server" class="tituloLabel form-label">Quantidade por Empregados</asp:Label>
                                <div class="row">
                                    <div class="col-12">
                                        <asp:TextBox ID="txtQtdEmpregado" runat="server" class="texto form-control form-control-sm" MaxLength="10" ReadOnly="True" Enabled="False"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-3 gy-2">
                                <asp:Label ID="lblQtdCedulas" runat="server" class="tituloLabel form-label">Quantidade de Cédulas</asp:Label>
                                <div class="row">
                                    <div class="col-12">
                                        <asp:TextBox ID="txtQtdCedulas" runat="server" class="texto form-control form-control-sm" MaxLength="10"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-2 gx-2 ms-4" style="margin-top: 2.2rem">
                                <asp:CheckBox ID="chkRegistro" runat="server"  type="checkbox" class="chkRegistsro" Text="Com Nº Registro" />
                            </div>

                            <div class="col-12 gx-2 gy-2">
                                <asp:Button ID="btnImprimirCedulas" runat="server" class="btn" Text="Imprimir Cédulas" OnClick="btnImprimirCedulas_Click" style="font-size: 14.5px !important; text-align:center; vertical-align:central;"/>
                            </div>
                        </div>
                    </div>
                </eo:PageView>

                <!-- Ata de Eleição da Posse -->
                <eo:PageView ID="Pageview9" runat="server" Width="1050px">
                    <div class="col-12 subtituloBG">
                        <h2 class="subtitulo" style="padding-top: .8rem;">Ata de Eleição da Posse</h2>
                    </div>

                    <eo:TabStrip ID="TabStrip2" runat="server" ControlSkinID="None" Width="1050px" Style="margin-top: 14px; border-collapse: separate; border-spacing: 10px;" MultiPageID="MultiPage2" RowSpacing="10">
                        <LookItems>
                           <eo:TabItem ItemID="_Default"
                                 NormalStyle-CssClass="tabSecundaria"
                                SelectedStyle-CssClass="tabSecundariaAtivado" NormalStyle-CssText="margin-bottom: 10px;">
                                <SubGroup OverlapDepth="8" ItemSpacing="20"
                                    Style-CssText="font-family:'Ubuntu'; font-size: 12px; color: #B0ABAB; padding: 10px 10px 10px 0px; border-radius: 8px; cursor: hand; width: fit-content">
                                </SubGroup>
                           </eo:TabItem>
                        </LookItems>
                        <TopGroup>
                            <Items>
                                <eo:TabItem Text-Html="Eleição CIPA"></eo:TabItem>
                                <eo:TabItem Text-Html="Texto Adicional para Ata de Eleição" ></eo:TabItem>
                            </Items>
                        </TopGroup>
                    </eo:TabStrip>

                    <eo:MultiPage ID="MultiPage2" runat="server" Height="200" Width="1050px">
                        <eo:PageView ID="Pageview12" runat="server" Width="1050px">
                            <div class="col-12">
                                <div class="row">
                                    <div class="col-12 gx-2 gy-2">
                                        <div class="row">
                                            <div class="col-2 gx-2">
                                                <asp:Label ID="lblDataCalendario" runat="server" class="tituloLabel form-label col-form-label col-form-label-sm">Data</asp:Label>
                                                <asp:TextBox ID="txtDataCalendario" runat="server" class="texto form-control form-control-sm" MaxLength="10" AutoPostBack="True" OnTextChanged="txtDataCalendario_TextChanged" Style="width:95%"></asp:TextBox> 
                                            </div>
                                            <div class="col-2 gx-2">
                                                <asp:Label ID="lblHoraReuniao" runat="server" class="tituloLabel">Horário Início</asp:Label>
                                                <asp:TextBox ID="txtHoraReuniao" runat="server" class="texto form-control form-control-sm" MaxLength="10" AutoPostBack="True"  OnTextChanged="txtHoraReuniao_TextChanged" ></asp:TextBox> 
                                            </div>
                                            <div class="col-2 gx-2">
                                                <asp:Label ID="lblHoraReuniao2" runat="server" class="tituloLabel">Horário Término</asp:Label>
                                                <asp:TextBox ID="txtHoraReuniao2" runat="server" class="texto form-control form-control-sm" MaxLength="10" AutoPostBack="true" OnTextChanged="txtHoraReuniao2_TextChanged" ></asp:TextBox>
                                            </div>

                                            <div class="col-3 gx-2">
                                                <asp:Label ID="lblLocal" runat="server" class="tituloLabel">Local</asp:Label>
                                                <asp:TextBox ID="txtLocal" runat="server" class="texto form-control form-control-sm" AutoPostBack="True" OnTextChanged="txtLocal_TextChanged" Style="width: 66%"></asp:TextBox> 
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-12 gy-2">
                                        <div class="row">
                                            <div class="col-2 gx-2">
                                                <asp:Label ID="lblBranco" runat="server" class="tituloLabel">Branco</asp:Label>
                                                <asp:TextBox ID="txtBranco" runat="server" class="texto form-control form-control-sm" AutoPostBack="True"  OnTextChanged="txtBranco_TextChanged" ></asp:TextBox>
                                            </div>

                                            <div class="col-2 gx-2">
                                                <asp:Label ID="lblNulo" runat="server" class="tituloLabel">Nulo</asp:Label>
                                                <asp:TextBox ID="txtNulo" runat="server" class="texto form-control form-control-sm" AutoPostBack="True" OnTextChanged="txtNulo_TextChanged"></asp:TextBox>
                                            </div>

                                            <div class="col-2 gx-2">
                                                <asp:Label ID="lblTotal" runat="server" class="tituloLabel">Total</asp:Label>
                                                <asp:TextBox ID="txtTotal" runat="server" class="texto form-control form-control-sm" Enabled="false"></asp:TextBox>
                                            </div>

                                            <div class="col-2 gx-2">
                                                <asp:Label ID="lblQtdEmpregados" runat="server" class="tituloLabel">Qtd. Empregados</asp:Label>
                                                <asp:TextBox ID="txtQtdEmpregados" runat="server" class="texto form-control form-control-sm" Enabled="false"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </eo:PageView>

                        <eo:PageView ID="Pageview13" runat="server" Width="1050px">
                            <div class="col-12 gx-2 gy-2">
                                <asp:TextBox ID="lstTexto" runat="server" class="texto form-control form-control-sm" Rows="5" TextMode="MultiLine" AutoPostBack="true" OnTextChanged="lstTexto_TextChanged"></asp:TextBox>
                            </div>
                        </eo:PageView>
                    </eo:MultiPage>

                    <eo:TabStrip ID="TabStrip3" runat="server" ControlSkinID="None" Width="1050px" MultiPageID="MultiPage3">
                        <LookItems>
                            <eo:TabItem ItemID="_Default"
                                NormalStyle-CssClass="tabSecundaria"
                                SelectedStyle-CssClass="tabSecundariaAtivado">
                                <SubGroup OverlapDepth="8" ItemSpacing="5"
                                    Style-CssText="font-family:'Ubuntu'; font-size: 12px; color: #B0ABAB; padding: 10px 10px 10px 0px; border-radius: 8px; cursor: hand; width: fit-content;">
                                </SubGroup>
                            </eo:TabItem>
                        </LookItems>
                        <TopGroup>
                            <Items>
                                <eo:TabItem Text-Html="Candidatos"></eo:TabItem>

                                <eo:TabItem Text-Html="Empregados Eleitos"></eo:TabItem>

                                <eo:TabItem Text-Html="Empregador"></eo:TabItem>

                            </Items>
                        </TopGroup>
                    </eo:TabStrip>

                    <eo:MultiPage ID="MultiPage3" runat="server" Height="200" Width="1050px">
                        <eo:PageView ID="Pageview14" runat="server" Width="1050px">
                            <div class="row">
                                <div class="col-7 gx-2 gy-2">
                                    <eo:Grid ID="GridCandidatos" runat="server" ColumnHeaderAscImage="00050403" 
                                                ColumnHeaderDescImage="00050404" FixedColumnCount="0" GridLines="Both" Height="200px" Width="600px"
                                                ColumnHeaderHeight="30" ItemHeight="30" KeyField="IdEmpregado" ClientSideOnItemCommand="OnItemCommand" ClientSideOnCellSelected="OnCellSelected" OnItemCommand="GridCandidatos_ItemCommand">                    
                                            <itemstyles>
                                                <eo:GridItemStyleSet>
                                                    <ItemStyle CssText="background-color: #FAFAFA;" />
                                                    <ItemHoverStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500;  text-align: left; height: 30px !important; background: rgba(28, 148, 137, 0.80); color: #FFFFFF; padding: 0.3rem 0.3rem 0.3rem 0.5rem; " />
                                                    <AlternatingItemStyle CssText="background-color: #F2F2F2;" />
                                                    <AlternatingItemHoverStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500;  text-align: left; height: 30px !important; background: rgba(28, 148, 137, 0.80); color: #FFFFFF; padding: 0.3rem 0.3rem 0.3rem 0.5rem;" />
                                                    <SelectedStyle CssText="border-bottom-style:solid;border-left-style:solid;border-right-style:solid;border-top-style:solid;" />
                                                    <CellStyle CssText="padding: 0.3rem 0.3rem 0.3rem 0.5rem; height: 30px !important;" />
                                                </eo:GridItemStyleSet>
                                            </itemstyles>
                                            <ColumnHeaderStyle CssClass="tabelaC colunas tabelaIndicados" />
                                            <Columns>
                                                <eo:ButtonColumn Name="Editar" DataField="Editar" Width="50" >
                                                                <CellStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: normal; color: #7D7B7B; text-align: center; height: 30px !important; " CssClass="tituloLabel editar" />
                                                </eo:ButtonColumn>
                                                <eo:StaticColumn HeaderText="Empregado" AllowSort="True" 
                                                    DataField="NomeEmpregado" Name="NomeEmpregado" ReadOnly="True" 
                                                    SortOrder="Ascending" Text="" Width="290">
                                                    <CellStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: normal; color: #7D7B7B; text-align: left; height: 30px !important; "  CssClass="celularIndicados"/>
                                                </eo:StaticColumn>
                                                <eo:TextBoxColumn HeaderText="Votos" AllowSort="True"  
                                                    DataField="Votos" Name="Votos" 
                                                    Width="120">
                                                    <CellStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: normal; color: #7D7B7B; text-align: left; height: 30px !important; "  CssClass="celularIndicados"/>  
                                                    <TextBoxStyle CssText="votos-textbox" />                  
                                                </eo:TextBoxColumn>
                                                <eo:StaticColumn HeaderText="Vice-Presidente" AllowSort="True" 
                                                    DataField="VicePresidente" Name="VicePresidente" ReadOnly="True" 
                                                    Width="130">
                                                    <CellStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: normal; color: #7D7B7B; text-align: left; height: 30px !important; "  CssClass="celularIndicados"/>
                                                </eo:StaticColumn>
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
                                                    <MaskedEdit ControlSkinID="None"  runat="server"
                                                        TextBoxStyle-CssText="BORDER-RIGHT: #7f9db9 1px solid; PADDING-RIGHT: 2px; BORDER-TOP: #7f9db9 1px solid; PADDING-LEFT: 2px; PADDING-BOTTOM: 1px; MARGIN: 0px; BORDER-LEFT: #7f9db9 1px solid; PADDING-TOP: 2px; BORDER-BOTTOM: #7f9db9 1px solid; font-family:Courier New;font-size:8pt;">
                                                    </MaskedEdit>
                                                </eo:MaskedEditColumn>
                                            </ColumnTemplates>
                                            <FooterStyle CssText="padding-bottom:4px;padding-left:4px;padding-right:4px;padding-top:4px;" />
                                        </eo:Grid>
                                        
                                </div>
                                <div class="col-5 gx-2">
                                            <asp:Label ID="lblAlterarVotos" runat="server" class="tituloLabel form-label col-form-label col-form-label-sm" Visible="False"></asp:Label>
                                            <asp:TextBox ID="txtAlterarVotos" runat="server" class="texto form-control form-control-sm" Visible="False"></asp:TextBox>
                                            <asp:Button ID="btnAplicarVotos" runat="server" class="btn" Text="Aplicar" Visible="False" AutoPostBack="true" OnClick="btnAplicarVotos_Click" style="margin-top: .3rem;font-size: 14.5px !important; text-align:center; vertical-align:central;" /> 
                                            <asp:Button ID="btnCancelarVotos" runat="server" class="btn" Text="Cancelar" Visible="False" OnClick="btnCancelarVotos_Click" Width="57px" style="margin-top: .3rem;font-size: 14.5px !important; text-align:center; vertical-align:central;"/> 
                                 </div>
                                <div class="col-12 gx-2 gy-2">
                                    <asp:Button ID="btnAtaEleicao1" runat="server" CssClass="btn" Text="Ata Eleição" OnClick="btnAtaEleicao1_Click" style="font-size: 14.5px !important; text-align:center; vertical-align:central;"/>
                                    <asp:Button ID="btnFolhaApuracao" runat="server" CssClass="btn" Text="Folha de Apuração" OnClick="btnFolhaApuracao_Click" style="font-size: 14.5px !important; text-align:center; vertical-align:central;"/>
                                    <asp:Button ID="btnApurarVotos" runat="server" CssClass="btn" Text="Apurar Votos" OnClick="btnApurarVotos_Click" style="font-size: 14.5px !important; text-align:center; vertical-align:central;"/>
                                </div>
                            </div>
                        </eo:PageView>

                        <eo:PageView ID="Pageview15" runat="server" Width="1050px">
                            <div class="row">
                                <div class="col-7 gx-2 gy-2">
                                    <eo:Grid ID="Grid1" runat="server" ColumnHeaderAscImage="00050403" 
                                                ColumnHeaderDescImage="00050404" FixedColumnCount="0" GridLines="Both" Height="200px" Width="600px"
                                                ColumnHeaderHeight="30" ItemHeight="30" ClientSideOnCellSelected="OnCellSelected" ClientSideOnItemCommand="OnItemCommand" FullRowMode="False">
                                            <itemstyles>
                                                <eo:GridItemStyleSet>
                                                    <ItemStyle CssText="background-color: #FAFAFA;" />
                                                    <ItemHoverStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500;  text-align: left; height: 30px !important; background: rgba(28, 148, 137, 0.80); color: #FFFFFF; padding: 0.3rem 0.3rem 0.3rem 0.5rem; " />
                                                    <AlternatingItemStyle CssText="background-color: #F2F2F2;" />
                                                    <AlternatingItemHoverStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500;  text-align: left; height: 30px !important; background: rgba(28, 148, 137, 0.80); color: #FFFFFF; padding: 0.3rem 0.3rem 0.3rem 0.5rem;" />
                                                    <SelectedStyle CssText="border-bottom-style:solid;border-left-style:solid;border-right-style:solid;border-top-style:solid;" />
                                                    <CellStyle CssText="padding: 0.3rem 0.3rem 0.3rem 0.5rem; height: 30px !important;" />
                                                </eo:GridItemStyleSet>
                                            </itemstyles>
                                            <ColumnHeaderStyle CssClass="tabelaC colunas tabelaIndicados" />
                                            <Columns>
                                                <eo:StaticColumn HeaderText="Empregado" AllowSort="True" 
                                                    DataField="NomeEmpregado" Name="NomeEmpregado" ReadOnly="True" 
                                                    SortOrder="Ascending" Text="" Width="290">
                                                    <CellStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: normal; color: #7D7B7B; text-align: left; height: 30px !important;" CssClass="celularIndicados" />
                                                </eo:StaticColumn>
                                                <eo:StaticColumn HeaderText="Cargo CIPA" AllowSort="True"  
                                                    DataField="CargoCipa" Name="CargoCipa" ReadOnly="True" 
                                                    Width="155">
                                                    <CellStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: normal; color: #7D7B7B; text-align: left; height: 30px !important; "  CssClass="celularIndicados"/>                   
                                                </eo:StaticColumn>
                                                <eo:StaticColumn HeaderText="Estabilidade" AllowSort="True"  
                                                    DataField="Estabilidade" Name="Estabilidade" ReadOnly="True" 
                                                    Width="155">
                                                    <CellStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: normal; color: #7D7B7B; text-align: left; height: 30px !important; "  CssClass="celularIndicados"/>                   
                                                </eo:StaticColumn>
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
                                </div>

                                <div class="col-12 gx-5 gy-2">
                                    <asp:CheckBox ID="chkInativos1" runat="server" class="texto form-check-label bg-transparent chkInativos1" Text="Mostrar Empregados Eleitos Inativos" OnCheckedChanged="chkInativos1_CheckedChanged" AutoPostBack="true"/>
                                </div>

                                <div class="col-12 gx-2 gy-2">
                                    <asp:Button ID="btnAtaEleicao2" runat="server" CssClass="btn" Text="Ata Eleição" style="font-size: 14.5px !important; text-align:center; vertical-align:central;"/>
                                </div>
                            </div>
                        </eo:PageView>

                        <eo:PageView ID="Pageview16" runat="server" Width="1050px">
                            <div class="row">
                                <div class="col-7 gx-2 gy-2">
                                    <eo:Grid ID="Grid2" runat="server" ColumnHeaderAscImage="00050403" 
                                                ColumnHeaderDescImage="00050404" FixedColumnCount="0" GridLines="Both" Height="200px" Width="600px"
                                                ColumnHeaderHeight="30" ItemHeight="30" ClientSideOnCellSelected="OnCellSelected" ClientSideOnItemCommand="OnItemCommand" FullRowMode="False">
                                            <itemstyles>
                                                <eo:GridItemStyleSet>
                                                    <ItemStyle CssText="background-color: #FAFAFA;" />
                                                    <ItemHoverStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500;  text-align: left; height: 30px !important; background: rgba(28, 148, 137, 0.80); color: #FFFFFF; padding: 0.3rem 0.3rem 0.3rem 0.5rem; " />
                                                    <AlternatingItemStyle CssText="background-color: #F2F2F2;" />
                                                    <AlternatingItemHoverStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500;  text-align: left; height: 30px !important; background: rgba(28, 148, 137, 0.80); color: #FFFFFF; padding: 0.3rem 0.3rem 0.3rem 0.5rem;" />
                                                    <SelectedStyle CssText="border-bottom-style:solid;border-left-style:solid;border-right-style:solid;border-top-style:solid;" />
                                                    <CellStyle CssText="padding: 0.3rem 0.3rem 0.3rem 0.5rem; height: 30px !important;" />
                                                </eo:GridItemStyleSet>
                                            </itemstyles>
                                            <ColumnHeaderStyle CssClass="tabelaC colunas tabelaIndicados" />
                                            <Columns>
                                                <eo:StaticColumn HeaderText="Empregado" AllowSort="True"  
                                                    DataField="NomeEmpregado" Name="NomeEmpregado" ReadOnly="True" 
                                                    Width="350">
                                                    <CellStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: normal; color: #7D7B7B; text-align: left; height: 30px !important; " CssClass="celularIndicados" />                   
                                                </eo:StaticColumn>
                                                <eo:StaticColumn HeaderText="Cargo CIPA" AllowSort="True"  
                                                    DataField="CargoCipa" Name="CargoCipa" ReadOnly="True" 
                                                    Width="250">
                                                    <CellStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: normal; color: #7D7B7B; text-align: left; height: 30px !important; " CssClass="celularIndicados" />                   
                                                </eo:StaticColumn>
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
                                </div>
                                <div class="col-12 gx-5">
                                    <asp:CheckBox ID="chkInativos2" runat="server" class="texto form-check-label bg-transparent chkInativos2" Text="Mostrar Indicados do Empregador Inativos" OnCheckedChanged="chkInativos2_CheckedChanged" AutoPostBack="true" />
                                </div>
                                <div class="col-12 gx-2 gy-2">
                                    <asp:Button ID="btnAtaEleicao3" runat="server" CssClass="btn" Text="Ata Eleição" style="font-size: 14.5px !important; text-align:center; vertical-align:central;"/>
                                    <asp:Button ID="btnAddIndicados" runat="server" CssClass="btn" Text="Adicionar Indicados" AutoPostBack="true" OnClick="btnAddIndicados_Click" style="font-size: 14.5px !important; text-align:center; vertical-align:central;"/>
                                </div>
                            </div>
                        </eo:PageView>

                    </eo:MultiPage>
                </eo:PageView>

                <!-- Ata da Posse da Nova Gestão -->
                <eo:PageView ID="Pageview10" runat="server" Width="1050px">
                    <div class="col-12 subtituloBG">
                        <h2 class="subtitulo" style="padding-top: .8rem;">Ata da Posse da Nova Gestão</h2>
                    </div>

                    <div class="col-12">
                        <div class="row">
                            <div class="col-4 gx-2 gy-2">
                                <div class="row">
                                    <div class="col-12">
                                        <asp:Label ID="lblDataPosse" runat="server" class="tituloLabel form-label col-form-label col-form-label-sm">Data</asp:Label>
                                        <asp:TextBox ID="txtDataPosse" runat="server" class="texto form-control form-control-sm" MaxLength="10" OnTextChanged="txtDataPosse_TextChanged" AutoPostBack="True"></asp:TextBox> 
                                    </div>
                                </div>
                            </div>

                            <div class="col-8 gx-2 gy-2">
                                <div class="row">
                                    <div class="col-3">
                                        <asp:Label ID="lblHoraPosse" runat="server" class="tituloLabel">Horário Início</asp:Label>
                                        <asp:TextBox ID="txtHoraPosse" runat="server" class="texto form-control form-control-sm" MaxLength="10" OnTextChanged="txtHoraPosse_TextChanged" AutoPostBack="True"></asp:TextBox> 
                                    </div>
                                    <div class="col-3 gx-3">
                                        <asp:Label ID="Label2" runat="server" class="tituloLabel">Horário Término</asp:Label>
                                        <asp:TextBox ID="txtHoraPosse2" runat="server" class="texto form-control form-control-sm" MaxLength="10" OnTextChanged="txtHoraPosse2_TextChanged" AutoPostBack="True"></asp:TextBox>
                                    </div>

                                    <div class="col-5 gx-0">
                                        <asp:Label ID="lblLocalPosse" runat="server" class="tituloLabel">Local</asp:Label>
                                        <asp:TextBox ID="txtLocalPosse" runat="server" class="texto form-control form-control-sm" OnTextChanged="txtLocalPosse_TextChanged" AutoPostBack="True"></asp:TextBox> 
                                    </div>
                                </div>
                            </div>

                            <div class="col-12 gx-2 gy-2">
                                <asp:Button ID="btnAtaPosse" runat="server" CssClass="btn" Text="Imprimir Ata da Posse" OnClick="btnAtaPosse_Click" style="font-size: 14.5px !important; text-align:center; vertical-align:central;"/>
                            </div>
                        </div>
                    </div>
                </eo:PageView>

                <!-- Calendário das Reuniões Ordinárias -->
                <eo:PageView ID="Pageview11" runat="server" Width="1050px">
                    <div class="col-12 subtituloBG">
                        <h2 class="subtitulo" style="padding-top: .8rem;">Calendário das Reuniões Ordinárias</h2>
                    </div>

                    <div class="col-12">
                        <div class="row">
                            <div class="col-4 gx-2 gy-2">
                                <div class="row">
                                    <div class="col-12 ">
                                        <asp:Label ID="lblDataCal" runat="server" class="tituloLabel form-label col-form-label col-form-label-sm">Data</asp:Label>
                                        <asp:TextBox ID="txtDataCal" runat="server" class="texto form-control form-control-sm" MaxLength="10" OnTextChanged="txtDataCal_TextChanged" AutoPostBack="True"></asp:TextBox> 
                                    </div>
                                </div>
                            </div>

                            <div class="col-8 gx-2 gy-2">
                                <div class="row">
                                    <div class="col-3">
                                        <asp:Label ID="lblHoraCal" runat="server" class="tituloLabel">Horário Início</asp:Label>
                                        <asp:TextBox ID="txtHoraCal" runat="server" class="texto form-control form-control-sm" MaxLength="10" OnTextChanged="txtHoraCal_TextChanged" AutoPostBack="True"></asp:TextBox> 
                                    </div>
                                    <div class="col-3 gx-3">
                                        <asp:Label ID="lblHoraCal2" runat="server" class="tituloLabel">Horário Término</asp:Label>
                                        <asp:TextBox ID="txtHoraCal2" runat="server" class="texto form-control form-control-sm" MaxLength="10" OnTextChanged="txtHoraCal2_TextChanged" AutoPostBack="True"></asp:TextBox>
                                    </div>
                                    <div class="col-5 gx-0">
                                        <asp:Label ID="lblLocalCal" runat="server" class="tituloLabel">Local</asp:Label>
                                        <asp:TextBox ID="txtLocalCal" runat="server" class="texto form-control form-control-sm" OnTextChanged="txtLocalCal_TextChanged"></asp:TextBox> 
                                    </div>
                                </div>
                            </div>

                            <div class="col-12 gx-2 gy-2">
                                <div class="col-6 gx-2 gy-4">
                                    <asp:Button ID="btnRecalcular" runat="server" class="btn" Text="Recalcula Reuniões" OnClick="btnRecalcular_Click" AutoPostBack="True" style="font-size: 14.5px !important; text-align:center; vertical-align:central;"/>
                                    <asp:Button ID="btnExtraordinarias" runat="server" class="btn" Text="Extraordinárias" OnClick="btnExtraordinarias_Click" style="font-size: 14.5px !important; text-align:center; vertical-align:central; margin-left: 1%; width: 29%"/>
                                </div>      
                            </div>
                            <div class="col-12 gy-4 gx-2 mt-2">
                            <div class="row">
                                <div class="col-6 gy-1">
                                  <eo:Grid ID="grdReunioes" runat="server" FixedColumnCount="1" ColumnHeaderDividerOffset="6" 
                                    ColumnHeaderAscImage="00050403" ColumnHeaderDescImage="00050404"
                                    GridLines="Both" PageSize="500" KeyField="IdReuniao" CssClass="grid"
                                    ColumnHeaderHeight="30" ItemHeight="30" Width="440px" ClientSideOnItemCommand="OnItemCommand" ClientSideOnCellSelected="OnCellSelected" OnItemCommand="grdReunioes_ItemCommand">
                                    <ItemStyles>
                                        <eo:GridItemStyleSet>
                                            <ItemStyle CssText="background-color: #FAFAFA" />
                                            <ItemHoverStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500;  text-align: left; height: 30px !important; background-color: rgb(28, 148, 137, 0.32); color: #FFFFFF; padding: 0.3rem 0.3rem 0.3rem 0.5rem; " />
                                            <AlternatingItemStyle CssText="background-color: #F2F2F2;" />
                                            <AlternatingItemHoverStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500;  text-align: left; height: 30px !important; background-color: rgba(28, 148, 137, 0.32); color: #FFFFFF; padding: 0.3rem 0.3rem 0.3rem 0.5rem;" />
                                            <SelectedStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500;  text-align: left; height: 30px !important; background-color: rgba(28, 148, 137, 0.32); color: #FFFFFF; padding: 0.3rem 0.3rem 0.3rem 0.5rem;" />
                                            <CellStyle CssText="padding: 0.3rem 0.3rem 0.3rem 0.5rem; height: 30px !important;" />
                                        </eo:GridItemStyleSet>
                                    </ItemStyles>
                                    <ColumnHeaderTextStyle CssText="" />
                                    <ColumnHeaderStyle CssClass="tabelaC colunas tabelaIndicados"/>
                                    <Columns>
                                    <eo:ButtonColumn ButtonText="Editar" CommandName="Editar" Name="Editar" DataField="Editar" Width="60" ButtonType="LinkButton">
                                        <CellStyle CssText="text-align: center; height: 30px !important;" CssClass="tituloLabel editarReunioes"/>
                                     </eo:ButtonColumn>
                                    <eo:StaticColumn HeaderText="Reunião" AllowSort="True" 
                                        DataField="Reuniao" Name="Reuniao" ReadOnly="True" Width="180">
                                    <CellStyle CssText="font-family: 'UniviaPro'; font-size: 11.5px; font-weight: normal; color: #7D7B7B; text-align: left; height: 30px !important; " CssClass="celularIndicados"/>
                                    </eo:StaticColumn>
                                    <eo:StaticColumn HeaderText="Data" AllowSort="True" 
                                        DataField="Data" Name="Data" ReadOnly="True" Width="80">
                                    <CellStyle CssText="font-family: 'UniviaPro'; font-size: 11.5px; font-weight: normal; color: #7D7B7B; text-align: left; height: 30px !important; " CssClass="celularIndicados"/>
                                    </eo:StaticColumn>
                                 </Columns>
                                </eo:Grid> 
                                </div>
                                 <div class="col-6 gx-3 gy-2">
                                    
                                </div>
                            </div>       
                          </div>
                        </div>
                    </div>
                    <asp:Button ID="btnImpCal" class="btn" runat="server" Text="Imprimir Calendário das Reuniões Ordinárias" OnClick="btnImpCal_Click" style="font-size: 14.5px !important; text-align:center; vertical-align:central;"/>
                </eo:PageView>
            </eo:MultiPage>
        </div>
    </div>
    <eo:MsgBox ID="MsgBox1" runat="server" BackColor="#ffffff" ControlSkinID="None" HeaderHtml="Dialog Title" Height="192px" Width="345px" CssClass="card border border-1 p-2 text-center" IconUrl="Images/alerta_icon.svg">
        <HeaderStyleActive CssText="font-family: Ubuntu; font-size: 14px; font-weight: bold; color: #1C9489; text-align: center; padding: 5px;" />
        <ContentStyleActive CssText="font-family: Ubuntu; font-size: 14px; font-weight: bold; color: #7D7B7B; text-align: center; padding: 5px; width: 345px; height: 60px" />
        <FooterStyleActive CssText="width: 345px;" />
    </eo:MsgBox>
</asp:Content>