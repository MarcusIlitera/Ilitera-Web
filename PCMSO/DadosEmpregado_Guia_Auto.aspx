<%@ Page Language="C#"   AutoEventWireup="True"  MasterPageFile="~/Site.Master" CodeBehind="DadosEmpregado_Guia_Auto.aspx.cs"  Inherits="Ilitera.Net.DadosEmpregado_Guia_Auto" Title="Ilitera.Net" EnableEventValidation="false"  ValidateRequest="false"  %>

<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>


<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
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
            height: 37px;
        }
        .style2
        {
            font-size: x-small;
        }
        .style3
        {
            width: 372px;
        }
    .auto-style1 {
        width: 372px;
        height: 21px;
    }
    </style>
</asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent" >

     <LINK href="scripts/style.css" type="text/css" rel="stylesheet">
	<script language="javascript">

	    function Reload() {
	        var f = document.getElementById('SubDados');
	        //f.src = f.src;
	        f.contentWindow.location.reload(true);
	    }


    </script>

  

	<table class="normalFont" id="Table1" cellspacing="0" cellpadding="0" width="600" align="center"
				border="0">
				<tr>
					<td nowrap align="center">
						<table class="fotoborder" bgcolor="#edffeb" id="Table2" cellspacing="0" 
                            cellpadding="0" align="center"
							border="0">
							<tr> <!-- celula esquerda-->
								<td class="normalFont" width="490">
									<table id="Table3" cellSpacing="0" cellPadding="0" width="490" border="0">
										<tr>
											<td vAlign="middle" align="left" width="7" bgcolor="#edffeb">&nbsp;</TD>
											<td class="tableEdit" valign="middle" align="right" width="7" bgcolor="#edffeb">&nbsp;</TD>
											<td class="textDadosOpcao" valign="middle" align="left" width="31" bgcolor="#edffeb">
                                                <asp:label id="lblNome" runat="server" BackColor="#EDFFEB" Font-Bold="True">Nome&nbsp;</asp:label></TD>
											<td valign="middle" align="left" width="445" bgcolor="#edffeb">
												<table id="Table4" cellSpacing="0" cellPadding="0" width="444" border="0">
													<tr>
														<td class="textDadosNome">
                                                            <asp:label id="lblValorNome" runat="server" 
                                                                Font-Bold="True" BackColor="#EDFFEB"></asp:label></TD>
													</tr>
												</TABLE>
											</TD>
										</TR>
									</TABLE>
									<TABLE id="Table5" cellSpacing="0" cellPadding="0" width="490" border="0">
										<TR>
											<TD class="tableSpace"></TD>
										</TR>
									</TABLE>
									<TABLE class="tableEdit" id="Table6" cellSpacing="0" cellPadding="0" width="490" border="0">
										<TR class="tableEdit" vAlign="middle">
											<TD class="tableEdit" vAlign="middle" align="left" width="7" bgColor="#edffeb">&nbsp;</TD>
											<TD class="tableEdit" vAlign="middle" align="right" width="7" bgColor="#edffeb">&nbsp;</TD>
											<TD class="textDadosOpcao" align="left" width="105" bgColor="#edffeb">
                                                <asp:label id="lblTipoBene" runat="server" CssClass="textDadosOpcao" 
                                                    BackColor="#EDFFEB" Font-Bold="True">Tipo&nbsp;de&nbsp;Beneficiário&nbsp;</asp:label></TD>
											<TD align="left" width="56" bgColor="#edffeb">
												<TABLE id="Table7" cellSpacing="0" cellPadding="0" width="30" border="0">
													<TR>
														<TD class="textDadosCampo" bgColor="#edffeb"><asp:label id="lblValorBene" 
                                                                runat="server" BackColor="#EDFFEB"></asp:label></TD>
													</TR>
												</TABLE>
											    </TD>
											<TD class="textDadosOpcao" align="left" width="64" bgColor="#edffeb">
                                                &nbsp; 
                                                <asp:label id="lblDataNascimento" runat="server" CssClass="textDadosOpcao" 
                                                    BackColor="#EDFFEB" Font-Bold="True">Nascimento&nbsp;</asp:label>&nbsp; </TD>
											<TD align="left" width="98" bgColor="#edffeb">
												<TABLE id="Table8" cellSpacing="0" cellPadding="0" width="76" border="0">
													<TR>
														<TD class="textDadosCampo" width="97" bgColor="#edffeb">
                                                            <asp:label id="lblValorNasc" runat="server" BackColor="#EDFFEB"></asp:label></TD>
													</TR>
												</TABLE>
											</TD>
											<TD class="textDadosOpcao" align="left" width="32" bgColor="#edffeb">
                                                <asp:label id="lblIdade" runat="server" CssClass="textDadosOpcao" 
                                                    BackColor="#EDFFEB" Font-Bold="True">Idade&nbsp;</asp:label></TD>
											<TD align="left" width="34" bgColor="#edffeb">
												<TABLE id="Table9" cellSpacing="0" cellPadding="0" width="22" border="0">
													<TR>
														<TD class="textDadosCampo" width="27" bgColor="#edffeb">
                                                            <asp:label id="lblValorIdade" runat="server" BackColor="#EDFFEB"></asp:label>
                                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </TD>
													</TR>
												</TABLE>
											</TD>
											<TD class="textDadosOpcao" align="left" width="27" bgColor="#edffeb">
                                                <asp:label id="lblSexo" runat="server" CssClass="textDadosOpcao" 
                                                    BackColor="#EDFFEB" Font-Bold="True">Sexo&nbsp;</asp:label></TD>
											<TD align="left" width="60" bgColor="#edffeb">
												<TABLE id="Table10" cellSpacing="0" cellPadding="0" width="59" border="0">
													<TR>
														<TD class="textDadosCampo" width="58" bgColor="#edffeb">
                                                            <asp:label id="lblValorSexo" runat="server" BackColor="#EDFFEB"></asp:label></TD>
													</TR>
												</TABLE>
											</TD>
										</TR>
									</TABLE>
									<TABLE id="Table11" cellSpacing="0" cellPadding="0" width="490" border="0">
										<TR>
											<TD class="tableSpace"></TD>
										</TR>
									</TABLE>
									<TABLE id="Table12" cellSpacing="0" cellPadding="0" width="490" border="0">
										<TR vAlign="middle">
											<TD vAlign="middle" align="left" width="7" bgColor="#edffeb">&nbsp;</TD>
											<TD class="textDadosOpcao" vAlign="middle" align="right" width="7" bgColor="#edffeb">&nbsp;</TD>
											<TD class="textDadosOpcao" align="left" width="19" bgColor="#edffeb">
                                                <asp:label id="lblRegistro" runat="server" CssClass="textDadosOpcao" 
                                                    BackColor="#EDFFEB" Font-Bold="True">RE&nbsp;</asp:label></TD>
											<TD align="left" width="100" bgColor="#edffeb">
												<TABLE id="Table13" cellSpacing="0" cellPadding="0" border="0">
													<TR>
														<TD class="textDadosCampo" width="85" bgColor="#edffeb">
                                                            <asp:label id="lblValorRegistro" runat="server" BackColor="#EDFFEB"></asp:label></TD>
													</TR>
												</TABLE>
											    </TD>
											<TD class="textDadosOpcao" align="left" width="111" bgColor="#edffeb">&nbsp;<ASP:LABEL 
                                                    id="lblRegRev" runat="server" CssClass="textDadosOpcao" Font-Bold="True" 
                                                    BackColor="#edffeb">Regime&nbsp;Revezamento&nbsp;</ASP:LABEL></TD>
											<TD align="left" width="123" bgColor="#edffeb">
												<TABLE id="Table14" cellSpacing="0" cellPadding="0" width="75" border="0">
													<TR>
														<TD class="textDadosCampo" width="61" bgColor="#edffeb">
                                                            <asp:label id="lblValorRegRev" runat="server" Width="93px" BackColor="#EDFFEB"></asp:label></TD>
													</TR>
												</TABLE>
											</TD>
											<TD class="textDadosOpcao" align="left" width="45" bgColor="#edffeb">
                                                &nbsp;&nbsp;
                                                <asp:label id="lblJornada" runat="server" CssClass="textDadosOpcao" 
                                                    BackColor="#EDFFEB" Font-Bold="True" Visible="False">Jornada&nbsp;</asp:label></TD>
											<TD align="left" width="78" bgColor="#edffeb">
												<TABLE id="Table15" cellSpacing="0" cellPadding="0" width="77" border="0">
													<TR>
														<TD class="textDadosCampo" width="75" bgColor="#edffeb">
                                                            <asp:label id="lblValorJornada" runat="server" BackColor="#EDFFEB" 
                                                                Visible="False"></asp:label></TD>
													</TR>
												</TABLE>
											</TD>
										</TR>
									</TABLE>
									<TABLE id="Table16" cellSpacing="0" cellPadding="0" width="490" border="0">
										<TR>
											<TD class="tableSpace"></TD>
										</TR>
									</TABLE>
									<TABLE id="Table17" cellSpacing="0" cellPadding="0" width="490" border="0">
										<TR vAlign="middle">
											<TD vAlign="middle" align="left" width="7" bgColor="#edffeb">&nbsp;</TD>
											<TD class="textDadosOpcao" vAlign="middle" align="right" width="7" bgColor="#edffeb">&nbsp;</TD>
											<TD class="textDadosOpcao" align="left" width="51" bgColor="#edffeb">
                                                <asp:label id="lblAdmissao" runat="server" CssClass="textDadosOpcao" 
                                                    BackColor="#EDFFEB" Font-Bold="True">Admissão&nbsp;</asp:label></TD>
											<TD align="left" width="109" bgColor="#edffeb">
												<TABLE id="Table18" cellSpacing="0" cellPadding="0" width="75" border="0">
													<TR>
														<TD class="textDadosCampo" width="75" bgColor="#edffeb">
                                                            <asp:label id="lblValorAdmissao" runat="server" BackColor="#EDFFEB"></asp:label></TD>
													</TR>
												</TABLE>
											</TD>
											<TD class="textDadosOpcao" vAlign="bottom" align="left" width="50" bgColor="#edffeb">
                                                &nbsp;&nbsp;<asp:label id="lblDemissao" runat="server" CssClass="textDadosOpcao" 
                                                    BackColor="#EDFFEB" Font-Bold="True">Demissão&nbsp;</asp:label></TD>
											<TD align="left" width="121" bgColor="#edffeb">
												<TABLE id="Table19" cellSpacing="0" cellPadding="0" width="75" border="0">
													<TR>
														<TD class="textDadosCampo" width="75" bgColor="#edffeb">
                                                            <asp:label id="lblValorDemissao" runat="server" BackColor="#EDFFEB"></asp:label></TD>
													</TR>
												</TABLE>
											</TD>
											<TD class="textDadosOpcao" align="left" width="69" bgColor="#edffeb">
                                                &nbsp;
                                                <asp:label id="lblDataIni" runat="server" CssClass="textDadosOpcao" 
                                                    BackColor="#EDFFEB" Font-Bold="True">Início&nbsp;Função&nbsp;</asp:label></TD>
											<TD align="left" width="76" bgColor="#edffeb">
												<TABLE id="Table20" cellSpacing="0" cellPadding="0" width="75" border="0">
													<TR>
														<TD class="textDadosCampo" bgColor="#edffeb"><asp:label id="lblValorDataIni" 
                                                                runat="server" BackColor="#EDFFEB"></asp:label></TD>
													</TR>
												</TABLE>
											</TD>
										</TR>
									</TABLE>
									<TABLE id="Table21" cellSpacing="0" cellPadding="0" width="490" border="0">
										<TR>
											<TD class="tableSpace"></TD>
										</TR>
									</TABLE>
									<TABLE id="Table22" cellSpacing="0" cellPadding="0" width="490" border="0">
										<TR vAlign="middle">
											<TD vAlign="middle" align="left" width="7" bgColor="#edffeb">&nbsp;</TD>
											<TD class="textDadosOpcao" vAlign="middle" align="right" width="7" bgColor="#edffeb">&nbsp;</TD>
											<TD class="textDadosOpcao" align="left" width="95" bgColor="#edffeb">
                                                <asp:label id="lblGHE" runat="server" CssClass="textDadosOpcao" 
                                                    BackColor="#EDFFEB" Font-Bold="True">Tempo&nbsp;de&nbsp;Empresa</asp:label></TD>
											<TD align="left" width="100" bgColor="#edffeb">
												<TABLE id="Table23" cellSpacing="0" cellPadding="0" width="85" border="0">
													<TR>
														<TD class="textDadosCampo" width="85" bgColor="#edffeb">
                                                            <asp:label id="lblValorTempoEmpresa" runat="server" BackColor="#EDFFEB"></asp:label></TD>
													</TR>
												</TABLE>
											</TD>
											<TD class="textDadosOpcao" align="left" width="33" bgColor="#edffeb">
                                                <asp:label id="lblSetor" runat="server" CssClass="textDadosOpcao" 
                                                    BackColor="#EDFFEB" Font-Bold="True">Setor&nbsp;</asp:label></TD>
											<TD align="left" width="248" bgColor="#edffeb">
												<TABLE id="Table24" cellSpacing="0" cellPadding="0" width="247" border="0">
													<TR>
														<TD class="textDadosCampo" bgColor="#edffeb" width="247">
                                                            <asp:label id="lblValorSetor" runat="server" BackColor="#EDFFEB"></asp:label></TD>
													</TR>
												</TABLE>
											</TD>
										</TR>
									</TABLE>
									<TABLE id="Table25" cellSpacing="0" cellPadding="0" width="490" border="0">
										<TR>
											<TD class="tableSpace"></TD>
										</TR>
									</TABLE>
									<TABLE id="Table26" cellSpacing="0" cellPadding="0" width="490" border="0" >
										<TR>
											<TD vAlign="middle" align="left" width="7" bgColor="#edffeb">&nbsp;</TD>
											<TD class="textDadosOpcao" vAlign="middle" align="right" width="7" bgColor="#edffeb">&nbsp;</TD>
											<TD class="textDadosOpcao" align="left" width="38" bgColor="#edffeb">
                                                <asp:label id="lblFuncao" runat="server" CssClass="textDadosOpcao" 
                                                    BackColor="#EDFFEB" Font-Bold="True">Função&nbsp;</asp:label></TD>
											<TD align="left" width="438" bgColor="#edffeb">
												<TABLE id="Table27" cellSpacing="0" cellPadding="0" width="437" border="0">
													<TR>
														<TD class="textDadosOpcao" width="437" bgColor="#edffeb">
                                                            <asp:label id="lblValorFuncao" runat="server" BackColor="#EDFFEB"></asp:label></TD>
													</TR>
												</TABLE>
											</TD>
										</TR>
									</TABLE>
								</TD> <!-- celula direita-->
								<TD class="normalFont" align="center" width="110">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                                    </TD>

                                        
                                            <caption>
						<asp:Label id="Label4" runat="server" CssClass="largeboldFont">Guia de Encaminhamento</asp:Label>
                                                <input id="txtIdUsuario" type="text" visible="False"  style="visibility:hidden"  />
                                                <input id="txtIdEmpregado" type="text" visible="False" style="visibility:hidden" />
                                                <input id="txtIdEmpresa" type="text" visible="False" style="visibility:hidden"/>
                                </caption>
                                
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD noWrap align="left" class="style1"><BR>
					    <asp:CheckBox ID="chk_Basico" runat="server" 
                            Text="Imprimir apenas Nome/Setor/GHE" Font-Bold="True" 
                            Font-Size="X-Small" />
                            </td>
                            <tr>
                            <td>
					    <asp:Button ID="cmd_Voltar" runat="server" BackColor="#999999" 
                            CssClass="buttonFoto" Font-Size="XX-Small" onclick="cmd_Voltar_Click" 
                            Text="Voltar" Width="132px" />                        
                                <br />
                        </br>
					</TD>
				</TR>
			</TABLE>
            
            <table class="normalFont" id="Table1" cellspacing="0" cellpadding="0" width="600" align="center" border="0"  >

            <tr>
            
                <td>
                    <hr />
                </td>
                <tr>
                    <td>
                        <br />
                        <b><span class="style2">Clínicas Credenciadas :&nbsp;</span> </b>
                        <asp:DropDownList ID="cmb_Clinicas" runat="server" Font-Size="X-Small" 
                            Width="577px" AutoPostBack="True" 
                            onselectedindexchanged="cmb_Clinicas_SelectedIndexChanged" Height="17px">
                        </asp:DropDownList>
                        <asp:Label ID="lbl_Id_Clinica" runat="server" Text="0" Visible="False"></asp:Label>
                        <br />
                        <br />
                        <asp:Label ID="lbl_End_Clinica" runat="server" BackColor="#EDFFEB" 
                            ForeColor="#4B4B4B"></asp:Label>
                        <br />
                        <asp:Label ID="lbl_Fone_Clinica" runat="server" BackColor="#EDFFEB" 
                            ForeColor="#4B4B4B"></asp:Label>
                        <br />
                        <asp:Label ID="lbl_Horario" runat="server" BackColor="#EDFFEB" 
                            ForeColor="#4B4B4B"></asp:Label>
                        <br />
                        <asp:Label ID="lbl_email" runat="server" BackColor="#EDFFEB" Visible="false" 
                            ForeColor="#4B4B4B"></asp:Label>
                        <br />
                        <br />
                    </td>
                </tr>
            

                <tr>
                    <td>
                        <b><span class="style2">Tipo Exame:&nbsp; 
                        </span>
                        </b>
                        
                        <br />                        
                        <asp:RadioButton ID="rd_Admissao" Text="Admissão" runat="server" 
                            GroupName="Tipo" Font-Size="X-Small" AutoPostBack="True" 
                            CausesValidation="True" oncheckedchanged="rd_Admissao_CheckedChanged"/>                        
                        &nbsp;<asp:RadioButton ID="rd_Demissional" Text="Demissional" runat="server" 
                            GroupName="Tipo" Font-Size="X-Small" AutoPostBack="True" 
                            CausesValidation="True" oncheckedchanged="rd_Demissional_CheckedChanged"/>
                        &nbsp;&nbsp;
                        <asp:Label ID="lbl_Demissao" runat="server" Text="Data:"></asp:Label>
                        <b><asp:TextBox ID="txt_Demissao" runat="server" Width="82px" MaxLength="10" AutoPostBack="True" OnTextChanged="txt_Data_TextChanged" Enabled="False"></asp:TextBox>

                        </b>&nbsp;
                        <asp:RadioButton ID="rd_Mudanca" Text="Mudança Função" runat="server" 
                            GroupName="Tipo" Font-Size="X-Small" AutoPostBack="True" 
                            CausesValidation="True" oncheckedchanged="rd_Mudanca_CheckedChanged"/>
                        &nbsp;<asp:RadioButton ID="rd_Retorno" Text="Retorno ao Trabalho" runat="server" 
                            GroupName="Tipo" Font-Size="X-Small" AutoPostBack="True" 
                            CausesValidation="True" oncheckedchanged="rd_Retorno_CheckedChanged"/>
                        &nbsp;<asp:RadioButton ID="rd_Periodico" Text="Periódico" runat="server" 
                            GroupName="Tipo" Font-Size="X-Small" AutoPostBack="True" 
                            CausesValidation="True" oncheckedchanged="rd_Periodico_CheckedChanged" Checked="True"/>
                        &nbsp;<asp:RadioButton ID="rd_Outro" Text="Outro" runat="server" 
                            GroupName="Tipo" Font-Size="X-Small" AutoPostBack="True" 
                            CausesValidation="True" oncheckedchanged="rd_Outro_CheckedChanged" />
                        <br />
                    </td>
                </tr>


                <tr>
                    <td>
                    <hr />
                        <br />
                        <b><span class="style2">Data do Exame:</span> 
                        <asp:TextBox ID="txt_Data" runat="server" Width="82px" MaxLength="10" AutoPostBack="True" OnTextChanged="txt_Data_TextChanged"></asp:TextBox>

                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="style2">Hora do Exame:</span>
                        <asp:TextBox ID="txt_Hora" runat="server" Width="72px" MaxLength="10"></asp:TextBox>

                        &nbsp;&nbsp;&nbsp;&nbsp; 
                        <span class="style2">Obs.:</span>
                        <asp:TextBox ID="txt_Obs" runat="server" Width="270px" MaxLength="50" 
                            Height="19px" Font-Size="X-Small"></asp:TextBox>


                        <br />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:CheckBox ID="chk_Data" runat="server" Font-Size="X-Small" 
                            Text="Exibir Data na guia" Checked="True" />
                        </td>
                </tr>

                </table>

                <table>


  <tr>
                                                <td align="left" class="b">
                                                    &nbsp;&nbsp;&nbsp;
                                                <asp:CheckBox ID="chk_apt_Altura" runat="server" Font-Size="X-Small" 
                                                    Text="Indicar no ASO aptidao para trabalho em altura" />
                                                <br />
                                                    &nbsp;&nbsp;&nbsp;
                                                <asp:CheckBox ID="chk_apt_Confinado" runat="server" Font-Size="X-Small" 
                                                    Text="Indicar no ASO aptidao para trabalho para espaços confinados" />
                                                <br />
                                                    &nbsp;&nbsp;&nbsp;
                                                <asp:CheckBox ID="chk_apt_Eletricidade" runat="server" Font-Size="X-Small" 
                                                    Text="Indicar no ASO aptidao para serviços em eletricidade (NR 10)" />
                                                <br />
                                                    &nbsp;&nbsp;&nbsp;
                                                <asp:CheckBox ID="chk_apt_Alimento" runat="server" Font-Size="X-Small" 
                                                    Text="Indicar no ASO aptidao para manipular alimentos" />

                                                    <br />
