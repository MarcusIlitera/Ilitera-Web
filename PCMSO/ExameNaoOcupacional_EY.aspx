<%@ Page Title="Ilitera.Net" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="True"
    CodeBehind="ExameNaoOcupacional_EY.aspx.cs" Inherits="Ilitera.Net.ExameNaoOcupacional_EY" %>

<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>


<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    

    <style type="text/css">
        .defaultFont
    {
        width: 586px;
            height: 20px;
        }
        
        .auto-style1 {
            width: 795px;
        }
                
        .auto-style2 {
            font-family: Verdana;
            font-weight: bold;
            font-size: xx-small;
            color: #44926D;
        }
                
        .auto-style3 {
            width: 787px;
            height: 20px;
        }
                
        .auto-style4 {
            width: 149px;
        }
                
    </style>
</asp:Content>

  


<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent" >

    <eo:CallbackPanel runat="server" id="CallbackPanel1" Triggers="" Width="519px">
	
        <LINK href="scripts/style.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="scripts/validador.js"></script>
		<script language="javascript">
        var jaclicou = 0;

		function AbreCadastro(strPage, IdCliente, IdUsuario)
		{
			addItemPop(centerWin(strPage + '.aspx?IdEmpresa=' + IdCliente + '&IdUsuario=' + IdUsuario + '', 560, 370, 'CadQueixaClinica'));
        }

        				
		function VerificaProcesso()
		{
			if (jaclicou == 0)
				jaclicou=1;
			else
			{
				window.alert("Sua solicitação está sendo processada.\nAguarde!");
				return false;
			}
		}
		
		function ConsisteCkeckBoxDeAlteracao(checkBox){
			tipo = checkBox.id.substring(checkBox.id.length - 1, checkBox.id.length);
			if(tipo == 'S')
				checkBoxAux = eval('document.frmExameClinico.' + checkBox.id.substring(0, checkBox.id.length - 1) + 'N');
			if (tipo == 'N')	
				checkBoxAux = eval('document.frmExameClinico.' + checkBox.id.substring(0, checkBox.id.length - 1) + 'S');
			if (checkBoxAux.checked)  
				checkBoxAux.checked = !checkBox.checked
		}
		function btnAddQueixas_onclick() {

		}



		function on_column_gettext(column, item, cellValue) {
		    //if (cellValue == 1)
		    //    return "Sim";
		    //else
		    //	return "Nao";
		    return cellValue;
		}

		function on_begin_edit(cell) {
		    //Get the current cell value
		    var v = 0;
		    var valor = cell.getValue();

		    if (valor == "Sim")
		        v = 1;
		    else
		        v = 0;
		    //alert(v);
		    //Use index 0 if there is no value
		    //if (v == null)
		    //  v = 0;

		    //Load the value into our drop down box
		    var dropDownBox = document.getElementById("grade_dropdown");
		    dropDownBox.selectedIndex = v;
		}

		function on_end_edit(cell) {
		    //Get the new value from the drop down box
		    var dropDownBox = document.getElementById("grade_dropdown");
		    var v = dropDownBox.selectedIndex;

		    //Use null value if user has not selected a
		    //value or selected "-Please Select-"
		    //if (v == 0)    
		    //  v = null;

		    //Return the new value to the grid    
		    //return v;

		    if (v == 1)
		        return "Sim";
		    else
		        return "Nao";
		}


        </script>

     
	<%--</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0">
		<form id="FormExameNaoOcupacional" method="post" runat="server">--%>
<%--        <igmisc:WebAsyncRefreshPanel ID="warpExameClinico" runat="server" CssClass="defaultFont"
                            Height="" Width="560px" InitializePanel="warpExameClinico_InitializePanel" RefreshComplete="warpExameClinico_RefreshComplete">
