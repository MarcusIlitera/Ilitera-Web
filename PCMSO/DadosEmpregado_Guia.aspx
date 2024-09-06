<%@ Page Language="C#"   AutoEventWireup="True"  MasterPageFile="~/Site.Master" CodeBehind="DadosEmpregado_Guia.aspx.cs"  Inherits="Ilitera.Net.DadosEmpregado_Guia" Title="Ilitera.Net" EnableEventValidation="false"  ValidateRequest="false"  %>

<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <title>Guia de Encaminhamento</title>
    <link href="../css/forms.css" rel="stylesheet" type="text/css" />
    <style type="text/css">

.buttonBox
{
	background: #3E684D center top;
	font: bold 9px Verdana, Arial, Helvetica, sans-serif, Tahoma;
	color:White;
	border-top: thin solid #597C4E;
	border-right: thin solid #23361F;
	border-bottom: thin solid #23361F;
	border-left: thin solid #597C4E;
}
    </style>
</asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div class="container-fluid d-flex ms-5 ps-4">
        <div class="row gx-3 gy-2 w-100">

            <%-- PRIMEIRO CONTAINER --%>

            <div class="col-11 subtituloBG">
                <h2 class="subtitulo">Dados do Empregado</h2>
            </div>

            <div class="col-11">
                <div class="row">
                     <%-- LINHA UM --%>
                    <div class="col-md-4 gx-3 gy-2">
                        <fieldset>
                            <asp:Label ID="lblNome" runat="server" Text="Nome" class="tituloLabel form-label col-form-label col-form-label-sm" Style="text-align: left;"></asp:Label>
                            <asp:Literal runat="server"><br /></asp:Literal>
                            <asp:Label ID="lblValorNome" runat="server" class="texto form-control form-control-sm" Style="text-align: left;"></asp:Label>
                        </fieldset>
                   </div>

                  <div class="col-md-3 gx-3 gy-2">
                    <fieldset>
                        <asp:Label ID="lblTipoBene" runat="server" Text="Tipo de Beneficiário" class="tituloLabel form-label col-form-label col-form-label-sm" Style="text-align: left;"></asp:Label>
                        <asp:Literal runat="server"><br /></asp:Literal>
                        <asp:Label ID="lblValorBene" runat="server" class="texto form-control form-control-sm" Style="text-align: left;"></asp:Label>


                    </fieldset>
                  </div>

                  <div class="col-md-2 gx-3 gy-2">
                    <fieldset>
                        <asp:Label ID="lblDataNascimento" runat="server" Text="Nascimento" CssClass="tituloLabel form-label col-form-label col-form-label-sm"></asp:Label>
                        <asp:Literal runat="server"><br /></asp:Literal>
                        <asp:Label ID="lblValorNasc" runat="server" CssClass="texto form-control form-control-sm" type="date"></asp:Label>
                    </fieldset>
                  </div>

                  <div class="col-md-1 gx-3 gy-2">
                    <fieldset>
                        <asp:Label ID="lblIdade" runat="server" Text="Idade" class="tituloLabel form-label col-form-label col-form-label-sm" Style="text-align: left;"></asp:Label>
                        <asp:Literal runat="server"><br /></asp:Literal>
                        <asp:Label ID="lblValorIdade" runat="server" class="texto form-control form-control-sm" Style="text-align: left;"></asp:Label>
                                                                    <asp:Label ID="lblTipoGuia" runat="server" Text="1" Visible="False"></asp:Label>
                    </fieldset>
                  </div>

                  <div class="col-md-2 gx-3 gy-2">
                    <fieldset>
                        <asp:Label ID="lblSexo" runat="server" Text="Sexo" class="tituloLabel form-label col-form-label col-form-label-sm" Style="text-align: left;"></asp:Label>
                        <asp:Literal runat="server"><br /></asp:Literal>
                        <asp:Label ID="lblValorSexo" runat="server" class="texto form-control form-control-sm" Style="text-align: left;"></asp:Label>
                    </fieldset>
                  </div>

                    <%-- LINHA DOIS --%>

                  <div class="col-md-2 gx-3 gy-2">
                    <fieldset>
                        <asp:Label ID="lblAdmissao" runat="server" Text="Admissão" CssClass="tituloLabel form-label col-form-label col-form-label-sm"></asp:Label>
                        <asp:Literal runat="server"><br /></asp:Literal>
                        <asp:Label ID="lblValorAdmissao" runat="server" CssClass="texto form-control form-control-sm" type="date"></asp:Label>
                    </fieldset>
                  </div>

                  <div class="col-md-2 gx-3 gy-2">
                      <fieldset>
                        <asp:Label ID="lblDemissao" runat="server" Text="Demissão" CssClass="tituloLabel form-label col-form-label col-form-label-sm"></asp:Label>
                        <asp:Literal runat="server"><br /></asp:Literal>
                        <asp:Label ID="lblValorDemissao" runat="server" CssClass="texto form-control form-control-sm" type="date"></asp:Label>
                    </fieldset>
                  </div>
          
                  <div class="col-2 gx-3 gy-2">
                          <fieldset>
                              <asp:Label ID="lblJornada" runat="server" Text="Jornada" class="tituloLabel form-label col-form-label col-form-label-sm" Style="text-align: left;"></asp:Label>
                              <asp:Literal runat="server"><br /></asp:Literal>
                              <asp:Label ID="lblValorJornada" runat="server" class="texto form-control form-control-sm" Style="text-align: left;"></asp:Label>
                            </fieldset>
                  </div>

                  <div class="col-3 gx-3 gy-2">
                      <fieldset>
                        <asp:Label ID="lblRegistro" runat="server" Text="RE" class="tituloLabel form-label col-form-label col-form-label-sm" Style="text-align: left;"></asp:Label>
                        <asp:Literal runat="server"><br /></asp:Literal>
                        <asp:Label ID="lblValorRegistro" runat="server" class="texto form-control form-control-sm" Style="text-align: left;"></asp:Label>
                    </fieldset>
                  </div>

                  <div class="col-3 gx-3 gy-2">
                          <fieldset>
                              <asp:Label ID="lblRegRev" runat="server" Text="Regime de Revezamento" class="tituloLabel form-label col-form-label col-form-label-sm" Style="text-align: left;"></asp:Label>
                              <asp:Literal runat="server"><br /></asp:Literal>
                              <asp:Label ID="lblValorRegRev" runat="server" class="texto form-control form-control-sm" Style="text-align: left;"></asp:Label>
                            </fieldset>
                  </div>

                 <%-- LINHA TRÊS --%>

                  <div class="col-md-2 gx-3 gy-2">
                    <fieldset>
                        <asp:Label ID="lblGHE" runat="server" Text="Tempo de Empresa" CssClass="tituloLabel form-label col-form-label col-form-label-sm"></asp:Label>
                        <asp:Literal runat="server"><br /></asp:Literal>
                        <asp:Label ID="lblValorTempoEmpresa" runat="server" CssClass="texto form-control form-control-sm" ></asp:Label>
                    </fieldset>
                  </div>

                  <div class="col-md-2 gx-3 gy-2">
                      <fieldset>
                        <asp:Label ID="lblDataIni" runat="server" Text="Início da Função" CssClass="tituloLabel form-label col-form-label col-form-label-sm"></asp:Label>
                        <asp:Literal runat="server"><br /></asp:Literal>
                        <asp:Label ID="lblValorDataIni" runat="server" CssClass="texto form-control form-control-sm" text="27/03/2015" type="date"></asp:Label>
                    </fieldset>
                  </div>

                  <div class="col-4 gx-3 gy-2">
                      <fieldset>
                        <asp:Label ID="lblSetor" runat="server" Text="Setor" class="tituloLabel form-label col-form-label col-form-label-sm" Style="text-align: left;"></asp:Label>
                        <asp:Literal runat="server"><br /></asp:Literal>
                        <asp:Label ID="lblValorSetor" runat="server" class="texto form-control form-control-sm" Style="text-align: left;"></asp:Label>
                    </fieldset>
                  </div>

                  <div class="col-4 gx-3 gy-2">
                    <fieldset>
                        <asp:Label ID="lblFuncao" runat="server" Text="Função" class="tituloLabel form-label col-form-label col-form-label-sm" Style="text-align: left;"></asp:Label>
                        <asp:Literal runat="server"><br /></asp:Literal>
                        <asp:Label ID="lblValorFuncao" runat="server" class="texto form-control form-control-sm" Style="text-align: left;"></asp:Label>
                    </fieldset>
                  </div> 
          
                  <div class="col-12 gx-3 gy-2" style="margin-bottom: 30px;">
                      <div class="form-check">
                        <asp:CheckBox runat="server" type="checkbox" value="" id="chk_Basico" />
                          <label class="texto form-check-sm-label" for="flexCheckDefault">
                            Imprimir apenas Nome, Setor e GHE
                        </label>
                      </div>
                  </div>   
                </div>
            </div>

           <div class="col-11 mb-2">
               <div class="row">
                    <%-- SEGUNDO CONTAINER --%>
                <div class="col-12 subtituloBG gx-3 gy-2" style="padding-left: 0px !important;">
                    <asp:Image ID="menu1" runat="server" AlternateText="2_menu" ImageAlign="Left" ImageUrl="images/numero_1.png" CssClass="image1"/>
                    <h2 class="subtitulo">Escolha da Clínica para Realização do Exame</h2>
                </div>

                <%-- LINHA UM --%>

                <div class="col-md-6 gx-3 gy-2">
                    <fieldset>
                        <asp:Label runat="server" Text="Selecionar a Clínica" class="tituloLabel form-label col-form-label col-form-label-sm" Style="text-align: left;"></asp:Label>
                        <asp:Literal runat="server"><br /></asp:Literal>
                        <asp:DropDownList ID="cmb_Clinicas" onselectedindexchanged="cmb_Clinicas_SelectedIndexChanged" AutoPostBack="true"  runat="server" class="texto form-select form-select-sm" Style="text-align: left;">
                        </asp:DropDownList>
                        <asp:Label ID="lbl_Id_Clinica" runat="server" Text="0" Visible="False"></asp:Label>
                    </fieldset>
               </div>

               <div class="col-md-6 gx-3 gy-2">
                   <fieldset>
                       <asp:Label runat="server" Text="Endereço da Clínica Selecionada" class="tituloLabel form-label col-form-label col-form-label-sm" Style="text-align: left;"></asp:Label>
                        <asp:Literal runat="server"><br /></asp:Literal>
                        <asp:Label ID="lbl_End_Clinica" runat="server" class="texto form-control form-control-sm" Style="text-align: left;"></asp:Label>
                   </fieldset>
               </div>

              <%-- LINHA DOIS --%>

              <div class="col-md-4 gx-3 gy-2">
                   <fieldset>
                       <asp:Label runat="server" Text="Telefone da Clínica" class="tituloLabel form-label col-form-label col-form-label-sm" Style="text-align: left;"></asp:Label>
                        <asp:Literal runat="server"><br /></asp:Literal>
                        <asp:Label ID="lbl_Fone_Clinica" runat="server" class="texto form-control form-control-sm" Style="text-align: left;"></asp:Label>
                   </fieldset>
               </div>

               <div class="col-md-4 gx-3 gy-2">
                   <fieldset>
                       <asp:Label runat="server" Text="Horário de Atendimento" class="tituloLabel form-label col-form-label col-form-label-sm" Style="text-align: left;"></asp:Label>
                        <asp:Literal runat="server"><br /></asp:Literal>
                        <asp:Label ID="lbl_Horario" runat="server" class="texto form-control form-control-sm" Style="text-align: left;"></asp:Label>
                   </fieldset>
               </div>
           
               <div class="col-md-4 gx-3 gy-2">
                   <fieldset>
                       <asp:Label runat="server" Text="E-Mail da Clínica" class="tituloLabel form-label col-form-label col-form-label-sm" Style="text-align: left;"></asp:Label>
                        <asp:Literal runat="server"><br /></asp:Literal>
                        <asp:Label ID="lbl_email" runat="server" class="texto form-control form-control-sm" Style="text-align: left;"></asp:Label>
                   </fieldset>
               </div>
              <%-- LINHA TRÊS --%>

              <div class="col-md-12 gx-3 gy-2">
                  <fieldset>
                      <asp:Label runat="server" Text="Observações da Clínica Selecionada" class="tituloLabel form-label col-form-label col-form-label-sm" Style="text-align: left;"></asp:Label>
                        <asp:Literal runat="server"><br /></asp:Literal>
                        <asp:Label ID="TextBox15" runat="server" class="texto form-control form-control-sm" Style="text-align: left;" TextMode="MultiLine"></asp:Label>
                  </fieldset>
              </div>
               </div>
           </div>

            <div class="col-11 subtituloBG" >
              <h2 class="subtitulo">Escolha do Exame a ser Realizado</h2>
          </div>

           <div class="col-11">
               <div class="row">
                    <%-- LINHA UM --%>
                 <div class="col-md-4 gx-3 gy-2" style="margin-bottom: 30px;">
                     <div class="subtituloBG mb-2">
                         <asp:Image runat="server" AlternateText="2_menu" ImageAlign="Left" ImageUrl="images/numero_2.png" CssClass="image2"/>
                         <h2 class="subtitulo" style="padding-top: .8rem;">Selecione o tipo de Exame</h2>
                     </div>
                     <fieldset style="margin-bottom: 30px;">
                         <asp:Label runat="server" Text="Selecionar o Exame" class="tituloLabel form-label col-form-label col-form-label-sm" Style="text-align: left;"></asp:Label>
                         <asp:Literal runat="server"><br /></asp:Literal>
                         <div class="form-check">
                             <asp:RadioButton runat="server" ID="rd_Admissao"  oncheckedchanged="rd_Admissao_CheckedChanged" Text="Admissão" type="radio" class="form-check-label texto" GroupName="Tipo" AutoPostBack="true" CausesValidation="true" />
                         </div>
                         <div class="form-check">
                             <asp:RadioButton runat="server" ID="rd_Demissional" oncheckedchanged="rd_Demissional_CheckedChanged" Text="Demissional" type="radio" class="form-check-label texto" GroupName="Tipo" AutoPostBack="true" CausesValidation="true" />
                         </div>
                         <div class="form-check">
                             <asp:RadioButton runat="server" ID="rd_Mudanca" oncheckedchanged="rd_Mudanca_CheckedChanged" Text="Mudança de Risco Ocupacional" type="radio" class="form-check-label texto" GroupName="Tipo" AutoPostBack="true"  CausesValidation="true" />
                         </div>
                         <div class="form-check">
                             <asp:RadioButton runat="server" ID="rd_Retorno" oncheckedchanged="rd_Retorno_CheckedChanged" Text="Retorno ao Trabalho" type="radio" class="form-check-label texto" GroupName="Tipo" AutoPostBack="true"  CausesValidation="true" />
                         </div>
                         <div class="form-check">
                             <asp:RadioButton runat="server" ID="rd_Periodico" oncheckedchanged="rd_Periodico_CheckedChanged"  Text="Periódico" type="radio" class="form-check-label texto" GroupName="Tipo" AutoPostBack="true"  CausesValidation="true" Checked="true" />
                         </div>
                         <div class="form-check">
                             <asp:RadioButton runat="server" ID="rd_Outro" oncheckedchanged="rd_Outro_CheckedChanged"  Text="Outro" type="radio" class="form-check-label texto" AutoPostBack="true" CausesValidation="true" />
                         </div>
                 

                        <%--  <asp:DropDownList runat="server" class="texto form-select form-select-sm" Style="text-align: left;">
                             <asp:ListItem Selected="True" Value="0">Selecione uma das opções</asp:ListItem>
                             <asp:ListItem Value="1" ID="rd_Admissao" CausesValidation="True">Admissão</asp:ListItem>
                             <asp:ListItem Value="2" ID="rd_Demissional" CausesValidation="True">Demissional</asp:ListItem>
                             <asp:ListItem Value="3" ID="rd_Mudanca" CausesValidation="True">Mudança de Função</asp:ListItem>
                             <asp:ListItem Value="4" ID="rd_Retorno" CausesValidation="True">Retorno ao Trabalho</asp:ListItem>
                             <asp:ListItem Value="5" ID="rd_Periodico" CausesValidation="True">Periódico</asp:ListItem>
                             <asp:ListItem Value="6" ID="rd_Outro" CausesValidation="True">Outro</asp:ListItem>
                         </asp:DropDownList>--%>
                     </fieldset>

                     <div class="subtituloBG mb-2">
                         <h2 class="subtitulo" style="padding-top: .8rem;">Exames Previstos no PCMSO</h2>
                     </div>
                     <fieldset>
               
                                <asp:TextBox ID="txt_Exames" runat="server" Width="335px" class="texto form-control"
                                Height="200px" TextMode="MultiLine" ></asp:TextBox>

                                                        <input id="txtIdUsuario" type="text" visible="False"  style="visibility:hidden"  />
                                                        <input id="txtIdEmpregado" type="text" visible="False" style="visibility:hidden" />
                                                        <input id="txtIdEmpresa" type="text" visible="False" style="visibility:hidden"/>
                        
                     </fieldset>
                  </div>

                  <div class="col-md-4 gx-3 gy-2">
                      <div class="subtituloBG mb-2">
                         <asp:Image runat="server" AlternateText="2_menu" ImageAlign="Left" ImageUrl="images/numero_3.png" CssClass="image2"/>
                         <h2 class="subtitulo" style="padding-top: .8rem;">Exames Selecionados</h2>
                     </div>
                     <div class="col-md-12">
                         <fieldset>
                             <asp:CheckBoxList runat="server" ID="lst_Exames" CssClass="form-check texto bg-transparent border-0" Height="150px" Width="350px" RepeatColumns="2"></asp:CheckBoxList>
                         </fieldset>
                         <asp:CheckBox runat="server" type="checkbox" value="" Text="Inserir Exame Toxicológico na guia" id="chk_Toxicologico" AutoPostBack="True" Enabled="False" />
                     </div>
                  </div>
          
                  <div class="col-md-4 gx-3 gy-2">
                      <div class="subtituloBG mb-2">
                         <asp:Image runat="server" AlternateText="2_menu" ImageAlign="Left" ImageUrl="images/numero_4.png" CssClass="image2"/>
                         <h2 class="subtitulo" style="padding-top: .8rem;">Escolha Data e Hora do Exame</h2>
                     </div>

                      <div class="row">
                          <div class="col-md-6 gx-3 gy-2">
                              <fieldset>
                                <asp:Label runat="server" Text="Data do Exame" CssClass="tituloLabel form-label col-form-label col-form-label-sm"></asp:Label>
                                <asp:Literal runat="server"><br /></asp:Literal>
                                <asp:TextBox ID="txt_Data" OnTextChanged="txt_Data_TextChanged" runat="server" CssClass="texto form-control form-control-sm" type="date"></asp:TextBox>
                            </fieldset>
                          </div>

                          <div class="col-md-6 gx-3 gy-2">
                              <fieldset>
                                <asp:Label runat="server" Text="Hora" CssClass="tituloLabel form-label col-form-label col-form-label-sm"></asp:Label>
                                <asp:Literal runat="server"><br /></asp:Literal>
                                <asp:TextBox ID="txt_Hora" runat="server" CssClass="texto form-control form-control-sm"></asp:TextBox>
                            </fieldset>
                         </div>

                         <div class="col-md-6 gx-3 gy-2">
                             <fieldset>
                                 <asp:Label ID="lbl_Demissao" runat="server" CssClass="tituloLabel form-label col-form-label col-form-label-sm" Text="Data da Demissão"></asp:Label>
                                 <asp:Literal runat="server"><br /></asp:Literal>
                                 <asp:TextBox ID="txt_Demissao" runat="server" CssClass="texto form-control form-control-sm" MaxLength="10" AutoPostBack="True" Enabled="False" OnTextChanged="txt_Data_TextChanged"></asp:TextBox>
                             </fieldset>
                         </div>
                 
                         <div class="col-md-12 gx-3 gy-2">
                             <fieldset>
                                 <asp:Label runat="server" Text="Observações da Clínica Selecionada" class="tituloLabel form-label col-form-label col-form-label-sm" Style="text-align: left;"></asp:Label>
                                 <asp:Literal runat="server"><br /></asp:Literal>
                                 <asp:TextBox ID="txt_Obs" runat="server" class="texto form-control form-control-sm" Style="text-align: left;" TextMode="MultiLine"></asp:TextBox>
                              </fieldset>
                         </div>

                         <div class="col-md-12 form-check mt-2 gx-3 gy-2" style="margin-left: 15px;">
                             <asp:CheckBox runat="server" type="checkbox" value="" id="chk_Data" Checked="True" />
                              <label class="texto form-check-sm-label" for="flexCheckDefault">
                                Exibir Data na Guia
                            </label>
                         </div>
                      </div>
              
                  </div>
               </div>
           </div>

           <div class="col-11 mb-1" >
               <div class="row" >
                   <%-- QUATRO CONTAINER --%>