&nbsp;&nbsp;&nbsp; <asp:CheckBox ID="chk_Apt_Brigadista" runat="server" AutoPostBack="True" 
                                                    Font-Size="X-Small" 
                                                    Text="Indicar no ASO aptidao para Brigadista" />

                                                    </td>                                                  
                                                    
                                                      <td>                                                   
                                                
                                                    &nbsp;&nbsp;&nbsp;
                                                <asp:CheckBox ID="chk_apt_Transportes" runat="server" AutoPostBack="True" 
                                                    Font-Size="X-Small" 
                                                    Text="Indicar no ASO aptidao para operar equipamentos de transporte motorizados" />
                                                                                                <br />
                                                    &nbsp;&nbsp;&nbsp;
                                                <asp:CheckBox ID="chk_apt_Submersas" runat="server" Font-Size="X-Small" 
                                                    Text="Indicar no ASO aptidao para atividades submersas (NR 15)" />
<br />
                                                    &nbsp;&nbsp;&nbsp;
                                                <asp:CheckBox ID="chk_apt_Aquaviarios" runat="server" Font-Size="X-Small" 
                                                    Text="Indicar no ASO aptidao para serviços aquaviários" />

                                                          <br />
&nbsp;&nbsp;&nbsp; <asp:CheckBox ID="chk_Apt_Socorrista" runat="server" AutoPostBack="True" 
                                                    Font-Size="X-Small" 
                                                    Text="Indicar no ASO aptidao para Socorrista" />

                                            </td>
                                        </tr>
                <tr>
                    <td class="style3">
                        <br />
                        <b><span class="style2">Exames PCMSO:&nbsp;</span> 
                        </td>
                        <td>
                    <span class="style2">
                <b>
                            Exames Selecionados :&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    </b>
                            </span>
                        </td>
                        </tr>
                <tr>
                    <td class="style3">

                        <asp:TextBox ID="txt_Exames" runat="server" Width="230px" BackColor="#FFEDAE" 
                            Font-Bold="True" Font-Size="X-Small" Height="161px" TextMode="MultiLine" ></asp:TextBox>

                        <br />

                        <br />
                                                         

                        </td>
                <td>
                                              
                        <asp:CheckBoxList ID="lst_Exames"  BackColor="#FFFFCC"  runat="server" style="text-align:left"
                            BorderStyle="Solid" Height="94px" Width="372px" RepeatColumns="2" 
                            BorderColor="#363636" Font-Size="X-Small"    >
                        </asp:CheckBoxList>
                      
                <b>
                    <br class="style2" />
                    <span class="style2">&nbsp;&nbsp;</span>&nbsp;<asp:Button 
                        ID="btnemp" runat="server" onclick="btnemp_Click" CssClass="buttonBox"
                        Text="Gerar Guia" Height="23px" Width="140px">
                    </asp:Button>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button 
                        ID="btnEmp2Via" runat="server" onclick="btnEmp2Via_Click" CssClass="buttonBox"
                        Text="Gerar 2.Via Guia/ASO/PCI" Height="23px" Width="140px">
                    </asp:Button>
                        <br />
                        <br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button 
                        ID="btnEmpASO" runat="server" onclick="btnempASO_Click" CssClass="buttonBox"
                        Text="Gerar Guia com ASO/PCI" Height="23px" Width="140px">
                    </asp:Button>
                    &nbsp;&nbsp;&nbsp;&nbsp;<br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:CheckBox ID="chk_eMail" runat="server" Font-Size="X-Small" 
                            Text="Enviar e-mail com Guia/ASO/PCI para clínica" />
                        &nbsp;&nbsp;&nbsp;
                    </b>
                </td>
                
                </tr>

                <tr>
                    <td align="left" class="auto-style1" >                        
                                              
                        <asp:ListBox ID="lst_IdExames" runat="server" Height="3px" Visible="False"></asp:ListBox>
                                              
                        <br />
                    </td>
                </tr>

            </tr>
            
            </table>


