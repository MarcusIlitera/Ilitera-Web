<%@ Page language="c#" Inherits="Ilitera.Net.CadAcidente" Codebehind="CadAcidente.aspx.cs" Title="Ilitera.Net"%>

<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>


<HTML>
	<HEAD>
		<link href="https://cdn.jsdelivr.net/npm/bootstrap@5.2.3/dist/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
        <link href="css/forms.css" rel="stylesheet" type="text/css" />
        <link href="css/tabelas.css" rel="stylesheet" type="text/css" />
		<LINK href="scripts/style.css" type="text/css" rel="stylesheet">
		<script language="javascript">

		function VerificaDataAcidente()
		{
			return validar_data(document.frmCadAcidente.uwtAcidentes__ctl0_tabtxtdda.value, document.frmCadAcidente.uwtAcidentes__ctl0_tabtxtmma.value, document.frmCadAcidente.uwtAcidentes__ctl0_tabtxtaaa.value, 'Data do Acidente');
		}
		
		function VerificaDataCAT()
		{
			if (validar_data(document.frmCadAcidente.uwtAcidentes__ctl4_tabuwtCAT__ctl0_tabtxtDiaCAT.value, document.frmCadAcidente.uwtAcidentes__ctl4_tabuwtCAT__ctl0_tabtxtMesCAT.value, document.frmCadAcidente.uwtAcidentes__ctl4_tabuwtCAT__ctl0_tabtxtAnoCAT.value, 'Data da CAT'))
				if (!document.frmCadAcidente.uwtAcidentes__ctl4_tabuwtCAT__ctl0_tabtxtddo.disabled)
					if (validar_data(document.frmCadAcidente.uwtAcidentes__ctl4_tabuwtCAT__ctl0_tabtxtddo.value, document.frmCadAcidente.uwtAcidentes__ctl4_tabuwtCAT__ctl0_tabtxtmmo.value, document.frmCadAcidente.uwtAcidentes__ctl4_tabuwtCAT__ctl0_tabtxtaao.value, 'Data de Óbito da CAT'))
						return true;
					else
						return false;
				else
					return true;
			else
				return false;
		}
		
		function VerificaDataAbsentismo()
		{
			var Inicial = validar_data(document.frmCadAcidente.uwtAcidentes__ctl5_tabtxtddi.value, document.frmCadAcidente.uwtAcidentes__ctl5_tabtxtmmi.value, document.frmCadAcidente.uwtAcidentes__ctl5_tabtxtaai.value, 'Data Inicial do Absentismo');
			var Prevista;
			var Retorno;
			
			if (isNull(document.frmCadAcidente.uwtAcidentes__ctl5_tabtxtddp.value) && isNull(document.frmCadAcidente.uwtAcidentes__ctl5_tabtxtmmp.value) && isNull(document.frmCadAcidente.uwtAcidentes__ctl5_tabtxtaap.value))
				Prevista = true;
			else
				Prevista = validar_data(document.frmCadAcidente.uwtAcidentes__ctl5_tabtxtddp.value, document.frmCadAcidente.uwtAcidentes__ctl5_tabtxtmmp.value, document.frmCadAcidente.uwtAcidentes__ctl5_tabtxtaap.value, 'Data Prevista de retorno do Absentismo');
			
			if (isNull(document.frmCadAcidente.uwtAcidentes__ctl5_tabtxtddr.value) && isNull(document.frmCadAcidente.uwtAcidentes__ctl5_tabtxtmmr.value) && isNull(document.frmCadAcidente.uwtAcidentes__ctl5_tabtxtaar.value))
				Retorno = true;
			else
				Retorno = validar_data(document.frmCadAcidente.uwtAcidentes__ctl5_tabtxtddr.value, document.frmCadAcidente.uwtAcidentes__ctl5_tabtxtmmr.value, document.frmCadAcidente.uwtAcidentes__ctl5_tabtxtaar.value, 'Data de Retorno do Absentismo');
			
			return (Inicial && Prevista && Retorno);
		}
		

		</script>
	    <style type="text/css">
            @font-face{
                font-family: "Univia Pro";
                src: url("css/UniviaPro-Regular.otf");
            }
            @font-face{
                font-family: "Ubuntu";
                src: url("css/Ubuntu-Regular.ttf");
            }

            .btnProcurar {
                min-width: fit-content;
                height: 32px; 
                padding-right: 10px;
                padding-left: 10px; 
                font-family: 'Univia Pro' !important;
                font-style: normal;
                font-weight: normal;
                font-size: 12px;
                /*text-align: center;*/
                color: #ffffff !important;
                background: linear-gradient(180deg, #48A79E 54.35%, #1C9489 54.36%);
                border-radius: 5px;
                border: none;
            }

            .btnProcurar:hover {
                color: #ffffff !important;
                background: linear-gradient(180deg, #F2B988 53.35%, #F09E60 53.36%);
            }

            .auto-style1 {
                width: 360px;
            }
            .auto-style2 {
                height: 44px;
            }
            .auto-style3 {
                height: 30px;
                width: 451px;
            }
            .auto-style4 {
                height: 44px;
                width: 308px;
            }
            .auto-style5 {
                width: 490px;
            }

            

            </style>
	</HEAD>

	<body bottomMargin="0" leftMargin="0" topMargin="0" onload="Inicialize()" rightMargin="0">
		<form method="post" id="frmCadAcidente" runat="server">
            <div class="container d-flex ms-5 ps-4 justify-content-center">
                <div class="row gx-3 gy-2 text-center" style="width: 923px">

                  <%-- subtitulo --%>

                    <div class="col-11 subtituloBG mb-3" style="padding-top: 10px; margin-top: 50px;">
                        <asp:Label ID="lblAcidentes" runat="server" SkinID="TitleFont" CssClass="subtitulo">Cadastro de Acidentes</asp:Label>
                    </div>

                     <%-- multipage --%>

                    <div class="col-11 mb-4">
                        <eo:TabStrip ID="TabStrip" runat="server" ControlSkinID="None" MultiPageID="MultiPage1" >
                            <topgroup>
                                <Items>
                                    <eo:TabItem Text-Html="1 - Dados Básicos"></eo:TabItem>
                                    <eo:TabItem Text-Html="2 - Local do Acidente"></eo:TabItem>
                                    <eo:TabItem Text-Html="3 - Atendimento Médico"></eo:TabItem>
                                    <eo:TabItem Text-Html="4 - Absenteísmo"></eo:TabItem>
                                    <eo:TabItem Text-Html="5 - Dados Adicionais"></eo:TabItem>
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
            
                        <eo:MultiPage ID="MultiPage1" runat="server" Height="400" Width="923">
                            <eo:PageView ID="Pageview1" runat="server">
                                <!-- dados básicos -->
                                <div class="col-12">
                                    <div class="row">
                                        <div class="col-4 gy-2 gx-2 ms-3">
                                            <asp:Label id="Label24" runat="server" CssClass="tituloLabel">Tipo da CAT</asp:Label>
                                            <asp:RadioButtonList id="tabrblTipoCAT" runat="server" CssClass="texto form-check-input bg-transparent border-0 ms-3" Width="380px" CellSpacing="0" CellPadding="0" RepeatDirection="Horizontal">
                                                <asp:ListItem Value="1" Selected="True">Inicial</asp:ListItem>
                                                <asp:ListItem Value="2">Reabertura</asp:ListItem>
                                                <asp:ListItem Value="3">Comunica&#231;&#227;o de &#211;bito</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                            
                                        <div class="col-3 gy-2 gx-2 ms-3">
                                            <asp:label id="lblTipoAcidente" runat="server" CssClass="tituloLabel form-label">Tipo de Acidente</asp:label>
                                            <asp:RadioButtonList ID="tabrblTipoAcidente" runat="server" RepeatColumns="3" tabIndex="1" CssClass="texto form-control-sm ms-3" Width="250" CellSpacing="0" CellPadding="0" RepeatDirection="Horizontal">
                                                <asp:ListItem Selected="True" Value="1">Típico</asp:ListItem>
                                                <asp:ListItem Value="2">Doença</asp:ListItem>
                                                <asp:ListItem Value="3">Trajeto</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                            
                                        <div class="col-3 ms-5">
                                            <asp:label id="Label191" runat="server" CssClass="tituloLabel form-label">Data do Acidente</asp:label>
                                            <div class="row gy-1 gx-2">
                                                <div class="col-3 text-center">
                                                    <asp:textbox id="tabtxtdda" runat="server" CssClass="texto form-control form-control-sm" MaxLength="2" onkeyup="NextTxt(this, 'uwtAcidentes__ctl0_tabtxtmma', 2)"></asp:textbox>
                                                </div>
                                                <div class="col-1 pt-2 text-center">
                                                    <asp:Label id="Label34" runat="server" CssClass="texto">/</asp:Label>
                                                </div>
                                                <div class="col-3 text-center">
                                                    <asp:textbox id="tabtxtmma" runat="server" CssClass="texto form-control form-control-sm" MaxLength="2" onkeyup="NextTxt(this, 'uwtAcidentes__ctl0_tabtxtaaa', 2)"></asp:textbox>
                                                </div>
                                                <div class="col-1 pt-2 text-center">
                                                    <asp:Label id="Label35" runat="server" CssClass="texto">/</asp:Label>
                                                </div>
                                                <div class="col-4 text-center">
                                                    <asp:textbox id="tabtxtaaa" runat="server" CssClass="texto form-control form-control-sm" MaxLength="4" onkeyup="NextTxt(this, 'uwtAcidentes__ctl0_tabddlHora', 4)"></asp:textbox>
                                                </div>
                                            </div>
                                        </div>  
                            
                                        <div class="col-3 gy-2 ps-3">
                                            <div class="col mb-2 mt-2">
                                                <asp:label id="Label201" runat="server" CssClass="tituloLabel form-label">Hora do Acidente</asp:label>
                                                <div class="row gx-1 gy-2">
                                                    <div class="col-4 text-center">
                                                        <asp:dropdownlist id="tabddlHora" runat="server" CssClass="texto form-select form-select-sm"></asp:dropdownlist>
                                                    </div>
                                                    <div class="col-1 pt-2 text-center">
                                                        <span class="texto mx-1">:</span>
                                                    </div>
                                                    <div class="col-4 text-center">
                                                        <asp:dropdownlist id="tabddlMinuto" runat="server" CssClass="texto form-select form-select-sm"></asp:dropdownlist>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                            
                                        <div class="col-4 gy-2">
                                            <div class="col mb-2 mt-2">
                                                <asp:label id="Label23" runat="server" CssClass="tituloLabel form-label">Horas Trabalhadas Antes do Acidente</asp:label>
                                                <div class="row gx-1 gy-2">
                                                    <div class="col-3 text-center">
                                                        <asp:dropdownlist id="ddlHoraAntes" runat="server" CssClass="texto form-select form-select-sm"></asp:dropdownlist>
                                                    </div>
                                                    <div class="col-1 pt-2 text-center">
                                                            <span class="texto mx-1">:</span>
                                                        </div>
                                                    <div class="col-3 text-center">
                                                        <asp:dropdownlist id="ddlMinutoAntes" runat="server" CssClass="texto form-select form-select-sm"></asp:dropdownlist>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                            
                                        <div class="col-5 gx-5 mt-3"> 
                                            <div class="row"> 
                                                <div class="col-12 gy-2 ms-2">
                                                    <asp:CheckBox ID="chk_UltDia" runat="server" CssClass="texto form-check-input bg-transparent border-0 m-1" Text="Último dia trabalhado diferente da data do acidente" AutoPostBack="True"  OnCheckedChanged="chk_UltDia_CheckedChanged" />
                                                </div>
                                                <div class="col-9">
                                                    <div class="row">
                                                        <div class="col-2 gx-1 gy-1">
                                                            <asp:TextBox ID="txtUltDia" runat="server" CssClass="texto form-control form-control-sm" Enabled="False" MaxLength="2" onkeyup="NextTxt(this, 'uwtAcidentes__ctl0_tabtxtmma', 2)"></asp:TextBox>
                                                        </div>
                                                        <div class="col-1 gx-0 pt-2 text-center">
                                                            <span class="texto mx-1">/</span>
                                                        </div>
                                                        <div class="col-2 gx-1 gy-1 text-center">
                                                            <asp:TextBox ID="txtUltMes" runat="server"  CssClass="texto form-control form-control-sm" Enabled="False" MaxLength="2" onkeyup="NextTxt(this, 'uwtAcidentes__ctl0_tabtxtaaa', 2)"></asp:TextBox>
                                                        </div>
                                                        <div class="col-1 gx-0 pt-2 text-center">
                                                            <span class="texto mx-1">/</span>
                                                        </div>
                                                        <div class="col-3 gx-2 gy-1 text-center">
                                                            <asp:TextBox ID="txtUltAno" runat="server"  CssClass="texto form-control form-control-sm" Enabled="False" MaxLength="4" onkeyup="NextTxt(this, 'uwtAcidentes__ctl0_tabddlHora', 4)"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-2 gy-2 ms-2">
                                            <asp:Label ID="Label26" runat="server"  class="tituloLabel form-label col-form-label col-form-label-sm">Houve Afastamento?</asp:Label>
                                                 <asp:RadioButtonList ID="tabrblAfastamento" runat="server" AutoPostBack="True" CellPadding="0" CellSpacing="0" CssClass="texto form-check-input bg-transparent border-0 ms-3" RepeatDirection="Horizontal" Width="125px">
                                                    <asp:ListItem Value="1">Sim</asp:ListItem>
                                                    <asp:ListItem Selected="True" Value="0">Não</asp:ListItem>
                                                </asp:RadioButtonList>
                                        </div>
                                        <div class="col-2 gy-2 ms-2">
                                            <asp:Label id="Label71" runat="server" CssClass="tituloLabel">Houve Óbito?</asp:Label>
                                                <asp:RadioButtonList id="tabrblMorte" runat="server" CssClass="texto form-check-input bg-transparent border-0 ms-3" Width="100px" CellSpacing="0" CellPadding="0" RepeatDirection="Horizontal" AutoPostBack="True">
                                                    <asp:ListItem Value="1">Sim</asp:ListItem>
                                                    <asp:ListItem Value="0" Selected="True">N&#227;o</asp:ListItem>
                                                </asp:RadioButtonList>
                                        </div>
                            
                                        <div class="col-4 gy-2 ms-2">
                                            <asp:Label id="tablblObito" runat="server" CssClass="tituloLabel">Data do Óbito</asp:Label>
                                            <div class="row gx-1 gy-2">
                                                <div class="col-2 text-center">
                                                    <asp:TextBox id="tabtxtddo" runat="server" CssClass="texto form-control form-control-sm" BorderColor="LightGray"  MaxLength="2" BackColor="#EBEBEB" Enabled="False" onkeyup="NextTxt(this, 'uwtAcidentes__ctl4_tabuwtCAT__ctl0_tabtxtmmo', 2)"></asp:TextBox>
                                                </div>
                                                <div class="col-1 pt-2 text-center">
                                                    <asp:Label id="tablblBarra1" runat="server" CssClass="texto">/</asp:Label>
                                                </div>
                                                <div class="col-2 text-center">
                                                    <asp:TextBox id="tabtxtmmo" runat="server" CssClass="texto form-control form-control-sm" BorderColor="LightGray" MaxLength="2" BackColor="#EBEBEB" Enabled="False" onkeyup="NextTxt(this, 'uwtAcidentes__ctl4_tabuwtCAT__ctl0_tabtxtaao', 2)"></asp:TextBox>
                                                </div>
                                                <div class="col-1 pt-2 text-center">
                                                    <asp:Label id="tablblBarra2" runat="server" CssClass="texto">/</asp:Label>
                                                </div>
                                                <div class="col-3 text-center">
                                                    <asp:TextBox id="tabtxtaao" runat="server" CssClass="texto form-control form-control-sm" BorderColor="LightGray" MaxLength="4" BackColor="#EBEBEB" Enabled="False"></asp:TextBox>                             
                                                </div>
                                            </div>
                                        </div>
                            
                                        <div class="col-2 gy-2 ms-2 mt-2">
                                            <asp:Label id="Label3" runat="server" CssClass="tituloLabel mb-1">Registro Policial?</asp:Label>
                                            <asp:RadioButtonList id="tabrblRegPol" runat="server" CssClass="texto form-check-input bg-transparent border-0 ms-3" Width="100px" CellSpacing="0" CellPadding="0" RepeatDirection="Horizontal" AutoPostBack="True">
                                                <asp:ListItem Value="1">Sim</asp:ListItem>
                                                <asp:ListItem Value="0" Selected="True">N&#227;o</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                            
                                        <div class="col-4 gy-2 ms-2">
                                            <asp:Label id="tablblBO" runat="server" CssClass="tituloLabel" ForeColor="LightGray">B.O. Policial</asp:Label>
                                            <asp:TextBox id="tabtxtBO" runat="server" CssClass="texto form-control form-control-sm" Enabled="False"></asp:TextBox>
                                        </div>
                            
                                        <div class="col-11 gy-2 ms-2">
                                            <fieldset>
                                                <asp:Label ID="Label211" runat="server"  class="tituloLabel form-label col-form-label col-form-label-sm">Tipo de Acidente ( e-Social )</asp:Label>
                                                <asp:DropDownList ID="ddlTipoAcidente" runat="server" AutoPostBack="True" CssClass="texto form-select form-select-sm"></asp:DropDownList>
                                            </fieldset>
                                        </div>
                            
                                        <div class="col-11 gy-2 ms-2">
                                            <fieldset>
                                                <asp:Label ID="Labe12329" runat="server" class="tituloLabel form-label col-form-label col-form-label-sm">Situação geradora</asp:Label>
                                                <asp:DropDownList ID="ddlSituacaoGeradora" runat="server" AutoPostBack="True" CssClass="texto form-select form-select-sm"></asp:DropDownList>
                                            </fieldset>
                                        </div>

                                        <div class="col-11 gy-2 ms-2">
                                            <asp:Label ID="Label12326" runat="server" class="tituloLabel form-label col-form-label col-form-label-sm">Agente Causador</asp:Label>
                                            <asp:DropDownList ID="ddlAgenteCausador" runat="server" AutoPostBack="True" CssClass="texto form-select form-select-sm"></asp:DropDownList>
                                        </div>
                            
                                        <div class="col-11 gy-2 ms-2">
                                            <fieldset>
                                                <asp:Label ID="lblAgenteCausador" runat="server" class="tituloLabel form-label col-form-label col-form-label-sm" Visible="False">Agente Causador</asp:Label>
                                                <asp:TextBox ID="tabtxtAgenteCausador" runat="server" CssClass="texto form-control form-control-sm" Rows="2" TextMode="MultiLine" Visible="False" Width="135px"></asp:TextBox>
                                            </fieldset>
                                        </div>
                            
                                        <div class="col-11 gy-2 ms-2">
                                            <asp:Label ID="Label1121" runat="server" CssClass="tituloLabel">Observações</asp:Label>
                                            <asp:TextBox ID="txtObservacoes" runat="server" CssClass="texto form-control form-control-sm" Rows="3" TextMode="MultiLine" MaxLength="255"></asp:TextBox>
                                        </div>
                            
                                        <div class="col-6 gy-2 ms-2">
                                            <asp:Label ID="Label12328" runat="server" CssClass="tituloLabel">Iniciativa</asp:Label>
                                            <asp:RadioButtonList ID="rblIniciativa" runat="server" CellPadding="0" CellSpacing="0" CssClass="texto form-check-input bg-transparent border-0 ms-3" Width="380px" RepeatDirection="Horizontal">
                                                <asp:ListItem Selected="True" Value="1">Empregador</asp:ListItem>
                                                <asp:ListItem Value="2">Ordem Judicial</asp:ListItem>
                                                <asp:ListItem Value="3">Orgão Fiscalizador</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                            
                                        <div class="col-4 mt-2 gy-2">
                                            <asp:label id="lblNumero" runat="server" CssClass="tituloLabel">Número / Recibo CAT (opcional)</asp:label>
                                            <asp:textbox id="tabtxtNumeroCAT" runat="server" CssClass="texto form-control form-control-sm"></asp:textbox>
                                        </div>
                            
                                        <div class="col-8 gy-2 ms-2">
                                            <asp:Label id="Label221" runat="server" CssClass="tituloLabel">Emitente</asp:Label>
                                            <asp:RadioButtonList id="tabrblEmitente" runat="server" CssClass="texto form-check-input bg-transparent border-0 ms-3" Height="160" Width="300px" CellSpacing="0" CellPadding="0">
                                                <asp:ListItem Value="1" Selected="True">Empregador</asp:ListItem>
                                                <asp:ListItem Value="2">Sindicato</asp:ListItem>
                                                <asp:ListItem Value="3">Médico</asp:ListItem>
                                                <asp:ListItem Value="4">Segurado ou Dependente</asp:ListItem>
                                                <asp:ListItem Value="5">Autoridade P&#250;blica</asp:ListItem>
                                                <asp:ListItem Value="6">Cooperativa</asp:ListItem>
                                                <asp:ListItem Value="7">Órgão Gestor Mão de Obra</asp:ListItem>
                                                <asp:ListItem Value="8">Sindicato trabalhadores avulsos não portuários</asp:ListItem>
                                                <asp:ListItem Value="9">Empregado</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>

                                        <div class="col-3 gx-3 gy-3">
                                             <asp:Label ID="Label222" runat="server" class="tituloLabel form-label col-form-label col-form-label-sm">Acidente com CAT?</asp:Label>
                                             <asp:RadioButtonList ID="tabrblCAT" runat="server" AutoPostBack="True" CellPadding="0" CellSpacing="0" CssClass="texto form-check-input bg-transparent border-0 ms-3" RepeatDirection="Horizontal" Width="125px">
                                                 <asp:ListItem Value="1">Sim</asp:ListItem>
                                                 <asp:ListItem Selected="True" Value="0">Não</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                    </div>
                                </div>
                            </eo:PageView>
                
                            <eo:PageView ID="Pageview2" runat="server" Width="923px">
                                <%-- local do acidente --%>
                                <div class="col-12">
                                    <div class="row">
                                        <div class="col-6 gx-3 gy-2">
                                            <asp:Label ID="lblEndereco" runat="server" CssClass="tituloLabel" Enabled="False">Endereço do Acidente</asp:Label>
                                            <asp:TextBox ID="txtEndereco" runat="server" CssClass="texto form-control form-control-sm" Rows="2"  Visible="true" MaxLength="100" Enabled="False"></asp:TextBox>
                                        </div>
                            
                                        <div class="col-3 gx-5 gy-2">
                                            <asp:Label ID="lblCEP" runat="server" CssClass="tituloLabel" Enabled="False">CEP (sem traço)</asp:Label>
                                            <asp:TextBox ID="txtCEP" runat="server" CssClass="texto form-control form-control-sm" Rows="2" Visible="true" MaxLength="8" Enabled="False"></asp:TextBox>
                                        </div>
                            
                                        <div class="col-2 gx-3 gy-2">
                                            <asp:Label ID="lblNumero2" runat="server" CssClass="tituloLabel" Enabled="False">Nº</asp:Label>
                                            <asp:TextBox ID="txtNumero" runat="server" CssClass="texto form-control form-control-sm" Rows="2"  Visible="true" MaxLength="20" Enabled="False"></asp:TextBox>
                                        </div>
                            
                                        <div class="col-5 gx-3 gy-2">
                                            <asp:Label ID="lblBairro" runat="server" CssClass="tituloLabel" Enabled="False">Bairro</asp:Label>
                                            <asp:TextBox ID="txtBairro" runat="server" CssClass="texto form-control form-control-sm" Rows="2"  Visible="true" MaxLength="90" Enabled="False"></asp:TextBox>
                                        </div>
                            
                                        <div class="col-5 gx-5 gy-2 mb-1">
                                            <asp:Label ID="lblComplemento" runat="server" CssClass="tituloLabel" Enabled="False">Complemento</asp:Label>
                                            <asp:TextBox ID="txtComplemento" runat="server" CssClass="texto form-control form-control-sm" Rows="2" Visible="true" MaxLength="30" Enabled="False"></asp:TextBox>
                                        </div>
                            
                                        <div class="col-7 gx-3 gy-2">
                                            <asp:Label ID="lblMunicipio" runat="server" CssClass="tituloLabel" Enabled="False">Município</asp:Label>
                                            <asp:DropDownList ID="ddlMunicipio" runat="server" AutoPostBack="True" CssClass="texto form-select form-select-sm" Enabled="False"></asp:DropDownList>
                                        </div>
                            
                                        <div class="col-2 gx-3 gy-2">
                                            <asp:Label ID="lblUF" runat="server" CssClass="tituloLabel" Enabled="False">Estado (UF)</asp:Label>
                                            <asp:DropDownList ID="ddlUF" runat="server" AutoPostBack="True" CssClass="texto form-select form-select-sm" Enabled="False" OnSelectedIndexChanged="ddlUF_SelectedIndexChanged"></asp:DropDownList>
                                        </div>
                            
                                        <div class="col-2 gx-3 gy-2 mb-1">
                                            <asp:Label ID="lblCodPostal" runat="server" CssClass="tituloLabel" Enabled="False">Cód.Postal</asp:Label>
                                            <asp:TextBox ID="txtCodPostal" runat="server" CssClass="texto form-control form-control-sm" Rows="2"  Visible="true" MaxLength="12" Enabled="False"></asp:TextBox>
                                        </div>
                            
                                        <div class="col-8 gx-3 gy-2">
                                            <asp:Label ID="lblCodPostal0" runat="server" CssClass="tituloLabel" Enabled="False">CNPJ (apenas números) se Empresa onde o empregador presta serviço</asp:Label>
                                            <asp:TextBox ID="txtCNPJTerceiros" runat="server" CssClass="texto form-control form-control-sm" Rows="2" Visible="true" MaxLength="14" Enabled="False"></asp:TextBox>
                                        </div>
                            
                                        <div class="col-5 gx-3 gy-2">
                                            <asp:Label ID="Label2" runat="server" CssClass="tituloLabel">Tipo de Local</asp:Label>
                                            <asp:RadioButtonList ID="rd_TipoLocal" runat="server" CellPadding="0" CellSpacing="0" CssClass="texto form-check-input bg-transparent border-0 ms-3" Width="300px" Height="160px" AutoPostBack="True" OnSelectedIndexChanged="rd_TipoLocal_SelectedIndexChanged">
                                                <asp:ListItem Selected="True" Value="1">Estabelecimento do empregador no Brasil</asp:ListItem>
                                                <asp:ListItem Value="2">Estabelecimento do empregador no Exterior</asp:ListItem>
                                                <asp:ListItem Value="3">Empresa onde o empregador presta serviço</asp:ListItem>
                                                <asp:ListItem Value="4">Via Pública</asp:ListItem>
                                                <asp:ListItem Value="5">Área Rural</asp:ListItem>
                                                <asp:ListItem Value="6">Embarcação</asp:ListItem>
                                                <asp:ListItem Value="9">Outros</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                            
                                        <div class="col-6 gx-3 gy-2">
                                            <div class="row">
                                                <div class="col-12 gy-2">
                                                    <asp:label id="Label281" runat="server" CssClass="tituloLabel">Especificação do Local</asp:label>
                                                    <asp:textbox id="tabtxtEspecLocal" runat="server" CssClass="texto form-control form-control-sm"></asp:textbox>
                                                </div>

                                                <div class="col-12 gy-2">
                                                    <asp:Label ID="lblDescLocal" runat="server" CssClass="tituloLabel">Descrição do Local</asp:Label>
                                                    <asp:TextBox ID="txtDescLocal" runat="server" CssClass="texto form-control form-control-sm" Rows="2"  Visible="true" MaxLength="255" Enabled="True" TextMode="MultiLine"></asp:TextBox>
                                                </div>

                                                <div class="col-12 gy-2">
                                                    <asp:label id="lblLocalAcidente" runat="server" CssClass="tituloLabel">Unidade de Trabalho</asp:label>
                                                    <asp:dropdownlist id="tabddlLocalAcidente" runat="server" CssClass="texto form-select form-select-sm"></asp:dropdownlist>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-6 gx-3 gy-3">
                                            <asp:label id="lblSetor" runat="server" CssClass="tituloLabel">Setor do Acidente</asp:label>
                                            <asp:radiobuttonlist id="tabrblSetor" runat="server" CssClass="texto form-check-input bg-transparent border-0 ms-3" Width="450px" CellSpacing="0" CellPadding="0" RepeatDirection="Horizontal" AutoPostBack="True">
                                                <asp:ListItem Value="1" Selected="True">No Setor de Trabalho normal</asp:ListItem>
                                                <asp:ListItem Value="2">Em Outro Setor</asp:ListItem>
                                                <asp:ListItem Value="3">Não Aplicável</asp:ListItem>
                                            </asp:radiobuttonlist>
                                        </div>

                                        <div class="col-5 gx-3 gy-3">
                                            <asp:label id="tablblSetor" runat="server" CssClass="tituloLabel">Selecione o Setor</asp:label>
                                            <asp:dropdownlist id="tabddlSetor" runat="server" CssClass="texto form-select form-select-sm" Enabled="False"></asp:dropdownlist>
                                        </div>
                                    </div>
                                </div>
                    
                                <COLUMNS></COLUMNS>
                                <DROPDOWNLAYOUT JavaScriptFileName="scripts/ig_WebGrid.js" GridLines="None" ColHeadersVisible="No"
                                    DropdownHeight="128px" AutoGenerateColumns="False" HeaderClickAction="Select" RowHeightDefault="14px" RowSelectors="No" BorderCollapse="Separate" DropdownWidth="380px">
                                </DROPDOWNLAYOUT>
                                <rowstyle backcolor="White" bordercolor="Gray" borderstyle="Solid" borderwidth="1px" />
                                <borderdetails widthleft="0px" widthtop="0px"></borderdetails>
                                <selectedrowstyle backcolor="MidnightBlue" forecolor="White" />
                                <headerstyle backcolor="LightGray" borderstyle="Solid" />
                                <borderdetails colorleft="White" colortop="White" widthleft="1px" widthtop="1px"></borderdetails>
                                <framestyle backcolor="Silver" bordercolor="Black" borderstyle="Solid" borderwidth="1px"
                                    cursor="Default" font-names="Verdana" font-size="XX-Small" forecolor="#004000"
                                    height="100%" width="100%"></framestyle>
                                <imageurls imagedirectory="img/"></imageurls>
                                <EXPANDEFFECTS ShadowColor="LightGray"></EXPANDEFFECTS>
                                <asp:button id="tabbtnAddLocal" runat="server" CssClass="btn" Text="..." Visible="False"></asp:button>
                            </eo:PageView>
                
                            <eo:PageView ID="Pageview3" runat="server" Width="923px">
                                <%-- atendimento médico --%>
                                <div class="col-12">
                                    <div class="row">
                                        <div class="col-3 gy-2">
                                            <asp:Label ID="Label1" runat="server" CssClass="tituloLabel">Data de Atendimento</asp:Label>
                                            <div class="row gx-1 gy-2">
                                                <div class="col-3">
                                                    <asp:TextBox ID="txtddAt" runat="server" CssClass="texto form-control form-control-sm" MaxLength="2" onkeyup="NextTxt(this, 'uwtAcidentes__ctl4_tabuwtCAT__ctl0_tabtxtMesCAT', 2)"></asp:TextBox>
                                                </div>
                                                <div class="col-1 pt-2 text-center">
                                                    <asp:Label runat="server" CssClass="texto">/</asp:Label>
                                                </div>
                                                <div class="col-3">
                                                    <asp:TextBox ID="txtmmAt" runat="server" CssClass="texto form-control form-control-sm" MaxLength="2" onkeyup="NextTxt(this, 'uwtAcidentes__ctl4_tabuwtCAT__ctl0_tabtxtAnoCAT', 2)"></asp:TextBox>
                                                </div>
                                                <div class="col-1 pt-2 text-center">
                                                    <asp:Label runat="server" CssClass="texto">/</asp:Label>
                                                </div>
                                                <div class="col-4">
                                                    <asp:TextBox ID="txtaaAt" runat="server" CssClass="texto form-control form-control-sm" MaxLength="4" onkeyup="NextTxt(this, 'uwtAcidentes__ctl4_tabuwtCAT__ctl0_tabddlHoraCAT', 4)"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                            
                                        <div class="col-3 gx-0 gy-2 ms-4">
                                            <asp:label id="Label13" runat="server" CssClass="tituloLabel">Hora de Atendimento</asp:label>
                                            <div class="row">
                                                <div class="col-4">
                                                    <asp:dropdownlist id="ddlHoraAt" runat="server" CssClass="texto form-select form-select-sm"></asp:dropdownlist>
                                                </div>
                                                <div class="col-1 gx-0 mt-2 text-center">
                                                    <asp:Label runat="server" CssClass="texto">:</asp:Label>
                                                </div>
                                                <div class="col-4">
                                                    <asp:dropdownlist id="ddlMinutoAt" runat="server" CssClass="texto form-select form-select-sm"></asp:dropdownlist>
                                                </div>
                                            </div>
                                        </div>
                            
                                        <div class="col-2 gx-0 gy-2">
                                            <asp:Label ID="Label17" runat="server" CssClass="tituloLabel">Houve internação?</asp:Label>
                                            <asp:RadioButtonList ID="rd_Internacao" runat="server" AutoPostBack="True" CellPadding="0" CellSpacing="0" CssClass="texto form-check-input bg-transparent border-0 ms-3" RepeatDirection="Horizontal" Width="100px">
                                                <asp:ListItem Value="1">Sim</asp:ListItem>
                                                <asp:ListItem Selected="True" Value="0">Não</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                            
                                        <div class="col-3 gx-3 gy-2">
                                            <asp:Label ID="Label14" runat="server" CssClass="tituloLabel">Duração do Tratamento em dias</asp:Label>
                                            <div class="row">
                                                <div class="col-5">
                                                    <asp:TextBox ID="txtDuracao" runat="server" CssClass="texto form-control form-control-sm" MaxLength="3"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                            
                                        <!-- segunda linha -->
                                        <div class="col-6 gx-3 gy-3">
                                            <fieldset>
                                                <asp:Label ID="Label12325" runat="server" class="tituloLabel form-label col-form-label col-form-label-sm">Parte do Corpo Atingida</asp:Label>
                                                <asp:DropDownList ID="ddlParteCorpo" runat="server" AutoPostBack="True" CssClass="texto form-select form-select-sm"></asp:DropDownList>
                                            </fieldset>
                                        </div>
                            
                                        <div class="col-6 gx-5  gy-3">
                                            <div class="row">
                                                <div class="col-12">
                                                    <asp:Label ID="Label12330" runat="server" class="tituloLabel form-label col-form-label col-form-label-sm"  OnSelectedIndexChanged="rd_TipoLocal_SelectedIndexChanged">Lateralidade - Parte do Corpo Atingida</asp:Label>
                                                </div>
                                                <div class="col-4 gx-3 ms-4">
                                                    <asp:RadioButtonList ID="rd_Lateralidade" runat="server" AutoPostBack="True" CellPadding="0" CellSpacing="0" CssClass="texto form-control-sm" RepeatDirection="Horizontal" Width="350" OnSelectedIndexChanged="rd_TipoLocal_SelectedIndexChanged">
                                                        <asp:ListItem Selected="True" Value="0">Não Aplicável</asp:ListItem>
                                                        <asp:ListItem Value="1">Esquerda</asp:ListItem>
                                                        <asp:ListItem Value="2">Direita</asp:ListItem>
                                                        <asp:ListItem Value="3">Ambas</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-6 gx-3 gy-3">
                                            <asp:label id="lblMembroAtingido" runat="server" class="tituloLabel form-label col-form-label col-form-label-sm" Visible="False">Membro Atingido</asp:label>
                                            <asp:textbox id="tabtxtMembroAtingido" runat="server" CssClass="texto form-control form-control-sm" Width="135px" Rows="2" TextMode="MultiLine" Visible="False"></asp:textbox>
                                        </div>

                                        <div class="col-6 gx-3 gy-3">
                                            <asp:label ID="lblNaturezaLesao" runat="server" class="tituloLabel form-label col-form-label col-form-label-sm" Visible="False">Natureza da Lesão</asp:label>
                                            <asp:textbox ID="tabtxtNaturezaLesao" runat="server" CssClass="texto form-control form-control-sm" Width="135px" Rows="2" TextMode="MultiLine" Visible="False"></asp:textbox>
                                        </div>
                            
                                        <!-- terceira linha -->
                            
                                        <div class="col-11 gx-3 gy-3">
                                            <asp:Label ID="Label12327" runat="server" class="tituloLabel form-label col-form-label col-form-label-sm">Descrição da Lesão</asp:Label>
                                            <asp:DropDownList ID="ddlDescricaoLesao" runat="server" AutoPostBack="True" CssClass="texto form-select form-select-sm"></asp:DropDownList>
                                        </div>
                            
                                        <!-- quarta linha -->
                                        <div class="col-11 gx-3 gy-3">
                                            <asp:Label ID="Label25" runat="server" class="tituloLabel form-label col-form-label col-form-label-sm">Descrição Complementar da Lesão</asp:Label>
                                            <asp:TextBox ID="tabtxtDescricao" runat="server" CssClass="texto form-control form-control-sm" Rows="2" TextMode="MultiLine" MaxLength="255"></asp:TextBox>
                                        </div>
                            
                                        <!-- quinta linha -->
                                        <div class="col-11 gx-3 gy-3">
                                            <asp:Label ID="Label9" runat="server" CssClass="tituloLabel">Diagnóstico Provável</asp:Label>
                                            <asp:TextBox ID="txtDiagnostico" runat="server" CssClass="texto form-control form-control-sm" Rows="2" TextMode="MultiLine" MaxLength="100"></asp:TextBox>
                                        </div>
                            
                                        <!-- sexta linha -->
                                        <div class="col-11 gx-3 gy-3">
                                            <asp:label id="Label282" runat="server" CssClass="texto">Digite o código desejado ou uma palavra chave para Procurar a CID</asp:label>
                                        </div>
                            
                                        <div class="col-6 gx-3 gy-3">
                                            <asp:Label ID="Label12324" runat="server" CssClass="tituloLabel">Principal</asp:Label>
                                            <div class="row">
                                                <div class="col-8">
                                                    <asp:TextBox ID="tabtxtCID" runat="server" CssClass="texto form-control form-control-sm"></asp:TextBox>
                                                    <asp:Label ID="lbl_Id2" runat="server" Text="0" Visible="False"></asp:Label>
                                                </div>
                                                <div class="col-3">
                                                    <asp:button id="tabbtnProcurar" runat="server" CssClass="btnProcurar" Text="Procurar" onclick="tabbtnProcurar_Click"></asp:button>
                                                </div>
                                            </div>
                                        </div>
                            
                                        <div class="col-6 gx-3 gy-3">
                                            <asp:Label ID="Label10" runat="server" CssClass="tituloLabel">2. CID</asp:Label>
                                            <div class="row">
                                                <div class="col-8">
                                                    <asp:TextBox ID="txtCID2" runat="server" CssClass="texto form-control form-control-sm"></asp:TextBox>
                                                    <asp:Label ID="lbl_Id1" runat="server" Text="0" Visible="False"></asp:Label>
                                                </div>
                                                <div class="col-3">
                                                    <asp:Button ID="btnProcurar2" runat="server" CssClass="btnProcurar" Text="Procurar" onclick="btnProcurar2_Click" />
                                                </div>
                                            </div>
                                        </div>
                            
                                        <div class="col-6 gx-3 gy-3">
                                            <asp:Label ID="Label11" runat="server" CssClass="tituloLabel">3. CID</asp:Label>
                                            <div class="row">
                                                <div class="col-8">
                                                    <asp:TextBox ID="txtCID3" runat="server" CssClass="texto form-control form-control-sm"></asp:TextBox>
                                                    <asp:Label ID="lbl_Id3" runat="server" Text="0" Visible="False"></asp:Label>
                                                </div>
                                                <div class="col-3">
                                                    <asp:Button ID="btnProcurar3" runat="server" CssClass="btnProcurar" Text="Procurar" onclick="btnProcurar3_Click" />
                                                </div>
                                            </div>
                                        </div>
                            
                                        <div class="col-6 gx-3 gy-3">
                                            <asp:Label ID="Label12" runat="server" CssClass="tituloLabel">4. CID</asp:Label>
                                            <div class="row">
                                                <div class="col-8">
                                                    <asp:TextBox ID="txtCID4" runat="server" CssClass="texto form-control form-control-sm"></asp:TextBox>
                                                    <asp:Label ID="lbl_Id4" runat="server" Text="0" Visible="False"></asp:Label>
                                                    <asp:Label ID="lbl_Procura" runat="server" Text="0" Visible="False"></asp:Label>
                                                </div>
                                                <div class="col-3">
                                                    <asp:Button ID="btnProcurar4" runat="server" CssClass="btnProcurar" Text="Procurar" onclick="btnProcurar4_Click" />
                                                </div>
                                            </div>
                                        </div>
                            
                                        <div class="col-5 gx-3 gy-2">
                                            <asp:Label runat="server" style="color: transparent;">.</asp:Label>
                                            <asp:DropDownList ID="tabddlCID" runat="server" AutoPostBack="True" CssClass="texto form-select form-select-sm" onselectedindexchanged="tabddlCID_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Acidente sem CID cadastrada!</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                            
                                        <div class="col-7"><br /></div>
                            
                                        <!-- última linha -->
                                        <div class="col-6 gx-3 gy-3">
                                            <asp:Label ID="Label16" runat="server" CssClass="tituloLabel">Médico ou dentista emitente</asp:Label>
                                            <asp:TextBox ID="txtMedico" runat="server" CssClass="texto form-control form-control-sm"></asp:TextBox>
                                        </div>
                            
                                        <div class="col-2 gx-3 gy-3">
                                            <asp:Label ID="Label18" runat="server" CssClass="tituloLabel">CRM Número</asp:Label>
                                            <asp:TextBox ID="txtCRM" runat="server" CssClass="texto form-control form-control-sm" MaxLength="14"></asp:TextBox>
                                        </div>
                            
                                        <div class="col-1 gx-3 gy-3">
                                            <asp:Label ID="Label19" runat="server" CssClass="tituloLabel">CRM UF</asp:Label>
                                            <asp:TextBox ID="txtCRMUF" runat="server" CssClass="texto form-control form-control-sm" MaxLength="2"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </eo:PageView>
                
                            <eo:PageView ID="Pageview4" runat="server" Width="923">
                                <!-- absenteísmo -->
                                <div class="col-12">
                                    <div class="row">
                                        <div class="col-4 gx-4 gy-2">
                                            <asp:Label id="Label6" runat="server" CssClass="tituloLabel">Selecione o Absenteísmo:  (opcional)</asp:Label>
                                            <asp:dropdownlist id="tabddlAbsentismo" runat="server" CssClass="texto form-select form-select-sm" AutoPostBack="True"></asp:dropdownlist>
                                            <asp:CheckBox ID="chk_INSS" runat="server" AutoPostBack="True" CssClass="boldFont" Text="Afastamento pelo INSS" Visible="False" />
                                        </div>
                            
                                        <div class="col-5"><br /></div>
                            
                                        <div class="row gy-2 gx-4">
                                            <div class="col-3 mb-2">
                                                <asp:Label ID="Label192" runat="server" CssClass="tituloLabel">Data de Início</asp:Label>
                                                <div class="row gx-1 gy-2">
                                                    <div class="col-3 text-center">
                                                        <asp:TextBox ID="tabtxtddi" runat="server" CssClass="texto form-control form-control-sm" MaxLength="2" onkeyup="NextTxt(this, 'uwtAcidentes__ctl5_tabtxtmmi', 2)"></asp:TextBox>
                                                    </div>
                                                    <div class="col-1 pt-2 text-center">
                                                        <asp:Label id="Label27" runat="server" CssClass="texto">/</asp:Label>
                                                    </div>
                                                    <div class="col-3 text-center">
                                                        <asp:TextBox ID="tabtxtmmi" runat="server" CssClass="texto form-control form-control-sm" MaxLength="2" onkeyup="NextTxt(this, 'uwtAcidentes__ctl5_tabtxtaai', 2)"></asp:TextBox>
                                                    </div>
                                                    <div class="col-1 pt-2 text-center">
                                                        <asp:Label id="Label28" runat="server" CssClass="texto">/</asp:Label>
                                                    </div>
                                                    <div class="col-4 text-center">
                                                        <asp:TextBox ID="tabtxtaai" runat="server" CssClass="texto form-control form-control-sm" MaxLength="4" onkeyup="NextTxt(this, 'uwtAcidentes__ctl5_tabddlHoraIni', 4)"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                
                                            <div class="col-2 mb-2">
                                                <asp:Label ID="Label7" runat="server" CssClass="tituloLabel">Hora de Início</asp:Label>
                                                <div class="row gx-1 gy-2">
                                                    <div class="col-5">
                                                        <asp:DropDownList ID="tabddlHoraIni" runat="server" CssClass="texto form-select form-select-sm"></asp:DropDownList>
                                                    </div>
                                                    <div class="col-1 pt-2 text-center">
                                                        <span class="texto">:</span> 
                                                    </div>
                                                    <div class="col-5">
                                                        <asp:DropDownList ID="tabddlMinutoIni" runat="server" CssClass="texto form-select form-select-sm"></asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                
                                            <div class="col-3 mb-2">
                                                <asp:Label ID="Label20" runat="server" CssClass="tituloLabel">Previsão de Retorno</asp:Label>
                                                <div class="row gx-1 gy-2">
                                                    <div class="col-3 text-center">
                                                        <asp:TextBox ID="tabtxtddp" runat="server" CssClass="texto form-control form-control-sm" MaxLength="2" onkeyup="NextTxt(this, 'uwtAcidentes__ctl5_tabtxtmmp', 2)"></asp:TextBox>                                    
                                                    </div>
                                                    <div class="col-1 pt-2 text-center">
                                                        <asp:Label id="Label29" runat="server" CssClass="texto">/</asp:Label>
                                                    </div>
                                                    <div class="col-3 text-center">
                                                        <asp:TextBox ID="tabtxtmmp" runat="server" CssClass="texto form-control form-control-sm" MaxLength="2" onkeyup="NextTxt(this, 'uwtAcidentes__ctl5_tabtxtaap', 2)"></asp:TextBox>                                    
                                                    </div>
                                                    <div class="col-1 pt-2 text-center">
                                                        <asp:Label id="Label30" runat="server" CssClass="texto">/</asp:Label>
                                                    </div>
                                                    <div class="col-4 text-center">
                                                        <asp:TextBox ID="tabtxtaap" runat="server" CssClass="texto form-control form-control-sm" MaxLength="4" onkeyup="NextTxt(this, 'uwtAcidentes__ctl5_tabtxtddr', 4)"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                
                                            <div class="col-3 mb-2">
                                                <asp:Label ID="Label21" runat="server" CssClass="tituloLabel">Data de Retorno</asp:Label>
                                                <div class="row gx-1 gy-2">
                                                    <div class="col-3 text-center">
                                                        <asp:TextBox ID="tabtxtddr" runat="server" CssClass="texto form-control form-control-sm" MaxLength="2" onkeyup="NextTxt(this, 'uwtAcidentes__ctl5_tabtxtmmr', 2)"></asp:TextBox>
                                                    </div>
                                                    <div class="col-1 pt-2 text-center">
                                                        <asp:Label id="Label32" runat="server" CssClass="texto">/</asp:Label>
                                                    </div>
                                                    <div class="col-3 text-center">
                                                        <asp:TextBox ID="tabtxtmmr" runat="server" CssClass="texto form-control form-control-sm" MaxLength="2" onkeyup="NextTxt(this, 'uwtAcidentes__ctl5_tabtxtaar', 2)"></asp:TextBox>
                                                    </div>
                                                    <div class="col-1 pt-2 text-center">
                                                        <asp:Label id="Label33" runat="server" CssClass="texto">/</asp:Label>
                                                    </div>
                                                    <div class="col-4 text-center">
                                                        <asp:TextBox ID="tabtxtaar" runat="server" CssClass="texto form-control form-control-sm" MaxLength="4" onkeyup="NextTxt(this, 'uwtAcidentes__ctl5_tabddlHoraRet', 4)"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                
                                            <div class="col-3 mb-2">
                                                <asp:Label ID="Label8" runat="server" CssClass="tituloLabel">Hora de Retorno</asp:Label>
                                                <div class="row gx-1 gy-2">
                                                    <div class="col-4">
                                                        <asp:DropDownList ID="tabddlHoraRet" runat="server" CssClass="texto form-select form-select-sm"></asp:DropDownList>
                                                    </div>
                                                    <div class="col-1 pt-2 text-center">
                                                        <span class="texto">:</span> 
                                                    </div>
                                                    <div class="col-4">
                                                        <asp:DropDownList ID="tabddlMinutoRet" runat="server" CssClass="texto form-select form-select-sm"></asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </eo:PageView>
                
                            <eo:PageView ID="Pageview5" runat="server" Width="923">
                            <!-- dados adicionais -->
                                <div class="col-12">
                                    <div class="row">
                                        <div class="col-6 gx-4 gy-2">
                                            <asp:Label id="Label31" runat="server" CssClass="tituloLabel">Empregado transferido para outro Setor devido ao acidente  (opcional)</asp:Label>
                                            <asp:RadioButtonList id="tabrblTransfSetor" runat="server" CssClass="texto form-check-input bg-transparent border-0 ms-3" Width="125px" CellSpacing="0" RepeatDirection="Horizontal">
                                                <asp:ListItem Value="0" Selected="True">N&#227;o</asp:ListItem>
                                                <asp:ListItem Value="1">Sim</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>

                                        <div class="col-6 gx-0 gy-2">
                                            <asp:Label id="Label51" runat="server" CssClass="tituloLabel">Empregado aposentado por invalidez causada pelo acidente</asp:Label>
                                            <asp:RadioButtonList id="tabrblAposInval" runat="server"  CssClass="texto form-check-input bg-transparent border-0 ms-3" Width="125px" CellSpacing="0" RepeatDirection="Horizontal">
                                                <asp:ListItem Value="0" Selected="True">N&#227;o</asp:ListItem>
                                                <asp:ListItem Value="1">Sim</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>

                                        <div class="col-6 gx-4 gy-4">
                                            <asp:Label ID="Label12323" runat="server" CssClass="tituloLabel">Perda Material Avaliada (R$)</asp:Label>
                                            <asp:textbox ID="wcePerdaMaterial" runat="server" CssClass="texto form-control form-control-sm" DataMode="Float" HorizontalAlign="Center" ImageDirectory="" NullText="0" Width="210px"></asp:textbox>
                                        </div>

                                        <div class="col-5"><br /></div>

                                        <div class="col-4 gx-4 gy-3">
                                            <div class="col mb-2">
                                                <asp:label id="lblData" runat="server" CssClass="tituloLabel mb-1">Data de Emissão</asp:label>
                                                    <div class="row gx-1 gy-2">
                                                        <div class="col-3 text-center">
                                                            <asp:textbox id="tabtxtDiaCAT" runat="server" CssClass="texto form-control form-control-sm me-2" MaxLength="2" onkeyup="NextTxt(this, 'uwtAcidentes__ctl4_tabuwtCAT__ctl0_tabtxtMesCAT', 2)"></asp:textbox>
                                                        </div>
                                                        <div class="col-1 pt-2 text-center">
                                                            <asp:Label id="Label60" runat="server" CssClass="texto">/</asp:Label>
                                                        </div>
                                                        <div class="col-3 text-center">
                                                            <asp:textbox id="tabtxtMesCAT" runat="server" CssClass="texto form-control form-control-sm me-2" MaxLength="2" onkeyup="NextTxt(this, 'uwtAcidentes__ctl4_tabuwtCAT__ctl0_tabtxtAnoCAT', 2)"></asp:textbox>
                                                        </div>
                                                        <div class="col-1 pt-2 text-center">
                                                            <asp:Label id="Label61" runat="server" CssClass="texto">/</asp:Label>
                                                        </div>
                                                        <div class="col-4 text-center">
                                                            <asp:textbox id="tabtxtAnoCAT" runat="server" CssClass="texto form-control form-control-sm" MaxLength="4" onkeyup="NextTxt(this, 'uwtAcidentes__ctl4_tabuwtCAT__ctl0_tabddlHoraCAT', 4)"></asp:textbox>
                                                        </div>
                                                    </div>
                                            </div>
                                        </div>

                                        <div class="col-4 gx-3 gy-2">
                                            <div class="col mb-2 mt-2">
                                                <asp:label id="Label4" runat="server" CssClass="tituloLabel mb-1">Hora de Emissão</asp:label>
                                                    <div class="row gx-1 gy-2">
                                                        <div class="col-3 text-center">
                                                            <asp:dropdownlist id="tabddlHoraCAT" runat="server" CssClass="texto form-select form-select-sm"></asp:dropdownlist>
                                                        </div>
                                                        <div class="col-1 pt-2 text-center">
                                                            <span class="texto mx-1">:</span> 
                                                        </div>
                                                        <div class="col-3 text-center">
                                                            <asp:dropdownlist id="tabddlMinutoCAT" runat="server" CssClass="texto form-select form-select-sm"></asp:dropdownlist>
                                                        </div>
                                                    </div>
                                            </div>
                                        </div>

                                        <div class="col-4"><br /></div>

                                        <div class="col-4 gx-3 gy-3">
                                            <asp:Label ID="lblAnotacoes0" runat="server" CssClass="tituloLabel">Arquivo CAT   (opcional)</asp:Label>
                                            <asp:TextBox ID="txt_Arq" runat="server" CssClass="texto form-control form-control-sm" Height="27px" ReadOnly="True" Rows="4" TextMode="MultiLine"></asp:TextBox>
                                        </div>

                                        <div class="col-2 gx-3 mt-4 pt-1">
                                            <asp:Button ID="btnProjeto" runat="server" Text="Visualizar Arquivo" CssClass="btn" OnClick="btnProjeto_Click" />
                                        </div>

                                        <div class="col-5 gx-3 gy-3">
                                            <asp:Label ID="Label22" runat="server" CssClass="tituloLabel">Inserir / Modificar Arquivo CAT</asp:Label>
                                            <asp:FileUpload ID="File1" runat="server" ClientIDMode="Static" CssClass="texto form-control" />
                                        </div>
                                    </div>
                                </div>
                            </eo:PageView>
                
                            <%--    <eo:PageView ID="Pageview6" runat="server" Width="457px">--%>
                        </eo:MultiPage>
                    </div>

						            <BR>
                            <asp:Label ID="Label15" runat="server" CssClass="tituloLabel" visible="false">Código CNES</asp:Label>
                            <asp:TextBox ID="txtCNES" runat="server" CssClass="texto form-control form-control-sm" MaxLength="7" visible="false"></asp:TextBox>

						            <div class="text-center mt-4 mb-3">
					                    <asp:button ID="btnOK" runat="server" tabIndex="1" Text="Gravar" CssClass="btn" onclick="btnOK_Click" />
                                        <asp:button id="btnExcluir" runat="server" CssClass="btn2" Text="Excluir" EnableViewState="False" onclick="btnExcluir_Click"></asp:button>
                                        <asp:button id="btnCancelar" runat="server" CssClass="btn2" Text="Cancelar"></asp:button>
                                    </div>

                    
                    
                                    <%--	</TD>
				            </TR>--%>
            
             <eo:MsgBox ID="MsgBox1" runat="server" BackColor="#47729F" ControlSkinID="None" 
                    HeaderHtml="Dialog Title" Height="100px" Width="168px">
                    <HeaderStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #ffbf00 3px solid; padding-left: 4px; font-weight: bold; font-size: 11px; padding-bottom: 2px; color: white; padding-top: 2px; border-bottom: #22456a 1px solid; font-family: verdana" />
                    <ContentStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #7d97b6 1px solid; padding-left: 4px; border-left-width: 1px; font-size: 11px; border-left-color: #728eb8; padding-bottom: 4px; color: white; padding-top: 4px; border-bottom: #22456a 1px solid; font-family: verdana" />
                    <FooterStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #7d97b6 1px solid; padding-left: 4px; border-left-width: 1px; font-size: 11px; border-left-color: #728eb8; padding-bottom: 4px; color: white; padding-top: 4px; border-bottom: #22456a 1px solid; font-family: verdana" />
                </eo:MsgBox>
                                    <%--	</TABLE>--%>
                            </div>
                         </div>
		</form>
	</body>
</HTML>