<%--                   <div class="col-12 subtituloBG" style="padding-left: 0px !important;" >
                      <asp:Image ID="Image5" runat="server" AlternateText="2_menu" ImageAlign="Left" ImageUrl="images/numero_5.png" CssClass="image1"/>
                      <h2 class="subtitulo">Escolha das Observações Possíveis para Inclusão no ASO</h2>
                  </div>   

                    <div class="col-md-12 gx-3 gy-2" >
                        <fieldset>  
                             <div class="form-check">
                                 <asp:CheckBox runat="server" type="checkbox" value="" id="chk_apt_Altura" Visible="False" />
                                   <label class="texto form-check-label" for="defaultCheck1" >
                                    Indicar no ASO aptidão para trabalho em altura (alínea 'd', do item 32.2.3.1 da NR 32)
                                  </label> 
                             </div>

                             <div class="form-check">
                                 <asp:CheckBox runat="server" type="checkbox" value="" id="chk_apt_Confinado" Visible="False" />
                                   <label class="texto form-check-label" for="defaultCheck1">
                                    Indicar no ASO aptidão para trabalho para espaços confinados (item 33.3.4.1 da NR 33)
                                  </label> 
                             </div>

                             <div class="form-check">
                                 <asp:CheckBox runat="server" type="checkbox" value="" id="chk_apt_Eletricidade" Visible="False" />
                                   <label class="texto form-check-label" for="defaultCheck1">
                                    Indicar no ASO aptidão para serviços em eletricidade (item 10.8.7 da NR 10)
                                  </label> 
                             </div>

                             <div class="form-check">
                                 <asp:CheckBox runat="server" type="checkbox" value="" id="chk_apt_Alimento" Visible="False" />
                                   <label class="texto form-check-label" for="defaultCheck1">
                                    Indicar no ASO aptidão para manipular alimentos (item 4.6.1 da Resolução RDC nº 216/2004)
                                  </label> 
                             </div>

                             <div class="form-check">
                                 <asp:CheckBox runat="server" type="checkbox" value="" id="chk_Apt_Brigadista" Visible="False"/>
                                   <label class="texto form-check-label" for="defaultCheck1">
                                    Indicar no ASO aptidão para Brigadista
                                  </label> 
                             </div>

                             <div class="form-check">
                                 <asp:CheckBox runat="server" type="checkbox" value="" id="chk_apt_Transportes" Visible="False"/>
                                   <label class="texto form-check-label" for="defaultCheck1">
                                    Indicar no ASO aptidão para operar equipamentos de transporte motorizados (item 11.1.6.1 da NR 11) 
                                  </label>
                             </div>

                             <div class="form-check">
                                 <asp:CheckBox runat="server" type="checkbox" value="" id="chk_apt_Submersas" Visible="False"/>
                                   <label class="texto form-check-label" for="defaultCheck1">
                                    Indicar no ASO aptidão para atividades submersas (anexo VI da NR 15)
                                  </label> 
                             </div>

                             <div class="form-check">
                                 <asp:CheckBox runat="server" type="checkbox" value="" id="chk_apt_Aquaviarios" Visible="False"/>
                                   <label class="texto form-check-label" for="defaultCheck1">
                                    Indicar no ASO aptidão para serviços aquaviários (item 30.5 da NR 30)
                                  </label>
                             </div>

                             <div class="form-check">
                                <asp:CheckBox runat="server" type="checkbox" value="" id="chk_Apt_Socorrista" Visible="False"/>
                                   <label class="texto form-check-label" for="defaultCheck1">
                                    Indicar no ASO aptidão para Socorrista
                                  </label> 
                             </div>

                             <div class="form-check">
                                 <asp:CheckBox runat="server" type="checkbox" value="" id="chk_Apt_Respiradores" Visible="False"/>
                                   <label class="texto form-check-label" for="defaultCheck1">
                                    Indicar no ASO aptidão para uso de respiradores
                                  </label> 
                             </div>

                             <br />

                             <div class="form-check">
                                  <asp:CheckBox type="checkbox" runat="server" value="" id="chk_eMail" />
                                  <label class="texto form-check-label" for="defaultCheck1">
                                    Enviar e-mail com Ficha Clínica para clínicas
                                  </label>
                             </div>
                          </fieldset>
                     </div>   
               </div>
           </div>

