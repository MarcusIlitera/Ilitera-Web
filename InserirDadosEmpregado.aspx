<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="True" CodeBehind="InserirDadosEmpregado.aspx.cs"
    Inherits="Ilitera.Net.InserirDadosEmpregado" Title="Ilitera.Net"  EnableEventValidation="false"  ValidateRequest="false"  %>
 
<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
     <title>Inserir Colaborador</title>
    <link href="css/forms.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .texto {
            color: #B0ABAB;
            font-size: 12px;
            font-family: 'Ubuntu';
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
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div class="container-fluid d-flex justify-content-center mb-4 ms-5 ps-4">
        <div class="row gx-2 gy-2" style="width: 100%;">

            <%-- PRIMEIRO CONTAINER --%>

            <div class="col-11 subtituloBG" style="padding-left: 0 !important;">
                <asp:Image ID="menu1" runat="server" AlternateText="2_menu" ImageAlign="Left" ImageUrl="Images/numero_1.png" CssClass="image1"/>
                <h2 class="subtitulo">Dados Cadastrais</h2>
            </div>

            <%-- LINHA UM --%>
            <div class="col-12">
                <div class="row">
                    <div class="col-md-3 gx-2 gy-2">
                        <fieldset>
                            <asp:Label ID="lblNome" runat="server" Text="Nome" class="tituloLabel form-label col-form-label col-form-label-sm" Style="text-align: left;"></asp:Label>
                            <asp:Literal runat="server"><br /></asp:Literal>
                            <asp:textbox ID="txtNomeEmpregado" runat="server" class="texto form-control form-control-sm" Style="text-align: left;"></asp:textbox>
                        </fieldset>
                   </div>

                    <div class="col-md-2 gx-2 gy-2">
                        <fieldset>
                            <asp:Label ID="lblNascimento" runat="server" Text="Nascimento" class="tituloLabel form-label col-form-label col-form-label-sm" Style="text-align: left;"></asp:Label>
                            <asp:Literal runat="server"><br /></asp:Literal>
                            <asp:textbox ID="txtDataNascimento" runat="server" class="texto form-control form-control-sm" type="date" Style="text-align: left;"></asp:textbox>
                        </fieldset>
                   </div>
          
                    <div class="col-md-2 gx-2 gy-2">
                        <fieldset>
                            <asp:Label ID="lblSexo" runat="server" Text="Sexo" class="tituloLabel form-label col-form-label col-form-label-sm" Style="text-align: left;"></asp:Label>
                            <asp:Literal runat="server"><br /></asp:Literal>
                            <asp:DropDownList ID="cbSexo" runat="server" class="texto form-select form-select-sm" Style="text-align: left;">
                                <asp:ListItem Selected="True">-</asp:ListItem>
                                <asp:ListItem Value="M">Masculino</asp:ListItem>
                                <asp:ListItem Value="F">Feminino</asp:ListItem>
                            </asp:DropDownList>
                        </fieldset>
                   </div>

                  <div class="col-md-1 gx-2 gy-2">
                    <fieldset>
                        <asp:Label ID="lblCTPS_Num" runat="server" Text="CTPS" CssClass="tituloLabel form-label col-form-label col-form-label-sm"></asp:Label>
                        <asp:Literal runat="server"><br /></asp:Literal>
                        <asp:textbox ID="txtCTPS_Num" runat="server" CssClass="texto form-control form-control-sm"></asp:textbox>
                    </fieldset>
                  </div>

            
                  <div class="col-md-1 gx-2 gy-2">
                      <fieldset>
                        <asp:Label ID="lblCTPS_SERIE" runat="server" Text="Série" class="tituloLabel form-label col-form-label col-form-label-sm" Style="text-align: left;"></asp:Label>
                        <asp:Literal runat="server"><br /></asp:Literal>
                        <asp:textbox ID="txtCTPS_Serie" runat="server" class="texto form-control form-control-sm" Style="text-align: left;"></asp:textbox>
                    </fieldset>
                  </div>

                  <div class="col-md-1 gx-2 gy-2">
                    <fieldset>
                        <asp:Label ID="lblCTPS_UF" runat="server" Text="UF" class="tituloLabel form-label col-form-label col-form-label-sm" Style="text-align: left;"></asp:Label>
                        <asp:Literal runat="server"><br /></asp:Literal>
                        <asp:textbox ID="txtCTPS_UF" runat="server" class="texto form-control form-control-sm" Style="text-align: left;"></asp:textbox>
                    </fieldset>
                  </div>

                  <div class="col-md-1 gx-2 gy-2">
                    <fieldset>
                        <asp:Label ID="lblDataDemissao" runat="server" Text="Admissão" class="tituloLabel form-label col-form-label col-form-label-sm" Style="text-align: left;"></asp:Label>
                        <asp:Literal runat="server"><br /></asp:Literal>
                        <asp:textbox ID="txtDataAdmissao" runat="server" class="texto form-control form-control-sm" type="date" Style="text-align: left;"></asp:textbox>
                    </fieldset>
                  </div>
                </div>
            </div>

            <%-- LINHA DOIS --%>
            
           <div class="col-12">
               <div class="row">
                   <div class="col-md-1 gx-2 gy-2">
                        <fieldset>
                            <asp:Label ID="lblDemissao" runat="server" Text="Demissão" class="tituloLabel form-label col-form-label col-form-label-sm" Style="text-align: left;"></asp:Label>
                            <asp:Literal runat="server"><br /></asp:Literal>
                            <asp:textbox ID="txtDataDemissao" runat="server" class="texto form-control form-control-sm" type="date" Style="text-align: left;"></asp:textbox>
                        </fieldset>
                      </div>

                       <div class="col-md-2 gx-2 gy-2">
                        <fieldset>
                            <asp:Label ID="lblApelido" runat="server" Text="Apelido" class="tituloLabel form-label col-form-label col-form-label-sm" Style="text-align: left;"></asp:Label>
                            <asp:Literal runat="server"><br /></asp:Literal>
                            <asp:textbox ID="txtApelidoEmpregado" runat="server" class="texto form-control form-control-sm" Style="text-align: left;"></asp:textbox>
                        </fieldset>
                      </div>

                       <div class="col-md-2 gx-2 gy-2">
                            <fieldset>
                                <asp:Label ID="lblMatricula" runat="server" Text="RE(Matrícula)" class="tituloLabel form-label col-form-label col-form-label-sm" Style="text-align: left;"></asp:Label>
                                <asp:Literal runat="server"><br /></asp:Literal>
                                <asp:textbox ID="txtMatricula" runat="server" class="texto form-control form-control-sm" Style="text-align: left;"></asp:textbox>
                            </fieldset>
                       </div>

                      <div class="col-md-2 gx-2 gy-2">
                        <fieldset>
                            <asp:Label ID="lblPISPASEP" runat="server" Text="PIS/PASEP" class="tituloLabel form-label col-form-label col-form-label-sm" Style="text-align: left;"></asp:Label>
                            <asp:Literal runat="server"><br /></asp:Literal>
                            <asp:textbox ID="txtPISPASEP" runat="server" class="texto form-control form-control-sm" Style="text-align: left;"></asp:textbox>
                        </fieldset>
                      </div>

                      <div class="col-md-2 gx-2 gy-2">
                        <fieldset>
                            <asp:Label ID="lblCPF" runat="server" Text="CPF" CssClass="tituloLabel form-label col-form-label col-form-label-sm"></asp:Label>
                            <asp:Literal runat="server"><br /></asp:Literal>
                            <asp:textbox ID="txtCPF" runat="server" CssClass="texto form-control form-control-sm" oninput="this.value = this.value.replace(/[^0-9]/g, '').replace(/(\..*)\./g, '$1');"></asp:textbox>
                        </fieldset>
                      </div>

                      <div class="col-md-2 gx-2 gy-2">
                        <fieldset>
                            <asp:Label ID="lblIdentidade" runat="server" Text="Identidade" class="tituloLabel form-label col-form-label col-form-label-sm" Style="text-align: left;"></asp:Label>
                            <asp:Literal runat="server"><br /></asp:Literal>
                            <asp:textbox ID="txtRG" runat="server" class="texto form-control form-control-sm" oninput="this.value = this.value.replace(/[^0-9]/g, '').replace(/(\..*)\./g, '$1');" Style="text-align: left;"></asp:textbox>
                        </fieldset>
                      </div>

                      <div class="col-md-3 gx-2 gy-2">
                        <fieldset>
                            <asp:Label runat="server" Text="E-mail" class="tituloLabel form-label col-form-label col-form-label-sm" Style="text-align: left;"></asp:Label>
                            <asp:Literal runat="server"><br /></asp:Literal>
                            <asp:textbox ID="txtEmail" runat="server" class="texto form-control form-control-sm" type="email" Style="text-align: left;"></asp:textbox>
                        </fieldset>
                      </div>
               </div>
           </div>
            

          

          


        <%-- SEGUNDO CONTAINER --%>

            <div class="col-11 subtituloBG" style="padding-left: 0 !important;margin-top: 30px;">
                <asp:Image ID="Image1" runat="server" AlternateText="2_menu" ImageAlign="Left" ImageUrl="Images/numero_2.png" CssClass="image1"/>
                <h2 class="subtitulo">Detalhes da Classificação Funcional</h2>
            </div>
          
        <%-- BOTÕES --%>

    <eo:TabStrip ID="TabStrip1" runat="server" ControlSkinID="None" Width="1000px" 
        MultiPageID="MultiPage1">
        <LookItems>
            <eo:TabItem ItemID="_Default" 
                NormalStyle-CssText="font-family:'Ubuntu'; font-size: 12px; color: #B0ABAB; padding: 10px 15px; background: #F1F1F1; border-radius: 8px; cursor: hand; width: fit-content; margin-right: 1rem;"
                SelectedStyle-CssText="font-family:'Ubuntu'; font-size: 12px; color: #1C9489; font-weight: bold; padding: 10px 15px; background: #D9D9D9; border-radius: 8px; cursor: hand; width: fit-content; margin-right: 1rem;">
                <SubGroup OverlapDepth="8" 
                    Style-CssText="font-family:'Ubuntu'; font-size: 12px; color: #B0ABAB; padding: 10px 15px 10px 0px; border-radius: 8px; cursor: hand; width: fit-content;">
                </SubGroup>
            </eo:TabItem>
        </LookItems>
        <TopGroup>
            <Items>
                <eo:TabItem Text-Html="Detalhes da Classificação Funcional">
                </eo:TabItem>
                <eo:TabItem Text-Html="Beneficiário">
                </eo:TabItem>

            </Items>
        </TopGroup>
    </eo:TabStrip>



    <eo:MultiPage ID="MultiPage1" runat="server" Height="80" Width="838">

              <eo:PageView ID="Pageview1" runat="server" Width="910px">

                  <%-- PRIMEIRA CLASSIFICAÇÃO--%>

                                    <table class="container d-flex">
                                        <tr class="row gx-3 gy-2">
                                            <td class="col-12 gx-3 gy-2">
                                                <svg xmlns="http://www.w3.org/2000/svg" width="21" height="21" fill="#1C9489"  class="bi bi-1-square-fill" viewBox="0 0 16 16">
                                                  <path d="M2 0a2 2 0 0 0-2 2v12a2 2 0 0 0 2 2h12a2 2 0 0 0 2-2V2a2 2 0 0 0-2-2H2Zm7.283 4.002V12H7.971V5.338h-.065L6.072 6.656V5.385l1.899-1.383h1.312Z"/>
                                                </svg>
                                            </td>
                                            <td class="col-md-6 gx-3 gy-2" style="margin-bottom: 15px;">
                                                <asp:Label ID="lblInicioFuncao" runat="server" Text="Início da Função" 
                                                    class="tituloLabel form-label col-form-label col-form-label-sm" Style="text-align: left;"></asp:Label>
                                                <asp:Literal runat="server"><br /></asp:Literal>
                                                <asp:textbox ID="txtInicioFuncao" runat="server" class="texto form-control form-control-sm" type="date"
                                                    style="text-align: left" Width="150px" MaxLength="10"></asp:textbox>
                                            </td>
                                            <td class="col-md-6 gx-3 gy-2" style="margin-bottom: 15px;">
                                                <asp:Label ID="lblTerminoFuncao" runat="server" Text="Término da Função"
                                                    class="tituloLabel form-label col-form-label col-form-label-sm" Style="text-align: left;"></asp:Label>
                                                <asp:Literal runat="server"><br /></asp:Literal>
                                                <asp:textbox ID="txtTerminoFuncao" runat="server" class="texto form-control form-control-sm" type="date"
                                                    style="text-align: left" Width="150px" MaxLength="10"></asp:textbox>
                                            </td>
                                        </tr>
                                        <tr class="row gx-3 gy-2">
                                            <td class="col-12 gx-3 gy-2">
                                                <asp:Label ID="lblFuncao" runat="server" Text="Função"
                                                    class="tituloLabel form-label col-form-label col-form-label-sm" Style="text-align: left;"></asp:Label>
                                                <asp:Literal runat="server"><br /></asp:Literal>
                                                <asp:textbox ID="txtFuncao" runat="server" class="texto form-control form-control-sm mb-1" placeholder="Inserir nova função" width="350px">
                                                </asp:textbox>
                                                <asp:DropDownList ID="cmb_Funcao1" runat="server" AutoPostBack="True" 
                                                    class="texto form-select form-select-sm" Height="28px" width="350px">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr class="row gx-3 gy-2">
                                            <td class="col-md-6 gx-3 gy-2">
                                                <asp:Label ID="lblSetor" runat="server" Text="Setor"
                                                    class="tituloLabel form-label col-form-label col-form-label-sm" Style="text-align: left;"></asp:Label>
                                                <asp:Literal runat="server"><br /></asp:Literal>
                                                <asp:textbox ID="txtSetor" runat="server" class="texto form-control form-control-sm mb-1" placeholder="Inserir novo setor" width="350px">
                                                </asp:textbox>
                                                <asp:DropDownList ID="cmb_Setor1" runat="server" AutoPostBack="True" 
                                                    class="texto form-select form-select-sm" Height="28px" width="350px">
                                                </asp:DropDownList>
                                            </td>
                                            <td class="col-md-6 gx-3 gy-2">
                                                <asp:Label ID="lblCargo" runat="server" Text="Cargo"
                                                    class="tituloLabel form-label col-form-label col-form-label-sm" Style="text-align: left;"></asp:Label>
                                                <asp:Literal runat="server"><br /></asp:Literal>
                                                <asp:textbox ID="txtCargo" runat="server" class="texto form-control form-control-sm mb-1" placeholder="Inserir novo cargo" width="350px">
                                                </asp:textbox>
                                                <asp:DropDownList ID="cmb_Cargo1" runat="server" AutoPostBack="True" 
                                                    class="texto form-select form-select-sm" Height="28px" width="350px">
                                                </asp:DropDownList>
                                            </td>
                                            </tr>
                                            <tr>
                                            <td>                                                
                                            <asp:Label ID="lbl_Id_Empresa" runat="server" Text="IdEmpresa" Visible="False"></asp:Label>
                                            &nbsp;&nbsp;
                                            <asp:Label ID="lbl_Id_Usuario" runat="server" Text="IdUsuario" Visible="False"></asp:Label>
                                           </td>
                                        </tr>
                                    </table>

                                   <%-- SEGUNDA CLASSIFICAÇÃO --%>

                                     <table class="container d-flex" style="margin-bottom: 30px;">
                                        <tr class="row gx-3 gy-2">
                                            <td class="col-12 gx-3 gy-2">
                                                <svg xmlns="http://www.w3.org/2000/svg" width="21" height="21" fill="#1C9489"  class="bi bi-1-square-fill" viewBox="0 0 16 16">
                                                  <path d="M2 0a2 2 0 0 0-2 2v12a2 2 0 0 0 2 2h12a2 2 0 0 0 2-2V2a2 2 0 0 0-2-2H2Zm4.646 6.24v.07H5.375v-.064c0-1.213.879-2.402 2.637-2.402 1.582 0 2.613.949 2.613 2.215 0 1.002-.6 1.667-1.287 2.43l-.096.107-1.974 2.22v.077h3.498V12H5.422v-.832l2.97-3.293c.434-.475.903-1.008.903-1.705 0-.744-.557-1.236-1.313-1.236-.843 0-1.336.615-1.336 1.306Z"/>
                                                </svg>
                                            </td>
                                            <td class="col-md-6 gx-3 gy-2" style="margin-bottom: 15px;">
                                                <asp:Label ID="Label1" runat="server" Text="Início da Função" 
                                                    class="tituloLabel form-label col-form-label col-form-label-sm" Style="text-align: left;"></asp:Label>
                                                <asp:Literal runat="server"><br /></asp:Literal>
                                                <asp:textbox ID="txtInicioFuncao2" runat="server" class="texto form-control form-control-sm" type="date"
                                                    style="text-align: left" Width="150px" MaxLength="10"></asp:textbox>
                                            </td>
                                            <td class="col-md-6 gx-3 gy-2" style="margin-bottom: 15px;">
                                                <asp:Label ID="Label2" runat="server" Text="Término da Função"
                                                    class="tituloLabel form-label col-form-label col-form-label-sm" Style="text-align: left;"></asp:Label>
                                                <asp:Literal runat="server"><br /></asp:Literal>
                                                <asp:textbox ID="txtTerminoFuncao2" runat="server" class="texto form-control form-control-sm" type="date"
                                                    style="text-align: left" Width="150px" MaxLength="10"></asp:textbox>
                                            </td>
                                        </tr>
                                        <tr class="row gx-3 gy-2">
                                            <td class="col-12 gx-3 gy-2">
                                                <asp:Label ID="Label3" runat="server" Text="Função"
                                                    class="tituloLabel form-label col-form-label col-form-label-sm" Style="text-align: left;"></asp:Label>
                                                <asp:Literal runat="server"><br /></asp:Literal>
                                                <asp:textbox ID="txtFuncao2" runat="server" class="texto form-control form-control-sm mb-1" placeholder="Inserir nova função" width="350px">
                                                </asp:textbox>
                                                <asp:DropDownList ID="cmb_Funcao2" runat="server" AutoPostBack="True" 
                                                    class="texto form-select form-select-sm" Height="28px" width="350px">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr class="row gx-3 gy-2">
                                            <td class="col-md-6 gx-3 gy-2">
                                                <asp:Label ID="Label4" runat="server" Text="Setor"
                                                    class="tituloLabel form-label col-form-label col-form-label-sm" Style="text-align: left;"></asp:Label>
                                                <asp:Literal runat="server"><br /></asp:Literal>
                                                <asp:textbox ID="txtSetor2" runat="server" class="texto form-control form-control-sm mb-1" placeholder="Inserir novo setor" width="350px">
                                                </asp:textbox>
                                                <asp:DropDownList ID="cmb_Setor2" runat="server" AutoPostBack="True" 
                                                    class="texto form-select form-select-sm" Height="28px" width="350px">
                                                </asp:DropDownList>
                                            </td>
                                            <td class="col-md-6 gx-3 gy-2">
                                                <asp:Label ID="Label5" runat="server" Text="Cargo"
                                                    class="tituloLabel form-label col-form-label col-form-label-sm" Style="text-align: left;"></asp:Label>
                                                <asp:Literal runat="server"><br /></asp:Literal>
                                                <asp:textbox ID="txtCargo2" runat="server" class="texto form-control form-control-sm mb-1" placeholder="Inserir novo cargo" width="350px">
                                                </asp:textbox>
                                                <asp:DropDownList ID="cmb_Cargo2" runat="server" AutoPostBack="True" 
                                                    class="texto form-select form-select-sm" Height="28px" width="350px">
                                                </asp:DropDownList>
                                            </td>
                                            </tr>
                                    </table>

                                    <%-- TERCEIRA CLASSIFICAÇÃO --%>

                                    <table class="container d-flex" style="margin-bottom: 30px;">
                                        <tr class="row gx-3 gy-2">
                                            <td class="col-12 gx-3 gy-2">
                                                <svg xmlns="http://www.w3.org/2000/svg" width="21" height="21" fill="#1C9489"  class="bi bi-1-square-fill" viewBox="0 0 16 16">
                                                  <path d="M2 0a2 2 0 0 0-2 2v12a2 2 0 0 0 2 2h12a2 2 0 0 0 2-2V2a2 2 0 0 0-2-2H2Zm5.918 8.414h-.879V7.342h.838c.78 0 1.348-.522 1.342-1.237 0-.709-.563-1.195-1.348-1.195-.79 0-1.312.498-1.348 1.055H5.275c.036-1.137.95-2.115 2.625-2.121 1.594-.012 2.608.885 2.637 2.062.023 1.137-.885 1.776-1.482 1.875v.07c.703.07 1.71.64 1.734 1.917.024 1.459-1.277 2.396-2.93 2.396-1.705 0-2.707-.967-2.754-2.144H6.33c.059.597.68 1.06 1.541 1.066.973.006 1.6-.563 1.588-1.354-.006-.779-.621-1.318-1.541-1.318Z"/>
                                                </svg>
                                            </td>
                                            <td class="col-md-6 gx-3 gy-2" style="margin-bottom: 15px;">
                                                <asp:Label ID="Label6" runat="server" Text="Início da Função" 
                                                    class="tituloLabel form-label col-form-label col-form-label-sm" Style="text-align: left;"></asp:Label>
                                                <asp:Literal runat="server"><br /></asp:Literal>
                                                <asp:textbox ID="txtInicioFuncao3" runat="server" class="texto form-control form-control-sm" type="date"
                                                    style="text-align: left" Width="150px" MaxLength="10"></asp:textbox>
                                            </td>
                                            <td class="col-md-6 gx-3 gy-2" style="margin-bottom: 15px;">
                                                <asp:Label ID="Label7" runat="server" Text="Término da Função"
                                                    class="tituloLabel form-label col-form-label col-form-label-sm" Style="text-align: left;"></asp:Label>
                                                <asp:Literal runat="server"><br /></asp:Literal>
                                                <asp:textbox ID="txtTerminoFuncao3" runat="server" class="texto form-control form-control-sm" type="date"
                                                    style="text-align: left" Width="150px" MaxLength="10"></asp:textbox>
                                            </td>
                                        </tr>
                                        <tr class="row gx-3 gy-2">
                                            <td class="col-12 gx-3 gy-2">
                                                <asp:Label ID="Label8" runat="server" Text="Função"
                                                    class="tituloLabel form-label col-form-label col-form-label-sm" Style="text-align: left;"></asp:Label>
                                                <asp:Literal runat="server"><br /></asp:Literal>
                                                <asp:textbox ID="txtFuncao3" runat="server" class="texto form-control form-control-sm mb-1" placeholder="Inserir nova função" width="350px">
                                                </asp:textbox>
                                                <asp:DropDownList ID="cmb_Funcao3" runat="server" AutoPostBack="True" 
                                                    class="texto form-select form-select-sm" Height="28px" width="350px">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr class="row gx-3 gy-2">
                                            <td class="col-md-6 gx-3 gy-2">
                                                <asp:Label ID="Label9" runat="server" Text="Setor"
                                                    class="tituloLabel form-label col-form-label col-form-label-sm" Style="text-align: left;"></asp:Label>
                                                <asp:Literal runat="server"><br /></asp:Literal>
                                                <asp:textbox ID="txtSetor3" runat="server" class="texto form-control form-control-sm mb-1" placeholder="Inserir novo setor" width="350px">
                                                </asp:textbox>
                                                <asp:DropDownList ID="cmb_Setor3" runat="server" AutoPostBack="True" 
                                                    class="texto form-select form-select-sm" Height="28px" width="350px">
                                                </asp:DropDownList>
                                            </td>
                                            <td class="col-md-6 gx-3 gy-2">
                                                <asp:Label ID="Label10" runat="server" Text="Cargo"
                                                    class="tituloLabel form-label col-form-label col-form-label-sm" Style="text-align: left;"></asp:Label>
                                                <asp:Literal runat="server"><br /></asp:Literal>
                                                <asp:textbox ID="txtCargo3" runat="server" class="texto form-control form-control-sm mb-1" placeholder="Inserir novo cargo" width="350px">
                                                </asp:textbox>
                                                <asp:DropDownList ID="cmb_Cargo3" runat="server" AutoPostBack="True" 
                                                    class="texto form-select form-select-sm" Height="28px" width="350px">
                                                </asp:DropDownList>
                                            </td>
                                            </tr>
                                    </table>
                                    
                                    <%-- QUARTA CLASSIFICAÇÃO --%>

                                    <table class="container d-flex" style="margin-bottom: 30px;">
                                        <tr class="row gx-3 gy-2">
                                            <td class="col-12 gx-3 gy-2">
                                                <svg xmlns="http://www.w3.org/2000/svg" width="21" height="21" fill="#1C9489"  class="bi bi-1-square-fill" viewBox="0 0 16 16">
                                                  <path d="M6.225 9.281v.053H8.85V5.063h-.065c-.867 1.33-1.787 2.806-2.56 4.218Z"/>
                                                  <path d="M2 0a2 2 0 0 0-2 2v12a2 2 0 0 0 2 2h12a2 2 0 0 0 2-2V2a2 2 0 0 0-2-2H2Zm5.519 5.057c.22-.352.439-.703.657-1.055h1.933v5.332h1.008v1.107H10.11V12H8.85v-1.559H4.978V9.322c.77-1.427 1.656-2.847 2.542-4.265Z"/>
                                                </svg>
                                            </td>
                                            <td class="col-md-6 gx-3 gy-2" style="margin-bottom: 15px;">
                                                <asp:Label ID="Label11" runat="server" Text="Início da Função" 
                                                    class="tituloLabel form-label col-form-label col-form-label-sm" Style="text-align: left;"></asp:Label>
                                                <asp:Literal runat="server"><br /></asp:Literal>
                                                <asp:textbox ID="txtInicioFuncao4" runat="server" class="texto form-control form-control-sm" type="date"
                                                    style="text-align: left" Width="150px" MaxLength="10"></asp:textbox>
                                            </td>
                                            <td class="col-md-6 gx-3 gy-2" style="margin-bottom: 15px;">
                                                <asp:Label ID="Label12" runat="server" Text="Término da Função"
                                                    class="tituloLabel form-label col-form-label col-form-label-sm" Style="text-align: left;"></asp:Label>
                                                <asp:Literal runat="server"><br /></asp:Literal>
                                                <asp:textbox ID="txtTerminoFuncao4" runat="server" class="texto form-control form-control-sm" type="date"
                                                    style="text-align: left" Width="150px" MaxLength="10"></asp:textbox>
                                            </td>
                                        </tr>
                                        <tr class="row gx-3 gy-2">
                                            <td class="col-12 gx-3 gy-2">
                                                <asp:Label ID="Label13" runat="server" Text="Função"
                                                    class="tituloLabel form-label col-form-label col-form-label-sm" Style="text-align: left;"></asp:Label>
                                                <asp:Literal runat="server"><br /></asp:Literal>
                                                <asp:textbox ID="txtFuncao4" runat="server" class="texto form-control form-control-sm mb-1" placeholder="Inserir nova função" width="350px">
                                                </asp:textbox>
                                                <asp:DropDownList ID="cmb_Funcao4" runat="server" AutoPostBack="True" 
                                                    class="texto form-select form-select-sm" Height="28px" width="350px">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr class="row gx-3 gy-2">
                                            <td class="col-md-6 gx-3 gy-2">
                                                <asp:Label ID="Label14" runat="server" Text="Setor"
                                                    class="tituloLabel form-label col-form-label col-form-label-sm" Style="text-align: left;"></asp:Label>
                                                <asp:Literal runat="server"><br /></asp:Literal>
                                                <asp:textbox ID="txtSetor4" runat="server" class="texto form-control form-control-sm mb-1" placeholder="Inserir novo setor" width="350px">
                                                </asp:textbox>
                                                <asp:DropDownList ID="cmb_Setor4" runat="server" AutoPostBack="True" 
                                                    class="texto form-select form-select-sm" Height="28px" width="350px">
                                                </asp:DropDownList>
                                            </td>
                                            <td class="col-md-6 gx-3 gy-2">
                                                <asp:Label ID="Label15" runat="server" Text="Cargo"
                                                    class="tituloLabel form-label col-form-label col-form-label-sm" Style="text-align: left;"></asp:Label>
                                                <asp:Literal runat="server"><br /></asp:Literal>
                                                <asp:textbox ID="txtCargo4" runat="server" class="texto form-control form-control-sm mb-1" placeholder="Inserir novo cargo" width="350px">
                                                </asp:textbox>
                                                <asp:DropDownList ID="cmb_Cargo4" runat="server" AutoPostBack="True" 
                                                    class="texto form-select form-select-sm" Height="28px" width="350px">
                                                </asp:DropDownList>
                                            </td>
                                            </tr>
                                    </table>

                                    <%-- QUINTA CLASSIFICAÇÃO --%>

                                    <table class="container d-flex">
                                        <tr class="row gx-3 gy-2">
                                            <td class="col-12 gx-3 gy-2">
                                                <svg xmlns="http://www.w3.org/2000/svg" width="21" height="21" fill="#1C9489"  class="bi bi-1-square-fill" viewBox="0 0 16 16">
                                                  <path d="M2 0a2 2 0 0 0-2 2v12a2 2 0 0 0 2 2h12a2 2 0 0 0 2-2V2a2 2 0 0 0-2-2H2Zm5.994 12.158c-1.57 0-2.654-.902-2.719-2.115h1.237c.14.72.832 1.031 1.529 1.031.791 0 1.57-.597 1.57-1.681 0-.967-.732-1.57-1.582-1.57-.767 0-1.242.45-1.435.808H5.445L5.791 4h4.705v1.103H6.875l-.193 2.343h.064c.17-.258.715-.68 1.611-.68 1.383 0 2.561.944 2.561 2.585 0 1.687-1.184 2.806-2.924 2.806Z"/>
                                                </svg>
                                            </td>
                                            <td class="col-md-6 gx-3 gy-2" style="margin-bottom: 15px;">
                                                <asp:Label ID="Label16" runat="server" Text="Início da Função" 
                                                    class="tituloLabel form-label col-form-label col-form-label-sm" Style="text-align: left;"></asp:Label>
                                                <asp:Literal runat="server"><br /></asp:Literal>
                                                <asp:textbox ID="txtInicioFuncao5" runat="server" class="texto form-control form-control-sm" type="date"
                                                    style="text-align: left" Width="150px" MaxLength="10"></asp:textbox>
                                            </td>
                                            <td class="col-md-6 gx-3 gy-2" style="margin-bottom: 15px;">
                                                <asp:Label ID="Label17" runat="server" Text="Término da Função"
                                                    class="tituloLabel form-label col-form-label col-form-label-sm" Style="text-align: left;"></asp:Label>
                                                <asp:Literal runat="server"><br /></asp:Literal>
                                                <asp:textbox ID="txtTerminoFuncao5" runat="server" class="texto form-control form-control-sm" type="date"
                                                    style="text-align: left" Width="150px" MaxLength="10"></asp:textbox>
                                            </td>
                                        </tr>
                                        <tr class="row gx-3 gy-2">
                                            <td class="col-12 gx-3 gy-2">
                                                <asp:Label ID="Label18" runat="server" Text="Função"
                                                    class="tituloLabel form-label col-form-label col-form-label-sm" Style="text-align: left;"></asp:Label>
                                                <asp:Literal runat="server"><br /></asp:Literal>
                                                <asp:textbox ID="txtFuncao5" runat="server" class="texto form-control form-control-sm mb-1" placeholder="Inserir nova função" width="350px">
                                                </asp:textbox>
                                                <asp:DropDownList ID="cmb_Funcao5" runat="server" AutoPostBack="True" 
                                                    class="texto form-select form-select-sm" Height="28px" width="350px">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr class="row gx-3 gy-2">
                                            <td class="col-md-6 gx-3 gy-2">
                                                <asp:Label ID="Label19" runat="server" Text="Setor"
                                                    class="tituloLabel form-label col-form-label col-form-label-sm" Style="text-align: left;"></asp:Label>
                                                <asp:Literal runat="server"><br /></asp:Literal>
                                                <asp:textbox ID="txtSetor5" runat="server" class="texto form-control form-control-sm mb-1" placeholder="Inserir novo setor" width="350px">
                                                </asp:textbox>
                                                <asp:DropDownList ID="cmb_Setor5" runat="server" AutoPostBack="True" 
                                                    class="texto form-select form-select-sm" Height="28px" width="350px">
                                                </asp:DropDownList>
                                            </td>
                                            <td class="col-md-6 gx-3 gy-2">
                                                <asp:Label ID="Label20" runat="server" Text="Cargo"
                                                    class="tituloLabel form-label col-form-label col-form-label-sm" Style="text-align: left;"></asp:Label>
                                                <asp:Literal runat="server"><br /></asp:Literal>
                                                <asp:textbox ID="txtCargo5" runat="server" class="texto form-control form-control-sm mb-1" placeholder="Inserir novo cargo" width="350px">
                                                </asp:textbox>
                                                <asp:DropDownList ID="cmb_Cargo5" runat="server" AutoPostBack="True" 
                                                    class="texto form-select form-select-sm" Height="28px" width="350px">
                                                </asp:DropDownList>
                                            </td>
                                            </tr>
                                    </table>

                    </eo:PageView>

                    <eo:PageView ID="Pageview2" runat="server">
                        <div class="container d-flex">
                            <div class="row gx-3 gy-2">
                                <fieldset>
                                    <asp:Label ID="Label21" runat="server" class="tituloLabel form-check-label"
                                        Text="Tipo de Beneficiário (Lei 8213 /91, Artigo 93)"></asp:Label>
                                    <asp:Literal runat="server"><br /></asp:Literal>
                                    <div class="form-check col-12">
                                        <asp:RadioButton ID="opt_Benef_Reabilitado" runat="server" CssClass="texto"
                                            GroupName="T1" Text="Beneficiário reabilitado" />
                                    </div>
                                    <div class="form-check col-12">
                                        <asp:RadioButton ID="opt_Benef_Deficiencia" runat="server" GroupName="T1" CssClass="texto" 
                                            Text="Portador de deficiência habilitada" />
                                    </div>
                                    <div class="form-check col-12">
                                        <asp:RadioButton ID="opt_Benef_NA" runat="server" Checked="True" CssClass="texto" 
                                            GroupName="T1" Text="Não aplicável" />
                                    </div>
                                </fieldset>
                                
                            </div>
                        </div>

              </eo:PageView>
                    </eo:MultiPage>
            <div class="col-12" style="margin-top: 30px;">
                <div class="text-center">
                    <asp:Label ID="lblNomeEmpregado" runat="server"></asp:Label>
                    <asp:Button ID="cmd_Atualizar" runat="server" CssClass="btn" Text="Salvar Registro" onclick="cmd_Atualizar_Click"  />
                </div>
            </div>

            <div class="col-12 ps-4">
                <div class="text-start">
                    <asp:Button ID="cmd_Voltar" runat="server" CssClass="btn2" Text="Voltar" />
                </div>
            </div>

    </div>
    </div>

        <eo:MsgBox ID="MsgBox1" runat="server" BackColor="#ffffff" ControlSkinID="None" HeaderHtml="Dialog Title" Height="192px" Width="345px" CssClass="card border border-1 p-2 text-center" IconUrl="Images/alerta_icon.svg">
            <HeaderStyleActive CssText="font-family: Ubuntu; font-size: 14px; font-weight: bold; color: #1C9489; text-align: center; padding: 5px;" />
            <ContentStyleActive CssText="width: 345px; font-family: Ubuntu; font-size: 14px; font-weight: bold; color: #7D7B7B; text-align: center; padding: 5px;" />
            <FooterStyleActive CssText="width: 345px;" />
        </eo:MsgBox>
</asp:Content>
