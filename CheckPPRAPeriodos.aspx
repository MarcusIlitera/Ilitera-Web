<%@ Page Title="Ilitera.Net" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="True"    CodeBehind="CheckPPRAPeriodos.aspx.cs" Inherits="Ilitera.Net.CheckPPRAPeriodos" %>

<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>

<asp:Content 
    ID="HeaderContent" 
    runat="server" 
    ContentPlaceHolderID="HeadContent">
    <link href="css/tabelas.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        /* geral */
        .margem {
            margin-right: 90px;
            margin-left: 10px;
        }
        /* fontes */
        @font-face{
            font-family: "Univia Pro";
            src: url("css/UniviaPro-Regular.otf");
        }
        @font-face{
            font-family: "Ubuntu";
            src: url("css/Ubuntu-Regular.ttf");
        }
        /* títulos e textos */
        .tituloConteudo {
            font-family: 'Univia Pro';
            font-size: 16px;
            font-weight: 500;
            font-variant: normal !important;
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
            font-size: 12px;
            font-family: 'Ubuntu';
            font-weight: 500;
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
            width: 260px;
            height: 32px;
            font-family: 'Univia Pro';
            font-style: normal;
            font-weight: 700;
            font-size: 12px;
            /*text-align: center;*/
            color: #ffffff;
            background: linear-gradient(180deg, #48A79E 54.35%, #1C9489 54.36%);
            border-radius: 5px;
            border: none;
            margin: 10px;
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
            max-width: 1190px;
            display: flex;
            justify-content: center;
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
        }
        .margemDataLaudo {
            margin-top: 9px;
            margin-bottom: 30px;
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
            height: 30px;
            font-family: 'Ubuntu';
            font-style: normal;
            font-weight: 500;
            font-size: 12px;
            color: #B0ABAB;
            border: 1px solid #B0ABAB;
            border-radius: 4px;
            outline: none;
        }
        /* imagens */
        .image2 {
            width: 2rem;
        }
        .linha2{
            width: 100%;
            text-align: center;
            padding: 0.5rem 0.5rem 0 1rem;
            background-color: #F1F1F1;
            border-radius: 4px;
            padding: 0 1rem 0 0;
        }
        .linha3{
            width: 100%;
            text-align: center;
            padding: 0.5rem 0.5rem 0 1rem;
            background-color: #F1F1F1;
            border-radius: 4px;
            margin-bottom: 30px;
            padding: 0 1rem 0 0;
        }
        .PPRA{
            font-family:"Univia Pro";
            font-size: 14px;
            color: #1C9489;
            text-align: left;
            max-width: 100%;
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
        .remendo {
            width: 100%;
        }
        .margens{
          margin: 0 60px 0 65px;
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
            margin: 0;
            text-align: left;
            /*border: 1px solid #ccc;*/
            border-collapse: collapse;
            padding: 0 1rem 1rem 3.5rem;
            width: 100%;
            table-layout: fixed;
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
            padding: 15px 30px;
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
            flex-basis: 25%;
          }
          .colTabela2 {
            flex-basis: 75%;
          }
        @media screen and (max-width: 767px) {
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
    <div runat="server" style="width: fit-content;" class="container-fluid d-flex ms-5 ps-4">
        <%-- PPRA --%>
        <div runat="server" class="row gx-3 gy-2">
            <div class="col-11">
                <div>
            <%-- Data do Laudo --%>
            <div runat="server" class="col margemDataLaudo wrapperSelect">
                <fieldset>
                    <asp:Label ID="lblPPRA" runat="server" Text="Data do Laudo" Style="align-content: center;" CssClass="margemDataLaudo label"></asp:Label>
                    <asp:Literal runat="server" ID="Literal17"><br /></asp:Literal>
                    <asp:DropDownList ID="ddlLaudos2" runat="server" CssClass="select" Style="text-align: center;" onselectionchanged="ddlLaudos2_SelectionChanged" 
                            onselectedindexchanged="ddlLaudos2_SelectedIndexChanged">
                        <asp:ListItem Selected="True" Value="0">Selecionar Data</asp:ListItem>
                    </asp:DropDownList>
                </fieldset>
            </div>
            <%-- Botões --%>
            <div runat="server" class="row centralizar margemBotoes">
                    <div runat="server" class="row centralizar botoes">
                        <asp:Button ID="btnIntroducao" runat="server" CssClass="btn" Text="PPRA - Introdução" OnClick="lkbIntroducao_Click" />
                        <asp:Button ID="btnDocumentoBase" runat="server" CssClass="btn" Text="PPRA - Documento Base" OnClick="lkbDocumentoBase_Click" />
                    </div>
                    <div runat="server" class="row centralizar botoes">
                        <asp:Button ID="btnPlanilha" runat="server" CssClass="btn" Text="PPRA - Planilha" OnClick="lkbPlanilha_Click" />
                        <asp:Button ID="btnQuadroEPI" runat="server" CssClass="btn" Text="PPRA - Quadro EPI" OnClick="lkbQuadroEPI_Click" />
                    </div>
                    <div runat="server" class="row centralizar botoes">
                        <asp:Button ID="btnEmpregado" runat="server" CssClass="btn" Text="Empregado" OnClick="lkbEmpregado_Click" />
                        <asp:Button ID="btnPPRA" runat="server" CssClass="btn" Text="PPRA Completo" OnClick="lkbPPRA_Click" />
                    </div>
                    <div runat="server" class="row centralizar botoes">
                        <asp:Button ID="Button1" runat="server" CssClass="btn" Text="PPRA Completo com Índice" OnClick="lkbPPRAcomIndice_Click" />
                        <asp:Button ID="btnEPI" runat="server" CssClass="btn" Text="Estimativa de uso de EPI" OnClick="lkbEPI_Click" />
                    </div>
                    <div runat="server" class="row centralizar botoes">
                        <asp:Button ID="btnPCA" runat="server" CssClass="btn" Text="PCA" OnClick="lkbPCA_Click" />
                        <asp:Button ID="btnPPR" runat="server" CssClass="btn" Text="PPR" OnClick="lkbPPR_Click" />
                    </div>
                </div> 
                </div>

        <%-- Certificado com Assinatura Digital --%>
        <div runat="server" class="row gx-3 gy-2">
            <%-- Título --%>
            <div runat="server" class="linha3 margemLinha">
                <asp:Image ID="menu1" runat="server" AlternateText="2_menu" ImageAlign="Left" ImageUrl="Images/numero_1.png" CssClass="image2"/>
                <h2 id="lblPPRA0" class="tituloConteudo tNumeracao">Certificado com Assinatura Digital</h2>
            </div>

            <%-- Tabela --%>
         <div class="container-fluid d-flex justify-content-center text-center gx-3 gy-2">
            <eo:Grid ID="grd_Clinicos" runat="server" FixedColumnCount="1" GridLines="Both" ColumnHeaderDividerOffset="6"
                KeyField="IdRepositorio" PageSize="500" CssClass="grid" ColumnHeaderHeight="28" ItemHeight="28" >
                <ItemStyles>
                        <eo:GridItemStyleSet>
                            <ItemStyle CssText="background-color: #FAFAFA;" />
                            <ItemHoverStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500;  text-align: left; height: 30px !important; background: rgba(28, 148, 137, 0.32); color: #FFFFFF; padding: 0.3rem 0.3rem 0.3rem 0.5rem; " />
                            <AlternatingItemStyle CssText="background-color: #F2F2F2;" />
                            <AlternatingItemHoverStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500;  text-align: left; height: 30px !important; background: rgba(28, 148, 137, 0.32); color: #FFFFFF; padding: 0.3rem 0.3rem 0.3rem 0.5rem;" />
                            <SelectedStyle CssText="border-bottom-style:solid;border-left-style:solid;border-right-style:solid;border-top-style:solid;" />
                            <CellStyle CssText="padding: 0.3rem 0.3rem 0.3rem 0.5rem; height: 30px !important;" />
                        </eo:GridItemStyleSet>
                    </ItemStyles>
                <ColumnHeaderStyle CssClass="tabelaC colunas" />
                <Columns>
                    <eo:StaticColumn AllowSort="True" DataField="DataHora" HeaderText="Data do Documento" Name="DataHora" ReadOnly="True" SortOrder="Ascending" Text="" Width="200" DataFormat="{0:dd/MM/yyyy}" DataType="DateTime">
                        <CellStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500; color: #7D7B7B !important; text-align: left; height: 35px !important; padding: 0.3rem 0.3rem 0.3rem 0.5rem; height: 35px !important;" />
                    </eo:StaticColumn>
                    <eo:StaticColumn AllowSort="True" DataField="Descricao" HeaderText="Descrição" Name="Descricao" ReadOnly="True" Width="450">
                        <CellStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500; color: #7D7B7B !important; text-align: left; height: 35px !important; padding: 0.3rem 0.3rem 0.3rem 0.5rem; height: 35px !important;" />
                    </eo:StaticColumn>
                    <eo:StaticColumn AllowSort="True" DataField="Anexo" HeaderText="Anexo" Name="Anexo" ReadOnly="True" Width="1">
                       <CellStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500; color: #7D7B7B !important; text-align: left; height: 35px !important; padding: 0.3rem 0.3rem 0.3rem 0.5rem; height: 35px !important;" />
                    </eo:StaticColumn>
                    <eo:ButtonColumn ButtonText="Visualizar" Name="Selecionar" Width="90">
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
        </div>

        </div>
        </div>
        
      </div>
    </div>

    <eo:MsgBox ID="MsgBox1" runat="server" BackColor="#47729F" ControlSkinID="None" 
        HeaderHtml="Dialog Title" Height="100px" Width="168px">
        <HeaderStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #ffbf00 3px solid; padding-left: 4px; font-weight: bold; font-size: 11px; padding-bottom: 2px; color: white; padding-top: 2px; border-bottom: #22456a 1px solid; font-family: verdana" />
        <ContentStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #7d97b6 1px solid; padding-left: 4px; border-left-width: 1px; font-size: 11px; border-left-color: #728eb8; padding-bottom: 4px; color: white; padding-top: 4px; border-bottom: #22456a 1px solid; font-family: verdana" />
        <FooterStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #7d97b6 1px solid; padding-left: 4px; border-left-width: 1px; font-size: 11px; border-left-color: #728eb8; padding-bottom: 4px; color: white; padding-top: 4px; border-bottom: #22456a 1px solid; font-family: verdana" />
    </eo:MsgBox>
</asp:Content>