--%>



							<eo:TabStrip ID="TabStrip1" runat="server" ControlSkinID="None" 
                            MultiPageID="MultiPage1">
                            <topgroup>
                                <Items>
                                    <eo:TabItem Text-Html="Dados">
                                    </eo:TabItem>
                                    <eo:TabItem Text-Html="Anamnese">
                                    </eo:TabItem>
                                </Items>
                            </topgroup>
                            <lookitems>
                                <eo:TabItem Height="21" ItemID="_Default" LeftIcon-SelectedUrl="00010605" 
                                    LeftIcon-Url="00010604" 
                                    NormalStyle-CssText="background-image: url(00010602); background-repeat: repeat-x; font-weight: normal; color: black;" 
                                    RightIcon-SelectedUrl="00010607" RightIcon-Url="00010606" 
                                    SelectedStyle-CssText="background-image: url(00010603); background-repeat: repeat-x; font-weight: bold; color: #ff7e00;" 
                                    Text-Padding-Bottom="2" Text-Padding-Top="1">
                                    <subgroup itemspacing="1" 
                                        style-csstext="background-image:url(00010601);background-position-y:bottom;background-repeat:repeat-x;color:black;cursor:hand;font-family:Verdana;font-size:12px;">
                                    </subgroup>
                                </eo:TabItem>
                            </lookitems>
                        </eo:TabStrip>



             <eo:MultiPage ID="MultiPage1" runat="server" Height="400" Width="550">
            
                  <eo:PageView ID="Pageview1" runat="server" Width="543px">







			<TABLE class="auto-style3" id="Table1" cellSpacing="0" cellPadding="0" align="center"
				border="0">
				<TR>
					<TD align="center"><%--                                            <eo:StaticColumn HeaderText="Resultado" AllowSort="True" 
                                                DataField="Resultado" Name="Resultado" ReadOnly="False" 
                                                SortOrder="Ascending" Text="" Width="75">
                                                <CellStyle CssText="font-family:Tahoma;font-size:6pt;text-align:Center;" />           
                                            </eo:StaticColumn>--%>
                        <asp:Label ID="Label4" runat="server" CssClass="largeboldFont">Exame Ambulatorial</asp:Label>
                        <br>
                        <br />
                        <table border="0" cellpadding="0" cellspacing="0" class="defaultFont" width="400">
                            <tr>
                                <td align="center" width="133">
                                    <asp:Label ID="Label2" runat="server" CssClass="boldFont" SkinID="BoldFont">Data Exame</asp:Label>
                                </td>
                                <td align="center" width="133">
                                    <asp:Label ID="Label14" runat="server" CssClass="boldFont" SkinID="BoldFont">Hora Exame</asp:Label>
                                </td>
                                <td align="center" width="134">
                                    <asp:Label ID="lblAltura" runat="server" CssClass="boldFont" SkinID="BoldFont">Altura (cm)</asp:Label>
                                </td>
                                <td align="center" width="133">
                                    <asp:Label ID="lblPeso" runat="server" CssClass="boldFont" SkinID="BoldFont">Peso(Kg)</asp:Label>
                                </td>
                                <td align="center" width="133">
                                    <asp:Label ID="Label1" runat="server" CssClass="boldFont" SkinID="BoldFont">IMC</asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" width="133">
                                    <asp:TextBox ID="wdtDataExame" runat="server" CssClass="inputBox" DisplayModeFormat="dd/MM/yyyy" HorizontalAlign="Center" ImageDirectory=" " Nullable="False" Width="98px"></asp:TextBox>
                                </td>
                                <td align="center" width="133">
                                    <asp:TextBox ID="txtHora" runat="server" CssClass="inputBox" DisplayModeFormat="HH:MM" HorizontalAlign="Center" ImageDirectory=" " Nullable="False" Width="66px" MaxLength="5"></asp:TextBox>
                                </td>
                                <td align="center" width="134">
                                    <asp:TextBox ID="wneAltura" runat="server" AutoPostBack="True" CssClass="inputBox" DataMode="Float" Nullable="False" Width="66px">0</asp:TextBox>
                                </td>
                                <td align="center" width="133">
                                    <asp:TextBox ID="wnePeso" runat="server" AutoPostBack="True" CssClass="inputBox"  DataMode="Float" Nullable="False" Width="57px">0</asp:TextBox>
                                </td>
                                <td align="center" width="133">
                                    <asp:TextBox ID="txtIMC" runat="server" BackColor="#666666" CssClass="inputBox" DataMode="Float" Font-Bold="True" ForeColor="White" Nullable="False" ReadOnly="True" Width="50px"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                        <br />
                        <table border="0" cellpadding="0" cellspacing="0" class="defaultFont" width="400">
                            <tr>
                                <td align="center" class="auto-style4">
                                    <asp:Label ID="lblPA" runat="server" CssClass="boldFont" SkinID="BoldFont">Sistólica x Diastólica</asp:Label>
                                </td>
                                <td align="center" width="134">
                                    <asp:Label ID="lblPulso" runat="server" CssClass="boldFont" SkinID="BoldFont">Pulso</asp:Label>
                                </td>
                                <td align="center" width="133">
                                    <asp:Label ID="lblTemperatura" runat="server" CssClass="boldFont" SkinID="BoldFont">Temperatura C°</asp:Label>
                                </td>
                                <td align="center" width="133">
                                    <asp:Label ID="Label5" runat="server" CssClass="boldFont" SkinID="BoldFont">Glicemia mg/dl</asp:Label>
                                </td>
                                <td align="center" width="133">
                                    <asp:Label ID="Label13" runat="server" CssClass="boldFont" SkinID="BoldFont">Sat. Oxigênio %</asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" class="auto-style4">
                                    <asp:TextBox ID="wteSistolica" runat="server" CssClass="inputBox" Width="32px" MaxLength="3">0</asp:TextBox>
                                    &nbsp;x
                                    <asp:TextBox ID="wteDiastolica" runat="server" CssClass="inputBox" MaxLength="3" Width="32px">0</asp:TextBox>
                                </td>
                                <td align="center" width="134">
                                    <asp:TextBox ID="wnePulso" runat="server" CssClass="inputBox" DataMode="Int" Nullable="False" Width="70px">0</asp:TextBox>
                                </td>
                                <td align="center" width="133">
                                    <asp:TextBox ID="wneTemperatura" runat="server" CssClass="inputBox" DataMode="Float" Nullable="False" Width="78px">0,0</asp:TextBox>
                                </td>
                                <td align="center" width="133">
                                    <asp:TextBox ID="txtGlicose" runat="server" CssClass="inputBox" DataMode="Float" MaxLength="20" Nullable="False" Width="85px">0</asp:TextBox>
                                </td>
                                <td align="center" width="133">
                                    <asp:TextBox ID="txtSaturacao" runat="server" CssClass="inputBox" DataMode="Float" MaxLength="20" Nullable="False" Width="91px">0,0</asp:TextBox>
                                </td>

                            </tr>
                        </table>
      
                        <br />
                        &nbsp;<asp:Label ID="Label8" runat="server" CssClass="boldFont" SkinID="BoldFont">Motivo do Atendimento :</asp:Label>
                        &nbsp;
                        <br />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:RadioButton ID="rd_MA_Acidente" runat="server" AutoPostBack="True" CssClass="normalFont" GroupName="1" OnCheckedChanged="rd_Enfermagem_CheckedChanged" Text="Acidente de Trabalho/Trajeto" />
                        &nbsp;&nbsp;
                        <asp:RadioButton ID="rd_MA_Amb" runat="server" AutoPostBack="True" CssClass="normalFont" GroupName="1" OnCheckedChanged="rd_Medico_CheckedChanged" Text="Atend.Ambulatorial" Checked="True" />
                        &nbsp;&nbsp;
                        <asp:RadioButton ID="rd_MA_Emerg" runat="server" AutoPostBack="True" CssClass="normalFont" GroupName="1" OnCheckedChanged="rd_Outros_CheckedChanged" Text="Emergência/Urgência" />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <br />
                        <br />
                        <asp:Label ID="lblMedico" runat="server" CssClass="boldFont" SkinID="BoldFont">Responsável pelo atendimento</asp:Label>
                        &nbsp;&nbsp;
                        <asp:TextBox ID="txt_Responsavel" runat="server" CssClass="inputBox" DataMode="Float" MaxLength="30" Nullable="False" Width="120px"></asp:TextBox>
                        &nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Label ID="lblMedico1" runat="server" CssClass="boldFont" SkinID="BoldFont">Histórico de Doenças :</asp:Label>
                        </span>&nbsp;&nbsp;
                        <asp:TextBox ID="txt_Historico" runat="server" CssClass="inputBox" MaxLength="100" Nullable="False" Width="230px"></asp:TextBox>
                        <br />
                        <span class="auto-style2">
                        <br />
                        <asp:CheckBox ID="chk_Alergia" runat="server" CssClass="inputBox" Text="Tem alergia à alguma medicação ?" />
                        &nbsp; &nbsp;
                        <asp:Label ID="Label9" runat="server" CssClass="boldFont" SkinID="BoldFont">Quais:</asp:Label>
                        &nbsp;<asp:TextBox ID="txt_Alergia" runat="server" CssClass="inputBox" DataMode="Float" MaxLength="40" Nullable="False" Width="180px"></asp:TextBox>
                        &nbsp;&nbsp;&nbsp;
                        <br />
                        <asp:CheckBox ID="chk_Medicacao" runat="server" CssClass="inputBox" Text="Faz uso de medicação contínua ?" />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="Label10" runat="server" CssClass="boldFont" SkinID="BoldFont">Quais:</asp:Label>
                        &nbsp;<asp:TextBox ID="txt_Medicacao" runat="server" CssClass="inputBox" DataMode="Float" MaxLength="40" Nullable="False" Width="180px"></asp:TextBox>
                        <br />
                        <asp:CheckBox ID="chk_PS" runat="server" CssClass="inputBox" Text="Colaborador conduzido ao P.S. ?" />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="Label6" runat="server" CssClass="boldFont" SkinID="BoldFont">Qual:</asp:Label>
                        &nbsp;<asp:TextBox ID="txt_PS" runat="server" CssClass="inputBox" DataMode="Float" MaxLength="40" Nullable="False" Width="180px"></asp:TextBox>
                        <br />

                        <br />
                        <asp:Label ID="lblMedico0" runat="server" CssClass="boldFont" SkinID="BoldFont">Queixa(s) Principal(is)</asp:Label>
                        <br>
                        <asp:CheckBoxList ID="chk_Diagnostico" runat="server" AutoPostBack="True" BorderStyle="Solid" Font-Size="X-Small" Height="108px" RepeatColumns="6" RepeatDirection="Horizontal" BorderColor="#6600FF" BorderWidth="2px">
                            <asp:ListItem Value="CF">Cefaléia</asp:ListItem>
                            <asp:ListItem Value="CM">Cólica Menstrual</asp:ListItem>
                            <asp:ListItem Value="AR">Alergia Respiratória</asp:ListItem>
                            <asp:ListItem Value="DE">Dor de Estômago</asp:ListItem>
                            <asp:ListItem Value="CV">Convulsão</asp:ListItem>
                            <asp:ListItem Value="DA">Dor Abdominal</asp:ListItem>
                            <asp:ListItem Value="DI">Diarréia</asp:ListItem>
                            <asp:ListItem Value="AD">Alergia Dermatológica</asp:ListItem>
                            <asp:ListItem Value="DM">Dor Muscular</asp:ListItem>
                            <asp:ListItem Value="CE">Contusão/Entorse</asp:ListItem>
                            <asp:ListItem Value="CP">Controle de PA</asp:ListItem>
                            <asp:ListItem Value="CT">Controle de Temperatura</asp:ListItem>
                            <asp:ListItem Value="CU">Curativo</asp:ListItem>
                            <asp:ListItem Value="SD">Síncope/Desmaio</asp:ListItem>
                            <asp:ListItem Value="FR">Fratura</asp:ListItem>
                            <asp:ListItem Value="VT">Vômito</asp:ListItem>
                            <asp:ListItem Value="EJ">Enjôo</asp:ListItem>
                            <asp:ListItem Value="AS">Ansiedade/Stress</asp:ListItem>
                            <asp:ListItem Value="TT">Tontura</asp:ListItem>
                            <asp:ListItem Value="RP">Retirada de Ponto</asp:ListItem>
                            <asp:ListItem Value="TO">Tosse</asp:ListItem>
                            <asp:ListItem Value="DG">Dor de Garganta</asp:ListItem>
                            <asp:ListItem Value="DO">Dor de Ouvido</asp:ListItem>
                            <asp:ListItem Value="VE">Vertigem</asp:ListItem>
                            <asp:ListItem Value="FB">Febre</asp:ListItem>
                            <asp:ListItem Value="GR">Gripe/Resfriado</asp:ListItem>
                            <asp:ListItem Value="ME">Mal-Estar</asp:ListItem>
                            <asp:ListItem Value="CL">Congestão Nasal</asp:ListItem>
                            <asp:ListItem Value="IO">Irritação Ocular</asp:ListItem>
                        </asp:CheckBoxList>
                        <asp:Label ID="Label12" runat="server" CssClass="boldFont" SkinID="BoldFont">Outros:</asp:Label>
                        &nbsp;<asp:TextBox ID="txt_Diagnostico_Enfermagem" runat="server" CssClass="inputBox" DataMode="Float" MaxLength="100" Nullable="False" Width="250px"></asp:TextBox>
                        <br />
                        &nbsp;<br>
                        <asp:Label ID="Label11" runat="server" CssClass="boldFont" SkinID="BoldFont">Medicações Administradas</asp:Label>
                        <br>
                        <asp:CheckBoxList ID="chk_Planejamento" runat="server" AutoPostBack="True" BorderStyle="Solid" Font-Size="X-Small" Height="108px" RepeatColumns="4" RepeatDirection="Horizontal" BorderColor="#0099CC" BorderWidth="2px">
                            <asp:ListItem Value="PC">Paracetamol 500mg(cp)</asp:ListItem>
                            <asp:ListItem Value="DP">Dipirona (gotas)</asp:ListItem>
                            <asp:ListItem Value="CV">Cefaliv (cp)</asp:ListItem>
                            <asp:ListItem Value="SF">Sal de Frutas</asp:ListItem>
                            <asp:ListItem Value="BS">Buscopan Simples (gotas)</asp:ListItem>
                            <asp:ListItem Value="DN">Dipirona (cp)</asp:ListItem>
                            <asp:ListItem Value="HA">Hidróxido de Alumínio</asp:ListItem>
                            <asp:ListItem Value="AF">Allexofedrin D (cp)</asp:ListItem>
                            <asp:ListItem Value="DD">Domperidona (cp)</asp:ListItem>
                            <asp:ListItem Value="ST">Simeticona (gotas)</asp:ListItem>
                            <asp:ListItem Value="PL">Paracetamol (gotas)</asp:ListItem>
                            <asp:ListItem Value="DF">Dorflex (cp)</asp:ListItem>
                            <asp:ListItem Value="DG">Doralgina (cp)</asp:ListItem>
                            <asp:ListItem Value="CF">Creme Fenergan</asp:ListItem>
                            <asp:ListItem Value="TD">Trimedal</asp:ListItem>
                            <asp:ListItem Value="BC">Buscopan Composto (gotas)</asp:ListItem>
                            <asp:ListItem Value="NL">Nimesulida (cp)</asp:ListItem>
                            <asp:ListItem Value="DY">Diclofenaco Spray</asp:ListItem>
                            <asp:ListItem Value="LR">Lerin (colírio)</asp:ListItem>
                            <asp:ListItem Value="MX">Maxidex (colírio)</asp:ListItem>
                            <asp:ListItem Value="IL">Inalação</asp:ListItem>
                            <asp:ListItem Value="BT">Berotec</asp:ListItem>
                            <asp:ListItem Value="AT">Atrovent</asp:ListItem>
                            <asp:ListItem Value="SN">Sulfato de Neomicina (pomada)</asp:ListItem>
                            <asp:ListItem Value="BE">Benalet (pastilhas)</asp:ListItem>
                            <asp:ListItem Value="FX">Flanax (naproxeno)</asp:ListItem>
                            <asp:ListItem Value="SK">Seakalm</asp:ListItem>
                            <asp:ListItem Value="D6">Dramin B6 (gotas)</asp:ListItem>
                            <asp:ListItem Value="IS">Ibuprofeno Sódico (cp)</asp:ListItem>
                            <asp:ListItem Value="ID">Isordil Sublingual</asp:ListItem>
                            <asp:ListItem Value="LI">Loratadina (cp)</asp:ListItem>
                            <asp:ListItem Value="PS">Plasil (solução injetável )</asp:ListItem>
                            <asp:ListItem Value="PD">Profenid (solução injetável)</asp:ListItem>
                            <asp:ListItem Value="TY">Tylenol Sinus</asp:ListItem>
                            <asp:ListItem Value="BL">Berlison (creme)</asp:ListItem>
                            <asp:ListItem Value="CI">Captropil 25mg</asp:ListItem>
                            <asp:ListItem Value="PN">Prednisona 20mg</asp:ListItem>
                            <asp:ListItem Value="PO">Ponstan</asp:ListItem>
                            <asp:ListItem Value="FG">Fenergan (solução injetável)</asp:ListItem>
                            <asp:ListItem Value="FL">Floratil (cp)</asp:ListItem>
                        </asp:CheckBoxList>
                        <asp:Label ID="Label3" runat="server" CssClass="boldFont" SkinID="BoldFont">Outros:</asp:Label>
                        &nbsp;<asp:TextBox ID="txt_Planejamento" runat="server" CssClass="inputBox" DataMode="Float" MaxLength="100" Nullable="False" Width="250px"></asp:TextBox>
                        <br />
                        <br />
                        <br />
                        <asp:Label ID="lblAnotacoes" runat="server" CssClass="boldFont" SkinID="BoldFont">Avaliação da Enfermagem</asp:Label>
                        <br>
                        <asp:TextBox ID="txt_Avaliacao" runat="server" CssClass="inputBox" MaxLength="500" Rows="3" tabIndex="1" TextMode="MultiLine" Width="459px"></asp:TextBox>
                        <br>

                      
                        </span>
     
 
                       
                 

