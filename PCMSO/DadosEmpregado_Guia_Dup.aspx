<%@ Page Language="C#"   AutoEventWireup="True"  MasterPageFile="~/Site.Master" CodeBehind="DadosEmpregado_Guia_Dup.aspx.cs"  Inherits="Ilitera.Net.DadosEmpregado_Guia_Dup" Title="Ilitera.Net" %>

<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>


<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <link href="css/forms.css" rel="stylesheet" type="text/css" />
    <link href="css/tabelas.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        #txtIdUsuario
        {
            width: 0px;
        }
        #txtIdUsuario0
        {
            width: 0px;
        }
        #txtIdEmpregado
        {
            width: 0px;
        }
        #txtIdEmpregado0
        {
            width: 0px;
        }
        #txtIdEmpresa
        {
            width: 0px;
        }
        #SubDados
        {
            height: 317px;
        }
        #Table1
        {
            width: 713px;
        }
        #Table2
        {
            width: 725px;
        }
        .buttonFoto
        {}
        #Table13
        {
            height: 12px;
            width: 97px;
        }
    
.largeboldFont
{
	font: Bold 8pt Verdana;
	color:#44926D;
	text-decoration: underline;
}

        .style1
        {
            height: 26px;
        }
        .style2
        {
            font-size: x-small;
        }
        .style3
        {
            width: 372px;
        }
        .style4
        {
            height: 41px;
        }
    .style5
    {
        width: 357px;
    }
    </style>
</asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent" >
    <div class="container-fluid d-flex ms-5 ps-4">
        <div class="row gx-3 gy-2">

     <LINK href="scripts/style.css" type="text/css" rel="stylesheet">
	<script language="javascript">

	    function Reload() {
	        var f = document.getElementById('SubDados');
	        //f.src = f.src;
	        f.contentWindow.location.reload(true);
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
             
            <%-- primeiro container --%>
            <div class="col-11 subtituloBG mb-3" style="padding-top: 10px;">
                <asp:Label runat="server" class="subtitulo">Dados do Empregado</asp:Label>
            </div>

            <div class="col-11">
                <div class="row">
                    <div class="col-md-4 gx-3 gy-2">
                        <fieldset>
                            <asp:Label ID="lblNome" runat="server" Text="Nome" class="tituloLabel form-label col-form-label col-form-label-sm" Style="text-align: left;"></asp:Label>
                            <asp:Label ID="lblValorNome" runat="server" class="texto form-control form-control-sm" Style="text-align: left;"></asp:Label>
                        </fieldset>
                    </div>

                    <div class="col-md-3 gx-3 gy-2">
                        <fieldset>
                            <asp:Label ID="lblTipoBene" runat="server" Text="Tipo de Beneficiário" class="tituloLabel form-label col-form-label col-form-label-sm" Style="text-align: left;"></asp:Label>
                            <asp:Label ID="lblValorBene" runat="server" class="texto form-control form-control-sm" Style="text-align: left;"></asp:Label>
                        </fieldset>
                    </div>
                    
                    <div class="col-md-2 gx-3 gy-2">
                        <fieldset>
                            <asp:Label ID="lblDataNascimento" runat="server" Text="Nascimento" CssClass="tituloLabel form-label col-form-label col-form-label-sm"></asp:Label>
                            <asp:Label ID="lblValorNasc" runat="server" CssClass="texto form-control form-control-sm" type="date"></asp:Label>
                        </fieldset>
                    </div>

                    <div class="col-md-1 gx-3 gy-2">
                        <fieldset>
                            <asp:Label ID="lblIdade" runat="server" Text="Idade" class="tituloLabel form-label col-form-label col-form-label-sm" Style="text-align: left;"></asp:Label>
                            <asp:Label ID="lblValorIdade" runat="server" class="texto form-control form-control-sm" Style="text-align: left;"></asp:Label>
                        </fieldset>
                    </div>

                    <div class="col-md-2 gx-3 gy-2">
                        <fieldset>
                            <asp:Label ID="lblSexo" runat="server" Text="Sexo" class="tituloLabel form-label col-form-label col-form-label-sm" Style="text-align: left;"></asp:Label>
                            <asp:Label ID="lblValorSexo" runat="server" class="texto form-control form-control-sm" Style="text-align: left;"></asp:Label>
                        </fieldset>
                    </div>

                    <div class="col-md-2 gx-3 gy-2">
                        <fieldset>
                            <asp:Label ID="lblAdmissao" runat="server" Text="Admissão" CssClass="tituloLabel form-label col-form-label col-form-label-sm"></asp:Label>
                            <asp:Label ID="lblValorAdmissao" runat="server" CssClass="texto form-control form-control-sm" type="date"></asp:Label>
                        </fieldset>
                    </div>
                    
                    <div class="col-md-2 gx-3 gy-2">
                          <fieldset>
                            <asp:Label ID="Label2" runat="server" Text="Demissão" CssClass="tituloLabel form-label col-form-label col-form-label-sm"></asp:Label>
                            <asp:Label ID="lblValorDemissao" runat="server" CssClass="texto form-control form-control-sm" type="date"></asp:Label>
                        </fieldset>
                    </div>

                     <div class="col-3 gx-3 gy-2">
                         <fieldset>
                            <asp:Label ID="lblRegistro" runat="server" Text="RE" class="tituloLabel form-label col-form-label col-form-label-sm" Style="text-align: left;"></asp:Label>
                            <asp:Label ID="lblValorRegistro" runat="server" class="texto form-control form-control-sm" Style="text-align: left;"></asp:Label>
                        </fieldset>
                      </div>

                      <div class="col-3 gx-3 gy-2">
                          <fieldset> 
                              <asp:Label ID="lblRegRev" runat="server" Text="Regime de Revezamento" class="tituloLabel form-label col-form-label col-form-label-sm" Style="text-align: left;"></asp:Label>
                              <asp:Label ID="lblValorRegRev" runat="server" class="texto form-control form-control-sm" Style="text-align: left;"></asp:Label>
                          </fieldset>
                      </div>

                    <div class="col-md-2 gx-3 gy-2">
                        <fieldset>
                            <asp:Label ID="lblGHE" runat="server" Text="Tempo de Empresa" CssClass="tituloLabel form-label col-form-label col-form-label-sm"></asp:Label>
                            <asp:Label ID="lblValorTempoEmpresa" runat="server" CssClass="texto form-control form-control-sm" ></asp:Label>
                        </fieldset>
                      </div>

                      <div class="col-md-2 gx-3 gy-2">
                          <fieldset>
                            <asp:Label ID="lblDataIni" runat="server" Text="Início da Função" CssClass="tituloLabel form-label col-form-label col-form-label-sm"></asp:Label>
                            <asp:Label ID="lblValorDataIni" runat="server" CssClass="texto form-control form-control-sm" text="27/03/2015" type="date"></asp:Label>
                        </fieldset>
                      </div>

                      <div class="col-md-4 gx-3 gy-2">
                          <fieldset>
                            <asp:Label ID="lblSetor" runat="server" Text="Setor" class="tituloLabel form-label col-form-label col-form-label-sm" Style="text-align: left;"></asp:Label>
                            <asp:Label ID="lblValorSetor" runat="server" class="texto form-control form-control-sm" Style="text-align: left;"></asp:Label>
                        </fieldset>
                      </div>

                      <div class="col-md-4 gx-3 gy-2">
                          <fieldset>
                              <asp:Label ID="lblFuncao" runat="server" Text="Função" class="tituloLabel form-label col-form-label col-form-label-sm" Style="text-align: left;"></asp:Label>
                              <asp:Label ID="lblValorFuncao" runat="server" class="texto form-control form-control-sm" Style="text-align: left;"></asp:Label>
                          </fieldset>
                      </div> 
                </div>
            </div>

            <asp:label id="lblJornada" runat="server" CssClass="textDadosOpcao" Visible="False">Jornada</asp:label>
            <asp:label id="lblValorJornada" runat="server" Visible="False"></asp:label>
            <asp:Label id="Label4" runat="server" CssClass="largeboldFont" Visible="false">Guia de Encaminhamento</asp:Label>
            <input id="txtIdUsuario" type="text" visible="False"  style="visibility:hidden"  />
            <input id="txtIdEmpregado" type="text" visible="False" style="visibility:hidden" />
            <input id="txtIdEmpresa" type="text" visible="False" style="visibility:hidden"/>

            <div class="col-11 mb-3">
              <div class="form-check ms-3">
                <asp:CheckBox runat="server" type="checkbox" value="" id="chk_Basico" />
                  <label class="texto form-check-sm-label bg-transparent border-0" for="flexCheckDefault">
                    Imprimir apenas Nome, Setor e GHE
                </label>
              </div>
            </div>

            <%-- segundo container --%>

            <div class="col-11 subtituloBG" style="padding-left: 0px !important;">
                <asp:Image ID="menu1" runat="server" AlternateText="2_menu" ImageAlign="Left" ImageUrl="images/numero_1.png" CssClass="image1"/>
                <h2 class="subtitulo">Escolha da Clínica para Realização do Exame</h2>
            </div>

            <div class="col-11">
                <div class="row">
                    <div class="col-md-6 gx-3 gy-2">
                        <asp:Label runat="server" Text="Clínicas Credenciadas" class="tituloLabel form-label col-form-label col-form-label-sm"></asp:Label>
                        <asp:DropDownList ID="cmb_Clinicas2" runat="server" AutoPostBack="True" CssClass="texto form-select form-select-sm" onselectedindexchanged="cmb_Clinicas2_SelectedIndexChanged"></asp:DropDownList>
                        <asp:Label ID="lbl_Id_Clinica" runat="server" Text="0" Visible="False"></asp:Label>
                    </div>

                    <div class="col-md-6 gx-3 gy-2">
                       <fieldset>
                           <asp:Label runat="server" Text="Endereço da Clínica Selecionada" class="tituloLabel form-label col-form-label col-form-label-sm" Style="text-align: left;"></asp:Label>
                            <asp:Label ID="lbl_End_Clinica2" runat="server" class="texto form-control form-control-sm" Style="text-align: left;"></asp:Label>
                       </fieldset>
                   </div>

                  <div class="col-md-4 gx-3 gy-2 mb-3">
                       <fieldset>
                           <asp:Label runat="server" Text="Telefone da Clínica" class="tituloLabel form-label col-form-label col-form-label-sm" Style="text-align: left;"></asp:Label>
                            <asp:Label ID="lbl_Fone_Clinica2" runat="server" class="texto form-control form-control-sm" Style="text-align: left;"></asp:Label>
                       </fieldset>
                   </div>

                   <div class="col-md-4 gx-3 gy-2 mb-3">
                       <fieldset>
                           <asp:Label runat="server" Text="Horário de Atendimento" class="tituloLabel form-label col-form-label col-form-label-sm" Style="text-align: left;"></asp:Label>
                            <asp:Label ID="lbl_Horario2" runat="server" class="texto form-control form-control-sm" Style="text-align: left;"></asp:Label>
                       </fieldset>
                   </div>
           
                   <div class="col-md-4 gx-3 gy-2 mb-3">
                       <fieldset>
                           <asp:Label runat="server" Text="E-Mail da Clínica" class="tituloLabel form-label col-form-label col-form-label-sm" Style="text-align: left;" Visible="false"></asp:Label>
                            <asp:Label ID="lbl_email2" runat="server" class="texto form-control form-control-sm" Style="text-align: left;" Visible="False"></asp:Label>
                       </fieldset>
                   </div>
                </div>
            </div>
            

            <%-- terceiro container --%>

            <div class="col-11 subtituloBG" >
              <h2 class="subtitulo">Escolha do Exame a ser Realizado</h2>
            </div>

            <div clss="col-11 mb-3">
                <div class="row">
                    <div class="col-md-3 gx-3 gy-2">
                        <fieldset>
                            <div class="subtituloBG mb-2">
                                 <asp:Image runat="server" AlternateText="2_menu" ImageAlign="Left" ImageUrl="images/numero_2.png" CssClass="image2"/>
                                 <h2 class="subtitulo" style="padding-top: .8rem;">Selecione o tipo de Exame</h2>
                             </div>
                            <div class="form-check ms-2">
                                <asp:RadioButton ID="rd_Admissao2" Text="Admissão" runat="server" class="form-check-label texto" GroupName="Tipo2" AutoPostBack="True" CausesValidation="True" oncheckedchanged="rd_Admissao2_CheckedChanged" />      
                            </div>

                            <div class="form-check ms-2">
                                <asp:RadioButton ID="rd_Demissional2" Text="Demissional" runat="server" class="form-check-label texto" GroupName="Tipo2" AutoPostBack="True" CausesValidation="True" oncheckedchanged="rd_Demissional2_CheckedChanged" />
                            </div>

                            <div class="form-check ms-2">
                                <asp:RadioButton ID="rd_Mudanca2" Text="Mudança de Risco Ocupacional" runat="server" class="form-check-label texto" GroupName="Tipo2" AutoPostBack="True" CausesValidation="True" oncheckedchanged="rd_Mudanca2_CheckedChanged" />
                            </div>

                            <div class="form-check ms-2">
                                <asp:RadioButton ID="rd_Retorno2" Text="Retorno ao Trabalho" runat="server" GroupName="Tipo2" class="form-check-label texto" AutoPostBack="True" CausesValidation="True" oncheckedchanged="rd_Retorno2_CheckedChanged" />
                            </div>

                            <div class="form-check ms-2">
                                <asp:RadioButton ID="rd_Periodico2" Text="Periódico" runat="server" class="form-check-label texto" GroupName="Tipo2" AutoPostBack="True" CausesValidation="True" oncheckedchanged="rd_Periodico2_CheckedChanged" />
                            </div>

                            <div class="form-check ms-2">
                                <asp:RadioButton ID="rd_Outro2" Text="Outro" Checked="true" runat="server" class="form-check-label texto" GroupName="Tipo2" AutoPostBack="True" CausesValidation="True" />
                            </div>
                        </fieldset>
                    </div>

                    <div class="col-md-4 gx-3 gy-2">
                        <div class="row">
                            <fieldset>
                                <div class="subtituloBG mb-2">
                                    <asp:Image runat="server" AlternateText="3_menu" ImageAlign="Left" ImageUrl="images/numero_3.png" CssClass="image2"/>
                                    <h2 class="subtitulo" style="padding-top: .8rem;">Data do Exame</h2>
                                 </div>
                            </fieldset>

                            <div class="col-6 gx-3">
                                <asp:Label runat="server" CssClass="tituloLabel form-label">Data do Exame</asp:Label>
                                <asp:TextBox ID="txt_Data2" runat="server" CssClass="texto form-control form-control-sm" MaxLength="10" OnTextChanged="txt_Data2_TextChanged"></asp:TextBox>
                            </div>

                            <div class="col-6 gx-3">
                                <asp:Label runat="server" CssClass="tituloLabel form-label">Hora do Exame</asp:Label>
                                <asp:TextBox ID="txt_Hora2" runat="server" CssClass="texto form-control form-control-sm" MaxLength="10"></asp:TextBox>
                            </div>

                            <div class="col-12 gx-3 gy-2 mb-2">
                                <asp:Label runat="server" CssClass="tituloLabel form-label">Observações</asp:Label>
                                <asp:TextBox ID="txt_Obs2" runat="server" CssClass="texto form-control form-control-sm" MaxLength="50" style="min-height: 70px;"></asp:TextBox>
                            </div>

                            <div class="form-check gx-3 gy-2 ms-2">
                                <fieldset>
                                    <asp:CheckBox ID="chk_Data2" runat="server" CssClass="form-check-label texto" Text="Exibir Data na guia" Checked="True" />
                                </fieldset>
                            </div>
                        </div>
                    </div>

                    <div class="col-md-4 gx-3 gy-2">
                        <div class="row">
                            <fieldset>
                                <div class="subtituloBG mb-2">
                                    <asp:Image runat="server" AlternateText="3_menu" ImageAlign="Left" ImageUrl="images/numero_4.png" CssClass="image2"/>
                                    <h2 class="subtitulo" style="padding-top: .8rem;">Opções</h2>
                                 </div>

                                <div class="form-check ms-2">
                                    <asp:CheckBox ID="chk_apt_Altura" runat="server" CssClass="form-check-label texto" Text="Indicar no ASO aptidao para trabalho em altura" />
                                </div>

                                <div class="form-check ms-2">
                                    <asp:CheckBox ID="chk_apt_Confinado" runat="server" CssClass="form-check-label texto" Text="Indicar no ASO aptidao para trabalho para espaços confinados" />
                                </div>

                                <div class="form-check ms-2">
                                    <asp:CheckBox ID="chk_apt_Transportes" runat="server" AutoPostBack="True" CssClass="form-check-label texto" Text="Indicar no ASO aptidao para operar equipamentos de transporte motorizados" />
                                </div>

                                <div class="form-check ms-2">
                                    <asp:CheckBox ID="chk_apt_Eletricidade" runat="server" CssClass="form-check-label texto" Text="Indicar no ASO aptidao para serviços em eletricidade (NR 10)" />
                                </div>

                                <div class="form-check ms-2">
                                    <asp:CheckBox ID="chk_apt_Submersas" runat="server" CssClass="form-check-label texto" Text="Indicar no ASO aptidao para atividades submersas (NR 15)" />
                                </div>

                                <div class="form-check ms-2">
                                    <asp:CheckBox ID="chk_apt_Aquaviarios" runat="server" CssClass="form-check-label texto" Text="Indicar no ASO aptidao para serviços aquaviários" />
                                </div>

                                <div class="form-check ms-2">
                                    <asp:CheckBox ID="chk_apt_Alimento" runat="server" CssClass="form-check-label texto" Text="Indicar no ASO aptidao para manipular alimentos" />
                                </div>

                                <div class="form-check ms-2">
                                    <asp:CheckBox ID="chk_Apt_Brigadista" runat="server" CssClass="form-check-label texto" AutoPostBack="True" Text="Indicar no ASO aptidao para Brigadista" />
                                </div>

                                <div class="form-check ms-2">
                                    <asp:CheckBox ID="chk_Apt_Socorrista" runat="server" CssClass="form-check-label texto" AutoPostBack="True" Text="Indicar no ASO aptidao para Socorrista" />
                                </div>

                                <div class="form-check ms-2">
                                    <asp:CheckBox ID="chk_Apt_Respiradores" runat="server" CssClass="form-check-label texto" AutoPostBack="True" Text="Indicar no ASO aptidao para uso de respiradores" />
                                </div>
                            </fieldset>
                        </div>
                    </div>
                </div>
            </div>

            <div class="col-11 mb-3">
                    <div class="row">
                        <div class="col-6 text-end">
                            <asp:Button ID="btnEmpASO" runat="server" CssClass="btn" Text="Gerar Guia com ASO/Ficha Clínica" onclick="btnempASO_Click"></asp:Button>
                        </div>

                        <div class="col-6 text-start ps-4 pt-1">
                            <fieldset>
                                <asp:CheckBox ID="chk_eMail2" runat="server" Text="Enviar e-mail com Guia/ASO/Ficha Clínica" CssClass="form-check-label texto" />
                            </fieldset>
                        </div>
                </div>
            </div>

            <%-- exame complementar --%>

            <div class="col-11 subtituloBG mb-2" style="padding-top: 10px;">
                <asp:Label ID="Label5" runat="server" class="subtitulo">Exame Complementar</asp:Label>
            </div>

            <div class="col-11 subtituloBG" style="padding-left: 0px !important;">
                <asp:Image ID="Image1" runat="server" AlternateText="2_menu" ImageAlign="Left" ImageUrl="images/numero_1.png" CssClass="image1"/>
                <h2 class="subtitulo">Escolha da Clínica para Realização do Exame</h2>
            </div>

            <div class="col-11 mb-3">
                <div class="row">
                    <div class="col-md-6 gx-3 gy-2">
                        <asp:Label runat="server" Text="Clínicas Credenciadas" class="tituloLabel form-label col-form-label col-form-label-sm"></asp:Label>
                        <asp:DropDownList  ID="cmb_Clinicas" runat="server" AutoPostBack="True" CssClass="texto form-select form-select-sm" onselectedindexchanged="cmb_Clinicas_SelectedIndexChanged"></asp:DropDownList>
                    </div>

                    <div class="col-md-6 gx-3 gy-2">
                       <fieldset>
                           <asp:Label runat="server" Text="Endereço da Clínica Selecionada" class="tituloLabel form-label col-form-label col-form-label-sm" Style="text-align: left;"></asp:Label>
                            <asp:Label ID="lbl_End_Clinica" runat="server" class="texto form-control form-control-sm" Style="text-align: left;"></asp:Label>
                       </fieldset>
                   </div>

                  <div class="col-md-4 gx-3 gy-2 mb-3">
                       <fieldset>
                           <asp:Label runat="server" Text="Telefone da Clínica" class="tituloLabel form-label col-form-label col-form-label-sm" Style="text-align: left;"></asp:Label>
                            <asp:Label ID="lbl_Fone_Clinica" runat="server" class="texto form-control form-control-sm" Style="text-align: left;"></asp:Label>
                       </fieldset>
                   </div>

                   <div class="col-md-4 gx-3 gy-2 mb-3">
                       <fieldset>
                           <asp:Label runat="server" Text="Horário de Atendimento" class="tituloLabel form-label col-form-label col-form-label-sm" Style="text-align: left;"></asp:Label>
                            <asp:Label ID="lbl_Horario" runat="server" class="texto form-control form-control-sm" Style="text-align: left;"></asp:Label>
                       </fieldset>
                   </div>
           
                   <div class="col-md-4 gx-3 gy-2 mb-3">
                       <fieldset>
                           <asp:Label runat="server" Text="E-Mail da Clínica" class="tituloLabel form-label col-form-label col-form-label-sm" Style="text-align: left;" Visible="false"></asp:Label>
                            <asp:Label ID="lbl_email" runat="server" class="texto form-control form-control-sm" Style="text-align: left;" Visible="False"></asp:Label>
                       </fieldset>
                   </div>
                </div>
            </div>

            <div class="col-11 subtituloBG">
              <h2 class="subtitulo">Escolha do Exame a ser Realizado</h2>
            </div>

            <div class="col-11 mb-3">
                <div class="row">
                    <div class="col-md-4 gx-3 gy-2">
                        <fieldset>
                            <div class="subtituloBG mb-2">
                                 <asp:Image runat="server" AlternateText="2_menu" ImageAlign="Left" ImageUrl="images/numero_2.png" CssClass="image2"/>
                                 <h2 class="subtitulo" style="padding-top: .8rem;">Selecione o tipo de Exame</h2>
                             </div>
                            <div class="form-check ms-2">
                                <asp:RadioButton ID="rd_Admissao" Text="Admissão" runat="server" class="form-check-label texto" GroupName="Tipo2" AutoPostBack="True" CausesValidation="True" oncheckedchanged="rd_Admissao_CheckedChanged" />      
                            </div>

                            <div class="form-check ms-2">
                                <asp:RadioButton ID="rd_Demissional" Text="Demissional" runat="server" class="form-check-label texto" GroupName="Tipo2" AutoPostBack="True" CausesValidation="True" oncheckedchanged="rd_Demissional_CheckedChanged" />
                            </div>

                            <div class="form-check ms-2">
                                <asp:RadioButton ID="rd_Mudanca" Text="Mudança de Risco Ocupacional" runat="server" class="form-check-label texto" GroupName="Tipo2" AutoPostBack="True" CausesValidation="True" oncheckedchanged="rd_Mudanca_CheckedChanged" />
                            </div>

                            <div class="form-check ms-2">
                                <asp:RadioButton ID="rd_Retorno" Text="Retorno ao Trabalho" runat="server" GroupName="Tipo2" class="form-check-label texto" AutoPostBack="True" CausesValidation="True" oncheckedchanged="rd_Retorno_CheckedChanged" />
                            </div>

                            <div class="form-check ms-2">
                                <asp:RadioButton ID="rd_Periodico" Text="Periódico" runat="server" class="form-check-label texto" GroupName="Tipo2" AutoPostBack="True" CausesValidation="True" oncheckedchanged="rd_Periodico_CheckedChanged" />
                            </div>

                            <div class="form-check ms-2">
                                <asp:RadioButton ID="rd_Outro" Text="Outro" Checked="true" runat="server" class="form-check-label texto" GroupName="Tipo2" AutoPostBack="True" CausesValidation="True" />
                            </div>
                        </fieldset>
                    </div>

                    <div class="col-md-4 gx-3 gy-2">
                        <div class="row">
                            <fieldset>
                                <div class="subtituloBG mb-2">
                                    <asp:Image runat="server" AlternateText="3_menu" ImageAlign="Left" ImageUrl="images/numero_3.png" CssClass="image2"/>
                                    <h2 class="subtitulo" style="padding-top: .8rem;">Data do Exame</h2>
                                 </div>
                            </fieldset>

                            <div class="col-6 gx-3">
                                <asp:Label runat="server" CssClass="tituloLabel form-label">Data do Exame</asp:Label>
                                <asp:TextBox ID="txt_Data" runat="server" CssClass="texto form-control form-control-sm" MaxLength="10" OnTextChanged="txt_Data_TextChanged"></asp:TextBox>
                            </div>

                            <div class="col-6 gx-3">
                                <asp:Label runat="server" CssClass="tituloLabel form-label">Hora do Exame</asp:Label>
                                <asp:TextBox ID="txt_Hora" runat="server" CssClass="texto form-control form-control-sm" MaxLength="10"></asp:TextBox>
                            </div>

                            <div class="col-12 gx-3 gy-2 mb-2">
                                <asp:Label runat="server" CssClass="tituloLabel form-label">Observações</asp:Label>
                                <asp:TextBox ID="txt_Obs" runat="server" CssClass="texto form-control form-control-sm" MaxLength="50" style="min-height: 70px;"></asp:TextBox>
                            </div>

                            <div class="form-check gx-3 gy-2 ms-2">
                                <fieldset>
                                    <asp:CheckBox ID="chk_Data" runat="server" CssClass="form-check-label texto" Text="Exibir Data na guia" Checked="True" />
                                </fieldset>
                            </div>
                        </div>
                    </div>

                    <div class="col-md-4 gx-3 gy-2">
                        <div class="row">
                            <fieldset>
                                <div class="subtituloBG mb-2">
                                    <h2 class="subtitulo" style="padding-top: .8rem;">Exames PCMSO</h2>
                                 </div>
                            </fieldset>

                            <div class="col-12 gx-3 gy-2 mb-2">
                                <asp:TextBox ID="txt_Exames" runat="server" Height="154px" TextMode="MultiLine" CssClass="texto form-control"></asp:TextBox>
                            </div>

                            <fieldset>
                                <div class="subtituloBG mb-2">
                                    <h2 class="subtitulo" style="padding-top: .8rem;">Exames Selecionados</h2>
                                 </div>
                            </fieldset>

                            <div class="col-12 gx-3 gy-2">
                                <asp:CheckBoxList ID="lst_Exames" runat="server" style="text-align:left" Height="94px" Width="370px" CssClass="texto form-check-input bg-transparent border-0" RepeatColumns="2"></asp:CheckBoxList>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="col-11 mb-3">
                    <div class="row">
                        <div class="col-6 text-end mb-3">
                            <asp:Button ID="btnEmpComp" Text="Gerar Guia de Complementar" runat="server" CssClass="btn" onclick="btnempComp_Click"></asp:Button>
                        </div>

                        <div class="col-6 text-start ps-4 pt-1 mb-3">
                            <fieldset>
                                <asp:CheckBox ID="chk_eMail" Text="Enviar e-mail com Guia de complementar" runat="server" CssClass="form-check-label texto" />
                            </fieldset>
                        </div>

                        <div class="col-12 text-start">
                            <asp:Button ID="cmd_Voltar" runat="server" CssClass="btn2" Text="Voltar" onclick="cmd_Voltar_Click" /> 
                        </div>
                    </div>
            </div>

            <asp:CheckBoxList ID="lst_Id_Exames"  BackColor="#FFFFCC"  runat="server" style="text-align:left"
                            BorderStyle="Solid" Height="16px" Width="63px" RepeatColumns="2" 
                            BorderColor="#363636" Font-Size="X-Small" Visible="False"    >
                        </asp:CheckBoxList>

<%--		</form>
	</body>
</HTML>
--%>
        
         <eo:MsgBox ID="MsgBox1" runat="server" BackColor="#47729F" ControlSkinID="None" 
        HeaderHtml="Dialog Title" Height="100px" Width="250px" OnButtonClick="MsgBox1_ButtonClick">
        <HeaderStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #ffbf00 3px solid; padding-left: 4px; font-weight: bold; font-size: 11px; padding-bottom: 2px; color: white; padding-top: 2px; border-bottom: #22456a 1px solid; font-family: verdana" />
        <ContentStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #7d97b6 1px solid; padding-left: 4px; border-left-width: 1px; font-size: 11px; border-left-color: #728eb8; padding-bottom: 4px; color: white; padding-top: 4px; border-bottom: #22456a 1px solid; font-family: verdana" />
        <FooterStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #7d97b6 1px solid; padding-left: 4px; border-left-width: 1px; font-size: 11px; border-left-color: #728eb8; padding-bottom: 4px; color: white; padding-top: 4px; border-bottom: #22456a 1px solid; font-family: verdana" />
        </eo:MsgBox>
         <eo:MsgBox ID="MsgBox2" runat="server" BackColor="#47729F" ControlSkinID="None" 
        HeaderHtml="Dialog Title" Height="350px" Width="650px" OnButtonClick="MsgBox2_ButtonClick">
        <HeaderStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #ffbf00 3px solid; padding-left: 4px; font-weight: bold; font-size: 11px; padding-bottom: 2px; color: white; padding-top: 2px; border-bottom: #22456a 1px solid; font-family: verdana" />
        <ContentStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #7d97b6 1px solid; padding-left: 4px; border-left-width: 1px; font-size: 11px; border-left-color: #728eb8; padding-bottom: 4px; color: white; padding-top: 4px; border-bottom: #22456a 1px solid; font-family: verdana" />
        <FooterStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #7d97b6 1px solid; padding-left: 4px; border-left-width: 1px; font-size: 11px; border-left-color: #728eb8; padding-bottom: 4px; color: white; padding-top: 4px; border-bottom: #22456a 1px solid; font-family: verdana" />
        </eo:MsgBox>

        </div>    
    </div>
</asp:Content>
