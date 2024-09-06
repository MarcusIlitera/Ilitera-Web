<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="True"
    Title="Ilitera.Net"  EnableEventValidation="false"  ValidateRequest="false"  
            CodeBehind="ListaEmpresas2.aspx.cs" Inherits="Ilitera.Net.ListaEmpresas2" %>

<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>

<asp:Content 
    ID="HeaderContent" 
    runat="server" 
    ContentPlaceHolderID="HeadContent">
    <title>Listagem de Empresas</title>
    <link href="css/forms.css" rel="stylesheet" type="text/css" />
    <link href="css/tabelas.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        /* geral */
        .alinhar {
            justify-content: center;
        }
        /* fontes */
        @font-face{
            font-family: "Univia Pro";
            src: url("/css/UniviaPro-Regular.otf");
        }
        @font-face{
            font-family: "Ubuntu";
            src: url("/css/Ubuntu-Regular.ttf");
        }
        /* títulos e textos */
        .tituloConteudo {
            font-family: 'Univia Pro';
            font-size: 16px;
            font-weight: 500;
            color: #1C9489;
            text-align: left;
        }
        .tNumeracao{
            margin-left: 2rem;
            top: 50%;
            left: 50%;
            transform: translate(1%, 50%);
        }
        .tAlinhar{
            margin-left: 2rem;
            transform: translate(2.6%, 20%);
        }
        .label {
            font-family: 'Ubuntu';
            font-weight: 400;
            color: #1C9489;           
        }
        /* botões */
        .btn {
            /*margin-left: 5px;
            font: 800 14px "Inter";
            color: white;
            padding: 8px;
            background-color: #5865f2;
            border-radius: 4px;
            border-color: #5865f2;*/
            width: 80px;
            height: 20px;
            font-family: 'Univia Pro';
            font-style: normal;
            font-weight: 500;
            font-size: 12px;
            /*text-align: center;*/
            color: #ffffff;
            background: linear-gradient(180deg, #48A79E 54.35%, #1C9489 54.36%);
            border-radius: 5px;
            border: none;
            padding: 0px;
            margin-left: 1rem;
        }
            .btn:hover {
                color: #ffffff;
                background: linear-gradient(180deg, #F2B988 53.35%, #F09E60 53.36%);
                border-radius: 5px;
            }
        /* alinhamento */
        .body-container {
            /*padding-left: 0px !important;*/
            /*padding-right: 0px !important;*/ 
            width: 100%;
            display: flex;
            justify-content: start;
            align-items: center;
        }
        .direita {
            display: flex;
            justify-content: end;
            align-items: end;
            margin: 0px;
        }
        .esquerda {
            display: flex;
            justify-content: start;
            align-items: start;
            margin: 0px;
            
        }
        @media screen and (max-width: 375px) {
            .direita{
            justify-content: center;
            align-items: center;
            margin: 0px;
            }
            .esquerda{
            justify-content: center;
            align-items: center;
            margin: 0px;
            }
        }
        .coteudoPagina {
            display: flex;
            flex-direction: column;
            height: 100vh;
        }
        .margemLinha {
            margin-bottom:20px;
        }
        .margemDataLaudo {
            margin-top: 40px;
            margin-bottom: 20px;
        }
        .margemBotoes {
            margin-bottom: 40px;
        }
        /* select */
        .wrapperSelect{
            display: flex;
            justify-content: center;
            align-items: center;
        }
        .select{
            max-width: 165px;
            height: 30px !important;
            font-family: 'Ubuntu';
            font-style: normal;
            font-weight: 500;
            color: #B0ABAB;
            border: 1px solid #B0ABAB;
            border-radius: 4px;
            outline: none;
        }
        /* imagens */
        .image2 {
            width: 2rem;
            left: 50%;
            transform: translate(-45%, 0);
        }
        .PPRA{
            font-family:"Univia Pro";
            font-size: 16px;
            color: #1C9489;
            text-align: left;
            max-width: 1190px;
            height: 39px;
        } 
        .final {
            display: flex;
            justify-content: end;
            align-items: center;
        }
        .borda {
            height: 50%;
            border-right: 4px solid #E98035;
        }
        .margens{
          margin: 0 25px 0 25px;
        }
        .botoesCentro {
            display: flex;
            justify-content: center;
            align-items: center;
            text-align: center;
        }
        .centro {
            display: flex;
            justify-content: center;
            align-items: center;
        }
        .centralizar {
            display: flex;
            justify-content: center;
            align-items: center;
        }
         /* tabela */
        .tabela {
            margin-top: 1rem;
            justify-content: center;
            text-align: left;
            /*border: 1px solid #ccc;*/
            border-collapse: collapse;
            table-layout: fixed;
            width: 75%;
        }
        .headerTabela {
            background-color: #D9D9D9;
            font-family: "Ubuntu";
            font-size: 0.75rem;
            color: #1C9489;
            font-weight: 800;
            text-align: left;
        }
        .linhaTabela1 {
            background-color: #f1f1f1;
            font-family: "Ubuntu";
            font-size: 0.75rem;
            color: #B0ABAB;
        }
            .linhaTabela1:hover {
                background-color: #c4c4c4;
                color: #fff;
            }
            
        .linhaTabela2 {
            background-color: #F5F5F5;
            font-family: "Ubuntu";
            font-size: 0.75rem;
            color: #B0ABAB;
        }
            .linhaTabela2:hover {
                 background-color: #c4c4c4;
                 color: #fff;
            }
        .paddingAdicional {
            padding-right: 20rem;
        }
        li {
            border-radius: 3px;
            padding: 15px 30px !important;
            display: flex;
            justify-content: space-between;
            margin-bottom: 0px;
          }
          .table-header {
            background-color: #C4C4C4;
            text-transform: uppercase;
            letter-spacing: 0.03em;
          }
          .table-row {
            /*box-shadow: 0px 0px 9px 0px rgba(0,0,0,0.1);*/
          }
          .colTabela1 {
            flex-basis: 30%;
          }
          .colTabela2 {
            flex-basis: 20%;
          }
          .colTabela3{
              flex-basis: 50%;
          }
        @media screen and (max-width: 800px) {
            .table-header {
                display: none;
            }
            .table-row {
            }
            li {
                display: block;
            }
            .col {
                flex-basis: 100%;
            }
            .col {
                display: flex;
                padding: 10px 0;
            }
                .col:before {
                    color: #1C9489;
                    font-family: "Ubuntu";
                    font-size: 0.75rem;
                    font-weight: 800;
                    padding-right: 10px;
                    content: attr(data-label);
                    flex-basis: 50%;
                    text-align: right;
                }
        }
       </style>
   

</asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
          <script type="text/javascript">
    function OnItemCommand(grid, itemIndex, colIndex, commandName) {
        
        //grid.raiseItemCommandEvent(itemIndex, commandName);
        grid.raiseItemCommandEvent(itemIndex, colIndex.toString());
    }
          </script>
    <div runat="server">   

        <div runat="server" class="row align-items-center" >
            
            <%-- FILTROS --%>

            <div class="container-fluid d-flex justify-content-center mb-4 ms-5 ps-4">
                <div class="row" style="width: 100%;">
                    <div runat="server" class="col-md-3 mb-1">
                        <asp:Label ID="Label3" runat="server" CssClass="tFiltros form-label"  
                        Text="Nome da Empresa" ></asp:Label>
                        <div class="d-flex flex-row">
                            <asp:TextBox ID="txt_Empresa" runat="server" AutoPostBack="True" 
                                   CausesValidation="True" Height="20px" MaxLength="50" 
                                    Width="150px" CssClass="form-control"
                            ></asp:TextBox>
                            <asp:Button ID="cmd_Busca_Emp" runat="server" 
                            Text="Busca" 
                            UseSubmitBehavior="False" CssClass="btn" onclick="cmd_Busca_Emp_Click"  />
                        </div>
                    </div>

                <div runat="server" class="col-md-3 mb-1">
                    <asp:Label ID="Label2" runat="server" CssClass="tFiltros form-label"  
                    Text="Nome do Empregado" ></asp:Label>
                        <div class="d-flex flex-row">
                            <asp:TextBox ID="txt_Nome" runat="server" AutoPostBack="True" 
                                CausesValidation="True" Height="20px" MaxLength="50" 
                                Width="150px" CssClass="form-control" 
                            ></asp:TextBox>
                            <asp:Button ID="cmd_Busca" runat="server" 
                            Text="Busca" 
                            UseSubmitBehavior="False" CssClass="btn" onclick="cmd_Busca_Click"  />
                        </div>
                    </div>

                <div runat="server" class="col-md-3 mb-1">
                    <asp:Label ID="Label4" runat="server" CssClass="tFiltros form-label" 
                        Text="Matrícula" ></asp:Label>
                    <div class="d-flex flex-row">
                        <asp:TextBox ID="txt_Matricula" runat="server" AutoPostBack="True" 
                        CausesValidation="True" Height="20px" MaxLength="50" Width="150px" CssClass="form-control"></asp:TextBox>
                        <asp:Button ID="cmd_Busca_Matr" runat="server" Text="Busca" UseSubmitBehavior="False" CssClass="btn" onclick="cmd_Busca_Click"  />
                    </div>
                </div> 

                <div runat="server" class="col-md-2 mb-1">
                        <asp:Label ID="lblGrupoEmpresa" runat="server" CssClass="tFiltros form-label"  
                        Text="Grupo Empresa" ></asp:Label>
                        <asp:DropDownList ID="cmbGrupoEmpresa" runat="server" AutoPostBack="True" CssClass="form-control" Height="20px" Width="150px">
                        </asp:DropDownList>
                </div>
                    
                    <div class="col-md-3 mt-3 form-check form-check-inline me-0" style="padding-left: 0rem!important;">
                        <div class="row">
                            <div class="col-12">
                                <asp:Label ID="Label5" runat="server" CssClass="tFiltros form-label pr-2" Text="Ordem Grid de Empresas"></asp:Label>
                            </div>
                            <div class="col-4 ms-3">
                                <asp:RadioButton ID="rd_Ascendente" runat="server" GroupName="Ordem" CssClass="form-check-label pr-2 pt-5 pFiltros" Text="Ascendente" 
                                    AutoPostBack="True"  Checked="True" oncheckedchanged="rd_Ascendente_CheckedChanged"  />
                            </div>
                            <div class="col-4">
                                <asp:RadioButton ID="rd_Descendente" runat="server" CssClass="pFiltros pr-2 pt-5" GroupName="Ordem" 
                                    Text="Descendente" AutoPostBack="True" oncheckedchanged="rd_Descendente_CheckedChanged"  />
                            </div>
                        </div>
                    </div> 

                    <div class="col-md-3 mt-3 form-check form-check-inline" style="padding-left: 0rem!important;">
                        <div class="row">
                            <div class="col-12">
                                <asp:Label ID="Label1" runat="server" CssClass="tFiltros pr-2" Text="Número de colaboradores"></asp:Label>
                            </div>
                            <div class="col-3 ms-3">
                                <asp:RadioButton ID="rd_Todos" runat="server" CssClass="pFiltros form-check-label pr-2 pt-5" GroupName="Func" Text="Todos" AutoPostBack="True" oncheckedchanged="rd_Todos_CheckedChanged"  />
                            </div>
                            <div class="col-3">
                                <asp:RadioButton ID="rd_Ativos" runat="server" GroupName="Func" CssClass=" pFiltros form-check-label pr-2 pt-5" Text="Ativos" AutoPostBack="True"  Checked="True" oncheckedchanged="rd_Ativos_CheckedChanged"  />
                            </div>
                            <div class="col-3">
                                <asp:RadioButton ID="rd_Inativos" runat="server" CssClass="pFiltros form-check-label pt-5" GroupName="Func" Text="Inativos" AutoPostBack="True"  oncheckedchanged="rd_Inativos_CheckedChanged" />
                            </div>
                        </div>
                    </div>
                </div>
                
            </div>

            <br /><br />

            <%-- TABELA --%>
                  <div class="container-fluid d-flex ms-5 ps-4" style="padding-left: 60px;">
                        <eo:Grid ID="grd_Empresas" runat="server" ColumnHeaderAscImage="00050403" 
                        ColumnHeaderDescImage="00050404" Width="800"
                        FixedColumnCount="1" GridLines="Both" ColumnHeaderDividerOffset="6" 
                        KeyField="IdJuridica" PageSize="500" CssClass="grid" Height="550"  OnItemCommand="grd_Empresas_ItemCommand"  ColumnHeaderHeight="30" 
        ClientSideOnItemCommand="OnItemCommand" ItemHeight="30" >
                    <ItemStyles>
                        <eo:GridItemStyleSet>
                            <ItemStyle CssText="background-color: #FAFAFA;" />
                            <ItemHoverStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500;  text-align: left; height: 30px !important; background: rgba(28, 148, 137, 0.80); color: #FFFFFF; padding: 0.3rem 0.3rem 0.3rem 0.5rem; " />
                            <AlternatingItemStyle CssText="background-color: #F2F2F2;" />
                            <AlternatingItemHoverStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500;  text-align: left; height: 30px !important; background: rgba(28, 148, 137, 0.80); color: #FFFFFF; padding: 0.3rem 0.3rem 0.3rem 0.5rem;" />
                            <SelectedStyle CssText="border-bottom-style:solid;border-left-style:solid;border-right-style:solid;border-top-style:solid;" />
                            <CellStyle CssText="padding: 0.3rem 0.3rem 0.3rem 0.5rem; height: 30px !important;" />
                        </eo:GridItemStyleSet>
                    </ItemStyles>
                    <ColumnHeaderStyle CssClass="tabelaC colunas" />
                    <Columns>
                        <eo:StaticColumn HeaderText="Nome Resumido" AllowSort="True" 
                            DataField="NomeAbreviado" Name="NomeAbreviado" ReadOnly="True" 
                            SortOrder="Ascending" Text="" Width="300">
                            <CellStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500; color: #7D7B7B !important; text-align: left; height: 30px !important;" />
                        </eo:StaticColumn>
                        <eo:StaticColumn HeaderText="Empregados" AllowSort="True" 
                            DataField="QtdEmpregados" Name="Qtde.Empregados" ReadOnly="True" Width="150">
                            <CellStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500; color: #7D7B7B !important; text-align: left; height: 30px !important;" />
                        </eo:StaticColumn>
                        <eo:StaticColumn AllowSort="True" DataField="DataCadastro" 
                            HeaderText="Data de Cadastro" Name="Data de Cadastro" ReadOnly="True" 
                            DataFormat="{0:dd/MM/yyyy}" DataType="DateTime" Width="185">
                            <CellStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500; color: #7D7B7B !important; text-align: left; height: 30px !important;" />
                        </eo:StaticColumn>
                        <eo:ButtonColumn ButtonText="Selecionar" 
                            Name="Selecionar" Width="190">
                            <CellStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500; color: #1C9489 !important; text-align: left; height: 30px !important;" />
                        </eo:ButtonColumn>
                        <eo:ButtonColumn ButtonText="Dados Cadastrais" Name="Cadastro" Width="190">
                            <CellStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500; color: #1C9489 !important; text-align: left; height: 30px !important;" />
                        </eo:ButtonColumn>
                    </Columns>
                    <ColumnTemplates>
                        <eo:TextBoxColumn>
                            <TextBoxStyle CssText="BORDER-RIGHT: #7f9db9 1px solid; PADDING-RIGHT: 2px; BORDER-TOP: #7f9db9 1px solid; PADDING-LEFT: 2px; FONT-SIZE: 8.75pt; PADDING-BOTTOM: 1px; MARGIN: 0px; BORDER-LEFT: #7f9db9 1px solid; PADDING-TOP: 2px; BORDER-BOTTOM: #7f9db9 1px solid; FONT-FAMILY: Tahoma" />
                        </eo:TextBoxColumn>
                        <eo:DateTimeColumn>
                            <DatePicker runat="server" ControlSkinID="None" DayCellWidth="19" 
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
                            <MaskedEdit runat="server" ControlSkinID="None" 
                                TextBoxStyle-CssText="BORDER-RIGHT: #7f9db9 1px solid; PADDING-RIGHT: 2px; BORDER-TOP: #7f9db9 1px solid; PADDING-LEFT: 2px; PADDING-BOTTOM: 1px; MARGIN: 0px; BORDER-LEFT: #7f9db9 1px solid; PADDING-TOP: 2px; BORDER-BOTTOM: #7f9db9 1px solid; font-family:Courier New;font-size:8pt;">
                            </MaskedEdit>
                        </eo:MaskedEditColumn>
                    </ColumnTemplates>
                    <FooterStyle CssText="padding-bottom:4px;padding-left:4px;padding-right:4px;padding-top:4px;" />
                </eo:Grid>
           

            <br />
            <br />
                <eo:Grid ID="grd_EmpresasNome" runat="server" ColumnHeaderAscImage="00050403" 
                        ColumnHeaderDescImage="00050404" Width="1000"
                        FixedColumnCount="1" GridLines="Both" ColumnHeaderDividerOffset="6" 
                        KeyField="IdJuridica" PageSize="500" CssClass="grid" Height="550" 
                     OnItemCommand="grd_Empresas_Nome_ItemCommand"  ColumnHeaderHeight="30" 
                     ClientSideOnItemCommand="OnItemCommand" ItemHeight="30" Visible="False"  >
                    <ItemStyles>
                        <eo:GridItemStyleSet>
                            <ItemStyle CssText="background-color: #FAFAFA;" />
                            <ItemHoverStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500;  text-align: left; height: 30px !important; background: rgba(28, 148, 137, 0.32); color: #FFFFFF; padding: 0.3rem;" />
                            <AlternatingItemStyle CssText="background-color: #F2F2F2;" />
                            <AlternatingItemHoverStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500;  text-align: left; height: 30px important; background: rgba(28, 148, 137, 0.32); color: #FFFFFF; padding: 0.3rem;" />
                            <SelectedStyle CssText="border-bottom-style:solid;border-left-style:solid;border-right-style:solid;border-top-style:solid;" />
                            <CellStyle CssText="padding: 0.3rem 0.3rem 0.3rem 0.5rem; height: 30px !important;" />
                        </eo:GridItemStyleSet>
                    </ItemStyles>
                    <ColumnHeaderStyle CssClass="tabelaC colunas" />
                    <Columns>
                <eo:StaticColumn HeaderText="Empresa/Obra" AllowSort="True" 
                    DataField="NomeAbreviado" Name="NomeAbreviado" ReadOnly="True" 
                    SortOrder="Ascending" Text="" Width="290">
                    <CellStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500; color: #7D7B7B; text-align: left; height: 30px !important; " />
                </eo:StaticColumn>
                <eo:StaticColumn HeaderText="Colaborador" AllowSort="True" 
                    DataField="Colaborador" Name="Colaborador" ReadOnly="True" 
                    Width="350">
                   <CellStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500; color: #7D7B7B; text-align: left; height: 30px !important; " />
                </eo:StaticColumn>
                <eo:StaticColumn HeaderText="Matrícula" AllowSort="True" 
                    DataField="tCod_Empr" Name="tCod_Empr" ReadOnly="True" 
                    Width="180">
                    <CellStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500; color: #7D7B7B; text-align: left; height: 30px !important; " />
                </eo:StaticColumn>               
                <eo:StaticColumn HeaderText="nId_Empregado" AllowSort="True" 
                    DataField="nId_Empregado" Name="nID_Empregado" ReadOnly="True" 
                    Width="0">
                    <CellStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500; color: #7D7B7B; text-align: left; height: 30px !important; " />
                </eo:StaticColumn>    
                <eo:ButtonColumn ButtonText="Selecionar" 
                    Name="Selecionar">
                    <CellStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500; color: #7D7B7B; text-align: left; height: 30px !important; " />
                </eo:ButtonColumn>               
                    </Columns>
                    <ColumnTemplates>
                        <eo:TextBoxColumn>
                            <TextBoxStyle CssText="BORDER-RIGHT: #7f9db9 1px solid; PADDING-RIGHT: 2px; BORDER-TOP: #7f9db9 1px solid; PADDING-LEFT: 2px; FONT-SIZE: 8.75pt; PADDING-BOTTOM: 1px; MARGIN: 0px; BORDER-LEFT: #7f9db9 1px solid; PADDING-TOP: 2px; BORDER-BOTTOM: #7f9db9 1px solid; FONT-FAMILY: Tahoma" />
                        </eo:TextBoxColumn>
                        <eo:DateTimeColumn>
                            <DatePicker runat="server" ControlSkinID="None" DayCellWidth="19" 
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
                            <MaskedEdit runat="server" ControlSkinID="None" 
                                TextBoxStyle-CssText="BORDER-RIGHT: #7f9db9 1px solid; PADDING-RIGHT: 2px; BORDER-TOP: #7f9db9 1px solid; PADDING-LEFT: 2px; PADDING-BOTTOM: 1px; MARGIN: 0px; BORDER-LEFT: #7f9db9 1px solid; PADDING-TOP: 2px; BORDER-BOTTOM: #7f9db9 1px solid; font-family:Courier New;font-size:8pt;">
                            </MaskedEdit>
                        </eo:MaskedEditColumn>
                    </ColumnTemplates>
                    <FooterStyle CssText="padding-bottom:4px;padding-left:4px;padding-right:4px;padding-top:4px;" />
                </eo:Grid>

                <eo:MsgBox ID="MsgBox1" runat="server" CssClass="tituloLabel card border border-1 text-center" BackColor="#ffffff" ControlSkinID="None" 
                HeaderHtml="Dialog Title" Height="192px" Width="345px">
                <HeaderStyleActive CssText="font-family: Ubuntu; font-size: 14px; font-weight: bold; color: #1C9489; text-align: center; border: #7D7B7B 1px solid; padding: 5px;" />
                <ContentStyleActive CssText="font-family: Ubuntu; font-size: 14px; font-weight: bold; color: #1C9489; text-align: center; border: #7D7B7B 1px solid; padding: 5px;" />
                <FooterStyleActive CssText="btn" />
                </eo:MsgBox>
            </div>

        </div>
    </div>

          <asp:Image ID="Img_Sistema" runat="server" Height="1" Width="1" ImageUrl="../Images/ilinet.svg" />

    
    
              <div class="row cardNovo" style="margin-top: 3rem;">
                    <div class="col-12 text-center">
                        &nbsp;<asp:Label ID="lbl_Sistema" Height="1" Width="1" runat="server" Font-Bold="True" Font-Size="Large" Text=""></asp:Label>                        
                        <br />
                        <asp:hyperlink runat="server" Height="1" Width="1" CssClass="btnCard" Text="" NavigateUrl="https://www.ilitera.net.br/essence2" Target="_blank" ID="lnk_Sistema"></asp:hyperlink>
                    </div>
                </div>
</asp:Content>