</TABLE>



                      </eo:PageView>



        <eo:PageView ID="Pageview2" runat="server">


                                    
                                    <eo:Grid ID="gridAnamnese" runat="server" BorderColor="Black" 
                                            BorderWidth="1px" ColumnHeaderAscImage="00050403" 
                                            ColumnHeaderDescImage="00050404" ColumnHeaderDividerImage="00050402" 
                                            FixedColumnCount="1" Font-Bold="False" Font-Italic="False" Font-Names="Verdana" 
                                            Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" 
                                            Font-Underline="False" GridLineColor="240, 240, 240" 
                                            GridLines="Both" Height="445px" Width="800px" ColumnHeaderDividerOffset="6" 
                                            ColumnHeaderHeight="18" ItemHeight="18" KeyField="IdAnamneseExame"  
                                    OnItemCommand="gridAnamnese_ItemCommand"  
                                    ClientSideOnItemCommand="OnItemCommand" FullRowMode="False"  >
                                        <ItemStyles>
                                            <eo:GridItemStyleSet>
                                                <ItemStyle CssText="background-color: white" />
                                                <AlternatingItemStyle CssText="background-color:#ffffcc;" />
                                                <SelectedStyle CssText="border-bottom-style:solid;border-left-style:solid;border-right-style:solid;border-top-style:solid;" />
                                                <CellStyle CssText="padding-left:8px;padding-top:2px; color:#black;white-space:nowrap;" />
                                            </eo:GridItemStyleSet>
                                        </ItemStyles>
                                        <ColumnHeaderTextStyle CssText="" />
                                        <ColumnHeaderStyle CssText="background-image:url('00050401');padding-left:8px;padding-top:2px;" />
                                        <Columns>

                                            <eo:StaticColumn HeaderText="" AllowSort="True" 
                                                DataField="IdAnamneseExame" Name="IdAnamneseExame" ReadOnly="True" 
                                                SortOrder="Ascending" Text="" Width="0">
                                                <CellStyle CssText="background-color:#ffffcc;font-family:Tahoma;font-weight:bold;text-align:center" />
                                            </eo:StaticColumn>

                                            <eo:StaticColumn HeaderText="" AllowSort="True" 
                                                DataField="IdExameBase" Name="IdExameBase" ReadOnly="True" 
                                                SortOrder="Ascending" Text="" Width="0">
                                                <CellStyle CssText="background-color:#ffffcc;font-family:Tahoma;font-weight:bold;text-align:center" />
                                            </eo:StaticColumn>

                                            <eo:StaticColumn HeaderText="" AllowSort="True" 
                                                DataField="IdAnamneseDinamica" Name="IdAnamneseDinamica" ReadOnly="True" 
                                                SortOrder="Ascending" Text="" Width="0">
                                                <CellStyle CssText="background-color:#ffffcc;font-family:Tahoma;font-weight:bold;text-align:center" />
                                            </eo:StaticColumn>

                                            <eo:StaticColumn HeaderText="" AllowSort="True" 
                                                DataField="Peso" Name="Peso" ReadOnly="True" 
                                                SortOrder="Ascending" Text="" Width="0">
                                                <CellStyle CssText="background-color:#ffffcc;font-family:Tahoma;font-weight:bold;text-align:center" />
                                            </eo:StaticColumn>

                                            <eo:StaticColumn HeaderText="Sistema" AllowSort="True" 
                                                DataField="Sistema" Name="Sistema" ReadOnly="True" 
                                                SortOrder="Ascending" Text="" Width="155">
                                                <CellStyle CssText="font-family:Tahoma;font-size:7pt;font-weight:bold;text-align:left;" />
                                            </eo:StaticColumn>

                                            <eo:StaticColumn HeaderText="Questao" AllowSort="True" 
                                                DataField="Questao" Name="Questao" ReadOnly="True" 
                                                SortOrder="Ascending" Text="" Width="535">
                                                <CellStyle CssText="font-family:Tahoma;font-size:7pt;text-align:left;" />
                                            </eo:StaticColumn>

                                            

                                            <eo:CustomColumn HeaderText="Resultado" 
                                                DataField="Resultado" Name="Resultado" ReadOnly="False" 
                                                Width="75" DataType="String"  ClientSideEndEdit="on_end_edit" ClientSideBeginEdit="on_begin_edit"  ClientSideGetText="on_column_gettext" >
                                                <CellStyle CssText="font-family:Tahoma;font-size:7pt;text-align:Center;" />           
                                               <EditorTemplate>
				                                                <select id="grade_dropdown"  >
                                                                    <option>Não</option>
					                                                <option>Sim</option>					                                                
				                                                </select>
			                                    </EditorTemplate>
                                            </eo:CustomColumn>