<%--		</form>
	</body>
</HTML>
--%>
        
         <eo:MsgBox ID="MsgBox1" runat="server" BackColor="#47729F" ControlSkinID="None" 
        HeaderHtml="Dialog Title" Height="100px" Width="250px" OnButtonClick="MsgBox1_ButtonClick" >
        <HeaderStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #ffbf00 3px solid; padding-left: 4px; font-weight: bold; font-size: 11px; padding-bottom: 2px; color: white; padding-top: 2px; border-bottom: #22456a 1px solid; font-family: verdana" />
        <ContentStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #7d97b6 1px solid; padding-left: 4px; border-left-width: 1px; font-size: 11px; border-left-color: #728eb8; padding-bottom: 4px; color: white; padding-top: 4px; border-bottom: #22456a 1px solid; font-family: verdana" />
        <FooterStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #7d97b6 1px solid; padding-left: 4px; border-left-width: 1px; font-size: 11px; border-left-color: #728eb8; padding-bottom: 4px; color: white; padding-top: 4px; border-bottom: #22456a 1px solid; font-family: verdana" />
        </eo:MsgBox>
         <eo:MsgBox ID="MsgBox2" runat="server" BackColor="#47729F" ControlSkinID="None" 
        HeaderHtml="Dialog Title" Height="350px" Width="650px" OnButtonClick="MsgBox2_ButtonClick" >
        <HeaderStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #ffbf00 3px solid; padding-left: 4px; font-weight: bold; font-size: 11px; padding-bottom: 2px; color: white; padding-top: 2px; border-bottom: #22456a 1px solid; font-family: verdana" />
        <ContentStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #7d97b6 1px solid; padding-left: 4px; border-left-width: 1px; font-size: 11px; border-left-color: #728eb8; padding-bottom: 4px; color: white; padding-top: 4px; border-bottom: #22456a 1px solid; font-family: verdana" />
        <FooterStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #7d97b6 1px solid; padding-left: 4px; border-left-width: 1px; font-size: 11px; border-left-color: #728eb8; padding-bottom: 4px; color: white; padding-top: 4px; border-bottom: #22456a 1px solid; font-family: verdana" />
        </eo:MsgBox>

</asp:Content>
