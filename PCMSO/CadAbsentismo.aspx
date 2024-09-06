<%@ Page language="c#" Inherits="Ilitera.Net.CadAbsentismo" Codebehind="CadAbsentismo.aspx.cs" Title="Ilitera.Net" %>

<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>


<HTML>
	<HEAD>

		<link href="https://cdn.jsdelivr.net/npm/bootstrap@5.2.3/dist/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
        <link href="css/forms.css" rel="stylesheet" type="text/css" />
        <link href="css/tabelas.css" rel="stylesheet" type="text/css" />
		<LINK href="scripts/style.css" type="text/css" rel="stylesheet">
		<script language="javascript">

		
		function VerificaData()
		{
			var Inicial = validar_data(document.frmCadAbsentismo.txtddi.value, document.frmCadAbsentismo.txtmmi.value, document.frmCadAbsentismo.txtaai.value, 'Data Inicial');
			var Prevista;
			var Retorno;
			
			if (isNull(document.frmCadAbsentismo.txtddp.value) && isNull(document.frmCadAbsentismo.txtmmp.value) && isNull(document.frmCadAbsentismo.txtaap.value))
				Prevista = true;
			else
				Prevista = validar_data(document.frmCadAbsentismo.txtddp.value, document.frmCadAbsentismo.txtmmp.value, document.frmCadAbsentismo.txtaap.value, 'Data Prevista');
			
			if (isNull(document.frmCadAbsentismo.txtddr.value) && isNull(document.frmCadAbsentismo.txtmmr.value) && isNull(document.frmCadAbsentismo.txtaar.value))
				Retorno = true;
			else
				Retorno = validar_data(document.frmCadAbsentismo.txtddr.value, document.frmCadAbsentismo.txtmmr.value, document.frmCadAbsentismo.txtaar.value, 'Data de Retorno');
			
			return (Inicial && Prevista && Retorno);
		}
		
		function Inicialize()
		{
			document.frmCadAbsentismo.txtddi.onkeypress = ChecarTAB;
			document.frmCadAbsentismo.txtddi.onfocus = PararTAB;
			document.frmCadAbsentismo.txtmmi.onkeypress = ChecarTAB;
			document.frmCadAbsentismo.txtmmi.onfocus = PararTAB;
			document.frmCadAbsentismo.txtaai.onkeypress = ChecarTAB;
			document.frmCadAbsentismo.txtaai.onfocus = PararTAB;
			document.frmCadAbsentismo.txtddp.onkeypress = ChecarTAB;
			document.frmCadAbsentismo.txtddp.onfocus = PararTAB;
			document.frmCadAbsentismo.txtmmp.onkeypress = ChecarTAB;
			document.frmCadAbsentismo.txtmmp.onfocus = PararTAB;
			document.frmCadAbsentismo.txtaap.onkeypress = ChecarTAB;
			document.frmCadAbsentismo.txtaap.onfocus = PararTAB;
			document.frmCadAbsentismo.txtddr.onkeypress = ChecarTAB;
			document.frmCadAbsentismo.txtddr.onfocus = PararTAB;
			document.frmCadAbsentismo.txtmmr.onkeypress = ChecarTAB;
			document.frmCadAbsentismo.txtmmr.onfocus = PararTAB;
			document.frmCadAbsentismo.txtaar.onkeypress = ChecarTAB;
			document.frmCadAbsentismo.txtaar.onfocus = PararTAB;
		}
        </script>
	   
	</HEAD>

	<body bottomMargin="0" leftMargin="0" topMargin="0" onload="Inicialize()" rightMargin="0">
		<form method="post" runat="server" defaultfocus="frmCadAbsentismo">
            <div class="container d-flex ms-5 ps-4 justify-content-center">
                <div class="row gx-3 gy-" style="width: 700px">
                        <br />

		 <%-- subtitulo --%>

                        <div class="col-12 subtituloBG mb-1 text-center" style="padding-top: 10px; margin-top: 50px;">
						    <asp:Label id="lblAbsentismo" runat="server" SkinID="TitleFont" CssClass="subtitulo">Cadastro de Absenteísmos</asp:Label>                       
                         </div>
					
						
					 <div class="col-12 mb-2">
						 <div class="col-12 mb-3">   
							 <div class="row">

								<div class="col-6">
									<div class="row">

										 <div class="col-12 gy-2">
											<asp:label id="Label19" runat="server" CssClass="tituloLabel form-label">Data & Hora de Início</asp:label>
										</div>
					
										<div class="col-3">
											<asp:textbox id="txtddi" onkeyup="NextTxt(this, 'txtmmi', 2)" runat="server" CssClass="texto form-control form-control-sm" MaxLength="2"></asp:textbox>
										</div>
			
										 <div class="col-1 text-center pt-2">
											<asp:Label runat="server" CssClass="texto">/</asp:Label>
										 </div>

										<div class="col-3">
											<asp:textbox id="txtmmi" onkeyup="NextTxt(this, 'txtaai', 2)" runat="server" CssClass="texto form-control form-control-sm" MaxLength="2"></asp:textbox>
											
										</div>
										
										 <div class="col-1 text-center pt-2">
											<asp:Label runat="server" CssClass="texto">/</asp:Label>
										</div>

										<div class="col-3">
											<asp:textbox id="txtaai" onkeyup="NextTxt(this, 'ddlHoraIni', 4)" runat="server" CssClass="texto form-control form-control-sm" MaxLength="4"></asp:textbox><BR>
										</div>

									<div class="col-4">
											<asp:dropdownlist id="ddlHoraIni" runat="server" CssClass="texto form-select form-select-sm"></asp:dropdownlist>
										</div>

										 <div class="col-1 mt-2 text-center">
                                              <asp:Label runat="server" CssClass="texto">:</asp:Label>
                                         </div>

									<div class="col-4">
										<asp:dropdownlist id="ddlMinutoIni" runat="server" CssClass="texto form-select form-select-sm"></asp:dropdownlist>
									</div>

									</div>
								</div>

								<div class="col-6 gx-5">
									<div class="row">
										
										<div class="col-12 gy-2">
											<asp:label id="Label20" runat="server" CssClass="tituloLabel form-label">Previsão de Retorno</asp:label>
										</div>

									 <div class="col-3">
										 <asp:textbox id="txtddp" onkeyup="NextTxt(this, 'txtmmp', 2)" runat="server" CssClass="texto form-control form-control-sm" MaxLength="2"></asp:textbox>
									</div>

										<div class="col-3">
											<asp:textbox id="txtmmp" onkeyup="NextTxt(this, 'txtaap', 2)" runat="server" CssClass="texto form-control form-control-sm" MaxLength="2"></asp:textbox>
										</div>

										<div class="col-3">
											<asp:textbox id="txtaap" onkeyup="NextTxt(this, 'txtddr', 4)" runat="server" CssClass="texto form-control form-control-sm" MaxLength="4"></asp:textbox>
										</div>
								</div>
								</div>

							<div class="col-6">
								<div class="row">

									<div class="col-12 gy-2">
										<asp:label id="Label21" runat="server" CssClass="tituloLabel form-label">Data & Hora de Retorno</asp:label>
									</div>

									<div class="col-3">
										<asp:textbox id="txtddr" onkeyup="NextTxt(this, 'txtmmr', 2)" runat="server" CssClass="texto form-control form-control-sm" MaxLength="2"></asp:textbox>
									</div>

									<div class="col-1 text-center pt-2">
											<asp:Label runat="server" CssClass="texto">/</asp:Label>
									</div>

									<div class="col-3">
										<asp:textbox id="txtmmr" onkeyup="NextTxt(this, 'txtaar', 2)" runat="server" CssClass="texto form-control form-control-sm" MaxLength="2"></asp:textbox>
									</div>
									
									<div class="col-1 text-center pt-2">
											<asp:Label runat="server" CssClass="texto">/</asp:Label>
									</div>

									<div class="col-3">
										<asp:textbox id="txtaar" onkeyup="NextTxt(this, 'ddlHoraRet', 4)" runat="server" CssClass="texto form-control form-control-sm" MaxLength="4"></asp:textbox>
									</div>
									
									<div class="col-4 gy-4">
										<asp:dropdownlist id="ddlHoraRet" runat="server" CssClass="texto form-select form-select-sm"></asp:dropdownlist>
									</div>

									 <div class="col-1 mt-4 text-center">
											<asp:Label runat="server" CssClass="texto">:</asp:Label>
                                     </div>

									<div class="col-4 gy-4">
										<asp:dropdownlist id="ddlMinutoRet" runat="server" CssClass="texto form-select form-select-sm"></asp:dropdownlist>
									</div>

								</div>
							</div>

									</div>
								</div>

						 <div class="col-12">
								<div class="row">

							<div class="col-12 gy-2">
								 <asp:Label ID="Label26" runat="server"  class="tituloLabel form-label col-form-label col-form-label-sm">Tipo de Absenteísmo</asp:Label>
							</div>

							<div class="col-6 gx-4 gy-2 ms-4">
								<asp:radiobuttonlist id="rblAbsentismo" runat="server" CssClass="texto form-control-sm" RepeatDirection="Horizontal" CellPadding="0" CellSpacing="0" Width="500px">
									<asp:ListItem Value="1" Selected="True">Ocupacional</asp:ListItem>
									<asp:ListItem Value="2">Outros (Assistencial)</asp:ListItem>
								</asp:radiobuttonlist>
							</div>

						 </div>
						</div>

						  <div class="col-12 mb-2">
							<div class="row">

						 <div class="col-3 gy-2">
								<asp:label id="Label5" runat="server" class="tituloLabel form-label col-form-label col-form-label-sm">Outros motivos:</asp:label>
								<asp:dropdownlist id="ddlOutros" runat="server" CssClass="texto form-select form-select-sm" AutoPostBack="True" ></asp:dropdownlist>
							</div>

						<div class="col-6 gy-2 mb-2">
							<asp:label id="Label2" runat="server" class="tituloLabel form-label col-form-label col-form-label-sm">Acidente que causou o Absenteísmo</asp:label>
							<asp:dropdownlist id="ddlAcidente" runat="server" CssClass="texto form-select form-select-sm" AutoPostBack="True" onselectedindexchanged="ddlAcidente_SelectedIndexChanged"></asp:dropdownlist>
						</div>

						<div class="col-12 gy-2">
							 <asp:label id="Label27" runat="server" class="tituloLabel form-label col-form-label col-form-label-sm">Observação:</asp:label>
							 <asp:TextBox ID="txtObs" runat="server" CssClass="texto form-control form-control-sm" MaxLength="50" ></asp:TextBox>
						 </div>

							</div>
							  </div>

						</div>
                                               
											

                                                <br />
                        <asp:CheckBox ID="chk_INSS" runat="server" AutoPostBack="True" CssClass="boldFont"  Text="Afastamento pelo INSS" Visible="False" />
                                                <br />
                                                <br />
                                       
				<div class="col-12 mb-2">
						<div class="row">
							<div class="col-8 gy-3">
								<asp:Label ID="lblAnotacoes0" runat="server" CssClass="tituloLabel">Arquivo do Atestado</asp:Label>
								<asp:TextBox ID="txt_Arq" runat="server" CssClass="texto form-control form-control-sm" ReadOnly="True" Rows="4" TextMode="MultiLine"></asp:TextBox>
							</div>
							<div class="col-4 gy-3">
								<asp:Button ID="btnProjeto" runat="server" Text="Visualizar Arquivo" CssClass="btn mt-5" OnClick="btnProjeto_Click"/>
							</div>
							<div class="col-8 gy-3">
								<asp:Label ID="Label7" runat="server" CssClass="tituloLabel">Inserir / Modificar Arquivo do Atestado: </asp:Label>
								<asp:FileUpload ID="File1" runat="server" ClientIDMode="Static" CssClass="texto form-control form-control-sm" />
							</div>

							<div class="col-8 gy-3">
								<asp:label id="Label29" runat="server" CssClass="tituloLabel">Emitente do Atestado: Órgão de Classe</asp:label>
								<asp:radiobuttonlist id="rblOrgaoOC" runat="server" CssClass="texto form-check-label bg-transparent border-0 ms-3" Width="500px" RepeatDirection="Horizontal"
												CellPadding="0" CellSpacing="0">
												<asp:ListItem Value="1" Selected="True">CRM</asp:ListItem>
												<asp:ListItem Value="2">CRO</asp:ListItem>
												<asp:ListItem Value="3">RMS</asp:ListItem>
												<asp:ListItem Value="4">Sem Dados Médicos</asp:ListItem>
								</asp:radiobuttonlist>
							</div>

							<div class="col-6 gy-3">
								<asp:label id="Label30" runat="server" CssClass="tituloLabel">Nome Médico/Dentista</asp:label>
								<asp:textbox id="txtNomeOC" runat="server" CssClass="texto form-control form-control-sm" MaxLength="70"></asp:textbox>
							</div>
							<div class="col-4 gx-2 gy-3">
								<asp:label id="Label31" runat="server" CssClass="tituloLabel">Número Conselho</asp:label>
								<asp:textbox id="txtNumeroOC" runat="server" CssClass="texto form-control form-control-sm" MaxLength="14"></asp:textbox>
							</div>
							<div class="col-2 gx-2 gy-3">
								<asp:label id="Label32" runat="server" CssClass="tituloLabel">UF</asp:label>
								<asp:textbox id="txtUFOC" runat="server" CssClass="texto form-control form-control-sm" MaxLength="2"></asp:textbox>
							</div>
						</div>
					</div>

								<div class="col-12 mb-3">   
									<div class="row">
										<asp:label id="lblCID" runat="server" class="tituloLabel form-label col-form-label col-form-label-sm">C.I.D. - Digite o código desejado ou uma palavra chave para Procurar a CID</asp:label>
								</div>
									</div>

								<div class="col-12">   
									<div class="row">

									<div class="col-6 gy-2">
										<div class="row">
											<div class="col-8">
												<asp:label id="Label28" runat="server" class="tituloLabel form-label col-form-label col-form-label-sm">Principal</asp:label>
												<asp:TextBox ID="txtCID" runat="server" CssClass="texto form-control form-control-sm" MaxLength="50" ></asp:TextBox>
											</div>
										<div class="col-4">
											<asp:button id="btnProcurar" runat="server" CssClass="btnMenor2 mt-3" Text="Procurar" onclick="btnProcurar_Click"></asp:button>
										</div>
										</div>
									</div>
							

									<div class="col-6 gx-2 gy-2">
										<div class="row">
											<div class="col-8">
												<asp:label id="Label1" runat="server" class="tituloLabel form-label col-form-label col-form-label-sm">2. CID</asp:label>
												 <asp:textbox id="txtCID2" runat="server" CssClass="texto form-control form-control-sm"></asp:textbox>
											</div>
										<div class="col-4">
											<asp:button id="btnProcurar2" runat="server" CssClass="btnMenor2 mt-3" Text="Procurar" onclick="btnProcurar2_Click"></asp:button>
										</div>
									</div>
								</div>

							</div>
							</div>


                                        
                                    <asp:Label ID="lbl_Id1" runat="server" Text="0" Visible="False"></asp:Label>
                                    <br />
                                    <br />

                                    <asp:Label ID="lbl_Id2" runat="server" Text="0" Visible="False"></asp:Label>
                                    <br />
                                    <br />

					<div class="col-12">
						<div class="row">

						<div class="col-6 gy-3">
							<div class="row">
								<div class="col-8">
									<asp:label id="Label3" runat="server" CssClass="tituloLabel">3. CID</asp:label>
									<asp:textbox id="txtCID3" runat="server" CssClass="texto form-control form-control-sm"></asp:textbox>
								</div>
								<div class="col-4">
									<asp:button id="btnProcurar3" runat="server" CssClass="btnMenor2 mt-3" Text="Procurar" onclick="btnProcurar3_Click"></asp:button>
								</div>
							</div>
						</div>

						<div class="col-6 gx-2 gy-3">
							<div class="row">
								<div class="col-8">
									<asp:label id="Label4" runat="server" CssClass="tituloLabel">4. CID</asp:label>
									<asp:textbox id="txtCID4" runat="server" CssClass="texto form-control form-control-sm"></asp:textbox>
								</div>
								<div class="col-4">
									<asp:button id="btnProcurar4" runat="server" CssClass="btnMenor2 mt-3" Text="Procurar" onclick="btnProcurar4_Click"></asp:button>
								</div>
							</div>
						</div>

						<div class="col-6 gy-3">
							<asp:dropdownlist id="ddlCID" runat="server" CssClass="texto form-select form-select-sm" AutoPostBack="True">
								<asp:ListItem Value="0">Absentismo sem CID cadastrada!</asp:ListItem>
							</asp:dropdownlist>
						</div>
						</div>
					</div>

					<div class="col-12 text-center mt-5">
						<asp:button id="btnOK" runat="server" CssClass="btn" Text="Gravar" onclick="btnOK_Click"></asp:button>
						<asp:button id="btnExcluir" runat="server" CssClass="btn" Text="Excluir" EnableViewState="False" onclick="btnExcluir_Click"></asp:button>
						<asp:button id="btnCancelar" runat="server" CssClass="btn2" Text="Cancelar"></asp:button>
					</div>

	
					<asp:Label ID="lbl_Procura" runat="server" Text="0" Visible="False"></asp:Label>

<eo:MsgBox ID="MsgBox1" runat="server" BackColor="#47729F" ControlSkinID="None" 
        HeaderHtml="Dialog Title" Height="100px" Width="168px">
        <HeaderStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #ffbf00 3px solid; padding-left: 4px; font-weight: bold; font-size: 11px; padding-bottom: 2px; color: white; padding-top: 2px; border-bottom: #22456a 1px solid; font-family: verdana" />
        <ContentStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #7d97b6 1px solid; padding-left: 4px; border-left-width: 1px; font-size: 11px; border-left-color: #728eb8; padding-bottom: 4px; color: white; padding-top: 4px; border-bottom: #22456a 1px solid; font-family: verdana" />
        <FooterStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #7d97b6 1px solid; padding-left: 4px; border-left-width: 1px; font-size: 11px; border-left-color: #728eb8; padding-bottom: 4px; color: white; padding-top: 4px; border-bottom: #22456a 1px solid; font-family: verdana" />
    </eo:MsgBox>

			
		</form>
	</body>
</HTML>