--%>         


                           
                                 <asp:CheckBox runat="server" type="checkbox" value="" id="chk_apt_Altura" Visible="False" />
                                 
                                 <asp:CheckBox runat="server" type="checkbox" value="" id="chk_apt_Confinado" Visible="False" />
                                
                                 <asp:CheckBox runat="server" type="checkbox" value="" id="chk_apt_Eletricidade" Visible="False" />
                                 
                                 <asp:CheckBox runat="server" type="checkbox" value="" id="chk_apt_Alimento" Visible="False" />
                                
                                 <asp:CheckBox runat="server" type="checkbox" value="" id="chk_Apt_Brigadista" Visible="False"/>
                                 
                                 <asp:CheckBox runat="server" type="checkbox" value="" id="chk_apt_Transportes" Visible="False"/>
                                 
                                 <asp:CheckBox runat="server" type="checkbox" value="" id="chk_apt_Submersas" Visible="False"/>
                                 
                                 <asp:CheckBox runat="server" type="checkbox" value="" id="chk_apt_Aquaviarios" Visible="False"/>
                                 
                                <asp:CheckBox runat="server" type="checkbox" value="" id="chk_Apt_Socorrista" Visible="False"/>
                                 
                                 <asp:CheckBox runat="server" type="checkbox" value="" id="chk_Apt_Respiradores" Visible="False"/>
                                
                             <div class="form-check">
                                  <asp:CheckBox type="checkbox" runat="server" value="" id="chk_eMail" />
                                  <label class="texto form-check-label" for="defaultCheck1">
                                    Enviar e-mail com Ficha Clínica para clínicas
                                  </label>
                             </div>

        

        <%-- FINAL DA PÁGINA --%>

            <div class="col-12">
                <div class="row">
                    <div class="col-12 text-center" style="padding-left: 0px !important;">
                        <div runat="server">
                            <asp:Button ID="btnEmpASO" runat="server" onclick="btnempASO_Click"  CssClass="btn" Text="Gerar Guia com ASO e Ficha Clínica" />
                            <asp:Button ID="btnEmpFicha" runat="server" onclick="btnempFicha_Click" CssClass="btn" Text="Gerar Guia com Ficha Clínica" />
                            <asp:Button ID="btnEmp2Via" onclick="btnEmp2Via_Click"  runat="server" CssClass="btn" Text="Gerar Segunda Via da Guia ASO e Ficha Clínica" />
                            <asp:Button ID="btnemp" onclick="btnemp_Click"  runat="server" CssClass="btn" Text="Gerar Guia" />
                            
                        </div>
                    </div>

                    <div class="col-12 gy-2" style="padding-left: 0px !important;">
                        <div runat="server">
                            <asp:Button ID="cmd_Voltar" runat="server" CssClass="btn2" Text="Voltar" onclick="cmd_Voltar_Click"   />
                            <asp:ListBox ID="lst_IdExames" runat="server" Height="3px" Visible="False"></asp:ListBox>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

     <eo:MsgBox ID="MsgBox1" runat="server" BackColor="#47729F" ControlSkinID="None" OnButtonClick="MsgBox1_ButtonClick" 
        HeaderHtml="Dialog Title" Height="100px" Width="250px" >
        <HeaderStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #ffbf00 3px solid; padding-left: 4px; font-weight: bold; font-size: 11px; padding-bottom: 2px; color: white; padding-top: 2px; border-bottom: #22456a 1px solid; font-family: verdana" />
        <ContentStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #7d97b6 1px solid; padding-left: 4px; border-left-width: 1px; font-size: 11px; border-left-color: #728eb8; padding-bottom: 4px; color: white; padding-top: 4px; border-bottom: #22456a 1px solid; font-family: verdana" />
        <FooterStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #7d97b6 1px solid; padding-left: 4px; border-left-width: 1px; font-size: 11px; border-left-color: #728eb8; padding-bottom: 4px; color: white; padding-top: 4px; border-bottom: #22456a 1px solid; font-family: verdana" />
        </eo:MsgBox>
         <eo:MsgBox ID="MsgBox2" runat="server" BackColor="#47729F" ControlSkinID="None" OnButtonClick="MsgBox2_ButtonClick" 
        HeaderHtml="Dialog Title" Height="350px" Width="650px" >
        <HeaderStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #ffbf00 3px solid; padding-left: 4px; font-weight: bold; font-size: 11px; padding-bottom: 2px; color: white; padding-top: 2px; border-bottom: #22456a 1px solid; font-family: verdana" />
        <ContentStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #7d97b6 1px solid; padding-left: 4px; border-left-width: 1px; font-size: 11px; border-left-color: #728eb8; padding-bottom: 4px; color: white; padding-top: 4px; border-bottom: #22456a 1px solid; font-family: verdana" />
        <FooterStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #7d97b6 1px solid; padding-left: 4px; border-left-width: 1px; font-size: 11px; border-left-color: #728eb8; padding-bottom: 4px; color: white; padding-top: 4px; border-bottom: #22456a 1px solid; font-family: verdana" />
        </eo:MsgBox>
</asp:Content>