<%--                                            <eo:StaticColumn HeaderText="Resultado" AllowSort="True" 
                                                DataField="Resultado" Name="Resultado" ReadOnly="False" 
                                                SortOrder="Ascending" Text="" Width="75">
                                                <CellStyle CssText="font-family:Tahoma;font-size:6pt;text-align:Center;" />           
                                            </eo:StaticColumn>--%>
  
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

                                        <asp:Button ID="cmd_Anamnese" runat="server" onclick="cmd_Anamnese_Click" tabIndex="1" 
                                            Text="Salvar Alterações na Anamnese" Width="209px" CssClass="buttonBox" BackColor="Silver" ForeColor="Black" />
                                        
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:LinkButton ID="lkbAnamnese" runat="server" SkinID="BoldLinkButton" Width="91px"><img border="0" src="img/print.gif"> Anamnese</img></asp:LinkButton>

       </eo:PageView>



                 </eo:MultiPage>


        <table>
            <tr>
                <td align="center" class="auto-style1">
                    <br />

                            <asp:Button ID="btnOK" runat="server" onclick="btnOK_Click" Text="Gravar"  CssClass="buttonBox"
                                Width="70px" Height="18px" />
                            &nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnExcluir" runat="server" onclick="btnExcluir_Click"  CssClass="buttonBox"
                                Text="Excluir" Width="70px" Height="18px" />
                    &nbsp;&nbsp;&nbsp;&nbsp; <b>
                    <asp:Button ID="btnemp" runat="server" CssClass="buttonBox" Height="18px" onclick="btnemp_Click" Text="Anamnese em Branco" Visible="False" Width="140px" />
                    </b>
                    <br />
                    </td>
                </tr>
            
                            
            <tr>
                <td align="center" class="auto-style1">
        
                            <input id="txtAuxAviso" type="hidden" runat="server"/>
                            <input id="txtCloseWindow" type="hidden" 
    runat="server"/>
                            <input id="txtExecutePost" 
    type="hidden" runat="server"/>
                           
                    <%--    </igmisc:WebAsyncRefreshPanel>--%>
                
                <%--</igmisc:WebAsyncRefreshPanel>--%>

                <asp:Button ID="cmd_Voltar" runat="server" BackColor="#999999" 
                                    CssClass="buttonFoto" Font-Size="XX-Small" onclick="cmd_Voltar_Click" 
                                    Text="Voltar" Width="127px" />
                </TD>
                </TR>
                </table>
            <input id="txtAuxiliar" runat="server" name="txtAuxiliar" type="hidden" />

    </eo:CallbackPanel>
       

        
         <eo:MsgBox ID="MsgBox1" runat="server" BackColor="#47729F" ControlSkinID="None" 
        HeaderHtml="Dialog Title" Height="100px" Width="168px">
        <HeaderStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #ffbf00 3px solid; padding-left: 4px; font-weight: bold; font-size: 11px; padding-bottom: 2px; color: white; padding-top: 2px; border-bottom: #22456a 1px solid; font-family: verdana" />
        <ContentStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #7d97b6 1px solid; padding-left: 4px; border-left-width: 1px; font-size: 11px; border-left-color: #728eb8; padding-bottom: 4px; color: white; padding-top: 4px; border-bottom: #22456a 1px solid; font-family: verdana" />
        <FooterStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #7d97b6 1px solid; padding-left: 4px; border-left-width: 1px; font-size: 11px; border-left-color: #728eb8; padding-bottom: 4px; color: white; padding-top: 4px; border-bottom: #22456a 1px solid; font-family: verdana" />
        </eo:MsgBox>
    </asp:Content>